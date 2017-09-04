Imports Jail.Interfaces
Imports System.Reflection
Imports System.Runtime.Remoting.Proxies
Imports System.Runtime.Remoting.Messaging

Namespace Proxies
    Public Class Typed
        Inherits RealProxy
        Public Property Proxy As IProxy
        Sub New(Proxy As IProxy, Type As Type)
            MyBase.New(Type)
            Me.Proxy = Proxy
        End Sub
        Public Overrides Function Invoke(Message As IMessage) As IMessage
            Return Me.Handle(TryCast(Message, IMethodCallMessage))
        End Function
        Private Function Handle(Message As IMethodCallMessage) As IMessage
            Try
                Return New ReturnMessage(Me.Proxy.Invoke(Message.MethodName, Message.InArgs), Nothing, 0, Message.LogicalCallContext, Message)
            Catch invocationException As TargetInvocationException
                Return New ReturnMessage(invocationException.InnerException, Message)
            End Try
        End Function
    End Class
End Namespace