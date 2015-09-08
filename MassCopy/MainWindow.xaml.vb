Imports System.Windows.Forms

Class MainWindow
    ' Functions
    Private Function CopyReady()
        If Not CopyFromList.Items.IsEmpty Then
            If Not CopyToList.Items.IsEmpty Then
                CopyStatus.Text = FindResource("COPY_NOT_READY")
            Else
                CopyStatus.Text = FindResource("COPY_NOT_READY")
            End If
        Else
            CopyStatus.Text = FindResource("COPY_NOT_READY")
        End If
    End Function

    Private Function InitCopierThreads()
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
    End Function

    ' Subs
    Private Sub BeginCopy_Click(sender As Object, e As RoutedEventArgs) Handles Copy.Click
        CopyStatus.Text = FindResource("COPY_ACTIVE")
        InitCopierThreads()
        CopyStatus.Text = FindResource("COPY_FINISH")
    End Sub

    Private Sub CopyFromList_DragEnter(sender As Object, e As Windows.DragEventArgs) Handles CopyFromList.DragOver
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effects = DragDropEffects.Copy
        Else
            e.Effects = DragDropEffects.None
        End If
    End Sub

    Private Sub CopyFromList_Drop(sender As Object, e As Windows.DragEventArgs) Handles CopyFromList.Drop
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            Dim files As String() = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
            For Each f As String In files
                If System.IO.Directory.Exists(f) Then
                    CopyFromList.Items.Add(f)
                ElseIf System.IO.File.Exists(f) Then
                    CopyFromList.Items.Add(f)
                End If
            Next
        End If
        CopyReady()
    End Sub

    Private Sub CopyToList_DragEnter(sender As Object, e As Windows.DragEventArgs) Handles CopyToList.DragOver
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effects = DragDropEffects.Copy
        Else
            e.Effects = DragDropEffects.None
        End If
    End Sub

    Private Sub CopyToList_Drop(sender As Object, e As Windows.DragEventArgs) Handles CopyToList.Drop
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            Dim files As String() = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
            For Each f As String In files
                If System.IO.Directory.Exists(f) Then
                    CopyToList.Items.Add(f)
                End If
            Next
        End If
        CopyReady()
    End Sub

    Private Sub CFClear_Click(sender As Object, e As RoutedEventArgs) Handles CFClear.Click
        CopyFromList.Items.Clear()
        CopyReady()
    End Sub

    Private Sub CTClear_Click(sender As Object, e As RoutedEventArgs) Handles CTClear.Click
        CopyToList.Items.Clear()
        CopyReady()
    End Sub
End Class
