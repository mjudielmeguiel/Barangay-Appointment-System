Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class frmUser_Dashboard
    Private activeFilterLabel As Label = Nothing

    Private Sub frmUser_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StyleDataGridView(dgvRequests)
        ApplyCorporateButtonStyles()
        SetActiveLabel(lblProcessing)

        MakeCircularPictureBox(picProfile)
        LoadUserProfileInfo()

        RefreshDashboardData()
    End Sub

    Private Sub MakeCircularPictureBox(pb As PictureBox)
        If pb Is Nothing Then Return
        Dim size As Integer = Math.Min(pb.Width, pb.Height)
        pb.Width = size
        pb.Height = size
        Dim gp As New GraphicsPath()
        gp.AddEllipse(0, 0, pb.Width - 1, pb.Height - 1)
        pb.Region = New Region(gp)
        pb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    Private Sub LoadUserProfileInfo()
        Try
            If cn.State <> ConnectionState.Open Then connection()
            Dim sql As String = "SELECT Firstname, Lastname, Role, AssignedOffice, Department, Email, ContactNumber, AccountStatus, CreatedAt, Picture FROM users WHERE UserID = @userId"

            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@userId", frmlogin.LoggedInUserID)

                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        lblFullname.Text = $"{dr("Firstname")} {dr("Lastname")}".Trim()
                        lblRole.Text = dr("Role").ToString()

                        lblAssignedOffice.Text = If(IsDBNull(dr("AssignedOffice")), "N/A", dr("AssignedOffice").ToString())
                        lblDepartment.Text = If(IsDBNull(dr("Department")), "N/A", dr("Department").ToString())
                        lblEmail.Text = If(IsDBNull(dr("Email")), "N/A", dr("Email").ToString())

                        Dim contact As String = If(IsDBNull(dr("ContactNumber")), "", dr("ContactNumber").ToString().Trim())
                        lblContact.Text = If(String.IsNullOrWhiteSpace(contact), "N/A", contact)

                        Dim statusVal As String = If(IsDBNull(dr("AccountStatus")), "", dr("AccountStatus").ToString().Trim())
                        If String.IsNullOrWhiteSpace(statusVal) Then statusVal = "Unknown"

                        lblAccountStatus.Text = $"● {statusVal}"

                        If statusVal.Equals("Active", StringComparison.OrdinalIgnoreCase) Then
                            lblAccountStatus.ForeColor = Color.Green
                        Else
                            lblAccountStatus.ForeColor = Color.Gray
                        End If

                        If Not IsDBNull(dr("CreatedAt")) Then
                            Dim createdDate As DateTime = Convert.ToDateTime(dr("CreatedAt"))
                            lblDateCreated.Text = "Since " & createdDate.ToString("MMM dd, yyyy")
                        Else
                            lblDateCreated.Text = "N/A"
                        End If

                        If Not IsDBNull(dr("Picture")) Then
                            Dim picData As Byte() = CType(dr("Picture"), Byte())
                            Using ms As New MemoryStream(picData)
                                picProfile.Image = Image.FromStream(ms)
                            End Using
                        Else
                            picProfile.Image = Nothing
                        End If
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading profile: " & ex.Message, MsgBoxStyle.Exclamation)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub picProfile_DoubleClick(sender As Object, e As EventArgs) Handles picProfile.DoubleClick
        Using ofd As New OpenFileDialog()
            ofd.Title = "Select New Profile Picture"
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"
            If ofd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim newImage As Image = Image.FromFile(ofd.FileName)
                    picProfile.Image = newImage

                    Dim picBytes As Byte()
                    Using ms As New MemoryStream()
                        newImage.Save(ms, newImage.RawFormat)
                        picBytes = ms.ToArray()
                    End Using

                    UpdateProfilePicture(picBytes)
                    MsgBox("Profile picture updated successfully!", MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox("Failed to update picture: " & ex.Message, MsgBoxStyle.Exclamation)
                End Try
            End If
        End Using
    End Sub

    Private Sub UpdateProfilePicture(picBytes As Byte())
        Try
            If cn.State <> ConnectionState.Open Then connection()
            Dim sql As String = "UPDATE users SET Picture = @pic WHERE UserID = @userId"
            Using cmd As New MySqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@pic", picBytes)
                cmd.Parameters.AddWithValue("@userId", frmlogin.LoggedInUserID)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Throw New Exception("Database error: " & ex.Message)
        Finally
            CloseConnection()
        End Try
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
        AutoRejectExpiredAppointments()
        Dim currentStatus As String = "PROCESSING"
        If activeFilterLabel IsNot Nothing Then
            If activeFilterLabel Is lblPending Then
                currentStatus = "PENDING"
            ElseIf activeFilterLabel Is lblApprove Then
                currentStatus = "APPROVE"
            ElseIf activeFilterLabel Is lblProcessing Then
                currentStatus = "PROCESSING"
            ElseIf activeFilterLabel Is lblTopay Then
                currentStatus = "UNPAID"
            ElseIf activeFilterLabel Is lblRelease Then
                currentStatus = "TO RELEASE"
            ElseIf activeFilterLabel Is lblUnclaimed Then
                currentStatus = "UNCLAIMED"
            ElseIf activeFilterLabel Is lblComplete Then
                currentStatus = "COMPLETE"
            ElseIf activeFilterLabel Is lblRejected Then
                currentStatus = "REJECTED"
            ElseIf activeFilterLabel Is lblCancelled Then
                currentStatus = "CANCELLED"
            End If
        End If
        LoadUserRequests(currentStatus)
    End Sub

    Private Sub AutoRejectExpiredAppointments()
        Try
            If cn.State <> ConnectionState.Open Then connection()
            Dim qry As String = "UPDATE appointments SET Status = 'REJECTED', UpdatedAt = NOW() " &
                                "WHERE UPPER(Status) = 'PENDING' AND DateSubmitted < NOW() - INTERVAL 24 HOUR"
            Using cmdObj As New MySqlCommand(qry, cn)
                cmdObj.ExecuteNonQuery()
            End Using
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub UpdateDashboardCounts()
        Try
            If cn.State <> ConnectionState.Open Then connection()

            Dim sqlCounts As String = "SELECT UPPER(Status) AS StatusName, COUNT(*) AS Total " &
                                     "FROM appointments " &
                                     "WHERE DateSubmitted >= NOW() - INTERVAL 24 HOUR " &
                                     "GROUP BY UPPER(Status)"

            Dim pendingVal As Integer = 0
            Dim approveVal As Integer = 0
            Dim processingVal As Integer = 0
            Dim unpaidVal As Integer = 0
            Dim releaseVal As Integer = 0
            Dim unclaimedVal As Integer = 0
            Dim completeVal As Integer = 0
            Dim rejectedVal As Integer = 0
            Dim cancelledVal As Integer = 0

            Using cmdObj As New MySqlCommand(sqlCounts, cn)
                Using dr As MySqlDataReader = cmdObj.ExecuteReader()
                    While dr.Read()
                        Dim st As String = dr("StatusName").ToString().Trim()
                        Dim count As Integer = Convert.ToInt32(dr("Total"))

                        If st = "PENDING" Then
                            pendingVal += count
                        ElseIf st = "APPROVE" OrElse st = "APPROVED" Then
                            approveVal += count
                        ElseIf st = "PROCESSING" Then
                            processingVal += count
                        ElseIf st = "UNPAID" OrElse st = "TO PAY" Then
                            unpaidVal += count
                        ElseIf st = "TO RELEASE" OrElse st = "RELEASE" Then
                            releaseVal += count
                        ElseIf st = "UNCLAIMED" Then
                            unclaimedVal += count
                        ElseIf st = "COMPLETE" OrElse st = "COMPLETED" Then
                            completeVal += count
                        ElseIf st = "REJECTED" Then
                            rejectedVal += count
                        ElseIf st = "CANCELLED" Then
                            cancelledVal += count
                        End If
                    End While
                End Using
            End Using

            If lblPending IsNot Nothing Then lblPending.Text = pendingVal.ToString()
            If lblApprove IsNot Nothing Then lblApprove.Text = approveVal.ToString()
            If lblProcessing IsNot Nothing Then lblProcessing.Text = processingVal.ToString()
            If lblTopay IsNot Nothing Then lblTopay.Text = unpaidVal.ToString()
            If lblRelease IsNot Nothing Then lblRelease.Text = releaseVal.ToString()
            If lblUnclaimed IsNot Nothing Then lblUnclaimed.Text = unclaimedVal.ToString()
            If lblComplete IsNot Nothing Then lblComplete.Text = completeVal.ToString()
            If lblRejected IsNot Nothing Then lblRejected.Text = rejectedVal.ToString()
            If lblCancelled IsNot Nothing Then lblCancelled.Text = cancelledVal.ToString()

        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadUserRequests(statusFilter As String)
        Try
            connection()
            Dim queryStr As String = ""
            Dim cmdObj As New MySqlCommand()
            cmdObj.Connection = cn

            Dim upperStatus As String = statusFilter.ToUpper()

            ' Kapag COMPLETE section, isasama ang Pickup Date gamit ang NULLIF para iwas-date error
            If upperStatus = "COMPLETE" OrElse upperStatus = "COMPLETED" Then
                queryStr = "SELECT ControlNo AS `Control No.`, RequestType AS `Request Type`, Purpose, " &
                           "Department, NULLIF(DateSubmitted, '0000-00-00 00:00:00') AS `Date Submitted`, " &
                           "NULLIF(AppointmentDate, '0000-00-00 00:00:00') AS `Appointment Date`, " &
                           "NULLIF(ScheduledDate, '0000-00-00 00:00:00') AS `Pickup Date`, Status " &
                           "FROM appointments " &
                           "WHERE (UPPER(Status) = 'COMPLETE' OR UPPER(Status) = 'COMPLETED') " &
                           "AND DateSubmitted >= NOW() - INTERVAL 24 HOUR " &
                           "ORDER BY AppointmentID DESC"
            Else
                ' Sa ibang sections, walang Pickup Date column para hindi lumitaw
                queryStr = "SELECT ControlNo AS `Control No.`, RequestType AS `Request Type`, Purpose, " &
                           "Department, NULLIF(DateSubmitted, '0000-00-00 00:00:00') AS `Date Submitted`, " &
                           "NULLIF(AppointmentDate, '0000-00-00 00:00:00') AS `Appointment Date`, Status " &
                           "FROM appointments " &
                           "WHERE UPPER(Status) = @status " &
                           "AND DateSubmitted >= NOW() - INTERVAL 24 HOUR " &
                           "ORDER BY AppointmentID DESC"
                cmdObj.Parameters.AddWithValue("@status", upperStatus)
            End If

            cmdObj.CommandText = queryStr

            Dim da As New MySqlDataAdapter(cmdObj)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvRequests.DataSource = dt
            AddGridActionButtons()
            AdjustGridColumnsByStatus(statusFilter)

            UpdateDashboardCounts()

            If lblSectionTitle IsNot Nothing Then
                Select Case upperStatus
                    Case "PENDING" : lblSectionTitle.Text = "Pending Requests"
                    Case "APPROVE", "APPROVED" : lblSectionTitle.Text = "Approved Requests"
                    Case "PROCESSING" : lblSectionTitle.Text = "Processing Requests"
                    Case "UNPAID", "TO PAY" : lblSectionTitle.Text = "To Pay / Unpaid Requests"
                    Case "TO RELEASE", "RELEASE" : lblSectionTitle.Text = "To Release Requests"
                    Case "UNCLAIMED" : lblSectionTitle.Text = "Unclaimed Requests"
                    Case "COMPLETE", "COMPLETED" : lblSectionTitle.Text = "Completed Requests"
                    Case "REJECTED" : lblSectionTitle.Text = "Rejected Requests"
                    Case "CANCELLED" : lblSectionTitle.Text = "Cancelled Requests"
                    Case Else : lblSectionTitle.Text = "Appointment Requests"
                End Select
            End If

            If dgvRequests.Columns.Count > 0 Then
                dgvRequests.Columns(0).DefaultCellStyle.BackColor = Color.FromArgb(220, 225, 255)
            End If
        Catch ex As Exception
            MsgBox("Error loading requests: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub AddGridActionButtons()
        If Not dgvRequests.Columns.Contains("colView") Then
            Dim btnViewCol As New DataGridViewButtonColumn()
            btnViewCol.Name = "colView"
            btnViewCol.HeaderText = "View"
            btnViewCol.FlatStyle = FlatStyle.Flat
            btnViewCol.Width = 70
            dgvRequests.Columns.Add(btnViewCol)
        End If

        If Not dgvRequests.Columns.Contains("colEdit") Then
            Dim btnEditCol As New DataGridViewButtonColumn()
            btnEditCol.Name = "colEdit"
            btnEditCol.HeaderText = "Edit"
            btnEditCol.FlatStyle = FlatStyle.Flat
            btnEditCol.Width = 70
            dgvRequests.Columns.Add(btnEditCol)
        End If

        If Not dgvRequests.Columns.Contains("colPay") Then
            Dim btnPayCol As New DataGridViewButtonColumn()
            btnPayCol.Name = "colPay"
            btnPayCol.HeaderText = "Payment"
            btnPayCol.FlatStyle = FlatStyle.Flat
            btnPayCol.Width = 80
            dgvRequests.Columns.Add(btnPayCol)
        End If

        If Not dgvRequests.Columns.Contains("colReleaseBtn") Then
            Dim btnReleaseCol As New DataGridViewButtonColumn()
            btnReleaseCol.Name = "colReleaseBtn"
            btnReleaseCol.HeaderText = "Release Action"
            btnReleaseCol.FlatStyle = FlatStyle.Flat
            btnReleaseCol.Width = 90
            dgvRequests.Columns.Add(btnReleaseCol)
        End If

        If Not dgvRequests.Columns.Contains("colMarkUnclaimed") Then
            Dim btnMarkUnclaimed As New DataGridViewButtonColumn()
            btnMarkUnclaimed.Name = "colMarkUnclaimed"
            btnMarkUnclaimed.HeaderText = "Mark Unclaimed"
            btnMarkUnclaimed.FlatStyle = FlatStyle.Flat
            btnMarkUnclaimed.Width = 110
            dgvRequests.Columns.Add(btnMarkUnclaimed)
        End If

        If Not dgvRequests.Columns.Contains("colUnclaimedActionBtn") Then
            Dim btnUnclaimedAction As New DataGridViewButtonColumn()
            btnUnclaimedAction.Name = "colUnclaimedActionBtn"
            btnUnclaimedAction.HeaderText = "Action"
            btnUnclaimedAction.FlatStyle = FlatStyle.Flat
            btnUnclaimedAction.Width = 90
            dgvRequests.Columns.Add(btnUnclaimedAction)
        End If

        If Not dgvRequests.Columns.Contains("colCancel") Then
            Dim btnCancelCol As New DataGridViewButtonColumn()
            btnCancelCol.Name = "colCancel"
            btnCancelCol.HeaderText = "Cancel"
            btnCancelCol.FlatStyle = FlatStyle.Flat
            btnCancelCol.Width = 70
            dgvRequests.Columns.Add(btnCancelCol)
        End If

        If dgvRequests.Columns.Contains("colView") Then dgvRequests.Columns("colView").DisplayIndex = dgvRequests.Columns.Count - 7
        If dgvRequests.Columns.Contains("colEdit") Then dgvRequests.Columns("colEdit").DisplayIndex = dgvRequests.Columns.Count - 6
        If dgvRequests.Columns.Contains("colPay") Then dgvRequests.Columns("colPay").DisplayIndex = dgvRequests.Columns.Count - 5
        If dgvRequests.Columns.Contains("colReleaseBtn") Then dgvRequests.Columns("colReleaseBtn").DisplayIndex = dgvRequests.Columns.Count - 4
        If dgvRequests.Columns.Contains("colMarkUnclaimed") Then dgvRequests.Columns("colMarkUnclaimed").DisplayIndex = dgvRequests.Columns.Count - 3
        If dgvRequests.Columns.Contains("colUnclaimedActionBtn") Then dgvRequests.Columns("colUnclaimedActionBtn").DisplayIndex = dgvRequests.Columns.Count - 2
        If dgvRequests.Columns.Contains("colCancel") Then dgvRequests.Columns("colCancel").DisplayIndex = dgvRequests.Columns.Count - 1
    End Sub

    Private Sub AdjustGridColumnsByStatus(currentStatus As String)
        If Not dgvRequests.Columns.Contains("colView") OrElse Not dgvRequests.Columns.Contains("colEdit") OrElse Not dgvRequests.Columns.Contains("colPay") OrElse Not dgvRequests.Columns.Contains("colReleaseBtn") OrElse Not dgvRequests.Columns.Contains("colMarkUnclaimed") OrElse Not dgvRequests.Columns.Contains("colUnclaimedActionBtn") OrElse Not dgvRequests.Columns.Contains("colCancel") Then Return

        Dim upperStatus As String = currentStatus.ToUpper()

        If dgvRequests.Columns.Contains("Pickup Date") Then
            If upperStatus = "COMPLETE" OrElse upperStatus = "COMPLETED" Then
                dgvRequests.Columns("Pickup Date").Visible = True
            Else
                dgvRequests.Columns("Pickup Date").Visible = False
            End If
        End If

        If upperStatus = "PENDING" Then
            dgvRequests.Columns("colView").Visible = True
        Else
            dgvRequests.Columns("colView").Visible = False
        End If

        If upperStatus = "APPROVE" OrElse upperStatus = "APPROVED" OrElse upperStatus = "PROCESSING" Then
            dgvRequests.Columns("colEdit").Visible = True
        Else
            dgvRequests.Columns("colEdit").Visible = False
        End If

        If upperStatus = "UNPAID" OrElse upperStatus = "TO PAY" Then
            dgvRequests.Columns("colPay").Visible = True
            dgvRequests.Columns("colCancel").Visible = True
        Else
            dgvRequests.Columns("colPay").Visible = False
        End If

        If upperStatus = "TO RELEASE" OrElse upperStatus = "RELEASE" Then
            dgvRequests.Columns("colReleaseBtn").Visible = True
            dgvRequests.Columns("colMarkUnclaimed").Visible = True
        Else
            dgvRequests.Columns("colReleaseBtn").Visible = False
            dgvRequests.Columns("colMarkUnclaimed").Visible = False
        End If

        If upperStatus = "UNCLAIMED" Then
            dgvRequests.Columns("colUnclaimedActionBtn").Visible = True
        Else
            dgvRequests.Columns("colUnclaimedActionBtn").Visible = False
        End If

        If upperStatus = "PENDING" OrElse upperStatus = "APPROVE" OrElse upperStatus = "APPROVED" OrElse upperStatus = "PROCESSING" OrElse upperStatus = "UNPAID" OrElse upperStatus = "TO PAY" Then
            dgvRequests.Columns("colCancel").Visible = True
        Else
            If upperStatus <> "UNPAID" AndAlso upperStatus <> "TO PAY" Then dgvRequests.Columns("colCancel").Visible = False
        End If
    End Sub

    Private Sub dgvRequests_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRequests.CellContentClick
        If e.RowIndex < 0 Then Return
        Dim controlNo As String = dgvRequests.Rows(e.RowIndex).Cells("Control No.").Value.ToString()
        Dim currentStatus As String = dgvRequests.Rows(e.RowIndex).Cells("Status").Value.ToString().ToUpper()
        Dim colName As String = dgvRequests.Columns(e.ColumnIndex).Name

        If colName = "colView" AndAlso currentStatus = "PENDING" Then
            Dim frm As New frmCreateAppointment(controlNo, currentStatus)
            frm.TopLevel = False
            frm.FormBorderStyle = FormBorderStyle.None
            frm.Dock = DockStyle.Fill

            frmMain.Panel2.Controls.Clear()
            frmMain.Panel2.Controls.Add(frm)
            frm.Show()
        End If

        If colName = "colEdit" AndAlso (currentStatus = "APPROVE" OrElse currentStatus = "APPROVED" OrElse currentStatus = "PROCESSING") Then
            Dim frm As New frmCreateAppointment(controlNo)
            frm.TopLevel = False
            frm.FormBorderStyle = FormBorderStyle.None
            frm.Dock = DockStyle.Fill

            frmMain.Panel2.Controls.Clear()
            frmMain.Panel2.Controls.Add(frm)
            frm.Show()
        End If

        If colName = "colPay" AndAlso (currentStatus = "UNPAID" OrElse currentStatus = "TO PAY") Then
            Dim frm As New frmPayments(controlNo)
            frm.TopLevel = False
            frm.FormBorderStyle = FormBorderStyle.None
            frm.Dock = DockStyle.Fill

            frmMain.Panel2.Controls.Clear()
            frmMain.Panel2.Controls.Add(frm)
            frm.Show()
        End If

        If colName = "colReleaseBtn" AndAlso (currentStatus = "TO RELEASE" OrElse currentStatus = "RELEASE") Then
            If MsgBox($"Mark appointment [{controlNo}] as COMPLETE/RELEASED?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Release") = MsgBoxResult.Yes Then
                UpdateRequestStatus(controlNo, "COMPLETE")
            End If
        End If

        If colName = "colMarkUnclaimed" AndAlso (currentStatus = "TO RELEASE" OrElse currentStatus = "RELEASE") Then
            If MsgBox($"Mark appointment [{controlNo}] as UNCLAIMED?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Unclaimed") = MsgBoxResult.Yes Then
                UpdateRequestStatus(controlNo, "UNCLAIMED")
            End If
        End If

        If colName = "colUnclaimedActionBtn" AndAlso currentStatus = "UNCLAIMED" Then
            If MsgBox($"Mark unclaimed appointment [{controlNo}] as COMPLETE?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Release") = MsgBoxResult.Yes Then
                UpdateRequestStatus(controlNo, "COMPLETE")
            End If
        End If
    End Sub

    Private Sub dgvRequests_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvRequests.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = dgvRequests.Columns(e.ColumnIndex).Name
            Dim rowStatus As String = dgvRequests.Rows(e.RowIndex).Cells("Status").Value.ToString().ToUpper()

            If colName = "colView" OrElse colName = "colEdit" OrElse colName = "colCancel" OrElse colName = "colPay" OrElse colName = "colReleaseBtn" OrElse colName = "colMarkUnclaimed" OrElse colName = "colUnclaimedActionBtn" Then
                e.PaintBackground(e.CellBounds, True)
                Dim buttonRect As New Rectangle(e.CellBounds.X + 4, e.CellBounds.Y + 4, e.CellBounds.Width - 8, e.CellBounds.Height - 8)
                Dim cornerRadius As Integer = 8
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

                If colName = "colView" AndAlso rowStatus = "PENDING" Then
                    Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                        Using brush As New SolidBrush(Color.FromArgb(23, 162, 184))
                            e.Graphics.FillPath(brush, path)
                        End Using
                    End Using
                    TextRenderer.DrawText(e.Graphics, "VIEW", New Font("Segoe UI", 8.0F, FontStyle.Bold), buttonRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                    e.Handled = True
                End If

                If colName = "colEdit" AndAlso (rowStatus = "APPROVE" OrElse rowStatus = "APPROVED" OrElse rowStatus = "PROCESSING") Then
                    Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                        Using brush As New SolidBrush(Color.FromArgb(25, 42, 86))
                            e.Graphics.FillPath(brush, path)
                        End Using
                    End Using
                    TextRenderer.DrawText(e.Graphics, "EDIT", New Font("Segoe UI", 8.0F, FontStyle.Bold), buttonRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                    e.Handled = True
                End If

                If colName = "colPay" AndAlso (rowStatus = "UNPAID" OrElse rowStatus = "TO PAY") Then
                    Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                        Using brush As New SolidBrush(Color.FromArgb(40, 167, 69))
                            e.Graphics.FillPath(brush, path)
                        End Using
                    End Using
                    TextRenderer.DrawText(e.Graphics, "PAY", New Font("Segoe UI", 8.0F, FontStyle.Bold), buttonRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                    e.Handled = True
                End If

                If colName = "colReleaseBtn" AndAlso (rowStatus = "TO RELEASE" OrElse rowStatus = "RELEASE") Then
                    Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                        Using brush As New SolidBrush(Color.FromArgb(0, 123, 255))
                            e.Graphics.FillPath(brush, path)
                        End Using
                    End Using
                    TextRenderer.DrawText(e.Graphics, "RELEASE", New Font("Segoe UI", 8.0F, FontStyle.Bold), buttonRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                    e.Handled = True
                End If

                If colName = "colMarkUnclaimed" AndAlso (rowStatus = "TO RELEASE" OrElse rowStatus = "RELEASE") Then
                    Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                        Using brush As New SolidBrush(Color.FromArgb(220, 120, 0))
                            e.Graphics.FillPath(brush, path)
                        End Using
                    End Using
                    TextRenderer.DrawText(e.Graphics, "UNCLAIMED", New Font("Segoe UI", 7.5F, FontStyle.Bold), buttonRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                    e.Handled = True
                End If

                If colName = "colUnclaimedActionBtn" AndAlso rowStatus = "UNCLAIMED" Then
                    Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                        Using brush As New SolidBrush(Color.FromArgb(40, 167, 69))
                            e.Graphics.FillPath(brush, path)
                        End Using
                    End Using
                    TextRenderer.DrawText(e.Graphics, "COMPLETE", New Font("Segoe UI", 7.5F, FontStyle.Bold), buttonRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                    e.Handled = True
                End If

                If colName = "colCancel" AndAlso (rowStatus = "PENDING" OrElse rowStatus = "APPROVE" OrElse rowStatus = "APPROVED" OrElse rowStatus = "PROCESSING" OrElse rowStatus = "UNPAID" OrElse rowStatus = "TO PAY") Then
                    Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                        Using brush As New SolidBrush(Color.FromArgb(128, 0, 0))
                            e.Graphics.FillPath(brush, path)
                        End Using
                    End Using
                    TextRenderer.DrawText(e.Graphics, "CANCEL", New Font("Segoe UI", 8.0F, FontStyle.Bold), buttonRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
                    e.Handled = True
                End If
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
            Dim qry As String = "UPDATE appointments SET Status = @status, ScheduledDate = IFNULL(ScheduledDate, NOW()), AppointmentDate = IFNULL(AppointmentDate, NOW()), UpdatedAt = NOW() WHERE ControlNo = @ctrl"

            Using cmdObj As New MySqlCommand(qry, cn)
                cmdObj.Parameters.AddWithValue("@status", newStatus)
                cmdObj.Parameters.AddWithValue("@ctrl", controlNo)
                Dim rowsAffected As Integer = cmdObj.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MsgBox($"Request [{controlNo}] successfully updated to {newStatus}!", MsgBoxStyle.Information, "Success")
                    RefreshDashboardData()
                Else
                    MsgBox("Failed to update status. Record not found.", MsgBoxStyle.Exclamation, "Warning")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error updating status: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub dgvRequests_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRequests.CellDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = dgvRequests.Columns(e.ColumnIndex).Name
            If colName = "colView" OrElse colName = "colCancel" OrElse colName = "colEdit" OrElse colName = "colPay" OrElse colName = "colReleaseBtn" OrElse colName = "colMarkUnclaimed" OrElse colName = "colUnclaimedActionBtn" Then Return

            Dim controlNo As String = dgvRequests.Rows(e.RowIndex).Cells("Control No.").Value.ToString()
            Using frmDetails As New frmAppointmentDetails(controlNo)
                If frmDetails.ShowDialog() = DialogResult.OK Then
                    RefreshDashboardData()
                End If
            End Using
        End If
    End Sub

    Private Sub lblPending_Click(sender As Object, e As EventArgs) Handles lblPending.Click
        SetActiveLabel(lblPending)
        LoadUserRequests("PENDING")
    End Sub

    Private Sub lblApprove_Click(sender As Object, e As EventArgs) Handles lblApprove.Click
        SetActiveLabel(lblApprove)
        LoadUserRequests("APPROVE")
    End Sub

    Private Sub lblProcessing_Click(sender As Object, e As EventArgs) Handles lblProcessing.Click
        SetActiveLabel(lblProcessing)
        LoadUserRequests("PROCESSING")
    End Sub

    Private Sub lblTopay_Click(sender As Object, e As EventArgs) Handles lblTopay.Click
        SetActiveLabel(lblTopay)
        LoadUserRequests("UNPAID")
    End Sub

    Private Sub lblRelease_Click(sender As Object, e As EventArgs) Handles lblRelease.Click
        SetActiveLabel(lblRelease)
        LoadUserRequests("TO RELEASE")
    End Sub

    Private Sub lblUnclaimed_Click(sender As Object, e As EventArgs) Handles lblUnclaimed.Click
        SetActiveLabel(lblUnclaimed)
        LoadUserRequests("UNCLAIMED")
    End Sub

    Private Sub lblComplete_Click(sender As Object, e As EventArgs) Handles lblComplete.Click
        SetActiveLabel(lblComplete)
        LoadUserRequests("COMPLETE")
    End Sub

    Private Sub lblRejected_Click(sender As Object, e As EventArgs) Handles lblRejected.Click
        SetActiveLabel(lblRejected)
        LoadUserRequests("REJECTED")
    End Sub

    Private Sub lblCancelled_Click(sender As Object, e As EventArgs) Handles lblCancelled.Click
        SetActiveLabel(lblCancelled)
        LoadUserRequests("CANCELLED")
    End Sub

    Private Sub SetActiveLabel(targetLabel As Label)
        Dim allFilterLabels As Label() = {lblPending, lblApprove, lblProcessing, lblTopay, lblRelease, lblUnclaimed, lblComplete, lblRejected, lblCancelled}
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

    Private Sub btnRef_Click(sender As Object, e As EventArgs) Handles btnRef.Click
        RefreshDashboardData()
    End Sub

    Private Sub btnCreateRequest_Click(sender As Object, e As EventArgs) Handles btnCreateRequest.Click
        Dim frm As New frmCreateAppointment()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill

        frmMain.Panel2.Controls.Clear()
        frmMain.Panel2.Controls.Add(frm)
        frm.Show()
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
End Class