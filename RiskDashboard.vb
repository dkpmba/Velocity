
Partial Public Class RiskDashboard
    Inherits Form

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub RiskDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize KPI defaults
        lblNetDelta.Text = "0.0"
        lblGamma.Text = "0.0"
        lblTheta.Text = "0.0"
        lblVega.Text = "0.0"
        lblMarginUsed.Text = "$0"
        lblMarginFree.Text = "$0"
        lblDailyPnL.Text = "$0"
        lblMaxDD.Text = "$0"

        ' Minimal chart scaffolding
        SetupChart(chartExposure, "Exposure", "Symbol/Expiry", "Value")
        SetupChart(chartBuckets, "Greek Buckets", "Bucket", "Value")
        SetupChart(chartConcentration, "Concentration", "Symbol", "Risk")

        lblSummary.Text = "Exposure rows: 0 • Limits near breach: 0"
    End Sub

    Private Sub SetupChart(ch As System.Windows.Forms.DataVisualization.Charting.Chart, title As String, xTitle As String, yTitle As String)
        ch.Series.Clear()
        ch.ChartAreas.Clear()
        ch.Titles.Clear()

        Dim area = New System.Windows.Forms.DataVisualization.Charting.ChartArea("area")
        area.AxisX.Title = xTitle
        area.AxisY.Title = yTitle
        ch.ChartAreas.Add(area)

        Dim series = New System.Windows.Forms.DataVisualization.Charting.Series("series")
        series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column
        ch.Series.Add(series)

        ch.Titles.Add(title)
    End Sub
End Class
