Imports System.Drawing
Imports System.Collections.Generic
Public Class DiscountOffers
    Private VOWELS As New List(Of Char)
    Private x, y As String()
    Private ssMatrix(,) As Double
    Private ssRank As New List(Of RankNode)
    Private ssMedian As Double
    Private rsDict As New Dictionary(Of Integer, Double)
    Private i, j, k As Integer
    Private Class RankNode
        Public Value As Double
        Public ri As Integer
        Public cj As Integer
        Public Sub New(ByVal v As Double, ByVal r As Integer, ByVal c As Integer)
            Value = v
            ri = r
            cj = c
        End Sub
    End Class
    Sub Main(ByVal args() As String)
        VOWELS.add(CChar("a"))
        VOWELS.add(CChar("e"))
        VOWELS.add(CChar("i"))
        VOWELS.add(CChar("o"))
        VOWELS.add(CChar("u"))
        VOWELS.add(CChar("y"))
        Dim tr As New IO.StreamReader(args(0))
        While Not tr.EndOfStream
            Dim currentLine As String = tr.ReadLine
            If currentLine.StartsWith("#") Then Continue While
            rsDict.Clear()
            Dim currentLineSplits As String() = currentLine.Split(CChar(";"))
            Dim n As String() = currentLineSplits(0).Trim.Split(CChar(","))
            Dim p As String() = currentLineSplits(1).Trim.Split(CChar(","))
            If n.Length < p.Length Then
                x = p ' x has the smaller number of items
                y = n
            Else
                x = n ' x has the smaller number of items
                y = p
            End If
            ReDim ssMatrix(x.Length - 1, y.Length - 1)
            ssRank.Clear()
            Dim ssTotal As Double = 0
            For i = 0 To x.Length - 1
                For j = 0 To y.Length - 1
                    ssMatrix(i, j) = GetSS(x(i), y(j))
                    ssTotal += ssMatrix(i, j)
                    InsertRank(New RankNode(ssMatrix(i, j), i, j))
                Next
            Next
            ssMedian = ssTotal / (x.Length * y.Length)
            Dim bestSS As Double = GetBestSS(0, 0)
            'Dim bestSS As Double = GetBestRemainingSS(0, 0)
            Console.WriteLine(bestSS.ToString("f2") & vbTab & "(" & CStr(x.Length) & "," & CStr(y.Length) & ")")
        End While
        tr.Close()
        tr.Dispose()
    End Sub
    Private Function GetBestSS(ByVal usedX As Integer, ByVal usedY As Integer) As Double
        Dim rsk As Integer = (usedX << (y.Length + 1)) + usedY
        If rsDict.ContainsKey(rsk) Then
            Return rsDict(rsk)
        End If
        Dim bestSS, bestDiff, colBestSS, colNextBest As Double
        Dim bestList, colBestList As New List(Of Point)
        bestDiff = 0
        bestSS = 0
        ' get options for best i,j
        For ix As Integer = 0 To x.Length - 1
            If Not CBool(usedX And (1 << ix)) Then
                colBestSS = 0
                colNextBest = 0
                colBestList.Clear()
                For jy As Integer = 0 To y.Length - 1
                    If Not CBool(usedY And (1 << jy)) Then
                        If ssMatrix(ix, jy) > colBestSS Then
                            colNextBest = colBestSS
                            colBestSS = ssMatrix(ix, jy)
                            colBestList.Clear()
                        End If
                        If ssMatrix(ix, jy) >= colBestSS - 1 Then
                            colBestList.Add(New Point(ix, jy))
                        End If
                    End If
                Next
                If colBestSS - colNextBest > bestDiff Then
                    bestDiff = colBestSS - colNextBest
                    bestList.Clear()
                    bestList.AddRange(colBestList)
                ElseIf colBestSS - colNextBest = bestDiff Then
                    bestList.AddRange(colBestList)
                End If
            End If
        Next
        For Each pt As Point In bestList
            Dim calcNewBestSS As Double = ssMatrix(pt.X, pt.Y) + GetBestSS(usedX Or CInt(1 << pt.X), usedY Or CInt(1 << pt.Y))
            If calcNewBestSS > bestSS Then
                bestSS = calcNewBestSS
            End If
        Next
        rsDict.Add(rsk, bestSS)
        Return bestSS
    End Function
    'Private Function GetBestSS(ByVal usedX As Integer, ByVal usedY As Integer) As Double
    '    Dim rsk As Integer = (usedX << (y.Length + 1)) + usedY
    '    If rsDict.ContainsKey(rsk) Then
    '        Return rsDict(rsk)
    '    End If
    '    'Dim bestSS, bestDiff, colBestSS, colNextBest As Double
    '    'Dim bestList, colBestList As New List(Of Point)
    '    'bestDiff = 0
    '    Dim bestSS As Double = 0
    '    ' get options for best i,j
    '    'Dim ssrn As Integer = 0
    '    For Each ssrn As RankNode In ssRank
    '        'If ssrn.Value > ssMedian Then
    '        If (Not CBool(usedX And 1 << ssrn.ri)) AndAlso (Not CBool(usedY And 1 << ssrn.cj)) Then
    '            Dim calcNewBestSS As Double = ssMatrix(ssrn.ri, ssrn.cj) + GetBestSS(usedX Or CInt(1 << ssrn.ri), usedY Or CInt(1 << ssrn.cj))
    '            If calcNewBestSS > bestSS Then
    '                bestSS = calcNewBestSS
    '            End If
    '        End If
    '        'End If
    '    Next
    '    'While ssrn < ssRank.Count AndAlso (Not CBool(usedX And 1 << ssRank(ssrn).ri)) AndAlso (Not CBool(usedY And 1 << ssRank(ssrn).cj))
    '    '    Dim calcNewBestSS As Double = ssMatrix(ssRank(ssrn).ri, ssRank(ssrn).cj) + GetBestSS(usedX Or CInt(1 << (ssRank(ssrn).ri)), usedY Or CInt(1 << (ssRank(ssrn).cj)))
    '    '    If calcNewBestSS > bestSS Then
    '    '        bestSS = calcNewBestSS
    '    '    End If
    '    '    ssrn += 1
    '    'End While

    '    'colBestSS = 0
    '    'colNextBest = 0
    '    'colBestList.Clear()
    '    'For ix As Integer = 0 To x.Length - 1
    '    '    If Not CBool(usedX And (1 << ix)) Then
    '    '        For jy As Integer = 0 To y.Length - 1
    '    '            If Not CBool(usedY And (1 << jy)) Then
    '    '                If ssMatrix(ix, jy) > colBestSS Then
    '    '                    colNextBest = colBestSS
    '    '                    colBestSS = ssMatrix(ix, jy)
    '    '                    colBestList.Clear()
    '    '                End If
    '    '                If ssMatrix(ix, jy) >= colBestSS Then
    '    '                    colBestList.Add(New Point(ix, jy))
    '    '                End If
    '    '            End If
    '    '        Next
    '    '        If colBestSS - colNextBest > bestDiff Then
    '    '            bestDiff = colBestSS - colNextBest
    '    '            bestList.Clear()
    '    '            bestList.AddRange(colBestList)
    '    '        ElseIf colBestSS - colNextBest = bestDiff Then
    '    '            bestList.AddRange(colBestList)
    '    '        End If
    '    '    End If
    '    'Next
    '    'For Each pt As Point In bestList
    '    '    Dim calcNewBestSS As Double = ssMatrix(pt.X, pt.Y) + GetBestSS(usedX Or CInt(1 << pt.X), usedY Or CInt(1 << pt.Y))
    '    '    If calcNewBestSS > bestSS Then
    '    '        bestSS = calcNewBestSS
    '    '    End If
    '    'Next
    '    rsDict.Add(rsk, bestSS)
    '    Return bestSS
    'End Function
    Private Sub InsertRank(ByVal rn As RankNode)
        If ssRank.Count = 0 Then
            ssRank.Add(rn)
        Else
            Dim m As Integer = 0
            While m < ssRank.Count AndAlso ssRank(m).Value >= rn.Value
                m += 1
            End While
            If m = ssRank.Count Then
                ' off the end of list: new smallest value
                ssRank.Add(rn)
            Else
                ' shift list down by one... innefficient for large lists
                ssRank.Add(ssRank(ssRank.Count - 1))
                For ni As Integer = ssRank.Count - 2 To m + 1 Step -1
                    ssRank(ni) = ssRank(ni - 1)
                Next
                ssRank(m) = rn
            End If
        End If
    End Sub
    Private Function GetBestRemainingSS(ByVal usedX As Integer, ByVal usedY As Integer) As Double
        Dim rsk As Integer = (usedX << (y.Length + 1)) + usedY
        If rsDict.ContainsKey(rsk) Then
            Return rsDict(rsk)
        End If
        Dim newBestSS As Double = 0
        For ix As Integer = 0 To x.Length - 1
            If Not CBool(usedX And (1 << ix)) Then
                For jy As Integer = 0 To y.Length - 1
                    If Not CBool(usedY And (1 << jy)) Then
                        If ssMatrix(ix, jy) > ssMedian Then ' don't even bother with low valued entries...
                            Dim calcNewBestSS As Double = ssMatrix(ix, jy) + GetBestRemainingSS(usedX Or CInt(1 << ix), usedY Or CInt(1 << jy))
                            If calcNewBestSS > newBestSS Then
                                newBestSS = calcNewBestSS
                            End If
                        End If
                    End If
                Next
            End If
        Next
        rsDict.Add(rsk, newBestSS)
        Return newBestSS
    End Function
    Private Function GetSS(ByVal ns As String, ByVal ps As String) As Double
        Dim ssScore As Double = 0.0
        Dim pNumLetters As Integer = 0
        Dim nNumLetters As Integer = 0
        Dim nNumVowels As Integer = 0
        Dim nNumConsonants As Integer = 0
        For Each ch As Char In ps.ToCharArray
            If Char.IsLetter(ch) Then
                pNumLetters += 1
            End If
        Next
        For Each ch As Char In ns.ToCharArray
            If Char.IsLetter(ch) Then
                nNumLetters += 1
                If VOWELS.Contains(Char.ToLower(ch)) Then
                    nNumVowels += 1
                Else
                    nNumConsonants += 1
                End If
            End If
        Next
        If pNumLetters Mod 2 = 0 Then
            ssScore = CDbl(nNumVowels) * 1.5
        Else
            ssScore = CDbl(nNumConsonants)
        End If
        If nNumLetters = pNumLetters OrElse HasCommonFactors(nNumLetters, pNumLetters) Then
            ssScore = 1.5 * ssScore
        End If
        Return ssScore
    End Function
    Private Function HasCommonFactors(ByVal n As Integer, ByVal p As Integer) As Boolean
        Dim lt As Integer = n
        Dim gt As Integer = p
        If p < n Then
            lt = p
            gt = n
        End If
        Dim sqrtlt As Integer = CInt(Math.Ceiling(Math.Sqrt(CDbl(lt))))
        For k = 2 To sqrtlt
            If lt Mod k = 0 AndAlso gt Mod k = 0 Then
                Return True
            End If
        Next
        Return False
    End Function
End Class
