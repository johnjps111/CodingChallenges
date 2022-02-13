Public Class KnightMoves
    Sub main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        Dim chArr As String
        Dim x, y As Integer
        Dim aInt As Integer = Asc(CChar("a"))
        Dim useSpace As Boolean
        While Not tr.EndOfStream
            chArr = tr.ReadLine.Trim.ToCharArray
            x = Asc(chArr(0)) - aInt
            y = Asc(chArr(1)) - Asc(CChar("0")) - 1
            useSpace = False
            'left-down
            If x - 2 >= 0 AndAlso y - 1 >= 0 Then
                If useSpace Then Console.Write(" ")
                Console.Write(Chr(x - 2 + aInt) & y)
                useSpace = True
            End If
            'left-up
            If x - 2 >= 0 AndAlso y + 1 < 8 Then
                If useSpace Then Console.Write(" ")
                Console.Write(Chr(x - 2 + aInt) & y + 2)
                useSpace = True
            End If
            'down-left
            If x - 1 >= 0 AndAlso y - 2 >= 0 Then
                If useSpace Then Console.Write(" ")
                Console.Write(Chr(x - 1 + aInt) & y - 1)
                useSpace = True
            End If
            'up-left
            If x - 1 >= 0 AndAlso y + 2 < 8 Then
                If useSpace Then Console.Write(" ")
                Console.Write(Chr(x - 1 + aInt) & y + 3)
                useSpace = True
            End If
            'up-right
            If x + 1 < 8 AndAlso y - 2 >= 0 Then
                If useSpace Then Console.Write(" ")
                Console.Write(Chr(x + 1 + aInt) & y - 1)
                useSpace = True
            End If
            'down-right
            If x + 1 < 8 AndAlso y + 2 < 8 Then
                If useSpace Then Console.Write(" ")
                Console.Write(Chr(x + 1 + aInt) & y + 3)
                useSpace = True
            End If
            'right-down
            If x + 2 < 8 AndAlso y - 1 >= 0 Then
                If useSpace Then Console.Write(" ")
                Console.Write(Chr(x + 2 + aInt) & y)
                useSpace = True
            End If
            'right-up
            If x + 2 < 8 AndAlso y + 1 < 8 Then
                If useSpace Then Console.Write(" ")
                Console.Write(Chr(x + 2 + aInt) & y + 2)
                useSpace = True
            End If
            Console.WriteLine("")
        End While
        tr.Close()
        tr.Dispose()
    End Sub
End Class
