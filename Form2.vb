Imports System.Net.Sockets
Imports System.Text
Imports System.Net

Public Class Form2
    Public WithEvents Server As New Server()
    Public Port As Integer
    Public SwitchingTime As Integer

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Server.Port = Port
        Server.SwitchingTime = SwitchingTime
        Server.StartWaitForConnections()
    End Sub

    Private Sub OnNewClient(ByRef Client As Player) Handles Server.OnNewClient
        'En ny Klient har anslutit sig
        Client.Name = Client.Recieve()
        ClientsCount.Text = Server.Clients.Count

        Console.WriteLine(Client.Name & " har just anslutit")

        'Meddelar alla andra om den nya och den nya om alla andra
        For Each c As Player In Server.Clients
            If Not (c.Equals(Client)) Then
                c.Send("newplayer=" & Client.Name)
            End If
            Client.Send("newplayer=" & c.Name)
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Server.EndWaitForConnections()
        Server.CancelAll()
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (Server.Clients.Count > 0) Then
            Server.EndWaitForConnections()
            Dim form3 As New Form3
            form3.Server = Server
            form3.Location = New System.Drawing.Point(Me.Location.X, Me.Location.Y)
            form3.Show()
            Me.Close()
        Else
            MsgBox("Du kan inte starta utan att någon spelare har anslutit")
        End If
    End Sub
End Class