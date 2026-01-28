Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesDecisions
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesDecisions "

        mInsertParameter = " EmpId, Code, MaritalStatusID, BankID, NationalityID, BankAccountNumber, DepartmentID, CompanyID, Remarks, BranchID, SponsorID, E_Mail, Phone, Mobile, ManagerID, SectorID, PassPortNo,SSnNo,PreviousSSnNo, Cost1, Cost2, Cost3, Cost4, 
LocationID, WorkE_Mail, ContractType, Professions, Position, EmployeeClass, LastEducations, GraduationDate,PreviousMaritalStatusID,PreviousBankID, PreviousNationalityID, PreviousBankAccountNumber, 
PreviousDepartmentID, PreviousCompanyID, PreviousRemarks, PreviousBranchID, PreviousSponsorID, PreviousE_Mail, PreviousPhone,PreviousMobile, PreviousManagerID, PreviousSectorID, PreviousPassPortNo, 
PreviousCost1, PreviousCost2, PreviousCost3, PreviousCost4, PreviousLocationID, PreviousWorkE_Mail,PreviousContractType,PreviousProfessions, PreviousPosition, PreviousEmployeeClass, PreviousLastEducations, 
PreviousGraduationDate,RegUserID "
        mInsertParameterValues = " @EmpId,@Code,@MaritalStatusID,@BankID,@NationalityID,@BankAccountNumber,@DepartmentID,@CompanyID,@Remarks,@BranchID,@SponsorID,@E_Mail,@Phone,@Mobile,@ManagerID,@SectorID,@PassPortNo,@SSnNo,@PreviousSSnNo,@Cost1,@Cost2,@Cost3,@Cost4,@LocationID,@WorkE_Mail,@ContractType,@Professions,@Position,@EmployeeClass,@LastEducations,@GraduationDate,@PreviousMaritalStatusID,@PreviousBankID,@PreviousNationalityID,@PreviousBankAccountNumber,@PreviousDepartmentID,@PreviousCompanyID,@PreviousRemarks,@PreviousBranchID,@PreviousSponsorID,@PreviousE_Mail,@PreviousPhone,@PreviousMobile,@PreviousManagerID,@PreviousSectorID,@PreviousPassPortNo,@PreviousCost1,@PreviousCost2,@PreviousCost3,@PreviousCost4,@PreviousLocationID,@PreviousWorkE_Mail,@PreviousContractType,@PreviousProfessions,@PreviousPosition,@PreviousEmployeeClass,@PreviousLastEducations,@PreviousGraduationDate,@RegUserID "
        mUpdateParameter = " [EmpId]=@EmpId,[Code]=@Code,[MaritalStatusID]=@MaritalStatusID,[BankID]=@BankID,[NationalityID]=@NationalityID,[BankAccountNumber]=@BankAccountNumber,[DepartmentID]=@DepartmentID,[CompanyID]=@CompanyID,Remarks=@Remarks,BranchID=@BranchID,SponsorID=@SponsorID,[E_Mail]=@E_Mail,Phone=@Phone,Mobile=@Mobile,ManagerID=@ManagerID,SectorID=@SectorID,PassPortNo=@PassPortNo,SSnNo=@SSnNo,PreviousSSnNo=@PreviousSSnNo,Cost1=@Cost1,Cost2=@Cost2,Cost13=@Cost3,Cost4=@Cost4,LocationID=@LocationID,WorkE_Mail=@WorkE_Mail,ContractType=@ContractType,Professions=@Professions,Position=@Position,EmployeeClass=@EmployeeClass,LastEducations=@LastEducations,GraduationDate=@GraduationDate,PreviousMaritalStatusID=@PreviousMaritalStatusID,PreviousBankID=@PreviousBankID,PreviousNationalityID=@PreviousNationalityID,PreviousBankAccountNumber=@PreviousBankAccountNumber,PreviousDepartmentID=@PreviousDepartmentID,PreviousCompanyID=@PreviousCompanyID,PreviousRemarks=@PreviousRemarks,PreviousBranchID=@PreviousBranchID,PreviousSponsorID=@PreviousSponsorID,PreviousE_Mail=@PreviousE_Mail,PreviousPhone=@PreviousPhone,PreviousMobile=@PreviousMobile,PreviousManagerID=@PreviousManagerID,PreviousSectorID=@PreviousSectorID,PreviousPassPortNo=@PreviousPassPortNo,PreviousCost1=@PreviousCost1,PreviousCost2=@PreviousCost2,PreviousCost13=@PreviousCost3,PreviousCost4=@PreviousCost4,PreviousLocationID=@PreviousLocationID,PreviousWorkE_Mail=@PreviousWorkE_Mail,PreviousContractType=@PreviousContractType,PreviousProfessions=@PreviousProfessions,PreviousPosition=@PreviousPosition,PreviousEmployeeClass=@PreviousEmployeeClass,PreviousLastEducations=@PreviousLastEducations,PreviousGraduationDate=@PreviousGraduationDate "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter

    End Sub

#End Region

#Region "Private Members"

    Public FilterBranches As String

    Public Enum ePrepareType
        BeginOfContract
        EndOfContract
        Normal
    End Enum

    Public Enum ePrepareStage
        Normal
        Vacation
        VacationCost
    End Enum

    Private mID As Object
    Private mEmpId As Object
    Private mCode As Object
    Private mMaritalStatusID As Object
    Private mBankID As Object
    Private mNationalityID As Object
    Private mBankAccountNumber As String
    Private mDepartmentID As Object
    Private mCompanyID As Object
    Private mRemarks As String
    Private mBranchID As Object
    Private mSponsorID As Object
    Private mE_Mail As String
    Private mPhone As String
    Private mMobile As String
    Private mManagerID As Object
    Private mSectorID As Object
    Private mPassPortNo As String
    Private mSSnNo As String
    Private mPreviousSSnNo As String
    Private mCost1 As Object
    Private mCost2	As	Object
    Private mCost3 As Object
    Private mCost4 As Object
    Private mLocationID As Object
    Private mWorkE_Mail As String
    Private mContractType As Object
    Private mProfessions As Object
    Private mPosition As Object
    Private mEmployeeClass As Object
    Private mLastEducations As Object
    Private mGraduationDate As Object
    Private mPreviousMaritalStatusID As Object
    Private mPreviousBankID As Object
    Private mPreviousNationalityID As Object
    Private mPreviousBankAccountNumber As String
    Private mPreviousDepartmentID As Object
    Private mPreviousCompanyID As Object
    Private mPreviousRemarks As String
    Private mPreviousBranchID As Object
    Private mPreviousSponsorID As Object
    Private mPreviousE_Mail As String
    Private mPreviousPhone As String
    Private mPreviousMobile As String
    Private mPreviousManagerID As Object
    Private mPreviousSectorID As Object
    Private mPreviousPassPortNo As String
    Private mPreviousCost1 As Object
    Private mPreviousCost2 As Object
    Private mPreviousCost3 As Object
    Private mPreviousCost4 As Object
    Private mPreviousLocationID As Object
    Private mPreviousWorkE_Mail As String
    Private mPreviousContractType As Object
    Private mPreviousProfessions As Object
    Private mPreviousPosition As Object
    Private mPreviousEmployeeClass As Object
    Private mPreviousLastEducations As Object
    Private mPreviousGraduationDate As Object
    Private mRegUserID As Object



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

    Public Property EmpId() As String
        Get
            Return mEmpId
        End Get
        Set(ByVal Value As String)
            mEmpId = Value
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
    Public Property MaritalStatusID() As Object
        Get
            Return mMaritalStatusID
        End Get
        Set(ByVal Value As Object)
            mMaritalStatusID = Value
        End Set
    End Property

    Public Property BankAccountNumber() As String
        Get
            Return mBankAccountNumber
        End Get
        Set(ByVal Value As String)
            mBankAccountNumber = Value
        End Set
    End Property


    Public Property BankID() As Object
        Get
            Return mBankID
        End Get
        Set(ByVal Value As Object)
            mBankID = Value
        End Set
    End Property
    Public Property NationalityID() As Object
        Get
            Return mNationalityID
        End Get
        Set(ByVal Value As Object)
            mNationalityID = Value
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
    Public Property DepartmentID() As Object
        Get
            Return mDepartmentID
        End Get
        Set(ByVal Value As Object)
            mDepartmentID = Value
        End Set
    End Property

    Public Property CompanyID() As Object
        Get
            Return mCompanyID
        End Get
        Set(ByVal Value As Object)
            mCompanyID = Value
        End Set
    End Property
    Public Property BranchID() As Object
        Get
            Return mBranchID
        End Get
        Set(ByVal Value As Object)
            mBranchID = Value
        End Set
    End Property

    Public Property SponsorID() As Object
        Get
            Return mSponsorID
        End Get
        Set(ByVal Value As Object)
            mSponsorID = Value
        End Set
    End Property
    Public Property ManagerID() As Object
        Get
            Return mManagerID
        End Get
        Set(ByVal Value As Object)
            mManagerID = Value
        End Set
    End Property
    'SectorID
    Public Property SectorID() As Object
        Get
            Return mSectorID
        End Get
        Set(ByVal Value As Object)
            mSectorID = Value
        End Set
    End Property
    Public Property E_Mail() As String
        Get
            Return mE_Mail
        End Get
        Set(ByVal Value As String)
            mE_Mail = Value
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


    Public Property Cost1() As Object
        Get
            Return mCost1
        End Get
        Set(ByVal value As Object)
            mCost1 = value
        End Set
    End Property
    Public Property Cost2() As Object
        Get
            Return mCost2
        End Get
        Set(ByVal value As Object)
            mCost2 = value
        End Set
    End Property
    Public Property Cost3() As Object
        Get
            Return mCost3
        End Get
        Set(ByVal value As Object)
            mCost3 = value
        End Set
    End Property
    Public Property Cost4() As Object
        Get
            Return mCost4
        End Get
        Set(ByVal value As Object)
            mCost4 = value
        End Set
    End Property

    Public Property LocationID() As Object
        Get
            Return mLocationID
        End Get
        Set(ByVal value As Object)
            mLocationID = value
        End Set
    End Property



    Public Property WorkE_Mail() As String
        Get
            Return mWorkE_Mail
        End Get
        Set(ByVal value As String)
            mWorkE_Mail = value
        End Set
    End Property
    Public Property PassPortNo() As String
        Get
            Return mPassPortNo
        End Get
        Set(ByVal value As String)
            mPassPortNo = value
        End Set
    End Property
    Public Property SSnNo() As String
        Get
            Return mSSnNo
        End Get
        Set(ByVal value As String)
            mSSnNo = value
        End Set
    End Property
    Public Property PreviousSSnNo() As String
        Get
            Return mPreviousSSnNo
        End Get
        Set(ByVal value As String)
            mPreviousSSnNo = value
        End Set
    End Property

    Public Property ContractType() As Object
        Get
            Return mContractType
        End Get
        Set(ByVal value As Object)
            mContractType = value
        End Set
    End Property

    Public Property Professions() As Object
        Get
            Return mProfessions
        End Get
        Set(ByVal value As Object)
            mProfessions = value
        End Set
    End Property
    Public Property Position() As Object
        Get
            Return mPosition
        End Get
        Set(ByVal value As Object)
            mPosition = value
        End Set
    End Property
    Public Property EmployeeClass() As Object
        Get
            Return mEmployeeClass
        End Get
        Set(ByVal value As Object)
            mEmployeeClass = value
        End Set
    End Property
    Public Property LastEducations() As Object
        Get
            Return mLastEducations
        End Get
        Set(ByVal value As Object)
            mLastEducations = value
        End Set
    End Property
    Public Property GraduationDate() As Object
        Get
            Return mGraduationDate
        End Get
        Set(ByVal value As Object)
            mGraduationDate = value
        End Set
    End Property

    Public Property PreviousMaritalStatusID() As Object
        Get
            Return mPreviousMaritalStatusID
        End Get
        Set(ByVal Value As Object)
            mPreviousMaritalStatusID = Value
        End Set
    End Property

    Public Property PreviousBankAccountNumber() As String
        Get
            Return mPreviousBankAccountNumber
        End Get
        Set(ByVal Value As String)
            mPreviousBankAccountNumber = Value
        End Set
    End Property


    Public Property PreviousBankID() As Object
        Get
            Return mPreviousBankID
        End Get
        Set(ByVal Value As Object)
            mPreviousBankID = Value
        End Set
    End Property
    Public Property PreviousNationalityID() As Object
        Get
            Return mPreviousNationalityID
        End Get
        Set(ByVal Value As Object)
            mPreviousNationalityID = Value
        End Set
    End Property


    Public Property PreviousRemarks() As String
        Get
            Return mPreviousRemarks
        End Get
        Set(ByVal Value As String)
            mPreviousRemarks = Value
        End Set
    End Property
    Public Property PreviousDepartmentID() As Object
        Get
            Return mPreviousDepartmentID
        End Get
        Set(ByVal Value As Object)
            mPreviousDepartmentID = Value
        End Set
    End Property

    Public Property PreviousCompanyID() As Object
        Get
            Return mPreviousCompanyID
        End Get
        Set(ByVal Value As Object)
            mPreviousCompanyID = Value
        End Set
    End Property
    Public Property PreviousBranchID() As Object
        Get
            Return mPreviousBranchID
        End Get
        Set(ByVal Value As Object)
            mPreviousBranchID = Value
        End Set
    End Property

    Public Property PreviousSponsorID() As Object
        Get
            Return mPreviousSponsorID
        End Get
        Set(ByVal Value As Object)
            mPreviousSponsorID = Value
        End Set
    End Property
    Public Property PreviousManagerID() As Object
        Get
            Return mPreviousManagerID
        End Get
        Set(ByVal Value As Object)
            mPreviousManagerID = Value
        End Set
    End Property

    Public Property PreviousSectorID() As Object
        Get
            Return mPreviousSectorID
        End Get
        Set(ByVal Value As Object)
            mPreviousSectorID = Value
        End Set
    End Property
    Public Property PreviousE_Mail() As String
        Get
            Return mPreviousE_Mail
        End Get
        Set(ByVal Value As String)
            mPreviousE_Mail = Value
        End Set
    End Property
    Public Property PreviousPhone() As String
        Get
            Return mPreviousPhone
        End Get
        Set(ByVal Value As String)
            mPreviousPhone = Value
        End Set
    End Property

    Public Property PreviousMobile() As String
        Get
            Return mPreviousMobile
        End Get
        Set(ByVal Value As String)
            mPreviousMobile = Value
        End Set
    End Property


    Public Property PreviousCost1() As Object
        Get
            Return mPreviousCost1
        End Get
        Set(ByVal value As Object)
            mPreviousCost1 = value
        End Set
    End Property
    Public Property PreviousCost2() As Object
        Get
            Return mPreviousCost2
        End Get
        Set(ByVal value As Object)
            mPreviousCost2 = value
        End Set
    End Property
    Public Property PreviousCost3() As Object
        Get
            Return mPreviousCost3
        End Get
        Set(ByVal value As Object)
            mPreviousCost3 = value
        End Set
    End Property
    Public Property PreviousCost4() As Object
        Get
            Return mPreviousCost4
        End Get
        Set(ByVal value As Object)
            mPreviousCost4 = value
        End Set
    End Property

    Public Property PreviousLocationID() As Object
        Get
            Return mPreviousLocationID
        End Get
        Set(ByVal value As Object)
            mPreviousLocationID = value
        End Set
    End Property



    Public Property PreviousWorkE_Mail() As String
        Get
            Return mPreviousWorkE_Mail
        End Get
        Set(ByVal value As String)
            mPreviousWorkE_Mail = value
        End Set
    End Property
    Public Property PreviousPassPortNo() As String
        Get
            Return mPreviousPassPortNo
        End Get
        Set(ByVal value As String)
            mPreviousPassPortNo = value
        End Set
    End Property

    Public Property PreviousContractType() As Object
        Get
            Return mPreviousContractType
        End Get
        Set(ByVal value As Object)
            mPreviousContractType = value
        End Set
    End Property

    Public Property PreviousProfessions() As Object
        Get
            Return mPreviousProfessions
        End Get
        Set(ByVal value As Object)
            mPreviousProfessions = value
        End Set
    End Property
    Public Property PreviousPosition() As Object
        Get
            Return mPreviousPosition
        End Get
        Set(ByVal value As Object)
            mPreviousPosition = value
        End Set
    End Property
    Public Property PreviousEmployeeClass() As Object
        Get
            Return mPreviousEmployeeClass
        End Get
        Set(ByVal value As Object)
            mPreviousEmployeeClass = value
        End Set
    End Property
    Public Property PreviousLastEducations() As Object
        Get
            Return mPreviousLastEducations
        End Get
        Set(ByVal value As Object)
            mPreviousLastEducations = value
        End Set
    End Property
    Public Property PreviousGraduationDate() As Object
        Get
            Return mPreviousGraduationDate
        End Get
        Set(ByVal value As Object)
            mPreviousGraduationDate = value
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

#End Region

#Region "Public Function"
    '==================================================================
    'Created by : [0258]
    'Date : 12/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from hrs_EmployeesDecisions table where filter and canceldate = null  
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
                orderByStr = " Order By ID "
            End If
            Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
            '==================== Order By Modification [ End ]
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")

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
    Public Function Find1(ByVal Filter As String) As Boolean
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
    Public Function FindNOCompany(ByVal Filter As String) As Boolean
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
    'Date : 12/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from hrs_EmployeesDecisions where filter 
    '       2-Check if ID > 0 this mean that row is already exist in hrs_EmployeesDecisions  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in hrs_EmployeesDecisions  table
    '==================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_EmployeesDecisions Where " & Filter
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
    'Date : 12/07/2007

    'Description: Save New Row in hrs_EmployeesDecisions  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in hrs_EmployeesDecisions  table

    '==================================================================
    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, OperationType.Save)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteScalar()
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
    'Date : 12/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in hrs_EmployeesDecisions  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in hrs_EmployeesDecisions  table

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
    'Date : 12/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in hrs_EmployeesDecisions  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in hrs_EmployeesDecisions  table

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
    'Date : 12/07/2007
    'Description: Clear all private members  of the class

    '==================================================================
    Public Function Clear() As Boolean
        Try
            ID = 0
            EmpId = 0
            Code = 0
            MaritalStatusID = 0
            BankID = 0
            NationalityID = 0
            BankAccountNumber = String.Empty
            DepartmentID = 0
            CompanyID = 0
            Remarks = String.Empty
            BranchID = 0
            SponsorID = 0
            E_Mail = String.Empty
            Phone = String.Empty
            Mobile = String.Empty
            ManagerID = 0
            SectorID = 0
            PassPortNo = String.Empty
            SSnNo = String.Empty
            PreviousSSnNo = String.Empty
            Cost1 = 0
            Cost2 = 0
            Cost3 = 0
            Cost4 = 0
            LocationID = 0
            WorkE_Mail = String.Empty
            ContractType = 0
            Professions = 0
            Position = 0
            EmployeeClass = 0
            LastEducations = 0
            GraduationDate = Nothing
            PreviousMaritalStatusID = 0
            PreviousBankID = 0
            PreviousNationalityID = 0
            PreviousBankAccountNumber = String.Empty
            PreviousDepartmentID = 0
            PreviousCompanyID = 0
            PreviousRemarks = String.Empty
            PreviousBranchID = 0
            PreviousSponsorID = 0
            PreviousE_Mail = String.Empty
            PreviousPhone = String.Empty
            PreviousMobile = String.Empty
            PreviousManagerID = 0
            PreviousSectorID = 0
            PreviousPassPortNo = String.Empty
            PreviousCost1 = 0
            PreviousCost2 = 0
            PreviousCost3 = 0
            PreviousCost4 = 0
            PreviousLocationID = 0
            PreviousWorkE_Mail = String.Empty
            PreviousContractType = 0
            PreviousProfessions = 0
            PreviousPosition = 0
            PreviousEmployeeClass = 0
            PreviousLastEducations = 0
            PreviousGraduationDate = Nothing
            RegUserID = 0

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 12/07/2007
    'Description:  find first row in hrs_EmployeesDecisions table
    'Steps: 
    '       1-execute sqlstatment to find first row in hrs_EmployeesDecisions  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & FilterBranches
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand & " Order By Case When IsNumeric(Code) = 1 then Right(Replicate('0',51) + Code, 50) When IsNumeric(Code) = 0 then Left(Code + Replicate('',51), 50) Else Code End ASC", mConnectionString)
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
    'Date : 12/07/2007
    'Description:  find Last row in hrs_EmployeesDecisions  table
    'Steps: 
    '       1-execute sqlstatment to find last row in hrs_EmployeesDecisions  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  " & FilterBranches
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand & " Order By Case When IsNumeric(Code) = 1 then Right(Replicate('0',51) + Code, 50) When IsNumeric(Code) = 0 then Left(Code + Replicate('',51), 50) Else Code End DESC", mConnectionString)
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
    'Date : 12/07/2007
    'Description:  find Next row in hrs_EmployeesDecisions  table
    'Steps: 
    '       1-execute sqlstatment to find Next row in hrs_EmployeesDecisions  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================


    '==================================================================
    'Created by : [0258]
    'Date : 12/07/2007
    'Description:  find previous row in hrs_EmployeesDecisions  table
    'Steps: 
    '       1-execute sqlstatment to find previous row in hrs_EmployeesDecisions  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

    '==================================================================
    'Created by  : [0261]
    'Date        : 15-05-2008
    'Description : find first row in hrs_EmployeesDecisions table
    'Steps       : 
    '              1-execute sqlstatment to find first row in hrs_EmployeesDecisions  table
    '              2-Fill dataset with result of sqlstatment
    '              3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function FirstRecord(ByVal Filter As String, ByVal oDate As Date) As Boolean
        Dim StrSelectCommand As String = String.Empty
        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find(" 1=1 ")
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        '------------------------------=============-----------------------------------------
        Try
            StrSelectCommand = CONFIG_DATEFORMAT & " Select TOP 1 * From hrs_EmployeesDecisions inner join hrs_contracts On hrs_EmployeesDecisions.Id = hrs_contracts.EmployeeId Where hrs_EmployeesDecisions.canceldate is null " & FilterBranches & "  And hrs_contracts.CancelDate is Null And ((hrs_contracts.StartDate  <= '" & Format(oDate, "dd/MM/yyyy") & "'And hrs_contracts.enddate is null) Or( '" & Format(oDate, "dd/MM/yyyy") & "' Between hrs_Contracts.StartDate and hrs_Contracts.EndDate))  "
            StrSelectCommand &= "Order By Case When IsNumeric(hrs_EmployeesDecisions.Code) = 1 then Right(Replicate('0',51) + hrs_EmployeesDecisions.Code, 50) When IsNumeric(hrs_EmployeesDecisions.Code) = 0 then Left(hrs_EmployeesDecisions.Code + Replicate('',51), 50) Else hrs_EmployeesDecisions.Code End ASC "
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
    'Created by  : [0261]
    'Date        : 15-05-2008
    'Description : find Last row in hrs_EmployeesDecisions  table
    'Steps       : 
    '              1-execute sqlstatment to find last row in hrs_EmployeesDecisions  table
    '              2-Fill dataset with result of sqlstatment
    '              3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function LastRecord(ByVal Filter As String, ByVal oDate As Date) As Boolean
        Dim StrSelectCommand As String = String.Empty
        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find(" 1=1 ")
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        '------------------------------=============-----------------------------------------
        Try
            StrSelectCommand = CONFIG_DATEFORMAT & " Select TOP 1 * From hrs_EmployeesDecisions inner join hrs_contracts On hrs_EmployeesDecisions.Id = hrs_contracts.EmployeeId Where hrs_EmployeesDecisions.canceldate is null " & FilterBranches & "  And hrs_contracts.CancelDate is Null And ((hrs_contracts.StartDate  <= '" & Format(oDate, "dd/MM/yyyy") & "'And hrs_contracts.enddate is null) Or( '" & Format(oDate, "dd/MM/yyyy") & "' Between hrs_Contracts.StartDate and hrs_Contracts.EndDate))  "
            StrSelectCommand &= "Order By Case When IsNumeric(hrs_EmployeesDecisions.Code) = 1 then Right(Replicate('0',51) + hrs_EmployeesDecisions.Code, 50) When IsNumeric(hrs_EmployeesDecisions.Code) = 0 then Left(hrs_EmployeesDecisions.Code + Replicate('',51), 50) Else hrs_EmployeesDecisions.Code End DESC "
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
    'Created by  : [0261]
    'Date        : 15-05-2008
    'Description : find Next row in hrs_EmployeesDecisions  table
    'Steps       : 
    '              1-execute sqlstatment to find Next row in hrs_EmployeesDecisions  table
    '              2-Fill dataset with result of sqlstatment
    '              3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function NextRecord(ByVal Filter As String, ByVal oDate As Date) As Boolean
        Dim StrSelectCommand As String = String.Empty
        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find(" 1=1 ")
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        '------------------------------=============-----------------------------------------
        Try
            StrSelectCommand = CONFIG_DATEFORMAT & " Select TOP 1 * From hrs_EmployeesDecisions inner join hrs_contracts On hrs_EmployeesDecisions.Id = hrs_contracts.EmployeeId Where hrs_EmployeesDecisions.canceldate is null " & FilterBranches & "  And hrs_contracts.CancelDate is Null And ((hrs_contracts.StartDate  <= '" & Format(oDate, "dd/MM/yyyy") & "'And hrs_contracts.enddate is null) Or( '" & Format(oDate, "dd/MM/yyyy") & "' Between hrs_Contracts.StartDate and hrs_Contracts.EndDate))  "
            StrSelectCommand &= Filter & " Order By Case When IsNumeric(hrs_EmployeesDecisions.Code) = 1 then Right(Replicate('0',51) + hrs_EmployeesDecisions.Code, 50) When IsNumeric(hrs_EmployeesDecisions.Code) = 0 then Left(hrs_EmployeesDecisions.Code + Replicate('',51), 50) Else hrs_EmployeesDecisions.Code End ASC "
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
    'Created by  : [0261]
    'Date        : 15-05-2008
    'Description : find previous row in hrs_EmployeesDecisions  table
    'Steps       : 
    '              1-execute sqlstatment to find previous row in hrs_EmployeesDecisions  table
    '              2-Fill dataset with result of sqlstatment
    '              3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function previousRecord(ByVal Filter As String, ByVal oDate As Date) As Boolean
        Dim StrSelectCommand As String = String.Empty
        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find(" 1=1 ")
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        '------------------------------=============-----------------------------------------
        Try
            StrSelectCommand = CONFIG_DATEFORMAT & " Select TOP 1 * From hrs_EmployeesDecisions inner join hrs_contracts On hrs_EmployeesDecisions.Id = hrs_contracts.EmployeeId Where hrs_EmployeesDecisions.canceldate is null " & FilterBranches & " And hrs_contracts.CancelDate is Null And ((hrs_contracts.StartDate  <= '" & Format(oDate, "dd/MM/yyyy") & "'And hrs_contracts.enddate is null) Or( '" & Format(oDate, "dd/MM/yyyy") & "' Between hrs_Contracts.StartDate and hrs_Contracts.EndDate))  "
            StrSelectCommand &= Filter & " Order By Case When IsNumeric(hrs_EmployeesDecisions.Code) = 1 then Right(Replicate('0',51) + hrs_EmployeesDecisions.Code, 50) When IsNumeric(hrs_EmployeesDecisions.Code) = 0 then Left(hrs_EmployeesDecisions.Code + Replicate('',51), 50) Else hrs_EmployeesDecisions.Code End DESC"
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

    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal cTable As String, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            'StrSelectCommand = "Select * From " & cTable & " " & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & cTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName ", "  Where IsNull(CancelDate,'')=''  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & cTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & " Order By EngName ")
            StrSelectCommand = "Select * From " & cTable & " " & " Where IsNull(CancelDate,'')='' " & " Order By EngName "
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")

                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار]")

                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow("Code"), SqlDbType.VarChar) & " - " &
                mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                '-------------------------------0257 MODIFIED-----------------------------------------
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow("Code"), SqlDbType.VarChar) & " - " &
                    mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                '-------------------------------=============-----------------------------------------

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

    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By Code", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Order By Code ")
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
                Item.Text = mDataHandler.DataValue(ObjDataRow("Code"), SqlDbType.VarChar) & " - " &
                mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "FullName/FullName")), SqlDbType.VarChar)
                '-------------------------------0257 MODIFIED-----------------------------------------
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow("Code"), SqlDbType.VarChar) & " - " &
                    mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "FullName/FullName")), SqlDbType.VarChar)
                End If
                '-------------------------------=============-----------------------------------------

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

    Public Function FindByTable(ByVal cTable As String, ByVal cFilter As String, ByRef ds As DataSet) As Boolean
        Try
            Dim StrSelectCommand As String = " Select * From " & cTable & IIf(cFilter.Length > 0, " Where " & cFilter & " And CancelDate Is NULL", " Where CancelDate Is NULL")
            ds = New DataSet
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(ds)
            If mDataHandler.CheckValidDataObject(ds) Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region

#Region "Class Private Function"
    '===================================================================
    'Created by : [0258]
    'Date : 12/07/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class

    '===================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean

        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)


                mSponsorID = mDataHandler.DataValue_Out(.Item("SponsorID"), SqlDbType.DateTime)
                mGraduationDate = mDataHandler.DataValue_Out(.Item("GraduationDate"), SqlDbType.DateTime)
                mManagerID = mDataHandler.DataValue_Out(.Item("ManagerID"), SqlDbType.DateTime)

                mDepartmentID = mDataHandler.DataValue_Out(.Item("DepartmentID"), SqlDbType.Int, True)
                mMaritalStatusID = mDataHandler.DataValue_Out(.Item("MaritalStatusID"), SqlDbType.Int, True)
                mCompanyID = mDataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Char)
                mBranchID = mDataHandler.DataValue_Out(.Item("BranchID"), SqlDbType.Int, True)
                mBankID = mDataHandler.DataValue_Out(.Item("BankID"), SqlDbType.Int, True)
                mNationalityID = mDataHandler.DataValue_Out(.Item("NationalityID"), SqlDbType.Int, True)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)

                mE_Mail = mDataHandler.DataValue_Out(.Item("E_mail"), SqlDbType.VarChar)

                mMobile = mDataHandler.DataValue_Out(.Item("Mobile"), SqlDbType.VarChar)
                mPassPortNo = mDataHandler.DataValue_Out(.Item("PassPortNo"), SqlDbType.VarChar)
                mSSnNo = mDataHandler.DataValue_Out(.Item("SSnNo"), SqlDbType.VarChar)
                mPreviousSSnNo = mDataHandler.DataValue_Out(.Item("PreviousSSnNo"), SqlDbType.VarChar)
                mEmpId = mDataHandler.DataValue_Out(.Item("EmpId"), SqlDbType.Int, True)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.Int, True)
                mBankAccountNumber = mDataHandler.DataValue_Out(.Item("BankAccountNumber"), SqlDbType.VarChar)
                mPhone = mDataHandler.DataValue_Out(.Item("Phone"), SqlDbType.VarChar)
                mSectorID = mDataHandler.DataValue_Out(.Item("SectorID"), SqlDbType.Int, True)
                mCost1 = mDataHandler.DataValue_Out(.Item("Cost1"), SqlDbType.Int, True)
                mCost2 = mDataHandler.DataValue_Out(.Item("Cost2"), SqlDbType.Int, True)
                mCost3 = mDataHandler.DataValue_Out(.Item("Cost3"), SqlDbType.Int, True)
                mCost4 = mDataHandler.DataValue_Out(.Item("Cost4"), SqlDbType.Int, True)
                mLocationID = mDataHandler.DataValue_Out(.Item("LocationID"), SqlDbType.Int, True)
                mContractType = mDataHandler.DataValue_Out(.Item("ContractType"), SqlDbType.Int, True)
                mProfessions = mDataHandler.DataValue_Out(.Item("Professions"), SqlDbType.Int, True)
                mPosition = mDataHandler.DataValue_Out(.Item("Position"), SqlDbType.Int, True)
                mEmployeeClass = mDataHandler.DataValue_Out(.Item("EmployeeClass"), SqlDbType.Int, True)
                mLastEducations = mDataHandler.DataValue_Out(.Item("LastEducations"), SqlDbType.Int, True)
                mWorkE_Mail = mDataHandler.DataValue_Out(.Item("WorkE_Mail"), SqlDbType.VarChar)
                mPreviousSponsorID = mDataHandler.DataValue_Out(.Item("PreviousSponsorID"), SqlDbType.DateTime)
                mPreviousGraduationDate = mDataHandler.DataValue_Out(.Item("PreviousGraduationDate"), SqlDbType.DateTime)
                mPreviousManagerID = mDataHandler.DataValue_Out(.Item("PreviousManagerID"), SqlDbType.DateTime)

                mPreviousDepartmentID = mDataHandler.DataValue_Out(.Item("PreviousDepartmentID"), SqlDbType.Int, True)
                mPreviousMaritalStatusID = mDataHandler.DataValue_Out(.Item("PreviousMaritalStatusID"), SqlDbType.Int, True)
                mPreviousCompanyID = mDataHandler.DataValue_Out(.Item("PreviousCompanyID"), SqlDbType.Char)
                mPreviousBranchID = mDataHandler.DataValue_Out(.Item("PreviousBranchID"), SqlDbType.Int, True)
                mPreviousBankID = mDataHandler.DataValue_Out(.Item("PreviousBankID"), SqlDbType.Int, True)
                mPreviousNationalityID = mDataHandler.DataValue_Out(.Item("PreviousNationalityID"), SqlDbType.Int, True)
                mPreviousRemarks = mDataHandler.DataValue_Out(.Item("PreviousRemarks"), SqlDbType.VarChar)

                mPreviousE_Mail = mDataHandler.DataValue_Out(.Item("PreviousE_mail"), SqlDbType.VarChar)

                mPreviousMobile = mDataHandler.DataValue_Out(.Item("PreviousMobile"), SqlDbType.VarChar)
                mPreviousPassPortNo = mDataHandler.DataValue_Out(.Item("PreviousPassPortNo"), SqlDbType.VarChar)
                mPreviousBankAccountNumber = mDataHandler.DataValue_Out(.Item("PreviousBankAccountNumber"), SqlDbType.VarChar)
                mPreviousPhone = mDataHandler.DataValue_Out(.Item("PreviousPhone"), SqlDbType.VarChar)
                mPreviousSectorID = mDataHandler.DataValue_Out(.Item("PreviousSectorID"), SqlDbType.Int, True)
                mPreviousCost1 = mDataHandler.DataValue_Out(.Item("PreviousCost1"), SqlDbType.Int, True)
                mPreviousCost2 = mDataHandler.DataValue_Out(.Item("PreviousCost2"), SqlDbType.Int, True)
                mPreviousCost3 = mDataHandler.DataValue_Out(.Item("PreviousCost3"), SqlDbType.Int, True)
                mPreviousCost4 = mDataHandler.DataValue_Out(.Item("PreviousCost4"), SqlDbType.Int, True)
                mPreviousLocationID = mDataHandler.DataValue_Out(.Item("PreviousLocationID"), SqlDbType.Int, True)
                mPreviousContractType = mDataHandler.DataValue_Out(.Item("PreviousContractType"), SqlDbType.Int, True)
                mPreviousProfessions = mDataHandler.DataValue_Out(.Item("PreviousProfessions"), SqlDbType.Int, True)
                mPreviousPosition = mDataHandler.DataValue_Out(.Item("PreviousPosition"), SqlDbType.Int, True)
                mPreviousEmployeeClass = mDataHandler.DataValue_Out(.Item("PreviousEmployeeClass"), SqlDbType.Int, True)
                mPreviousLastEducations = mDataHandler.DataValue_Out(.Item("PreviousLastEducations"), SqlDbType.Int, True)
                mPreviousWorkE_Mail = mDataHandler.DataValue_Out(.Item("PreviousWorkE_Mail"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.VarChar)
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
    'Date : 12/07/2007
    'Description:   Make the values of parameter equal values of private member  of the class

    '===================================================================
    Protected Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal operationType As OperationType) As Boolean

        Try

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmpId", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEmpId, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BankAccountNumber", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBankAccountNumber, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@E_Mail", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mE_Mail, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Phone", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPhone, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Mobile", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mMobile, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PassPortNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPassPortNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SSnNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSSnNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousSSnNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPreviousSSnNo, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@WorkE_Mail", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mWorkE_Mail, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GraduationDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mGraduationDate, SqlDbType.DateTime)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaritalStatusID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mMaritalStatusID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BankID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBankID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NationalityID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mNationalityID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DepartmentID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDepartmentID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCompanyID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BranchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBranchID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SponsorID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSponsorID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ManagerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mManagerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SectorID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSectorID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Cost1", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCost1, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Cost2", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCost2, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Cost3", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCost3, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Cost4", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCost4, SqlDbType.Int, True)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LocationID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mLocationID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ContractType", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mContractType, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Professions", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mProfessions, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Position", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPosition, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeClass", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeClass, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LastEducations", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mLastEducations, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousBankAccountNumber", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPreviousBankAccountNumber, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousRemarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPreviousRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousE_Mail", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPreviousE_Mail, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousPhone", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPreviousPhone, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousMobile", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPreviousMobile, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousPassPortNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPreviousPassPortNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousWorkE_Mail", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPreviousWorkE_Mail, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousGraduationDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mPreviousGraduationDate, SqlDbType.DateTime)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousMaritalStatusID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousMaritalStatusID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousBankID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousBankID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousNationalityID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousNationalityID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousDepartmentID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousDepartmentID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousCompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousCompanyID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousBranchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousBranchID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousSponsorID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousSponsorID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousManagerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousManagerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousSectorID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousSectorID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousCost1", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousCost1, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousCost2", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousCost2, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousCost3", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousCost3, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousCost4", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousCost4, SqlDbType.Int, True)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousLocationID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousLocationID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousContractType", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousContractType, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousProfessions", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousProfessions, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousPosition", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousPosition, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousEmployeeClass", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousEmployeeClass, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PreviousLastEducations", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPreviousLastEducations, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '===================================================================
    'Created by : DataOcean
    'Date : 12/07/2007
    'Description:   Return The Full Employee Name (English Name + Father Name + Grand Father Name + Family Name)
    '===================================================================




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



#End Region

End Class
