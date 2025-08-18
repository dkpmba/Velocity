Option Strict On
Option Explicit On

' ****************************************************************************************
' BarManager.vb
' ----------------------------------------------------------------------------------------
' PURPOSE
'   Single source of truth for time series (OHLCV) per (ConId, Timeframe).
'   - Stores completed historical bars appended by HistoricalBarManager.
'   - Maintains/updates the "current" in-progress bar from live ticks.
'   - Exposes light indicator helpers (EMA, ATR) used by DHUL triggers.
'
' DESIGN
'   - Thread-safe via SyncLock around all reads/writes.
'   - Time stored in UTC; callers should pass UTC (we enforce/specify kind internally).
'   - Timeframe is string-based (e.g., "1 min", "5 mins") to match IB format.
'   - Historical bars are appended via AddBar (completed bars).
'   - Live ticks roll the current bar via UpdateCurrentBarFromTick.
'
' NOTE
'   Keep this module UI-agnostic and IB-agnostic. Consumers pass clean inputs.
' ****************************************************************************************

Module BarManager

    ' --------------------- Internal types ---------------------

    ''' <summary>
    ''' Immutable record for a completed OHLCV bar.
    ''' TimeUtc is the start of the bar's bucket in UTC.
    ''' </summary>
    Private Structure BarRec
        Public ReadOnly TimeUtc As Date
        Public ReadOnly Open As Double
        Public ReadOnly High As Double
        Public ReadOnly Low As Double
        Public ReadOnly Close As Double
        Public ReadOnly Volume As Long
        Public Sub New(t As Date, o As Double, h As Double, l As Double, c As Double, v As Long)
            TimeUtc = Date.SpecifyKind(t, DateTimeKind.Utc)
            Open = o : High = h : Low = l : Close = c : Volume = v
        End Sub
    End Structure

    ''' <summary>
    ''' Mutable holder for the in-progress (current) bar built from live ticks.
    ''' </summary>
    Private Class CurrentBar
        Public TimeUtc As Date ' bucket start (UTC)
        Public Open As Double
        Public High As Double
        Public Low As Double
        Public Close As Double
        Public Volume As Long
        Public Sub Reset(t As Date, px As Double)
            TimeUtc = Date.SpecifyKind(t, DateTimeKind.Utc)
            Open = px : High = px : Low = px : Close = px : Volume = 0
        End Sub
    End Class

    ''' <summary>
    ''' Container per (ConId, Timeframe): completed bars + current rolling bar.
    ''' </summary>
    Private Class Series
        Public ReadOnly Completed As New List(Of BarRec)()
        Public ReadOnly Curr As New CurrentBar()
    End Class

    ' --------------------- Storage ---------------------

    ''' <summary>
    ''' Master map: ConId → (Timeframe → Series).
    ''' Private by design: BarManager is the only owner of time-series state.
    ''' </summary>
    Private ReadOnly _map As New Dictionary(Of Integer, Dictionary(Of String, Series))(EqualityComparer(Of Integer).Default)

    ''' <summary>
    ''' Single synchronization object to protect all state.
    ''' </summary>
    Private ReadOnly _sync As New Object()

    ' --------------------- Helpers ---------------------

    ''' <summary>Convert timeframe string to bucket size in minutes (supports "1 min", "5 mins").</summary>
    Private Function FrameToMinutes(frame As String) As Integer
        If String.IsNullOrWhiteSpace(frame) Then Return 1
        Dim f = frame.Trim().ToLowerInvariant()
        If f.StartsWith("1") Then Return 1
        If f.StartsWith("5") Then Return 5
        Return 1
    End Function

    ''' <summary>Return the UTC start of the bucket for a given tick time and timeframe.</summary>
    Private Function BucketStart(timeUtc As Date, timeframe As String) As Date
        Dim mins = FrameToMinutes(timeframe)
        Dim t = timeUtc.ToUniversalTime()
        Dim m As Integer = CInt(Math.Floor(t.Minute / mins)) * mins
        Return New Date(t.Year, t.Month, t.Day, t.Hour, m, 0, DateTimeKind.Utc)
    End Function

    ''' <summary>Get or create the Series for (ConId, Timeframe).</summary>
    Private Function GetSeries(conId As Integer, timeframe As String) As Series
        Dim tf As String = If(String.IsNullOrWhiteSpace(timeframe), "1 min", timeframe)
        If Not _map.ContainsKey(conId) Then
            _map(conId) = New Dictionary(Of String, Series)(StringComparer.OrdinalIgnoreCase)
        End If
        If Not _map(conId).ContainsKey(tf) Then
            _map(conId)(tf) = New Series()
        End If
        Return _map(conId)(tf)
    End Function

    ' --------------------- Public API ---------------------

    ''' <summary>
    ''' Append a completed historical bar. Bars must be chronological; duplicates are ignored.
    ''' timeUtc is the bar's bucket start in UTC.
    ''' </summary>
    Public Sub AddBar(conId As Integer, timeframe As String, timeUtc As Date,
                      o As Double, h As Double, l As Double, c As Double, v As Long)
        SyncLock _sync
            Dim s = GetSeries(conId, timeframe)
            Dim t = Date.SpecifyKind(timeUtc, DateTimeKind.Utc)
            ' De-dupe: skip if not strictly newer than last stored time
            If s.Completed.Count > 0 AndAlso s.Completed(s.Completed.Count - 1).TimeUtc >= t Then Exit Sub
            s.Completed.Add(New BarRec(t, o, h, l, c, v))
        End SyncLock
    End Sub

    ''' <summary>
    ''' Return the start time (UTC) of the latest completed bar for (ConId, Timeframe).
    ''' Returns Nothing if there are no completed bars.
    ''' </summary>
    Public Function GetLastBarTimeUtc(conId As Integer, timeframe As String) As Date?
        SyncLock _sync
            Dim s = GetSeries(conId, timeframe)
            If s.Completed.Count = 0 Then Return Nothing
            Return s.Completed(s.Completed.Count - 1).TimeUtc
        End SyncLock
    End Function

    ''' <summary>
    ''' Update the in-progress bar from a live underlying tick.
    ''' Rolls to a new bucket when the tick crosses into the next interval and finalizes the previous bar.
    ''' </summary>
    Public Sub UpdateCurrentBarFromTick(conId As Integer, timeframe As String,
                                        tickTimeUtc As Date, lastPrice As Double,
                                        Optional tickVolume As Long = 0)
        SyncLock _sync
            Dim s = GetSeries(conId, timeframe)
            Dim bucket = BucketStart(tickTimeUtc, timeframe)

            ' New bucket? finalize previous current bar into Completed if not already captured
            If s.Curr.TimeUtc <> Date.MinValue AndAlso s.Curr.TimeUtc <> bucket Then
                If s.Completed.Count = 0 OrElse s.Completed(s.Completed.Count - 1).TimeUtc < s.Curr.TimeUtc Then
                    s.Completed.Add(New BarRec(s.Curr.TimeUtc, s.Curr.Open, s.Curr.High, s.Curr.Low, s.Curr.Close, s.Curr.Volume))
                End If
                s.Curr.Reset(bucket, lastPrice)
                s.Curr.Volume = tickVolume
                Exit Sub
            End If

            ' Initialize first current bar
            If s.Curr.TimeUtc = Date.MinValue Then
                s.Curr.Reset(bucket, lastPrice)
                s.Curr.Volume = tickVolume
                Exit Sub
            End If

            ' Same bucket: update O/H/L/C; accumulate volume
            If lastPrice > s.Curr.High Then s.Curr.High = lastPrice
            If lastPrice < s.Curr.Low Then s.Curr.Low = lastPrice
            s.Curr.Close = lastPrice
            If tickVolume > 0 Then s.Curr.Volume += tickVolume
        End SyncLock
    End Sub

    ''' <summary>
    ''' Return last N completed closes (optionally include current bar close).
    ''' Used by EMA/ATR calculators.
    ''' </summary>
    Public Function GetRecentCloses(conId As Integer, timeframe As String,
                                    count As Integer, Optional includeCurrent As Boolean = True) As List(Of Double)
        Dim result As New List(Of Double)()
        SyncLock _sync
            Dim s = GetSeries(conId, timeframe)
            Dim startIdx As Integer = Math.Max(0, s.Completed.Count - count)
            For i As Integer = startIdx To s.Completed.Count - 1
                result.Add(s.Completed(i).Close)
            Next
            If includeCurrent AndAlso s.Curr.TimeUtc <> Date.MinValue Then
                result.Add(s.Curr.Close)
            End If
        End SyncLock
        Return result
    End Function

    ''' <summary>
    ''' Compute EMA(period) of closes, seeded by SMA(period) then α = 2/(n+1).
    ''' Returns Nothing if insufficient data.
    ''' </summary>
    Public Function GetEMA(conId As Integer, timeframe As String,
                           period As Integer, Optional includeCurrent As Boolean = True) As Double?
        If period <= 0 Then Return Nothing
        Dim closes = GetRecentCloses(conId, timeframe, period * 3, includeCurrent) ' warm-up
        If closes.Count < period Then Return Nothing

        Dim alpha As Double = 2.0 / (period + 1.0)
        ' Seed with SMA
        Dim ema As Double = 0
        For i As Integer = 0 To period - 1 : ema += closes(i) : Next
        ema /= period
        ' Continue EMA
        For i As Integer = period To closes.Count - 1
            ema = alpha * closes(i) + (1 - alpha) * ema
        Next
        Return ema
    End Function

    ''' <summary>
    ''' Compute ATR(period) with Wilder smoothing of True Range.
    ''' Returns Nothing if insufficient bars (need period+1).
    ''' </summary>
    Public Function GetATR(conId As Integer, timeframe As String,
                           period As Integer, Optional includeCurrent As Boolean = True) As Double?
        If period <= 0 Then Return Nothing

        Dim bars As New List(Of BarRec)()
        SyncLock _sync
            Dim s = GetSeries(conId, timeframe)
            For Each b In s.Completed : bars.Add(b) : Next
            If includeCurrent AndAlso s.Curr.TimeUtc <> Date.MinValue Then
                bars.Add(New BarRec(s.Curr.TimeUtc, s.Curr.Open, s.Curr.High, s.Curr.Low, s.Curr.Close, s.Curr.Volume))
            End If
        End SyncLock
        If bars.Count < period + 1 Then Return Nothing

        Dim trs As New List(Of Double)(bars.Count - 1)
        For i As Integer = 1 To bars.Count - 1
            Dim prev = bars(i - 1)
            Dim curr = bars(i)
            Dim tr1 = curr.High - curr.Low
            Dim tr2 = Math.Abs(curr.High - prev.Close)
            Dim tr3 = Math.Abs(curr.Low - prev.Close)
            trs.Add(Math.Max(tr1, Math.Max(tr2, tr3)))
        Next

        Dim atr As Double = 0
        For i As Integer = 0 To period - 1 : atr += trs(i) : Next
        atr /= period
        For i As Integer = period To trs.Count - 1
            atr = ((period - 1) * atr + trs(i)) / period
        Next
        Return atr
    End Function

    ''' <summary>
    ''' Return number of completed bars stored for (ConId, Timeframe) — for debug/telemetry.
    ''' </summary>
    Public Function GetCompletedCount(conId As Integer, timeframe As String) As Integer
        SyncLock _sync
            Return GetSeries(conId, timeframe).Completed.Count
        End SyncLock
    End Function

End Module
