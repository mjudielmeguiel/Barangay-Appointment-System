<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmcreateadmin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmcreateadmin))
        Me.txtFirstname = New System.Windows.Forms.TextBox()
        Me.txtLastname = New System.Windows.Forms.TextBox()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtConfirmPass = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.picUser = New System.Windows.Forms.PictureBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblErrorUsername = New System.Windows.Forms.Label()
        Me.lblErrorLastname = New System.Windows.Forms.Label()
        Me.lblErrorFirstname = New System.Windows.Forms.Label()
        Me.lblPassStatus = New System.Windows.Forms.Label()
        Me.lblErrorConfirmPass = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        CType(Me.picUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtFirstname
        '
        Me.txtFirstname.BackColor = System.Drawing.SystemColors.Control
        Me.txtFirstname.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstname.Location = New System.Drawing.Point(606, 227)
        Me.txtFirstname.Name = "txtFirstname"
        Me.txtFirstname.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtFirstname.Size = New System.Drawing.Size(151, 28)
        Me.txtFirstname.TabIndex = 143
        '
        'txtLastname
        '
        Me.txtLastname.BackColor = System.Drawing.SystemColors.Control
        Me.txtLastname.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastname.Location = New System.Drawing.Point(449, 227)
        Me.txtLastname.Name = "txtLastname"
        Me.txtLastname.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtLastname.Size = New System.Drawing.Size(151, 28)
        Me.txtLastname.TabIndex = 148
        '
        'txtUsername
        '
        Me.txtUsername.BackColor = System.Drawing.SystemColors.Control
        Me.txtUsername.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.Location = New System.Drawing.Point(449, 286)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtUsername.Size = New System.Drawing.Size(308, 28)
        Me.txtUsername.TabIndex = 150
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.SystemColors.Control
        Me.txtPassword.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(449, 340)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtPassword.Size = New System.Drawing.Size(151, 28)
        Me.txtPassword.TabIndex = 152
        '
        'txtConfirmPass
        '
        Me.txtConfirmPass.BackColor = System.Drawing.SystemColors.Control
        Me.txtConfirmPass.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmPass.Location = New System.Drawing.Point(606, 340)
        Me.txtConfirmPass.Name = "txtConfirmPass"
        Me.txtConfirmPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtConfirmPass.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtConfirmPass.Size = New System.Drawing.Size(151, 28)
        Me.txtConfirmPass.TabIndex = 154
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
        Me.btnClose.Location = New System.Drawing.Point(449, 452)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(308, 37)
        Me.btnClose.TabIndex = 606
        Me.btnClose.Text = "Back to Login"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'picUser
        '
        Me.picUser.BackColor = System.Drawing.Color.Transparent
        Me.picUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.picUser.Image = CType(resources.GetObject("picUser.Image"), System.Drawing.Image)
        Me.picUser.Location = New System.Drawing.Point(541, 49)
        Me.picUser.Name = "picUser"
        Me.picUser.Size = New System.Drawing.Size(130, 123)
        Me.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picUser.TabIndex = 607
        Me.picUser.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.Control
        Me.Label11.Location = New System.Drawing.Point(10, 120)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(179, 22)
        Me.Label11.TabIndex = 519
        Me.Label11.Text = "Create a new ADMIN"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.Control
        Me.Label10.Location = New System.Drawing.Point(10, 170)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(118, 19)
        Me.Label10.TabIndex = 520
        Me.Label10.Text = "Please fill all fields"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(409, 554)
        Me.Panel4.TabIndex = 605
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(11, 527)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(199, 17)
        Me.Label5.TabIndex = 521
        Me.Label5.Text = "©2026 BSMS All rights reserved."
        '
        'lblErrorUsername
        '
        Me.lblErrorUsername.AutoSize = True
        Me.lblErrorUsername.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorUsername.Location = New System.Drawing.Point(445, 317)
        Me.lblErrorUsername.Name = "lblErrorUsername"
        Me.lblErrorUsername.Size = New System.Drawing.Size(15, 19)
        Me.lblErrorUsername.TabIndex = 609
        Me.lblErrorUsername.Text = "-"
        '
        'lblErrorLastname
        '
        Me.lblErrorLastname.AutoSize = True
        Me.lblErrorLastname.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorLastname.Location = New System.Drawing.Point(445, 258)
        Me.lblErrorLastname.Name = "lblErrorLastname"
        Me.lblErrorLastname.Size = New System.Drawing.Size(15, 19)
        Me.lblErrorLastname.TabIndex = 610
        Me.lblErrorLastname.Text = "-"
        '
        'lblErrorFirstname
        '
        Me.lblErrorFirstname.AutoSize = True
        Me.lblErrorFirstname.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorFirstname.Location = New System.Drawing.Point(602, 258)
        Me.lblErrorFirstname.Name = "lblErrorFirstname"
        Me.lblErrorFirstname.Size = New System.Drawing.Size(15, 19)
        Me.lblErrorFirstname.TabIndex = 611
        Me.lblErrorFirstname.Text = "-"
        '
        'lblPassStatus
        '
        Me.lblPassStatus.AutoSize = True
        Me.lblPassStatus.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassStatus.Location = New System.Drawing.Point(445, 371)
        Me.lblPassStatus.Name = "lblPassStatus"
        Me.lblPassStatus.Size = New System.Drawing.Size(15, 19)
        Me.lblPassStatus.TabIndex = 612
        Me.lblPassStatus.Text = "-"
        '
        'lblErrorConfirmPass
        '
        Me.lblErrorConfirmPass.AutoSize = True
        Me.lblErrorConfirmPass.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorConfirmPass.Location = New System.Drawing.Point(602, 371)
        Me.lblErrorConfirmPass.Name = "lblErrorConfirmPass"
        Me.lblErrorConfirmPass.Size = New System.Drawing.Size(15, 19)
        Me.lblErrorConfirmPass.TabIndex = 160
        Me.lblErrorConfirmPass.Text = "-"
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
        Me.Button1.Location = New System.Drawing.Point(777, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(30, 28)
        Me.Button1.TabIndex = 613
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.Location = New System.Drawing.Point(449, 409)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(308, 37)
        Me.btnSave.TabIndex = 614
        Me.btnSave.Text = "Create ADMIN"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'frmcreateadmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(809, 554)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblPassStatus)
        Me.Controls.Add(Me.lblErrorFirstname)
        Me.Controls.Add(Me.lblErrorLastname)
        Me.Controls.Add(Me.lblErrorUsername)
        Me.Controls.Add(Me.picUser)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.lblErrorConfirmPass)
        Me.Controls.Add(Me.txtConfirmPass)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.txtLastname)
        Me.Controls.Add(Me.txtFirstname)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmcreateadmin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmcreateadmin"
        CType(Me.picUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtFirstname As TextBox
    Friend WithEvents txtLastname As TextBox
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtConfirmPass As TextBox
    Friend WithEvents btnClose As Button
    Friend WithEvents picUser As PictureBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents lblErrorUsername As Label
    Friend WithEvents lblErrorLastname As Label
    Friend WithEvents lblErrorFirstname As Label
    Friend WithEvents lblPassStatus As Label
    Friend WithEvents lblErrorConfirmPass As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents btnSave As Button
End Class
