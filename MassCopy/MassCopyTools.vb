Public Module MassCopyTools
    Public Function GetDirNameFromPath(FilePath As String) As String
        Dim split As String() = FilePath.Split("\")
        Return split(split.Length - 1)
    End Function
End Module