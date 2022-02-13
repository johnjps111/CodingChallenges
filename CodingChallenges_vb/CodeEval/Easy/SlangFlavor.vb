Imports System.Collections.Generic
Public Module SlangFlavor
    Sub main(ByVal args() As String)
        Dim zStr As String() = {", yeah!", ", this is crazy, I tell ya.", ", can U believe this?", ", eh?", ", aw yea.", ", yo.", "? No way!", ". Awesome!"}
        Dim mStr As String = ".!?"
        Dim tr As New IO.StreamReader(args(0))
        Dim cl As String
        Dim i, j As Integer
        Dim notDone, doIt As Boolean
        j = 0
        doIt = False
        While Not tr.EndOfStream
            cl = tr.ReadLine().Trim()
            notDone = True
            i = 0
            While notDone
                If mStr.IndexOf(cl.Substring(i, 1)) >= 0 Then
                    If doIt Then
                        cl = cl.Remove(i, 1).Insert(i, zStr(j Mod 8))
                        i += (zStr(j Mod 8)).Length
                        j += 1
                        doIt = False
                    Else
                        doIt = True
                        i += 1
                    End If
                Else
                    i += 1
                End If
                If i > cl.Length - 1 Then
                    notDone = False
                End If
            End While
            Console.WriteLine(cl)
        End While
        tr.Close()
        tr.Dispose()
    End Sub
End Module
