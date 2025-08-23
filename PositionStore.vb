Option Strict On
Option Explicit On

Imports IBApi

Module PositionStore

    Public Class PositionSnap
        Public Property ConId As Integer
        Public Property Symbol As String
        Public Property SecType As String
        Public Property Multiplier As Integer

        Public Property Qty As Integer           ' signed (long > 0, short < 0)
        Public Property AvgCost As Double       ' average open cost (per 1 unit)
        Public Property RealizedPnlToDate As Double
        Public Property LastUpdateUtc As Date
    End Class

    Private ReadOnly _sync As New Object()
    Private ReadOnly _byConId As New Dictionary(Of Integer, PositionSnap)()

    Public Function TryGet(conId As Integer, ByRef snap As PositionSnap) As Boolean
        SyncLock _sync
            Return _byConId.TryGetValue(conId, snap)
        End SyncLock
    End Function

    Public Function GetAll() As List(Of PositionSnap)
        SyncLock _sync
            Return _byConId.Values.Select(Function(s) Clone(s)).ToList()
        End SyncLock
    End Function

    Private Function Clone(s As PositionSnap) As PositionSnap
        Return New PositionSnap With {
            .ConId = s.ConId, .Symbol = s.Symbol, .SecType = s.SecType, .Multiplier = s.Multiplier,
            .Qty = s.Qty, .AvgCost = s.AvgCost, .RealizedPnlToDate = s.RealizedPnlToDate,
            .LastUpdateUtc = s.LastUpdateUtc
        }
    End Function

    ''' <summary>
    ''' Apply an execution to the position. Returns the realized PnL delta (can be 0).
    ''' BUY => qtyDelta > 0 ; SELL => qtyDelta < 0
    ''' </summary>
    Public Function OnExecution(contract As Contract, side As String, shares As Integer, price As Double) As Double
        If contract Is Nothing OrElse shares <= 0 OrElse price <= 0 Then Return 0
        Dim conId As Integer = contract.ConId
        Dim sec As String = If(contract.SecType, "")
        Dim sym As String = If(contract.Symbol, "")
        Dim mult As Integer = ResolveMultiplier(contract)

        Dim realizedDelta As Double = 0
        Dim delta As Integer = shares * If(side.Trim().ToUpperInvariant() = "BUY", +1, -1)

        SyncLock _sync
            Dim s As PositionSnap = Nothing
            If Not _byConId.TryGetValue(conId, s) Then
                s = New PositionSnap With {
                    .ConId = conId, .Symbol = sym, .SecType = sec, .Multiplier = mult,
                    .Qty = 0, .AvgCost = 0, .RealizedPnlToDate = 0, .LastUpdateUtc = Date.UtcNow
                }
                _byConId(conId) = s
            Else
                ' Update static info if missing
                If s.Multiplier <= 0 Then s.Multiplier = mult
                If String.IsNullOrWhiteSpace(s.Symbol) Then s.Symbol = sym
                If String.IsNullOrWhiteSpace(s.SecType) Then s.SecType = sec
            End If

            Dim oldQty As Integer = s.Qty
            Dim oldAvg As Double = s.AvgCost
            Dim newQty As Integer = oldQty + delta

            ' Same direction or opening from flat: weighted average, no realized
            If oldQty = 0 OrElse Math.Sign(oldQty) = Math.Sign(delta) Then
                Dim absOld As Integer = Math.Abs(oldQty)
                Dim absDelta As Integer = Math.Abs(delta)
                Dim totalAbs As Integer = absOld + absDelta
                Dim newAvg As Double = If(totalAbs > 0, ((absOld * oldAvg) + (absDelta * price)) / totalAbs, 0)
                s.AvgCost = newAvg
                s.Qty = newQty
            Else
                ' Closing some or all
                Dim closeQty As Integer = Math.Min(Math.Abs(oldQty), Math.Abs(delta))
                If closeQty > 0 Then
                    Dim sign As Integer = If(oldQty > 0, +1, -1) ' +1 long reduced by SELL, -1 short reduced by BUY
                    realizedDelta += sign * (price - oldAvg) * closeQty * s.Multiplier
                End If

                s.Qty = newQty
                If Math.Sign(oldQty) <> Math.Sign(newQty) AndAlso newQty <> 0 Then
                    ' Flip: the leftover open is at current fill price
                    s.AvgCost = price
                ElseIf newQty = 0 Then
                    s.AvgCost = 0
                End If
            End If

            s.RealizedPnlToDate += realizedDelta
            s.LastUpdateUtc = Date.UtcNow
        End SyncLock

        Return realizedDelta
    End Function

    ''' <summary>URGL at a given mark price.</summary>
    Public Function ComputeUrgl(conId As Integer, mark As Double) As Double
        If mark <= 0 Then Return 0
        SyncLock _sync
            Dim s As PositionSnap = Nothing
            If Not _byConId.TryGetValue(conId, s) Then Return 0
            ' (mark - avg) * qty * mult works for both long and short (qty carries sign)
            Return (mark - s.AvgCost) * s.Qty * s.Multiplier
        End SyncLock
    End Function

    Private Function ResolveMultiplier(c As Contract) As Integer
        If c Is Nothing Then Return 1
        Dim m As Integer = 0
        If Not String.IsNullOrWhiteSpace(c.Multiplier) Then
            Integer.TryParse(c.Multiplier, m)
        End If
        If m <= 0 Then
            Dim st = (If(c.SecType, "")).ToUpperInvariant()
            If st = "OPT" OrElse st = "FOP" Then m = 100 Else m = 1
        End If
        Return m
    End Function

End Module
