Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D

Public Class frmDepartmentList

    Private Sub frmDepartmentList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StyleDataGridView()
        LoadDepartments()
    End Sub

    Private Sub StyleDataGridView()
        dgvDepartments.EnableHeadersVisualStyles = False
        dgvDepartments.BorderStyle = BorderStyle.None
        dgvDepartments.BackgroundColor = Color.White
        dgvDepartments.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvDepartments.GridColor = Color.FromArgb(220, 224, 230)
        dgvDepartments.RowHeadersVisible = False
        dgvDepartments.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDepartments.MultiSelect = False
        dgvDepartments.AllowUserToAddRows = False
        dgvDepartments.ReadOnly = True

        ' Header Styling
        Dim headerStyle As New DataGridViewCellStyle With {
            .BackColor = Color.FromArgb(248, 249, 252),
            .ForeColor = Color.FromArgb(50, 50, 60),
            .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
            .Alignment = DataGridViewContentAlignment.MiddleLeft,
            .Padding = New Padding(10, 8, 10, 8)
        }
        dgvDepartments.ColumnHeadersDefaultCellStyle = headerStyle
        dgvDepartments.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvDepartments.ColumnHeadersHeight = 40
        dgvDepartments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        ' Default Row Styling
        Dim defaultRowStyle As New DataGridViewCellStyle With {
            .BackColor = Color.White,
            .ForeColor = Color.FromArgb(50, 50, 60),
            .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
            .SelectionBackColor = Color.FromArgb(210, 215, 240),
            .SelectionForeColor = Color.Black,
            .Padding = New Padding(10, 4, 10, 4)
        }

        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle) With {
            .BackColor = Color.FromArgb(235, 237, 255)
        }

        dgvDepartments.DefaultCellStyle = defaultRowStyle
        dgvDepartments.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        dgvDepartments.RowTemplate.Height = 36
        dgvDepartments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Public Sub LoadDepartments(Optional keyword As String = "")
        Try
            DBconnection.connection()

            DBconnection.sql = "SELECT DepartmentID, DepartmentCode AS 'Code', DepartmentName AS 'Department/Office Name', " &
                               "HeadOfOffice AS 'Head of Office', ContactNumber AS 'Contact Number', " &
                               "IF(IsActive = 1, 'Active', 'Inactive') AS 'Status' " &
                               "FROM departments "

            If Not String.IsNullOrWhiteSpace(keyword) Then
                DBconnection.sql &= "WHERE DepartmentCode LIKE @kw OR DepartmentName LIKE @kw OR HeadOfOffice LIKE @kw "
            End If

            DBconnection.sql &= "ORDER BY DepartmentName ASC"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            If Not String.IsNullOrWhiteSpace(keyword) Then
                DBconnection.cmd.Parameters.AddWithValue("@kw", "%" & keyword & "%")
            End If

            Dim da As New MySqlDataAdapter(DBconnection.cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvDepartments.DataSource = dt

            ' Add Action Columns
            AddGridActionButtons()

            ' Highlight Code Column
            If dgvDepartments.Columns.Count > 0 AndAlso dgvDepartments.Columns.Contains("Code") Then
                dgvDepartments.Columns("Code").DefaultCellStyle.BackColor = Color.FromArgb(220, 225, 255)
            End If

            ' Hide DepartmentID
            If dgvDepartments.Columns.Contains("DepartmentID") Then
                dgvDepartments.Columns("DepartmentID").Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading departments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' --- ADD DOCUMENTS / EDIT / REMOVE BUTTON COLUMNS ---
    Private Sub AddGridActionButtons()
        If Not dgvDepartments.Columns.Contains("colDocs") Then
            Dim btnDocsCol As New DataGridViewButtonColumn() With {
                .Name = "colDocs",
                .HeaderText = "Services",
                .FlatStyle = FlatStyle.Flat
            }
            dgvDepartments.Columns.Add(btnDocsCol)
        End If

        If Not dgvDepartments.Columns.Contains("colEdit") Then
            Dim btnEditCol As New DataGridViewButtonColumn() With {
                .Name = "colEdit",
                .HeaderText = "Action",
                .FlatStyle = FlatStyle.Flat
            }
            dgvDepartments.Columns.Add(btnEditCol)
        End If

        If Not dgvDepartments.Columns.Contains("colDelete") Then
            Dim btnDeleteCol As New DataGridViewButtonColumn() With {
                .Name = "colDelete",
                .HeaderText = "",
                .FlatStyle = FlatStyle.Flat
            }
            dgvDepartments.Columns.Add(btnDeleteCol)
        End If
    End Sub

    ' --- CUSTOM DRAW BUTTONS ---
    Private Sub dgvDepartments_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvDepartments.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = dgvDepartments.Columns(e.ColumnIndex).Name

            If colName = "colDocs" OrElse colName = "colEdit" OrElse colName = "colDelete" Then
                e.PaintBackground(e.CellBounds, True)

                Dim buttonColor As Color = Color.Gray
                Dim btnText As String = ""

                If colName = "colDocs" Then
                    buttonColor = Color.FromArgb(40, 167, 69)  ' Green
                    btnText = "DOCUMENTS"
                ElseIf colName = "colEdit" Then
                    buttonColor = Color.FromArgb(10, 25, 100)  ' Navy Blue
                    btnText = "EDIT"
                ElseIf colName = "colDelete" Then
                    buttonColor = Color.FromArgb(178, 34, 34)  ' Deep Red
                    btnText = "REMOVE"
                End If

                Dim buttonRect As New Rectangle(e.CellBounds.X + 4, e.CellBounds.Y + 4, e.CellBounds.Width - 8, e.CellBounds.Height - 8)
                Dim cornerRadius As Integer = 8

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                    Using brush As New SolidBrush(buttonColor)
                        e.Graphics.FillPath(brush, path)
                    End Using
                End Using

                TextRenderer.DrawText(e.Graphics, btnText, New Font("Segoe UI", 7.5F, FontStyle.Bold),
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

    ' --- CELL CLICK HANDLERS ---
    Private Sub dgvDepartments_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDepartments.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim colName As String = dgvDepartments.Columns(e.ColumnIndex).Name

        If colName = "colDocs" OrElse colName = "colEdit" OrElse colName = "colDelete" Then
            Dim cellID = dgvDepartments.Rows(e.RowIndex).Cells("DepartmentID").Value
            Dim cellName = dgvDepartments.Rows(e.RowIndex).Cells("Department/Office Name").Value

            If cellID Is Nothing OrElse IsDBNull(cellID) Then Return

            Dim deptID As Integer = Convert.ToInt32(cellID)
            Dim deptName As String = If(cellName IsNot Nothing, cellName.ToString(), "")

            If colName = "colDocs" Then
                ' OPENS DOCUMENT SERVICES FORM FILTERED BY THIS DEPARTMENT
                Using frmServices As New frmDocumentServices()
                    frmServices.SelectedDepartmentID = deptID
                    frmServices.Text = $"Document Services - {deptName}"
                    frmServices.ShowDialog()
                End Using

            ElseIf colName = "colEdit" Then
                ' OPENS EDIT DEPARTMENT FORM
                Using frmEdit As New frmAddDepartment()
                    frmEdit.SelectedDepartmentID = deptID
                    If frmEdit.ShowDialog() = DialogResult.OK Then
                        LoadDepartments(txtSearch.Text.Trim())
                    End If
                End Using

            ElseIf colName = "colDelete" Then
                ' CONFIRM AND REMOVE DEPARTMENT
                If MessageBox.Show($"Are you sure you want to remove [{deptName}]?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                    RemoveDepartment(deptID)
                End If
            End If
        End If
    End Sub

    Private Sub RemoveDepartment(id As Integer)
        Try
            DBconnection.connection()
            DBconnection.sql = "DELETE FROM departments WHERE DepartmentID = @id"
            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@id", id)

            Dim rows As Integer = DBconnection.cmd.ExecuteNonQuery()
            If rows > 0 Then
                MessageBox.Show("Department removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadDepartments(txtSearch.Text.Trim())
            End If

        Catch ex As Exception
            MessageBox.Show("Error removing department: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadDepartments(txtSearch.Text.Trim())
    End Sub

    Private Sub btnAddDepartment_Click(sender As Object, e As EventArgs) Handles btnAddDepartment.Click
        Using frmAdd As New frmAddDepartment()
            If frmAdd.ShowDialog() = DialogResult.OK Then
                LoadDepartments()
            End If
        End Using
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadDepartments()
    End Sub

End Class