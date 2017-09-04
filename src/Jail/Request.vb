Imports System.Security
Imports System.Reflection
Imports System.Security.Permissions
Imports System.Text.RegularExpressions
Imports System.Text

Public NotInheritable Class Request
    Public Property Type As Type
    Public Property Message As String
    Public Property StackTrace As String
    Public Property Zone As SecurityZone
    Public Property Action As SecurityAction
    Public Property Permission As IPermission
    Sub New(Exception As SecurityException)
        Me.Zone = Exception.Zone
        Me.Action = Exception.Action
        Me.Message = Exception.Message
        Me.Type = Exception.PermissionType
        Me.StackTrace = Exception.StackTrace
        Me.Permission = CType(Exception.Demanded, IPermission)
    End Sub
    Public Function IsType(Of T)() As Boolean
        Return TypeOf Me.Permission Is T
    End Function
    Public Function Cast(Of T)() As T
        Return CType(Me.Permission, T)
    End Function
    Public Overrides Function ToString() As String
        Dim values As New StringBuilder
        values.AppendLine("---------------------------------------------------------------------------------")
        For Each m As Match In New Regex("([a-z]+)(\=|\s\=\s)\""(.*)\""", RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace).Matches(Me.Permission.ToString)
            values.AppendLine(String.Format("{0}: {1}", m.Groups(1).Value.PadRight(13, " "c), Request.Unescape(m.Groups(3).Value)))
        Next
        values.AppendLine("---------------------------------------------------------------------------------")
        Return values.ToString
    End Function
    Private Shared Function Unescape(str As String) As String
        Return str.Replace("\.", ".")
    End Function
End Class