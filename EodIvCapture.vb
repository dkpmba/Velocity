Option Strict On
Option Explicit On

Imports System.Windows.Forms
Imports IBApi
Imports Velocity.Core

' Captures end-of-day IV for open OPT/FOP positions by issuing SNAPSHOT reqMktData (genericTick "106")
' around 15:59:45 America/Toronto. Emits EodIvCaptured(conId, iv, asOfUtc) for persistence.
Module EodIvCapture

    Public Event EodIvCaptured(conId As Integer, iv As Double, asOfUtc As Date)

    Private ReadOnly _sync As New Object()
    Private _timer As Timer
    Private _targetLocalTime As TimeSpan = New TimeSpan(15, 59, 45) ' 15:59:45 local (ET)

    ' tickerId -> conId
    Private ReadOnly _pending As New Dictionary(Of Integer, Integer)()
    Private _nextTid As Integer = 9_000_000 ' separate range to avoid collisions

    Public Sub Start()
        If _timer Is Nothing Then
            _timer = New Timer()
            AddHandler _timer.Tick, AddressOf OnTick
        End If
        ScheduleNext()
    End Sub

    Public Sub [Stop]()
        If _timer IsNot Nothing Then
            RemoveHandler _timer.Tick, AddressOf OnTick
            _timer.Stop()
            _timer.Dispose()
            _timer = Nothing
        End If
    End Sub

    Private Sub OnTick(sender As Object, e As EventArgs)
        If _timer Is Nothing Then Exit Sub
        _timer.Stop()
        Try
            BeginCapture()
        Catch ex As Exception
            Debug.WriteLine("EodIvCapture.BeginCapture error: " & ex.Message)
        End Try
        ScheduleNext()
    End Sub

    Private Sub ScheduleNext()
        If _timer Is Nothing Then Exit Sub
        Dim nowLocal = DateTime.Now
        Dim todayTarget = nowLocal.Date + _targetLocalTime
        Dim nextRun = If(nowLocal <= todayTarget, todayTarget, todayTarget.AddDays(1))
        Dim ms As Double = (nextRun - nowLocal).TotalMilliseconds
        If ms < 1000 Then ms = 1000
        If ms > Integer.MaxValue Then ms = Integer.MaxValue
        _timer.Interval = CInt(ms)
        _timer.Start()
        Debug.WriteLine($"EOD IV scheduled for {nextRun.ToString("yyyy-MM-dd HH:mm:ss")}")
    End Sub

    Private Sub BeginCapture()
        If TwsHost.Tws Is Nothing OrElse TwsHost.Tws.ClientSocket Is Nothing Then
            Debug.WriteLine("EodIvCapture: TWS not connected; skipping run.")
            Return
        End If

        ' Gather OPT/FOP conIds with non-zero position
        Dim conIds As List(Of Integer) = GetOpenOptionConIds()
        If conIds.Count = 0 Then
            Debug.WriteLine("EodIvCapture: No open option positions.")
            Return
        End If

        Dim count As Integer = 0
        For Each cid In conIds
            Dim ct As Contract = TryGetContract(cid)
            If ct Is Nothing Then Continue For

            Dim tid As Integer = AllocateTid()
            SyncLock _sync
                _pending(tid) = cid
            End SyncLock

            ' Request SNAPSHOT market data for option computation (generic tick 106 = Option Implied Vol)
            ' Note: snapshot:=True, regulatorySnapshot:=False
            Try
                TwsHost.Tws.ClientSocket.reqMktData(tid, ct, "106", True, False, Nothing)
                count += 1
            Catch ex As Exception
                Debug.WriteLine($"EodIvCapture: reqMktData failed for {cid}: {ex.Message}")
                SyncLock _sync
                    _pending.Remove(tid)
                End SyncLock
            End Try
        Next
        Debug.WriteLine($"EodIvCapture: requested IV snapshot for {count} option legs.")
    End Sub

    Private Function AllocateTid() As Integer
        SyncLock _sync
            _nextTid += 1
            Return _nextTid
        End SyncLock
    End Function

    ' Public entry for TWSEvents.tickOptionComputation to forward impliedVol for our snapshot requests
    Public Sub OnTickOptionComputation(tickerId As Integer, impliedVol As Double)
        Dim conId As Integer = 0
        SyncLock _sync
            If Not _pending.TryGetValue(tickerId, conId) Then Return
            _pending.Remove(tickerId)
        End SyncLock

        Dim iv As Double = If(Double.IsNaN(impliedVol) OrElse impliedVol <= 0, 0, impliedVol)
        If iv > 5 Then
            ' IB sometimes returns IV in percent (e.g., 25 meaning 2500%). Normalize if obviously wrong.
            iv /= 100.0
        End If

        RaiseEvent EodIvCaptured(conId, iv, Date.UtcNow)
    End Sub

    ' ===== helpers to gather positions & contracts =====

    Private Function GetOpenOptionConIds() As List(Of Integer)
        Dim list As New List(Of Integer)()
        Dim snaps = PositionStore.GetAll()
        For Each s In snaps
            If s Is Nothing Then Continue For
            If s.Qty = 0 Then Continue For
            Dim st = (If(s.SecType, "")).ToUpperInvariant()
            If st = "OPT" OrElse st = "FOP" Then
                list.Add(s.ConId)
            End If
        Next
        Return list.Distinct().ToList()
    End Function

    Private Function TryGetContract(conId As Integer) As Contract
        Try
            ' We’ve been maintaining SqlSymbolRepository.dictContract earlier; use it if available
            Dim dict = SqlSymbolRepository.dictContract
            If dict IsNot Nothing AndAlso dict.ContainsKey(conId) Then
                Dim baseCt = dict(conId)
                ' Clone minimal fields for reqMktData
                Return New Contract() With {
                    .ConId = baseCt.ConId,
                    .SecType = baseCt.SecType,
                    .Symbol = baseCt.Symbol,
                    .Currency = baseCt.Currency,
                    .Exchange = If(String.IsNullOrWhiteSpace(baseCt.Exchange), "SMART", baseCt.Exchange),
                    .LastTradeDateOrContractMonth = baseCt.LastTradeDateOrContractMonth,
                    .Strike = baseCt.Strike,
                    .Right = baseCt.Right,
                    .Multiplier = baseCt.Multiplier,
                    .IncludeExpired = False
                }
            End If
        Catch
        End Try
        Return Nothing
    End Function

End Module
