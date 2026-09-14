Imports System.Drawing
Imports MySql.Data.MySqlClient

Public Class frmAddDepartment

    ' Property to hold the Department ID when editing
    Public Property SelectedDepartmentID As Integer = 0

    Private Sub frmAddDepartment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDocumentListView()
        ClearForm()

        ' If SelectedDepartmentID > 0, load details and documents for editing
        If SelectedDepartmentID > 0 Then
            Me.Text = "Edit Department / Office"
            btnSave.Text = "Update"
            LoadDepartmentDetails()
            LoadDepartmentDocuments()
        Else
            Me.Text = "Add New Department / Office"
            btnSave.Text = "Save"
        End If
    End Sub

    ' --- CONFIGURE LISTVIEW FOR DOCUMENTS ---
    Private Sub InitializeDocumentListView()
        If lvDocuments Is Nothing Then Return

        lvDocuments.View = View.Details
        lvDocuments.FullRowSelect = True
        lvDocuments.GridLines = True
        lvDocuments.Columns.Clear()
        lvDocuments.Columns.Add("Code", 80)
        lvDocuments.Columns.Add("Document / Service Name", 260)
        lvDocuments.Columns.Add("Amount", 100)
        lvDocuments.Columns.Add("Status", 80)
    End Sub

    Private Sub ClearForm()
        txtCode.Clear()
        txtName.Clear()
        txtHead.Clear()
        txtContact.Clear()
        rchDescription.Clear()
        chkIsActive.Checked = True
        If lvDocuments IsNot Nothing Then lvDocuments.Items.Clear()
        ClearError()
    End Sub

    Private Sub ShowError(message As String)
        If lblError IsNot Nothing Then
            lblError.Text = "⚠ " & message
            lblError.ForeColor = Color.Red
            lblError.Visible = True
        End If
    End Sub

    Private Sub ClearError()
        If lblError IsNot Nothing Then
            lblError.Text = ""
            lblError.Visible = False
        End If
    End Sub

    ' --- FETCH EXISTING DEPARTMENT DATA ---
    Private Sub LoadDepartmentDetails()
        DBconnection.connection()
        Try
            DBconnection.sql = "SELECT DepartmentCode, DepartmentName, HeadOfOffice, ContactNumber, Description, IsActive " &
                               "FROM departments WHERE DepartmentID = @id LIMIT 1"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@id", SelectedDepartmentID)
            DBconnection.dr = DBconnection.cmd.ExecuteReader()

            If DBconnection.dr.Read() Then
                txtCode.Text = DBconnection.dr("DepartmentCode").ToString()
                txtName.Text = DBconnection.dr("DepartmentName").ToString()
                txtHead.Text = If(IsDBNull(DBconnection.dr("HeadOfOffice")), "", DBconnection.dr("HeadOfOffice").ToString())
                txtContact.Text = If(IsDBNull(DBconnection.dr("ContactNumber")), "", DBconnection.dr("ContactNumber").ToString())
                rchDescription.Text = If(IsDBNull(DBconnection.dr("Description")), "", DBconnection.dr("Description").ToString())
                chkIsActive.Checked = Convert.ToBoolean(DBconnection.dr("IsActive"))
            End If

        Catch ex As Exception
            ShowError("Error loading details: " & ex.Message)
        Finally
            If DBconnection.dr IsNot Nothing AndAlso Not DBconnection.dr.IsClosed Then DBconnection.dr.Close()
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' --- FETCH DOCUMENTS FOR THIS DEPARTMENT INTO LISTVIEW ---
    Private Sub LoadDepartmentDocuments()
        If lvDocuments Is Nothing OrElse SelectedDepartmentID <= 0 Then Return

        Try
            DBconnection.connection()
            lvDocuments.Items.Clear()

            DBconnection.sql = "SELECT ServiceCode, ServiceName, Amount, IsActive " &
                               "FROM document_services " &
                               "WHERE DepartmentID = @deptID " &
                               "ORDER BY ServiceName ASC"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@deptID", SelectedDepartmentID)
            DBconnection.dr = DBconnection.cmd.ExecuteReader()

            While DBconnection.dr.Read()
                Dim code As String = DBconnection.dr("ServiceCode").ToString()
                Dim name As String = DBconnection.dr("ServiceName").ToString()
                Dim amount As Decimal = Convert.ToDecimal(DBconnection.dr("Amount"))
                Dim isActive As Boolean = Convert.ToBoolean(DBconnection.dr("IsActive"))

                Dim item As New ListViewItem(code)
                item.SubItems.Add(name)
                item.SubItems.Add("₱ " & amount.ToString("N2"))
                item.SubItems.Add(If(isActive, "Active", "Inactive"))

                If Not isActive Then
                    item.ForeColor = Color.Gray
                End If

                lvDocuments.Items.Add(item)
            End While

        Catch ex As Exception
            ShowError("Error loading documents: " & ex.Message)
        Finally
            If DBconnection.dr IsNot Nothing AndAlso Not DBconnection.dr.IsClosed Then DBconnection.dr.Close()
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' --- SAVE / UPDATE BUTTON ---
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ClearError()

        If String.IsNullOrWhiteSpace(txtCode.Text) Then
            ShowError("Please enter a Department Code!")
            txtCode.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtName.Text) Then
            ShowError("Please enter a Department/Office Name!")
            txtName.Focus()
            Return
        End If

        DBconnection.connection()

        Try
            ' Check duplicate code (Excluding current record if editing)
            DBconnection.sql = "SELECT COUNT(*) FROM departments WHERE DepartmentCode = @code AND DepartmentID != @id"
            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@code", txtCode.Text.Trim())
            DBconnection.cmd.Parameters.AddWithValue("@id", SelectedDepartmentID)

            Dim count As Integer = Convert.ToInt32(DBconnection.cmd.ExecuteScalar())
            If count > 0 Then
                ShowError("Department Code already exists! Please use a unique code.")
                txtCode.Focus()
                Return
            End If

            ' Insert or Update query depending on SelectedDepartmentID
            If SelectedDepartmentID = 0 Then
                ' INSERT NEW
                DBconnection.sql = "INSERT INTO departments " &
                                   "(DepartmentCode, DepartmentName, HeadOfOffice, ContactNumber, Description, IsActive, CreatedAt, UpdatedAt) " &
                                   "VALUES (@code, @name, @head, @contact, @desc, @active, NOW(), NOW())"
            Else
                ' UPDATE EXISTING
                DBconnection.sql = "UPDATE departments " &
                                   "SET DepartmentCode = @code, DepartmentName = @name, HeadOfOffice = @head, " &
                                   "    ContactNumber = @contact, Description = @desc, IsActive = @active, UpdatedAt = NOW() " &
                                   "WHERE DepartmentID = @id"
            End If

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@code", txtCode.Text.Trim())
            DBconnection.cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
            DBconnection.cmd.Parameters.AddWithValue("@head", If(String.IsNullOrWhiteSpace(txtHead.Text), DBNull.Value, txtHead.Text.Trim()))
            DBconnection.cmd.Parameters.AddWithValue("@contact", If(String.IsNullOrWhiteSpace(txtContact.Text), DBNull.Value, txtContact.Text.Trim()))
            DBconnection.cmd.Parameters.AddWithValue("@desc", If(String.IsNullOrWhiteSpace(rchDescription.Text), DBNull.Value, rchDescription.Text.Trim()))
            DBconnection.cmd.Parameters.AddWithValue("@active", If(chkIsActive.Checked, 1, 0))
            DBconnection.cmd.Parameters.AddWithValue("@id", SelectedDepartmentID)

            DBconnection.cmd.ExecuteNonQuery()

            Dim msg As String = If(SelectedDepartmentID = 0, "Department added successfully!", "Department updated successfully!")
            MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            ShowError("Error saving department: " & ex.Message)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Input_TextChanged(sender As Object, e As EventArgs) Handles txtCode.TextChanged, txtName.TextChanged
        ClearError()
    End Sub

End Class