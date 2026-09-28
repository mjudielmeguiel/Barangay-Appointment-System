<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmforgotpassword
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmforgotpassword))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblPassStatus = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.lblDepartment = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.picUser = New System.Windows.Forms.PictureBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSingle = New System.Windows.Forms.Button()
        Me.lblErrorConfirmPass = New System.Windows.Forms.Label()
        Me.txtConfirmPass = New System.Windows.Forms.TextBox()
        Me.txtNewPassword = New System.Windows.Forms.TextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.lblRole = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblInfoStatus = New System.Windows.Forms.Label()
        Me.lblTicketNumber = New System.Windows.Forms.Label()
        Me.txtUserInput = New System.Windows.Forms.TextBox()
        Me.lblVerifyStatus = New System.Windows.Forms.Label()
        Me.Panel4.SuspendLayout()
        CType(Me.picUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Button1.Location = New System.Drawing.Point(778, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(30, 28)
        Me.Button1.TabIndex = 628
        Me.Button1.UseVisualStyleBackColor = False
        '
        'lblPassStatus
        '
        Me.lblPassStatus.AutoSize = True
        Me.lblPassStatus.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassStatus.Location = New System.Drawing.Point(446, 371)
        Me.lblPassStatus.Name = "lblPassStatus"
        Me.lblPassStatus.Size = New System.Drawing.Size(15, 19)
        Me.lblPassStatus.TabIndex = 627
        Me.lblPassStatus.Text = "-"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(446, 171)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(68, 19)
        Me.lblName.TabIndex = 626
        Me.lblName.Text = "Full name"
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.Location = New System.Drawing.Point(645, 171)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(71, 19)
        Me.lblUsername.TabIndex = 625
        Me.lblUsername.Text = "Username"
        '
        'lblDepartment
        '
        Me.lblDepartment.AutoSize = True
        Me.lblDepartment.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(446, 243)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(83, 19)
        Me.lblDepartment.TabIndex = 624
        Me.lblDepartment.Text = "Department"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.MidnightBlue
        Me.Label5.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(11, 527)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(199, 17)
        Me.Label5.TabIndex = 521
        Me.Label5.Text = "©2026 BSMS All rights reserved."
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.MidnightBlue
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(409, 554)
        Me.Panel4.TabIndex = 621
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.Control
        Me.Label10.Location = New System.Drawing.Point(10, 170)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(118, 19)
        Me.Label10.TabIndex = 520
        Me.Label10.Text = "Please fill all fields"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.Control
        Me.Label11.Location = New System.Drawing.Point(10, 120)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(150, 22)
        Me.Label11.TabIndex = 519
        Me.Label11.Text = "Forgot Password"
        '
        'picUser
        '
        Me.picUser.BackColor = System.Drawing.Color.Transparent
        Me.picUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.picUser.Image = CType(resources.GetObject("picUser.Image"), System.Drawing.Image)
        Me.picUser.Location = New System.Drawing.Point(545, 53)
        Me.picUser.Name = "picUser"
        Me.picUser.Size = New System.Drawing.Size(115, 104)
        Me.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picUser.TabIndex = 623
        Me.picUser.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.Location = New System.Drawing.Point(450, 452)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(308, 37)
        Me.btnClose.TabIndex = 622
        Me.btnClose.Text = "Back to Login"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSingle
        '
        Me.btnSingle.BackColor = System.Drawing.Color.MidnightBlue
        Me.btnSingle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSingle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSingle.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSingle.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSingle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSingle.Location = New System.Drawing.Point(450, 409)
        Me.btnSingle.Name = "btnSingle"
        Me.btnSingle.Size = New System.Drawing.Size(308, 37)
        Me.btnSingle.TabIndex = 620
        Me.btnSingle.Text = "Generate Ticket"
        Me.btnSingle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSingle.UseVisualStyleBackColor = False
        '
        'lblErrorConfirmPass
        '
        Me.lblErrorConfirmPass.AutoSize = True
        Me.lblErrorConfirmPass.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorConfirmPass.Location = New System.Drawing.Point(603, 371)
        Me.lblErrorConfirmPass.Name = "lblErrorConfirmPass"
        Me.lblErrorConfirmPass.Size = New System.Drawing.Size(15, 19)
        Me.lblErrorConfirmPass.TabIndex = 619
        Me.lblErrorConfirmPass.Text = "-"
        '
        'txtConfirmPass
        '
        Me.txtConfirmPass.BackColor = System.Drawing.SystemColors.Control
        Me.txtConfirmPass.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmPass.Location = New System.Drawing.Point(607, 340)
        Me.txtConfirmPass.Name = "txtConfirmPass"
        Me.txtConfirmPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtConfirmPass.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtConfirmPass.Size = New System.Drawing.Size(151, 28)
        Me.txtConfirmPass.TabIndex = 618
        '
        'txtNewPassword
        '
        Me.txtNewPassword.BackColor = System.Drawing.SystemColors.Control
        Me.txtNewPassword.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPassword.Location = New System.Drawing.Point(450, 340)
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtNewPassword.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtNewPassword.Size = New System.Drawing.Size(151, 28)
        Me.txtNewPassword.TabIndex = 617
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.Location = New System.Drawing.Point(446, 207)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(41, 19)
        Me.lblEmail.TabIndex = 629
        Me.lblEmail.Text = "Email"
        '
        'lblRole
        '
        Me.lblRole.AutoSize = True
        Me.lblRole.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRole.Location = New System.Drawing.Point(645, 207)
        Me.lblRole.Name = "lblRole"
        Me.lblRole.Size = New System.Drawing.Size(35, 19)
        Me.lblRole.TabIndex = 630
        Me.lblRole.Text = "Role"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(645, 243)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(48, 19)
        Me.lblStatus.TabIndex = 631
        Me.lblStatus.Text = "Status"
        '
        'lblInfoStatus
        '
        Me.lblInfoStatus.AutoSize = True
        Me.lblInfoStatus.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfoStatus.Location = New System.Drawing.Point(446, 272)
        Me.lblInfoStatus.Name = "lblInfoStatus"
        Me.lblInfoStatus.Size = New System.Drawing.Size(48, 19)
        Me.lblInfoStatus.TabIndex = 632
        Me.lblInfoStatus.Text = "Status"
        '
        'lblTicketNumber
        '
        Me.lblTicketNumber.AutoSize = True
        Me.lblTicketNumber.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTicketNumber.Location = New System.Drawing.Point(691, 65)
        Me.lblTicketNumber.Name = "lblTicketNumber"
        Me.lblTicketNumber.Size = New System.Drawing.Size(100, 19)
        Me.lblTicketNumber.TabIndex = 633
        Me.lblTicketNumber.Text = "Ticket Number"
        '
        'txtUserInput
        '
        Me.txtUserInput.BackColor = System.Drawing.SystemColors.Control
        Me.txtUserInput.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserInput.Location = New System.Drawing.Point(450, 306)
        Me.txtUserInput.Name = "txtUserInput"
        Me.txtUserInput.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtUserInput.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtUserInput.Size = New System.Drawing.Size(308, 28)
        Me.txtUserInput.TabIndex = 634
        '
        'lblVerifyStatus
        '
        Me.lblVerifyStatus.AutoSize = True
        Me.lblVerifyStatus.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVerifyStatus.Location = New System.Drawing.Point(691, 94)
        Me.lblVerifyStatus.Name = "lblVerifyStatus"
        Me.lblVerifyStatus.Size = New System.Drawing.Size(48, 19)
        Me.lblVerifyStatus.TabIndex = 635
        Me.lblVerifyStatus.Text = "Status"
        '
        'frmforgotpassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(809, 554)
        Me.Controls.Add(Me.lblVerifyStatus)
        Me.Controls.Add(Me.txtUserInput)
        Me.Controls.Add(Me.lblTicketNumber)
        Me.Controls.Add(Me.lblInfoStatus)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblRole)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblPassStatus)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.lblDepartment)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.picUser)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSingle)
        Me.Controls.Add(Me.lblErrorConfirmPass)
        Me.Controls.Add(Me.txtConfirmPass)
        Me.Controls.Add(Me.txtNewPassword)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmforgotpassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmForgotPassword"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.picUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents lblPassStatus As Label
    Friend WithEvents lblName As Label
    Friend WithEvents lblUsername As Label
    Friend WithEvents lblDepartment As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents picUser As PictureBox
    Friend WithEvents btnClose As Button
    Friend WithEvents btnSingle As Button
    Friend WithEvents lblErrorConfirmPass As Label
    Friend WithEvents txtConfirmPass As TextBox
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents lblRole As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblInfoStatus As Label
    Friend WithEvents lblTicketNumber As Label
    Friend WithEvents txtUserInput As TextBox
    Friend WithEvents lblVerifyStatus As Label
End Class
