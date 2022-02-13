Public Class ReallyEasy
    Sub main(ByVal args() As String)
        Dim cl As Char()
        Dim c As Char
        Dim needsSpace, newLine As Boolean
        Using tr As New IO.StreamReader(args(0))
            While Not tr.EndOfStream
                cl = tr.ReadLine.Trim.ToCharArray
                needsSpace = False
                newLine = True
                For Each c In cl
                    If Char.IsLetter(c) Then
                        If needsSpace Then
                            If Not newLine Then
                                Console.Write(" ")
                            End If
                        End If
                        Console.Write(Char.ToLower(c))
                        newLine = False
                        needsSpace = False
                    Else
                        needsSpace = True
                    End If
                Next
                Console.WriteLine("")
            End While
        End Using
    End Sub
End Class
