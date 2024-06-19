Public Class MainForm
    Inherits System.Windows.Forms.Form

    Private WithEvents btnViewSubmissions As System.Windows.Forms.Button
    Private WithEvents btnCreateSubmissions As System.Windows.Forms.Button

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateSubmissions.PerformClick()
        End If
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim viewForm As New ViewSubmissionsForm()
        ViewSubmissionsForm.Show()
    End Sub

    Private Sub btnCreateSubmissions_Click(sender As Object, e As EventArgs) Handles btnCreateSubmissions.Click
        Dim createForm As New CreateSubmissionsForm()
        createForm.Show()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'btnviewSubmissions
        '
        Me.btnViewSubmissions = New System.Windows.Forms.Button()
        Me.btnViewSubmissions.Location = New System.Drawing.Point(50, 50)
        Me.btnViewSubmissions.Name = "btnViewSubmissions"
        Me.btnViewSubmissions.Size = New System.Drawing.Size(150, 50)
        Me.btnViewSubmissions.Text = "View Submissions"
        '
        'btnCreateSubmissions
        '
        Me.btnCreateSubmissions = New System.Windows.Forms.Button()
        Me.btnCreateSubmissions.Location = New System.Drawing.Point(250, 50)
        Me.btnCreateSubmissions.Name = "btnCreateSubmissions"
        Me.btnCreateSubmissions.Size = New System.Drawing.Size(150, 50)
        Me.btnCreateSubmissions.Text = "Create Submission"
        '
        'MainForm
        '
        Me.ClientSize = New System.Drawing.Size(607, 375)
        Me.Controls.Add(Me.btnViewSubmissions)
        Me.Controls.Add(Me.btnCreateSubmissions)
        Me.Name = "MainForm"
        Me.ResumeLayout(False)

    End Sub
End Class
