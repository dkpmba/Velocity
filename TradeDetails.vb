Public Partial Class TradeDetails
    Inherits Form

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub TradeDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Default filter to the last 7 days
        dtpFrom.Value = Date.Now.AddDays(-7)
        dtpTo.Value = Date.Now

        ' Configure chart
        SetupPnLChart()

        ' Default summary
        lblSummary.Text = "TID: —" & vbCrLf &
                          "Symbol: —" & vbCrLf &
                          "Structure: —" & vbCrLf &
                          "Size: —" & vbCrLf &
                          "Credit: —" & vbCrLf &
                          "URGL: —" & vbCrLf &
                          "RGL: —" & vbCrLf &
                          "Net Δ/Γ/Θ/V: —"
        UpdateStatus(0, 0, 0)
    End Sub

    Private Sub SetupPnLChart()
        Dim ch = chartPnL
        ch.Series.Clear()
        ch.ChartAreas.Clear()
        ch.Legends.Clear()
        ch.Titles.Clear()

        Dim area = New System.Windows.Forms.DataVisualization.Charting.ChartArea("area")
        area.AxisX.MajorGrid.Enabled = False
        area.AxisX.Title = "Time"
        area.AxisY.Title = "PnL"
        ch.ChartAreas.Add(area)

        Dim sCum = New System.Windows.Forms.DataVisualization.Charting.Series("CumulativePnL")
        sCum.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        sCum.BorderWidth = 2
        sCum.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
        ch.Series.Add(sCum)

        ch.Titles.Add("Per-Trade Cumulative PnL")
    End Sub

    Private Sub UpdateStatus(rows As Integer, gross As Double, fees As Double)
        lblRows.Text = $"Rows: {rows}"
        Dim net = gross - fees
        lblGrossNet.Text = $"Gross: {gross:C0} • Fees: {fees:C0} • Net: {net:C0}"
    End Sub
End Class
