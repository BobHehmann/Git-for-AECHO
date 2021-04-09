Public Class Couplers

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TBoxCplrCode.KeyDown
        ' VALIDATION AVEC LA TOUCHE ENTER
        Dim cplrCode As Integer = Val(TBoxCplrCode.Text)
        Dim destCode As Integer = Int(cplrCode / 100)
        Dim srcCode As Integer = cplrCode Mod 100
        Dim typeCode As Integer = cplrCode Mod 5

        If e.KeyCode = Keys.Enter Then
            Select Case destCode
                Case 10 : RadioDPed.Checked = True
                Case 11 : RadioDMan1.Checked = True
                Case 12 : RadioDMan2.Checked = True
                Case 13 : RadioDMan3.Checked = True
                Case 14 : RadioDMan4.Checked = True
                Case 15 : RadioDman5.Checked = True
                Case 16 : RadioDMan6.Checked = True
                Case Else : MsgBox("Error ! This is not a valid coupler code")
            End Select
            Select Case srcCode
                Case 0 To 4 : RadioSPed.Checked = True
                Case 5 To 9 : RadioSMan1.Checked = True
                Case 10 To 14 : RadioSMan2.Checked = True
                Case 15 To 19 : RadioSman3.Checked = True
                Case 20 To 24 : RadioSMan4.Checked = True
                Case 29 To 29 : RadioSMan5.Checked = True
                Case 30 To 34 : RadioSMan6.Checked = True
                Case Else : MsgBox("Error ! This is not a valid coupler code")
            End Select
            Select Case typeCode
                Case 0 : Radio16.Checked = True
                Case 1 : Radio8.Checked = True
                Case 2 : Radio4.Checked = True
                Case 3 : RadioBass.Checked = True
                Case 4 : RadioMelody.Checked = True
            End Select
            ' cas de unisson off
            If RadioSPed.Checked = True And RadioDPed.Checked = True And typeCode = 1 Then RadioUOff.Checked = True
            If RadioSMan1.Checked = True And RadioDMan1.Checked = True And typeCode = 1 Then RadioUOff.Checked = True
            If RadioSMan2.Checked = True And RadioDMan2.Checked = True And typeCode = 1 Then RadioUOff.Checked = True
            If RadioSman3.Checked = True And RadioDMan3.Checked = True And typeCode = 1 Then RadioUOff.Checked = True
            If RadioSMan4.Checked = True And RadioDMan4.Checked = True And typeCode = 1 Then RadioUOff.Checked = True
            If RadioSMan5.Checked = True And RadioDman5.Checked = True And typeCode = 1 Then RadioUOff.Checked = True
            If RadioSMan6.Checked = True And RadioDMan6.Checked = True And typeCode = 1 Then RadioUOff.Checked = True

        End If
    End Sub


    Private Sub ButtonFindCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFindCode.Click
        ' TROUVE LE CODE A PARTIR DE L'ETAT DES RADIO BUTTONS
        Dim cplrCode As Integer = 0
        If RadioDPed.Checked = True Then cplrCode = 1000
        If RadioDMan1.Checked = True Then cplrCode = 1100
        If RadioDMan2.Checked = True Then cplrCode = 1200
        If RadioDMan3.Checked = True Then cplrCode = 1300
        If RadioDMan4.Checked = True Then cplrCode = 1400
        If RadioDman5.Checked = True Then cplrCode = 1500
        If RadioDMan6.Checked = True Then cplrCode = 1600
        '
        If RadioSPed.Checked = True Then cplrCode += 0
        If RadioSMan1.Checked = True Then cplrCode += 5
        If RadioSMan2.Checked = True Then cplrCode += 10
        If RadioSman3.Checked = True Then cplrCode += 15
        If RadioSMan4.Checked = True Then cplrCode += 20
        If RadioSMan5.Checked = True Then cplrCode += 25
        If RadioSMan6.Checked = True Then cplrCode += 30
        '
        If Radio16.Checked = True Then cplrCode += 0
        If Radio8.Checked = True Then cplrCode += 1
        If RadioUOff.Checked = True Then cplrCode += 1
        If Radio4.Checked = True Then cplrCode += 2
        If RadioBass.Checked = True Then cplrCode += 3
        If RadioMelody.Checked = True Then cplrCode += 4

        ' afficher
        TBoxCplrCode.Text = cplrCode.ToString
        TBoxCplrCode.Focus()


    End Sub
End Class