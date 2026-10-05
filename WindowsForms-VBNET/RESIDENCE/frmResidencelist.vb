Imports MySql.Data.MySqlClient

Public Class frmResidencelist
    ' Property para mai-pasa ang ID sa frmCreateAppointment
    Public Property SelectedResidentID As Integer = 0

    Private Sub frmResidencelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadResidents()
    End Sub

    Private Sub LoadResidents()
        Try
            If cn.State <> ConnectionState.Open Then connection()

            ' Kukunin lamang ang basic info para sa grid view
            sql = "SELECT ResidentID, FullName, Address, Gender, CivilStatus FROM residences"
            cmd = New MySqlCommand(sql, cn)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            ' I-reset at i-bind ang DataGridView
            DataGridView1.DataSource = Nothing
            DataGridView1.Columns.Clear()

            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = dt
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        Catch ex As Exception
            MsgBox("Error loading residence list: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' Tiyaking naka-link ito sa DataGridView sa Form Designer
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            ' Kukunin ang ResidentID sa unang column
            SelectedResidentID = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("ResidentID").Value)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub
End Class