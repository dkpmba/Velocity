Imports System.Drawing
Imports System.Windows.Forms
Imports System.Text.Json

' Centralized DataGridView column builders for GreekTrader
' Usage example (in each Form.Load):
'   GridColumns.BuildMonitorColumns(dgvMonitor)
'   GridColumns.RestoreGridLayout(dgvMonitor, "Main_Monitor")
'
' When closing the form:
'   GridColumns.SaveGridLayout(dgvMonitor, "Main_Monitor")
'
' NOTE on DataPropertyName:
'   We use ASCII-friendly names for binding: e.g., "Delta", "Gamma", "Theta", "Vega".
'   Headers may show Greek letters (Δ, Γ, Θ, V) but the underlying property names stay ASCII.
'   For combined columns like "Bid/Ask/Mid", the property name is "BidAskMid" (string).

Public Module GridColumns

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Public entry points (call these from your forms)
    ' ─────────────────────────────────────────────────────────────────────────────

    Public Sub BuildMonitorColumns(dgv As DataGridView)
        SetupGrid(dgv)
        AddText(dgv, "TID", "TID", width:=70, frozen:=True)
        AddText(dgv, "CID", "CID", width:=70, frozen:=True)
        AddText(dgv, "Symbol", "Symbol", width:=90, frozen:=True)
        AddText(dgv, "Right", "Right", width:=50, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Strike", "Strike", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Expiry", "Expiry", width:=90, format:="yyyy-MM-dd", align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Qty", "Qty", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "AvgCost", "AvgCost", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "BidAskMid", "Bid/Ask/Mid", width:=110, align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "URGL", "URGL", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Delta", "Δ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Gamma", "Γ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Theta", "Θ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Vega", "V", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "IV", "IV", width:=70, format:="P2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Mark", "Mark", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "TimeValue", "TimeValue", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
    End Sub

    Public Sub BuildTradesColumns(dgv As DataGridView)
        SetupGrid(dgv)
        AddText(dgv, "TID", "TID", width:=70, frozen:=True)
        AddText(dgv, "Symbol", "Symbol", width:=90, frozen:=True)
        AddText(dgv, "Structure", "Structure", width:=90)
        AddText(dgv, "Size", "Size", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "StrikeMid", "StrikeMid", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Credit", "Credit", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "URGL", "URGL", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "RGL", "RGL", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "NetDelta", "Net Δ", width:=85, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "NetGamma", "Γ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "NetTheta", "Θ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Status", "Status", width:=80)
        AddText(dgv, "StartLast", "Start/Last Update", width:=150, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "ATR3", "ATR", width:=70, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "EMAState", "EMA", width:=70, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "TimeValue", "TimeValue", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
    End Sub

    Public Sub BuildOrdersColumns(dgv As DataGridView)
        SetupGrid(dgv)
        AddText(dgv, "Account", "Account", width:=90, frozen:=True)
        AddText(dgv, "Time", "Time", width:=140, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "OID", "OID", width:=70)
        AddText(dgv, "TID", "TID", width:=70)
        AddText(dgv, "Symbol", "Symbol", width:=90)
        AddText(dgv, "Side", "Side", width:=60, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "SecType", "SecType", width:=70, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Qty", "Qty", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "OrderType", "Lmt/Mkt", width:=70, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Price", "Price", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Status", "Status", width:=90)
        AddText(dgv, "Fills", "Fills", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Remaining", "Remaining", width:=80, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Exchange", "Exchange", width:=90)
        AddText(dgv, "Error", "Error", width:=200)
    End Sub

    Public Sub BuildSymbolsColumns(dgv As DataGridView)
        SetupGrid(dgv)
        AddText(dgv, "CID", "CID", width:=70, frozen:=True)
        AddText(dgv, "Symbol", "Symbol", width:=90, frozen:=True)
        AddText(dgv, "SecType", "SecType", width:=70)
        AddText(dgv, "Exchange", "Exchange", width:=90)
        AddText(dgv, "Mult", "Mult", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Increment", "Increment", width:=90, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "BidAskLast", "Bid/Ask/Last", width:=110, align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "IV", "IV", width:=70, format:="P2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "LiquidityScore", "Liquidity score", width:=110, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddCheck(dgv, "Enabled", "Enabled", width:=70, isReadOnly:=False)
    End Sub

    Public Sub BuildOptionChainColumns(dgv As DataGridView)
        SetupGrid(dgv)
        AddText(dgv, "Expiry", "Expiry", width:=90, format:="yyyy-MM-dd", align:=DataGridViewContentAlignment.MiddleCenter, frozen:=True)
        AddText(dgv, "DTE", "DTE", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Strike", "Strike", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight, frozen:=True)
        AddText(dgv, "Right", "Right", width:=50, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Bid", "Bid", width:=70, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Ask", "Ask", width:=70, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Mid", "Mid", width:=70, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Last", "Last", width:=70, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "IV", "IV", width:=70, format:="P2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Delta", "Δ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Gamma", "Γ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Theta", "Θ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Vega", "V", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Volume", "Vol", width:=70, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "OpenInterest", "OI", width:=70, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Mark", "Mark", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Extrinsic", "Extrinsic", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
    End Sub

    Public Sub BuildTradeLogColumns(dgv As DataGridView)
        SetupGrid(dgv)
        AddText(dgv, "Date", "Date", width:=100, format:="yyyy-MM-dd", align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "TID", "TID", width:=70)
        AddText(dgv, "Symbol", "Symbol", width:=90)
        AddText(dgv, "Structure", "Structure", width:=90)
        AddText(dgv, "Size", "Size", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "EntryCredit", "Entry Credit", width:=100, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "ExitDebit", "Exit Debit", width:=100, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "RGL", "RGL", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "URGL", "URGL", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "NetPnL", "Net", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "MaxDD", "Max DD", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Days", "Days", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Status", "Status", width:=90)
    End Sub

    Public Sub BuildTradeDetailsColumns(dgv As DataGridView)
        SetupGrid(dgv)
        AddText(dgv, "Time", "Time", width:=140, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "OID", "OID", width:=70)
        AddText(dgv, "Action", "Action", width:=70, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Symbol", "Symbol", width:=90)
        AddText(dgv, "Right", "Right", width:=50, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Strike", "Strike", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Expiry", "Expiry", width:=90, format:="yyyy-MM-dd", align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Qty", "Qty", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Price", "Price", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Commission", "Comm", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Fees", "Fees", width:=70, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Realized", "Realized", width:=90, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Exchange", "Exch", width:=70)
        AddText(dgv, "Liquidity", "Liq", width:=70)
        AddText(dgv, "Note", "Note", width:=200)
    End Sub
    ' Build columns for DesignForm's dgvDesign
    Public Sub BuildDesignColumns(dgv As DataGridView)
        SetupGrid(dgv)
        AddCheck(dgv, "UseLeg", "Use", width:=50, isReadOnly:=False)
        AddText(dgv, "LegID", "#", width:=40, align:=DataGridViewContentAlignment.MiddleCenter, frozen:=True)
        AddText(dgv, "CID", "CID", width:=80)
        AddText(dgv, "SecType", "SecType", width:=70)
        AddText(dgv, "Symbol", "Symbol", width:=100, frozen:=True)
        AddText(dgv, "Right", "Right", width:=50, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Strike", "Strike", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Expiry", "Expiry", width:=90, format:="yyyy-MM-dd", align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Qty", "Qty", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight, isReadOnly:=False)
        AddText(dgv, "Ratio", "Ratio", width:=60, format:="N2", align:=DataGridViewContentAlignment.MiddleRight, isReadOnly:=False)
        AddText(dgv, "OrderType", "Type", width:=60, align:=DataGridViewContentAlignment.MiddleCenter, isReadOnly:=False)
        AddText(dgv, "LimitPrice", "Limit", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight, isReadOnly:=False)
        AddText(dgv, "BidAskMid", "B/A/M", width:=100, align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Mark", "Mark", width:=80, format:="N2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Delta", "Δ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Gamma", "Γ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Theta", "Θ", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Vega", "V", width:=70, format:="N4", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "IV", "IV", width:=70, format:="P2", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Multiplier", "Mult", width:=60, format:="N0", align:=DataGridViewContentAlignment.MiddleRight)
        AddText(dgv, "Exchange", "Exch", width:=80)
        AddText(dgv, "TIF", "TIF", width:=60, align:=DataGridViewContentAlignment.MiddleCenter, isReadOnly:=False)
        AddText(dgv, "Status", "Status", width:=100)
        AddText(dgv, "Note", "Note", width:=180, isReadOnly:=False)
    End Sub

    Public Sub BuildAlertsColumns(dgv As DataGridView)
        SetupGrid(dgv)
        AddText(dgv, "Time", "Time", width:=140, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Severity", "Severity", width:=80)
        AddText(dgv, "Type", "Type", width:=90)
        AddText(dgv, "Code", "Code", width:=70)
        AddText(dgv, "Message", "Message", width:=350)
        AddText(dgv, "Symbol", "Symbol", width:=90)
        AddText(dgv, "TID", "TID", width:=70)
        AddCheck(dgv, "Ack", "Ack", width:=50, isReadOnly:=False)
        AddText(dgv, "SnoozeUntil", "Snooze Until", width:=140, align:=DataGridViewContentAlignment.MiddleCenter)
    End Sub

    Public Sub BuildActivityColumns(dgv As DataGridView)
        SetupGrid(dgv)
        AddText(dgv, "Time", "Time", width:=140, align:=DataGridViewContentAlignment.MiddleCenter)
        AddText(dgv, "Source", "Source", width:=110)
        AddText(dgv, "Level", "Level", width:=80)
        AddText(dgv, "Message", "Message", width:=600)
        AddText(dgv, "Context", "Context", width:=200)
    End Sub

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Layout save/restore (JSON stored in %AppData%\GreekTrader\grid_layout_*.json)
    ' ─────────────────────────────────────────────────────────────────────────────

    Public Sub SaveGridLayout(dgv As DataGridView, layoutName As String)
        Try
            Dim dto As New GridLayout With {
                .Columns = New List(Of GridColLayout)()
            }

            For Each c As DataGridViewColumn In dgv.Columns
                dto.Columns.Add(New GridColLayout With {
                    .Name = c.Name,
                    .DisplayIndex = c.DisplayIndex,
                    .Width = c.Width,
                    .Visible = c.Visible,
                    .Frozen = c.Frozen
                })
            Next

            If dgv.SortedColumn IsNot Nothing Then
                dto.SortColumn = dgv.SortedColumn.Name
                dto.SortOrder = dgv.SortOrder.ToString()
            End If

            Dim options = New JsonSerializerOptions With {.WriteIndented = True}
            Dim json = JsonSerializer.Serialize(dto, options)
            Dim path = LayoutPath(layoutName)
            Dim dir = IO.Path.GetDirectoryName(path)
            If Not IO.Directory.Exists(dir) Then IO.Directory.CreateDirectory(dir)
            IO.File.WriteAllText(path, json)
        Catch
            ' swallow
        End Try
    End Sub

    Public Sub RestoreGridLayout(dgv As DataGridView, layoutName As String)
        Try
            Dim path = LayoutPath(layoutName)
            If Not IO.File.Exists(path) Then Return

            Dim json = IO.File.ReadAllText(path)
            Dim dto = JsonSerializer.Deserialize(Of GridLayout)(json)
            If dto Is Nothing OrElse dto.Columns Is Nothing Then Return

            ' Apply column properties
            For Each spec In dto.Columns
                If dgv.Columns.Contains(spec.Name) Then
                    Dim c = dgv.Columns(spec.Name)
                    If spec.DisplayIndex >= 0 AndAlso spec.DisplayIndex < dgv.Columns.Count Then
                        c.DisplayIndex = spec.DisplayIndex
                    End If
                    If spec.Width > 0 Then c.Width = spec.Width
                    c.Visible = spec.Visible
                    c.Frozen = spec.Frozen
                End If
            Next

            ' Apply sort if present
            If Not String.IsNullOrWhiteSpace(dto.SortColumn) AndAlso dgv.Columns.Contains(dto.SortColumn) Then
                Dim c = dgv.Columns(dto.SortColumn)
                Dim asc = String.Equals(dto.SortOrder, "Ascending", StringComparison.OrdinalIgnoreCase)
                Dim dir = If(asc, System.ComponentModel.ListSortDirection.Ascending, System.ComponentModel.ListSortDirection.Descending)
                dgv.Sort(c, dir)
            End If
        Catch
            ' swallow
        End Try
    End Sub

    Private Function LayoutPath(layoutName As String) As String
        Dim root = IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "GreekTrader")
        Return IO.Path.Combine(root, $"grid_layout_{layoutName}.json")
    End Function

    ' ─────────────────────────────────────────────────────────────────────────────
    ' Helpers
    ' ─────────────────────────────────────────────────────────────────────────────

    Private Sub SetupGrid(dgv As DataGridView)
        dgv.AutoGenerateColumns = False
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 255)
        EnableDoubleBuffer(dgv)
        dgv.Columns.Clear()
    End Sub

    Private Sub EnableDoubleBuffer(dgv As DataGridView)
        Try
            Dim t = dgv.GetType()
            Dim pi = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
            If pi IsNot Nothing Then pi.SetValue(dgv, True, Nothing)
        Catch
            ' ignore
        End Try
    End Sub

    Private Function AddText(dgv As DataGridView,
                             name As String,
                             header As String,
                             Optional dataProperty As String = Nothing,
                             Optional width As Integer = 80,
                             Optional format As String = Nothing,
                             Optional align As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleLeft,
                             Optional isReadOnly As Boolean = True,
                             Optional frozen As Boolean = False) As DataGridViewTextBoxColumn
        Dim col As New DataGridViewTextBoxColumn()
        col.Name = name
        col.HeaderText = header
        col.DataPropertyName = If(String.IsNullOrEmpty(dataProperty), name, dataProperty)
        col.ReadOnly = isReadOnly
        col.Width = width
        col.DefaultCellStyle.Alignment = align
        If Not String.IsNullOrEmpty(format) Then col.DefaultCellStyle.Format = format
        col.SortMode = DataGridViewColumnSortMode.Programmatic
        col.Frozen = frozen
        dgv.Columns.Add(col)
        Return col
    End Function

    Private Function AddCheck(dgv As DataGridView,
                              name As String,
                              header As String,
                              Optional dataProperty As String = Nothing,
                              Optional width As Integer = 60,
                              Optional isReadOnly As Boolean = False) As DataGridViewCheckBoxColumn
        Dim col As New DataGridViewCheckBoxColumn()
        col.Name = name
        col.HeaderText = header
        col.DataPropertyName = If(String.IsNullOrEmpty(dataProperty), name, dataProperty)
        col.ThreeState = False
        col.Width = width
        col.ReadOnly = isReadOnly
        col.SortMode = DataGridViewColumnSortMode.Programmatic
        dgv.Columns.Add(col)
        Return col
    End Function

    ' Layout DTOs
    Private Class GridLayout
        Public Property Columns As List(Of GridColLayout)
        Public Property SortColumn As String
        Public Property SortOrder As String
    End Class

    Private Class GridColLayout
        Public Property Name As String
        Public Property DisplayIndex As Integer
        Public Property Width As Integer
        Public Property Visible As Boolean
        Public Property Frozen As Boolean
    End Class

End Module
