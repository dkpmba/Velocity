Option Strict On
Option Explicit On

Imports IBApi
Imports System.Windows.Forms

Module MarginPreview

    Private Class PreviewPlan
        Public Property Contract As Contract
        Public Property RealOrder As IBApi.Order
        Public Property Note As String
        Public Property TradeId As Integer?
    End Class


    ' Map preview (what-if) orderId -> plan to execute if approved
    Private ReadOnly _pending As New Dictionary(Of Integer, PreviewPlan)()

    ''' <summary>
    ''' Send a WHAT-IF order (no transmit) to retrieve margin impact. On openOrder,
    ''' we will prompt the user and, if approved and no deficiency warning, place the real order.
    ''' </summary>
    Public Sub PlaceWithMarginCheck(contract As Contract,
                                side As String,
                                qty As Integer,
                                orderType As String,
                                lmt As Double,
                                tif As String,
                                account As String,
                                note As String,
                                Optional tradeId As Integer? = Nothing)

        If TwsHost.Tws Is Nothing OrElse TwsHost.Tws.ClientSocket Is Nothing Then
            Throw New InvalidOperationException("TWS is not connected.")
        End If

        ' 1) Build WHAT-IF preview order
        Dim previewId As Integer = OrderRouter.NextOrderId()
        Dim preview As New IBApi.Order With {
            .OrderId = previewId,
            .Action = side.ToUpperInvariant(),
            .OrderType = orderType.ToUpperInvariant(),
            .TotalQuantity = qty,
            .Tif = If(String.IsNullOrWhiteSpace(tif), "DAY", tif),
            .LmtPrice = If(orderType.Equals("LMT", StringComparison.OrdinalIgnoreCase), lmt, 0),
            .WhatIf = True,
            .Transmit = False,
            .OutsideRth = False,
            .Account = account
        }

        ' 2) Build the REAL order (no id yet; router will allocate if 0)
        Dim realOrder As New IBApi.Order With {
            .Action = side.ToUpperInvariant(),
            .OrderType = orderType.ToUpperInvariant(),
            .TotalQuantity = qty,
            .Tif = If(String.IsNullOrWhiteSpace(tif), "DAY", tif),
            .LmtPrice = If(orderType.Equals("LMT", StringComparison.OrdinalIgnoreCase), lmt, 0),
            .WhatIf = False,
            .Transmit = True,
            .OutsideRth = False,
            .Account = account
        }

        _pending(previewId) = New PreviewPlan With {
            .Contract = contract,
            .RealOrder = realOrder,
            .Note = note,
            .TradeId = tradeId
        }

        ' 3) Send WHAT-IF to IB
        TwsHost.Tws.ClientSocket.placeOrder(previewId, contract, preview)
    End Sub

    ''' <summary>
    ''' Called from TWSEvents.openOrder for WHAT-IF previews.
    ''' Shows margin lines, blocks on warning, or asks user to proceed.
    ''' </summary>
    ' Replace the contents of OnWhatIfOpenOrder with this (or just edit the lines shown)
    Public Sub OnWhatIfOpenOrder(orderId As Integer, c As Contract, o As IBApi.Order, st As IBApi.OrderState)
        If o Is Nothing OrElse Not o.WhatIf Then Exit Sub
        Dim plan As PreviewPlan = Nothing
        If Not _pending.TryGetValue(orderId, plan) Then Exit Sub

        ' Pull margin fields available on most API versions
        Dim initChg As String = If(st IsNot Nothing, st.InitMarginChange, Nothing)
        Dim maintChg As String = If(st IsNot Nothing, st.MaintMarginChange, Nothing)
        Dim ewlChg As String = If(st IsNot Nothing, st.EquityWithLoanChange, Nothing)
        Dim warn As String = If(st IsNot Nothing, st.WarningText, Nothing)

        ' Some IB API versions don't expose Commission in OrderState; try reflect, else blank
        Dim comm As String = TryGetOrderStateProp(st, "Commission") ' may be Nothing

        ' Deficiency detection (textual)
        Dim hasDeficiency As Boolean = Not String.IsNullOrWhiteSpace(warn) AndAlso
                                   warn.ToLowerInvariant().Contains("insufficient")

        ' Build message lines conditionally
        Dim hdr As String = $"What-If Margin Preview for {o.Action} {CInt(o.TotalQuantity)} {c.Symbol} {o.OrderType}@" &
                        $"{If(o.OrderType.Equals("LMT", StringComparison.OrdinalIgnoreCase), o.LmtPrice.ToString("0.#####"), "MKT")}:"
        Dim lines As New List(Of String) From {
        hdr,
        $"",
        $"Initial Margin Change:   {initChg}",
        $"Maintenance Margin Chg:  {maintChg}",
        $"Equity w/ Loan Change:   {ewlChg}"
    }
        If Not String.IsNullOrWhiteSpace(comm) Then
            lines.Add($"Estimated Commission:    {comm}")
        End If
        lines.Add($"Warnings:                {warn}")

        Dim msg As String = String.Join(Environment.NewLine, lines)

        If hasDeficiency Then
            MessageBox.Show(msg & vbCrLf & vbCrLf & "Order blocked due to margin deficiency.",
                        "Margin Check", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _pending.Remove(orderId)
            Exit Sub
        End If

        Dim res = MessageBox.Show(msg & vbCrLf & vbCrLf & "Proceed to place the order?",
                              "Margin Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If res = DialogResult.Yes Then
            OrderRouter.Place(plan.Contract, plan.RealOrder, plan.Note, source:="dgvMonitor", tradeId:=plan.TradeId)
        End If

        _pending.Remove(orderId)
    End Sub

    ' Put this helper inside MarginPreview module (anywhere above End Module)
    Private Function TryGetOrderStateProp(st As IBApi.OrderState, propName As String) As String
        If st Is Nothing Then Return Nothing
        Try
            Dim p = st.GetType().GetProperty(propName, Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public)
            If p Is Nothing Then Return Nothing
            Dim v = p.GetValue(st, Nothing)
            Return If(v Is Nothing, Nothing, v.ToString())
        Catch
            Return Nothing
        End Try
    End Function

End Module
