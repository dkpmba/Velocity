
Imports System.ComponentModel
Imports Velocity.Core

Partial Class MainForm

    Private _symbolsBinding As BindingList(Of SymbolRow)
    Private _tradesBinding As BindingList(Of Trade)
    Private _ordersBinding As BindingList(Of OrderRow)
    Private _legsBinding As BindingList(Of TradeLeg)
    Private _symbolsBindingHandlerHooked As Boolean = False

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
        WireTradeRgl()

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
            ' Hook once: auto-notify when first item arrives
            If Not _symbolsBindingHandlerHooked Then
                AddHandler _symbolsBinding.ListChanged, AddressOf SymbolsBinding_ListChanged
                _symbolsBindingHandlerHooked = True
            End If
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

        'If _ordersBinding Is Nothing Then
        '    _ordersBinding = New BindingList(Of OrderRow)()
        '    If dgvOrders IsNot Nothing AndAlso dgvOrders.DataSource Is Nothing Then
        '        dgvOrders.AutoGenerateColumns = False
        '        dgvOrders.DataSource = _ordersBinding
        '    End If
        'End If
        If dgvOrders IsNot Nothing Then
            dgvOrders.AutoGenerateColumns = False
            dgvOrders.DataSource = OrderStateStore.OrdersBinding   ' ← use the live list
            BuildOrdersColumns(dgvOrders)                          ' keep your existing columns
            WireOrdersContextMenu()
        End If

        If _legsBinding Is Nothing Then
            _legsBinding = New BindingList(Of TradeLeg)()
            If dgvMonitor IsNot Nothing AndAlso dgvMonitor.DataSource Is Nothing Then
                dgvMonitor.AutoGenerateColumns = False
                dgvMonitor.DataSource = _legsBinding
                WireMonitorContextMenu()
            End If
        End If
    End Sub
    Private Sub SymbolsBinding_ListChanged(sender As Object, e As ListChangedEventArgs)
        If _hbootPrereqSymbols Then Exit Sub
        If e.ListChangedType = ListChangedType.ItemAdded OrElse e.ListChangedType = ListChangedType.Reset Then
            If _symbolsBinding IsNot Nothing AndAlso _symbolsBinding.Count > 0 Then
                'HBoot_MarkSymbolsReady("listchanged")
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
        _symbolsBinding.ResetBindings()
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
        _tradesBinding.ResetBindings()

        ' --- Orders / Legs (contextual) ---
        _ordersBinding.Clear()
        _legsBinding.Clear()

        NotifySymbolsLoaded()
    End Sub

    ' ===================== MainForm.vb ADD/UPDATE =====================


    ' Small item type so we can show friendly text while keeping the enum value.
    Private Class TfItem
        Public ReadOnly Property Text As String
        Public ReadOnly Property Value As TimeFrames
        Public Sub New(t As String, v As TimeFrames)
            Text = t : Value = v
        End Sub
        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    ' Prereq flags and one-shot guard
    Private _hbootPrereqTws As Boolean = False
    Private _hbootPrereqSymbols As Boolean = False
    Private _hbootPrereqTf As Boolean = False
    Private _hbootStarted As Boolean = False

    ' Call this once (e.g., in Form_Load) to populate the ToolStripComboBox cbTimeFrame.
    Private Sub InitTimeFrameCombo()
        ' Use the hosted ComboBox API via .ComboBox
        cbTimeFrame.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
        cbTimeFrame.ComboBox.DisplayMember = "Text"   ' show TfItem.Text
        cbTimeFrame.ComboBox.ValueMember = "Value"    ' (not required, but fine)

        cbTimeFrame.Items.Clear()
        cbTimeFrame.Items.Add(New TfItem("1 Minute", TimeFrames.M1))
        cbTimeFrame.Items.Add(New TfItem("5 Minutes", TimeFrames.M5))

        cbTimeFrame.SelectedIndex = 0   ' default to 1 Minute

        ' Mark timeframe prereq ready and try to start
        _hbootPrereqTf = True
        HBoot_TryStart()
    End Sub

    ' Helper: read the selected enum value safely from cbTimeFrame.
    Private Function GetSelectedTimeFrame() As TimeFrames
        Dim item As TfItem = TryCast(cbTimeFrame.SelectedItem, TfItem)
        If item Is Nothing Then Return TimeFrames.M1
        Return item.Value
    End Function

    ''' <summary>
    ''' Translate the selected enum into IB bar parameters (barSize, duration).
    ''' Per your guidance: 1 D for 1-min, 2 D for 5-min.
    ''' </summary>
    Private Function HBoot_GetIbBarParamsFromCombo() As (barSize As String, duration As String)
        Select Case GetSelectedTimeFrame()
            Case TimeFrames.M1 : Return ("1 min", "1 D")
            Case TimeFrames.M5 : Return ("5 mins", "2 D")
            Case Else : Return ("1 min", "1 D")
        End Select
    End Function

    ' =================== /MainForm.vb ADD/UPDATE ======================
    ''' <summary>
    ''' Notify the coordinator that TWS is connected.
    ''' Call this from your TWS connection success path (e.g., NextValidId or a "Connected" event).
    ''' Example: in TWSEvents.NextValidId handler, call MainForm.NotifyTwsConnected().
    ''' </summary>
    Public Sub NotifyTwsConnected()
        _hbootPrereqTws = True
        ' Ensure the HistoricalBarManager requester is set (will no-op if already set)
        HBoot_SetupRequesterIfNeeded()
        Debug.Print("NotifyTwsConnected Loaded")
        HBoot_TryStart()
    End Sub

    ''' <summary>
    ''' Notify the coordinator that symbols are fully loaded into dgvSymbols.
    ''' If you bind a DataTable, you can also wire this to DataBindingComplete.
    ''' Otherwise, call it at the end of your "populate symbols" method.
    ''' </summary>
    Public Sub NotifySymbolsLoaded()
        ' Optional sanity: ensure we actually have rows
        If _symbolsBinding IsNot Nothing AndAlso _symbolsBinding.Count > 0 Then
            HBoot_MarkSymbolsReady("manual")
        End If
    End Sub

    ' If you bind dgvSymbols, this auto-notifies when binding completes.
    ' Comment out the Handles clause if you prefer to call NotifySymbolsLoaded() manually.
    Private Sub dgvSymbols_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) _
    Handles dgvSymbols.DataBindingComplete
        If _hbootPrereqSymbols Then Exit Sub
        If dgvSymbols IsNot Nothing AndAlso dgvSymbols.Rows IsNot Nothing AndAlso dgvSymbols.Rows.Count > 0 Then
            HBoot_MarkSymbolsReady("databindingcomplete")
        End If
    End Sub
    Private Sub HBoot_MarkSymbolsReady(Optional reason As String = "")
        If _hbootPrereqSymbols Then Exit Sub   ' idempotent guard
        _hbootPrereqSymbols = True

        ' (Optional) unhook symbol-ready detectors; we don’t need them after first success
        Try
            RemoveHandler dgvSymbols.DataBindingComplete, AddressOf dgvSymbols_DataBindingComplete
        Catch : End Try
        Try
            If _symbolsBinding IsNot Nothing AndAlso _symbolsBindingHandlerHooked Then
                RemoveHandler _symbolsBinding.ListChanged, AddressOf SymbolsBinding_ListChanged
                _symbolsBindingHandlerHooked = False
            End If
        Catch : End Try

        HBoot_TryStart()
    End Sub
    ''' <summary>
    ''' Central gate: when all three prereqs are true, start the historical boot exactly once.
    ''' </summary>
    Private Sub HBoot_TryStart()
        If _hbootStarted Then Exit Sub
        If Not (_hbootPrereqTws AndAlso _hbootPrereqSymbols AndAlso _hbootPrereqTf) Then Exit Sub

        _hbootStarted = True
        HBoot_Start()   ' calls into the Manager-Orchestrated boot you already implemented
    End Sub

End Class
