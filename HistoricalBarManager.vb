Option Strict On
Option Explicit On

' ****************************************************************************************
' HistoricalBarManager.vb
' ----------------------------------------------------------------------------------------
' PURPOSE
'   Serial historical backfill orchestrator for a set of (ConId)s and a single timeframe.
'   - Builds a FIFO request queue to avoid IB pacing.
'   - Issues requests via a pluggable delegate (Requester).
'   - Receives IB callbacks (forwarded by TWSEvents) and writes bars into BarManager.
'   - Raises progress/completion events for MainForm to gate timers & market subs.
'
' PUBLIC FLOW
'   MainForm:
'     - Set HistoricalBarManager.Requester = AddressOf YourTwsWrapper.RequestHistoricalData
'     - Subscribe to BootProgress / BootCompleted
'     - Call StartBoot(conIds, barSize, duration, whatToShow, useRth, endUtc)
'
'   TWSEvents:
'     - TWSConn_historicalData(reqId, bar)      → HistoricalBarManager.HandleHistoricalBar(reqId, bar)
'     - TWSConn_historicalDataEnd(reqId, .. ..) → HistoricalBarManager.HandleEndOfHistoricalData(reqId)
'
' NOTES
'   - This module holds NO canonical series state; BarManager owns that.
'   - Time is normalized to UTC here once before passing to BarManager.
'   - It’s safe to call StartBoot again (e.g., timeframe switch); it resets internal state.
' ****************************************************************************************

Module HistoricalBarManager

    ' --------------------- Events for UI ---------------------

    ''' <summary>Progress event fired when a request finishes (done/total & current ConId).</summary>
    Public Event BootProgress(ByVal done As Integer, ByVal total As Integer, ByVal currentConId As Integer)

    ''' <summary>Completion event fired once after the final request finishes.</summary>
    Public Event BootCompleted()

    ' --------------------- Delegates / Types ---------------------

    ''' <summary>
    ''' Delegate to perform the actual IB historical request.
    ''' Must return the IB reqId assigned for the request.
    ''' </summary>
    Public Delegate Function HistoricalRequester(
        ByVal conId As Integer,
        ByVal endUtc As Date,
        ByVal durationStr As String,
        ByVal barSizeStr As String,
        ByVal whatToShow As String,
        ByVal useRth As Integer
    ) As Integer

    ''' <summary>
    ''' Light mapping of an incoming IB bar (we map reflectively from the wrapper’s Bar).
    ''' </summary>
    Public Class IbBar
        Public Property [Date] As Object  ' IB may send Date or String
        Public Property Open As Double
        Public Property High As Double
        Public Property Low As Double
        Public Property Close As Double
        Public Property Volume As Long
    End Class

    ' --------------------- Public configuration ---------------------

    ''' <summary>
    ''' Set this to your TWS wrapper method that issues the historical request.
    ''' Example: HistoricalBarManager.Requester = AddressOf Tws.RequestHistoricalData
    ''' </summary>
    Public Property Requester As HistoricalRequester

    ' --------------------- Internal state ---------------------

    ''' <summary>Queue item for each requested ConId.</summary>
    Private Class Req
        Public ConId As Integer
        Public EndUtc As Date
        Public DurationStr As String
        Public BarSizeStr As String
        Public WhatToShow As String
        Public UseRth As Integer
    End Class

    ''' <summary>Serial FIFO of pending requests.</summary>
    Private ReadOnly _queue As New Queue(Of Req)()

    ''' <summary>Map IB reqId → ConId for callback resolution.</summary>
    Private ReadOnly _reqIdToConId As New Dictionary(Of Integer, Integer)()

    ''' <summary>Synchronization gate for state.</summary>
    Private ReadOnly _sync As New Object()

    ''' <summary>Total number of requests in this boot cycle.</summary>
    Private _total As Integer = 0

    ''' <summary>Number of completed requests in this boot cycle.</summary>
    Private _done As Integer = 0

    ''' <summary>Whether a boot cycle is active.</summary>
    Private _active As Boolean = False

    ''' <summary>Current barSize string (e.g., "1 min", "5 mins").</summary>
    Private _currentBarSize As String = "1 min"

    ' --------------------- Public API ---------------------

    ''' <summary>
    ''' Start a serial historical backfill for the given ConIds and timeframe.
    ''' Resets any previous state; safe to call again (e.g., after timeframe change).
    ''' </summary>
    Public Sub StartBoot(conIds As IEnumerable(Of Integer),
                         barSize As String,
                         duration As String,
                         whatToShow As String,
                         useRth As Integer,
                         endUtc As Date)

        If conIds Is Nothing Then Throw New ArgumentNullException(NameOf(conIds))
        If Requester Is Nothing Then Throw New InvalidOperationException("HistoricalBarManager.Requester is not set. Assign your TWS historical request function before calling StartBoot.")

        SyncLock _sync
            _queue.Clear()
            _reqIdToConId.Clear()
            _total = 0 : _done = 0 : _active = True
            _currentBarSize = If(String.IsNullOrWhiteSpace(barSize), "1 min", barSize)

            For Each id In conIds
                If id > 0 Then
                    _queue.Enqueue(New Req With {
                        .ConId = id,
                        .EndUtc = endUtc,
                        .DurationStr = duration,
                        .BarSizeStr = _currentBarSize,
                        .WhatToShow = whatToShow,
                        .UseRth = useRth
                    })
                    _total += 1
                End If
            Next
        End SyncLock

        ' Kick off the first request
        RequestNext()
    End Sub

    ''' <summary>
    ''' Resolve a ConId for a given IB reqId (useful for logging/debug).
    ''' Returns 0 if unknown.
    ''' </summary>
    Public Function ResolveConId(reqId As Integer) As Integer
        SyncLock _sync
            If _reqIdToConId.ContainsKey(reqId) Then Return _reqIdToConId(reqId)
        End SyncLock
        Return 0
    End Function

    ''' <summary>
    ''' Forward a received historical bar from TWSEvents.
    ''' - Normalizes time to UTC.
    ''' - De-dupes against BarManager’s latest stored time for (ConId, timeframe).
    ''' - Appends into BarManager as a completed bar.
    ''' </summary>
    Public Sub HandleHistoricalBar(reqId As Integer, bar As Object)
        Dim conId As Integer = ResolveConId(reqId)
        If conId <= 0 Then Exit Sub ' Unknown reqId (late bar or out-of-cycle)

        Dim ib As IbBar = MapToIbBar(bar)
        If ib Is Nothing Then Exit Sub

        Dim tUtc As Date = ParseToUtc(ib.[Date])

        ' Drop duplicates/older bars
        Dim lastCompleted As Date? = BarManager.GetLastBarTimeUtc(conId, _currentBarSize)
        If lastCompleted.HasValue AndAlso tUtc <= lastCompleted.Value Then Exit Sub

        ' Append into canonical series store
        BarManager.AddBar(conId, _currentBarSize, tUtc, ib.Open, ib.High, ib.Low, ib.Close, ib.Volume)
    End Sub

    ''' <summary>
    ''' Forward the "end of historical data" signal from TWSEvents.
    ''' - Raises a progress event.
    ''' - Schedules the next request (with a small delay).
    ''' - Fires BootCompleted when the queue is exhausted.
    ''' </summary>
    Public Sub HandleEndOfHistoricalData(reqId As Integer)
        Dim conId As Integer = ResolveConId(reqId)
        Dim isDone As Boolean = False
        Dim nextReq As Req = Nothing

        SyncLock _sync
            If Not _active Then Exit Sub
            _done += 1
            _reqIdToConId.Remove(reqId)

            If _queue.Count > 0 Then
                nextReq = _queue.Dequeue()
            Else
                isDone = True
            End If
        End SyncLock

        ' Emit progress update outside lock
        RaiseEvent BootProgress(_done, _total, conId)

        If isDone Then
            _active = False
            RaiseEvent BootCompleted()
        Else
            ' Pace-friendly 250 ms delay before issuing next request
            Dim t As New System.Timers.Timer(250)
            AddHandler t.Elapsed, Sub(s, e)
                                      Dim tt = DirectCast(s, System.Timers.Timer)
                                      tt.Stop() : tt.Dispose()
                                      RequestOne(nextReq)
                                  End Sub
            t.AutoReset = False
            t.Start()
        End If
    End Sub

    ' --------------------- Internals ---------------------

    ''' <summary>
    ''' Dequeue and request the next historical batch, or complete immediately if none pending.
    ''' </summary>
    Private Sub RequestNext()
        Dim req As Req = Nothing
        SyncLock _sync
            If Not _active Then Exit Sub
            If _queue.Count > 0 Then req = _queue.Dequeue()
        End SyncLock

        If req Is Nothing Then
            ' Nothing to do; consider the boot complete.
            RaiseEvent BootCompleted()
            Exit Sub
        End If

        RequestOne(req)
    End Sub

    ''' <summary>
    ''' Perform a single historical data request via Requester delegate and track reqId → ConId.
    ''' </summary>
    Private Sub RequestOne(r As Req)
        Dim rid As Integer = Requester.Invoke(r.ConId, r.EndUtc, r.DurationStr, r.BarSizeStr, r.WhatToShow, r.UseRth)
        SyncLock _sync
            _reqIdToConId(rid) = r.ConId
        End SyncLock
    End Sub

    ''' <summary>
    ''' Map an incoming wrapper "bar" object to IbBar. Uses reflection for flexibility.
    ''' If your wrapper has different property names, adjust the Property lookups here.
    ''' </summary>
    Private Function MapToIbBar(obj As Object) As IbBar
        If obj Is Nothing Then Return Nothing
        If TypeOf obj Is IbBar Then Return DirectCast(obj, IbBar)

        Dim t = obj.GetType()
        Dim ib As New IbBar()

        Dim pDate = t.GetProperty("Date")
        Dim pOpen = t.GetProperty("Open")
        Dim pHigh = t.GetProperty("High")
        Dim pLow = t.GetProperty("Low")
        Dim pClose = t.GetProperty("Close")
        Dim pVol = t.GetProperty("Volume")

        If pDate Is Nothing OrElse pOpen Is Nothing OrElse pHigh Is Nothing _
           OrElse pLow Is Nothing OrElse pClose Is Nothing OrElse pVol Is Nothing Then
            Return Nothing
        End If

        ib.[Date] = pDate.GetValue(obj, Nothing)
        ib.Open = CDbl(pOpen.GetValue(obj, Nothing))
        ib.High = CDbl(pHigh.GetValue(obj, Nothing))
        ib.Low = CDbl(pLow.GetValue(obj, Nothing))
        ib.Close = CDbl(pClose.GetValue(obj, Nothing))
        ib.Volume = CLng(pVol.GetValue(obj, Nothing))
        Return ib
    End Function

    ''' <summary>
    ''' Parse an IB date (String or Date) to UTC Date.
    ''' Fallback is UTC Now if unparsable (should be rare).
    ''' </summary>
    Private Function ParseToUtc(v As Object) As Date
        If TypeOf v Is Date Then
            Return Date.SpecifyKind(DirectCast(v, Date), DateTimeKind.Utc).ToUniversalTime()
        End If
        Dim s As String = Convert.ToString(v)
        Dim dt As Date
        If Date.TryParse(s, dt) Then
            Return Date.SpecifyKind(dt, DateTimeKind.Utc)
        End If
        Return Date.UtcNow
    End Function

End Module
