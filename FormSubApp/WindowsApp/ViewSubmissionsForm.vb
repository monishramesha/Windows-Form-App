Imports System.ComponentModel
Imports System.Net.Http
Imports NewtonSoft.Json
Public Class ViewSubmissionsForm
    Inherits System.Windows.Forms.Form

    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0

    Private WithEvents btnPrevious As System.Windows.Forms.Button
    Private WithEvents btnNext As System.Windows.Forms.Button
    Private lblSubmissionDetails As System.Windows.Forms.Label

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
        Try
            Dim client As New HttpClient()
            Dim response As HttpResponseMessage = client.GetAsync("http://localhost:3000/read?index=0").Result
            response.EnsureSuccessStatusCode()

            Dim json As String = Await response.Content.ReadAsStringAsync()
            submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(json)

            If submissions Is Nothing Then
                submissions = New List(Of Submission)()
            End If

            DisplayCurrentSubmission()
        Catch ex As Exception
            MessageBox.Show("Error loading submissions: " & ex.Message)
            submissions = New List(Of Submission)()
        End Try
    End Sub

    Private Sub DisplayCurrentSubmission()
        If submissions.Count > 0 AndAlso currentIndex >= 0 AndAlso currentIndex < submissions.Count Then
            Dim submission = submissions(currentIndex)
            ' Display submission details
            lblSubmissionDetails.Text = $"Name: {submission.Name}{Environment.NewLine}" &
                                        $"Email: {submission.Email}{Environment.NewLine}" &
                                        $"Phone: {submission.Phone}{Environment.NewLine}" &
                                        $"GitHub: {submission.GitHubLink}{Environment.NewLine}" &
                                        $"Time: {submission.StopwatchTime}"
            'MessageBox.Show($"Name: {submission.Name}, Email: {submission.Email}, Phone: {submission.Phone}, GitHub: {submission.GitHubLink}, Time: {submission.StopwatchTime}")
        Else
            lblSubmissionDetails.Text = "No submissions available."
        End If
    End Sub
    Private Sub InitializeComponent()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.lblSubmissionDetails = New System.Windows.Forms.Label()

        ' 
        ' btnPrevious
        ' 
        Me.btnPrevious.Location = New System.Drawing.Point(50, 250)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(100, 30)
        Me.btnPrevious.Text = "Previous"
        Me.btnPrevious.UseVisualStyleBackColor = True

        ' 
        ' btnNext
        ' 
        Me.btnNext.Location = New System.Drawing.Point(200, 250)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(100, 30)
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True

        ' 
        ' lblSubmissionDetails
        ' 
        Me.lblSubmissionDetails.Location = New System.Drawing.Point(50, 50)
        Me.lblSubmissionDetails.Name = "lblSubmissionDetails"
        Me.lblSubmissionDetails.Size = New System.Drawing.Size(400, 150)
        Me.lblSubmissionDetails.Text = "Loading submissions..."
        Me.lblSubmissionDetails.AutoSize = True

        ' 
        ' ViewSubmissionsForm
        ' 
        Me.ClientSize = New System.Drawing.Size(600, 400)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.lblSubmissionDetails)
        Me.Name = "ViewSubmissionsForm"
        Me.Text = "View Submissions"
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
