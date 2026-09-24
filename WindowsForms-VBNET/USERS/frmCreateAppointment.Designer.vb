<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCreateAppointment
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreateAppointment))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboRequestType = New System.Windows.Forms.ComboBox()
        Me.cboRequestFor = New System.Windows.Forms.ComboBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblControlNo = New System.Windows.Forms.Label()
        Me.txtPurpose = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSelectUser = New System.Windows.Forms.Button()
        Me.picUserProfile = New System.Windows.Forms.PictureBox()
        Me.txtNameOfRepresentative = New System.Windows.Forms.TextBox()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnSelectRepresentative = New System.Windows.Forms.Button()
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36 = New System.Windows.Forms.Button()
        CType(Me.picUserProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(8, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(169, 22)
        Me.Label1.TabIndex = 503
        Me.Label1.Text = "Service Information"
        '
        'cboRequestType
        '
        Me.cboRequestType.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboRequestType.Font = New System.Drawing.Font("Microsoft YaHei UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRequestType.FormattingEnabled = True
        Me.cboRequestType.Location = New System.Drawing.Point(156, 213)
        Me.cboRequestType.Name = "cboRequestType"
        Me.cboRequestType.Size = New System.Drawing.Size(597, 28)
        Me.cboRequestType.TabIndex = 508
        '
        'cboRequestFor
        '
        Me.cboRequestFor.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboRequestFor.Font = New System.Drawing.Font("Microsoft YaHei UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRequestFor.FormattingEnabled = True
        Me.cboRequestFor.Location = New System.Drawing.Point(156, 172)
        Me.cboRequestFor.Name = "cboRequestFor"
        Me.cboRequestFor.Size = New System.Drawing.Size(597, 28)
        Me.cboRequestFor.TabIndex = 583
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(156, 130)
        Me.txtName.Name = "txtName"
        Me.txtName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtName.Size = New System.Drawing.Size(551, 28)
        Me.txtName.TabIndex = 584
        '
        'lblControlNo
        '
        Me.lblControlNo.AutoSize = True
        Me.lblControlNo.Font = New System.Drawing.Font("Microsoft YaHei UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblControlNo.Location = New System.Drawing.Point(12, 244)
        Me.lblControlNo.Name = "lblControlNo"
        Me.lblControlNo.Size = New System.Drawing.Size(15, 19)
        Me.lblControlNo.TabIndex = 593
        Me.lblControlNo.Text = "-"
        '
        'txtPurpose
        '
        Me.txtPurpose.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtPurpose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurpose.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurpose.Location = New System.Drawing.Point(156, 256)
        Me.txtPurpose.Name = "txtPurpose"
        Me.txtPurpose.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtPurpose.Size = New System.Drawing.Size(597, 28)
        Me.txtPurpose.TabIndex = 594
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.Location = New System.Drawing.Point(622, 510)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(131, 37)
        Me.btnCancel.TabIndex = 597
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSelectUser
        '
        Me.btnSelectUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectUser.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSelectUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSelectUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectUser.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectUser.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnSelectUser.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSelectUser.Location = New System.Drawing.Point(713, 130)
        Me.btnSelectUser.Name = "btnSelectUser"
        Me.btnSelectUser.Size = New System.Drawing.Size(40, 27)
        Me.btnSelectUser.TabIndex = 598
        Me.btnSelectUser.Text = "..."
        Me.btnSelectUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSelectUser.UseVisualStyleBackColor = False
        '
        'picUserProfile
        '
        Me.picUserProfile.Image = CType(resources.GetObject("picUserProfile.Image"), System.Drawing.Image)
        Me.picUserProfile.Location = New System.Drawing.Point(12, 130)
        Me.picUserProfile.Name = "picUserProfile"
        Me.picUserProfile.Size = New System.Drawing.Size(138, 111)
        Me.picUserProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picUserProfile.TabIndex = 601
        Me.picUserProfile.TabStop = False
        '
        'txtNameOfRepresentative
        '
        Me.txtNameOfRepresentative.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtNameOfRepresentative.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNameOfRepresentative.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameOfRepresentative.Location = New System.Drawing.Point(156, 319)
        Me.txtNameOfRepresentative.Name = "txtNameOfRepresentative"
        Me.txtNameOfRepresentative.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtNameOfRepresentative.Size = New System.Drawing.Size(551, 28)
        Me.txtNameOfRepresentative.TabIndex = 610
        '
        'btnSubmit
        '
        Me.btnSubmit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSubmit.BackColor = System.Drawing.Color.Navy
        Me.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubmit.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubmit.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSubmit.Location = New System.Drawing.Point(485, 510)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(131, 37)
        Me.btnSubmit.TabIndex = 611
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSubmit.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(765, 60)
        Me.Panel4.TabIndex = 612
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(67, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(177, 19)
        Me.Label3.TabIndex = 520
        Me.Label3.Text = "Create a New Appointment"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(11, 11)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(50, 41)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 518
        Me.PictureBox2.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(66, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(132, 22)
        Me.Label9.TabIndex = 519
        Me.Label9.Text = "Create Request"
        '
        'btnSelectRepresentative
        '
        Me.btnSelectRepresentative.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectRepresentative.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSelectRepresentative.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSelectRepresentative.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectRepresentative.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectRepresentative.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnSelectRepresentative.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSelectRepresentative.Location = New System.Drawing.Point(713, 319)
        Me.btnSelectRepresentative.Name = "btnSelectRepresentative"
        Me.btnSelectRepresentative.Size = New System.Drawing.Size(40, 27)
        Me.btnSelectRepresentative.TabIndex = 613
        Me.btnSelectRepresentative.Text = "..."
        Me.btnSelectRepresentative.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSelectRepresentative.UseVisualStyleBackColor = False
        '
        'object_dcdd2b84_7f89_408d_8299_a12d391d0d36
        '
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.ForeColor = System.Drawing.Color.DarkBlue
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.Location = New System.Drawing.Point(713, 325)
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.Name = "object_dcdd2b84_7f89_408d_8299_a12d391d0d36"
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.Size = New System.Drawing.Size(40, 27)
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.TabIndex = 613
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.Text = "..."
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.UseVisualStyleBackColor = False
        Me.object_dcdd2b84_7f89_408d_8299_a12d391d0d36.Visible = False
        '
        'frmCreateAppointment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(765, 559)
        Me.Controls.Add(Me.btnSelectRepresentative)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.txtNameOfRepresentative)
        Me.Controls.Add(Me.picUserProfile)
        Me.Controls.Add(Me.btnSelectUser)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtPurpose)
        Me.Controls.Add(Me.lblControlNo)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.cboRequestFor)
        Me.Controls.Add(Me.cboRequestType)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCreateAppointment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCreateAppointment"
        CType(Me.picUserProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents cboRequestType As ComboBox
    Friend WithEvents cboRequestFor As ComboBox
    Friend WithEvents txtName As TextBox
    Friend WithEvents lblControlNo As Label
    Friend WithEvents txtPurpose As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSelectUser As Button
    Friend WithEvents picUserProfile As PictureBox
    Friend WithEvents txtNameOfRepresentative As TextBox
    Friend WithEvents btnSubmit As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label9 As Label
    Friend WithEvents btnSelectRepresentative As Button
    Friend WithEvents object_dcdd2b84_7f89_408d_8299_a12d391d0d36 As Button
End Class
