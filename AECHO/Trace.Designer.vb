<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Trace
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_SampleID = New System.Windows.Forms.TextBox()
        Me.Btn_TraceSample = New System.Windows.Forms.Button()
        Me.Menu_SampleTrace = New System.Windows.Forms.MenuStrip()
        Me.MenuTSExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuTSClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuTSPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintDialogTrace = New System.Windows.Forms.PrintDialog()
        Me.PrintDocumentTrace = New System.Drawing.Printing.PrintDocument()
        Me.Menu_SampleTrace.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(152, 35)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "   Sample ID:"
        '
        'Txt_SampleID
        '
        Me.Txt_SampleID.Location = New System.Drawing.Point(224, 31)
        Me.Txt_SampleID.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Txt_SampleID.Name = "Txt_SampleID"
        Me.Txt_SampleID.Size = New System.Drawing.Size(96, 23)
        Me.Txt_SampleID.TabIndex = 1
        '
        'Btn_TraceSample
        '
        Me.Btn_TraceSample.AutoSize = True
        Me.Btn_TraceSample.Location = New System.Drawing.Point(349, 30)
        Me.Btn_TraceSample.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_TraceSample.Name = "Btn_TraceSample"
        Me.Btn_TraceSample.Size = New System.Drawing.Size(87, 25)
        Me.Btn_TraceSample.TabIndex = 3
        Me.Btn_TraceSample.Text = "Trace"
        Me.Btn_TraceSample.UseVisualStyleBackColor = True
        '
        'Menu_SampleTrace
        '
        Me.Menu_SampleTrace.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuTSExit, Me.MenuTSClear, Me.MenuTSPrint})
        Me.Menu_SampleTrace.Location = New System.Drawing.Point(0, 0)
        Me.Menu_SampleTrace.Name = "Menu_SampleTrace"
        Me.Menu_SampleTrace.Size = New System.Drawing.Size(852, 24)
        Me.Menu_SampleTrace.TabIndex = 5
        Me.Menu_SampleTrace.Text = "Sample Trace"
        '
        'MenuTSExit
        '
        Me.MenuTSExit.Name = "MenuTSExit"
        Me.MenuTSExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.MenuTSExit.Size = New System.Drawing.Size(38, 20)
        Me.MenuTSExit.Text = "E&xit"
        '
        'MenuTSClear
        '
        Me.MenuTSClear.Name = "MenuTSClear"
        Me.MenuTSClear.Size = New System.Drawing.Size(81, 20)
        Me.MenuTSClear.Text = "&Clear Traces"
        '
        'MenuTSPrint
        '
        Me.MenuTSPrint.Name = "MenuTSPrint"
        Me.MenuTSPrint.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.MenuTSPrint.Size = New System.Drawing.Size(88, 20)
        Me.MenuTSPrint.Text = "&Print Traces..."
        '
        'PrintDialogTrace
        '
        Me.PrintDialogTrace.Document = Me.PrintDocumentTrace
        Me.PrintDialogTrace.UseEXDialog = True
        '
        'PrintDocumentTrace
        '
        Me.PrintDocumentTrace.DocumentName = "AECHOSampleTrace"
        '
        'Trace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(852, 576)
        Me.Controls.Add(Me.Btn_TraceSample)
        Me.Controls.Add(Me.Txt_SampleID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Menu_SampleTrace)
        Me.MainMenuStrip = Me.Menu_SampleTrace
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "Trace"
        Me.Opacity = 0.9R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sample Trace"
        Me.Menu_SampleTrace.ResumeLayout(False)
        Me.Menu_SampleTrace.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Txt_SampleID As System.Windows.Forms.TextBox
    Friend WithEvents Btn_TraceSample As System.Windows.Forms.Button
    Friend WithEvents Menu_SampleTrace As Windows.Forms.MenuStrip
    Friend WithEvents MenuTSExit As Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuTSClear As Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuTSPrint As Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintDialogTrace As Windows.Forms.PrintDialog
    Friend WithEvents PrintDocumentTrace As Printing.PrintDocument
End Class
