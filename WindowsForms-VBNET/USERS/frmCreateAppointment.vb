Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Runtime.InteropServices

Public Class frmCreateAppointment
    Private selectedResidentID As Integer = 0
    Private selectedResidentAddress As String = ""
    Private _skipClosePrompt As Boolean = False
    Private selectedRepresentativeID As Integer = 0

    Public Class ServiceItem
        Public Property ServiceName As String
        Public Property DepartmentName As String
        Public Overrides Function ToString() As String
            Return ServiceName
        End Function
    End Class

    Private Sub frmCreateAppointment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboRequestFor.Items.Clear()
        cboRequestFor.Items.AddRange({
            "Self",
            "Family Member / Relative",
            "Representative / On Behalf"
        })
        cboRequestFor.SelectedIndex = 0
        LoadDocumentServices()
        lblControlNo.Text = GenerateControlNumber()

        ' Kung i-l-load pa rin ang logged in user, tatawagin ito
        LoadLoggedUserDefault()

        cboRequestType.DropDownStyle = ComboBoxStyle.DropDown
        CueBanner.SetText(txtNameOfRepresentative, "Representative's full name")
        CueBanner.SetText(txtPurpose, "State the purpose of your appointment")
        CueBanner.SetText(cboRequestType, "Select Request Type / Document Service")

        ToggleRepresentativeFields(False)
    End Sub

    Private Sub frmCreateAppointment_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If _skipClosePrompt Then Return

        ' Gumamit ako ng txtFirstname at txtLastname base sa bago mong layout
        If Not String.IsNullOrWhiteSpace(txtFirstname.Text) OrElse Not String.IsNullOrWhiteSpace(txtLastname.Text) Then
            Dim result As DialogResult = MsgBox($"Are you sure you want to cancel the appointment for {txtFirstname.Text.Trim()}?",
                                                 MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Cancel")
            If result = DialogResult.Yes Then
                SaveAppointment("PENDING")
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    ' === NILAGYAN NG HANDLES PARA GUMANA ANG TOGGLE ===
    Private Sub cboRequestFor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRequestFor.SelectedIndexChanged
        Dim selectedOption As String = cboRequestFor.Text.Trim()
        If selectedOption = "Family Member / Relative" OrElse selectedOption = "Representative / On Behalf" Then
            ToggleRepresentativeFields(True)
        Else
            ToggleRepresentativeFields(False)
            txtNameOfRepresentative.Clear()
            selectedRepresentativeID = 0
        End If
    End Sub

    ' === HIDE / SHOW REPRESENTATIVE FIELDS ===
    Private Sub ToggleRepresentativeFields(isVisible As Boolean)
        If txtNameOfRepresentative IsNot Nothing Then
            txtNameOfRepresentative.Visible = isVisible
        End If
        ' Siguraduhing may lblRepresentative (o kung ano man ang pangalan ng label mo) sa designer
        If lblRepresentative IsNot Nothing Then
            lblRepresentative.Visible = isVisible
        End If
    End Sub

    Private Sub LoadDocumentServices()
        cboRequestType.Items.Clear()
        Try
            connection()
            sql = "SELECT ds.ServiceName, d.DepartmentName " &
                  "FROM document_services ds " &
                  "LEFT JOIN departments d ON ds.DepartmentID = d.DepartmentID " &
                  "WHERE (ds.IsActive = 1 OR ds.IsActive IS NULL) " &
                  "ORDER BY ds.ServiceName ASC"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                Dim item As New ServiceItem With {
                    .ServiceName = dr("ServiceName").ToString(),
                    .DepartmentName = If(IsDBNull(dr("DepartmentName")), "", dr("DepartmentName").ToString())
                }
                cboRequestType.Items.Add(item)
            End While
            dr.Close()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Function GetSelectedDepartment() As String
        If cboRequestType.SelectedItem IsNot Nothing AndAlso TypeOf cboRequestType.SelectedItem Is ServiceItem Then
            Return CType(cboRequestType.SelectedItem, ServiceItem).DepartmentName
        End If
        Dim typedText As String = cboRequestType.Text.Trim()
        For Each itm As Object In cboRequestType.Items
            If TypeOf itm Is ServiceItem AndAlso
               CType(itm, ServiceItem).ServiceName.Equals(typedText, StringComparison.OrdinalIgnoreCase) Then
                Return CType(itm, ServiceItem).DepartmentName
            End If
        Next
        Return ""
    End Function

    Private Function GenerateControlNumber() As String
        Dim newCtrlNo As String = "APP-001"
        Try
            connection()
            sql = "SELECT ControlNo FROM appointments ORDER BY AppointmentID DESC LIMIT 1"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                Dim lastCtrl As String = dr("ControlNo").ToString()
                Dim numPart As Integer = Convert.ToInt32(lastCtrl.Replace("APP-", ""))
                newCtrlNo = $"APP-{(numPart + 1):D3}"
            End If
            dr.Close()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
        Return newCtrlNo
    End Function

    ' === AUTO-FILL DEFAULT LOGGED IN USER ===
    Private Sub LoadLoggedUserDefault()
        If String.IsNullOrEmpty(LoggedFullname) Then Return
        Try
            connection()
            sql = "SELECT ResidentID FROM residences WHERE FullName=@name OR Username=@name"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@name", LoggedFullname)
            Dim resIDObj = cmd.ExecuteScalar()

            If resIDObj IsNot Nothing AndAlso Not IsDBNull(resIDObj) Then
                selectedResidentID = Convert.ToInt32(resIDObj)
                LoadResidentData(selectedResidentID) ' I-load sa mga bagong textboxes
            End If
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    ' =================================================================================
    ' === BUTTON CLICK PARA PUMILI NG RESIDENT ===
    ' =================================================================================
    Private Sub btnSelectResident_Click(sender As Object, e As EventArgs) Handles btnSelectResident.Click
        ' Buksan ang frmResidencelist bilang Dialog
        Using frmLookup As New frmResidencelist()
            If frmLookup.ShowDialog() = DialogResult.OK Then
                ' Makuha ang piniling ID at i-autofill ang textboxes
                Dim resID As Integer = frmLookup.SelectedResidentID
                LoadResidentData(resID)
            End If
        End Using
    End Sub

    Private Sub LoadResidentData(resID As Integer)
        Try
            connection()
            sql = "SELECT * FROM residences WHERE ResidentID = @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", resID)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                selectedResidentID = resID

                ' I-map ang data sa mga textboxes
                txtLastname.Text = dr("Lastname").ToString()
                txtFirstname.Text = dr("Firstname").ToString()
                txtMiddle.Text = If(IsDBNull(dr("Middlename")), "", dr("Middlename").ToString())
                cboSuffix.Text = If(IsDBNull(dr("Suffix")), "", dr("Suffix").ToString())

                ' Ginamit ang "Birthday" base sa table schema
                If Not IsDBNull(dr("Birthday")) Then
                    dtpDateOfBirth.Value = Convert.ToDateTime(dr("Birthday"))
                End If

                txtBirthPlace.Text = If(IsDBNull(dr("BirthPlace")), "", dr("BirthPlace").ToString())
                cboGender.Text = If(IsDBNull(dr("Gender")), "", dr("Gender").ToString())
                cboCivilStatus.Text = If(IsDBNull(dr("CivilStatus")), "", dr("CivilStatus").ToString())

                ' Ginamit ang "Address" column dahil walang hiwalay na Street, Barangay, City sa table
                Dim fullAddress As String = If(IsDBNull(dr("Address")), "", dr("Address").ToString())
                selectedResidentAddress = fullAddress

                ' === SIMULA NG ADDRESS SPLITTING ===
                ' Hahatiin ang fullAddress base sa comma (,) para ilagay sa hiwa-hiwalay na textboxes
                Dim addressParts As String() = fullAddress.Split(","c)

                If addressParts.Length >= 3 Then
                    ' Kung may 3 o higit pang bahagi (Hal: "Street, Barangay, City")
                    txtStreet.Text = addressParts(0).Trim()
                    txtBarangay.Text = addressParts(1).Trim()
                    txtCity.Text = addressParts(2).Trim()
                ElseIf addressParts.Length = 2 Then
                    ' Kung 2 bahagi lang (Hal: "Street, Barangay")
                    txtStreet.Text = addressParts(0).Trim()
                    txtBarangay.Text = addressParts(1).Trim()
                    txtCity.Text = ""
                ElseIf addressParts.Length = 1 Then
                    ' Kung walang comma, ilalagay lahat sa Street
                    txtStreet.Text = addressParts(0).Trim()
                    txtBarangay.Text = ""
                    txtCity.Text = ""
                Else
                    txtStreet.Text = ""
                    txtBarangay.Text = ""
                    txtCity.Text = ""
                End If
                ' === KATAPUSAN NG ADDRESS SPLITTING ===

                txtFatherName.Text = If(IsDBNull(dr("FatherName")), "", dr("FatherName").ToString())
                txtMotherName.Text = If(IsDBNull(dr("MotherName")), "", dr("MotherName").ToString())
                txtMobileNumber.Text = If(IsDBNull(dr("MobileNumber")), "", dr("MobileNumber").ToString())
                txtEmail.Text = If(IsDBNull(dr("Email")), "", dr("Email").ToString())
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox("Error loading resident data: " & ex.Message, MsgBoxStyle.Exclamation)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === SAVE LOGIC ===
    Private Function SaveAppointment(ByVal status As String) As Boolean
        Try
            connection()
            Dim isRepresentative As Boolean = (cboRequestFor.Text.Trim() = "Family Member / Relative" OrElse
                                               cboRequestFor.Text.Trim() = "Representative / On Behalf")
            Dim autoDepartment As String = GetSelectedDepartment()

            ' Pinagsama ang Firstname at Lastname dahil nakahiwalay na sila sa UI
            Dim combinedFullName As String = $"{txtFirstname.Text.Trim()} {txtLastname.Text.Trim()}".Trim()

            sql = "INSERT INTO appointments (ControlNo, ResidentID, FullName, FullAddress, RequestFor, " &
                  "RepresentativeName, RequestType, Purpose, Department, DateSubmitted, " &
                  "ScheduledDate, Status, CreatedAt) " &
                  "VALUES (@ctrl, @resID, @name, @address, @reqFor, @repName, " &
                  "@reqType, @purpose, @dept, NOW(), NOW(), @status, NOW())"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ctrl", lblControlNo.Text.Trim())
            cmd.Parameters.AddWithValue("@resID", If(selectedResidentID > 0, selectedResidentID, DBNull.Value))
            cmd.Parameters.AddWithValue("@name", combinedFullName)
            cmd.Parameters.AddWithValue("@address", If(String.IsNullOrWhiteSpace(selectedResidentAddress), "", selectedResidentAddress))
            cmd.Parameters.AddWithValue("@reqFor", If(String.IsNullOrWhiteSpace(cboRequestFor.Text.Trim()), "", cboRequestFor.Text.Trim()))
            cmd.Parameters.AddWithValue("@repName", If(isRepresentative AndAlso Not String.IsNullOrWhiteSpace(txtNameOfRepresentative.Text.Trim()),
                                                        txtNameOfRepresentative.Text.Trim(), ""))
            cmd.Parameters.AddWithValue("@reqType", If(String.IsNullOrWhiteSpace(cboRequestType.Text.Trim()), "", cboRequestType.Text.Trim()))
            cmd.Parameters.AddWithValue("@purpose", If(String.IsNullOrWhiteSpace(txtPurpose.Text.Trim()), "", txtPurpose.Text.Trim()))
            cmd.Parameters.AddWithValue("@dept", If(String.IsNullOrWhiteSpace(autoDepartment), "", autoDepartment))
            cmd.Parameters.AddWithValue("@status", status)

            Return cmd.ExecuteNonQuery() > 0
        Catch ex As Exception
            MsgBox("Error saving appointment: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnCreateRequest.Click ' Pinalitan ko na rito kung ang pangalan sa designer ay btnCreateRequest
        If String.IsNullOrWhiteSpace(txtLastname.Text) OrElse String.IsNullOrWhiteSpace(txtFirstname.Text) Then
            MsgBox("Please select or enter a resident name.", MsgBoxStyle.Exclamation, "Validation Error")
            txtLastname.Focus()
            Return
        End If

        Dim isRepresentative As Boolean = (cboRequestFor.Text.Trim() = "Family Member / Relative" OrElse
                                            cboRequestFor.Text.Trim() = "Representative / On Behalf")

        If isRepresentative Then
            If String.IsNullOrWhiteSpace(txtNameOfRepresentative.Text) Then
                MsgBox("Please enter the Representative's full name.", MsgBoxStyle.Exclamation, "Validation Error")
                txtNameOfRepresentative.Focus()
                Return
            End If
        End If

        If cboRequestType.SelectedIndex = -1 AndAlso String.IsNullOrWhiteSpace(cboRequestType.Text) Then
            MsgBox("Please select a valid Request Type / Document Service.", MsgBoxStyle.Exclamation, "Validation Error")
            cboRequestType.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtPurpose.Text) Then
            MsgBox("Please state the purpose of your appointment.", MsgBoxStyle.Exclamation, "Validation Error")
            txtPurpose.Focus()
            Return
        End If

        If SaveAppointment("APPROVED") Then
            _skipClosePrompt = True
            MsgBox($"Pick-up appointment request {lblControlNo.Text.Trim()} submitted and APPROVED successfully!",
                   MsgBoxStyle.Information, "Success")

            ' DITO PAPASOK YUNG COUPON VIEW NA MANGYAYARI KAPAG EMBEDDED ANG FORM SA MAIN DASHBOARD
            Dim newControlNo As String = GenerateControlNumber() ' O yung ginamit mong GetLatestCreatedControlNo()
            If Not String.IsNullOrEmpty(newControlNo) Then
                ' I-show ang coupon view (kung naka-design na itong lumabas bilang dialog form)
                Using frmCoupon As New frmCouponView(lblControlNo.Text.Trim())
                    frmCoupon.ShowDialog()
                End Using
            End If

            ' Isara o itago ang panel content at bumalik sa dashboard (base sa pinag-usapan natin kanina)
            Dim dashboard As New frmUser_Dashboard()
            dashboard.TopLevel = False
            dashboard.FormBorderStyle = FormBorderStyle.None
            dashboard.Dock = DockStyle.Fill

            frmMain.Panel2.Controls.Clear()
            frmMain.Panel2.Controls.Add(dashboard)
            dashboard.Show()
        Else
            MsgBox("Failed to submit pick-up appointment request.", MsgBoxStyle.Exclamation, "Warning")
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click ' Base sa button sa ui na "Clear All" / Cancel
        _skipClosePrompt = True

        Dim dashboard As New frmUser_Dashboard()
        dashboard.TopLevel = False
        dashboard.FormBorderStyle = FormBorderStyle.None
        dashboard.Dock = DockStyle.Fill

        frmMain.Panel2.Controls.Clear()
        frmMain.Panel2.Controls.Add(dashboard)
        dashboard.Show()
    End Sub
End Class

Public Class CueBanner
    Private Const EM_SETCUEBANNER As Integer = &H1501
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer,
                                        ByVal wParam As Integer,
                                        <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function FindWindowEx(ByVal hWndParent As IntPtr, ByVal hWndChildAfter As IntPtr,
                                         ByVal lpszClass As String, ByVal lpszWindow As String) As IntPtr
    End Function

    Public Shared Sub SetText(ByVal txt As TextBox, ByVal cueText As String)
        If txt.IsHandleCreated Then
            SendMessage(txt.Handle, EM_SETCUEBANNER, 0, cueText)
        End If
    End Sub

    Public Shared Sub SetText(ByVal cbo As ComboBox, ByVal cueText As String)
        If cbo.IsHandleCreated Then
            Dim editHandle As IntPtr = FindWindowEx(cbo.Handle, IntPtr.Zero, "Edit", Nothing)
            If editHandle <> IntPtr.Zero Then
                SendMessage(editHandle, EM_SETCUEBANNER, 0, cueText)
            End If
        End If
    End Sub
End Class