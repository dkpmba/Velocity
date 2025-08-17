
Imports System.Data
Imports Microsoft.Data.SqlClient

Namespace Velocity.Core

    Public Class SqlMarketDataRepository
        Implements IMarketDataRepository

        Private ReadOnly _cf As IConnectionFactory
        Public Sub New(cf As IConnectionFactory)
            _cf = cf
        End Sub

        Public Sub UpsertQuote(q As Quote) Implements IMarketDataRepository.UpsertQuote
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
UPDATE dbo.quotes SET [time]=@time, bid=@bid, ask=@ask, last=@last, iv=@iv WHERE cid=@cid;
IF @@ROWCOUNT=0
    INSERT INTO dbo.quotes(cid, [time], bid, ask, last, iv) VALUES(@cid, @time, @bid, @ask, @last, @iv);", conn)
                    cmd.Parameters.AddWithValue("@cid", q.CID)
                    cmd.Parameters.AddWithValue("@time", q.Time)
                    cmd.Parameters.AddWithValue("@bid", q.Bid)
                    cmd.Parameters.AddWithValue("@ask", q.Ask)
                    cmd.Parameters.AddWithValue("@last", q.Last)
                    cmd.Parameters.AddWithValue("@iv", q.IV)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub UpsertGreeks(g As Greeks) Implements IMarketDataRepository.UpsertGreeks
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
UPDATE dbo.greeks SET [time]=@time, delta=@delta, gamma=@gamma, theta=@theta, vega=@vega, iv=@iv WHERE cid=@cid;
IF @@ROWCOUNT=0
    INSERT INTO dbo.greeks(cid, [time], delta, gamma, theta, vega, iv) VALUES(@cid, @time, @delta, @gamma, @theta, @vega, @iv);", conn)
                    cmd.Parameters.AddWithValue("@cid", g.CID)
                    cmd.Parameters.AddWithValue("@time", g.Time)
                    cmd.Parameters.AddWithValue("@delta", g.Delta)
                    cmd.Parameters.AddWithValue("@gamma", g.Gamma)
                    cmd.Parameters.AddWithValue("@theta", g.Theta)
                    cmd.Parameters.AddWithValue("@vega", g.Vega)
                    cmd.Parameters.AddWithValue("@iv", g.IV)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub UpsertBar1m(b As Bar1m) Implements IMarketDataRepository.UpsertBar1m
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
UPDATE dbo.bars_1m SET [open]=@o, [high]=@h, [low]=@l, [close]=@c, vol=@v WHERE cid=@cid AND [time]=@t;
IF @@ROWCOUNT=0
    INSERT INTO dbo.bars_1m(cid, [time], [open], [high], [low], [close], vol) VALUES(@cid, @t, @o, @h, @l, @c, @v);", conn)
                    cmd.Parameters.AddWithValue("@cid", b.CID)
                    cmd.Parameters.AddWithValue("@t", b.Time)
                    cmd.Parameters.AddWithValue("@o", b.Open)
                    cmd.Parameters.AddWithValue("@h", b.High)
                    cmd.Parameters.AddWithValue("@l", b.Low)
                    cmd.Parameters.AddWithValue("@c", b.Close)
                    cmd.Parameters.AddWithValue("@v", b.Volume)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Function GetLatestBar(cid As Long) As Bar1m Implements IMarketDataRepository.GetLatestBar
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("SELECT TOP 1 cid,[time],[open],[high],[low],[close],vol FROM dbo.bars_1m WHERE cid=@cid ORDER BY [time] DESC", conn)
                    cmd.Parameters.AddWithValue("@cid", cid)
                    Using r = cmd.ExecuteReader()
                        If r.Read() Then
                            Return New Bar1m With {
                                .CID = CLng(r("cid")),
                                .Time = CType(r("time"), DateTime),
                                .Open = CDec(r("open")),
                                .High = CDec(r("high")),
                                .Low = CDec(r("low")),
                                .Close = CDec(r("close")),
                                .Volume = CLng(r("vol"))
                            }
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function GetBars1m(cid As Long, fromUtc As DateTime, toUtc As DateTime) As IEnumerable(Of Bar1m) Implements IMarketDataRepository.GetBars1m
            Dim list As New List(Of Bar1m)()
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("SELECT cid,[time],[open],[high],[low],[close],vol FROM dbo.bars_1m WHERE cid=@cid AND [time] BETWEEN @f AND @t ORDER BY [time]", conn)
                    cmd.Parameters.AddWithValue("@cid", cid)
                    cmd.Parameters.AddWithValue("@f", fromUtc)
                    cmd.Parameters.AddWithValue("@t", toUtc)
                    Using r = cmd.ExecuteReader()
                        While r.Read()
                            list.Add(New Bar1m With {
                                .CID = CLng(r("cid")),
                                .Time = CType(r("time"), DateTime),
                                .Open = CDec(r("open")),
                                .High = CDec(r("high")),
                                .Low = CDec(r("low")),
                                .Close = CDec(r("close")),
                                .Volume = CLng(r("vol"))
                            })
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

    End Class

End Namespace
