<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Couplers
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Pnl_Type = New System.Windows.Forms.Panel()
        Me.RadioMelody = New System.Windows.Forms.RadioButton()
        Me.RadioBass = New System.Windows.Forms.RadioButton()
        Me.RadioUOff = New System.Windows.Forms.RadioButton()
        Me.Radio4 = New System.Windows.Forms.RadioButton()
        Me.Radio8 = New System.Windows.Forms.RadioButton()
        Me.Radio16 = New System.Windows.Forms.RadioButton()
        Me.Pnl_Dest = New System.Windows.Forms.Panel()
        Me.RadioDMan6 = New System.Windows.Forms.RadioButton()
        Me.RadioDMan5 = New System.Windows.Forms.RadioButton()
        Me.RadioDMan4 = New System.Windows.Forms.RadioButton()
        Me.RadioDMan3 = New System.Windows.Forms.RadioButton()
        Me.RadioDMan2 = New System.Windows.Forms.RadioButton()
        Me.RadioDMan1 = New System.Windows.Forms.RadioButton()
        Me.RadioDPed = New System.Windows.Forms.RadioButton()
        Me.Pnl_Source = New System.Windows.Forms.Panel()
        Me.RadioSMan6 = New System.Windows.Forms.RadioButton()
        Me.RadioSMan5 = New System.Windows.Forms.RadioButton()
        Me.RadioSMan4 = New System.Windows.Forms.RadioButton()
        Me.RadioSMan3 = New System.Windows.Forms.RadioButton()
        Me.RadioSMan2 = New System.Windows.Forms.RadioButton()
        Me.RadioSMan1 = New System.Windows.Forms.RadioButton()
        Me.RadioSPed = New System.Windows.Forms.RadioButton()
        Me.Lbl_Source = New System.Windows.Forms.Label()
        Me.Lbl_Dest = New System.Windows.Forms.Label()
        Me.Lbl_Type = New System.Windows.Forms.Label()
        Me.Lbl_CplrCode1 = New System.Windows.Forms.Label()
        Me.Txt_CplrCode = New System.Windows.Forms.TextBox()
        Me.Lbl_CplrCode2 = New System.Windows.Forms.Label()
        Me.Lbl_Find = New System.Windows.Forms.Label()
        Me.Btn_FindCode = New System.Windows.Forms.Button()
        Me.Pnl_Type.SuspendLayout()
        Me.Pnl_Dest.SuspendLayout()
        Me.Pnl_Source.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl_Type
        '
        Me.Pnl_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pnl_Type.Controls.Add(Me.RadioMelody)
        Me.Pnl_Type.Controls.Add(Me.RadioBass)
        Me.Pnl_Type.Controls.Add(Me.RadioUOff)
        Me.Pnl_Type.Controls.Add(Me.Radio4)
        Me.Pnl_Type.Controls.Add(Me.Radio8)
        Me.Pnl_Type.Controls.Add(Me.Radio16)
        Me.Pnl_Type.Location = New System.Drawing.Point(310, 63)
        Me.Pnl_Type.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Pnl_Type.Name = "Pnl_Type"
        Me.Pnl_Type.Size = New System.Drawing.Size(110, 196)
        Me.Pnl_Type.TabIndex = 0
        '
        'RadioMelody
        '
        Me.RadioMelody.AutoSize = True
        Me.RadioMelody.Location = New System.Drawing.Point(15, 159)
        Me.RadioMelody.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioMelody.Name = "RadioMelody"
        Me.RadioMelody.Size = New System.Drawing.Size(65, 19)
        Me.RadioMelody.TabIndex = 5
        Me.RadioMelody.Tag = "4"
        Me.RadioMelody.Text = "Melody"
        Me.RadioMelody.UseVisualStyleBackColor = True
        '
        'RadioBass
        '
        Me.RadioBass.AutoSize = True
        Me.RadioBass.Location = New System.Drawing.Point(15, 132)
        Me.RadioBass.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioBass.Name = "RadioBass"
        Me.RadioBass.Size = New System.Drawing.Size(48, 19)
        Me.RadioBass.TabIndex = 4
        Me.RadioBass.Tag = "3"
        Me.RadioBass.Text = "Bass"
        Me.RadioBass.UseVisualStyleBackColor = True
        '
        'RadioUOff
        '
        Me.RadioUOff.AutoSize = True
        Me.RadioUOff.Location = New System.Drawing.Point(15, 104)
        Me.RadioUOff.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioUOff.Name = "RadioUOff"
        Me.RadioUOff.Size = New System.Drawing.Size(66, 19)
        Me.RadioUOff.TabIndex = 3
        Me.RadioUOff.Tag = "1"
        Me.RadioUOff.Text = "Uni. Off"
        Me.RadioUOff.UseVisualStyleBackColor = True
        '
        'Radio4
        '
        Me.Radio4.AutoSize = True
        Me.Radio4.Location = New System.Drawing.Point(15, 76)
        Me.Radio4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Radio4.Name = "Radio4"
        Me.Radio4.Size = New System.Drawing.Size(76, 19)
        Me.Radio4.TabIndex = 2
        Me.Radio4.Tag = "2"
        Me.Radio4.Text = "4 ft. (Sup)"
        Me.Radio4.UseVisualStyleBackColor = True
        '
        'Radio8
        '
        Me.Radio8.AutoSize = True
        Me.Radio8.Location = New System.Drawing.Point(15, 48)
        Me.Radio8.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Radio8.Name = "Radio8"
        Me.Radio8.Size = New System.Drawing.Size(74, 19)
        Me.Radio8.TabIndex = 1
        Me.Radio8.Tag = "1"
        Me.Radio8.Text = "8 ft. (Uni)"
        Me.Radio8.UseVisualStyleBackColor = True
        '
        'Radio16
        '
        Me.Radio16.AutoSize = True
        Me.Radio16.Checked = True
        Me.Radio16.Location = New System.Drawing.Point(15, 21)
        Me.Radio16.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Radio16.Name = "Radio16"
        Me.Radio16.Size = New System.Drawing.Size(82, 19)
        Me.Radio16.TabIndex = 0
        Me.Radio16.TabStop = True
        Me.Radio16.Tag = "0"
        Me.Radio16.Text = "16 ft. (Sub)"
        Me.Radio16.UseVisualStyleBackColor = True
        '
        'Pnl_Dest
        '
        Me.Pnl_Dest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pnl_Dest.Controls.Add(Me.RadioDMan6)
        Me.Pnl_Dest.Controls.Add(Me.RadioDMan5)
        Me.Pnl_Dest.Controls.Add(Me.RadioDMan4)
        Me.Pnl_Dest.Controls.Add(Me.RadioDMan3)
        Me.Pnl_Dest.Controls.Add(Me.RadioDMan2)
        Me.Pnl_Dest.Controls.Add(Me.RadioDMan1)
        Me.Pnl_Dest.Controls.Add(Me.RadioDPed)
        Me.Pnl_Dest.Location = New System.Drawing.Point(172, 63)
        Me.Pnl_Dest.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Pnl_Dest.Name = "Pnl_Dest"
        Me.Pnl_Dest.Size = New System.Drawing.Size(110, 225)
        Me.Pnl_Dest.TabIndex = 1
        '
        'RadioDMan6
        '
        Me.RadioDMan6.AutoSize = True
        Me.RadioDMan6.Location = New System.Drawing.Point(14, 188)
        Me.RadioDMan6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioDMan6.Name = "RadioDMan6"
        Me.RadioDMan6.Size = New System.Drawing.Size(74, 19)
        Me.RadioDMan6.TabIndex = 6
        Me.RadioDMan6.Tag = "1600"
        Me.RadioDMan6.Text = "Manual 6"
        Me.RadioDMan6.UseVisualStyleBackColor = True
        '
        'RadioDMan5
        '
        Me.RadioDMan5.AutoSize = True
        Me.RadioDMan5.Location = New System.Drawing.Point(15, 159)
        Me.RadioDMan5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioDMan5.Name = "RadioDMan5"
        Me.RadioDMan5.Size = New System.Drawing.Size(74, 19)
        Me.RadioDMan5.TabIndex = 5
        Me.RadioDMan5.Tag = "1500"
        Me.RadioDMan5.Text = "Manual 5"
        Me.RadioDMan5.UseVisualStyleBackColor = True
        '
        'RadioDMan4
        '
        Me.RadioDMan4.AutoSize = True
        Me.RadioDMan4.Location = New System.Drawing.Point(15, 132)
        Me.RadioDMan4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioDMan4.Name = "RadioDMan4"
        Me.RadioDMan4.Size = New System.Drawing.Size(74, 19)
        Me.RadioDMan4.TabIndex = 4
        Me.RadioDMan4.Tag = "1400"
        Me.RadioDMan4.Text = "Manual 4"
        Me.RadioDMan4.UseVisualStyleBackColor = True
        '
        'RadioDMan3
        '
        Me.RadioDMan3.AutoSize = True
        Me.RadioDMan3.Location = New System.Drawing.Point(15, 104)
        Me.RadioDMan3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioDMan3.Name = "RadioDMan3"
        Me.RadioDMan3.Size = New System.Drawing.Size(74, 19)
        Me.RadioDMan3.TabIndex = 3
        Me.RadioDMan3.Tag = "1300"
        Me.RadioDMan3.Text = "Manual 3"
        Me.RadioDMan3.UseVisualStyleBackColor = True
        '
        'RadioDMan2
        '
        Me.RadioDMan2.AutoSize = True
        Me.RadioDMan2.Location = New System.Drawing.Point(15, 76)
        Me.RadioDMan2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioDMan2.Name = "RadioDMan2"
        Me.RadioDMan2.Size = New System.Drawing.Size(74, 19)
        Me.RadioDMan2.TabIndex = 2
        Me.RadioDMan2.Tag = "1200"
        Me.RadioDMan2.Text = "Manual 2"
        Me.RadioDMan2.UseVisualStyleBackColor = True
        '
        'RadioDMan1
        '
        Me.RadioDMan1.AutoSize = True
        Me.RadioDMan1.Location = New System.Drawing.Point(15, 48)
        Me.RadioDMan1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioDMan1.Name = "RadioDMan1"
        Me.RadioDMan1.Size = New System.Drawing.Size(74, 19)
        Me.RadioDMan1.TabIndex = 1
        Me.RadioDMan1.Tag = "1100"
        Me.RadioDMan1.Text = "Manual 1"
        Me.RadioDMan1.UseVisualStyleBackColor = True
        '
        'RadioDPed
        '
        Me.RadioDPed.AutoSize = True
        Me.RadioDPed.Checked = True
        Me.RadioDPed.Location = New System.Drawing.Point(15, 21)
        Me.RadioDPed.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioDPed.Name = "RadioDPed"
        Me.RadioDPed.Size = New System.Drawing.Size(54, 19)
        Me.RadioDPed.TabIndex = 0
        Me.RadioDPed.TabStop = True
        Me.RadioDPed.Tag = "1000"
        Me.RadioDPed.Text = "Pedal"
        Me.RadioDPed.UseVisualStyleBackColor = True
        '
        'Pnl_Source
        '
        Me.Pnl_Source.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pnl_Source.Controls.Add(Me.RadioSMan6)
        Me.Pnl_Source.Controls.Add(Me.RadioSMan5)
        Me.Pnl_Source.Controls.Add(Me.RadioSMan4)
        Me.Pnl_Source.Controls.Add(Me.RadioSMan3)
        Me.Pnl_Source.Controls.Add(Me.RadioSMan2)
        Me.Pnl_Source.Controls.Add(Me.RadioSMan1)
        Me.Pnl_Source.Controls.Add(Me.RadioSPed)
        Me.Pnl_Source.Location = New System.Drawing.Point(36, 62)
        Me.Pnl_Source.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Pnl_Source.Name = "Pnl_Source"
        Me.Pnl_Source.Size = New System.Drawing.Size(110, 225)
        Me.Pnl_Source.TabIndex = 2
        '
        'RadioSMan6
        '
        Me.RadioSMan6.AutoSize = True
        Me.RadioSMan6.Location = New System.Drawing.Point(14, 188)
        Me.RadioSMan6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioSMan6.Name = "RadioSMan6"
        Me.RadioSMan6.Size = New System.Drawing.Size(74, 19)
        Me.RadioSMan6.TabIndex = 6
        Me.RadioSMan6.Tag = "30"
        Me.RadioSMan6.Text = "Manual 6"
        Me.RadioSMan6.UseVisualStyleBackColor = True
        '
        'RadioSMan5
        '
        Me.RadioSMan5.AutoSize = True
        Me.RadioSMan5.Location = New System.Drawing.Point(15, 159)
        Me.RadioSMan5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioSMan5.Name = "RadioSMan5"
        Me.RadioSMan5.Size = New System.Drawing.Size(74, 19)
        Me.RadioSMan5.TabIndex = 5
        Me.RadioSMan5.Tag = "25"
        Me.RadioSMan5.Text = "Manual 5"
        Me.RadioSMan5.UseVisualStyleBackColor = True
        '
        'RadioSMan4
        '
        Me.RadioSMan4.AutoSize = True
        Me.RadioSMan4.Location = New System.Drawing.Point(15, 132)
        Me.RadioSMan4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioSMan4.Name = "RadioSMan4"
        Me.RadioSMan4.Size = New System.Drawing.Size(74, 19)
        Me.RadioSMan4.TabIndex = 4
        Me.RadioSMan4.Tag = "20"
        Me.RadioSMan4.Text = "Manual 4"
        Me.RadioSMan4.UseVisualStyleBackColor = True
        '
        'RadioSMan3
        '
        Me.RadioSMan3.AutoSize = True
        Me.RadioSMan3.Location = New System.Drawing.Point(15, 104)
        Me.RadioSMan3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioSMan3.Name = "RadioSMan3"
        Me.RadioSMan3.Size = New System.Drawing.Size(74, 19)
        Me.RadioSMan3.TabIndex = 3
        Me.RadioSMan3.Tag = "15"
        Me.RadioSMan3.Text = "Manual 3"
        Me.RadioSMan3.UseVisualStyleBackColor = True
        '
        'RadioSMan2
        '
        Me.RadioSMan2.AutoSize = True
        Me.RadioSMan2.Location = New System.Drawing.Point(15, 76)
        Me.RadioSMan2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioSMan2.Name = "RadioSMan2"
        Me.RadioSMan2.Size = New System.Drawing.Size(74, 19)
        Me.RadioSMan2.TabIndex = 2
        Me.RadioSMan2.Tag = "10"
        Me.RadioSMan2.Text = "Manual 2"
        Me.RadioSMan2.UseVisualStyleBackColor = True
        '
        'RadioSMan1
        '
        Me.RadioSMan1.AutoSize = True
        Me.RadioSMan1.Location = New System.Drawing.Point(15, 48)
        Me.RadioSMan1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioSMan1.Name = "RadioSMan1"
        Me.RadioSMan1.Size = New System.Drawing.Size(74, 19)
        Me.RadioSMan1.TabIndex = 1
        Me.RadioSMan1.Tag = "5"
        Me.RadioSMan1.Text = "Manual 1"
        Me.RadioSMan1.UseVisualStyleBackColor = True
        '
        'RadioSPed
        '
        Me.RadioSPed.AutoSize = True
        Me.RadioSPed.Checked = True
        Me.RadioSPed.Location = New System.Drawing.Point(15, 21)
        Me.RadioSPed.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.RadioSPed.Name = "RadioSPed"
        Me.RadioSPed.Size = New System.Drawing.Size(54, 19)
        Me.RadioSPed.TabIndex = 0
        Me.RadioSPed.TabStop = True
        Me.RadioSPed.Tag = "0"
        Me.RadioSPed.Text = "Pedal"
        Me.RadioSPed.UseVisualStyleBackColor = True
        '
        'Lbl_Source
        '
        Me.Lbl_Source.AutoSize = True
        Me.Lbl_Source.ForeColor = System.Drawing.Color.DarkRed
        Me.Lbl_Source.Location = New System.Drawing.Point(48, 44)
        Me.Lbl_Source.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_Source.Name = "Lbl_Source"
        Me.Lbl_Source.Size = New System.Drawing.Size(43, 15)
        Me.Lbl_Source.TabIndex = 3
        Me.Lbl_Source.Text = "Source"
        '
        'Lbl_Dest
        '
        Me.Lbl_Dest.AutoSize = True
        Me.Lbl_Dest.ForeColor = System.Drawing.Color.DarkRed
        Me.Lbl_Dest.Location = New System.Drawing.Point(185, 45)
        Me.Lbl_Dest.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_Dest.Name = "Lbl_Dest"
        Me.Lbl_Dest.Size = New System.Drawing.Size(67, 15)
        Me.Lbl_Dest.TabIndex = 4
        Me.Lbl_Dest.Text = "Destination"
        '
        'Lbl_Type
        '
        Me.Lbl_Type.AutoSize = True
        Me.Lbl_Type.ForeColor = System.Drawing.Color.DarkRed
        Me.Lbl_Type.Location = New System.Drawing.Point(318, 45)
        Me.Lbl_Type.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_Type.Name = "Lbl_Type"
        Me.Lbl_Type.Size = New System.Drawing.Size(31, 15)
        Me.Lbl_Type.TabIndex = 5
        Me.Lbl_Type.Text = "Type"
        '
        'Lbl_CplrCode1
        '
        Me.Lbl_CplrCode1.AutoSize = True
        Me.Lbl_CplrCode1.Location = New System.Drawing.Point(33, 10)
        Me.Lbl_CplrCode1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_CplrCode1.Name = "Lbl_CplrCode1"
        Me.Lbl_CplrCode1.Size = New System.Drawing.Size(166, 15)
        Me.Lbl_CplrCode1.TabIndex = 6
        Me.Lbl_CplrCode1.Text = "Enter the 4 digit Coupler Code"
        '
        'Txt_CplrCode
        '
        Me.Txt_CplrCode.Location = New System.Drawing.Point(204, 7)
        Me.Txt_CplrCode.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Txt_CplrCode.Name = "Txt_CplrCode"
        Me.Txt_CplrCode.Size = New System.Drawing.Size(62, 23)
        Me.Txt_CplrCode.TabIndex = 7
        '
        'Lbl_CplrCode2
        '
        Me.Lbl_CplrCode2.AutoSize = True
        Me.Lbl_CplrCode2.Location = New System.Drawing.Point(273, 10)
        Me.Lbl_CplrCode2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_CplrCode2.Name = "Lbl_CplrCode2"
        Me.Lbl_CplrCode2.Size = New System.Drawing.Size(139, 15)
        Me.Lbl_CplrCode2.TabIndex = 8
        Me.Lbl_CplrCode2.Text = "then press the ENTER key"
        '
        'Lbl_Find
        '
        Me.Lbl_Find.AutoSize = True
        Me.Lbl_Find.Location = New System.Drawing.Point(33, 308)
        Me.Lbl_Find.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_Find.Name = "Lbl_Find"
        Me.Lbl_Find.Size = New System.Drawing.Size(274, 15)
        Me.Lbl_Find.TabIndex = 9
        Me.Lbl_Find.Text = "Check a radio button in each rectangle, then press:"
        '
        'Btn_FindCode
        '
        Me.Btn_FindCode.Location = New System.Drawing.Point(310, 301)
        Me.Btn_FindCode.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_FindCode.Name = "Btn_FindCode"
        Me.Btn_FindCode.Size = New System.Drawing.Size(80, 29)
        Me.Btn_FindCode.TabIndex = 10
        Me.Btn_FindCode.Text = "Find Code"
        Me.Btn_FindCode.UseVisualStyleBackColor = True
        '
        'Couplers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(461, 347)
        Me.Controls.Add(Me.Btn_FindCode)
        Me.Controls.Add(Me.Lbl_Find)
        Me.Controls.Add(Me.Lbl_CplrCode2)
        Me.Controls.Add(Me.Txt_CplrCode)
        Me.Controls.Add(Me.Lbl_CplrCode1)
        Me.Controls.Add(Me.Lbl_Type)
        Me.Controls.Add(Me.Lbl_Dest)
        Me.Controls.Add(Me.Lbl_Source)
        Me.Controls.Add(Me.Pnl_Source)
        Me.Controls.Add(Me.Pnl_Dest)
        Me.Controls.Add(Me.Pnl_Type)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "Couplers"
        Me.Text = "Couplers"
        Me.Pnl_Type.ResumeLayout(False)
        Me.Pnl_Type.PerformLayout()
        Me.Pnl_Dest.ResumeLayout(False)
        Me.Pnl_Dest.PerformLayout()
        Me.Pnl_Source.ResumeLayout(False)
        Me.Pnl_Source.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Pnl_Type As System.Windows.Forms.Panel
    Friend WithEvents Radio16 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioMelody As System.Windows.Forms.RadioButton
    Friend WithEvents RadioBass As System.Windows.Forms.RadioButton
    Friend WithEvents RadioUOff As System.Windows.Forms.RadioButton
    Friend WithEvents Radio4 As System.Windows.Forms.RadioButton
    Friend WithEvents Radio8 As System.Windows.Forms.RadioButton
    Friend WithEvents Pnl_Dest As System.Windows.Forms.Panel
    Friend WithEvents RadioDMan5 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioDMan4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioDMan3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioDMan2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioDMan1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioDPed As System.Windows.Forms.RadioButton
    Friend WithEvents RadioDMan6 As System.Windows.Forms.RadioButton
    Friend WithEvents Pnl_Source As System.Windows.Forms.Panel
    Friend WithEvents RadioSMan6 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioSMan5 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioSMan4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioSMan3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioSMan2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioSMan1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioSPed As System.Windows.Forms.RadioButton
    Friend WithEvents Lbl_Source As System.Windows.Forms.Label
    Friend WithEvents Lbl_Dest As System.Windows.Forms.Label
    Friend WithEvents Lbl_Type As System.Windows.Forms.Label
    Friend WithEvents Lbl_CplrCode1 As System.Windows.Forms.Label
    Friend WithEvents Txt_CplrCode As System.Windows.Forms.TextBox
    Friend WithEvents Lbl_CplrCode2 As System.Windows.Forms.Label
    Friend WithEvents Lbl_Find As System.Windows.Forms.Label
    Friend WithEvents Btn_FindCode As System.Windows.Forms.Button
End Class
