Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text


Public Class CreateSubmissionsForm
    Inherits System.Windows.Forms.Form
    Private stopwatch As Stopwatch = New Stopwatch()
    Private WithEvents btnToggleStopwatch As System.Windows.Forms.Button
    Private WithEvents btnSubmit As System.Windows.Forms.Button
    Private WithEvents txtName As System.Windows.Forms.TextBox
    Private WithEvents txtEmail As System.Windows.Forms.TextBox
    Private WithEvents txtPhone As System.Windows.Forms.TextBox
    Private WithEvents txtGithubLink As System.Windows.Forms.TextBox

    Private Sub CreateSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub CreateSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.T Then
            btnToggleStopwatch.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        End If
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatch.IsRunning Then
            stopwatch.Stop()
        Else
            stopwatch.Start()
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim name = txtName.Text
        Dim email = txtEmail.Text
        Dim phone = txtPhone.Text
        Dim githubLink = txtGithubLink.Text
        Dim stopwatchTime = stopwatch.Elapsed.ToString()

        ' Validate input fields
        If String.IsNullOrWhiteSpace(name) OrElse String.IsNullOrWhiteSpace(email) Then
            MessageBox.Show("Name and Email are required.")
            Return
        End If

        'Create Submission object
        Dim submission = New Submission(name, email, phone, githubLink, stopwatchTime)

        'Send submission to backend
        Try
            Dim client As New HttpClient()
            Dim jsonString As String = JsonConvert.SerializeObject(submission)
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

            Dim response = client.PostAsync("http://localhost:3000/submit", content).Result
            response.EnsureSuccessStatusCode()

            MessageBox.Show("Submission saved successfully!")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error submitting form: " & ex.Message)
        End Try

    End Sub
End Class
