Imports System.Windows.Forms

Public Class Follow

    Private Sub Btn_TraceSample_Click(          ' User clicked the "OK" Button
            sender As Object,
            e As EventArgs
            ) Handles Btn_TraceSample.Click

        ' Purpose:      Dispatch TraceSample() to expand all possible Fields in a Sample Record,
        '               and follow linkages to other related Sections, also expanding all of their
        '               Fields.
        ' Process:		Call TraceSample() to do the work.
        ' Called By:    Btn_TraceSample.Click Event
        ' Side Effects: <None>
        ' Notes:        <None>
        ' Updates:		<1.060.2> Moved all logic to routine TraceSample. Widened the Form, and the
        '               ListBox; added horizontal scrolling to the ListBox.

        Const lclProcName As String =           ' <1.060.2> Routine's name for message handling
            "Btn_TraceSample_Click"

        TraceSample(Val(Txt_SampleID.Text),
                    Lb_TraceSample)             ' Invoke the routine to retrieve data tags related to a specified Sample ID. <1.060.2> Pass itemID as parm to TraceSample

    End Sub


End Class