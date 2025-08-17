
Imports System.Data
Imports Microsoft.Data.SqlClient

Namespace Velocity.Core

    Public Class SqlAlertRepository
        Implements IAlertRepository

        Private ReadOnly _cf As IConnectionFactory
        Public Sub New(cf As IConnectionFactory)
            _cf = cf
        End Sub

        Public Sub Insert(a As AlertRow) Implements IAlertRepository.Insert
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
INSERT INTO dbo.alerts([time], severity, [type], code, message, symbol, tid, ack, snooze_until)
VALUES(@time, @sev, @type, @code, @msg, @sym, @tid, @ack, @snooze)", conn)
                    cmd.Parameters.AddWithValue("@time", a.Time)
                    cmd.Parameters.AddWithValue("@sev", a.Severity)
                    cmd.Parameters.AddWithValue("@type", a.Type)
                    cmd.Parameters.AddWithValue("@code", If(a.Code, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@msg", a.Message)
                    cmd.Parameters.AddWithValue("@sym", If(a.Symbol, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@tid", If(a.TID.HasValue, CType(a.TID.Value, Object), DBNull.Value))
                    cmd.Parameters.AddWithValue("@ack", a.Ack)
                    cmd.Parameters.AddWithValue("@snooze", If(a.SnoozeUntil.HasValue, CType(a.SnoozeUntil.Value, Object), DBNull.Value))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Sub Acknowledge(ids As IEnumerable(Of Long)) Implements IAlertRepository.Acknowledge
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                For Each id In ids
                    Using cmd As New SqlCommand("UPDATE dbo.alerts SET ack=1 WHERE id=@id", conn)
                        cmd.Parameters.AddWithValue("@id", id)
                        cmd.ExecuteNonQuery()
                    End Using
                Next
            End Using
        End Sub

        Public Sub Dismiss(ids As IEnumerable(Of Long)) Implements IAlertRepository.Dismiss
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                For Each id In ids
                    Using cmd As New SqlCommand("DELETE FROM dbo.alerts WHERE id=@id", conn)
                        cmd.Parameters.AddWithValue("@id", id)
                        cmd.ExecuteNonQuery()
                    End Using
                Next
            End Using
        End Sub

        Public Function Query(fromUtc As DateTime, toUtc As DateTime, Optional severity As String = Nothing) As IEnumerable(Of AlertRow) Implements IAlertRepository.Query
            Dim list As New List(Of AlertRow)()
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Dim sql = "SELECT id,[time],severity,[type],code,message,symbol,tid,ack,snooze_until FROM dbo.alerts WHERE [time] BETWEEN @f AND @t"
                If Not String.IsNullOrEmpty(severity) Then sql &= " AND severity=@sev"
                sql &= " ORDER BY [time] DESC"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@f", fromUtc)
                    cmd.Parameters.AddWithValue("@t", toUtc)
                    If Not String.IsNullOrEmpty(severity) Then cmd.Parameters.AddWithValue("@sev", severity)
                    Using r = cmd.ExecuteReader()
                        While r.Read()
                            list.Add(New AlertRow With {
                                .Time = CType(r("time"), DateTime),
                                .Severity = CStr(r("severity")),
                                .Type = CStr(r("type")),
                                .Code = If(IsDBNull(r("code")), Nothing, CStr(r("code"))),
                                .Message = CStr(r("message")),
                                .Symbol = If(IsDBNull(r("symbol")), Nothing, CStr(r("symbol"))),
                                .TID = If(IsDBNull(r("tid")), CType(Nothing, Long?), CLng(r("tid"))),
                                .Ack = CBool(r("ack")),
                                .SnoozeUntil = If(IsDBNull(r("snooze_until")), CType(Nothing, DateTime?), CType(r("snooze_until"), DateTime))
                            })
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

    End Class

    Public Class SqlActivityRepository
        Implements IActivityRepository

        Private ReadOnly _cf As IConnectionFactory
        Public Sub New(cf As IConnectionFactory)
            _cf = cf
        End Sub

        Public Sub Insert(evt As ActivityRow) Implements IActivityRepository.Insert
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("INSERT INTO dbo.activity([time],source,level,message,context) VALUES(@t,@src,@lvl,@msg,@ctx)", conn)
                    cmd.Parameters.AddWithValue("@t", evt.Time)
                    cmd.Parameters.AddWithValue("@src", evt.Source)
                    cmd.Parameters.AddWithValue("@lvl", evt.Level)
                    cmd.Parameters.AddWithValue("@msg", evt.Message)
                    cmd.Parameters.AddWithValue("@ctx", If(evt.Context, CType(DBNull.Value, Object)))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Function Query(fromUtc As DateTime, toUtc As DateTime, Optional level As String = Nothing) As IEnumerable(Of ActivityRow) Implements IActivityRepository.Query
            Dim list As New List(Of ActivityRow)()
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Dim sql = "SELECT [time],source,level,message,context FROM dbo.activity WHERE [time] BETWEEN @f AND @t"
                If Not String.IsNullOrEmpty(level) Then sql &= " AND level=@lvl"
                sql &= " ORDER BY [time] DESC"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@f", fromUtc)
                    cmd.Parameters.AddWithValue("@t", toUtc)
                    If Not String.IsNullOrEmpty(level) Then cmd.Parameters.AddWithValue("@lvl", level)
                    Using r = cmd.ExecuteReader()
                        While r.Read()
                            list.Add(New ActivityRow With {
                                .Time = CType(r("time"), DateTime),
                                .Source = CStr(r("source")),
                                .Level = CStr(r("level")),
                                .Message = CStr(r("message")),
                                .Context = If(IsDBNull(r("context")), Nothing, CStr(r("context")))
                            })
                        End While
                    End Using
                End Using
            End Using
            Return list
        End Function

    End Class

    Public Class SqlSettingsRepository
        Implements ISettingsRepository

        Private ReadOnly _cf As IConnectionFactory
        Public Sub New(cf As IConnectionFactory)
            _cf = cf
        End Sub

        Public Function GetValue(key As String) As String Implements ISettingsRepository.GetValue
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("SELECT json FROM dbo.settings WHERE [key]=@k", conn)
                    cmd.Parameters.AddWithValue("@k", key)
                    Dim o = cmd.ExecuteScalar()
                    If o Is Nothing OrElse o Is DBNull.Value Then Return Nothing
                    Return CStr(o)
                End Using
            End Using
        End Function

        Public Sub SetValue(key As String, json As String) Implements ISettingsRepository.SetValue
            Using conn = CType(_cf.CreateOpen(), SqlConnection)
                Using cmd As New SqlCommand("
UPDATE dbo.settings SET json=@j, updated_utc=SYSUTCDATETIME() WHERE [key]=@k;
IF @@ROWCOUNT=0
    INSERT INTO dbo.settings([key], json) VALUES(@k, @j);", conn)
                    cmd.Parameters.AddWithValue("@k", key)
                    cmd.Parameters.AddWithValue("@j", json)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

    End Class

End Namespace
