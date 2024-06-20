﻿Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic.FileIO
Imports System.Windows.Forms.Design
Imports System.Text

Public Class ViewSubmissionsForm
    Inherits System.Windows.Forms.Form

    Private submissions As List(Of WorkflowItem)
    Private currentIndex As Integer = 0

    Private WithEvents btnPrevious As System.Windows.Forms.Button
    Private WithEvents btnNext As System.Windows.Forms.Button
    Private WithEvents btnDelete As System.Windows.Forms.Button
    Private WithEvents btnEdit As System.Windows.Forms.Button
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

    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If submissions.Count > 0 AndAlso currentIndex >= 0 AndAlso currentIndex < submissions.Count Then
            Dim submissionToDelete = submissions(currentIndex)
            Dim client As New HttpClient()
            Dim response = Await client.DeleteAsync($"http://localhost:3000/delete?email={submissionToDelete.Email}")
            response.EnsureSuccessStatusCode()

            MessageBox.Show("Submission deleted successfully")
            LoadSubmissions()
        Else
            MessageBox.Show("Not submissions selected to delete")
        End If
    End Sub

    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If submissions IsNot Nothing AndAlso currentIndex >= 0 AndAlso currentIndex < submissions.Count Then
            Dim submissionId = submissions(currentIndex).Email


            Dim updatedSubmission As New WorkflowItem() With {
            .Name = txtName.Text,
            .Email = txtEmail.Text,
            .Phone = txtPhone.Text,
            .GitHubLink = txtGithubLink.Text,
            .StopwatchTime = Stopwatch.Elapsed.ToString()
        }

            Try
                Dim client As New HttpClient()
                Dim jsonString As String = JsonConvert.SerializeObject(updatedSubmission)
                Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")

                Dim response = Await client.PutAsync($"http://localhost:3000/edit/{submissionId}", content)
                response.EnsureSuccessStatusCode()

                MessageBox.Show("Submission updated successfully!")
                LoadSubmissions() ' Refresh submissions after update
            Catch ex As Exception
                MessageBox.Show($"Error updating submission: {ex.Message}")
            End Try
        Else
            MessageBox.Show("No submission selected.")
        End If
    End Sub

    Private Async Sub LoadSubmissions()
        Try
            Dim jsonData As String = System.IO.File.ReadAllText("C:\Users\monis\source\repos\FormSubApp\FormSubApp\Backend\src\db.json")
            MessageBox.Show("JSON Data: " & jsonData)
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
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.lblSubmissionDetails = New System.Windows.Forms.Label()
        Me.Controls.Add(Me.lblSubmissionDetails)
        Me.SuspendLayout()

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
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(50, 300)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 30)
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True

        '
        ' btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(200, 300)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(100, 30)
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True

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
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.lblSubmissionDetails)
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