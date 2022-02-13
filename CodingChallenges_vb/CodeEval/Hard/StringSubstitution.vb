Public Class StringSubstitution

    Sub Main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        While Not tr.EndOfStream
            Dim currentLine As String = tr.ReadLine
            Dim currentLineSplits As String() = currentLine.Split((";".ToCharArray)(0))
            Dim origLine As String = currentLineSplits(0)
            Dim origLineMarkers As String = ""
            origLineMarkers = origLineMarkers.PadRight(origLine.Length, ("0".ToCharArray)(0))
            Dim updates As String() = currentLineSplits(1).Split((",".ToCharArray)(0))
            For i As Integer = 0 To updates.Length - 1 Step 2
                Dim indexOfReplaceStart As Integer = origLine.IndexOf(updates(i))
                While indexOfReplaceStart >= 0
                    Dim markers As String = origLineMarkers.Substring(indexOfReplaceStart, updates(i).Length)
                    If markers.Contains("1") Then
                        indexOfReplaceStart = origLine.IndexOf(updates(i), indexOfReplaceStart + 1)
                    Else
                        Dim markerOnes As String = ""
                        markerOnes = markerOnes.PadRight(updates(i + 1).Length, ("1".ToCharArray)(0))
                        origLineMarkers = origLineMarkers.Remove(indexOfReplaceStart, updates(i).Length)
                        origLineMarkers = origLineMarkers.Insert(indexOfReplaceStart, markerOnes)
                        origLine = origLine.Remove(indexOfReplaceStart, updates(i).Length)
                        origLine = origLine.Insert(indexOfReplaceStart, updates(i + 1))
                        indexOfReplaceStart = origLine.IndexOf(updates(i), indexOfReplaceStart + 1)
                    End If
                End While
            Next
            Console.WriteLine(origLine)
        End While
        tr.Close()
        tr.Dispose()
    End Sub

End Class
