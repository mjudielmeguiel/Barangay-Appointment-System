Imports System.Net
Imports MySql.Data.MySqlClient

Public Class frmResidence_Records

    Private ReadOnly placeholderText As String = "Search Resident Name..."

    Private Sub frmResidence_Records_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblDateTime.Text = DateTime.Now.ToString("F")
        Timer1.Interval = 1000
        Timer1.Start()

        SetupSearchPlaceholder()
        StyleDataGridView(dgvResidences)
        LoadResidenceRecords()
        LoadSummaryMetrics() ' Loads counts for New Today and Active Residents
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblDateTime.Text = DateTime.Now.ToString("F")
    End Sub

    Private Sub LoadResidenceRecords(Optional searchKeyword As String = "")
        Try
            connection()

            sql = "SELECT ResidentID, ResidentCode AS 'Resident Code', FullName AS 'Full Name', Gender, " &
                  "Birthday, MobileNumber AS 'Mobile No.', Email, CivilStatus AS 'Civil Status', " &
                  "Address, AccountStatus AS 'Status', CreatedAt AS 'Date Registered' " &
                  "FROM residences WHERE AccountStatus <> 'Deleted' "

            If Not String.IsNullOrEmpty(searchKeyword) AndAlso searchKeyword <> placeholderText Then
                sql &= "AND FullName LIKE @search "
            End If

            sql &= "ORDER BY ResidentID DESC"

            cmd = New MySqlCommand(sql, cn)

            If Not String.IsNullOrEmpty(searchKeyword) AndAlso searchKeyword <> placeholderText Then
                cmd.Parameters.AddWithValue("@search", $"%{searchKeyword.Trim()}%")
            End If

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvResidences.DataSource = dt
            lblTotalRecords.Text = $"{dt.Rows.Count}"

            If dgvResidences.Columns.Contains("ResidentID") Then
                dgvResidences.Columns("ResidentID").Visible = False
            End If

            ' Programmatically removes the View and Delete buttons from the DataGridView
            If dgvResidences.Columns.Contains("btnView") Then
                dgvResidences.Columns.Remove("btnView")
            End If
            If dgvResidences.Columns.Contains("btnDelete") Then
                dgvResidences.Columns.Remove("btnDelete")
            End If

        Catch ex As Exception
            MsgBox("Error loading residence records: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadSummaryMetrics()
        Try
            connection()

            ' 1. Count residents added today (resets daily)
            Dim queryNewToday As String = "SELECT COUNT(*) FROM residences WHERE AccountStatus <> 'Deleted' AND DATE(CreatedAt) = CURDATE()"
            cmd = New MySqlCommand(queryNewToday, cn)
            Dim newTodayCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            lblNewToday.Text = newTodayCount.ToString()

            ' 2. Count active residents (Change 'Active' if your status keyword is different)
            Dim queryActive As String = "SELECT COUNT(*) FROM residences WHERE AccountStatus = 'Active'"
            cmd = New MySqlCommand(queryActive, cn)
            Dim activeCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            lblActiveResidents.Text = activeCount.ToString()

        Catch ex As Exception
            MsgBox("Error loading summary metrics: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub dgvResidences_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidences.CellDoubleClick
        ' Make sure a valid row was clicked (not the header)
        If e.RowIndex >= 0 Then
            ' 1. Extract the ResidentID from the double-clicked row
            Dim selectedResidentID As Integer = Convert.ToInt32(dgvResidences.Rows(e.RowIndex).Cells("ResidentID").Value)

            ' 2. Clear the existing controls in the main panel
            frmMain.Panel2.Controls.Clear()

            ' 3. Initialize your form with the extracted ID
            Dim editForm As New Barangay_Residences(selectedResidentID)

            ' 4. Set properties to make it act like a docked control instead of a window
            editForm.TopLevel = False
            editForm.FormBorderStyle = FormBorderStyle.None
            editForm.Dock = DockStyle.Fill

            ' 5. Add to panel, bring to front, and show
            frmMain.Panel2.Controls.Add(editForm)
            editForm.BringToFront()
            editForm.Show()
        End If
    End Sub

    Private Sub SetupSearchPlaceholder()
        If txtSearch IsNot Nothing Then
            txtSearch.Text = placeholderText
            txtSearch.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub txtSearch_Enter(sender As Object, e As EventArgs) Handles txtSearch.Enter
        If txtSearch.Text = placeholderText Then
            txtSearch.Text = ""
            txtSearch.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtSearch_Leave(sender As Object, e As EventArgs) Handles txtSearch.Leave
        If String.IsNullOrWhiteSpace(txtSearch.Text) Then
            SetupSearchPlaceholder()
            LoadResidenceRecords()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text <> placeholderText Then
            LoadResidenceRecords(txtSearch.Text)
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        SetupSearchPlaceholder()
        LoadResidenceRecords()
        LoadSummaryMetrics()
    End Sub

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

        Dim headerStyle As New DataGridViewCellStyle()
        headerStyle.BackColor = Color.FromArgb(10, 25, 100)
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

    Private Sub btnCreateRequest_Click(sender As Object, e As EventArgs) Handles btnCreateRequest.Click
        Dim addForm As New Barangay_Residences()

        ' Ensure the new creation form is also docked inside frmMain.Panel2
        frmMain.Panel2.Controls.Clear()
        addForm.TopLevel = False
        addForm.FormBorderStyle = FormBorderStyle.None
        addForm.Dock = DockStyle.Fill
        frmMain.Panel2.Controls.Add(addForm)
        addForm.Show()
    End Sub

End Class