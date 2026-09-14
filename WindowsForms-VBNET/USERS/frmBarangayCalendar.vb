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

        ' 2. Previous Month Padding Days
        Dim prevMonthDate As DateTime = currentDisplayDate.AddMonths(-1)
        Dim daysInPrevMonth As Integer = DateTime.DaysInMonth(prevMonthDate.Year, prevMonthDate.Month)

        For i As Integer = (dayOfWeekOffset - 1) To 0 Step -1
            Dim prevDayNum As Integer = daysInPrevMonth - i
            Dim dateOfCell As New DateTime(prevMonthDate.Year, prevMonthDate.Month, prevDayNum)
            flpCalendarGrid.Controls.Add(CreateDayCell(dateOfCell, True, exactCellWidth, exactCellHeight))
        Next

        ' 3. Current Month Active Days
        Dim appointmentCounts As Dictionary(Of Integer, Integer) = GetMonthlyAppointmentCounts(currentDisplayDate.Year, currentDisplayDate.Month)
        Dim eventCounts As Dictionary(Of Integer, Integer) = GetMonthlyEventCounts(currentDisplayDate.Year, currentDisplayDate.Month)
        Dim eventData As Dictionary(Of Integer, (Title As String, Category As String)) = GetMonthlyEventData(currentDisplayDate.Year, currentDisplayDate.Month)

        For day As Integer = 1 To daysInMonth
            Dim dateOfCell As New DateTime(currentDisplayDate.Year, currentDisplayDate.Month, day)
            Dim apptCount As Integer = If(appointmentCounts.ContainsKey(day), appointmentCounts(day), 0)
            Dim evCount As Integer = If(eventCounts.ContainsKey(day), eventCounts(day), 0)
            Dim evTitle As String = If(eventData.ContainsKey(day), eventData(day).Title, "")
            Dim evCategory As String = If(eventData.ContainsKey(day), eventData(day).Category, "Community Event")

            flpCalendarGrid.Controls.Add(CreateDayCell(dateOfCell, False, exactCellWidth, exactCellHeight, apptCount, evCount, evTitle, evCategory))
        Next

        ' 4. Next Month Padding Days
        Dim remainingCells As Integer = totalCellsInGrid - totalCellsNeeded
        Dim nextMonthDate As DateTime = currentDisplayDate.AddMonths(1)

        For day As Integer = 1 To remainingCells
            Dim dateOfCell As New DateTime(nextMonthDate.Year, nextMonthDate.Month, day)
            flpCalendarGrid.Controls.Add(CreateDayCell(dateOfCell, True, exactCellWidth, exactCellHeight))
        Next
    End Sub

    Private Function CreateDayCell(cellDate As DateTime, isOutsideMonth As Boolean, width As Integer, height As Integer, Optional apptCount As Integer = 0, Optional evCount As Integer = 0, Optional eventTitle As String = "", Optional eventCategory As String = "") As Panel
        Dim isWeekend As Boolean = (cellDate.DayOfWeek = DayOfWeek.Saturday OrElse cellDate.DayOfWeek = DayOfWeek.Sunday)
        Dim isToday As Boolean = (cellDate.Date = DateTime.Today)

        Dim pnlDay As New Panel With {
            .Size = New Size(width - 1, height - 1),
            .BorderStyle = BorderStyle.FixedSingle,
            .Margin = New Padding(0),
            .Tag = cellDate,
            .Cursor = Cursors.Hand,
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
            .Cursor = Cursors.Hand,
            .ForeColor = If(isOutsideMonth, Color.FromArgb(160, 160, 160), If(isWeekend, Color.FromArgb(100, 100, 120), Color.FromArgb(30, 30, 30)))
        }

        If isToday Then
            lblDayNum.ForeColor = Color.FromArgb(70, 78, 184)
        End If

        ' --- 1. APPOINTMENT BADGE (BLUE) ---
        If Not isOutsideMonth AndAlso apptCount > 0 Then
            Dim apptColors = GetCategoryColors("Appointment Request")

            Dim pnlApptBadge As New Panel With {
                .Height = 26,
                .Dock = DockStyle.Top,
                .BackColor = apptColors.BgColor,
                .Margin = New Padding(2),
                .Padding = New Padding(4, 2, 4, 2),
                .Cursor = Cursors.Hand
            }

            Dim lblAppt As New Label With {
                .Text = $"{apptCount} Appointment{(If(apptCount > 1, "s", ""))}",
                .Font = New Font("Segoe UI", 7.5F, FontStyle.Bold),
                .ForeColor = apptColors.FgColor,
                .Dock = DockStyle.Top,
                .Height = 15,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Cursor = Cursors.Hand
            }

            Dim apptMaxCapacity As Integer = 10
            Dim apptWidthPct As Double = Math.Min(1.0, apptCount / CDbl(apptMaxCapacity))

            Dim pnlApptBarBg As New Panel With {
                .Height = 4,
                .Dock = DockStyle.Bottom,
                .BackColor = Color.FromArgb(200, 205, 235)
            }

            Dim pnlApptBarFill As New Panel With {
                .Height = 4,
                .Dock = DockStyle.Left,
                .Width = CInt(Math.Max(5, (width - 16) * apptWidthPct)),
                .BackColor = apptColors.BarColor
            }

            pnlApptBarBg.Controls.Add(pnlApptBarFill)
            pnlApptBadge.Controls.Add(lblAppt)
            pnlApptBadge.Controls.Add(pnlApptBarBg)
            pnlDay.Controls.Add(pnlApptBadge)

            AddHandler pnlApptBadge.Click, Sub(s, ev) OpenDayDetailsForm(cellDate)
            AddHandler lblAppt.Click, Sub(s, ev) OpenDayDetailsForm(cellDate)
            AddHandler pnlApptBarBg.Click, Sub(s, ev) OpenDayDetailsForm(cellDate)
            AddHandler pnlApptBarFill.Click, Sub(s, ev) OpenDayDetailsForm(cellDate)
        End If

        ' --- 2. EVENT BADGE (DYNAMIC CATEGORY COLORS) ---
        If Not isOutsideMonth AndAlso (evCount > 0 OrElse Not String.IsNullOrEmpty(eventTitle)) Then
            Dim catColors = GetCategoryColors(eventCategory)

            Dim pnlEventBadge As New Panel With {
                .Height = 26,
                .Dock = DockStyle.Top,
                .BackColor = catColors.BgColor,
                .Margin = New Padding(2),
                .Padding = New Padding(4, 2, 4, 2),
                .Cursor = Cursors.Hand
            }

            Dim eventDisplayText As String = If(Not String.IsNullOrEmpty(eventTitle), $"📢 {eventTitle}", $"{evCount} Event{(If(evCount > 1, "s", ""))}")

            Dim lblEvent As New Label With {
                .Text = eventDisplayText,
                .Font = New Font("Segoe UI", 7.5F, FontStyle.Bold),
                .ForeColor = catColors.FgColor,
                .Dock = DockStyle.Top,
                .Height = 15,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Cursor = Cursors.Hand
            }

            Dim eventMaxCapacity As Integer = 5
            Dim eventCountForBar As Integer = If(evCount > 0, evCount, 1)
            Dim eventWidthPct As Double = Math.Min(1.0, eventCountForBar / CDbl(eventMaxCapacity))

            Dim pnlEventBarBg As New Panel With {
                .Height = 4,
                .Dock = DockStyle.Bottom,
                .BackColor = Color.FromArgb(230, 230, 230)
            }

            Dim pnlEventBarFill As New Panel With {
                .Height = 4,
                .Dock = DockStyle.Left,
                .Width = CInt(Math.Max(5, (width - 16) * eventWidthPct)),
                .BackColor = catColors.BarColor
            }

            pnlEventBarBg.Controls.Add(pnlEventBarFill)
            pnlEventBadge.Controls.Add(lblEvent)
            pnlEventBadge.Controls.Add(pnlEventBarBg)
            pnlDay.Controls.Add(pnlEventBadge)

            AddHandler pnlEventBadge.Click, Sub(s, ev) OpenDayDetailsForm(cellDate)
            AddHandler lblEvent.Click, Sub(s, ev) OpenDayDetailsForm(cellDate)
            AddHandler pnlEventBarBg.Click, Sub(s, ev) OpenDayDetailsForm(cellDate)
            AddHandler pnlEventBarFill.Click, Sub(s, ev) OpenDayDetailsForm(cellDate)
        End If

        ' Add Day Number LAST to remain anchored at top
        pnlDay.Controls.Add(lblDayNum)

        ' Cell-level Click Handler
        AddHandler pnlDay.Click, Sub(s, ev) OpenDayDetailsForm(cellDate)
        AddHandler lblDayNum.Click, Sub(s, ev) OpenDayDetailsForm(cellDate)

        Return pnlDay
    End Function

    Private Function GetCategoryColors(category As String) As (BgColor As Color, FgColor As Color, BarColor As Color)
        Select Case category.Trim()
            Case "Appointment Request"
                Return (Color.FromArgb(232, 235, 250), Color.FromArgb(70, 78, 184), Color.FromArgb(70, 78, 184)) ' Blue
            Case "Holiday"
                Return (Color.FromArgb(254, 237, 222), Color.FromArgb(180, 80, 0), Color.FromArgb(180, 80, 0)) ' Orange / Yellow
            Case "Meeting"
                Return (Color.FromArgb(243, 229, 245), Color.FromArgb(123, 31, 162), Color.FromArgb(123, 31, 162)) ' Purple
            Case "Announcement"
                Return (Color.FromArgb(255, 235, 238), Color.FromArgb(198, 40, 40), Color.FromArgb(198, 40, 40)) ' Red
            Case "Community Event"
                Return (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50), Color.FromArgb(46, 125, 50)) ' Green
            Case Else
                Return (Color.FromArgb(240, 240, 240), Color.FromArgb(60, 60, 60), Color.FromArgb(100, 100, 100))
        End Select
    End Function

    Private Function GetMonthlyAppointmentCounts(year As Integer, month As Integer) As Dictionary(Of Integer, Integer)
        Dim countMap As New Dictionary(Of Integer, Integer)()

        Try
            connection()
            sql = "SELECT DAY(ScheduledDate) AS SchedDay, COUNT(*) AS TotalAppts " &
                  "FROM appointments " &
                  "WHERE YEAR(ScheduledDate) = @yr AND MONTH(ScheduledDate) = @mo AND UPPER(Status) != 'CANCELLED' " &
                  "GROUP BY DAY(ScheduledDate)"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@yr", year)
            cmd.Parameters.AddWithValue("@mo", month)
            dr = cmd.ExecuteReader()

            While dr.Read()
                If Not IsDBNull(dr("SchedDay")) Then
                    Dim dayNum As Integer = Convert.ToInt32(dr("SchedDay"))
                    Dim total As Integer = Convert.ToInt32(dr("TotalAppts"))
                    countMap(dayNum) = total
                End If
            End While
            dr.Close()

        Catch ex As Exception
        Finally
            CloseConnection()
        End Try

        Return countMap
    End Function

    Private Function GetMonthlyEventCounts(year As Integer, month As Integer) As Dictionary(Of Integer, Integer)
        Dim countMap As New Dictionary(Of Integer, Integer)()

        Try
            connection()
            sql = "SELECT DAY(EventDate) AS EvDay, COUNT(*) AS TotalEvents " &
                  "FROM calendar_events " &
                  "WHERE YEAR(EventDate) = @yr AND MONTH(EventDate) = @mo " &
                  "GROUP BY DAY(EventDate)"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@yr", year)
            cmd.Parameters.AddWithValue("@mo", month)
            dr = cmd.ExecuteReader()

            While dr.Read()
                If Not IsDBNull(dr("EvDay")) Then
                    Dim dayNum As Integer = Convert.ToInt32(dr("EvDay"))
                    Dim total As Integer = Convert.ToInt32(dr("TotalEvents"))
                    countMap(dayNum) = total
                End If
            End While
            dr.Close()

        Catch ex As Exception
        Finally
            CloseConnection()
        End Try

        Return countMap
    End Function

    Private Function GetMonthlyEventData(year As Integer, month As Integer) As Dictionary(Of Integer, (Title As String, Category As String))
        Dim dataMap As New Dictionary(Of Integer, (Title As String, Category As String))()

        Try
            connection()
            sql = "SELECT DAY(EventDate) AS EvDay, EventTitle, Category " &
                  "FROM calendar_events " &
                  "WHERE YEAR(EventDate) = @yr AND MONTH(EventDate) = @mo " &
                  "ORDER BY EventID ASC"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@yr", year)
            cmd.Parameters.AddWithValue("@mo", month)
            dr = cmd.ExecuteReader()

            While dr.Read()
                If Not IsDBNull(dr("EvDay")) Then
                    Dim dayNum As Integer = Convert.ToInt32(dr("EvDay"))
                    Dim title As String = dr("EventTitle").ToString()
                    Dim category As String = If(IsDBNull(dr("Category")), "Community Event", dr("Category").ToString())

                    If Not dataMap.ContainsKey(dayNum) Then
                        dataMap(dayNum) = (title, category)
                    End If
                End If
            End While
            dr.Close()

        Catch ex As Exception
        Finally
            CloseConnection()
        End Try

        Return dataMap
    End Function

    Private Sub OpenDayDetailsForm(selectedDate As DateTime)
        Using frmList As New frmDayAppointmentsList(selectedDate)
            frmList.ShowDialog()
            DisplayTeamsCalendar()
        End Using
    End Sub

End Class