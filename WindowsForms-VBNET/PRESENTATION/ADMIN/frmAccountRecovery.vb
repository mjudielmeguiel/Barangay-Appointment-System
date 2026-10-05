Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmAccountRecovery

    ' ============================================================
    ' USER TABLE CONFIGURATION
    ' ============================================================
    Private Const USER_TABLE As String = "users"

    ' Data binding objects
    Private dtRequests As New DataTable()
    Private WithEvents bsRequests As New BindingSource()

    ' Variable para malaman kung kasalukuyang nakikita ang Pending o History Records
    Private isShowingHistory As Boolean = False

    ' ============================================================
    ' FORM LOAD: Initialize DataGridView and load data
    ' ============================================================
    Private Sub frmAccountRecovery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With dgvRequests
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .RowHeadersVisible = False
            .DataSource = bsRequests
        End With

        StyleDataGridView(dgvRequests) ' I-apply ang disenyo

        btnSetActive.Enabled = False
        LoadPendingRequests()
        LoadLockedAccountsCount()
        LoadRecoveredAccountsTodayCount()
    End Sub

    ' ============================================================
    ' 🎨 DATAGRIDVIEW DESIGN
    ' ============================================================
    Private Sub StyleDataGridView(dgv As DataGridView)
        dgv.EnableHeadersVisualStyles = False
        dgv.BorderStyle = BorderStyle.None
        dgv.BackgroundColor = Color.White

        ' Horizontal lines lang
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.GridColor = Color.FromArgb(230, 235, 240)

        ' Header Styling (Dark Navy Blue, White Text)
        Dim headerStyle As New DataGridViewCellStyle()
        headerStyle.BackColor = Color.FromArgb(24, 30, 80) ' Dark Navy Blue
        headerStyle.ForeColor = Color.White
        headerStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        headerStyle.Padding = New Padding(10, 8, 10, 8)

        dgv.ColumnHeadersDefaultCellStyle = headerStyle
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersHeight = 40

        ' Default Row Styling (White background)
        Dim defaultRowStyle As New DataGridViewCellStyle()
        defaultRowStyle.BackColor = Color.White
        defaultRowStyle.ForeColor = Color.FromArgb(40, 40, 50)
        defaultRowStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        defaultRowStyle.SelectionBackColor = Color.FromArgb(210, 220, 240)
        defaultRowStyle.SelectionForeColor = Color.Black

        dgv.DefaultCellStyle = defaultRowStyle
        dgv.RowTemplate.Height = 35
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    ' ============================================================
    ' 🎨 COLUMN AT STATUS COLORING 
    ' ============================================================
    Private Sub dgvRequests_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvRequests.CellFormatting
        Dim dgv As DataGridView = CType(sender, DataGridView)
        If e.RowIndex < 0 Then Return

        ' 1. Kulayan ang background ng "Ticket No." (Light Blue/Gray)
        If dgv.Columns(e.ColumnIndex).Name = "ticket_number" Then
            e.CellStyle.BackColor = Color.FromArgb(225, 230, 245)
        End If

        ' 2. Kulayan ang Status Text
        If dgv.Columns(e.ColumnIndex).Name = "status" AndAlso e.Value IsNot Nothing Then
            Dim statusValue As String = e.Value.ToString().ToUpper().Trim()

            Select Case statusValue
                Case "PENDING"
                    e.CellStyle.ForeColor = Color.FromArgb(204, 153, 0) ' Orange/Gold
                    e.CellStyle.Font = New Font(dgv.Font, FontStyle.Bold)
                Case "ACTIVE"
                    e.CellStyle.ForeColor = Color.FromArgb(0, 120, 200) ' Blue
                    e.CellStyle.Font = New Font(dgv.Font, FontStyle.Bold)
            End Select
        End If
    End Sub

    ' ============================================================
    ' LOAD PENDING REQUESTS
    ' ============================================================
    Private Sub LoadPendingRequests()
        Try
            isShowingHistory = False
            DBconnection.connection()

            DBconnection.sql =
                "SELECT ticket_id, ticket_number, username, full_name, email, " &
                "       requested_at, status, notes " &
                "FROM forgot_password_tickets " &
                "WHERE status = 'PENDING' " &
                "ORDER BY requested_at DESC"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)

            Using da As New MySqlDataAdapter(DBconnection.cmd)
                dtRequests.Clear()
                da.Fill(dtRequests)
                bsRequests.DataSource = dtRequests
            End Using

            If dgvRequests.Columns.Count > 0 Then
                dgvRequests.Columns("ticket_id").Visible = False
                dgvRequests.Columns("ticket_number").HeaderText = "Ticket No."
                dgvRequests.Columns("username").HeaderText = "Username"
                dgvRequests.Columns("full_name").HeaderText = "Full Name"
                dgvRequests.Columns("email").HeaderText = "Email"
                dgvRequests.Columns("requested_at").HeaderText = "Date Requested"
                dgvRequests.Columns("status").HeaderText = "Status"
                dgvRequests.Columns("notes").HeaderText = "Notes"
            End If

            lblCount.Text = dtRequests.Rows.Count.ToString()

        Catch ex As MySqlException
            MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' ============================================================
    ' LOAD ALL RECORDS (HISTORY)
    ' ============================================================
    Private Sub LoadAllTicketHistory()
        Try
            isShowingHistory = True
            DBconnection.connection()

            DBconnection.sql =
                "SELECT ticket_id, ticket_number, username, full_name, email, " &
                "       requested_at, status, notes " &
                "FROM forgot_password_tickets " &
                "ORDER BY requested_at DESC"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)

            Using da As New MySqlDataAdapter(DBconnection.cmd)
                dtRequests.Clear()
                da.Fill(dtRequests)
                bsRequests.DataSource = dtRequests
            End Using

            If dgvRequests.Columns.Count > 0 Then
                dgvRequests.Columns("ticket_id").Visible = False
                dgvRequests.Columns("ticket_number").HeaderText = "Ticket No."
                dgvRequests.Columns("username").HeaderText = "Username"
                dgvRequests.Columns("full_name").HeaderText = "Full Name"
                dgvRequests.Columns("email").HeaderText = "Email"
                dgvRequests.Columns("requested_at").HeaderText = "Date Requested"
                dgvRequests.Columns("status").HeaderText = "Status"
                dgvRequests.Columns("notes").HeaderText = "Notes"
            End If

            lblCount.Text = dtRequests.Rows.Count.ToString()
            btnSetActive.Enabled = False

        Catch ex As MySqlException
            MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub LoadLockedAccountsCount()
        Try
            DBconnection.connection()
            DBconnection.sql = "SELECT COUNT(*) FROM " & USER_TABLE & " WHERE AccountStatus = 'Locked'"
            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            Dim lockedCount As Integer = Convert.ToInt32(DBconnection.cmd.ExecuteScalar())
            lblLockedCount.Text = lockedCount.ToString()
        Catch ex As Exception
            lblLockedCount.Text = "0"
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' ============================================================
    ' 🔢 INAYOS NA COUNT PARA SA RECOVERED NGAYONG ARAW
    ' ============================================================
    Private Sub LoadRecoveredAccountsTodayCount()
        Try
            DBconnection.connection()
            ' Binibilang ang mga na-update ngayong araw (sumasaklaw sa ACTIVE o kaya ay nakabase sa petsa ng pagka-update)
            DBconnection.sql = "SELECT COUNT(*) FROM forgot_password_tickets WHERE (UPPER(status) = 'ACTIVE' OR UPPER(status) = 'APPROVED') AND (DATE(reset_at) = CURDATE() OR DATE(requested_at) = CURDATE())"
            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            Dim recoveredTodayCount As Integer = Convert.ToInt32(DBconnection.cmd.ExecuteScalar())
            lblRecoveredCount.Text = recoveredTodayCount.ToString()
        Catch ex As Exception
            lblRecoveredCount.Text = "0"
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' ============================================================
    ' BUTTON: VIEW HISTORY / TOGGLE RECORDS
    ' ============================================================
    Private Sub btnViewOldRecords_Click(sender As Object, e As EventArgs) Handles btnViewOldRecords.Click
        If isShowingHistory Then
            LoadPendingRequests()
            btnViewOldRecords.Text = "History"
        Else
            LoadAllTicketHistory()
            btnViewOldRecords.Text = "Pending Requests"
        End If
        LoadLockedAccountsCount()
        LoadRecoveredAccountsTodayCount()
    End Sub

    Private Sub btnSetActive_Click(sender As Object, e As EventArgs) Handles btnSetActive.Click
        If isShowingHistory Then Return

        If dgvRequests.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a request from the list.", "No Selection",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row As DataGridViewRow = dgvRequests.SelectedRows(0)
        Dim ticketId As Integer = Convert.ToInt32(row.Cells("ticket_id").Value)
        Dim uname As String = row.Cells("username").Value.ToString()
        Dim tnum As String = row.Cells("ticket_number").Value.ToString()

        Dim ans As DialogResult = MessageBox.Show(
            "Are you sure you want to set this user as ACTIVE?" & vbCrLf &
            "Username: " & uname & vbCrLf &
            "Ticket: " & tnum,
            "Confirm Set Active",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If ans <> DialogResult.Yes Then Return

        Try
            DBconnection.connection()

            ' --- 1) Update the forgot_password_tickets table ---
            DBconnection.sql =
                "UPDATE forgot_password_tickets " &
                "SET status = 'ACTIVE', reset_at = NOW() " &
                "WHERE ticket_id = @tid"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@tid", ticketId)
            DBconnection.cmd.ExecuteNonQuery()

            ' --- 2) Activate the ACCOUNT in the user table ---
            DBconnection.sql =
                "UPDATE " & USER_TABLE & " " &
                "SET AccountStatus = 'Active', " &
                "    LoginAttempts = 0, " &
                "    LockoutExpiry = NULL " &
                "WHERE Username = @uname"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@uname", uname)
            Dim userRows As Integer = DBconnection.cmd.ExecuteNonQuery()

            If userRows = 0 Then
                MessageBox.Show(
                    "The ticket was updated, BUT no user was found with username '" & uname & "' " &
                    "in the '" & USER_TABLE & "' table. " & vbCrLf &
                    "Please verify the USER_TABLE name and ensure the Username matches.",
                    "Warning: Account Not Updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show("Successfully set user '" & uname & "' to ACTIVE.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As MySqlException
            MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            DBconnection.CloseConnection()
        End Try

        LoadPendingRequests()
        LoadLockedAccountsCount()
        LoadRecoveredAccountsTodayCount()
        btnSetActive.Enabled = (dgvRequests.SelectedRows.Count > 0)
    End Sub

    Private Sub dgvRequests_SelectionChanged(sender As Object, e As EventArgs) Handles dgvRequests.SelectionChanged
        If Not isShowingHistory Then
            btnSetActive.Enabled = (dgvRequests.SelectedRows.Count > 0)
        End If
    End Sub

    Private Sub dgvRequests_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRequests.CellDoubleClick
        If e.RowIndex >= 0 AndAlso Not isShowingHistory Then btnSetActive.PerformClick()
    End Sub

End Class