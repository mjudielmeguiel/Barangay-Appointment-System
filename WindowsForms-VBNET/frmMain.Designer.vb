<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.picProfile = New System.Windows.Forms.PictureBox()
        Me.lblFullname = New System.Windows.Forms.Label()
        Me.btnToggleSidebar = New System.Windows.Forms.Button()
        Me.lblUserRole = New System.Windows.Forms.Label()
        Me.menuItemsPanel = New System.Windows.Forms.Panel()
        Me.btnDocuments = New System.Windows.Forms.Button()
        Me.btnHistory = New System.Windows.Forms.Button()
        Me.btnCalendar = New System.Windows.Forms.Button()
        Me.btnPayments = New System.Windows.Forms.Button()
        Me.btnUsers = New System.Windows.Forms.Button()
        Me.btnResidents = New System.Windows.Forms.Button()
        Me.btnHome = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.sidebarPanel = New System.Windows.Forms.Panel()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menuItemsPanel.SuspendLayout()
        Me.sidebarPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.picProfile)
        Me.Panel1.Controls.Add(Me.lblFullname)
        Me.Panel1.Controls.Add(Me.btnToggleSidebar)
        Me.Panel1.Controls.Add(Me.lblUserRole)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1366, 60)
        Me.Panel1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(139, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 19)
        Me.Label2.TabIndex = 523
        Me.Label2.Text = "City of Muntinlupa"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(83, 10)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(50, 41)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 521
        Me.PictureBox2.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(138, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(153, 22)
        Me.Label9.TabIndex = 522
        Me.Label9.Text = "Barangay Putatan"
        '
        'picProfile
        '
        Me.picProfile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picProfile.Image = CType(resources.GetObject("picProfile.Image"), System.Drawing.Image)
        Me.picProfile.Location = New System.Drawing.Point(1082, 10)
        Me.picProfile.Name = "picProfile"
        Me.picProfile.Size = New System.Drawing.Size(45, 44)
        Me.picProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picProfile.TabIndex = 0
        Me.picProfile.TabStop = False
        '
        'lblFullname
        '
        Me.lblFullname.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFullname.AutoSize = True
        Me.lblFullname.Font = New System.Drawing.Font("Microsoft YaHei UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFullname.ForeColor = System.Drawing.Color.Navy
        Me.lblFullname.Location = New System.Drawing.Point(1133, 10)
        Me.lblFullname.Name = "lblFullname"
        Me.lblFullname.Size = New System.Drawing.Size(82, 19)
        Me.lblFullname.TabIndex = 163
        Me.lblFullname.Text = "Full name"
        '
        'btnToggleSidebar
        '
        Me.btnToggleSidebar.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnToggleSidebar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnToggleSidebar.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnToggleSidebar.FlatAppearance.BorderSize = 0
        Me.btnToggleSidebar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnToggleSidebar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnToggleSidebar.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnToggleSidebar.Image = CType(resources.GetObject("btnToggleSidebar.Image"), System.Drawing.Image)
        Me.btnToggleSidebar.Location = New System.Drawing.Point(0, 0)
        Me.btnToggleSidebar.Name = "btnToggleSidebar"
        Me.btnToggleSidebar.Size = New System.Drawing.Size(77, 60)
        Me.btnToggleSidebar.TabIndex = 142
        Me.btnToggleSidebar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnToggleSidebar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnToggleSidebar.UseVisualStyleBackColor = False
        '
        'lblUserRole
        '
        Me.lblUserRole.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUserRole.AutoSize = True
        Me.lblUserRole.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserRole.ForeColor = System.Drawing.Color.Navy
        Me.lblUserRole.Location = New System.Drawing.Point(1133, 35)
        Me.lblUserRole.Name = "lblUserRole"
        Me.lblUserRole.Size = New System.Drawing.Size(39, 19)
        Me.lblUserRole.TabIndex = 164
        Me.lblUserRole.Text = "Role"
        '
        'menuItemsPanel
        '
        Me.menuItemsPanel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.menuItemsPanel.Controls.Add(Me.btnDocuments)
        Me.menuItemsPanel.Controls.Add(Me.btnHistory)
        Me.menuItemsPanel.Controls.Add(Me.btnCalendar)
        Me.menuItemsPanel.Controls.Add(Me.btnPayments)
        Me.menuItemsPanel.Controls.Add(Me.btnUsers)
        Me.menuItemsPanel.Controls.Add(Me.btnResidents)
        Me.menuItemsPanel.Controls.Add(Me.btnHome)
        Me.menuItemsPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.menuItemsPanel.Location = New System.Drawing.Point(0, 60)
        Me.menuItemsPanel.Name = "menuItemsPanel"
        Me.menuItemsPanel.Size = New System.Drawing.Size(77, 708)
        Me.menuItemsPanel.TabIndex = 2
        '
        'btnDocuments
        '
        Me.btnDocuments.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnDocuments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnDocuments.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnDocuments.FlatAppearance.BorderSize = 0
        Me.btnDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDocuments.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDocuments.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnDocuments.Image = CType(resources.GetObject("btnDocuments.Image"), System.Drawing.Image)
        Me.btnDocuments.Location = New System.Drawing.Point(0, 366)
        Me.btnDocuments.Name = "btnDocuments"
        Me.btnDocuments.Size = New System.Drawing.Size(77, 61)
        Me.btnDocuments.TabIndex = 154
        Me.btnDocuments.Text = "Documents"
        Me.btnDocuments.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDocuments.UseVisualStyleBackColor = False
        '
        'btnHistory
        '
        Me.btnHistory.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnHistory.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnHistory.FlatAppearance.BorderSize = 0
        Me.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistory.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnHistory.Image = CType(resources.GetObject("btnHistory.Image"), System.Drawing.Image)
        Me.btnHistory.Location = New System.Drawing.Point(0, 305)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(77, 61)
        Me.btnHistory.TabIndex = 152
        Me.btnHistory.Text = "History"
        Me.btnHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHistory.UseVisualStyleBackColor = False
        '
        'btnCalendar
        '
        Me.btnCalendar.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnCalendar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCalendar.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnCalendar.FlatAppearance.BorderSize = 0
        Me.btnCalendar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCalendar.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalendar.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnCalendar.Image = CType(resources.GetObject("btnCalendar.Image"), System.Drawing.Image)
        Me.btnCalendar.Location = New System.Drawing.Point(0, 244)
        Me.btnCalendar.Name = "btnCalendar"
        Me.btnCalendar.Size = New System.Drawing.Size(77, 61)
        Me.btnCalendar.TabIndex = 150
        Me.btnCalendar.Text = "Calendar"
        Me.btnCalendar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCalendar.UseVisualStyleBackColor = False
        '
        'btnPayments
        '
        Me.btnPayments.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnPayments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnPayments.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPayments.FlatAppearance.BorderSize = 0
        Me.btnPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPayments.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPayments.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnPayments.Image = CType(resources.GetObject("btnPayments.Image"), System.Drawing.Image)
        Me.btnPayments.Location = New System.Drawing.Point(0, 183)
        Me.btnPayments.Name = "btnPayments"
        Me.btnPayments.Size = New System.Drawing.Size(77, 61)
        Me.btnPayments.TabIndex = 153
        Me.btnPayments.Text = "Payments"
        Me.btnPayments.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPayments.UseVisualStyleBackColor = False
        '
        'btnUsers
        '
        Me.btnUsers.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnUsers.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnUsers.FlatAppearance.BorderSize = 0
        Me.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUsers.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUsers.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnUsers.Image = CType(resources.GetObject("btnUsers.Image"), System.Drawing.Image)
        Me.btnUsers.Location = New System.Drawing.Point(0, 122)
        Me.btnUsers.Name = "btnUsers"
        Me.btnUsers.Size = New System.Drawing.Size(77, 61)
        Me.btnUsers.TabIndex = 155
        Me.btnUsers.Text = "Users"
        Me.btnUsers.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnUsers.UseVisualStyleBackColor = False
        '
        'btnResidents
        '
        Me.btnResidents.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnResidents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnResidents.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnResidents.FlatAppearance.BorderSize = 0
        Me.btnResidents.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResidents.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnResidents.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnResidents.Image = CType(resources.GetObject("btnResidents.Image"), System.Drawing.Image)
        Me.btnResidents.Location = New System.Drawing.Point(0, 61)
        Me.btnResidents.Name = "btnResidents"
        Me.btnResidents.Size = New System.Drawing.Size(77, 61)
        Me.btnResidents.TabIndex = 149
        Me.btnResidents.Text = "Residents"
        Me.btnResidents.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnResidents.UseVisualStyleBackColor = False
        '
        'btnHome
        '
        Me.btnHome.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnHome.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnHome.FlatAppearance.BorderSize = 0
        Me.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHome.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHome.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnHome.Image = CType(resources.GetObject("btnHome.Image"), System.Drawing.Image)
        Me.btnHome.Location = New System.Drawing.Point(0, 0)
        Me.btnHome.Name = "btnHome"
        Me.btnHome.Size = New System.Drawing.Size(77, 61)
        Me.btnHome.TabIndex = 143
        Me.btnHome.Text = "Home"
        Me.btnHome.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHome.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(319, 60)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1047, 708)
        Me.Panel2.TabIndex = 3
        '
        'sidebarPanel
        '
        Me.sidebarPanel.Controls.Add(Me.Button4)
        Me.sidebarPanel.Controls.Add(Me.Panel3)
        Me.sidebarPanel.Controls.Add(Me.Button3)
        Me.sidebarPanel.Controls.Add(Me.Button2)
        Me.sidebarPanel.Controls.Add(Me.Button1)
        Me.sidebarPanel.Controls.Add(Me.Button5)
        Me.sidebarPanel.Controls.Add(Me.btnClose)
        Me.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.sidebarPanel.Location = New System.Drawing.Point(77, 60)
        Me.sidebarPanel.Name = "sidebarPanel"
        Me.sidebarPanel.Size = New System.Drawing.Size(242, 708)
        Me.sidebarPanel.TabIndex = 0
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.Location = New System.Drawing.Point(0, 342)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(242, 61)
        Me.Button4.TabIndex = 168
        Me.Button4.Text = "  Change Password"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(242, 137)
        Me.Panel3.TabIndex = 0
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button3.Location = New System.Drawing.Point(0, 403)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(242, 61)
        Me.Button3.TabIndex = 167
        Me.Button3.Text = "  Activity Log"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.Location = New System.Drawing.Point(0, 464)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(242, 61)
        Me.Button2.TabIndex = 166
        Me.Button2.Text = "  Report Generation"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(0, 525)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(242, 61)
        Me.Button1.TabIndex = 165
        Me.Button1.Text = "  Roles"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button5.Location = New System.Drawing.Point(0, 586)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(242, 61)
        Me.Button5.TabIndex = 169
        Me.Button5.Text = "  Deactivate Account"
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Button5.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.Location = New System.Drawing.Point(0, 647)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(242, 61)
        Me.btnClose.TabIndex = 156
        Me.btnClose.Text = "  Sign out"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.sidebarPanel)
        Me.Controls.Add(Me.menuItemsPanel)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmMain"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menuItemsPanel.ResumeLayout(False)
        Me.sidebarPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnToggleSidebar As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnResidents As Button
    Friend WithEvents btnCalendar As Button
    Friend WithEvents btnHistory As Button
    Friend WithEvents btnHome As Button
    Friend WithEvents btnPayments As Button
    Friend WithEvents btnDocuments As Button
    Friend WithEvents btnUsers As Button
    Friend WithEvents menuItemsPanel As Panel
    Friend WithEvents sidebarPanel As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents picProfile As PictureBox
    Friend WithEvents lblUserRole As Label
    Friend WithEvents lblFullname As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label9 As Label
End Class
