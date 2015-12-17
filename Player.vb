Imports System.Net.Sockets
Imports System.Text
Imports System.Net

Public Class Player
    Public Name As String
    Public Client As TcpClient
    Public Stream As NetworkStream
    Public Finished As Boolean = False

    Public Sub Send(ByVal Text As String)
        Try
            Console.WriteLine("Skickar till " & Name & ": " & Text)
            Text += Chr(0) 'Måste lägga till det av någon anledning
            Dim myWriteBuffer As Byte() = Encoding.UTF8.GetBytes(Text)
            Stream.Write(myWriteBuffer, 0, myWriteBuffer.Length)
        Catch ex As Exception
            MsgBox("Gick inte att skicka datat till " & Name & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Function Recieve() As String
        Try
            Dim bytes(Client.ReceiveBufferSize) As Byte
            Stream.Read(bytes, 0, CInt(Client.ReceiveBufferSize))
            Console.WriteLine(Name & " säger: " & Encoding.UTF8.GetString(bytes).Trim(Chr(0)))
            Return Encoding.UTF8.GetString(bytes).Trim(Chr(0))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub SendHandshake()
        Dim sb As String = "<?xml version=""1.0""?><!DOCTYPE cross-domain-policy SYSTEM ""/xml/dtds/cross-domain-policy.dtd""><cross-domain-policy><site-control permitted-cross-domain-policies=""all""/><allow-access-from domain=""*"" to-ports=""*"" /></cross-domain-policy>"
        Send(sb)
    End Sub
End Class

