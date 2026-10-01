Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Drawing
Imports System.Drawing.Drawing2D
Public Class frmMain
    ' === PROPERTIES ===
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
    ' === PANEL DIMENSIONS PARA SA TOGGLE LOGIC LANG ===
    Private Const ICON_RAIL_WIDTH As Integer = 70
    Private Const MENU_PANEL_WIDTH As Integer = 200

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' --- SECURITY CHECK ---
        If String.IsNullOrWhiteSpace(frmlogin.LoggedInUsername) OrElse
       String.IsNullOrWhiteSpace(frmlogin.LoggedInFullname) Then
            LogUnauthorizedAttempt()
            Me.Close()
            frmlogin.Show()
            Return
        End If
        ' --- GAWING BILOG ANG PICTURE BOX ---
        MakeCircularPictureBox(picProfile)
        ' --- LOAD PROFILE INFO ---
        LoadUserProfileInfo()
        ' --- ROLE-BASED VISIBILITY ---
        Dim isAdmin As Boolean = String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase)

        ' ✅ Admin lang makakakita sa mga ito
        btnUsers.Visible = isAdmin
        Button1.Visible = isAdmin
        Button2.Visible = isAdmin
        btnRecoverAccount.Visible = isAdmin   ' ← DAGDAG ITO

        ' --- LOAD DEFAULT DASHBOARD ---
        Panel2.Controls.Clear()
        Dim defaultForm As Form = If(isAdmin,
        New frmAdmin_Dashboard With {.TopLevel = False, .FormBorderStyle = FormBorderStyle.None, .Dock = DockStyle.Fill},
        New frmUser_Dashboard With {.TopLevel = False, .FormBorderStyle = FormBorderStyle.None, .Dock = DockStyle.Fill})
        Panel2.Controls.Add(defaultForm)
        defaultForm.Show()
    End Sub

    ' === KAPAG NAG-RESIZE ANG FORM ===
    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If panelIcons IsNot Nothing Then
            panelIcons.Height = Me.ClientSize.Height
        End If
        If panelMenu IsNot Nothing Then
            panelMenu.Height = Me.ClientSize.Height
        End If
        If Panel2 IsNot Nothing Then
            Panel2.Location = New Point(panelIcons.Width + If(panelMenu.Visible, panelMenu.Width, 0), Panel2.Top)
            Panel2.Width = Me.ClientSize.Width - Panel2.Left
            Panel2.Height = Me.ClientSize.Height - Panel2.Top
        End If
    End Sub
    ' === GAWING BILOG ANG PICTUREBOX ===
    Private Sub MakeCircularPictureBox(pb As PictureBox)
        If pb Is Nothing Then Return
        Dim size As Integer = Math.Min(pb.Width, pb.Height)
        pb.Width = size
        pb.Height = size
        Dim gp As New GraphicsPath()
        gp.AddEllipse(0, 0, pb.Width, pb.Height)
        pb.Region = New Region(gp)
        pb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub
    ' === LOAD PROFILE INFO ===
    Private Sub LoadUserProfileInfo()
        Try
            connection()
            Dim isAdmin As Boolean = String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase)
            Dim targetTable As String = If(isAdmin, "admin", "users")
            Dim idColumn As String = If(isAdmin, "AdminID", "UserID")
            Dim sql As String = $"SELECT Firstname, Lastname, Role, Picture FROM {targetTable} WHERE {idColumn} = @userId"
            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@userId", LoggedUserID)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        Dim fName As String = dr("Firstname").ToString().Trim()
                        Dim lName As String = dr("Lastname").ToString().Trim()
                        Dim displayName As String = $"{fName} {lName}".Trim()
                        lblFullname.Text = If(Not String.IsNullOrWhiteSpace(displayName), displayName, LoggedFullname)
                        Dim roleValue As String = If(Not IsDBNull(dr("Role")), dr("Role").ToString().Trim(), "")
                        If String.IsNullOrWhiteSpace(roleValue) Then roleValue = If(isAdmin, "Administrator", LoggedRole)
                        lblUserRole.Text = roleValue.ToUpper()
                        If Not IsDBNull(dr("Picture")) Then
                            Using ms As New MemoryStream(CType(dr("Picture"), Byte()))
                                picProfile.Image = Image.FromStream(ms)
                            End Using
                        Else
                            picProfile.Image = Nothing
                        End If
                    Else
                        lblFullname.Text = LoggedFullname
                        lblUserRole.Text = If(isAdmin, "ADMINISTRATOR", LoggedRole.ToUpper())
                        picProfile.Image = Nothing
                    End If
                End Using
            End Using
        Catch ex As Exception
            lblFullname.Text = LoggedFullname
            lblUserRole.Text = If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
                "ADMINISTRATOR", LoggedRole.ToUpper())
            picProfile.Image = Nothing
        Finally
            CloseConnection()
        End Try
    End Sub
    ' === TOGGLE SIDEBAR ===

    ' === MENU NAVIGATION ===
    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        LoadFormIntoPanel(If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
            GetType(frmAdmin_Dashboard), GetType(frmUser_Dashboard)))
    End Sub
    Private Sub btnResidents_Click(sender As Object, e As EventArgs) Handles btnResidents.Click
        LoadFormIntoPanel(GetType(frmResidence_Records))
    End Sub
    Private Sub btnUsers_Click(sender As Object, e As EventArgs) Handles btnUsers.Click
        LoadFormIntoPanel(GetType(frmManage_Users))
    End Sub
    Private Sub btnPayments_Click(sender As Object, e As EventArgs) Handles btnPayments.Click
        Dim Payments As New frmPayments With {
            .TopLevel = False, .FormBorderStyle = FormBorderStyle.None, .Dock = DockStyle.Fill, .PreviousForm = Me}
        Panel2.Controls.Clear()
        Panel2.Controls.Add(Payments)
        Payments.Show()
    End Sub
    Private Sub btnCalendar_Click(sender As Object, e As EventArgs) Handles btnCalendar.Click
        LoadFormIntoPanel(GetType(frmBarangayCalendar))
    End Sub

    Private Sub LoadFormIntoPanel(formType As Type)
        Panel2.Controls.Clear()
        Dim frm As Form = CType(Activator.CreateInstance(formType), Form)
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        Panel2.Controls.Add(frm)
        frm.Show()
    End Sub
    ' === LOGOUT ===
    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure you want to logout?",
                  MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout") = MsgBoxResult.No Then Return
        Try
            connection()
            Dim userId As Integer = LoggedUserID
            Dim targetTable As String = "users"
            Dim idColumnName As String = "UserID"
            If String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase) Then
                targetTable = "admin" : idColumnName = "AdminID"
            ElseIf String.Equals(LoggedRole, "Residence", StringComparison.OrdinalIgnoreCase) OrElse
                   String.Equals(LoggedRole, "Resident", StringComparison.OrdinalIgnoreCase) Then
                targetTable = "residences" : idColumnName = "ResidentID"
            End If
            If userId > 0 Then
                Using cmdUpdate As New MySqlCommand($"UPDATE {targetTable} SET AccountStatus='Offline' WHERE {idColumnName}=@userId", cn)
                    cmdUpdate.Parameters.AddWithValue("@userId", userId)
                    cmdUpdate.ExecuteNonQuery()
                End Using
            End If
            Dim ipAddress As String = GetLocalIPAddress()
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
    ' === UNAUTHORIZED LOGGING ===
    Private Sub LogUnauthorizedAttempt()
        Try
            connection()
            Dim ipAddress As String = GetLocalIPAddress()
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
    ' === HELPER: GET LOCAL IP ===
    Private Function GetLocalIPAddress() As String
        Try
            Dim host As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName())
            For Each ip As IPAddress In host.AddressList
                If ip.AddressFamily = AddressFamily.InterNetwork Then Return ip.ToString()
            Next
        Catch
        End Try
        Return "127.0.0.1"
    End Function
    ' === DOUBLE-CLICK PROFILE ===
    Private Sub picProfile_DoubleClick(sender As Object, e As EventArgs) Handles picProfile.DoubleClick
        Using ofd As New OpenFileDialog()
            ofd.Title = "Select New Profile Picture"
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"
            ofd.RestoreDirectory = True
            If ofd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim newImage As Image = Image.FromFile(ofd.FileName)
                    picProfile.Image = newImage
                    Dim picBytes As Byte()
                    Using ms As New MemoryStream()
                        newImage.Save(ms, newImage.RawFormat)
                        picBytes = ms.ToArray()
                    End Using
                    UpdateProfilePicture(picBytes)
                    MsgBox("Profile picture updated!", MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox("Failed to update picture: " & ex.Message, MsgBoxStyle.Exclamation)
                End Try
            End If
        End Using
    End Sub
    ' === UPDATE PROFILE PICTURE SA DB ===
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
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        LoadFormIntoPanel(GetType(frmAppointmentHistory))
    End Sub
    Private Sub BtnReports_Click(sender As Object, e As EventArgs) Handles BtnReports.Click
        LoadFormIntoPanel(If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
            GetType(frmReportGeneration), GetType(frmReportGeneration)))
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadFormIntoPanel(If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
            GetType(frmcreateuser), GetType(frmcreateuser)))
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadFormIntoPanel(If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
            GetType(frmDocumentServices), GetType(frmDocumentServices)))
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LoadFormIntoPanel(If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
            GetType(frmActivityLogs), GetType(frmActivityLogs)))
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        frmChange_Password.Show()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        LoadFormIntoPanel(If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
            GetType(Barangay_Residences), GetType(Barangay_Residences)))
    End Sub

    ' === HELPER: LOAD FORM SA PANEL ===

    Private Sub btnToggleSidebar_Click(sender As Object, e As EventArgs) Handles btnToggleSidebar.Click
        If panelMenu.Visible Then
            panelMenu.Visible = False
            Panel2.Location = New Point(panelIcons.Width, Panel2.Top)
            Panel2.Width = Me.ClientSize.Width - panelIcons.Width
        Else
            panelMenu.Visible = True
            Panel2.Location = New Point(panelIcons.Width + panelMenu.Width, Panel2.Top)
            Panel2.Width = Me.ClientSize.Width - (panelIcons.Width + panelMenu.Width)
        End If
    End Sub

    'Create a new Sattelite Office Form
    Private Sub btnCreateSatelliteOffice_Click(sender As Object, e As EventArgs) Handles btnCreateSatelliteOffice.Click
        LoadFormIntoPanel(If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
    GetType(frmCreateNewSateliteOffice), GetType(frmCreateNewSateliteOffice)))
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadFormIntoPanel(If(String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase),
GetType(frmSatelliteOfficeList), GetType(frmSatelliteOfficeList)))
    End Sub
End Class