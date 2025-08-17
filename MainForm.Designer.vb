<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        ToolStrip1 = New ToolStrip()
        btnConnect = New ToolStripButton()
        btnDisconnect = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnKillSwitch = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        lblEnv = New ToolStripLabel()
        cboAccount = New ToolStripComboBox()
        ToolStripSeparator3 = New ToolStripSeparator()
        lblStatus = New ToolStripLabel()
        txtFilterSymbol = New ToolStripTextBox()
        MainSplit = New SplitContainer()
        flpKpis = New FlowLayoutPanel()
        grpNetDelta = New GroupBox()
        lblNetDelta = New Label()
        grpGamma = New GroupBox()
        lblGamma = New Label()
        grpTheta = New GroupBox()
        lblTheta = New Label()
        grpURGL = New GroupBox()
        lblURGL = New Label()
        grpRGL = New GroupBox()
        lblRGL = New Label()
        grpATR = New GroupBox()
        lblATR = New Label()
        grpEMA = New GroupBox()
        lblEMA = New Label()
        grpRefPoint = New GroupBox()
        lblRefPoint = New Label()
        grpStrikeMid = New GroupBox()
        lblStrikeMid = New Label()
        grpInsurance = New GroupBox()
        lblInsurance = New Label()
        grpHedgeCap = New GroupBox()
        lblHedgeCap = New Label()
        SplitCenterRight = New SplitContainer()
        tabMain = New TabControl()
        tabMonitor = New TabPage()
        dgvMonitor = New DataGridView()
        tabTrades = New TabPage()
        dgvTrades = New DataGridView()
        tabOrders = New TabPage()
        dgvOrders = New DataGridView()
        tabSymbols = New TabPage()
        dgvSymbols = New DataGridView()
        pnlAlerts = New Panel()
        lblAlerts = New Label()
        lstAlerts = New ListBox()
        StatusStrip1 = New StatusStrip()
        lblConn = New ToolStripStatusLabel()
        lblLatency = New ToolStripStatusLabel()
        lblDb = New ToolStripStatusLabel()
        ToolStrip1.SuspendLayout()
        CType(MainSplit, ComponentModel.ISupportInitialize).BeginInit()
        MainSplit.Panel1.SuspendLayout()
        MainSplit.Panel2.SuspendLayout()
        MainSplit.SuspendLayout()
        flpKpis.SuspendLayout()
        grpNetDelta.SuspendLayout()
        grpGamma.SuspendLayout()
        grpTheta.SuspendLayout()
        grpURGL.SuspendLayout()
        grpRGL.SuspendLayout()
        grpATR.SuspendLayout()
        grpEMA.SuspendLayout()
        grpRefPoint.SuspendLayout()
        grpStrikeMid.SuspendLayout()
        grpInsurance.SuspendLayout()
        grpHedgeCap.SuspendLayout()
        CType(SplitCenterRight, ComponentModel.ISupportInitialize).BeginInit()
        SplitCenterRight.Panel1.SuspendLayout()
        SplitCenterRight.Panel2.SuspendLayout()
        SplitCenterRight.SuspendLayout()
        tabMain.SuspendLayout()
        tabMonitor.SuspendLayout()
        CType(dgvMonitor, ComponentModel.ISupportInitialize).BeginInit()
        tabTrades.SuspendLayout()
        CType(dgvTrades, ComponentModel.ISupportInitialize).BeginInit()
        tabOrders.SuspendLayout()
        CType(dgvOrders, ComponentModel.ISupportInitialize).BeginInit()
        tabSymbols.SuspendLayout()
        CType(dgvSymbols, ComponentModel.ISupportInitialize).BeginInit()
        pnlAlerts.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.GripStyle = ToolStripGripStyle.Hidden
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnConnect, btnDisconnect, ToolStripSeparator1, btnKillSwitch, ToolStripSeparator2, lblEnv, cboAccount, ToolStripSeparator3, lblStatus, txtFilterSymbol})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1280, 25)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
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
        ' btnKillSwitch
        ' 
        btnKillSwitch.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnKillSwitch.ForeColor = Color.DarkRed
        btnKillSwitch.Name = "btnKillSwitch"
        btnKillSwitch.Size = New Size(56, 22)
        btnKillSwitch.Text = "KILL ALL"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 25)
        ' 
        ' lblEnv
        ' 
        lblEnv.Name = "lblEnv"
        lblEnv.Size = New Size(93, 22)
        lblEnv.Text = "Paper / Account"
        ' 
        ' cboAccount
        ' 
        cboAccount.DropDownStyle = ComboBoxStyle.DropDownList
        cboAccount.Name = "cboAccount"
        cboAccount.Size = New Size(121, 25)
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 25)
        ' 
        ' lblStatus
        ' 
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(79, 22)
        lblStatus.Text = "Disconnected"
        ' 
        ' txtFilterSymbol
        ' 
        txtFilterSymbol.Alignment = ToolStripItemAlignment.Right
        txtFilterSymbol.BorderStyle = BorderStyle.FixedSingle
        txtFilterSymbol.Name = "txtFilterSymbol"
        txtFilterSymbol.Size = New Size(120, 25)
        txtFilterSymbol.ToolTipText = "Filter by Symbol"
        ' 
        ' MainSplit
        ' 
        MainSplit.Dock = DockStyle.Fill
        MainSplit.Location = New Point(0, 25)
        MainSplit.Name = "MainSplit"
        ' 
        ' MainSplit.Panel1
        ' 
        MainSplit.Panel1.Controls.Add(flpKpis)
        ' 
        ' MainSplit.Panel2
        ' 
        MainSplit.Panel2.Controls.Add(SplitCenterRight)
        MainSplit.Size = New Size(1280, 695)
        MainSplit.SplitterDistance = 240
        MainSplit.TabIndex = 1
        ' 
        ' flpKpis
        ' 
        flpKpis.AutoScroll = True
        flpKpis.Controls.Add(grpNetDelta)
        flpKpis.Controls.Add(grpGamma)
        flpKpis.Controls.Add(grpTheta)
        flpKpis.Controls.Add(grpURGL)
        flpKpis.Controls.Add(grpRGL)
        flpKpis.Controls.Add(grpATR)
        flpKpis.Controls.Add(grpEMA)
        flpKpis.Controls.Add(grpRefPoint)
        flpKpis.Controls.Add(grpStrikeMid)
        flpKpis.Controls.Add(grpInsurance)
        flpKpis.Controls.Add(grpHedgeCap)
        flpKpis.Dock = DockStyle.Fill
        flpKpis.FlowDirection = FlowDirection.TopDown
        flpKpis.Location = New Point(0, 0)
        flpKpis.Name = "flpKpis"
        flpKpis.Padding = New Padding(6)
        flpKpis.Size = New Size(240, 695)
        flpKpis.TabIndex = 0
        flpKpis.WrapContents = False
        ' 
        ' grpNetDelta
        ' 
        grpNetDelta.Controls.Add(lblNetDelta)
        grpNetDelta.Location = New Point(9, 9)
        grpNetDelta.Margin = New Padding(3, 3, 3, 6)
        grpNetDelta.Name = "grpNetDelta"
        grpNetDelta.Size = New Size(220, 50)
        grpNetDelta.TabIndex = 0
        grpNetDelta.TabStop = False
        grpNetDelta.Text = "Net Δ"
        ' 
        ' lblNetDelta
        ' 
        lblNetDelta.Dock = DockStyle.Fill
        lblNetDelta.Location = New Point(3, 19)
        lblNetDelta.Name = "lblNetDelta"
        lblNetDelta.Size = New Size(214, 28)
        lblNetDelta.TabIndex = 0
        lblNetDelta.Text = "0.00"
        lblNetDelta.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpGamma
        ' 
        grpGamma.Controls.Add(lblGamma)
        grpGamma.Location = New Point(9, 68)
        grpGamma.Margin = New Padding(3, 3, 3, 6)
        grpGamma.Name = "grpGamma"
        grpGamma.Size = New Size(220, 50)
        grpGamma.TabIndex = 1
        grpGamma.TabStop = False
        grpGamma.Text = "Γ"
        ' 
        ' lblGamma
        ' 
        lblGamma.Dock = DockStyle.Fill
        lblGamma.Location = New Point(3, 19)
        lblGamma.Name = "lblGamma"
        lblGamma.Size = New Size(214, 28)
        lblGamma.TabIndex = 0
        lblGamma.Text = "0.00"
        lblGamma.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpTheta
        ' 
        grpTheta.Controls.Add(lblTheta)
        grpTheta.Location = New Point(9, 127)
        grpTheta.Margin = New Padding(3, 3, 3, 6)
        grpTheta.Name = "grpTheta"
        grpTheta.Size = New Size(220, 50)
        grpTheta.TabIndex = 2
        grpTheta.TabStop = False
        grpTheta.Text = "Θ / day"
        ' 
        ' lblTheta
        ' 
        lblTheta.Dock = DockStyle.Fill
        lblTheta.Location = New Point(3, 19)
        lblTheta.Name = "lblTheta"
        lblTheta.Size = New Size(214, 28)
        lblTheta.TabIndex = 0
        lblTheta.Text = "0.00"
        lblTheta.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpURGL
        ' 
        grpURGL.Controls.Add(lblURGL)
        grpURGL.Location = New Point(9, 186)
        grpURGL.Margin = New Padding(3, 3, 3, 6)
        grpURGL.Name = "grpURGL"
        grpURGL.Size = New Size(220, 50)
        grpURGL.TabIndex = 3
        grpURGL.TabStop = False
        grpURGL.Text = "URGL"
        ' 
        ' lblURGL
        ' 
        lblURGL.Dock = DockStyle.Fill
        lblURGL.Location = New Point(3, 19)
        lblURGL.Name = "lblURGL"
        lblURGL.Size = New Size(214, 28)
        lblURGL.TabIndex = 0
        lblURGL.Text = "$0.00"
        lblURGL.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpRGL
        ' 
        grpRGL.Controls.Add(lblRGL)
        grpRGL.Location = New Point(9, 245)
        grpRGL.Margin = New Padding(3, 3, 3, 6)
        grpRGL.Name = "grpRGL"
        grpRGL.Size = New Size(220, 50)
        grpRGL.TabIndex = 4
        grpRGL.TabStop = False
        grpRGL.Text = "RGL"
        ' 
        ' lblRGL
        ' 
        lblRGL.Dock = DockStyle.Fill
        lblRGL.Location = New Point(3, 19)
        lblRGL.Name = "lblRGL"
        lblRGL.Size = New Size(214, 28)
        lblRGL.TabIndex = 0
        lblRGL.Text = "$0.00"
        lblRGL.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpATR
        ' 
        grpATR.Controls.Add(lblATR)
        grpATR.Location = New Point(9, 304)
        grpATR.Margin = New Padding(3, 3, 3, 6)
        grpATR.Name = "grpATR"
        grpATR.Size = New Size(220, 50)
        grpATR.TabIndex = 5
        grpATR.TabStop = False
        grpATR.Text = "ATR(3)"
        ' 
        ' lblATR
        ' 
        lblATR.Dock = DockStyle.Fill
        lblATR.Location = New Point(3, 19)
        lblATR.Name = "lblATR"
        lblATR.Size = New Size(214, 28)
        lblATR.TabIndex = 0
        lblATR.Text = "0.00"
        lblATR.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpEMA
        ' 
        grpEMA.Controls.Add(lblEMA)
        grpEMA.Location = New Point(9, 363)
        grpEMA.Margin = New Padding(3, 3, 3, 6)
        grpEMA.Name = "grpEMA"
        grpEMA.Size = New Size(220, 50)
        grpEMA.TabIndex = 6
        grpEMA.TabStop = False
        grpEMA.Text = "EMA 9/21"
        ' 
        ' lblEMA
        ' 
        lblEMA.Dock = DockStyle.Fill
        lblEMA.Location = New Point(3, 19)
        lblEMA.Name = "lblEMA"
        lblEMA.Size = New Size(214, 28)
        lblEMA.TabIndex = 0
        lblEMA.Text = "9 > 21"
        lblEMA.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpRefPoint
        ' 
        grpRefPoint.Controls.Add(lblRefPoint)
        grpRefPoint.Location = New Point(9, 422)
        grpRefPoint.Margin = New Padding(3, 3, 3, 6)
        grpRefPoint.Name = "grpRefPoint"
        grpRefPoint.Size = New Size(220, 50)
        grpRefPoint.TabIndex = 7
        grpRefPoint.TabStop = False
        grpRefPoint.Text = "RefPoint"
        ' 
        ' lblRefPoint
        ' 
        lblRefPoint.Dock = DockStyle.Fill
        lblRefPoint.Location = New Point(3, 19)
        lblRefPoint.Name = "lblRefPoint"
        lblRefPoint.Size = New Size(214, 28)
        lblRefPoint.TabIndex = 0
        lblRefPoint.Text = "0.00"
        lblRefPoint.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpStrikeMid
        ' 
        grpStrikeMid.Controls.Add(lblStrikeMid)
        grpStrikeMid.Location = New Point(9, 481)
        grpStrikeMid.Margin = New Padding(3, 3, 3, 6)
        grpStrikeMid.Name = "grpStrikeMid"
        grpStrikeMid.Size = New Size(220, 50)
        grpStrikeMid.TabIndex = 8
        grpStrikeMid.TabStop = False
        grpStrikeMid.Text = "StrikeMid"
        ' 
        ' lblStrikeMid
        ' 
        lblStrikeMid.Dock = DockStyle.Fill
        lblStrikeMid.Location = New Point(3, 19)
        lblStrikeMid.Name = "lblStrikeMid"
        lblStrikeMid.Size = New Size(214, 28)
        lblStrikeMid.TabIndex = 0
        lblStrikeMid.Text = "0.00"
        lblStrikeMid.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpInsurance
        ' 
        grpInsurance.Controls.Add(lblInsurance)
        grpInsurance.Location = New Point(9, 540)
        grpInsurance.Margin = New Padding(3, 3, 3, 6)
        grpInsurance.Name = "grpInsurance"
        grpInsurance.Size = New Size(220, 50)
        grpInsurance.TabIndex = 9
        grpInsurance.TabStop = False
        grpInsurance.Text = "Insurance Ratio"
        ' 
        ' lblInsurance
        ' 
        lblInsurance.Dock = DockStyle.Fill
        lblInsurance.Location = New Point(3, 19)
        lblInsurance.Name = "lblInsurance"
        lblInsurance.Size = New Size(214, 28)
        lblInsurance.TabIndex = 0
        lblInsurance.Text = "0.20×"
        lblInsurance.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' grpHedgeCap
        ' 
        grpHedgeCap.Controls.Add(lblHedgeCap)
        grpHedgeCap.Location = New Point(9, 599)
        grpHedgeCap.Margin = New Padding(3, 3, 3, 6)
        grpHedgeCap.Name = "grpHedgeCap"
        grpHedgeCap.Size = New Size(220, 50)
        grpHedgeCap.TabIndex = 10
        grpHedgeCap.TabStop = False
        grpHedgeCap.Text = "Hedge Cap"
        ' 
        ' lblHedgeCap
        ' 
        lblHedgeCap.Dock = DockStyle.Fill
        lblHedgeCap.Location = New Point(3, 19)
        lblHedgeCap.Name = "lblHedgeCap"
        lblHedgeCap.Size = New Size(214, 28)
        lblHedgeCap.TabIndex = 0
        lblHedgeCap.Text = "80%"
        lblHedgeCap.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' SplitCenterRight
        ' 
        SplitCenterRight.Dock = DockStyle.Fill
        SplitCenterRight.Location = New Point(0, 0)
        SplitCenterRight.Name = "SplitCenterRight"
        ' 
        ' SplitCenterRight.Panel1
        ' 
        SplitCenterRight.Panel1.Controls.Add(tabMain)
        ' 
        ' SplitCenterRight.Panel2
        ' 
        SplitCenterRight.Panel2.Controls.Add(pnlAlerts)
        SplitCenterRight.Size = New Size(1036, 695)
        SplitCenterRight.SplitterDistance = 828
        SplitCenterRight.TabIndex = 0
        ' 
        ' tabMain
        ' 
        tabMain.Controls.Add(tabMonitor)
        tabMain.Controls.Add(tabTrades)
        tabMain.Controls.Add(tabOrders)
        tabMain.Controls.Add(tabSymbols)
        tabMain.Dock = DockStyle.Fill
        tabMain.Location = New Point(0, 0)
        tabMain.Name = "tabMain"
        tabMain.SelectedIndex = 0
        tabMain.Size = New Size(828, 695)
        tabMain.TabIndex = 0
        ' 
        ' tabMonitor
        ' 
        tabMonitor.Controls.Add(dgvMonitor)
        tabMonitor.Location = New Point(4, 24)
        tabMonitor.Name = "tabMonitor"
        tabMonitor.Padding = New Padding(3)
        tabMonitor.Size = New Size(820, 667)
        tabMonitor.TabIndex = 0
        tabMonitor.Text = "Monitor"
        tabMonitor.UseVisualStyleBackColor = True
        ' 
        ' dgvMonitor
        ' 
        dgvMonitor.AllowUserToAddRows = False
        dgvMonitor.AllowUserToDeleteRows = False
        dgvMonitor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        dgvMonitor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMonitor.Dock = DockStyle.Fill
        dgvMonitor.Location = New Point(3, 3)
        dgvMonitor.MultiSelect = False
        dgvMonitor.Name = "dgvMonitor"
        dgvMonitor.ReadOnly = True
        dgvMonitor.RowHeadersVisible = False
        dgvMonitor.Size = New Size(814, 661)
        dgvMonitor.TabIndex = 0
        ' 
        ' tabTrades
        ' 
        tabTrades.Controls.Add(dgvTrades)
        tabTrades.Location = New Point(4, 24)
        tabTrades.Name = "tabTrades"
        tabTrades.Padding = New Padding(3)
        tabTrades.Size = New Size(820, 667)
        tabTrades.TabIndex = 1
        tabTrades.Text = "Trades"
        tabTrades.UseVisualStyleBackColor = True
        ' 
        ' dgvTrades
        ' 
        dgvTrades.AllowUserToAddRows = False
        dgvTrades.AllowUserToDeleteRows = False
        dgvTrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        dgvTrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTrades.Dock = DockStyle.Fill
        dgvTrades.Location = New Point(3, 3)
        dgvTrades.MultiSelect = False
        dgvTrades.Name = "dgvTrades"
        dgvTrades.ReadOnly = True
        dgvTrades.RowHeadersVisible = False
        dgvTrades.Size = New Size(814, 661)
        dgvTrades.TabIndex = 0
        ' 
        ' tabOrders
        ' 
        tabOrders.Controls.Add(dgvOrders)
        tabOrders.Location = New Point(4, 24)
        tabOrders.Name = "tabOrders"
        tabOrders.Padding = New Padding(3)
        tabOrders.Size = New Size(820, 667)
        tabOrders.TabIndex = 2
        tabOrders.Text = "Orders"
        tabOrders.UseVisualStyleBackColor = True
        ' 
        ' dgvOrders
        ' 
        dgvOrders.AllowUserToAddRows = False
        dgvOrders.AllowUserToDeleteRows = False
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvOrders.Dock = DockStyle.Fill
        dgvOrders.Location = New Point(3, 3)
        dgvOrders.MultiSelect = False
        dgvOrders.Name = "dgvOrders"
        dgvOrders.ReadOnly = True
        dgvOrders.RowHeadersVisible = False
        dgvOrders.Size = New Size(814, 661)
        dgvOrders.TabIndex = 0
        ' 
        ' tabSymbols
        ' 
        tabSymbols.Controls.Add(dgvSymbols)
        tabSymbols.Location = New Point(4, 24)
        tabSymbols.Name = "tabSymbols"
        tabSymbols.Padding = New Padding(3)
        tabSymbols.Size = New Size(820, 667)
        tabSymbols.TabIndex = 3
        tabSymbols.Text = "Symbols"
        tabSymbols.UseVisualStyleBackColor = True
        ' 
        ' dgvSymbols
        ' 
        dgvSymbols.AllowUserToAddRows = False
        dgvSymbols.AllowUserToDeleteRows = False
        dgvSymbols.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        dgvSymbols.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSymbols.Dock = DockStyle.Fill
        dgvSymbols.Location = New Point(3, 3)
        dgvSymbols.MultiSelect = False
        dgvSymbols.Name = "dgvSymbols"
        dgvSymbols.ReadOnly = True
        dgvSymbols.RowHeadersVisible = False
        dgvSymbols.Size = New Size(814, 661)
        dgvSymbols.TabIndex = 0
        ' 
        ' pnlAlerts
        ' 
        pnlAlerts.BorderStyle = BorderStyle.FixedSingle
        pnlAlerts.Controls.Add(lblAlerts)
        pnlAlerts.Controls.Add(lstAlerts)
        pnlAlerts.Dock = DockStyle.Fill
        pnlAlerts.Location = New Point(0, 0)
        pnlAlerts.Name = "pnlAlerts"
        pnlAlerts.Size = New Size(204, 695)
        pnlAlerts.TabIndex = 0
        ' 
        ' lblAlerts
        ' 
        lblAlerts.Dock = DockStyle.Top
        lblAlerts.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblAlerts.Location = New Point(0, 0)
        lblAlerts.Name = "lblAlerts"
        lblAlerts.Padding = New Padding(6)
        lblAlerts.Size = New Size(202, 28)
        lblAlerts.TabIndex = 1
        lblAlerts.Text = "Alerts & Activity"
        ' 
        ' lstAlerts
        ' 
        lstAlerts.Dock = DockStyle.Fill
        lstAlerts.FormattingEnabled = True
        lstAlerts.IntegralHeight = False
        lstAlerts.Location = New Point(0, 0)
        lstAlerts.Name = "lstAlerts"
        lstAlerts.Size = New Size(202, 693)
        lstAlerts.TabIndex = 0
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblConn, lblLatency, lblDb})
        StatusStrip1.Location = New Point(0, 720)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(1280, 22)
        StatusStrip1.TabIndex = 2
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblConn
        ' 
        lblConn.Name = "lblConn"
        lblConn.Size = New Size(108, 17)
        lblConn.Text = "TWS: Disconnected"
        ' 
        ' lblLatency
        ' 
        lblLatency.Name = "lblLatency"
        lblLatency.Size = New Size(59, 17)
        lblLatency.Text = "Latency: -"
        ' 
        ' lblDb
        ' 
        lblDb.Name = "lblDb"
        lblDb.Size = New Size(79, 17)
        lblDb.Text = "DB: Unknown"
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1280, 742)
        Controls.Add(MainSplit)
        Controls.Add(ToolStrip1)
        Controls.Add(StatusStrip1)
        MinimumSize = New Size(1100, 700)
        Name = "MainForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "GreekTrader — Gamma-Neutral Straddle Cockpit"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        MainSplit.Panel1.ResumeLayout(False)
        MainSplit.Panel2.ResumeLayout(False)
        CType(MainSplit, ComponentModel.ISupportInitialize).EndInit()
        MainSplit.ResumeLayout(False)
        flpKpis.ResumeLayout(False)
        grpNetDelta.ResumeLayout(False)
        grpGamma.ResumeLayout(False)
        grpTheta.ResumeLayout(False)
        grpURGL.ResumeLayout(False)
        grpRGL.ResumeLayout(False)
        grpATR.ResumeLayout(False)
        grpEMA.ResumeLayout(False)
        grpRefPoint.ResumeLayout(False)
        grpStrikeMid.ResumeLayout(False)
        grpInsurance.ResumeLayout(False)
        grpHedgeCap.ResumeLayout(False)
        SplitCenterRight.Panel1.ResumeLayout(False)
        SplitCenterRight.Panel2.ResumeLayout(False)
        CType(SplitCenterRight, ComponentModel.ISupportInitialize).EndInit()
        SplitCenterRight.ResumeLayout(False)
        tabMain.ResumeLayout(False)
        tabMonitor.ResumeLayout(False)
        CType(dgvMonitor, ComponentModel.ISupportInitialize).EndInit()
        tabTrades.ResumeLayout(False)
        CType(dgvTrades, ComponentModel.ISupportInitialize).EndInit()
        tabOrders.ResumeLayout(False)
        CType(dgvOrders, ComponentModel.ISupportInitialize).EndInit()
        tabSymbols.ResumeLayout(False)
        CType(dgvSymbols, ComponentModel.ISupportInitialize).EndInit()
        pnlAlerts.ResumeLayout(False)
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents btnConnect As ToolStripButton
    Friend WithEvents btnDisconnect As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnKillSwitch As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents lblEnv As ToolStripLabel
    Friend WithEvents cboAccount As ToolStripComboBox
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents lblStatus As ToolStripLabel
    Friend WithEvents txtFilterSymbol As ToolStripTextBox
    Friend WithEvents MainSplit As SplitContainer
    Friend WithEvents flpKpis As FlowLayoutPanel
    Friend WithEvents grpNetDelta As GroupBox
    Friend WithEvents lblNetDelta As Label
    Friend WithEvents grpGamma As GroupBox
    Friend WithEvents lblGamma As Label
    Friend WithEvents grpTheta As GroupBox
    Friend WithEvents lblTheta As Label
    Friend WithEvents grpURGL As GroupBox
    Friend WithEvents lblURGL As Label
    Friend WithEvents grpRGL As GroupBox
    Friend WithEvents lblRGL As Label
    Friend WithEvents grpATR As GroupBox
    Friend WithEvents lblATR As Label
    Friend WithEvents grpEMA As GroupBox
    Friend WithEvents lblEMA As Label
    Friend WithEvents grpRefPoint As GroupBox
    Friend WithEvents lblRefPoint As Label
    Friend WithEvents grpStrikeMid As GroupBox
    Friend WithEvents lblStrikeMid As Label
    Friend WithEvents grpInsurance As GroupBox
    Friend WithEvents lblInsurance As Label
    Friend WithEvents grpHedgeCap As GroupBox
    Friend WithEvents lblHedgeCap As Label
    Friend WithEvents SplitCenterRight As SplitContainer
    Friend WithEvents tabMain As TabControl
    Friend WithEvents tabMonitor As TabPage
    Friend WithEvents dgvMonitor As DataGridView
    Friend WithEvents tabTrades As TabPage
    Friend WithEvents dgvTrades As DataGridView
    Friend WithEvents tabOrders As TabPage
    Friend WithEvents dgvOrders As DataGridView
    Friend WithEvents tabSymbols As TabPage
    Friend WithEvents dgvSymbols As DataGridView
    Friend WithEvents pnlAlerts As Panel
    Friend WithEvents lblAlerts As Label
    Friend WithEvents lstAlerts As ListBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblConn As ToolStripStatusLabel
    Friend WithEvents lblLatency As ToolStripStatusLabel
    Friend WithEvents lblDb As ToolStripStatusLabel
End Class
