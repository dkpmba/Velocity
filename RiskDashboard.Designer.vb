<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RiskDashboard
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
        Me.kpiPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.grpNetDelta = New System.Windows.Forms.GroupBox()
        Me.lblNetDelta = New System.Windows.Forms.Label()
        Me.grpGamma = New System.Windows.Forms.GroupBox()
        Me.lblGamma = New System.Windows.Forms.Label()
        Me.grpTheta = New System.Windows.Forms.GroupBox()
        Me.lblTheta = New System.Windows.Forms.Label()
        Me.grpVega = New System.Windows.Forms.GroupBox()
        Me.lblVega = New System.Windows.Forms.Label()
        Me.grpMarginUsed = New System.Windows.Forms.GroupBox()
        Me.lblMarginUsed = New System.Windows.Forms.Label()
        Me.grpMarginFree = New System.Windows.Forms.GroupBox()
        Me.lblMarginFree = New System.Windows.Forms.Label()
        Me.grpDailyPnL = New System.Windows.Forms.GroupBox()
        Me.lblDailyPnL = New System.Windows.Forms.Label()
        Me.grpMaxDD = New System.Windows.Forms.GroupBox()
        Me.lblMaxDD = New System.Windows.Forms.Label()
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.tabTables = New System.Windows.Forms.TabControl()
        Me.tabExposureSymbol = New System.Windows.Forms.TabPage()
        Me.dgvExposureBySymbol = New System.Windows.Forms.DataGridView()
        Me.tabExposureExpiry = New System.Windows.Forms.TabPage()
        Me.dgvExposureByExpiry = New System.Windows.Forms.DataGridView()
        Me.tabLimits = New System.Windows.Forms.TabPage()
        Me.dgvLimits = New System.Windows.Forms.DataGridView()
        Me.tabCharts = New System.Windows.Forms.TabControl()
        Me.tabExposureChart = New System.Windows.Forms.TabPage()
        Me.chartExposure = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.tabBuckets = New System.Windows.Forms.TabPage()
        Me.chartBuckets = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.tabConcentration = New System.Windows.Forms.TabPage()
        Me.chartConcentration = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.actionPanel = New System.Windows.Forms.Panel()
        Me.btnOpenRiskSettings = New System.Windows.Forms.Button()
        Me.btnCloseHighestRisk = New System.Windows.Forms.Button()
        Me.btnFlattenAll = New System.Windows.Forms.Button()
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.lblSummary = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.kpiPanel.SuspendLayout()
        Me.grpNetDelta.SuspendLayout()
        Me.grpGamma.SuspendLayout()
        Me.grpTheta.SuspendLayout()
        Me.grpVega.SuspendLayout()
        Me.grpMarginUsed.SuspendLayout()
        Me.grpMarginFree.SuspendLayout()
        Me.grpDailyPnL.SuspendLayout()
        Me.grpMaxDD.SuspendLayout()
        Me.tabTables.SuspendLayout()
        Me.tabExposureSymbol.SuspendLayout()
        CType(Me.dgvExposureBySymbol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabExposureExpiry.SuspendLayout()
        CType(Me.dgvExposureByExpiry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLimits.SuspendLayout()
        CType(Me.dgvLimits, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabCharts.SuspendLayout()
        Me.tabExposureChart.SuspendLayout()
        CType(Me.chartExposure, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabBuckets.SuspendLayout()
        CType(Me.chartBuckets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabConcentration.SuspendLayout()
        CType(Me.chartConcentration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.actionPanel.SuspendLayout()
        Me.status.SuspendLayout()
        Me.SuspendLayout()
        '
        'kpiPanel
        '
        Me.kpiPanel.AutoScroll = True
        Me.kpiPanel.Controls.Add(Me.grpNetDelta)
        Me.kpiPanel.Controls.Add(Me.grpGamma)
        Me.kpiPanel.Controls.Add(Me.grpTheta)
        Me.kpiPanel.Controls.Add(Me.grpVega)
        Me.kpiPanel.Controls.Add(Me.grpMarginUsed)
        Me.kpiPanel.Controls.Add(Me.grpMarginFree)
        Me.kpiPanel.Controls.Add(Me.grpDailyPnL)
        Me.kpiPanel.Controls.Add(Me.grpMaxDD)
        Me.kpiPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.kpiPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight
        Me.kpiPanel.Location = New System.Drawing.Point(0, 0)
        Me.kpiPanel.Name = "kpiPanel"
        Me.kpiPanel.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.kpiPanel.Size = New System.Drawing.Size(1200, 72)
        Me.kpiPanel.TabIndex = 0
        Me.kpiPanel.WrapContents = False
        '
        'grpNetDelta
        '
        Me.grpNetDelta.Controls.Add(Me.lblNetDelta)
        Me.grpNetDelta.Location = New System.Drawing.Point(11, 9)
        Me.grpNetDelta.Margin = New System.Windows.Forms.Padding(3, 3, 6, 3)
        Me.grpNetDelta.Name = "grpNetDelta"
        Me.grpNetDelta.Size = New System.Drawing.Size(130, 54)
        Me.grpNetDelta.TabIndex = 0
        Me.grpNetDelta.TabStop = False
        Me.grpNetDelta.Text = "Net Δ"
        '
        'lblNetDelta
        '
        Me.lblNetDelta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNetDelta.Location = New System.Drawing.Point(3, 19)
        Me.lblNetDelta.Name = "lblNetDelta"
        Me.lblNetDelta.Size = New System.Drawing.Size(124, 32)
        Me.lblNetDelta.TabIndex = 0
        Me.lblNetDelta.Text = "0.0"
        Me.lblNetDelta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpGamma
        '
        Me.grpGamma.Controls.Add(Me.lblGamma)
        Me.grpGamma.Location = New System.Drawing.Point(150, 9)
        Me.grpGamma.Margin = New System.Windows.Forms.Padding(3, 3, 6, 3)
        Me.grpGamma.Name = "grpGamma"
        Me.grpGamma.Size = New System.Drawing.Size(130, 54)
        Me.grpGamma.TabIndex = 1
        Me.grpGamma.TabStop = False
        Me.grpGamma.Text = "Γ"
        '
        'lblGamma
        '
        Me.lblGamma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblGamma.Location = New System.Drawing.Point(3, 19)
        Me.lblGamma.Name = "lblGamma"
        Me.lblGamma.Size = New System.Drawing.Size(124, 32)
        Me.lblGamma.TabIndex = 0
        Me.lblGamma.Text = "0.0"
        Me.lblGamma.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpTheta
        '
        Me.grpTheta.Controls.Add(Me.lblTheta)
        Me.grpTheta.Location = New System.Drawing.Point(289, 9)
        Me.grpTheta.Margin = New System.Windows.Forms.Padding(3, 3, 6, 3)
        Me.grpTheta.Name = "grpTheta"
        Me.grpTheta.Size = New System.Drawing.Size(130, 54)
        Me.grpTheta.TabIndex = 2
        Me.grpTheta.TabStop = False
        Me.grpTheta.Text = "Θ / day"
        '
        'lblTheta
        '
        Me.lblTheta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTheta.Location = New System.Drawing.Point(3, 19)
        Me.lblTheta.Name = "lblTheta"
        Me.lblTheta.Size = New System.Drawing.Size(124, 32)
        Me.lblTheta.TabIndex = 0
        Me.lblTheta.Text = "0.0"
        Me.lblTheta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpVega
        '
        Me.grpVega.Controls.Add(Me.lblVega)
        Me.grpVega.Location = New System.Drawing.Point(428, 9)
        Me.grpVega.Margin = New System.Windows.Forms.Padding(3, 3, 6, 3)
        Me.grpVega.Name = "grpVega"
        Me.grpVega.Size = New System.Drawing.Size(130, 54)
        Me.grpVega.TabIndex = 3
        Me.grpVega.TabStop = False
        Me.grpVega.Text = "V"
        '
        'lblVega
        '
        Me.lblVega.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblVega.Location = New System.Drawing.Point(3, 19)
        Me.lblVega.Name = "lblVega"
        Me.lblVega.Size = New System.Drawing.Size(124, 32)
        Me.lblVega.TabIndex = 0
        Me.lblVega.Text = "0.0"
        Me.lblVega.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpMarginUsed
        '
        Me.grpMarginUsed.Controls.Add(Me.lblMarginUsed)
        Me.grpMarginUsed.Location = New System.Drawing.Point(567, 9)
        Me.grpMarginUsed.Margin = New System.Windows.Forms.Padding(3, 3, 6, 3)
        Me.grpMarginUsed.Name = "grpMarginUsed"
        Me.grpMarginUsed.Size = New System.Drawing.Size(150, 54)
        Me.grpMarginUsed.TabIndex = 4
        Me.grpMarginUsed.TabStop = False
        Me.grpMarginUsed.Text = "Margin Used"
        '
        'lblMarginUsed
        '
        Me.lblMarginUsed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMarginUsed.Location = New System.Drawing.Point(3, 19)
        Me.lblMarginUsed.Name = "lblMarginUsed"
        Me.lblMarginUsed.Size = New System.Drawing.Size(144, 32)
        Me.lblMarginUsed.TabIndex = 0
        Me.lblMarginUsed.Text = "$0"
        Me.lblMarginUsed.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpMarginFree
        '
        Me.grpMarginFree.Controls.Add(Me.lblMarginFree)
        Me.grpMarginFree.Location = New System.Drawing.Point(726, 9)
        Me.grpMarginFree.Margin = New System.Windows.Forms.Padding(3, 3, 6, 3)
        Me.grpMarginFree.Name = "grpMarginFree"
        Me.grpMarginFree.Size = New System.Drawing.Size(150, 54)
        Me.grpMarginFree.TabIndex = 5
        Me.grpMarginFree.TabStop = False
        Me.grpMarginFree.Text = "Margin Free"
        '
        'lblMarginFree
        '
        Me.lblMarginFree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMarginFree.Location = New System.Drawing.Point(3, 19)
        Me.lblMarginFree.Name = "lblMarginFree"
        Me.lblMarginFree.Size = New System.Drawing.Size(144, 32)
        Me.lblMarginFree.TabIndex = 0
        Me.lblMarginFree.Text = "$0"
        Me.lblMarginFree.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpDailyPnL
        '
        Me.grpDailyPnL.Controls.Add(Me.lblDailyPnL)
        Me.grpDailyPnL.Location = New System.Drawing.Point(885, 9)
        Me.grpDailyPnL.Margin = New System.Windows.Forms.Padding(3, 3, 6, 3)
        Me.grpDailyPnL.Name = "grpDailyPnL"
        Me.grpDailyPnL.Size = New System.Drawing.Size(150, 54)
        Me.grpDailyPnL.TabIndex = 6
        Me.grpDailyPnL.TabStop = False
        Me.grpDailyPnL.Text = "Daily PnL"
        '
        'lblDailyPnL
        '
        Me.lblDailyPnL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDailyPnL.Location = New System.Drawing.Point(3, 19)
        Me.lblDailyPnL.Name = "lblDailyPnL"
        Me.lblDailyPnL.Size = New System.Drawing.Size(144, 32)
        Me.lblDailyPnL.TabIndex = 0
        Me.lblDailyPnL.Text = "$0"
        Me.lblDailyPnL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpMaxDD
        '
        Me.grpMaxDD.Controls.Add(Me.lblMaxDD)
        Me.grpMaxDD.Location = New System.Drawing.Point(1044, 9)
        Me.grpMaxDD.Margin = New System.Windows.Forms.Padding(3, 3, 6, 3)
        Me.grpMaxDD.Name = "grpMaxDD"
        Me.grpMaxDD.Size = New System.Drawing.Size(150, 54)
        Me.grpMaxDD.TabIndex = 7
        Me.grpMaxDD.TabStop = False
        Me.grpMaxDD.Text = "Max Drawdown"
        '
        'lblMaxDD
        '
        Me.lblMaxDD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMaxDD.Location = New System.Drawing.Point(3, 19)
        Me.lblMaxDD.Name = "lblMaxDD"
        Me.lblMaxDD.Size = New System.Drawing.Size(144, 32)
        Me.lblMaxDD.TabIndex = 0
        Me.lblMaxDD.Text = "$0"
        Me.lblMaxDD.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'splitMain
        '
        Me.splitMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMain.Location = New System.Drawing.Point(0, 72)
        Me.splitMain.Name = "splitMain"
        '
        'splitMain.Panel1
        '
        Me.splitMain.Panel1.Controls.Add(Me.tabTables)
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.tabCharts)
        Me.splitMain.Size = New System.Drawing.Size(1200, 508)
        Me.splitMain.SplitterDistance = 550
        Me.splitMain.TabIndex = 1
        '
        'tabTables
        '
        Me.tabTables.Controls.Add(Me.tabExposureSymbol)
        Me.tabTables.Controls.Add(Me.tabExposureExpiry)
        Me.tabTables.Controls.Add(Me.tabLimits)
        Me.tabTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabTables.Location = New System.Drawing.Point(0, 0)
        Me.tabTables.Name = "tabTables"
        Me.tabTables.SelectedIndex = 0
        Me.tabTables.Size = New System.Drawing.Size(550, 508)
        Me.tabTables.TabIndex = 0
        '
        'tabExposureSymbol
        '
        Me.tabExposureSymbol.Controls.Add(Me.dgvExposureBySymbol)
        Me.tabExposureSymbol.Location = New System.Drawing.Point(4, 24)
        Me.tabExposureSymbol.Name = "tabExposureSymbol"
        Me.tabExposureSymbol.Padding = New System.Windows.Forms.Padding(3)
        Me.tabExposureSymbol.Size = New System.Drawing.Size(542, 480)
        Me.tabExposureSymbol.TabIndex = 0
        Me.tabExposureSymbol.Text = "Exposure — Symbol"
        Me.tabExposureSymbol.UseVisualStyleBackColor = True
        '
        'dgvExposureBySymbol
        '
        Me.dgvExposureBySymbol.AllowUserToAddRows = False
        Me.dgvExposureBySymbol.AllowUserToDeleteRows = False
        Me.dgvExposureBySymbol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvExposureBySymbol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExposureBySymbol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvExposureBySymbol.Location = New System.Drawing.Point(3, 3)
        Me.dgvExposureBySymbol.MultiSelect = False
        Me.dgvExposureBySymbol.Name = "dgvExposureBySymbol"
        Me.dgvExposureBySymbol.ReadOnly = True
        Me.dgvExposureBySymbol.RowHeadersVisible = False
        Me.dgvExposureBySymbol.Size = New System.Drawing.Size(536, 474)
        Me.dgvExposureBySymbol.TabIndex = 0
        '
        'tabExposureExpiry
        '
        Me.tabExposureExpiry.Controls.Add(Me.dgvExposureByExpiry)
        Me.tabExposureExpiry.Location = New System.Drawing.Point(4, 24)
        Me.tabExposureExpiry.Name = "tabExposureExpiry"
        Me.tabExposureExpiry.Padding = New System.Windows.Forms.Padding(3)
        Me.tabExposureExpiry.Size = New System.Drawing.Size(542, 480)
        Me.tabExposureExpiry.TabIndex = 1
        Me.tabExposureExpiry.Text = "Exposure — Expiry"
        Me.tabExposureExpiry.UseVisualStyleBackColor = True
        '
        'dgvExposureByExpiry
        '
        Me.dgvExposureByExpiry.AllowUserToAddRows = False
        Me.dgvExposureByExpiry.AllowUserToDeleteRows = False
        Me.dgvExposureByExpiry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvExposureByExpiry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExposureByExpiry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvExposureByExpiry.Location = New System.Drawing.Point(3, 3)
        Me.dgvExposureByExpiry.MultiSelect = False
        Me.dgvExposureByExpiry.Name = "dgvExposureByExpiry"
        Me.dgvExposureByExpiry.ReadOnly = True
        Me.dgvExposureByExpiry.RowHeadersVisible = False
        Me.dgvExposureByExpiry.Size = New System.Drawing.Size(536, 474)
        Me.dgvExposureByExpiry.TabIndex = 0
        '
        'tabLimits
        '
        Me.tabLimits.Controls.Add(Me.dgvLimits)
        Me.tabLimits.Location = New System.Drawing.Point(4, 24)
        Me.tabLimits.Name = "tabLimits"
        Me.tabLimits.Padding = New System.Windows.Forms.Padding(3)
        Me.tabLimits.Size = New System.Drawing.Size(542, 480)
        Me.tabLimits.TabIndex = 2
        Me.tabLimits.Text = "Limits"
        Me.tabLimits.UseVisualStyleBackColor = True
        '
        'dgvLimits
        '
        Me.dgvLimits.AllowUserToAddRows = False
        Me.dgvLimits.AllowUserToDeleteRows = False
        Me.dgvLimits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvLimits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLimits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLimits.Location = New System.Drawing.Point(3, 3)
        Me.dgvLimits.MultiSelect = False
        Me.dgvLimits.Name = "dgvLimits"
        Me.dgvLimits.ReadOnly = True
        Me.dgvLimits.RowHeadersVisible = False
        Me.dgvLimits.Size = New System.Drawing.Size(536, 474)
        Me.dgvLimits.TabIndex = 0
        '
        'tabCharts
        '
        Me.tabCharts.Controls.Add(Me.tabExposureChart)
        Me.tabCharts.Controls.Add(Me.tabBuckets)
        Me.tabCharts.Controls.Add(Me.tabConcentration)
        Me.tabCharts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabCharts.Location = New System.Drawing.Point(0, 0)
        Me.tabCharts.Name = "tabCharts"
        Me.tabCharts.SelectedIndex = 0
        Me.tabCharts.Size = New System.Drawing.Size(646, 508)
        Me.tabCharts.TabIndex = 0
        '
        'tabExposureChart
        '
        Me.tabExposureChart.Controls.Add(Me.chartExposure)
        Me.tabExposureChart.Location = New System.Drawing.Point(4, 24)
        Me.tabExposureChart.Name = "tabExposureChart"
        Me.tabExposureChart.Padding = New System.Windows.Forms.Padding(3)
        Me.tabExposureChart.Size = New System.Drawing.Size(638, 480)
        Me.tabExposureChart.TabIndex = 0
        Me.tabExposureChart.Text = "Exposure"
        Me.tabExposureChart.UseVisualStyleBackColor = True
        '
        'chartExposure
        '
        Me.chartExposure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartExposure.Location = New System.Drawing.Point(3, 3)
        Me.chartExposure.Name = "chartExposure"
        Me.chartExposure.Size = New System.Drawing.Size(632, 474)
        Me.chartExposure.TabIndex = 0
        Me.chartExposure.Text = "Chart1"
        '
        'tabBuckets
        '
        Me.tabBuckets.Controls.Add(Me.chartBuckets)
        Me.tabBuckets.Location = New System.Drawing.Point(4, 24)
        Me.tabBuckets.Name = "tabBuckets"
        Me.tabBuckets.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBuckets.Size = New System.Drawing.Size(638, 480)
        Me.tabBuckets.TabIndex = 1
        Me.tabBuckets.Text = "Greek Buckets"
        Me.tabBuckets.UseVisualStyleBackColor = True
        '
        'chartBuckets
        '
        Me.chartBuckets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartBuckets.Location = New System.Drawing.Point(3, 3)
        Me.chartBuckets.Name = "chartBuckets"
        Me.chartBuckets.Size = New System.Drawing.Size(632, 474)
        Me.chartBuckets.TabIndex = 0
        Me.chartBuckets.Text = "Chart2"
        '
        'tabConcentration
        '
        Me.tabConcentration.Controls.Add(Me.chartConcentration)
        Me.tabConcentration.Location = New System.Drawing.Point(4, 24)
        Me.tabConcentration.Name = "tabConcentration"
        Me.tabConcentration.Padding = New System.Windows.Forms.Padding(3)
        Me.tabConcentration.Size = New System.Drawing.Size(638, 480)
        Me.tabConcentration.TabIndex = 2
        Me.tabConcentration.Text = "Concentration"
        Me.tabConcentration.UseVisualStyleBackColor = True
        '
        'chartConcentration
        '
        Me.chartConcentration.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartConcentration.Location = New System.Drawing.Point(3, 3)
        Me.chartConcentration.Name = "chartConcentration"
        Me.chartConcentration.Size = New System.Drawing.Size(632, 474)
        Me.chartConcentration.TabIndex = 0
        Me.chartConcentration.Text = "Chart3"
        '
        'actionPanel
        '
        Me.actionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.actionPanel.Controls.Add(Me.btnOpenRiskSettings)
        Me.actionPanel.Controls.Add(Me.btnCloseHighestRisk)
        Me.actionPanel.Controls.Add(Me.btnFlattenAll)
        Me.actionPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.actionPanel.Location = New System.Drawing.Point(0, 580)
        Me.actionPanel.Name = "actionPanel"
        Me.actionPanel.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.actionPanel.Size = New System.Drawing.Size(1200, 40)
        Me.actionPanel.TabIndex = 2
        '
        'btnOpenRiskSettings
        '
        Me.btnOpenRiskSettings.Location = New System.Drawing.Point(12, 6)
        Me.btnOpenRiskSettings.Name = "btnOpenRiskSettings"
        Me.btnOpenRiskSettings.Size = New System.Drawing.Size(140, 26)
        Me.btnOpenRiskSettings.TabIndex = 0
        Me.btnOpenRiskSettings.Text = "Open Risk Settings"
        Me.btnOpenRiskSettings.UseVisualStyleBackColor = True
        '
        'btnCloseHighestRisk
        '
        Me.btnCloseHighestRisk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloseHighestRisk.Location = New System.Drawing.Point(884, 6)
        Me.btnCloseHighestRisk.Name = "btnCloseHighestRisk"
        Me.btnCloseHighestRisk.Size = New System.Drawing.Size(150, 26)
        Me.btnCloseHighestRisk.TabIndex = 1
        Me.btnCloseHighestRisk.Text = "Close Highest Risk"
        Me.btnCloseHighestRisk.UseVisualStyleBackColor = True
        '
        'btnFlattenAll
        '
        Me.btnFlattenAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFlattenAll.Location = New System.Drawing.Point(1040, 6)
        Me.btnFlattenAll.Name = "btnFlattenAll"
        Me.btnFlattenAll.Size = New System.Drawing.Size(148, 26)
        Me.btnFlattenAll.TabIndex = 2
        Me.btnFlattenAll.Text = "Flatten All (Confirm)"
        Me.btnFlattenAll.UseVisualStyleBackColor = True
        '
        'status
        '
        Me.status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblSummary})
        Me.status.Location = New System.Drawing.Point(0, 620)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(1200, 22)
        Me.status.TabIndex = 3
        Me.status.Text = "StatusStrip1"
        '
        'lblSummary
        '
        Me.lblSummary.Name = "lblSummary"
        Me.lblSummary.Size = New System.Drawing.Size(226, 17)
        Me.lblSummary.Text = "Exposure rows: 0 • Limits near breach: 0"
        '
        'RiskDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 642)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.kpiPanel)
        Me.Controls.Add(Me.actionPanel)
        Me.Controls.Add(Me.status)
        Me.MinimumSize = New System.Drawing.Size(1100, 600)
        Me.Name = "RiskDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Risk Dashboard"
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.kpiPanel.ResumeLayout(False)
        Me.grpNetDelta.ResumeLayout(False)
        Me.grpGamma.ResumeLayout(False)
        Me.grpTheta.ResumeLayout(False)
        Me.grpVega.ResumeLayout(False)
        Me.grpMarginUsed.ResumeLayout(False)
        Me.grpMarginFree.ResumeLayout(False)
        Me.grpDailyPnL.ResumeLayout(False)
        Me.grpMaxDD.ResumeLayout(False)
        Me.tabTables.ResumeLayout(False)
        Me.tabExposureSymbol.ResumeLayout(False)
        CType(Me.dgvExposureBySymbol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabExposureExpiry.ResumeLayout(False)
        CType(Me.dgvExposureByExpiry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLimits.ResumeLayout(False)
        CType(Me.dgvLimits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabCharts.ResumeLayout(False)
        Me.tabExposureChart.ResumeLayout(False)
        CType(Me.chartExposure, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabBuckets.ResumeLayout(False)
        CType(Me.chartBuckets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabConcentration.ResumeLayout(False)
        CType(Me.chartConcentration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.actionPanel.ResumeLayout(False)
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents kpiPanel As FlowLayoutPanel
    Friend WithEvents grpNetDelta As GroupBox
    Friend WithEvents lblNetDelta As Label
    Friend WithEvents grpGamma As GroupBox
    Friend WithEvents lblGamma As Label
    Friend WithEvents grpTheta As GroupBox
    Friend WithEvents lblTheta As Label
    Friend WithEvents grpVega As GroupBox
    Friend WithEvents lblVega As Label
    Friend WithEvents grpMarginUsed As GroupBox
    Friend WithEvents lblMarginUsed As Label
    Friend WithEvents grpMarginFree As GroupBox
    Friend WithEvents lblMarginFree As Label
    Friend WithEvents grpDailyPnL As GroupBox
    Friend WithEvents lblDailyPnL As Label
    Friend WithEvents grpMaxDD As GroupBox
    Friend WithEvents lblMaxDD As Label
    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents tabTables As TabControl
    Friend WithEvents tabExposureSymbol As TabPage
    Friend WithEvents dgvExposureBySymbol As DataGridView
    Friend WithEvents tabExposureExpiry As TabPage
    Friend WithEvents dgvExposureByExpiry As DataGridView
    Friend WithEvents tabLimits As TabPage
    Friend WithEvents dgvLimits As DataGridView
    Friend WithEvents tabCharts As TabControl
    Friend WithEvents tabExposureChart As TabPage
    Friend WithEvents chartExposure As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents tabBuckets As TabPage
    Friend WithEvents chartBuckets As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents tabConcentration As TabPage
    Friend WithEvents chartConcentration As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents actionPanel As Panel
    Friend WithEvents btnOpenRiskSettings As Button
    Friend WithEvents btnCloseHighestRisk As Button
    Friend WithEvents btnFlattenAll As Button
    Friend WithEvents status As StatusStrip
    Friend WithEvents lblSummary As ToolStripStatusLabel
End Class
