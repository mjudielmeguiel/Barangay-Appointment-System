Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text

Public Class frmActivityLogs

    Private Sub frmActivityLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Pamagat at petsa
        Me.Text = "Activity Logs & Security Monitoring"
        dtpFrom.Value = DateTime.Today.AddDays(-7) ' Huling 7 araw bilang default
        dtpTo.Value = DateTime.Now

        ' Punan ang Action Type filter
        cboActionType.Items.Clear()
        cboActionType.Items.Add("All")
        cboActionType.Items.Add("LOGIN")
        cboActionType.Items.Add("FAILED_LOGIN")
        cboActionType.Items.Add("EMPTY_ATTEMPT")
        cboActionType.Items.Add("ACCESS_DENIED")
        cboActionType.SelectedIndex = 0

        ' I-setup ang DataGridView
        SetupDataGridView()

        ' I-load ang unang datos
        LoadLogs()
    End Sub

    Private Sub SetupDataGridView()
        With dgvLogs
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .RowHeadersVisible = False
            .EnableHeadersVisualStyles = False
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .GridColor = Color.FromArgb(220, 224, 230)

            ' Header Styling (Dark Blue)
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 25, 85)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
            .ColumnHeadersHeight = 40

            ' Row Styling & Height
            .RowTemplate.Height = 35
            .DefaultCellStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
            .DefaultCellStyle.ForeColor = Color.Black
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 220, 245)
            .DefaultCellStyle.SelectionForeColor = Color.Black

            ' Alternating Row Colors (Light grayish-blue tint)
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 245, 250)
            .DefaultCellStyle.BackColor = Color.White

            .ClearSelection()
        End With
    End Sub

    Private Sub LoadLogs()
        Try
            connection()

            Dim fromDate As String = dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00")
            Dim toDate As String = dtpTo.Value.ToString("yyyy-MM-dd 23:59:59")
            Dim actionFilter As String = ""

            ' Kung may napiling uri, magdagdag ng kondisyon
            If cboActionType.SelectedIndex > 0 Then
                actionFilter = " AND ActionType = @actionType "
            End If

            Dim sql As String = "SELECT LogID, ActionDate, FullName, UserRole, ActionType, " &
                                       "Module, Details, IPAddress, DeviceInfo " &
                                "FROM activity_logs " &
                                "WHERE ActionDate BETWEEN @fromDate AND @toDate " &
                                actionFilter &
                                "ORDER BY ActionDate DESC"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@fromDate", fromDate)
                cmd.Parameters.AddWithValue("@toDate", toDate)
                If cboActionType.SelectedIndex > 0 Then
                    cmd.Parameters.AddWithValue("@actionType", cboActionType.Text)
                End If

                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    dgvLogs.DataSource = dt

                    ' Ayusin ang pangalan ng ulo
                    dgvLogs.Columns("LogID").HeaderText = "ID"
                    dgvLogs.Columns("ActionDate").HeaderText = "Date & Time"
                    dgvLogs.Columns("FullName").HeaderText = "Name"
                    dgvLogs.Columns("UserRole").HeaderText = "Role"
                    dgvLogs.Columns("ActionType").HeaderText = "Action Type"
                    dgvLogs.Columns("Module").HeaderText = "Module"
                    dgvLogs.Columns("Details").HeaderText = "Details / Input"
                    dgvLogs.Columns("IPAddress").HeaderText = "IP Address"
                    dgvLogs.Columns("DeviceInfo").HeaderText = "Device / OS"

                    ' Ipakita ang kabuuan
                    lblCount.Text = $"Total Records: {dt.Rows.Count:N0}"
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading logs: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadLogs()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If dgvLogs.Rows.Count = 0 Then
                MsgBox("No data to export.", MsgBoxStyle.Information)
                Return
            End If

            Dim saveFile As New SaveFileDialog()
            saveFile.Filter = "CSV File (*.csv)|*.csv"
            saveFile.FileName = $"ActivityLogs_{DateTime.Now:yyyyMMdd_HHmmss}.csv"

            If saveFile.ShowDialog() = DialogResult.OK Then
                Dim sb As New StringBuilder()

                ' Ulo
                Dim headers = dgvLogs.Columns.Cast(Of DataGridViewColumn)().Select(Function(c) c.HeaderText)
                sb.AppendLine(String.Join(",", headers))

                ' Nilalaman
                For Each row As DataGridViewRow In dgvLogs.Rows
                    Dim cells = row.Cells.Cast(Of DataGridViewCell)().Select(Function(c) """" & If(c.Value IsNot Nothing, c.Value.ToString().Replace("""", """"""), "") & """")
                    sb.AppendLine(String.Join(",", cells))
                Next

                File.WriteAllText(saveFile.FileName, sb.ToString(), Encoding.UTF8)
                MsgBox($"Exported successfully!{vbCrLf}{saveFile.FileName}", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox("Export Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' I-refresh kapag nagbago ang petsa o filter
    Private Sub dtpFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged
        LoadLogs()
    End Sub

    Private Sub dtpTo_ValueChanged(sender As Object, e As EventArgs) Handles dtpTo.ValueChanged
        LoadLogs()
    End Sub

    Private Sub cboActionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboActionType.SelectedIndexChanged
        LoadLogs()
    End Sub

End Class