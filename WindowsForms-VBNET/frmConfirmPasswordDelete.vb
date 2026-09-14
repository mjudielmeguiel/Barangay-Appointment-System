Imports System.Drawing
Imports MySql.Data.MySqlClient

Public Class frmConfirmPasswordDelete

    Private userFirstName As String = ""

    Private Sub frmConfirmPasswordDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.UseSystemPasswordChar = True
        txtConfirmPassword.UseSystemPasswordChar = True

        FetchUserFirstName()
        SetupRichTextBoxNotice()
    End Sub

    Private Sub FetchUserFirstName()
        Try
            DBconnection.connection()

            Dim parsedLname As String = ""
            Dim parsedFname As String = ""

            If LoggedFullname.Contains(",") Then
                Dim parts() As String = LoggedFullname.Split(","c)
                parsedLname = parts(0).Trim()
                parsedFname = parts(1).Trim()
            Else
                parsedFname = LoggedFullname.Trim()
            End If

            Dim targetTable As String = If(LoggedRole.ToUpper().Contains("ADMIN"), "admin", "users")

            Dim sql As String = $"SELECT Firstname FROM {targetTable} WHERE (Lastname=@lname AND Firstname=@fname) OR Username=@uname"
            DBconnection.cmd = New MySqlCommand(sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@lname", parsedLname)
            DBconnection.cmd.Parameters.AddWithValue("@fname", parsedFname)
            DBconnection.cmd.Parameters.AddWithValue("@uname", LoggedFullname)

            Dim result = DBconnection.cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                userFirstName = result.ToString()
            Else
                userFirstName = parsedFname
            End If

        Catch ex As Exception
            userFirstName = "User"
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub SetupRichTextBoxNotice()
        If rtbNotice Is Nothing Then Return

        rtbNotice.ReadOnly = True
        rtbNotice.BorderStyle = BorderStyle.None
        rtbNotice.BackColor = Color.FromArgb(248, 249, 252)
        rtbNotice.Clear()

        ' Title Line
        rtbNotice.SelectionFont = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        rtbNotice.SelectionColor = Color.FromArgb(178, 34, 34)
        rtbNotice.AppendText("ACCOUNT DELETION & DATA RETENTION NOTICE" & vbCrLf & vbCrLf)

        ' Personalized Greeting
        rtbNotice.SelectionFont = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        rtbNotice.SelectionColor = Color.FromArgb(10, 25, 100)
        rtbNotice.AppendText($"Hello, {userFirstName}," & vbCrLf & vbCrLf)

        ' Paragraph 1: Intent & Schedule
        rtbNotice.SelectionFont = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        rtbNotice.SelectionColor = Color.FromArgb(40, 40, 50)
        rtbNotice.AppendText($"By submitting a request to terminate your user profile, {userFirstName}, you are initiating a standard 7-day scheduled deletion grace period. During this mandatory holding phase, your active access privileges across the system will be temporarily restricted, but your account profile, personal data, and submitted records will remain safely preserved in an inactive state." & vbCrLf & vbCrLf)

        ' Highlighted Subheading
        rtbNotice.SelectionFont = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        rtbNotice.SelectionColor = Color.FromArgb(10, 25, 100)
        rtbNotice.AppendText("Grace Period & Cancellation Terms:" & vbCrLf)

        ' Paragraph 2: How to Cancel
        rtbNotice.SelectionFont = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        rtbNotice.SelectionColor = Color.FromArgb(40, 40, 50)
        rtbNotice.AppendText($"If you change your mind, {userFirstName}, or submitted this request by accident, you retain full rights to revoke the deletion process. To automatically cancel the pending termination and immediately reinstate your complete account access, simply log back into your account using your valid credentials at any time before the 7-day period expires. Logging in instantly voids the deletion request and removes your profile from the deletion queue." & vbCrLf & vbCrLf)

        ' Warning Subheading
        rtbNotice.SelectionFont = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        rtbNotice.SelectionColor = Color.FromArgb(178, 34, 34)
        rtbNotice.AppendText("Permanent Erasure Warning:" & vbCrLf)

        ' Paragraph 3: Finality & Execution
        rtbNotice.SelectionFont = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        rtbNotice.SelectionColor = Color.FromArgb(40, 40, 50)
        rtbNotice.AppendText($"Please take note, {userFirstName}, that if you do not log in within 7 days (168 consecutive hours), our automated database system will execute a permanent purge. Following execution, data recovery is technically impossible, and lost accounts cannot be restored by system administrators. Please confirm your authorization by re-entering your password below.")

        ' Scroll back to top
        rtbNotice.SelectionStart = 0
        rtbNotice.ScrollToCaret()
    End Sub

    Private Sub btnConfirmDelete_Click(sender As Object, e As EventArgs) Handles btnConfirmDelete.Click
        Dim pass As String = txtPassword.Text.Trim()
        Dim confirmPass As String = txtConfirmPassword.Text.Trim()

        If String.IsNullOrEmpty(pass) OrElse String.IsNullOrEmpty(confirmPass) Then
            MessageBox.Show("Please fill out both password fields.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If pass <> confirmPass Then
            MessageBox.Show("Passwords do not match. Please try again.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtConfirmPassword.Focus()
            Return
        End If

        If MessageBox.Show($"Are you sure you want to schedule your account for permanent deletion after 7 days, {userFirstName}?", "Confirm Deletion Request", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            DBconnection.connection()

            Dim parsedLname As String = ""
            Dim parsedFname As String = ""

            If LoggedFullname.Contains(",") Then
                Dim parts() As String = LoggedFullname.Split(","c)
                parsedLname = parts(0).Trim()
                parsedFname = parts(1).Trim()
            Else
                parsedFname = LoggedFullname.Trim()
            End If

            Dim targetTable As String = If(LoggedRole.ToUpper().Contains("ADMIN"), "admin", "users")

            ' Verify Password
            Dim checkSql As String = $"SELECT COUNT(*) FROM {targetTable} WHERE ((Lastname = @lname AND Firstname = @fname) OR Username = @uname) AND Password = @pass"
            DBconnection.cmd = New MySqlCommand(checkSql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@lname", parsedLname)
            DBconnection.cmd.Parameters.AddWithValue("@fname", parsedFname)
            DBconnection.cmd.Parameters.AddWithValue("@uname", LoggedFullname)
            DBconnection.cmd.Parameters.AddWithValue("@pass", pass)

            If Convert.ToInt32(DBconnection.cmd.ExecuteScalar()) = 0 Then
                MessageBox.Show("Incorrect password.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Focus()
                Return
            End If

            ' Set Deletion Request Timestamp
            Dim updateSql As String = $"UPDATE {targetTable} SET DeletionRequestedAt = NOW() WHERE (Lastname = @lname AND Firstname = @fname) OR Username = @uname"
            DBconnection.cmd = New MySqlCommand(updateSql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@lname", parsedLname)
            DBconnection.cmd.Parameters.AddWithValue("@fname", parsedFname)
            DBconnection.cmd.Parameters.AddWithValue("@uname", LoggedFullname)
            DBconnection.cmd.ExecuteNonQuery()

            MessageBox.Show($"Your account deletion request has been scheduled, {userFirstName}. You have 7 days to cancel this request by simply logging in.", "Deletion Scheduled", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoggedFullname = ""
            LoggedRole = ""
            Me.DialogResult = DialogResult.OK
            Me.Close()
            Application.Restart()

        Catch ex As Exception
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class