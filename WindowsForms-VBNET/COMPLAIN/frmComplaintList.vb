Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D

Public Class frmComplaintList

    ' ===== Theme =====
    Private ReadOnly Navy As Color = Color.FromArgb(25, 42, 86)
    Private ReadOnly NavyHover As Color = Color.FromArgb(45, 68, 128)
    Private ReadOnly LightBlue As Color = Color.FromArgb(210, 220, 245)
    Private ReadOnly AltRow As Color = Color.FromArgb(247, 249, 252)
    Private ReadOnly LineColor As Color = Color.FromArgb(233, 236, 242)
    Private ReadOnly TextColor As Color = Color.FromArgb(45, 52, 70)

    Private hoverRow As Integer = -1

    Private Sub frmComplaintList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StyleGrid()
        LoadComplaints()
    End Sub

    ' ===== Grid design =====
    Private Sub StyleGrid()
        GetType(DataGridView).GetProperty("DoubleBuffered",
            Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).
            SetValue(dgvComplaints, True, Nothing)

        With dgvComplaints
            .EnableHeadersVisualStyles = False
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .GridColor = LineColor
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .ReadOnly = True
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 42
            .RowTemplate.Height = 40

            With .DefaultCellStyle
                .Font = New Font("Segoe UI", 9)
                .ForeColor = TextColor
                .BackColor = Color.White
                .SelectionBackColor = LightBlue
                .SelectionForeColor = Navy
                .Padding = New Padding(6, 0, 6, 0)
            End With

            .AlternatingRowsDefaultCellStyle.BackColor = AltRow

            With .ColumnHeadersDefaultCellStyle
                .Font = New Font("Segoe UI", 9.5, FontStyle.Bold)
                .BackColor = Navy
                .ForeColor = Color.White
                .SelectionBackColor = Navy
                .SelectionForeColor = Color.White
                .Alignment = DataGridViewContentAlignment.MiddleLeft
                .Padding = New Padding(6, 0, 6, 0)
            End With
        End With
    End Sub

    Private Sub FormatGrid()
        With dgvComplaints
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            .Columns("ComplaintID").Visible = False
            .Columns("ControlNo").HeaderText = "Control No."
            .Columns("ComplaintType").HeaderText = "Type"
            .Columns("Title").HeaderText = "Subject / Title"
            .Columns("DateSubmitted").HeaderText = "Date Submitted"
            .Columns("Status").HeaderText = "Status"
            .Columns("IsAnonymous").HeaderText = "Anonymous"

            .Columns("DateSubmitted").DefaultCellStyle.Format = "yyyy-MM-dd"

            .Columns("ControlNo").FillWeight = 90
            .Columns("ComplaintType").FillWeight = 90
            .Columns("Title").FillWeight = 180
            .Columns("DateSubmitted").FillWeight = 90
            .Columns("Status").FillWeight = 80
            .Columns("IsAnonymous").FillWeight = 70

            .Columns("ControlNo").HeaderCell.Style.BackColor = LightBlue
            .Columns("ControlNo").HeaderCell.Style.SelectionBackColor = LightBlue
            .Columns("ControlNo").HeaderCell.Style.ForeColor = Color.White

            If Not .Columns.Contains("colRespond") Then
                Dim btnCol As New DataGridViewButtonColumn()
                btnCol.Name = "colRespond"
                btnCol.HeaderText = "Action"
                btnCol.SortMode = DataGridViewColumnSortMode.NotSortable
                btnCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                btnCol.Width = 120
                btnCol.Resizable = DataGridViewTriState.False
                .Columns.Add(btnCol)
            End If
            .Columns("colRespond").DisplayIndex = .Columns.Count - 1
        End With
    End Sub

    ' ===== Data =====
    Private Sub LoadComplaints(Optional searchKeyword As String = "")
        Try
            connection()

            Dim sql As String = "SELECT ComplaintID, ControlNo, ComplaintType, Title, " &
                                "DateSubmitted, Status, " &
                                "CASE WHEN IsAnonymous = 1 THEN 'Yes' ELSE 'No' END AS IsAnonymous " &
                                "FROM complaints"

            Dim hasSearch As Boolean = Not String.IsNullOrWhiteSpace(searchKeyword)
            If hasSearch Then
                sql &= " WHERE ControlNo LIKE @kw OR Title LIKE @kw OR ComplaintType LIKE @kw OR Status LIKE @kw"
            End If
            sql &= " ORDER BY DateSubmitted DESC"

            Using cmdList As New MySqlCommand(sql, cn)
                If hasSearch Then cmdList.Parameters.AddWithValue("@kw", "%" & searchKeyword & "%")

                Using da As New MySqlDataAdapter(cmdList)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvComplaints.DataSource = dt
                End Using
            End Using

            FormatGrid()

        Catch ex As Exception
            MsgBox("Error loading complaints: " & ex.Message, MsgBoxStyle.Critical, "Complaints")
        Finally
            CloseConnection()
        End Try
    End Sub

    ' ===== Cell display =====
    Private Sub dgvComplaints_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvComplaints.CellFormatting
        If e.RowIndex < 0 OrElse e.Value Is Nothing OrElse IsDBNull(e.Value) Then Exit Sub

        Dim colName As String = dgvComplaints.Columns(e.ColumnIndex).Name

        If colName = "Status" Then
            Dim st As String = e.Value.ToString().Trim().ToLower()
            e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            Select Case st
                Case "pending" : e.CellStyle.ForeColor = Color.FromArgb(191, 113, 0)
                Case "in progress" : e.CellStyle.ForeColor = Color.FromArgb(30, 90, 200)
                Case "resolved" : e.CellStyle.ForeColor = Color.FromArgb(22, 128, 70)
            End Select
        End If
    End Sub

    Private Sub dgvComplaints_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvComplaints.DataError
        e.ThrowException = False
    End Sub

    ' ===== Modern Button Painting =====
    Private Sub dgvComplaints_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvComplaints.CellPainting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
        If dgvComplaints.Columns(e.ColumnIndex).Name <> "colRespond" Then Exit Sub

        e.Paint(e.CellBounds, DataGridViewPaintParts.Background Or DataGridViewPaintParts.Border)

        Dim isResolved As Boolean = False
        Dim stVal = dgvComplaints.Rows(e.RowIndex).Cells("Status").Value
        If stVal IsNot Nothing AndAlso Not IsDBNull(stVal) Then
            isResolved = stVal.ToString().Trim().ToLower() = "resolved"
        End If

        Dim rect As New Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 7,
                              e.CellBounds.Width - 20, e.CellBounds.Height - 14)
        Dim isHover As Boolean = (e.RowIndex = hoverRow)

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Using path As GraphicsPath = RoundedRect(rect, 6)
            If isResolved Then
                Using br As New SolidBrush(If(isHover, LightBlue, Color.White))
                    e.Graphics.FillPath(br, path)
                End Using
                Using pen As New Pen(Navy, 1.2F)
                    e.Graphics.DrawPath(pen, path)
                End Using
                TextRenderer.DrawText(e.Graphics, "View", New Font("Segoe UI Semibold", 9),
                                      rect, Navy,
                                      TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            Else
                Using br As New SolidBrush(If(isHover, NavyHover, Navy))
                    e.Graphics.FillPath(br, path)
                End Using
                TextRenderer.DrawText(e.Graphics, "Respond", New Font("Segoe UI Semibold", 9),
                                      rect, Color.White,
                                      TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            End If
        End Using

        e.Handled = True
    End Sub

    Private Function RoundedRect(r As Rectangle, radius As Integer) As GraphicsPath
        Dim d As Integer = radius * 2
        Dim p As New GraphicsPath()
        p.AddArc(r.X, r.Y, d, d, 180, 95)
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        p.CloseFigure()
        Return p
    End Function

    Private Sub dgvComplaints_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvComplaints.CellMouseEnter
        If e.RowIndex >= 0 AndAlso dgvComplaints.Columns(e.ColumnIndex).Name = "colRespond" Then
            hoverRow = e.RowIndex
            dgvComplaints.Cursor = Cursors.Hand
            dgvComplaints.InvalidateCell(e.ColumnIndex, e.RowIndex)
        End If
    End Sub

    Private Sub dgvComplaints_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvComplaints.CellMouseLeave
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 AndAlso
           dgvComplaints.Columns(e.ColumnIndex).Name = "colRespond" Then
            hoverRow = -1
            dgvComplaints.InvalidateCell(e.ColumnIndex, e.RowIndex)
        End If
        dgvComplaints.Cursor = Cursors.Default
    End Sub

    ' ===== Actions: Load inside frmMain.Panel2 =====
    Private Sub dgvComplaints_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvComplaints.CellClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
        If dgvComplaints.Columns(e.ColumnIndex).Name = "colRespond" Then
            OpenDetails(e.RowIndex, True)
        End If
    End Sub

    Private Sub dgvComplaints_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvComplaints.CellDoubleClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
        If dgvComplaints.Columns(e.ColumnIndex).Name <> "colRespond" Then
            OpenDetails(e.RowIndex, False)
        End If
    End Sub

    Private Sub OpenDetails(rowIndex As Integer, respondMode As Boolean)
        Dim complaintID As Integer = Convert.ToInt32(dgvComplaints.Rows(rowIndex).Cells("ComplaintID").Value)

        Dim mainForm = TryCast(Application.OpenForms("frmMain"), frmMain)
        If mainForm IsNot Nothing Then
            ' I-pass ang complaintID at respondMode sa constructor
            Dim Complaints As New frmComplaintDetails(complaintID, respondMode)
            Complaints.TopLevel = False
            Complaints.FormBorderStyle = FormBorderStyle.None
            Complaints.Dock = DockStyle.Fill

            mainForm.Panel2.Controls.Clear()
            mainForm.Panel2.Controls.Add(Complaints)
            Complaints.Show()
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadComplaints()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadComplaints(txtSearch.Text.Trim())
    End Sub
End Class