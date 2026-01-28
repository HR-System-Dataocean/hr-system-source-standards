Public Class StringHandler

#Region "Public Decleration"

    '*************************************************************
    'NAME:          PD_ObjErrorHandler 
    'PURPOSE:       to handle all the errors of the module 
    '*************************************************************

    Dim PD_ObjErrorHandler As New Venus.Shared.ErrorsHandler

#End Region

#Region "Friend Enumeration"

    '*************************************************************
    'NAME:          CaseType  
    'PURPOSE:       to handle the upper and lower case 
    '*************************************************************

    Public Enum CaseType
        UpperCase
        LowerCase
        Non
    End Enum

    '*************************************************************
    'NAME:          Direction 
    'PURPOSE:       to handle the both direction of switch parts
    '*************************************************************

    Public Enum Direction
        Left
        Right
    End Enum

#End Region

#Region "Public Functions"
    '*************************************************************
    'NAME:          SetValue 
    'PURPOSE:       set the value in a specific place in the string 
    'PARAMETERS:    Expression - Find -Replacement - Separator
    '               sending any string to this function 
    '               and send the place that you want to 
    '               modifay it and send the modification 
    '               string and the function will replace 
    '               the specific place by the new string
    'RETURNS:       The string after modification 
    '*************************************************************
    Public Shared Function SetValue(ByVal Expression As String, _
                           ByVal Find As String, _
                           ByVal Replacement As String, _
                           Optional ByVal Separator As String = ";") As String

        Dim StrString As String
        Dim IntLocation As Integer
        Dim DblLenght As Double
        Dim StrLeftPart As String
        Dim StrRightPart As String
        Dim StrFinalResult As String
        Dim IntNextSeparator As Integer

        Try

            StrString = Find
            IntLocation = Expression.IndexOf(StrString)
            DblLenght = StrString.Length

            If IntLocation < 0 Then
                Return ""
            End If

            StrRightPart = Expression.Substring(IntLocation + DblLenght)
            IntNextSeparator = StrRightPart.IndexOf(Separator)

            If IntNextSeparator > 0 Then
                StrRightPart = StrRightPart.Substring(IntNextSeparator)
            Else
                StrRightPart = String.Empty
            End If
            StrLeftPart = Expression.Substring(0, IntLocation)
            StrFinalResult = StrLeftPart & Find & "=" & Replacement & StrRightPart


            Return StrFinalResult
        Catch ex As Exception
            Return ""
        End Try
    End Function
    '*************************************************************
    'NAME:          GetValue
    'PURPOSE:       Get the value of specific place in the string 
    'PARAMETERS:    Expression - Find - Separator - CaseSensitive
    '               this function will help you to 
    '               get the data from specific place in any 
    '               string 
    'RETURNS:       The string after modification 
    '*************************************************************
    Public Shared Function GetValue(ByVal Expression As String, _
                              ByVal Find As String, _
                              Optional ByVal Separator As String = ";", _
                              Optional ByVal CaseSensitive As CaseType = CaseType.Non) As String

        Dim StrString As String = String.Empty
        Dim IntLocation As Integer
        Dim DblLenght As Double
        Dim StrRightPart As String = String.Empty
        Dim StrFinalResult As String = String.Empty
        Dim IntNextSeparator As Integer

        Try

            StrString = Find
            'If Expression.IndexOf(StrString) > 0 Then
            IntLocation = Expression.IndexOf(StrString)
                DblLenght = StrString.Length
            'End If


            If IntLocation < 0 Then
                Return ""
            End If

            StrRightPart = Expression.Substring(IntLocation + DblLenght + 1)
            IntNextSeparator = StrRightPart.IndexOf(Separator)
            If IntNextSeparator >= 0 Then
                StrRightPart = StrRightPart.Substring(0, IntNextSeparator)
            End If

            Select Case CaseSensitive
                Case CaseType.UpperCase
                    StrFinalResult = StrRightPart.ToUpper
                Case CaseType.LowerCase
                    StrFinalResult = StrRightPart.ToLower
                Case CaseType.Non
                    StrFinalResult = StrRightPart
            End Select

            Return StrFinalResult
        Catch ex As Exception
            Return ""
        End Try
    End Function
    '*************************************************************
    'NAME:          StripQuotation 
    'PURPOSE:       Replace the Quotation in the specific place 
    '               in order to avoid the data base conflict 
    'PARAMETERS:    Expression 
    '               Replacing the single Quote to doubel Quote
    'RETURNS:       The string after modification 
    '*************************************************************
    Public Shared Function StripQuotation(ByVal Expression As String) As String
        Return Replace(Expression, "'", "''")
    End Function
    '*************************************************************
    'NAME:          ReplaceHamza 
    'PURPOSE:       Replace the Hamza in order to make the search easy
    'PARAMETERS:    Expression 
    '               Replace the hamza of the sended string 
    'RETURNS:       The string after modification 
    '*************************************************************
    Public Shared Function ReplaceHamza(ByVal Expression As String) As String
        Dim StrString As String
        StrString = Expression
        StrString = Replace(StrString, "‹", vbNullString, , , CompareMethod.Binary)
        StrString = Replace(StrString, "√", "«", , , CompareMethod.Text)
        StrString = Replace(StrString, "¬", "«", , , CompareMethod.Text)
        StrString = Replace(StrString, "≈", "«", , , CompareMethod.Text)
        StrString = Replace(StrString, "ƒ", "Ê", , , CompareMethod.Text)
        StrString = Replace(StrString, "…", "Â", , , CompareMethod.Binary)
        StrString = Replace(StrString, "·¬", "·«", , , CompareMethod.Text)
        StrString = Replace(StrString, "·√", "·«", , , CompareMethod.Text)
        StrString = Replace(StrString, "·≈", "·«", , , CompareMethod.Text)
        StrString = Replace(StrString, "Ì", "Ï", , , CompareMethod.Text)
        StrString = Replace(StrString, "∆", "¡", , , CompareMethod.Text)
        StrString = Replace(StrString, "'", vbNullString, , , CompareMethod.Text)
        While InStr(1, StrString, "  ")
            StrString = Replace(StrString, "  ", " ", , , CompareMethod.Text)
        End While
        Return StrString
    End Function
    '*************************************************************
    'NAME:          SwithParts 
    'PURPOSE:       Get one word with tow langauge separated by
    '               separator and the function will return 
    '               one of this tow word to according to 
    '               Direction selected 
    'PARAMETERS:    Expression  - Separator 
    'RETURNS:       The string after modification 
    '*************************************************************
    Public Shared Function SwitchParts(ByVal Expression As String, ByVal Separator As String, _
                                    Optional ByVal Direction As Direction = StringHandler.Direction.Left) As String

        Dim StrRightPart As String = String.Empty
        Dim StrLeftPart As String = String.Empty
        Dim IntLocation As Integer
        Dim StrFinalResult As String = String.Empty


        Try
            IntLocation = Expression.IndexOf(Separator)
            StrRightPart = Expression.Substring(IntLocation + 1)
            StrLeftPart = Expression.Substring(0, Expression.Length - StrRightPart.Length - 1)

            Select Case Direction
                Case StringHandler.Direction.Left
                    StrFinalResult = StrLeftPart
                Case StringHandler.Direction.Right
                    StrFinalResult = StrRightPart
            End Select

            Return StrFinalResult
        Catch ex As Exception
            Return ""
        End Try
    End Function
    '*************************************************************
    'NAME:          SwithParts 
    'PURPOSE:       Get one word with tow langauge separated by
    '               separator and the function will return 
    '               one of this tow word to according to 
    '               Direction selected 
    'PARAMETERS:    Expression  - Separator 
    'RETURNS:       The string after modification 
    '*************************************************************
    'Public Function ToWords(ByVal No As String, Optional ByVal CurrName As String = " (Riyal)/ —Ì«·", Optional ByVal CurrDecimal As String = " Halalah/ Â··…", Optional ByVal Lang As Double = -1) As String
    '    ' Convert Numbers to Character
    '    Dim FirstArray As Object
    '    Dim FirstArray1 As Object
    '    Dim SecondArray As Object
    '    Dim ThirdArray As Object

    '    ReDim Parts(5)
    '    ReDim PartStr(-1 To 3) As String
    '    Dim Length As Integer, I As Integer, TempLength As Integer
    '    Dim NoString As String, pos As Integer
    '    Dim AfterPoint As String
    '    Dim txt As String

    '    If Not Registered Then
    '        ToWords = SwitchParts("not registered copy ... you can't use this function/‰”Œ… €Ì— „ÊÀﬁ…  .... ·« ” ÿÌ⁄ «” Œœ«„ Â–… «·⁄„·Ì…", Lang)
    '        Exit Function
    '    End If
    '    If Val(No) = 0 Then ToWords = "" : Exit Function

    '    If Lang <> 1 Then Lang = 0
    '    If Lang = 1 Then
    '        FirstArray =("", "Ê«Õœ ", "«À‰«‰ ", "À·«À… ", "√—»⁄… ", "Œ„”… ", "” … ", "”»⁄… ", "À„«‰Ì… ", " ”⁄… ")
    '        FirstArray1 = Array("", "√Õœ ", "«À‰« ")
    '        SecondArray = Array("", "⁄‘—… ", "⁄‘—Ê‰ ", "À·«ÀÊ‰ ", "√—»⁄Ê‰ ", "Œ„”Ê‰ ", "” Ê‰ ", "”»⁄Ê‰ ", "À„«‰Ê‰ ", " ”⁄Ê‰ ")
    '        ThirdArray = Array("", "„∆… ", "„∆ «‰ ", "À·«À„«∆… ", "√—»⁄„«∆… ", "Œ„”„«∆… ", "” „«∆… ", "”»⁄„«∆… ", "À„«‰„«∆… ", " ”⁄„«∆… ")
    '    Else
    '        FirstArray = Array("", " One ", " Two ", " Three ", " Four ", " Five ", " Six ", " Seven ", " Eight ", " Nine ")
    '        FirstArray1 = Array("Ten", "Eleven", "Twelve", "Thirteen", "Forteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen")
    '        SecondArray = Array("", " Ten ", " Twenty ", " Thirty ", " Forty ", " Fifty ", " Sixty ", " Seventy ", " Eighty ", " Ninety ")
    '    End If
    '    If No = vbNullString Then
    '        ToWords = vbNullString
    '        Exit Function
    '    End If
    '    NoString = No
    '    Length = Len(NoString)
    '    pos = InStr(NoString, ".")
    '    If pos > 0 Then
    '        AfterPoint = Right$(NoString, Length - pos)
    '        NoString = Left$(NoString, pos - 1)
    '        Length = Len(NoString)
    '    End If
    '    TempLength = Length
    '    Parts(I) = NoString
    '    Do While TempLength >= 3
    '        TempLength = TempLength - 3
    '        Parts(I) = Right$(NoString, 3)
    '        NoString$ = Left$(NoString, TempLength)
    '        I = I + 1
    '    Loop
    '    Parts(I) = NoString
    '    For I = 0 To 3
    '        If Len(Parts(I)) > 0 Then
    '            PartStr(I) = GetNo(Parts(I), I, FirstArray, FirstArray1, SecondArray, ThirdArray, Lang)
    '        Else
    '            Exit For
    '        End If
    '    Next I
    '    For I = 3 To 0 Step -1
    '        If Len(PartStr(I)) > 0 Then
    '            If Len(PartStr(I - 1)) > 0 Then
    '                txt = txt & " " & PartStr(I) & IIf(Lang = doEnglish, " ", "Ê")
    '            Else
    '                txt = txt & " " & PartStr(I) & " "
    '            End If
    '        End If
    '    Next
    '    txt = Trim$(txt) + " " + StripCaption(CurrName, Lang)
    '    If Val(AfterPoint) > 0 Then
    '        txt = txt & IIf(Lang = 1, " And ", " Ê ") & GetNo(AfterPoint, 0, FirstArray, FirstArray1, SecondArray, ThirdArray, Lang) + StripCaption(CurrDecimal, Lang)
    '    End If
    '    ToWords = IIf(Lang = 1, "Only " & txt, " ›ﬁÿ " & txt & " ·«€Ì— ")
    'End Function
#End Region

End Class
