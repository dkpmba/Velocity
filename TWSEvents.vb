' ==========================
' MODULE: TWSEvents.vb
' ==========================

Imports IBApi

Public Module TWSEvents
    ' === Public events other code can subscribe to ===
    Public Event NextValidId(id As Integer)
    Public Event ConnClosed()
    Public Event ApiError(code As Integer, message As String)

    ' === Add near the top with your other events ===
    Public Event ServerTime(epoch As Long)

    ' === In your forwarders (called by Tws.vb) add/ensure this exists ===
    Public Sub TWSConn_currentTime(time As Long)
        RaiseEvent ServerTime(time)
    End Sub


    Public Sub TWSConn_historicalData(reqId As Integer, bar As Bar)
        HistoricalBarManager.HandleHistoricalBar(reqId, bar)
    End Sub

    ' Finalize historical bar load and update stop/risk logic
    Public Sub TWSConn_historicalDataEnd(reqId As Integer, start As String, endTime As String)
        Console.WriteLine("Barcount in TWSConn_historicalDataEnd : " & HistoricalBarManager.BarMap(reqId).Count)
    End Sub

    Public Sub TWSConn_contractDetails(reqId As Integer, details As ContractDetails)

    End Sub



    Public Sub TWSConn_tickSize(tickerId As Integer, field As Integer, size As Decimal)

    End Sub

    Public Sub TWSConn_tickPrice(tickerId As Integer, field As Integer, price As Double, attribs As TickAttrib)
    End Sub


    Public Sub TWSConn_tickOptionComputation(tickerId As Integer, field As Integer, impliedVol As Double,
                                         delta As Double, optPrice As Double, pvDividend As Double,
                                         gamma As Double, vega As Double, theta As Double, undPrice As Double)

    End Sub


    Public Sub TWSConn_orderStatus(orderId As Integer, status As String, filled As Decimal, remaining As Decimal,
                                avgFillPrice As Double, permId As Integer, parentId As Integer, lastFillPrice As Double,
                                clientId As Integer, whyHeld As String, mktCapPrice As Double)

    End Sub


    Public Sub TWSConn_openOrder(orderId As Integer, contract As Contract, order As Order, orderState As OrderState)

    End Sub

    Public Sub TWSConn_pnlSingle(reqId As Integer, pos As Decimal, dailyPnL As Double, unrealizedPnL As Double, realizedPnL As Double, value As Double)

    End Sub

    ' Called by Tws when connection closes
    Public Sub TWSConn_connectionClosed()
        RaiseEvent ConnClosed()
    End Sub

    Public Sub TWSConn_errMsg(str As String)

    End Sub
    Public Sub TWSConn_errMsg2(id As Integer, errorTime As Long, errorCode As Integer, errorMsg As String, advancedOrderRejectJson As String)
        RaiseEvent ApiError(errorCode, errorMsg)
    End Sub

    Public Sub TWSConn_nextValidId(orderId As Integer)
        RaiseEvent NextValidId(orderId)
    End Sub

    Public Sub TWSConn_updateAccountValue(key As String, value As String, currency As String, accountName As String)

    End Sub

    Private Sub UpdateBuyingPowerUI(value As String, labelKey As String)

    End Sub

    Public Sub TWSConn_managedAccounts(accountsList As String)

    End Sub

    Public Sub TWSConn_position(account As String, contract As Contract, pos As Decimal, avgCost As Double)



    End Sub
    Public Sub TWSConn_positionEnd()

    End Sub

    Public Sub TwsConn_securityDefinitionOptionParameter(reqId As Integer, exchange As String, underlyingConId As Integer,
                                                       tradingClass As String, multiplier As String,
                                                       expirations As HashSet(Of String), strikes As HashSet(Of Double))



    End Sub

    Public Sub TwsConn_securityDefinitionOptionParameterEnd(reqId As Integer)

    End Sub



End Module
