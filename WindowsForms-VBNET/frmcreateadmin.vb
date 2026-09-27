Imports MySql.Data.MySqlClient

Public Class frmcreateadmin

    Private Sub frmcreateadmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblPassStatus.Text = ""
    End Sub

    Private Sub ClearAllFields()
        txtDepartment.Clear()
        txtLastname.Clear()
        txtFirstname.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        txtConfirmPass.Clear()
        lblPassStatus.Text = ""
        txtDepartment.Focus()
    End Sub

    Private Sub CheckPasswordMatch()
        If String.IsNullOrWhiteSpace(txtPassword.Text) AndAlso String.IsNullOrWhiteSpace(txtConfirmPass.Text) Then
            lblPassStatus.Text = ""
            lblPassStatus.ForeColor = Color.Black
            Return
        End If

        If txtPassword.Text.Trim() = txtConfirmPass.Text.Trim() Then
            lblPassStatus.Text = "Password Match"
            lblPassStatus.ForeColor = Color.Green
        Else
            lblPassStatus.Text = "Password does not match"
            lblPassStatus.ForeColor = Color.Red
        End If
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged, txtConfirmPass.TextChanged
        CheckPasswordMatch()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtDepartment.Text) OrElse
           String.IsNullOrWhiteSpace(txtLastname.Text) OrElse
           String.IsNullOrWhiteSpace(txtFirstname.Text) OrElse
           String.IsNullOrWhiteSpace(txtUsername.Text) OrElse
           String.IsNullOrWhiteSpace(txtPassword.Text) OrElse
           String.IsNullOrWhiteSpace(txtConfirmPass.Text) Then
            MessageBox.Show("Please fill all fields including Department!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If txtPassword.Text.Trim() <> txtConfirmPass.Text.Trim() Then
            MessageBox.Show("Passwords do not match!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            connection()

            Using cmdCheck As New MySqlCommand("SELECT COUNT(*) FROM admin", cn)
                If Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0 Then
                    MessageBox.Show("Admin account already exists! Only one Admin is allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ClearAllFields()
                    Return
                End If
            End Using

            Using cmdCheckUser As New MySqlCommand("SELECT COUNT(*) FROM admin WHERE Username = @user", cn)
                cmdCheckUser.Parameters.AddWithValue("@user", txtUsername.Text.Trim())
                If Convert.ToInt32(cmdCheckUser.ExecuteScalar()) > 0 Then
                    MessageBox.Show("Username already exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ClearAllFields()
                    Return
                End If
            End Using

            Using cmdInsert As New MySqlCommand(
                "INSERT INTO admin (Department, Lastname, Firstname, Username, Password, Role, AccountStatus) " &
                "VALUES (@dept, @last, @first, @user, @pass, 'Administrator', 'Active')", cn)
                cmdInsert.Parameters.AddWithValue("@dept", txtDepartment.Text.Trim())
                cmdInsert.Parameters.AddWithValue("@last", txtLastname.Text.Trim())
                cmdInsert.Parameters.AddWithValue("@first", txtFirstname.Text.Trim())
                cmdInsert.Parameters.AddWithValue("@user", txtUsername.Text.Trim())
                cmdInsert.Parameters.AddWithValue("@pass", txtPassword.Text)
                cmdInsert.ExecuteNonQuery()
            End Using

            MessageBox.Show("Admin Account Created Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearAllFields()
            Me.Hide()
            frmlogin.Show()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Hide()
        frmlogin.Show()
    End Sub

End Class