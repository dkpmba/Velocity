Option Strict On
Option Explicit On

Module TradePnLManager

    Private ReadOnly _sync As New Object()
    Private ReadOnly _rglByTradeId As New Dictionary(Of Integer, Double)()

    Public Event TradeRglChanged(tradeId As Integer, newRgl As Double, delta As Double)

    Public Function GetRgl(tradeId As Integer) As Double
        SyncLock _sync
            Dim v As Double = 0
            _rglByTradeId.TryGetValue(tradeId, v)
            Return v
        End SyncLock
    End Function

    Public Sub AddRealized(tradeId As Integer, delta As Double)
        If tradeId <= 0 OrElse delta = 0 Then Return
        Dim newVal As Double
        SyncLock _sync
            Dim cur As Double = 0
            _rglByTradeId.TryGetValue(tradeId, cur)
            newVal = cur + delta
            _rglByTradeId(tradeId) = newVal
        End SyncLock
        RaiseEvent TradeRglChanged(tradeId, newVal, delta)
    End Sub

    ''' <summary>Apply commission/fees (negative delta reduces RGL).</summary>
    Public Sub AddCommission(tradeId As Integer, commissionAndFees As Double)
        If tradeId <= 0 OrElse commissionAndFees = 0 Then Return
        ' Commissions reduce realized PnL
        AddRealized(tradeId, -Math.Abs(commissionAndFees))
    End Sub

End Module

