<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAppointmentDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAppointmentDetails))
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.picProfile = New System.Windows.Forms.PictureBox()
        Me.lblControlNo = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblRequestFor = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblServiceType = New System.Windows.Forms.Label()
        Me.lblExpectedPickup = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnViewRepDetails = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblDateSubmitted = New System.Windows.Forms.Label()
        Me.RtbAddress = New System.Windows.Forms.RichTextBox()
        CType(Me.picProfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(252, 206)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(17, 21)
        Me.lblName.TabIndex = 592
        Me.lblName.Text = "-"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(466, 125)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(17, 21)
        Me.lblStatus.TabIndex = 596
        Me.lblStatus.Text = "-"
        '
        'picProfile
        '
        Me.picProfile.Location = New System.Drawing.Point(12, 192)
        Me.picProfile.Name = "picProfile"
        Me.picProfile.Size = New System.Drawing.Size(113, 106)
        Me.picProfile.TabIndex = 600
        Me.picProfile.TabStop = False
        '
        'lblControlNo
        '
        Me.lblControlNo.AutoSize = True
        Me.lblControlNo.Font = New System.Drawing.Font("Microsoft YaHei UI", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblControlNo.ForeColor = System.Drawing.Color.DarkRed
        Me.lblControlNo.Location = New System.Drawing.Point(12, 66)
        Me.lblControlNo.Name = "lblControlNo"
        Me.lblControlNo.Size = New System.Drawing.Size(27, 36)
        Me.lblControlNo.TabIndex = 602
        Me.lblControlNo.Text = "-"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(394, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 22)
        Me.Label5.TabIndex = 617
        Me.Label5.Text = "Status:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(135, 279)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 22)
        Me.Label6.TabIndex = 616
        Me.Label6.Text = "Address"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(135, 205)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 22)
        Me.Label8.TabIndex = 614
        Me.Label8.Text = "Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(135, 243)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 22)
        Me.Label2.TabIndex = 618
        Me.Label2.Text = "Service Type"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.Location = New System.Drawing.Point(12, 402)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(642, 37)
        Me.btnClose.TabIndex = 619
        Me.btnClose.Text = "Close"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblRequestFor
        '
        Me.lblRequestFor.AutoSize = True
        Me.lblRequestFor.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequestFor.Location = New System.Drawing.Point(269, 11)
        Me.lblRequestFor.Name = "lblRequestFor"
        Me.lblRequestFor.Size = New System.Drawing.Size(17, 22)
        Me.lblRequestFor.TabIndex = 632
        Me.lblRequestFor.Text = "-"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.lblRequestFor)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(666, 60)
        Me.Panel4.TabIndex = 635
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(67, 33)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(195, 19)
        Me.Label15.TabIndex = 520
        Me.Label15.Text = "Manage Appointments Details"
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
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(66, 11)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(197, 22)
        Me.Label16.TabIndex = 519
        Me.Label16.Text = "Appointment Details - "
        '
        'lblServiceType
        '
        Me.lblServiceType.AutoSize = True
        Me.lblServiceType.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceType.Location = New System.Drawing.Point(252, 244)
        Me.lblServiceType.Name = "lblServiceType"
        Me.lblServiceType.Size = New System.Drawing.Size(17, 21)
        Me.lblServiceType.TabIndex = 636
        Me.lblServiceType.Text = "-"
        '
        'lblExpectedPickup
        '
        Me.lblExpectedPickup.AutoSize = True
        Me.lblExpectedPickup.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpectedPickup.Location = New System.Drawing.Point(162, 156)
        Me.lblExpectedPickup.Name = "lblExpectedPickup"
        Me.lblExpectedPickup.Size = New System.Drawing.Size(17, 21)
        Me.lblExpectedPickup.TabIndex = 597
        Me.lblExpectedPickup.Text = "-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 124)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 22)
        Me.Label1.TabIndex = 612
        Me.Label1.Text = "Date Submitted:"
        '
        'btnViewRepDetails
        '
        Me.btnViewRepDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnViewRepDetails.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnViewRepDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnViewRepDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewRepDetails.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewRepDetails.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnViewRepDetails.Image = CType(resources.GetObject("btnViewRepDetails.Image"), System.Drawing.Image)
        Me.btnViewRepDetails.Location = New System.Drawing.Point(618, 66)
        Me.btnViewRepDetails.Name = "btnViewRepDetails"
        Me.btnViewRepDetails.Size = New System.Drawing.Size(36, 36)
        Me.btnViewRepDetails.TabIndex = 640
        Me.btnViewRepDetails.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 158)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 22)
        Me.Label3.TabIndex = 642
        Me.Label3.Text = "Pickup"
        '
        'lblDateSubmitted
        '
        Me.lblDateSubmitted.AutoSize = True
        Me.lblDateSubmitted.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateSubmitted.Location = New System.Drawing.Point(162, 125)
        Me.lblDateSubmitted.Name = "lblDateSubmitted"
        Me.lblDateSubmitted.Size = New System.Drawing.Size(17, 21)
        Me.lblDateSubmitted.TabIndex = 641
        Me.lblDateSubmitted.Text = "-"
        '
        'RtbAddress
        '
        Me.RtbAddress.BackColor = System.Drawing.SystemColors.Control
        Me.RtbAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RtbAddress.Enabled = False
        Me.RtbAddress.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RtbAddress.Location = New System.Drawing.Point(256, 282)
        Me.RtbAddress.Name = "RtbAddress"
        Me.RtbAddress.Size = New System.Drawing.Size(398, 76)
        Me.RtbAddress.TabIndex = 643
        Me.RtbAddress.Text = ""
        '
        'frmAppointmentDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 451)
        Me.Controls.Add(Me.RtbAddress)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblDateSubmitted)
        Me.Controls.Add(Me.btnViewRepDetails)
        Me.Controls.Add(Me.lblControlNo)
        Me.Controls.Add(Me.lblServiceType)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.picProfile)
        Me.Controls.Add(Me.lblExpectedPickup)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblName)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAppointmentDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmAppointmentDetails"
        CType(Me.picProfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblName As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents picProfile As PictureBox
    Friend WithEvents lblControlNo As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents lblRequestFor As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label16 As Label
    Friend WithEvents lblServiceType As Label
    Friend WithEvents lblExpectedPickup As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnViewRepDetails As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents lblDateSubmitted As Label
    Friend WithEvents RtbAddress As RichTextBox
End Class
