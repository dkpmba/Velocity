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
        HandleHistoricalBar(reqId, bar)
    End Sub

    ' Finalize historical bar load and update stop/risk logic
    Public Sub TWSConn_historicalDataEnd(reqId As Integer, start As String, endTime As String)
        HandleEndOfHistoricalData(reqId)
    End Sub

    Public Sub TWSConn_contractDetails(reqId As Integer, details As ContractDetails)

    End Sub



    Public Sub TWSConn_tickSize(tickerId As Integer, field As Integer, size As Decimal)
        Try
            MainForm.Mkt_OnTickSize(tickerId, field, size)
        Catch ex As Exception
            Debug.WriteLine($"tickSize route error: {ex.Message}")
        End Try
    End Sub

    Public Sub TWSConn_tickPrice(tickerId As Integer, field As Integer, price As Double, attribs As TickAttrib)
        Try
            MainForm.Mkt_OnTickPrice(tickerId, field, price)
        Catch ex As Exception
            Debug.WriteLine($"tickPrice route error: {ex.Message}")
        End Try
    End Sub


    Public Sub TWSConn_tickOptionComputation(tickerId As Integer, field As Integer, impliedVol As Double,
                                         delta As Double, optPrice As Double, pvDividend As Double,
                                         gamma As Double, vega As Double, theta As Double, undPrice As Double)

        Try
            EodIvCapture.OnTickOptionComputation(tickerId, impliedVol)
        Catch
        End Try

    End Sub


    Public Sub TWSConn_orderStatus(orderId As Integer, status As String, filled As Decimal, remaining As Decimal,
                                avgFillPrice As Double, permId As Integer, parentId As Integer, lastFillPrice As Double,
                                clientId As Integer, whyHeld As String, mktCapPrice As Double)
        Try
            OrderStateStore.OnOrderStatus(orderId, status, CDbl(filled), CDbl(remaining), avgFillPrice, permId, parentId, lastFillPrice, clientId, whyHeld, mktCapPrice)
        Catch ex As Exception
            Debug.WriteLine($"orderStatus route error: {ex.Message}")
        End Try
    End Sub


    Public Sub TWSConn_openOrder(orderId As Integer, contract As Contract, order As Order, orderState As OrderState)
        Try
            OrderStateStore.OnOpenOrder(orderId, contract, order, orderState)
        Catch ex As Exception
            Debug.WriteLine($"openOrder route error: {ex.Message}")
        End Try

        ' NEW: also feed the what-if preview broker
        Try
            MarginPreview.OnWhatIfOpenOrder(orderId, contract, order, orderState)
        Catch : End Try
    End Sub
    ' =========================
    ' == EXEC DETAILS =========
    ' =========================
    Public Sub TWSConn_execDetails(reqId As Integer, contract As IBApi.Contract, execution As IBApi.Execution)
        Try
            OrderStateStore.OnExecDetails(contract, execution)
        Catch ex As Exception
            Debug.WriteLine($"execDetails route error: {ex.Message}")
        End Try
    End Sub

    ' =========================
    ' == COMMISSION ===========
    ' =========================
    Public Sub TWSConn_commissionReport(report As IBApi.CommissionAndFeesReport)
        Try
            OrderStateStore.OnCommissionReport(report)
        Catch ex As Exception
            Debug.WriteLine($"commissionReport route error: {ex.Message}")
        End Try
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
        Debug.Print($"ERROR: {errorMsg}")

        If id > 0 Then
            OnOrderError(id, errorMsg)
        End If
    End Sub

    Public Sub TWSConn_nextValidId(orderId As Integer)
        RaiseEvent NextValidId(orderId)
        Try
            OrderRouter.SeedNextValidId(orderId)
            ' Subscribe for any orders created outside this app, too
            If TwsHost.Tws IsNot Nothing AndAlso TwsHost.Tws.ClientSocket IsNot Nothing Then
                TwsHost.Tws.ClientSocket.reqAutoOpenOrders(True)
                TwsHost.Tws.ClientSocket.reqOpenOrders()
            End If
        Catch ex As Exception
            Debug.WriteLine($"nextValidId error: {ex.Message}")
        End Try
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
