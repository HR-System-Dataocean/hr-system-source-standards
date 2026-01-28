Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeeClassesBases
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesClasses "
        mInsertParameter = "" &
          "Code," &
          "EngName," &
          "ArbName," &
          "ArbName4S," &
          "NoOfDaysPerPeriod," &
          "WorkHoursPerDay," &
          "NoOfHoursPerWeek," &
          "NoOfHoursPerPeriod," &
          "OvertimeFactor," &
          "HolidayFactor," &
          "FirstDayOfWeek," &
          "DefultStartTime," &
          "DefultEndTime," &
          "WorkingUnitsIsHours," &
          "DefaultProjectID," &
          "CompanyID," &
          "Remarks," &
          "RegUserID," &
          "RegComputerID," &
          "NonPermiLatTransaction," &
          "PerDailyDelaying," &
          "PerMonthlyDelaying," &
          "NonProfitOverTimeH," &
          "EOBFormula," &
          "OvertimeFormula," &
          "HolidayFormula," &
          "OvertimeTransaction," &
          "HOvertimeTransaction," &
          "PolicyCheckMachine," &
          "HasAttendance," &
          "PunishementCalc," &
          "OnNoExit," &
          "DeductionMethod," &
          "MaxLoanAmtPCT," &
          "MinServiceMonth," &
          "MaxInstallementPCT," &
          "EOSCostingTrns," &
          "TicketsCostingTrns," &
          "VacCostingTrns," &
          "HICostingTrns," &
          "AbsentFormula," &
          "LateFormula," &
          "VacCostFormula," &
          "HasFingerPrint," &
          "HasFlexableFingerPrint," &
          "HasOvertimeList," &
          "AttendanceFromTimeSheet," &
           "AdvanceBalance," &
           "AccumulatedBalance," &
           "VacationTrans," &
           "VactionTransType," &
           "TransValue," &
           "AddBalanceInAddEmp"

        mInsertParameterValues = "" &
          " @Code," &
          " @EngName," &
          " @ArbName," &
          " @ArbName4S," &
          " @NoOfDaysPerPeriod," &
          " @WorkHoursPerDay," &
          " @NoOfHoursPerWeek," &
          " @NoOfHoursPerPeriod," &
          " @OvertimeFactor," &
          " @HolidayFactor," &
          " @FirstDayOfWeek," &
          " @DefultStartTime," &
          " @DefultEndTime," &
          " @WorkingUnitsIsHours," &
          " @DefaultProjectID," &
          " @CompanyID," &
          " @Remarks," &
          " @RegUserID," &
          " @RegComputerID," &
          " @NonPermiLatTransaction," &
          " @PerDailyDelaying," &
          " @PerMonthlyDelaying," &
          " @NonProfitOverTimeH," &
          " @EOBFormula," &
          " @OvertimeFormula," &
          " @HolidayFormula," &
          " @OvertimeTransaction," &
          " @HOvertimeTransaction," &
          " @PolicyCheckMachine," &
          " @HasAttendance," &
          " @PunishementCalc," &
          " @OnNoExit," &
          " @DeductionMethod," &
          " @MaxLoanAmtPCT," &
          " @MinServiceMonth," &
          " @MaxInstallementPCT," &
          " @EOSCostingTrns," &
          " @TicketsCostingTrns," &
          " @VacCostingTrns," &
          " @HICostingTrns," &
          " @AbsentFormula," &
          " @LateFormula," &
          " @VacCostFormula," &
          " @HasFingerPrint," &
          "@HasFlexableFingerPrint," &
          " @HasOvertimeList," &
          " @AttendanceFromTimeSheet," &
          " @TravalTrans," &
          " @AdvanceBalance," &
          " @AccumulatedBalance," &
           " @VacationTrans," &
           " @VactionTransType," &
           " @TransValue," &
           " @AddBalanceInAddEmp"
        mUpdateParameter = "" &
          "Code=@Code," &
          "EngName=@EngName," &
          "ArbName=@ArbName," &
          "ArbName4S=@ArbName4S," &
          "NoOfDaysPerPeriod=@NoOfDaysPerPeriod," &
          "WorkHoursPerDay=@WorkHoursPerDay," &
          "NoOfHoursPerWeek=@NoOfHoursPerWeek," &
          "NoOfHoursPerPeriod=@NoOfHoursPerPeriod," &
          "OvertimeFactor=@OvertimeFactor," &
          "HolidayFactor=@HolidayFactor," &
          "FirstDayOfWeek=@FirstDayOfWeek," &
          "DefultStartTime=@DefultStartTime," &
          "DefultEndTime=@DefultEndTime," &
          "WorkingUnitsIsHours=@WorkingUnitsIsHours," &
          "DefaultProjectID=@DefaultProjectID," &
          "Remarks=@Remarks," &
          "NonPermiLatTransaction=@NonPermiLatTransaction," &
          "PerDailyDelaying=@PerDailyDelaying," &
          "PerMonthlyDelaying=@PerMonthlyDelaying," &
          "NonProfitOverTimeH=@NonProfitOverTimeH," &
          "EOBFormula=@EOBFormula," &
          "OvertimeFormula=@OvertimeFormula," &
          "HolidayFormula=@HolidayFormula," &
          "OvertimeTransaction=@OvertimeTransaction," &
          "HOvertimeTransaction=@HOvertimeTransaction," &
          "PolicyCheckMachine=@PolicyCheckMachine," &
          "HasAttendance=@HasAttendance," &
          "PunishementCalc=@PunishementCalc," &
          "OnNoExit=@OnNoExit," &
          "DeductionMethod=@DeductionMethod," &
          "MaxLoanAmtPCT=@MaxLoanAmtPCT," &
          "MinServiceMonth=@MinServiceMonth," &
          "MaxInstallementPCT=@MaxInstallementPCT," &
          "EOSCostingTrns=@EOSCostingTrns," &
          "TicketsCostingTrns=@TicketsCostingTrns," &
          "RegComputerID=@RegComputerID," &
          "VacCostingTrns=@VacCostingTrns," &
          "HICostingTrns=@HICostingTrns," &
          "AbsentFormula=@AbsentFormula," &
          "LateFormula=@LateFormula," &
          "VacCostFormula=@VacCostFormula," &
          "HasFingerPrint=@HasFingerPrint," &
          "HasFlexableFingerPrint=@HasFlexableFingerPrint," &
          "HasOvertimeList=@HasOvertimeList," &
          "AttendanceFromTimeSheet=@AttendanceFromTimeSheet," &
          "TravalTrans=@TravalTrans," &
          "AdvanceBalance=@AdvanceBalance," &
          "AccumulatedBalance=@AccumulatedBalance," &
          "VacationTrans=@VacationTrans," &
          "VactionTransType=@VactionTransType," &
          "TransValue=@TransValue," &
          "AddBalanceInAddEmp=@AddBalanceInAddEmp"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region

#Region "Private Members"
    Private mID As Integer
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mNoOfDaysPerPeriod As Object
    Private mWorkHoursPerDay As Object
    Private mNoOfHoursPerWeek As Integer
    Private mNoOfHoursPerPeriod As Integer
    Private mOvertimeFactor As Object
    Private mHolidayFactor As Object
    Private mFirstDayOfWeek As Object
    Private mDefultStartTime As DateTime
    Private mDefultEndTime As DateTime
    Private mWorkingUnitsIsHours As Boolean
    Private mDefaultProjectID As Integer
    Private mCompanyID As Integer
    Private mRemarks As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As DateTime
    Private mCancelDate As DateTime
    Private mNonPermiLatTransaction As Integer
    Private mPerDailyDelaying As Integer
    Private mPerMonthlyDelaying As Integer
    Private mNonProfitOverTimeH As Integer
    Private mEOBFormula As String
    Private mOvertimeFormula As String
    Private mHolidayFormula As String
    Private mOvertimeTransaction As Integer
    Private mHOvertimeTransaction As Integer
    Private mPolicyCheckMachine As Boolean
    Private mHasAttendance As Boolean
    Private mPunishementCalc As Integer
    Private mOnNoExit As Integer
    Private mDeductionMethod As Integer
    Private mMaxLoanAmtPCT As Integer
    Private mMinServiceMonth As Integer
    Private mMaxInstallementPCT As Integer
    Private mEOSCostingTrns As Integer
    Private mTicketsCostingTrns As Integer
    Private mVacCostingTrns As Integer
    Private mHICostingTrns As Integer
    Private mTravalTrans As Integer

    Private mAdvanceBalance As Boolean
    Private mAccumulatedBalance As Boolean
    Private mVacationTrans As Boolean
    Private mVactionTransType As Integer
    Private mTransValue As Integer
    Private mAddBalanceInAddEmp As Boolean

    Private mAbsentFormula As String
    Private mLateFormula As String
    Private mVacCostFormula As String
    Private mHasFingerPrint As Boolean
    Private mHasFlexableFingerPrint As Boolean
    Private mHasOvertimeList As Boolean
    Private mAttendanceFromTimeSheet As Boolean

#End Region

#Region "Public property"

    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal Value As Integer)
            mID = Value
        End Set
    End Property
    Public Property Code() As String
        Get
            Return mCode
        End Get
        Set(ByVal Value As String)
            mCode = Value
        End Set
    End Property
    Public Property EngName() As String
        Get
            Return mEngName
        End Get
        Set(ByVal Value As String)
            mEngName = Value
        End Set
    End Property
    Public Property ArbName() As String
        Get
            Return mArbName
        End Get
        Set(ByVal Value As String)
            mArbName = Value
        End Set
    End Property
    Public Property ArbName4S() As String
        Get
            Return mArbName4S
        End Get
        Set(ByVal Value As String)
            mArbName4S = Value
        End Set
    End Property
    Public Property NoOfDaysPerPeriod() As Object
        Get
            Return mNoOfDaysPerPeriod
        End Get
        Set(ByVal Value As Object)
            mNoOfDaysPerPeriod = Value
        End Set
    End Property
    Public Property WorkHoursPerDay() As Object
        Get
            Return mWorkHoursPerDay
        End Get
        Set(ByVal Value As Object)
            mWorkHoursPerDay = Value
        End Set
    End Property
    Public Property NoOfHoursPerWeek() As Integer
        Get
            Return mNoOfHoursPerWeek
        End Get
        Set(ByVal Value As Integer)
            mNoOfHoursPerWeek = Value
        End Set
    End Property
    Public Property NoOfHoursPerPeriod() As Integer
        Get
            Return mNoOfHoursPerPeriod
        End Get
        Set(ByVal Value As Integer)
            mNoOfHoursPerPeriod = Value
        End Set
    End Property
    Public Property OvertimeFactor() As Object
        Get
            Return mOvertimeFactor
        End Get
        Set(ByVal Value As Object)
            mOvertimeFactor = Value
        End Set
    End Property
    Public Property HolidayFactor() As Object
        Get
            Return mHolidayFactor
        End Get
        Set(ByVal Value As Object)
            mHolidayFactor = Value
        End Set
    End Property
    Public Property FirstDayOfWeek() As Object
        Get
            Return mFirstDayOfWeek
        End Get
        Set(ByVal Value As Object)
            mFirstDayOfWeek = Value
        End Set
    End Property
    Public Property DefultStartTime() As DateTime
        Get
            Return mDefultStartTime
        End Get
        Set(ByVal Value As DateTime)
            mDefultStartTime = Value
        End Set
    End Property
    Public Property DefultEndTime() As DateTime
        Get
            Return mDefultEndTime
        End Get
        Set(ByVal Value As DateTime)
            mDefultEndTime = Value
        End Set
    End Property
    Public Property WorkingUnitsIsHours() As Boolean
        Get
            Return mWorkingUnitsIsHours
        End Get
        Set(ByVal Value As Boolean)
            mWorkingUnitsIsHours = Value
        End Set
    End Property
    Public Property DefaultProjectID() As Integer
        Get
            Return mDefaultProjectID
        End Get
        Set(ByVal Value As Integer)
            mDefaultProjectID = Value
        End Set
    End Property
    Public Property CompanyID() As Integer
        Get
            Return mCompanyID
        End Get
        Set(ByVal Value As Integer)
            mCompanyID = Value
        End Set
    End Property
    Public Property Remarks() As String
        Get
            Return mRemarks
        End Get
        Set(ByVal Value As String)
            mRemarks = Value
        End Set
    End Property
    Public Property RegUserID() As Integer
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Integer)
            mRegUserID = Value
        End Set
    End Property
    Public Property RegComputerID() As Integer
        Get
            Return mRegComputerID
        End Get
        Set(ByVal Value As Integer)
            mRegComputerID = Value
        End Set
    End Property
    Public Property RegDate() As DateTime
        Get
            Return mRegDate
        End Get
        Set(ByVal Value As DateTime)
            mRegDate = Value
        End Set
    End Property
    Public Property CancelDate() As DateTime
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As DateTime)
            mCancelDate = Value
        End Set
    End Property
    Public Property NonPermiLatTransaction() As Integer
        Get
            Return mNonPermiLatTransaction
        End Get
        Set(ByVal Value As Integer)
            mNonPermiLatTransaction = Value
        End Set
    End Property
    Public Property PerDailyDelaying() As Integer
        Get
            Return mPerDailyDelaying
        End Get
        Set(ByVal Value As Integer)
            mPerDailyDelaying = Value
        End Set
    End Property
    Public Property PerMonthlyDelaying() As Integer
        Get
            Return mPerMonthlyDelaying
        End Get
        Set(ByVal Value As Integer)
            mPerMonthlyDelaying = Value
        End Set
    End Property
    Public Property NonProfitOverTimeH() As Integer
        Get
            Return mNonProfitOverTimeH
        End Get
        Set(ByVal Value As Integer)
            mNonProfitOverTimeH = Value
        End Set
    End Property
    Public Property EOBFormula() As String
        Get
            Return mEOBFormula
        End Get
        Set(ByVal Value As String)
            mEOBFormula = Value
        End Set
    End Property
    Public Property OvertimeFormula() As String
        Get
            Return mOvertimeFormula
        End Get
        Set(ByVal Value As String)
            mOvertimeFormula = Value
        End Set
    End Property
    Public Property HolidayFormula() As String
        Get
            Return mHolidayFormula
        End Get
        Set(ByVal Value As String)
            mHolidayFormula = Value
        End Set
    End Property
    Public Property OvertimeTransaction() As Integer
        Get
            Return mOvertimeTransaction
        End Get
        Set(ByVal Value As Integer)
            mOvertimeTransaction = Value
        End Set
    End Property
    Public Property HOvertimeTransaction() As Integer
        Get
            Return mHOvertimeTransaction
        End Get
        Set(ByVal Value As Integer)
            mHOvertimeTransaction = Value
        End Set
    End Property
    Public Property PolicyCheckMachine() As Boolean
        Get
            Return mPolicyCheckMachine
        End Get
        Set(ByVal Value As Boolean)
            mPolicyCheckMachine = Value
        End Set
    End Property
    Public Property HasAttendance() As Boolean
        Get
            Return mHasAttendance
        End Get
        Set(ByVal Value As Boolean)
            mHasAttendance = Value
        End Set
    End Property
    Public Property PunishementCalc() As Integer
        Get
            Return mPunishementCalc
        End Get
        Set(ByVal Value As Integer)
            mPunishementCalc = Value
        End Set
    End Property
    Public Property OnNoExit() As Integer
        Get
            Return mOnNoExit
        End Get
        Set(ByVal Value As Integer)
            mOnNoExit = Value
        End Set
    End Property
    Public Property DeductionMethod() As Integer
        Get
            Return mDeductionMethod
        End Get
        Set(ByVal Value As Integer)
            mDeductionMethod = Value
        End Set
    End Property
    Public Property MaxLoanAmtPCT() As Integer
        Get
            Return mMaxLoanAmtPCT
        End Get
        Set(ByVal Value As Integer)
            mMaxLoanAmtPCT = Value
        End Set
    End Property
    Public Property MinServiceMonth() As Integer
        Get
            Return mMinServiceMonth
        End Get
        Set(ByVal Value As Integer)
            mMinServiceMonth = Value
        End Set
    End Property
    Public Property MaxInstallementPCT() As Integer
        Get
            Return mMaxInstallementPCT
        End Get
        Set(ByVal Value As Integer)
            mMaxInstallementPCT = Value
        End Set
    End Property
    Public Property EOSCostingTrns() As Integer
        Get
            Return mEOSCostingTrns
        End Get
        Set(ByVal Value As Integer)
            mEOSCostingTrns = Value
        End Set
    End Property
    Public Property TicketsCostingTrns() As Integer
        Get
            Return mTicketsCostingTrns
        End Get
        Set(ByVal Value As Integer)
            mTicketsCostingTrns = Value
        End Set
    End Property
    Public Property VacCostingTrns() As Integer
        Get
            Return mVacCostingTrns
        End Get
        Set(ByVal Value As Integer)
            mVacCostingTrns = Value
        End Set
    End Property
    Public Property HICostingTrns() As Integer
        Get
            Return mHICostingTrns
        End Get
        Set(ByVal Value As Integer)
            mHICostingTrns = Value
        End Set
    End Property
    Public Property TravalTrans() As Integer
        Get
            Return mTravalTrans
        End Get
        Set(ByVal Value As Integer)
            mTravalTrans = Value
        End Set
    End Property

    Public Property AdvanceBalance() As Boolean
        Get
            Return mAdvanceBalance
        End Get
        Set(ByVal Value As Boolean)
            mAdvanceBalance = Value
        End Set
    End Property
    Public Property AccumulatedBalance() As Boolean
        Get
            Return mAccumulatedBalance
        End Get
        Set(ByVal Value As Boolean)
            mAccumulatedBalance = Value
        End Set
    End Property

    Public Property VacationTrans() As Boolean
        Get
            Return mVacationTrans
        End Get
        Set(ByVal Value As Boolean)
            mVacationTrans = Value
        End Set
    End Property

    Public Property VactionTransType() As Integer
        Get
            Return mVactionTransType
        End Get
        Set(ByVal Value As Integer)
            mVactionTransType = Value
        End Set
    End Property

    Public Property TransValue() As Integer
        Get
            Return mTransValue
        End Get
        Set(ByVal Value As Integer)
            mTransValue = Value
        End Set
    End Property

    Public Property AddBalanceInAddEmp() As Boolean
        Get
            Return mAddBalanceInAddEmp
        End Get
        Set(ByVal Value As Boolean)
            mAddBalanceInAddEmp = Value
        End Set
    End Property
    Public Property AbsentFormula() As String
        Get
            Return mAbsentFormula
        End Get
        Set(ByVal Value As String)
            mAbsentFormula = Value
        End Set
    End Property
    Public Property LateFormula() As String
        Get
            Return mLateFormula
        End Get
        Set(ByVal Value As String)
            mLateFormula = Value
        End Set
    End Property
    Public Property VacCostFormula() As String
        Get
            Return mVacCostFormula
        End Get
        Set(ByVal Value As String)
            mVacCostFormula = Value
        End Set
    End Property
    Public Property HasFingerPrint() As Boolean
        Get
            Return mHasFingerPrint
        End Get
        Set(ByVal Value As Boolean)
            mHasFingerPrint = Value
        End Set
    End Property
    Public Property HasflexableFingerPrint() As Boolean
        Get
            Return mHasFlexableFingerPrint
        End Get
        Set(ByVal Value As Boolean)
            mHasFlexableFingerPrint = Value
        End Set
    End Property
    Public Property HasOvertimeList() As Boolean
        Get
            Return mHasOvertimeList
        End Get
        Set(ByVal Value As Boolean)
            mHasOvertimeList = Value
        End Set
    End Property
    Public Property AttendanceFromTimeSheet() As Boolean
        Get
            Return mAttendanceFromTimeSheet
        End Get
        Set(ByVal Value As Boolean)
            mAttendanceFromTimeSheet = Value
        End Set
    End Property

#End Region

#Region "Public Function"

    '==================================================================
    'Created by : [0258]
    'Date : 15/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from hrs_EmployeesClasses table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If
            Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
            '==================== Order By Modification [ End ]
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    '==================================================================
    'Created by : [0258]
    'Date : 15/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from hrs_EmployeesClasses where filter 
    '       2-Check if ID > 0 this mean that row is already exist in hrs_EmployeesClasses  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in hrs_EmployeesClasses  table
    '==================================================================

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_EmployeesClasses Where " & Filter
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = strSQL
            mSqlCommand.Connection.Open()
            Value = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            If Value > 0 Then
                Update(Filter)
            Else
                Save()
            End If
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(strSQL, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 15/07/2007

    'Description: Save New Row in hrs_EmployeesClasses  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in hrs_EmployeesClasses  table

    '==================================================================

    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, OperationType.Save)
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
    'Created by : [0258]
    'Date : 15/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in hrs_EmployeesClasses  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in hrs_EmployeesClasses  table

    '==================================================================

    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String = String.Empty
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()

        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, OperationType.Update)
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")

            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 15/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in hrs_EmployeesClasses  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in hrs_EmployeesClasses  table

    '==================================================================

    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand, OperationType.Update)
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
    'Created by : [0258]
    'Date : 15/07/2007
    'Description: Clear all private members  of the class

    '==================================================================

    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mNoOfDaysPerPeriod = Nothing
            mWorkHoursPerDay = Nothing
            mNoOfHoursPerWeek = 0
            mNoOfHoursPerPeriod = 0
            mOvertimeFactor = Nothing
            mHolidayFactor = Nothing
            mFirstDayOfWeek = Nothing
            mDefultStartTime = Nothing
            mDefultEndTime = Nothing
            mWorkingUnitsIsHours = False
            mDefaultProjectID = 0
            mCompanyID = 0
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mNonPermiLatTransaction = 0
            mPerDailyDelaying = 0
            mPerMonthlyDelaying = 0
            mNonProfitOverTimeH = 0
            mEOBFormula = String.Empty
            mOvertimeFormula = String.Empty
            mHolidayFormula = String.Empty
            mOvertimeTransaction = 0
            mHOvertimeTransaction = 0
            mPolicyCheckMachine = False
            mHasAttendance = False
            mPunishementCalc = 0
            mOnNoExit = 0
            mDeductionMethod = 0
            mMaxLoanAmtPCT = 0
            mMinServiceMonth = 0
            mMaxInstallementPCT = 0
            mEOSCostingTrns = 0
            mTicketsCostingTrns = 0
            mVacCostingTrns = 0
            mHICostingTrns = 0
            mTravalTrans = 0

            mAdvanceBalance = False
            mAccumulatedBalance = False
            mVacationTrans = False
            mVactionTransType = 0
            mTransValue = 0
            mAddBalanceInAddEmp = False

            mAbsentFormula = String.Empty
            mLateFormula = String.Empty
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 15/07/2007
    'Description:  find first row in hrs_EmployeesClasses table
    'Steps: 
    '       1-execute sqlstatment to find first row in hrs_EmployeesClasses  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code ASC"
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
    'Created by : [0258]
    'Date : 15/07/2007
    'Description:  find Last row in hrs_EmployeesClasses  table
    'Steps: 
    '       1-execute sqlstatment to find last row in hrs_EmployeesClasses  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '====================================================================

    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code DESC"
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
    'Created by : [0258]
    'Date : 15/07/2007
    'Description:  find Next row in hrs_EmployeesClasses  table
    'Steps: 
    '       1-execute sqlstatment to find Next row in hrs_EmployeesClasses  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code ASC"
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
    'Created by : [0258]
    'Date : 15/07/2007
    'Description:  find previous row in hrs_EmployeesClasses  table
    'Steps: 
    '       1-execute sqlstatment to find previous row in hrs_EmployeesClasses  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code < '" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code DESC"
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
    'Created by : [0258]
    'Date : 15/07/2007
    'Description: execute stored procedure to get vacation types (RequiredExperience , Duration of each vacation) for each employeeclass

    '==================================================================

    'Public Function GetEmployeeClassVacation(ByVal EmployeeClassid As Integer, ByRef ObjDs As DataSet) As Boolean
    '    Dim StrSelectCommand As String = String.Empty
    '    Try
    '        ObjDs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, "hrs_GetEmployeeClassVacations", EmployeeClassid)
    '        If mDataHandler.CheckValidDataObject(ObjDs) Then
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        mPage.Session.Add("ErrorValue", ex)
    '        mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
    '        mPage.Response.Redirect("ErrorPage.aspx")
    '    End Try
    'End Function

    '==================================================================
    'Created by : [0258]
    'Date : 15/07/2007
    'Description: execute sqlstatement to insert into hrs_EmployeesClassesVacations table (DatawebGrid Control) 
    '       Insert into hrs_EmployeesClassesVacation table RequiredExperience,Duration for all vacationtypes of Current EmployeeClass
    '==================================================================

    Public Function SetEmployeeClassVacation(ByVal EmployeeClassId As Integer, ByVal ObjGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid) As Boolean

        Dim StrSelectCommand As String = String.Empty
        Dim ObjDataRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Try
            StrSelectCommand = " Delete From hrs_EmployeesClassesVacations Where EmployeeClassid = " & EmployeeClassId & ";" & vbNewLine
            For Each ObjDataRow In ObjGrid.Rows
                If (Not IsNothing(ObjDataRow.Cells.FromKey("FromMonth").Value)) And (Not IsNothing(ObjDataRow.Cells.FromKey("VacationTypeID").Value)) Then
                    StrSelectCommand &= " Insert Into hrs_EmployeesClassesVacations(vacationtypeid,employeeClassid,FromMonth,ToMonth,DurationDays,RequiredWorkingMonths,TicketsRnd,DependantTicketRnd,MaxKeepDays)" & _
                    " Values(" & ObjDataRow.Cells.FromKey("VacationTypeID").Value & _
                    "," & EmployeeClassId & _
                    "," & IIf(ObjDataRow.Cells.FromKey("FromMonth").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("FromMonth").Value) & _
                    "," & IIf(ObjDataRow.Cells.FromKey("ToMonth").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("ToMonth").Value) & _
                    "," & IIf(ObjDataRow.Cells.FromKey("DurationDays").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("DurationDays").Value) & _
                    "," & IIf(ObjDataRow.Cells.FromKey("RequiredWorkingMonths").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("RequiredWorkingMonths").Value) & _
                    "," & IIf(ObjDataRow.Cells.FromKey("TicketsRnd").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("TicketsRnd").Value) & _
                    "," & IIf(ObjDataRow.Cells.FromKey("DependantTicketRnd").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("DependantTicketRnd").Value) & _
                    "," & IIf(ObjDataRow.Cells.FromKey("MaxKeepDays").Value Is Nothing, "Null", ObjDataRow.Cells.FromKey("MaxKeepDays").Value) & ")" & _
                    ";" & vbNewLine
                End If
            Next
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, StrSelectCommand)
            Return True

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0259]
    'Date : 26/08/2007
    'Description: Getting if there are any difference between DataSet and Class's Properties
    '==================================================================

    Public Function CheckDiff(ByVal ClassObj As Object, ByVal DSData As DataSet, ByVal Filter As String) As Boolean

        Dim myPropertyInfo As Reflection.PropertyInfo() = CType(ClassObj.GetType, Type).GetProperties()
        Dim PropertyCounter As Integer
        Dim DSCounter As Integer
        Dim Index() As Object

        For PropertyCounter = 1 To myPropertyInfo.Length - 1
            For DSCounter = 1 To DSData.Tables(0).Columns.Count - 1

                With DSData.Tables(0).Columns(DSCounter)
                    Dim myPropInfo As Reflection.PropertyInfo = CType(myPropertyInfo(PropertyCounter), Reflection.PropertyInfo)

                    'Check column name (ex: CODE=CODE)
                    If myPropInfo.Name.ToString.ToUpper = .ColumnName.ToUpper Then

                        'Check column value (ex: 002=002)
                        If Not myPropInfo.GetValue(ClassObj, Index) = FixNull(DSData.Tables(0).Rows(0)(DSCounter), DSData.Tables(0).Columns(DSCounter)) Then

                            '-------------------------------0257 MODIFIED-----------------------------------------
                            If (myPropInfo.Name.ToString.ToUpper = "DEFULTSTARTTIME" Or myPropInfo.Name.ToString.ToUpper = "DEFULTENDTIME") Then
                                If Not myPropInfo.GetValue(ClassObj, Index) = CDate(FixNull(DSData.Tables(0).Rows(0)(DSCounter), DSData.Tables(0).Columns(DSCounter))).ToShortTimeString Then
                                    Return True
                                Else
                                    Exit For
                                End If
                            End If
                            '-------------------------------=============-----------------------------------------

                            Return True

                            Exit For
                        End If

                        Exit For
                    End If

                End With

            Next
        Next

    End Function
#End Region

#Region "Class Private Function"

    '===================================================================
    'Created by : [0258]
    'Date : 15/07/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class

    '===================================================================

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Try
            With Ds.Tables(0).Rows(0)
                mID = [Shared].DataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = [Shared].DataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = [Shared].DataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = [Shared].DataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = [Shared].DataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mNoOfDaysPerPeriod = [Shared].DataHandler.DataValue_Out(.Item("NoOfDaysPerPeriod"), SqlDbType.TinyInt)
                mWorkHoursPerDay = [Shared].DataHandler.DataValue_Out(.Item("WorkHoursPerDay"), SqlDbType.Real)
                mNoOfHoursPerWeek = [Shared].DataHandler.DataValue_Out(.Item("NoOfHoursPerWeek"), SqlDbType.Int, True)
                mNoOfHoursPerPeriod = [Shared].DataHandler.DataValue_Out(.Item("NoOfHoursPerPeriod"), SqlDbType.Int, True)
                mOvertimeFactor = [Shared].DataHandler.DataValue_Out(.Item("OvertimeFactor"), SqlDbType.Real)
                mHolidayFactor = [Shared].DataHandler.DataValue_Out(.Item("HolidayFactor"), SqlDbType.Real)
                mFirstDayOfWeek = [Shared].DataHandler.DataValue_Out(.Item("FirstDayOfWeek"), SqlDbType.TinyInt)
                If clsCompanies.IsHigry Then
                    mDefultStartTime = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDateTime(.Item("DefultStartTime"), Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Output), SqlDbType.DateTime)
                    mDefultEndTime = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDateTime(.Item("DefultEndTime"), Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Output), SqlDbType.DateTime)
                Else
                    mDefultStartTime = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDateTime(.Item("DefultStartTime"), Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Output), SqlDbType.DateTime)
                    mDefultEndTime = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDateTime(.Item("DefultEndTime"), Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Output), SqlDbType.DateTime)
                End If
                mWorkingUnitsIsHours = [Shared].DataHandler.DataValue_Out(.Item("WorkingUnitsIsHours"), SqlDbType.Bit)
                mDefaultProjectID = [Shared].DataHandler.DataValue_Out(.Item("DefaultProjectID"), SqlDbType.Int, True)
                mCompanyID = [Shared].DataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int, True)
                mRemarks = [Shared].DataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mNonPermiLatTransaction = [Shared].DataHandler.DataValue_Out(.Item("NonPermiLatTransaction"), SqlDbType.Int, True)
                mPerDailyDelaying = [Shared].DataHandler.DataValue_Out(.Item("PerDailyDelaying"), SqlDbType.Int, True)
                mPerMonthlyDelaying = [Shared].DataHandler.DataValue_Out(.Item("PerMonthlyDelaying"), SqlDbType.Int, True)
                mNonProfitOverTimeH = [Shared].DataHandler.DataValue_Out(.Item("NonProfitOverTimeH"), SqlDbType.Int, True)
                mEOBFormula = [Shared].DataHandler.DataValue_Out(.Item("EOBFormula"), SqlDbType.VarChar)
                mOvertimeFormula = [Shared].DataHandler.DataValue_Out(.Item("OvertimeFormula"), SqlDbType.VarChar)
                mHolidayFormula = [Shared].DataHandler.DataValue_Out(.Item("HolidayFormula"), SqlDbType.VarChar)
                mOvertimeTransaction = [Shared].DataHandler.DataValue_Out(.Item("OvertimeTransaction"), SqlDbType.Int, True)
                mHOvertimeTransaction = [Shared].DataHandler.DataValue_Out(.Item("HOvertimeTransaction"), SqlDbType.Int, True)
                mPolicyCheckMachine = [Shared].DataHandler.DataValue_Out(.Item("PolicyCheckMachine"), SqlDbType.Bit)
                mHasAttendance = [Shared].DataHandler.DataValue_Out(.Item("HasAttendance"), SqlDbType.Bit)
                mPunishementCalc = [Shared].DataHandler.DataValue_Out(.Item("PunishementCalc"), SqlDbType.Int, True)
                mOnNoExit = [Shared].DataHandler.DataValue_Out(.Item("OnNoExit"), SqlDbType.Int, True)
                mDeductionMethod = [Shared].DataHandler.DataValue_Out(.Item("DeductionMethod"), SqlDbType.Int, True)
                mMaxLoanAmtPCT = [Shared].DataHandler.DataValue_Out(.Item("MaxLoanAmtPCT"), SqlDbType.Int, True)
                mMinServiceMonth = [Shared].DataHandler.DataValue_Out(.Item("MinServiceMonth"), SqlDbType.Int, True)
                mMaxInstallementPCT = [Shared].DataHandler.DataValue_Out(.Item("MaxInstallementPCT"), SqlDbType.Int, True)
                mEOSCostingTrns = [Shared].DataHandler.DataValue_Out(.Item("EOSCostingTrns"), SqlDbType.Int, True)
                mTicketsCostingTrns = [Shared].DataHandler.DataValue_Out(.Item("TicketsCostingTrns"), SqlDbType.Int, True)
                mVacCostingTrns = [Shared].DataHandler.DataValue_Out(.Item("VacCostingTrns"), SqlDbType.Int, True)
                mHICostingTrns = [Shared].DataHandler.DataValue_Out(.Item("HICostingTrns"), SqlDbType.Int, True)
                mTravalTrans = [Shared].DataHandler.DataValue_Out(.Item("TravalTrans"), SqlDbType.Int, True)

                mAdvanceBalance = [Shared].DataHandler.DataValue_Out(.Item("AdvanceBalance"), SqlDbType.Bit)
                mAccumulatedBalance = [Shared].DataHandler.DataValue_Out(.Item("AccumulatedBalance"), SqlDbType.Bit)
                mVacationTrans = [Shared].DataHandler.DataValue_Out(.Item("VacationTrans"), SqlDbType.Bit)
                mVactionTransType = [Shared].DataHandler.DataValue_Out(.Item("VactionTransType"), SqlDbType.Int)
                mTransValue = [Shared].DataHandler.DataValue_Out(.Item("TransValue"), SqlDbType.Int)
                mAddBalanceInAddEmp = [Shared].DataHandler.DataValue_Out(.Item("AddBalanceInAddEmp"), SqlDbType.Bit)

                mAbsentFormula = [Shared].DataHandler.DataValue_Out(.Item("AbsentFormula"), SqlDbType.VarChar)
                mLateFormula = [Shared].DataHandler.DataValue_Out(.Item("LateFormula"), SqlDbType.VarChar)
                mVacCostFormula = [Shared].DataHandler.DataValue_Out(.Item("VacCostFormula"), SqlDbType.VarChar)
                mHasFingerPrint = [Shared].DataHandler.DataValue_Out(.Item("HasFingerPrint"), SqlDbType.Bit)
                mHasFlexableFingerPrint = [Shared].DataHandler.DataValue_Out(.Item("HasFlexableFingerPrint"), SqlDbType.Bit)
                mHasOvertimeList = [Shared].DataHandler.DataValue_Out(.Item("HasOvertimeList"), SqlDbType.Bit)
                mAttendanceFromTimeSheet = [Shared].DataHandler.DataValue_Out(.Item("AttendanceFromTimeSheet"), SqlDbType.Bit)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '===================================================================
    'Created by : [0258]
    'Date : 15/07/2007
    'Description:   Make the values of parameter equal values of private member  of the class

    '===================================================================

    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal operationtype As OperationType) As Boolean
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NoOfDaysPerPeriod", SqlDbType.TinyInt)).Value = [Shared].DataHandler.DataValue_In(mNoOfDaysPerPeriod, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@WorkHoursPerDay", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mWorkHoursPerDay, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NoOfHoursPerWeek", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mNoOfHoursPerWeek, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NoOfHoursPerPeriod", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mNoOfHoursPerPeriod, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OvertimeFactor", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mOvertimeFactor, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HolidayFactor", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mHolidayFactor, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FirstDayOfWeek", SqlDbType.TinyInt)).Value = [Shared].DataHandler.DataValue_In(mFirstDayOfWeek, SqlDbType.TinyInt)
            If clsCompanies.IsHigry Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefultStartTime", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDateTime(mDefultStartTime, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefultEndTime", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDateTime(mDefultEndTime, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
            Else
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefultStartTime", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDateTime(mDefultStartTime, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefultEndTime", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(ClsGHCalender.GetRelativeDateTime(mDefultEndTime, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input), SqlDbType.DateTime)
            End If
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@WorkingUnitsIsHours", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mWorkingUnitsIsHours, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefaultProjectID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mDefaultProjectID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NonPermiLatTransaction", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mNonPermiLatTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PerDailyDelaying", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPerDailyDelaying, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PerMonthlyDelaying", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPerMonthlyDelaying, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NonProfitOverTimeH", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mNonProfitOverTimeH, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EOBFormula", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mEOBFormula, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OvertimeFormula", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mOvertimeFormula, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HolidayFormula", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mHolidayFormula, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OvertimeTransaction", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mOvertimeTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HOvertimeTransaction", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mHOvertimeTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PolicyCheckMachine", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mPolicyCheckMachine, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HasAttendance", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mHasAttendance, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PunishementCalc", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPunishementCalc, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OnNoExit", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mOnNoExit, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DeductionMethod", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mDeductionMethod, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaxLoanAmtPCT", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mMaxLoanAmtPCT, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MinServiceMonth", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mMinServiceMonth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaxInstallementPCT", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mMaxInstallementPCT, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EOSCostingTrns", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mEOSCostingTrns, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TicketsCostingTrns", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mTicketsCostingTrns, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VacCostingTrns", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mVacCostingTrns, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HICostingTrns", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mHICostingTrns, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TravalTrans", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mTravalTrans, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AdvanceBalance", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mAdvanceBalance, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AccumulatedBalance", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mAccumulatedBalance, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VacationTrans", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mVacationTrans, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VactionTransType", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mVactionTransType, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransValue", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mTransValue, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AddBalanceInAddEmp", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mAddBalanceInAddEmp, SqlDbType.Bit)


            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AbsentFormula", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mAbsentFormula, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LateFormula", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mLateFormula, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VacCostFormula", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mVacCostFormula, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HasFingerPrint", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mHasFingerPrint, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HasFlexableFingerPrint", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mHasFlexableFingerPrint, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HasOvertimeList", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mHasOvertimeList, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AttendanceFromTimeSheet", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mAttendanceFromTimeSheet, SqlDbType.Bit)
            Select Case operationtype
                Case ClsDataAcessLayer.OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
            End Select
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function FixNull(ByVal obj As Object, ByVal DataColumn As Global.System.Data.DataColumn) As Object
        Try

            Select Case DataColumn.DataType.Name.ToUpper
                Case "DECIMAL", "DOUBLE"
                    If obj Is DBNull.Value Then
                        Return 0.0
                    Else
                        Return obj
                    End If
                Case "INT32"
                    If obj Is DBNull.Value Then
                        Return 0
                    Else
                        Return obj
                    End If
                Case "DATETIME"
                    If obj Is DBNull.Value Then
                        Return Nothing
                    Else
                        Return obj
                    End If
                Case "STRING"
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
                Case Else
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
            End Select

        Catch ex As Exception
            Return ""
        End Try
    End Function
#End Region

#Region "Class Destructors"

    Public Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class

Public Class Clshrs_EmployeeClasses
    Inherits Clshrs_EmployeeClassesBases

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
    End Sub
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = Me.mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')=''  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')=''  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")

                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                Item.DataValue = ObjDataRow("ID")
                DdlValues.ValueListItems.Add(Item)
            Next

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 15/07/2007
    'Input : DdlValues as DropDownList
    '       Condition to fill dropdownlist (if Wanted)
    '       NullNode to make attention to user by make first item ="Please Select"    
    'Description:  Fill DropDownList 
    'Steps: 1- Exceute sql statement to get all columns from hrs_EmployeesClasses table where filter parameter(if spcefied)
    '       2- if NullNode is true --then make first text item is  "Please Select" and it's value is 0
    '       3- fill DtatText of Dropdownlist with English Nameand DataValue with ID 
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    '==================================================================

    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String = String.Empty
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = Me.mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')=''  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')=''  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow("ID")
                DdlValues.Items.Add(Item)
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    Public Function GetEmployeeClassVacations(Optional ByVal intEmpClassID As Integer = 0) As Data.DataSet
        Dim objNav As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetEmployeeClassVacations", intEmpClassID, objNav.SetLanguage(mPage, "0/1"))
    End Function

    Public Function GetEmployeeClassDesrvedAnnualVacationsBalance(Optional ByVal intEmpClassID As Integer = 0, Optional ByVal NoOfDays As Integer = 0) As Integer
        Dim objNav As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Dim DesrvedDays As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, "hrs_GetEmployeeClassDesrvedAnnualVacationsBalance", intEmpClassID, NoOfDays)
        Return DesrvedDays
    End Function

    Public Function GetAllVacationTypes() As Data.DataSet
        Dim objNav As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetAllVacationTypes", objNav.SetLanguage(mPage, "0/1"))
    End Function
    Public Function GetEmployeeClassAnnualVacations(Optional ByVal intEmpClassID As Integer = 0) As Data.DataSet
        Dim objNav As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetEmployeeClassAnnualVacations", intEmpClassID, objNav.SetLanguage(mPage, "0/1"))
    End Function
End Class