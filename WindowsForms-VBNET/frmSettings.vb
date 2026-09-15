Public Class frmSettings

    Private Sub btnCreateRequest_Click(sender As Object, e As EventArgs) Handles btnCreateRequest.Click
        frmChange_Password.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmMain.Panel2.Controls.Clear()
        Dim Manage As New frmManage_Users With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        frmMain.Panel2.Controls.Add(Manage)
        Manage.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frmDocumentServices.Show()
    End Sub

    Private Sub btnDeleteAccount_Click(sender As Object, e As EventArgs) Handles BtnDeleteAccount.Click
        ' Show the custom password confirmation form
        Using confirmFrm As New frmConfirmPasswordDelete()
            If confirmFrm.ShowDialog() = DialogResult.OK Then
                ' Account deleted and user logged out automatically inside the confirm form
                frmMain.Close()
            End If
        End Using
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        frmSystemSettings.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        frmMain.Panel2.Controls.Clear()
        Dim Generate As New frmReportGeneration With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        frmMain.Panel2.Controls.Add(Generate)
        Generate.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmMain.Panel2.Controls.Clear()
        Dim Logs As New frmActivityLogs With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        frmMain.Panel2.Controls.Add(Logs)
        Logs.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        frmMain.Panel2.Controls.Clear()
        Dim about As New frm_About_us With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill
        }
        frmMain.Panel2.Controls.Add(about)
        about.Show()
    End Sub
End Class