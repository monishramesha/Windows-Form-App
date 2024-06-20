Imports System.Drawing
Imports System.Windows.Forms

Public Class MainForm
    Inherits System.Windows.Forms.Form

    Private WithEvents btnViewSubmissions As System.Windows.Forms.Button
    Private WithEvents btnCreateSubmissions As System.Windows.Forms.Button
    Private lblFooter As System.Windows.Forms.Label

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
        viewForm.Show()
    End Sub

    Private Sub btnCreateSubmissions_Click(sender As Object, e As EventArgs) Handles btnCreateSubmissions.Click
        Dim createForm As New CreateSubmissionsForm()
        createForm.Show()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()

        ' Footer Label
        Me.lblFooter = New Label With {
          .Text = "Monish R, Slidely Task 2 - Slidely Form App",
          .Dock = DockStyle.Bottom,
          .AutoSize = False,
          .TextAlign = ContentAlignment.MiddleCenter,
          .BackColor = Color.LightGray,
          .ForeColor = Color.Black,
          .Font = New Font("Segoe UI", 9, FontStyle.Bold)
        }
        Me.Controls.Add(lblFooter)

        ' View Submissions Button
        Me.btnViewSubmissions = New Button With {
          .Location = New Point(Me.ClientSize.Width - 150, Me.ClientSize.Height \ 2 - 25),
          .Size = New Size(180, 50),
          .Text = "View Submissions (Ctrl + V)",
          .BackColor = Color.FromArgb(12, 191, 233),
          .Font = New Font("Segoe UI", 9, FontStyle.Regular),
          .FlatStyle = FlatStyle.Flat
        }
        Me.Controls.Add(btnViewSubmissions)

        ' Create Submissions Button
        Me.btnCreateSubmissions = New Button With {
          .Location = New Point(Me.ClientSize.Width + 150, Me.ClientSize.Height \ 2 - 25),
          .Size = New Size(180, 50),
          .Text = "Create Submission (Ctrl + C)",
          .BackColor = Color.FromArgb(12, 191, 233),
          .Font = New Font("Segoe UI", 9, FontStyle.Regular),
          .FlatStyle = FlatStyle.Flat
        }
        Me.Controls.Add(btnCreateSubmissions)

        ' MainForm
        Me.ClientSize = New Size(800, 500)
        Me.Text = "Slidely Form App"
        Me.Name = "MainForm"
        Me.ResumeLayout(False)

        ' Set the FlatAppearance properties after initialization
        btnViewSubmissions.FlatStyle = FlatStyle.Flat
        btnViewSubmissions.FlatAppearance.BorderColor = Color.LightGray
        btnViewSubmissions.FlatAppearance.BorderSize = 2
        btnViewSubmissions.TabStop = True

        btnCreateSubmissions.FlatStyle = FlatStyle.Flat
        btnCreateSubmissions.FlatAppearance.BorderColor = Color.LightGray
        btnCreateSubmissions.FlatAppearance.BorderSize = 2
        btnCreateSubmissions.TabStop = True
    End Sub

    ' Custom method to draw flat button border
    Private Sub PaintFlatBorder(ByVal sender As Object, ByVal e As PaintEventArgs)
        Dim button As Button = CType(sender, Button)
        ControlPaint.DrawBorder(e.Graphics, button.ClientRectangle, button.FlatAppearance.BorderColor, ButtonBorderStyle.Solid)
    End Sub

End Class
