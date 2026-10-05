Imports System.Text
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D

Public Class frmForgotPassword
    Private currentUserId As Integer? = Nothing
    Private currentUserType As String = ""
    Private currentUsername As String = ""
    Private currentEmail As String = ""
    Private currentFullName As String = ""

    ' Placeholder text for RichTextBox
    Private ReadOnly notesPlaceholder As String = "Write any notes or reason for password reset here..."

    ' Windows API for Placeholder (Cue Banner) in Standard Textboxes
    Private Const EM_SETCUEBANNER As Integer = &H1501
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> lParam As String) As IntPtr
    End Function

    Private Sub frmforgotpassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set native placeholders for regular textboxes
        SendMessage(txtUserInput.Handle, EM_SETCUEBANNER, 0, "Enter Username or ID...")
        SendMessage(txtNewPassword.Handle, EM_SETCUEBANNER, 0, "Min 8 chars (Aa123)")
        SendMessage(txtConfirmPass.Handle, EM_SETCUEBANNER, 0, "Re-enter new password")

        ' Make PictureBox circular
        MakePictureBoxCircular()

        ' Set initial state for RichTextBox placeholder
        InitializeRichTextBoxPlaceholder()

        ResetToInitialState()
        txtUserInput.Focus()
    End Sub

    Private Sub MakePictureBoxCircular()
        If PictureBox1 IsNot Nothing Then
            Dim path As New GraphicsPath()
            path.AddEllipse(0, 0, PictureBox1.Width, PictureBox1.Height)
            PictureBox1.Region = New Region(path)
        End If
    End Sub

    Private Sub InitializeRichTextBoxPlaceholder()
        If RichTextBoxNotes IsNot Nothing Then
            If String.IsNullOrWhiteSpace(RichTextBoxNotes.Text) OrElse RichTextBoxNotes.Text = notesPlaceholder Then
                RichTextBoxNotes.Text = notesPlaceholder
                RichTextBoxNotes.ForeColor = Color.Gray
            End If
        End If
    End Sub

    Private Sub RichTextBoxNotes_Enter(sender As Object, e As EventArgs) Handles RichTextBoxNotes.Enter
        If RichTextBoxNotes.Text = notesPlaceholder Then
            RichTextBoxNotes.Text = ""
            RichTextBoxNotes.ForeColor = Color.Black
        End If
    End Sub

    Private Sub RichTextBoxNotes_Leave(sender As Object, e As EventArgs) Handles RichTextBoxNotes.Leave
        If String.IsNullOrWhiteSpace(RichTextBoxNotes.Text) Then
            RichTextBoxNotes.Text = notesPlaceholder
            RichTextBoxNotes.ForeColor = Color.Gray
        End If
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
            ' Keep error logging quiet on UI during typing, or show status text if needed
            ShowUserNotFound()
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
        Select Case tableName
            Case "admin"
                sql = "SELECT * FROM admin WHERE Username = @input LIMIT 1"
            Case "users"
                sql = "SELECT * FROM users WHERE Username = @input OR Email = @input LIMIT 1"
            Case "residences"
                sql = "SELECT * FROM residences WHERE Username = @input LIMIT 1"
        End Select

        Dim defaultRole As String = If(tableName = "admin", "Administrator", If(tableName = "users", "Staff", "Residence"))

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
                    currentFullName = $"{fName} {lName}".Trim()

                    currentUsername = If(HasColumn(dr, "Username"), SafeStr(dr("Username")), "")
                    currentEmail = If(HasColumn(dr, "Email") AndAlso Not IsDBNull(dr("Email")), SafeStr(dr("Email")), "Not provided")

                    Dim accStatus As String = If(HasColumn(dr, "AccountStatus") AndAlso SafeStr(dr("AccountStatus")) <> "", SafeStr(dr("AccountStatus")), "Active")
                    Dim role As String = If(HasColumn(dr, "Role") AndAlso SafeStr(dr("Role")) <> "", SafeStr(dr("Role")), defaultRole)
                    Dim userImg As Image = Nothing

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

                    DisplayUserInfo(currentFullName, role, accStatus, userImg)
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

    Private Sub DisplayUserInfo(fullName As String, role As String, status As String, userImage As Image)
        lblName.Text = $"Name:    {fullName}"
        lblDepartment.Text = $"Role:      {role}"
        lblStatus.Text = $"Status:    {status}"
        lblStatus.ForeColor = Color.Green

        If userImage IsNot Nothing Then
            PictureBox1.Image = userImage
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage ' Better fit for circular regions
        Else
            PictureBox1.Image = Nothing
        End If

        btnSingle.Text = "Reset Password"
        btnSingle.Enabled = True
    End Sub

    Private Sub ShowUserNotFound()
        ResetDisplayLabels()
        lblStatus.Text = "Status:    User not found"
        lblStatus.ForeColor = Color.Red
        btnSingle.Enabled = False
        currentUserId = Nothing
        currentUserType = ""
        currentUsername = ""
        currentEmail = ""
        currentFullName = ""
    End Sub

    ' Responsive password check as user types
    Private Sub txtNewPassword_TextChanged(sender As Object, e As EventArgs) Handles txtNewPassword.TextChanged
        ValidatePasswordInput()
    End Sub

    Private Sub txtConfirmPass_TextChanged(sender As Object, e As EventArgs) Handles txtConfirmPass.TextChanged
        ValidatePasswordInput()
    End Sub

    Private Sub ValidatePasswordInput()
        Dim pass As String = txtNewPassword.Text
        Dim confirm As String = txtConfirmPass.Text

        ' Optional: If you have an error label on your form (e.g., lblPasswordError), update it here dynamically.
        ' Example: lblPasswordError.Text = ...
    End Sub

    Private Sub btnSingle_Click(sender As Object, e As EventArgs) Handles btnSingle.Click
        If currentUserId.HasValue Then
            ResetUserPassword()
        Else
            lblStatus.Text = "Status:    Search for a valid user first."
            lblStatus.ForeColor = Color.Red
        End If
    End Sub

    Private Sub ResetUserPassword()
        Dim newPassword As String = txtNewPassword.Text.Trim()
        Dim confirmPassword As String = txtConfirmPass.Text.Trim()
        Dim userNotes As String = If(RichTextBoxNotes IsNot Nothing AndAlso RichTextBoxNotes.Text <> notesPlaceholder, RichTextBoxNotes.Text.Trim(), "")

        If String.IsNullOrWhiteSpace(newPassword) OrElse newPassword.Length < 8 Then
            lblStatus.Text = "Status: Password must be at least 8 chars."
            lblStatus.ForeColor = Color.Red
            Return
        End If

        If Not Regex.IsMatch(newPassword, "(?=.*[a-z])(?=.*[A-Z])") Then
            lblStatus.Text = "Status: Password needs upper & lowercase."
            lblStatus.ForeColor = Color.Red
            Return
        End If

        If newPassword <> confirmPassword Then
            lblStatus.Text = "Status: Passwords do not match."
            lblStatus.ForeColor = Color.Red
            Return
        End If

        Try
            connection()

            Dim updateSql As String = ""
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
                cmd.ExecuteNonQuery()
            End Using

            Dim ticketNumber As String = "TKT-" & DateTime.Now.ToString("yyyyMMddHHmmss")
            sql = "INSERT INTO forgot_password_tickets (ticket_number, username, full_name, email, status, notes, requested_at) VALUES (@tktNo, @uname, @fname, @email, 'PENDING', @notes, NOW())"

            Dim ticketSql As String = "INSERT INTO forgot_password_tickets (ticket_number, username, full_name, email, status, notes, requested_at) " &
                                      "VALUES (@tktNo, @uname, @fname, @email, 'PENDING', @notes, NOW())"

            Using cmdTicket As New MySqlCommand(ticketSql, cn)
                cmdTicket.Parameters.AddWithValue("@tktNo", ticketNumber)
                cmdTicket.Parameters.AddWithValue("@uname", currentUsername)
                cmdTicket.Parameters.AddWithValue("@fname", currentFullName)
                cmdTicket.Parameters.AddWithValue("@email", currentEmail)
                cmdTicket.Parameters.AddWithValue("@notes", If(String.IsNullOrWhiteSpace(userNotes), "Password reset requested via Forgot Password module.", userNotes))

                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                cmdTicket.ExecuteNonQuery()
            End Using

            ' Only successful saves show message boxes now
            MessageBox.Show(
                $"Password reset successfully!{Environment.NewLine}{Environment.NewLine}" &
                $"Ticket Number: {ticketNumber}{Environment.NewLine}" &
                "Your account is now marked as PENDING. Please wait for the Administrator to activate your account.",
                "Account Pending Activation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

            Me.Close()
            frmlogin.Show()

        Catch ex As Exception
            MessageBox.Show("Reset Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub ResetToInitialState()
        ResetDisplayLabels()
        txtNewPassword.Clear()
        txtConfirmPass.Clear()
        InitializeRichTextBoxPlaceholder()
        btnSingle.Text = "Submit"
        btnSingle.Enabled = False
        currentUserId = Nothing
        currentUserType = ""
        currentUsername = ""
        currentEmail = ""
        currentFullName = ""
        PictureBox1.Image = Nothing
    End Sub

    Private Sub ResetDisplayLabels()
        lblName.Text = "Name:    —"
        lblDepartment.Text = "Role:      —"
        lblStatus.Text = "Status:    —"
        lblStatus.ForeColor = Color.Black
    End Sub

    'Back to Login Form
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        frmlogin.Show()
    End Sub


    'Exit Button If User Wants to Cancel the Password Reset Process
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MessageBox.Show("Are you sure you want to cancel the password reset process? All entered data will be lost.",
                           "Confirm Cancel",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then

            ResetToInitialState()
            Me.Close()
            Application.Exit()
        End If
    End Sub
End Class