Partial Class OptionChainViewer

    Private Sub OptionChainViewer_GridInit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Me.dgvCalls IsNot Nothing Then
                GridColumns.BuildOptionChainColumns(Me.dgvCalls)
                GridColumns.RestoreGridLayout(Me.dgvCalls, "Chain_Calls")
            End If
        Catch
        End Try
        Try
            If Me.dgvPuts IsNot Nothing Then
                GridColumns.BuildOptionChainColumns(Me.dgvPuts)
                GridColumns.RestoreGridLayout(Me.dgvPuts, "Chain_Puts")
            End If
        Catch
        End Try
    End Sub

    Private Sub OptionChainViewer_GridInit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try : GridColumns.SaveGridLayout(Me.dgvCalls, "Chain_Calls") : Catch : End Try
        Try : GridColumns.SaveGridLayout(Me.dgvPuts, "Chain_Puts") : Catch : End Try
    End Sub

End Class
