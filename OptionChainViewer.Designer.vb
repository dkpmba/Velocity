<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionChainViewer
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
        Me.tsTop = New System.Windows.Forms.ToolStrip()
        Me.lblTsSymbol = New System.Windows.Forms.ToolStripLabel()
        Me.tsCboSymbol = New System.Windows.Forms.ToolStripComboBox()
        Me.lblTsExpiry = New System.Windows.Forms.ToolStripLabel()
        Me.tsCboExpiry = New System.Windows.Forms.ToolStripComboBox()
        Me.lblTsGroup = New System.Windows.Forms.ToolStripLabel()
        Me.tsCboGroupBy = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSendATMStraddle = New System.Windows.Forms.ToolStripButton()
        Me.btnSend20DStrangle = New System.Windows.Forms.ToolStripButton()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.flowFilters = New System.Windows.Forms.FlowLayoutPanel()
        Me.grpDelta = New System.Windows.Forms.GroupBox()
        Me.lblDeltaRange = New System.Windows.Forms.Label()
        Me.nudDeltaMin = New System.Windows.Forms.NumericUpDown()
        Me.nudDeltaMax = New System.Windows.Forms.NumericUpDown()
        Me.grpMoneyness = New System.Windows.Forms.GroupBox()
        Me.chkITM = New System.Windows.Forms.CheckBox()
        Me.chkATM = New System.Windows.Forms.CheckBox()
        Me.chkOTM = New System.Windows.Forms.CheckBox()
        Me.grpLiquidity = New System.Windows.Forms.GroupBox()
        Me.lblVolMin = New System.Windows.Forms.Label()
        Me.nudVolMin = New System.Windows.Forms.NumericUpDown()
        Me.lblOIMin = New System.Windows.Forms.Label()
        Me.nudOIMin = New System.Windows.Forms.NumericUpDown()
        Me.lblSpread = New System.Windows.Forms.Label()
        Me.nudMaxSpreadPct = New System.Windows.Forms.NumericUpDown()
        Me.splitGrids = New System.Windows.Forms.SplitContainer()
        Me.grpCalls = New System.Windows.Forms.GroupBox()
        Me.dgvCalls = New System.Windows.Forms.DataGridView()
        Me.grpPuts = New System.Windows.Forms.GroupBox()
        Me.dgvPuts = New System.Windows.Forms.DataGridView()
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.lblSummary = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblLastUpdate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsTop.SuspendLayout()
        Me.pnlFilters.SuspendLayout()
        Me.flowFilters.SuspendLayout()
        Me.grpDelta.SuspendLayout()
        CType(Me.nudDeltaMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudDeltaMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMoneyness.SuspendLayout()
        Me.grpLiquidity.SuspendLayout()
        CType(Me.nudVolMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudOIMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMaxSpreadPct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitGrids, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitGrids.Panel1.SuspendLayout()
        Me.splitGrids.Panel2.SuspendLayout()
        Me.splitGrids.SuspendLayout()
        Me.grpCalls.SuspendLayout()
        CType(Me.dgvCalls, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPuts.SuspendLayout()
        CType(Me.dgvPuts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.status.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsTop
        '
        Me.tsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblTsSymbol, Me.tsCboSymbol, Me.lblTsExpiry, Me.tsCboExpiry, Me.lblTsGroup, Me.tsCboGroupBy, Me.ToolStripSeparator1, Me.btnRefresh, Me.ToolStripSeparator2, Me.btnSendATMStraddle, Me.btnSend20DStrangle})
        Me.tsTop.Location = New System.Drawing.Point(0, 0)
        Me.tsTop.Name = "tsTop"
        Me.tsTop.Size = New System.Drawing.Size(1100, 27)
        Me.tsTop.TabIndex = 0
        Me.tsTop.Text = "ToolStrip1"
        '
        'lblTsSymbol
        '
        Me.lblTsSymbol.Name = "lblTsSymbol"
        Me.lblTsSymbol.Size = New System.Drawing.Size(54, 24)
        Me.lblTsSymbol.Text = "Symbol:"
        '
        'tsCboSymbol
        '
        Me.tsCboSymbol.AutoSize = False
        Me.tsCboSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsCboSymbol.Name = "tsCboSymbol"
        Me.tsCboSymbol.Size = New System.Drawing.Size(120, 23)
        '
        'lblTsExpiry
        '
        Me.lblTsExpiry.Name = "lblTsExpiry"
        Me.lblTsExpiry.Size = New System.Drawing.Size(44, 24)
        Me.lblTsExpiry.Text = "Expiry:"
        '
        'tsCboExpiry
        '
        Me.tsCboExpiry.AutoSize = False
        Me.tsCboExpiry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsCboExpiry.Name = "tsCboExpiry"
        Me.tsCboExpiry.Size = New System.Drawing.Size(120, 23)
        '
        'lblTsGroup
        '
        Me.lblTsGroup.Name = "lblTsGroup"
        Me.lblTsGroup.Size = New System.Drawing.Size(60, 24)
        Me.lblTsGroup.Text = "Group by:"
        '
        'tsCboGroupBy
        '
        Me.tsCboGroupBy.AutoSize = False
        Me.tsCboGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsCboGroupBy.Items.AddRange(New Object() {"None", "Expiry", "Δ bucket"})
        Me.tsCboGroupBy.Name = "tsCboGroupBy"
        Me.tsCboGroupBy.Size = New System.Drawing.Size(100, 23)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'btnRefresh
        '
        Me.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(55, 24)
        Me.btnRefresh.Text = "Refresh"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 27)
        '
        'btnSendATMStraddle
        '
        Me.btnSendATMStraddle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnSendATMStraddle.Name = "btnSendATMStraddle"
        Me.btnSendATMStraddle.Size = New System.Drawing.Size(140, 24)
        Me.btnSendATMStraddle.Text = "Send ATM Straddle → Design"
        '
        'btnSend20DStrangle
        '
        Me.btnSend20DStrangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnSend20DStrangle.Name = "btnSend20DStrangle"
        Me.btnSend20DStrangle.Size = New System.Drawing.Size(150, 24)
        Me.btnSend20DStrangle.Text = "Send 20Δ Strangle → Design"
        '
        'pnlFilters
        '
        Me.pnlFilters.Controls.Add(Me.flowFilters)
        Me.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilters.Location = New System.Drawing.Point(0, 27)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.pnlFilters.Size = New System.Drawing.Size(1100, 86)
        Me.pnlFilters.TabIndex = 1
        '
        'flowFilters
        '
        Me.flowFilters.AutoScroll = True
        Me.flowFilters.Controls.Add(Me.grpDelta)
        Me.flowFilters.Controls.Add(Me.grpMoneyness)
        Me.flowFilters.Controls.Add(Me.grpLiquidity)
        Me.flowFilters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowFilters.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight
        Me.flowFilters.Location = New System.Drawing.Point(8, 6)
        Me.flowFilters.Name = "flowFilters"
        Me.flowFilters.Padding = New System.Windows.Forms.Padding(2)
        Me.flowFilters.Size = New System.Drawing.Size(1084, 74)
        Me.flowFilters.TabIndex = 0
        '
        'grpDelta
        '
        Me.grpDelta.Controls.Add(Me.lblDeltaRange)
        Me.grpDelta.Controls.Add(Me.nudDeltaMin)
        Me.grpDelta.Controls.Add(Me.nudDeltaMax)
        Me.grpDelta.Location = New System.Drawing.Point(7, 7)
        Me.grpDelta.Margin = New System.Windows.Forms.Padding(5)
        Me.grpDelta.Name = "grpDelta"
        Me.grpDelta.Padding = New System.Windows.Forms.Padding(8)
        Me.grpDelta.Size = New System.Drawing.Size(230, 58)
        Me.grpDelta.TabIndex = 0
        Me.grpDelta.TabStop = False
        Me.grpDelta.Text = "Δ Range"
        '
        'lblDeltaRange
        '
        Me.lblDeltaRange.AutoSize = True
        Me.lblDeltaRange.Location = New System.Drawing.Point(14, 25)
        Me.lblDeltaRange.Name = "lblDeltaRange"
        Me.lblDeltaRange.Size = New System.Drawing.Size(39, 15)
        Me.lblDeltaRange.TabIndex = 0
        Me.lblDeltaRange.Text = "Min / Max"
        '
        'nudDeltaMin
        '
        Me.nudDeltaMin.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudDeltaMin.Location = New System.Drawing.Point(90, 22)
        Me.nudDeltaMin.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nudDeltaMin.Name = "nudDeltaMin"
        Me.nudDeltaMin.Size = New System.Drawing.Size(60, 23)
        Me.nudDeltaMin.TabIndex = 1
        Me.nudDeltaMin.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nudDeltaMax
        '
        Me.nudDeltaMax.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudDeltaMax.Location = New System.Drawing.Point(156, 22)
        Me.nudDeltaMax.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nudDeltaMax.Name = "nudDeltaMax"
        Me.nudDeltaMax.Size = New System.Drawing.Size(60, 23)
        Me.nudDeltaMax.TabIndex = 2
        Me.nudDeltaMax.Value = New Decimal(New Integer() {90, 0, 0, 0})
        '
        'grpMoneyness
        '
        Me.grpMoneyness.Controls.Add(Me.chkITM)
        Me.grpMoneyness.Controls.Add(Me.chkATM)
        Me.grpMoneyness.Controls.Add(Me.chkOTM)
        Me.grpMoneyness.Location = New System.Drawing.Point(247, 7)
        Me.grpMoneyness.Margin = New System.Windows.Forms.Padding(5)
        Me.grpMoneyness.Name = "grpMoneyness"
        Me.grpMoneyness.Padding = New System.Windows.Forms.Padding(8)
        Me.grpMoneyness.Size = New System.Drawing.Size(220, 58)
        Me.grpMoneyness.TabIndex = 1
        Me.grpMoneyness.TabStop = False
        Me.grpMoneyness.Text = "Moneyness"
        '
        'chkITM
        '
        Me.chkITM.AutoSize = True
        Me.chkITM.Checked = True
        Me.chkITM.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkITM.Location = New System.Drawing.Point(14, 24)
        Me.chkITM.Name = "chkITM"
        Me.chkITM.Size = New System.Drawing.Size(48, 19)
        Me.chkITM.TabIndex = 0
        Me.chkITM.Text = "ITM"
        Me.chkITM.UseVisualStyleBackColor = True
        '
        'chkATM
        '
        Me.chkATM.AutoSize = True
        Me.chkATM.Checked = True
        Me.chkATM.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkATM.Location = New System.Drawing.Point(83, 24)
        Me.chkATM.Name = "chkATM"
        Me.chkATM.Size = New System.Drawing.Size(52, 19)
        Me.chkATM.TabIndex = 1
        Me.chkATM.Text = "ATM"
        Me.chkATM.UseVisualStyleBackColor = True
        '
        'chkOTM
        '
        Me.chkOTM.AutoSize = True
        Me.chkOTM.Checked = True
        Me.chkOTM.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOTM.Location = New System.Drawing.Point(150, 24)
        Me.chkOTM.Name = "chkOTM"
        Me.chkOTM.Size = New System.Drawing.Size(55, 19)
        Me.chkOTM.TabIndex = 2
        Me.chkOTM.Text = "OTM"
        Me.chkOTM.UseVisualStyleBackColor = True
        '
        'grpLiquidity
        '
        Me.grpLiquidity.Controls.Add(Me.lblVolMin)
        Me.grpLiquidity.Controls.Add(Me.nudVolMin)
        Me.grpLiquidity.Controls.Add(Me.lblOIMin)
        Me.grpLiquidity.Controls.Add(Me.nudOIMin)
        Me.grpLiquidity.Controls.Add(Me.lblSpread)
        Me.grpLiquidity.Controls.Add(Me.nudMaxSpreadPct)
        Me.grpLiquidity.Location = New System.Drawing.Point(477, 7)
        Me.grpLiquidity.Margin = New System.Windows.Forms.Padding(5)
        Me.grpLiquidity.Name = "grpLiquidity"
        Me.grpLiquidity.Padding = New System.Windows.Forms.Padding(8)
        Me.grpLiquidity.Size = New System.Drawing.Size(420, 58)
        Me.grpLiquidity.TabIndex = 2
        Me.grpLiquidity.TabStop = False
        Me.grpLiquidity.Text = "Liquidity Filters"
        '
        'lblVolMin
        '
        Me.lblVolMin.AutoSize = True
        Me.lblVolMin.Location = New System.Drawing.Point(14, 25)
        Me.lblVolMin.Name = "lblVolMin"
        Me.lblVolMin.Size = New System.Drawing.Size(52, 15)
        Me.lblVolMin.TabIndex = 0
        Me.lblVolMin.Text = "Vol ≥"
        '
        'nudVolMin
        '
        Me.nudVolMin.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nudVolMin.Location = New System.Drawing.Point(64, 22)
        Me.nudVolMin.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudVolMin.Name = "nudVolMin"
        Me.nudVolMin.Size = New System.Drawing.Size(80, 23)
        Me.nudVolMin.TabIndex = 1
        '
        'lblOIMin
        '
        Me.lblOIMin.AutoSize = True
        Me.lblOIMin.Location = New System.Drawing.Point(150, 25)
        Me.lblOIMin.Name = "lblOIMin"
        Me.lblOIMin.Size = New System.Drawing.Size(46, 15)
        Me.lblOIMin.TabIndex = 2
        Me.lblOIMin.Text = "OI ≥"
        '
        'nudOIMin
        '
        Me.nudOIMin.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nudOIMin.Location = New System.Drawing.Point(196, 22)
        Me.nudOIMin.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudOIMin.Name = "nudOIMin"
        Me.nudOIMin.Size = New System.Drawing.Size(80, 23)
        Me.nudOIMin.TabIndex = 3
        '
        'lblSpread
        '
        Me.lblSpread.AutoSize = True
        Me.lblSpread.Location = New System.Drawing.Point(282, 25)
        Me.lblSpread.Name = "lblSpread"
        Me.lblSpread.Size = New System.Drawing.Size(78, 15)
        Me.lblSpread.TabIndex = 4
        Me.lblSpread.Text = "Spread ≤ %"
        '
        'nudMaxSpreadPct
        '
        Me.nudMaxSpreadPct.DecimalPlaces = 1
        Me.nudMaxSpreadPct.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nudMaxSpreadPct.Location = New System.Drawing.Point(366, 22)
        Me.nudMaxSpreadPct.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nudMaxSpreadPct.Name = "nudMaxSpreadPct"
        Me.nudMaxSpreadPct.Size = New System.Drawing.Size(48, 23)
        Me.nudMaxSpreadPct.TabIndex = 5
        Me.nudMaxSpreadPct.Value = New Decimal(New Integer() {25, 0, 0, 0})
        '
        'splitGrids
        '
        Me.splitGrids.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitGrids.Location = New System.Drawing.Point(0, 113)
        Me.splitGrids.Name = "splitGrids"
        '
        'splitGrids.Panel1
        '
        Me.splitGrids.Panel1.Controls.Add(Me.grpCalls)
        '
        'splitGrids.Panel2
        '
        Me.splitGrids.Panel2.Controls.Add(Me.grpPuts)
        Me.splitGrids.Size = New System.Drawing.Size(1100, 507)
        Me.splitGrids.SplitterDistance = 550
        Me.splitGrids.TabIndex = 2
        '
        'grpCalls
        '
        Me.grpCalls.Controls.Add(Me.dgvCalls)
        Me.grpCalls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCalls.Location = New System.Drawing.Point(0, 0)
        Me.grpCalls.Name = "grpCalls"
        Me.grpCalls.Padding = New System.Windows.Forms.Padding(6)
        Me.grpCalls.Size = New System.Drawing.Size(550, 507)
        Me.grpCalls.TabIndex = 0
        Me.grpCalls.TabStop = False
        Me.grpCalls.Text = "Calls"
        '
        'dgvCalls
        '
        Me.dgvCalls.AllowUserToAddRows = False
        Me.dgvCalls.AllowUserToDeleteRows = False
        Me.dgvCalls.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCalls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCalls.Location = New System.Drawing.Point(6, 22)
        Me.dgvCalls.MultiSelect = True
        Me.dgvCalls.Name = "dgvCalls"
        Me.dgvCalls.ReadOnly = True
        Me.dgvCalls.RowHeadersVisible = False
        Me.dgvCalls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCalls.Size = New System.Drawing.Size(538, 479)
        Me.dgvCalls.TabIndex = 0
        '
        'grpPuts
        '
        Me.grpPuts.Controls.Add(Me.dgvPuts)
        Me.grpPuts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPuts.Location = New System.Drawing.Point(0, 0)
        Me.grpPuts.Name = "grpPuts"
        Me.grpPuts.Padding = New System.Windows.Forms.Padding(6)
        Me.grpPuts.Size = New System.Drawing.Size(546, 507)
        Me.grpPuts.TabIndex = 0
        Me.grpPuts.TabStop = False
        Me.grpPuts.Text = "Puts"
        '
        'dgvPuts
        '
        Me.dgvPuts.AllowUserToAddRows = False
        Me.dgvPuts.AllowUserToDeleteRows = False
        Me.dgvPuts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvPuts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPuts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPuts.Location = New System.Drawing.Point(6, 22)
        Me.dgvPuts.MultiSelect = True
        Me.dgvPuts.Name = "dgvPuts"
        Me.dgvPuts.ReadOnly = True
        Me.dgvPuts.RowHeadersVisible = False
        Me.dgvPuts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPuts.Size = New System.Drawing.Size(534, 479)
        Me.dgvPuts.TabIndex = 0
        '
        'status
        '
        Me.status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblSummary, Me.lblLastUpdate})
        Me.status.Location = New System.Drawing.Point(0, 620)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(1100, 22)
        Me.status.TabIndex = 3
        Me.status.Text = "StatusStrip1"
        '
        'lblSummary
        '
        Me.lblSummary.Name = "lblSummary"
        Me.lblSummary.Size = New System.Drawing.Size(176, 17)
        Me.lblSummary.Text = "0 calls • 0 puts (filtered / total)"
        '
        'lblLastUpdate
        '
        Me.lblLastUpdate.Name = "lblLastUpdate"
        Me.lblLastUpdate.Size = New System.Drawing.Size(85, 17)
        Me.lblLastUpdate.Text = "Updated: —"
        '
        'OptionChainViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 642)
        Me.Controls.Add(Me.splitGrids)
        Me.Controls.Add(Me.pnlFilters)
        Me.Controls.Add(Me.tsTop)
        Me.Controls.Add(Me.status)
        Me.MinimumSize = New System.Drawing.Size(1000, 600)
        Me.Name = "OptionChainViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Option Chain Viewer"
        Me.tsTop.ResumeLayout(False)
        Me.tsTop.PerformLayout()
        Me.pnlFilters.ResumeLayout(False)
        Me.flowFilters.ResumeLayout(False)
        Me.grpDelta.ResumeLayout(False)
        Me.grpDelta.PerformLayout()
        CType(Me.nudDeltaMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudDeltaMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMoneyness.ResumeLayout(False)
        Me.grpMoneyness.PerformLayout()
        Me.grpLiquidity.ResumeLayout(False)
        Me.grpLiquidity.PerformLayout()
        CType(Me.nudVolMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudOIMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMaxSpreadPct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitGrids.Panel1.ResumeLayout(False)
        Me.splitGrids.Panel2.ResumeLayout(False)
        CType(Me.splitGrids, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitGrids.ResumeLayout(False)
        Me.grpCalls.ResumeLayout(False)
        CType(Me.dgvCalls, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPuts.ResumeLayout(False)
        CType(Me.dgvPuts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tsTop As ToolStrip
    Friend WithEvents lblTsSymbol As ToolStripLabel
    Friend WithEvents tsCboSymbol As ToolStripComboBox
    Friend WithEvents lblTsExpiry As ToolStripLabel
    Friend WithEvents tsCboExpiry As ToolStripComboBox
    Friend WithEvents lblTsGroup As ToolStripLabel
    Friend WithEvents tsCboGroupBy As ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnRefresh As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnSendATMStraddle As ToolStripButton
    Friend WithEvents btnSend20DStrangle As ToolStripButton
    Friend WithEvents pnlFilters As Panel
    Friend WithEvents flowFilters As FlowLayoutPanel
    Friend WithEvents grpDelta As GroupBox
    Friend WithEvents lblDeltaRange As Label
    Friend WithEvents nudDeltaMin As NumericUpDown
    Friend WithEvents nudDeltaMax As NumericUpDown
    Friend WithEvents grpMoneyness As GroupBox
    Friend WithEvents chkITM As CheckBox
    Friend WithEvents chkATM As CheckBox
    Friend WithEvents chkOTM As CheckBox
    Friend WithEvents grpLiquidity As GroupBox
    Friend WithEvents lblVolMin As Label
    Friend WithEvents nudVolMin As NumericUpDown
    Friend WithEvents lblOIMin As Label
    Friend WithEvents nudOIMin As NumericUpDown
    Friend WithEvents lblSpread As Label
    Friend WithEvents nudMaxSpreadPct As NumericUpDown
    Friend WithEvents splitGrids As SplitContainer
    Friend WithEvents grpCalls As GroupBox
    Friend WithEvents dgvCalls As DataGridView
    Friend WithEvents grpPuts As GroupBox
    Friend WithEvents dgvPuts As DataGridView
    Friend WithEvents status As StatusStrip
    Friend WithEvents lblSummary As ToolStripStatusLabel
    Friend WithEvents lblLastUpdate As ToolStripStatusLabel
End Class
