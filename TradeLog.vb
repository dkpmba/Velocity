Public Partial Class TradeLog
    Inherits Form

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub TradeLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Sensible defaults
        dtpFrom.Value = Date.Today.AddDays(-30)
        dtpTo.Value = Date.Today

        ' Chart setup
        SetupEquityChart()
        UpdateStatus(0, 0D, 0D)
    End Sub

    Private Sub SetupEquityChart()
        Dim ch = chartEquity

        ch.Series.Clear()
        ch.ChartAreas.Clear()
        ch.Legends.Clear()
        ch.Titles.Clear()

        Dim area = New System.Windows.Forms.DataVisualization.Charting.ChartArea("area")
        area.AxisX.MajorGrid.Enabled = False
        area.AxisX.Title = "Date/Time"
        area.AxisY.Title = "PnL / Equity"
        ch.ChartAreas.Add(area)

        Dim sCum = New System.Windows.Forms.DataVisualization.Charting.Series("CumulativePnL")
        sCum.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        sCum.BorderWidth = 2
        sCum.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
        ch.Series.Add(sCum)

        Dim sEq = New System.Windows.Forms.DataVisualization.Charting.Series("Equity")
        sEq.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        sEq.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
        sEq.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
        ch.Series.Add(sEq)

        ch.Titles.Add("Cumulative PnL & Equity Line")
    End Sub

    Private Sub UpdateStatus(rowCount As Integer, urgl As Double, rgl As Double)
        lblRows.Text = $"Rows: {rowCount}"
        Dim net = urgl + rgl
        lblPnLTotals.Text = $"URGL: {urgl:C0} • RGL: {rgl:C0} • Net: {net:C0}"
    End Sub
End Class
