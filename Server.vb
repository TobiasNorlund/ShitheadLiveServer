Imports System.Net.Sockets
Imports System.Text
Imports System.Net
Imports System.Threading

Public Class Server

    Public Port As Integer
    Public SwitchingTime As Integer
    Public Clients As New ArrayList
    Public Turn As Integer
    Public Event OnNewClient(ByRef Client As Player)

    Private Listener As TcpListener
    Dim Reciever As New Thread(AddressOf WaitForConnections)
    Dim Wait As Boolean = True

    Public Sub StartWaitForConnections()
        Dim hostEntry As IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
        Dim ipAddress As IPAddress = CType(hostEntry.AddressList.GetValue(2), IPAddress)
        'ipAddress.

        Listener = New TcpListener(ipAddress, Port)
        Reciever.Start()
    End Sub

    Private Sub WaitForConnections()
        Listener.Start()
        While True
            Try
                'Fångar Klienter
                Dim NewClient As New Player
                NewClient.Client = Listener.AcceptTcpClient()
                NewClient.Stream = NewClient.Client.GetStream()

                'Kolla så att klienten spelar rätt spel
                Dim rec = NewClient.Recieve()
                If (rec = "game=Vändtia") Then
                    NewClient.Send("OK")
                    Clients.Add(NewClient)
                    'Reser eventet OnNewClient i rätt thread
                    Application.OpenForms(0).Invoke(New MethodInvoker(AddressOf RaisingEvent))
                ElseIf rec = "<policy-file-request/>" Then
                    NewClient.SendHandshake()
                    rec = NewClient.Recieve()
                    If (rec = "game=Vändtia") Then
                        NewClient.Send("OK")
                    Else
                        NewClient.Send("wronggame")
                        NewClient.Stream.Close()
                    End If
                Else
                    NewClient.Send("wronggame")
                    NewClient.Stream.Close()
                End If
            Catch ex As SocketException
                If Not (ex.SocketErrorCode = SocketError.Interrupted) Then
                    Throw
                End If
            End Try
        End While
    End Sub

    Public Sub EndWaitForConnections()
        Reciever.Abort()
        Listener.Stop()
    End Sub

    Public Sub Send(ByVal Text As String)
        For Each c As Player In Clients
            c.Send(Text)
        Next
    End Sub

    Public Sub CancelAll()
        For Each player As Player In Clients
            If (player.Client.Connected) Then
                player.Client.Client.Shutdown(SocketShutdown.Both)
                player.Stream.Close()
                player.Client.Client.Close()
                player.Client.Close()
            End If
        Next
    End Sub

    Private Sub RaisingEvent()
        RaiseEvent OnNewClient(Clients(Clients.Count - 1))
    End Sub
End Class
