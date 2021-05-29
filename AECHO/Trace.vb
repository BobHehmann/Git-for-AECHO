Imports System.Windows.Forms

Public Class Trace

    Dim M_FirstChar As Integer                  ' Current position in print-stream, between page calls

    Friend Rtb_Trace As RTBPrint = New RTBPrint ' <1.060.3> Replace ListBox with this enhanced RTB, to support easy pretty-printing of the Trace
    Private Sub Btn_TraceSample_Click(          ' User clicked the "OK" Button
            sender As Object,
            e As EventArgs
            ) Handles Btn_TraceSample.Click

        ' Purpose:      Dispatch TraceSample() to expand all possible Fields in a Sample Record,
        '               and follow linkages to other related Sections, also expanding all of their
        '               Fields.
        ' Process:		Call TraceSample() to do the work.
        ' Called By:    Btn_TraceSample Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:		<1.060.2> Moved all logic to routine TraceSample. Widened the Form, and the
        '               ListBox; added horizontal scrolling to the ListBox.

        Const lclProcName As String =           ' <1.060.2> Routine's name for message handling
            "Btn_TraceSample_Click"

        TraceSample(Val(Txt_SampleID.Text),
                    Rtb_Trace)                  ' Invoke the routine to retrieve data tags related to a specified Sample ID. <1.060.2> Pass itemID as parm to TraceSample

    End Sub
    Private Sub Follow_Load(                            ' Form to implement output of a SampleTrace
            sender As Object,
            e As EventArgs
            ) Handles MyBase.Load

        ' Purpose:      Init the form by adding the enhanced RTB as the target output area.
        ' Process:		This is done manually in code, as the VS Designer doesn't handle an
        '               extended control gracefully. Init and create the control onform. This
        '               new RTB replaces the ListBox of prior AECHO versions.
        ' Called By:    Follow Load Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:		<1.060.3> First code.

        Const lclPRocName As String = "Follow_Load"     ' Routine's name for message handling

        With Rtb_Trace                                  ' To support WYSIWYG printing, create an enhanced RTB
            .Font = New Font("Verdana", 10.0!,
                             FontStyle.Regular,
                             GraphicsUnit.Point)
            .Location = New Point(14, 70)               ' Locate it below the user controls
            .Margin = New Padding(4, 3, 4, 3)
            .Name = "Rtb_Trace"
            .Size = New Size(825, 484)
            .Text = ""
            .WordWrap = False
            .TabStop = False
            .ScrollBars = RichTextBoxScrollBars.Both
            .AcceptsTab = True
            .Visible = True
        End With
        Controls.Add(Me.Rtb_Trace)                      ' Create the display box

        AppendTxt(Rtb_Trace,                            ' Add the filename and title headers to the display
                   fnt_Fname,                           ' A reduced size to accomodate long strings
                   Color.Black,
                   G_OrganFile & vbCrLf & vbCrLf)
        AppendTxt(Rtb_Trace,
                   fnt_Title,                           ' An increased size, bolded, for emphasis
                   Color.Black,
                   "                    Sample Trace" & vbCrLf)

        MenuTSPrint.Enabled = False                     ' Nothing to print yet

    End Sub
    Private Sub MenuTSPrint_Click(                      ' Standard Event parameters
            sender As Object,
            e As EventArgs
            ) Handles MenuTSPrint.Click

        ' Purpose:      Initialize Print Dialog, if User agrees, submit print job
        ' Process:      Display dialog, get answer, if Yes, submit print job
        ' Called By:    MenuTSPrint Click event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented for printing Sample Trace

        Const lclProcName As String =                   ' Routine's name for message handling
            "MenuTSPrintDR_Click"

        Dim result As DialogResult

        '                                                 Set defaults to: Portrait;  <1/2-inch left, 
        '                                                 right, and bottom margins; 3/4-inch top margin
        PrintDocumentTrace.DocumentName = "AECHO Sample Trace"
        PrintDocumentTrace.DefaultPageSettings.Margins.Left = 40
        PrintDocumentTrace.DefaultPageSettings.Margins.Right = 40
        PrintDocumentTrace.DefaultPageSettings.Margins.Top = 75
        PrintDocumentTrace.DefaultPageSettings.Margins.Bottom = 40

        PrintDialogTrace.Document = PrintDocumentTrace  ' Bind Print Dialog to Print Object
        result = PrintDialogTrace.ShowDialog            ' User response

        If result = DialogResult.OK Then
            Try
                PrintDocumentTrace.Print()              ' Dispatch Print job
            Catch ex As Exception
                DispMsg(lclProcName, conMsgExcl,
                        "Unable to submit the print job. " & vbCrLf &
                        "Exception code is: " & ex.Message)
            End Try
        End If

    End Sub
    Private Sub MenuTSClear_Click(          ' Standard Event parameters
            sender As Object,
            e As EventArgs
            ) Handles MenuTSClear.Click

        ' Purpose:      Clear the Trace display area, adds the header back
        ' Process:		Clear Method
        ' Called By:    MenuTSClear Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:		<1.060.3> First code.

        Const lclProcName As String =
            "MenuTSClear_Click"             ' Routine's name for message handling

        Rtb_Trace.Clear()                   ' Clear box, then add filename as title header
        AppendTxt(Rtb_Trace,
                   fnt_Fname,               ' A reduced size to accomodate long strings
                   Color.Black,
                   G_OrganFile & vbCrLf & vbCrLf)
        AppendTxt(Rtb_Trace,
                   fnt_Title,               ' An increased size, bolded, for emphasis
                   Color.Black,
                   "                    Sample Trace" & vbCrLf)

        MenuTSPrint.Enabled = False         ' Nothing to print

    End Sub
    Private Sub MenuTSExit_Click(           ' Standard Event parameters
            sender As Object,
            e As EventArgs
            ) Handles MenuTSExit.Click

        ' Purpose:      Close the Trace form
        ' Process:		Close Method
        ' Called By:    MenuTSCExit Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:		<1.060.3> First code.

        Const lclProcName As String =
            "MenuTSExit_Click"              ' Routine's name for message handling

        Me.Close()

    End Sub
    Private Sub PrintDocumentTrace_PrintPage(               ' Standard Control event parms...
            sender As Object,
            e As Printing.PrintPageEventArgs
            ) Handles PrintDocumentTrace.PrintPage

        ' Purpose:      Handle each page in order as print job progresses
        ' Process:      Invoke formatter, set Property that controls subsequent page printing. Called once
        '               for each page, until it sets e.HasMorePages to False
        ' Called By:    PrintDocumentTrace PrintPage Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented for Sample Trace

        Const lclProcName As String =                       ' Routine's name for message handling
            "PrintDocumentTrace_PrintPage"

        M_FirstChar =
            Rtb_Trace.FormatRange(False,                    ' Select Render mode
                                  e,                        ' Control to print
                                  M_FirstChar,              ' First char on this page
                                  Rtb_Trace.TextLength)     ' End of print content

        If (M_FirstChar < Rtb_Trace.TextLength) Then        ' check if there are more pages to print
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub
    Private Sub PrintDocumentTrace_BeginPrint() Handles PrintDocumentTrace.BeginPrint

        ' Purpose:      Initializer invoked as start of print job. Sets start to 1 char in control
        ' Process:      Set a shared variable that tracks the next char to print
        ' Called By:    PrintDocumentTrace BeginPRint Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented, for Sample Trace

        Const lclProcName As String =   ' Routine's name for message handling
            "PrintDocumentTrace_BeginPrint"

        M_FirstChar = 0                 ' Start at first character

    End Sub
    Private Sub PrintDocumentTrace_EndPrint() Handles PrintDocumentTrace.EndPrint

        ' Purpose:      Free graphics memory used by formatter, once print job is fully rendered
        ' Process:      Have an appropriate message sent
        ' Called By:    PrintDocumentTrace EndPrint Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented, for Sample Trace
        Const lclProcName As String =   ' Routine's name for message handling
            "PrintDocumentTrace_EndPrint"

        Rtb_Trace.FormatRangeDone()     ' Free graphics memory now that we're done

    End Sub

End Class