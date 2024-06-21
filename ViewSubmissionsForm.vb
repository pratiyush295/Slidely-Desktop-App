Imports Newtonsoft.Json

Public Class ViewSubmissionsForm

    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FetchSubmissions()
        DisplaySubmission()
    End Sub

    Private Sub FetchSubmissions()
        Try
            Dim httpClient As New System.Net.Http.HttpClient()
            Dim response As System.Net.Http.HttpResponseMessage = httpClient.GetAsync("http://localhost:3000/submissions").Result
            If response.IsSuccessStatusCode Then
                Dim json As String = response.Content.ReadAsStringAsync().Result
                submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(json)
            Else
                MessageBox.Show("Failed to fetch submissions. Please try again.")
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
    End Sub

    Private Sub DisplaySubmission()
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim submission As Submission = submissions(currentIndex)
            txtName.Text = submission.name
            txtEmail.Text = submission.email
            txtPhone.Text = submission.phone
            txtGithubLink.Text = submission.githubLink
            lblStopwatchTime.Text = submission.stopwatchTime
        Else
            MessageBox.Show("No submissions found.")
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission()
        Else
            MessageBox.Show("This is the first submission.")
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission()
        Else
            MessageBox.Show("This is the last submission.")
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.P) Then
            btnPrevious.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.N) Then
            btnNext.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles txtGithubLink.TextChanged

    End Sub

    Private Sub textPhone_TextChanged(sender As Object, e As EventArgs) Handles txtPhone.TextChanged

    End Sub

    Private Sub textEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged

    End Sub
End Class

Public Class Submission
    Public Property name As String
    Public Property email As String
    Public Property phone As String
    Public Property githubLink As String
    Public Property stopwatchTime As String
End Class
