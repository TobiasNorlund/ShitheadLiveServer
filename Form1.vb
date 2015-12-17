Imports System.Net

Public Class Form1
    Private Sub Start_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Start_btn.Click
        Try
            Dim form2 As New Form2()
            form2.Port = Convert.ToInt32(port_txt.Text)
            form2.SwitchingTime = Me.SwitchingTime.Value
            form2.Location = New System.Drawing.Point(Me.Location.X, Me.Location.Y)
            form2.Show()
            Me.Close()
        Catch ex As Exception
            MsgBox("Ett fel uppstod: " & vbCrLf & vbCrLf & ex.Message)
        End Try
    End Sub

End Class
