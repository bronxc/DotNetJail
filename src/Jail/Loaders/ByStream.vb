Imports Jail.Interfaces
Imports System.IO
Imports System.Reflection.Emit

Namespace Loaders
    <Serializable>
    Public Class ByStream
        Inherits MemoryStream
        Implements IType
        Public Property Type As PEFileKinds Implements IType.Type
        Private Property Filename As String
        Sub New(Filename As String)
            If (Not File.Exists(Filename)) Then Throw New Exception("Filename does not exist")
            Me.Filename = Filename
            Me.Type = Utility.GetAssemblyType(Filename)
            Using fs As New FileStream(Filename, FileMode.Open)
                Using reader As New BinaryReader(fs)
                    Me.Write(reader.ReadBytes(CInt(fs.Length)), 0, CInt(fs.Length))
                End Using
            End Using
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("{0}", IO.Path.GetFileName(Me.Filename))
        End Function
    End Class
End Namespace