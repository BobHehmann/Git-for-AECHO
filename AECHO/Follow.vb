Public Class Follow
    Public F_StartPos As Integer
    Public F_LimitPos As Integer
    Public F_ItemID
    Public Function Find_a_Tag(ByVal text As String, ByVal start As Integer) As Integer
        ' CHERCHE UN TAG DE HAUT EN BAS DANS RTBOX ET RETOURNE SA POSITION
        ' Initialize the return value to false by default.
        Dim returnValue As Integer = -1
        ' dans les limites de la ligne ?
        ' If start > F_LimitPos Then Return returnValue
        ' Ensure that a search string has been specified and a valid start point.
        If text.Length > 0 Then
            ' Obtain the location of the search string in richTextBox.
            Dim indexToText As Integer = MAIN.RTBox.Find(text, start, RichTextBoxFinds.None)
            ' Determine whether the text was found in richTextBox1.
            ' dans les limites de la ligne ?
            If indexToText > F_LimitPos Then Return returnValue

            If indexToText >= 0 Then returnValue = indexToText
        End If
        Return returnValue
    End Function
    Public Function Read_a_Tag(ByVal tag As String, ByVal pos As Integer) As String
        ' CHERCHE UN TAG DANS RTBOXLINE ET LIT LE  TEXTE CONTENU DANS LE TAG
        Dim returnValue As String = ""
        Dim tagStart As Integer
        Dim tagEnd As Integer
        ' trouve le tag de début et de fin
        tagStart = Find_a_Tag("<" & tag & ">", pos) + 2 + Len(tag)
        If tagStart = -1 Then Return "" : Exit Function
        ' trouve le tag de fin
        tagEnd = Find_a_Tag("</" & tag & ">", tagStart)
        If tagEnd = -1 Then Return "" : Exit Function
        ' lit le contenu du tag
        MAIN.RTBox.SelectionStart = tagStart
        MAIN.RTBox.SelectionLength = tagEnd - tagStart
        returnValue = MAIN.RTBox.SelectedText
        Return returnValue
    End Function
    Private Sub ButtonFollowOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFollowOK.Click
        'FollowListBox.
        Follow_a_Sample()
    End Sub
    Private Sub Follow_a_Sample()
        ' SUIT UN SAMPLE DANS LES DIFFERENTES SECTIONS QU'IL UTILISE
        Dim tagText As String
        Dim tagPos As Integer

        ' SECTION SAMPLE
        F_ItemID = Val(TextBoxItemID.Text)
        F_StartPos = Find_a_Tag("<ObjectList ObjectType=""Sample"">", 0)
        F_LimitPos = MAIN.RTBox.MaxLength

        If F_StartPos = -1 Then Exit Sub
        FollowListBox.Items.Add("SECTION SAMPLE (Start at " & F_StartPos.ToString & ")")
        ' tag <a> de Sample
        F_StartPos = Find_a_Tag("<a>" & F_ItemID.ToString & "</a>", F_StartPos)
        If F_StartPos = -1 Then
            MsgBox("Sorry, but your ODF does not contain this item") : Exit Sub
        End If
        tagText = Read_a_Tag("a", F_StartPos)
        FollowListBox.Items.Add("Sample ID = " & tagText)
        ' limite de recherche = position de </o>
        F_LimitPos = Find_a_Tag("</o>", F_StartPos)
        ' tag <b> de Sample
        tagPos = Find_a_Tag("<b>", F_StartPos)
        tagText = Read_a_Tag("b", tagPos)
        FollowListBox.Items.Add("Installation Package ID = " & tagText)
        ' tag <c> de Sample
        tagPos = Find_a_Tag("<c>", F_StartPos)
        tagText = Read_a_Tag("c", tagPos)
        FollowListBox.Items.Add("Sample Filename = " & tagText)
        ' tag <d> de Sample
        tagPos = Find_a_Tag("<d>", F_StartPos)
        If tagPos <> -1 Then
            tagText = Read_a_Tag("d", tagPos)
            FollowListBox.Items.Add("Pitch_Specification Method Code = " & tagText)
        Else
            FollowListBox.Items.Add("no tag Pitch_SpecificationMethodCode")
        End If
        ' tag <e> de Sample
        tagPos = Find_a_Tag("<e>", F_StartPos)
        If tagPos <> -1 Then
            tagText = Read_a_Tag("e", tagPos)
            FollowListBox.Items.Add("Pitch_Rank Base Pitch 64ft Harmonic Num = " & tagText)
        Else
            FollowListBox.Items.Add("no tag Pitch_RankBasePitch64ftHarmonicNum")
        End If
        ' tag <f> de Sample
        tagPos = Find_a_Tag("<f>", F_StartPos)
        If tagPos <> -1 Then
            tagText = Read_a_Tag("f", tagPos)
            FollowListBox.Items.Add("Pitch_Normal MIDI Note Number = " & tagText)
        Else
            FollowListBox.Items.Add("no tag")
        End If
        ' tag <g> de Sample
        tagPos = Find_a_Tag("<g>", F_StartPos)
        If tagPos <> -1 Then
            tagText = Read_a_Tag("g", tagPos)
            FollowListBox.Items.Add("Pitch_ExactSamplePitch = " & tagText)
        Else
            FollowListBox.Items.Add("no tag")
        End If
        ' tag <h> de Sample
        tagPos = Find_a_Tag("<h>", F_StartPos)
        If tagPos <> -1 Then
            tagText = Read_a_Tag("h", tagPos)
            FollowListBox.Items.Add("Licence Serial Num Required For SampleFile = " & tagText)
        Else
            FollowListBox.Items.Add("no tag LicenceSerialNumRequiredForSampleFile")
        End If

        ' SOUND ENGINE 01 ATTACK
        F_ItemID = Val(TextBoxItemID.Text)
        F_StartPos = Find_a_Tag("<ObjectList ObjectType=""Pipe_SoundEngine01_AttackSample"">", 0)
        F_LimitPos = MAIN.RTBox.MaxLength

        If F_StartPos = -1 Then Exit Sub
        FollowListBox.Items.Add("SECTION SOUND ENGINE 01 ATTACK SAMPLE (Start at " & F_StartPos.ToString & ")")
        ' tag <a> de Sound Engine
        F_StartPos = Find_a_Tag("<c>" & F_ItemID.ToString & "</c>", F_StartPos)
        If F_StartPos = -1 Then
            MsgBox("Sorry, but your ODF does not contain this item") : Exit Sub
        End If
        tagText = Read_a_Tag("c", F_StartPos)
        FollowListBox.Items.Add("Sample ID = " & tagText)
        ' limite de recherche = position de </o>
        F_LimitPos = Find_a_Tag("</o>", F_StartPos)


    End Sub
End Class