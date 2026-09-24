Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Public Class frmPayments
    Public Property SelectedControlNo As String
    Private docPrice As Decimal = 0D
    Private adminFee As Decimal = 10D
    Private isWaived As Boolean = False

    Private Sub frmPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearError()
        StyleDataGridView(dgvPayments)
        LoadUnpaidAppointmentsGrid()
        SetPaymentFieldsVisibility(False)
        ClearDetailLabels()
    End Sub

    ' --- ISANG Button Column lang: CANCEL ---
    Private Sub AddCancelButtonColumn()
        If dgvPayments.Columns.Contains("Cancel") Then
            dgvPayments.Columns.Remove("Cancel")
        End If
        Dim cancelCol As New DataGridViewButtonColumn()
        cancelCol.Name = "Cancel"
        cancelCol.HeaderText = ""
        cancelCol.Text = "CANCEL"
        cancelCol.UseColumnTextForButtonValue = True
        cancelCol.Width = 110
        cancelCol.FlatStyle = FlatStyle.Flat
        dgvPayments.Columns.Add(cancelCol)
    End Sub

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

    Private Sub SetPaymentFieldsVisibility(show As Boolean)
        Dim hasPayment As Boolean = show AndAlso Not isWaived
        lblControlNo.Visible = show
        lblStatus.Visible = show
        lblFullName.Visible = show
        lblDocumentType.Visible = show
        lblORNumber.Visible = hasPayment
        txtORNo.Visible = hasPayment
        lblAmountPaid.Visible = hasPayment
        txtAmountPaid.Visible = hasPayment
        lblOnlinePayment.Visible = hasPayment
        btnGcash.Visible = hasPayment
        btnMaya.Visible = hasPayment
        lblAdminFeeNotice.Visible = hasPayment
        lblSenderName.Visible = hasPayment
        txtSenderName.Visible = hasPayment
        lblTransactionNo.Visible = hasPayment
        txtTransactionNo.Visible = hasPayment
        btnMarkPaid.Visible = show
        If isWaived Then
            btnMarkPaid.Text = "MARK AS WAIVED"
            btnMarkPaid.BackColor = Color.FromArgb(40, 167, 69)
        Else
            btnMarkPaid.Text = "MARK PAID"
            btnMarkPaid.BackColor = Color.FromArgb(10, 25, 100)
        End If
        btnMarkPaid.ForeColor = Color.White
    End Sub

    Private Sub ClearDetailLabels()
        lblControlNo.Text = "-"
        lblStatus.Text = "-"
        lblFullName.Text = "-"
        lblDocumentType.Text = "-"
        txtORNo.Clear()
        txtAmountPaid.Clear()
        txtSenderName.Clear()
        txtTransactionNo.Clear()
        docPrice = 0D
        isWaived = False
    End Sub

    Public Sub LoadUnpaidAppointmentsGrid()
        Try
            DBconnection.connection()
            DBconnection.sql = "SELECT a.AppointmentID, a.ControlNo AS `Control No.`, a.RequestType AS `Request Type`, " &
                               "a.Purpose, a.Department, a.DateSubmitted AS `Date Submitted`, " &
                               "a.FullName, a.PaymentStatus, a.Status, a.Amount AS DocAmount " &
                               "FROM appointments a " &
                               "WHERE a.Status = 'APPROVED' " &
                               "AND (a.PaymentStatus = 'UNPAID' OR a.PaymentStatus IS NULL OR a.PaymentStatus = '') " &
                               "ORDER BY a.AppointmentID DESC"
            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            Dim da As New MySqlDataAdapter(DBconnection.cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvPayments.DataSource = dt
            AddCancelButtonColumn()
            If dgvPayments.Columns.Contains("AppointmentID") Then dgvPayments.Columns("AppointmentID").Visible = False
            If dgvPayments.Columns.Contains("DocAmount") Then dgvPayments.Columns("DocAmount").Visible = False
            ' Highlight ang Control No. column
            If dgvPayments.Columns.Contains("Control No.") Then
                dgvPayments.Columns("Control No.").DefaultCellStyle.BackColor = Color.FromArgb(230, 235, 255)
            End If
        Catch ex As Exception
            ShowError("Error loading list: " & ex.Message)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Function GetDocumentPrice(serviceName As String) As Decimal
        Try
            DBconnection.connection()
            DBconnection.sql = "SELECT Amount FROM document_services WHERE ServiceName LIKE @Name LIMIT 1"
            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@Name", "%" & serviceName & "%")
            Dim result = DBconnection.cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return Convert.ToDecimal(result)
            End If
        Catch ex As Exception
        Finally
            DBconnection.CloseConnection()
        End Try
        Return 0D
    End Function ' ✅ INAYOS — may tamang End Function na

    Private Sub LoadAppointmentDetails(row As DataGridViewRow)
        ClearError()
        ClearDetailLabels()
        SelectedControlNo = row.Cells("Control No.").Value.ToString()
        Dim requestType As String = If(row.Cells("Request Type").Value?.ToString(), "").Trim()
        Dim fullname As String = If(row.Cells("FullName").Value IsNot Nothing, row.Cells("FullName").Value.ToString(), "-")
        Dim status As String = If(row.Cells("Status").Value?.ToString(), "")
        docPrice = GetDocumentPrice(requestType)
        If docPrice = 0D AndAlso Not IsDBNull(row.Cells("DocAmount").Value) Then
            docPrice = Convert.ToDecimal(row.Cells("DocAmount").Value)
        End If
        If docPrice <= 0D Then
            isWaived = True
            txtAmountPaid.Clear()
        Else
            isWaived = False
            Dim totalAmount As Decimal = docPrice + adminFee
            txtAmountPaid.Text = totalAmount.ToString("N2")
        End If
        lblControlNo.Text = SelectedControlNo
        lblStatus.Text = status
        lblFullName.Text = fullname
        lblDocumentType.Text = requestType
        SetPaymentFieldsVisibility(True)
    End Sub

    ' --- Cell Click: CANCEL button o pumili ng row ---
    Private Sub dgvPayments_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPayments.CellMouseClick
        If e.RowIndex < 0 Then Return
        Dim colName As String = dgvPayments.Columns(e.ColumnIndex).Name
        Dim ctrlNo As String = dgvPayments.Rows(e.RowIndex).Cells("Control No.").Value.ToString()
        ' ✅ CANCEL button lang
        If colName = "Cancel" Then
            Using reasonForm As New frmCancelReason()
                If reasonForm.ShowDialog() = DialogResult.OK Then
                    ExecuteCancellation(ctrlNo, reasonForm.ReasonText)
                End If
            End Using
            Return
        End If
        ' Kung ibang cell — i-load ang detalye para mag-pay
        LoadAppointmentDetails(dgvPayments.Rows(e.RowIndex))
    End Sub

    ' --- CELL PAINTING: Rounded PILL CANCEL button ---
    Private Sub dgvPayments_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvPayments.CellPainting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If dgvPayments.Columns(e.ColumnIndex).Name = "Cancel" Then
            e.PaintBackground(e.CellBounds, True)
            Dim btnRect As New Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + 6,
                                         e.CellBounds.Width - 12, e.CellBounds.Height - 12)
            Dim radius As Integer = btnRect.Height \ 2 ' Fully rounded pill
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            Using path As GraphicsPath = GetRoundedPath(btnRect, radius)
                Using brush As New SolidBrush(Color.FromArgb(220, 53, 69)) ' Red
                    e.Graphics.FillPath(brush, path)
                End Using
            End Using
            TextRenderer.DrawText(e.Graphics, "CANCEL",
                                  New Font("Segoe UI", 8.5F, FontStyle.Bold),
                                  btnRect, Color.White,
                                  TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            e.Handled = True
        End If
    End Sub

    Private Function GetRoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d As Integer = radius * 2
        If d > rect.Width Then d = rect.Width
        If d > rect.Height Then d = rect.Height
        path.AddArc(rect.X, rect.Y, d, d, 180, 90)
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Private Sub btnMarkPaid_Click(sender As Object, e As EventArgs) Handles btnMarkPaid.Click
        If String.IsNullOrWhiteSpace(SelectedControlNo) Then
            ShowError("Pumili muna mula sa listahan.")
            Return
        End If
        If isWaived Then
            UpdatePaymentStatus("WAIVED", "", 0D, "", "")
            Return
        End If
        If String.IsNullOrWhiteSpace(txtORNo.Text) Then
            ShowError("Ilagay ang OR Number.")
            Return
        End If
        Dim paidAmount As Decimal
        If Not Decimal.TryParse(txtAmountPaid.Text.Trim(), paidAmount) OrElse paidAmount <= 0 Then
            ShowError("Ilagay ang tamang halaga.")
            Return
        End If
        UpdatePaymentStatus("PAID", txtORNo.Text.Trim(), paidAmount,
                            txtSenderName.Text.Trim(), txtTransactionNo.Text.Trim())
    End Sub

    Private Sub UpdatePaymentStatus(paymentStatus As String, orNo As String, amount As Decimal, senderName As String, transNo As String)
        Try
            DBconnection.connection()
            DBconnection.sql = "UPDATE appointments " &
                               "SET PaymentStatus = @PaymentStatus, " &
                               "    OfficialReceiptNo = @ORNo, " &
                               "    Amount = @Amount, " &
                               "    SenderName = @SenderName, " &
                               "    TransactionNumber = @TransNo, " &
                               "    PaymentDate = NOW(), " &
                               "    UpdatedAt = NOW() " &
                               "WHERE ControlNo = @ControlNo"
            Using cmd As New MySqlCommand(DBconnection.sql, DBconnection.cn)
                cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus)
                cmd.Parameters.AddWithValue("@ORNo", orNo)
                cmd.Parameters.AddWithValue("@Amount", amount)
                cmd.Parameters.AddWithValue("@SenderName", senderName)
                cmd.Parameters.AddWithValue("@TransNo", transNo)
                cmd.Parameters.AddWithValue("@ControlNo", SelectedControlNo)
                cmd.ExecuteNonQuery()
            End Using
            ShowSuccess($"Matagumpay! — {paymentStatus}")
            SelectedControlNo = ""
            LoadUnpaidAppointmentsGrid()
            ClearDetailLabels()
            SetPaymentFieldsVisibility(False)
        Catch ex As Exception
            ShowError("Error: " & ex.Message)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub ExecuteCancellation(ctrlNo As String, reason As String)
        Try
            DBconnection.connection()
            Dim currentUsername As String = "Administrator"
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
            ShowSuccess("Appointment cancelled.")
            If SelectedControlNo = ctrlNo Then
                SelectedControlNo = ""
                ClearDetailLabels()
                SetPaymentFieldsVisibility(False)
            End If
            LoadUnpaidAppointmentsGrid()
        Catch ex As Exception
            ShowError("Error cancelling: " & ex.Message)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' --- Clean Grid Style ---
    Private Sub StyleDataGridView(dgv As DataGridView)
        dgv.EnableHeadersVisualStyles = False
        dgv.BorderStyle = BorderStyle.None
        dgv.BackgroundColor = Color.White
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.None
        dgv.GridColor = Color.White
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.AllowUserToResizeRows = False
        Dim headerStyle As New DataGridViewCellStyle()
        headerStyle.BackColor = Color.FromArgb(248, 249, 252)
        headerStyle.ForeColor = Color.FromArgb(40, 50, 70)
        headerStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        headerStyle.Padding = New Padding(12, 10, 12, 10)
        dgv.ColumnHeadersDefaultCellStyle = headerStyle
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersHeight = 42
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Dim defaultRowStyle As New DataGridViewCellStyle()
        defaultRowStyle.BackColor = Color.White
        defaultRowStyle.ForeColor = Color.FromArgb(50, 50, 60)
        defaultRowStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        defaultRowStyle.SelectionBackColor = Color.FromArgb(235, 237, 255)
        defaultRowStyle.SelectionForeColor = Color.Black
        defaultRowStyle.Padding = New Padding(12, 6, 12, 6)
        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle)
        alternatingRowStyle.BackColor = Color.FromArgb(245, 247, 255)
        dgv.DefaultCellStyle = defaultRowStyle
        dgv.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        dgv.RowTemplate.Height = 42
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub ' ✅ INAYOS — may tamang End Sub na

End Class