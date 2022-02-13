Imports System.Collections.Generic
Public Class PascalsTriangle
    Sub main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        Dim ri As New List(Of List(Of Integer))
        Dim n, i, j As Integer
        Dim s As String = ""
        While Not tr.EndOfStream
            n = CInt(tr.ReadLine().Trim())
            ri.Clear()
            For i = 0 To n - 1
                Dim rj As New List(Of Integer)
                For j = 0 To i
                    If j = 0 OrElse j = i Then
                        rj.Add(1)
                    Else
                        rj.Add(ri(i - 1)(j - 1) + ri(i - 1)(j))
                    End If
                Next
                ri.Add(rj)
            Next
            s = ""
            For i = 0 To ri.Count - 1
                For j = 0 To ri(i).Count - 1
                    s &= CStr((ri(i))(j)) & " "
                Next
            Next
            Console.WriteLine(s.Trim)
        End While
        tr.Close()
        tr.Dispose()
    End Sub
End Class
