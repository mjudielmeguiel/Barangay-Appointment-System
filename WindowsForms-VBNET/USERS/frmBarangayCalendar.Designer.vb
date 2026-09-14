<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBarangayCalendar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBarangayCalendar))
        Me.btnNextMonth = New System.Windows.Forms.Button()
        Me.btnPrevMonth = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblCurrentMonthYear = New System.Windows.Forms.Label()
        Me.flpCalendarGrid = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnAddEvent = New System.Windows.Forms.Button()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNextMonth
        '
        Me.btnNextMonth.BackColor = System.Drawing.SystemColors.Control
        Me.btnNextMonth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnNextMonth.FlatAppearance.BorderSize = 0
        Me.btnNextMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNextMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNextMonth.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnNextMonth.Image = CType(resources.GetObject("btnNextMonth.Image"), System.Drawing.Image)
        Me.btnNextMonth.Location = New System.Drawing.Point(229, 66)
        Me.btnNextMonth.Name = "btnNextMonth"
        Me.btnNextMonth.Size = New System.Drawing.Size(47, 37)
        Me.btnNextMonth.TabIndex = 587
        Me.btnNextMonth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNextMonth.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNextMonth.UseVisualStyleBackColor = False
        '
        'btnPrevMonth
        '
        Me.btnPrevMonth.BackColor = System.Drawing.SystemColors.Control
        Me.btnPrevMonth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnPrevMonth.FlatAppearance.BorderSize = 0
        Me.btnPrevMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrevMonth.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnPrevMonth.Image = CType(resources.GetObject("btnPrevMonth.Image"), System.Drawing.Image)
        Me.btnPrevMonth.Location = New System.Drawing.Point(12, 68)
        Me.btnPrevMonth.Name = "btnPrevMonth"
        Me.btnPrevMonth.Size = New System.Drawing.Size(47, 37)
        Me.btnPrevMonth.TabIndex = 588
        Me.btnPrevMonth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrevMonth.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPrevMonth.UseVisualStyleBackColor = False
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
        Me.Panel4.TabIndex = 589
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
        Me.Label9.Size = New System.Drawing.Size(153, 22)
        Me.Label9.TabIndex = 519
        Me.Label9.Text = "Barangay Putatan"
        '
        'lblCurrentMonthYear
        '
        Me.lblCurrentMonthYear.AutoSize = True
        Me.lblCurrentMonthYear.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentMonthYear.ForeColor = System.Drawing.Color.Navy
        Me.lblCurrentMonthYear.Location = New System.Drawing.Point(65, 74)
        Me.lblCurrentMonthYear.Name = "lblCurrentMonthYear"
        Me.lblCurrentMonthYear.Size = New System.Drawing.Size(137, 22)
        Me.lblCurrentMonthYear.TabIndex = 593
        Me.lblCurrentMonthYear.Text = "December 2026"
        '
        'flpCalendarGrid
        '
        Me.flpCalendarGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpCalendarGrid.Location = New System.Drawing.Point(12, 111)
        Me.flpCalendarGrid.Name = "flpCalendarGrid"
        Me.flpCalendarGrid.Size = New System.Drawing.Size(1342, 645)
        Me.flpCalendarGrid.TabIndex = 0
        '
        'btnAddEvent
        '
        Me.btnAddEvent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddEvent.BackColor = System.Drawing.Color.DarkBlue
        Me.btnAddEvent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnAddEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddEvent.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddEvent.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAddEvent.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddEvent.Location = New System.Drawing.Point(1223, 66)
        Me.btnAddEvent.Name = "btnAddEvent"
        Me.btnAddEvent.Size = New System.Drawing.Size(131, 37)
        Me.btnAddEvent.TabIndex = 596
        Me.btnAddEvent.Text = "Add Event"
        Me.btnAddEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAddEvent.UseVisualStyleBackColor = False
        '
        'frmBarangayCalendar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.btnAddEvent)
        Me.Controls.Add(Me.flpCalendarGrid)
        Me.Controls.Add(Me.lblCurrentMonthYear)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnPrevMonth)
        Me.Controls.Add(Me.btnNextMonth)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBarangayCalendar"
        Me.Text = "frmBarangayCalendar"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnNextMonth As Button
    Friend WithEvents btnPrevMonth As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblCurrentMonthYear As Label
    Friend WithEvents flpCalendarGrid As FlowLayoutPanel
    Friend WithEvents btnAddEvent As Button
End Class
