<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDayAppointmentsList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDayAppointmentsList))
        Me.lvwAppointments = New System.Windows.Forms.ListView()
        Me.btnNewAppointment = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lvwEvents = New System.Windows.Forms.ListView()
        Me.lblApptHeader = New System.Windows.Forms.Label()
        Me.lblEventsHeader = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvwAppointments
        '
        Me.lvwAppointments.HideSelection = False
        Me.lvwAppointments.Location = New System.Drawing.Point(14, 100)
        Me.lvwAppointments.Name = "lvwAppointments"
        Me.lvwAppointments.Size = New System.Drawing.Size(645, 337)
        Me.lvwAppointments.TabIndex = 0
        Me.lvwAppointments.UseCompatibleStateImageBehavior = False
        '
        'btnNewAppointment
        '
        Me.btnNewAppointment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewAppointment.BackColor = System.Drawing.Color.Navy
        Me.btnNewAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnNewAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewAppointment.Font = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewAppointment.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnNewAppointment.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNewAppointment.Location = New System.Drawing.Point(705, 478)
        Me.btnNewAppointment.Name = "btnNewAppointment"
        Me.btnNewAppointment.Size = New System.Drawing.Size(131, 37)
        Me.btnNewAppointment.TabIndex = 507
        Me.btnNewAppointment.Text = "Create Request"
        Me.btnNewAppointment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNewAppointment.UseVisualStyleBackColor = False
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
        Me.btnClose.Location = New System.Drawing.Point(842, 478)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(131, 37)
        Me.btnClose.TabIndex = 553
        Me.btnClose.Text = "Close"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lvwEvents
        '
        Me.lvwEvents.HideSelection = False
        Me.lvwEvents.Location = New System.Drawing.Point(665, 100)
        Me.lvwEvents.Name = "lvwEvents"
        Me.lvwEvents.Size = New System.Drawing.Size(308, 337)
        Me.lvwEvents.TabIndex = 555
        Me.lvwEvents.UseCompatibleStateImageBehavior = False
        '
        'lblApptHeader
        '
        Me.lblApptHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblApptHeader.AutoSize = True
        Me.lblApptHeader.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApptHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblApptHeader.Location = New System.Drawing.Point(12, 440)
        Me.lblApptHeader.Name = "lblApptHeader"
        Me.lblApptHeader.Size = New System.Drawing.Size(91, 19)
        Me.lblApptHeader.TabIndex = 556
        Me.lblApptHeader.Text = "Appoinments"
        '
        'lblEventsHeader
        '
        Me.lblEventsHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEventsHeader.AutoSize = True
        Me.lblEventsHeader.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEventsHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblEventsHeader.Location = New System.Drawing.Point(663, 440)
        Me.lblEventsHeader.Name = "lblEventsHeader"
        Me.lblEventsHeader.Size = New System.Drawing.Size(49, 19)
        Me.lblEventsHeader.TabIndex = 557
        Me.lblEventsHeader.Text = "Events"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(22, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtSearch.Size = New System.Drawing.Size(254, 28)
        Me.txtSearch.TabIndex = 558
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lvwEvents)
        Me.Panel1.Controls.Add(Me.lblEventsHeader)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.lblApptHeader)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.lvwAppointments)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(985, 472)
        Me.Panel1.TabIndex = 559
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel3.Controls.Add(Me.txtSearch)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 52)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(985, 38)
        Me.Panel3.TabIndex = 557
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(985, 52)
        Me.Panel2.TabIndex = 556
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft YaHei UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(17, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(308, 28)
        Me.Label3.TabIndex = 560
        Me.Label3.Text = "Appointment and Event List"
        '
        'frmDayAppointmentsList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(985, 527)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnNewAppointment)
        Me.Controls.Add(Me.btnClose)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDayAppointmentsList"
        Me.Text = "frmDayAppointmentsList"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lvwAppointments As ListView
    Friend WithEvents btnNewAppointment As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents lvwEvents As ListView
    Friend WithEvents lblApptHeader As Label
    Friend WithEvents lblEventsHeader As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label3 As Label
End Class
