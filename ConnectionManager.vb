Public Partial Class ConnectionManager
    Inherits Form

    Private Enum ConnState
        Disconnected
        Connecting
        Connected
    End Enum

    Private _state As ConnState = ConnState.Disconnected
    Private _profiles As New List(Of Profile)()
    Private _defaultProfileName As String = ""

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ConnectionManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Defaults
        cboConnType.SelectedIndex = 0
        txtHost.Text = "127.0.0.1"
        nudPort.Value = 9001
        nudClientId.Value = 1
        chkAutoReconnect.Checked = True
        chkResubMarketData.Checked = True
        chkResubPositions.Checked = True
        nudHeartbeat.Value = 150
        nudMaxReqPerSec.Value = 50
        nudConnTimeout.Value = 10
        nudReqTimeout.Value = 30
        UpdateConnUi(ConnState.Disconnected)
        AppendLog("INFO", "Ready.")

        AddHandler TWSEvents.NextValidId, AddressOf OnNextValidId
        AddHandler TWSEvents.ConnClosed, AddressOf OnTwsClosed
        AddHandler TWSEvents.ApiError, AddressOf OnTwsError

        ' Make the app auto-load the saved connection and launch MainForm if possible
        AutoStartIfPossible()

    End Sub
    Private Sub AutoStartIfPossible()
        ' 1) Get a bootstrap connection string
        Dim bootstrap As String = GetConnString()
        If String.IsNullOrWhiteSpace(bootstrap) Then
            bootstrap = GetBootstrapCandidate()
            If Not String.IsNullOrWhiteSpace(bootstrap) Then
                SetConnString(bootstrap)
            End If
        End If

        ' 2) Try to fetch the saved app connection from dbo.settings
        Dim saved As String = TryGetDbSavedConnection(bootstrap)

        ' 3) If found, use it and launch Main
        If Not String.IsNullOrWhiteSpace(saved) Then
            SetConnString(saved)
            SetStatus("Loaded saved connection from dbo.settings. Launching workspace…", True)
            'LaunchMain(saved)
        Else
            ' leave the screen up; user can Test/Save manually
            If Not String.IsNullOrWhiteSpace(bootstrap) Then
                SetStatus("Enter/Test connection or click Load. (No saved 'app.db.connection' found.)", False)
            End If
        End If
    End Sub

    Private Function GetBootstrapCandidate() As String
        ' Optional: allow an environment override
        Dim env As String = Environment.GetEnvironmentVariable("VELOCITY_DB_CS")
        If Not String.IsNullOrWhiteSpace(env) Then Return env

        ' Default: local trusted connection to VelocityDB on your trading box
        Return Velocity.Core.SqlConnectionFactory.BuildLocalTrusted("DESKTOP-TRADING")
    End Function

    Private Function TryGetDbSavedConnection(bootstrapCs As String) As String
        If String.IsNullOrWhiteSpace(bootstrapCs) Then Return Nothing
        Try
            Using c As New Microsoft.Data.SqlClient.SqlConnection(bootstrapCs)
                c.Open()

                ' Ensure dbo.settings exists
                Dim hasSettings As Boolean
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(
                "SELECT COUNT(*) FROM sys.tables WHERE name='settings' AND schema_id=SCHEMA_ID('dbo');", c)
                    hasSettings = Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
                If Not hasSettings Then Return Nothing

                ' Read the saved app connection
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(
                "SELECT json FROM dbo.settings WHERE [key]='app.db.connection';", c)
                    Dim o = cmd.ExecuteScalar()
                    If o Is Nothing OrElse o Is DBNull.Value Then Return Nothing
                    Dim cs As String = Convert.ToString(o)
                    If String.IsNullOrWhiteSpace(cs) Then Return Nothing
                    Return cs
                End Using
            End Using
        Catch ex As Exception
            ' Don’t block startup; just show a soft status and let user interact
            SetStatus("Auto-load skipped: " & ex.Message, False)
            Return Nothing
        End Try
    End Function

    Private Sub LaunchMain(cs As String)
        If String.IsNullOrWhiteSpace(cs) Then
            MessageBox.Show("Connection string is empty.")
            Return
        End If
        If Me.InvokeRequired Then
            Me.BeginInvoke(Sub() LaunchMain(cs)) : Return
        End If
        Try
            Dim mf As New MainForm()
            mf.Init(cs)          ' uses AppServices + binds grids
            mf.Show()
            Me.Close()            ' or Me.Close() if Shutdown mode = "When last form closes"
        Catch ex As Exception
            MessageBox.Show("Failed to open MainForm: " & ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '── ToolStrip actions ───────────────────────────────────────────────────────

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Try
            Dim host = txtHost.Text.Trim()
            Dim port = CInt(nudPort.Value)
            Dim clientId = CInt(nudClientId.Value)

            UpdateConnUi(ConnState.Connecting)
            TwsHost.Connect(host, port, clientId, Sub(m) AppendLog("INFO", m))
            AppendLog("INFO", "Connect requested.")

        Catch ex As Exception
            UpdateConnUi(ConnState.Disconnected)
            AppendLog("ERROR", "TWS connect failed: " & ex.Message)
            MessageBox.Show("TWS connect failed: " & ex.Message, "TWS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    ' ConnectionManager.vb
    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
        Try
            UpdateConnUi(ConnState.Disconnected)
            AppendLog("INFO", "Disconnect requested...")

            ' If you subscribed earlier, it's safe to unhook now (optional)
            RemoveHandler TWSEvents.NextValidId, AddressOf OnNextValidId
            RemoveHandler TWSEvents.ConnClosed, AddressOf OnTwsClosed
            RemoveHandler TWSEvents.ApiError, AddressOf OnTwsError

            ' Tell the host to tear down the session (closes reader + socket)
            TwsHost.Disconnect()

            ' Small wait so UI reflects the state reliably
            Dim t0 = Environment.TickCount
            Do While TwsHost.IsConnected() AndAlso Environment.TickCount - t0 < 2000
                Application.DoEvents()
                Threading.Thread.Sleep(25)
            Loop

            UpdateConnUi(ConnState.Disconnected)
            AppendLog("INFO", "Disconnected from TWS.")
        Catch ex As Exception
            UpdateConnUi(ConnState.Disconnected)
            AppendLog("ERROR", "Disconnect error: " & ex.Message)
        End Try
    End Sub

    ' Fired once connection is fully ready
    Private Sub OnNextValidId(nextId As Integer)
        If Me.IsHandleCreated Then
            BeginInvoke(Sub()
                            UpdateConnUi(ConnState.Connected)
                            AppendLog("INFO", "Connected. nextValidId=" & nextId)
                        End Sub)
        End If
    End Sub

    Private Sub OnTwsClosed()
        If Me.IsHandleCreated Then
            BeginInvoke(Sub()
                            UpdateConnUi(ConnState.Disconnected)
                            AppendLog("WARN", "TWS connection closed.")
                        End Sub)
        End If
    End Sub

    Private Sub OnTwsError(code As Integer, msg As String)
        If Me.IsHandleCreated Then
            BeginInvoke(Sub() AppendLog("TWS", $"Error {code}: {msg}"))
        End If
    End Sub



    Private Sub btnTestPing_Click(sender As Object, e As EventArgs) Handles btnTestPing.Click
        Dim rnd As New Random()
        Dim ms = rnd.Next(3, 30)
        lblLatency.Text = $"Latency: {ms} ms"
        AppendLog("INFO", $"Ping measured {ms} ms (simulated)")
    End Sub

    Private Sub btnSaveProfile_Click(sender As Object, e As EventArgs) Handles btnSaveProfile.Click, btnAdd.Click
        Dim name = txtProfileName.Text.Trim()
        If name.Length = 0 Then
            MessageBox.Show("Enter a profile name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim p = BuildProfileFromUi()
        p.Name = name

        Dim idx = _profiles.FindIndex(Function(x) x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        If idx >= 0 Then
            _profiles(idx) = p
            AppendLog("INFO", $"Profile '{name}' updated.")
        Else
            _profiles.Add(p)
            AppendLog("INFO", $"Profile '{name}' added.")
        End If
        RefreshProfilesList(name)
        lblActiveProfile.Text = $"Profile: {name}"
    End Sub

    Private Sub btnDeleteProfile_Click(sender As Object, e As EventArgs) Handles btnDeleteProfile.Click, btnRemove.Click
        Dim name = TryCast(lbProfiles.SelectedItem, String)
        If String.IsNullOrEmpty(name) Then
            MessageBox.Show("Select a profile to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If MessageBox.Show($"Delete profile '{name}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            _profiles.RemoveAll(Function(x) x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            RefreshProfilesList("")
            AppendLog("INFO", $"Profile '{name}' deleted.")
            If _defaultProfileName.Equals(name, StringComparison.OrdinalIgnoreCase) Then
                _defaultProfileName = ""
            End If
        End If
    End Sub

    Private Sub btnSetDefault_Click(sender As Object, e As EventArgs) Handles btnSetDefault.Click
        Dim name = TryCast(lbProfiles.SelectedItem, String)
        If String.IsNullOrEmpty(name) Then
            MessageBox.Show("Select a profile to set default.", "Default profile", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        _defaultProfileName = name
        AppendLog("INFO", $"Default profile set to '{name}'.")
    End Sub

    '── Profiles list handling ─────────────────────────────────────────────────

    Private Sub lbProfiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbProfiles.SelectedIndexChanged
        Dim name = TryCast(lbProfiles.SelectedItem, String)
        If String.IsNullOrEmpty(name) Then Return
        Dim p = _profiles.FirstOrDefault(Function(x) x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        If p Is Nothing Then Return
        LoadProfileToUi(p)
        lblActiveProfile.Text = $"Profile: {name}"
        AppendLog("INFO", $"Loaded profile '{name}'.")
    End Sub

    Private Sub RefreshProfilesList(Optional selectName As String = "")
        lbProfiles.Items.Clear()
        lbProfiles.Items.AddRange(_profiles.Select(Function(x) x.Name).OrderBy(Function(s) s).ToArray())
        If selectName.Length > 0 Then
            Dim idx = lbProfiles.Items.IndexOf(selectName)
            If idx >= 0 Then lbProfiles.SelectedIndex = idx
        End If
    End Sub

    Private Function BuildProfileFromUi() As Profile
        Dim p As New Profile()
        p.Name = txtProfileName.Text.Trim()
        p.ConnType = TryCast(cboConnType.SelectedItem, String)
        p.Host = txtHost.Text.Trim()
        p.Port = CInt(nudPort.Value)
        p.ClientId = CInt(nudClientId.Value)
        p.SSL = chkSSL.Checked
        p.ConnectAtStartup = chkConnectAtStartup.Checked
        p.AutoReconnect = chkAutoReconnect.Checked
        p.ResubMD = chkResubMarketData.Checked
        p.ResubPos = chkResubPositions.Checked
        p.HeartbeatMs = CInt(nudHeartbeat.Value)
        p.MaxReqPerSec = CInt(nudMaxReqPerSec.Value)
        p.ProxyEnabled = chkUseProxy.Checked
        p.ProxyHost = txtProxyHost.Text.Trim()
        p.ProxyPort = CInt(nudProxyPort.Value)
        p.ConnTimeout = CInt(nudConnTimeout.Value)
        p.ReqTimeout = CInt(nudReqTimeout.Value)
        p.LogIbMessages = chkLogIbMessages.Checked
        Return p
    End Function

    Private Sub LoadProfileToUi(p As Profile)
        txtProfileName.Text = p.Name
        cboConnType.SelectedItem = p.ConnType
        txtHost.Text = p.Host
        nudPort.Value = Math.Max(1, Math.Min(65535, p.Port))
        nudClientId.Value = Math.Max(0, Math.Min(Integer.MaxValue, p.ClientId))
        chkSSL.Checked = p.SSL
        chkConnectAtStartup.Checked = p.ConnectAtStartup
        chkAutoReconnect.Checked = p.AutoReconnect
        chkResubMarketData.Checked = p.ResubMD
        chkResubPositions.Checked = p.ResubPos
        nudHeartbeat.Value = Math.Max(50, Math.Min(5000, p.HeartbeatMs))
        nudMaxReqPerSec.Value = Math.Max(1, Math.Min(1000, p.MaxReqPerSec))
        chkUseProxy.Checked = p.ProxyEnabled
        txtProxyHost.Text = p.ProxyHost
        nudProxyPort.Value = Math.Max(1, Math.Min(65535, p.ProxyPort))
        nudConnTimeout.Value = Math.Max(1, Math.Min(120, p.ConnTimeout))
        nudReqTimeout.Value = Math.Max(1, Math.Min(600, p.ReqTimeout))
        chkLogIbMessages.Checked = p.LogIbMessages
    End Sub

    '── Helpers ────────────────────────────────────────────────────────────────

    Private Function ValidateEndpoint() As Boolean
        If String.IsNullOrWhiteSpace(txtHost.Text) Then
            MessageBox.Show("Host is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If nudPort.Value < 1 OrElse nudPort.Value > 65535 Then
            MessageBox.Show("Port must be between 1 and 65535.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If nudClientId.Value < 0 Then
            MessageBox.Show("Client ID cannot be negative.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Sub UpdateConnUi(state As ConnState)
        _state = state
        Select Case state
            Case ConnState.Disconnected
                lblConnIndicator.ForeColor = Color.Firebrick
                lblConnStatus.Text = "Disconnected"
            Case ConnState.Connecting
                lblConnIndicator.ForeColor = Color.Orange
                lblConnStatus.Text = "Connecting..."
            Case ConnState.Connected
                lblConnIndicator.ForeColor = Color.SeaGreen
                lblConnStatus.Text = "Connected"
        End Select
        btnConnect.Enabled = (state <> ConnState.Connected)
        btnDisconnect.Enabled = (state = ConnState.Connected)
    End Sub

    Private Sub AppendLog(level As String, message As String)
        Dim item = New ListViewItem(New String() {
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            level,
            message
        })
        lvLog.Items.Add(item)
        item.EnsureVisible()
    End Sub

    '── Profile model ─────────────────────────────────────────────────────────

    Private Class Profile
        Public Property Name As String
        Public Property ConnType As String
        Public Property Host As String
        Public Property Port As Integer
        Public Property ClientId As Integer
        Public Property SSL As Boolean
        Public Property ConnectAtStartup As Boolean
        Public Property AutoReconnect As Boolean
        Public Property ResubMD As Boolean
        Public Property ResubPos As Boolean
        Public Property HeartbeatMs As Integer
        Public Property MaxReqPerSec As Integer
        Public Property ProxyEnabled As Boolean
        Public Property ProxyHost As String
        Public Property ProxyPort As Integer
        Public Property ConnTimeout As Integer
        Public Property ReqTimeout As Integer
        Public Property LogIbMessages As Boolean
    End Class

End Class
