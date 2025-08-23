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

    ' Nudge selected LIMIT order up/down by N increments and resend
    Public Sub ModifySelectedOrderPrice(deltaSteps As Integer)
        If dgvOrders Is Nothing OrElse dgvOrders.CurrentRow Is Nothing Then Exit Sub
        Dim r = TryCast(dgvOrders.CurrentRow.DataBoundItem, OrderStateStore.OrderRow)
        If r Is Nothing Then Exit Sub

        If String.IsNullOrWhiteSpace(r.OrderType) OrElse r.OrderType.ToUpperInvariant() <> "LMT" Then
            AppendAlert("Only LIMIT orders can be modified by price.", "WARN") : Exit Sub
        End If
        If r.OrderId <= 0 OrElse r.ConId <= 0 Then Exit Sub

        Dim c As IBApi.Contract = ContractBuilder.FromConId(r.ConId)
        Dim inc As Double = GetIncrementFromSymbols(r.ConId)
        If inc <= 0 Then inc = 0.01 ' safe fallback

        Dim newLmt As Double = If(r.LmtPrice.HasValue, r.LmtPrice.Value, 0)
        If newLmt <= 0 Then newLmt = GetMark(r.ConId)
        If newLmt <= 0 Then
            AppendAlert("No reference price available to modify.", "WARN")
            Exit Sub
        End If

        newLmt += deltaSteps * inc
        newLmt = RoundToIncrement(newLmt, inc)

        Dim o As New IBApi.Order With {
        .OrderId = r.OrderId,     ' same id
        .Action = r.Side,
        .OrderType = "LMT",
        .TotalQuantity = If(r.Qty > 0, r.Qty, Math.Max(r.FilledQty + r.Remaining, 1)),
        .LmtPrice = newLmt,
        .Tif = If(String.IsNullOrWhiteSpace(r.Tif), "DAY", r.Tif),
        .Transmit = True,
        .OutsideRth = False,
        .Account = r.Account
    }

        OrderRouter.Replace(r.OrderId, c, o, $"Modify LMT by {deltaSteps}×inc")
    End Sub

    ' Increase/decrease TotalQuantity and resend the order (same OrderId)
    Public Sub ModifySelectedOrderQty(delta As Integer)
        If dgvOrders Is Nothing OrElse dgvOrders.CurrentRow Is Nothing Then Exit Sub
        Dim r = TryCast(dgvOrders.CurrentRow.DataBoundItem, OrderStateStore.OrderRow)
        If r Is Nothing Then Exit Sub
        If r.OrderId <= 0 OrElse r.ConId <= 0 Then Exit Sub

        ' Determine current total qty reliably
        Dim currentQty As Integer = r.Qty
        If currentQty <= 0 Then
            currentQty = If(r.TotalQty > 0, r.TotalQty, Math.Max(1, r.FilledQty + r.LeavesQty))
        End If

        Dim newQty As Integer = currentQty + delta
        Dim minQty As Integer = Math.Max(1, r.FilledQty) ' cannot be less than already filled
        If newQty < minQty Then
            AppendAlert($"New quantity {newQty} is below filled qty {r.FilledQty}.", "WARN")
            Exit Sub
        End If

        Dim c As IBApi.Contract = ContractBuilder.FromConId(r.ConId)

        Dim orderType As String = If(String.IsNullOrWhiteSpace(r.OrderType), "LMT", r.OrderType.ToUpperInvariant())
        Dim newLmt As Double = 0

        If orderType = "LMT" Then
            ' Keep current limit if available; else fall back to mark (rounded)
            newLmt = If(r.LmtPrice.HasValue, r.LmtPrice.Value, 0)
            If newLmt <= 0 Then
                Dim mark = GetMark(r.ConId)
                If mark > 0 Then
                    Dim inc = GetIncrementFromSymbols(r.ConId)
                    If inc > 0 Then mark = RoundToIncrement(mark, inc)
                End If
                newLmt = mark
            End If
            If newLmt <= 0 Then
                AppendAlert("Cannot determine limit price for quantity change.", "WARN")
                Exit Sub
            End If
        End If

        ' Optional risk check
        Dim risk = PreTradeRisk.Validate(r.ConId, r.Side, newQty, orderType, If(orderType = "LMT", newLmt, CType(Nothing, Double?)), Nothing)
        If Not risk.ok Then
            AppendAlert("Risk block: " & risk.reason, "WARN")
            Exit Sub
        End If

        Dim o As New IBApi.Order With {
        .OrderId = r.OrderId,                ' SAME order id
        .Action = r.Side,
        .OrderType = orderType,
        .TotalQuantity = newQty,
        .Tif = If(String.IsNullOrWhiteSpace(r.Tif), "DAY", r.Tif),
        .Transmit = True,
        .OutsideRth = False,
        .Account = r.Account
    }
        If orderType = "LMT" Then o.LmtPrice = newLmt

        OrderRouter.Replace(r.OrderId, c, o, $"Change qty by {delta}")
    End Sub

    Private Sub dgvOrders_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvOrders.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Up Then
            ModifySelectedOrderPrice(+1) : e.Handled = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.Down Then
            ModifySelectedOrderPrice(-1) : e.Handled = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.PageUp Then
            ModifySelectedOrderPrice(+5) : e.Handled = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.PageDown Then
            ModifySelectedOrderPrice(-5) : e.Handled = True
        End If
    End Sub

    ' ===== Orders grid context menu =====
    Private _ordersCms As ContextMenuStrip

    Private Sub WireOrdersContextMenu()
        If dgvOrders Is Nothing Then Exit Sub
        If _ordersCms IsNot Nothing Then Return

        _ordersCms = New ContextMenuStrip()

        ' Cancel
        Dim mCancel As New ToolStripMenuItem("Cancel Selected Order", Nothing, Sub() CancelSelectedOrder())
        mCancel.Name = "mCancel"

        ' Price submenu
        Dim mPrice As New ToolStripMenuItem("Modify Selected Order Price")
        mPrice.Name = "mPrice"
        mPrice.DropDownItems.Add("Price +1 inc", Nothing, Sub() ModifySelectedOrderPrice(+1))
        mPrice.DropDownItems.Add("Price -1 inc", Nothing, Sub() ModifySelectedOrderPrice(-1))
        mPrice.DropDownItems.Add(New ToolStripSeparator())
        mPrice.DropDownItems.Add("Price +5 inc", Nothing, Sub() ModifySelectedOrderPrice(+5))
        mPrice.DropDownItems.Add("Price -5 inc", Nothing, Sub() ModifySelectedOrderPrice(-5))

        ' Quantity submenu
        Dim mQty As New ToolStripMenuItem("Change Quantity")
        mQty.Name = "mQty"
        mQty.DropDownItems.Add("Increase +1", Nothing, Sub() ModifySelectedOrderQty(+1))
        mQty.DropDownItems.Add("Decrease -1", Nothing, Sub() ModifySelectedOrderQty(-1))
        mQty.DropDownItems.Add(New ToolStripSeparator())
        mQty.DropDownItems.Add("Increase +5", Nothing, Sub() ModifySelectedOrderQty(+5))
        mQty.DropDownItems.Add("Decrease -5", Nothing, Sub() ModifySelectedOrderQty(-5))

        _ordersCms.Items.AddRange(New ToolStripItem() {mCancel, mPrice, mQty})
        AddHandler _ordersCms.Opening, AddressOf OrdersCms_Opening

        dgvOrders.ContextMenuStrip = _ordersCms

        ' Ensure right-click selects the row under the cursor
        AddHandler dgvOrders.CellMouseDown, AddressOf dgvOrders_CellMouseDownForContext
    End Sub

    Private Sub dgvOrders_CellMouseDownForContext(sender As Object, e As DataGridViewCellMouseEventArgs)
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            dgvOrders.ClearSelection()
            dgvOrders.CurrentCell = dgvOrders(e.ColumnIndex, e.RowIndex)
            dgvOrders.Rows(e.RowIndex).Selected = True
        End If
    End Sub

    ' Enable/disable menu items based on the selected row
    Private Sub OrdersCms_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs)
        Dim r As OrderStateStore.OrderRow = Nothing
        Dim hasRow As Boolean = (dgvOrders IsNot Nothing AndAlso dgvOrders.CurrentRow IsNot Nothing)
        If hasRow Then r = TryCast(dgvOrders.CurrentRow.DataBoundItem, OrderStateStore.OrderRow)

        Dim canAct As Boolean = (r IsNot Nothing)
        Dim isWorking As Boolean = canAct AndAlso
        (String.Equals(r.Status, "Submitted", StringComparison.OrdinalIgnoreCase) OrElse
         String.Equals(r.Status, "PreSubmitted", StringComparison.OrdinalIgnoreCase) OrElse
         String.Equals(r.Status, "PartiallyFilled", StringComparison.OrdinalIgnoreCase))

        Dim mCancel = TryCast(_ordersCms.Items("mCancel"), ToolStripMenuItem)
        Dim mPrice = TryCast(_ordersCms.Items("mPrice"), ToolStripMenuItem)
        Dim mQty = TryCast(_ordersCms.Items("mQty"), ToolStripMenuItem)

        If mCancel IsNot Nothing Then
            mCancel.Enabled = canAct AndAlso isWorking AndAlso
                          Not String.Equals(r.Status, "Cancelled", StringComparison.OrdinalIgnoreCase) AndAlso
                          Not String.Equals(r.Status, "Filled", StringComparison.OrdinalIgnoreCase) AndAlso
                          Not String.Equals(r.Status, "Inactive", StringComparison.OrdinalIgnoreCase)
        End If

        If mPrice IsNot Nothing Then
            mPrice.Enabled = canAct AndAlso isWorking AndAlso
                         Not String.IsNullOrWhiteSpace(r.OrderType) AndAlso
                         String.Equals(r.OrderType, "LMT", StringComparison.OrdinalIgnoreCase)
        End If

        If mQty IsNot Nothing Then
            mQty.Enabled = canAct AndAlso isWorking
        End If
    End Sub

    ' ===== Monitor grid context menu (positions/legs) =====
    Private _monitorCms As ContextMenuStrip

    Private Sub WireMonitorContextMenu()
        If dgvMonitor Is Nothing Then Exit Sub
        If _monitorCms IsNot Nothing Then Return

        _monitorCms = New ContextMenuStrip()

        ' --- Close submenu ---
        Dim mClose As New ToolStripMenuItem("Close Position")
        mClose.Name = "mClose"
        mClose.DropDownItems.Add("Close ALL", Nothing, Sub() Monitor_CloseAll())
        mClose.DropDownItems.Add("Close CUSTOM…", Nothing, Sub() Monitor_CloseCustom())

        ' --- Add submenu ---
        Dim mAdd As New ToolStripMenuItem("Add to Position")
        mAdd.Name = "mAdd"
        mAdd.DropDownItems.Add("Add +1", Nothing, Sub() Monitor_AddDelta(+1))
        mAdd.DropDownItems.Add("Add -1", Nothing, Sub() Monitor_AddDelta(-1)) ' (reduce / flip)
        mAdd.DropDownItems.Add(New ToolStripSeparator())
        mAdd.DropDownItems.Add("Add +5", Nothing, Sub() Monitor_AddDelta(+5))
        mAdd.DropDownItems.Add("Add -5", Nothing, Sub() Monitor_AddDelta(-5))
        mAdd.DropDownItems.Add(New ToolStripSeparator())
        mAdd.DropDownItems.Add("Add CUSTOM…", Nothing, Sub() Monitor_AddCustom())

        _monitorCms.Items.AddRange(New ToolStripItem() {mClose, mAdd})
        AddHandler _monitorCms.Opening, AddressOf MonitorCms_Opening

        dgvMonitor.ContextMenuStrip = _monitorCms

        ' Right-click should select the row under cursor
        AddHandler dgvMonitor.CellMouseDown, AddressOf dgvMonitor_CellMouseDownForContext
    End Sub

    Private Sub dgvMonitor_CellMouseDownForContext(sender As Object, e As DataGridViewCellMouseEventArgs)
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            dgvMonitor.ClearSelection()
            dgvMonitor.CurrentCell = dgvMonitor(e.ColumnIndex, e.RowIndex)
            dgvMonitor.Rows(e.RowIndex).Selected = True
        End If
    End Sub

    Private Sub MonitorCms_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs)
        Dim hasRow As Boolean = (dgvMonitor IsNot Nothing AndAlso dgvMonitor.CurrentRow IsNot Nothing)
        Dim posQty As Integer = If(hasRow, GetMonitorPositionQty(dgvMonitor.CurrentRow), 0)
        Dim isPosition As Boolean = (posQty <> 0)

        Dim mClose = TryCast(_monitorCms.Items("mClose"), ToolStripMenuItem)
        Dim mAdd = TryCast(_monitorCms.Items("mAdd"), ToolStripMenuItem)

        If mClose IsNot Nothing Then mClose.Enabled = isPosition
        If mAdd IsNot Nothing Then mAdd.Enabled = hasRow  ' allow even if 0 (start new)
    End Sub

    ' Pull ConId from dgvMonitor row (CID or sCID column)
    Private Function Monitor_ConId() As Integer
        If dgvMonitor Is Nothing OrElse dgvMonitor.CurrentRow Is Nothing Then Return 0
        Return ExtractConIdFromRow(dgvMonitor.CurrentRow) ' you already have this helper
    End Function

    ' Heuristic: try common column names for position qty
    Private Function GetMonitorPositionQty(row As DataGridViewRow) As Integer
        If row Is Nothing Then Return 0
        Dim names = New String() {"Qty", "Quantity", "OpenQty", "Position", "Pos", "Lots"}
        For Each nm In names
            Dim idx = HBoot_GetColumnIndexByName(dgvMonitor, nm)
            If idx >= 0 Then
                Dim v = row.Cells(idx).Value
                Dim n As Integer
                If v IsNot Nothing AndAlso Integer.TryParse(v.ToString(), n) Then Return n
            End If
        Next
        ' If your TradeLeg object is bound, try reflection:
        Dim leg = TryCast(row.DataBoundItem, Object)
        If leg IsNot Nothing Then
            For Each nm In names
                Dim p = leg.GetType().GetProperty(nm)
                If p IsNot Nothing Then
                    Dim v = p.GetValue(leg, Nothing)
                    Dim n As Integer
                    If v IsNot Nothing AndAlso Integer.TryParse(v.ToString(), n) Then Return n
                End If
            Next
        End If
        Return 0
    End Function

    ' BUY to close if short (<0), SELL to close if long (>0)
    Private Function CloseSide(positionQty As Integer) As String
        Return If(positionQty > 0, "SELL", "BUY")
    End Function

    ' BUY to add if long (>=0), SELL to add if short (<0)
    Private Function AddSide(positionQty As Integer) As String
        Return If(positionQty >= 0, "BUY", "SELL")
    End Function

    Private Sub Monitor_CloseAll()
        Dim cid = Monitor_ConId() : If cid <= 0 Then Exit Sub
        Dim pos = GetMonitorPositionQty(dgvMonitor.CurrentRow)
        If pos = 0 Then
            AppendAlert("No open position in selected row.", "WARN") : Exit Sub
        End If
        Dim side = CloseSide(pos)
        Dim qty As Integer = Math.Abs(pos)

        Monitor_SendWithMarginCheck(cid, side, qty)
    End Sub

    Private Sub Monitor_CloseCustom()
        Dim cid = Monitor_ConId() : If cid <= 0 Then Exit Sub
        Dim pos = GetMonitorPositionQty(dgvMonitor.CurrentRow)
        If pos = 0 Then
            AppendAlert("No open position in selected row.", "WARN") : Exit Sub
        End If
        Dim maxQty = Math.Abs(pos)
        Dim q = UiPrompts.AskQuantity("Close Position", $"Enter close quantity (max {maxQty}):", 1, maxQty, maxQty)
        If Not q.HasValue Then Exit Sub

        Dim side = CloseSide(pos)
        Monitor_SendWithMarginCheck(cid, side, q.Value)
    End Sub

    Private Sub Monitor_AddCustom()
        Dim cid = Monitor_ConId() : If cid <= 0 Then Exit Sub
        Dim pos = GetMonitorPositionQty(dgvMonitor.CurrentRow)

        Dim defaultSide As String = AddSide(pos) ' BUY if long/flat, SELL if short
        Dim ans = UiPrompts.AskAddQuantity("Add to Position",
                                       "Select side and enter quantity to add:",
                                       defaultSide, 1)
        If Not ans.ok Then Exit Sub

        Monitor_SendWithMarginCheck(cid, ans.side, ans.qty)
    End Sub


    Private Sub Monitor_AddDelta(delta As Integer)
        Dim cid = Monitor_ConId() : If cid <= 0 Then Exit Sub
        Dim pos = GetMonitorPositionQty(dgvMonitor.CurrentRow)
        ' Positive delta means add towards BUY; negative towards SELL.
        Dim side = If(delta >= 0, AddSide(pos), If(AddSide(pos) = "BUY", "SELL", "BUY"))
        Dim q As Integer = Math.Abs(delta)
        Monitor_SendWithMarginCheck(cid, side, q)
    End Sub

    ' Build a LMT near bid/ask, then run a margin "what-if" preview before placing
    Private Sub Monitor_SendWithMarginCheck(conId As Integer, side As String, qty As Integer)
        Dim c As IBApi.Contract = ContractBuilder.FromConId(conId)
        Dim lmt As Double = DetermineLimitPrice(conId, side, useMid:=False)
        Dim inc = GetIncrementFromSymbols(conId)
        If inc > 0 Then lmt = RoundToIncrement(lmt, inc)
        If lmt <= 0 Then
            AppendAlert("No market price available to compute limit.", "WARN") : Exit Sub
        End If

        Dim risk = PreTradeRisk.Validate(conId, side, qty, "LMT", lmt, Nothing)
        If Not risk.ok Then
            AppendAlert("Risk block: " & risk.reason, "WARN") : Exit Sub
        End If

        Dim tid As Integer = Monitor_TradeId()

        MarginPreview.PlaceWithMarginCheck(
        contract:=c,
        side:=side,
        qty:=qty,
        orderType:="LMT",
        lmt:=lmt,
        tif:="DAY",
        account:=Nothing,
        note:=$"dgvMonitor {side} {qty} LMT@{lmt}",
        tradeId:=If(tid > 0, CType(tid, Integer?), Nothing)
    )
    End Sub


    Private Function Monitor_TradeId() As Integer
        If dgvMonitor Is Nothing OrElse dgvMonitor.CurrentRow Is Nothing Then Return 0
        ' Try common column names
        For Each nm In New String() {"TID", "TradeId", "TradeID"}
            Dim idx = HBoot_GetColumnIndexByName(dgvMonitor, nm)
            If idx >= 0 Then
                Dim v = dgvMonitor.Rows(dgvMonitor.CurrentRow.Index).Cells(idx).Value
                Dim n As Integer
                If v IsNot Nothing AndAlso Integer.TryParse(v.ToString(), n) Then Return n
            End If
        Next
        ' Try DataBoundItem via reflection
        Dim item = dgvMonitor.CurrentRow.DataBoundItem
        If item IsNot Nothing Then
            Dim p = item.GetType().GetProperty("TID") : If p Is Nothing Then p = item.GetType().GetProperty("TradeId")
            If p IsNot Nothing Then
                Dim v = p.GetValue(item, Nothing)
                Dim n As Integer
                If v IsNot Nothing AndAlso Integer.TryParse(v.ToString(), n) Then Return n
            End If
        End If
        Return 0
    End Function

    Private Sub WireTradeRgl()
        If _tradeRglWired Then Exit Sub
        AddHandler TradePnLManager.TradeRglChanged, AddressOf OnTradeRglChanged
        _tradeRglWired = True
    End Sub


    Private Sub OnTradeRglChanged(tradeId As Integer, newRgl As Double, delta As Double)
        ' 1) Update dgvTrades row (assuming your Trade object has a property "RGL" and "TID")
        If _tradesBinding IsNot Nothing Then
            For i = 0 To _tradesBinding.Count - 1
                Dim t = _tradesBinding(i)
                Dim tidProp = t.GetType().GetProperty("TID")
                If tidProp IsNot Nothing Then
                    Dim tidVal = tidProp.GetValue(t, Nothing)
                    If tidVal IsNot Nothing AndAlso CInt(tidVal) = tradeId Then
                        Dim rglProp = t.GetType().GetProperty("RGL")
                        If rglProp IsNot Nothing Then
                            Dim cur = 0D
                            Dim tmp = rglProp.GetValue(t, Nothing)
                            If tmp IsNot Nothing Then Decimal.TryParse(tmp.ToString(), cur)
                            rglProp.SetValue(t, CDec(newRgl))
                            _tradesBinding.ResetItem(i)
                        End If
                        Exit For
                    End If
                End If
            Next
        End If

        ' 2) Persist to DB (uncomment and adjust to your repo signature)
        Try
            If Global.AppServices.Trades IsNot Nothing Then
                Global.AppServices.Trades.UpdateRglDelta(tradeId, CDec(delta))
            End If
        Catch ex As Exception
            AppendAlert("Persist RGL failed: " & ex.Message, "ERROR")
        End Try
    End Sub

End Class
