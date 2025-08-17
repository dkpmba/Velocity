<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TradeLog
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
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnExportCsv = New System.Windows.Forms.ToolStripButton()
        Me.btnExportPng = New System.Windows.Forms.ToolStripButton()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.cboAccount = New System.Windows.Forms.ComboBox()
        Me.lblSymbol = New System.Windows.Forms.Label()
        Me.cboSymbol = New System.Windows.Forms.ComboBox()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.grpSummary = New System.Windows.Forms.GroupBox()
        Me.dgvSummary = New System.Windows.Forms.DataGridView()
        Me.grpChart = New System.Windows.Forms.GroupBox()
        Me.chartEquity = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.lblRows = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblPnLTotals = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsTop.SuspendLayout()
        Me.pnlFilters.SuspendLayout()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.grpSummary.SuspendLayout()
        CType(Me.dgvSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpChart.SuspendLayout()
        CType(Me.chartEquity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.status.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsTop
        '
        Me.tsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.ToolStripSeparator1, Me.btnExportCsv, Me.btnExportPng})
        Me.tsTop.Location = New System.Drawing.Point(0, 0)
        Me.tsTop.Name = "tsTop"
        Me.tsTop.Size = New System.Drawing.Size(1100, 27)
        Me.tsTop.TabIndex = 0
        Me.tsTop.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(55, 24)
        Me.btnRefresh.Text = "Refresh"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'btnExportCsv
        '
        Me.btnExportCsv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnExportCsv.Name = "btnExportCsv"
        Me.btnExportCsv.Size = New System.Drawing.Size(73, 24)
        Me.btnExportCsv.Text = "Export CSV"
        '
        'btnExportPng
        '
        Me.btnExportPng.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnExportPng.Name = "btnExportPng"
        Me.btnExportPng.Size = New System.Drawing.Size(77, 24)
        Me.btnExportPng.Text = "Export PNG"
        '
        'pnlFilters
        '
        Me.pnlFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilters.Controls.Add(Me.lblFrom)
        Me.pnlFilters.Controls.Add(Me.dtpFrom)
        Me.pnlFilters.Controls.Add(Me.lblTo)
        Me.pnlFilters.Controls.Add(Me.dtpTo)
        Me.pnlFilters.Controls.Add(Me.lblAccount)
        Me.pnlFilters.Controls.Add(Me.cboAccount)
        Me.pnlFilters.Controls.Add(Me.lblSymbol)
        Me.pnlFilters.Controls.Add(Me.cboSymbol)
        Me.pnlFilters.Controls.Add(Me.btnApply)
        Me.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilters.Location = New System.Drawing.Point(0, 27)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.pnlFilters.Size = New System.Drawing.Size(1100, 46)
        Me.pnlFilters.TabIndex = 1
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(12, 14)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(39, 15)
        Me.lblFrom.TabIndex = 0
        Me.lblFrom.Text = "From"
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "yyyy-MM-dd"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(57, 10)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(104, 23)
        Me.dtpFrom.TabIndex = 1
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(167, 14)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(21, 15)
        Me.lblTo.TabIndex = 2
        Me.lblTo.Text = "To"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "yyyy-MM-dd"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(194, 10)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(104, 23)
        Me.dtpTo.TabIndex = 3
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.Location = New System.Drawing.Point(308, 14)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(54, 15)
        Me.lblAccount.TabIndex = 4
        Me.lblAccount.Text = "Account"
        '
        'cboAccount
        '
        Me.cboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAccount.FormattingEnabled = True
        Me.cboAccount.Location = New System.Drawing.Point(368, 10)
        Me.cboAccount.Name = "cboAccount"
        Me.cboAccount.Size = New System.Drawing.Size(120, 23)
        Me.cboAccount.TabIndex = 5
        '
        'lblSymbol
        '
        Me.lblSymbol.AutoSize = True
        Me.lblSymbol.Location = New System.Drawing.Point(494, 14)
        Me.lblSymbol.Name = "lblSymbol"
        Me.lblSymbol.Size = New System.Drawing.Size(50, 15)
        Me.lblSymbol.TabIndex = 6
        Me.lblSymbol.Text = "Symbol"
        '
        'cboSymbol
        '
        Me.cboSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSymbol.FormattingEnabled = True
        Me.cboSymbol.Location = New System.Drawing.Point(550, 10)
        Me.cboSymbol.Name = "cboSymbol"
        Me.cboSymbol.Size = New System.Drawing.Size(120, 23)
        Me.cboSymbol.TabIndex = 7
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApply.Location = New System.Drawing.Point(1008, 9)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(80, 26)
        Me.btnApply.TabIndex = 8
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'splitMain
        '
        Me.splitMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMain.Location = New System.Drawing.Point(0, 73)
        Me.splitMain.Name = "splitMain"
        Me.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitMain.Panel1
        '
        Me.splitMain.Panel1.Controls.Add(Me.grpSummary)
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.grpChart)
        Me.splitMain.Size = New System.Drawing.Size(1100, 547)
        Me.splitMain.SplitterDistance = 300
        Me.splitMain.TabIndex = 2
        '
        'grpSummary
        '
        Me.grpSummary.Controls.Add(Me.dgvSummary)
        Me.grpSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpSummary.Location = New System.Drawing.Point(0, 0)
        Me.grpSummary.Name = "grpSummary"
        Me.grpSummary.Padding = New System.Windows.Forms.Padding(6)
        Me.grpSummary.Size = New System.Drawing.Size(1100, 300)
        Me.grpSummary.TabIndex = 0
        Me.grpSummary.TabStop = False
        Me.grpSummary.Text = "Trades Summary"
        '
        'dgvSummary
        '
        Me.dgvSummary.AllowUserToAddRows = False
        Me.dgvSummary.AllowUserToDeleteRows = False
        Me.dgvSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSummary.Location = New System.Drawing.Point(6, 22)
        Me.dgvSummary.MultiSelect = False
        Me.dgvSummary.Name = "dgvSummary"
        Me.dgvSummary.ReadOnly = True
        Me.dgvSummary.RowHeadersVisible = False
        Me.dgvSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSummary.Size = New System.Drawing.Size(1088, 272)
        Me.dgvSummary.TabIndex = 0
        '
        'grpChart
        '
        Me.grpChart.Controls.Add(Me.chartEquity)
        Me.grpChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpChart.Location = New System.Drawing.Point(0, 0)
        Me.grpChart.Name = "grpChart"
        Me.grpChart.Padding = New System.Windows.Forms.Padding(6)
        Me.grpChart.Size = New System.Drawing.Size(1100, 243)
        Me.grpChart.TabIndex = 0
        Me.grpChart.TabStop = False
        Me.grpChart.Text = "Cumulative PnL / Equity Line"
        '
        'chartEquity
        '
        Me.chartEquity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartEquity.Location = New System.Drawing.Point(6, 22)
        Me.chartEquity.Name = "chartEquity"
        Me.chartEquity.Size = New System.Drawing.Size(1088, 215)
        Me.chartEquity.TabIndex = 0
        Me.chartEquity.Text = "Chart1"
        '
        'status
        '
        Me.status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblRows, Me.lblPnLTotals})
        Me.status.Location = New System.Drawing.Point(0, 620)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(1100, 22)
        Me.status.TabIndex = 3
        Me.status.Text = "StatusStrip1"
        '
        'lblRows
        '
        Me.lblRows.Name = "lblRows"
        Me.lblRows.Size = New System.Drawing.Size(56, 17)
        Me.lblRows.Text = "Rows: 0"
        '
        'lblPnLTotals
        '
        Me.lblPnLTotals.Name = "lblPnLTotals"
        Me.lblPnLTotals.Size = New System.Drawing.Size(159, 17)
        Me.lblPnLTotals.Text = "URGL: $0 • RGL: $0 • Net: $0"
        '
        'TradeLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 642)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.pnlFilters)
        Me.Controls.Add(Me.tsTop)
        Me.Controls.Add(Me.status)
        Me.MinimumSize = New System.Drawing.Size(1000, 600)
        Me.Name = "TradeLog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Trade Log"
        Me.tsTop.ResumeLayout(False)
        Me.tsTop.PerformLayout()
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.grpSummary.ResumeLayout(False)
        CType(Me.dgvSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpChart.ResumeLayout(False)
        CType(Me.chartEquity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tsTop As ToolStrip
    Friend WithEvents btnRefresh As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnExportCsv As ToolStripButton
    Friend WithEvents btnExportPng As ToolStripButton
    Friend WithEvents pnlFilters As Panel
    Friend WithEvents lblFrom As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents lblTo As Label
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents lblAccount As Label
    Friend WithEvents cboAccount As ComboBox
    Friend WithEvents lblSymbol As Label
    Friend WithEvents cboSymbol As ComboBox
    Friend WithEvents btnApply As Button
    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents grpSummary As GroupBox
    Friend WithEvents dgvSummary As DataGridView
    Friend WithEvents grpChart As GroupBox
    Friend WithEvents chartEquity As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents status As StatusStrip
    Friend WithEvents lblRows As ToolStripStatusLabel
    Friend WithEvents lblPnLTotals As ToolStripStatusLabel
End Class
