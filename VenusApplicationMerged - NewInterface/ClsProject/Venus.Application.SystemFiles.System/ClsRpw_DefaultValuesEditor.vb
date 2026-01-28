Public Class Clshrs_DefaultValuesSolver

#Region "Public Decleration"

    Private mStrConnectionString As String
    Private mStrTableName As String
    Private mIntID As Integer
    Private mStrErrorMsg As String
    Private mDblOutput As Double
    Private mPassed As Boolean
    Private mEmployeeID As Integer
    Private mFiscalPeriodID As Integer
    Private mDataset As New DataSet
    Private mPage As Global.System.Web.UI.Page
    Private mOverTimePerPeriod As Double
    Private mHolidayHoursperPeriod As Double
    Private mSalaryPricePerHour As Double
    Private mSalaryPricePerDay As Double
    Private mHolidayPrepare As Boolean

#End Region

#Region "Public Property"

    Public Property ID() As Integer
        Get
            Return mIntID
        End Get
        Set(ByVal value As Integer)
            mIntID = value
        End Set
    End Property

    Public Property ConnectionString() As String
        Get
            Return mStrConnectionString
        End Get
        Set(ByVal value As String)
            mStrConnectionString = value
        End Set
    End Property

    Public Property TableName() As String
        Get
            Return mStrTableName
        End Get
        Set(ByVal value As String)
            mStrTableName = value
        End Set
    End Property

    Public ReadOnly Property ErorrMsg() As String
        Get
            Return mStrErrorMsg
        End Get
    End Property

    Public ReadOnly Property Output() As Double
        Get
            Return mDblOutput
        End Get
    End Property

    Public ReadOnly Property Passed() As Boolean
        Get
            Return mPassed
        End Get
    End Property

    Public Property EmployeeID() As Integer
        Get
            Return mEmployeeID
        End Get
        Set(ByVal value As Integer)
            mEmployeeID = value
        End Set
    End Property

    Public Property FiscalPeriodID() As Integer
        Get
            Return mFiscalPeriodID
        End Get
        Set(ByVal value As Integer)
            mFiscalPeriodID = value
        End Set
    End Property

    Public Property Page() As Global.System.Web.UI.Page
        Get
            Return mPage
        End Get
        Set(ByVal value As Global.System.Web.UI.Page)
            mPage = value
        End Set
    End Property

    Public Property OverTimePerPeriod() As Double
        Get
            Return mOverTimePerPeriod
        End Get
        Set(ByVal value As Double)
            mOverTimePerPeriod = value
        End Set
    End Property

    Public Property HolidayHoursperPeriod() As Double
        Get
            Return mHolidayHoursperPeriod
        End Get
        Set(ByVal value As Double)
            mHolidayHoursperPeriod = value
        End Set
    End Property
#End Region

#Region "Public Constructor"

    Public Sub New(ByVal ConnectionString As String, ByVal ObjPage As Global.System.Web.UI.Page)
        mStrConnectionString = ConnectionString
        mPage = ObjPage
    End Sub

#End Region

#Region "Public Functions"

    Public Function EvaluateExpression(ByVal StrExpression As String) As Double

        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(mPage)
        Dim StrSqlCommand As String

        Dim dFisicalStartDate As Date = Nothing
        Dim dFisicalEndDate As Date = Nothing

        Dim IntFiscalPeriodId As Integer
        Dim DblValue As Double

        ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")

        If ClsFisicalYearsPeriods.ID <= 0 Then
            Return 0
        End If

        dFisicalStartDate = ClsFisicalYearsPeriods.FromDate
        dFisicalEndDate = ClsFisicalYearsPeriods.ToDate
        IntFiscalPeriodId = ClsFisicalYearsPeriods.FiscalYearID

        Dim ObjStringHandler As New Venus.Shared.StringHandler

        Try

            Select Case StrExpression

                Case "NDPP"
                    StrSqlCommand = " Select Datediff(dd,FromDate,DATEADD(Day,1,ToDate)) From sys_FiscalYearsPeriods " & _
                                    " Where id = " & mFiscalPeriodID
                Case "NHPP"
                    StrSqlCommand = " Select NoOfHoursPerPeriod From hrs_Employees Inner Join hrs_Contracts " & _
                                    " On hrs_Contracts.EmployeeID = hrs_Employees.id Inner Join hrs_EmployeesClasses " & _
                                    " On hrs_Contracts.EmployeeClassid = hrs_employeesClasses.id  " & _
                                    " Where GetDate() between hrs_Contracts.startDate and hrs_Contracts.EndDate"

                Case "NPPY"
                    StrSqlCommand = " Select Count(*) From sys_FiscalYearsPeriods where FiscalYearID = " & IntFiscalPeriodId

                Case Else
                    Return 0
            End Select

            DblValue = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
            Return CDbl(DblValue)

        Catch ex As Exception
            Return 0
        End Try


    End Function


    Public Function EvaluateExpressionDates(ByVal StrExpression As String) As Date
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(mPage)
        Dim ClsFisicalYears As New Clssys_FiscalYears(mPage)
        Dim StrSqlCommand As String
        Dim dFisicalStartDate As Date = Nothing
        Dim dFisicalEndDate As Date = Nothing
        Dim dFisicalStartYearDate As Date = Nothing
        Dim dFisicalEndYearDate As Date = Nothing
        Dim IntFiscalPeriodId As Integer
        Dim dValue As Date

        dFisicalEndDate = ClsFisicalYearsPeriods.ToDate
        Try
            Select Case StrExpression

                Case "FDOP"
                    ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")
                    If ClsFisicalYearsPeriods.ID <= 0 Then
                        Return Nothing
                    Else
                        Return ClsFisicalYearsPeriods.FromDate
                    End If

                    Return dFisicalStartDate
                Case "LDOP"
                    ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")
                    If ClsFisicalYearsPeriods.ID <= 0 Then
                        Return Nothing
                    Else
                        Return ClsFisicalYearsPeriods.ToDate
                    End If
                Case "FDOY"
                    ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")
                    If ClsFisicalYearsPeriods.ID <= 0 Then
                        Return Nothing
                    Else
                        IntFiscalPeriodId = ClsFisicalYearsPeriods.FiscalYearID
                        ClsFisicalYearsPeriods.Find(" FiscalYearID = " & IntFiscalPeriodId & " Order by FromDate ASC ")
                        dFisicalStartYearDate = ClsFisicalYearsPeriods.FromDate
                    End If

                Case "LDOY"
                    ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")
                    If ClsFisicalYearsPeriods.ID <= 0 Then
                        Return Nothing
                    Else
                        IntFiscalPeriodId = ClsFisicalYearsPeriods.FiscalYearID
                        ClsFisicalYearsPeriods.Find(" FiscalYearID = " & IntFiscalPeriodId & " Order by FromDate Desc ")
                        dFisicalStartYearDate = ClsFisicalYearsPeriods.ToDate
                    End If
                Case "CDate+30"
                    Return Date.Now.AddDays(30)
                Case "CDate+60"
                    Return Date.Now.AddDays(60)
                Case Else
                    Return Date.Now
            End Select

            dValue = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
            Return CDate(dValue)

        Catch ex As Exception
            Return Nothing
        End Try


    End Function


#End Region

#Region "Private Function"

#End Region

End Class

