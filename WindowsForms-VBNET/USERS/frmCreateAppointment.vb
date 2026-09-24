Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices

Public Class frmCreateAppointment
    Private selectedResidentID As Integer = 0
    Private selectedResidentAddress As String = ""
    Private _skipClosePrompt As Boolean = False
    Private selectedRepresentativeID As Integer = 0

    Public Class ServiceItem
        Public Property ServiceName As String
        Public Property DepartmentName As String
        Public Overrides Function ToString() As String
            Return ServiceName
        End Function
    End Class

    Private Sub frmCreateAppointment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboRequestFor.Items.Clear()
        cboRequestFor.Items.AddRange({
            "Self",
            "Family Member / Relative",
            "Representative / On Behalf"
        })
        cboRequestFor.SelectedIndex = 0
        LoadDocumentServices()
        lblControlNo.Text = GenerateControlNumber()
        LoadLoggedUserDefault()

        cboRequestType.DropDownStyle = ComboBoxStyle.DropDown
        CueBanner.SetText(txtName, "Type resident name or click 'Select User'")
        CueBanner.SetText(txtNameOfRepresentative, "Representative's full name")
        CueBanner.SetText(txtPurpose, "State the purpose of your appointment")
        CueBanner.SetText(cboRequestType, "Select Request Type / Document Service")

        ToggleRepresentativeFields(False)
    End Sub

    Private Sub frmCreateAppointment_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If _skipClosePrompt Then Return
        If Not String.IsNullOrWhiteSpace(txtName.Text) Then
            Dim result As DialogResult = MsgBox($"Are you sure you want to cancel {txtName.Text.Trim()}?",
                                                 MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Cancel")
            If result = DialogResult.Yes Then
                SaveAppointment("PENDING")
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub cboRequestFor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRequestFor.SelectedIndexChanged
        Dim selectedOption As String = cboRequestFor.Text.Trim()
        If selectedOption = "Family Member / Relative" OrElse selectedOption = "Representative / On Behalf" Then
            ToggleRepresentativeFields(True)
        Else
            ToggleRepresentativeFields(False)
            txtNameOfRepresentative.Clear()
            selectedRepresentativeID = 0
        End If
    End Sub

    ' === TINANGGAL NA ANG DALAWANG PICTUREBOX DITO ===
    Private Sub ToggleRepresentativeFields(isVisible As Boolean)
        If txtNameOfRepresentative IsNot Nothing Then
            txtNameOfRepresentative.Visible = isVisible
        End If
        If btnSelectRepresentative IsNot Nothing Then
            btnSelectRepresentative.Visible = isVisible
        End If
        ' ✅ TINANGGAL NA: lblAuthLetter, picAuthLetter, lblRepID, picRepID
    End Sub

    Private Sub btnSelectRepresentative_Click(sender As Object, e As EventArgs) Handles btnSelectRepresentative.Click
        Using frm As New ResidenceList
            If frm.ShowDialog() = DialogResult.OK Then
                txtNameOfRepresentative.Text = frm.SelectedFullName
                selectedRepresentativeID = frm.SelectedResidentID
            End If
        End Using
    End Sub

    Private Sub LoadDocumentServices()
        cboRequestType.Items.Clear()
        Try
            connection()
            sql = "SELECT ds.ServiceName, d.DepartmentName " &
                  "FROM document_services ds " &
                  "LEFT JOIN departments d ON ds.DepartmentID = d.DepartmentID " &
                  "WHERE (ds.IsActive = 1 OR ds.IsActive IS NULL) " &
                  "ORDER BY ds.ServiceName ASC"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                Dim item As New ServiceItem With {
                    .ServiceName = dr("ServiceName").ToString(),
                    .DepartmentName = If(IsDBNull(dr("DepartmentName")), "", dr("DepartmentName").ToString())
                }
                cboRequestType.Items.Add(item)
            End While
            dr.Close()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Function GetSelectedDepartment() As String
        If cboRequestType.SelectedItem IsNot Nothing AndAlso TypeOf cboRequestType.SelectedItem Is ServiceItem Then
            Return CType(cboRequestType.SelectedItem, ServiceItem).DepartmentName
        End If
        Dim typedText As String = cboRequestType.Text.Trim()
        For Each itm As Object In cboRequestType.Items
            If TypeOf itm Is ServiceItem AndAlso
               CType(itm, ServiceItem).ServiceName.Equals(typedText, StringComparison.OrdinalIgnoreCase) Then
                Return CType(itm, ServiceItem).DepartmentName
            End If
        Next
        Return ""
    End Function

    Private Function GenerateControlNumber() As String
        Dim newCtrlNo As String = "APP-001"
        Try
            connection()
            sql = "SELECT ControlNo FROM appointments ORDER BY AppointmentID DESC LIMIT 1"
            cmd = New MySqlCommand(sql, cn)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                Dim lastCtrl As String = dr("ControlNo").ToString()
                Dim numPart As Integer = Convert.ToInt32(lastCtrl.Replace("APP-", ""))
                newCtrlNo = $"APP-{(numPart + 1):D3}"
            End If
            dr.Close()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
        Return newCtrlNo
    End Function

    Private Sub LoadLoggedUserDefault()
        If String.IsNullOrEmpty(LoggedFullname) Then Return
        Try
            connection()
            sql = "SELECT ResidentID, FullName, Picture, Address FROM residences WHERE FullName=@name OR Username=@name"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@name", LoggedFullname)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                selectedResidentID = If(IsDBNull(dr("ResidentID")), 0, Convert.ToInt32(dr("ResidentID")))
                txtName.Text = dr("FullName").ToString()
                selectedResidentAddress = If(IsDBNull(dr("Address")), "", dr("Address").ToString())
                If Not IsDBNull(dr("Picture")) Then
                    Dim imgBytes As Byte() = CType(dr("Picture"), Byte())
                    Using ms As New MemoryStream(imgBytes)
                        Dim rawImg As Image = Image.FromStream(ms)
                        If picUserProfile.Image IsNot Nothing Then picUserProfile.Image.Dispose()
                        picUserProfile.Image = MakeCircularImage(rawImg)
                    End Using
                End If
            End If
            dr.Close()
        Catch ex As Exception
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub btnSelectUser_Click(sender As Object, e As EventArgs) Handles btnSelectUser.Click
        Using frm As New ResidenceList
            If frm.ShowDialog() = DialogResult.OK Then
                selectedResidentID = frm.SelectedResidentID
                txtName.Text = frm.SelectedFullName
                LoadSelectedResidentDetails(selectedResidentID)
            End If
        End Using
    End Sub

    Private Sub LoadSelectedResidentDetails(resID As Integer)
        Try
            connection()
            sql = "SELECT Picture, Address FROM residences WHERE ResidentID = @id"
            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@id", resID)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                selectedResidentAddress = If(IsDBNull(dr("Address")), "", dr("Address").ToString())
                If Not IsDBNull(dr("Picture")) Then
                    Dim imgBytes As Byte() = CType(dr("Picture"), Byte())
                    Using ms As New MemoryStream(imgBytes)
                        Dim rawImg As Image = Image.FromStream(ms)
                        If picUserProfile.Image IsNot Nothing Then picUserProfile.Image.Dispose()
                        picUserProfile.Image = MakeCircularImage(rawImg)
                    End Using
                Else
                    picUserProfile.Image = Nothing
                End If
            Else
                picUserProfile.Image = Nothing
            End If
            dr.Close()
        Catch ex As Exception
            picUserProfile.Image = Nothing
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Function MakeCircularImage(srcImage As Image) As Image
        Dim targetWidth As Integer = If(picUserProfile IsNot Nothing AndAlso picUserProfile.Width > 0, picUserProfile.Width, 100)
        Dim targetHeight As Integer = If(picUserProfile IsNot Nothing AndAlso picUserProfile.Height > 0, picUserProfile.Height, 100)
        Dim circleDiameter As Integer = Math.Min(targetWidth, targetHeight)
        Dim bmp As New Bitmap(circleDiameter, circleDiameter)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.PixelOffsetMode = PixelOffsetMode.HighQuality
            g.CompositingQuality = CompositingQuality.HighQuality
            Using path As New GraphicsPath()
                path.AddEllipse(0, 0, circleDiameter, circleDiameter)
                g.SetClip(path)
                Dim minSrcDim As Integer = Math.Min(srcImage.Width, srcImage.Height)
                Dim srcRect As New Rectangle((srcImage.Width - minSrcDim) \ 2, (srcImage.Height - minSrcDim) \ 2, minSrcDim, minSrcDim)
                g.DrawImage(srcImage, New Rectangle(0, 0, circleDiameter, circleDiameter), srcRect, GraphicsUnit.Pixel)
            End Using
        End Using
        Return bmp
    End Function

    Private Function SaveAppointment(ByVal status As String) As Boolean
        Try
            connection()
            Dim isRepresentative As Boolean = (cboRequestFor.Text.Trim() = "Family Member / Relative" OrElse
                                               cboRequestFor.Text.Trim() = "Representative / On Behalf")
            Dim autoDepartment As String = GetSelectedDepartment()

            sql = "INSERT INTO appointments (ControlNo, ResidentID, FullName, FullAddress, RequestFor, " &
                  "RepresentativeName, RequestType, Purpose, Department, DateSubmitted, " &
                  "ScheduledDate, Status, CreatedAt) " &
                  "VALUES (@ctrl, @resID, @name, @address, @reqFor, @repName, " &
                  "@reqType, @purpose, @dept, NOW(), NOW(), @status, NOW())"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ctrl", lblControlNo.Text.Trim())
            cmd.Parameters.AddWithValue("@resID", If(selectedResidentID > 0, selectedResidentID, DBNull.Value))
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@address", If(String.IsNullOrWhiteSpace(selectedResidentAddress), "", selectedResidentAddress))
            cmd.Parameters.AddWithValue("@reqFor", If(String.IsNullOrWhiteSpace(cboRequestFor.Text.Trim()), "", cboRequestFor.Text.Trim()))
            cmd.Parameters.AddWithValue("@repName", If(isRepresentative AndAlso Not String.IsNullOrWhiteSpace(txtNameOfRepresentative.Text.Trim()),
                                                        txtNameOfRepresentative.Text.Trim(), ""))
            cmd.Parameters.AddWithValue("@reqType", If(String.IsNullOrWhiteSpace(cboRequestType.Text.Trim()), "", cboRequestType.Text.Trim()))
            cmd.Parameters.AddWithValue("@purpose", If(String.IsNullOrWhiteSpace(txtPurpose.Text.Trim()), "", txtPurpose.Text.Trim()))
            cmd.Parameters.AddWithValue("@dept", If(String.IsNullOrWhiteSpace(autoDepartment), "", autoDepartment))
            cmd.Parameters.AddWithValue("@status", status)

            Return cmd.ExecuteNonQuery() > 0
        Catch ex As Exception
            MsgBox("Error saving appointment: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MsgBox("Please select or enter a resident name.", MsgBoxStyle.Exclamation, "Validation Error")
            txtName.Focus()
            Return
        End If

        Dim isRepresentative As Boolean = (cboRequestFor.Text.Trim() = "Family Member / Relative" OrElse
                                            cboRequestFor.Text.Trim() = "Representative / On Behalf")

        If isRepresentative Then
            If String.IsNullOrWhiteSpace(txtNameOfRepresentative.Text) Then
                MsgBox("Please enter the Representative's full name.", MsgBoxStyle.Exclamation, "Validation Error")
                txtNameOfRepresentative.Focus()
                Return
            End If
            ' ✅ TINANGGAL NA ANG PAG-CHECK SA UPLOAD — hindi na kailangan dito
        End If

        If cboRequestType.SelectedIndex = -1 AndAlso String.IsNullOrWhiteSpace(cboRequestType.Text) Then
            MsgBox("Please select a valid Request Type / Document Service.", MsgBoxStyle.Exclamation, "Validation Error")
            cboRequestType.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtPurpose.Text) Then
            MsgBox("Please state the purpose of your appointment.", MsgBoxStyle.Exclamation, "Validation Error")
            txtPurpose.Focus()
            Return
        End If

        If SaveAppointment("APPROVED") Then
            _skipClosePrompt = True
            MsgBox($"Pick-up appointment request {lblControlNo.Text.Trim()} submitted and APPROVED successfully!",
                   MsgBoxStyle.Information, "Success")
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MsgBox("Failed to submit pick-up appointment request.", MsgBoxStyle.Exclamation, "Warning")
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            _skipClosePrompt = True
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
            Return
        End If

        Dim result As DialogResult = MsgBox($"Are you sure you want to cancel {txtName.Text.Trim()}?",
                                             MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Cancel")
        If result = DialogResult.Yes Then
            If SaveAppointment("PENDING") Then
                MsgBox($"Appointment {lblControlNo.Text.Trim()} saved as PENDING.", MsgBoxStyle.Information, "Saved")
            End If
            _skipClosePrompt = True
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End If
    End Sub
End Class

Public Class CueBanner
    Private Const EM_SETCUEBANNER As Integer = &H1501
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer,
                                        ByVal wParam As Integer,
                                        <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function FindWindowEx(ByVal hWndParent As IntPtr, ByVal hWndChildAfter As IntPtr,
                                         ByVal lpszClass As String, ByVal lpszWindow As String) As IntPtr
    End Function

    Public Shared Sub SetText(ByVal txt As TextBox, ByVal cueText As String)
        If txt.IsHandleCreated Then
            SendMessage(txt.Handle, EM_SETCUEBANNER, 0, cueText)
        End If
    End Sub

    Public Shared Sub SetText(ByVal cbo As ComboBox, ByVal cueText As String)
        If cbo.IsHandleCreated Then
            Dim editHandle As IntPtr = FindWindowEx(cbo.Handle, IntPtr.Zero, "Edit", Nothing)
            If editHandle <> IntPtr.Zero Then
                SendMessage(editHandle, EM_SETCUEBANNER, 0, cueText)
            End If
        End If
    End Sub
End Class