Option Strict On
Option Explicit On
Imports Microsoft.Data.SqlClient

Namespace Velocity.Core
    Public Class SqlOptionIvRepository
        Implements IOptionIvRepository

        Private ReadOnly _cf As IConnectionFactory

        Public Sub New(cf As IConnectionFactory)
            _cf = cf
        End Sub

        Public Sub UpsertEod(conId As Integer, iv As Decimal, asOfUtc As Date) _
            Implements IOptionIvRepository.UpsertEod

            If conId <= 0 OrElse iv <= 0D Then Exit Sub

            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
                    MERGE dbo.option_iv_eod AS t
                    USING (SELECT @conId AS conId, CAST(@asOfUtc AS date) AS asof_date) AS s
                       ON (t.conId = s.conId AND CAST(t.asof_utc AS date) = s.asof_date)
                    WHEN MATCHED THEN
                        UPDATE SET iv = @iv, asof_utc = @asOfUtc
                    WHEN NOT MATCHED THEN
                        INSERT (conId, iv, asof_utc) VALUES (@conId, @iv, @asOfUtc);
                    ", conn)
                    cmd.Parameters.AddWithValue("@conId", conId)
                    cmd.Parameters.AddWithValue("@iv", iv)
                    cmd.Parameters.AddWithValue("@asOfUtc", asOfUtc)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub
    End Class
End Namespace
