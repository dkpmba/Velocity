Imports IBApi

Public Module HistoricalBarManager

    Public BarRequestMap As New Dictionary(Of Integer, Long) ' reqId -> conId
    Public BarMap As New Dictionary(Of Long, List(Of BarManager.BarData)) ' conId -> bars
    Public OutstandingBarRequests As Integer = 0

    Public Sub RequestHistoricalBars(tws As EClientSocket, conId As Long, contract As Contract, durationStr As String, barSize As String, reqId As Integer)
        If Not BarRequestMap.ContainsKey(reqId) Then
            BarRequestMap(reqId) = conId
            OutstandingBarRequests += 1
        End If
        tws.reqHistoricalData(reqId, contract, "", durationStr, barSize, "TRADES", 0, 1, False, Nothing)
    End Sub

    Public Sub HandleHistoricalBar(reqId As Integer, bar As Bar)
        If Not BarRequestMap.ContainsKey(reqId) Then Exit Sub
        Dim conId = BarRequestMap(reqId)

        If Not BarMap.ContainsKey(conId) Then
            BarMap(conId) = New List(Of BarManager.BarData)()
        End If
        Dim cleanTimeStr As String = bar.Time.Split(" "c)(0) & " " & bar.Time.Split(" "c)(1)
        Dim parsedTime As DateTime = DateTime.ParseExact(cleanTimeStr, "yyyyMMdd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)

        Dim barData As New BarManager.BarData With {
            .Time = parsedTime,
            .Open = bar.Open,
            .High = bar.High,
            .Low = bar.Low,
            .Close = bar.Close,
            .Volume = CLng(bar.Volume)
        }

        BarMap(conId).Add(barData)

        ' Optionally trim to N bars
        If BarMap(conId).Count > 100 Then
            BarMap(conId).RemoveAt(0)
        End If
    End Sub

    Public Sub HandleEndOfHistoricalData(reqId As Integer)
        If BarRequestMap.ContainsKey(reqId) Then
            BarRequestMap.Remove(reqId)
            OutstandingBarRequests -= 1
        End If
    End Sub

    Public Function HasBars(conId As Long) As Boolean
        Return BarMap.ContainsKey(conId) AndAlso BarMap(conId).Count >= 10
    End Function

    Public Function GetATR(cid As Long, period As Integer, Optional index As Integer = -1) As Double
        If Not BarMap.ContainsKey(cid) Then Return 0
        Dim bars = BarMap(cid)
        If bars.Count < period + 1 Then Return 0

        ' Default to last bar
        Dim i = If(index = -1, bars.Count - 1, index)
        If i < period Then Return 0

        Dim trList As New List(Of Double)
        For j = i - period + 1 To i
            Dim high = bars(j).High
            Dim low = bars(j).Low
            Dim prevClose = bars(j - 1).Close
            Dim tr = Math.Max(high - low, Math.Max(Math.Abs(high - prevClose), Math.Abs(low - prevClose)))
            trList.Add(tr)
        Next

        Return trList.Average()
    End Function


End Module
