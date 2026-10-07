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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnClearAll = New System.Windows.Forms.Button()
        Me.btnCreateRequest = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.pnlaccountsystem = New System.Windows.Forms.Panel()
        Me.cboPurpose = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblRepresentative = New System.Windows.Forms.Label()
        Me.lblControlNo = New System.Windows.Forms.Label()
        Me.txtNameOfRepresentative = New System.Windows.Forms.TextBox()
        Me.cboRequestFor = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.cboRequestType = New System.Windows.Forms.ComboBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblUsernameError = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnSelectResident = New System.Windows.Forms.Button()
        Me.lblMiddlenameError = New System.Windows.Forms.Label()
        Me.lblEmailError = New System.Windows.Forms.Label()
        Me.lblMobileError = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtMobileNumber = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtMotherName = New System.Windows.Forms.TextBox()
        Me.txtFatherName = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtStreet = New System.Windows.Forms.TextBox()
        Me.lblStreetAddressError = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cboCivilStatus = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cboGender = New System.Windows.Forms.ComboBox()
        Me.txtBirthPlace = New System.Windows.Forms.TextBox()
        Me.lblBirthPlaceError = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dtpDateOfBirth = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtMiddle = New System.Windows.Forms.TextBox()
        Me.txtFirstname = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblFirstnameError = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboSuffix = New System.Windows.Forms.ComboBox()
        Me.txtLastname = New System.Windows.Forms.TextBox()
        Me.lblLastnameError = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlaccountsystem.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.btnClearAll)
        Me.Panel1.Controls.Add(Me.btnCreateRequest)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.pnlaccountsystem)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1366, 768)
        Me.Panel1.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1349, 60)
        Me.Panel5.TabIndex = 715
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(12, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(154, 17)
        Me.Label4.TabIndex = 520
        Me.Label4.Text = "Fill all Document Request"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(12, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(122, 21)
        Me.Label9.TabIndex = 519
        Me.Label9.Text = "Create Request"
        '
        'btnClearAll
        '
        Me.btnClearAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearAll.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearAll.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.btnClearAll.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClearAll.Location = New System.Drawing.Point(902, 838)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(161, 37)
        Me.btnClearAll.TabIndex = 714
        Me.btnClearAll.Text = "Clear All"
        Me.btnClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClearAll.UseVisualStyleBackColor = False
        '
        'btnCreateRequest
        '
        Me.btnCreateRequest.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateRequest.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.btnCreateRequest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCreateRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateRequest.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateRequest.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCreateRequest.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCreateRequest.Location = New System.Drawing.Point(1069, 838)
        Me.btnCreateRequest.Name = "btnCreateRequest"
        Me.btnCreateRequest.Size = New System.Drawing.Size(161, 37)
        Me.btnCreateRequest.TabIndex = 713
        Me.btnCreateRequest.Text = "Create Request"
        Me.btnCreateRequest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCreateRequest.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.Location = New System.Drawing.Point(37, 881)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1278, 127)
        Me.Panel4.TabIndex = 712
        '
        'pnlaccountsystem
        '
        Me.pnlaccountsystem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlaccountsystem.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.pnlaccountsystem.Controls.Add(Me.cboPurpose)
        Me.pnlaccountsystem.Controls.Add(Me.Label1)
        Me.pnlaccountsystem.Controls.Add(Me.lblRepresentative)
        Me.pnlaccountsystem.Controls.Add(Me.lblControlNo)
        Me.pnlaccountsystem.Controls.Add(Me.txtNameOfRepresentative)
        Me.pnlaccountsystem.Controls.Add(Me.cboRequestFor)
        Me.pnlaccountsystem.Controls.Add(Me.Label38)
        Me.pnlaccountsystem.Controls.Add(Me.cboRequestType)
        Me.pnlaccountsystem.Controls.Add(Me.Label39)
        Me.pnlaccountsystem.Controls.Add(Me.Label25)
        Me.pnlaccountsystem.Controls.Add(Me.Label27)
        Me.pnlaccountsystem.Controls.Add(Me.lblUsernameError)
        Me.pnlaccountsystem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlaccountsystem.Location = New System.Drawing.Point(37, 126)
        Me.pnlaccountsystem.Name = "pnlaccountsystem"
        Me.pnlaccountsystem.Size = New System.Drawing.Size(1193, 235)
        Me.pnlaccountsystem.TabIndex = 711
        '
        'cboPurpose
        '
        Me.cboPurpose.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboPurpose.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPurpose.FormattingEnabled = True
        Me.cboPurpose.Location = New System.Drawing.Point(28, 166)
        Me.cboPurpose.Name = "cboPurpose"
        Me.cboPurpose.Size = New System.Drawing.Size(290, 28)
        Me.cboPurpose.TabIndex = 726
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(25, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 25)
        Me.Label1.TabIndex = 725
        Me.Label1.Text = "Request Details"
        '
        'lblRepresentative
        '
        Me.lblRepresentative.AutoSize = True
        Me.lblRepresentative.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRepresentative.Location = New System.Drawing.Point(333, 145)
        Me.lblRepresentative.Name = "lblRepresentative"
        Me.lblRepresentative.Size = New System.Drawing.Size(94, 17)
        Me.lblRepresentative.TabIndex = 722
        Me.lblRepresentative.Text = "Representative"
        '
        'lblControlNo
        '
        Me.lblControlNo.AutoSize = True
        Me.lblControlNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblControlNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblControlNo.Location = New System.Drawing.Point(331, 20)
        Me.lblControlNo.Name = "lblControlNo"
        Me.lblControlNo.Size = New System.Drawing.Size(13, 17)
        Me.lblControlNo.TabIndex = 721
        Me.lblControlNo.Text = "-"
        '
        'txtNameOfRepresentative
        '
        Me.txtNameOfRepresentative.BackColor = System.Drawing.SystemColors.Window
        Me.txtNameOfRepresentative.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameOfRepresentative.Location = New System.Drawing.Point(336, 165)
        Me.txtNameOfRepresentative.Name = "txtNameOfRepresentative"
        Me.txtNameOfRepresentative.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtNameOfRepresentative.Size = New System.Drawing.Size(290, 29)
        Me.txtNameOfRepresentative.TabIndex = 720
        '
        'cboRequestFor
        '
        Me.cboRequestFor.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboRequestFor.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRequestFor.FormattingEnabled = True
        Me.cboRequestFor.Location = New System.Drawing.Point(28, 80)
        Me.cboRequestFor.Name = "cboRequestFor"
        Me.cboRequestFor.Size = New System.Drawing.Size(290, 28)
        Me.cboRequestFor.TabIndex = 720
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(332, 111)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(13, 17)
        Me.Label38.TabIndex = 673
        Me.Label38.Text = "-"
        '
        'cboRequestType
        '
        Me.cboRequestType.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboRequestType.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRequestType.FormattingEnabled = True
        Me.cboRequestType.Location = New System.Drawing.Point(336, 80)
        Me.cboRequestType.Name = "cboRequestType"
        Me.cboRequestType.Size = New System.Drawing.Size(290, 28)
        Me.cboRequestType.TabIndex = 720
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(25, 60)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(74, 17)
        Me.Label39.TabIndex = 672
        Me.Label39.Text = "RequestFor"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(27, 145)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(56, 17)
        Me.Label25.TabIndex = 669
        Me.Label25.Text = "Purpose"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(332, 60)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(82, 17)
        Me.Label27.TabIndex = 662
        Me.Label27.Text = "RequestType"
        '
        'lblUsernameError
        '
        Me.lblUsernameError.AutoSize = True
        Me.lblUsernameError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsernameError.Location = New System.Drawing.Point(31, 111)
        Me.lblUsernameError.Name = "lblUsernameError"
        Me.lblUsernameError.Size = New System.Drawing.Size(13, 17)
        Me.lblUsernameError.TabIndex = 570
        Me.lblUsernameError.Text = "-"
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel3.Controls.Add(Me.btnSelectResident)
        Me.Panel3.Controls.Add(Me.lblMiddlenameError)
        Me.Panel3.Controls.Add(Me.lblEmailError)
        Me.Panel3.Controls.Add(Me.lblMobileError)
        Me.Panel3.Controls.Add(Me.Label36)
        Me.Panel3.Controls.Add(Me.txtEmail)
        Me.Panel3.Controls.Add(Me.txtMobileNumber)
        Me.Panel3.Controls.Add(Me.Label37)
        Me.Panel3.Controls.Add(Me.Label33)
        Me.Panel3.Controls.Add(Me.txtMotherName)
        Me.Panel3.Controls.Add(Me.txtFatherName)
        Me.Panel3.Controls.Add(Me.Label34)
        Me.Panel3.Controls.Add(Me.txtAddress)
        Me.Panel3.Controls.Add(Me.Label35)
        Me.Panel3.Controls.Add(Me.txtStreet)
        Me.Panel3.Controls.Add(Me.lblStreetAddressError)
        Me.Panel3.Controls.Add(Me.Label29)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.cboCivilStatus)
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Controls.Add(Me.cboGender)
        Me.Panel3.Controls.Add(Me.txtBirthPlace)
        Me.Panel3.Controls.Add(Me.lblBirthPlaceError)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.dtpDateOfBirth)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.txtMiddle)
        Me.Panel3.Controls.Add(Me.txtFirstname)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.lblFirstnameError)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.cboSuffix)
        Me.Panel3.Controls.Add(Me.txtLastname)
        Me.Panel3.Controls.Add(Me.lblLastnameError)
        Me.Panel3.Location = New System.Drawing.Point(37, 415)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1193, 401)
        Me.Panel3.TabIndex = 710
        '
        'btnSelectResident
        '
        Me.btnSelectResident.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectResident.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSelectResident.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSelectResident.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectResident.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectResident.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.btnSelectResident.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSelectResident.Location = New System.Drawing.Point(1142, 13)
        Me.btnSelectResident.Name = "btnSelectResident"
        Me.btnSelectResident.Size = New System.Drawing.Size(36, 30)
        Me.btnSelectResident.TabIndex = 724
        Me.btnSelectResident.Text = "..."
        Me.btnSelectResident.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSelectResident.UseVisualStyleBackColor = False
        '
        'lblMiddlenameError
        '
        Me.lblMiddlenameError.AutoSize = True
        Me.lblMiddlenameError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMiddlenameError.Location = New System.Drawing.Point(640, 109)
        Me.lblMiddlenameError.Name = "lblMiddlenameError"
        Me.lblMiddlenameError.Size = New System.Drawing.Size(13, 17)
        Me.lblMiddlenameError.TabIndex = 719
        Me.lblMiddlenameError.Text = "-"
        '
        'lblEmailError
        '
        Me.lblEmailError.AutoSize = True
        Me.lblEmailError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmailError.Location = New System.Drawing.Point(919, 370)
        Me.lblEmailError.Name = "lblEmailError"
        Me.lblEmailError.Size = New System.Drawing.Size(13, 17)
        Me.lblEmailError.TabIndex = 718
        Me.lblEmailError.Text = "-"
        '
        'lblMobileError
        '
        Me.lblMobileError.AutoSize = True
        Me.lblMobileError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobileError.Location = New System.Drawing.Point(640, 370)
        Me.lblMobileError.Name = "lblMobileError"
        Me.lblMobileError.Size = New System.Drawing.Size(15, 19)
        Me.lblMobileError.TabIndex = 717
        Me.lblMobileError.Text = "-"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(920, 319)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(39, 17)
        Me.Label36.TabIndex = 716
        Me.Label36.Text = "Email"
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(923, 339)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtEmail.Size = New System.Drawing.Size(257, 28)
        Me.txtEmail.TabIndex = 715
        '
        'txtMobileNumber
        '
        Me.txtMobileNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobileNumber.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobileNumber.Location = New System.Drawing.Point(644, 339)
        Me.txtMobileNumber.Name = "txtMobileNumber"
        Me.txtMobileNumber.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtMobileNumber.Size = New System.Drawing.Size(257, 28)
        Me.txtMobileNumber.TabIndex = 714
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(641, 319)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(101, 17)
        Me.Label37.TabIndex = 713
        Me.Label37.Text = "Mobile Number"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(333, 319)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(86, 17)
        Me.Label33.TabIndex = 712
        Me.Label33.Text = "MotherName"
        '
        'txtMotherName
        '
        Me.txtMotherName.BackColor = System.Drawing.SystemColors.Window
        Me.txtMotherName.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMotherName.Location = New System.Drawing.Point(336, 339)
        Me.txtMotherName.Name = "txtMotherName"
        Me.txtMotherName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtMotherName.Size = New System.Drawing.Size(290, 28)
        Me.txtMotherName.TabIndex = 711
        '
        'txtFatherName
        '
        Me.txtFatherName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFatherName.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFatherName.Location = New System.Drawing.Point(29, 339)
        Me.txtFatherName.Name = "txtFatherName"
        Me.txtFatherName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtFatherName.Size = New System.Drawing.Size(290, 28)
        Me.txtFatherName.TabIndex = 710
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(26, 319)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(79, 17)
        Me.Label34.TabIndex = 709
        Me.Label34.Text = "FatherName"
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddress.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.Location = New System.Drawing.Point(29, 254)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtAddress.Size = New System.Drawing.Size(1151, 29)
        Me.txtAddress.TabIndex = 707
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(26, 234)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(79, 17)
        Me.Label35.TabIndex = 708
        Me.Label35.Text = "Full Address"
        '
        'txtStreet
        '
        Me.txtStreet.BackColor = System.Drawing.SystemColors.Window
        Me.txtStreet.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStreet.Location = New System.Drawing.Point(29, 254)
        Me.txtStreet.Name = "txtStreet"
        Me.txtStreet.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtStreet.Size = New System.Drawing.Size(597, 28)
        Me.txtStreet.TabIndex = 707
        '
        'lblStreetAddressError
        '
        Me.lblStreetAddressError.AutoSize = True
        Me.lblStreetAddressError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStreetAddressError.Location = New System.Drawing.Point(26, 285)
        Me.lblStreetAddressError.Name = "lblStreetAddressError"
        Me.lblStreetAddressError.Size = New System.Drawing.Size(13, 17)
        Me.lblStreetAddressError.TabIndex = 701
        Me.lblStreetAddressError.Text = "-"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(26, 195)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(13, 17)
        Me.Label29.TabIndex = 698
        Me.Label29.Text = "-"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(920, 144)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(70, 17)
        Me.Label23.TabIndex = 697
        Me.Label23.Text = "Civil Status"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(919, 199)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(13, 17)
        Me.Label24.TabIndex = 696
        Me.Label24.Text = "-"
        '
        'cboCivilStatus
        '
        Me.cboCivilStatus.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboCivilStatus.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCivilStatus.FormattingEnabled = True
        Me.cboCivilStatus.Location = New System.Drawing.Point(923, 164)
        Me.cboCivilStatus.Name = "cboCivilStatus"
        Me.cboCivilStatus.Size = New System.Drawing.Size(257, 28)
        Me.cboCivilStatus.TabIndex = 695
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(641, 144)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(51, 17)
        Me.Label21.TabIndex = 694
        Me.Label21.Text = "Gender"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(640, 195)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(13, 17)
        Me.Label22.TabIndex = 693
        Me.Label22.Text = "-"
        '
        'cboGender
        '
        Me.cboGender.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboGender.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGender.FormattingEnabled = True
        Me.cboGender.Location = New System.Drawing.Point(644, 164)
        Me.cboGender.Name = "cboGender"
        Me.cboGender.Size = New System.Drawing.Size(257, 28)
        Me.cboGender.TabIndex = 692
        '
        'txtBirthPlace
        '
        Me.txtBirthPlace.BackColor = System.Drawing.SystemColors.Window
        Me.txtBirthPlace.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBirthPlace.Location = New System.Drawing.Point(336, 164)
        Me.txtBirthPlace.Name = "txtBirthPlace"
        Me.txtBirthPlace.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtBirthPlace.Size = New System.Drawing.Size(290, 29)
        Me.txtBirthPlace.TabIndex = 690
        '
        'lblBirthPlaceError
        '
        Me.lblBirthPlaceError.AutoSize = True
        Me.lblBirthPlaceError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBirthPlaceError.Location = New System.Drawing.Point(332, 194)
        Me.lblBirthPlaceError.Name = "lblBirthPlaceError"
        Me.lblBirthPlaceError.Size = New System.Drawing.Size(13, 17)
        Me.lblBirthPlaceError.TabIndex = 691
        Me.lblBirthPlaceError.Text = "-"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(333, 144)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(64, 17)
        Me.Label20.TabIndex = 689
        Me.Label20.Text = "BirthPlace"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(27, 144)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(81, 17)
        Me.Label18.TabIndex = 688
        Me.Label18.Text = "Date of Birth"
        '
        'dtpDateOfBirth
        '
        Me.dtpDateOfBirth.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateOfBirth.Location = New System.Drawing.Point(29, 164)
        Me.dtpDateOfBirth.Name = "dtpDateOfBirth"
        Me.dtpDateOfBirth.Size = New System.Drawing.Size(290, 28)
        Me.dtpDateOfBirth.TabIndex = 687
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(920, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 17)
        Me.Label2.TabIndex = 686
        Me.Label2.Text = "Suffix"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(641, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 17)
        Me.Label6.TabIndex = 685
        Me.Label6.Text = "Middle"
        '
        'txtMiddle
        '
        Me.txtMiddle.BackColor = System.Drawing.SystemColors.Window
        Me.txtMiddle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMiddle.Location = New System.Drawing.Point(644, 79)
        Me.txtMiddle.Name = "txtMiddle"
        Me.txtMiddle.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtMiddle.Size = New System.Drawing.Size(257, 29)
        Me.txtMiddle.TabIndex = 684
        '
        'txtFirstname
        '
        Me.txtFirstname.BackColor = System.Drawing.SystemColors.Window
        Me.txtFirstname.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstname.Location = New System.Drawing.Point(336, 80)
        Me.txtFirstname.Name = "txtFirstname"
        Me.txtFirstname.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtFirstname.Size = New System.Drawing.Size(290, 29)
        Me.txtFirstname.TabIndex = 682
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(919, 111)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 17)
        Me.Label5.TabIndex = 492
        Me.Label5.Text = "-"
        '
        'lblFirstnameError
        '
        Me.lblFirstnameError.AutoSize = True
        Me.lblFirstnameError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFirstnameError.Location = New System.Drawing.Point(332, 109)
        Me.lblFirstnameError.Name = "lblFirstnameError"
        Me.lblFirstnameError.Size = New System.Drawing.Size(13, 17)
        Me.lblFirstnameError.TabIndex = 683
        Me.lblFirstnameError.Text = "-"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(333, 59)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 17)
        Me.Label11.TabIndex = 660
        Me.Label11.Text = "Firstname"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(26, 59)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(63, 17)
        Me.Label12.TabIndex = 659
        Me.Label12.Text = "Lastname"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(24, 17)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(270, 25)
        Me.Label13.TabIndex = 658
        Me.Label13.Text = "Resident Personal Information"
        '
        'cboSuffix
        '
        Me.cboSuffix.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboSuffix.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSuffix.FormattingEnabled = True
        Me.cboSuffix.Location = New System.Drawing.Point(923, 80)
        Me.cboSuffix.Name = "cboSuffix"
        Me.cboSuffix.Size = New System.Drawing.Size(257, 28)
        Me.cboSuffix.TabIndex = 458
        '
        'txtLastname
        '
        Me.txtLastname.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastname.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastname.Location = New System.Drawing.Point(29, 79)
        Me.txtLastname.Name = "txtLastname"
        Me.txtLastname.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtLastname.Size = New System.Drawing.Size(290, 29)
        Me.txtLastname.TabIndex = 679
        '
        'lblLastnameError
        '
        Me.lblLastnameError.AutoSize = True
        Me.lblLastnameError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastnameError.Location = New System.Drawing.Point(25, 109)
        Me.lblLastnameError.Name = "lblLastnameError"
        Me.lblLastnameError.Size = New System.Drawing.Size(13, 17)
        Me.lblLastnameError.TabIndex = 681
        Me.lblLastnameError.Text = "-"
        '
        'frmCreateAppointment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCreateAppointment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCreateAppointment"
        Me.Panel1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.pnlaccountsystem.ResumeLayout(False)
        Me.pnlaccountsystem.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents pnlaccountsystem As Panel
    Friend WithEvents Label38 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents lblUsernameError As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lblMiddlenameError As Label
    Friend WithEvents lblEmailError As Label
    Friend WithEvents lblMobileError As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtMobileNumber As TextBox
    Friend WithEvents Label37 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents txtMotherName As TextBox
    Friend WithEvents txtFatherName As TextBox
    Friend WithEvents Label34 As Label
    Friend WithEvents Label35 As Label
    Friend WithEvents txtStreet As TextBox
    Friend WithEvents lblStreetAddressError As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents cboCivilStatus As ComboBox
    Friend WithEvents Label21 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents cboGender As ComboBox
    Friend WithEvents txtBirthPlace As TextBox
    Friend WithEvents lblBirthPlaceError As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtMiddle As TextBox
    Friend WithEvents txtFirstname As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents lblFirstnameError As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents cboSuffix As ComboBox
    Friend WithEvents txtLastname As TextBox
    Friend WithEvents lblLastnameError As Label
    Friend WithEvents cboRequestType As ComboBox
    Friend WithEvents txtNameOfRepresentative As TextBox
    Friend WithEvents cboRequestFor As ComboBox
    Friend WithEvents lblControlNo As Label
    Friend WithEvents lblRepresentative As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents btnClearAll As Button
    Friend WithEvents btnCreateRequest As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents btnSelectResident As Button
    Friend WithEvents dtpDateOfBirth As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents cboPurpose As ComboBox
End Class
