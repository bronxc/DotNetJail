Imports System.Dynamic
Imports Jail.Interfaces

Namespace Proxies
    Public Class Dynamic
        Inherits DynamicObject
        Public Property Proxy As IProxy
        Sub New(Proxy As IProxy)
            Me.Proxy = Proxy
        End Sub
        Public Overrides Function TryGetMember(Binder As GetMemberBinder, ByRef Result As Object) As Boolean
            Try
                Result = Me.Proxy.GetProperty(Binder.Name)
                Return True
            Catch e As Exception
                Return False
            End Try
        End Function
        Public Overrides Function TrySetMember(Binder As SetMemberBinder, Value As Object) As Boolean
            Try
                Me.Proxy.SetProperty(Binder.Name, Value)
                Return True
            Catch e As Exception
                Return False
            End Try
        End Function
        Public Overrides Function TryInvokeMember(Binder As InvokeMemberBinder, Parameters As Object(), ByRef Result As Object) As Boolean
            Try
                Result = Me.Proxy.Invoke(Binder.Name, Parameters)
                Return True
            Catch e As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace