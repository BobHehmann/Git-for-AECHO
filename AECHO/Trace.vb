Imports System.Windows.Forms

Public Class Trace

    Dim M_FirstChar As Integer                  ' Current position in print-stream, between page calls

    Friend Rtb_Trace As RTBPrint = New RTBPrint ' <1.060.3> Replace ListBox with this enhanced RTB, to support easy pretty-printing of the Trace
    Private Sub Btn_TraceSampleClick(           ' User clicked the "OK" Button
            sender As Object,                   ' Standard Event Parameters for a Control
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
            "Btn_TraceSampleClick"

        TraceSample(Val(Txt_SampleID.Text),
                    Rtb_Trace)                  ' Invoke the routine to retrieve data tags related to a specified Sample ID. <1.060.2> Pass itemID as parm to TraceSample

    End Sub
    Private Sub Follow_Load(                            ' Form to implement output of a SampleTrace
            sender As Object,                           ' Standard Event Parameters for a Control
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
        '               <1.060.8> Add Context Menu for Undo, Redo, Cut, Cupy, and Paste to Rtb_Trace

        Const lclPRocName As String =                   ' Routine's name for message handling
            "Follow_Load"

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
            .ContextMenuStrip = CM_SampleTrace          ' <1.060.8> Associate menu with this RTB
        End With
        Controls.Add(Me.Rtb_Trace)                      ' Create the display box
        Rtb_Trace.AutoWordSelection = False             ' Work-around for old RTB bug, RTB self-enable this property, over-riding Designer setting.

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
    Private Sub MenuTSPrintClick(                       ' Print command from the form's Menu
            sender As Object,                           ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles MenuTSPrint.Click

        ' Purpose:      Initialize Print Dialog, if User agrees, submit print job
        ' Process:      Display dialog, get answer, if Yes, submit print job
        ' Called By:    MenuTSPrint Click event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented for printing Sample Trace

        Const lclProcName As String =                   ' Routine's name for message handling
            "MenuTSPrintDRClick"

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
    Private Sub MenuTSClearClick(           ' Clear Traces command from the form's Menu
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles MenuTSClear.Click

        ' Purpose:      Clear the Trace display area, adds the header back
        ' Process:		Clear Method
        ' Called By:    MenuTSClear Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:		<1.060.3> First code.

        Const lclProcName As String =       ' Routine's name for message handling
            "MenuTSClearClick"

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
    Private Sub MenuTSExitClick(            ' Exit command from the form's Menu
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles MenuTSExit.Click

        ' Purpose:      Close the Trace form
        ' Process:		Close Method
        ' Called By:    MenuTSCExit Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:		<1.060.3> First code.

        Const lclProcName As String =       ' Routine's name for message handling
            "MenuTSExitClick"

        Me.Close()

    End Sub
    Private Sub PrintDocumentTracePrintPage(                ' Invoked 1 or more Times to process a Print Job
            sender As Object,                               ' Standard Event Parameters for a Control
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
            "PrintDocumentTracePrintPage"

        M_FirstChar =
            Rtb_Trace.FormatRange(False,                    ' Select Render mode
                                  e,                        ' Control to print
                                  M_FirstChar,              ' First char on this page
                                  Rtb_Trace.TextLength)     ' End of print content

        e.HasMorePages =
            (M_FirstChar < Rtb_Trace.TextLength)            ' check if there are more pages to print

    End Sub
    Private Sub PrintDocumentTraceBeginPrint() Handles PrintDocumentTrace.BeginPrint

        ' Purpose:      Initializer invoked as start of print job. Sets start to 1 char in control
        ' Process:      Set a shared variable that tracks the next char to print
        ' Called By:    PrintDocumentTrace BeginPRint Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented, for Sample Trace

        Const lclProcName As String =   ' Routine's name for message handling
            "PrintDocumentTraceBeginPrint"

        M_FirstChar = 0                 ' Start at first character

    End Sub
    Private Sub PrintDocumentTraceEndPrint() Handles PrintDocumentTrace.EndPrint

        ' Purpose:      Free graphics memory used by formatter, once print job is fully rendered
        ' Process:      Have an appropriate message sent
        ' Called By:    PrintDocumentTrace EndPrint Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented, for Sample Trace

        Const lclProcName As String =   ' Routine's name for message handling
            "PrintDocumentTraceEndPrint"

        Rtb_Trace.FormatRangeDone()     ' Free graphics memory now that we're done

    End Sub
    Private Sub CM_SampleTraceOpening(              ' When CM is opening, tweak available commands
            sender As Object,                       ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_SampleTrace.Opening

        ' Purpose:      Adjust availability of CM items based on apllicability: Cut/Copy
        '               require that text be selected; Paste requires usable content in clipboard
        ' Process:      Check the conditions, set the sub-menu items .enabled properties to match.
        '               The Open Event is invoked before the CM is displayed. For paste, accepts:
        '               Unicode Text; ANSI Text; RTF Text; Images
        ' Called By:    CM_SampleTrace Opening Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =                 ' Routine's name for message handling
            "CM_SampleTraceOpening"

        CM_TraceCopy.Enabled =                      ' There is a selection, enable Cut & Copy
            (Rtb_Trace.SelectionLength > 0)
        CM_TraceCut.Enabled = CM_TraceCopy.Enabled

        CM_TracePaste.Enabled =                     ' Accept Unicode, Ansi, RTF, and Image Data
            (Clipboard.ContainsText() Or
            Clipboard.ContainsText(TextDataFormat.Text) Or
            Clipboard.ContainsText(TextDataFormat.Rtf) Or
            Clipboard.ContainsImage())

        CM_TraceUndo.Enabled =                      ' Reflect ability to Undo
            Rtb_Trace.CanUndo

        CM_TraceRedo.Enabled =                      ' Reflect ability to Redo
                Rtb_Trace.CanRedo

    End Sub
    Private Sub CM_TraceCutClick(           ' Context-Menu Cut Command
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_TraceCut.Click

        ' Purpose:      Context-Menu Cut, from SampleTrace RTB
        ' Process:      If there is selected text, invoke the .Cut Method
        ' Called By:    CM_TraceCut Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =
            "CM_TraceCutClick"

        If Rtb_Trace.SelectionLength > 0 Then
            Rtb_Trace.Cut()
        End If

    End Sub
    Private Sub CM_TraceCopyClick(          ' Context-Menu Copy Command
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_TraceCopy.Click

        ' Purpose:      Context-Menu Copy, from SampleTrace RTB
        ' Process:      If there is selected text, invoke the .Copy Method
        ' Called By:    CM_TraceCopy Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =         ' Routine's name for message handling
            "CM_TraceCopyClick"

        If Rtb_Trace.SelectionLength > 0 Then
            Rtb_Trace.Copy()
        End If

    End Sub
    Private Sub CM_TracePasteClick(         ' Context-Menu Paste command
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_TracePaste.Click

        ' Purpose:      Context-Menu Paste, from SampleTrace RTB
        ' Process:      If there is valid pasteable content in Clipboard, invoke the .Paste Method
        ' Called By:    CM_TracePaste Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =         ' Routine's name for message handling
            "CM_TracePasteClick"

        If (Clipboard.ContainsText() Or     ' Accept Unicode, Ansi, RTF, and Image Data
        Clipboard.ContainsText(TextDataFormat.Text) Or
        Clipboard.ContainsText(TextDataFormat.Rtf) Or
        Clipboard.ContainsImage()) Then
            Rtb_Trace.Paste()
        End If

    End Sub
    Private Sub CM_TraceUndoClick(          ' Context-Menu Undo command
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_TraceUndo.Click

        ' Purpose:      Context-Menu Undo, from SampleTrace RTB
        ' Process:      If Undo is allowed, invoke .Undo Method
        ' Called By:    CM_TraceUndo Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =         ' Routine's name for message handling
            "CM_TraceUndoClick"

        If Rtb_Trace.CanUndo Then
            Rtb_Trace.Undo()
        End If

    End Sub
    Private Sub CM_TraceRedoClick(          ' Context-menu Redo command
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_TraceRedo.Click

        ' Purpose:      Context-Menu Redo, from SampleTrace RTB
        ' Process:      If Redo is allowed, invoke .Redo Method
        ' Called By:    CM_TraceRedo Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =         ' Routine's name for message handling
            "CM_TraceRedoClick"

        If Rtb_Trace.CanRedo Then
            Rtb_Trace.Redo()
        End If

    End Sub
End Class