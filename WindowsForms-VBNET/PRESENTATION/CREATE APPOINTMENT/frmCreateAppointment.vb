Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Runtime.InteropServices

Public Class frmCreateAppointment
    Private selectedResidentID As Integer = 0
    Private selectedResidentAddress As String = ""
    Private _skipClosePrompt As Boolean = False
    Private selectedRepresentativeID As Integer = 0

    Public IsEditMode As Boolean = False
    Public editControlNo As String = ""
    Private currentAppointmentStatus As String = "PROCESSING"

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

    Public Sub New(ByVal controlNo As String, Optional ByVal status As String = "PROCESSING")
        InitializeComponent()
        IsEditMode = True
        editControlNo = controlNo
        currentAppointmentStatus = status.ToUpper()
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

        If IsEditMode Then
            lblControlNo.Text = editControlNo
            LoadExistingDataForEdit()

            Select Case currentAppointmentStatus
                Case "PENDING"
                    btnCreateRequest.Text = "Proceed to Processing" ' ✅ Binago mula sa "Review & Approve"
                Case "APPROVE", "APPROVED"
                    btnCreateRequest.Text = "Proceed to Processing"
                Case "PROCESSING"
                    btnCreateRequest.Text = "Update & Move Next"
                Case Else
                    btnCreateRequest.Text = "Update Request"
            End Select
        Else
            lblControlNo.Text = GenerateControlNumber()
            LoadLoggedUserDefault()
            currentAppointmentStatus = "PROCESSING" ' ✅ Nagsisimula na sa PROCESSING kapag bago (imbes na APPROVE)
            btnCreateRequest.Text = "Submit Request"
        End If
    End Sub

    Private Sub cboRequestType_TextUpdate(sender As Object, e As EventArgs) Handles cboRequestType.TextUpdate
        isFilteringType = True
        RefilterRequestTypes()
        isFilteringType = False
    End Sub

    Private Sub RefilterRequestTypes()
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

                If Not IsDBNull(dr("Status")) Then
                    currentAppointmentStatus = dr("Status").ToString().Trim().ToUpper()
                End If
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

    Private Function GetDocumentPrice(serviceName As String) As Decimal
        Try
            If cn.State <> ConnectionState.Open Then connection()
            sql = "SELECT Amount FROM document_services WHERE ServiceName LIKE @Name LIMIT 1"
            Using cmdPrice As New MySqlCommand(sql, cn)
                cmdPrice.Parameters.AddWithValue("@Name", "%" & serviceName & "%")
                Dim result = cmdPrice.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return Convert.ToDecimal(result)
                End If
            End Using
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
        Return 0D
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

    Private Sub LoadResidentData(resID As Integer)
        Try
            If cn.State <> ConnectionState.Open Then connection()
            sql = "SELECT * FROM residences WHERE ResidentID = @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", resID)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                selectedResidentID = resID

                If Not IsDBNull(dr("Lastname")) Then txtLastname.Text = dr("Lastname").ToString().Trim() Else txtLastname.Text = "N/A"
                If Not IsDBNull(dr("Firstname")) Then txtFirstname.Text = dr("Firstname").ToString().Trim() Else txtFirstname.Text = "N/A"
                If Not IsDBNull(dr("Middlename")) Then txtMiddle.Text = dr("Middlename").ToString().Trim() Else txtMiddle.Text = "N/A"
                If Not IsDBNull(dr("Suffix")) Then cboSuffix.Text = dr("Suffix").ToString().Trim() Else cboSuffix.Text = "N/A"

                If Not IsDBNull(dr("Birthday")) Then
                    dtpDateOfBirth.Value = Convert.ToDateTime(dr("Birthday"))
                End If

                If Not IsDBNull(dr("BirthPlace")) Then txtBirthPlace.Text = dr("BirthPlace").ToString().Trim() Else txtBirthPlace.Text = "N/A"
                If Not IsDBNull(dr("Gender")) Then cboGender.Text = dr("Gender").ToString().Trim() Else cboGender.Text = "N/A"
                If Not IsDBNull(dr("CivilStatus")) Then cboCivilStatus.Text = dr("CivilStatus").ToString().Trim() Else cboCivilStatus.Text = "N/A"
                If Not IsDBNull(dr("FatherName")) Then txtFatherName.Text = dr("FatherName").ToString().Trim() Else txtFatherName.Text = "N/A"
                If Not IsDBNull(dr("MotherName")) Then txtMotherName.Text = dr("MotherName").ToString().Trim() Else txtMotherName.Text = "N/A"
                If Not IsDBNull(dr("MobileNumber")) Then txtMobileNumber.Text = dr("MobileNumber").ToString().Trim() Else txtMobileNumber.Text = "N/A"
                If Not IsDBNull(dr("Email")) Then txtEmail.Text = dr("Email").ToString().Trim() Else txtEmail.Text = "N/A"

                Dim fAddress As String = If(IsDBNull(dr("Address")), "N/A", dr("Address").ToString().Trim())
                txtAddress.Text = fAddress
                selectedResidentAddress = fAddress
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

    Private Function SaveAppointment(ByVal statusToSave As String) As Boolean
        Try
            If cn.State <> ConnectionState.Open Then connection()
            Dim isRepresentative As Boolean = (cboRequestFor.Text.Trim() = "Family Member / Relative" OrElse cboRequestFor.Text.Trim() = "Representative / On Behalf")
            Dim autoDepartment As String = GetSelectedDepartment()
            Dim combinedFullName As String = $"{txtFirstname.Text.Trim()} {txtLastname.Text.Trim()}".Trim()

            If IsEditMode Then
                sql = "UPDATE appointments SET ResidentID=@resID, FullName=@name, FullAddress=@address, RequestFor=@reqFor, RepresentativeName=@repName, RequestType=@reqType, Purpose=@purpose, Department=@dept, Status=@status, UpdatedAt=NOW() WHERE ControlNo=@ctrl"
            Else
                sql = "INSERT INTO appointments (ControlNo, ResidentID, FullName, FullAddress, RequestFor, RepresentativeName, RequestType, Purpose, Department, DateSubmitted, ScheduledDate, Status, CreatedAt) VALUES (@ctrl, @resID, @name, @address, @reqFor, @repName, @reqType, @purpose, @dept, NOW(), NOW(), @status, NOW())"
            End If

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ctrl", lblControlNo.Text.Trim())
            cmd.Parameters.AddWithValue("@resID", If(selectedResidentID > 0, selectedResidentID, DBNull.Value))
            cmd.Parameters.AddWithValue("@name", combinedFullName)
            cmd.Parameters.AddWithValue("@address", If(String.IsNullOrWhiteSpace(txtAddress.Text), "", txtAddress.Text.Trim()))
            cmd.Parameters.AddWithValue("@reqFor", If(String.IsNullOrWhiteSpace(cboRequestFor.Text.Trim()), "", cboRequestFor.Text.Trim()))
            cmd.Parameters.AddWithValue("@repName", If(isRepresentative AndAlso Not String.IsNullOrWhiteSpace(txtNameOfRepresentative.Text.Trim()), txtNameOfRepresentative.Text.Trim(), ""))
            cmd.Parameters.AddWithValue("@reqType", If(String.IsNullOrWhiteSpace(cboRequestType.Text.Trim()), "", cboRequestType.Text.Trim()))
            cmd.Parameters.AddWithValue("@purpose", If(String.IsNullOrWhiteSpace(cboPurpose.Text.Trim()), "", cboPurpose.Text.Trim()))
            cmd.Parameters.AddWithValue("@dept", If(String.IsNullOrWhiteSpace(autoDepartment), "", autoDepartment))
            cmd.Parameters.AddWithValue("@status", statusToSave)

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

        Dim targetNextStatus As String = currentAppointmentStatus
        If Not IsEditMode Then
            targetNextStatus = "PROCESSING" ' ✅ Bagong request, deretso na sa PROCESSING
        Else
            If currentAppointmentStatus = "PENDING" Then
                targetNextStatus = "PROCESSING" ' ✅ PENDING to PROCESSING agad
            ElseIf currentAppointmentStatus = "APPROVE" OrElse currentAppointmentStatus = "APPROVED" Then
                targetNextStatus = "PROCESSING" ' ✅ (Fallback lang ito kung sakaling may natirang Approved data)
            ElseIf currentAppointmentStatus = "PROCESSING" Then
                Dim docPrice As Decimal = GetDocumentPrice(cboRequestType.Text.Trim())
                If docPrice <= 0 Then
                    targetNextStatus = "TO RELEASE"
                Else
                    targetNextStatus = "UNPAID"
                End If
            End If
        End If

        If SaveAppointment(targetNextStatus) Then
            _skipClosePrompt = True
            MsgBox($"Appointment {lblControlNo.Text.Trim()} successfully updated to status: {targetNextStatus}!", MsgBoxStyle.Information)

            Dim dashboard As New frmUser_Dashboard()
            dashboard.TopLevel = False
            dashboard.FormBorderStyle = FormBorderStyle.None
            dashboard.Dock = DockStyle.Fill
            frmMain.Panel2.Controls.Clear()
            frmMain.Panel2.Controls.Add(dashboard)
            dashboard.Show()
        Else
            MsgBox("Failed to update appointment process.", MsgBoxStyle.Exclamation)
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