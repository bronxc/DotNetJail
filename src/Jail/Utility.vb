Imports System.IO
Imports System.Reflection.Emit

<Serializable>
Public Class Utility
    Public Shared Function GetAssemblyType(Filename As String) As PEFileKinds
        Dim offset As UInt32 = 0
        Using fs As New FileStream(New Uri(Filename).LocalPath, FileMode.Open, FileAccess.Read)
            Dim buffer = New Byte(3) {}
            fs.Seek(&H3C, SeekOrigin.Begin)
            fs.Read(buffer, 0, 4)
            offset = BitConverter.ToUInt32(buffer, 0)
            fs.Seek(offset + &H5C, SeekOrigin.Begin)
            fs.Read(buffer, 0, 1)
            If buffer(0) = 3 Then
                Return PEFileKinds.ConsoleApplication
            ElseIf buffer(0) = 2 Then
                Return PEFileKinds.WindowApplication
            Else
                Return PEFileKinds.Dll
            End If
        End Using
    End Function
End Class
