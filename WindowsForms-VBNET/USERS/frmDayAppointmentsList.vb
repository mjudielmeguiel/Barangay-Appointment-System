Imports MySql.Data.MySqlClient

Public Class frmDayAppointmentsList

    Private targetDate As DateTime

    Public Sub New(ByVal selectedDate As DateTime)
        InitializeComponent()
        targetDate = selectedDate
    End Sub

    Private Sub frmDayAppointmentsList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupListViews()
        LoadDailyAppointments()
        LoadDailyEvents()
    End Sub

    ' --- CONFIGURE BOTH LISTVIEWS ---
    Private Sub SetupListViews()
        ' 1. Appointments ListView Setup
        lvwAppointments.View = View.Details
        lvwAppointments.FullRowSelect = True
        lvwAppointments.GridLines = True
        lvwAppointments.Columns.Clear()

        lvwAppointments.Columns.Add("Control No.", 110)
        lvwAppointments.Columns.Add("Pick-up Time", 100)
        lvwAppointments.Columns.Add("Resident Name", 180)
        lvwAppointments.Columns.Add("Document / Service", 160)
        lvwAppointments.Columns.Add("Status", 90)

        ' 2. Events ListView Setup
        lvwEvents.View = View.Details
        lvwEvents.FullRowSelect = True
        lvwEvents.GridLines = True
        lvwEvents.Columns.Clear()

        lvwEvents.Columns.Add("Event Title", 180)
        lvwEvents.Columns.Add("Category", 120)
        lvwEvents.Columns.Add("Start Time", 100)
        lvwEvents.Columns.Add("End Time", 100)
        lvwEvents.Columns.Add("Description", 200)
    End Sub

    ' --- 1. LOAD APPOINTMENTS INTO lvwAppointments ---
    Private Sub LoadDailyAppointments()
        lvwAppointments.Items.Clear()

        Try
            connection()
            sql = "SELECT ControlNo, TIME_FORMAT(ScheduledDate, '%h:%i %p') AS STime, " &
                  "FullName, RequestType, Status " &
                  "FROM appointments " &
                  "WHERE DATE(ScheduledDate) = @targetDate AND UPPER(Status) != 'CANCELLED' " &
                  "ORDER BY ScheduledDate ASC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@targetDate", targetDate.ToString("yyyy-MM-dd"))
            dr = cmd.ExecuteReader()

            While dr.Read()
                Dim ctrlNo As String = dr("ControlNo").ToString()
                Dim schedTime As String = If(IsDBNull(dr("STime")), "-", dr("STime").ToString())
                Dim name As String = dr("FullName").ToString()
                Dim reqType As String = dr("RequestType").ToString()
                Dim status As String = dr("Status").ToString()

                Dim item As New ListViewItem(ctrlNo)
                item.SubItems.Add(schedTime)
                item.SubItems.Add(name)
                item.SubItems.Add(reqType)
                item.SubItems.Add(status)

                lvwAppointments.Items.Add(item)
            End While
            dr.Close()

            If lblApptHeader IsNot Nothing Then
                lblApptHeader.Text = $"Pick-up Appointments ({lvwAppointments.Items.Count})"
            End If

        Catch ex As Exception
            MsgBox("Error loading daily appointments: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    ' --- 2. LOAD EVENTS WITH START & END TIME INTO lvwEvents ---
    Private Sub LoadDailyEvents()
        lvwEvents.Items.Clear()

        Try
            connection()
            sql = "SELECT EventTitle, Category, " &
                  "TIME_FORMAT(StartTime, '%h:%i %p') AS STime, " &
                  "TIME_FORMAT(EndTime, '%h:%i %p') AS ETime, " &
                  "EventDescription " &
                  "FROM calendar_events " &
                  "WHERE EventDate = @targetDate " &
                  "ORDER BY StartTime ASC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@targetDate", targetDate.ToString("yyyy-MM-dd"))
            dr = cmd.ExecuteReader()

            While dr.Read()
                Dim title As String = dr("EventTitle").ToString()
                Dim cat As String = dr("Category").ToString()
                Dim sTime As String = If(IsDBNull(dr("STime")) OrElse String.IsNullOrWhiteSpace(dr("STime").ToString()), "N/A", dr("STime").ToString())
                Dim eTime As String = If(IsDBNull(dr("ETime")) OrElse String.IsNullOrWhiteSpace(dr("ETime").ToString()), "N/A", dr("ETime").ToString())
                Dim desc As String = If(IsDBNull(dr("EventDescription")), "-", dr("EventDescription").ToString())

                Dim item As New ListViewItem(title)
                item.SubItems.Add(cat)
                item.SubItems.Add(sTime)
                item.SubItems.Add(eTime)
                item.SubItems.Add(desc)

                lvwEvents.Items.Add(item)
            End While
            dr.Close()

            If lblEventsHeader IsNot Nothing Then
                lblEventsHeader.Text = $"Barangay Events ({lvwEvents.Items.Count})"
            End If

        Catch ex As Exception
            MsgBox("Error loading daily events: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    ' --- DOUBLE CLICK APPOINTMENT TO VIEW FULL DETAILS ---
    Private Sub lvwAppointments_DoubleClick(sender As Object, e As EventArgs) Handles lvwAppointments.DoubleClick
        If lvwAppointments.SelectedItems.Count > 0 Then
            Dim selectedControlNo As String = lvwAppointments.SelectedItems(0).Text
            Using frmDetails As New frmAppointmentDetails(selectedControlNo)
                If frmDetails.ShowDialog() = DialogResult.OK Then
                    LoadDailyAppointments()
                End If
            End Using
        End If
    End Sub

    Private Sub btnNewAppointment_Click(sender As Object, e As EventArgs) Handles btnNewAppointment.Click
        Using frmCreate As New frmCreateAppointment()
            If frmCreate.ShowDialog() = DialogResult.OK Then
                LoadDailyAppointments()
            End If
        End Using
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class