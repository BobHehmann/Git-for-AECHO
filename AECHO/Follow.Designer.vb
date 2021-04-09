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
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxItemID = New System.Windows.Forms.TextBox
        Me.FollowListBox = New System.Windows.Forms.ListBox
        Me.ButtonFollowOK = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(109, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "   Sample ID"
        '
        'TextBoxItemID
        '
        Me.TextBoxItemID.Location = New System.Drawing.Point(192, 44)
        Me.TextBoxItemID.Name = "TextBoxItemID"
        Me.TextBoxItemID.Size = New System.Drawing.Size(83, 20)
        Me.TextBoxItemID.TabIndex = 1
        '
        'FollowListBox
        '
        Me.FollowListBox.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FollowListBox.FormattingEnabled = True
        Me.FollowListBox.ItemHeight = 16
        Me.FollowListBox.Location = New System.Drawing.Point(12, 108)
        Me.FollowListBox.Name = "FollowListBox"
        Me.FollowListBox.Size = New System.Drawing.Size(542, 372)
        Me.FollowListBox.TabIndex = 2
        '
        'ButtonFollowOK
        '
        Me.ButtonFollowOK.Location = New System.Drawing.Point(302, 44)
        Me.ButtonFollowOK.Name = "ButtonFollowOK"
        Me.ButtonFollowOK.Size = New System.Drawing.Size(32, 19)
        Me.ButtonFollowOK.TabIndex = 3
        Me.ButtonFollowOK.Text = "OK"
        Me.ButtonFollowOK.UseVisualStyleBackColor = True
        '
        'Follow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(566, 499)
        Me.Controls.Add(Me.ButtonFollowOK)
        Me.Controls.Add(Me.FollowListBox)
        Me.Controls.Add(Me.TextBoxItemID)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Follow"
        Me.Opacity = 0.9
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Follow"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxItemID As System.Windows.Forms.TextBox
    Friend WithEvents FollowListBox As System.Windows.Forms.ListBox
    Friend WithEvents ButtonFollowOK As System.Windows.Forms.Button
End Class
