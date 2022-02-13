Imports System.Collections
Public Class Minesweeper
    Private wordGrid(2)() As Char
    Sub main(ByVal args() As String)
        Dim tr As New IO.StreamReader(args(0))
        Dim resultStr As String
        Dim mineField As Char()
        Dim clSplits As String()
        Dim matrixDimStr As String()
        Dim i, j, i2, j2, rowCnt, colCnt, slotCnt As Integer
        While Not tr.EndOfStream
            clSplits = tr.ReadLine.Split(CChar(";"))
            mineField = clSplits(1).Trim.ToCharArray
            matrixDimStr = clSplits(0).Split(CChar(","))
            rowCnt = CInt(matrixDimStr(0).Trim)
            colCnt = CInt(matrixDimStr(1).Trim)
            resultStr = ""
            For i = 0 To rowCnt - 1
                For j = 0 To colCnt - 1
                    If mineField(i * colCnt + j) = CChar("*") Then
                        resultStr &= "*"
                    Else
                        slotCnt = 0
                        For i2 = i - 1 To i + 1
                            For j2 = j - 1 To j + 1
                                If i2 <> i OrElse j2 <> j Then
                                    If i2 >= 0 AndAlso i2 < rowCnt AndAlso j2 >= 0 AndAlso j2 < colCnt AndAlso mineField(i2 * colCnt + j2) = CChar("*") Then
                                        slotCnt += 1
                                    End If
                                End If
                            Next
                        Next
                        resultStr &= CStr(slotCnt)
                    End If
                Next
            Next
            Console.WriteLine(resultStr)
        End While
        tr.Close()
        tr.Dispose()
    End Sub
End Class
