Public Class BarManager

    'Store bars per conId
    Private BarMap As New Dictionary(Of Long, List(Of BarData))

    Public Sub AddBar(conId As Long, bar As BarData)
        If Not BarMap.ContainsKey(conId) Then
            BarMap(conId) = New List(Of BarData)()
        End If
        BarMap(conId).Add(bar)

        ' Keep a maximum of 100 bars for memory efficiency
        If BarMap(conId).Count > 100 Then
            BarMap(conId).RemoveAt(0)
        End If
    End Sub



    Public Class BarData
        Public Property Time As DateTime
        Public Property Open As Double
        Public Property High As Double
        Public Property Low As Double
        Public Property Close As Double
        Public Property Volume As Long
    End Class

End Class
