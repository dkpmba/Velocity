Public Partial Class OptionChainViewer
    Inherits Form

    Public Sub New()
        InitializeComponent()
        ' UI-only scaffold. No data wiring yet.
    End Sub

    Private Sub OptionChainViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tsCboGroupBy.SelectedItem = "None"
        nudDeltaMin.Value = 10D
        nudDeltaMax.Value = 90D
        chkITM.Checked = True
        chkATM.Checked = True
        chkOTM.Checked = True
        nudMaxSpreadPct.Value = 25D

        UpdateSummary(0, 0)
        lblLastUpdate.Text = "Updated: —"
    End Sub

    Private Sub UpdateSummary(calls As Integer, puts As Integer)
        lblSummary.Text = $"{calls} calls • {puts} puts (filtered / total)"
    End Sub
End Class
