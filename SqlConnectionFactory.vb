
Imports System.Data
Imports Microsoft.Data.SqlClient

Namespace Velocity.Core

    ''' <summary>
    ''' Simple SQL Server connection factory using Microsoft.Data.SqlClient.
    ''' Usage:
    '''   Dim cf As New SqlConnectionFactory("Data Source=DESKTOP-TRADING;Initial Catalog=VelocityDB;Integrated Security=True;Trust Server Certificate=True;")
    '''   Using conn = cf.CreateOpen()
    '''       ' ...
    '''   End Using
    ''' </summary>
    Public Class SqlConnectionFactory
        Implements IConnectionFactory

        Private ReadOnly _connectionString As String

        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub

        Public Function CreateOpen() As IDbConnection Implements IConnectionFactory.CreateOpen
            Dim c As New SqlConnection(_connectionString)
            c.Open()
            Return c
        End Function

        Public Shared Function BuildLocalTrusted(Optional server As String = "(local)") As String
            ' Helper to quickly build a local connection string for Windows auth
            Return $"Data Source={server};Initial Catalog=VelocityDB;Integrated Security=True;Trust Server Certificate=True;App=Velocity;"
        End Function
    End Class

End Namespace
