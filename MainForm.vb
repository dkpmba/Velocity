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

End Class
