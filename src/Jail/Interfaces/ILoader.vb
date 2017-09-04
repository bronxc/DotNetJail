Imports System.Reflection

Namespace Interfaces
    Public Interface ILoader
        Sub Run(ParamArray Parameters() As Object)
        Sub Run(Name As String, ParamArray Parameters() As Object)
        Function Resolve(Name As String) As IProxy
    End Interface
End Namespace