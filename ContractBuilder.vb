Option Strict On
Option Explicit On

Imports IBApi
Imports Velocity.Core

Module ContractBuilder
    ''' <summary>
    ''' Get an IB Contract by ConId using SqlSymbolRepository.dictContract.
    ''' Falls back to a minimal SMART/STK/USD contract if missing.
    ''' </summary>
    Public Function FromConId(conId As Integer) As Contract
        Dim c As Contract = Nothing
        If SqlSymbolRepository.dictContract IsNot Nothing AndAlso
           SqlSymbolRepository.dictContract.TryGetValue(conId, c) AndAlso c IsNot Nothing Then
            Return c
        End If
        ' Fallback – should be rare if GetAll() ran first.
        Return New Contract With {
            .ConId = conId,
            .SecType = "STK",
            .Exchange = "SMART",
            .Currency = "USD",
            .IncludeExpired = False
        }
    End Function
End Module
