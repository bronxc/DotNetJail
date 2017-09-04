Imports System.Reflection
Namespace Factory
    Public Class TypeActivator
        Public Shared Function Create(TypeName As String) As Object
            For Each asm As Assembly In AppDomain.CurrentDomain.GetAssemblies
                For Each type As Type In asm.GetTypes
                    If (type.Name.Equals(TypeName, StringComparison.OrdinalIgnoreCase)) Then
                        Return Activator.CreateInstance(type)
                    End If
                Next
            Next
            Return Nothing
        End Function
    End Class
End Namespace