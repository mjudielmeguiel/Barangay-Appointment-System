Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class frmSpecificAppointments

    Public Property TargetDocumentType As String = ""
    Public Property FilterStartDate As String = ""
    Public Property FilterEndDate As String = ""

    Private Sub frmSpecificAppointments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = If(String.IsNullOrEmpty(TargetDocumentType), "Specific Document Appointments", $"Appointments for: {TargetDocumentType}")
        StyleDataGridView(dgvSpecificAppointments)
        LoadSpecificAppointments()
    End Sub

    Private Sub LoadSpecificAppointments()
        Try
            connection()

            ' Fallback to defaults if dates weren't passed
            Dim startDate As String = If(String.IsNullOrEmpty(FilterStartDate), DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd 00:00:00"), FilterStartDate)
            Dim endDate As String = If(String.IsNullOrEmpty(FilterEndDate), DateTime.Now.ToString("yyyy-MM-dd 23:59:59"), FilterEndDate)

            sql = "SELECT ControlNo AS 'Control No.', FullName AS 'Full Name', RequestType AS 'Document Type', " &
                  "Purpose, Department, Amount, DateSubmitted AS 'Date Submitted', Status " &
                  "FROM appointments " &
                  "WHERE RequestType = @docType AND DateSubmitted BETWEEN @fromDate AND @toDate " &
                  "ORDER BY AppointmentID DESC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@docType", TargetDocumentType)
            cmd.Parameters.AddWithValue("@fromDate", startDate)
            cmd.Parameters.AddWithValue("@toDate", endDate)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvSpecificAppointments.DataSource = dt

            If dgvSpecificAppointments.Columns.Contains("Amount") Then
                dgvSpecificAppointments.Columns("Amount").DefaultCellStyle.Format = "N2"
                dgvSpecificAppointments.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If

        Catch ex As Exception
            MsgBox("Error loading specific appointments: " & ex.Message, MsgBoxStyle.Critical)
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
        defaultRowStyle.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        defaultRowStyle.SelectionBackColor = Color.FromArgb(210, 215, 240)
        defaultRowStyle.SelectionForeColor = Color.Black
        defaultRowStyle.Padding = New Padding(10, 4, 10, 4)

        Dim alternatingRowStyle As New DataGridViewCellStyle(defaultRowStyle)
        alternatingRowStyle.BackColor = Color.FromArgb(235, 237, 255)

        dgv.DefaultCellStyle = defaultRowStyle
        dgv.AlternatingRowsDefaultCellStyle = alternatingRowStyle
        dgv.RowTemplate.Height = 32
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

End Class