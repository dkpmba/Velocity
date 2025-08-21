Imports Velocity.Core

Partial Class MainForm

    '===========================================
    '== LIVE MARKET DATA (Subs + Tick Routing) ==
    '===========================================
#Region "Live Market Data (Subs + Tick Routing)"

    ' --- TickType constants (subset) ---
    Private Const TT_BID As Integer = 1
    Private Const TT_ASK As Integer = 2
    Private Const TT_LAST As Integer = 4
    Private Const TT_DELAYED_BID As Integer = 66
    Private Const TT_DELAYED_ASK As Integer = 67
    Private Const TT_DELAYED_LAST As Integer = 68

    ' --- Quote snapshot per ConId ---
    Private Class MdQuote
        Public Bid As Double
        Public Ask As Double
        Public Last As Double
        Public LastUpdateUtc As Date
        Public ReadOnly Property Mid As Double
            Get
                'If Bid > 0 AndAlso Ask > 0 Then Return (Bid + Ask) / 2.0
                If Last > 0 Then Return Last
                Return 0
            End Get
        End Property
    End Class

    ' --- Subscriptions state ---
    Private Shared _mdNextTickerId As Integer = 300000
    Private Shared ReadOnly _mdTickerByConId As New Dictionary(Of Integer, Integer)() ' conId -> tickerId
    Private Shared ReadOnly _mdConIdByTicker As New Dictionary(Of Integer, Integer)() ' tickerId -> conId
    Private Shared ReadOnly _mdQuoteByConId As New Dictionary(Of Integer, MdQuote)()


    ''' <summary>Get a unique ticker id for reqMktData.</summary>
    Private Function Mkt_GetNextTickerId() As Integer
        _mdNextTickerId += 1
        Return _mdNextTickerId
    End Function

    ''' <summary>
    ''' Subscribe to market data for every ConId in _symbolsBinding (underlyings).
    ''' Uses SqlSymbolRepository.dictContract to build IB Contract. Idempotent per ConId.
    ''' Call this after Historical boot completes.
    ''' </summary>
    Public Sub EnsureSymbolSubscriptions()
        If _symbolsBinding Is Nothing OrElse _symbolsBinding.Count = 0 Then Exit Sub
        If TwsHost.Tws Is Nothing OrElse TwsHost.Tws.ClientSocket Is Nothing Then Exit Sub

        For Each anyRow As Object In _symbolsBinding
            If anyRow Is Nothing Then Continue For
            Dim conId As Integer = ExtractConId(anyRow)
            If conId <= 0 Then Continue For
            If _mdTickerByConId.ContainsKey(conId) Then Continue For ' already subscribed

            Dim c As IBApi.Contract = Nothing
            If Not SqlSymbolRepository.dictContract.TryGetValue(conId, c) Then Continue For

            Dim tid As Integer = Mkt_GetNextTickerId()
            _mdTickerByConId(conId) = tid
            _mdConIdByTicker(tid) = conId
            _mdQuoteByConId(conId) = New MdQuote()

            ' Empty genericTickList, snapshot:=False, regulatorySnapshot:=False, options:=Nothing
            TwsHost.Tws.ClientSocket.reqMktData(tid, c, "", False, False, Nothing)
        Next
    End Sub

    ''' <summary>
    ''' Subscribe to market data for any legs present in dgvMonitor / _tradesBinding,
    ''' but only if those CIDs exist in dictContract (keeps it simple).
    ''' You can call this together with EnsureSymbolSubscriptions().
    ''' </summary>
    Public Sub EnsureOpenLegSubscriptions()
        ' Prefer a binding list if you have one (e.g., _tradesBinding). Here we show a grid fallback.
        If dgvMonitor Is Nothing OrElse dgvMonitor.Rows Is Nothing Then Exit Sub
        If TwsHost.Tws Is Nothing OrElse TwsHost.Tws.ClientSocket Is Nothing Then Exit Sub

        For Each r As DataGridViewRow In dgvMonitor.Rows
            If r Is Nothing OrElse r.IsNewRow Then Continue For
            Dim conId As Integer = ExtractConIdFromRow(r)
            If conId <= 0 Then Continue For
            If _mdTickerByConId.ContainsKey(conId) Then Continue For

            Dim c As IBApi.Contract = Nothing
            If Not SqlSymbolRepository.dictContract.TryGetValue(conId, c) Then Continue For ' skip legs not in symbols cache

            Dim tid As Integer = Mkt_GetNextTickerId()
            _mdTickerByConId(conId) = tid
            _mdConIdByTicker(tid) = conId
            _mdQuoteByConId(conId) = New MdQuote()

            TwsHost.Tws.ClientSocket.reqMktData(tid, c, "", False, False, Nothing)
        Next
    End Sub

    ''' <summary>
    ''' Helper: extract ConId from a bound SymbolRow using common property names ("CID" or "sCID").
    ''' </summary>
    Private Function ExtractConId(anyRow As Object) As Integer
        If anyRow Is Nothing Then Return 0
        Dim t = anyRow.GetType()
        Dim pCID = t.GetProperty("CID")
        Dim pSCID = t.GetProperty("sCID")
        Dim val As Object = Nothing
        If pCID IsNot Nothing Then val = pCID.GetValue(anyRow, Nothing)
        If val Is Nothing AndAlso pSCID IsNot Nothing Then val = pSCID.GetValue(anyRow, Nothing)
        Dim cid As Integer
        If val IsNot Nothing AndAlso Integer.TryParse(Convert.ToString(val), cid) Then Return cid
        Return 0
    End Function

    ''' <summary>
    ''' Helper: extract ConId from a DataGridViewRow ("CID" or "sCID" column).
    ''' </summary>
    Private Function ExtractConIdFromRow(row As DataGridViewRow) As Integer
        If row Is Nothing OrElse row.IsNewRow Then Return 0
        Dim idx As Integer = HBoot_GetColumnIndexByName(dgvMonitor, "CID")
        If idx = -1 Then idx = HBoot_GetColumnIndexByName(dgvMonitor, "sCID")
        If idx = -1 Then Return 0
        Dim val = row.Cells(idx).Value
        Dim cid As Integer
        If val IsNot Nothing AndAlso Integer.TryParse(Convert.ToString(val), cid) Then Return cid
        Return 0
    End Function

    ' Returns True only for STK/FUT contracts (uses the repository cache)
    Private Shared Function IsBarEligible(conId As Integer) As Boolean
        Dim c As IBApi.Contract = Nothing
        If SqlSymbolRepository.dictContract.TryGetValue(conId, c) Then
            Dim st As String = If(c.SecType, "").ToUpperInvariant()
            Return (st = "STK" OrElse st = "FUT")
        End If
        Return False
    End Function

    Private Shared Function GetSecType(conId As Integer) As String
        Dim c As IBApi.Contract = Nothing
        If SqlSymbolRepository.dictContract.TryGetValue(conId, c) Then
            Return If(c.SecType, "")
        End If
        Return ""
    End Function

    ''' <summary>
    ''' Route a price tick (from TWSEvents) into quote cache and roll the current bar in BarManager.
    ''' Prefers mid (bid/ask) when available; otherwise uses last.
    ''' </summary>
    Friend Sub Mkt_OnTickPrice(tickerId As Integer, field As Integer, price As Double)
        Dim conId As Integer
        If Not _mdConIdByTicker.TryGetValue(tickerId, conId) Then Exit Sub

        Dim q As MdQuote = Nothing
        If Not _mdQuoteByConId.TryGetValue(conId, q) Then
            q = New MdQuote() : _mdQuoteByConId(conId) = q
        End If

        Select Case field
            Case TT_BID, TT_DELAYED_BID : q.Bid = price
            Case TT_ASK, TT_DELAYED_ASK : q.Ask = price
            Case TT_LAST, TT_DELAYED_LAST : q.Last = price
            Case Else
                ' ignore other fields
        End Select
        q.LastUpdateUtc = Date.UtcNow

        Quotes_MarkDirty(conId)

        Dim mark As Double = q.Mid
        If mark <= 0 AndAlso price > 0 Then mark = price
        If mark <= 0 Then Exit Sub

        If IsBarEligible(conId) Then
            Dim tf As String = HBoot_GetIbBarParamsFromCombo().barSize ' "1 min" or "5 mins"
            BarManager.UpdateCurrentBarFromTick(conId, tf, Date.UtcNow, mark)
        End If
    End Sub

    ''' <summary>
    ''' Route a size tick (currently we only use it to accumulate volume in the rolling bar).
    ''' </summary>
    Friend Sub Mkt_OnTickSize(tickerId As Integer, field As Integer, size As Integer)
        Dim conId As Integer
        If Not _mdConIdByTicker.TryGetValue(tickerId, conId) Then Exit Sub

        ' Optionally accumulate volume into rolling bar (IB size fields vary; keep simple)
        If size > 0 AndAlso IsBarEligible(conId) Then
            Dim tf As String = HBoot_GetIbBarParamsFromCombo().barSize
            BarManager.UpdateCurrentBarFromTick(conId, tf, Date.UtcNow,
            If(_mdQuoteByConId.ContainsKey(conId), _mdQuoteByConId(conId).Mid, 0), tickVolume:=size)
        End If
    End Sub

#End Region

    ' Flush previous minute bars at boundaries so thin symbols don’t miss completion
    Private _barFlushTimer As System.Windows.Forms.Timer

    Private Sub InitBarFlushTimer()
        If _barFlushTimer IsNot Nothing Then Return
        _barFlushTimer = New System.Windows.Forms.Timer()
        _barFlushTimer.Interval = 1000  ' 1 second is plenty
        AddHandler _barFlushTimer.Tick, AddressOf OnBarFlushTimerTick
        _barFlushTimer.Start()
    End Sub

    Private Sub OnBarFlushTimerTick(sender As Object, e As EventArgs)
        ' Ask BarManager to seal any stale current bars across all (ConId, TF)
        BarManager.FlushAllAtBoundary(Date.UtcNow)
    End Sub

    Private Sub StopBarFlushTimer()
        If _barFlushTimer Is Nothing Then Return
        RemoveHandler _barFlushTimer.Tick, AddressOf OnBarFlushTimerTick
        _barFlushTimer.Stop()
        _barFlushTimer = Nothing
    End Sub

    '===========================================
    '== QUOTES → GRIDS (throttled UI refresh) ==
    '===========================================
#Region "Quotes → Grids (UI Refresh)"

    ' Reuse your Shared MD maps; add a tiny dirty set
    Private Shared ReadOnly _mdDirtyConIds As New HashSet(Of Integer)()
    Private Shared ReadOnly _mdUiSync As New Object()

    ' 250 ms UI timer to push quote changes into the grids
    Private _mdUiTimer As System.Windows.Forms.Timer

    Private Sub InitQuotesUiTimer()
        If _mdUiTimer IsNot Nothing Then Return
        _mdUiTimer = New System.Windows.Forms.Timer()
        _mdUiTimer.Interval = 250
        AddHandler _mdUiTimer.Tick, AddressOf Quotes_UiTick
        _mdUiTimer.Start()
    End Sub

    Private Sub StopQuotesUiTimer()
        If _mdUiTimer Is Nothing Then Return
        RemoveHandler _mdUiTimer.Tick, AddressOf Quotes_UiTick
        _mdUiTimer.Stop()
        _mdUiTimer = Nothing
    End Sub

    ' Mark a conId as dirty whenever its quote changes
    Private Shared Sub Quotes_MarkDirty(conId As Integer)
        SyncLock _mdUiSync
            _mdDirtyConIds.Add(conId)
        End SyncLock
    End Sub

    ' Timer tick: push dirty quotes into all three grids
    Private Sub Quotes_UiTick(sender As Object, e As EventArgs)
        Dim toFlush As Integer()
        SyncLock _mdUiSync
            If _mdDirtyConIds.Count = 0 Then Exit Sub
            toFlush = _mdDirtyConIds.ToArray()
            _mdDirtyConIds.Clear()
        End SyncLock

        For Each cid In toFlush
            Dim q As MdQuote = Nothing
            If Not _mdQuoteByConId.TryGetValue(cid, q) OrElse q Is Nothing Then Continue For
            UpdateGridQuote(dgvSymbols, cid, q)
            UpdateGridQuote(dgvMonitor, cid, q)
            UpdateGridQuote(DesignForm.dgvDesign, cid, q)
        Next
    End Sub

    ' Update Bid/Ask/Last (and Mid if present) in a single grid for a given conId
    Private Sub UpdateGridQuote(grid As DataGridView, conId As Integer, q As MdQuote)
        If grid Is Nothing OrElse grid.Rows Is Nothing OrElse grid.Columns Is Nothing Then Exit Sub
        If grid.IsDisposed Then Exit Sub

        ' Find conId column and row
        Dim idxCID As Integer = HBoot_GetColumnIndexByName(grid, "CID")
        If idxCID = -1 Then idxCID = HBoot_GetColumnIndexByName(grid, "sCID")
        If idxCID = -1 Then Exit Sub

        Dim rowHit As DataGridViewRow = Nothing
        For Each r As DataGridViewRow In grid.Rows
            If r Is Nothing OrElse r.IsNewRow Then Continue For
            Dim val = r.Cells(idxCID).Value
            Dim cid As Integer
            If val IsNot Nothing AndAlso Integer.TryParse(Convert.ToString(val), cid) AndAlso cid = conId Then
                rowHit = r : Exit For
            End If
        Next
        If rowHit Is Nothing Then Exit Sub

        ' Locate quote columns (case-insensitive); skip if not present in a grid
        Dim idxBid As Integer = HBoot_GetColumnIndexByName(grid, "Bid")
        Dim idxAsk As Integer = HBoot_GetColumnIndexByName(grid, "Ask")
        Dim idxLast As Integer = HBoot_GetColumnIndexByName(grid, "Last")
        Dim idxMid As Integer = HBoot_GetColumnIndexByName(grid, "Mid") ' optional

        ' Simple formatting; tweak if you want per-symbol increments
        Dim sBid As String = If(q.Bid > 0, q.Bid.ToString("0.#####"), "")
        Dim sAsk As String = If(q.Ask > 0, q.Ask.ToString("0.#####"), "")
        Dim sLast As String = If(q.Last > 0, q.Last.ToString("0.#####"), "")
        Dim sMid As String = If(q.Mid > 0, q.Mid.ToString("0.#####"), "")

        If idxBid >= 0 Then rowHit.Cells(idxBid).Value = sBid
        If idxAsk >= 0 Then rowHit.Cells(idxAsk).Value = sAsk
        If idxLast >= 0 Then rowHit.Cells(idxLast).Value = sLast
        If idxMid >= 0 Then rowHit.Cells(idxMid).Value = sMid
    End Sub

#End Region


End Class
