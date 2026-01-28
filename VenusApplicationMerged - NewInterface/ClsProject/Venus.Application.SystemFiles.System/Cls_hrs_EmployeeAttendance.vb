'======================================================================
'Project name  : Venus V. 
'Program name  : clsFingerprintSettings.vb
'Date Created  : 20-09-2021
'Issue #       :       
'Developer     : Hassan Kurdi
'Description   : For fingerprint settings
'              : 
'              : 
'              : 
'Modifacations :
'======================================================================

Imports System.Data.SqlClient

Public Class Cls_hrs_EmployeeAttendance
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
    End Sub
#End Region
#Region "Private Members"
    '=====================================================================
    'Description :  Private members declrations
    '=====================================================================
    Private mDayNo As Integer
    Private mTimeIn As Date
    Private mTimeOut As Date
    Private mTimeIn2nd As Date
    Private mTimeOut2nd As Date
    Private mIsDayOff As Boolean
    Private mFirstShiftTimeInFingerprintStart As Date
    Private mFirstShiftEntryTimeInClose As Date
    Private mFirstShiftTimeOutFingerprintClose As Date
    Private mSecondShiftTimeInFingerprintStart As Date
    Private mSecondShiftEntryTimeInClose As Date
    Private mSecondShiftTimeOutFingerprintClose As Date
#End Region
#Region "Public Properties"
    Public Property DayNo() As Integer
        Get
            Return mDayNo
        End Get
        Set(ByVal value As Integer)
            mDayNo = value
        End Set
    End Property
    Public Property TimeIn() As Date
        Get
            Return mTimeIn
        End Get
        Set(ByVal value As Date)
            mTimeIn = value
        End Set
    End Property
    Public Property TimeOut() As Date
        Get
            Return mTimeOut
        End Get
        Set(ByVal value As Date)
            mTimeOut = value
        End Set
    End Property
    Public Property TimeIn2nd() As Date
        Get
            Return mTimeIn2nd
        End Get
        Set(ByVal value As Date)
            mTimeIn2nd = value
        End Set
    End Property
    Public Property TimeOut2nd() As Date
        Get
            Return mTimeOut2nd
        End Get
        Set(ByVal value As Date)
            mTimeOut2nd = value
        End Set
    End Property
    Public Property IsDayOff() As Boolean
        Get
            Return mIsDayOff
        End Get
        Set(ByVal value As Boolean)
            mIsDayOff = value
        End Set
    End Property
    Public Property FirstShiftTimeInFingerprintStart() As Date
        Get
            Return mFirstShiftTimeInFingerprintStart
        End Get
        Set(ByVal value As Date)
            mFirstShiftTimeInFingerprintStart = value
        End Set
    End Property
    Public Property FirstShiftEntryTimeInClose() As Date
        Get
            Return mFirstShiftEntryTimeInClose
        End Get
        Set(ByVal value As Date)
            mFirstShiftEntryTimeInClose = value
        End Set
    End Property
    Public Property FirstShiftTimeOutFingerprintClose() As Date
        Get
            Return mFirstShiftTimeOutFingerprintClose
        End Get
        Set(ByVal value As Date)
            mFirstShiftTimeOutFingerprintClose = value
        End Set
    End Property
    Public Property SecondShiftTimeInFingerprintStart() As Date
        Get
            Return mSecondShiftTimeInFingerprintStart
        End Get
        Set(ByVal value As Date)
            mSecondShiftTimeInFingerprintStart = value
        End Set
    End Property
    Public Property SecondShiftEntryTimeInClose() As Date
        Get
            Return mSecondShiftEntryTimeInClose
        End Get
        Set(ByVal value As Date)
            mSecondShiftEntryTimeInClose = value
        End Set
    End Property
    Public Property SecondShiftTimeOutFingerprintClose() As Date
        Get
            Return mSecondShiftTimeOutFingerprintClose
        End Get
        Set(ByVal value As Date)
            mSecondShiftTimeOutFingerprintClose = value
        End Set
    End Property
#End Region
#Region "Class Public Function"
    Public Function GetEmployeeAttendance(EmpID As Integer, FromDate As Date) As Boolean

        Try
            Dim mSqlCommand As New SqlClient.SqlCommand
            Dim da As New SqlDataAdapter
            Dim dt As DataSet = New DataSet

            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.StoredProcedure
            mSqlCommand.CommandText = "Att_GetCustomAttandace"
            mSqlCommand.Parameters.AddWithValue("@EmployeeID ", Odbc.OdbcType.NVarChar).Value = EmpID
            mSqlCommand.Parameters.AddWithValue("@Attandancedate ", Odbc.OdbcType.SmallDateTime).Value = FromDate
            mSqlCommand.Connection.Open()
            da.SelectCommand = mSqlCommand
            da.Fill(dt)
            mSqlCommand.Connection.Close()

            If mDataHandler.CheckValidDataObject(dt) Then
                GetParameter(dt, FromDate)
                Return True
            Else
                Clear()
                Return False
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function Clear() As Boolean
        Try
            mDayNo = 0
            mTimeIn = Nothing
            mTimeOut = Nothing
            mTimeIn2nd = Nothing
            mTimeOut2nd = Nothing
            mIsDayOff = 0
            mFirstShiftTimeInFingerprintStart = Nothing
            mFirstShiftEntryTimeInClose = Nothing
            mFirstShiftTimeOutFingerprintClose = Nothing
            mSecondShiftTimeInFingerprintStart = Nothing
            mSecondShiftEntryTimeInClose = Nothing
            mSecondShiftTimeOutFingerprintClose = Nothing

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Class Private Function"
    'Description:   Asign the Result of Ds to the private members of the class
    '=====================================================================
    Private Function GetParameter(ByVal Ds As DataSet, FromDate As Date) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mDayNo = mDataHandler.DataValue_Out(.Item("DayNo"), SqlDbType.Int)

                Dim dbTimeIn As String = mDataHandler.DataValue_Out(.Item("TimeIn"), SqlDbType.VarChar)
                If Not IsDBNull(dbTimeIn) And dbTimeIn <> "" Then
                    Dim Hour As Integer = Convert.ToInt32(dbTimeIn.ToString().Split(":")(0).ToString())
                    Dim mintues As Integer = Convert.ToInt32(dbTimeIn.ToString().Split(":")(1).ToString())
                    mTimeIn = New DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Hour, mintues, 0)
                Else
                    mTimeIn = Date.MinValue
                End If

                Dim dbTimeOut As String = mDataHandler.DataValue_Out(.Item("TimeOut"), SqlDbType.VarChar)
                If Not IsDBNull(dbTimeOut) And dbTimeOut <> "" Then
                    Dim Hour As Integer = Convert.ToInt32(dbTimeOut.ToString().Split(":")(0).ToString())
                    Dim mintues As Integer = Convert.ToInt32(dbTimeOut.ToString().Split(":")(1).ToString())
                    mTimeOut = New DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Hour, mintues, 0)
                Else
                    mTimeOut = Date.MinValue
                End If

                Dim dbTimeIn2nd As String = mDataHandler.DataValue_Out(.Item("TimeIn2nd"), SqlDbType.VarChar)
                If Not IsDBNull(dbTimeIn2nd) And dbTimeIn2nd <> "" Then
                    Dim Hour As Integer = Convert.ToInt32(dbTimeIn2nd.ToString().Split(":")(0).ToString())
                    Dim mintues As Integer = Convert.ToInt32(dbTimeIn2nd.ToString().Split(":")(1).ToString())
                    mTimeIn2nd = New DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Hour, mintues, 0)
                Else
                    mTimeIn2nd = Date.MinValue
                End If

                Dim dbTimeOut2nd As String = mDataHandler.DataValue_Out(.Item("TimeOut2nd"), SqlDbType.VarChar)
                If Not IsDBNull(dbTimeOut2nd) And dbTimeOut2nd <> "" Then
                    Dim Hour As Integer = Convert.ToInt32(dbTimeOut2nd.ToString().Split(":")(0).ToString())
                    Dim mintues As Integer = Convert.ToInt32(dbTimeOut2nd.ToString().Split(":")(1).ToString())
                    mTimeOut2nd = New DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Hour, mintues, 0)
                Else
                    mTimeOut2nd = Date.MinValue
                End If

                Dim dbFirstShiftTimeInFingerprintStart As String = mDataHandler.DataValue_Out(.Item("FirstShiftTimeInFingerprintStart"), SqlDbType.VarChar)
                If Not IsDBNull(dbFirstShiftTimeInFingerprintStart) And dbFirstShiftTimeInFingerprintStart <> "" Then
                    Dim Hour As Integer = Convert.ToInt32(dbFirstShiftTimeInFingerprintStart.ToString().Split(":")(0).ToString())
                    Dim mintues As Integer = Convert.ToInt32(dbFirstShiftTimeInFingerprintStart.ToString().Split(":")(1).ToString())
                    mFirstShiftTimeInFingerprintStart = New DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Hour, mintues, 0)
                Else
                    mFirstShiftTimeInFingerprintStart = Date.MinValue
                End If

                Dim dbFirstShiftEntryTimeInClose As String = mDataHandler.DataValue_Out(.Item("FirstShiftEntryTimeInClose"), SqlDbType.VarChar)
                If Not IsDBNull(dbFirstShiftEntryTimeInClose) And dbFirstShiftEntryTimeInClose <> "" Then
                    Dim Hour As Integer = Convert.ToInt32(dbFirstShiftEntryTimeInClose.ToString().Split(":")(0).ToString())
                    Dim mintues As Integer = Convert.ToInt32(dbFirstShiftEntryTimeInClose.ToString().Split(":")(1).ToString())
                    mFirstShiftEntryTimeInClose = New DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Hour, mintues, 0)
                Else
                    mFirstShiftEntryTimeInClose = Date.MinValue
                End If

                Dim dbFirstShiftTimeOutFingerprintClose As String = mDataHandler.DataValue_Out(.Item("FirstShiftTimeOutFingerprintClose"), SqlDbType.VarChar)
                If Not IsDBNull(dbFirstShiftTimeOutFingerprintClose) And dbFirstShiftTimeOutFingerprintClose <> "" Then
                    Dim Hour As Integer = Convert.ToInt32(dbFirstShiftTimeOutFingerprintClose.ToString().Split(":")(0).ToString())
                    Dim mintues As Integer = Convert.ToInt32(dbFirstShiftTimeOutFingerprintClose.ToString().Split(":")(1).ToString())
                    mFirstShiftTimeOutFingerprintClose = New DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Hour, mintues, 0)
                Else
                    mFirstShiftTimeOutFingerprintClose = Date.MinValue
                End If

                Dim dbSecondShiftTimeInFingerprintStart As String = mDataHandler.DataValue_Out(.Item("SecondShiftTimeInFingerprintStart"), SqlDbType.VarChar)
                If Not IsDBNull(dbSecondShiftTimeInFingerprintStart) And dbSecondShiftTimeInFingerprintStart <> "" Then
                    Dim Hour As Integer = Convert.ToInt32(dbSecondShiftTimeInFingerprintStart.ToString().Split(":")(0).ToString())
                    Dim mintues As Integer = Convert.ToInt32(dbSecondShiftTimeInFingerprintStart.ToString().Split(":")(1).ToString())
                    mSecondShiftTimeInFingerprintStart = New DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Hour, mintues, 0)
                Else
                    mSecondShiftTimeInFingerprintStart = Date.MinValue
                End If

                Dim dbSecondShiftEntryTimeInClose As String = mDataHandler.DataValue_Out(.Item("SecondShiftEntryTimeInClose"), SqlDbType.VarChar)
                If Not IsDBNull(dbSecondShiftEntryTimeInClose) And dbSecondShiftEntryTimeInClose <> "" Then
                    Dim Hour As Integer = Convert.ToInt32(dbSecondShiftEntryTimeInClose.ToString().Split(":")(0).ToString())
                    Dim mintues As Integer = Convert.ToInt32(dbSecondShiftEntryTimeInClose.ToString().Split(":")(1).ToString())
                    mSecondShiftEntryTimeInClose = New DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Hour, mintues, 0)
                Else
                    mSecondShiftEntryTimeInClose = Date.MinValue
                End If

                Dim dbSecondShiftTimeOutFingerprintClose As String = mDataHandler.DataValue_Out(.Item("SecondShiftTimeOutFingerprintClose"), SqlDbType.VarChar)
                If Not IsDBNull(dbSecondShiftTimeOutFingerprintClose) And dbSecondShiftTimeOutFingerprintClose <> "" Then
                    Dim Hour As Integer = Convert.ToInt32(dbSecondShiftTimeOutFingerprintClose.ToString().Split(":")(0).ToString())
                    Dim mintues As Integer = Convert.ToInt32(dbSecondShiftTimeOutFingerprintClose.ToString().Split(":")(1).ToString())
                    mSecondShiftTimeOutFingerprintClose = New DateTime(FromDate.Year, FromDate.Month, FromDate.Day, Hour, mintues, 0)
                Else
                    mSecondShiftTimeOutFingerprintClose = Date.MinValue
                End If

                mIsDayOff = mDataHandler.DataValue_Out(.Item("IsDayOff"), SqlDbType.Bit)

            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '=====================================================================
#End Region
End Class
