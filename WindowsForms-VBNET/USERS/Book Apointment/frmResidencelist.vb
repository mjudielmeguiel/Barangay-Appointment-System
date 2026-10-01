Imports MySql.Data.MySqlClient

Public Class frmResidencelist
    ' Property para mai-pasa ang ID sa frmCreateAppointment
    Public Property SelectedResidentID As Integer = 0

    Private Sub frmResidencelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadResidents()
    End Sub

    ' Function para kunin ang data sa database at ilagay sa DataGridView
    Private Sub LoadResidents()
        Try
            connection() ' Tatawagin ang iyong global database connection

            ' Kinukuha ang mga basic info na kailangan lang makita sa pagpili
            sql = "SELECT ResidentID, FullName, Address, Gender, CivilStatus FROM residences"
            cmd = New MySqlCommand(sql, cn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            ' Ilalagay ang data sa DataGridView. 
            ' Palitan ang "DataGridView1" kung iba ang pangalan nito sa Properties ng form mo.
            DataGridView1.DataSource = dt

            ' (Optional) Ayusin ang lapad ng columns para maganda tignan
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        Catch ex As Exception
            MsgBox("Error loading residence list: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' Event kapag na-double click ang row sa DataGridView1
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            ' Kukunin ang ResidentID base sa na-click na row
            SelectedResidentID = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("ResidentID").Value)

            ' I-set ang result para ma-trigger ang pag-load sa frmCreateAppointment
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub
End Class