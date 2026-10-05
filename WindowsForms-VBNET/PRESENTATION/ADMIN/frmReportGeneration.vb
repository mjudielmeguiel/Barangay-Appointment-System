Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

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

            sql = "SELECT RequestType AS 'Document Type', " &
                  "COUNT(*) AS 'Total', " &
                  "SUM(CASE WHEN Status IN ('Completed','Claimed') THEN 1 ELSE 0 END) AS 'Completed', " &
                  "SUM(CASE WHEN Status IN ('Completed','Claimed') AND Amount > 0 THEN 1 ELSE 0 END) AS 'Paid', " &
                  "SUM(CASE WHEN Status IN ('Completed','Claimed') AND Amount > 0 THEN 1 ELSE 0 END) AS 'Shipped', " &
                  "SUM(CASE WHEN Status = 'Pending' THEN 1 ELSE 0 END) AS 'Pending', " &
                  "MAX(IFNULL(Amount, 0)) AS 'Unit Amount', " &
                  "SUM(CASE WHEN Status IN ('Completed','Claimed') AND Amount > 0 THEN Amount ELSE 0 END) AS 'Total Amount' " &
                  "FROM appointments " &
                  "WHERE DateSubmitted BETWEEN @fromDate AND @toDate " &
                  "GROUP BY RequestType ORDER BY RequestType ASC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvReport.DataSource = dt

            If Not dgvReport.Columns.Contains("ActionBtn") Then
                Dim btnCol As New DataGridViewButtonColumn()
                btnCol.Name = "ActionBtn"
                btnCol.HeaderText = "Action"
                btnCol.Text = "View List"
                btnCol.UseColumnTextForButtonValue = True
                dgvReport.Columns.Add(btnCol)
            End If

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
                dgvReport.Columns("Unit Amount").DefaultCellStyle.Format = "N2"
                dgvReport.Columns("Unit Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If

            If dgvReport.Columns.Contains("Total Amount") Then
                dgvReport.Columns("Total Amount").DefaultCellStyle.Format = "N2"
                dgvReport.Columns("Total Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If

            Dim grandTotalPaid As Long = 0
            Dim grandTotalShipped As Long = 0
            Dim grandTotalAmount As Decimal = 0D

            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("Paid")) Then
                    grandTotalPaid += Convert.ToInt64(row("Paid"))
                End If
                If Not IsDBNull(row("Shipped")) Then
                    grandTotalShipped += Convert.ToInt64(row("Shipped"))
                End If
                If Not IsDBNull(row("Total Amount")) Then
                    Dim amt As Decimal = Convert.ToDecimal(row("Total Amount"))
                    If amt > 0 Then
                        grandTotalAmount += amt
                    End If
                End If
            Next

            If lblPaid IsNot Nothing Then lblPaid.Text = grandTotalPaid.ToString("N0")
            If lblShipped IsNot Nothing Then lblShipped.Text = grandTotalShipped.ToString("N0")
            If lblAmount IsNot Nothing Then lblAmount.Text = grandTotalAmount.ToString("N2")

        Catch ex As Exception
            MsgBox("Error loading summary: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub dgvReport_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvReport.CellPainting
        If e.RowIndex >= 0 AndAlso dgvReport.Columns(e.ColumnIndex).Name = "ActionBtn" Then
            e.Paint(e.CellBounds, DataGridViewPaintParts.All)

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality

            Dim rect As Rectangle = e.CellBounds
            rect.Inflate(-6, -5)

            Using path As GraphicsPath = GetRoundedRectanglePath(rect, 14)
                Using brush As New SolidBrush(Color.FromArgb(10, 25, 85))
                    e.Graphics.FillPath(brush, path)
                End Using

                Using sf As New StringFormat()
                    sf.Alignment = StringAlignment.Center
                    sf.LineAlignment = StringAlignment.Center
                    Using textBrush As New SolidBrush(Color.White)
                        e.Graphics.DrawString("VIEW LIST", New Font("Segoe UI", 9.0F, FontStyle.Bold), textBrush, rect, sf)
                    End Using
                End Using
            End Using

            e.Handled = True
        End If
    End Sub

    Private Function GetRoundedRectanglePath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim diameter As Integer = radius * 2
        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Private Sub dgvReport_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReport.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = dgvReport.Columns("ActionBtn").Index Then
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
                sfd.FileName = "All_Appointment_Records_Report.xlsx"

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
        dgv.GridColor = Color.FromArgb(220, 224, 230)
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.AllowUserToResizeRows = False

        ' Segoe UI Header Style
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

        ' Segoe UI Row Style
        Dim defaultRowStyle As New DataGridViewCellStyle()
        defaultRowStyle.BackColor = Color.White
        defaultRowStyle.ForeColor = Color.FromArgb(50, 50, 60)
        defaultRowStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        defaultRowStyle.SelectionBackColor = Color.FromArgb(210, 215, 240)
        defaultRowStyle.SelectionForeColor = Color.Black
        defaultRowStyle.Padding = New Padding(10, 4, 10, 4)

        dataGridAlternatingRowStyle(defaultRowStyle, dgv)
    End Sub

    Private Sub dataGridAlternatingRowStyle(defaultRowStyle As DataGridViewCellStyle, dgv As DataGridView)
        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle)
        alternatingRowStyle.BackColor = Color.FromArgb(235, 237, 255)
        dgv.DefaultCellStyle = defaultRowStyle
        dgv.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        dgv.RowTemplate.Height = 38
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
End Class