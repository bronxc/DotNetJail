Imports System.Reflection
Namespace Interfaces
    Public Interface IJail
        Inherits IDisposable
        Sub Run(ParamArray Parameters() As Object)
        Sub Run(Name As String, ParamArray Parameters() As Object)
        Function ToDynamicProxy(Name As String) As Object
        Function ToTypedProxy(Of T As Class)(Name As String) As T
    End Interface
End Namespace
