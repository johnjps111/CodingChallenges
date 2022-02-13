Imports System.Collections.Generic
Public Class BrainFark
    Private chLine As Char()
    Sub Main(ByVal args() As String)
        Dim arr As New Generic.List(Of Integer)
        Dim arrIx, chIx As Integer
        Dim ch As Char
        Using tr As New IO.StreamReader(args(0))
            arr.Clear()
            arr.Add(0)
            arrIx = 0
            chLine = tr.ReadLine().Trim().ToCharArray()
            For Each ch In chLine
                ProcessChar(chIx, arr, arrIx)
            Next
            Console.WriteLine("")
            Dim z As String = "Put a breakpoint here."
        End Using
    End Sub
    Private Sub ProcessChar(ByVal chIx As Integer, ByVal arr As Generic.List(Of Integer), ByVal arrIx As Integer)
        While chIx < chLine.Count
            Select Case chLine(chIx)
                Case CChar(">")
                    arrIx += 1
                    If arrIx >= arr.Count Then
                        arr.Add(0)
                    End If
                Case CChar("<")
                    If arrIx > 0 Then
                        arrIx -= 1
                    End If
                Case CChar("+")
                    arr(arrIx) += 1
                Case CChar("-")
                    arr(arrIx) -= 1
                Case CChar(".")
                    Console.Write(Chr(arr(arrIx)))
                Case CChar(",")
                    arr(arrIx) = Asc(chLine(chIx))
                Case CChar("[")
                    ProcessChar(chIx + 1, arr, arrIx)
                'If arr(arrIx) = 0 Then
                '    chIx += 1
                'End If
                Case CChar("]")
                    chIx += 1
                    Return
                    'If arr(arrIx) <> 0 Then
                    '    chIx -= 1
                    'End If
            End Select
            chIx += 1
        End While
    End Sub
End Class
