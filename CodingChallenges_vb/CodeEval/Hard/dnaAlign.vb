Imports System.Collections.Generic
Public Class dnaAlign
    Private s1 As String ' the longer of the two strings in a given line
    Private s2 As String ' the shorter of the two strings in a given line
    Private rsDict As New Dictionary(Of Integer, Integer)
    Sub Main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        While Not tr.EndOfStream
            rsDict.Clear()
            Dim currentLine As String = tr.ReadLine
            If currentLine.StartsWith("#") Then Continue While
            Dim currentLineSplits As String() = currentLine.Split(("|".ToCharArray)(0))
            If currentLineSplits(0).Length < currentLineSplits(1).Length Then
                s1 = currentLineSplits(1).Trim
                s2 = currentLineSplits(0).Trim
            Else
                s1 = currentLineSplits(0).Trim
                s2 = currentLineSplits(1).Trim
            End If
            Dim bestResult As Integer = GetRemainingAlignment(0, "", "", 0, 0)
            'Console.WriteLine(s1 & " | " & s2 & " | " & bestResult & " | (" & rsDict.Count & ")")
            Console.WriteLine(CStr(bestResult))
        End While
        tr.Close()
        tr.Dispose()
    End Sub

    Private Function GetRemainingAlignment(ByVal priorScore As Integer, ByVal priorAlignS1 As String, ByVal priorAlignS2 As String, ByVal priorAlignLetterCountS1 As Integer, ByVal priorAlignLetterCountS2 As Integer) As Integer
        If priorAlignLetterCountS1 >= s1.Length AndAlso priorAlignLetterCountS2 >= s2.Length Then
            Return priorScore
        End If
        ' if "whatever is left" has been seen before, utilize that knowledge...
        ' use an integer key; multiply one argument by something larger than anything we expect to see, to ensure unique keys
        Dim mdk As Integer = (priorAlignLetterCountS1 * 1000000) + (priorAlignLetterCountS2 * 1000)
        If priorAlignS1.Length > 0 Then
            Select Case priorAlignS1.Substring(priorAlignS1.Length - 1, 1)
                Case "A"
                    mdk += 10
                Case "C"
                    mdk += 20
                Case "G"
                    mdk += 30
                Case "T"
                    mdk += 40
                Case "-"
                    mdk += 50
            End Select
        End If
        If priorAlignS2.Length > 0 Then
            Select Case priorAlignS2.Substring(priorAlignS2.Length - 1, 1)
                Case "A"
                    mdk += 1
                Case "C"
                    mdk += 2
                Case "G"
                    mdk += 3
                Case "T"
                    mdk += 4
                Case "-"
                    mdk += 5
            End Select
        End If
        If rsDict.ContainsKey(mdk) Then
            Return priorScore + rsDict(mdk)
        End If
        If priorAlignLetterCountS2 >= s2.Length Then
            ' we've used all the letters in s2: s2 will new contain only dashes to match whatever is left from s1
            Dim remainingScore As Integer = (s1.Length - priorAlignLetterCountS1) * (-1)
            If Not priorAlignS2.EndsWith("-") Then
                remainingScore -= 7
            End If
            rsDict.Add(mdk, remainingScore)
            Return priorScore + remainingScore
        End If
        If priorAlignLetterCountS1 >= s1.Length Then
            ' we've used all the letters in s1: s1 will new contain only dashes to match whatever is left from s2
            Dim remainingScore As Integer = (s2.Length - priorAlignLetterCountS2) * (-1)
            If Not priorAlignS1.EndsWith("-") Then
                remainingScore -= 7
            End If
            rsDict.Add(mdk, remainingScore)
            Return priorScore + remainingScore
        End If
        ' possible next choices:
        ' s1 = dash; s2 = dash (never do this - it always reduces your score)
        ' s1 = dash; s2 = char
        Dim remainingScoreDashChar As Integer = GetRemainingAlignment(CInt(IIf(priorAlignS1.EndsWith("-"), -1, -8)), priorAlignS1 & "-", priorAlignS2 & s2.Substring(priorAlignLetterCountS2, 1), priorAlignLetterCountS1, priorAlignLetterCountS2 + 1)
        ' s1 = char; s2 = dash
        Dim remainingScoreCharDash As Integer = GetRemainingAlignment(CInt(IIf(priorAlignS2.EndsWith("-"), -1, -8)), priorAlignS1 & s1.Substring(priorAlignLetterCountS1, 1), priorAlignS2 & "-", priorAlignLetterCountS1 + 1, priorAlignLetterCountS2)
        ' s1 = char; s2 = char
        Dim remainingScoreCharChar As Integer = GetRemainingAlignment(CInt(IIf(s1.Substring(priorAlignLetterCountS1, 1) = s2.Substring(priorAlignLetterCountS2, 1), 3, -3)), priorAlignS1 & s1.Substring(priorAlignLetterCountS1, 1), priorAlignS2 & s2.Substring(priorAlignLetterCountS2, 1), priorAlignLetterCountS1 + 1, priorAlignLetterCountS2 + 1)
        ' ... use whichever was best...
        If remainingScoreCharDash > remainingScoreDashChar Then
            If remainingScoreCharDash > remainingScoreCharChar Then
                rsDict.Add(mdk, remainingScoreCharDash)
                Return priorScore + remainingScoreCharDash
            Else
                rsDict.Add(mdk, remainingScoreCharChar)
                Return priorScore + remainingScoreCharChar
            End If
        Else
            If remainingScoreDashChar > remainingScoreCharChar Then
                rsDict.Add(mdk, remainingScoreDashChar)
                Return priorScore + remainingScoreDashChar
            Else
                rsDict.Add(mdk, remainingScoreCharChar)
                Return priorScore + remainingScoreCharChar
            End If
        End If
    End Function
End Class
