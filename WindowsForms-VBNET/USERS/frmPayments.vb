Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class frmPayments

    Public Property SelectedControlNo As String
    Private totalAmount As Decimal = 0

    Private Sub frmPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearError()
        LoadAppointmentInfo()
    End Sub

    ' Helper method to show error in red text
    Private Sub ShowError(msg As String)
        If lblError IsNot Nothing Then
            lblError.Text = msg
            lblError.ForeColor = Color.Red
            lblError.Font = New Font(lblError.Font, FontStyle.Bold)
            lblError.Visible = True
        End If
    End Sub

    ' Helper method to clear error message
    Private Sub ClearError()
        If lblError IsNot Nothing Then
            lblError.Text = ""
            lblError.Visible = False
        End If
    End Sub

    Private Sub LoadAppointmentInfo()
        ClearError()

        If String.IsNullOrWhiteSpace(SelectedControlNo) Then
            ShowError("No appointment selected!")
            Return
        End If

        DBconnection.connection()

        Try
            ' JOIN appointments with document_services to get exact document Amount
            DBconnection.sql = "SELECT a.ControlNo, a.FullName, a.RequestType, a.Status, " &
                               "       COALESCE(ds.Amount, a.Amount, 0.00) AS DocAmount, " &
                               "       a.PaymentStatus, a.OfficialReceiptNo " &
                               "FROM appointments a " &
                               "LEFT JOIN document_services ds ON a.RequestType = ds.ServiceName " &
                               "WHERE a.ControlNo = @ControlNo LIMIT 1"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@ControlNo", SelectedControlNo)
            DBconnection.dr = DBconnection.cmd.ExecuteReader()

            If DBconnection.dr.Read() Then
                lblControlNo.Text = DBconnection.dr("ControlNo").ToString()
                lblFullName.Text = DBconnection.dr("FullName").ToString()
                lblDocument.Text = DBconnection.dr("RequestType").ToString()

                ' Get the fetched document fee amount
                totalAmount = Convert.ToDecimal(DBconnection.dr("DocAmount"))
                lblAmount.Text = "₱ " & totalAmount.ToString("N2")

                Dim payStatus = DBconnection.dr("PaymentStatus").ToString()
                lblPaymentStatus.Text = payStatus

                If Not IsDBNull(DBconnection.dr("OfficialReceiptNo")) Then
                    txtORNo.Text = DBconnection.dr("OfficialReceiptNo").ToString()
                End If

                If payStatus = "PAID" Then
                    lblPaymentStatus.ForeColor = Color.Green
                    btnMarkPaid.Enabled = False
                    btnWaive.Enabled = False
                    txtORNo.ReadOnly = True
                    txtAmountPaid.ReadOnly = True
                ElseIf payStatus = "WAIVED" Then
                    lblPaymentStatus.ForeColor = Color.Orange
                    btnMarkPaid.Enabled = False
                    btnWaive.Enabled = False
                    txtORNo.ReadOnly = True
                    txtAmountPaid.ReadOnly = True
                Else
                    lblPaymentStatus.ForeColor = Color.Red
                End If
            Else
                ShowError("Appointment record not found!")
            End If

        Catch ex As Exception
            ShowError("Database error: " & ex.Message)
        Finally
            If DBconnection.dr IsNot Nothing AndAlso Not DBconnection.dr.IsClosed Then
                DBconnection.dr.Close()
            End If
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' --- MARK AS PAID BUTTON ---
    Private Sub btnMarkPaid_Click(sender As Object, e As EventArgs) Handles btnMarkPaid.Click
        ClearError()

        ' 1. Validate Official Receipt Number
        If String.IsNullOrWhiteSpace(txtORNo.Text.Trim()) Then
            ShowError("⚠ Please enter Official Receipt Number!")
            txtORNo.Focus()
            Return
        End If

        ' 2. Validate Amount Paid Input
        Dim paidAmount As Decimal = 0
        If Not Decimal.TryParse(txtAmountPaid.Text.Trim(), paidAmount) OrElse paidAmount <= 0 Then
            ShowError("⚠ Please enter a valid amount paid!")
            txtAmountPaid.Focus()
            Return
        End If

        ' 3. Block Submission if Payment is Insufficient
        If totalAmount > 0 AndAlso paidAmount < totalAmount Then
            ShowError($"⚠ Insufficient payment! Required: ₱{totalAmount:N2} | Paid: ₱{paidAmount:N2}")
            txtAmountPaid.Focus()
            Return
        End If

        ' 4. Process Payment into Database
        DBconnection.connection()
        Try
            DBconnection.sql = "UPDATE appointments " &
                               "SET Amount = @Amount, " &
                               "    PaymentStatus = 'PAID', " &
                               "    Status = 'COMPLETED', " &
                               "    PaymentDate = NOW(), " &
                               "    OfficialReceiptNo = @ORNo, " &
                               "    UpdatedAt = NOW() " &
                               "WHERE ControlNo = @ControlNo"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@Amount", totalAmount)
            DBconnection.cmd.Parameters.AddWithValue("@ORNo", txtORNo.Text.Trim())
            DBconnection.cmd.Parameters.AddWithValue("@ControlNo", SelectedControlNo)
            DBconnection.cmd.ExecuteNonQuery()

            MessageBox.Show("Payment successfully processed! Appointment moved to COMPLETED." & vbCrLf & "O.R. Number: " & txtORNo.Text.Trim(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            ShowError("Error updating payment: " & ex.Message)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    ' --- WAIVE FEE BUTTON ---
    Private Sub btnWaive_Click(sender As Object, e As EventArgs) Handles btnWaive.Click
        ClearError()

        Dim result = MessageBox.Show("Are you sure you want to waive the fee for this document?", "Confirm Waive", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result <> DialogResult.Yes Then Return

        DBconnection.connection()
        Try
            DBconnection.sql = "UPDATE appointments " &
                               "SET Amount = 0.00, " &
                               "    PaymentStatus = 'WAIVED', " &
                               "    Status = 'COMPLETED', " &
                               "    PaymentDate = NOW(), " &
                               "    OfficialReceiptNo = 'WAIVED', " &
                               "    UpdatedAt = NOW() " &
                               "WHERE ControlNo = @ControlNo"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@ControlNo", SelectedControlNo)
            DBconnection.cmd.ExecuteNonQuery()

            MessageBox.Show("Fee waived successfully! Appointment moved to COMPLETED.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            ShowError("Error waiving fee: " & ex.Message)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub txtAmountPaid_TextChanged(sender As Object, e As EventArgs) Handles txtAmountPaid.TextChanged
        ClearError()

        Dim paid As Decimal
        If Decimal.TryParse(txtAmountPaid.Text.Trim(), paid) AndAlso paid >= 0 Then
            Dim sukli = paid - totalAmount
            lblChange.Text = If(sukli >= 0, "₱ " & sukli.ToString("N2"), "Insufficient Amount")
        Else
            lblChange.Text = "₱ 0.00"
        End If
    End Sub

    Private Sub txtORNo_TextChanged(sender As Object, e As EventArgs) Handles txtORNo.TextChanged
        ClearError()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class