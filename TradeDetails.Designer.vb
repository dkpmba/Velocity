<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TradeDetails
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
        Me.lblTsTID = New System.Windows.Forms.ToolStripLabel()
        Me.tsCboTID = New System.Windows.Forms.ToolStripComboBox()
        Me.lblTsAccount = New System.Windows.Forms.ToolStripLabel()
        Me.tsCboAccount = New System.Windows.Forms.ToolStripComboBox()
        Me.lblTsSymbol = New System.Windows.Forms.ToolStripLabel()
        Me.tsCboSymbol = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnExportCsv = New System.Windows.Forms.ToolStripButton()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.chkShowHedges = New System.Windows.Forms.CheckBox()
        Me.chkShowFees = New System.Windows.Forms.CheckBox()
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.grpTransactions = New System.Windows.Forms.GroupBox()
        Me.dgvTransactions = New System.Windows.Forms.DataGridView()
        Me.pnlBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.grpSummary = New System.Windows.Forms.GroupBox()
        Me.lblSummary = New System.Windows.Forms.Label()
        Me.grpPnL = New System.Windows.Forms.GroupBox()
        Me.chartPnL = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.lblRows = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblGrossNet = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsTop.SuspendLayout()
        Me.pnlFilters.SuspendLayout()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.grpTransactions.SuspendLayout()
        CType(Me.dgvTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.grpSummary.SuspendLayout()
        Me.grpPnL.SuspendLayout()
        CType(Me.chartPnL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.status.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsTop
        '
        Me.tsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblTsTID, Me.tsCboTID, Me.lblTsAccount, Me.tsCboAccount, Me.lblTsSymbol, Me.tsCboSymbol, Me.ToolStripSeparator1, Me.btnRefresh, Me.btnExportCsv})
        Me.tsTop.Location = New System.Drawing.Point(0, 0)
        Me.tsTop.Name = "tsTop"
        Me.tsTop.Size = New System.Drawing.Size(1100, 27)
        Me.tsTop.TabIndex = 0
        Me.tsTop.Text = "ToolStrip1"
        '
        'lblTsTID
        '
        Me.lblTsTID.Name = "lblTsTID"
        Me.lblTsTID.Size = New System.Drawing.Size(29, 24)
        Me.lblTsTID.Text = "TID:"
        '
        'tsCboTID
        '
        Me.tsCboTID.AutoSize = False
        Me.tsCboTID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsCboTID.Name = "tsCboTID"
        Me.tsCboTID.Size = New System.Drawing.Size(100, 23)
        '
        'lblTsAccount
        '
        Me.lblTsAccount.Name = "lblTsAccount"
        Me.lblTsAccount.Size = New System.Drawing.Size(57, 24)
        Me.lblTsAccount.Text = "Account:"
        '
        'tsCboAccount
        '
        Me.tsCboAccount.AutoSize = False
        Me.tsCboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsCboAccount.Name = "tsCboAccount"
        Me.tsCboAccount.Size = New System.Drawing.Size(120, 23)
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
        'btnExportCsv
        '
        Me.btnExportCsv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnExportCsv.Name = "btnExportCsv"
        Me.btnExportCsv.Size = New System.Drawing.Size(73, 24)
        Me.btnExportCsv.Text = "Export CSV"
        '
        'pnlFilters
        '
        Me.pnlFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilters.Controls.Add(Me.lblFrom)
        Me.pnlFilters.Controls.Add(Me.dtpFrom)
        Me.pnlFilters.Controls.Add(Me.lblTo)
        Me.pnlFilters.Controls.Add(Me.dtpTo)
        Me.pnlFilters.Controls.Add(Me.chkShowHedges)
        Me.pnlFilters.Controls.Add(Me.chkShowFees)
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
        Me.dtpFrom.CustomFormat = "yyyy-MM-dd HH:mm"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(57, 10)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(140, 23)
        Me.dtpFrom.TabIndex = 1
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(203, 14)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(21, 15)
        Me.lblTo.TabIndex = 2
        Me.lblTo.Text = "To"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "yyyy-MM-dd HH:mm"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(230, 10)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(140, 23)
        Me.dtpTo.TabIndex = 3
        '
        'chkShowHedges
        '
        Me.chkShowHedges.AutoSize = True
        Me.chkShowHedges.Location = New System.Drawing.Point(392, 12)
        Me.chkShowHedges.Name = "chkShowHedges"
        Me.chkShowHedges.Size = New System.Drawing.Size(101, 19)
        Me.chkShowHedges.TabIndex = 4
        Me.chkShowHedges.Text = "Show hedges"
        Me.chkShowHedges.UseVisualStyleBackColor = True
        '
        'chkShowFees
        '
        Me.chkShowFees.AutoSize = True
        Me.chkShowFees.Location = New System.Drawing.Point(499, 12)
        Me.chkShowFees.Name = "chkShowFees"
        Me.chkShowFees.Size = New System.Drawing.Size(83, 19)
        Me.chkShowFees.TabIndex = 5
        Me.chkShowFees.Text = "Show fees"
        Me.chkShowFees.UseVisualStyleBackColor = True
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
        Me.splitMain.Panel1.Controls.Add(Me.grpTransactions)
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.pnlBottom)
        Me.splitMain.Size = New System.Drawing.Size(1100, 547)
        Me.splitMain.SplitterDistance = 330
        Me.splitMain.TabIndex = 2
        '
        'grpTransactions
        '
        Me.grpTransactions.Controls.Add(Me.dgvTransactions)
        Me.grpTransactions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpTransactions.Location = New System.Drawing.Point(0, 0)
        Me.grpTransactions.Name = "grpTransactions"
        Me.grpTransactions.Padding = New System.Windows.Forms.Padding(6)
        Me.grpTransactions.Size = New System.Drawing.Size(1100, 330)
        Me.grpTransactions.TabIndex = 0
        Me.grpTransactions.TabStop = False
        Me.grpTransactions.Text = "Transactions"
        '
        'dgvTransactions
        '
        Me.dgvTransactions.AllowUserToAddRows = False
        Me.dgvTransactions.AllowUserToDeleteRows = False
        Me.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTransactions.Location = New System.Drawing.Point(6, 22)
        Me.dgvTransactions.MultiSelect = False
        Me.dgvTransactions.Name = "dgvTransactions"
        Me.dgvTransactions.ReadOnly = True
        Me.dgvTransactions.RowHeadersVisible = False
        Me.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTransactions.Size = New System.Drawing.Size(1088, 302)
        Me.dgvTransactions.TabIndex = 0
        '
        'pnlBottom
        '
        Me.pnlBottom.ColumnCount = 2
        Me.pnlBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.pnlBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.pnlBottom.Controls.Add(Me.grpSummary, 0, 0)
        Me.pnlBottom.Controls.Add(Me.grpPnL, 1, 0)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.RowCount = 1
        Me.pnlBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.pnlBottom.Size = New System.Drawing.Size(1100, 213)
        Me.pnlBottom.TabIndex = 0
        '
        'grpSummary
        '
        Me.grpSummary.Controls.Add(Me.lblSummary)
        Me.grpSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpSummary.Location = New System.Drawing.Point(3, 3)
        Me.grpSummary.Name = "grpSummary"
        Me.grpSummary.Padding = New System.Windows.Forms.Padding(8)
        Me.grpSummary.Size = New System.Drawing.Size(379, 207)
        Me.grpSummary.TabIndex = 0
        Me.grpSummary.TabStop = False
        Me.grpSummary.Text = "Trade Summary"
        '
        'lblSummary
        '
        Me.lblSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSummary.Location = New System.Drawing.Point(8, 24)
        Me.lblSummary.Name = "lblSummary"
        Me.lblSummary.Size = New System.Drawing.Size(363, 175)
        Me.lblSummary.TabIndex = 0
        Me.lblSummary.Text = "TID: —" & Global.Microsoft.VisualBasic.ChrW(10) & "Symbol: —" & Global.Microsoft.VisualBasic.ChrW(10) & "Structure: —" & Global.Microsoft.VisualBasic.ChrW(10) & "Size: —" & Global.Microsoft.VisualBasic.ChrW(10) & "Credit: —" & Global.Microsoft.VisualBasic.ChrW(10) & "URGL: —" & Global.Microsoft.VisualBasic.ChrW(10) & "RGL: —" & Global.Microsoft.VisualBasic.ChrW(10) & "Net Δ/Γ/Θ/V: —"
        '
        'grpPnL
        '
        Me.grpPnL.Controls.Add(Me.chartPnL)
        Me.grpPnL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPnL.Location = New System.Drawing.Point(388, 3)
        Me.grpPnL.Name = "grpPnL"
        Me.grpPnL.Padding = New System.Windows.Forms.Padding(6)
        Me.grpPnL.Size = New System.Drawing.Size(709, 207)
        Me.grpPnL.TabIndex = 1
        Me.grpPnL.TabStop = False
        Me.grpPnL.Text = "In-Trade PnL"
        '
        'chartPnL
        '
        Me.chartPnL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartPnL.Location = New System.Drawing.Point(6, 22)
        Me.chartPnL.Name = "chartPnL"
        Me.chartPnL.Size = New System.Drawing.Size(697, 179)
        Me.chartPnL.TabIndex = 0
        Me.chartPnL.Text = "Chart1"
        '
        'status
        '
        Me.status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblRows, Me.lblGrossNet})
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
        'lblGrossNet
        '
        Me.lblGrossNet.Name = "lblGrossNet"
        Me.lblGrossNet.Size = New System.Drawing.Size(170, 17)
        Me.lblGrossNet.Text = "Gross: $0 • Fees: $0 • Net: $0"
        '
        'TradeDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 642)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.pnlFilters)
        Me.Controls.Add(Me.tsTop)
        Me.Controls.Add(Me.status)
        Me.MinimumSize = New System.Drawing.Size(1000, 600)
        Me.Name = "TradeDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Trade Details"
        Me.tsTop.ResumeLayout(False)
        Me.tsTop.PerformLayout()
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.grpTransactions.ResumeLayout(False)
        CType(Me.dgvTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.grpSummary.ResumeLayout(False)
        Me.grpPnL.ResumeLayout(False)
        CType(Me.chartPnL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tsTop As ToolStrip
    Friend WithEvents lblTsTID As ToolStripLabel
    Friend WithEvents tsCboTID As ToolStripComboBox
    Friend WithEvents lblTsAccount As ToolStripLabel
    Friend WithEvents tsCboAccount As ToolStripComboBox
    Friend WithEvents lblTsSymbol As ToolStripLabel
    Friend WithEvents tsCboSymbol As ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnRefresh As ToolStripButton
    Friend WithEvents btnExportCsv As ToolStripButton
    Friend WithEvents pnlFilters As Panel
    Friend WithEvents lblFrom As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents lblTo As Label
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents chkShowHedges As CheckBox
    Friend WithEvents chkShowFees As CheckBox
    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents grpTransactions As GroupBox
    Friend WithEvents dgvTransactions As DataGridView
    Friend WithEvents pnlBottom As TableLayoutPanel
    Friend WithEvents grpSummary As GroupBox
    Friend WithEvents lblSummary As Label
    Friend WithEvents grpPnL As GroupBox
    Friend WithEvents chartPnL As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents status As StatusStrip
    Friend WithEvents lblRows As ToolStripStatusLabel
    Friend WithEvents lblGrossNet As ToolStripStatusLabel
End Class
