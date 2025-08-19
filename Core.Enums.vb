Namespace Velocity.Core

    Public Enum SecType
        STK
        OPT
        FUT
        FOP
        CFD
        CASH
        IND
        BOND
        WAR
    End Enum

    Public Enum OrderSide
        Buy
        Sell
        [Short]
        Cover
    End Enum

    Public Enum OrderType
        MKT
        LMT
        STP
        STP_LMT
        MOC
        LOC
    End Enum

    Public Enum TradeStatus
        PendingEntry
        Live
        HedgeLong
        HedgeShort
        FlatHedge
        Rolled
        Closed
        Cancelled
        [Error]
    End Enum

    Public Enum AlertSeverity
        Info = 0
        Warning = 1
        [Error] = 2
        Critical = 3
    End Enum

    Public Enum TimeFrames
        M1
        M5
    End Enum
End Namespace
