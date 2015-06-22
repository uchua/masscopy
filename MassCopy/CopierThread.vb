Imports System.Threading
Imports System.IO

Public Class CopierThread
    Private destination As String
    Private items As System.Windows.Controls.ItemCollection
    Public Thread As Thread

    Public Sub New(DestinationFolder As String, CopiedItems As System.Windows.Controls.ItemCollection)
        Me.destination = DestinationFolder
        Me.items = CopiedItems
        Me.Thread = New Thread(AddressOf ThreadProc)
    End Sub

    Private Sub ThreadProc()
        For Each D As String In Me.items
            If (System.IO.Directory.Exists(D)) Then
                ' Copy the directory to its destination
                Dim DestinationDirectory As String = Me.destination + "\" + GetDirNameFromPath(D)

                My.Computer.FileSystem.CopyDirectory(D, DestinationDirectory, False)
            ElseIf (System.IO.File.Exists(D)) Then
                ' Copy the file to its destination
                Dim DestinationFile As String = Me.destination + "\" + Path.GetFileName(D)

                My.Computer.FileSystem.CopyFile(D, DestinationFile, False)
            End If
        Next
    End Sub
End Class
