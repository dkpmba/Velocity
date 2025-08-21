Option Strict On
Option Explicit On

Module PreTradeRisk
    ''' <summary>
    ''' Validate an order before sending (qty caps, price sanity, etc.).
    ''' For now, always ok. Wire real checks later.
    ''' </summary>
    Public Function Validate(conId As Integer, side As String, qty As Integer, orderType As String, lmt As Double?, aux As Double?) As (ok As Boolean, reason As String)
        If qty <= 0 Then Return (False, "Quantity must be > 0.")
        Return (True, "")
    End Function
End Module

