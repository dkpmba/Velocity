' ========================= TwsHost.vb ADD =========================
' Place in TwsHost.vb (e.g., near other request helpers)

Imports IBApi
Imports Velocity.Core

Partial Module TwsHost

    ' Maintain a separate sequence for historical request IDs (NOT order IDs).
    ' IB lets the client choose reqIds for data requests; they must be unique per session.
    Private _nextHistReqId As Integer = 100000   ' start from a high range to avoid collisions

    ''' <summary>
    ''' Returns the next unique request id for historical data requests.
    ''' </summary>
    Private Function GetNextHistReqId() As Integer
        _nextHistReqId += 1
        Return _nextHistReqId
    End Function

    ''' <summary>
    ''' Issue a historical bars request to IB and return the reqId that will flow back
    ''' through TWS historical callbacks. This is designed to match the
    ''' HistoricalBarManager.HistoricalRequester delegate.
    ''' 
    ''' Notes:
    ''' - We build a Contract using only ConId (IB accepts this and ignores other fields).
    ''' - endUtc is formatted with explicit UTC suffix, which IB supports.
    ''' - formatDate:=1 returns human-readable timestamps; 2 returns Unix epoch.
    ''' </summary>
    Public Function RequestHistoricalData(conId As Integer,
                                          endUtc As Date,
                                          durationStr As String,
                                          barSizeStr As String,
                                          whatToShow As String,
                                          useRth As Integer) As Integer
        If Tws Is Nothing OrElse Tws.ClientSocket Is Nothing Then
            Throw New InvalidOperationException("TWS client is not connected.")
        End If

        Dim reqId As Integer = GetNextHistReqId()

        ' Build a minimal contract using ConId (no need to set symbol/sectype/exchange when ConId is present)
        'Dim c As New Contract() With {
        '    .ConId = conId,
        '    .IncludeExpired = False
        '}
        Dim c As IBApi.Contract = Nothing
        SqlSymbolRepository.dictContract.TryGetValue(conId, c)
        ' IB accepts UTC suffix; keep it explicit so everything is timezone-stable
        Dim endStr As String = endUtc.ToUniversalTime().ToString("yyyyMMdd HH:mm:ss 'UTC'")

        ' formatDate:=1 (human readable), keepUpToDate:=False (one-shot backfill)
        ' chartOptions can be Nothing.
        Tws.ClientSocket.reqHistoricalData(
            reqId,
            c,
            endStr,
            durationStr,
            barSizeStr,
            whatToShow,
            useRth,
            1,          ' formatDate: 1 = yyyyMMdd HH:mm:ss; 2 = epoch
            False,      ' keepUpToDate: only backfill, not streaming bars
            Nothing     ' chartOptions: Nothing / empty list
        )

        Return reqId
    End Function

End Module
' ======================= /TwsHost.vb ADD ==========================
