Partial Class MainForm

    Private Const ALERTS_MAX As Integer = 2000

    Public Sub AppendAlert(message As String, Optional level As String = "INFO")
        If Me.IsHandleCreated AndAlso Me.InvokeRequired Then
            Me.BeginInvoke(Sub() AppendAlert(message, level))
            Return
        End If

        Dim lb As ListBox = Nothing
        ' Use the field if it exists, otherwise find by name
        Try
            lb = lstAlerts
        Catch
        End Try
        If lb Is Nothing Then
            lb = TryCast(Me.Controls.Find("lstAlerts", True).FirstOrDefault(), ListBox)
        End If

        Dim line As String = String.Format("{0:HH:mm:ss} [{1}] {2}", DateTime.Now, level, message)

        If lb IsNot Nothing Then
            lb.Items.Add(line)
            If lb.Items.Count > ALERTS_MAX Then lb.Items.RemoveAt(0)
            lb.TopIndex = lb.Items.Count - 1
        Else
            System.Diagnostics.Debug.WriteLine(line)
        End If
    End Sub

    Private Sub MainForm_GridInit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Me.dgvMonitor IsNot Nothing Then
                GridColumns.BuildMonitorColumns(Me.dgvMonitor)
                GridColumns.RestoreGridLayout(Me.dgvMonitor, "Main_Monitor")
            End If
        Catch
        End Try
        Try
            If Me.dgvSymbols IsNot Nothing Then
                GridColumns.BuildSymbolsColumns(Me.dgvSymbols)
                GridColumns.RestoreGridLayout(Me.dgvSymbols, "Main_Symbols")
            End If
        Catch
        End Try
        Try
            If Me.dgvOrders IsNot Nothing Then
                GridColumns.BuildOrdersColumns(Me.dgvOrders)
                GridColumns.RestoreGridLayout(Me.dgvOrders, "Main_Orders")
            End If
        Catch
        End Try
        Try
            If Me.dgvTrades IsNot Nothing Then
                GridColumns.BuildTradesColumns(Me.dgvTrades)
                GridColumns.RestoreGridLayout(Me.dgvTrades, "Main_Trades")
            End If
        Catch
        End Try
    End Sub

    Private Sub MainForm_GridInit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try : GridColumns.SaveGridLayout(Me.dgvMonitor, "Main_Monitor") : Catch : End Try
        Try : GridColumns.SaveGridLayout(Me.dgvSymbols, "Main_Symbols") : Catch : End Try
        Try : GridColumns.SaveGridLayout(Me.dgvOrders, "Main_Orders") : Catch : End Try
        Try : GridColumns.SaveGridLayout(Me.dgvTrades, "Main_Trades") : Catch : End Try
    End Sub

End Class
