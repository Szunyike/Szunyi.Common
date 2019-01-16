Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports System.Reflection
Imports System.Text

Public Class Util_Helpers
    ''' <summary>
    ''' Return All Enum Values
    ''' </summary>
    ''' <typeparam name="t"></typeparam>
    ''' <param name="currentlySelectedEnum"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_Enum_Names_Values(Of t)(ByVal currentlySelectedEnum As Object) As List(Of String)
        Dim out As New List(Of String)
        Dim enumList As Type = GetType(t)
        If Not enumList.IsEnum Then Throw New InvalidOperationException("Object is not an Enum.")

        Dim values() As Integer = CType([Enum].GetValues(GetType(t)), Integer())
        Dim Names() = CType([Enum].GetNames(GetType(t)), String())


        For i1 = 0 To values.Count - 1
            out.Add(Names(i1) & ":" & values(i1))
        Next
        Return out


    End Function
    ''' <summary>
    ''' Get All Enum Names
    ''' </summary>
    ''' <typeparam name="t"></typeparam>
    ''' <param name="currentlySelectedEnum"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_Enum_Names(Of t)(ByVal currentlySelectedEnum As Object) As List(Of String)
        Dim out As New List(Of String)
        Dim enumList As Type = GetType(t)
        If Not enumList.IsEnum Then Throw New InvalidOperationException("Object is not an Enum.")

        Dim values() As Integer = CType([Enum].GetValues(GetType(t)), Integer())
        Dim Names() = CType([Enum].GetNames(GetType(t)), String())

        Return Names.ToList


    End Function
    ''' <summary>
    ''' Get Enum Name From Enum Value
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="first"></param>
    ''' <returns></returns>
    Public Shared Function Get_Enum_Name(Of T)(first As String) As String
        Dim enumList As Type = GetType(T)
        Dim x = CType([Enum].Parse(GetType(T), first), T)
        Dim values() As Integer = CType([Enum].GetValues(GetType(T)), Integer())
        Dim Names() = CType([Enum].GetNames(GetType(T)), String())

        For i1 = 0 To Names.Count - 1
            If values(i1) = first Then
                Return Names(i1)
            End If
        Next
        Return -1
    End Function
    ''' <summary>
    ''' Get Enum Value From Enum Name
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="first"></param>
    ''' <returns></returns>
    Public Shared Function Get_Enum_Value(Of T)(first As String) As Integer
        Dim enumList As Type = GetType(T)
        Dim x = CType([Enum].Parse(GetType(T), first), T)
        Dim values() As Integer = CType([Enum].GetValues(GetType(T)), Integer())
        Dim Names() = CType([Enum].GetNames(GetType(T)), String())

        For i1 = 0 To Names.Count - 1
            If Names(i1) = first Then
                Return values(i1)
            End If
        Next
        Return -1
    End Function
    ''' <summary>
    ''' 'Get All Enum Valus From Enum Names
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="Items"></param>
    ''' <returns></returns>
    Public Shared Function Get_Enum_Values(Of T)(Items As List(Of String)) As List(Of Integer)
        Dim enumList As Type = GetType(T)
        Dim x = CType([Enum].Parse(GetType(T), Items.First), T)
        Dim values() As Integer = CType([Enum].GetValues(GetType(T)), Integer())
        Dim Names() = CType([Enum].GetNames(GetType(T)), String())
        Dim out As New List(Of Integer)
        For Each Item In Items
            For i1 = 0 To Names.Count - 1
                If Names(i1) = Item Then
                    out.Add(values(i1))
                End If
            Next
        Next

        Return out
    End Function
    Public Shared Function Get_Enums(Of T)(Items As List(Of String)) As List(Of T)
        Dim enumList As Type = GetType(T)
        Dim x = CType([Enum].Parse(GetType(T), Items.First), T)
        Dim values() As Integer = CType([Enum].GetValues(GetType(T)), Integer())
        Dim Names() = CType([Enum].GetNames(GetType(T)), String())
        Dim out As New List(Of T)
        For Each Item In Items
            out.Add(CType([Enum].Parse(GetType(T), Item), T))
        Next

        Return out
    End Function

    ''' <summary>
    ''' Get Property Valur From Object by Property Name
    ''' </summary>
    ''' <param name="src"></param>
    ''' <param name="Property_Name"></param>
    ''' <returns></returns>
    Public Shared Function Get_Property_Value(src As Object, Property_Name As String)

        If IsNothing(src.GetType().GetProperty(Property_Name)) = True Then Return String.Empty

        Return src.GetType().GetProperty(Property_Name).GetValue(src)
    End Function

    Public Shared Function Get_Const_Values(Type As Type) As List(Of FieldInfo)

        Dim fieldInfos As FieldInfo() = Type.GetFields(BindingFlags.[Public] Or BindingFlags.[Static] Or BindingFlags.FlattenHierarchy)
        Return fieldInfos.Where(Function(fi) fi.IsLiteral AndAlso Not fi.IsInitOnly).ToList()
    End Function

    Public Shared Function HasContain(ls As IEnumerable(Of String), Item As String) As Boolean
        Dim res = From x In ls Where x.IndexOf(Item, comparisonType:=StringComparison.InvariantCultureIgnoreCase) > -1
        If res.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Shared Function GetConstants(ByVal type As Type) As IEnumerable(Of FieldInfo)
        Dim fieldInfos = type.GetFields(BindingFlags.[Public] Or BindingFlags.[Static] Or BindingFlags.FlattenHierarchy)
        Return fieldInfos.Where(Function(fi) fi.IsLiteral AndAlso Not fi.IsInitOnly)
    End Function


    Public Shared Iterator Function GetConstantsValues(Of T As Class)(ByVal type As Type) As IEnumerable(Of String)
        Dim fieldInfos = GetConstants(type)
        For Each f In fieldInfos
            Yield f.GetRawConstantValue

        Next

    End Function
    Public Shared Function CreatePassword(ByVal length As Integer) As String
        Const valid As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890~!@#$%^&*_-+=`|\(){}[]:;'<>,.?/"
        Dim res As StringBuilder = New StringBuilder()
        Dim rnd As Random = New Random()

        While 0 < Math.Max(System.Threading.Interlocked.Decrement(length), length + 1)
            res.Append(valid(rnd.[Next](valid.Length)))
        End While

        Return res.ToString()
    End Function

End Class



