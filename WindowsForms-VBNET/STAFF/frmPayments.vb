Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Public Class frmPayments
    Public PreviousForm As Form = Nothing
    Public Property SelectedControlNo As String
    Private docPrice As Decimal = 0D
    Private ReadOnly adminFee As Decimal = 10D
    Private isWaived As Boolean = False
    Private currentProcessedByName As String = ""
    Private currentFullName As String = ""
    Private currentDocumentType As String = ""

    ' === 1. CONSTRUCTORS PARA MATANGGAP ANG CONTROL NO ===
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal controlNo As String)
        InitializeComponent()
        SelectedControlNo = controlNo
    End Sub
    ' ====================================================

    Private Sub UpdatePayButtonText()
        If isWaived Then
            btnMarkPaid.Text = "MARK AS WAIVED"
            btnMarkPaid.BackColor = Color.FromArgb(40, 167, 69)
            btnMarkPaid.ForeColor = Color.White
            Return
        End If

        Dim totalDue As Decimal = docPrice + adminFee
        Dim enteredAmount As Decimal = 0D
        Decimal.TryParse(txtAmountPaid.Text.Trim(), enteredAmount)

        If enteredAmount <= 0 Then
            btnMarkPaid.Text = $"PAY: {totalDue:N2}"
            btnMarkPaid.BackColor = Color.FromArgb(10, 25, 100)
        ElseIf enteredAmount > totalDue Then
            Dim changeAmt As Decimal = enteredAmount - totalDue
            btnMarkPaid.Text = $"TOTAL: {totalDue:N2} | CHANGE: {changeAmt:N2}"
            btnMarkPaid.BackColor = Color.FromArgb(16, 124, 65)
        ElseIf enteredAmount = totalDue Then
            btnMarkPaid.Text = $"EXACT AMOUNT: {totalDue:N2}"
            btnMarkPaid.BackColor = Color.FromArgb(16, 124, 65)
        Else
            btnMarkPaid.Text = $"DUE: {totalDue:N2} | SHORTAGE: {(totalDue - enteredAmount):N2}"
            btnMarkPaid.BackColor = Color.FromArgb(204, 153, 0)
        End If

        btnMarkPaid.ForeColor = Color.White
        btnMarkPaid.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
    End Sub

    Private Sub frmPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearError()
        LoadCurrentUserName()
        StyleDataGridView(dgvPayments)
        LoadUnpaidAppointmentsGrid()
        SetPaymentFieldsVisibility(False)
        ClearDetailLabels()

        AddHandler txtAmountPaid.TextChanged, AddressOf txtAmountPaid_TextChanged
        AddHandler chkOnlinePayment.CheckedChanged, AddressOf chkOnlinePayment_CheckedChanged
        AddHandler txtSearch.TextChanged, AddressOf txtSearch_TextChanged

        SetOnlinePaymentVisibility(False)

        ' ✅ CANCEL button — Papalitan natin ang function para bumalik sa Dashboard
        btnClose.Text = "CANCEL / BACK"
        btnClose.BackColor = Color.FromArgb(220, 53, 69)
        btnClose.ForeColor = Color.White
        btnClose.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)

        ' === 2. AUTO-LOAD KAPAG MAY IPINASANG CONTROL NO ===
        If Not String.IsNullOrEmpty(SelectedControlNo) Then
            AutoSelectAppointment(SelectedControlNo)
        End If
        ' ===================================================
    End Sub

    ' === AUTO-SELECT LOGIC ===
    Private Sub AutoSelectAppointment(ctrlNo As String)
        For Each row As DataGridViewRow In dgvPayments.Rows
            If row.Cells("Control No.").Value IsNot Nothing AndAlso row.Cells("Control No.").Value.ToString() = ctrlNo Then
                row.Selected = True
                LoadAppointmentDetails(row)
                Exit For
            End If
        Next
    End Sub

    ' === 3. GO BACK TO DASHBOARD LOGIC ===
    Private Sub GoBackToDashboard()
        Dim frm As New frmUser_Dashboard()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill

        frmMain.Panel2.Controls.Clear()
        frmMain.Panel2.Controls.Add(frm)
        frm.Show()
    End Sub

    Private Sub LoadCurrentUserName()
        Try
            DBconnection.connection()
            If String.IsNullOrWhiteSpace(frmlogin.LoggedInUsername) Then
                currentProcessedByName = "Unknown User"
                Exit Sub
            End If

            DBconnection.sql = "SELECT CONCAT(Lastname, ', ', Firstname) AS FullName " &
                               "FROM users WHERE Username = @Username LIMIT 1"
            Using cmd As New MySqlCommand(DBconnection.sql, DBconnection.cn)
                cmd.Parameters.AddWithValue("@Username", frmlogin.LoggedInUsername)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    currentProcessedByName = result.ToString()
                Else
                    DBconnection.sql = "SELECT CONCAT(Lastname, ', ', Firstname) AS FullName " &
                                       "FROM admin WHERE Username = @Username LIMIT 1"
                    Using cmdAdmin As New MySqlCommand(DBconnection.sql, DBconnection.cn)
                        cmdAdmin.Parameters.AddWithValue("@Username", frmlogin.LoggedInUsername)
                        Dim adminResult = cmdAdmin.ExecuteScalar()
                        If adminResult IsNot Nothing AndAlso Not IsDBNull(adminResult) Then
                            currentProcessedByName = adminResult.ToString()
                        Else
                            currentProcessedByName = frmlogin.LoggedInFullname
                        End If
                    End Using
                End If
            End Using
        Catch ex As Exception
            currentProcessedByName = frmlogin.LoggedInFullname
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub txtAmountPaid_TextChanged(sender As Object, e As EventArgs)
        If Not isWaived Then UpdatePayButtonText()
    End Sub

    Private Sub chkOnlinePayment_CheckedChanged(sender As Object, e As EventArgs)
        SetOnlinePaymentVisibility(chkOnlinePayment.Checked)
        ClearOnlinePaymentFields()
        UpdatePayButtonText()
    End Sub

    Private Sub SetOnlinePaymentVisibility(show As Boolean)
        lblOnlinePayment.Visible = show
        btnGcash.Visible = show
        btnMaya.Visible = show
        lblSenderName.Visible = show
        txtSenderName.Visible = show
        lblTransactionNo.Visible = show
        txtTransactionNo.Visible = show
        lblWalletUsed.Visible = show
        txtWalletUsed.Visible = show
    End Sub

    Private Sub ClearOnlinePaymentFields()
        txtSenderName.Clear()
        txtTransactionNo.Clear()
        txtWalletUsed.Clear()
    End Sub

    Private Sub btnGcash_Click(sender As Object, e As EventArgs) Handles btnGcash.Click
        txtWalletUsed.Text = "GCASH"
    End Sub

    Private Sub btnMaya_Click(sender As Object, e As EventArgs) Handles btnMaya.Click
        txtWalletUsed.Text = "PAYMAYA"
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        LoadUnpaidAppointmentsGrid(txtSearch.Text.Trim())
    End Sub

    Private Sub ShowError(msg As String)
        lblError.Text = "⚠ " & msg
        lblError.ForeColor = Color.Red
        lblError.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblError.Visible = True
    End Sub

    Private Sub ShowSuccess(msg As String)
        lblError.Text = "✔ " & msg
        lblError.ForeColor = Color.FromArgb(16, 124, 65)
        lblError.Visible = True
    End Sub

    Private Sub ClearError()
        lblError.Text = ""
        lblError.Visible = False
    End Sub

    Private Sub SetPaymentFieldsVisibility(show As Boolean)
        lblControlNo.Visible = show
        lblStatus.Visible = show
        lblFullName.Visible = show
        lblDocumentType.Visible = show

        ' Hide OR Number and Amount Paid fields if waived
        If isWaived Then
            lblORNumber.Visible = False
            txtORNo.Visible = False
            lblAmountPaid.Visible = False
            txtAmountPaid.Visible = False
            chkOnlinePayment.Visible = False
            SetOnlinePaymentVisibility(False)
        Else
            lblORNumber.Visible = show
            txtORNo.Visible = show
            lblAmountPaid.Visible = show
            txtAmountPaid.Visible = show
            chkOnlinePayment.Visible = show

            If show AndAlso chkOnlinePayment.Checked Then
                SetOnlinePaymentVisibility(True)
            Else
                SetOnlinePaymentVisibility(False)
            End If
        End If

        btnMarkPaid.Visible = show
        UpdatePayButtonText()
    End Sub

    Private Sub ClearDetailLabels()
        lblControlNo.Text = "-"
        lblStatus.Text = "-"
        lblFullName.Text = "-"
        lblDocumentType.Text = "-"
        txtORNo.Clear()
        txtAmountPaid.Clear()
        ClearOnlinePaymentFields()
        chkOnlinePayment.Checked = False
        isWaived = False
        docPrice = 0D
        currentFullName = ""
        currentDocumentType = ""
    End Sub

    Public Sub LoadUnpaidAppointmentsGrid(Optional searchKeyword As String = "")
        Try
            DBconnection.connection()
            Dim baseSQL As String = "SELECT a.AppointmentID, a.ControlNo AS `Control No.`, " &
                                    "a.RequestType AS `Request Type`, a.Purpose, a.Department, " &
                                    "a.DateSubmitted AS `Date Submitted`, a.FullName, " &
                                    "a.PaymentStatus, a.Status, a.Amount AS DocAmount " &
                                    "FROM appointments a " &
                                    "WHERE a.Status = 'APPROVED' " &
                                    "AND (a.PaymentStatus = 'UNPAID' OR a.PaymentStatus IS NULL OR a.PaymentStatus = '') "

            If Not String.IsNullOrWhiteSpace(searchKeyword) Then
                baseSQL &= " AND (a.ControlNo LIKE @Keyword OR a.FullName LIKE @Keyword) "
            End If
            baseSQL &= " ORDER BY a.AppointmentID DESC"

            Using cmd As New MySqlCommand(baseSQL, DBconnection.cn)
                If Not String.IsNullOrWhiteSpace(searchKeyword) Then
                    cmd.Parameters.AddWithValue("@Keyword", "%" & searchKeyword & "%")
                End If
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvPayments.DataSource = dt
            End Using

            If dgvPayments.Columns.Contains("AppointmentID") Then dgvPayments.Columns("AppointmentID").Visible = False
            If dgvPayments.Columns.Contains("DocAmount") Then dgvPayments.Columns("DocAmount").Visible = False
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
            Using cmd As New MySqlCommand("SELECT Amount FROM document_services WHERE ServiceName LIKE @Name LIMIT 1", DBconnection.cn)
                cmd.Parameters.AddWithValue("@Name", "%" & serviceName & "%")
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return Convert.ToDecimal(result)
                End If
            End Using
        Catch ex As Exception
        Finally
            DBconnection.CloseConnection()
        End Try
        Return 0D
    End Function

    Private Sub LoadAppointmentDetails(row As DataGridViewRow)
        ClearError()
        ClearDetailLabels()
        SelectedControlNo = row.Cells("Control No.").Value.ToString()
        Dim requestType As String = If(row.Cells("Request Type").Value?.ToString(), "").Trim()
        Dim fullname As String = If(row.Cells("FullName").Value?.ToString(), "-")
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
            txtAmountPaid.Text = (docPrice + adminFee).ToString("N2")
        End If

        currentFullName = fullname
        currentDocumentType = requestType

        lblControlNo.Text = SelectedControlNo
        lblStatus.Text = status
        lblFullName.Text = fullname
        lblDocumentType.Text = requestType

        ' Update visibility after setting isWaived
        SetPaymentFieldsVisibility(True)
    End Sub

    Private Sub dgvPayments_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvPayments.CellMouseClick
        If e.RowIndex < 0 Then Return
        LoadAppointmentDetails(dgvPayments.Rows(e.RowIndex))
    End Sub

    Private Sub btnMarkPaid_Click(sender As Object, e As EventArgs) Handles btnMarkPaid.Click
        If String.IsNullOrWhiteSpace(SelectedControlNo) Then
            ShowError("Pumili muna mula sa listahan.")
            Return
        End If

        If isWaived Then
            Dim waiveMsg As String = $"Are you sure you want to MARK AS WAIVED?{Environment.NewLine}" &
                                     $"{Environment.NewLine}" &
                                     $"Name: {currentFullName}{Environment.NewLine}" &
                                     $"Document: {currentDocumentType}{Environment.NewLine}" &
                                     $"Control No.: {SelectedControlNo}"
            If MessageBox.Show(waiveMsg, "Confirm Waive",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If
            UpdatePaymentStatus("WAIVED", "", 0D, "", "", "")
            Return
        End If

        If String.IsNullOrWhiteSpace(txtORNo.Text.Trim()) Then
            ShowError("Ilagay ang OR Number.")
            txtORNo.Focus()
            Return
        End If

        Dim paidAmount As Decimal
        If Not Decimal.TryParse(txtAmountPaid.Text.Trim(), paidAmount) OrElse paidAmount <= 0 Then
            ShowError("Ilagay ang tamang halaga.")
            txtAmountPaid.Focus()
            Return
        End If

        Dim totalDue As Decimal = docPrice + adminFee
        Dim paymentMethod As String = If(chkOnlinePayment.Checked, "Online Payment", "Cash Payment")
        Dim walletInfo As String = If(chkOnlinePayment.Checked, txtWalletUsed.Text.Trim(), "CASH")

        If chkOnlinePayment.Checked Then
            If String.IsNullOrWhiteSpace(txtSenderName.Text.Trim()) Then
                ShowError("Ilagay ang Sender Name para sa Online Payment.")
                txtSenderName.Focus()
                Return
            End If
            If String.IsNullOrWhiteSpace(txtTransactionNo.Text.Trim()) Then
                ShowError("Ilagay ang Transaction Number.")
                txtTransactionNo.Focus()
                Return
            End If
            If String.IsNullOrWhiteSpace(txtWalletUsed.Text.Trim()) Then
                ShowError("Pumili ng wallet (GCASH/PAYMAYA) o ilagay kung saan nagbayad.")
                txtWalletUsed.Focus()
                Return
            End If
        End If

        Dim confirmMsg As String = $"Are you sure you want to PROCESS PAYMENT?{Environment.NewLine}" &
                                   $"{Environment.NewLine}" &
                                   $"Name: {currentFullName}{Environment.NewLine}" &
                                   $"Document: {currentDocumentType}{Environment.NewLine}" &
                                   $"Control No.: {SelectedControlNo}{Environment.NewLine}" &
                                   $"OR Number: {txtORNo.Text.Trim()}{Environment.NewLine}" &
                                   $"Total Amount: {totalDue:N2}{Environment.NewLine}" &
                                   $"Amount Paid: {paidAmount:N2}{Environment.NewLine}" &
                                   $"Payment Method: {paymentMethod} ({walletInfo})"

        If MessageBox.Show(confirmMsg, "Confirm Payment",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Dim senderName As String = If(chkOnlinePayment.Checked, txtSenderName.Text.Trim(), "")
        Dim transNo As String = If(chkOnlinePayment.Checked, txtTransactionNo.Text.Trim(), "")
        Dim walletUsedFinal As String = If(chkOnlinePayment.Checked, txtWalletUsed.Text.Trim(), "CASH")

        UpdatePaymentStatus("PAID", txtORNo.Text.Trim(), paidAmount, senderName, transNo, walletUsedFinal)
    End Sub

    Private Sub UpdatePaymentStatus(paymentStatus As String, orNo As String, amount As Decimal,
                                    senderName As String, transNo As String, walletUsed As String)
        Try
            DBconnection.connection()
            Dim sql As String = "UPDATE appointments " &
                               "SET PaymentStatus = @PaymentStatus, " &
                               "    OfficialReceiptNo = @ORNo, " &
                               "    Amount = @Amount, " &
                               "    SenderName = @SenderName, " &
                               "    TransactionNumber = @TransNo, " &
                               "    WalletUsed = @WalletUsed, " &
                               "    ProcessedBy = @ProcessedBy, " &
                               "    PaymentDate = NOW(), " &
                               "    UpdatedAt = NOW() " &
                               "WHERE ControlNo = @ControlNo"

            Using cmd As New MySqlCommand(sql, DBconnection.cn)
                cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus)
                cmd.Parameters.AddWithValue("@ORNo", orNo)
                cmd.Parameters.AddWithValue("@Amount", amount)
                cmd.Parameters.AddWithValue("@SenderName", senderName)
                cmd.Parameters.AddWithValue("@TransNo", transNo)
                cmd.Parameters.AddWithValue("@WalletUsed", walletUsed)
                cmd.Parameters.AddWithValue("@ProcessedBy", currentProcessedByName)
                cmd.Parameters.AddWithValue("@ControlNo", SelectedControlNo)
                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show($"Payment Successful!{Environment.NewLine}{Environment.NewLine}" &
                           $"Status: {paymentStatus}{Environment.NewLine}" &
                           $"Processed By: {currentProcessedByName}",
                           "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            GoBackToDashboard()

        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        GoBackToDashboard()
    End Sub

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
    End Sub
End Class