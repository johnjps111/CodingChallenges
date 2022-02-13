Imports System.Collections.Generic
Public Class DateRanges
    Sub main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        Dim drList As LinkedList(Of DateRange)
        Dim cdr As LinkedListNode(Of DateRange)
        Dim mt As Integer
        Dim dr As DateRange
        While Not tr.EndOfStream
            drList = Nothing
            For Each s As String In tr.ReadLine.Trim.Replace(" ", "").Split(CChar(";"))
                dr = New DateRange(New Date(CInt(s.Substring(3, 4)), GetMonthInt(s.Substring(0, 3)), 1), New Date(CInt(s.Substring(11, 4)), GetMonthInt(s.Substring(8, 3)), 1).AddMonths(1))
                If drList Is Nothing Then
                    drList = New LinkedList(Of DateRange)
                    drList.AddFirst(dr)
                Else
                    cdr = drList.First
                    While cdr IsNot Nothing
                        If dr.d2 < cdr.Value.d1 Then
                            ' new range ends before current node
                            drList.AddBefore(cdr, dr)
                            cdr = Nothing
                        ElseIf dr.d1 > cdr.Value.d2 Then
                            ' new range begins after current node
                            If cdr.Next Is Nothing Then
                                drList.AddAfter(cdr, dr)
                                cdr = Nothing
                            Else
                                cdr = cdr.Next
                            End If
                        Else
                            'there is overlap... extend cdr if necessary
                            If dr.d1 < cdr.Value.d1 Then
                                cdr.Value.d1 = dr.d1
                            End If
                            If dr.d2 > cdr.Value.d2 Then
                                cdr.Value.d2 = dr.d2
                            End If
                            While cdr.Next IsNot Nothing AndAlso cdr.Value.d2 >= cdr.Next.Value.d1
                                cdr.Value.d2 = CDate(IIf(dr.d2 >= cdr.Next.Value.d2, dr.d2, cdr.Next.Value.d2))
                                drList.Remove(cdr.Next)
                            End While
                            While cdr.Previous IsNot Nothing AndAlso cdr.Value.d1 <= cdr.Previous.Value.d2
                                cdr.Value.d1 = CDate(IIf(dr.d1 <= cdr.Next.Value.d1, dr.d1, cdr.Next.Value.d1))
                                drList.Remove(cdr.Previous)
                            End While
                            cdr = Nothing
                        End If
                    End While
                End If
            Next
            mt = 0
            cdr = drList.First
            While cdr IsNot Nothing
                mt += CInt(DateDiff(DateInterval.Month, cdr.Value.d1, cdr.Value.d2))
                cdr = cdr.Next
            End While
            Console.WriteLine(CInt(Math.Floor(mt / 12)))
        End While
        tr.Close()
        tr.Dispose()
    End Sub
    Private Class DateRange
        Public d1 As Date
        Public d2 As Date
        Public Sub New(ByVal pd1 As Date, ByVal pd2 As Date)
            d1 = pd1
            d2 = pd2
        End Sub
    End Class
    Private Function GetMonthInt(ByVal m As String) As Integer
        Select Case m
            Case "Jan"
                Return 1
            Case "Feb"
                Return 2
            Case "Mar"
                Return 3
            Case "Apr"
                Return 4
            Case "May"
                Return 5
            Case "Jun"
                Return 6
            Case "Jul"
                Return 7
            Case "Aug"
                Return 8
            Case "Sep"
                Return 9
            Case "Oct"
                Return 10
            Case "Nov"
                Return 11
            Case "Dec"
                Return 12
        End Select
        Return 9999
    End Function
End Class
