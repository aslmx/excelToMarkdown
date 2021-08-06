Sub rangeToMarkDown()
    
    Dim cell As Range
    Dim selectedRange As Range

    Set selectedRange = Application.Selection

    Dim rowCounter As Integer
    Dim columnCounter As Integer
    Dim totalColumns As Integer
    Dim currentColumnWidth As Integer
    
    Dim FullMarkDown As String
    

    totalColumns = selectedRange.Columns.Count

    Dim columnWidth(50) As String    'maximum of 50 columns

    '///
    '/// init lengths of columns
    '///
    For i = 0 To totalColumns
        columnWidth(i) = 0
    Next i

    '///
    '/// go through range to calculate maximum lengths of each column
    '///
    For Each Row In selectedRange.Rows

        columnCounter = 0

        For Each cell In Row.Cells
        
    
            currentColumnWidth = Len(cell.Value)

            If (currentColumnWidth > columnWidth(columnCounter)) Then

                columnWidth(columnCounter) = currentColumnWidth

            End If

            columnCounter = columnCounter + 1
            '/// Debug.Print cell.Address, " ", cell.Value, "->", Len(cell.Value)

        Next cell

    Next Row

    '///
    '/// go through range to calculate maximum lengths of each column
    '///
    Dim currentLine As String

    rowCounter = 0
    For Each Row In selectedRange.Rows

        columnCounter = 0

        currentLine = "|"

        For Each cell In Row.Cells

            currentColumnWidth = columnWidth(columnCounter)
            Dim extraSpaces As Integer

            currentLine = currentLine & " "
            currentLine = currentLine & cell.Value
            extraSpaces = currentColumnWidth - Len(cell.Value)

            For j = 0 To extraSpaces

                currentLine = currentLine & " "

            Next j

            currentLine = currentLine & " |"

            columnCounter = columnCounter + 1
            '/// Debug.Print cell.Address, " ", cell.Value, "->", Len(cell.Value)

        Next cell

                        ' Debug.Print "Currentline is: " & currentLine
                         '    Debug.Print "Without CRLF  : " & Replace(currentLine, vbCrLf, "")
                                                 
                                                '  Replace(str, vbCRLf, "")
                                                 ' remove CRLFs from current line, they will break Markdown
            FullMarkDown = FullMarkDown & vbCrLf & Replace(currentLine, vbLf, " ")
        
            'Debug.Print "Full Markdown so far: " & vbCrLf & "------------------" & vbCrLf & vbCrLf & FullMarkDown & vbCrLf & vbCrLf & "------------------"

        If (rowCounter = 0) Then

            currentLine = "|"
            columnCounter = 0

            For j = 0 To (totalColumns - 1)

                currentLine = currentLine
                currentColumnWidth = columnWidth(columnCounter)
                currentLine = currentLine & "-"

                For k = 0 To currentColumnWidth

                    currentLine = currentLine & "-"
                Next k

                currentLine = currentLine & "-|"
                columnCounter = columnCounter + 1

            Next j
            '         Debug.Print "Currentline is: " & currentLine
            '         Debug.Print "Without CRLF  : " & Replace(currentLine, vbCrLf, "")
                                                 
                                                 '  Replace(str, vbCRLf, "")
                                                 ' remove CRLFs from current line, they will break Markdown
            FullMarkDown = FullMarkDown & vbCrLf & Replace(currentLine, vbLf, " ")
            
            'Debug.Print "Full Markdown so far: " & vbCrLf & "------------------" & vbCrLf & vbCrLf & FullMarkDown & vbCrLf & vbCrLf & "------------------"
            
        End If

        rowCounter = rowCounter + 1

    Next Row

' Copy Markdown to Clipboard immediately
' needs Microsoft Forms 2.0 Library enabled
Dim clip As New DataObject
clip.SetText FullMarkDown
clip.PutInClipboard

'MsgBox (FullMarkDown)
' Clipboard FullMarkDown


End Sub
