Namespace Interfaces
    Public Interface IConfinement
        Function Create(Collection As ICollection, Callback As Action(Of Object, Boolean, Exception)) As ILoader
    End Interface
End Namespace