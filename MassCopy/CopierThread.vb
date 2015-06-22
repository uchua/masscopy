Imports System.Threading

Public Class CopierThread
    Private destination As String
    Private items As System.Windows.Controls.ItemCollection
    Public Thread As Thread

    Public Sub New(DestinationFolder As String, CopiedItems As System.Windows.Controls.ItemCollection)
        destination = DestinationFolder
        items = CopiedItems
    End Sub

    Private Sub ThreadProc()
        For Each D As String In items
            If (System.IO.Directory.Exists(D)) Then
                ' Copy the directory to its destination
            ElseIf (System.IO.File.Exists(D)) Then
                ' Copy the file to its destination
            End If
        Next
    End Sub
End Class
