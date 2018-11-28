Imports System.Runtime.CompilerServices
Imports Bio.IO.GenBank
Imports Szunyi.Common.Enums

Public Class Enums
    Public Enum SearchType
        Exact = 0
        Contains = 1
        NoValue = 2
        NotConsistOf = 3
        NotExactValue = 4
    End Enum

    <Flags()>
    Public Enum Locations_By As Integer
        TSS = 1
        PAS = 2
        LS = 4
        LE = 8
        Intron = 16

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

Public Module Double_Extensions
    <Extension()>
    Public Function ToString_Decimal(x As Double)
        Return x.ToString.Replace(",", ".")
    End Function
    <Extension()>
    Public Function GetDouble(ByVal doublestring As String) As Double
        Dim retval As Double
        Dim sep As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

        Double.TryParse(Replace(Replace(doublestring, ".", sep), ",", sep), retval)
        Return retval
    End Function

End Module

Public Module Location_Extension
    <Extension()>
    Public Function TSS(Location As Bio.IO.GenBank.Location) As Integer
        If Location.Operator = LocationOperator.Complement Then
            Return Location.LocationEnd
        ElseIf Location.Operator = LocationOperator.Join AndAlso Location.SubLocations.Count > 0 AndAlso Location.SubLocations.First.Operator = LocationOperator.Complement Then
            Return Location.LocationEnd
        Else
            Return Location.LocationStart
        End If
    End Function
    <Extension()>
    Public Function PAS(Location As Bio.IO.GenBank.Location) As Integer
        If Location.Operator = LocationOperator.Complement Then
            Return Location.LocationStart
        ElseIf Location.Operator = LocationOperator.Join AndAlso Location.SubLocations.Count > 0 AndAlso Location.SubLocations.First.Operator = LocationOperator.Complement Then
            Return Location.LocationStart
        Else
            Return Location.LocationEnd
        End If
    End Function
    <Extension()>
    Public Function IsComplementer(Location As Bio.IO.GenBank.Location) As Boolean
        If Location.Operator = LocationOperator.Complement Then
            Return True
        ElseIf Location.Operator = LocationOperator.Join AndAlso Location.SubLocations.Count > 0 AndAlso Location.SubLocations.First.Operator = LocationOperator.Complement Then
            Return True
        Else
            Return False
        End If
    End Function
    <Extension()>
    Public Function Current_Position(Location As Bio.IO.GenBank.Location, Loc_By As Locations_By) As Integer
        Select Case Loc_By
            Case Locations_By.LE
                Return Location.LocationEnd
            Case Locations_By.LS
                Return Location.LocationStart
            Case Locations_By.PAS
                Return Location.PAS
            Case Locations_By.TSS
                Return Location.TSS
            Case Else
                Return 0
        End Select

    End Function

    <Extension()>
    Public Function TSS(Location As Bio.IO.GenBank.ILocation) As Integer
        If Location.Operator = LocationOperator.Complement Then
            Return Location.LocationEnd
        ElseIf Location.Operator = LocationOperator.Join AndAlso Location.SubLocations.Count > 0 AndAlso Location.SubLocations.First.Operator = LocationOperator.Complement Then
            Return Location.LocationEnd
        Else
            Return Location.LocationStart
        End If
    End Function
    <Extension()>
    Public Function PAS(Location As Bio.IO.GenBank.ILocation) As Integer
        If Location.Operator = LocationOperator.Complement Then
            Return Location.LocationStart
        ElseIf Location.Operator = LocationOperator.Join AndAlso Location.SubLocations.Count > 0 AndAlso Location.SubLocations.First.Operator = LocationOperator.Complement Then
            Return Location.LocationStart
        Else
            Return Location.LocationEnd
        End If
    End Function
    <Extension()>
    Public Function IsComplementer(Location As Bio.IO.GenBank.ILocation) As Boolean
        If Location.Operator = LocationOperator.Complement Then
            Return True
        ElseIf Location.Operator = LocationOperator.Join AndAlso Location.SubLocations.Count > 0 AndAlso Location.SubLocations.First.Operator = LocationOperator.Complement Then
            Return True
        Else
            Return False
        End If
    End Function
    <Extension()>
    Public Function Current_Position(Location As Bio.IO.GenBank.ILocation, Loc_By As Locations_By) As Integer
        Select Case Loc_By
            Case Locations_By.LE
                Return Location.LocationEnd
            Case Locations_By.LS
                Return Location.LocationStart
            Case Locations_By.PAS
                Return Location.PAS
            Case Locations_By.TSS
                Return Location.TSS
            Case Else
                Return 0
        End Select
    End Function
End Module


