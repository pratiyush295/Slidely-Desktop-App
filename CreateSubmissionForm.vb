Imports Newtonsoft.Json

Public Class CreateSubmissionForm
    Private Sub Timer1_Tick(sender As Object, e As EventArgs)

    End Sub
    Private stopwatchRunning As Boolean = False
    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatchRunning Then
            Timer1.Stop()
            stopwatchRunning = False
            btnToggleStopwatch.Text = "RESUME STOPWATCH (Ctrl + T)"
        Else
            Timer1.Start()
            stopwatchRunning = True
            btnToggleStopwatch.Text = "PAUSE STOPWATCH (Ctrl + T)"
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Collect data and send it to the backend
        Dim name As String = txtName.Text
        Dim email As String = txtEmail.Text
        Dim phone As String = txtPhone.Text
        Dim githubLink As String = txtGithubLink.Text
        Dim stopwatchTime As String = lblStopwatchTime.Text

        ' Send data to backend (implement HTTP request)
        Dim httpClient As New System.Net.Http.HttpClient()
        Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(New With {name, email, phone, githubLink, stopwatchTime})
        Dim content As New System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json")

        Dim response As System.Net.Http.HttpResponseMessage = httpClient.PostAsync("http://localhost:3000/submissions", content).Result
        If response.IsSuccessStatusCode Then
            MessageBox.Show("Submission successful!")
        Else
            MessageBox.Show("Failed to submit. Please try again.")
        End If
    End Sub
    Private elapsedTime As TimeSpan = TimeSpan.Zero
    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick
        elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1))
        lblStopwatchTime.Text = elapsedTime.ToString("hh\:mm\:ss")
    End Sub


    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.T) Then
            btnToggleStopwatch.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged

    End Sub
End Class