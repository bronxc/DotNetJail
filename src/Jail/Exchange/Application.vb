Imports Jail.Interfaces
Imports System.Reflection
Imports System.Security
Imports System.Security.Permissions

Namespace Exchange
    Public Class Application
        Inherits MarshalByRefObject
        Implements IApp
        Public Sub Run(ParamArray Parameters() As Object)
            Try
                '// Complex assemblies often have trouble being executed into a seperate domain
                '// the exceptions of TypeLoad/Resource will be coming from these procedures.

                Call New ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess).Assert()
                Dim method As MethodInfo = Me.Scan.EntryPoint
                method.Invoke(Nothing, If(method IsNot Nothing AndAlso method.GetParameters.Length = 0, New Object() {}, Parameters))
            Finally
                CodeAccessPermission.RevertAll()
            End Try
        End Sub
        Public Sub Run(Name As String, ParamArray Parameters() As Object)
            Try
                Call New ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess).Assert()
                Dim method As MethodInfo = Me.Scan(Name).EntryPoint
                method.Invoke(Nothing, If(method IsNot Nothing AndAlso method.GetParameters.Length = 0, New Object() {}, Parameters))
            Finally
                CodeAccessPermission.RevertAll()
            End Try
        End Sub
        Private Function Scan() As Assembly
            For Each asm As Assembly In AppDomain.CurrentDomain.GetAssemblies
                If (asm.EntryPoint IsNot Nothing) Then
                    Return asm
                End If
            Next
            Throw New Exception("No entrypoint found")
        End Function
        Private Function Scan(Name As String) As Assembly
            For Each asm As Assembly In AppDomain.CurrentDomain.GetAssemblies
                If (asm.EntryPoint IsNot Nothing AndAlso asm.GetName.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)) Then
                    Return asm
                End If
            Next
            Throw New Exception(String.Format("No entrypoint found in '{0}'", Name))
        End Function
    End Class
End Namespace