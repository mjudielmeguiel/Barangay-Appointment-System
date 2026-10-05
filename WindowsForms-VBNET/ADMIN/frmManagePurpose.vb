Imports MySql.Data.MySqlClient

Public Class frmManagePurpose

    Private selectedServiceID As Integer = 0
    Private selectedDetailID As Integer = 0
    Private serviceName As String = ""

    Public Sub New(sID As Integer, sName As String)
        InitializeComponent()
        selectedServiceID = sID
        serviceName = sName
    End Sub

    Private Sub frmManagePurpose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If lblTitle IsNot Nothing Then
            lblTitle.Text = "Managing Purposes for: " & serviceName
        End If
        StyleDataGridView()
        LoadServiceDetails()
    End Sub

    Private Sub StyleDataGridView()
        With dgvDetails
            .EnableHeadersVisualStyles = False
            .BorderStyle = BorderStyle.None
            .BackgroundColor = Color.White
            .GridColor = Color.FromArgb(230, 235, 245)
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AllowUserToAddRows = False
            .ReadOnly = True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            ' Header Styling (Dark Navy Blue)
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 34, 68)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            .ColumnHeadersHeight = 40
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            ' Row Styling
            .RowsDefaultCellStyle.BackColor = Color.White
            .RowsDefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40)
            .RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 242)
            .RowsDefaultCellStyle.SelectionForeColor = Color.Black
            .RowsDefaultCellStyle.Font = New Font("Segoe UI", 9.5, FontStyle.Regular)
            .RowTemplate.Height = 35
        End With
    End Sub

    Private Sub LoadServiceDetails()
        Try
            DBconnection.connection()
            Dim query As String = "SELECT ID, Purpose, Description, Date_Added FROM service_details WHERE Service_ID = @Service_ID"
            Using cmd As New MySqlCommand(query, DBconnection.cn)
                cmd.Parameters.AddWithValue("@Service_ID", selectedServiceID)
                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvDetails.DataSource = dt
                End Using
            End Using

            ' Column Configuration & Header Renaming
            If dgvDetails.Columns.Contains("ID") Then dgvDetails.Columns("ID").Visible = False

            If dgvDetails.Columns.Contains("Purpose") Then
                dgvDetails.Columns("Purpose").HeaderText = "Purpose Code / Title"
                dgvDetails.Columns("Purpose").Width = 180
            End If

            If dgvDetails.Columns.Contains("Description") Then
                dgvDetails.Columns("Description").HeaderText = "Description"
                dgvDetails.Columns("Description").Width = 300
            End If

            If dgvDetails.Columns.Contains("Date_Added") Then
                dgvDetails.Columns("Date_Added").HeaderText = "Date Added"
                dgvDetails.Columns("Date_Added").Width = 120
            End If

        Catch ex As Exception
            MessageBox.Show("ERROR LOADING DETAILS: " & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' This event styles the first column (Purpose) to have a distinct light blue background block[cite: 3]
    Private Sub dgvDetails_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvDetails.CellFormatting
        If e.RowIndex >= 0 AndAlso dgvDetails.Columns(e.ColumnIndex).Name = "Purpose" Then
            e.CellStyle.BackColor = Color.FromArgb(210, 224, 248) ' Soft Periwinkle Blue block[cite: 3]
            e.CellStyle.ForeColor = Color.FromArgb(17, 34, 68)
            e.CellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        End If
    End Sub

    Private Sub dgvDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetails.CellClick
        If e.RowIndex < 0 Then Return

        Dim row As DataGridViewRow = dgvDetails.Rows(e.RowIndex)
        selectedDetailID = Convert.ToInt32(row.Cells("ID").Value)
        txtPurpose.Text = row.Cells("Purpose").Value.ToString()
        txtDescription.Text = row.Cells("Description").Value.ToString()
        btnSave.Text = "Update Purpose"
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtPurpose.Text) Then
            MessageBox.Show("Please enter a purpose.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            DBconnection.connection()

            If selectedDetailID = 0 Then
                Dim query As String = "INSERT INTO service_details (Service_ID, Purpose, Description, Date_Added) VALUES (@Service_ID, @Purpose, @Description, CURDATE())"
                Using cmd As New MySqlCommand(query, DBconnection.cn)
                    cmd.Parameters.AddWithValue("@Service_ID", selectedServiceID)
                    cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text.Trim())
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using
                MessageBox.Show("Purpose added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim query As String = "UPDATE service_details SET Purpose = @Purpose, Description = @Description WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, DBconnection.cn)
                    cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text.Trim())
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim())
                    cmd.Parameters.AddWithValue("@ID", selectedDetailID)
                    cmd.ExecuteNonQuery()
                End Using
                MessageBox.Show("Purpose updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ClearFields()
            LoadServiceDetails()

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If selectedDetailID = 0 Then
            MessageBox.Show("Please select a purpose from the list below to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete this specific purpose?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                DBconnection.connection()
                Dim query As String = "DELETE FROM service_details WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, DBconnection.cn)
                    cmd.Parameters.AddWithValue("@ID", selectedDetailID)
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Purpose deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                LoadServiceDetails()

            Catch ex As Exception
                MessageBox.Show("Error deleting purpose: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                DBconnection.CloseConnection()
            End Try
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        selectedDetailID = 0
        txtPurpose.Clear()
        txtDescription.Clear()
        btnSave.Text = "Save Purpose"
        dgvDetails.ClearSelection()
        txtPurpose.Focus()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

End Class