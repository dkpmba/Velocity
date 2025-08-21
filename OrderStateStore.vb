Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports IBApi

' =============================================================================
' OrderStateStore
' -----------------------------------------------------------------------------
' • Authoritative in-memory state for orders & executions (no market data).
' • Event sinks to be called from TWSEvents:
'     - OnOpenOrder(orderId, contract, order, state)
'     - OnOrderStatus(...)
'     - OnExecDetails(contract, execution)
'     - OnCommissionReport(report As CommissionAndFeesReport)
' • Exposes OrdersBinding (BindingList) for UI grids.
' • Thread-safe via a single SyncLock.
' =============================================================================
Module OrderStateStore

    ' =========================
    ' Model (one row per order)
    ' =========================
    Public Class OrderRow
        Public Property OrderId As Integer
        Public Property PermId As Long
        Public Property ClientId As Integer
        Public Property ParentId As Integer

        Public Property ConId As Integer
        Public Property SecType As String
        Public Property Symbol As String

        Public Property Side As String          ' BUY / SELL
        Public Property OrderType As String     ' LMT / MKT / STP / etc
        Public Property Tif As String           ' DAY / GTC / etc
        Public Property TotalQty As Integer
        Public Property LmtPrice As Double?
        Public Property AuxPrice As Double?

        Public Property Status As String        ' normalized status text
        Public Property FilledQty As Integer
        Public Property AvgFillPrice As Double
        Public Property LeavesQty As Integer

        Public Property Commission As Double    ' commission + fees (new IB API)
        Public Property RealizedPnl As Double   ' if reported by commission report

        Public Property Source As String        ' UI / DHUL / etc
        Public Property Note As String
        Public Property LastUpdateUtc As Date

        ' --- Columns expected by your existing Orders grid ---
        Public Property Account As String
        Public Property Exchange As String
        Public Property [Error] As String

        Public Property Time As Date            ' bind "Time" column here (updated on events)

        Public ReadOnly Property OID As Integer
            Get
                Return OrderId
            End Get
        End Property

        Public ReadOnly Property TID As Long
            Get
                Return PermId
            End Get
        End Property

        Public Property Qty As Integer          ' alias for TotalQty (kept in sync)
        Public Property Price As Double?        ' alias for LmtPrice (Nothing for MKT)
        Public Property Fills As Integer        ' alias for FilledQty
        Public Property Remaining As Integer    ' alias for LeavesQty

        Public ReadOnly Property Display As String
            Get
                Dim px As String = If(LmtPrice.HasValue, LmtPrice.Value.ToString("G"), "-")
                Return $"{OrderId}:{Symbol} {Side} {TotalQty} {OrderType}@{px} [{Status}]"
            End Get
        End Property
    End Class

    ' ===================
    ' Storage & Bindings
    ' ===================
    Private ReadOnly _sync As New Object()
    Private ReadOnly _byOrderId As New Dictionary(Of Integer, OrderRow)()
    Private ReadOnly _byPermId As New Dictionary(Of Long, OrderRow)()
    Private ReadOnly _orderIdByExecId As New Dictionary(Of String, Integer)(StringComparer.Ordinal)

    Public ReadOnly OrdersBinding As New BindingList(Of OrderRow)()

    ' ------------
    ' Query / Util
    ' ------------
    Public Function TryGet(orderId As Integer, ByRef row As OrderRow) As Boolean
        SyncLock _sync
            Return _byOrderId.TryGetValue(orderId, row)
        End SyncLock
    End Function

    Public Function All() As IEnumerable(Of OrderRow)
        SyncLock _sync
            Return _byOrderId.Values.ToList()
        End SyncLock
    End Function

    Public Sub ClearAll()
        SyncLock _sync
            _byOrderId.Clear()
            _byPermId.Clear()
            _orderIdByExecId.Clear()
            OrdersBinding.Clear()
        End SyncLock
    End Sub

    ' ============================================
    ' Local pre-submit snapshot (called by Router)
    ' ============================================
    Public Sub LocalSubmitSnapshot(contract As Contract, order As IBApi.Order, note As String, source As String)
        Dim r As New OrderRow With {
            .OrderId = order.OrderId,
            .PermId = CLng(order.PermId),
            .ClientId = order.ClientId,
            .ParentId = order.ParentId,
            .ConId = contract.ConId,
            .SecType = contract.SecType,
            .Symbol = contract.Symbol,
            .Side = order.Action,
            .OrderType = order.OrderType,
            .Tif = order.Tif,
            .TotalQty = CInt(order.TotalQuantity),
            .LmtPrice = NzNullable(order.LmtPrice),
            .AuxPrice = NzNullable(order.AuxPrice),
            .Status = "PendingSubmit",
            .FilledQty = 0,
            .AvgFillPrice = 0,
            .LeavesQty = CInt(order.TotalQuantity),
            .Commission = 0,
            .RealizedPnl = 0,
            .Source = source,
            .Note = note,
            .LastUpdateUtc = Date.UtcNow
        }

        ' Grid-facing aliases & extras
        r.Account = order.Account
        r.Exchange = If(String.IsNullOrWhiteSpace(contract.PrimaryExch), contract.Exchange, contract.PrimaryExch)
        r.Time = Date.UtcNow
        r.Qty = r.TotalQty
        r.Price = r.LmtPrice
        r.Fills = 0
        r.Remaining = r.TotalQty

        Upsert(r)
    End Sub

    Public Sub MarkPendingChange(orderId As Integer, note As String)
        SyncLock _sync
            Dim r As OrderRow = Nothing
            If _byOrderId.TryGetValue(orderId, r) Then
                r.Status = "PendingChange"
                r.Note = note
                r.LastUpdateUtc = Date.UtcNow
                r.Time = r.LastUpdateUtc
            End If
        End SyncLock
    End Sub

    Public Sub MarkPendingCancel(orderId As Integer, note As String)
        SyncLock _sync
            Dim r As OrderRow = Nothing
            If _byOrderId.TryGetValue(orderId, r) Then
                r.Status = "PendingCancel"
                r.Note = note
                r.LastUpdateUtc = Date.UtcNow
                r.Time = r.LastUpdateUtc
            End If
        End SyncLock
    End Sub

    ' ======================
    ' IB Event Sinks (TWS)
    ' ======================

    ' openOrder: authoritative snapshot of a working order
    Public Sub OnOpenOrder(orderId As Integer, c As Contract, o As IBApi.Order, state As IBApi.OrderState)
        Dim r As New OrderRow With {
            .OrderId = orderId,
            .PermId = CLng(o.PermId),
            .ClientId = o.ClientId,
            .ParentId = o.ParentId,
            .ConId = c.ConId,
            .SecType = c.SecType,
            .Symbol = c.Symbol,
            .Side = o.Action,
            .OrderType = o.OrderType,
            .Tif = o.Tif,
            .TotalQty = CInt(o.TotalQuantity),
            .LmtPrice = NzNullable(o.LmtPrice),
            .AuxPrice = NzNullable(o.AuxPrice),
            .Status = NormalizeStatus(If(state IsNot Nothing, state.Status, Nothing)),
            .LastUpdateUtc = Date.UtcNow
        }

        ' Grid-facing
        r.Account = o.Account
        r.Exchange = If(String.IsNullOrWhiteSpace(c.PrimaryExch), c.Exchange, c.PrimaryExch)
        r.Time = Date.UtcNow
        r.Qty = CInt(o.TotalQuantity)
        r.Price = r.LmtPrice
        r.Fills = 0
        r.Remaining = r.Qty

        Upsert(r)
    End Sub

    ' orderStatus: updates live status + fill progress
    Public Sub OnOrderStatus(orderId As Integer,
                             status As String,
                             filled As Double,
                             remaining As Double,
                             avgFillPrice As Double,
                             permId As Integer,
                             parentId As Integer,
                             lastFillPrice As Double,
                             clientId As Integer,
                             whyHeld As String,
                             mktCapPrice As Double)

        SyncLock _sync
            Dim r As OrderRow = Nothing
            If Not _byOrderId.TryGetValue(orderId, r) Then
                r = New OrderRow With {.OrderId = orderId}
                _byOrderId(orderId) = r
                OrdersBinding.Add(r)
            End If

            r.Status = NormalizeStatus(status)
            r.FilledQty = SafeInt(filled)
            r.LeavesQty = SafeInt(remaining)
            If avgFillPrice > 0 Then r.AvgFillPrice = avgFillPrice

            If permId > 0 Then
                r.PermId = permId
                _byPermId(permId) = r
            End If

            r.ParentId = parentId
            r.ClientId = clientId
            r.LastUpdateUtc = Date.UtcNow

            ' keep grid aliases in sync
            r.Fills = r.FilledQty
            r.Remaining = r.LeavesQty
            r.Qty = Math.Max(r.TotalQty, r.FilledQty + r.LeavesQty)
            r.Price = r.LmtPrice
            r.Time = r.LastUpdateUtc
        End SyncLock
    End Sub

    ' execDetails: attach execution effects to the owning order
    Public Sub OnExecDetails(c As Contract, exec As Execution)
        Dim shares As Integer = SafeInt(exec.Shares)
        Dim px As Double = exec.Price
        Dim oid As Integer = exec.OrderId

        SyncLock _sync
            Dim r As OrderRow = Nothing
            If Not _byOrderId.TryGetValue(oid, r) Then
                r = New OrderRow With {.OrderId = oid}
                _byOrderId(oid) = r
                OrdersBinding.Add(r)
            End If

            ' Map execId → orderId so commission handler can find the order
            If Not String.IsNullOrEmpty(exec.ExecId) Then
                _orderIdByExecId(exec.ExecId) = oid
            End If

            ' Attach basic contract info
            If c IsNot Nothing Then
                r.ConId = c.ConId
                r.SecType = c.SecType
                r.Symbol = c.Symbol
                r.Exchange = If(String.IsNullOrWhiteSpace(c.PrimaryExch), c.Exchange, c.PrimaryExch)
            End If

            ' Progress
            If r.TotalQty <= 0 Then r.TotalQty = shares ' best-effort if unknown
            Dim prevFilled As Integer = r.FilledQty
            r.FilledQty = Math.Max(prevFilled + shares, 0)
            r.LeavesQty = Math.Max(r.TotalQty - r.FilledQty, 0)
            r.AvgFillPrice = WeightedAvg(prevFilled, r.AvgFillPrice, shares, px)
            r.Status = If(r.FilledQty >= r.TotalQty AndAlso r.TotalQty > 0, "Filled", "PartiallyFilled")
            r.LastUpdateUtc = Date.UtcNow

            ' grid aliases
            r.Fills = r.FilledQty
            r.Remaining = r.LeavesQty
            r.Qty = Math.Max(r.TotalQty, r.FilledQty + r.LeavesQty)
            r.Price = r.LmtPrice
            r.Time = r.LastUpdateUtc
        End SyncLock
    End Sub

    ' New IB API: Commission + Fees
    Public Sub OnCommissionReport(report As CommissionAndFeesReport)
        If report Is Nothing Then Exit Sub

        Dim execId As String = report.ExecId
        Dim amt As Double = report.CommissionAndFees
        Dim realized As Double = If(Double.IsNaN(report.RealizedPNL), 0.0, report.RealizedPNL)

        If String.IsNullOrWhiteSpace(execId) Then Exit Sub

        SyncLock _sync
            Dim oid As Integer = 0
            If _orderIdByExecId.TryGetValue(execId, oid) Then
                Dim r As OrderRow = Nothing
                If _byOrderId.TryGetValue(oid, r) Then
                    r.Commission += amt
                    r.RealizedPnl += realized
                    r.LastUpdateUtc = Date.UtcNow
                    r.Time = r.LastUpdateUtc
                End If
            End If
        End SyncLock
    End Sub

    ' Add IB error hook (optional)
    Public Sub OnOrderError(orderId As Integer, message As String)
        If orderId <= 0 OrElse String.IsNullOrWhiteSpace(message) Then Exit Sub
        SyncLock _sync
            Dim r As OrderRow = Nothing
            If _byOrderId.TryGetValue(orderId, r) Then
                r.Error = message
                r.LastUpdateUtc = Date.UtcNow
                r.Time = r.LastUpdateUtc
            End If
        End SyncLock
    End Sub

    ' =============
    ' Internals
    ' =============
    Private Sub Upsert(r As OrderRow)
        SyncLock _sync
            Dim existing As OrderRow = Nothing
            If _byOrderId.TryGetValue(r.OrderId, existing) Then
                CopyInto(existing, r)
            Else
                _byOrderId(r.OrderId) = r
                If r.PermId > 0 Then _byPermId(r.PermId) = r
                OrdersBinding.Add(r)
            End If
        End SyncLock
    End Sub

    Private Sub CopyInto(dst As OrderRow, src As OrderRow)
        ' IDs / parents
        If src.PermId > 0 Then dst.PermId = src.PermId
        If src.ClientId > 0 Then dst.ClientId = src.ClientId
        If src.ParentId > 0 Then dst.ParentId = src.ParentId

        ' Contract
        If src.ConId > 0 Then dst.ConId = src.ConId
        If Not String.IsNullOrWhiteSpace(src.SecType) Then dst.SecType = src.SecType
        If Not String.IsNullOrWhiteSpace(src.Symbol) Then dst.Symbol = src.Symbol
        If Not String.IsNullOrWhiteSpace(src.Exchange) Then dst.Exchange = src.Exchange

        ' Side / type / tif
        If Not String.IsNullOrWhiteSpace(src.Side) Then dst.Side = src.Side
        If Not String.IsNullOrWhiteSpace(src.OrderType) Then dst.OrderType = src.OrderType
        If Not String.IsNullOrWhiteSpace(src.Tif) Then dst.Tif = src.Tif

        ' Qty / prices
        If src.TotalQty > 0 Then dst.TotalQty = src.TotalQty
        If src.LmtPrice.HasValue Then dst.LmtPrice = src.LmtPrice
        If src.AuxPrice.HasValue Then dst.AuxPrice = src.AuxPrice

        ' Status / progress
        If Not String.IsNullOrWhiteSpace(src.Status) Then dst.Status = src.Status
        If src.FilledQty > 0 Then dst.FilledQty = src.FilledQty
        If src.LeavesQty >= 0 Then dst.LeavesQty = src.LeavesQty
        If src.AvgFillPrice > 0 Then dst.AvgFillPrice = src.AvgFillPrice

        ' Money
        If src.Commission > 0 Then dst.Commission = Math.Max(dst.Commission, src.Commission)
        If src.RealizedPnl <> 0 Then dst.RealizedPnl = dst.RealizedPnl + src.RealizedPnl

        ' Grid aliases / misc
        If Not String.IsNullOrWhiteSpace(src.Account) Then dst.Account = src.Account
        If Not String.IsNullOrWhiteSpace(src.Note) Then dst.Note = src.Note
        If Not String.IsNullOrWhiteSpace(src.Error) Then dst.Error = src.Error

        If src.Qty > 0 Then dst.Qty = src.Qty
        If src.Price.HasValue Then dst.Price = src.Price
        If src.Fills > 0 Then dst.Fills = src.Fills
        If src.Remaining >= 0 Then dst.Remaining = src.Remaining

        If src.LastUpdateUtc > dst.LastUpdateUtc Then
            dst.LastUpdateUtc = src.LastUpdateUtc
            dst.Time = src.LastUpdateUtc
        End If
    End Sub

    Private Function NormalizeStatus(ib As String) As String
        If String.IsNullOrWhiteSpace(ib) Then Return "Unknown"
        Select Case ib.Trim().ToLowerInvariant()
            Case "pendingsubmit" : Return "PendingSubmit"
            Case "presubmitted" : Return "PreSubmitted"
            Case "submitted" : Return "Submitted"
            Case "partiallyfilled" : Return "PartiallyFilled"
            Case "filled" : Return "Filled"
            Case "cancelled" : Return "Cancelled"
            Case "inactive" : Return "Inactive"
            Case Else : Return ib
        End Select
    End Function

    Private Function SafeInt(v As Double) As Integer
        Dim n As Integer
        Try
            n = CInt(Math.Round(v))
        Catch
            n = 0
        End Try
        If n < 0 Then n = 0
        Return n
    End Function

    Private Function NzNullable(v As Double) As Double?
        If Double.IsNaN(v) OrElse v = 0 Then
            Return Nothing
        End If
        Return v
    End Function

    Private Function WeightedAvg(prevQty As Integer, prevAvg As Double, addQty As Integer, addPx As Double) As Double
        If addQty <= 0 Then Return prevAvg
        If prevQty <= 0 OrElse prevAvg <= 0 Then Return addPx
        Dim tot As Integer = prevQty + addQty
        Return ((prevQty * prevAvg) + (addQty * addPx)) / tot
    End Function


End Module
