Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Sockets

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
        Dim frm As New frmChange_Password()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill

        Panel2.Controls.Clear()
        Panel2.Controls.Add(frm)
        frm.Show()
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

    Private Sub btnRecoverAccount_Click(sender As Object, e As EventArgs) Handles btnRecoverAccount.Click
        Dim Recovery As New frmAccountRecovery()
        Recovery.TopLevel = False
        Recovery.FormBorderStyle = FormBorderStyle.None
        Recovery.Dock = DockStyle.Fill

        Panel2.Controls.Clear()
        Panel2.Controls.Add(Recovery)
        Recovery.Show()
    End Sub
End Class