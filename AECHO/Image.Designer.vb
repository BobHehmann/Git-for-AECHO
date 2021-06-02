<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImageDisp
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Lbl_ImageTitle = New System.Windows.Forms.Label()
        Me.PBox = New System.Windows.Forms.PictureBox()
        Me.Lbl_ImageSize = New System.Windows.Forms.Label()
        Me.Lbl_PackageID = New System.Windows.Forms.Label()
        CType(Me.PBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Lbl_ImageTitle
        '
        Me.Lbl_ImageTitle.AutoSize = True
        Me.Lbl_ImageTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_ImageTitle.Location = New System.Drawing.Point(13, 13)
        Me.Lbl_ImageTitle.Name = "Lbl_ImageTitle"
        Me.Lbl_ImageTitle.Size = New System.Drawing.Size(42, 15)
        Me.Lbl_ImageTitle.TabIndex = 0
        Me.Lbl_ImageTitle.Text = "Image"
        '
        'PBox
        '
        Me.PBox.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.PBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBox.Location = New System.Drawing.Point(37, 99)
        Me.PBox.Name = "PBox"
        Me.PBox.Size = New System.Drawing.Size(100, 50)
        Me.PBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PBox.TabIndex = 1
        Me.PBox.TabStop = False
        '
        'Lbl_ImageSize
        '
        Me.Lbl_ImageSize.AutoSize = True
        Me.Lbl_ImageSize.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_ImageSize.Location = New System.Drawing.Point(13, 38)
        Me.Lbl_ImageSize.Name = "Lbl_ImageSize"
        Me.Lbl_ImageSize.Size = New System.Drawing.Size(68, 15)
        Me.Lbl_ImageSize.TabIndex = 2
        Me.Lbl_ImageSize.Text = "Image Size"
        '
        'Lbl_PackageID
        '
        Me.Lbl_PackageID.AutoSize = True
        Me.Lbl_PackageID.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_PackageID.Location = New System.Drawing.Point(13, 63)
        Me.Lbl_PackageID.Name = "Lbl_PackageID"
        Me.Lbl_PackageID.Size = New System.Drawing.Size(69, 15)
        Me.Lbl_PackageID.TabIndex = 3
        Me.Lbl_PackageID.Text = "Package ID"
        '
        'ImageDisp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(902, 612)
        Me.Controls.Add(Me.Lbl_PackageID)
        Me.Controls.Add(Me.Lbl_ImageSize)
        Me.Controls.Add(Me.PBox)
        Me.Controls.Add(Me.Lbl_ImageTitle)
        Me.Name = "ImageDisp"
        Me.Text = "Image"
        CType(Me.PBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Lbl_ImageTitle As Windows.Forms.Label
    Friend WithEvents PBox As Windows.Forms.PictureBox
    Friend WithEvents Lbl_ImageSize As Windows.Forms.Label
    Friend WithEvents Lbl_PackageID As Windows.Forms.Label
End Class
