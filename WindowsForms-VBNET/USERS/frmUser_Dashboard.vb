Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class frmUser_Dashboard

    Private loggedResidentID As Integer = 0
    Private activeFilterLabel As Label = Nothing

    Private Sub frmUser_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblDateTime.Text = DateTime.Now.ToString("F")
        Timer1.Interval = 1000
        Timer1.Start()

        StyleDataGridView(dgvRequests)
        ApplyCorporateButtonStyles()

        SetActiveLabel(lblPending)
        RefreshDashboardData()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblDateTime.Text = DateTime.Now.ToString("F")
    End Sub

    Private Sub ApplyCorporateButtonStyles()
        Dim CorporateStyle = Sub(btn As Button, backColor As Color, foreColor As Color)
                                 btn.FlatStyle = FlatStyle.Flat
                                 btn.FlatAppearance.BorderSize = 1
                                 btn.FlatAppearance.BorderColor = Color.FromArgb(200, 205, 215)
                                 btn.BackColor = backColor
                                 btn.ForeColor = foreColor
                                 btn.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
                                 btn.Cursor = Cursors.Hand
                             End Sub

        If btnCreateRequest IsNot Nothing Then CorporateStyle(btnCreateRequest, Color.FromArgb(10, 25, 100), Color.White)
        If btnRef IsNot Nothing Then CorporateStyle(btnRef, Color.FromArgb(245, 247, 250), Color.FromArgb(40, 50, 70))
    End Sub

    Private Sub RefreshDashboardData()
        AutoRejectExpiredAppointments() ' Automatically rejects pending items older than 24 hours
        LoadResidentProfile()
        LoadStatusCounts()
        LoadTaskMetrics()

        Dim currentStatus As String = "PENDING"
        If activeFilterLabel IsNot Nothing Then
            If activeFilterLabel Is lblApproved Then
                currentStatus = "APPROVED"
            ElseIf activeFilterLabel Is lblRejected Then
                currentStatus = "REJECTED"
            ElseIf activeFilterLabel Is lblCompleted Then
                currentStatus = "COMPLETED"
            ElseIf activeFilterLabel Is lblCancelled Then
                currentStatus = "CANCELLED"
            End If
        End If

        LoadUserRequests(currentStatus)
    End Sub

    ' --- AUTOMATICALLY REJECT PENDING APPOINTMENTS OLDER THAN 24 HOURS ---
    Private Sub AutoRejectExpiredAppointments()
        Try
            If cn.State <> ConnectionState.Open Then connection()

            sql = "UPDATE appointments SET Status = 'REJECTED', UpdatedAt = NOW() " &
                  "WHERE UPPER(Status) = 'PENDING' AND DateSubmitted < NOW() - INTERVAL 24 HOUR"

            cmd = New MySqlCommand(sql, cn)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ' Silent handling or log if needed
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadTaskMetrics()
        Dim dailyCount As Integer = 0
        Dim uncompletedCount As Integer = 0
        Dim totalCount As Integer = 0

        Try
            If cn.State <> ConnectionState.Open Then connection()

            sql = "SELECT COUNT(*) FROM appointments WHERE DATE(DateSubmitted) = CURDATE()"
            cmd = New MySqlCommand(sql, cn)
            dailyCount = Convert.ToInt32(cmd.ExecuteScalar())

            sql = "SELECT COUNT(*) FROM appointments WHERE UPPER(Status) <> 'COMPLETED'"
            cmd = New MySqlCommand(sql, cn)
            uncompletedCount = Convert.ToInt32(cmd.ExecuteScalar())

            sql = "SELECT COUNT(*) FROM appointments"
            cmd = New MySqlCommand(sql, cn)
            totalCount = Convert.ToInt32(cmd.ExecuteScalar())

            If lblDailyTasks IsNot Nothing Then
                lblDailyTasks.Text = $"Daily Tasks: {dailyCount}"
            End If

            If lblUncompletedTasks IsNot Nothing Then
                lblUncompletedTasks.Text = $"Uncompleted Tasks: {uncompletedCount}"
            End If

            If lblTotalTasks IsNot Nothing Then
                lblTotalTasks.Text = $"Total Tasks: {totalCount}"
            End If

        Catch ex As Exception
            MsgBox("Error loading task metrics: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub btnRef_Click(sender As Object, e As EventArgs) Handles btnRef.Click
        RefreshDashboardData()
    End Sub

    Private Sub AddGridActionButtons()
        If Not dgvRequests.Columns.Contains("colApprove") Then
            Dim btnApproveCol As New DataGridViewButtonColumn()
            btnApproveCol.Name = "colApprove"
            btnApproveCol.HeaderText = "Action"
            btnApproveCol.FlatStyle = FlatStyle.Flat
            dgvRequests.Columns.Add(btnApproveCol)
        End If

        If Not dgvRequests.Columns.Contains("colReject") Then
            Dim btnRejectCol As New DataGridViewButtonColumn()
            btnRejectCol.Name = "colReject"
            btnRejectCol.HeaderText = ""
            btnRejectCol.FlatStyle = FlatStyle.Flat
            dgvRequests.Columns.Add(btnRejectCol)
        End If
    End Sub

    Private Sub AdjustGridColumnsByStatus(currentStatus As String)
        If Not dgvRequests.Columns.Contains("colApprove") OrElse Not dgvRequests.Columns.Contains("colReject") Then Return

        Select Case currentStatus.ToUpper()
            Case "PENDING"
                dgvRequests.Columns("colApprove").Visible = True
                dgvRequests.Columns("colApprove").HeaderText = "Action"
                dgvRequests.Columns("colReject").Visible = True

            Case "APPROVED"
                dgvRequests.Columns("colApprove").Visible = True
                dgvRequests.Columns("colApprove").HeaderText = "Action"
                dgvRequests.Columns("colReject").Visible = True

            Case "REJECTED"
                dgvRequests.Columns("colApprove").Visible = True
                dgvRequests.Columns("colApprove").HeaderText = "Action"
                dgvRequests.Columns("colReject").Visible = False

            Case Else
                dgvRequests.Columns("colApprove").Visible = False
                dgvRequests.Columns("colReject").Visible = False
        End Select
    End Sub

    Private Sub dgvRequests_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRequests.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim controlNo As String = dgvRequests.Rows(e.RowIndex).Cells("Control No.").Value.ToString()
        Dim currentStatus As String = dgvRequests.Rows(e.RowIndex).Cells("Status").Value.ToString().ToUpper()
        Dim colName As String = dgvRequests.Columns(e.ColumnIndex).Name

        If currentStatus = "PENDING" Then
            If colName = "colApprove" Then
                If MsgBox($"Are you sure you want to approve request [{controlNo}]?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Approval") = MsgBoxResult.Yes Then
                    UpdateRequestStatus(controlNo, "APPROVED")
                End If
            ElseIf colName = "colReject" Then
                If MsgBox($"Are you sure you want to reject request [{controlNo}]?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Rejection") = MsgBoxResult.Yes Then
                    UpdateRequestStatus(controlNo, "REJECTED")
                End If
            End If

        ElseIf currentStatus = "APPROVED" Then
            If colName = "colApprove" Then
                Using frmPay As New frmPayments()
                    frmPay.SelectedControlNo = controlNo
                    If frmPay.ShowDialog() = DialogResult.OK Then
                        SetActiveLabel(lblCompleted)
                        RefreshDashboardData()
                    End If
                End Using

            ElseIf colName = "colReject" Then
                If MsgBox($"Are you sure you want to cancel appointment [{controlNo}]?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Confirm Cancellation") = MsgBoxResult.Yes Then
                    UpdateRequestStatus(controlNo, "CANCELLED")
                End If
            End If

        ElseIf currentStatus = "REJECTED" Then
            If colName = "colApprove" Then
                If MsgBox($"Do you want to re-apply for appointment [{controlNo}]? This will resubmit the appointment to PENDING status.", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Re-Application") = MsgBoxResult.Yes Then
                    ReApplyAppointment(controlNo)
                End If
            End If
        End If
    End Sub

    Private Sub ReApplyAppointment(controlNo As String)
        Try
            connection()
            sql = "UPDATE appointments SET Status = 'PENDING', DateSubmitted = NOW(), UpdatedAt = NOW() WHERE ControlNo = @ctrl"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ctrl", controlNo)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            If rowsAffected > 0 Then
                MsgBox($"Request [{controlNo}] has been resubmitted and moved to PENDING status!", MsgBoxStyle.Information, "Re-Applied Successfully")

                Using frmCoupon As New frmCouponView(controlNo)
                    frmCoupon.ShowDialog()
                End Using

                SetActiveLabel(lblPending)
                RefreshDashboardData()
            Else
                MsgBox("Failed to resubmit request. Record not found.", MsgBoxStyle.Exclamation, "Warning")
            End If

        Catch ex As Exception
            MsgBox("Error re-applying appointment: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub dgvRequests_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRequests.CellDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = dgvRequests.Columns(e.ColumnIndex).Name
            If colName = "colApprove" OrElse colName = "colReject" Then Return

            Dim controlNo As String = dgvRequests.Rows(e.RowIndex).Cells("Control No.").Value.ToString()

            Using frmDetails As New frmAppointmentDetails(controlNo)
                If frmDetails.ShowDialog() = DialogResult.OK Then
                    RefreshDashboardData()
                End If
            End Using
        End If
    End Sub

    Private Sub dgvRequests_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvRequests.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = dgvRequests.Columns(e.ColumnIndex).Name

            If colName = "colApprove" OrElse colName = "colReject" Then
                e.PaintBackground(e.CellBounds, True)

                Dim rowStatus As String = dgvRequests.Rows(e.RowIndex).Cells("Status").Value.ToString().ToUpper()

                Dim drawButton As Boolean = False
                Dim buttonColor As Color = Color.Gray
                Dim btnText As String = ""

                If rowStatus = "PENDING" Then
                    If colName = "colApprove" Then
                        buttonColor = Color.FromArgb(10, 25, 100)
                        btnText = "APPROVE"
                        drawButton = True
                    ElseIf colName = "colReject" Then
                        buttonColor = Color.FromArgb(178, 34, 34)
                        btnText = "REJECT"
                        drawButton = True
                    End If

                ElseIf rowStatus = "APPROVED" Then
                    If colName = "colApprove" Then
                        buttonColor = Color.FromArgb(10, 25, 100)
                        btnText = "PAY / WAIVE"
                        drawButton = True
                    ElseIf colName = "colReject" Then
                        buttonColor = Color.FromArgb(220, 53, 69)
                        btnText = "CANCEL"
                        drawButton = True
                    End If

                ElseIf rowStatus = "REJECTED" Then
                    If colName = "colApprove" Then
                        buttonColor = Color.FromArgb(40, 167, 69)
                        btnText = "RE-APPLY"
                        drawButton = True
                    End If
                End If

                If drawButton Then
                    Dim buttonRect As New Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + 4, e.CellBounds.Width - 12, e.CellBounds.Height - 8)
                    Dim cornerRadius As Integer = 8

                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                    Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                        Using brush As New SolidBrush(buttonColor)
                            e.Graphics.FillPath(brush, path)
                        End Using
                    End Using

                    TextRenderer.DrawText(e.Graphics, btnText, New Font("Segoe UI", 8.0F, FontStyle.Bold),
                                         buttonRect, Color.White,
                                         TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                End If

                e.Handled = True
            End If
        End If
    End Sub

    Private Function GetRoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim diameter As Integer = radius * 2
        Dim arc As New Rectangle(rect.Location, New Size(diameter, diameter))

        path.AddArc(arc, 180, 90)

        arc.X = rect.Right - diameter
        path.AddArc(arc, 270, 90)

        arc.Y = rect.Bottom - diameter
        path.AddArc(arc, 0, 90)

        arc.X = rect.Left
        path.AddArc(arc, 90, 90)

        path.CloseFigure()
        Return path
    End Function

    Private Sub UpdateRequestStatus(controlNo As String, newStatus As String)
        Try
            connection()
            sql = "UPDATE appointments SET Status = @status, UpdatedAt = NOW() WHERE ControlNo = @ctrl"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@status", newStatus)
            cmd.Parameters.AddWithValue("@ctrl", controlNo)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            If rowsAffected > 0 Then
                MsgBox($"Request [{controlNo}] successfully updated to {newStatus}!", MsgBoxStyle.Information, "Success")

                If newStatus.ToUpper() = "APPROVED" Then
                    Using frmCoupon As New frmCouponView(controlNo)
                        frmCoupon.ShowDialog()
                    End Using
                End If

                RefreshDashboardData()
            Else
                MsgBox("Failed to update status. Record not found.", MsgBoxStyle.Exclamation, "Warning")
            End If

        Catch ex As Exception
            MsgBox("Error updating status: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
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
        headerStyle.BackColor = Color.FromArgb(248, 249, 252)
        headerStyle.ForeColor = Color.FromArgb(50, 50, 60)
        headerStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
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
        dgv.RowTemplate.Height = 32
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub LoadResidentProfile()
        Try
            connection()

            Dim parsedLname As String = ""
            Dim parsedFname As String = ""

            If Not String.IsNullOrEmpty(LoggedFullname) AndAlso LoggedFullname.Contains(",") Then
                Dim parts() As String = LoggedFullname.Split(","c)
                parsedLname = parts(0).Trim()
                parsedFname = parts(1).Trim()
            ElseIf Not String.IsNullOrEmpty(LoggedFullname) Then
                parsedFname = LoggedFullname.Trim()
            End If

            Dim userDisplayName As String = LoggedFullname

            sql = "SELECT ResidentID, CONCAT(Lastname, ', ', Firstname) AS SystemFullName, Picture " &
                  "FROM residences WHERE (Lastname=@lname AND Firstname=@fname) OR Username=@name"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@lname", parsedLname)
            cmd.Parameters.AddWithValue("@fname", parsedFname)
            cmd.Parameters.AddWithValue("@name", LoggedFullname)
            dr = cmd.ExecuteReader()

            Dim profileFound As Boolean = False

            If dr.Read() Then
                profileFound = True
                loggedResidentID = If(IsDBNull(dr("ResidentID")), 0, Convert.ToInt32(dr("ResidentID")))
                userDisplayName = dr("SystemFullName").ToString()

                If Not IsDBNull(dr("Picture")) Then
                    Dim imgBytes As Byte() = CType(dr("Picture"), Byte())
                    Using ms As New MemoryStream(imgBytes)
                        Dim rawImg As Image = Image.FromStream(ms)
                        If picUserProfile.Image IsNot Nothing Then picUserProfile.Image.Dispose()
                        picUserProfile.Image = MakeCircularImage(rawImg)
                    End Using
                End If
            End If
            dr.Close()

            If Not profileFound Then
                sql = "SELECT UserID, CONCAT(Lastname, ', ', Firstname) AS SystemFullName, Picture " &
                      "FROM users WHERE (Lastname=@lname AND Firstname=@fname) OR Username=@name"

                cmd = New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@lname", parsedLname)
                cmd.Parameters.AddWithValue("@fname", parsedFname)
                cmd.Parameters.AddWithValue("@name", LoggedFullname)
                dr = cmd.ExecuteReader()

                If dr.Read() Then
                    loggedResidentID = If(IsDBNull(dr("UserID")), 0, Convert.ToInt32(dr("UserID")))
                    userDisplayName = dr("SystemFullName").ToString()

                    If Not IsDBNull(dr("Picture")) Then
                        Dim imgBytes As Byte() = CType(dr("Picture"), Byte())
                        Using ms As New MemoryStream(imgBytes)
                            Dim rawImg As Image = Image.FromStream(ms)
                            If picUserProfile.Image IsNot Nothing Then picUserProfile.Image.Dispose()
                            picUserProfile.Image = MakeCircularImage(rawImg)
                        End Using
                    End If
                End If
                dr.Close()
            End If

            lblWelcomeUser.Text = $"{userDisplayName}!"

        Catch ex As Exception
            MsgBox("Error loading profile: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Function MakeCircularImage(srcImage As Image) As Image
        Dim size As Integer = Math.Min(srcImage.Width, srcImage.Height)
        Dim bmp As New Bitmap(size, size)

        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.CompositingQuality = CompositingQuality.HighQuality

            Using path As New GraphicsPath()
                path.AddEllipse(0, 0, size, size)
                g.SetClip(path)

                Dim srcRect As New Rectangle((srcImage.Width - size) \ 2, (srcImage.Height - size) \ 2, size, size)
                g.DrawImage(srcImage, New Rectangle(0, 0, size, size), srcRect, GraphicsUnit.Pixel)
            End Using
        End Using

        Return bmp
    End Function

    Private Sub picUserProfile_Paint(sender As Object, e As PaintEventArgs) Handles picUserProfile.Paint
        If picUserProfile.Image IsNot Nothing Then
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            Dim borderRect As New Rectangle(1, 1, picUserProfile.Width - 3, picUserProfile.Height - 3)
            Using pen As New Pen(Color.FromArgb(200, 205, 220), 2)
                e.Graphics.DrawEllipse(pen, borderRect)
            End Using
        End If
    End Sub

    Private Sub LoadStatusCounts()
        Dim pendingCount As Integer = 0
        Dim approvedCount As Integer = 0
        Dim rejectedCount As Integer = 0
        Dim completedCount As Integer = 0
        Dim cancelledCount As Integer = 0

        Try
            If cn.State <> ConnectionState.Open Then connection()

            sql = "SELECT UPPER(Status) AS StatusName, COUNT(*) AS Total FROM appointments WHERE DATE(DateSubmitted) = CURDATE() GROUP BY UPPER(Status)"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()

            While dr.Read()
                Dim st As String = dr("StatusName").ToString().Trim()
                Dim count As Integer = Convert.ToInt32(dr("Total"))

                Select Case st
                    Case "PENDING"
                        pendingCount = count
                    Case "APPROVED"
                        approvedCount = count
                    Case "REJECTED"
                        rejectedCount = count
                    Case "COMPLETED"
                        completedCount = count
                    Case "CANCELLED"
                        cancelledCount = count
                End Select
            End While
            dr.Close()

            lblPending.Text = $"Pending ({pendingCount})"
            lblApproved.Text = $"Approved ({approvedCount})"
            lblRejected.Text = $"Rejected ({rejectedCount})"
            lblCompleted.Text = $"Completed ({completedCount})"
            lblCancelled.Text = $"Cancelled ({cancelledCount})"

        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadUserRequests(Optional statusFilter As String = "")
        Try
            connection()

            ' Filtered to ONLY show Today's Appointments via DATE(DateSubmitted) = CURDATE()
            sql = "SELECT ControlNo AS 'Control No.', RequestType AS 'Request Type', Purpose, " &
                  "Department, DateSubmitted AS 'Date Submitted', PaymentStatus AS 'Payment Status', Status " &
                  "FROM appointments " &
                  "WHERE DATE(DateSubmitted) = CURDATE() "

            If Not String.IsNullOrEmpty(statusFilter) Then
                sql &= "AND UPPER(Status) = @status "
            End If

            sql &= "ORDER BY AppointmentID DESC"

            cmd = New MySqlCommand(sql, cn)

            If Not String.IsNullOrEmpty(statusFilter) Then
                cmd.Parameters.AddWithValue("@status", statusFilter.ToUpper())
            End If

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvRequests.DataSource = dt

            AddGridActionButtons()
            AdjustGridColumnsByStatus(statusFilter)

            If dgvRequests.Columns.Count > 0 Then
                dgvRequests.Columns(0).DefaultCellStyle.BackColor = Color.FromArgb(220, 225, 255)
            End If

        Catch ex As Exception
            MsgBox("Error loading requests: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub lblPending_Click(sender As Object, e As EventArgs) Handles lblPending.Click
        SetActiveLabel(lblPending)
        LoadUserRequests("PENDING")
    End Sub

    Private Sub lblApproved_Click(sender As Object, e As EventArgs) Handles lblApproved.Click
        SetActiveLabel(lblApproved)
        LoadUserRequests("APPROVED")
    End Sub

    Private Sub lblRejected_Click(sender As Object, e As EventArgs) Handles lblRejected.Click
        SetActiveLabel(lblRejected)
        LoadUserRequests("REJECTED")
    End Sub

    Private Sub lblCompleted_Click(sender As Object, e As EventArgs) Handles lblCompleted.Click
        SetActiveLabel(lblCompleted)
        LoadUserRequests("COMPLETED")
    End Sub

    Private Sub lblCancelled_Click(sender As Object, e As EventArgs) Handles lblCancelled.Click
        SetActiveLabel(lblCancelled)
        LoadUserRequests("CANCELLED")
    End Sub

    Private Sub SetActiveLabel(targetLabel As Label)
        Dim allFilterLabels As Label() = {lblPending, lblApproved, lblRejected, lblCompleted, lblCancelled}

        For Each lbl As Label In allFilterLabels
            If lbl IsNot Nothing Then
                lbl.Font = New Font(lbl.Font, FontStyle.Regular)
                lbl.ForeColor = Color.Black
            End If
        Next

        If targetLabel IsNot Nothing Then
            targetLabel.Font = New Font(targetLabel.Font, FontStyle.Bold)
            targetLabel.ForeColor = Color.Navy
            activeFilterLabel = targetLabel
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim filterText As String = txtSearch.Text.Trim().Replace("'", "''")
        If dgvRequests.DataSource IsNot Nothing Then
            Dim dt As DataTable = CType(dgvRequests.DataSource, DataTable)
            dt.DefaultView.RowFilter = $"`Control No.` LIKE '%{filterText}%' OR `Request Type` LIKE '%{filterText}%' OR Purpose LIKE '%{filterText}%'"
        End If
    End Sub

    Private Sub btnCreateRequest_Click(sender As Object, e As EventArgs) Handles btnCreateRequest.Click
        Using frm As New frmCreateAppointment()
            If frm.ShowDialog() = DialogResult.OK Then
                Dim newControlNo As String = GetLatestCreatedControlNo()

                If Not String.IsNullOrEmpty(newControlNo) Then
                    Using frmCoupon As New frmCouponView(newControlNo)
                        frmCoupon.ShowDialog()
                    End Using
                End If

                SetActiveLabel(lblPending)
                RefreshDashboardData()
            End If
        End Using
    End Sub

    Private Function GetLatestCreatedControlNo() As String
        Dim ctrlNo As String = ""
        Try
            connection()
            sql = "SELECT ControlNo FROM appointments ORDER BY AppointmentID DESC LIMIT 1"
            cmd = New MySqlCommand(sql, cn)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                ctrlNo = result.ToString()
            End If
        Catch ex As Exception
            MsgBox("Error retrieving new appointment coupon: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
        Return ctrlNo
    End Function

End Class