Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Text
Imports MySql.Data.MySqlClient

Public Class frmDocumentServices

    Public Property SelectedCodes As String = ""
    Public Property SelectedNames As String = ""
    Public Property TotalAmount As Decimal = 0.00D

    Private originalCode As String = ""

    Private Sub frmDocumentServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StyleDataGridView()
        LoadDepartments()
        LoadServicesToGrid()
    End Sub

    Private Sub StyleDataGridView()
        dgvServices.EnableHeadersVisualStyles = False
        dgvServices.BorderStyle = BorderStyle.None
        dgvServices.BackgroundColor = Color.White
        dgvServices.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvServices.GridColor = Color.FromArgb(230, 233, 245)
        dgvServices.RowHeadersVisible = False
        dgvServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvServices.MultiSelect = False
        dgvServices.AllowUserToAddRows = False
        dgvServices.ReadOnly = True

        ' Header Styling
        Dim headerStyle As New DataGridViewCellStyle With {
            .BackColor = Color.FromArgb(245, 246, 250),
            .ForeColor = Color.FromArgb(30, 35, 50),
            .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
            .Alignment = DataGridViewContentAlignment.MiddleLeft,
            .Padding = New Padding(10, 8, 10, 8)
        }
        dgvServices.ColumnHeadersDefaultCellStyle = headerStyle
        dgvServices.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvServices.ColumnHeadersHeight = 42
        dgvServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        ' Row Styling
        Dim defaultRowStyle As New DataGridViewCellStyle With {
            .BackColor = Color.FromArgb(238, 241, 252),
            .ForeColor = Color.FromArgb(40, 40, 50),
            .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
            .SelectionBackColor = Color.FromArgb(215, 225, 250),
            .SelectionForeColor = Color.Black,
            .Padding = New Padding(10, 4, 10, 4)
        }

        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle) With {
            .BackColor = Color.White
        }

        dgvServices.DefaultCellStyle = defaultRowStyle
        dgvServices.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        dgvServices.RowTemplate.Height = 38
        dgvServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub LoadDepartments()
        Try
            DBconnection.connection()
            cboDepartment.Items.Clear()

            Dim dt As New DataTable()
            Using cmd As New MySqlCommand("SELECT DepartmentID, DepartmentName FROM departments ORDER BY DepartmentName", DBconnection.cn)
                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            cboDepartment.DataSource = dt
            cboDepartment.DisplayMember = "DepartmentName"
            cboDepartment.ValueMember = "DepartmentID"
            cboDepartment.SelectedIndex = -1

        Catch ex As Exception
            MessageBox.Show("ERROR LOADING DEPARTMENTS: " & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub LoadServicesToGrid()
        Try
            DBconnection.connection()

            Dim query As String = "SELECT d.ServiceCode AS 'Code', d.ServiceName AS 'Document / Service', " &
                                 "d.Amount, IFNULL(dept.DepartmentName, 'N/A') AS 'Department', d.DepartmentID " &
                                 "FROM document_services d " &
                                 "LEFT JOIN departments dept ON d.DepartmentID = dept.DepartmentID " &
                                 "WHERE d.IsActive = 1 ORDER BY d.ServiceName ASC"

            Using cmd As New MySqlCommand(query, DBconnection.cn)
                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvServices.DataSource = dt
                End Using
            End Using

            AddGridActionButtons()

            ' Highlight Code Column to match Theme
            If dgvServices.Columns.Contains("Code") Then
                dgvServices.Columns("Code").DefaultCellStyle.BackColor = Color.FromArgb(216, 222, 245)
                dgvServices.Columns("Code").DefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
            End If

            If dgvServices.Columns.Contains("DepartmentID") Then
                dgvServices.Columns("DepartmentID").Visible = False
            End If

            If dgvServices.Columns.Contains("Amount") Then
                dgvServices.Columns("Amount").DefaultCellStyle.Format = "₱ #,##0.00"
            End If

            UpdateTotalDocumentsCount()

        Catch ex As Exception
            MessageBox.Show("ERROR LOADING LIST: " & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub UpdateTotalDocumentsCount()
        If lblTotalDocuments IsNot Nothing Then
            Dim count As Integer = dgvServices.Rows.Count
            lblTotalDocuments.Text = "Total Documents: " & count
        End If
    End Sub

    Private Sub AddGridActionButtons()
        If Not dgvServices.Columns.Contains("colEdit") Then
            Dim btnEditCol As New DataGridViewButtonColumn With {
                .Name = "colEdit",
                .HeaderText = "Action",
                .FlatStyle = FlatStyle.Flat
            }
            dgvServices.Columns.Add(btnEditCol)
        End If

        If Not dgvServices.Columns.Contains("colDelete") Then
            Dim btnDeleteCol As New DataGridViewButtonColumn With {
                .Name = "colDelete",
                .HeaderText = "",
                .FlatStyle = FlatStyle.Flat
            }
            dgvServices.Columns.Add(btnDeleteCol)
        End If
    End Sub

    ' --- CUSTOM BUTTON DRAWING (PILL BUTTONS) ---
    Private Sub dgvServices_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvServices.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = dgvServices.Columns(e.ColumnIndex).Name

            If colName = "colEdit" OrElse colName = "colDelete" Then
                e.PaintBackground(e.CellBounds, True)

                Dim buttonColor As Color = Color.Gray
                Dim btnText As String = ""

                If colName = "colEdit" Then
                    buttonColor = Color.FromArgb(10, 25, 100) ' Navy Blue
                    btnText = "EDIT"
                ElseIf colName = "colDelete" Then
                    buttonColor = Color.FromArgb(178, 34, 34) ' Red
                    btnText = "DELETE"
                End If

                Dim buttonRect As New Rectangle(e.CellBounds.X + 4, e.CellBounds.Y + 5, e.CellBounds.Width - 8, e.CellBounds.Height - 10)
                Dim cornerRadius As Integer = 10

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

    Private Sub dgvServices_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvServices.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim colName As String = dgvServices.Columns(e.ColumnIndex).Name
        Dim selectedRow As DataGridViewRow = dgvServices.Rows(e.RowIndex)

        Dim code As String = selectedRow.Cells("Code").Value.ToString()
        Dim name As String = selectedRow.Cells("Document / Service").Value.ToString()

        If colName = "colEdit" Then
            originalCode = code
            lblServiceCode.Text = "Service Code — " & originalCode
            txtServiceName.Text = name
            txtAmount.Text = Convert.ToDecimal(selectedRow.Cells("Amount").Value).ToString("F2")

            If selectedRow.Cells("DepartmentID").Value IsNot DBNull.Value Then
                cboDepartment.SelectedValue = Convert.ToInt32(selectedRow.Cells("DepartmentID").Value)
            Else
                cboDepartment.SelectedIndex = -1
            End If

        ElseIf colName = "colDelete" Then
            If MessageBox.Show($"Are you sure you want to delete this Document service?" & vbCrLf & $"{name} (CODE: {code})", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                DeleteService(code)
            End If
        End If
    End Sub

    Private Sub DeleteService(code As String)
        Try
            DBconnection.connection()

            Using cmd As New MySqlCommand("UPDATE document_services SET IsActive = 0 WHERE ServiceCode = @Code", DBconnection.cn)
                cmd.Parameters.AddWithValue("@Code", code)
                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("Deleted Successfully! CODE: " & code, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadServicesToGrid()
            btnClear_Click(Nothing, Nothing)

        Catch ex As Exception
            MessageBox.Show("Error deleting: " & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub txtServiceName_TextChanged(sender As Object, e As EventArgs) Handles txtServiceName.TextChanged
        If String.IsNullOrWhiteSpace(originalCode) AndAlso Not String.IsNullOrWhiteSpace(txtServiceName.Text) Then
            Dim autoCode As String = GenerateCodeFromName(txtServiceName.Text)
            lblServiceCode.Text = "Service Code — " & autoCode & " (AWTO)"
        ElseIf String.IsNullOrWhiteSpace(txtServiceName.Text) Then
            lblServiceCode.Text = "Service Code — (AWTO)"
        End If
    End Sub

    Private Function GenerateCodeFromName(fullName As String) As String
        If String.IsNullOrWhiteSpace(fullName) Then Return ""

        Dim words As String() = fullName.ToUpper().Split({" "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim code As New StringBuilder()

        If words.Length = 1 Then
            Dim word = words(0).Trim()
            Return If(word.Length >= 4, word.Substring(0, 4), word.PadRight(4, "X"c)).Substring(0, 4)
        End If

        For Each word In words
            Dim clean = word.Trim()
            If clean.Length > 0 AndAlso Not {"OF", "THE", "AND", "FOR"}.Contains(clean) Then
                code.Append(clean(0))
            End If
        Next

        If code.Length < 4 AndAlso words(0).Length >= code.Length Then
            For i As Integer = code.Length To Math.Min(words(0).Length - 1, 3)
                code.Append(words(0)(i))
            Next
        End If

        Return code.ToString().Replace(" ", "").Substring(0, Math.Min(code.Length, 4))
    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtServiceName.Clear()
        txtAmount.Clear()
        cboDepartment.SelectedIndex = -1
        lblServiceCode.Text = "Service Code — (AWTO)"
        originalCode = ""
        txtServiceName.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtServiceName.Text) OrElse String.IsNullOrWhiteSpace(txtAmount.Text) Then
            MessageBox.Show("ILAGAY ANG PANGALAN AT HALAGA.", "PAALALA", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim deptId As Object = DBNull.Value
        If cboDepartment.SelectedValue IsNot Nothing Then
            deptId = cboDepartment.SelectedValue
        End If

        Try
            DBconnection.connection()

            If String.IsNullOrWhiteSpace(originalCode) Then
                Dim newCode As String = GenerateCodeFromName(txtServiceName.Text.Trim())
                Dim finalCode As String = newCode
                Dim counter As Integer = 1

                Using cmdCheck As New MySqlCommand("SELECT COUNT(*) FROM document_services WHERE ServiceCode = @Code", DBconnection.cn)
                    Do
                        cmdCheck.Parameters.Clear()
                        cmdCheck.Parameters.AddWithValue("@Code", finalCode)
                        Dim exists As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
                        If exists = 0 Then Exit Do
                        finalCode = newCode & counter.ToString()(0)
                        counter += 1
                    Loop
                End Using

                Using cmd As New MySqlCommand("INSERT INTO document_services (ServiceCode, ServiceName, Amount, DepartmentID) VALUES (@Code, @Name, @Amount, @DeptID)", DBconnection.cn)
                    cmd.Parameters.AddWithValue("@Code", finalCode)
                    cmd.Parameters.AddWithValue("@Name", txtServiceName.Text.Trim())
                    cmd.Parameters.AddWithValue("@Amount", Decimal.Parse(txtAmount.Text.Trim()))
                    cmd.Parameters.AddWithValue("@DeptID", deptId)
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("NADAGDAG! CODE: " & finalCode, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Using cmd As New MySqlCommand("UPDATE document_services SET ServiceName = @Name, Amount = @Amount, DepartmentID = @DeptID WHERE ServiceCode = @Code", DBconnection.cn)
                    cmd.Parameters.AddWithValue("@Name", txtServiceName.Text.Trim())
                    cmd.Parameters.AddWithValue("@Amount", Decimal.Parse(txtAmount.Text.Trim()))
                    cmd.Parameters.AddWithValue("@DeptID", deptId)
                    cmd.Parameters.AddWithValue("@Code", originalCode)
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("NAI-UPDATE NA! CODE: " & originalCode, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            LoadServicesToGrid()
            btnClear_Click(sender, e)

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class