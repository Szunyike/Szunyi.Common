Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Text.RegularExpressions

Public Module Extensions
#Region "GetText"

    <Extension()>
    Public Function GetText(Ls As IEnumerable(Of String), Optional separator As String = vbTab) As String
        Dim str As New StringBuilder
        For Each s In Ls
            str.Append(s).Append(separator)
        Next
        If str.Length > 0 Then str.Length -= separator.Length
        Return str.ToString
    End Function
    <Extension()>
    Public Function GetText(Ls As IEnumerable(Of Long), Optional separator As String = vbTab) As String
        Dim str As New StringBuilder
        For Each s In Ls
            str.Append(s).Append(separator)
        Next
        If str.Length > 0 Then str.Length -= separator.Length
        Return str.ToString
    End Function
    <Extension()>
    Public Function GetText(Ls As IEnumerable(Of Double), Optional separator As String = vbTab) As String
        Dim str As New StringBuilder
        For Each s In Ls
            str.Append(s).Append(separator)
        Next
        If str.Length > 0 Then str.Length -= separator.Length
        Return str.ToString
    End Function
    <Extension()>
    Public Function GetText(Ls As IEnumerable(Of Integer), Optional separator As String = vbTab) As String
        Dim str As New StringBuilder
        For Each s In Ls
            str.Append(s).Append(separator)
        Next
        If str.Length > 0 Then str.Length -= separator.Length
        Return str.ToString
    End Function
    Public Function GetText(Bytes As List(Of Byte)) As String
        Return System.Text.Encoding.ASCII.GetString(Bytes.ToArray())
    End Function
    <Extension()>
    Public Function GetText(x1 As Dictionary(Of String, Integer), Optional separator As String = vbTab) As String
        If IsNothing(x1) = True OrElse x1.Count = 0 Then Return String.Empty
        Dim str As New System.Text.StringBuilder
        For Each item In x1
            str.Append(item.Key).Append(separator).Append(item.Value).AppendLine()
        Next
        If str.Length > 0 Then str.Length -= 2
        Return str.ToString
    End Function
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
    <Extension()>
    Public Iterator Function ToDouble(ls As IEnumerable(Of String)) As IEnumerable(Of Double)
        For Each s In ls
            Try
                Dim i = CDbl(s)
                Yield i
            Catch ex As Exception

            End Try
        Next

    End Function
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
    <Extension()>
    Public Iterator Function Remove(OriginalStrings As IEnumerable(Of String), StringsToRemove As IEnumerable(Of String)) As IEnumerable(Of String)
        For Each OriginalString In OriginalStrings
            Yield Remove(OriginalString, StringsToRemove)
        Next
    End Function
    <Extension()>
    Public Function Remove(OriginalString As String, StringsToRemove As IEnumerable(Of String)) As String
        If IsNothing(StringsToRemove) = True Then Return OriginalString
        For Each StringToRemove In StringsToRemove
            OriginalString = OriginalString.Replace(StringToRemove, "")
        Next
        Return OriginalString
    End Function
    <Extension()>
    Public Function Remove(OriginalString As String, StringToRemove As String) As String
        Return OriginalString.Replace(StringToRemove, "")
    End Function
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
    <Extension()>
    Public Iterator Function InsertBefore(ls As IEnumerable(Of String), Before As String) As IEnumerable(Of String)
        For Each s In ls
            Yield Before & s
        Next
    End Function
    <Extension()>
    Public Iterator Function InsertAfter(ls As IEnumerable(Of String), After As String) As IEnumerable(Of String)
        For Each s In ls
            Yield s & After
        Next
    End Function
    <Extension()>
    Public Iterator Function InsertBeforeAfter(ls As IEnumerable(Of String), BeforeAfter As String) As IEnumerable(Of String)
        For Each s In ls
            Yield BeforeAfter & s & BeforeAfter
        Next
    End Function
    <Extension()>
    Public Iterator Function InsertBeforeAfter(ls As IEnumerable(Of String), Before As String, After As String) As IEnumerable(Of String)
        For Each s In ls
            Yield Before & s & After
        Next
    End Function
#End Region

    <Extension()>
    Public Function aZ09_(ByVal str As String) As String
        Return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled)
    End Function
    <Extension()>
    Public Function Multiply(s As String, Nof As Integer) As String
        Dim str As New System.Text.StringBuilder
        For i1 = 1 To Nof
            str.Append(s)
        Next
        Return str.ToString
    End Function

    <Extension()>
    Public Iterator Function Trim(ByVal ls As IEnumerable(Of String), ToTrim As String) As IEnumerable(Of String)
        For Each s In ls
            Yield s.Trim(ToTrim)
        Next
    End Function
    <Extension()>
    Public Iterator Function Trim(ByVal ls As IEnumerable(Of String), ToTrims As IEnumerable(Of String)) As IEnumerable(Of String)
        For Each s In ls
            For Each ToTrim In ToTrims
                s = s.Trim(ToTrim)
            Next
            Yield s
        Next
    End Function
End Module
