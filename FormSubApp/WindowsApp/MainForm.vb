Public Class MainForm
    Inherits System.Windows.Forms.Form

    Private WithEvents btnViewSubmissions As System.Windows.Forms.Button
    Private WithEvents btnCreateSubmissions As System.Windows.Forms.Button
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
        'MainForm
        '
        Me.ClientSize = New System.Drawing.Size(607, 375)
        Me.Name = "MainForm"
        Me.ResumeLayout(False)

    End Sub
End Class
