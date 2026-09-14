Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions

Public Class frmChange_Password

    Private Sub frmChange_Password_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCurrentPassword.UseSystemPasswordChar = True
        txtNewPassword.UseSystemPasswordChar = True
        txtConfirmPassword.UseSystemPasswordChar = True

        ClearAllErrors()
    End Sub

    Private Sub ClearAllErrors()
        SetLabelError(lblCurrentError, "")
        SetLabelError(lblNewError, "")
        SetLabelError(lblConfirmError, "")
    End Sub

    Private Sub SetLabelError(lbl As Label, message As String)
        If lbl IsNot Nothing Then
            If String.IsNullOrEmpty(message) Then
                lbl.Text = ""
                lbl.Visible = False
            Else
                lbl.Text = message
                lbl.ForeColor = Color.Crimson
                lbl.Font = New Font("Segoe UI", 8.0F, FontStyle.Italic)
                lbl.Visible = True
            End If
        End If
    End Sub

    Private Sub txtCurrentPassword_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentPassword.TextChanged
        SetLabelError(lblCurrentError, "")
    End Sub

    Private Sub txtNewPassword_TextChanged(sender As Object, e As EventArgs) Handles txtNewPassword.TextChanged
        SetLabelError(lblNewError, "")
    End Sub

    Private Sub txtConfirmPassword_TextChanged(sender As Object, e As EventArgs) Handles txtConfirmPassword.TextChanged
        SetLabelError(lblConfirmError, "")
    End Sub

    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        ClearAllErrors()

        Dim currentPass As String = txtCurrentPassword.Text.Trim()
        Dim newPass As String = txtNewPassword.Text.Trim()
        Dim confirmPass As String = txtConfirmPassword.Text.Trim()
        Dim hasError As Boolean = False

        If String.IsNullOrEmpty(currentPass) Then
            SetLabelError(lblCurrentError, "Current password is required.")
            hasError = True
        End If

        If String.IsNullOrEmpty(newPass) Then
            SetLabelError(lblNewError, "New password is required.")
            hasError = True
        Else
            If newPass.Length < 6 Then
                SetLabelError(lblNewError, "Password must be at least 6 characters.")
                hasError = True
            ElseIf Not Regex.IsMatch(newPass, "[A-Z]") Then
                SetLabelError(lblNewError, "Password must contain at least one uppercase letter (A-Z).")
                hasError = True
            ElseIf Not Regex.IsMatch(newPass, "[a-z]") Then
                SetLabelError(lblNewError, "Password must contain at least one lowercase letter (a-z).")
                hasError = True
            End If
        End If

        If String.IsNullOrEmpty(confirmPass) Then
            SetLabelError(lblConfirmError, "Please confirm your new password.")
            hasError = True
        End If

        If hasError Then Return

        If newPass <> confirmPass Then
            SetLabelError(lblConfirmError, "New password and confirm password do not match.")
            txtConfirmPassword.Focus()
            Return
        End If

        Try
            DBconnection.connection()

            ' Parse Lastname and Firstname from LoggedFullname ("Lastname, Firstname")
            Dim parsedLname As String = ""
            Dim parsedFname As String = ""

            If LoggedFullname.Contains(",") Then
                Dim parts() As String = LoggedFullname.Split(","c)
                parsedLname = parts(0).Trim()
                parsedFname = parts(1).Trim()
            Else
                parsedFname = LoggedFullname.Trim()
            End If

            ' Determine table based on user role
            Dim targetTable As String = "users"
            If LoggedRole.ToUpper() = "ADMINISTRATOR" OrElse LoggedRole.ToUpper() = "SYSTEM ADMIN" OrElse LoggedRole.ToUpper() = "ADMIN" Then
                targetTable = "admin"
            End If

            ' Verify current password
            Dim checkSql As String = $"SELECT COUNT(*) FROM {targetTable} WHERE ((Lastname = @lname AND Firstname = @fname) OR Username = @uname) AND Password = @currentPass"
            DBconnection.cmd = New MySqlCommand(checkSql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@lname", parsedLname)
            DBconnection.cmd.Parameters.AddWithValue("@fname", parsedFname)
            DBconnection.cmd.Parameters.AddWithValue("@uname", LoggedFullname)
            DBconnection.cmd.Parameters.AddWithValue("@currentPass", currentPass)

            Dim isValid As Integer = Convert.ToInt32(DBconnection.cmd.ExecuteScalar())

            If isValid = 0 Then
                SetLabelError(lblCurrentError, "Incorrect current password.")
                txtCurrentPassword.Focus()
                Return
            End If

            If currentPass = newPass Then
                SetLabelError(lblNewError, "New password cannot be the same as current password.")
                txtNewPassword.Focus()
                Return
            End If

            ' Update password in the target table
            Dim updateSql As String = $"UPDATE {targetTable} SET Password = @newPass WHERE (Lastname = @lname AND Firstname = @fname) OR Username = @uname"
            DBconnection.cmd = New MySqlCommand(updateSql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@newPass", newPass)
            DBconnection.cmd.Parameters.AddWithValue("@lname", parsedLname)
            DBconnection.cmd.Parameters.AddWithValue("@fname", parsedFname)
            DBconnection.cmd.Parameters.AddWithValue("@uname", LoggedFullname)

            Dim rowsAffected As Integer = DBconnection.cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("Failed to update password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Database error: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class