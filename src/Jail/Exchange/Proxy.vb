Imports Jail.Factory
Imports Jail.Interfaces
Imports System.Security
Imports System.Reflection

Namespace Exchange
    Public Class Proxy
        Inherits MarshalByRefObject
        Implements IProxy
        Public Property Name As String
        Public Property Types As Lazy(Of Object)
        Sub New(Name As String)
            Me.Name = Name
            Me.Types = New Lazy(Of Object)(Function() TypeActivator.Create(Name))
        End Sub
        Public Function GetProperty(Name As String) As Object Implements IProxy.GetProperty

            Throw New NotImplementedException

        End Function
        Public Sub SetProperty(Name As String, value As Object) Implements IProxy.SetProperty

            Throw New NotImplementedException

        End Sub
        Public Function Invoke(Name As String, ParamArray Parameters As Object()) As Object Implements IProxy.Invoke
            Dim objectType As Object = Me.Types.Value
            For Each m As MethodInfo In objectType.GetType.GetMethods(BindingFlags.Instance Or BindingFlags.Public)
                If (m.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)) Then
                    Return m.Invoke(objectType, Parameters)
                End If
            Next
            Return Nothing
        End Function
    End Class
End Namespace