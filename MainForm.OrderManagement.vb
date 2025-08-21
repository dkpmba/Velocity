Partial Class MainForm
    ' --- Helpers to read current mark and increment ---
    Private Function GetMark(conId As Integer) As Double
        Dim q As Object = Nothing
        If _mdQuoteByConId.TryGetValue(conId, q) AndAlso q IsNot Nothing Then
            Dim m As Double = q.Mid
            If m <= 0 AndAlso q.Last > 0 Then m = q.Last
            If m <= 0 AndAlso q.Bid > 0 AndAlso q.Ask > 0 Then m = (q.Bid + q.Ask) / 2.0
            Return m
        End If
        Return 0
    End Function

    Private Function GetIncrementFromSymbols(conId As Integer) As Double
        If _symbolsBinding Is Nothing Then Return 0
        For Each anyRow In _symbolsBinding
            If ExtractConId(anyRow) = conId Then
                Dim t = anyRow.GetType()
                Dim p = t.GetProperty("Increment")
                If p IsNot Nothing Then
                    Dim v = p.GetValue(anyRow, Nothing)
                    Dim d As Double
                    If v IsNot Nothing AndAlso Double.TryParse(v.ToString(), d) Then Return d
                End If
            End If
        Next
        Return 0
    End Function

    Private Function RoundToIncrement(px As Double, inc As Double) As Double
        If inc <= 0 Then Return px
        Dim steps As Double = Math.Round(px / inc)
        Return steps * inc
    End Function

    ' --- Build a limit price from quotes (Bid/Ask/Mid) ---
    Private Function DetermineLimitPrice(conId As Integer, side As String, useMid As Boolean) As Double
        Dim bid As Double = 0, ask As Double = 0, last As Double = 0, mid As Double = 0
        Dim q As MdQuote = Nothing
        If _mdQuoteByConId.TryGetValue(conId, q) AndAlso q IsNot Nothing Then
            bid = q.Bid : ask = q.Ask : last = q.Last : mid = q.Mid
        End If

        Dim px As Double = 0
        Dim s = side.Trim().ToUpperInvariant()
        If useMid AndAlso mid > 0 Then
            px = mid
        ElseIf s = "BUY" Then
            px = If(bid > 0, bid, If(last > 0, last, mid))
        Else
            px = If(ask > 0, ask, If(last > 0, last, mid))
        End If

        Dim inc = GetIncrementFromSymbols(conId)
        px = RoundToIncrement(px, inc)
        Return px
    End Function

    ' --- Place from a selected symbols-row ---
    Public Sub PlaceLimitFromSelectedSymbol(side As String, qty As Integer, Optional useMid As Boolean = False)
        If dgvSymbols Is Nothing OrElse dgvSymbols.CurrentRow Is Nothing Then Exit Sub
        Dim conId As Integer = ExtractConIdFromRow(dgvSymbols.CurrentRow)
        If conId <= 0 Then Exit Sub

        Dim c As IBApi.Contract = ContractBuilder.FromConId(conId)
        Dim lmt As Double = DetermineLimitPrice(conId, side, useMid)
        If lmt <= 0 Then
            AppendAlert("No price available to place order.", "WARN")
            Exit Sub
        End If

        Dim risk = PreTradeRisk.Validate(conId, side, qty, "LMT", lmt, Nothing)
        If Not risk.ok Then
            AppendAlert("Risk block: " & risk.reason, "WARN")
            Exit Sub
        End If

        Dim o As New IBApi.Order With {
        .Action = side.ToUpperInvariant(),
        .OrderType = "LMT",
        .TotalQuantity = qty,
        .LmtPrice = lmt,
        .Tif = "DAY",
        .Transmit = True,
        .OutsideRth = False
    }

        OrderRouter.Place(c, o, note:=$"Symbols {side} LMT {qty}@{lmt}", source:="UI")
    End Sub

    ' --- Cancel selected from Orders grid ---
    Public Sub CancelSelectedOrder()
        If dgvOrders Is Nothing OrElse dgvOrders.CurrentRow Is Nothing Then Exit Sub
        Dim r As OrderStateStore.OrderRow = TryCast(dgvOrders.CurrentRow.DataBoundItem, OrderStateStore.OrderRow)
        If r Is Nothing Then Exit Sub
        If r.OrderId <= 0 Then Exit Sub
        OrderRouter.Cancel(r.OrderId, "UI cancel")
    End Sub

End Class
