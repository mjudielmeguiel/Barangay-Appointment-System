Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class frmManage_Users

    Private Sub frmManage_Users_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StyleDataGridView()
        LoadAllUsers()
        UpdateUserCounts()
        AttachLabelClickEvents()
    End Sub

#Region "UI & Grid Styling"
    Private Sub StyleDataGridView()
        With DataGridView1
            .EnableHeadersVisualStyles = False
            .BorderStyle = BorderStyle.None
            .BackgroundColor = Color.White
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .GridColor = Color.FromArgb(220, 224, 235)
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AllowUserToAddRows = False
            .ReadOnly = True
            .AllowUserToOrderColumns = False ' ✅ Lock column reordering

            .ColumnHeadersDefaultCellStyle = New DataGridViewCellStyle With {
                .BackColor = Color.FromArgb(235, 238, 250),
                .ForeColor = Color.FromArgb(30, 30, 45),
                .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
                .Alignment = DataGridViewContentAlignment.MiddleLeft,
                .Padding = New Padding(10, 8, 10, 8)
            }
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .ColumnHeadersHeight = 42
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            Dim defaultRowStyle As New DataGridViewCellStyle With {
                .BackColor = Color.FromArgb(242, 245, 255),
                .ForeColor = Color.FromArgb(40, 40, 50),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
                .SelectionBackColor = Color.FromArgb(215, 225, 250),
                .SelectionForeColor = Color.Black,
                .Padding = New Padding(10, 4, 10, 4)
            }

            .DefaultCellStyle = defaultRowStyle
            .AlternatingRowsDefaultCellStyle = New DataGridViewCellStyle(defaultRowStyle) With {.BackColor = Color.White}
            .RowTemplate.Height = 40
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub

    Private Sub ApplyGridFormatting()
        ' ✅ Hide background utility columns
        Dim hiddenCols() As String = {"SourceTable", "SourceID", "CreatedAt"}
        For Each col In hiddenCols
            If DataGridView1.Columns.Contains(col) Then DataGridView1.Columns(col).Visible = False
        Next

        ' ✅ Reorder remaining columns
        Dim order As New List(Of String) From {
            "Staff Code", "Full Name", "Department", "Role", "Email", "Status", "Failed Attempts"
        }

        Dim displayIndex As Integer = 0
        For Each colName In order
            If DataGridView1.Columns.Contains(colName) Then
                DataGridView1.Columns(colName).DisplayIndex = displayIndex
                displayIndex += 1
            End If
        Next

        ' ✅ Remove old action buttons to prevent duplicates
        If DataGridView1.Columns.Contains("colLogout") Then DataGridView1.Columns.Remove("colLogout")
        If DataGridView1.Columns.Contains("colReset") Then DataGridView1.Columns.Remove("colReset")
        If DataGridView1.Columns.Contains("colDelete") Then DataGridView1.Columns.Remove("colDelete")

        ' ✅ Add Action Buttons at the very end
        Dim btnLogoutCol As New DataGridViewButtonColumn() With {
            .Name = "colLogout", .HeaderText = "Log Out", .Text = "LOG OUT", .UseColumnTextForButtonValue = True, .FlatStyle = FlatStyle.Flat, .Width = 75
        }
        Dim btnResetCol As New DataGridViewButtonColumn() With {
            .Name = "colReset", .HeaderText = "Reset PW", .Text = "RESET", .UseColumnTextForButtonValue = True, .FlatStyle = FlatStyle.Flat, .Width = 85
        }
        Dim btnDelCol As New DataGridViewButtonColumn() With {
            .Name = "colDelete", .HeaderText = "Action", .Text = "DELETE", .UseColumnTextForButtonValue = True, .FlatStyle = FlatStyle.Flat, .Width = 75
        }

        DataGridView1.Columns.Add(btnLogoutCol)
        DataGridView1.Columns.Add(btnResetCol)
        DataGridView1.Columns.Add(btnDelCol)
    End Sub

    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = DataGridView1.Columns(e.ColumnIndex).Name

            If colName = "colDelete" OrElse colName = "colReset" OrElse colName = "colLogout" Then
                e.PaintBackground(e.CellBounds, True)
                Dim btnRect = New Rectangle(e.CellBounds.X + 4, e.CellBounds.Y + 6, e.CellBounds.Width - 8, e.CellBounds.Height - 12)
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

                Dim bgColor As Color
                Dim btnText As String = ""

                ' Assign colors based on button type
                Select Case colName
                    Case "colLogout"
                        bgColor = Color.FromArgb(100, 100, 100) ' Gray
                        btnText = "LOG OUT"
                    Case "colReset"
                        bgColor = ColorTranslator.FromHtml("#192a56") ' Requested Dark Blue
                        btnText = "RESET PW"
                    Case "colDelete"
                        bgColor = Color.FromArgb(180, 30, 30) ' Red
                        btnText = "DELETE"
                End Select

                Using path = GetRoundedPath(btnRect, 10), brush = New SolidBrush(bgColor)
                    e.Graphics.FillPath(brush, path)
                End Using

                TextRenderer.DrawText(e.Graphics, btnText, New Font("Segoe UI", 7.5F, FontStyle.Bold),
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
#End Region

#Region "Data Loading & Filtering"
    ' Centralized base query used by all loaders
    Private ReadOnly Property BaseUserQuery As String
        Get
            Return "
                SELECT 'admin' AS SourceTable, AdminID AS SourceID, '' AS `Staff Code`, 
                       CONCAT(Lastname, ', ', Firstname) AS `Full Name`, '' AS Department, 
                       Role, '' AS Email, IFNULL(AccountStatus, 'New') AS `Status`, 
                       LoginAttempts AS `Failed Attempts`, NULL AS CreatedAt 
                FROM admin 
                UNION ALL 
                SELECT 'users' AS SourceTable, UserID AS SourceID, StaffCode AS `Staff Code`, 
                       CONCAT(Lastname, ', ', Firstname) AS `Full Name`, Department, 
                       Role, Email, IFNULL(AccountStatus, 'New') AS `Status`, 
                       LoginAttempts AS `Failed Attempts`, CreatedAt 
                FROM users"
        End Get
    End Property

    ' Single helper method to handle database execution for the grid
    Private Sub LoadDataToGrid(query As String, Optional paramName As String = Nothing, Optional paramValue As String = Nothing)
        Try
            connection()
            If cn.State = ConnectionState.Open Then cn.Close()
            cn.Open()

            Dim dt As New DataTable()
            Using cmd As New MySqlCommand(query, cn)
                If Not String.IsNullOrEmpty(paramName) Then
                    cmd.Parameters.AddWithValue(paramName, paramValue)
                End If

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using

            DataGridView1.DataSource = Nothing
            DataGridView1.DataSource = dt

            ApplyGridFormatting()
            UpdateUserCounts()
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadAllUsers(Optional ByVal searchQuery As String = "")
        Dim sql As String = $"SELECT * FROM ({BaseUserQuery}) AS combined "
        Dim paramName As String = Nothing
        Dim paramValue As String = Nothing

        If Not String.IsNullOrWhiteSpace(searchQuery) Then
            sql &= "WHERE `Full Name` LIKE @search OR Role LIKE @search OR Email LIKE @search OR `Status` LIKE @search OR `Staff Code` LIKE @search "
            paramName = "@search"
            paramValue = "%" & searchQuery & "%"
        End If

        sql &= "ORDER BY `Full Name` ASC"
        LoadDataToGrid(sql, paramName, paramValue)
    End Sub

    Private Sub FilterByStatus(statusValue As String)
        txtSearch.Clear()
        Dim sql As String = $"SELECT * FROM ({BaseUserQuery}) AS combined WHERE `Status` = @stat ORDER BY `Full Name` ASC"
        LoadDataToGrid(sql, "@stat", statusValue)
    End Sub

    Private Sub FilterByToday()
        txtSearch.Clear()
        ' Filters the grid to only show users created today
        Dim sql As String = $"SELECT * FROM ({BaseUserQuery}) AS combined WHERE DATE(CreatedAt) = CURDATE() ORDER BY `Full Name` ASC"
        LoadDataToGrid(sql)
    End Sub
#End Region

#Region "Counts & Dashboard"
    Private Sub UpdateUserCounts()
        Try
            connection()
            If cn.State = ConnectionState.Open Then cn.Close()
            cn.Open()

            ' 1. AUTO-RESET LOGIC (Every 24 Hours)
            Dim resetSql As String = "
                UPDATE users SET AccountStatus = 'Active' WHERE (AccountStatus IS NULL OR AccountStatus IN ('', 'New')) AND CreatedAt < NOW() - INTERVAL 24 HOUR;
                UPDATE users SET AccountStatus = 'Active', LoginAttempts = 0, LockoutExpiry = NULL WHERE AccountStatus = 'Locked' AND UpdatedAt < NOW() - INTERVAL 24 HOUR;"
            Using cmdReset As New MySqlCommand(resetSql, cn)
                Try : cmdReset.ExecuteNonQuery() : Catch : End Try
            End Using

            Dim resetAdminSql As String = "
                UPDATE admin SET AccountStatus = 'Active' WHERE (AccountStatus IS NULL OR AccountStatus IN ('', 'New')) AND CreatedAt < NOW() - INTERVAL 24 HOUR;
                UPDATE admin SET AccountStatus = 'Active', LoginAttempts = 0 WHERE AccountStatus = 'Locked' AND UpdatedAt < NOW() - INTERVAL 24 HOUR;"
            Using cmdResetAdmin As New MySqlCommand(resetAdminSql, cn)
                Try : cmdResetAdmin.ExecuteNonQuery() : Catch : End Try
            End Using

            ' 2. FETCH COUNTS
            Dim queries As New Dictionary(Of Label, String) From {
                {lblNewAccount, "SELECT COUNT(*) FROM users WHERE DATE(CreatedAt) = CURDATE()"},
                {lblActiveUsers, "SELECT (SELECT COUNT(*) FROM admin WHERE AccountStatus='Active') + (SELECT COUNT(*) FROM users WHERE AccountStatus='Active')"},
                {lblTotalUsers, "SELECT (SELECT COUNT(*) FROM admin) + (SELECT COUNT(*) FROM users)"},
                {lblLockedUsers, "SELECT (SELECT COUNT(*) FROM admin WHERE AccountStatus='Locked') + (SELECT COUNT(*) FROM users WHERE AccountStatus='Locked')"}
            }

            For Each kvp In queries
                Using cmd As New MySqlCommand(kvp.Value, cn)
                    Try
                        kvp.Key.Text = Convert.ToInt32(cmd.ExecuteScalar()).ToString()
                    Catch
                        kvp.Key.Text = "0"
                    End Try
                End Using
            Next

        Catch ex As Exception
            MessageBox.Show("Count error: " & ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            CloseConnection()
        End Try
    End Sub
#End Region

#Region "Form Events"
    Private Sub AttachLabelClickEvents()
        AddHandler lblNewAccount.Click, Sub() FilterByToday()
        AddHandler lblActiveUsers.Click, Sub() FilterByStatus("Active")
        AddHandler lblLockedUsers.Click, Sub() FilterByStatus("Locked")
        AddHandler lblTotalUsers.Click, Sub()
                                            txtSearch.Clear()
                                            LoadAllUsers()
                                        End Sub
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadAllUsers(txtSearch.Text.Trim())
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadAllUsers()
    End Sub

    Private Sub btnCreatenew_Click(sender As Object, e As EventArgs) Handles btnCreatenew.Click
        LoadFormToPanel(New frmcreateuser())
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim sourceTable = DataGridView1.Rows(e.RowIndex).Cells("SourceTable").Value.ToString()
            Dim sourceID = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("SourceID").Value)
            LoadFormToPanel(New frmcreateuser(sourceID, sourceTable))
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim colName = DataGridView1.Columns(e.ColumnIndex).Name

        If colName = "colDelete" OrElse colName = "colReset" OrElse colName = "colLogout" Then
            Dim fullName = DataGridView1.Rows(e.RowIndex).Cells("Full Name").Value.ToString()
            Dim sourceTable = DataGridView1.Rows(e.RowIndex).Cells("SourceTable").Value.ToString()
            Dim sourceID = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("SourceID").Value)
            Dim idCol = If(sourceTable = "admin", "AdminID", "UserID")

            Try
                connection()
                If cn.State = ConnectionState.Open Then cn.Close()
                cn.Open()

                Select Case colName
                    Case "colLogout"
                        If MessageBox.Show($"Force log out {fullName}?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Using cmd As New MySqlCommand($"UPDATE {sourceTable} SET AccountStatus = 'Offline' WHERE {idCol} = @uid", cn)
                                cmd.Parameters.AddWithValue("@uid", sourceID)
                                cmd.ExecuteNonQuery()
                            End Using
                            MessageBox.Show("User has been logged out.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Case "colReset"
                        If MessageBox.Show($"Reset password to 'Password1*' and force log out {fullName}?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                            ' Note: Updates password and forces 'Offline' to immediately log them out
                            Using cmd As New MySqlCommand($"UPDATE {sourceTable} SET Password = 'Password1*', AccountStatus = 'Offline' WHERE {idCol} = @uid", cn)
                                cmd.Parameters.AddWithValue("@uid", sourceID)
                                cmd.ExecuteNonQuery()
                            End Using
                            MessageBox.Show("Password reset successfully. The user has been logged out.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Case "colDelete"
                        If MessageBox.Show($"Are you sure you want to delete:{vbCrLf}{fullName}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Using cmd As New MySqlCommand($"DELETE FROM {sourceTable} WHERE {idCol} = @uid", cn)
                                cmd.Parameters.AddWithValue("@uid", sourceID)
                                cmd.ExecuteNonQuery()
                            End Using
                            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                End Select

                ' Refresh grid and counts after any action
                LoadAllUsers()

            Catch ex As Exception
                MessageBox.Show("Action error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                CloseConnection()
            End Try
        End If
    End Sub

    ' Helper to load forms into the main panel cleanly
    Private Sub LoadFormToPanel(frm As Form)
        frmMain.Panel2.Controls.Clear()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        frmMain.Panel2.Controls.Add(frm)
        frm.BringToFront()
        frm.Show()
    End Sub
#End Region

End Class