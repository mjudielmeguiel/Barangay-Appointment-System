Imports MySql.Data.MySqlClient

Public Class frmComplaintDetails

    Private ReadOnly Navy As Color = Color.FromArgb(25, 42, 86)
    Private ReadOnly NavyHover As Color = Color.FromArgb(45, 68, 128)
    Private ReadOnly LightBlue As Color = Color.FromArgb(210, 220, 245)
    Private ReadOnly SoftGray As Color = Color.FromArgb(247, 249, 252)

    Private complaintID As Integer
    Private respondMode As Boolean
    Private selectedStatus As String = "Pending"
    Private currentControlNo As String = ""
    Private currentTitle As String = ""

    Public Sub New(id As Integer, Optional openInRespondMode As Boolean = False)
        InitializeComponent()
        complaintID = id
        respondMode = openInRespondMode
    End Sub

    Private Sub frmComplaintDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyTheme()
        LoadComplaintDetails()
        If respondMode Then txtStaffRemarks.Focus()
    End Sub

    ' ===== Corporate theme =====

    Private Sub ApplyTheme()
        Me.Font = New Font("Segoe UI", 9)
        Me.BackColor = Color.White
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "Complaint Details"

        lblTitle.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTitle.ForeColor = Navy

        rtbDescription.ReadOnly = True
        rtbDescription.BorderStyle = BorderStyle.None
        rtbDescription.BackColor = SoftGray
        rtbDescription.Font = New Font("Segoe UI", 9.5)

        txtStaffRemarks.Font = New Font("Segoe UI", 9.5)
        txtStaffRemarks.Multiline = True
        txtStaffRemarks.ScrollBars = ScrollBars.Vertical

        picEvidence.SizeMode = PictureBoxSizeMode.Zoom
        picEvidence.BackColor = SoftGray

        StyleButton(btnApprove, True)
        StyleButton(btnBack, False)
    End Sub

    Private Sub StyleButton(btn As Button, primary As Boolean)
        btn.FlatStyle = FlatStyle.Flat
        btn.Cursor = Cursors.Hand
        btn.Font = New Font("Segoe UI Semibold", 9.5)
        btn.Height = 38
        If primary Then
            btn.BackColor = Navy
            btn.ForeColor = Color.White
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = NavyHover
            btn.FlatAppearance.MouseDownBackColor = Navy
        Else
            btn.BackColor = Color.White
            btn.ForeColor = Navy
            btn.FlatAppearance.BorderSize = 1
            btn.FlatAppearance.BorderColor = Navy
            btn.FlatAppearance.MouseOverBackColor = LightBlue
            btn.FlatAppearance.MouseDownBackColor = LightBlue
        End If
    End Sub

    ' ===== Load =====

    Private Sub LoadComplaintDetails()
        Try
            connection()

            Dim sql As String =
                "SELECT c.ControlNo, c.ComplaintType, c.Title, c.DateSubmitted, c.DateUpdated, " &
                "c.Status, c.IsAnonymous, c.LocationDetails, c.Latitude, c.Longitude, " &
                "c.Description, c.PhotoPath, c.StaffRemarks, " &
                "r.FullName, r.Email, r.MobileNumber " &
                "FROM complaints c " &
                "LEFT JOIN residences r ON c.ResidentID = r.ResidentID " &
                "WHERE c.ComplaintID = @id"

            Using cmdDet As New MySqlCommand(sql, cn)
                cmdDet.Parameters.AddWithValue("@id", complaintID)

                Using rd As MySqlDataReader = cmdDet.ExecuteReader()
                    If rd.Read() Then
                        currentControlNo = rd("ControlNo").ToString()
                        currentTitle = rd("Title").ToString()

                        lblControlNo.Text = currentControlNo
                        lblType.Text = rd("ComplaintType").ToString()
                        lblTitle.Text = currentTitle
                        lblDateSubmitted.Text = Convert.ToDateTime(rd("DateSubmitted")).ToString("F")

                        Dim currentStatus As String = rd("Status").ToString().Trim()
                        lblStatus.Text = currentStatus
                        selectedStatus = currentStatus

                        lblLastUpdated.Text = If(IsDBNull(rd("DateUpdated")), "Not yet updated",
                                                 Convert.ToDateTime(rd("DateUpdated")).ToString("F"))

                        ' Hide identity if anonymous
                        If Convert.ToBoolean(rd("IsAnonymous")) Then
                            lblResidentName.Text = "Anonymous / Lihim"
                            lblContactInfo.Text = "Nakatago"
                        Else
                            lblResidentName.Text = If(IsDBNull(rd("FullName")), "-", rd("FullName").ToString())
                            lblContactInfo.Text = "Email: " & If(IsDBNull(rd("Email")), "-", rd("Email").ToString()) &
                                                  "  |  Contact: " & If(IsDBNull(rd("MobileNumber")), "-", rd("MobileNumber").ToString())
                        End If

                        lblLocation.Text = If(IsDBNull(rd("LocationDetails")), "-", rd("LocationDetails").ToString())

                        If Not IsDBNull(rd("Latitude")) AndAlso Not IsDBNull(rd("Longitude")) Then
                            lblCoordinates.Text = $"{Convert.ToDecimal(rd("Latitude")):0.000000}, {Convert.ToDecimal(rd("Longitude")):0.000000}"
                        Else
                            lblCoordinates.Text = "No coordinates"
                        End If

                        rtbDescription.Text = rd("Description").ToString()
                        txtStaffRemarks.Text = If(IsDBNull(rd("StaffRemarks")), "", rd("StaffRemarks").ToString())

                        ' If already Resolved, disable button and remarks
                        If currentStatus.Equals("Resolved", StringComparison.OrdinalIgnoreCase) Then
                            btnApprove.Enabled = False
                            btnApprove.BackColor = Color.LightGray
                            txtStaffRemarks.ReadOnly = True
                        End If

                        LoadPhoto(rd("PhotoPath"))
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error loading details: " & ex.Message, MsgBoxStyle.Critical, "Complaint Details")
        Finally
            CloseConnection()
        End Try
    End Sub

    ' PhotoPath is a longblob in your table; this handles bytes or a text file path
    Private Sub LoadPhoto(value As Object)
        picEvidence.Image = Nothing
        If value Is Nothing OrElse IsDBNull(value) Then Exit Sub

        Try
            If TypeOf value Is Byte() Then
                Dim bytes As Byte() = DirectCast(value, Byte())
                If bytes.Length = 0 Then Exit Sub
                Using ms As New IO.MemoryStream(bytes)
                    picEvidence.Image = Image.FromStream(ms)
                End Using
            ElseIf TypeOf value Is String Then
                Dim path As String = value.ToString()
                If IO.File.Exists(path) Then picEvidence.Image = Image.FromFile(path)
            End If
        Catch
            picEvidence.Image = Nothing
        End Try
    End Sub

    ' ===== Save response & Advance Status with Message Box =====
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        If String.IsNullOrWhiteSpace(txtStaffRemarks.Text) Then
            MsgBox("Please enter your remarks/response.", MsgBoxStyle.Exclamation, "Respond")
            txtStaffRemarks.Focus()
            Exit Sub
        End If

        ' Advance status when the button is clicked
        Dim nextStatus As String = selectedStatus
        If selectedStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase) Then
            nextStatus = "In Progress"
        ElseIf selectedStatus.Equals("In Progress", StringComparison.OrdinalIgnoreCase) Then
            nextStatus = "Resolved"
        End If

        ' Show confirmation message box including Control Number and Title
        Dim msg As String = $"Do you want to update the status of this complaint?" & vbCrLf & vbCrLf &
                            $"Control No: {currentControlNo}" & vbCrLf &
                            $"Title: {currentTitle}" & vbCrLf &
                            $"New Status: {nextStatus}"

        Dim result As DialogResult = MessageBox.Show(msg, "Confirm Response", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            selectedStatus = nextStatus
            lblStatus.Text = selectedStatus

            Try
                connection()

                Dim sql As String =
                    "UPDATE complaints SET Status = @status, StaffRemarks = @remarks, " &
                    "AssignedStaffID = @staff, DateUpdated = NOW() " &
                    "WHERE ComplaintID = @id"

                Using cmdSave As New MySqlCommand(sql, cn)
                    cmdSave.Parameters.AddWithValue("@status", selectedStatus)
                    cmdSave.Parameters.AddWithValue("@remarks", txtStaffRemarks.Text.Trim())
                    cmdSave.Parameters.AddWithValue("@staff", If(LoggedInUserID > 0, CObj(LoggedInUserID), DBNull.Value))
                    cmdSave.Parameters.AddWithValue("@id", complaintID)
                    cmdSave.ExecuteNonQuery()
                End Using

                MsgBox($"Response for Control No. {currentControlNo} has been saved successfully!", MsgBoxStyle.Information, "Success")
                ReturnToList()

            Catch ex As Exception
                MsgBox("Error saving response: " & ex.Message, MsgBoxStyle.Critical, "Respond")
            Finally
                CloseConnection()
            End Try
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ReturnToList()
    End Sub

    ' Helper to return frmComplaintList to Panel2
    Private Sub ReturnToList()
        Dim mainForm = TryCast(Application.OpenForms("frmMain"), frmMain)
        If mainForm IsNot Nothing Then
            mainForm.Panel2.Controls.Clear()

            Dim frmList As New frmComplaintList()
            frmList.TopLevel = False
            frmList.FormBorderStyle = FormBorderStyle.None
            frmList.Dock = DockStyle.Fill

            mainForm.Panel2.Controls.Add(frmList)
            frmList.Show()
        End If
    End Sub
End Class