Namespace Interfaces
    Public Interface IProxy
        Function GetProperty(Reference As String) As Object
        Sub SetProperty(Reference As String, value As Object)
        Function Invoke(Reference As String, ParamArray Parameters As Object()) As Object
    End Interface
End Namespace