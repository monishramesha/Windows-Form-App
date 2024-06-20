' Imports necessary for HTTP requests and JSON serialization
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text

' Define the class that matches the TypeScript WorkFlowItem structure
Public Class WorkFlowItemVB
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property GitHubLink As String
    Public Property StopwatchTime As String

    ' Default constructor required for serialization
    Public Sub New()
    End Sub

    ' Additional constructor to initialize all properties
    Public Sub New(name As String, email As String, phone As String, githubLink As String, stopwatchTime As String)
        Me.Name = name
        Me.Email = email
        Me.Phone = phone
        Me.GitHubLink = githubLink
        Me.StopwatchTime = stopwatchTime
    End Sub
End Class

Public Class CreateSubmissionsForm
    Inherits System.Windows.Forms.Form
    Private stopwatch As Stopwatch = New Stopwatch()

    Private WithEvents btnToggleStopwatch As System.Windows.Forms.Button
    Private WithEvents btnSubmit As System.Windows.Forms.Button
    Private WithEvents txtName As System.Windows.Forms.TextBox
    Private WithEvents txtEmail As System.Windows.Forms.TextBox
    Private WithEvents txtPhone As System.Windows.Forms.TextBox
    Private WithEvents txtGithubLink As System.Windows.Forms.TextBox

    Public Sub New()
        InitializeComponent()
    End Sub

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

        ' Create WorkFlowItemVB object
        Dim submission As New WorkFlowItemVB(name, email, phone, githubLink, stopwatchTime)

        ' Send submission to backend
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

    Private Sub InitializeComponent()
        Me.btnToggleStopwatch = New System.Windows.Forms.Button()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtGithubLink = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()

        ' btnToggleStopwatch
        Me.btnToggleStopwatch.Location = New System.Drawing.Point(100, 200)
        Me.btnToggleStopwatch.Name = "btnToggleStopwatch"
        Me.btnToggleStopwatch.Size = New System.Drawing.Size(150, 30)
        Me.btnToggleStopwatch.TabIndex = 0
        Me.btnToggleStopwatch.Text = "Toggle Stopwatch"

        ' btnSubmit
        Me.btnSubmit.Location = New System.Drawing.Point(300, 200)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(150, 30)
        Me.btnSubmit.TabIndex = 1
        Me.btnSubmit.Text = "Submit"

        ' txtName
        Me.txtName.Location = New System.Drawing.Point(102, 50)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(350, 22)
        Me.txtName.TabIndex = 2
        Me.txtName.Text = "Enter Name"

        ' txtEmail
        Me.txtEmail.Location = New System.Drawing.Point(102, 80)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(350, 22)
        Me.txtEmail.TabIndex = 3
        Me.txtEmail.Text = "Enter Email"

        ' txtPhone
        Me.txtPhone.Location = New System.Drawing.Point(102, 110)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(350, 22)
        Me.txtPhone.TabIndex = 4
        Me.txtPhone.Text = "Enter Phone"

        ' txtGithubLink
        Me.txtGithubLink.Location = New System.Drawing.Point(102, 140)
        Me.txtGithubLink.Name = "txtGithubLink"
        Me.txtGithubLink.Size = New System.Drawing.Size(350, 22)
        Me.txtGithubLink.TabIndex = 5
        Me.txtGithubLink.Text = "Enter GitHub Link"

        ' CreateSubmissionsForm
        Me.ClientSize = New System.Drawing.Size(600, 300)
        Me.Controls.Add(Me.btnToggleStopwatch)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtGithubLink)
        Me.Name = "CreateSubmissionsForm"
        Me.Text = "Create Submission"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
End Class
