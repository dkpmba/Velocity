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

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        ConnectionManager.Show()
    End Sub
    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If TwsHost.IsConnected() Then
            AppendAlert("Requesting server time...")
            TwsHost.Tws.ClientSocket.reqCurrentTime()
            If Not _twseventsHooked Then
                AddHandler TWSEvents.ApiError, Sub(code, msg) AppendAlert($"TWS {code}: {msg}", "TWS")
                AddHandler TWSEvents.ConnClosed, Sub() AppendAlert("TWS connection closed.", "WARN")
                AddHandler TWSEvents.NextValidId, Sub(id) AppendAlert("Connected. nextValidId=" & id, "INFO")
                _twseventsHooked = True
            End If
            ' or: TwsHost.Tws.ClientSocket.reqAccountSummary(9001, "All", "NetLiquidation,TotalCashValue")
        End If
    End Sub

End Class
