Public Class Follow
    Public F_StartPos As Integer                                    ' Beginning / current position of a search
    Public F_LimitPos As Integer                                    ' Upper bound on a search: either EOF, or EOL, depending on context
    Public F_ItemID As Integer                                      ' Sample ID from form, as an integer
    Public Function Find_a_Tag(                                     ' Searches the RTF, from start to F_LimitPos
            text As String,                                         ' text to search for
            start As Integer                                        ' Begin search from character position 'start'
            ) As Integer                                            ' CHERCHE UN TAG DE HAUT EN BAS DANS RTBOX ET RETOURNE SA POSITION; return -1 if not found, otherwise position of match

        Dim returnValue As Integer = -1                             ' Initialize the default return value to false
        ' dans les limites de la ligne ?
        ' If start > F_LimitPos Then Return returnValue
        ' Ensure that a search string has been specified and a valid start point.

        If text.Length > 0 Then                                     ' Return "no match" if search text is null
            Dim indexToText As Integer = MAIN.RTBox.Find(text, start, RichTextBoxFinds.None)
            '                                                         Obtain the location of the search string in richTextBox
            '                                                         Determine whether the text was found in richTextBox1.
            If indexToText > F_LimitPos Then Return returnValue     ' dans les limites de la ligne ? If found, but past the outer search bound, return "no match"
            If indexToText >= 0 Then returnValue = indexToText      ' Found it, it is within limit, return index
        End If

        Return returnValue                                          ' Return either -1 -> "no match within range", or index to match

    End Function
    Public Function Read_a_Tag(                                     ' Searches for an XML tag, selecting and returning the value of the tag
            tag As String,                                          ' Tag name to find: 'a' -> search for '<a>TextToReturn</a>
            pos As Integer                                          ' Starting position of search
            ) As String                                             ' CHERCHE UN TAG DANS RTBOXLINE ET LIT LE  TEXTE CONTENU DANS LE TAG; return null if not found, else content of the tag

        Dim returnValue As String                                   ' Removed initialization, not required
        Dim tagStart As Integer                                     ' Index to opening '<' of tag, if the tag is located
        Dim tagEnd As Integer                                       ' Index to closing '</' of tag, if located

        tagStart = Find_a_Tag("<" & tag & ">", pos) + 2 + Len(tag)  ' trouve le tag de début et de fin; look for opening of the tag
        If tagStart = -1 Then Return "" : Exit Function

        tagEnd = Find_a_Tag("</" & tag & ">", tagStart)             ' trouve le tag de fin; look for the close of the tag
        If tagEnd = -1 Then Return "" : Exit Function

        MAIN.RTBox.SelectionStart = tagStart                        ' lit le contenu du tag; found it, select the text between the opening and closing, without the tag markers
        MAIN.RTBox.SelectionLength = tagEnd - tagStart
        returnValue = MAIN.RTBox.SelectedText                       ' Return the selected string

        Return returnValue                                          ' Return null string if tag is not found

    End Function
    Private Sub ButtonFollowOK_Click(                               ' User clicked the "OK" Button
            sender As Object,
            e As EventArgs
            ) Handles ButtonFollowOK.Click

        Follow_a_Sample()                                           ' Invoke the routine to retrieve data tags related to a specified Sample ID

    End Sub
    Private Sub Follow_a_Sample()                                   ' SUIT UN SAMPLE DANS LES DIFFERENTES SECTIONS QU'IL UTILISE

        Dim tagText As String
        Dim tagPos As Integer

        ' SECTION SAMPLE
        F_ItemID = Val(TextBoxItemID.Text)                          ' Get the Sample ID (text), converted to an integer
        F_StartPos = Find_a_Tag("<ObjectList ObjectType=""Sample"">", 0)
        '                                                             Search the RTF/ODF from the beginning, looking for the Sample section's opening tag
        F_LimitPos = MAIN.RTBox.MaxLength                           ' A double bug: should be before the call to Find_a_Tag (otherwise that fails the first time), and should be .TextLength, the acutal length)
        If F_StartPos = -1 Then Exit Sub                            ' Not found at all

        FollowListBox.Items.Add("SECTION SAMPLE (Start at " & F_StartPos.ToString & ")")
        '                                                             Append starting position of Section to output

        F_StartPos = Find_a_Tag("<a>" & F_ItemID.ToString & "</a>", F_StartPos)
        '                                                             tag <a> de Sample; look for the <a>SampleID</a>, if found, this is the line with that sample's data
        If F_StartPos = -1 Then                                     ' Not found, display message, and return
            MsgBox("Sorry, but your ODF does not contain this item") : Exit Sub
        End If

        tagText = Read_a_Tag("a", F_StartPos)                       ' Found, so fetch the content text within the tag
        FollowListBox.Items.Add("Sample ID = " & tagText)           ' Add it to the output

        F_LimitPos = Find_a_Tag("</o>", F_StartPos)                 ' limite de recherche = position de </o>; Bring the outer serach-bound in to the end of this entry

        tagPos = Find_a_Tag("<b>", F_StartPos)                      ' tag <b> de Sample; locate a <b>InstallationPacageID"</b>
        tagText = Read_a_Tag("b", tagPos)                           ' If not found, will return a null
        FollowListBox.Items.Add("Installation Package ID = " & tagText)

        tagPos = Find_a_Tag("<c>", F_StartPos)                      ' tag <c> de Sample; locate <c>SampleFilename</c>
        tagText = Read_a_Tag("c", tagPos)
        FollowListBox.Items.Add("Sample Filename = " & tagText)

        tagPos = Find_a_Tag("<d>", F_StartPos)                      ' tag <d> de Sample; locate <d>Pitch_SpecificationMethodCode</d>
        If tagPos <> -1 Then
            tagText = Read_a_Tag("d", tagPos)
            FollowListBox.Items.Add("Pitch_Specification Method Code = " & tagText)
        Else
            FollowListBox.Items.Add("no tag Pitch_SpecificationMethodCode")
        End If

        tagPos = Find_a_Tag("<e>", F_StartPos)                      ' tag <e> de Sample
        If tagPos <> -1 Then
            tagText = Read_a_Tag("e", tagPos)
            FollowListBox.Items.Add("Pitch_Rank Base Pitch 64ft Harmonic Num = " & tagText)
        Else
            FollowListBox.Items.Add("no tag Pitch_RankBasePitch64ftHarmonicNum")
        End If

        tagPos = Find_a_Tag("<f>", F_StartPos)                      ' tag <f> de Sample
        If tagPos <> -1 Then
            tagText = Read_a_Tag("f", tagPos)
            FollowListBox.Items.Add("Pitch_Normal MIDI Note Number = " & tagText)
        Else
            FollowListBox.Items.Add("no tag")
        End If

        tagPos = Find_a_Tag("<g>", F_StartPos)                      ' tag <g> de Sample
        If tagPos <> -1 Then
            tagText = Read_a_Tag("g", tagPos)
            FollowListBox.Items.Add("Pitch_ExactSamplePitch = " & tagText)
        Else
            FollowListBox.Items.Add("no tag")
        End If

        tagPos = Find_a_Tag("<h>", F_StartPos)                      ' tag <h> de Sample
        If tagPos <> -1 Then
            tagText = Read_a_Tag("h", tagPos)
            FollowListBox.Items.Add("Licence Serial Num Required For SampleFile = " & tagText)
        Else
            FollowListBox.Items.Add("no tag LicenceSerialNumRequiredForSampleFile")
        End If


        F_ItemID = Val(TextBoxItemID.Text)                          ' SOUND ENGINE 01 ATTACK; see if this section exists
        F_StartPos = Find_a_Tag("<ObjectList ObjectType=""Pipe_SoundEngine01_AttackSample"">", 0)
        F_LimitPos = MAIN.RTBox.MaxLength

        If F_StartPos = -1 Then Exit Sub
        FollowListBox.Items.Add("SECTION SOUND ENGINE 01 ATTACK SAMPLE (Start at " & F_StartPos.ToString & ")")

        F_StartPos = Find_a_Tag("<c>" & F_ItemID.ToString & "</c>", F_StartPos) ' tag <a> de Sound Engine
        If F_StartPos = -1 Then
            MsgBox("Sorry, but your ODF does not contain this item") : Exit Sub
        End If
        tagText = Read_a_Tag("c", F_StartPos)
        FollowListBox.Items.Add("Sample ID = " & tagText)

        F_LimitPos = Find_a_Tag("</o>", F_StartPos)                 ' limite de recherche = position de </o>

    End Sub
End Class