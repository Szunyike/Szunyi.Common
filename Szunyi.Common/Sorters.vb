Namespace Sorters
    Public Class ByteArray
        Implements IEqualityComparer(Of Byte())
        Implements IComparer(Of Byte())


        Public Function Compare(x() As Byte, y() As Byte) As Integer Implements IComparer(Of Byte()).Compare
            Dim result As Integer
            Dim min = System.Math.Min(x.Count, y.Count)
            For index As Integer = 0 To min - 1
                result = x(index).CompareTo(y(index))
                If result <> 0 Then Return result
            Next

            Return x.Count.CompareTo(y.Count)
        End Function

        Public Function Equals(x() As Byte, y() As Byte) As Boolean Implements IEqualityComparer(Of Byte()).Equals
            If Compare(x, y) = 0 Then Return True
            Return False
        End Function

        Public Function GetHashCode(obj() As Byte) As Integer Implements IEqualityComparer(Of Byte()).GetHashCode
            Return obj.First
        End Function
    End Class


End Namespace
