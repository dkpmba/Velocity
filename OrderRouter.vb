Option Strict On
Option Explicit On

Imports System.Threading
Imports IBApi

Module OrderRouter

    ' ===== Order ID allocator =====
    ' IB provides nextValidId; we seed to (id-1) then Interlocked.Increment for each NextOrderId().
    Private _nextOrderId As Integer = 0
    Private _seeded As Boolean = False
    Private ReadOnly _sync As New Object()

    ''' <summary>Seed from TWS nextValidId (call once per session).</summary>
    Public Sub SeedNextValidId(firstValidId As Integer)
        SyncLock _sync
            If Not _seeded OrElse firstValidId > _nextOrderId Then
                _nextOrderId = firstValidId - 1
                _seeded = True
            End If
        End SyncLock
    End Sub

    ''' <summary>Reserve the next unique orderId.</summary>
    Public Function NextOrderId() As Integer
        If Not _seeded Then Throw New InvalidOperationException("OrderRouter not seeded (nextValidId not received).")
        Return Interlocked.Increment(_nextOrderId)
    End Function

    ' ===== Duplicate-click guard (lightweight) =====
    ' Tracks recent "signature" of submits to avoid accidental double-clicks.
    Private ReadOnly _recentSends As New Dictionary(Of String, Date)()
    Private ReadOnly _recentSync As New Object()
    Private Const RecentHoldMs As Integer = 2000

    Private Function BuildSig(conId As Integer, side As String, qty As Integer, orderType As String, lmt As Double?, aux As Double?) As String
        Return $"{conId}|{side}|{qty}|{orderType}|{If(lmt.HasValue, lmt.Value.ToString("G"), "_")}|{If(aux.HasValue, aux.Value.ToString("G"), "_")}"
    End Function

    Private Function IsRecent(sig As String) As Boolean
        SyncLock _recentSync
            Dim now = Date.UtcNow
            ' purge old
            Dim kills As New List(Of String)
            For Each kvp In _recentSends
                If (now - kvp.Value).TotalMilliseconds > RecentHoldMs Then kills.Add(kvp.Key)
            Next
            For Each k In kills : _recentSends.Remove(k) : Next

            If _recentSends.ContainsKey(sig) Then Return True
            _recentSends(sig) = now
            Return False
        End SyncLock
    End Function

    ' ===== Outbound API =====

    ''' <summary>
    ''' Place a new order. Will compute an orderId if not provided (order.OrderId=0).
    ''' Records a local "PendingSubmit" snapshot in OrderStateStore before sending.
    ''' </summary>
    Public Sub Place(contract As Contract, order As IBApi.Order, note As String, Optional source As String = "UI", Optional tradeId As Integer? = Nothing)
        If TwsHost.Tws Is Nothing OrElse TwsHost.Tws.ClientSocket Is Nothing Then
            Throw New InvalidOperationException("TWS is not connected.")
        End If
        If order Is Nothing Then Throw New ArgumentNullException(NameOf(order))
        If contract Is Nothing Then Throw New ArgumentNullException(NameOf(contract))

        ' Guard against duplicate rapid submits (best-effort)
        Dim sig = BuildSig(contract.ConId, If(order.Action, ""), CInt(order.TotalQuantity), If(order.OrderType, ""), IfNullable(order.LmtPrice), IfNullable(order.AuxPrice))
        If IsRecent(sig) Then
            Debug.WriteLine($"[OrderRouter] Ignored duplicate submit: {sig}")
            Exit Sub
        End If

        ' Allocate id if needed
        If order.OrderId <= 0 Then order.OrderId = NextOrderId()

        ' Local state (PendingSubmit)
        OrderStateStore.LocalSubmitSnapshot(contract, order, note, source)

        ' Send to IB
        TwsHost.Tws.ClientSocket.placeOrder(order.OrderId, contract, order)
    End Sub

    ''' <summary>
    ''' Modify an existing order by re-sending placeOrder with the same orderId and changed fields.
    ''' </summary>
    Public Sub Replace(orderId As Integer, contract As Contract, order As IBApi.Order, note As String)
        If TwsHost.Tws Is Nothing OrElse TwsHost.Tws.ClientSocket Is Nothing Then
            Throw New InvalidOperationException("TWS is not connected.")
        End If
        If order Is Nothing Then Throw New ArgumentNullException(NameOf(order))
        If contract Is Nothing Then Throw New ArgumentNullException(NameOf(contract))
        If orderId <= 0 Then Throw New ArgumentOutOfRangeException(NameOf(orderId))

        order.OrderId = orderId
        OrderStateStore.MarkPendingChange(orderId, note)
        TwsHost.Tws.ClientSocket.placeOrder(orderId, contract, order)
    End Sub

    ''' <summary>Cancel a working order.</summary>
    Public Sub Cancel(orderId As Integer, Optional note As String = "")
        If TwsHost.Tws Is Nothing OrElse TwsHost.Tws.ClientSocket Is Nothing Then
            Throw New InvalidOperationException("TWS is not connected.")
        End If
        If orderId <= 0 Then Throw New ArgumentOutOfRangeException(NameOf(orderId))

        OrderStateStore.MarkPendingCancel(orderId, note)

        ' OLD (compile error):
        ' TwsHost.Tws.ClientSocket.cancelOrder(orderId)

        ' NEW (pass an empty OrderCancel):
        TwsHost.Tws.ClientSocket.cancelOrder(orderId, New OrderCancel())
    End Sub


    ' ===== Helpers =====
    Private Function IfNullable(v As Double) As Double?
        If Double.IsNaN(v) OrElse v = 0 Then Return Nothing
        Return v
    End Function

End Module
