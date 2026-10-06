<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAdmin_Dashboard
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdmin_Dashboard))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblDateTime = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dgvTickets = New System.Windows.Forms.DataGridView()
        Me.dgvActivityLogs = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvUsers = New System.Windows.Forms.DataGridView()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblnewResident = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblAccountRecovery = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblUsers = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblFullname = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTotalAppointments = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblCancelledWalkIn = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblOnline = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCancelledOnline = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblWalkIn = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblActive = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.lblLocked = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblStaff = New System.Windows.Forms.Label()
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66 = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvTickets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvActivityLogs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel11.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        CType(Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'lblDateTime
        '
        Me.lblDateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDateTime.AutoSize = True
        Me.lblDateTime.Font = New System.Drawing.Font("Microsoft YaHei UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateTime.ForeColor = System.Drawing.Color.Navy
        Me.lblDateTime.Location = New System.Drawing.Point(937, 37)
        Me.lblDateTime.Name = "lblDateTime"
        Me.lblDateTime.Size = New System.Drawing.Size(20, 26)
        Me.lblDateTime.TabIndex = 179
        Me.lblDateTime.Text = "-"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.Panel15)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.dgvTickets)
        Me.Panel1.Controls.Add(Me.dgvActivityLogs)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.dgvUsers)
        Me.Panel1.Controls.Add(Me.lblTime)
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Panel9)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.lblFullname)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Panel11)
        Me.Panel1.Controls.Add(Me.Panel10)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Controls.Add(Me.Panel12)
        Me.Panel1.Controls.Add(Me.Panel13)
        Me.Panel1.Controls.Add(Me.Panel14)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1366, 768)
        Me.Panel1.TabIndex = 520
        '
        'Panel15
        '
        Me.Panel15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel15.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel15.Location = New System.Drawing.Point(997, 756)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(325, 1109)
        Me.Panel15.TabIndex = 748
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label25.Location = New System.Drawing.Point(60, 1116)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(208, 21)
        Me.Label25.TabIndex = 751
        Me.Label25.Text = "Account Recovery Request"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(60, 1524)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(161, 21)
        Me.Label17.TabIndex = 750
        Me.Label17.Text = "Activity System Logs"
        '
        'dgvTickets
        '
        Me.dgvTickets.AllowUserToAddRows = False
        Me.dgvTickets.AllowUserToDeleteRows = False
        Me.dgvTickets.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTickets.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvTickets.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTickets.Location = New System.Drawing.Point(64, 1168)
        Me.dgvTickets.Name = "dgvTickets"
        Me.dgvTickets.ReadOnly = True
        Me.dgvTickets.Size = New System.Drawing.Size(908, 290)
        Me.dgvTickets.TabIndex = 749
        '
        'dgvActivityLogs
        '
        Me.dgvActivityLogs.AllowUserToAddRows = False
        Me.dgvActivityLogs.AllowUserToDeleteRows = False
        Me.dgvActivityLogs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvActivityLogs.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvActivityLogs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvActivityLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvActivityLogs.Location = New System.Drawing.Point(64, 1575)
        Me.dgvActivityLogs.Name = "dgvActivityLogs"
        Me.dgvActivityLogs.ReadOnly = True
        Me.dgvActivityLogs.Size = New System.Drawing.Size(908, 290)
        Me.dgvActivityLogs.TabIndex = 748
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(60, 706)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(194, 21)
        Me.Label3.TabIndex = 747
        Me.Label3.Text = "User Accounts Active List"
        '
        'dgvUsers
        '
        Me.dgvUsers.AllowUserToAddRows = False
        Me.dgvUsers.AllowUserToDeleteRows = False
        Me.dgvUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUsers.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUsers.Location = New System.Drawing.Point(64, 756)
        Me.dgvUsers.Name = "dgvUsers"
        Me.dgvUsers.ReadOnly = True
        Me.dgvUsers.Size = New System.Drawing.Size(908, 290)
        Me.dgvUsers.TabIndex = 745
        '
        'lblTime
        '
        Me.lblTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblTime.Location = New System.Drawing.Point(960, 151)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(84, 21)
        Me.lblTime.TabIndex = 744
        Me.lblTime.Text = "Date/time"
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.Label24)
        Me.Panel6.Controls.Add(Me.Label18)
        Me.Panel6.Controls.Add(Me.lblnewResident)
        Me.Panel6.Location = New System.Drawing.Point(997, 321)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(325, 191)
        Me.Panel6.TabIndex = 639
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(16, 21)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(240, 25)
        Me.Label24.TabIndex = 748
        Me.Label24.Text = "New Residents Registered"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(20, 46)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(190, 17)
        Me.Label18.TabIndex = 746
        Me.Label18.Text = "New Residence Portal Created"
        '
        'lblnewResident
        '
        Me.lblnewResident.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblnewResident.AutoSize = True
        Me.lblnewResident.Font = New System.Drawing.Font("Segoe UI", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnewResident.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblnewResident.Location = New System.Drawing.Point(14, 72)
        Me.lblnewResident.Name = "lblnewResident"
        Me.lblnewResident.Size = New System.Drawing.Size(43, 50)
        Me.lblnewResident.TabIndex = 525
        Me.lblnewResident.Text = "0"
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.lblAccountRecovery)
        Me.Panel5.Controls.Add(Me.Label19)
        Me.Panel5.Controls.Add(Me.Label21)
        Me.Panel5.Location = New System.Drawing.Point(997, 534)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(325, 191)
        Me.Panel5.TabIndex = 638
        '
        'lblAccountRecovery
        '
        Me.lblAccountRecovery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAccountRecovery.AutoSize = True
        Me.lblAccountRecovery.Font = New System.Drawing.Font("Segoe UI", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountRecovery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblAccountRecovery.Location = New System.Drawing.Point(14, 67)
        Me.lblAccountRecovery.Name = "lblAccountRecovery"
        Me.lblAccountRecovery.Size = New System.Drawing.Size(43, 50)
        Me.lblAccountRecovery.TabIndex = 747
        Me.lblAccountRecovery.Text = "0"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(18, 41)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(106, 17)
        Me.Label19.TabIndex = 527
        Me.Label19.Text = "Take action now"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(16, 16)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(249, 25)
        Me.Label21.TabIndex = 745
        Me.Label21.Text = "Account Recovery Request"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(60, 485)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(173, 21)
        Me.Label14.TabIndex = 743
        Me.Label14.Text = "Appointment Services"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(60, 264)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(108, 21)
        Me.Label12.TabIndex = 521
        Me.Label12.Text = "User Account"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.Label8)
        Me.Panel9.Controls.Add(Me.lblUsers)
        Me.Panel9.Location = New System.Drawing.Point(64, 321)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(170, 86)
        Me.Panel9.TabIndex = 636
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(12, 51)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 17)
        Me.Label8.TabIndex = 164
        Me.Label8.Text = "Request"
        '
        'lblUsers
        '
        Me.lblUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUsers.AutoSize = True
        Me.lblUsers.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblUsers.Location = New System.Drawing.Point(10, 10)
        Me.lblUsers.Name = "lblUsers"
        Me.lblUsers.Size = New System.Drawing.Size(20, 25)
        Me.lblUsers.TabIndex = 524
        Me.lblUsers.Text = "-"
        '
        'Label11
        '
        Me.Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(963, 13679)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(20, 25)
        Me.Label11.TabIndex = 529
        Me.Label11.Text = "-"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(87, 101)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(119, 119)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 741
        Me.PictureBox1.TabStop = False
        '
        'lblFullname
        '
        Me.lblFullname.AutoSize = True
        Me.lblFullname.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFullname.ForeColor = System.Drawing.Color.DarkRed
        Me.lblFullname.Location = New System.Drawing.Point(385, 135)
        Me.lblFullname.Name = "lblFullname"
        Me.lblFullname.Size = New System.Drawing.Size(28, 37)
        Me.lblFullname.TabIndex = 649
        Me.lblFullname.Text = "-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(242, 135)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 37)
        Me.Label1.TabIndex = 521
        Me.Label1.Text = "Welcome,"
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel11.Controls.Add(Me.Label7)
        Me.Panel11.Controls.Add(Me.lblTotalAppointments)
        Me.Panel11.Location = New System.Drawing.Point(434, 534)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(170, 86)
        Me.Panel11.TabIndex = 642
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(15, 51)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(128, 17)
        Me.Label7.TabIndex = 164
        Me.Label7.Text = "Total Appointments"
        '
        'lblTotalAppointments
        '
        Me.lblTotalAppointments.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalAppointments.AutoSize = True
        Me.lblTotalAppointments.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAppointments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblTotalAppointments.Location = New System.Drawing.Point(13, 10)
        Me.lblTotalAppointments.Name = "lblTotalAppointments"
        Me.lblTotalAppointments.Size = New System.Drawing.Size(20, 25)
        Me.lblTotalAppointments.TabIndex = 528
        Me.lblTotalAppointments.Text = "-"
        '
        'Panel10
        '
        Me.Panel10.Location = New System.Drawing.Point(12, 66)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(10, 1907)
        Me.Panel10.TabIndex = 646
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel8.Controls.Add(Me.Label5)
        Me.Panel8.Controls.Add(Me.Label9)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1349, 60)
        Me.Panel8.TabIndex = 645
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(12, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(191, 19)
        Me.Label5.TabIndex = 520
        Me.Label5.Text = "Manage Account and System"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(12, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(118, 21)
        Me.Label9.TabIndex = 519
        Me.Label9.Text = "Admisnistrator"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.lblCancelledWalkIn)
        Me.Panel2.Location = New System.Drawing.Point(807, 534)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(170, 86)
        Me.Panel2.TabIndex = 641
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(15, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(121, 17)
        Me.Label6.TabIndex = 164
        Me.Label6.Text = "Cancelled (Walk in)"
        '
        'lblCancelledWalkIn
        '
        Me.lblCancelledWalkIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCancelledWalkIn.AutoSize = True
        Me.lblCancelledWalkIn.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCancelledWalkIn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblCancelledWalkIn.Location = New System.Drawing.Point(13, 10)
        Me.lblCancelledWalkIn.Name = "lblCancelledWalkIn"
        Me.lblCancelledWalkIn.Size = New System.Drawing.Size(20, 25)
        Me.lblCancelledWalkIn.TabIndex = 528
        Me.lblCancelledWalkIn.Text = "-"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.lblOnline)
        Me.Panel3.Location = New System.Drawing.Point(64, 534)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(170, 86)
        Me.Panel3.TabIndex = 639
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(19, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 17)
        Me.Label2.TabIndex = 164
        Me.Label2.Text = "Online"
        '
        'lblOnline
        '
        Me.lblOnline.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOnline.AutoSize = True
        Me.lblOnline.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOnline.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblOnline.Location = New System.Drawing.Point(17, 10)
        Me.lblOnline.Name = "lblOnline"
        Me.lblOnline.Size = New System.Drawing.Size(20, 25)
        Me.lblOnline.TabIndex = 526
        Me.lblOnline.Text = "-"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.lblCancelledOnline)
        Me.Panel4.Location = New System.Drawing.Point(621, 534)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(170, 86)
        Me.Panel4.TabIndex = 640
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(15, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 17)
        Me.Label4.TabIndex = 164
        Me.Label4.Text = "Cancelled (Online)"
        '
        'lblCancelledOnline
        '
        Me.lblCancelledOnline.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCancelledOnline.AutoSize = True
        Me.lblCancelledOnline.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCancelledOnline.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblCancelledOnline.Location = New System.Drawing.Point(13, 10)
        Me.lblCancelledOnline.Name = "lblCancelledOnline"
        Me.lblCancelledOnline.Size = New System.Drawing.Size(20, 25)
        Me.lblCancelledOnline.TabIndex = 528
        Me.lblCancelledOnline.Text = "-"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Label10)
        Me.Panel7.Controls.Add(Me.lblWalkIn)
        Me.Panel7.Location = New System.Drawing.Point(249, 534)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(170, 86)
        Me.Panel7.TabIndex = 638
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(19, 51)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 17)
        Me.Label10.TabIndex = 164
        Me.Label10.Text = "Walk in"
        '
        'lblWalkIn
        '
        Me.lblWalkIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblWalkIn.AutoSize = True
        Me.lblWalkIn.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWalkIn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblWalkIn.Location = New System.Drawing.Point(17, 10)
        Me.lblWalkIn.Name = "lblWalkIn"
        Me.lblWalkIn.Size = New System.Drawing.Size(20, 25)
        Me.lblWalkIn.TabIndex = 526
        Me.lblWalkIn.Text = "-"
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Controls.Add(Me.Label23)
        Me.Panel12.Controls.Add(Me.Label13)
        Me.Panel12.Controls.Add(Me.lblActive)
        Me.Panel12.Location = New System.Drawing.Point(434, 321)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(170, 86)
        Me.Panel12.TabIndex = 636
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(732, 10)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(77, 21)
        Me.Label23.TabIndex = 164
        Me.Label23.Text = "Cancelled"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(18, 51)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 17)
        Me.Label13.TabIndex = 164
        Me.Label13.Text = "Active"
        '
        'lblActive
        '
        Me.lblActive.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblActive.AutoSize = True
        Me.lblActive.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblActive.Location = New System.Drawing.Point(16, 10)
        Me.lblActive.Name = "lblActive"
        Me.lblActive.Size = New System.Drawing.Size(20, 25)
        Me.lblActive.TabIndex = 527
        Me.lblActive.Text = "-"
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel13.Controls.Add(Me.lblLocked)
        Me.Panel13.Controls.Add(Me.Label15)
        Me.Panel13.Location = New System.Drawing.Point(619, 321)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(170, 86)
        Me.Panel13.TabIndex = 637
        '
        'lblLocked
        '
        Me.lblLocked.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLocked.AutoSize = True
        Me.lblLocked.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocked.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblLocked.Location = New System.Drawing.Point(15, 10)
        Me.lblLocked.Name = "lblLocked"
        Me.lblLocked.Size = New System.Drawing.Size(20, 25)
        Me.lblLocked.TabIndex = 526
        Me.lblLocked.Text = "-"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(16, 51)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(50, 17)
        Me.Label15.TabIndex = 164
        Me.Label15.Text = "Locked"
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel14.Controls.Add(Me.Label16)
        Me.Panel14.Controls.Add(Me.lblStaff)
        Me.Panel14.Location = New System.Drawing.Point(249, 321)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(170, 86)
        Me.Panel14.TabIndex = 635
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(12, 51)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(35, 17)
        Me.Label16.TabIndex = 164
        Me.Label16.Text = "Staff"
        '
        'lblStaff
        '
        Me.lblStaff.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStaff.AutoSize = True
        Me.lblStaff.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStaff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblStaff.Location = New System.Drawing.Point(10, 10)
        Me.lblStaff.Name = "lblStaff"
        Me.lblStaff.Size = New System.Drawing.Size(20, 25)
        Me.lblStaff.TabIndex = 524
        Me.lblStaff.Text = "-"
        '
        'object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66
        '
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.AllowUserToAddRows = False
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.AllowUserToDeleteRows = False
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.Location = New System.Drawing.Point(3, 100)
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.Name = "object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66"
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.ReadOnly = True
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.Size = New System.Drawing.Size(928, 383)
        Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66.TabIndex = 508
        '
        'frmAdmin_Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblDateTime)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAdmin_Dashboard"
        Me.Text = "frmAdmin_Dashboard"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvTickets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvActivityLogs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        CType(Me.object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblDateTime As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents lblCancelledWalkIn As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents lblOnline As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents lblCancelledOnline As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents lblWalkIn As Label
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Label23 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents lblActive As Label
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents lblnewResident As Label
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents lblStaff As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents object_229da1f4_9cd3_4f72_b5f4_825c60d6ad66 As DataGridView
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents lblTotalAppointments As Label
    Friend WithEvents lblFullname As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents lblUsers As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents lblLocked As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents lblTime As Label
    Friend WithEvents dgvUsers As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents lblAccountRecovery As Label
    Friend WithEvents dgvActivityLogs As DataGridView
    Friend WithEvents Label24 As Label
    Friend WithEvents dgvTickets As DataGridView
    Friend WithEvents Label25 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Panel15 As Panel
End Class
