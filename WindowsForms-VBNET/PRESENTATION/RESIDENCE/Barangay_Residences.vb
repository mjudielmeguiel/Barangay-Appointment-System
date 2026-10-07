Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Text.RegularExpressions
Imports System.Net

Public Class Barangay_Residences

    Private profileImageBytes As Byte() = Nothing
    Private idFrontBytes As Byte() = Nothing
    Private idBackBytes As Byte() = Nothing
    Private editResidentID As Integer = 0

    Private ReadOnly PH_LASTNAME As String = "Dela Cruz *"
    Private ReadOnly PH_FIRSTNAME As String = "Juan *"
    Private ReadOnly PH_MIDDLENAME As String = "Rizal (Optional)"
    Private ReadOnly PH_STREET As String = "Blk 1 Lot 2 Makisig St. *"
    Private ReadOnly PH_BIRTHPLACE As String = "Manila City *"
    Private ReadOnly PH_FATHER As String = "Mario Dela Cruz"
    Private ReadOnly PH_MOTHER As String = "Maria Dela Cruz"
    Private ReadOnly PH_MOBILE As String = "09123456789 (Optional)"
    Private ReadOnly PH_EMAIL As String = "juandelacruz@gmail.com (Optional)"
    Private ReadOnly PH_USERNAME As String = "juandc123"
    Private ReadOnly PH_PASSWORD As String = "Password123"
    Private ReadOnly PH_CONFIRMPASS As String = "Password123"
    Private ReadOnly PH_SUFFIX As String = "Select Suffix"
    Private ReadOnly PH_CIVIL As String = "Civil Status *"
    Private ReadOnly PH_GENDER As String = "Gender *"
    Private ReadOnly PH_SATELLITE As String = "Select Satellite Office *"

    Private ReadOnly COLOR_PLACEHOLDER As Color = Color.Gray
    Private ReadOnly COLOR_NORMAL As Color = Color.FromArgb(30, 30, 30)
    Private ReadOnly COLOR_REQUIRED As Color = Color.FromArgb(190, 30, 30)

    Public Sub New()
        InitializeComponent()
        editResidentID = 0
    End Sub

    Public Sub New(ByVal residentID As Integer)
        InitializeComponent()
        editResidentID = residentID
    End Sub

    Private Sub Barangay_Residences_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboSuffix.Items.Clear()
        cboSuffix.Items.AddRange({"N/A", "JR.", "SR.", "II", "III", "IV", "V"})
        cboSuffix.SelectedIndex = -1

        cboCivilStatus.Items.Clear()
        cboCivilStatus.Items.AddRange({"SINGLE", "MARRIED", "WIDOWED", "SEPARATED", "DIVORCED"})
        cboCivilStatus.SelectedIndex = -1

        cboGender.Items.Clear()
        cboGender.Items.AddRange({"MALE", "FEMALE", "OTHER"})
        cboGender.SelectedIndex = -1

        txtBarangay.Text = "PUTATAN"
        txtBarangay.ReadOnly = True
        txtCity.Text = "MUNTINLUPA CITY"
        txtCity.ReadOnly = True

        pnlaccountsystem.Enabled = False
        pnlaccountsystem.Visible = True

        LoadSatelliteOffices()

        ClearAllValidationLabels()
        SetAllPlaceholders()

        If editResidentID > 0 Then
            btnSubmit.Text = "Update Record"
            LoadResidentDataForEdit()
        Else
            btnSubmit.Text = "Submit"
            GenerateResidentCode()
        End If
    End Sub

    ' --- SATELLITE OFFICE LOADING & SEARCH SETUP ---
    Private Sub LoadSatelliteOffices()
        Try
            cboSatelliteOffice.Items.Clear()
            connection()
            Using cmd As New MySqlCommand("SELECT facility_name FROM brgy_putatan_satellite_offices", cn)
                Using dr = cmd.ExecuteReader()
                    While dr.Read()
                        cboSatelliteOffice.Items.Add(dr("facility_name").ToString())
                    End While
                End Using
            End Using

            cboSatelliteOffice.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cboSatelliteOffice.AutoCompleteSource = AutoCompleteSource.ListItems
        Catch ex As Exception
            MsgBox("Error loading satellite offices: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' --- FUNCTION PARA KUNIN ANG ID NG NAPILING SATELLITE OFFICE AUTOMATICALLY ---
    Private Function GetSatelliteOfficeID(facilityName As String) As Integer
        Dim satID As Integer = 0
        Try
            connection()
            Using cmd As New MySqlCommand("SELECT id FROM brgy_putatan_satellite_offices WHERE facility_name = @name", cn)
                cmd.Parameters.AddWithValue("@name", facilityName)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    satID = Convert.ToInt32(result)
                End If
            End Using
        Catch
            satID = 0
        Finally
            CloseConnection()
        End Try
        Return satID
    End Function

    Private Sub ReturnToRecordList()
        frmMain.Panel2.Controls.Clear()

        Dim gridForm As New frmResidence_Records()
        gridForm.TopLevel = False
        gridForm.FormBorderStyle = FormBorderStyle.None
        gridForm.Dock = DockStyle.Fill

        frmMain.Panel2.Controls.Add(gridForm)
        gridForm.BringToFront()
        gridForm.Show()
    End Sub

    Private Sub SetAllPlaceholders()
        SetPlaceholder(txtLastname, PH_LASTNAME)
        SetPlaceholder(txtFirstname, PH_FIRSTNAME)
        SetPlaceholder(txtMiddlename, PH_MIDDLENAME)
        SetPlaceholder(txtStreetAddress, PH_STREET)
        SetPlaceholder(txtBirthPlace, PH_BIRTHPLACE)
        SetPlaceholder(txtFatherName, PH_FATHER)
        SetPlaceholder(txtMotherName, PH_MOTHER)
        SetPlaceholder(txtMobileNumber, PH_MOBILE)
        SetPlaceholder(txtEmail, PH_EMAIL)

        txtPassword.PasswordChar = ControlChars.NullChar
        txtConfirmPassword.PasswordChar = ControlChars.NullChar
        SetPlaceholder(txtUsername, PH_USERNAME)
        SetPlaceholder(txtPassword, PH_PASSWORD)
        SetPlaceholder(txtConfirmPassword, PH_CONFIRMPASS)

        SetComboPlaceholder(cboSuffix, PH_SUFFIX)
        SetComboPlaceholder(cboCivilStatus, PH_CIVIL)
        SetComboPlaceholder(cboGender, PH_GENDER)
        SetComboPlaceholder(cboSatelliteOffice, PH_SATELLITE)
    End Sub

    Private Sub SetPlaceholder(txt As TextBox, text As String)
        If String.IsNullOrWhiteSpace(txt.Text) OrElse txt.Text = text Then
            txt.Text = text
            txt.ForeColor = COLOR_PLACEHOLDER
        End If
    End Sub

    Private Sub ClearPlaceholder(txt As TextBox)
        If txt.Text = GetPlaceholderFor(txt) Then
            txt.Text = String.Empty
            txt.ForeColor = COLOR_NORMAL
        End If
    End Sub

    Private Function GetPlaceholderFor(txt As TextBox) As String
        Select Case txt.Name
            Case NameOf(txtLastname) : Return PH_LASTNAME
            Case NameOf(txtFirstname) : Return PH_FIRSTNAME
            Case NameOf(txtMiddlename) : Return PH_MIDDLENAME
            Case NameOf(txtStreetAddress) : Return PH_STREET
            Case NameOf(txtBirthPlace) : Return PH_BIRTHPLACE
            Case NameOf(txtFatherName) : Return PH_FATHER
            Case NameOf(txtMotherName) : Return PH_MOTHER
            Case NameOf(txtMobileNumber) : Return PH_MOBILE
            Case NameOf(txtEmail) : Return PH_EMAIL
            Case NameOf(txtUsername) : Return PH_USERNAME
            Case NameOf(txtPassword) : Return PH_PASSWORD
            Case NameOf(txtConfirmPassword) : Return PH_CONFIRMPASS
            Case Else : Return String.Empty
        End Select
    End Function

    Private Function HasPlaceholderText(txt As TextBox) As Boolean
        Return txt.Text = GetPlaceholderFor(txt)
    End Function

    Private Sub SetComboPlaceholder(cbo As ComboBox, text As String)
        If cbo.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(cbo.Text) Then
            cbo.Text = text
            cbo.ForeColor = COLOR_PLACEHOLDER
        End If
    End Sub

    Private Sub ClearComboPlaceholder(cbo As ComboBox)
        If cbo.ForeColor = COLOR_PLACEHOLDER Then
            cbo.Text = String.Empty
            cbo.ForeColor = COLOR_NORMAL
        End If
    End Sub

    Private Sub txtLastname_GotFocus(sender As Object, e As EventArgs) Handles txtLastname.GotFocus
        ClearPlaceholder(txtLastname)
    End Sub
    Private Sub txtLastname_LostFocus(sender As Object, e As EventArgs) Handles txtLastname.LostFocus
        If String.IsNullOrWhiteSpace(txtLastname.Text) Then SetPlaceholder(txtLastname, PH_LASTNAME)
    End Sub

    Private Sub txtFirstname_GotFocus(sender As Object, e As EventArgs) Handles txtFirstname.GotFocus
        ClearPlaceholder(txtFirstname)
    End Sub
    Private Sub txtFirstname_LostFocus(sender As Object, e As EventArgs) Handles txtFirstname.LostFocus
        If String.IsNullOrWhiteSpace(txtFirstname.Text) Then SetPlaceholder(txtFirstname, PH_FIRSTNAME)
    End Sub

    Private Sub txtMiddlename_GotFocus(sender As Object, e As EventArgs) Handles txtMiddlename.GotFocus
        ClearPlaceholder(txtMiddlename)
    End Sub
    Private Sub txtMiddlename_LostFocus(sender As Object, e As EventArgs) Handles txtMiddlename.LostFocus
        If String.IsNullOrWhiteSpace(txtMiddlename.Text) Then SetPlaceholder(txtMiddlename, PH_MIDDLENAME)
    End Sub

    Private Sub txtStreetAddress_GotFocus(sender As Object, e As EventArgs) Handles txtStreetAddress.GotFocus
        ClearPlaceholder(txtStreetAddress)
    End Sub
    Private Sub txtStreetAddress_LostFocus(sender As Object, e As EventArgs) Handles txtStreetAddress.LostFocus
        If String.IsNullOrWhiteSpace(txtStreetAddress.Text) Then SetPlaceholder(txtStreetAddress, PH_STREET)
    End Sub

    Private Sub txtBirthPlace_GotFocus(sender As Object, e As EventArgs) Handles txtBirthPlace.GotFocus
        ClearPlaceholder(txtBirthPlace)
    End Sub
    Private Sub txtBirthPlace_LostFocus(sender As Object, e As EventArgs) Handles txtBirthPlace.LostFocus
        If String.IsNullOrWhiteSpace(txtBirthPlace.Text) Then SetPlaceholder(txtBirthPlace, PH_BIRTHPLACE)
    End Sub

    Private Sub txtFatherName_GotFocus(sender As Object, e As EventArgs) Handles txtFatherName.GotFocus
        ClearPlaceholder(txtFatherName)
    End Sub
    Private Sub txtFatherName_LostFocus(sender As Object, e As EventArgs) Handles txtFatherName.LostFocus
        If String.IsNullOrWhiteSpace(txtFatherName.Text) Then SetPlaceholder(txtFatherName, PH_FATHER)
    End Sub

    Private Sub txtMotherName_GotFocus(sender As Object, e As EventArgs) Handles txtMotherName.GotFocus
        ClearPlaceholder(txtMotherName)
    End Sub
    Private Sub txtMotherName_LostFocus(sender As Object, e As EventArgs) Handles txtMotherName.LostFocus
        If String.IsNullOrWhiteSpace(txtMotherName.Text) Then SetPlaceholder(txtMotherName, PH_MOTHER)
    End Sub

    Private Sub txtMobileNumber_GotFocus(sender As Object, e As EventArgs) Handles txtMobileNumber.GotFocus
        ClearPlaceholder(txtMobileNumber)
    End Sub
    Private Sub txtMobileNumber_LostFocus(sender As Object, e As EventArgs) Handles txtMobileNumber.LostFocus
        If String.IsNullOrWhiteSpace(txtMobileNumber.Text) Then SetPlaceholder(txtMobileNumber, PH_MOBILE)
    End Sub

    Private Sub txtEmail_GotFocus(sender As Object, e As EventArgs) Handles txtEmail.GotFocus
        ClearPlaceholder(txtEmail)
    End Sub
    Private Sub txtEmail_LostFocus(sender As Object, e As EventArgs) Handles txtEmail.LostFocus
        If String.IsNullOrWhiteSpace(txtEmail.Text) Then
            SetPlaceholder(txtEmail, PH_EMAIL)
            pnlaccountsystem.Enabled = False
            pnlaccountsystem.Visible = True
        End If
    End Sub

    Private Sub txtUsername_GotFocus(sender As Object, e As EventArgs) Handles txtUsername.GotFocus
        ClearPlaceholder(txtUsername)
    End Sub
    Private Sub txtUsername_LostFocus(sender As Object, e As EventArgs) Handles txtUsername.LostFocus
        If String.IsNullOrWhiteSpace(txtUsername.Text) Then SetPlaceholder(txtUsername, PH_USERNAME)
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged
        If Not pnlaccountsystem.Enabled OrElse HasPlaceholderText(txtUsername) OrElse String.IsNullOrWhiteSpace(txtUsername.Text) Then
            SetFeedbackLabel(lblUsernameError, "", False)
            Return
        End If

        Dim val = txtUsername.Text.Trim()

        If val.IndexOf("admin", StringComparison.OrdinalIgnoreCase) >= 0 Then
            SetFeedbackLabel(lblUsernameError, "Username cannot contain 'admin'", True)
            Return
        End If

        Try
            connection()
            Dim count As Integer
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM residences WHERE LOWER(Username) = LOWER(@username) AND ResidentID <> @currentId", cn)
                cmd.Parameters.AddWithValue("@username", val)
                cmd.Parameters.AddWithValue("@currentId", editResidentID)
                count = CInt(cmd.ExecuteScalar())
            End Using
            SetFeedbackLabel(lblUsernameError, If(count > 0, "Username already taken", ""), count > 0)
        Catch
            SetFeedbackLabel(lblUsernameError, "", False)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub txtPassword_GotFocus(sender As Object, e As EventArgs) Handles txtPassword.GotFocus
        ClearPlaceholder(txtPassword)
        txtPassword.PasswordChar = "•"c
    End Sub
    Private Sub txtPassword_LostFocus(sender As Object, e As EventArgs) Handles txtPassword.LostFocus
        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            txtPassword.PasswordChar = ControlChars.NullChar
            SetPlaceholder(txtPassword, PH_PASSWORD)
        End If
    End Sub

    Private Sub txtConfirmPassword_GotFocus(sender As Object, e As EventArgs) Handles txtConfirmPassword.GotFocus
        ClearPlaceholder(txtConfirmPassword)
        txtConfirmPassword.PasswordChar = "•"c
    End Sub
    Private Sub txtConfirmPassword_LostFocus(sender As Object, e As EventArgs) Handles txtConfirmPassword.LostFocus
        If String.IsNullOrWhiteSpace(txtConfirmPassword.Text) Then
            txtConfirmPassword.PasswordChar = ControlChars.NullChar
            SetPlaceholder(txtConfirmPassword, PH_CONFIRMPASS)
        End If
    End Sub

    Private Sub cboSuffix_GotFocus(sender As Object, e As EventArgs) Handles cboSuffix.GotFocus
        ClearComboPlaceholder(cboSuffix)
    End Sub
    Private Sub cboSuffix_LostFocus(sender As Object, e As EventArgs) Handles cboSuffix.LostFocus
        If cboSuffix.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(cboSuffix.Text) Then
            SetComboPlaceholder(cboSuffix, PH_SUFFIX)
        End If
    End Sub

    Private Sub cboCivilStatus_GotFocus(sender As Object, e As EventArgs) Handles cboCivilStatus.GotFocus
        ClearComboPlaceholder(cboCivilStatus)
    End Sub
    Private Sub cboCivilStatus_LostFocus(sender As Object, e As EventArgs) Handles cboCivilStatus.LostFocus
        If cboCivilStatus.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(cboCivilStatus.Text) Then
            SetComboPlaceholder(cboCivilStatus, PH_CIVIL)
        End If
    End Sub

    Private Sub cboGender_GotFocus(sender As Object, e As EventArgs) Handles cboGender.GotFocus
        ClearComboPlaceholder(cboGender)
    End Sub
    Private Sub cboGender_LostFocus(sender As Object, e As EventArgs) Handles cboGender.LostFocus
        If cboGender.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(cboGender.Text) Then
            SetComboPlaceholder(cboGender, PH_GENDER)
        End If
    End Sub

    Private Sub cboSatelliteOffice_GotFocus(sender As Object, e As EventArgs) Handles cboSatelliteOffice.GotFocus
        ClearComboPlaceholder(cboSatelliteOffice)
    End Sub
    Private Sub cboSatelliteOffice_LostFocus(sender As Object, e As EventArgs) Handles cboSatelliteOffice.LostFocus
        If cboSatelliteOffice.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(cboSatelliteOffice.Text) Then
            SetComboPlaceholder(cboSatelliteOffice, PH_SATELLITE)
        End If
    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
        If Not HasPlaceholderText(txtEmail) AndAlso Not String.IsNullOrWhiteSpace(txtEmail.Text) Then
            pnlaccountsystem.Enabled = True
            pnlaccountsystem.Visible = True
        ElseIf String.IsNullOrWhiteSpace(txtEmail.Text) OrElse HasPlaceholderText(txtEmail) Then
            pnlaccountsystem.Enabled = False
            pnlaccountsystem.Visible = True
        End If
        ValidateEmailField()
    End Sub

    Private Sub LoadResidentDataForEdit()
        Dim lName As String = String.Empty
        Dim fName As String = String.Empty
        Dim mName As String = String.Empty
        Dim sfx As String = String.Empty
        Dim bPlace As String = String.Empty
        Dim cStatus As String = String.Empty
        Dim gen As String = String.Empty
        Dim mob As String = String.Empty
        Dim eml As String = String.Empty
        Dim usr As String = String.Empty
        Dim addr As String = String.Empty
        Dim fathName As String = String.Empty
        Dim mothName As String = String.Empty
        Dim satOffice As String = String.Empty
        Dim bDay As DateTime = DateTime.Now
        Dim hasBday As Boolean = False
        Dim picBytes As Byte() = Nothing

        Try
            connection()
            Using cmd As New MySqlCommand("SELECT * FROM residences WHERE ResidentID = @resid", cn)
                cmd.Parameters.AddWithValue("@resid", editResidentID)
                Using dr = cmd.ExecuteReader()
                    If dr.Read() Then
                        lName = dr("Lastname").ToString()
                        fName = dr("Firstname").ToString()
                        mName = dr("Middlename").ToString()
                        sfx = dr("Suffix").ToString()
                        bPlace = dr("BirthPlace").ToString()
                        cStatus = dr("CivilStatus").ToString()
                        gen = dr("Gender").ToString()
                        mob = dr("MobileNumber").ToString()
                        eml = dr("Email").ToString()
                        usr = If(IsDBNull(dr("Username")), "", dr("Username").ToString())
                        addr = dr("Address").ToString()
                        fathName = If(IsDBNull(dr("FatherName")) OrElse String.IsNullOrWhiteSpace(dr("FatherName").ToString()) OrElse dr("FatherName").ToString() = "N/A", String.Empty, dr("FatherName").ToString())
                        mothName = If(IsDBNull(dr("MotherName")) OrElse String.IsNullOrWhiteSpace(dr("MotherName").ToString()) OrElse dr("MotherName").ToString() = "N/A", String.Empty, dr("MotherName").ToString())

                        satOffice = If(IsDBNull(dr("SatelliteOffice")), "", dr("SatelliteOffice").ToString())

                        If Not IsDBNull(dr("Birthday")) Then
                            bDay = Convert.ToDateTime(dr("Birthday"))
                            hasBday = True
                        End If
                        If Not IsDBNull(dr("Picture")) Then
                            picBytes = CType(dr("Picture"), Byte())
                        End If
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading data: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try

        txtLastname.Text = lName.Trim() : txtLastname.ForeColor = COLOR_NORMAL
        txtFirstname.Text = fName.Trim() : txtFirstname.ForeColor = COLOR_NORMAL

        If String.IsNullOrWhiteSpace(mName) Then
            SetPlaceholder(txtMiddlename, PH_MIDDLENAME)
        Else
            txtMiddlename.Text = mName.Trim() : txtMiddlename.ForeColor = COLOR_NORMAL
        End If

        Dim addrParts = addr.Split({","c}, StringSplitOptions.None)
        If addrParts.Length > 0 Then
            txtStreetAddress.Text = addrParts(0).Trim() : txtStreetAddress.ForeColor = COLOR_NORMAL
        Else
            SetPlaceholder(txtStreetAddress, PH_STREET)
        End If

        txtBirthPlace.Text = bPlace.Trim() : txtBirthPlace.ForeColor = COLOR_NORMAL

        If Not String.IsNullOrWhiteSpace(sfx) AndAlso cboSuffix.Items.Contains(sfx) Then
            cboSuffix.Text = sfx : cboSuffix.ForeColor = COLOR_NORMAL
        Else
            cboSuffix.SelectedIndex = -1 : SetComboPlaceholder(cboSuffix, PH_SUFFIX)
        End If

        If Not String.IsNullOrWhiteSpace(cStatus) AndAlso cboCivilStatus.Items.Contains(cStatus) Then
            cboCivilStatus.Text = cStatus : cboCivilStatus.ForeColor = COLOR_NORMAL
        Else
            SetComboPlaceholder(cboCivilStatus, PH_CIVIL)
        End If

        If Not String.IsNullOrWhiteSpace(gen) AndAlso cboGender.Items.Contains(gen) Then
            cboGender.Text = gen : cboGender.ForeColor = COLOR_NORMAL
        Else
            SetComboPlaceholder(cboGender, PH_GENDER)
        End If

        If Not String.IsNullOrWhiteSpace(satOffice) AndAlso cboSatelliteOffice.Items.Contains(satOffice) Then
            cboSatelliteOffice.Text = satOffice : cboSatelliteOffice.ForeColor = COLOR_NORMAL
        Else
            cboSatelliteOffice.SelectedIndex = -1 : SetComboPlaceholder(cboSatelliteOffice, PH_SATELLITE)
        End If

        If String.IsNullOrWhiteSpace(fathName) Then
            SetPlaceholder(txtFatherName, PH_FATHER)
        Else
            txtFatherName.Text = fathName.Trim() : txtFatherName.ForeColor = COLOR_NORMAL
        End If

        If String.IsNullOrWhiteSpace(mothName) Then
            SetPlaceholder(txtMotherName, PH_MOTHER)
        Else
            txtMotherName.Text = mothName.Trim() : txtMotherName.ForeColor = COLOR_NORMAL
        End If

        If String.IsNullOrWhiteSpace(mob) OrElse mob = "N/A" Then
            SetPlaceholder(txtMobileNumber, PH_MOBILE)
        Else
            txtMobileNumber.Text = mob.Trim() : txtMobileNumber.ForeColor = COLOR_NORMAL
        End If

        If String.IsNullOrWhiteSpace(eml) OrElse eml.Equals("N/A", StringComparison.OrdinalIgnoreCase) Then
            SetPlaceholder(txtEmail, PH_EMAIL)
            pnlaccountsystem.Enabled = False
            pnlaccountsystem.Visible = True
        Else
            txtEmail.Text = eml.Trim() : txtEmail.ForeColor = COLOR_NORMAL
            pnlaccountsystem.Enabled = True
            pnlaccountsystem.Visible = True
            If Not String.IsNullOrWhiteSpace(usr) Then
                txtUsername.Text = usr.Trim() : txtUsername.ForeColor = COLOR_NORMAL
            End If
        End If

        If hasBday Then dtpBirthday.Value = bDay

        If picBytes IsNot Nothing Then
            Using ms As New MemoryStream(picBytes)
                picUser.Image = Image.FromStream(ms)
                picUser.SizeMode = PictureBoxSizeMode.StretchImage
            End Using
            profileImageBytes = picBytes
        End If
    End Sub

    Private Sub Barangay_Residences_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnSubmit.PerformClick()
        End If
    End Sub

    Private Sub ConvertToUpperCase_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStreetAddress.KeyPress, txtBirthPlace.KeyPress, txtFatherName.KeyPress, txtMotherName.KeyPress
        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub ClearAllValidationLabels()
        SetFeedbackLabel(lblLastnameError, "", False)
        SetFeedbackLabel(lblFirstnameError, "", False)
        SetFeedbackLabel(lblStreetAddressError, "", False)
        SetFeedbackLabel(lblBirthPlaceError, "", False)
        SetFeedbackLabel(lblMobileError, "", False)
        SetFeedbackLabel(lblEmailError, "", False)
        SetFeedbackLabel(lblUsernameError, "", False)
        SetFeedbackLabel(lblPasswordError, "", False)
        SetFeedbackLabel(lblPictureError, "", False)
    End Sub

    Private Sub SetFeedbackLabel(lbl As Label, message As String, isError As Boolean)
        If lbl Is Nothing Then Return
        lbl.Text = message
        lbl.Visible = Not String.IsNullOrEmpty(message)
        lbl.ForeColor = If(isError, COLOR_REQUIRED, Color.FromArgb(30, 140, 60))
    End Sub

    Private Function GetNextResidentCode() As String
        Dim nextCode = "RES-001"
        Try
            connection()
            Using cmd As New MySqlCommand("SELECT ResidentCode FROM residences WHERE ResidentCode LIKE 'RES-%' ORDER BY ResidentID DESC LIMIT 1", cn)
                Using dr = cmd.ExecuteReader()
                    If dr.Read() Then
                        Dim lastCode = dr("ResidentCode").ToString()
                        If lastCode.Contains("-") Then
                            Dim parts = lastCode.Split("-"c)
                            If Integer.TryParse(parts(1), Nothing) Then
                                nextCode = "RES-" & (CInt(parts(1)) + 1).ToString("D3")
                            End If
                        End If
                    End If
                End Using
            End Using
        Catch
        Finally
            CloseConnection()
        End Try
        Return nextCode
    End Function

    Private Sub GenerateResidentCode()
    End Sub

    Private Sub txtMobileNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMobileNumber.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtLastname_TextChanged(sender As Object, e As EventArgs) Handles txtLastname.TextChanged
        If HasPlaceholderText(txtLastname) OrElse String.IsNullOrWhiteSpace(txtLastname.Text) Then
            SetFeedbackLabel(lblLastnameError, "Lastname is required *", True)
        ElseIf Not Regex.IsMatch(txtLastname.Text.Trim(), "^[a-zA-ZñÑ\s]+$") Then
            SetFeedbackLabel(lblLastnameError, "Letters only", True)
        Else
            SetFeedbackLabel(lblLastnameError, "", False)
        End If
    End Sub

    Private Sub txtFirstname_TextChanged(sender As Object, e As EventArgs) Handles txtFirstname.TextChanged
        If HasPlaceholderText(txtFirstname) OrElse String.IsNullOrWhiteSpace(txtFirstname.Text) Then
            SetFeedbackLabel(lblFirstnameError, "Firstname is required *", True)
        ElseIf Not Regex.IsMatch(txtFirstname.Text.Trim(), "^[a-zA-ZñÑ\s]+$") Then
            SetFeedbackLabel(lblFirstnameError, "Letters only", True)
        Else
            SetFeedbackLabel(lblFirstnameError, "", False)
        End If
    End Sub

    Private Sub txtStreetAddress_TextChanged(sender As Object, e As EventArgs) Handles txtStreetAddress.TextChanged
        If HasPlaceholderText(txtStreetAddress) OrElse String.IsNullOrWhiteSpace(txtStreetAddress.Text) Then
            SetFeedbackLabel(lblStreetAddressError, "Street address is required *", True)
        Else
            SetFeedbackLabel(lblStreetAddressError, "", False)
        End If
    End Sub

    Private Sub txtBirthPlace_TextChanged(sender As Object, e As EventArgs) Handles txtBirthPlace.TextChanged
        If HasPlaceholderText(txtBirthPlace) OrElse String.IsNullOrWhiteSpace(txtBirthPlace.Text) Then
            SetFeedbackLabel(lblBirthPlaceError, "Birth place is required *", True)
        Else
            SetFeedbackLabel(lblBirthPlaceError, "", False)
        End If
    End Sub

    Private Sub txtMobileNumber_TextChanged(sender As Object, e As EventArgs) Handles txtMobileNumber.TextChanged
        If HasPlaceholderText(txtMobileNumber) OrElse String.IsNullOrWhiteSpace(txtMobileNumber.Text) Then
            SetFeedbackLabel(lblMobileError, "", False)
            Return
        End If

        Dim val = txtMobileNumber.Text.Trim()
        If Not val.StartsWith("09") Then
            SetFeedbackLabel(lblMobileError, "Must start with 09", True)
            Return
        End If
        If val.Length <> 11 Then
            SetFeedbackLabel(lblMobileError, "Must be 11 digits", True)
            Return
        End If

        Try
            connection()
            Dim count As Integer
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM residences WHERE MobileNumber = @mobile AND MobileNumber <> 'N/A' AND ResidentID <> @currentId", cn)
                cmd.Parameters.AddWithValue("@mobile", val)
                cmd.Parameters.AddWithValue("@currentId", editResidentID)
                count = CInt(cmd.ExecuteScalar())
            End Using
            SetFeedbackLabel(lblMobileError, If(count > 0, "Number already registered", ""), count > 0)
        Catch
            SetFeedbackLabel(lblMobileError, "", False)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub ValidateEmailField()
        If HasPlaceholderText(txtEmail) OrElse String.IsNullOrWhiteSpace(txtEmail.Text) Then
            SetFeedbackLabel(lblEmailError, "", False)
            Return
        End If

        Dim val = txtEmail.Text.Trim().ToLower()
        Dim gmailRegex As New Regex("^[a-zA-Z0-9._%+-]+@gmail\.com$", RegexOptions.IgnoreCase)
        If Not gmailRegex.IsMatch(val) Then
            SetFeedbackLabel(lblEmailError, "Use @gmail.com only", True)
            Return
        End If

        Try
            connection()
            Dim count As Integer
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM residences WHERE LOWER(Email) = LOWER(@email) AND Email <> 'N/A' AND ResidentID <> @currentId", cn)
                cmd.Parameters.AddWithValue("@email", val)
                cmd.Parameters.AddWithValue("@currentId", editResidentID)
                count = CInt(cmd.ExecuteScalar())
            End Using
            SetFeedbackLabel(lblEmailError, If(count > 0, "Email already registered", ""), count > 0)
        Catch
            SetFeedbackLabel(lblEmailError, "", False)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub txtConfirmPassword_TextChanged(sender As Object, e As EventArgs) Handles txtConfirmPassword.TextChanged
        If pnlaccountsystem.Enabled AndAlso Not String.IsNullOrWhiteSpace(txtPassword.Text) AndAlso Not String.IsNullOrWhiteSpace(txtConfirmPassword.Text) Then
            If txtPassword.Text <> txtConfirmPassword.Text Then
                SetFeedbackLabel(lblPasswordError, "Passwords do not match", True)
            Else
                SetFeedbackLabel(lblPasswordError, "", False)
            End If
        Else
            SetFeedbackLabel(lblPasswordError, "", False)
        End If
    End Sub

    Private Sub picUser_DoubleClick(sender As Object, e As EventArgs) Handles picUser.DoubleClick
        SelectImage(picUser, profileImageBytes)
    End Sub

    Private Sub picIDFront_DoubleClick(sender As Object, e As EventArgs) Handles picIDFront.DoubleClick
        SelectImage(picIDFront, idFrontBytes)
    End Sub

    Private Sub picIDBack_DoubleClick(sender As Object, e As EventArgs) Handles picIDBack.DoubleClick
        SelectImage(picIDBack, idBackBytes)
    End Sub

    Private Sub SelectImage(pictureBox As PictureBox, ByRef targetBytes As Byte())
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            If ofd.ShowDialog() = DialogResult.OK Then
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage
                pictureBox.Image = Image.FromFile(ofd.FileName)
                Using ms As New MemoryStream()
                    pictureBox.Image.Save(ms, ImageFormat.Jpeg)
                    targetBytes = ms.ToArray()
                End Using
                SetFeedbackLabel(lblPictureError, "", False)
            End If
        End Using
    End Sub

    Private Sub WriteActivityLog(actionType As String, details As String, residentId As Integer)
        Try
            connection()
            Dim sqlLog = "INSERT INTO activity_logs (ActionType, Details, FullName, UserRole, Module, IPAddress, DeviceInfo, UserID) VALUES (@actType, @det, @fullname, @role, @module, @ip, @dev, @usrid)"
            Using cmdLog As New MySqlCommand(sqlLog, cn)
                cmdLog.Parameters.AddWithValue("@actType", actionType)
                cmdLog.Parameters.AddWithValue("@det", details)
                cmdLog.Parameters.AddWithValue("@fullname", If(String.IsNullOrWhiteSpace(LoggedFullname), "Unknown", LoggedFullname))
                cmdLog.Parameters.AddWithValue("@role", If(String.IsNullOrWhiteSpace(LoggedRole), "Staff", LoggedRole))
                cmdLog.Parameters.AddWithValue("@module", "RESIDENTS")
                cmdLog.Parameters.AddWithValue("@ip", GetLocalIPAddress())
                cmdLog.Parameters.AddWithValue("@dev", Environment.MachineName)
                cmdLog.Parameters.AddWithValue("@usrid", residentId)
                cmdLog.ExecuteNonQuery()
            End Using
        Catch
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Function GetLocalIPAddress() As String
        Try
            Dim host = Dns.GetHostEntry(Dns.GetHostName())
            For Each ip In host.AddressList
                If ip.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
        Catch
        End Try
        Return "Unknown"
    End Function

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If editResidentID = 0 AndAlso profileImageBytes Is Nothing Then
            SetFeedbackLabel(lblPictureError, "Profile picture is required *", True)
        Else
            SetFeedbackLabel(lblPictureError, "", False)
        End If

        Dim hasError As Boolean = (lblLastnameError.Visible AndAlso lblLastnameError.ForeColor = COLOR_REQUIRED) OrElse
                                  (lblFirstnameError.Visible AndAlso lblFirstnameError.ForeColor = COLOR_REQUIRED) OrElse
                                  (lblStreetAddressError.Visible AndAlso lblStreetAddressError.ForeColor = COLOR_REQUIRED) OrElse
                                  (lblBirthPlaceError.Visible AndAlso lblBirthPlaceError.ForeColor = COLOR_REQUIRED) OrElse
                                  (lblMobileError.Visible AndAlso lblMobileError.ForeColor = COLOR_REQUIRED) OrElse
                                  (lblEmailError.Visible AndAlso lblEmailError.ForeColor = COLOR_REQUIRED) OrElse
                                  (lblUsernameError IsNot Nothing AndAlso lblUsernameError.Visible AndAlso lblUsernameError.ForeColor = COLOR_REQUIRED) OrElse
                                  (lblPasswordError.Visible AndAlso lblPasswordError.ForeColor = COLOR_REQUIRED) OrElse
                                  (lblPictureError.Visible AndAlso lblPictureError.ForeColor = COLOR_REQUIRED)

        If hasError Then
            MsgBox("Please fix the highlighted fields before saving.", MsgBoxStyle.Exclamation, "Check Fields")
            Return
        End If

        Dim midName = If(HasPlaceholderText(txtMiddlename) OrElse String.IsNullOrWhiteSpace(txtMiddlename.Text), "N/A", txtMiddlename.Text.Trim().ToUpper())
        Dim mobileNum = If(HasPlaceholderText(txtMobileNumber) OrElse String.IsNullOrWhiteSpace(txtMobileNumber.Text), "N/A", txtMobileNumber.Text.Trim())
        Dim emailAddr = If(HasPlaceholderText(txtEmail) OrElse String.IsNullOrWhiteSpace(txtEmail.Text), "N/A", txtEmail.Text.Trim().ToLower())
        Dim usernameVal = If(Not pnlaccountsystem.Enabled OrElse HasPlaceholderText(txtUsername) OrElse String.IsNullOrWhiteSpace(txtUsername.Text), DBNull.Value, txtUsername.Text.Trim())
        Dim passwordVal = If(Not pnlaccountsystem.Enabled OrElse HasPlaceholderText(txtPassword) OrElse String.IsNullOrWhiteSpace(txtPassword.Text), DBNull.Value, txtPassword.Text.Trim())
        Dim suffixVal = If(cboSuffix.SelectedIndex = -1 OrElse cboSuffix.Text = PH_SUFFIX OrElse String.IsNullOrWhiteSpace(cboSuffix.Text) OrElse cboSuffix.Text = "N/A", "N/A", cboSuffix.Text.Trim().ToUpper())
        Dim fatherVal = If(HasPlaceholderText(txtFatherName) OrElse String.IsNullOrWhiteSpace(txtFatherName.Text), "N/A", txtFatherName.Text.Trim().ToUpper())
        Dim motherVal = If(HasPlaceholderText(txtMotherName) OrElse String.IsNullOrWhiteSpace(txtMotherName.Text), "N/A", txtMotherName.Text.Trim().ToUpper())

        Dim satOfficeVal = If(cboSatelliteOffice.SelectedIndex = -1 OrElse cboSatelliteOffice.Text = PH_SATELLITE, "", cboSatelliteOffice.Text.Trim())

        ' Awtomatikong kinukuha ang ID ng satellite office base sa napiling pangalan sa ComboBox
        Dim satelliteOfficeIDVal As Integer = GetSatelliteOfficeID(satOfficeVal)

        Dim fullName = $"{txtLastname.Text.Trim().ToUpper()}, {txtFirstname.Text.Trim().ToUpper()}{If(midName = "N/A", "", $" {midName}")}{If(suffixVal = "N/A", "", $" {suffixVal}")}"
        Dim fullAddress = $"{txtStreetAddress.Text.Trim().ToUpper()}, {txtBarangay.Text.Trim().ToUpper()}, {txtCity.Text.Trim().ToUpper()}"
        Dim civilVal = If(cboCivilStatus.SelectedIndex = -1 OrElse cboCivilStatus.Text = PH_CIVIL, "", cboCivilStatus.Text.ToUpper())
        Dim genderVal = If(cboGender.SelectedIndex = -1 OrElse cboGender.Text = PH_GENDER, "", cboGender.Text.ToUpper())

        If String.IsNullOrWhiteSpace(civilVal) Then
            MsgBox("Please select Civil Status *", MsgBoxStyle.Exclamation)
            Return
        End If
        If String.IsNullOrWhiteSpace(genderVal) Then
            MsgBox("Please select Gender *", MsgBoxStyle.Exclamation)
            Return
        End If

        If pnlaccountsystem.Enabled Then
            If String.IsNullOrWhiteSpace(txtUsername.Text) OrElse HasPlaceholderText(txtUsername) Then
                MsgBox("Username is required since Email is provided", MsgBoxStyle.Exclamation)
                Return
            End If
            If String.IsNullOrWhiteSpace(txtPassword.Text) OrElse HasPlaceholderText(txtPassword) Then
                MsgBox("Password is required since Email is provided", MsgBoxStyle.Exclamation)
                Return
            End If
            If txtPassword.Text <> txtConfirmPassword.Text Then
                MsgBox("Passwords do not match", MsgBoxStyle.Exclamation)
                Return
            End If
        End If

        Dim adminDeptID As Integer = 1
        Dim natVal = "FILIPINO"
        Dim accStatVal = "Active"

        Dim freshResidentCode As String = ""
        If editResidentID = 0 Then
            freshResidentCode = GetNextResidentCode()
        End If

        Try
            connection()

            If emailAddr <> "N/A" Then
                Dim eCount As Integer
                Using cmdCheck2 As New MySqlCommand("SELECT COUNT(*) FROM residences WHERE LOWER(Email) = LOWER(@email) AND Email <> 'N/A' AND ResidentID <> @currentId", cn)
                    cmdCheck2.Parameters.AddWithValue("@email", emailAddr)
                    cmdCheck2.Parameters.AddWithValue("@currentId", editResidentID)
                    eCount = CInt(cmdCheck2.ExecuteScalar())
                End Using
                If eCount > 0 Then
                    MsgBox("Email already registered!", MsgBoxStyle.Critical)
                    CloseConnection()
                    Return
                End If
            End If

            If mobileNum <> "N/A" Then
                Dim mCount As Integer
                Using cmdCheck3 As New MySqlCommand("SELECT COUNT(*) FROM residences WHERE MobileNumber = @mobile AND MobileNumber <> 'N/A' AND ResidentID <> @currentId", cn)
                    cmdCheck3.Parameters.AddWithValue("@mobile", mobileNum)
                    cmdCheck3.Parameters.AddWithValue("@currentId", editResidentID)
                    mCount = CInt(cmdCheck3.ExecuteScalar())
                End Using
                If mCount > 0 Then
                    MsgBox("Mobile number already registered!", MsgBoxStyle.Critical)
                    CloseConnection()
                    Return
                End If
            End If

            If pnlaccountsystem.Enabled AndAlso usernameVal IsNot DBNull.Value AndAlso usernameVal.ToString() <> "N/A" Then
                If usernameVal.ToString().IndexOf("admin", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    MsgBox("Username cannot contain 'admin'!", MsgBoxStyle.Critical)
                    CloseConnection()
                    Return
                End If

                Dim uCount As Integer
                Using cmdCheckUser As New MySqlCommand("SELECT COUNT(*) FROM residences WHERE LOWER(Username) = LOWER(@username) AND ResidentID <> @currentId", cn)
                    cmdCheckUser.Parameters.AddWithValue("@username", usernameVal.ToString())
                    cmdCheckUser.Parameters.AddWithValue("@currentId", editResidentID)
                    uCount = CInt(cmdCheckUser.ExecuteScalar())
                End Using

                If uCount > 0 Then
                    MsgBox("Username already registered to another user!", MsgBoxStyle.Critical)
                    CloseConnection()
                    Return
                End If
            End If

            Try
                Using cmd As New MySqlCommand("SELECT AdminID FROM admin WHERE FullName=@adminname", cn)
                    cmd.Parameters.AddWithValue("@adminname", LoggedFullname)
                    Using dr = cmd.ExecuteReader()
                        If dr.Read() Then adminDeptID = CInt(dr("AdminID"))
                    End Using
                End Using
            Catch
                adminDeptID = 1
            End Try

            If editResidentID > 0 Then
                Dim sqlUpdate = "UPDATE residences SET Lastname=@lname, Firstname=@fname, Middlename=@mname, Suffix=@suffix, FullName=@fullname, Address=@address, Birthday=@bday, BirthPlace=@bplace, CivilStatus=@cstatus, Gender=@gender, MobileNumber=@mobile, Email=@email, Nationality=@nat, AccountStatus=@accstat, FatherName=@father, MotherName=@mother, DepartmentID=@deptid, Username=@username, Password=@password, SatelliteOffice=@satoffice, SatelliteOfficeID=@satid"

                If profileImageBytes IsNot Nothing Then sqlUpdate &= ", Picture=@pic"
                If idFrontBytes IsNot Nothing Then sqlUpdate &= ", IdentificationFront=@idfront"
                If idBackBytes IsNot Nothing Then sqlUpdate &= ", IdentificationBack=@idback"

                sqlUpdate &= " WHERE ResidentID=@resid"

                Using cmd As New MySqlCommand(sqlUpdate, cn)
                    cmd.Parameters.AddWithValue("@resid", editResidentID)
                    cmd.Parameters.AddWithValue("@lname", txtLastname.Text.Trim().ToUpper())
                    cmd.Parameters.AddWithValue("@fname", txtFirstname.Text.Trim().ToUpper())
                    cmd.Parameters.AddWithValue("@mname", midName)
                    cmd.Parameters.AddWithValue("@suffix", suffixVal)
                    cmd.Parameters.AddWithValue("@fullname", fullName)
                    cmd.Parameters.AddWithValue("@address", fullAddress)
                    cmd.Parameters.AddWithValue("@bday", dtpBirthday.Value.ToString("yyyy-MM-dd"))
                    cmd.Parameters.AddWithValue("@bplace", txtBirthPlace.Text.Trim().ToUpper())
                    cmd.Parameters.AddWithValue("@cstatus", civilVal)
                    cmd.Parameters.AddWithValue("@gender", genderVal)
                    cmd.Parameters.AddWithValue("@mobile", mobileNum)
                    cmd.Parameters.AddWithValue("@email", emailAddr)
                    cmd.Parameters.AddWithValue("@nat", natVal)
                    cmd.Parameters.AddWithValue("@accstat", accStatVal)
                    cmd.Parameters.AddWithValue("@father", fatherVal)
                    cmd.Parameters.AddWithValue("@mother", motherVal)
                    cmd.Parameters.AddWithValue("@deptid", adminDeptID)
                    cmd.Parameters.AddWithValue("@username", usernameVal)
                    cmd.Parameters.AddWithValue("@password", passwordVal)
                    cmd.Parameters.AddWithValue("@satoffice", satOfficeVal)
                    cmd.Parameters.AddWithValue("@satid", satelliteOfficeIDVal)

                    If profileImageBytes IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@pic", profileImageBytes)
                    End If
                    If idFrontBytes IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@idfront", idFrontBytes)
                    End If
                    If idBackBytes IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@idback", idBackBytes)
                    End If

                    cmd.ExecuteNonQuery()
                End Using

                CloseConnection()
                WriteActivityLog("UPDATE_RESIDENT", $"Updated: {fullName}", editResidentID)
                MsgBox("Record updated successfully!", MsgBoxStyle.Information)

                ReturnToRecordList()

            Else
                Dim sqlInsert = "INSERT INTO residences (ResidentCode, Lastname, Firstname, Middlename, Suffix, FullName, Address, Birthday, BirthPlace, CivilStatus, Gender, MobileNumber, Email, Nationality, FatherName, MotherName, AccountStatus, DepartmentID, Username, Password, SatelliteOffice, SatelliteOfficeID, Picture, IdentificationFront, IdentificationBack) VALUES (@rcode, @lname, @fname, @mname, @suffix, @fullname, @address, @bday, @bplace, @cstatus, @gender, @mobile, @email, @nat, @father, @mother, @accstat, @deptid, @username, @password, @satoffice, @satid, @pic, @idfront, @idback)"

                Using cmd As New MySqlCommand(sqlInsert, cn)
                    cmd.Parameters.AddWithValue("@rcode", freshResidentCode)
                    cmd.Parameters.AddWithValue("@deptid", adminDeptID)
                    cmd.Parameters.AddWithValue("@lname", txtLastname.Text.Trim().ToUpper())
                    cmd.Parameters.AddWithValue("@fname", txtFirstname.Text.Trim().ToUpper())
                    cmd.Parameters.AddWithValue("@mname", midName)
                    cmd.Parameters.AddWithValue("@suffix", suffixVal)
                    cmd.Parameters.AddWithValue("@fullname", fullName)
                    cmd.Parameters.AddWithValue("@address", fullAddress)
                    cmd.Parameters.AddWithValue("@bday", dtpBirthday.Value.ToString("yyyy-MM-dd"))
                    cmd.Parameters.AddWithValue("@bplace", txtBirthPlace.Text.Trim().ToUpper())
                    cmd.Parameters.AddWithValue("@cstatus", civilVal)
                    cmd.Parameters.AddWithValue("@gender", genderVal)
                    cmd.Parameters.AddWithValue("@mobile", mobileNum)
                    cmd.Parameters.AddWithValue("@email", emailAddr)
                    cmd.Parameters.AddWithValue("@nat", natVal)
                    cmd.Parameters.AddWithValue("@father", fatherVal)
                    cmd.Parameters.AddWithValue("@mother", motherVal)
                    cmd.Parameters.AddWithValue("@accstat", accStatVal)
                    cmd.Parameters.AddWithValue("@username", usernameVal)
                    cmd.Parameters.AddWithValue("@password", passwordVal)
                    cmd.Parameters.AddWithValue("@satoffice", satOfficeVal)
                    cmd.Parameters.AddWithValue("@satid", satelliteOfficeIDVal)

                    If profileImageBytes IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@pic", profileImageBytes)
                    Else
                        cmd.Parameters.AddWithValue("@pic", DBNull.Value)
                    End If
                    If idFrontBytes IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@idfront", idFrontBytes)
                    Else
                        cmd.Parameters.AddWithValue("@idfront", DBNull.Value)
                    End If
                    If idBackBytes IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@idback", idBackBytes)
                    Else
                        cmd.Parameters.AddWithValue("@idback", DBNull.Value)
                    End If

                    cmd.ExecuteNonQuery()
                    Dim newResidentId = CInt(cmd.LastInsertedId)
                    CloseConnection()
                    WriteActivityLog("ADD_RESIDENT", $"Added: {fullName} | Code: {freshResidentCode}", newResidentId)
                    MsgBox("Registered successfully!" & vbCrLf & $"Code: {freshResidentCode}", MsgBoxStyle.Information)
                End Using

                ReturnToRecordList()
            End If

        Catch ex As MySqlException
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then CloseConnection()
            MsgBox(If(ex.Number = 1062, "Duplicate entry detected.", "Database Error: " & ex.Message), MsgBoxStyle.Critical)
        Catch ex As Exception
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then CloseConnection()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ClearForm()
        txtLastname.Clear()
        txtFirstname.Clear()
        txtMiddlename.Clear()
        cboSuffix.SelectedIndex = -1
        txtStreetAddress.Clear()
        dtpBirthday.Value = DateTime.Now
        txtBirthPlace.Clear()
        cboCivilStatus.SelectedIndex = -1
        cboGender.SelectedIndex = -1
        cboSatelliteOffice.SelectedIndex = -1
        txtMobileNumber.Clear()
        txtEmail.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        txtConfirmPassword.Clear()
        txtFatherName.Clear()
        txtMotherName.Clear()
        picUser.Image = Nothing
        picIDFront.Image = Nothing
        picIDBack.Image = Nothing
        profileImageBytes = Nothing
        idFrontBytes = Nothing
        idBackBytes = Nothing
        pnlaccountsystem.Enabled = False
        pnlaccountsystem.Visible = True
        txtPassword.PasswordChar = ControlChars.NullChar
        txtConfirmPassword.PasswordChar = ControlChars.NullChar
        ClearAllValidationLabels()
        SetAllPlaceholders()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to Cancel?", vbYesNo + MsgBoxStyle.Question, "Confirm Cancel") = MsgBoxResult.Yes Then
            ReturnToRecordList()
        End If
    End Sub
End Class