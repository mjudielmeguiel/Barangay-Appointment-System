Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Public Class frmPayments

    Public Property SelectedControlNo As String
    Private totalAmount As Decimal = 0
    Private isFirstTimeJobSeeker As Boolean = False

    Private Sub frmPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearError()
        StyleDataGridView(dgvPayments)
        LoadUnpaidAppointmentsGrid()
    End Sub

    ' --- ERROR & SUCCESS LABEL HELPERS ---
    Private Sub ShowError(msg As String)
        If lblError IsNot Nothing Then
            lblError.Text = "⚠ " & msg
            lblError.ForeColor = Color.Red
            lblError.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            lblError.Visible = True
        End If
    End Sub

    Private Sub ShowSuccess(msg As String)
        If lblError IsNot Nothing Then
            lblError.Text = "✔ " & msg
            lblError.ForeColor = Color.FromArgb(16, 124, 65)
            lblError.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            lblError.Visible = True
        End If
    End Sub

    Private Sub ClearError()
        If lblError IsNot Nothing Then
            lblError.Text = ""
            lblError.Visible = False
        End If
    End Sub

    ' --- LOAD UNPAID APPOINTMENTS INTO THE DATAGRIDVIEW ---
    Public Sub LoadUnpaidAppointmentsGrid()
        Try
            DBconnection.connection()

            DBconnection.sql = "SELECT a.AppointmentID, a.ControlNo AS 'Control No.', a.RequestType AS 'Request Type', " &
                               "a.Purpose, a.Department, a.DateSubmitted AS 'Date Submitted', " &
                               "COALESCE(ds.Amount, a.Amount, 0.00) AS DocAmount, " &
                               "a.PaymentStatus, a.Status " &
                               "FROM appointments a " &
                               "LEFT JOIN document_services ds ON a.RequestType = ds.ServiceName " &
                               "WHERE a.Status = 'APPROVED' " &
                               "AND (a.PaymentStatus = 'UNPAID' OR a.PaymentStatus IS NULL OR a.PaymentStatus = '') " &
                               "ORDER BY a.AppointmentID DESC"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            Dim da As New MySqlDataAdapter(DBconnection.cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvPayments.DataSource = dt

            If dgvPayments.Columns.Contains("AppointmentID") Then
                dgvPayments.Columns("AppointmentID").Visible = False
            End If
            If dgvPayments.Columns.Contains("DocAmount") Then
                dgvPayments.Columns("DocAmount").Visible = False
            End If

        Catch ex As Exception
            ShowError("Error loading unpaid list: " & ex.Message)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' --- FETCH APPOINTMENT DETAILS & CHECK FIRST-TIME JOBSEEKER ---
    Private Sub LoadAppointmentDetails(row As DataGridViewRow)
        ClearError()
        SelectedControlNo = row.Cells("Control No.").Value.ToString()
        totalAmount = Convert.ToDecimal(row.Cells("DocAmount").Value)

        Dim reqType As String = row.Cells("Request Type").Value.ToString().ToUpper()
        Dim purposeText As String = row.Cells("Purpose").Value.ToString().ToUpper()

        If reqType.Contains("JOBSEEKER") OrElse purposeText.Contains("JOBSEEKER") OrElse reqType.Contains("FIRST TIME") Then
            isFirstTimeJobSeeker = True
            totalAmount = 0.00
        Else
            isFirstTimeJobSeeker = False
        End If

        UpdatePaymentButtonText()
    End Sub

    ' --- DYNAMIC BUTTON TEXT UPDATER ---
    Private Sub UpdatePaymentButtonText()
        Dim paid As Decimal = 0
        If txtAmountPaid IsNot Nothing Then
            Decimal.TryParse(txtAmountPaid.Text.Trim(), paid)
        End If

        If isFirstTimeJobSeeker Then
            btnMarkPaid.Text = "Waive Fee (Free)"
        ElseIf paid > totalAmount Then
            Dim change = paid - totalAmount
            btnMarkPaid.Text = $"Change: ₱ {change.ToString("N2")}"
        Else
            If totalAmount > 0 Then
                btnMarkPaid.Text = $"Pay: ₱ {totalAmount.ToString("N2")}"
            Else
                btnMarkPaid.Text = "Proceed to Check Out"
            End If
        End If
    End Sub

    ' --- HANDLE CELL CLICKS TO SELECT ROW DETAILS ---
    Private Sub dgvPayments_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPayments.CellMouseClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = dgvPayments.Rows(e.RowIndex)
            LoadAppointmentDetails(selectedRow)
        End If
    End Sub

    ' --- CANCEL APPOINTMENT BUTTON EVENT CONNECTED TO frmCancelReason ---
    Private Sub btnCancelAppointment_Click(sender As Object, e As EventArgs) Handles btnCancelAppointment.Click
        If String.IsNullOrWhiteSpace(SelectedControlNo) Then
            ShowError("Please select an appointment from the list first.")
            Return
        End If

        ' Open frmCancelReason dialog and pull reason text
        Using reasonForm As New frmCancelReason()
            If reasonForm.ShowDialog() = DialogResult.OK Then
                Dim cancelReason As String = reasonForm.ReasonText
                ExecuteCancellation(SelectedControlNo, cancelReason)
            End If
        End Using
    End Sub

    ' --- EXECUTE CANCELLATION WITH AUTOMATIC USER LOOKUP ---
    ' --- EXECUTE CANCELLATION WITH AUTOMATIC USER LOOKUP ---
    Private Sub ExecuteCancellation(ctrlNo As String, reason As String)
        DBconnection.connection()
        Try
            ' -----------------------------------------------------------------
            ' FIX FOR BC30451: 
            ' Replace "Administrator" below with whatever variable or form control 
            ' holds your logged-in user's username (e.g., frmLogin.txtUsername.Text)
            ' -----------------------------------------------------------------
            Dim currentUsername As String = "Administrator"

            ' Automatically query full name formatted as "Lastname, Firstname" from admin or users table
            Dim fullName As String = "System Administrator"

            DBconnection.sql = "SELECT CONCAT(Lastname, ', ', Firstname) AS FullName FROM admin WHERE Username = @Username " &
                               "UNION " &
                               "SELECT CONCAT(Lastname, ', ', Firstname) AS FullName FROM users WHERE Username = @Username LIMIT 1"

            Using nameCmd As New MySqlCommand(DBconnection.sql, DBconnection.cn)
                nameCmd.Parameters.AddWithValue("@Username", currentUsername)
                Dim result = nameCmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    fullName = result.ToString()
                End If
            End Using

            ' Update appointment with cancellation status, reason, and formatted name
            DBconnection.sql = "UPDATE appointments " &
                               "SET Status = 'CANCELLED', " &
                               "    CancellationReason = @Reason, " &
                               "    CancelledBy = @CancelledBy, " &
                               "    UpdatedAt = NOW() " &
                               "WHERE ControlNo = @ControlNo"

            Using updateCmd As New MySqlCommand(DBconnection.sql, DBconnection.cn)
                updateCmd.Parameters.AddWithValue("@Reason", reason)
                updateCmd.Parameters.AddWithValue("@CancelledBy", fullName)
                updateCmd.Parameters.AddWithValue("@ControlNo", ctrlNo)
                updateCmd.ExecuteNonQuery()
            End Using

            ShowSuccess("Appointment successfully cancelled.")

            ' Clear selection and refresh grid so the canceled row automatically hides
            SelectedControlNo = ""
            LoadUnpaidAppointmentsGrid()

        Catch ex As Exception
            ShowError("Error cancelling appointment: " & ex.Message)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' --- DATAGRIDVIEW STYLING ---
    Private Sub StyleDataGridView(dgv As DataGridView)
        dgv.EnableHeadersVisualStyles = False
        dgv.BorderStyle = BorderStyle.None
        dgv.BackgroundColor = Color.White
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.GridColor = Color.FromArgb(220, 224, 230)
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.AllowUserToResizeRows = False

        Dim headerStyle As New DataGridViewCellStyle()
        headerStyle.BackColor = Color.FromArgb(10, 25, 85)
        headerStyle.ForeColor = Color.White
        headerStyle.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        headerStyle.Padding = New Padding(10, 8, 10, 8)
        dgv.ColumnHeadersDefaultCellStyle = headerStyle
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersHeight = 40
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        Dim defaultRowStyle As New DataGridViewCellStyle()
        defaultRowStyle.BackColor = Color.White
        defaultRowStyle.ForeColor = Color.FromArgb(50, 50, 60)
        defaultRowStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        defaultRowStyle.SelectionBackColor = Color.FromArgb(210, 215, 240)
        defaultRowStyle.SelectionForeColor = Color.Black
        defaultRowStyle.Padding = New Padding(10, 4, 10, 4)

        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle)
        alternatingRowStyle.BackColor = Color.FromArgb(235, 237, 255)

        dgv.DefaultCellStyle = defaultRowStyle
        dgv.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        dgv.RowTemplate.Height = 38
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub txtAmountPaid_TextChanged(sender As Object, e As EventArgs) Handles txtAmountPaid.TextChanged
        ClearError()
        UpdatePaymentButtonText()
    End Sub

    Private Sub txtORNo_TextChanged(sender As Object, e As EventArgs) Handles txtORNo.TextChanged
        ClearError()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class