Public Class Form1
    Private Property HostFile As String
    Private Property RegisterRun As String
    Private Property Website As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = String.Format("Jail Test [Domain: {0}]", AppDomain.CurrentDomain.FriendlyName)
        Me.Label1.Text = TruncatePath(Application.StartupPath)

        Me.HostFile = "C:\Windows\system32\drivers\etc\hosts"
        Me.RegisterRun = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
        Me.Website = "http://www.google.com/"

    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim buffer As String = IO.File.ReadAllText(Me.HostFile)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Microsoft.Win32.Registry.SetValue(Me.RegisterRun, "TEST", "TEST", Microsoft.Win32.RegistryValueKind.String)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim buffer As String = New System.Net.WebClient().DownloadString(Me.Website)
    End Sub
    Private Function TruncatePath(str) As String
        Dim path As String = str
        Return String.Format("...{0}", path.Substring(path.Length - path.Length \ 2))
    End Function
End Class
