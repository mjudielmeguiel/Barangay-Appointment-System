Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmAuthorizationLetter

    Private targetControlNo As String = ""

    ' I-modify natin ang constructor para tanggapin ang ControlNo galing sa main form
    Public Sub New(ByVal controlNo As String)
        InitializeComponent()
        targetControlNo = controlNo
    End Sub

    Private Sub frmAuthorizationLetter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Representative Details - " & targetControlNo
        LoadRepresentativeDetails()
    End Sub

    Private Sub LoadRepresentativeDetails()
        If String.IsNullOrEmpty(targetControlNo) Then Return

        Try
            connection() ' Siguraduhing naka-access dito yung public module/method ng database connection niyo

            ' Isinama na natin ang RepresentativeName at RepresentativeIDCard sa SELECT statement
            Dim sql As String = "SELECT RepresentativeName, AuthorizationLetter, RepresentativeIDCard FROM appointments WHERE ControlNo = @ctrl"
            Dim cmd As New MySqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@ctrl", targetControlNo)

            Dim dr As MySqlDataReader = cmd.ExecuteReader()

            If dr.Read() Then
                ' 1. I-display ang Representative Name
                If lblRepName IsNot Nothing Then
                    lblRepName.Text = If(IsDBNull(dr("RepresentativeName")), "N/A", dr("RepresentativeName").ToString())
                End If

                ' 2. I-display ang Authorization Letter
                If Not IsDBNull(dr("AuthorizationLetter")) AndAlso picAuthLetter IsNot Nothing Then
                    DisplayImage(CType(dr("AuthorizationLetter"), Byte()), picAuthLetter)
                End If

                ' 3. I-display ang Representative ID Card
                If Not IsDBNull(dr("RepresentativeIDCard")) AndAlso picRepID IsNot Nothing Then
                    DisplayImage(CType(dr("RepresentativeIDCard"), Byte()), picRepID)
                End If
            Else
                MsgBox("Walang nakitang record para sa appointment na ito.", MsgBoxStyle.Information, "No Record")
            End If
            dr.Close()

        Catch ex As Exception
            MsgBox("Error loading Representative Details: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            CloseConnection() ' I-close ang connection palagi pagtapos
        End Try
    End Sub

    ' Helper function para malinis ang code natin sa paglo-load ng images
    Private Sub DisplayImage(imgBytes As Byte(), picBox As PictureBox)
        If imgBytes IsNot Nothing AndAlso imgBytes.Length > 0 Then
            Using ms As New MemoryStream(imgBytes)
                picBox.SizeMode = PictureBoxSizeMode.Zoom
                If picBox.Image IsNot Nothing Then picBox.Image.Dispose()
                picBox.Image = Image.FromStream(ms)
            End Using
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class