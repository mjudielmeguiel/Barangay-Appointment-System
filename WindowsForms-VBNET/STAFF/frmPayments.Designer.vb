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
        Me.lblControlNo = New System.Windows.Forms.Label()
        Me.lblFullName = New System.Windows.Forms.Label()
        Me.lblDocumentType = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtORNo = New System.Windows.Forms.TextBox()
        Me.lblORNumber = New System.Windows.Forms.Label()
        Me.btnGcash = New System.Windows.Forms.Button()
        Me.btnMarkPaid = New System.Windows.Forms.Button()
        Me.lblAmountPaid = New System.Windows.Forms.Label()
        Me.txtAmountPaid = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblError = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblWalletUsed = New System.Windows.Forms.Label()
        Me.txtWalletUsed = New System.Windows.Forms.TextBox()
        Me.chkOnlinePayment = New System.Windows.Forms.CheckBox()
        Me.btnMaya = New System.Windows.Forms.Button()
        Me.lblTransactionNo = New System.Windows.Forms.Label()
        Me.txtTransactionNo = New System.Windows.Forms.TextBox()
        Me.lblSenderName = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtSenderName = New System.Windows.Forms.TextBox()
        Me.lblOnlinePayment = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.dgvPayments = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgvPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblControlNo
        '
        Me.lblControlNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblControlNo.AutoSize = True
        Me.lblControlNo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblControlNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblControlNo.Location = New System.Drawing.Point(175, 32)
        Me.lblControlNo.Name = "lblControlNo"
        Me.lblControlNo.Size = New System.Drawing.Size(13, 17)
        Me.lblControlNo.TabIndex = 521
        Me.lblControlNo.Text = "-"
        '
        'lblFullName
        '
        Me.lblFullName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFullName.AutoSize = True
        Me.lblFullName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFullName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblFullName.Location = New System.Drawing.Point(175, 140)
        Me.lblFullName.Name = "lblFullName"
        Me.lblFullName.Size = New System.Drawing.Size(13, 17)
        Me.lblFullName.TabIndex = 522
        Me.lblFullName.Text = "-"
        '
        'lblDocumentType
        '
        Me.lblDocumentType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDocumentType.AutoSize = True
        Me.lblDocumentType.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblDocumentType.Location = New System.Drawing.Point(175, 187)
        Me.lblDocumentType.Name = "lblDocumentType"
        Me.lblDocumentType.Size = New System.Drawing.Size(13, 17)
        Me.lblDocumentType.TabIndex = 523
        Me.lblDocumentType.Text = "-"
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblStatus.Location = New System.Drawing.Point(175, 85)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(13, 17)
        Me.lblStatus.TabIndex = 525
        Me.lblStatus.Text = "-"
        '
        'txtORNo
        '
        Me.txtORNo.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtORNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtORNo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtORNo.Location = New System.Drawing.Point(178, 236)
        Me.txtORNo.Name = "txtORNo"
        Me.txtORNo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtORNo.Size = New System.Drawing.Size(268, 29)
        Me.txtORNo.TabIndex = 526
        '
        'lblORNumber
        '
        Me.lblORNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblORNumber.AutoSize = True
        Me.lblORNumber.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblORNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblORNumber.Location = New System.Drawing.Point(19, 242)
        Me.lblORNumber.Name = "lblORNumber"
        Me.lblORNumber.Size = New System.Drawing.Size(84, 17)
        Me.lblORNumber.TabIndex = 527
        Me.lblORNumber.Text = "OR Number:"
        '
        'btnGcash
        '
        Me.btnGcash.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGcash.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnGcash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnGcash.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGcash.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGcash.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnGcash.Image = CType(resources.GetObject("btnGcash.Image"), System.Drawing.Image)
        Me.btnGcash.Location = New System.Drawing.Point(178, 343)
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
        Me.btnMarkPaid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
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
        'lblAmountPaid
        '
        Me.lblAmountPaid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAmountPaid.AutoSize = True
        Me.lblAmountPaid.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountPaid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblAmountPaid.Location = New System.Drawing.Point(20, 288)
        Me.lblAmountPaid.Name = "lblAmountPaid"
        Me.lblAmountPaid.Size = New System.Drawing.Size(91, 17)
        Me.lblAmountPaid.TabIndex = 557
        Me.lblAmountPaid.Text = "Amount Paid:"
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountPaid.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmountPaid.Location = New System.Drawing.Point(178, 284)
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtAmountPaid.Size = New System.Drawing.Size(268, 29)
        Me.txtAmountPaid.TabIndex = 556
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(20, 83)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 17)
        Me.Label4.TabIndex = 563
        Me.Label4.Text = "Status:"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(19, 187)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 17)
        Me.Label6.TabIndex = 561
        Me.Label6.Text = "Document Type:"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(20, 140)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 17)
        Me.Label7.TabIndex = 560
        Me.Label7.Text = "Fullname:"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(19, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(111, 17)
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
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblWalletUsed)
        Me.Panel1.Controls.Add(Me.txtWalletUsed)
        Me.Panel1.Controls.Add(Me.chkOnlinePayment)
        Me.Panel1.Controls.Add(Me.btnMaya)
        Me.Panel1.Controls.Add(Me.lblTransactionNo)
        Me.Panel1.Controls.Add(Me.txtTransactionNo)
        Me.Panel1.Controls.Add(Me.lblSenderName)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.txtSenderName)
        Me.Panel1.Controls.Add(Me.btnGcash)
        Me.Panel1.Controls.Add(Me.lblOnlinePayment)
        Me.Panel1.Controls.Add(Me.btnMarkPaid)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.lblControlNo)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.lblAmountPaid)
        Me.Panel1.Controls.Add(Me.txtORNo)
        Me.Panel1.Controls.Add(Me.lblDocumentType)
        Me.Panel1.Controls.Add(Me.txtAmountPaid)
        Me.Panel1.Controls.Add(Me.lblFullName)
        Me.Panel1.Controls.Add(Me.lblORNumber)
        Me.Panel1.Location = New System.Drawing.Point(12, 95)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(464, 646)
        Me.Panel1.TabIndex = 566
        '
        'lblWalletUsed
        '
        Me.lblWalletUsed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblWalletUsed.AutoSize = True
        Me.lblWalletUsed.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWalletUsed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblWalletUsed.Location = New System.Drawing.Point(22, 390)
        Me.lblWalletUsed.Name = "lblWalletUsed"
        Me.lblWalletUsed.Size = New System.Drawing.Size(120, 17)
        Me.lblWalletUsed.TabIndex = 577
        Me.lblWalletUsed.Text = "Mode of Payment:"
        '
        'txtWalletUsed
        '
        Me.txtWalletUsed.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtWalletUsed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWalletUsed.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWalletUsed.Location = New System.Drawing.Point(178, 386)
        Me.txtWalletUsed.Name = "txtWalletUsed"
        Me.txtWalletUsed.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtWalletUsed.Size = New System.Drawing.Size(268, 29)
        Me.txtWalletUsed.TabIndex = 576
        '
        'chkOnlinePayment
        '
        Me.chkOnlinePayment.AutoSize = True
        Me.chkOnlinePayment.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnlinePayment.Location = New System.Drawing.Point(339, 319)
        Me.chkOnlinePayment.Name = "chkOnlinePayment"
        Me.chkOnlinePayment.Size = New System.Drawing.Size(107, 17)
        Me.chkOnlinePayment.TabIndex = 567
        Me.chkOnlinePayment.Text = "Online Payment"
        Me.chkOnlinePayment.UseVisualStyleBackColor = True
        '
        'btnMaya
        '
        Me.btnMaya.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMaya.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnMaya.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnMaya.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMaya.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMaya.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnMaya.Image = CType(resources.GetObject("btnMaya.Image"), System.Drawing.Image)
        Me.btnMaya.Location = New System.Drawing.Point(315, 343)
        Me.btnMaya.Name = "btnMaya"
        Me.btnMaya.Size = New System.Drawing.Size(131, 37)
        Me.btnMaya.TabIndex = 574
        Me.btnMaya.Text = " Maya"
        Me.btnMaya.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMaya.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnMaya.UseVisualStyleBackColor = False
        '
        'lblTransactionNo
        '
        Me.lblTransactionNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTransactionNo.AutoSize = True
        Me.lblTransactionNo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransactionNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblTransactionNo.Location = New System.Drawing.Point(20, 470)
        Me.lblTransactionNo.Name = "lblTransactionNo"
        Me.lblTransactionNo.Size = New System.Drawing.Size(135, 17)
        Me.lblTransactionNo.TabIndex = 573
        Me.lblTransactionNo.Text = "Transaction Number:"
        '
        'txtTransactionNo
        '
        Me.txtTransactionNo.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtTransactionNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransactionNo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransactionNo.Location = New System.Drawing.Point(178, 466)
        Me.txtTransactionNo.Name = "txtTransactionNo"
        Me.txtTransactionNo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtTransactionNo.Size = New System.Drawing.Size(268, 29)
        Me.txtTransactionNo.TabIndex = 572
        '
        'lblSenderName
        '
        Me.lblSenderName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSenderName.AutoSize = True
        Me.lblSenderName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSenderName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblSenderName.Location = New System.Drawing.Point(22, 424)
        Me.lblSenderName.Name = "lblSenderName"
        Me.lblSenderName.Size = New System.Drawing.Size(91, 17)
        Me.lblSenderName.TabIndex = 571
        Me.lblSenderName.Text = "Sender name:"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.DarkRed
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.Location = New System.Drawing.Point(12, 587)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(434, 37)
        Me.btnClose.TabIndex = 568
        Me.btnClose.Text = "Close"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'txtSenderName
        '
        Me.txtSenderName.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtSenderName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSenderName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSenderName.Location = New System.Drawing.Point(178, 420)
        Me.txtSenderName.Name = "txtSenderName"
        Me.txtSenderName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtSenderName.Size = New System.Drawing.Size(268, 29)
        Me.txtSenderName.TabIndex = 570
        '
        'lblOnlinePayment
        '
        Me.lblOnlinePayment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOnlinePayment.AutoSize = True
        Me.lblOnlinePayment.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOnlinePayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblOnlinePayment.Location = New System.Drawing.Point(22, 350)
        Me.lblOnlinePayment.Name = "lblOnlinePayment"
        Me.lblOnlinePayment.Size = New System.Drawing.Size(108, 17)
        Me.lblOnlinePayment.TabIndex = 569
        Me.lblOnlinePayment.Text = "Online Payment:"
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel7.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Label2)
        Me.Panel7.Controls.Add(Me.Panel3)
        Me.Panel7.Controls.Add(Me.dgvPayments)
        Me.Panel7.Location = New System.Drawing.Point(482, 94)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(872, 647)
        Me.Panel7.TabIndex = 560
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel3.Controls.Add(Me.txtSearch)
        Me.Panel3.Location = New System.Drawing.Point(0, 62)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(870, 38)
        Me.Panel3.TabIndex = 509
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(15, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtSearch.Size = New System.Drawing.Size(254, 29)
        Me.txtSearch.TabIndex = 223
        '
        'dgvPayments
        '
        Me.dgvPayments.AllowUserToAddRows = False
        Me.dgvPayments.AllowUserToDeleteRows = False
        Me.dgvPayments.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPayments.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvPayments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPayments.Location = New System.Drawing.Point(-1, 101)
        Me.dgvPayments.Name = "dgvPayments"
        Me.dgvPayments.ReadOnly = True
        Me.dgvPayments.Size = New System.Drawing.Size(872, 545)
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
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1366, 60)
        Me.Panel5.TabIndex = 717
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(12, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(157, 17)
        Me.Label5.TabIndex = 520
        Me.Label5.Text = "Pay/Waive for Documents"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 21)
        Me.Label1.TabIndex = 519
        Me.Label1.Text = "Add Payment"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(11, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 21)
        Me.Label2.TabIndex = 521
        Me.Label2.Text = "Payment List"
        '
        'frmPayments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblError)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPayments"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPayments"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.dgvPayments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblControlNo As Label
    Friend WithEvents lblFullName As Label
    Friend WithEvents lblDocumentType As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents txtORNo As TextBox
    Friend WithEvents lblORNumber As Label
    Friend WithEvents btnGcash As Button
    Friend WithEvents btnMarkPaid As Button
    Friend WithEvents lblAmountPaid As Label
    Friend WithEvents txtAmountPaid As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lblError As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dgvPayments As DataGridView
    Friend WithEvents lblOnlinePayment As Label
    Friend WithEvents txtSenderName As TextBox
    Friend WithEvents lblSenderName As Label
    Friend WithEvents btnMaya As Button
    Friend WithEvents lblTransactionNo As Label
    Friend WithEvents txtTransactionNo As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents chkOnlinePayment As CheckBox
    Friend WithEvents lblWalletUsed As Label
    Friend WithEvents txtWalletUsed As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
End Class
