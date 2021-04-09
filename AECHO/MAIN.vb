
Imports System.IO
Public Class MAIN
    '   Version Log
    '   1.058.2 Baseline Vesion
    '   1.059.0     April-07-2021   Bob Hehmann
    '               1)  Updated Version to 1.059, updated copyright date, corrected version number in Window Title Bar & About Box
    '               2)  Changed ODF I/O to allow UTF-8 compatibilty, useful if including special/extended characters, i.e. adding an umlaut to a Stop Name
    '               3)  Changed "Compilated" to "Compiled" in Title Bar; Made Vesion info in Title Bar & About Box dynamic
    '               4)  Changed Menu Bar text to mixed-case, added ... to "Save As" & "Open Hauptwerk Organ"; Changed text for "Help" to "View Help", "About" to "About AECHO", reflecting standatd practice
    '               5)  Replaced MSgBox implementation of "About" with VS AboutBox object, which takes its content from Project Assembly parameters.
    '               6)  Updated .NET Framework from 3.5 to 4.8 (and tested successfully with .NET 5.0)
    '               7) 	Add AECHO 1.059.0 Project to GIT, to manage source versioning

    Public G_registered As Boolean ' version payante ou demo
    Public G_EditMode As Boolean ' true si actif
    Public G_OrganFile As String
    Public G_OdfLength As Integer ' taille de l'ODF
    Public G_StartPos As Integer ' G_ signifie variable globale
    Public G_EndPos As Integer
    Public G_CaretPos As Integer ' position du curseur
    Public G_LineIndex As Integer
    Public G_LineText As String
    Public G_LineStart As Integer ' position de <o>
    Public G_LineEnd As Integer ' position de </o>
    Public G_SectionStart As Integer
    Public G_SectionEnd As Integer
    Public G_Section As Integer ' N° de la section en cours (1 à 44)
    Public G_AppPath As String ' application Path
    Public G_DataPath As String ' repertoire data
    Public G_InitialDir As String
    Public G_PackagePath As String 'HauptwerkSampleSetsAndComponents/OrganInstallationPackages
    Public G_PackageID As String ' numero à 6 chiffres du dossier d'installation
    Public G_SectionName As String
    Public G_LastSectionName As String
    Public G_RTF_File As String
    Public G_Previous_RTF_File As String
    ' images
    Public G_ImageFile As String
    Public G_ImageSet
    Public G_ImageIndex
    ' Fonction Find
    Public G_TextToFind As String
    Public G_FindStartPosition As Integer
    ' Fonction Follow
    Public G_Item_to_Follow

    ' Structure
    Structure Str_Section
        Public index As Integer
        Public title As String ' tag complet
        Public name As String ' rien que le nom
        Public startPos As Integer
        Public endPos As Integer
        Public len As Integer ' longueur de la section
        Public firstLine As Integer ' n° de la 1ere ligne
        Public titleEnd As Integer ' position de > à la fin du titre
        Public isEmpty As Boolean
        Public firstLineStartPos As Integer ' debut de la premiere ligne
        Public firstLineEndPos As Integer ' fin de la premiere ligne
    End Structure
    Public S_Section(44) As Str_Section

    '
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
    Private Sub MAIN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        G_AppPath = System.IO.Path.GetDirectoryName(
           System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        ' il faut retirer File:\ au début
        G_AppPath = G_AppPath.Substring(6, Len(G_AppPath) - 6)
        G_DataPath = G_AppPath & "\DATA"
        'MessageBox.Show(G_AppPath & vbCrLf & G_DataPath)
        ' Savoir si version enregistre ou non
        Me.Text = "AECHO : HAUPTWERK Organ Analyzer, Version " & My.Application.Info.Version.ToString   ' <1.059.0> Add version ID dynmically to the initial Window Title Bar
        registered_unRegistered()
        Read_InitialDir()                                       ' Establishes G_InitialDir as (possible) location of user's ODF files
        G_SectionName = "_General"                              ' par defaut
        clear_Tags_Panel()
        PBox.Visible = False
        ' ajout version 055
        G_EditMode = False                                      ' on est en mode lecture seule
    End Sub
    Private Sub registered_unRegistered()
        ' Savoir si version enregistre ou non
        ' modifié pour passer en freeware à partir de v1-0-57
        G_registered = True                                     ' As of V1.057, always treat as Registered / Freeware
        Exit Sub
        Dim verpeaux_File = G_DataPath & "\_verpeaux.txt"
        Dim reponse
        If My.Computer.FileSystem.FileExists(verpeaux_File) Then
            ' me donner le choix
            G_registered = True
            reponse = MsgBox("Veux-tu émuler la version demo ?", MsgBoxStyle.YesNo, "OPTION PERSO (vpx_file présent)")
            If reponse = vbYes Then G_registered = False
        Else ' pas de fichier licence verpeaux
            G_registered = False
            ' tester ici si fichier licence.txt existe et est valide
        End If
        ' 
        ' Restriction si unRegistered
        If G_registered = False Then
            Menu_FollowASample.Visible = False
            NumericTextSize.Visible = False
            ButtonLed.Visible = False
        End If
    End Sub

    ' FONCTIONS RICH TEXT BOX
    Public Function FindMyText(ByVal text As String, ByVal start As Integer) As Integer
        ' CHERCHE UN TEXTE DE HAUT EN BAS DANS RTBOX
        ' Initialize the return value to false by default.
        Dim returnValue As Integer = -1

        ' Ensure that a search string has been specified and a valid start point.
        If text.Length > 0 And start >= 0 Then
            ' Obtain the location of the search string in richTextBox.
            Dim indexToText As Integer = RTBox.Find(text, start, RichTextBoxFinds.None)
            ' Determine whether the text was found in richTextBox1.
            If indexToText >= 0 Then returnValue = indexToText
        End If
        Return returnValue
    End Function
    Public Function FindMyTag(ByVal text As String, ByVal start As Integer) As Integer
        ' CHERCHE UN TAG DE HAUT EN BAS DANS RTBOX2 ET RETOURNE SA POSITION
        If RTBox.TextLength = 0 Then Exit Function ' MODIF V058
        ' Initialize the return value to false by default.
        Dim returnValue As Integer = -1

        ' Ensure that a search string has been specified and a valid start point.
        If text.Length > 0 Then
            ' Obtain the location of the search string in richTextBox.
            Dim indexToText As Integer = RTBoxLine.Find(text, start, RichTextBoxFinds.None)
            ' Determine whether the text was found in richTextBox1.
            If indexToText >= 0 Then returnValue = indexToText
        End If
        Return returnValue
    End Function
    Public Function FindReverse(ByVal text As String, ByVal start As Integer) As Integer
        ' CHERCHE UN TEXTE DE BAS EN HAUT
        If RTBox.TextLength = 0 Then Exit Function ' MODIF V058
        ' Initialize the return value to false by default.
        Dim returnValue As Integer = -1
        ' Ensure that a search string has been specified and a valid start point.
        If text.Length > 0 And start >= 0 Then
            ' Obtain the location of the search string in richTextBox1.
            Dim indexToText As Integer = RTBox.Find(text, 1, start, RichTextBoxFinds.Reverse)
            ' Determine whether the text was found in richTextBox1.
            If indexToText >= 0 Then returnValue = indexToText
        End If
        Return returnValue
    End Function
    Public Function ReadTag(ByVal tag As String, ByVal pos As Integer) As String
        ' CHERCHE UN TAG DANS RTBOXLINE ET LIT LE  TEXTE CONTENU DANS LE TAG
        If RTBox.TextLength = 0 Then Exit Function ' MODIF V058
        Dim returnValue As String = ""
        Dim tagStart As Integer
        Dim tagEnd As Integer
        ' ignorer le tag <o>
        If tag = "o" Then Return "" : Exit Function
        ' trouve le tag de début et de fin
        tagStart = FindMyTag("<" & tag & ">", pos) + 2 + Len(tag)
        If tagStart = -1 Then Return "" : Exit Function
        ' trouve le tag de fin
        tagEnd = FindMyTag("</" & tag & ">", tagStart)
        If tagEnd = -1 Then Return "" : Exit Function
        ' lit le contenu du tag
        RTBoxLine.SelectionStart = tagStart
        If tagEnd - tagStart < 1 Then
            MsgBox("May be a trouble in ""readTag"" function")

        End If
        RTBoxLine.SelectionLength = tagEnd - tagStart
        returnValue = RTBoxLine.SelectedText

        ' remplace "" par " "
        If returnValue = "" Then returnValue = " "
        Return returnValue
        'Else
        'debug
        'MsgBox(tag & vbCrLf & "tagStart " & tagStart.ToString & vbCrLf & "tagend " & tagEnd.ToString & vbCrLf & "lineStart " & lineStart.ToString & vbCrLf & "lineEnd " & lineEnd.ToString)
        Return ""
        ' End If
    End Function
    Public Function count_Tags(ByVal lgStart, ByVal lgEnd)
        ' COMPTE LE NOMBRE DE TAGS DANS UNE LIGNE DE RTBOXLine
        Dim count As Single = 0
        Dim idx As Single
        Dim returnValue As Single = 0
        For idx = 0 To 24
            returnValue = FindMyTag("</", lgStart)
            If returnValue = -1 Then Exit For
            lgStart = returnValue + 1
            If returnValue <= lgEnd Then
                count += 1
            Else
                Exit For
            End If
            'MsgBox("lgstart " & lgStart.ToString & vbCrLf & "lgEnd " & lgEnd.ToString & vbCrLf & count.ToString & " tags")
        Next
        LabelNumberOfTags.Text = (count - 1).ToString
        Return count - 1 ' pour ne pas compter "<o>"
    End Function

    ' CONTROLES
    Private Sub NumericTextSize_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericTextSize.ValueChanged
        Dim FontSize = NumericTextSize.Value
        RTBox.SelectAll()
        RTBox.SelectionFont = New Font(New System.Drawing.FontFamily("microsoft sans serif"), FontSize, System.Drawing.FontStyle.Regular)

    End Sub

    ' PROCEDURES RICH TEXT BOX
    Private Sub RTBox_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RTBox.MouseDoubleClick
        If RTBox.TextLength = 0 Then MsgBox("Do not click if the box is empty") : Exit Sub ' MODIF V058

        ' TROUVER L'INDEX SELON POSITION SOURIS
        G_CaretPos = RTBox.GetCharIndexFromPosition(e.Location)
        LabelCaretPos.Text = G_CaretPos.ToString
        ' Trouver et selectionner la ligne
        Get_Line_From_Index(G_CaretPos)
        ' verifie que la ligne appartient bien à la meme section
        Dim sectionHead = FindReverse("<ObjectList", G_CaretPos)
        If G_LineStart < sectionHead Then
            MsgBox("Please, do not click on this line. Try with an other line.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Message from RTBox.DoubleClick") 'La ligne n'est pas dans la meme section
        End If

        ' MODIF VERSION 025
        ' afficher la ligne dans RTBoxLine
        RTBoxLine.Text = G_LineText
        ' retrouver la section auquel il appartient
        Get_Section_From_Index(G_LineStart)
        ' compter les tags de la ligne dans RTBoxLine
        count_Tags(1, RTBoxLine.TextLength)
        ' montrer les infos de la ligne
        display_object(1)
        ' selectionner la ligne
        RTBox.SelectionStart = G_LineStart
        RTBox.SelectionLength = G_LineEnd - G_LineStart
        ' charger le fichier RTF
        load_RTF_File()
    End Sub
    Private Sub Get_Line_From_Index(ByVal index As Integer)
        ' map the character index to a line
        G_LineIndex = RTBox.GetLineFromCharIndex(index)
        ' find out where the line starts
        G_LineStart = RTBox.GetFirstCharIndexFromLine(G_LineIndex)
        ' find out where it ends, which is the start of the next line minus one
        G_LineEnd = RTBox.GetFirstCharIndexFromLine(G_LineIndex + 1) - 1
        If G_LineEnd < G_LineStart Then Exit Sub ' MODIF V058
        ' selectionner la ligne et recuperer le texte
        RTBox.SelectionStart = G_LineStart
        RTBox.SelectionLength = G_LineEnd - G_LineStart
        G_LineText = RTBox.SelectedText
        ' MODIF V058.2
        If G_LineText = "</Hauptwerk>" Then MsgBox("This is the End of your ODF") : Exit Sub

        ' l'objet <0> ... </o> peut etre sur 2 lignes
        Dim line_Ending = G_LineText.Substring(Len(G_LineText) - 4, 4) ' recherche </o>
        Dim line_starting = G_LineText.Substring(0, 3) ' recherche <o>
        If G_SectionName <> "_General" Then
            If line_Ending <> "</o>" Then
                ' trouver fin de ligne
                G_LineEnd = RTBox.GetFirstCharIndexFromLine(G_LineIndex + 2) - 1
                ' selectionner la ligne et recuperer le texte
                RTBox.SelectionStart = G_LineStart
                RTBox.SelectionLength = G_LineEnd - G_LineStart
                G_LineText = RTBox.SelectedText
            End If
            If line_starting <> "<o>" Then
                ' trouver debut de ligne
                G_LineStart = RTBox.GetFirstCharIndexFromLine(G_LineIndex - 1) - 1
                ' selectionner la ligne et recuperer le texte
                RTBox.SelectionStart = G_LineStart
                RTBox.SelectionLength = G_LineEnd - G_LineStart
                G_LineText = RTBox.SelectedText
            End If
        End If



        ' afficher les positions
        LabelLineNumber.Text = G_LineIndex
        LabelLineStart.Text = G_LineStart.ToString
        LabelLineEnd.Text = G_LineEnd.ToString
    End Sub
    Private Sub Get_Section_From_Index(ByVal index)
        ' TROUVE LA SECTION DANS LAQUELLE SE TROUVE LE CARET
        If RTBox.TextLength = 0 Then Exit Sub ' MODIF V058
        Dim belongStart As Integer
        Dim belongend As Integer
        ' memoriser nom de la section precedente
        If G_SectionName <> Nothing Then G_LastSectionName = G_SectionName
        ' retrouver la section auquel il appartient
        belongStart = FindReverse("<ObjectList ObjectType=", index)
        If belongStart = -1 Then MsgBox("Cannot get section for the index " & index.ToString, , "Message from Get_Section_From_Index") : Exit Sub
        belongend = FindMyText(">", belongStart)
        RTBox.Focus()
        RTBox.SelectionStart = belongStart + 24
        RTBox.SelectionLength = belongend - belongStart - 25
        G_SectionName = RTBox.SelectedText
        LabelSection.Text = G_SectionName
    End Sub
    Private Sub RTBox_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RTBox.MouseClick
        ' TROUVER L'INDEX SELON POSITION SOURIS
        ' modif version 055
        G_CaretPos = RTBox.GetCharIndexFromPosition(e.Location)
        LabelCaretPos.Text = G_CaretPos.ToString
        ' Trouver et selectionner la ligne
        Get_Line_From_Index(G_CaretPos)
        ' de-selectionner la ligne si on est en mode edition
        If G_EditMode = True Then
            RTBox.SelectionLength = 0 ' modif v055
        End If

    End Sub

    'FONCTIONS ET PROCEDURES
    Private Sub Get_Section_From_Menu(ByVal section As String)
        ' TROUVER UNE SECTION DE L'ORGUE A PARTIR DU MENU
        ' memoriser nom de la section precedente
        ' MsgBox(G_SectionName & vbCrLf & G_LastSectionName)
        ' If G_SectionName <> Nothing Then G_LastSectionName = G_SectionName
        ' trouver la section active
        Dim idx As Integer
        For idx = 1 To 44
            If S_Section(idx).title = section Then
                G_Section = idx
                ' scroller de façon a avoir le titre de la section en haut de rtbox
                RTBox.Focus()
                RTBox.Select(S_Section(G_Section).startPos, 0)
                RTBox.ScrollToCaret()
                G_CaretPos = RTBox.SelectionStart
                ' trouver et selectionner la 1ere ligne de la section, si la section n'est pas vide
                If S_Section(G_Section).isEmpty = False Then
                    RTBox.Select(S_Section(G_Section).firstLineStartPos, S_Section(G_Section).firstLineEndPos - S_Section(G_Section).firstLineStartPos + 4)
                    G_LineText = RTBox.SelectedText
                Else
                    G_LineText = ""
                End If

                ' afficher infos debut et fin
                LabelSectionStart.Text = S_Section(G_Section).startPos
                LabelSectionEnd.Text = S_Section(G_Section).endPos
                ' afficher le nom de la section
                LabelSection.Text = S_Section(G_Section).name
                G_SectionName = S_Section(G_Section).name
                ' caalculer et afficher n° dee ligne
                G_LineIndex = RTBox.GetLineFromCharIndex(G_CaretPos)
                LabelLineNumber.Text = G_LineIndex.ToString

                If S_Section(G_Section).isEmpty = False Then
                    ' afficher la ligne dans RTBoxLine
                    RTBoxLine.Text = G_LineText
                    ' compter les tags de la ligne dans RTBoxLine
                    count_Tags(1, RTBoxLine.TextLength)
                    ' montrer les infos de la ligne
                    display_object(1)
                Else
                    RTBoxLine.Text = ""
                    clear_Tags_Panel()
                End If

                ' scroller de façon a avoir le titre de la section en haut de rtbox
                RTBox.Focus()
                RTBox.Select(S_Section(G_Section).startPos, 0)
                RTBox.ScrollToCaret()

                ' charger le fichier RTF
                G_SectionName = S_Section(G_Section).name
                'MsgBox(G_SectionName & vbCrLf & G_LastSectionName)
                load_RTF_File()
            End If
        Next

    End Sub
    Private Sub old_Get_Section_From_Menu(ByVal section As String)
        ' TROUVER UNE SECTION DE L'ORGUE A PQRTIR DU MENU
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
        Get_Line_From_Index(G_LineStart)
        ' afficher la ligne dans RTBoxLine
        RTBoxLine.Text = G_LineText
        ' retrouver la section auquel il appartient
        Get_Section_From_Index(G_LineStart)
        ' compter les tags de la ligne dans RTBoxLine
        count_Tags(1, RTBoxLine.TextLength)
        ' montrer les infos de la ligne
        display_object(1)
        ' se positionner sur la ligne avec le nom de la section
        RTBox.SelectionStart = G_SectionStart
        RTBox.SelectionLength = 0

        ' charger le fichier RTF
        load_RTF_File()


        Exit Sub
        ' trouver la premiere ligne objet si elle existe
        Get_First_Line(G_StartPos)
        ' Afficher le nom de la section
        G_SectionName = section.Substring(24, Len(section) - 26)
        LabelSection.Text = G_SectionName
        ' charger le fichier RTF
        load_RTF_File()
    End Sub
    Private Sub Get_First_Line(ByVal startPos)
        ' trouver la premiere ligne objet d'une section si cette ligne existe
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
            display_object(G_LineStart)
        Else
            LabelLineStart.Text = "No line"
            LabelLineEnd.Text = "No line"
            RTBoxLine.Text = "No Item in this section"
        End If
    End Sub
    Private Sub display_Tag_Text(ByRef lastIdx, ByRef counter, ByVal nbTags, ByVal labelTag, ByVal textTag)
        ' AFFICHE DANS LE PANNEL LE TAG ET SON CONTENU
        Dim idx As Single
        Dim txt As String

        If counter < nbTags Then
            ' recherche des tags <a> à <z>
            For idx = lastIdx To 122 + 27
                Select Case idx
                    Case Is <= 122
                        labelTag.Text = Chr(idx)
                        txt = ReadTag(Chr(idx), 1)
                        textTag.Text = txt
                        ' si presence image
                        If G_SectionName = "ImageSet" Then
                            If idx = 99 Then ' Lettre c :  package ID
                                ' le packageID doit avoir 6 chiffres
                                While Len(txt) < 6
                                    txt = "0" & txt
                                End While
                                G_PackageID = txt & "\"
                            End If

                            If idx = 106 Then ' Lettre j : mask de transparence, s'il existe
                                G_ImageFile = txt
                            End If
                        End If
                        If G_SectionName = "ImageSetElement" Then
                            If idx = 97 Then ' Lettre a : image set
                                G_ImageSet = txt
                            End If
                            If idx = 98 Then ' Lettre b : index dans image set
                                G_ImageIndex = txt

                            End If
                            If idx = 100 Then 'Lettre d : bitmap filename
                                G_ImageFile = txt
                                'MsgBox(G_ImageSet & vbCrLf & G_ImageIndex & vbCrLf & G_ImageFile)
                            End If

                        End If

                    Case Is > 122 ' tags <a1>, <b1> ...
                        labelTag.Text = Chr(idx - 26) & "1"
                        txt = ReadTag(Chr(idx - 26) & "1", 1)
                        textTag.Text = txt
                    Case Else
                        txt = ""
                End Select
                If txt <> "" Then lastIdx = idx + 1 : counter += 1 : Exit For
            Next
        Else
            labelTag.Text = "" : textTag.Text = ""
        End If

    End Sub
    Private Sub display_object(ByVal linestart)
        ' AFFICHE LES TAGS ET LEUR CONTENU DANS LE PANEL
        show_Tags_Panel()
        Dim charIdx As Single = 97 ' lettre "a"
        Dim lastIdx As Single = 97
        Dim counter As Single = 0
        Dim nbtags = count_Tags(1, RTBoxLine.TextLength)
        Dim labelTag As Control
        Dim txtTag As Control
        If G_SectionName = "ImageSet" Or G_SectionName = "ImageSetElement" Then
            ButtonDisplayImage.Visible = True
        Else
            ButtonDisplayImage.Visible = False
        End If

        ' tag <a>
        labelTag = LabelTag1 : txtTag = tag1
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <b>
        labelTag = LabelTag2 : txtTag = tag2
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <c>
        labelTag = LabelTag3 : txtTag = tag3
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <d>
        labelTag = LabelTag4 : txtTag = tag4
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <e>
        labelTag = LabelTag5 : txtTag = tag5
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <f>
        labelTag = LabelTag6 : txtTag = tag6
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <g>
        labelTag = LabelTag7 : txtTag = tag7
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <h>
        labelTag = LabelTag8 : txtTag = Tag8
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <i>
        labelTag = LabelTag9 : txtTag = Tag9
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <j>
        labelTag = LabelTag10 : txtTag = tag10
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <k>
        labelTag = LabelTag11 : txtTag = tag11
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <l>
        labelTag = LabelTag12 : txtTag = tag12
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <m>
        labelTag = LabelTag13 : txtTag = tag13
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <n>
        labelTag = LabelTag14 : txtTag = tag14
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <p>
        labelTag = LabelTag15 : txtTag = tag15
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <q>
        labelTag = LabelTag16 : txtTag = tag16
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <r>
        labelTag = LabelTag17 : txtTag = tag17
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <s>
        labelTag = LabelTag18 : txtTag = tag18
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <t>
        labelTag = LabelTag19 : txtTag = Tag19
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <u>
        labelTag = LabelTag20 : txtTag = Tag20
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <v>
        labelTag = LabelTag21 : txtTag = Tag21
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <w>
        labelTag = LabelTag22 : txtTag = Tag22
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <x>
        labelTag = LabelTag23 : txtTag = Tag23
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)
        ' tag <y>
        labelTag = LabelTag24 : txtTag = Tag24
        display_Tag_Text(lastIdx, counter, nbtags, labelTag, txtTag)

        ' se repositionner en debut de ligne et selectionner la ligne
        RTBox.SelectionStart = G_LineStart
        RTBox.SelectionLength = G_LineEnd - G_LineStart + 4 ' 4 pour inclure </o>
        RTBox.Refresh()
    End Sub
    Private Sub load_RTF_File()
        ' CHARGE LE FICHIER RTF DECRIVANT UNE SECTION
        G_RTF_File = G_DataPath & "\" & G_SectionName & ".rtf"
        If G_Previous_RTF_File <> G_RTF_File Then
            If My.Computer.FileSystem.FileExists(G_RTF_File) Then
                RTBoxRTF.LoadFile(G_RTF_File)
            Else
                If My.Computer.FileSystem.FileExists(G_DataPath & "\modele.rtf") Then
                    RTBoxRTF.LoadFile(G_DataPath & "\modele.rtf")
                End If
            End If
            G_Previous_RTF_File = G_RTF_File

            ' Force la police
            Dim fnt As New System.Drawing.Font("verdana", 10)
            RTBoxRTF.Focus()
            RTBoxRTF.Font = fnt
            RTBoxRTF.SelectAll()
            RTBoxRTF.SelectionFont = fnt
            RTBoxRTF.DeselectAll()
        End If
    End Sub
    Private Sub save_RTF_File(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSaveRtbox2.Click
        ' activé par ButtonSaveRtbox2_Click
        If G_RTF_File = G_DataPath & "\help.rtf" Then
            MsgBox("Help file cannot be saved")
            Exit Sub
        End If
        If G_SectionName <> "" Then
            G_RTF_File = G_DataPath & "\" & G_SectionName & ".rtf"
            RTBoxRTF.SaveFile(G_RTF_File)
        End If
    End Sub
    Private Sub clear_Tags_Panel()
        ' VIDE LE PANNEL AFFICHANT LES TAGS
        Dim ctrL As Control
        For Each ctrL In Me.PanelTags.Controls
            ctrL.Visible = False
        Next
    End Sub
    Private Sub show_Tags_Panel()
        '  AFFICHANT LES TAGS
        Dim ctrL As Control
        For Each ctrL In Me.PanelTags.Controls
            ctrL.Visible = True
        Next
    End Sub
    Private Sub get_Sections_Infos(ByVal verbose As Boolean)
        ' EXAMINE L'ODF ET REPERE LES SECTIONS
        ' AFFICHE SI VERBOSE=TRUE
        Dim idx As Integer = 1 ' index
        Dim startPos As Integer = 1
        'Dim retour As Integer
        Const startText = "<ObjectList ObjectType="
        Const endText = "</ObjectList>"
        ButtonLed.BackColor = Color.Red : ButtonLed.Refresh()
        ' vider RTBox_RTF
        RTBoxRTF.Clear()

        ' Boucle
        Do
            ' trouver le tag de début de section
            startPos = RTBox.Find(startText, startPos, RichTextBoxFinds.None)
            If startPos = -1 Then Exit Do
            S_Section(idx).index = idx
            S_Section(idx).startPos = startPos
            'trouver le numero de la 1ere ligne
            S_Section(idx).firstLine = RTBox.GetLineFromCharIndex(S_Section(idx).startPos)
            ' trouver la fin du tag "<ObjectList ObjectType="
            S_Section(idx).titleEnd = RTBox.Find(">", startPos, RichTextBoxFinds.None)

            ' trouver le nom de la section
            RTBox.SelectionStart = startPos + 24
            RTBox.SelectionLength = S_Section(idx).titleEnd - (startPos + 25)
            S_Section(idx).name = RTBox.SelectedText
            ' titre de la section
            S_Section(idx).title = startText & """" & S_Section(idx).name & """>"
            ' trouver la fin de la section
            ' retour = FindMyText(endText, startPos)
            S_Section(idx).endPos = RTBox.Find(endText, startPos, RichTextBoxFinds.None) + 13

            ' recherche sections vides
            S_Section(idx).len = (S_Section(idx).endPos - S_Section(idx).startPos)
            If S_Section(idx).len = Len(startText & endText & S_Section(idx).name) + 4 Then
                S_Section(idx).isEmpty = True
                ' il n'y aura donc pas de 1ere ligne
                S_Section(idx).firstLineStartPos = -1
            Else
                S_Section(idx).isEmpty = False
                ' trouver debut 1ere ligne
                S_Section(idx).firstLineStartPos = S_Section(idx).titleEnd + 1
                ' si la ligne commence bien par <o> ....
                'MsgBox(S_Section(idx).firstLineStartPos.ToString & "      " & RTBox.Find("<o>", S_Section(idx).firstLineStartPos, RichTextBoxFinds.None).ToString)
                'If RTBox.Find("<o>", S_Section(idx).firstLineStartPos, RichTextBoxFinds.None) <> -1 Then
                ' trouver la fin de la 1ere ligne
                S_Section(idx).firstLineEndPos = RTBox.Find("</o>", S_Section(idx).firstLineStartPos, RichTextBoxFinds.None)
                'End If
            End If
            ' colorer le titre de la section
            Color_Sections_Titles(idx, Color.Blue)
            ' verif dans rtbox_rtf
            If verbose = True Then
                RTBoxRTF.Text += vbCrLf & idx.ToString & "    " & S_Section(idx).startPos.ToString & " to  " & S_Section(idx).endPos.ToString & "    " & S_Section(idx).name
            End If
            ' nouvelle position de depart
            startPos = S_Section(idx).endPos + 1
            ' nouvel index
            idx += 1
            Application.DoEvents()
        Loop

        ButtonLed.BackColor = Color.LightGreen : ButtonLed.Refresh()
    End Sub
    Private Sub Color_Sections_Titles(ByVal idx, ByVal color)
        ' COLORIER LE TITRE DE LA SECTION IDX
        RTBox.SelectionStart = S_Section(idx).startPos
        RTBox.SelectionLength = S_Section(idx).titleEnd - S_Section(idx).startPos
        RTBox.SelectionFont = New Font("Arial", 11)
        RTBox.SelectionColor = color
        RTBox.DeselectAll()
    End Sub
    Private Sub nextLine_commun()
        ' Trouver et selectionner la ligne
        Get_Line_From_Index(G_CaretPos)
        ' verifie que la ligne appartient bien à la meme section
        Dim sectionHead = FindReverse("<ObjectList", G_CaretPos)
        If G_LineStart < sectionHead Then
            MsgBox("This line is not in the current section. Please try again.", , "Function Get_line_From_Index") 'La ligne n'est pas dans la meme section
        End If
        ' afficher la ligne dans RTBoxLine
        RTBoxLine.Text = G_LineText
        ' retrouver la section auquel il appartient
        Get_Section_From_Index(G_LineStart)
        ' compter les tags de la ligne dans RTBoxLine
        count_Tags(1, RTBoxLine.TextLength)
        ' montrer les infos de la ligne
        display_object(1)
        ' selectionner la ligne
        RTBox.SelectionStart = G_LineStart
        RTBox.SelectionLength = G_LineEnd - G_LineStart
        ' charger le fichier RTF
        load_RTF_File()
    End Sub

    ' MENU FILES
    Private Sub Menu_OpenHauptwerkOrgan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_OpenHauptwerkOrgan.Click
        '   User selects an organ (ODL/XML) file to be opened. If user cancels the open, AECHO state remains unchanged.
        '   <1.059.0> Modified loading of ODF data to decode using UTF8
        OpenFileDialog.RestoreDirectory = True
        OpenFileDialog.InitialDirectory = G_InitialDir                                          ' "D:\HAUPTWERK\HauptwerkSampleSetsAndComponents\OrganDefinitions"
        'MsgBox("G_InitialDir: " & G_InitialDir)
        OpenFileDialog.Title = "Open one organ"
        OpenFileDialog.DefaultExt = "Organ_Hauptwerk_xml"
        OpenFileDialog.Filter = "organ files (*.Organ_Hauptwerk_xml)|*.Organ_Hauptwerk_xml"     ' <1.059.0> added the closing ) from 1.058b
        OpenFileDialog.FileName = ""                                                            ' MODIF V058

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then                                   ' <1.059.0> extraneous System.Windows.Forms reference removed

            G_OrganFile = OpenFileDialog.FileName

            ' tester longueur du fichier
            Dim file As New FileInfo(G_OrganFile)                                               ' As of 1.057, AECHO is freeware, G_registered set to "True" during initialization
            Dim sizeInBytes As Long = file.Length
            'MsgBox("taille : " & sizeInBytes.ToString)
            If G_registered = False And sizeInBytes > 1024000 Then
                MsgBox("DEMO VERSION " & vbCrLf & "Only ODF smaller than 1 Mb can be opened with this version." & vbCrLf & "(your file is " & sizeInBytes.ToString & ")")
                OpenFileDialog.Dispose()
                Exit Sub
            End If

            Me.Text = "Analyzer/Editor for Compiled Hauptwerk Organs   File = " & G_OrganFile   ' Update the Windows Title Bar. <1.059.0> changed text to "Compiled"

            OpenFileDialog.Dispose()
            ' valider les menus Sections et boutons - enable the menu sections and buttons
            SectionsMenuItem.Enabled = True
            NextSectionsMenuItem.Enabled = True
            Menu_EDITMODE.Enabled = True
            ToolsMenuItem.Enabled = True
            ButtonLed.Enabled = True
            ' effacer
            clear_Tags_Panel()
            PBox.Visible = False
            G_SectionName = ""
            G_LastSectionName = ""
            G_Previous_RTF_File = ""
            ' Copier dans la RichTextBox
            RTBox.Text = My.Computer.FileSystem.ReadAllText(G_OrganFile, System.Text.Encoding.UTF8) ' <1.059.0>, load RTBox, reading ODF file forcing UTF-8 decoding
            'RTBox.LoadFile(G_OrganFile, RichTextBoxStreamType.PlainText)                           ' Pre V1.059 code, assumed non-UTF8 text stream
            RTBox.SelectAll()
            RTBox.SelectionFont = New Font(New System.Drawing.FontFamily("microsoft sans serif"), 10, System.Drawing.FontStyle.Regular)
            RTBox.SelectionLength = 0
            G_OdfLength = RTBox.TextLength
            ' calculer les sections
            Dim verbose = True
            get_Sections_Infos(verbose)
            ' trouver le path pour OrganInstallationPackages
            get_PackagePath(G_OrganFile)

        End If
    End Sub
    Private Sub Menu_SaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_SaveAs.Click
        ' SAUVE L'ODF MODIFIE

        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "Save the ODF as ..."
        saveFileDialog.DefaultExt = "Organ_Hauptwerk_xml"
        saveFileDialog.Filter = "organ files (*.Organ_Hauptwerk_xml)|*.Organ_Hauptwerk_xml"     ' <1.059.0> Added closing )
        saveFileDialog.InitialDirectory = "D:\HAUPTWERK\HauptwerkSampleSetsAndComponents\OrganDefinitions"


        'saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        'saveFileDialog1.FilterIndex = 2
        saveFileDialog.RestoreDirectory = True

        If saveFileDialog.ShowDialog() = DialogResult.OK Then                                   ' <1.059.0> Write out RTBox content to the ODF forcing UTF8 encoding
            Dim UTF8NoBOM = New System.Text.UTF8Encoding(False)                                 ' <1.059.0> Supress insertion of UTF-8 BOM at beginning of text stream
            My.Computer.FileSystem.WriteAllText(saveFileDialog.FileName, RTBox.Text, False, UTF8NoBOM)
            'RTBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText)           ' Pre V1.059 code, wrote file with defaul encoding

            ' myStream = saveFileDialog1.OpenFile()
            ' If (myStream IsNot Nothing) Then
            '' Code to write the stream goes here.
            ' myStream.Close()
            'End If
        End If
    End Sub
    Private Sub Menu_Quit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Quit.Click
        End
    End Sub
    Private Sub get_PackagePath(ByVal path As String)
        ' trouver le path pour OrganInstallationPackages à partir de G_OrganFile
        ' search the path for OrganInstallationPackages from G_OrganFile
        ' for exemple G_OrganFile = "C:\Users\jean-paul\Downloads\Bonoldi Rouen3.Organ_Hauptwerk_xml"
        Dim idx As Integer = 1
        Dim retour As Integer = 0
        ' we search for the last "\", from left to right
        Do
            idx = retour
            retour = InStr(retour + 1, path, "\")
        Loop Until retour = 0
        ' with the exemple of Bonoldi, "\" are found when retour = 3, 9, 19 and 29
        ' then retour = 0 because there is no more "\" in the string
        ' retour = 0 but idx keeps the last positive value found : 29
        ' once retour = 0, we can delete the filename which is the text on the right of the last "\"
        path = path.Remove(idx, Len(path) - idx)
        ' after this action, path = "C:\Users\jean-paul\Downloads\"
        path = path.Remove(Len(path) - Len("organdefinitions\"), Len("organdefinitions\"))
        path += "OrganInstallationPackages\"
        'MsgBox(path)
        G_PackagePath = path
    End Sub
    Private Sub Read_InitialDir()
        ' Executed once when Main Form is loaded. G_InitialDir is set to the (likely) location of the ODF files. If AECHO's DATA directory contains the file initialdir.txt, 
        ' the contents of this file are taken as the default location - initialdir.txt is updated with the location a file is saved to by the "Save As..." menu item.
        ' Dir par defaut
        G_InitialDir = "D:\HAUPTWERK\HauptwerkSampleSetsAndComponents\OrganDefinitions"     ' Default if initial.txt is missing
        Try
            If File.Exists(G_DataPath & "/initialdir.txt") Then
                'MsgBox("initialdir.txt exists: " & G_DataPath)
                Dim sr As StreamReader = New StreamReader(G_DataPath & "/initialdir.txt")
                G_InitialDir = sr.ReadLine
                'MsgBox(G_InitialDir)
                sr.Close()
            End If
        Catch
            '
        End Try
    End Sub

    ' MENU SECTIONS
    Private Sub Menu_DisplayPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_DisplayPage.Click
        Get_Section_From_Menu(section01)
    End Sub
    Private Sub Menu_TextStyle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_TextStyle.Click
        Get_Section_From_Menu(section02)
    End Sub
    Private Sub Menu_TextInstance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_TextInstance.Click
        Get_Section_From_Menu(section03)
    End Sub
    Private Sub Menu_ImageSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ImageSet.Click
        Get_Section_From_Menu(section04)
    End Sub
    Private Sub Menu_ImageSetElement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ImageSetElement.Click
        Get_Section_From_Menu(section05)
    End Sub
    Private Sub Menu_ImageSetInstance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ImageSetInstance.Click
        Get_Section_From_Menu(section06)
    End Sub
    Private Sub Menu_KeyImageSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_KeyImageSet.Click
        Get_Section_From_Menu(section07)
    End Sub
    Private Sub Menu_Division_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Division.Click
        Get_Section_From_Menu(section08)
    End Sub
    Private Sub Menu_DivisionInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_DivisionInput.Click
        Get_Section_From_Menu(section09)
    End Sub
    Private Sub Menu_Switch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Switch.Click
        Get_Section_From_Menu(section10)
    End Sub
    Private Sub Menu_SwitchLinkage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_SwitchLinkage.Click
        Get_Section_From_Menu(section11)
    End Sub
    Private Sub Menu_SwitchExclusiveSelectGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_SwitchExclusiveSelectGroup.Click
        Get_Section_From_Menu(section12)
    End Sub
    Private Sub Menu_SwitchExclusiveSelectGroupElement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_SwitchExclusiveSelectGroupElement.Click
        Get_Section_From_Menu(section13)
    End Sub
    Private Sub Menu_Keyboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Keyboard.Click
        Get_Section_From_Menu(section14)
    End Sub
    Private Sub Menu_KeyboardKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_KeyboardKey.Click
        Get_Section_From_Menu(section15)
    End Sub
    Private Sub Menu_KeyAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_KeyAction.Click
        Get_Section_From_Menu(section16)
    End Sub
    Private Sub Menu_Rank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Rank.Click
        Get_Section_From_Menu(section17)
    End Sub
    Private Sub Menu_ExternalRank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ExternalRank.Click
        Get_Section_From_Menu(section18)
    End Sub
    Private Sub Menu_ExternalPipe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ExternalPipe.Click
        Get_Section_From_Menu(section19)
    End Sub
    Private Sub Menu_Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Stop.Click
        Get_Section_From_Menu(section20)
    End Sub
    Private Sub Menu_StopRank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_StopRank.Click
        Get_Section_From_Menu(section21)
    End Sub
    Private Sub Menu_ReversiblePiston_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ReversiblePiston.Click
        Get_Section_From_Menu(section22)
    End Sub
    Private Sub Menu_Combination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Combination.Click
        Get_Section_From_Menu(section23)
    End Sub
    Private Sub Menu_CombinationElement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_CombinationElement.Click
        Get_Section_From_Menu(section24)
    End Sub
    Private Sub Menu_ContinuousControl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ContinuousControl.Click
        Get_Section_From_Menu(section25)
    End Sub
    Private Sub Menu_ContinuousControlStageSwitch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ContinuousControlStageSwitch.Click
        Get_Section_From_Menu(section26)
    End Sub
    Private Sub Menu_ContinuousControlImageSetStage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ContinuousControlImageSetStage.Click
        Get_Section_From_Menu(section27)
    End Sub
    Private Sub Menu_Enclosure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Enclosure.Click
        Get_Section_From_Menu(section28)
    End Sub
    Private Sub Menu_EnclosurePipe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_EnclosurePipe.Click
        Get_Section_From_Menu(section29)
    End Sub
    Private Sub Menu_Tremulant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Tremulant.Click
        Get_Section_From_Menu(section30)
    End Sub
    Private Sub Menu_TremulantWaveform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_TremulantWaveform.Click
        Get_Section_From_Menu(section31)
    End Sub
    Private Sub Menu_TremulantWaveformPipe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_TremulantWaveformPipe.Click
        Get_Section_From_Menu(section32)
    End Sub
    Private Sub Menu_ContinuousControlLinkage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ContinuousControlLinkage.Click
        Get_Section_From_Menu(section33)
    End Sub
    Private Sub Menu_ContinuousControlDoubleLinkage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ContinuousControlDoubleLinkage.Click
        Get_Section_From_Menu(section34)
    End Sub
    Private Sub Menu_ThreePositionSwitchImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ThreePositionSwitchImage.Click
        Get_Section_From_Menu(section35)
    End Sub
    Private Sub Menu_WindCompartment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_WindCompartment.Click
        Get_Section_From_Menu(section36)
    End Sub
    Private Sub Menu_WindCompartmentLinkage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_WindCompartmentLinkage.Click
        Get_Section_From_Menu(section37)
    End Sub
    Private Sub Menu_Sample_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Sample.Click
        Get_Section_From_Menu(section38)
    End Sub
    Private Sub Menu_PipeSoundEngine01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_PipeSoundEngine01.Click
        Get_Section_From_Menu(section39)
    End Sub
    Private Sub Menu_PipeSoundEngine01Layer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_PipeSoundEngine01Layer.Click
        Get_Section_From_Menu(section40)
    End Sub
    Private Sub Menu_PipeSoundEngine01AttackSample_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_PipeSoundEngine01AttackSample.Click
        Get_Section_From_Menu(section41)
    End Sub
    Private Sub Menu_PipeSoundEngine01ReleaseSample_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_PipeSoundEngine01ReleaseSample.Click
        Get_Section_From_Menu(section42)
    End Sub
    Private Sub Menu_RequiredInstallationPackage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_RequiredInstallationPackage.Click
        Get_Section_From_Menu(section43)
    End Sub

    ' MENU EDIT MODE
    Private Sub Menu_StartEditMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_StartEditMode.Click
        ' MET L'ODF EN MODE EDITION AUTORISEE
        ' modif version 055 & 56
        RTBox.ReadOnly = False
        RTBox.BackColor = Color.White
        Menu_StartEditMode.Checked = True
        Menu_ExitEditMode.Checked = False
        Menu_EDITMODE.BackColor = Color.Red
        G_EditMode = True ' indique edition en cours (modif 055)
        ' warning
        Dim txt = "WARNING" & vbCrLf
        txt += "If you modify the length of the ODF ,by adding or removing some characters" & " "
        txt += "you must re-compute the parameters of the 44 sections" & vbCrLf
        txt += "So, please, once you have finished editing one or several lines in the same section, "
        txt += "click on the menu ""Re-compute the section"" before editing a line in an other section." & vbCrLf
        txt += "Thanks" & vbCrLf
        txt += "Notice that the cursor will allways be at the begining of the line you click on, move it with the arrows to the place you want to write."
        MsgBox(txt, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Entering EDIT MODE")
    End Sub
    Private Sub Menu_ExitEditMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ExitEditMode.Click
        ' REMET L'ODF EN MODE EDITION INTERDITE
        RTBox.ReadOnly = True
        RTBox.BackColor = Color.WhiteSmoke
        Menu_StartEditMode.Checked = False
        Menu_ExitEditMode.Checked = True
        Menu_EDITMODE.BackColor = Color.LightSteelBlue
        G_EditMode = False ' indique retour au mode lecture seule (modif v055)
        G_LastSectionName = "" ' (modif v056)
        ' warning
        Dim rep As Integer
        Dim verbose = True
        Dim txt = "WARNING" & vbCrLf
        txt += "If you have changed the length of the ODF ,by adding or removing some text, "
        txt += "you must re-compute the parameters of the 44 sections" & vbCrLf
        txt += "Do you want to re-compute the sections ?"
        rep = MsgBox(txt, MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Exiting EDIT MODE")
        If rep = 6 Then get_Sections_Infos(verbose)

    End Sub
    Private Sub Menu_ReComputeSections_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ReComputeSections.Click
        Dim verbose = True
        get_Sections_Infos(verbose)
    End Sub

    ' MENU TOOLS
    Private Sub Menu_ClearMarkers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_ClearMarkers.Click
        ButtonMarker1.Text = "Marker 1" : ButtonMarker1.BackColor = Color.Gainsboro
        ButtonMarker2.Text = "Marker 2" : ButtonMarker2.BackColor = Color.Gainsboro
        ButtonMarker3.Text = "Marker 3" : ButtonMarker3.BackColor = Color.Gainsboro
        ButtonMarker4.Text = "Marker 4" : ButtonMarker4.BackColor = Color.Gainsboro
    End Sub
    Private Sub CouplersCodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_CouplersCode.Click
        ' AFFICHE LA FENETRE COUPLERS
        Couplers.Visible = False
        Couplers.Show(Me)
    End Sub
    Private Sub FollowASampleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_FollowASample.Click
        ' AFFICHE LA FENETRE FOLLOW
        G_Item_to_Follow = "<ObjectList ObjectType=""Sample"">"
        Follow.Visible = False
        Follow.Show(Me)
    End Sub

    ' MENU ?
    Private Sub Menu_Help_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Menu_Help.Click
        G_RTF_File = G_DataPath & "\help.rtf"
        If My.Computer.FileSystem.FileExists(G_RTF_File) Then
            RTBoxRTF.LoadFile(G_RTF_File)
        End If
    End Sub

    ' BOUTONS
    Private Sub FindButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindButton.Click
        G_FindStartPosition = 1
        FindButtonProcedure()
    End Sub
    Private Sub FindNextButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindNextButton.Click
        If G_FindStartPosition < 0 Then G_FindStartPosition = 1
        FindButtonProcedure()
    End Sub
    Private Sub FindButtonProcedure()
        ' CODE COMMUN A FIND ET FIND NEXT
        Dim startPos As Integer
        Dim textLenght As Integer
        G_TextToFind = TextToFindBox.Text
        If G_TextToFind = "" Then Exit Sub
        textLenght = G_TextToFind.Length
        startPos = FindMyText(G_TextToFind, G_FindStartPosition)
        If startPos <= 0 Then MsgBox("Not Found") : Exit Sub
        ' vide le panneau des tags
        clear_Tags_Panel()
        ' MsgBox("Section : " & G_SectionName & vbCrLf & "Last section : " & G_LastSectionName)
        G_LastSectionName = G_SectionName
        'G_SectionName = ""
        RTBoxLine.Text = ""
        Get_Section_From_Index(startPos)
        Get_Line_From_Index(startPos)
        G_FindStartPosition = startPos + 1
        LabelCaretPos.Text = startPos
        RTBox.Focus()
        RTBox.SelectionStart = startPos
        RTBox.SelectionLength = textLenght

    End Sub
    Private Sub ButtonLed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLed.Click
        Dim verbose = False
        get_Sections_Infos(verbose)
    End Sub
    Private Sub ButtonFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFont.Click
        ' FORCE LA POLICE DE LA BOX
        Dim fnt As New System.Drawing.Font("verdana", 10)
        RTBoxRTF.Focus()
        RTBoxRTF.Font = fnt
        RTBoxRTF.SelectAll()
        RTBoxRTF.SelectionFont = fnt
        RTBoxRTF.DeselectAll()
        RTBoxRTF.Refresh()
    End Sub
    Private Sub ButtonMarkers_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ButtonMarker1.MouseDown,
        ButtonMarker2.MouseDown, ButtonMarker3.MouseDown, ButtonMarker4.MouseDown
        ' MEMORISE OU RETROUVE UNE LIGNE
        Dim bouton As String = e.Button.ToString
        Dim marker As Control = sender
        Dim caret As Integer

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
    Private Sub ButtonDisplayImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDisplayImage.Click
        DisplayImage()
    End Sub
    Private Sub ButtonDisplayImage_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDisplayImage.LostFocus
        PBox.Visible = False
        PBox.Image = Nothing
        PBox.BorderStyle = BorderStyle.None
        ' retrecir le panel
        Dim psize = New System.Drawing.Size
        psize.Height = 330 : psize.Width = 600
        PanelTags.Size = psize
    End Sub
    Private Sub ButtonNextLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNextLine.Click
        ' RECHERCHE LA PROCHAINE LIGNE
        Dim newPos As Integer
        Try
            G_LineIndex += 1
            newPos = RTBox.GetFirstCharIndexFromLine(G_LineIndex)
            G_CaretPos = newPos
            LabelCaretPos.Text = newPos
            LabelLineNumber.Text = G_LineIndex
            LabelLineStart.Text = ""
            LabelLineEnd.Text = ""
            nextLine_commun()
            RTBox.Focus()
            RTBox.Select(newPos, 0)
            RTBox.ScrollToCaret()
        Catch
            ' ras
        End Try
    End Sub
    Private Sub ButtonNext10Lines_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNext10Lines.Click
        ' RECHERCHE LA 10eme PROCHAINE LIGNE
        Dim newPos As Integer
        Try
            G_LineIndex += 10
            newPos = RTBox.GetFirstCharIndexFromLine(G_LineIndex)
            G_CaretPos = newPos
            LabelCaretPos.Text = newPos
            LabelLineNumber.Text = G_LineIndex
            LabelLineStart.Text = ""
            LabelLineEnd.Text = ""
            nextLine_commun()
            RTBox.Focus()
            RTBox.Select(newPos, 0)
            RTBox.ScrollToCaret()
        Catch
            ' ras
        End Try

    End Sub
    Private Sub ButtonNext100Lines_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNext100Lines.Click
        ' RECHERCHE LA 100eme PROCHAINE LIGNE
        Dim newPos As Integer
        Try
            G_LineIndex += 100
            newPos = RTBox.GetFirstCharIndexFromLine(G_LineIndex)
            G_CaretPos = newPos
            LabelCaretPos.Text = newPos
            LabelLineNumber.Text = G_LineIndex
            LabelLineStart.Text = ""
            LabelLineEnd.Text = ""
            nextLine_commun()
            RTBox.Focus()
            RTBox.Select(newPos, 0)
            RTBox.ScrollToCaret()
        Catch
            ' ras
        End Try
    End Sub

    ' IMAGES
    Private Sub DisplayImage()
        ' AFFICHE L'IMAGE (OU LE MASK)
        ' identifie d'abord le package
        FindImagePackageID(G_ImageSet)
        LabelPackageID.Text = "PkgID = " & G_PackageID
        Dim url As String = G_PackagePath & G_PackageID & G_ImageFile
        ' agrandir le panel
        Dim psize = New System.Drawing.Size
        psize.Height = 330 : psize.Width = 1150
        PanelTags.Size = psize
        Try
            ' Retrieve the image.
            Dim image1 As Bitmap
            ' Load the picture into a Bitmap.
            image1 = New Bitmap(url, True)

            ' Display the results.
            PBox.BorderStyle = BorderStyle.FixedSingle
            PBox.Image = image1
            PBox.SizeMode = PictureBoxSizeMode.AutoSize
            PBox.Visible = True
        Catch ex As ArgumentException
            MessageBox.Show("Sorry. Cannot display the image.")
            MsgBox(url, , "Message from Display_Image function")
        End Try
    End Sub
    Private Sub FindImagePackageID(ByVal imgSet)
        ' CHERCHE LE PACKAGEID D'UNE IMAGE D'IMAGE SET ELEMENT 
        Dim sectidx As Integer = 5 ' imageSet
        Dim startPos As Integer = S_Section(sectidx).startPos
        Dim endPos As Integer = S_Section(sectidx).endPos
        Dim tagStartA As Integer
        Dim txtA As String = "<a>" & imgSet & "</a>"
        Dim tagStartC As Integer
        Dim tagEndC As Integer
        Dim txtC As String = "<c>"
        Dim msg As String
        msg = "Search in section " & S_Section(sectidx).name & vbCrLf
        msg += "From " & startPos.ToString & " to " & endPos.ToString & vbCrLf

        ' RECHERCHE DU TAG <a>
        tagStartA = RTBox.Find(txtA, startPos, RichTextBoxFinds.None)
        If tagStartA < 0 Then Exit Sub
        startPos = tagStartA + Len(txtA)
        'Recherche du tag <c>
        tagStartC = RTBox.Find(txtC, startPos, RichTextBoxFinds.None)
        If tagStartC < 0 Then Exit Sub
        startPos = tagStartC + 3
        tagEndC = RTBox.Find("</c>", startPos, RichTextBoxFinds.None)
        ' lire le tag <c> = packageID
        RTBox.SelectionStart = tagStartC + 3
        RTBox.SelectionLength = tagEndC - (tagStartC + 3)
        G_PackageID = RTBox.SelectedText
        Do While Len(G_PackageID) < 6
            G_PackageID = "0" & G_PackageID
        Loop
        G_PackageID += "\"
        msg += G_PackageID
        'MsgBox(msg)
    End Sub
    Private Sub PBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBox.Click
        PBox.Visible = False
        PBox.Image = Nothing
        PBox.BorderStyle = BorderStyle.None
        ' retrecir le panel
        Dim psize = New System.Drawing.Size
        psize.Height = 330 : psize.Width = 600
        PanelTags.Size = psize
    End Sub

    '
    Private Sub RTBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RTBox.TextChanged
        ' FAUT-IL RECALCULER LES SECTIONS ?
        Dim verbose = False
        If RTBox.TextLength <> G_OdfLength Then
            'MsgBox(RTBox.TextLength.ToString)
            G_OdfLength = RTBox.TextLength
            'get_Sections_Infos(verbose)
        End If
    End Sub

    ' DOC RICH TEXT BOX

    'AppendText 	Ajoute du texte au texte en cours dans une zone de texte. (Hérité de TextBoxBase.)
    'CanPaste 	Détermine si vous pouvez coller des informations du Presse-papiers dans le format de données spécifié.
    'Clear 	Efface tout le texte du contrôle zone de texte. (Hérité de TextBoxBase.)
    'ClearUndo 	Efface les informations sur la dernière opération effectuée à partir de la mémoire tampon d'annulation de la zone de texte. (Hérité de TextBoxBase.)
    'Copy 	Copie la sélection actuelle dans la zone de texte vers le Presse-papiers. (Hérité de TextBoxBase.)
    'Cut 	Déplace la sélection actuelle entre la zone de texte et le Presse-papiers. (Hérité de TextBoxBase.)
    'DeselectAll 	Spécifie que la valeur de la propriété SelectionLength est zéro afin qu'aucun caractère ne soit sélectionné dans le contrôle. (Hérité de TextBoxBase.)
    'Find(Char()) 	Recherche dans le texte d'un contrôle RichTextBox la première occurrence d'un caractère issu d'une liste.
    'Find(String) 	Recherche une chaîne donnée dans le texte d'un contrôle RichTextBox.
    'Find(Char(), Int32) 	Recherche dans le texte d'un contrôle RichTextBox, à partir d'un point spécifique, la première occurrence d'un caractère parmi une liste de caractères.
    'Find(String, RichTextBoxFinds) 	Recherche une chaîne dans le texte d'un contrôle RichTextBox en appliquant des options de recherche spécifiques.
    'Find(Char(), Int32, Int32) 	Recherche dans une plage de texte d'un contrôle RichTextBox la première occurrence d'un caractère issu d'une liste.
    'Find(String, Int32, RichTextBoxFinds) 	Recherche une chaîne à un emplacement spécifique dans le texte d'un contrôle RichTextBox en appliquant des options de recherche spécifiques.
    'Find(String, Int32, Int32, RichTextBoxFinds) 	Recherche une chaîne dans une plage de texte d'un contrôle RichTextBox en appliquant des options de recherche spécifiques. 
    'GetCharFromPosition 	Récupère le caractère le plus proche de l'emplacement spécifié dans le contrôle. (Hérité de TextBoxBase.)
    'GetCharIndexFromPosition 	Récupère l'index du caractère le plus proche de l'emplacement spécifié par un System.Drawning.Point.
    'GetFirstCharIndexFromLine 	Récupère l'index du premier caractère d'une ligne donnée. (Hérité de TextBoxBase.)
    'GetFirstCharIndexOfCurrentLine 	Récupère l'index du premier caractère de la ligne active. (Hérité de TextBoxBase.)
    'GetLineFromCharIndex 	Récupère le numéro de ligne à partir de la position de caractère spécifiée dans le texte du contrôle RichTextBox. (Substitue TextBoxBase.GetLineFromCharIndex(Int32).)
    'GetPositionFromCharIndex 	Récupère l'emplacement de l'index de caractère spécifié dans le contrôle. (Substitue TextBoxBase.GetPositionFromCharIndex(Int32).)
    'ResetFont 	Rétablit la valeur par défaut de la propriété Font. (Hérité de Control.)
    'ResetForeColor 	Rétablit la valeur par défaut de la propriété ForeColor. (Hérité de Control.)
    'ScrollToCaret 	Fait défiler le contenu du contrôle vers la position indiquée par le signe insertion. (Hérité de TextBoxBase.)
    'Select(Int32, Int32) 	Sélectionne une plage de texte dans la zone de texte.(start position, longueur selection)
    'SelectAll 	Sélectionne tout le texte de la zone de texte. (Hérité de TextBoxBase.)
    'Undo 	Annule la dernière modification apportée dans la zone de texte. (Hérité de TextBoxBase.)
    'Update 	Force le contrôle à redessiner les zones invalidées dans sa zone cliente. (Hérité de Control.)
    'UpdateStyles 	Force la réapplication au contrôle des styles assignés. (Hérité de Control.)

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click

        ' <1.059.0> Replaced MsgBox with VS embedded "AboutBox" control. This control takes its content from the Project's Assemby Parameters, maintained by the IDE.
        ' Commented-out code below is the original

        'Dim txt As String
        ' txt = "AECHO " & My.Application.Info.Version.ToString & vbCrLf
        ' txt += "Freeware version" & vbCrLf
        'txt += "© 2021 - Jean-Paul Verpeaux" & vbCrLf
        'txt += "If you realise something interesting about AECHO, please share your work by sending me information." & vbCrLf
        'txt += "MUSICALIS@NEUF.FR"
        'MsgBox(txt, 0, "About Aecho")
        AboutBox1.Show(Me)  ' Display the form

    End Sub
End Class
