Imports System.Net
Imports MySql.Data.MySqlClient

Public Class frmDeleteReason
    Public Property DeleteReason As String = String.Empty
    Public Property IsPermanentDelete As Boolean = False

    Private currentResidentID As Integer
    Private currentResidentName As String

    ' =========================================================================
    ' CONSTRUCTOR 1: Para sa normal na "Request Deletion"
    ' =========================================================================
    Public Sub New(residentID As Integer)
        InitializeComponent()
        LoadResidentDetails(residentID)
    End Sub

    ' =========================================================================
    ' CONSTRUCTOR 2: Para sa Admin View (Permanent Delete Confirmation na may kumpletong info)
    ' =========================================================================
    Public Sub New(residentID As Integer, requestedBy As String, reqDate As String, reason As String)
        InitializeComponent()
        LoadResidentDetails(residentID, requestedBy, reqDate)

        IsPermanentDelete = True

        ' I-lock ang textbox para basahin na lang ng Admin ang rason
        rtfReason.Text = reason
        rtfReason.ReadOnly = True

        btnConfirm.Text = "CONFIRM PERMANENT DELETE"
        btnConfirm.BackColor = Color.DarkRed
        btnConfirm.ForeColor = Color.White
    End Sub

    ' Kunin ang lahat ng impormasyon direkta sa database maliban sa password
    Private Sub LoadResidentDetails(residentID As Integer, Optional requestedBy As String = "", Optional reqDate As String = "")
        currentResidentID = residentID
        rtfInfo.ReadOnly = True

        Try
            If cn.State <> ConnectionState.Open Then connection()
            Dim query As String = "SELECT * FROM residences WHERE ResidentID = @id"
            Using cmd As New MySqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@id", residentID)
                Using dr As MySqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        currentResidentName = If(Not IsDBNull(dr("FullName")), dr("FullName").ToString(), "")

                        Dim infoText As String = "=== RESIDENT INFORMATION ===" & vbCrLf &
                            $"• Resident Code: {If(Not IsDBNull(dr("ResidentCode")), dr("ResidentCode"), "")}" & vbCrLf &
                            $"• Full Name: {currentResidentName}" & vbCrLf &
                            $"• First Name: {If(Not IsDBNull(dr("Firstname")), dr("Firstname"), "")}" & vbCrLf &
                            $"• Middle Name: {If(Not IsDBNull(dr("Middlename")), dr("Middlename"), "")}" & vbCrLf &
                            $"• Last Name: {If(Not IsDBNull(dr("Lastname")), dr("Lastname"), "")}" & vbCrLf &
                            $"• Suffix: {If(Not IsDBNull(dr("Suffix")), dr("Suffix"), "")}" & vbCrLf &
                            $"• Gender: {If(Not IsDBNull(dr("Gender")), dr("Gender"), "")}" & vbCrLf &
                            $"• Birthday: {If(Not IsDBNull(dr("Birthday")), Convert.ToDateTime(dr("Birthday")).ToShortDateString(), "")}" & vbCrLf &
                            $"• Birth Place: {If(Not IsDBNull(dr("BirthPlace")), dr("BirthPlace"), "")}" & vbCrLf &
                            $"• Civil Status: {If(Not IsDBNull(dr("CivilStatus")), dr("CivilStatus"), "")}" & vbCrLf &
                            $"• Nationality: {If(Not IsDBNull(dr("Nationality")), dr("Nationality"), "")}" & vbCrLf &
                            $"• Mobile Number: {If(Not IsDBNull(dr("MobileNumber")), dr("MobileNumber"), "")}" & vbCrLf &
                            $"• Email: {If(Not IsDBNull(dr("Email")), dr("Email"), "")}" & vbCrLf &
                            $"• Address: {If(Not IsDBNull(dr("Address")), dr("Address"), "")}" & vbCrLf &
                            $"• Satellite Office: {If(Not IsDBNull(dr("SatelliteOffice")), dr("SatelliteOffice"), "")}" & vbCrLf &
                            $"• Father's Name: {If(Not IsDBNull(dr("FatherName")), dr("FatherName"), "")}" & vbCrLf &
                            $"• Mother's Name: {If(Not IsDBNull(dr("MotherName")), dr("MotherName"), "")}" & vbCrLf &
                            $"• Account Status: {If(Not IsDBNull(dr("AccountStatus")), dr("AccountStatus"), "")}" & vbCrLf &
                            $"• Username: {If(Not IsDBNull(dr("Username")), dr("Username"), "")}" & vbCrLf &
                            $"• Date Registered: {If(Not IsDBNull(dr("CreatedAt")), dr("CreatedAt"), "")}"

                        ' Kung Admin View, idagdag sa ibaba ang Deletion Request Details
                        If Not String.IsNullOrEmpty(requestedBy) Then
                            infoText &= vbCrLf & vbCrLf & "=== DELETION REQUEST DETAILS ===" & vbCrLf &
                                        $"• Requested By: {requestedBy}" & vbCrLf &
                                        $"• Request Date/Time: {reqDate}"
                        End If

                        rtfInfo.Text = infoText
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading resident info: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub frmDeleteReason_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Text = If(IsPermanentDelete, "Admin: Confirm Permanent Deletion", "Request Deletion")

        If Not IsPermanentDelete Then
            rtfReason.Clear()
            rtfReason.Focus()
        End If
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If Not IsPermanentDelete AndAlso String.IsNullOrWhiteSpace(rtfReason.Text) Then
            MessageBox.Show("Kinakailangan ang rason o comment bago i-delete ang record.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            rtfReason.Focus()
            Return
        End If

        DeleteReason = rtfReason.Text.Trim()

        Dim currentFullName As String = If(String.IsNullOrWhiteSpace(frmlogin.LoggedInFullname), "Admin", frmlogin.LoggedInFullname)
        Dim currentRole As String = If(String.IsNullOrWhiteSpace(frmlogin.LoggedInRole), "Administrator", frmlogin.LoggedInRole)
        Dim currentUserID As Integer = frmlogin.LoggedInUserID

        Try
            connection()

            ' 📌 Kung Permanent Delete na ginawa ng Admin, tuluyan nang burahin sa database
            If IsPermanentDelete Then
                Dim deleteSql As String = "DELETE FROM residences WHERE ResidentID = @id"
                Using cmdDelete As New MySqlCommand(deleteSql, cn)
                    cmdDelete.Parameters.AddWithValue("@id", currentResidentID)
                    cmdDelete.ExecuteNonQuery()
                End Using
            End If

            ' Isulat sa activity_logs
            Dim logSql As String = "INSERT INTO activity_logs (ActionDate, ActionType, Details, DeviceInfo, FullName, IPAddress, Module, UserID, UserRole) " &
                                   "VALUES (NOW(), @actionType, @details, @device, @fullname, @ip, @module, @userid, @role)"

            Using cmdLog As New MySqlCommand(logSql, cn)
                cmdLog.Parameters.AddWithValue("@actionType", If(IsPermanentDelete, "PERMANENT_DELETE_RESIDENT", "REQUEST_DELETE_RESIDENT"))
                cmdLog.Parameters.AddWithValue("@details", $"Resident: {currentResidentName} | Reason: {DeleteReason}")
                cmdLog.Parameters.AddWithValue("@device", Environment.MachineName)
                cmdLog.Parameters.AddWithValue("@fullname", currentFullName)
                cmdLog.Parameters.AddWithValue("@ip", GetLocalIPAddress())
                cmdLog.Parameters.AddWithValue("@module", "RESIDENCE_RECORDS")
                cmdLog.Parameters.AddWithValue("@userid", currentUserID)
                cmdLog.Parameters.AddWithValue("@role", currentRole)

                cmdLog.ExecuteNonQuery()
            End Using

            If IsPermanentDelete Then
                MessageBox.Show("Permanently deleted from system.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error processing deletion: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function GetLocalIPAddress() As String
        Try
            Dim host = Dns.GetHostEntry(Dns.GetHostName())
            For Each ip In host.AddressList
                If ip.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
        Catch
        End Try
        Return "Unknown"
    End Function
End Class