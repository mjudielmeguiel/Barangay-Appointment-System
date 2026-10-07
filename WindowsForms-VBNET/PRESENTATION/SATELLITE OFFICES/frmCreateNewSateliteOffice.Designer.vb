<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCreateNewSateliteOffice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreateNewSateliteOffice))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClearAll = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txtSearchResident = New System.Windows.Forms.TextBox()
        Me.dgvSatelliteResidents = New System.Windows.Forms.DataGridView()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblOperationStatusError = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cboOperationStatus = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.cboFacilityType = New System.Windows.Forms.ComboBox()
        Me.lblFacilityTypeError = New System.Windows.Forms.Label()
        Me.cboHasPermanentStaff = New System.Windows.Forms.ComboBox()
        Me.lblRoleError = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtServingArea = New System.Windows.Forms.TextBox()
        Me.lblServingAreaError = New System.Windows.Forms.Label()
        Me.txtContactNote = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblSatelliteOfficeNumber = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtFacilityName = New System.Windows.Forms.TextBox()
        Me.lblFacilityNameError = New System.Windows.Forms.Label()
        Me.txtLocationDetails = New System.Windows.Forms.TextBox()
        Me.lblLocationDetailsError = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgvSatelliteResidents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1366, 768)
        Me.Panel1.TabIndex = 0
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel7.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel7.Controls.Add(Me.Label3)
        Me.Panel7.Controls.Add(Me.btnClearAll)
        Me.Panel7.Controls.Add(Me.Panel5)
        Me.Panel7.Controls.Add(Me.dgvSatelliteResidents)
        Me.Panel7.Controls.Add(Me.btnSubmit)
        Me.Panel7.Location = New System.Drawing.Point(37, 613)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1259, 143)
        Me.Panel7.TabIndex = 687
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(25, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(228, 21)
        Me.Label3.TabIndex = 521
        Me.Label3.Text = "Sattelite Office Residence List"
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
        Me.btnClearAll.Location = New System.Drawing.Point(1114, 12)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(131, 37)
        Me.btnClearAll.TabIndex = 552
        Me.btnClearAll.Text = "refresh"
        Me.btnClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClearAll.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Panel5.Controls.Add(Me.txtSearchResident)
        Me.Panel5.Location = New System.Drawing.Point(0, 61)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1259, 38)
        Me.Panel5.TabIndex = 509
        '
        'txtSearchResident
        '
        Me.txtSearchResident.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtSearchResident.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchResident.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchResident.Location = New System.Drawing.Point(15, 5)
        Me.txtSearchResident.Name = "txtSearchResident"
        Me.txtSearchResident.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtSearchResident.Size = New System.Drawing.Size(254, 28)
        Me.txtSearchResident.TabIndex = 223
        '
        'dgvSatelliteResidents
        '
        Me.dgvSatelliteResidents.AllowUserToAddRows = False
        Me.dgvSatelliteResidents.AllowUserToDeleteRows = False
        Me.dgvSatelliteResidents.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSatelliteResidents.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvSatelliteResidents.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvSatelliteResidents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSatelliteResidents.Location = New System.Drawing.Point(0, 100)
        Me.dgvSatelliteResidents.Name = "dgvSatelliteResidents"
        Me.dgvSatelliteResidents.ReadOnly = True
        Me.dgvSatelliteResidents.Size = New System.Drawing.Size(1259, 43)
        Me.dgvSatelliteResidents.TabIndex = 508
        '
        'btnSubmit
        '
        Me.btnSubmit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSubmit.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubmit.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubmit.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSubmit.Location = New System.Drawing.Point(977, 12)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(131, 37)
        Me.btnSubmit.TabIndex = 506
        Me.btnSubmit.Text = "Create"
        Me.btnSubmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSubmit.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel6.Controls.Add(Me.lblOperationStatusError)
        Me.Panel6.Controls.Add(Me.Label25)
        Me.Panel6.Controls.Add(Me.cboOperationStatus)
        Me.Panel6.Controls.Add(Me.Label26)
        Me.Panel6.Controls.Add(Me.Label27)
        Me.Panel6.Controls.Add(Me.Label28)
        Me.Panel6.Controls.Add(Me.cboFacilityType)
        Me.Panel6.Controls.Add(Me.lblFacilityTypeError)
        Me.Panel6.Controls.Add(Me.cboHasPermanentStaff)
        Me.Panel6.Controls.Add(Me.lblRoleError)
        Me.Panel6.Location = New System.Drawing.Point(37, 362)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1259, 223)
        Me.Panel6.TabIndex = 683
        '
        'lblOperationStatusError
        '
        Me.lblOperationStatusError.AutoSize = True
        Me.lblOperationStatusError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOperationStatusError.Location = New System.Drawing.Point(422, 111)
        Me.lblOperationStatusError.Name = "lblOperationStatusError"
        Me.lblOperationStatusError.Size = New System.Drawing.Size(15, 19)
        Me.lblOperationStatusError.TabIndex = 670
        Me.lblOperationStatusError.Text = "-"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(423, 60)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(106, 17)
        Me.Label25.TabIndex = 669
        Me.Label25.Text = "Operation Status"
        '
        'cboOperationStatus
        '
        Me.cboOperationStatus.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboOperationStatus.Font = New System.Drawing.Font("Microsoft YaHei UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOperationStatus.FormattingEnabled = True
        Me.cboOperationStatus.Location = New System.Drawing.Point(426, 80)
        Me.cboOperationStatus.Name = "cboOperationStatus"
        Me.cboOperationStatus.Size = New System.Drawing.Size(388, 28)
        Me.cboOperationStatus.TabIndex = 668
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(27, 140)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(126, 17)
        Me.Label26.TabIndex = 663
        Me.Label26.Text = "Has Permanent Staff"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(27, 60)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(78, 17)
        Me.Label27.TabIndex = 662
        Me.Label27.Text = "Facility Type"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft YaHei UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(29, 20)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(153, 26)
        Me.Label28.TabIndex = 658
        Me.Label28.Text = "Facility Details"
        '
        'cboFacilityType
        '
        Me.cboFacilityType.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboFacilityType.Font = New System.Drawing.Font("Microsoft YaHei UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFacilityType.FormattingEnabled = True
        Me.cboFacilityType.Location = New System.Drawing.Point(30, 80)
        Me.cboFacilityType.Name = "cboFacilityType"
        Me.cboFacilityType.Size = New System.Drawing.Size(371, 28)
        Me.cboFacilityType.TabIndex = 568
        '
        'lblFacilityTypeError
        '
        Me.lblFacilityTypeError.AutoSize = True
        Me.lblFacilityTypeError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacilityTypeError.Location = New System.Drawing.Point(31, 111)
        Me.lblFacilityTypeError.Name = "lblFacilityTypeError"
        Me.lblFacilityTypeError.Size = New System.Drawing.Size(15, 19)
        Me.lblFacilityTypeError.TabIndex = 570
        Me.lblFacilityTypeError.Text = "-"
        '
        'cboHasPermanentStaff
        '
        Me.cboHasPermanentStaff.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboHasPermanentStaff.Font = New System.Drawing.Font("Microsoft YaHei UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboHasPermanentStaff.FormattingEnabled = True
        Me.cboHasPermanentStaff.Location = New System.Drawing.Point(30, 160)
        Me.cboHasPermanentStaff.Name = "cboHasPermanentStaff"
        Me.cboHasPermanentStaff.Size = New System.Drawing.Size(371, 28)
        Me.cboHasPermanentStaff.TabIndex = 565
        '
        'lblRoleError
        '
        Me.lblRoleError.AutoSize = True
        Me.lblRoleError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoleError.Location = New System.Drawing.Point(26, 191)
        Me.lblRoleError.Name = "lblRoleError"
        Me.lblRoleError.Size = New System.Drawing.Size(15, 19)
        Me.lblRoleError.TabIndex = 567
        Me.lblRoleError.Text = "-"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1366, 60)
        Me.Panel4.TabIndex = 684
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(12, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(254, 19)
        Me.Label2.TabIndex = 520
        Me.Label2.Text = "Fill in Saltellite Office Information Below"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 21)
        Me.Label1.TabIndex = 519
        Me.Label1.Text = "Satellite Office"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.txtServingArea)
        Me.Panel2.Controls.Add(Me.lblServingAreaError)
        Me.Panel2.Controls.Add(Me.txtContactNote)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.txtRemarks)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.lblSatelliteOfficeNumber)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.txtFacilityName)
        Me.Panel2.Controls.Add(Me.lblFacilityNameError)
        Me.Panel2.Controls.Add(Me.txtLocationDetails)
        Me.Panel2.Controls.Add(Me.lblLocationDetailsError)
        Me.Panel2.Location = New System.Drawing.Point(37, 97)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1259, 223)
        Me.Panel2.TabIndex = 682
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(826, 140)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 17)
        Me.Label9.TabIndex = 668
        Me.Label9.Text = "Remarks"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(423, 59)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 17)
        Me.Label10.TabIndex = 667
        Me.Label10.Text = "Serving Area"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(422, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(139, 22)
        Me.Label11.TabIndex = 667
        Me.Label11.Text = "Office Number :"
        '
        'txtServingArea
        '
        Me.txtServingArea.BackColor = System.Drawing.SystemColors.Window
        Me.txtServingArea.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServingArea.Location = New System.Drawing.Point(426, 79)
        Me.txtServingArea.Name = "txtServingArea"
        Me.txtServingArea.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtServingArea.Size = New System.Drawing.Size(388, 28)
        Me.txtServingArea.TabIndex = 664
        '
        'lblServingAreaError
        '
        Me.lblServingAreaError.AutoSize = True
        Me.lblServingAreaError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServingAreaError.Location = New System.Drawing.Point(422, 109)
        Me.lblServingAreaError.Name = "lblServingAreaError"
        Me.lblServingAreaError.Size = New System.Drawing.Size(15, 19)
        Me.lblServingAreaError.TabIndex = 666
        Me.lblServingAreaError.Text = "-"
        '
        'txtContactNote
        '
        Me.txtContactNote.BackColor = System.Drawing.SystemColors.Window
        Me.txtContactNote.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactNote.Location = New System.Drawing.Point(829, 79)
        Me.txtContactNote.Name = "txtContactNote"
        Me.txtContactNote.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtContactNote.Size = New System.Drawing.Size(388, 28)
        Me.txtContactNote.TabIndex = 506
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(825, 110)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(15, 19)
        Me.Label16.TabIndex = 518
        Me.Label16.Text = "-"
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(829, 160)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtRemarks.Size = New System.Drawing.Size(388, 28)
        Me.txtRemarks.TabIndex = 663
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(826, 59)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(85, 17)
        Me.Label18.TabIndex = 660
        Me.Label18.Text = "Contact Note"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(825, 191)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(15, 19)
        Me.Label19.TabIndex = 665
        Me.Label19.Text = "-"
        '
        'lblSatelliteOfficeNumber
        '
        Me.lblSatelliteOfficeNumber.AutoSize = True
        Me.lblSatelliteOfficeNumber.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSatelliteOfficeNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.lblSatelliteOfficeNumber.Location = New System.Drawing.Point(567, 21)
        Me.lblSatelliteOfficeNumber.Name = "lblSatelliteOfficeNumber"
        Me.lblSatelliteOfficeNumber.Size = New System.Drawing.Size(17, 22)
        Me.lblSatelliteOfficeNumber.TabIndex = 554
        Me.lblSatelliteOfficeNumber.Text = "-"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(26, 140)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(100, 17)
        Me.Label21.TabIndex = 661
        Me.Label21.Text = "Location Details"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(26, 59)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(85, 17)
        Me.Label22.TabIndex = 659
        Me.Label22.Text = "Facility Name"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft YaHei UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(24, 17)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(201, 26)
        Me.Label23.TabIndex = 658
        Me.Label23.Text = "Facility Information"
        '
        'txtFacilityName
        '
        Me.txtFacilityName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFacilityName.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacilityName.Location = New System.Drawing.Point(29, 79)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtFacilityName.Size = New System.Drawing.Size(372, 28)
        Me.txtFacilityName.TabIndex = 508
        '
        'lblFacilityNameError
        '
        Me.lblFacilityNameError.AutoSize = True
        Me.lblFacilityNameError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacilityNameError.Location = New System.Drawing.Point(25, 109)
        Me.lblFacilityNameError.Name = "lblFacilityNameError"
        Me.lblFacilityNameError.Size = New System.Drawing.Size(15, 19)
        Me.lblFacilityNameError.TabIndex = 519
        Me.lblFacilityNameError.Text = "-"
        '
        'txtLocationDetails
        '
        Me.txtLocationDetails.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocationDetails.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationDetails.Location = New System.Drawing.Point(29, 160)
        Me.txtLocationDetails.Name = "txtLocationDetails"
        Me.txtLocationDetails.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtLocationDetails.Size = New System.Drawing.Size(785, 28)
        Me.txtLocationDetails.TabIndex = 520
        '
        'lblLocationDetailsError
        '
        Me.lblLocationDetailsError.AutoSize = True
        Me.lblLocationDetailsError.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationDetailsError.Location = New System.Drawing.Point(26, 191)
        Me.lblLocationDetailsError.Name = "lblLocationDetailsError"
        Me.lblLocationDetailsError.Size = New System.Drawing.Size(15, 19)
        Me.lblLocationDetailsError.TabIndex = 522
        Me.lblLocationDetailsError.Text = "-"
        '
        'frmCreateNewSateliteOffice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCreateNewSateliteOffice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCreateNewSateliteOffice"
        Me.Panel1.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.dgvSatelliteResidents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents lblOperationStatusError As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents cboOperationStatus As ComboBox
    Friend WithEvents Label26 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents cboFacilityType As ComboBox
    Friend WithEvents lblFacilityTypeError As Label
    Friend WithEvents cboHasPermanentStaff As ComboBox
    Friend WithEvents lblRoleError As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents txtServingArea As TextBox
    Friend WithEvents lblServingAreaError As Label
    Friend WithEvents txtContactNote As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtRemarks As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents lblSatelliteOfficeNumber As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents txtFacilityName As TextBox
    Friend WithEvents lblFacilityNameError As Label
    Friend WithEvents txtLocationDetails As TextBox
    Friend WithEvents lblLocationDetailsError As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents btnClearAll As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents txtSearchResident As TextBox
    Friend WithEvents dgvSatelliteResidents As DataGridView
    Friend WithEvents btnSubmit As Button
    Friend WithEvents Label3 As Label
End Class
