Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports System.Reflection

Public Class Util_Helpers
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

    Public Shared Function Get_All_Enum_Names(Of t)(ByVal currentlySelectedEnum As Object) As List(Of String)
        Dim out As New List(Of String)
        Dim enumList As Type = GetType(t)
        If Not enumList.IsEnum Then Throw New InvalidOperationException("Object is not an Enum.")

        Dim values() As Integer = CType([Enum].GetValues(GetType(t)), Integer())
        Dim Names() = CType([Enum].GetNames(GetType(t)), String())

        Return Names.ToList


    End Function
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
    Public Shared Function Get_Enum_Value(Of T)(Items As List(Of String)) As List(Of Integer)
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
    Public Shared Function Get_Enum_Valueit(Of T)(value As Object) As Integer
        Throw New NotImplementedException()
    End Function

    Public Shared Function Get_Enum_Value() As Integer
        Throw New NotImplementedException()
    End Function

    Public Shared Function Get_Property_Value(src As Object, Property_Name As String)

        If IsNothing(src.GetType().GetProperty(Property_Name)) = True Then Return String.Empty

        Return src.GetType().GetProperty(Property_Name).GetValue(src)
    End Function

End Class

Public Class SelectLambdaBuilder(Of T)
    Private Shared _typePropertyInfoMappings As Dictionary(Of Type, PropertyInfo()) = New Dictionary(Of Type, PropertyInfo())()
    Private ReadOnly _typeOfBaseClass As Type = GetType(T)

    Private Function GetFieldMapping(ByVal fields As String) As Dictionary(Of String, List(Of String))
        If String.IsNullOrEmpty(fields) Then
            Return Nothing
        End If

        Dim selectedFieldsMap = New Dictionary(Of String, List(Of String))()

        For Each s In fields.Split(","c)
            Dim nestedFields = s.Split("."c).[Select](Function(f) f.Trim()).ToArray()
            Dim nestedValue = If(nestedFields.Length > 1, nestedFields(1), Nothing)

            If selectedFieldsMap.Keys.Any(Function(key) key = nestedFields(0)) Then
                selectedFieldsMap(nestedFields(0)).Add(nestedValue)
            Else
                selectedFieldsMap.Add(nestedFields(0), New List(Of String) From {
                    nestedValue
                })
            End If
        Next

        Return selectedFieldsMap
    End Function

    Public Function CreateNewStatement(ByVal fields As String) As Func(Of T, T)
        Dim selectFields = GetFieldMapping(fields)

        If selectFields Is Nothing Then
            Return Function(s) s
        End If

        Dim xParameter As ParameterExpression = Expression.Parameter(_typeOfBaseClass, "s")
        Dim xNew As NewExpression = Expression.[New](_typeOfBaseClass)
        Dim shpNestedPropertyBindings = New List(Of MemberAssignment)()

        For Each keyValuePair In selectFields
            Dim propertyInfos As PropertyInfo()

            If Not _typePropertyInfoMappings.TryGetValue(_typeOfBaseClass, propertyInfos) Then
                Dim properties = _typeOfBaseClass.GetProperties()
                propertyInfos = properties
                _typePropertyInfoMappings.Add(_typeOfBaseClass, properties)
            End If

            Dim propertyType = propertyInfos.FirstOrDefault(Function(p) p.Name.ToLowerInvariant().Equals(keyValuePair.Key.ToLowerInvariant())).PropertyType

            If propertyType.IsClass Then
                Dim objClassPropInfo As PropertyInfo = _typeOfBaseClass.GetProperty(keyValuePair.Key)
                Dim objNestedMemberExpression As MemberExpression = Expression.[Property](xParameter, objClassPropInfo)
                Dim innerObjNew As NewExpression = Expression.[New](propertyType)
                Dim nestedBindings = keyValuePair.Value.[Select](Function(v)
                                                                     Dim nestedObjPropInfo As PropertyInfo = propertyType.GetProperty(v)
                                                                     Dim nestedOrigin2 As MemberExpression = Expression.[Property](objNestedMemberExpression, nestedObjPropInfo)
                                                                     Dim binding2 = Expression.Bind(nestedObjPropInfo, nestedOrigin2)
                                                                     Return binding2
                                                                 End Function)
                Dim nestedInit As MemberInitExpression = Expression.MemberInit(innerObjNew, nestedBindings)
                shpNestedPropertyBindings.Add(Expression.Bind(objClassPropInfo, nestedInit))
            Else
                Dim mbr As Expression = xParameter
                mbr = Expression.PropertyOrField(mbr, keyValuePair.Key)
                Dim mi As PropertyInfo = _typeOfBaseClass.GetProperty((CType(mbr, MemberExpression)).Member.Name)
                Dim xOriginal = Expression.[Property](xParameter, mi)
                shpNestedPropertyBindings.Add(Expression.Bind(mi, xOriginal))
            End If
        Next

        Dim xInit = Expression.MemberInit(xNew, shpNestedPropertyBindings)
        Dim lambda = Expression.Lambda(Of Func(Of T, T))(xInit, xParameter)
        Return lambda.Compile()
    End Function
End Class


Public Module PredicateBuilder
    Function [True](Of T)() As Expression(Of Func(Of T, Boolean))
        Return Function(f) True
    End Function

    Function [False](Of T)() As Expression(Of Func(Of T, Boolean))
        Return Function(f) False
    End Function

    <Extension()>
    Function [Or](Of T)(ByVal expr1 As Expression(Of Func(Of T, Boolean)), ByVal expr2 As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
        Dim invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast(Of Expression)())
        Return Expression.Lambda(Of Func(Of T, Boolean))(Expression.[OrElse](expr1.Body, invokedExpr), expr1.Parameters)
    End Function

    <Extension()>
    Function [And](Of T)(ByVal expr1 As Expression(Of Func(Of T, Boolean)), ByVal expr2 As Expression(Of Func(Of T, Boolean))) As Expression(Of Func(Of T, Boolean))
        Dim invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast(Of Expression)())
        Return Expression.Lambda(Of Func(Of T, Boolean))(Expression.[AndAlso](expr1.Body, invokedExpr), expr1.Parameters)
    End Function
End Module
