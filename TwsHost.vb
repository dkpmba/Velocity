Imports System.Threading
Imports System.Threading.Tasks

Module TwsHost
    Public Tws As Tws
    Public Reader As IBApi.EReader
    Public ReaderCts As CancellationTokenSource

    Public Function IsConnected() As Boolean
        Return Tws IsNot Nothing AndAlso Tws.ClientSocket IsNot Nothing AndAlso Tws.ClientSocket.IsConnected()
    End Function

    Public Sub Connect(host As String, port As Integer, clientId As Integer, Optional log As Action(Of String) = Nothing)
        If IsConnected() Then
            If log IsNot Nothing Then log("Already connected.")
            Exit Sub
        End If

        Tws = New Tws()
        Tws.ClientSocket.eConnect(host, port, clientId)

        Reader = New IBApi.EReader(Tws.ClientSocket, Tws.Signal)
        Reader.Start()

        ReaderCts = New CancellationTokenSource()
        Dim token = ReaderCts.Token
        Task.Run(Sub()
                     Try
                         While IsConnected() AndAlso Not token.IsCancellationRequested
                             Tws.Signal.waitForSignal()
                             Reader.processMsgs()
                         End While
                     Catch ex As Exception
                         If log IsNot Nothing Then log("TWS reader: " & ex.Message)
                     End Try
                 End Sub, token)

        Tws.ClientSocket.reqMarketDataType(1) ' live
        If log IsNot Nothing Then log("Socket opened; awaiting nextValidId …")
    End Sub

    Public Sub Disconnect()
        Try
            If ReaderCts IsNot Nothing Then
                ReaderCts.Cancel()
                ReaderCts.Dispose()
                ReaderCts = Nothing
            End If
        Catch
        End Try
        Try
            If Tws IsNot Nothing AndAlso Tws.ClientSocket IsNot Nothing AndAlso Tws.ClientSocket.IsConnected() Then
                Tws.ClientSocket.eDisconnect()
            End If
        Finally
            Tws = Nothing
            Reader = Nothing
        End Try
    End Sub
End Module
