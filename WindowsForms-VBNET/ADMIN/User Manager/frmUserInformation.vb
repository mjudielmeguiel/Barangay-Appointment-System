Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Text.RegularExpressions

Public Class frmUserInformation

    Private currentUserID As Integer
    Private originalPassword As String = String.Empty
    Private userImageBytes As Byte() = Nothing

    Private allowedRoles As New List(Of String) From {
        "Administrator", "System Admin", "Manager", "Supervisor",
        "Staff", "Office Staff", "Encoder", "Clerk",
        "Security", "Security Guard", "Receptionist",
        "Cashier", "Accounting", "Auditor", "HR Staff",
        "IT Support", "Technician", "Maintenance",
        "Driver", "Inventory Staff", "Nurse", "Medical Staff"
    }

    Public Sub New(ByVal userID As Integer)
        InitializeComponent()
        currentUserID = userID
    End Sub

    Private Sub frmUserInformation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        LoadDepartmentsCombo()
        LoadRoles()
        LoadUserData()
        ClearAllErrors()
    End Sub

    ' --- LOAD DEPARTMENTS INTO COMBOBOX USING DEPARTMENTID & DEPARTMENTNAME ---
    Private Sub LoadDepartmentsCombo()
        Try
            connection()
            sql = "SELECT DepartmentID, DepartmentName FROM departments WHERE IsActive = 1 ORDER BY DepartmentName ASC"

            Dim dtDept As New DataTable()
            Using localCmd As New MySqlCommand(sql, cn)
                Using adapter As New MySqlDataAdapter(localCmd)
                    adapter.Fill(dtDept)
                End Using
            End Using

            cboDepartment.DataSource = dtDept
            cboDepartment.DisplayMember = "DepartmentName"
            cboDepartment.ValueMember = "DepartmentID"
            cboDepartment.SelectedIndex = -1

        Catch ex As Exception
            MsgBox("Error loading departments: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadRoles()
        cboRole.Items.Clear()
        cboRole.Items.AddRange(allowedRoles.ToArray())
    End Sub

    Private Sub LoadUserData()
        Try
            connection()
            Dim query As String = "SELECT StaffCode, Lastname, Firstname, DepartmentID, Role, Username, Password, AccountStatus, Picture " &
                                 "FROM users WHERE UserID = @uid"

            Dim dt As New DataTable()
            Using localCmd As New MySqlCommand(query, cn)
                localCmd.Parameters.AddWithValue("@uid", currentUserID)
                Using adapter As New MySqlDataAdapter(localCmd)
                    adapter.Fill(dt)
                End Using
            End Using

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                lblStaffCode.Text = If(row("StaffCode") Is DBNull.Value, "", row("StaffCode").ToString())
                txtLastname.Text = If(row("Lastname") Is DBNull.Value, "", row("Lastname").ToString())
                txtFirstname.Text = If(row("Firstname") Is DBNull.Value, "", row("Firstname").ToString())

                ' Set selected department by DepartmentID
                If row("DepartmentID") IsNot DBNull.Value Then
                    cboDepartment.SelectedValue = Convert.ToInt32(row("DepartmentID"))
                Else
                    cboDepartment.SelectedIndex = -1
                End If

                cboRole.Text = If(row("Role") Is DBNull.Value, "", row("Role").ToString())
                txtUsername.Text = If(row("Username") Is DBNull.Value, "", row("Username").ToString())

                originalPassword = If(row("Password") Is DBNull.Value, "", row("Password").ToString())
                txtPassword.Text = originalPassword
                txtConfirmPass.Text = originalPassword

                lblAccountStatus.Text = "Status: " & If(row("AccountStatus") Is DBNull.Value, "Active", row("AccountStatus").ToString())

                If row("Picture") IsNot DBNull.Value Then
                    userImageBytes = CType(row("Picture"), Byte())

                    If userImageBytes.Length > 0 Then
                        Try
                            Using ms As New MemoryStream(userImageBytes)
                                picUser.SizeMode = PictureBoxSizeMode.StretchImage
                                picUser.Image = Image.FromStream(ms)
                            End Using
                        Catch exStream As Exception
                            picUser.Image = Nothing
                            userImageBytes = Nothing
                        End Try
                    Else
                        picUser.Image = Nothing
                        userImageBytes = Nothing
                    End If
                Else
                    picUser.Image = Nothing
                    userImageBytes = Nothing
                End If
            Else
                MsgBox("User record not found.", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox("Error loading user information: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub ClearAllErrors()
        lblLastnameError.Text = ""
        lblFirstnameError.Text = ""
        lblDepartmentError.Text = ""
        lblUsernameError.Text = ""
        lblConfirmPassError.Text = ""
        lblRoleError.Text = ""
    End Sub

    Private Sub txtLastname_TextChanged(sender As Object, e As EventArgs) Handles txtLastname.TextChanged
        If String.IsNullOrWhiteSpace(txtLastname.Text) Then
            lblLastnameError.Text = "Lastname is required."
            lblLastnameError.ForeColor = Color.Red
        ElseIf Not Regex.IsMatch(txtLastname.Text.Trim(), "^[a-zA-ZñÑ\s]+$") Then
            lblLastnameError.Text = "Letters only (no numbers/symbols)."
            lblLastnameError.ForeColor = Color.Red
        Else
            lblLastnameError.Text = ""
        End If
    End Sub

    Private Sub txtFirstname_TextChanged(sender As Object, e As EventArgs) Handles txtFirstname.TextChanged
        If String.IsNullOrWhiteSpace(txtFirstname.Text) Then
            lblFirstnameError.Text = "Firstname is required."
            lblFirstnameError.ForeColor = Color.Red
        ElseIf Not Regex.IsMatch(txtFirstname.Text.Trim(), "^[a-zA-ZñÑ\s]+$") Then
            lblFirstnameError.Text = "Letters only (no numbers/symbols)."
            lblFirstnameError.ForeColor = Color.Red
        Else
            lblFirstnameError.Text = ""
        End If
    End Sub

    Private Sub cboDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDepartment.SelectedIndexChanged
        If cboDepartment.SelectedIndex = -1 Then
            lblDepartmentError.Text = "Department is required."
            lblDepartmentError.ForeColor = Color.Red
        Else
            lblDepartmentError.Text = ""
        End If
    End Sub

    Private Sub cboRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRole.SelectedIndexChanged, cboRole.TextChanged
        If Not allowedRoles.Contains(cboRole.Text.Trim()) Then
            lblRoleError.Text = "Invalid role selected."
            lblRoleError.ForeColor = Color.Red
        Else
            lblRoleError.Text = ""
        End If
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged
        If String.IsNullOrWhiteSpace(txtUsername.Text) Then
            lblUsernameError.Text = "Username is required."
            lblUsernameError.ForeColor = Color.Red
            Return
        End If

        Dim lowerUser As String = txtUsername.Text.Trim().ToLower()
        If lowerUser.Contains("admin") Then
            lblUsernameError.Text = "Usernames related to 'admin' are strictly prohibited!"
            lblUsernameError.ForeColor = Color.Red
            Return
        End If

        Try
            connection()
            sql = "SELECT COUNT(*) FROM users WHERE Username = @uname AND UserID <> @uid"
            Using localCmd As New MySqlCommand(sql, cn)
                localCmd.Parameters.AddWithValue("@uname", txtUsername.Text.Trim())
                localCmd.Parameters.AddWithValue("@uid", currentUserID)
                Dim userCount As Integer = Convert.ToInt32(localCmd.ExecuteScalar())

                If userCount > 0 Then
                    lblUsernameError.Text = "Username already exists."
                    lblUsernameError.ForeColor = Color.Red
                Else
                    lblUsernameError.Text = ""
                End If
            End Using

        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged, txtConfirmPass.TextChanged
        If txtPassword.Text = originalPassword Then
            lblConfirmPassError.Text = ""
            Return
        End If

        Dim hasUpper As Boolean = txtPassword.Text.Any(AddressOf Char.IsUpper)
        Dim hasLower As Boolean = txtPassword.Text.Any(AddressOf Char.IsLower)

        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            lblConfirmPassError.Text = "Password is required."
            lblConfirmPassError.ForeColor = Color.Red
            Return
        End If

        If Not hasUpper OrElse Not hasLower OrElse txtPassword.Text.Length < 6 Then
            lblConfirmPassError.Text = "Password must be at least 6 chars with uppercase and lowercase."
            lblConfirmPassError.ForeColor = Color.Red
            Return
        End If

        If txtPassword.Text.Trim() <> txtConfirmPass.Text.Trim() Then
            lblConfirmPassError.Text = "Passwords do not match."
            lblConfirmPassError.ForeColor = Color.Red
        Else
            lblConfirmPassError.Text = "Passwords match."
            lblConfirmPassError.ForeColor = Color.Green
        End If
    End Sub

    Private Sub picUser_DoubleClick(sender As Object, e As EventArgs) Handles picUser.DoubleClick
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            If ofd.ShowDialog() = DialogResult.OK Then
                picUser.SizeMode = PictureBoxSizeMode.StretchImage
                picUser.Image = Image.FromFile(ofd.FileName)

                Using ms As New MemoryStream()
                    picUser.Image.Save(ms, ImageFormat.Jpeg)
                    userImageBytes = ms.ToArray()
                End Using
            End If
        End Using
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If String.IsNullOrWhiteSpace(txtLastname.Text) OrElse
           String.IsNullOrWhiteSpace(txtFirstname.Text) OrElse
           cboDepartment.SelectedValue Is Nothing OrElse
           String.IsNullOrWhiteSpace(txtUsername.Text) OrElse
           String.IsNullOrWhiteSpace(txtPassword.Text) OrElse
           String.IsNullOrWhiteSpace(txtConfirmPass.Text) OrElse
           cboRole.SelectedIndex = -1 Then
            MsgBox("Please fill all required fields correctly!", MsgBoxStyle.Exclamation)
            Return
        End If

        If txtUsername.Text.Trim().ToLower().Contains("admin") Then
            MsgBox("Usernames related to 'admin' are strictly prohibited!", MsgBoxStyle.Critical)
            Return
        End If

        If Not allowedRoles.Contains(cboRole.Text.Trim()) Then
            MsgBox("Selected role is invalid!", MsgBoxStyle.Exclamation)
            Return
        End If

        If Not Regex.IsMatch(txtLastname.Text.Trim(), "^[a-zA-ZñÑ\s]+$") OrElse Not Regex.IsMatch(txtFirstname.Text.Trim(), "^[a-zA-ZñÑ\s]+$") Then
            MsgBox("First name and Last name must contain letters only!", MsgBoxStyle.Exclamation)
            Return
        End If

        If txtPassword.Text <> originalPassword Then
            Dim hasUpper As Boolean = txtPassword.Text.Any(AddressOf Char.IsUpper)
            Dim hasLower As Boolean = txtPassword.Text.Any(AddressOf Char.IsLower)

            If Not hasUpper OrElse Not hasLower OrElse txtPassword.Text.Length < 6 Then
                MsgBox("Password must be at least 6 characters and contain both uppercase and lowercase letters!", MsgBoxStyle.Exclamation)
                Return
            End If

            If txtPassword.Text.Trim() <> txtConfirmPass.Text.Trim() Then
                MsgBox("Passwords do not match!", MsgBoxStyle.Exclamation)
                Return
            End If
        End If

        If MsgBox("Are you sure you want to update this user information?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Update") = MsgBoxResult.No Then
            Return
        End If

        Dim fullName As String = $"{txtLastname.Text.Trim()}, {txtFirstname.Text.Trim()}"
        Dim selectedDeptID As Integer = Convert.ToInt32(cboDepartment.SelectedValue)

        Try
            connection()
            sql = "UPDATE users SET Lastname = @lname, Firstname = @fname, FullName = @fullname, DepartmentID = @deptID, " &
                  "Username = @uname, Password = @pass, Role = @role, Picture = @pic WHERE UserID = @uid"

            Using localCmd As New MySqlCommand(sql, cn)
                localCmd.Parameters.AddWithValue("@lname", txtLastname.Text.Trim())
                localCmd.Parameters.AddWithValue("@fname", txtFirstname.Text.Trim())
                localCmd.Parameters.AddWithValue("@fullname", fullName)
                localCmd.Parameters.AddWithValue("@deptID", selectedDeptID)
                localCmd.Parameters.AddWithValue("@uname", txtUsername.Text.Trim())
                localCmd.Parameters.AddWithValue("@pass", txtPassword.Text)
                localCmd.Parameters.AddWithValue("@role", cboRole.Text)

                If userImageBytes IsNot Nothing Then
                    localCmd.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = userImageBytes
                Else
                    localCmd.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = DBNull.Value
                End If

                localCmd.Parameters.AddWithValue("@uid", currentUserID)
                localCmd.ExecuteNonQuery()
            End Using

            MsgBox("User information updated successfully!", MsgBoxStyle.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MsgBox("Error updating user: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If MsgBox("Are you sure you want to delete this user?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Delete") = MsgBoxResult.Yes Then
            Try
                connection()
                sql = "DELETE FROM users WHERE UserID = @uid"
                Using localCmd As New MySqlCommand(sql, cn)
                    localCmd.Parameters.AddWithValue("@uid", currentUserID)
                    localCmd.ExecuteNonQuery()
                End Using

                MsgBox("User deleted successfully!", MsgBoxStyle.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()

            Catch ex As Exception
                MsgBox("Error deleting user: " & ex.Message, MsgBoxStyle.Critical)
            Finally
                CloseConnection()
            End Try
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class