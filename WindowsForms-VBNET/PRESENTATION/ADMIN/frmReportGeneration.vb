Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmReportGeneration

    Private Sub frmReportGeneration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePickerFrom.Value = DateTime.Today.AddDays(-30)
        DateTimePickerTo.Value = DateTime.Today
        StyleDataGridView(dgvReport)
        LoadSummaryReport()
    End Sub

    Private Sub LoadSummaryReport()
        Try
            connection()
            Dim fromDate As String = DateTimePickerFrom.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim toDate As String = DateTimePickerTo.Value.ToString("yyyy-MM-dd 23:59:59")

            ' ✅ SQL Query para sa Summary Report ng bawat Document Type
            sql = "SELECT RequestType AS 'Document Type', " &
                  "COUNT(*) AS 'Total', " &
                  "SUM(CASE WHEN Status IN ('Completed','Claimed') THEN 1 ELSE 0 END) AS 'Completed', " &
                  "SUM(CASE WHEN Status IN ('Completed','Claimed') AND Amount > 0 THEN 1 ELSE 0 END) AS 'Paid', " &
                  "SUM(CASE WHEN Status IN ('Completed','Claimed') AND Amount > 0 THEN 1 ELSE 0 END) AS 'Shipped', " &
                  "SUM(CASE WHEN Status = 'Pending' THEN 1 ELSE 0 END) AS 'Pending', " &
                  "MAX(IFNULL(Amount, 0)) AS 'Unit Amount', " &
                  "SUM(IFNULL(Amount, 0)) AS 'Total Amount' " &
                  "FROM appointments " &
                  "WHERE DateSubmitted BETWEEN @fromDate AND @toDate " &
                  "GROUP BY RequestType ORDER BY RequestType ASC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            ' ✅ Kalkulahin ang Total Amount bawat row (Total x Unit Amount)
            For Each row As DataRow In dt.Rows
                Dim totalQty As Decimal = Convert.ToDecimal(row("Total"))
                Dim unitAmt As Decimal = Convert.ToDecimal(row("Unit Amount"))
                row("Total Amount") = totalQty * unitAmt
            Next

            dgvReport.DataSource = dt

            If dgvReport.Columns.Contains("Total") Then
                dgvReport.Columns("Total").DefaultCellStyle.Format = "N0"
                dgvReport.Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            If dgvReport.Columns.Contains("Completed") Then
                dgvReport.Columns("Completed").DefaultCellStyle.Format = "N0"
                dgvReport.Columns("Completed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            If dgvReport.Columns.Contains("Paid") Then
                dgvReport.Columns("Paid").DefaultCellStyle.Format = "N0"
                dgvReport.Columns("Paid").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            If dgvReport.Columns.Contains("Shipped") Then
                dgvReport.Columns("Shipped").DefaultCellStyle.Format = "N0"
                dgvReport.Columns("Shipped").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            If dgvReport.Columns.Contains("Pending") Then
                dgvReport.Columns("Pending").DefaultCellStyle.Format = "N0"
                dgvReport.Columns("Pending").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If

            If dgvReport.Columns.Contains("Unit Amount") Then
                dgvReport.Columns("Unit Amount").DefaultCellStyle.Format = "₱#,##0.00"
                dgvReport.Columns("Unit Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If

            If dgvReport.Columns.Contains("Total Amount") Then
                dgvReport.Columns("Total Amount").DefaultCellStyle.Format = "₱#,##0.00"
                dgvReport.Columns("Total Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If

            ' === ✅ ACCURATE COMPUTATION PARA SA MGA LABELS SA ITAAS ===
            Dim grandTotalDocuments As Long = 0
NamesOfStatuses:
            Dim grandTotalCompleted As Long = 0
            Dim grandTotalPaid As Long = 0
            Dim grandTotalShipped As Long = 0
            Dim grandTotalPending As Long = 0
            Dim grandTotalAmount As Decimal = 0D

            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("Total")) Then
                    grandTotalDocuments += Convert.ToInt64(row("Total"))
                End If
                If Not IsDBNull(row("Completed")) Then
                    grandTotalCompleted += Convert.ToInt64(row("Completed"))
                End If
                If Not IsDBNull(row("Paid")) Then
                    grandTotalPaid += Convert.ToInt64(row("Paid"))
                End If
                If Not IsDBNull(row("Shipped")) Then
                    grandTotalShipped += Convert.ToInt64(row("Shipped"))
                End If
                If Not IsDBNull(row("Pending")) Then
                    grandTotalPending += Convert.ToInt64(row("Pending"))
                End If
                If Not IsDBNull(row("Total Amount")) Then
                    Dim amt As Decimal = Convert.ToDecimal(row("Total Amount"))
                    grandTotalAmount += amt
                End If
            Next

            ' Kung sakaling gusto mo ring kunin ang eksaktong overall count diretso sa database para sa mga labels:
            LoadLabelsDirectly(fromDate, toDate)

        Catch ex As Exception
            MsgBox("Error loading summary: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === HELPER PARA SIGURADONG TAMA ANG BILANG SA MGA LABELS SA ITAAS ===
    Private Sub LoadLabelsDirectly(fromDate As String, toDate As String)
        Try
            ' 1. Pending Count
            cmd = New MySqlCommand("SELECT COUNT(*) FROM appointments WHERE Status = 'Pending' AND DateSubmitted BETWEEN @fromDate AND @toDate", cn)
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)
            Dim pendingVal = cmd.ExecuteScalar()
            If lblPending IsNot Nothing Then lblPending.Text = If(pendingVal IsNot Nothing, Convert.ToInt64(pendingVal).ToString("N0"), "0")

            ' 2. Completed Count
            cmd = New MySqlCommand("SELECT COUNT(*) FROM appointments WHERE Status IN ('Completed','Claimed') AND DateSubmitted BETWEEN @fromDate AND @toDate", cn)
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)
            Dim completedVal = cmd.ExecuteScalar()
            If lblCompleted IsNot Nothing Then lblCompleted.Text = If(completedVal IsNot Nothing, Convert.ToInt64(completedVal).ToString("N0"), "0")

            ' 3. Paid Count
            cmd = New MySqlCommand("SELECT COUNT(*) FROM appointments WHERE Status IN ('Completed','Claimed') AND Amount > 0 AND DateSubmitted BETWEEN @fromDate AND @toDate", cn)
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)
            Dim paidVal = cmd.ExecuteScalar()
            If lblPaid IsNot Nothing Then lblPaid.Text = If(paidVal IsNot Nothing, Convert.ToInt64(paidVal).ToString("N0"), "0")

            ' 4. Shipped Count
            cmd = New MySqlCommand("SELECT COUNT(*) FROM appointments WHERE Status IN ('Completed','Claimed') AND Amount > 0 AND DateSubmitted BETWEEN @fromDate AND @toDate", cn)
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)
            Dim shippedVal = cmd.ExecuteScalar()
            If lblShipped IsNot Nothing Then lblShipped.Text = If(shippedVal IsNot Nothing, Convert.ToInt64(shippedVal).ToString("N0"), "0")

            ' 5. Total Documents Count
            cmd = New MySqlCommand("SELECT COUNT(*) FROM appointments WHERE DateSubmitted BETWEEN @fromDate AND @toDate", cn)
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)
            Dim totalDocsVal = cmd.ExecuteScalar()
            If lblTotalDocuments IsNot Nothing Then lblTotalDocuments.Text = If(totalDocsVal IsNot Nothing, Convert.ToInt64(totalDocsVal).ToString("N0"), "0")

            ' 6. Grand Total Amount (Sum ng lahat ng Amount ng mga dokumentong nasa sakop ng petsa)
            cmd = New MySqlCommand("SELECT SUM(IFNULL(Amount, 0)) FROM appointments WHERE DateSubmitted BETWEEN @fromDate AND @toDate", cn)
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)
            Dim totalAmtVal = cmd.ExecuteScalar()
            Dim finalAmount As Decimal = If(totalAmtVal IsNot Nothing AndAlso totalAmtVal IsNot DBNull.Value, Convert.ToDecimal(totalAmtVal), 0D)

            If lblAmount IsNot Nothing Then lblAmount.Text = "₱" & finalAmount.ToString("N2")

        Catch ex As Exception
            ' Huwag nang pansinin o i-log kung sakaling may minor query exception para tuloy ang daloy
        End Try
    End Sub

    ' === DOUBLE-CLICK EVENT SA DATAGRIDVIEW ===
    Private Sub dgvReport_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReport.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim selectedDocType As String = dgvReport.Rows(e.RowIndex).Cells("Document Type").Value.ToString()

            Using frm As New frmSpecificAppointments()
                frm.TargetDocumentType = selectedDocType
                frm.FilterStartDate = DateTimePickerFrom.Value.ToString("yyyy-MM-dd 00:00:00")
                frm.FilterEndDate = DateTimePickerTo.Value.ToString("yyyy-MM-dd 23:59:59")
                frm.ShowDialog()
            End Using
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadSummaryReport()
    End Sub

    Private Sub DateTimePickerFrom_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerFrom.ValueChanged
        LoadSummaryReport()
    End Sub

    Private Sub DateTimePickerTo_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerTo.ValueChanged
        LoadSummaryReport()
    End Sub

    Private Sub btnExportAll_Click(sender As Object, e As EventArgs) Handles btnExportAll.Click
        Try
            connection()
            Dim fromDate As String = DateTimePickerFrom.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim toDate As String = DateTimePickerTo.Value.ToString("yyyy-MM-dd 23:59:59")

            sql = "SELECT ControlNo AS 'Control No.', FullName AS 'Full Name', RequestType AS 'Document Type', " &
                  "Purpose, Department, Amount, DateSubmitted AS 'Date Submitted', Status " &
                  "FROM appointments " &
                  "WHERE DateSubmitted BETWEEN @fromDate AND @toDate " &
                  "ORDER BY AppointmentID DESC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                MsgBox("No records available to export.", MsgBoxStyle.Exclamation, "Warning")
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Filter = "Excel Workbook|*.xlsx"
                sfd.FileName = "Appointment_Records_Report.xlsx"

                If sfd.ShowDialog() = DialogResult.OK Then
                    Dim xlApp As Object = CreateObject("Excel.Application")
                    Dim xlWb As Object = xlApp.Workbooks.Add()
                    Dim xlSheet As Object = xlWb.Worksheets(1)

                    For i As Integer = 0 To dt.Columns.Count - 1
                        xlSheet.Cells(1, i + 1) = dt.Columns(i).ColumnName
                    Next

                    For r As Integer = 0 To dt.Rows.Count - 1
                        For c As Integer = 0 To dt.Columns.Count - 1
                            xlSheet.Cells(r + 2, c + 1) = dt.Rows(r)(c).ToString()
                        Next
                    Next

                    xlWb.SaveAs(sfd.FileName)
                    xlWb.Close(False)
                    xlApp.Quit()

                    MsgBox("Appointment records successfully exported to Excel!", MsgBoxStyle.Information, "Success")
                End If
            End Using

        Catch ex As Exception
            MsgBox("Error exporting records: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub StyleDataGridView(dgv As DataGridView)
        dgv.EnableHeadersVisualStyles = False
        dgv.BorderStyle = BorderStyle.None
        dgv.BackgroundColor = Color.White
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.GridColor = Color.FromArgb(230, 235, 245)
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.AllowUserToResizeRows = False

        Dim headerStyle As New DataGridViewCellStyle()
        headerStyle.BackColor = Color.FromArgb(25, 42, 86)
        headerStyle.ForeColor = Color.White
        headerStyle.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        headerStyle.Padding = New Padding(12, 10, 12, 10)
        dgv.ColumnHeadersDefaultCellStyle = headerStyle
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersHeight = 42
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        Dim defaultRowStyle As New DataGridViewCellStyle()
        defaultRowStyle.BackColor = Color.White
        defaultRowStyle.ForeColor = Color.FromArgb(30, 41, 59)
        defaultRowStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        defaultRowStyle.SelectionBackColor = Color.FromArgb(210, 220, 245)
        defaultRowStyle.SelectionForeColor = Color.FromArgb(25, 42, 86)
        defaultRowStyle.Padding = New Padding(12, 6, 12, 6)

        dataGridAlternatingRowStyle(defaultRowStyle, dgv)
    End Sub

    Private Sub dataGridAlternatingRowStyle(defaultRowStyle As DataGridViewCellStyle, dgv As DataGridView)
        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle)
        alternatingRowStyle.BackColor = Color.FromArgb(248, 250, 252)
        dgv.DefaultCellStyle = defaultRowStyle
        dgv.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        dgv.RowTemplate.Height = 40
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
End Class