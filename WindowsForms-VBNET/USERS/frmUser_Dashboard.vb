Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class frmUser_Dashboard
    Private activeFilterLabel As Label = Nothing

    Private Sub frmUser_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StyleDataGridView(dgvRequests)
        ApplyCorporateButtonStyles()
        SetActiveLabel(lblPending)
        RefreshDashboardData()
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
        LoadStatusCounts()
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

    Private Sub AutoRejectExpiredAppointments()
        Try
            If cn.State <> ConnectionState.Open Then connection()
            sql = "UPDATE appointments SET Status = 'REJECTED', UpdatedAt = NOW() " &
                  "WHERE UPPER(Status) = 'PENDING' AND DateSubmitted < NOW() - INTERVAL 24 HOUR"
            cmd = New MySqlCommand(sql, cn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadStatusCounts()
        Dim pendingCount As Integer = 0
        Dim approvedCount As Integer = 0
        Dim rejectedCount As Integer = 0
        Dim completedCount As Integer = 0
        Dim cancelledCount As Integer = 0
        Try
            If cn.State <> ConnectionState.Open Then connection()
            sql = "SELECT UPPER(Status) AS StatusName, COUNT(*) AS Total " &
                  "FROM appointments " &
                  "WHERE DATE(DateSubmitted) = CURDATE() " &
                  "GROUP BY UPPER(Status)"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                Dim st As String = dr("StatusName").ToString().Trim()
                Dim count As Integer = Convert.ToInt32(dr("Total"))
                Select Case st
                    Case "PENDING" : pendingCount = count
                    Case "APPROVED" : approvedCount = count
                    Case "REJECTED" : rejectedCount = count
                    Case "COMPLETED" : completedCount = count
                    Case "CANCELLED" : cancelledCount = count
                End Select
            End While
            dr.Close()
            lblPending.Text = pendingCount.ToString()
            lblApproved.Text = approvedCount.ToString()
            lblRejected.Text = rejectedCount.ToString()
            lblCompleted.Text = completedCount.ToString()
            lblCancelled.Text = cancelledCount.ToString()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    ' === ✅ ISANG BUTTON NA LANG — CANCEL ===
    Private Sub AddGridActionButtons()
        If Not dgvRequests.Columns.Contains("colCancel") Then
            Dim btnCancelCol As New DataGridViewButtonColumn()
            btnCancelCol.Name = "colCancel"
            btnCancelCol.HeaderText = "Action"
            btnCancelCol.FlatStyle = FlatStyle.Flat
            btnCancelCol.Width = 120
            dgvRequests.Columns.Add(btnCancelCol)
        End If
    End Sub

    ' === ✅ Itago ang Cancel button kapag hindi APPROVED ===
    Private Sub AdjustGridColumnsByStatus(currentStatus As String)
        If Not dgvRequests.Columns.Contains("colCancel") Then Return
        If currentStatus.ToUpper() = "APPROVED" Then
            dgvRequests.Columns("colCancel").Visible = True
            dgvRequests.Columns("colCancel").HeaderText = "Action"
        Else
            dgvRequests.Columns("colCancel").Visible = False
        End If
    End Sub

    ' === ✅ CANCEL BUTTON CLICK — cancel lang ang ginagawa ===
    Private Sub dgvRequests_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRequests.CellContentClick
        If e.RowIndex < 0 Then Return
        Dim controlNo As String = dgvRequests.Rows(e.RowIndex).Cells("Control No.").Value.ToString()
        Dim currentStatus As String = dgvRequests.Rows(e.RowIndex).Cells("Status").Value.ToString().ToUpper()
        Dim colName As String = dgvRequests.Columns(e.ColumnIndex).Name

        If colName = "colCancel" AndAlso currentStatus = "APPROVED" Then
            If MsgBox($"Are you sure you want to CANCEL appointment [{controlNo}]?",
                     MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Confirm Cancellation") = MsgBoxResult.Yes Then
                UpdateRequestStatus(controlNo, "CANCELLED")
            End If
        End If
    End Sub

    ' === ✅ CELL PAINTING — CANCEL button lang ang iginuguhit ===
    Private Sub dgvRequests_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvRequests.CellPainting
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = dgvRequests.Columns(e.ColumnIndex).Name
            If colName = "colCancel" Then
                e.PaintBackground(e.CellBounds, True)
                Dim rowStatus As String = dgvRequests.Rows(e.RowIndex).Cells("Status").Value.ToString().ToUpper()

                If rowStatus = "APPROVED" Then
                    Dim buttonRect As New Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + 4,
                                                    e.CellBounds.Width - 12, e.CellBounds.Height - 8)
                    Dim cornerRadius As Integer = 8
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                    Using path As GraphicsPath = GetRoundedPath(buttonRect, cornerRadius)
                        Using brush As New SolidBrush(Color.FromArgb(220, 53, 69))
                            e.Graphics.FillPath(brush, path)
                        End Using
                    End Using
                    TextRenderer.DrawText(e.Graphics, "CANCEL",
                                         New Font("Segoe UI", 8.0F, FontStyle.Bold),
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

    Private Sub dgvRequests_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRequests.CellDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim colName As String = dgvRequests.Columns(e.ColumnIndex).Name
            If colName = "colCancel" Then Return
            Dim controlNo As String = dgvRequests.Rows(e.RowIndex).Cells("Control No.").Value.ToString()
            Using frmDetails As New frmAppointmentDetails(controlNo)
                If frmDetails.ShowDialog() = DialogResult.OK Then
                    RefreshDashboardData()
                End If
            End Using
        End If
    End Sub

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

    Private Sub LoadUserRequests(Optional statusFilter As String = "")
        Try
            connection()
            Dim sqlBase As String = "SELECT ControlNo AS `Control No.`, RequestType AS `Request Type`, Purpose, " &
                  "Department, DateSubmitted AS `Date Submitted`, PaymentStatus AS `Payment Status`, Status " &
                  "FROM appointments " &
                  "WHERE UPPER(PaymentStatus) NOT IN ('PAID', 'WAIVED') "
            If Not String.IsNullOrEmpty(statusFilter) Then
                sqlBase &= "AND UPPER(Status) = @status "
            End If
            sqlBase &= "ORDER BY AppointmentID DESC"
            cmd = New MySqlCommand(sqlBase, cn)
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

    Private Sub btnRef_Click(sender As Object, e As EventArgs) Handles btnRef.Click
        RefreshDashboardData()
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