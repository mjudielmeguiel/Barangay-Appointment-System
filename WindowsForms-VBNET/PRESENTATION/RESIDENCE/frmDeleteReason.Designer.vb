<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeleteReason
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDeleteReason))
        Me.rtfInfo = New System.Windows.Forms.RichTextBox()
        Me.rtfReason = New System.Windows.Forms.RichTextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.lblAssignedOffice = New System.Windows.Forms.Label()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'rtfInfo
        '
        Me.rtfInfo.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.rtfInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtfInfo.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtfInfo.Location = New System.Drawing.Point(16, 66)
        Me.rtfInfo.Name = "rtfInfo"
        Me.rtfInfo.Size = New System.Drawing.Size(553, 198)
        Me.rtfInfo.TabIndex = 0
        Me.rtfInfo.Text = ""
        '
        'rtfReason
        '
        Me.rtfReason.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.rtfReason.Location = New System.Drawing.Point(16, 305)
        Me.rtfReason.Name = "rtfReason"
        Me.rtfReason.Size = New System.Drawing.Size(553, 81)
        Me.rtfReason.TabIndex = 1
        Me.rtfReason.Text = ""
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel5.Controls.Add(Me.btnClose)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(585, 60)
        Me.Panel5.TabIndex = 717
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.Location = New System.Drawing.Point(912, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(108, 37)
        Me.btnClose.TabIndex = 554
        Me.btnClose.Text = "Close"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(12, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(300, 19)
        Me.Label4.TabIndex = 520
        Me.Label4.Text = "Type the Reason of Deletion Residents Account"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(12, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(137, 21)
        Me.Label9.TabIndex = 519
        Me.Label9.Text = "Account Deletion"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.Location = New System.Drawing.Point(301, 423)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(131, 37)
        Me.btnCancel.TabIndex = 719
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnConfirm
        '
        Me.btnConfirm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfirm.BackColor = System.Drawing.Color.Maroon
        Me.btnConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfirm.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirm.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConfirm.Location = New System.Drawing.Point(438, 423)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(131, 37)
        Me.btnConfirm.TabIndex = 718
        Me.btnConfirm.Text = "Remove Account"
        Me.btnConfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnConfirm.UseVisualStyleBackColor = False
        '
        'lblAssignedOffice
        '
        Me.lblAssignedOffice.AutoSize = True
        Me.lblAssignedOffice.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssignedOffice.Location = New System.Drawing.Point(13, 285)
        Me.lblAssignedOffice.Name = "lblAssignedOffice"
        Me.lblAssignedOffice.Size = New System.Drawing.Size(54, 17)
        Me.lblAssignedOffice.TabIndex = 720
        Me.lblAssignedOffice.Text = "Reason:"
        '
        'frmDeleteReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ClientSize = New System.Drawing.Size(585, 472)
        Me.Controls.Add(Me.lblAssignedOffice)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.rtfReason)
        Me.Controls.Add(Me.rtfInfo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDeleteReason"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmDeleteReason"
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rtfInfo As RichTextBox
    Friend WithEvents rtfReason As RichTextBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnConfirm As Button
    Friend WithEvents lblAssignedOffice As Label
End Class
