Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class frmMain
    ' === ✅ PROPERTIES ===
    Private ReadOnly Property LoggedFullname As String
        Get
            Return frmlogin.LoggedInFullname
        End Get
    End Property
    Private ReadOnly Property LoggedRole As String
        Get
            Return frmlogin.LoggedInRole
        End Get
    End Property
    Private ReadOnly Property LoggedUserID As Integer
        Get
            Return frmlogin.LoggedInUserID
        End Get
    End Property

    ' === SIDEBAR STATE ===
    Private isSidebarOpen As Boolean = False

    ' === ✅ FORM LOAD ===
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' --- SECURITY CHECK ---
        If String.IsNullOrWhiteSpace(frmlogin.LoggedInUsername) OrElse
           String.IsNullOrWhiteSpace(frmlogin.LoggedInFullname) Then
            LogUnauthorizedAttempt()
            Me.Close()
            frmlogin.Show()
            Return
        End If

        ' --- ✅ GAWING BILOG ANG PICTURE BOX ---
        MakeCircularPictureBox(picProfile)

        ' --- SIDEBAR INIT ---
        sidebarPanel.Visible = False
        sidebarPanel.Width = 240

        ' --- ✅ LOAD PROFILE INFO ---
        LoadUserProfileInfo()

        ' --- ROLE-BASED VISIBILITY ---
        Dim isAdmin As Boolean = String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase)
        btnUsers.Visible = isAdmin

        ' --- LOAD DEFAULT DASHBOARD ---
        Panel2.Controls.Clear()
        Dim defaultForm As Form = If(isAdmin,
            New frmAdmin_Dashboard With {
                .TopLevel = False,
                .FormBorderStyle = FormBorderStyle.None,
                .Dock = DockStyle.Fill
            },
            New frmUser_Dashboard With {
                .TopLevel = False,
                .FormBorderStyle = FormBorderStyle.None,
                .Dock = DockStyle.Fill
            })
        Panel2.Controls.Add(defaultForm)
        defaultForm.Show()
    End Sub

    ' === ✅ BAGONG METHOD: GAWING BILOG ===
    Private Sub MakeCircularPictureBox(pb As PictureBox)
        Dim size As Integer = Math.Min(pb.Width, pb.Height)
        pb.Width = size
        pb.Height = size
        Dim gp As New GraphicsPath()
        gp.AddEllipse(0, 0, pb.Width, pb.Height)
        pb.Region = New Region(gp)
        pb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    ' === ✅ FIXED LOAD PROFILE INFO — Supports BOTH admin & users tables ===
    Private Sub LoadUserProfileInfo()
        Try
            connection()
            Dim isAdmin As Boolean = String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase)
            Dim targetTable As String = If(isAdmin, "admin", "users")
            Dim idColumn As String = If(isAdmin, "AdminID", "UserID")
            Dim userId As Integer = LoggedUserID

            ' --- ✅ SAFE SELECT — Handles tables properly ---
            Dim sql As String = $"SELECT Firstname, Lastname, Picture FROM {targetTable} WHERE {idColumn} = @userId"

            ' Add Role to SELECT only if it exists in the table
            If Not isAdmin Then
                sql = $"SELECT Firstname, Lastname, Role, Picture FROM {targetTable} WHERE {idColumn} = @userId"
            Else
                ' Admin table — Role has default value, safe to include now
                sql = $"SELECT Firstname, Lastname, Role, Picture FROM {targetTable} WHERE {idColumn} = @userId"
            End If

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@userId", userId)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        ' --- FULL NAME ---
                        Dim fName As String = dr("Firstname").ToString().Trim()
                        Dim lName As String = dr("Lastname").ToString().Trim()
                        Dim displayName As String = $"{fName} {lName}".Trim()
                        lblFullname.Text = If(Not String.IsNullOrWhiteSpace(displayName), displayName, LoggedFullname)

                        ' --- ✅ ROLE — Smart fallback for admin table ---
                        Dim roleValue As String = ""
                        If Not isAdmin Then
                            If Not IsDBNull(dr("Role")) Then
                                roleValue = dr("Role").ToString().Trim()
                            End If
                        Else
                            ' Admin: use DB value or default
                            If Not IsDBNull(dr("Role")) Then
                                roleValue = dr("Role").ToString().Trim()
                            End If
                            If String.IsNullOrWhiteSpace(roleValue) Then
                                roleValue = "Administrator"
                            End If
                        End If

                        lblUserRole.Text = If(Not String.IsNullOrWhiteSpace(roleValue), roleValue.ToUpper(),
                            If(Not String.IsNullOrWhiteSpace(LoggedRole), LoggedRole.ToUpper(), "USER"))

                        ' --- PROFILE PICTURE ---
                        If Not IsDBNull(dr("Picture")) Then
                            Dim picBytes As Byte() = CType(dr("Picture"), Byte())
                            Using ms As New MemoryStream(picBytes)
                                picProfile.Image = Image.FromStream(ms)
                            End Using
                        Else
                            picProfile.Image = Nothing
                        End If
                    Else
                        ' --- Fallback if no record found ---
                        lblFullname.Text = LoggedFullname
                        lblUserRole.Text = If(isAdmin, "ADMINISTRATOR",
                            If(Not String.IsNullOrWhiteSpace(LoggedRole), LoggedRole.ToUpper(), "USER"))
                        picProfile.Image = Nothing
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading profile: " & ex.Message, MsgBoxStyle.Information)
            ' --- Safe fallback on error ---
            lblFullname.Text = LoggedFullname
            lblUserRole.Text = If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
                "ADMINISTRATOR",
                If(Not String.IsNullOrWhiteSpace(LoggedRole), LoggedRole.ToUpper(), "USER"))
            picProfile.Image = Nothing
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === ✅ SIDEBAR TOGGLE ===
    Private Sub btnToggleSidebar_Click(sender As Object, e As EventArgs) Handles btnToggleSidebar.Click
        isSidebarOpen = Not isSidebarOpen
        sidebarPanel.Visible = isSidebarOpen
        sidebarPanel.BringToFront()
    End Sub

    ' === ✅ MENU NAVIGATION ===
    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        LoadFormIntoPanel(If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
            GetType(frmAdmin_Dashboard), GetType(frmUser_Dashboard)))
        CloseSidebar()
    End Sub

    Private Sub btnResidents_Click(sender As Object, e As EventArgs) Handles btnResidents.Click
        LoadFormIntoPanel(GetType(frmResidence_Records))
        CloseSidebar()
    End Sub

    Private Sub btnUsers_Click(sender As Object, e As EventArgs) Handles btnUsers.Click
        LoadFormIntoPanel(GetType(frmManage_Users))
        CloseSidebar()
    End Sub

    Private Sub btnPayments_Click(sender As Object, e As EventArgs) Handles btnPayments.Click
        Dim Payments As New frmPayments With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill,
            .PreviousForm = Me
        }
        Panel2.Controls.Clear()
        Panel2.Controls.Add(Payments)
        Payments.Show()
        CloseSidebar()
    End Sub

    Private Sub btnCalendar_Click(sender As Object, e As EventArgs) Handles btnCalendar.Click
        LoadFormIntoPanel(GetType(frmBarangayCalendar))
        CloseSidebar()
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        LoadFormIntoPanel(GetType(frmAppointmentHistory))
        CloseSidebar()
    End Sub

    Private Sub btnDocuments_Click(sender As Object, e As EventArgs) Handles btnDocuments.Click
        frmDocumentServices.Show()
        CloseSidebar()
    End Sub

    ' === ✅ HELPER METHODS ===
    Private Sub LoadFormIntoPanel(formType As Type)
        Panel2.Controls.Clear()
        Dim frm As Form = CType(Activator.CreateInstance(formType), Form)
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        Panel2.Controls.Add(frm)
        frm.Show()
    End Sub

    Private Sub CloseSidebar()
        isSidebarOpen = False
        sidebarPanel.Visible = False
    End Sub

    ' === ✅ LOGOUT — Fully supports admin, users, residences tables ===
    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure you want to logout?",
                  MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout") = MsgBoxResult.No Then
            Return
        End If

        Try
            connection()
            Dim userId As Integer = LoggedUserID
            Dim targetTable As String = ""
            Dim idColumnName As String = ""

            If String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase) Then
                targetTable = "admin"
                idColumnName = "AdminID"
            ElseIf String.Equals(LoggedRole, "Residence", StringComparison.OrdinalIgnoreCase) OrElse
                   String.Equals(LoggedRole, "Resident", StringComparison.OrdinalIgnoreCase) Then
                targetTable = "residences"
                idColumnName = "ResidentID"
            Else
                targetTable = "users"
                idColumnName = "UserID"
            End If

            If Not String.IsNullOrEmpty(targetTable) AndAlso userId > 0 Then
                Using cmdUpdate As New MySqlCommand($"UPDATE {targetTable} SET AccountStatus='Offline' WHERE {idColumnName}=@userId", cn)
                    cmdUpdate.Parameters.AddWithValue("@userId", userId)
                    cmdUpdate.ExecuteNonQuery()
                End Using
            End If

            Dim ipAddress As String = "127.0.0.1"
            Try
                Dim host As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName())
                For Each ip As IPAddress In host.AddressList
                    If ip.AddressFamily = AddressFamily.InterNetwork Then
                        ipAddress = ip.ToString()
                        Exit For
                    End If
                Next
            Catch
                ipAddress = "Unknown"
            End Try

            Dim deviceInfo As String = $"{Environment.MachineName} | {Environment.OSVersion.VersionString}"
            Using cmdLog As New MySqlCommand(
                "INSERT INTO activity_logs (UserID,FullName,UserRole,ActionType,Module,Details,ActionDate,IPAddress,DeviceInfo) " &
                "VALUES (@uid,@fn,@role,'LOGOUT','Authentication','User logged out',NOW(),@ip,@dev)", cn)
                cmdLog.Parameters.AddWithValue("@uid", userId)
                cmdLog.Parameters.AddWithValue("@fn", LoggedFullname)
                cmdLog.Parameters.AddWithValue("@role", LoggedRole)
                cmdLog.Parameters.AddWithValue("@ip", ipAddress)
                cmdLog.Parameters.AddWithValue("@dev", deviceInfo)
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

    ' === ✅ UNAUTHORIZED LOGGING ===
    Private Sub LogUnauthorizedAttempt()
        Try
            connection()
            Dim ipAddress As String = "127.0.0.1"
            Try
                Dim host As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName())
                For Each ip As IPAddress In host.AddressList
                    If ip.AddressFamily = AddressFamily.InterNetwork Then
                        ipAddress = ip.ToString()
                        Exit For
                    End If
                Next
            Catch
                ipAddress = "Unknown"
            End Try

            Dim deviceInfo As String = $"{Environment.MachineName} | {Environment.OSVersion.VersionString}"
            Using cmdLog As New MySqlCommand(
                "INSERT INTO activity_logs (UserID,FullName,UserRole,ActionType,Module,Details,ActionDate,IPAddress,DeviceInfo) " &
                "VALUES (0,'UNAUTHORIZED','SYSTEM','ACCESS_DENIED','Security','No login',NOW(),@ip,@dev)", cn)
                cmdLog.Parameters.AddWithValue("@ip", ipAddress)
                cmdLog.Parameters.AddWithValue("@dev", deviceInfo)
                cmdLog.ExecuteNonQuery()
            End Using
        Catch
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === ✅ SIDEBAR MENU BUTTONS ===
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frmChange_Password.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel2.Controls.Clear()
        Dim Logs As New frmActivityLogs With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        Panel2.Controls.Add(Logs)
        Logs.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel2.Controls.Clear()
        Dim Generate As New frmReportGeneration With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        Panel2.Controls.Add(Generate)
        Generate.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Using confirmFrm As New frmConfirmPasswordDelete()
            If confirmFrm.ShowDialog() = DialogResult.OK Then
                Application.Restart()
            End If
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmRoleManagement.Show()
    End Sub


    Private Sub picProfile_DoubleClick(sender As Object, e As EventArgs) Handles picProfile.DoubleClick
        Using ofd As New OpenFileDialog()
            ofd.Title = "Pumili ng Bagong Profile Picture"
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"
            ofd.RestoreDirectory = True

            If ofd.ShowDialog() = DialogResult.OK Then
                Try
                    ' --- I-load ang napiling litrato ---
                    Dim newImage As Image = Image.FromFile(ofd.FileName)
                    picProfile.Image = newImage

                    ' --- I-convert sa byte array para sa DB ---
                    Dim picBytes As Byte()
                    Using ms As New MemoryStream()
                        newImage.Save(ms, newImage.RawFormat)
                        picBytes = ms.ToArray()
                    End Using

                    ' --- I-update sa database ---
                    UpdateProfilePicture(picBytes)

                    MsgBox("Profile picture na-update!", MsgBoxStyle.Information)

                Catch ex As Exception
                    MsgBox("Hindi mapalitan ang litrato: " & ex.Message, MsgBoxStyle.Exclamation)
                End Try
            End If
        End Using
    End Sub

    ' === ✅ I-UPDATE ANG PICTURE SA DATABASE ===
    Private Sub UpdateProfilePicture(picBytes As Byte())
        Try
            connection()
            Dim isAdmin As Boolean = String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase)
            Dim targetTable As String = If(isAdmin, "admin", "users")
            Dim idColumn As String = If(isAdmin, "AdminID", "UserID")

            Dim sql As String = $"UPDATE {targetTable} SET Picture = @pic WHERE {idColumn} = @userId"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@pic", picBytes)
                cmd.Parameters.AddWithValue("@userId", LoggedUserID)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Throw New Exception("Database error: " & ex.Message)
        Finally
            CloseConnection()
        End Try
    End Sub
End Class