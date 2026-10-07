Imports System.Net
Imports MySql.Data.MySqlClient

Public Class frmDeleteReason

    Public Property DeleteReason As String = String.Empty
    Private currentResidentID As Integer
    Private currentResidentName As String

    ' Overloaded Constructor na tumatanggap ng ID at mga detalye para sa log
    Public Sub New(residentID As Integer, residentCode As String, residentName As String, email As String, satelliteOffice As String)
        InitializeComponent()

        currentResidentID = residentID
        currentResidentName = residentName

        ' I-display ang Information sa RichTextBox
        rtfInfo.ReadOnly = True
        rtfInfo.BackColor = Color.FromArgb(245, 247, 252)
        rtfInfo.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        rtfInfo.Text = $"=== RESIDENT INFORMATION ===" & vbCrLf &
                       $"• Resident Code: {residentCode}" & vbCrLf &
                       $"• Full Name: {residentName}" & vbCrLf &
                       $"• Email Address: {email}" & vbCrLf &
                       $"• Satellite Office: {satelliteOffice}"
    End Sub

    Private Sub frmDeleteReason_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Text = "Delete Reason & Resident Information"

        rtfReason.Clear()
        rtfReason.Focus()
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ' Validation: Bawal i-submit kung walang laman o puro spaces
        If String.IsNullOrWhiteSpace(rtfReason.Text) Then
            MessageBox.Show("Kinakailangan ang rason o comment bago i-delete ang record.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            rtfReason.Focus()
            Return
        End If

        DeleteReason = rtfReason.Text.Trim()

        ' Isulat agad sa activity_logs para secure at malaman kung sino ang nag-delete
        Try
            connection()
            Dim logSql As String = "INSERT INTO activity_logs (ActionDate, ActionType, Details, DeviceInfo, FullName, IPAddress, Module, UserID, UserRole) " &
                                   "VALUES (NOW(), @actionType, @details, @device, @fullname, @ip, @module, @userid, @role)"

            Using cmdLog As New MySqlCommand(logSql, cn)
                cmdLog.Parameters.AddWithValue("@actionType", "DELETE_RESIDENT")
                cmdLog.Parameters.AddWithValue("@details", $"Removed resident: {currentResidentName} | Reason: {DeleteReason}")
                cmdLog.Parameters.AddWithValue("@device", Environment.MachineName)
                cmdLog.Parameters.AddWithValue("@fullname", If(String.IsNullOrWhiteSpace(LoggedFullname), "Admin", LoggedFullname))
                cmdLog.Parameters.AddWithValue("@ip", GetLocalIPAddress())
                cmdLog.Parameters.AddWithValue("@module", "RESIDENCE_RECORDS")
                cmdLog.Parameters.AddWithValue("@userid", currentResidentID)
                cmdLog.Parameters.AddWithValue("@role", If(String.IsNullOrWhiteSpace(LoggedRole), "Admin", LoggedRole))

                cmdLog.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving activity log: " & ex.Message, "Log Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    ' Helper function para makuha ang IP Address
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