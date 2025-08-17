Imports System.Drawing

Partial Class MainForm
    Private _latencySw As New Stopwatch()
    Private _twsStrip As StatusStrip
    Private _twsStatus As ToolStripStatusLabel
    Private _statusStrip As StatusStrip
    Private _latencyTimer As Timer

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        EnsureTwsStatusUi()
        HookTwseventsOnce()
        UpdateTwsStatus()
        If Not String.IsNullOrWhiteSpace(AppServices.ConnectionString) Then
            UpdateDbStatus(GetDbStatusText(AppServices.ConnectionString))
        End If
    End Sub
    Private Sub EnsureTwsStatusUi()
        ' Use existing StatusStrip1
        _twsStrip = Me.Controls.Find("StatusStrip1", True).OfType(Of StatusStrip)().FirstOrDefault()
        If _twsStrip Is Nothing Then
            _twsStrip = New StatusStrip() With {.Name = "StatusStrip1", .Dock = DockStyle.Bottom}
            Me.Controls.Add(_twsStrip)
            _twsStrip.BringToFront()
        End If

        ' Prefer existing lblConn as the TWS status label
        _twsStatus = _twsStrip.Items.OfType(Of ToolStripStatusLabel)().
        FirstOrDefault(Function(i) i.Name = "lblConn")

        If _twsStatus Is Nothing Then
            _twsStatus = New ToolStripStatusLabel() With {.Name = "lblConn", .Text = "TWS: n/a"}
            _twsStrip.Items.Add(New ToolStripStatusLabel() With {.Spring = True}) ' spacer
            _twsStrip.Items.Add(_twsStatus)
        End If
    End Sub

    ' Update DB label
    Private Sub UpdateDbStatus(text As String)
        If lblDb IsNot Nothing Then lblDb.Text = text
    End Sub

    ' Update latency label
    Private Sub UpdateLatency(ms As Integer)
        If lblLatency IsNot Nothing Then lblLatency.Text = $"RTT: {ms} ms"
    End Sub

    ' Start/stop a periodic latency ping
    Private Sub EnsureLatencyTimer()
        If _latencyTimer Is Nothing Then
            _latencyTimer = New Timer() With {.Interval = 30000} ' 30s
            AddHandler _latencyTimer.Tick, Sub()
                                               If TwsHost.IsConnected() Then PingServerTime()
                                           End Sub
        End If
        If TwsHost.IsConnected() Then _latencyTimer.Start() Else _latencyTimer.Stop()
    End Sub
    ' Call this from a menu/toolbar button if you like, or automatically after connecting:
    Public Sub PingServerTime()
        If Not TwsHost.IsConnected() Then
            AppendAlert("Cannot ping server time: not connected.", "WARN")
            Return
        End If
        _latencySw.Restart()
        TwsHost.Tws.ClientSocket.reqCurrentTime()
        AppendAlert("Requested server time.", "INFO")
    End Sub


    Private Sub UpdateTwsStatus(Optional connected As Boolean? = Nothing, Optional text As String = Nothing)
        If lblConn Is Nothing Then Exit Sub
        Dim isConn = If(connected.HasValue, connected.Value, TwsHost.IsConnected())
        lblConn.Text = If(text Is Nothing, If(isConn, "TWS: Connected", "TWS: Disconnected"), text)
        lblConn.ForeColor = If(isConn, Color.DarkGreen, Color.Maroon)
    End Sub

End Class
