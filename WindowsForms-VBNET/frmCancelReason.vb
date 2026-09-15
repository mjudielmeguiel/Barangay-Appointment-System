Public Class frmCancelReason

    ' Property to hold the reason so frmPayments can access it
    Public Property ReasonText As String = ""

    Private Sub frmCancelReason_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearError()
    End Sub

    ' Helper method to show error via label
    Private Sub ShowError(msg As String)
        If lblError IsNot Nothing Then
            lblError.Text = "⚠ " & msg
            lblError.ForeColor = Color.Red
            lblError.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            lblError.Visible = True
        End If
    End Sub

    ' Helper method to clear the error label
    Private Sub ClearError()
        If lblError IsNot Nothing Then
            lblError.Text = ""
            lblError.Visible = False
        End If
    End Sub

    Private Sub rtbReason_TextChanged(sender As Object, e As EventArgs) Handles rtbReason.TextChanged
        ClearError()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If String.IsNullOrWhiteSpace(rtbReason.Text.Trim()) Then
            ShowError("Please enter a reason for cancellation.")
            rtbReason.Focus()
            Return
        End If

        ReasonText = rtbReason.Text.Trim()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class