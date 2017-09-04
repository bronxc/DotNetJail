Imports Jail.Proxies
Imports Jail.Interfaces
Imports System.IO
Imports System.Security
Imports System.Reflection
Imports System.Security.Permissions
Imports System.Reflection.Emit

Namespace Exchange
    Public NotInheritable Class Loader
        Inherits MarshalByRefObject
        Implements ILoader
        Sub New()
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf Me.EventResolve
        End Sub
        Public Function Append(Loader As Loaders.ByName) As Boolean
            Try
                Call New FileIOPermission(PermissionState.Unrestricted).Assert()
                Assembly.Load(Loader.Name)
                Return True
            Catch
                Return False
            Finally
                CodeAccessPermission.RevertAll()
            End Try
        End Function
        Public Function Append(Loader As Loaders.ByFilename) As Boolean
            Try
                Call New FileIOPermission(PermissionState.Unrestricted).Assert()
                If (Not File.Exists(Loader.Filename)) Then Throw New IOException
                Assembly.LoadFile(Path.GetFullPath(Loader.Filename))
                Return True
            Catch
                Return False
            Finally
                CodeAccessPermission.RevertAll()
            End Try
        End Function
        Public Function Append(Loader As Loaders.ByStream) As Boolean
            Try
                Assembly.Load(Loader.ToArray, Nothing, SecurityContextSource.CurrentAppDomain)
                Return True
            Catch
                Return False
            End Try
        End Function
        Public Sub Run(ParamArray Parameters() As Object) Implements ILoader.Run
            Call New Exchange.Application().Run(Parameters)
        End Sub
        Public Sub Run(Name As String, ParamArray Parameters() As Object) Implements ILoader.Run
            Call New Exchange.Application().Run(Name, Parameters)
        End Sub
        Public Function Resolve(Name As String) As IProxy Implements ILoader.Resolve
            Return New Exchange.Proxy(Name)
        End Function
        Private Function EventResolve(Sender As Object, Arguments As ResolveEventArgs) As Assembly
            Return (From asm In AppDomain.CurrentDomain.GetAssemblies() Where asm.FullName = Arguments.Name).FirstOrDefault()
        End Function
    End Class
End Namespace