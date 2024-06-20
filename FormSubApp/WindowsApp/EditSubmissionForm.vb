Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text

Public Class EditSubmissionForm
    Inherits System.Windows.Forms.Form

    Private submission As WorkflowItem
    Private WithEvents btnSubmitEdit As System.Windows.Forms.Button
    Private WithEvents txtName As System.Windows.Forms.TextBox
    Private WithEvents txtEmail As System.Windows.Forms.TextBox
    Private WithEvents txtPhone As System.Windows.Forms.TextBox
    Private WithEvents txtGithubLink As System.Windows.Forms.TextBox

    Public Sub New(submission As WorkflowItem)
        Me.submission = submission
        InitializeComponent()
        LoadSubmissionDetails()
    End Sub

    Private Sub LoadSubmissionDetails()
        txtName.Text = submission.Name
        txtEmail.Text = submission.Email
        txtPhone.Text = submission.Phone
        txtGithubLink.Text = submission.GitHubLink
    End Sub

    Private Async Sub btnSubmitEdit_Click(sender As Object, e As EventArgs) Handles btnSubmitEdit.Click
        submission.Name = txtName.Text
        submission.Email = txtEmail.Text
        submission.Phone = txtPhone.Text
        submission.GitHubLink = txtGithubLink.Text

        Dim client As New HttpClient()
        Dim json As String = JsonConvert.SerializeObject(submission)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")
        Dim response = Await client.PutAsync("http://localhost:3000/update", content)
        response.EnsureSuccessStatusCode()

        MessageBox.Show("Submission updated successfully!")
        Me.Close()
    End Sub

    Private Sub InitializeComponent()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtGithubLink = New System.Windows.Forms.TextBox()
        Me.btnSubmitEdit = New System.Windows.Forms.Button()

        Me.SuspendLayout()

        ' 
        ' txtName
        ' 
        Me.txtName.Location = New System.Drawing.Point(50, 50)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(200, 22)

        ' 
        ' txtEmail
        ' 
        Me.txtEmail.Location = New System.Drawing.Point(50, 100)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(200, 22)

        ' 
        ' txtPhone
        ' 
        Me.txtPhone.Location = New System.Drawing.Point(50, 150)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(200, 22)

        ' 
        ' txtGithubLink
        ' 
        Me.txtGithubLink.Location = New System.Drawing.Point(50, 200)
        Me.txtGithubLink.Name = "txtGithubLink"
        Me.txtGithubLink.Size = New System.Drawing.Size(200, 22)

        ' 
        ' btnSubmitEdit
        ' 
        Me.btnSubmitEdit.Location = New System.Drawing.Point(50, 250)
        Me.btnSubmitEdit.Name = "btnSubmitEdit"
        Me.btnSubmitEdit.Size = New System.Drawing.Size(100, 30)
        Me.btnSubmitEdit.Text = "Submit"
        Me.btnSubmitEdit.UseVisualStyleBackColor = True

        ' 
        ' EditSubmissionForm
        ' 
        Me.ClientSize = New System.Drawing.Size(300, 300)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtGithubLink)
        Me.Controls.Add(Me.btnSubmitEdit)
        Me.Name = "EditSubmissionForm"
        Me.Text = "Edit Submission"
    End Sub
End Class
