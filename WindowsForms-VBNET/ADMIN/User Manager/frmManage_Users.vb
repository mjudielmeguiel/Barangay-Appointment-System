Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class frmManage_Users

    Private Sub frmManage_Users_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StyleDataGridView()
        LoadAllUsers()
        UpdateUserCounts()
        AttachLabelClickEvents()
    End Sub

    Private Sub StyleDataGridView()
        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.BorderStyle = BorderStyle.None
        DataGridView1.BackgroundColor = Color.White
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        DataGridView1.GridColor = Color.FromArgb(220, 224, 235)
        DataGridView1.RowHeadersVisible = False
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.ReadOnly = True

        Dim headerStyle As New DataGridViewCellStyle With {
            .BackColor = Color.FromArgb(235, 238, 250),
            .ForeColor = Color.FromArgb(30, 30, 45),
            .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
            .Alignment = DataGridViewContentAlignment.MiddleLeft,
            .Padding = New Padding(10, 8, 10, 8)
        }
        DataGridView1.ColumnHeadersDefaultCellStyle = headerStyle
        DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridView1.ColumnHeadersHeight = 42
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        Dim defaultRowStyle As New DataGridViewCellStyle With {
            .BackColor = Color.FromArgb(242, 245, 255),
            .ForeColor = Color.FromArgb(40, 40, 50),
            .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
            .SelectionBackColor = Color.FromArgb(215, 225, 250),
            .SelectionForeColor = Color.Black,
            .Padding = New Padding(10, 4, 10, 4)
        }
        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle) With {.BackColor = Color.White}
        DataGridView1.DefaultCellStyle = defaultRowStyle
        DataGridView1.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        DataGridView1.RowTemplate.Height = 40
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub LoadAllUsers(Optional ByVal searchQuery As String = "")
        Try
            connection()
            Dim unionSql As String

            If String.IsNullOrWhiteSpace(searchQuery) Then
                unionSql = "
                SELECT 
                    'admin' AS SourceTable,
                    AdminID AS SourceID,
                    '' AS `Staff Code`,
                    CONCAT(Lastname, ', ', Firstname) AS `Full Name`,
                    Department,
                    Role,
                    '' AS Email,
                    IFNULL(AccountStatus, 'New') AS `Status`,
                    LoginAttempts AS `Failed Attempts`,
                    Username
                FROM admin
                UNION ALL
                SELECT 
                    'users' AS SourceTable,
                    UserID AS SourceID,
                    StaffCode AS `Staff Code`,
                    CONCAT(Lastname, ', ', Firstname) AS `Full Name`,
                    Department,
                    Role,
                    Email,
                    IFNULL(AccountStatus, 'New') AS `Status`,
                    LoginAttempts AS `Failed Attempts`,
                    Username
                FROM users
                ORDER BY `Full Name` ASC"
            Else
                unionSql = "
                SELECT * FROM (
                    SELECT 
                        'admin' AS SourceTable,
                        AdminID AS SourceID,
                        '' AS `Staff Code`,
                        CONCAT(Lastname, ', ', Firstname) AS `Full Name`,
                        Department,
                        Role,
                        '' AS Email,
                        IFNULL(AccountStatus, 'New') AS `Status`,
                        LoginAttempts AS `Failed Attempts`,
                        Username
                    FROM admin
                    UNION ALL
                    SELECT 
                        'users' AS SourceTable,
                        UserID AS SourceID,
                        StaffCode AS `Staff Code`,
                        CONCAT(Lastname, ', ', Firstname) AS `Full Name`,
                        Department,
                        Role,
                        Email,
                        IFNULL(AccountStatus, 'New') AS `Status`,
                        LoginAttempts AS `Failed Attempts`,
                        Username
                    FROM users
                ) AS combined
                WHERE 
                    Username LIKE @search OR `Full Name` LIKE @search OR Department LIKE @search OR 
                    Role LIKE @search OR Email LIKE @search OR `Status` LIKE @search OR `Staff Code` LIKE @search
                ORDER BY `Full Name` ASC"
            End If

            Dim dtUsers As New DataTable()
            Using cmdUsers As New MySqlCommand(unionSql, cn)
                If Not String.IsNullOrWhiteSpace(searchQuery) Then
                    cmdUsers.Parameters.AddWithValue("@search", "%" & searchQuery & "%")
                End If
                Using adapter As New MySqlDataAdapter(cmdUsers)
                    adapter.Fill(dtUsers)
                End Using
            End Using

            DataGridView1.DataSource = dtUsers
            AddGridActionButtons()

            If DataGridView1.Columns.Contains("SourceTable") Then DataGridView1.Columns("SourceTable").Visible = False
            If DataGridView1.Columns.Contains("SourceID") Then DataGridView1.Columns("SourceID").Visible = False
            If DataGridView1.Columns.Contains("Username") Then DataGridView1.Columns("Username").Visible = False

            If DataGridView1.Columns.Contains("Staff Code") AndAlso
               Not String.IsNullOrEmpty(DataGridView1.Columns("Staff Code").HeaderText) Then
                DataGridView1.Columns("Staff Code").DefaultCellStyle.BackColor = Color.FromArgb(220, 225, 248)
                DataGridView1.Columns("Staff Code").DefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            End If

            UpdateUserCounts()
        Catch ex As Exception
            MessageBox.Show("Error loading user data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub AddGridActionButtons()
        If Not DataGridView1.Columns.Contains("colView") Then
            Dim btnViewCol As New DataGridViewButtonColumn() With {
                .Name = "colView",
                .HeaderText = "Action",
                .FlatStyle = FlatStyle.Flat,
                .Width = 85
            }
            DataGridView1.Columns.Add(btnViewCol)
        End If

        If Not DataGridView1.Columns.Contains("colDelete") Then
            Dim btnDelCol As New DataGridViewButtonColumn() With {
                .Name = "colDelete",
                .HeaderText = "",
                .FlatStyle = FlatStyle.Flat,
                .Width = 95
            }
            DataGridView1.Columns.Add(btnDelCol)
        End If
    End Sub

    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = DataGridView1.Columns(e.ColumnIndex).Name

            If colName = "colView" Then
                e.PaintBackground(e.CellBounds, True)
                Dim btnRect = New Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + 6, e.CellBounds.Width - 12, e.CellBounds.Height - 12)
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                Using path = GetRoundedPath(btnRect, 12), brush = New SolidBrush(Color.FromArgb(10, 25, 100))
                    e.Graphics.FillPath(brush, path)
                End Using
                TextRenderer.DrawText(e.Graphics, "VIEW", New Font("Segoe UI", 8.0F, FontStyle.Bold),
                                     btnRect, Color.White,
                                     TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                e.Handled = True

            ElseIf colName = "colDelete" Then
                e.PaintBackground(e.CellBounds, True)
                Dim btnRect = New Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + 6, e.CellBounds.Width - 12, e.CellBounds.Height - 12)
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                Using path = GetRoundedPath(btnRect, 12), brush = New SolidBrush(Color.FromArgb(180, 30, 30))
                    e.Graphics.FillPath(brush, path)
                End Using
                TextRenderer.DrawText(e.Graphics, "DELETE", New Font("Segoe UI", 8.0F, FontStyle.Bold),
                                     btnRect, Color.White,
                                     TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                e.Handled = True
            End If
        End If
    End Sub

    Private Function GetRoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d = radius * 2
        Dim arc As New Rectangle(rect.Location, New Size(d, d))
        path.AddArc(arc, 180, 90)
        arc.X = rect.Right - d
        path.AddArc(arc, 270, 90)
        arc.Y = rect.Bottom - d
        path.AddArc(arc, 0, 90)
        arc.X = rect.Left
        path.AddArc(arc, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex < 0 Then Return
        Dim colName = DataGridView1.Columns(e.ColumnIndex).Name
        Dim sourceTable = DataGridView1.Rows(e.RowIndex).Cells("SourceTable").Value.ToString()
        Dim sourceID = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("SourceID").Value)
        Dim fullName = DataGridView1.Rows(e.RowIndex).Cells("Full Name").Value.ToString()

        If colName = "colView" Then
            Dim frm As New frmcreateuser(sourceID, sourceTable)
            If frm.ShowDialog() = DialogResult.OK Then LoadAllUsers()

        ElseIf colName = "colDelete" Then
            If MessageBox.Show($"Are you sure you want to delete:{vbCrLf}{fullName}?",
                      "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

            Try
                connection()
                Dim idCol = If(sourceTable = "admin", "AdminID", "UserID")
                Using cmd As New MySqlCommand($"DELETE FROM {sourceTable} WHERE {idCol} = @uid", cn)
                    cmd.Parameters.AddWithValue("@uid", sourceID)
                    cmd.ExecuteNonQuery()
                End Using
                MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadAllUsers()
            Catch ex As Exception
                MessageBox.Show("Delete error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                CloseConnection()
            End Try
        End If
    End Sub

    Private Sub UpdateUserCounts()
        Try
            connection()
            Using cmdNew As New MySqlCommand("
                SELECT (SELECT COUNT(*) FROM admin WHERE AccountStatus IS NULL OR AccountStatus IN ('','New')) +
                       (SELECT COUNT(*) FROM users WHERE AccountStatus IS NULL OR AccountStatus IN ('','New'))", cn)
                lblNewAccount.Text = Convert.ToInt32(cmdNew.ExecuteScalar()).ToString()
            End Using
            Using cmdActive As New MySqlCommand("
                SELECT (SELECT COUNT(*) FROM admin WHERE AccountStatus='Active') +
                       (SELECT COUNT(*) FROM users WHERE AccountStatus='Active')", cn)
                lblActiveUsers.Text = Convert.ToInt32(cmdActive.ExecuteScalar()).ToString()
            End Using
            Using cmdTotal As New MySqlCommand("
                SELECT (SELECT COUNT(*) FROM admin) + (SELECT COUNT(*) FROM users)", cn)
                lblTotalUsers.Text = Convert.ToInt32(cmdTotal.ExecuteScalar()).ToString()
            End Using
            Using cmdLocked As New MySqlCommand("
                SELECT (SELECT COUNT(*) FROM admin WHERE AccountStatus='Locked') +
                       (SELECT COUNT(*) FROM users WHERE AccountStatus='Locked')", cn)
                lblLockedUsers.Text = Convert.ToInt32(cmdLocked.ExecuteScalar()).ToString()
            End Using
        Catch ex As Exception
            MessageBox.Show("Count error: " & ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub AttachLabelClickEvents()
        AddHandler lblNewAccount.Click,
            Sub(s, e)
                FilterByStatus("New")
            End Sub

        AddHandler lblActiveUsers.Click,
            Sub(s, e)
                FilterByStatus("Active")
            End Sub

        AddHandler lblTotalUsers.Click,
            Sub(s, e)
                txtSearch.Clear()
                LoadAllUsers()
            End Sub

        AddHandler lblLockedUsers.Click,
            Sub(s, e)
                FilterByStatus("Locked")
            End Sub
    End Sub

    Private Sub FilterByStatus(statusValue As String)
        txtSearch.Clear()
        Try
            connection()
            Dim filterSql = "
            SELECT * FROM (
                SELECT 'admin' AS SourceTable, AdminID AS SourceID, '' AS `Staff Code`,
                       CONCAT(Lastname, ', ', Firstname) AS `Full Name`, Department, Role,
                       '' AS Email, IFNULL(AccountStatus, 'New') AS `Status`, LoginAttempts AS `Failed Attempts`, Username
                FROM admin
                UNION ALL
                SELECT 'users' AS SourceTable, UserID AS SourceID, StaffCode AS `Staff Code`,
                       CONCAT(Lastname, ', ', Firstname) AS `Full Name`, Department, Role,
                       Email, IFNULL(AccountStatus, 'New') AS `Status`, LoginAttempts AS `Failed Attempts`, Username
                FROM users
            ) AS combined WHERE `Status` = @stat ORDER BY `Full Name` ASC"

            Dim dt As New DataTable()
            Using cmd As New MySqlCommand(filterSql, cn)
                cmd.Parameters.AddWithValue("@stat", statusValue)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using

            DataGridView1.DataSource = dt
            If DataGridView1.Columns.Contains("SourceTable") Then DataGridView1.Columns("SourceTable").Visible = False
            If DataGridView1.Columns.Contains("SourceID") Then DataGridView1.Columns("SourceID").Visible = False
            If DataGridView1.Columns.Contains("Username") Then DataGridView1.Columns("Username").Visible = False
            AddGridActionButtons()
        Catch ex As Exception
            MessageBox.Show("Filter error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadAllUsers(txtSearch.Text.Trim())
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadAllUsers()
    End Sub

    Private Sub btnCreatenew_Click(sender As Object, e As EventArgs) Handles btnCreatenew.Click
        Dim frm As New frmcreateuser()
        frm.ShowDialog()
        LoadAllUsers()
    End Sub

End Class