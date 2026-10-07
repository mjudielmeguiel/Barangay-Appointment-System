Imports MySql.Data.MySqlClient

Public Class frmBarangayCalendar

    Private currentDisplayDate As DateTime = DateTime.Today

    Private Sub frmBarangayCalendar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RightToLeft = RightToLeft.No
        flpCalendarGrid.RightToLeft = RightToLeft.No
        flpCalendarGrid.WrapContents = True
        flpCalendarGrid.AutoScroll = False
        flpCalendarGrid.BackColor = Color.FromArgb(245, 245, 245)

        DisplayTeamsCalendar()
    End Sub

    Private Sub flpCalendarGrid_Resize(sender As Object, e As EventArgs) Handles flpCalendarGrid.Resize
        DisplayTeamsCalendar()
    End Sub

    ' --- ADD EVENT BUTTON ---
    Private Sub btnAddEvent_Click(sender As Object, e As EventArgs) Handles btnAddEvent.Click
        Using frmCreate As New frmCreateEvent()
            If frmCreate.ShowDialog() = DialogResult.OK Then
                DisplayTeamsCalendar()
            End If
        End Using
    End Sub

    Private Sub btnPrevMonth_Click(sender As Object, e As EventArgs) Handles btnPrevMonth.Click
        currentDisplayDate = currentDisplayDate.AddMonths(-1)
        DisplayTeamsCalendar()
    End Sub

    Private Sub btnNextMonth_Click(sender As Object, e As EventArgs) Handles btnNextMonth.Click
        currentDisplayDate = currentDisplayDate.AddMonths(1)
        DisplayTeamsCalendar()
    End Sub

    Private Sub DisplayTeamsCalendar()
        flpCalendarGrid.Controls.Clear()

        If lblCurrentMonthYear IsNot Nothing Then
            lblCurrentMonthYear.Text = currentDisplayDate.ToString("MMMM yyyy")
        End If

        Dim firstDayOfMonth As New DateTime(currentDisplayDate.Year, currentDisplayDate.Month, 1)
        Dim daysInMonth As Integer = DateTime.DaysInMonth(currentDisplayDate.Year, currentDisplayDate.Month)
        Dim dayOfWeekOffset As Integer = CInt(firstDayOfMonth.DayOfWeek)

        Dim totalCellsNeeded As Integer = dayOfWeekOffset + daysInMonth
        Dim totalRows As Integer = If(totalCellsNeeded > 35, 6, 5)
        Dim totalCellsInGrid As Integer = totalRows * 7

        Dim headerHeight As Integer = 30
        Dim gridWidth As Double = flpCalendarGrid.ClientSize.Width
        Dim gridHeight As Double = flpCalendarGrid.ClientSize.Height - headerHeight

        Dim exactCellWidth As Integer = CInt(Math.Floor(gridWidth / 7.0))
        Dim exactCellHeight As Integer = CInt(Math.Floor(gridHeight / CDbl(totalRows)))

        If exactCellWidth <= 0 OrElse exactCellHeight <= 0 Then Return

        ' 1. Day of Week Headers
        Dim days As String() = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"}
        For i As Integer = 0 To 6
            Dim lblHeader As New Label With {
                .Text = days(i),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
                .TextAlign = ContentAlignment.MiddleLeft,
                .Size = New Size(exactCellWidth - 1, headerHeight),
                .Margin = New Padding(0),
                .ForeColor = Color.FromArgb(90, 90, 90),
                .Padding = New Padding(8, 0, 0, 0),
                .BackColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle,
                .RightToLeft = RightToLeft.No
            }
            flpCalendarGrid.Controls.Add(lblHeader)
        Next

        ' Kunin ang buong mapa ng appointments at events para sa buong database
        Dim appointmentCounts As Dictionary(Of DateTime, Integer) = GetAllAppointmentsMap()
        Dim eventDataList As Dictionary(Of DateTime, List(Of (Title As String, Category As String))) = GetAllEventsMap()

        ' 2. Previous Month Padding Days
        Dim prevMonthDate As DateTime = currentDisplayDate.AddMonths(-1)
        Dim daysInPrevMonth As Integer = DateTime.DaysInMonth(prevMonthDate.Year, prevMonthDate.Month)

        For i As Integer = (dayOfWeekOffset - 1) To 0 Step -1
            Dim prevDayNum As Integer = daysInPrevMonth - i
            Dim dateOfCell As New DateTime(prevMonthDate.Year, prevMonthDate.Month, prevDayNum)

            Dim apptCount As Integer = If(appointmentCounts.ContainsKey(dateOfCell.Date), appointmentCounts(dateOfCell.Date), 0)
            Dim dayEvents As List(Of (Title As String, Category As String)) = If(eventDataList.ContainsKey(dateOfCell.Date), eventDataList(dateOfCell.Date), New List(Of (Title As String, Category As String))())

            flpCalendarGrid.Controls.Add(CreateDayCell(dateOfCell, True, exactCellWidth, exactCellHeight, apptCount, dayEvents))
        Next

        ' 3. Current Month Active Days
        For day As Integer = 1 To daysInMonth
            Dim dateOfCell As New DateTime(currentDisplayDate.Year, currentDisplayDate.Month, day)
            Dim apptCount As Integer = If(appointmentCounts.ContainsKey(dateOfCell.Date), appointmentCounts(dateOfCell.Date), 0)
            Dim dayEvents As List(Of (Title As String, Category As String)) = If(eventDataList.ContainsKey(dateOfCell.Date), eventDataList(dateOfCell.Date), New List(Of (Title As String, Category As String))())

            flpCalendarGrid.Controls.Add(CreateDayCell(dateOfCell, False, exactCellWidth, exactCellHeight, apptCount, dayEvents))
        Next

        ' 4. Next Month Padding Days
        Dim remainingCells As Integer = totalCellsInGrid - totalCellsNeeded
        Dim nextMonthDate As DateTime = currentDisplayDate.AddMonths(1)

        For day As Integer = 1 To remainingCells
            Dim dateOfCell As New DateTime(nextMonthDate.Year, nextMonthDate.Month, day)

            Dim apptCount As Integer = If(appointmentCounts.ContainsKey(dateOfCell.Date), appointmentCounts(dateOfCell.Date), 0)
            Dim dayEvents As List(Of (Title As String, Category As String)) = If(eventDataList.ContainsKey(dateOfCell.Date), eventDataList(dateOfCell.Date), New List(Of (Title As String, Category As String))())

            flpCalendarGrid.Controls.Add(CreateDayCell(dateOfCell, True, exactCellWidth, exactCellHeight, apptCount, dayEvents))
        Next
    End Sub

    Private Function CreateDayCell(cellDate As DateTime, isOutsideMonth As Boolean, width As Integer, height As Integer, Optional apptCount As Integer = 0, Optional dayEvents As List(Of (Title As String, Category As String)) = Nothing) As Panel
        Dim isWeekend As Boolean = (cellDate.DayOfWeek = DayOfWeek.Saturday OrElse cellDate.DayOfWeek = DayOfWeek.Sunday)
        Dim isToday As Boolean = (cellDate.Date = DateTime.Today)

        Dim pnlDay As New Panel With {
            .Size = New Size(width - 1, height - 1),
            .BorderStyle = BorderStyle.FixedSingle,
            .Margin = New Padding(0),
            .Tag = cellDate,
            .Cursor = Cursors.Default,
            .RightToLeft = RightToLeft.No,
            .BackColor = If(isToday, Color.FromArgb(243, 242, 250), If(isWeekend, Color.FromArgb(250, 250, 252), Color.White))
        }

        Dim dayText As String = cellDate.Day.ToString()
        If cellDate.Day = 1 Then
            dayText = cellDate.ToString("MMM d")
        End If

        Dim lblDayNum As New Label With {
            .Text = dayText,
            .Font = New Font("Segoe UI", 9.0F, If(isToday, FontStyle.Bold, FontStyle.Regular)),
            .Dock = DockStyle.Top,
            .Height = 22,
            .Padding = New Padding(6, 2, 0, 0),
            .RightToLeft = RightToLeft.No,
            .Cursor = Cursors.Default,
            .ForeColor = If(isOutsideMonth, Color.FromArgb(160, 160, 160), If(isWeekend, Color.FromArgb(100, 100, 120), Color.FromArgb(30, 30, 30)))
        }

        If isToday Then
            lblDayNum.ForeColor = Color.FromArgb(70, 78, 184)
        End If

        ' --- 1. APPOINTMENT BADGE (BLUE) ---
        If apptCount > 0 Then
            Dim apptColors = GetCategoryColors("Appointment Request")

            Dim pnlApptBadge As New Panel With {
                .Height = 24,
                .Dock = DockStyle.Top,
                .BackColor = apptColors.BgColor,
                .Margin = New Padding(2),
                .Padding = New Padding(4, 2, 4, 2),
                .Cursor = Cursors.Default
            }

            Dim lblAppt As New Label With {
                .Text = $"{apptCount} Appointment{(If(apptCount > 1, "s", ""))}",
                .Font = New Font("Segoe UI", 7.5F, FontStyle.Bold),
                .ForeColor = apptColors.FgColor,
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Cursor = Cursors.Default
            }

            pnlApptBadge.Controls.Add(lblAppt)
            pnlDay.Controls.Add(pnlApptBadge)
        End If

        ' --- 2. MULTIPLE EVENT BADGES ---
        If dayEvents IsNot Nothing AndAlso dayEvents.Count > 0 Then
            For Each ev In dayEvents
                Dim catColors = GetCategoryColors(ev.Category)

                Dim pnlEventBadge As New Panel With {
                    .Height = 24,
                    .Dock = DockStyle.Top,
                    .BackColor = catColors.BgColor,
                    .Margin = New Padding(2),
                    .Padding = New Padding(4, 2, 4, 2),
                    .Cursor = Cursors.Default
                }

                Dim lblEvent As New Label With {
                    .Text = $"📢 {ev.Title}",
                    .Font = New Font("Segoe UI", 7.5F, FontStyle.Bold),
                    .ForeColor = catColors.FgColor,
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleLeft,
                    .Cursor = Cursors.Default
                }

                pnlEventBadge.Controls.Add(lblEvent)
                pnlDay.Controls.Add(pnlEventBadge)
            Next
        End If

        pnlDay.Controls.Add(lblDayNum)

        Return pnlDay
    End Function

    Private Function GetCategoryColors(category As String) As (BgColor As Color, FgColor As Color, BarColor As Color)
        Select Case category.Trim()
            Case "Appointment Request"
                Return (Color.FromArgb(232, 235, 250), Color.FromArgb(70, 78, 184), Color.FromArgb(70, 78, 184))
            Case "Holiday"
                Return (Color.FromArgb(254, 237, 222), Color.FromArgb(180, 80, 0), Color.FromArgb(180, 80, 0))
            Case "Meeting"
                Return (Color.FromArgb(243, 229, 245), Color.FromArgb(123, 31, 162), Color.FromArgb(123, 31, 162))
            Case "Announcement"
                Return (Color.FromArgb(255, 235, 238), Color.FromArgb(198, 40, 40), Color.FromArgb(198, 40, 40))
            Case "Community Event"
                Return (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50), Color.FromArgb(46, 125, 50))
            Case Else
                Return (Color.FromArgb(240, 240, 240), Color.FromArgb(60, 60, 60), Color.FromArgb(100, 100, 100))
        End Select
    End Function

    Private Function GetAllAppointmentsMap() As Dictionary(Of DateTime, Integer)
        Dim countMap As New Dictionary(Of DateTime, Integer)()

        Try
            connection()
            Dim query As String = "SELECT ScheduledDate, COUNT(*) AS TotalAppts " &
                                  "FROM appointments " &
                                  "WHERE UPPER(Status) != 'CANCELLED' " &
                                  "GROUP BY DATE(ScheduledDate)"

            Using localCmd As New MySqlCommand(query, cn)
                Using localDr As MySqlDataReader = localCmd.ExecuteReader()
                    While localDr.Read()
                        If Not IsDBNull(localDr("ScheduledDate")) Then
                            Dim schedDate As DateTime = Convert.ToDateTime(localDr("ScheduledDate")).Date
                            Dim total As Integer = Convert.ToInt32(localDr("TotalAppts"))
                            countMap(schedDate) = total
                        End If
                    End While
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error loading appointments: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try

        Return countMap
    End Function

    Private Function GetAllEventsMap() As Dictionary(Of DateTime, List(Of (Title As String, Category As String)))
        Dim dataMap As New Dictionary(Of DateTime, List(Of (Title As String, Category As String)))()

        Try
            connection()
            Dim query As String = "SELECT EventDate, EventTitle, Category " &
                                  "FROM calendar_events " &
                                  "ORDER BY EventID ASC"

            Using localCmd As New MySqlCommand(query, cn)
                Using localDr As MySqlDataReader = localCmd.ExecuteReader()
                    While localDr.Read()
                        If Not IsDBNull(localDr("EventDate")) Then
                            Dim evDate As DateTime = Convert.ToDateTime(localDr("EventDate")).Date
                            Dim title As String = localDr("EventTitle").ToString()
                            Dim category As String = If(IsDBNull(localDr("Category")), "Community Event", localDr("Category").ToString())

                            If Not dataMap.ContainsKey(evDate) Then
                                dataMap(evDate) = New List(Of (Title As String, Category As String))()
                            End If

                            dataMap(evDate).Add((title, category))
                        End If
                    End While
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error loading events: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try

        Return dataMap
    End Function

End Class