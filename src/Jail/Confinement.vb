﻿Imports Jail.Loaders
Imports Jail.Exchange
Imports Jail.Interfaces
Imports System.IO
Imports System.Security.Policy

Public NotInheritable Class Confinement
    Inherits Profile
    Implements IConfinement
    Sub New(Name As String)
        MyBase.New(Name)
    End Sub
    Protected Friend Function Create(Collection As ICollection, Callback As Action(Of Object, Boolean, Exception)) As ILoader Implements IConfinement.Create
        Me.Domain = Me.CreateDomain(Confinement.GetName(Of Jail))
        Me.Loader = Me.CreateLoader(Me.Domain)
        Me.Loader.Append(New Loaders.ByName(GetType(Jail).Assembly.GetName))

        For Each entry As Object In Collection.ToList
            If TypeOf entry Is Loaders.ByName Then
                If (Callback IsNot Nothing) Then
                    If (Not Me.Loader.Append(CType(entry, Loaders.ByName))) Then
                        Callback.Invoke(entry, False, New IOException("Unable to load assembly"))
                    Else
                        Callback.Invoke(entry, True, Nothing)
                    End If
                ElseIf TypeOf entry Is Loaders.ByStream Then
                    If (Not Me.Loader.Append(CType(entry, Loaders.ByStream))) Then
                        Callback.Invoke(entry, False, New IOException("Unable to load assembly"))
                    Else
                        Callback.Invoke(entry, True, Nothing)
                    End If
                ElseIf TypeOf entry Is Loaders.ByFilename Then
                    Dim Loader As Loaders.ByFilename = CType(entry, Loaders.ByFilename)
                    Dim filename As String = String.Format("{0}\{1}", Me.Path, Loader.Name)
                    File.Copy(Loader.Filename, filename, True)
                    If (Not Me.Loader.Append(New Loaders.ByFilename(filename))) Then
                        Callback.Invoke(entry, False, New IOException("Unable to load assembly"))
                    Else
                        Callback.Invoke(entry, True, Nothing)
                    End If
                End If
            End If
        Next
        Return Me.Loader
    End Function
    Public Shared Function GetName(Of T)() As StrongName
        Return GetType(T).Assembly.Evidence.GetHostEvidence(Of StrongName)()
    End Function
    Protected Friend Function CreateLoader(Domain As AppDomain) As Loader
        Return CType(Activator.CreateInstanceFrom(Domain, GetType(Loader).Assembly.CodeBase, GetType(Loader).FullName).Unwrap, Loader)
    End Function
    Protected Friend Function CreateDomain(Name As StrongName) As AppDomain
        Return AppDomain.CreateDomain(Me.Name, Nothing, Me.Setup, Me.Permissions, Name, Confinement.GetName(Of Object))
    End Function
End Class