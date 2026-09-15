<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmUser_Dashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUser_Dashboard))
        Me.lblCancelled = New System.Windows.Forms.Label()
        Me.lblApproved = New System.Windows.Forms.Label()
        Me.lblCompleted = New System.Windows.Forms.Label()
        Me.lblRejected = New System.Windows.Forms.Label()
        Me.lblPending = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.btnRef = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgvRequests = New System.Windows.Forms.DataGridView()
        Me.btnCreateRequest = New System.Windows.Forms.Button()
        Me.picUserProfile = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblUncompletedTasks = New System.Windows.Forms.Label()
        Me.lblDailyTasks = New System.Windows.Forms.Label()
        Me.lblTotalTasks = New System.Windows.Forms.Label()
        Me.lblDateTime = New System.Windows.Forms.Label()
        Me.lblWelcomeUser = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel7.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUserProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCancelled
        '
        Me.lblCancelled.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCancelled.AutoSize = True
        Me.lblCancelled.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCancelled.Location = New System.Drawing.Point(1101, 740)
        Me.lblCancelled.Name = "lblCancelled"
        Me.lblCancelled.Size = New System.Drawing.Size(68, 19)
        Me.lblCancelled.TabIndex = 528
        Me.lblCancelled.Text = "Cancelled"
        '
        'lblApproved
        '
        Me.lblApproved.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblApproved.AutoSize = True
        Me.lblApproved.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApproved.Location = New System.Drawing.Point(530, 740)
        Me.lblApproved.Name = "lblApproved"
        Me.lblApproved.Size = New System.Drawing.Size(69, 19)
        Me.lblApproved.TabIndex = 527
        Me.lblApproved.Text = "Approved"
        '
        'lblCompleted
        '
        Me.lblCompleted.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCompleted.AutoSize = True
        Me.lblCompleted.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompleted.Location = New System.Drawing.Point(915, 740)
        Me.lblCompleted.Name = "lblCompleted"
        Me.lblCompleted.Size = New System.Drawing.Size(76, 19)
        Me.lblCompleted.TabIndex = 526
        Me.lblCompleted.Text = "Completed"
        '
        'lblRejected
        '
        Me.lblRejected.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRejected.AutoSize = True
        Me.lblRejected.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRejected.Location = New System.Drawing.Point(736, 740)
        Me.lblRejected.Name = "lblRejected"
        Me.lblRejected.Size = New System.Drawing.Size(61, 19)
        Me.lblRejected.TabIndex = 525
        Me.lblRejected.Text = "Rejected"
        '
        'lblPending
        '
        Me.lblPending.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPending.AutoSize = True
        Me.lblPending.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Location = New System.Drawing.Point(362, 740)
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(59, 19)
        Me.lblPending.TabIndex = 524
        Me.lblPending.Text = "Pending"
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel7.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.btnRef)
        Me.Panel7.Controls.Add(Me.Panel2)
        Me.Panel7.Controls.Add(Me.Label5)
        Me.Panel7.Controls.Add(Me.dgvRequests)
        Me.Panel7.Controls.Add(Me.btnCreateRequest)
        Me.Panel7.Location = New System.Drawing.Point(365, 251)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(956, 484)
        Me.Panel7.TabIndex = 522
        '
        'btnRef
        '
        Me.btnRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRef.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnRef.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnRef.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRef.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRef.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnRef.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRef.Location = New System.Drawing.Point(809, 12)
        Me.btnRef.Name = "btnRef"
        Me.btnRef.Size = New System.Drawing.Size(131, 37)
        Me.btnRef.TabIndex = 552
        Me.btnRef.Text = "refresh"
        Me.btnRef.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnRef.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Location = New System.Drawing.Point(0, 61)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(954, 38)
        Me.Panel2.TabIndex = 509
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(15, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtSearch.Size = New System.Drawing.Size(254, 28)
        Me.txtSearch.TabIndex = 223
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft YaHei UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label5.Location = New System.Drawing.Point(9, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 31)
        Me.Label5.TabIndex = 188
        Me.Label5.Text = "Request"
        '
        'dgvRequests
        '
        Me.dgvRequests.AllowUserToAddRows = False
        Me.dgvRequests.AllowUserToDeleteRows = False
        Me.dgvRequests.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRequests.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvRequests.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRequests.Location = New System.Drawing.Point(0, 99)
        Me.dgvRequests.Name = "dgvRequests"
        Me.dgvRequests.ReadOnly = True
        Me.dgvRequests.Size = New System.Drawing.Size(954, 383)
        Me.dgvRequests.TabIndex = 508
        '
        'btnCreateRequest
        '
        Me.btnCreateRequest.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateRequest.BackColor = System.Drawing.Color.Navy
        Me.btnCreateRequest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCreateRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateRequest.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateRequest.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCreateRequest.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCreateRequest.Location = New System.Drawing.Point(672, 12)
        Me.btnCreateRequest.Name = "btnCreateRequest"
        Me.btnCreateRequest.Size = New System.Drawing.Size(131, 37)
        Me.btnCreateRequest.TabIndex = 506
        Me.btnCreateRequest.Text = "Create Request"
        Me.btnCreateRequest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCreateRequest.UseVisualStyleBackColor = False
        '
        'picUserProfile
        '
        Me.picUserProfile.Image = CType(resources.GetObject("picUserProfile.Image"), System.Drawing.Image)
        Me.picUserProfile.Location = New System.Drawing.Point(137, 167)
        Me.picUserProfile.Name = "picUserProfile"
        Me.picUserProfile.Size = New System.Drawing.Size(151, 150)
        Me.picUserProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picUserProfile.TabIndex = 521
        Me.picUserProfile.TabStop = False
        '
        'Timer1
        '
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Location = New System.Drawing.Point(44, 251)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(315, 484)
        Me.Panel5.TabIndex = 519
        '
        'lblUncompletedTasks
        '
        Me.lblUncompletedTasks.AutoSize = True
        Me.lblUncompletedTasks.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUncompletedTasks.ForeColor = System.Drawing.Color.Navy
        Me.lblUncompletedTasks.Location = New System.Drawing.Point(865, 226)
        Me.lblUncompletedTasks.Name = "lblUncompletedTasks"
        Me.lblUncompletedTasks.Size = New System.Drawing.Size(20, 22)
        Me.lblUncompletedTasks.TabIndex = 537
        Me.lblUncompletedTasks.Text = "0"
        '
        'lblDailyTasks
        '
        Me.lblDailyTasks.AutoSize = True
        Me.lblDailyTasks.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyTasks.ForeColor = System.Drawing.Color.Navy
        Me.lblDailyTasks.Location = New System.Drawing.Point(367, 226)
        Me.lblDailyTasks.Name = "lblDailyTasks"
        Me.lblDailyTasks.Size = New System.Drawing.Size(20, 22)
        Me.lblDailyTasks.TabIndex = 534
        Me.lblDailyTasks.Text = "0"
        '
        'lblTotalTasks
        '
        Me.lblTotalTasks.AutoSize = True
        Me.lblTotalTasks.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTasks.ForeColor = System.Drawing.Color.Navy
        Me.lblTotalTasks.Location = New System.Drawing.Point(615, 226)
        Me.lblTotalTasks.Name = "lblTotalTasks"
        Me.lblTotalTasks.Size = New System.Drawing.Size(20, 22)
        Me.lblTotalTasks.TabIndex = 535
        Me.lblTotalTasks.Text = "0"
        '
        'lblDateTime
        '
        Me.lblDateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDateTime.AutoSize = True
        Me.lblDateTime.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateTime.ForeColor = System.Drawing.Color.Navy
        Me.lblDateTime.Location = New System.Drawing.Point(987, 120)
        Me.lblDateTime.Name = "lblDateTime"
        Me.lblDateTime.Size = New System.Drawing.Size(128, 22)
        Me.lblDateTime.TabIndex = 520
        Me.lblDateTime.Text = "Date and Time"
        '
        'lblWelcomeUser
        '
        Me.lblWelcomeUser.AutoSize = True
        Me.lblWelcomeUser.Font = New System.Drawing.Font("Microsoft YaHei UI", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWelcomeUser.ForeColor = System.Drawing.Color.DarkRed
        Me.lblWelcomeUser.Location = New System.Drawing.Point(364, 103)
        Me.lblWelcomeUser.Name = "lblWelcomeUser"
        Me.lblWelcomeUser.Size = New System.Drawing.Size(103, 42)
        Me.lblWelcomeUser.TabIndex = 530
        Me.lblWelcomeUser.Text = "User!"
        Me.lblWelcomeUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft YaHei UI", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(181, 103)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(177, 42)
        Me.Label1.TabIndex = 531
        Me.Label1.Text = "Welcome,"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(66, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(153, 22)
        Me.Label9.TabIndex = 519
        Me.Label9.Text = "Barangay Putatan"
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(67, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 19)
        Me.Label2.TabIndex = 520
        Me.Label2.Text = "City of Muntinlupa"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1366, 60)
        Me.Panel4.TabIndex = 518
        '
        'frmUser_Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.lblUncompletedTasks)
        Me.Controls.Add(Me.picUserProfile)
        Me.Controls.Add(Me.lblTotalTasks)
        Me.Controls.Add(Me.lblDailyTasks)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblWelcomeUser)
        Me.Controls.Add(Me.lblCancelled)
        Me.Controls.Add(Me.lblApproved)
        Me.Controls.Add(Me.lblCompleted)
        Me.Controls.Add(Me.lblRejected)
        Me.Controls.Add(Me.lblPending)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.lblDateTime)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUser_Dashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmHome"
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgvRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUserProfile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblCancelled As Label
    Friend WithEvents lblApproved As Label
    Friend WithEvents lblCompleted As Label
    Friend WithEvents lblRejected As Label
    Friend WithEvents lblPending As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dgvRequests As DataGridView
    Friend WithEvents btnCreateRequest As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents picUserProfile As PictureBox
    Friend WithEvents btnRef As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents lblTotalTasks As Label
    Friend WithEvents lblDailyTasks As Label
    Friend WithEvents lblUncompletedTasks As Label
    Friend WithEvents lblDateTime As Label
    Friend WithEvents lblWelcomeUser As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel4 As Panel
End Class
