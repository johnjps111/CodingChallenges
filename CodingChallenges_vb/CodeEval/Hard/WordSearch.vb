Imports System.Collections.Generic
Imports System.Drawing
Public Class WordSearch

    ''' <summary>
    ''' Given a grid 
    '''   A B C E
    '''   S F C S
    '''   A D E E
    ''' </summary>
    ''' <remarks></remarks>
    Private wordGrid(2)() As Char
    Sub main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        wordGrid(0) = "ABCE".ToCharArray
        wordGrid(1) = "SFCS".ToCharArray
        wordGrid(2) = "ADEE".ToCharArray
        While Not tr.EndOfStream
            Dim currentLine As Char() = tr.ReadLine.Trim.ToCharArray
            Dim usedGrid As String = "000000000000"
            Dim ism As Boolean = IsMatch(0, Nothing, currentLine, usedGrid)
            Console.WriteLine(CStr(ism))
        End While
        tr.Close()
        tr.Dispose()
        Console.WriteLine()
    End Sub
    Private Function IsMatch(ByVal d As Integer, ByVal lastPoint As Point, ByVal chList As Char(), ByVal usedGrid As String) As Boolean
        If d >= chList.Length Then
            Return True
        End If
        Dim ch As Char = chList(d)
        Dim matchPoints As IList(Of Point) = FindMatchPoints(ch, usedGrid)
        Dim foundMatch As Boolean = False
        For Each mp As Point In matchPoints
            If d = 0 OrElse IsAdjacent(mp, lastPoint) Then
                Dim newUsedGrid As String = usedGrid.Remove((mp.X * 4) + mp.Y, 1)
                newUsedGrid = newUsedGrid.Insert((mp.X * 4) + mp.Y, "1")
                If (IsMatch(d + 1, mp, chList, newUsedGrid)) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function
    Private Function FindMatchPoints(ByVal ch As Char, ByVal usedGrid As String) As IList(Of Point)
        Dim rsp As New List(Of Point)
        For i As Integer = 0 To wordGrid.Length - 1
            For j As Integer = 0 To wordGrid(i).Length - 1
                Dim used As String = usedGrid.Substring((i * 4) + j, 1)
                If wordGrid(i)(j) = ch AndAlso used = "0" Then
                    rsp.Add(New Point(i, j))
                End If
            Next
        Next
        Return rsp
    End Function
    Private Function IsAdjacent(ByVal pt1 As Point, ByVal pt2 As Point) As Boolean
        ' horizontally adjacent
        If pt1.Y = pt2.Y And Math.Abs(pt1.X - pt2.X) = 1 Then
            Return True
        End If
        ' vertically adjacent
        If pt1.X = pt2.X And Math.Abs(pt1.Y - pt2.Y) = 1 Then
            Return True
        End If
        '' diagonally adjacent
        'If Math.Abs(pt1.X - pt2.X) = 1 And Math.Abs(pt1.Y - pt2.Y) = 1 Then
        '    Return True
        'End If
        ' not adjacent
        Return False
    End Function

End Class
