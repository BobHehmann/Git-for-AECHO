<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Follow
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
        Me.Lb_TraceSample = New System.Windows.Forms.ListBox()
        Me.Btn_TraceSample = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(152, 55)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "   Sample ID:"
        '
        'Txt_SampleID
        '
        Me.Txt_SampleID.Location = New System.Drawing.Point(224, 51)
        Me.Txt_SampleID.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Txt_SampleID.Name = "Txt_SampleID"
        Me.Txt_SampleID.Size = New System.Drawing.Size(96, 23)
        Me.Txt_SampleID.TabIndex = 1
        '
        'Lb_TraceSample
        '
        Me.Lb_TraceSample.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lb_TraceSample.FormattingEnabled = True
        Me.Lb_TraceSample.HorizontalScrollbar = True
        Me.Lb_TraceSample.ItemHeight = 16
        Me.Lb_TraceSample.Location = New System.Drawing.Point(14, 125)
        Me.Lb_TraceSample.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Lb_TraceSample.Name = "Lb_TraceSample"
        Me.Lb_TraceSample.Size = New System.Drawing.Size(825, 420)
        Me.Lb_TraceSample.TabIndex = 2
        '
        'Btn_TraceSample
        '
        Me.Btn_TraceSample.AutoSize = True
        Me.Btn_TraceSample.Location = New System.Drawing.Point(352, 51)
        Me.Btn_TraceSample.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Btn_TraceSample.Name = "Btn_TraceSample"
        Me.Btn_TraceSample.Size = New System.Drawing.Size(87, 25)
        Me.Btn_TraceSample.TabIndex = 3
        Me.Btn_TraceSample.Text = "Trace Sample"
        Me.Btn_TraceSample.UseVisualStyleBackColor = True
        '
        'Follow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(852, 576)
        Me.Controls.Add(Me.Btn_TraceSample)
        Me.Controls.Add(Me.Lb_TraceSample)
        Me.Controls.Add(Me.Txt_SampleID)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "Follow"
        Me.Opacity = 0.9R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Follow"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Txt_SampleID As System.Windows.Forms.TextBox
    Friend WithEvents Lb_TraceSample As System.Windows.Forms.ListBox
    Friend WithEvents Btn_TraceSample As System.Windows.Forms.Button
End Class
