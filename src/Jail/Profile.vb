Imports Jail.Loaders
Imports Jail.Exchange
Imports System.IO
Imports System.Text
Imports System.Security
Imports System.Security.Permissions

Public MustInherit Class Profile
    Implements IDisposable
    Public Property Name As String
    Public Property Path As String
    Public Property Loader As Loader
    Public Property Domain As AppDomain
    Public Property Setup As AppDomainSetup
    Public Property Permissions As PermissionSet
    Sub New(Name As String)
        Me.Name = Name
        Me.Path = String.Format("{0}\{1}", Environment.CurrentDirectory, Me.Name)
        If (Not Directory.Exists(Me.Path)) Then
            Directory.CreateDirectory(Me.Path)
        End If
        Me.Setup = New AppDomainSetup()
        Me.Setup.ApplicationBase = Me.Path
        Me.Setup.DisallowCodeDownload = True
        Me.Setup.DisallowBindingRedirects = False
        Me.Setup.LoaderOptimization = LoaderOptimization.MultiDomainHost
        Me.Permissions = New PermissionSet(PermissionState.None)
    End Sub
    Public Sub SetPermissions(ParamArray Perms() As IPermission)
        Perms.ToList.ForEach(AddressOf Me.SetPermissions)
    End Sub
    Public Sub SetPermissions(Perm As IPermission)
        Me.Permissions.AddPermission(Perm)
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                AppDomain.Unload(Me.Domain)
                Directory.Delete(Me.Path, True)
                Me.Domain = Nothing
                Me.Setup = Nothing
                Me.Permissions = Nothing
                Me.Loader = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
