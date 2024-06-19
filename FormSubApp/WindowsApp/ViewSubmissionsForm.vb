Imports System.ComponentModel
Imports System.Net.Http
Imports NewtonSoft.Json
Public Class ViewSubmissionsForm
    Inherits System.Windows.Forms.Form
    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0
    Private WithEvents btnPrevious As System.Windows.Forms.Button
    Private WithEvents btnNext As System.Windows.Forms.Button

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        LoadSubmissions()
        DisplayCurrentSubmission()
    End Sub

    Private Sub ViewSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplayCurrentSubmission()
        End If
    End Sub

    Private Sub btnNnext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplayCurrentSubmission()
        End If
    End Sub

    Private Async Sub LoadSubmissions()
        'Replace with actual API call logic
        Try
            Dim client As New HttpClient()
            Dim response As HttpResponseMessage = client.GetAsync("http://localhost:3000/read?index=0").Result
            response.EnsureSuccessStatusCode()

            Dim json As String = Await response.Content.ReadAsStringAsync()
            submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(json)

        Catch ex As Exception
            MessageBox.Show("Error loading submissions: " & ex.Message)
            submissions = New List(Of Submission)()
        End Try
    End Sub

    Private Sub DisplayCurrentSubmission()
        If submissions.Count > 0 AndAlso currentIndex >= 0 AndAlso currentIndex < submissions.Count Then
            Dim submission = submissions(currentIndex)
            ' Display submission details
            MessageBox.Show($"Name: {submission.Name}, Email: {submission.Email}, Phone: {submission.Phone}, GitHub: {submission.GitHubLink}, Time: {submission.StopwatchTime}")
        End If
    End Sub
End Class

Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property GitHubLink As String
    Public Property StopwatchTime As String

    Public Sub New(name As String, email As String, phone As String, gitHubLink As String, stopwatchTime As String)
        Me.Name = name
        Me.Email = email
        Me.Phone = phone
        Me.GitHubLink = gitHubLink
        Me.StopwatchTime = stopwatchTime
    End Sub
End Class
