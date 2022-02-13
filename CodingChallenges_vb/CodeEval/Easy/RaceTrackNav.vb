Public Class RacingTrackNav
    Private raceTrack As New List(Of String)
    Public Sub main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        While Not tr.EndOfStream
            raceTrack.Add(tr.ReadLine.Trim)
        End While
        'Console.WriteLine(GetMaxRaceRoute(""))
        tr.Close()
        tr.Dispose()
    End Sub
    'Public Function GetMaxRaceRoute(ByVal d As Integer, ByVal r As String) As String
    '    If d >= raceTrack.Count Then
    '        Return r
    '    End If
    '    Dim lt, st, rt As Integer
    '    Dim m As Integer = 0
    '    Dim i As Integer = route.IndexOfAny("/|\".ToCharArray)
    '    Dim ms, ls, ss, rs As String
    '    If i > 0 Then
    '        For j As Integer = i - 1 To i + 1
    '            If j >= 0 AndAlso j < raceTrack(d).Length AndAlso raceTrack(d).Substring(j, 1) <> "#" Then
    '                If raceTrack(d).Substring(j, 1) = "C" Then
    '                    ' it's a checkpoint
    '                    lt = GetMaxRaceRoute(d + 1, raceTrack(d).Remove(j, 1).Insert(j, "/"))
    '                Else
    '                    ' it's a regular gate
    '                    Dim lt
    '                End If
    '            End If
    '        Next
    '    Else
    '        For j As Integer = 0 To raceTrack(d).Length - 1
    '            If raceTrack(d).Substring(j, 1) <> "#" Then
    '                If raceTrack(d).Substring(j, 1) = "C" Then
    '                    ' it's a checkpoint
    '                    ls = raceTrack(d).Remove(j, 1).Insert(j, "/")
    '                    lt = 4 + GetMaxRaceRoute(d + 1, ls)
    '                    If lt > m Then
    '                        m = lt
    '                        ms = ls
    '                    End If
    '                    ss = raceTrack(d).Remove(j, 1).Insert(j, "|")
    '                    st = 4 + GetMaxRaceRoute(d + 1, ss)
    '                    If st > m Then
    '                        m = st
    '                        ms = ss
    '                    End If
    '                    rs = raceTrack(d).Remove(j, 1).Insert(j, "\")
    '                    rt = 4 + GetMaxRaceRoute(d + 1, rs)
    '                    If rt > m Then
    '                        m = rt
    '                        ms = rs
    '                    End If
    '                Else
    '                    ' it's a regular gate
    '                    ls = raceTrack(d).Remove(j, 1).Insert(j, "/")
    '                    lt = 1 + GetMaxRaceRoute(d + 1, ls)
    '                    If lt > m Then
    '                        m = lt
    '                        ms = ls
    '                    End If
    '                    ss = raceTrack(d).Remove(j, 1).Insert(j, "|")
    '                    st = 1 + GetMaxRaceRoute(d + 1, ss)
    '                    If st > m Then
    '                        m = st
    '                        ms = ss
    '                    End If
    '                    rs = raceTrack(d).Remove(j, 1).Insert(j, "\")
    '                    rt = 1 + GetMaxRaceRoute(d + 1, rs)
    '                    If rt > m Then
    '                        m = rt
    '                        ms = rs
    '                    End If
    '                End If
    '            End If
    '        Next
    '        Return m
    '    End If
    'End Function
End Class
