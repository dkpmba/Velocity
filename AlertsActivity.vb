Public Partial Class AlertsActivity
    Inherits Form

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub AlertsActivity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFrom.Value = Date.Now.AddHours(-4)
        dtpTo.Value = Date.Now
        cboType.SelectedIndex = 0
        cboSeverity.SelectedIndex = 0
        chkOnlyActive.Checked = True
        miSnooze5.Tag = 5 : miSnooze15.Tag = 15 : miSnooze60.Tag = 60
        UpdateStatus()
    End Sub

    Private Sub UpdateStatus()
        lblCounts.Text = $"Active: {dgvAlerts.Rows.Count} • Activity rows: {dgvActivity.Rows.Count}"
        lblUpdated.Text = "Last update: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Sub

    ' Top actions (stubs)
    Private Sub btnAck_Click(sender As Object, e As EventArgs) Handles btnAck.Click, ctxAck.Click
        If dgvAlerts.SelectedRows.Count > 0 Then
            ' TODO: mark selected alert(s) as acknowledged
        End If
        UpdateStatus()
    End Sub

    Private Sub btnDismiss_Click(sender As Object, e As EventArgs) Handles btnDismiss.Click, ctxDismiss.Click
        If dgvAlerts.SelectedRows.Count > 0 Then
            ' TODO: dismiss selected alert(s)
        End If
        UpdateStatus()
    End Sub

    Private Sub btnTestSound_Click(sender As Object, e As EventArgs) Handles btnTestSound.Click
        ' TODO: play test notification sound
    End Sub

    Private Sub btnExportCsv_Click(sender As Object, e As EventArgs) Handles btnExportCsv.Click
        ' TODO: export visible alerts + activity to CSV
    End Sub

    Private Sub Snooze_Click(sender As Object, e As EventArgs) Handles miSnooze5.Click, miSnooze15.Click, miSnooze60.Click
        Dim minutes As Integer = 5
        Dim item = TryCast(sender, ToolStripItem)
        If item IsNot Nothing AndAlso TypeOf item.Tag Is Integer Then
            minutes = CInt(item.Tag)
        ElseIf item IsNot Nothing Then
            If item.Name = miSnooze15.Name Then minutes = 15
            If item.Name = miSnooze60.Name Then minutes = 60
        End If
        ' TODO: snooze selected alert(s) for 'minutes'
        UpdateStatus()
    End Sub

    Private Sub ctxCopy_Click(sender As Object, e As EventArgs) Handles ctxCopy.Click
        If dgvAlerts.SelectedRows.Count > 0 Then
            Dim s As String = ""
            For Each r As DataGridViewRow In dgvAlerts.SelectedRows
                s &= String.Join(vbTab, r.Cells.Cast(Of DataGridViewCell).Select(Function(c) If(c.Value Is Nothing, "", c.Value.ToString()))) & vbCrLf
            Next
            Clipboard.SetText(s)
        End If
    End Sub
End Class
