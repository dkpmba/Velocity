
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


    ' Ensure the four BindingLists and DataGridView.DataSource are set
    Private Sub EnsureBindings()
        If _symbolsBinding Is Nothing Then
            _symbolsBinding = New BindingList(Of SymbolRow)()
            If dgvSymbols IsNot Nothing AndAlso dgvSymbols.DataSource Is Nothing Then
                dgvSymbols.AutoGenerateColumns = False
                dgvSymbols.DataSource = _symbolsBinding
            End If
        End If

        If _tradesBinding Is Nothing Then
            _tradesBinding = New BindingList(Of Trade)()
            If dgvTrades IsNot Nothing AndAlso dgvTrades.DataSource Is Nothing Then
                dgvTrades.AutoGenerateColumns = False
                dgvTrades.DataSource = _tradesBinding
            End If
        End If

        If _ordersBinding Is Nothing Then
            _ordersBinding = New BindingList(Of OrderRow)()
            If dgvOrders IsNot Nothing AndAlso dgvOrders.DataSource Is Nothing Then
                dgvOrders.AutoGenerateColumns = False
                dgvOrders.DataSource = _ordersBinding
            End If
        End If

        If _legsBinding Is Nothing Then
            _legsBinding = New BindingList(Of TradeLeg)()
            If dgvMonitor IsNot Nothing AndAlso dgvMonitor.DataSource Is Nothing Then
                dgvMonitor.AutoGenerateColumns = False
                dgvMonitor.DataSource = _legsBinding
            End If
        End If
    End Sub

    Public Sub RefreshAllData()
        ' Make sure bindings exist even if Init/BindAllGrids wasn’t called
        EnsureBindings()

        ' --- Symbols ---
        Dim symRows As IEnumerable(Of SymbolRow) = Nothing
        Try
            If AppServices.Symbols IsNot Nothing Then
                symRows = AppServices.Symbols.GetAll()
            End If
        Catch ex As Exception
            AppendAlert("Symbols load failed: " & ex.Message, "ERROR")
        End Try
        If symRows Is Nothing Then symRows = New List(Of SymbolRow)()

        _symbolsBinding.RaiseListChangedEvents = False
        _symbolsBinding.Clear()
        For Each r In symRows
            _symbolsBinding.Add(r)
        Next
        _symbolsBinding.RaiseListChangedEvents = True

        ' --- Open trades ---
        Dim trs As IEnumerable(Of Trade) = Nothing
        Try
            If AppServices.Trades IsNot Nothing Then
                trs = AppServices.Trades.GetOpenTrades()
            End If
        Catch ex As Exception
            AppendAlert("Trades load failed: " & ex.Message, "ERROR")
        End Try
        If trs Is Nothing Then trs = New List(Of Trade)()

        _tradesBinding.RaiseListChangedEvents = False
        _tradesBinding.Clear()
        For Each t In trs
            _tradesBinding.Add(t)
        Next
        _tradesBinding.RaiseListChangedEvents = True

        ' --- Orders / Legs (contextual) ---
        _ordersBinding.Clear()
        _legsBinding.Clear()
    End Sub

End Class
