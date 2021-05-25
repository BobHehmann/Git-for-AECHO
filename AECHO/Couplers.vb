Imports System.Windows.Forms

Public Class Couplers

    Private Sub Txt_CplrCode_KeyDown(                       ' Standard Control event parms...
            sender As Object,
            e As KeyEventArgs
            ) Handles Txt_CplrCode.KeyDown                  ' VALIDATION AVEC LA TOUCHE ENTER

        ' Purpose:      Decode a Coupler-Code, setting the Radio Buttons to show the
        '               Code's functional interpretation. Inverse function to ButtonFindCode().
        ' Process:		Decode 3 sections by moduli -> Destination is hundreds, Source is mod 100,
        '               Type is mod 5. Unison off is 8' Type, where Dest=Source.
        ' Called By:    TBoxCplrCode KeyDown Event
        ' Side Effects: Updates form fields
        ' Notes:        <None>
        ' Updates:      <1.060.2> Modified Control names to standard. Added use of DispMsg for display
        '               of user messages. Validate input - only update buttons if code is valid.

        Const lclProcName As String = "TextBox1_KeyDown"

        Dim cplrCode As Integer                             ' Full code as entered on the form
        Dim destCode As Integer                             ' "Hundreds" is Destination Code
        Dim srcCode As Integer                              ' Source is 
        Dim typeCode As Integer

        If e.KeyCode = Keys.Enter Then                      ' Only process an "Enter" - ignore other typing in the field
            If Not Integer.TryParse(
                Trim(Txt_CplrCode.Text), cplrCode) Then     ' <1.060.2> Strip surrounding whitespace, check if is a valid number
                DispMsg("", conMsgInfo, "Coupler Codes must be an integer value.")
                Txt_CplrCode.Clear()                        ' <1.060.2> Erase entry and try again
                Return
            End If
            If Trim(Txt_CplrCode.Text).Length <> 4 Then     ' <1.060.2> Must be exactly 4 digits
                DispMsg("", conMsgInfo, "Coupler Codes must be exactly 4-digits long.")
                If Trim(Txt_CplrCode.Text).Length < 4 Then  ' <1.060.2> Too short, leave alone
                    Return
                End If
                Txt_CplrCode.Text =
                    Strings.Left(Txt_CplrCode.Text, 4)      ' <1.060.2> Too long, truncate from the right end
                Return
            End If

            cplrCode = Val(Txt_CplrCode.Text)               ' Full code as entered on the form
            destCode = Int(cplrCode / 100)                  ' "Hundreds" is Destination Code
            srcCode = cplrCode Mod 100                      ' Source is groups of 5, lowest 2 digits
            typeCode = cplrCode Mod 5                       ' Type is Code mod 5

            If (destCode < 10) Or (destCode > 16) Then      ' <1.060.2> Validate Destination range
                DispMsg("", conMsgInfo, "The leftmost 2 digits must be between 10 & 16 inclusive.")
                Txt_CplrCode.Clear()                        ' <1.060.2> Clear, and try again...
                Return
            End If
            If srcCode > 34 Then                            ' <1.060.2> Validate Source & Type range
                DispMsg("", conMsgInfo, "The rightmost 2 digits must be between 00 & 34 inclusive.")
                Txt_CplrCode.Clear()                        ' <1.060.2> Clear, and try again...
                Return
            End If

            Select Case srcCode                             ' Lower 2 digits, grouped by 5
                Case 0 To 4 : RadioSPed.Checked = True      ' xx00 - xx04
                Case 5 To 9 : RadioSMan1.Checked = True     ' xx05 - xx09
                Case 10 To 14 : RadioSMan2.Checked = True
                Case 15 To 19 : RadioSMan3.Checked = True
                Case 20 To 24 : RadioSMan4.Checked = True
                Case 29 To 29 : RadioSMan5.Checked = True
                Case 30 To 34 : RadioSMan6.Checked = True
            End Select

            Select Case destCode                            ' Hundreds digits
                Case 10 : RadioDPed.Checked = True          ' 10xx
                Case 11 : RadioDMan1.Checked = True         ' 11xx
                Case 12 : RadioDMan2.Checked = True
                Case 13 : RadioDMan3.Checked = True
                Case 14 : RadioDMan4.Checked = True
                Case 15 : RadioDMan5.Checked = True
                Case 16 : RadioDMan6.Checked = True
            End Select

            Select Case typeCode                            ' Mod 5 remainder
                Case 0 : Radio16.Checked = True             ' xxx0 or xxx5
                Case 1 : Radio8.Checked = True              ' xxx1 or xxx6
                Case 2 : Radio4.Checked = True
                Case 3 : RadioBass.Checked = True
                Case 4 : RadioMelody.Checked = True
            End Select

            ' cas de unisson off
            If (typeCode = 1) And
            (Int(srcCode / 5) = (destCode - 10)) Then       ' <1.060.2> If Src = Dest, then change from "8 ft." to "Unison-Off"
                RadioUOff.Checked = True
            End If

            e.SuppressKeyPress = True                       ' <1.060.2> Suppress "Enter" to avoid console "Ding" sound
        End If

    End Sub


    Private Sub Btn_FindCode_Click(                     ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles Btn_FindCode.Click                ' TROUVE LE CODE A PARTIR DE L'ETAT DES RADIO BUTTONS

        ' Purpose:      Display the encoding of the functional Radio-Buttons into an integer Coupler-
        '               Code. Inverse function to TextBox1().
        ' Process:		Sum the defined encodings for the Destination, Source, and Type. Validate consistancy
        '               of Uni-Off/8ft. choices with Source/Destination selections. Display code onscreen.
        ' Called By:    ButtonFindCode Click Event Event
        ' Side Effects: Updates form fields
        ' Notes:        <None>
        ' Updates:      <1.060.2> Ensure 1 button from each of the three classes is checked on form load; Place values
        '               of buttons in their Btn.Tag fields, eliminating long if/then series - instead, loop over each
        '               collection. Upon "Find", ensure that if Type=8' is checked, Source/Dest are different; likewise, if
        '               Type=Uni-Off is checked, Source and Dest must be the same.
        ' Button Tag Codes (contain the values that each Radio Button contributes to the Coupler Code):

        '   Destinations:   RadioDPed  = 1000   RadioDMan1 = 1100   RadioDMan2  = 1200
        '                   RadioDMan3 = 1300   RadioDMan4 = 1400   RadioDMan5  = 1500  RadioDMan6 = 1600

        '   Sources:        RadioSPed  =    0   RadioSMan1 =    5   RadioSMan2  =   10
        '                   RadioSMan3 =   15   RadioSMan4 =   20   RadioSMan5  =   25  RadioSMan6 =   30

        '   Types:          Radio16    =    0   Radio8     =    1   RadioUOff   =    1
        '                   Radio4     =    2   RadioBass  =    3   RadioMelody =    4


        Const lclProcName As String =
            "Btn_FindCode_Click"                        ' <1.060.2> Routine name for message handling

        Dim r As RadioButton                            ' <1.060.2> Collection context variable, to loop through controls
        Dim destVal As Integer = 0                      ' <1.060.2> 3 Integers, the contribution of each Panel to the Code
        Dim srcVal As Integer = 0
        Dim typeVal As Integer = 0

        For Each r In Pnl_Dest.Controls                 ' <1.060.2> Within each of the 3 Panels, find the .Tag value of the
            If r.Checked Then destVal = CInt(r.Tag)     ' <1.060.2> checked Radio Button.
        Next
        For Each r In Pnl_Source.Controls
            If r.Checked Then srcVal = CInt(r.Tag)
        Next
        For Each r In Pnl_Type.Controls
            If r.Checked Then typeVal = CInt(r.Tag)
        Next

        If RadioUOff.Checked AndAlso                    ' <1.060.2> If Unison-Off, ensure Source = Destination
            (srcVal * 20) <> (destVal - 1000) Then      ' <1.060.2> This bit of math allows comparison of Source & Destination
            DispMsg("", conMsgInfo,                     ' <1.060.2> Warn User
                    "For Unison-Off, Source and Destination must be the same.")
            Txt_CplrCode.Clear()                        ' <1.060.2> Clear any residual value in the Coupler Code display
            Return                                      ' <1.060.2> And dismiss the interaction
        End If

        If Radio8.Checked AndAlso                       ' <1.060.2>, Using similar logic to previous snippet, if coupling is
            (srcVal * 20) = (destVal - 1000) Then       ' <1.060.2> 8 ft./Unison, Source and Destination must be different
            DispMsg("", conMsgInfo,
                    " For 8 ft., Source and Destination must be different.")
            Txt_CplrCode.Clear()
            Return
        End If

        Txt_CplrCode.Text = srcVal + destVal + typeVal  ' afficher; Display the encoding (1.060.2> Sum all 3 contributions
        Txt_CplrCode.Focus()                            ' Position focus on the integer encoding field

    End Sub
End Class