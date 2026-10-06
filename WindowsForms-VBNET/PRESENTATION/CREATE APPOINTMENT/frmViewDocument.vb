Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmViewDocument
    Private controlNumber As String = ""
    Private residentExportName As String = "Resident"

    Public Sub New(ByVal ctrlNo As String)
        InitializeComponent()
        controlNumber = ctrlNo
    End Sub

    Private Sub frmViewDocument_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configure RichTextBox font style base
        RichTextBox1.Font = New Font("Segoe UI", 14, FontStyle.Regular)
        RichTextBox1.ReadOnly = True

        LoadDocumentDetails()
    End Sub

    Private Sub LoadDocumentDetails()
        Try
            If cn.State <> ConnectionState.Open Then connection()

            ' Query joining appointments, residences, and document services/purposes
            sql = "SELECT a.*, r.* FROM appointments a " &
                  "LEFT JOIN residences r ON a.ResidentID = r.ResidentID " &
                  "WHERE a.ControlNo = @ctrl"

            cmd = New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ctrl", controlNumber)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                Dim fullName As String = If(IsDBNull(dr("FullName")), "N/A", dr("FullName").ToString().Trim())

                ' Set clean filename format: ResidentName_Date
                residentExportName = fullName.Replace(" ", "_")

                Dim bday As String = If(IsDBNull(dr("Birthday")), "N/A", Convert.ToDateTime(dr("Birthday")).ToString("MMMM dd, yyyy"))
                Dim bplace As String = If(IsDBNull(dr("BirthPlace")), "N/A", dr("BirthPlace").ToString().Trim())
                Dim gender As String = If(IsDBNull(dr("Gender")), "N/A", dr("Gender").ToString().Trim())
                Dim civilStatus As String = If(IsDBNull(dr("CivilStatus")), "N/A", dr("CivilStatus").ToString().Trim())
                Dim address As String = If(IsDBNull(dr("FullAddress")), If(IsDBNull(dr("Address")), "N/A", dr("Address").ToString().Trim()), dr("FullAddress").ToString().Trim())
                Dim mobile As String = If(IsDBNull(dr("MobileNumber")), "N/A", dr("MobileNumber").ToString().Trim())
                Dim email As String = If(IsDBNull(dr("Email")), "N/A", dr("Email").ToString().Trim())
                Dim father As String = If(IsDBNull(dr("FatherName")), "N/A", dr("FatherName").ToString().Trim())
                Dim mother As String = If(IsDBNull(dr("MotherName")), "N/A", dr("MotherName").ToString().Trim())

                Dim requestFor As String = If(IsDBNull(dr("RequestFor")), "N/A", dr("RequestFor").ToString().Trim())
                Dim repName As String = If(IsDBNull(dr("RepresentativeName")), "N/A", dr("RepresentativeName").ToString().Trim())
                Dim requestType As String = If(IsDBNull(dr("RequestType")), "N/A", dr("RequestType").ToString().Trim())
                Dim purpose As String = If(IsDBNull(dr("Purpose")), "N/A", dr("Purpose").ToString().Trim())
                Dim department As String = If(IsDBNull(dr("Department")), "N/A", dr("Department").ToString().Trim())
                Dim status As String = If(IsDBNull(dr("Status")), "N/A", dr("Status").ToString().Trim())

                RichTextBox1.Clear()

                ' Helper lambda to write Bold Labels and Regular Values
                Dim AppendField = Sub(label As String, value As String)
                                      RichTextBox1.SelectionFont = New Font("Segoe UI", 14, FontStyle.Bold)
                                      RichTextBox1.AppendText(label & " ")

                                      RichTextBox1.SelectionFont = New Font("Segoe UI", 14, FontStyle.Regular)
                                      RichTextBox1.AppendText(value & vbCrLf)
                                  End Sub

                ' Populate Fields with Bold Labels
                AppendField("Control No:", controlNumber)
                AppendField("Name:", fullName)
                AppendField("Date of Birth:", bday)
                AppendField("Place of Birth:", bplace)
                AppendField("Gender:", gender)
                AppendField("Civil Status:", civilStatus)
                AppendField("Address:", address)
                AppendField("Mobile Number:", mobile)
                AppendField("Email Address:", email)
                AppendField("Father's Name:", father)
                AppendField("Mother's Name:", mother)
                AppendField("Request For:", requestFor)

                If requestFor <> "Self" AndAlso requestFor <> "N/A" Then
                    AppendField("Representative Name:", repName)
                End If

                AppendField("Document Type / Request Type:", requestType)
                AppendField("Department:", department)
                AppendField("Purpose:", purpose)
                AppendField("Status:", status)

            Else
                RichTextBox1.SelectionFont = New Font("Segoe UI", 14, FontStyle.Regular)
                RichTextBox1.Text = "No details found for Control Number: " & controlNumber
            End If
            dr.Close()

        Catch ex As Exception
            MsgBox("Error loading document details: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            CloseConnection()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnExportExcel_Click_1(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Using sfd As New SaveFileDialog()
            sfd.FileName = $"{residentExportName}_{DateTime.Now.ToString("yyyy-MM-dd")}.csv"
            sfd.Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*"
            sfd.Title = "Save Document Details as Excel CSV"

            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    ' Build structured rows and columns (Column 1 = Label, Column 2 = Value)
                    Dim csvContent As New System.Text.StringBuilder()

                    ' Helper to add rows separated by commas
                    Dim AddRow = Sub(label As String, val As String)
                                     ' Enclose values in quotes to handle spaces or commas safely in Excel
                                     csvContent.AppendLine($"""{label}"",""{val}""")
                                 End Sub

                    ' Fetch data directly into structured cells
                    If cn.State <> ConnectionState.Open Then connection()
                    sql = "SELECT a.*, r.* FROM appointments a LEFT JOIN residences r ON a.ResidentID = r.ResidentID WHERE a.ControlNo = @ctrl"
                    cmd = New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@ctrl", controlNumber)
                    dr = cmd.ExecuteReader()

                    If dr.Read() Then
                        Dim fullName As String = If(IsDBNull(dr("FullName")), "N/A", dr("FullName").ToString().Trim())
                        Dim bday As String = If(IsDBNull(dr("Birthday")), "N/A", Convert.ToDateTime(dr("Birthday")).ToString("MMMM dd, yyyy"))
                        Dim bplace As String = If(IsDBNull(dr("BirthPlace")), "N/A", dr("BirthPlace").ToString().Trim())
                        Dim gender As String = If(IsDBNull(dr("Gender")), "N/A", dr("Gender").ToString().Trim())
                        Dim civilStatus As String = If(IsDBNull(dr("CivilStatus")), "N/A", dr("CivilStatus").ToString().Trim())
                        Dim address As String = If(IsDBNull(dr("FullAddress")), If(IsDBNull(dr("Address")), "N/A", dr("Address").ToString().Trim()), dr("FullAddress").ToString().Trim())
                        Dim mobile As String = If(IsDBNull(dr("MobileNumber")), "N/A", dr("MobileNumber").ToString().Trim())
                        Dim email As String = If(IsDBNull(dr("Email")), "N/A", dr("Email").ToString().Trim())
                        Dim father As String = If(IsDBNull(dr("FatherName")), "N/A", dr("FatherName").ToString().Trim())
                        Dim mother As String = If(IsDBNull(dr("MotherName")), "N/A", dr("MotherName").ToString().Trim())

                        Dim requestFor As String = If(IsDBNull(dr("RequestFor")), "N/A", dr("RequestFor").ToString().Trim())
                        Dim repName As String = If(IsDBNull(dr("RepresentativeName")), "N/A", dr("RepresentativeName").ToString().Trim())
                        Dim requestType As String = If(IsDBNull(dr("RequestType")), "N/A", dr("RequestType").ToString().Trim())
                        Dim purpose As String = If(IsDBNull(dr("Purpose")), "N/A", dr("Purpose").ToString().Trim())
                        Dim department As String = If(IsDBNull(dr("Department")), "N/A", dr("Department").ToString().Trim())
                        Dim status As String = If(IsDBNull(dr("Status")), "N/A", dr("Status").ToString().Trim())

                        ' Map fields into separate columns (Column A, Column B)
                        AddRow("Control No:", controlNumber)
                        AddRow("Name:", fullName)
                        AddRow("Date of Birth:", bday)
                        AddRow("Place of Birth:", bplace)
                        AddRow("Gender:", gender)
                        AddRow("Civil Status:", civilStatus)
                        AddRow("Address:", address)
                        AddRow("Mobile Number:", mobile)
                        AddRow("Email Address:", email)
                        AddRow("Father's Name:", father)
                        AddRow("Mother's Name:", mother)
                        AddRow("Request For:", requestFor)

                        If requestFor <> "Self" AndAlso requestFor <> "N/A" Then
                            AddRow("Representative Name:", repName)
                        End If

                        AddRow("Document Type / Request Type:", requestType)
                        AddRow("Department:", department)
                        AddRow("Purpose:", purpose)
                        AddRow("Status:", status)
                    End If
                    dr.Close()

                    ' Write to CSV file format readable cleanly by Excel
                    File.WriteAllText(sfd.FileName, csvContent.ToString(), System.Text.Encoding.UTF8)
                    MsgBox("Successfully exported structured Excel columns!", MsgBoxStyle.Information)

                Catch ex As Exception
                    MsgBox("Error exporting file: " & ex.Message, MsgBoxStyle.Critical)
                Finally
                    CloseConnection()
                End Try
            End If
        End Using
    End Sub
End Class