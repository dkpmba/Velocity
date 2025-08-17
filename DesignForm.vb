Public Partial Class DesignForm
    Inherits Form

    Public Sub New()
        InitializeComponent()
        ' UI-only scaffold. No strategy logic yet.
    End Sub

    Private Sub DesignForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Minimal defaults for UI readability
        lblValidation.Text = "Ready — no validation issues."
        chkInsurance.Checked = True
        tbInsuranceRatio.Value = 20 ' represents 0.20×
        UpdateInsuranceRatioLabel()
    End Sub

    Private Sub UpdateInsuranceRatioLabel()
        Dim ratio As Double = tbInsuranceRatio.Value / 100.0R
        lblInsuranceRatio.Text = $"Ratio: {ratio:0.00}×"
    End Sub

    Private Sub tbInsuranceRatio_Scroll(sender As Object, e As EventArgs) Handles tbInsuranceRatio.Scroll
        UpdateInsuranceRatioLabel()
    End Sub
End Class
