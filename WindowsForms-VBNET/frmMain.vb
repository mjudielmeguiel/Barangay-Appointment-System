Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Sockets

Public Class frmMain
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If String.IsNullOrWhiteSpace(LoggedFullname) OrElse String.IsNullOrWhiteSpace(LoggedRole) Then
            MsgBox("⚠️ SECURITY ALERT" & vbCrLf &
                   "Someone attempted to access this restricted area." & vbCrLf &
                   "Unauthorized attempt has been logged.",
                   MsgBoxStyle.Exclamation, "Security Warning")
            Try
                connection()
                Dim logSql As String = "INSERT INTO activity_logs (UserID, FullName, UserRole, ActionType, Module, Details, ActionDate) " &
                                       "VALUES (@userId, @fullName, @userRole, @actionType, @module, @details, NOW())"
                Using cmdLog As New MySqlCommand(logSql, cn)
                    cmdLog.Parameters.AddWithValue("@userId", 0)
                    cmdLog.Parameters.AddWithValue("@fullName", "UNAUTHORIZED")
                    cmdLog.Parameters.AddWithValue("@userRole", "SYSTEM")
                    cmdLog.Parameters.AddWithValue("@actionType", "ACCESS_DENIED")
                    cmdLog.Parameters.AddWithValue("@module", "Security")
                    cmdLog.Parameters.AddWithValue("@details", "Attempted access without login")
                    cmdLog.ExecuteNonQuery()
                End Using
            Catch ex As Exception
            Finally
                CloseConnection()
            End Try

            Me.Close()
            frmlogin.Show()
            Return
        End If

        Menupanel.Visible = False

        If Not String.IsNullOrEmpty(LoggedRole) Then
            lblUserRole.Text = $"{LoggedRole.ToUpper()}"
        Else
            lblUserRole.Text = "USER"
        End If

        ' ==========================================
        ' ROLE-BASED ACCESS CONTROL
        ' ==========================================
        Dim isAdmin As Boolean = String.Equals(LoggedRole, "Administrator", StringComparison.OrdinalIgnoreCase)

        ' ✅ Itago ang Button2 para sa Admin
        Button2.Visible = Not isAdmin
        Button5.Visible = True

        ' ✅ IBA-IBANG DASHBOARD AYON SA ROLE
        Panel2.Controls.Clear()
        If isAdmin Then
            ' Admin — Admin Dashboard agad
            Dim AdminDash As New frmAdmin_Dashboard With {
                .TopLevel = False,
                .FormBorderStyle = FormBorderStyle.None,
                .Dock = DockStyle.Fill
            }
            Panel2.Controls.Add(AdminDash)
            AdminDash.Show()
        Else
            ' Regular User — User Dashboard
            Dim Home As New frmUser_Dashboard With {
                .TopLevel = False,
                .FormBorderStyle = FormBorderStyle.None,
                .Dock = DockStyle.Fill
            }
            Panel2.Controls.Add(Home)
            Home.Show()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout") = MsgBoxResult.No Then
            Return
        End If

        Try
            connection()

            Dim lname As String = ""
            Dim fname As String = ""
            Dim userId As Integer = 0

            If Not String.IsNullOrEmpty(LoggedFullname) AndAlso LoggedFullname.Contains(",") Then
                Dim parts() As String = LoggedFullname.Split(","c)
                lname = parts(0).Trim()
                fname = parts(1).Trim()
            ElseIf Not String.IsNullOrEmpty(LoggedFullname) Then
                fname = LoggedFullname.Trim()
            End If

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

            If Not String.IsNullOrEmpty(targetTable) Then
                Dim selectSql As String = $"SELECT {idColumnName} FROM {targetTable} WHERE Lastname=@lname AND Firstname=@fname LIMIT 1"
                Using cmdGetId As New MySqlCommand(selectSql, cn)
                    cmdGetId.Parameters.AddWithValue("@lname", lname)
                    cmdGetId.Parameters.AddWithValue("@fname", fname)
                    Dim res = cmdGetId.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not IsDBNull(res) Then
                        userId = Convert.ToInt32(res)
                    End If
                End Using

                Dim updateSql As String = $"UPDATE {targetTable} SET AccountStatus='Offline' WHERE Lastname=@lname AND Firstname=@fname"
                Using cmdUpdate As New MySqlCommand(updateSql, cn)
                    cmdUpdate.Parameters.AddWithValue("@lname", lname)
                    cmdUpdate.Parameters.AddWithValue("@fname", fname)
                    cmdUpdate.ExecuteNonQuery()
                End Using
            End If

            Dim logSql As String = "INSERT INTO activity_logs (UserID, FullName, UserRole, ActionType, Module, Details, ActionDate, IPAddress, DeviceInfo) " &
                                   "VALUES (@userId, @fullName, @userRole, @actionType, @module, @details, NOW(), @ipAddress, @deviceInfo)"

            Using cmdLog As New MySqlCommand(logSql, cn)
                cmdLog.Parameters.AddWithValue("@userId", If(userId > 0, userId, 1))
                cmdLog.Parameters.AddWithValue("@fullName", If(String.IsNullOrEmpty(LoggedFullname), "Unknown", LoggedFullname))
                cmdLog.Parameters.AddWithValue("@userRole", If(String.IsNullOrEmpty(LoggedRole), "User", LoggedRole))
                cmdLog.Parameters.AddWithValue("@actionType", "LOGOUT")
                cmdLog.Parameters.AddWithValue("@module", "Authentication")
                cmdLog.Parameters.AddWithValue("@details", "User logged out successfully.")

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
                cmdLog.Parameters.AddWithValue("@ipAddress", ipAddress)

                Dim deviceInfo As String = $"{Environment.MachineName} ({Environment.OSVersion.VersionString})"
                cmdLog.Parameters.AddWithValue("@deviceInfo", deviceInfo)

                cmdLog.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MsgBox("Error during logout process: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try

        LoggedFullname = ""
        LoggedRole = ""
        Me.Hide()
        Application.Restart()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Menupanel.Visible = Not Menupanel.Visible
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel2.Controls.Clear()
        Dim Home As New frmUser_Dashboard With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        Panel2.Controls.Add(Home)
        Home.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Panel2.Controls.Clear()
        Dim Records As New frmResidence_Records With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        Panel2.Controls.Add(Records)
        Records.Show()
    End Sub

    Private Sub btnAppointment_Click(sender As Object, e As EventArgs) Handles btnAppointment.Click
        Panel2.Controls.Clear()
        Dim Calendar As New frmBarangayCalendar With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        Panel2.Controls.Add(Calendar)
        Calendar.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Panel2.Controls.Clear()
        Dim History As New frmAppointmentHistory With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        Panel2.Controls.Add(History)
        History.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Panel2.Controls.Clear()
        Dim Settings As New frmSettings With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        Panel2.Controls.Add(Settings)
        Settings.Show()
    End Sub
End Class