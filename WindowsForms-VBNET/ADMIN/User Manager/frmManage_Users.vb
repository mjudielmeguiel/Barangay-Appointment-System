Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class frmManage_Users

    Private Sub frmManage_Users_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StyleDataGridView()
        LoadAllUsers()
        UpdateUserCounts()
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

        ' Header Style
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

        ' Default Row Styling
        Dim defaultRowStyle As New DataGridViewCellStyle With {
            .BackColor = Color.FromArgb(242, 245, 255),
            .ForeColor = Color.FromArgb(40, 40, 50),
            .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
            .SelectionBackColor = Color.FromArgb(215, 225, 250),
            .SelectionForeColor = Color.Black,
            .Padding = New Padding(10, 4, 10, 4)
        }

        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle) With {
            .BackColor = Color.White
        }

        DataGridView1.DefaultCellStyle = defaultRowStyle
        DataGridView1.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        DataGridView1.RowTemplate.Height = 40
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub LoadAllUsers(Optional ByVal searchQuery As String = "")
        Try
            connection()

            Dim querySql As String = ""
            If String.IsNullOrWhiteSpace(searchQuery) Then
                querySql = "SELECT UserID, StaffCode AS 'Staff Code', CONCAT(Lastname, ', ', Firstname) AS 'Full Name', Department, Role, Email, " &
                           "IFNULL(AccountStatus, 'Offline') AS 'Status', LoginAttempts AS 'Failed Attempts' " &
                           "FROM users ORDER BY Lastname ASC, Firstname ASC"
            Else
                querySql = "SELECT UserID, StaffCode AS 'Staff Code', CONCAT(Lastname, ', ', Firstname) AS 'Full Name', Department, Role, Email, " &
                           "IFNULL(AccountStatus, 'Offline') AS 'Status', LoginAttempts AS 'Failed Attempts' " &
                           "FROM users WHERE " &
                           "(Username LIKE @search OR Firstname LIKE @search OR Lastname LIKE @search OR " &
                           "Department LIKE @search OR Role LIKE @search OR " &
                           "StaffCode LIKE @search OR Email LIKE @search OR AccountStatus LIKE @search) " &
                           "ORDER BY Lastname ASC, Firstname ASC"
            End If

            Using cmdUsers As New MySqlCommand(querySql, cn)
                If Not String.IsNullOrWhiteSpace(searchQuery) Then
                    cmdUsers.Parameters.AddWithValue("@search", "%" & searchQuery & "%")
                End If

                Using drUsers As MySqlDataReader = cmdUsers.ExecuteReader()
                    Dim dtUsers As New DataTable()
                    dtUsers.Load(drUsers)
                    DataGridView1.DataSource = dtUsers
                End Using
            End Using

            AddGridActionButtons()

            ' Highlight Staff Code Column
            If DataGridView1.Columns.Contains("Staff Code") Then
                DataGridView1.Columns("Staff Code").DefaultCellStyle.BackColor = Color.FromArgb(220, 225, 248)
                DataGridView1.Columns("Staff Code").DefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            End If

            If DataGridView1.Columns.Contains("UserID") Then
                DataGridView1.Columns("UserID").Visible = False
            End If

            UpdateUserCounts()

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' --- ACTION BUTTON COLUMNS ---
    Private Sub AddGridActionButtons()
        If Not DataGridView1.Columns.Contains("colView") Then
            Dim btnViewCol As New DataGridViewButtonColumn() With {
                .Name = "colView",
                .HeaderText = "Action",
                .FlatStyle = FlatStyle.Flat
            }
            DataGridView1.Columns.Add(btnViewCol)
        End If

        If Not DataGridView1.Columns.Contains("colDeactivate") Then
            Dim btnDeactCol As New DataGridViewButtonColumn() With {
                .Name = "colDeactivate",
                .HeaderText = "",
                .FlatStyle = FlatStyle.Flat
            }
            DataGridView1.Columns.Add(btnDeactCol)
        End If
    End Sub

    ' --- CUSTOM BUTTON DRAWING ---
    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = DataGridView1.Columns(e.ColumnIndex).Name

            If colName = "colView" OrElse colName = "colDeactivate" Then
                e.PaintBackground(e.CellBounds, True)

                Dim buttonColor As Color = Color.Gray
                Dim btnText As String = ""

                If colName = "colView" Then
                    buttonColor = Color.FromArgb(10, 25, 100)
                    btnText = "VIEW"
                ElseIf colName = "colDeactivate" Then
                    buttonColor = Color.FromArgb(178, 34, 34)
                    btnText = "DEACTIVATE"
                End If

                Dim buttonRect As New Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + 6, e.CellBounds.Width - 12, e.CellBounds.Height - 12)
                Dim cornerRadius As Integer = 12

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                    Using brush As New SolidBrush(buttonColor)
                        e.Graphics.FillPath(brush, path)
                    End Using
                End Using

                TextRenderer.DrawText(e.Graphics, btnText, New Font("Segoe UI", 8.0F, FontStyle.Bold),
                                     buttonRect, Color.White,
                                     TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)

                e.Handled = True
            End If
        End If
    End Sub

    Private Function GetRoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim diameter As Integer = radius * 2
        Dim arc As New Rectangle(rect.Location, New Size(diameter, diameter))

        path.AddArc(arc, 180, 90)

        arc.X = rect.Right - diameter
        path.AddArc(arc, 270, 90)

        arc.Y = rect.Bottom - diameter
        path.AddArc(arc, 0, 90)

        arc.X = rect.Left
        path.AddArc(arc, 90, 90)

        path.CloseFigure()
        Return path
    End Function

    ' --- CLICK HANDLERS ---
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim colName As String = DataGridView1.Columns(e.ColumnIndex).Name

        If colName = "colView" OrElse colName = "colDeactivate" Then
            Dim cellID = DataGridView1.Rows(e.RowIndex).Cells("UserID").Value
            Dim cellName = DataGridView1.Rows(e.RowIndex).Cells("Full Name").Value

            If cellID Is Nothing OrElse IsDBNull(cellID) Then Return

            Dim selectedUserID As Integer = Convert.ToInt32(cellID)
            Dim selectedFullName As String = If(cellName IsNot Nothing, cellName.ToString(), "")

            If colName = "colView" Then
                Dim frm As New frmUserInformation(selectedUserID)
                If frm.ShowDialog() = DialogResult.OK Then
                    LoadAllUsers()
                End If

            ElseIf colName = "colDeactivate" Then
                If selectedFullName = LoggedFullname Then
                    MsgBox("You cannot deactivate your own account!", MsgBoxStyle.Exclamation)
                    Return
                End If

                If MsgBox($"Are you sure you want to deactivate user: {selectedFullName}?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Deactivation") = MsgBoxResult.Yes Then
                    DeactivateUser(selectedUserID)
                End If
            End If
        End If
    End Sub

    Private Sub DeactivateUser(id As Integer)
        Try
            connection()
            sql = "UPDATE users SET AccountStatus = 'Offline' WHERE UserID = @uid"
            Using cmdDeact As New MySqlCommand(sql, cn)
                cmdDeact.Parameters.AddWithValue("@uid", id)
                cmdDeact.ExecuteNonQuery()
            End Using

            MsgBox("User account deactivated successfully!", MsgBoxStyle.Information)
            LoadAllUsers()

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub UpdateUserCounts()
        Dim totalCount As Integer = 0
        Dim activeCount As Integer = 0
        Dim lockedCount As Integer = 0

        Try
            If cn.State <> ConnectionState.Open Then connection()

            Using cmdTotal As New MySqlCommand("SELECT COUNT(*) FROM users", cn)
                totalCount = Convert.ToInt32(cmdTotal.ExecuteScalar())
            End Using

            Using cmdActive As New MySqlCommand("SELECT COUNT(*) FROM users WHERE AccountStatus='Active'", cn)
                activeCount = Convert.ToInt32(cmdActive.ExecuteScalar())
            End Using

            Using cmdLocked As New MySqlCommand("SELECT COUNT(*) FROM users WHERE AccountStatus='Locked'", cn)
                lockedCount = Convert.ToInt32(cmdLocked.ExecuteScalar())
            End Using

            lblTotalUsers.Text = "Total Users: " & totalCount
            lblActiveUsers.Text = "Active Users: " & activeCount
            lblLockedUsers.Text = "Locked: " & lockedCount

        Catch ex As Exception
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

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        frmcreateuser.Show()
    End Sub
End Class