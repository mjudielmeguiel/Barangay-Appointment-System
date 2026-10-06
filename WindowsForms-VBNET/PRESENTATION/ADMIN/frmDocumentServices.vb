Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Text
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class frmDocumentServices

    Public Property SelectedCodes As String = ""
    Public Property SelectedNames As String = ""
    Public Property TotalAmount As Decimal = 0.00D
    Private originalCode As String = ""
    Private selectedServiceID As Integer = 0

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

    Private Sub LoadServicesToGrid(Optional searchKeyword As String = "")
        Try
            DBconnection.connection()
            Dim query As String = "SELECT d.ID AS 'ServiceInternalID', d.ServiceCode AS 'Code', d.ServiceName AS 'Document / Service', " &
                                   "d.Amount, IFNULL(dept.DepartmentName, 'N/A') AS 'Department', d.DepartmentID, d.DocumentTemplate " &
                                   "FROM document_services d " &
                                   "LEFT JOIN departments dept ON d.DepartmentID = dept.DepartmentID " &
                                   "WHERE d.IsActive = 1 "

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

            If dgvServices.Columns.Contains("ServiceInternalID") Then dgvServices.Columns("ServiceInternalID").Visible = False
            If dgvServices.Columns.Contains("DocumentTemplate") Then dgvServices.Columns("DocumentTemplate").Visible = False
            If dgvServices.Columns.Contains("Code") Then
                dgvServices.Columns("Code").Width = 90
                dgvServices.Columns("Code").DefaultCellStyle.BackColor = Color.FromArgb(200, 215, 240)
                dgvServices.Columns("Code").DefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            End If
            If dgvServices.Columns.Contains("Document / Service") Then dgvServices.Columns("Document / Service").Width = 250
            If dgvServices.Columns.Contains("Amount") Then
                dgvServices.Columns("Amount").Width = 100
                dgvServices.Columns("Amount").DefaultCellStyle.Format = "₱ #,##0.00"
            End If
            If dgvServices.Columns.Contains("Department") Then dgvServices.Columns("Department").Width = 180
            If dgvServices.Columns.Contains("DepartmentID") Then dgvServices.Columns("DepartmentID").Visible = False

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

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadServicesToGrid(txtSearch.Text)
    End Sub

    Private Sub dgvServices_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvServices.CellClick
        If e.RowIndex < 0 Then Return

        Dim selectedRow As DataGridViewRow = dgvServices.Rows(e.RowIndex)
        Dim idValue As String = selectedRow.Cells("ServiceInternalID").Value.ToString()

        selectedServiceID = Convert.ToInt32(idValue)
        originalCode = selectedRow.Cells("Code").Value.ToString()
        lblServiceCode.Text = "Service Code — " & originalCode
        txtServiceName.Text = selectedRow.Cells("Document / Service").Value.ToString()
        txtAmount.Text = Convert.ToDecimal(selectedRow.Cells("Amount").Value).ToString("F2")

        ' ✅ I-load ang template text kung meron man sa database
        If selectedRow.Cells("DocumentTemplate").Value IsNot DBNull.Value Then
            txtTemplatePath.Text = selectedRow.Cells("DocumentTemplate").Value.ToString()
        Else
            txtTemplatePath.Clear()
        End If

        If selectedRow.Cells("DepartmentID").Value IsNot DBNull.Value Then
            cboDepartment.SelectedValue = Convert.ToInt32(selectedRow.Cells("DepartmentID").Value)
        Else
            cboDepartment.SelectedIndex = -1
        End If

        btnSave.Text = "Update Service"
    End Sub

    Private Sub dgvServices_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvServices.CellDoubleClick
        If e.RowIndex < 0 Then Return

        Dim selectedRow As DataGridViewRow = dgvServices.Rows(e.RowIndex)
        Dim idValue As String = selectedRow.Cells("ServiceInternalID").Value.ToString()
        selectedServiceID = Convert.ToInt32(idValue)
        Dim currentServiceName As String = selectedRow.Cells("Document / Service").Value.ToString()

        Dim mainForm As frmMain = TryCast(Application.OpenForms("frmMain"), frmMain)
        If mainForm IsNot Nothing Then
            mainForm.Panel2.Controls.Clear()
            Dim frm As New frmManagePurpose(selectedServiceID, currentServiceName)
            frm.TopLevel = False
            frm.FormBorderStyle = FormBorderStyle.None
            frm.Dock = DockStyle.Fill
            mainForm.Panel2.Controls.Add(frm)
            frm.Show()
        End If
    End Sub

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
        txtTemplatePath.Clear()
        cboDepartment.SelectedIndex = -1
        lblServiceCode.Text = "Service Code — (AUTO)"
        originalCode = ""
        selectedServiceID = 0
        btnSave.Text = "Save Service"
        txtServiceName.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtServiceName.Text) OrElse String.IsNullOrWhiteSpace(txtAmount.Text) Then
            MessageBox.Show("Please enter the service name and amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim deptId As Object = DBNull.Value
        If cboDepartment.SelectedValue IsNot Nothing Then
            deptId = cboDepartment.SelectedValue
        End If

        Dim templateContent As Object = If(String.IsNullOrWhiteSpace(txtTemplatePath.Text), DBNull.Value, txtTemplatePath.Text.Trim())

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

                Using cmd As New MySqlCommand("INSERT INTO document_services (ServiceCode, ServiceName, Amount, DepartmentID, DocumentTemplate) VALUES (@Code, @Name, @Amount, @DeptID, @Template)", DBconnection.cn)
                    cmd.Parameters.AddWithValue("@Code", finalCode)
                    cmd.Parameters.AddWithValue("@Name", txtServiceName.Text.Trim())
                    cmd.Parameters.AddWithValue("@Amount", Decimal.Parse(txtAmount.Text.Trim()))
                    cmd.Parameters.AddWithValue("@DeptID", deptId)
                    cmd.Parameters.AddWithValue("@Template", templateContent)
                    cmd.ExecuteNonQuery()
                End Using

                SaveActivityLog("CREATE", "Added Document Service with Template: " & finalCode)
                MessageBox.Show("Service added successfully! CODE: " & finalCode, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else
                Using cmd As New MySqlCommand("UPDATE document_services SET ServiceName = @Name, Amount = @Amount, DepartmentID = @DeptID, DocumentTemplate = @Template WHERE ServiceCode = @Code", DBconnection.cn)
                    cmd.Parameters.AddWithValue("@Name", txtServiceName.Text.Trim())
                    cmd.Parameters.AddWithValue("@Amount", Decimal.Parse(txtAmount.Text.Trim()))
                    cmd.Parameters.AddWithValue("@DeptID", deptId)
                    cmd.Parameters.AddWithValue("@Template", templateContent)
                    cmd.Parameters.AddWithValue("@Code", originalCode)
                    cmd.ExecuteNonQuery()
                End Using

                SaveActivityLog("UPDATE", "Updated Document Service and Template: " & originalCode)
                MessageBox.Show("Service updated successfully! CODE: " & originalCode, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            LoadServicesToGrid(txtSearch.Text)
            btnClear_Click(sender, e)

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(originalCode) Then
            MessageBox.Show("Please select a service from the list first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim serviceNameText As String = txtServiceName.Text.Trim()
        If MessageBox.Show($"Are you sure you want to delete this Document Service and all its related details?" & vbCrLf & $"{serviceNameText} (CODE: {originalCode})",
                            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            Try
                DBconnection.connection()

                Using cmd As New MySqlCommand("UPDATE document_services SET IsActive = 0 WHERE ServiceCode = @Code", DBconnection.cn)
                    cmd.Parameters.AddWithValue("@Code", originalCode)
                    cmd.ExecuteNonQuery()
                End Using

                Using cmdDetails As New MySqlCommand("DELETE FROM service_details WHERE Service_ID = @Service_ID", DBconnection.cn)
                    cmdDetails.Parameters.AddWithValue("@Service_ID", selectedServiceID)
                    cmdDetails.ExecuteNonQuery()
                End Using

                SaveActivityLog("DELETE", "Deleted Document Service and Details: " & originalCode & " - " & serviceNameText)
                MessageBox.Show("Service deleted successfully along with its details!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LoadServicesToGrid(txtSearch.Text)
                btnClear_Click(sender, e)

            Catch ex As Exception
                MessageBox.Show("Error deleting service: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                DBconnection.CloseConnection()
            End Try
        End If
    End Sub

    ' === BROWSE TEMPLATE FILE BUTTON EVENT (I-save ang File Path) ===
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Word & Document Files (*.docx;*.pdf;*.txt)|*.docx;*.pdf;*.txt|All Files (*.*)|*.*"
            ofd.Title = "Select Document Template File"
            If ofd.ShowDialog() = DialogResult.OK Then
                ' Ilalagay ang buong file path sa textbox
                txtTemplatePath.Text = ofd.FileName
            End If
        End Using
    End Sub
End Class