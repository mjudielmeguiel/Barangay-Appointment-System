Imports System.Text
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmforgotpassword
    Private currentUserId As Integer? = Nothing
    Private currentUserType As String = ""

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
            lblInfoStatus.Text = "❌ Search Error: " & ex.Message
            lblInfoStatus.ForeColor = Color.Red
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
                sql = $"SELECT * FROM admin WHERE Username = @input LIMIT 1"
                defaultRole = "Administrator"
            Case "users"
                sql = $"SELECT * FROM users WHERE Username = @input OR Email = @input LIMIT 1"
                defaultRole = "Staff"
            Case "residences"
                sql = $"SELECT * FROM residences WHERE Username = @input LIMIT 1"
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

                    Dim fName As String = If(HasColumn(dr, "Firstname"), SafeStr(dr("Firstname")), "")
                    Dim lName As String = If(HasColumn(dr, "Lastname"), SafeStr(dr("Lastname")), "")
                    Dim fullName As String = $"{fName} {lName}".Trim()

                    Dim username As String = If(HasColumn(dr, "Username"), SafeStr(dr("Username")), "")
                    Dim email As String = "Not provided"
                    Dim accStatus As String = "Active"
                    Dim role As String = defaultRole
                    Dim userImg As Image = Nothing

                    If HasColumn(dr, "Email") Then email = If(SafeStr(dr("Email")) = "", "Not provided", SafeStr(dr("Email")))
                    If HasColumn(dr, "AccountStatus") Then accStatus = If(SafeStr(dr("AccountStatus")) = "", "Active", SafeStr(dr("AccountStatus")))
                    If HasColumn(dr, "Role") Then role = If(SafeStr(dr("Role")) = "", defaultRole, SafeStr(dr("Role")))

                    If HasColumn(dr, "Picture") AndAlso Not IsDBNull(dr("Picture")) Then
                        Try
                            Dim imgData As Byte() = DirectCast(dr("Picture"), Byte())
                            Using ms As New MemoryStream(imgData)
                                userImg = Image.FromStream(ms)
                            End Using
                        Catch
                            userImg = Nothing
                        End Try
                    End If

                    DisplayUserInfo(fullName, username, email, role, accStatus, userImg)
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
        status As String,
        userImage As Image
    )
        lblName.Text = $"Name:    {fullName}"
        lblUsername.Text = $"Username: {username}"
        lblEmail.Text = $"Email:    {email}"
        lblDepartment.Text = $"Role:      {role}"
        lblStatus.Text = $"Status:   {status}"

        If userImage IsNot Nothing Then
            PictureBox1.Image = userImage
            PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        Else
            PictureBox1.Image = Nothing
        End If

        lblInfoStatus.Text = "✅ User Found"
        lblInfoStatus.ForeColor = Color.Green

        btnSingle.Text = "Reset Password"
        btnSingle.Enabled = True
    End Sub

    Private Sub ShowUserNotFound()
        ResetDisplayLabels()
        lblInfoStatus.Text = "❌ No user found"
        lblInfoStatus.ForeColor = Color.Red
        btnSingle.Enabled = False
        currentUserId = Nothing
        currentUserType = ""
    End Sub

    Private Sub btnSingle_Click(sender As Object, e As EventArgs) Handles btnSingle.Click
        If currentUserId.HasValue Then
            ResetUserPassword()
        Else
            lblInfoStatus.Text = "⚠️ Search for a user first."
            lblInfoStatus.ForeColor = Color.Orange
        End If
    End Sub

    Private Sub ResetUserPassword()
        Dim newPassword As String = txtNewPassword.Text.Trim()

        If String.IsNullOrWhiteSpace(newPassword) OrElse newPassword.Length < 8 Then
            lblVerifyStatus.Text = "⚠️ Password must be at least 8 characters."
            lblVerifyStatus.ForeColor = Color.Orange
            Return
        End If

        If Not Regex.IsMatch(newPassword, "(?=.*[a-z])(?=.*[A-Z])") Then
            lblVerifyStatus.Text = "⚠️ Password must contain uppercase & lowercase letters."
            lblVerifyStatus.ForeColor = Color.Orange
            Return
        End If

        Try
            connection()

            Dim updateSql As String = ""

            ' Awtomatikong pinapalitan ang Password at sineset ang AccountStatus sa 'Pending'
            Select Case currentUserType
                Case "admin" : updateSql = "UPDATE admin SET Password = @newPass, AccountStatus = 'Pending' WHERE AdminID = @id"
                Case "users" : updateSql = "UPDATE users SET Password = @newPass, AccountStatus = 'Pending' WHERE UserID = @id"
                Case "residences" : updateSql = "UPDATE residences SET Password = @newPass, AccountStatus = 'Pending' WHERE ResidentID = @id"
            End Select

            Using cmd As New MySqlCommand(updateSql, cn)
                cmd.Parameters.AddWithValue("@newPass", newPassword)
                cmd.Parameters.AddWithValue("@id", currentUserId.Value)

                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()

                If cmd.ExecuteNonQuery() > 0 Then
                    MessageBox.Show(
                        $"✅ Password reset successfully!{Environment.NewLine}{Environment.NewLine}" &
                        "Your account is now marked as PENDING. Please wait for the Administrator to activate your account before you can log in.",
                        "Account Pending Activation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    )

                    Me.Close()
                    frmlogin.Show()
                Else
                    lblVerifyStatus.Text = "❌ Failed to update password."
                    lblVerifyStatus.ForeColor = Color.Red
                End If
            End Using

        Catch ex As Exception
            lblVerifyStatus.Text = "❌ Reset Error: " & ex.Message
            lblVerifyStatus.ForeColor = Color.Red
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub ResetToInitialState()
        ResetDisplayLabels()
        lblInfoStatus.Text = ""
        lblVerifyStatus.Text = ""
        txtNewPassword.Clear()
        btnSingle.Text = "Submit"
        btnSingle.Enabled = False
        currentUserId = Nothing
        currentUserType = ""
        PictureBox1.Image = Nothing
    End Sub

    Private Sub ResetDisplayLabels()
        lblName.Text = "Name:     —"
        lblUsername.Text = "Username: —"
        lblEmail.Text = "Email:    —"
        lblDepartment.Text = "Role:     —"
        lblStatus.Text = "Status:   —"
        ' Tinanggal ang lblTicketNumber dahil hindi na ito kailangan
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        frmlogin.Show()
    End Sub
End Class