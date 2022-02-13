Imports System.Collections
Public Class FollowingInteger
    Dim ba As BitArray
    Private nList As New List(Of Integer)
    Private currLine, newNum, tmp As String
    Private i, j As Integer
    Sub main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        While Not tr.EndOfStream
            currLine = tr.ReadLine.Trim
            nList.Clear()
            For Each ch As Char In currLine.ToCharArray
                nList.Add(Asc(ch) - 48)
            Next
            ' simple case: something can be swapped internally
            For i = nList.Count - 1 To 1 Step -1
                For j = i - 1 To 0 Step -1
                    If nList(j) < nList(i) Then
                        tmp = currLine.Substring(j, 1)
                        newNum = currLine.Insert(i + 1, tmp).Remove(j, 1)
                        Console.WriteLine(newNum)
                        Continue While
                    End If
                Next
            Next

        End While
        tr.Close()
        tr.Dispose()
    End Sub
End Class