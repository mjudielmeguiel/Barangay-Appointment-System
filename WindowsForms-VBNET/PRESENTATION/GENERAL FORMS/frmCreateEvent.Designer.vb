<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCreateEvent
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreateEvent))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lblFirstnameError = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpEventDate = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboStartTime = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboEndTime = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(269, 137)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 17)
        Me.Label5.TabIndex = 558
        Me.Label5.Text = "-"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(269, 86)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(61, 17)
        Me.Label63.TabIndex = 557
        Me.Label63.Text = "Category"
        '
        'cboCategory
        '
        Me.cboCategory.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboCategory.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(272, 106)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(315, 28)
        Me.cboCategory.TabIndex = 556
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(13, 85)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(32, 17)
        Me.Label53.TabIndex = 561
        Me.Label53.Text = "Title"
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Control
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(12, 105)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtTitle.Size = New System.Drawing.Size(254, 29)
        Me.txtTitle.TabIndex = 560
        '
        'lblFirstnameError
        '
        Me.lblFirstnameError.AutoSize = True
        Me.lblFirstnameError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFirstnameError.Location = New System.Drawing.Point(12, 137)
        Me.lblFirstnameError.Name = "lblFirstnameError"
        Me.lblFirstnameError.Size = New System.Drawing.Size(13, 17)
        Me.lblFirstnameError.TabIndex = 562
        Me.lblFirstnameError.Text = "-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(307, 292)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 17)
        Me.Label1.TabIndex = 565
        Me.Label1.Text = "Start"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(451, 292)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 17)
        Me.Label2.TabIndex = 566
        Me.Label2.Text = "End"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 179)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 17)
        Me.Label3.TabIndex = 568
        Me.Label3.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.BackColor = System.Drawing.SystemColors.Control
        Me.txtDescription.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(12, 199)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtDescription.Size = New System.Drawing.Size(575, 29)
        Me.txtDescription.TabIndex = 567
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 231)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(13, 17)
        Me.Label4.TabIndex = 569
        Me.Label4.Text = "-"
        '
        'dtpEventDate
        '
        Me.dtpEventDate.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEventDate.Location = New System.Drawing.Point(12, 312)
        Me.dtpEventDate.Name = "dtpEventDate"
        Me.dtpEventDate.Size = New System.Drawing.Size(292, 29)
        Me.dtpEventDate.TabIndex = 559
        Me.dtpEventDate.Value = New Date(2026, 9, 6, 13, 20, 0, 0)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 292)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 17)
        Me.Label6.TabIndex = 598
        Me.Label6.Text = "Event Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(310, 343)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(13, 17)
        Me.Label7.TabIndex = 601
        Me.Label7.Text = "-"
        '
        'cboStartTime
        '
        Me.cboStartTime.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboStartTime.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStartTime.FormattingEnabled = True
        Me.cboStartTime.Location = New System.Drawing.Point(310, 312)
        Me.cboStartTime.Name = "cboStartTime"
        Me.cboStartTime.Size = New System.Drawing.Size(138, 28)
        Me.cboStartTime.TabIndex = 600
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(453, 343)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(13, 17)
        Me.Label9.TabIndex = 603
        Me.Label9.Text = "-"
        '
        'cboEndTime
        '
        Me.cboEndTime.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboEndTime.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEndTime.FormattingEnabled = True
        Me.cboEndTime.Location = New System.Drawing.Point(454, 312)
        Me.cboEndTime.Name = "cboEndTime"
        Me.cboEndTime.Size = New System.Drawing.Size(133, 28)
        Me.cboEndTime.TabIndex = 602
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(599, 60)
        Me.Panel1.TabIndex = 632
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(12, 30)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(188, 17)
        Me.Label8.TabIndex = 520
        Me.Label8.Text = "Events or Activities in Barangay"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(12, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(151, 21)
        Me.Label12.TabIndex = 519
        Me.Label12.Text = "Create a New Event"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.Location = New System.Drawing.Point(456, 391)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(131, 37)
        Me.btnCancel.TabIndex = 634
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.Location = New System.Drawing.Point(319, 391)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(131, 37)
        Me.btnSave.TabIndex = 633
        Me.btnSave.Text = "Create"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'frmCreateEvent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(599, 442)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboEndTime)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboStartTime)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label53)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.lblFirstnameError)
        Me.Controls.Add(Me.dtpEventDate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label63)
        Me.Controls.Add(Me.cboCategory)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCreateEvent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create Event"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label5 As Label
    Friend WithEvents Label63 As Label
    Friend WithEvents cboCategory As ComboBox
    Friend WithEvents Label53 As Label
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents lblFirstnameError As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents dtpEventDate As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents cboStartTime As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents cboEndTime As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
End Class
