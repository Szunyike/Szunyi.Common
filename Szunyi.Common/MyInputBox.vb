Public Class MyInputBox
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
    Public Shared Function GetString(Title As String) As String
        Dim s = InputBox(Title)

        Return s

    End Function
    Public Shared Function GetDoubles(Title As String) As List(Of Double)
        Dim s1 = InputBox(Title & "Separated by space")
        Dim decimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        s1 = s1.Replace(".", decimalSeparator).Replace(",", decimalSeparator)
        Dim out As New List(Of Double)
        For Each s In Split(s1, " ")
            Try
                Dim d As Double = s
                out.Add(d)
            Catch ex As Exception

            End Try
        Next
        Return out
    End Function
    ''' <summary>
    ''' Return Integer Or Nothing 
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
    ''' Return Integer Or Empty List 
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <returns></returns>
    Public Shared Function GetIntegers(Title As String) As List(Of Integer)
        Dim s1() = Split(InputBox(Title), " ")
        Dim out As New List(Of Integer)
        For Each s In s1
            Try
                out.Add(s)

            Catch ex As Exception

            End Try

        Next
        Return out
    End Function
End Class