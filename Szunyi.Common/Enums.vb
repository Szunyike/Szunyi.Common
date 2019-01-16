Imports System.Runtime.CompilerServices
Imports Bio.IO.GenBank
Imports Szunyi.Common.Enums

''' <summary>
''' Contains Enumeration For Search, Operarators
''' </summary>
Public Class Enums
    Public Enum SearchType
        Exact = 0
        Contains = 1
        NoValue = 2
        NotConsistOf = 3
        NotExactValue = 4
    End Enum

    Public Enum TextMatch
        Exact = 1
        Exact_SmallAndCapitalIsSame = 2
        Contains = 3
        Contains_SmallAndCapitalIsSame = 4
        StartWith = 5
        StartWith_SmallAndCapitalIsSame = 6
    End Enum

    Public Enum Filter
        Bigger = 0
        Bigger_or_Equal = 1
        Smaller = 2
        Smaller_or_Equal = 3
        Equal = 4
        Start_with = 5
        Not_Start_with = 6
        End_with = 7
        Not_End_with = 8
        Contain = 9
        Not_Contain = 10
    End Enum
End Class




