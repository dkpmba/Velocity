Partial Class TradeLog

    Private Sub TradeLog_GridInit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Me.dgvSummary IsNot Nothing Then
                GridColumns.BuildTradeLogColumns(Me.dgvSummary)
                GridColumns.RestoreGridLayout(Me.dgvSummary, "TradeLog_Summary")
            End If
        Catch
        End Try
    End Sub

    Private Sub TradeLog_GridInit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try : GridColumns.SaveGridLayout(Me.dgvSummary, "TradeLog_Summary") : Catch : End Try
    End Sub

End Class
