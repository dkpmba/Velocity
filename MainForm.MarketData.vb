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
    Private _tradeRglWired As Boolean = False



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
            UiQuotesTimer_Tick_UpdateUrgl()
            UpdateUrglForTrades()
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

    Private Sub UiQuotesTimer_Tick_UpdateUrgl()
        If dgvMonitor IsNot Nothing AndAlso dgvMonitor.Rows.Count > 0 Then
            For Each r As DataGridViewRow In dgvMonitor.Rows
                If Not r.IsNewRow Then UpdateUrglForMonitorRow(r)
            Next
        End If
    End Sub

    ' Signed leg qty from row (tries “Qty/Quantity/OpenQty/Position/Pos/Lots”; uses “Side” if sign is missing)
    Private Function GetRowSignedQty(row As DataGridViewRow) As Integer
        If row Is Nothing Then Return 0
        Dim qtyNames = New String() {"Qty", "Quantity", "OpenQty", "Position", "Pos", "Lots"}
        Dim val As Integer = 0
        For Each nm In qtyNames
            Dim idx = HBoot_GetColumnIndexByName(dgvMonitor, nm)
            If idx >= 0 Then
                Dim cell = row.Cells(idx)
                If cell IsNot Nothing AndAlso cell.Value IsNot Nothing AndAlso Integer.TryParse(cell.Value.ToString(), val) Then
                    Exit For
                End If
            End If
        Next
        ' If sign is unclear, try “Side”
        If val > 0 Then
            Dim sideIdx = HBoot_GetColumnIndexByName(dgvMonitor, "Side")
            If sideIdx >= 0 Then
                Dim side = CStr(dgvMonitor.Rows(row.Index).Cells(sideIdx).Value)?.Trim().ToUpperInvariant()
                If side = "SELL" OrElse side = "SHORT" Then val = -Math.Abs(val)
            End If
        End If
        Return val
    End Function

    ' Update URGL for one monitor row (writes to “URGL” column if present, else updates bound object property if it exists)
    Private Sub UpdateUrglForMonitorRow(row As DataGridViewRow)
        If row Is Nothing Then Exit Sub
        Dim conId As Integer = ExtractConIdFromRow(row)
        If conId <= 0 Then Exit Sub

        ' mark: prefer Mid, then Last
        Dim mark As Double = GetMark(conId)
        If mark <= 0 Then Exit Sub

        ' position snapshot
        Dim snap As PositionStore.PositionSnap = Nothing
        If Not PositionStore.TryGet(conId, snap) OrElse snap Is Nothing Then Exit Sub

        ' leg signed qty (if your monitor qty is signed, this will keep sign; otherwise side will fix it)
        Dim legQty As Integer = GetRowSignedQty(row)
        If legQty = 0 Then Exit Sub

        ' per-leg URGL using account-level avg cost (good approximation when conId is unique to a trade)
        Dim urgl As Double = (mark - snap.AvgCost) * legQty * snap.Multiplier

        ' write to grid cell if present
        Dim urglIdx = HBoot_GetColumnIndexByName(dgvMonitor, "URGL")
        If urglIdx >= 0 Then
            row.Cells(urglIdx).Value = urgl
        Else
            ' or update bound object property "URGL" if it exists
            Dim item = row.DataBoundItem
            If item IsNot Nothing Then
                Dim p = item.GetType().GetProperty("URGL")
                If p IsNot Nothing AndAlso p.CanWrite Then
                    p.SetValue(item, urgl, Nothing)
                    ' if you keep a BindingList(Of TradeLeg), you may need to ResetItem on its index
                End If
            End If
        End If
    End Sub

    ' Sum per-leg URGLs for each TID and write into dgvTrades "URGL"
    Private Sub UpdateUrglForTrades()
        If dgvTrades Is Nothing OrElse dgvMonitor Is Nothing Then Exit Sub

        ' Build TID -> URGL sum from monitor rows
        Dim urglByTid As New Dictionary(Of Integer, Double)()
        For Each r As DataGridViewRow In dgvMonitor.Rows
            If r.IsNewRow Then Continue For
            ' TID column name candidates
            Dim tidIdx = HBoot_GetColumnIndexByName(dgvMonitor, "TID")
            If tidIdx < 0 Then tidIdx = HBoot_GetColumnIndexByName(dgvMonitor, "TradeId")
            If tidIdx < 0 Then Continue For

            Dim tidObj = r.Cells(tidIdx).Value
            Dim tid As Integer
            If tidObj Is Nothing OrElse Not Integer.TryParse(tidObj.ToString(), tid) OrElse tid <= 0 Then Continue For

            ' If the row already has URGL cell filled by UpdateUrglForMonitorRow, reuse it; otherwise compute now
            Dim urglVal As Double
            Dim urglIdx = HBoot_GetColumnIndexByName(dgvMonitor, "URGL")
            If urglIdx >= 0 AndAlso r.Cells(urglIdx).Value IsNot Nothing AndAlso Double.TryParse(r.Cells(urglIdx).Value.ToString(), urglVal) Then
                ' ok
            Else
                ' compute inline (same as monitor helper)
                Dim conId As Integer = ExtractConIdFromRow(r)
                Dim mark As Double = GetMark(conId)
                Dim snap As PositionStore.PositionSnap = Nothing
                If mark > 0 AndAlso PositionStore.TryGet(conId, snap) AndAlso snap IsNot Nothing Then
                    Dim legQty As Integer = GetRowSignedQty(r)
                    urglVal = (mark - snap.AvgCost) * legQty * snap.Multiplier
                Else
                    urglVal = 0
                End If
            End If

            urglByTid(tid) = If(urglByTid.ContainsKey(tid), urglByTid(tid) + urglVal, urglVal)
        Next

        ' Write totals into dgvTrades "URGL"
        Dim urglTradeIdx = HBoot_GetColumnIndexByName(dgvTrades, "URGL")
        For Each tr As DataGridViewRow In dgvTrades.Rows
            If tr.IsNewRow Then Continue For
            Dim tidIdx = HBoot_GetColumnIndexByName(dgvTrades, "TID")
            If tidIdx < 0 Then tidIdx = HBoot_GetColumnIndexByName(dgvTrades, "TradeId")
            If tidIdx < 0 Then Continue For

            Dim tidObj = tr.Cells(tidIdx).Value
            Dim tid As Integer
            If tidObj Is Nothing OrElse Not Integer.TryParse(tidObj.ToString(), tid) Then Continue For

            Dim tot As Double = 0
            If urglByTid.TryGetValue(tid, tot) Then
                If urglTradeIdx >= 0 Then
                    tr.Cells(urglTradeIdx).Value = tot
                Else
                    ' or push into bound Trade object property "URGL"
                    Dim item = tr.DataBoundItem
                    If item IsNot Nothing Then
                        Dim p = item.GetType().GetProperty("URGL")
                        If p IsNot Nothing AndAlso p.CanWrite Then
                            p.SetValue(item, tot, Nothing)
                        End If
                    End If
                End If
            End If
        Next
    End Sub


#End Region


End Class
