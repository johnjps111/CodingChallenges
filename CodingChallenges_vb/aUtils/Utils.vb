Public Class Utils
    Private Function IsPrime(ByVal n As Integer) As Boolean
        If n = 0 OrElse n = 1 Then
            Return False
        ElseIf n = 2 Then
            Return True
        Else
            For i As Integer = 3 To CInt(Math.Floor(Math.Sqrt(n))) Step 2
                If n Mod i = 0 Then Return False
            Next
        End If
        Return True
    End Function
End Class
