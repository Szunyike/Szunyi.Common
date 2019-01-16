Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Text.RegularExpressions

Public Module Extensions
#Region "GO"
    ''' <summary>
    ''' Return the GO number in correct format or EmptyString
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function Convert_to_GO(s As String) As String
        Try
            Dim i As Integer = s
            Return "GO:" & i.ToString("0000000")
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    ''' <summary>
    ''' Return the GO number in correct format or EmptyString
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function Convert_to_GO(i As Integer) As String
        Try
            Return "GO:" & i.ToString("0000000")
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
#End Region
#Region "GetText"
    ''' <summary>
    ''' Merge Enumerable Of String in to String Using User Selected Separator
    ''' </summary>
    ''' <param name="Ls"></param>
    ''' <param name="separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetText(Ls As IEnumerable(Of String), Optional separator As String = vbTab) As String
        Dim str As New StringBuilder
        For Each s In Ls
            str.Append(s).Append(separator)
        Next
        If str.Length > 0 Then str.Length -= separator.Length
        Return str.ToString
    End Function
    ''' <summary>
    ''' Merge Enumerable Of Long in to String Using User Selected Separator
    ''' </summary>
    ''' <param name="Ls"></param>
    ''' <param name="separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetText(Ls As IEnumerable(Of Long), Optional separator As String = vbTab) As String
        Dim str As New StringBuilder
        For Each s In Ls
            str.Append(s).Append(separator)
        Next
        If str.Length > 0 Then str.Length -= separator.Length
        Return str.ToString
    End Function
    ''' <summary>
    ''' Merge Enumerable Of Double in to String Using User Selected Separator
    ''' </summary>
    ''' <param name="Ls"></param>
    ''' <param name="separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetText(Ls As IEnumerable(Of Double), Optional separator As String = vbTab) As String
        Dim str As New StringBuilder
        For Each s In Ls
            str.Append(s).Append(separator)
        Next
        If str.Length > 0 Then str.Length -= separator.Length
        Return str.ToString
    End Function
    ''' <summary>
    ''' Merge Enumerable Of Integer in to String Using User Selected Separator
    ''' </summary>
    ''' <param name="Ls"></param>
    ''' <param name="separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetText(Ls As IEnumerable(Of Integer), Optional separator As String = vbTab) As String
        Dim str As New StringBuilder
        For Each s In Ls
            str.Append(s).Append(separator)
        Next
        If str.Length > 0 Then str.Length -= separator.Length
        Return str.ToString
    End Function
    ''' <summary>
    ''' Merge Enumerable Of Byte in to String
    ''' </summary>
    ''' <param name="Bytes"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetText(Bytes As IEnumerable(Of Byte)) As String
        Return System.Text.Encoding.ASCII.GetString(Bytes.ToArray())
    End Function
    ''' <summary>
    ''' Merge Dictionary Keys And Values Using User Defined Separator and vbcrlf
    ''' </summary>
    ''' <param name="Dict"></param>
    ''' <param name="separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetText(Dict As Dictionary(Of String, Integer), Optional separator As String = vbTab) As String
        If IsNothing(Dict) = True OrElse Dict.Count = 0 Then Return String.Empty
        Dim str As New System.Text.StringBuilder
        For Each item In Dict
            str.Append(item.Key).Append(separator).Append(item.Value).AppendLine()
        Next
        If str.Length > 0 Then str.Length -= separator.Length
        Return str.ToString
    End Function
    ''' <summary>
    ''' Merge Dictionary Keys And Values Using User Defined Separator and vbcrlf
    ''' </summary>
    ''' <param name="x1"></param>
    ''' <param name="separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetText(x1 As Dictionary(Of Integer, Integer), Optional separator As String = vbTab) As String
        If IsNothing(x1) = True OrElse x1.Count = 0 Then Return String.Empty
        Dim str As New System.Text.StringBuilder
        For Each item In x1
            str.Append(item.Key).Append(separator).Append(item.Value).AppendLine()
        Next
        If str.Length > 0 Then str.Length -= 2
        Return str.ToString
    End Function
    ''' <summary>
    ''' Merge Dictionary Keys And Values Using User Defined Separator and vbcrlf
    ''' </summary>
    ''' <param name="x1"></param>
    ''' <param name="separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetText(x1 As Dictionary(Of String, List(Of String)), Optional separator As String = vbTab) As String
        If IsNothing(x1) = True OrElse x1.Count = 0 Then Return String.Empty
        Dim str As New System.Text.StringBuilder
        Dim out As New List(Of String)
        Dim Index As Integer = 0
        For Each i In x1
            str.Append(i.Key).Append(vbTab)
            If Index = 0 Then
                For Each s In i.Value
                    out.Add(s)
                Next
                Index += 1
            Else
                For i1 = 0 To i.Value.Count - 1
                    out(i1) = out(i1) & vbTab & i.Value(i1)
                Next
            End If
        Next
        str.AppendLine()
        str.Append(out.GetText(separator))
        Return str.ToString
    End Function
#End Region

#Region "Column"
    ''' <summary>
    ''' Create Dictionary From Lines and two Indexes
    ''' </summary>
    ''' <param name="Lines"></param>
    ''' <param name="Index1"></param>
    ''' <param name="index2"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function ToDictionary(Lines As IEnumerable(Of String), Index1 As Integer, index2 As Integer, Optional Separator As String = vbTab)
        Dim d As New Dictionary(Of String, String)
        For Each Line In Lines
            Dim s = Split(Line, Separator)
            d.Add(s(Index1), s(index2))
        Next
        Return d
    End Function
    ''' <summary>
    ''' Return the index of column, or -1
    ''' </summary>
    ''' <param name="Header"></param>
    ''' <param name="Name"></param>
    ''' <param name="s"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function Index(Header As IEnumerable(Of String), Name As String, s As Enums.TextMatch) As Integer
        Select Case s
            Case Enums.TextMatch.Contains
                For i = 0 To Header.Count - 1
                    If Header(i).Contains(Name) Then
                        Return i
                    End If
                Next
            Case Enums.TextMatch.Contains_SmallAndCapitalIsSame
                For i = 0 To Header.Count - 1
                    If Header(i).ToLower.Contains(Name.ToLower) Then
                        Return i
                    End If
                Next
            Case Enums.TextMatch.StartWith
                For i = 0 To Header.Count - 1
                    If Header(i).StartsWith(Name) Then
                        Return i
                    End If
                Next

            Case Enums.TextMatch.StartWith_SmallAndCapitalIsSame
                For i = 0 To Header.Count - 1
                    If Header(i).ToLower.StartsWith(Name.ToLower) Then
                        Return i
                    End If
                Next
            Case Enums.TextMatch.Exact
                For i = 0 To Header.Count - 1
                    If Header(i) = Name Then
                        Return i
                    End If
                Next
            Case Enums.TextMatch.Exact_SmallAndCapitalIsSame
                For i = 0 To Header.Count - 1
                    If Header(i).ToLower = Name.ToLower Then
                        Return i
                    End If
                Next
        End Select
        Return -1
    End Function

    ''' <summary>
    ''' Return the First index of column which contain both of the string
    ''' </summary>
    ''' <param name="Header"></param>
    ''' <param name="FirstString"></param>
    ''' <param name="SecondString"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function Index(Header As IEnumerable(Of String), FirstString As String, SecondString As String) As Integer
        For i1 = 0 To Header.Count - 1
            If Header(i1).ToUpper.Contains(FirstString.ToUpper) AndAlso Header(i1).ToUpper.Contains(SecondString.ToUpper) Then Return i1
        Next
        Return -1
    End Function

#End Region

#Region "ConvertToNumbers"
    ''' <summary>
    ''' Convert Enumerable of Strings into Enumerable Of Integer
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function ToInteger(ls As IEnumerable(Of String)) As IEnumerable(Of Integer)
        For Each s In ls
            Try
                Dim i = CInt(s)
                Yield i
            Catch ex As Exception

            End Try
        Next

    End Function
    ''' <summary>
    ''' Convert a String Into Double
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function ToDouble(ls As String) As Double
        Try
            Dim i = CDbl(ls)
            Return i
        Catch ex As Exception
            Return Double.NaN
        End Try


    End Function
    ''' <summary>
    ''' Convert Enumerable of Strings into Enumerable Of Double
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function ToDouble(ls As IEnumerable(Of String)) As IEnumerable(Of Double)
        For Each s In ls
            Yield s.ToDouble
        Next

    End Function
    ''' <summary>
    ''' Convert Enumerable of Strings into Enumerable Of Int16
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function ToInt16(ls As IEnumerable(Of String)) As IEnumerable(Of Int16)
        For Each s In ls
            Try
                Dim i = CShort(s)
                Yield i
            Catch ex As Exception

            End Try
        Next

    End Function
    ''' <summary>
    ''' Convert Enumerable of Strings into Enumerable Of Single
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function ToSingle(ls As IEnumerable(Of String)) As IEnumerable(Of Single)
        For Each s In ls
            Try
                Dim i = CSng(s)
                Yield i
            Catch ex As Exception

            End Try
        Next

    End Function
    ''' <summary>
    ''' Convert Enumerable of Strings into Enumerable Of Long (Int64)
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function ToInt64(ls As IEnumerable(Of String)) As IEnumerable(Of Int64)
        For Each s In ls
            Try
                Dim i = CULng(s)
                Yield i
            Catch ex As Exception

            End Try
        Next

    End Function
#End Region

#Region "Split"
    ''' <summary>
    ''' Split a String by User Selected Separator Into Enumerable of Integer
    ''' </summary>
    ''' <param name="s1"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function SplitToInteger(s1 As String, Optional Separator As String = vbTab) As IEnumerable(Of Integer)

        For Each s In Split(s1, Separator)
            Try
                Dim i = CInt(s)
                Yield i
            Catch ex As Exception

            End Try
        Next

    End Function
    ''' <summary>
    ''' Split a String by User Selected Separator Into Enumerable of Double
    ''' </summary>
    ''' <param name="s1"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function SplitToDouble(s1 As String, Optional Separator As String = vbTab) As IEnumerable(Of Double)
        For Each s In Split(s1, Separator)
            Try
                Dim i = CDbl(s)
                Yield i
            Catch ex As Exception

            End Try
        Next

    End Function
    ''' <summary>
    ''' Split a String by User Selected Separator Into Enumerable of Int16
    ''' </summary>
    ''' <param name="s1"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function SplitToInt16(s1 As String, Optional Separator As String = vbTab) As IEnumerable(Of Int16)
        For Each s In Split(s1, Separator)
            Try
                Dim i = CShort(s)
                Yield i
            Catch ex As Exception

            End Try
        Next

    End Function
    ''' <summary>
    ''' Split a String by User Selected Separator Into Enumerable of Single
    ''' </summary>
    ''' <param name="s1"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function SplitToSingle(s1 As String, Optional Separator As String = vbTab) As IEnumerable(Of Single)
        For Each s In Split(s1, Separator)
            Try
                Dim i = CSng(s)
                Yield i
            Catch ex As Exception

            End Try
        Next

    End Function
    ''' <summary>
    ''' Split a String by User Selected Separator Into Enumerable of Long (Int64)
    ''' </summary>
    ''' <param name="s1"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function SplitToInt64(s1 As String, Optional Separator As String = vbTab) As IEnumerable(Of Int64)
        For Each s In Split(s1, Separator)
            Try
                Dim i = CULng(s)
                Yield i
            Catch ex As Exception

            End Try
        Next

    End Function
#End Region

#Region "Remove"
    ''' <summary>
    ''' Yield all where there is no e,mpty string
    ''' </summary>
    ''' <param name="Strings"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function RemoveEmpty(Strings As IEnumerable(Of String)) As IEnumerable(Of String)
        For Each s In Strings
            If s <> String.Empty Then Yield s
        Next
    End Function



    ''' <summary>
    ''' Replace a SubStrings Into Empty Strings In StringS
    ''' </summary>
    ''' <param name="OriginalStringS"></param>
    ''' <param name="StringsToRemove"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function Remove(OriginalStrings As IEnumerable(Of String), StringsToRemove As IEnumerable(Of String)) As IEnumerable(Of String)
        For Each OriginalString In OriginalStrings
            Yield Remove(OriginalString, StringsToRemove)
        Next
    End Function
    ''' <summary>
    ''' Replace a SubStrings Into Empty Strings In String
    ''' </summary>
    ''' <param name="OriginalString"></param>
    ''' <param name="StringsToRemove"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function Remove(OriginalString As String, StringsToRemove As IEnumerable(Of String)) As String
        If IsNothing(StringsToRemove) = True Then Return OriginalString
        For Each StringToRemove In StringsToRemove
            OriginalString = OriginalString.Replace(StringToRemove, "")
        Next
        Return OriginalString
    End Function
    ''' <summary>
    ''' Replace a SubString Into Empty String In String
    ''' </summary>
    ''' <param name="OriginalString"></param>
    ''' <param name="StringToRemove"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function Remove(OriginalString As String, StringToRemove As String) As String
        Return OriginalString.Replace(StringToRemove, "")
    End Function
    ''' <summary>
    ''' Replace a SubString Into Empty Strings In StringS
    ''' </summary>
    ''' <param name="OriginalStrings"></param>
    ''' <param name="StringToRemove"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function Remove(OriginalStrings As IEnumerable(Of String), StringToRemove As String) As IEnumerable(Of String)

        For Each OriginalString In OriginalStrings
            Yield Remove(OriginalString, StringToRemove)
        Next
    End Function
#End Region

#Region "First Last Xth Parts"
    ''' <summary>
    ''' Get the first x Chars 
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="Length"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function FirstParts(ls As IEnumerable(Of String), Length As Integer) As IEnumerable(Of String)
        For Each s In ls
            Yield FirstPart(s, Length)
        Next
    End Function
    ''' <summary>
    ''' Get the first x Chars 
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="Length"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function FirstPart(s As String, Length As Integer) As String
        If s.Length >= Length Then
            Return s.Substring(0, Length)
        Else
            Return s
        End If
    End Function

    ''' <summary>
    ''' Get the first part after splitted by separator
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function FirstParts(ls As IEnumerable(Of String), Separator As String) As IEnumerable(Of String)
        For Each s In ls
            Yield FirstPart(s, Separator)
        Next
    End Function
    ''' <summary>
    ''' Get the first part after splitted by separator
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function FirstPart(s As String, Separator As String) As String
        Return Split(s, Separator).First
    End Function

    ''' <summary>
    ''' Get the last part after splitted by separator
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function LastParts(ls As IEnumerable(Of String), Separator As String) As IEnumerable(Of String)
        For Each s In ls
            Yield LastPart(s, Separator)
        Next
    End Function
    ''' <summary>
    ''' Get the last part after splitted by separator
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function LastPart(s As String, Separator As String) As String
        Return Split(s, Separator).Last
    End Function

    ''' <summary>
    ''' Get the Xth part after splitted by separator
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function XthParts(ls As IEnumerable(Of String), Separator As String, Index As Integer) As IEnumerable(Of String)
        For Each s In ls
            Yield XthPart(s, Separator, Index)
        Next
    End Function
    ''' <summary>
    ''' Get the Xth part after splitted by separator
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function XthPart(s As String, Separator As String, Index As Integer) As String
        Try
            Return Split(s, Separator)(Index)
        Catch ex As Exception
            Return String.Empty
        End Try

    End Function

    ''' <summary>
    ''' Get the last part after splitted by separator
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function NotLastParts(ls As IEnumerable(Of String), Separator As String) As IEnumerable(Of String)
        For Each s In ls
            Yield NotLastPart(s, Separator)
        Next
    End Function
    ''' <summary>
    ''' Get the last part after splitted by separator
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function NotLastPart(s As String, Separator As String) As String
        Dim Index = s.LastIndexOf(Separator)
        If Index > -1 Then
            Return s.Substring(0, Index)
        Else
            Return s
        End If
    End Function

    ''' <summary>
    ''' Get the last part after splitted by separator
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function NotFirstParts(ls As IEnumerable(Of String), Separator As String) As IEnumerable(Of String)
        For Each s In ls
            Yield NotFirstPart(s, Separator)
        Next
    End Function
    ''' <summary>
    ''' Get the last part after splitted by separator
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="Separator"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function NotFirstPart(s As String, Separator As String) As String
        Dim Index = s.IndexOf(Separator)
        If Index > -1 Then
            Return s.Substring(Index + 1, s.Length - Index - 1)
        Else
            Return s
        End If
    End Function
#End Region

#Region "Insert"
    ''' <summary>
    ''' Insert a SubString Before Every Enumerable of Strings
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="Before"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function InsertBefore(ls As IEnumerable(Of String), Before As String) As IEnumerable(Of String)
        For Each s In ls
            Yield Before & s
        Next
    End Function
    ''' <summary>
    ''' Insert a SubString After Every Enumerable of Strings
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="After"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function InsertAfter(ls As IEnumerable(Of String), After As String) As IEnumerable(Of String)
        For Each s In ls
            Yield s & After
        Next
    End Function
    ''' <summary>
    ''' Insert a SubString Before And After Every Enumerable of Strings
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="BeforeAfter"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function InsertBeforeAfter(ls As IEnumerable(Of String), BeforeAfter As String) As IEnumerable(Of String)
        For Each s In ls
            Yield BeforeAfter & s & BeforeAfter
        Next
    End Function
    ''' <summary>
    ''' Insert a SubString before and Insert a SubString After Every Enumerable of Strings
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="Before"></param>
    ''' <param name="After"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function InsertBeforeAfter(ls As IEnumerable(Of String), Before As String, After As String) As IEnumerable(Of String)
        For Each s In ls
            Yield Before & s & After
        Next
    End Function
#End Region

#Region "Trim"
    ''' <summary>
    ''' Trim IEnumarable of String with Pattern from Both End of strings
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="ToTrim"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function Trim(ByVal ls As IEnumerable(Of String), ToTrim As String) As IEnumerable(Of String)
        For Each s In ls
            Yield s.Trim(ToTrim)
        Next
    End Function
    ''' <summary>
    ''' Trim IEnumarable of String with Patterns from Both End of strings
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="ToTrims"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function Trim(ByVal ls As IEnumerable(Of String), ToTrims As IEnumerable(Of String)) As IEnumerable(Of String)
        For Each s In ls
            For Each ToTrim In ToTrims
                s = s.Trim(ToTrim)
            Next
            Yield s
        Next
    End Function
    ''' <summary>
    ''' Trim IEnumarable of String with Pattern from Start of strings
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="ToTrim"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function TrimStart(ByVal ls As IEnumerable(Of String), ToTrim As String) As IEnumerable(Of String)
        For Each s In ls
            Yield s.TrimStart(ToTrim)
        Next
    End Function
    ''' <summary>
    ''' Trim IEnumarable of String with Patterns from Start of strings
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="ToTrims"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function TrimStart(ByVal ls As IEnumerable(Of String), ToTrims As IEnumerable(Of String)) As IEnumerable(Of String)
        For Each s In ls
            For Each ToTrim In ToTrims
                s = s.TrimStart(ToTrim)
            Next
            Yield s
        Next
    End Function

    ''' <summary>
    ''' Trim IEnumarable of String with Pattern from End of strings
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="ToTrim"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function TrimEnd(ByVal ls As IEnumerable(Of String), ToTrim As String) As IEnumerable(Of String)
        For Each s In ls
            Yield s.TrimEnd(ToTrim)
        Next
    End Function
    ''' <summary>
    ''' Trim IEnumarable of String with Patterns from End of strings
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="ToTrims"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function TrimEnd(ByVal ls As IEnumerable(Of String), ToTrims As IEnumerable(Of String)) As IEnumerable(Of String)
        For Each s In ls
            For Each ToTrim In ToTrims
                s = s.TrimEnd(ToTrim)
            Next
            Yield s
        Next
    End Function
#End Region

    <Extension()>
    Public Function aZ09_(ByVal str As String) As String
        Return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled)
    End Function
    ''' <summary>
    ''' Multiply a String
    ''' </summary>
    ''' <param name="s"></param>
    ''' <param name="Nof"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function Multiply(s As String, Nof As Integer) As String
        Dim str As New System.Text.StringBuilder
        For i1 = 1 To Nof
            str.Append(s)
        Next
        Return str.ToString
    End Function

    ''' <summary>
    ''' Using RegExp It Iterate every Match
    ''' </summary>
    ''' <param name="ToSearch"></param>
    ''' <param name="Pattern"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function Matches(ToSearch As String, Pattern As String) As IEnumerable(Of Match)
        Dim TheMatches = Regex.Matches(ToSearch, Pattern, RegexOptions.IgnoreCase)
        Dim TheMatch = Regex.Match(ToSearch, Pattern, RegexOptions.IgnoreCase)

        For Each match As Match In TheMatches
            Yield match
        Next
    End Function
    ''' <summary>
    ''' Return a New,Cloned Enumerable of String
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <returns></returns>
    <Extension()>
    Public Iterator Function CloneStrings(ls As IEnumerable(Of String)) As IEnumerable(Of String)
        Dim out As New List(Of String)
        For Each Item In ls
            Dim s As String = Item
            Yield s
        Next

    End Function

    ''' <summary>
    ''' Invariant Culture, Ignore Case
    ''' </summary>
    ''' <param name="ls"></param>
    ''' <param name="Item"></param>
    ''' <returns></returns>
    <Extension>
    Public Function HasContain(ls As IEnumerable(Of String), Item As String) As Boolean
        Dim res = From x In ls Where x.IndexOf(Item, comparisonType:=StringComparison.InvariantCultureIgnoreCase) > -1
        If res.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    <Extension()>
    Public Function GetConstants(ByVal type As Type) As IEnumerable(Of FieldInfo)
        Dim fieldInfos = type.GetFields(BindingFlags.[Public] Or BindingFlags.[Static] Or BindingFlags.FlattenHierarchy)
        Return fieldInfos.Where(Function(fi) fi.IsLiteral AndAlso Not fi.IsInitOnly)
    End Function

    <Extension()>
    Public Function GetConstantsValues(Of T As Class)(ByVal type As Type) As IEnumerable(Of T)
        Dim fieldInfos = GetConstants(type)
        Return fieldInfos.[Select](Function(fi) TryCast(fi.GetRawConstantValue(), T))
    End Function
End Module
