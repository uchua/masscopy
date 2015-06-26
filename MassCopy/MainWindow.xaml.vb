Imports System.Windows.Forms

Class MainWindow

    ' Subs
    Private Sub ListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Private Sub ListBox_SelectionChanged_1(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Private Sub CopyFromAddFile_Click(sender As Object, e As RoutedEventArgs) Handles CopyFromAddFile.Click
        If System.IO.File.Exists(CopyFromTextBox.Text) Then
            If Not CopyFromList.Items.Contains(CopyFromTextBox.Text) Then
                CopyFromList.Items.Add(CopyFromTextBox.Text)
            End If
        ElseIf System.IO.Directory.Exists(CopyFromTextBox.Text) Then
            If Not CopyFromList.Items.Contains(CopyFromTextBox.Text) Then
                CopyFromList.Items.Add(CopyFromTextBox.Text)
            End If
        Else
            Dim ofd As New OpenFileDialog
            ofd.Multiselect = True

            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                For Each F In ofd.FileNames
                    If Not CopyFromList.Items.Contains(F) Then
                        CopyFromList.Items.Add(F)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub CopyFromAddFolder_Click(sender As Object, e As RoutedEventArgs) Handles CopyFromAddFolder.Click
        If System.IO.File.Exists(CopyFromTextBox.Text) Then
            If Not CopyFromList.Items.Contains(CopyFromTextBox.Text) Then
                CopyFromList.Items.Add(CopyFromTextBox.Text)
            End If
        ElseIf System.IO.Directory.Exists(CopyFromTextBox.Text) Then
            If Not CopyFromList.Items.Contains(CopyFromTextBox.Text) Then
                CopyFromList.Items.Add(CopyFromTextBox.Text)
            End If
        Else
            Dim fbd As New FolderBrowserDialog

            If fbd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If Not CopyFromList.Items.Contains(fbd.SelectedPath) Then
                    CopyFromList.Items.Add(fbd.SelectedPath)
                End If
            End If
        End If
    End Sub

    Private Sub CopyFromSub_Click(sender As Object, e As RoutedEventArgs) Handles CopyFromSub.Click
        While CopyFromList.SelectedItems.Count > 0
            CopyFromList.Items.Remove(CopyFromList.SelectedItem)
        End While
    End Sub

    Private Sub CopyToAdd_Click(sender As Object, e As RoutedEventArgs) Handles CopyToAdd.Click
        If System.IO.File.Exists(CopyToTextBox.Text) Then
            If Not CopyToList.Items.Contains(CopyToTextBox.Text) Then
                CopyToList.Items.Add(CopyToTextBox.Text)
            End If
        ElseIf System.IO.Directory.Exists(CopyToTextBox.Text) Then
            If Not CopyToList.Items.Contains(CopyToTextBox.Text) Then
                CopyToList.Items.Add(CopyToTextBox.Text)
            End If
        Else
            Dim fbd As New FolderBrowserDialog

            If fbd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If Not CopyToList.Items.Contains(fbd.SelectedPath) Then
                    CopyToList.Items.Add(fbd.SelectedPath)
                End If
            End If
        End If
    End Sub

    Private Sub CopyToSub_Click(sender As Object, e As RoutedEventArgs) Handles CopyToSub.Click
        While CopyToList.SelectedItems.Count > 0
            CopyToList.Items.Remove(CopyToList.SelectedItem)
        End While
    End Sub

    Private Sub BeginCopy_Click(sender As Object, e As RoutedEventArgs) Handles BeginCopy.Click
        Dim threads = New List(Of CopierThread)

        For Each Destination As String In CopyToList.Items
            threads.Add(New CopierThread(Destination, CopyFromList.Items, Overwrite.IsChecked))
        Next

        For Each CT As CopierThread In threads
            CT.Thread.Start()
        Next

        For Each CT As CopierThread In threads
            CT.Thread.Join()
        Next
    End Sub
End Class
