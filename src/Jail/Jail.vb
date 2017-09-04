Imports Jail.Proxies
Imports Jail.Interfaces
Imports System.Security

Public NotInheritable Class Jail
    Implements IJail
    Private Property Loader As ILoader
    Public Event JailCaughtException(Sender As Object, ex As Exception)
    Public Event JailBlockedPermission(Sender As Object, Request As Request)
    Sub New(Loader As ILoader)
        Me.Loader = Loader
    End Sub
    Public Sub Run(ParamArray Parameters() As Object) Implements IJail.Run
        Try
            Me.Loader.Run(Parameters)
        Catch ex As Exception
            Me.CallEventHandler(ex)
        End Try
    End Sub
    Public Sub Run(Name As String, ParamArray Parameters() As Object) Implements IJail.Run
        Try
            Me.Loader.Run(Name, Parameters)
        Catch ex As Exception
            Me.CallEventHandler(ex)
        End Try
    End Sub
    Public Function ToDynamicProxy(Name As String) As Object Implements IJail.ToDynamicProxy
        Try
            Return New Dynamic(Me.Loader.Resolve(Name))
        Catch ex As Exception
            Me.CallEventHandler(ex)
            Return Nothing
        End Try
    End Function
    Public Function ToTypedProxy(Of T As Class)(Name As String) As T Implements IJail.ToTypedProxy
        Try
            Return TryCast(New Typed(Me.Loader.Resolve(Name), GetType(T)).GetTransparentProxy, T)
        Catch ex As Exception
            Me.CallEventHandler(ex)
            Return Nothing
        End Try
    End Function
    Public Sub CallEventHandler(ex As Exception)
        Dim exception As Exception = If(ex.InnerException IsNot Nothing, ex.InnerException, ex)
        If (TypeOf exception Is SecurityException) Then
            RaiseEvent JailBlockedPermission(Me, New Request(CType(exception, SecurityException)))
        Else
            RaiseEvent JailCaughtException(Me, exception)
        End If
    End Sub
    Public Shared Function Create(Confinement As IConfinement, Callback As Action(Of Object, Boolean, Exception), Optional Collection As ICollection = Nothing) As IJail
        Return New Jail(Confinement.Create(Collection, Callback))
    End Function
#Region "IDisposable Support"
    Private disposedValue As Boolean
    Protected Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
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
