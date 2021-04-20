
Imports System.IO
Imports System.Windows.Forms

Public Class MAIN
    '   Version Log
    '   1.058.2 Baseline Vesion
    '   1.059.0     April, 07-2021   Bob Hehmann
    '               1)  Updated Version to 1.059, updated copyright date, corrected version number in Window Title Bar & About Box
    '               2)  Changed ODF I/O to allow UTF-8 compatibilty, useful if including special/extended characters, i.e. adding an
    '                   umlaut to a Stop Name
    '               3)  Changed "Compilated" to "Compiled" in Title Bar; Made Vesion info in Title Bar & About Box dynamic
    '               4)  Changed Menu Bar text to mixed-case, added ... to "Save As" & "Open Hauptwerk Organ"; Changed text for
    '                   "Help" to "View Help", "About" to "About AECHO", reflecting standatd practice
    '               5)  Replaced MSgBox implementation of "About" with VS AboutBox object, which takes its content from Project
    '                   Assembly parameters.
    '               6)  Updated .NET Framework from 3.5 to 4.8 (and tested successfully with .NET 5.0)
    '               7) 	Added AECHO 1.059.0 Project to GIT, to manage source versioning. Updates 1-7 define the initial baseline
    '                   Git Branch 'master'
    '   1.060.0     April 18, 2021  Bob Hehmann
    '               1)  Git branch 'cleanup-InitialPass-Syntax-Comments'. Cleanup code formatting, comment formatting,
    '                   resolved all IDE Warnings and Informational messages.
    '               2)  Add English comments to all code.
    '               3)  Add standardized documentation to all procedures: Purpose; Process; Called-By; Side-Effects; Notes;
    '               4)  Add XML terminology, will relate to new XML-specific logic to be added soon.
    '               5)  Modified "Exit Sub" to "Return" (avoids IDE editing/auto-complete issues.)
    '               6)  Added ability to locate the "\DATA" directory from 2 locations: the ususal location, adjacent to
    '                   the executable, for use when AECHO is not explicitly installed; from a distinct non-adjacent synthetic
    '                   directory, when installed as a Click-once application. For Click-once apps, Windows moves data files to
    '                   a non-adjacent area, need to use a system call to locate.
    '               7)  Added the \DATA directory to the project, maintained at the same level in the VS Proejct as the Project
    '                   file and VB sourcecode - this places it under Git version management. Added to installer, so correct version
    '                   is created via Publish. To use with non-Published versions, copy this master to the usual adjacent location,
    '                   from Windows.
    '               8)  Made Publication-ready in Visual Studio.
    '   1.060.1     April 18, 2021 Bob Hehmann
    '               1)  Upgrade from .NET 4.8 to .NET 5.0
    '               2)  Added an installer project, so AECHO can run from a self-contained directory (legacy), or be installed as a
    '                   standard application. When installing, \DATA dir is placed into "\User\Romaing\Aecho\DATA". Installer will
    '                   install the code, place the \DATA directory under \Roaming\, create desktop icon, and a Program Start-Menu
    '                   entry. Includes Uninstalling capability (removes code, data, icon, and Start-Menu folder/icon.)
    '               3)  Added code to locate the \DATA directory either adjacent to the executable (legacy), or in its installed location

    ' XML & Related Teminology in comments, as used in AECHO (not all aspects of XML are defined or used) -
    '   (XML):
    '       Tag ->          A markup string starting with "<", and ending with ">". When nested, individual Tags are the closest
    '                       matching bracket-pairs - standard nesting conventions apply. Example: "<ImaTag>"
    '
    '       Start-Tag ->    The opening tag of an Element e.g. "<Pitch>". Contains the Name immediately following the opening "<".
    '                       Always paired with an End-Tag.
    '
    '       End-Tag ->      The closing tag of an Element e.g. "</Pitch>". Contains the same name as its paired Start-Tag,
    '                       preceded by "/". 
    '
    '       Name/Type ->    The first string between the markup-brackets of a Tag: in the prior two examples, "Pitch".
    '                       Names are case-sensitive. The Name of an Element identifies its Type, which can be associated with
    '                       rules for acceptable syntax and structure of the Content within an Element of a particular Type.
    '
    '       Element ->      A string consisting of a Start-Tag ("<xyz>"), and ending with a matching End-Tag ("</xyz>"). There
    '                       may be text between the Tags (the Content), or not.
    '
    '       Content ->      The text found between an Element's Start and End-Tags; the Content may be null/empty. Content may
    '                       include one or more nested Elements, in addition to unmarked text. Such nested Elements are called
    '                       Child Elements, which are wholly contained within their Parent Element.
    '
    '                       Example: "<o><a>123</a><b>ABC</b></o>" has an Parent (outer) Element "o", whose Content consists of
    '                       two Child Elements, "a" and "b". The Content of Element "a" is "123"; of "b" is "ABC".
    '
    '                       Nesting depth can be arbitrary, but nested Elements must be wholly contained in their Parent's Content,
    '                       and Elements may never overlap.
    '
    '                       Example: "<o><a>123</a></o>" is OK (wholly nested), but "<o><a>123</o></a>" is incorrect, as the
    '                       Parent "o" cannot end before its Child "a" ends.
    '
    '       Attribute ->    Optional Name/Value pairs contained in a Start-Tag, following the Name. Attributes are seperated from
    '                       the Name and from each other by whitespace. Their syntax is attribute_name="attribute_value". A
    '                       Start-Tag may contain 0, 1, or more Attributes.
    '
    '                       Example: '<image source="picture.jpg" border="None">Stuff...</image>' is an Element named "image",
    '                       with two Attributes ("source" and "border") whose values are respectively "picture.jpg" and "None".
    '                       The Content is "Stuff...".
    '
    '   XML includes many additional elements, such as escapes, comments, Empty-Element-Tags... These are not defined here, and
    '   are generally not used in an ODF.
    '
    '   (ODF):              Organ Definition Format/File
    '
    '       ODF ->          The content of a Hauptwerk Organ Definition File, which is organized as an XML document. Loaded into
    '                       the RTBox control, ODF will generally refer to this in-memory copy, not to the underlying file.
    '                       AECHO facilitates the presentation, decoding, and simple editing of ODF structured files.
    '
    '       Compressed ODF ->
    '                       Most ODF Files are distributed in their "Compressed" form, where the Element Names of the myriad of
    '                       lowest level Child Elements are replaced with single letter codes: "<a>", "<b>"..., instead of
    '                       their long-form descriptive names. And those that would have null values are eliminated entirely.
    '                       This is to increase efficiency in HW whan these files are processed, and to reduce file size.
    '                       However, it leaves the resulting ODF File essentially uninterpretable to a non-expert user. One
    '                       purpose of AECHO is to provide a context-aware presentation of the full-length Names of all
    '                       Elements avaiable within a Section record - including both populated and unpopulated fields.
    '
    '       Section ->      In an ODF, an XML Element of Type "ObjectList", with the Attribute "ObjectType". This Attribute's
    '                       Value is the name of the Section. A Section specifies a group of related Elements defining a particular
    '                       type of Organ content. For example, the Content of the Section "TextStyle" defines font types and
    '                       styles that might be applied to instances of Text (itself another Section Type.) Conceptually, a
    '                       Section resembles a database table.
    '
    '                       Example: '<ObjectList ObjectType="DisplayPage"><o><a>1</a><b>Console</b></o></ObjectList>' is the
    '                       Section DisplayPage, with a single Content Row.
    '
    '                       In Hauptwerk, ODF Sections have been very stable over time, comprising the same 44 Sections for many
    '                       years. Each Section has its own defintion of the Elements that encode its Content, analgous to the
    '                       definition of a database table's records. The Content of a Section Element may be empty: such Sections
    '                       appear as Elements with no Content.
    '
    '                       Example: '<ObjectList ObjectType="ExternalRank"></ObjectList> defines an empty Section "ExternalRank".
    '
    '                       Sections 2-44 have a consistant structure, where their Child Elements are all of Type "o". The
    '                       structure of each "o" Element in a given Section is the same, but differs from Section to Section,
    '                       much as records within a single database table have a similar structure, but are different from
    '                       records in another table. AECHO parses the structure of each Section Type when presenting its Content.
    '
    '                       Section 1, "_General" has a distinctly different structure from the other 43 Sections: it consists
    '                       of a single Child Element named "_General", which in turn contains numerous Child Elements with
    '                       fully expanded and self-explanatory Names. These Elements define common meta-data such as the Organ's
    '                       Name, Location, and Builder. In contrast, the Child Elements within an "o" Element (found in
    '                       Sections 2-44), have highly compressed Names, usually consisting of a single alphabetic letter without
    '                       obvious meaning: '<a>', '<d>"... Part of AECHO's purpose is to expand these compressed Names into
    '                       their full HW internal Names, which are mostly self-explanatory.
    '
    '                       Sections are a major navigational element of AECHO, directly accessible from the program's Menu Bar.
    '                       
    '       Row ->          For the Child Elements of Sections 2-44, a single XML Element of Type "o": the standard record that
    '                       comprises most of an ODF's content. Note that a Row may span multiple lines in the RTBox/ODF, due to
    '                       either arbitrary line-wrapping in a RichTextBox control, or explicit newlines in the underlying file.
    '                       Generally, when presenting parsed "o" Element Content, the outer bounding tags "<o>" and "</o>" are
    '                       ignored, being implicit: only the Child Elements inside the "o" Element are expanded upon. "o" Rows
    '                       are analagous to individual database records in a Section table; Child Elements of an "o" represent
    '                       the fields within each record.
    '
    '                       For all other Elements, such as Section 1 ("_General"), the ObjectList Start and End Tags, XML
    '                       control Elements, and the outermost Element "Hauptwerk", AECHO treats each display line in the RTF
    '                       box as a Row, not to be parsed. These rows may be edited, however.
    '
    '       Line ->         RichTextBox controls express positioning in terms of Lines, zero-based. When loading a text or RTF file
    '                       into such a control, lines are broken both at the control's right-side border (soft), and when the
    '                       source file itself contains a line-break (hard). Therefore, a line number in such a control is not
    '                       necessarily in 1-1 correspondance with the source file. When writing the control's content back out
    '                       to a file, soft/wrap line-breaks removed, but hard-breaks, original or manually entered while editing,
    '                       remain.

    '           General Globals

    Public Const conVerbose As Boolean = True   ' Constant to indicate Section Parser should display results
    Public Const conSilent As Boolean = False   ' Constant to indicate Section Parse should not display results

    Public G_Registered As Boolean          ' version payante ou demo; set to True if copy of AECHO is registered (now Freeware, always set True at initialization
    Public G_EditMode As Boolean            ' true si actif; True if Edit Mode is active - allows modifications to ODF copy in RTBox
    Public G_OrganFile As String            ' Complete path/filename of the current ODF
    Public G_ODFLength As Integer           ' taille de l'ODF; length, in characters, of the current ODF image stored in RTBox
    Public G_StartPos As Integer            ' G_ signifie variable globale' ; often used as the Start position within RTBox for searches, and can be updated e.g. FindNext
    Public G_EndPos As Integer              ' Not presently used
    Public G_CaretPos As Integer            ' position du curseur; current Cursor position withn RTBox representation of ODF
    Public G_LineIndex As Integer           ' RTBox Line Number of the present / selected line
    Public G_LineText As String             ' A line of text from the RTBox, often the presently selected text
    Public G_LineStart As Integer           ' position de <o>; index within the RTF Box to the "<" character of "<o>", indicates start of line
    Public G_LineEnd As Integer             ' position de </o>; index of the "<" of "</o>: beginning of EOL flag, not the actual EOL
    Public G_SectionStart As Integer        ' Deprecated, no longer used: from OldGetSectionFromMenu()
    Public G_SectionEnd As Integer          ' Deprecated, no longer used: from OldGetSectionFromMenu()
    Public G_Section As Integer             ' N° de la section en cours (1 à 44)
    Public G_AppPath As String              ' Application Path; location of the exectuable
    Public G_DataPath As String             ' repertoire data; location of the local \DATA directory, presently \DATA at the top of AppPath: contains all the static .rtf files
    Public G_InitialDir As String           ' Directory to start from when opening/writing an ODF file. Can come from previous use, from \DATA\initial.txt, or from harcoded default in the code
    Public G_PackagePath As String          ' HauptwerkSampleSetsAndComponents\OrganInstallationPackages
    Public G_PackageID As String            ' numero à 6 chiffres du dossier d'installation; Package (i.e. Organ) unique ID: 6 chars, leading zeroes - "000001" is St. Anne's
    Public G_SectionName As String          ' The standard name for the ODF Section we are presently positioned at
    Public G_LastSectionName As String      ' The previous ODF Section, updated when positioning to a new Section; set, but not used in the present code 
    Public G_RTFFile As String              ' Path and contructed filename of a .rtf file to be loaded and displayed - presently Section description files or HelpFile
    Public G_PreviousRTFFile As String      ' Path & filename of the previously loaded .rtf file, if any. Used to avoid reloading the same file over and over. May contain a minor bug.
    '       images
    Public G_ImageFile As String            ' Path and filename of an image file to be loaded and displayed (presently only .bmp (bitmap) format is allowed)
    Public G_ImageSet As String             ' (Type as String) When parsing an ImageSetElement record, this is the number i.e. ImageSetIndex that links to the parent ImageSet.
    Public G_ImageIndex As String           ' (Type as String) When parsing an ImageSetElement record, this is the ImageIndex of the desired Image instance within the ImageSet.
    '       Fonction Find
    Public G_TextToFind As String           ' Desired Search Text pulled from the Main Form's "TextToFindBox", then looked for by the "FindMyText(...)" function
    Public G_FindStartPosition As Integer   ' Initial starting position for "FindMyText" text searches, updated when text is found, becomes start for FindNext.
    '       Fonction Follow
    Public G_Item_to_Follow As String       ' (Type as String) Set to SectionID for a "Sample" object, but never used

    ' Structure Definition

    Structure Str_Section                   ' The structure of an ODF Section, built when the ODF is loaded, can be rebuilt upon command
        Public index As Integer             ' Index 1-44: superflous, always = to the idx of the array of this structure
        Public title As String              ' tag complet; fully qualified Section Title e.g. '<ObjectList ObjectType="TextInstance">'
        Public name As String               ' rien que le nom; Section Name, without the XML overhead e.g. 'TextInstance' (without the single-quotes)
        Public startPos As Integer          ' Index within RTBox to the starting character of this Section, opening '<' of '<ObjectList...>'
        Public endPos As Integer            ' Index within RTBox of the last char of this section, the closing '>' of '</ObjectList>'
        Public len As Integer               ' longueur de la section; length of the Section, from Start-Tag first char to End-Tag last char
        Public firstLine As Integer         ' n° de la 1ere ligne; RTBox line number, 0-based, # of '<ObjectList...>'
        Public titleEnd As Integer          ' position de > à la fin du titre; index within RTBox of the closing '>' of the firstLine
        Public isEmpty As Boolean           ' True if Section is defined, but has no content i.e. no Child Elements / No Rows
        Public firstLineStartPos As Integer ' debut de la premiere ligne; index of opening '<' of '<o>...' of first content Row. If isEmpty, then =-1, else = titleEnd+1
        Public firstLineEndPos As Integer   ' fin de la premiere ligne; index of closing '>' of first content Row. If isEmpty, then = 0, else = character position
    End Structure
    Public S_Section(44) As Str_Section     ' Table of Sections, built when ODF is first loaded, can be recomputed. Editing text can change line & position data for current & subsequent Sections

    '                                         (Note - change to constants) List of XML for the introduction to each of the 44 Sections: sectionxx will equal S_Section(xx).title, once S_Section is loaded
    '                                         Becomes search text for a Section when the Section Menu item of the same name is clicked.

    Public section01 = "<ObjectList ObjectType=""DisplayPage"">"
    Public section02 = "<ObjectList ObjectType=""TextStyle"">"
    Public section03 = "<ObjectList ObjectType=""TextInstance"">"
    Public section04 = "<ObjectList ObjectType=""ImageSet"">"
    Public section05 = "<ObjectList ObjectType=""ImageSetElement"">"
    Public section06 = "<ObjectList ObjectType=""ImageSetInstance"">"
    Public section07 = "<ObjectList ObjectType=""KeyImageSet"">"
    Public section08 = "<ObjectList ObjectType=""Division"">"
    Public section09 = "<ObjectList ObjectType=""DivisionInput"">"
    Public section10 = "<ObjectList ObjectType=""Switch"">"
    Public section11 = "<ObjectList ObjectType=""SwitchLinkage"">"
    Public section12 = "<ObjectList ObjectType=""SwitchExclusiveSelectGroup"">"
    Public section13 = "<ObjectList ObjectType=""SwitchExclusiveSelectGroupElement"">"
    Public section14 = "<ObjectList ObjectType=""Keyboard"">"
    Public section15 = "<ObjectList ObjectType=""KeyboardKey"">"
    Public section16 = "<ObjectList ObjectType=""KeyAction"">"
    Public section17 = "<ObjectList ObjectType=""Rank"">"
    Public section18 = "<ObjectList ObjectType=""ExternalRank"">"
    Public section19 = "<ObjectList ObjectType=""ExternalPipe"">"
    Public section20 = "<ObjectList ObjectType=""Stop"">"
    Public section21 = "<ObjectList ObjectType=""StopRank"">"
    Public section22 = "<ObjectList ObjectType=""ReversiblePiston"">"
    Public section23 = "<ObjectList ObjectType=""Combination"">"
    Public section24 = "<ObjectList ObjectType=""CombinationElement"">"
    Public section25 = "<ObjectList ObjectType=""ContinuousControl"">"
    Public section26 = "<ObjectList ObjectType=""ContinuousControlStageSwitch"">"
    Public section27 = "<ObjectList ObjectType=""ContinuousControlImageSetStage"">"
    Public section28 = "<ObjectList ObjectType=""Enclosure"">"
    Public section29 = "<ObjectList ObjectType=""EnclosurePipe"">"
    Public section30 = "<ObjectList ObjectType=""Tremulant"">"
    Public section31 = "<ObjectList ObjectType=""TremulantWaveform"">"
    Public section32 = "<ObjectList ObjectType=""TremulantWaveformPipe"">"
    Public section33 = "<ObjectList ObjectType=""ContinuousControlLinkage"">"
    Public section34 = "<ObjectList ObjectType=""ContinuousControlDoubleLinkage"">"
    Public section35 = "<ObjectList ObjectType=""ThreePositionSwitchImage"">"
    Public section36 = "<ObjectList ObjectType=""WindCompartment"">"
    Public section37 = "<ObjectList ObjectType=""WindCompartmentLinkage"">"
    Public section38 = "<ObjectList ObjectType=""Sample"">"
    Public section39 = "<ObjectList ObjectType=""Pipe_SoundEngine01"">"
    Public section40 = "<ObjectList ObjectType=""Pipe_SoundEngine01_Layer"">"
    Public section41 = "<ObjectList ObjectType=""Pipe_SoundEngine01_AttackSample"">"
    Public section42 = "<ObjectList ObjectType=""Pipe_SoundEngine01_ReleaseSample"">"
    Public section43 = "<ObjectList ObjectType=""RequiredInstallationPackage"">"
    ' FORM LOAD

    Private Sub MAIN_Load(sender As Object,                     ' AECHO's base Form
                          e As EventArgs
                          ) Handles Me.Load

        ' Purpose:      Initialize the application: application registered check, prep default paths to AppData & ODF,
        '               update Window Title Bar, ensure the tags display is cleared and Edit Mode is off.
        ' Process:      Contruct file path (directory path) variables, call initialization routines.
        ' Called By:    Window's Event Handler, MAIN form Load Event
        ' Side Effects: Initializes a number of Class variables; updates MAIN form
        ' Notes:        Reference to G_SectionName is suspect, RTBox is empty at this point - better handled elsewhere

        Dim workingDataDir As String                            ' Interim var while finding path to data
        Dim trimDir As String                                   ' Constructed to be "ProductName\ProductVersion", which we have to strip off of data path for installed app
        Dim idx As Integer                                      ' String-search index

        'G_AppPath = Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        'G_AppPath = G_AppPath.Substring(6, Len(G_AppPath) - 6) ' il faut retirer File:\ au début; initialize path to the application, stripping off initial "file:\"

        G_AppPath = Application.StartupPath                     ' Better way to locate executable path in .NET 5
        workingDataDir = Path.Combine(G_AppPath, "DATA")        ' Construct the first attempt, \DATA adjacent to executable

        If Directory.Exists(workingDataDir) Then                ' Is \DATA next to executable? If so, then AECHO is not an installed application, and we are good
            G_DataPath = workingDataDir                         ' G_DataPath points to non-installed \DATA directory containing Section Descriptive files and Help file (.rtf files)
        Else                                                    ' POssibly an installed app, DATA will be in AppData\Roaming\AECHO\DATA
            workingDataDir = Application.UserAppDataPath        ' Retrieve the full, version-specific Path, we'll need to truncate back to \Roaming\AECHO
            trimDir = Path.Combine(Application.ProductName,
                                    Application.ProductVersion) ' Construct "AECHO\1.60...", which we need to trim off
            idx = Strings.InStr(workingDataDir, trimDir,
                                CompareMethod.Text)             ' Find this excess trailer...
            If idx > 1 Then                                     ' We found the trailer
                workingDataDir = Strings.Left(workingDataDir,
                                              idx - 1)          ' Truncate the trailer, leaving "\Roaming\AECHO\"
                workingDataDir = Path.Combine(workingDataDir,
                                              "DATA")           ' Append \DATA suffix
                If Directory.Exists(workingDataDir) Then        ' Good, we found it in Installed location, go with it
                    G_DataPath = workingDataDir                 ' G_DataPath now points to the \DATA directory for a standard installed application
                Else
                    G_DataPath = ""                             ' Cannot locate our Data, clear the Path
                End If
            Else
                G_DataPath = ""                                 ' Search for trailer-text failed, we give up
            End If
        End If
        If G_DataPath = "" Then                                 ' Did we find it? If not, present a warning message
            MsgBox("Cannot locate the \DATA directory. AECHO will still run, but will be unable to access Section Descriptions and the Help file.", vbInformation)
        End If

        Me.Text = "AECHO: Hauptwek Organ Analyzer, Version " &
            My.Application.Info.Version.ToString                ' <1.059.0> Add version ID to the initial Window Title Bar at run-time
        RegisteredUnregistered()                                ' Savoir si version enregistre ou non; detect if this version is registered or not - as of 1.057, always "Registered"
        ReadInitialDir()                                        ' Establishes G_InitialDir as (possible) location of user's ODF files
        G_SectionName = "_General"                              ' par defaut; the initial Section Name
        ClearTagsPanel()                                        ' Hide all controls in the PackageID Panel
        PBox.Visible = False
        G_EditMode = False                                      ' ajout version 055. on est en mode lecture seule; default is to disallow editing until manually enabled from the Menu Bar

    End Sub
    Private Sub RegisteredUnregistered()                        ' Savoir si version enregistre ou non; modifié pour passer en freeware à partir de v1-0-57

        ' Purpose:      Determine if the application is Registered (paid-for) or not.
        ' Process:      As of V1.057, became Freeware, so always return "Registered". Previously, looked for presence of a license file.
        ' Called By:    MAIN_Load(), once upon application start
        ' Side Effects: Alters G_Registered; Hides FollowASample Menu, Font Size Control, Non-Verbose Sections Mode (ButtonLed)
        ' Notes:        <None>

        G_Registered = True                                     ' As of V1.057, always treat as Registered / Freeware
        Exit Sub

        Dim verpeaux_File = G_DataPath & "\_verpeaux.txt"       ' Pre V1.057 detection code begins here...
        Dim reponse
        If My.Computer.FileSystem.FileExists(verpeaux_File) Then
            G_Registered = True                                 ' me donner le choix; detect existance of license file
            reponse = MsgBox("Veux-tu émuler la version demo ?", MsgBoxStyle.YesNo, "OPTION PERSO (vpx_file présent)")
            If reponse = vbYes Then G_Registered = False        ' Is registered, ask user if code should emulate non-registered
        Else                                                    ' pas de fichier licence verpeaux
            G_Registered = False                                ' tester ici si fichier licence.txt existe et est valide
        End If

        If G_Registered = False Then                            ' Restriction si unRegistered
            Menu_FollowASample.Visible = False                  ' Enforce restrictions if unregistered
            NumericTextSize.Visible = False
            ButtonLed.Visible = False
        End If

    End Sub
    ' FONCTIONS RICH TEXT BOX

    Public Function FindMyText(text As String,                  ' Text string to search for
                               start As Integer                 ' Starting location for search within RTBox
                               ) As Integer                     ' Index to first character of found string, -1 if not found

        ' CHERCHE UN TEXTE DE HAUT EN BAS DANS RTBOX

        ' Purpose:      Search RTBox (ODF) for [text], beginning from position [start], returning the position
        '               of the first character of [text] if found.
        ' Process:      Validate parameters, use RichTextBox Find Method to execute search.
        ' Called By:    GetSectionFromIndex(); GetFirstLine(); OldGetSectionFromMenu() (deprecated)
        ' Side Effects: <None>
        ' Notes:        Need to correct several bugs & flow.

        Dim returnValue As Integer = -1                         ' Initialize the return value to false by default.
        ' Add check here to ensure RTBox is not null, otherwise this will trigger an exception
        If text.Length > 0 And start >= 0 Then                  ' Ensure that a search string has been specified and a valid start point.
            Dim indexToText As Integer =
                RTBox.Find(text, start, RichTextBoxFinds.None)  ' Obtain the location of the search string in richTextBox.
            If indexToText >= 0 Then returnValue = indexToText  ' Determine whether the text was found
        End If

        Return returnValue

    End Function
    Public Function FindMyTag(text As String,                   ' Text string to search for
                              start As Integer                  ' Starting location for search within RTBoxLine
                              ) As Integer                      ' Index to first character of located string, -1 if not found

        ' CHERCHE UN TAG DE HAUT EN BAS DANS RTBOX2 ET RETOURNE SA POSITION

        ' Purpose:      Search RTBoxLine (1 selected ODF Row) for [text], beginning from position [start],
        '               returning the position of the first character of [text] if found.
        ' Process:      Validate parameters, use RichTextBox Find Method to execute search.
        ' Called By:    ReadTag(); CountTags()
        ' Side Effects: <None>
        ' Notes:        Bug - test for no source text is aginst wrong control, should test RTBoxLine.
        '               Insufficient integrity checks on Parameters - needs hardening.

        If RTBox.TextLength = 0 Then Exit Function              ' <bug>: should be RTBoxLine; MODIF V058; to avoid searching an empty RTBox, might be bug as value is not defined

        Dim returnValue As Integer = -1                         ' Initialize the return value to false by default.
        Dim indexToText As Integer                              ' Position of search-text in RTBox, when found

        If text.Length > 0 Then                                 ' Ensure that a search string has been specified and a valid start point.
            indexToText = RTBoxLine.Find(text, start,
                                         RichTextBoxFinds.None) ' Obtain the location of the search string in richTextBox.
            If indexToText >= 0 Then returnValue = indexToText  ' Determine whether the text was found in richTextBox1.
        End If

        Return returnValue

    End Function
    Public Function FindReverse(text As String,                 ' Text string to search for
                                start As Integer                ' Starting location for search, searches backwards from 'start' towards beginning of RTBox
                                ) As Integer                    ' Index to first character of found string, -1 if not found

        ' CHERCHE UN TEXTE DE BAS EN HAUT

        ' Purpose:      Search backwards in RTBox (ODF) for [text], beginning from position [start], returning
        '               the position of the first character of [text] if found
        ' Process:      Validate parameters, use RichTextBox Find Method, Reverse option, to execute search.
        ' Called By:    RTBox_DoubleClick(); GetSectionFromIndex(); NextLineCommun()
        ' Side Effects: <None>
        ' Notes:        Insufficient integrity check on Parameters - needs hardening. Possible undefined return error.

        If RTBox.TextLength = 0 Then Exit Function              ' MODIF V058; to avoid searching an empty RTBox, might be bug as function value is not defined

        Dim returnValue As Integer = -1                         ' Initialize the return value to false by default.
        Dim indexToText As Integer                              ' Position of search-text in RTBox, when found

        If text.Length > 0 And start >= 0 Then                  ' Ensure that a search string has been specified and a valid start point.
            indexToText = RTBox.Find(text, 1, start,
                                     RichTextBoxFinds.Reverse)  ' Obtain the location of the search string in richTextBox1.
            If indexToText >= 0 Then returnValue = indexToText  ' Determine whether the text was found in richTextBox1.
        End If

        Return returnValue

    End Function
    Public Function ReadTag(Tag As String,                      ' Element-Name to locate and parse
                            pos As Integer                      ' Begin search from this position within RTBoxLine
                            ) As String                         ' Element-Content text: " " if Element exists but has null Content, "" if Element is not foumd

        ' CHERCHE UN TAG DANS RTBOXLINE ET LIT LE  TEXTE CONTENU DANS LE TAG

        ' Purpose:      Search RTBoxLine (the selected ODF Row) for the Element-Name [Tag] starting at [pos], returning
        '               the Content from between the Start and End-Tags (without the bounding Tags themselves):
        '               if [Tag]='x', find "<x>Some Text</x>", return "Some Text".
        ' Process:      Validate paramters, return "not found" if no ODF or if searching for Element '<o>'. Search forward
        '               for Start-Tag, if found, continue searching to find End-Tag, extract Content as
        '               a substring from between the bounding Tags. If Element cannot be found, return null string ""
        '               to indicate failure; if found, but there is no Content, return a single blank (" ") instead.
        ' Called By:    DisplayTagText()
        ' Side Effects: If the Element is found, leaves the Element's Content text in RTBoxLine selected.
        ' Notes:        Bug - after looking for Start-Tag, test for failure (tagStart=-1) happens after tagStart has been
        '               incremented past end-of-tag - can never be -1. Consider combining several functions into a single call:
        '               from a position (within a line?), search for an Element, return its start, end, and Content. Consider
        '               changing to a boolean function to indicate success, with the extracted string a ByRef parameter
        '               rather than returned as the function's result.

        Dim returnValue As String                                   ' Interim Function return
        Dim tagStart As Integer                                     ' 1 past Start-Tag: the earliest starting point of non-null Content
        Dim tagEnd As Integer                                       ' Position of End-Tag

        If RTBox.TextLength = 0 Then Return ""                      ' MODIF V058 <1.059.0> corrected error of not assigning a return value, which would cause a run-time exception
        If Tag = "o" Then Return ""                                 ' ignorer le tag <o>; <1.059.0> deleted extraneous Exit Function: Return already exits

        tagStart = FindMyTag("<" & Tag & ">", pos) + 2 + Len(Tag)   ' trouve le tag de début et de fin; Search for STart-Tag ("<x>"): if found, set tagStart just past marker, where Content text begins
        If tagStart = -1 Then Return ""                             ' <bug>, test before increment. Never found Start-Tag; <1.059.0> deleted extraneous Exit Function: Return already exits

        tagEnd = FindMyTag("</" & Tag & ">", tagStart)              ' trouve le tag de fin; Search for End-Tag ("</x>"), continuing from start of Content
        If tagEnd = -1 Then Return ""                               ' Never found End-Tag; <1.059.0> deleted extraneous Exit FUnction: Return already exits

        RTBoxLine.SelectionStart = tagStart                         ' lit le contenu du tag; Position selection start to beginning of possible Content
        If tagEnd - tagStart < 1 Then                               ' End was before Start, signal error. Note that End can equal Start, if there is no Content
            MsgBox("Error: In ReadTag, tagEnd (" & tagEnd & ") was < tagStart (" & tagStart & "). Searching for Tag=(" & Tag & "), pos=(" & pos & ").",, "Error")
            Return ""                                               ' Return null
        End If
        RTBoxLine.SelectionLength = tagEnd - tagStart               ' Length of text between Tags
        returnValue = RTBoxLine.SelectedText                        ' Select the Content, and return it

        If returnValue = "" Then returnValue = " "                  ' remplace "" par " "; Return a single blank instead of a null string, if the Element was found, but has no Content
        Return returnValue
        'Else
        'debug
        'MsgBox(tag & vbCrLf & "tagStart " & tagStart.ToString & vbCrLf & "tagend " & tagEnd.ToString & vbCrLf & "lineStart " & lineStart.ToString & vbCrLf & "lineEnd " & lineEnd.ToString)
        Return ""
        ' End If

    End Function
    Public Function CountTags(lgStart,                      ' Points to beginning of the search region
                               lgEnd                        ' And the end of the search region
                               ) As Integer                 ' Return the count; <1.059.0> Changed from Single (Floating) to Integer type

        ' COMPTE LE NOMBRE DE TAGS DANS UNE LIGNE DE RTBOXLine

        ' Purpose:      Calc number of Elements within an RTBoxLine (1 ODF row) range, not including the bounding '<o>...</o>'
        '               In practice, range is from 1 to end-of-row; returns the Count and updates the Tag Count form field.
        ' Process:      Count the number of End-Tag markers '</' in the range, subtract 1 to discount the outer End-Tag
        '               marker '</o>' from the tally.
        ' Called By:    RTBox_DoubleClick(); GetSectionFromMenu(); DisplayOject(); NextLineCommun();
        '               OldGetSectionFromMenu() (deprecated)
        ' Side Effects: Updates on-screen count of the number of Elements in the chosen line - usually invoked just for this,
        '               function return-value is generally discarded by callers
        ' Notes:        Max count (24) should be a global constant; logic is slightly off - assumes the '</o>' End-Tag will be in
        '               the range, and therefore decrements Count by 1, as this function does count the Parent Element: works when the
        '               entire row with the closing '</o>' is in scope, but if the bounds are narrower, Count would be low by 1.

        Dim count As Integer = 0                            ' Counts number of End-Tag markers found
        Dim idx As Integer                                  ' Loop control variable
        Dim returnValue As Integer                          ' Temp var for the Function's return value

        For idx = 0 To 24                                   ' Parse up to 24 Elements max. 0 Start is to allow for Parent Element
            returnValue = FindMyTag("</", lgStart)          ' Search for End-Tag marker: '</'
            If returnValue = -1 Then Exit For

            lgStart = returnValue + 1                       ' Advance past found End-Tag marker before continuing search for next End-Tag
            If returnValue <= lgEnd Then                    ' We haven't gone past the end bound, increment count & continue
                count += 1
            Else                                            ' Past end of search-range, we're done...
                Exit For
            End If
            'MsgBox("lgstart " & lgStart & vbCrLf & "lgEnd " & lgEnd & vbCrLf & count.ToString & " tags")
        Next

        LabelNumberOfTags.Text = (count - 1).ToString       ' Update onscreen display of the number of Elements
        Return count - 1                                    ' pour ne pas compter "<o>"; do not include outer </o> in count

    End Function
    ' CONTROLES

    Private Sub NumericTextSize_ValueChanged(sender As Object,      ' Standard Control event parms...
                                             e As EventArgs
                                             ) Handles NumericTextSize.ValueChanged

        ' Purpose:      Set RTBox's (ODF display) font-size to match the new value in the font-size control
        ' Process:      Create an appropriate FontSize object, select and change the ODF text
        ' Called By:    NumericTextSize ValueChanged Event
        ' Side Effects: Updates RTBox display, and forces font-type and style
        ' Notes:        <None>

        Dim fontSize As Integer                                 ' Value from Control

        fontSize = NumericTextSize.Value                        ' Grab the desired font-size from the control, in points
        RTBox.SelectAll()                                           ' Affects all text in RTBox (displayed ODF)
        RTBox.SelectionFont = New Font(New FontFamily(              ' Set entire RTBox to MS Sans, selected font-size, Regular stlye
                                       "microsoft sans serif"),
                                       fontSize,
                                       FontStyle.Regular)

    End Sub
    ' PROCEDURES RICH TEXT BOX
    Private Sub RTBox_DoubleClick(sender As Object,                 ' Standard Control event parms...
                                  e As MouseEventArgs
                                  ) Handles RTBox.MouseDoubleClick

        ' Purpose:      Select a complete (XML) Row from the ODF, copy it to RTBoxLine, highlight the Row in the ODF
        ' Process:      Position Cursor to the point double-clicked in RTBox & update Cursor Position field to match.
        '               Locate the ODF Section the Cursor is in. Extract and display the current Row in RTBoxLine, and
        '               the number of Child Elements in this row (not including bounding '<o>...</o>'. Select the
        '               Cursor's line in RTBox. Retrieve & display the Section's description from the Section's
        '               associated .rtf file.
        ' Called By:    RTBox MouseDoubleClick Event
        ' Side Effects: Alters globals: CaretPos, LineStart, LineText. Updates RTBox, RTBoxLine, RTBoxRTF, various
        '               display fields on Main Form.
        ' Notes:        Unwind nested updates to Global Variables, replace with ByRef/Functions where that would reduce
        '               use of side-effects. Update Section Begin/End fields on the MAIN form to reflect movement to a
        '               new Section (fields not presently updated, probably a bug.)

        If RTBox.TextLength = 0 Then                                ' RTBox is empty, no text, issue informational message
            MsgBox("No text to select.", vbInformation)             ' <1.059.0> Clarify text, set message type to Informational
            Exit Sub                                                ' MODIF V058
        End If


        G_CaretPos = RTBox.GetCharIndexFromPosition(e.Location)     ' TROUVER L'INDEX SELON POSITION SOURIS; fetch position of the double-click
        LabelCaretPos.Text = G_CaretPos.ToString                    ' Update form field, displaying numeric value of Cursor's position
        GetLineFromIndex(G_CaretPos)                                ' Trouver et selectionner la ligne; select the line containing the cursor

        Dim sectionHead = FindReverse("<ObjectList", G_CaretPos)    ' verifie que la ligne appartient bien à la meme section; reverse-search to verify we are in a standard Section (avoiding _General?)
        If G_LineStart < sectionHead Then
            MsgBox("Please, do not click on this line. Try with an other line.",
                   MsgBoxStyle.Information + MsgBoxStyle.OkOnly,
                   "Message from RTBox.DoubleClick")                'La ligne n'est pas dans la meme section
        End If

        ' MODIF VERSION 025
        RTBoxLine.Text = G_LineText                                 ' afficher la ligne dans RTBoxLine; display Row in the RTBoxLine control
        GetSectionFromIndex(G_LineStart)                            ' retrouver la section auquel il appartient; update the current Section Name, and display it onscreen
        CountTags(1, RTBoxLine.TextLength)                          ' compter les tags de la ligne dans RTBoxLine; determine the number of Elements in this Row
        DisplayObject()                                             ' montrer les infos de la ligne; update screen fields; <1.059.0> Removed parameter (1), DisplayObject is now parameterless

        RTBox.SelectionStart = G_LineStart                          ' selectionner la ligne; select this Row on the ODF display
        RTBox.SelectionLength = G_LineEnd - G_LineStart
        LoadRTFFile()                                               ' charger le fichier RTF; retrieve and display the .rtf file that descibes this Section

    End Sub
    Private Sub GetLineFromIndex(index As Integer)                  ' Position within ODF

        ' Purpose:      Extract the row that contains the character position [index]. This row may consist of multiple
        '               text-lines in RTBox. Select/Highlight the row's text.
        ' Process:      Locate the present physical line, interogate RTBox for character position of first & last
        '               characters of this line. Select and retrieve this text into G_LineText. If the ending characters
        '               are not the End-Tag for Element Type "o" ("</o>"), extend selection to next line; if
        '               opening chars are not the "o" Start-Tag ("<o">, extend start to previous line.
        ' Called By:    RTBox_DoubleClick(); RTBox_MouseClick(); NextLIne_commun(); FindButtonProcedure();
        '               OldGetSectionFromMenu() (deprecated)
        ' Side Effects: Updates various Global Variables; updates onscreen fields for Line Number, Start, and
        '               End positions. Highlights (selects) Row text in ODF.
        ' Notes:        Cleanup this section, integrate logic for parsing a single text line with the additional logic
        '               to extend to a multi-line row. Remove assumption that a row is 1-2 lines, use "<o>...</o>"
        '               Tag-pair to bound the limits. Can be result of RTBox wrapping of text i.e. a line is wider
        '               than the box and is displayed as multiple physical lines, or the Row is more than 1 line
        '               long in the ODF itself.

        G_LineIndex = RTBox.GetLineFromCharIndex(index)             ' Map the character index to a line
        G_LineStart = RTBox.GetFirstCharIndexFromLine(G_LineIndex)  ' Find out where the line starts
        G_LineEnd = RTBox.GetFirstCharIndexFromLine(
            G_LineIndex + 1) - 1                                    ' And where it ends, which is equivalent to the start of the following line, minus one
        If G_LineEnd < G_LineStart Then Exit Sub                    ' MODIF V058

        RTBox.SelectionStart = G_LineStart                          ' selectionner la ligne et recuperer le texte; select the line
        RTBox.SelectionLength = G_LineEnd - G_LineStart
        G_LineText = RTBox.SelectedText                             ' Extract the selected text

        ' MODIF V058.2
        If G_LineText = "</Hauptwerk>" Then                         ' If the text is the End-Tag of the ODF, present informational message
            MsgBox("This is the End of your ODF", vbInformation)
            Exit Sub
        End If

        '                                                             l'objet <0> ... </o> peut etre sur 2 lignes; logic to handle one Row split across two lines
        Dim line_Ending = G_LineText.Substring(
            Len(G_LineText) - 4, 4)                                 ' recherche </o>; grab the End-Tag on this line
        Dim line_starting = G_LineText.Substring(0, 3)              ' recherche <o>; grab the Start-Tag on this line
        If G_SectionName <> "_General" Then                         ' This logic applies to all sections other than _General
            If line_Ending <> "</o>" Then                           ' trouver fin de ligne; this Row extends into the next line
                G_LineEnd = RTBox.GetFirstCharIndexFromLine(
                    G_LineIndex + 2) - 1                            ' Advance line end to end of the next line
                RTBox.SelectionStart = G_LineStart                  ' selectionner la ligne et recuperer le texte; extend the selection
                RTBox.SelectionLength = G_LineEnd - G_LineStart
                G_LineText = RTBox.SelectedText
            End If

            If line_starting <> "<o>" Then                          ' The beginning of the row ("<o>" Start-Tag) is in a prior line, need to move the start back

                G_LineStart = RTBox.GetFirstCharIndexFromLine(
                    G_LineIndex - 1) - 1                            ' trouver debut de ligne; is final -1 a bug?

                RTBox.SelectionStart = G_LineStart                  ' selectionner la ligne et recuperer le texte; extend the selection to include the previous line
                RTBox.SelectionLength = G_LineEnd - G_LineStart
                G_LineText = RTBox.SelectedText
            End If
        End If

        LabelLineNumber.Text = G_LineIndex                          ' afficher les positions; update form fields for line number, start, and end position of the Row
        LabelLineStart.Text = G_LineStart.ToString
        LabelLineEnd.Text = G_LineEnd.ToString

    End Sub
    Private Sub GetSectionFromIndex(index)                          ' TROUVE LA SECTION DANS LAQUELLE SE TROUVE LE CARET; location in ODF

        ' Purpose:      Retrieve Section info of the Section containing the character at [index] in the ODF.
        ' Process:      Starting from position [index] in RTBox, search backwards for the Section's Start-Tag,
        '               and extract the Section's Name from the Attribute.
        '               Update the Section Name onscreen. Save the name of the previous Section we were in.
        ' Called By:    RTBox_DoubleClick(); NextLineCommun(); FindButtonProcedure();
        '               OldGetSectionFroMenu() (deprecated)
        ' Side Effects: Updates globals for SectionName and LastSectionName. Updates Section Name field on Main Form.
        ' Notes:        Possible bug when saving prior Section's Name before finding the new Section. If new Section
        '               is not found, should prior Section remain unchanged?

        If RTBox.TextLength = 0 Then Return                         ' MODIF V058; if no ODF text at all, just exit

        Dim belongStart As Integer                                  ' XML Section Start-Tag's starting character position
        Dim belongend As Integer                                    ' XML Section Start-Tag's ending character position


        If G_SectionName <> Nothing Then
            G_LastSectionName = G_SectionName                       ' memoriser nom de la section precedente; Save prior Section's Name
        End If

        belongStart = FindReverse("<ObjectList ObjectType=", index) ' retrouver la section auquel il appartient; search backwards for first Section Start-Tag
        If belongStart = -1 Then                                    ' Did not find any Section Start-Tag, display message and return
            MsgBox("Cannot get section for the index " & index.ToString, , "Message from GetSectionFromIndex")
            Return
        End If

        belongend = FindMyText(">", belongStart)                    ' Locate end of Start-Tag
        RTBox.Focus()
        RTBox.SelectionStart = belongStart + 24                     ' Extract the Section Name (the XML Attribute Value)
        RTBox.SelectionLength = belongend - belongStart - 25
        G_SectionName = RTBox.SelectedText
        LabelSection.Text = G_SectionName                           ' Update the Section Name onscreen

    End Sub
    Private Sub RTBox_MouseClick(sender As Object,                  ' Standard Control event parms...
                                 e As MouseEventArgs
                                 ) Handles RTBox.MouseClick
        ' TROUVER L'INDEX SELON POSITION SOURIS
        ' modif version 055

        ' Purpose:      Select an XML Row from the ODF, but do not extract or parse it.
        ' Process:      Field a single-click in the ODF: position the Cursor to the point of the click, select the
        '               entire XML Row (unless in Edit Mode), and update the Cursor & Line #, Start, and End fields.
        '               Note that this function does not update the Section information, does not extract the XML
        '               Row, does not count Tags, nor update Section descriptive text - those require a double-click.
        ' Called By:    RTBox MouseClick Event
        ' Side Effects: Update global CaretPos, Line & Cursor Position fields, select RTBox text.
        ' Notes:        <None>

        G_CaretPos = RTBox.GetCharIndexFromPosition(e.Location) ' Extract the Cursor position from RTBox
        LabelCaretPos.Text = G_CaretPos.ToString                ' Update onscreen field

        GetLineFromIndex(G_CaretPos)                            ' Trouver et selectionner la ligne; Select (highlight) the text of the Row in the ODF

        If G_EditMode = True Then                               ' de-selectionner la ligne si on est en mode edition; if Editing, ensure new line is not selected in RTBox
            RTBox.SelectionLength = 0                           ' modif v055
        End If

    End Sub
    'FONCTIONS ET PROCEDURES

    Private Sub GetSectionFromMenu(section As String)                       ' Name of Section we are looking for

        ' TROUVER UNE SECTION DE L'ORGUE A PARTIR DU MENU

        ' Purpose:      Processes a Menu Section click, positioning the ODF to the beginning of the desired Section.
        ' Process:      Locate the Section by name ([section]) in the S_Section table. When found, position RTBox
        '               to the Section, and update screen fields relating to Section/Line/Cursor. UPdate the
        '               Section's descriptive text. If Section is not empty, extract its first Row into
        '               RTBoxLine, parse it, and update the Tag Count for this XML row.
        ' Called By:    The Menu-Bar "Section" entries, one Call-Point for each Section.
        ' Side Effects: Alters Global Variables. Populates RTBoxLine. Positions in RTBox. Updates MAIN form fields.
        ' Notes:        Reorder logic surrounding isEmpty. Does S_Section.firstLineStart/EndPos define the first
        '               Row or the the first line, so RTBoxLine is populated OK. Repositioning RTBox executes twice,
        '               once at beginning, once at end - redundant? Setting RTBox focus should be after .rtf load,
        '               otherwise focus shifts away from the ODF onto its descriptive text. Search loop can be
        '               exited once found - should never be two instances of same Section, and if there is, just
        '               load the first.

        ' memoriser nom de la section precedente
        ' MsgBox(G_SectionName & vbCrLf & G_LastSectionName)
        ' If G_SectionName <> Nothing Then G_LastSectionName = G_SectionName
        ' trouver la section active

        Dim idx As Integer                                                  ' Loop index as we pass through all 44 Sections

        For idx = 1 To 44                                                   ' Do a linear search over S_Section array for [section]==.Title
            If S_Section(idx).title = section Then                          ' We've located the Section data
                G_Section = idx

                RTBox.Focus()                                               ' scroller de façon a avoir le titre de la section en haut de rtbox 
                RTBox.Select(S_Section(G_Section).startPos, 0)              ' Position the Section Header Start-Tag at the top of the RTBox display
                RTBox.ScrollToCaret()
                G_CaretPos = RTBox.SelectionStart

                If S_Section(G_Section).isEmpty = False Then                ' trouver et selectionner la 1ere ligne de la section, si la section n'est pas vide
                    RTBox.Select(S_Section(G_Section).firstLineStartPos,    ' If there is content in this Section, extract first Row to into G_LineText
                                 S_Section(G_Section).firstLineEndPos -
                                 S_Section(G_Section).firstLineStartPos + 4)
                    G_LineText = RTBox.SelectedText
                Else                                                        ' Section is empty, so set variable to reflect null content.
                    G_LineText = ""
                End If

                LabelSectionStart.Text = S_Section(G_Section).startPos      ' afficher infos debut et fin; update Section Start/End on screen
                LabelSectionEnd.Text = S_Section(G_Section).endPos
                LabelSection.Text = S_Section(G_Section).name               ' afficher le nom de la section; update the Section Name on screen
                G_SectionName = S_Section(G_Section).name                   ' And save name of current Section
                G_LineIndex = RTBox.GetLineFromCharIndex(G_CaretPos)        ' caalculer et afficher n° dee ligne; update Line Number
                LabelLineNumber.Text = G_LineIndex.ToString                 ' And place onscreen

                If S_Section(G_Section).isEmpty = False Then
                    RTBoxLine.Text = G_LineText                             ' afficher la ligne dans RTBoxLine; display text for SEction's first Row
                    CountTags(1, RTBoxLine.TextLength)                      ' compter les tags de la ligne dans RTBoxLine; calculate and display the number of Elements in this Row
                    DisplayObject()                                         ' montrer les infos de la ligne; build and display all relevant Element Names & Content
                    '                                                         <1.059.0> Removed parameter (1), DisplayObject is now parameterless
                Else                                                        ' No content, clear RTBoxLine and Element Name/Content info
                    RTBoxLine.Text = ""
                    ClearTagsPanel()
                End If


                RTBox.Focus()                                               ' scroller de façon a avoir le titre de la section en haut de rtbox; position Section Start-Tag at top of RTBox
                RTBox.Select(S_Section(G_Section).startPos, 0)
                RTBox.ScrollToCaret()

                G_SectionName = S_Section(G_Section).name                   ' charger le fichier RTF; update the Section descriptive text
                'MsgBox(G_SectionName & vbCrLf & G_LastSectionName)
                LoadRTFFile()
            End If
        Next

    End Sub
    Private Sub OldGetSectionFromMenu(section As String)

        ' TROUVER UNE SECTION DE L'ORGUE A PQRTIR DU MENU

        ' Purpose:      Deprecated, replace by GetSectionFromMenu(section). This variant looked for Sections
        '               dynamically, rather than using precomputed S_Section data.
        ' Called By:    <None>
        ' Side Effects: <NA>
        ' Notes:        Should Section handling be a combination of Static & Dynamic e.g. switching to dynamic processing
        '               while editing, reverting to static when not in Edit Mode. Suspect that static was to minimize
        '               processing times through very large ODF files. However, extensive use is made of dynamic searching
        '               throughout the code, except for when going to a Section via the Menu bar. Consider auto recomputing
        '               when both: a) text length has changed since last recompute, and; b) Menu repositioning is done.
        '               Can also recompute when leaving Edit Mode or Saving File while in Edit Mode, if text has changed.

        ' memoriser nom de la section precedente
        If G_SectionName <> Nothing Then G_LastSectionName = G_SectionName
        ' trouver le début de section
        G_StartPos = FindMyText(section, 1)
        G_EndPos = G_StartPos + Len(section)
        G_SectionStart = G_StartPos
        RTBox.SelectionStart = G_StartPos
        RTBox.SelectionLength = Len(section)
        RTBox.SelectionFont = New Font("Arial", 11)
        RTBox.SelectionColor = Color.Red
        LabelSectionStart.Text = G_StartPos.ToString
        ' trouver fin de section
        G_SectionEnd = FindMyText("</ObjectList>", G_StartPos)
        LabelSectionEnd.Text = G_SectionEnd.ToString

        ' MODIF 026
        ' trouver le premier <o> apres la section
        G_LineStart = FindMyText("<o>", G_SectionStart)
        GetLineFromIndex(G_LineStart)
        ' afficher la ligne dans RTBoxLine
        RTBoxLine.Text = G_LineText
        ' retrouver la section auquel il appartient
        GetSectionFromIndex(G_LineStart)
        ' compter les tags de la ligne dans RTBoxLine
        CountTags(1, RTBoxLine.TextLength)
        ' montrer les infos de la ligne
        DisplayObject()                                          ' <1.059.0> Removed parameter (1), DisplayObject is now parameterless
        ' se positionner sur la ligne avec le nom de la section
        RTBox.SelectionStart = G_SectionStart
        RTBox.SelectionLength = 0

        ' charger le fichier RTF
        LoadRTFFile()


        Exit Sub
        ' trouver la premiere ligne objet si elle existe
        GetFirstLine(G_StartPos)
        ' Afficher le nom de la section
        G_SectionName = section.Substring(24, Len(section) - 26)
        LabelSection.Text = G_SectionName
        ' charger le fichier RTF
        LoadRTFFile()

    End Sub
    Private Sub GetFirstLine(startPos As Integer)

        ' trouver la premiere ligne objet d'une section si cette ligne existe

        ' Purpose:      Deprecated
        ' Called By:    OldGetSectionFromMenu()
        ' Side Effects: <NA>
        ' Notes:        <None>

        G_LineStart = FindMyText("<o>", startPos)
        G_LineEnd = FindMyText("</o>", startPos)

        If G_LineStart < G_SectionEnd Then
            LabelLineStart.Text = G_LineStart.ToString
            LabelLineEnd.Text = G_LineEnd.ToString
            ' afficher la 1ere ligne entiere si elle existe
            RTBox.SelectionStart = G_LineStart
            RTBox.SelectionLength = G_LineEnd - G_LineStart + 4
            RTBoxLine.Text = RTBox.SelectedText
            ' afficher les valeurs des tags de la 1ere ligne
            DisplayObject()                                          ' <1.059.0> Removed parameter (G_LineStart), DisplayObject is now parameterless
        Else
            LabelLineStart.Text = "No line"
            LabelLineEnd.Text = "No line"
            RTBoxLine.Text = "No Item in this section"
        End If

    End Sub
    Private Sub DisplayTagText(ByRef lastIdx As Integer,        ' Integer representation of letters ('a' to 'z+')
                               ByRef counter As Integer,        ' Number of Elements already identified and displayed
                               nbTags As Integer,               ' Known number of Elements to display in all
                               labelTag As Control,             ' Onscreen Form-field, display the Element's Name here
                               textTag As Control)              ' Onscreen Form-field, display the Element's Content here

        ' AFFICHE DANS LE PANNEL LE TAG ET SON CONTENU

        ' Purpose:      Find the next Element in a Row, starting with Name=lastIDX; when found, display the Name and
        '               Content in the designated Form-fields. In coordination with DisplayObject, returns the
        '               Elements one at a time, in alphabetical order by Name/Type.
        ' Process:      Upon entry, [lastIdx] represents the current alphabetic character to search for (a, b, c,
        '               ... encoded as integers). The entire XML Row is searched: if not found, [lastIdx] is
        '               advanced to the next character, and the search loops until a matching Element Name is
        '               found, or the range of possible Names is exhausted. When found, the Name & Content is extracted,
        '               the tally of Elements-found is incremented ([counter]), and [lastIdx] is left advanced one-past
        '               the located Element, in preparation for the next search. Once the number of parsed Elements
        '               ([counter]) reaches the known number of Elements in the Row ([nbTags]), this routine no longer
        '               searches, it just fills the designated Form-fields with blanks.
        ' Called By:    DisplayObject()
        ' Side Effects: When the row is part of Sections "ImgageSet" or "ImageSetElement", global variables relating
        '               to Images are assigned values from Elemnt Content.
        ' Notes:        Could be re-written to do a single left to right scan parsing Elements, maintaining an
        '               alphabetized list, then make a single pass to display the fields. For/Next loop is better as While loop,
        '               as search success is also an exit criteria.

        Dim idx As Integer                                      ' Current position in loop, ranging from lastIdx to 'z'+27
        Dim txt As String                                       ' Content of a Element

        If counter < nbTags Then                                ' We haven't parsed every known Element yet, so execute parsing logic

            For idx = lastIdx To 122 + 27                       ' recherche des tags <a> à <z>; search for names from <a>...<z>, <a1>...<z1>

                Select Case idx
                    Case Is <= 122                              ' idx is between 'a' and 'z' inclusive
                        labelTag.Text = Chr(idx)                ' Convert idx to the equivalent character e.g. 97 -> 'a'
                        txt = ReadTag(Chr(idx), 1)              ' Search entire Row for an Element with this Name
                        textTag.Text = txt                      ' Place Element's Content onscreen (blank if not found)

                        If G_SectionName = "ImageSet" Then      ' si presence image; if inside ImageSet, save useful info
                            If idx = 99 Then                    ' Lettre c :  package ID; Element is a PackageID

                                While Len(txt) < 6              ' le packageID doit avoir 6 chiffres; left-pad with zeroes to length 6
                                    txt = "0" & txt
                                End While
                                G_PackageID = txt & "\"         ' And add backslash suffix: "1234" -> "001234\"
                            End If

                            If idx = 106 Then                   ' Lettre j : mask de transparence, s'il existe; Element is an image mask file
                                G_ImageFile = txt               ' Save the filename for use in "Display Image"
                            End If
                        End If

                        If G_SectionName = "ImageSetElement" Then
                            If idx = 97 Then                    ' Lettre a : image set; Element is the related ImageSetID
                                G_ImageSet = txt                ' Save ImageSetID for later use
                            End If
                            If idx = 98 Then                    ' Lettre b : index dans image set; Element is ImageIndex
                                G_ImageIndex = txt              ' Save ImageIndex for later use
                            End If
                            If idx = 100 Then                   ' Lettre d : bitmap filename; Element is ImageFile (name of the file)
                                G_ImageFile = txt               ' Save the filename for later use in "Display Image"
                                'MsgBox(G_ImageSet & vbCrLf & G_ImageIndex & vbCrLf & G_ImageFile)
                            End If

                        End If

                    Case Is > 122                               ' tags <a1>, <b1> ...; idx is > 'z': after <z> comes <a1>...<z1>
                        labelTag.Text = Chr(idx - 26) & "1"     ' Create numeric-suffix Element Names from idx
                        txt = ReadTag(Chr(idx - 26) & "1", 1)
                        textTag.Text = txt

                    Case Else                                   ' This seems in error - two prior cases cover all possibilities
                        txt = ""
                End Select

                If txt <> "" Then                               ' When not null, then the Element was found
                    lastIdx = idx + 1                           ' For next entry, start one Element Name past the Name just found
                    counter += 1                                ' Advance count of Elements found, exit loop
                    Exit For
                End If
            Next

        Else                                                    ' We've already parsed every known Element, just clear the Name & Content displays
            labelTag.Text = ""
            textTag.Text = ""
        End If

    End Sub
    Private Sub DisplayObject()                                        ' <1.059.0> Deleted parameter linestart, never used in code; aligned calls to DisplayObject to match

        ' AFFICHE LES TAGS ET LEUR CONTENU DANS LE PANEL

        ' Purpose:      Parse contents of RTBoxLine (ODF Row), displaying all Element Names & Content in alphabetical
        '               order. Ignores the Parent '<o>...</o>' Element. For Sections that link to image files, present
        '               the control to optionally locate and display an image file specified by an Element, if any.
        ' Process:      Visit each of the 24 fixed positions on the panel for displaying Element Names and Content,
        '               in alpahbetical order. Call DisplayTagText(), which searches for the presence of the next
        '               possible Element Name, starting the search each time at lastIdx. Upon return from
        '               DisplayTagNext(), lastIdx is set to 1 more than the last Element found, and [counter] has been
        '               incremented. The search range is from "<a>" to "<z>", then "<a1>" to "<z1>". DisplayTagText()
        '               also places the Name and Content of a parsed Elemet into the designated form fields. Once
        '               [counter] exceeds the known number of Elements in the Row, further display fields are simply
        '               cleared, erasing any prior content from a previous Element.
        ' Called By:    RTBox_DoubleClick(); GetSectionFromMenu(); GetFirstLine(); NextLIne_commun();
        '               OldGetSectionFromMenu() (deprecated)
        ' Side Effects: Updates all 24 pairs of Form-fields used to display Element Names & Content; Selects and
        '               Highlights the Row in the RTBOX (ODF) display.
        ' Notes:        DisplayImage button should only be enabled for rows that have content for a filename.
        '               Image-related Globals should be cleared if processing in the ImageSet or ImageSetElement
        '               Sections, to avoid carrying over an older, now incorrect value if the associated Elements
        '               are not found. Consider re-writing to use an artificial array of Controls for labelTag and
        '               textTag, and substituing a loop for the 24 explicit call seen below.

        Dim lastIdx As Integer = 97                                     ' The current Element Name to look for - cycled from a to z1. Expressed as ASCII value
        Dim counter As Integer = 0                                      ' Number of Elemnts parsed and displayed so far. Updated by DisplayTagText().
        Dim nbtags As Integer = CountTags(1, RTBoxLine.TextLength)      ' Number of Child Elements in the row: once [count] reaches [nbtags], display fields are cleared
        Dim labelTag As Control                                         ' Parameter to inform DisplayTagText() which Form-field is to display the Element Name (or blank)
        Dim txtTag As Control                                           ' Parameter to inform DisplayTagText() which Form-field is to display the Element Content (or blank)

        ShowTagsPanel()                                                 ' Make all 24-pairs of Element Form-fields visble, in preparation for parsing & display.

        If G_SectionName = "ImageSet" Or G_SectionName = "ImageSetElement" Then
            ButtonDisplayImage.Visible = True                           ' These two Sections can have rows that link to image files: enable screen control that displays those images.
        Else
            ButtonDisplayImage.Visible = False                          ' Hide the control for non-Image Sections.
        End If

        labelTag = LabelTag1 : txtTag = tag1                            ' Process each of the 24 Form-field pairs in order: DisplayTagText() fills in blanks once all Child Elements are parsed.
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag2 : txtTag = tag2
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag3 : txtTag = tag3
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag4 : txtTag = tag4
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag5 : txtTag = tag5
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag6 : txtTag = tag6
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag7 : txtTag = tag7
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag8 : txtTag = Tag8
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag9 : txtTag = Tag9
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag10 : txtTag = tag10
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag11 : txtTag = tag11
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag12 : txtTag = tag12
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag13 : txtTag = tag13
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag14 : txtTag = tag14
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag15 : txtTag = tag15
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag16 : txtTag = tag16
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag17 : txtTag = tag17
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag18 : txtTag = tag18
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag19 : txtTag = Tag19
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag20 : txtTag = Tag20
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag21 : txtTag = Tag21
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag22 : txtTag = Tag22
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag23 : txtTag = Tag23
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)
        labelTag = LabelTag24 : txtTag = Tag24
        DisplayTagText(lastIdx, counter, nbtags, labelTag, txtTag)


        RTBox.SelectionStart = G_LineStart                              ' se repositionner en debut de ligne et selectionner la ligne; Select & Highlight the original ODF Row.
        RTBox.SelectionLength = G_LineEnd - G_LineStart + 4             ' 4 pour inclure </o>; including the Parent Element's End-Tag (the "</o>").
        RTBox.Refresh()                                                 ' Repaint the RTBox (ODF) onscreen.

    End Sub
    Private Sub LoadRTFFile()                                           ' CHARGE LE FICHIER RTF DECRIVANT UNE SECTION

        ' Purpose:      Display the descriptive text file (.rtf) associated with the current Section, or modele.rtf
        '               if the Section file cannot be located.
        ' Process:		Build the file's name from the path to the application data files, suffixed by the Section
        '               Name, then ".rtf". If this is the same file as displayed the last time through, we're done.
        '               If not, try to locate the file - if it cannot be found, substitute a generic, catch-all
        '               file instead. Display the resulting file into the RTFFile control, and force the font to
        '               10-point Verdana.
        ' Called By:    RTBox_DoubleClick(); GetSectionFromMenu(); NextLineCommun();
        '               OldGetSectionFromMenu() (deprecated)
        ' Side Effects: Updates RTFFile globals, and RTBoxRTF control. 
        ' Notes:        G_PreviousRTFFile should only by updated if something was displayed - the code falsely assumes
        '               either the Section file or modele.rtf is found, even though it checks for existence in both
        '               cases. Consider making global File variables local static, and receiving target Section name
        '               as a variable. Has bug were display of Help file invalidates the content of this display
        '               control, but re-entering the same Section will not repaint the Section text over the Help text.
        '               Move Help file display to its own Window.

        G_RTFFile = G_DataPath & "\" & G_SectionName & ".rtf"           ' Build candidate filename based on the Section Name.

        If G_PreviousRTFFile <> G_RTFFile Then                          ' (Bug if Help file has been loaded...) Check if Section has changed
            If My.Computer.FileSystem.FileExists(G_RTFFile) Then        ' Ensure Section File exists
                RTBoxRTF.LoadFile(G_RTFFile)                            ' It does, so display it.
            Else                                                        ' Section file was not found, so try to load default text instead
                If My.Computer.FileSystem.FileExists(
                    G_DataPath & "\modele.rtf") Then                    ' Check if default file exists
                    RTBoxRTF.LoadFile(G_DataPath & "\modele.rtf")       ' Yes, load it What if both were missing?
                End If
            End If

            G_PreviousRTFFile = G_RTFFile                               ' Remember what file we loaded, to avoid unnecessary repainting

            Dim fnt As New System.Drawing.Font("verdana", 10)           ' Force la police; force display to 10-point Verdana
            RTBoxRTF.Focus()
            RTBoxRTF.Font = fnt
            RTBoxRTF.SelectAll()
            RTBoxRTF.SelectionFont = fnt
            RTBoxRTF.DeselectAll()
        End If

    End Sub
    Private Sub SaveRTFFile(sender As Object,                           ' Standard Control event parms...
                              e As EventArgs
                              ) Handles ButtonSaveRtbox2.Click

        ' activé par ButtonSaveRtbox2_Click

        ' Purpose:      Save the contents of the descriptive-text area back to its original source file. This allows
        '               for editing of the text.
        ' Process:		Disallow altering the help-text file. Otherwise, save the text back to the file for the
        '               current Section.
        ' Called By:    ButtonSaveRtbox2 Click Event
        ' Side Effects: Alters G_RTFFile; alters content in the \DATA directory
        ' Notes:        Should have exception handling for file operations. Not robust, as it (incorrectly) assumes
        '               that the displayed text is always that of the Section G_SectionName. Perhaps better to use
        '               File variable as set by LoadRTFFile(), rather than reconstructing it here.

        If G_RTFFile = G_DataPath & "\help.rtf" Then                    ' Check if attempting to alter the Help file - disallow
            MsgBox("Help file cannot be saved")
            Exit Sub
        End If
        If G_SectionName <> "" Then                                     ' Only save if Section Name is not blank
            G_RTFFile = G_DataPath & "\" & G_SectionName & ".rtf"       ' Construct path and filename
            RTBoxRTF.SaveFile(G_RTFFile)                                ' Save control contents.
        End If

    End Sub
    Private Sub ClearTagsPanel()                    ' VIDE LE PANNEL AFFICHANT LES TAGS

        ' Purpose:      Hide all the controls in the panel used to display Element Names and Contents.
        ' Process:		Let runtime loop through all controls, while we turn off their .Visible propety.
        ' Called By:    MAIN_Load(); GetSectionFromMenu(); Menu_OpenHauptwerkOrgan_Click();
        '               FindBUttonProcedure()
        ' Side Effects: Changes control properties.
        ' Notes:        <None>

        Dim ctrL As Control                         ' Object definition for loop

        For Each ctrL In Me.PanelTags.Controls      ' Cycle through collection of all controls in PanelTags
            ctrL.Visible = False                    ' Hide it
        Next

    End Sub
    Private Sub ShowTagsPanel()                     '  AFFICHANT LES TAGS

        ' Purpose:      Make visible all the controls in the panel used to display Element Names and Contents.
        ' Process:		Let runtime loop through all controls, while we turn on their .Visible propety.
        ' Called By:    DisplayObject()
        ' Side Effects: Changes control properties.
        ' Notes:        <None>

        Dim ctrL As Control                         ' Object definition for loop

        For Each ctrL In Me.PanelTags.Controls      ' Cycle through collection of all controls in PanelTags
            ctrL.Visible = True                     ' "Let there be light..."
        Next

    End Sub
    Private Sub GetSectionsInfos(verbose As Boolean)                                ' Display Section progress in RTBoxRTF as Sections are parsed?

        ' EXAMINE L'ODF ET REPERE LES SECTIONS
        ' AFFICHE SI VERBOSE=TRUE

        ' Purpose:      Parse the ODF, building the S_Section data structure. If [verbose], display progress
        '               in the RTBoxRTF control, updating as each Section in completed. If not [verbose],
        '               clear the RTBoxRTF at the start, but do not post updates. Clicking on ButtonLed
        '               does a non-verbose recompute - all other invocations are verbose.
        ' Process:		Assumes RTBox contains a well-structured ODF. Searches for sucessive instances of
        '               the Section Element Start-Tag, then extracts needed info into the S_Section array.
        '               The process loops until the Start-Tag is no longer found (all Sections have been
        '               processed.
        ' Called By:    Menu_OpenHauptwerkOrgan_Click(); Menu_ExitEditMode_Click();
        '               Menu_ReComputeSections_Click(); ButtonLed_Click()
        ' Side Effects: x
        ' Notes:        Consider making Star/End-Tag constants Global. The general approach is fairly robust,
        '               as missing or out-of-order Sections still result in correct navigation by AECHO.
        '               Is saving the index into S_Section redundant? When extracting the Section Name
        '               (Start-Tag's Attribute Value), perhaps parse dynamically, rather than assuming that
        '               XML whitespace positioning is determanistic. Ditto for the End-Tag. Ditto isEmpty
        '               determination. Should FirstLineEndPosition point to opening "<" of End-Tag (current),
        '               or to last character ">" of End-Tag?

        Dim idx As Integer = 1                                                      ' Index into S_Section array, incremented as we find Section Start-Tags
        Dim startPos As Integer = 1                                                 ' Position within ODF as we parse off data
        'Dim retour As Integer
        Const startText = "<ObjectList ObjectType="                                 ' Identifies Start-Tag text unique to beginning of a Section in ODF
        Const endText = "</ObjectList>"                                             ' The End-Tag text for a Section

        ButtonLed.BackColor = Color.Red                                             ' At the start, change ButtonLed to Red, then back to Green when done
        ButtonLed.Refresh()
        RTBoxRTF.Clear()                                                            ' vider RTBox_RTF; clear the control that displays descriptive/help text

        Do                                                                          ' Boucle; main loop, exit when all Sections have been processed

            startPos = RTBox.Find(startText, startPos, RichTextBoxFinds.None)       ' trouver le tag de début de section' locate the start of the next Section
            If startPos = -1 Then Exit Do                                           ' If no Section Start-Tag is found, we are done
            S_Section(idx).index = idx                                              ' Save the index in the current S_Section slot
            S_Section(idx).startPos = startPos                                      ' Save position of the first character of the Section's Start-Tag ("<")
            S_Section(idx).firstLine = RTBox.GetLineFromCharIndex(
                S_Section(idx).startPos)                                            ' trouver le numero de la 1ere ligne; locate the 0-base line number within RTBox of the Section Start-Tag
            S_Section(idx).titleEnd = RTBox.Find(
                ">", startPos, RichTextBoxFinds.None)                               ' trouver la fin du tag "<ObjectList ObjectType="; locate & save position of end of the Start-Tag
            RTBox.SelectionStart = startPos + 24                                    ' trouver le nom de la section
            RTBox.SelectionLength = S_Section(idx).titleEnd - (startPos + 25)
            S_Section(idx).name = RTBox.SelectedText                                ' Locate & save the Section Name from the Start-Tag's "ObjectType" Attribute Value
            S_Section(idx).title = startText & """" & S_Section(idx).name & """>"   ' titre de la section; recreates the entire Start-Tag, assuming no other Attributes!
            ' retour = FindMyText(endText, startPos)
            S_Section(idx).endPos = RTBox.Find(endText, startPos,
                                               RichTextBoxFinds.None) + 13          ' trouver la fin de la section; locate the Section's End-Tag, save position of its last character
            S_Section(idx).len = (S_Section(idx).endPos - S_Section(idx).startPos)  ' recherche sections vides; calculate and save the Section's length

            If S_Section(idx).len = Len(startText &                                 ' Check if Section is empty
                                        endText & S_Section(idx).name) + 4 Then     ' If lengths match, code is assuming (incorrectly) that the Section is empty
                S_Section(idx).isEmpty = True                                       ' Mark Section as empty i.e. no Child Elements
                S_Section(idx).firstLineStartPos = -1                               ' il n'y aura donc pas de 1ere ligne; if empty, set position of first Child Element to -1
            Else
                S_Section(idx).isEmpty = False                                      ' Mark Section as not Empty e.g. it is assumed to contain at least one Child Element
                S_Section(idx).firstLineStartPos = S_Section(idx).titleEnd + 1      ' trouver debut 1ere ligne; save beginning of fir Child Element Row
                ' si la ligne commence bien par <o> ....
                'MsgBox(S_Section(idx).firstLineStartPos.ToString & "      " & RTBox.Find("<o>", S_Section(idx).firstLineStartPos, RichTextBoxFinds.None).ToString)
                'If RTBox.Find("<o>", S_Section(idx).firstLineStartPos, RichTextBoxFinds.None) <> -1 Then
                ' trouver la fin de la 1ere ligne
                S_Section(idx).firstLineEndPos =
                    RTBox.Find("</o>",
                               S_Section(idx).firstLineStartPos,
                               RichTextBoxFinds.None)                               ' Locate and save position of first char of the first Child Element's End-Tag
                'End If
            End If

            ColorSectionsTitles(idx, Color.Blue)                                    ' colorer le titre de la section
            If verbose = True Then                                                  ' verif dans rtbox_rtf; if verbose, add a Section Info Line to RTBoxRTF
                RTBoxRTF.Text += vbCrLf & idx.ToString & "    " & S_Section(idx).startPos.ToString & " to  " & S_Section(idx).endPos.ToString & "    " & S_Section(idx).name
            End If

            startPos = S_Section(idx).endPos + 1                                    ' nouvelle position de depart; continue parsing after end of this Section

            'MsgBox("Section Structure - idx = " & idx & vbCrLf &
            '       "   index                  " & S_Section(idx).index & vbCrLf &
            '       "   title                  " & S_Section(idx).title & vbCrLf &
            '       "   name                   " & S_Section(idx).name & vbCrLf &
            '       "   startPos               " & S_Section(idx).startPos & vbCrLf &
            '       "   endPos                 " & S_Section(idx).endPos & vbCrLf &
            '       "   len                    " & S_Section(idx).len & vbCrLf &
            '       "   firstLine              " & S_Section(idx).firstLine & vbCrLf &
            '       "   titleEnd               " & S_Section(idx).titleEnd & vbCrLf &
            '       "   isEmpty                " & S_Section(idx).isEmpty & vbCrLf &
            '       "   firstLineStartPos      " & S_Section(idx).firstLineStartPos & vbCrLf &
            '       "   firsLineEndPos         " & S_Section(idx).firstLineEndPos)

            idx += 1                                                                ' nouvel index; advance to next slot in S_Sections array
            Application.DoEvents()                                                  ' Allow screen to update
        Loop

        ButtonLed.BackColor = Color.LightGreen : ButtonLed.Refresh()

    End Sub
    Private Sub ColorSectionsTitles(idx As Integer,                                 ' Index into S_Sections array, entry contains Position Information
                                    color As Color)                                 ' Color to apply to Title's font

        ' COLORIER LE TITRE DE LA SECTION IDX

        ' Purpose:      Apply a color to Section[idx] Title text in the ODF
        ' Process:		Use the S_Section array to locate the start and end of the Title Text in the ODF;
        '               select it, force the font to 11-point Arial, and apply the desired color. Deselect.
        ' Called By:    GetSectionsInfos()
        ' Side Effects: Change the font settings of some text in RTBox
        ' Notes:        Perhaps change to applying a Font, Size, Style, and Color to any Font range -
        '               then use to color several key elements of the ODF display

        RTBox.SelectionStart = S_Section(idx).startPos
        RTBox.SelectionLength = S_Section(idx).titleEnd - S_Section(idx).startPos
        RTBox.SelectionFont = New Font("Arial", 11)
        RTBox.SelectionColor = color
        RTBox.DeselectAll()

    End Sub
    Private Sub NextLineCommun()

        ' Purpose:      After using an advance Line (1, 10, or 100) control, locate the Section, update info, parse
        '               and display the first Row.
        ' Process:		Find the Cursor's line position, search backwards for the closest Section Start-Tag
        '               (this is falsely assumed to be the Section we are in), display the Row content in the
        '               Row display, update Section display information, Child Element Count, parse and display
        '               the Child Element Names and Content, select the current Row in the ODF.
        ' Called By:    ButtonNextLine_click(); ButtonNext10Lines_click(); ButtonNext100Lines_click()
        ' Side Effects: Update form fields, ODF display, Row display, Section Elements panel display,
        '               Section descriptive text.
        ' Notes:        Positioning logic needs work - moving into a Section Start or End-Tag causes errors
        '               in the Element display panel. Section positioning can be damaged at Section boundaries. 

        Dim sectionHead As Integer                                  ' Position of Section's Start-Tag

        GetLineFromIndex(G_CaretPos)                                ' Trouver et selectionner la ligne; determine the line in the ODF where the Cursor is positioned
        sectionHead = FindReverse("<ObjectList", G_CaretPos)        ' verifie que la ligne appartient bien à la meme section; search backwards to find the closest Section Start-Tag

        If G_LineStart < sectionHead Then                           ' Mystery Comment - triggers when stepping to Section End-Tag of an Empty Section.
            MsgBox("This line is not in the current section. Please try again.", , "Function Get_line_From_Index") 'La ligne n'est pas dans la meme section
        End If

        RTBoxLine.Text = G_LineText                                 ' afficher la ligne dans RTBoxLine; copy the single Row into RTBoxLine
        GetSectionFromIndex(G_LineStart)                            ' retrouver la section auquel il appartient; locate and load Section Data for the now current Section

        CountTags(1, RTBoxLine.TextLength)                          ' compter les tags de la ligne dans RTBoxLine; determine and display the number of Child Elements in this Row
        DisplayObject()                                             ' montrer les infos de la ligne; <1.059.0> Removed parameter (1), DisplayObject is now parameterless; parse & display Row

        RTBox.SelectionStart = G_LineStart                          ' selectionner la ligne; Select the Row in the ODF
        RTBox.SelectionLength = G_LineEnd - G_LineStart

        LoadRTFFile()                                               ' charger le fichier RTF; retrieve and display Section Descriptive Text

    End Sub

    ' MENU FILES
    Private Sub Menu_OpenHauptwerkOrgan_Click(                      ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles Menu_OpenHauptwerkOrgan.Click

        '   <1.059.0> Modified loading of ODF data to correctly decode UTF8

        ' Purpose:      Choose and load an ODF File into RTBox, and buid the S_Section array to assist in navigation.
        '               If the open in cancelled, AECHO's state remains unchanged.
        ' Process:		Use the standard dialog to select the file to open. Check its length, enforcing a
        '               1MB limit if unregistered (all AECHO's are registered as of 1.057.0). Enable the menus
        '               and clear the Child Elements panel. Open and read the file into memory
        '               (RTBox). Parse the Sections, building the S_Section array, and updaing the screen.
        ' Called By:    Menu_OpenHauptwerkOrgan Click Event
        ' Side Effects: Updates valrious globals, loads RTBox with initial content.
        ' Notes:        Change max file length allowed, Title-bar text to Constants. Consider saving last directory
        '               used in Properties. Make file-open more robust, fielding exceptions. Only process the
        '               other functions if the file is loaded with error.

        Dim sizeInBytes As Integer                                          ' Length of ODF file that is to be opened
        Dim file As FileInfo                                                ' FileInfo object used to determine file length

        OpenFileDialog.RestoreDirectory = True                              ' Use Windows standard open-file: restore to initial directory when done
        OpenFileDialog.InitialDirectory = G_InitialDir                      ' "D:\HAUPTWERK\HauptwerkSampleSetsAndComponents\OrganDefinitions"
        'MsgBox("G_InitialDir: " & G_InitialDir)
        OpenFileDialog.Title = "Open one organ"
        OpenFileDialog.DefaultExt = "Organ_Hauptwerk_xml"
        OpenFileDialog.Filter =
            "organ files (*.Organ_Hauptwerk_xml)|*.Organ_Hauptwerk_xml"     ' <1.059.0> added the closing ) from 1.058b
        OpenFileDialog.FileName = ""                                        ' MODIF V058

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then               ' <1.059.0> extraneous System.Windows.Forms reference removed; User selected a file

            G_OrganFile = OpenFileDialog.FileName                           ' Save the name of the file

            file = New FileInfo(G_OrganFile)                                ' tester longueur du fichier; test if file is longer than allowed for unregistered AECHO
            sizeInBytes = file.Length                                       ' As of 1.057.0, AECHO is freeware, so file length will not matter
            'MsgBox("taille : " & sizeInBytes.ToString)
            If G_Registered = False And sizeInBytes > 1024000 Then          ' Max allowable in not regsistered in 1MB
                MsgBox("DEMO VERSION " & vbCrLf &
                       "Only ODF smaller than 1 Mb can be opened with this version." &
                       vbCrLf & "(your file is " & sizeInBytes.ToString & ")")
                OpenFileDialog.Dispose()                                    ' We're done here. Go away now.
                Exit Sub
            End If

            Me.Text = "Analyzer/Editor for Compiled Hauptwerk Organs   File = " &
                G_OrganFile                                                 ' Update the Windows Title Bar. <1.059.0> changed text to "Compiled"

            OpenFileDialog.Dispose()                                        ' Clean up
            SectionsMenuItem.Enabled = True                                 ' valider les menus Sections et boutons; enable the menu sections and buttons
            NextSectionsMenuItem.Enabled = True
            Menu_EDITMODE.Enabled = True
            ToolsMenuItem.Enabled = True
            ButtonLed.Enabled = True

            ClearTagsPanel()                                                ' effacer; clear the Child Elements panel
            PBox.Visible = False                                            ' And hid it
            G_SectionName = ""                                              ' No current not previous Section, yet
            G_LastSectionName = ""
            G_PreviousRTFFile = ""                                          ' No prior decriptive text file opened, yet

            RTBox.Text = My.Computer.FileSystem.ReadAllText(                ' Copier dans la RichTextBox
                G_OrganFile, System.Text.Encoding.UTF8)                     ' <1.059.0>, load RTBox, reading ODF file forcing UTF-8 decoding
            'RTBox.LoadFile(G_OrganFile, RichTextBoxStreamType.PlainText)   ' Pre V1.059 code, assumed non-UTF8 text stream
            RTBox.SelectAll()                                               ' Force all text to a uniform MS Sans, 10 point, Regular-Stlye font
            RTBox.SelectionFont = New Font(
                New System.Drawing.FontFamily("microsoft sans serif"),
                10, System.Drawing.FontStyle.Regular)
            RTBox.SelectionLength = 0                                       ' Once set, undo Select
            G_ODFLength = RTBox.TextLength                                  ' Initial length of ODF representation in RTBox

            GetSectionsInfos(conVerbose)                                    ' calculer les sections; Parse ODF Sections, displaying results (verbose)
            GetPackagePath(G_OrganFile)                                     ' trouver le path pour OrganInstallationPackages; Location of Packages: sounds, iamge files...

        End If

    End Sub
    Private Sub Menu_SaveAs_Click(                              ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles Menu_SaveAs.Click

        ' SAUVE L'ODF MODIFIE

        ' Purpose:      Save the internal ODF to a designated Organ File.
        ' Process:		Build Save dialog, if User approves, write out the data. Force UTF-8 without BOM.
        ' Called By:    Menu_SaveAs Click Event
        ' Side Effects: Writes data to the file system
        ' Notes:        Consider Save's InitialDirectory should be either the last directory from which an ODF
        '               was loaded, or the actual HW directory (not necessarily "D:\"). Harden exception
        '               handling.

        'saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        'saveFileDialog1.FilterIndex = 2
        Dim saveFileDialog As New SaveFileDialog With {                                 ' Setup Save Dialog
            .Title = "Save the ODF as ...",
            .DefaultExt = "Organ_Hauptwerk_xml",
            .Filter = "organ files (*.Organ_Hauptwerk_xml)|*.Organ_Hauptwerk_xml",      ' <1.059.0> Added closing )
            .InitialDirectory = "D:\HAUPTWERK\HauptwerkSampleSetsAndComponents\OrganDefinitions",
            .RestoreDirectory = True
        }

        If saveFileDialog.ShowDialog() = DialogResult.OK Then                           ' <1.059.0> Write out RTBox content to the ODF forcing UTF8 encoding
            Dim UTF8NoBOM = New System.Text.UTF8Encoding(False)                         ' <1.059.0> Supress insertion of UTF-8 BOM at beginning of text stream
            My.Computer.FileSystem.WriteAllText(saveFileDialog.FileName,
                                                RTBox.Text, False, UTF8NoBOM)
            'RTBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText)   ' Pre V1.059 code, wrote file with default encoding

            ' myStream = saveFileDialog1.OpenFile()
            ' If (myStream IsNot Nothing) Then
            '' Code to write the stream goes here.
            ' myStream.Close()
            'End If
        End If

    End Sub
    Private Sub Menu_Quit_Click(                            ' Standard Control event parms...
            sender As Object,
            e As EventArgs) Handles Menu_Quit.Click

        ' Purpose:      Terminate AECHO
        ' Process:		You can figure this one out by yourself...
        ' Called By:    Menu_Quit Click Event
        ' Side Effects: Exits AECHO
        ' Notes:        Consider adding "Would you like to save..." if ODF is altered since
        '               the last save.

        End                                                 ' Just leave

    End Sub
    Private Sub GetPackagePath(path As String)              ' Standard Control event parms...

        ' trouver le path pour OrganInstallationPackages à partir de G_OrganFile

        ' Purpose:      Build the Organ Installation Packages directory path, a peer of the Organ
        '               Definitions directory.
        ' Process:		Starting with the supplied path (including filename) of the ODF file, trim the filename,
        '               then replace the ending "\OrganDefinitions\" with "\OrganInstallationPackages\"
        ' Called By:    Menu_OpenHauptwerkOrgan_Click()
        ' Side Effects: Changes G_PackagePath
        ' Notes:        Consider making a function, removing side-effect. New parsing algorithm.

        Dim idx As Integer                                  ' Index to last-seen "\" in [path]
        Dim retour As Integer = 0                           ' Index to current "\" in [path]

        Do                                                  ' Search for the last "\" in [path], from left to right
            idx = retour
            retour = InStr(retour + 1, path, "\")           ' Find next "\", returns 0 when not found
        Loop Until retour = 0                               ' idx now points at final "\"

        ' Example: [path] = "C:\Users\jean-paul\Downloads\OrganDefinitions\Bonoldi.Organ_Hauptwerk_xml" at entry
        '       "\" is found when retour = 3, 9, 19, 29, and 46
        '       then retour = 0 because there are no more backslashes in the string
        '       retour = 0, but idx retains the last positive value found: 46
        '       once retour = 0, we can delete the filename which is the text on the right of the last "\"

        path = path.Remove(idx, Len(path) - idx)            ' Trim off the filename: [path]="C:\Users\jean-paul\Downloads\OrganDefinitions\"
        path = path.Remove(Len(path) -                      ' Trim off "OrganDefinitions\": [path]="C:\Users\jean-paul\Downloads\"
                           Len("organdefinitions\"),
                           Len("organdefinitions\"))
        path += "OrganInstallationPackages\"                ' Append "OrganInstallationPackages\"
        'MsgBox(path)
        G_PackagePath = path                                ' Global is now "C:\Users\jean-paul\Downloads\OrganInstallationPackages\"

    End Sub
    Private Sub ReadInitialDir()

        ' Dir par defaut

        ' Purpose:      Set G_InitialDir to the (presumed) location of the ODF file.
        ' Process:		Hardcoded default, check if initialdir.txt exists, if so, use its contents
        '               for the Initial Directory.
        ' Called By:    Main_Load
        ' Side Effects: Alters G_InitialDir
        ' Notes:        Make default path string a constant at top of code.  Use Properties to save last known
        '               good path, rather than initialdir.txt

        Dim sr As StreamReader                                                              ' Stream to use to read initialdir.txt file

        G_InitialDir = "D:\HAUPTWERK\HauptwerkSampleSetsAndComponents\OrganDefinitions"     ' Default path if initial.txt is missing

        Try                                                                                 ' Absorb exceptiom, tagets "File not found..."
            If File.Exists(G_DataPath & "/initialdir.txt") Then                             ' File exists
                'MsgBox("initialdir.txt exists: " & G_DataPath)
                sr = New StreamReader(G_DataPath & "/initialdir.txt")                       ' Open the file
                G_InitialDir = sr.ReadLine                                                  ' Read its record
                'MsgBox(G_InitialDir)
                sr.Close()                                                                  ' Close
            End If
        Catch
        End Try

    End Sub

    ' MENU SECTIONS
    ' Purpose:      The routines process the 43 "Select a Section" Menu Bar choices, to navigate directly
    '               to the desired Section in the ODF.
    ' Process:		Pass the hard-coded name of the Section to GetSectionFromMenu(), based on which item
    '               is chosen from the Menu Bar.
    ' Called By:    Each of the Section Menu choices' Click Events...
    ' Side Effects: NA
    ' Notes:        Presently ignores the first section of the 44, "_General". Add _General to the codebase.
    '               This can be shortened to a single procedure call (rather than 43 or 44), calculating an
    '               index from the choice of Section in the Menu Bar, then using that index to retrieve the
    '               Section's Name, and finally invoke GetSectionFromMenu(SectionChoice) from a single spot.

    Private Sub Menu_DisplayPage_Click(sender As Object, e As EventArgs) Handles Menu_DisplayPage.Click
        GetSectionFromMenu(section01)
    End Sub
    Private Sub Menu_TextStyle_Click(sender As System.Object, e As System.EventArgs) Handles Menu_TextStyle.Click
        GetSectionFromMenu(section02)
    End Sub
    Private Sub Menu_TextInstance_Click(sender As System.Object, e As System.EventArgs) Handles Menu_TextInstance.Click
        GetSectionFromMenu(section03)
    End Sub
    Private Sub Menu_ImageSet_Click(sender As System.Object, e As System.EventArgs) Handles Menu_ImageSet.Click
        GetSectionFromMenu(section04)
    End Sub
    Private Sub Menu_ImageSetElement_Click(sender As System.Object, e As System.EventArgs) Handles Menu_ImageSetElement.Click
        GetSectionFromMenu(section05)
    End Sub
    Private Sub Menu_ImageSetInstance_Click(sender As System.Object, e As System.EventArgs) Handles Menu_ImageSetInstance.Click
        GetSectionFromMenu(section06)
    End Sub
    Private Sub Menu_KeyImageSet_Click(sender As System.Object, e As System.EventArgs) Handles Menu_KeyImageSet.Click
        GetSectionFromMenu(section07)
    End Sub
    Private Sub Menu_Division_Click(sender As System.Object, e As System.EventArgs) Handles Menu_Division.Click
        GetSectionFromMenu(section08)
    End Sub
    Private Sub Menu_DivisionInput_Click(sender As System.Object, e As System.EventArgs) Handles Menu_DivisionInput.Click
        GetSectionFromMenu(section09)
    End Sub
    Private Sub Menu_Switch_Click(sender As System.Object, e As System.EventArgs) Handles Menu_Switch.Click
        GetSectionFromMenu(section10)
    End Sub
    Private Sub Menu_SwitchLinkage_Click(sender As Object, e As EventArgs) Handles Menu_SwitchLinkage.Click
        GetSectionFromMenu(section11)
    End Sub
    Private Sub Menu_SwitchExclusiveSelectGroup_Click(sender As Object, e As EventArgs) Handles Menu_SwitchExclusiveSelectGroup.Click
        GetSectionFromMenu(section12)
    End Sub
    Private Sub Menu_SwitchExclusiveSelectGroupElement_Click(sender As Object, e As EventArgs) Handles Menu_SwitchExclusiveSelectGroupElement.Click
        GetSectionFromMenu(section13)
    End Sub
    Private Sub Menu_Keyboard_Click(sender As Object, e As EventArgs) Handles Menu_Keyboard.Click
        GetSectionFromMenu(section14)
    End Sub
    Private Sub Menu_KeyboardKey_Click(sender As Object, e As EventArgs) Handles Menu_KeyboardKey.Click
        GetSectionFromMenu(section15)
    End Sub
    Private Sub Menu_KeyAction_Click(sender As Object, e As EventArgs) Handles Menu_KeyAction.Click
        GetSectionFromMenu(section16)
    End Sub
    Private Sub Menu_Rank_Click(sender As Object, e As EventArgs) Handles Menu_Rank.Click
        GetSectionFromMenu(section17)
    End Sub
    Private Sub Menu_ExternalRank_Click(sender As Object, e As EventArgs) Handles Menu_ExternalRank.Click
        GetSectionFromMenu(section18)
    End Sub
    Private Sub Menu_ExternalPipe_Click(sender As Object, e As EventArgs) Handles Menu_ExternalPipe.Click
        GetSectionFromMenu(section19)
    End Sub
    Private Sub Menu_Stop_Click(sender As Object, e As EventArgs) Handles Menu_Stop.Click
        GetSectionFromMenu(section20)
    End Sub
    Private Sub Menu_StopRank_Click(sender As Object, e As EventArgs) Handles Menu_StopRank.Click
        GetSectionFromMenu(section21)
    End Sub
    Private Sub Menu_ReversiblePiston_Click(sender As Object, e As EventArgs) Handles Menu_ReversiblePiston.Click
        GetSectionFromMenu(section22)
    End Sub
    Private Sub Menu_Combination_Click(sender As Object, e As EventArgs) Handles Menu_Combination.Click
        GetSectionFromMenu(section23)
    End Sub
    Private Sub Menu_CombinationElement_Click(sender As Object, e As EventArgs) Handles Menu_CombinationElement.Click
        GetSectionFromMenu(section24)
    End Sub
    Private Sub Menu_ContinuousControl_Click(sender As Object, e As EventArgs) Handles Menu_ContinuousControl.Click
        GetSectionFromMenu(section25)
    End Sub
    Private Sub Menu_ContinuousControlStageSwitch_Click(sender As Object, e As EventArgs) Handles Menu_ContinuousControlStageSwitch.Click
        GetSectionFromMenu(section26)
    End Sub
    Private Sub Menu_ContinuousControlImageSetStage_Click(sender As Object, e As EventArgs) Handles Menu_ContinuousControlImageSetStage.Click
        GetSectionFromMenu(section27)
    End Sub
    Private Sub Menu_Enclosure_Click(sender As Object, e As EventArgs) Handles Menu_Enclosure.Click
        GetSectionFromMenu(section28)
    End Sub
    Private Sub Menu_EnclosurePipe_Click(sender As Object, e As EventArgs) Handles Menu_EnclosurePipe.Click
        GetSectionFromMenu(section29)
    End Sub
    Private Sub Menu_Tremulant_Click(sender As Object, e As EventArgs) Handles Menu_Tremulant.Click
        GetSectionFromMenu(section30)
    End Sub
    Private Sub Menu_TremulantWaveform_Click(sender As Object, e As EventArgs) Handles Menu_TremulantWaveform.Click
        GetSectionFromMenu(section31)
    End Sub
    Private Sub Menu_TremulantWaveformPipe_Click(sender As Object, e As EventArgs) Handles Menu_TremulantWaveformPipe.Click
        GetSectionFromMenu(section32)
    End Sub
    Private Sub Menu_ContinuousControlLinkage_Click(sender As Object, e As EventArgs) Handles Menu_ContinuousControlLinkage.Click
        GetSectionFromMenu(section33)
    End Sub
    Private Sub Menu_ContinuousControlDoubleLinkage_Click(sender As Object, e As EventArgs) Handles Menu_ContinuousControlDoubleLinkage.Click
        GetSectionFromMenu(section34)
    End Sub
    Private Sub Menu_ThreePositionSwitchImage_Click(sender As Object, e As EventArgs) Handles Menu_ThreePositionSwitchImage.Click
        GetSectionFromMenu(section35)
    End Sub
    Private Sub Menu_WindCompartment_Click(sender As Object, e As EventArgs) Handles Menu_WindCompartment.Click
        GetSectionFromMenu(section36)
    End Sub
    Private Sub Menu_WindCompartmentLinkage_Click(sender As Object, e As EventArgs) Handles Menu_WindCompartmentLinkage.Click
        GetSectionFromMenu(section37)
    End Sub
    Private Sub Menu_Sample_Click(sender As Object, e As EventArgs) Handles Menu_Sample.Click
        GetSectionFromMenu(section38)
    End Sub
    Private Sub Menu_PipeSoundEngine01_Click(sender As Object, e As EventArgs) Handles Menu_PipeSoundEngine01.Click
        GetSectionFromMenu(section39)
    End Sub
    Private Sub Menu_PipeSoundEngine01Layer_Click(sender As Object, e As EventArgs) Handles Menu_PipeSoundEngine01Layer.Click
        GetSectionFromMenu(section40)
    End Sub
    Private Sub Menu_PipeSoundEngine01AttackSample_Click(sender As Object, e As EventArgs) Handles Menu_PipeSoundEngine01AttackSample.Click
        GetSectionFromMenu(section41)
    End Sub
    Private Sub Menu_PipeSoundEngine01ReleaseSample_Click(sender As Object, e As EventArgs) Handles Menu_PipeSoundEngine01ReleaseSample.Click
        GetSectionFromMenu(section42)
    End Sub
    Private Sub Menu_RequiredInstallationPackage_Click(sender As Object, e As EventArgs) Handles Menu_RequiredInstallationPackage.Click
        GetSectionFromMenu(section43)
    End Sub

    ' MENU EDIT MODE
    Private Sub Menu_StartEditMode_Click(           ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles Menu_StartEditMode.Click

        ' MET L'ODF EN MODE EDITION AUTORISEE
        ' modif version 055 & 56

        ' Purpose:      Allows modifications to the ODF text in RTBox
        ' Process:		Enable modification of the RTBox control, adjust the Menu to reflect the
        '               state, issue a warning message re. recomputing Section details if text
        '               has been added/deleted.
        ' Called By:    Menu_StartEditMode Click Event
        ' Side Effects: Alters Menu State, sets global G_Edit_Mode
        ' Notes:        <None>

        Dim txt As String                           ' Warning text

        RTBox.ReadOnly = False                      ' Set control to allow modifications
        RTBox.BackColor = Color.White               ' Lighten the background to indicate text is editable
        Menu_StartEditMode.Checked = True           ' Check Menu item to indicate it is enabled
        Menu_ExitEditMode.Checked = False           ' Uncheck its obverse
        Menu_EDITMODE.BackColor = Color.Red         ' Paint Main Menu text with RED background
        G_EditMode = True                           ' indique edition en cours (modif 055); save State

        txt = "WARNING" & vbCrLf                    ' warning; construct warning text, then display it
        txt += "If you add or remove characters from a Section, changing its length, "
        txt += "you must re-compute the parameters of the 44 Sections. "
        txt += "So, please, once you have finished editing a Section, "
        txt += "click on the menu ""Re-compute the Section"" before editing another Section. "
        txt += "Thanks!" & vbCrLf & vbCrLf
        txt += "Notice that the cursor will allways be at the begining of the Row you click on, move it with the arrows to the place you want to write."

        MsgBox(txt, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Entering EDIT MODE")

    End Sub
    Private Sub Menu_ExitEditMode_Click(            ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles Menu_ExitEditMode.Click

        ' REMET L'ODF EN MODE EDITION INTERDITE

        ' Purpose:      Exit Edit Mode - solicit recomputation of the Section data (S_Section array)
        ' Process:		Disable editing in the ODF, revert background color to the standard Light Gray,
        '               adjuest the EDIT-MODE menus to reflect status, ask about recompute, recompute
        '               Section data on request.
        ' Called By:    Menu_ExitEditMode Click Event
        ' Side Effects: Alters Menu state, adjusts G_EditMode and G_LastSectionName
        ' Notes:        We can definitively know if recomputation is required, avoid asking when it is
        '               not needed.

        Dim rep As Integer                          ' Response from warning MsgBox: recompute Sections or not?
        Dim txt As String                           ' Warning Text

        RTBox.ReadOnly = True                       ' Return ODF to non-editable
        RTBox.BackColor = Color.WhiteSmoke          ' Reset to slightly darker background
        Menu_StartEditMode.Checked = False          ' Uncheck "In Edit Mode"
        Menu_ExitEditMode.Checked = True            ' Check "Exit Edit Mode"
        Menu_EDITMODE.BackColor =
            Color.LightSteelBlue                    ' Reset Main Menu text background color
        G_EditMode = False                          ' indique retour au mode lecture seule (modif v055); save State
        G_LastSectionName = ""                      ' (modif v056); Last Section undefined

        txt = "WARNING" & vbCrLf                    ' warning; construct warning text, then display it
        txt += "If you add or remove characters from a Section, changing its length, "
        txt += "you must re-compute the parameters of the 44 Sections." & vbCrLf
        txt += "Do you want to re-compute the Sections?"

        rep = MsgBox(txt, MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Exiting EDIT MODE")
        If rep = vbYes Then GetSectionsInfos(conVerbose)

    End Sub
    Private Sub Menu_ReComputeSections_Click(       ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles Menu_ReComputeSections.Click

        ' Purpose:      Force recompute on-demand
        ' Process:		Call Recompute routine, enabling verbose-mode (display Section data as it is computed)
        ' Called By:    Menu_RecomputeSections Click Event
        ' Side Effects: NA
        ' Notes:        <None>

        GetSectionsInfos(conVerbose)                ' Call standard routine to recompute Sections, in verbose-mode

    End Sub

    ' MENU TOOLS
    Private Sub Menu_ClearMarkers_Click(            ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles Menu_ClearMarkers.Click

        ' Purpose:      Erase any stored Marker info, reset to default state
        ' Process:		Set to initial text content and color
        ' Called By:    Menu_ClearMarkers Click Event
        ' Side Effects: Updates Marker controls on Main form
        ' Notes:        Consider making this a Collection, traverse entire collection - makes it
        '               easy to add additional Markers without chaging this code.

        ButtonMarker1.Text = "Marker 1" : ButtonMarker1.BackColor = Color.Gainsboro
        ButtonMarker2.Text = "Marker 2" : ButtonMarker2.BackColor = Color.Gainsboro
        ButtonMarker3.Text = "Marker 3" : ButtonMarker3.BackColor = Color.Gainsboro
        ButtonMarker4.Text = "Marker 4" : ButtonMarker4.BackColor = Color.Gainsboro

    End Sub
    Private Sub CouplersCodeToolStripMenuItem_Click(    ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles Menu_CouplersCode.Click

        ' AFFICHE LA FENETRE COUPLERS

        ' Purpose:      Display the Couplers form in its own Window
        ' Process:		Show it
        ' Called By:    Menu_CouplersCOde Click Event
        ' Side Effects: Displays a Couplers Form
        ' Notes:        <None>

        Couplers.Visible = False
        Couplers.Show(Me)                               ' Open the form, if already open, give it focus

    End Sub
    Private Sub FollowASampleToolStripMenuItem_Click(   ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles Menu_FollowASample.Click

        ' AFFICHE LA FENETRE FOLLOW

        ' Purpose:      Display the FollowSample form in its own Window
        ' Process:		Force Section Type to Follow to "Sample", show form
        ' Called By:    Menu_FollowASample Click Event
        ' Side Effects: Updates G_ITem_to_Follow, Displays Follow A Sample form
        ' Notes:        <None>

        G_Item_to_Follow = "<ObjectList ObjectType=""Sample"">"
        Follow.Visible = False
        Follow.Show(Me)                                 ' Open the form, if already open, give it focus

    End Sub

    ' MENU ?
    Private Sub Menu_Help_Click(                ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles Menu_Help.Click

        ' Purpose:      Display the helptest file in the Section Description box
        ' Process:		Construct path to file, check if it exists, if so, copy its content into
        '               the box.
        ' Called By:    Menu_Help Click Event
        ' Side Effects: Updates G_RTFFile, updates RTBoxRTF control contents
        ' Notes:        Consider hardening for file access exceptions, and/or changing to a standard Help object.

        G_RTFFile = G_DataPath & "\help.rtf"            ' Build the path
        If My.Computer.FileSystem.FileExists(           ' Does it exist?
            G_RTFFile) Then
            RTBoxRTF.LoadFile(G_RTFFile)                ' Yes, copy its contents into display box
        End If

    End Sub

    ' BOUTONS
    Private Sub FindButton_Click(               ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles FindButton.Click

        ' Purpose:      Initiate a text search in the ODF, starting from the beginning
        ' Process:		Set search position to the beginning, search forward
        ' Called By:    FindBUtton Click Event
        ' Side Effects: Alters G_FindStartPosition
        ' Notes:        <None>

        G_FindStartPosition = 1                 ' Start search from beginning of ODF
        FindButtonProcedure()                   ' Execute search

    End Sub
    Private Sub FindNextButton_Click(           ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles FindNextButton.Click

        ' Purpose:      Continue a text search in the ODF, starting from the current position
        ' Process:		Leave search position as-is, search forward
        ' Called By:    FindNExtButton Click Event
        ' Side Effects: Alters G_FindStartPosition
        ' Notes:        <None>

        If G_FindStartPosition < 0 Then         ' Make sure we are in the ODF
            G_FindStartPosition = 1
        End If

        FindButtonProcedure()                   ' Search from present position

    End Sub
    Private Sub FindButtonProcedure()                   ' CODE COMMUN A FIND ET FIND NEXT

        ' Purpose:      Do a forward search for text, position to found text, highlight it, update
        '               position info.
        ' Process:		Search, if found, locate Section, then extract and parse Child Element Row
        ' Called By:    Findbutton_(); FindNextButton_Click()
        ' Side Effects: Updates globals, form fields, position variables
        ' Notes:        Should G_LastSectionName be updated if we are still in the same Section? Algorithm
        '               for determining Section is faulty, especially if we don't land in a Child Element
        '               Row.

        Dim startPos As Integer                         ' Index returned by Search
        Dim textLength As Integer                       ' Length of Search Text

        G_TextToFind = TextToFindBox.Text               ' Retrieve search text from form control
        If G_TextToFind = "" Then Return                ' Null search, just return
        textLength = G_TextToFind.Length                ' Length of search text

        startPos = FindMyText(G_TextToFind,
                              G_FindStartPosition)      ' Search forward
        If startPos <= 0 Then                           ' Did not find the text
            MsgBox("Not Found")
            Return
        End If


        ClearTagsPanel()                                ' vide le panneau des tags; clear the Child Elements panel
        ' MsgBox("Section : " & G_SectionName & vbCrLf & "Last section : " & G_LastSectionName)
        G_LastSectionName = G_SectionName               ' Save old Section Name
        'G_SectionName = ""
        RTBoxLine.Text = ""                             ' Clear Row Text
        GetSectionFromIndex(startPos)                   ' Locate the Section we are now in
        GetLineFromIndex(startPos)                      ' Fetch the Row
        G_FindStartPosition = startPos + 1              ' Leave position after end of found text, for Find Next
        LabelCaretPos.Text = startPos                   ' Move the Cursor to the found text
        RTBox.Focus()
        RTBox.SelectionStart = startPos                 ' Select and Highlight in the ODF the located text
        RTBox.SelectionLength = textLength

    End Sub
    Private Sub ButtonLed_Click(                ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles ButtonLed.Click

        ' Purpose:      Force a silent recalculation of the Section data
        ' Process:		Calls recalc routine in non-verbose mode
        ' Called By:    ButtonLed Click Event
        ' Side Effects: NA
        ' Notes:        <None>

        GetSectionsInfos(conSilent)             ' Recalculate the Section data silently

    End Sub
    Private Sub ButtonFont_Click(               ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles ButtonFont.Click

        ' FORCE LA POLICE DE LA BOX

        ' Purpose:      Force Descriptive text box to 10-point Verdana
        ' Process:		Select the control, set the font properties
        ' Called By:    ButtonFont Click Event
        ' Side Effects: Alters RTBoxRTF content
        ' Notes:        <None>

        Dim fnt As New System.Drawing.Font("verdana", 10)

        RTBoxRTF.Focus()
        RTBoxRTF.Font = fnt                     ' Font for future text typed into the control
        RTBoxRTF.SelectAll()
        RTBoxRTF.SelectionFont = fnt            ' Change all existing content
        RTBoxRTF.DeselectAll()
        RTBoxRTF.Refresh()                      ' Update screen

    End Sub
    Private Sub ButtonMarkers_MouseDown(        ' Standard Control event parms...
            sender As Object,
            e As MouseEventArgs
            ) Handles ButtonMarker1.MouseDown, ButtonMarker2.MouseDown, ButtonMarker3.MouseDown, ButtonMarker4.MouseDown

        ' MEMORISE OU RETROUVE UNE LIGNE

        ' Purpose:      Process right click to set a marker, left click to position to a marker
        ' Process:		To set, copy the Cursor position into the Button's text, and color the control.
        '               To follow, retrieve text from control, move Cursor to that location in the ODF.
        ' Called By:    ButtonMarker1/2/3/4 MouseDown Events
        ' Side Effects: Can alter Button's text and color; can alter Cursor position in ODF.
        ' Notes:        <None>

        Dim bouton As String = e.Button.ToString    ' "Left" or "Right", tells us which button
        Dim marker As Control = sender              ' Superfluous (just use [sender]); Control object that raised the event
        Dim caret As Integer                        ' Position to move to, retrieved from a Marker

        If bouton = "Right" Then
            marker.Text = RTBox.GetFirstCharIndexOfCurrentLine.ToString
            marker.BackColor = Color.LightCyan
        End If

        If bouton = "Left" Then
            caret = Val(marker.Text)
            RTBox.Focus()
            RTBox.Select(caret, 0)
            RTBox.ScrollToCaret()
        End If

    End Sub
    Private Sub ButtonDisplayImage_Click(       ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles ButtonDisplayImage.Click

        ' Purpose:      When the current Row includes a Child Element that names an Image File (Mask,
        '               Imgage), attempt to retrieve and display that file.
        ' Process:		Dispatch the processing routine
        ' Called By:    ButtonDisplayImage Click Event
        ' Side Effects: NA
        ' Notes:        <None>

        DisplayImage()                          ' Subroutine does the work

    End Sub
    Private Sub ButtonDisplayImage_LostFocus(   ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles ButtonDisplayImage.LostFocus

        ' Purpose:      Remove the image and revert panel to Row Child-Element display format
        ' Process:		Hide the image (PBox), reduce the panel to its standard size
        ' Called By:    ButtonDisplayImage LostFocus Event
        ' Side Effects: Update the panel object
        ' Notes:        Panel size values should be top-level constants

        PBox.Visible = False                        ' Hide the image display box
        PBox.Image = Nothing                        ' Clear the image
        PBox.BorderStyle = BorderStyle.None         ' Remove the borders

        Dim psize = New System.Drawing.Size With {  ' retrecir le panel; resize the panel
            .Height = 330,
            .Width = 600
        }
        PanelTags.Size = psize                      ' Resize the panel to its normal dimensions

    End Sub
    Private Sub ButtonNextLine_Click(                   ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles ButtonNextLine.Click

        ' RECHERCHE LA PROCHAINE LIGNE

        ' Purpose:      Advance position in the ODF by 1 line
        ' Process:		Advance Line, locate first character in line, place Cursor on first character,
        '               locate containing Section, extract, parse, and display Child Elements of new
        '               Row, scroll main ODF display to the new Position.
        ' Called By:    ButtonNextLine Click Event
        ' Side Effects: Update position globals, update fields on Main form
        ' Notes:        Some glitches in advancing through Section boundaries.

        Dim newPos As Integer                           ' Updated Cursor position

        Try
            G_LineIndex += 1                            ' Advance line position by 1 line
            newPos = RTBox.GetFirstCharIndexFromLine(   ' Move Cursor to first character of the new line
                G_LineIndex)
            G_CaretPos = newPos                         ' newPos was set to actual position at beginning of a line
            LabelCaretPos.Text = newPos                 ' Update cursor position text field
            LabelLineNumber.Text = G_LineIndex          ' Update line number text field
            LabelLineStart.Text = ""                    ' Clear Line Start/End positions on screen; NextLineCommun will cause them to be filled in
            LabelLineEnd.Text = ""
            NextLineCommun()                            ' Update Section, extract and display Row, parse Row Elements
            RTBox.Focus()
            RTBox.Select(newPos, 0)
            RTBox.ScrollToCaret()                       ' Position cursor to beginning of the new line, and make that the top line displayed on the RT Box (ODF display)
        Catch                                           ' Catch exceptions here, and ignore
            ' ras
        End Try

    End Sub
    Private Sub ButtonNext10Lines_Click(                ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles ButtonNext10Lines.Click

        ' RECHERCHE LA 10eme PROCHAINE LIGNE

        ' Purpose:      Advance position in the ODF by 10 lines
        ' Process:		Advance Line by 10, locate first character in line, place Cursor on first character,
        '               locate containing Section, extract, parse, and display Child Elements of new
        '               Row, scroll main ODF display to the new Position.
        ' Called By:    ButtonNext10Lines Click Event
        ' Side Effects: Update position globals, update fields on Main form
        ' Notes:        Some glitches in advancing through Section boundaries.

        Dim newPos As Integer                           ' Updated Cursor position

        Try
            G_LineIndex += 10                           ' Advance line position by 10 lines
            newPos = RTBox.GetFirstCharIndexFromLine(
                G_LineIndex)
            G_CaretPos = newPos                         ' newPos was set to actual position at beginning of a line (may not be 10 lines to skip)
            LabelCaretPos.Text = newPos                 ' Update cursor position text field
            LabelLineNumber.Text = G_LineIndex          ' Update line number text field
            LabelLineStart.Text = ""                    ' Clear Line Start/End positions on screen; NextLineCommun will cause them to be filled in
            LabelLineEnd.Text = ""
            NextLineCommun()                            ' Update Section, extract and display Row, parse Row Elements
            RTBox.Focus()
            RTBox.Select(newPos, 0)
            RTBox.ScrollToCaret()                       ' Position cursor to beginning of the new line, and make that the top line displayed on the RT Box (ODF display)
        Catch                                           ' Catch exceptions here, and ignore
            ' ras
        End Try

    End Sub
    Private Sub ButtonNext100Lines_Click(       ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles ButtonNext100Lines.Click

        ' RECHERCHE LA 100eme PROCHAINE LIGNE; advance the ODF window 100 lines

        ' Purpose:      Advance position in the ODF by 100 lines
        ' Process:		Advance Line by 100, locate first character in line, place Cursor on first character,
        '               locate containing Section, extract, parse, and display Child Elements of new
        '               Row, scroll main ODF display to the new Position.
        ' Called By:    ButtonNext100Lines Click Event
        ' Side Effects: Update position globals, update fields on Main form
        ' Notes:        Some glitches in advancing through Section boundaries.

        Dim newPos As Integer                               ' Updated Cursor position

        Try
            G_LineIndex += 100                              ' Skip forward 100 lines
            newPos = RTBox.GetFirstCharIndexFromLine(
                G_LineIndex)
            G_CaretPos = newPos                             ' newPos was set to actual position at beginning of a line (may not be 100 lines to skip)
            LabelCaretPos.Text = newPos                     ' Update cursor position text field
            LabelLineNumber.Text = G_LineIndex              ' Update line number text field
            LabelLineStart.Text = ""                        ' Clear Line Start/End positions on screen; NextLineCommun will cause them to be filled in
            LabelLineEnd.Text = ""
            NextLineCommun()                                ' Update Section, extract and display Row, parse Row Elements
            RTBox.Focus()
            RTBox.Select(newPos, 0)
            RTBox.ScrollToCaret()                           ' Position cursor to beginning of the new line, and make that the top line displayed on the RT Box (ODF display)
        Catch                                               ' Catch exceptions here, and ignore
            '                                                 ras
        End Try

    End Sub

    ' IMAGES
    Private Sub DisplayImage()                          ' AFFICHE L'IMAGE (OU LE MASK)

        ' Purpose:      Retrieve and display an image file. When parsing a Row that contains an Element for an image file
        '               (found in the ImageSet (masks) and ImageSetElement (images) Sections), the Image File and other
        '               info will have already been extracted by the parser, and placed in global variables.
        ' Process:		An ImageSet Section Row will define the PackageID (needed to locate the source instrument), and the
        '               filename (Mask file); An ImageSetElement Section Row will contain the ImgageSetID, and a filename - so
        '               the owning ImageSet has to be located, to get the PackageID. The PackageID is found, regardless of
        '               which Section we are in (by a call to FIndImagePackageID(ImageSet)). This is displayed, and a fully
        '               qualified file path is built. The panel is extended all the way to the right to make room for the
        '               image display, then the image is loaded into a bitmap, and displayed.
        ' Called By:    ButtonDisplayImage_Click
        ' Side Effects: Alters image file globals, panel size, and paints image onscreen.
        ' Notes:        Presently limited to bitmap files (.bmp). Need to also process .png, possibly .jpg. Resize variables should
        '               be defined at the top level. Improve exception handling.

        Dim url As String                               ' Full path\filename
        Dim image1 As Bitmap                            ' Bucket to hold image

        FindImagePackageID(G_ImageSet)                  ' identifie d'abord le package; Populate G_PackageID from the ImageSet with ImageSetID = G_ImageSet
        LabelPackageID.Text = "PkgID = " & G_PackageID  ' Update PackageID on the Panel-Header of the Main form i.e. internal ID of the organ supplying the image
        url = G_PackagePath & G_PackageID & G_ImageFile ' Build url to be entire path\filename of the image to be displayed
        Dim psize = New System.Drawing.Size With {      ' agrandir le panel; Widen the Tags Panel to make more room for the imgage, covering the RTF explanatory text portion of the form
            .Height = 330,
            .Width = 1150
        }
        PanelTags.Size = psize

        Try                                             ' Retrieve the image, use Try to field exception when attempting to open the file
            image1 = New Bitmap(url, True)              ' Load the picture into a Bitmap. Note that this only works for .bmp image files: a .png file will fail, raising an exception
            PBox.BorderStyle = BorderStyle.FixedSingle  ' Display the results.
            PBox.Image = image1                         ' Put the bitmap into the image display box
            PBox.SizeMode = PictureBoxSizeMode.AutoSize
            PBox.Visible = True                         ' Display it

        Catch ex As ArgumentException                   ' Couldn't retrieve the requested data - could be missing file, or a file that isn't a bitmap (.bmp)
            MessageBox.Show("Sorry. Cannot display the image.")
            MsgBox(url, , "Message from Display_Image function")
        End Try

    End Sub
    Private Sub FindImagePackageID(imgSet As String)            ' CHERCHE LE PACKAGEID D'UNE IMAGE D'IMAGE SET ELEMENT

        ' Purpose:      Populate G_PackageID for the ImageSet where ImageSetID=[imgSet]
        ' Process:		Search for ImageSet <a> Start-Tag: if in the ImageSet Section, this will fail, which is OK.
        '               In ImageSetElement Section, this will return the ID of the ImageSet, which in turn
        '               contains the PackageID we need. Find that ID, pad with leading zeroes, build version
        '               useful as a file path.
        ' Called By:    DisplayImage()
        ' Side Effects: Alter global imgage variables
        ' Notes:        Fix logic errors.

        Dim sectidx As Integer = 5                              ' Index for Section "Image Set"
        Dim startPos As Integer = S_Section(sectidx).startPos
        Dim endPos As Integer = S_Section(sectidx).endPos
        Dim tagStartA As Integer
        Dim txtA As String = "<a>" & imgSet & "</a>"            ' The 'a' tag delimits the ImageSetID, this is the search field
        Dim tagStartC As Integer
        Dim tagEndC As Integer
        Dim txtC As String = "<c>"                              ' The '<c>' tag Delimits the ImagePackageID, the data we want

        ' Dim msg As String
        ' msg = "Search in section " & S_Section(sectidx).name & vbCrLf
        ' msg += "From " & startPos.ToString & " to " & endPos.ToString & vbCrLf

        tagStartA = RTBox.Find(txtA, startPos, RichTextBoxFinds.None)
        If tagStartA < 0 Then Return                            ' RECHERCHE DU TAG <a>; Look for tag '<a>' - if not found, return
        startPos = tagStartA + Len(txtA)                        ' Recherche du tag <c>; We found the row with ImageSetID = imgSet, skip past ID text

        tagStartC = RTBox.Find(txtC, startPos, RichTextBoxFinds.None)
        If tagStartC < 0 Then Return                            ' Never found the PackageID tag, return empty-handed...
        startPos = tagStartC + 3                                ' Skip past the opening '<c>' tag
        tagEndC = RTBox.Find("</c>", startPos, RichTextBoxFinds.None)

        RTBox.SelectionStart = tagStartC + 3                    ' lire le tag <c> = packageID; Select the text between '<c>' & '</c': this is the PackageID
        RTBox.SelectionLength = tagEndC - (tagStartC + 3)
        G_PackageID = RTBox.SelectedText

        Do While Len(G_PackageID) < 6                           ' Pad with leading zeroes until string is exactly 6 digits long
            G_PackageID = "0" & G_PackageID
        Loop
        G_PackageID += "\"                                      ' Add backslash suffix, as part of a filesystem directory name

        ' msg += G_PackageID
        ' MsgBox("FindImagePackageID, looking for imgSet = " & imgSet & ", G_PackageID is " & msg)

    End Sub
    Private Sub PBox_Click(                         ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles PBox.Click

        ' Purpose:      Clicking on an image display (PBox) clears the image, hides the box, resets the Tags Panel to its default size
        ' Process:		Set the needed properties, adjust the size back to normal.
        ' Called By:    PBox Click Event
        ' Side Effects: Alters properties of panel
        ' Notes:        Elevate size variables to top-level constants.

        PBox.Visible = False                        ' Hide the control displaying the image
        PBox.Image = Nothing                        ' Clear the underlying image from the control
        PBox.BorderStyle = BorderStyle.None         ' Remove the borders

        Dim psize = New System.Drawing.Size With {  ' retrecir le panel; resize the Tags Panel
            .Height = 330,
            .Width = 600
        }
        PanelTags.Size = psize

    End Sub

    Private Sub RTBox_TextChanged(                  ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles RTBox.TextChanged

        ' Purpose:      Event triggers whenever text of main (ODF) RTBox is changed: loaded, cleared, edited by user
        ' Process:		If length changed, set length global to new length
        ' Called By:    RTBox TextChanged Event
        ' Side Effects: Set ODF Length global to new size.
        ' Notes:        Looks like original idea was to auto-recompute sections if text changed, which
        '               would take too much time with current algorithm.

        If RTBox.TextLength <> G_ODFLength Then     ' FAUT-IL RECALCULER LES SECTIONS ?; Recalculate Section data? No, not here, unless using an efficient algorithm
            'MsgBox("RTBox Length changed, is now " & RTBox.TextLength.ToString)
            G_ODFLength = RTBox.TextLength          ' Update current ODF (text) length
            'get_Sections_Infos(False)
        End If

    End Sub

    '                   DOC RICH TEXT BOX (useful Methods/Properties)

    'AppendText 	                                Ajoute du texte au texte en cours dans une zone de texte. (Hérité de TextBoxBase.)
    'CanPaste 	                                    Détermine si vous pouvez coller des informations du Presse-papiers dans le format de données spécifié.
    'Clear 	                                        Efface tout le texte du contrôle zone de texte. (Hérité de TextBoxBase.)
    'ClearUndo 	                                    Efface les informations sur la dernière opération effectuée à partir de la mémoire tampon d'annulation de la zone de texte. (Hérité de TextBoxBase.)
    'Copy 	                                        Copie la sélection actuelle dans la zone de texte vers le Presse-papiers. (Hérité de TextBoxBase.)
    'Cut 	                                        Déplace la sélection actuelle entre la zone de texte et le Presse-papiers. (Hérité de TextBoxBase.)
    'DeselectAll 	                                Spécifie que la valeur de la propriété SelectionLength est zéro afin qu'aucun caractère ne soit sélectionné dans le contrôle. (Hérité de TextBoxBase.)
    'Find(Char()) 	                                Recherche dans le texte d'un contrôle RichTextBox la première occurrence d'un caractère issu d'une liste.
    'Find(String) 	                                Recherche une chaîne donnée dans le texte d'un contrôle RichTextBox.
    'Find(Char(), Int32) 	                        Recherche dans le texte d'un contrôle RichTextBox, à partir d'un point spécifique, la première occurrence d'un caractère parmi une liste de caractères.
    'Find(String, RichTextBoxFinds) 	            Recherche une chaîne dans le texte d'un contrôle RichTextBox en appliquant des options de recherche spécifiques.
    'Find(Char(), Int32, Int32) 	                Recherche dans une plage de texte d'un contrôle RichTextBox la première occurrence d'un caractère issu d'une liste.
    'Find(String, Int32, RichTextBoxFinds) 	        Recherche une chaîne à un emplacement spécifique dans le texte d'un contrôle RichTextBox en appliquant des options de recherche spécifiques.
    'Find(String, Int32, Int32, RichTextBoxFinds)   Recherche une chaîne dans une plage de texte d'un contrôle RichTextBox en appliquant des options de recherche spécifiques. 
    'GetCharFromPosition 	                        Récupère le caractère le plus proche de l'emplacement spécifié dans le contrôle. (Hérité de TextBoxBase.)
    'GetCharIndexFromPosition 	                    Récupère l'index du caractère le plus proche de l'emplacement spécifié par un System.Drawning.Point.
    'GetFirstCharIndexFromLine 	                    Récupère l'index du premier caractère d'une ligne donnée. (Hérité de TextBoxBase.)
    'GetFirstCharIndexOfCurrentLine 	            Récupère l'index du premier caractère de la ligne active. (Hérité de TextBoxBase.)
    'GetLineFromCharIndex 	                        Récupère le numéro de ligne à partir de la position de caractère spécifiée dans le texte du contrôle RichTextBox. (Substitue TextBoxBase.GetLineFromCharIndex(Int32).)
    'GetPositionFromCharIndex 	                    Récupère l'emplacement de l'index de caractère spécifié dans le contrôle. (Substitue TextBoxBase.GetPositionFromCharIndex(Int32).)
    'ResetFont 	                                    Rétablit la valeur par défaut de la propriété Font. (Hérité de Control.)
    'ResetForeColor 	                            Rétablit la valeur par défaut de la propriété ForeColor. (Hérité de Control.)
    'ScrollToCaret 	                                Fait défiler le contenu du contrôle vers la position indiquée par le signe insertion. (Hérité de TextBoxBase.)
    'Select(Int32, Int32) 	                        Sélectionne une plage de texte dans la zone de texte.(start position, longueur selection)
    'SelectAll 	                                    Sélectionne tout le texte de la zone de texte. (Hérité de TextBoxBase.)
    'Undo 	                                        Annule la dernière modification apportée dans la zone de texte. (Hérité de TextBoxBase.)
    'Update 	                                    Force le contrôle à redessiner les zones invalidées dans sa zone cliente. (Hérité de Control.)
    'UpdateStyles 	                                Force la réapplication au contrôle des styles assignés. (Hérité de Control.)

    Private Sub AboutToolStripMenuItem_Click(       ' Standard Control event parms...
            sender As Object,
            e As EventArgs
            ) Handles AboutToolStripMenuItem.Click

        ' Purpose:      Display the About Box
        ' Process:		Just use form Show method
        ' Called By:    AboutToolStripMenuItem Click Event
        ' Side Effects: Displays form in new Window
        ' Notes:        Replaced original MsgBox code.
        '                                                             <1.059.0> Replaced MsgBox with VS embedded "AboutBox" control.
        '                                                             This control takes its content from the Project's Assemby Parameters, maintained by the IDE.
        '                                                             Commented-out code below is the original, based on MsgBox

        'Dim txt As String
        ' txt = "AECHO " & My.Application.Info.Version.ToString & vbCrLf
        ' txt += "Freeware version" & vbCrLf
        'txt += "© 2021 - Jean-Paul Verpeaux" & vbCrLf
        'txt += "If you realise something interesting about AECHO, please share your work by sending me information." & vbCrLf
        'txt += "MUSICALIS@NEUF.FR"
        'MsgBox(txt, 0, "About Aecho")

        AboutBox1.Show(Me)                                          ' Display the form

    End Sub
End Class
