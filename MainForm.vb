Public Partial Class MainForm
    Inherits Form

    Private _twseventsHooked As Boolean = False
    Public Sub New()
        InitializeComponent()
        ' UI-only scaffold. No logic yet.
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Placeholder: set initial labels/visuals
        lblStatus.Text = "Disconnected"
        lblConn.Text = "TWS: Disconnected"
        lblLatency.Text = "Latency: -"
        lblDb.Text = "DB: Unknown"

        ' Optional: set default tab
        tabMain.SelectedTab = tabMonitor

        ' Columns are intentionally not hard-wired here; you will bind later.
        ' You can still predefine column sets at runtime once your schemas are finalized.
    End Sub

    Private Sub HookTwseventsOnce()
        If _twseventsHooked Then Return
        _twseventsHooked = True

        ' ensure the status UI exists
        EnsureTwsStatusUi()

        ' existing alert hooks (keep yours)
        AddHandler TWSEvents.ApiError, Sub(code, msg) AppendAlert($"TWS {code}: {msg}", "TWS")

        AddHandler TWSEvents.ConnClosed, Sub()
                                             BeginInvoke(Sub()
                                                             UpdateTwsStatus(False, "TWS: Disconnected")
                                                             AppendAlert("TWS connection closed.", "WARN")
                                                         End Sub)
                                         End Sub

        AddHandler TWSEvents.NextValidId, Sub(id)
                                              BeginInvoke(Sub()
                                                              UpdateTwsStatus(True, $"TWS: Connected (NextValidId={id})")
                                                              AppendAlert("Connected. nextValidId=" & id, "INFO")
                                                              EnsureLatencyTimer()   ' if you use one
                                                              PingServerTime()       ' kick off first RTT
                                                          End Sub)
                                          End Sub

        ' NEW: server time -> status strip
        AddHandler TWSEvents.ServerTime, Sub(epoch)
                                             Dim ms As Integer = CInt(_latencySw.ElapsedMilliseconds)
                                             Dim dt = DateTimeOffset.FromUnixTimeSeconds(epoch).ToLocalTime().DateTime
                                             BeginInvoke(Sub()
                                                             UpdateLatency(ms)
                                                             'EnsureStatusStripRefs()
                                                             If lblConn IsNot Nothing Then
                                                                 lblConn.ToolTipText = "TWS Server Time: " & dt.ToString("yyyy-MM-dd HH:mm:ss")
                                                             End If
                                                         End Sub)
                                         End Sub


    End Sub


    ' Hook this to your “Data Connection…” menu item or button
    Private Sub mnuDataConnection_Click(sender As Object, e As EventArgs) Handles mnuDataConnection.Click
        Using dlg As New ConnectionManager()
            Dim result = dlg.ShowDialog(Me)
            ' We don’t force DialogResult=OK in your current flow; so just refresh after the dialog if a CS is present
            Dim cs = AppServices.ConnectionString  ' or your GetConnString()
            If Not String.IsNullOrWhiteSpace(cs) Then
                AppServices.Initialize(cs)
                RefreshAllData()
                UpdateDbStatus(GetDbStatusText(cs))
                AppendAlert("DB connected and UI refreshed.", "INFO")
            End If

            HookTwseventsOnce()
            If TwsHost.IsConnected() Then
                AppendAlert("TWS session active.", "INFO")
            Else
                AppendAlert("TWS not connected (use Connect in Connection Manager).", "WARN")
            End If
        End Using
    End Sub

    Private Function GetDbStatusText(cs As String) As String
        Try
            Dim b = New Microsoft.Data.SqlClient.SqlConnectionStringBuilder(cs)
            Return $"DB: {If(b.InitialCatalog, "(none)")}@{b.DataSource}"
        Catch
            Return "DB: Connected"
        End Try
    End Function

    '========================
    '== HISTORICAL BOOT =====
    '========================
#Region "Historical Boot (Symbols → BarManager)"

    ' Queue item for historical requests
    Private Class HBootReq
        Public Property ConId As Integer
        Public Property BarSize As String     ' e.g., "1 min" or "5 mins"
        Public Property Duration As String    ' e.g., "3 D", "10 D"
        Public Property WhatToShow As String  ' e.g., "TRADES"
        Public Property UseRth As Integer     ' 1 = RTH, 0 = all
        Public Property EndTimeUtc As Date    ' optional (UTC)
    End Class

    ' State
    Private ReadOnly _hbootQueue As New Queue(Of HBootReq)()
    Private _hbootInProgress As Boolean = False
    Private _hbootTotal As Integer = 0
    Private _hbootDone As Integer = 0

    ''' <summary>
    ''' Entry point: build request queue for ALL rows in dgvSymbols, then start dequeuing.
    ''' Call this once after DB/TWS connect and after dgvSymbols is populated.
    ''' </summary>
    Public Sub HBoot_Start()
        _hbootQueue.Clear()
        _hbootInProgress = False
        _hbootTotal = 0
        _hbootDone = 0

        ' Determine timeframe from UI (assumes "1" or "5" minutes selection; adjust if your values differ)
        Dim barSize As String
        Dim duration As String
        Dim tfText As String = TryCast(cbTimeframe?.SelectedItem, String)
        If String.IsNullOrWhiteSpace(tfText) Then tfText = "1" ' default to 1 minute

        If tfText.Trim() = "1" OrElse tfText.Contains("1") Then
            barSize = "1 min"
            duration = "3 D"        ' enough history to stabilize ATR/EMA for 1m
        Else
            barSize = "5 mins"
            duration = "10 D"       ' deeper history for 5m
        End If

        ' Build queue from dgvSymbols
        For Each row As DataGridViewRow In dgvSymbols.Rows
            If row.IsNewRow Then Continue For

            Dim cidObj As Object = Nothing
            ' Prefer sCID (per your schema); fall back to "CID" if needed
            If row.Cells.Contains("sCID") Then
                cidObj = row.Cells("sCID").Value
            ElseIf row.Cells.Contains("CID") Then
                cidObj = row.Cells("CID").Value
            End If
            If cidObj Is Nothing Then Continue For

            Dim conId As Integer
            If Integer.TryParse(Convert.ToString(cidObj), conId) AndAlso conId > 0 Then
                _hbootQueue.Enqueue(New HBootReq With {
                .ConId = conId,
                .BarSize = barSize,
                .Duration = duration,
                .WhatToShow = "TRADES",
                .UseRth = 0,
                .EndTimeUtc = Date.UtcNow
            })
            End If
        Next

        _hbootTotal = _hbootQueue.Count
        If _hbootTotal = 0 Then
            ' Nothing to do; mark ready immediately
            HBoot_OnAllHistoryReady()
            Exit Sub
        End If

        _hbootInProgress = True
        HBoot_RequestNext()
    End Sub

    ''' <summary>
    ''' Dequeue one request and send to TWS. We run strictly serial to avoid pacing violations.
    ''' </summary>
    Private Sub HBoot_RequestNext()
        If Not _hbootInProgress Then Exit Sub
        If _hbootQueue.Count = 0 Then
            ' Done
            HBoot_OnAllHistoryReady()
            Exit Sub
        End If

        Dim req As HBootReq = _hbootQueue.Peek()

        ' === TODO: Replace this call with YOUR wrapper to request historical data ===
        ' Expected semantics (adjust to your Tws.vb): 
        ' RequestHistoricalData(conId, endTimeUtc, durationStr, barSizeStr, whatToShow, useRth)
        '
        ' Example signatures you may have:
        ' Tws.RequestHistoricalData(req.ConId, req.EndTimeUtc, req.Duration, req.BarSize, req.WhatToShow, req.UseRth)
        ' TwsHost.Client.reqHistoricalData( ... )
        '
        CALL_TWS_HISTORICAL_DATA(req.ConId, req.EndTimeUtc, req.Duration, req.BarSize, req.WhatToShow, req.UseRth)
    End Sub

    ''' <summary>
    ''' TWSEvents should call this for each historical bar received.
    ''' </summary>
    Public Sub HBoot_OnHistoricalBar(conId As Integer, barTimeUtc As Date, o As Double, h As Double, l As Double, c As Double, v As Long)
        Try
            ' === TODO: Map to your BarManager API ===
            ' Typical idea: BarManager.AppendHistoricalBar(conId, timeframe, barTimeUtc, o,h,l,c,v)
            BarManager.AppendHistoricalBar(conId, barTimeUtc, o, h, l, c, v)
        Catch ex As Exception
            ' Swallow per-bar exceptions to avoid breaking the boot
            Debug.WriteLine($"HBoot_OnHistoricalBar error: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' TWSEvents should call this once for the matching historicalDataEnd.
    ''' </summary>
    Public Sub HBoot_OnHistoricalEnd(conId As Integer)
        ' Confirm the head of queue is this conId; then pop and proceed
        If _hbootQueue.Count > 0 AndAlso _hbootQueue.Peek().ConId = conId Then
            _hbootQueue.Dequeue()
            _hbootDone += 1
        Else
            ' If out-of-order or duplicate end events, we still progress defensively
            _hbootDone = Math.Min(_hbootDone + 1, _hbootTotal)
            If _hbootQueue.Count > 0 Then _hbootQueue.Dequeue()
        End If

        ' Small visual cue (optional if you have a status label)
        Try
            If lblStatus IsNot Nothing Then
                lblStatus.Text = $"Loading history... {_hbootDone}/{_hbootTotal}"
            End If
        Catch
            ' ignore
        End Try

        ' Request next or finish
        If _hbootQueue.Count = 0 Then
            HBoot_OnAllHistoryReady()
        Else
            ' Add a tiny delay to be gentle with pacing
            Dim tmr As New Timer With {.Interval = 200}
            AddHandler tmr.Tick, Sub(sender As Object, e As EventArgs)
                                     Dim tm = DirectCast(sender, Timer)
                                     tm.Stop() : tm.Dispose()
                                     HBoot_RequestNext()
                                 End Sub
            tmr.Start()
        End If
    End Sub

    ''' <summary>
    ''' Fired when all symbols’ histories are loaded. Starts strategy timers safely.
    ''' </summary>
    Private Sub HBoot_OnAllHistoryReady()
        _hbootInProgress = False
        Try
            ' If you defer starting timers until history is ready, do it here:
            ' Example: timerSpread.Interval = If(cbTimeframe.SelectedItem?.ToString() = "1", 1000 * 60, 1000 * 60 * 5)
            ' timerSpread.Start()
            OnAllHistoryReady() ' If you already have a hook method, this will call it.
        Catch ex As Exception
            Debug.WriteLine($"OnAllHistoryReady hook error: {ex.Message}")
        End Try

        Try
            If lblStatus IsNot Nothing Then
                lblStatus.Text = "History ready."
            End If
        Catch
            ' ignore
        End Try
    End Sub

#End Region

End Class
