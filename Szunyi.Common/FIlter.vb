Public Class Filter
    Private obj As Object



    Public Shared Function Pass(o1 As Object, Constant As Object, type As Enums.Filter) As Boolean
        Select Case type
            Case Enums.Filter.Bigger
                Return o1 > Constant
            Case Enums.Filter.Bigger_or_Equal
                Return o1 >= Constant
            Case Enums.Filter.Smaller
                Return o1 < Constant
            Case Enums.Filter.Smaller_or_Equal
                Return o1 <= Constant
            Case Enums.Filter.Equal
                Return o1 = Constant

            Case Enums.Filter.Contain
                Return o1.ToString.Contains(Constant.ToString)
            Case Enums.Filter.Not_Contain
                Return Not (o1.ToString.Contains(Constant.ToString))
            Case Enums.Filter.Start_with
                Return o1.ToString.StartsWith(Constant.ToString)
            Case Enums.Filter.Not_Start_with
                Return Not (o1.ToString.StartsWith(Constant.ToString))
            Case Enums.Filter.End_with
                Return o1.ToString.EndsWith(Constant.ToString)
            Case Enums.Filter.Not_End_with
                Return Not (o1.ToString.EndsWith(Constant.ToString))
            Case Else
                Return Nothing
        End Select
    End Function

    Public Shared Iterator Function Parse(ls As List(Of Object), Constant As Object, Type As Enums.Filter) As IEnumerable(Of Object)
        Select Case Type
            Case Enums.Filter.Bigger
                Yield Bigger(ls, Constant)
            Case Enums.Filter.Bigger_or_Equal
                Yield Bigger_or_Equal(ls, Constant)
            Case Enums.Filter.Smaller
                Yield Smaller(ls, Constant)
            Case Enums.Filter.Smaller_or_Equal
                Yield Smaller_or_Equal(ls, Constant)
            Case Enums.Filter.Equal
                Yield Equal(ls, Constant)

            Case Enums.Filter.Contain
                Yield Contains(ls, Constant)
            Case Enums.Filter.Not_Contain
                Yield Not_Contains(ls, Constant)
            Case Enums.Filter.Start_with
                Yield Start_with(ls, Constant)
            Case Enums.Filter.Not_Start_with
                Yield Not_Start_With(ls, Constant)
            Case Enums.Filter.End_with
                Yield End_with(ls, Constant)
            Case Enums.Filter.Not_End_with
                Yield Not_End_With(ls, Constant)
        End Select
    End Function

#Region "Private Iterators"
    Private Shared Iterator Function Bigger(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If o1 > Constant Then
                Yield o1
            End If
        Next
    End Function
    Private Shared Iterator Function Bigger_or_Equal(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If o1 >= Constant Then
                Yield o1
            End If
        Next
    End Function
    Private Shared Iterator Function Smaller(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If o1 < Constant Then
                Yield o1
            End If
        Next
    End Function
    Private Shared Iterator Function Smaller_or_Equal(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If o1 <= Constant Then
                Yield o1
            End If
        Next
    End Function
    Private Shared Iterator Function Equal(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If o1 = Constant Then
                Yield o1
            End If
        Next
    End Function
    Private Shared Iterator Function Contains(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If o1.ToString.Contains(Constant.ToString) Then
                Yield o1
            End If
        Next
    End Function
    Private Shared Iterator Function Not_Contains(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If (o1.ToString.Contains(Constant.ToString)) = False Then
                Yield o1
            End If
        Next
    End Function
    Private Shared Iterator Function Start_with(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If o1.ToString.StartsWith(Constant.ToString) Then
                Yield o1
            End If
        Next
    End Function
    Private Shared Iterator Function Not_Start_With(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If (o1.ToString.StartsWith(Constant.ToString)) = False Then
                Yield o1
            End If
        Next
    End Function
    Private Shared Iterator Function End_with(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If o1.ToString.EndsWith(Constant.ToString) Then
                Yield o1
            End If
        Next
    End Function
    Private Shared Iterator Function Not_End_With(ls As List(Of Object), Constant As Object) As IEnumerable(Of Object)
        For Each o1 In ls
            If (o1.ToString.EndsWith(Constant.ToString)) = False Then
                Yield o1
            End If
        Next
    End Function
#End Region


End Class
