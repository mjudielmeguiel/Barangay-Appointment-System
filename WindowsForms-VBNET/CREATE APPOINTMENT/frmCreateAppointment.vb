Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Runtime.InteropServices

Public Class frmCreateAppointment
    Private selectedResidentID As Integer = 0
    Private selectedResidentAddress As String = ""
    Private _skipClosePrompt As Boolean = False
    Private selectedRepresentativeID As Integer = 0

    Public isEditMode As Boolean = False
    Public editControlNo As String = ""

    Private allServices As New List(Of ServiceItem)()
    Private allPurposes As New List(Of String)()
    Private isFilteringType As Boolean = False
    Private isFilteringPurpose As Boolean = False

    Public Class ServiceItem
        Public Property ServiceID As Integer
        Public Property ServiceName As String
        Public Property DepartmentName As String
        Public Overrides Function ToString() As String
            Return ServiceName
        End Function
    End Class

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal controlNo As String)
        InitializeComponent()
        isEditMode = True
        editControlNo = controlNo
    End Sub

    Private Sub frmCreateAppointment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboRequestFor.Items.Clear()
        cboRequestFor.Items.AddRange({
            "Self",
            "Family Member / Relative",
            "Representative / On Behalf"
        })
        cboRequestFor.SelectedIndex = 0

        cboRequestType.DropDownStyle = ComboBoxStyle.DropDown
        cboPurpose.DropDownStyle = ComboBoxStyle.DropDown

        CueBanner.SetText(txtNameOfRepresentative, "Representative's full name")
        CueBanner.SetText(cboPurpose, "Select or state the purpose of your appointment")
        CueBanner.SetText(cboRequestType, "Select Request Type / Document Service")

        ToggleRepresentativeFields(False)
        LoadDocumentServices()

        If isEditMode Then
            lblControlNo.Text = editControlNo
            LoadExistingDataForEdit()
            btnCreateRequest.Text = "Update Request"
        Else
            lblControlNo.Text = GenerateControlNumber()
            LoadLoggedUserDefault()
        End If
    End Sub

    Private Sub cboRequestType_TextUpdate(sender As Object, e As EventArgs) Handles cboRequestType.TextUpdate
        isFilteringType = True
        Dim typedText As String = cboRequestType.Text
        Dim cursorPos As Integer = cboRequestType.SelectionStart

        cboRequestType.Items.Clear()

        If String.IsNullOrWhiteSpace(typedText) Then
            cboRequestType.Items.AddRange(allServices.ToArray())
        Else
            Dim filtered = allServices.Where(Function(x) x.ServiceName.ToLower().Contains(typedText.ToLower())).ToArray()
            If filtered.Length > 0 Then cboRequestType.Items.AddRange(filtered)
        End If

        cboRequestType.Text = typedText
        cboRequestType.SelectionStart = cursorPos
        cboRequestType.DroppedDown = True
        Cursor.Current = Cursors.Default
        isFilteringType = False
    End Sub

    Private Sub cboPurpose_TextUpdate(sender As Object, e As EventArgs) Handles cboPurpose.TextUpdate
        isFilteringPurpose = True
        Dim typedText As String = cboPurpose.Text
        Dim cursorPos As Integer = cboPurpose.SelectionStart

        cboPurpose.Items.Clear()

        If String.IsNullOrWhiteSpace(typedText) Then
            cboPurpose.Items.AddRange(allPurposes.ToArray())
        Else
            Dim filtered = allPurposes.Where(Function(x) x.ToLower().Contains(typedText.ToLower())).ToArray()
            If filtered.Length > 0 Then cboPurpose.Items.AddRange(filtered)
        End If

        cboPurpose.Text = typedText
        cboPurpose.SelectionStart = cursorPos
        cboPurpose.DroppedDown = True
        Cursor.Current = Cursors.Default
        isFilteringPurpose = False
    End Sub

    Private Sub cboRequestType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRequestType.SelectedIndexChanged
        If isFilteringType Then Return
        If cboRequestType.SelectedItem IsNot Nothing AndAlso TypeOf cboRequestType.SelectedItem Is ServiceItem Then
            Dim selectedService As ServiceItem = CType(cboRequestType.SelectedItem, ServiceItem)
            LoadPurposeOptions(selectedService.ServiceID)
        End If
    End Sub

    Private Sub LoadDocumentServices()
        cboRequestType.Items.Clear()
        allServices.Clear()
        Try
            If cn.State <> ConnectionState.Open Then connection()
            sql = "SELECT ds.ID, ds.ServiceName, d.DepartmentName FROM document_services ds LEFT JOIN departments d ON ds.DepartmentID = d.DepartmentID WHERE (ds.IsActive = 1 OR ds.IsActive IS NULL) ORDER BY ds.ServiceName ASC"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                Dim item As New ServiceItem With {
                    .ServiceID = Convert.ToInt32(dr("ID")),
                    .ServiceName = dr("ServiceName").ToString(),
                    .DepartmentName = If(IsDBNull(dr("DepartmentName")), "", dr("DepartmentName").ToString())
                }
                allServices.Add(item)
                cboRequestType.Items.Add(item)
            End While
            dr.Close()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadPurposeOptions(serviceID As Integer)
        cboPurpose.Items.Clear()
        allPurposes.Clear()
        cboPurpose.Text = ""
        Try
            If cn.State <> ConnectionState.Open Then connection()
            sql = "SELECT Purpose FROM service_details WHERE Service_ID = @serviceID"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@serviceID", serviceID)
            dr = cmd.ExecuteReader()

            While dr.Read()
                If Not IsDBNull(dr("Purpose")) Then
                    Dim p As String = dr("Purpose").ToString()
                    allPurposes.Add(p)
                    cboPurpose.Items.Add(p)
                End If
            End While
            dr.Close()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadExistingDataForEdit()
        Dim resID As Integer = 0
        Dim reqFor As String = "Self"
        Dim repName As String = ""
        Dim reqType As String = ""
        Dim purpose As String = ""

        Try
            If cn.State <> ConnectionState.Open Then connection()
            sql = "SELECT * FROM appointments WHERE ControlNo = @ctrl"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ctrl", editControlNo)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                If Not IsDBNull(dr("ResidentID")) Then resID = Convert.ToInt32(dr("ResidentID"))
                reqFor = If(IsDBNull(dr("RequestFor")), "Self", dr("RequestFor").ToString())
                repName = If(IsDBNull(dr("RepresentativeName")), "", dr("RepresentativeName").ToString())
                reqType = If(IsDBNull(dr("RequestType")), "", dr("RequestType").ToString())
                purpose = If(IsDBNull(dr("Purpose")), "", dr("Purpose").ToString())
            End If
            dr.Close()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try

        If resID > 0 Then LoadResidentData(resID)

        cboRequestFor.Text = reqFor
        If reqFor = "Family Member / Relative" OrElse reqFor = "Representative / On Behalf" Then
            ToggleRepresentativeFields(True)
            txtNameOfRepresentative.Text = repName
        End If

        cboRequestType.Text = reqType
        cboPurpose.Text = purpose
    End Sub

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

    Private Sub ToggleRepresentativeFields(isVisible As Boolean)
        If txtNameOfRepresentative IsNot Nothing Then txtNameOfRepresentative.Visible = isVisible
        If lblRepresentative IsNot Nothing Then lblRepresentative.Visible = isVisible
    End Sub

    Private Function GetSelectedDepartment() As String
        If cboRequestType.SelectedItem IsNot Nothing AndAlso TypeOf cboRequestType.SelectedItem Is ServiceItem Then
            Return CType(cboRequestType.SelectedItem, ServiceItem).DepartmentName
        End If
        Dim typedText As String = cboRequestType.Text.Trim()
        For Each itm As Object In cboRequestType.Items
            If TypeOf itm Is ServiceItem AndAlso CType(itm, ServiceItem).ServiceName.Equals(typedText, StringComparison.OrdinalIgnoreCase) Then
                Return CType(itm, ServiceItem).DepartmentName
            End If
        Next
        Return ""
    End Function

    Private Function GenerateControlNumber() As String
        Dim newCtrlNo As String = "APP-001"
        Try
            If cn.State <> ConnectionState.Open Then connection()
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

    Private Sub LoadLoggedUserDefault()
        If String.IsNullOrEmpty(LoggedFullname) Then Return
        Try
            If cn.State <> ConnectionState.Open Then connection()
            sql = "SELECT ResidentID FROM residences WHERE FullName=@name OR Username=@name"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@name", LoggedFullname)
            Dim resIDObj = cmd.ExecuteScalar()

            If resIDObj IsNot Nothing AndAlso Not IsDBNull(resIDObj) Then
                selectedResidentID = Convert.ToInt32(resIDObj)
                LoadResidentData(selectedResidentID)
            End If
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub btnSelectResident_Click(sender As Object, e As EventArgs) Handles btnSelectResident.Click
        Using frmLookup As New frmResidencelist()
            If frmLookup.ShowDialog() = DialogResult.OK Then
                Dim resID As Integer = frmLookup.SelectedResidentID
                If resID > 0 Then LoadResidentData(resID)
            End If
        End Using
    End Sub

    ' === PINATIBAY NA LOGIC (MAY KANYA-KANYANG TRY-CATCH) ===
    Private Sub LoadResidentData(resID As Integer)
        Try
            If cn.State <> ConnectionState.Open Then connection()
            sql = "SELECT * FROM residences WHERE ResidentID = @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", resID)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                selectedResidentID = resID

                ' 1. Lastname
                Try
                    If IsDBNull(dr("Lastname")) OrElse String.IsNullOrWhiteSpace(dr("Lastname").ToString()) Then
                        txtLastname.Text = "N/A"
                    Else
                        txtLastname.Text = dr("Lastname").ToString().Trim()
                    End If
                Catch : End Try

                ' 2. Firstname
                Try
                    If IsDBNull(dr("Firstname")) OrElse String.IsNullOrWhiteSpace(dr("Firstname").ToString()) Then
                        txtFirstname.Text = "N/A"
                    Else
                        txtFirstname.Text = dr("Firstname").ToString().Trim()
                    End If
                Catch : End Try

                ' 3. Middlename
                Try
                    If IsDBNull(dr("Middlename")) OrElse String.IsNullOrWhiteSpace(dr("Middlename").ToString()) Then
                        txtMiddle.Text = "N/A"
                    Else
                        txtMiddle.Text = dr("Middlename").ToString().Trim()
                    End If
                Catch : End Try

                ' 4. Suffix
                Try
                    If IsDBNull(dr("Suffix")) OrElse String.IsNullOrWhiteSpace(dr("Suffix").ToString()) Then
                        cboSuffix.Text = "N/A"
                    Else
                        cboSuffix.Text = dr("Suffix").ToString().Trim()
                    End If
                Catch : End Try

                ' 5. Birthday
                Try
                    If Not IsDBNull(dr("Birthday")) Then
                        dtpDateOfBirth.Value = Convert.ToDateTime(dr("Birthday"))
                    End If
                Catch : End Try

                ' 6. BirthPlace
                Try
                    If IsDBNull(dr("BirthPlace")) OrElse String.IsNullOrWhiteSpace(dr("BirthPlace").ToString()) Then
                        txtBirthPlace.Text = "N/A"
                    Else
                        txtBirthPlace.Text = dr("BirthPlace").ToString().Trim()
                    End If
                Catch : End Try

                ' 7. Gender
                Try
                    If IsDBNull(dr("Gender")) OrElse String.IsNullOrWhiteSpace(dr("Gender").ToString()) Then
                        cboGender.Text = "N/A"
                    Else
                        cboGender.Text = dr("Gender").ToString().Trim()
                    End If
                Catch : End Try

                ' 8. Civil Status
                Try
                    If IsDBNull(dr("CivilStatus")) OrElse String.IsNullOrWhiteSpace(dr("CivilStatus").ToString()) Then
                        cboCivilStatus.Text = "N/A"
                    Else
                        cboCivilStatus.Text = dr("CivilStatus").ToString().Trim()
                    End If
                Catch : End Try

                ' 9. FatherName
                Try
                    If IsDBNull(dr("FatherName")) OrElse String.IsNullOrWhiteSpace(dr("FatherName").ToString()) Then
                        txtFatherName.Text = "N/A"
                    Else
                        txtFatherName.Text = dr("FatherName").ToString().Trim()
                    End If
                Catch : End Try

                ' 10. MotherName
                Try
                    If IsDBNull(dr("MotherName")) OrElse String.IsNullOrWhiteSpace(dr("MotherName").ToString()) Then
                        txtMotherName.Text = "N/A"
                    Else
                        txtMotherName.Text = dr("MotherName").ToString().Trim()
                    End If
                Catch : End Try

                ' 11. Mobile Number
                Try
                    If IsDBNull(dr("MobileNumber")) OrElse String.IsNullOrWhiteSpace(dr("MobileNumber").ToString()) Then
                        txtMobileNumber.Text = "N/A"
                    Else
                        txtMobileNumber.Text = dr("MobileNumber").ToString().Trim()
                    End If
                Catch : End Try

                ' 12. Email
                Try
                    If IsDBNull(dr("Email")) OrElse String.IsNullOrWhiteSpace(dr("Email").ToString()) Then
                        txtEmail.Text = "N/A"
                    Else
                        txtEmail.Text = dr("Email").ToString().Trim()
                    End If
                Catch : End Try

                ' 13. Full Address (Pinalitan ang lumang Street/Barangay/City logic)
                Try
                    If IsDBNull(dr("Address")) OrElse String.IsNullOrWhiteSpace(dr("Address").ToString()) Then
                        txtAddress.Text = "N/A"
                        selectedResidentAddress = ""
                    Else
                        Dim fAddress As String = dr("Address").ToString().Trim()
                        txtAddress.Text = fAddress
                        selectedResidentAddress = fAddress
                    End If
                Catch : End Try

            Else
                MsgBox("Walang nahanap na impormasyon sa database para sa Resident ID: " & resID, MsgBoxStyle.Exclamation)
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox("Error sa pagkuha ng impormasyon: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Function SaveAppointment(ByVal status As String) As Boolean
        Try
            If cn.State <> ConnectionState.Open Then connection()
            Dim isRepresentative As Boolean = (cboRequestFor.Text.Trim() = "Family Member / Relative" OrElse cboRequestFor.Text.Trim() = "Representative / On Behalf")
            Dim autoDepartment As String = GetSelectedDepartment()
            Dim combinedFullName As String = $"{txtFirstname.Text.Trim()} {txtLastname.Text.Trim()}".Trim()

            If isEditMode Then
                sql = "UPDATE appointments SET ResidentID=@resID, FullName=@name, FullAddress=@address, RequestFor=@reqFor, RepresentativeName=@repName, RequestType=@reqType, Purpose=@purpose, Department=@dept, UpdatedAt=NOW() WHERE ControlNo=@ctrl"
            Else
                sql = "INSERT INTO appointments (ControlNo, ResidentID, FullName, FullAddress, RequestFor, RepresentativeName, RequestType, Purpose, Department, DateSubmitted, ScheduledDate, Status, CreatedAt) VALUES (@ctrl, @resID, @name, @address, @reqFor, @repName, @reqType, @purpose, @dept, NOW(), NOW(), @status, NOW())"
            End If

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ctrl", lblControlNo.Text.Trim())
            cmd.Parameters.AddWithValue("@resID", If(selectedResidentID > 0, selectedResidentID, DBNull.Value))
            cmd.Parameters.AddWithValue("@name", combinedFullName)

            ' Kinukuha ang address mula mismo sa txtAddress para kapag na-edit sa form ay ma-update sa database
            cmd.Parameters.AddWithValue("@address", If(String.IsNullOrWhiteSpace(txtAddress.Text), "", txtAddress.Text.Trim()))

            cmd.Parameters.AddWithValue("@reqFor", If(String.IsNullOrWhiteSpace(cboRequestFor.Text.Trim()), "", cboRequestFor.Text.Trim()))
            cmd.Parameters.AddWithValue("@repName", If(isRepresentative AndAlso Not String.IsNullOrWhiteSpace(txtNameOfRepresentative.Text.Trim()), txtNameOfRepresentative.Text.Trim(), ""))
            cmd.Parameters.AddWithValue("@reqType", If(String.IsNullOrWhiteSpace(cboRequestType.Text.Trim()), "", cboRequestType.Text.Trim()))
            cmd.Parameters.AddWithValue("@purpose", If(String.IsNullOrWhiteSpace(cboPurpose.Text.Trim()), "", cboPurpose.Text.Trim()))
            cmd.Parameters.AddWithValue("@dept", If(String.IsNullOrWhiteSpace(autoDepartment), "", autoDepartment))

            If Not isEditMode Then cmd.Parameters.AddWithValue("@status", status)

            Return cmd.ExecuteNonQuery() > 0
        Catch ex As Exception
            MsgBox("Error saving appointment: " & ex.Message, MsgBoxStyle.Critical)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnCreateRequest.Click
        If String.IsNullOrWhiteSpace(txtLastname.Text) OrElse String.IsNullOrWhiteSpace(txtFirstname.Text) Then
            MsgBox("Please select or enter a resident name.", MsgBoxStyle.Exclamation)
            txtLastname.Focus()
            Return
        End If

        If (cboRequestFor.Text.Trim() = "Family Member / Relative" OrElse cboRequestFor.Text.Trim() = "Representative / On Behalf") AndAlso String.IsNullOrWhiteSpace(txtNameOfRepresentative.Text) Then
            MsgBox("Please enter the Representative's full name.", MsgBoxStyle.Exclamation)
            txtNameOfRepresentative.Focus()
            Return
        End If

        If cboRequestType.SelectedIndex = -1 AndAlso String.IsNullOrWhiteSpace(cboRequestType.Text) Then
            MsgBox("Please select a valid Request Type / Document Service.", MsgBoxStyle.Exclamation)
            cboRequestType.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(cboPurpose.Text) Then
            MsgBox("Please select or state the purpose of your appointment.", MsgBoxStyle.Exclamation)
            cboPurpose.Focus()
            Return
        End If

        If SaveAppointment("APPROVED") Then
            _skipClosePrompt = True
            If isEditMode Then
                MsgBox($"Pick-up appointment request {lblControlNo.Text.Trim()} successfully UPDATED!", MsgBoxStyle.Information)
            Else
                MsgBox($"Pick-up appointment request {lblControlNo.Text.Trim()} submitted and APPROVED successfully!", MsgBoxStyle.Information)
                Dim newControlNo As String = GenerateControlNumber()
                If Not String.IsNullOrEmpty(newControlNo) Then
                    Using frmCoupon As New frmCouponView(lblControlNo.Text.Trim())
                        frmCoupon.ShowDialog()
                    End Using
                End If
            End If

            Dim dashboard As New frmUser_Dashboard()
            dashboard.TopLevel = False
            dashboard.FormBorderStyle = FormBorderStyle.None
            dashboard.Dock = DockStyle.Fill
            frmMain.Panel2.Controls.Clear()
            frmMain.Panel2.Controls.Add(dashboard)
            dashboard.Show()
        Else
            MsgBox("Failed to process pick-up appointment request.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        _skipClosePrompt = True
        Dim dashboard As New frmUser_Dashboard()
        dashboard.TopLevel = False
        dashboard.FormBorderStyle = FormBorderStyle.None
        dashboard.Dock = DockStyle.Fill
        frmMain.Panel2.Controls.Clear()
        frmMain.Panel2.Controls.Add(dashboard)
        dashboard.Show()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    End Sub
End Class

Public Class CueBanner
    Private Const EM_SETCUEBANNER As Integer = &H1501
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function FindWindowEx(ByVal hWndParent As IntPtr, ByVal hWndChildAfter As IntPtr, ByVal lpszClass As String, ByVal lpszWindow As String) As IntPtr
    End Function

    Public Shared Sub SetText(ByVal txt As TextBox, ByVal cueText As String)
        If txt.IsHandleCreated Then SendMessage(txt.Handle, EM_SETCUEBANNER, 0, cueText)
    End Sub

    Public Shared Sub SetText(ByVal cbo As ComboBox, ByVal cueText As String)
        If cbo.IsHandleCreated Then
            Dim editHandle As IntPtr = FindWindowEx(cbo.Handle, IntPtr.Zero, "Edit", Nothing)
            If editHandle <> IntPtr.Zero Then SendMessage(editHandle, EM_SETCUEBANNER, 0, cueText)
        End If
    End Sub
End Class