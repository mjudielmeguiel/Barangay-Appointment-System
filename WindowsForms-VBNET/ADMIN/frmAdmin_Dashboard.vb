Imports MySql.Data.MySqlClient

Public Class frmAdmin_Dashboard
    Private Sub frmAdmin_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardStats()
    End Sub

    Private Sub LoadDashboardStats()
        Try
            connection()

            ' ======================================
            ' 1. TOTAL RESIDENT RECORDS
            ' ======================================
            Dim totalResidents As Integer = 0
            sql = "SELECT COUNT(*) FROM residences"
            cmd = New MySqlCommand(sql, cn)
            totalResidents = Convert.ToInt32(cmd.ExecuteScalar())
            Label14.Text = totalResidents.ToString()

            ' ======================================
            ' 2. ACTIVE RESIDENTS
            ' ======================================
            Dim activeResidents As Integer = 0
            sql = "SELECT COUNT(*) FROM residences WHERE AccountStatus = 'Active'"
            cmd = New MySqlCommand(sql, cn)
            activeResidents = Convert.ToInt32(cmd.ExecuteScalar())
            Label12.Text = activeResidents.ToString()

            ' ======================================
            ' 3. LOCKED / DISABLED ACCOUNTS
            ' ======================================
            Dim lockedAccounts As Integer = 0
            sql = "SELECT COUNT(*) FROM residences WHERE AccountStatus = 'Locked' OR AccountStatus = 'Disabled'"
            cmd = New MySqlCommand(sql, cn)
            lockedAccounts = Convert.ToInt32(cmd.ExecuteScalar())
            lblLockedAccounts.Text = lockedAccounts.ToString()

            ' ======================================
            ' 4. TOTAL APPOINTMENTS
            ' ======================================
            Dim totalAppointments As Integer = 0
            ' Kung iba ang pangalan ng table mo, palitan mo rito
            sql = "SELECT COUNT(*) FROM appointments"
            cmd = New MySqlCommand(sql, cn)
            totalAppointments = Convert.ToInt32(cmd.ExecuteScalar())
            lblTotalAppointments.Text = totalAppointments.ToString()

            ' ======================================
            ' 5. PENDING APPOINTMENTS
            ' ======================================
            Dim pendingAppointments As Integer = 0
            sql = "SELECT COUNT(*) FROM appointments WHERE Status = 'Pending'"
            cmd = New MySqlCommand(sql, cn)
            pendingAppointments = Convert.ToInt32(cmd.ExecuteScalar())
            lblPendingAppointments.Text = pendingAppointments.ToString()

        Catch ex As Exception
            MsgBox("Error loading dashboard: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub
End Class