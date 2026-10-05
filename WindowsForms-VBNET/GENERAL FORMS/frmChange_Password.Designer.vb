<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChange_Password
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChange_Password))
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.txtNewPassword = New System.Windows.Forms.TextBox()
        Me.txtCurrentPassword = New System.Windows.Forms.TextBox()
        Me.lblNewError = New System.Windows.Forms.Label()
        Me.lblConfirmError = New System.Windows.Forms.Label()
        Me.lblCurrentError = New System.Windows.Forms.Label()
        Me.btnChangePassword = New System.Windows.Forms.Button()
        Me.pnlaccountsystem = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblRepresentative = New System.Windows.Forms.Label()
        Me.lblControlNo = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlaccountsystem.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmPassword.Location = New System.Drawing.Point(336, 165)
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.Size = New System.Drawing.Size(284, 29)
        Me.txtConfirmPassword.TabIndex = 538
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPassword.Location = New System.Drawing.Point(30, 165)
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.Size = New System.Drawing.Size(284, 29)
        Me.txtNewPassword.TabIndex = 539
        '
        'txtCurrentPassword
        '
        Me.txtCurrentPassword.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrentPassword.Location = New System.Drawing.Point(30, 80)
        Me.txtCurrentPassword.Name = "txtCurrentPassword"
        Me.txtCurrentPassword.Size = New System.Drawing.Size(284, 29)
        Me.txtCurrentPassword.TabIndex = 540
        '
        'lblNewError
        '
        Me.lblNewError.AutoSize = True
        Me.lblNewError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewError.Location = New System.Drawing.Point(30, 196)
        Me.lblNewError.Name = "lblNewError"
        Me.lblNewError.Size = New System.Drawing.Size(13, 17)
        Me.lblNewError.TabIndex = 565
        Me.lblNewError.Text = "-"
        '
        'lblConfirmError
        '
        Me.lblConfirmError.AutoSize = True
        Me.lblConfirmError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfirmError.Location = New System.Drawing.Point(332, 196)
        Me.lblConfirmError.Name = "lblConfirmError"
        Me.lblConfirmError.Size = New System.Drawing.Size(13, 17)
        Me.lblConfirmError.TabIndex = 566
        Me.lblConfirmError.Text = "-"
        '
        'lblCurrentError
        '
        Me.lblCurrentError.AutoSize = True
        Me.lblCurrentError.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentError.Location = New System.Drawing.Point(26, 111)
        Me.lblCurrentError.Name = "lblCurrentError"
        Me.lblCurrentError.Size = New System.Drawing.Size(13, 17)
        Me.lblCurrentError.TabIndex = 567
        Me.lblCurrentError.Text = "-"
        '
        'btnChangePassword
        '
        Me.btnChangePassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.btnChangePassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnChangePassword.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangePassword.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnChangePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnChangePassword.Location = New System.Drawing.Point(1159, 380)
        Me.btnChangePassword.Name = "btnChangePassword"
        Me.btnChangePassword.Size = New System.Drawing.Size(138, 37)
        Me.btnChangePassword.TabIndex = 598
        Me.btnChangePassword.Text = "Change Password"
        Me.btnChangePassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnChangePassword.UseVisualStyleBackColor = False
        '
        'pnlaccountsystem
        '
        Me.pnlaccountsystem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlaccountsystem.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.pnlaccountsystem.Controls.Add(Me.Label4)
        Me.pnlaccountsystem.Controls.Add(Me.lblRepresentative)
        Me.pnlaccountsystem.Controls.Add(Me.lblControlNo)
        Me.pnlaccountsystem.Controls.Add(Me.Label25)
        Me.pnlaccountsystem.Controls.Add(Me.lblCurrentError)
        Me.pnlaccountsystem.Controls.Add(Me.txtCurrentPassword)
        Me.pnlaccountsystem.Controls.Add(Me.lblNewError)
        Me.pnlaccountsystem.Controls.Add(Me.lblConfirmError)
        Me.pnlaccountsystem.Controls.Add(Me.txtNewPassword)
        Me.pnlaccountsystem.Controls.Add(Me.Label27)
        Me.pnlaccountsystem.Controls.Add(Me.txtConfirmPassword)
        Me.pnlaccountsystem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlaccountsystem.Location = New System.Drawing.Point(37, 126)
        Me.pnlaccountsystem.Name = "pnlaccountsystem"
        Me.pnlaccountsystem.Size = New System.Drawing.Size(1260, 235)
        Me.pnlaccountsystem.TabIndex = 712
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(25, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(144, 25)
        Me.Label4.TabIndex = 725
        Me.Label4.Text = "Request Details"
        '
        'lblRepresentative
        '
        Me.lblRepresentative.AutoSize = True
        Me.lblRepresentative.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRepresentative.Location = New System.Drawing.Point(333, 145)
        Me.lblRepresentative.Name = "lblRepresentative"
        Me.lblRepresentative.Size = New System.Drawing.Size(141, 17)
        Me.lblRepresentative.TabIndex = 722
        Me.lblRepresentative.Text = "Confirm new Password"
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
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(27, 145)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(94, 17)
        Me.Label25.TabIndex = 669
        Me.Label25.Text = "New Password"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(27, 60)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(111, 17)
        Me.Label27.TabIndex = 662
        Me.Label27.Text = "Current Password"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1366, 60)
        Me.Panel5.TabIndex = 716
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(12, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(142, 17)
        Me.Label5.TabIndex = 520
        Me.Label5.Text = "Change your Password"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(12, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(138, 21)
        Me.Label6.TabIndex = 519
        Me.Label6.Text = "Change Password"
        '
        'frmChange_Password
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ClientSize = New System.Drawing.Size(1366, 768)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.pnlaccountsystem)
        Me.Controls.Add(Me.btnChangePassword)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmChange_Password"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmChange_Password"
        Me.pnlaccountsystem.ResumeLayout(False)
        Me.pnlaccountsystem.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtConfirmPassword As TextBox
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents txtCurrentPassword As TextBox
    Friend WithEvents lblNewError As Label
    Friend WithEvents lblConfirmError As Label
    Friend WithEvents lblCurrentError As Label
    Friend WithEvents btnChangePassword As Button
    Friend WithEvents pnlaccountsystem As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents lblRepresentative As Label
    Friend WithEvents lblControlNo As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
End Class
