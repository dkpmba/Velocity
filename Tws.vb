' ==========================
' MODULE: Tws.vb
' ==========================

Imports IBApi


Public Class Tws
    Implements EWrapper

    Public ReadOnly Property Signal As EReaderSignal
        Get
            Return internalSignal
        End Get
    End Property

    Private ReadOnly internalSignal As EReaderSignal = New EReaderMonitorSignal()

    Private ReadOnly client As EClientSocket

    Public Sub New()
        client = New EClientSocket(Me, signal)
    End Sub

    Public ReadOnly Property ClientSocket As EClientSocket
        Get
            Return client
        End Get
    End Property

    Public Sub accountDownloadEnd(account As String) Implements EWrapper.accountDownloadEnd
    End Sub

    Public Sub accountSummary(reqId As Integer, account As String, tag As String, value As String, currency As String) Implements EWrapper.accountSummary
    End Sub

    Public Sub accountSummaryEnd(reqId As Integer) Implements EWrapper.accountSummaryEnd
    End Sub

    Public Sub bondContractDetails(reqId As Integer, contractDetails As ContractDetails) Implements EWrapper.bondContractDetails
    End Sub

    Public Sub commissionAndFeesReport(commissionAndFeesReport As CommissionAndFeesReport) Implements EWrapper.commissionAndFeesReport
    End Sub

    Public Sub completedOrder(contract As Contract, order As Order, orderState As OrderState) Implements EWrapper.completedOrder
    End Sub

    Public Sub completedOrdersEnd() Implements EWrapper.completedOrdersEnd
    End Sub

    Public Sub connectAck() Implements EWrapper.connectAck
    End Sub

    Public Sub connectionClosed() Implements EWrapper.connectionClosed
    End Sub

    Public Sub contractDetails(reqId As Integer, contractDetails As ContractDetails) Implements EWrapper.contractDetails
        TWSEvents.TWSConn_contractDetails(reqId, contractDetails)
    End Sub

    Public Sub contractDetailsEnd(reqId As Integer) Implements EWrapper.contractDetailsEnd
    End Sub

    Public Sub currentTime(time As Long) Implements EWrapper.currentTime
    End Sub

    Public Sub deltaNeutralValidation(reqId As Integer, deltaNeutralContract As DeltaNeutralContract) Implements EWrapper.deltaNeutralValidation
    End Sub

    Public Sub displayGroupList(reqId As Integer, groups As String) Implements EWrapper.displayGroupList
    End Sub

    Public Sub displayGroupUpdated(reqId As Integer, contractInfo As String) Implements EWrapper.displayGroupUpdated
    End Sub

    Public Sub [Error](str As String) Implements EWrapper.error
        TWSEvents.TWSConn_errMsg(str)
    End Sub

    Public Sub [Error](e As Exception) Implements EWrapper.error
    End Sub

    Public Sub [Error](id As Integer, errorTime As Long, errorCode As Integer, errorMsg As String, advancedOrderRejectJson As String) Implements EWrapper.error
        TWSConn_errMsg2(id, errorTime, errorCode, errorMsg, advancedOrderRejectJson)
    End Sub

    Public Sub execDetails(reqId As Integer, contract As Contract, execution As Execution) Implements EWrapper.execDetails
    End Sub

    Public Sub execDetailsEnd(reqId As Integer) Implements EWrapper.execDetailsEnd
    End Sub

    Public Sub familyCodes(familyCodes() As FamilyCode) Implements EWrapper.familyCodes
    End Sub

    Public Sub fundamentalData(reqId As Integer, data As String) Implements EWrapper.fundamentalData
    End Sub

    Public Sub headTimestamp(reqId As Integer, headTimestamp As String) Implements EWrapper.headTimestamp
    End Sub

    Public Sub histogramData(reqId As Integer, data() As HistogramEntry) Implements EWrapper.histogramData
    End Sub

    Public Sub historicalData(reqId As Integer, bar As Bar) Implements EWrapper.historicalData
        TWSEvents.TWSConn_historicalData(reqId, bar)
    End Sub

    Public Sub historicalDataEnd(reqId As Integer, start As String, [end] As String) Implements EWrapper.historicalDataEnd
        TWSEvents.TWSConn_historicalDataEnd(reqId, start, [end])
    End Sub

    Public Sub historicalDataUpdate(reqId As Integer, bar As Bar) Implements EWrapper.historicalDataUpdate
    End Sub

    Public Sub historicalNews(requestId As Integer, time As String, providerCode As String, articleId As String, headline As String) Implements EWrapper.historicalNews
    End Sub

    Public Sub historicalNewsEnd(requestId As Integer, hasMore As Boolean) Implements EWrapper.historicalNewsEnd
    End Sub

    Public Sub historicalSchedule(reqId As Integer, startDateTime As String, endDateTime As String, timeZone As String, sessions() As HistoricalSession) Implements EWrapper.historicalSchedule
    End Sub

    Public Sub managedAccounts(accountsList As String) Implements EWrapper.managedAccounts
        TWSEvents.TWSConn_managedAccounts(accountsList)
    End Sub

    Public Sub marketRule(marketRuleId As Integer, priceIncrements() As PriceIncrement) Implements EWrapper.marketRule
    End Sub

    Public Sub mktDepthExchanges(depthMktDataDescriptions() As DepthMktDataDescription) Implements EWrapper.mktDepthExchanges
    End Sub

    Public Sub newsArticle(requestId As Integer, articleType As Integer, articleText As String) Implements EWrapper.newsArticle
    End Sub

    Public Sub newsProviders(newsProviders() As NewsProvider) Implements EWrapper.newsProviders
    End Sub

    Public Sub nextValidId(orderId As Integer) Implements EWrapper.nextValidId
        TWSEvents.TWSConn_nextValidId(orderId)
    End Sub

    Public Sub openOrder(orderId As Integer, contract As Contract, order As Order, orderState As OrderState) Implements EWrapper.openOrder
        TWSEvents.TWSConn_openOrder(orderId, contract, order, orderState)
    End Sub

    Public Sub openOrderEnd() Implements EWrapper.openOrderEnd
    End Sub

    Public Sub orderBound(permId As Long, clientId As Integer, orderId As Integer) Implements EWrapper.orderBound
    End Sub

    Public Sub orderStatus(orderId As Integer, status As String, filled As Decimal, remaining As Decimal, avgFillPrice As Double, permId As Long, parentId As Integer, lastFillPrice As Double, clientId As Integer, whyHeld As String, mktCapPrice As Double) Implements EWrapper.orderStatus
        TWSEvents.TWSConn_orderStatus(orderId, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId, whyHeld, mktCapPrice)
    End Sub

    Public Sub pnl(reqId As Integer, dailyPnL As Double, unrealizedPnL As Double, realizedPnL As Double) Implements EWrapper.pnl
    End Sub

    Public Sub pnlSingle(reqId As Integer, pos As Decimal, dailyPnL As Double, unrealizedPnL As Double, realizedPnL As Double, value As Double) Implements EWrapper.pnlSingle
        TWSEvents.TWSConn_pnlSingle(reqId, pos, dailyPnL, unrealizedPnL, realizedPnL, value)
    End Sub

    Public Sub position(account As String, contract As Contract, pos As Decimal, avgCost As Double) Implements EWrapper.position
        TWSEvents.TWSConn_position(account, contract, pos, avgCost)
    End Sub

    Public Sub positionEnd() Implements EWrapper.positionEnd
        TWSConn_positionEnd()
    End Sub

    Public Sub positionMulti(requestId As Integer, account As String, modelCode As String, contract As Contract, pos As Decimal, avgCost As Double) Implements EWrapper.positionMulti
    End Sub

    Public Sub positionMultiEnd(requestId As Integer) Implements EWrapper.positionMultiEnd
    End Sub

    Public Sub rerouteMktDataReq(reqId As Integer, conId As Integer, exchange As String) Implements EWrapper.rerouteMktDataReq
    End Sub

    Public Sub rerouteMktDepthReq(reqId As Integer, conId As Integer, exchange As String) Implements EWrapper.rerouteMktDepthReq
    End Sub

    Public Sub realtimeBar(reqId As Integer, [date] As Long, open As Double, high As Double, low As Double, close As Double, volume As Decimal, wap As Decimal, count As Integer) Implements EWrapper.realtimeBar
    End Sub

    Public Sub receiveFA(faDataType As Integer, faXmlData As String) Implements EWrapper.receiveFA
    End Sub

    Public Sub replaceFAEnd(reqId As Integer, text As String) Implements EWrapper.replaceFAEnd
    End Sub

    Public Sub scannerData(reqId As Integer, rank As Integer, contractDetails As ContractDetails, distance As String, benchmark As String, projection As String, legsStr As String) Implements EWrapper.scannerData
    End Sub

    Public Sub scannerDataEnd(reqId As Integer) Implements EWrapper.scannerDataEnd
    End Sub

    Public Sub scannerParameters(xml As String) Implements EWrapper.scannerParameters
    End Sub

    Public Sub securityDefinitionOptionParameter(reqId As Integer, exchange As String, underlyingConId As Integer, tradingClass As String, multiplier As String, expirations As HashSet(Of String), strikes As HashSet(Of Double)) Implements EWrapper.securityDefinitionOptionParameter
        TwsConn_securityDefinitionOptionParameter(reqId, exchange, underlyingConId, tradingClass, multiplier, expirations, strikes)
    End Sub

    Public Sub securityDefinitionOptionParameterEnd(reqId As Integer) Implements EWrapper.securityDefinitionOptionParameterEnd
        TwsConn_securityDefinitionOptionParameterEnd(reqId)
    End Sub

    Public Sub smartComponents(reqId As Integer, theMap As Dictionary(Of Integer, KeyValuePair(Of String, Char))) Implements EWrapper.smartComponents
    End Sub

    Public Sub softDollarTiers(reqId As Integer, tiers() As SoftDollarTier) Implements EWrapper.softDollarTiers
    End Sub

    Public Sub symbolSamples(reqId As Integer, contractDescriptions() As ContractDescription) Implements EWrapper.symbolSamples
    End Sub

    Public Sub tickByTickAllLast(reqId As Integer, tickType As Integer, time As Long, price As Double, size As Decimal, tickAttribLast As TickAttribLast, exchange As String, specialConditions As String) Implements EWrapper.tickByTickAllLast
    End Sub

    Public Sub tickByTickBidAsk(reqId As Integer, time As Long, bidPrice As Double, askPrice As Double, bidSize As Decimal, askSize As Decimal, tickAttribBidAsk As TickAttribBidAsk) Implements EWrapper.tickByTickBidAsk
    End Sub

    Public Sub tickByTickMidPoint(reqId As Integer, time As Long, midPoint As Double) Implements EWrapper.tickByTickMidPoint
    End Sub

    Public Sub tickEFP(tickerId As Integer, field As Integer, basisPoints As Double, formattedBasisPoints As String, impliedFuture As Double, holdDays As Integer, futureLastTradeDate As String, dividendImpact As Double, dividendsToLastTradeDate As Double) Implements EWrapper.tickEFP
    End Sub

    Public Sub tickGeneric(tickerId As Integer, tickType As Integer, value As Double) Implements EWrapper.tickGeneric
    End Sub

    Public Sub tickNews(tickerId As Integer, timeStamp As Long, providerCode As String, articleId As String, headline As String, extraData As String) Implements EWrapper.tickNews
    End Sub

    Public Sub tickOptionComputation(tickerId As Integer, field As Integer, tickAttrib As Integer, impliedVolatility As Double, delta As Double, optPrice As Double, pvDividend As Double, gamma As Double, vega As Double, theta As Double, undPrice As Double) Implements EWrapper.tickOptionComputation
        'Console.WriteLine("field:" & field & " tickerId: " & tickerId & " delta: " & delta)
        TWSConn_tickOptionComputation(tickerId, field, impliedVolatility, delta, optPrice, pvDividend, gamma, vega, theta, undPrice)
    End Sub

    Public Sub tickPrice(tickerId As Integer, field As Integer, price As Double, attribs As TickAttrib) Implements EWrapper.tickPrice
        TWSEvents.TWSConn_tickPrice(tickerId, field, price, attribs)
    End Sub

    Public Sub tickReqParams(tickerId As Integer, minTick As Double, bboExchange As String, snapshotPermissions As Integer) Implements EWrapper.tickReqParams
    End Sub

    Public Sub tickSize(tickerId As Integer, field As Integer, size As Decimal) Implements EWrapper.tickSize
        TWSConn_tickSize(tickerId, field, size)
    End Sub

    Public Sub updateAccountTime(timeStamp As String) Implements EWrapper.updateAccountTime
    End Sub

    Public Sub updateAccountValue(key As String, value As String, currency As String, accountName As String) Implements EWrapper.updateAccountValue
        'Console.WriteLine($"[updateAccountValue] {key} = {value} {currency}, Account: {accountName}")
        TWSConn_updateAccountValue(key, value, currency, accountName)
    End Sub

    Public Sub updateMktDepth(tickerId As Integer, position As Integer, operation As Integer, side As Integer, price As Double, size As Decimal) Implements EWrapper.updateMktDepth
    End Sub

    Public Sub updateMktDepthL2(tickerId As Integer, position As Integer, marketMaker As String, operation As Integer, side As Integer, price As Double, size As Decimal, isSmartDepth As Boolean) Implements EWrapper.updateMktDepthL2
    End Sub

    Public Sub updateNewsBulletin(msgId As Integer, msgType As Integer, message As String, origExchange As String) Implements EWrapper.updateNewsBulletin
    End Sub

    Public Sub updatePortfolio(contract As Contract, position As Decimal, marketPrice As Double, marketValue As Double, averageCost As Double, unrealizedPNL As Double, realizedPNL As Double, accountName As String) Implements EWrapper.updatePortfolio
    End Sub

    Public Sub userInfo(reqId As Integer, whiteBrandingId As String) Implements EWrapper.userInfo
    End Sub

    Public Sub verifyAndAuthCompleted(isSuccessful As Boolean, errorText As String) Implements EWrapper.verifyAndAuthCompleted
    End Sub

    Public Sub verifyAndAuthMessageAPI(apiData As String, xyzChallenge As String) Implements EWrapper.verifyAndAuthMessageAPI
    End Sub

    Public Sub verifyCompleted(isSuccessful As Boolean, errorText As String) Implements EWrapper.verifyCompleted
    End Sub

    Public Sub verifyMessageAPI(apiData As String) Implements EWrapper.verifyMessageAPI
    End Sub

    Public Sub wshEventData(reqId As Integer, dataJson As String) Implements EWrapper.wshEventData
    End Sub

    Public Sub wshMetaData(reqId As Integer, dataJson As String) Implements EWrapper.wshMetaData
    End Sub

    Public Sub tickString(tickerId As Integer, field As Integer, value As String) Implements EWrapper.tickString
    End Sub

    Public Sub tickSnapshotEnd(tickerId As Integer) Implements EWrapper.tickSnapshotEnd
    End Sub

    Public Sub marketDataType(reqId As Integer, marketDataType As Integer) Implements EWrapper.marketDataType
    End Sub

    Public Sub accountUpdateMulti(requestId As Integer, account As String, modelCode As String, key As String, value As String, currency As String) Implements EWrapper.accountUpdateMulti
    End Sub

    Public Sub accountUpdateMultiEnd(requestId As Integer) Implements EWrapper.accountUpdateMultiEnd
    End Sub

    Public Sub historicalTicks(reqId As Integer, ticks As HistoricalTick(), done As Boolean) Implements EWrapper.historicalTicks
    End Sub

    Public Sub historicalTicksBidAsk(reqId As Integer, ticks As HistoricalTickBidAsk(), done As Boolean) Implements EWrapper.historicalTicksBidAsk
    End Sub

    Public Sub historicalTicksLast(reqId As Integer, ticks As HistoricalTickLast(), done As Boolean) Implements EWrapper.historicalTicksLast
    End Sub

    Public Sub currentTimeInMillis(timeInMillis As Long) Implements EWrapper.currentTimeInMillis
    End Sub
End Class
