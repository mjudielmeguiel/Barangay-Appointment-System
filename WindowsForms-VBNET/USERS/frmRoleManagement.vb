Imports MySql.Data.MySqlClient

Public Class frmRoleManagement

    Private Sub frmRoleManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRoles()
        SetupDataGridViewDeleteButton()
    End Sub

    Private Sub SetupDataGridViewDeleteButton()
        dgvRoles.AllowUserToAddRows = False
        If dgvRoles.Columns("DeleteBtn") Is Nothing Then
            Dim btnCol As New DataGridViewButtonColumn()
            btnCol.Name = "DeleteBtn"
            btnCol.HeaderText = "Action"
            btnCol.Text = "Delete"
            btnCol.UseColumnTextForButtonValue = True
            dgvRoles.Columns.Add(btnCol)
        End If
    End Sub

    Private Sub LoadRoles()
        Try
            connection()
            Dim sql As String = "SELECT RoleID, RoleName, Description FROM user_roles ORDER BY RoleName"
            Dim da As New MySqlDataAdapter(sql, cn)
            Dim dt As New DataTable
            da.Fill(dt)

            dgvRoles.DataSource = dt
            dgvRoles.Columns("RoleID").HeaderText = "ID"
            dgvRoles.Columns("RoleName").HeaderText = "Role Name"
            dgvRoles.Columns("Description").HeaderText = "Description"
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Catch ex As Exception
            MsgBox("Error loading records: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If String.IsNullOrWhiteSpace(txtRoleName.Text) Then
            MsgBox("Please enter the role name!", MsgBoxStyle.Exclamation)
            txtRoleName.Focus()
            Return
        End If

        If MsgBox("Save this record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then
            Return
        End If

        Try
            connection()
            Dim sql As String = "INSERT INTO user_roles (RoleName, Description) VALUES (@name, @desc)"
            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@name", txtRoleName.Text.Trim())
                cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim())
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Record saved successfully!", MsgBoxStyle.Information)
            ClearInputs()
            LoadRoles()
        Catch ex As MySqlException
            If ex.Number = 1062 Then
                MsgBox("A role with this name already exists!", MsgBoxStyle.Exclamation)
            Else
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
            End If
        End Try
    End Sub

    Private Sub dgvRoles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRoles.CellContentClick
        If e.ColumnIndex = dgvRoles.Columns("DeleteBtn").Index AndAlso e.RowIndex >= 0 Then
            Dim roleID As Integer = CInt(dgvRoles.Rows(e.RowIndex).Cells("RoleID").Value)

            If roleID = 1 Then
                MsgBox("The Admin role cannot be deleted!", MsgBoxStyle.Exclamation)
                Return
            End If

            If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Try
                    connection()
                    Dim sql As String = "DELETE FROM user_roles WHERE RoleID = @id"
                    Using cmd As New MySqlCommand(sql, cn)
                        cmd.Parameters.AddWithValue("@id", roleID)
                        cmd.ExecuteNonQuery()
                    End Using

                    MsgBox("Record deleted successfully!", MsgBoxStyle.Information)
                    LoadRoles()
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
                End Try
            End If
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If MsgBox("Are you sure you want to exit?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub ClearInputs()
        txtRoleName.Clear()
        txtDescription.Clear()
    End Sub

End Class