Imports MySql.Data.MySqlClient

Public Class frmSatelliteOfficeList

    Private Sub frmSatelliteOfficeList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ✅ SIGURADONG TATAKBO — wire ang event gamit ang AddHandler
        AddHandler dgvOffices.CellDoubleClick, AddressOf dgvOffices_CellDoubleClick

        ' ✅ Iwasan na pumasok sa cell edit mode pag nag-double click
        dgvOffices.EditMode = DataGridViewEditMode.EditProgrammatically

        LoadOfficeList()
    End Sub

    Private Sub LoadOfficeList(Optional searchKeyword As String = "")
        Try
            connection()
            Dim sql As String = "SELECT ID, satellite_office_number AS `Office No`, facility_name AS `Facility Name`, " &
                                "facility_type AS `Type`, operation_status AS `Status`, " &
                                "location_details AS `Location`, serving_area AS `Serving Area`, " &
                                "has_permanent_staff AS `Permanent Staff`, date_updated AS `Last Updated` " &
                                "FROM brgy_putatan_satellite_offices"
            If Not String.IsNullOrWhiteSpace(searchKeyword) Then
                sql &= " WHERE facility_name LIKE @kw OR satellite_office_number LIKE @kw " &
                       "OR location_details LIKE @kw OR serving_area LIKE @kw"
            End If
            sql &= " ORDER BY ID DESC"

            Dim da As New MySqlDataAdapter(sql, cn)
            If Not String.IsNullOrWhiteSpace(searchKeyword) Then
                da.SelectCommand.Parameters.AddWithValue("@kw", "%" & searchKeyword & "%")
            End If

            Dim dt As New DataTable
            da.Fill(dt)
            dgvOffices.DataSource = dt
            FormatDataGridView()
            UpdateSummary(dt)
        Catch ex As Exception
            MsgBox("Error loading list: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub FormatDataGridView()
        dgvOffices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvOffices.AllowUserToAddRows = False
        dgvOffices.RowHeadersVisible = False
        dgvOffices.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOffices.MultiSelect = False
        dgvOffices.ReadOnly = True
        dgvOffices.EnableHeadersVisualStyles = False
        dgvOffices.BackgroundColor = Color.White
        dgvOffices.BorderStyle = BorderStyle.None
        dgvOffices.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvOffices.GridColor = Color.Gainsboro

        Dim headerStyle As New DataGridViewCellStyle()
        headerStyle.BackColor = Color.FromArgb(25, 42, 86)
        headerStyle.ForeColor = Color.White
        headerStyle.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        headerStyle.Padding = New Padding(5, 10, 5, 10)
        dgvOffices.ColumnHeadersDefaultCellStyle = headerStyle
        dgvOffices.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvOffices.ColumnHeadersHeight = 40

        Dim rowStyle As New DataGridViewCellStyle()
        rowStyle.BackColor = Color.White
        rowStyle.ForeColor = Color.Black
        rowStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        rowStyle.SelectionBackColor = Color.FromArgb(218, 223, 240)
        rowStyle.SelectionForeColor = Color.Black
        rowStyle.Padding = New Padding(5, 5, 5, 5)
        dgvOffices.DefaultCellStyle = rowStyle
        dgvOffices.RowTemplate.Height = 35
        dgvOffices.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 252)

        With dgvOffices
            If .Columns.Contains("ID") Then .Columns("ID").Visible = False
            If .Columns.Contains("Office No") Then .Columns("Office No").Width = 90
            If .Columns.Contains("Facility Name") Then .Columns("Facility Name").Width = 200
            If .Columns.Contains("Type") Then .Columns("Type").Width = 130
            If .Columns.Contains("Status") Then .Columns("Status").Width = 120
            If .Columns.Contains("Location") Then .Columns("Location").Width = 220
            If .Columns.Contains("Serving Area") Then .Columns("Serving Area").Width = 150
            If .Columns.Contains("Permanent Staff") Then .Columns("Permanent Staff").Width = 110
            If .Columns.Contains("Last Updated") Then .Columns("Last Updated").Width = 140

            For Each row As DataGridViewRow In .Rows
                Dim statusVal = row.Cells("Status").Value?.ToString()
                If statusVal = "Operational" Then
                    row.Cells("Status").Style.ForeColor = Color.SeaGreen
                ElseIf statusVal = "Closed" Then
                    row.Cells("Status").Style.ForeColor = Color.Crimson
                Else
                    row.Cells("Status").Style.ForeColor = Color.DarkOrange
                End If
                row.Cells("Status").Style.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            Next
        End With
    End Sub

    Private Sub UpdateSummary(dt As DataTable)
        lblTotal.Text = dt.Rows.Count.ToString()
        lblActive.Text = dt.Select("Status = 'Operational'").Length.ToString()
        lblStaff.Text = dt.Select("`Permanent Staff` = 'Yes'").Length.ToString()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadOfficeList(txtSearch.Text.Trim())
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadOfficeList()
    End Sub

    Private Sub dgvOffices_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return

        Try
            ' Kunin ang ID mula sa row
            Dim selectedRow As DataGridViewRow = dgvOffices.Rows(e.RowIndex)
            Dim drv As DataRowView = CType(selectedRow.DataBoundItem, DataRowView)
            Dim officeID As Integer = Convert.ToInt32(drv("ID"))

            Dim main As frmMain = TryCast(Application.OpenForms("frmMain"), frmMain)
            If main Is Nothing Then
                MsgBox("Main form not found.", MsgBoxStyle.Exclamation)
                Return
            End If

            main.Panel2.Controls.Clear()

            ' ✅ Buksan sa EDIT MODE — ipasa ang ID
            Dim frmEdit As New frmCreateNewSateliteOffice(officeID)
            frmEdit.TopLevel = False
            frmEdit.FormBorderStyle = FormBorderStyle.None
            frmEdit.Dock = DockStyle.Fill
            main.Panel2.Controls.Add(frmEdit)
            frmEdit.BringToFront()
            frmEdit.Show()

        Catch ex As Exception
            MsgBox("Error opening office: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub frmSatelliteOfficeList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim mainForm As frmMain = TryCast(Application.OpenForms("frmMain"), frmMain)
        If mainForm IsNot Nothing Then
            mainForm.Enabled = True
            mainForm.Activate()
        End If
    End Sub

    Private Sub btnCreateRequest_Click(sender As Object, e As EventArgs) Handles btnCreateRequest.Click
        MessageBox.Show("This feature is currently under development.", "Developer", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class