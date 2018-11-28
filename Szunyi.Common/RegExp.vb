Imports System.Text.RegularExpressions

Public Class RegExp
    Public Shared Iterator Function Get_Matches(ToSearch As String, Pattern As String) As IEnumerable(Of Match)
        Dim TheMatches = Regex.Matches(ToSearch, Pattern, RegexOptions.IgnoreCase)
        Dim TheMatch = Regex.Match(ToSearch, Pattern, RegexOptions.IgnoreCase)

        For Each match As Match In TheMatches
            Yield match

        Next


    End Function

End Class
