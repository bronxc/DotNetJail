Imports Jail.Interfaces
Imports System.Reflection.Emit

Namespace Loaders
    <Serializable>
    Public Class ByFilename
        Implements IType
        Public Property Type As PEFileKinds Implements IType.Type
        Public Property Filename As String
        Sub New(Filename As String)
            Me.Filename = Filename
            Me.Type = Utility.GetAssemblyType(Filename)
        End Sub
        Public Function Name() As String
            Return IO.Path.GetFileName(Me.Filename)
        End Function
        Public Overrides Function ToString() As String
            Return String.Format("{0}", IO.Path.GetFileName(Me.Filename))
        End Function

    End Class
End Namespace