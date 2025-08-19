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
        InitTimeFrameCombo()
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
                                                              NotifyTwsConnected()
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
#Region "Historical Boot (Manager-Orchestrated)"

    ' -------------------------------------------------------------------------------------------------
    ' Public entry point — call this after:
    '   1) TWS connection is up,
    '   2) dgvSymbols is populated,
    '   3) cbTimeframe has a selection (e.g., "1" or "5").
    ' This wires events (idempotent), sets the Requester delegate if needed, and starts the boot.
    ' -------------------------------------------------------------------------------------------------
    Public Sub HBoot_Start()
        ' 1) Ensure HistoricalBarManager has a requester delegate to call into your TWS wrapper.
        HBoot_SetupRequesterIfNeeded()

        ' 2) Subscribe to progress/completion events (no-op if already wired).
        HBoot_WireEventsOnce()

        ' 3) Build the request from UI and start the manager-driven boot.
        HBoot_StartFromSymbols()
    End Sub

    ' -------------------------------------------------------------------------------------------------
    ' One-time event subscription for progress + completion.
    ' Safe to call multiple times; we remove before add to avoid duplicates.
    ' -------------------------------------------------------------------------------------------------
    Private _hbootEventsWired As Boolean = False
    Private Sub HBoot_WireEventsOnce()
        If _hbootEventsWired Then Return
        RemoveHandler HistoricalBarManager.BootProgress, AddressOf HBoot_OnProgress
        RemoveHandler HistoricalBarManager.BootCompleted, AddressOf HBoot_OnCompleted
        AddHandler HistoricalBarManager.BootProgress, AddressOf HBoot_OnProgress
        AddHandler HistoricalBarManager.BootCompleted, AddressOf HBoot_OnCompleted
        _hbootEventsWired = True
    End Sub
    ''' <summary>
    ''' Return the index of a column by name (case-insensitive). Returns -1 if not found.
    ''' </summary>
    Private Function HBoot_GetColumnIndexByName(grid As DataGridView, columnName As String) As Integer
        If grid Is Nothing OrElse String.IsNullOrWhiteSpace(columnName) Then Return -1
        For Each col As DataGridViewColumn In grid.Columns
            If String.Equals(col.Name, columnName, StringComparison.OrdinalIgnoreCase) Then
                Return col.Index
            End If
        Next
        Return -1
    End Function

    ' -------------------------------------------------------------------------------------------------
    ' Determine timeframe/duration from cbTimeframe and collect all ConIds from dgvSymbols,
    ' then kick off HistoricalBarManager.StartBoot(...)
    ' -------------------------------------------------------------------------------------------------
    Private Sub HBoot_StartFromSymbols()
        ' Resolve timeframe/duration from the combo (enum-backed)
        Dim ibParams = HBoot_GetIbBarParamsFromCombo()
        Dim barSize As String = ibParams.barSize      ' "1 min" or "5 mins"
        Dim duration As String = ibParams.duration    ' "1 D" or "2 D"
        Dim whatToShow As String = "TRADES"
        Dim useRth As Integer = 0
        Dim endUtc As Date = Date.UtcNow

        ' Collect ConIds from dgvSymbols
        ' Replace just the "Collect ConIds from dgvSymbols" block with this:
        ' -----------------------------------------------------------------------------
        ' 2) Build the ConId list from dgvSymbols (prefers sCID, falls back to CID)
        Dim conIds As New List(Of Integer)

        Dim cidColIndex As Integer = HBoot_GetColumnIndexByName(dgvSymbols, "sCID")
        If cidColIndex = -1 Then
            cidColIndex = HBoot_GetColumnIndexByName(dgvSymbols, "CID")
        End If

        If cidColIndex = -1 Then
            ' No suitable column found — bail gracefully
            HBoot_SetStatusSafe("History load aborted: no sCID/CID column.")
            Return
        End If

        For Each row As DataGridViewRow In dgvSymbols.Rows
            If row Is Nothing OrElse row.IsNewRow Then Continue For
            Dim cell As DataGridViewCell = row.Cells(cidColIndex)
            If cell Is Nothing Then Continue For
            Dim raw As Object = cell.Value
            Dim cid As Integer
            If raw IsNot Nothing AndAlso Integer.TryParse(Convert.ToString(raw), cid) AndAlso cid > 0 Then
                conIds.Add(cid)
            End If
        Next
        ' -----------------------------------------------------------------------------


        ' Update UI status (optional)
        HBoot_SetStatusSafe($"Loading history... 0/{conIds.Count}")

        ' Kick off the manager-driven boot (serialized requests + progress events)
        HistoricalBarManager.StartBoot(conIds, barSize, duration, whatToShow, useRth, endUtc)
    End Sub

    ' -------------------------------------------------------------------------------------------------
    ' Progress event handler (done/total/currentConId). Marshals to UI thread when needed.
    ' -------------------------------------------------------------------------------------------------
    Private Sub HBoot_OnProgress(done As Integer, total As Integer, currentConId As Integer)
        If Me.IsHandleCreated AndAlso Me.InvokeRequired Then
            Me.BeginInvoke(Sub() HBoot_OnProgress(done, total, currentConId))
            Return
        End If
        HBoot_SetStatusSafe($"Loading history... {done}/{total}")
    End Sub

    ' -------------------------------------------------------------------------------------------------
    ' Completion event handler — fires once after the last request finishes.
    ' Start any timers, market-data subscriptions, and enable strategies here.
    ' Kept lean and defensive: no hard dependencies on specific methods.
    ' -------------------------------------------------------------------------------------------------
    Private Sub HBoot_OnCompleted()
        If Me.IsHandleCreated AndAlso Me.InvokeRequired Then
            Me.BeginInvoke(Sub() HBoot_OnCompleted())
            Return
        End If

        HBoot_SetStatusSafe("History ready.")

        ' === Gate everything that depends on backfill being complete ===
        ' If you already have methods below, uncomment the calls; if not, place your starts here.

        ' 1) Start market data subscriptions (underlyings + open option legs)
        'Try
        '    EnsureSymbolSubscriptions()      ' subscribes all underlyings in dgvSymbols (deduped)
        '    EnsureOpenLegSubscriptions()     ' subscribes all open option CIDs in dgvMonitor
        'Catch ex As Exception
        '    Debug.WriteLine($"Market subs error: {ex.Message}")
        'End Try

        ' 2) Start any per-second/per-bar timers (theta decay, strategy loop, etc.)
        'Try
        '    StartStrategyTimers()            ' your existing timer bootstrap, if any
        'Catch ex As Exception
        '    Debug.WriteLine($"Timer start error: {ex.Message}")
        'End Try

        ' 3) Enable DHUL automation hooks if you gate them post-history
        'Try
        '    EnableDHULIfReady()
        'Catch ex As Exception
        '    Debug.WriteLine($"DHUL enable error: {ex.Message}")
        'End Try
    End Sub

    ' -------------------------------------------------------------------------------------------------
    ' Assign the Requester delegate if it isn't set.
    ' This is a thin adapter to your TWS historical request wrapper.
    ' IMPORTANT: Replace the TODO call with your exact wrapper method.
    ' Expected signature for the delegate:
    '   Function(conId As Integer, endUtc As Date, duration As String, barSize As String, whatToShow As String, useRth As Integer) As Integer
    ' -------------------------------------------------------------------------------------------------
    Private Sub HBoot_SetupRequesterIfNeeded()
        If HistoricalBarManager.Requester IsNot Nothing Then Exit Sub
        'HistoricalBarManager.Requester = AddressOf HBoot_TwsRequester
        HistoricalBarManager.Requester = AddressOf TwsHost.RequestHistoricalData
    End Sub

    ''' <summary>
    ''' Thin adapter that matches HistoricalBarManager.HistoricalRequester delegate.
    ''' Replace the TODO line with your real TWS wrapper call and return its reqId.
    ''' </summary>
    Private Function HBoot_TwsRequester(conId As Integer,
                                    endUtc As Date,
                                    durationStr As String,
                                    barSizeStr As String,
                                    whatToShow As String,
                                    useRth As Integer) As Integer
        ' TODO: Replace the following NotImplementedException with your actual call into the TWS wrapper.
        ' Examples (commented) — pick the one that matches your project:
        '
        ' Return Tws.RequestHistoricalData(conId, endUtc, durationStr, barSizeStr, whatToShow, useRth)
        ' Return TWSConn.RequestHistoricalData(conId, endUtc, durationStr, barSizeStr, whatToShow, useRth)
        ' Return TwsHost.Client.reqHistoricalData(conId, endUtc, durationStr, barSizeStr, whatToShow, useRth)
        '
        Throw New NotImplementedException("Wire HBoot_TwsRequester to your TWS historical request wrapper and return reqId.")
    End Function

    ' -------------------------------------------------------------------------------------------------
    ' Small helper to set a status label safely (optional — ignores if lblStatus missing).
    ' -------------------------------------------------------------------------------------------------
    Private Sub HBoot_SetStatusSafe(text As String)
        Try
            If lblStatus Is Nothing Then Return
            If Me.IsHandleCreated AndAlso Me.InvokeRequired Then
                Me.BeginInvoke(Sub() lblStatus.Text = text)
            Else
                lblStatus.Text = text
            End If
        Catch
            ' ignore — label may not exist in some forms/layouts
        End Try
    End Sub

#End Region


End Class
