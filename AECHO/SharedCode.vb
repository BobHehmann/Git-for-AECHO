Imports System.IO
Imports System.Windows.Forms
Imports System.Math

Module SharedCode

    ' Purpose:      Declarations and initialization of Globals; repository for general routines
    ' Notes:        As time permits, relocate Subroutines and Functions from the Main Form to here.
    ' Updates:      <1.060.2> Created module. Moved delcaration and initialization of Global
    '                   Constants and Variables from the Main Form to here. Started populating
    '                   with supporting routines. Coding new routines here.

    ' Global Constants      <1.060.2> Moved many Global Constant declarations from Main Form to here, and
    '                       narrowed their scope from "Public" to "Friend".
    '                       <1.060.2> Minor changes to optimize Printing

    Friend Const conHTMLHelpDir As String = "AECHOHelpFiles"                ' <1.060.2> Location of HTML Help Files with \DATA dir
    Friend Const conHTMLHelpFile As String = "AECHOMain.HTM"                ' <1.060.2> Top level HTML Help File
    Friend Const conRTFHelpFile As String = "help.rtf"                      ' <1.060.2> Original style Help File, fallback

    Friend Const conMsgCrit As MsgBoxStyle = MsgBoxStyle.Critical           ' <1.060.2> These three message criticality levels are used
    Friend Const conMsgExcl As MsgBoxStyle = MsgBoxStyle.Exclamation
    Friend Const conMsgInfo As MsgBoxStyle = MsgBoxStyle.Information

    Friend Const conQuickNoH As RichTextBoxFinds =                          ' <1.060.2> Fastest search, do not highlight found text
            RichTextBoxFinds.NoHighlight
    Friend Const conSrchRev As RichTextBoxFinds = RichTextBoxFinds.Reverse  ' <1.060.2> Reverse search

    Friend Const conPanelTitle As String = "XML Tags in the Current Row"    ' <1.060.2> Various Title Strings, displayed as needed
    Friend Const conTextBoxTitle_Def As String = "Descriptive Information"
    Friend Const conTextBoxTitle_ODFLoad As String = "Section Locations"
    Friend Const conTextBoxTitle_Desc As String = "Section Field/Tag Descriptions"
    Friend Const conTextBoxTitle2_Desc As String = " Tag       Expanded Tag Name"
    Friend Const conTextBoxTitle_Help As String = "AECHO Help Text"
    Friend Const conTextBoxTitle_List As String = "  Sec ID.    Start-Line               End-Line                 Start-Char                      End-Char              Section Name"
    Friend Const conMainTitle As String = "AECHO: Hauptwerk Organ Analyzer, Version "

    Friend Const conDefSectionName As String = "None"
    Friend Const conTraceSample As String = "Trace Sample"                  ' <1.060.2> Button Text when in the Sample Section
    Friend Const conDisplayImage As String = "Display Image"                ' <1.060.2> Button Text when Row references a displayable Image
    Friend Const conLeftMargin As Integer = 14                              ' <1.060.2> Leftmost allowable display margin with Main Form, except for Menu & Status Strips

    Friend Const conSecStartTag As String = "<ObjectList ObjectType="       ' <1.060.2> Moved to Global, renamed. Identifies Start-Tag text unique to beginning of a Section in ODF
    Friend Const conSecEndTag As String = "</ObjectList>"                   ' <1.060.2> Moved to Global, renamed. The End-Tag marker for a Section
    Friend Const conRowStartTag As String = "<o>"                           ' <1.060.2> Start and End-Tag markers for a Row
    Friend Const conRowEndTag As String = "</o>"
    Friend Const conAStartTag As String = "<a>"                             ' <1.060.2> Start/End-Tag markers for an "a" Element
    Friend Const conAEndTag As String = "</a>"
    Friend Const conCStartTag As String = "<c>"                             ' <1.060.2> Start/End-Tag markers for a "c" Element
    Friend Const conCEndTag As String = "</c>"

    Friend Const conImageSet As String = "ImageSet"                         ' <1.060.2> To display an Image File, we may need to find this Section by name
    Friend Const conImageSetElement As String = "ImageSetElement"
    Friend Const conSample As String = "Sample"                             ' <1.060.2> When we need to find the Sample Section e.g. in Trace-a-Sample
    Friend Const conPipe01Attack As String = "Pipe_SoundEngine01_AttackSample"
    Friend Const conPipe01Release As String = "Pipe_SoundEngine01_ReleaseSample"
    Friend Const conTremWave As String = "TremulantWaveForm"
    Friend Const conReqInstPckg As String = "RequiredInstallationPackage"

    Friend Const conIntFmt As String = "###,##0"                            ' <1.060.2> Standard format for displayed integers: thousands demarcation, no leading zeros
    Friend Const conFont As String = "Arial"                                ' <1.060.2> Change standard ODF font to Arial, scales better than ms sans
    Friend Const conDefODFFontSize As Integer = 10                          ' <1.060.2> Size of ODF in points - initializes ODF Font Size Control
    Friend Const conDescFont As String = "Verdana"                          ' <1.060.2> Font for the Descriptive Text Box
    Friend conTitleColor As Color = Color.Blue                              ' <1.060.2> Color to apply to Section Titles in the ODF, when emphasizing their display
    Friend Const conTitleFontInc As Integer = 1                             ' <1.060.2> # of points to increase an emphasized Section Title, over the present display default

    Friend Const conRowTypeODFStart As String = "ODF Start-Tag"             ' <1.060.2> The constants are the recognized Line/Row types, displayed in the Status-Bar
    Friend Const conRowTypeODFEnd As String = "ODF End-Tag"
    Friend Const conRowTypeXMSHdr As String = "XML Header"
    Friend Const conRowTypeSecStart As String = "Sec Start-Tag"
    Friend Const conRowTypeSecEnd As String = "Sec End-Tag"
    Friend Const conRowTypeGenData As String = "Gen Sec Data"
    Friend Const conRowTypeRecord As String = "Record"

    Friend Const conFieldSID As String = "SampleID"                                         ' <1.060.2> Sample's "<a>" Field'; Pipe-Attack's <c> Field
    Friend Const conFieldIPID As String = "InstallationPackageID"                           ' <1.060.2> Sample's "<b>" Field
    Friend Const conFieldFName As String = "SampleFilename"                                 ' <1.060.2> Sample's "<c>" Field
    Friend Const conFieldPSpec As String = "Pitch_SpecificationMethodCode"                  ' <1.060.2> Sample's "<d>" Field
    Friend Const conFieldPRank As String = "Pitch_RankBasePitch64ftHarmonicNum"             ' <1.060.2> Sample's "<e>" Field
    Friend Const conFieldPNorm As String = "Pitch_NormalMIDINoteNumber"                     ' <1.060.2> Sample's "<f>" Field
    Friend Const conFieldPEx As String = "Pitch_ExactSamplePitch"                           ' <1.060.2> Sample's "<g>" Field
    Friend Const conFieldLic As String = "LicenseSerialNumRequiredForSampleFile"            ' <1.060.2> Sample's "<h>" Field
    Friend Const conFieldUID As String = "UniqueID"                                         ' <1.060.2> Pipe-Attack & Release Sections' <a> Field
    Friend Const conFieldLayID As String = "LayerID"                                        ' <1.060.2> Pipe-Attack & Release Sections' <b> Field
    Friend Const conFieldLSRSPTC As String = "LoadSampleRange_StartPositionTypeCode"        ' <1.060.2> Pipe-Attack & Release Sections' <d> Field
    Friend Const conFieldLSRSPV As String = "LoadSampleRange_StartPositionValue"            ' <1.060.2> Pipe-Attack & Release Sections' <e> Field
    Friend Const conFieldLSREPTC As String = "LoadSampleRange_EndPositionTypeCode"          ' <1.060.2> Pipe-Attack & Release Sections' <f> Field
    Friend Const conFieldLSREPV As String = "LoadSampleRange_EndPositionValue"              ' <1.060.2> Pipe-Attack & Release Sections' <g> Field
    Friend Const conFieldASCHV As String = "AttackSelCriteria_HighestVelocity"              ' <1.060.2> Pipe-Attack & Release Sections' <h> Field
    Friend Const conFieldASCMTS As String = "AttackSelCriteria_MinTimeSincePrevPipeCloseMs" ' <1.060.2> Pipe-Attack & Release Sections' <i> Field
    Friend Const conFieldASCHCC As String = "AttackSelCriteria_HighestCtsCtrlValue"         ' <1.060.2> Pipe-Attack & Release Sections' <j> Field
    Friend Const conFieldLCL As String = "LoopCrossfadeLengthInSrcSampleMs"                 ' <1.060.2> Pipe-Attack's <k> Field; TremulantWaveform's <f> Field
    Friend Const conFieldSAA As String = "ScaleAmplitudeAutomatically"                      ' <1.060.2> Pipe-Release's <k> Field
    Friend Const conFieldDBA As String =
        "DontBypassAmplitudeScalingIfUserDisablesMultipleReleases"                          ' <1.060.2> Pipe-Release's <l> Field
    Friend Const conFieldPAA As String = "PhaseAlignAutomatically"                          ' <1.060.2> Pipe-Release's <m> Field
    Friend Const conFieldRCL As String = "ReleaseCrossfadeLengthMS"                         ' <1.060.2> Pipe-Release's <n> Field
    Friend Const conFieldRSCHV As String = "ReleaseSelCriteria_HighestVelocity"             ' <1.060.2> Pipe-Release's <p> Field
    Friend Const conFieldRSCLKR As String = "ReleaseSelCriteria_LatestKeyReleaseTimeMs"     ' <1.060.2> Pipe-Release's <r> Field
    Friend Const conFieldRSCHCC As String = "ReleaseSelCriteria_HighestCtsCtrlValue"        ' < 1.06.2> Pipe-Release's <r> Field
    Friend Const conFieldRSCPTR As String = "ReleaseSelCriteria_PreferThisRelForAttackID"   ' <1.060.2> Pipe-Release's <s> Field
    Friend Const conFieldTWID As String = "TremulantWaveformID"                             ' <1.060.2> TremulantWaveform's <a> Field
    Friend Const conFieldTID As String = "TremulantID"                                      ' <1.060.2> TremulantWaveform's <c> Field
    Friend Const conFieldPAF As String = "PitchAndFundamentalWaveformSampleID"              ' <1.060.2> TremulantWaveform's <d> Field
    Friend Const conFieldTHW As String = "ThirdHarmonicWaveformSampleID"                    ' <1.060.2> TremulantWaveform's <e> Field
    Friend Const conFieldPOC As String = "PitchOutputContinuousControlID"                   ' <1.060.2> TremulantWaveform's <g> Field
    Friend Const conFieldInPkId As String = "InstallationPackageID"                         ' <1.060.2> RequiredInstallationPackage's <a> Field
    Friend Const conFieldName As String = "Name"                                            ' <1.060.2> RequiredInstallationPackage's <b> Field
    Friend Const conFieldSName As String = "ShortName"                                      ' <1.060.2> RequiredInstallationPackage's <c> Field
    Friend Const conFieldPSID As String = "PackageSupplierID"                               ' <1.060.2> RequiredInstallationPackage's <d> Field
    Friend Const conFieldSupName As String = "SupplierName"                                 ' <1.060.2> RequiredInstallationPackage's <e> Field
    Friend Const conFieldMPV As String = "MinimumPackageVersion"                            ' <1.060.2> RequiredInstallationPackage's <f> Field

    '   <1.060.2> Shortened to just the Attribute Value, then eliminated these constants entirely, their values are now in their respective Menu Item's .Tag Properties
    'Friend Const section01 As String = "DisplayPage"

    ' Structure Definitions                 <1.060.2> Relocated to here from Main Form, narrowed naming scope from Public to Friend
    '                                       Deleted field "index", superfluous.
    Friend Structure Str_Section            ' The structure of an ODF Section, built when a Section is located by name
        Friend name As String               ' rien que le nom; Section Name, without the XML overhead e.g. 'TextInstance' (without the single-quotes)
        Friend startPos As Integer          ' Index within Rtb_ODF to the starting character of this Section, opening "<" of "<ObjectList...>"
        Friend endPos As Integer            ' Index within RTB_ODF to the last character of this Section, the CrLF following the closing ">" of "</ObjectList>"
        Friend titleLen As Integer          ' Length of Section Element, from Start-Tag "<" to End-Tag">"
        Friend firstRowStart As Integer     ' debut de la premiere ligne; index of opening "<" of "<o>..." of first content Row. If Empty, then =-1, else = Title-End+1 <1.060.2> renamed from firstLineStartPos
        Friend firstRowLen As Integer       ' <1.060.2> Length of first data ("<o>...</o>") Row, if it exists. Set to 0 if it doesn't, replacing isEmpty Property
    End Structure

    ' Global Variables                      <1.060.2> Relocated here from Main Form, narrowed naming scope from Public to Friend

    Friend fnt_Fname As Font                ' Font types useful for the Trace display: File Name
    Friend fnt_Title As Font                ' Top-of-display Title
    Friend fnt_Section As Font              ' Other Section Headers
    Friend fnt_Fields As Font               ' Field Lines

    Friend G_Registered As Boolean          ' version payante ou demo; set to True if copy of AECHO is registered (now Freeware, always set True at initialization
    Friend G_EditMode As Boolean            ' true si actif; True if Edit Mode is active - allows modifications to ODF copy in Rtb_ODF
    Friend G_ODFModSinceSaved As Boolean    ' <1.060.2> False when freshly loaded, set to True when modified. Used to determine if we should prompt to save modified ODF.
    Friend G_NoODF As Boolean = True        ' True -> No ODF loaded; False -> ODF in memory, Section Table is loaded

    Friend G_LineIndex As Integer           ' Rtb_ODF Line Number of the present / selected line

    Friend G_SectionName As String          ' The standard name for the ODF Section we are presently positioned at

    Friend G_DataPath As String             ' repertoire data; location of the local \DATA directory, presently \DATA at the top of AppPath: contains all the static .rtf files
    Friend G_HelpFilePath As String         ' <1.060.2> Path to HTML Help Files: currently \DATA\AECHOHelpFiles: inside the DataPath
    Friend G_ODFLibPath As String           ' Def Dir when locating an ODF file. From prior use, or \DATA\initial.txt, or hardcoded default. <1.060.2> changed var name from G_InitialDir
    Friend G_OrganFile As String            ' Complete path/filename of the current ODF
    Friend G_PackagePath As String          ' HauptwerkSampleSetsAndComponents\OrganInstallationPackages

    Friend G_PreviousRTFFile As String      ' Path & filename of the previously loaded .rtf file, if any. Used to avoid reloading the same file over and over.

    '       Images                            Supports the handling of Image Display, which temporarily reuses screen regions dedicated to Descriptove Text display.
    Friend G_ImageFile As String            ' Path and filename of an image file to be loaded and displayed.
    Friend G_PackageID As String            ' numero à 6 chiffres du dossier d'installation; Package (i.e. Organ) unique ID: 6 chars, leading zeroes - "000001" is St. Anne's
    Friend G_ImageSet As String             ' When parsing an ImageSetElement record, this is the number i.e. ImageSetIndex that links to the parent ImageSet.
    Friend G_SampleID As String             ' <1.060.2> When parsing a Sample record, this is the SampleID ("<a>" Tag value) of the selected Sample
    Friend G_InitTagPanelWidth As Integer   ' <1.060.2> Starting width of the Tags Panel, to restore original size when cancelling an Image Display
    Friend G_MinPanelHeights As Integer     ' <1.060.2> Height of the Tags Panel and Descriptive Text box: these are the same now. Prep for dynamic resizing.

    Friend Sub DispMsg(caller As String,            ' Calling procedure's name (self-supplied)
                       severity As MsgBoxStyle,     ' Severity of error, using standard VB levels e.g. .Critical
                       txt As String)               ' Text to display

        ' Purpose:      Common message processor for AECHO. Displays calling routine's name,
        '               severity, and supplied message text.
        ' Process:		If caller is blank and severity Informational, treat as a simple user interaction,
        '               such as a warning about an invalid input. Otherwise treat as an internal
        '               error message.
        ' Called By:    Various
        ' Side Effects: Modal message box display.
        ' Notes:        Basic stub just displays the content. May implement more sophisticated error
        '               and exception handling in the future.
        ' Updates:		<1.060.2> First implementation

        Dim introMessage As String                  ' Different introductions if caller is unknown or known

        If caller = "" And
            severity = MsgBoxStyle.Information Then ' Special case for caller is blank + informational severity: simple user interaction
            MsgBox(txt, severity, "User Message From AECHO")
            Return
        End If

        If caller = "" Then                         ' Unknown caller, not a normal user interaction
            introMessage = "Calling routine unknown:"
        Else                                        ' Known caller, severity irrelevant
            introMessage = "Message from routine: " & caller
        End If

        MsgBox(introMessage & vbCrLf &
                txt, severity, "Internal AECHO Message")

    End Sub
    Friend Function IsRegistered(dataPath As String,            ' Path to \DATA directory
                                 licenseFName As String         ' <1.060.2> Name of license file to look for
                                 ) As Boolean                   ' Savoir si version enregistre ou non; modifié pour passer en freeware à partir de v1-0-57

        ' Purpose:      Determine if the application is Registered (paid-for) or not.
        ' Process:      As of V1.057, became Freeware, so always return True i.e. "Registered". Previously,
        '               looked for presence of a license file.
        ' Called By:    MAIN_Load(), once upon application start
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> Changed to function, to minimize side-effect changes to global variables. Changed
        '               path building to use more robust Path.Combine, to be OS neutral. Moved action code (disabling
        '               of feature-set when not registered) to be the caller's responsibility. Pass path as parm.
        '               Relocated from MAIN form to here. Passed License File Name in as parm from caller. Renamed from
        '               RegisteredUnregistered() to IsRegistered().

        Const lclProcName As String = "IsRegistered"            ' <1.060.2> Function's name for message handling

        Dim verpeaux_File As String                             ' <1.060.2> Path to key file
        Dim reponse As MsgBoxResult                             ' <1.060.2>

        Return (True)                                           ' As of V1.057, always treat as Registered / Freeware

        If dataPath = "" Then Return (False)                    ' If null, return False. Need guard check, as Path.Combine can error if element is null.

        verpeaux_File = Path.Combine(dataPath, licenseFName)    ' re V1.057 detection code begins here... <1.060.2> use Path.Combine() to create valid path for any OS
        If Not File.Exists(verpeaux_File) Then                  ' <1.060.2> Changed Method to File.Exists
            Return (False)                                      ' me donner le choix; detect existance of license file - if not found, we are unregistered
        End If
        reponse = MsgBox("Veux-tu émuler la version demo ?",
                         MsgBoxStyle.YesNo,
                         "OPTION PERSO (vpx_file présent)")
        If reponse = vbYes Then Return (False)                  ' Is registered, ask user if code should emulate non-registered: If Yes, return False

        Return (True)                                           ' Proceed as Registered

    End Function
    Friend Function CenterLbl(ByRef item As Control,    ' Control with Text, to center between margins
                              leftMargin As Integer,    ' Left Margin of centering region
                              rightMargin As Integer    ' Right Margin of centering region
                              ) As Boolean              ' True -> Positioned Control; False -> Error, did not position

        ' Purpose:      Center a variable-width object (normally a Label Control containing text) in
        '               the region between two margins. Note that positioning is relative to the MAIN
        '               form, not a parent container.
        ' Process:		Check various boundary conditions, display error message if request is ill-formed.
        '               Returns False if validity checks fail and no action is performed. If text is too wide
        '               to fit between the margins, display a warning message, but proceed to center it anyway.
        ' Called By:    MAIN_Load(); CenterText()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:		<1.060.2> First implementation

        Const lclProcName As String = "CenterLbl"       ' <1.060.2> Function's name for DispMsg calls

        If (leftMargin < conLeftMargin) Or (rightMargin > MAIN.Right) Then
            DispMsg(lclProcName, conMsgExcl,
                    "Requested Margin is out-of bounds" & vbCrLf &
                    " Left Min: " & conLeftMargin & vbCrLf &
                    " Right Max: " & MAIN.Right & vbCrLf &
                    " Left Requested: " & leftMargin & vbCrLf &
                    " Right Requested: " & rightMargin & vbCr)
            Return False
        End If
        If (leftMargin >= rightMargin) Then
            DispMsg(lclProcName, conMsgExcl,
                    "Requested Left Margin must be less than the Right Margin" & vbCrLf &
                    " Left Margin: " & leftMargin & vbCrLf &
                    " Right Margin: " & rightMargin)
            Return False
        End If
        If (item.Width >= (rightMargin - leftMargin) - 1) Then
            DispMsg(lclProcName, conMsgExcl,
                    "Text is too long to fit within the requested margins" & vbCrLf &
                    "Will still attempt to display text." & vbCrLf &
                    " Left Margin: " & leftMargin & vbCrLf &
                    " Right Margin: " & rightMargin & vbCrLf &
                    " Text Width: " & item.Width & vbCrLf &
                    " Text: " & item.Text & vbCrLf)
            item.Left = leftMargin + (0.5 * (rightMargin - leftMargin))
            Return True
        End If

        item.Left = leftMargin + (0.5 * ((rightMargin - leftMargin) - item.Width))
        Return True

    End Function
    Friend Function CenterText(txtToCenter As String,       ' Text String to place into the Control [field]
                               ByRef field As Control,      ' Control to receive txtToCenter, and then be centered
                               leftMargin As Integer,       ' Left margin of the region within which to center the text
                               rightMargin As Integer       ' Right margin of the centering region
                               ) As Boolean                 ' -> Assigned text and centered; Fales -> Error, did not position

        ' Purpose:      Place text into a Control's (usually a Label) text, then center it between margins.
        ' Process:		Save the Control's original text, insert the new, and call CenterLbl() to position it.
        '               If CenterLbl() fails, restore the original text, and return failure.
        ' Called By:    ResetToNoODF(); LoadRTFFile(); ParseSections(); Menu_Help_Click(); TakeRowAction()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:		<1.060.2> First implementation

        Const lclProcName As String = "CenterText"          ' Function's name for message handling

        Dim oldText As String                               ' Control's original Text, to be restored if centering the new text fails

        oldText = field.Text                                ' Save current text
        field.Text = txtToCenter
        If CenterLbl(field, leftMargin, rightMargin) Then
            Return True                                     ' Succeeded, just return
        Else                                                ' Centering operation returned an error, restore original text
            field.Text = oldText
            Return False
        End If

    End Function
    Friend Function GetDataPath(appPath As String,              ' Path to Application Executable
                                dataFileName As String          ' Name of Data Directory, usually "\DATA"
                                ) As String                     ' Path to AECHO's data (Section descriptive .rtf files...)

        ' Purpose:      Locate the path to AECHO's data (\DATA) directory
        ' Process:		Validate parameters. Then see if it is located adjacent to the Application executable. If
        '               so, return that path. If not, perhaps AECHO is an installed application. Retrieve the full
        '               OS path to an application data directory, try trimming off excess couple of embedded
        '               directory levels, append the data directory suffix, and see if that directory exists. If so,
        '               return that path. If all else fails, signal an error, and proceed without access to data.
        ' Called By:    MAIN_Load
        ' Side Effects: <None>
        ' Notes:        Future - use Properties to save last known good path, rather than initialdir.txt
        ' Updates:      <1.060.2> Moved inline logic from MAIN form to here, implementing as a Function, to reduce
        '               excessive logical nesting and clarify the code. Added Guard Clauses, and Try/Catch error handling.

        Const lclProcName As String = "GetDataPath"                 ' <1.060.2> Function's name for DispMsg calls
        Const lclWarning As String = "AECHO will still run, but will be unable to access Section Descriptions or the Help file."

        Dim workingDataDir As String = ""                           ' <1.060.1> Interim var while finding path to \DATA
        Dim trimDir As String                                       ' <1.060.1> Assigned "ProdName\ProdVer", which is then truncated off the right end of the path
        Dim idx As Integer                                          ' <1.060.1> String-search index

        If appPath = "" Then                                        ' <1.060.2> Guard Code, validate parameters: must not be null
            DispMsg(lclProcName, conMsgCrit,
                    "appPath is Null. " & lclWarning)
            Return ""
        End If
        If dataFileName = "" Then
            DispMsg(lclProcName, conMsgCrit,
                    "dataFileName is Null. " & lclWarning)
            Return ""
        End If

        Try                                                         ' <1.060.2> Added Try/Catch, to handle general exception beyond the Guard-clauses
            workingDataDir = Path.Combine(appPath, dataFileName)    ' <1.060.1> Construct the first attempt, see if data directory is adjacent to the AECHO executable
            If Directory.Exists(workingDataDir) Then                ' Is \DATA next to executable? If so, then AECHO is not an installed application, and we are good
                Return workingDataDir                               ' workingDataDir points to non-installed data directory, we're done...
            End If
            '                                                         Possibly an installed app, DATA will be in AppData\Roaming\AECHO\DATA
            workingDataDir = Application.UserAppDataPath            ' Retrieve the full, version-specific data path, we'll need to truncate it back to \Roaming\AECHO
            trimDir = Path.Combine(Application.ProductName,
                               Application.ProductVersion)          ' Construct e.g. "AECHO\1.60...", (using run-time values), which we need to trim off
            idx = Strings.InStr(workingDataDir, trimDir,
                            CompareMethod.Text)                     ' Find this excess trailer...
            If idx > 1 Then                                         ' We found the trailer
                workingDataDir = Path.Combine(Strings.Left(workingDataDir, idx - 1),
                                          dataFileName)             ' Truncate the trailer, leaving "\Roaming\AECHO\", then append data directory suffix
                If Directory.Exists(workingDataDir) Then            ' Good, we found it in the standard "Installed Application" location, go with it
                    Return workingDataDir                           ' workingDataDir now points to the data directory, we're done...
                End If
            End If
            '                                                         we get here if we fail at any point, present a warning message
            DispMsg(lclProcName, conMsgExcl,
                    "Cannot locate the \DATA directory. " & lclWarning & vbCrLf &
                    "appPath: " & appPath & vbCrLf &
                    "dataFileName: " & dataFileName & vbCrLf &
                    "workingDataDir: " & workingDataDir)
            Return ""                                               ' Error return

        Catch ex As Exception                                       ' Catch general exceptions here
            DispMsg(lclProcName, conMsgCrit,
                    "General Exception, " & lclWarning & vbCrLf &
                    "appPath: " & appPath & vbCrLf &
                    "dataFileName: " & dataFileName & vbCrLf &
                    "workingDataDir: " & workingDataDir &
                    "Exception Code is: " & vbCrLf & ex.ToString)
            Return ""                                               ' Exception error return
        End Try

    End Function
    Friend Function GetODFLibPath(dataPath As String,               ' Path to \DATA directory
                                  initFileName As String            ' Filename of initialization file
                                  ) As String                       ' Possible path to HW ODF files
        ' Dir par defaut

        ' Purpose:      Return the (presumed) directory for HW's ODF Library
        ' Process:		Check if initialization file exists, if so, read the path to the ODF Lbrary
        '               from it, otherwise use the hardcoded default path.
        ' Called By:    MAIN_Load
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> Converted to a Function, rather than setting a global directly; use a constant
        '               for default path; renamed function from ReadInitialDir(); use Path.Combine() to create an
        '               OS-independant file path; relocated from MAIN form to here. Messaging through DispMsg.     

        Const lclProcName As String = "GetODFLibPath"               ' <1.060.2> Function's name for DispMsg calls
        Const conDefODFPath As String = "D:\Hauptwerk\HauptwerkSampleSetsAndComponents\OrganDefinitions"

        Dim sr As StreamReader                                      ' Stream to use to read initialdir.txt file
        Dim retValue As String                                      ' <1.060.2> Working var for Path to ODF library
        Dim initialdirFName As String                               ' <1.060.2> Working var for Path to InitialDir.txt

        If dataPath = "" Then                                       ' <1.060.2> Test guard conditions for parameter validity
            DispMsg(lclProcName, conMsgExcl,
                    "DataPath is null, will use default ODF Library location")
            Return conDefODFPath
        End If
        If initFileName = "" Then
            DispMsg(lclProcName, conMsgExcl,
                    "Init Filename is null, will use default ODF Library location")
            Return conDefODFPath
        End If

        initialdirFName = Path.Combine(dataPath, initFileName)      ' Create path to InitialDir.txt; <1.060.2> Changed literal string to constant
        retValue = conDefODFPath                                    ' Default to hard-coded path, to be used when InitialDir.txt can't be read
        Try                                                         ' Absorb exceptions, targets "File not found..."
            If File.Exists(initialdirFName) Then                    ' Did we find InitialDir.txt?
                sr = New StreamReader(initialdirFName)              ' Open InitialDir.txt, read its record, close
                retValue = sr.ReadLine
                sr.Close()
            End If
        Catch                                                       ' Remain silent about any errors
        End Try

        Return retValue

    End Function
    Friend Function GetPackagePath(fnameODF As String       ' Fully qualified path\filename of ODF file that was just loaded
                                   ) As String              ' Path to the Installation Packages

        ' trouver le path pour OrganInstallationPackages à partir de G_OrganFile

        ' Purpose:      Build the Organ Installation Packages directory path, a peer of the Organ
        '               Definitions directory.
        ' Process:		Starting with the supplied path (including filename) of the ODF file, replace the
        '               ending "\OrganDefinitions\filenamexxx" with "\OrganInstallationPackages"
        ' Example:      [fnameODF] =    "C:\Hauptwerk\OrganDefinitions\MyOrgan.Organ_Hauptwerk_xml" at entry
        '               Func Return =   "C:\Hauptwerk\OrganInstallationPackages"
        ' Called By:    Menu_OpenHauptwerkOrgan_Click()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> Moved here from MAIN form, converting to a function to avoid side effect.
        '               Improve error handling. Simplified path/file parsing, made OS independant.

        Const lclProcName As String = "GetPackagePath"      ' <1.060.2> Function's Name for messaging

        Const organDefName As String = "OrganDefinitions"   ' <1.060.2> Directory Participle we want to find and remove
        Const instPackSubDir As String =
            "OrganInstallationPackages"                     ' <1.060.2> Final Directory Participle to add

        Dim idx As Integer                                  ' Index to last "OrganDefinitions" directory in [fnameODF]
        Dim organDefPath As String                          ' <1.060.2> Build our default path here "\[organDefName]\"
        Dim newPath As String                               ' <1.060.2> Build our return value in newPath

        organDefPath = Path.DirectorySeparatorChar &
            organDefName &
            Path.DirectorySeparatorChar                     ' <1.060.2> Surround dirname with OS-independant separators
        idx = fnameODF.LastIndexOf(organDefPath,            ' <1.060.2> LastIndexOf Method is 0 based, not VB's normal 1 based - find last instance
                                   fnameODF.Length - 1)     ' <1.060.2> Find last instance of OrganDefinitions Directory
        If idx < 0 Then
            DispMsg(lclProcName, conMsgCrit,
                    "Unable to locate Organ Definitions Directory" & vbCrLf &
                    "Some functions will fail.")            ' <1.060.2> Never found organDefPath
            Return ""
        End If

        newPath = fnameODF.Remove(idx)                      ' <1.060.2> Truncate OrganDefs & Filename from path
        newPath = Path.Combine(newPath, instPackSubDir)     ' <1.060.2> Add suffix, and we're done
        If Directory.Exists(newPath) Then                   ' <1.060.2> Final check, see if directory exists
            Return newPath                                  ' <1.060.2> It does, let's go with it...
        End If

        DispMsg(lclProcName, conMsgCrit,                    ' <1.060.2> Oopsie, no such dir, message and return empty string
                "Unable to locate Organ Definitions Directory" & vbCrLf &
                "Some functions will fail.")
        Return ""

    End Function
    Friend Sub TagsPanelVisible(visible As Boolean)    ' VIDE LE PANNEL AFFICHANT LES TAGS

        ' Purpose:      Hide or show all the controls in the panel used to display Element Names and Contents.
        ' Process:		Let runtime loop through all controls, while we set their .Visible propety.
        ' Called By:    ResetToNoODF(); ParseSections(); PositionToSectionByName(); DisplayXMLRow()
        ' Side Effects: Changes property values for each Control in MAIN.Pnl_Tags (24xLabelTag, 24xtag)
        ' Notes:        <None>
        ' Updates:      <1.060.2> Relocated here from Main; changed scope from "Private" to "Friend"; added
        '               object reference to "MAIN" to Pnl.Tags due to relocation. Renamed from ClearTagsPanel().
        '               Added parameter, to consolidate function of ShowTagsPanel().

        Const lclProcName As String =
            "TagsPanelVisible"                      ' <1.060.2> Routine's name for message handling

        Dim tags As Control                         ' Object definition for loop

        For Each tags In MAIN.Pnl_Tags.Controls     ' <1.060.2> Added "MAIN" ref; cycle through collection of all controls in Tags Panel
            tags.Visible = visible                  ' Make it Visible or Hide it
        Next

    End Sub
    Friend Sub ResetToNoODF()

        ' Purpose:      Initialize Menu's, Variables, and Controls to default, nothing loaded state
        ' Process:		Explicitly set each thing we are interested in, to its "freshly loaded" state.
        ' Called By:    MAIN_Load(); Menu_OpenHauptwerkOrgan_Click(); Menu_CloseODF_Click
        ' Side Effects: Updates various Global Variables, Controls, Fields, and Menus.
        ' Notes:        <None>
        ' Updates:      <1.060.2> Initial version of this routine.
        '               <1.060.4> Add Line & Char counts to the Status-Bar
        '               <1.060.5> Remove call to RemoveImage(), made Titles visible here
        '               <1.060.6> Init Display Panel position field's Tags to "NA"

        Const lclProcName As String = "ResetToNoODF"        ' Routine's name for message handling

        Dim idx As Integer                                  ' Index to loop over Sections table

        G_EditMode = False                                  ' Set all useful Global Vars to 0/Empty: NEVER initialize global App File Paths here!
        G_ODFModSinceSaved = False
        G_NoODF = True
        G_LineIndex = 0
        G_PackagePath = "" : G_PackageID = ""
        G_SectionName = ""
        G_PreviousRTFFile = ""
        G_ImageFile = "" : G_ImageSet = ""

        TagsPanelVisible(False)                             ' Hide all controls in the Tags Panel
        ClearMarkers()                                      ' Reset Markers

        With MAIN                                           ' Need MAIN reference to access these Controls
            .Text = conMainTitle &                          ' Reset Title Bar to basic program name/info - we'll add the ODF filename when we've got one open
                My.Application.Info.Version.ToString
            .Rtb_ODF.Clear()                                ' Clear main ODF display
            .Rtb_ODF.Font = New Font(conFont,               ' Set entire Rtb_ODF to default (Arial), selected font-size, Regular stlye
                                     conDefODFFontSize,
                                     FontStyle.Regular)
            .Rtb_ODF.ReadOnly = True                        ' Return ODF to non-editable
            .Rtb_ODF.BackColor = Color.WhiteSmoke           ' Reset to slightly darker background
            .Rtb_XMLRow.Clear()                             ' Clear Single Row display
            .Rtb_DescText.Clear()                           ' Clear Descriptive Text/Help Text
            .Lbl_TextBoxTitle1.Text = ""                    ' Clear Top Title Field
            .Lbl_TextBoxTitle1.Visible = True               ' <1.060.5> Make sure it is visible (this use to be in now-deprecated RemoveImage()
            CenterText(conTextBoxTitle_Def,                 ' Set/Restore & Center Descriptive Title Text to its default in the Bottom Title field
                       .Lbl_TextBoxTitle2,
                       .Rtb_DescText.Left,
                       .Rtb_DescText.Right)
            .Lbl_TextBoxTitle2.Visible = True               ' <1.060.5> Make sure it is visible (this use to be in now-deprecated RemoveImage()
            .Txt_SearchText.Clear()                         ' Clear Search Box Text
            .Lbl_SecStartVal.Text = "NA" : .Lbl_SecStartVal.Tag = "NA" : .Lbl_SecStartVal.Enabled = False
            .Lbl_SecEndVal.Text = "NA" : .Lbl_SecEndVal.Tag = "NA" : .Lbl_SecEndVal.Enabled = False
            .Lbl_LineNumVal.Text = "NA" : .Lbl_LineNumVal.Tag = "NA" : .Lbl_LineNumVal.Enabled = False
            .Lbl_LineStartVal.Text = "NA" : .Lbl_LineStartVal.Tag = "NA" : .Lbl_LineStartVal.Enabled = False
            .Lbl_LineEndVal.Text = "NA" : .Lbl_LineEndVal.Tag = "NA" : .Lbl_LineEndVal.Enabled = False
            .Lbl_RowStartVal.Text = "NA" : .Lbl_RowStartVal.Tag = "NA" : .Lbl_RowStartVal.Enabled = False
            .Lbl_RowEndVal.Text = "NA" : .Lbl_RowEndVal.Tag = "NA" : .Lbl_RowEndVal.Enabled = False
            .Lbl_CursorPosVal.Text = "NA" : .Lbl_CursorPosVal.Tag = "NA" : .Lbl_CursorPosVal.Enabled = False
            .Lbl_NumTagsVal.Text = "0"
            .Status_RowTypeVal.Text = "<NA>"                ' <1.060.2> Status Bar, initialize Row Type to unknown
            .Status_FileDirtyVal.BackColor = Color.DarkOrange
            .Btn_Led.BackColor = Color.DarkOrange           ' Orange when nothing is happening; Red is ODF-parsing; Green is ODF-loaded and parsed.
            .Status_LinesVal.Text = "NA"                    ' <1.060.4> Status-Bar Line & Char counts are undefined until ODF is loaded
            .Status_CharsVal.Text = "NA"

            .Menu_File.Enabled = True                       ' Set Menu to default
            .Menu_SaveAs.Enabled = False                    ' Can't save what isn't opened...
            .Menu_CloseODF.Enabled = False                  ' Nothing to Close...
            .Menu_PrintDT.Enabled = False                   ' Nothing to Print
            .Menu_Sections1.Enabled = False                 ' No Sections yet
            .Menu_Sections2.Enabled = False
            .Menu_EditMode.Enabled = False                  ' Nothing to Edit
            .Menu_Tools.Enabled = True                      ' Tools in general are enabled, but
            .Menu_FollowASample.Enabled = False             '   Trace is disabled, as it requires a loaded ODF to function
            .Menu_About.Enabled = True

            SetRTBDescButtons(False)                        ' <1.060.2> Only enable the Section Set/Save Buttons when usable e.g. when a Section's text is loaded
            SetODFButtons(False)                            ' <1.060.2> Disable Controls that require presence of ODF text, such as Search, Marker, and Next/Prev

            Trace.Close()                                   ' <1.060.2> Close Sample Trace form - note, no error if Trace is not open.

        End With

        MAIN.Refresh()                                      ' Update screen now, before loading ODF

    End Sub
    Friend Sub ClearMarkers()

        ' Purpose:      Clear all Markers, set their default color
        ' Process:		Set the Controls properties to initial text and color
        ' Called By:    Menu_ClearMarkers_Click(); ResetToNoODF(); ParseSections()
        ' Side Effects: Updates all Marker Buttons: Displayed Text & Background Color
        ' Notes:        Consider making this a Collection, then can traverse entire Collection
        '               - making it easy to add additional Markers without changing much code.
        ' Updates:      <1.060.2> Relocated here from MAIN form.
        '               <1.060.6> Reset .Tag properties to "NA".

        Const lclProcName As String = "ClearMarkers"    ' <1.060.2> Routine's name for message handling

        With MAIN
            .Btn_Marker1.Text = "Marker 1" : .Btn_Marker1.Tag = 0 : .Btn_Marker1.BackColor = Color.Gainsboro
            .Btn_Marker2.Text = "Marker 2" : .Btn_Marker2.Tag = 0 : .Btn_Marker2.BackColor = Color.Gainsboro
            .Btn_Marker3.Text = "Marker 3" : .Btn_Marker3.Tag = 0 : .Btn_Marker3.BackColor = Color.Gainsboro
            .Btn_Marker4.Text = "Marker 4" : .Btn_Marker4.Tag = 0 : .Btn_Marker4.BackColor = Color.Gainsboro
        End With

    End Sub

    Friend Sub CheckUnloadODF(fnameODF As String,           ' Filename of a current ODF, "" if none
                              ByRef dirtyODF As Boolean)    ' If true, ODF has changed since opened or last saved

        ' Purpose:      Checks if we have an ODF loaded, and if so, has it been modified since it was
        '               loaded or last saved? If so, offer to save it. Used before Loading a new ODF,
        '               closing the current ODF, or exiting AECHO.
        ' Process:		Use the S_Section array to locate the start and end of the Title Text in the ODF;
        '               select it, force the font to 11-point Arial, and apply the desired color. Deselect.
        ' Called By:    Menu_Quit_Click(); Menu_OpenHauptwerkOrgan_Click(); Menu_CloseODF_Click()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      New with <1.060.2>

        Const lclProcName As String = "CheckUnloadODF"      ' Routine's name for message handling

        Dim questResp As MsgBoxResult

        If fnameODF <> "" AndAlso dirtyODF Then             ' Non-blank ODF Filename (data is loaded), change bit set
            questResp = MsgBox("The current ODF has been modified. Would you like to save it?", MsgBoxStyle.YesNo, "ODF Changed")
            If questResp = vbNo Then
                Return
            End If
            If SaveODFAs() Then                             ' User said "Yes", so initiate dialog and possibly save
                dirtyODF = False                            ' ODF was safely written, so reset the dirty-bit, otherwise leave unchanged
            End If
        End If

    End Sub
    Friend Function SaveODFAs() As Boolean                                          ' <1.060.2> True -> iff File was written, otherwise False

        ' Purpose:      Save the internal ODF to a designated Organ File.
        ' Process:		Build Save dialog, if User approves, write out the data. Force UTF-8 without BOM.
        ' Called By:    Menu_SaveAs_Click(), CheckUnloadODF()
        ' Side Effects: Writes data to the file system
        ' Notes:        <None>
        ' Updates:      <1.060.2> Moved code from Menu function, so it can be called from multiple locations.
        '               Add exception trapping to output operation. Converted to a Function that returns
        '               True if file was safely written, False if user chose not to save, or if save failed.

        Const lclProcName As String = "SaveODFAs"                                   ' <1.060.2> Routine's name for message handling

        Dim saveFileDialog As SaveFileDialog

        SaveODFAs = False                                                           ' <1.060.2> Assume user chooses to not save, or save fails
        saveFileDialog = New SaveFileDialog With {                                  ' Setup Save Dialog
            .Title = "Save the ODF as ...",
            .DefaultExt = "Organ_Hauptwerk_xml",
            .Filter = "organ files (*.Organ_Hauptwerk_xml)|*.Organ_Hauptwerk_xml",  ' <1.059.0> Added closing )
            .InitialDirectory = "D:\HAUPTWERK\HauptwerkSampleSetsAndComponents\OrganDefinitions",
            .RestoreDirectory = True
        }

        If saveFileDialog.ShowDialog() = DialogResult.OK Then                       ' <1.059.0> Write out Rtb_ODF content to the ODF File, forcing UTF8 encoding
            Try                                                                     ' <1.060.2> Intercept exceptions if I/O fails
                Dim UTF8NoBOM = New System.Text.UTF8Encoding(False)                 ' <1.059.0> Supress insertion of UTF-8 BOM at beginning of text stream
                My.Computer.FileSystem.WriteAllText(saveFileDialog.FileName,
                                                    MAIN.Rtb_ODF.Text, False, UTF8NoBOM)
                SaveODFAs = True                                                    ' <1.060.2> Safely written, update status to True
            Catch ex As Exception                                                   ' <1.060.2> I/O blew up, display exception message, return default False
                DispMsg(lclProcName, conMsgCrit,
                    "General Exception while attempting to write ODF File" & vbCrLf &
                    "Output filename is: " & saveFileDialog.FileName & vbCrLf &
                    "Exception Code is: " & vbCrLf & ex.ToString)
            End Try
        End If

        saveFileDialog.Dispose()                                                    ' <1.060.2> Clean up memory alloc

    End Function
    Friend Sub PositionToSectionByName(secName As String,                   ' Name of Section to move to
                                       loadFirstRow As Boolean)             ' <1.060.2> if True, update Description Box, otherwise leave it alone

        ' TROUVER UNE SECTION DE L'ORGUE A PARTIR DU MENU

        ' Purpose:      Processes a Menu Section click, positioning the ODF to the beginning of the desired Section.
        '               Loading of First Row (should it exists), and the related display of Descriptive Text, is optional.
        ' Process:      Locate the Section by name using GotSectionDataByName. When found, position Rtb_ODF
        '               to the Section, and update screen fields relating to Section/Line/Row/Cursor. If Section is not empty,
        '               and <loadFirstRow> is True, extract the Section's first Row into Rtb_XMLRow, parse it,
        '               update the Tag Count for this XML row, and display the Descriptive Text file.
        ' Called By:    Menu_SectionChoice(); Menu_OpenHauptwerkOrgan_Click(); Menu_ReComputeSections_Click()
        ' Side Effects: Alters Global Variables. Populates Rtb_XMLRow. Positions in Rtb_ODF. Updates MAIN form fields.
        ' Notes:        <None>
        ' Updates:      <1.060.2> Removed redundant checks for IsEmpty(), and redundant setting of Focus and scrolling
        '               to the desired position. Exit the For-Loop once a Section is found and processed. Changed
        '               Select Methods to Substring in ODF text extraction. Moved from MAIN form to here. Added With
        '               MAIN block to reference form Controls. Renamed from GetSectionFromMenu(). Add "Not found" message.
        '               Added boolean parameter to control if it also updates the Description Box to match the Section, or not.
        '               Update is suppressed when called from Organ Load to allow Section Location data to remain onscreen.
        '               When called from the Menu, the caller will ask for the Descriptive Text to be updated. Replace global
        '               G_CaretPos with local var cursorPos. Replace search through Sections array with a call to GotSectionDataByName() -
        '               which dynamically returns the same data elements as previsouly held in the static array.
        '               <1.060.6> Assign character-based positions to the .Tag properties of position fields.

        Const lclProcName As String = "PositionToSectionByName"             ' <1.060.2> Routine's Name for message handling

        Dim cursorPos As Integer                                            ' <1.060.2> Current location of cursor, replaces use of G_CaretPos
        Dim sec As Str_Section                                              ' <1.060.2> Data structure to represent 1 section, replacing array of 44 Sections
        Dim secEndLine As Integer                                           ' <1.060.6> Section's Last Line, derived from Last Char

        sec.name = ""                                                       ' <1.060.2> Need to allocate instance of the structure.

        If Not GotSectionDataByName(secName, sec) Then                      ' <1.060.2> True -> Found secName, its data is now in structure sec
            DispMsg(lclProcName, conMsgCrit,                                ' <1.060.2> False -> Whoops, didn't find it, alert and return.
                "Unable to locate Section named """ & secName & """ " & vbCrLf &
                "Could be either an ill-formed ODF, or an internal AECHO error.")
            Return
        End If

        With MAIN
            G_SectionName = sec.name                                        ' Save name of current Section

            If loadFirstRow Then                                            ' <1.060.2> Load Descriptive text, even if there is no first row
                G_PreviousRTFFile = LoadRTFFile(G_DataPath,
                                                G_SectionName,
                                                G_PreviousRTFFile)
            End If
            If (sec.firstRowLen > 0) And
                        loadFirstRow Then                                   ' trouver et selectionner la 1ere ligne de la section, si la section n'est pas vide
                .Rtb_XMLRow.Text =
                    .Rtb_ODF.Text.Substring(sec.firstRowStart,
                                            sec.firstRowLen)                ' If there is Row content in this Section, extract first Row's content and display it
                DisplayXMLRow()                                             ' montrer les infos de la ligne; build and display all relevant Element Names & Content
            Else                                                            ' Section is empty, or not loading the First Row, so clear fields to reflect null content.                                                        
                .Rtb_XMLRow.Clear()                                         ' <1.060.2> Changed Method to Clear
                TagsPanelVisible(False)
            End If

            .Rtb_ODF.Focus()                                                ' scroller de façon a avoir le titre de la section en haut de Rtb_ODF; return focus to the ODF
            .Rtb_ODF.Select(sec.startPos, 0)                                ' Scroll the Section Header's Start-Tag to the top of the Rtb_ODF display; positions cursor at the Section Title
            .Rtb_ODF.ScrollToCaret()                                        ' Make sure cursor is onscreen
            cursorPos = .Rtb_ODF.SelectionStart
            G_LineIndex =
                        .Rtb_ODF.GetLineFromCharIndex(cursorPos) + 1        ' caalculer et afficher n° dee ligne; Get the line number. Add 1 because this Method is 0-based

            .Lbl_SecStartVal.Text = G_LineIndex.ToString(conIntFmt) &
                " / " & sec.startPos.ToString(conIntFmt)                    ' afficher infos debut et fin; update Section Start/End on screen; <1.060.2> Added formatting
            .Lbl_SecStartVal.Tag = sec.startPos                             ' <1.060.6> Save the raw character position in the .Tag property, to be used when control is clicked
            secEndLine = MAIN.Rtb_ODF.GetLineFromCharIndex(sec.endPos) + 1  ' <1.060.6> Determine 1-based Line Number of Section's last Line, from its last Char
            .Lbl_SecEndVal.Text = secEndLine.ToString(conIntFmt) &
                        " / " & sec.endPos.ToString(conIntFmt)              ' <1.060.2> Added formatting
            .Lbl_SecEndVal.Tag = sec.endPos                                 ' <1.060.6> Save the raw character position in the .Tag property, to be used when control is clicked
            .Lbl_SectionName.Text = sec.name                                ' afficher le nom de la section; update the Section Name on screen
            .Lbl_LineNumVal.Text = G_LineIndex.ToString(conIntFmt)          ' <1.060.2> format as integer and place onscreen
            .Lbl_LineNumVal.Tag = cursorPos                                 ' <1.060.6> Save the raw character position in the .Tag property, to be used when control is clicked
            .Lbl_RowStartVal.Text = .Lbl_SecStartVal.Text                   ' <1.060.6> Row-Start = Section-Start
            .Lbl_RowStartVal.Tag = sec.startPos
            .Lbl_LineStartVal.Text = sec.startPos.ToString(conIntFmt)
            .Lbl_LineStartVal.Tag = sec.startPos
            .Lbl_RowEndVal.Text = G_LineIndex.ToString(conIntFmt) &         ' <1.060.6> Added Line Number
                " / " & (sec.startPos + sec.titleLen).ToString(conIntFmt)
            .Lbl_RowEndVal.Tag = sec.startPos + sec.titleLen
            .Lbl_LineEndVal.Text = (sec.startPos + sec.titleLen).ToString(conIntFmt)
            .Lbl_LineEndVal.Tag = .Lbl_RowEndVal.Tag
            .Lbl_CursorPosVal.Text = cursorPos.ToString(conIntFmt)
            .Lbl_CursorPosVal.Tag = cursorPos
            .Status_RowTypeVal.Text = "Sec Start-Tag"
        End With

        Return                                                          ' <1.060.2> We found it, we displayed it, we're done

    End Sub
    Friend Function CountTags(startPos As Integer,          ' Start of range to search within srcText, zero based
                              endPos As Integer,            ' End of range to search, also zeo based
                              srcText As RichTextBox        ' Control to search (must be an RTB)
                              ) As Integer                  ' Return the count; <1.059.0> Changed from Single (Floating) to Integer type

        ' COMPTE LE NOMBRE DE TAGS DANS UNE LIGNE DE Rtb_XMLRow

        ' Purpose:      Calc number of Elements within an Rtb_XMLRow (1 ODF row) range,  including the bounding '<o>...</o>'
        '               Caller can decide to discount the outer Parent tags, and is responsible for displaying the result,
        '               when desired.
        ' Process:      Count the number of End-Tag markers '</' in the range
        ' Called By:    DisplayXMLRow();
        '               OldGetSectionFromMenu() (deprecated)
        ' Side Effects: < None >
        ' Notes:        <None>
        ' Updates:      <1.060.2> Relocated from MAIN form. Altered to only return the count, removed updating the onscreen field:
        '               that is now the caller's responsibility. Simplified logic.

        Const lclProcName As String = "CountTags"           ' <1.060.2> Function name 

        Dim count As Integer = 0                            ' Counts number of End-Tag markers found
        Dim curSearchPos As Integer = startPos              ' <1.060.2> Start search from this point, used to "find next"

        While curSearchPos <= endPos                        ' <1.060.2> Loop until we are past the EOS
            curSearchPos = FindText("</",                   ' <1.060.2> curSearchPos is position of End-Tag, or -1
                                 curSearchPos,
                                 srcText)                   ' <1.060.2> Search for End-Tag marker: '</'
            If curSearchPos = -1 Then                       ' <1.060.2> We did not find any more End-Tags, so we're done
                Return count
            End If
            curSearchPos += 4                               ' <1.060.2> Advance position past the shortest possible End-Tag "</x>"
            count += 1                                      ' <1.060.2> Ring up another Tag
        End While

        Return count                                        ' <1.060.2> Exactly hit EOS, return the count

    End Function
    Friend Function FindText(searchText As String,              ' Text string to search for
                             start As Integer,                  ' Starting location for search within the srcText RTB
                             srcText As RichTextBox             ' RTB Control that holds the text to be searched
                             ) As Integer                       ' Index to first character of located string, -1 if not found

        ' CHERCHE UN TAG DE HAUT EN BAS DANS Rtb_XMLRow ET RETOURNE SA POSITION

        ' Purpose:      Search a RichTextBox control for [searchText], beginning from [start],
        '               returning the position of the first character of [searchText] if found.
        ' Process:      Validate parameters, use RichTextBox Find Method to execute search.
        ' Called By:    ReadTag(); CountTags(); GetSectionFromIndex(); FindButtonsProc()
        ' Side Effects: <None>
        ' Notes:        Consolidates both Tag and general text searches, from any RTB Control.
        ' Updates:      <1.060.2> Relocated from MAIN form. Replace hard-coded search target with a Control
        '               object parameter. Added guard-clauses to validate parameters. Simplified logic.
        '               Combined functionality of old FindMyText() and FindMyTag() funcitons.

        Const lclProcName As String = "FindText"                ' <1.060.2> Function's name for message handling

        If (srcText.TextLength) <= 0 Or
            (start >= srcText.TextLength) Then                  ' <1.060.2> Return -1 if we have no source text, or start is past EOS
            Return -1                                           ' MODIF V058; to avoid searching an empty source
        End If
        If searchText.Length <= 0 Then
            DispMsg(lclProcName, conMsgExcl,                    ' <1.060.2> Let user know about internal error, search text should not be null
                    "Function called with null search text.")
            Return -1
        End If
        If start < 0 Then
            DispMsg(lclProcName, conMsgExcl,                    ' <1.060.2> Let user know about internal error, search start should be >=0
                    "Function called with start of search before beginning." & vbCrLf &
                    "start is: " & start)
            Return -1
        End If

        Return srcText.Find(searchText,
                            start,
                            conQuickNoH)                        ' <1.060.2> Obtain the location of the search string in the XML Row

    End Function
    Friend Sub DisplayXMLRow()                                  ' <1.059.0> Deleted parameter linestart, never used in code; aligned calls to DisplayXMLRow to match

        ' AFFICHE LES TAGS ET LEUR CONTENU DANS LE PANEL

        ' Purpose:      Parse contents of Rtb_XMLRow (ODF Row), displaying all Element Names & Content in alphabetical
        '               order. Ignores the Parent '<o>...</o>' Element. For Sections that link to image files, present
        '               the control to optionally locate and display an image file specified by an Element, if any.
        ' Process:      Visit each of the 24 fixed positions on the panel for displaying Element Names and Content,
        '               in alpahbetical order. Call DisplayTagText(), which searches for the presence of the next
        '               possible Element Name, starting the search each time at lastIdx. Upon return from
        '               DisplayTagNext(), lastIdx is set to 1 more than the last Element found, and [counter] has been
        '               incremented. The search range is from "<a>" to "<z>", then "<a1>" to "<z1>". DisplayTagText()
        '               also places the Name and Content of a parsed Element into the designated form fields. Once
        '               [counter] exceeds the known number of Elements in the Row, further display fields are simply
        '               cleared, erasing any prior content from a previous Element.
        ' Called By:    Rtb_ODF_MouseDoubleClick(); PositionToSectionByName(); Btn_NextLine_Click();
        '               OldGetSectionFromMenu() (deprecated)
        ' Side Effects: Updates all 24 pairs of Form-fields used to display Element Names & Content; Selects and
        '               Highlights the Row in the Rtb_ODF (ODF) display.
        ' Notes:        Consider re-writing to use an artificial array of Controls for labelTag and
        '               textTag, and substituing a loop for the 24 explicit call seen below.
        ' Updates:      <1.060.2> Relocated from MAIN form, added With Main block to reference form Controls. Removed
        '               extraneous intermediate variable assignments for those 24 call to DisplayTagText(). Modifed check
        '               for showing the "Row Action" Button: the Row has to contain a reference to an Image file or
        '               a Sample. Renamed from DisplayObject(). Removed call to make all Tags visible, that is now done
        '               by DisplayTagText as needed, a Field at a time.
        '               <1.060.6> Fix bug in counting Tags, only do this if this is a Record-Row

        Const lclProcName As String = "DisplayXMLRow"           ' <1.060.2> Routine's name for message handling

        Dim lastIdx As Integer = Asc("a")                       ' The current Element Name to look for - cycled from 'a' to 'z', then 'a1' to 'z1'. Expressed as ASCII value
        Dim counter As Integer = 0                              ' Number of Elements parsed and displayed so far. Updated by DisplayTagText().
        Dim nbtags As Integer                                   ' Number of Child Elements in the row: once [count] reaches [nbtags], display fields are cleared

        G_ImageFile = ""                                        ' <1.060.2> If not null after calls to DisplayTagText, then we have an Image that can be displayed
        G_ImageSet = ""                                         ' <1.060.2> For an ImageSetElement Row, will be set to the parent ImageSet, and G_PackageID will remain empty
        G_PackageID = ""                                        ' <1.060.2> For an ImageSet Row, will be set to the PackageID, and G_ImageSet will remain empty
        G_SampleID = ""                                         ' <1.060.2> For a Sample Row, will be set to the SampleID

        With MAIN
            If FindText(conRowStartTag, 0, MAIN.Rtb_XMLRow) >= 0 Then
                nbtags = CountTags(0,                           ' <1.060.6> Only execute this code if we're in a Record Row (<o>...</o>)
                           .Rtb_XMLRow.TextLength - 1,
                           .Rtb_XMLRow) - 1                     ' <1.060.2> Get count of tags - scans entire XML Row. Reduce count by one, to ignore outer "<o>...</o>"
                .Lbl_NumTagsVal.Text = nbtags                   ' <1.060.2> and display it

                DisplayTagText(lastIdx, counter, nbtags, .LabelTag1, .tag1)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag2, .tag2)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag3, .tag3)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag4, .tag4)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag5, .tag5)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag6, .tag6)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag7, .tag7)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag8, .tag8)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag9, .tag9)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag10, .tag10)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag11, .tag11)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag12, .tag12)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag13, .tag13)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag14, .tag14)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag15, .tag15)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag16, .tag16)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag17, .tag17)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag18, .tag18)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag19, .tag19)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag20, .tag20)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag21, .tag21)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag22, .tag22)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag23, .tag23)
                DisplayTagText(lastIdx, counter, nbtags, .LabelTag24, .tag24)

                If G_ImageFile <> "" Then
                    .Btn_RowAction.Text = conDisplayImage       ' <1.060.2> Paint the button as "Display Image" for an Image Row
                    .Btn_RowAction.Visible = True               ' <1.060.2> Found an Image File Tag in ImageSet or ImageSetElement: enable the Control to display that Image
                    If (G_ImageFile.Substring(0, 1) = "/") Or   ' <1.060.2> Trim a leading "/" or "\", if there is one, from the name, so Path.Combine will correctly handle the filename
                    (G_ImageFile.Substring(0, 1) = "\") Then
                        G_ImageFile = Right(G_ImageFile, G_ImageFile.Length - 1)
                    End If
                ElseIf G_SampleID <> "" Then
                    .Btn_RowAction.Text = conTraceSample        '<1.060.2> This is a Sample Row, display Trace Sample action
                    .Btn_RowAction.Visible = True
                Else
                    .Btn_RowAction.Visible = False              ' Hide the control for Rows without context actions
                End If
            Else                                                ' <1.060.6> Fix bug, if not Rec-Row, display "0"
                .Lbl_NumTagsVal.Text = "0"
            End If
        End With

    End Sub
    Friend Function LoadRTFFile(dataPath As String,                         ' <1.060.2> Path to \DATA directory
                                sectionName As String,                      ' <1.060.2> Section Name we are requesting
                                prevFile As String                          ' <1.060.> Full Path/File of rtf file currently on display, "" if none
                                ) As String                                 ' <1.060.2> Full Path/File of newly loaded file, "" if none found, prevFile if already loaded

        ' CHARGE LE FICHIER RTF DECRIVANT UNE SECTION

        ' Purpose:      Display the Descriptive Text file (.rtf) associated with a Section, or modele.rtf
        '               if the desired Section file cannot be located.
        ' Process:		Build the file's name from the path to the application data files, suffixed by the Section
        '               Name, then ".rtf". If this is the same file as displayed the last time through, we're done.
        '               If not, try to locate the file - if it cannot be found, substitute a generic, catch-all
        '               file instead (modele.rtf). Display the resulting file into the RTFFile control. If successful,
        '               enable the "Set Font" and "Save Description" Buttons, and return the fully-qualified path/filename
        '               of the loaded file. If no file can be found, return "".
        ' Called By:    Rtb_ODF_MouseDoubleClick(); PositionToSectionByName(); Btn_NextLine_Click();
        '               OldGetSectionFromMenu() (deprecated)
        ' Side Effects: Updates Rtb_DescText Control, TextBoxTitles 1 & 2
        ' Notes:        Move Help file display to its own Window.
        ' Updates:      <1.060.2> Relocated from MAIN form, added With Main block to reference form Controls. Changed
        '               filename patch/filename catenation to Path.Combine for improved OS independence. Resolved
        '               issues with helpfile display damaging context for what needs to be redisplayed. Removed code
        '               that resets the Font to Verdana 10, as this also removes other Style attributes, such as
        '               Bolding & Underlining. "Set Font" can still be used to force the Font. Removed references to
        '               globals. Added exception handler. Rewrote as a function that returns the path of the loaded file.

        Const lclProcName As String = "LoadRTFFile"                         ' <1.060.2> Funciton's name for message handling

        Dim pathToRTFFile As String                                         ' <1.060.2> Path variable, replaces global G_RTFFile

        pathToRTFFile = Path.Combine(dataPath, (sectionName & ".rtf"))      ' <1.060.2> Build candidate filename based on the Section Name.
        If prevFile = pathToRTFFile Then                                    ' <1.060.2> Only take action if requesting different content from what is currently loaded
            SetRTBDescButtons(True)                                         ' <1.060.2> Enable controls, we have a loaded Section file (or modele.rtf)
            Return prevFile                                                 ' <1.060.2> File was already loaded, so return the same name - it is still loaded
        End If

        With MAIN                                                           ' <1.060.2> Estabish reference to MAIN form
            CenterText(conTextBoxTitle_Desc,                                ' <1.060.2> Display/Center Section Descriptive Title on the Top Title Line
                   .Lbl_TextBoxTitle1,
                   .Rtb_DescText.Left,
                   .Rtb_DescText.Right)
            .Lbl_TextBoxTitle2.Left = .Rtb_DescText.Left                    ' <1.060.2> Place column titles on the Bottom Title Line
            .Lbl_TextBoxTitle2.Text = conTextBoxTitle2_Desc
            Try
                If File.Exists(pathToRTFFile) Then                          ' Ensure Section File exists <1.060.2> Changed Method to File.Exists
                    .Rtb_DescText.LoadFile(pathToRTFFile)                   ' It does, so display it.
                    SetRTBDescButtons(True)                                 ' <1.060.2> Section Text is loaded, enable "Set Font" and "Save Description" Buttons
                Else                                                        ' Section file was not found, so try to load default text instead
                    pathToRTFFile = Path.Combine(dataPath, "modele.rtf")    ' <1.060.2> Path to Model File - let's try loading this as a catch-all
                    If File.Exists(pathToRTFFile) Then                      ' Check if default file exists <1.060.2> Use File.Exists
                        .Rtb_DescText.LoadFile(pathToRTFFile)               ' Load the default Model.rtf file
                        SetRTBDescButtons(True)                             ' <1.060.2> Model Text is loaded, enable "Set Font" and "Save Description" Buttons
                    Else                                                    ' What if both Section and Model are missing?
                        SetRTBDescButtons(False)                            ' <1.060.2> Nothing to edit, disable "Set Font" and "Save Description" Buttons
                        DispMsg(lclProcName, conMsgCrit,                    ' <1.060.2> Whine to the user...
                                "Unable to locate a Descriptive Text file." & vbCrLf &
                                "Section requested was: " & sectionName & vbCrLf &
                                "Data Path was: " * dataPath & vbCrLf &
                                "Last path/file tried was: " & pathToRTFFile)
                        pathToRTFFile = ""                                  ' <1.060.2> Nothing is loaded. Set to null to ensure next attempt will try to load a file.
                    End If
                End If
            Catch ex As ArgumentException                                   ' <1.060.2> Couldn't retrieve the requested data, & raised an Exception
                DispMsg(lclProcName, conMsgCrit,
                        "Unable to load a Descriptive Text File" & vbCrLf &
                        "Section requested was: " & sectionName & vbCrLf &
                        "Data Path was: " * dataPath & vbCrLf &
                        "Last path/file tried was: " & pathToRTFFile & vbCrLf &
                        "Exception Code is: " & ex.Message)
                pathToRTFFile = ""                                          ' <1.060.2> Nothing is loaded
            End Try
            Return pathToRTFFile                                            ' <1.060.2> Remember what file we loaded, to avoid unnecessary repainting
        End With

    End Function
    Friend Sub DisplayTagText(ByRef lastIdx As Integer,         ' Integer representation of letters ('a' to 'z+')
                              ByRef counter As Integer,         ' Number of Elements already identified and displayed
                              nbTags As Integer,                ' Known number of Elements to display in all
                              labelTag As Control,              ' Onscreen Form-field, display the Element's Name here
                              textTag As Control)               ' Onscreen Form-field, display the Element's Content here

        ' AFFICHE DANS LE PANNEL LE TAG ET SON CONTENU

        ' Purpose:      Find the next Element in a Row, starting with Name=[lastIDX]; when found, display the Name and
        '               Content in the designated Form-fields. In coordination with DisplayXMLRow(), returns the
        '               Elements one at a time, in alphabetical order by Name/Type.
        ' Process:      Upon entry, [lastIdx] represents the current alphabetic character to search for (a, b, c,
        '               ... encoded as integers). The entire XML Row is searched: if not found, [lastIdx] is
        '               advanced to the next character, and the search loops until a matching Element Name is
        '               found, or the range of possible Names is exhausted. When found, the Name & Content is extracted,
        '               the tally of Elements-found is incremented ([counter]), and [lastIdx] is left advanced one-past
        '               the located Element, in preparation for the next search. Once the number of parsed Elements
        '               ([counter]) reaches the known number of Elements in the Row ([nbTags]), this routine no longer
        '               searches, it just fills the designated Form-fields [labelTag] & [textTag]) with blanks.
        ' Called By:    DisplayXMLRow()
        ' Side Effects: When the row is part of Sections "ImageSet" or "ImageSetElement", global variables relating
        '               to Images are assigned values from parsed Element Content.
        ' Notes:        Could be re-written to do a single left to right scan parsing Elements, maintaining an
        '               alphabetized list, then make a single pass to display the fields. For/Next loop is better as While loop,
        '               as search success is also an exit criteria.
        ' Updates:      <1.060.2> Relocated from MAIN form.. Set unused labels to invisible, filled-in labels to visible.

        Dim idx As Integer                                      ' Current position in loop, ranging from lastIdx to 'z'+27
        Dim txt As String                                       ' Content of a Element

        If counter < nbTags Then                                ' We haven't parsed every known Element yet, so execute parsing logic

            For idx = lastIdx To Asc("z") + 27                  ' recherche des tags <a> à <z>; search for names from <a>...<z>, <a1>...<z1>

                Select Case idx
                    Case Is <= Asc("z")                         ' idx is between 'a' and 'z' inclusive
                        labelTag.Text = Chr(idx)                ' Convert idx to the equivalent character e.g. 97 -> 'a'
                        txt = ReadTag(Chr(idx), 0)              ' Search entire Row for an Element with this Name; Note RTB is zero-based.
                        textTag.Text = txt                      ' Place Element's Content onscreen (blank if not found)
                        If txt <> "" Then                       ' <1.060.2> Look for special Field content only if Tag was found, with content
                            If G_SectionName = conImageSet Then ' si presence image; if inside ImageSet, save useful info
                                If idx = Asc("c") Then          ' Lettre c : package ID; Element is a PackageID
                                    G_PackageID =               ' <1.060.2> Ensure length is 6, left-padded with zeroes
                                    txt.PadLeft(6, "0")
                                End If
                                If idx = Asc("j") Then          ' Lettre j : mask de transparence, s'il existe; Element is an image mask file
                                    G_ImageFile = txt           ' Save the filename for use in "Display Image"
                                End If
                            End If

                            If G_SectionName = conImageSetElement Then
                                If idx = Asc("a") Then          ' Lettre a : image set; Element is the related ImageSetID
                                    G_ImageSet = txt            ' Save ImageSetID for later use
                                End If
                                If idx = Asc("d") Then          ' Lettre d : bitmap filename; Element is ImageFile (name of the file)
                                    G_ImageFile = txt           ' Save the filename for later use in "Display Image"
                                End If
                            End If

                            If G_SectionName = conSample Then
                                If txt <> "" AndAlso idx = Asc("a") Then
                                    G_SampleID = txt
                                End If
                            End If
                        End If
                    Case Is > Asc("z")                          ' tags <a1>, <b1> ...; idx is > 'z': after <z> comes <a1>...<z1>
                        labelTag.Text = Chr(idx - 26) & "1"     ' Create numeric-suffix Element Names from idx
                        txt = ReadTag(Chr(idx - 26) & "1", 0)
                        textTag.Text = txt

                    Case Else                                   ' This seems superfluous - two prior cases cover all possibilities
                        txt = ""
                End Select

                If txt <> "" Then                               ' When not null, then the Element was found
                    labelTag.Visible = True                     ' <1.060.2> Since there is content, make the Field visible
                    textTag.Visible = True
                    lastIdx = idx + 1                           ' For next entry, start one Element Name past the Name just found
                    counter += 1                                ' Advance count of Elements found, exit loop
                    Exit For
                End If
            Next

        Else                                                    ' We've already parsed every known Element, just clear the remaining Name & Content fields
            labelTag.Text = ""
            labelTag.Visible = False                            ' <1.060.2> And make them invisible, for good measure!
            textTag.Text = ""
            textTag.Visible = False
        End If

    End Sub
    Friend Function ReadTag(tag As String,                      ' Element-Name to locate and parse
                            pos As Integer                      ' Begin search from this position within Rtb_XMLRow
                            ) As String                         ' Element-Content text: " " if Element exists but has null Content, "" if Element is not foumd

        ' CHERCHE UN TAG DANS Rtb_XMLRow ET LIT LE  TEXTE CONTENU DANS LE TAG

        ' Purpose:      Search Rtb_XMLRow (the selected ODF Row) for the Element-Name [Tag] starting at [pos], returning
        '               the Content from between the Start and End-Tags (without the bounding Tags themselves):
        '               if [tag]='x', find "<x>Some Text</x>", return "Some Text".
        ' Process:      Validate parameters, return "not found" if no ODF or if searching for Element '<o>'. Search forward
        '               for Start-Tag, if found, continue searching to find End-Tag, extract Content as
        '               a substring from between the bounding Tags. If Element cannot be found, return null string ""
        '               to indicate failure; if found, but there is no Content, return a single blank (" ") instead.
        ' Called By:    DisplayTagText()
        ' Side Effects: <None>
        ' Notes:        Consider combining several functions into a single call:
        '               from a position (within a line?), search for an Element, return its start, end, and Content. Consider
        '               changing to a boolean function to indicate success, with the extracted string a ByRef parameter
        '               rather than returned as the function's result.
        ' Updates:      <1.060.2> Relocated from MAIN form; Added exception message handling. Fixed bug where test for failure to
        '               find the Start-Tag could never return "not found", as the flag value of -1 was incremented by the size of the
        '               tag before testing if the search returned -1. Unselected the Element's Content after extraction.

        Const lclProcName As String = "ReadTag"                     ' Function's name for message handling

        Dim returnValue As String                                   ' Interim Function return
        Dim tagStart As Integer                                     ' 1 past Start-Tag's closing ">": the earliest starting point of non-null Content
        Dim tagEnd As Integer                                       ' Position of End-Tag

        If MAIN.Rtb_XMLRow.TextLength = 0 Then Return ""            ' MODIF V058 <1.059.0> corrected error of not assigning a return value, which would cause a run-time exception
        If tag = "o" Then Return ""                                 ' ignorer le tag <o>; <1.059.0> deleted extraneous Exit Function: Return already exits

        tagStart = FindText("<" & tag & ">",
                             pos,
                             MAIN.Rtb_XMLRow)                       ' trouve le tag de début et de fin; Search for Start-Tag ("<x>")
        If tagStart = -1 Then Return ""                             ' Never found Start-Tag; <1.059.0> deleted extraneous Exit Function: Return already exits
        tagStart += 2 + Len(tag)                                    ' <1.060.2> Bug fix, set tagStart past the Start-Tag AFTER checking for search-failure (tagStart = -1)

        tagEnd = FindText("</" & tag & ">",
                           tagStart,
                           MAIN.Rtb_XMLRow)                         ' trouve le tag de fin; Search for End-Tag ("</x>"), continuing from start of Content
        If tagEnd = -1 Then                                         ' Didn't find the matching End-Tag: warn user, return failure.
            DispMsg(lclProcName, conMsgExcl,
                    "While parsing the XML Row, did not find matching End-Tag for Start-Tag: <" & tag & ">" & vbCrLf &
                    "Content or End-Tag search began at position: " & tagStart & vbCrLf &
                    "Likely syntax error in the ODF - will continue processing.")
            Return ""                                               ' Never found End-Tag; <1.059.0> deleted extraneous Exit Function: Return already exits
        End If

        If tagEnd - tagStart < 1 Then                               ' End was before Start, signal error. Note that End can equal Start, if there is no Content
            DispMsg(lclProcName, conMsgExcl,
                    "tagEnd: " & tagEnd & " was < tagStart: " & tagStart & vbCrLf &
                    "While searching for Tag <" & tag & ">" & vbCrLf &
                    "From position: " & pos & vbCrLf &
                    "Likely syntax error in the ODF - will continue processing.")
            Return ""                                               ' Return null
        End If

        MAIN.Rtb_XMLRow.SelectionStart = tagStart                   ' lit le contenu du tag; Position selection start to beginning of possible Content
        MAIN.Rtb_XMLRow.SelectionLength = tagEnd - tagStart         ' Length of text between Tags
        returnValue = MAIN.Rtb_XMLRow.SelectedText                  ' Select the Content, and return it
        MAIN.Rtb_XMLRow.DeselectAll()                               ' <1.060.2> Unselect text before returning

        If returnValue = "" Then returnValue = " "                  ' remplace "" par " "; Return a single blank instead of a null string, if the Element was found, but has no Content
        Return returnValue

    End Function
    Friend Sub FindButtonsProc(srchPos As Integer,          ' Position to start from
                               retPos As Integer,           ' <1.060.2> Position to return cursor to, if search fails
                               ByRef foundStart As Integer, ' <1.060.2> Found-text Start/End Context vars, returned to called
                               ByRef foundEnd As Integer,   ' <1.060.2> 0 -> Context Cleared
                               dirForward As Boolean)       ' <1.060.2> True -> search forwards; False -> search backwards

        ' Purpose:      Do a forward or backward search for text, position to found text, highlight it.
        '               [srchPos] is index to begin search from. Leave cursor at [retPos]
        '               if text is not found. If found, clears Tags and XMLRow.
        ' Process:		Search, if found, locate Section, then extract and parse Child Element Row.
        '               If not found, reposition cursor to point from which search began.
        ' Called By:    Btn_FindFirst_Click(); Btn_FindNext_Click(); Btn_FindPrev_Click
        ' Side Effects: Updates ODF display, form fields displaying Section & Position Info
        ' Notes:        <None>
        ' Updates:      <1.060.2> Added local routine name string for message handling. Changed display of current
        '               cursor position to use formatted output. Relocated here from MAIN form, added "MAIN" context
        '               references. Eliminated global G_TextToFind. Updated messaging to use DispMsg(). Replaced
        '               global G_FindStartPosition with parameter [srchPos]. Added parm to control direction
        '               of search. removed use of G_RowText. No longer affects XML Row Text, Tag Panels, Descriptive
        '               Text, Section Title, or Section Position data - is now similar to a single Mouse-click.
        '               Add parameters for Found-Text Start/End context, which are set on success, cleared on failure.
        '               <1.060.6> Save position in .Tag properties, for later repositioning

        Const lclProcName As String =                       ' <1.060.2> Routine's name for message handling
            "FindButtonsProc"

        Dim startPos As Integer                             ' <1.060.2> Starting position for search, passed in as [srchPos]
        Dim lineStart As Integer

        If MAIN.Txt_SearchText.Text = "" Then
            Return                                          ' Null search, just return
        End If

        If dirForward Then
            If srchPos = foundEnd Then                      ' <1.060.2> Forward search, if cursor is still at end of last successful search,
                srchPos = foundStart + 1                    ' <1.060.2>   begin this search 1 char past start of last found text, otherwise search from cursor
            End If
            startPos = Clamp(srchPos,
                         0,                                 ' <1.060.2> Limit start of search to be within ODF bounds
                         MAIN.Rtb_ODF.TextLength - 1)
            foundStart = FindText(MAIN.Txt_SearchText.Text,
                            startPos,                       ' <1.060.2> FindText validates parameters, returns -1 for null or illegal values.
                            MAIN.Rtb_ODF)                   ' Search forward
        Else
            If srchPos = foundEnd Then                      ' <1.060.2> Backwards search, if cursor is still at end of prior search
                srchPos = srchPos - 1                       ' <1.060.2>   backup start by 1, otherwise search from cursor
            End If
            startPos = Clamp(srchPos,
                             0,                             ' <1.060.2> Limit start of search to be within ODF bounds
                             MAIN.Rtb_ODF.TextLength - 1)
            foundStart = FindReverse(MAIN.Txt_SearchText.Text,
                            startPos,                       ' <1.060.2> FindReverse validates parameters, returns -1 for null or illegal values.
                            MAIN.Rtb_ODF)                   ' Search backward
        End If

        If foundStart < 0 Then                              ' Did not find the text <1.060.2> Changed test to <0 i.e. -1, 0 is a valid return, text is zero-based
            foundStart = -1                                 ' <1.060.2> Clear Found-Text Start/End context, return to caller
            foundEnd = 0
            DispMsg("", conMsgInfo,
                    "Search Text not found.")
            MAIN.Lbl_CursorPosVal.Text =                    ' <1.060.2> Return display to initial position, on screen
                retPos.ToString(conIntFmt)
            MAIN.Lbl_CursorPosVal.Tag = retPos              ' <1.060.6> Save position in the .Tag property, for repositioning
            MAIN.Rtb_ODF.Focus()
            MAIN.Rtb_ODF.Select(retPos, 0)                  ' <1.060.2> Reset to the original cursor position
            Return
        End If

        foundEnd = foundStart + MAIN.Txt_SearchText.TextLength
        MoveToPosition(foundEnd,                            ' Fetch the Row - this updates Line#, Line Start, Line End, Row Start, and Row End on screen
                       G_LineIndex,                         ' <1.060.2> Returns Line Number, 0-based
                       lineStart,                           ' <1.060.2> Returns start of Line
                       True,                                ' <1.060.2> Reposition the cursor
                       False)                               ' <1.060.2> Don't extend selection to entire row, it will be set in code below
        MAIN.Rtb_ODF.Select(foundStart,                     ' <1.060.2> Select and Highlight the located text in the ODF 
                            MAIN.Txt_SearchText.TextLength)

    End Sub
    Friend Sub EnumerateSectionsSetFont(srcRTB As RichTextBox,      ' Object to resize (must be an RTB)
                                        newSize As Integer,         ' New font size in points
                                        noODF As Boolean,           ' True -> ODF not yet loaded, ignore Title resizing
                                        enumSecs As Boolean,        ' True -> List Section Inventory in Descriptive Text Area, like old "ReCompute"
                                        resizeTitles As Boolean)    ' True -> Scan Rtb_ODF for Title Lines, rescale them; False -> Leave ODF Title Lines alone

        ' Purpose:      Set the contents of a designated RTB Control to a designated size, forcing the font face and emphasis to
        '               the defaults. Optionally, readjust the emphasis (upsizing/bold) for Section Titles - used when changing the
        '               font size in the Rtb_ODF display. If enumSecs is True, also display the Section position data in the
        '               Display Text Area, as the Section Title are located.
        ' Process:		Regardless of Title handling, first set the entire RTB Control to the standard font at the specified size.
        '               If re-emphasizing Section Titles, sequentially scan the Section Title Start-Tags, and when found,
        '               apply the emphasis. Additionally, if enumerating the Sections, set up the display areas and headers,
        '               clear the Record-Row, Parsed Tags, and Descriptive Area, and isolate the Section's Name and End-Tag
        '               position, displaying it in the Descriptive Text Area as each Section is located.
        ' Called By:    Num_ODFFontSize_ValueChanged()
        ' Side Effects: Alters the designated Control's properties.
        ' Notes:        <None>
        ' Updates:		New with <1.060.2>.
        '               <1.060.5> Removed ref to RemoveImage(), moved code to make Title1/2 visible here.
        '               <1.060.6> Changed from .Text to .Tag property when retrieving Cursor's position

        Const lclProcName As String = "EnumerateSectionsSetFont"    ' Routine's name for message handling

        Dim srchPos As Integer                                      ' Current position in ODF, as we search for Section Start-Tags
        Dim eTag As Integer                                         ' Position of end of a Start Tag, the ">" character
        Dim secNum As Integer                                       ' Ordinal number of Sections as they are located in order, 1-based
        Dim secNameStart As Integer                                 ' Start of Section Name, found in Attribute Value
        Dim secNameEnd As Integer                                   ' End of Section Name, closing '"' -1
        Dim secName As String                                       ' Extracted Section Name
        Dim secEnd As Integer                                       ' Section End-Tag location, start of "</ObjectList>"

        srcRTB.SelectAll()                                          ' Affects all text in target RTB
        srcRTB.SelectionFont = New Font(conFont,                    ' Set entire RTB to default (Arial), selected font-size, Regular stlye
                                         newSize,
                                         FontStyle.Regular)

        If resizeTitles AndAlso (Not noODF) Then                    ' Only do this loop if asked, and we have an ODF present
            secNum = 0                                              ' Section counter starts at 0
            If enumSecs Then
                TagsPanelVisible(False)                             ' Hide all controls in the PackageID Panel
                ClearMarkers()                                      ' Reset Markers
                MAIN.Rtb_DescText.Clear()                           ' vider Rtb_DescText; clear the control that displays descriptive/help text
                G_PreviousRTFFile = ""                              ' To enforce re-display of Section Text if menu-clicking on the present Section
                SetRTBDescButtons(False)                            ' Disable "Set Font" and "Save Description" buttons, no Section content to work with
                MAIN.Rtb_XMLRow.Clear()                             ' Reset Record-Row display are (<o>...</o> tags)
                CenterText(conTextBoxTitle_ODFLoad,                 ' Display "Section Locations" Title over Display Text Area
                    MAIN.Lbl_TextBoxTitle1,                         ' Center text on the Top Title Line
                    MAIN.Rtb_DescText.Left,
                    MAIN.Rtb_DescText.Right)
                MAIN.Lbl_TextBoxTitle2.Left =
                    MAIN.Rtb_DescText.Left                          ' Place legend text on the left of the Bottom Title Line
                MAIN.Lbl_TextBoxTitle2.Text = conTextBoxTitle_List
                MAIN.Rtb_DescText.SelectAll()                       ' Set tab locations to approximate columnar alignment
                MAIN.Rtb_DescText.SelectionTabs = New Integer() {60, 130, 160, 260, 350, 380, 470}
                MAIN.Lbl_TextBoxTitle1.Visible = True               ' <1.060.5> Moved here from deprecated call to RemoveImage()
                MAIN.Lbl_TextBoxTitle2.Visible = True               ' <1.060.5> Moved here from deprecated call to RemoveImage()
                MAIN.Lbl_TextBoxTitle1.Refresh()
                MAIN.Lbl_TextBoxTitle2.Refresh()
            End If

            MAIN.Btn_Led.BackColor = Color.Red                      ' At the start, change Btn_Led to Red, then back to Green when done
            MAIN.Btn_Led.Refresh()
            srchPos = srcRTB.Find(conSecStartTag, 0,                ' Find location of the first Section Start-Tag '<ObjectList ObjectType='
                                  conQuickNoH)
            While (srchPos > 0)                                     ' We found a Section Start-Tag
                secNum += 1                                         ' Found next Section
                eTag = srcRTB.Find(">",                             ' Set eTag to be its ending ">" character
                                   srchPos + conSecStartTag.Length,
                                   conQuickNoH)
                If eTag < 0 Then                                    ' No closing ">"?
                    DispMsg(lclProcName, conMsgCrit,
                            "Failed to locate closing '>' of Section Start-Tag from position " & srchPos & vbCrLf &
                            "Possible ill-formed ODF")
                Else
                    srcRTB.Select(srchPos,                          ' Select the Section Title
                                (eTag - srchPos) + 1)
                    srcRTB.SelectionFont =                          ' Make Title's font-size relative to present normal size.
                        New Font(conFont,
                                 MAIN.Num_ODFFontSize.Value + conTitleFontInc,
                                 FontStyle.Bold)
                    srcRTB.SelectionColor = conTitleColor           ' And set its color to teh Title Color
                    srcRTB.DeselectAll()                            ' Deselect the Title Text
                End If

                If enumSecs Then                                    ' verif dans Rtb_DescText; if enumerating, add a Section Info Line to Rtb_DescText
                    secNameStart =
                        srchPos + conSecStartTag.Length + 1         ' First char after 'ObjectType="', i.e. start of the Section Name
                    secNameEnd = srcRTB.Find("""",                  ' Closing double-quote is end of Attribute Value
                                             secNameStart,          ' Begin search from opening char of Section Name
                                             conQuickNoH)
                    If secNameEnd < 0 Then
                        DispMsg(lclProcName, conMsgExcl,
                                "Did not find closing quotes at end of Section Name. Search began at position: " & secNameStart & vbCrLf &
                                "Possible ill-formed ODF.")
                        secName = ""
                    Else                                            ' Found the Name, extract it
                        secName = srcRTB.Text.Substring(secNameStart,
                                                        (secNameEnd - secNameStart))
                    End If

                    secEnd = srcRTB.Find(conSecEndTag,              ' Find the Section End-Tag "</ObjectList>"
                                         secNameStart,
                                         conQuickNoH)
                    If secEnd < 0 Then                              ' Did not find it, warn
                        DispMsg(lclProcName, conMsgExcl,
                                "Did not find Section End-Tag. Search began at position: " & secNameStart & vbCrLf &
                                "Possible ill-formed ODF.")
                        secEnd = 0
                    Else
                        secEnd += conSecEndTag.Length               ' Add length of Tag to get position of closing <CR>
                    End If

                    MAIN.Rtb_DescText.Text +=                       ' Use tabs to left align content; String.Format to format & embed values
                            String.Format("{0,5:N0}{1,1}{2,-1:N0}{3,1}{4,2}{5,1}{6,-1:N0}{7,1}{8,-1:N0}{9,1}{10,1}{11,1}{12,-1:N0}{13,1}{14,-1}",
                                          secNum, vbTab,
                                          srcRTB.GetLineFromCharIndex(srchPos) + 1, vbTab, "to", vbTab,
                                          srcRTB.GetLineFromCharIndex(secEnd) + 1, vbTab,
                                          srchPos, vbTab, "to", vbTab, secEnd, vbTab, secName) & vbCrLf
                    MAIN.Rtb_DescText.Refresh()                     ' Repaint the progress description
                    eTag = Max(eTag, secEnd)                        ' Begin search for next section from farthest known location in previous one
                End If
                srchPos = srcRTB.Find(conSecStartTag, eTag,         ' Starting from the end of the present Title, find the next one
                                      conQuickNoH)
            End While

            HotClickCursorPosition(MAIN.Lbl_CursorPosVal.Tag)       ' <1.060.6> Reposition to original cursor position, as text was scrolled by this routine: use .Tag
            MAIN.Btn_Led.BackColor = Color.LightGreen               ' Set Led color to Green to indicate completion of enumeration
            MAIN.Btn_Led.Refresh()
            If enumSecs Then                                        ' If we listed Sections in Descriptive Text, enable Print Menu
                MAIN.Menu_PrintDT.Enabled = True
            End If
        End If

    End Sub
    Friend Sub TakeRowAction(packagePath As String,         ' <1.060.2> Main directory where all organ packages are located
                            imageFileName As String,        ' <1.060.2> Image File Name, as parsed from an ODF Row Element (Tag)
                            imageSetID As String,           ' <1.060.2> ImageSet index (PK into ImageSet), non-blank iff source is an ImageSetElement
                            packageID As String,            ' <1.060.2> PackageID, non-blank iff source is an ImageSet
                            minPanelHeight As Integer       ' <1.060.2> Original Panel Height settings, to restore size onscreen           
                            )                               ' AFFICHE L'IMAGE (OU LE MASK)

        ' Purpose:      Based on the RowAction button's display text, choose an Action to perform: Display an Image;
        '               Trace a Sample;
        ' Process:		For a Display Image: if the source Row is from an ImageSet (a Mask File), then the parser will
        '               already have saved the PackageID & FileName taken from the Values of the appropriate Tags.
        '               Along with the PackagePath, these can be assembled into a fully qualified filename. If the source
        '               is an ImageSetElement (an Image File), then the parser will have extracted the FileName and the
        '               ImageSetID, so locate the referenced ImageSet based on that ID, and extract the PackageID. Then construct
        '               the completely qualified filname with full path. The image is loaded and displayed in a dedicated form.
        '               On the ImageDisp form, fill in the filename, the PackageID, and the Image size fields.

        '               If the action to be performed is a Sample Trace, dispatch the Trace form, pass it the SampleID, and
        '               emulate pushing that form's "Trace Sample" button.
        ' Called By:    Btn_RowAction_Click
        ' Side Effects: Alters image file globals, panel size, and paints image onscreen.
        ' Notes:        Presently limited to bitmap files (.bmp). Need to also process .png, possibly .jpg; Improve exception handling.
        ' Updates:      <1.060.2> Added unified message handling. Used Path.Combine to build OS independant file paths. Skip the
        '               intermediate Bitmap object, just load Image File directly into the Pbox Control. Added exception handling.
        '               Accept all required info from parameters (or MAIN form itself), eliminate all direct references to globals.
        '               Relocate here from MAIN form.
        '               <1.060.5> Move Image presentation to its own form, so main-form controls & titles can remain static. Moved
        '               PBox (image display area), Lbl_ImageTitle (filename), Lbl_PackageID (package); added Image Size display.
        '               Decide Action using Case statement.

        Const lclProcName As String = "TakeRowAction"               ' <1.060.2> Routine's name for message handling

        Dim url As String                                           ' Full path\filename
        Dim pID As String                                           ' <1.060.2> Becomes the PackageID, either from passed parm, or from search of the parent ImageSet Row

        Select Case MAIN.Btn_RowAction.Text                         ' <1.060.5> Choose a contextual action based on the ActionButtons display text
            Case conTraceSample                                     ' <1.060.5> It's a Trace Sample command...
                Trace.Visible = False
                Trace.Show(MAIN)
                Trace.Txt_SampleID.Text = G_SampleID                ' <1.060.2> Preload the current SampleID
                TraceSample(Val(Trace.Txt_SampleID.Text),
                        Trace.Rtb_Trace)                            ' <1.060.2> Call TraceSample to execute Trace in the Follow form

            Case conDisplayImage                                    ' <1.060.5> It's a Display Image command...
                If packageID = "" Then                              ' <1.060.2> Search for the PackageID if we don't already have it passed in as a parameter
                    pID = FindImagePackageID(imageSetID)            ' identifie d'abord le package; Populate pID from the ImageSet where ImageSetID = [imageSetID]
                Else
                    pID = packageID                                 ' <1.060.2> ImageSet Row, so we have the PackageID directly, from the XML, passed in through [packageID]
                End If
                If pID = "" Then
                    DispMsg(lclProcName, conMsgExcl,
                            "Unable to determine a PackageID to locate the Image File: " & imageFileName & vbCrLf &
                            "imageSetID is: " & imageSetID & vbCrLf &
                            "packageID is: " & packageID)
                    Return                                          ' <1.060.2> Do nothing, the PackageID could not be located, probably a corrupt ODF
                End If

                With ImageDisp                                      ' <1.060.5> Subset of original code, now updates image-related fields on ImageDisp form instead of Main
                    .Lbl_PackageID.Text = "PackageID = " & pID      ' <1.060.5> Moved PackageID field from main form to ImageDisp form
                    url = Path.Combine(packagePath, pID,            ' Build url to be entire path\filename of the image to be displayed
                                       imageFileName)               ' <1.060.2> Changed from concatnating strings to Path.Combine for improved OS independance
                    .Show()                                         ' <1.060.5> Make for visible (it may already be, that's OK)
                    .Lbl_ImageTitle.Text = "Image File: " & url     ' <1.060.5> Present full path/filename of the image file
                    Try                                             ' <1.060.5> Protect against file access problems
                        .PBox.Image = Image.FromFile(url)           ' <1.060.5> Load the pic, PBox is Auto-sizing
                        .Lbl_ImageSize.Text = "Image Size: " &      ' <1.060.5> Present the image's width and height 
                            .PBox.Width & " pixels wide, " &
                            .PBox.Height & " pixels tall."

                    Catch ex As Exception
                        DispMsg(lclProcName, conMsgExcl,
                               "Unable to display an Image file." & vbCrLf &
                               "Requested URL was: " & url & vbCrLf &
                               "Exception Code Is: " & ex.Message)
                    End Try
                End With

            Case Else                                               ' <1.060.5> Unknown command
                DispMsg(lclProcName, conMsgCrit,
                        "Unknown Row Action Button Value: " &
                        MAIN.Btn_RowAction.Text)
        End Select

    End Sub
    Friend Function FindImagePackageID(imgSetID As String           ' <1.060.2> PK of ImageSet Row containing the PackageID we want
                                       ) As String                  ' CHERCHE LE PACKAGEID D'UNE IMAGE D'IMAGE SET ELEMENT (1.060.2> Returns PackageID

        ' Purpose:      Return the PackageID from the ImageSet where ImageSetID=[imgSet], as 6 char long field
        '               left-padded with zeros. Returns "" if the ImageSet or PackageID are not found.
        ' Process:		Search for ImageSet <a> Start-Tag within the ImageSet Section, whose <a> Tags are
        '               the primary keys. If found, search backwards for the Row opening "<o>", then forwards
        '               for the Row's closing "</o>", establishing the limits of the row. Then, from the Row's
        '               start, search forward for the "<c>" Start-Tag, ensuring it's within this Row. If found, locate
        '               the End-Tag "</c>", and extract the content between - this is the PackageID we need.
        '               Pad the PackageID with leading zeroes to length 6, this string is now the useful PackageID
        '               subdirectory name.
        ' Called By:    TakeRowAction()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      Replaced parsing algorithm to not depend upon assumption that <c> Tags come after the
        '               <a> Tag. Added error messaging logic. Rewrite as a function, rather than setting global
        '               variables. Relocated here from MAIN form. Changed RTB searches from .None to .NoHighlight
        '               for execution speed. Fetch ImageSet Section location dynamically by calling GotSectionDataByName(),
        '               instead of using deprecated S_Sections array.

        Const lclProcName As String = "FindImagePackageID"          ' <1.060.2> Routine's name for message handling

        Dim tagStartC As Integer                                    ' Opening "<" char of the "c" Element's Start & End Tags ("<c>" & "</c>")
        Dim tagEndC As Integer
        Dim tagStartRow As Integer                                  ' <1.060.2> Opening "<" char of the "o" Start & End Tags: define the limits of the Row
        Dim tagEndRow As Integer
        Dim packageID As String
        Dim sec As Str_Section                                      ' <1.060.2> IamgeSet Section data, filled in by call to GetSectionDataByName()
        Dim retStatus As Integer

        sec.name = ""                                               ' <1.060.2> Allocate instance of the Section info structure
        retStatus = LocateRecRowByKey(conImageSet,                  ' <1.060.2> Searching the Sample Section
                                      "a",                          ' <1.060.2> Its Primary Key is an "a" tag (SampleID)
                                      imgSetID,                     ' <1.060.2> This is the Primary Key Value we want to find
                                      sec,                          ' <1.060.2> Return Section data here
                                      tagStartRow,                  ' <1.060.2> Return position of (found) Record's Start-Tag here
                                      tagEndRow)                    ' <1.060.2> Return position of (found) Record's End-Tag here
        If retStatus = -2 Then                                      ' <1.060.2> Section not found
            Return ""
        End If
        If retStatus <> 1 Then                                      ' <1.060.2> Got Section, but no Record
            DispMsg(lclProcName, conMsgExcl,                        ' <1.060.2> Implies FK from ImageSetElement has no matching PK in the ImageSet Section
                   "Unable to locate the ImageSet Row with ImageSetID Element (primary key): '<a>" & imgSetID & "</a>'" & vbCrLf &
                   "This may be an error in the ODF, or in the AECHO code.")
            Return ""
        End If

        With MAIN                                                   ' <1.060.2> To reference the MAIN form
            tagStartC = .Rtb_ODF.Find(conCStartTag,                 ' <1.060.2> Having located the outer boundaries of the desired Row, find the "<c>" Start Tag
                                      tagStartRow,
                                      tagEndRow,
                                      conQuickNoH)
            If tagStartC < 0 Then
                DispMsg(lclProcName, conMsgExcl,                    ' <1.060.2> Implies "<c>" Tag (PackageID) is missing in ImageSet Row
                        "Unable to locate the PackageID Start-Tag " & conCStartTag & " from ImageSetID: '<a>" & imgSetID & "</a>'" & vbCrLf &
                        "This may be an error in the ODF, or in the AECHO code.")
                Return ""                                           ' Didn't find the PackageID Start-Tag, return empty-handed...
            End If

            tagEndC = .Rtb_ODF.Find(conCEndTag,                     ' <1.060.2> From the end of the "<c>" Start-Tag, locate the matching "</c>" End-Tag, within the Row
                                    tagStartC + conCStartTag.Length,
                                    tagEndRow,
                                    conQuickNoH)
            If tagEndC < 0 Then
                DispMsg(lclProcName, conMsgExcl,                    ' <1.060.2> Implies "<c>" Tag (PackageID) is missing in ImageSet Row
                       "Unable to locate the PackageID End-Tag " & conCEndTag & " from ImageSetID: '<a>" & imgSetID & "</a>'" & vbCrLf &
                       "This may be an error in the ODF, or in the AECHO code.")
                Return ""                                           ' Didn't find the matching PackageID End-Tag, return empty-handed...
            End If

            packageID = .Rtb_ODF.Text.Substring(                    ' lire le tag <c> = packageID; Select the text between '<c>' & '</c': this is the PackageID
                (tagStartC + conCStartTag.Length),                  ' <1.060.2> Pad with leading zeroes to length 6
                (tagEndC - (tagStartC + conCStartTag.Length))).PadLeft(6, "0")

        End With

        Return packageID

    End Function
    Friend Sub SetRTBDescButtons(enabled As Boolean)    ' <1.060.2> Enable/Disable RTBDesc Controls

        ' Purpose:      Enable or disable the "Set Font" and "Save Description" buttons
        ' Process:		Make assignment
        ' Called By:    Menu_Help_Click(); ResetToNoODF(); ParseSections(); LoadRTFFile()
        ' Side Effects: Changes "SetFont" and "Save Description" Control's states
        ' Notes:        Placeholder for logic associated with uniform management of controls relating
        '               to Section Descriptive Text.
        ' Updates:		<1.060.2> New with this version.

        Const lclProcName As String =                   ' <1.060.2> Routine's name for message handling
            "SetRTBDescButtons"

        MAIN.Btn_SetFont.Enabled = enabled
        MAIN.Btn_SaveDescText.Enabled = enabled
        MAIN.Menu_PrintDT.Enabled = enabled

    End Sub
    Friend Sub SetODFButtons(enabled As Boolean)    ' <1.060.2> Enable/Disable Buttons/Controls that depend on ODF Text presence

        ' Purpose:      Enable or disable Controls that depend upon the presence of ODF Text
        ' Process:		Make assignment
        ' Called By:    Menu_OpenHauptwerkOrgan_Click; ResetToNoODF();
        ' Side Effects: Changes Control's States
        ' Notes:        Placeholder for logic associated with uniform management of controls relating
        '               to ODF management.
        ' Updates:		<1.060.2> New with this version.

        Const lclProcName As String =               ' <1.060.2> Routine's name for message handling
            "SetODButtons"

        MAIN.Num_ODFFontSize.Enabled = enabled      ' <1.060.2> Adjust ODF Font-Size spinner

        MAIN.Btn_NextLine.Enabled = enabled         ' <1.060.2> Next/Prev 1, 10, and 100 Line Buttons
        MAIN.Btn_PrevLine.Enabled = enabled
        MAIN.Btn_Next10Lines.Enabled = enabled
        MAIN.Btn_Prev10Lines.Enabled = enabled
        MAIN.Btn_Next100Lines.Enabled = enabled
        MAIN.Btn_Prev100Lines.Enabled = enabled

        MAIN.Btn_Marker1.Enabled = enabled          ' <1.060.2> The Marker Buttons
        MAIN.Btn_Marker2.Enabled = enabled          ' <1.060.2> The Marker Buttons
        MAIN.Btn_Marker3.Enabled = enabled          ' <1.060.2> The Marker Buttons
        MAIN.Btn_Marker4.Enabled = enabled          ' <1.060.2> The Marker Buttons

        MAIN.Btn_FindFirst.Enabled = enabled        ' <1.060.2> Text Search Buttons
        MAIN.Btn_FindNext.Enabled = enabled
        MAIN.Btn_FindPrev.Enabled = enabled

        MAIN.Btn_Led.Enabled = enabled              ' <1.060.2>

    End Sub
    Friend Function GetSectionFromIndex(index As Integer,           ' TROUVE LA SECTION DANS LAQUELLE SE TROUVE LE CARET; location in ODF
                                        ByRef secStart As Integer,  ' <1.060.2> Return dynamic Section Start/End indices
                                        ByRef secEnd As Integer,
                                        ByRef secStartLine As Integer,
                                        ByRef secEndLine As Integer
                                        ) As String                 ' <1.060.2> Return the Section Name as the function result

        ' Purpose:      Retrieve Section info of the Section containing the character at [index] in the ODF:
        '               Section Name, Start, and End.
        ' Process:      Interogate the Status-Bar to determine if the Row-Type is extra-Sectional. If so
        '               set the name to "None" and set Start and End to the immediate extra-Sectional text. If not,
        '               starting from position [index] in Rtb_ODF, search backwards for the Section's Start-Tag,
        '               and extract the Section's Name from the Attribute. Determine its Start/End positions by
        '               scanning.
        ' Called By:    Rtb_ODF_MouseDoubleClick(); Btn_NextLine_Click();
        '               OldGetSectionFroMenu() (deprecated)
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> Changed search subroutine from FindMyText() to the re-written FindText(). Relocated here
        '               from MAIN form; added MAIN references. Changed to Function that returns the Section Name,
        '               removing reference to G_SectionName, and letting caller update the screen. Added code to
        '               locate Section if we are at it (searching backwards would miss it if we are exactly on
        '               the Section Start-Tag Line.) Added code to hangle positioning in the XML-Header, and the
        '               ODF Start/End Tags.
        '               <1.060.6> Added returning of Starting & Ending Lines, in 1-based form

        Const lclProcName As String =                               ' <1.060.2> Routine's name for message handling
            "GetSectionFromIndex"

        Dim sLen As Integer = conSecStartTag.Length                 ' <1.060.2> Length of Start-Tag leadin text
        Dim belongend As Integer                                    ' XML Section Start-Tag's ending character position

        If MAIN.Rtb_ODF.TextLength = 0 Then
            secStart = 0
            secEnd = 0
            secStartLine = 0                                        ' <1.060.6> Added Line Nums to return
            secEndLine = 0
            Return conDefSectionName                                ' MODIF V058; if no ODF text at all, just exit
        End If
        If MAIN.Status_RowTypeVal.Text = conRowTypeODFEnd Then      ' <1.060.2> In final </Hauptwerk> tag
            secStart = MAIN.Rtb_ODF.GetFirstCharIndexOfCurrentLine  ' <1.060.2> Start is first char of this line
            secStartLine =                                          ' <1.060.6> Convert to Line #
                MAIN.Rtb_ODF.GetLineFromCharIndex(secStart) + 1
            secEnd = MAIN.Rtb_ODF.TextLength - 1                    ' <1.060.2> End is end-of-ODF
            secEndLine = MAIN.Rtb_ODF.Lines.Count().ToString(conIntFmt)
            Return conDefSectionName                                ' <1.060.2> Section is "None"
        End If
        If (MAIN.Status_RowTypeVal.Text = conRowTypeXMSHdr) Or      ' <1.060.2> In XML-Header or ODF Start-Tag
            (MAIN.Status_RowTypeVal.Text = conRowTypeODFStart) Then
            secStart = 0                                            ' <1.060.2> Start is first char of ODF
            secStartLine = 1                                        ' <1.060.6> And this is also the first Line (one-based)
            secEnd = FindText(conSecStartTag, 1, MAIN.Rtb_ODF) - 1  ' <1.060.2> End is just before start of first regular Section
            secEndLine =                                            ' <1.060.6> Calculate Last Line from Last Char
                MAIN.Rtb_ODF.GetLineFromCharIndex(secEnd) + 1
            Return conDefSectionName                                ' <1.060.2> Section is none
        End If


        If ((MAIN.Rtb_ODF.TextLength - index) >= sLen) AndAlso
            (MAIN.Rtb_ODF.Text.Substring(index, sLen) = conSecStartTag) Then
            secStart = index                                        ' <1.060.2> We are precisely on top of the Tag, don't search back for it
        Else
            secStart = FindReverse(conSecStartTag,                  ' retrouver la section auquel il appartient; search backwards for Section Start-Tag, we are inside the Section
                                      index,
                                      MAIN.Rtb_ODF)                 ' <1.060.2> Added source of text to be searched as a parameter) 
        End If

        If secStart = -1 Then                                       ' Did not find any Section Start-Tag, return default
            secStart = 0                                            ' <1.060.2> When not found, set Start/End to zero
            secEnd = 0
            secStartLine = 0                                        ' <1.060.6> Added line nums to return
            secEndLine = 0
            Return conDefSectionName
        End If

        belongend = FindText(">", secStart, MAIN.Rtb_ODF)           ' Locate end of Start-Tag <1.060.2> Changed FindMyText to FindText
        secEnd = FindText(conSecEndTag,                             ' <1.060.2> Find Section End-Tag, add length of Tag to get End of Section
                          belongend + 1,
                          MAIN.Rtb_ODF) + conSecEndTag.Length
        secStartLine =                                              ' <1.060.6> Convert to Line #
                MAIN.Rtb_ODF.GetLineFromCharIndex(secStart) + 1
        secEndLine =                                                ' <1.060.6> Calculate Last Line from Last Char
                MAIN.Rtb_ODF.GetLineFromCharIndex(secEnd) + 1
        Return MAIN.Rtb_ODF.Text.Substring(secStart + sLen + 1,     ' <1.060.2> Extract the Section's Name
                                           (belongend - secStart) - (sLen + 2))

    End Function
    Friend Function GetRowFromIndex(index As Integer,                   ' Position within ODF
                                    ByRef lineNum As Integer,           ' <1.060.2> Return Line Number, 0-based
                                    ByRef lineStart As Integer,         ' <1.060.2> Return Start of Line
                                    expandSelectionToRow As Boolean     ' <1.060.2> If true, select/highlight entire row; otherwise leave as user chose
                                    ) As String                         ' <a.060.2> Text comprising the Row, "" if not located

        ' Purpose:      Extract the Row that contains the character position [index]. This row may consist of multiple
        '               text-lines in Rtb_ODF. Select/Highlight the Row's text.
        ' Process:      Locate the present physical line, interogate Rtb_ODF for character position of first & last
        '               characters of this line. Select and retrieve this text to return as Function's value. If the
        '               ending characters are not the End-Tag for Element Type "o" ("</o>"), extend selection to next
        '               line; if opening chars are not the "o" Start-Tag ("<o">, extend start to previous line.
        ' Called By:    Rtb_ODF_MouseDoubleClick(); Rtb_ODF_MouseClick(); Btn_NextLine_Click(); FindButtonsProc();
        '               OldGetSectionFromMenu() (deprecated)
        ' Side Effects: Updates various Global Variables; updates onscreen fields for Line Number, Start, and
        '               End positions. Highlights (selects) Row text in ODF.
        ' Notes:        Remove assumption that a record-row has no leading/trailing white-space,
        '               use "<o>...</o>" Tag-pair to bound the limits.
        ' Updates:      <1.060.2> Add 1 to displayed Line Number, to offset zero-based internal Line Numbers. Relocated
        '               here from MAIN form; added MAIN references. Renamed from GetRowFromIndex(). Changed to a
        '               Function that returns the Row's content, rather than saving it to the global G_LineText.
        '               Detect Row type of the Line, update the Status-Bar. Only search for multiple-Lines in a Row
        '               if it is a Record type; otherwise, Row=Line. Added code to conditionalize selection behavior:
        '               double-click and Next/Prev 1/10/100 Lines will expand selection to entire Row, while single-click
        '               and Text Searches will leave selection as user set it.
        '               <1.060.6> Save character positions in .Tag properties of Position Fields, for repositioning

        Const lclProcName As String = "GetRowFromIndex"                 ' <1.060.2> Function's name for message handling

        Dim rowText As String                                           ' <1.060.2> Store the extracted Row text here
        Dim rowType As String                                           ' <1.060.2> Description of type of Row the Line is in
        Dim rowStart As Integer                                         ' <1.060.2> Start of Row
        Dim rowLineStart As Integer                                     ' <1.060.6> Starting Line of Row, derived from Staring Char
        Dim rowExtStart As Integer                                      ' <1.060.2> Extended Start of Row, when opening <o> Tagis on a previous Line
        Dim rowEnd As Integer                                           ' <1.060.2> End of Row
        Dim rowLineEnd As Integer                                       ' <1.060.6> Ending Line of Row, derived from End Char
        Dim rowExtEnd As Integer                                        ' <1.060.2> Extended End of Row, when closing </o> Tag is on a subsequent Line
        Dim lineEnd As Integer                                          ' <1.060.2> End of Line

        With MAIN
            lineNum = .Rtb_ODF.GetLineFromCharIndex(index)              ' Map the character Index to a Line Index (=LineNumber-1)
            lineStart = .Rtb_ODF.GetFirstCharIndexFromLine(             ' Find out where the line starts
            lineNum)
            lineEnd = .Rtb_ODF.Find(vbCr,                               ' Locate the end of the line we are presently in 
                                    lineStart,
                                    conQuickNoH)
            rowText = .Rtb_ODF.Text.Substring(lineStart,
                                              (lineEnd - lineStart))    ' <1.060.2> Extract the Line's content, less the terminal <CR>
            rowStart = lineStart                                        ' <1.060.2> For Lines not part of a Record-Row, Row=Line
            rowEnd = lineEnd

            ' MODIF V058.2
            If rowText = "</Hauptwerk>" Then                            ' If the text is the End-Tag of the ODF, present informational message
                rowType = conRowTypeODFEnd
            ElseIf InStr(rowText, "<?XML ", CompareMethod.Text) = 1 Then    ' <1.060.2> Update the Status-Bar with the Row Type
                rowType = conRowTypeXMSHdr
            ElseIf InStr(rowText, "<Hauptwerk ", CompareMethod.Text) = 1 Then
                rowType = conRowTypeODFStart
            ElseIf InStr(rowText, "<ObjectList ", CompareMethod.Text) = 1 Then
                rowType = conRowTypeSecStart
            ElseIf rowText = "</ObjectList>" Then
                rowType = conRowTypeSecEnd
            ElseIf InStr(rowText, vbTab, CompareMethod.Text) = 1 Then
                rowType = conRowTypeGenData
            Else
                rowType = conRowTypeRecord                              ' <1.060.2> If we get here, we need to check for possible multiple-Lines comprising the Row
                '                                                         l'objet <0> ... </o> peut etre sur 2 lignes; logic to handle one Row split across two lines
                Dim line_Ending = rowText.Substring(
            Len(rowText) - 4, 4)                                        ' recherche </o>; grab the End-Tag on this line
                Dim line_starting = rowText.Substring(0, 3)             ' recherche <o>; grab the Start-Tag on this line

                If line_Ending <> conRowEndTag Then                     ' trouver fin de ligne; this Row extends to other Lines
                    rowExtEnd = FindText(conRowEndTag,                  ' <1.060.2> Search for </o> closing Tag
                                     rowEnd + 1,
                                     .Rtb_ODF)
                    If rowExtEnd < 0 Then                               ' <1.060.2> Never found it, just keep Row ending on this Line
                        DispMsg(lclProcName, conMsgExcl,
                            "Did not find subsequent closing </o> tag, will not extend this Line.")
                    Else                                                ' <1.060.2> We did find it, add in the closing Tag, that's our new Row End
                        rowEnd = rowExtEnd + conRowEndTag.Length
                    End If
                End If

                If line_starting <> conRowStartTag Then                 ' The beginning of the row ("<o>" Start-Tag) is in a prior line, need to move the start back
                    rowExtStart = FindReverse(conRowStartTag,           ' <1.060.2> Search backwards for opening <o> Tag, starting from present beginning of Line
                                          rowStart,
                                          .Rtb_ODF)
                    If rowExtStart < 0 Then                             ' <1.060.2> Never found a prior opening Tag, just use the Line Start we have
                        DispMsg(lclProcName, conMsgExcl,
                            "Did not find prior opening <o> Tag, will not extend this Line.")
                    Else
                        rowStart = rowExtStart                          ' <1.060.2> Found it, index already points to start of the Tag
                    End If
                End If
                rowText = .Rtb_ODF.Text.Substring(rowStart,             ' <1.060.2> Extract the extended text
                                                  (rowEnd - rowStart))
            End If

            .Status_RowTypeVal.Text = rowType
            .Lbl_LineNumVal.Text = (lineNum + 1).ToString(conIntFmt)    ' afficher les positions; <1.060.2> add 1, Internal Line #s are 0-based
            .Lbl_LineNumVal.Tag = lineStart                             ' <1.060.6> Save raw character position in .Tag property
            rowLineStart = .Rtb_ODF.GetLineFromCharIndex(rowStart) + 1  ' <1.060.6> Determine the Starting/Ending Lines for the Row from the S/E Chars
            rowLineEnd = .Rtb_ODF.GetLineFromCharIndex(rowEnd) + 1
            .Lbl_RowStartVal.Text = rowLineStart.ToString(conIntFmt) &  ' <1.060.6> Added Line Number component
                " / " & rowStart.ToString(conIntFmt)                    ' Format & update fields for Line & Row Start & End
            .Lbl_RowStartVal.Tag = rowStart
            .Lbl_RowEndVal.Text = rowLineEnd.ToString(conIntFmt) &      ' <1.060.6> Added Line Number component
                " / " & rowEnd.ToString(conIntFmt)
            .Lbl_RowEndVal.Tag = rowEnd
            .Lbl_LineStartVal.Text = lineStart.ToString(conIntFmt)
            .Lbl_LineStartVal.Tag = lineStart
            .Lbl_LineEndVal.Text = lineEnd.ToString(conIntFmt)
            .Lbl_LineEndVal.Tag = lineEnd
            If expandSelectionToRow Then                                ' <1.060.2> if False (single-click and Find/Next/Prev) leave user's selection alone
                .Rtb_ODF.Select(rowStart, rowEnd - rowStart)            ' <1.060.2> Select entire row, used for double-click and Next/Prev 1/10/100 Lines
            End If

            Return rowText
        End With

    End Function
    Friend Function FindReverse(text As String,                 ' Text string to search for
                                start As Integer,               ' Starting location for search, searches backwards from 'start' towards beginning of Rtb_ODF
                                srcText As RichTextBox          ' Any RTB Control, it holds the text to be searched
                                ) As Integer                    ' Index to first character of found string, -1 if not found

        ' CHERCHE UN TEXTE DE BAS EN HAUT

        ' Purpose:      Search backwards in [srcText] for [text], beginning from position [start], returning
        '               the position of the first character of [text] if found
        ' Process:      Validate parameters, use RichTextBox Find Method, Reverse option, to execute search.
        ' Called By:    Rtb_ODF_MouseDoubleClick(); GetSectionFromIndex(); Btn_NextLine_Click(); FindButtonsProc()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> Modified search to include .NoHighlight, to improve execution time and reduce screen flashing.
        '               Relocated here from MAIN form; added MAIN references. Generalized to work with any
        '               RTB Control, passed in as a parm.

        Const lclProcName As String = "FindReverse"             ' <1.060.2> Function's name for message handling.

        Dim searchResult As Integer                             ' <1.060.2> Result of the Find

        If (srcText.TextLength = 0) Or                          ' MODIF V058; to avoid searching an empty ODF, 
            (start >= srcText.TextLength) Then                  ' <1.060.2> Make sure start is within Control
            Return -1
        End If
        If text.Length <= 0 Then
            DispMsg(lclProcName, conMsgExcl,
                    "Function called with null search text.")
            Return -1
        End If
        If start < 0 Then
            DispMsg(lclProcName, conMsgExcl,
                    "Function called with start of search before beginning." & vbCrLf &
                    "start is: " & start)
            Return -1
        End If

        searchResult = srcText.Find(text,                       ' Obtain the location of the search string in the Control
                                    0,                          ' <1.060.2> Changed from 1 to 0, this is a zero-based control
                                    start,
                                    conSrchRev + conQuickNoH)
        If searchResult > start Then                            ' <1.060.2> Prevent continuation wrapping-around from ODF start to end of ODF
            Return -1
        Else
            Return searchResult
        End If

    End Function
    Friend Function MoveToPosition(curPos As Integer,               ' Move cursor to this position
                                   ByRef lineIndex As Integer,      ' Return the new internal Line Number
                                   ByRef lineStart As Integer,      ' Return the start of the new Line
                                   notMouseClick As Boolean,        ' True -> was Mouse click, do not position cursor (that would erase a select)
                                   expandSelectionToRow As Boolean  ' Pass-through - expand selection to entire Row, or leave as set
                                   ) As String                      ' Returns the entire Row's text

        ' Purpose:      Move to the cursor position, update on screen, update Line/Row data, return Row Text
        ' Process:		Update the cusrsor position if not a Mouse-click, then let GetRowFromIndex() do the work
        ' Called By:    Lbl_LineNumVal_Click(); Btn_NextLine_Click(); Btn_Markers_MouseDown();
        '               Rtb_ODF_MouseClick(); Rtb_ODF_MouseDoubleClick(); HotClickCursorPosition();
        '               Lbl_LineNUmVal_Click()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.
        '               <1.060.6> Save cusor position as .Tag property, for repositioning

        Const lclProcName As String =                           ' Routine's name for message handling
            "MoveToPosition"

        MAIN.Rtb_ODF.Focus()                                    ' Give the ODF focus
        If notMouseClick Then                                   ' Move the cursor to its new position
            MAIN.Rtb_ODF.Select(curPos, 0)                      ' If not, then cursor/selection is already set, leave it alone
        End If
        MAIN.Lbl_CursorPosVal.Text =
            curPos.ToString(conIntFmt)                          ' Update cursor position onscreen
        MAIN.Lbl_CursorPosVal.Tag = curPos                      ' <1.060.6> Save position in the .Tag property, for later repositioning
        Return GetRowFromIndex(curPos,                          ' Position to the Line in the ODF at position "caret"
                               lineIndex,                       ' Returns Line Number, 0-based, pass back to caller
                               lineStart,                       ' Returns index to beginning of Line, pass back to caller
                               expandSelectionToRow)            ' Expand Selection to entire Row, or leave as previsouly set
    End Function
    Friend Function GotSectionDataByName(secName As String,             ' Section to locate
                                         ByRef secData As Str_Section   ' Returns this structure data if found
                                         ) As Boolean                   ' True -> Found the Section, filled in the data
        Const lclProcName As String = "GotSectionDataByName"

        Dim secTag As String                                            ' Build full Section Start-Tag here
        Dim secStart As Integer                                         ' Index to Section Start-Tag
        Dim secEnd As Integer                                           ' Index to Section End-Tag
        Dim rowStart As Integer                                         ' Index to <o> Start-Tag, -1 if it doesn't exist
        Dim rowEnd As Integer                                           ' Index to Row End-Tag "</o>"

        With MAIN.Rtb_ODF                                               ' For easier reference
            If secName = "" Or (.TextLength = 0) Then
                DispMsg("", conMsgInfo,
                        "Either no Section Name or no ODF" & vbCrLf &
                        "Requested Section: " & secName & vbCrLf &
                        "ODF Length: " & .TextLength.ToString(conIntFmt))
                Return False
            End If

            secData.name = secName                                      ' The Name field (this one is easy)
            secTag = conSecStartTag & """" & secName & """>"            ' Build Start-Tag: '<ObjectList ObjectType="secName">'
            secStart = .Find(secTag, 0, conQuickNoH)                    ' Locate the Section Start-Tag
            If secStart < 0 Then
                DispMsg(lclProcName, conMsgExcl,
                        "Did not find Section Start-Tag for Section: " & secName & vbCrLf &
                        "Start-Tag string is: " & secTag & vbCrLf &
                        "May be either an ill-formed ODF or an AECHO error.")
                Return False
            End If
            secData.startPos = secStart                                 ' Add the starting location to the return data structure
            secData.titleLen = secTag.Length                            ' Take length directly from Tag

            secEnd = .Find(conSecEndTag,
                           secStart,
                           conQuickNoH)                                 ' Find End-Tag, "</ObjectList>"
            If secEnd < 0 Then                                          ' Did not locate an End-Tag
                DispMsg(lclProcName, conMsgExcl,
                        "Did not find Section End-Tag for Section: " & secName & vbCrLf &
                        "Start-Tag string is: " & secTag & vbCrLf &
                        "May be either an ill-formed ODF or an AECHO error.")
                Return False
            End If
            secData.endPos = secEnd + conSecEndTag.Length               ' End is the <CR> after then End-Tag

            rowStart = .Find(conRowStartTag,
                             secStart + 1,                              ' Begin search after Section Start-Tag
                             secEnd,
                             conQuickNoH)                               ' Is there an <o> inside this Section range?
            If rowStart < 0 Then                                        ' No Record-Row, set values to indicate this
                secData.firstRowStart = -1
                secData.firstRowLen = 0
            Else                                                        ' Found an <o> Row Start-Tag in Section
                secData.firstRowStart = rowStart                        ' Record the Row's Start location
                rowEnd = .Find(conRowEndTag, rowStart + 3,              ' Look for matching Row End-Tag
                               secEnd, conQuickNoH)
                If rowEnd < 0 Then                                      ' Nope. Is an error, but we can continue
                    DispMsg(lclProcName, conMsgExcl,
                            "Did not find matching Record-Row End Tag." & vbCrLf &
                            "This may be an ill-formed ODF, will continue.")
                    secData.firstRowStart = -1
                    secData.firstRowLen = 0
                Else
                    secData.firstRowLen = rowEnd - rowStart + 5         ' Got is length, we're done
                End If
            End If
            Return True

        End With

    End Function
    Friend Sub HotClickCursorPosition(hotVal As String)

        ' Purpose:      Position to the index indicated by the value, using single-click semantics.
        ' Process:		If input parm is "NA", just return - no position data. Otherwise, extract
        '               the value as an integer, ensure it is range inside the ODF, then position
        '               to that place using single-click semantics, updating the Data fields onscreen.
        ' Called By:    Lbl_SecStartVal_Click(); Lbl_SecEndVal_Click(); Lbl_LineStartVal_Click();
        '               Lbl_LineEndVal_Click(); Lbl_RowStartVal_Click(); Lbl_RowEndVal_Click();
        '               Lbl_CursorPosVal_Click(); EnumerateSectionsSetFont()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.

        Const lclProcName As String =       ' Routine's name for message handling
            "HotClickCursorPosition"

        Dim lineStart As Integer            ' Throw-away varaible For Call To MoveToPosition()
        Dim curPos As Integer               ' Cursor Position extracted from text input

        If hotVal = "NA" Then               ' If input is "NA", just return
            Return
        End If

        curPos = CInt(hotVal)               ' Grab the integer value
        If (curPos < 0) Or (curPos > (MAIN.Rtb_ODF.TextLength - 1)) Then
            Return                          ' Make sure position is in bounds of current ODF
        End If

        MoveToPosition(curPos,              ' Position to the Line in the ODF at position "caret"
                       G_LineIndex,         ' Returns Line Number, 0-based - update the Global
                       lineStart,           ' Returns index to beginning of Line
                       True,                ' Reposition the cursor
                       False)               ' False -> Do not extend selection on screen, leave as user set it

    End Sub
    Friend Function ReadAndDisplayTag(tag As String,        ' Tag to search for e.g. "a" -> "<a>...</a>"
                                      startPos As Integer,  ' Start of search range
                                      endPos As Integer,    ' End of search range
                                      longTag As String,    ' Long form of Tag's Name
                                      defVal As String,     ' Default value, presented if not found
                                      tBox As RTBPrint      ' Append output to this printable RTB
                                      ) As String           ' Tag's content: "" if Tag not found, or found but has no content

        ' Purpose:      Between the startPos and endPos (usually a Record-Row's limits), locate the
        '               Start/End-Tags based on the supplied tag. If found, add a line to the
        '               output stream naming the short and long forms of the Tag, and its value; if
        '               not found, add a message saying it wasn't located, and display its default
        '               value.
        ' Process:		Create the opening-Tag string, and search for it. If found. create the end-Tag
        '               string, and search for it. If found, extract the content between these two Tags,
        '               and output the "Found" message, adding Default Value clause if there is no content.
        '               If either of those Tag-searches fails, output the "Not Found" message, adding the
        '               Default Value clause.
        ' Called By:    TraceSample()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.2> First code.
        '               <1.060.3> Changed output control from List Box to enhanced RTB, to support printing of Traces

        Dim tagStart As Integer                             ' Position of the Start-Tag
        Dim tagEnd As Integer                               ' Position of the End-Tag
        Dim tagVal As String                                ' Tag's Value content

        If defVal = "" Then                                 ' Expand null DefVal to text
            defVal = "<No Default Value>"
        End If

        tagStart = MAIN.Rtb_ODF.Find("<" & tag & ">",       ' Look for the Start-Tag, within the limiting range
                                     startPos,
                                     endPos,
                                     conQuickNoH)
        If tagStart >= 0 Then                               ' Found it, continue
            tagStart += (2 + tag.Length)                    ' Accounts for the brackets surrounding the Tag
            tagEnd = MAIN.Rtb_ODF.Find("</" & tag & ">",    ' Locate the End-Tag, within the range
                                       tagStart,
                                       endPos,
                                       conQuickNoH)
            If tagEnd >= 0 Then                             ' Found it also, continue
                tagVal =
                    MAIN.Rtb_ODF.Text.Substring(tagStart,   ' Extract the content from between the Tags
                                                (tagEnd - tagStart))
                If tagVal <> "" Then                        ' We have the Tag, and content
                    AppendTxt(tBox, fnt_Fields, Color.Black,
                              "    " & longTag & " (<" & tag & ">) = '" & tagVal & "'" & vbCrLf)
                Else                                        ' We have the Tag, but null content, present Default Value
                    AppendTxt(tBox, fnt_Fields, Color.SaddleBrown,
                              "    " & longTag & " (<" & tag & ">) is present, but has no value. Default Value = '" & defVal & "'" & vbCrLf)
                End If
                Return tagVal                               ' Return the Tag's content, can be ""
            End If
        End If

        AppendTxt(tBox, fnt_Fields, Color.SaddleBrown,
                  "    No tag " & longTag & " (<" & tag & ">), Default Value = '" & defVal & "'" & vbCrLf)
        Return ""                                           ' All or part of the Tag wasn't found

    End Function
    Friend Sub TraceSample(sampleID As Integer,                     ' <1.060.2> Sample to Trace
                           tb As RTBPrint)                          ' <1.060.3> Append output here - a printable RTB

        ' SUIT UN SAMPLE DANS LES DIFFERENTES SECTIONS QU'IL UTILISE

        ' Purpose:      Read a (sound) SampleID from the form, and display the fields related to the
        '               Sample, from the Sample Section and from relationally-linked records in other
        '               Sections: Pipe_SoundEngine01_AttackSamples; Pip_SoundEngine01_ReleaseSamples;
        '               TremulantWaveform; RequiredInstallationPackage.
        ' Process:		Locate the Sample Section. If not found, signal an error and exit. Otherwise,
        '               search for the Record with the requested SampleID. If not found, this is not
        '               an error, just post notice to the ListBox and Exit. If found, display all possible
        '               Tags in their expanded form, including their content and any default values if the
        '               Tag is missing. Retain the value of the InstallationPackageID field, this will be
        '               a Primary Key used later into RequiredInstallationPackages. Generally, the SampleID
        '               is a Foreign-Key contained in the linked Sections. After the Sample itself, all
        '               further Sections are optional: if not found, just proceed with the Next. In order,
        '               attempt to locate and expand Pipe_SoundEngine01_AttackSample (Sample.SampleID = SampleID);
        '               Pipe_SoundEngine01_ReleaseSample (Sample.SampleID = SampleID); TremulantWaveform
        '               (Sample.SampleID = PitchAndFundamentalWaveformID); TremulantWaveform (Sample.SampleID =
        '               ThirdHarmonicWaveformSampleID); RequiredInstallationPackage (Sample.InstallationPackageID =
        '               InstallationPackageID).
        ' Called By:    Btn_TraceSample_Click()
        ' Side Effects: Updates Trace form only
        ' Notes:        <None>
        ' Updates:		<1.060.2> Changed Function calls to simpler in-line text e.g. Find. Hardened parsing logic e.g., when
        '               searching for <a>SampleID</a>, ensure search remains in bounds of the Sample Section, doesn't find some
        '               other <a> tag match in a different Section. Receive SampleID to Trace and Output ListBox as parms.
        '               Call LocateRecRowByKey() to find arbitrary Record inside arbitrary Section by Primary of Foreign Key.
        '               <1.060.3> Modified to support extended RTB as output control, replacing the prior List Box - this is
        '               to allow easy printing. Changed .Item.Add methodes to .AppendText, adde CR/LFs at line ends. Added blank
        '               line between Traces, for visual separation. Use AppendTxt() to control font size, style, and color as
        '               text is appended into the enhanced RTB.

        Const lclProcName As String = "TraceSample"                 ' <1.060.2> Routine's name for message handling

        Dim rowStartTag As Integer                                  ' <1.060.2> Position of Start-of-Row Tag "<o>", starting point for locating specific Fields in the Record
        Dim rowEndTag As Integer                                    ' <1.060.2> Position of End-of-Row Tag, "</o>" to limit searches for Fields within a Record
        Dim sec As Str_Section                                      ' <1.060.2> IamgeSet Section data, filled in by call to GetSectionDataByName()
        Dim retStatus As Integer                                    ' <1.060.2> Return from Record lookup: 1 -> Sec & Rec OK; 0 -> Sec OK, no Rec; -1 -> Sec OK, Rec error; -2 -> No Sec
        Dim reqInstID As String = ""                                ' <1.060.2> Installation Package ID of Sample, if found and Tag exists, "" otherwise

        ' SECTION SAMPLE
        sec.name = ""                                               ' <1.060.2> Allocate instance of the Section info structure
        retStatus = LocateRecRowByKey(conSample,                    ' <1.060.2> Searching the Sample Section
                                      "a",                          ' <1.060.2> Its Primary Key is an "a" tag (SampleID)
                                      sampleID.ToString,            ' <1.060.2> This is the Primary Key Value we want to find
                                      sec,                          ' <1.060.2> Return Section data here
                                      rowStartTag,                  ' <1.060.2> Return position of (found) Record's Start-Tag here
                                      rowEndTag)                    ' <1.060.2> Return position of (found) Record's End-Tag here
        If retStatus = -2 Then                                      ' <1.060.2> Nothing found, there were errors in ODF
            Return
        End If
        If tb.Text.Length > 0 Then
            AppendTxt(tb, fnt_Fields, Color.Black, vbCrLf)          ' <1.060.3> Add a blank line before a Trace for visual seperation, except for first Trace
        End If

        Trace.MenuTSPrint.Enabled = True                            ' <1.060.3> Something printable, so enable the Print menu choice
        If retStatus > -2 Then                                      ' <1.060.2> Though not everything was located, the Section was, so output its record
            TraceAddSection(tb, "", sec)                            ' <1.060.3> Output the Section header: for Sample, no prefix
        End If
        If retStatus = -1 Then                                      ' <1.060.2> The search errored while looking for Record, already displayed dialog
            Return                                                  ' <1.060.2> So just return, can't continue with Record processing
        End If
        If retStatus = 0 Then                                       ' <1.060.2> We found the Sample Section, but not the requested Record. Append notice and return.
            AppendTxt(tb, fnt_Fields, Color.Red,
                      "    No " & sec.name & " Record located with Key " & conFieldSID & " (<a>) = '" & sampleID.ToString & "'" & vbCrLf)
            Return                                                  ' <1.060.2> No errors, but no Record to process - also don't need to continue with other Sections
        End If

        ReadAndDisplayTag("a", rowStartTag, rowEndTag, conFieldSID, "", tb)     ' <1.060.2> Search for Tag "<a>...</a>", (SampleID i.e. the Record Key Field)
        reqInstID = ReadAndDisplayTag("b", rowStartTag, rowEndTag, conFieldIPID, "", tb)    ' <1.060.2> Search for Tag "<b>...</b>", (InstallationPackageID Field), save content
        ReadAndDisplayTag("c", rowStartTag, rowEndTag, conFieldFName, "", tb)   ' <1.060.2> Search for Tag "<c>...</c>", (SampleFilename Field)
        ReadAndDisplayTag("d", rowStartTag, rowEndTag, conFieldPSpec, "1", tb)  ' <1.060.2> Search for Tag "<d>...</d>", (Pitch_SpecificationMethodCode Field)
        ReadAndDisplayTag("e", rowStartTag, rowEndTag, conFieldPRank, "0", tb)  ' <1.060.2> Search for Tag "<e>...</e>", (Pitch_RankBasePitch64ftHarmonicNum Field)
        ReadAndDisplayTag("f", rowStartTag, rowEndTag, conFieldPNorm, "0", tb)  ' <1.060.2> Search for Tag "<f>...</f>", (Pitch_NormalMIDINoteNumber Field)
        ReadAndDisplayTag("g", rowStartTag, rowEndTag, conFieldPEx, "0", tb)    ' <1.060.2> Search for Tag "<g>...</g>", (Pitch_ExactSamplePitch Field)
        ReadAndDisplayTag("h", rowStartTag, rowEndTag, conFieldLic, "0", tb)    ' <1.060.2> Search for Tag "<h>...</h>", (LicenseSerialNumRequiredForSampleFile Field)

        ' SOUND ENGINE 01 ATTACK; see if this section exists

        sec.name = ""                                               ' <1.060.2> New Section
        retStatus = LocateRecRowByKey(conPipe01Attack,              ' <1.060.2> Get the Attack Section
                                      "c",                          ' <1.060.2> This section uses the "c" Tag as a foreign key from Sample
                                      sampleID.ToString,
                                      sec,
                                      rowStartTag,
                                      rowEndTag)
        If retStatus = -2 Then                                      ' <1.060.2> Nothing found, there were errors in ODF
            Return
        End If
        If retStatus > -2 Then                                      ' <1.060.2> Though not everything was located, the Attack Section was, so output its record
            TraceAddSection(tb, "  ", sec)                          ' <1.060.3> Insert the Section Header, indent by 2
        End If
        If retStatus = -1 Then                                      ' <1.060.2> The search errored while looking for Record, already displayed dialog
            Return                                                  ' <1.060.2> So just return, can't continue with Record processing
        End If
        If retStatus = 0 Then                                       ' <1.060.2> We found the Attack Section, but not the requested Record. Append notice and continue with next Section.
            AppendTxt(tb, fnt_Fields, Color.Red,
                      "    No " & sec.name & " Record located with Key " & conFieldSID & " (<c>) = '" & sampleID.ToString & "'" & vbCrLf)
        Else                                                                            ' <1.060.2> Got the Record, display all present and non-present Fields
            ReadAndDisplayTag("c", rowStartTag, rowEndTag, conFieldSID, "", tb)         ' <1.060.2> Search for Tag "<c>...</c>", (SampleID from Sample Section, foreign-key into the Attack Records)
            ReadAndDisplayTag("a", rowStartTag, rowEndTag, conFieldUID, "", tb)         ' <1.060.2> Search for Tag "<a>...</a>", (UniqueID Field)
            ReadAndDisplayTag("b", rowStartTag, rowEndTag, conFieldLayID, "", tb)       ' <1.060.2> Search for Tag "<b>...</b>", (LayerID Field)
            ReadAndDisplayTag("d", rowStartTag, rowEndTag, conFieldLSRSPTC, "1", tb)    ' <1.060.2> Search for Tag "<d>...</d>", (LoadSampleRange_StartPositionTypeCode Field)
            ReadAndDisplayTag("e", rowStartTag, rowEndTag, conFieldLSRSPV, "0", tb)     ' <1.060.2> Search for Tag "<e>...</e>", (LoadSampleRange_StartPositionValue Field)
            ReadAndDisplayTag("f", rowStartTag, rowEndTag, conFieldLSREPTC, "6", tb)    ' <1.060.2> Search for Tag "<f>...</f>", (LoadSampleRange_EndPositionTypeCode Field)
            ReadAndDisplayTag("g", rowStartTag, rowEndTag, conFieldLSREPV, "0", tb)     ' <1.060.2> Search for Tag "<g>...</g>", (LoadSampleRange_EndPositionValue Field)
            ReadAndDisplayTag("h", rowStartTag, rowEndTag, conFieldASCHV, "127", tb)    ' <1.060.2> Search for Tag "<h>...</h>", (AttackSelCriteria_HighestVelocity Field)
            ReadAndDisplayTag("i", rowStartTag, rowEndTag, conFieldASCMTS, "0", tb)     ' <1.060.2> Search for Tag "<i>...</i>", (AttackSelCriteria_MinTimeSincePrevPipeCloseMs Field)
            ReadAndDisplayTag("j", rowStartTag, rowEndTag, conFieldASCHCC, "127", tb)   ' <1.060.2> Search for Tag "<j>...</j>", (AttackSelCriteria_HighestCtsCtrlValue Field)
            ReadAndDisplayTag("k", rowStartTag, rowEndTag, conFieldLCL, "80", tb)       ' <1.060.2> Search for Tag "<k>...</k>", (LoopCrossfadeLengthInSrcSamples<s Field)
        End If

        ' SOUND ENGINE 01 RELEASE; see if this section exists

        sec.name = ""                                               ' <1.060.2> New Section
        retStatus = LocateRecRowByKey(conPipe01Release,             ' <1.060.2> Get the Release Section
                                      "c",                          ' <1.060.2> This section uses the "c" Tag as a foreign key from Sample
                                      sampleID.ToString,
                                      sec,
                                      rowStartTag,
                                      rowEndTag)
        If retStatus = -2 Then                                      ' <1.060.2> Nothing found, there were errors in ODF
            Return
        End If
        If retStatus > -2 Then                                      ' <1.060.2> Though not everything was located, the Release Section was, so output its record
            TraceAddSection(tb, "  ", sec)                          ' <1.060.3> Insert the Section Header, indent by 2
        End If
        If retStatus = -1 Then                                      ' <1.060.2> The search errored while looking for Record, already displayed dialog
            Return                                                  ' <1.060.2> So just return, can't continue with Record processing
        End If
        If retStatus = 0 Then                                       ' <1.060.2> We found the Release Section, but not the requested Record. Append notice and continue with next Section.
            AppendTxt(tb, fnt_Fields, Color.Red,
                      "    No " & sec.name & " Record located with Key " & conFieldSID & " (<c>) = '" & sampleID.ToString & "'" & vbCrLf)
        Else                                                                            ' <1.060.2> Got the Record, display all present and non-present Fields
            ReadAndDisplayTag("c", rowStartTag, rowEndTag, conFieldSID, "", tb)         ' <1.060.2> Search for Tag "<c>...</c>", (SampleID from Sample Section, foreign-key into the Release Records)
            ReadAndDisplayTag("a", rowStartTag, rowEndTag, conFieldUID, "", tb)         ' <1.060.2> Search for Tag "<a>...</a>", (UniqueID Field)
            ReadAndDisplayTag("b", rowStartTag, rowEndTag, conFieldLayID, "", tb)       ' <1.060.2> Search for Tag "<b>...</b>", (LayerID Field)
            ReadAndDisplayTag("d", rowStartTag, rowEndTag, conFieldLSRSPTC, "1", tb)    ' <1.060.2> Search for Tag "<d>...</d>", (LoadSampleRange_StartPositionTypeCode Field)
            ReadAndDisplayTag("e", rowStartTag, rowEndTag, conFieldLSRSPV, "0", tb)     ' <1.060.2> Search for Tag "<e>...</e>", (LoadSampleRange_StartPositionValue Field)
            ReadAndDisplayTag("f", rowStartTag, rowEndTag, conFieldLSREPTC, "6", tb)    ' <1.060.2> Search for Tag "<f>...</f>", (LoadSampleRange_EndPositionTypeCode Field)
            ReadAndDisplayTag("g", rowStartTag, rowEndTag, conFieldLSREPV, "0", tb)     ' <1.060.2> Search for Tag "<g>...</g>", (LoadSampleRange_EndPositionValue Field)
            ReadAndDisplayTag("h", rowStartTag, rowEndTag, conFieldASCHV, "127", tb)    ' <1.060.2> Search for Tag "<h>...</h>", (AttackSelCriteria_HighestVelocity Field)
            ReadAndDisplayTag("i", rowStartTag, rowEndTag, conFieldASCMTS, "0", tb)     ' <1.060.2> Search for Tag "<i>...</i>", (AttackSelCriteria_MinTimeSincePrevPipeCloseMs Field)
            ReadAndDisplayTag("j", rowStartTag, rowEndTag, conFieldASCHCC, "127", tb)   ' <1.060.2> Search for Tag "<j>...</j>", (AttackSelCriteria_HighestCtsCtrlValue Field)
            ReadAndDisplayTag("k", rowStartTag, rowEndTag, conFieldSAA, "Y", tb)        ' <1.060.2> Search for Tag "<k>...</k>", (ScaleAmplitudeAutomatically Field)
            ReadAndDisplayTag("l", rowStartTag, rowEndTag, conFieldDBA, "N", tb)        ' <1.060.2> Search for Tag "<l>...</l>", (DontBypassAmplitudeScalingIfUserDisablesMultipleReleases Field)
            ReadAndDisplayTag("m", rowStartTag, rowEndTag, conFieldPAA, "Y", tb)        ' <1.060.2> Search for Tag "<m>...</m>", (PhaseAlignAutomatically Field)
            ReadAndDisplayTag("n", rowStartTag, rowEndTag, conFieldRCL, "45", tb)       ' <1.060.2> Search for Tag "<n>...</n>", (ReleaseCrossfadeLengthMS Field)
            ReadAndDisplayTag("p", rowStartTag, rowEndTag, conFieldRSCHV, "127", tb)    ' <1.060.2> Search for Tag "<p>...</p>", (ReleaseSelCriteria_HighestVelocity Field)
            ReadAndDisplayTag("q", rowStartTag, rowEndTag, conFieldRSCLKR, "9999", tb)  ' <1.060.2> Search for Tag "<q>...</q>", (ReleaseSelCriteria_LatestKeyReleaseTimeMs Field)
            ReadAndDisplayTag("r", rowStartTag, rowEndTag, conFieldRSCHCC, "127", tb)   ' <1.060.2> Search for Tag "<r>...</r>", (ReleaseSelCriteria_HighestCtsCtrlValue Field)
            ReadAndDisplayTag("s", rowStartTag, rowEndTag, conFieldRSCPTR, "", tb)      ' <1.060.2> Search for Tag "<s>...</s>", (ReleaseSelCriteria_PreferThisRelForAttackID Field)
        End If

        ' Tremulant Waveform; see if this section exists

        sec.name = ""                                               ' <1.060.2> New Section
        retStatus = LocateRecRowByKey(conTremWave,                  ' <1.060.2> Get the TremulantWaveform Section
                                      "d",                          ' <1.060.2> This section uses the "d" Tag as a foreign key from Sample
                                      sampleID.ToString,
                                      sec,
                                      rowStartTag,
                                      rowEndTag)
        If retStatus = -2 Then                                      ' <1.060.2> Nothing found, there were errors in ODF
            Return
        End If
        If retStatus > -2 Then                                      ' <1.060.2> Though not everything was located, the TremulantWaveform Section was, so output its record
            TraceAddSection(tb, "  ", sec)                          ' <1.060.3> Insert the Section Header, indent by 2
        End If
        If retStatus = -1 Then                                      ' <1.060.2> The search errored while looking for Record, already displayed dialog
            Return                                                  ' <1.060.2> So just return, can't continue with Record processing
        End If
        If retStatus = 0 Then                                       ' <1.060.2> We found the Section, but not the requested Record. Append notice and continue with next Section.
            AppendTxt(tb, fnt_Fields, Color.Red,
                      "    No " & sec.name & " Record located with Foreign-Key " & conFieldPAF & " (<d>) = '" & sampleID.ToString & "'" & vbCrLf)
        Else                                                        ' <1.060.2> Got the Record, display all present and non-present Fields
            AppendTxt(tb, fnt_Fields, Color.Black,
                      "  Record located via Foreign-Key " & conFieldPAF & " (<d>)")
            ReadAndDisplayTag("d", rowStartTag, rowEndTag, conFieldPAF, "", tb)         ' <1.060.2> Search for Tag "<d>...</d>", (PitchAndFundamentalWaveformSampleID is foreign key of SampleID)
            ReadAndDisplayTag("a", rowStartTag, rowEndTag, conFieldTWID, "", tb)        ' <1.060.2> Search for Tag "<a>...</a>", (TremulantWaveformID Field)
            ReadAndDisplayTag("c", rowStartTag, rowEndTag, conFieldTID, "", tb)         ' <1.060.2> Search for Tag "<c>...</c>", (TremulantID Field)
            ReadAndDisplayTag("e", rowStartTag, rowEndTag, conFieldTHW, "", tb)         ' <1.060.2> Search for Tag "<e>...</e>", (ThirdHarmonicWaveformSampleID Field)
            ReadAndDisplayTag("f", rowStartTag, rowEndTag, conFieldLCL, "10", tb)       ' <1.060.2> Search for Tag "<f>...</f>", (LoopCrossfadeLengthInSecSampleMs Field)
            ReadAndDisplayTag("g", rowStartTag, rowEndTag, conFieldPOC, "", tb)         ' <1.060.2> Search for Tag "<g>...</g>", (PitchOutputContinuousControlID Field)
        End If

        sec.name = ""                                               ' <1.060.2> New Section - this time we're searching an alternate Foreign-key
        retStatus = LocateRecRowByKey(conTremWave,                  ' <1.060.2> Get the TremulantWaveform Section
                                      "e",                          ' <1.060.2> This section uses the "e" Tag as a foreign key from Sample
                                      sampleID.ToString,
                                      sec,
                                      rowStartTag,
                                      rowEndTag)
        If retStatus = -2 Then                                      ' <1.060.2> Nothing found, there were errors in ODF
            Return
        End If
        If retStatus > -2 Then                                      ' <1.060.2> Though not everything was located, the Section was, so output its record
            TraceAddSection(tb, "  ", sec)                          ' <1.060.3> Insert the Section Header, indent by 2
        End If
        If retStatus = -1 Then                                      ' <1.060.2> The search errored while looking for Record, already displayed dialog
            Return                                                  ' <1.060.2> So just return, can't continue with Record processing
        End If
        If retStatus = 0 Then                                       ' <1.060.2> We found the Section, but not the requested Record. Append notice and continue with next Section.
            AppendTxt(tb, fnt_Fields, Color.Red,
                      "    No " & sec.name & " Record located with Foreign-Key " & conFieldTHW & " (<e>) = '" & sampleID.ToString & "'" & vbCrLf)
        Else                                                        ' <1.060.2> Got the Record, display all present and non-present Fields
            AppendTxt(tb, fnt_Fields, Color.Black,
                      "    Record located via Foreign-Key " & conFieldTHW & " (<e>)" & vbCrLf)
            ReadAndDisplayTag("e", rowStartTag, rowEndTag, conFieldTHW, "", tb)         ' <1.060.2> Search for Tag "<e>...</e>", (ThirdHarmonicWaveformSampleID is foreign key of SampleID)
            ReadAndDisplayTag("d", rowStartTag, rowEndTag, conFieldPAF, "", tb)         ' <1.060.2> Search for Tag "<d>...</d>", (PitchAndFundamentalWaveformSampleID Field)
            ReadAndDisplayTag("a", rowStartTag, rowEndTag, conFieldTWID, "", tb)        ' <1.060.2> Search for Tag "<a>...</a>", (TremulantWaveformID Field)
            ReadAndDisplayTag("c", rowStartTag, rowEndTag, conFieldTID, "", tb)         ' <1.060.2> Search for Tag "<c>...</c>", (TremulantID Field)
            ReadAndDisplayTag("f", rowStartTag, rowEndTag, conFieldLCL, "10", tb)       ' <1.060.2> Search for Tag "<f>...</f>", (LoopCrossfadeLengthInSecSampleMs Field)
            ReadAndDisplayTag("g", rowStartTag, rowEndTag, conFieldPOC, "", tb)         ' <1.060.2> Search for Tag "<g>...</g>", (PitchOutputContinuousControlID Field)
        End If

        If reqInstID = "" Then
            AppendTxt(tb, fnt_Fields, Color.Red,
                      "  Sample does not have a Primary-Key Value for InstallationPackageID." & vbCrLf)
            Return
        End If
        sec.name = ""                                               ' <1.060.2> New Section
        retStatus = LocateRecRowByKey(conReqInstPckg,               ' <1.060.2> Get the RequiredInstallationPackage Section
                                      "a",                          ' <1.060.2> This section uses the "e" Tag as a Primary key from Sample
                                      reqInstID,                    ' <1.060.2> Originally retrieved from the Sample Record up top
                                      sec,
                                      rowStartTag,
                                      rowEndTag)
        If retStatus = -2 Then                                      ' <1.060.2> Nothing found, there were errors in ODF
            Return
        End If
        If retStatus > -2 Then                                      ' <1.060.2> Though not everything was located, the Section was, so output its record
            TraceAddSection(tb, "  ", sec)                          ' <1.060.3> Insert the Section Header, indent by 2
        End If
        If retStatus = -1 Then                                      ' <1.060.2> The search errored while looking for Record, already displayed dialog
            Return                                                  ' <1.060.2> So just return, can't continue with Record processing
        End If
        If retStatus = 0 Then                                       ' <1.060.2> We found the Section, but not the requested Record. Append notice and continue with next Section.
            AppendTxt(tb, fnt_Fields, Color.Red,
                      "    No " & sec.name & " Record located with Key " & conFieldInPkId & " (<a>) = '" & reqInstID & "'" & vbCrLf)
        Else                                                                            ' <1.060.2> Got the Record, display all present and non-present Fields
            AppendTxt(tb, fnt_Fields, Color.Black,
                      "    Record located via Primary-Key " & conFieldInPkId & " (<a>)" & vbCrLf)
            ReadAndDisplayTag("a", rowStartTag, rowEndTag, conFieldInPkId, "", tb)      ' <1.060.2> Search for Tag "<a>...</a>", (InstallationPackageID, PK of this Section, FK from Sample)
            ReadAndDisplayTag("b", rowStartTag, rowEndTag, conFieldName, "", tb)        ' <1.060.2> Search for Tag "<b>...</b>", (Name Field)
            ReadAndDisplayTag("c", rowStartTag, rowEndTag, conFieldSName, "", tb)       ' <1.060.2> Search for Tag "<c>...</c>", (ShortName Field)
            ReadAndDisplayTag("d", rowStartTag, rowEndTag, conFieldPSID, "0", tb)       ' <1.060.2> Search for Tag "<d>...</d>", (PackageSupplierID Field)
            ReadAndDisplayTag("e", rowStartTag, rowEndTag, conFieldSupName, "", tb)     ' <1.060.2> Search for Tag "<e>...</e>", (SupplierName Field)
            ReadAndDisplayTag("f", rowStartTag, rowEndTag, conFieldMPV, "1", tb)        ' <1.060.2> Search for Tag "<f>...</f>", (MinimumPackageVersion Field)
        End If

    End Sub
    Private Sub TraceAddSection(oBox As RTBPrint,                   ' Direct output to this enhanced RTB
            prefix As String,                                       ' Prepend output with this string, "" for Sample, otherwise has blanks to force indent
            sec As Str_Section)                                     ' Structure containing Section data: Name, StartPos, EndPos are needed here

        ' Purpose:      Append a Section Header to the output of a Sample Trace, prefacing with the [prefix]
        '               string, which sets the indent.
        ' Process:		TBD
        ' Called By:    TraceSample()
        ' Side Effects: None
        ' Notes:        <None>
        ' Updates:		<1.060.3> New code. Abstracted repeated code in TraceSample, placed it here.

        Const lclProcName As String = "TraceAddSection"             ' Function's name for message handling

        Dim sLine As Integer                                        ' Section Starting Line #
        Dim eLine As Integer                                        ' Section Ending Line #
        Dim hdrColor As Color                                       ' Color, based on Section Type: Sample is Blue, others Dark Green

        If prefix = "" Then                                         ' Decode color based on prefix: no prefix is for Sample Section -> Blue
            hdrColor = Color.Blue
        Else                                                        ' All other Sections are indented by [prefix], and colored Dark Green
            hdrColor = Color.DarkGreen
        End If

        sLine = MAIN.Rtb_ODF.GetLineFromCharIndex(sec.startPos) + 1
        eLine = MAIN.Rtb_ODF.GetLineFromCharIndex(sec.endPos) + 1   ' Translate the Start/End positions to Line Numbers, +1 to convert from zero-based internal numbering

        AppendTxt(oBox, fnt_Section, hdrColor,
                  prefix & "Section '" & sec.name &                 ' Append to output the Name and Line-Range of the Section defined by [sec]
                  "' (Starts at Line " & sLine.ToString(conIntFmt) &
                  ",  ends at " & eLine.ToString(conIntFmt) & ")" & vbCrLf)
        Return

    End Sub
    Friend Function LocateRecRowByKey(secName As String,                ' Name of Section containing Record we want
                                      fieldTag As String,               ' Tag the defines the Key Field, without the "<>" brackets
                                      recKey As String,                 ' Key Value to look for
                                      ByRef sec As Str_Section,         ' If the Section is located, return data block here; sec.name = "" if not located
                                      ByRef rowStart As Integer,        ' If Record is located, return its starting position here
                                      ByRef rowEnd As Integer           ' If Record is located, return index of its End-Tag ("</fieldTag>")
                                      ) As Integer                      ' 1 -> Sec & Rec found; 0 -> Sec found, no Rec, no error; -1 -> Sec found, Rec error; -2 -> Sec not Found

        ' Purpose:      Locate a specific Record-Row within a named Section, searching for a Primary Key match. Return a
        '               Section data-block and the located Row's Start/End positions.
        ' Process:		TBD
        ' Called By:    TraceSample()
        ' Side Effects: None
        ' Notes:        <None>
        ' Updates:		<1.060.2> New. Abstracted search logic from the original TraceSample code, generalized it here.

        Const lclProcName As String = "LocateRecRowByKey"               ' Function's name for message handling

        Dim recPos As Integer                                           ' Position of located KeyField, if the requested Record is found
        Dim keyTag As String                                            ' Assemble the complete Key in this var

        sec.name = ""                                                   ' Init as "Section Not Found"
        If Not GotSectionDataByName(secName, sec) Then                  ' Didn't find the requested Section
            DispMsg(lclProcName, conMsgExcl,
                    "Could not locate the " & secName & " Section. This may be an ill-formed ODF.")
            Return -2                                                   ' Error return, nothing was found, no good data returned
        End If

        recPos = sec.startPos + sec.titleLen                            ' recPos points to first char of first line following the Section Start-Tag: should be first Record Row
        keyTag = "<" & fieldTag & ">" & recKey & "</" & fieldTag & ">"  ' Assemble the KeyTag: "<fieldTag>recKey</fieldTag>" e.g., "<a>123</a> to find "123" in Field "a"

        recPos = MAIN.Rtb_ODF.Find(keyTag,                              ' Assembled Tag with embedded Value, look for it
                                   recPos,                              ' Start search for Record just after the Section's Start-Tag
                                   sec.endPos,                          ' Limit search to the Section's range, so we don't get an accidental match in a subsequent Section
                                   conQuickNoH)

        If recPos < 0 Then                                              ' Section not found, display message and return
            Return 0                                                    ' Section is OK, but requested Record does not exist
        End If

        rowStart = MAIN.Rtb_ODF.Find(conRowStartTag,                    ' Search backwards to find Record Start-Tag: "<o"> for this Record
                                     sec.startPos + sec.titleLen,       ' Don't go back further than the Section's Start-Tag
                                     recPos,                            ' Start search from the previsouly located Key-Tag
                                     conQuickNoH + conSrchRev)
        If rowStart < 0 Then                                            ' Not found, display message and return
            DispMsg(lclProcName, conMsgExcl,
                    "Could not locate the Record Start-Tag '<o>'. This may be an ill-formed ODF.")
            Return -1                                                   ' Section is OK, but an error while identifying the Row
        End If

        rowEnd = MAIN.Rtb_ODF.Find(conRowEndTag,                        ' Now search forward within the Record Row to find its End-Tag "</o>"
                                      rowStart,                         ' Starting from Row's Start-Tag
                                      sec.endPos,                       ' Limit search to the Section's boundaries
                                      conQuickNoH)
        If rowEnd < 0 Then                                              ' Row End-Tag not found, display message and return
            DispMsg(lclProcName, conMsgExcl,
                    "Could not locate the Record End-Tag '</o>'. This may be an ill-formed ODF.")
            Return -1                                                   ' Section is OK, but an error while identifying the Row
        End If

        Return 1                                                        ' Found Section, Key, and Record boundaries, we're good

    End Function
    Friend Sub AppendTxt(oBox As RTBPrint,              ' Append text to this Control
           txtFont As Font,                             ' Set it to this font
           txtColor As Color,                           ' And this color
           txtVal As String)                            ' Text to add

        ' Purpose:      Append the text to any enhanced RTB control, setting it to the
        '               desired font and color; leave positioned after appended text.
        ' Process:      Position at end, select, set atributes, append text, deselect
        ' Called By:    Follow_Load(); TraceSample()
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:      <1.060.3> First implemented for printing Sample Trace

        Const lclProcName As String = "AppendTxt"       ' Routine's name for message handling

        oBox.Select(oBox.Text.Length, 0)                ' Select last position, will append here
        oBox.SelectionFont = txtFont                    ' Set the font
        oBox.SelectionColor = txtColor                  ' Set the Color
        oBox.AppendText(txtVal)                         ' Append the text with these values
        oBox.DeselectAll()                              ' Deselect in prep for next action
        Return

    End Sub

End Module
