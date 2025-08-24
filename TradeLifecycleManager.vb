Option Strict On
Option Explicit On

' Tracks per-trade exposure (Σ signed qty across legs) and whether any working orders remain.
' When exposure==0 and no working orders, raises TradeClosed(tradeId, finalRgl).
Module TradeLifecycleManager

    Private ReadOnly _sync As New Object()

    ' TradeId -> net exposure (signed sum of leg qty)
    Private ReadOnly _exposure As New Dictionary(Of Integer, Integer)()

    ' TradeId -> count of working orders (Submitted/PreSubmitted/PartiallyFilled/Pending*)
    Private ReadOnly _workingByTrade As New Dictionary(Of Integer, Integer)()

    ' OrderId -> last-known "is working?" to avoid double-counting
    Private ReadOnly _orderWorking As New Dictionary(Of Integer, Boolean)()

    Public Event TradeClosed(tradeId As Integer, finalRgl As Double)

    ' ===== Exposure =====

    ' Replace exposure snapshot for a trade (used to seed from monitor after boot)
    Public Sub SetExposure(tradeId As Integer, exposure As Integer)
        If tradeId <= 0 Then Exit Sub
        SyncLock _sync
            _exposure(tradeId) = exposure
            TryCloseIfEligible_NoLock(tradeId)
        End SyncLock
    End Sub

    ' Execution delta: BUY=+shares, SELL=-shares
    Public Sub OnExec(tradeId As Integer?, side As String, shares As Integer)
        If Not tradeId.HasValue OrElse tradeId.Value <= 0 OrElse shares <= 0 Then Exit Sub
        Dim delta As Integer = shares * If(String.Equals(side, "BUY", StringComparison.OrdinalIgnoreCase), +1, -1)
        SyncLock _sync
            Dim cur As Integer = 0
            _exposure.TryGetValue(tradeId.Value, cur)
            _exposure(tradeId.Value) = cur + delta
            TryCloseIfEligible_NoLock(tradeId.Value)
        End SyncLock
    End Sub

    Public Function GetExposure(tradeId As Integer) As Integer
        SyncLock _sync
            Dim v As Integer = 0
            _exposure.TryGetValue(tradeId, v)
            Return v
        End SyncLock
    End Function

    ' ===== Working orders =====

    ' Call from OrderStateStore.OnOpenOrder + OnOrderStatus with the latest snapshot of an order row
    Public Sub OnOrderSnapshot(r As OrderStateStore.OrderRow)
        If r Is Nothing Then Exit Sub
        Dim tid As Integer = If(r.TradeId.HasValue, r.TradeId.Value, 0)
        If tid <= 0 Then Exit Sub

        Dim isWorking As Boolean = IsWorkingStatus(r.Status)

        SyncLock _sync
            Dim prev As Boolean = False
            _orderWorking.TryGetValue(r.OrderId, prev)

            If isWorking <> prev Then
                _orderWorking(r.OrderId) = isWorking
                Dim cur As Integer = 0
                _workingByTrade.TryGetValue(tid, cur)
                cur += If(isWorking, +1, -1)
                If cur < 0 Then cur = 0
                _workingByTrade(tid) = cur
            End If

            TryCloseIfEligible_NoLock(tid)
        End SyncLock
    End Sub

    Public Function HasWorkingOrders(tradeId As Integer) As Boolean
        SyncLock _sync
            Dim c As Integer = 0
            _workingByTrade.TryGetValue(tradeId, c)
            Return c > 0
        End SyncLock
    End Function

    Private Function IsWorkingStatus(st As String) As Boolean
        If String.IsNullOrWhiteSpace(st) Then Return False
        Select Case st.Trim().ToLowerInvariant()
            Case "pendingsubmit", "pendingchange", "presubmitted", "submitted", "partiallyfilled"
                Return True
            Case Else
                Return False
        End Select
    End Function

    ' ===== Closure check =====

    Private Sub TryCloseIfEligible_NoLock(tradeId As Integer)
        Dim exp As Integer = 0
        _exposure.TryGetValue(tradeId, exp)
        If exp <> 0 Then Exit Sub

        Dim wk As Integer = 0
        _workingByTrade.TryGetValue(tradeId, wk)
        If wk <> 0 Then Exit Sub

        ' All flat & no working orders → Closed
        Dim finalRgl As Double = TradePnLManager.GetRgl(tradeId)
        RaiseEvent TradeClosed(tradeId, finalRgl)
    End Sub

    ' ===== Utilities =====

    ' Recompute working counts from a full list (optional one-shot, e.g., after reconnect/boot)
    Public Sub RecomputeWorkingFrom(orders As IEnumerable(Of OrderStateStore.OrderRow))
        If orders Is Nothing Then Exit Sub
        SyncLock _sync
            _workingByTrade.Clear()
            _orderWorking.Clear()
            For Each r In orders
                If r Is Nothing OrElse Not r.TradeId.HasValue OrElse r.TradeId.Value <= 0 Then Continue For
                Dim tid = r.TradeId.Value
                If IsWorkingStatus(r.Status) Then
                    Dim c As Integer = 0 : _workingByTrade.TryGetValue(tid, c)
                    _workingByTrade(tid) = c + 1
                    _orderWorking(r.OrderId) = True
                End If
            Next
        End SyncLock
    End Sub

End Module

