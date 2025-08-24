
Imports Velocity.Core

Module AppServices

    Public ConnectionString As String

    ' Repository instances
    Public ConnFactory As IConnectionFactory
    Public Symbols As ISymbolRepository
    Public Trades As ITradeRepository
    Public Market As IMarketDataRepository
    Public Orders As IOrderRepository
    Public Alerts As IAlertRepository
    Public Activity As IActivityRepository
    Public Settings As ISettingsRepository
    Public Property OptionIv As IOptionIvRepository

    Public Sub Initialize(cs As String)
        ConnectionString = cs
        ConnFactory = New SqlConnectionFactory(cs)
        Symbols = New SqlSymbolRepository(ConnFactory)
        Trades = New SqlTradeRepository(ConnFactory)
        Market = New SqlMarketDataRepository(ConnFactory)
        Orders = New SqlOrderRepository(ConnFactory)
        Alerts = New SqlAlertRepository(ConnFactory)
        Activity = New SqlActivityRepository(ConnFactory)
        Settings = New SqlSettingsRepository(ConnFactory)
    End Sub

End Module
