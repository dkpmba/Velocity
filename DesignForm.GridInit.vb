Partial Class DesignForm

    Private Sub DesignForm_GridInit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Me.dgvDesign IsNot Nothing Then
                GridColumns.BuildDesignColumns(Me.dgvDesign)
                GridColumns.RestoreGridLayout(Me.dgvDesign, "Design_Legs")
            End If
        Catch
        End Try
    End Sub

    Private Sub DesignForm_GridInit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try : GridColumns.SaveGridLayout(Me.dgvDesign, "Design_Legs") : Catch : End Try
    End Sub

End Class
