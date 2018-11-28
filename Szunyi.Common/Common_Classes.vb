Public Class Common_Classes
    <System.ComponentModel.Browsable(False)>
    Public Class KeyObject
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Property Key As String
        <System.ComponentModel.Browsable(False)>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Property Obj As Object
        Public Sub New(key As String, obj As Object)
            Me.Key = key
            Me.Obj = obj

        End Sub
    End Class
    Public Class partialIDs
        Public Property partialIDs As New List(Of PartialID)


        Public Sub New(newID_oldID As Dictionary(Of String, String))
            For Each Item In newID_oldID
                partialIDs.Add(New PartialID(Item.Value, Item.Key))
            Next
        End Sub

        Public Sub New(lines As IEnumerable(Of String), indexOfSeqID As Integer, indexOfNewName As Integer)
            For Each Line In lines
                Dim s = Split(Line, vbTab)
                Me.partialIDs.Add(New PartialID(s(indexOfSeqID), s(indexOfNewName)))
            Next
        End Sub

        Public Function GetID(name As String) As List(Of String)
            Dim out As New List(Of String)
            For Each Item In Me.partialIDs
                Dim Index = name.IndexOf(Item.partialID, comparisonType:=StringComparison.InvariantCultureIgnoreCase)
                If Index > -1 Then
                    out.Add(Item.ID)
                End If
            Next
            Return out
        End Function
    End Class
    Public Class PartialID
        Public Property partialID As String
        Public Property ID As String
        Public Sub New(partialID As String, ID As String)
            Me.partialID = partialID
            Me.ID = ID
        End Sub

    End Class
End Class
