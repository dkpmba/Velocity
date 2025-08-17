<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DesignForm
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
        Me.lblTsAccount = New System.Windows.Forms.ToolStripLabel()
        Me.tsCboAccount = New System.Windows.Forms.ToolStripComboBox()
        Me.lblTsExpiry = New System.Windows.Forms.ToolStripLabel()
        Me.tsCboExpiry = New System.Windows.Forms.ToolStripComboBox()
        Me.lblTsStructure = New System.Windows.Forms.ToolStripLabel()
        Me.tsCboStructure = New System.Windows.Forms.ToolStripComboBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblLiquidity = New System.Windows.Forms.Label()
        Me.lblInsuranceRatio = New System.Windows.Forms.Label()
        Me.tbInsuranceRatio = New System.Windows.Forms.TrackBar()
        Me.chkInsurance = New System.Windows.Forms.CheckBox()
        Me.pnlValidation = New System.Windows.Forms.Panel()
        Me.lblValidation = New System.Windows.Forms.Label()
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.grpChainPicker = New System.Windows.Forms.GroupBox()
        Me.pnlChainInner = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnATMStraddle = New System.Windows.Forms.Button()
        Me.btnATMStrangle = New System.Windows.Forms.Button()
        Me.lblDeltaRange = New System.Windows.Forms.Label()
        Me.nudDeltaMin = New System.Windows.Forms.NumericUpDown()
        Me.nudDeltaMax = New System.Windows.Forms.NumericUpDown()
        Me.pnlStrikeNudge = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnStrikeDown = New System.Windows.Forms.Button()
        Me.btnStrikeUp = New System.Windows.Forms.Button()
        Me.dgvDesign = New System.Windows.Forms.DataGridView()
        Me.pnlSummary = New System.Windows.Forms.Panel()
        Me.lblSummary = New System.Windows.Forms.Label()
        Me.lblNetCredit = New System.Windows.Forms.Label()
        Me.lblGreeks = New System.Windows.Forms.Label()
        Me.lblMargin = New System.Windows.Forms.Label()
        Me.lblSlippage = New System.Windows.Forms.Label()
        Me.btnValidate = New System.Windows.Forms.Button()
        Me.btnSaveTemplate = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnPlace = New System.Windows.Forms.Button()
        Me.tsTop.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.tbInsuranceRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlValidation.SuspendLayout()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.grpChainPicker.SuspendLayout()
        Me.pnlChainInner.SuspendLayout()
        CType(Me.nudDeltaMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudDeltaMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStrikeNudge.SuspendLayout()
        CType(Me.dgvDesign, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSummary.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsTop
        '
        Me.tsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblTsSymbol, Me.tsCboSymbol, Me.lblTsAccount, Me.tsCboAccount, Me.lblTsExpiry, Me.tsCboExpiry, Me.lblTsStructure, Me.tsCboStructure})
        Me.tsTop.Location = New System.Drawing.Point(0, 0)
        Me.tsTop.Name = "tsTop"
        Me.tsTop.Size = New System.Drawing.Size(1000, 27)
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
        'lblTsStructure
        '
        Me.lblTsStructure.Name = "lblTsStructure"
        Me.lblTsStructure.Size = New System.Drawing.Size(62, 24)
        Me.lblTsStructure.Text = "Structure:"
        '
        'tsCboStructure
        '
        Me.tsCboStructure.AutoSize = False
        Me.tsCboStructure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsCboStructure.Items.AddRange(New Object() {"Straddle", "Strangle"})
        Me.tsCboStructure.Name = "tsCboStructure"
        Me.tsCboStructure.Size = New System.Drawing.Size(100, 23)
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblLiquidity)
        Me.pnlHeader.Controls.Add(Me.lblInsuranceRatio)
        Me.pnlHeader.Controls.Add(Me.tbInsuranceRatio)
        Me.pnlHeader.Controls.Add(Me.chkInsurance)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 27)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.pnlHeader.Size = New System.Drawing.Size(1000, 54)
        Me.pnlHeader.TabIndex = 1
        '
        'lblLiquidity
        '
        Me.lblLiquidity.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLiquidity.BackColor = System.Drawing.Color.Gainsboro
        Me.lblLiquidity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLiquidity.Location = New System.Drawing.Point(860, 10)
        Me.lblLiquidity.Name = "lblLiquidity"
        Me.lblLiquidity.Padding = New System.Windows.Forms.Padding(6, 4, 6, 4)
        Me.lblLiquidity.Size = New System.Drawing.Size(128, 32)
        Me.lblLiquidity.TabIndex = 3
        Me.lblLiquidity.Text = "Liquidity: —"
        Me.lblLiquidity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInsuranceRatio
        '
        Me.lblInsuranceRatio.AutoSize = True
        Me.lblInsuranceRatio.Location = New System.Drawing.Point(320, 18)
        Me.lblInsuranceRatio.Name = "lblInsuranceRatio"
        Me.lblInsuranceRatio.Size = New System.Drawing.Size(49, 15)
        Me.lblInsuranceRatio.TabIndex = 2
        Me.lblInsuranceRatio.Text = "Ratio: 0"
        '
        'tbInsuranceRatio
        '
        Me.tbInsuranceRatio.AutoSize = False
        Me.tbInsuranceRatio.Location = New System.Drawing.Point(142, 13)
        Me.tbInsuranceRatio.Maximum = 50
        Me.tbInsuranceRatio.Name = "tbInsuranceRatio"
        Me.tbInsuranceRatio.Size = New System.Drawing.Size(172, 30)
        Me.tbInsuranceRatio.TabIndex = 1
        Me.tbInsuranceRatio.TickFrequency = 5
        '
        'chkInsurance
        '
        Me.chkInsurance.AutoSize = True
        Me.chkInsurance.Location = New System.Drawing.Point(12, 17)
        Me.chkInsurance.Name = "chkInsurance"
        Me.chkInsurance.Size = New System.Drawing.Size(124, 19)
        Me.chkInsurance.TabIndex = 0
        Me.chkInsurance.Text = "Insurance (20Δ) on"
        Me.chkInsurance.UseVisualStyleBackColor = True
        '
        'pnlValidation
        '
        Me.pnlValidation.BackColor = System.Drawing.SystemColors.Info
        Me.pnlValidation.Controls.Add(Me.lblValidation)
        Me.pnlValidation.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlValidation.Location = New System.Drawing.Point(0, 81)
        Me.pnlValidation.Name = "pnlValidation"
        Me.pnlValidation.Padding = New System.Windows.Forms.Padding(8, 2, 8, 4)
        Me.pnlValidation.Size = New System.Drawing.Size(1000, 26)
        Me.pnlValidation.TabIndex = 2
        '
        'lblValidation
        '
        Me.lblValidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblValidation.Location = New System.Drawing.Point(8, 2)
        Me.lblValidation.Name = "lblValidation"
        Me.lblValidation.Size = New System.Drawing.Size(984, 20)
        Me.lblValidation.TabIndex = 0
        Me.lblValidation.Text = "Ready — no validation issues."
        Me.lblValidation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'splitMain
        '
        Me.splitMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMain.Location = New System.Drawing.Point(0, 107)
        Me.splitMain.Name = "splitMain"
        '
        'splitMain.Panel1
        '
        Me.splitMain.Panel1.Controls.Add(Me.grpChainPicker)
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.dgvDesign)
        Me.splitMain.Size = New System.Drawing.Size(1000, 493)
        Me.splitMain.SplitterDistance = 300
        Me.splitMain.TabIndex = 3
        '
        'grpChainPicker
        '
        Me.grpChainPicker.Controls.Add(Me.pnlChainInner)
        Me.grpChainPicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpChainPicker.Location = New System.Drawing.Point(0, 0)
        Me.grpChainPicker.Name = "grpChainPicker"
        Me.grpChainPicker.Padding = New System.Windows.Forms.Padding(8)
        Me.grpChainPicker.Size = New System.Drawing.Size(300, 493)
        Me.grpChainPicker.TabIndex = 0
        Me.grpChainPicker.TabStop = False
        Me.grpChainPicker.Text = "Chain / Quick Picks"
        '
        'pnlChainInner
        '
        Me.pnlChainInner.AutoScroll = True
        Me.pnlChainInner.Controls.Add(Me.btnATMStraddle)
        Me.pnlChainInner.Controls.Add(Me.btnATMStrangle)
        Me.pnlChainInner.Controls.Add(Me.lblDeltaRange)
        Me.pnlChainInner.Controls.Add(Me.nudDeltaMin)
        Me.pnlChainInner.Controls.Add(Me.nudDeltaMax)
        Me.pnlChainInner.Controls.Add(Me.pnlStrikeNudge)
        Me.pnlChainInner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlChainInner.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.pnlChainInner.Location = New System.Drawing.Point(8, 24)
        Me.pnlChainInner.Name = "pnlChainInner"
        Me.pnlChainInner.Padding = New System.Windows.Forms.Padding(4)
        Me.pnlChainInner.Size = New System.Drawing.Size(284, 461)
        Me.pnlChainInner.TabIndex = 0
        Me.pnlChainInner.WrapContents = False
        '
        'btnATMStraddle
        '
        Me.btnATMStraddle.Location = New System.Drawing.Point(7, 7)
        Me.btnATMStraddle.Name = "btnATMStraddle"
        Me.btnATMStraddle.Size = New System.Drawing.Size(260, 30)
        Me.btnATMStraddle.TabIndex = 0
        Me.btnATMStraddle.Text = "Pick ATM Straddle"
        Me.btnATMStraddle.UseVisualStyleBackColor = True
        '
        'btnATMStrangle
        '
        Me.btnATMStrangle.Location = New System.Drawing.Point(7, 43)
        Me.btnATMStrangle.Name = "btnATMStrangle"
        Me.btnATMStrangle.Size = New System.Drawing.Size(260, 30)
        Me.btnATMStrangle.TabIndex = 1
        Me.btnATMStrangle.Text = "Pick 20Δ Strangle"
        Me.btnATMStrangle.UseVisualStyleBackColor = True
        '
        'lblDeltaRange
        '
        Me.lblDeltaRange.Location = New System.Drawing.Point(7, 79)
        Me.lblDeltaRange.Name = "lblDeltaRange"
        Me.lblDeltaRange.Padding = New System.Windows.Forms.Padding(0, 6, 0, 4)
        Me.lblDeltaRange.Size = New System.Drawing.Size(260, 26)
        Me.lblDeltaRange.TabIndex = 2
        Me.lblDeltaRange.Text = "Δ Filter: Min / Max"
        '
        'nudDeltaMin
        '
        Me.nudDeltaMin.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudDeltaMin.Location = New System.Drawing.Point(7, 108)
        Me.nudDeltaMin.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nudDeltaMin.Name = "nudDeltaMin"
        Me.nudDeltaMin.Size = New System.Drawing.Size(120, 23)
        Me.nudDeltaMin.TabIndex = 3
        Me.nudDeltaMin.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nudDeltaMax
        '
        Me.nudDeltaMax.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.nudDeltaMax.Location = New System.Drawing.Point(133, 108)
        Me.nudDeltaMax.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nudDeltaMax.Name = "nudDeltaMax"
        Me.nudDeltaMax.Size = New System.Drawing.Size(120, 23)
        Me.nudDeltaMax.TabIndex = 4
        Me.nudDeltaMax.Value = New Decimal(New Integer() {90, 0, 0, 0})
        '
        'pnlStrikeNudge
        '
        Me.pnlStrikeNudge.Controls.Add(Me.btnStrikeDown)
        Me.pnlStrikeNudge.Controls.Add(Me.btnStrikeUp)
        Me.pnlStrikeNudge.Location = New System.Drawing.Point(7, 137)
        Me.pnlStrikeNudge.Name = "pnlStrikeNudge"
        Me.pnlStrikeNudge.Size = New System.Drawing.Size(260, 36)
        Me.pnlStrikeNudge.TabIndex = 5
        '
        'btnStrikeDown
        '
        Me.btnStrikeDown.Location = New System.Drawing.Point(3, 3)
        Me.btnStrikeDown.Name = "btnStrikeDown"
        Me.btnStrikeDown.Size = New System.Drawing.Size(120, 30)
        Me.btnStrikeDown.TabIndex = 0
        Me.btnStrikeDown.Text = "− Strike"
        Me.btnStrikeDown.UseVisualStyleBackColor = True
        '
        'btnStrikeUp
        '
        Me.btnStrikeUp.Location = New System.Drawing.Point(129, 3)
        Me.btnStrikeUp.Name = "btnStrikeUp"
        Me.btnStrikeUp.Size = New System.Drawing.Size(120, 30)
        Me.btnStrikeUp.TabIndex = 1
        Me.btnStrikeUp.Text = "+ Strike"
        Me.btnStrikeUp.UseVisualStyleBackColor = True
        '
        'dgvDesign
        '
        Me.dgvDesign.AllowUserToAddRows = False
        Me.dgvDesign.AllowUserToDeleteRows = False
        Me.dgvDesign.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvDesign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDesign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDesign.Location = New System.Drawing.Point(0, 0)
        Me.dgvDesign.MultiSelect = False
        Me.dgvDesign.Name = "dgvDesign"
        Me.dgvDesign.ReadOnly = True
        Me.dgvDesign.RowHeadersVisible = False
        Me.dgvDesign.Size = New System.Drawing.Size(696, 493)
        Me.dgvDesign.TabIndex = 0
        '
        'pnlSummary
        '
        Me.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSummary.Controls.Add(Me.lblSummary)
        Me.pnlSummary.Controls.Add(Me.lblNetCredit)
        Me.pnlSummary.Controls.Add(Me.lblGreeks)
        Me.pnlSummary.Controls.Add(Me.lblMargin)
        Me.pnlSummary.Controls.Add(Me.lblSlippage)
        Me.pnlSummary.Controls.Add(Me.btnValidate)
        Me.pnlSummary.Controls.Add(Me.btnSaveTemplate)
        Me.pnlSummary.Controls.Add(Me.btnClear)
        Me.pnlSummary.Controls.Add(Me.btnPlace)
        Me.pnlSummary.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSummary.Location = New System.Drawing.Point(0, 600)
        Me.pnlSummary.Name = "pnlSummary"
        Me.pnlSummary.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
        Me.pnlSummary.Size = New System.Drawing.Size(1000, 50)
        Me.pnlSummary.TabIndex = 4
        '
        'lblSummary
        '
        Me.lblSummary.AutoSize = True
        Me.lblSummary.Location = New System.Drawing.Point(12, 16)
        Me.lblSummary.Name = "lblSummary"
        Me.lblSummary.Size = New System.Drawing.Size(63, 15)
        Me.lblSummary.TabIndex = 0
        Me.lblSummary.Text = "Summary:"
        '
        'lblNetCredit
        '
        Me.lblNetCredit.AutoSize = True
        Me.lblNetCredit.Location = New System.Drawing.Point(90, 16)
        Me.lblNetCredit.Name = "lblNetCredit"
        Me.lblNetCredit.Size = New System.Drawing.Size(68, 15)
        Me.lblNetCredit.TabIndex = 1
        Me.lblNetCredit.Text = "Credit: $0.0"
        '
        'lblGreeks
        '
        Me.lblGreeks.AutoSize = True
        Me.lblGreeks.Location = New System.Drawing.Point(190, 16)
        Me.lblGreeks.Name = "lblGreeks"
        Me.lblGreeks.Size = New System.Drawing.Size(94, 15)
        Me.lblGreeks.TabIndex = 2
        Me.lblGreeks.Text = "Δ/Γ/Θ/V: 0/0/0/0"
        '
        'lblMargin
        '
        Me.lblMargin.AutoSize = True
        Me.lblMargin.Location = New System.Drawing.Point(310, 16)
        Me.lblMargin.Name = "lblMargin"
        Me.lblMargin.Size = New System.Drawing.Size(86, 15)
        Me.lblMargin.TabIndex = 3
        Me.lblMargin.Text = "Margin: $0.00"
        '
        'lblSlippage
        '
        Me.lblSlippage.AutoSize = True
        Me.lblSlippage.Location = New System.Drawing.Point(420, 16)
        Me.lblSlippage.Name = "lblSlippage"
        Me.lblSlippage.Size = New System.Drawing.Size(82, 15)
        Me.lblSlippage.TabIndex = 4
        Me.lblSlippage.Text = "Slip: $0.00 est."
        '
        'btnValidate
        '
        Me.btnValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnValidate.Location = New System.Drawing.Point(620, 10)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(85, 28)
        Me.btnValidate.TabIndex = 5
        Me.btnValidate.Text = "Validate"
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'btnSaveTemplate
        '
        Me.btnSaveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveTemplate.Location = New System.Drawing.Point(711, 10)
        Me.btnSaveTemplate.Name = "btnSaveTemplate"
        Me.btnSaveTemplate.Size = New System.Drawing.Size(110, 28)
        Me.btnSaveTemplate.TabIndex = 6
        Me.btnSaveTemplate.Text = "Save Template"
        Me.btnSaveTemplate.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(827, 10)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(70, 28)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnPlace
        '
        Me.btnPlace.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPlace.Location = New System.Drawing.Point(903, 10)
        Me.btnPlace.Name = "btnPlace"
        Me.btnPlace.Size = New System.Drawing.Size(85, 28)
        Me.btnPlace.TabIndex = 8
        Me.btnPlace.Text = "Place"
        Me.btnPlace.UseVisualStyleBackColor = True
        '
        'DesignForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 650)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.pnlValidation)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.tsTop)
        Me.Controls.Add(Me.pnlSummary)
        Me.MinimumSize = New System.Drawing.Size(900, 600)
        Me.Name = "DesignForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Trade Designer"
        Me.tsTop.ResumeLayout(False)
        Me.tsTop.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.tbInsuranceRatio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlValidation.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        Me.grpChainPicker.ResumeLayout(False)
        Me.pnlChainInner.ResumeLayout(False)
        CType(Me.nudDeltaMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudDeltaMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStrikeNudge.ResumeLayout(False)
        CType(Me.dgvDesign, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSummary.ResumeLayout(False)
        Me.pnlSummary.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tsTop As ToolStrip
    Friend WithEvents lblTsSymbol As ToolStripLabel
    Friend WithEvents tsCboSymbol As ToolStripComboBox
    Friend WithEvents lblTsAccount As ToolStripLabel
    Friend WithEvents tsCboAccount As ToolStripComboBox
    Friend WithEvents lblTsExpiry As ToolStripLabel
    Friend WithEvents tsCboExpiry As ToolStripComboBox
    Friend WithEvents lblTsStructure As ToolStripLabel
    Friend WithEvents tsCboStructure As ToolStripComboBox
    Friend WithEvents pnlHeader As Panel
    Friend WithEvents chkInsurance As CheckBox
    Friend WithEvents tbInsuranceRatio As TrackBar
    Friend WithEvents lblInsuranceRatio As Label
    Friend WithEvents lblLiquidity As Label
    Friend WithEvents pnlValidation As Panel
    Friend WithEvents lblValidation As Label
    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents grpChainPicker As GroupBox
    Friend WithEvents pnlChainInner As FlowLayoutPanel
    Friend WithEvents btnATMStraddle As Button
    Friend WithEvents btnATMStrangle As Button
    Friend WithEvents lblDeltaRange As Label
    Friend WithEvents nudDeltaMin As NumericUpDown
    Friend WithEvents nudDeltaMax As NumericUpDown
    Friend WithEvents pnlStrikeNudge As FlowLayoutPanel
    Friend WithEvents btnStrikeDown As Button
    Friend WithEvents btnStrikeUp As Button
    Friend WithEvents dgvDesign As DataGridView
    Friend WithEvents pnlSummary As Panel
    Friend WithEvents lblSummary As Label
    Friend WithEvents lblNetCredit As Label
    Friend WithEvents lblGreeks As Label
    Friend WithEvents lblMargin As Label
    Friend WithEvents lblSlippage As Label
    Friend WithEvents btnValidate As Button
    Friend WithEvents btnSaveTemplate As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnPlace As Button
End Class
