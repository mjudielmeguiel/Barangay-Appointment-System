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
        dgvServices.GridColor = Color.FromArgb(210, 220, 235)
        dgvServices.RowHeadersVisible = False
        dgvServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvServices.MultiSelect = False
        dgvServices.AllowUserToAddRows = False
        dgvServices.ReadOnly = True
        dgvServices.EditMode = DataGridViewEditMode.EditProgrammatically

        ' ===== HEADER — Dark Blue Background, White Text =====
        Dim headerStyle As New DataGridViewCellStyle With {
            .BackColor = Color.FromArgb(15, 35, 90),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 9.75F, FontStyle.Bold),
            .Alignment = DataGridViewContentAlignment.MiddleLeft,
            .Padding = New Padding(12, 10, 12, 10)
        }
        dgvServices.ColumnHeadersDefaultCellStyle = headerStyle
        dgvServices.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvServices.ColumnHeadersHeight = 45
        dgvServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        ' ===== ROWS — Alternating Light Blue / White =====
        Dim defaultRowStyle As New DataGridViewCellStyle With {
            .BackColor = Color.FromArgb(230, 235, 245),
            .ForeColor = Color.FromArgb(25, 35, 60),
            .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
            .SelectionBackColor = Color.FromArgb(180, 200, 230),
            .SelectionForeColor = Color.FromArgb(15, 25, 55),
            .Padding = New Padding(12, 6, 12, 6)
        }
        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle) With {
            .BackColor = Color.White
        }
        dgvServices.DefaultCellStyle = defaultRowStyle
        dgvServices.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        dgvServices.RowTemplate.Height = 40
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

    ' ==================================================
    '  MAY SEARCH PARAMETER NA
    ' ==================================================
    Private Sub LoadServicesToGrid(Optional searchKeyword As String = "")
        Try
            DBconnection.connection()
            Dim query As String = "SELECT d.ServiceCode AS 'Code', d.ServiceName AS 'Document / Service', " &
                                 "d.Amount, IFNULL(dept.DepartmentName, 'N/A') AS 'Department', d.DepartmentID " &
                                 "FROM document_services d " &
                                 "LEFT JOIN departments dept ON d.DepartmentID = dept.DepartmentID " &
                                 "WHERE d.IsActive = 1 "

            ' ✅ Magdadagdag ng filter kung may hinahanap
            If Not String.IsNullOrWhiteSpace(searchKeyword) Then
                query &= " AND (d.ServiceCode LIKE @kw OR " &
                         "d.ServiceName LIKE @kw OR " &
                         "IFNULL(dept.DepartmentName, 'N/A') LIKE @kw) "
            End If

            query &= " ORDER BY d.ServiceName ASC"

            Using cmd As New MySqlCommand(query, DBconnection.cn)
                If Not String.IsNullOrWhiteSpace(searchKeyword) Then
                    cmd.Parameters.AddWithValue("@kw", "%" & searchKeyword.Trim() & "%")
                End If

                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvServices.DataSource = dt
                End Using
            End Using

            ' Highlight Code Column
            If dgvServices.Columns.Contains("Code") Then
                dgvServices.Columns("Code").Width = 100
                dgvServices.Columns("Code").DefaultCellStyle.BackColor = Color.FromArgb(200, 215, 240)
                dgvServices.Columns("Code").DefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            End If
            If dgvServices.Columns.Contains("Document / Service") Then
                dgvServices.Columns("Document / Service").Width = 250
            End If
            If dgvServices.Columns.Contains("Amount") Then
                dgvServices.Columns("Amount").Width = 120
                dgvServices.Columns("Amount").DefaultCellStyle.Format = "₱ #,##0.00"
            End If
            If dgvServices.Columns.Contains("Department") Then
                dgvServices.Columns("Department").Width = 180
            End If
            If dgvServices.Columns.Contains("DepartmentID") Then
                dgvServices.Columns("DepartmentID").Visible = False
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

    ' ==================================================
    '  ✅ SEARCH — Habang nagta-type, nagfi-filter
    ' ==================================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadServicesToGrid(txtSearch.Text)
    End Sub

    ' ==================================================
    '  DOUBLE-CLICK — I-load sa form para i-edit
    ' ==================================================
    Private Sub dgvServices_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvServices.CellDoubleClick
        If e.RowIndex < 0 Then Return

        Dim selectedRow As DataGridViewRow = dgvServices.Rows(e.RowIndex)
        originalCode = selectedRow.Cells("Code").Value.ToString()
        lblServiceCode.Text = "Service Code — " & originalCode
        txtServiceName.Text = selectedRow.Cells("Document / Service").Value.ToString()
        txtAmount.Text = Convert.ToDecimal(selectedRow.Cells("Amount").Value).ToString("F2")

        If selectedRow.Cells("DepartmentID").Value IsNot DBNull.Value Then
            cboDepartment.SelectedValue = Convert.ToInt32(selectedRow.Cells("DepartmentID").Value)
        Else
            cboDepartment.SelectedIndex = -1
        End If

        btnSave.Text = "Update Service"
    End Sub

    ' ==================================================
    '  SAVE ACTIVITY LOG
    ' ==================================================
    Private Sub SaveActivityLog(ByVal actionType As String, ByVal details As String)
        Try
            DBconnection.connection()
            Dim sql = "INSERT INTO activity_logs " &
                      "(ActionDate, ActionType, Details, FullName, Module, UserID, UserRole, IPAddress, DeviceInfo) " &
                      "VALUES (NOW(), @action, @details, @fullname, @module, @userid, @role, @ip, @device)"
            Using cmd As New MySqlCommand(sql, DBconnection.cn)
                cmd.Parameters.AddWithValue("@action", actionType)
                cmd.Parameters.AddWithValue("@details", details)
                cmd.Parameters.AddWithValue("@fullname", LoggedInFullname)
                cmd.Parameters.AddWithValue("@module", "Document Services")
                cmd.Parameters.AddWithValue("@userid", LoggedInUserID)
                cmd.Parameters.AddWithValue("@role", If(String.IsNullOrEmpty(LoggedInRole), "Staff", LoggedInRole))
                cmd.Parameters.AddWithValue("@ip", GetLocalIPAddress())
                cmd.Parameters.AddWithValue("@device", Environment.MachineName)

                If DBconnection.cn.State = ConnectionState.Open Then DBconnection.cn.Close()
                DBconnection.cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        Catch
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Function GetLocalIPAddress() As String
        Try
            Dim host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            For Each ip In host.AddressList
                If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
        Catch
        End Try
        Return "Unknown"
    End Function

    Private Sub txtServiceName_TextChanged(sender As Object, e As EventArgs) Handles txtServiceName.TextChanged
        If String.IsNullOrWhiteSpace(originalCode) AndAlso Not String.IsNullOrWhiteSpace(txtServiceName.Text) Then
            Dim autoCode As String = GenerateCodeFromName(txtServiceName.Text)
            lblServiceCode.Text = "Service Code — " & autoCode & " (AUTO)"
        ElseIf String.IsNullOrWhiteSpace(txtServiceName.Text) Then
            lblServiceCode.Text = "Service Code — (AUTO)"
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
        lblServiceCode.Text = "Service Code — (AUTO)"
        originalCode = ""
        btnSave.Text = "Save Service"
        txtServiceName.Focus()
    End Sub

    ' ==================================================
    '  SAVE / UPDATE
    ' ==================================================
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
                ' ===== ADD NEW =====
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

                SaveActivityLog("CREATE", "Added Document Service: " & finalCode & " - " & txtServiceName.Text.Trim())
                MessageBox.Show("NADAGDAG! CODE: " & finalCode, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else
                ' ===== UPDATE =====
                Using cmd As New MySqlCommand("UPDATE document_services SET ServiceName = @Name, Amount = @Amount, DepartmentID = @DeptID WHERE ServiceCode = @Code", DBconnection.cn)
                    cmd.Parameters.AddWithValue("@Name", txtServiceName.Text.Trim())
                    cmd.Parameters.AddWithValue("@Amount", Decimal.Parse(txtAmount.Text.Trim()))
                    cmd.Parameters.AddWithValue("@DeptID", deptId)
                    cmd.Parameters.AddWithValue("@Code", originalCode)
                    cmd.ExecuteNonQuery()
                End Using

                SaveActivityLog("UPDATE", "Updated Document Service: " & originalCode & " - " & txtServiceName.Text.Trim())
                MessageBox.Show("NAI-UPDATE NA! CODE: " & originalCode, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            LoadServicesToGrid(txtSearch.Text) ' ✅ Panatilihin ang search filter pagkatapos mag-save
            btnClear_Click(sender, e)

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' ==================================================
    '  DELETE
    ' ==================================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(originalCode) Then
            MessageBox.Show("Pumili muna ng service gamit ang DOUBLE-CLICK sa listahan.", "PAALALA", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim serviceName As String = txtServiceName.Text.Trim()
        If MessageBox.Show($"Are you sure you want to DELETE this Document Service?" & vbCrLf & $"{serviceName} (CODE: {originalCode})",
                          "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            Try
                DBconnection.connection()
                Using cmd As New MySqlCommand("UPDATE document_services SET IsActive = 0 WHERE ServiceCode = @Code", DBconnection.cn)
                    cmd.Parameters.AddWithValue("@Code", originalCode)
                    cmd.ExecuteNonQuery()
                End Using

                SaveActivityLog("DELETE", "Deleted Document Service: " & originalCode & " - " & serviceName)
                MessageBox.Show("DELETED SUCCESSFULLY! CODE: " & originalCode, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LoadServicesToGrid(txtSearch.Text) ' ✅ Panatilihin ang search filter pagkatapos mag-delete
                btnClear_Click(sender, e)

            Catch ex As Exception
                MessageBox.Show("Error deleting: " & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                DBconnection.CloseConnection()
            End Try
        End If
    End Sub

End Class