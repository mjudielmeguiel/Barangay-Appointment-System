Imports MySql.Data.MySqlClient

Public Class frmCreateEvent

    Private SelectedStartTime As TimeSpan = New TimeSpan(9, 0, 0)  ' Default 9:00 AM
    Private SelectedEndTime As TimeSpan = New TimeSpan(9, 30, 0)   ' Default 9:30 AM

    Private Sub frmCreateEvent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Categories corresponding to calendar color legend
        cboCategory.Items.Clear()
        cboCategory.Items.AddRange({
            "Appointment Request",
            "Holiday",
            "Meeting",
            "Announcement",
            "Community Event"
        })
        cboCategory.SelectedIndex = 0

        PopulateTimeComboBoxes()
    End Sub

    Private Sub PopulateTimeComboBoxes()
        If cboStartTime Is Nothing OrElse cboEndTime Is Nothing Then Return

        cboStartTime.Items.Clear()
        cboEndTime.Items.Clear()

        ' Configure Scrollable Dropdown Properties
        cboStartTime.MaxDropDownItems = 8
        cboStartTime.IntegralHeight = False
        cboStartTime.DropDownHeight = 200

        cboEndTime.MaxDropDownItems = 8
        cboEndTime.IntegralHeight = False
        cboEndTime.DropDownHeight = 200

        ' Generate 24-hour day in 30-minute intervals
        Dim timeSlot As DateTime = DateTime.Today

        For i As Integer = 0 To 47
            Dim timeStr As String = timeSlot.ToString("h:mm tt")
            cboStartTime.Items.Add(timeStr)
            cboEndTime.Items.Add(timeStr)
            timeSlot = timeSlot.AddMinutes(30)
        Next

        ' Default selections
        cboStartTime.Text = "9:00 AM"
        cboEndTime.Text = "9:30 AM"
    End Sub

    ' --- COMBOBOX SELECTION CHANGE HANDLERS ---
    Private Sub cboStartTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStartTime.SelectedIndexChanged
        Dim parsedTime As DateTime
        If DateTime.TryParse(cboStartTime.Text, parsedTime) Then
            SelectedStartTime = parsedTime.TimeOfDay

            ' Automatically adjust end time to 30 minutes after start time
            Dim defaultEnd As DateTime = parsedTime.AddMinutes(30)
            cboEndTime.Text = defaultEnd.ToString("h:mm tt")
            SelectedEndTime = defaultEnd.TimeOfDay
        End If
    End Sub

    Private Sub cboEndTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEndTime.SelectedIndexChanged
        Dim parsedTime As DateTime
        If DateTime.TryParse(cboEndTime.Text, parsedTime) Then
            SelectedEndTime = parsedTime.TimeOfDay
        End If
    End Sub

    ' --- SAVE EVENT TO DATABASE ---
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtTitle.Text) Then
            MsgBox("Please enter an Event Title.", MsgBoxStyle.Exclamation, "Validation Error")
            txtTitle.Focus()
            Return
        End If

        ' Verify start time is before end time
        If SelectedStartTime >= SelectedEndTime Then
            MsgBox("End time must be later than start time.", MsgBoxStyle.Exclamation, "Validation Error")
            cboEndTime.Focus()
            Return
        End If

        Try
            connection()

            ' Saves selected Event Date (default value), Start Time & End Time to database
            sql = "INSERT INTO calendar_events (EventTitle, EventDescription, EventDate, StartTime, EndTime, Category, CreatedBy, CreatedAt) " &
                  "VALUES (@title, @desc, @date, @start, @end, @cat, @createdBy, NOW())"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim())
            cmd.Parameters.AddWithValue("@desc", If(String.IsNullOrWhiteSpace(txtDescription.Text), DBNull.Value, txtDescription.Text.Trim()))
            cmd.Parameters.AddWithValue("@date", dtpEventDate.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@start", SelectedStartTime.ToString())
            cmd.Parameters.AddWithValue("@end", SelectedEndTime.ToString())
            cmd.Parameters.AddWithValue("@cat", cboCategory.Text.Trim())
            cmd.Parameters.AddWithValue("@createdBy", LoggedFullname)

            Dim rows As Integer = cmd.ExecuteNonQuery()
            If rows > 0 Then
                MsgBox("Barangay Event created successfully!", MsgBoxStyle.Information, "Success")
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MsgBox("Failed to record event.", MsgBoxStyle.Exclamation, "Warning")
            End If

        Catch ex As Exception
            MsgBox("Error creating event: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class