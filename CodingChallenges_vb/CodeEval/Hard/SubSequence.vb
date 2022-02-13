Imports System.Collections.Generic
Module SubSequence
    Private s1 As String
    Private s2 As String
    Sub Main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        While Not tr.EndOfStream
            Dim currentLine As String = tr.ReadLine
            If currentLine Is Nothing OrElse currentLine.Trim.Length = 0 Then
                Continue While
            End If
            Dim currentLineSplits As String() = currentLine.Split((";".ToCharArray)(0))
            If currentLine Is Nothing OrElse currentLineSplits.Length <> 2 Then
                Continue While
            End If
            'If currentLineSplits(0).Length > 50 OrElse currentLineSplits(1).Length > 50 Then
            '    Continue While
            'End If
            s1 = currentLineSplits(0)
            s2 = currentLineSplits(1)
            If currentLineSplits(0).Length > currentLineSplits(1).Length Then
                s1 = currentLineSplits(1)
                s2 = currentLineSplits(0)
            End If
            Dim currentResult As String = ""
            Dim currentIndexInS1 As Integer = 0
            Dim currentIndexInS2 As Integer = 0
            Dim bestResult As String = GetLCS(currentResult, currentIndexInS1, currentIndexInS2)
            Console.WriteLine(bestResult.TrimEnd)
        End While
        tr.Close()
        tr.Dispose()
    End Sub
    Private Function GetLCS(ByVal currentResult As String, ByVal currentIndexInS1 As Integer, ByVal currentIndexInS2 As Integer) As String
        Dim bestResult As String = currentResult
        For i As Integer = currentIndexInS1 To s1.Length - 1
            Dim allMatchesInS2 As IList(Of Integer) = GetAllIndexes(s1.Substring(i, 1), s2, currentIndexInS2)
            If allMatchesInS2 Is Nothing OrElse allMatchesInS2.Count = 0 Then
                Continue For
            End If
            For Each j As Integer In allMatchesInS2
                Dim tmpResult As String = currentResult & s1.Substring(i, 1)
                Dim newResult As String = GetLCS(tmpResult, i + 1, j + 1)
                If newResult.Length > bestResult.Length Then
                    bestResult = newResult
                End If
            Next
        Next
        Return bestResult
    End Function
    Private Function GetAllIndexes(ByVal searchString As String, ByVal containerString As String, ByVal startIndex As Integer) As IList(Of Integer)
        Dim indices As New List(Of Integer)
        Dim currentIndex As Integer = containerString.IndexOf(searchString, startIndex)
        While currentIndex >= 0
            indices.Add(currentIndex)
            currentIndex = containerString.IndexOf(searchString, currentIndex + 1)
        End While
        Return indices
    End Function
End Module
