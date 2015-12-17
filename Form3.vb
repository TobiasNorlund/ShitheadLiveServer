Imports System.Threading

Public Class Form3

    Public WithEvents Server As Server

    Private WithEvents PackOfCards As New PackOfCards
    Dim WorkThread As New System.Threading.Thread(AddressOf Play)
    Private GameState As String

    Private PlayerThreads As New ArrayList
    Dim ThreadCount As Integer = 0

    Private Sub Form3_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Avslutar spelet
        WorkThread.Abort()
        Server.CancelAll()
    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Fyller listan med spelare
        FillList()
        WorkThread.Start()
    End Sub

    Private Sub FillList()
        PlayersList.Items.Clear()
        For Each i As Player In Server.Clients
            PlayersList.Items.Add(i.Name)
        Next
    End Sub

    Private Sub Play()
        PackOfCards.Bland()

        'Meddelar att spelet startar
        Server.Send("start")

        'Skickar 9 kort till alla spelarna, 3 dolda, 3 kända och 3 till handen
        For i As Integer = 0 To Server.Clients.Count - 1
            '3 dolda
            For j As Integer = 1 To 3
                Server.Clients(i).Send("yournewcard=" & PackOfCards.GetNext().Merged & "=hidden")
            Next
            '3 uppåtvända
            For j As Integer = 1 To 3
                Server.Send("tablecard=" & PackOfCards.GetNext().Merged & "=" & Server.Clients(i).Name)
            Next
            '3 till handen
            For j As Integer = 1 To 3
                Server.Clients(i).Send("yournewcard=" & PackOfCards.GetNext().Merged)
            Next

            'Nu görs alla byten. Skapar en thread för varje spelare.
            Dim thread As New Thread(AddressOf HandleSinglePlayer)
            PlayerThreads.Add(thread)
            thread.Start()

        Next
        Server.Send("switching=start=" & Server.SwitchingTime)

        'I 30 sekunder kan man byta och läga ner kort...
        GameState = "Switching"
        Thread.Sleep(Server.SwitchingTime * 1000)

        Server.Send("switching=stop")
        GameState = "Playing"

        Console.WriteLine("Spelet har startat")

        Server.Turn = 0
        While True
            'Try
            'LOOP FÖR TURORDNINGEN

            If Server.Clients.Count = 0 Then
                Exit While
            End If

            'Talar om vilkens tur det är
            Server.Send("turn=" & Server.Clients(Server.Turn).Name)

            WaitForAnswer()

            Server.Turn += 1
            If (Server.Turn >= Server.Clients.Count) Then
                Server.Turn = 0
            End If
            If Server.Clients.Count = 0 Then
                Exit While
            End If
            For i As Integer = 0 To Server.Clients.Count
                If Server.Clients(Server.Turn).Finished Then
                    If (i = Server.Clients.Count) Then
                        Exit While
                    End If
                    Server.Turn += 1
                    If (Server.Turn >= Server.Clients.Count) Then
                        Server.Turn = 0
                    End If
                ElseIf (Server.Clients(Server.Turn).Client.Connected) Then
                    Exit For
                End If
            Next
            'Catch ex As Exception
            'MsgBox("Ett fel uppstod och spelet måste tyvärr avslutas:" & vbCrLf & vbCrLf & ex.Message)
            'Me.Close()
            'End Try
        End While

        MsgBox("Spelet klart!")
    End Sub

    Private Sub WaitForAnswer()
        'Tar emot ett svar från en spelare
        Dim answer As String = Server.Clients(Server.Turn).Recieve()
        HandleAnswer(answer)
    End Sub

    Private Sub HandleAnswer(ByVal Recieved As String)
        'tar emot ett svar, hanterar och skickar vidare det
        Dim LayedCards As New ArrayList
        Dim answer() = Recieved.Split("=")
        If (answer(0) = "layedcard") Then
            'Spelaren har lagt ett kort

            'Lägger in de korten spelaren lagt i LayedCards-arrayen
            For c As Integer = 0 To answer(1).Split(",").Length - 1
                LayedCards.Add(New Card(answer(1).Split(",")(c).Split("&")(0)))
                PackOfCards.LayedCards.Add(LayedCards(LayedCards.Count - 1))
            Next

            'Om spelaren har gått ut eller vill ha fler kort
            If (answer.Length >= 3) Then
                If (answer.Length >= 4) Then
                    If (answer(3) = "win") Then
                        'Den aktuelle spelaren har gått ut.
                        Server.Clients(Server.Turn).Finished = True
                    End If
                Else
                    'Den aktuelle spelaren vill ha fler kort
                    For c As Integer = 1 To Integer.Parse(answer(2))
                        If (PackOfCards.Cards.Count > 0) Then
                            Server.Clients(Server.Turn).Send("yournewcard=" & PackOfCards.GetNext().Merged)
                            For i As Integer = 0 To Server.Clients.Count - 1
                                If Not (i = Server.Turn) Then
                                    Server.Clients(i).Send("newcard=" & Server.Clients(Server.Turn).Name)
                                End If
                            Next
                        End If
                    Next
                End If
            End If

            'Meddelar alla andra om vad det är för kort som är lagt
            Server.Send(Recieved)

            Dim j = PackOfCards.LayedCards.Count - 4
            If ((LayedCards(0).Number = 2 Or LayedCards(0).Number = 10) And Not Server.Clients(Server.Turn).Finished) Then
                Server.Send("turn=" & Server.Clients(Server.Turn).Name)
                WaitForAnswer()
            ElseIf (j >= 0) Then
                If (PackOfCards.LayedCards(j).Number = PackOfCards.LayedCards(j + 1).Number And PackOfCards.LayedCards(j + 1).Number = PackOfCards.LayedCards(j + 2).Number And PackOfCards.LayedCards(j + 2).Number = PackOfCards.LayedCards(j + 3).Number) And Not Server.Clients(Server.Turn).Finished Then
                    Server.Send("turn=" & Server.Clients(Server.Turn).Name)
                    WaitForAnswer()
                End If
            End If

        ElseIf (answer(0) = "laydown") Then
            Server.Send(Recieved)
            If Not (answer(5) = "") Then
                'Den aktuelle spelaren vill ha fler kort
                For c As Integer = 1 To Integer.Parse(answer(5))
                    If (PackOfCards.Cards.Count > 0) Then
                        Server.Clients(Server.Turn).Send("yournewcard=" & PackOfCards.GetNext().Merged)
                        For i As Integer = 0 To Server.Clients.Count - 1
                            If Not (i = Server.Turn) Then
                                Server.Clients(i).Send("newcard=" & Server.Clients(Server.Turn).Name)
                            End If
                        Next
                    End If
                Next
            End If
            WaitForAnswer()
            ElseIf (answer(0) = "chance") Then
            Server.Send("chancecard=" & PackOfCards.GetNext().Merged)
                WaitForAnswer()
            ElseIf (answer(0) = "OK") Then
                Exit Sub
        ElseIf (answer(0) = "tookpile") Then
            If (answer.Length >= 2) Then
                PackOfCards.LayedCards.Add(New Card(answer(1).Split("&")(0)))
            End If
            PackOfCards.LayedCards.Clear()
            Server.Send(Recieved)
        ElseIf (answer(0) = "") Then
            'Klientens anslutning avbröts, tar bort spelaren
            MsgBox(Server.Clients(Server.Turn).Name & "s anslutning avbröts och denne utesluts från spelet")
            Dim Name = Server.Clients(Server.Turn).Name
            Server.Clients.Remove(Server.Clients(Server.Turn))
            Server.Send("excluded=" & Name)
            Application.OpenForms(0).Invoke(New MethodInvoker(AddressOf FillList))
            Server.Turn -= 1
        Else
            WaitForAnswer() 'Om klienten svarar nått annat så börja om...
        End If
    End Sub

    Private Sub HandleSinglePlayer()
        Dim Player As Player = Server.Clients(ThreadCount)
        ThreadCount += 1

        While True
            Dim data As String = Player.Recieve()

            'Hantera det mottagna datat...
            If (GameState = "Switching") Then
                Dim DataSplitted = data.Split("=")

                If (DataSplitted(0) = "tookup") Then
                    Server.Send(data)
                ElseIf (DataSplitted(0) = "laydown") Then
                    Server.Send(data)
                    If Not (DataSplitted(5) = "") Then
                        'Skickar ett nytt kort till spelaren
                        For i As Integer = 1 To Integer.Parse(DataSplitted(5))
                            If (PackOfCards.Cards.Count > 0) Then
                                Player.Send("yournewcard=" & PackOfCards.GetNext().Merged)
                                For Each c As Player In Server.Clients
                                    If Not (c.Equals(Player)) Then
                                        c.Send("newcard=" & Player.Name)
                                    End If
                                Next
                            End If
                        Next
                    End If
                ElseIf (DataSplitted(0) = "newcard") Then

                    'Skickar ett nytt kort till spelaren
                    If (PackOfCards.Cards.Count > 0) Then
                        Player.Send("yournewcard=" & PackOfCards.GetNext().Merged)
                        For Each c As Player In Server.Clients
                            If Not (c.Equals(Player)) Then
                                c.Send(data)
                            End If
                        Next
                    End If
                ElseIf (DataSplitted(0) = "") Then
                    'Klientens anslutning avbröts, tar bort spelaren
                    MsgBox(Player.Name & "s anslutning avbröts och denne utesluts från spelet")
                    Dim Name = Player.Name
                    Server.Clients.Remove(Player)
                    Server.Send("excluded=" & Name)
                    Application.OpenForms(0).Invoke(New MethodInvoker(AddressOf FillList))
                    Exit Sub
                End If

            ElseIf (GameState = "Playing") Then
                Server.Clients(Server.Turn).Send("sendagain=" & data)
                Console.WriteLine("PlayerThread Ends")
                Exit Sub
            End If
        End While
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Avsluta.Click
        'Stänger alla threads
        For Each t As Thread In PlayerThreads
            Try
                t.Abort()
            Catch ex As Exception
            End Try
        Next
        Dim form1 As New Form1
        form1.Location = New System.Drawing.Point(Me.Location.X, Me.Location.Y)
        form1.Show()
        Me.Close()
    End Sub

    Private Sub PackOfCards_OnClear() Handles PackOfCards.OnClear
        Server.Send("packofcards=out")
    End Sub

    Private Sub BanPlayer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BanPlayer.Click
        If Not PlayersList.SelectedIndex = -1 Then
            Server.Clients(PlayersList.SelectedIndex).Client.Close()
            If Not PlayersList.SelectedIndex = Server.Turn Then
                Dim Name = Server.Clients(PlayersList.SelectedIndex).Name
                Server.Clients.Remove(Server.Clients(PlayersList.SelectedIndex))
                Server.Send("excluded=" & Name)
                FillList()
            End If
        End If
    End Sub

    Private Sub PlayersList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PlayersList.SelectedIndexChanged
        If Not PlayersList.SelectedIndex = -1 Then
            Me.BanPlayer.Enabled = True
        Else
            Me.BanPlayer.Enabled = False
        End If
    End Sub
End Class