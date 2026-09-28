Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class frmcreateadmin
    Private profileImageBytes As Byte() = Nothing

    Private Sub frmcreateadmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblPassStatus.Text = ""
        ClearAllErrorLabels()
        MakePictureBoxCircular(picUser)
        If picUser.Image Is Nothing Then
            picUser.BackColor = Color.FromArgb(235, 238, 250)
        End If
        SetPlaceholder(txtLastname, "Enter Last Name")
        SetPlaceholder(txtFirstname, "Enter First Name")
        SetPlaceholder(txtUsername, "Enter Username")
        SetPlaceholder(txtPassword, "Enter Password")
        SetPlaceholder(txtConfirmPass, "Re-enter Password")
    End Sub

    Private Sub MakePictureBoxCircular(pic As PictureBox)
        pic.SizeMode = PictureBoxSizeMode.StretchImage
        AddHandler pic.Paint, AddressOf RoundPictureBox_Paint
    End Sub

    Private Sub RoundPictureBox_Paint(sender As Object, e As PaintEventArgs)
        Dim pic As PictureBox = CType(sender, PictureBox)
        Dim radius As Integer = Math.Min(pic.Width, pic.Height) \ 2
        Dim path As New GraphicsPath()
        path.AddEllipse(0, 0, pic.Width, pic.Height)
        pic.Region = New Region(path)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Using pen As New Pen(Color.FromArgb(10, 25, 100), 3)
            e.Graphics.DrawEllipse(pen, 1, 1, pic.Width - 3, pic.Height - 3)
        End Using
    End Sub

    Private Sub picUser_DoubleClick(sender As Object, e As EventArgs) Handles picUser.DoubleClick
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            ofd.Title = "Select Profile Picture"
            If ofd.ShowDialog() = DialogResult.OK Then
                If picUser.Image IsNot Nothing Then picUser.Image.Dispose()
                picUser.Image = Image.FromFile(ofd.FileName)
                picUser.BackColor = Color.Transparent
                Using ms As New MemoryStream()
                    picUser.Image.Save(ms, Imaging.ImageFormat.Jpeg)
                    profileImageBytes = ms.ToArray()
                End Using
                lblPassStatus.Text = "✓ Photo Uploaded"
                lblPassStatus.ForeColor = Color.Green
            End If
        End Using
    End Sub

    Private Sub SetPlaceholder(txt As TextBox, placeholderText As String)
        If String.IsNullOrWhiteSpace(txt.Text) Then
            txt.Text = placeholderText
            txt.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub RemovePlaceholder(txt As TextBox, placeholderText As String)
        If txt.Text = placeholderText AndAlso txt.ForeColor = Color.Gray Then
            txt.Text = ""
            txt.ForeColor = Color.Black
            If txt Is txtPassword OrElse txt Is txtConfirmPass Then
                txt.PasswordChar = "●"c
            End If
        End If
    End Sub

    Private Sub txtLastname_Enter(sender As Object, e As EventArgs) Handles txtLastname.Enter
        RemovePlaceholder(txtLastname, "Enter Last Name")
    End Sub

    Private Sub txtLastname_Leave(sender As Object, e As EventArgs) Handles txtLastname.Leave
        SetPlaceholder(txtLastname, "Enter Last Name")
    End Sub

    Private Sub txtFirstname_Enter(sender As Object, e As EventArgs) Handles txtFirstname.Enter
        RemovePlaceholder(txtFirstname, "Enter First Name")
    End Sub

    Private Sub txtFirstname_Leave(sender As Object, e As EventArgs) Handles txtFirstname.Leave
        SetPlaceholder(txtFirstname, "Enter First Name")
    End Sub

    Private Sub txtUsername_Enter(sender As Object, e As EventArgs) Handles txtUsername.Enter
        RemovePlaceholder(txtUsername, "Enter Username")
    End Sub

    Private Sub txtUsername_Leave(sender As Object, e As EventArgs) Handles txtUsername.Leave
        SetPlaceholder(txtUsername, "Enter Username")
    End Sub

    Private Sub txtPassword_Enter(sender As Object, e As EventArgs) Handles txtPassword.Enter
        RemovePlaceholder(txtPassword, "Enter Password")
    End Sub

    Private Sub txtPassword_Leave(sender As Object, e As EventArgs) Handles txtPassword.Leave
        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            txtPassword.PasswordChar = ControlChars.NullChar
            SetPlaceholder(txtPassword, "Enter Password")
        End If
        CheckPasswordMatch()
    End Sub

    Private Sub txtConfirmPass_Enter(sender As Object, e As EventArgs) Handles txtConfirmPass.Enter
        RemovePlaceholder(txtConfirmPass, "Re-enter Password")
    End Sub

    Private Sub txtConfirmPass_Leave(sender As Object, e As EventArgs) Handles txtConfirmPass.Leave
        If String.IsNullOrWhiteSpace(txtConfirmPass.Text) Then
            txtConfirmPass.PasswordChar = ControlChars.NullChar
            SetPlaceholder(txtConfirmPass, "Re-enter Password")
        End If
        CheckPasswordMatch()
    End Sub

    Private Sub ClearAllErrorLabels()
        lblErrorLastname.Text = ""
        lblErrorFirstname.Text = ""
        lblErrorUsername.Text = ""
        lblPassStatus.Text = ""
        lblErrorConfirmPass.Text = ""
    End Sub

    Private Sub ShowError(lbl As Label, message As String)
        lbl.Text = message
        lbl.ForeColor = Color.Red
    End Sub

    Private Sub ValidateLastname()
        Dim val As String = txtLastname.Text.Trim()
        If String.IsNullOrWhiteSpace(val) OrElse val = "Enter Last Name" Then
            ShowError(lblErrorLastname, "Last Name is required")
        ElseIf val.Length < 2 Then
            ShowError(lblErrorLastname, "At least 2 characters required")
        Else
            lblErrorLastname.Text = ""
        End If
    End Sub

    Private Sub ValidateFirstname()
        Dim val As String = txtFirstname.Text.Trim()
        If String.IsNullOrWhiteSpace(val) OrElse val = "Enter First Name" Then
            ShowError(lblErrorFirstname, "First Name is required")
        ElseIf val.Length < 2 Then
            ShowError(lblErrorFirstname, "At least 2 characters required")
        Else
            lblErrorFirstname.Text = ""
        End If
    End Sub

    Private Sub ValidateUsername()
        Dim val As String = txtUsername.Text.Trim()
        If String.IsNullOrWhiteSpace(val) OrElse val = "Enter Username" Then
            ShowError(lblErrorUsername, "Username is required")
        ElseIf val.Length < 4 Then
            ShowError(lblErrorUsername, "At least 4 characters required")
        ElseIf Not Regex.IsMatch(val, "^[a-zA-Z0-9_]+$") Then
            ShowError(lblErrorUsername, "Letters, numbers, underscore only")
        Else
            lblErrorUsername.Text = ""
        End If
    End Sub

    Private Sub ValidatePassword()
        Dim val As String = txtPassword.Text.Trim()
        If String.IsNullOrWhiteSpace(val) OrElse val = "Enter Password" Then
            ShowError(lblPassStatus, "Password is required")
        Else
            lblPassStatus.Text = ""
        End If
        CheckPasswordMatch()
    End Sub

    Private Sub ValidateConfirmPassword()
        Dim pass As String = txtPassword.Text.Trim()
        Dim confirm As String = txtConfirmPass.Text.Trim()
        If String.IsNullOrWhiteSpace(confirm) OrElse confirm = "Re-enter Password" Then
            ShowError(lblErrorConfirmPass, "Confirm your password")
        ElseIf pass <> confirm Then
            ShowError(lblErrorConfirmPass, "Passwords do not match")
        Else
            lblErrorConfirmPass.Text = ""
        End If
        CheckPasswordMatch()
    End Sub

    Private Sub CheckPasswordMatch()
        Dim pass As String = txtPassword.Text.Trim()
        Dim confirm As String = txtConfirmPass.Text.Trim()

        If (pass = "" OrElse pass = "Enter Password") AndAlso
           (confirm = "" OrElse confirm = "Re-enter Password") Then
            If profileImageBytes Is Nothing Then lblPassStatus.Text = ""
            Return
        End If

        If pass = confirm AndAlso Not String.IsNullOrEmpty(pass) AndAlso pass <> "Enter Password" Then
            lblPassStatus.Text = "✅ Password Match"
            lblPassStatus.ForeColor = Color.Green
        ElseIf Not String.IsNullOrEmpty(confirm) AndAlso confirm <> "Re-enter Password" AndAlso pass <> confirm Then
            lblPassStatus.Text = "❌ Password does not match"
            lblPassStatus.ForeColor = Color.Red
        End If
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged, txtConfirmPass.TextChanged
        CheckPasswordMatch()
    End Sub

    ' ✅ INAYOS ANG VALIDATION LOGIC DITO
    Private Function ValidateAllFields() As Boolean
        ClearAllErrorLabels()

        ValidateLastname()
        ValidateFirstname()
        ValidateUsername()
        ValidatePassword()
        ValidateConfirmPassword()

        Dim hasError As Boolean = False

        ' Kung may laman ang mga normal error labels, ibig sabihin may error
        If Not String.IsNullOrEmpty(lblErrorLastname.Text) Then hasError = True
        If Not String.IsNullOrEmpty(lblErrorFirstname.Text) Then hasError = True
        If Not String.IsNullOrEmpty(lblErrorUsername.Text) Then hasError = True
        If Not String.IsNullOrEmpty(lblErrorConfirmPass.Text) Then hasError = True

        ' Dahil ang lblPassStatus ay ginagamit din sa success (Green), 
        ' iche-check lang natin kung RED ba ang kulay niya para masabing error ito.
        If lblPassStatus.ForeColor = Color.Red Then hasError = True

        Return Not hasError
    End Function

    Private Sub ClearAllFields()
        txtLastname.Clear()
        txtFirstname.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        txtConfirmPass.Clear()
        profileImageBytes = Nothing
        If picUser.Image IsNot Nothing Then picUser.Image.Dispose()
        picUser.Image = Nothing
        picUser.BackColor = Color.FromArgb(235, 238, 250)
        ClearAllErrorLabels()
        lblPassStatus.Text = ""
        frmcreateadmin_Load(Nothing, Nothing)
        txtLastname.Focus()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Hide()
        frmlogin.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    ' ✅ BINALIK ANG PANGALAN SA NORMAL NA "btnSave_Click"
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateAllFields() Then
            Return ' Pipigilan ang pag-save kapag may error
        End If

        Try
            connection()

            Using cmdCheckUser As New MySqlCommand("SELECT COUNT(*) FROM admin WHERE Username = @user", cn)
                cmdCheckUser.Parameters.AddWithValue("@user", txtUsername.Text.Trim())
                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                If Convert.ToInt32(cmdCheckUser.ExecuteScalar()) > 0 Then
                    ShowError(lblErrorUsername, "Username already taken")
                    txtUsername.Focus()
                    Return
                End If
            End Using

            Dim query As String = "INSERT INTO admin (Firstname, Lastname, Username, Password, Role, AccountStatus, Picture) " &
                                  "VALUES (@first, @last, @user, @pass, 'Admin', 'Active', @pic)"

            Using cmdInsert As New MySqlCommand(query, cn)
                cmdInsert.Parameters.AddWithValue("@first", txtFirstname.Text.Trim())
                cmdInsert.Parameters.AddWithValue("@last", txtLastname.Text.Trim())
                cmdInsert.Parameters.AddWithValue("@user", txtUsername.Text.Trim())
                cmdInsert.Parameters.AddWithValue("@pass", txtPassword.Text)

                cmdInsert.Parameters.Add("@pic", MySqlDbType.LongBlob).Value =
                    If(profileImageBytes IsNot Nothing, profileImageBytes, DBNull.Value)

                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                cmdInsert.ExecuteNonQuery()
            End Using

            MessageBox.Show("✅ Admin Account Created Successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearAllFields()
            Me.Hide()
            frmlogin.Show()

        Catch ex As Exception
            MessageBox.Show("Database Error: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub
End Class