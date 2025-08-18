Module BestExecution

    ' Tracks how many times we've tried improving each order
    Private OrderAttempts As New Dictionary(Of Integer, Integer)

    ' Call this to calculate initial limit price based on Bid/Ask and tick size
    Public Function GetInitialLMTPrice(action As String, bid As Double, ask As Double, tickSize As Double) As Double
        If bid <= 0 OrElse ask <= 0 Then Return 0

        Dim spread = ask - bid

        ' If spread is tight (<= 4 ticks), be aggressive
        If spread <= 4 * tickSize Then
            Return If(action = "BUY", ask, bid)
        End If

        ' Otherwise start at the mid
        Dim mid = (bid + ask) / 2
        Return Math.Round(mid / tickSize) * tickSize
    End Function

    ' Call this to improve price for an existing order
    Public Function ImproveLMTPrice(orderId As Integer, bid As Double, ask As Double, tickSize As Double, action As String, Optional relatedOrdersFilled As Boolean = False) As Double
        If bid <= 0 OrElse ask <= 0 Then Return 0

        Dim mid = (bid + ask) / 2
        Dim spreadTicks = CInt((ask - bid) / tickSize)

        If spreadTicks <= 1 Then Return If(action = "BUY", ask, bid) ' Very tight market, cross the spread

        ' Default to 1 tick improvement
        Dim tickShift As Integer = 1

        ' If spread is wide, increase aggressiveness
        If spreadTicks >= 5 Then tickShift = 2

        ' If other related legs have filled, become more aggressive
        If spreadTicks < 5 AndAlso relatedOrdersFilled Then tickShift += 1

        ' Track how many times we've tried improving
        If Not OrderAttempts.ContainsKey(orderId) Then
            OrderAttempts(orderId) = 1
        Else
            OrderAttempts(orderId) += 1
        End If

        Dim totalShift = tickShift * OrderAttempts(orderId)

        ' Adjust price based on direction
        Dim newLmt As Double
        If action = "BUY" Then
            newLmt = mid + totalShift * tickSize
            If newLmt > ask Then newLmt = ask ' Never worse than ask
        Else
            newLmt = mid - totalShift * tickSize
            If newLmt < bid Then newLmt = bid ' Never worse than bid
        End If

        Return Math.Round(newLmt / tickSize) * tickSize
    End Function

    ' Optional: Reset tracking when order is filled or cancelled
    Public Sub ClearOrder(orderId As Integer)
        If OrderAttempts.ContainsKey(orderId) Then OrderAttempts.Remove(orderId)
    End Sub

End Module
