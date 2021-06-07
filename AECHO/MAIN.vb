
Imports System.IO
Imports System.Windows.Forms
Imports System.Math

Public Class MAIN

    ' Version Log Summary (see ChangeLog.txt file for more detail)

    '   1.058.2 Baseline Vesion
    '   Git:        NA
    '   Summary:    Code as initially received from Jean-Paul V.

    '   1.059.0     April, 07-2021   Bob Hehmann
    '   Git:        master
    '   Summary:    Minor spelling/text changes; added UTF-8 compatability; About Box; Loaded into Git; .NET from 3.5 To 4.8

    '   1.060.0     April 18, 2021  Bob Hehmann
    '   Git:        cleanup-InitialPass-Syntax-Comments
    '   Summary:    Improve in-code documentation; added \DATA to Git management; remove IDE Warnings; formatting

    '   1.060.1     April 18, 2021 Bob Hehmann
    '   Git:        NET5-Upgrade
    '   Summary:    Upgrade to .Net 5.0; Added Windows Installer Project; Modified \DATA locator to support  both installed
    '               and unintstalled variants.

    '   1.060.2     April 20, 2021 Bob Hehmann
    '   Git:        Code-Cleanup2
    '   Summary:    Deeper clean, remove many side-effect; bug fixes; standardized naming conventions; no-wrap ODF display
    '               Much faster; enhanced menus. Moving non-event handler routines from here to SharedCode.vb.

    '   1.060.3     March 23, 2021 Bob Hehmann
    '   Git:        Printing
    '   Summary:    More robust, formatted printing of Descriptive Text RTB; formatted printing of Sample Traces

    '   1.060.4     June 1, 2021 Bob Hehmann
    '   Git:        Printing
    '   Summary:    Added ODF current Line & Char counts to the Status-Bar. Completed almost all HTML Help.

    '   1.060.5     June 2, 2021 Bob Hehmann
    '   Git:        ImageDisplay
    '   Summary:    Display Images in their own form/window, remove resizing of controls to make room for image display.

    '   1.060.6     June 2, 2021 Bob Hehmann
    '   Git:        PositionsAsLines
    '   Summary:    Modify Section Start/End & Row Star/End fields to display both Line and Char Positions.

    '   1.060.7     June 3, 2021 Bob Hehmann
    '   Git:        DynamicSecMenus
    '   Summary:    Replace static Section Navigation Menus with Menus created from ODF Content - including via Recompute

    '   1.060.8     June 4, 2021 Bob Hehmann
    '   Git:        ContextMenus
    '   Summary:    Add Context (right-click) Menus to Sample-Trace RTB, ODF Area, XML Row Area, Search Text, and Descriptive
    '               Text Area

    '   1.060.9     June 6, 2021 Bob Hehmann
    '   Git:        ArrowKeys
    '   Summary:    Treat Up, Down, Left, and Right arrow keys as single-mouse clicks at new Cursor position, updating locatio info.

    Dim M_FoundStart As Integer = -1    ' <1.060.2> When a text-search succeeds, this becomes index of start of located text
    Dim M_FoundEnd As Integer = 0       ' <1.060.2> Defines the end of located text; when 0, there is no located text defined.
    Dim M_FirstChar As Integer          ' <1.060.2> Current position in print-stream, between page calls
    Dim Rtb_DText As RTBPrint =         ' <1.060.2> Hidden "enhanced" RTB for formatted printing of the Display Text Area
        New RTBPrint

    ' MAIN FORM LOAD
    Private Sub MAIN_Load(                                      ' AECHO's base Form
            sender As Object,
            e As EventArgs
            ) Handles Me.Load

        ' Purpose:      Initialize the application: application registered check, prep default paths to AppData & ODF,
        '               update Window Title Bar, ensure the tags display is cleared and Edit Mode is off.
        ' Process:      Contruct file path (directory path) variables, call initialization routines.
        ' Called By:    Window's Event Handler, MAIN form Load Event
        ' Side Effects: Initializes G_AppPath, G_DataPath, G_ODFLibPath, G_SectionName, G_InitTagPanelWidth,
        '               G_MinPanelHeigths, G_EditMode; updates MAIN form
        ' Notes:        <None>
        ' Updates:      <1.060.1> Added logic to find \DATA in either the legacy location (next to AECHO.EXE), or in the user's
        '               AppData\Romaing\Aecho\... directory (if AECHO is installed as an application).
        '               <1.060.2> Altered init to improve flow, begin removing "Side Effects" in subroutine logic, prepared
        '               for dynamically adjustable form-sizing by calculating control positions, centering title-text in code...
        '               In Demo Mode, also hide Title for Control to change ODF Font Size, not just the Control itself. Eliminate
        '               global G_AppPath.
        '               <1.060.3> Create hidden instance (Rtb_DText) of printing-enhanced RTB, placed behind the Tags panel. Work around
        '               15 year-old RTB bug re AutoWordSelection.

        Const lclProcName As String =                           ' <1.060.2> Routine's name for message handling
            "MAIN_Load"

        Const conDATADir As String = "DATA"                     ' <1.060.2> Path extension of application's data directory
        Const conInitialDir As String = "initialdir.txt"        ' <1.060.2> Filename of (optional) text file that contains a user-supplied DefODFPath
        Const conLicenseFileName As String = "_verpeaux.txt"    ' <1.060.2> Filename for (deprecated) license file

        '   Locate the Application Executable, Data, and ODF Directories

        G_DataPath = GetDataPath(Application.StartupPath,       ' <1.060.2> Introduced new function to determine this path, reduce excessive logic nesting
                                 conDATADir)
        G_HelpFilePath = Path.Combine(G_DataPath,               ' <1.060.2> Construct offset to HTMl Help directory with \DATA
                                      conHTMLHelpDir)
        If Not Directory.Exists(G_HelpFilePath) Then            ' <1.060.2> If HTML Directory doesn't exist, warn user and fallback to classic Help
            DispMsg(lclProcName, conMsgExcl,
                    "Unable to locate HTML Help Directory:" & vbCrLf &
                    G_HelpFilePath & vbCrLf &
                    "Will fallback to simplified Help displayed in the Descriptive Text Area.")
            G_HelpFilePath = ""                                 ' <1.060.2> Help loader will check this: when null, use old-style help
        End If

        G_ODFLibPath = GetODFLibPath(G_DataPath, conInitialDir) ' <1.060.2> Modified to function call: Establishes G_ODFLibPath as (possible) location of HW's ODF files
        G_Registered = IsRegistered(G_DataPath,
                                    conLicenseFileName)         ' Savoir si version enregistre ou non; detect if this version is registered or not - as of 1.057, always "Registered"
        If G_Registered = False Then                            ' Restriction si unRegistered; <1.060.2> Moved disabling-code here from IsRegistered()
            Menu_FollowASample.Visible = False                  ' Enforce restrictions if unregistered - No Follow A Sample
            Num_ODFFontSize.Visible = False                     ' Cannot alter ODF Presentation Font Size, hide the Control
            Lbl_FontSize.Visible = False                        ' <1.060.2> Also hide the Font-Size Control's title/label
            Btn_Led.Visible = False                             ' Status Light/Quick Recompute unavailable
        End If

        ' Initialize the main form

        Lbl_SectionName.Text = conDefSectionName                ' <1.060.2> Default, until we establish position in a real Section
        CenterLbl(Lbl_ODFTitle, Rtb_ODF.Left,
                   Rtb_ODF.Right)                               ' <1.060.2> Center the ODF-Title between the SectionName label and FontSize control
        G_InitTagPanelWidth = Pnl_Tags.Width                    ' <1.060.2> Save the Panel's initial width at startup, to restore when resizing after image display
        G_MinPanelHeights = Pnl_Tags.Height                     ' <1.060.2> Remember the initial height, which will also be the minimum when we allow dynamic resizing

        ResetToNoODF()                                          ' <1.060.2> Initalize state when there is no ODF file loaded - general init routine

        With Rtb_DText                                          ' <1.060.3> To support WYSIWYG printing, create hidden instance of enhanced RTB
            .Font = New Font("Verdana", 10.0!, FontStyle.Regular, GraphicsUnit.Point)
            .Location = New Point(50, 451)                      ' <1.060.3> Behind the Tags Panel
            .Margin = New Padding(4, 3, 4, 3)                   ' Same parameters as Rtb_DescText, so layout will be the same
            .Name = "Rtb_DText"
            .Size = New Size(750, 380)
            .Text = ""
            .WordWrap = False
            .TabStop = False
            .ScrollBars = RichTextBoxScrollBars.Both
            .AcceptsTab = True
            .Visible = False                                    ' <1.060.3> Keep it hidden, it is just an internal scratch-pad, not to be seen onscreen
        End With
        Controls.Add(Me.Rtb_DText)                              ' <1.060.3> Create it
        Rtb_XMLRow.AutoWordSelection = False                    ' <1.060.3> Work-around to RTB bug, where control enables AutoWordSelection upon loading
        Rtb_ODF.AutoWordSelection = False
        Rtb_DescText.AutoWordSelection = False

        fnt_Fname = New Font(conDescFont,                       ' <1.060.3> Init useful fonts: slightly reduced size, these path/filename strings can be very long
                      conDefODFFontSize - 1,
                      FontStyle.Regular)
        fnt_Title = New Font(conDescFont,                       ' <1.060.3> Increased size and bold
                             conDefODFFontSize + 1,
                             FontStyle.Bold)
        fnt_Fields = New Font(conDescFont,                      ' <1.060.3> Standard vanilla
                              conDefODFFontSize,
                              FontStyle.Regular)
        fnt_Section = New Font(conDescFont,                     ' <1.060.3> Standard size, but bold
                                     conDefODFFontSize,
                                     FontStyle.Bold)


    End Sub

    ' FONCTIONS RICH TEXT BOX; Useful Functions for the Rich Text Boxes, ODF, XML Rows...
    ' CONTROLES
    Private Sub Num_ODFFontSize_ValueChanged(                   ' Changed the ODF Font-size Control
            sender As Object,                                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Num_ODFFontSize.ValueChanged

        ' Purpose:      Set Rtb_ODF's (ODF display) font-size to match the new value in the font-size control
        ' Process:      Call EnumerateSectionsSetFont() to do the work, specifying the ODF (with Titles)
        ' Called By:    Num_ODFFontSize ValueChanged Event
        ' Side Effects: Cause update to Rtb_ODF display
        ' Notes:        <None>
        ' Updates:      <1.060.2> Moved main logic to a common routine. No-opped noise invocations triggered before
        '               MAIN form begins loading, which can cause exception events. Calls EnumerateSectionsSetFont()
        '               to change entire ODF, then scan for Section Titles and emphasize them.

        Const lclProcName As String =                           ' <1.060.2> Routine's name for message handling
            "Num_ODFFontSize_ValueChanged"

        If G_NoODF Then Return                                  ' <1.060.2> This event triggers twice before MAIN form load procedure gets control!
        EnumerateSectionsSetFont(Rtb_ODF,                       ' <1.060.2> Moved code to EnumerateSectionsSetFont(), forcing a Title size/emphasis reapplication
                                 Num_ODFFontSize.Value,         ' <1.060.2> Font-Size to use, taken from the Control
                                 G_NoODF,                       ' <1.060.2> Have an ODF, this is False
                                 False,                         ' <1.060.2> Do not enumerate the Sections in the Descriptive Text Area
                                 True)                          ' <1.060.2> Re-emphasize Titles after setting overall ODF Font size (size change resets all other attributes)

    End Sub
    Private Sub Rtb_ODF_MouseDoubleClick(                           ' Double Mouse-click in the ODF
            sender As Object,                                       ' Standard Event Parameters for a Control
            e As MouseEventArgs
            ) Handles Rtb_ODF.MouseDoubleClick

        ' Purpose:      Select a complete (XML) Row from the ODF, copy it to Rtb_XMLRow, highlight the Row in the ODF
        ' Process:      Position Cursor to the point double-clicked in Rtb_ODF & update Cursor Position field to match.
        '               Locate the ODF Section the Cursor is in. Extract and display the current Row in Rtb_XMLRow, and
        '               the number of Child Elements in this row (not including bounding '<o>...</o>'. Select the
        '               Cursor's line in Rtb_ODF. Retrieve & display the Section's description from the Section's
        '               associated .rtf file.
        ' Called By:    Rtb_ODF MouseDoubleClick Event
        ' Side Effects: Alters globals: CaretPos, LineStart, LineText. Updates Rtb_ODF, Rtb_XMLRow, Rtb_DescText, various
        '               display fields on Main Form.
        ' Notes:        Unwind nested updates to Global Variables, replace with ByRef/Functions where that would reduce
        '               use of side-effects. Update Section Begin/End fields on the MAIN form to reflect movement to a
        '               new Section (fields not presently updated, probably a bug.)
        ' Updates:      <1.060.2> Converted to use standard message handling. Eliminated global G_CaretPos, subbed local
        '               cursorPos. Eliminated G_RowText, take Row Text from Function value of MoveToPosition().
        '               <1.060.6> Save Section Start/End positions in the .Tag properties, for later use when repositioning.

        Const lclProcName As String =                               ' <1.060.2> Routine's name for message handling
            " Rtb_ODF_MouseDoubleClick"

        Dim cursorPos As Integer                                    ' <1.060.2> Local var for current Cursor Position, replaced G_CaretPos
        Dim secStart As Integer
        Dim secEnd As Integer
        Dim secLStart As Integer                                    '<1.060.6> Starting and Ending Line Nums, already one-based
        Dim secLEnd As Integer
        Dim lineStart As Integer

        If Rtb_ODF.TextLength = 0 Then                              ' ODF is empty, no text, issue informational message
            DispMsg("", conMsgInfo,
                    "There is presently no ODF text to select.")
            Return                                                  ' MODIF V058
        End If

        cursorPos = Rtb_ODF.GetCharIndexFromPosition(e.Location)    ' TROUVER L'INDEX SELON POSITION SOURIS; cursorPos is now the position of the double-click
        Rtb_XMLRow.Text = MoveToPosition(cursorPos,                 ' Trouver et selectionner la ligne; select the Row containing the cursor, and return its content
                                         G_LineIndex,               ' <1.060.2> Returns Line Number, 0-based
                                         lineStart,                 ' <1.060.2> Returns start of the Line
                                         True,                      ' <1.060.2> Update the cursor position
                                         True)                      ' <1.060.2> Select expanded text of entire Row
        ' MODIF VERSION 025
        G_SectionName = GetSectionFromIndex(lineStart,
                                            secStart,
                                            secEnd,
                                            secLStart,
                                            secLEnd)                ' retrouver la section auquel il appartient; update the current Section Name, and display it onscreen
        Lbl_SectionName.Text = G_SectionName                        ' <1.060.2> Screen update no longer done by GetSectionFromIndex
        Lbl_SecStartVal.Text = secLStart.ToString(conIntFmt) &      ' <1.060.6> Display Line # / Char Pos
            " / " & secStart.ToString(conIntFmt)
        Lbl_SecStartVal.Tag = secStart                              ' <1.060.6> Save raw position in .Tag property, for later repositioning
        Lbl_SecEndVal.Text = secLEnd.ToString(conIntFmt) &
            " / " & secEnd.ToString(conIntFmt)
        Lbl_SecEndVal.Tag = secEnd
        DisplayXMLRow()                                             ' montrer les infos de la ligne; update screen fields; <1.059.0> Removed parameter (1), DisplayXMLRow is now parameterless
        G_PreviousRTFFile = LoadRTFFile(G_DataPath,                 ' charger le fichier RTF; retrieve and display the .rtf file that descibes this Section
                                      G_SectionName,
                                      G_PreviousRTFFile)

    End Sub
    Private Sub Rtb_ODF_MouseClick(     ' Single Mouse-click in the ODF
            sender As Object,           ' Standard Event Parameters for a Control
            e As MouseEventArgs
            ) Handles Rtb_ODF.MouseClick

        ' TROUVER L'INDEX SELON POSITION SOURIS
        ' modif version 055

        ' Purpose:      Move the curosr to the click-point, and update the Data Panel (Cursor Position, Line Number,
        '               Line Start, Line End, Row Start, and Row End), but do not: change the Section;
        '               update Section Data; extract Row content; parse Row Tags; nor update Descriptive Text.
        ' Process:      Ignore if there is no ODF loaded. Otherwise, call MoveToPosition to update
        '               to the Cursor position, telling it not to extend/highlight the entire Row.
        ' Side Effects: Update the global G_LineIndex (the current Line).
        ' Notes:        <None>
        ' Updates:      <1.060.2> Replaced use of global G_CaretPos with local cursorPos. Added parm to MoveToPosition()
        '               call, to tell it not to extend the selection to the entire Row, rather leave it as user set it.
        '               Removed code to reduce selection to a single caret position when in Edit-Mode.

        Const lclProcName As String =   ' <1.060.2> Routine's name for message handling
            "Rtb_ODF_MouseClick"

        Dim cursorPos As Integer        ' <1.060.2> Local var to replace use of G_CaretPos global
        Dim lineStart As Integer

        If Rtb_ODF.TextLength = 0 Then  ' ODF is empty, no text, issue informational message
            Return                      ' MODIF V058
        End If

        cursorPos =                     ' Extract the Cursor position from Rtb_ODF
            Rtb_ODF.GetCharIndexFromPosition(e.Location)
        MoveToPosition(cursorPos,       ' Trouver et selectionner la ligne; Select (highlight) the text of the Row in the ODF
                       G_LineIndex,     ' <1.060.2> Returns Line Number, 0-based
                       lineStart,       ' <1.060.2> Returns index to beginning of Line
                       False,           ' <1.060.2> Do not further position the cursor, this would erase a selection
                       False)           ' <1.060.2> False -> Do not extend selection on screen, leave as user set it

    End Sub

    'FONCTIONS ET PROCEDURES
    Private Sub Btn_SaveDescTextClick(                  ' Clicked Save Desc. Button
            sender As Object,                           ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Btn_SaveDescText.Click

        ' activé par Btn_SaveDescText_Click

        ' Purpose:      Save the contents of the Descriptive-Text Area back to its original source file. This allows
        '               for editing of the text.
        ' Process:		Disallow altering the help-text file. Otherwise, save the text back to the file for the
        '               current Section.
        ' Called By:    Btn_SaveDescText Click Event
        ' Side Effects: Rewrites files in the \DATA directory
        ' Notes:        <None>
        ' Updates:      <1.060.2> Added common message handling. Removed references to global G_RTFFile, relying instead
        '               on G_PreviousRTFFile to contain file path/name of the loaded & displayed .rtf file. Added
        '               exception handler.

        Const lclProcName As String =
            "Btn_SaveDescTextClick"                     ' <1.060.2> Routine's name for message handling

        Dim pathToRTFFile As String                     ' <1.060.2> Var for constructing path/filename

        Try
            If G_PreviousRTFFile =
            Path.Combine(G_DataPath, "help.rtf") Then   ' Disallow altering the Help file. <1.060.2> Modified to check PreviousRTFFile variable
                DispMsg("", conMsgInfo,
                        "Rewriting the Help File is not permitted.")
                'Rtb_DescText.SaveFile(G_PreviousRTFFile)   ' temp - uncomment when need to write to the Help file, such as initial setup or major editing.
                Exit Sub
            End If

            If G_SectionName <> "" Then                 ' Only save if Section Name is not blank
                pathToRTFFile =
                        Path.Combine(G_DataPath, (G_SectionName & ".rtf"))
                Rtb_DescText.SaveFile(pathToRTFFile)    ' Save Control's contents.
            End If
        Catch ex As ArgumentException                   ' Couldn't rewrite the requested data
            DispMsg(lclProcName, conMsgCrit,
                    "Unable to rewrite a Descriptive Text File" & vbCrLf &
                    "Section requested was: " & G_SectionName & vbCrLf &
                    "Exception Code is: " & ex.Message)
        End Try

    End Sub

    ' MENU FILES
    Private Sub Menu_OpenHauptwerkOrganClick(                               ' menu > File > Open Hauptwerk Organ...
            sender As Object,                                               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_OpenHauptwerkOrgan.Click

        ' Purpose:      Choose and load an ODF File into Rtb_ODF, and buid the S_Section array to assist in navigation.
        '               If the open in cancelled, AECHO's state remains unchanged. Note that if the path specified by
        '               G_ODFLibPath does not exist, then Windows will default to the last-known succesful path,
        '               stored in a Registry Key.
        ' Process:		Use the standard dialog to select the file to open. Check its length, enforcing a
        '               1MB limit if unregistered (all AECHO's are registered as of 1.057.0). Enable the menus
        '               and clear the Child Elements panel. Open and read the file into memory
        '               (Rtb_ODF). Parse the Sections, building the S_Section array, and updaing the screen.
        ' Called By:    Menu_OpenHauptwerkOrgan Click Event
        ' Side Effects: Updates various globals, loads file into Rtb_ODF.
        ' Notes:        Change max file length allowed, Title-bar text to Constants.
        '               used in Properties. Make file-open more robust, fielding exceptions. Only process the
        '               other functions if the file is loaded with error.
        ' Updates:      <1.059.0> Modified loading of ODF data to correctly decode UTF8
        '               <1.060.2> Added exception handler, reordered logic to reset things if ODF fails to load, only enabling
        '               after the load completes. Replaced parsing routines, calling EnumerateSectionsSetFont() to both
        '               list the Sections in the Descriptive Text Area, and set the initial Font and Section Title emphasis.

        Const lclProcName As String = "Menu_OpenHauptwerkOrganClick"        ' <1.060.2> Routine's name for message handling
        Const conDemoMaxODFBytes As Integer = 1024000                       ' <1.060.2> Max allowable ODF file size when in Demo mode

        Dim sizeInBytes As Integer                                          ' Length of ODF file that is to be opened
        Dim file As FileInfo                                                ' FileInfo object used to determine file length

        CheckUnloadODF(G_OrganFile, G_ODFModSinceSaved)                     ' <1.060.2> Ask about saving a changed ODF before overwriting it with a new one?

        OpenFileDialog.RestoreDirectory = True                              ' Use Windows standard open-file: restore to initial directory when done
        OpenFileDialog.InitialDirectory = G_ODFLibPath                      ' "C:\HAUPTWERK\HauptwerkSampleSetsAndComponents\OrganDefinitions" is default
        OpenFileDialog.Title = "Open an organ"
        OpenFileDialog.DefaultExt = "Organ_Hauptwerk_xml"
        OpenFileDialog.Filter =
            "organ files (*.Organ_Hauptwerk_xml)|*.Organ_Hauptwerk_xml"     ' <1.059.0> added the closing ) from 1.058b
        OpenFileDialog.FileName = ""                                        ' MODIF V058

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then               ' <1.059.0> extraneous System.Windows.Forms reference removed; User selected a file

            G_OrganFile = OpenFileDialog.FileName                           ' Save the name of the file
            OpenFileDialog.Dispose()                                        ' Clean up

            file = New FileInfo(G_OrganFile)                                ' tester longueur du fichier; test if file is longer than allowed for unregistered AECHO
            sizeInBytes = file.Length                                       ' As of 1.057.0, AECHO is freeware, so file length will not matter
            ' DispMsg(lclProcName, MsgBoxStyle.Information,
            '        "ODF File size: " &
            '       sizeInBytes.ToString("N0") & " bytes long.")             ' <1.060.2> Debug
            If (Not G_Registered) And (sizeInBytes > conDemoMaxODFBytes) Then
                DispMsg("", conMsgInfo,
                        "DEMO VERSION " & vbCrLf &                          ' Max allowable when not regsistered is 1MB
                        "Only an ODF smaller than " &
                        conDemoMaxODFBytes.ToString("N0") &
                        " bytes can be opened with this version." & vbCrLf &
                        "Your file is: " &
                         sizeInBytes.ToString("N0") & " bytes long.")
                Exit Sub
            End If

            ResetToNoODF()                                                  ' <1.060.2> Initialize, no data, most menus off,...

            Try                                                             ' <1.060.2> Add Try/Catch in case we can't read the ODF file
                Rtb_ODF.Text = My.Computer.FileSystem.ReadAllText(          ' Copier dans la RichTextBox
                G_OrganFile, System.Text.Encoding.UTF8)                     ' <1.059.0> Load Rtb_ODF, reading ODF file forcing UTF-8 decoding
            Catch ex As Exception                                           ' <1.060.2> If error, display message, clear box, reset Menus, exit
                DispMsg(lclProcName, conMsgCrit,
                    "General Exception while attempting to read ODF File" & vbCrLf &
                    "Exception Code is: " & vbCrLf & ex.ToString)
                Return
            End Try
            ' <1.060.2> ODF now exists in memory
            G_NoODF = False
            Text = conMainTitle & My.Application.Info.Version.ToString &
                ", Current ODF is " & G_OrganFile                           ' Update the Windows Title Bar. <1.059.0> changed text to "Compiled" <1.060.2> Setup for "Close file" in Menu Bar

            Menu_CloseODF.Enabled = True                                    ' <1.060.2> New menu choice, close the current ODF (with option to save if content changed)
            Menu_SaveAs.Enabled = True                                      ' <1.060.2> File is loaded, so now we can save it
            Menu_EditMode.Enabled = True
            Menu_EditModeStart.Checked = False                              ' <1.060.2> Ensure "ODF Editing Enabled" is unchecked and enabled
            Menu_EditModeStart.Enabled = True
            Menu_EditModeExit.Checked = True                                ' <1.060.2> And set "ODF Editing Disabled" to checked but disabled
            Menu_EditModeExit.Enabled = False
            Menu_EditMode.BackColor =
                Color.Gainsboro                                             ' Reset Main Menu text background color
            Menu_Tools.Enabled = True
            Menu_FollowASample.Enabled = True
            Status_LinesVal.Text = Rtb_ODF.Lines.Count().ToString(conIntFmt)
            Status_CharsVal.Text = Rtb_ODF.TextLength().ToString(conIntFmt) ' <1.060.4> Display initial length in Lines and Characters

            SetODFButtons(True)                                             ' <1.060.2> Enable Controls that required ODF Text e.g. Search, Next/Prev, Markers...

            EnumerateSectionsSetFont(Rtb_ODF,                               ' <1.060.2> List all Sections, displaying resulting data in the Descriptive Text Area
                                     Num_ODFFontSize.Value,
                                     G_NoODF,
                                     True,                                  ' <1.060.2> True -> display Section parsing results
                                     True)
            PositionToSectionByName("DisplayPage", False)                   ' <1.060.2> Position to the initial Section; False -> Don't display/parse a row, do not alter Descriptive Box

            Lbl_LineNumVal.Enabled = True                                   ' <1.060.2.> Now that ODF is loaded and these fields are updated, enable them to permit hot-linking
            Lbl_SecStartVal.Enabled = True
            Lbl_SecEndVal.Enabled = True
            Lbl_LineStartVal.Enabled = True
            Lbl_LineEndVal.Enabled = True
            Lbl_RowStartVal.Enabled = True
            Lbl_RowEndVal.Enabled = True
            Lbl_CursorPosVal.Enabled = True

            G_ODFModSinceSaved = False                                      ' <1.060.2> Introduced to track if ODF is modified, and we should prompt to save it before discarding.
            Status_FileDirtyVal.BackColor = Color.LightGreen                ' <1.060.2> Clean file loaded, update Status-Bar
            G_PackagePath = GetPackagePath(G_OrganFile)                     ' trouver le path pour OrganInstallationPackages; Location of Packages: sounds, image files...
        End If

    End Sub
    Private Sub Menu_SaveAsClick(           ' menu > File > Save As...
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_SaveAs.Click

        ' SAUVE L'ODF MODIFIE

        ' Purpose:      Save the internal ODF to a designated Organ File.
        ' Process:		Call SaveODFAs() to do the hevy lifting
        ' Called By:    Menu_SaveAs Click Event
        ' Side Effects: Updates G_ODFModSinceSaved
        ' Notes:        <None>
        ' Updates:      <1.060.2> Moved code content to SaveODFAs(), for reuse from several callpoints
        '               If SaveODFAs() returns True, the file was safely written, so clear the file-dirty bit.

        Const lclProcName As String =       ' <1.060.2> Routine's name for message handling
            "Menu_SaveAsClick"

        If SaveODFAs() Then                 ' <1.060.2> If save did not complete, leave G_ODFModSinceSaved as it was, whatever its state.
            G_ODFModSinceSaved = False      ' <1.060.2> Only reset the dirty-bit if we actually saved it to a file. Also update Status-Bar
            Status_FileDirtyVal.BackColor = Color.LightGreen
        End If

    End Sub
    Private Sub Menu_CloseODFClick(         ' menu > File > Close ODF...
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_CloseODF.Click

        ' Purpose:      Close the open ODF. If modified since load or last save, offer to save as
        '               a file first.
        ' Process:		Call SaveODFAs() to do the hevy lifting
        ' Called By:    Menu_CloseODF Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> New

        Const lclProcName As String =       ' <1.060.2> Routine's name for message handling
            "Menu_CloseODFClick"

        CheckUnloadODF(G_OrganFile,         ' If there is a dirty current ODF, offer to save it
                       G_ODFModSinceSaved)
        ResetToNoODF()                      ' Clears data areas, resets menus and globals...

    End Sub
    Private Sub Menu_QuitClick(             ' menu > File > Exit
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs) Handles Menu_Quit.Click

        ' Purpose:      Terminate AECHO
        ' Process:		You can figure this one out by yourself...
        ' Called By:    Menu_Quit Click Event
        ' Side Effects: Exits AECHO; Rewrite ODF File
        ' Notes:        <None>
        ' Updates:      <1.060.2> If ODF is loaded and has been modified, prompt user to save before
        '               exiting.

        Const lclProcName As String =       ' <1.060.2> Routine's name for message handling
            "Menu_QuitClick"

        CheckUnloadODF(G_OrganFile,
                       G_ODFModSinceSaved)  ' <1.060.2> Check if we have a dirty ODF - if so, offer to save it

        End                                 ' Leave AECHO

    End Sub

    ' MENU SECTIONS
    Private Sub Menu_SectionChoiceClick(            ' Any menu > Sections > sub-menu choice
            sender As ToolStripMenuItem,            ' Standard Event Parameters for a Control
            e As EventArgs)                         ' <1.060.7> Event Handlers added dynamically as Sections are located

        ' Purpose:      Process Section Menu choices, to navigate directly to Sections in the ODF.
        ' Process:		When building the Sections Menu, Sections are linked dynamically to this handler. The .Tags property of
        '               each Section entry has the exact name of the Section: pass this to PositionToSectionByName().
        ' Called By:    Each of the Section Menu choices' Click Events...
        ' Side Effects: <NA>
        ' Notes:        <None>
        ' Updates:      <1.060.2> Eliminated 43 distinct functions/calls, moved SectionName literals into the Tag
        '               Properties of the Menu Items, just retrieve that Tag-text and go to that section. Added
        '               _General Section to the Menu, other logic now handles this unique Section.
        '               <1.060.7> Removed all explicit "Handles" references, Event Handler linkages are now built
        '               programatically.

        Const lclProcName As String =               ' <1.060.2> Routine's name for message handling
            "Menu_SectionChoiceClick"

        PositionToSectionByName(sender.Tag, True)   ' <1.060.2> Pass embedded Tag data to search: Tag = SectionName; True -> update Descriptive Box

    End Sub

    ' MENU EDIT MODE
    Private Sub Menu_StartEditModeClick(            ' Clicked in menu > Tools > ODF Editing Enabled
            sender As Object,                       ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_EditModeStart.Click

        ' MET L'ODF EN MODE EDITION AUTORISEE
        ' modif version 055 & 56

        ' Purpose:      Allows modifications to the ODF text in Rtb_ODF
        ' Process:		Call EnableEditing()
        ' Called By:    Menu_EditModeStart Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> Seperated processing logic from Event Handler, to allow other calls
        '               to the logic.

        Const lclProcName As String =               ' <1.060.2> Routine's name for message handling
            "Menu_StartEditModeClick"

        EnableEditing()                             ' <1.060.8> Call relocated logic

    End Sub
    Private Sub EnableEditing()

        ' Purpose:      Allows modifications to the ODF text in Rtb_ODF
        ' Process:		Enable modification of the Rtb_ODF control, adjust the Menu to reflect the
        '               new state; issue a warning message re. recomputing Section details if text
        '               has been added/deleted.
        ' Called By:    Menu_StartEditModeClick(); CM_ODFEditModeClick();
        ' Side Effects: Alters Menu State, sets global G_EditMode
        ' Notes:        <None>
        ' Updates:      <1.060.2> Force Enable/Disable submenu choices to be a toggle. Shorten warning.
        '               Removed recompute warning, ReCompute is no longer needed.
        '               <1.060.8> Seperated processing logic from Event Handler, to allow other calls
        '               to the logic.

        Const lclProcName As String =               ' <1.060.2> Routine's name for message handling
            "EnableEditing"

        Rtb_ODF.ReadOnly = False                    ' Set control to allow modifications
        Rtb_ODF.BackColor = Color.White             ' Lighten the background to indicate text is editable
        Menu_EditModeStart.Checked = True           ' Check Menu item to indicate it is enabled
        Menu_EditModeStart.Enabled = False          ' <1.060.2> Disable this submenu choice, as we've already enabled editing
        Menu_EditModeExit.Checked = False           ' Uncheck its obverse
        Menu_EditModeExit.Enabled = True            ' <1.060.2> Permit the choice to disable editing
        Menu_EditMode.BackColor = Color.Red         ' Paint Main Menu text with RED background
        G_EditMode = True                           ' indique edition en cours (modif 055); save State

        DispMsg("", conMsgInfo,
                "ODF Text is now editable")
    End Sub
    Private Sub Menu_ExitEditModeClick(             ' menu > Edit Mode > ODF Editing Disabled
            sender As Object,                       ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_EditModeExit.Click

        ' REMET L'ODF EN MODE EDITION INTERDITE

        ' Purpose:      Exit Edit Mode
        ' Process:		Call DisableEditing()
        ' Called By:    Menu_EditModeExit Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates       <1.060.8> Seperated processing logic from Event Handler, allowing logic to be
        '               called from other procedures.

        Const lclProcName As String =               ' <1.060.2> Routine's name for message handling
            "Menu_ExitEditModeClick"

        DisableEditing()                            ' <1.060.8> Call relocated logic

    End Sub
    Private Sub DisableEditing()

        ' REMET L'ODF EN MODE EDITION INTERDITE

        ' Purpose:      Exit Edit Mode - Resets mode and menus to Read-Only
        ' Process:		Disable editing in the ODF, revert background color to the standard Light Gray,
        '               adjust the EDIT-MODE menus to reflect status. ReComputes no longer needed.
        ' Called By:    Menu_ExitEditModeClick(); CM_ODFEditModeClick()
        ' Side Effects: Alters Menu state, adjusts G_EditMode
        ' Notes:        <None>
        ' Updates       <1.060.2> Added enable/disable settings to force the submenu items for enabling
        '               and disabling ODF editing to act as a true toggle - only one choice is available
        '               at a time. Changed "Edit Mode" menu item to use standard background color when
        '               editing is disabled (the default). Removed solicitation to recompute.
        '               <1.060.8> Seperated processing logic from Event Handler, allowing logic to be
        '               called from other procedures.

        Const lclProcName As String =               ' <1.060.2> Routine's name for message handling
            "DisableEditing"

        'Dim rep As Integer                          ' Response from warning MsgBox: recompute Sections or not?

        Rtb_ODF.ReadOnly = True                     ' Return ODF to non-editable
        Rtb_ODF.BackColor = Color.WhiteSmoke        ' Reset to slightly darker background
        Menu_EditModeStart.Checked = False          ' Uncheck "ODF Editing Enabled"
        Menu_EditModeStart.Enabled = True           ' <1.060.2> Allow choice to enable editing
        Menu_EditModeExit.Checked = True            ' Check "ODF Editing Disabled"
        Menu_EditModeExit.Enabled = False           ' <1.060.2> Disallow choice to disable editing
        Menu_EditMode.BackColor =
            Color.Gainsboro                         ' Reset Main Menu text background color <1.060.2> changed from Light Steel Blue to Gainsboro
        G_EditMode = False                          ' indique retour au mode lecture seule (modif v055); save State

        'If G_ODFModSinceParsed Then                 ' <1.060.2> Only solicit recompute if changes were made since the last recompute
        '    rep = MsgBox("The ODF may have been changed." & vbCrLf &
        '                 "Do you want to recompute the Section data?",
        '             MsgBoxStyle.YesNo + MsgBoxStyle.Question,
        '             "Exiting Edit-Mode")
        '    If rep = vbYes Then
        '        ParseSections(conVerbose)
        '        PositionToSectionByName("DisplayPage", False)                   ' <1.060.2> Position to the initial Section; True -> Don't display/parse a row, do not alter Descriptive Box
        '    End If
        'End If

    End Sub
    Private Sub Menu_ReComputeSectionsClick(            ' menu > Edit Mode > Recompute Sections
            sender As Object,                           ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_ReComputeSections.Click

        ' Purpose:      Replace old ReCompute with a scan of all Sections, displaying them as we find them
        ' Process:		Call EnumerateSectionsSetFont(), which scans for all Section Headers - let it list the Section Data
        ' Called By:    Menu_RecomputeSections Click Event
        ' Side Effects: <NA>
        ' Notes:        <None>
        ' Updates:      <1.060.2> Repositioned to start of ODF after parsing. Changed to call EnumerateSectionsSetFont(),
        '               deprecating actual ReCompute, as we no longer use a static data table to navigate.

        Const lclProcName As String =                   ' <1.060.2> Routine's name for message handling
            "Menu_ReComputeSectionsClick"

        EnumerateSectionsSetFont(Rtb_ODF,
                                 Num_ODFFontSize.Value, ' <1.060.2> Keep present Font size
                                 G_NoODF,
                                 True,                  ' <1.060.2> Enumerate Sections in Display Text Area
                                 True)                  ' <1.060.2> Adjust Title Emphasis
        PositionToSectionByName("DisplayPage",
                                False)                  ' <1.060.2> Position to the initial Section; False -> Don't display/parse a row, do not alter Descriptive Text Area

    End Sub

    ' MENU TOOLS
    Private Sub Menu_ClearMarkersClick(     ' menu > Tools > Clear Markers
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_ClearMarkers.Click

        ' Purpose:      Erase any stored Marker info, reset to default state
        ' Process:		Call processing subroutine
        ' Called By:    Menu_ClearMarkers Click Event
        ' Side Effects: <None>
        ' Notes:        <None>

        Const lclProcName As String =       ' <1.060.2> Routine's name for message handling
            "Menu_ClearMarkersClick"

        ClearMarkers()                      ' Dispatch common routine

    End Sub
    Private Sub Menu_CouplersCodeClick(     ' menu > Tools > Couplers (Encode/Decode)
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_CouplersCode.Click

        ' AFFICHE LA FENETRE COUPLERS

        ' Purpose:      Display the Couplers form in its own Window
        ' Process:		Show it
        ' Called By:    Menu_CouplersCode Click Event
        ' Side Effects: Displays the Coupler Decoding Form
        ' Notes:        <None>

        Const lclProcName As String =       ' <1.060.2> Routine's name for message handling
            "Menu_CouplersCodeClick"

        Couplers.Visible = False
        Couplers.Show(Me)                   ' Open the form; if already open, give it focus

    End Sub
    Private Sub FollowASampleToolStripMenuItemClick(    ' menu > Tools > Trace a Sample
            sender As Object,                           ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_FollowASample.Click

        ' AFFICHE LA FENETRE FOLLOW

        ' Purpose:      Display the FollowSample form in its own Window
        ' Process:		Force Section Type to Follow to "Sample", show form
        ' Called By:    Menu_FollowASample Click Event
        ' Side Effects: Updates G_Item_to_Follow, Displays Follow-A-Sample form
        ' Notes:        <None>

        Const lclProcName As String =                   ' <1.060.2> Routine's name for message handling
            "FollowASampleToolStripMenuItemClick"

        Trace.Visible = False
        Trace.Show(Me)                                 ' Open the form, if already open, give it focus

    End Sub

    ' MENU ?
    Private Sub Menu_HelpClick(                                 ' menu > ? > View Help
            sender As Object,                                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_Help.Click

        ' Purpose:      Display the help text: HTML version is first option, short version in
        '               Descriptive Text Area is fallback if HTML can't be found or opened.
        ' Process:		COnstruct path to HTML Help, if found, attempt to dispatch in default
        '               browser; if any of this fails, fallback to locating the short-help .rtf
        '               file and displaying that. If that fails, apologize.
        ' Called By:    Menu_Help Click Event
        ' Side Effects: If HTML fails, updates Rtb_DescText control contents
        ' Notes:        <None>
        ' Updates:      <1.060.2> Add code to let user know if help file is missing. Change filname/path assembly
        '               to Path.Combine, for greater OS independence. Update Title over RTB. Eliminate reference
        '               to unneeded global G_RTFFile, use a local var instead to construct the path/filename.
        '               Disable "Set Font" and "Save Description" Buttons. Added exception handler. Added
        '               HTML Help as the default help system, falling back to the original if the HTML
        '               content is missing.

        Const lclProcName As String =                           ' <1.060.2> Routine's name for message handling
            "Menu_HelpClick"

        Dim pathToHelpFile As String = ""                       ' <1.060.2> To build full path/filename to the Help File

        Try
            If G_HelpFilePath <> "" Then                        ' <1.060.2> Directory Exists
                pathToHelpFile = Path.Combine(G_HelpFilePath,   ' <1.060.2> Build path to the HTML Top-level Help
                                          conHTMLHelpFile)
                If File.Exists(pathToHelpFile) Then             ' <1.060.2> Directory exists, does top HTML file?
                    Help.ShowHelp(Me, pathToHelpFile)           ' <1.060.2> Yes, display it
                    Return                                      ' <1.060.2> And we are done
                Else                                            ' <1.060.2> HTML file is missing, warn user, and continue with old-style Help
                    DispMsg("", conMsgInfo,
                        "Unable to locate the top HTML Help File:" & vbCrLf &
                        "Full file path is: " & pathToHelpFile & vbCrLf &
                        "Will attempt to continue with limited Help displayed in the Descriptive Text Area.")
                    G_HelpFilePath = ""                         ' <1.060.0> Next time, go straight to old-style Help
                End If
            End If
        Catch ex1 As Exception
            DispMsg(lclProcName, conMsgCrit,
                    "Exception while attempting to locate or load the HTML Help File." & vbCrLf &
                    "Full file path is: " & pathToHelpFile & vbCrLf &
                    "Will attempt to continue with limited Help displayed in the Descriptive Text Area." & vbCrLf &
                    "Exception code is: " & ex1.Message)
            G_HelpFilePath = ""                                 ' <1.060.2> Clear this var, so next time we go staright to non-HTML Help
        End Try

        Try
            pathToHelpFile = Path.Combine(G_DataPath,
                                 conRTFHelpFile)                ' <1.060.2> Build the path to the help file
            If File.Exists(pathToHelpFile) Then                 ' Does it exist? <1.060.2> Changed Method to File.Exists
                Rtb_DescText.LoadFile(pathToHelpFile)           ' Yes, copy its contents into display box
                G_PreviousRTFFile = pathToHelpFile              ' <1.060.2> Remember that Help File is now loaded
                SetRTBDescButtons(False)                        ' <1.060.2> Disable "Set Font" and "Save Description" Controls
                Lbl_TextBoxTitle1.Text = ""                     ' <1.060.2> Clear the Top Title Line
                CenterText(conTextBoxTitle_Help,                ' <1.060.2> Display Help Title centered on the Bottom Title Line
                           Lbl_TextBoxTitle2,
                           Rtb_DescText.Left,
                           Rtb_DescText.Right)
                Menu_PrintDT.Enabled = True                     ' <1.060.3> Enable printing when short-help is on display
                'Btn_SaveDescText.Enabled = True                 ' Temp - Enable this line to enable control to allow saving to the Help File e.g., to make major changes.
            Else                                                ' <1.060.2> Message the user
                DispMsg(lclProcName, conMsgCrit,
                        "Unable to locate Help File." & vbCrLf &
                        "Expected path is: " & pathToHelpFile)
            End If
        Catch ex As Exception
            DispMsg(lclProcName, conMsgCrit,
                       "Exception while attempting to locate or load the RTF Help File." & vbCrLf &
                       "Full file path is: " & pathToHelpFile & vbCrLf &
                       "Exception Code is: " & ex.Message)
        End Try

    End Sub

    ' BOUTONS
    Private Sub Btn_FindFirstClick(     ' Clicked the Find First Button
            sender As Object,           ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Btn_FindFirst.Click

        ' Purpose:      Initiate a text search in the ODF, starting from the beginning
        ' Process:		Set search position to the beginning, search forward
        ' Called By:    Btn_FindFirst Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> Replaced global G_FindStartPosition with parameter
        '               <1.060.6> Take Cursor position from .Tag property rather than .Text

        Const lclProcName As String =   ' <1.060.2> Routine's name for message handling 
            "Btn_FindFirstClick"

        Dim curPos As Integer           ' <1.060.2> Set to current Cursor position

        Try                             ' <1.060.6> Take position from onscreen display's .Tag property
            curPos = CInt(Lbl_CursorPosVal.Tag)
        Catch                           ' <1.060.2> If for some reason field wasn't a valid integer, place cursor at beginning
            curPos = 0
        End Try

        FindButtonsProc(-1,             ' <1.060.2> Search from beginning of ODF (FindButtonsProc will add 1 to this, to start at 0)
                        curPos,         ' <1.060.2> Return cursor to its current position if search fails
                        M_FoundStart,   ' <1.060.2> Start/End positions of last successful Find, for Next/Prev processing
                        M_FoundEnd,
                        True)           ' <1.060.2> Execute a forward search

    End Sub
    Private Sub Btn_FindNextClick(      ' Clicked the Find Next > Button
            sender As Object,           ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Btn_FindNext.Click

        ' Purpose:      Continue a text search in the ODF, starting from the current position
        ' Process:		Leave search position as-is, search forward
        ' Called By:    Btn_FindNext Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> Replaced global G_FindStartPosition with a forward search
        '               from the current cursor position.
        '               <1.060.6> Changed from .Text to .Tag property for retrieving Cursor's position

        Const lclProcName As String =   ' <1.060.2> Routine's name for message handling 
            "Btn_FindNextClick"

        Dim curPos As Integer           ' <1.060.2> Set to current Cursor position

        Try                             ' <1.060.2> Take position from onscreen display's .Tag property, not .Text
            curPos = CInt(Lbl_CursorPosVal.Tag)
        Catch                           ' <1.060.2> If for some reason field wasn't a valid integer, use 0 as beginning
            curPos = 0
        End Try

        FindButtonsProc(curPos,         ' <1.060.2> Start search at current Cursor position
                        curPos,         ' <1.060.2> Return to current Cursor position if search fails
                        M_FoundStart,   ' <1.060.2> Start and End of previsouly found Text, for Next/Prev processing
                        M_FoundEnd,
                        True)           ' <1.060.2> Search forward

    End Sub
    Private Sub Btn_LedClick(                       ' Clicked the Led Button in the Data Panel
            sender As Object,                       ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Btn_Led.Click

        ' Purpose:      Replaced silent ReCompute with null, we no longer use static navigation data.
        '               Control remains, proving color indicator, when scanning the entire ODF.
        ' Process:		Returns to caller.
        ' Called By:    Btn_Led Click Event
        ' Side Effects: <NA>
        ' Notes:        <None>
        ' Updates:      <1.060.2> After parsing, reset to top Section. Removed call to parser.
        '               In last revision, just return - actions are no longer required.

        Const lclProcName As String =               ' <a1.060.2> Routine's name for message handling
            "Btn_LedClick"

        Return

    End Sub
    Private Sub Btn_SetFontClick(               ' Clicked the Set Desc. Font Button
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Btn_SetFont.Click

        ' FORCE LA POLICE DE LA BOX

        ' Purpose:      Force Descriptive text box to 10-point Verdana
        ' Process:		Select the control, loop through the text setting the Font and Size
        '               1 character at-a-time, so other Style attributes can be retained.
        ' Called By:    Btn_SetFont Click Event
        ' Side Effects: Alters Rtb_DescText content
        ' Notes:        <None>
        ' Updates:      <1.060.2> Modified code that forces the Font to Verdana 10 so it retains other
        '               text attributes, such as Bolding & Underlining. This requires processing a single
        '               character at a time, very inefficient, but OK for an occasional use. Changed choice
        '               of Font and Size to symbolic constants (still defined as Verdana 10)

        Const lclProcName As String =           ' <1.060.2> Routine's name for message handling
            "Btn_SetFontClick"

        Dim i As Integer                        ' <1.060.2> Loop index var

        For i = 1 To Rtb_DescText.TextLength    ' <1.060.2> Loop over each character in the box
            Rtb_DescText.Select(i, 1)           ' <1.060.2> Select a single char, set its font/size, retain other Stlye attributes
            Rtb_DescText.SelectionFont = New Font(conDescFont, conDefODFFontSize, Rtb_DescText.SelectionFont.Style)
        Next

        Rtb_DescText.DeselectAll()
        Rtb_DescText.Refresh()                  ' Update screen

    End Sub
    Private Sub Btn_MarkersMouseDown(               ' Mouse-clicked on a Marker Button
            sender As Object,                       ' Standard Event Parameters for a Control
            e As MouseEventArgs
            ) Handles Btn_Marker1.MouseDown, Btn_Marker2.MouseDown, Btn_Marker3.MouseDown, Btn_Marker4.MouseDown

        ' MEMORISE OU RETROUVE UNE LIGNE

        ' Purpose:      Process right click to set a marker, left click to position to a marker
        ' Process:		To set, copy the Cursor position into the Button's text, and color the control.
        '               To follow, retrieve text from control, move Cursor to that location in the ODF.
        ' Called By:    Btn_Marker1/2/3/4 MouseDown Events
        ' Side Effects: Can alter Button's text and color; can alter Cursor position in ODF.
        ' Notes:        <None>
        ' Updates:      <1.060.2> Converted Markers to be Line-based rather than character-based, as they position
        '               to the beginning of a Line. Extended semantics to include all single Mouse-click behavior
        '               at the Marker's return-point: most importantly, updating Line#, Caret Pos, Line Start/End, and
        '               Row Start/End.
        '               <1.060.6> Save char-based position in Marker's .Tag property.

        Const lclProcName As String =               ' <1.060.2> Routine's name for message handling
            "Btn_MarkersMouseDown"

        Dim bouton As String = e.Button.ToString    ' "Left" or "Right", tells us which button
        Dim marker As Control = sender              ' Superfluous (just use [sender]); Control object that raised the event
        Dim caret As Integer                        ' Position to move to, retrieved from a Marker
        Dim ln As Integer
        Dim lineStart As Integer

        If bouton = "Right" Then                    ' Save Cursor Position as text, change button's color
            marker.Text = Lbl_LineNumVal.Text
            marker.Tag = Lbl_CursorPosVal.Tag       ' Save internally as Char postion in .Tag property
            marker.BackColor = Color.LightCyan      ' Visual indicator that the Marker is set
        End If

        If bouton = "Left" Then                     ' Retrieve position from text, reposition the cursor
            If marker.Tag = 0 Then                  ' <1.060.6> Null Marker, just return
                Return
            End If
            'Try
            '    ln = CInt(marker.Text) - 1          ' <1.060.2> Convert Line Number back to 0-base (Marker displayed 1-base Ln#)
            'Catch
            '    Return
            'End Try

            'caret = Rtb_ODF.GetFirstCharIndexFromLine(ln)
            caret = marker.Tag                      ' <1.060.6> Retrieve the stored Cursor position from the .Tag property
            MoveToPosition(caret,                   ' Position to the Line in the ODF at position "caret"
                           G_LineIndex,             ' <1.060.2> Returns Line Number, 0-based
                           lineStart,               ' <1.060.2> Returns index to beginning of Line
                           True,                    ' <1.060.2> Position the cursor, this is a new position
                           False)                   ' <1.060.2> False -> Do not extend selection on screen, leave as user set it
        End If

    End Sub
    Private Sub Btn_RowActionClick(         ' Take a contextual action on a Row: Display Image, Trace Sample...
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Btn_RowAction.Click

        ' Purpose:      When the current Row includes a Child Element supports a contextual action assigned
        '               to the ActionButton, dispatch that Action e.g. Display Image and Trace Sample
        ' Process:		Call TakeRowAction() to do the work
        ' Called By:    Btn_DisplayImage Click Event
        ' Side Effects: <NA>
        ' Notes:        <None>
        ' Updates:      <1.060.2> Added all global variable references required by TakeRowAction() to the call, so
        '               TakeRowAction() need not directly reference these globals. G_PackagePath was established when
        '               the ODF was loaded; the other vars were set when the Row Parse displayed an ImageSet or
        '               ImageSetElement Row.

        Const lclProcName As String =       ' <1.060.2> Routine's name for message handling
            "Btn_RowActionClick"

        TakeRowAction(G_PackagePath,        ' <1.060.2> Let TakeRowAction() do all the work
                     G_ImageFile,
                     G_ImageSet,
                     G_PackageID,
                     G_MinPanelHeights)

    End Sub
    Private Sub Btn_NextLineClick(                              ' Handle the 6 Next/Prev Line Buttons
            sender As Button,                                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Btn_NextLine.Click, Btn_Next10Lines.Click, Btn_Next100Lines.Click,
            Btn_PrevLine.Click, Btn_Prev10Lines.Click, Btn_Prev100Lines.Click

        ' RECHERCHE LA PROCHAINE LIGNE

        ' Purpose:      Advance/Retard position in the ODF by 1, 10, or 100 lines
        ' Process:		Retrieve change (+/- 1, +/- 10, +/- 100) from calling Control.
        '               Reposition Line, locate first character in line, place Cursor on first character,
        '               locate containing Section, extract, parse, and display Child Elements of new
        '               Row, scroll main ODF display to the new Position.
        ' Called By:    Btn_NextLine Click Event
        ' Side Effects: Update position globals, update fields on Main form
        ' Notes:        <None>
        ' Updates:      <1.060.2> Fix off-by-one in displaying Line Number - internal LNums are zero-based.
        '               Added support for Previous 1, 10, and 100 lines. Replaced use of global G_CaretPos
        '               with local cursorPos. Removed G_RowText reference, assign directly from MoveToPosition().
        '               Get Section Start/End dynamically from GetSectionFromIndex(), update screen.
        '               <1.060.6> Save Section Start/End positions into the .Tag properties, for later use repositioning.

        Const lclProcName As String =                           ' <1.060.2> Routine's name for message handling
            "Btn_NextLineClick"

        Dim desiredNewLine As Integer                           ' Where do we want to move to, prior to imposing limitations
        Dim cursorPos As Integer                                ' <1.060.2> Local var to hold cursor's location, replaced G_CaretPos
        Dim secStart As Integer                                 ' <1.060.2> Section Start, determined dynamically by GetSectionFromIndex()
        Dim secEnd As Integer                                   ' <1.060.2> Section End, determined dynamically by GetSectionFromIndex()
        Dim secLStart As Integer                                ' <1.060.6> Section's Starting Line
        Dim secLEnd As Integer                                  ' <1.060.6> Section's Ending Line
        Dim lineStart As Integer                                ' <1.060.2> Line Start, do our Section search from here

        Try
            desiredNewLine = G_LineIndex + sender.Tag           ' Advance/Retard desired line position by 1, 10, or 100 lines
            G_LineIndex = Clamp(desiredNewLine,                 ' Limit Line Position to stay with 0-based range of lines in ODF 
                                0,
                                Rtb_ODF.Lines.Length - 1)
            cursorPos = Rtb_ODF.GetFirstCharIndexFromLine(
                G_LineIndex)                                    ' Move Cursor to first character of the new line
            Rtb_XMLRow.Text = MoveToPosition(cursorPos,         ' Update Line/Row Data, select entire Row, get Row's text 
                                             G_LineIndex,
                                             lineStart,
                                             True,              ' <1.060.2> Reposition the cursor
                                             True)
            G_SectionName = GetSectionFromIndex(lineStart,      ' retrouver la section auquel il appartient; locate and load Section Data for the now current Section
                                                secStart,       ' <1.060.2> Function returns the Start and End positions for the Section dynamically (no recompute)
                                                secEnd,
                                                secLStart,      ' <1.060.6> Added Starting/Ending Lines
                                                secLEnd)
            Lbl_SectionName.Text = G_SectionName                ' <1.060.2> GetSectionFromIndex() is no longer responsible for screen update, update the Name from here
            DisplayXMLRow()                                     ' montrer les infos de la ligne; <1.059.0> Removed parameter (1), DisplayXMLRow is now parameterless; parse & display Row
            Lbl_SecStartVal.Text = secLStart.ToString(conIntFmt) &
                " / " & secStart.ToString(conIntFmt)
            Lbl_SecStartVal.Tag = secStart                      ' <1.060.6> Save the raw position data in the .Tag property
            Lbl_SecEndVal.Text = secLEnd.ToString(conIntFmt) &
                " / " & secEnd.ToString(conIntFmt)
            Lbl_SecEndVal.Tag = secEnd

            G_PreviousRTFFile = LoadRTFFile(G_DataPath,         ' charger le fichier RTF; retrieve and display Section's Descriptive Text
                                            G_SectionName,
                                            G_PreviousRTFFile)
            Rtb_ODF.Focus()

        Catch                                                   ' Catch exceptions here, and ignore
            ' ras
        End Try

    End Sub
    Private Sub Rtb_ODFTextChanged(                     ' Updadate Status-Bar and "Dirty Bits"
            sender As Object,                           ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Rtb_ODF.TextChanged

        ' Purpose:      Event triggers whenever text of Rtb_ODF is changed: loaded, cleared, edited by user.
        '               Updates Status-Bar and "Dirty" Bits.
        ' Process:		If length changed, set length global to new length; If in Edit Mode, set ODF dirty bit
        ' Called By:    Rtb_ODF TextChanged Event
        ' Side Effects: Set global ODF Dirty Bit to True, and alter Status-Bar on screen.
        ' Notes:        This event is also triggered by modifications made internally by AECHO, such as
        '               loading a file, or altering text sizes.
        ' Updates:      <1.060.2> Sets the file dirty bit, and updates the Status-Bar
        '               <1.060.2> Update Status-Bar Line & Char counts if changed while in Edit Mode

        Const lclProcName As String =                   ' <1.060.2> Routine's name for message handling
            "Rtb_ODFTextChanged"

        If G_EditMode Then
            G_ODFModSinceSaved = True
            Status_FileDirtyVal.BackColor = Color.Red   ' <1.060.2> Set File-Dirty indiator on the Status-Bar
            Status_LinesVal.Text =                      ' <1.060.4> Display initial length in Lines and Characters
                Rtb_ODF.Lines.Count().ToString(conIntFmt)
            Status_CharsVal.Text =
                Rtb_ODF.TextLength().ToString(conIntFmt)
        End If

    End Sub
    Private Sub Rtb_ODFKeyup(               ' When Key is released, detect if it was an Arrow Key
            sender As Object,               ' Standard Event Parameters for a Control
            e As KeyEventArgs
            ) Handles Rtb_ODF.KeyUp

        ' Purpose:      Intercept Arrow Keys, cause them to update Positin Data.
        ' Process:		KeyUp triggers after screen has updated, so an arrow key has already moved the
        '               onscreen cursor to its new location, per normal rules. We now need to update
        '               the related position data - identify where the cursor is, and process a
        '               Single-Click Move to that location.
        ' Called By:    Rtb_ODF KeyUp Event
        ' Side Effects: Updates Data Panel Position Fields & G_LineIndex
        ' Notes:        <None>
        ' Updates:      <1.060.9> First Implementation

        Const lclProcName As String =       ' Routine's name for message handling
                "Rtb_ODFKeyup"

        Dim c As Keys                       ' Will get the value of the key that was pressed
        Dim cursorPos As Integer            ' New cursor location after ey was pressed
        Dim lineStart As Integer            ' Dummy variable for call to MoveToPostion()

        c = e.KeyValue                      ' Retrieve the Key's ID
        If (c = Keys.Up) Or                 ' We only take action for Up, Down, Left, & Right Arrows
            (c = Keys.Down) Or
            (c = Keys.Left) Or
            (c = Keys.Right) Then

            If Rtb_ODF.TextLength = 0 Then  ' ODF is empty
                Return
            End If

            cursorPos =                     ' Extract the Cursor position from Rtb_ODF
                Rtb_ODF.SelectionStart

            MoveToPosition(cursorPos,       ' Move to this position "officially", so data matches screen
                           G_LineIndex,     ' Returns Line Number, 0-based
                           lineStart,       ' Returns index to beginning of Line, we can ignore
                           False,           ' Do not further position the cursor, this would erase a selection
                           False)           ' False -> Do not extend selection on screen, leave as user set it - a single-click
        End If

    End Sub
    Private Sub Menu_AboutClick(        ' menu > ? > About AECHO was clicked
            sender As Object,           ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_About.Click

        ' Purpose:      Display the About Box
        ' Process:		Just use form Show method for the AboutBox
        ' Called By:    Menu_About Click Event
        ' Side Effects: Displays form in a new Window
        ' Notes:        Replaced original MsgBox code.
        ' Updates:      <1.059.0> Replaced MsgBox with VStudio embedded "AboutBox" control.
        '               This control takes its content from the Project's Assemby Parameters, maintained by the IDE.

        Const lclProcName As String =   ' <1.060.2> Routine's name for message handling
            "Menu_AboutClick"

        AboutBox1.Show(Me)              ' Display the form

    End Sub
    Private Sub Btn_FindPrevClick(      ' < Find Prev button was clicked
            sender As Object,           ' Standard Event Parameters for a Control
            e As EventArgs) Handles Btn_FindPrev.Click

        ' Purpose:      Continue a reverse search in the ODF, starting from the current position
        ' Process:		Leave search position as-is, search backwards
        ' Called By:    Btn_FindPrev Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.
        '               <1.060.6> Changed to take position from .Tag property rather than .Text

        Const lclProcName As String =   ' <1.060.2> Routine's name for message handling 
            "Btn_FindPrevClick"

        Dim curPos As Integer           ' <1.060.2> Set to current Cursor postiion

        Try                             ' <1.060.2> Take position from onscreen display's .Tag property
            curPos = CInt(Lbl_CursorPosVal.Tag)
        Catch                           ' <1.060.2> If for some reason field wasn't a valid integer, search from end of ODF
            curPos = Rtb_ODF.TextLength - 1
        End Try

        FindButtonsProc(curPos,         ' <1.060.2> Search from here (current cursor position)
                        curPos,         ' <1.060.2> Return to here if not found (current cursor position)
                        M_FoundStart,   ' <1.060.2> Start/End indices of last successful Find, for Next/Prev processing
                        M_FoundEnd,
                        False)          ' <1.060.2> Search backwards from present cursor position

    End Sub
    Private Sub Lbl_LineNumValClick(        ' Clicked on Line Number field in Daa Panel
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs) Handles Lbl_LineNumVal.Click

        ' Purpose:      If active, position to the beginning of the line identified here. Use single-click model.
        ' Process:		Ensure we have a good value, convert to character index, set new position, update Data.
        ' Called By:    Lbl_LineNumVal Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.

        Const lclProcName As String =       ' Routine's name for message handling
            "Lbl_LineNumValClick"

        Dim ln As Integer
        Dim curPos As Integer
        Dim lineStart As Integer

        If Lbl_LineNumVal.Text = "NA" Then  ' If no data, just return
            Return
        End If

        ln = CInt(Lbl_LineNumVal.Text) - 1  ' Get the Line Number, return it to 0-based
        If (ln < 0) Or (ln > (Rtb_ODF.Lines.Length - 1)) Then
            Return                          ' Make sure Line is in bounds of current ODF
        End If

        curPos = Rtb_ODF.GetFirstCharIndexFromLine(ln)
        MoveToPosition(curPos,              ' Position to the Line in the ODF at position "caret"
                       G_LineIndex,         ' Returns Line Number, 0-based - update the Global
                       lineStart,           ' Returns index to beginning of Line
                       True,                ' Reposition the cursor
                       False)               ' False -> Do not extend selection on screen, leave as user set it

    End Sub
    Private Sub Lbl_SecStartValClick(       ' Clicked on Section Start field of Data Panel
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Lbl_SecStartVal.Click

        ' Purpose:      If active, position to the Section Start. Use single-click model.
        ' Process:		Calls HotClickCursorPosition() to ensure we have a good value: set new position, update Data.
        ' Called By:    Lbl_SecStartVal Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.
        '               <1.060.6> Changed from using .Text to .Tag property, which holds the raw position

        Const lclProcName As String =       ' Routine's name for message handling
            "Lbl_SecStartValClick"

        HotClickCursorPosition(Lbl_SecStartVal.Tag)

    End Sub
    Private Sub Lbl_SecEndValClick(         ' Clicked on Section End field of Data Panel
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Lbl_SecEndVal.Click

        ' Purpose:      If active, position to the Section End. Use single-click model.
        ' Process:		Calls HotClickCursorPosition() to ensure we have a good value: set new position, update Data.
        ' Called By:    Lbl_SecEndVal Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.
        '               <1.060.6> Changed from using .Text to .Tag property, which holds the raw position

        Const lclProcName As String =       ' Routine's name for message handling
            "Lbl_SecEndValClick"

        HotClickCursorPosition(Lbl_SecEndVal.Tag)

    End Sub
    Private Sub Lbl_LineStartValClick(      ' Clciked on Line Start field of Data Panel
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Lbl_LineStartVal.Click

        ' Purpose:      If active, position to the Line Start. Use single-click model.
        ' Process:		Calls HotClickCursorPosition() to ensure we have a good value: set new position, update Data.
        ' Called By:    Lbl_LineStartVal Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.
        '               <1.060.6> Changed from using .Text to .Tag property, which holds the raw position

        Const lclProcName As String =       ' Routine's name for message handling
            "Lbl_LineStartValClick"

        HotClickCursorPosition(Lbl_LineStartVal.Tag)

    End Sub
    Private Sub Lbl_LineEndValClick(        ' Clicked on Line End field of Data Panel
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Lbl_LineEndVal.Click

        ' Purpose:      If active, position to the Line End. Use single-click model.
        ' Process:		Calls HotClickCursorPosition() to ensure we have a good value: set new position, update Data.
        ' Called By:    Lbl_LineEndVal Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.
        '               <1.060.6> Changed from using .Text to .Tag property, which holds the raw position

        Const lclProcName As String =       ' Routine's name for message handling
            "Lbl_LineEndValClick"

        HotClickCursorPosition(Lbl_LineEndVal.Tag)

    End Sub
    Private Sub Lbl_RowStartValClick(       ' Clicked on Row Start field of Data Panel
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Lbl_RowStartVal.Click

        ' Purpose:      If active, position to the Row Start. Use single-click model.
        ' Process:		Calls HotClickCursorPosition() to ensure we have a good value: set new position, update Data.
        ' Called By:    Lbl_RowStartVal Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.
        '               <1.060.6> Changed from using .Text to .Tag property, which holds the raw position

        Const lclProcName As String =       ' Routine's name for message handling
            "Lbl_RowStartValClick"

        HotClickCursorPosition(Lbl_RowStartVal.Tag)

    End Sub
    Private Sub Lbl_RowEndValClick(         ' Clicked on Row End field of Data Panel
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Lbl_RowEndVal.Click

        ' Purpose:      If active, position to the Row End. Use single-click model.
        ' Process:		Calls HotClickCursorPosition() to ensure we have a good value: set new position, update Data.
        ' Called By:    Lbl_RowEndVal Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.
        '               <1.060.6> Changed from using .Text to .Tag property, which holds the raw position

        Const lclProcName As String =       ' Routine's name for message handling
            "Lbl_RowEndValClick"

        HotClickCursorPosition(Lbl_RowEndVal.Tag)

    End Sub
    Private Sub Lbl_CursorPosValClick(      ' Clicked on Caret Position Field of Data Panel
            sender As Object,               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Lbl_CursorPosVal.Click

        ' Purpose:      If active, position to the Cursor. Use single-click model.
        ' Process:		Calls HotClickCursorPosition() to ensure we have a good value: set new position, update Data.
        ' Called By:    Lbl_CursorPosVal Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.
        '               <1.060.6> Changed from using .Text to .Tag property, which holds the raw position

        Const lclProcName As String =       ' Routine's name for message handling
            "Lbl_CursorPosValClick"

        HotClickCursorPosition(Lbl_CursorPosVal.Tag)

    End Sub
    Private Sub Menu_PrintDTClick(      ' Clicked on Menu > File > Print Descriptive Text...
            sender As Object,           ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles Menu_PrintDT.Click

        ' Purpose:      Initialize Print Dialog, if User agrees, submit print job
        ' Process:      Call Print Hanlfer
        ' Called By:    Menu_Print Click event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First implemented as a test skeleton
        '               <1.060.8> Seperated Event Handler from Print Logic, so Print can be
        '               called from elsewhere.

        Const lclProcName As String =   ' Routine's name for message handling
            "Menu_PrintDRClick"

        PrintDT()                       ' <1.060.8> Invoke Print command handler

    End Sub
    Private Sub PrintDT()                           ' Handle Print request for Descriptive Text Area

        ' Purpose:      Initialize Print Dialog, if User agrees, submit print job
        ' Process:      Display dialog, get answer, if Yes, submit print job
        ' Called By:    Menu_PrintDT_Click(); CM_DescPrintClick()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First implemented as a test skeleton
        '               <1.060.3> Added logic to copy text to hidden custom RTB, adding
        '               headers in the custom box and printing from there. The hidden
        '               RTB is extended through inheitence to add WYSIWYG printing capability,
        '               which the standard RTB does not support.
        '               <1.060.8> Seperated Event Handler from Print Logic, so Print can be
        '               called from elsewhere.

        Const lclProcName As String =               ' Routine's name for message handling
            "PrintDT"

        Dim result As DialogResult
        Dim header As String                        ' Build print header here...
        Dim todayDateTime As Date                   ' Will be today's date and time for use in a heafer

        If Rtb_DescText.TextLength = 0 Or Lbl_TextBoxTitle2.Text = conTextBoxTitle_Def Then
            DispMsg("", conMsgInfo,                 ' Null content, display informational message and exit
                    "Nothing to print...")
            Return
        End If
        '                                             Set defaults to: Portrait;  <1/2-inch left, 
        '                                             right, and bottom margins; 3/4-inch top margin
        PrintDocument1.DocumentName = "AECHO Descriptive Text"
        PrintDocument1.DefaultPageSettings.Margins.Left = 40
        PrintDocument1.DefaultPageSettings.Margins.Right = 40
        PrintDocument1.DefaultPageSettings.Margins.Top = 75
        PrintDocument1.DefaultPageSettings.Margins.Bottom = 40

        PrintDialog1.Document = PrintDocument1      ' Bind Print Dialog to Print Object
        result = PrintDialog1.ShowDialog            ' User response

        Rtb_DText.Clear()                           ' Eliminate any residue from prior printing

        If result = DialogResult.OK Then            ' Detect content type of Descriptive Text Area and set appropriate Header Text
            If Lbl_TextBoxTitle2.Text = conTextBoxTitle2_Desc Then
                Rtb_DText.SelectionFont =           ' Add header content, centered, bold, larger font
                New Font(conDescFont, conDefODFFontSize + 4, FontStyle.Bold Or FontStyle.Underline)
                Rtb_DText.SelectionAlignment = HorizontalAlignment.Center
                header = "Section Description"
            ElseIf Lbl_TextBoxTitle2.Text = conTextBoxTitle_List Then
                todayDateTime = Date.Now
                Rtb_DText.SelectionFont =           ' First, display full filename of the organ, reduced font-size, regular weight, centered
                    New Font(conDescFont, conDefODFFontSize - 2, FontStyle.Regular)
                Rtb_DText.SelectionAlignment = HorizontalAlignment.Center
                header = todayDateTime & vbCrLf & G_OrganFile & vbCrLf & vbCrLf & " "
                Rtb_DText.SelectedText = header
                Rtb_DText.Select(Rtb_DText.TextLength - 1, 1)
                Rtb_DText.SelectionFont =           ' Then display Column Titles in regular size, bolded
                    New Font(conDescFont, conDefODFFontSize, FontStyle.Bold)
                Rtb_DText.SelectionAlignment = HorizontalAlignment.Left
                header =
                    "SecID   Start-Line      End-Line       Start-Char          End-Char     Section Name"
            ElseIf Lbl_TextBoxTitle2.Text = conTextBoxTitle_Help Then
                Rtb_DText.SelectionFont =           ' Add header content, centered, bold, larger font
                New Font(conDescFont, conDefODFFontSize + 4, FontStyle.Bold Or FontStyle.Underline)
                Rtb_DText.SelectionAlignment = HorizontalAlignment.Center
                header = "Short Help"
            Else
                Rtb_DText.SelectionFont =           ' Add header content, centered, bold, larger font
                New Font(conDescFont, conDefODFFontSize + 4, FontStyle.Bold Or FontStyle.Underline)
                Rtb_DText.SelectionAlignment = HorizontalAlignment.Center
                header = ""
            End If
            header =                                ' Add a blank line, and a closing <blank> character that will be replaced by the content text
                header & vbCrLf & vbCrLf & " "

            Rtb_DText.SelectedText = header         ' Insert header, includes spacing and sacrificial <blank>
            Rtb_DText.DeselectAll()

            Rtb_DText.Select(Rtb_DText.TextLength - 1, 1)
            Rtb_DText.SelectionFont =               ' Return font settings & alignment to normal for the sacrificial character
                New Font(conDescFont, conDefODFFontSize, FontStyle.Regular)
            Rtb_DText.SelectionAlignment = HorizontalAlignment.Left

            Rtb_DescText.SelectAll()                ' Select entire content are
            Rtb_DText.SelectedRtf =                 ' Replace sacrificial blank with formatted text (.SelectedRTF)
                Rtb_DescText.SelectedRtf
            Rtb_DText.SelectionTabs =               ' Ensure tab stops are the same
                Rtb_DescText.SelectionTabs
            Rtb_DescText.DeselectAll()              ' Deselect both source & destination boxes
            Rtb_DText.DeselectAll()
            Try
                PrintDocument1.Print()              ' Dispatch Print job for hidden, destination box
            Catch ex As Exception
                DispMsg(lclProcName, conMsgExcl,
                        "Unable to submit the print job. " & vbCrLf &
                        "Exception code is: " & ex.Message)
            End Try

        End If

    End Sub
    Private Sub PrintDocument1BeginPrint() Handles PrintDocument1.BeginPrint

        ' Purpose:      Initializer invoked as start of print job. Sets start to 1 char in control
        ' Process:      Set a shared variable that tracks the next char to print
        ' Called By:    PrintDocument1 BeginPRint Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented, supports extended RTB printing

        Const lclProcName As String =   ' Routine's name for message handling
            "PrintDocument1BeginPrint"

        M_FirstChar = 0                 ' Start at first character

    End Sub
    Private Sub PrintDocument1EndPrint() Handles PrintDocument1.EndPrint

        ' Purpose:      Free graphics memory used by formatter, once print job is fully rendered
        ' Process:      Have an appropriate message sent
        ' Called By:    PrintDocument1 EndPrint Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented, supports extended RTB printing

        Const lclProcName As String =   ' Routine's name for message handling
            "PrintDocument1EndPrint"

        Rtb_DText.FormatRangeDone()     ' Free graphics memory now that we're done

    End Sub
    Private Sub PrintDocument1PrintPage(
            sender As Object,                               ' Standard Event Parameters for a Control
            e As Printing.PrintPageEventArgs
            ) Handles PrintDocument1.PrintPage

        ' Purpose:      Handle each page in order as print job progresses
        ' Process:      Invoke formatter, set Property that controls subsequent page printing. Called once
        '               for each page, until it sets e.HasMorePages to False
        ' Called By:    PrintDocument1 PrintPage Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First implemented as a concept skeleton
        '               <1.060.3> Modified for production use with extended RTB print-capable control

        Const lclProcName As String =                       ' Routine's name for message handling
            "PrintDocument1PrintPage"

        M_FirstChar =
            Rtb_DText.FormatRange(False,                    ' Select Render mode
                                  e,                        ' Control to print
                                  M_FirstChar,              ' First char on this page
                                  Rtb_DText.TextLength)     ' End of print content


        If (M_FirstChar < Rtb_DText.TextLength) Then        ' check if there are more pages to print
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub
    Public Sub BuildSecMenus()

        ' Purpose:      Scans the Sections Menu, filling out .Tag properties, adding
        '               references to the Event Handler for lowest-level entries,
        '               and completing the range (1-22, 22-44...) in the text of the
        '               intermediate entries.
        ' Process:      If this starts a new group of 22, add the Group entry first,
        '               then add the new Section as the last entry of the last group.
        ' Called By:    EnumerateSectionsSetFont()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.7> First implemented, for dynamic Section Menus

        Const lclProcName As String =                                       ' Routine's name for message handling
            "BuildSecMenus"

        Dim beg As Integer = 1                                              ' Beginning value of each Group's range: 1, 23, 45...

        For Each i As ToolStripMenuItem In Menu_SectionsA.DropDownItems     ' This level loops through the intermediate Groups
            i.Tag = ""                                                      ' No .Tag properties at this level
            i.Enabled = True
            For Each j As ToolStripMenuItem In i.DropDownItems              ' Loop over the individual entries within each Group
                j.Tag = j.Text                                              ' These are the actual active links, set the .Tag
                AddHandler j.Click, AddressOf Menu_SectionChoiceClick       ' Link the individual entries to the Event Handler
                j.Enabled = True
            Next
            i.Text += " " & beg.ToString & "-" &                            ' Add the range to the text of the Group level entry
                ((beg + i.DropDownItems.Count) - 1).ToString
            beg += i.DropDownItems.Count                                    ' Advance to beginning value for the next Group
        Next

        If Menu_SectionsA.DropDownItems.Count > 0 Then                      ' Enable the Menu-Bar level if there is any content
            Menu_SectionsA.Enabled = True
        Else                                                                ' Otherwise leave is disabled, no Sections
            Menu_SectionsA.Enabled = False
        End If

    End Sub
    Public Sub ClearSecMenus()

        ' Purpose:      Deletes all Group and Detail entries from the Sections menu
        ' Process:      For each Group, delete all its children, then delete the Group
        ' Called By:    ResetToNoODF(); EnumerateSectionsSetFont()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.7> First implemented, for dynamic Section Menus

        Const lclProcName As String =           ' Routine's name for message handling
            "ClearSecMenus"

        For Each i As ToolStripMenuItem In Menu_SectionsA.DropDownItems
            i.DropDownItems.Clear()             ' Delete the detail level entries first
        Next
        Menu_SectionsA.DropDownItems.Clear()    ' Then their parent Group entry
        Menu_SectionsA.Enabled = False          ' When done, grey-out the main Menu-Bar entry

    End Sub
    Private Sub CM_XMLOpening(                  ' Right-clicked in XML Row, init the CM Menu
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_XML.Opening

        ' Purpose:      Init CM Menu for XML Row
        ' Process:      Enable Copy item if text is currently selected
        ' Called By:    CM_XML Opening Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_XMLOpening"

        CM_XMLCopy.Enabled = (Rtb_XMLRow.SelectionLength > 0)

    End Sub
    Private Sub CM_XMLCopyClick(                    ' CM Copy or Ctrl-C in XML Row Area
            sender As Object,                       ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_XMLCopy.Click

        ' Purpose:      Handles Click on CM-Copy while in XML Row
        ' Process:      Executes .Copy Method if text is selected
        ' Called By:    CM_CMLCopy Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =                 ' Routine's name for message handling
            "CM_XMLCopyClick"

        If Rtb_XMLRow.SelectionLength > 0 Then      ' Selected Text present: do the Copy
            Rtb_XMLRow.Copy()
        End If

    End Sub
    Private Sub CM_SearchOpening(               ' Right-clicked in Search Box, init CM Menu
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_Search.Opening

        ' Purpose:      Init CM Menu for Search Box
        ' Process:      For Cut/Copy, enable if text is selected; For Paste, if ANSI/Unicode Text is
        '               in the Clipbord; For Undo, undo is enabled.
        ' Called By:    CM_Search Opening Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_SearchOpening"

        CM_SearchUndo.Enabled =                 ' System tells us its OK
            Txt_SearchText.CanUndo
        CM_SearchCopy.Enabled =                 ' Got a selection, Copy is enabled
            (Txt_SearchText.SelectionLength > 0)
        CM_SearchCut.Enabled =                  ' Cut has same criteria as Copy
            CM_SearchCopy.Enabled
        CM_SearchPaste.Enabled =                ' Unicode or ANSI text is present in Clipboard
            (Clipboard.ContainsText() Or
            Clipboard.ContainsText(TextDataFormat.Text))

    End Sub
    Private Sub CM_SearchUndoClick(             ' CM or Ctrl-Z Undo for Search Box
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_SearchUndo.Click

        ' Purpose:      Handles Click on Undo for Search Box
        ' Process:      If system allows, perform .Undo Method
        ' Called By:    CM_SearchUndo Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_SearchUndoClick"

        If Txt_SearchText.CanUndo Then
            Txt_SearchText.Undo()
        End If

    End Sub
    Private Sub CM_SearchCutClick(              ' CM Cut or Ctrl-X in Search Box
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_SearchCut.Click

        ' Purpose:      Handles Click on Cut for Search Box
        ' Process:      If text selected, perform .Cut Method
        ' Called By:    CM_SearchCut Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_SearchCutClick"

        If Txt_SearchText.SelectionLength > 0 Then
            Txt_SearchText.Cut()
        End If

    End Sub
    Private Sub CM_SearchCopyClick(             ' CM Copy or Ctrl-C in Search Box
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_SearchCopy.Click

        ' Purpose:      Handles Click on Copy for Search Box
        ' Process:      If text selected, perform .Copy Method
        ' Called By:    CM_SearchCopy Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_SearchCopyClick"

        If Txt_SearchText.SelectionLength > 0 Then
            Txt_SearchText.Copy()
        End If

    End Sub
    Private Sub CM_SearchPasteClick(            ' CM Paste or Ctrl-V in Search Box
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_SearchPaste.Click

        ' Purpose:      Handles Click on Paste for Search Box
        ' Process:      If acceptable text present in Clipboard, perform .Paste Method
        ' Called By:    CM_SearchPaste Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_SearchPasteClick"

        If (Clipboard.ContainsText() Or
            Clipboard.ContainsText(TextDataFormat.Text)) Then
            Txt_SearchText.Paste()
        End If

    End Sub
    Private Sub CM_DescOpening(                 ' Right-clicked in Descriptive Text Area, init CM Menu
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_Desc.Opening

        ' Purpose:      Init CM Menu for Descriptive Text Are
        ' Process:      For Cut/Copy, enable if text is selected; For Paste, if any Text or Image is
        '               in the Clipbord; For Undo, undo is enabled; Redo, if redo is enabled. Print
        '               if there is content
        ' Called By:    CM_Desc Opening Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_DescOpening"

        CM_DescUndo.Enabled =                   ' System tells us Undo is OK
            Rtb_DescText.CanUndo
        CM_DescRedo.Enabled =                   ' System controls Redo
            Rtb_DescText.CanRedo
        CM_DescCopy.Enabled =                   ' Got a selection, enable Copy
            (Rtb_DescText.SelectionLength > 0)
        CM_DescCut.Enabled =                    ' Cut has same criteria as Copy
            CM_DescCopy.Enabled
        CM_DescPrint.Enabled =                  ' Print enabled if there is text
            (Rtb_DescText.TextLength > 0)
        CM_DescPaste.Enabled =                  ' Paste allowed if Clipboard has any Text or Image content
            (Clipboard.ContainsText() Or
            Clipboard.ContainsText(TextDataFormat.Text) Or
            Clipboard.ContainsText(TextDataFormat.Rtf) Or
            Clipboard.ContainsImage)

    End Sub
    Private Sub CM_DescUndoClick(               ' CM Undo or Ctrl-Z Click in Descriptive Text Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_DescUndo.Click

        ' Purpose:      Handles Click on Undo for Descriptive Text
        ' Process:      If system allows, perform .Undo Method
        ' Called By:    CM_DescUndo Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_DescUndoClick"

        If Rtb_DescText.CanUndo Then
            Rtb_DescText.Undo()
        End If

    End Sub
    Private Sub CM_DescRedoClick(               ' CM Redo or Ctrl-Y Click in Descriptive Text Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_DescRedo.Click

        ' Purpose:      Handles Click on Redo for Descriptive Text Area
        ' Process:      If system allows, perform .Redo Method
        ' Called By:    CM_DeschUndo Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_DescUndoClick"

        If Rtb_DescText.CanRedo Then
            Rtb_DescText.Redo()
        End If

    End Sub
    Private Sub CM_DescCutClick(                ' CM Cut or Ctrl-X Click in Descriptive Text Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_DescCut.Click

        ' Purpose:      Handles Click on Cut for Descriptive Text Area
        ' Process:      If text selected, perform .Cut Method
        ' Called By:    CM_DescCut Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_DescCutClick"

        If Rtb_DescText.SelectionLength > 0 Then
            Rtb_DescText.Cut()
        End If

    End Sub
    Private Sub CM_DescCopyClick(               ' CM Copy or Ctrl-C Click in Descriptive Text Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_DescCopy.Click

        ' Purpose:      Handles Click on Copy for Descriptive Text Area
        ' Process:      If text selected, perform .Copy Method
        ' Called By:    CM_DescCopy Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_DescCopyClick"

        If Rtb_DescText.SelectionLength > 0 Then
            Rtb_DescText.Copy()
        End If

    End Sub
    Private Sub CM_DescPasteClick(              ' CM Paste or Ctrl-V Click in Descriptive Text Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_DescPaste.Click

        ' Purpose:      Handles Click on Paste for Descriptive Text Area
        ' Process:      If there is valid pasteable content in Clipboard, invoke the .Paste Method
        ' Called By:    CM_DescPaste Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =             ' Routine's name for message handling
            "CM_DescPasteClick"

        If (Clipboard.ContainsText() Or         ' Accept Unicode, Ansi, RTF, and Image Data
        Clipboard.ContainsText(TextDataFormat.Text) Or
        Clipboard.ContainsText(TextDataFormat.Rtf) Or
        Clipboard.ContainsImage()) Then
            Rtb_DescText.Paste()
        End If

    End Sub
    Private Sub CM_DescPrintClick(              ' CM Print or Ctrl-P Click in Descriptive Text Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_DescPrint.Click

        ' Purpose:      Handles Click on Print for Descriptive Text Area
        ' Process:      If there is content in the RTB, invoke the .Paste Method
        ' Called By:    CM_DescPaste Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =             ' Routine's name for message handling
            "CM_DescPrintClick"

        If Rtb_DescText.TextLength > 0 Then     ' Something in RTB to print
            PrintDT()                           ' Call common print handler
        End If

    End Sub
    Private Sub CM_ODFOpening(                  ' Right-clicked in ODF Area, init CM Menu
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_ODF.Opening

        ' Purpose:      Init CM Menu for ODF Are
        ' Process:      For Copy, ODF text must be selected; For Undo or Redo, ODF must be
        '               Editable, and operation OKd by the system; For Cut, must be
        '               Editable with text selected. For Paste, must be Editable with
        '               text present in the clipboard. For Enable/Disable-Edit and
        '               Recompute, an ODF must be loaded.
        ' Called By:    CM_ODF Opening Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Const lclProcName As String =           ' Routine's name for message handling
            "CM_ODFOpening"

        CM_ODFUndo.Enabled =                    ' Undo & Redo require Editing enabled and system permission
            (G_EditMode And Rtb_ODF.CanUndo)
        CM_ODFRedo.Enabled =
            (G_EditMode And Rtb_ODF.CanRedo)

        CM_ODFCut.Enabled =                     ' Cut requires Editing enabled and selected text
            (G_EditMode And (Rtb_ODF.SelectionLength > 0))
        CM_ODFCopy.Enabled =                    ' Copy requires selected text
            (Rtb_ODF.SelectionLength > 0)
        CM_ODFPaste.Enabled =                   ' Pasting requires Editing enabled and Text in the clipboard (Unicode or ANSI or RTF)
            (G_EditMode And
            (Clipboard.ContainsText Or Clipboard.ContainsText(TextDataFormat.Text) Or Clipboard.ContainsText(TextDataFormat.Rtf)))

        If G_NoODF Then                         ' Edit Mode changes require an ODF
            CM_ODFEditMode.Enabled = False      ' Set to default, even though disabled
            CM_ODFEditMode.Text = "Enable Editing"
        Else                                    ' Have and ODF
            CM_ODFEditMode.Enabled = True       ' Enable the CM item
            If G_EditMode Then                  ' Set its text to the opposite of the current Edit Mode state
                CM_ODFEditMode.Text = "Disable Editing"
            Else
                CM_ODFEditMode.Text = "Enable Editing"
            End If
        End If

        CM_ODFRecompute.Enabled =               ' Recompute requires an ODF to be loaded
            (Not (G_NoODF) And
            (Rtb_ODF.TextLength > 0))

    End Sub
    Private Sub CM_ODFUndoClick(                ' CM Undo or Ctrl-Z Click in ODF Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_ODFUndo.Click

        ' Purpose:      Handles Click on Undo for ODF Area
        ' Process:      If Editable and system allows, perform .Undo Method
        ' Called By:    CM_ODF Undo Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =             ' Routine's name for message handling
            "CM_ODFUndoClick"

        If G_EditMode And Rtb_ODF.CanUndo Then  ' Undo requires Editing enbaled and system permission
            Rtb_ODF.Undo()
        End If

    End Sub
    Private Sub CM_ODFRedoClick(                ' CM Redo or Ctrl-Y Click in ODF Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_ODFRedo.Click

        ' Purpose:      Handles Click on Redo for ODF Area
        ' Process:      If Editable and system allows, perform .Redo Method
        ' Called By:    CM_ODF Redo Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =             ' Routine's name for message handling
            "CM_ODFRedoClick"

        If G_EditMode And Rtb_ODF.CanRedo Then  ' Redo requires Editing enabled and system permission
            Rtb_ODF.Redo()
        End If

    End Sub
    Private Sub CM_ODFCutClick(                             ' CM Cut or Ctrl-X Click in ODF Area
            sender As Object,                               ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_ODFCut.Click

        ' Purpose:      Handles Click on Cut for ODF Area
        ' Process:      If Editable and selected text is present in ODF, perform .Cut Method
        ' Called By:    CM_ODF Cut Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =                         ' Routine's name for message handling
            "CM_ODFCutClick"

        If G_EditMode And (Rtb_ODF.SelectionLength > 0) Then  ' Cut requires Editing enabled and selected text
            Rtb_ODF.Cut()
        End If

    End Sub
    Private Sub CM_ODFCopyClick(                ' CM Copy or Ctrl-C Click in ODF Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_ODFCopy.Click

        ' Purpose:      Handles Click on Copy for ODF Area
        ' Process:      If selected text is present in ODF, perform .Copy Method
        ' Called By:    CM_ODF Copy Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =             ' Routine's name for message handling
            "CM_ODFCopyClick"

        If (Rtb_ODF.SelectionLength > 0) Then   ' Copy requires selected text
            Rtb_ODF.Copy()
        End If

    End Sub
    Private Sub CM_ODFPasteClick(               ' CM Paste or Ctrl-V Click in ODF Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_ODFPaste.Click

        ' Purpose:      Handles Click on Paste for ODF Area
        ' Process:      If Editable and Text is present in the Clipboard, perform .Paste Method
        ' Called By:    CM_ODF Paste Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =             ' Routine's name for message handling
            "CM_ODFPasteClick"

        If (G_EditMode And                      ' Pating requires Editing enabled, and Unicode/ANSI/RTF text in clipboard
            (Clipboard.ContainsText Or
            Clipboard.ContainsText(TextDataFormat.Text) Or
            Clipboard.ContainsText(TextDataFormat.Rtf))) Then
            Rtb_ODF.Paste()
        End If

    End Sub
    Private Sub CM_ODFEditModeClick(            ' CM Enable/Disable Editing Click in ODF Area
            sender As Object,                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_ODFEditMode.Click

        ' Purpose:      Handles Click on EditMode for ODF Area
        ' Process:      If ODF is loaded, call ...
        ' Called By:    CM_ODF EditMode Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =             ' Routine's name for message handling
            "CM_ODFEditmodeClick"

        If Not (G_NoODF) Then
            If CM_ODFEditMode.Text = "Enable Editing" Then
                CM_ODFEditMode.Text = "Disable Editing"
                EnableEditing()
            Else
                CM_ODFEditMode.Text = "Enable Editing"
                DisableEditing()
            End If
        End If

    End Sub
    Private Sub CM_ODFRecomputeClick(                           ' CM Recompute Click in ODF Area
            sender As Object,                                   ' Standard Event Parameters for a Control
            e As EventArgs
            ) Handles CM_ODFRecompute.Click

        ' Purpose:      Handles Click on Recompute for ODF Area
        ' Process:      If ODF is loaded, call EnumerateSectionsSetFont()
        ' Called By:    CM_ODF Recompute Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.8> First implementation

        Dim lclProcName As String =                             ' Routine's name for message handling
            "CM_ODFRecomputeClick"

        If (Not (G_NoODF) And (Rtb_ODF.TextLength > 0)) Then
            EnumerateSectionsSetFont(Rtb_ODF,                   ' Operate on the ODF
                                     Num_ODFFontSize.Value,     ' Maintain present font-size
                                     G_NoODF,                   ' Have ODF, this is False
                                     True,                      ' List Sections in the Descriptive Text Area
                                     True)                      ' Ensure all Titles are emphasize correctly (there may be new Sections after an edit!)
        End If

    End Sub

End Class