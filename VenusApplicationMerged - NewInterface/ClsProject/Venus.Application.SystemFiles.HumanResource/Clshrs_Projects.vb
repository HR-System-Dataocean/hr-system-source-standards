Imports Venus.Application.SystemFiles.System
Public Class Clshrs_Projects
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page, TName As String)
        MyBase.New(Page)
        mTable = " hrs_Projects "
        mInsertParameter = "" &
          "Code," &
          "EngName," &
          "ArbName," &
          "DepartmentID," &
          "ArbName4S," &
          "Phone," &
          "Mobile," &
          "Fax," &
          "Email," &
          "Adress," &
          "ContactPerson," &
          "ProjectPeriod," &
          "ClaimDuration," &
          "StartDate," &
          "EndDate," &
          "CreditLimit," &
          "CreditPeriod," &
          "IsAdvance," &
          "IsHijri," &
          "NotifyPeriod," &
          "CompanyConditions," &
          "ClientConditions," &
          "IsLocked," &
          "IsStoped," &
          "BranchID," &
          "CompanyID," &
          "Remarks," &
          "RegUserID," &
          "RegComputerID," &
          "WorkConditions," &
          "LocationID," &
          "AbsentTransaction," &
          "LeaveTransaction," &
          "LateTransaction," &
          "OTTransaction," &
          "HOTTransaction," &
          "SickTransaction," &
              "CostCenterCode1," &
"CostCenterCode2," &
"CostCenterCode3," &
"CostCenterCode4"
        mInsertParameterValues = "" &
          " @Code," &
          " @EngName," &
          " @ArbName," &
          " @DepartmentID," &
          " @ArbName4S," &
          " @Phone," &
          " @Mobile," &
          " @Fax," &
          " @Email," &
          " @Adress," &
          " @ContactPerson," &
          " @ProjectPeriod," &
          " @ClaimDuration," &
          " @StartDate," &
          " @EndDate," &
          " @CreditLimit," &
          " @CreditPeriod," &
          " @IsAdvance," &
          " @IsHijri," &
          " @NotifyPeriod," &
          " @CompanyConditions," &
          " @ClientConditions," &
          " @IsLocked," &
          " @IsStoped," &
          " @BranchID," &
          " @CompanyID," &
          " @Remarks," &
          " @RegUserID," &
          " @RegComputerID," &
          " @WorkConditions," &
          " @LocationID," &
          " @AbsentTransaction," &
          " @LeaveTransaction," &
          " @LateTransaction," &
          " @OTTransaction," &
          " @HOTTransaction," &
          " @SickTransaction," &
             "@CostCenterCode1," &
"@CostCenterCode2," &
"@CostCenterCode3," &
"@CostCenterCode4"
        mUpdateParameter = "" &
          "Code=@Code," &
          "EngName=@EngName," &
          "ArbName=@ArbName," &
          "DepartmentID=@DepartmentID," &
          "ArbName4S=@ArbName4S," &
          "Phone=@Phone," &
          "Mobile=@Mobile," &
          "Fax=@Fax," &
          "Email=@Email," &
          "Adress=@Adress," &
          "ContactPerson=@ContactPerson," &
          "ProjectPeriod=@ProjectPeriod," &
          "ClaimDuration=@ClaimDuration," &
          "StartDate=@StartDate," &
          "EndDate=@EndDate," &
          "CreditLimit=@CreditLimit," &
          "CreditPeriod=@CreditPeriod," &
          "IsAdvance=@IsAdvance," &
          "IsHijri=@IsHijri," &
          "NotifyPeriod=@NotifyPeriod," &
          "CompanyConditions=@CompanyConditions," &
          "ClientConditions=@ClientConditions," &
          "IsLocked=@IsLocked," &
          "IsStoped=@IsStoped," &
          "BranchID=@BranchID," &
          "Remarks=@Remarks," &
          "WorkConditions=@WorkConditions," &
          "LocationID=@LocationID," &
          "AbsentTransaction=@AbsentTransaction," &
          "LeaveTransaction=@LeaveTransaction," &
          "LateTransaction=@LateTransaction," &
          "OTTransaction=@OTTransaction," &
          "HOTTransaction=@HOTTransaction," &
          "SickTransaction=@SickTransaction," &
        "CostCenterCode1=@CostCenterCode1," &
        "CostCenterCode2=@CostCenterCode2," &
        "CostCenterCode3=@CostCenterCode3," &
        "CostCenterCode4=@CostCenterCode4"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

    Public Sub New(page As Web.UI.Page)
        MyBase.New(page)
    End Sub
#End Region

#Region "Private Members"
    Private mID As Integer
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
    Private mDepartmentID As Integer
    Private mArbName4S As String
    Private mPhone As String
    Private mMobile As String
    Private mFax As String
    Private mEmail As String
    Private mAdress As String
    Private mContactPerson As String
    Private mProjectPeriod As Integer
    Private mClaimDuration As Integer
    Private mStartDate As DateTime
    Private mEndDate As DateTime
    Private mCreditLimit As Decimal
    Private mCreditPeriod As Integer
    Private mIsAdvance As Boolean
    Private mIsHijri As Boolean
    Private mNotifyPeriod As Integer
    Private mCompanyConditions As String
    Private mClientConditions As String
    Private mIsLocked As Boolean
    Private mIsStoped As Boolean
    Private mBranchID As Integer
    Private mCompanyID As Integer
    Private mRemarks As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As DateTime
    Private mCancelDate As DateTime
    Private mWorkConditions As String
    Private mLocationID As Integer
    Private mAbsentTransaction As Integer
    Private mLeaveTransaction As Integer
    Private mLateTransaction As Integer
    Private mSickTransaction As Integer
    Private mOTTransaction As Integer
    Private mHOTTransaction As Integer
    Private mCostCenterCode1 As String
    Private mCostCenterCode2 As String
    Private mCostCenterCode3 As String
    Private mCostCenterCode4 As String

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
    Public Property DepartmentID() As Integer
        Get
            Return mDepartmentID
        End Get
        Set(ByVal Value As Integer)
            mDepartmentID = Value
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
    Public Property Phone() As String
        Get
            Return mPhone
        End Get
        Set(ByVal Value As String)
            mPhone = Value
        End Set
    End Property
    Public Property Mobile() As String
        Get
            Return mMobile
        End Get
        Set(ByVal Value As String)
            mMobile = Value
        End Set
    End Property
    Public Property Fax() As String
        Get
            Return mFax
        End Get
        Set(ByVal Value As String)
            mFax = Value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return mEmail
        End Get
        Set(ByVal Value As String)
            mEmail = Value
        End Set
    End Property
    Public Property Adress() As String
        Get
            Return mAdress
        End Get
        Set(ByVal Value As String)
            mAdress = Value
        End Set
    End Property
    Public Property ContactPerson() As String
        Get
            Return mContactPerson
        End Get
        Set(ByVal Value As String)
            mContactPerson = Value
        End Set
    End Property
    Public Property ProjectPeriod() As Integer
        Get
            Return mProjectPeriod
        End Get
        Set(ByVal Value As Integer)
            mProjectPeriod = Value
        End Set
    End Property
    Public Property ClaimDuration() As Integer
        Get
            Return mClaimDuration
        End Get
        Set(ByVal Value As Integer)
            mClaimDuration = Value
        End Set
    End Property
    Public Property StartDate() As DateTime
        Get
            Return mStartDate
        End Get
        Set(ByVal Value As DateTime)
            mStartDate = Value
        End Set
    End Property
    Public Property EndDate() As DateTime
        Get
            Return mEndDate
        End Get
        Set(ByVal Value As DateTime)
            mEndDate = Value
        End Set
    End Property
    Public Property CreditLimit() As Decimal
        Get
            Return mCreditLimit
        End Get
        Set(ByVal Value As Decimal)
            mCreditLimit = Value
        End Set
    End Property
    Public Property CreditPeriod() As Integer
        Get
            Return mCreditPeriod
        End Get
        Set(ByVal Value As Integer)
            mCreditPeriod = Value
        End Set
    End Property
    Public Property IsAdvance() As Boolean
        Get
            Return mIsAdvance
        End Get
        Set(ByVal Value As Boolean)
            mIsAdvance = Value
        End Set
    End Property
    Public Property IsHijri() As Boolean
        Get
            Return mIsHijri
        End Get
        Set(ByVal Value As Boolean)
            mIsHijri = Value
        End Set
    End Property
    Public Property NotifyPeriod() As Integer
        Get
            Return mNotifyPeriod
        End Get
        Set(ByVal Value As Integer)
            mNotifyPeriod = Value
        End Set
    End Property
    Public Property CompanyConditions() As String
        Get
            Return mCompanyConditions
        End Get
        Set(ByVal Value As String)
            mCompanyConditions = Value
        End Set
    End Property
    Public Property ClientConditions() As String
        Get
            Return mClientConditions
        End Get
        Set(ByVal Value As String)
            mClientConditions = Value
        End Set
    End Property
    Public Property IsLocked() As Boolean
        Get
            Return mIsLocked
        End Get
        Set(ByVal Value As Boolean)
            mIsLocked = Value
        End Set
    End Property
    Public Property IsStoped() As Boolean
        Get
            Return mIsStoped
        End Get
        Set(ByVal Value As Boolean)
            mIsStoped = Value
        End Set
    End Property
    Public Property BranchID() As Integer
        Get
            Return mBranchID
        End Get
        Set(ByVal Value As Integer)
            mBranchID = Value
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
    Public Property WorkConditions() As String
        Get
            Return mWorkConditions
        End Get
        Set(ByVal Value As String)
            mWorkConditions = Value
        End Set
    End Property
    Public Property LocationID() As Integer
        Get
            Return mLocationID
        End Get
        Set(ByVal Value As Integer)
            mLocationID = Value
        End Set
    End Property
    Public Property AbsentTransaction() As Integer
        Get
            Return mAbsentTransaction
        End Get
        Set(ByVal Value As Integer)
            mAbsentTransaction = Value
        End Set
    End Property
    Public Property LeaveTransaction() As Integer
        Get
            Return mLeaveTransaction
        End Get
        Set(ByVal Value As Integer)
            mLeaveTransaction = Value
        End Set
    End Property
    Public Property LateTransaction() As Integer
        Get
            Return mLateTransaction
        End Get
        Set(ByVal Value As Integer)
            mLateTransaction = Value
        End Set
    End Property
    Public Property SickTransaction() As Integer
        Get
            Return mSickTransaction
        End Get
        Set(ByVal Value As Integer)
            mSickTransaction = Value
        End Set
    End Property
    Public Property OTTransaction() As Integer
        Get
            Return mOTTransaction
        End Get
        Set(ByVal Value As Integer)
            mOTTransaction = Value
        End Set
    End Property
    Public Property HOTTransaction() As Integer
        Get
            Return mHOTTransaction
        End Get
        Set(ByVal Value As Integer)
            mHOTTransaction = Value
        End Set
    End Property
    Public Property CostCenterCode1() As String
        Get
            Return mCostCenterCode1
        End Get
        Set(ByVal Value As String)
            mCostCenterCode1 = Value
        End Set
    End Property

    Public Property CostCenterCode2() As String
        Get
            Return mCostCenterCode2
        End Get
        Set(ByVal Value As String)
            mCostCenterCode2 = Value
        End Set
    End Property
    Public Property CostCenterCode3() As String
        Get
            Return mCostCenterCode3
        End Get
        Set(ByVal Value As String)
            mCostCenterCode3 = Value
        End Set
    End Property
    Public Property CostCenterCode4() As String
        Get
            Return mCostCenterCode4
        End Get
        Set(ByVal Value As String)
            mCostCenterCode4 = Value
        End Set
    End Property
#End Region

#Region "Public Function"

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Input : Filter as string (ex. ID=2)
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '
    'Description: Find all columns from Clshrs_Projects table where filter and canceldate = null 
    '=====================================================================

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID = " & Me.MainCompanyID & " And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID)
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = StrSelectCommand & orderByStr
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
    'Input : Filter as string (ex. ID=2)
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '
    'Description: Find all columns from Clshrs_Projects table where filter and canceldate = null 
    '=====================================================================

    Public Function FindTop(ByVal Filter As String) As Integer
        Dim StrSelectCommand As String = ""

        Try
            Dim query As String = "SELECT TOP 1 ID FROM " & mTable
            Dim projectId As Integer
            mSqlCommand = New SqlClient.SqlCommand

            StrSelectCommand = query & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 "
            StrSelectCommand = StrSelectCommand & Filter
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSelectCommand
            mSqlCommand.Connection.Open()
            projectId = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

            Return projectId

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from Clshrs_Projects where filter 
    '       2-Check if ID > 0 this mean that row is already exist in Clshrs_Projects  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in Clshrs_Projects  table
    '=====================================================================

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Try
            Dim StrSqlCommand As String
            Dim Value As Integer
            StrSqlCommand = "Select ID From hrs_Projects Where " & Filter
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSqlCommand
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007

    'Description: Save New Row in Clshrs_Projects  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in Clshrs_Projects  table

    '=====================================================================

    Public Function Save() As Integer
        Dim Value As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, OperationType.Save)
            mSqlCommand.Connection.Open()
            Value = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            Return Value
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in Clshrs_Projects  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in Clshrs_Projects  table

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
            SetParameter(mSqlCommand, OperationType.Update)
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in Clshrs_Projects  table where filter
    'Steps: 
    '       execute sqlstatment to Delete existing row in Clshrs_Projects  table
    '=====================================================================

    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
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
    'Description: Clear all private members  of the class
    '=====================================================================

    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mDepartmentID = 0
            mArbName4S = String.Empty
            mPhone = String.Empty
            mMobile = String.Empty
            mFax = String.Empty
            mEmail = String.Empty
            mAdress = String.Empty
            mContactPerson = String.Empty
            mProjectPeriod = 0
            mClaimDuration = 0
            mStartDate = Nothing
            mEndDate = Nothing
            mCreditLimit = 0
            mCreditPeriod = 0
            mIsAdvance = False
            mIsHijri = False
            mNotifyPeriod = 0
            mCompanyConditions = String.Empty
            mClientConditions = String.Empty
            mIsLocked = False
            mIsStoped = False
            mBranchID = 0
            mCompanyID = 0
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mWorkConditions = String.Empty
            mLocationID = 0
            mAbsentTransaction = 0
            mLeaveTransaction = 0
            mLateTransaction = 0
            mSickTransaction = 0
            mOTTransaction = 0
            mHOTTransaction = 0
            mCostCenterCode1 = String.Empty
            mCostCenterCode2 = String.Empty
            mCostCenterCode3 = String.Empty
            mCostCenterCode4 = String.Empty
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Description:  find first row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to find first row in Clshrs_Projects  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '=====================================================================

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID & " ORDER BY Code ASC"
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
    'Description:  find Last row in Clshrs_Projects  table
    'Steps: 
    '       1-execute sqlstatment to find last row in Clshrs_Projects  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '=====================================================================

    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID & "  ORDER BY Code DESC"
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
    'Description:  find Next row in Clshrs_Projects  table
    'Steps: 
    '       1-execute sqlstatment to find Next row in Clshrs_Projects  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '=====================================================================

    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID & "  ORDER BY Code ASC"
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
    '               - Execute proc sqlstatment to find previous row in Clshrs_Projects  table
    '               - Fill dataset with result of the proc
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:   Find previous row in Clshrs_Projects  table
    '=====================================================================

    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code < '" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID & "  ORDER BY Code DESC"
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
    'Created by : [0259]
    'Date : 16/07/2007
    'Steps: 
    '               - Clear the DropDownList Values
    '               - Check NullNode()
    '               - If Null screen [Select Your Choice] choice
    '               - If not screen the EngName for the selected Item value
    'Description:   Fill in the DropDownList with DB records
    '==================================================================
    'Modification :  [0256] 5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '             :                  According to Page Language 
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By Code ", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & " Order By Code ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")

                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow("Code"), SqlDbType.VarChar) & " - " & _
                mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
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
    '=====================================================================
    'Created by : [0259]
    'Date : 16/07/2007
    'Input : Ds as Dataset 
    'Description:   Get all the records data from the DataBase Table(Me.mTable)
    '=====================================================================
    'Modification :  [0256] 5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '             :                  According to Page Language 
    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)

        Try

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 and companyID = " & Me.MainCompanyID & "  Order By EngName "
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
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

            StrCommandString = "  Select * From " & mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And CompanyID = " & Me.MainCompanyID & IIf(Filter.Length > 0, " And " & Filter, " ")
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
            DdlValues.DataTextField = "Code"

            DdlValues.DataSource = ObjDataset.Tables(0).DefaultView
            DdlValues.DataBind()
            If DdlValues.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            'ObjDataset.Dispose()
        End Try

    End Function
#End Region

#Region "Class Private Function"
    '========================================================================
    'ProcedureName  :  GetParameter 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :10/03/2015
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = [Shared].DataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = [Shared].DataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = [Shared].DataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = [Shared].DataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mDepartmentID = [Shared].DataHandler.DataValue_Out(.Item("DepartmentID"), SqlDbType.Int)
                mArbName4S = [Shared].DataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mPhone = [Shared].DataHandler.DataValue_Out(.Item("Phone"), SqlDbType.VarChar)
                mMobile = [Shared].DataHandler.DataValue_Out(.Item("Mobile"), SqlDbType.VarChar)
                mFax = [Shared].DataHandler.DataValue_Out(.Item("Fax"), SqlDbType.VarChar)
                mEmail = [Shared].DataHandler.DataValue_Out(.Item("Email"), SqlDbType.VarChar)
                mAdress = [Shared].DataHandler.DataValue_Out(.Item("Adress"), SqlDbType.VarChar)
                mContactPerson = [Shared].DataHandler.DataValue_Out(.Item("ContactPerson"), SqlDbType.VarChar)
                mProjectPeriod = [Shared].DataHandler.DataValue_Out(.Item("ProjectPeriod"), SqlDbType.Int, True)
                mClaimDuration = [Shared].DataHandler.DataValue_Out(.Item("ClaimDuration"), SqlDbType.Int, True)
                mStartDate = [Shared].DataHandler.DataValue_Out(.Item("StartDate"), SqlDbType.DateTime)
                mEndDate = [Shared].DataHandler.DataValue_Out(.Item("EndDate"), SqlDbType.DateTime)
                mCreditLimit = [Shared].DataHandler.DataValue_Out(.Item("CreditLimit"), SqlDbType.Decimal)
                mCreditPeriod = [Shared].DataHandler.DataValue_Out(.Item("CreditPeriod"), SqlDbType.Int, True)
                mIsAdvance = [Shared].DataHandler.DataValue_Out(.Item("IsAdvance"), SqlDbType.Bit)
                mIsHijri = [Shared].DataHandler.DataValue_Out(.Item("IsHijri"), SqlDbType.Bit)
                mNotifyPeriod = [Shared].DataHandler.DataValue_Out(.Item("NotifyPeriod"), SqlDbType.Int, True)
                mCompanyConditions = [Shared].DataHandler.DataValue_Out(.Item("CompanyConditions"), SqlDbType.VarChar)
                mClientConditions = [Shared].DataHandler.DataValue_Out(.Item("ClientConditions"), SqlDbType.VarChar)
                mIsLocked = [Shared].DataHandler.DataValue_Out(.Item("IsLocked"), SqlDbType.Bit)
                mIsStoped = [Shared].DataHandler.DataValue_Out(.Item("IsStoped"), SqlDbType.Bit)
                mBranchID = [Shared].DataHandler.DataValue_Out(.Item("BranchID"), SqlDbType.Int, True)
                mCompanyID = [Shared].DataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int, True)
                mRemarks = [Shared].DataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mWorkConditions = [Shared].DataHandler.DataValue_Out(.Item("WorkConditions"), SqlDbType.VarChar)
                mLocationID = [Shared].DataHandler.DataValue_Out(.Item("LocationID"), SqlDbType.Int, True)
                mAbsentTransaction = [Shared].DataHandler.DataValue_Out(.Item("AbsentTransaction"), SqlDbType.Int, True)
                mLeaveTransaction = [Shared].DataHandler.DataValue_Out(.Item("LeaveTransaction"), SqlDbType.Int, True)
                mLateTransaction = [Shared].DataHandler.DataValue_Out(.Item("LateTransaction"), SqlDbType.Int, True)
                mSickTransaction = [Shared].DataHandler.DataValue_Out(.Item("SickTransaction"), SqlDbType.Int, True)
                mOTTransaction = [Shared].DataHandler.DataValue_Out(.Item("OTTransaction"), SqlDbType.Int, True)
                mHOTTransaction = [Shared].DataHandler.DataValue_Out(.Item("HOTTransaction"), SqlDbType.Int, True)
                mCostCenterCode1 = [Shared].DataHandler.DataValue_Out(.Item("CostCenterCode1"), SqlDbType.VarChar)
                mCostCenterCode2 = [Shared].DataHandler.DataValue_Out(.Item("CostCenterCode2"), SqlDbType.VarChar)
                mCostCenterCode3 = [Shared].DataHandler.DataValue_Out(.Item("CostCenterCode3"), SqlDbType.VarChar)
                mCostCenterCode4 = [Shared].DataHandler.DataValue_Out(.Item("CostCenterCode4"), SqlDbType.VarChar)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   : 10/03/2015 14:25:19
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal ptrOperationType As OperationType) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DepartmentID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mDepartmentID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Phone", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mPhone, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Mobile", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mMobile, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Fax", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mFax, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Email", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mEmail, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Adress", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mAdress, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ContactPerson", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mContactPerson, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ProjectPeriod", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mProjectPeriod, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ClaimDuration", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mClaimDuration, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@StartDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mStartDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EndDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mEndDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CreditLimit", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mCreditLimit, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CreditPeriod", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCreditPeriod, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsAdvance", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsAdvance, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsHijri", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsHijri, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NotifyPeriod", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mNotifyPeriod, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyConditions", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCompanyConditions, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ClientConditions", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mClientConditions, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsLocked", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsLocked, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsStoped", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsStoped, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BranchID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mBranchID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@WorkConditions", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mWorkConditions, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LocationID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mLocationID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AbsentTransaction", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mAbsentTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LeaveTransaction", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mLeaveTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LateTransaction", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mLateTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SickTransaction", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mSickTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OTTransaction", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mOTTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HOTTransaction", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mHOTTransaction, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenterCode1", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCostCenterCode1, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenterCode2", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCostCenterCode2, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenterCode3", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCostCenterCode3, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenterCode4", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCostCenterCode4, SqlDbType.VarChar)
            Select Case ptrOperationType
                Case OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End Select
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region

#Region "Class Destructors"

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Description :  Dispose dataset from the stack
    '=====================================================================

    Protected Overloads Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class

