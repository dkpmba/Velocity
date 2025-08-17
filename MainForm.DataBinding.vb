
Imports System.ComponentModel
Imports Velocity.Core

Partial Class MainForm

    Private _symbolsBinding As BindingList(Of SymbolRow)
    Private _tradesBinding As BindingList(Of Trade)
    Private _ordersBinding As BindingList(Of OrderRow)
    Private _legsBinding As BindingList(Of TradeLeg)

    ' Call from ConnectionManager after you have a connection string:
    '   Dim mf As New MainForm()
    '   mf.Init(connectionString)
    '   mf.Show()
    Public Sub Init(connectionString As String)
        If String.IsNullOrWhiteSpace(connectionString) Then
            Throw New ArgumentException("Connection string cannot be empty.", NameOf(connectionString))
        End If

        AppServices.Initialize(connectionString)

        BindAllGrids()
        RefreshAllData()
    End Sub

    Private Sub BindAllGrids()
        If dgvSymbols IsNot Nothing Then dgvSymbols.AutoGenerateColumns = False
        If dgvTrades IsNot Nothing Then dgvTrades.AutoGenerateColumns = False
        If dgvOrders IsNot Nothing Then dgvOrders.AutoGenerateColumns = False
        If dgvMonitor IsNot Nothing Then dgvMonitor.AutoGenerateColumns = False

        _symbolsBinding = New BindingList(Of SymbolRow)()
        _tradesBinding = New BindingList(Of Trade)()
        _ordersBinding = New BindingList(Of OrderRow)()
        _legsBinding = New BindingList(Of TradeLeg)()

        If dgvSymbols IsNot Nothing Then dgvSymbols.DataSource = _symbolsBinding
        If dgvTrades IsNot Nothing Then dgvTrades.DataSource = _tradesBinding
        If dgvOrders IsNot Nothing Then dgvOrders.DataSource = _ordersBinding
        If dgvMonitor IsNot Nothing Then dgvMonitor.DataSource = _legsBinding
    End Sub

    Public Sub RefreshAllData()
        ' Symbols
        If AppServices.Symbols IsNot Nothing Then
            Dim rows = AppServices.Symbols.GetAll()
            _symbolsBinding.Clear()
            For Each r In rows
                _symbolsBinding.Add(r)
            Next
        End If

        ' Open trades
        If AppServices.Trades IsNot Nothing Then
            Dim trs = AppServices.Trades.GetOpenTrades()
            _tradesBinding.Clear()
            For Each t In trs
                _tradesBinding.Add(t)
            Next
        End If

        ' Orders and legs are context-driven; keep empty until selection
        _ordersBinding.Clear()
        _legsBinding.Clear()
    End Sub

End Class
