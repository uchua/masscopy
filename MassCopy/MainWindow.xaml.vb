Imports System.Windows.Forms

Class MainWindow
    ' Subs
    Private Sub ListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Private Sub ListBox_SelectionChanged_1(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Private Sub CopyFromAddFile_Click(sender As Object, e As RoutedEventArgs) Handles CopyFromAddFile.Click
        Dim ofd As New OpenFileDialog
        ofd.Multiselect = True

        If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            For Each F In ofd.FileNames
                If Not CopyFromList.Items.Contains(F) Then
                    CopyFromList.Items.Add(F)
                End If
            Next
        End If
    End Sub

    Private Sub CopyFromAddFolder_Click(sender As Object, e As RoutedEventArgs) Handles CopyFromAddFolder.Click
        Dim fbd As New FolderBrowserDialog

        If fbd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CopyFromList.Items.Add(fbd.SelectedPath)
        End If
    End Sub

    Private Sub CopyFromSub_Click(sender As Object, e As RoutedEventArgs) Handles CopyFromSub.Click
        While CopyFromList.SelectedItems.Count > 0
            CopyFromList.Items.Remove(CopyFromList.SelectedItem)
        End While
    End Sub

    Private Sub CopyToAdd_Click(sender As Object, e As RoutedEventArgs) Handles CopyToAdd.Click
        Dim fbd As New FolderBrowserDialog

        If fbd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CopyToList.Items.Add(fbd.SelectedPath)
        End If
    End Sub

    Private Sub CopyToSub_Click(sender As Object, e As RoutedEventArgs) Handles CopyToSub.Click
        While CopyToList.SelectedItems.Count > 0
            CopyToList.Items.Remove(CopyToList.SelectedItem)
        End While
    End Sub

    Private Sub BeginCopy_Click(sender As Object, e As RoutedEventArgs) Handles BeginCopy.Click
        Dim threads = New List(Of CopierThread)

        For Each P As String In CopyToList.Items
            threads.Add(New CopierThread(P, CopyFromList.Items))
        Next
    End Sub
End Class
