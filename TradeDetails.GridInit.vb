Partial Class TradeDetails

    Private Sub TradeDetails_GridInit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Me.dgvTransactions IsNot Nothing Then
                GridColumns.BuildTradeDetailsColumns(Me.dgvTransactions)
                GridColumns.RestoreGridLayout(Me.dgvTransactions, "TradeDetails_Tx")
            End If
        Catch
        End Try
    End Sub

    Private Sub TradeDetails_GridInit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try : GridColumns.SaveGridLayout(Me.dgvTransactions, "TradeDetails_Tx") : Catch : End Try
    End Sub

End Class
