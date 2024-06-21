Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreatNewSubmission_Click(sender As Object, e As EventArgs) Handles btnCreatNewSubmission.Click
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub
End Class
