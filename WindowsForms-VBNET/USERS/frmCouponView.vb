Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Drawing.Printing

Public Class frmCouponView

    Private controlNo As String
    Private residentName As String = ""
    Private documentType As String = ""
    Private scheduledDateTime As String = ""
    Private status As String = ""

    Public Sub New(ByVal ctrlNo As String)
        InitializeComponent()
        Me.controlNo = ctrlNo
    End Sub

    Private Sub frmCouponView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Appointment Claim Stub - " & controlNo
        Me.Size = New Size(380, 480)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.FromArgb(240, 242, 245)

        FetchAppointmentData()
        BuildCouponUI()
    End Sub

    Private Sub FetchAppointmentData()
        Try
            DBconnection.connection()
            DBconnection.sql = "SELECT ControlNo, FullName, RequestType, ScheduledDate, Status " &
                               "FROM appointments WHERE ControlNo = @ctrl LIMIT 1"

            DBconnection.cmd = New MySqlCommand(DBconnection.sql, DBconnection.cn)
            DBconnection.cmd.Parameters.AddWithValue("@ctrl", controlNo)
            DBconnection.dr = DBconnection.cmd.ExecuteReader()

            If DBconnection.dr.Read() Then
                residentName = DBconnection.dr("FullName").ToString()
                documentType = DBconnection.dr("RequestType").ToString()
                status = DBconnection.dr("Status").ToString()

                If Not IsDBNull(DBconnection.dr("ScheduledDate")) Then
                    Dim dt As DateTime = Convert.ToDateTime(DBconnection.dr("ScheduledDate"))
                    scheduledDateTime = dt.ToString("MMMM dd, yyyy - h:mm tt")
                Else
                    scheduledDateTime = "N/A"
                End If
            End If
            DBconnection.dr.Close()
        Catch ex As Exception
            MsgBox("Error reading coupon data: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            DBconnection.CloseConnection()
        End Try
    End Sub

    Private Sub BuildCouponUI()
        ' White Ticket Card Panel
        Dim pnlCard As New Panel With {
            .Size = New Size(320, 350),
            .Location = New Point(22, 15),
            .BackColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle
        }

        Dim yOffset As Integer = 15

        ' Header Labels
        Dim lblHeader As New Label With {
            .Text = "BARANGAY PUTATAN",
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .ForeColor = Color.FromArgb(10, 25, 100),
            .AutoSize = False,
            .Size = New Size(318, 22),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(0, yOffset)
        }
        yOffset += 22

        Dim lblSubHeader As New Label With {
            .Text = "City of Muntinlupa",
            .Font = New Font("Segoe UI", 8, FontStyle.Italic),
            .ForeColor = Color.Gray,
            .AutoSize = False,
            .Size = New Size(318, 18),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(0, yOffset)
        }
        yOffset += 25

        ' Divider Line
        Dim pnlDivider1 As New Panel With {
            .Size = New Size(280, 1),
            .BackColor = Color.LightGray,
            .Location = New Point(20, yOffset)
        }
        yOffset += 10

        ' Control Number
        Dim lblTitle As New Label With {
            .Text = "CLAIM STUB",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .ForeColor = Color.FromArgb(80, 80, 80),
            .AutoSize = False,
            .Size = New Size(318, 20),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(0, yOffset)
        }
        yOffset += 20

        Dim lblCtrlNo As New Label With {
            .Text = controlNo,
            .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = Color.DarkRed,
            .AutoSize = False,
            .Size = New Size(318, 30),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(0, yOffset)
        }
        yOffset += 35

        ' Details Section
        Dim lblNameTitle As New Label With {.Text = "Resident Name:", .Font = New Font("Segoe UI", 8, FontStyle.Bold), .ForeColor = Color.Gray, .Location = New Point(20, yOffset), .AutoSize = True}
        yOffset += 15
        Dim lblNameVal As New Label With {.Text = residentName, .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold), .ForeColor = Color.Black, .Location = New Point(20, yOffset), .AutoSize = True}
        yOffset += 22

        Dim lblDocTitle As New Label With {.Text = "Document / Service:", .Font = New Font("Segoe UI", 8, FontStyle.Bold), .ForeColor = Color.Gray, .Location = New Point(20, yOffset), .AutoSize = True}
        yOffset += 15
        Dim lblDocVal As New Label With {.Text = documentType, .Font = New Font("Segoe UI", 9, FontStyle.Regular), .ForeColor = Color.Black, .Location = New Point(20, yOffset), .AutoSize = True}
        yOffset += 22

        Dim lblDateTitle As New Label With {.Text = "Pick-up Schedule:", .Font = New Font("Segoe UI", 8, FontStyle.Bold), .ForeColor = Color.Gray, .Location = New Point(20, yOffset), .AutoSize = True}
        yOffset += 15
        Dim lblDateVal As New Label With {.Text = scheduledDateTime, .Font = New Font("Segoe UI", 9, FontStyle.Bold), .ForeColor = Color.DarkBlue, .Location = New Point(20, yOffset), .AutoSize = True}
        yOffset += 25

        ' Footer Note
        Dim lblFooter As New Label With {
            .Text = "Please present this coupon upon pick-up.",
            .Font = New Font("Segoe UI", 8, FontStyle.Italic),
            .ForeColor = Color.DimGray,
            .AutoSize = False,
            .Size = New Size(318, 18),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(0, yOffset)
        }

        pnlCard.Controls.AddRange({lblHeader, lblSubHeader, pnlDivider1, lblTitle, lblCtrlNo, lblNameTitle, lblNameVal, lblDocTitle, lblDocVal, lblDateTitle, lblDateVal, lblFooter})
        Me.Controls.Add(pnlCard)

        ' Print & Close Buttons
        Dim btnPrint As New Button With {
            .Text = "Print Coupon",
            .Size = New Size(110, 32),
            .Location = New Point(70, 380),
            .BackColor = Color.FromArgb(10, 25, 100),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }
        AddHandler btnPrint.Click, Sub(s, e) PrintCouponDocument()

        Dim btnClose As New Button With {
            .Text = "Close",
            .Size = New Size(90, 32),
            .Location = New Point(190, 380),
            .BackColor = Color.White,
            .ForeColor = Color.Black,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
            .Cursor = Cursors.Hand
        }
        AddHandler btnClose.Click, Sub(s, e) Me.Close()

        Me.Controls.Add(btnPrint)
        Me.Controls.Add(btnClose)
    End Sub

    Private Sub PrintCouponDocument()
        Dim printDoc As New PrintDocument()
        AddHandler printDoc.PrintPage, AddressOf DrawPrintPage
        Dim previewDlg As New PrintPreviewDialog With {.Document = printDoc}
        previewDlg.ShowDialog()
    End Sub

    Private Sub DrawPrintPage(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Dim fontHeader As New Font("Segoe UI", 12, FontStyle.Bold)
        Dim fontSubHeader As New Font("Segoe UI", 9, FontStyle.Italic)
        Dim fontTitle As New Font("Segoe UI", 14, FontStyle.Bold)
        Dim fontBodyBold As New Font("Segoe UI", 9, FontStyle.Bold)
        Dim fontBody As New Font("Segoe UI", 9, FontStyle.Regular)

        Dim startX As Integer = 30
        Dim startY As Integer = 30
        Dim offset As Integer = 0

        g.DrawString("BARANGAY PUTATAN", fontHeader, Brushes.Black, startX + 40, startY + offset)
        offset += 20
        g.DrawString("City of Muntinlupa", fontSubHeader, Brushes.Black, startX + 65, startY + offset)
        offset += 25
        g.DrawLine(Pens.Black, startX, startY + offset, startX + 280, startY + offset)
        offset += 15

        g.DrawString("APPOINTMENT CLAIM STUB", fontTitle, Brushes.DarkBlue, startX + 10, startY + offset)
        offset += 30
        g.DrawString($"CONTROL NO: {controlNo}", fontHeader, Brushes.Red, startX + 40, startY + offset)
        offset += 30
        g.DrawLine(Pens.Gray, startX, startY + offset, startX + 280, startY + offset)
        offset += 15

        g.DrawString("Resident Name:", fontBodyBold, Brushes.Gray, startX, startY + offset)
        offset += 16
        g.DrawString(residentName, fontBodyBold, Brushes.Black, startX + 10, startY + offset)
        offset += 22

        g.DrawString("Document / Service:", fontBodyBold, Brushes.Gray, startX, startY + offset)
        offset += 16
        g.DrawString(documentType, fontBody, Brushes.Black, startX + 10, startY + offset)
        offset += 22

        g.DrawString("Pick-up Schedule:", fontBodyBold, Brushes.Gray, startX, startY + offset)
        offset += 16
        g.DrawString(scheduledDateTime, fontBodyBold, Brushes.DarkBlue, startX + 10, startY + offset)
        offset += 30

        g.DrawLine(Pens.Black, startX, startY + offset, startX + 280, startY + offset)
        offset += 10
        g.DrawString("Please present this coupon upon pick-up.", fontSubHeader, Brushes.Black, startX + 15, startY + offset)
    End Sub

End Class