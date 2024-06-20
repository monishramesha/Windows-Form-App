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
    Private WithEvents lblName As System.Windows.Forms.Label
    Private WithEvents lblEmail As System.Windows.Forms.Label
    Private WithEvents lblPhone As System.Windows.Forms.Label
    Private WithEvents lblGithubLink As System.Windows.Forms.Label
    Private WithEvents lblStopwatch As System.Windows.Forms.Label

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
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.lblGithubLink = New System.Windows.Forms.Label()
        Me.lblStopwatch = New System.Windows.Forms.Label()
        Me.SuspendLayout()

        Dim lblFooter As New Label With {
            .Text = "Monish R, Slidely Task 2 - Create Submission",
            .Dock = DockStyle.Bottom,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .BackColor = Color.LightGray,
            .ForeColor = Color.Black,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
        }
        Me.Controls.Add(lblFooter)

        ' btnToggleStopwatch
        Me.btnToggleStopwatch.Location = New System.Drawing.Point(100, 200)
        Me.btnToggleStopwatch.Name = "btnToggleStopwatch"
        Me.btnToggleStopwatch.Size = New System.Drawing.Size(150, 30)
        Me.btnToggleStopwatch.TabIndex = 0
        Me.btnToggleStopwatch.Text = "Toggle Stopwatch"

        ' btnSubmit
        Me.btnSubmit.Location = New System.Drawing.Point(100, 250)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(400, 40) ' Extended width
        Me.btnSubmit.TabIndex = 1
        Me.btnSubmit.Text = "Submit"

        ' txtName
        Me.txtName.Location = New System.Drawing.Point(200, 50)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(300, 22)
        Me.txtName.TabIndex = 2

        ' txtEmail
        Me.txtEmail.Location = New System.Drawing.Point(200, 80)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(300, 22)
        Me.txtEmail.TabIndex = 3

        ' txtPhone
        Me.txtPhone.Location = New System.Drawing.Point(200, 110)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(300, 22)
        Me.txtPhone.TabIndex = 4

        ' txtGithubLink
        Me.txtGithubLink.Location = New System.Drawing.Point(200, 140)
        Me.txtGithubLink.Name = "txtGithubLink"
        Me.txtGithubLink.Size = New System.Drawing.Size(300, 22)
        Me.txtGithubLink.TabIndex = 5

        ' lblName
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(100, 53)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(49, 17)
        Me.lblName.Text = "Name:"

        ' lblEmail
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(100, 83)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(46, 17)
        Me.lblEmail.Text = "Email:"

        ' lblPhone
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Location = New System.Drawing.Point(100, 113)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(53, 17)
        Me.lblPhone.Text = "Phone:"

        ' lblGithubLink
        Me.lblGithubLink.AutoSize = True
        Me.lblGithubLink.Location = New System.Drawing.Point(100, 143)
        Me.lblGithubLink.Name = "lblGithubLink"
        Me.lblGithubLink.Size = New System.Drawing.Size(88, 17)
        Me.lblGithubLink.Text = "GitHub Link:"

        ' lblStopwatch
        Me.lblStopwatch.AutoSize = True
        Me.lblStopwatch.Location = New System.Drawing.Point(100, 173)
        Me.lblStopwatch.Name = "lblStopwatch"
        Me.lblStopwatch.Size = New System.Drawing.Size(83, 17)
        Me.lblStopwatch.Text = "00:00:00.000"

        ' Timer to update stopwatch display
        Dim timer As New Timer()
        timer.Interval = 100 ' Update every 100 milliseconds
        AddHandler timer.Tick, AddressOf UpdateStopwatchLabel
        timer.Start()

        ' CreateSubmissionsForm
        Me.ClientSize = New System.Drawing.Size(600, 350)
        Me.Controls.Add(Me.btnToggleStopwatch)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtGithubLink)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.lblPhone)
        Me.Controls.Add(Me.lblGithubLink)
        Me.Controls.Add(Me.lblStopwatch)
        Me.Name = "CreateSubmissionsForm"
        Me.Text = "Create Submission"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Private Sub UpdateStopwatchLabel(sender As Object, e As EventArgs)
        lblStopwatch.Text = If(stopwatch.IsRunning, stopwatch.Elapsed.ToString("hh\:mm\:ss\.fff"), stopwatch.Elapsed.ToString("hh\:mm\:ss\.fff"))
    End Sub
End Class

