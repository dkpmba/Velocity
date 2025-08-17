
Imports System.Data
Imports Microsoft.Data.SqlClient

Namespace Velocity.Core

    Public Class SqlOrderRepository
        Implements IOrderRepository

        Private ReadOnly _cf As IConnectionFactory
        Public Sub New(cf As IConnectionFactory)
            _cf = cf
        End Sub

        Public Sub SaveOrder(o As OrderRow) Implements IOrderRepository.SaveOrder
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
IF EXISTS (SELECT 1 FROM dbo.orders WHERE oid=@oid)
BEGIN
    UPDATE dbo.orders SET tid=@tid, account=@account, symbol=@symbol, side=@side, sectype=@sectype, qty=@qty,
        order_type=@otype, price=@price, status=@status, fills=@fills, remaining=@remaining, exchange=@exch, [error]=@err, [time]=@time
    WHERE oid=@oid;
END
ELSE
BEGIN
    INSERT INTO dbo.orders(oid, tid, account, symbol, side, sectype, qty, order_type, price, status, fills, remaining, exchange, [error], [time])
    VALUES(@oid, @tid, @account, @symbol, @side, @sectype, @qty, @otype, @price, @status, @fills, @remaining, @exch, @err, @time);
END", conn)
                    cmd.Parameters.AddWithValue("@oid", o.OID)
                    cmd.Parameters.AddWithValue("@tid", If(o.TID.HasValue, CType(o.TID.Value, Object), DBNull.Value))
                    cmd.Parameters.AddWithValue("@account", If(o.Account, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@symbol", If(o.Symbol, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@side", If(o.Side, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@sectype", If(o.SecType, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@qty", o.Qty)
                    cmd.Parameters.AddWithValue("@otype", If(o.OrderType, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@price", o.Price)
                    cmd.Parameters.AddWithValue("@status", If(o.Status, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@fills", o.Fills)
                    cmd.Parameters.AddWithValue("@remaining", o.Remaining)
                    cmd.Parameters.AddWithValue("@exch", If(o.Exchange, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@err", If(o.Error, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@time", o.Time)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub UpdateOrder(o As OrderRow) Implements IOrderRepository.UpdateOrder
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
UPDATE dbo.orders SET tid=@tid, account=@account, symbol=@symbol, side=@side, sectype=@sectype, qty=@qty,
    order_type=@otype, price=@price, status=@status, fills=@fills, remaining=@remaining, exchange=@exch, [error]=@err, [time]=@time
WHERE oid=@oid;", conn)
                    cmd.Parameters.AddWithValue("@oid", o.OID)
                    cmd.Parameters.AddWithValue("@tid", If(o.TID.HasValue, CType(o.TID.Value, Object), DBNull.Value))
                    cmd.Parameters.AddWithValue("@account", If(o.Account, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@symbol", If(o.Symbol, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@side", If(o.Side, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@sectype", If(o.SecType, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@qty", o.Qty)
                    cmd.Parameters.AddWithValue("@otype", If(o.OrderType, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@price", o.Price)
                    cmd.Parameters.AddWithValue("@status", If(o.Status, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@fills", o.Fills)
                    cmd.Parameters.AddWithValue("@remaining", o.Remaining)
                    cmd.Parameters.AddWithValue("@exch", If(o.Exchange, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@err", If(o.Error, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@time", o.Time)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub SaveFill(f As FillRow) Implements IOrderRepository.SaveFill
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
IF EXISTS (SELECT 1 FROM dbo.fills WHERE exec_id=@id)
BEGIN
    UPDATE dbo.fills SET oid=@oid, tid=@tid, [time]=@time, qty=@qty, price=@price, commission=@comm, fees=@fees, exchange=@exch, liquidity=@liq WHERE exec_id=@id;
END
ELSE
BEGIN
    INSERT INTO dbo.fills(exec_id, oid, tid, [time], qty, price, commission, fees, exchange, liquidity)
    VALUES(@id, @oid, @tid, @time, @qty, @price, @comm, @fees, @exch, @liq);
END", conn)
                    cmd.Parameters.AddWithValue("@id", f.ExecId)
                    cmd.Parameters.AddWithValue("@oid", f.OID)
                    cmd.Parameters.AddWithValue("@tid", If(f.TID.HasValue, CType(f.TID.Value, Object), DBNull.Value))
                    cmd.Parameters.AddWithValue("@time", f.Time)
                    cmd.Parameters.AddWithValue("@qty", f.Qty)
                    cmd.Parameters.AddWithValue("@price", f.Price)
                    cmd.Parameters.AddWithValue("@comm", f.Commission)
                    cmd.Parameters.AddWithValue("@fees", f.Fees)
                    cmd.Parameters.AddWithValue("@exch", If(f.Exchange, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@liq", If(f.Liquidity, CType(DBNull.Value, Object)))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Function GetOrdersByTID(tid As Long) As IEnumerable(Of OrderRow) Implements IOrderRepository.GetOrdersByTID
            Dim list As New List(Of OrderRow)()
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("SELECT * FROM dbo.orders WHERE tid=@tid ORDER BY [time] DESC", conn)
                    cmd.Parameters.AddWithValue("@tid", tid)
                    Using r = cmd.ExecuteReader()
                        While r.Read()
                            list.Add(New OrderRow With {
                                .OID = CLng(r("oid")),
                                .TID = If(IsDBNull(r("tid")), CType(Nothing, Long?), CLng(r("tid"))),
                                .Account = If(IsDBNull(r("account")), Nothing, CStr(r("account"))),
                                .Symbol = If(IsDBNull(r("symbol")), Nothing, CStr(r("symbol"))),
                                .Side = If(IsDBNull(r("side")), Nothing, CStr(r("side"))),
                                .SecType = If(IsDBNull(r("sectype")), Nothing, CStr(r("sectype"))),
                                .Qty = If(IsDBNull(r("qty")), 0, CInt(r("qty"))),
                                .OrderType = If(IsDBNull(r("order_type")), Nothing, CStr(r("order_type"))),
                                .Price = If(IsDBNull(r("price")), 0D, CDec(r("price"))),
                                .Status = If(IsDBNull(r("status")), Nothing, CStr(r("status"))),
                                .Fills = If(IsDBNull(r("fills")), 0, CInt(r("fills"))),
                                .Remaining = If(IsDBNull(r("remaining")), 0, CInt(r("remaining"))),
                                .Exchange = If(IsDBNull(r("exchange")), Nothing, CStr(r("exchange"))),
                                .Error = If(IsDBNull(r("error")), Nothing, CStr(r("error"))),
                                .Time = CType(r("time"), DateTime)
                            })
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Function GetFillsByOID(oid As Long) As IEnumerable(Of FillRow) Implements IOrderRepository.GetFillsByOID
            Dim list As New List(Of FillRow)()
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("SELECT * FROM dbo.fills WHERE oid=@oid ORDER BY [time] DESC", conn)
                    cmd.Parameters.AddWithValue("@oid", oid)
                    Using r = cmd.ExecuteReader()
                        While r.Read()
                            list.Add(New FillRow With {
                                .ExecId = CStr(r("exec_id")),
                                .OID = CLng(r("oid")),
                                .TID = If(IsDBNull(r("tid")), CType(Nothing, Long?), CLng(r("tid"))),
                                .Time = CType(r("time"), DateTime),
                                .Qty = CInt(r("qty")),
                                .Price = CDec(r("price")),
                                .Commission = If(IsDBNull(r("commission")), 0D, CDec(r("commission"))),
                                .Fees = If(IsDBNull(r("fees")), 0D, CDec(r("fees"))),
                                .Exchange = If(IsDBNull(r("exchange")), Nothing, CStr(r("exchange"))),
                                .Liquidity = If(IsDBNull(r("liquidity")), Nothing, CStr(r("liquidity")))
                            })
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

    End Class

End Namespace
