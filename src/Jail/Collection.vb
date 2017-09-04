Imports Jail.Interfaces

Public Class Collection
    Implements ICollection
    Public Property Assemblies As List(Of Object)
    Sub New()
        Me.Assemblies = New List(Of Object)
    End Sub
    Public Sub Load(Name As Loaders.ByName) Implements ICollection.Load
        Me.Assemblies.Add(Name)
    End Sub
    Public Sub Load(Stream As Loaders.ByStream) Implements ICollection.Load
        Me.Assemblies.Add(Stream)
    End Sub
    Public Sub Load(Filename As Loaders.ByFilename) Implements ICollection.Load
        Me.Assemblies.Add(Filename)
    End Sub
    Private Function ToList() As List(Of Object) Implements ICollection.ToList
        Return Me.Assemblies
    End Function
End Class