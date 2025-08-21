
Imports System.Data
Imports Microsoft.Data.SqlClient

Namespace Velocity.Core

    Public Class SqlSymbolRepository
        Implements ISymbolRepository
        Public Shared dictContract As New Dictionary(Of Integer, Contract)()
        Private ReadOnly _cf As IConnectionFactory
        Public Sub New(cf As IConnectionFactory)
            _cf = cf
        End Sub

        Public Function GetAll() As IEnumerable(Of SymbolRow) Implements ISymbolRepository.GetAll
            Dim list As New List(Of SymbolRow)()

            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("SELECT cid, symbol, sectype, exchange, mult, increment, iv, liquidity_score, enabled FROM dbo.symbols ORDER BY symbol", conn)
                    Using r = cmd.ExecuteReader()
                        While r.Read()
                            Dim row As New SymbolRow With {
                                .CID = CLng(r("cid")),
                                .Symbol = CStr(r("symbol")),
                                .SecType = CStr(r("sectype")),
                                .Exchange = If(IsDBNull(r("exchange")), Nothing, CStr(r("exchange"))),
                                .Mult = If(IsDBNull(r("mult")), 0, CInt(r("mult"))),
                                .Increment = If(IsDBNull(r("increment")), 0D, CDec(r("increment"))),
                                .IV = If(IsDBNull(r("iv")), CType(Nothing, Decimal?), CDec(r("iv"))),
                                .LiquidityScore = If(IsDBNull(r("liquidity_score")), CType(Nothing, Decimal?), CDec(r("liquidity_score"))),
                                .Enabled = CBool(r("enabled"))
                            }
                            list.Add(row)
                            ' ----- Minimal IB contract for this ConId (SMART/STK/USD defaults) -----
                            Dim cid As Integer = CInt(row.CID)  ' IB Contract.ConId is Integer
                            Dim sec As String = If(String.IsNullOrWhiteSpace(row.SecType), "FUT", row.SecType)
                            Dim sym As String = If(String.IsNullOrWhiteSpace(row.Symbol), "NA", row.Symbol)
                            Dim mult As String = If(String.IsNullOrWhiteSpace(row.Mult), 100, row.Mult)
                            Dim exg As String = If(String.IsNullOrWhiteSpace(row.Exchange), "CME", row.Exchange)

                            Dim ct As New Contract With {
                                .ConId = cid,
                                .SecType = sec,
                                .Exchange = exg,
                                .Symbol = sym,
                                .Multiplier = mult,
                                .Currency = "USD",
                                .IncludeExpired = False
                            }

                            ' Upsert into cache (overwrite if reload changes exchange, etc.)
                            dictContract(cid) = ct
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

        Public Sub Upsert(symbol As SymbolRow) Implements ISymbolRepository.Upsert
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand(" 
                                            IF EXISTS (SELECT 1 FROM dbo.symbols WHERE cid=@cid)
                                            BEGIN
                                                UPDATE dbo.symbols
                                                SET symbol=@symbol, sectype=@sectype, exchange=@exchange, mult=@mult, increment=@increment,
                                                    iv=@iv, liquidity_score=@liq, enabled=@enabled, updated_utc=SYSUTCDATETIME()
                                                WHERE cid=@cid;
                                            END
                                            ELSE
                                            BEGIN
                                                INSERT INTO dbo.symbols (cid, symbol, sectype, exchange, mult, increment, iv, liquidity_score, enabled)
                                                VALUES (@cid, @symbol, @sectype, @exchange, @mult, @increment, @iv, @liq, @enabled);
                                            END
                                            ", conn)
                    cmd.Parameters.AddWithValue("@cid", symbol.CID)
                    cmd.Parameters.AddWithValue("@symbol", symbol.Symbol)
                    cmd.Parameters.AddWithValue("@sectype", symbol.SecType)
                    cmd.Parameters.AddWithValue("@exchange", If(symbol.Exchange, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@mult", symbol.Mult)
                    cmd.Parameters.AddWithValue("@increment", symbol.Increment)
                    cmd.Parameters.AddWithValue("@iv", If(symbol.IV.HasValue, CType(symbol.IV.Value, Object), DBNull.Value))
                    cmd.Parameters.AddWithValue("@liq", If(symbol.LiquidityScore.HasValue, CType(symbol.LiquidityScore.Value, Object), DBNull.Value))
                    cmd.Parameters.AddWithValue("@enabled", symbol.Enabled)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub SetEnabled(cid As Long, enabled As Boolean) Implements ISymbolRepository.SetEnabled
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("UPDATE dbo.symbols SET enabled=@en, updated_utc=SYSUTCDATETIME() WHERE cid=@cid", conn)
                    cmd.Parameters.AddWithValue("@en", enabled)
                    cmd.Parameters.AddWithValue("@cid", cid)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub Delete(cid As Long) Implements ISymbolRepository.Delete
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("DELETE FROM dbo.symbols WHERE cid=@cid", conn)
                    cmd.Parameters.AddWithValue("@cid", cid)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

    End Class

End Namespace
