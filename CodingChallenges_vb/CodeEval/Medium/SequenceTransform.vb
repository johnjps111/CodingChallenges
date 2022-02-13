Public Class SequenceTransform

    Sub main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        While Not tr.EndOfStream
            Dim currentLine As String = tr.ReadLine.Trim
            Dim clSplits As String() = currentLine.Split((" ".ToCharArray)(0))
            Dim intStr As String = clSplits(0).Trim
            Dim chStr As String = clSplits(1).Trim
            If CanTransform(0, 0, intStr, chStr) Then
                Console.WriteLine("YES")
            Else
                Console.WriteLine("NO")
            End If
        End While
        tr.Close()
        tr.Dispose()
    End Sub
    Private Function CanTransform(ByVal i1 As Integer, ByVal i2 As Integer, ByVal s1 As String, ByVal s2 As String) As Boolean
        If i1 >= s1.Length Then
            ' we made it to the end of s1... make sure whatever we're on in s2 has no further transitions
            If i2 > s2.Length - 1 Then
                Return True
            Else
                If i2 = 0 Then Return False
                Dim lastChar As String = s2.Substring(i2 - 1, 1)
                While (i2 < s2.Length)
                    If s2.Substring(i2, 1) <> lastChar Then
                        Return False
                    End If
                    i2 += 1
                End While
                Return True
            End If
        End If
        Dim currInt As Integer = CInt(s1.Substring(i1, 1))
        Dim currIndexA As Integer = s2.IndexOf("A", i2)
        Dim currIndexB As Integer = s2.IndexOf("B", i2)
        If currInt = 0 Then
            If currIndexA < 0 OrElse (currIndexB >= 0 AndAlso currIndexB < currIndexA) Then
                'this currIndexB is extending a sequence of B's successfully up to currIndexA, then we're fine to continue
                Dim iCheck As Integer = currIndexB - 1
                While iCheck < currIndexA
                    If iCheck < 0 OrElse s2.Substring(iCheck, 1) <> "B" Then
                        Return False
                    End If
                    iCheck += 1
                End While
                Return CanTransform(i1 + 1, currIndexA + 1, s1, s2)
            Else
                Return CanTransform(i1 + 1, currIndexA + 1, s1, s2)
            End If
        Else
            If currIndexA < 0 AndAlso currIndexB < 0 Then
                Return False
            Else
                If currIndexA < 0 Then
                    Return CanTransform(i1 + 1, currIndexB + 1, s1, s2)
                ElseIf currIndexB < 0 Then
                    Return CanTransform(i1 + 1, currIndexA + 1, s1, s2)
                Else
                    Return CanTransform(i1 + 1, currIndexA + 1, s1, s2) OrElse CanTransform(i1 + 1, currIndexB + 1, s1, s2)
                End If
            End If
        End If
    End Function

End Class
