Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D

Public Class frmAdmin_Dashboard

    Private WithEvents clockTimer As New Timer()

    Private Sub frmAdmin_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MakeCircularPictureBox()
        LoadAdminProfile()
        LoadDashboardStats()

        ' ✅ I-setup ang Real-Time Clock Timer
        clockTimer.Interval = 1000 ' Bawat 1 segundo
        clockTimer.Start()
        UpdateDateTimeDisplay() ' Agad na tawagin para di maghintay ng 1 segundo

        ' ✅ I-apply ang styling at i-load ang mga data sa tatlong DataGridViews
        If dgvActivityLogs IsNot Nothing Then
            StyleDataGridView(dgvActivityLogs)
            LoadActivityLogs()
        End If

        If dgvUsers IsNot Nothing Then
            StyleDataGridView(dgvUsers)
            LoadUsersBasicInfo()
        End If

        If dgvTickets IsNot Nothing Then
            StyleDataGridView(dgvTickets)
            LoadPasswordTickets()
        End If
    End Sub

    ' === TICK EVENT PARA SA REAL-TIME DATE AND TIME ===
    Private Sub clockTimer_Tick(sender As Object, e As EventArgs) Handles clockTimer.Tick
        UpdateDateTimeDisplay()
    End Sub

    Private Sub UpdateDateTimeDisplay()
        ' PALITAN ANG lblTime NG KUNG ANO MAN ANG TAWAG MO SA LABEL SA PROPERTIES MO
        If lblTime IsNot Nothing Then
            lblTime.Text = DateTime.Now.ToString("MMMM dd, yyyy - dddd | hh:mm:ss tt")
        End If
    End Sub

    ' === FUNCTION PARA MAGING BILOG ANG PROFILE PICTURE ===
    Private Sub MakeCircularPictureBox()
        If PictureBox1 IsNot Nothing Then
            Dim path As New GraphicsPath()
            path.AddEllipse(0, 0, PictureBox1.Width, PictureBox1.Height)
            PictureBox1.Region = New Region(path)
        End If
    End Sub

    Private Sub LoadAdminProfile()
        Try
            Dim targetID As Integer = If(frmlogin.LoggedInUserID > 0, frmlogin.LoggedInUserID, 1)

            connection()
            Dim query As String = "SELECT Firstname, Lastname, Picture FROM admin WHERE AdminID = @AdminID"
            Using cmdAdmin As New MySqlCommand(query, cn)
                cmdAdmin.Parameters.AddWithValue("@AdminID", targetID)

                Using reader As MySqlDataReader = cmdAdmin.ExecuteReader()
                    If reader.Read() Then
                        Dim firstName As String = If(reader("Firstname") IsNot DBNull.Value, reader("Firstname").ToString(), "")
                        Dim lastName As String = If(reader("Lastname") IsNot DBNull.Value, reader("Lastname").ToString(), "")

                        If lblFullname IsNot Nothing Then
                            lblFullname.Text = (firstName & " " & lastName).Trim()
                        End If

                        If reader("Picture") IsNot DBNull.Value Then
                            Dim imgData As Byte() = DirectCast(reader("Picture"), Byte())
                            Using ms As New MemoryStream(imgData)
                                If PictureBox1 IsNot Nothing Then
                                    PictureBox1.Image = Image.FromStream(ms)
                                    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                                End If
                            End Using
                        Else
                            If PictureBox1 IsNot Nothing Then
                                PictureBox1.Image = Nothing
                            End If
                        End If
                    Else
                        If lblFullname IsNot Nothing Then
                            lblFullname.Text = "Not Found"
                        End If
                        MsgBox("Hindi mahanap ang Admin record.", MsgBoxStyle.Exclamation)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading admin profile: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Title = "Select Profile Picture"
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim imgBytes As Byte() = File.ReadAllBytes(openFileDialog.FileName)
                    Dim targetID As Integer = If(frmlogin.LoggedInUserID > 0, frmlogin.LoggedInUserID, 1)

                    connection()
                    Dim query As String = "UPDATE admin SET Picture = @Picture WHERE AdminID = @AdminID"
                    Using cmd As New MySqlCommand(query, cn)
                        cmd.Parameters.AddWithValue("@Picture", imgBytes)
                        cmd.Parameters.AddWithValue("@AdminID", targetID)

                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected > 0 Then
                            MsgBox("Profile picture updated successfully!", MsgBoxStyle.Information)
                            LoadAdminProfile()
                        Else
                            MsgBox("Failed to update profile picture.", MsgBoxStyle.Exclamation)
                        End If
                    End Using
                Catch ex As Exception
                    MsgBox("Error updating profile picture: " & ex.Message, MsgBoxStyle.Critical)
                Finally
                    CloseConnection()
                End Try
            End If
        End Using
    End Sub

    Private Function GetScalarCount(query As String) As Integer
        Try
            Using cmd As New MySqlCommand(query, cn)
                Dim res = cmd.ExecuteScalar()
                If res IsNot Nothing AndAlso res IsNot DBNull.Value Then
                    Return Convert.ToInt32(res)
                End If
            End Using
        Catch ex As Exception
        End Try
        Return 0
    End Function

    Private Sub LoadDashboardStats()
        Try
            connection()

            ' 1. Online: Kasama ang lahat ng pumasok ngayong araw (CURDATE) OR 'pending' ang status kahit luma na.
            Dim onlineCount As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%online%' OR LOWER(AppointmentType) LIKE '%online%') AND (DATE(DateSubmitted) = CURDATE() OR LOWER(Status) = 'pending')")
            SafeSetText(lblOnline, onlineCount.ToString())

            ' 2. Walk-in: Magre-reset araw-araw (CURDATE lang)
            Dim walkinCount As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%walk%' OR LOWER(AppointmentType) LIKE '%walk%') AND DATE(DateSubmitted) = CURDATE()")
            SafeSetText(lblWalkIn, walkinCount.ToString())

            ' 3. Total Appointments: Magre-reset araw-araw (CURDATE lang)
            Dim totalAppointments As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE DATE(DateSubmitted) = CURDATE()")
            SafeSetText(lblTotalAppointments, totalAppointments.ToString())

            ' 4. Cancelled Online: Magre-reset araw-araw (CURDATE lang)
            Dim cancelledOnline As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%online%' OR LOWER(AppointmentType) LIKE '%online%') AND LOWER(Status) LIKE '%cancel%' AND DATE(DateSubmitted) = CURDATE()")
            SafeSetText(lblCancelledOnline, cancelledOnline.ToString())

            ' 5. Cancelled Walk-in: Magre-reset araw-araw (CURDATE lang)
            Dim cancelledWalkin As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%walk%' OR LOWER(AppointmentType) LIKE '%walk%') AND LOWER(Status) LIKE '%cancel%' AND DATE(DateSubmitted) = CURDATE()")
            SafeSetText(lblCancelledWalkIn, cancelledWalkin.ToString())

            ' --- USER STATS ---
            Dim totalUsers As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE IsDeleted = 0")
            SafeSetText(lblUsers, totalUsers.ToString())

            Dim staffCount As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE LOWER(Role) = 'staff' AND IsDeleted = 0")
            SafeSetText(lblStaff, staffCount.ToString())

            Dim activeCount As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE LOWER(AccountStatus) = 'active' AND IsDeleted = 0")
            SafeSetText(lblActive, activeCount.ToString())

            ' 6. Locked Users Fix: Nilagay sa lblLocked
            Dim lockedCount As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE (LOWER(AccountStatus) LIKE '%lock%' OR (LockoutExpiry IS NOT NULL AND LockoutExpiry > NOW())) AND IsDeleted = 0")
            SafeSetText(lblLocked, lockedCount.ToString())

            ' 7. NEW RESIDENCES TODAY LOGIC
            Dim newResCount As Integer = GetScalarCount("SELECT COUNT(*) FROM residences WHERE DATE(CreatedAt) = CURDATE()")
            SafeSetText(lblnewResident, newResCount.ToString())

            Dim recoveryCount As Integer = GetScalarCount("SELECT COUNT(*) FROM forgot_password_tickets WHERE status = 'PENDING'")
            SafeSetText(lblAccountRecovery, recoveryCount.ToString())

        Catch ex As Exception
            MsgBox("Error loading dashboard stats: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === 1. LOAD ACTIVITY LOGS ===
    Private Sub LoadActivityLogs()
        Try
            connection()
            Dim query As String = "SELECT ActionDate AS 'Date & Time', FullName AS 'Full Name', UserRole AS 'Role', Module, ActionType AS 'Action', Details, IPAddress AS 'IP Address' " &
                                  "FROM activity_logs WHERE DATE(ActionDate) = CURDATE() ORDER BY ActionDate DESC"
            Using cmd As New MySqlCommand(query, cn)
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvActivityLogs.DataSource = dt
            End Using
        Catch ex As Exception
            MsgBox("Error loading activity logs: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === 2. LOAD USERS BASIC INFO ===
    Private Sub LoadUsersBasicInfo()
        Try
            connection()
            Dim query As String = "SELECT CONCAT(Firstname, ' ', Lastname) AS 'Full Name', Username, Email, ContactNumber AS 'Contact No.', Role, AccountStatus AS 'Status', " &
                                  "CASE WHEN LOWER(AccountStatus) = 'active' THEN 'Active' ELSE 'Offline' END AS 'Activity Status', " &
                                  "LastLogin AS 'Last Login' FROM users WHERE IsDeleted = 0 ORDER BY UserID DESC"
            Using cmd As New MySqlCommand(query, cn)
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvUsers.DataSource = dt
            End Using
        Catch ex As Exception
            MsgBox("Error loading users: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === 3. LOAD FORGOT PASSWORD TICKETS ===
    Private Sub LoadPasswordTickets()
        Try
            connection()
            Dim query As String = "SELECT ticket_number AS 'Ticket No.', full_name AS 'Full Name', username AS 'Username', email, status AS 'Status', " &
                                  "IFNULL(DATE_FORMAT(requested_at, '%Y-%m-%d %H:%i:%s'), 'N/A') AS 'Requested At', " &
                                  "IFNULL(DATE_FORMAT(expires_at, '%Y-%m-%d %H:%i:%s'), 'N/A') AS 'Expires At' " &
                                  "FROM forgot_password_tickets WHERE status = 'PENDING' ORDER BY requested_at DESC"
            Using cmd As New MySqlCommand(query, cn)
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvTickets.DataSource = dt
            End Using
        Catch ex As Exception
            MsgBox("Error loading tickets: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === STYLING PARA SA MGA DATAGRIDVIEW ===
    Private Sub StyleDataGridView(dgv As DataGridView)
        dgv.EnableHeadersVisualStyles = False
        dgv.BorderStyle = BorderStyle.None
        dgv.BackgroundColor = Color.White
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.GridColor = Color.FromArgb(230, 235, 245)
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.AllowUserToResizeRows = False

        ' Header Style
        Dim headerStyle As New DataGridViewCellStyle()
        headerStyle.BackColor = Color.FromArgb(25, 42, 86)
        headerStyle.ForeColor = Color.White
        headerStyle.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        headerStyle.Padding = New Padding(12, 10, 12, 10)
        dgv.ColumnHeadersDefaultCellStyle = headerStyle
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersHeight = 42
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        ' Default Row Style
        Dim defaultRowStyle As New DataGridViewCellStyle()
        defaultRowStyle.BackColor = Color.White
        defaultRowStyle.ForeColor = Color.FromArgb(30, 41, 59)
        defaultRowStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        defaultRowStyle.SelectionBackColor = Color.FromArgb(210, 220, 245)
        defaultRowStyle.SelectionForeColor = Color.FromArgb(25, 42, 86)
        defaultRowStyle.Padding = New Padding(12, 6, 12, 6)

        dataGridAlternatingRowStyle(defaultRowStyle, dgv)
    End Sub

    Private Sub dataGridAlternatingRowStyle(defaultRowStyle As DataGridViewCellStyle, dgv As DataGridView)
        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle)
        alternatingRowStyle.BackColor = Color.FromArgb(248, 250, 252)
        dgv.DefaultCellStyle = defaultRowStyle
        dgv.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        dgv.RowTemplate.Height = 40
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub SafeSetText(lbl As Label, val As String)
        If lbl IsNot Nothing Then
            lbl.Text = val
        End If
    End Sub
End Class