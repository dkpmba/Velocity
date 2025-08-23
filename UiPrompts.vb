Option Strict On
Option Explicit On

Imports System.Windows.Forms
Imports System.Drawing

Module UiPrompts

    ''' <summary>
    ''' Ask the user for a positive quantity between [minQty, maxQty].
    ''' Returns Nothing if user cancels.
    ''' </summary>
    Public Function AskQuantity(title As String,
                                message As String,
                                minQty As Integer,
                                maxQty As Integer,
                                defaultQty As Integer) As Integer?

        If minQty < 1 Then minQty = 1
        If maxQty < minQty Then maxQty = minQty
        If defaultQty < minQty Then defaultQty = minQty
        If defaultQty > maxQty Then defaultQty = maxQty

        Using f As New Form()
            f.Text = title
            f.FormBorderStyle = FormBorderStyle.FixedDialog
            f.MaximizeBox = False
            f.MinimizeBox = False
            f.StartPosition = FormStartPosition.CenterScreen
            f.ClientSize = New Size(360, 140)
            f.TopMost = False
            f.ShowInTaskbar = False

            Dim lbl As New Label() With {
                .Text = message,
                .AutoSize = False,
                .Location = New Point(12, 12),
                .Size = New Size(336, 40)
            }
            f.Controls.Add(lbl)

            Dim nud As New NumericUpDown() With {
                .Minimum = minQty,
                .Maximum = maxQty,
                .Value = defaultQty,
                .Location = New Point(15, 60),
                .Size = New Size(120, 24),
                .ThousandsSeparator = True
            }
            f.Controls.Add(nud)

            Dim btnOk As New Button() With {.Text = "OK", .DialogResult = DialogResult.OK, .Location = New Point(190, 90), .Size = New Size(75, 26)}
            Dim btnCancel As New Button() With {.Text = "Cancel", .DialogResult = DialogResult.Cancel, .Location = New Point(273, 90), .Size = New Size(75, 26)}
            f.AcceptButton = btnOk : f.CancelButton = btnCancel
            f.Controls.Add(btnOk) : f.Controls.Add(btnCancel)

            If f.ShowDialog() = DialogResult.OK Then
                Return CInt(nud.Value)
            End If
        End Using

        Return Nothing
    End Function

    ''' <summary>
    ''' Ask the user for side (BUY/SELL) and a positive quantity >= 1.
    ''' Returns (ok:=True, side, qty) if confirmed; otherwise (False, "", 0).
    ''' </summary>
    Public Function AskAddQuantity(title As String,
                                   message As String,
                                   defaultSide As String,
                                   defaultQty As Integer) As (ok As Boolean, side As String, qty As Integer)

        If defaultQty < 1 Then defaultQty = 1
        Dim sideNorm = If(String.IsNullOrWhiteSpace(defaultSide), "BUY", defaultSide.Trim().ToUpperInvariant())

        Using f As New Form()
            f.Text = title
            f.FormBorderStyle = FormBorderStyle.FixedDialog
            f.MaximizeBox = False
            f.MinimizeBox = False
            f.StartPosition = FormStartPosition.CenterScreen
            f.ClientSize = New Size(400, 180)
            f.TopMost = False
            f.ShowInTaskbar = False

            Dim lbl As New Label() With {
                .Text = message,
                .AutoSize = False,
                .Location = New Point(12, 12),
                .Size = New Size(376, 40)
            }
            f.Controls.Add(lbl)

            Dim grp As New GroupBox() With {.Text = "Side", .Location = New Point(15, 60), .Size = New Size(200, 60)}
            Dim rbBuy As New RadioButton() With {.Text = "BUY", .Location = New Point(10, 25), .AutoSize = True}
            Dim rbSell As New RadioButton() With {.Text = "SELL", .Location = New Point(100, 25), .AutoSize = True}
            If sideNorm = "SELL" Then rbSell.Checked = True Else rbBuy.Checked = True
            grp.Controls.Add(rbBuy) : grp.Controls.Add(rbSell)
            f.Controls.Add(grp)

            Dim lblQty As New Label() With {.Text = "Quantity:", .Location = New Point(230, 76), .AutoSize = True}
            Dim nud As New NumericUpDown() With {
                .Minimum = 1, .Maximum = 1000000,
                .Value = defaultQty,
                .Location = New Point(300, 72), .Size = New Size(80, 24),
                .ThousandsSeparator = True
            }
            f.Controls.Add(lblQty) : f.Controls.Add(nud)

            Dim btnOk As New Button() With {.Text = "OK", .DialogResult = DialogResult.OK, .Location = New Point(224, 130), .Size = New Size(75, 26)}
            Dim btnCancel As New Button() With {.Text = "Cancel", .DialogResult = DialogResult.Cancel, .Location = New Point(307, 130), .Size = New Size(75, 26)}
            f.AcceptButton = btnOk : f.CancelButton = btnCancel
            f.Controls.Add(btnOk) : f.Controls.Add(btnCancel)

            If f.ShowDialog() = DialogResult.OK Then
                Dim sideSel = If(rbSell.Checked, "SELL", "BUY")
                Return (True, sideSel, CInt(nud.Value))
            End If
        End Using

        Return (False, "", 0)
    End Function

End Module
