Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices

Public Class frmCreateNewSateliteOffice

    Private Const EM_SETCUEBANNER As Integer = &H1501
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> lParam As String) As IntPtr
    End Function

    Private editingOfficeID As Integer? = Nothing

    ' ADD MODE
    Public Sub New()
        InitializeComponent()
        editingOfficeID = Nothing
    End Sub

    ' EDIT MODE
    Public Sub New(ByVal officeID As Integer)
        InitializeComponent()
        editingOfficeID = officeID
    End Sub

    Private Sub frmCreateNewSateliteOffice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDropdowns()
        InitializePlaceholders()

        If editingOfficeID.HasValue Then
            ' ===== EDIT MODE =====
            Me.Text = "Edit Satellite Office"
            btnSubmit.Text = "Update Office"
            btnClearAll.Text = "Delete Office"
            LoadOfficeData()
        Else
            ' ===== ADD MODE =====
            Me.Text = "Create New Satellite Office"
            btnSubmit.Text = "Create new Office"
            btnClearAll.Text = "Clear All"
            GenerateOfficeNumber()
        End If

        ClearAllValidationLabels()
    End Sub

    Private Sub InitializePlaceholders()
        SendMessage(txtFacilityName.Handle, EM_SETCUEBANNER, 0, "e.g. Putatan Satellite Office - North")
        SendMessage(txtContactNote.Handle, EM_SETCUEBANNER, 0, "Contact number or person in-charge")
        SendMessage(txtLocationDetails.Handle, EM_SETCUEBANNER, 0, "Complete address / landmark")
        SendMessage(txtServingArea.Handle, EM_SETCUEBANNER, 0, "Barangay / Zone covered")
        SendMessage(txtRemarks.Handle, EM_SETCUEBANNER, 0, "Additional notes (optional)")
    End Sub

    Private Sub LoadDropdowns()
        cboFacilityType.Items.Clear()
        cboFacilityType.Items.AddRange({"Satellite Office", "Service Point", "Extension Office", "Extension Facility"})
        cboFacilityType.SelectedIndex = -1

        cboHasPermanentStaff.Items.Clear()
        cboHasPermanentStaff.Items.AddRange({"Yes", "No"})
        cboHasPermanentStaff.SelectedIndex = -1

        cboOperationStatus.Items.Clear()
        cboOperationStatus.Items.AddRange({"Operational", "Closed", "Under Maintenance", "Temporarily Closed", "Under Rehabilitation"})
        cboOperationStatus.SelectedIndex = -1
    End Sub

    Private Sub GenerateOfficeNumber()
        Try
            connection()
            Dim sql = "SELECT satellite_office_number FROM brgy_putatan_satellite_offices " &
                      "WHERE satellite_office_number LIKE 'SAT-%' ORDER BY ID DESC LIMIT 1"
            Using cmd As New MySqlCommand(sql, cn)
                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        Dim lastNum = dr("satellite_office_number").ToString()
                        If lastNum.Contains("-") Then
                            Dim numPart = lastNum.Split("-"c)(1)
                            If Integer.TryParse(numPart, Nothing) Then
                                lblSatelliteOfficeNumber.Text = "SAT-" & (CInt(numPart) + 1).ToString("D3")
                                Return
                            End If
                        End If
                    End If
                    lblSatelliteOfficeNumber.Text = "SAT-001"
                End Using
            End Using
        Catch ex As Exception
            lblSatelliteOfficeNumber.Text = "SAT-001"
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadOfficeData()
        Try
            connection()
            Dim sql = "SELECT * FROM brgy_putatan_satellite_offices WHERE ID = @id"
            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@id", editingOfficeID.Value)
                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        txtFacilityName.Text = If(IsDBNull(dr("facility_name")), "", dr("facility_name").ToString())
                        txtServingArea.Text = If(IsDBNull(dr("serving_area")), "", dr("serving_area").ToString())
                        txtContactNote.Text = If(IsDBNull(dr("contact_note")), "", dr("contact_note").ToString())
                        txtLocationDetails.Text = If(IsDBNull(dr("location_details")), "", dr("location_details").ToString())
                        txtRemarks.Text = If(IsDBNull(dr("remarks")), "", dr("remarks").ToString())
                        cboFacilityType.Text = If(IsDBNull(dr("facility_type")), "", dr("facility_type").ToString())
                        cboHasPermanentStaff.Text = If(IsDBNull(dr("has_permanent_staff")), "No", dr("has_permanent_staff").ToString())
                        cboOperationStatus.Text = If(IsDBNull(dr("operation_status")), "", dr("operation_status").ToString())
                        lblSatelliteOfficeNumber.Text = If(IsDBNull(dr("satellite_office_number")), "", dr("satellite_office_number").ToString())
                    End If
                End Using
            End Using

            ClearAllValidationLabels()
            txtFacilityName_TextChanged(Nothing, Nothing)
            txtLocationDetails_TextChanged(Nothing, Nothing)
            txtServingArea_TextChanged(Nothing, Nothing)
            cboFacilityType_SelectedIndexChanged(Nothing, Nothing)
            cboOperationStatus_SelectedIndexChanged(Nothing, Nothing)

        Catch ex As Exception
            MsgBox("Error loading data: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' ==================================================
    '  SAVE ACTIVITY LOG — para sa ADD, EDIT, DELETE
    '  ✅ TAMA NA — gamit ang GlobalVars variables
    ' ==================================================
    Private Sub SaveActivityLog(ByVal actionType As String, ByVal details As String)
        Try
            connection()
            Dim sql = "INSERT INTO activity_logs " &
                      "(ActionDate, ActionType, Details, FullName, Module, UserID, UserRole, IPAddress, DeviceInfo) " &
                      "VALUES (NOW(), @action, @details, @fullname, @module, @userid, @role, @ip, @device)"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@action", actionType)
                cmd.Parameters.AddWithValue("@details", details)
                cmd.Parameters.AddWithValue("@fullname", LoggedInFullname)              ' ✅ Tama
                cmd.Parameters.AddWithValue("@module", "Satellite Offices")
                cmd.Parameters.AddWithValue("@userid", LoggedInUserID)                   ' ✅ Tama
                cmd.Parameters.AddWithValue("@role", If(String.IsNullOrEmpty(LoggedInRole), "Staff", LoggedInRole)) ' ✅ Tama
                cmd.Parameters.AddWithValue("@ip", GetLocalIPAddress())
                cmd.Parameters.AddWithValue("@device", Environment.MachineName)

                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            ' Huwag ipahinto ang main operation kahit mag-fail ang log
        Finally
            CloseConnection()
        End Try
    End Sub

    ' Helper — kunin ang local IP address ng computer
    Private Function GetLocalIPAddress() As String
        Try
            Dim host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            For Each ip In host.AddressList
                If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
        Catch
        End Try
        Return "Unknown"
    End Function

    Private Sub ClearAllValidationLabels()
        SetFeedbackLabel(lblFacilityNameError, "", False)
        SetFeedbackLabel(lblFacilityTypeError, "", False)
        SetFeedbackLabel(lblLocationDetailsError, "", False)
        SetFeedbackLabel(lblOperationStatusError, "", False)
        SetFeedbackLabel(lblServingAreaError, "", False)
    End Sub

    Private Sub SetFeedbackLabel(lbl As Label, message As String, isError As Boolean)
        If lbl Is Nothing Then Exit Sub
        lbl.Text = message
        lbl.ForeColor = If(String.IsNullOrEmpty(message), Color.Black, If(isError, Color.Red, Color.Green))
    End Sub

    ' ✅ Dinagdagan ng Handles — dati wala, kaya hindi gumagana validation
    Private Sub txtFacilityName_TextChanged(sender As Object, e As EventArgs) Handles txtFacilityName.TextChanged
        SetFeedbackLabel(lblFacilityNameError, If(String.IsNullOrWhiteSpace(txtFacilityName.Text), "Facility name is required.", "✓ OK"), String.IsNullOrWhiteSpace(txtFacilityName.Text))
    End Sub

    Private Sub cboFacilityType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFacilityType.SelectedIndexChanged
        SetFeedbackLabel(lblFacilityTypeError, If(cboFacilityType.SelectedIndex = -1, "Please select facility type.", "✓ OK"), cboFacilityType.SelectedIndex = -1)
    End Sub

    Private Sub txtLocationDetails_TextChanged(sender As Object, e As EventArgs) Handles txtLocationDetails.TextChanged
        SetFeedbackLabel(lblLocationDetailsError, If(String.IsNullOrWhiteSpace(txtLocationDetails.Text), "Location details are required.", "✓ OK"), String.IsNullOrWhiteSpace(txtLocationDetails.Text))
    End Sub

    Private Sub cboOperationStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboOperationStatus.SelectedIndexChanged
        SetFeedbackLabel(lblOperationStatusError, If(cboOperationStatus.SelectedIndex = -1, "Please select status.", "✓ OK"), cboOperationStatus.SelectedIndex = -1)
    End Sub

    Private Sub txtServingArea_TextChanged(sender As Object, e As EventArgs) Handles txtServingArea.TextChanged
        SetFeedbackLabel(lblServingAreaError, If(String.IsNullOrWhiteSpace(txtServingArea.Text), "Serving area is required.", "✓ OK"), String.IsNullOrWhiteSpace(txtServingArea.Text))
    End Sub

    ' ==================================================
    '  CLEAR ALL / DELETE BUTTON — may Handles na rin
    ' ==================================================
    Private Sub btnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        If editingOfficeID.HasValue Then
            ' ===== DELETE MODE =====
            Dim result = MsgBox("Are you sure you want to DELETE this Satellite Office?" & vbCrLf & vbCrLf &
                                "Office: " & lblSatelliteOfficeNumber.Text & " - " & txtFacilityName.Text & vbCrLf & vbCrLf &
                                "This action CANNOT be undone!",
                                MsgBoxStyle.YesNo + MsgBoxStyle.Critical + MsgBoxStyle.DefaultButton2,
                                "Confirm Delete")

            If result = MsgBoxResult.No Then Return

            Try
                connection()
                Dim officeInfo As String = lblSatelliteOfficeNumber.Text & " - " & txtFacilityName.Text

                Dim sql = "DELETE FROM brgy_putatan_satellite_offices WHERE ID = @id"
                Using cmd As New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@id", editingOfficeID.Value)
                    If cn.State = ConnectionState.Open Then cn.Close()
                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using

                ' ✅ LOG — DELETE
                SaveActivityLog("DELETE", "Deleted Satellite Office: " & officeInfo)

                MsgBox("Satellite Office deleted successfully!", MsgBoxStyle.Information)

                ' Bumalik sa Listahan
                ReturnToList()

            Catch ex As Exception
                MsgBox("Error deleting: " & ex.Message, MsgBoxStyle.Critical)
            Finally
                CloseConnection()
            End Try

        Else
            ' ===== CLEAR ALL (Add Mode) =====
            If MsgBox("Clear all fields?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Return
            ClearForm()
        End If
    End Sub

    Private Sub ClearForm()
        txtFacilityName.Clear()
        cboFacilityType.SelectedIndex = -1
        cboHasPermanentStaff.SelectedIndex = -1
        txtContactNote.Clear()
        txtLocationDetails.Clear()
        cboOperationStatus.SelectedIndex = -1
        txtServingArea.Clear()
        txtRemarks.Clear()
        ClearAllValidationLabels()
        GenerateOfficeNumber()
    End Sub

    ' Helper — bumalik sa list form
    Private Sub ReturnToList()
        Dim main As frmMain = TryCast(Application.OpenForms("frmMain"), frmMain)
        If main IsNot Nothing Then
            main.Panel2.Controls.Clear()
            Dim frmList As New frmSatelliteOfficeList()
            frmList.TopLevel = False
            frmList.FormBorderStyle = FormBorderStyle.None
            frmList.Dock = DockStyle.Fill
            main.Panel2.Controls.Add(frmList)
            frmList.BringToFront()
            frmList.Show()
        End If
    End Sub

    Private Sub frmCreateNewSateliteOffice_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim mainForm As frmMain = TryCast(Application.OpenForms("frmMain"), frmMain)
        If mainForm IsNot Nothing Then
            mainForm.Enabled = True
            mainForm.Activate()
        End If
    End Sub

    ' ==================================================
    '  SUBMIT — CREATE o UPDATE (may LOG na)
    ' ==================================================
    Private Sub btnSubmit_Click_1(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If lblFacilityNameError.ForeColor = Color.Red OrElse
           lblFacilityTypeError.ForeColor = Color.Red OrElse
           lblLocationDetailsError.ForeColor = Color.Red OrElse
           lblOperationStatusError.ForeColor = Color.Red OrElse
           lblServingAreaError.ForeColor = Color.Red Then
            MsgBox("Please fix all required fields before saving!", MsgBoxStyle.Exclamation)
            Return
        End If

        Try
            connection()

            If editingOfficeID.HasValue Then
                ' ===== UPDATE / EDIT =====
                If MsgBox("Update this satellite office?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Return

                Dim sql = "UPDATE brgy_putatan_satellite_offices SET " &
                          "facility_name=@fname, facility_type=@ftype, has_permanent_staff=@staff, " &
                          "contact_note=@contact, location_details=@loc, operation_status=@status, " &
                          "satellite_office_number=@onum, serving_area=@area, remarks=@remarks, date_updated=NOW() " &
                          "WHERE ID=@id"

                Using cmd As New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@fname", txtFacilityName.Text.Trim())
                    cmd.Parameters.AddWithValue("@ftype", cboFacilityType.Text.Trim())
                    cmd.Parameters.AddWithValue("@staff", If(cboHasPermanentStaff.Text.Trim() = "", "No", cboHasPermanentStaff.Text.Trim()))
                    cmd.Parameters.AddWithValue("@contact", If(String.IsNullOrWhiteSpace(txtContactNote.Text), DBNull.Value, txtContactNote.Text.Trim()))
                    cmd.Parameters.AddWithValue("@loc", txtLocationDetails.Text.Trim())
                    cmd.Parameters.AddWithValue("@status", cboOperationStatus.Text.Trim())
                    cmd.Parameters.AddWithValue("@onum", lblSatelliteOfficeNumber.Text)
                    cmd.Parameters.AddWithValue("@area", txtServingArea.Text.Trim())
                    cmd.Parameters.AddWithValue("@remarks", If(String.IsNullOrWhiteSpace(txtRemarks.Text), DBNull.Value, txtRemarks.Text.Trim()))
                    cmd.Parameters.AddWithValue("@id", editingOfficeID.Value)

                    If cn.State = ConnectionState.Open Then cn.Close()
                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using

                ' ✅ LOG — EDIT / UPDATE
                SaveActivityLog("UPDATE", "Updated Satellite Office: " & lblSatelliteOfficeNumber.Text & " - " & txtFacilityName.Text.Trim())

                MsgBox("Satellite Office updated successfully!", MsgBoxStyle.Information)

            Else
                ' ===== INSERT / ADD =====
                Dim sql = "INSERT INTO brgy_putatan_satellite_offices " &
                          "(facility_name, facility_type, has_permanent_staff, contact_note, location_details, " &
                          "operation_status, satellite_office_number, serving_area, remarks, date_updated) " &
                          "VALUES (@fname, @ftype, @staff, @contact, @loc, @status, @onum, @area, @remarks, NOW())"

                Using cmd As New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@fname", txtFacilityName.Text.Trim())
                    cmd.Parameters.AddWithValue("@ftype", cboFacilityType.Text.Trim())
                    cmd.Parameters.AddWithValue("@staff", If(cboHasPermanentStaff.Text.Trim() = "", "No", cboHasPermanentStaff.Text.Trim()))
                    cmd.Parameters.AddWithValue("@contact", If(String.IsNullOrWhiteSpace(txtContactNote.Text), DBNull.Value, txtContactNote.Text.Trim()))
                    cmd.Parameters.AddWithValue("@loc", txtLocationDetails.Text.Trim())
                    cmd.Parameters.AddWithValue("@status", cboOperationStatus.Text.Trim())
                    cmd.Parameters.AddWithValue("@onum", lblSatelliteOfficeNumber.Text)
                    cmd.Parameters.AddWithValue("@area", txtServingArea.Text.Trim())
                    cmd.Parameters.AddWithValue("@remarks", If(String.IsNullOrWhiteSpace(txtRemarks.Text), DBNull.Value, txtRemarks.Text.Trim()))

                    If cn.State = ConnectionState.Open Then cn.Close()
                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using

                ' ✅ LOG — ADD / CREATE
                SaveActivityLog("CREATE", "Created new Satellite Office: " & lblSatelliteOfficeNumber.Text & " - " & txtFacilityName.Text.Trim())

                MsgBox("Satellite Office created successfully!", MsgBoxStyle.Information)
                ClearForm()
            End If

            ReturnToList()

        Catch ex As MySqlException
            MsgBox(If(ex.Number = 1062, "Duplicate: Office Number already exists!", "Database Error: " & ex.Message), MsgBoxStyle.Critical)
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

End Class