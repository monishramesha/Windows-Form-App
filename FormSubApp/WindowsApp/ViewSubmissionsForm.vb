Imports System.Drawing
Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Inherits System.Windows.Forms.Form

    Private submissions As List(Of WorkflowItem)
    Private currentIndex As Integer = 0

    Private WithEvents btnPrevious As System.Windows.Forms.Button
    Private WithEvents btnNext As System.Windows.Forms.Button
    Private lblSubmissionDetails As System.Windows.Forms.Label

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        LoadSubmissions()
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

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplayCurrentSubmission()
        End If
    End Sub

    Private Sub LoadSubmissions()
        Try
            Dim jsonData As String = System.IO.File.ReadAllText("C:\Users\monis\source\repos\FormSubApp\FormSubApp\Backend\src\db.json")
            Dim workflowContainer As WorkflowContainer = JsonConvert.DeserializeObject(Of WorkflowContainer)(jsonData)
            submissions = workflowContainer.Workflows

            If submissions Is Nothing OrElse submissions.Count = 0 Then
                submissions = New List(Of WorkflowItem)()
                lblSubmissionDetails.Text = "No submissions available."
            Else
                DisplayCurrentSubmission()
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading submissions: " & ex.Message)
            submissions = New List(Of WorkflowItem)()
            lblSubmissionDetails.Text = "Error loading submissions."
        End Try
    End Sub

    Private Sub DisplayCurrentSubmission()
        If submissions.Count > 0 AndAlso currentIndex >= 0 AndAlso currentIndex < submissions.Count Then
            Dim submission = submissions(currentIndex)
            lblSubmissionDetails.Text = $"Name: {submission.Name}{Environment.NewLine}" &
                                        $"Email: {submission.Email}{Environment.NewLine}" &
                                        $"Phone: {submission.Phone}{Environment.NewLine}" &
                                        $"GitHub: {submission.GitHubLink}{Environment.NewLine}" &
                                        $"Time: {submission.StopwatchTime}"
        Else
            lblSubmissionDetails.Text = "No submissions available."
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.lblSubmissionDetails = New System.Windows.Forms.Label()
        Me.SuspendLayout()

        ' Footer Label
        Dim lblFooter As New Label With {
            .Text = "Monish R, Slidely Task 2 - View Submissions",
            .Dock = DockStyle.Bottom,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .BackColor = Color.LightGray,
            .ForeColor = Color.Black,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
        }
        Me.Controls.Add(lblFooter)

        ' Previous Button
        Me.btnPrevious.Location = New Point(Me.ClientSize.Width \ 2 + 20, Me.ClientSize.Height)
        Me.btnPrevious.Size = New Size(150, 30)
        Me.btnPrevious.Text = "Previous (Ctrl + P)"
        Me.Controls.Add(btnPrevious)

        ' Next Button
        Me.btnNext.Location = New Point(Me.ClientSize.Width \ 2 + 200, Me.ClientSize.Height)
        Me.btnNext.Size = New Size(150, 30)
        Me.btnNext.Text = "Next (Ctrl + N)"
        Me.Controls.Add(btnNext)

        ' Submission Details Label
        Me.lblSubmissionDetails.Location = New Point(Me.ClientSize.Width, Me.ClientSize.Height - 150)
        Me.lblSubmissionDetails.Size = New Size(300, 150)
        Me.lblSubmissionDetails.Text = "Loading submissions..."
        Me.lblSubmissionDetails.Font = New Font("Segoe UI", 12, FontStyle.Regular)
        Me.lblSubmissionDetails.AutoSize = True
        Me.Controls.Add(lblSubmissionDetails)

        ' ViewSubmissionsForm
        Me.ClientSize = New Size(800, 500)
        Me.Controls.Add(Me.lblSubmissionDetails)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrevious)
        Me.Name = "ViewSubmissionsForm"
        Me.Text = "View Submissions"
        Me.ResumeLayout(False)
    End Sub
End Class

Public Class WorkflowItem
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property GitHubLink As String
    Public Property StopwatchTime As String

    ' Parameterless constructor for serialization
    Public Sub New()
    End Sub

    ' Parameterized constructor for creating new instances
    Public Sub New(Name As String, Email As String, Phone As String, GitHubLink As String, StopwatchTime As String)
        Me.Name = Name
        Me.Email = Email
        Me.Phone = Phone
        Me.GitHubLink = GitHubLink
        Me.StopwatchTime = StopwatchTime
    End Sub
End Class

' Define WorkflowContainer class to hold list of WorkflowItem
Public Class WorkflowContainer
    Public Property Workflows As List(Of WorkflowItem)

    ' Parameterless constructor for serialization
    Public Sub New()
        Workflows = New List(Of WorkflowItem)()
    End Sub

    ' Constructor to initialize the workflows list
    Public Sub New(workflows As List(Of WorkflowItem))
        Me.Workflows = workflows
    End Sub
End Class
