<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmcreateuser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmcreateuser))
        Me.picUser = New System.Windows.Forms.PictureBox()
        Me.lblStaffCode = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.lblUsernameError = New System.Windows.Forms.Label()
        Me.lblConfirmPassError = New System.Windows.Forms.Label()
        Me.txtConfirmPass = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtFirstname = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.txtLastname = New System.Windows.Forms.TextBox()
        Me.lblFirstnameError = New System.Windows.Forms.Label()
        Me.lblLastnameError = New System.Windows.Forms.Label()
        Me.lblEmailError = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.cboRole = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboDepartment = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblDepartmentError = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPictureError = New System.Windows.Forms.Label()
        CType(Me.picUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picUser
        '
        Me.picUser.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.picUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.picUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picUser.Location = New System.Drawing.Point(12, 108)
        Me.picUser.Name = "picUser"
        Me.picUser.Size = New System.Drawing.Size(117, 105)
        Me.picUser.TabIndex = 561
        Me.picUser.TabStop = False
        '
        'lblStaffCode
        '
        Me.lblStaffCode.AutoSize = True
        Me.lblStaffCode.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStaffCode.ForeColor = System.Drawing.Color.Navy
        Me.lblStaffCode.Location = New System.Drawing.Point(12, 83)
        Me.lblStaffCode.Name = "lblStaffCode"
        Me.lblStaffCode.Size = New System.Drawing.Size(17, 22)
        Me.lblStaffCode.TabIndex = 554
        Me.lblStaffCode.Text = "-"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft YaHei UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(146, 550)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(304, 38)
        Me.Label4.TabIndex = 553
        Me.Label4.Text = "By signing up, you agree to our Terms of Services" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "and Privacy Policy."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.btnCancel.Location = New System.Drawing.Point(12, 510)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(568, 37)
        Me.btnCancel.TabIndex = 551
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSubmit
        '
        Me.btnSubmit.BackColor = System.Drawing.Color.DarkBlue
        Me.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubmit.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubmit.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSubmit.Location = New System.Drawing.Point(12, 467)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(568, 37)
        Me.btnSubmit.TabIndex = 505
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSubmit.UseVisualStyleBackColor = False
        '
        'lblUsernameError
        '
        Me.lblUsernameError.AutoSize = True
        Me.lblUsernameError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsernameError.Location = New System.Drawing.Point(13, 412)
        Me.lblUsernameError.Name = "lblUsernameError"
        Me.lblUsernameError.Size = New System.Drawing.Size(15, 19)
        Me.lblUsernameError.TabIndex = 517
        Me.lblUsernameError.Text = "-"
        '
        'lblConfirmPassError
        '
        Me.lblConfirmPassError.AutoSize = True
        Me.lblConfirmPassError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfirmPassError.Location = New System.Drawing.Point(402, 412)
        Me.lblConfirmPassError.Name = "lblConfirmPassError"
        Me.lblConfirmPassError.Size = New System.Drawing.Size(15, 19)
        Me.lblConfirmPassError.TabIndex = 516
        Me.lblConfirmPassError.Text = "-"
        '
        'txtConfirmPass
        '
        Me.txtConfirmPass.BackColor = System.Drawing.SystemColors.Control
        Me.txtConfirmPass.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmPass.Location = New System.Drawing.Point(406, 381)
        Me.txtConfirmPass.Name = "txtConfirmPass"
        Me.txtConfirmPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtConfirmPass.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtConfirmPass.Size = New System.Drawing.Size(174, 28)
        Me.txtConfirmPass.TabIndex = 514
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Microsoft YaHei UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(402, 359)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(118, 19)
        Me.Label48.TabIndex = 515
        Me.Label48.Text = "Confirm Password"
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.SystemColors.Control
        Me.txtPassword.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(209, 381)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtPassword.Size = New System.Drawing.Size(191, 28)
        Me.txtPassword.TabIndex = 512
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Microsoft YaHei UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(205, 359)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(65, 19)
        Me.Label52.TabIndex = 513
        Me.Label52.Text = "Password"
        '
        'txtUsername
        '
        Me.txtUsername.BackColor = System.Drawing.SystemColors.Control
        Me.txtUsername.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.Location = New System.Drawing.Point(12, 381)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtUsername.Size = New System.Drawing.Size(191, 28)
        Me.txtUsername.TabIndex = 510
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Font = New System.Drawing.Font("Microsoft YaHei UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(351, 108)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(67, 19)
        Me.Label53.TabIndex = 507
        Me.Label53.Text = "Firstname"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Font = New System.Drawing.Font("Microsoft YaHei UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(8, 359)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(69, 19)
        Me.Label54.TabIndex = 511
        Me.Label54.Text = "Username"
        '
        'txtFirstname
        '
        Me.txtFirstname.BackColor = System.Drawing.SystemColors.Control
        Me.txtFirstname.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstname.Location = New System.Drawing.Point(355, 130)
        Me.txtFirstname.Name = "txtFirstname"
        Me.txtFirstname.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtFirstname.Size = New System.Drawing.Size(218, 28)
        Me.txtFirstname.TabIndex = 506
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Font = New System.Drawing.Font("Microsoft YaHei UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(135, 108)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(65, 19)
        Me.Label60.TabIndex = 509
        Me.Label60.Text = "Lastname"
        '
        'txtLastname
        '
        Me.txtLastname.BackColor = System.Drawing.SystemColors.Control
        Me.txtLastname.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastname.Location = New System.Drawing.Point(137, 130)
        Me.txtLastname.Name = "txtLastname"
        Me.txtLastname.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtLastname.Size = New System.Drawing.Size(212, 28)
        Me.txtLastname.TabIndex = 508
        '
        'lblFirstnameError
        '
        Me.lblFirstnameError.AutoSize = True
        Me.lblFirstnameError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFirstnameError.Location = New System.Drawing.Point(351, 161)
        Me.lblFirstnameError.Name = "lblFirstnameError"
        Me.lblFirstnameError.Size = New System.Drawing.Size(15, 19)
        Me.lblFirstnameError.TabIndex = 518
        Me.lblFirstnameError.Text = "-"
        '
        'lblLastnameError
        '
        Me.lblLastnameError.AutoSize = True
        Me.lblLastnameError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastnameError.Location = New System.Drawing.Point(133, 160)
        Me.lblLastnameError.Name = "lblLastnameError"
        Me.lblLastnameError.Size = New System.Drawing.Size(15, 19)
        Me.lblLastnameError.TabIndex = 519
        Me.lblLastnameError.Text = "-"
        '
        'lblEmailError
        '
        Me.lblEmailError.AutoSize = True
        Me.lblEmailError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmailError.Location = New System.Drawing.Point(136, 242)
        Me.lblEmailError.Name = "lblEmailError"
        Me.lblEmailError.Size = New System.Drawing.Size(15, 19)
        Me.lblEmailError.TabIndex = 522
        Me.lblEmailError.Text = "-"
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Font = New System.Drawing.Font("Microsoft YaHei UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.Location = New System.Drawing.Point(136, 189)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(41, 19)
        Me.Label74.TabIndex = 521
        Me.Label74.Text = "Email"
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Control
        Me.txtEmail.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(139, 211)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtEmail.Size = New System.Drawing.Size(441, 28)
        Me.txtEmail.TabIndex = 520
        '
        'cboRole
        '
        Me.cboRole.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboRole.Font = New System.Drawing.Font("Microsoft YaHei UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRole.FormattingEnabled = True
        Me.cboRole.Location = New System.Drawing.Point(355, 286)
        Me.cboRole.Name = "cboRole"
        Me.cboRole.Size = New System.Drawing.Size(225, 28)
        Me.cboRole.TabIndex = 565
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft YaHei UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(351, 264)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 19)
        Me.Label7.TabIndex = 566
        Me.Label7.Text = "Role"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(351, 317)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 19)
        Me.Label8.TabIndex = 567
        Me.Label8.Text = "-"
        '
        'cboDepartment
        '
        Me.cboDepartment.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboDepartment.Font = New System.Drawing.Font("Microsoft YaHei UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDepartment.FormattingEnabled = True
        Me.cboDepartment.Location = New System.Drawing.Point(12, 286)
        Me.cboDepartment.Name = "cboDepartment"
        Me.cboDepartment.Size = New System.Drawing.Size(337, 28)
        Me.cboDepartment.TabIndex = 568
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft YaHei UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(12, 264)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 19)
        Me.Label9.TabIndex = 569
        Me.Label9.Text = "Department"
        '
        'lblDepartmentError
        '
        Me.lblDepartmentError.AutoSize = True
        Me.lblDepartmentError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartmentError.Location = New System.Drawing.Point(13, 317)
        Me.lblDepartmentError.Name = "lblDepartmentError"
        Me.lblDepartmentError.Size = New System.Drawing.Size(15, 19)
        Me.lblDepartmentError.TabIndex = 570
        Me.lblDepartmentError.Text = "-"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(592, 60)
        Me.Panel4.TabIndex = 571
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(67, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(132, 19)
        Me.Label2.TabIndex = 520
        Me.Label2.Text = "Fill user Information"
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(66, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 22)
        Me.Label1.TabIndex = 519
        Me.Label1.Text = "Create User"
        '
        'lblPictureError
        '
        Me.lblPictureError.AutoSize = True
        Me.lblPictureError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPictureError.Location = New System.Drawing.Point(12, 242)
        Me.lblPictureError.Name = "lblPictureError"
        Me.lblPictureError.Size = New System.Drawing.Size(15, 19)
        Me.lblPictureError.TabIndex = 572
        Me.lblPictureError.Text = "-"
        '
        'frmcreateuser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(592, 608)
        Me.Controls.Add(Me.lblPictureError)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.cboDepartment)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblDepartmentError)
        Me.Controls.Add(Me.cboRole)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.picUser)
        Me.Controls.Add(Me.lblStaffCode)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.lblUsernameError)
        Me.Controls.Add(Me.lblConfirmPassError)
        Me.Controls.Add(Me.txtConfirmPass)
        Me.Controls.Add(Me.Label48)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.Label52)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.Label53)
        Me.Controls.Add(Me.Label54)
        Me.Controls.Add(Me.txtFirstname)
        Me.Controls.Add(Me.Label60)
        Me.Controls.Add(Me.txtLastname)
        Me.Controls.Add(Me.lblFirstnameError)
        Me.Controls.Add(Me.lblLastnameError)
        Me.Controls.Add(Me.lblEmailError)
        Me.Controls.Add(Me.Label74)
        Me.Controls.Add(Me.txtEmail)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmcreateuser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmcreateuser"
        CType(Me.picUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picUser As PictureBox
    Friend WithEvents lblStaffCode As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSubmit As Button
    Friend WithEvents lblUsernameError As Label
    Friend WithEvents lblConfirmPassError As Label
    Friend WithEvents txtConfirmPass As TextBox
    Friend WithEvents Label48 As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Label52 As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents Label53 As Label
    Friend WithEvents Label54 As Label
    Friend WithEvents txtFirstname As TextBox
    Friend WithEvents Label60 As Label
    Friend WithEvents txtLastname As TextBox
    Friend WithEvents lblFirstnameError As Label
    Friend WithEvents lblLastnameError As Label
    Friend WithEvents lblEmailError As Label
    Friend WithEvents Label74 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents cboRole As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cboDepartment As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents lblDepartmentError As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblPictureError As Label
End Class
