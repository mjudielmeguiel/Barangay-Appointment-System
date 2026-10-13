Imports System.Net
Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class frmResidence_Records
    Private ReadOnly placeholderText As String = "Search Resident Name..."
    Private IsAdminUser As Boolean = False
    Private ShowDeletionList As Boolean = False

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
        ' Update real-time clock if needed
    End Sub

    Private Sub LoadResidenceRecords(Optional searchKeyword As String = "")
        Try
            connection()
            Dim filterCondition As String
            If ShowDeletionList Then
                filterCondition = "AccountStatus = 'Deactivated' "
            Else
                filterCondition = "AccountStatus NOT IN ('Deactivated', 'Removed') "
            End If

            sql = "SELECT ResidentID, ResidentCode AS 'Resident Co.', FullName AS 'Full Name', Gender, " &
                  "Birthday, MobileNumber AS 'Mobile No.', Email, CivilStatus AS 'Civil Status', " &
                  "Address, SatelliteOffice AS 'Satellite Off.', AccountStatus AS 'Status', " &
                  "CreatedAt AS 'Date Registed', DeleteComment AS 'Deactivation Reason', " &
                  "RequestedBy AS 'Requested By', RequestedDate AS 'Request Date' " &
                  "FROM residences WHERE " & filterCondition

            If Not String.IsNullOrEmpty(searchKeyword) AndAlso searchKeyword <> placeholderText Then
                sql &= "AND FullName LIKE @search "
            End If

            sql &= "ORDER BY CreatedAt DESC"

            Using cmd As New MySqlCommand(sql, cn)
                If Not String.IsNullOrEmpty(searchKeyword) AndAlso searchKeyword <> placeholderText Then
                    cmd.Parameters.AddWithValue("@search", $"%{searchKeyword.Trim()}%")
                End If

                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvResidences.DataSource = dt
                    lblTotalRecord.Text = $"{dt.Rows.Count}"
                End Using
            End Using

            If dgvResidences.Columns.Contains("ResidentID") Then
                dgvResidences.Columns("ResidentID").Visible = False
            End If

            If dgvResidences.Columns.Contains("Deactivation Reason") Then
                If ShowDeletionList Then
                    dgvResidences.Columns("Deactivation Reason").Visible = True
                    dgvResidences.Columns("Deactivation Reason").HeaderText = "Reason"
                Else
                    dgvResidences.Columns("Deactivation Reason").Visible = False
                End If
            End If

            If dgvResidences.Columns.Contains("Requested By") Then
                dgvResidences.Columns("Requested By").Visible = ShowDeletionList
            End If

            If dgvResidences.Columns.Contains("Request Date") Then
                dgvResidences.Columns("Request Date").Visible = ShowDeletionList
            End If

            SetupActionButtons()
        Catch ex As Exception
            MsgBox("Error loading records: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub lblRequestForDeletion_Click(sender As Object, e As EventArgs) Handles lblRequestForDeletion.Click
        ShowDeletionList = True
        LoadResidenceRecords()
    End Sub

    Private Sub lblTotalRecord_Click(sender As Object, e As EventArgs) Handles lblTotalRecord.Click
        ShowDeletionList = False
        LoadResidenceRecords()
    End Sub

    Private Sub lblActive_Click(sender As Object, e As EventArgs) Handles lblActive.Click
        ShowDeletionList = False
        LoadResidenceRecords()
    End Sub

    Private Sub SetupActionButtons()
        If dgvResidences.Columns.Contains("btnDelete") Then dgvResidences.Columns.Remove("btnDelete")
        If dgvResidences.Columns.Contains("btnRestore") Then dgvResidences.Columns.Remove("btnRestore")
        If dgvResidences.Columns.Contains("btnPermDelete") Then dgvResidences.Columns.Remove("btnPermDelete")

        If ShowDeletionList Then
            dgvResidences.Columns.Add(CreateActionColumn("btnRestore", "Action", "Restore", 100))
            If IsAdminUser Then
                dgvResidences.Columns.Add(CreateActionColumn("btnPermDelete", "Admin", "Delete Permanently", 150))
            End If
        Else
            dgvResidences.Columns.Add(CreateActionColumn("btnDelete", "Action", "Delete", 100))
        End If

        For i As Integer = 0 To dgvResidences.Columns.Count - 1
            Dim n As String = dgvResidences.Columns(i).Name
            If n = "btnDelete" OrElse n = "btnRestore" OrElse n = "btnPermDelete" Then
                dgvResidences.Columns(i).DisplayIndex = dgvResidences.Columns.Count - 1
            End If
        Next
    End Sub

    Private Function CreateActionColumn(name As String, header As String, text As String, minWidth As Integer) As DataGridViewButtonColumn
        Dim col As New DataGridViewButtonColumn()
        col.Name = name
        col.HeaderText = header
        col.Text = text
        col.UseColumnTextForButtonValue = True
        col.FlatStyle = FlatStyle.Flat
        col.MinimumWidth = minWidth
        col.FillWeight = 60
        Return col
    End Function

    Private Sub dgvResidences_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvResidences.CellPainting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return

        Dim colName As String = dgvResidences.Columns(e.ColumnIndex).Name
        Dim btnColor As Color

        Select Case colName
            Case "btnDelete", "btnPermDelete"
                btnColor = Color.FromArgb(128, 0, 0)
            Case "btnRestore"
                btnColor = Color.FromArgb(25, 42, 86)
            Case Else
                Return
        End Select

        e.PaintBackground(e.CellBounds, True)

        Dim rect As New Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + 4, e.CellBounds.Width - 12, e.CellBounds.Height - 8)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Using path As GraphicsPath = RoundedRectPath(rect, 8)
            Using br As New SolidBrush(btnColor)
                e.Graphics.FillPath(br, path)
            End Using
        End Using

        Dim btnText As String = DirectCast(dgvResidences.Columns(e.ColumnIndex), DataGridViewButtonColumn).Text.ToUpper()
        Using f As New Font("Segoe UI", 8, FontStyle.Bold)
            TextRenderer.DrawText(e.Graphics, btnText, f, rect, Color.White,
                                  TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.EndEllipsis)
        End Using

        e.Handled = True
    End Sub

    Private Function RoundedRectPath(r As Rectangle, radius As Integer) As GraphicsPath
        Dim d As Integer = radius * 2
        Dim path As New GraphicsPath()
        path.AddArc(r.X, r.Y, d, d, 180, 90)
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        path.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Private Sub dgvResidences_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidences.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        Dim colName As String = dgvResidences.Columns(e.ColumnIndex).Name

        Dim residentID As Integer = Convert.ToInt32(dgvResidences.Rows(e.RowIndex).Cells("ResidentID").Value)
        Dim residentName As String = If(dgvResidences.Rows(e.RowIndex).Cells("Full Name").Value IsNot Nothing, dgvResidences.Rows(e.RowIndex).Cells("Full Name").Value.ToString(), "")

        Select Case colName
            Case "btnDelete"
                Using deleteForm As New frmDeleteReason(residentID)
                    If deleteForm.ShowDialog() = DialogResult.OK Then
                        Dim confirm As DialogResult = MessageBox.Show($"Move to Request for Deletion:{vbCrLf}{residentName}", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        If confirm = DialogResult.Yes Then
                            ExecuteDeactivate(residentID, deleteForm.DeleteReason)
                        End If
                    End If
                End Using

            Case "btnRestore"
                Dim confirm As DialogResult = MessageBox.Show($"Restore to Active:{vbCrLf}{residentName}", "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    ExecuteRestore(residentID)
                End If

            Case "btnPermDelete"
                If Not IsAdminUser Then Return
                Dim requestedBy As String = If(dgvResidences.Columns.Contains("Requested By") AndAlso dgvResidences.Rows(e.RowIndex).Cells("Requested By").Value IsNot Nothing, dgvResidences.Rows(e.RowIndex).Cells("Requested By").Value.ToString(), "N/A")
                Dim reqDate As String = If(dgvResidences.Columns.Contains("Request Date") AndAlso dgvResidences.Rows(e.RowIndex).Cells("Request Date").Value IsNot Nothing, dgvResidences.Rows(e.RowIndex).Cells("Request Date").Value.ToString(), "N/A")
                Dim reason As String = If(dgvResidences.Columns.Contains("Deactivation Reason") AndAlso dgvResidences.Rows(e.RowIndex).Cells("Deactivation Reason").Value IsNot Nothing, dgvResidences.Rows(e.RowIndex).Cells("Deactivation Reason").Value.ToString(), "")

                Using deleteForm As New frmDeleteReason(residentID, requestedBy, reqDate, reason)
                    If deleteForm.ShowDialog() = DialogResult.OK AndAlso deleteForm.IsPermanentDelete Then
                        ExecutePermanentDelete(residentID)
                    End If
                End Using
        End Select
    End Sub

    Private Sub ExecuteDeactivate(residentID As Integer, reason As String)
        Try
            connection()
            Dim updateSql As String = "UPDATE residences SET AccountStatus = 'Deactivated', DeleteComment = @reason, RequestedBy = @reqBy, RequestedDate = NOW() WHERE ResidentID = @id"
            Using cmdUpdate As New MySqlCommand(updateSql, cn)
                cmdUpdate.Parameters.AddWithValue("@reason", reason)
                cmdUpdate.Parameters.AddWithValue("@reqBy", frmlogin.LoggedInFullname)
                cmdUpdate.Parameters.AddWithValue("@id", residentID)
                cmdUpdate.ExecuteNonQuery()
            End Using
            MessageBox.Show("Moved to Request for Deletion list.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadResidenceRecords()
            LoadSummaryMetrics()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub ExecuteRestore(residentID As Integer)
        Try
            connection()
            Dim updateSql As String = "UPDATE residences SET AccountStatus = 'Active', DeleteComment = NULL, RequestedBy = NULL, RequestedDate = NULL WHERE ResidentID = @id"
            Using cmdUpdate As New MySqlCommand(updateSql, cn)
                cmdUpdate.Parameters.AddWithValue("@id", residentID)
                cmdUpdate.ExecuteNonQuery()
            End Using
            MessageBox.Show("Restored to Active Records.", "Restored", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadResidenceRecords()
            LoadSummaryMetrics()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub ExecutePermanentDelete(residentID As Integer)
        Try
            connection()
            Dim updateSql As String = "UPDATE residences SET AccountStatus = 'Removed', DeleteComment = 'Permanently deleted' WHERE ResidentID = @id"
            Using cmdUpdate As New MySqlCommand(updateSql, cn)
                cmdUpdate.Parameters.AddWithValue("@id", residentID)
                cmdUpdate.ExecuteNonQuery()
            End Using
            MessageBox.Show("Permanently deleted from system.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadResidenceRecords()
            LoadSummaryMetrics()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadSummaryMetrics()
        Try
            connection()

            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM residences WHERE AccountStatus NOT IN ('Deactivated','Removed') AND DATE(CreatedAt) = CURDATE()", cn)
                lblNewResident.Text = cmd.ExecuteScalar().ToString()
            End Using

            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM residences WHERE AccountStatus = 'Active'", cn)
                lblActive.Text = cmd.ExecuteScalar().ToString()
            End Using

            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM residences WHERE AccountStatus NOT IN ('Removed')", cn)
                lblTotalRecord.Text = cmd.ExecuteScalar().ToString()
            End Using

            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM residences WHERE AccountStatus = 'Deactivated'", cn)
                lblRequestForDeletion.Text = cmd.ExecuteScalar().ToString()
            End Using
        Catch ex As Exception
            MsgBox("Error loading summary: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub dgvResidences_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidences.CellDoubleClick
        Try
            If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
            If dgvResidences.Columns(e.ColumnIndex).Name.StartsWith("btn") Then Return

            Dim residentID As Integer = Convert.ToInt32(dgvResidences.Rows(e.RowIndex).Cells("ResidentID").Value)
            frmMain.Panel2.Controls.Clear()

            Dim editForm As New Barangay_Residences(residentID)
            editForm.TopLevel = False
            editForm.FormBorderStyle = FormBorderStyle.None
            editForm.Dock = DockStyle.Fill
            frmMain.Panel2.Controls.Add(editForm)
            editForm.BringToFront()
            editForm.Show()
        Catch ex As Exception
            MsgBox("Error opening record: " & ex.Message, MsgBoxStyle.Critical, "Error")
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

    Public Sub SetUserRole(isAdmin As Boolean)
        IsAdminUser = isAdmin
        LoadResidenceRecords()
        LoadSummaryMetrics()
    End Sub

    Private Sub StyleDataGridView(dgv As DataGridView)
        If dgv Is Nothing Then Return
        dgv.EnableHeadersVisualStyles = False
        dgv.BorderStyle = BorderStyle.None
        dgv.BackgroundColor = Color.White
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.None
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.GridColor = Color.FromArgb(220, 224, 230)
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.AllowUserToResizeRows = False

        Dim headerStyle As New DataGridViewCellStyle()
        headerStyle.BackColor = Color.FromArgb(21, 67, 96)
        headerStyle.ForeColor = Color.White
        headerStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        headerStyle.Padding = New Padding(8, 6, 8, 6)
        dgv.ColumnHeadersDefaultCellStyle = headerStyle
        dgv.ColumnHeadersHeight = 38
        dgv.RowTemplate.Height = 32
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub btnCreateNew_Click(sender As Object, e As EventArgs) Handles btnCreateNew.Click
        Dim addForm As New Barangay_Residences()
        frmMain.Panel2.Controls.Clear()
        addForm.TopLevel = False
        addForm.FormBorderStyle = FormBorderStyle.None
        addForm.Dock = DockStyle.Fill
        frmMain.Panel2.Controls.Add(addForm)
        addForm.Show()
    End Sub
End Class