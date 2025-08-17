Imports System.ComponentModel
Imports System.Diagnostics

Namespace Velocity.Core

    <DebuggerDisplay("{Symbol} ({CID})")>
    Public Class SymbolRow
        Public Property CID As Long
        Public Property Symbol As String
        Public Property SecType As String
        Public Property Exchange As String
        Public Property Mult As Integer
        Public Property Increment As Decimal
        Public Property IV As Decimal?
        Public Property LiquidityScore As Decimal?
        Public Property Enabled As Boolean

        ' Convenience for binding (Symbols grid)
        Public ReadOnly Property BidAskLast As String
            Get
                Return $"{Bid:N2}/{Ask:N2}/{Last:N2}"
            End Get
        End Property

        ' Backing quote snapshot (optional for binding)
        Public Property Bid As Decimal
        Public Property Ask As Decimal
        Public Property Last As Decimal
    End Class

    <DebuggerDisplay("{CID} Δ={Delta}, Γ={Gamma}, Θ={Theta}, V={Vega}")>
    Public Class Greeks
        Public Property CID As Long
        Public Property Time As DateTime
        Public Property Delta As Decimal
        Public Property Gamma As Decimal
        Public Property Theta As Decimal
        Public Property Vega As Decimal
        Public Property IV As Decimal
    End Class

    Public Class Quote
        Public Property CID As Long
        Public Property Time As DateTime
        Public Property Bid As Decimal
        Public Property Ask As Decimal
        Public Property Last As Decimal
        Public ReadOnly Property Mid As Decimal
            Get
                If Ask = 0D AndAlso Bid = 0D Then Return 0D
                If Ask = 0D Then Return Bid
                If Bid = 0D Then Return Ask
                Return (Bid + Ask) / 2D
            End Get
        End Property
        Public Property IV As Decimal
    End Class

    Public Class Bar1m
        Public Property CID As Long
        Public Property Time As DateTime
        Public Property Open As Decimal
        Public Property High As Decimal
        Public Property Low As Decimal
        Public Property Close As Decimal
        Public Property Volume As Long
    End Class

    <DebuggerDisplay("T{TID} {Symbol} {Structure} Size={Size} Status={Status}")>
    Public Class Trade
        Public Property TID As Long
        Public Property Symbol As String
        Public Property [Structure] As String ' Straddle/Strangle/etc.
        Public Property Size As Integer
        Public Property StrikeMid As Decimal
        Public Property Credit As Decimal
        Public Property Status As String
        Public Property StartTime As DateTime
        Public Property LastUpdate As DateTime?

        ' Risk/indicators
        Public Property ATR3 As Decimal
        Public Property EMAState As String ' e.g., "9>21", "9<21"
        Public Property TimeValue As Decimal

        ' PnL + Greeks rollup
        Public Property URGL As Decimal
        Public Property RGL As Decimal
        Public Property NetDelta As Decimal
        Public Property NetGamma As Decimal
        Public Property NetTheta As Decimal
    End Class

    <DebuggerDisplay("T{TID}/L{LegID} {Right} {Strike} {Expiry:yyyy-MM-dd} Qty={Qty}")>
    Public Class TradeLeg
        Public Property TID As Long
        Public Property LegID As Integer
        Public Property CID As Long
        Public Property SecType As String
        Public Property Symbol As String
        Public Property Right As String ' C/P or LONG/SHORT for futures
        Public Property Strike As Decimal
        Public Property Expiry As Date
        Public Property Qty As Integer
        Public Property AvgCost As Decimal
        Public Property Mark As Decimal
        Public Property TimeValue As Decimal
        Public Property Delta As Decimal
        Public Property Gamma As Decimal
        Public Property Theta As Decimal
        Public Property Vega As Decimal
        Public Property Exchange As String
    End Class

    Public Class OrderRow
        Public Property Account As String
        Public Property Time As DateTime
        Public Property OID As Long
        Public Property TID As Long?
        Public Property Symbol As String
        Public Property Side As String
        Public Property SecType As String
        Public Property Qty As Integer
        Public Property OrderType As String
        Public Property Price As Decimal
        Public Property Status As String
        Public Property Fills As Integer
        Public Property Remaining As Integer
        Public Property Exchange As String
        Public Property [Error] As String
    End Class

    Public Class FillRow
        Public Property ExecId As String
        Public Property OID As Long
        Public Property TID As Long?
        Public Property Time As DateTime
        Public Property Qty As Integer
        Public Property Price As Decimal
        Public Property Commission As Decimal
        Public Property Fees As Decimal
        Public Property Exchange As String
        Public Property Liquidity As String
    End Class

    Public Class AlertRow
        Public Property Time As DateTime
        Public Property Severity As String
        Public Property Type As String
        Public Property Code As String
        Public Property Message As String
        Public Property Symbol As String
        Public Property TID As Long?
        Public Property Ack As Boolean
        Public Property SnoozeUntil As DateTime?
    End Class

    Public Class ActivityRow
        Public Property Time As DateTime
        Public Property Source As String
        Public Property Level As String
        Public Property Message As String
        Public Property Context As String
    End Class

    ' Editable rows for DesignForm (dgvDesign)
    Public Class DesignLegRow
        Public Property UseLeg As Boolean
        Public Property LegID As Integer
        Public Property CID As Long
        Public Property SecType As String
        Public Property Symbol As String
        Public Property Right As String
        Public Property Strike As Decimal
        Public Property Expiry As Date
        Public Property Qty As Integer
        Public Property Ratio As Decimal
        Public Property OrderType As String
        Public Property LimitPrice As Decimal
        Public Property BidAskMid As String
        Public Property Mark As Decimal
        Public Property Delta As Decimal
        Public Property Gamma As Decimal
        Public Property Theta As Decimal
        Public Property Vega As Decimal
        Public Property IV As Decimal
        Public Property Multiplier As Integer
        Public Property Exchange As String
        Public Property TIF As String
        Public Property Status As String
        Public Property Note As String
    End Class

End Namespace
