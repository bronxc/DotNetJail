Imports System.IO
Imports System.Security
Imports System.Security.Permissions

Public Class frmMain
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub frmMain_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub frmMain_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Me.JailCreateAndRun(CType(e.Data.GetData(DataFormats.FileDrop), String()))
    End Sub
    Private Sub WriteLog(Message As String, Optional Clear As Boolean = False)
        With Me.tbLog
            If (Clear) Then .Clear()
            .AppendText(String.Format("{0}{1}", Message, Environment.NewLine))
            .SelectionLength = .Text.Length
            .ScrollToCaret()
        End With
    End Sub
    Private Sub JailCreateAndRun(Assemblies() As String)

        Me.WriteLog("Status       : Creating new jail", True)

        '// Collect and append assemblies into our collection
        Dim Collection As New Collection
        For Each filename As String In Assemblies
            If (IO.File.Exists(filename)) Then
                Me.WriteLog(String.Format("Loading      : {0}", IO.Path.GetFileName(filename)))
                Collection.Load(New Loaders.ByFilename(filename))
            End If
        Next

        '// Create confinement
        Using confinement As New Confinement("My Jail")

            '// Bare minimum (Execution and local path access)
            confinement.Permissions.AddPermission(New SecurityPermission(SecurityPermissionFlag.Execution))
            confinement.Permissions.AddPermission(New FileIOPermission(FileIOPermissionAccess.AllAccess, confinement.Path))

            '// GUI permission
            confinement.Permissions.AddPermission(New UIPermission(UIPermissionWindow.AllWindows))


            '// Create jail with confinement and collection, will call back for load results
            Using jail As Jail = CType(jail.Create(confinement, AddressOf Me.LoadCallback, Collection), Jail)
                Try
                    '// Assign event handler
                    AddHandler jail.JailBlockedPermission, AddressOf Me.JailBlockedPermission

                    '// Run application (assuming it is an executable)
                    jail.Run(New Object() {New String() {}})
                Finally

                    '// Remove event handler
                    RemoveHandler jail.JailBlockedPermission, AddressOf Me.JailBlockedPermission
                End Try
            End Using
        End Using

        Me.WriteLog("Status       : Jail unloaded")

    End Sub

    '// Callback result
    Private Sub LoadCallback(sender As Object, loaded As Boolean, ex As Exception)
        If (Not loaded) Then Me.WriteLog(String.Format("Load error   : {0} {1}", sender.ToString, ex.Message))
    End Sub

    '// Callback blocked security events
    Private Sub JailBlockedPermission(Sender As Object, Request As Request)
        Me.WriteLog(String.Format("-> Blocked   : {0} on {1} {3}{2}", Request.Type.Name, Request.Zone, Request.ToString, Environment.NewLine))
    End Sub

End Class
