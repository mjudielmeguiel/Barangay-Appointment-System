<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAppointmentHistory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAppointmentHistory))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.dgvHistory = New System.Windows.Forms.DataGridView()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.cboStatusFilter = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlaccountsystem = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblControlNo = New System.Windows.Forms.Label()
        Me.txtSearchControlNo = New System.Windows.Forms.TextBox()
        Me.cboAppointmentType = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblUsernameError = New System.Windows.Forms.Label()
        CType(Me.dgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.pnlaccountsystem.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvHistory
        '
        Me.dgvHistory.AllowUserToAddRows = False
        Me.dgvHistory.AllowUserToDeleteRows = False
        Me.dgvHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvHistory.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHistory.Location = New System.Drawing.Point(37, 417)
        Me.dgvHistory.Name = "dgvHistory"
        Me.dgvHistory.ReadOnly = True
        Me.dgvHistory.Size = New System.Drawing.Size(1278, 339)
        Me.dgvHistory.TabIndex = 602
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(918, 143)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(22, 17)
        Me.Label8.TabIndex = 606
        Me.Label8.Text = "To"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(918, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 17)
        Me.Label7.TabIndex = 605
        Me.Label7.Text = "From"
        '
        'dtpToDate
        '
        Me.dtpToDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpToDate.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Location = New System.Drawing.Point(922, 165)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(289, 29)
        Me.dtpToDate.TabIndex = 604
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFromDate.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Location = New System.Drawing.Point(922, 80)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(289, 29)
        Me.dtpFromDate.TabIndex = 603
        '
        'cboStatusFilter
        '
        Me.cboStatusFilter.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboStatusFilter.Font = New System.Drawing.Font("Microsoft YaHei UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatusFilter.FormattingEnabled = True
        Me.cboStatusFilter.Location = New System.Drawing.Point(28, 165)
        Me.cboStatusFilter.Name = "cboStatusFilter"
        Me.cboStatusFilter.Size = New System.Drawing.Size(597, 28)
        Me.cboStatusFilter.TabIndex = 607
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1366, 60)
        Me.Panel5.TabIndex = 717
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(12, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(280, 19)
        Me.Label1.TabIndex = 520
        Me.Label1.Text = "You can View the Old Appointment Records"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(12, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(166, 21)
        Me.Label9.TabIndex = 519
        Me.Label9.Text = "Appointment History"
        '
        'pnlaccountsystem
        '
        Me.pnlaccountsystem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlaccountsystem.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.pnlaccountsystem.Controls.Add(Me.Label2)
        Me.pnlaccountsystem.Controls.Add(Me.lblControlNo)
        Me.pnlaccountsystem.Controls.Add(Me.Label7)
        Me.pnlaccountsystem.Controls.Add(Me.Label8)
        Me.pnlaccountsystem.Controls.Add(Me.dtpFromDate)
        Me.pnlaccountsystem.Controls.Add(Me.cboStatusFilter)
        Me.pnlaccountsystem.Controls.Add(Me.txtSearchControlNo)
        Me.pnlaccountsystem.Controls.Add(Me.dtpToDate)
        Me.pnlaccountsystem.Controls.Add(Me.cboAppointmentType)
        Me.pnlaccountsystem.Controls.Add(Me.Label38)
        Me.pnlaccountsystem.Controls.Add(Me.Label39)
        Me.pnlaccountsystem.Controls.Add(Me.Label25)
        Me.pnlaccountsystem.Controls.Add(Me.Label27)
        Me.pnlaccountsystem.Controls.Add(Me.lblUsernameError)
        Me.pnlaccountsystem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlaccountsystem.Location = New System.Drawing.Point(37, 126)
        Me.pnlaccountsystem.Name = "pnlaccountsystem"
        Me.pnlaccountsystem.Size = New System.Drawing.Size(1278, 235)
        Me.pnlaccountsystem.TabIndex = 718
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(25, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(144, 25)
        Me.Label2.TabIndex = 725
        Me.Label2.Text = "Request Details"
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
        'txtSearchControlNo
        '
        Me.txtSearchControlNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearchControlNo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchControlNo.Location = New System.Drawing.Point(335, 79)
        Me.txtSearchControlNo.Name = "txtSearchControlNo"
        Me.txtSearchControlNo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtSearchControlNo.Size = New System.Drawing.Size(290, 29)
        Me.txtSearchControlNo.TabIndex = 720
        '
        'cboAppointmentType
        '
        Me.cboAppointmentType.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboAppointmentType.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAppointmentType.FormattingEnabled = True
        Me.cboAppointmentType.Location = New System.Drawing.Point(28, 80)
        Me.cboAppointmentType.Name = "cboAppointmentType"
        Me.cboAppointmentType.Size = New System.Drawing.Size(290, 28)
        Me.cboAppointmentType.TabIndex = 720
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
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(25, 60)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(114, 17)
        Me.Label39.TabIndex = 672
        Me.Label39.Text = "Appointment Type"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(27, 145)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(80, 17)
        Me.Label25.TabIndex = 669
        Me.Label25.Text = "Service Type"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(332, 60)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(76, 17)
        Me.Label27.TabIndex = 662
        Me.Label27.Text = "Control No."
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
        'frmAppointmentHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.pnlaccountsystem)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.dgvHistory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAppointmentHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmAppointmentHistory"
        CType(Me.dgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.pnlaccountsystem.ResumeLayout(False)
        Me.pnlaccountsystem.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents dgvHistory As DataGridView
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents dtpToDate As DateTimePicker
    Friend WithEvents dtpFromDate As DateTimePicker
    Friend WithEvents cboStatusFilter As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents pnlaccountsystem As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents lblControlNo As Label
    Friend WithEvents txtSearchControlNo As TextBox
    Friend WithEvents cboAppointmentType As ComboBox
    Friend WithEvents Label38 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents lblUsernameError As Label
End Class
