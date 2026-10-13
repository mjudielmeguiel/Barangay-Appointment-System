Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient

Public Class frmAdmin_Dashboard

    ' Declare a timer for the real-time clock display
    Private WithEvents clockTimer As New Timer()

    ' Form Load event: Initializes UI elements, timers, and loads data grids
    Private Sub frmAdmin_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MakeCircularPictureBox()
        LoadAdminProfile()
        LoadDashboardStats()

        ' Setup and start the real-time clock timer (ticks every 1 second)
        clockTimer.Interval = 1000
        clockTimer.Start()
        UpdateDateTimeDisplay() ' Update immediately to avoid 1-second delay

        ' Apply styling and load data into DataGridViews if they exist on the form
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

        ' Setup and load complaints grid (hiding resolved items)
        If dgvComplaints IsNot Nothing Then
            StyleDataGridView(dgvComplaints)
            LoadComplaints()
        End If

        ' Setup and load appointments grid (default to Total Today)
        If dgvAppointments IsNot Nothing Then
            StyleDataGridView(dgvAppointments)
            LoadAppointments("TOTAL")
        End If
    End Sub

    ' Tick event handler for the real-time clock
    Private Sub clockTimer_Tick(sender As Object, e As EventArgs) Handles clockTimer.Tick
        UpdateDateTimeDisplay()
    End Sub

    ' Updates the time label with the current date and time format
    Private Sub UpdateDateTimeDisplay()
        If lblTime IsNot Nothing Then
            lblTime.Text = DateTime.Now.ToString("MMMM dd, yyyy - dddd | hh:mm:ss tt")
        End If
    End Sub

    ' Makes the admin profile picture circular using GraphicsPath
    Private Sub MakeCircularPictureBox()
        If PictureBox1 IsNot Nothing Then
            Dim path As New GraphicsPath()
            path.AddEllipse(0, 0, PictureBox1.Width, PictureBox1.Height)
            PictureBox1.Region = New Region(path)
        End If
    End Sub

    ' Loads the logged-in admin's profile details (Name and Image) from the database
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
                        MsgBox("Admin record not found.", MsgBoxStyle.Exclamation)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading admin profile: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' Allows the admin to change their profile picture by double-clicking the PictureBox
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

    ' Helper function to execute scalar queries safely
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

    ' Loads statistics for appointments, users, and tickets to display on dashboard cards
    Private Sub LoadDashboardStats()
        Try
            connection()

            ' 📊 APPOINTMENT STATS COUNTERS (Today Only)
            Dim onlineCount As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%online%' OR LOWER(AppointmentType) LIKE '%online%') AND DATE(DateSubmitted) = CURDATE()")
            SafeSetText(lblOnline, onlineCount.ToString())

            Dim walkinCount As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%walk%' OR LOWER(AppointmentType) LIKE '%walk%') AND DATE(DateSubmitted) = CURDATE()")
            SafeSetText(lblWalkIn, walkinCount.ToString())

            Dim totalAppointments As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE DATE(DateSubmitted) = CURDATE()")
            SafeSetText(lblTotalAppointments, totalAppointments.ToString())

            Dim cancelledOnline As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%online%' OR LOWER(AppointmentType) LIKE '%online%') AND LOWER(Status) LIKE '%cancel%' AND DATE(DateSubmitted) = CURDATE()")
            SafeSetText(lblCancelledOnline, cancelledOnline.ToString())

            Dim cancelledWalkin As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%walk%' OR LOWER(AppointmentType) LIKE '%walk%') AND LOWER(Status) LIKE '%cancel%' AND DATE(DateSubmitted) = CURDATE()")
            SafeSetText(lblCancelledWalkIn, cancelledWalkin.ToString())

            ' 👥 OTHER SYSTEM STATS
            Dim staffCount As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE LOWER(Role) = 'staff' AND IsDeleted = 0")
            SafeSetText(lblStaff, staffCount.ToString())

            Dim activeCount As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE LOWER(AccountStatus) = 'active' AND IsDeleted = 0")
            SafeSetText(lblActive, activeCount.ToString())

            Dim lockedCount As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE (LOWER(AccountStatus) LIKE '%lock%' OR (LockoutExpiry IS NOT NULL AND LockoutExpiry > NOW())) AND IsDeleted = 0")
            SafeSetText(lblLocked, lockedCount.ToString())

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

    ' === 4. LOAD COMPLAINTS (HIDES RESOLVED COMPLAINTS & LOADS PENDING COUNT) ===
    Private Sub LoadComplaints()
        Try
            connection()
            Dim query As String = "SELECT ComplaintID, ControlNo AS 'Control No.', ComplaintType AS 'Type', Title AS 'Subject / Title', " &
                                  "DateSubmitted AS 'Date Submitted', Status, " &
                                  "CASE WHEN IsAnonymous = 1 THEN 'Yes' ELSE 'No' END AS 'Anonymous' " &
                                  "FROM complaints WHERE LOWER(Status) <> 'resolved' ORDER BY DateSubmitted DESC"

            Using cmd As New MySqlCommand(query, cn)
                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvComplaints.DataSource = dt
                End Using
            End Using

            If dgvComplaints.Columns.Contains("ComplaintID") Then
                dgvComplaints.Columns("ComplaintID").Visible = False
            End If

            LoadPendingComplaintsCount()

        Catch ex As Exception
            MsgBox("Error loading complaints: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === 5. LOAD APPOINTMENTS RECORDS WITH FILTER (TODAY ONLY) ===
    Private Sub LoadAppointments(filterType As String)
        Try
            connection()
            Dim whereClause As String = "DATE(DateSubmitted) = CURDATE()"

            Select Case filterType.ToUpper()
                Case "ONLINE"
                    whereClause &= " AND (LOWER(RequestType) LIKE '%online%' OR LOWER(AppointmentType) LIKE '%online%')"
                Case "WALKIN"
                    whereClause &= " AND (LOWER(RequestType) LIKE '%walk%' OR LOWER(AppointmentType) LIKE '%walk%')"
                Case "CANCELLED_ONLINE"
                    whereClause &= " AND (LOWER(RequestType) LIKE '%online%' OR LOWER(AppointmentType) LIKE '%online%') AND LOWER(Status) LIKE '%cancel%'"
                Case "CANCELLED_WALKIN"
                    whereClause &= " AND (LOWER(RequestType) LIKE '%walk%' OR LOWER(AppointmentType) LIKE '%walk%') AND LOWER(Status) LIKE '%cancel%'"
                Case Else
                    ' TOTAL (Today) - No extra filter needed
            End Select

            Dim query As String = "SELECT AppointmentID, ControlNo AS 'Control No.', FullName AS 'Full Name', RequestType AS 'Type', " &
                                  "Department, IFNULL(DATE_FORMAT(AppointmentDate, '%Y-%m-%d'), 'N/A') AS 'Appointment Date', Status " &
                                  "FROM appointments WHERE " & whereClause & " ORDER BY DateSubmitted DESC"

            Using cmd As New MySqlCommand(query, cn)
                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvAppointments.DataSource = dt
                End Using
            End Using

            If dgvAppointments.Columns.Contains("AppointmentID") Then
                dgvAppointments.Columns("AppointmentID").Visible = False
            End If

        Catch ex As Exception
            MsgBox("Error loading appointments: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === CLICK EVENTS PARA SA APPOINTMENT STAT CARDS / LABELS ===
    Private Sub lblOnline_Click(sender As Object, e As EventArgs) Handles lblOnline.Click
        LoadAppointments("ONLINE")
    End Sub

    Private Sub lblWalkIn_Click(sender As Object, e As EventArgs) Handles lblWalkIn.Click
        LoadAppointments("WALKIN")
    End Sub

    Private Sub lblTotalAppointments_Click(sender As Object, e As EventArgs) Handles lblTotalAppointments.Click
        LoadAppointments("TOTAL")
    End Sub

    Private Sub lblCancelledOnline_Click(sender As Object, e As EventArgs) Handles lblCancelledOnline.Click
        LoadAppointments("CANCELLED_ONLINE")
    End Sub

    Private Sub lblCancelledWalkIn_Click(sender As Object, e As EventArgs) Handles lblCancelledWalkIn.Click
        LoadAppointments("CANCELLED_WALKIN")
    End Sub

    ' Counts pending complaints submitted within the past 24 hours
    Private Sub LoadPendingComplaintsCount()
        Try
            Dim pendingCount As Integer = GetScalarCount("SELECT COUNT(*) FROM complaints WHERE LOWER(Status) = 'pending' AND DateSubmitted >= (NOW() - INTERVAL 24 HOUR)")

            If lblPendingComplaints IsNot Nothing Then
                lblPendingComplaints.Text = pendingCount.ToString()
            End If

            If dgvComplaints IsNot Nothing Then
                Dim activeCount As Integer = 0
                For Each row As DataGridViewRow In dgvComplaints.Rows
                    If row.Cells("Status") IsNot Nothing AndAlso row.Cells("Status").Value IsNot Nothing Then
                        If row.Cells("Status").Value.ToString().ToLower() = "pending" Then
                            activeCount += 1
                        End If
                    End If
                Next

                If lblPendingComplaints IsNot Nothing Then
                    lblPendingComplaints.Text = activeCount.ToString()
                End If
            End If

        Catch ex As Exception
            MsgBox("Error counting complaints: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' === STYLING FOR DATAGRIDVIEWS ===
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

    Private Sub BtnSignout_Click(sender As Object, e As EventArgs) Handles BtnSignout.Click
        If MsgBox("Are you sure you want to logout?",
                MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout") = MsgBoxResult.No Then Return

        Try
            connection()
            Dim userId As Integer = If(frmlogin.LoggedInUserID > 0, frmlogin.LoggedInUserID, 1)
            Dim targetTable As String = "admin"
            Dim idColumnName As String = "AdminID"

            Using cmdUpdate As New MySqlCommand($"UPDATE {targetTable} SET AccountStatus='Offline' WHERE {idColumnName}=@userId", cn)
                cmdUpdate.Parameters.AddWithValue("@userId", userId)
                cmdUpdate.ExecuteNonQuery()
            End Using

            Dim ipAddress As String = GetLocalIPAddress()
            Dim deviceInfo As String = $"{Environment.MachineName} | {Environment.OSVersion.VersionString}"

            Dim logQuery As String = "INSERT INTO activity_logs (ActionDate, ActionType, Details, DeviceInfo, FullName, IPAddress, Module, UserID, UserRole) " &
                                     "VALUES (NOW(), @actionType, @details, @device, @fullname, @ip, @module, @userid, @role)"

            Using cmdLog As New MySqlCommand(logQuery, cn)
                cmdLog.Parameters.AddWithValue("@actionType", "LOGOUT")
                cmdLog.Parameters.AddWithValue("@details", "Admin logged out successfully")
                cmdLog.Parameters.AddWithValue("@device", deviceInfo)
                cmdLog.Parameters.AddWithValue("@fullname", If(Not String.IsNullOrEmpty(frmlogin.LoggedInFullname), frmlogin.LoggedInFullname, "Administrator"))
                cmdLog.Parameters.AddWithValue("@ip", ipAddress)
                cmdLog.Parameters.AddWithValue("@module", "AUTHENTICATION")
                cmdLog.Parameters.AddWithValue("@userid", userId)
                cmdLog.Parameters.AddWithValue("@role", If(Not String.IsNullOrEmpty(frmlogin.LoggedInRole), frmlogin.LoggedInRole, "Administrator"))

                cmdLog.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MsgBox("Logout error: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
            frmlogin.LoggedInUsername = ""
            frmlogin.LoggedInFullname = ""
            frmlogin.LoggedInRole = ""
            frmlogin.LoggedInUserID = 0
            Me.Hide()
            Application.Restart()
        End Try
    End Sub

    Private Function GetLocalIPAddress() As String
        Try
            Dim host = Dns.GetHostEntry(Dns.GetHostName())
            For Each ip In host.AddressList
                If ip.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
        Catch
        End Try
        Return "Unknown"
    End Function
End Class