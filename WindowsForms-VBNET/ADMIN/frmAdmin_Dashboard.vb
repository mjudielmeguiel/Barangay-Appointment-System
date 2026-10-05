Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D ' ✅ Kailangan ito para sa pag-gawa ng bilog na PictureBox

Public Class frmAdmin_Dashboard

    Private Sub frmAdmin_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MakeCircularPictureBox() ' ✅ Tinatawag ito para maging bilog agad ang PictureBox
        LoadAdminProfile()
        LoadDashboardStats()
    End Sub

    ' === ✅ FUNCTION PARA MAGING BILOG ANG PROFILE PICTURE ===
    Private Sub MakeCircularPictureBox()
        If PictureBox1 IsNot Nothing Then
            Dim path As New GraphicsPath()
            ' Gumagawa ng hugis bilog base sa size ng PictureBox
            path.AddEllipse(0, 0, PictureBox1.Width, PictureBox1.Height)
            PictureBox1.Region = New Region(path)
        End If
    End Sub

    Private Sub LoadAdminProfile()
        Try
            ' ✅ Kukunin natin ang eksaktong ID mula sa frmlogin para malaman kung sino ang nag-login
            Dim targetID As Integer = If(frmlogin.LoggedInUserID > 0, frmlogin.LoggedInUserID, 1)

            connection()
            ' Kukunin ang Firstname, Lastname, at Picture mula sa admin table
            Dim query As String = "SELECT Firstname, Lastname, Picture FROM admin WHERE AdminID = @AdminID"
            Using cmdAdmin As New MySqlCommand(query, cn)
                cmdAdmin.Parameters.AddWithValue("@AdminID", targetID)

                Using reader As MySqlDataReader = cmdAdmin.ExecuteReader()
                    If reader.Read() Then
                        ' ✅ Ilagay ang Pangalan
                        Dim firstName As String = If(reader("Firstname") IsNot DBNull.Value, reader("Firstname").ToString(), "")
                        Dim lastName As String = If(reader("Lastname") IsNot DBNull.Value, reader("Lastname").ToString(), "")

                        If lblFullname IsNot Nothing Then
                            lblFullname.Text = (firstName & " " & lastName).Trim()
                        End If

                        ' ✅ I-load ang Picture
                        If reader("Picture") IsNot DBNull.Value Then
                            Dim imgData As Byte() = DirectCast(reader("Picture"), Byte())
                            Using ms As New MemoryStream(imgData)
                                If PictureBox1 IsNot Nothing Then
                                    PictureBox1.Image = Image.FromStream(ms)
                                    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                                End If
                            End Using
                        Else
                            ' Kung walang nakasave na picture sa database
                            If PictureBox1 IsNot Nothing Then
                                PictureBox1.Image = Nothing
                            End If
                        End If
                    Else
                        ' Kung hindi nag-match ang ID sa database
                        If lblFullname IsNot Nothing Then
                            lblFullname.Text = "Not Found"
                        End If
                        MsgBox("Hindi mahanap ang Admin record. Siguraduhing tama ang pag-pasa ng LoggedInUserID mula sa login form.", MsgBoxStyle.Exclamation)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error loading admin profile: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' --- ✅ INAYOS: Double click PictureBox1 to change and update admin profile picture ---
    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Title = "Select Profile Picture"
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    ' Convert the selected image file into a byte array
                    Dim imgBytes As Byte() = File.ReadAllBytes(openFileDialog.FileName)

                    ' ✅ Kukunin muli ang ID ng kasalukuyang nakalogin
                    Dim targetID As Integer = If(frmlogin.LoggedInUserID > 0, frmlogin.LoggedInUserID, 1)

                    connection()
                    Dim query As String = "UPDATE admin SET Picture = @Picture WHERE AdminID = @AdminID"
                    Using cmd As New MySqlCommand(query, cn)
                        cmd.Parameters.AddWithValue("@Picture", imgBytes)
                        cmd.Parameters.AddWithValue("@AdminID", targetID) ' ✅ Ginamit na ang tamang targetID

                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected > 0 Then
                            MsgBox("Profile picture updated successfully!", MsgBoxStyle.Information)
                            LoadAdminProfile() ' ✅ I-reload ang profile para makita agad ang bagong picture
                        Else
                            MsgBox("Failed to update profile picture.", MsgBoxStyle.Exclamation)
                        End If
                    End Using
                Catch ex As Exception
                    MsgBox("Error updating profile picture: " & ex.Message, MsgBoxStyle.Critical)
                Finally
                    CloseConnection()
                End Try
            End If
        End Using
    End Sub

    ' Helper function to execute scalar COUNT queries and reduce repeated code blocks
    Private Function GetScalarCount(query As String) As Integer
        Try
            Using cmd As New MySqlCommand(query, cn)
                Dim res = cmd.ExecuteScalar()
                If res IsNot Nothing AndAlso res IsNot DBNull.Value Then
                    Return Convert.ToInt32(res)
                End If
            End Using
        Catch ex As Exception
            ' Optional: handle or log error
        End Try
        Return 0
    End Function

    Private Sub LoadDashboardStats()
        Try
            connection()

            ' 1. Online Count (All-time)
            Dim onlineCount As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%online%' OR LOWER(AppointmentType) LIKE '%online%')")
            SafeSetText(lblOnline, onlineCount.ToString())

            ' 2. Walk-in Count (All-time)
            Dim walkinCount As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%walk%' OR LOWER(AppointmentType) LIKE '%walk%')")
            SafeSetText(lblWalkIn, walkinCount.ToString())

            ' 3. Total Appointments Count (All-time)
            Dim totalAppointments As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments")
            SafeSetText(lblTotalAppointments, totalAppointments.ToString())

            ' 4. Cancelled Online Count (All-time)
            Dim cancelledOnline As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%online%' OR LOWER(AppointmentType) LIKE '%online%') AND LOWER(Status) LIKE '%cancel%'")
            SafeSetText(lblCancelledOnline, cancelledOnline.ToString())

            ' 5. Cancelled Walk-in Count (All-time)
            Dim cancelledWalkin As Integer = GetScalarCount("SELECT COUNT(*) FROM appointments WHERE (LOWER(RequestType) LIKE '%walk%' OR LOWER(AppointmentType) LIKE '%walk%') AND LOWER(Status) LIKE '%cancel%'")
            SafeSetText(lblCancelledWalkIn, cancelledWalkin.ToString())

            ' 6. Total Users Count ('Request' card)
            Dim totalUsers As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE IsDeleted = 0")
            SafeSetText(lblUsers, totalUsers.ToString())

            ' 7. Staff Count ('Staff' card)
            Dim staffCount As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE LOWER(Role) = 'staff' AND IsDeleted = 0")
            SafeSetText(lblStaff, staffCount.ToString())

            ' 8. Active Users Count ('Active' card)
            Dim activeCount As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE LOWER(AccountStatus) = 'active' AND IsDeleted = 0")
            SafeSetText(lblActive, activeCount.ToString())

            ' 9. Locked Users Count ('Locked' card)
            Dim lockedCount As Integer = GetScalarCount("SELECT COUNT(*) FROM users WHERE (LOWER(AccountStatus) LIKE '%lock%' OR (LockoutExpiry IS NOT NULL AND LockoutExpiry > NOW())) AND IsDeleted = 0")
            SafeSetText(lblnewResident, lockedCount.ToString())

        Catch ex As Exception
            MsgBox("Error loading dashboard stats: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub SafeSetText(lbl As Label, val As String)
        If lbl IsNot Nothing Then
            lbl.Text = val
        End If
    End Sub
End Class