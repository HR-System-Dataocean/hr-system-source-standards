Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Globalization

Public Class HelperCls
    Public Shared Function RetDropDownList(ByRef DDL As DropDownList, ByRef DT As DataTable, ByVal valueMember As String, ByVal DisMember As String) As DropDownList
        DDL.DataSource = DT
        DDL.DataValueField = valueMember
        DDL.DataTextField = DisMember
        Return (DDL)
    End Function
    Public Class GlobalEnum
        Public Enum ListTypes
            DirectTable = 1
            StoredProcedure = 2
            ValueList = 3
        End Enum
        Public Enum ActionMovement
            StepForward = 1
            StepBackward = 2
            StepFirest = 3
            ForwordTo = 4
        End Enum
        Public Enum StagePeople
            DirectManager = 1
            Position = 2
            Employee = 3
        End Enum
        Public Enum StageActionNotificationTarget
            Stage = 1
            Position = 2
            Employee = 3
            Initiator = 4
        End Enum
        Public Enum StageActionNotificationWays
            ByMail = 1
            BySms = 2
            ByNotifySystem = 3
        End Enum
    End Class
    Public Class DateCls
        Private Const startGreg As Integer = 1900
        Private Const endGreg As Integer = 2100
        Private allFormats As String() = {"yyyy/MM/dd", "yyyy/M/d", "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy", _
         "yyyy-MM-dd", "yyyy-M-d", "dd-MM-yyyy", "d-M-yyyy", "dd-M-yyyy", "d-MM-yyyy", _
         "yyyy MM dd", "yyyy M d", "dd MM yyyy", "d M yyyy", "dd M yyyy", "d MM yyyy"}
        Private arCul As CultureInfo
        Private enCul As CultureInfo
        Private h As UmAlQuraCalendar
        Private g As GregorianCalendar

        Public Sub New()
            arCul = New CultureInfo("ar-SA")
            enCul = New CultureInfo("en-US")

            h = New UmAlQuraCalendar()
            g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

            arCul.DateTimeFormat.Calendar = h
        End Sub

        Public Function IsHijri(hijri As String) As Boolean
            If hijri.Length <= 0 Then
                Return False
            End If
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                If tempDate.Year >= startGreg AndAlso tempDate.Year <= endGreg Then
                    Return True
                Else
                    Return False
                End If
            Catch
                Throw
            End Try
        End Function

        Public Function IsGreg(greg As String) As Boolean
            If greg.Length <= 0 Then
                Return False
            End If
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                If tempDate.Year >= startGreg AndAlso tempDate.Year <= endGreg Then
                    Return True
                Else
                    Return False
                End If
            Catch
                Throw
            End Try
        End Function

        Public Function FormatHijristring([date] As String, format As String) As String
            Try
                Dim tempDate As DateTime = DateTime.ParseExact([date], allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.ToString(format, arCul.DateTimeFormat)
            Catch
                Throw
            End Try
        End Function

        Public Function FormatHijriDate([date] As String, format As String) As DateTime
            Try
                Dim tempDate As DateTime = DateTime.ParseExact([date], allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate
            Catch
                Throw
            End Try
        End Function

        Public Function FormatGregstring([date] As String, format As String) As String
            Try
                Dim tempDate As DateTime = DateTime.ParseExact([date], allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.ToString(format, enCul.DateTimeFormat)
            Catch
                Throw
            End Try
        End Function

        Public Function FormatGregDate([date] As String, format As String) As DateTime
            Try
                Dim tempDate As DateTime = DateTime.ParseExact([date], allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate
            Catch
                Throw
            End Try
        End Function

        Public Function GDateNow() As String
            Try
                Return DateTime.Now.ToString("yyyy/MM/dd", enCul.DateTimeFormat)
            Catch
                Throw
            End Try
        End Function

        Public Function GDateNow(format As String) As String
            Try
                Return DateTime.Now.ToString(format, enCul.DateTimeFormat)
            Catch
                Throw
            End Try
        End Function

        Public Function HDateNow() As String
            Try
                Return DateTime.Now.ToString("yyyy/MM/dd", arCul.DateTimeFormat)
            Catch
                Throw
            End Try
        End Function

        Public Function HDateNow(format As String) As String
            Try
                Return DateTime.Now.ToString(format, arCul.DateTimeFormat)
            Catch
                Throw
            End Try
        End Function

        Public Function HijriToGreg(hijri As String) As String
            If hijri.Length <= 0 Then
                Return ""
            End If
            Try
                If IsHijri(hijri) Then
                    Dim tempDate As DateTime = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                    Return tempDate.ToString("yyyy/MM/dd", enCul.DateTimeFormat)
                Else
                    Return hijri
                End If
            Catch
                Throw
            End Try
        End Function

        Public Function HijriToGreg(hijri As String, format As String) As String
            If hijri.Length <= 0 Then
                Return ""
            End If
            Try
                If IsHijri(hijri) Then
                    Dim tempDate As DateTime = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                    Return tempDate.ToString(format, enCul.DateTimeFormat)
                Else
                    Return hijri
                End If
            Catch
                Throw
            End Try
        End Function

        Public Function HijriToGreg(hijri As String, format As String, Year As Integer, Month As Integer, Day As Integer) As String
            If hijri.Length <= 0 Then
                Return ""
            End If
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.AddYears(Year).AddMonths(Month).AddDays(Day).ToString(format, enCul.DateTimeFormat)
            Catch
                Throw
            End Try
        End Function

        Public Function GregToHijri(greg As String) As String
            If greg.Length <= 0 Then
                Return ""
            End If
            Try
                If IsGreg(greg) Then
                    Dim tempDate As DateTime = DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                    Return tempDate.ToString("yyyy/MM/dd", arCul.DateTimeFormat)
                Else
                    Return greg
                End If
            Catch
                Throw
            End Try
        End Function

        Public Function GregToHijri(greg As String, format As String) As String
            If greg.Length <= 0 Then
                Return ""
            End If
            Try
                If IsGreg(greg) Then
                    Dim tempDate As DateTime = DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                    Return tempDate.ToString(format, arCul.DateTimeFormat)
                Else
                    Return greg
                End If
            Catch
                Throw
            End Try
        End Function

        Public Function GregToHijri(greg As String, format As String, Year As Integer, Month As Integer, Day As Integer) As String
            If greg.Length <= 0 Then
                Return ""
            End If
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.AddYears(Year).AddMonths(Month).AddDays(Day).ToString(format, arCul.DateTimeFormat)
            Catch
                Throw
            End Try
        End Function

        Public Function GTimeStamp() As String
            Return GDateNow("yyyyMMddHHmmss")
        End Function

        Public Function HTimeStamp() As String
            Return HDateNow("yyyyMMddHHmmss")
        End Function

        Public Function Compare(d1 As String, d2 As String) As Integer
            Try
                Dim date1 As DateTime = DateTime.ParseExact(d1, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Dim date2 As DateTime = DateTime.ParseExact(d2, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return DateTime.Compare(date1, date2)
            Catch
                Throw
            End Try

        End Function

        Public Function Is29(iMonth As Integer, iYear As Integer) As Integer
            Try
                Return If(arCul.Calendar.GetDaysInMonth(iYear, iMonth) = 29, 1, 0)
            Catch
                Return -1
            End Try
        End Function

        Public Function CalendarExists(culture As CultureInfo, cal As Calendar) As Boolean
            For Each optionalCalendar As Calendar In culture.OptionalCalendars
                If cal.ToString().Equals(optionalCalendar.ToString()) Then
                    Return True
                End If
            Next

            Return False
        End Function

        Public Function HDateDef(Start As String, [End] As String, Type As [String]) As Integer
            arCul.DateTimeFormat.Calendar = h
            Dim Def As Integer = 0
            Dim span As TimeSpan = FormatHijriDate([End], "dd/MM/yyyy").Subtract(FormatHijriDate(Start, "dd/MM/yyyy"))
            If Type = "H" Then
                Def = span.Days * 24 + span.Hours
            End If

            If Type = "D" Then
                Def = span.Days * 24 + span.Hours
                Def = (Def \ 24)
            End If
            Return Def
        End Function

        Public Function GDateDef(Start As String, [End] As String, Type As [String]) As Integer
            enCul.DateTimeFormat.Calendar = g
            Dim Def As Integer = 0
            Dim span As TimeSpan = FormatGregDate([End], "dd/MM/yyyy").Subtract(FormatGregDate(Start, "dd/MM/yyyy"))
            If Type = "H" Then
                Def = span.Days * 24 + span.Hours
            End If

            If Type = "D" Then
                Def = span.Days * 24 + span.Hours
                Def = (Def \ 24)
            End If
            Return Def
        End Function
    End Class
End Class
