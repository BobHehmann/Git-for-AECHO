﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MAIN
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MAIN))
        Me.Menu_Strip = New System.Windows.Forms.MenuStrip()
        Me.Menu_File = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_OpenHauptwerkOrgan = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Recent = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_CloseODF = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_SaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Sep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.Menu_PrintDT = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Sep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.Menu_Quit = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_SectionsA = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_EditMode = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_EditModeStart = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_EditModeExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_ReComputeSections = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Tools = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_ClearMarkers = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_CouplersCode = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_FollowASample = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_ListSections = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_HelpAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Help = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_About = New System.Windows.Forms.ToolStripMenuItem()
        Me.Rtb_ODF = New System.Windows.Forms.RichTextBox()
        Me.CM_ODF = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CM_ODFUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.CM_ODFRedo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CM_ODFCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.CM_ODFCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.CM_ODFPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.CM_ODFEditMode = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.CM_ODFRecompute = New System.Windows.Forms.ToolStripMenuItem()
        Me.CM_XML = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CM_XMLCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.Num_ODFFontSize = New System.Windows.Forms.NumericUpDown()
        Me.Lbl_SecStartTitle = New System.Windows.Forms.Label()
        Me.Lbl_SecEndTitle = New System.Windows.Forms.Label()
        Me.Lbl_RowStartTitle = New System.Windows.Forms.Label()
        Me.Lbl_SecStartVal = New System.Windows.Forms.Label()
        Me.Lbl_SecEndVal = New System.Windows.Forms.Label()
        Me.Lbl_RowStartVal = New System.Windows.Forms.Label()
        Me.Lbl_CursorPosTitle = New System.Windows.Forms.Label()
        Me.Lbl_CursorPosVal = New System.Windows.Forms.Label()
        Me.Lbl_RowEndTitle = New System.Windows.Forms.Label()
        Me.Lbl_RowEndVal = New System.Windows.Forms.Label()
        Me.Lbl_SectionTitle = New System.Windows.Forms.Label()
        Me.Lbl_SectionName = New System.Windows.Forms.Label()
        Me.Lbl_NumTagsTitle = New System.Windows.Forms.Label()
        Me.Lbl_NumTagsVal = New System.Windows.Forms.Label()
        Me.Rtb_DescText = New System.Windows.Forms.RichTextBox()
        Me.CM_Desc = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CM_DescUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.CM_DescRedo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.CM_DescCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.CM_DescCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.CM_DescPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CM_DescPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.CM_DescFont = New System.Windows.Forms.ToolStripMenuItem()
        Me.Rtb_XMLRow = New System.Windows.Forms.RichTextBox()
        Me.Btn_SaveDescText = New System.Windows.Forms.Button()
        Me.Pnl_Tags = New System.Windows.Forms.Panel()
        Me.Btn_RowAction = New System.Windows.Forms.Button()
        Me.tag24 = New System.Windows.Forms.Label()
        Me.LabelTag24 = New System.Windows.Forms.Label()
        Me.tag23 = New System.Windows.Forms.Label()
        Me.LabelTag23 = New System.Windows.Forms.Label()
        Me.tag22 = New System.Windows.Forms.Label()
        Me.LabelTag22 = New System.Windows.Forms.Label()
        Me.tag21 = New System.Windows.Forms.Label()
        Me.LabelTag21 = New System.Windows.Forms.Label()
        Me.tag20 = New System.Windows.Forms.Label()
        Me.LabelTag20 = New System.Windows.Forms.Label()
        Me.tag19 = New System.Windows.Forms.Label()
        Me.LabelTag19 = New System.Windows.Forms.Label()
        Me.tag18 = New System.Windows.Forms.Label()
        Me.LabelTag18 = New System.Windows.Forms.Label()
        Me.tag17 = New System.Windows.Forms.Label()
        Me.LabelTag17 = New System.Windows.Forms.Label()
        Me.tag16 = New System.Windows.Forms.Label()
        Me.LabelTag16 = New System.Windows.Forms.Label()
        Me.tag15 = New System.Windows.Forms.Label()
        Me.tag14 = New System.Windows.Forms.Label()
        Me.LabelTag15 = New System.Windows.Forms.Label()
        Me.LabelTag14 = New System.Windows.Forms.Label()
        Me.tag13 = New System.Windows.Forms.Label()
        Me.LabelTag13 = New System.Windows.Forms.Label()
        Me.tag12 = New System.Windows.Forms.Label()
        Me.LabelTag12 = New System.Windows.Forms.Label()
        Me.tag11 = New System.Windows.Forms.Label()
        Me.LabelTag11 = New System.Windows.Forms.Label()
        Me.tag10 = New System.Windows.Forms.Label()
        Me.LabelTag10 = New System.Windows.Forms.Label()
        Me.tag9 = New System.Windows.Forms.Label()
        Me.LabelTag9 = New System.Windows.Forms.Label()
        Me.tag8 = New System.Windows.Forms.Label()
        Me.LabelTag8 = New System.Windows.Forms.Label()
        Me.tag7 = New System.Windows.Forms.Label()
        Me.LabelTag7 = New System.Windows.Forms.Label()
        Me.tag6 = New System.Windows.Forms.Label()
        Me.tag5 = New System.Windows.Forms.Label()
        Me.LabelTag6 = New System.Windows.Forms.Label()
        Me.LabelTag5 = New System.Windows.Forms.Label()
        Me.tag4 = New System.Windows.Forms.Label()
        Me.LabelTag4 = New System.Windows.Forms.Label()
        Me.tag3 = New System.Windows.Forms.Label()
        Me.LabelTag3 = New System.Windows.Forms.Label()
        Me.tag2 = New System.Windows.Forms.Label()
        Me.LabelTag2 = New System.Windows.Forms.Label()
        Me.tag1 = New System.Windows.Forms.Label()
        Me.LabelTag1 = New System.Windows.Forms.Label()
        Me.Lbl_LineNum = New System.Windows.Forms.Label()
        Me.Lbl_LineNumVal = New System.Windows.Forms.Label()
        Me.Lbl_FindTitle = New System.Windows.Forms.Label()
        Me.Pnl_Find = New System.Windows.Forms.Panel()
        Me.Btn_FindPrev = New System.Windows.Forms.Button()
        Me.Btn_FindNext = New System.Windows.Forms.Button()
        Me.Btn_FindFirst = New System.Windows.Forms.Button()
        Me.Txt_SearchText = New System.Windows.Forms.TextBox()
        Me.CM_Search = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CM_SearchUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CM_SearchCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.CM_SearchCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.CM_SearchPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.Btn_Led = New System.Windows.Forms.Button()
        Me.Btn_SetFont = New System.Windows.Forms.Button()
        Me.Btn_Marker1 = New System.Windows.Forms.Button()
        Me.Btn_Marker2 = New System.Windows.Forms.Button()
        Me.Btn_Marker3 = New System.Windows.Forms.Button()
        Me.Btn_Marker4 = New System.Windows.Forms.Button()
        Me.Pnl_Data = New System.Windows.Forms.Panel()
        Me.Lbl_LineEndVal = New System.Windows.Forms.Label()
        Me.Lbl_LineStartVal = New System.Windows.Forms.Label()
        Me.Lbl_LineEndTitle = New System.Windows.Forms.Label()
        Me.Lbl_LineStartTitle = New System.Windows.Forms.Label()
        Me.Lbl_DataPanelTitle = New System.Windows.Forms.Label()
        Me.Btn_NextLine = New System.Windows.Forms.Button()
        Me.Btn_Next10Lines = New System.Windows.Forms.Button()
        Me.Btn_Next100Lines = New System.Windows.Forms.Button()
        Me.Lbl_TagPanelTitle = New System.Windows.Forms.Label()
        Me.Lbl_TextBoxTitle1 = New System.Windows.Forms.Label()
        Me.Lbl_ImageTitle = New System.Windows.Forms.Label()
        Me.Status_Strip1 = New System.Windows.Forms.StatusStrip()
        Me.Status_RowTypeTitle = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Status_RowTypeVal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Status_FileDirtyTitle = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Status_FileDirtyVal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Status_LinesTitle = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Status_LinesVal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Status_CharsTitle = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Status_CharsVal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Lbl_XMLRowTitle = New System.Windows.Forms.Label()
        Me.Lbl_FontSize = New System.Windows.Forms.Label()
        Me.Lbl_ODFTitle = New System.Windows.Forms.Label()
        Me.Lbl_TextBoxTitle2 = New System.Windows.Forms.Label()
        Me.Btn_PrevLine = New System.Windows.Forms.Button()
        Me.Btn_Prev10Lines = New System.Windows.Forms.Button()
        Me.Btn_Prev100Lines = New System.Windows.Forms.Button()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.FntDial_Desc = New System.Windows.Forms.FontDialog()
        Me.Menu_Strip.SuspendLayout()
        Me.CM_ODF.SuspendLayout()
        Me.CM_XML.SuspendLayout()
        CType(Me.Num_ODFFontSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CM_Desc.SuspendLayout()
        Me.Pnl_Tags.SuspendLayout()
        Me.Pnl_Find.SuspendLayout()
        Me.CM_Search.SuspendLayout()
        Me.Pnl_Data.SuspendLayout()
        Me.Status_Strip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Menu_Strip
        '
        Me.Menu_Strip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_File, Me.Menu_SectionsA, Me.Menu_EditMode, Me.Menu_Tools, Me.Menu_HelpAbout})
        Me.Menu_Strip.Location = New System.Drawing.Point(0, 0)
        Me.Menu_Strip.Name = "Menu_Strip"
        Me.Menu_Strip.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.Menu_Strip.ShowItemToolTips = True
        Me.Menu_Strip.Size = New System.Drawing.Size(1484, 24)
        Me.Menu_Strip.TabIndex = 0
        Me.Menu_Strip.Text = "MenuStrip1"
        '
        'Menu_File
        '
        Me.Menu_File.AutoToolTip = True
        Me.Menu_File.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_OpenHauptwerkOrgan, Me.Menu_Recent, Me.Menu_CloseODF, Me.Menu_SaveAs, Me.Menu_Sep2, Me.Menu_PrintDT, Me.Menu_Sep3, Me.Menu_Quit})
        Me.Menu_File.Name = "Menu_File"
        Me.Menu_File.Size = New System.Drawing.Size(37, 20)
        Me.Menu_File.Text = "&File"
        '
        'Menu_OpenHauptwerkOrgan
        '
        Me.Menu_OpenHauptwerkOrgan.Name = "Menu_OpenHauptwerkOrgan"
        Me.Menu_OpenHauptwerkOrgan.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.Menu_OpenHauptwerkOrgan.Size = New System.Drawing.Size(252, 22)
        Me.Menu_OpenHauptwerkOrgan.Text = "&Open Hauptwerk Organ..."
        Me.Menu_OpenHauptwerkOrgan.ToolTipText = resources.GetString("Menu_OpenHauptwerkOrgan.ToolTipText")
        '
        'Menu_Recent
        '
        Me.Menu_Recent.Name = "Menu_Recent"
        Me.Menu_Recent.Size = New System.Drawing.Size(252, 22)
        Me.Menu_Recent.Text = "Open a Recent ODF..."
        '
        'Menu_CloseODF
        '
        Me.Menu_CloseODF.Enabled = False
        Me.Menu_CloseODF.Name = "Menu_CloseODF"
        Me.Menu_CloseODF.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.Menu_CloseODF.Size = New System.Drawing.Size(252, 22)
        Me.Menu_CloseODF.Text = "&Close ODF..."
        Me.Menu_CloseODF.ToolTipText = "Closes the current ODF, remaining in AECHO. If" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the current ODF has been modified" &
    " since it was" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "loaded or last saved, then Close ODF offers to save" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the current " &
    "ODF into a file."
        '
        'Menu_SaveAs
        '
        Me.Menu_SaveAs.Enabled = False
        Me.Menu_SaveAs.Name = "Menu_SaveAs"
        Me.Menu_SaveAs.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.Menu_SaveAs.Size = New System.Drawing.Size(252, 22)
        Me.Menu_SaveAs.Text = "&Save As..."
        Me.Menu_SaveAs.ToolTipText = "Saves the current ODF contents to a file. If" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "saved, the current ODF will be mark" &
    "ed as" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "unchanged until it undergoes further modifications."
        '
        'Menu_Sep2
        '
        Me.Menu_Sep2.Name = "Menu_Sep2"
        Me.Menu_Sep2.Size = New System.Drawing.Size(249, 6)
        '
        'Menu_PrintDT
        '
        Me.Menu_PrintDT.Name = "Menu_PrintDT"
        Me.Menu_PrintDT.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.Menu_PrintDT.Size = New System.Drawing.Size(252, 22)
        Me.Menu_PrintDT.Text = "&Print Descriptive Text..."
        Me.Menu_PrintDT.ToolTipText = "Print the contents of the Descriptive Text area."
        '
        'Menu_Sep3
        '
        Me.Menu_Sep3.Name = "Menu_Sep3"
        Me.Menu_Sep3.Size = New System.Drawing.Size(249, 6)
        '
        'Menu_Quit
        '
        Me.Menu_Quit.Name = "Menu_Quit"
        Me.Menu_Quit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.Menu_Quit.Size = New System.Drawing.Size(252, 22)
        Me.Menu_Quit.Text = "E&xit"
        Me.Menu_Quit.ToolTipText = "Exits AECHO. If there is an ODF onscreen that has" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "been modified since it was ope" &
    "ned or last saved," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "then Exit will offer to save that file before exiting."
        '
        'Menu_SectionsA
        '
        Me.Menu_SectionsA.Enabled = False
        Me.Menu_SectionsA.Name = "Menu_SectionsA"
        Me.Menu_SectionsA.Size = New System.Drawing.Size(63, 20)
        Me.Menu_SectionsA.Text = "&Sections"
        '
        'Menu_EditMode
        '
        Me.Menu_EditMode.BackColor = System.Drawing.Color.Gainsboro
        Me.Menu_EditMode.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_EditModeStart, Me.Menu_EditModeExit, Me.Menu_ReComputeSections})
        Me.Menu_EditMode.Enabled = False
        Me.Menu_EditMode.Name = "Menu_EditMode"
        Me.Menu_EditMode.Size = New System.Drawing.Size(73, 20)
        Me.Menu_EditMode.Text = "&Edit Mode"
        '
        'Menu_EditModeStart
        '
        Me.Menu_EditModeStart.Name = "Menu_EditModeStart"
        Me.Menu_EditModeStart.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.Menu_EditModeStart.Size = New System.Drawing.Size(250, 22)
        Me.Menu_EditModeStart.Text = "ODF Editing &Enabled"
        Me.Menu_EditModeStart.ToolTipText = "Selecting this enters Edit-Mode, allowing modifications" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to the current ODF. When" &
    " enabled, the Main Menu" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Item ""Edit Mode"" is highlighted with a RED background."
        '
        'Menu_EditModeExit
        '
        Me.Menu_EditModeExit.Name = "Menu_EditModeExit"
        Me.Menu_EditModeExit.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.Menu_EditModeExit.Size = New System.Drawing.Size(250, 22)
        Me.Menu_EditModeExit.Text = "ODF Editing &Disabled"
        Me.Menu_EditModeExit.ToolTipText = "This command exits Edit-Mode, returning the ODF" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to read-only."
        '
        'Menu_ReComputeSections
        '
        Me.Menu_ReComputeSections.Name = "Menu_ReComputeSections"
        Me.Menu_ReComputeSections.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.Menu_ReComputeSections.Size = New System.Drawing.Size(250, 22)
        Me.Menu_ReComputeSections.Text = "&Recompute Sections"
        Me.Menu_ReComputeSections.ToolTipText = resources.GetString("Menu_ReComputeSections.ToolTipText")
        '
        'Menu_Tools
        '
        Me.Menu_Tools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_ClearMarkers, Me.Menu_CouplersCode, Me.Menu_FollowASample, Me.Menu_ListSections})
        Me.Menu_Tools.Name = "Menu_Tools"
        Me.Menu_Tools.Size = New System.Drawing.Size(46, 20)
        Me.Menu_Tools.Text = "&Tools"
        '
        'Menu_ClearMarkers
        '
        Me.Menu_ClearMarkers.Name = "Menu_ClearMarkers"
        Me.Menu_ClearMarkers.Size = New System.Drawing.Size(216, 22)
        Me.Menu_ClearMarkers.Text = "Clear &Markers"
        Me.Menu_ClearMarkers.ToolTipText = "All Markers will be reset to empty."
        '
        'Menu_CouplersCode
        '
        Me.Menu_CouplersCode.Name = "Menu_CouplersCode"
        Me.Menu_CouplersCode.Size = New System.Drawing.Size(216, 22)
        Me.Menu_CouplersCode.Text = "&Couplers (Encode/Decode)"
        Me.Menu_CouplersCode.ToolTipText = resources.GetString("Menu_CouplersCode.ToolTipText")
        '
        'Menu_FollowASample
        '
        Me.Menu_FollowASample.Name = "Menu_FollowASample"
        Me.Menu_FollowASample.Size = New System.Drawing.Size(216, 22)
        Me.Menu_FollowASample.Text = "&Trace a Sample"
        Me.Menu_FollowASample.ToolTipText = resources.GetString("Menu_FollowASample.ToolTipText")
        '
        'Menu_ListSections
        '
        Me.Menu_ListSections.Name = "Menu_ListSections"
        Me.Menu_ListSections.Size = New System.Drawing.Size(216, 22)
        Me.Menu_ListSections.Text = "&List Sections"
        Me.Menu_ListSections.ToolTipText = resources.GetString("Menu_ListSections.ToolTipText")
        '
        'Menu_HelpAbout
        '
        Me.Menu_HelpAbout.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_Help, Me.Menu_About})
        Me.Menu_HelpAbout.Name = "Menu_HelpAbout"
        Me.Menu_HelpAbout.Size = New System.Drawing.Size(24, 20)
        Me.Menu_HelpAbout.Text = "&?"
        '
        'Menu_Help
        '
        Me.Menu_Help.Name = "Menu_Help"
        Me.Menu_Help.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F1), System.Windows.Forms.Keys)
        Me.Menu_Help.Size = New System.Drawing.Size(173, 22)
        Me.Menu_Help.Text = "View &Help"
        Me.Menu_Help.ToolTipText = "Presents AECHO's Help Text."
        '
        'Menu_About
        '
        Me.Menu_About.Name = "Menu_About"
        Me.Menu_About.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.Menu_About.Size = New System.Drawing.Size(173, 22)
        Me.Menu_About.Text = "&About AECHO"
        Me.Menu_About.ToolTipText = "Displays AECHO's About Box."
        '
        'Rtb_ODF
        '
        Me.Rtb_ODF.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Rtb_ODF.ContextMenuStrip = Me.CM_ODF
        Me.Rtb_ODF.DetectUrls = False
        Me.Rtb_ODF.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Rtb_ODF.HideSelection = False
        Me.Rtb_ODF.Location = New System.Drawing.Point(14, 57)
        Me.Rtb_ODF.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Rtb_ODF.Name = "Rtb_ODF"
        Me.Rtb_ODF.ReadOnly = True
        Me.Rtb_ODF.Size = New System.Drawing.Size(1227, 220)
        Me.Rtb_ODF.TabIndex = 1
        Me.Rtb_ODF.Text = ""
        Me.Rtb_ODF.WordWrap = False
        '
        'CM_ODF
        '
        Me.CM_ODF.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CM_ODFUndo, Me.CM_ODFRedo, Me.ToolStripSeparator4, Me.CM_ODFCut, Me.CM_ODFCopy, Me.CM_ODFPaste, Me.ToolStripSeparator5, Me.CM_ODFEditMode, Me.ToolStripSeparator6, Me.CM_ODFRecompute})
        Me.CM_ODF.Name = "CM_ODF"
        Me.CM_ODF.Size = New System.Drawing.Size(183, 176)
        '
        'CM_ODFUndo
        '
        Me.CM_ODFUndo.Name = "CM_ODFUndo"
        Me.CM_ODFUndo.ShortcutKeyDisplayString = " "
        Me.CM_ODFUndo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.CM_ODFUndo.Size = New System.Drawing.Size(182, 22)
        Me.CM_ODFUndo.Text = "Undo"
        Me.CM_ODFUndo.ToolTipText = "Undo last operation in the" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ODF Area"
        '
        'CM_ODFRedo
        '
        Me.CM_ODFRedo.Name = "CM_ODFRedo"
        Me.CM_ODFRedo.ShortcutKeyDisplayString = " "
        Me.CM_ODFRedo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.CM_ODFRedo.Size = New System.Drawing.Size(182, 22)
        Me.CM_ODFRedo.Text = "Redo"
        Me.CM_ODFRedo.ToolTipText = "Redo  last operation in the " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ODF Area"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(179, 6)
        '
        'CM_ODFCut
        '
        Me.CM_ODFCut.Name = "CM_ODFCut"
        Me.CM_ODFCut.ShortcutKeyDisplayString = " "
        Me.CM_ODFCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CM_ODFCut.Size = New System.Drawing.Size(182, 22)
        Me.CM_ODFCut.Text = "Cut"
        Me.CM_ODFCut.ToolTipText = "Cut selected text from the" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ODF Area"
        '
        'CM_ODFCopy
        '
        Me.CM_ODFCopy.Name = "CM_ODFCopy"
        Me.CM_ODFCopy.ShortcutKeyDisplayString = " "
        Me.CM_ODFCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CM_ODFCopy.Size = New System.Drawing.Size(182, 22)
        Me.CM_ODFCopy.Text = "Copy"
        Me.CM_ODFCopy.ToolTipText = "Copy selected text from the" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ODF Area"
        '
        'CM_ODFPaste
        '
        Me.CM_ODFPaste.Name = "CM_ODFPaste"
        Me.CM_ODFPaste.ShortcutKeyDisplayString = " "
        Me.CM_ODFPaste.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.CM_ODFPaste.Size = New System.Drawing.Size(182, 22)
        Me.CM_ODFPaste.Text = "Paste"
        Me.CM_ODFPaste.ToolTipText = "Paste text into the ODF Area"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(179, 6)
        '
        'CM_ODFEditMode
        '
        Me.CM_ODFEditMode.Name = "CM_ODFEditMode"
        Me.CM_ODFEditMode.Size = New System.Drawing.Size(182, 22)
        Me.CM_ODFEditMode.Text = "Enable Editing"
        Me.CM_ODFEditMode.ToolTipText = "Enable or Disable editing of the" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ODF Area"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(179, 6)
        '
        'CM_ODFRecompute
        '
        Me.CM_ODFRecompute.Name = "CM_ODFRecompute"
        Me.CM_ODFRecompute.Size = New System.Drawing.Size(182, 22)
        Me.CM_ODFRecompute.Text = "Recompute Sections"
        Me.CM_ODFRecompute.ToolTipText = "Recompute the Section locations"
        '
        'CM_XML
        '
        Me.CM_XML.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CM_XMLCopy})
        Me.CM_XML.Name = "CM_XML"
        Me.CM_XML.Size = New System.Drawing.Size(113, 26)
        '
        'CM_XMLCopy
        '
        Me.CM_XMLCopy.Name = "CM_XMLCopy"
        Me.CM_XMLCopy.ShortcutKeyDisplayString = " "
        Me.CM_XMLCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CM_XMLCopy.Size = New System.Drawing.Size(112, 22)
        Me.CM_XMLCopy.Text = "Copy"
        Me.CM_XMLCopy.ToolTipText = "Copy text from the XML Row/Record Area"
        '
        'Num_ODFFontSize
        '
        Me.Num_ODFFontSize.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Num_ODFFontSize.Location = New System.Drawing.Point(828, 32)
        Me.Num_ODFFontSize.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Num_ODFFontSize.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.Num_ODFFontSize.Minimum = New Decimal(New Integer() {7, 0, 0, 0})
        Me.Num_ODFFontSize.Name = "Num_ODFFontSize"
        Me.Num_ODFFontSize.Size = New System.Drawing.Size(44, 19)
        Me.Num_ODFFontSize.TabIndex = 4
        Me.Num_ODFFontSize.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Lbl_SecStartTitle
        '
        Me.Lbl_SecStartTitle.AutoSize = True
        Me.Lbl_SecStartTitle.Location = New System.Drawing.Point(7, 210)
        Me.Lbl_SecStartTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_SecStartTitle.Name = "Lbl_SecStartTitle"
        Me.Lbl_SecStartTitle.Size = New System.Drawing.Size(52, 15)
        Me.Lbl_SecStartTitle.TabIndex = 6
        Me.Lbl_SecStartTitle.Text = "Sec Start"
        '
        'Lbl_SecEndTitle
        '
        Me.Lbl_SecEndTitle.AutoSize = True
        Me.Lbl_SecEndTitle.Location = New System.Drawing.Point(7, 239)
        Me.Lbl_SecEndTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_SecEndTitle.Name = "Lbl_SecEndTitle"
        Me.Lbl_SecEndTitle.Size = New System.Drawing.Size(48, 15)
        Me.Lbl_SecEndTitle.TabIndex = 7
        Me.Lbl_SecEndTitle.Text = "Sec End"
        '
        'Lbl_RowStartTitle
        '
        Me.Lbl_RowStartTitle.AutoSize = True
        Me.Lbl_RowStartTitle.Location = New System.Drawing.Point(7, 152)
        Me.Lbl_RowStartTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_RowStartTitle.Name = "Lbl_RowStartTitle"
        Me.Lbl_RowStartTitle.Size = New System.Drawing.Size(57, 15)
        Me.Lbl_RowStartTitle.TabIndex = 8
        Me.Lbl_RowStartTitle.Text = "Row Start"
        '
        'Lbl_SecStartVal
        '
        Me.Lbl_SecStartVal.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Lbl_SecStartVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_SecStartVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_SecStartVal.Location = New System.Drawing.Point(67, 206)
        Me.Lbl_SecStartVal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_SecStartVal.Name = "Lbl_SecStartVal"
        Me.Lbl_SecStartVal.Size = New System.Drawing.Size(142, 22)
        Me.Lbl_SecStartVal.TabIndex = 9
        Me.Lbl_SecStartVal.Text = "<NA>"
        Me.Lbl_SecStartVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lbl_SecEndVal
        '
        Me.Lbl_SecEndVal.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Lbl_SecEndVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_SecEndVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_SecEndVal.Location = New System.Drawing.Point(67, 235)
        Me.Lbl_SecEndVal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_SecEndVal.Name = "Lbl_SecEndVal"
        Me.Lbl_SecEndVal.Size = New System.Drawing.Size(142, 22)
        Me.Lbl_SecEndVal.TabIndex = 10
        Me.Lbl_SecEndVal.Text = "<NA>"
        Me.Lbl_SecEndVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lbl_RowStartVal
        '
        Me.Lbl_RowStartVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Lbl_RowStartVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_RowStartVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_RowStartVal.Location = New System.Drawing.Point(67, 148)
        Me.Lbl_RowStartVal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_RowStartVal.Name = "Lbl_RowStartVal"
        Me.Lbl_RowStartVal.Size = New System.Drawing.Size(142, 22)
        Me.Lbl_RowStartVal.TabIndex = 11
        Me.Lbl_RowStartVal.Text = "<NA>"
        Me.Lbl_RowStartVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lbl_CursorPosTitle
        '
        Me.Lbl_CursorPosTitle.AutoSize = True
        Me.Lbl_CursorPosTitle.Location = New System.Drawing.Point(7, 36)
        Me.Lbl_CursorPosTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_CursorPosTitle.Name = "Lbl_CursorPosTitle"
        Me.Lbl_CursorPosTitle.Size = New System.Drawing.Size(57, 15)
        Me.Lbl_CursorPosTitle.TabIndex = 12
        Me.Lbl_CursorPosTitle.Text = "Caret Pos"
        '
        'Lbl_CursorPosVal
        '
        Me.Lbl_CursorPosVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Lbl_CursorPosVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_CursorPosVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_CursorPosVal.Location = New System.Drawing.Point(67, 32)
        Me.Lbl_CursorPosVal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_CursorPosVal.Name = "Lbl_CursorPosVal"
        Me.Lbl_CursorPosVal.Size = New System.Drawing.Size(142, 22)
        Me.Lbl_CursorPosVal.TabIndex = 13
        Me.Lbl_CursorPosVal.Text = "<NA>"
        Me.Lbl_CursorPosVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lbl_RowEndTitle
        '
        Me.Lbl_RowEndTitle.AutoSize = True
        Me.Lbl_RowEndTitle.Location = New System.Drawing.Point(7, 181)
        Me.Lbl_RowEndTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_RowEndTitle.Name = "Lbl_RowEndTitle"
        Me.Lbl_RowEndTitle.Size = New System.Drawing.Size(53, 15)
        Me.Lbl_RowEndTitle.TabIndex = 22
        Me.Lbl_RowEndTitle.Text = "Row End"
        '
        'Lbl_RowEndVal
        '
        Me.Lbl_RowEndVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Lbl_RowEndVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_RowEndVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_RowEndVal.Location = New System.Drawing.Point(67, 177)
        Me.Lbl_RowEndVal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_RowEndVal.Name = "Lbl_RowEndVal"
        Me.Lbl_RowEndVal.Size = New System.Drawing.Size(142, 22)
        Me.Lbl_RowEndVal.TabIndex = 23
        Me.Lbl_RowEndVal.Text = "<NA>"
        Me.Lbl_RowEndVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lbl_SectionTitle
        '
        Me.Lbl_SectionTitle.AutoSize = True
        Me.Lbl_SectionTitle.Cursor = System.Windows.Forms.Cursors.Default
        Me.Lbl_SectionTitle.Location = New System.Drawing.Point(14, 38)
        Me.Lbl_SectionTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_SectionTitle.Name = "Lbl_SectionTitle"
        Me.Lbl_SectionTitle.Size = New System.Drawing.Size(49, 15)
        Me.Lbl_SectionTitle.TabIndex = 28
        Me.Lbl_SectionTitle.Text = "Section:"
        '
        'Lbl_SectionName
        '
        Me.Lbl_SectionName.AutoSize = True
        Me.Lbl_SectionName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_SectionName.ForeColor = System.Drawing.Color.Red
        Me.Lbl_SectionName.Location = New System.Drawing.Point(68, 38)
        Me.Lbl_SectionName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_SectionName.Name = "Lbl_SectionName"
        Me.Lbl_SectionName.Size = New System.Drawing.Size(44, 16)
        Me.Lbl_SectionName.TabIndex = 29
        Me.Lbl_SectionName.Text = "None"
        '
        'Lbl_NumTagsTitle
        '
        Me.Lbl_NumTagsTitle.AutoSize = True
        Me.Lbl_NumTagsTitle.Location = New System.Drawing.Point(7, 269)
        Me.Lbl_NumTagsTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_NumTagsTitle.Name = "Lbl_NumTagsTitle"
        Me.Lbl_NumTagsTitle.Size = New System.Drawing.Size(91, 15)
        Me.Lbl_NumTagsTitle.TabIndex = 30
        Me.Lbl_NumTagsTitle.Text = "Number of Tags"
        '
        'Lbl_NumTagsVal
        '
        Me.Lbl_NumTagsVal.AutoSize = True
        Me.Lbl_NumTagsVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_NumTagsVal.ForeColor = System.Drawing.Color.Red
        Me.Lbl_NumTagsVal.Location = New System.Drawing.Point(97, 267)
        Me.Lbl_NumTagsVal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_NumTagsVal.Name = "Lbl_NumTagsVal"
        Me.Lbl_NumTagsVal.Size = New System.Drawing.Size(17, 17)
        Me.Lbl_NumTagsVal.TabIndex = 31
        Me.Lbl_NumTagsVal.Text = "0"
        '
        'Rtb_DescText
        '
        Me.Rtb_DescText.AcceptsTab = True
        Me.Rtb_DescText.ContextMenuStrip = Me.CM_Desc
        Me.Rtb_DescText.DetectUrls = False
        Me.Rtb_DescText.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Rtb_DescText.Location = New System.Drawing.Point(722, 451)
        Me.Rtb_DescText.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Rtb_DescText.Name = "Rtb_DescText"
        Me.Rtb_DescText.Size = New System.Drawing.Size(750, 380)
        Me.Rtb_DescText.TabIndex = 56
        Me.Rtb_DescText.Text = ""
        Me.Rtb_DescText.WordWrap = False
        '
        'CM_Desc
        '
        Me.CM_Desc.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CM_DescUndo, Me.CM_DescRedo, Me.ToolStripSeparator2, Me.CM_DescCut, Me.CM_DescCopy, Me.CM_DescPaste, Me.ToolStripSeparator3, Me.CM_DescPrint, Me.ToolStripSeparator7, Me.CM_DescFont})
        Me.CM_Desc.Name = "CM_Desc"
        Me.CM_Desc.Size = New System.Drawing.Size(119, 176)
        '
        'CM_DescUndo
        '
        Me.CM_DescUndo.Name = "CM_DescUndo"
        Me.CM_DescUndo.ShortcutKeyDisplayString = " "
        Me.CM_DescUndo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.CM_DescUndo.Size = New System.Drawing.Size(118, 22)
        Me.CM_DescUndo.Text = "Undo"
        Me.CM_DescUndo.ToolTipText = "Undo last from the Descriptive" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Text Area"
        '
        'CM_DescRedo
        '
        Me.CM_DescRedo.Name = "CM_DescRedo"
        Me.CM_DescRedo.ShortcutKeyDisplayString = " "
        Me.CM_DescRedo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.CM_DescRedo.Size = New System.Drawing.Size(118, 22)
        Me.CM_DescRedo.Text = "Redo"
        Me.CM_DescRedo.ToolTipText = "Redo the last operation in the" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Descriptive Text Area"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(115, 6)
        '
        'CM_DescCut
        '
        Me.CM_DescCut.Name = "CM_DescCut"
        Me.CM_DescCut.ShortcutKeyDisplayString = " "
        Me.CM_DescCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CM_DescCut.Size = New System.Drawing.Size(118, 22)
        Me.CM_DescCut.Text = "Cut"
        Me.CM_DescCut.ToolTipText = "Cut text from the Descriptive" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Text Area"
        '
        'CM_DescCopy
        '
        Me.CM_DescCopy.Name = "CM_DescCopy"
        Me.CM_DescCopy.ShortcutKeyDisplayString = " "
        Me.CM_DescCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CM_DescCopy.Size = New System.Drawing.Size(118, 22)
        Me.CM_DescCopy.Text = "Copy"
        Me.CM_DescCopy.ToolTipText = "Copy text from the Descriptive" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Text Area"
        '
        'CM_DescPaste
        '
        Me.CM_DescPaste.Name = "CM_DescPaste"
        Me.CM_DescPaste.ShortcutKeyDisplayString = " "
        Me.CM_DescPaste.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.CM_DescPaste.Size = New System.Drawing.Size(118, 22)
        Me.CM_DescPaste.Text = "Paste"
        Me.CM_DescPaste.ToolTipText = "Paste text or image into the" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Descriptive Text Area"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(115, 6)
        '
        'CM_DescPrint
        '
        Me.CM_DescPrint.Name = "CM_DescPrint"
        Me.CM_DescPrint.ShortcutKeyDisplayString = " "
        Me.CM_DescPrint.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.CM_DescPrint.Size = New System.Drawing.Size(118, 22)
        Me.CM_DescPrint.Text = "Print..."
        Me.CM_DescPrint.ToolTipText = "Print the Descriptive Text Area"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(115, 6)
        '
        'CM_DescFont
        '
        Me.CM_DescFont.Name = "CM_DescFont"
        Me.CM_DescFont.Size = New System.Drawing.Size(118, 22)
        Me.CM_DescFont.Text = "Font"
        '
        'Rtb_XMLRow
        '
        Me.Rtb_XMLRow.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Rtb_XMLRow.ContextMenuStrip = Me.CM_XML
        Me.Rtb_XMLRow.DetectUrls = False
        Me.Rtb_XMLRow.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Rtb_XMLRow.Location = New System.Drawing.Point(14, 307)
        Me.Rtb_XMLRow.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Rtb_XMLRow.Name = "Rtb_XMLRow"
        Me.Rtb_XMLRow.ReadOnly = True
        Me.Rtb_XMLRow.Size = New System.Drawing.Size(1227, 47)
        Me.Rtb_XMLRow.TabIndex = 61
        Me.Rtb_XMLRow.Text = ""
        '
        'Btn_SaveDescText
        '
        Me.Btn_SaveDescText.Enabled = False
        Me.Btn_SaveDescText.Location = New System.Drawing.Point(1369, 389)
        Me.Btn_SaveDescText.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_SaveDescText.Name = "Btn_SaveDescText"
        Me.Btn_SaveDescText.Size = New System.Drawing.Size(93, 23)
        Me.Btn_SaveDescText.TabIndex = 62
        Me.Btn_SaveDescText.Text = "Save Desc. Text"
        Me.Btn_SaveDescText.UseVisualStyleBackColor = True
        '
        'Pnl_Tags
        '
        Me.Pnl_Tags.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Pnl_Tags.Controls.Add(Me.Btn_RowAction)
        Me.Pnl_Tags.Controls.Add(Me.tag24)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag24)
        Me.Pnl_Tags.Controls.Add(Me.tag23)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag23)
        Me.Pnl_Tags.Controls.Add(Me.tag22)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag22)
        Me.Pnl_Tags.Controls.Add(Me.tag21)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag21)
        Me.Pnl_Tags.Controls.Add(Me.tag20)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag20)
        Me.Pnl_Tags.Controls.Add(Me.tag19)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag19)
        Me.Pnl_Tags.Controls.Add(Me.tag18)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag18)
        Me.Pnl_Tags.Controls.Add(Me.tag17)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag17)
        Me.Pnl_Tags.Controls.Add(Me.tag16)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag16)
        Me.Pnl_Tags.Controls.Add(Me.tag15)
        Me.Pnl_Tags.Controls.Add(Me.tag14)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag15)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag14)
        Me.Pnl_Tags.Controls.Add(Me.tag13)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag13)
        Me.Pnl_Tags.Controls.Add(Me.tag12)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag12)
        Me.Pnl_Tags.Controls.Add(Me.tag11)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag11)
        Me.Pnl_Tags.Controls.Add(Me.tag10)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag10)
        Me.Pnl_Tags.Controls.Add(Me.tag9)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag9)
        Me.Pnl_Tags.Controls.Add(Me.tag8)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag8)
        Me.Pnl_Tags.Controls.Add(Me.tag7)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag7)
        Me.Pnl_Tags.Controls.Add(Me.tag6)
        Me.Pnl_Tags.Controls.Add(Me.tag5)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag6)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag5)
        Me.Pnl_Tags.Controls.Add(Me.tag4)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag4)
        Me.Pnl_Tags.Controls.Add(Me.tag3)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag3)
        Me.Pnl_Tags.Controls.Add(Me.tag2)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag2)
        Me.Pnl_Tags.Controls.Add(Me.tag1)
        Me.Pnl_Tags.Controls.Add(Me.LabelTag1)
        Me.Pnl_Tags.Location = New System.Drawing.Point(14, 451)
        Me.Pnl_Tags.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Pnl_Tags.Name = "Pnl_Tags"
        Me.Pnl_Tags.Size = New System.Drawing.Size(699, 380)
        Me.Pnl_Tags.TabIndex = 71
        '
        'Btn_RowAction
        '
        Me.Btn_RowAction.Location = New System.Drawing.Point(275, 316)
        Me.Btn_RowAction.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_RowAction.Name = "Btn_RowAction"
        Me.Btn_RowAction.Size = New System.Drawing.Size(70, 57)
        Me.Btn_RowAction.TabIndex = 95
        Me.Btn_RowAction.Text = "Row Action"
        Me.Btn_RowAction.UseVisualStyleBackColor = True
        '
        'tag24
        '
        Me.tag24.AutoSize = True
        Me.tag24.BackColor = System.Drawing.Color.Gainsboro
        Me.tag24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag24.Location = New System.Drawing.Point(398, 355)
        Me.tag24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag24.Name = "tag24"
        Me.tag24.Size = New System.Drawing.Size(41, 16)
        Me.tag24.TabIndex = 94
        Me.tag24.Text = "Tag24"
        Me.tag24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag24
        '
        Me.LabelTag24.AutoSize = True
        Me.LabelTag24.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag24.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag24.ForeColor = System.Drawing.Color.Black
        Me.LabelTag24.Location = New System.Drawing.Point(356, 351)
        Me.LabelTag24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag24.Name = "LabelTag24"
        Me.LabelTag24.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag24.TabIndex = 93
        Me.LabelTag24.Text = "X"
        '
        'tag23
        '
        Me.tag23.AutoSize = True
        Me.tag23.BackColor = System.Drawing.Color.Gainsboro
        Me.tag23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag23.Location = New System.Drawing.Point(398, 325)
        Me.tag23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag23.Name = "tag23"
        Me.tag23.Size = New System.Drawing.Size(41, 16)
        Me.tag23.TabIndex = 92
        Me.tag23.Text = "Tag23"
        Me.tag23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag23
        '
        Me.LabelTag23.AutoSize = True
        Me.LabelTag23.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag23.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag23.ForeColor = System.Drawing.Color.Black
        Me.LabelTag23.Location = New System.Drawing.Point(356, 321)
        Me.LabelTag23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag23.Name = "LabelTag23"
        Me.LabelTag23.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag23.TabIndex = 91
        Me.LabelTag23.Text = "X"
        '
        'tag22
        '
        Me.tag22.AutoSize = True
        Me.tag22.BackColor = System.Drawing.Color.Gainsboro
        Me.tag22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag22.Location = New System.Drawing.Point(398, 295)
        Me.tag22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag22.Name = "tag22"
        Me.tag22.Size = New System.Drawing.Size(41, 16)
        Me.tag22.TabIndex = 90
        Me.tag22.Text = "Tag22"
        Me.tag22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag22
        '
        Me.LabelTag22.AutoSize = True
        Me.LabelTag22.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag22.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag22.ForeColor = System.Drawing.Color.Black
        Me.LabelTag22.Location = New System.Drawing.Point(356, 291)
        Me.LabelTag22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag22.Name = "LabelTag22"
        Me.LabelTag22.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag22.TabIndex = 89
        Me.LabelTag22.Text = "X"
        '
        'tag21
        '
        Me.tag21.AutoSize = True
        Me.tag21.BackColor = System.Drawing.Color.Gainsboro
        Me.tag21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag21.Location = New System.Drawing.Point(398, 265)
        Me.tag21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag21.Name = "tag21"
        Me.tag21.Size = New System.Drawing.Size(41, 16)
        Me.tag21.TabIndex = 88
        Me.tag21.Text = "Tag21"
        Me.tag21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag21
        '
        Me.LabelTag21.AutoSize = True
        Me.LabelTag21.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag21.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag21.ForeColor = System.Drawing.Color.Black
        Me.LabelTag21.Location = New System.Drawing.Point(356, 261)
        Me.LabelTag21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag21.Name = "LabelTag21"
        Me.LabelTag21.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag21.TabIndex = 87
        Me.LabelTag21.Text = "X"
        '
        'tag20
        '
        Me.tag20.AutoSize = True
        Me.tag20.BackColor = System.Drawing.Color.Gainsboro
        Me.tag20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag20.Location = New System.Drawing.Point(398, 235)
        Me.tag20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag20.Name = "tag20"
        Me.tag20.Size = New System.Drawing.Size(41, 16)
        Me.tag20.TabIndex = 86
        Me.tag20.Text = "Tag20"
        Me.tag20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag20
        '
        Me.LabelTag20.AutoSize = True
        Me.LabelTag20.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag20.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag20.ForeColor = System.Drawing.Color.Black
        Me.LabelTag20.Location = New System.Drawing.Point(356, 231)
        Me.LabelTag20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag20.Name = "LabelTag20"
        Me.LabelTag20.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag20.TabIndex = 85
        Me.LabelTag20.Text = "X"
        '
        'tag19
        '
        Me.tag19.AutoSize = True
        Me.tag19.BackColor = System.Drawing.Color.Gainsboro
        Me.tag19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag19.Location = New System.Drawing.Point(398, 205)
        Me.tag19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag19.Name = "tag19"
        Me.tag19.Size = New System.Drawing.Size(41, 16)
        Me.tag19.TabIndex = 84
        Me.tag19.Text = "Tag19"
        Me.tag19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag19
        '
        Me.LabelTag19.AutoSize = True
        Me.LabelTag19.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag19.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag19.ForeColor = System.Drawing.Color.Black
        Me.LabelTag19.Location = New System.Drawing.Point(356, 201)
        Me.LabelTag19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag19.Name = "LabelTag19"
        Me.LabelTag19.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag19.TabIndex = 83
        Me.LabelTag19.Text = "X"
        '
        'tag18
        '
        Me.tag18.AutoSize = True
        Me.tag18.BackColor = System.Drawing.Color.Gainsboro
        Me.tag18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag18.Location = New System.Drawing.Point(398, 175)
        Me.tag18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag18.Name = "tag18"
        Me.tag18.Size = New System.Drawing.Size(41, 16)
        Me.tag18.TabIndex = 82
        Me.tag18.Text = "Tag18"
        Me.tag18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag18
        '
        Me.LabelTag18.AutoSize = True
        Me.LabelTag18.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag18.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag18.ForeColor = System.Drawing.Color.Black
        Me.LabelTag18.Location = New System.Drawing.Point(356, 171)
        Me.LabelTag18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag18.Name = "LabelTag18"
        Me.LabelTag18.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag18.TabIndex = 81
        Me.LabelTag18.Text = "X"
        '
        'tag17
        '
        Me.tag17.AutoSize = True
        Me.tag17.BackColor = System.Drawing.Color.Gainsboro
        Me.tag17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag17.Location = New System.Drawing.Point(398, 145)
        Me.tag17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag17.Name = "tag17"
        Me.tag17.Size = New System.Drawing.Size(41, 16)
        Me.tag17.TabIndex = 80
        Me.tag17.Text = "Tag17"
        Me.tag17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag17
        '
        Me.LabelTag17.AutoSize = True
        Me.LabelTag17.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag17.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag17.ForeColor = System.Drawing.Color.Black
        Me.LabelTag17.Location = New System.Drawing.Point(356, 141)
        Me.LabelTag17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag17.Name = "LabelTag17"
        Me.LabelTag17.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag17.TabIndex = 79
        Me.LabelTag17.Text = "X"
        '
        'tag16
        '
        Me.tag16.AutoSize = True
        Me.tag16.BackColor = System.Drawing.Color.Gainsboro
        Me.tag16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag16.Location = New System.Drawing.Point(398, 115)
        Me.tag16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag16.Name = "tag16"
        Me.tag16.Size = New System.Drawing.Size(41, 16)
        Me.tag16.TabIndex = 78
        Me.tag16.Text = "Tag16"
        Me.tag16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag16
        '
        Me.LabelTag16.AutoSize = True
        Me.LabelTag16.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag16.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag16.ForeColor = System.Drawing.Color.Black
        Me.LabelTag16.Location = New System.Drawing.Point(356, 111)
        Me.LabelTag16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag16.Name = "LabelTag16"
        Me.LabelTag16.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag16.TabIndex = 77
        Me.LabelTag16.Text = "X"
        '
        'tag15
        '
        Me.tag15.AutoSize = True
        Me.tag15.BackColor = System.Drawing.Color.Gainsboro
        Me.tag15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag15.Location = New System.Drawing.Point(398, 85)
        Me.tag15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag15.Name = "tag15"
        Me.tag15.Size = New System.Drawing.Size(41, 16)
        Me.tag15.TabIndex = 76
        Me.tag15.Text = "Tag15"
        Me.tag15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tag14
        '
        Me.tag14.AutoSize = True
        Me.tag14.BackColor = System.Drawing.Color.Gainsboro
        Me.tag14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag14.Location = New System.Drawing.Point(398, 55)
        Me.tag14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag14.Name = "tag14"
        Me.tag14.Size = New System.Drawing.Size(41, 16)
        Me.tag14.TabIndex = 75
        Me.tag14.Text = "Tag14"
        Me.tag14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag15
        '
        Me.LabelTag15.AutoSize = True
        Me.LabelTag15.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag15.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag15.ForeColor = System.Drawing.Color.Black
        Me.LabelTag15.Location = New System.Drawing.Point(356, 81)
        Me.LabelTag15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag15.Name = "LabelTag15"
        Me.LabelTag15.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag15.TabIndex = 74
        Me.LabelTag15.Text = "X"
        '
        'LabelTag14
        '
        Me.LabelTag14.AutoSize = True
        Me.LabelTag14.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag14.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag14.ForeColor = System.Drawing.Color.Black
        Me.LabelTag14.Location = New System.Drawing.Point(356, 51)
        Me.LabelTag14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag14.Name = "LabelTag14"
        Me.LabelTag14.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag14.TabIndex = 73
        Me.LabelTag14.Text = "X"
        '
        'tag13
        '
        Me.tag13.AutoSize = True
        Me.tag13.BackColor = System.Drawing.Color.Gainsboro
        Me.tag13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag13.Location = New System.Drawing.Point(398, 25)
        Me.tag13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag13.Name = "tag13"
        Me.tag13.Size = New System.Drawing.Size(41, 16)
        Me.tag13.TabIndex = 72
        Me.tag13.Text = "Tag13"
        Me.tag13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag13
        '
        Me.LabelTag13.AutoSize = True
        Me.LabelTag13.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag13.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag13.ForeColor = System.Drawing.Color.Black
        Me.LabelTag13.Location = New System.Drawing.Point(356, 21)
        Me.LabelTag13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag13.Name = "LabelTag13"
        Me.LabelTag13.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag13.TabIndex = 71
        Me.LabelTag13.Text = "X"
        '
        'tag12
        '
        Me.tag12.AutoSize = True
        Me.tag12.BackColor = System.Drawing.Color.Gainsboro
        Me.tag12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag12.Location = New System.Drawing.Point(62, 355)
        Me.tag12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag12.Name = "tag12"
        Me.tag12.Size = New System.Drawing.Size(41, 16)
        Me.tag12.TabIndex = 67
        Me.tag12.Text = "Tag12"
        Me.tag12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag12
        '
        Me.LabelTag12.AutoSize = True
        Me.LabelTag12.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag12.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag12.ForeColor = System.Drawing.Color.Black
        Me.LabelTag12.Location = New System.Drawing.Point(20, 351)
        Me.LabelTag12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag12.Name = "LabelTag12"
        Me.LabelTag12.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag12.TabIndex = 66
        Me.LabelTag12.Text = "X"
        '
        'tag11
        '
        Me.tag11.AutoSize = True
        Me.tag11.BackColor = System.Drawing.Color.Gainsboro
        Me.tag11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag11.Location = New System.Drawing.Point(62, 325)
        Me.tag11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag11.Name = "tag11"
        Me.tag11.Size = New System.Drawing.Size(40, 16)
        Me.tag11.TabIndex = 65
        Me.tag11.Text = "Tag11"
        Me.tag11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag11
        '
        Me.LabelTag11.AutoSize = True
        Me.LabelTag11.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag11.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag11.ForeColor = System.Drawing.Color.Black
        Me.LabelTag11.Location = New System.Drawing.Point(20, 321)
        Me.LabelTag11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag11.Name = "LabelTag11"
        Me.LabelTag11.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag11.TabIndex = 64
        Me.LabelTag11.Text = "X"
        '
        'tag10
        '
        Me.tag10.AutoSize = True
        Me.tag10.BackColor = System.Drawing.Color.Gainsboro
        Me.tag10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag10.Location = New System.Drawing.Point(62, 295)
        Me.tag10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag10.Name = "tag10"
        Me.tag10.Size = New System.Drawing.Size(41, 16)
        Me.tag10.TabIndex = 63
        Me.tag10.Text = "Tag10"
        Me.tag10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag10
        '
        Me.LabelTag10.AutoSize = True
        Me.LabelTag10.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag10.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag10.ForeColor = System.Drawing.Color.Black
        Me.LabelTag10.Location = New System.Drawing.Point(20, 291)
        Me.LabelTag10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag10.Name = "LabelTag10"
        Me.LabelTag10.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag10.TabIndex = 62
        Me.LabelTag10.Text = "X"
        '
        'tag9
        '
        Me.tag9.AutoSize = True
        Me.tag9.BackColor = System.Drawing.Color.Gainsboro
        Me.tag9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag9.Location = New System.Drawing.Point(62, 265)
        Me.tag9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag9.Name = "tag9"
        Me.tag9.Size = New System.Drawing.Size(41, 16)
        Me.tag9.TabIndex = 61
        Me.tag9.Text = "Tag09"
        Me.tag9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag9
        '
        Me.LabelTag9.AutoSize = True
        Me.LabelTag9.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag9.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag9.ForeColor = System.Drawing.Color.Black
        Me.LabelTag9.Location = New System.Drawing.Point(20, 261)
        Me.LabelTag9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag9.Name = "LabelTag9"
        Me.LabelTag9.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag9.TabIndex = 60
        Me.LabelTag9.Text = "X"
        '
        'tag8
        '
        Me.tag8.AutoSize = True
        Me.tag8.BackColor = System.Drawing.Color.Gainsboro
        Me.tag8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag8.Location = New System.Drawing.Point(62, 235)
        Me.tag8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag8.Name = "tag8"
        Me.tag8.Size = New System.Drawing.Size(41, 16)
        Me.tag8.TabIndex = 59
        Me.tag8.Text = "Tag08"
        Me.tag8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag8
        '
        Me.LabelTag8.AutoSize = True
        Me.LabelTag8.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag8.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag8.ForeColor = System.Drawing.Color.Black
        Me.LabelTag8.Location = New System.Drawing.Point(20, 231)
        Me.LabelTag8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag8.Name = "LabelTag8"
        Me.LabelTag8.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag8.TabIndex = 58
        Me.LabelTag8.Text = "X"
        '
        'tag7
        '
        Me.tag7.AutoSize = True
        Me.tag7.BackColor = System.Drawing.Color.Gainsboro
        Me.tag7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag7.Location = New System.Drawing.Point(62, 205)
        Me.tag7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag7.Name = "tag7"
        Me.tag7.Size = New System.Drawing.Size(41, 16)
        Me.tag7.TabIndex = 57
        Me.tag7.Text = "Tag07"
        Me.tag7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag7
        '
        Me.LabelTag7.AutoSize = True
        Me.LabelTag7.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag7.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag7.ForeColor = System.Drawing.Color.Black
        Me.LabelTag7.Location = New System.Drawing.Point(20, 201)
        Me.LabelTag7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag7.Name = "LabelTag7"
        Me.LabelTag7.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag7.TabIndex = 56
        Me.LabelTag7.Text = "X"
        '
        'tag6
        '
        Me.tag6.AutoSize = True
        Me.tag6.BackColor = System.Drawing.Color.Gainsboro
        Me.tag6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag6.Location = New System.Drawing.Point(62, 175)
        Me.tag6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag6.Name = "tag6"
        Me.tag6.Size = New System.Drawing.Size(41, 16)
        Me.tag6.TabIndex = 55
        Me.tag6.Text = "Tag06"
        Me.tag6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tag5
        '
        Me.tag5.AutoSize = True
        Me.tag5.BackColor = System.Drawing.Color.Gainsboro
        Me.tag5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag5.Location = New System.Drawing.Point(62, 145)
        Me.tag5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag5.Name = "tag5"
        Me.tag5.Size = New System.Drawing.Size(41, 16)
        Me.tag5.TabIndex = 54
        Me.tag5.Text = "Tag05"
        Me.tag5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag6
        '
        Me.LabelTag6.AutoSize = True
        Me.LabelTag6.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag6.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag6.ForeColor = System.Drawing.Color.Black
        Me.LabelTag6.Location = New System.Drawing.Point(20, 171)
        Me.LabelTag6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag6.Name = "LabelTag6"
        Me.LabelTag6.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag6.TabIndex = 53
        Me.LabelTag6.Text = "X"
        '
        'LabelTag5
        '
        Me.LabelTag5.AutoSize = True
        Me.LabelTag5.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag5.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag5.ForeColor = System.Drawing.Color.Black
        Me.LabelTag5.Location = New System.Drawing.Point(20, 141)
        Me.LabelTag5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag5.Name = "LabelTag5"
        Me.LabelTag5.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag5.TabIndex = 52
        Me.LabelTag5.Text = "X"
        '
        'tag4
        '
        Me.tag4.AutoSize = True
        Me.tag4.BackColor = System.Drawing.Color.Gainsboro
        Me.tag4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag4.Location = New System.Drawing.Point(62, 115)
        Me.tag4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag4.Name = "tag4"
        Me.tag4.Size = New System.Drawing.Size(41, 16)
        Me.tag4.TabIndex = 51
        Me.tag4.Text = "Tag04"
        Me.tag4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag4
        '
        Me.LabelTag4.AutoSize = True
        Me.LabelTag4.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag4.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag4.ForeColor = System.Drawing.Color.Black
        Me.LabelTag4.Location = New System.Drawing.Point(20, 111)
        Me.LabelTag4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag4.Name = "LabelTag4"
        Me.LabelTag4.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag4.TabIndex = 50
        Me.LabelTag4.Text = "X"
        '
        'tag3
        '
        Me.tag3.AutoSize = True
        Me.tag3.BackColor = System.Drawing.Color.Gainsboro
        Me.tag3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag3.Location = New System.Drawing.Point(62, 85)
        Me.tag3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag3.Name = "tag3"
        Me.tag3.Size = New System.Drawing.Size(41, 16)
        Me.tag3.TabIndex = 49
        Me.tag3.Text = "Tag03"
        Me.tag3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag3
        '
        Me.LabelTag3.AutoSize = True
        Me.LabelTag3.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag3.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag3.ForeColor = System.Drawing.Color.Black
        Me.LabelTag3.Location = New System.Drawing.Point(20, 81)
        Me.LabelTag3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag3.Name = "LabelTag3"
        Me.LabelTag3.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag3.TabIndex = 48
        Me.LabelTag3.Text = "X"
        '
        'tag2
        '
        Me.tag2.AutoSize = True
        Me.tag2.BackColor = System.Drawing.Color.Gainsboro
        Me.tag2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag2.Location = New System.Drawing.Point(62, 55)
        Me.tag2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag2.Name = "tag2"
        Me.tag2.Size = New System.Drawing.Size(41, 16)
        Me.tag2.TabIndex = 47
        Me.tag2.Text = "Tag02"
        Me.tag2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag2
        '
        Me.LabelTag2.AutoSize = True
        Me.LabelTag2.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag2.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag2.ForeColor = System.Drawing.Color.Black
        Me.LabelTag2.Location = New System.Drawing.Point(20, 51)
        Me.LabelTag2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag2.Name = "LabelTag2"
        Me.LabelTag2.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag2.TabIndex = 46
        Me.LabelTag2.Text = "X"
        '
        'tag1
        '
        Me.tag1.AutoSize = True
        Me.tag1.BackColor = System.Drawing.Color.Gainsboro
        Me.tag1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tag1.Location = New System.Drawing.Point(62, 25)
        Me.tag1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tag1.Name = "tag1"
        Me.tag1.Size = New System.Drawing.Size(41, 16)
        Me.tag1.TabIndex = 45
        Me.tag1.Text = "Tag01"
        Me.tag1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelTag1
        '
        Me.LabelTag1.AutoSize = True
        Me.LabelTag1.BackColor = System.Drawing.Color.Gainsboro
        Me.LabelTag1.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.LabelTag1.ForeColor = System.Drawing.Color.Black
        Me.LabelTag1.Location = New System.Drawing.Point(20, 21)
        Me.LabelTag1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTag1.Name = "LabelTag1"
        Me.LabelTag1.Size = New System.Drawing.Size(19, 18)
        Me.LabelTag1.TabIndex = 44
        Me.LabelTag1.Text = "X"
        '
        'Lbl_LineNum
        '
        Me.Lbl_LineNum.AutoSize = True
        Me.Lbl_LineNum.Location = New System.Drawing.Point(7, 65)
        Me.Lbl_LineNum.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_LineNum.Name = "Lbl_LineNum"
        Me.Lbl_LineNum.Size = New System.Drawing.Size(39, 15)
        Me.Lbl_LineNum.TabIndex = 72
        Me.Lbl_LineNum.Text = "Line #"
        '
        'Lbl_LineNumVal
        '
        Me.Lbl_LineNumVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Lbl_LineNumVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_LineNumVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_LineNumVal.Location = New System.Drawing.Point(67, 61)
        Me.Lbl_LineNumVal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_LineNumVal.Name = "Lbl_LineNumVal"
        Me.Lbl_LineNumVal.Size = New System.Drawing.Size(142, 22)
        Me.Lbl_LineNumVal.TabIndex = 73
        Me.Lbl_LineNumVal.Text = "<NA>"
        Me.Lbl_LineNumVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lbl_FindTitle
        '
        Me.Lbl_FindTitle.AutoSize = True
        Me.Lbl_FindTitle.Location = New System.Drawing.Point(10, 16)
        Me.Lbl_FindTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_FindTitle.Name = "Lbl_FindTitle"
        Me.Lbl_FindTitle.Size = New System.Drawing.Size(79, 15)
        Me.Lbl_FindTitle.TabIndex = 74
        Me.Lbl_FindTitle.Text = "String to find:"
        '
        'Pnl_Find
        '
        Me.Pnl_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pnl_Find.Controls.Add(Me.Btn_FindPrev)
        Me.Pnl_Find.Controls.Add(Me.Btn_FindNext)
        Me.Pnl_Find.Controls.Add(Me.Btn_FindFirst)
        Me.Pnl_Find.Controls.Add(Me.Txt_SearchText)
        Me.Pnl_Find.Controls.Add(Me.Lbl_FindTitle)
        Me.Pnl_Find.Location = New System.Drawing.Point(14, 363)
        Me.Pnl_Find.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Pnl_Find.Name = "Pnl_Find"
        Me.Pnl_Find.Size = New System.Drawing.Size(1227, 48)
        Me.Pnl_Find.TabIndex = 75
        '
        'Btn_FindPrev
        '
        Me.Btn_FindPrev.Enabled = False
        Me.Btn_FindPrev.Location = New System.Drawing.Point(1137, 7)
        Me.Btn_FindPrev.Name = "Btn_FindPrev"
        Me.Btn_FindPrev.Size = New System.Drawing.Size(80, 33)
        Me.Btn_FindPrev.TabIndex = 78
        Me.Btn_FindPrev.Text = "< Find Prev"
        Me.Btn_FindPrev.UseVisualStyleBackColor = True
        '
        'Btn_FindNext
        '
        Me.Btn_FindNext.Location = New System.Drawing.Point(1049, 7)
        Me.Btn_FindNext.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_FindNext.Name = "Btn_FindNext"
        Me.Btn_FindNext.Size = New System.Drawing.Size(80, 33)
        Me.Btn_FindNext.TabIndex = 77
        Me.Btn_FindNext.Text = "Find Next >"
        Me.Btn_FindNext.UseVisualStyleBackColor = True
        '
        'Btn_FindFirst
        '
        Me.Btn_FindFirst.Location = New System.Drawing.Point(961, 7)
        Me.Btn_FindFirst.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_FindFirst.Name = "Btn_FindFirst"
        Me.Btn_FindFirst.Size = New System.Drawing.Size(80, 33)
        Me.Btn_FindFirst.TabIndex = 76
        Me.Btn_FindFirst.Text = "Find First"
        Me.Btn_FindFirst.UseVisualStyleBackColor = True
        '
        'Txt_SearchText
        '
        Me.Txt_SearchText.ContextMenuStrip = Me.CM_Search
        Me.Txt_SearchText.Location = New System.Drawing.Point(94, 12)
        Me.Txt_SearchText.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Txt_SearchText.Name = "Txt_SearchText"
        Me.Txt_SearchText.Size = New System.Drawing.Size(857, 23)
        Me.Txt_SearchText.TabIndex = 75
        '
        'CM_Search
        '
        Me.CM_Search.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CM_SearchUndo, Me.ToolStripSeparator1, Me.CM_SearchCut, Me.CM_SearchCopy, Me.CM_SearchPaste})
        Me.CM_Search.Name = "ContextMenuStrip1"
        Me.CM_Search.Size = New System.Drawing.Size(114, 98)
        Me.CM_Search.Text = "Search"
        '
        'CM_SearchUndo
        '
        Me.CM_SearchUndo.Name = "CM_SearchUndo"
        Me.CM_SearchUndo.ShortcutKeyDisplayString = " "
        Me.CM_SearchUndo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.CM_SearchUndo.Size = New System.Drawing.Size(113, 22)
        Me.CM_SearchUndo.Text = "Undo"
        Me.CM_SearchUndo.ToolTipText = "Undo the last operation in the" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Search Box"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(110, 6)
        '
        'CM_SearchCut
        '
        Me.CM_SearchCut.Name = "CM_SearchCut"
        Me.CM_SearchCut.ShortcutKeyDisplayString = " "
        Me.CM_SearchCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CM_SearchCut.Size = New System.Drawing.Size(113, 22)
        Me.CM_SearchCut.Text = "Cut"
        Me.CM_SearchCut.ToolTipText = "Cut text from the Search Box"
        '
        'CM_SearchCopy
        '
        Me.CM_SearchCopy.Name = "CM_SearchCopy"
        Me.CM_SearchCopy.ShortcutKeyDisplayString = " "
        Me.CM_SearchCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CM_SearchCopy.Size = New System.Drawing.Size(113, 22)
        Me.CM_SearchCopy.Text = "Copy"
        Me.CM_SearchCopy.ToolTipText = "Copy Text from the Search Box"
        '
        'CM_SearchPaste
        '
        Me.CM_SearchPaste.Name = "CM_SearchPaste"
        Me.CM_SearchPaste.ShortcutKeyDisplayString = " "
        Me.CM_SearchPaste.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.CM_SearchPaste.Size = New System.Drawing.Size(113, 22)
        Me.CM_SearchPaste.Text = "Paste"
        Me.CM_SearchPaste.ToolTipText = "Paste text into the Search Box"
        '
        'Btn_Led
        '
        Me.Btn_Led.BackColor = System.Drawing.Color.DarkOrange
        Me.Btn_Led.Enabled = False
        Me.Btn_Led.Location = New System.Drawing.Point(182, 8)
        Me.Btn_Led.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_Led.Name = "Btn_Led"
        Me.Btn_Led.Size = New System.Drawing.Size(27, 17)
        Me.Btn_Led.TabIndex = 76
        Me.Btn_Led.Text = "-"
        Me.Btn_Led.UseVisualStyleBackColor = False
        '
        'Btn_SetFont
        '
        Me.Btn_SetFont.Enabled = False
        Me.Btn_SetFont.Location = New System.Drawing.Point(1261, 389)
        Me.Btn_SetFont.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_SetFont.Name = "Btn_SetFont"
        Me.Btn_SetFont.Size = New System.Drawing.Size(93, 23)
        Me.Btn_SetFont.TabIndex = 77
        Me.Btn_SetFont.Text = "Set Desc. Font"
        Me.Btn_SetFont.UseVisualStyleBackColor = True
        '
        'Btn_Marker1
        '
        Me.Btn_Marker1.Location = New System.Drawing.Point(7, 297)
        Me.Btn_Marker1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_Marker1.Name = "Btn_Marker1"
        Me.Btn_Marker1.Size = New System.Drawing.Size(93, 24)
        Me.Btn_Marker1.TabIndex = 78
        Me.Btn_Marker1.Text = "Marker1"
        Me.Btn_Marker1.UseVisualStyleBackColor = True
        '
        'Btn_Marker2
        '
        Me.Btn_Marker2.Location = New System.Drawing.Point(115, 297)
        Me.Btn_Marker2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_Marker2.Name = "Btn_Marker2"
        Me.Btn_Marker2.Size = New System.Drawing.Size(93, 24)
        Me.Btn_Marker2.TabIndex = 79
        Me.Btn_Marker2.Text = "Marker2"
        Me.Btn_Marker2.UseVisualStyleBackColor = True
        '
        'Btn_Marker3
        '
        Me.Btn_Marker3.Location = New System.Drawing.Point(6, 325)
        Me.Btn_Marker3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_Marker3.Name = "Btn_Marker3"
        Me.Btn_Marker3.Size = New System.Drawing.Size(93, 24)
        Me.Btn_Marker3.TabIndex = 80
        Me.Btn_Marker3.Text = "Marker3"
        Me.Btn_Marker3.UseVisualStyleBackColor = True
        '
        'Btn_Marker4
        '
        Me.Btn_Marker4.Location = New System.Drawing.Point(115, 325)
        Me.Btn_Marker4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_Marker4.Name = "Btn_Marker4"
        Me.Btn_Marker4.Size = New System.Drawing.Size(93, 24)
        Me.Btn_Marker4.TabIndex = 81
        Me.Btn_Marker4.Text = "Marker4"
        Me.Btn_Marker4.UseVisualStyleBackColor = True
        '
        'Pnl_Data
        '
        Me.Pnl_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pnl_Data.Controls.Add(Me.Lbl_LineEndVal)
        Me.Pnl_Data.Controls.Add(Me.Lbl_LineStartVal)
        Me.Pnl_Data.Controls.Add(Me.Lbl_LineEndTitle)
        Me.Pnl_Data.Controls.Add(Me.Lbl_LineStartTitle)
        Me.Pnl_Data.Controls.Add(Me.Lbl_DataPanelTitle)
        Me.Pnl_Data.Controls.Add(Me.Btn_Marker4)
        Me.Pnl_Data.Controls.Add(Me.Btn_Marker3)
        Me.Pnl_Data.Controls.Add(Me.Btn_Marker2)
        Me.Pnl_Data.Controls.Add(Me.Btn_Marker1)
        Me.Pnl_Data.Controls.Add(Me.Btn_Led)
        Me.Pnl_Data.Controls.Add(Me.Lbl_LineNumVal)
        Me.Pnl_Data.Controls.Add(Me.Lbl_LineNum)
        Me.Pnl_Data.Controls.Add(Me.Lbl_NumTagsVal)
        Me.Pnl_Data.Controls.Add(Me.Lbl_NumTagsTitle)
        Me.Pnl_Data.Controls.Add(Me.Lbl_RowEndVal)
        Me.Pnl_Data.Controls.Add(Me.Lbl_RowEndTitle)
        Me.Pnl_Data.Controls.Add(Me.Lbl_CursorPosVal)
        Me.Pnl_Data.Controls.Add(Me.Lbl_CursorPosTitle)
        Me.Pnl_Data.Controls.Add(Me.Lbl_RowStartVal)
        Me.Pnl_Data.Controls.Add(Me.Lbl_SecEndVal)
        Me.Pnl_Data.Controls.Add(Me.Lbl_SecStartVal)
        Me.Pnl_Data.Controls.Add(Me.Lbl_RowStartTitle)
        Me.Pnl_Data.Controls.Add(Me.Lbl_SecEndTitle)
        Me.Pnl_Data.Controls.Add(Me.Lbl_SecStartTitle)
        Me.Pnl_Data.Location = New System.Drawing.Point(1253, 29)
        Me.Pnl_Data.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Pnl_Data.Name = "Pnl_Data"
        Me.Pnl_Data.Size = New System.Drawing.Size(219, 354)
        Me.Pnl_Data.TabIndex = 85
        '
        'Lbl_LineEndVal
        '
        Me.Lbl_LineEndVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Lbl_LineEndVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_LineEndVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_LineEndVal.Location = New System.Drawing.Point(67, 119)
        Me.Lbl_LineEndVal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_LineEndVal.Name = "Lbl_LineEndVal"
        Me.Lbl_LineEndVal.Size = New System.Drawing.Size(142, 22)
        Me.Lbl_LineEndVal.TabIndex = 86
        Me.Lbl_LineEndVal.Text = "<NA>"
        Me.Lbl_LineEndVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lbl_LineStartVal
        '
        Me.Lbl_LineStartVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Lbl_LineStartVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_LineStartVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_LineStartVal.Location = New System.Drawing.Point(67, 90)
        Me.Lbl_LineStartVal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_LineStartVal.Name = "Lbl_LineStartVal"
        Me.Lbl_LineStartVal.Size = New System.Drawing.Size(142, 22)
        Me.Lbl_LineStartVal.TabIndex = 85
        Me.Lbl_LineStartVal.Text = "<NA>"
        Me.Lbl_LineStartVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lbl_LineEndTitle
        '
        Me.Lbl_LineEndTitle.AutoSize = True
        Me.Lbl_LineEndTitle.Location = New System.Drawing.Point(7, 123)
        Me.Lbl_LineEndTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_LineEndTitle.Name = "Lbl_LineEndTitle"
        Me.Lbl_LineEndTitle.Size = New System.Drawing.Size(52, 15)
        Me.Lbl_LineEndTitle.TabIndex = 84
        Me.Lbl_LineEndTitle.Text = "Line End"
        '
        'Lbl_LineStartTitle
        '
        Me.Lbl_LineStartTitle.AutoSize = True
        Me.Lbl_LineStartTitle.Location = New System.Drawing.Point(7, 94)
        Me.Lbl_LineStartTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbl_LineStartTitle.Name = "Lbl_LineStartTitle"
        Me.Lbl_LineStartTitle.Size = New System.Drawing.Size(56, 15)
        Me.Lbl_LineStartTitle.TabIndex = 83
        Me.Lbl_LineStartTitle.Text = "Line Start"
        '
        'Lbl_DataPanelTitle
        '
        Me.Lbl_DataPanelTitle.AutoSize = True
        Me.Lbl_DataPanelTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_DataPanelTitle.Location = New System.Drawing.Point(97, 3)
        Me.Lbl_DataPanelTitle.Name = "Lbl_DataPanelTitle"
        Me.Lbl_DataPanelTitle.Size = New System.Drawing.Size(33, 15)
        Me.Lbl_DataPanelTitle.TabIndex = 82
        Me.Lbl_DataPanelTitle.Text = "Data"
        '
        'Btn_NextLine
        '
        Me.Btn_NextLine.Location = New System.Drawing.Point(881, 29)
        Me.Btn_NextLine.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_NextLine.Name = "Btn_NextLine"
        Me.Btn_NextLine.Size = New System.Drawing.Size(56, 25)
        Me.Btn_NextLine.TabIndex = 86
        Me.Btn_NextLine.Tag = "1"
        Me.Btn_NextLine.Text = "Next Ln"
        Me.Btn_NextLine.UseVisualStyleBackColor = True
        '
        'Btn_Next10Lines
        '
        Me.Btn_Next10Lines.Location = New System.Drawing.Point(1004, 29)
        Me.Btn_Next10Lines.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_Next10Lines.Name = "Btn_Next10Lines"
        Me.Btn_Next10Lines.Size = New System.Drawing.Size(56, 25)
        Me.Btn_Next10Lines.TabIndex = 87
        Me.Btn_Next10Lines.Tag = "10"
        Me.Btn_Next10Lines.Text = ">10 L"
        Me.Btn_Next10Lines.UseVisualStyleBackColor = True
        '
        'Btn_Next100Lines
        '
        Me.Btn_Next100Lines.Location = New System.Drawing.Point(1127, 29)
        Me.Btn_Next100Lines.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_Next100Lines.Name = "Btn_Next100Lines"
        Me.Btn_Next100Lines.Size = New System.Drawing.Size(56, 25)
        Me.Btn_Next100Lines.TabIndex = 88
        Me.Btn_Next100Lines.Tag = "100"
        Me.Btn_Next100Lines.Text = "> 100 L"
        Me.Btn_Next100Lines.UseVisualStyleBackColor = True
        '
        'Lbl_TagPanelTitle
        '
        Me.Lbl_TagPanelTitle.AutoSize = True
        Me.Lbl_TagPanelTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_TagPanelTitle.Location = New System.Drawing.Point(415, 432)
        Me.Lbl_TagPanelTitle.Name = "Lbl_TagPanelTitle"
        Me.Lbl_TagPanelTitle.Size = New System.Drawing.Size(168, 15)
        Me.Lbl_TagPanelTitle.TabIndex = 89
        Me.Lbl_TagPanelTitle.Text = "XML Tags in the Current Row"
        '
        'Lbl_TextBoxTitle1
        '
        Me.Lbl_TextBoxTitle1.AutoSize = True
        Me.Lbl_TextBoxTitle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_TextBoxTitle1.Location = New System.Drawing.Point(1067, 415)
        Me.Lbl_TextBoxTitle1.Name = "Lbl_TextBoxTitle1"
        Me.Lbl_TextBoxTitle1.Size = New System.Drawing.Size(101, 15)
        Me.Lbl_TextBoxTitle1.TabIndex = 90
        Me.Lbl_TextBoxTitle1.Text = "Desc. Title Line 1"
        Me.Lbl_TextBoxTitle1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Lbl_ImageTitle
        '
        Me.Lbl_ImageTitle.AutoSize = True
        Me.Lbl_ImageTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_ImageTitle.Location = New System.Drawing.Point(741, 432)
        Me.Lbl_ImageTitle.Name = "Lbl_ImageTitle"
        Me.Lbl_ImageTitle.Size = New System.Drawing.Size(42, 15)
        Me.Lbl_ImageTitle.TabIndex = 91
        Me.Lbl_ImageTitle.Text = "Image"
        '
        'Status_Strip1
        '
        Me.Status_Strip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Status_RowTypeTitle, Me.Status_RowTypeVal, Me.Status_FileDirtyTitle, Me.Status_FileDirtyVal, Me.Status_LinesTitle, Me.Status_LinesVal, Me.Status_CharsTitle, Me.Status_CharsVal})
        Me.Status_Strip1.Location = New System.Drawing.Point(0, 839)
        Me.Status_Strip1.Name = "Status_Strip1"
        Me.Status_Strip1.ShowItemToolTips = True
        Me.Status_Strip1.Size = New System.Drawing.Size(1484, 22)
        Me.Status_Strip1.TabIndex = 92
        Me.Status_Strip1.Text = "StatusStrip1"
        '
        'Status_RowTypeTitle
        '
        Me.Status_RowTypeTitle.BackColor = System.Drawing.Color.Transparent
        Me.Status_RowTypeTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Status_RowTypeTitle.Name = "Status_RowTypeTitle"
        Me.Status_RowTypeTitle.Size = New System.Drawing.Size(64, 17)
        Me.Status_RowTypeTitle.Text = "Row Type:"
        '
        'Status_RowTypeVal
        '
        Me.Status_RowTypeVal.AutoSize = False
        Me.Status_RowTypeVal.AutoToolTip = True
        Me.Status_RowTypeVal.BackColor = System.Drawing.Color.Transparent
        Me.Status_RowTypeVal.Name = "Status_RowTypeVal"
        Me.Status_RowTypeVal.Size = New System.Drawing.Size(100, 17)
        Me.Status_RowTypeVal.Text = "ODF Start-Tag"
        Me.Status_RowTypeVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Status_RowTypeVal.ToolTipText = resources.GetString("Status_RowTypeVal.ToolTipText")
        '
        'Status_FileDirtyTitle
        '
        Me.Status_FileDirtyTitle.BackColor = System.Drawing.Color.Transparent
        Me.Status_FileDirtyTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Status_FileDirtyTitle.Name = "Status_FileDirtyTitle"
        Me.Status_FileDirtyTitle.Size = New System.Drawing.Size(200, 17)
        Me.Status_FileDirtyTitle.Text = "      ODF Modified from Saved Copy"
        '
        'Status_FileDirtyVal
        '
        Me.Status_FileDirtyVal.AutoSize = False
        Me.Status_FileDirtyVal.AutoToolTip = True
        Me.Status_FileDirtyVal.BackColor = System.Drawing.Color.Orange
        Me.Status_FileDirtyVal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Status_FileDirtyVal.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Status_FileDirtyVal.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.Status_FileDirtyVal.Name = "Status_FileDirtyVal"
        Me.Status_FileDirtyVal.Size = New System.Drawing.Size(25, 17)
        Me.Status_FileDirtyVal.Text = "      "
        Me.Status_FileDirtyVal.ToolTipText = resources.GetString("Status_FileDirtyVal.ToolTipText")
        '
        'Status_LinesTitle
        '
        Me.Status_LinesTitle.BackColor = System.Drawing.Color.Transparent
        Me.Status_LinesTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Status_LinesTitle.Name = "Status_LinesTitle"
        Me.Status_LinesTitle.Size = New System.Drawing.Size(90, 17)
        Me.Status_LinesTitle.Text = "    Lines in ODF:"
        '
        'Status_LinesVal
        '
        Me.Status_LinesVal.AutoSize = False
        Me.Status_LinesVal.BackColor = System.Drawing.Color.Transparent
        Me.Status_LinesVal.Name = "Status_LinesVal"
        Me.Status_LinesVal.Size = New System.Drawing.Size(55, 17)
        Me.Status_LinesVal.Text = "<NA>"
        Me.Status_LinesVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Status_LinesVal.ToolTipText = "Number of Lines in a loaded ODF."
        '
        'Status_CharsTitle
        '
        Me.Status_CharsTitle.BackColor = System.Drawing.Color.Transparent
        Me.Status_CharsTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Status_CharsTitle.Name = "Status_CharsTitle"
        Me.Status_CharsTitle.Size = New System.Drawing.Size(121, 17)
        Me.Status_CharsTitle.Text = "    Characters in ODF:"
        '
        'Status_CharsVal
        '
        Me.Status_CharsVal.AutoSize = False
        Me.Status_CharsVal.BackColor = System.Drawing.Color.Transparent
        Me.Status_CharsVal.Name = "Status_CharsVal"
        Me.Status_CharsVal.Size = New System.Drawing.Size(70, 17)
        Me.Status_CharsVal.Text = "<NA>"
        Me.Status_CharsVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Status_CharsVal.ToolTipText = "Number of characters in a loaded ODF."
        '
        'Lbl_XMLRowTitle
        '
        Me.Lbl_XMLRowTitle.AutoSize = True
        Me.Lbl_XMLRowTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_XMLRowTitle.Location = New System.Drawing.Point(14, 289)
        Me.Lbl_XMLRowTitle.Name = "Lbl_XMLRowTitle"
        Me.Lbl_XMLRowTitle.Size = New System.Drawing.Size(105, 15)
        Me.Lbl_XMLRowTitle.TabIndex = 93
        Me.Lbl_XMLRowTitle.Text = "XML Row/Record"
        '
        'Lbl_FontSize
        '
        Me.Lbl_FontSize.AutoSize = True
        Me.Lbl_FontSize.Location = New System.Drawing.Point(745, 34)
        Me.Lbl_FontSize.Name = "Lbl_FontSize"
        Me.Lbl_FontSize.Size = New System.Drawing.Size(83, 15)
        Me.Lbl_FontSize.TabIndex = 94
        Me.Lbl_FontSize.Text = "ODF Font Size:"
        '
        'Lbl_ODFTitle
        '
        Me.Lbl_ODFTitle.AutoSize = True
        Me.Lbl_ODFTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_ODFTitle.Location = New System.Drawing.Point(521, 38)
        Me.Lbl_ODFTitle.Name = "Lbl_ODFTitle"
        Me.Lbl_ODFTitle.Size = New System.Drawing.Size(210, 15)
        Me.Lbl_ODFTitle.TabIndex = 95
        Me.Lbl_ODFTitle.Text = "Organ Defintion File (ODF) XML Text"
        '
        'Lbl_TextBoxTitle2
        '
        Me.Lbl_TextBoxTitle2.AutoSize = True
        Me.Lbl_TextBoxTitle2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_TextBoxTitle2.Location = New System.Drawing.Point(1067, 434)
        Me.Lbl_TextBoxTitle2.Name = "Lbl_TextBoxTitle2"
        Me.Lbl_TextBoxTitle2.Size = New System.Drawing.Size(91, 15)
        Me.Lbl_TextBoxTitle2.TabIndex = 96
        Me.Lbl_TextBoxTitle2.Text = "Title2 / Legend"
        Me.Lbl_TextBoxTitle2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Btn_PrevLine
        '
        Me.Btn_PrevLine.Location = New System.Drawing.Point(940, 29)
        Me.Btn_PrevLine.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_PrevLine.Name = "Btn_PrevLine"
        Me.Btn_PrevLine.Size = New System.Drawing.Size(56, 25)
        Me.Btn_PrevLine.TabIndex = 97
        Me.Btn_PrevLine.Tag = "-1"
        Me.Btn_PrevLine.Text = "Prev Ln"
        Me.Btn_PrevLine.UseVisualStyleBackColor = True
        '
        'Btn_Prev10Lines
        '
        Me.Btn_Prev10Lines.Location = New System.Drawing.Point(1063, 29)
        Me.Btn_Prev10Lines.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_Prev10Lines.Name = "Btn_Prev10Lines"
        Me.Btn_Prev10Lines.Size = New System.Drawing.Size(56, 25)
        Me.Btn_Prev10Lines.TabIndex = 98
        Me.Btn_Prev10Lines.Tag = "-10"
        Me.Btn_Prev10Lines.Text = "< 10 L"
        Me.Btn_Prev10Lines.UseVisualStyleBackColor = True
        '
        'Btn_Prev100Lines
        '
        Me.Btn_Prev100Lines.Location = New System.Drawing.Point(1186, 29)
        Me.Btn_Prev100Lines.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_Prev100Lines.Name = "Btn_Prev100Lines"
        Me.Btn_Prev100Lines.Size = New System.Drawing.Size(56, 25)
        Me.Btn_Prev100Lines.TabIndex = 99
        Me.Btn_Prev100Lines.Tag = "-100"
        Me.Btn_Prev100Lines.Text = "< 100 L"
        Me.Btn_Prev100Lines.UseVisualStyleBackColor = True
        '
        'PrintDialog1
        '
        Me.PrintDialog1.Document = Me.PrintDocument1
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        Me.PrintDocument1.DocumentName = "AECHODescText"
        '
        'FntDial_Desc
        '
        Me.FntDial_Desc.ShowApply = True
        Me.FntDial_Desc.ShowColor = True
        '
        'MAIN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(1484, 861)
        Me.Controls.Add(Me.Btn_Prev100Lines)
        Me.Controls.Add(Me.Btn_Prev10Lines)
        Me.Controls.Add(Me.Btn_PrevLine)
        Me.Controls.Add(Me.Lbl_TextBoxTitle2)
        Me.Controls.Add(Me.Lbl_ODFTitle)
        Me.Controls.Add(Me.Lbl_FontSize)
        Me.Controls.Add(Me.Lbl_XMLRowTitle)
        Me.Controls.Add(Me.Status_Strip1)
        Me.Controls.Add(Me.Lbl_ImageTitle)
        Me.Controls.Add(Me.Lbl_TextBoxTitle1)
        Me.Controls.Add(Me.Lbl_TagPanelTitle)
        Me.Controls.Add(Me.Btn_Next100Lines)
        Me.Controls.Add(Me.Btn_Next10Lines)
        Me.Controls.Add(Me.Btn_NextLine)
        Me.Controls.Add(Me.Pnl_Data)
        Me.Controls.Add(Me.Btn_SetFont)
        Me.Controls.Add(Me.Pnl_Find)
        Me.Controls.Add(Me.Pnl_Tags)
        Me.Controls.Add(Me.Btn_SaveDescText)
        Me.Controls.Add(Me.Rtb_XMLRow)
        Me.Controls.Add(Me.Rtb_DescText)
        Me.Controls.Add(Me.Lbl_SectionName)
        Me.Controls.Add(Me.Lbl_SectionTitle)
        Me.Controls.Add(Me.Num_ODFFontSize)
        Me.Controls.Add(Me.Rtb_ODF)
        Me.Controls.Add(Me.Menu_Strip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.Menu_Strip
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "MAIN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AECHO : Hauptwerk Organ Analyzer , Version 1.62.X"
        Me.Menu_Strip.ResumeLayout(False)
        Me.Menu_Strip.PerformLayout()
        Me.CM_ODF.ResumeLayout(False)
        Me.CM_XML.ResumeLayout(False)
        CType(Me.Num_ODFFontSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CM_Desc.ResumeLayout(False)
        Me.Pnl_Tags.ResumeLayout(False)
        Me.Pnl_Tags.PerformLayout()
        Me.Pnl_Find.ResumeLayout(False)
        Me.Pnl_Find.PerformLayout()
        Me.CM_Search.ResumeLayout(False)
        Me.Pnl_Data.ResumeLayout(False)
        Me.Pnl_Data.PerformLayout()
        Me.Status_Strip1.ResumeLayout(False)
        Me.Status_Strip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Menu_Strip As System.Windows.Forms.MenuStrip
    Friend WithEvents Menu_File As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_OpenHauptwerkOrgan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_Quit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Rtb_ODF As System.Windows.Forms.RichTextBox
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Num_ODFFontSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents Lbl_SecStartTitle As System.Windows.Forms.Label
    Friend WithEvents Lbl_SecEndTitle As System.Windows.Forms.Label
    Friend WithEvents Lbl_RowStartTitle As System.Windows.Forms.Label
    Friend WithEvents Lbl_SecStartVal As System.Windows.Forms.Label
    Friend WithEvents Lbl_SecEndVal As System.Windows.Forms.Label
    Friend WithEvents Lbl_RowStartVal As System.Windows.Forms.Label
    Friend WithEvents Lbl_CursorPosTitle As System.Windows.Forms.Label
    Friend WithEvents Lbl_CursorPosVal As System.Windows.Forms.Label
    Friend WithEvents Lbl_RowEndTitle As System.Windows.Forms.Label
    Friend WithEvents Lbl_RowEndVal As System.Windows.Forms.Label
    Friend WithEvents Lbl_SectionTitle As System.Windows.Forms.Label
    Friend WithEvents Lbl_SectionName As System.Windows.Forms.Label
    Friend WithEvents Lbl_NumTagsTitle As System.Windows.Forms.Label
    Friend WithEvents Lbl_NumTagsVal As System.Windows.Forms.Label
    Friend WithEvents Rtb_DescText As System.Windows.Forms.RichTextBox
    Friend WithEvents Rtb_XMLRow As System.Windows.Forms.RichTextBox
    Friend WithEvents Btn_SaveDescText As System.Windows.Forms.Button
    Friend WithEvents Pnl_Tags As System.Windows.Forms.Panel
    Friend WithEvents tag24 As System.Windows.Forms.Label
    Friend WithEvents LabelTag24 As System.Windows.Forms.Label
    Friend WithEvents tag23 As System.Windows.Forms.Label
    Friend WithEvents LabelTag23 As System.Windows.Forms.Label
    Friend WithEvents tag22 As System.Windows.Forms.Label
    Friend WithEvents LabelTag22 As System.Windows.Forms.Label
    Friend WithEvents tag21 As System.Windows.Forms.Label
    Friend WithEvents LabelTag21 As System.Windows.Forms.Label
    Friend WithEvents tag20 As System.Windows.Forms.Label
    Friend WithEvents LabelTag20 As System.Windows.Forms.Label
    Friend WithEvents tag19 As System.Windows.Forms.Label
    Friend WithEvents LabelTag19 As System.Windows.Forms.Label
    Friend WithEvents tag18 As System.Windows.Forms.Label
    Friend WithEvents LabelTag18 As System.Windows.Forms.Label
    Friend WithEvents tag17 As System.Windows.Forms.Label
    Friend WithEvents LabelTag17 As System.Windows.Forms.Label
    Friend WithEvents tag16 As System.Windows.Forms.Label
    Friend WithEvents LabelTag16 As System.Windows.Forms.Label
    Friend WithEvents tag15 As System.Windows.Forms.Label
    Friend WithEvents tag14 As System.Windows.Forms.Label
    Friend WithEvents LabelTag15 As System.Windows.Forms.Label
    Friend WithEvents LabelTag14 As System.Windows.Forms.Label
    Friend WithEvents tag13 As System.Windows.Forms.Label
    Friend WithEvents LabelTag13 As System.Windows.Forms.Label
    Friend WithEvents tag12 As System.Windows.Forms.Label
    Friend WithEvents LabelTag12 As System.Windows.Forms.Label
    Friend WithEvents tag11 As System.Windows.Forms.Label
    Friend WithEvents LabelTag11 As System.Windows.Forms.Label
    Friend WithEvents tag10 As System.Windows.Forms.Label
    Friend WithEvents LabelTag10 As System.Windows.Forms.Label
    Friend WithEvents tag9 As System.Windows.Forms.Label
    Friend WithEvents LabelTag9 As System.Windows.Forms.Label
    Friend WithEvents tag8 As System.Windows.Forms.Label
    Friend WithEvents LabelTag8 As System.Windows.Forms.Label
    Friend WithEvents tag7 As System.Windows.Forms.Label
    Friend WithEvents LabelTag7 As System.Windows.Forms.Label
    Friend WithEvents tag6 As System.Windows.Forms.Label
    Friend WithEvents tag5 As System.Windows.Forms.Label
    Friend WithEvents LabelTag6 As System.Windows.Forms.Label
    Friend WithEvents LabelTag5 As System.Windows.Forms.Label
    Friend WithEvents tag4 As System.Windows.Forms.Label
    Friend WithEvents LabelTag4 As System.Windows.Forms.Label
    Friend WithEvents tag3 As System.Windows.Forms.Label
    Friend WithEvents LabelTag3 As System.Windows.Forms.Label
    Friend WithEvents tag2 As System.Windows.Forms.Label
    Friend WithEvents LabelTag2 As System.Windows.Forms.Label
    Friend WithEvents tag1 As System.Windows.Forms.Label
    Friend WithEvents LabelTag1 As System.Windows.Forms.Label
    Friend WithEvents Menu_HelpAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_Help As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Lbl_LineNum As System.Windows.Forms.Label
    Friend WithEvents Lbl_LineNumVal As System.Windows.Forms.Label
    Friend WithEvents Menu_Tools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_ClearMarkers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Lbl_FindTitle As System.Windows.Forms.Label
    Friend WithEvents Pnl_Find As System.Windows.Forms.Panel
    Friend WithEvents Txt_SearchText As System.Windows.Forms.TextBox
    Friend WithEvents Btn_FindFirst As System.Windows.Forms.Button
    Friend WithEvents Btn_FindNext As System.Windows.Forms.Button
    Friend WithEvents Btn_Led As System.Windows.Forms.Button
    Friend WithEvents Btn_SetFont As System.Windows.Forms.Button
    Friend WithEvents Menu_EditMode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_EditModeStart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_EditModeExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_ReComputeSections As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Btn_Marker1 As System.Windows.Forms.Button
    Friend WithEvents Btn_Marker2 As System.Windows.Forms.Button
    Friend WithEvents Btn_Marker3 As System.Windows.Forms.Button
    Friend WithEvents Btn_Marker4 As System.Windows.Forms.Button
    Friend WithEvents Btn_RowAction As System.Windows.Forms.Button
    Friend WithEvents Menu_SaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_Sep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Menu_CouplersCode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Pnl_Data As System.Windows.Forms.Panel
    Friend WithEvents Btn_NextLine As System.Windows.Forms.Button
    Friend WithEvents Btn_Next10Lines As System.Windows.Forms.Button
    Friend WithEvents Btn_Next100Lines As System.Windows.Forms.Button
    Friend WithEvents Menu_FollowASample As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_About As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Lbl_TagPanelTitle As Windows.Forms.Label
    Friend WithEvents Lbl_TextBoxTitle1 As Windows.Forms.Label
    Friend WithEvents Lbl_ImageTitle As Windows.Forms.Label
    Friend WithEvents Status_Strip1 As Windows.Forms.StatusStrip
    Friend WithEvents Lbl_XMLRowTitle As Windows.Forms.Label
    Friend WithEvents Lbl_FontSize As Windows.Forms.Label
    Friend WithEvents Btn_FindPrev As Windows.Forms.Button
    Friend WithEvents itl As Windows.Forms.Label
    Friend WithEvents Lbl_ODFTitle As Windows.Forms.Label
    Friend WithEvents Menu_CloseODF As Windows.Forms.ToolStripMenuItem
    Friend WithEvents Lbl_TextBoxTitle2 As Windows.Forms.Label
    Friend WithEvents Btn_PrevLine As Windows.Forms.Button
    Friend WithEvents Btn_Prev10Lines As Windows.Forms.Button
    Friend WithEvents Btn_Prev100Lines As Windows.Forms.Button
    Friend WithEvents Lbl_LocationsPanelTitle As Windows.Forms.Label
    Friend WithEvents Lbl_DataPanelTitle As Windows.Forms.Label
    Friend WithEvents Lbl_LineEndVal As Windows.Forms.Label
    Friend WithEvents Lbl_LineStartVal As Windows.Forms.Label
    Friend WithEvents Lbl_LineEndTitle As Windows.Forms.Label
    Friend WithEvents Lbl_LineStartTitle As Windows.Forms.Label
    Friend WithEvents Status_RowTypeTitle As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Status_RowTypeVal As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Status_FileDirtyTitle As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Status_FileDirtyVal As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Menu_PrintDT As Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_Sep3 As Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintDialog1 As Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents Status_LinesTitle As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Status_LinesVal As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Status_CharsTitle As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Status_CharsVal As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Menu_SectionsA As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_XML As Windows.Forms.ContextMenuStrip
    Friend WithEvents CM_XMLCopy As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_Search As Windows.Forms.ContextMenuStrip
    Friend WithEvents CM_SearchUndo As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CM_SearchCut As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_SearchCopy As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_SearchPaste As Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_ListSections As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_Desc As Windows.Forms.ContextMenuStrip
    Friend WithEvents CM_DescUndo As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_DescRedo As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CM_DescCut As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_DescCopy As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_DescPaste As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CM_DescPrint As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_ODF As Windows.Forms.ContextMenuStrip
    Friend WithEvents CM_ODFUndo As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_ODFRedo As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CM_ODFCut As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_ODFCopy As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CM_ODFPaste As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CM_ODFEditMode As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CM_ODFRecompute As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As Windows.Forms.ToolStripSeparator
    Friend WithEvents CM_DescFont As Windows.Forms.ToolStripMenuItem
    Friend WithEvents FntDial_Desc As Windows.Forms.FontDialog
    Friend WithEvents Menu_Recent As Windows.Forms.ToolStripMenuItem
End Class
