'======================================================================
'Project name  : Venus V. 
'Program name  : clsFingerprintSettings.vb
'Date Created  : 21-09-2021
'Issue #       :       
'Developer     : Hassan Kurdi
'Description   : For Employee Fingerprints
'              : 
'              : 
'              : 
'Modifacations :
'======================================================================
Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.System

Public Class Cls_EmployeeFingerPrint
    Inherits ClsDataAcessLayer

#Region "Class Constructor"
    '========================================================================
    'ProcedureName  :  new()
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Initialize Class Sql Related Members 
    'Developer      :   
    'Date Created   :  08-07-2021
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesFingerprints"
        mSelectCommand = " SELECT * FROM  " & mTable
    End Sub
#End Region
#Region "Private Members"
    '=====================================================================
    'Description :  Private members declrations
    '=====================================================================
    Private mID As Integer
    Private mUserId As Integer
    Private mUserCode As Integer
    Private mFingerprintTime As Date
    Private mFirstShiftIn As Date
    Private mFirstShiftOut As Date
    Private mSecondShiftIn As Date
    Private mSecondShiftOut As Date
#End Region
#Region "Public Properties"
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal value As Integer)
            mID = value
        End Set
    End Property
    Public Property UserId() As Integer
        Get
            Return mUserId
        End Get
        Set(ByVal value As Integer)
            mUserId = value
        End Set
    End Property
    Public Property UserCode() As String
        Get
            Return mUserCode
        End Get
        Set(ByVal value As String)
            mUserCode = value
        End Set
    End Property
    Public Property FingerprintTime() As Date
        Get
            Return mFingerprintTime
        End Get
        Set(ByVal value As Date)
            mFingerprintTime = value
        End Set
    End Property
    Public Property FirstShiftIn() As Date
        Get
            Return mFirstShiftIn
        End Get
        Set(ByVal value As Date)
            mFirstShiftIn = value
        End Set
    End Property
    Public Property FirstShiftOut() As Date
        Get
            Return mFirstShiftOut
        End Get
        Set(ByVal value As Date)
            mFirstShiftOut = value
        End Set
    End Property
    Public Property SecondShiftIn() As Date
        Get
            Return mSecondShiftIn
        End Get
        Set(ByVal value As Date)
            mSecondShiftIn = value
        End Set
    End Property
    Public Property SecondShiftOut() As Date
        Get
            Return mSecondShiftOut
        End Get
        Set(ByVal value As Date)
            mSecondShiftOut = value
        End Set
    End Property
#End Region
#Region "Public Function"
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            '==================== Order By Modification [Start]
            StrSelectCommand = StrSelectCommand & orderByStr
            '==================== Order By Modification [ End ]
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    ' First shift - One shift - Timein - Old Method
    Public Function GetFirstShiftTimeinFingerPrintOld(TimeIn As Date, TimeOut As Date, UserCode As String)
        Dim SrcCommand As String = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & TimeIn.AddHours(-3).ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & TimeOut.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime ASC"
        Try
            Dim mSqlCommand As New SqlClient.SqlCommand
            Dim da As New SqlDataAdapter
            Dim dt As DataSet = New DataSet

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            FirstShiftIn = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    ' First shift - One shift - Timeout - Old Method
    Public Function GetFirstShiftTimeoutFingerPrintOld(TimeIn As Date, TimeOut As Date, UserCode As String)

        Dim SrcCommand As String = ""

        If TimeOut < TimeIn Then
            SrcCommand = "SET DATEFORMAT dmy; SELECT FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & TimeIn.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & TimeOut.AddDays(1).AddHours(3).ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"
        Else
            SrcCommand = "SET DATEFORMAT dmy; SELECT FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & TimeIn.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & TimeOut.AddHours(3).ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"
        End If

        Try
            Dim mSqlCommand As New SqlClient.SqlCommand
            Dim da As New SqlDataAdapter
            Dim dt As DataSet = New DataSet

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            FirstShiftOut = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    'First shift - One shift - Timein - New Method
    Public Function GetFirstShiftTimeinFingerPrint(FirstShiftTimeInFingerprintStart As Date, FirstShiftEntryTimeInClose As Date, TimeIn As Date, UserCode As String)
        'Rabie 22-10-2024
        'Add one day + in case time in close < time in start to get time in thnext day
        Dim SrcCommand As String = ""
        If FirstShiftEntryTimeInClose < FirstShiftTimeInFingerprintStart Then
            If TimeIn < FirstShiftTimeInFingerprintStart Then
                SrcCommand = "SET DATEFORMAT dmy; SELECT FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftTimeInFingerprintStart.AddDays(-1).ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftEntryTimeInClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime ASC"
            Else
                SrcCommand = "SET DATEFORMAT dmy; SELECT FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftEntryTimeInClose.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime ASC"
            End If
        Else
            If TimeIn < FirstShiftTimeInFingerprintStart Then
                SrcCommand = "SET DATEFORMAT dmy; SELECT FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftTimeInFingerprintStart.AddDays(-1).ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftEntryTimeInClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime ASC"
            Else
                SrcCommand = "SET DATEFORMAT dmy; SELECT FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftEntryTimeInClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime ASC"
            End If
        End If


        Try
            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            FirstShiftIn = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    'FlexableFingerPrintTimeIn Rabie 4-1-2023
    Public Function GetFlexableFingerPrintTimein(TransDatetime As Date, TimeIn As Date, UserCode As String)

        Dim SrcCommand As String = ""

        SrcCommand = "SET DATEFORMAT dmy; SELECT ISNULL(CAST(MIN(FingerprintTime) AS VARCHAR(25)),'1/1/0001') FROM " & mTable & " WHERE convert (DATE,fingerprinttime) = '" & TransDatetime & "' AND UserCode = '" & UserCode & "'"


        Try
            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            FirstShiftIn = mSqlCommand.ExecuteScalar()

            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function



    ' First shift - One shift - Timeout - New Method
    Public Function GetFirstShiftTimeoutFingerPrint(FirstShiftTimeInFingerprintStart As Date, FirstShiftEntryTimeInClose As Date, FirstShiftTimeOutFingerprintClose As Date, TimeOut As Date, UserCode As String)

        Dim SrcCommand As String = ""
        Dim firstShiftFingerprintCount As Integer = 0
        If FirstShiftTimeInFingerprintStart > FirstShiftTimeOutFingerprintClose Then
            SrcCommand = "SET DATEFORMAT dmy; SELECT COUNT(*) FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftTimeOutFingerprintClose.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "'"
        Else
            SrcCommand = "SET DATEFORMAT dmy; SELECT COUNT(*) FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftTimeOutFingerprintClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "'"
        End If

        Try
            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            firstShiftFingerprintCount = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try


        If (TimeOut < FirstShiftTimeInFingerprintStart Or FirstShiftTimeInFingerprintStart > FirstShiftTimeOutFingerprintClose And FirstShiftEntryTimeInClose > Date.MinValue
            ) Then
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftEntryTimeInClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftTimeOutFingerprintClose.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"
        ElseIf firstShiftFingerprintCount = 1 Then

            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftTimeOutFingerprintClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"

        ElseIf firstShiftFingerprintCount > 1 Then
            'Rabie 9-9-2024
            'SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftTimeOutFingerprintClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' And FingerprintTime>'" & FirstShiftEntryTimeInClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftTimeOutFingerprintClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"

        Else
            Return False
        End If

        Try

            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            FirstShiftOut = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try


    End Function
    'Rabie 4-12-2022 Flexable Finger Print

    Public Function GetFlexableFingerPrintout(FirstShiftTimeInFingerprintStart As Date, FirstShiftEntryTimeInClose As Date, FirstShiftTimeOutFingerprintClose As Date, TimeOut As Date, UserCode As String)

        Dim SrcCommand As String = ""
        Dim firstShiftFingerprintCount As Integer = 0
        If FirstShiftTimeInFingerprintStart > FirstShiftTimeOutFingerprintClose Then
            SrcCommand = "SET DATEFORMAT dmy; SELECT COUNT(*) FROM " & mTable & " WHERE FingerprintTime >= '" & FirstShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & FirstShiftTimeOutFingerprintClose.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "'"
        Else
            SrcCommand = "SET DATEFORMAT dmy; SELECT COUNT(*) FROM " & mTable & " WHERE CONVERT(date, FingerprintTime)  >= '" & FirstShiftTimeInFingerprintStart.ToString("dd/MM/yyyy") & "' AND CONVERT(date, FingerprintTime)  <= '" & FirstShiftTimeOutFingerprintClose.ToString("dd/MM/yyyy") & "' AND UserCode = '" & UserCode & "'"
        End If

        Try
            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            firstShiftFingerprintCount = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try


        If firstShiftFingerprintCount > 1 Then
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE  CONVERT(date, FingerprintTime) >= '" & FirstShiftTimeInFingerprintStart.ToString("dd/MM/yyyy") & "' AND  CONVERT(date, FingerprintTime) <= '" & FirstShiftTimeOutFingerprintClose.ToString("dd/MM/yyyy") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"
        Else
            Return False
        End If

        Try
            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            FirstShiftOut = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try


    End Function
    ' Second shift - Two shifts - Timein - Old Method
    Public Function GetSecondShiftTimeinFingerPrintOld(TimeIn2nd As Date, TimeOut2nd As Date, UserCode As String)
        Dim SrcCommand As String = ""
        If (FirstShiftOut = Nothing) Then
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime > '" & TimeIn2nd.AddMinutes(-70).ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '"
        Else
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime > '" & FirstShiftOut.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '"
        End If

        If (TimeOut2nd < TimeIn2nd) Then
            SrcCommand = SrcCommand & TimeOut2nd.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime ASC"
        Else
            SrcCommand = SrcCommand & TimeOut2nd.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime ASC"
        End If

        Try
            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            SecondShiftIn = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    ' Second shift - Two shifts - Timeout - Old Method
    Public Function GetSecondShiftTimeoutFingerPrintOld(TimeIn2nd As Date, TimeOut2nd As Date, UserCode As String)
        Dim SrcCommand As String = ""
        If (SecondShiftIn = Nothing) Then
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime > '" & TimeIn2nd.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '"
        Else
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime > '" & SecondShiftIn.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '"
        End If

        If (TimeOut2nd < TimeIn2nd) Then
            SrcCommand = SrcCommand & TimeOut2nd.AddDays(1).AddHours(3).ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"
        Else
            SrcCommand = SrcCommand & TimeOut2nd.AddHours(3).ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"
        End If

        Try
            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            SecondShiftOut = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    ' Second shift - Two shifts - Timein - New Method
    Public Function GetSecondShiftTimeinFingerPrint(SecondShiftTimeInFingerprintStart As Date, SecondShiftEntryTimeInClose As Date, TimeIn2nd As Date, UserCode As String)
        Dim SrcCommand As String = ""

        If (TimeIn2nd < SecondShiftTimeInFingerprintStart) Then
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & SecondShiftTimeInFingerprintStart.AddDays(-1).ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & SecondShiftEntryTimeInClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime ASC"
        Else
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime > '" & SecondShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & SecondShiftEntryTimeInClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime ASC"
        End If

        Try
            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            SecondShiftIn = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    ' Second shift - Two shifts - Timeout - New Method
    Public Function GetSecondShiftTimeoutFingerPrint(SecondShiftTimeInFingerprintStart As Date, SecondShiftTimeOutFingerprintClose As Date, TimeOut2nd As Date, SecondShiftEntryTimeInClose As Date, UserCode As String)

        Dim SrcCommand As String = ""
        Dim firstShiftFingerprintCount As Integer = 0

        If (SecondShiftTimeInFingerprintStart > SecondShiftTimeOutFingerprintClose) Then
            SecondShiftTimeOutFingerprintClose = SecondShiftTimeOutFingerprintClose.AddDays(1)
        End If

        SrcCommand = "SET DATEFORMAT dmy; SELECT COUNT(*) FROM " & mTable & " WHERE FingerprintTime >= '" & SecondShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & SecondShiftTimeOutFingerprintClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "'"

        Try
            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            firstShiftFingerprintCount = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

        If (TimeOut2nd < SecondShiftTimeInFingerprintStart) Then
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & SecondShiftEntryTimeInClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & SecondShiftTimeInFingerprintStart.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"
        ElseIf firstShiftFingerprintCount > 1 Then
            SrcCommand = "SET DATEFORMAT dmy; SELECT TOP 1 FingerprintTime FROM " & mTable & " WHERE FingerprintTime >= '" & SecondShiftTimeInFingerprintStart.ToString("dd/MM/yyyy HH:mm:ss") & "' AND FingerprintTime <= '" & SecondShiftTimeOutFingerprintClose.ToString("dd/MM/yyyy HH:mm:ss") & "' AND UserCode = '" & UserCode & "' ORDER BY FingerprintTime DESC"
        Else
            Return False
        End If

        Try
            Dim mSqlCommand As New SqlClient.SqlCommand

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = SrcCommand
            mSqlCommand.Connection.Open()
            SecondShiftOut = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
    ' Late Old Method
    Public Function GetLateOld(EmpTimein As Date, Timein As Date) As Double
        Dim totalLate As Double = 0

        If (EmpTimein <= Date.MinValue) Then
            Return totalLate
        End If

        If (Timein <= Date.MinValue) Then
            Return totalLate
        End If

        If (EmpTimein > Timein) Then
            totalLate = (EmpTimein - Timein).TotalMinutes / 60
            Return totalLate
        End If

        Return totalLate
    End Function
    ' Late New Method
    Public Function GetLate(EmpTimein As Date, Timein As Date, ShiftEntryTimeInClose As Date) As Double
        Dim totalLate As Double
        If (EmpTimein <= Date.MinValue) Then
            Return totalLate
        End If

        If (Timein <= Date.MinValue) Then
            Return totalLate
        End If

        If (ShiftEntryTimeInClose <= Date.MinValue) Then
            Return totalLate
        End If

        If (EmpTimein > Timein And EmpTimein < ShiftEntryTimeInClose) Then
            totalLate = (EmpTimein - Timein).TotalMinutes
            Return totalLate / 60
        End If

        Return totalLate
    End Function
    '
    Public Function GetLeaveEarlyOld(EmpTimeOut As Date, TimeOut As Date) As Double
        Dim totalLate As Double = 0

        If (EmpTimeOut <= Date.MinValue) Then
            Return totalLate
        End If

        If (TimeOut <= Date.MinValue) Then
            Return totalLate
        End If

        If (EmpTimeOut < TimeOut) Then
            totalLate = (TimeOut - EmpTimeOut).TotalMinutes / 60
            Return totalLate
        End If

        Return totalLate

    End Function
    '
    Public Function GetLeaveEarly(EmpTimein As Date, EmpTimeOut As Date, Timein As Date, TimeOut As Date) As Double
        Dim totalLate As Double = 0

        If (EmpTimein <= Date.MinValue) Then
            Return totalLate
        End If

        If (EmpTimeOut <= Date.MinValue) Then
            Return totalLate
        End If

        If (Timein <= Date.MinValue) Then
            Return totalLate
        End If

        If (TimeOut <= Date.MinValue) Then
            Return totalLate
        End If

        If ((EmpTimeOut - Timein).TotalMinutes / 60) < ((TimeOut - Timein).TotalMinutes / 60) Then
            totalLate = (TimeOut - EmpTimeOut).TotalMinutes
            Return totalLate / 60
        Else
            Return totalLate
        End If

    End Function
    ''''''Rabie 4-01-2022
    'Total Late in FlexableFingerPrint
    Public Function GetFlexableFingerPrintTotalLate(EmpTimein As Date, EmpTimeOut As Date, Timein As Date, TimeOut As Date) As Double
        Dim totalLate As Double = 0

        If EmpTimeOut > "01/01/1980 12:00:00 AM" Then
            totalLate = (EmpTimeOut - EmpTimein).TotalMinutes
            Return totalLate / 60
        Else
            totalLate = 0
            Return totalLate

        End If



    End Function
    ' Overtime
    Public Function GetOvertime(EmpTimeOut As Date, TimeOut As Date) As Double
        Dim overtime As Double = 0

        If (EmpTimeOut <= Date.MinValue) Then
            Return overtime
        End If

        If (TimeOut <= Date.MinValue) Then
            Return overtime
        End If

        If (TimeOut > EmpTimeOut And TimeOut = EmpTimeOut) Then
            overtime = (720 - (TimeOut.TimeOfDay.TotalMinutes - EmpTimeOut.TimeOfDay.TotalMinutes)) / 60
        ElseIf (TimeOut.Date < EmpTimeOut.Date) Then
            overtime = (Convert.ToDateTime(EmpTimeOut).Subtract(Convert.ToDateTime(TimeOut))).TotalMinutes / 60
        Else
            overtime = (EmpTimeOut.TimeOfDay.TotalMinutes - TimeOut.TimeOfDay.TotalMinutes) / 60
        End If

        If overtime > 0 Then
            Return overtime
        Else
            overtime = 0
            Return overtime
        End If

    End Function
    Public Function GetOvertime(EmpTimeOut As Date, TimeOut As Date, Hours As Double) As Double
        Dim overtime As Double = 0

        If (EmpTimeOut <= Date.MinValue) Then
            Return overtime
        End If

        If (TimeOut <= Date.MinValue) Then
            Return overtime
        End If

        If (TimeOut > EmpTimeOut And TimeOut = EmpTimeOut) Then
            overtime = (720 - (TimeOut.TimeOfDay.TotalMinutes - EmpTimeOut.TimeOfDay.TotalMinutes)) / 60
        Else
            overtime = (EmpTimeOut.TimeOfDay.TotalMinutes - TimeOut.TimeOfDay.TotalMinutes) / 60
        End If

        If overtime > Hours Then
            Return Hours
        End If

        If overtime > 0 Then
            Return overtime
        Else
            overtime = 0
            Return overtime
        End If

    End Function
    '
    Public Function CheckForEmployeeExcuse(EmpTimeIn As Date, EmpTimeout As Date, TimeIn As Date, TimeOut As Date, EntryTimeInClose As Date,
                                           EmployeesExcuses As Clshrs_EmployeesExcuses) As Object
        Dim late As Double = 0
        Dim leaveEarly As Double = 0
        Dim totalLate As Double = 0
        Dim totalHour As Double = 0
        Dim excuseTimein As Date = Date.MinValue
        Dim excuseTimeout As Date = Date.MinValue

        For Each row As DataRow In EmployeesExcuses.DataSet.Tables(0).Rows
            If (row("ExcuseType").ToString() = "Full" And row("ExcuseCalcType").ToString() <> "3") Then
                totalLate = 0
                totalHour = (TimeOut - TimeIn).Minutes

            Else
                Dim d As Double = 0

                If (Not IsDBNull(row("ExcuseHours"))) Then
                    d = Convert.ToDateTime(row("ExcuseHours")).Hour + (Convert.ToDateTime(row("ExcuseHours")).Minute / 60)
                End If

                If (row("ExcuseType").ToString() = "IN") Then
                    excuseTimein = IIf(EmpTimeIn > Date.MinValue, EmpTimeIn, TimeIn)
                Else
                    excuseTimein = EmpTimeIn
                End If

                If (row("ExcuseType").ToString() = "Out") Then
                    excuseTimeout = IIf(EmpTimeout > Date.MinValue, EmpTimeout, TimeOut)
                Else
                    excuseTimeout = EmpTimeout
                End If

                late = GetLate(excuseTimein, TimeIn, EntryTimeInClose)
                late = IIf(late > d, late - d, 0)
                late = IIf(Math.Round(late, 1) > 0, late, 0)

                leaveEarly = GetLeaveEarly(TimeIn, excuseTimeout, TimeIn, TimeOut)
                leaveEarly = IIf(leaveEarly > d, leaveEarly - d, 0)
                leaveEarly = IIf(Math.Round(leaveEarly, 1) > 0, leaveEarly, 0)

                totalLate = late + leaveEarly
                totalHour = IIf(excuseTimeout > Date.MinValue And excuseTimein > Date.MinValue, (excuseTimeout - excuseTimein).Hours, 0)

            End If
        Next
        Return New With {Key .totalLate = totalLate, .totalHour = totalHour}
    End Function
    Public Function Clear() As Boolean
        Try
            mID = 0
            mUserId = 0
            mFingerprinttime = Date.MinValue
            mFirstShiftIn = Date.MinValue
            mFirstShiftOut = Date.MinValue
            mSecondShiftIn = Date.MinValue
            mSecondShiftOut = Date.MinValue
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Class Private Function"
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mUserId = mDataHandler.DataValue_Out(.Item("UserId"), SqlDbType.Int)
                mFingerprintTime = mDataHandler.DataValue_Out(.Item("IsDefault"), SqlDbType.SmallDateTime)
                mUserCode = mDataHandler.DataValue_Out(.Item("UserCode"), SqlDbType.VarChar)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region

End Class
