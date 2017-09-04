Imports System.Reflection
Namespace Interfaces
    Public Interface ICollection
        Sub Load(Name As Loaders.ByName)
        Sub Load(Stream As Loaders.ByStream)
        Sub Load(Filename As Loaders.ByFilename)
        Function ToList() As List(Of Object)
    End Interface
End Namespace