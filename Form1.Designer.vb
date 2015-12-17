<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label
        Me.port_txt = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Start_btn = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.SwitchingTime = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        CType(Me.SwitchingTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(319, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Välkommen till Vändtia Live Server. Starta servern genom att ange" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "portnummer och" & _
            " önskad bytestid och klicka på ""Starta"" nedan!"
        '
        'port_txt
        '
        Me.port_txt.Location = New System.Drawing.Point(15, 78)
        Me.port_txt.Name = "port_txt"
        Me.port_txt.Size = New System.Drawing.Size(100, 20)
        Me.port_txt.TabIndex = 1
        Me.port_txt.Text = "9090"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Portnummer:"
        '
        'Start_btn
        '
        Me.Start_btn.Location = New System.Drawing.Point(12, 161)
        Me.Start_btn.Name = "Start_btn"
        Me.Start_btn.Size = New System.Drawing.Size(75, 23)
        Me.Start_btn.TabIndex = 3
        Me.Start_btn.Text = "Starta"
        Me.Start_btn.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Bytestid:"
        '
        'SwitchingTime
        '
        Me.SwitchingTime.Location = New System.Drawing.Point(15, 122)
        Me.SwitchingTime.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.SwitchingTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.SwitchingTime.Name = "SwitchingTime"
        Me.SwitchingTime.Size = New System.Drawing.Size(65, 20)
        Me.SwitchingTime.TabIndex = 5
        Me.SwitchingTime.Value = New Decimal(New Integer() {45, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(281, 171)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Version: 1.1.2"
        '
        'Form1
        '
        Me.AcceptButton = Me.Start_btn
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(372, 196)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.SwitchingTime)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Start_btn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.port_txt)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Vändtia Live Server"
        CType(Me.SwitchingTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents port_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Start_btn As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SwitchingTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
