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
        DataGridView1.AllowUserToOrderColumns = False ' ✅ I-lock ang pagbabago ng pwesto

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
                    '' AS Department,
                    Role,
                    '' AS Email,
                    IFNULL(AccountStatus, 'New') AS `Status`,
                    LoginAttempts AS `Failed Attempts`
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
                    LoginAttempts AS `Failed Attempts`
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
                        '' AS Department,
                        Role,
                        '' AS Email,
                        IFNULL(AccountStatus, 'New') AS `Status`,
                        LoginAttempts AS `Failed Attempts`
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
                        LoginAttempts AS `Failed Attempts`
                    FROM users
                ) AS combined
                WHERE 
                    Username LIKE @search OR `Full Name` LIKE @search OR 
                    Role LIKE @search OR Email LIKE @search OR `Status` LIKE @search OR `Staff Code` LIKE @search
                ORDER BY `Full Name` ASC"
            End If

            Dim dtUsers As New DataTable()
            Using cmdUsers As New MySqlCommand(unionSql, cn)
                If Not String.IsNullOrWhiteSpace(searchQuery) Then
                    cmdUsers.Parameters.AddWithValue("@search", "%" & searchQuery & "%")
                End If
                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()
                Using adapter As New MySqlDataAdapter(cmdUsers)
                    dtUsers.Clear()
                    adapter.Fill(dtUsers)
                End Using
            End Using

            DataGridView1.DataSource = Nothing
            DataGridView1.DataSource = dtUsers

            ' ✅ Itago ang mga hidden columns
            If DataGridView1.Columns.Contains("SourceTable") Then DataGridView1.Columns("SourceTable").Visible = False
            If DataGridView1.Columns.Contains("SourceID") Then DataGridView1.Columns("SourceID").Visible = False

            ' ✅ Ayusin ang pagkakasunod-sunod
            ReorderColumns()

            ' ✅ Idagdag ang Action Buttons sa HULI
            AddGridActionButtons()

            UpdateUserCounts()
        Catch ex As Exception
            MessageBox.Show("Error loading user data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' ✅ Bagong Function — Ayusin ang Column Order
    Private Sub ReorderColumns()
        ' Gustong pagkakasunod-sunod:
        ' Staff Code → Full Name → Department → Role → Email → Status → Failed Attempts → DELETE
        Dim order As New List(Of String) From {
            "Staff Code",
            "Full Name",
            "Department",
            "Role",
            "Email",
            "Status",
            "Failed Attempts"
        }

        Dim displayIndex As Integer = 0
        For Each colName In order
            If DataGridView1.Columns.Contains(colName) Then
                DataGridView1.Columns(colName).DisplayIndex = displayIndex
                displayIndex += 1
            End If
        Next
    End Sub

    Private Sub AddGridActionButtons()
        ' ✅ Alisin muna kung nandoon na
        If DataGridView1.Columns.Contains("colView") Then DataGridView1.Columns.Remove("colView")
        If DataGridView1.Columns.Contains("colDelete") Then DataGridView1.Columns.Remove("colDelete")

        ' ✅ DELETE ONLY — sa pinakahuli
        Dim btnDelCol As New DataGridViewButtonColumn() With {
            .Name = "colDelete",
            .HeaderText = "Action",
            .Text = "DELETE",
            .UseColumnTextForButtonValue = True,
            .FlatStyle = FlatStyle.Flat,
            .Width = 100
        }
        DataGridView1.Columns.Add(btnDelCol)
    End Sub

    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = DataGridView1.Columns(e.ColumnIndex).Name

            ' Only paint the DELETE button now
            If colName = "colDelete" Then
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

    ' ✅ NEW: Double click opens the user profile in the panel
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim sourceTable = DataGridView1.Rows(e.RowIndex).Cells("SourceTable").Value.ToString()
            Dim sourceID = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("SourceID").Value)

            ' 1. Clear main panel
            frmMain.Panel2.Controls.Clear()

            ' 2. Initialize form with ID and Table
            Dim frm As New frmcreateuser(sourceID, sourceTable)

            ' 3. Dock into panel
            frm.TopLevel = False
            frm.FormBorderStyle = FormBorderStyle.None
            frm.Dock = DockStyle.Fill

            ' 4. Show
            frmMain.Panel2.Controls.Add(frm)
            frm.BringToFront()
            frm.Show()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex < 0 Then Return
        Dim colName = DataGridView1.Columns(e.ColumnIndex).Name
        Dim sourceTable = DataGridView1.Rows(e.RowIndex).Cells("SourceTable").Value.ToString()
        Dim sourceID = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("SourceID").Value)
        Dim fullName = DataGridView1.Rows(e.RowIndex).Cells("Full Name").Value.ToString()

        ' View click logic is removed, only Delete remains here
        If colName = "colDelete" Then
            If MessageBox.Show($"Are you sure you want to delete:{vbCrLf}{fullName}?",
                      "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
            Try
                connection()
                Dim idCol = If(sourceTable = "admin", "AdminID", "UserID")
                Using cmd As New MySqlCommand($"DELETE FROM {sourceTable} WHERE {idCol} = @uid", cn)
                    cmd.Parameters.AddWithValue("@uid", sourceID)
                    If cn.State = ConnectionState.Open Then cn.Close()
                    cn.Open()
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
            If cn.State = ConnectionState.Open Then cn.Close()
            cn.Open()

            Using cmdNew As New MySqlCommand("
                SELECT 
                    (SELECT COUNT(*) FROM admin WHERE AccountStatus IS NULL OR AccountStatus IN ('','New')) +
                    (SELECT COUNT(*) FROM users WHERE AccountStatus IS NULL OR AccountStatus IN ('','New'))", cn)
                lblNewAccount.Text = Convert.ToInt32(cmdNew.ExecuteScalar()).ToString()
            End Using

            Using cmdActive As New MySqlCommand("
                SELECT 
                    (SELECT COUNT(*) FROM admin WHERE AccountStatus='Active') +
                    (SELECT COUNT(*) FROM users WHERE AccountStatus='Active')", cn)
                lblActiveUsers.Text = Convert.ToInt32(cmdActive.ExecuteScalar()).ToString()
            End Using

            Using cmdTotal As New MySqlCommand("
                SELECT (SELECT COUNT(*) FROM admin) + (SELECT COUNT(*) FROM users)", cn)
                lblTotalUsers.Text = Convert.ToInt32(cmdTotal.ExecuteScalar()).ToString()
            End Using

            Using cmdLocked As New MySqlCommand("
                SELECT 
                    (SELECT COUNT(*) FROM admin WHERE AccountStatus='Locked') +
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
                       CONCAT(Lastname, ', ', Firstname) AS `Full Name`,
                       '' AS Department,
                       Role,
                       '' AS Email, IFNULL(AccountStatus, 'New') AS `Status`, LoginAttempts AS `Failed Attempts`
                FROM admin
                UNION ALL
                SELECT 'users' AS SourceTable, UserID AS SourceID, StaffCode AS `Staff Code`,
                       CONCAT(Lastname, ', ', Firstname) AS `Full Name`,
                       Department,
                       Role,
                       Email, IFNULL(AccountStatus, 'New') AS `Status`, LoginAttempts AS `Failed Attempts`
                FROM users
            ) AS combined WHERE `Status` = @stat ORDER BY `Full Name` ASC"

            Dim dt As New DataTable()
            If cn.State = ConnectionState.Open Then cn.Close()
            cn.Open()
            Using cmd As New MySqlCommand(filterSql, cn)
                cmd.Parameters.AddWithValue("@stat", statusValue)
                Using adapter As New MySqlDataAdapter(cmd)
                    dt.Clear()
                    adapter.Fill(dt)
                End Using
            End Using
            DataGridView1.DataSource = Nothing
            DataGridView1.DataSource = dt

            If DataGridView1.Columns.Contains("SourceTable") Then DataGridView1.Columns("SourceTable").Visible = False
            If DataGridView1.Columns.Contains("SourceID") Then DataGridView1.Columns("SourceID").Visible = False

            ReorderColumns()
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

    ' ✅ NEW: Creates the user profile inside the panel
    Private Sub btnCreatenew_Click(sender As Object, e As EventArgs) Handles btnCreatenew.Click
        frmMain.Panel2.Controls.Clear()

        Dim frm As New frmcreateuser()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill

        frmMain.Panel2.Controls.Add(frm)
        frm.BringToFront()
        frm.Show()
    End Sub
End Class