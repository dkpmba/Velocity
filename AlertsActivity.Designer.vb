<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AlertsActivity
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
        Me.components = New System.ComponentModel.Container()
        Me.tsTop = New System.Windows.Forms.ToolStrip()
        Me.btnAck = New System.Windows.Forms.ToolStripButton()
        Me.btnSnooze = New System.Windows.Forms.ToolStripDropDownButton()
        Me.miSnooze5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.miSnooze15 = New System.Windows.Forms.ToolStripMenuItem()
        Me.miSnooze60 = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnDismiss = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnNewRule = New System.Windows.Forms.ToolStripButton()
        Me.btnEditRule = New System.Windows.Forms.ToolStripButton()
        Me.btnDeleteRule = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnTestSound = New System.Windows.Forms.ToolStripButton()
        Me.btnExportCsv = New System.Windows.Forms.ToolStripButton()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.cboSeverity = New System.Windows.Forms.ComboBox()
        Me.lblSeverity = New System.Windows.Forms.Label()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.chkOnlyActive = New System.Windows.Forms.CheckBox()
        Me.chkPopup = New System.Windows.Forms.CheckBox()
        Me.chkSound = New System.Windows.Forms.CheckBox()
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.grpAlerts = New System.Windows.Forms.GroupBox()
        Me.dgvAlerts = New System.Windows.Forms.DataGridView()
        Me.grpActivity = New System.Windows.Forms.GroupBox()
        Me.dgvActivity = New System.Windows.Forms.DataGridView()
        Me.ctxAlerts = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ctxAck = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxDismiss = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.lblCounts = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblUpdated = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsTop.SuspendLayout()
        Me.pnlFilters.SuspendLayout()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.grpAlerts.SuspendLayout()
        CType(Me.dgvAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpActivity.SuspendLayout()
        CType(Me.dgvActivity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ctxAlerts.SuspendLayout()
        Me.status.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsTop
        '
        Me.tsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAck, Me.btnSnooze, Me.btnDismiss, Me.ToolStripSeparator1, Me.btnNewRule, Me.btnEditRule, Me.btnDeleteRule, Me.ToolStripSeparator2, Me.btnTestSound, Me.btnExportCsv})
        Me.tsTop.Location = New System.Drawing.Point(0, 0)
        Me.tsTop.Name = "tsTop"
        Me.tsTop.Size = New System.Drawing.Size(1100, 27)
        Me.tsTop.TabIndex = 0
        '
        'btnAck
        '
        Me.btnAck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnAck.Name = "btnAck"
        Me.btnAck.Size = New System.Drawing.Size(86, 24)
        Me.btnAck.Text = "Acknowledge"
        '
        'btnSnooze
        '
        Me.btnSnooze.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnSnooze.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miSnooze5, Me.miSnooze15, Me.miSnooze60})
        Me.btnSnooze.Name = "btnSnooze"
        Me.btnSnooze.Size = New System.Drawing.Size(64, 24)
        Me.btnSnooze.Text = "Snooze"
        '
        'miSnooze5
        '
        Me.miSnooze5.Name = "miSnooze5"
        Me.miSnooze5.Size = New System.Drawing.Size(147, 22)
        Me.miSnooze5.Text = "5 minutes"
        '
        'miSnooze15
        '
        Me.miSnooze15.Name = "miSnooze15"
        Me.miSnooze15.Size = New System.Drawing.Size(147, 22)
        Me.miSnooze15.Text = "15 minutes"
        '
        'miSnooze60
        '
        Me.miSnooze60.Name = "miSnooze60"
        Me.miSnooze60.Size = New System.Drawing.Size(147, 22)
        Me.miSnooze60.Text = "1 hour"
        '
        'btnDismiss
        '
        Me.btnDismiss.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnDismiss.Name = "btnDismiss"
        Me.btnDismiss.Size = New System.Drawing.Size(57, 24)
        Me.btnDismiss.Text = "Dismiss"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'btnNewRule
        '
        Me.btnNewRule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnNewRule.Name = "btnNewRule"
        Me.btnNewRule.Size = New System.Drawing.Size(67, 24)
        Me.btnNewRule.Text = "New Rule"
        '
        'btnEditRule
        '
        Me.btnEditRule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnEditRule.Name = "btnEditRule"
        Me.btnEditRule.Size = New System.Drawing.Size(61, 24)
        Me.btnEditRule.Text = "Edit Rule"
        '
        'btnDeleteRule
        '
        Me.btnDeleteRule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnDeleteRule.Name = "btnDeleteRule"
        Me.btnDeleteRule.Size = New System.Drawing.Size(77, 24)
        Me.btnDeleteRule.Text = "Delete Rule"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 27)
        '
        'btnTestSound
        '
        Me.btnTestSound.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnTestSound.Name = "btnTestSound"
        Me.btnTestSound.Size = New System.Drawing.Size(73, 24)
        Me.btnTestSound.Text = "Test Sound"
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
        Me.pnlFilters.Controls.Add(Me.txtSearch)
        Me.pnlFilters.Controls.Add(Me.lblSearch)
        Me.pnlFilters.Controls.Add(Me.cboSeverity)
        Me.pnlFilters.Controls.Add(Me.lblSeverity)
        Me.pnlFilters.Controls.Add(Me.cboType)
        Me.pnlFilters.Controls.Add(Me.lblType)
        Me.pnlFilters.Controls.Add(Me.lblFrom)
        Me.pnlFilters.Controls.Add(Me.dtpFrom)
        Me.pnlFilters.Controls.Add(Me.lblTo)
        Me.pnlFilters.Controls.Add(Me.dtpTo)
        Me.pnlFilters.Controls.Add(Me.chkOnlyActive)
        Me.pnlFilters.Controls.Add(Me.chkPopup)
        Me.pnlFilters.Controls.Add(Me.chkSound)
        Me.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilters.Location = New System.Drawing.Point(0, 27)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.pnlFilters.Size = New System.Drawing.Size(1100, 52)
        Me.pnlFilters.TabIndex = 1
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(880, 12)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.PlaceholderText = "Search text…"
        Me.txtSearch.Size = New System.Drawing.Size(208, 23)
        Me.txtSearch.TabIndex = 12
        '
        'lblSearch
        '
        Me.lblSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(829, 16)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(45, 15)
        Me.lblSearch.TabIndex = 11
        Me.lblSearch.Text = "Search"
        '
        'cboSeverity
        '
        Me.cboSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSeverity.FormattingEnabled = True
        Me.cboSeverity.Items.AddRange(New Object() {"All", "Info", "Warning", "Error", "Critical"})
        Me.cboSeverity.Location = New System.Drawing.Point(470, 12)
        Me.cboSeverity.Name = "cboSeverity"
        Me.cboSeverity.Size = New System.Drawing.Size(90, 23)
        Me.cboSeverity.TabIndex = 6
        '
        'lblSeverity
        '
        Me.lblSeverity.AutoSize = True
        Me.lblSeverity.Location = New System.Drawing.Point(414, 16)
        Me.lblSeverity.Name = "lblSeverity"
        Me.lblSeverity.Size = New System.Drawing.Size(50, 15)
        Me.lblSeverity.TabIndex = 5
        Me.lblSeverity.Text = "Severity"
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.FormattingEnabled = True
        Me.cboType.Items.AddRange(New Object() {"All", "Alert", "Order", "Connection", "Risk", "System"})
        Me.cboType.Location = New System.Drawing.Point(314, 12)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(90, 23)
        Me.cboType.TabIndex = 4
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(272, 16)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(33, 15)
        Me.lblType.TabIndex = 3
        Me.lblType.Text = "Type"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(12, 16)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(39, 15)
        Me.lblFrom.TabIndex = 0
        Me.lblFrom.Text = "From"
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "yyyy-MM-dd HH:mm"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(57, 12)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(120, 23)
        Me.dtpFrom.TabIndex = 1
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(183, 16)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(21, 15)
        Me.lblTo.TabIndex = 2
        Me.lblTo.Text = "To"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "yyyy-MM-dd HH:mm"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(210, 12)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(120, 23)
        Me.dtpTo.TabIndex = 3
        '
        'chkOnlyActive
        '
        Me.chkOnlyActive.AutoSize = True
        Me.chkOnlyActive.Location = New System.Drawing.Point(570, 14)
        Me.chkOnlyActive.Name = "chkOnlyActive"
        Me.chkOnlyActive.Size = New System.Drawing.Size(89, 19)
        Me.chkOnlyActive.TabIndex = 7
        Me.chkOnlyActive.Text = "Only active"
        Me.chkOnlyActive.UseVisualStyleBackColor = True
        '
        'chkPopup
        '
        Me.chkPopup.AutoSize = True
        Me.chkPopup.Location = New System.Drawing.Point(757, 14)
        Me.chkPopup.Name = "chkPopup"
        Me.chkPopup.Size = New System.Drawing.Size(61, 19)
        Me.chkPopup.TabIndex = 10
        Me.chkPopup.Text = "Popup"
        Me.chkPopup.UseVisualStyleBackColor = True
        '
        'chkSound
        '
        Me.chkSound.AutoSize = True
        Me.chkSound.Location = New System.Drawing.Point(665, 14)
        Me.chkSound.Name = "chkSound"
        Me.chkSound.Size = New System.Drawing.Size(58, 19)
        Me.chkSound.TabIndex = 8
        Me.chkSound.Text = "Sound"
        Me.chkSound.UseVisualStyleBackColor = True
        '
        'splitMain
        '
        Me.splitMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMain.Location = New System.Drawing.Point(0, 79)
        Me.splitMain.Name = "splitMain"
        Me.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitMain.Panel1
        '
        Me.splitMain.Panel1.Controls.Add(Me.grpAlerts)
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.grpActivity)
        Me.splitMain.Size = New System.Drawing.Size(1100, 521)
        Me.splitMain.SplitterDistance = 260
        Me.splitMain.TabIndex = 2
        '
        'grpAlerts
        '
        Me.grpAlerts.Controls.Add(Me.dgvAlerts)
        Me.grpAlerts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpAlerts.Location = New System.Drawing.Point(0, 0)
        Me.grpAlerts.Name = "grpAlerts"
        Me.grpAlerts.Padding = New System.Windows.Forms.Padding(6)
        Me.grpAlerts.Size = New System.Drawing.Size(1100, 260)
        Me.grpAlerts.TabIndex = 0
        Me.grpAlerts.TabStop = False
        Me.grpAlerts.Text = "Active Alerts"
        '
        'dgvAlerts
        '
        Me.dgvAlerts.AllowUserToAddRows = False
        Me.dgvAlerts.AllowUserToDeleteRows = False
        Me.dgvAlerts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvAlerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAlerts.ContextMenuStrip = Me.ctxAlerts
        Me.dgvAlerts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAlerts.Location = New System.Drawing.Point(6, 22)
        Me.dgvAlerts.MultiSelect = False
        Me.dgvAlerts.Name = "dgvAlerts"
        Me.dgvAlerts.ReadOnly = True
        Me.dgvAlerts.RowHeadersVisible = False
        Me.dgvAlerts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAlerts.Size = New System.Drawing.Size(1088, 232)
        Me.dgvAlerts.TabIndex = 0
        '
        'grpActivity
        '
        Me.grpActivity.Controls.Add(Me.dgvActivity)
        Me.grpActivity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpActivity.Location = New System.Drawing.Point(0, 0)
        Me.grpActivity.Name = "grpActivity"
        Me.grpActivity.Padding = New System.Windows.Forms.Padding(6)
        Me.grpActivity.Size = New System.Drawing.Size(1100, 257)
        Me.grpActivity.TabIndex = 0
        Me.grpActivity.TabStop = False
        Me.grpActivity.Text = "Activity Log"
        '
        'dgvActivity
        '
        Me.dgvActivity.AllowUserToAddRows = False
        Me.dgvActivity.AllowUserToDeleteRows = False
        Me.dgvActivity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvActivity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvActivity.Location = New System.Drawing.Point(6, 22)
        Me.dgvActivity.MultiSelect = False
        Me.dgvActivity.Name = "dgvActivity"
        Me.dgvActivity.ReadOnly = True
        Me.dgvActivity.RowHeadersVisible = False
        Me.dgvActivity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvActivity.Size = New System.Drawing.Size(1088, 229)
        Me.dgvActivity.TabIndex = 0
        '
        'ctxAlerts
        '
        Me.ctxAlerts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ctxAck, Me.ctxDismiss, Me.ctxCopy})
        Me.ctxAlerts.Name = "ctxAlerts"
        Me.ctxAlerts.Size = New System.Drawing.Size(140, 70)
        '
        'ctxAck
        '
        Me.ctxAck.Name = "ctxAck"
        Me.ctxAck.Size = New System.Drawing.Size(139, 22)
        Me.ctxAck.Text = "Acknowledge"
        '
        'ctxDismiss
        '
        Me.ctxDismiss.Name = "ctxDismiss"
        Me.ctxDismiss.Size = New System.Drawing.Size(139, 22)
        Me.ctxDismiss.Text = "Dismiss"
        '
        'ctxCopy
        '
        Me.ctxCopy.Name = "ctxCopy"
        Me.ctxCopy.Size = New System.Drawing.Size(139, 22)
        Me.ctxCopy.Text = "Copy"
        '
        'status
        '
        Me.status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblCounts, Me.lblUpdated})
        Me.status.Location = New System.Drawing.Point(0, 600)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(1100, 22)
        Me.status.TabIndex = 3
        '
        'lblCounts
        '
        Me.lblCounts.Name = "lblCounts"
        Me.lblCounts.Size = New System.Drawing.Size(167, 17)
        Me.lblCounts.Text = "Active: 0 • Activity rows: 0"
        '
        'lblUpdated
        '
        Me.lblUpdated.Name = "lblUpdated"
        Me.lblUpdated.Size = New System.Drawing.Size(162, 17)
        Me.lblUpdated.Text = "Last update: (not refreshed)"
        '
        'AlertsActivity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 622)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.pnlFilters)
        Me.Controls.Add(Me.tsTop)
        Me.Controls.Add(Me.status)
        Me.MinimumSize = New System.Drawing.Size(900, 500)
        Me.Name = "AlertsActivity"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Alerts & Activity"
        Me.tsTop.ResumeLayout(False)
        Me.tsTop.PerformLayout()
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.grpAlerts.ResumeLayout(False)
        CType(Me.dgvAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpActivity.ResumeLayout(False)
        CType(Me.dgvActivity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ctxAlerts.ResumeLayout(False)
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tsTop As ToolStrip
    Friend WithEvents btnAck As ToolStripButton
    Friend WithEvents btnSnooze As ToolStripDropDownButton
    Friend WithEvents miSnooze5 As ToolStripMenuItem
    Friend WithEvents miSnooze15 As ToolStripMenuItem
    Friend WithEvents miSnooze60 As ToolStripMenuItem
    Friend WithEvents btnDismiss As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnNewRule As ToolStripButton
    Friend WithEvents btnEditRule As ToolStripButton
    Friend WithEvents btnDeleteRule As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnTestSound As ToolStripButton
    Friend WithEvents btnExportCsv As ToolStripButton
    Friend WithEvents pnlFilters As Panel
    Friend WithEvents lblFrom As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents lblTo As Label
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents cboType As ComboBox
    Friend WithEvents lblType As Label
    Friend WithEvents cboSeverity As ComboBox
    Friend WithEvents lblSeverity As Label
    Friend WithEvents chkOnlyActive As CheckBox
    Friend WithEvents chkPopup As CheckBox
    Friend WithEvents chkSound As CheckBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents grpAlerts As GroupBox
    Friend WithEvents dgvAlerts As DataGridView
    Friend WithEvents grpActivity As GroupBox
    Friend WithEvents dgvActivity As DataGridView
    Friend WithEvents ctxAlerts As ContextMenuStrip
    Friend WithEvents ctxAck As ToolStripMenuItem
    Friend WithEvents ctxDismiss As ToolStripMenuItem
    Friend WithEvents ctxCopy As ToolStripMenuItem
    Friend WithEvents status As StatusStrip
    Friend WithEvents lblCounts As ToolStripStatusLabel
    Friend WithEvents lblUpdated As ToolStripStatusLabel
End Class
