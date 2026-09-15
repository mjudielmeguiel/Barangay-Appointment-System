<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPayments
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayments))
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblControlNo = New System.Windows.Forms.Label()
        Me.lblFullName = New System.Windows.Forms.Label()
        Me.lblDocument = New System.Windows.Forms.Label()
        Me.lblPaymentStatus = New System.Windows.Forms.Label()
        Me.txtORNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnGcash = New System.Windows.Forms.Button()
        Me.btnMarkPaid = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAmountPaid = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblError = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnCancelAppointment = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dgvPayments = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgvPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.Panel4.TabIndex = 519
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(67, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(171, 19)
        Me.Label2.TabIndex = 520
        Me.Label2.Text = "Pay/Waive for Documents"
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
        Me.Label9.Size = New System.Drawing.Size(120, 22)
        Me.Label9.TabIndex = 519
        Me.Label9.Text = "Add Payment"
        '
        'lblControlNo
        '
        Me.lblControlNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblControlNo.AutoSize = True
        Me.lblControlNo.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblControlNo.ForeColor = System.Drawing.Color.Navy
        Me.lblControlNo.Location = New System.Drawing.Point(159, 30)
        Me.lblControlNo.Name = "lblControlNo"
        Me.lblControlNo.Size = New System.Drawing.Size(15, 19)
        Me.lblControlNo.TabIndex = 521
        Me.lblControlNo.Text = "-"
        '
        'lblFullName
        '
        Me.lblFullName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFullName.AutoSize = True
        Me.lblFullName.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFullName.ForeColor = System.Drawing.Color.Navy
        Me.lblFullName.Location = New System.Drawing.Point(159, 138)
        Me.lblFullName.Name = "lblFullName"
        Me.lblFullName.Size = New System.Drawing.Size(15, 19)
        Me.lblFullName.TabIndex = 522
        Me.lblFullName.Text = "-"
        '
        'lblDocument
        '
        Me.lblDocument.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDocument.AutoSize = True
        Me.lblDocument.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocument.ForeColor = System.Drawing.Color.Navy
        Me.lblDocument.Location = New System.Drawing.Point(159, 185)
        Me.lblDocument.Name = "lblDocument"
        Me.lblDocument.Size = New System.Drawing.Size(15, 19)
        Me.lblDocument.TabIndex = 523
        Me.lblDocument.Text = "-"
        '
        'lblPaymentStatus
        '
        Me.lblPaymentStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPaymentStatus.AutoSize = True
        Me.lblPaymentStatus.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentStatus.ForeColor = System.Drawing.Color.Navy
        Me.lblPaymentStatus.Location = New System.Drawing.Point(159, 83)
        Me.lblPaymentStatus.Name = "lblPaymentStatus"
        Me.lblPaymentStatus.Size = New System.Drawing.Size(15, 19)
        Me.lblPaymentStatus.TabIndex = 525
        Me.lblPaymentStatus.Text = "-"
        '
        'txtORNo
        '
        Me.txtORNo.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtORNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtORNo.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtORNo.Location = New System.Drawing.Point(178, 236)
        Me.txtORNo.Name = "txtORNo"
        Me.txtORNo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtORNo.Size = New System.Drawing.Size(268, 28)
        Me.txtORNo.TabIndex = 526
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(8, 240)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 19)
        Me.Label1.TabIndex = 527
        Me.Label1.Text = "OR Number:"
        '
        'btnGcash
        '
        Me.btnGcash.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGcash.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnGcash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnGcash.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGcash.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGcash.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnGcash.Image = CType(resources.GetObject("btnGcash.Image"), System.Drawing.Image)
        Me.btnGcash.Location = New System.Drawing.Point(178, 356)
        Me.btnGcash.Name = "btnGcash"
        Me.btnGcash.Size = New System.Drawing.Size(131, 37)
        Me.btnGcash.TabIndex = 554
        Me.btnGcash.Text = " Gcash"
        Me.btnGcash.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGcash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnGcash.UseVisualStyleBackColor = False
        '
        'btnMarkPaid
        '
        Me.btnMarkPaid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMarkPaid.BackColor = System.Drawing.Color.Navy
        Me.btnMarkPaid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnMarkPaid.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMarkPaid.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMarkPaid.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnMarkPaid.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMarkPaid.Location = New System.Drawing.Point(12, 544)
        Me.btnMarkPaid.Name = "btnMarkPaid"
        Me.btnMarkPaid.Size = New System.Drawing.Size(434, 37)
        Me.btnMarkPaid.TabIndex = 553
        Me.btnMarkPaid.Text = "Mark Paid"
        Me.btnMarkPaid.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnMarkPaid.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(7, 301)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 19)
        Me.Label3.TabIndex = 557
        Me.Label3.Text = "Amount Paid:"
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountPaid.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmountPaid.Location = New System.Drawing.Point(178, 297)
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtAmountPaid.Size = New System.Drawing.Size(268, 28)
        Me.txtAmountPaid.TabIndex = 556
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(9, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 19)
        Me.Label4.TabIndex = 563
        Me.Label4.Text = "Status:"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(8, 185)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(117, 19)
        Me.Label6.TabIndex = 561
        Me.Label6.Text = "Document Type:"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(9, 138)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 19)
        Me.Label7.TabIndex = 560
        Me.Label7.Text = "Fullname:"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Navy
        Me.Label8.Location = New System.Drawing.Point(8, 30)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(122, 19)
        Me.Label8.TabIndex = 559
        Me.Label8.Text = "Control Number:"
        '
        'lblError
        '
        Me.lblError.AutoSize = True
        Me.lblError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblError.Location = New System.Drawing.Point(12, 571)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(15, 19)
        Me.lblError.TabIndex = 565
        Me.lblError.Text = "-"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.btnCancelAppointment)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.btnGcash)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.btnMarkPaid)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.lblControlNo)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblPaymentStatus)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtORNo)
        Me.Panel1.Controls.Add(Me.lblDocument)
        Me.Panel1.Controls.Add(Me.txtAmountPaid)
        Me.Panel1.Controls.Add(Me.lblFullName)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(12, 95)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(464, 646)
        Me.Panel1.TabIndex = 566
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Navy
        Me.Label5.Location = New System.Drawing.Point(174, 396)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(195, 19)
        Me.Label5.TabIndex = 575
        Me.Label5.Text = "ADD 10 Pesos for ADMIN Fee"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(315, 356)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(131, 37)
        Me.Button1.TabIndex = 574
        Me.Button1.Text = " Maya"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Navy
        Me.Label14.Location = New System.Drawing.Point(8, 467)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(149, 19)
        Me.Label14.TabIndex = 573
        Me.Label14.Text = "Transaction Number:"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(178, 463)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.TextBox2.Size = New System.Drawing.Size(268, 28)
        Me.TextBox2.TabIndex = 572
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Navy
        Me.Label13.Location = New System.Drawing.Point(8, 433)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(101, 19)
        Me.Label13.TabIndex = 571
        Me.Label13.Text = "Sender name:"
        '
        'btnCancelAppointment
        '
        Me.btnCancelAppointment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelAppointment.BackColor = System.Drawing.Color.DarkRed
        Me.btnCancelAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCancelAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelAppointment.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelAppointment.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCancelAppointment.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelAppointment.Location = New System.Drawing.Point(12, 587)
        Me.btnCancelAppointment.Name = "btnCancelAppointment"
        Me.btnCancelAppointment.Size = New System.Drawing.Size(434, 37)
        Me.btnCancelAppointment.TabIndex = 568
        Me.btnCancelAppointment.Text = "CANCEL"
        Me.btnCancelAppointment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelAppointment.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(178, 429)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.TextBox1.Size = New System.Drawing.Size(268, 28)
        Me.TextBox1.TabIndex = 570
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Navy
        Me.Label12.Location = New System.Drawing.Point(22, 363)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(118, 19)
        Me.Label12.TabIndex = 569
        Me.Label12.Text = "Online Payment:"
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel7.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Panel3)
        Me.Panel7.Controls.Add(Me.btnCancel)
        Me.Panel7.Controls.Add(Me.Label11)
        Me.Panel7.Controls.Add(Me.dgvPayments)
        Me.Panel7.Location = New System.Drawing.Point(482, 94)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(872, 647)
        Me.Panel7.TabIndex = 560
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel3.Controls.Add(Me.txtSearch)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 62)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(870, 38)
        Me.Panel3.TabIndex = 509
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
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.Location = New System.Drawing.Point(725, 12)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(131, 37)
        Me.btnCancel.TabIndex = 552
        Me.btnCancel.Text = "Close"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft YaHei UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label11.Location = New System.Drawing.Point(9, 12)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(242, 31)
        Me.Label11.TabIndex = 188
        Me.Label11.Text = "Pending Payments"
        '
        'dgvPayments
        '
        Me.dgvPayments.AllowUserToAddRows = False
        Me.dgvPayments.AllowUserToDeleteRows = False
        Me.dgvPayments.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvPayments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPayments.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvPayments.Location = New System.Drawing.Point(0, 100)
        Me.dgvPayments.Name = "dgvPayments"
        Me.dgvPayments.ReadOnly = True
        Me.dgvPayments.Size = New System.Drawing.Size(870, 545)
        Me.dgvPayments.TabIndex = 508
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.Navy
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.Location = New System.Drawing.Point(14, 304)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(434, 37)
        Me.Button2.TabIndex = 576
        Me.Button2.Text = "Mark Paid"
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button2.UseVisualStyleBackColor = False
        '
        'frmPayments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.Panel4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPayments"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPayments"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.dgvPayments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label9 As Label
    Friend WithEvents lblControlNo As Label
    Friend WithEvents lblFullName As Label
    Friend WithEvents lblDocument As Label
    Friend WithEvents lblPaymentStatus As Label
    Friend WithEvents txtORNo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnGcash As Button
    Friend WithEvents btnMarkPaid As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents txtAmountPaid As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lblError As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnCancelAppointment As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents btnCancel As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents dgvPayments As DataGridView
    Friend WithEvents Label12 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Button2 As Button
End Class
