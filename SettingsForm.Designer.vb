<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsForm
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tabGeneral = New System.Windows.Forms.TabPage()
        Me.grpStartup = New System.Windows.Forms.GroupBox()
        Me.chkRestoreLayout = New System.Windows.Forms.CheckBox()
        Me.chkConnectStartup = New System.Windows.Forms.CheckBox()
        Me.grpAppearance = New System.Windows.Forms.GroupBox()
        Me.cboTheme = New System.Windows.Forms.ComboBox()
        Me.lblTheme = New System.Windows.Forms.Label()
        Me.cboLogLevel = New System.Windows.Forms.ComboBox()
        Me.lblLogLevel = New System.Windows.Forms.Label()
        Me.tabTrading = New System.Windows.Forms.TabPage()
        Me.grpTradingDefaults = New System.Windows.Forms.GroupBox()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.cboAccount = New System.Windows.Forms.ComboBox()
        Me.lblDefSize = New System.Windows.Forms.Label()
        Me.nudDefaultSize = New System.Windows.Forms.NumericUpDown()
        Me.lblTif = New System.Windows.Forms.Label()
        Me.cboTif = New System.Windows.Forms.ComboBox()
        Me.chkConfirmPlace = New System.Windows.Forms.CheckBox()
        Me.chkConfirmCancel = New System.Windows.Forms.CheckBox()
        Me.tabRisk = New System.Windows.Forms.TabPage()
        Me.grpRiskLimits = New System.Windows.Forms.GroupBox()
        Me.lblDailyLoss = New System.Windows.Forms.Label()
        Me.nudDailyLoss = New System.Windows.Forms.NumericUpDown()
        Me.lblTradeLoss = New System.Windows.Forms.Label()
        Me.nudTradeLoss = New System.Windows.Forms.NumericUpDown()
        Me.lblPosPerSym = New System.Windows.Forms.Label()
        Me.nudPosPerSym = New System.Windows.Forms.NumericUpDown()
        Me.chkStopOnLimit = New System.Windows.Forms.CheckBox()
        Me.tabMarket = New System.Windows.Forms.TabPage()
        Me.grpMktData = New System.Windows.Forms.GroupBox()
        Me.chkRthOnly = New System.Windows.Forms.CheckBox()
        Me.chkStreamQuotes = New System.Windows.Forms.CheckBox()
        Me.lblPacing = New System.Windows.Forms.Label()
        Me.nudPacing = New System.Windows.Forms.NumericUpDown()
        Me.tabNotify = New System.Windows.Forms.TabPage()
        Me.grpNotify = New System.Windows.Forms.GroupBox()
        Me.chkPopup = New System.Windows.Forms.CheckBox()
        Me.chkSound = New System.Windows.Forms.CheckBox()
        Me.txtSoundPath = New System.Windows.Forms.TextBox()
        Me.btnBrowseSound = New System.Windows.Forms.Button()
        Me.lblSound = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.tabStorage = New System.Windows.Forms.TabPage()
        Me.grpStorage = New System.Windows.Forms.GroupBox()
        Me.txtDataFolder = New System.Windows.Forms.TextBox()
        Me.btnBrowseFolder = New System.Windows.Forms.Button()
        Me.lblDataFolder = New System.Windows.Forms.Label()
        Me.btnClearCache = New System.Windows.Forms.Button()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.dlgFolder = New System.Windows.Forms.FolderBrowserDialog()
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.lblHint = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tabMain.SuspendLayout()
        Me.tabGeneral.SuspendLayout()
        Me.grpStartup.SuspendLayout()
        Me.grpAppearance.SuspendLayout()
        Me.tabTrading.SuspendLayout()
        Me.grpTradingDefaults.SuspendLayout()
        CType(Me.nudDefaultSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabRisk.SuspendLayout()
        Me.grpRiskLimits.SuspendLayout()
        CType(Me.nudDailyLoss, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudTradeLoss, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudPosPerSym, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMarket.SuspendLayout()
        Me.grpMktData.SuspendLayout()
        CType(Me.nudPacing, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabNotify.SuspendLayout()
        Me.grpNotify.SuspendLayout()
        Me.tabStorage.SuspendLayout()
        Me.grpStorage.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.status.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tabGeneral)
        Me.tabMain.Controls.Add(Me.tabTrading)
        Me.tabMain.Controls.Add(Me.tabRisk)
        Me.tabMain.Controls.Add(Me.tabMarket)
        Me.tabMain.Controls.Add(Me.tabNotify)
        Me.tabMain.Controls.Add(Me.tabStorage)
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Location = New System.Drawing.Point(0, 0)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(880, 486)
        Me.tabMain.TabIndex = 0
        '
        'tabGeneral
        '
        Me.tabGeneral.Controls.Add(Me.grpAppearance)
        Me.tabGeneral.Controls.Add(Me.grpStartup)
        Me.tabGeneral.Location = New System.Drawing.Point(4, 24)
        Me.tabGeneral.Name = "tabGeneral"
        Me.tabGeneral.Padding = New System.Windows.Forms.Padding(8)
        Me.tabGeneral.Size = New System.Drawing.Size(872, 458)
        Me.tabGeneral.TabIndex = 0
        Me.tabGeneral.Text = "General"
        Me.tabGeneral.UseVisualStyleBackColor = True
        '
        'grpStartup
        '
        Me.grpStartup.Controls.Add(Me.chkRestoreLayout)
        Me.grpStartup.Controls.Add(Me.chkConnectStartup)
        Me.grpStartup.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpStartup.Location = New System.Drawing.Point(8, 8)
        Me.grpStartup.Name = "grpStartup"
        Me.grpStartup.Padding = New System.Windows.Forms.Padding(8)
        Me.grpStartup.Size = New System.Drawing.Size(856, 72)
        Me.grpStartup.TabIndex = 0
        Me.grpStartup.TabStop = False
        Me.grpStartup.Text = "Startup"
        '
        'chkRestoreLayout
        '
        Me.chkRestoreLayout.AutoSize = True
        Me.chkRestoreLayout.Location = New System.Drawing.Point(18, 45)
        Me.chkRestoreLayout.Name = "chkRestoreLayout"
        Me.chkRestoreLayout.Size = New System.Drawing.Size(227, 19)
        Me.chkRestoreLayout.TabIndex = 1
        Me.chkRestoreLayout.Text = "Restore window layout on startup"
        Me.chkRestoreLayout.UseVisualStyleBackColor = True
        '
        'chkConnectStartup
        '
        Me.chkConnectStartup.AutoSize = True
        Me.chkConnectStartup.Location = New System.Drawing.Point(18, 22)
        Me.chkConnectStartup.Name = "chkConnectStartup"
        Me.chkConnectStartup.Size = New System.Drawing.Size(208, 19)
        Me.chkConnectStartup.TabIndex = 0
        Me.chkConnectStartup.Text = "Connect to TWS/Gateway at start"
        Me.chkConnectStartup.UseVisualStyleBackColor = True
        '
        'grpAppearance
        '
        Me.grpAppearance.Controls.Add(Me.cboTheme)
        Me.grpAppearance.Controls.Add(Me.lblTheme)
        Me.grpAppearance.Controls.Add(Me.cboLogLevel)
        Me.grpAppearance.Controls.Add(Me.lblLogLevel)
        Me.grpAppearance.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpAppearance.Location = New System.Drawing.Point(8, 80)
        Me.grpAppearance.Name = "grpAppearance"
        Me.grpAppearance.Padding = New System.Windows.Forms.Padding(8)
        Me.grpAppearance.Size = New System.Drawing.Size(856, 96)
        Me.grpAppearance.TabIndex = 1
        Me.grpAppearance.TabStop = False
        Me.grpAppearance.Text = "Appearance & Logging"
        '
        'cboTheme
        '
        Me.cboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTheme.FormattingEnabled = True
        Me.cboTheme.Items.AddRange(New Object() {"System", "Light", "Dark"})
        Me.cboTheme.Location = New System.Drawing.Point(96, 58)
        Me.cboTheme.Name = "cboTheme"
        Me.cboTheme.Size = New System.Drawing.Size(140, 23)
        Me.cboTheme.TabIndex = 3
        '
        'lblTheme
        '
        Me.lblTheme.AutoSize = True
        Me.lblTheme.Location = New System.Drawing.Point(18, 62)
        Me.lblTheme.Name = "lblTheme"
        Me.lblTheme.Size = New System.Drawing.Size(46, 15)
        Me.lblTheme.TabIndex = 2
        Me.lblTheme.Text = "Theme"
        '
        'cboLogLevel
        '
        Me.cboLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLogLevel.FormattingEnabled = True
        Me.cboLogLevel.Items.AddRange(New Object() {"Error", "Warning", "Info", "Debug", "Trace"})
        Me.cboLogLevel.Location = New System.Drawing.Point(96, 27)
        Me.cboLogLevel.Name = "cboLogLevel"
        Me.cboLogLevel.Size = New System.Drawing.Size(140, 23)
        Me.cboLogLevel.TabIndex = 1
        '
        'lblLogLevel
        '
        Me.lblLogLevel.AutoSize = True
        Me.lblLogLevel.Location = New System.Drawing.Point(18, 31)
        Me.lblLogLevel.Name = "lblLogLevel"
        Me.lblLogLevel.Size = New System.Drawing.Size(59, 15)
        Me.lblLogLevel.TabIndex = 0
        Me.lblLogLevel.Text = "Log level"
        '
        'tabTrading
        '
        Me.tabTrading.Controls.Add(Me.grpTradingDefaults)
        Me.tabTrading.Location = New System.Drawing.Point(4, 24)
        Me.tabTrading.Name = "tabTrading"
        Me.tabTrading.Padding = New System.Windows.Forms.Padding(8)
        Me.tabTrading.Size = New System.Drawing.Size(872, 458)
        Me.tabTrading.TabIndex = 1
        Me.tabTrading.Text = "Trading"
        Me.tabTrading.UseVisualStyleBackColor = True
        '
        'grpTradingDefaults
        '
        Me.grpTradingDefaults.Controls.Add(Me.chkConfirmCancel)
        Me.grpTradingDefaults.Controls.Add(Me.chkConfirmPlace)
        Me.grpTradingDefaults.Controls.Add(Me.cboTif)
        Me.grpTradingDefaults.Controls.Add(Me.lblTif)
        Me.grpTradingDefaults.Controls.Add(Me.nudDefaultSize)
        Me.grpTradingDefaults.Controls.Add(Me.lblDefSize)
        Me.grpTradingDefaults.Controls.Add(Me.cboAccount)
        Me.grpTradingDefaults.Controls.Add(Me.lblAccount)
        Me.grpTradingDefaults.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpTradingDefaults.Location = New System.Drawing.Point(8, 8)
        Me.grpTradingDefaults.Name = "grpTradingDefaults"
        Me.grpTradingDefaults.Padding = New System.Windows.Forms.Padding(8)
        Me.grpTradingDefaults.Size = New System.Drawing.Size(856, 136)
        Me.grpTradingDefaults.TabIndex = 0
        Me.grpTradingDefaults.TabStop = False
        Me.grpTradingDefaults.Text = "Defaults"
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.Location = New System.Drawing.Point(18, 31)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(54, 15)
        Me.lblAccount.TabIndex = 0
        Me.lblAccount.Text = "Account"
        '
        'cboAccount
        '
        Me.cboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAccount.FormattingEnabled = True
        Me.cboAccount.Location = New System.Drawing.Point(96, 27)
        Me.cboAccount.Name = "cboAccount"
        Me.cboAccount.Size = New System.Drawing.Size(180, 23)
        Me.cboAccount.TabIndex = 1
        '
        'lblDefSize
        '
        Me.lblDefSize.AutoSize = True
        Me.lblDefSize.Location = New System.Drawing.Point(18, 64)
        Me.lblDefSize.Name = "lblDefSize"
        Me.lblDefSize.Size = New System.Drawing.Size(41, 15)
        Me.lblDefSize.TabIndex = 2
        Me.lblDefSize.Text = "Size"
        '
        'nudDefaultSize
        '
        Me.nudDefaultSize.Location = New System.Drawing.Point(96, 60)
        Me.nudDefaultSize.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudDefaultSize.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudDefaultSize.Name = "nudDefaultSize"
        Me.nudDefaultSize.Size = New System.Drawing.Size(100, 23)
        Me.nudDefaultSize.TabIndex = 3
        Me.nudDefaultSize.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblTif
        '
        Me.lblTif.AutoSize = True
        Me.lblTif.Location = New System.Drawing.Point(218, 64)
        Me.lblTif.Name = "lblTif"
        Me.lblTif.Size = New System.Drawing.Size(26, 15)
        Me.lblTif.TabIndex = 4
        Me.lblTif.Text = "TIF"
        '
        'cboTif
        '
        Me.cboTif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTif.FormattingEnabled = True
        Me.cboTif.Items.AddRange(New Object() {"DAY", "IOC", "GTC", "GTD"})
        Me.cboTif.Location = New System.Drawing.Point(250, 60)
        Me.cboTif.Name = "cboTif"
        Me.cboTif.Size = New System.Drawing.Size(100, 23)
        Me.cboTif.TabIndex = 5
        '
        'chkConfirmPlace
        '
        Me.chkConfirmPlace.AutoSize = True
        Me.chkConfirmPlace.Location = New System.Drawing.Point(18, 98)
        Me.chkConfirmPlace.Name = "chkConfirmPlace"
        Me.chkConfirmPlace.Size = New System.Drawing.Size(173, 19)
        Me.chkConfirmPlace.TabIndex = 6
        Me.chkConfirmPlace.Text = "Confirm before placing"
        Me.chkConfirmPlace.UseVisualStyleBackColor = True
        '
        'chkConfirmCancel
        '
        Me.chkConfirmCancel.AutoSize = True
        Me.chkConfirmCancel.Location = New System.Drawing.Point(218, 98)
        Me.chkConfirmCancel.Name = "chkConfirmCancel"
        Me.chkConfirmCancel.Size = New System.Drawing.Size(168, 19)
        Me.chkConfirmCancel.TabIndex = 7
        Me.chkConfirmCancel.Text = "Confirm before cancel"
        Me.chkConfirmCancel.UseVisualStyleBackColor = True
        '
        'tabRisk
        '
        Me.tabRisk.Controls.Add(Me.grpRiskLimits)
        Me.tabRisk.Location = New System.Drawing.Point(4, 24)
        Me.tabRisk.Name = "tabRisk"
        Me.tabRisk.Padding = New System.Windows.Forms.Padding(8)
        Me.tabRisk.Size = New System.Drawing.Size(872, 458)
        Me.tabRisk.TabIndex = 2
        Me.tabRisk.Text = "Risk"
        Me.tabRisk.UseVisualStyleBackColor = True
        '
        'grpRiskLimits
        '
        Me.grpRiskLimits.Controls.Add(Me.chkStopOnLimit)
        Me.grpRiskLimits.Controls.Add(Me.nudPosPerSym)
        Me.grpRiskLimits.Controls.Add(Me.lblPosPerSym)
        Me.grpRiskLimits.Controls.Add(Me.nudTradeLoss)
        Me.grpRiskLimits.Controls.Add(Me.lblTradeLoss)
        Me.grpRiskLimits.Controls.Add(Me.nudDailyLoss)
        Me.grpRiskLimits.Controls.Add(Me.lblDailyLoss)
        Me.grpRiskLimits.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpRiskLimits.Location = New System.Drawing.Point(8, 8)
        Me.grpRiskLimits.Name = "grpRiskLimits"
        Me.grpRiskLimits.Padding = New System.Windows.Forms.Padding(8)
        Me.grpRiskLimits.Size = New System.Drawing.Size(856, 140)
        Me.grpRiskLimits.TabIndex = 0
        Me.grpRiskLimits.TabStop = False
        Me.grpRiskLimits.Text = "Limits"
        '
        'lblDailyLoss
        '
        Me.lblDailyLoss.AutoSize = True
        Me.lblDailyLoss.Location = New System.Drawing.Point(18, 31)
        Me.lblDailyLoss.Name = "lblDailyLoss"
        Me.lblDailyLoss.Size = New System.Drawing.Size(138, 15)
        Me.lblDailyLoss.TabIndex = 0
        Me.lblDailyLoss.Text = "Max daily loss (USD)"
        '
        'nudDailyLoss
        '
        Me.nudDailyLoss.DecimalPlaces = 2
        Me.nudDailyLoss.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudDailyLoss.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.nudDailyLoss.Name = "nudDailyLoss"
        Me.nudDailyLoss.Size = New System.Drawing.Size(120, 23)
        Me.nudDailyLoss.TabIndex = 1
        '
        'lblTradeLoss
        '
        Me.lblTradeLoss.AutoSize = True
        Me.lblTradeLoss.Location = New System.Drawing.Point(254, 31)
        Me.lblTradeLoss.Name = "lblTradeLoss"
        Me.lblTradeLoss.Size = New System.Drawing.Size(143, 15)
        Me.lblTradeLoss.TabIndex = 2
        Me.lblTradeLoss.Text = "Max trade loss (USD)"
        '
        'nudTradeLoss
        '
        Me.nudTradeLoss.DecimalPlaces = 2
        Me.nudTradeLoss.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudTradeLoss.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudTradeLoss.Name = "nudTradeLoss"
        Me.nudTradeLoss.Size = New System.Drawing.Size(120, 23)
        Me.nudTradeLoss.TabIndex = 3
        '
        'lblPosPerSym
        '
        Me.lblPosPerSym.AutoSize = True
        Me.lblPosPerSym.Location = New System.Drawing.Point(490, 31)
        Me.lblPosPerSym.Name = "lblPosPerSym"
        Me.lblPosPerSym.Size = New System.Drawing.Size(149, 15)
        Me.lblPosPerSym.TabIndex = 4
        Me.lblPosPerSym.Text = "Max position per symbol"
        '
        'nudPosPerSym
        '
        Me.nudPosPerSym.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudPosPerSym.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudPosPerSym.Name = "nudPosPerSym"
        Me.nudPosPerSym.Size = New System.Drawing.Size(100, 23)
        Me.nudPosPerSym.TabIndex = 5
        Me.nudPosPerSym.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'chkStopOnLimit
        '
        Me.chkStopOnLimit.AutoSize = True
        Me.chkStopOnLimit.Location = New System.Drawing.Point(18, 98)
        Me.chkStopOnLimit.Name = "chkStopOnLimit"
        Me.chkStopOnLimit.Size = New System.Drawing.Size(324, 19)
        Me.chkStopOnLimit.TabIndex = 6
        Me.chkStopOnLimit.Text = "Stop trading automatically upon limit breach"
        Me.chkStopOnLimit.UseVisualStyleBackColor = True
        '
        'tabMarket
        '
        Me.tabMarket.Controls.Add(Me.grpMktData)
        Me.tabMarket.Location = New System.Drawing.Point(4, 24)
        Me.tabMarket.Name = "tabMarket"
        Me.tabMarket.Padding = New System.Windows.Forms.Padding(8)
        Me.tabMarket.Size = New System.Drawing.Size(872, 458)
        Me.tabMarket.TabIndex = 3
        Me.tabMarket.Text = "Market Data"
        Me.tabMarket.UseVisualStyleBackColor = True
        '
        'grpMktData
        '
        Me.grpMktData.Controls.Add(Me.nudPacing)
        Me.grpMktData.Controls.Add(Me.lblPacing)
        Me.grpMktData.Controls.Add(Me.chkStreamQuotes)
        Me.grpMktData.Controls.Add(Me.chkRthOnly)
        Me.grpMktData.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpMktData.Location = New System.Drawing.Point(8, 8)
        Me.grpMktData.Name = "grpMktData"
        Me.grpMktData.Padding = New System.Windows.Forms.Padding(8)
        Me.grpMktData.Size = New System.Drawing.Size(856, 112)
        Me.grpMktData.TabIndex = 0
        Me.grpMktData.TabStop = False
        Me.grpMktData.Text = "Subscriptions"
        '
        'chkRthOnly
        '
        Me.chkRthOnly.AutoSize = True
        Me.chkRthOnly.Location = New System.Drawing.Point(18, 26)
        Me.chkRthOnly.Name = "chkRthOnly"
        Me.chkRthOnly.Size = New System.Drawing.Size(189, 19)
        Me.chkRthOnly.TabIndex = 0
        Me.chkRthOnly.Text = "Regular Trading Hours only"
        Me.chkRthOnly.UseVisualStyleBackColor = True
        '
        'chkStreamQuotes
        '
        Me.chkStreamQuotes.AutoSize = True
        Me.chkStreamQuotes.Location = New System.Drawing.Point(18, 51)
        Me.chkStreamQuotes.Name = "chkStreamQuotes"
        Me.chkStreamQuotes.Size = New System.Drawing.Size(237, 19)
        Me.chkStreamQuotes.TabIndex = 1
        Me.chkStreamQuotes.Text = "Use streaming quotes when available"
        Me.chkStreamQuotes.UseVisualStyleBackColor = True
        '
        'lblPacing
        '
        Me.lblPacing.AutoSize = True
        Me.lblPacing.Location = New System.Drawing.Point(18, 80)
        Me.lblPacing.Name = "lblPacing"
        Me.lblPacing.Size = New System.Drawing.Size(149, 15)
        Me.lblPacing.TabIndex = 2
        Me.lblPacing.Text = "Max reqs / second (cap)"
        '
        'nudPacing
        '
        Me.nudPacing.Location = New System.Drawing.Point(173, 76)
        Me.nudPacing.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudPacing.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudPacing.Name = "nudPacing"
        Me.nudPacing.Size = New System.Drawing.Size(80, 23)
        Me.nudPacing.TabIndex = 3
        Me.nudPacing.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'tabNotify
        '
        Me.tabNotify.Controls.Add(Me.grpNotify)
        Me.tabNotify.Location = New System.Drawing.Point(4, 24)
        Me.tabNotify.Name = "tabNotify"
        Me.tabNotify.Padding = New System.Windows.Forms.Padding(8)
        Me.tabNotify.Size = New System.Drawing.Size(872, 458)
        Me.tabNotify.TabIndex = 4
        Me.tabNotify.Text = "Notifications"
        Me.tabNotify.UseVisualStyleBackColor = True
        '
        'grpNotify
        '
        Me.grpNotify.Controls.Add(Me.txtEmail)
        Me.grpNotify.Controls.Add(Me.lblEmail)
        Me.grpNotify.Controls.Add(Me.txtSoundPath)
        Me.grpNotify.Controls.Add(Me.btnBrowseSound)
        Me.grpNotify.Controls.Add(Me.lblSound)
        Me.grpNotify.Controls.Add(Me.chkSound)
        Me.grpNotify.Controls.Add(Me.chkPopup)
        Me.grpNotify.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpNotify.Location = New System.Drawing.Point(8, 8)
        Me.grpNotify.Name = "grpNotify"
        Me.grpNotify.Padding = New System.Windows.Forms.Padding(8)
        Me.grpNotify.Size = New System.Drawing.Size(856, 128)
        Me.grpNotify.TabIndex = 0
        Me.grpNotify.TabStop = False
        Me.grpNotify.Text = "Channels"
        '
        'chkPopup
        '
        Me.chkPopup.AutoSize = True
        Me.chkPopup.Location = New System.Drawing.Point(18, 26)
        Me.chkPopup.Name = "chkPopup"
        Me.chkPopup.Size = New System.Drawing.Size(140, 19)
        Me.chkPopup.TabIndex = 0
        Me.chkPopup.Text = "Show desktop popup"
        Me.chkPopup.UseVisualStyleBackColor = True
        '
        'chkSound
        '
        Me.chkSound.AutoSize = True
        Me.chkSound.Location = New System.Drawing.Point(18, 51)
        Me.chkSound.Name = "chkSound"
        Me.chkSound.Size = New System.Drawing.Size(152, 19)
        Me.chkSound.TabIndex = 1
        Me.chkSound.Text = "Play sound on alert"
        Me.chkSound.UseVisualStyleBackColor = True
        '
        'txtSoundPath
        '
        Me.txtSoundPath.Location = New System.Drawing.Point(96, 76)
        Me.txtSoundPath.Name = "txtSoundPath"
        Me.txtSoundPath.Size = New System.Drawing.Size(560, 23)
        Me.txtSoundPath.TabIndex = 3
        '
        'btnBrowseSound
        '
        Me.btnBrowseSound.Location = New System.Drawing.Point(662, 75)
        Me.btnBrowseSound.Name = "btnBrowseSound"
        Me.btnBrowseSound.Size = New System.Drawing.Size(75, 25)
        Me.btnBrowseSound.TabIndex = 4
        Me.btnBrowseSound.Text = "Browse…"
        Me.btnBrowseSound.UseVisualStyleBackColor = True
        '
        'lblSound
        '
        Me.lblSound.AutoSize = True
        Me.lblSound.Location = New System.Drawing.Point(18, 80)
        Me.lblSound.Name = "lblSound"
        Me.lblSound.Size = New System.Drawing.Size(43, 15)
        Me.lblSound.TabIndex = 2
        Me.lblSound.Text = "Sound"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(96, 104)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(300, 23)
        Me.txtEmail.TabIndex = 6
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(18, 108)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(38, 15)
        Me.lblEmail.TabIndex = 5
        Me.lblEmail.Text = "Email"
        '
        'tabStorage
        '
        Me.tabStorage.Controls.Add(Me.grpStorage)
        Me.tabStorage.Location = New System.Drawing.Point(4, 24)
        Me.tabStorage.Name = "tabStorage"
        Me.tabStorage.Padding = New System.Windows.Forms.Padding(8)
        Me.tabStorage.Size = New System.Drawing.Size(872, 458)
        Me.tabStorage.TabIndex = 5
        Me.tabStorage.Text = "Storage & UI"
        Me.tabStorage.UseVisualStyleBackColor = True
        '
        'grpStorage
        '
        Me.grpStorage.Controls.Add(Me.btnClearCache)
        Me.grpStorage.Controls.Add(Me.txtDataFolder)
        Me.grpStorage.Controls.Add(Me.btnBrowseFolder)
        Me.grpStorage.Controls.Add(Me.lblDataFolder)
        Me.grpStorage.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpStorage.Location = New System.Drawing.Point(8, 8)
        Me.grpStorage.Name = "grpStorage"
        Me.grpStorage.Padding = New System.Windows.Forms.Padding(8)
        Me.grpStorage.Size = New System.Drawing.Size(856, 96)
        Me.grpStorage.TabIndex = 0
        Me.grpStorage.TabStop = False
        Me.grpStorage.Text = "Locations"
        '
        'txtDataFolder
        '
        Me.txtDataFolder.Location = New System.Drawing.Point(96, 27)
        Me.txtDataFolder.Name = "txtDataFolder"
        Me.txtDataFolder.Size = New System.Drawing.Size(560, 23)
        Me.txtDataFolder.TabIndex = 1
        '
        'btnBrowseFolder
        '
        Me.btnBrowseFolder.Location = New System.Drawing.Point(662, 26)
        Me.btnBrowseFolder.Name = "btnBrowseFolder"
        Me.btnBrowseFolder.Size = New System.Drawing.Size(75, 25)
        Me.btnBrowseFolder.TabIndex = 2
        Me.btnBrowseFolder.Text = "Browse…"
        Me.btnBrowseFolder.UseVisualStyleBackColor = True
        '
        'lblDataFolder
        '
        Me.lblDataFolder.AutoSize = True
        Me.lblDataFolder.Location = New System.Drawing.Point(18, 31)
        Me.lblDataFolder.Name = "lblDataFolder"
        Me.lblDataFolder.Size = New System.Drawing.Size(71, 15)
        Me.lblDataFolder.TabIndex = 0
        Me.lblDataFolder.Text = "Data folder"
        '
        'btnClearCache
        '
        Me.btnClearCache.Location = New System.Drawing.Point(96, 58)
        Me.btnClearCache.Name = "btnClearCache"
        Me.btnClearCache.Size = New System.Drawing.Size(140, 25)
        Me.btnClearCache.TabIndex = 3
        Me.btnClearCache.Text = "Clear caches"
        Me.btnClearCache.UseVisualStyleBackColor = True
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnCancel)
        Me.pnlBottom.Controls.Add(Me.btnApply)
        Me.pnlBottom.Controls.Add(Me.btnOK)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 486)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlBottom.Size = New System.Drawing.Size(880, 46)
        Me.pnlBottom.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(616, 8)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(80, 30)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApply.Location = New System.Drawing.Point(702, 8)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(80, 30)
        Me.btnApply.TabIndex = 1
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(788, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 30)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'dlgOpen
        '
        Me.dlgOpen.Filter = "Audio files|*.wav;*.mp3;*.aac;*.wma|All files|*.*"
        Me.dlgOpen.Title = "Select sound file"
        '
        'status
        '
        Me.status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblHint})
        Me.status.Location = New System.Drawing.Point(0, 532)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(880, 22)
        Me.status.TabIndex = 2
        '
        'lblHint
        '
        Me.lblHint.Name = "lblHint"
        Me.lblHint.Size = New System.Drawing.Size(229, 17)
        Me.lblHint.Text = "Changes apply on OK or Apply."
        '
        'SettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(880, 554)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.status)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(800, 550)
        Me.Name = "SettingsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.tabMain.ResumeLayout(False)
        Me.tabGeneral.ResumeLayout(False)
        Me.grpStartup.ResumeLayout(False)
        Me.grpStartup.PerformLayout()
        Me.grpAppearance.ResumeLayout(False)
        Me.grpAppearance.PerformLayout()
        Me.tabTrading.ResumeLayout(False)
        Me.grpTradingDefaults.ResumeLayout(False)
        Me.grpTradingDefaults.PerformLayout()
        CType(Me.nudDefaultSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabRisk.ResumeLayout(False)
        Me.grpRiskLimits.ResumeLayout(False)
        Me.grpRiskLimits.PerformLayout()
        CType(Me.nudDailyLoss, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudTradeLoss, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudPosPerSym, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMarket.ResumeLayout(False)
        Me.grpMktData.ResumeLayout(False)
        Me.grpMktData.PerformLayout()
        CType(Me.nudPacing, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabNotify.ResumeLayout(False)
        Me.grpNotify.ResumeLayout(False)
        Me.grpNotify.PerformLayout()
        Me.tabStorage.ResumeLayout(False)
        Me.grpStorage.ResumeLayout(False)
        Me.grpStorage.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tabMain As TabControl
    Friend WithEvents tabGeneral As TabPage
    Friend WithEvents grpStartup As GroupBox
    Friend WithEvents chkConnectStartup As CheckBox
    Friend WithEvents chkRestoreLayout As CheckBox
    Friend WithEvents grpAppearance As GroupBox
    Friend WithEvents cboLogLevel As ComboBox
    Friend WithEvents lblLogLevel As Label
    Friend WithEvents cboTheme As ComboBox
    Friend WithEvents lblTheme As Label
    Friend WithEvents tabTrading As TabPage
    Friend WithEvents grpTradingDefaults As GroupBox
    Friend WithEvents lblAccount As Label
    Friend WithEvents cboAccount As ComboBox
    Friend WithEvents lblDefSize As Label
    Friend WithEvents nudDefaultSize As NumericUpDown
    Friend WithEvents lblTif As Label
    Friend WithEvents cboTif As ComboBox
    Friend WithEvents chkConfirmPlace As CheckBox
    Friend WithEvents chkConfirmCancel As CheckBox
    Friend WithEvents tabRisk As TabPage
    Friend WithEvents grpRiskLimits As GroupBox
    Friend WithEvents lblDailyLoss As Label
    Friend WithEvents nudDailyLoss As NumericUpDown
    Friend WithEvents lblTradeLoss As Label
    Friend WithEvents nudTradeLoss As NumericUpDown
    Friend WithEvents lblPosPerSym As Label
    Friend WithEvents nudPosPerSym As NumericUpDown
    Friend WithEvents chkStopOnLimit As CheckBox
    Friend WithEvents tabMarket As TabPage
    Friend WithEvents grpMktData As GroupBox
    Friend WithEvents chkRthOnly As CheckBox
    Friend WithEvents chkStreamQuotes As CheckBox
    Friend WithEvents lblPacing As Label
    Friend WithEvents nudPacing As NumericUpDown
    Friend WithEvents tabNotify As TabPage
    Friend WithEvents grpNotify As GroupBox
    Friend WithEvents chkPopup As CheckBox
    Friend WithEvents chkSound As CheckBox
    Friend WithEvents txtSoundPath As TextBox
    Friend WithEvents btnBrowseSound As Button
    Friend WithEvents lblSound As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents tabStorage As TabPage
    Friend WithEvents grpStorage As GroupBox
    Friend WithEvents txtDataFolder As TextBox
    Friend WithEvents btnBrowseFolder As Button
    Friend WithEvents lblDataFolder As Label
    Friend WithEvents btnClearCache As Button
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents btnOK As Button
    Friend WithEvents btnApply As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents dlgFolder As FolderBrowserDialog
    Friend WithEvents dlgOpen As OpenFileDialog
    Friend WithEvents status As StatusStrip
    Friend WithEvents lblHint As ToolStripStatusLabel
End Class
