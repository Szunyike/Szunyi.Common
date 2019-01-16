Imports System.IO
''' <summary>
''' These Class are Used For General OneToOne and OneToMany DataTables
''' </summary>
Public Class CrossRefs
    Public Class CrossRefBuilders
        Public Property OneToOneByFirstComparer As New ComparerOfCrossrefOneToOneByFirst
        Public Property OneToOneBySecondComparer As New ComparerOfCrossRefOneToOneBySecond
        Public Property OneToManyComparer As New ComparerOfCrossRefOneToMany
        Public Shared Function Get_Distinct_Seconds(Keys As List(Of String), x As List(Of CrossRefOneToOne)) As List(Of String)
            Dim tmp As New List(Of String)
            Dim comp As New ComparerOfCrossrefOneToOneByFirst
            Dim basic As New CrossRefOneToOne("", "")
            For Each Key In Keys
                basic.First = Key
                Dim Index = x.BinarySearch(basic, comp)
                If Index > -1 Then
                    tmp.Add(x(Index).Second)
                End If
            Next
            If tmp.Count > 0 Then
                Return tmp.Distinct.ToList
            Else
                Return New List(Of String)
            End If
        End Function

        Public Function GetOneToManyByFirst(tmp As List(Of CrossRefOneToOne)) As List(Of CrossRefOneToMany)
            tmp.Sort(OneToOneByFirstComparer)
            Dim out As New List(Of CrossRefOneToMany)
            Dim tmpS = tmp.First.First
            out.Add(New CrossRefOneToMany(tmpS))
            For Each Item In tmp
                If Item.First = tmpS Then
                    out.Last.Many.Add(Item.Second)
                Else
                    out.Add(New CrossRefOneToMany(Item.First))
                    out.Last.Many.Add(Item.Second)
                    tmpS = Item.First
                End If
            Next
            Return out
        End Function

        Public Function GetOneToManyBySecond(tmp As List(Of CrossRefOneToOne)) As List(Of CrossRefOneToMany)
            tmp.Sort(OneToOneByFirstComparer)
            Dim out As New List(Of CrossRefOneToMany)
            Dim tmpS = tmp.First.Second
            out.Add(New CrossRefOneToMany(tmpS))
            For Each Item In tmp
                If Item.Second = tmpS Then
                    out.Last.Many.Add(Item.First)
                Else
                    out.Add(New CrossRefOneToMany(Item.Second))
                    out.Last.Many.Add(Item.First)
                    tmpS = Item.Second
                End If
            Next
            Return out
        End Function

        ''' <summary>
        ''' Get Path And FileName
        ''' One After VbTab After Many Separated By Comma 
        ''' </summary>
        ''' <param name="destinationPath"></param>
        ''' <param name="FileName"></param>
        ''' <param name="OneToMany"></param>
        Public Shared Sub SaveOneToMany(destinationPath As String, FileName As String, OneToMany As List(Of CrossRefOneToMany), Optional Separator As String = ";")
            Dim File As New FileInfo(destinationPath & "\" & FileName)
            '   SaveOneToMany(File, OneToMany, Separator)
        End Sub
        ''' <summary>
        ''' Get Full FileName
        ''' One After VbTab After Many Separated By Comma 
        ''' </summary>
        ''' <param name="FileName"></param>
        ''' <param name="OneToMany"></param>
        Friend Sub SaveOneToMany(FileName As String, OneToMany As List(Of CrossRefOneToMany), Optional Separator As String = ";")
            Dim File As New FileInfo(FileName)
            '     SaveOneToMany(File, OneToMany, Separator)
        End Sub
        ''' <summary>
        ''' Return Corresponding Values From OneToMany And OneToOne
        ''' </summary>
        ''' <param name="crossRefOneToMany"></param>
        ''' <param name="crossRefOneToOne"></param>
        ''' <returns></returns>
        Public Function GetValues(crossRefOneToMany As CrossRefOneToMany, crossRefOneToOne As List(Of CrossRefOneToOne)) As List(Of String)
            Dim Out As New List(Of String)
            Dim x As New CrossRefOneToOne("", "")
            For Each Item In crossRefOneToMany.Many
                x.First = Item
                Dim Index = crossRefOneToOne.BinarySearch(x, OneToOneByFirstComparer)
                If Index > -1 Then
                    Out.Add(crossRefOneToOne(Index).Second)
                Else
                    Dim half As Int16 = 54
                End If

            Next
            Return Out
        End Function

        Public Shared Function GetOneToMany(File As FileInfo, OneToMany As List(Of CrossRefOneToMany), Optional Separator As String = ";") As String
            Dim str As New System.Text.StringBuilder
            For Each Item In OneToMany
                str.Append(Item.One).Append(vbTab)
                For Each Many In Item.Many
                    str.Append(Many).Append(Separator)
                Next
                str.Length -= Separator.Length
                str.AppendLine()
            Next
            str.Length -= 2
            Return str.ToString
        End Function


        Public Shared Function GetOneToManyFromFile(Lines As IEnumerable(Of String), Optional Separator As String = ";") As List(Of CrossRefOneToMany)
            Dim out As New List(Of CrossRefOneToMany)
            For Each Line In Lines
                Dim s = Split(Line, vbTab)
                Dim s1 = Split(s.Last, Separator)
                out.Add(New CrossRefOneToMany(s.First, s1))
            Next
            Return out
        End Function

        Public Shared Function GetOneToOneFromFile(Lines As IEnumerable(Of String)) As List(Of CrossRefOneToOne)
            Dim out As New List(Of CrossRefOneToOne)
            For Each Line In Lines
                Dim s = Split(Line, vbTab)

                out.Add(New CrossRefOneToOne(s.First, s.Last))
            Next
            Return out

        End Function
        Public Shared Function GetOneToOneFromFile(Lines As IEnumerable(Of String), IndexOfFirst As Integer, indexOfSecond As Integer, FirstLine As Integer) As List(Of CrossRefOneToOne)
            Dim out As New List(Of CrossRefOneToOne)
            For Each Line In Lines
                Dim s = Split(Line, vbTab)

                out.Add(New CrossRefOneToOne(s(IndexOfFirst), s(indexOfSecond)))
            Next
            out.Sort(New CrossRefs.ComparerOfCrossrefOneToOneByFirst)
            Return out

        End Function
        Public Shared Sub SaveOneToOne(FileName As String, x As List(Of CrossRefOneToOne))
            Dim str As New System.Text.StringBuilder
            For Each Item In x
                str.Append(Item.First).Append(vbTab).Append(Item.Second).Append(vbCrLf)
            Next
            If str.Length > 1 Then str.Length -= vbCrLf.Length
            If str.Length > 0 Then
                '  Szunyi.IO.Export.SaveText(str.ToString, New FileInfo(FileName))
            End If
        End Sub
        ''' <summary>
        ''' True Sort By First or False Sort By Second
        ''' </summary>
        ''' <param name="IsByFirst"></param>
        ''' <param name="Original"></param>
        ''' <returns></returns>
        Friend Function GetSortedOneToOne(IsByFirst As Boolean, Original As List(Of CrossRefOneToOne)) As List(Of CrossRefOneToOne)
            Dim out As New List(Of CrossRefOneToOne)
            For Each Item In Original
                out.Add(Item)
            Next
            If IsByFirst = True Then
                out.Sort(OneToOneByFirstComparer)
                Return out
            Else
                out.Sort(OneToOneBySecondComparer)
                Return FlipOneToOne(out)
            End If
        End Function
        Public Shadows Function FlipOneToOne(x As List(Of CrossRefOneToOne))
            Dim out As New List(Of CrossRefOneToOne)
            For Each Item In x
                out.Add(New CrossRefOneToOne(Item.Second, Item.First))
            Next
            Return out
        End Function
        ''' <summary>
        ''' If True Then Short The Forst If False Change the Second To Short LocusTag
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="IsFirst"></param>
        ''' <returns></returns>
        Public Function GetwShortLocusTag(x As List(Of CrossRefOneToOne), IsFirst As Boolean) As List(Of CrossRefOneToOne)
            Dim out As New List(Of CrossRefOneToOne)
            If IsFirst = True Then
                For Each Item In x
                    Dim t As New CrossRefOneToOne(Split(Item.First, ".").First, Item.Second)
                    out.Add(t)
                Next
            Else
                For Each Item In x
                    Dim t As New CrossRefOneToOne(Item.Second, Split(Item.First, ".").First)
                    out.Add(t)
                Next
            End If
            out.Sort(Me.OneToOneByFirstComparer)
            Return out
        End Function
    End Class

    Public Class CrossRefOneToOne
        Public Property First As String
        Public Property Second As String
        Public Sub New(First As String, Second As String)
            Me.First = First
            Me.Second = Second
        End Sub
    End Class

    ''' <summary>
    ''' Sort/Find By The First Item in One To One Relation
    ''' </summary>
    Public Class ComparerOfCrossrefOneToOneByFirst
        Implements IComparer(Of CrossRefOneToOne)

        Public Function Compare(x As CrossRefOneToOne, y As CrossRefOneToOne) As Integer Implements IComparer(Of CrossRefOneToOne).Compare
            If IsNothing(x.First) = True Then Return -1
            If IsNothing(y.First) = True Then Return 1
            Return x.First.CompareTo(y.First)
        End Function
    End Class
    ''' <summary>
    ''' Sort/Find By The Second Item in One To One Relation
    ''' </summary>
    Public Class ComparerOfCrossRefOneToOneBySecond
        Implements IComparer(Of CrossRefOneToOne)

        Public Function Compare(x As CrossRefOneToOne, y As CrossRefOneToOne) As Integer Implements IComparer(Of CrossRefOneToOne).Compare
            Return x.Second.CompareTo(y.Second)
        End Function
    End Class
    Public Class CrossRefOneToMany
        Public One As String
        Public Many As New Generic.List(Of String)
        Public Sub New(One As String)
            Me.One = One
        End Sub

        Public Sub New(One As String, s1() As String)
            Me.One = One
            Me.Many = s1.ToList
        End Sub
        Public Sub New(One As String, s1 As Generic.List(Of String))
            Me.One = One
            Me.Many = s1
        End Sub
    End Class
    ''' <summary>
    ''' Sort/Find By The ONE Item in One To Many Relation
    ''' </summary>
    Public Class ComparerOfCrossRefOneToMany
        Implements IComparer(Of CrossRefOneToMany)

        Public Function Compare(x As CrossRefOneToMany, y As CrossRefOneToMany) As Integer Implements IComparer(Of CrossRefOneToMany).Compare
            Return x.One.CompareTo(y.One)
        End Function
    End Class
End Class
