Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Text.RegularExpressions
Imports System.Net

Public Class Barangay_Residences
    Private profileImageBytes As Byte() = Nothing
    Private editResidentID As Integer = 0

    Public Sub New()
        InitializeComponent()
        editResidentID = 0
    End Sub

    Public Sub New(ByVal residentID As Integer)
        InitializeComponent()
        editResidentID = residentID
    End Sub

    Private Sub Barangay_Residences_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboSuffix.Items.AddRange({"N/A", "JR.", "SR.", "II", "III", "IV", "V"})
        cboSuffix.SelectedIndex = 0
        cboCivilStatus.Items.AddRange({"SINGLE", "MARRIED", "WIDOWED", "SEPARATED", "DIVORCED"})
        cboCivilStatus.SelectedIndex = 0
        cboGender.Items.AddRange({"MALE", "FEMALE", "OTHER"})
        cboGender.SelectedIndex = 0
        txtBarangay.Text = "PUTATAN"
        txtBarangay.ReadOnly = True
        txtCity.Text = "MUNTINLUPA CITY"
        txtCity.ReadOnly = True
        ClearAllValidationLabels()

        If editResidentID > 0 Then
            btnSubmit.Text = "Update Record"
            LoadResidentDataForEdit()
        Else
            btnSubmit.Text = "Register Resident"
            GenerateResidentCode()
        End If
    End Sub

    Private Sub LoadResidentDataForEdit()
        Dim rCode As String = "", lName As String = "", fName As String = "", mName As String = ""
        Dim sfx As String = "", bPlace As String = "", cStatus As String = "", gen As String = ""
        Dim mob As String = "", eml As String = "", addr As String = ""
        Dim bDay As DateTime = DateTime.Now
        Dim hasBday As Boolean = False
        Dim picBytes As Byte() = Nothing

        Try
            connection()
            sql = "SELECT * FROM residences WHERE ResidentID = @resid"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@resid", editResidentID)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                rCode = dr("ResidentCode").ToString()
                lName = dr("Lastname").ToString()
                fName = dr("Firstname").ToString()
                mName = dr("Middlename").ToString()
                sfx = dr("Suffix").ToString()
                bPlace = dr("BirthPlace").ToString()
                cStatus = dr("CivilStatus").ToString()
                gen = dr("Gender").ToString()
                mob = dr("MobileNumber").ToString()
                eml = dr("Email").ToString()
                addr = dr("Address").ToString()

                If Not IsDBNull(dr("Birthday")) Then
                    bDay = Convert.ToDateTime(dr("Birthday"))
                    hasBday = True
                End If
                If Not IsDBNull(dr("Picture")) Then
                    picBytes = CType(dr("Picture"), Byte())
                End If
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox("Error loading data: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try

        lblStaffCode.Text = rCode
        txtLastname.Text = lName
        txtFirstname.Text = fName
        txtMiddlename.Text = If(mName = "N/A", "", mName)
        If cboSuffix.Items.Contains(sfx) Then
            cboSuffix.Text = sfx
        Else
            cboSuffix.Text = "N/A"
        End If
        txtBirthPlace.Text = bPlace
        cboCivilStatus.Text = cStatus
        cboGender.Text = gen
        txtMobileNumber.Text = If(mob = "N/A", "", mob)
        txtEmail.Text = If(eml = "n/a", "", eml)

        If hasBday Then
            dtpBirthday.Value = bDay
        End If

        Dim addrParts As String() = addr.Split(New String() {","}, StringSplitOptions.None)
        If addrParts.Length > 0 Then
            txtStreetAddress.Text = addrParts(0).Trim()
        End If

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

    Private Sub ConvertToUpperCase_KeyPress(sender As Object, e As KeyPressEventArgs) Handles _
        txtLastname.KeyPress, txtFirstname.KeyPress, txtMiddlename.KeyPress,
        txtStreetAddress.KeyPress, txtBirthPlace.KeyPress

        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub ClearAllValidationLabels()
        SetFeedbackLabel(lblLastnameError, "", False)
        SetFeedbackLabel(lblFirstnameError, "", False)
        SetFeedbackLabel(lblMiddlenameError, "", False)
        SetFeedbackLabel(lblStreetAddressError, "", False)
        SetFeedbackLabel(lblBirthPlaceError, "", False)
        SetFeedbackLabel(lblMobileError, "", False)
        SetFeedbackLabel(lblEmailError, "", False)
        SetFeedbackLabel(lblPictureError, "", False)
    End Sub

    Private Sub SetFeedbackLabel(lbl As Label, message As String, isError As Boolean)
        If lbl Is Nothing Then Exit Sub
        lbl.Text = message
        If String.IsNullOrEmpty(message) Then
            lbl.ForeColor = Color.Black
        ElseIf isError Then
            lbl.ForeColor = Color.Red
        Else
            lbl.ForeColor = Color.Green
        End If
    End Sub

    Private Function GetNextResidentCode() As String
        Dim nextCode As String = "RES-001"
        Try
            connection()
            sql = "SELECT ResidentCode FROM residences WHERE ResidentCode LIKE 'RES-%' ORDER BY ResidentID DESC LIMIT 1"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                Dim lastCode As String = dr("ResidentCode").ToString()
                If lastCode.Contains("-") Then
                    Dim parts() As String = lastCode.Split("-"c)
                    Dim numericPart As Integer
                    If Integer.TryParse(parts(1), numericPart) Then
                        nextCode = "RES-" & (numericPart + 1).ToString("D3")
                    End If
                End If
            End If
            dr.Close()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
        Return nextCode
    End Function

    Private Sub GenerateResidentCode()
        Dim code As String = GetNextResidentCode()
        If lblStaffCode IsNot Nothing Then
            lblStaffCode.Text = code
        End If
    End Sub

    Private Sub txtMobileNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMobileNumber.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtLastname_TextChanged(sender As Object, e As EventArgs) Handles txtLastname.TextChanged
        If String.IsNullOrWhiteSpace(txtLastname.Text) Then
            SetFeedbackLabel(lblLastnameError, "Lastname is required.", True)
        ElseIf Not Regex.IsMatch(txtLastname.Text.Trim(), "^[a-zA-ZñÑ\s]+$") Then
            SetFeedbackLabel(lblLastnameError, "Letters only (no numbers/symbols).", True)
        Else
            SetFeedbackLabel(lblLastnameError, "OK", False)
        End If
    End Sub

    Private Sub txtFirstname_TextChanged(sender As Object, e As EventArgs) Handles txtFirstname.TextChanged
        If String.IsNullOrWhiteSpace(txtFirstname.Text) Then
            SetFeedbackLabel(lblFirstnameError, "Firstname is required.", True)
        ElseIf Not Regex.IsMatch(txtFirstname.Text.Trim(), "^[a-zA-ZñÑ\s]+$") Then
            SetFeedbackLabel(lblFirstnameError, "Letters only (no numbers/symbols).", True)
        Else
            SetFeedbackLabel(lblFirstnameError, "OK", False)
        End If
    End Sub

    Private Sub txtMiddlename_TextChanged(sender As Object, e As EventArgs) Handles txtMiddlename.TextChanged
        If String.IsNullOrWhiteSpace(txtMiddlename.Text) Then
            SetFeedbackLabel(lblMiddlenameError, "Optional (will be saved as N/A)", False)
        ElseIf Not Regex.IsMatch(txtMiddlename.Text.Trim(), "^[a-zA-ZñÑ\s]+$") Then
            SetFeedbackLabel(lblMiddlenameError, "Letters only.", True)
        Else
            SetFeedbackLabel(lblMiddlenameError, "OK", False)
        End If
    End Sub

    Private Sub txtStreetAddress_TextChanged(sender As Object, e As EventArgs) Handles txtStreetAddress.TextChanged
        If String.IsNullOrWhiteSpace(txtStreetAddress.Text) Then
            SetFeedbackLabel(lblStreetAddressError, "Street address is required.", True)
        Else
            SetFeedbackLabel(lblStreetAddressError, "OK", False)
        End If
    End Sub

    Private Sub txtBirthPlace_TextChanged(sender As Object, e As EventArgs) Handles txtBirthPlace.TextChanged
        If String.IsNullOrWhiteSpace(txtBirthPlace.Text) Then
            SetFeedbackLabel(lblBirthPlaceError, "Birth place is required.", True)
        Else
            SetFeedbackLabel(lblBirthPlaceError, "OK", False)
        End If
    End Sub

    Private Sub txtMobileNumber_TextChanged(sender As Object, e As EventArgs) Handles txtMobileNumber.TextChanged
        Dim val As String = txtMobileNumber.Text.Trim()
        If String.IsNullOrWhiteSpace(val) Then
            SetFeedbackLabel(lblMobileError, "Optional (will be saved as N/A)", False)
            Exit Sub
        End If
        If Not val.StartsWith("09") Then
            SetFeedbackLabel(lblMobileError, "Must start with 09.", True)
            Exit Sub
        End If
        If val.Length <> 11 Then
            SetFeedbackLabel(lblMobileError, "Must be exactly 11 digits.", True)
            Exit Sub
        End If

        Try
            connection()
            Dim count As Integer = 0
            sql = "SELECT COUNT(*) FROM residences WHERE MobileNumber = @mobile AND MobileNumber <> 'N/A' AND ResidentID <> @currentId"
            Using localCmd As New MySqlCommand(sql, cn)
                localCmd.Parameters.AddWithValue("@mobile", val)
                localCmd.Parameters.AddWithValue("@currentId", editResidentID)
                count = Convert.ToInt32(localCmd.ExecuteScalar())
            End Using
            If count > 0 Then
                SetFeedbackLabel(lblMobileError, "Mobile number already registered!", True)
            Else
                SetFeedbackLabel(lblMobileError, "OK", False)
            End If
        Catch ex As Exception
            SetFeedbackLabel(lblMobileError, "Error verifying mobile number.", True)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
        Dim val As String = txtEmail.Text.Trim().ToLower()
        If String.IsNullOrWhiteSpace(val) Then
            SetFeedbackLabel(lblEmailError, "Optional (will be saved as N/A)", False)
            Exit Sub
        End If

        Dim strictGmailRegex As New Regex("^[a-zA-Z0-9._%+-]+@gmail\.com$", RegexOptions.IgnoreCase)
        If Not strictGmailRegex.IsMatch(val) Then
            SetFeedbackLabel(lblEmailError, "Must end strictly with @gmail.com.", True)
            Exit Sub
        End If

        Try
            connection()
            Dim count As Integer = 0
            sql = "SELECT COUNT(*) FROM residences WHERE LOWER(Email) = LOWER(@email) AND LOWER(Email) <> 'n/a' AND ResidentID <> @currentId"
            Using localCmd As New MySqlCommand(sql, cn)
                localCmd.Parameters.AddWithValue("@email", val)
                localCmd.Parameters.AddWithValue("@currentId", editResidentID)
                count = Convert.ToInt32(localCmd.ExecuteScalar())
            End Using
            If count > 0 Then
                SetFeedbackLabel(lblEmailError, "Email address already registered!", True)
            Else
                SetFeedbackLabel(lblEmailError, "OK", False)
            End If
        Catch ex As Exception
            SetFeedbackLabel(lblEmailError, "Error verifying email address.", True)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub picUser_DoubleClick(sender As Object, e As EventArgs) Handles picUser.DoubleClick
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            If ofd.ShowDialog() = DialogResult.OK Then
                picUser.SizeMode = PictureBoxSizeMode.StretchImage
                picUser.Image = Image.FromFile(ofd.FileName)
                Using ms As New MemoryStream()
                    picUser.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                    profileImageBytes = ms.ToArray()
                End Using
                SetFeedbackLabel(lblPictureError, "Uploaded", False)
            End If
        End Using
    End Sub

    ' ✅ TUGMA NA SA TABLE STRUCTURE MO — walang mali sa column names
    Private Sub WriteActivityLog(actionType As String, details As String, residentId As Integer)
        Try
            connection()
            Dim sqlLog As String = "INSERT INTO activity_logs " &
                "(ActionType, Details, FullName, UserRole, Module, IPAddress, DeviceInfo, UserID) " &
                "VALUES (@actType, @det, @fullname, @role, @module, @ip, @dev, @usrid)"

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
        Catch ex As Exception
            MsgBox("Log Save Error: " & ex.Message, MsgBoxStyle.Information)
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
            SetFeedbackLabel(lblPictureError, "Picture is required.", True)
        End If

        If lblLastnameError.ForeColor = Color.Red OrElse lblFirstnameError.ForeColor = Color.Red OrElse
           lblMiddlenameError.ForeColor = Color.Red OrElse lblStreetAddressError.ForeColor = Color.Red OrElse
           lblBirthPlaceError.ForeColor = Color.Red OrElse lblMobileError.ForeColor = Color.Red OrElse
           lblEmailError.ForeColor = Color.Red OrElse lblPictureError.ForeColor = Color.Red Then
            MsgBox("Please resolve all validation errors and ensure a Profile Picture is set before saving!", MsgBoxStyle.Exclamation, "Validation Error")
            Return
        End If

        Dim midName As String = If(String.IsNullOrWhiteSpace(txtMiddlename.Text), "N/A", txtMiddlename.Text.Trim().ToUpper())
        Dim mobileNum As String = If(String.IsNullOrWhiteSpace(txtMobileNumber.Text), "N/A", txtMobileNumber.Text.Trim())
        Dim emailAddr As String = If(String.IsNullOrWhiteSpace(txtEmail.Text), "n/a", txtEmail.Text.Trim().ToLower())
        Dim suffixVal As String = If(cboSuffix.Text = "N/A", "", " " & cboSuffix.Text.Trim().ToUpper())
        Dim middleVal As String = If(midName = "N/A", "", " " & midName)
        Dim fullName As String = $"{txtLastname.Text.Trim().ToUpper()}, {txtFirstname.Text.Trim().ToUpper()}{middleVal}{suffixVal}"
        Dim fullAddress As String = $"{txtStreetAddress.Text.Trim().ToUpper()}, {txtBarangay.Text.Trim().ToUpper()}, {txtCity.Text.Trim().ToUpper()}"
        Dim adminDeptID As Integer = 1

        Try
            connection()

            If emailAddr <> "n/a" Then
                Dim eCount As Integer = 0
                sql = "SELECT COUNT(*) FROM residences WHERE LOWER(Email) = LOWER(@email) AND ResidentID <> @currentId"
                Using cmdCheck2 As New MySqlCommand(sql, cn)
                    cmdCheck2.Parameters.AddWithValue("@email", emailAddr)
                    cmdCheck2.Parameters.AddWithValue("@currentId", editResidentID)
                    eCount = Convert.ToInt32(cmdCheck2.ExecuteScalar())
                End Using
                If eCount > 0 Then
                    MsgBox("Email address is already registered!", MsgBoxStyle.Critical, "Duplicate Error")
                    CloseConnection()
                    Return
                End If
            End If

            If mobileNum <> "N/A" Then
                Dim mCount As Integer = 0
                sql = "SELECT COUNT(*) FROM residences WHERE MobileNumber = @mobile AND ResidentID <> @currentId"
                Using cmdCheck3 As New MySqlCommand(sql, cn)
                    cmdCheck3.Parameters.AddWithValue("@mobile", mobileNum)
                    cmdCheck3.Parameters.AddWithValue("@currentId", editResidentID)
                    mCount = Convert.ToInt32(cmdCheck3.ExecuteScalar())
                End Using
                If mCount > 0 Then
                    MsgBox("Mobile number is already registered!", MsgBoxStyle.Critical, "Duplicate Error")
                    CloseConnection()
                    Return
                End If
            End If

            Try
                sql = "SELECT AdminID FROM admin WHERE FullName=@adminname"
                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@adminname", LoggedFullname)
                dr = cmd.ExecuteReader()
                If dr.Read() Then
                    adminDeptID = Convert.ToInt32(dr("AdminID"))
                End If
                dr.Close()
            Catch ex As Exception
                adminDeptID = 1
            End Try

            If editResidentID > 0 Then
                sql = "UPDATE residences SET Lastname=@lname, Firstname=@fname, Middlename=@mname, Suffix=@suffix, " &
                      "FullName=@fullname, Address=@address, Birthday=@bday, BirthPlace=@bplace, CivilStatus=@cstatus, " &
                      "Gender=@gender, MobileNumber=@mobile, Email=@email"
                If profileImageBytes IsNot Nothing Then
                    sql &= ", Picture=@pic"
                End If
                sql &= " WHERE ResidentID=@resid"

                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@resid", editResidentID)
                cmd.Parameters.AddWithValue("@lname", txtLastname.Text.Trim().ToUpper())
                cmd.Parameters.AddWithValue("@fname", txtFirstname.Text.Trim().ToUpper())
                cmd.Parameters.AddWithValue("@mname", midName)
                cmd.Parameters.AddWithValue("@suffix", cboSuffix.Text.Trim().ToUpper())
                cmd.Parameters.AddWithValue("@fullname", fullName)
                cmd.Parameters.AddWithValue("@address", fullAddress)
                cmd.Parameters.AddWithValue("@bday", dtpBirthday.Value.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@bplace", txtBirthPlace.Text.Trim().ToUpper())
                cmd.Parameters.AddWithValue("@cstatus", cboCivilStatus.Text.ToUpper())
                cmd.Parameters.AddWithValue("@gender", cboGender.Text.ToUpper())
                cmd.Parameters.AddWithValue("@mobile", mobileNum)
                cmd.Parameters.AddWithValue("@email", emailAddr)
                If profileImageBytes IsNot Nothing Then
                    cmd.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = profileImageBytes
                End If

                cmd.ExecuteNonQuery()
                CloseConnection()
                WriteActivityLog("UPDATE_RESIDENT", "Updated resident: " & fullName, editResidentID)
                MsgBox("Resident record successfully updated!", MsgBoxStyle.Information, "Updated")
                Me.Close()
            Else
                Dim freshResidentCode As String = GetNextResidentCode()
                connection()
                sql = "INSERT INTO residences (ResidentCode, Lastname, Firstname, Middlename, Suffix, FullName, Address, Birthday, BirthPlace, CivilStatus, Gender, MobileNumber, Email, Nationality, Picture, AccountStatus, DepartmentID) " &
                       "VALUES (@rcode, @lname, @fname, @mname, @suffix, @fullname, @address, @bday, @bplace, @cstatus, @gender, @mobile, @email, 'FILIPINO', @pic, 'Active', @deptid)"

                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@rcode", freshResidentCode)
                cmd.Parameters.AddWithValue("@deptid", adminDeptID)
                cmd.Parameters.AddWithValue("@lname", txtLastname.Text.Trim().ToUpper())
                cmd.Parameters.AddWithValue("@fname", txtFirstname.Text.Trim().ToUpper())
                cmd.Parameters.AddWithValue("@mname", midName)
                cmd.Parameters.AddWithValue("@suffix", cboSuffix.Text.Trim().ToUpper())
                cmd.Parameters.AddWithValue("@fullname", fullName)
                cmd.Parameters.AddWithValue("@address", fullAddress)
                cmd.Parameters.AddWithValue("@bday", dtpBirthday.Value.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@bplace", txtBirthPlace.Text.Trim().ToUpper())
                cmd.Parameters.AddWithValue("@cstatus", cboCivilStatus.Text.ToUpper())
                cmd.Parameters.AddWithValue("@gender", cboGender.Text.ToUpper())
                cmd.Parameters.AddWithValue("@mobile", mobileNum)
                cmd.Parameters.AddWithValue("@email", emailAddr)
                If profileImageBytes IsNot Nothing Then
                    cmd.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = profileImageBytes
                Else
                    cmd.Parameters.Add("@pic", MySqlDbType.LongBlob).Value = DBNull.Value
                End If

                cmd.ExecuteNonQuery()
                Dim newResidentId As Integer = CInt(cmd.LastInsertedId)
                CloseConnection()
                WriteActivityLog("ADD_RESIDENT", "Added resident: " & fullName & " | Code: " & freshResidentCode, newResidentId)
                MsgBox("Successfully Registered New Resident!", MsgBoxStyle.Information, "Success")
                ClearForm()
                GenerateResidentCode()
            End If
        Catch ex As MySqlException
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then CloseConnection()
            If ex.Number = 1062 Then
                MsgBox("Database constraint conflict (Error 1062): " & ex.Message, MsgBoxStyle.Critical, "MySQL Index Conflict")
            Else
                MsgBox("Database Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then CloseConnection()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ClearForm()
        txtLastname.Clear()
        txtFirstname.Clear()
        txtMiddlename.Clear()
        cboSuffix.SelectedIndex = 0
        txtStreetAddress.Clear()
        dtpBirthday.Value = DateTime.Now
        txtBirthPlace.Clear()
        cboCivilStatus.SelectedIndex = 0
        cboGender.SelectedIndex = 0
        txtMobileNumber.Clear()
        txtEmail.Clear()
        picUser.Image = Nothing
        profileImageBytes = Nothing
        ClearAllValidationLabels()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to Cancel?", vbYesNo + MsgBoxStyle.Question, "Confirm Cancel") = MsgBoxResult.Yes Then
            ClearForm()
            Me.Hide()
        End If
    End Sub
End Class