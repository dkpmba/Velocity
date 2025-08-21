<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConnectionManager
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        tsTop = New ToolStrip()
        btnConnect = New ToolStripButton()
        btnDisconnect = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSaveProfile = New ToolStripButton()
        btnDeleteProfile = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnTestPing = New ToolStripButton()
        splitMain = New SplitContainer()
        grpProfiles = New GroupBox()
        btnSetDefault = New Button()
        btnRemove = New Button()
        btnAdd = New Button()
        txtProfileName = New TextBox()
        lblProfileName = New Label()
        lbProfiles = New ListBox()
        tabRight = New TabControl()
        tabEndpoint = New TabPage()
        grpLiveInfo = New GroupBox()
        lblServerVersionValue = New Label()
        lblServerVersion = New Label()
        lblServerTimeValue = New Label()
        lblServerTime = New Label()
        lblAccountValue = New Label()
        lblAccount = New Label()
        grpEndpoint = New GroupBox()
        chkConnectAtStartup = New CheckBox()
        chkSSL = New CheckBox()
        nudClientId = New NumericUpDown()
        lblClientId = New Label()
        nudPort = New NumericUpDown()
        lblPort = New Label()
        txtHost = New TextBox()
        lblHost = New Label()
        cboConnType = New ComboBox()
        lblConnType = New Label()
        tabAdvanced = New TabPage()
        grpBehavior = New GroupBox()
        chkLogIbMessages = New CheckBox()
        chkResubPositions = New CheckBox()
        chkResubMarketData = New CheckBox()
        chkAutoReconnect = New CheckBox()
        grpThrottles = New GroupBox()
        nudMaxReqPerSec = New NumericUpDown()
        lblMaxReqPerSec = New Label()
        nudHeartbeat = New NumericUpDown()
        lblHeartbeat = New Label()
        tabNetwork = New TabPage()
        grpProxy = New GroupBox()
        chkUseProxy = New CheckBox()
        nudProxyPort = New NumericUpDown()
        lblProxyPort = New Label()
        txtProxyHost = New TextBox()
        lblProxyHost = New Label()
        grpTimeouts = New GroupBox()
        nudReqTimeout = New NumericUpDown()
        lblReqTimeout = New Label()
        nudConnTimeout = New NumericUpDown()
        lblConnTimeout = New Label()
        tabDataConnection = New TabPage()
        grpDatabase = New GroupBox()
        lblConn = New Label()
        txtDbConnection = New TextBox()
        btnTestDb = New Button()
        btnSaveDb = New Button()
        btnLoadDb = New Button()
        lblDbStatus = New Label()
        grpLog = New GroupBox()
        lvLog = New ListView()
        colTime = New ColumnHeader()
        colLevel = New ColumnHeader()
        colMessage = New ColumnHeader()
        status = New StatusStrip()
        lblConnIndicator = New ToolStripStatusLabel()
        lblConnStatus = New ToolStripStatusLabel()
        lblLatency = New ToolStripStatusLabel()
        lblActiveProfile = New ToolStripStatusLabel()
        tsTop.SuspendLayout()
        CType(splitMain, ComponentModel.ISupportInitialize).BeginInit()
        splitMain.Panel1.SuspendLayout()
        splitMain.Panel2.SuspendLayout()
        splitMain.SuspendLayout()
        grpProfiles.SuspendLayout()
        tabRight.SuspendLayout()
        tabEndpoint.SuspendLayout()
        grpLiveInfo.SuspendLayout()
        grpEndpoint.SuspendLayout()
        CType(nudClientId, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPort, ComponentModel.ISupportInitialize).BeginInit()
        tabAdvanced.SuspendLayout()
        grpBehavior.SuspendLayout()
        grpThrottles.SuspendLayout()
        CType(nudMaxReqPerSec, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudHeartbeat, ComponentModel.ISupportInitialize).BeginInit()
        tabNetwork.SuspendLayout()
        grpProxy.SuspendLayout()
        CType(nudProxyPort, ComponentModel.ISupportInitialize).BeginInit()
        grpTimeouts.SuspendLayout()
        CType(nudReqTimeout, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudConnTimeout, ComponentModel.ISupportInitialize).BeginInit()
        tabDataConnection.SuspendLayout()
        grpDatabase.SuspendLayout()
        grpLog.SuspendLayout()
        status.SuspendLayout()
        SuspendLayout()
        ' 
        ' tsTop
        ' 
        tsTop.GripStyle = ToolStripGripStyle.Hidden
        tsTop.Items.AddRange(New ToolStripItem() {btnConnect, btnDisconnect, ToolStripSeparator1, btnSaveProfile, btnDeleteProfile, ToolStripSeparator2, btnTestPing})
        tsTop.Location = New Point(0, 0)
        tsTop.Name = "tsTop"
        tsTop.Size = New Size(1200, 25)
        tsTop.TabIndex = 0
        tsTop.Text = "ToolStrip1"
        ' 
        ' btnConnect
        ' 
        btnConnect.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnConnect.Name = "btnConnect"
        btnConnect.Size = New Size(56, 22)
        btnConnect.Text = "Connect"
        ' 
        ' btnDisconnect
        ' 
        btnDisconnect.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnDisconnect.Name = "btnDisconnect"
        btnDisconnect.Size = New Size(70, 22)
        btnDisconnect.Text = "Disconnect"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 25)
        ' 
        ' btnSaveProfile
        ' 
        btnSaveProfile.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnSaveProfile.Name = "btnSaveProfile"
        btnSaveProfile.Size = New Size(72, 22)
        btnSaveProfile.Text = "Save Profile"
        ' 
        ' btnDeleteProfile
        ' 
        btnDeleteProfile.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnDeleteProfile.Name = "btnDeleteProfile"
        btnDeleteProfile.Size = New Size(81, 22)
        btnDeleteProfile.Text = "Delete Profile"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 25)
        ' 
        ' btnTestPing
        ' 
        btnTestPing.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnTestPing.Name = "btnTestPing"
        btnTestPing.Size = New Size(58, 22)
        btnTestPing.Text = "Test Ping"
        ' 
        ' splitMain
        ' 
        splitMain.Dock = DockStyle.Fill
        splitMain.Location = New Point(0, 25)
        splitMain.Name = "splitMain"
        splitMain.Orientation = Orientation.Horizontal
        ' 
        ' splitMain.Panel1
        ' 
        splitMain.Panel1.Controls.Add(grpProfiles)
        splitMain.Panel1.Controls.Add(tabRight)
        ' 
        ' splitMain.Panel2
        ' 
        splitMain.Panel2.Controls.Add(grpLog)
        splitMain.Size = New Size(1200, 595)
        splitMain.SplitterDistance = 411
        splitMain.TabIndex = 1
        ' 
        ' grpProfiles
        ' 
        grpProfiles.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        grpProfiles.Controls.Add(btnSetDefault)
        grpProfiles.Controls.Add(btnRemove)
        grpProfiles.Controls.Add(btnAdd)
        grpProfiles.Controls.Add(txtProfileName)
        grpProfiles.Controls.Add(lblProfileName)
        grpProfiles.Controls.Add(lbProfiles)
        grpProfiles.Location = New Point(12, 6)
        grpProfiles.Name = "grpProfiles"
        grpProfiles.Padding = New Padding(6)
        grpProfiles.Size = New Size(260, 399)
        grpProfiles.TabIndex = 0
        grpProfiles.TabStop = False
        grpProfiles.Text = "Profiles"
        ' 
        ' btnSetDefault
        ' 
        btnSetDefault.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnSetDefault.Location = New Point(10, 362)
        btnSetDefault.Name = "btnSetDefault"
        btnSetDefault.Size = New Size(240, 26)
        btnSetDefault.TabIndex = 5
        btnSetDefault.Text = "Set Selected as Default"
        btnSetDefault.UseVisualStyleBackColor = True
        ' 
        ' btnRemove
        ' 
        btnRemove.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnRemove.Location = New Point(135, 330)
        btnRemove.Name = "btnRemove"
        btnRemove.Size = New Size(115, 26)
        btnRemove.TabIndex = 4
        btnRemove.Text = "Remove"
        btnRemove.UseVisualStyleBackColor = True
        ' 
        ' btnAdd
        ' 
        btnAdd.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnAdd.Location = New Point(10, 330)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(115, 26)
        btnAdd.TabIndex = 3
        btnAdd.Text = "Add / Update"
        btnAdd.UseVisualStyleBackColor = True
        ' 
        ' txtProfileName
        ' 
        txtProfileName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtProfileName.Location = New Point(10, 40)
        txtProfileName.Name = "txtProfileName"
        txtProfileName.PlaceholderText = "Profile name (e.g., Paper TWS)"
        txtProfileName.Size = New Size(240, 23)
        txtProfileName.TabIndex = 1
        ' 
        ' lblProfileName
        ' 
        lblProfileName.AutoSize = True
        lblProfileName.Location = New Point(10, 22)
        lblProfileName.Name = "lblProfileName"
        lblProfileName.Size = New Size(74, 15)
        lblProfileName.TabIndex = 0
        lblProfileName.Text = "Profile name"
        ' 
        ' lbProfiles
        ' 
        lbProfiles.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lbProfiles.FormattingEnabled = True
        lbProfiles.IntegralHeight = False
        lbProfiles.Location = New Point(10, 72)
        lbProfiles.Name = "lbProfiles"
        lbProfiles.Size = New Size(240, 252)
        lbProfiles.TabIndex = 2
        ' 
        ' tabRight
        ' 
        tabRight.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tabRight.Controls.Add(tabEndpoint)
        tabRight.Controls.Add(tabAdvanced)
        tabRight.Controls.Add(tabNetwork)
        tabRight.Controls.Add(tabDataConnection)
        tabRight.Location = New Point(278, 6)
        tabRight.Name = "tabRight"
        tabRight.SelectedIndex = 0
        tabRight.Size = New Size(910, 399)
        tabRight.TabIndex = 1
        ' 
        ' tabEndpoint
        ' 
        tabEndpoint.Controls.Add(grpLiveInfo)
        tabEndpoint.Controls.Add(grpEndpoint)
        tabEndpoint.Location = New Point(4, 24)
        tabEndpoint.Name = "tabEndpoint"
        tabEndpoint.Padding = New Padding(6)
        tabEndpoint.Size = New Size(902, 371)
        tabEndpoint.TabIndex = 0
        tabEndpoint.Text = "Gateway / TWS"
        tabEndpoint.UseVisualStyleBackColor = True
        ' 
        ' grpLiveInfo
        ' 
        grpLiveInfo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpLiveInfo.Controls.Add(lblServerVersionValue)
        grpLiveInfo.Controls.Add(lblServerVersion)
        grpLiveInfo.Controls.Add(lblServerTimeValue)
        grpLiveInfo.Controls.Add(lblServerTime)
        grpLiveInfo.Controls.Add(lblAccountValue)
        grpLiveInfo.Controls.Add(lblAccount)
        grpLiveInfo.Location = New Point(9, 200)
        grpLiveInfo.Name = "grpLiveInfo"
        grpLiveInfo.Padding = New Padding(8)
        grpLiveInfo.Size = New Size(884, 80)
        grpLiveInfo.TabIndex = 1
        grpLiveInfo.TabStop = False
        grpLiveInfo.Text = "Live Info"
        ' 
        ' lblServerVersionValue
        ' 
        lblServerVersionValue.AutoSize = True
        lblServerVersionValue.Location = New Point(351, 51)
        lblServerVersionValue.Name = "lblServerVersionValue"
        lblServerVersionValue.Size = New Size(19, 15)
        lblServerVersionValue.TabIndex = 5
        lblServerVersionValue.Text = "—"
        ' 
        ' lblServerVersion
        ' 
        lblServerVersion.AutoSize = True
        lblServerVersion.Location = New Point(264, 51)
        lblServerVersion.Name = "lblServerVersion"
        lblServerVersion.Size = New Size(80, 15)
        lblServerVersion.TabIndex = 4
        lblServerVersion.Text = "Server version"
        ' 
        ' lblServerTimeValue
        ' 
        lblServerTimeValue.AutoSize = True
        lblServerTimeValue.Location = New Point(351, 26)
        lblServerTimeValue.Name = "lblServerTimeValue"
        lblServerTimeValue.Size = New Size(19, 15)
        lblServerTimeValue.TabIndex = 3
        lblServerTimeValue.Text = "—"
        ' 
        ' lblServerTime
        ' 
        lblServerTime.AutoSize = True
        lblServerTime.Location = New Point(264, 26)
        lblServerTime.Name = "lblServerTime"
        lblServerTime.Size = New Size(66, 15)
        lblServerTime.TabIndex = 2
        lblServerTime.Text = "Server time"
        ' 
        ' lblAccountValue
        ' 
        lblAccountValue.AutoSize = True
        lblAccountValue.Location = New Point(79, 26)
        lblAccountValue.Name = "lblAccountValue"
        lblAccountValue.Size = New Size(19, 15)
        lblAccountValue.TabIndex = 1
        lblAccountValue.Text = "—"
        ' 
        ' lblAccount
        ' 
        lblAccount.AutoSize = True
        lblAccount.Location = New Point(14, 26)
        lblAccount.Name = "lblAccount"
        lblAccount.Size = New Size(52, 15)
        lblAccount.TabIndex = 0
        lblAccount.Text = "Account"
        ' 
        ' grpEndpoint
        ' 
        grpEndpoint.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpEndpoint.Controls.Add(chkConnectAtStartup)
        grpEndpoint.Controls.Add(chkSSL)
        grpEndpoint.Controls.Add(nudClientId)
        grpEndpoint.Controls.Add(lblClientId)
        grpEndpoint.Controls.Add(nudPort)
        grpEndpoint.Controls.Add(lblPort)
        grpEndpoint.Controls.Add(txtHost)
        grpEndpoint.Controls.Add(lblHost)
        grpEndpoint.Controls.Add(cboConnType)
        grpEndpoint.Controls.Add(lblConnType)
        grpEndpoint.Location = New Point(9, 10)
        grpEndpoint.Name = "grpEndpoint"
        grpEndpoint.Padding = New Padding(8)
        grpEndpoint.Size = New Size(884, 184)
        grpEndpoint.TabIndex = 0
        grpEndpoint.TabStop = False
        grpEndpoint.Text = "Endpoint"
        ' 
        ' chkConnectAtStartup
        ' 
        chkConnectAtStartup.AutoSize = True
        chkConnectAtStartup.Location = New Point(17, 144)
        chkConnectAtStartup.Name = "chkConnectAtStartup"
        chkConnectAtStartup.Size = New Size(124, 19)
        chkConnectAtStartup.TabIndex = 9
        chkConnectAtStartup.Text = "Connect at startup"
        chkConnectAtStartup.UseVisualStyleBackColor = True
        ' 
        ' chkSSL
        ' 
        chkSSL.AutoSize = True
        chkSSL.Location = New Point(17, 119)
        chkSSL.Name = "chkSSL"
        chkSSL.Size = New Size(44, 19)
        chkSSL.TabIndex = 8
        chkSSL.Text = "SSL"
        chkSSL.UseVisualStyleBackColor = True
        ' 
        ' nudClientId
        ' 
        nudClientId.Location = New Point(497, 80)
        nudClientId.Maximum = New Decimal(New Integer() {Integer.MaxValue, 0, 0, 0})
        nudClientId.Name = "nudClientId"
        nudClientId.Size = New Size(120, 23)
        nudClientId.TabIndex = 7
        ' 
        ' lblClientId
        ' 
        lblClientId.AutoSize = True
        lblClientId.Location = New Point(432, 84)
        lblClientId.Name = "lblClientId"
        lblClientId.Size = New Size(52, 15)
        lblClientId.TabIndex = 6
        lblClientId.Text = "Client ID"
        ' 
        ' nudPort
        ' 
        nudPort.Location = New Point(280, 80)
        nudPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        nudPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudPort.Name = "nudPort"
        nudPort.Size = New Size(120, 23)
        nudPort.TabIndex = 5
        nudPort.Value = New Decimal(New Integer() {7497, 0, 0, 0})
        ' 
        ' lblPort
        ' 
        lblPort.AutoSize = True
        lblPort.Location = New Point(245, 84)
        lblPort.Name = "lblPort"
        lblPort.Size = New Size(29, 15)
        lblPort.TabIndex = 4
        lblPort.Text = "Port"
        ' 
        ' txtHost
        ' 
        txtHost.Location = New Point(58, 80)
        txtHost.Name = "txtHost"
        txtHost.PlaceholderText = "127.0.0.1"
        txtHost.Size = New Size(170, 23)
        txtHost.TabIndex = 3
        ' 
        ' lblHost
        ' 
        lblHost.AutoSize = True
        lblHost.Location = New Point(14, 84)
        lblHost.Name = "lblHost"
        lblHost.Size = New Size(32, 15)
        lblHost.TabIndex = 2
        lblHost.Text = "Host"
        ' 
        ' cboConnType
        ' 
        cboConnType.DropDownStyle = ComboBoxStyle.DropDownList
        cboConnType.FormattingEnabled = True
        cboConnType.Items.AddRange(New Object() {"TWS", "IB Gateway"})
        cboConnType.Location = New Point(98, 36)
        cboConnType.Name = "cboConnType"
        cboConnType.Size = New Size(130, 23)
        cboConnType.TabIndex = 1
        ' 
        ' lblConnType
        ' 
        lblConnType.AutoSize = True
        lblConnType.Location = New Point(14, 40)
        lblConnType.Name = "lblConnType"
        lblConnType.Size = New Size(69, 15)
        lblConnType.TabIndex = 0
        lblConnType.Text = "Connection"
        ' 
        ' tabAdvanced
        ' 
        tabAdvanced.Controls.Add(grpBehavior)
        tabAdvanced.Controls.Add(grpThrottles)
        tabAdvanced.Location = New Point(4, 24)
        tabAdvanced.Name = "tabAdvanced"
        tabAdvanced.Padding = New Padding(6)
        tabAdvanced.Size = New Size(902, 371)
        tabAdvanced.TabIndex = 1
        tabAdvanced.Text = "Advanced"
        tabAdvanced.UseVisualStyleBackColor = True
        ' 
        ' grpBehavior
        ' 
        grpBehavior.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpBehavior.Controls.Add(chkLogIbMessages)
        grpBehavior.Controls.Add(chkResubPositions)
        grpBehavior.Controls.Add(chkResubMarketData)
        grpBehavior.Controls.Add(chkAutoReconnect)
        grpBehavior.Location = New Point(9, 10)
        grpBehavior.Name = "grpBehavior"
        grpBehavior.Padding = New Padding(8)
        grpBehavior.Size = New Size(884, 92)
        grpBehavior.TabIndex = 0
        grpBehavior.TabStop = False
        grpBehavior.Text = "Behavior"
        ' 
        ' chkLogIbMessages
        ' 
        chkLogIbMessages.AutoSize = True
        chkLogIbMessages.Location = New Point(17, 59)
        chkLogIbMessages.Name = "chkLogIbMessages"
        chkLogIbMessages.Size = New Size(191, 19)
        chkLogIbMessages.TabIndex = 3
        chkLogIbMessages.Text = "Log IB messages to Diagnostics"
        chkLogIbMessages.UseVisualStyleBackColor = True
        ' 
        ' chkResubPositions
        ' 
        chkResubPositions.AutoSize = True
        chkResubPositions.Location = New Point(491, 26)
        chkResubPositions.Name = "chkResubPositions"
        chkResubPositions.Size = New Size(213, 19)
        chkResubPositions.TabIndex = 2
        chkResubPositions.Text = "Resubscribe positions on reconnect"
        chkResubPositions.UseVisualStyleBackColor = True
        ' 
        ' chkResubMarketData
        ' 
        chkResubMarketData.AutoSize = True
        chkResubMarketData.Location = New Point(244, 26)
        chkResubMarketData.Name = "chkResubMarketData"
        chkResubMarketData.Size = New Size(228, 19)
        chkResubMarketData.TabIndex = 1
        chkResubMarketData.Text = "Resubscribe market data on reconnect"
        chkResubMarketData.UseVisualStyleBackColor = True
        ' 
        ' chkAutoReconnect
        ' 
        chkAutoReconnect.AutoSize = True
        chkAutoReconnect.Location = New Point(17, 26)
        chkAutoReconnect.Name = "chkAutoReconnect"
        chkAutoReconnect.Size = New Size(108, 19)
        chkAutoReconnect.TabIndex = 0
        chkAutoReconnect.Text = "Auto reconnect"
        chkAutoReconnect.UseVisualStyleBackColor = True
        ' 
        ' grpThrottles
        ' 
        grpThrottles.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpThrottles.Controls.Add(nudMaxReqPerSec)
        grpThrottles.Controls.Add(lblMaxReqPerSec)
        grpThrottles.Controls.Add(nudHeartbeat)
        grpThrottles.Controls.Add(lblHeartbeat)
        grpThrottles.Location = New Point(9, 112)
        grpThrottles.Name = "grpThrottles"
        grpThrottles.Padding = New Padding(8)
        grpThrottles.Size = New Size(884, 86)
        grpThrottles.TabIndex = 1
        grpThrottles.TabStop = False
        grpThrottles.Text = "Throttles"
        ' 
        ' nudMaxReqPerSec
        ' 
        nudMaxReqPerSec.Location = New Point(351, 36)
        nudMaxReqPerSec.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        nudMaxReqPerSec.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudMaxReqPerSec.Name = "nudMaxReqPerSec"
        nudMaxReqPerSec.Size = New Size(80, 23)
        nudMaxReqPerSec.TabIndex = 3
        nudMaxReqPerSec.Value = New Decimal(New Integer() {50, 0, 0, 0})
        ' 
        ' lblMaxReqPerSec
        ' 
        lblMaxReqPerSec.AutoSize = True
        lblMaxReqPerSec.Location = New Point(241, 40)
        lblMaxReqPerSec.Name = "lblMaxReqPerSec"
        lblMaxReqPerSec.Size = New Size(104, 15)
        lblMaxReqPerSec.TabIndex = 2
        lblMaxReqPerSec.Text = "Max reqs / second"
        ' 
        ' nudHeartbeat
        ' 
        nudHeartbeat.Location = New Point(98, 36)
        nudHeartbeat.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        nudHeartbeat.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        nudHeartbeat.Name = "nudHeartbeat"
        nudHeartbeat.Size = New Size(80, 23)
        nudHeartbeat.TabIndex = 1
        nudHeartbeat.Value = New Decimal(New Integer() {150, 0, 0, 0})
        ' 
        ' lblHeartbeat
        ' 
        lblHeartbeat.AutoSize = True
        lblHeartbeat.Location = New Point(14, 40)
        lblHeartbeat.Name = "lblHeartbeat"
        lblHeartbeat.Size = New Size(86, 15)
        lblHeartbeat.TabIndex = 0
        lblHeartbeat.Text = "Heartbeat (ms)"
        ' 
        ' tabNetwork
        ' 
        tabNetwork.Controls.Add(grpProxy)
        tabNetwork.Controls.Add(grpTimeouts)
        tabNetwork.Location = New Point(4, 24)
        tabNetwork.Name = "tabNetwork"
        tabNetwork.Padding = New Padding(6)
        tabNetwork.Size = New Size(902, 371)
        tabNetwork.TabIndex = 2
        tabNetwork.Text = "Network"
        tabNetwork.UseVisualStyleBackColor = True
        ' 
        ' grpProxy
        ' 
        grpProxy.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpProxy.Controls.Add(chkUseProxy)
        grpProxy.Controls.Add(nudProxyPort)
        grpProxy.Controls.Add(lblProxyPort)
        grpProxy.Controls.Add(txtProxyHost)
        grpProxy.Controls.Add(lblProxyHost)
        grpProxy.Location = New Point(9, 10)
        grpProxy.Name = "grpProxy"
        grpProxy.Padding = New Padding(8)
        grpProxy.Size = New Size(884, 88)
        grpProxy.TabIndex = 0
        grpProxy.TabStop = False
        grpProxy.Text = "Proxy"
        ' 
        ' chkUseProxy
        ' 
        chkUseProxy.AutoSize = True
        chkUseProxy.Location = New Point(17, 26)
        chkUseProxy.Name = "chkUseProxy"
        chkUseProxy.Size = New Size(78, 19)
        chkUseProxy.TabIndex = 0
        chkUseProxy.Text = "Use proxy"
        chkUseProxy.UseVisualStyleBackColor = True
        ' 
        ' nudProxyPort
        ' 
        nudProxyPort.Location = New Point(497, 52)
        nudProxyPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        nudProxyPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudProxyPort.Name = "nudProxyPort"
        nudProxyPort.Size = New Size(120, 23)
        nudProxyPort.TabIndex = 4
        nudProxyPort.Value = New Decimal(New Integer() {8080, 0, 0, 0})
        ' 
        ' lblProxyPort
        ' 
        lblProxyPort.AutoSize = True
        lblProxyPort.Location = New Point(432, 56)
        lblProxyPort.Name = "lblProxyPort"
        lblProxyPort.Size = New Size(29, 15)
        lblProxyPort.TabIndex = 3
        lblProxyPort.Text = "Port"
        ' 
        ' txtProxyHost
        ' 
        txtProxyHost.Location = New Point(98, 52)
        txtProxyHost.Name = "txtProxyHost"
        txtProxyHost.PlaceholderText = "proxy.mycorp.local"
        txtProxyHost.Size = New Size(310, 23)
        txtProxyHost.TabIndex = 2
        ' 
        ' lblProxyHost
        ' 
        lblProxyHost.AutoSize = True
        lblProxyHost.Location = New Point(14, 56)
        lblProxyHost.Name = "lblProxyHost"
        lblProxyHost.Size = New Size(63, 15)
        lblProxyHost.TabIndex = 1
        lblProxyHost.Text = "Proxy host"
        ' 
        ' grpTimeouts
        ' 
        grpTimeouts.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpTimeouts.Controls.Add(nudReqTimeout)
        grpTimeouts.Controls.Add(lblReqTimeout)
        grpTimeouts.Controls.Add(nudConnTimeout)
        grpTimeouts.Controls.Add(lblConnTimeout)
        grpTimeouts.Location = New Point(9, 108)
        grpTimeouts.Name = "grpTimeouts"
        grpTimeouts.Padding = New Padding(8)
        grpTimeouts.Size = New Size(884, 86)
        grpTimeouts.TabIndex = 1
        grpTimeouts.TabStop = False
        grpTimeouts.Text = "Timeouts (seconds)"
        ' 
        ' nudReqTimeout
        ' 
        nudReqTimeout.Location = New Point(351, 36)
        nudReqTimeout.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        nudReqTimeout.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudReqTimeout.Name = "nudReqTimeout"
        nudReqTimeout.Size = New Size(80, 23)
        nudReqTimeout.TabIndex = 3
        nudReqTimeout.Value = New Decimal(New Integer() {30, 0, 0, 0})
        ' 
        ' lblReqTimeout
        ' 
        lblReqTimeout.AutoSize = True
        lblReqTimeout.Location = New Point(241, 40)
        lblReqTimeout.Name = "lblReqTimeout"
        lblReqTimeout.Size = New Size(94, 15)
        lblReqTimeout.TabIndex = 2
        lblReqTimeout.Text = "Request timeout"
        ' 
        ' nudConnTimeout
        ' 
        nudConnTimeout.Location = New Point(98, 36)
        nudConnTimeout.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        nudConnTimeout.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudConnTimeout.Name = "nudConnTimeout"
        nudConnTimeout.Size = New Size(80, 23)
        nudConnTimeout.TabIndex = 1
        nudConnTimeout.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' lblConnTimeout
        ' 
        lblConnTimeout.AutoSize = True
        lblConnTimeout.Location = New Point(14, 40)
        lblConnTimeout.Name = "lblConnTimeout"
        lblConnTimeout.Size = New Size(114, 15)
        lblConnTimeout.TabIndex = 0
        lblConnTimeout.Text = "Connection timeout"
        ' 
        ' tabDataConnection
        ' 
        tabDataConnection.Controls.Add(grpDatabase)
        tabDataConnection.Location = New Point(4, 24)
        tabDataConnection.Name = "tabDataConnection"
        tabDataConnection.Size = New Size(902, 371)
        tabDataConnection.TabIndex = 3
        tabDataConnection.Text = "Data Connection"
        tabDataConnection.UseVisualStyleBackColor = True
        ' 
        ' grpDatabase
        ' 
        grpDatabase.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpDatabase.Controls.Add(lblConn)
        grpDatabase.Controls.Add(txtDbConnection)
        grpDatabase.Controls.Add(btnTestDb)
        grpDatabase.Controls.Add(btnSaveDb)
        grpDatabase.Controls.Add(btnLoadDb)
        grpDatabase.Controls.Add(lblDbStatus)
        grpDatabase.Location = New Point(12, 12)
        grpDatabase.Name = "grpDatabase"
        grpDatabase.Size = New Size(560, 150)
        grpDatabase.TabIndex = 0
        grpDatabase.TabStop = False
        grpDatabase.Text = "Database"
        ' 
        ' lblConn
        ' 
        lblConn.AutoSize = True
        lblConn.Location = New Point(12, 28)
        lblConn.Name = "lblConn"
        lblConn.Size = New Size(106, 15)
        lblConn.TabIndex = 0
        lblConn.Text = "Connection String:"
        ' 
        ' txtDbConnection
        ' 
        txtDbConnection.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtDbConnection.Location = New Point(15, 46)
        txtDbConnection.Name = "txtDbConnection"
        txtDbConnection.Size = New Size(530, 23)
        txtDbConnection.TabIndex = 1
        ' 
        ' btnTestDb
        ' 
        btnTestDb.Location = New Point(15, 84)
        btnTestDb.Name = "btnTestDb"
        btnTestDb.Size = New Size(90, 28)
        btnTestDb.TabIndex = 2
        btnTestDb.Text = "Test"
        btnTestDb.UseVisualStyleBackColor = True
        ' 
        ' btnSaveDb
        ' 
        btnSaveDb.Location = New Point(115, 84)
        btnSaveDb.Name = "btnSaveDb"
        btnSaveDb.Size = New Size(90, 28)
        btnSaveDb.TabIndex = 3
        btnSaveDb.Text = "Save"
        btnSaveDb.UseVisualStyleBackColor = True
        ' 
        ' btnLoadDb
        ' 
        btnLoadDb.Location = New Point(215, 84)
        btnLoadDb.Name = "btnLoadDb"
        btnLoadDb.Size = New Size(90, 28)
        btnLoadDb.TabIndex = 4
        btnLoadDb.Text = "Load"
        btnLoadDb.UseVisualStyleBackColor = True
        ' 
        ' lblDbStatus
        ' 
        lblDbStatus.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblDbStatus.ForeColor = Color.DimGray
        lblDbStatus.Location = New Point(15, 122)
        lblDbStatus.Name = "lblDbStatus"
        lblDbStatus.Size = New Size(530, 20)
        lblDbStatus.TabIndex = 5
        lblDbStatus.Text = "Enter a connection string and click Test."
        ' 
        ' grpLog
        ' 
        grpLog.Controls.Add(lvLog)
        grpLog.Dock = DockStyle.Fill
        grpLog.Location = New Point(0, 0)
        grpLog.Name = "grpLog"
        grpLog.Padding = New Padding(6)
        grpLog.Size = New Size(1200, 180)
        grpLog.TabIndex = 0
        grpLog.TabStop = False
        grpLog.Text = "Diagnostics"
        ' 
        ' lvLog
        ' 
        lvLog.Columns.AddRange(New ColumnHeader() {colTime, colLevel, colMessage})
        lvLog.Dock = DockStyle.Fill
        lvLog.FullRowSelect = True
        lvLog.GridLines = True
        lvLog.Location = New Point(6, 22)
        lvLog.MultiSelect = False
        lvLog.Name = "lvLog"
        lvLog.Size = New Size(1188, 152)
        lvLog.TabIndex = 0
        lvLog.UseCompatibleStateImageBehavior = False
        lvLog.View = View.Details
        ' 
        ' colTime
        ' 
        colTime.Text = "Time"
        colTime.Width = 150
        ' 
        ' colLevel
        ' 
        colLevel.Text = "Level"
        colLevel.Width = 80
        ' 
        ' colMessage
        ' 
        colMessage.Text = "Message"
        colMessage.Width = 900
        ' 
        ' status
        ' 
        status.Items.AddRange(New ToolStripItem() {lblConnIndicator, lblConnStatus, lblLatency, lblActiveProfile})
        status.Location = New Point(0, 620)
        status.Name = "status"
        status.Size = New Size(1200, 22)
        status.TabIndex = 2
        status.Text = "StatusStrip1"
        ' 
        ' lblConnIndicator
        ' 
        lblConnIndicator.Margin = New Padding(5, 3, 0, 2)
        lblConnIndicator.Name = "lblConnIndicator"
        lblConnIndicator.Size = New Size(14, 17)
        lblConnIndicator.Text = "●"
        ' 
        ' lblConnStatus
        ' 
        lblConnStatus.Margin = New Padding(0, 3, 15, 2)
        lblConnStatus.Name = "lblConnStatus"
        lblConnStatus.Size = New Size(79, 17)
        lblConnStatus.Text = "Disconnected"
        ' 
        ' lblLatency
        ' 
        lblLatency.Margin = New Padding(0, 3, 15, 2)
        lblLatency.Name = "lblLatency"
        lblLatency.Size = New Size(66, 17)
        lblLatency.Text = "Latency: —"
        ' 
        ' lblActiveProfile
        ' 
        lblActiveProfile.Name = "lblActiveProfile"
        lblActiveProfile.Size = New Size(99, 17)
        lblActiveProfile.Text = "Profile: (unsaved)"
        ' 
        ' ConnectionManager
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1200, 642)
        Controls.Add(splitMain)
        Controls.Add(tsTop)
        Controls.Add(status)
        MinimumSize = New Size(1100, 600)
        Name = "ConnectionManager"
        StartPosition = FormStartPosition.CenterParent
        Text = "Connection Manager"
        tsTop.ResumeLayout(False)
        tsTop.PerformLayout()
        splitMain.Panel1.ResumeLayout(False)
        splitMain.Panel2.ResumeLayout(False)
        CType(splitMain, ComponentModel.ISupportInitialize).EndInit()
        splitMain.ResumeLayout(False)
        grpProfiles.ResumeLayout(False)
        grpProfiles.PerformLayout()
        tabRight.ResumeLayout(False)
        tabEndpoint.ResumeLayout(False)
        grpLiveInfo.ResumeLayout(False)
        grpLiveInfo.PerformLayout()
        grpEndpoint.ResumeLayout(False)
        grpEndpoint.PerformLayout()
        CType(nudClientId, ComponentModel.ISupportInitialize).EndInit()
        CType(nudPort, ComponentModel.ISupportInitialize).EndInit()
        tabAdvanced.ResumeLayout(False)
        grpBehavior.ResumeLayout(False)
        grpBehavior.PerformLayout()
        grpThrottles.ResumeLayout(False)
        grpThrottles.PerformLayout()
        CType(nudMaxReqPerSec, ComponentModel.ISupportInitialize).EndInit()
        CType(nudHeartbeat, ComponentModel.ISupportInitialize).EndInit()
        tabNetwork.ResumeLayout(False)
        grpProxy.ResumeLayout(False)
        grpProxy.PerformLayout()
        CType(nudProxyPort, ComponentModel.ISupportInitialize).EndInit()
        grpTimeouts.ResumeLayout(False)
        grpTimeouts.PerformLayout()
        CType(nudReqTimeout, ComponentModel.ISupportInitialize).EndInit()
        CType(nudConnTimeout, ComponentModel.ISupportInitialize).EndInit()
        tabDataConnection.ResumeLayout(False)
        grpDatabase.ResumeLayout(False)
        grpDatabase.PerformLayout()
        grpLog.ResumeLayout(False)
        status.ResumeLayout(False)
        status.PerformLayout()
        ResumeLayout(False)
        PerformLayout()


    End Sub

    Friend WithEvents tsTop As ToolStrip
    Friend WithEvents btnConnect As ToolStripButton
    Friend WithEvents btnDisconnect As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnSaveProfile As ToolStripButton
    Friend WithEvents btnDeleteProfile As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnTestPing As ToolStripButton
    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents grpProfiles As GroupBox
    Friend WithEvents lbProfiles As ListBox
    Friend WithEvents tabRight As TabControl
    Friend WithEvents tabEndpoint As TabPage
    Friend WithEvents grpEndpoint As GroupBox
    Friend WithEvents cboConnType As ComboBox
    Friend WithEvents lblConnType As Label
    Friend WithEvents txtHost As TextBox
    Friend WithEvents lblHost As Label
    Friend WithEvents nudPort As NumericUpDown
    Friend WithEvents lblPort As Label
    Friend WithEvents nudClientId As NumericUpDown
    Friend WithEvents lblClientId As Label
    Friend WithEvents chkSSL As CheckBox
    Friend WithEvents chkConnectAtStartup As CheckBox
    Friend WithEvents grpLiveInfo As GroupBox
    Friend WithEvents lblServerVersionValue As Label
    Friend WithEvents lblServerVersion As Label
    Friend WithEvents lblServerTimeValue As Label
    Friend WithEvents lblServerTime As Label
    Friend WithEvents lblAccountValue As Label
    Friend WithEvents lblAccount As Label
    Friend WithEvents tabAdvanced As TabPage
    Friend WithEvents grpBehavior As GroupBox
    Friend WithEvents chkAutoReconnect As CheckBox
    Friend WithEvents chkResubMarketData As CheckBox
    Friend WithEvents chkResubPositions As CheckBox
    Friend WithEvents chkLogIbMessages As CheckBox
    Friend WithEvents grpThrottles As GroupBox
    Friend WithEvents nudHeartbeat As NumericUpDown
    Friend WithEvents lblHeartbeat As Label
    Friend WithEvents nudMaxReqPerSec As NumericUpDown
    Friend WithEvents lblMaxReqPerSec As Label
    Friend WithEvents tabNetwork As TabPage
    Friend WithEvents grpProxy As GroupBox
    Friend WithEvents chkUseProxy As CheckBox
    Friend WithEvents nudProxyPort As NumericUpDown
    Friend WithEvents lblProxyPort As Label
    Friend WithEvents txtProxyHost As TextBox
    Friend WithEvents lblProxyHost As Label
    Friend WithEvents grpTimeouts As GroupBox
    Friend WithEvents nudReqTimeout As NumericUpDown
    Friend WithEvents lblReqTimeout As Label
    Friend WithEvents nudConnTimeout As NumericUpDown
    Friend WithEvents lblConnTimeout As Label
    Friend WithEvents grpLog As GroupBox
    Friend WithEvents lvLog As ListView
    Friend WithEvents colTime As ColumnHeader
    Friend WithEvents colLevel As ColumnHeader
    Friend WithEvents colMessage As ColumnHeader
    Friend WithEvents status As StatusStrip
    Friend WithEvents lblConnIndicator As ToolStripStatusLabel
    Friend WithEvents lblConnStatus As ToolStripStatusLabel
    Friend WithEvents lblLatency As ToolStripStatusLabel
    Friend WithEvents lblActiveProfile As ToolStripStatusLabel
    Friend WithEvents btnRemove As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents txtProfileName As TextBox
    Friend WithEvents lblProfileName As Label
    Friend WithEvents btnSetDefault As Button
    Friend WithEvents grpDatabase As System.Windows.Forms.GroupBox
    Friend WithEvents lblConn As System.Windows.Forms.Label
    Friend WithEvents txtDbConnection As System.Windows.Forms.TextBox
    Friend WithEvents btnTestDb As System.Windows.Forms.Button
    Friend WithEvents btnSaveDb As System.Windows.Forms.Button
    Friend WithEvents btnLoadDb As System.Windows.Forms.Button
    Friend WithEvents lblDbStatus As System.Windows.Forms.Label
    Friend WithEvents tabDataConnection As TabPage

End Class
