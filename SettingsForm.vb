Public Partial Class SettingsForm
    Inherits Form

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboLogLevel.SelectedIndex = 2 ' Info
        cboTheme.SelectedIndex = 0 ' System
        cboTif.SelectedIndex = 0 ' DAY
        nudDefaultSize.Value = 1
        nudPacing.Value = 50
        nudPosPerSym.Value = 100
        lblHint.Text = "Changes apply on OK or Apply."
    End Sub

    Private Sub btnBrowseFolder_Click(sender As Object, e As EventArgs) Handles btnBrowseFolder.Click
        If dlgFolder.ShowDialog(Me) = DialogResult.OK Then
            txtDataFolder.Text = dlgFolder.SelectedPath
        End If
    End Sub

    Private Sub btnBrowseSound_Click(sender As Object, e As EventArgs) Handles btnBrowseSound.Click
        If dlgOpen.ShowDialog(Me) = DialogResult.OK Then
            txtSoundPath.Text = dlgOpen.FileName
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        SaveSettings()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        SaveSettings()
        lblHint.Text = "Saved at " & DateTime.Now.ToString("HH:mm:ss")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' Persist your settings here (stub)
    Private Sub SaveSettings()
        ' TODO: wire to your actual settings storage (e.g., JSON file)
        ' Example of reading values:
        Dim connectAtStart = chkConnectStartup.Checked
        Dim restoreLayout = chkRestoreLayout.Checked
        Dim theme = TryCast(cboTheme.SelectedItem, String)
        Dim logLevel = TryCast(cboLogLevel.SelectedItem, String)
        Dim defAccount = TryCast(cboAccount.SelectedItem, String)
        Dim defSize = CInt(nudDefaultSize.Value)
        Dim tif = TryCast(cboTif.SelectedItem, String)
        Dim confirmPlace = chkConfirmPlace.Checked
        Dim confirmCancel = chkConfirmCancel.Checked
        Dim dailyLoss = nudDailyLoss.Value
        Dim tradeLoss = nudTradeLoss.Value
        Dim posPerSym = CInt(nudPosPerSym.Value)
        Dim stopOnLimit = chkStopOnLimit.Checked
        Dim rthOnly = chkRthOnly.Checked
        Dim streamQuotes = chkStreamQuotes.Checked
        Dim pacing = CInt(nudPacing.Value)
        Dim popup = chkPopup.Checked
        Dim sound = chkSound.Checked
        Dim soundPath = txtSoundPath.Text.Trim()
        Dim email = txtEmail.Text.Trim()
        Dim dataFolder = txtDataFolder.Text.Trim()
        ' No-op: just a stub so you can wire your config later.
    End Sub
End Class
