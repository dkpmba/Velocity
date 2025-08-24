Imports System.Data

Namespace Velocity.Core

    ' Abstract factory for DB connections so we can swap providers if needed.
    Public Interface IConnectionFactory
        Function CreateOpen() As IDbConnection
    End Interface

    Public Interface ISymbolRepository
        Function GetAll() As IEnumerable(Of SymbolRow)
        Sub Upsert(symbol As SymbolRow)
        Sub SetEnabled(cid As Long, enabled As Boolean)
        Sub Delete(cid As Long)
    End Interface

    Public Interface IMarketDataRepository
        Sub UpsertQuote(q As Quote)
        Sub UpsertGreeks(g As Greeks)
        Sub UpsertBar1m(b As Bar1m)
        Function GetLatestBar(cid As Long) As Bar1m
        Function GetBars1m(cid As Long, fromUtc As DateTime, toUtc As DateTime) As IEnumerable(Of Bar1m)
    End Interface

    Public Interface ITradeRepository
        Function CreateTrade(t As Trade) As Long ' returns TID
        Sub AddLeg(tid As Long, leg As TradeLeg)
        Sub UpdateTrade(t As Trade)
        Sub UpdateLeg(leg As TradeLeg)
        Function GetTrade(tid As Long) As Trade
        Function GetTradeLegs(tid As Long) As IEnumerable(Of TradeLeg)
        Function GetOpenTrades() As IEnumerable(Of Trade)
        ' Adds delta to current RGL
        Sub UpdateRglDelta(tid As Integer, delta As Decimal)

        ' Sets absolute RGL value
        Sub UpdateRgl(tid As Integer, newRgl As Decimal)
        Sub CloseTrade(tid As Integer, finalRgl As Decimal)

    End Interface

    Public Interface IOrderRepository
        Sub SaveOrder(o As OrderRow)
        Sub UpdateOrder(o As OrderRow)
        Sub SaveFill(f As FillRow)
        Function GetOrdersByTID(tid As Long) As IEnumerable(Of OrderRow)
        Function GetFillsByOID(oid As Long) As IEnumerable(Of FillRow)
    End Interface

    Public Interface IAlertRepository
        Sub Insert(a As AlertRow)
        Sub Acknowledge(ids As IEnumerable(Of Long))
        Sub Dismiss(ids As IEnumerable(Of Long))
        Function Query(fromUtc As DateTime, toUtc As DateTime, Optional severity As String = Nothing) As IEnumerable(Of AlertRow)
    End Interface

    Public Interface IActivityRepository
        Sub Insert(evt As ActivityRow)
        Function Query(fromUtc As DateTime, toUtc As DateTime, Optional level As String = Nothing) As IEnumerable(Of ActivityRow)
    End Interface

    Public Interface ISettingsRepository
        Function GetValue(key As String) As String
        Sub SetValue(key As String, json As String)
    End Interface

    Public Interface IOptionIvRepository
        Sub UpsertEod(conId As Integer, iv As Decimal, asOfUtc As Date)
    End Interface

End Namespace
