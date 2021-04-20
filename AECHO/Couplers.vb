Imports System.Windows.Forms

Public Class Couplers

    Private Sub TextBox1_KeyDown(                           ' Standard Control event parms...
            sender As Object,
            e As KeyEventArgs
            ) Handles TBoxCplrCode.KeyDown                  ' VALIDATION AVEC LA TOUCHE ENTER

        ' Purpose:      Decode the entered Coupler-Code, and set the Radio Buttons to show the
        '               code's functional interpretation. Inverse function to ButtonFindCode().
        ' Process:		Decode 3 sections by moduli -> Destination is hundreds, Source is mod 100,
        '               Type is mod 5. Unison off is 8' Type, where Dest=Source.
        ' Called By:    TBoxCplrCode KeyDown Event
        ' Side Effects: Updates form fields
        ' Notes:        Validate integer input? Improve checking of Code, do not update Buttons
        '               with invalid data. Button initialization is suspect. Considering clearing
        '               all radio buttons on invalid code.

        Dim cplrCode As Integer = Val(TBoxCplrCode.Text)    ' Full code as entered on the form
        Dim destCode As Integer = Int(cplrCode / 100)       ' "Hundreds" is Destination Code
        Dim srcCode As Integer = cplrCode Mod 100           ' Source is 
        Dim typeCode As Integer = cplrCode Mod 5

        If e.KeyCode = Keys.Enter Then                      ' Only process an "Enter" - ignore other typing in the field
            Select Case destCode                            ' Hundreds digits
                Case 10 : RadioDPed.Checked = True          ' 10xx
                Case 11 : RadioDMan1.Checked = True         ' 11xx
                Case 12 : RadioDMan2.Checked = True
                Case 13 : RadioDMan3.Checked = True
                Case 14 : RadioDMan4.Checked = True
                Case 15 : RadioDman5.Checked = True
                Case 16 : RadioDMan6.Checked = True
                Case Else : MsgBox("Error ! This is not a valid coupler code")
            End Select

            Select Case srcCode                             ' Lower 2 digits, grouped by 5
                Case 0 To 4 : RadioSPed.Checked = True      ' xx00 - xx04
                Case 5 To 9 : RadioSMan1.Checked = True     ' xx05 - xx09
                Case 10 To 14 : RadioSMan2.Checked = True
                Case 15 To 19 : RadioSman3.Checked = True
                Case 20 To 24 : RadioSMan4.Checked = True
                Case 29 To 29 : RadioSMan5.Checked = True
                Case 30 To 34 : RadioSMan6.Checked = True
                Case Else : MsgBox("Error ! This is not a valid coupler code")
            End Select

            Select Case typeCode                            ' Mod 5 remainder
                Case 0 : Radio16.Checked = True             ' xxx0 or xxx5
                Case 1 : Radio8.Checked = True              ' xxx1 or xxx6
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


    Private Sub ButtonFindCode_Click(                           ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles ButtonFindCode.Click                      ' TROUVE LE CODE A PARTIR DE L'ETAT DES RADIO BUTTONS

        ' Purpose:      Display the encoding of the functional Radio-Buttons into an integer Couple-
        '               Code. Inverse function to TextBox1().
        ' Process:		Sum the defined encodings for the Destination, Source, and Type; display onscreen.
        ' Called By:    TButtonFindCode Click Event Event
        ' Side Effects: Updates form fields
        ' Notes:        Should validate: one button enabled per category - there are cases where a Radio Button
        '               group may have nothing checked, while other groups are checked - should be all or
        '               nothing before processing a Find?

        Dim cplrCode As Integer = 0                             ' Build the code - sum of the values of the 3 parts

        If RadioDPed.Checked = True Then cplrCode = 1000        ' Destination Codes = 100's; determine Destination and init running total
        If RadioDMan1.Checked = True Then cplrCode = 1100
        If RadioDMan2.Checked = True Then cplrCode = 1200
        If RadioDMan3.Checked = True Then cplrCode = 1300
        If RadioDMan4.Checked = True Then cplrCode = 1400
        If RadioDman5.Checked = True Then cplrCode = 1500
        If RadioDMan6.Checked = True Then cplrCode = 1600
        '
        If RadioSPed.Checked = True Then cplrCode += 0          ' Source Codes = Fives; determine Source and add to total
        If RadioSMan1.Checked = True Then cplrCode += 5
        If RadioSMan2.Checked = True Then cplrCode += 10
        If RadioSman3.Checked = True Then cplrCode += 15
        If RadioSMan4.Checked = True Then cplrCode += 20
        If RadioSMan5.Checked = True Then cplrCode += 25
        If RadioSMan6.Checked = True Then cplrCode += 30
        '
        If Radio16.Checked = True Then cplrCode += 0            ' Coupling Type = Ones (mod 5); determine Type and add to total
        If Radio8.Checked = True Then cplrCode += 1
        If RadioUOff.Checked = True Then cplrCode += 1
        If Radio4.Checked = True Then cplrCode += 2
        If RadioBass.Checked = True Then cplrCode += 3
        If RadioMelody.Checked = True Then cplrCode += 4


        TBoxCplrCode.Text = cplrCode.ToString                   ' afficher; Display the encoding
        TBoxCplrCode.Focus()                                    ' Position focus on the interger encoding field

    End Sub
End Class