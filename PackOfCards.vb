Public Class PackOfCards
    Public Cards As New ArrayList
    Public LayedCards As New ArrayList
    Public Event OnClear()

    Public Sub New()
        'Skapar alla kort
        Dim Type As String = "Hjärter"
        For i As Integer = 1 To 4
            For j As Integer = 1 To 13
                Cards.Add(New Card(Type, j))
            Next
            Select Case (Type)
                Case "Hjärter"
                    Type = "Spader"
                    Exit Select
                Case "Spader"
                    Type = "Ruter"
                    Exit Select
                Case "Ruter"
                    Type = "Klöver"
            End Select
        Next
    End Sub

    Public Sub Bland()
        Dim NewList As New ArrayList, r As New Random(), Test As Integer
        While NewList.Count < Cards.Count
            Test = Convert.ToInt32(Math.Floor(r.NextDouble * Cards.Count))
            'Itererar igenom och kollar så den inte har slumpats tidigare
            For Each Iterator As Card In NewList
                If (Iterator.Merged = Cards(Test).Merged) Then
                    Continue While
                End If
            Next
            'Den verkar inte ha funnits
            NewList.Add(Cards(Test))
        End While
        Cards.Clear()
        Cards.AddRange(NewList)
    End Sub

    Public Function GetNext() As Card
        Try
            Dim card As Card = Cards(Cards.Count - 1)
            Cards.Remove(card)

            If (Cards.Count = 0) Then
                RaiseEvent OnClear()
            End If
            Return card
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class

Public Class Card
    Public Type As String
    Public Number As Integer
    Public Merged As String

    Public Sub New(ByVal Type As String, ByVal Number As Integer)
        Me.Type = Type
        Me.Number = Number
        Me.Merged = Type.Substring(0, 1) & Number
    End Sub

    Public Sub New(ByVal Merged As String)
        Me.Merged = Merged
        Dim TypeStart = Merged.Substring(0, 1)
        If (TypeStart = "H") Then
            Me.Type = "Hjärter"
        ElseIf (TypeStart = "S") Then
            Me.Type = "Spader"
        ElseIf (TypeStart = "R") Then
            Me.Type = "Ruter"
        Else
            Me.Type = "Klöver"
        End If
        Me.Number = Convert.ToInt16(Merged.Substring(1))
    End Sub
End Class
