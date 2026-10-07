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
        LoadSummaryMetrics()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblDateTime.Text = DateTime.Now.ToString("F")
    End Sub

    Private Sub LoadResidenceRecords(Optional searchKeyword As String = "")
        Try
            connection()

            ' Sinasala ang query para itago sa DataGridView ang mga may status na 'Removed'
            sql = "SELECT ResidentID, ResidentCode AS 'Resident Code', FullName AS 'Full Name', Gender, " &
                  "Birthday, MobileNumber AS 'Mobile No.', Email, CivilStatus AS 'Civil Status', " &
                  "Address, SatelliteOffice AS 'Satellite Office', AccountStatus AS 'Status', CreatedAt AS 'Date Registered' " &
                  "FROM residences WHERE AccountStatus <> 'Removed' "

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

            If dgvResidences.Columns.Contains("btnView") Then
                dgvResidences.Columns.Remove("btnView")
            End If

            ' MAG-ADD NG DELETE BUTTON KUNG WALA PA
            If Not dgvResidences.Columns.Contains("btnDelete") Then
                Dim btnDelete As New DataGridViewButtonColumn()
                btnDelete.Name = "btnDelete"
                btnDelete.HeaderText = "Action"
                btnDelete.Text = "Delete"
                btnDelete.UseColumnTextForButtonValue = True
                btnDelete.FlatStyle = FlatStyle.Standard

                dgvResidences.Columns.Add(btnDelete)
            End If

            If dgvResidences.Columns.Contains("btnDelete") Then
                dgvResidences.Columns("btnDelete").DisplayIndex = dgvResidences.Columns.Count - 1
            End If

        Catch ex As Exception
            MsgBox("Error loading residence records: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    ' --- CUSTOM PAINT PARA SA SMOOTH ROUNDED DARK RED BUTTON ---
    Private Sub dgvResidences_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvResidences.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 AndAlso dgvResidences.Columns(e.ColumnIndex).Name = "btnDelete" Then
            e.Paint(e.CellBounds, DataGridViewPaintParts.All)

            ' I-on ang Anti-Alias para mawala ang pixelated edges
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias

            Dim buttonRect As Rectangle = e.CellBounds
            buttonRect.Inflate(-6, -4) ' Margin sa paligid ng button

            ' Gumawa ng rounded path para sa smooth pill style
            Using path As System.Drawing.Drawing2D.GraphicsPath = GetRoundedRectPath(buttonRect, 8)
                ' Kulay Dark Red
                Using brush As New SolidBrush(Color.FromArgb(130, 0, 0))
                    e.Graphics.FillPath(brush, path)
                End Using

                ' Smooth border line
                Using pen As New Pen(Color.FromArgb(90, 0, 0), 1)
                    e.Graphics.DrawPath(pen, path)
                End Using
            End Using

            ' Pagsulat ng "DELETE" text sa gitna
            TextRenderer.DrawText(e.Graphics, "DELETE", New Font("Segoe UI", 8.5F, FontStyle.Bold), buttonRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)

            e.Handled = True
        End If
    End Sub

    ' Helper function para sa smooth rounded corners
    Private Function GetRoundedRectPath(rect As Rectangle, radius As Integer) As System.Drawing.Drawing2D.GraphicsPath
        Dim path As New System.Drawing.Drawing2D.GraphicsPath()
        Dim d As Integer = radius * 2
        path.AddArc(rect.X, rect.Y, d, d, 180, 90)
        path.AddArc(rect.X + rect.Width - d, rect.Y, d, d, 270, 90)
        path.AddArc(rect.X + rect.Width - d, rect.Y + rect.Height - d, d, d, 0, 90)
        path.AddArc(rect.X, rect.Y + rect.Height - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    ' --- DELETE BUTTON CLICK EVENT HANDLER ---
    Private Sub dgvResidences_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidences.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If dgvResidences.Columns(e.ColumnIndex).Name = "btnDelete" Then
                Dim cellId = dgvResidences.Rows(e.RowIndex).Cells("ResidentID").Value
                If cellId Is Nothing OrElse IsDBNull(cellId) Then Return

                Dim residentID As Integer = Convert.ToInt32(cellId)
                Dim residentCode As String = If(dgvResidences.Rows(e.RowIndex).Cells("Resident Code").Value IsNot Nothing, dgvResidences.Rows(e.RowIndex).Cells("Resident Code").Value.ToString(), "")
                Dim residentName As String = If(dgvResidences.Rows(e.RowIndex).Cells("Full Name").Value IsNot Nothing, dgvResidences.Rows(e.RowIndex).Cells("Full Name").Value.ToString(), "")
                Dim email As String = If(dgvResidences.Rows(e.RowIndex).Cells("Email").Value IsNot Nothing, dgvResidences.Rows(e.RowIndex).Cells("Email").Value.ToString(), "")
                Dim satelliteOffice As String = If(dgvResidences.Rows(e.RowIndex).Cells("Satellite Office").Value IsNot Nothing, dgvResidences.Rows(e.RowIndex).Cells("Satellite Office").Value.ToString(), "")

                ' Ipasa ang residentID kasama ang iba pang detalye sa frmDeleteReason
                Using deleteForm As New frmDeleteReason(residentID, residentCode, residentName, email, satelliteOffice)
                    If deleteForm.ShowDialog() = DialogResult.OK Then
                        Dim reason As String = deleteForm.DeleteReason

                        Dim confirm As DialogResult = MessageBox.Show($"Sigurado ka ba na gusto mong i-delete si {residentName}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

                        If confirm = DialogResult.Yes Then
                            ExecuteSoftDelete(residentID, residentName, reason)
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ' --- METHOD PARA SA SOFT DELETE (STATUS: REMOVED) ---
    Private Sub ExecuteSoftDelete(residentID As Integer, residentName As String, reason As String)
        Try
            connection()

            ' Ginagawang 'Removed' ang AccountStatus pero nananatili sa database para protektado ang email
            Dim updateSql As String = "UPDATE residences SET AccountStatus = 'Removed', DeleteComment = @reason WHERE ResidentID = @id"
            Using cmdUpdate As New MySqlCommand(updateSql, cn)
                cmdUpdate.Parameters.AddWithValue("@reason", reason)
                cmdUpdate.Parameters.AddWithValue("@id", residentID)
                cmdUpdate.ExecuteNonQuery()
            End Using

            MessageBox.Show("Record successfully removed and hidden from the list.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadResidenceRecords()
            LoadSummaryMetrics()

        Catch ex As Exception
            MessageBox.Show("Error deleting record: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadSummaryMetrics()
        Try
            connection()

            Dim queryNewToday As String = "SELECT COUNT(*) FROM residences WHERE AccountStatus <> 'Removed' AND DATE(CreatedAt) = CURDATE()"
            cmd = New MySqlCommand(queryNewToday, cn)
            Dim newTodayCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            lblNewToday.Text = newTodayCount.ToString()

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
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                If dgvResidences.Columns(e.ColumnIndex).Name = "btnDelete" Then Return

                Dim cellValue = dgvResidences.Rows(e.RowIndex).Cells("ResidentID").Value

                If cellValue IsNot Nothing AndAlso Not IsDBNull(cellValue) Then
                    Dim selectedResidentID As Integer = Convert.ToInt32(cellValue)

                    frmMain.Panel2.Controls.Clear()
                    Dim editForm As New Barangay_Residences(selectedResidentID)
                    editForm.TopLevel = False
                    editForm.FormBorderStyle = FormBorderStyle.None
                    editForm.Dock = DockStyle.Fill

                    frmMain.Panel2.Controls.Add(editForm)
                    editForm.BringToFront()
                    editForm.Show()
                End If
            End If
        Catch ex As Exception
            MsgBox("May problema sa pagbukas ng record: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
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

        frmMain.Panel2.Controls.Clear()
        addForm.TopLevel = False
        addForm.FormBorderStyle = FormBorderStyle.None
        addForm.Dock = DockStyle.Fill
        frmMain.Panel2.Controls.Add(addForm)
        addForm.Show()
    End Sub
End Class