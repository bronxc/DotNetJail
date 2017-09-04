Imports Jail.Interfaces
Imports System.Reflection
Imports System.Reflection.Emit

Namespace Loaders
    <Serializable>
    Public Class ByName
        Implements IType
        Public Property Name As AssemblyName
        Public Property Type As PEFileKinds Implements IType.Type
        Sub New(Name As AssemblyName)
            Me.Name = Name
            Me.Type = Utility.GetAssemblyType(Name.CodeBase)
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("{0}", Me.Name.Name)
        End Function
    End Class
End Namespace