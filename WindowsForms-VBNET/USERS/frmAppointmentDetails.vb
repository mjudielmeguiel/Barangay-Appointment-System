Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmAppointmentDetails

    Private targetControlNo As String = ""
    Private currentStatus As String = "PENDING"

    Public Sub New(ByVal controlNo As String)
        InitializeComponent()
        targetControlNo = controlNo
    End Sub

    Private Sub frmAppointmentDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup para sa Address RichTextBox para magmukhang flat label
        If RtbAddress IsNot Nothing Then
            RtbAddress.ReadOnly = True
            RtbAddress.BackColor = Color.FromArgb(245, 247, 250)
            RtbAddress.BorderStyle = BorderStyle.None
        End If

        LoadAppointmentDetails()
    End Sub

    Private Sub LoadAppointmentDetails()
        If String.IsNullOrEmpty(targetControlNo) Then Return

        Dim residentID As Integer = 0
        Dim fullName As String = ""

        Try
            connection()

            ' Kasama na dito ang FullAddress base sa appointments table mo
            sql = "SELECT ControlNo, ResidentID, FullName, FullAddress, " &
                  "RequestType, Status, DateSubmitted, RequestFor " &
                  "FROM appointments WHERE ControlNo = @ctrl"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ctrl", targetControlNo)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                lblControlNo.Text = dr("ControlNo").ToString()
                fullName = dr("FullName").ToString()
                lblName.Text = fullName
                residentID = If(IsDBNull(dr("ResidentID")), 0, Convert.ToInt32(dr("ResidentID")))

                ' DITO IDI-DISPLAY ANG ADDRESS NG NAKA-APPOINTMENT:
                If RtbAddress IsNot Nothing Then
                    RtbAddress.Text = If(IsDBNull(dr("FullAddress")), "No Address Provided", dr("FullAddress").ToString())
                End If

                currentStatus = dr("Status").ToString().ToUpper()
                lblStatus.Text = currentStatus

                If Not IsDBNull(dr("DateSubmitted")) Then
                    lblDateSubmitted.Text = Convert.ToDateTime(dr("DateSubmitted")).ToString("f")
                Else
                    lblDateSubmitted.Text = "-"
                End If

                lblServiceType.Text = dr("RequestType").ToString()

                Dim requestFor As String = If(IsDBNull(dr("RequestFor")), "Self", dr("RequestFor").ToString())

                If lblRequestFor IsNot Nothing Then lblRequestFor.Text = requestFor

                ' Expected Pickup Logic
                If lblExpectedPickup IsNot Nothing Then
                    If currentStatus = "APPROVED" Then
                        lblExpectedPickup.Text = "Expected Pickup: " & DateTime.Now.ToString("MMMM dd, yyyy") & " (Today)"
                        lblExpectedPickup.Visible = True
                    Else
                        lblExpectedPickup.Visible = False
                    End If
                End If

                ' Button para sa Representative Details Form
                If btnViewRepDetails IsNot Nothing Then
                    If requestFor.Equals("Self", StringComparison.OrdinalIgnoreCase) Then
                        btnViewRepDetails.Visible = False
                    Else
                        btnViewRepDetails.Visible = True
                    End If
                End If
            End If
            dr.Close()

            LoadResidentMedia(residentID, fullName)

        Catch ex As Exception
            MsgBox("Error loading appointment details: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            CloseConnection()
        End Try
    End Sub

    ' Click event para magbukas ang form ng Representative Details
    Private Sub btnViewRepDetails_Click(sender As Object, e As EventArgs) Handles btnViewRepDetails.Click
        Dim repForm As New frmAuthorizationLetter(targetControlNo)
        repForm.ShowDialog()
    End Sub

    ' Click event para isara ang form
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub LoadResidentMedia(resID As Integer, name As String)
        Try
            sql = "SELECT Picture FROM residences " &
                  "WHERE ResidentID = @id OR FullName = @name"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", resID)
            cmd.Parameters.AddWithValue("@name", name)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                If Not IsDBNull(dr("Picture")) AndAlso picProfile IsNot Nothing Then
                    DisplayImage(CType(dr("Picture"), Byte()), picProfile)
                End If
            End If
            dr.Close()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub DisplayImage(imgBytes As Byte(), picBox As PictureBox)
        If imgBytes IsNot Nothing AndAlso imgBytes.Length > 0 Then
            Using ms As New MemoryStream(imgBytes)
                picBox.SizeMode = PictureBoxSizeMode.Zoom
                If picBox.Image IsNot Nothing Then picBox.Image.Dispose()
                picBox.Image = Image.FromStream(ms)
            End Using
        End If
    End Sub
End Class