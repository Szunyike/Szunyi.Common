''' <summary>
''' General Class To Get Different Type(s) of Variable From InputBox
''' </summary>
Public Class MyInputBox
    ''' <summary>
    ''' Return a String From TextBox
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <returns></returns>
    Public Shared Function GetString(Title As String) As String
        Dim s = InputBox(Title)
        Return s
    End Function
    ''' <summary>
    ''' Return a Strings From TextBox, Separator String must Add
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <returns></returns>
    Public Shared Iterator Function GetStrings(Title As String, Separator As String) As IEnumerable(Of String)
        Dim s = InputBox(Title & "Separated by " & Separator)
        For Each Item In Split(s, Separator)
            Yield Item
        Next
    End Function
    ''' <summary>
    ''' Return Double Or Nothing From TextBox
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <returns></returns>
    Public Shared Function GetDouble(Title As String) As Double
        Dim s = InputBox(Title)
        Try
            Dim d As Double = s
            Return d
        Catch ex As Exception
            If s.Contains(".") Then
                s = s.Replace(".", ",")
                Try
                    Dim d As Double = s
                    Return d
                Catch ex1 As Exception
                    Return Nothing
                End Try
            ElseIf s.Contains(",") Then
                s = s.Replace(",", ".")
                Try
                    Dim d As Double = s
                    Return d
                Catch ex1 As Exception
                    Dim alf As Int16 = 43
                    Return Nothing
                End Try
            End If
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Return Enumerable of Double From TextBox 
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <returns></returns>
    Public Shared Iterator Function GetDoubles(Title As String) As IEnumerable(Of Double)
        Dim s1 = InputBox(Title & " Separated by space")
        Dim decimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        s1 = s1.Replace(".", decimalSeparator).Replace(",", decimalSeparator)
        Dim out As New List(Of Double)
        For Each s In Split(s1, " ")
            Try
                Dim d As Double = s
                Yield d
            Catch ex As Exception

            End Try
        Next
    End Function

    ''' <summary>
    ''' Return Integer Or Nothing From TextBox 
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <returns></returns>
    Public Shared Function GetInteger(Title As String, Optional txt As String = "") As Integer
        Try
            Dim i As Integer = InputBox(Title,, txt)
            Return i
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Return Enumerable of Integer From TextBox 
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <returns></returns>
    Public Shared Iterator Function GetIntegers(Title As String) As IEnumerable(Of Integer)
        Dim s1() = Split(InputBox(Title & " Separated by space"), " ")

        For Each s In s1
            Try
                Dim i As Integer = s
                Yield i
            Catch ex As Exception

            End Try
        Next
    End Function
End Class