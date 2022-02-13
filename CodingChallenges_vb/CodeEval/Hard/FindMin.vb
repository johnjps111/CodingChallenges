Imports System.Collections
Imports System.Collections.Generic
Public Class FindMin
    Dim ba As BitArray
    Private kList As New List(Of Integer)
    Private n, k, a, b, c, r, i, mp, mn As Integer
    Sub main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        Dim clSplits As String()
        While Not tr.EndOfStream
            clSplits = tr.ReadLine.Split(CChar(","))
            n = CInt(clSplits(0))
            k = CInt(clSplits(1))
            a = CInt(clSplits(2))
            b = CInt(clSplits(3))
            c = CInt(clSplits(4))
            r = CInt(clSplits(5))
            mp = a
            kList.Clear()
            For i = 0 To k - 1
                kList.Add(mp)
                If i = n Then
                    Console.WriteLine(CStr(mp))
                    Continue While
                End If
                mn = ((b * mp) + c) Mod r
                mp = mn
            Next
            For i = k To 2 * k
                kList.Add(GetLargestUnused(i))
                If i = n Then
                    Console.WriteLine(kList(i - 1))
                    Continue While
                End If
            Next
            If n > 2 * k Then
                Console.WriteLine(kList(k + n Mod (k + 1)))
            Else
                Console.WriteLine(kList(n))
            End If
        End While
        tr.Close()
        tr.Dispose()
    End Sub
    Private Function GetLargestUnused(ByVal u As Integer) As Integer
        ba = New BitArray(k + 1)
        For x As Integer = u - k To u - 1
            If kList(x) > k Then Continue For
            ba(kList(x)) = True
        Next
        For x As Integer = 0 To k
            If Not ba(x) Then
                Return x
            End If
        Next
        Return k
    End Function
End Class