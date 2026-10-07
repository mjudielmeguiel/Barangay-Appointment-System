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
            Me.Text = "Edit Satellite Office & View Residents"
            btnSubmit.Text = "Update Office"
            btnClearAll.Text = "Delete Office"
            LoadOfficeData()

            ' I-load ang mga residente sa DataGridView kung ito ay nag-eexist sa form
            If dgvSatelliteResidents IsNot Nothing Then
                StyleDataGridView(dgvSatelliteResidents)
                LoadResidentsForThisOffice(editingOfficeID.Value)
                dgvSatelliteResidents.Visible = True
            End If

            If txtSearchResident IsNot Nothing Then
                txtSearchResident.Visible = True
            End If
        Else
            ' ===== ADD MODE =====
            Me.Text = "Create New Satellite Office"
            btnSubmit.Text = "Create new Office"
            btnClearAll.Text = "Clear All"
            GenerateOfficeNumber()

            ' Itago muna ang listahan ng residente at search box kapag bago pa lang ginagawa ang office
            If dgvSatelliteResidents IsNot Nothing Then
                dgvSatelliteResidents.Visible = False
            End If

            If txtSearchResident IsNot Nothing Then
                txtSearchResident.Visible = False
            End If
        End If

        ClearAllValidationLabels()
    End Sub

    Private Sub InitializePlaceholders()
        SendMessage(txtFacilityName.Handle, EM_SETCUEBANNER, 0, "e.g. Putatan Satellite Office - North")
        SendMessage(txtContactNote.Handle, EM_SETCUEBANNER, 0, "Contact number or person in-charge")
        SendMessage(txtLocationDetails.Handle, EM_SETCUEBANNER, 0, "Complete address / landmark")
        SendMessage(txtServingArea.Handle, EM_SETCUEBANNER, 0, "Barangay / Zone covered")
        SendMessage(txtRemarks.Handle, EM_SETCUEBANNER, 0, "Additional notes (optional)")

        If txtSearchResident IsNot Nothing Then
            SendMessage(txtSearchResident.Handle, EM_SETCUEBANNER, 0, "Search resident name or code...")
        End If
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

    ' --- LOAD NG MGA RESIDENTE SA ILALIM NG SATELLITE OFFICE NA MAY SEARCH SUPPORT ---
    Private Sub LoadResidentsForThisOffice(officeID As Integer, Optional searchKeyword As String = "")
        Try
            connection()
            Dim sql As String = "SELECT ResidentID, ResidentCode AS 'Resident Code', FullName AS 'Full Name', Gender, " &
                                "Birthday, MobileNumber AS 'Mobile No.', Email, CivilStatus AS 'Civil Status', " &
                                "Address, AccountStatus AS 'Status', CreatedAt AS 'Date Registered' " &
                                "FROM residences WHERE SatelliteOfficeID = @satid AND AccountStatus <> 'Removed' "

            If Not String.IsNullOrWhiteSpace(searchKeyword) Then
                sql &= "AND (FullName LIKE @kw OR ResidentCode LIKE @kw OR MobileNumber LIKE @kw) "
            End If

            sql &= "ORDER BY ResidentID DESC"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@satid", officeID)
                If Not String.IsNullOrWhiteSpace(searchKeyword) Then
                    cmd.Parameters.AddWithValue("@kw", "%" & searchKeyword.Trim() & "%")
                End If

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvSatelliteResidents.DataSource = dt

                ' Optional label para ipakita ang total count kung mayroon kang lblResidentCount
                If Me.Controls.ContainsKey("lblResidentCount") Then
                    DirectCast(Me.Controls("lblResidentCount"), Label).Text = $"Kabuuang Residente: {dt.Rows.Count}"
                End If

                If dgvSatelliteResidents.Columns.Contains("ResidentID") Then
                    dgvSatelliteResidents.Columns("ResidentID").Visible = False
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error loading residents list: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' --- EVENT HANDLER PARA SA SEARCH TEXTBOX NG MGA RESIDENTE ---
    Private Sub txtSearchResident_TextChanged(sender As Object, e As EventArgs) Handles txtSearchResident.TextChanged
        If editingOfficeID.HasValue Then
            LoadResidentsForThisOffice(editingOfficeID.Value, txtSearchResident.Text)
        End If
    End Sub

    ' --- DESIGN PARA SA DATAGRIDVIEW NG MGA RESIDENTE ---
    Private Sub StyleDataGridView(dgv As DataGridView)
        If dgv Is Nothing Then Return

        dgv.EnableHeadersVisualStyles = False
        dgv.BorderStyle = BorderStyle.None
        dgv.BackgroundColor = Color.White
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.GridColor = Color.FromArgb(220, 224, 230)
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.AllowUserToResizeRows = False
        dgv.ReadOnly = True

        Dim headerStyle As New DataGridViewCellStyle()
        headerStyle.BackColor = Color.FromArgb(25, 42, 86)
        headerStyle.ForeColor = Color.White
        headerStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        headerStyle.Padding = New Padding(8, 6, 8, 6)

        dgv.ColumnHeadersDefaultCellStyle = headerStyle
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersHeight = 38
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        Dim defaultRowStyle As New DataGridViewCellStyle()
        defaultRowStyle.BackColor = Color.White
        defaultRowStyle.ForeColor = Color.FromArgb(50, 50, 60)
        defaultRowStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        defaultRowStyle.SelectionBackColor = Color.FromArgb(210, 215, 240)
        defaultRowStyle.SelectionForeColor = Color.Black
        defaultRowStyle.Padding = New Padding(8, 4, 8, 4)

        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle)
        alternatingRowStyle.BackColor = Color.FromArgb(245, 247, 252)

        dgv.DefaultCellStyle = defaultRowStyle
        dgv.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        dgv.RowTemplate.Height = 32
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub SaveActivityLog(ByVal actionType As String, ByVal details As String)
        Try
            connection()
            Dim sql = "INSERT INTO activity_logs " &
                      "(ActionDate, ActionType, Details, FullName, Module, UserID, UserRole, IPAddress, DeviceInfo) " &
                      "VALUES (NOW(), @action, @details, @fullname, @module, @userid, @role, @ip, @device)"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@action", actionType)
                cmd.Parameters.AddWithValue("@details", details)
                cmd.Parameters.AddWithValue("@fullname", LoggedInFullname)
                cmd.Parameters.AddWithValue("@module", "Satellite Offices")
                cmd.Parameters.AddWithValue("@userid", LoggedInUserID)
                cmd.Parameters.AddWithValue("@role", If(String.IsNullOrEmpty(LoggedInRole), "Staff", LoggedInRole))
                cmd.Parameters.AddWithValue("@ip", GetLocalIPAddress())
                cmd.Parameters.AddWithValue("@device", Environment.MachineName)

                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

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

    Private Sub btnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        If editingOfficeID.HasValue Then
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

                SaveActivityLog("DELETE", "Deleted Satellite Office: " & officeInfo)
                MsgBox("Satellite Office deleted successfully!", MsgBoxStyle.Information)
                ReturnToList()

            Catch ex As Exception
                MsgBox("Error deleting: " & ex.Message, MsgBoxStyle.Critical)
            Finally
                CloseConnection()
            End Try
        Else
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

                SaveActivityLog("UPDATE", "Updated Satellite Office: " & lblSatelliteOfficeNumber.Text & " - " & txtFacilityName.Text.Trim())
                MsgBox("Satellite Office updated successfully!", MsgBoxStyle.Information)

            Else
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