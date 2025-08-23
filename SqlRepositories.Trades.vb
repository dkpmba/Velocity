
Imports System.Data
Imports Microsoft.Data.SqlClient

Namespace Velocity.Core

    Public Class SqlTradeRepository
        Implements ITradeRepository

        Private ReadOnly _cf As IConnectionFactory
        Public Sub New(cf As IConnectionFactory)
            _cf = cf
        End Sub

        Public Function CreateTrade(t As Trade) As Long Implements ITradeRepository.CreateTrade
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
                    INSERT INTO dbo.trades(symbol, structure, size, strike_mid, credit, status, start_time, atr3, ema_state, time_value, urgl, rgl, net_delta, net_gamma, net_theta)
                    VALUES (@symbol, @structure, @size, @strike_mid, @credit, @status, SYSUTCDATETIME(), @atr3, @ema_state, @time_value, @urgl, @rgl, @net_delta, @net_gamma, @net_theta);
                    SELECT CAST(SCOPE_IDENTITY() AS BIGINT);", conn)
                    cmd.Parameters.AddWithValue("@symbol", t.Symbol)
                    cmd.Parameters.AddWithValue("@structure", t.[Structure])
                    cmd.Parameters.AddWithValue("@size", t.Size)
                    cmd.Parameters.AddWithValue("@strike_mid", t.StrikeMid)
                    cmd.Parameters.AddWithValue("@credit", t.Credit)
                    cmd.Parameters.AddWithValue("@status", t.Status)
                    cmd.Parameters.AddWithValue("@atr3", t.ATR3)
                    cmd.Parameters.AddWithValue("@ema_state", If(t.EMAState, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@time_value", t.TimeValue)
                    cmd.Parameters.AddWithValue("@urgl", t.URGL)
                    cmd.Parameters.AddWithValue("@rgl", t.RGL)
                    cmd.Parameters.AddWithValue("@net_delta", t.NetDelta)
                    cmd.Parameters.AddWithValue("@net_gamma", t.NetGamma)
                    cmd.Parameters.AddWithValue("@net_theta", t.NetTheta)
                    Dim idObj = cmd.ExecuteScalar()
                    Return CLng(idObj)
                End Using
            End Using
        End Function

        Public Sub AddLeg(tid As Long, leg As TradeLeg) Implements ITradeRepository.AddLeg
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
                    INSERT INTO dbo.trade_legs(tid, leg_id, cid, sectype, symbol, [right], strike, expiry, qty, avg_cost, mark, time_value, delta, gamma, theta, vega, exchange,mult)
                    VALUES(@tid, @leg_id, @cid, @sectype, @symbol, @right, @strike, @expiry, @qty, @avg_cost, @mark, @time_value, @delta, @gamma, @theta, @vega, @exchange,@mult);", conn)
                    cmd.Parameters.AddWithValue("@tid", tid)
                    cmd.Parameters.AddWithValue("@leg_id", leg.LegID)
                    cmd.Parameters.AddWithValue("@cid", leg.CID)
                    cmd.Parameters.AddWithValue("@sectype", leg.SecType)
                    cmd.Parameters.AddWithValue("@symbol", leg.Symbol)
                    cmd.Parameters.AddWithValue("@right", If(leg.Right, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@strike", leg.Strike)
                    cmd.Parameters.AddWithValue("@expiry", If(leg.Expiry = Date.MinValue, CType(DBNull.Value, Object), leg.Expiry))
                    cmd.Parameters.AddWithValue("@qty", leg.Qty)
                    cmd.Parameters.AddWithValue("@avg_cost", leg.AvgCost)
                    cmd.Parameters.AddWithValue("@mark", leg.Mark)
                    cmd.Parameters.AddWithValue("@time_value", leg.TimeValue)
                    cmd.Parameters.AddWithValue("@delta", leg.Delta)
                    cmd.Parameters.AddWithValue("@gamma", leg.Gamma)
                    cmd.Parameters.AddWithValue("@theta", leg.Theta)
                    cmd.Parameters.AddWithValue("@vega", leg.Vega)
                    cmd.Parameters.AddWithValue("@mult", leg.Mult)
                    cmd.Parameters.AddWithValue("@exchange", If(leg.Exchange, CType(DBNull.Value, Object)))
                    cmd.ExecuteNonQuery()

                    Dim cid As Integer = CInt(leg.CID)  ' IB Contract.ConId is Integer
                    Dim sec As String = If(String.IsNullOrWhiteSpace(leg.SecType), "FUT", leg.SecType)
                    Dim sym As String = If(String.IsNullOrWhiteSpace(leg.Symbol), "NA", leg.Symbol)
                    Dim mult As String = If(String.IsNullOrWhiteSpace(leg.Mult), 100, leg.Mult)
                    Dim exg As String = If(String.IsNullOrWhiteSpace(leg.Exchange), "CME", leg.Exchange)

                    Dim ct As New Contract With {
                                .ConId = cid,
                                .SecType = sec,
                                .Exchange = exg,
                                .Symbol = sym,
                                .Multiplier = mult,
                                .Currency = "USD",
                                .IncludeExpired = False
                            }

                    If Not SqlSymbolRepository.dictContract.ContainsKey(cid) Then SqlSymbolRepository.dictContract(cid) = ct
                End Using
            End Using
        End Sub

        Public Sub UpdateTrade(t As Trade) Implements ITradeRepository.UpdateTrade
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
                    UPDATE dbo.trades
                    SET symbol=@symbol, structure=@structure, size=@size, strike_mid=@strike_mid, credit=@credit,
                        status=@status, last_update=SYSUTCDATETIME(), atr3=@atr3, ema_state=@ema_state, time_value=@time_value,
                        urgl=@urgl, rgl=@rgl, net_delta=@net_delta, net_gamma=@net_gamma, net_theta=@net_theta
                    WHERE tid=@tid;", conn)
                    cmd.Parameters.AddWithValue("@tid", t.TID)
                    cmd.Parameters.AddWithValue("@symbol", t.Symbol)
                    cmd.Parameters.AddWithValue("@structure", t.[Structure])
                    cmd.Parameters.AddWithValue("@size", t.Size)
                    cmd.Parameters.AddWithValue("@strike_mid", t.StrikeMid)
                    cmd.Parameters.AddWithValue("@credit", t.Credit)
                    cmd.Parameters.AddWithValue("@status", t.Status)
                    cmd.Parameters.AddWithValue("@atr3", t.ATR3)
                    cmd.Parameters.AddWithValue("@ema_state", If(t.EMAState, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@time_value", t.TimeValue)
                    cmd.Parameters.AddWithValue("@urgl", t.URGL)
                    cmd.Parameters.AddWithValue("@rgl", t.RGL)
                    cmd.Parameters.AddWithValue("@net_delta", t.NetDelta)
                    cmd.Parameters.AddWithValue("@net_gamma", t.NetGamma)
                    cmd.Parameters.AddWithValue("@net_theta", t.NetTheta)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub UpdateLeg(leg As TradeLeg) Implements ITradeRepository.UpdateLeg
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
                    UPDATE dbo.trade_legs
                    SET cid=@cid, sectype=@sectype, symbol=@symbol, [right]=@right, strike=@strike, expiry=@expiry, mult=@mult,
                        qty=@qty, avg_cost=@avg_cost, mark=@mark, time_value=@time_value, delta=@delta, gamma=@gamma, theta=@theta, vega=@vega, exchange=@exchange
                    WHERE tid=@tid AND leg_id=@leg_id;", conn)
                    cmd.Parameters.AddWithValue("@tid", leg.TID)
                    cmd.Parameters.AddWithValue("@leg_id", leg.LegID)
                    cmd.Parameters.AddWithValue("@cid", leg.CID)
                    cmd.Parameters.AddWithValue("@sectype", leg.SecType)
                    cmd.Parameters.AddWithValue("@symbol", leg.Symbol)
                    cmd.Parameters.AddWithValue("@right", If(leg.Right, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@strike", leg.Strike)
                    cmd.Parameters.AddWithValue("@mult", leg.Mult)
                    cmd.Parameters.AddWithValue("@expiry", If(leg.Expiry = Date.MinValue, CType(DBNull.Value, Object), leg.Expiry))
                    cmd.Parameters.AddWithValue("@qty", leg.Qty)
                    cmd.Parameters.AddWithValue("@avg_cost", leg.AvgCost)
                    cmd.Parameters.AddWithValue("@mark", leg.Mark)
                    cmd.Parameters.AddWithValue("@time_value", leg.TimeValue)
                    cmd.Parameters.AddWithValue("@delta", leg.Delta)
                    cmd.Parameters.AddWithValue("@gamma", leg.Gamma)
                    cmd.Parameters.AddWithValue("@theta", leg.Theta)
                    cmd.Parameters.AddWithValue("@vega", leg.Vega)
                    cmd.Parameters.AddWithValue("@exchange", If(leg.Exchange, CType(DBNull.Value, Object)))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Function GetTrade(tid As Long) As Trade Implements ITradeRepository.GetTrade
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("SELECT TOP 1 * FROM dbo.trades WHERE tid=@tid", conn)
                    cmd.Parameters.AddWithValue("@tid", tid)
                    Using r = cmd.ExecuteReader()
                        If r.Read() Then
                            Dim t As New Trade With {
                                .TID = CLng(r("tid")),
                                .Symbol = CStr(r("symbol")),
                                .[Structure] = CStr(r("structure")),
                                .Size = CInt(r("size")),
                                .StrikeMid = If(IsDBNull(r("strike_mid")), 0D, CDec(r("strike_mid"))),
                                .Credit = If(IsDBNull(r("credit")), 0D, CDec(r("credit"))),
                                .Status = CStr(r("status")),
                                .StartTime = CType(r("start_time"), DateTime),
                                .LastUpdate = If(IsDBNull(r("last_update")), CType(Nothing, DateTime?), CType(r("last_update"), DateTime)),
                                .ATR3 = If(IsDBNull(r("atr3")), 0D, CDec(r("atr3"))),
                                .EMAState = If(IsDBNull(r("ema_state")), Nothing, CStr(r("ema_state"))),
                                .TimeValue = If(IsDBNull(r("time_value")), 0D, CDec(r("time_value"))),
                                .URGL = If(IsDBNull(r("urgl")), 0D, CDec(r("urgl"))),
                                .RGL = If(IsDBNull(r("rgl")), 0D, CDec(r("rgl"))),
                                .NetDelta = If(IsDBNull(r("net_delta")), 0D, CDec(r("net_delta"))),
                                .NetGamma = If(IsDBNull(r("net_gamma")), 0D, CDec(r("net_gamma"))),
                                .NetTheta = If(IsDBNull(r("net_theta")), 0D, CDec(r("net_theta")))
                            }
                            Return t
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        Public Function GetTradeLegs(tid As Long) As IEnumerable(Of TradeLeg) Implements ITradeRepository.GetTradeLegs
            Dim list As New List(Of TradeLeg)()
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("SELECT * FROM dbo.trade_legs WHERE tid=@tid ORDER BY leg_id", conn)
                    cmd.Parameters.AddWithValue("@tid", tid)
                    Using r = cmd.ExecuteReader()
                        While r.Read()
                            Dim leg As New TradeLeg With {
                                .TID = CLng(r("tid")),
                                .LegID = CInt(r("leg_id")),
                                .CID = CLng(r("cid")),
                                .SecType = CStr(r("sectype")),
                                .Symbol = CStr(r("symbol")),
                                .Right = If(IsDBNull(r("right")), Nothing, CStr(r("right"))),
                                .Strike = If(IsDBNull(r("strike")), 0D, CDec(r("strike"))),
                                .Expiry = If(IsDBNull(r("expiry")), Date.MinValue, CType(r("expiry"), Date)),
                                .Qty = CInt(r("qty")),
                                .AvgCost = If(IsDBNull(r("avg_cost")), 0D, CDec(r("avg_cost"))),
                                .Mark = If(IsDBNull(r("mark")), 0D, CDec(r("mark"))),
                                .TimeValue = If(IsDBNull(r("time_value")), 0D, CDec(r("time_value"))),
                                .Delta = If(IsDBNull(r("delta")), 0D, CDec(r("delta"))),
                                .Gamma = If(IsDBNull(r("gamma")), 0D, CDec(r("gamma"))),
                                .Theta = If(IsDBNull(r("theta")), 0D, CDec(r("theta"))),
                                .Vega = If(IsDBNull(r("vega")), 0D, CDec(r("vega"))),
                                .Exchange = If(IsDBNull(r("exchange")), Nothing, CStr(r("exchange"))),
                                .Mult = If(IsDBNull(r("mult")), Nothing, CStr(r("mult")))
                            }
                            list.Add(leg)
                            Dim cid As Integer = CInt(leg.CID)  ' IB Contract.ConId is Integer
                            Dim sec As String = If(String.IsNullOrWhiteSpace(leg.SecType), "FUT", leg.SecType)
                            Dim sym As String = If(String.IsNullOrWhiteSpace(leg.Symbol), "NA", leg.Symbol)
                            Dim mult As String = If(String.IsNullOrWhiteSpace(leg.Mult), 100, leg.Mult)
                            Dim exg As String = If(String.IsNullOrWhiteSpace(leg.Exchange), "CME", leg.Exchange)

                            Dim ct As New Contract With {
                                .ConId = cid,
                                .SecType = sec,
                                .Exchange = exg,
                                .Symbol = sym,
                                .Multiplier = mult,
                                .Currency = "USD",
                                .IncludeExpired = False
                            }

                            If Not SqlSymbolRepository.dictContract.ContainsKey(cid) Then SqlSymbolRepository.dictContract(cid) = ct
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Function GetOpenTrades() As IEnumerable(Of Trade) Implements ITradeRepository.GetOpenTrades
            Dim list As New List(Of Trade)()
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("SELECT * FROM dbo.trades WHERE status NOT IN ('Closed','Cancelled') ORDER BY start_time DESC", conn)
                    Using r = cmd.ExecuteReader()
                        While r.Read()
                            Dim t As New Trade With {
                                .TID = CLng(r("tid")),
                                .Symbol = CStr(r("symbol")),
                                .[Structure] = CStr(r("structure")),
                                .Size = CInt(r("size")),
                                .StrikeMid = If(IsDBNull(r("strike_mid")), 0D, CDec(r("strike_mid"))),
                                .Credit = If(IsDBNull(r("credit")), 0D, CDec(r("credit"))),
                                .Status = CStr(r("status")),
                                .StartTime = CType(r("start_time"), DateTime),
                                .LastUpdate = If(IsDBNull(r("last_update")), CType(Nothing, DateTime?), CType(r("last_update"), DateTime)),
                                .ATR3 = If(IsDBNull(r("atr3")), 0D, CDec(r("atr3"))),
                                .EMAState = If(IsDBNull(r("ema_state")), Nothing, CStr(r("ema_state"))),
                                .TimeValue = If(IsDBNull(r("time_value")), 0D, CDec(r("time_value"))),
                                .URGL = If(IsDBNull(r("urgl")), 0D, CDec(r("urgl"))),
                                .RGL = If(IsDBNull(r("rgl")), 0D, CDec(r("rgl"))),
                                .NetDelta = If(IsDBNull(r("net_delta")), 0D, CDec(r("net_delta"))),
                                .NetGamma = If(IsDBNull(r("net_gamma")), 0D, CDec(r("net_gamma"))),
                                .NetTheta = If(IsDBNull(r("net_theta")), 0D, CDec(r("net_theta")))
                            }
                            list.Add(t)
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        ' Add/accumulate delta into dbo.trades.rgl
        Public Sub UpdateRglDelta(tid As Integer, delta As Decimal) _
        Implements ITradeRepository.UpdateRglDelta   ' <-- remove Implements if no interface

            If tid <= 0 OrElse delta = 0D Then Exit Sub

            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
                    UPDATE dbo.trades
                    SET rgl = ISNULL(rgl, 0) + @delta,
                        updated_utc = SYSUTCDATETIME()
                    WHERE tid = @tid;", conn)

                    cmd.Parameters.AddWithValue("@tid", tid)
                    cmd.Parameters.AddWithValue("@delta", delta)

                    Dim n = cmd.ExecuteNonQuery()
                    ' Optional: if n = 0 then row not found — decide if you want to INSERT or just ignore.
                End Using
            End Using
        End Sub

        ' Set absolute RGL value in dbo.trades.rgl
        Public Sub UpdateRgl(tid As Integer, newRgl As Decimal) _
        Implements ITradeRepository.UpdateRgl   ' <-- remove Implements if no interface

            If tid <= 0 Then Exit Sub

            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
                    UPDATE dbo.trades
                    SET rgl = @rgl,
                        updated_utc = SYSUTCDATETIME()
                    WHERE tid = @tid;", conn)

                    cmd.Parameters.AddWithValue("@tid", tid)
                    cmd.Parameters.AddWithValue("@rgl", newRgl)

                    Dim n = cmd.ExecuteNonQuery()
                    ' Optional: handle n = 0 similarly
                End Using
            End Using
        End Sub
    End Class

End Namespace
