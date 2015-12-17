<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3))
        Me.Label1 = New System.Windows.Forms.Label
        Me.PlayersList = New System.Windows.Forms.ListBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Avsluta = New System.Windows.Forms.Button
        Me.BanPlayer = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Spelet har börjat"
        '
        'PlayersList
        '
        Me.PlayersList.FormattingEnabled = True
        Me.PlayersList.Location = New System.Drawing.Point(16, 56)
        Me.PlayersList.Name = "PlayersList"
        Me.PlayersList.Size = New System.Drawing.Size(191, 95)
        Me.PlayersList.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Spelare:"
        '
        'Avsluta
        '
        Me.Avsluta.Location = New System.Drawing.Point(278, 161)
        Me.Avsluta.Name = "Avsluta"
        Me.Avsluta.Size = New System.Drawing.Size(82, 23)
        Me.Avsluta.TabIndex = 3
        Me.Avsluta.Text = "Avsluta"
        Me.Avsluta.UseVisualStyleBackColor = True
        '
        'BanPlayer
        '
        Me.BanPlayer.Enabled = False
        Me.BanPlayer.Location = New System.Drawing.Point(213, 56)
        Me.BanPlayer.Name = "BanPlayer"
        Me.BanPlayer.Size = New System.Drawing.Size(98, 23)
        Me.BanPlayer.TabIndex = 4
        Me.BanPlayer.Text = "Stäng av spelare"
        Me.BanPlayer.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(168, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Copyright © 2008 Tobias Norlund"
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(372, 196)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BanPlayer)
        Me.Controls.Add(Me.Avsluta)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PlayersList)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form3"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Vändtia Live Server"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PlayersList As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Avsluta As System.Windows.Forms.Button
    Friend WithEvents BanPlayer As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
