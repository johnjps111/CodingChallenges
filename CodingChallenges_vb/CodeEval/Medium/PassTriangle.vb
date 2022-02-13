Imports System.Collections.Generic
Public Class PassTriangle

    Sub main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        Dim lastPS As New List(Of Integer)
        Dim currPS As New List(Of Integer)
        While Not tr.EndOfStream
            Dim currentLine As String = tr.ReadLine.Trim
            Dim clSplits As String() = currentLine.Split((" ".ToCharArray)(0))
            currPS = New List(Of Integer)
            For i As Integer = 0 To clSplits.Length - 1
                Dim currInt As Integer = CInt(clSplits(i))
                If lastPS.Count = 0 Then
                    currPS.Add(currInt)
                Else
                    ' calculate new partial sums
                    Dim currPSi As New List(Of Integer)
                    Dim miniMax As Integer = 0
                    If i < lastPS.Count Then
                        ' do i
                        If lastPS(i) + currInt > miniMax Then
                            miniMax = lastPS(i) + currInt
                        End If
                    End If
                    If i > 0 Then
                        ' do i - 1
                        If lastPS(i - 1) + currInt > miniMax Then
                            miniMax = lastPS(i - 1) + currInt
                        End If
                    End If
                    currPS.Add(miniMax)
                End If
            Next
            lastPS = currPS
        End While
        tr.Close()
        tr.Dispose()
        Dim max As Integer = 0
        For Each psi As Integer In lastPS
            If psi > max Then
                max = psi
            End If
        Next
        Console.WriteLine(max)
    End Sub

End Class
