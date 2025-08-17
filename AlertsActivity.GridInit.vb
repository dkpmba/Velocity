Partial Class AlertsActivity

    Private Sub AlertsActivity_GridInit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Me.dgvAlerts IsNot Nothing Then
                GridColumns.BuildAlertsColumns(Me.dgvAlerts)
                GridColumns.RestoreGridLayout(Me.dgvAlerts, "Alerts_Active")
            End If
        Catch
        End Try
        Try
            If Me.dgvActivity IsNot Nothing Then
                GridColumns.BuildActivityColumns(Me.dgvActivity)
                GridColumns.RestoreGridLayout(Me.dgvActivity, "Alerts_Activity")
            End If
        Catch
        End Try
    End Sub

    Private Sub AlertsActivity_GridInit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try : GridColumns.SaveGridLayout(Me.dgvAlerts, "Alerts_Active") : Catch : End Try
        Try : GridColumns.SaveGridLayout(Me.dgvActivity, "Alerts_Activity") : Catch : End Try
    End Sub

End Class
