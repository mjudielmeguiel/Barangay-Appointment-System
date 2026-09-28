Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.Net.Sockets

Public Class frmlogin
    Private lockoutSecondsRemaining As Integer = 60
    Private isLoginSuccess As Boolean = False
    ' === ✅ NAKALOGIN NA IMPORMASYON — IBABAHAGI SA LAHAT NG FORM ===
    Public Shared Property LoggedInUsername As String = ""
    Public Shared Property LoggedInFullname As String = ""
    Public Shared Property LoggedInRole As String = ""
    Public Shared Property LoggedInUserID As Integer = 0

    ' === ✅ FLAG — MAY ADMIN BA? ===
    Private hasAdminAccount As Boolean = False

    Private Sub frmlogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForSecurityAttempts()
        ' ✅ TIGNAN KUNG MAY ADMIN — PALITAN ANG BUTTON
        CheckAdminExists()
        UpdateCreateButton()

        txtUsername.Text = "Please Enter your username"
        txtUsername.ForeColor = Color.DarkGray
        txtPassword.Text = "Please Enter your Password"
        txtPassword.ForeColor = Color.DarkGray
        txtPassword.PasswordChar = Nothing
        lblError.Text = ""
        lblAttempts.Text = "0"
        Timer1.Interval = 1000
    End Sub

    ' === ✅ TIGNAN KUNG MAY ADMIN SA DATABASE ===
    Private Sub CheckAdminExists()
        Try
            connection()
            Dim checkSql As String = "SELECT COUNT(*) FROM admin"
            Using cmd As New MySqlCommand(checkSql, cn)
                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                Dim adminCount As Integer = CInt(cmd.ExecuteScalar())
                hasAdminAccount = (adminCount > 0)
            End Using
        Catch ex As Exception
            ' Kung walang admin table pa — ituring na wala pang admin
            hasAdminAccount = False
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === ✅ PALITAN ANG BUTTON TEXT AYON SA SITWASYON ===
    Private Sub UpdateCreateButton()
        If hasAdminAccount Then
            btnCreate.Text = "Forgot Password"
        Else
            btnCreate.Text = "Create ADMIN"
        End If
    End Sub

    Private Sub CheckForSecurityAttempts()
        Try
            connection()
            Dim sqlCheck As String = "SELECT COUNT(*) FROM activity_logs 
                                       WHERE ActionType = 'ACCESS_DENIED' 
                                       AND ActionDate >= NOW() - INTERVAL 1 HOUR"
            Using cmdCheck As New MySqlCommand(sqlCheck, cn)
                Dim deniedCount As Integer = CInt(cmdCheck.ExecuteScalar())
                If deniedCount > 0 Then
                    MsgBox("⚠️ SECURITY NOTICE" & vbCrLf &
                           "Someone attempted to access restricted area(s) without login." & vbCrLf &
                           deniedCount & " attempt(s) have been recorded.",
                           MsgBoxStyle.Exclamation, "System Alert")
                End If
            End Using
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub txtUsername_GotFocus(sender As Object, e As EventArgs) Handles txtUsername.GotFocus
        If Timer1.Enabled AndAlso Not isLoginSuccess Then Return
        lblError.Text = ""
        If txtUsername.Text = "Please Enter your username" Then
            txtUsername.Clear()
            txtUsername.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtUsername_LostFocus(sender As Object, e As EventArgs) Handles txtUsername.LostFocus
        If String.IsNullOrWhiteSpace(txtUsername.Text) Then
            txtUsername.Text = "Please Enter your username"
            txtUsername.ForeColor = Color.DarkGray
        End If
    End Sub

    Private Sub txtPassword_GotFocus(sender As Object, e As EventArgs) Handles txtPassword.GotFocus
        If Timer1.Enabled AndAlso Not isLoginSuccess Then Return
        lblError.Text = ""
        If txtPassword.Text = "Please Enter your Password" Then
            txtPassword.Clear()
            txtPassword.ForeColor = Color.Black
            txtPassword.PasswordChar = "●"c
        End If
    End Sub

    Private Sub txtPassword_LostFocus(sender As Object, e As EventArgs) Handles txtPassword.LostFocus
        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            txtPassword.Text = "Please Enter your Password"
            txtPassword.ForeColor = Color.DarkGray
            txtPassword.PasswordChar = Nothing
        End If
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        If Timer1.Enabled AndAlso Not isLoginSuccess Then
            lblError.Text = $"🔒 Locked. Try again in {lockoutSecondsRemaining}s."
            Return
        End If
        lblError.Text = ""
        Dim userInput As String = txtUsername.Text.Trim()
        Dim passInput As String = txtPassword.Text.Trim()
        If userInput = "" OrElse userInput = "Please Enter your username" OrElse
           passInput = "" OrElse passInput = "Please Enter your Password" Then
            RecordActivityLog(0, userInput, "Anonymous", "EMPTY_ATTEMPT", "Authentication",
                             $"Empty attempt — Username: [{userInput}]")
            lblError.Text = "⚠️ Enter username and password."
            Return
        End If
        ProcessLogin(userInput, passInput)
    End Sub

    Private Sub ProcessLogin(userInput As String, passInput As String)
        Dim foundUserType As String = ""
        Dim recordId As Integer = 0
        Dim attempts As Integer = 0
        Dim lockoutExpiry As DateTime = DateTime.MinValue
        Dim dbPass As String = ""
        Dim fullName As String = ""
        Dim roleName As String = ""
        Try
            connection()
            Dim isSuspicious As Boolean = False
            Dim note As String = ""
            If userInput.Contains("'") OrElse userInput.Contains(" OR ") OrElse
               userInput.Contains("--") OrElse userInput.Contains(" UNION ") OrElse
               userInput.Contains(" DROP ") OrElse userInput.Contains(" SLEEP(") Then
                isSuspicious = True
                note = "⚠️ SUSPICIOUS INPUT — Possible SQL Injection. "
            End If

            ' === HANAPIN SA ADMIN TABLE ===
            Dim adminSql As String = "SELECT AdminID, Firstname, Lastname, Password, LoginAttempts, LockoutExpiry FROM admin WHERE Username=@user"
            Using cmdAdmin As New MySqlCommand(adminSql, cn)
                cmdAdmin.Parameters.AddWithValue("@user", userInput)
                Using drAdmin As MySqlDataReader = cmdAdmin.ExecuteReader()
                    If drAdmin.Read() Then
                        foundUserType = "admin"
                        recordId = If(IsDBNull(drAdmin("AdminID")), 0, Convert.ToInt32(drAdmin("AdminID")))
                        attempts = If(IsDBNull(drAdmin("LoginAttempts")), 0, Convert.ToInt32(drAdmin("LoginAttempts")))
                        If Not IsDBNull(drAdmin("LockoutExpiry")) Then
                            lockoutExpiry = Convert.ToDateTime(drAdmin("LockoutExpiry"))
                        End If
                        dbPass = drAdmin("Password").ToString()
                        Dim fNameAdmin As String = If(IsDBNull(drAdmin("Firstname")), "", drAdmin("Firstname").ToString().Trim())
                        Dim lNameAdmin As String = If(IsDBNull(drAdmin("Lastname")), "", drAdmin("Lastname").ToString().Trim())
                        fullName = If(String.IsNullOrEmpty(lNameAdmin), fNameAdmin, $"{lNameAdmin}, {fNameAdmin}")
                        roleName = "Administrator"
                    End If
                End Using
            End Using

            ' === HANAPIN SA USERS TABLE ===
            If String.IsNullOrEmpty(foundUserType) Then
                Dim userSql As String = "SELECT UserID, Firstname, Lastname, Role, Password, LoginAttempts, LockoutExpiry FROM users WHERE Username=@user"
                Using cmdUser As New MySqlCommand(userSql, cn)
                    cmdUser.Parameters.AddWithValue("@user", userInput)
                    Using drUser As MySqlDataReader = cmdUser.ExecuteReader()
                        If drUser.Read() Then
                            foundUserType = "users"
                            recordId = If(IsDBNull(drUser("UserID")), 0, Convert.ToInt32(drUser("UserID")))
                            attempts = If(IsDBNull(drUser("LoginAttempts")), 0, Convert.ToInt32(drUser("LoginAttempts")))
                            If Not IsDBNull(drUser("LockoutExpiry")) Then
                                lockoutExpiry = Convert.ToDateTime(drUser("LockoutExpiry"))
                            End If
                            dbPass = drUser("Password").ToString()
                            Dim fName As String = If(IsDBNull(drUser("Firstname")), "", drUser("Firstname").ToString().Trim())
                            Dim lName As String = If(IsDBNull(drUser("Lastname")), "", drUser("Lastname").ToString().Trim())
                            fullName = If(String.IsNullOrEmpty(lName), fName, $"{lName}, {fName}")
                            Dim uRole As String = drUser("Role").ToString().Trim()
                            roleName = If(String.IsNullOrEmpty(uRole), "Staff", uRole)
                        End If
                    End Using
                End Using
            End If

            ' === HANAPIN SA RESIDENCES TABLE ===
            If String.IsNullOrEmpty(foundUserType) Then
                Dim resSql As String = "SELECT ResidentID, Firstname, Lastname, Password, LoginAttempts, LockoutExpiry FROM residences WHERE Username=@user"
                Using cmdRes As New MySqlCommand(resSql, cn)
                    cmdRes.Parameters.AddWithValue("@user", userInput)
                    Using drRes As MySqlDataReader = cmdRes.ExecuteReader()
                        If drRes.Read() Then
                            foundUserType = "residences"
                            recordId = If(IsDBNull(drRes("ResidentID")), 0, Convert.ToInt32(drRes("ResidentID")))
                            attempts = If(IsDBNull(drRes("LoginAttempts")), 0, Convert.ToInt32(drRes("LoginAttempts")))
                            If Not IsDBNull(drRes("LockoutExpiry")) Then
                                lockoutExpiry = Convert.ToDateTime(drRes("LockoutExpiry"))
                            End If
                            dbPass = drRes("Password").ToString()
                            Dim fName As String = If(IsDBNull(drRes("Firstname")), "", drRes("Firstname").ToString().Trim())
                            Dim lName As String = If(IsDBNull(drRes("Lastname")), "", drRes("Lastname").ToString().Trim())
                            fullName = If(String.IsNullOrEmpty(lName), fName, $"{lName}, {fName}")
                            roleName = "Residence"
                        End If
                    End Using
                End Using
            End If

            ' === WALANG NAKITANG USER ===
            If String.IsNullOrEmpty(foundUserType) Then
                RecordActivityLog(0, userInput, "Unknown", "FAILED_LOGIN", "Authentication",
                                 note & $"Username not found — Input: [{userInput}]")
                lblError.Text = "⚠️ User does not exist."
                Return
            End If

            ' === NAKALOCKOUT PA ===
            If lockoutExpiry > DateTime.Now Then
                lockoutSecondsRemaining = CInt((lockoutExpiry - DateTime.Now).TotalSeconds)
                lblError.Text = $"🔒 Locked. Try again in {lockoutSecondsRemaining}s."
                Timer1.Start()
                RecordActivityLog(recordId, userInput, roleName, "FAILED_LOGIN", "Authentication",
                                 note & $"Attempt during lockout period — Input: [{userInput}]")
                Return
            End If

            ' === ✅ TAMA ANG PASSWORD ===
            If dbPass = passInput Then
                LoggedInUsername = userInput
                LoggedInFullname = If(String.IsNullOrWhiteSpace(fullName), userInput, fullName)
                LoggedInRole = roleName
                LoggedInUserID = recordId
                ResetAttempts(foundUserType, userInput)
                RecordActivityLog(recordId, LoggedInFullname, LoggedInRole, "LOGIN", "Authentication",
                                 $"{roleName} logged in successfully — Username used: [{userInput}]")
                isLoginSuccess = True
                lblError.Text = "✅ Login Success! Redirecting..."
                Timer1.Interval = 800
                Timer1.Start()
            Else
                ' === MALING PASSWORD ===
                attempts += 1
                lblAttempts.Text = attempts.ToString()
                UpdateAttemptCount(foundUserType, userInput, attempts)
                RecordActivityLog(recordId, userInput, roleName, "FAILED_LOGIN", "Authentication",
                                 note & $"Wrong password — Attempt {attempts} of 3 — Input: [{userInput}]")
                If attempts >= 3 Then
                    LockoutWithTimer(foundUserType, userInput, 60)
                    lockoutSecondsRemaining = 60
                    lblError.Text = "🔒 3 failed attempts. Locked for 60 seconds."
                    Timer1.Start()
                Else
                    lblError.Text = $"⚠️ Incorrect Password. Attempt {attempts} of 3."
                End If
            End If
        Catch ex As Exception
            MsgBox("Login Error: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub ResetAttempts(tableName As String, user As String)
        Dim updateSql As String = $"UPDATE {tableName} SET LoginAttempts=0, LockoutExpiry=NULL WHERE Username=@user"
        Using cmd As New MySqlCommand(updateSql, cn)
            cmd.Parameters.AddWithValue("@user", user)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub UpdateAttemptCount(tableName As String, user As String, attempts As Integer)
        Dim updateSql As String = $"UPDATE {tableName} SET LoginAttempts=@attempts WHERE Username=@user"
        Using cmd As New MySqlCommand(updateSql, cn)
            cmd.Parameters.AddWithValue("@attempts", attempts)
            cmd.Parameters.AddWithValue("@user", user)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub LockoutWithTimer(tableName As String, user As String, seconds As Integer)
        Dim expiryTime As DateTime = DateTime.Now.AddSeconds(seconds)
        Dim updateSql As String = $"UPDATE {tableName} SET LoginAttempts=@attempts, LockoutExpiry=@expiry WHERE Username=@user"
        Using cmd As New MySqlCommand(updateSql, cn)
            cmd.Parameters.AddWithValue("@attempts", 3)
            cmd.Parameters.AddWithValue("@expiry", expiryTime)
            cmd.Parameters.AddWithValue("@user", user)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If isLoginSuccess Then
            Timer1.Stop()
            Me.Hide()
            frmMain.Show()
            Return
        End If
        lockoutSecondsRemaining -= 1
        If lockoutSecondsRemaining > 0 Then
            lblError.Text = $"Locked. Try again in {lockoutSecondsRemaining}s."
        Else
            Timer1.Stop()
            lblError.Text = "You may try logging in now."
            lblAttempts.Text = "0"
        End If
    End Sub

    Private Sub RecordActivityLog(userId As Integer, fullName As String, userRole As String, actionType As String, moduleName As String, details As String)
        Try
            connection()
            Dim ipAddress As String = "127.0.0.1"
            Try
                Dim host As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
                For Each ip As System.Net.IPAddress In host.AddressList
                    If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                        ipAddress = ip.ToString()
                        Exit For
                    End If
                Next
            Catch
                ipAddress = "Unknown"
            End Try
            Dim deviceInfo As String = $"{Environment.MachineName} | {Environment.OSVersion.VersionString}"
            Dim logSql As String = "INSERT INTO activity_logs " &
                "(UserID, FullName, UserRole, ActionType, Module, Details, ActionDate, IPAddress, DeviceInfo) " &
                "VALUES (@userId, @fullName, @userRole, @actionType, @module, @details, NOW(), @ipAddress, @deviceInfo)"
            Using cmdLog As New MySqlCommand(logSql, cn)
                cmdLog.Parameters.AddWithValue("@userId", If(userId > 0, userId, 0))
                cmdLog.Parameters.AddWithValue("@fullName", If(String.IsNullOrEmpty(fullName), "Unknown", fullName))
                cmdLog.Parameters.AddWithValue("@userRole", If(String.IsNullOrEmpty(userRole), "Anonymous", userRole))
                cmdLog.Parameters.AddWithValue("@actionType", actionType)
                cmdLog.Parameters.AddWithValue("@module", moduleName)
                cmdLog.Parameters.AddWithValue("@details", details)
                cmdLog.Parameters.AddWithValue("@ipAddress", ipAddress)
                cmdLog.Parameters.AddWithValue("@deviceInfo", deviceInfo)
                cmdLog.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MsgBox("Log Save Error: " & ex.Message, MsgBoxStyle.Exclamation)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub btnShowPass_Click(sender As Object, e As EventArgs) Handles btnShowPass.Click
        txtPassword.PasswordChar = If(txtPassword.PasswordChar = "●"c, Char.MinValue, "●"c)
    End Sub

    ' ❌ TINANGGAL NA ANG LINKLABEL — ISANG BUTTON NA LANG
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    ' === ✅ ANG ISANG BUTTON — DITO NAGPAPALIT NG FUNCTION ===
    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        If Not hasAdminAccount Then
            ' Walang admin → Pumunta sa Create Admin
            Me.Hide()
            frmcreateadmin.Show()
        Else
            ' May admin na → Pumunta sa Forgot Password
            Me.Hide()
            frmforgotpassword.Show()
        End If
    End Sub
End Class