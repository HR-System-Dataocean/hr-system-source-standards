
'=========================================================================
'Created by : DataOcean
'Date : 06/08/2007
'                   Class: VacationsType
'                   Table: hrs_VacationsTypes
'Developer  : [0259]
'              : B#g0010 [0256] 23-06-2008 Add _Forgin arrgument to data_ValueIn method in SetParameters Function 
'              :                           And _Forgin arrgument to data_value_Out Method in GetParameters Function 
'              :                           To avoid saving non-existing forign key in any table 
'              :                           And convert the DBnull values to zero in case of forign key fields only 
'=========================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_VacationsTypesBase
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    '=====================================================================
    'Created by : DataOcean
    'Date : 05/08/2007
    'Description:   In the constructor of the class set
    '                                       -Table name 
    '                                       -Sqlstatment(s) of (Insert,Update,Delete,select) row(s) dealing with the database
    '=====================================================================

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_VacationsTypes "
        mInsertParameter = " Code,EngName,ArbName,ArbName4S,IsPaid,Sex,IsAnnual,IsSickVacation,AffectEOS,HasPayment,ForSalaryTransaction,Remarks,RegUserID,RegComputerID,CompanyId,OBalanceTransactionID,OverDueVacationID,Stage1PCT,Stage2PCT,Stage3PCT,ForDeductionTransaction,ExceededDaysType,RoundAnnualVacBalance,Religion,IsOfficial,OverlapWithAnotherVac,ConsiderAllowedDays,TimesNoInYear,AllowedDaysNo,ExcludedFromSSRequests "
        mInsertParameterValues = " @Code,@EngName,@ArbName,@ArbName4S,@IsPaid,@Sex,@IsAnnual,@IsSickVacation,@AffectEOS,@HasPayment,@ForSalaryTransaction,@Remarks,@RegUserID,@RegComputerID,@CompanyId,@OBalanceTransactionID,@OverDueVacationID,@Stage1PCT,@Stage2PCT,@Stage3PCT,@ForDeductionTransaction,@ExceededDaysType,@RoundAnnualVacBalance,@Religion,@IsOfficial,@OverlapWithAnotherVac,@ConsiderAllowedDays,@TimesNoInYear,@AllowedDaysNo,@ExcludedFromSSRequests "
        mUpdateParameter = " Code=@Code,EngName=@EngName,ArbName=@ArbName,ArbName4S=@ArbName4S,IsPaid=@IsPaid,Sex=@Sex,IsAnnual=@IsAnnual,IsSickVacation=@IsSickVacation,AffectEOS=@AffectEOS,HasPayment=@HasPayment,ForSalaryTransaction=@ForSalaryTransaction,Remarks=@Remarks,OBalanceTransactionID=@OBalanceTransactionID,OverDueVacationID=@OverDueVacationID,Stage1PCT=@Stage1PCT,Stage2PCT=@Stage2PCT,Stage3PCT=@Stage3PCT,ForDeductionTransaction=@ForDeductionTransaction ,ExceededDaysType=@ExceededDaysType,RoundAnnualVacBalance=@RoundAnnualVacBalance,Religion=@Religion,IsOfficial=@IsOfficial,OverlapWithAnotherVac=@OverlapWithAnotherVac,ConsiderAllowedDays=@ConsiderAllowedDays,TimesNoInYear=@TimesNoInYear,AllowedDaysNo=@AllowedDaysNo,ExcludedFromSSRequests=@ExcludedFromSSRequests "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    Private mID As Object
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mIsPaid As Object
    Private mSex As Char
    Private mReligion As String
    Private mIsAnnual As Boolean
    Private mIsOfficial As Boolean
    Private mOverlapWithAnotherVac As Boolean

    Private mConsiderAllowedDays As Boolean
    Private mExcludedFromSSRequests As Boolean
    Private mAllowedDaysNo As Integer
    Private mTimesNoInYear As Integer

    Private mRoundAnnualVacBalance As Boolean

    Private mIsSickVacation As Boolean
    Private mAffectEOS As Boolean
    Private mHasPayment As Boolean
    Private mForSalaryTransaction As Integer
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mCompanyId As Object
    Private mOBalanceTransactionID As Object
    Private mOverDueVacationID As Object
    Private mStage1PCT As Object
    Private mStage2PCT As Object
    Private mStage3PCT As Object
    Private mForDeductionTransaction As Integer
    Private mExceededDaysType As Integer


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
            mArbName4S = mStringHandler.ReplaceHamza(Value)
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
    Public Property IsPaid() As Object
        Get
            Return mIsPaid
        End Get
        Set(ByVal Value As Object)
            mIsPaid = Value
        End Set
    End Property
    Public Property Sex() As Object
        Get
            Return mSex
        End Get
        Set(ByVal Value As Object)
            mSex = Value
        End Set
    End Property
    Public Property Religion() As Object
        Get
            Return mReligion
        End Get
        Set(ByVal Value As Object)
            mReligion = Value
        End Set
    End Property
    Public Property IsAnnual() As Boolean
        Get
            Return mIsAnnual
        End Get
        Set(ByVal Value As Boolean)
            mIsAnnual = Value
        End Set
    End Property
    Public Property IsOfficial() As Boolean
        Get
            Return mIsOfficial
        End Get
        Set(ByVal Value As Boolean)
            mIsOfficial = Value
        End Set
    End Property
    Public Property OverlapWithAnotherVac() As Boolean
        Get
            Return mOverlapWithAnotherVac
        End Get
        Set(ByVal Value As Boolean)
            mOverlapWithAnotherVac = Value
        End Set
    End Property

    Public Property ConsiderAllowedDays() As Boolean
        Get
            Return mConsiderAllowedDays
        End Get
        Set(ByVal Value As Boolean)
            mConsiderAllowedDays = Value
        End Set
    End Property
    Public Property ExcludedFromSSRequests() As Boolean
        Get
            Return mExcludedFromSSRequests
        End Get
        Set(ByVal Value As Boolean)
            mExcludedFromSSRequests = Value
        End Set
    End Property

    Public Property TimesNoInYear() As Integer
        Get
            Return mTimesNoInYear
        End Get
        Set(ByVal Value As Integer)
            mTimesNoInYear = Value
        End Set
    End Property
    Public Property AllowedDaysNo() As Integer
        Get
            Return mAllowedDaysNo
        End Get
        Set(ByVal Value As Integer)
            mAllowedDaysNo = Value
        End Set
    End Property
    Public Property RoundAnnualVacBalance() As Boolean
        Get
            Return mRoundAnnualVacBalance
        End Get
        Set(ByVal Value As Boolean)
            mRoundAnnualVacBalance = Value
        End Set
    End Property
    Public Property IsSickVacation() As Boolean
        Get
            Return mIsSickVacation
        End Get
        Set(ByVal Value As Boolean)
            mIsSickVacation = Value
        End Set
    End Property
    Public Property AffectEOS() As Boolean
        Get
            Return mAffectEOS
        End Get
        Set(ByVal Value As Boolean)
            mAffectEOS = Value
        End Set
    End Property
    Public Property HasPayment() As Boolean
        Get
            Return mHasPayment
        End Get
        Set(ByVal Value As Boolean)
            mHasPayment = Value
        End Set
    End Property
    Public Property ForSalaryTransaction() As Integer
        Get
            Return mForSalaryTransaction
        End Get
        Set(ByVal Value As Integer)
            mForSalaryTransaction = Value
        End Set
    End Property
    Public Property ForDeductionTransaction() As Integer
        Get
            Return mForDeductionTransaction
        End Get
        Set(ByVal Value As Integer)
            mForDeductionTransaction = Value
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
    Public Property RegUserID() As Object
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Object)
            mRegUserID = Value
        End Set
    End Property
    Public Property RegComputerID() As Object
        Get
            Return mRegComputerID
        End Get
        Set(ByVal Value As Object)
            mRegComputerID = Value
        End Set
    End Property
    Public Property RegDate() As Object
        Get
            Return mRegDate
        End Get
        Set(ByVal Value As Object)
            mRegDate = Value
        End Set
    End Property
    Public Property CancelDate() As Object
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As Object)
            mCancelDate = Value
        End Set
    End Property
    Public Property CompanyId() As Object
        Get
            Return mCompanyId
        End Get
        Set(ByVal Value As Object)
            mCompanyId = Value
        End Set
    End Property
    Public Property OBalanceTransactionID() As Object
        Get
            Return mOBalanceTransactionID
        End Get
        Set(ByVal Value As Object)
            mOBalanceTransactionID = Value
        End Set
    End Property
    Public Property OverDueVacationID() As Object
        Get
            Return mOverDueVacationID
        End Get
        Set(ByVal Value As Object)
            mOverDueVacationID = Value
        End Set
    End Property
    Public Property Stage1PCT() As Object
        Get
            Return mStage1PCT
        End Get
        Set(ByVal Value As Object)
            mStage1PCT = Value
        End Set
    End Property
    Public Property Stage2PCT() As Object
        Get
            Return mStage2PCT
        End Get
        Set(ByVal Value As Object)
            mStage2PCT = Value
        End Set
    End Property
    Public Property Stage3PCT() As Object
        Get
            Return mStage3PCT
        End Get
        Set(ByVal Value As Object)
            mStage3PCT = Value
        End Set
    End Property
    Public Property ExceededDaysType() As Integer
        Get
            Return mExceededDaysType
        End Get
        Set(ByVal Value As Integer)
            mExceededDaysType = Value
        End Set
    End Property

#End Region

#Region "Public Function"

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Input : Filter AS String
    'Steps: 
    '               - Fill Dataset with the results of sqldataAdapter
    '               - Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '               - Clear all private members of the class
    '               - Return true if ID of Filteration >0 (Condition Is Found)
    '
    'Description:   Find all columns from hrs_VacationsTypes table WHERE filter and canceldate = null 
    '=====================================================================

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If
            'Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
            '==================== Order By Modification [ End ]
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where CancelDate Is Null And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID = " & Me.MainCompanyID)
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Input : Filter AS String
    'Steps: 
    '               - Execute sql statment to get ID from hrs_VacationsTypes where filter 
    '               - Check if record already exist in hrs_VacationsTypes table 
    '                       If exist, Run Update
    '                       If not exist, Run Save
    '
    'Description:   Save or Update row 
    '=====================================================================

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_VacationsTypes Where " & Filter
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: 
    '               - Execute proc (mInsertCommand) to insert new row in hrs_VacationsTypes table
    '               - Return True
    '
    'Description:   Save New Row in hrs_VacationsTypes table
    '=====================================================================

    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand)
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: 
    '               - Execute proc (StrUpdateCommand) to update existing row in hrs_VacationsTypes table
    '               - Return True
    '
    'Description:   Update existing row in hrs_VacationsTypes table WHERE (Filter)
    '=====================================================================

    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
                            Return True

                            Exit For
                        End If

                        Exit For
                    End If

                End With

            Next
        Next

    End Function

    Public Function FixNull(ByVal obj As Object, ByVal DataColumn As Global.System.Data.DataColumn) As Object
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: 
    '               - Execute proc (StrDeleteCommand) to delete existing row in hrs_VacationsTypes table
    '               - Return True
    '
    'Description:   Delete existing row in hrs_VacationsTypes table WHERE (Filter)
    '=====================================================================

    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            'SetParameter(mSqlCommand)
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Description: Clear all private members  of the class
    '=====================================================================

    Public Function Clear() As Boolean

        Try
            mID = 0
            mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mIsPaid = 0
            mSex = " "
            mReligion = String.Empty
            mIsAnnual = False
            mIsOfficial = False

            mOverlapWithAnotherVac = False
            mConsiderAllowedDays = False
            mExcludedFromSSRequests = False
            mTimesNoInYear = 0
            mAllowedDaysNo = 0
            mRoundAnnualVacBalance = False
            mIsSickVacation = False
            mAffectEOS = False
            mHasPayment = False
            mForSalaryTransaction = Nothing
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mCompanyId = 0
            mOBalanceTransactionID = Nothing
            mOverDueVacationID = Nothing
            mForDeductionTransaction = 0
            mStage1PCT = 0
            mStage2PCT = 0
            mStage3PCT = 0
            mExceededDaysType = 0
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: 
    '               -Execute proc (StrSelectCommand) to find first row in hrs_VacationsTypes table
    '               -Fill dataset with result of the proc
    '               -Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:   Find first row in hrs_VacationsTypes table
    '=====================================================================

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  and CompanyID = " & Me.MainCompanyID & " ORDER BY Code ASC"
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: 
    '               - Execute proc (StrSelectCommand) to find last row in hrs_VacationsTypes table
    '               - Fill dataset with result of sqlstatment
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:  Find Last row in hrs_VacationsTypes table
    '=====================================================================

    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  and CompanyID = " & Me.MainCompanyID & " ORDER BY Code DESC"
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: 
    '               - Execute proc (StrSelectCommand) to find Next row in hrs_VacationsTypes table
    '               - Fill dataset with result of the proc
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:   Find Next row in hrs_VacationsTypes table
    '=====================================================================

    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  and CompanyID = " & Me.MainCompanyID & " ORDER BY Code ASC"
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
    'Date : 10/07/2007
    'Steps: 
    '               - Execute proc (StrSelectCommand) to find previous row in hrs_VacationsTypes table
    '               - Fill dataset with result of the proc
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:   Find previous row in hrs_VacationsTypes table
    '==================================================================

    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code < '" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  and CompanyID = " & Me.MainCompanyID & " ORDER BY Code DESC"
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

#End Region

#Region "Class Private Function"

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class
    '=====================================================================

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mIsPaid = mDataHandler.DataValue_Out(.Item("IsPaid"), SqlDbType.SmallInt)
                mSex = mDataHandler.DataValue_Out(.Item("Sex"), SqlDbType.VarChar)
                mReligion = mDataHandler.DataValue_Out(.Item("Religion"), SqlDbType.VarChar)
                mIsAnnual = mDataHandler.DataValue_Out(.Item("IsAnnual"), SqlDbType.Bit)
                mIsOfficial = mDataHandler.DataValue_Out(.Item("IsOfficial"), SqlDbType.Bit)
                mOverlapWithAnotherVac = mDataHandler.DataValue_Out(.Item("OverlapWithAnotherVac"), SqlDbType.Bit)
                mConsiderAllowedDays = mDataHandler.DataValue_Out(.Item("ConsiderAllowedDays"), SqlDbType.Bit)
                mExcludedFromSSRequests = mDataHandler.DataValue_Out(.Item("ExcludedFromSSRequests"), SqlDbType.Bit)
                mTimesNoInYear = mDataHandler.DataValue_Out(.Item("TimesNoInYear"), SqlDbType.Int)
                mAllowedDaysNo = mDataHandler.DataValue_Out(.Item("AllowedDaysNo"), SqlDbType.Int)

                mRoundAnnualVacBalance = mDataHandler.DataValue_Out(.Item("RoundAnnualVacBalance"), SqlDbType.Bit)
                mIsSickVacation = mDataHandler.DataValue_Out(.Item("IsSickVacation"), SqlDbType.Bit)
                mAffectEOS = mDataHandler.DataValue_Out(.Item("AffectEOS"), SqlDbType.Bit)
                mHasPayment = mDataHandler.DataValue_Out(.Item("HasPayment"), SqlDbType.Bit)
                mForSalaryTransaction = mDataHandler.DataValue_Out(.Item("ForSalaryTransaction"), SqlDbType.Int)
                mForDeductionTransaction = mDataHandler.DataValue_Out(.Item("ForDeductionTransaction"), SqlDbType.Int)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mCompanyId = mDataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int, True)
                mOBalanceTransactionID = mDataHandler.DataValue_Out(.Item("OBalanceTransactionID"), SqlDbType.Int, True)
                mOverDueVacationID = mDataHandler.DataValue_Out(.Item("OverDueVacationID"), SqlDbType.Int, True)
                mStage1PCT = mDataHandler.DataValue_Out(.Item("Stage1PCT"), SqlDbType.Real)
                mStage2PCT = mDataHandler.DataValue_Out(.Item("Stage2PCT"), SqlDbType.Real)
                mStage3PCT = mDataHandler.DataValue_Out(.Item("Stage3PCT"), SqlDbType.Real)
                mExceededDaysType = mDataHandler.DataValue_Out(.Item("ExceededDaysType"), SqlDbType.Int, True)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Description:   Make the values of parameter equal values of private member of the class
    '=====================================================================

    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsPaid", SqlDbType.SmallInt)).Value = mIsPaid
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Sex", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSex, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Religion", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mReligion, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsAnnual", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsAnnual, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsOfficial", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsOfficial, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OverlapWithAnotherVac", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mOverlapWithAnotherVac, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ConsiderAllowedDays", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mConsiderAllowedDays, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExcludedFromSSRequests", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mExcludedFromSSRequests, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TimesNoInYear", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTimesNoInYear, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AllowedDaysNo", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mAllowedDaysNo, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RoundAnnualVacBalance", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mRoundAnnualVacBalance, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsSickVacation", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsSickVacation, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AffectEOS", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mAffectEOS, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HasPayment", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHasPayment, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ForSalaryTransaction", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mForSalaryTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ForDeductionTransaction", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mForDeductionTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OBalanceTransactionID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mOBalanceTransactionID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OverDueVacationID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mOverDueVacationID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Stage1PCT", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mStage1PCT, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Stage2PCT", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mStage2PCT, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Stage3PCT", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mStage3PCT, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExceededDaysType", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mExceededDaysType, SqlDbType.Int)


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


Public Class Clshrs_VacationsTypes
    Inherits Clshrs_VacationsTypesBase

    Public Sub New(ByVal page As Global.System.Web.UI.Page)
        MyBase.New(page)
    End Sub
    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()
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

    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                Item.DisplayText = " "
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

    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.WebCombo.WebCombo, ByVal Filter As String) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = "Select ID , Code , EngName ,ArbName From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 and CompanyID= " & Me.MainCompanyID & IIf(Filter.Length > 0, " And " & Filter, " ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Rows.Clear()
            DdlValues.DataValueField = "ID"
            If ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName") = "ArbName" Then
                DdlValues.Columns.FromKey("ArbName").Hidden = False
                DdlValues.Columns.FromKey("EngName").Hidden = True
            Else
                DdlValues.Columns.FromKey("ArbName").Hidden = True
                DdlValues.Columns.FromKey("EngName").Hidden = False
            End If
            DdlValues.DataTextField = ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")

            DdlValues.DataSource = ObjDataset.Tables(0).DefaultView
            DdlValues.DataBind()
            If DdlValues.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")

        Finally
            'ObjDataset.Dispose()
        End Try

    End Function

    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, Optional ByVal NullNode As Boolean = False, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                Item.Value = 0
                'DdlValues.SelectedValue = Item.Value
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
    'Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, Optional ByVal Filter As String = "") As Boolean
    '    Dim ObjDataRow As DataRow
    '    Dim StrSelectCommand As String
    '    Dim ObjDataset As New DataSet
    '    Dim Item As Global.System.Web.UI.WebControls.ListItem
    '    Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
    '    Try

    '        StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
    '        ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
    '        DdlValues.Items.Clear()

    '        For Each ObjDataRow In ObjDataset.Tables(0).Rows
    '            Item = New Global.System.Web.UI.WebControls.ListItem
    '            'Item.Text = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
    '            Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
    '            If (Item.Text.Trim = "") Then
    '                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
    '            End If
    '            Item.Value = ObjDataRow("ID")
    '            DdlValues.Items.Add(Item)
    '        Next

    '        If DdlValues.Items.Count > 0 Then
    '            Return True
    '        End If

    '    Catch ex As Exception
    '        mPage.Session.Add("ErrorValue", ex)
    '        mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
    '        mPage.Response.Redirect("ErrorPage.aspx")
    '    Finally
    '        ObjDataset.Dispose()
    '    End Try
    'End Function


    Public Function CountAnnual() As Integer
        Dim StrSelectCommand As String
        Dim intCountAnnual As Integer
        Try
            StrSelectCommand = "SELECT COUNT(*) AS Expr1 FROM hrs_VacationsTypes" & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And IsAnnual = 1 and IsNull(CancelDate,'')='' "

            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSelectCommand

            mSqlCommand.Connection.Open()
            intCountAnnual = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

            Return intCountAnnual
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function CountSick() As Integer
        Dim StrSelectCommand As String
        Dim intCountSick As Integer
        Try
            StrSelectCommand = "SELECT COUNT(*) AS Expr1 FROM hrs_VacationsTypes" & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And IsSickVacation = 1 and IsNull(CancelDate,'')='' "

            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSelectCommand

            mSqlCommand.Connection.Open()
            intCountSick = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

            Return intCountSick
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function UpdateAnnual()
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "Update hrs_VacationsTypes Set IsAnnual = 0  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  and IsNull(CancelDate,'')='' "

            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSelectCommand

            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function UpdateSick()
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "Update hrs_VacationsTypes Set IsSickVacation = 0  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  and IsNull(CancelDate,'')='' "

            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSelectCommand

            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function

    'Added: Hassan Kurdi
    'Date: 2022-04-04
    'Purpose: Get Is Sick Vacations
    Public Function GetIsSickVacations() As Data.DataSet
        Dim strcommand As String = "SELECT * FROM hrs_VacationsTypes WHERE IsSickVacation = 1 "
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, strcommand)
    End Function
End Class
