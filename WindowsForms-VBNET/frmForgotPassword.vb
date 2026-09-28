Imports System.Text
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class frmforgotpassword
    Private currentUserId As Integer? = Nothing
    Private currentUserType As String = ""
    Private currentTicketNumber As String = ""
    Private isTicketVerified As Boolean = False

    Private Sub frmforgotpassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ResetToInitialState()
        txtUserInput.Focus()
    End Sub

    Private Sub txtUserInput_TextChanged(sender As Object, e As EventArgs) Handles txtUserInput.TextChanged
        Dim input As String = txtUserInput.Text.Trim()

        If String.IsNullOrWhiteSpace(input) Then
            ResetToInitialState()
            Return
        End If

        SearchForUser(input)
    End Sub

    Private Sub SearchForUser(input As String)
        Try
            connection()

            If SearchInTable("admin", "AdminID", input) Then Return
            If SearchInTable("users", "UserID", input) Then Return
            If SearchInTable("residences", "ResidentID", input) Then Return

            ShowUserNotFound()

        Catch ex As Exception
            MessageBox.Show(
                "Search Error: " & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Function SearchInTable(
        tableName As String,
        idColumn As String,
        input As String
    ) As Boolean
        Dim sql As String = ""
        Dim defaultRole As String = ""

        Select Case tableName
            Case "admin"
                sql = $"SELECT {idColumn}, Firstname, Lastname, Username, Email, AccountStatus 
                       FROM admin 
                       WHERE Username = @input OR Email = @input 
                       LIMIT 1"
                defaultRole = "Administrator"

            Case "users"
                sql = $"SELECT {idColumn}, Firstname, Lastname, Username, Email, Role, AccountStatus 
                       FROM users 
                       WHERE Username = @input OR Email = @input 
                       LIMIT 1"
                defaultRole = "Staff"

            Case "residences"
                sql = $"SELECT {idColumn}, Firstname, Lastname, Username 
                       FROM residences 
                       WHERE Username = @input 
                       LIMIT 1"
                defaultRole = "Residence"
        End Select

        Using cmd As New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@input", input)
            If cn.State = ConnectionState.Open Then cn.Close()
            cn.Open()

            Using dr As MySqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then
                    currentUserType = tableName
                    currentUserId = Convert.ToInt32(dr(idColumn))

                    Dim fullName As String = $"{SafeStr(dr("Firstname"))} {SafeStr(dr("Lastname"))}".Trim()
                    Dim username As String = SafeStr(dr("Username"))

                    Dim email As String = "Not provided"
                    Dim accStatus As String = "Active"
                    Dim role As String = defaultRole

                    If HasColumn(dr, "Email") Then email = If(SafeStr(dr("Email")) = "", "Not provided", SafeStr(dr("Email")))
                    If HasColumn(dr, "AccountStatus") Then accStatus = If(SafeStr(dr("AccountStatus")) = "", "Active", SafeStr(dr("AccountStatus")))
                    If HasColumn(dr, "Role") Then role = If(SafeStr(dr("Role")) = "", defaultRole, SafeStr(dr("Role")))

                    DisplayUserInfo(fullName, username, email, role, accStatus)
                    Return True
                End If
            End Using
        End Using

        Return False
    End Function

    Private Function SafeStr(obj As Object) As String
        If obj Is Nothing OrElse IsDBNull(obj) Then Return ""
        Return obj.ToString().Trim()
    End Function

    Private Function HasColumn(dr As MySqlDataReader, columnName As String) As Boolean
        Try
            Return dr.GetOrdinal(columnName) >= 0
        Catch
            Return False
        End Try
    End Function

    Private Sub DisplayUserInfo(
        fullName As String,
        username As String,
        email As String,
        role As String,
        status As String
    )
        lblName.Text = $"Name:    {fullName}"
        lblUsername.Text = $"Username: {username}"
        lblEmail.Text = $"Email:    {email}"
        lblDepartment.Text = $"Role:     {role}"
        lblStatus.Text = $"Status:   {status}"

        lblInfoStatus.Text = "✅ User Found"
        lblInfoStatus.ForeColor = Color.Green

        btnSingle.Text = "Submit Ticket"
        btnSingle.Enabled = True
        isTicketVerified = False
    End Sub

    Private Sub ShowUserNotFound()
        ResetDisplayLabels()
        lblInfoStatus.Text = "❌ No user found"
        lblInfoStatus.ForeColor = Color.Red
        btnSingle.Enabled = False
        currentUserId = Nothing
        currentUserType = ""
        isTicketVerified = False
    End Sub

    Private Sub btnSingle_Click(sender As Object, e As EventArgs) Handles btnSingle.Click
        If Not isTicketVerified Then
            SubmitTicket()
        Else
            ResetUserPassword()
        End If
    End Sub

    Private Sub SubmitTicket()
        If Not currentUserId.HasValue Then
            MessageBox.Show(
                "Search for a user first.",
                "Notice",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
            Return
        End If

        Dim ticketNumber As String = GenerateTicketNumber()
        Dim fullName As String = lblName.Text.Replace("Name:", "").Trim()
        Dim username As String = lblUsername.Text.Replace("Username:", "").Trim()
        Dim email As String = lblEmail.Text.Replace("Email:", "").Trim()
        If email = "Not provided" Then email = Nothing

        Try
            connection()

            Dim insertSql As String = "
                INSERT INTO forgot_password_tickets
                    (ticket_number, username, full_name, email, expires_at, status)
                VALUES
                    (@ticketNum, @uname, @fullname, @email, @expires, 'PENDING')
            "

            Using cmd As New MySqlCommand(insertSql, cn)
                cmd.Parameters.AddWithValue("@ticketNum", ticketNumber)
                cmd.Parameters.AddWithValue("@uname", username)
                cmd.Parameters.AddWithValue("@fullname", fullName)
                cmd.Parameters.AddWithValue("@email", If(String.IsNullOrWhiteSpace(email), DBNull.Value, email))
                cmd.Parameters.AddWithValue("@expires", DateTime.Now.AddHours(24))

                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

            currentTicketNumber = ticketNumber
            lblTicketNumber.Text = $"Ticket: {ticketNumber}"
            lblTicketNumber.ForeColor = Color.Blue

            If VerifyTicket(ticketNumber, username) Then
                isTicketVerified = True
                btnSingle.Text = "Reset Password"
                lblVerifyStatus.Text = "✅ Verified — Enter new password below"
                lblVerifyStatus.ForeColor = Color.Green

                MessageBox.Show(
                    $"✅ Ticket Created!{Environment.NewLine}{Environment.NewLine}" &
                    $"Ticket No.: {ticketNumber}{Environment.NewLine}" &
                    $"Valid for 24 hours{Environment.NewLine}{Environment.NewLine}" &
                    "Enter your new password and click Reset Password.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )
            End If

        Catch ex As Exception
            MessageBox.Show(
                "Ticket Error: " & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Function VerifyTicket(ticketNum As String, username As String) As Boolean
        Try
            connection()

            Dim sql As String = "
                SELECT status, expires_at
                FROM forgot_password_tickets
                WHERE ticket_number = @ticketNum AND username = @uname
                LIMIT 1
            "

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ticketNum", ticketNum)
                cmd.Parameters.AddWithValue("@uname", username)

                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()

                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        Dim status As String = SafeStr(dr("status"))
                        Dim expires As DateTime = Convert.ToDateTime(dr("expires_at"))

                        If expires < DateTime.Now Then
                            lblVerifyStatus.Text = "❌ Ticket Expired"
                            lblVerifyStatus.ForeColor = Color.Red
                            Return False
                        End If

                        If status <> "PENDING" Then
                            lblVerifyStatus.Text = "❌ Already Used"
                            lblVerifyStatus.ForeColor = Color.Red
                            Return False
                        End If

                        UpdateTicketStatus(ticketNum, "VERIFIED")
                        Return True
                    End If
                End Using
            End Using
        Catch ex As Exception
        End Try

        Return False
    End Function

    Private Sub ResetUserPassword()
        Dim newPassword As String = txtNewPassword.Text.Trim()

        If String.IsNullOrWhiteSpace(newPassword) OrElse newPassword.Length < 8 Then
            MessageBox.Show(
                "⚠️ Password must be at least 8 characters.",
                "Invalid",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            Return
        End If

        If Not Regex.IsMatch(newPassword, "(?=.*[a-z])(?=.*[A-Z])") Then
            MessageBox.Show(
                "⚠️ Password must contain both uppercase and lowercase letters.",
                "Invalid",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            Return
        End If

        Try
            connection()

            Dim updateSql As String = ""
            Select Case currentUserType
                Case "admin" : updateSql = "UPDATE admin SET Password = @newPass WHERE AdminID = @id"
                Case "users" : updateSql = "UPDATE users SET Password = @newPass WHERE UserID = @id"
                Case "residences" : updateSql = "UPDATE residences SET Password = @newPass WHERE ResidentID = @id"
            End Select

            Using cmd As New MySqlCommand(updateSql, cn)
                cmd.Parameters.AddWithValue("@newPass", newPassword)
                cmd.Parameters.AddWithValue("@id", currentUserId.Value)

                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()

                If cmd.ExecuteNonQuery() > 0 Then
                    UpdateTicketStatus(currentTicketNumber, "RESET")

                    MessageBox.Show(
                        "✅ Password reset successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    )

                    Me.Close()
                    frmlogin.Show()
                Else
                    MessageBox.Show(
                        "❌ Failed to update password.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    )
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show(
                "Reset Error: " & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Function GenerateTicketNumber() As String
        Dim datePart As String = DateTime.Now.ToString("yyMMdd")
        Dim randomChars As New StringBuilder()
        Dim rnd As New Random()
        Dim chars As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"

        For i = 1 To 4
            randomChars.Append(chars(rnd.Next(chars.Length)))
        Next

        Return $"FP-{datePart}-{randomChars}"
    End Function

    Private Sub UpdateTicketStatus(ticketNum As String, newStatus As String)
        Try
            connection()

            Dim sql As String = "
                UPDATE forgot_password_tickets
                SET status = @status, verified_at = NOW()
                WHERE ticket_number = @ticketNum
            "

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@status", newStatus)
                cmd.Parameters.AddWithValue("@ticketNum", ticketNum)
                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ResetToInitialState()
        ResetDisplayLabels()
        lblInfoStatus.Text = ""
        lblVerifyStatus.Text = ""
        txtNewPassword.Clear()
        btnSingle.Text = "Submit Ticket"
        btnSingle.Enabled = False
        currentUserId = Nothing
        currentUserType = ""
        currentTicketNumber = ""
        isTicketVerified = False
    End Sub

    Private Sub ResetDisplayLabels()
        lblName.Text = "Name:     —"
        lblUsername.Text = "Username: —"
        lblEmail.Text = "Email:    —"
        lblDepartment.Text = "Role:     —"
        lblStatus.Text = "Status:   —"
        lblTicketNumber.Text = "Ticket:   —"
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        frmlogin.Show()
    End Sub
End Class