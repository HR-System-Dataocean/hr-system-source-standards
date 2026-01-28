Imports System.Diagnostics
Imports System.Globalization
Imports System.Data
Imports System.Collections
Imports System.Configuration

Public Class Clssys_GHCalendar
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_GHCalendar "
        mInsertParameter = " GDate,HDate "
        mInsertParameterValues = " @GDate,@HDate "
        mUpdateParameter = " GDate=@GDate,HDate=@HDate "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region

#Region "Private Members"
    Private mID As Object
    Private mGDate As Object
    Private mHDate As String
    Public Enum DateType
        Gregorian
        Hijri
    End Enum
    Public Enum DateFormat
        DDMMYYYY
        MMDDYYYY
        YYYYMMDD
        YYYYDDMM
        DDMMYY
        MMDDYY
        YYMMDD
        YYDDMM
    End Enum
    Public Enum Directions
        Input
        Output
    End Enum
#End Region

#Region "Public property"
    Public Property ID() As Object
        Get
            Return mID
        End Get
        Set(ByVal Value As Object)
            mID = Value
        End Set
    End Property
    Public Property GDate() As Object
        Get
            Return mGDate
        End Get
        Set(ByVal Value As Object)
            mGDate = Value
        End Set
    End Property
    Public Property HDate() As String
        Get
            Return mHDate
        End Get
        Set(ByVal Value As String)
            mHDate = Value
        End Set
    End Property
#End Region

#Region "Public Function"
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : Get the hijri date by filter 
    'Description: Find all columns from G2H table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '==================================================================
    Public Function GetHDateSetting(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "Select ID, GDate, Substring(HDate,4,2)As HMonth ,(Select Count(sys_GHCalendar1.HDate) From sys_GHCalendar sys_GHCalendar1 Where Right(sys_GHCalendar1.HDate,7)=Right(sys_GHCalendar.HDate,7)) As HDays From " & Me.mTable & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.Table & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & " Order By GDate Asc")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = CONFIG_DATEFORMAT & mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
            Else
                Clear()
            End If
            If mID > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function ShiftsNextDays(ByVal oldDate As Date, ByVal newDate As Date) As Boolean
        Dim strSQL As String
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text

            strSQL = " Set DateFormat DMY "
            strSQL &= " Update " & mTable & " Set GDate = DateAdd( dd,DateDiff(dd,'" & oldDate & "','" & newDate & "'),GDate) where GDate >= '" & oldDate & "' "

            mSqlCommand.CommandText = strSQL
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
            Return False
        End Try

    End Function

    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : 
    'Description: Generate the script file that map the hijir date to gregorian at the client side 
    'Steps: 
    '       1-Determine the mapping file for the g2h file
    '       2-Get all the information form the G2H file
    '       3-Itrate through the file to insert the new data into js file 
    '==================================================================
    Public Function GenerateHDate() As Boolean
        Dim strSQL As String
        Dim ObjDataRow As Data.DataRow
        Dim ObjFile As Global.System.IO.File
        Dim mSqlDataAdapter As SqlClient.SqlDataAdapter
        Dim GHCalendarJsFile As String = Me.mPage.Request.PhysicalApplicationPath & "Interfaces\App_JScriptMenuEditor.js"
        Dim TextStream As String

        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            'Generate The Data Base Data
            If mDataSet.Tables(0).Rows.Count > 0 Then
                strSQL = "Set DateFormat DMY "
                For Each ObjDataRow In mDataSet.Tables(0).Rows
                    strSQL = strSQL & " Insert Into " & Me.mTable & " Values ('" & ObjDataRow("GDate").ToString().Replace("Õ", "") & "','" & ObjDataRow("HDate") & "');"
                Next
            End If
            mSqlCommand.CommandText = strSQL
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()



            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : oDate the current date to convert , oDateType the conversion direction 
    'Description: This function make the conversion form the hijri date to gregorian date and viseversa 
    'Steps: 
    '       1- Get the incoming Date and separate it according to the date format 
    '       2- Search for the relative date in the database return it back 
    '==================================================================
    Public Function GetRelativeDate(ByVal oDate As Date, ByVal oType As DateType) As String
        Dim StrSQLCommand As String = String.Empty
        Dim IntYear As Integer
        Dim IntMonth As Integer
        Dim IntDay As Integer
        Dim StrDate As String = String.Empty
        Dim StrReturnDate As String
        Try

            IntYear = DatePart(DateInterval.Year, oDate)
            IntMonth = DatePart(DateInterval.Month, oDate)
            IntDay = DatePart(DateInterval.Day, oDate)

            StrDate = IntDay & "/" & IntMonth & "/" & IntYear

            Select Case oType
                Case DateType.Hijri
                    StrSQLCommand = "Set Dateformat dmy Select HDate from sys_GHCalendar Where GDate ='" & StrDate & "'"
                Case DateType.Gregorian
                    StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
            End Select
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSQLCommand
            mSqlCommand.Connection.Open()
            StrReturnDate = mSqlCommand.ExecuteScalar
            mSqlCommand.Connection.Close()
            Return StrReturnDate
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : oDate the current date to convert , oDateType the conversion direction ,oDirection 
    'Description: make the convirsion between the hijri and gregorian date 
    'Steps: 
    '       1- At first we should know that this function bult on some rules 
    '           * All the date's in the database will be in gregorian date 
    '           * Now we will classify the fuction into tow parts 
    '           * First  :The date display language that the user will see (Hijri,Gregorian)
    '           * Second :The direction if this date will going (output) to the user interface or going (input) to the database 
    '           * If its going (input) to the database the function will get hijri or gregorian date but at the end the databse will get gregorian date 
    '           * If its going (output) to the user interface the function will get only gregorian date and it will produce either hijri or gregorian date 
    '==================================================================
    Public Function GetRelativeDate(ByVal iDate As Object, ByVal DateDisplayType As DateType, ByVal oDirection As Directions) As Object

        If IsNothing(iDate) And oDirection = Directions.Output Then
            Return Nothing
        ElseIf IsNothing(iDate) And oDirection = Directions.Input Then
            Return DBNull.Value
        End If

        If (oDirection = Directions.Output And (IsDBNull(iDate) Or IsNothing(iDate) Or iDate.ToString.Trim.ToLower = "null" Or iDate.ToString.Trim = "")) Then
            Return Nothing
        Else
            If (oDirection = Directions.Input And (IsDBNull(iDate) Or IsNothing(iDate) Or iDate.ToString.Trim.ToLower = "null" Or iDate.ToString.Trim = "")) Then
                Return DBNull.Value
            End If
        End If
        If (CDate(iDate).Year < 1300) Then
            Return Nothing
        End If

        If iDate = "1/1/1900" Then
            Return Nothing
        End If

        Dim oDate As Date = iDate

        Dim strFormatDate As String = ConfigurationSettings.AppSettings("DATEFORMAT")
        Dim StrSQLCommand As String = String.Empty
        Dim UmAlQuraCalender As New Global.System.Globalization.UmAlQuraCalendar
        Dim GregorianCalender As New Global.System.Globalization.GregorianCalendar
        Dim IntYear As Integer
        Dim IntMonth As Integer
        Dim IntDay As Integer

        Dim IntTempGYear As Integer
        Dim IntTempGMonth As Integer
        Dim IntTempGDay As Integer
        Dim StrTempGDate As String

        Dim IntTempHYear As Integer
        Dim IntTempHMonth As Integer
        Dim IntTempHDay As Integer
        Dim StrTempHDate As String

        Dim StrDate As String = String.Empty
        Dim StrReturnDate As String
        Dim CurrentDate As Date
        Dim GDate As Date
        Dim HDate As Date

        Dim nav As New Venus.Shared.Web.NavigationHandler(mConnectionString)

        Try

            '---------------------------------------------------------------------------'
            'This part is to prepare the data to get the relative date from the databese' 
            '---------------------------------------------------------------------------'
            IntYear = DatePart(DateInterval.Year, oDate)
            IntMonth = DatePart(DateInterval.Month, oDate)
            IntDay = DatePart(DateInterval.Day, oDate)
            StrDate = Format(IntDay, "00") & "/" & Format(IntMonth, "00") & "/" & IntYear.ToString

            '---------------------------------------------------------------------------'
            'This part to get the relative date according to microsoft standerd calender' 
            '---------------------------------------------------------------------------'
            If IntYear <= 1600 Then
                IntTempGYear = GregorianCalender.GetYear(IIf(oDate < GregorianCalender.MinSupportedDateTime, GregorianCalender.MinSupportedDateTime, oDate))
                IntTempGMonth = GregorianCalender.GetMonth(IIf(oDate < GregorianCalender.MinSupportedDateTime, GregorianCalender.MinSupportedDateTime, oDate))
                IntTempGDay = GregorianCalender.GetDayOfMonth(IIf(oDate < GregorianCalender.MinSupportedDateTime, GregorianCalender.MinSupportedDateTime, oDate))
                StrTempGDate = Format(IntTempGDay, "00") + "/" + Format(IntTempGMonth, "00") + "/" + IntTempGYear.ToString
            Else
                IntTempHYear = UmAlQuraCalender.GetYear(IIf(oDate < UmAlQuraCalender.MinSupportedDateTime, UmAlQuraCalender.MinSupportedDateTime, oDate))
                IntTempHMonth = UmAlQuraCalender.GetMonth(IIf(oDate < UmAlQuraCalender.MinSupportedDateTime, UmAlQuraCalender.MinSupportedDateTime, oDate))
                IntTempHDay = UmAlQuraCalender.GetDayOfMonth(IIf(oDate < UmAlQuraCalender.MinSupportedDateTime, UmAlQuraCalender.MinSupportedDateTime, oDate))
                StrTempHDate = Format(IntTempHDay, "00") + "/" + Format(IntTempHMonth, "00") + "/" + IntTempHYear.ToString
            End If

            '---------------------------------------------------------------------------'
            'Get the Relative Date according to the input paramters and depending on the'
            'display language                                                           '
            '---------------------------------------------------------------------------'
            Select Case DateDisplayType
                Case DateType.Hijri

                    Select Case oDirection
                        Case Directions.Input
                            If IntYear < 1600 Then
                                StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
                                mSqlCommand = New SqlClient.SqlCommand
                                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                                mSqlCommand.CommandType = CommandType.Text
                                mSqlCommand.CommandText = StrSQLCommand
                                mSqlCommand.Connection.Open()
                                StrReturnDate = mSqlCommand.ExecuteScalar
                                mSqlCommand.Connection.Close()
                                If StrReturnDate Is Nothing Then
                                    StrReturnDate = HijriToGreg(StrDate, strFormatDate)
                                    If Not IsNothing(StrReturnDate) Then
                                        '============================== Insert GH Date if not found [Start]
                                        Dim strInser As String = "Set Dateformat dmy  Insert Into " & Me.mTable & " Values ('" & StrReturnDate & "','" & StrDate & "');"
                                        mSqlCommand.CommandText = strInser
                                        mSqlCommand.Connection.Open()
                                        mSqlCommand.ExecuteNonQuery()
                                        mSqlCommand.Connection.Close()
                                        '============================== Insert GH Date if not found [ END ]
                                    Else
                                    StrReturnDate = StrTempGDate
                                End If

                                End If
                            Else
                                Return oDate
                            End If
                        Case Directions.Output
                            StrSQLCommand = "Set Dateformat dmy Select HDate from sys_GHCalendar Where GDate between '" & StrDate & " 00:00:00 " & "' And '" & StrDate & " 23:00:00 '"
                            mSqlCommand = New SqlClient.SqlCommand
                            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                            mSqlCommand.CommandType = CommandType.Text
                            mSqlCommand.CommandText = StrSQLCommand
                            mSqlCommand.Connection.Open()
                            StrReturnDate = mSqlCommand.ExecuteScalar
                            mSqlCommand.Connection.Close()
                            If StrReturnDate Is Nothing Then
                                Try
                                '============================== Insert GH Date if not found [Start]
                                Dim strInser As String = "Set Dateformat dmy  Insert Into " & Me.mTable & " Values ('" & StrDate & "','" & StrTempHDate & "');"
                                mSqlCommand.CommandText = strInser
                                mSqlCommand.Connection.Open()
                                mSqlCommand.ExecuteNonQuery()
                                mSqlCommand.Connection.Close()
                                '============================== Insert GH Date if not found [ END ]
                                StrReturnDate = StrTempHDate
                                Catch ex As SqlClient.SqlException
                                    If ex.Number = 2601 Then
                                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(mPage, nav.SetLanguage(mPage, "Found invalid saved Dates/íæÌÏ ÊæÇÑíÎ ãÍÝæÙÉ ÎØÃ"))
                                        Return Nothing
                                    End If

                                End Try
                            End If
                    End Select

                Case DateType.Gregorian
                    Select Case oDirection
                        Case Directions.Input
                            If IntYear < 1600 Then

                                StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
                                mSqlCommand = New SqlClient.SqlCommand
                                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                                mSqlCommand.CommandType = CommandType.Text
                                mSqlCommand.CommandText = StrSQLCommand
                                mSqlCommand.Connection.Open()
                                StrReturnDate = mSqlCommand.ExecuteScalar
                                mSqlCommand.Connection.Close()
                                If StrReturnDate Is Nothing Then
                                    StrReturnDate = HijriToGreg(StrDate, strFormatDate)
                                    If Not IsNothing(StrReturnDate) Then
                                        '============================== Insert GH Date if not found [Start]
                                        Dim strInser As String = "Set Dateformat dmy  Insert Into " & Me.mTable & " Values ('" & StrReturnDate & "','" & StrDate & "');"
                                        mSqlCommand.CommandText = strInser
                                        mSqlCommand.Connection.Open()
                                        mSqlCommand.ExecuteNonQuery()
                                        mSqlCommand.Connection.Close()
                                        '============================== Insert GH Date if not found [ END ]
                                    Else
                                    StrReturnDate = StrTempGDate
                                End If
                                End If

                            Else
                                Return oDate
                            End If
                        Case Directions.Output
                            Return oDate
                    End Select
            End Select


            Return StrReturnDate

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
            'Finally
            '    mSqlCommand.Connection.Close()
        End Try
    End Function

    Public Function HijriToGreg(ByVal hijri As String, ByVal format As String) As String
        Dim allFormats() As String = {"yyyy/MM/dd", _
                                      "yyyy/M/d", _
                                      "dd/MM/yyyy", _
                                      "d/M/yyyy", _
                                      "dd/M/yyyy", _
                                      "d/MM/yyyy", _
                                      "yyyy-MM-dd", _
                                      "yyyy-M-d", _
                                      "dd-MM-yyyy", _
                                      "d-M-yyyy", _
                                      "dd-M-yyyy", _
                                      "d-MM-yyyy", _
                                      "yyyy MM dd", _
                                      "yyyy M d", _
                                      "dd MM yyyy", _
                                      "d M yyyy", _
                                      "dd M yyyy", _
                                      "d MM yyyy"}

        Dim arCul As CultureInfo
        Dim enCul As CultureInfo
        Dim h As HijriCalendar
        Dim g As GregorianCalendar
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New HijriCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

        arCul.DateTimeFormat.Calendar = h

        If hijri.Length > 0 Then
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.ToString(format, enCul.DateTimeFormat)
            Catch ex As Exception
                Return Nothing
            End Try
        End If
    End Function

    '-------------------------------0257 MODIFIED-----------------------------------------
    Public Function GetRelativeDateTime(ByVal iDate As Object, ByVal DateDisplayType As DateType, ByVal oDirection As Directions) As Object

        If (oDirection = Directions.Output And (IsDBNull(iDate) Or IsNothing(iDate))) Then
            Return Nothing
        Else
            If (oDirection = Directions.Input And (IsDBNull(iDate) Or IsNothing(iDate))) Then
                Return DBNull.Value
            End If
        End If

        Dim oDate As Date = iDate

        Dim StrSQLCommand As String = String.Empty
        Dim UmAlQuraCalender As New Global.System.Globalization.UmAlQuraCalendar
        Dim GregorianCalender As New Global.System.Globalization.GregorianCalendar
        Dim IntYear As Integer
        Dim IntMonth As Integer
        Dim IntDay As Integer
        Dim IntHours As Integer
        Dim IntMinutes As Integer

        Dim IntTempGYear As Integer
        Dim IntTempGMonth As Integer
        Dim IntTempGDay As Integer
        Dim IntTempGHours As Integer
        Dim IntTempGMinutes As Integer
        Dim StrTempGDate As String

        Dim IntTempHYear As Integer
        Dim IntTempHMonth As Integer
        Dim IntTempHDay As Integer
        Dim IntTempHHours As Integer
        Dim IntTempHMinutes As Integer
        Dim StrTempHDate As String

        Dim StrDate As String = String.Empty
        Dim StrReturnDate As String
        Dim CurrentDate As Date
        Dim GDate As Date
        Dim HDate As Date

        Try

            '---------------------------------------------------------------------------'
            'This part is to prepare the data to get the relative date from the databese' 
            '---------------------------------------------------------------------------'
            IntYear = DatePart(DateInterval.Year, oDate)
            IntMonth = DatePart(DateInterval.Month, oDate)
            IntDay = DatePart(DateInterval.Day, oDate)
            IntHours = DatePart(DateInterval.Hour, oDate)
            IntMinutes = DatePart(DateInterval.Minute, oDate)
            StrDate = Format(IntDay, "00") & "/" & Format(IntMonth, "00") & "/" & IntYear.ToString & " " & Format(IntHours, "00") & ":" & Format(IntMinutes, "00")

            '---------------------------------------------------------------------------'
            'This part to get the relative date according to microsoft standerd calender' 
            '---------------------------------------------------------------------------'
            If IntYear <= 1600 Then
                IntTempGYear = GregorianCalender.GetYear(oDate)
                IntTempGMonth = GregorianCalender.GetMonth(oDate)
                IntTempGDay = GregorianCalender.GetDayOfMonth(oDate)
                IntTempGHours = GregorianCalender.GetHour(oDate)
                IntTempGMinutes = GregorianCalender.GetMinute(oDate)
                StrTempGDate = Format(IntTempGDay, "00") + "/" + Format(IntTempGMonth, "00") + "/" + IntTempGYear.ToString + " " + Format(IntTempGHours, "00") + ":" + Format(IntTempGMinutes, "00")
            Else
                IntTempHYear = UmAlQuraCalender.GetYear(oDate)
                IntTempHMonth = UmAlQuraCalender.GetMonth(oDate)
                IntTempHDay = UmAlQuraCalender.GetDayOfMonth(oDate)
                IntTempHHours = UmAlQuraCalender.GetHour(oDate)
                IntTempHMinutes = UmAlQuraCalender.GetMinute(oDate)
                StrTempHDate = Format(IntTempHDay, "00") + "/" + Format(IntTempHMonth, "00") + "/" + IntTempHYear.ToString + " " + Format(IntTempHHours, "00") + ":" + Format(IntTempHMinutes, "00")
            End If

            '---------------------------------------------------------------------------'
            'Get the Relative Date according to the input paramters and depending on the'
            'display language                                                           '
            '---------------------------------------------------------------------------'
            Select Case DateDisplayType
                Case DateType.Hijri

                    Select Case oDirection
                        Case Directions.Input
                            If IntYear < 1600 Then
                                StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
                                mSqlCommand = New SqlClient.SqlCommand
                                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                                mSqlCommand.CommandType = CommandType.Text
                                mSqlCommand.CommandText = StrSQLCommand
                                mSqlCommand.Connection.Open()
                                StrReturnDate = mSqlCommand.ExecuteScalar
                                mSqlCommand.Connection.Close()
                                If StrReturnDate Is Nothing Then
                                    StrReturnDate = StrTempGDate
                                End If
                            Else
                                Return oDate
                            End If
                        Case Directions.Output
                            StrSQLCommand = "Set Dateformat dmy Select HDate from sys_GHCalendar Where GDate ='" & StrDate & "'"
                            mSqlCommand = New SqlClient.SqlCommand
                            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                            mSqlCommand.CommandType = CommandType.Text
                            mSqlCommand.CommandText = StrSQLCommand
                            mSqlCommand.Connection.Open()
                            StrReturnDate = mSqlCommand.ExecuteScalar
                            mSqlCommand.Connection.Close()
                            If StrReturnDate Is Nothing Then
                                StrReturnDate = StrTempHDate
                            End If
                    End Select

                Case DateType.Gregorian
                    Select Case oDirection
                        Case Directions.Input
                            If IntYear < 1600 Then

                                StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
                                mSqlCommand = New SqlClient.SqlCommand
                                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                                mSqlCommand.CommandType = CommandType.Text
                                mSqlCommand.CommandText = StrSQLCommand
                                mSqlCommand.Connection.Open()
                                StrReturnDate = mSqlCommand.ExecuteScalar
                                mSqlCommand.Connection.Close()
                                If StrReturnDate Is Nothing Then
                                    StrReturnDate = StrTempGDate
                                End If

                            Else
                                Return oDate
                            End If
                        Case Directions.Output
                            Return oDate
                    End Select
            End Select


            Return StrReturnDate

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
            'Finally
            '    mSqlCommand.Connection.Close()
        End Try
    End Function
    '-------------------------------=============-----------------------------------------


    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : filter which determine the range of update 
    'Description: update spacific record or range of records 
    '==================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : filter which determine the range of Delete
    'Description: Delete spacific record or range of records 
    '==================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = "Delete From " & Me.mTable & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : Nothing 
    'Description: Clear all the property of the class 
    '==================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mGDate = Nothing
            mHDate = String.Empty

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : Nothing 
    'Description: Get the first record of the class 
    '==================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : Nothing 
    'Description: Get the last record of the class 
    '==================================================================
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : Nothing 
    'Description: Get the last record of the class 
    '==================================================================
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : Nothing 
    'Description: Get Previose record of the class depending on the last record 
    '==================================================================
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : ddlvlaue the incoming control that will take the lsit of data  
    'Description: Get Yars list  
    '==================================================================
    Public Function GetYearsList(ByRef DdlValues As Global.System.Web.UI.WebControls.DropDownList, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim YearCount As Integer
        Try

            StrCommandString = "Select Distinct Right(HDate, 4) AS HYear From " & Me.mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & Filter & " Order By Right(HDate, 4) Asc "
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Items.Clear()

            If ObjDataset.Tables(0).Rows.Count > 0 Then
                For Each ObjDataRow In ObjDataset.Tables(0).Rows
                    Item = New Global.System.Web.UI.WebControls.ListItem
                    Item.Text = mDataHandler.DataValue(ObjDataRow("HYear"), SqlDbType.VarChar)
                    DdlValues.Items.Add(Item)
                Next
            Else
                For YearCount = 1420 To 2420
                    Item = New Global.System.Web.UI.WebControls.ListItem
                    Item.Text = YearCount.ToString
                    DdlValues.Items.Add(Item)
                Next
            End If
            Return True

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function
    Public Function GetLastHdayDate(ByVal intYear As Integer) As Date
        Dim lastDate As Date = Nothing
        Dim StrSelectCommand As String = " Select Gdate From sys_GHCalendar Where  Right(HDate, 4)= '" & intYear & "' Order By gdate  Desc "
        lastDate = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, StrSelectCommand)
        Return (lastDate.AddDays(1))
    End Function

#End Region

#Region "Class Private Function"
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : Ds which represent the incoming data from any filter  
    'Description: get the data and distribute it over the property 
    '==================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mGDate = mDataHandler.DataValue_Out(.Item("GDate"), SqlDbType.DateTime)
                mHDate = mDataHandler.DataValue_Out(.Item("HDate"), SqlDbType.VarChar)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : DataOcean
    'Date : 26/11/2007
    'Input : SqlCommand 
    'Description: this function is fill all the command parameters according to class property values 
    '==================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mGDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HDate", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHDate, SqlDbType.VarChar)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region

#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region

End Class

