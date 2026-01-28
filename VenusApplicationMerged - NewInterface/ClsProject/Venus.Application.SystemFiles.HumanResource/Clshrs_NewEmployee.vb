Imports Venus.Application.SystemFiles.System
Public Class Clshrs_NewEmployeeBase
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_NewEmployee "
        'mInsertParameter = " Code,EngName,ArbName,ArbName4S,FamilyEngName,FamilyArbName,FamilyArbName4S,FatherEngName,FatherArbName,FatherArbName4S,GrandEngName,GrandArbName,GrandArbName4S,BirthDate,DepartmentID,BirthCityID,ReligionID,MaritalStatusID,Sex,BloodGroupID,BankID,ProfissionID,BankAccountNumber,CompanyID,Remarks,RegUserID,RegComputerID,GosiNumber,GOSIJoinDate,GOSIExcludeDate,BranchID,SponsorID,E_Mail,WorkE_Mail,Phone,Mobile,ManagerID,MachineCode,JoinDate,SectorID,Cost1,Cost2,Cost3,Cost4,SSNO,PassPortNo,EntryNo,LocationID,WHours,IsProjectRelated,IsSpecialForce,MaxLoanDedution,LedgerCode,HasTaqat,BankAccountType,Hasflexiblesalarydist,paymenttype"
        'mInsertParameterValues = " @Code,@EngName,@ArbName,@ArbName4S,@FamilyEngName,@FamilyArbName,@FamilyArbName4S,@FatherEngName,@FatherArbName,@FatherArbName4S,@GrandEngName,@GrandArbName,@GrandArbName4S,@BirthDate,@DepartmentID,@BirthCityID,@ReligionID,@MaritalStatusID,@Sex,@BloodGroupID,@BankID,@ProfissionID,@BankAccountNumber,@CompanyID,@Remarks,@RegUserID,@RegComputerID,@GosiNumber,@GOSIJoinDate,@GOSIExcludeDate,@BranchID,@SponsorID,@E_Mail,@WorkE_Mail,@Phone,@Mobile,@ManagerID,@MachineCode,@JoinDate,@SectorID,@Cost1,@Cost2,@Cost3,@Cost4,@SSNO,@PassPortNo,@EntryNo,@LocationID,@WHours,@IsProjectRelated,@IsSpecialForce,@MaxLoanDedution,@LedgerCode,@HasTaqat,@BankAccountType,@Hasflexiblesalarydist,@paymenttype"
        mUpdateParameter = " [FirstNameEnglish]=@EngName,[FirstNameArabic]=@ArbName,[FamilyNameEnglish]=@FamilyEngName,[FamilyNameArabic]=@FamilyArbName,[FatherNameEnglish]=@FatherEngName,[FatherNameArabic]=@FatherArbName,[GrandfatherNameEnglish]=@GrandEngName,[GrandfatherNameArabic]=@GrandArbName,BirthDate=@BirthDate,BirthCityID=@BirthCityID,ReligionID=@ReligionID,MaritalStatusID=@MaritalStatusID,GenderID=@Sex,BloodGroupID=@BloodGroupID,BankID=@BankID,NationalityID=@NationalityID,PersonalEmail=@E_Mail,Mobile=@Mobile,RegisterDate=@JoinDate,SSNO=@SSNO,PassPortNo=@PassPortNo "
        mSelectCommand = " Select *," & ArrangeIncomingName(Me.mLangauge) & " As FullName From  " & mTable
        'mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        'mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
        'FilterBranches = "  And BranchID IN(Select BrancheID From sys_CompaniesBranches Where UserID=" & DataBaseUserRelatedID & " And CanView=1) "

        'Dim ClsUsers As New Clssys_Users(Page)
        'ClsUsers.Find("ID=" & DataBaseUserRelatedID)
        'Dim strcommand As String = "select * from Sys_UsersViewDomain where UserCode = '" & ClsUsers.Code & "'"
        'Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(MyBase.ConnectionString, CommandType.Text, strcommand).Tables(0)
        'If (DT.Rows.Count > 0) Then
        '    If Convert.ToString(DT.Rows(0)("Departments")) <> "" Then
        '        FilterBranches = FilterBranches & " And DepartmentID in (" & Convert.ToString(DT.Rows(0)("Departments")) & ")"
        '    End If
        '    If Convert.ToString(DT.Rows(0)("Cost1")) <> "" Then
        '        FilterBranches = FilterBranches & " And Cost1 in (" & Convert.ToString(DT.Rows(0)("Cost1")) & ")"
        '    End If
        '    If Convert.ToString(DT.Rows(0)("Cost2")) <> "" Then
        '        FilterBranches = FilterBranches & " And Cost2 in (" & Convert.ToString(DT.Rows(0)("Cost2")) & ")"
        '    End If
        '    If Convert.ToString(DT.Rows(0)("Cost3")) <> "" Then
        '        FilterBranches = FilterBranches & " And Cost3 in (" & Convert.ToString(DT.Rows(0)("Cost3")) & ")"
        '    End If
        '    If Convert.ToString(DT.Rows(0)("Cost4")) <> "" Then
        '        FilterBranches = FilterBranches & " And Cost4 in (" & Convert.ToString(DT.Rows(0)("Cost4")) & ")"
        '    End If
        'End If
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
    Private mEngName As String
    Private mArbName As String
    Private mFamilyEngName As String
    Private mFamilyArbName As String
    Private mFatherEngName As String
    Private mFatherArbName As String
    Private mGrandEngName As String
    Private mGrandArbName As String
    Private mEnglishName As String
    Private mArabicName As String
    Private mName As String
    Private mBirthDate As Object
    Private mBirthCityID As Object
    Private mReligionID As Object
    Private mMaritalStatusID As Object
    Private mGenderID As Object
    Private mBloodGroupID As Object
    Private mBankID As Object
    Private mNationalityID As Object
    Private mRemarks As String
    Private mRegisterDate As Object
    Private mFullname As String
    Private mPersonalEmail As String
    Private mMobile As String
    Private mSSNO As String
    Private mPassPortNo As String
    Private mIBANNo As String
    Private mLastEducationCertificateID As Object
    Private mGraduationDate As Object

    Private mSSNOIssueDate As Object
    Private mSSNOExpireDate As Object
    Private mPassportIssueDate As Object
    Private mPassportExpireDate As Object
    Private mIsTransfered As Boolean
    Private mTransfereDate As Object



    Private mAddressAsPerContract As String
    Private mProfissionID As Object

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
    Public Property FamilyEngName() As String
        Get
            Return mFamilyEngName
        End Get
        Set(ByVal Value As String)
            mFamilyEngName = Value
        End Set
    End Property
    Public Property FamilyArbName() As String
        Get
            Return mFamilyArbName
        End Get
        Set(ByVal Value As String)
            mFamilyArbName = Value
        End Set
    End Property
    Public Property FatherEngName() As String
        Get
            Return mFatherEngName
        End Get
        Set(ByVal Value As String)
            mFatherEngName = Value
        End Set
    End Property
    Public Property FatherArbName() As String
        Get
            Return mFatherArbName
        End Get
        Set(ByVal Value As String)
            mFatherArbName = Value
        End Set
    End Property
    Public Property GrandEngName() As String
        Get
            Return mGrandEngName
        End Get
        Set(ByVal Value As String)
            mGrandEngName = Value
        End Set
    End Property
    Public Property GrandArbName() As String
        Get
            Return mGrandArbName
        End Get
        Set(ByVal Value As String)
            mGrandArbName = Value
        End Set
    End Property
    Public Property EnglishName() As String
        Get
            Return mEnglishName
        End Get
        Set(ByVal value As String)
            mEnglishName = value
        End Set
    End Property
    Public Property ArabicName() As String
        Get
            Return mArabicName
        End Get
        Set(ByVal value As String)
            mArabicName = value
        End Set
    End Property
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property
    Public Property BirthDate() As Object
        Get
            Return mBirthDate
        End Get
        Set(ByVal Value As Object)
            mBirthDate = Value
        End Set
    End Property
    Public Property BirthCityID() As Object
        Get
            Return mBirthCityID
        End Get
        Set(ByVal Value As Object)
            mBirthCityID = Value
        End Set
    End Property

    Public Property ReligionID() As Object
        Get
            Return mReligionID
        End Get
        Set(ByVal Value As Object)
            mReligionID = Value
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
    Public Property GenderID() As Object
        Get
            Return mGenderID
        End Get
        Set(ByVal Value As Object)
            mGenderID = Value
        End Set
    End Property
    Public Property BloodGroupID() As Object
        Get
            Return mBloodGroupID
        End Get
        Set(ByVal Value As Object)
            mBloodGroupID = Value
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
    Public Property IBANNo() As String
        Get
            Return mIBANNo
        End Get
        Set(ByVal Value As String)
            mIBANNo = Value
        End Set
    End Property


    Public Property ProfissionID() As Object
        Get
            Return mProfissionID
        End Get
        Set(ByVal Value As Object)
            mProfissionID = Value
        End Set
    End Property
    Public Property AddressAsPerContract() As String
        Get
            Return mAddressAsPerContract
        End Get
        Set(ByVal Value As String)
            mAddressAsPerContract = Value
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


    Public Property RegisterDate() As Object
        Get
            Return mRegisterDate
        End Get
        Set(ByVal Value As Object)
            mRegisterDate = Value
        End Set
    End Property
    Public Property TransfereDate() As Object
        Get
            Return mTransfereDate
        End Get
        Set(ByVal Value As Object)
            mTransfereDate = Value
        End Set
    End Property

    'Add Property FullName to ficialite using Employee full name
    Public ReadOnly Property FullName()
        Get

            '-------------------------------0257 MODIFIED-----------------------------------------
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            Dim StrLanguage As String = String.Empty
            WebHandler.GetCookies(mPage, "Lang", StrLanguage)


            If StrLanguage = "ar-EG" Then
                mFullname = ArrangeName(Language.Arbic)
            Else
                mFullname = ArrangeName(Language.English)
            End If

            '-------------------------------=============-----------------------------------------
            Return mFullname

        End Get
    End Property




    Public Property LastEducationCertificateID() As Integer
        Get
            Return mLastEducationCertificateID
        End Get
        Set(ByVal value As Integer)
            mLastEducationCertificateID = value
        End Set
    End Property
    Public Property GraduationDate() As Object
        Get
            Return mGraduationDate
        End Get
        Set(ByVal Value As Object)
            mGraduationDate = Value
        End Set
    End Property

    Public Property SSNOIssueDate() As Object
        Get
            Return mSSNOIssueDate
        End Get
        Set(ByVal Value As Object)
            mSSNOIssueDate = Value
        End Set
    End Property
    Public Property SSNOExpireDate() As Object
        Get
            Return mSSNOExpireDate
        End Get
        Set(ByVal Value As Object)
            mSSNOExpireDate = Value
        End Set
    End Property
    Public Property PassportIssueDate() As Object
        Get
            Return mPassportIssueDate
        End Get
        Set(ByVal Value As Object)
            mPassportIssueDate = Value
        End Set
    End Property
    Public Property PassportExpireDate() As Object
        Get
            Return mPassportExpireDate
        End Get
        Set(ByVal Value As Object)
            mPassportExpireDate = Value
        End Set
    End Property
    Public Property PersonalEmail() As String
        Get
            Return mPersonalEmail
        End Get
        Set(ByVal value As String)
            mPersonalEmail = value
        End Set
    End Property
    Public Property Mobile() As String
        Get
            Return mMobile
        End Get
        Set(ByVal value As String)
            mMobile = value
        End Set
    End Property


    Public Property SSNO() As String
        Get
            Return mSSNO
        End Get
        Set(ByVal value As String)
            mSSNO = value
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

    Public Property IsTransfered() As Boolean
        Get
            Return mIsTransfered
        End Get
        Set(ByVal value As Boolean)
            mIsTransfered = value
        End Set
    End Property




#End Region

#Region "Public Function"
    '==================================================================
    'Created by : [0258]
    'Date : 12/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from hrs_NewEmployee table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================
    Public Function FindRabie(ByVal Filter As String) As Boolean
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
    '       1-Execute sql statment to get ID from hrs_NewEmployee where filter 
    '       2-Check if ID > 0 this mean that row is already exist in hrs_NewEmployee  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in hrs_NewEmployee  table
    '==================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_NewEmployee Where " & Filter
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

    'Description: Save New Row in hrs_NewEmployee  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in hrs_NewEmployee  table

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
    'Description: Update existing Row in hrs_NewEmployee  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in hrs_NewEmployee  table

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
    'Description: Delete existing Row in hrs_NewEmployee  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in hrs_NewEmployee  table

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
            mID = 0
            'mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mFamilyEngName = String.Empty
            mFamilyArbName = String.Empty
            mFatherEngName = String.Empty
            mFatherArbName = String.Empty
            mGrandEngName = String.Empty
            mGrandArbName = String.Empty
            mBirthDate = Nothing
            mBirthCityID = 0
            mReligionID = 0
            mMaritalStatusID = 0
            mGenderID = 0
            mBloodGroupID = 0
            mBankID = 0
            mNationalityID = 0
            mRemarks = String.Empty

            mProfissionID = 0
            mRemarks = String.Empty

            mRegisterDate = Nothing
            mTransfereDate = Nothing
            mFullname = String.Empty
            mGraduationDate = Nothing
            mSSNOIssueDate = Nothing
            mSSNOExpireDate = Nothing
            mPassportIssueDate = Nothing
            mPassportExpireDate = Nothing
            mLastEducationCertificateID = 0
            mMobile = String.Empty
            mPersonalEmail = String.Empty
            mSSNO = String.Empty
            mPassPortNo = String.Empty
            mIBANNo = String.Empty
            AddressAsPerContract = String.Empty
            mIsTransfered = False

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 12/07/2007
    'Description:  find first row in hrs_NewEmployee table
    'Steps: 
    '       1-execute sqlstatment to find first row in hrs_NewEmployee  table
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
    'Description:  find Last row in hrs_NewEmployee  table
    'Steps: 
    '       1-execute sqlstatment to find last row in hrs_NewEmployee  table
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
    'Description:  find Next row in hrs_NewEmployee  table
    'Steps: 
    '       1-execute sqlstatment to find Next row in hrs_NewEmployee  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================


    '==================================================================
    'Created by : [0258]
    'Date : 12/07/2007
    'Description:  find previous row in hrs_NewEmployee  table
    'Steps: 
    '       1-execute sqlstatment to find previous row in hrs_NewEmployee  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

    '==================================================================
    'Created by  : [0261]
    'Date        : 15-05-2008
    'Description : find first row in hrs_NewEmployee table
    'Steps       : 
    '              1-execute sqlstatment to find first row in hrs_NewEmployee  table
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
            StrSelectCommand = CONFIG_DATEFORMAT & " Select TOP 1 * From hrs_NewEmployee inner join hrs_contracts On hrs_NewEmployee.Id = hrs_contracts.EmployeeId Where hrs_NewEmployee.canceldate is null " & FilterBranches & "  And hrs_contracts.CancelDate is Null And ((hrs_contracts.StartDate  <= '" & Format(oDate, "dd/MM/yyyy") & "'And hrs_contracts.enddate is null) Or( '" & Format(oDate, "dd/MM/yyyy") & "' Between hrs_Contracts.StartDate and hrs_Contracts.EndDate))  "
            StrSelectCommand &= "Order By Case When IsNumeric(hrs_NewEmployee.Code) = 1 then Right(Replicate('0',51) + hrs_NewEmployee.Code, 50) When IsNumeric(hrs_NewEmployee.Code) = 0 then Left(hrs_NewEmployee.Code + Replicate('',51), 50) Else hrs_NewEmployee.Code End ASC "
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
    'Description : find Last row in hrs_NewEmployee  table
    'Steps       : 
    '              1-execute sqlstatment to find last row in hrs_NewEmployee  table
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
            StrSelectCommand = CONFIG_DATEFORMAT & " Select TOP 1 * From hrs_NewEmployee inner join hrs_contracts On hrs_NewEmployee.Id = hrs_contracts.EmployeeId Where hrs_NewEmployee.canceldate is null " & FilterBranches & "  And hrs_contracts.CancelDate is Null And ((hrs_contracts.StartDate  <= '" & Format(oDate, "dd/MM/yyyy") & "'And hrs_contracts.enddate is null) Or( '" & Format(oDate, "dd/MM/yyyy") & "' Between hrs_Contracts.StartDate and hrs_Contracts.EndDate))  "
            StrSelectCommand &= "Order By Case When IsNumeric(hrs_NewEmployee.Code) = 1 then Right(Replicate('0',51) + hrs_NewEmployee.Code, 50) When IsNumeric(hrs_NewEmployee.Code) = 0 then Left(hrs_NewEmployee.Code + Replicate('',51), 50) Else hrs_NewEmployee.Code End DESC "
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
    'Description : find Next row in hrs_NewEmployee  table
    'Steps       : 
    '              1-execute sqlstatment to find Next row in hrs_NewEmployee  table
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
            StrSelectCommand = CONFIG_DATEFORMAT & " Select TOP 1 * From hrs_NewEmployee inner join hrs_contracts On hrs_NewEmployee.Id = hrs_contracts.EmployeeId Where hrs_NewEmployee.canceldate is null " & FilterBranches & "  And hrs_contracts.CancelDate is Null And ((hrs_contracts.StartDate  <= '" & Format(oDate, "dd/MM/yyyy") & "'And hrs_contracts.enddate is null) Or( '" & Format(oDate, "dd/MM/yyyy") & "' Between hrs_Contracts.StartDate and hrs_Contracts.EndDate))  "
            StrSelectCommand &= Filter & " Order By Case When IsNumeric(hrs_NewEmployee.Code) = 1 then Right(Replicate('0',51) + hrs_NewEmployee.Code, 50) When IsNumeric(hrs_NewEmployee.Code) = 0 then Left(hrs_NewEmployee.Code + Replicate('',51), 50) Else hrs_NewEmployee.Code End ASC "
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
    'Description : find previous row in hrs_NewEmployee  table
    'Steps       : 
    '              1-execute sqlstatment to find previous row in hrs_NewEmployee  table
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
            StrSelectCommand = CONFIG_DATEFORMAT & " Select TOP 1 * From hrs_NewEmployee inner join hrs_contracts On hrs_NewEmployee.Id = hrs_contracts.EmployeeId Where hrs_NewEmployee.canceldate is null " & FilterBranches & " And hrs_contracts.CancelDate is Null And ((hrs_contracts.StartDate  <= '" & Format(oDate, "dd/MM/yyyy") & "'And hrs_contracts.enddate is null) Or( '" & Format(oDate, "dd/MM/yyyy") & "' Between hrs_Contracts.StartDate and hrs_Contracts.EndDate))  "
            StrSelectCommand &= Filter & " Order By Case When IsNumeric(hrs_NewEmployee.Code) = 1 then Right(Replicate('0',51) + hrs_NewEmployee.Code, 50) When IsNumeric(hrs_NewEmployee.Code) = 0 then Left(hrs_NewEmployee.Code + Replicate('',51), 50) Else hrs_NewEmployee.Code End DESC"
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
                mEngName = mDataHandler.DataValue_Out(.Item("FirstNameEnglish"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("FirstNameArabic"), SqlDbType.VarChar)
                mFamilyEngName = mDataHandler.DataValue_Out(.Item("FamilyNameEnglish"), SqlDbType.VarChar)
                mFamilyArbName = mDataHandler.DataValue_Out(.Item("FamilyNameArabic"), SqlDbType.VarChar)
                mFatherEngName = mDataHandler.DataValue_Out(.Item("FatherNameEnglish"), SqlDbType.VarChar)
                mFatherArbName = mDataHandler.DataValue_Out(.Item("FatherNameArabic"), SqlDbType.VarChar)
                mGrandEngName = mDataHandler.DataValue_Out(.Item("GrandfatherNameEnglish"), SqlDbType.VarChar)
                mGrandArbName = mDataHandler.DataValue_Out(.Item("GrandfatherNameArabic"), SqlDbType.VarChar)
                mBirthDate = mDataHandler.DataValue_Out(.Item("BirthDate"), SqlDbType.DateTime)
                mRegisterDate = mDataHandler.DataValue_Out(.Item("RegisterDate"), SqlDbType.DateTime)
                mGraduationDate = mDataHandler.DataValue_Out(.Item("GraduationDate"), SqlDbType.DateTime)

                mSSNOIssueDate = mDataHandler.DataValue_Out(.Item("SSNOIssueDate"), SqlDbType.DateTime)
                mSSNOExpireDate = mDataHandler.DataValue_Out(.Item("SSNOExpireDate"), SqlDbType.DateTime)
                mPassportIssueDate = mDataHandler.DataValue_Out(.Item("PassportIssueDate"), SqlDbType.DateTime)
                mPassportExpireDate = mDataHandler.DataValue_Out(.Item("PassportExpireDate"), SqlDbType.DateTime)

                mTransfereDate = mDataHandler.DataValue_Out(.Item("TransfereDate"), SqlDbType.DateTime)
                mIBANNo = mDataHandler.DataValue_Out(.Item("IBANNo"), SqlDbType.VarChar)
                mAddressAsPerContract = mDataHandler.DataValue_Out(.Item("AddressAsPerContract"), SqlDbType.VarChar)
                mBirthCityID = mDataHandler.DataValue_Out(.Item("BirthCityID"), SqlDbType.Int, True)
                mReligionID = mDataHandler.DataValue_Out(.Item("ReligionID"), SqlDbType.Int, True)
                mMaritalStatusID = mDataHandler.DataValue_Out(.Item("MaritalStatusID"), SqlDbType.Int, True)
                mGenderID = mDataHandler.DataValue_Out(.Item("GenderID"), SqlDbType.Char)
                mBloodGroupID = mDataHandler.DataValue_Out(.Item("BloodGroupID"), SqlDbType.Int, True)
                mBankID = mDataHandler.DataValue_Out(.Item("BankID"), SqlDbType.Int, True)
                mNationalityID = mDataHandler.DataValue_Out(.Item("NationalityID"), SqlDbType.Int, True)
                mProfissionID = mDataHandler.DataValue_Out(.Item("ProfissionID"), SqlDbType.Int, True)
                mLastEducationCertificateID = mDataHandler.DataValue_Out(.Item("LastEducationCertificateID"), SqlDbType.Int, True)

                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)

                mEnglishName = ArrangeName(Language.English)
                mArabicName = ArrangeName(Language.Arbic)
                mName = ArrangeName(Me.mLangauge)

                mPersonalEmail = mDataHandler.DataValue_Out(.Item("PersonalEmail"), SqlDbType.VarChar)

                mMobile = mDataHandler.DataValue_Out(.Item("Mobile"), SqlDbType.VarChar)

                mSSNO = mDataHandler.DataValue_Out(.Item("SSNo"), SqlDbType.VarChar)
                mPassPortNo = mDataHandler.DataValue_Out(.Item("PassPortNo"), SqlDbType.VarChar)


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

            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FamilyEngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFamilyEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FamilyArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFamilyArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FatherEngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFatherEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FatherArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFatherArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GrandEngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mGrandEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GrandArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mGrandArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BirthDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mBirthDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegisterDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mRegisterDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransfereDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mTransfereDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GraduationDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mGraduationDate, SqlDbType.DateTime)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SSNOIssueDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mSSNOIssueDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SSNOExpireDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mSSNOExpireDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PassportIssueDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mPassportIssueDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PassportExpireDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mPassportExpireDate, SqlDbType.DateTime)


            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IBANNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mIBANNo, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AddressAsPerContract", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mAddressAsPerContract, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BirthCityID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBirthCityID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReligionID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReligionID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaritalStatusID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mMaritalStatusID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GenderID", SqlDbType.Char)).Value = mDataHandler.DataValue_In(mGenderID, SqlDbType.Char)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BloodGroupID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBloodGroupID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BankID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBankID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NationalityID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mNationalityID, SqlDbType.Int, True)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ProfissionID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mProfissionID, SqlDbType.Int, True)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LastEducationCertificateID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mLastEducationCertificateID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PersonalEmail", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPersonalEmail, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Mobile", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mMobile, SqlDbType.VarChar)


            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SSNO", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSSNO, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PassPortNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPassPortNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsTransfered", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsTransfered, SqlDbType.Bit)


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
    Private Function ArrangeName(ByVal prtLanguage As Language) As String
        Dim StrReturn As String = String.Empty
        Dim IntCounter As Integer
        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(mPage, "Lang", StrLanguage)

        If StrLanguage = "ar-EG" Then
            prtLanguage = Language.Arbic
        Else
            prtLanguage = Language.English
        End If
        Dim clsCompany As New Clssys_Companies(mPage)
        clsCompany.Find(" 1=1 ")
        With clsCompany
            Me.mNameArrangment = .EmpFirstName & .EmpNameSeparator &
                                 .EmpSecondName & .EmpNameSeparator &
                                 .EmpThirdName & .EmpNameSeparator &
                                 .EmpFourthName & .EmpNameSeparator
            Me.mNameSeparator = .EmpNameSeparator
        End With
        '-------------------------------=============-----------------------------------------


        Try
            Dim Arr() As String
            '-------------------------------0257 MODIFIED-----------------------------------------

            'Arr = Me.mNameArrangment.Split(",")
            Arr = Me.mNameArrangment.Split(clsCompany.EmpNameSeparator)
            '-------------------------------=============-----------------------------------------

            For IntCounter = 0 To Arr.GetUpperBound(0)
                Select Case Arr(IntCounter)
                    Case "FR"
                        If prtLanguage = Language.English Then
                            StrReturn &= Me.mNameSeparator & Me.EngName
                        ElseIf prtLanguage = Language.Arbic Then
                            StrReturn &= Me.mNameSeparator & Me.ArbName
                        End If
                    Case "FA"
                        If prtLanguage = Language.English Then
                            StrReturn &= Me.mNameSeparator & Me.FatherEngName
                        ElseIf prtLanguage = Language.Arbic Then
                            StrReturn &= Me.mNameSeparator & Me.FatherArbName
                        End If
                    Case "GR"
                        If prtLanguage = Language.English Then
                            StrReturn &= Me.mNameSeparator & Me.mGrandEngName
                        ElseIf prtLanguage = Language.Arbic Then
                            StrReturn &= Me.mNameSeparator & Me.mGrandArbName
                        End If
                    Case "FM"
                        If prtLanguage = Language.English Then
                            StrReturn &= Me.mNameSeparator & Me.mFamilyEngName
                        ElseIf prtLanguage = Language.Arbic Then
                            StrReturn &= Me.mNameSeparator & Me.mFamilyArbName
                        End If
                End Select
            Next
            If Len(StrReturn) = 0 Then
                If prtLanguage = Language.English Then
                    StrReturn &= mEngName & Me.mNameSeparator & mFatherEngName & Me.mNameSeparator & mGrandEngName & Me.mNameSeparator & mFamilyEngName
                ElseIf prtLanguage = Language.Arbic Then
                    StrReturn &= mArbName & Me.mNameSeparator & mFatherArbName & Me.mNameSeparator & mGrandArbName & Me.mNameSeparator & mFamilyArbName
                End If
            Else
                StrReturn = StrReturn.Substring(Len(Me.mNameSeparator))
            End If
            Return StrReturn
        Catch ex As Exception

        End Try
    End Function

    Public Function ArrangeIncomingName(ByVal prtLanguage As Language) As String
        Dim StrReturn As String
        Dim IntCounter As Integer


        '-------------------------------0257 MODIFIED----------------------------------------- 
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(mPage, "Lang", StrLanguage)

        If StrLanguage = "ar-EG" Then
            prtLanguage = Language.Arbic
        Else
            prtLanguage = Language.English
        End If


        Dim clsCompany As New Clssys_Companies(mPage)
        clsCompany.Find(" 1=1 ")
        With clsCompany
            Me.mNameArrangment = .EmpFirstName & .EmpNameSeparator &
                                 .EmpSecondName & .EmpNameSeparator &
                                 .EmpThirdName & .EmpNameSeparator &
                                 .EmpFourthName & .EmpNameSeparator
            Me.mNameSeparator = .EmpNameSeparator
        End With
        '-------------------------------=============-----------------------------------------
        Try
            Dim Arr() As String
            '-------------------------------0257 MODIFIED-----------------------------------------
            'Arr = Me.mNameArrangment.Split(",")
            Arr = Me.mNameArrangment.Split(clsCompany.EmpNameSeparator)
            '-------------------------------=============-----------------------------------------

            For IntCounter = 0 To Arr.GetUpperBound(0)
                Select Case Arr(IntCounter)
                    Case "FR"
                        If prtLanguage = Language.English Then
                            StrReturn &= "+'" & Me.mNameSeparator & "'+IsNull(" & mTable & ".[FirstNameEnglish],'')"
                        ElseIf prtLanguage = Language.Arbic Then
                            StrReturn &= "+'" & Me.mNameSeparator & "'+IsNull(" & mTable & ".[FirstNameArabic],'')"
                        End If
                    Case "FA"
                        If prtLanguage = Language.English Then
                            StrReturn &= "+'" & Me.mNameSeparator & "'+IsNull(" & mTable & ".[FatherNameEnglish],'')"
                        ElseIf prtLanguage = Language.Arbic Then
                            StrReturn &= "+'" & Me.mNameSeparator & "'+IsNull(" & mTable & ".[FatherNameArabic],'')"
                        End If
                    Case "GR"
                        If prtLanguage = Language.English Then
                            StrReturn &= "+'" & Me.mNameSeparator & "'+IsNull(" & mTable & ".[GrandfatherNameEnglish],'')"
                        ElseIf prtLanguage = Language.Arbic Then
                            StrReturn &= "+'" & Me.mNameSeparator & "'+IsNull(" & mTable & ".[GrandfatherNameArabic],'')"
                        End If
                    Case "FM"
                        If prtLanguage = Language.English Then
                            StrReturn &= "+'" & Me.mNameSeparator & "'+IsNull(" & mTable & ".[FamilyNameEnglish],'')"
                        ElseIf prtLanguage = Language.Arbic Then
                            StrReturn &= "+'" & Me.mNameSeparator & "'+IsNull(" & mTable & ".[FamilyNameArabic],'')"
                        End If
                End Select
            Next
            If Len(StrReturn) = 0 Then
                If prtLanguage = Language.English Then
                    StrReturn &= "" & mTable & ".[FirstNameEnglish]+'" & Me.mNameSeparator & "'+" & mTable & ".[FatherNameEnglish]+'" & Me.mNameSeparator & "'+" & mTable & ".[GrandfatherNameEnglish]+'" & Me.mNameSeparator & "'+" & mTable & ".[FamilyNameEnglish]"
                ElseIf prtLanguage = Language.Arbic Then
                    StrReturn &= "" & mTable & ".[FirstNameArabic]+'" & Me.mNameSeparator & "'+" & mTable & ".[FatherNameArabic]+'" & Me.mNameSeparator & "'+" & mTable & ".[GrandfatherNameArabic]+'" & Me.mNameSeparator & "'+" & mTable & ".[FamilyNameArabic]"
                End If
            Else
                StrReturn = StrReturn.Substring(Len(Me.mNameSeparator) + 4)
            End If
            Return StrReturn
        Catch ex As Exception

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

Public Class Clshrs_NewEmployee
    Inherits Clshrs_NewEmployeeBase

    Const StrPaidOnDays As String = "Paid On Days"
    Const StrPaid As String = "Paid"
    Const StrNotPaid As String = "Not Paid"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)

    End Sub
    '==================================================================
    'Created by : [0258]
    'Date : 12/07/2007

    'Description: Save New Row in hrs_NewEmployee  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in hrs_NewEmployee  table
    '       and return the ID to insert the employeephoto into ObjectAttachment Table

    '==================================================================
    Public Function SaveEmployee() As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, OperationType.Save)
            mSqlCommand.Connection.Open()
            Return mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '===========================================
    'Function Name : GetContractsTransactions
    'Date Created  : 25-05-2008
    'Developer     : [0256]
    'Description   : Get All Grades Steps Transactions 
    '===========================================
    Public Function GetNewEmployeeDependantsData(ByVal IntEmployeeId As Integer, ByVal IntObjectId As Integer, ByRef DsTransactions As Data.DataSet) As Boolean
        Try
            DsTransactions = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetNewEmployeeDependantsData", IntEmployeeId, IntObjectId)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '===========================================
    'Function Name : CheckTabsSecuirty
    'Date Created  : 26-05-2008
    'Developer     : [0256]
    'Description   : Get All Grades Steps Transactions 
    '===========================================
    Public Function CheckTabsSecuirty(ByRef blArraySecuirty() As Boolean)
        Dim DS As DataSet

        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetFormsPermissions", Me.DataBaseUserRelatedID, Me.GroupID, "frmContracts")
        If DS.Tables(0).Rows.Count <= 0 Then blArraySecuirty(0) = False

        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetFormsPermissions", Me.DataBaseUserRelatedID, Me.GroupID, "frmEmployeePeriodicalTransactions")
        If DS.Tables(0).Rows.Count <= 0 Then blArraySecuirty(1) = False

        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetFormsPermissions", Me.DataBaseUserRelatedID, Me.GroupID, "frmNewEmployeeDocuments")
        If DS.Tables(0).Rows.Count <= 0 Then blArraySecuirty(2) = False

        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetFormsPermissions", Me.DataBaseUserRelatedID, Me.GroupID, "frmNewEmployeeDependants")
        If DS.Tables(0).Rows.Count <= 0 Then blArraySecuirty(3) = False

    End Function

    '===========================================
    'Function Name : CheckTabsSecuirty
    'Date Created  : 26-05-2008
    'Developer     : [0256]
    'Description   : Get All Grades Steps Transactions 
    '===========================================
    Public Function CheckFormSecuirty(ByVal StrFormName As String, ByRef blArraySecuirty() As Integer)
        Dim DS As DataSet
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetFormsPermissions", Me.DataBaseUserRelatedID, Me.GroupID, StrFormName)
        If DS.Tables(0).Rows.Count > 0 Then
            With DS.Tables(0).Rows(0)
                blArraySecuirty(0) = IIf(.Item("AllowView") = True, 1, 0)
                blArraySecuirty(1) = IIf(.Item("AllowAdd") = True, 1, 0)
                blArraySecuirty(2) = IIf(.Item("AllowEdit") = True, 1, 0)
                blArraySecuirty(3) = IIf(.Item("AllowDelete") = True, 1, 0)
                blArraySecuirty(4) = IIf(.Item("AllowPrint") = True, 1, 0)
            End With
        End If
    End Function
    Public Function GetAllEmployeeValidContract(ByRef DS As Data.DataSet, ByVal IntEmployeeID As Integer) As Boolean ', Optional ByVal IntBranchID As Integer = 0
        Dim strQuery As String = String.Empty
        Try
            strQuery = "Set DateFormat DMY  "
            strQuery &= "Select "
            strQuery &= "hrs_NewEmployee.ID					As ID , "
            strQuery &= "hrs_contracts.ID					As ContractID, "
            strQuery &= "hrs_NewEmployee.Code					As Code,  "
            strQuery &= "hrs_NewEmployee.EngName				As EngName, "
            strQuery &= "hrs_NewEmployee.ArbName				As ArbName, "
            strQuery &= "hrs_contracts.StartDate			As StartDate,   "
            strQuery &= "From    "
            strQuery &= "hrs_NewEmployee  "
            strQuery &= "Inner Join hrs_contracts On hrs_contracts.EmployeeID = hrs_NewEmployee.ID   "
            strQuery &= "Where   "
            strQuery &= "hrs_NewEmployee.CancelDate			Is Null "
            strQuery &= FilterBranches
            strQuery &= "And hrs_contracts.CancelDate		Is Null "
            strQuery &= "and hrs_contracts.ID in (select Max(Dtl.ID) from hrs_contracts as Dtl where Dtl.EmployeeID = hrs_NewEmployee.ID) "
            strQuery &= "and hrs_NewEmployee.ID = " & IntEmployeeID
            'strQuery &= IIf(IntBranchID > 0, "And hrs_NewEmployee.BranchID = " & IntBranchID, "")
            strQuery &= " Order by  "
            strQuery &= "hrs_NewEmployee.ID  "
            DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strQuery)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
            Return False
        End Try
    End Function
    Public Function GetAllEmployeeValidContract(ByRef DS As Data.DataSet, ByVal NewEmployeeIDs As String, ByVal FromDate As Date, ByVal ToDate As Date) As Boolean
        Dim strQuery As String = String.Empty
        Try
            strQuery = "Set DateFormat DMY  "
            strQuery &= "Select "
            strQuery &= "hrs_NewEmployee.ID					As ID , "
            strQuery &= "hrs_contracts.ID					As ContractID, "
            strQuery &= "hrs_contracts.EmployeeClassID		As ClassID, "
            strQuery &= "hrs_NewEmployee.Code					As Code,  "
            strQuery &= "hrs_NewEmployee.EngName				As EngName, "
            strQuery &= "hrs_NewEmployee.ArbName				As ArbName, "
            strQuery &= "hrs_contracts.StartDate			As StartDate,   "
            strQuery &= "hrs_NewEmployee.IsProjectRelated			    As ProjectRelated,"
            strQuery &= "(select top 1 Att2.ProjectID from Att_AttendTransactions Att2 where Att2.EmployeeID = hrs_NewEmployee.ID and CONVERT(Datetime,Att2.TrnsDatetime,103)>= CONVERT(Datetime,'" & FromDate & "',103) and CONVERT(Datetime,Att2.TrnsDatetime,103) <= CONVERT(Datetime,'" & ToDate & "',103) order by Att2.TrnsDatetime DESC)  As CProject, "
            strQuery &= "(select top 1 Att2.RegComputerID from Att_AttendTransactions Att2 where Att2.EmployeeID = hrs_NewEmployee.ID and CONVERT(Datetime,Att2.TrnsDatetime,103)>= CONVERT(Datetime,'" & FromDate & "',103) and CONVERT(Datetime,Att2.TrnsDatetime,103) <= CONVERT(Datetime,'" & ToDate & "',103) order by Att2.TrnsDatetime DESC)  As CRef  "
            strQuery &= "From    "
            strQuery &= "hrs_NewEmployee  "
            strQuery &= "Inner Join hrs_contracts On hrs_contracts.EmployeeID = hrs_NewEmployee.ID   "
            strQuery &= "Where   "
            strQuery &= "hrs_NewEmployee.CancelDate			Is Null "
            strQuery &= FilterBranches
            strQuery &= "And hrs_contracts.CancelDate		Is Null "
            strQuery &= "and hrs_contracts.ID in (select top 1 Dtl.ID from hrs_contracts as Dtl where Dtl.EmployeeID = hrs_NewEmployee.ID and Dtl.StartDate <= '" & Format(ToDate, "dd/MM/yyyy") & "' And (Dtl.enddate is null or Dtl.EndDate >= '" & Format(FromDate, "dd/MM/yyyy") & "')  order by StartDate ASC) "
            strQuery &= "and hrs_NewEmployee.ID in (" & NewEmployeeIDs & ")"
            strQuery &= " Order by  "
            strQuery &= "hrs_NewEmployee.ID  "
            DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strQuery)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
            Return False
        End Try
    End Function
    Public Function SetVacationTransaction(ByVal IntEmployeeID As Integer, ByVal dteStartDate As Date, ByVal dteEndDate As Date) As Boolean
        Dim clsEndofServices As New Clshrs_EndOfServices(mPage)
        Dim clsEmployeeTransactions As New Clshrs_EmployeesTransactions(mPage)
        Dim clsFiscalPeriod As New Clssys_FiscalYearsPeriods(mPage)
        Const CSReqMonths As Integer = 330
        Const CSDurationDays As Integer = 30
        Dim dblVactor As Double
        Dim dblDiffDaye As Single
        Dim dblFinancialWorkingUnits As Double
        Dim tempResult As Double
        Dim intVaraction As Integer
        Dim intNoOfMonths As Integer
        Dim intNoOfDays As Double
        Dim dteActualEndDate As Date
        Try
            dblVactor = CSDurationDays / CSReqMonths
            dblDiffDaye = clsEndofServices.GetDateDiffrance(dteStartDate, dteEndDate)
            dblFinancialWorkingUnits = (dblDiffDaye * dblVactor) - 30
            tempResult = dblFinancialWorkingUnits * (330 / 30)

            intNoOfMonths = tempResult \ 30
            intNoOfDays = ((tempResult / 30) - (tempResult \ 30)) * 30
            intNoOfDays = Math.Round(intNoOfDays, 5)
            dteActualEndDate = dteStartDate.AddMonths(intNoOfMonths)
            dteActualEndDate = dteStartDate.AddDays(intNoOfDays)

            clsFiscalPeriod.Find("'" & dteActualEndDate & "' BETWEEN FromDate  AND ToDate ")
            If clsFiscalPeriod.ID > 0 Then
                clsEmployeeTransactions.FiscalYearPeriodID = clsFiscalPeriod.ID
            Else
                clsFiscalPeriod.Find(" sys_FiscalYearsPeriods.CancelDate IS NULL ")
                clsEmployeeTransactions.FiscalYearPeriodID = clsFiscalPeriod.ID
            End If

            clsEmployeeTransactions.EmployeeID = IntEmployeeID
            clsEmployeeTransactions.PaidDate = dteActualEndDate
            clsEmployeeTransactions.FinancialWorkingUnits = dblFinancialWorkingUnits
            clsEmployeeTransactions.Save()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    '===========================================
    'Function Name : CheckTabsSecuirty
    'Date Created  : 03-11-2008
    'Developer     : DataOcean
    'Description   : check employee if in the first month of the contract 
    '===========================================
    Public Function CheckBeginOfContract() As Boolean
        Dim ClsContracts As New Clshrs_Contracts(mPage)
        Dim IntContractID As Integer = 0
        Dim IntNoofMonths As Integer = 0
        Try
            ClsContracts.ContractValidatoinId(Me.ID, Date.Now)
            IntNoofMonths = DateDiff(DateInterval.Month, ClsContracts.StartDate, Date.Now)
            If IntNoofMonths < 1 Then Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '===========================================
    'Function Name : CreateDataTable
    'Date Created  : 07-07-2012
    'Developer     : Abdulrahman
    'Description   : Create cata table for transaction and add columes
    '===========================================
    Private Function CreateDataTable(ByRef DtTable As Data.DataTable, ByVal PtrTableName As String) As Boolean

        Dim ObjDataColumn As New Data.DataColumn
        Try
            DtTable = New DataTable(PtrTableName)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "TransactionTypeID"
            ObjDataColumn.DataType = Type.GetType("System.Int32")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Amount"
            ObjDataColumn.DataType = Type.GetType("System.Double")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Description"
            ObjDataColumn.DataType = Type.GetType("System.String")
            DtTable.Columns.Add(ObjDataColumn)

            'B#003 [0256]
            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "EmpSchID"
            ObjDataColumn.DataType = Type.GetType("System.Int32")
            DtTable.Columns.Add(ObjDataColumn)

            '#0001 [Abdulrahman] [07/07/2012]
            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "DescriptionSign"
            ObjDataColumn.DataType = Type.GetType("System.String")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "OrgAmount"
            ObjDataColumn.DataType = Type.GetType("System.Double")
            DtTable.Columns.Add(ObjDataColumn)

        Catch ex As Exception

        End Try
    End Function

    '===========================================
    'Function Name : CollectEmployeesTransactions
    'Date Created  : 03-11-2008
    'Developer     : DataOcean
    'Description   : GetAll the desirved Transaction For this employee
    '              :
    'Modified      : [Abdulrahman] [07/07/2012]
    '===========================================
    Public Function CollectEmployeesTransactions(ByVal SalarySrc As String, ByVal ToDate As Date, ByVal IntEmployeeId As Integer, ByVal IntFiscalYearPeriod As Integer,
    ByRef PtrDtBenefits As Data.DataTable, ByRef PtrDtDeductions As Data.DataTable, ByRef dblBenifets As Double, ByRef dbldeduction As Double,
    ByVal ProjectWorkingUnits As Double, ByVal TotalProjectsWorkingUnits As Double,
    Optional ByVal NoOfWorkingUnits As Double = 0, Optional ByVal OvertimeWorkHours As Double = 0, Optional ByVal HolidayWorkHours As Double = 0,
    Optional ByVal SalaryPerHour As Double = 0, Optional ByVal SalaryPerDay As Double = 0, Optional ByVal NonPermit As Double = 0, Optional ByVal AbsentValues As Double = 0, Optional ByVal PrepareType As ePrepareType = ePrepareType.Normal,
    Optional ByVal PrepareStage As ePrepareStage = ePrepareStage.Normal, Optional ByVal Act As Double = 0, Optional ByVal IsToAttend As Integer = 0, Optional ToAttendEndDate As Date = Nothing) As Boolean

        Dim FormulaExpression As String
        Dim ClsContractTransactions As New Clshrs_ContractsTransactions(mPage)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(mPage)
        Dim ClsGradeSteps As New Clshrs_GradesStepsTransactions(mPage)
        Dim ClsTransactionsTypes As New Clshrs_TransactionsTypes(mPage)
        Dim ClsSolver As New Clshrs_FormulaSolver(ClsTransactionsTypes.ConnectionString, mPage)
        Dim ClsContracts As New Clshrs_Contracts(mPage)
        Dim ClsEmployeesClasses As New Clshrs_EmployeeClasses(mPage)
        Dim IntContractID As Integer = 0
        Dim ObjDatahandler As New Venus.Shared.DataHandler
        Dim ObjdtRow As Data.DataRow
        Dim DblAmount As Double = 0
        Dim IntTransactionTypeID As Integer
        Dim IntNoOfDaysPerPeriod As Decimal = 0
        Dim IntPeriodWorkingUint As Double = 0
        Dim StrPrepareStage As String

        dblBenifets = 0
        dbldeduction = 0
        Try

            CreateDataTable(PtrDtBenefits, "Benefits")
            CreateDataTable(PtrDtDeductions, "Deductions")

            Select Case PrepareStage
                Case ePrepareStage.Normal
                    StrPrepareStage = " And  PaidAtVacation <> 2 "
                Case ePrepareStage.Vacation
                    StrPrepareStage = " And (PaidAtVacation = 1 or PaidAtVacation = 2)"
                Case ePrepareStage.VacationCost
                    StrPrepareStage = " And (PaidAtVacation = 1 or PaidAtVacation = 2) And TransactionTypeID not in (select ID from hrs_TransactionsTypes where TransactionGroupID = 3)"
            End Select

            '1-Get Valid Contract ID 
            IntContractID = ClsContracts.ContractValidatoinId(IntEmployeeId, IntFiscalYearPeriod)
            ClsEmployeesClasses.Find("id=" & ClsContracts.EmployeeClassID)

            If IntContractID = 0 Then
                Return False
            End If

            '2-Assign Working days and no. of days per period 
            If ClsEmployeesClasses.WorkingUnitsIsHours Then
                IntNoOfDaysPerPeriod = ClsEmployeesClasses.NoOfDaysPerPeriod * ClsEmployeesClasses.WorkHoursPerDay
            Else
                IntNoOfDaysPerPeriod = ClsEmployeesClasses.NoOfDaysPerPeriod
            End If

            If PrepareStage = ePrepareStage.Vacation Then
                If ClsEmployeesClasses.NoOfDaysPerPeriod = 0 Then
                    Dim clscompany As New Clssys_Companies(mPage)
                    clscompany.Find(" 1=1 ")
                    If clscompany.IncludeAbsencDays = True Then
                        IntNoOfDaysPerPeriod = 30
                    Else
                        If clscompany.IsHigry Then
                            IntNoOfDaysPerPeriod = 354 / 12
                        Else
                            IntNoOfDaysPerPeriod = 360 / 12
                        End If
                    End If
                End If
            End If
            IntPeriodWorkingUint = NoOfWorkingUnits

            If ClsContractTransactions.Find(IntContractID, ToDate, " Active = 1 and TransactionTypeID not in (select ID from hrs_TransactionsTypes where isnull(IsProjectRelatedItem,0) = 1)" & StrPrepareStage) Then
                For Each row As Data.DataRow In ClsContractTransactions.DataSet.Tables(0).Rows

                    'Check Once At Period  
                    '===================== [Begin]
                    If CBool(row.Item("OnceAtPeriod")) = True Then
                        'Get No Of Times This Transaction Employee Earned 
                        If ClsEmployeesTransactions.GetPaidTransactionsNosPerPeriod(IntEmployeeId, CInt(row.Item(2)), IntFiscalYearPeriod) >= 1 Then
                            Continue For
                        End If
                    End If

                    'Check Transaction Period 
                    If Not ClsContractTransactions.ValidTransaction(IntEmployeeId, row.Item(2), ToDate, IntFiscalYearPeriod) Then
                        Continue For
                    End If

                    'Check If Transaction Type being Caculated Has Formula or Not And Calculate It  
                    ClsTransactionsTypes.Find("ID=" & row.Item(2))
                    'check execluded salary items
                    If ClsTransactionsTypes.IsSalaryEOSExeclude = True And PrepareStage = ePrepareStage.Normal And PrepareType = ePrepareType.EndOfContract Then
                        Continue For
                    End If

                    Select Case PrepareType
                        Case ePrepareType.BeginOfContract
                            FormulaExpression = ClsTransactionsTypes.BeginContractFormula
                        Case ePrepareType.EndOfContract
                            FormulaExpression = ClsTransactionsTypes.EndContractFormula
                        Case ePrepareType.Normal
                            FormulaExpression = ClsTransactionsTypes.Formula
                    End Select

                    If FormulaExpression = String.Empty Then
                        DblAmount = ObjDatahandler.DataValue_Out(row.Item(3), Data.SqlDbType.Money)
                    Else
                        ClsSolver = New Clshrs_FormulaSolver(ClsTransactionsTypes.ConnectionString, mPage)
                        ClsSolver.EmployeeID = IntEmployeeId
                        ClsSolver.FiscalPeriodID = IntFiscalYearPeriod
                        ClsSolver.OverTimePerPeriod = OvertimeWorkHours
                        ClsSolver.HolidayHoursperPeriod = HolidayWorkHours
                        ClsSolver.SalaryPricePerHour = SalaryPerHour
                        ClsSolver.SalaryPricePerDay = SalaryPerDay
                        ClsSolver.NoOfDaysPerPeriod = IntNoOfDaysPerPeriod
                        ClsSolver.NoOfWorkingDays = IntPeriodWorkingUint
                        ClsSolver.ProjectWorkingUnits = ProjectWorkingUnits
                        ClsSolver.IsToAttend = IsToAttend
                        ClsSolver.ToAttenddate = ToAttendEndDate
                        ClsSolver.BolBeginOfContract = False
                        Select Case PrepareType
                            Case ePrepareType.BeginOfContract
                                ClsSolver.BolBeginOfContract = True
                        End Select
                        ClsSolver.Executedate = ToDate
                        ClsSolver.TotalProjectsWorkingUnits = TotalProjectsWorkingUnits
                        ClsSolver.SalarySrc = SalarySrc
                        ClsSolver.EvaluateExpression(FormulaExpression)
                        DblAmount = IIf(IsNumeric(ClsSolver.Output), ClsSolver.Output, 2)
                    End If
                    DblAmount = Math.Round(DblAmount, 2)
                    'Create Rows And Assign Values To it 
                    If ClsTransactionsTypes.Sign > 0 Then
                        ObjdtRow = PtrDtBenefits.NewRow()
                        ObjdtRow(0) = ClsTransactionsTypes.ID
                        ObjdtRow(1) = DblAmount
                        If ClsTransactionsTypes.IsPaid Then
                            If ClsSolver.WorkOfDays Then
                                ObjdtRow(2) = StrPaidOnDays
                                ObjdtRow(4) = "Paid By Days"
                                ClsSolver.WorkOfDays = False
                            Else
                                ObjdtRow(2) = StrPaid
                                ObjdtRow(4) = "Paid"
                            End If

                            dblBenifets += DblAmount
                        Else
                            ObjdtRow(2) = StrNotPaid
                            ObjdtRow(4) = "Not Paid"
                        End If
                        PtrDtBenefits.Rows.Add(ObjdtRow)
                    Else
                        ObjdtRow = PtrDtDeductions.NewRow()
                        ObjdtRow(0) = ClsTransactionsTypes.ID
                        ObjdtRow(1) = DblAmount
                        If ClsTransactionsTypes.IsPaid Then
                            ObjdtRow(2) = StrPaid
                            ObjdtRow(4) = "Paid"
                            dbldeduction += DblAmount
                        Else
                            ObjdtRow(2) = StrNotPaid
                            ObjdtRow(4) = "Not Paid"
                        End If
                        PtrDtDeductions.Rows.Add(ObjdtRow)
                    End If
                Next
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function CollectEmployeesTransactionsForSettlement(ByVal SalarySrc As String, ByVal ToDate As Date, ByVal IntEmployeeId As Integer, ByVal IntFiscalYearPeriod As Integer,
    ByRef PtrDtBenefits As Data.DataTable, ByRef PtrDtDeductions As Data.DataTable, ByRef dblBenifets As Double, ByRef dbldeduction As Double,
    ByVal ProjectWorkingUnits As Double, ByVal TotalProjectsWorkingUnits As Double,
    Optional ByVal NoOfWorkingUnits As Double = 0, Optional ByVal OvertimeWorkHours As Double = 0, Optional ByVal HolidayWorkHours As Double = 0,
    Optional ByVal SalaryPerHour As Double = 0, Optional ByVal SalaryPerDay As Double = 0, Optional ByVal NonPermit As Double = 0, Optional ByVal AbsentValues As Double = 0, Optional ByVal PrepareType As ePrepareType = ePrepareType.Normal,
    Optional ByVal PrepareStage As ePrepareStage = ePrepareStage.Normal, Optional ByVal Act As Double = 0, Optional ByVal IsToAttend As Integer = 0, Optional ToAttendEndDate As Date = Nothing) As Boolean

        Dim FormulaExpression As String
        Dim ClsContractTransactions As New Clshrs_ContractsTransactions(mPage)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(mPage)
        Dim ClsGradeSteps As New Clshrs_GradesStepsTransactions(mPage)
        Dim ClsTransactionsTypes As New Clshrs_TransactionsTypes(mPage)
        Dim ClsSolver As New Clshrs_FormulaSolver(ClsTransactionsTypes.ConnectionString, mPage)
        Dim ClsContracts As New Clshrs_Contracts(mPage)
        Dim ClsEmployeesClasses As New Clshrs_EmployeeClasses(mPage)
        Dim IntContractID As Integer = 0
        Dim ObjDatahandler As New Venus.Shared.DataHandler
        Dim ObjdtRow As Data.DataRow
        Dim DblAmount As Double = 0
        Dim IntTransactionTypeID As Integer
        Dim IntNoOfDaysPerPeriod As Decimal = 0
        Dim IntPeriodWorkingUint As Double = 0
        Dim StrPrepareStage As String
        Dim checkContractTransActiveDate As Date = Date.Now.Date

        dblBenifets = 0
        dbldeduction = 0
        Try

            CreateDataTable(PtrDtBenefits, "Benefits")
            CreateDataTable(PtrDtDeductions, "Deductions")

            Select Case PrepareStage
                Case ePrepareStage.Normal
                    StrPrepareStage = " And  PaidAtVacation <> 2 "
                Case ePrepareStage.Vacation
                    StrPrepareStage = " And (PaidAtVacation = 1 or PaidAtVacation = 2)"
                Case ePrepareStage.VacationCost
                    StrPrepareStage = " And (PaidAtVacation = 1 or PaidAtVacation = 2) And TransactionTypeID not in (select ID from hrs_TransactionsTypes where TransactionGroupID = 3)"
            End Select

            '1-Get Valid Contract ID 
            IntContractID = ClsContracts.ContractValidatoinId(IntEmployeeId, IntFiscalYearPeriod)
            ClsEmployeesClasses.Find("id=" & ClsContracts.EmployeeClassID)

            If IntContractID = 0 Then
                Return False
            End If

            '2-Assign Working days and no. of days per period 
            If ClsEmployeesClasses.WorkingUnitsIsHours Then
                IntNoOfDaysPerPeriod = ClsEmployeesClasses.NoOfDaysPerPeriod * ClsEmployeesClasses.WorkHoursPerDay
            Else
                IntNoOfDaysPerPeriod = ClsEmployeesClasses.NoOfDaysPerPeriod
            End If

            If PrepareStage = ePrepareStage.Vacation Then
                If ClsEmployeesClasses.NoOfDaysPerPeriod = 0 Then
                    Dim clscompany As New Clssys_Companies(mPage)
                    clscompany.Find(" 1=1 ")
                    If clscompany.IncludeAbsencDays = True Then
                        IntNoOfDaysPerPeriod = 30
                    Else
                        If clscompany.IsHigry Then
                            IntNoOfDaysPerPeriod = 354 / 12
                        Else
                            IntNoOfDaysPerPeriod = 360 / 12
                        End If
                    End If
                End If
            End If
            IntPeriodWorkingUint = NoOfWorkingUnits

            If ClsContractTransactions.Find(IntContractID, checkContractTransActiveDate, " Active = 1 and TransactionTypeID not in (select ID from hrs_TransactionsTypes where isnull(IsProjectRelatedItem,0) = 1)" & StrPrepareStage) Then
                For Each row As Data.DataRow In ClsContractTransactions.DataSet.Tables(0).Rows

                    'Check Once At Period  
                    '===================== [Begin]
                    If CBool(row.Item("OnceAtPeriod")) = True Then
                        'Get No Of Times This Transaction Employee Earned 
                        If ClsEmployeesTransactions.GetPaidTransactionsNosPerPeriod(IntEmployeeId, CInt(row.Item(2)), IntFiscalYearPeriod) >= 1 Then
                            Continue For
                        End If
                    End If

                    'Check Transaction Period 
                    If Not ClsContractTransactions.ValidTransaction(IntEmployeeId, row.Item(2), checkContractTransActiveDate, IntFiscalYearPeriod) Then
                        Continue For
                    End If

                    'Check If Transaction Type being Caculated Has Formula or Not And Calculate It  
                    ClsTransactionsTypes.Find("ID=" & row.Item(2))
                    'check execluded salary items
                    If ClsTransactionsTypes.IsSalaryEOSExeclude = True And PrepareStage = ePrepareStage.Normal And PrepareType = ePrepareType.EndOfContract Then
                        Continue For
                    End If

                    Select Case PrepareType
                        Case ePrepareType.BeginOfContract
                            FormulaExpression = ClsTransactionsTypes.BeginContractFormula
                        Case ePrepareType.EndOfContract
                            FormulaExpression = ClsTransactionsTypes.EndContractFormula
                        Case ePrepareType.Normal
                            FormulaExpression = ClsTransactionsTypes.Formula
                    End Select

                    If FormulaExpression = String.Empty Then
                        DblAmount = ObjDatahandler.DataValue_Out(row.Item(3), Data.SqlDbType.Money)
                    Else
                        ClsSolver = New Clshrs_FormulaSolver(ClsTransactionsTypes.ConnectionString, mPage)
                        ClsSolver.EmployeeID = IntEmployeeId
                        ClsSolver.FiscalPeriodID = IntFiscalYearPeriod
                        ClsSolver.OverTimePerPeriod = OvertimeWorkHours
                        ClsSolver.HolidayHoursperPeriod = HolidayWorkHours
                        ClsSolver.SalaryPricePerHour = SalaryPerHour
                        ClsSolver.SalaryPricePerDay = SalaryPerDay
                        ClsSolver.NoOfDaysPerPeriod = IntNoOfDaysPerPeriod
                        ClsSolver.NoOfWorkingDays = IntPeriodWorkingUint
                        ClsSolver.ProjectWorkingUnits = ProjectWorkingUnits
                        ClsSolver.IsToAttend = IsToAttend
                        ClsSolver.ToAttenddate = ToAttendEndDate
                        ClsSolver.BolBeginOfContract = False
                        Select Case PrepareType
                            Case ePrepareType.BeginOfContract
                                ClsSolver.BolBeginOfContract = True
                        End Select
                        ClsSolver.Executedate = ToDate
                        ClsSolver.TotalProjectsWorkingUnits = TotalProjectsWorkingUnits
                        ClsSolver.SalarySrc = SalarySrc
                        ClsSolver.EvaluateExpression(FormulaExpression)
                        DblAmount = IIf(IsNumeric(ClsSolver.Output), ClsSolver.Output, 0)
                    End If

                    'Edited By: Hassan Kurdi
                    'Date: 2022-04-11
                    'Purpose: Add two digits after comma

                    'DblAmount = Math.Round(DblAmount, 0)
                    DblAmount = Math.Round(DblAmount, 2)
                    'End

                    'Create Rows And Assign Values To it 
                    If ClsTransactionsTypes.Sign > 0 Then
                        ObjdtRow = PtrDtBenefits.NewRow()
                        ObjdtRow(0) = ClsTransactionsTypes.ID
                        ObjdtRow(1) = DblAmount
                        If ClsTransactionsTypes.IsPaid Then
                            If ClsSolver.WorkOfDays Then
                                ObjdtRow(2) = StrPaidOnDays
                                ObjdtRow(4) = "Paid By Days"
                                ClsSolver.WorkOfDays = False
                            Else
                                ObjdtRow(2) = StrPaid
                                ObjdtRow(4) = "Paid"
                            End If

                            dblBenifets += DblAmount
                        Else
                            ObjdtRow(2) = StrNotPaid
                            ObjdtRow(4) = "Not Paid"
                        End If
                        PtrDtBenefits.Rows.Add(ObjdtRow)
                    Else
                        ObjdtRow = PtrDtDeductions.NewRow()
                        ObjdtRow(0) = ClsTransactionsTypes.ID
                        ObjdtRow(1) = DblAmount
                        If ClsTransactionsTypes.IsPaid Then
                            ObjdtRow(2) = StrPaid
                            ObjdtRow(4) = "Paid"
                            dbldeduction += DblAmount
                        Else
                            ObjdtRow(2) = StrNotPaid
                            ObjdtRow(4) = "Not Paid"
                        End If
                        PtrDtDeductions.Rows.Add(ObjdtRow)
                    End If
                Next
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    '========================================================================
    'ProcedureName  :   CollectEmployeesPayabilites 
    'Module         :   Hrs (Human Resource Module)
    'Project        :   Venus V.
    'Description    :   getting the payability data for the employee in order to 
    '               :   modify the previos information 
    'Steps          :   1-Getting the EmployeeTransaction Headid for this employee for this spacific period 
    '               :   2-Get the plus Transactions and minus Transactions by meployeeTransactionDetail Class 
    'Developer      :   DataOcean 
    'Date Created   :   25-10-2007
    'fn. Arguments  :
    'Calls          :   Clshrs_EmployeesTransactions,Clshrs_EmployeeTrnasctionsDetails,ClsFiscalPeriods 
    'From           :   PageLoad()
    'To             :
    '               :
    'Modified       : [Abdulrahman] [07/07/2012]
    '========================================================================ClsEmployee.
    Public Function CollectEmployeesPayablities(ByVal EmployeeID As Integer, ByVal FisicalPeriodID As Integer, ByVal PtrDtBenefits As Data.DataTable,
    ByVal PtrDtDeductions As Data.DataTable, ByRef dblbenifits As Double, ByRef dbldeduction As Double, ByVal PrepType As String) As Boolean
        Dim ClsEmployeesPayability As New Clshrs_EmployeesPayability(mPage)
        Dim ObjDs As New Data.DataSet
        Dim DrDataRow As Data.DataRow
        Dim DrNewDataRow As Data.DataRow
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim dblPayable As Double = 0
        Try

            If EmployeeID = 0 Then Return False
            ClsEmployeesPayability.GetEmployeesPayabilities(EmployeeID, FisicalPeriodID, ObjDs, PrepType)
            For Each DrDataRow In ObjDs.Tables(0).Rows
                If DrDataRow("Sign") > 0 Then
                    DrNewDataRow = PtrDtBenefits.NewRow()
                    DrNewDataRow(0) = DrDataRow("TransactionTypeID")
                    DrNewDataRow(1) = DrDataRow("Amount")
                    If PrepType = "N" Then
                        DrNewDataRow(5) = DrDataRow("Amount")
                    End If
                    If DrDataRow("IsPaid") Then
                        DrNewDataRow(2) = StrPaid
                        DrNewDataRow(4) = "Paid"
                        dblbenifits += DrDataRow("Amount")
                    Else
                        DrNewDataRow(2) = StrNotPaid
                        DrNewDataRow(4) = "Not Paid"
                    End If
                    DrNewDataRow(3) = DrDataRow("EmpSchID")
                    PtrDtBenefits.Rows.Add(DrNewDataRow)
                Else
                    DrNewDataRow = PtrDtDeductions.NewRow()
                    DrNewDataRow(0) = DrDataRow("TransactionTypeID")
                    DrNewDataRow(1) = DrDataRow("Amount")
                    If PrepType = "N" Then
                        DrNewDataRow(5) = DrDataRow("Amount")
                    End If
                    If DrDataRow("IsPaid") Then
                        DrNewDataRow(2) = StrPaid
                        DrNewDataRow(4) = "Paid"
                        dbldeduction += DrDataRow("Amount")
                    Else
                        DrNewDataRow(2) = StrNotPaid
                        DrNewDataRow(4) = "Not Paid"
                    End If
                    DrNewDataRow(3) = DrDataRow("EmpSchID")
                    PtrDtDeductions.Rows.Add(DrNewDataRow)
                End If
            Next
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '===========================================================================
    'Function Name  : 
    'Date Created   : 
    'Developer      : 
    'Description    : 
    '===========================================================================










    '===========================================================================
    'Function Name  : fnGetDates()
    'Date Created   : 10-07-2007
    'Developer      : [AGR] 
    'Description    : Makes DateTime Object from intger values  and save it into arraylist 
    '               : uses date from calender and values entered in the txtshift text boxes and by combination convert them to datetime values 
    '===========================================================================
    Private Function fnGetDates(ByVal dDate As Date) As Date

        Dim dDay As Integer = dDate.Day
        Dim dMonth As Integer = dDate.Month
        Dim dYear As Integer = dDate.Year
        Return fnMakeDateTime(dYear, dMonth, dDay, 0, 0, 0)
    End Function

    '===========================================================================
    'Function Name  : fnMakeDateTime()
    'Date Created   : 10-07-2007
    'Developer      : [AGR] 
    'Description    : Makes DateTime Object from intger values  
    '===========================================================================
    Private Function fnMakeDateTime(ByVal dYear As Integer, ByVal dMonth As Integer, ByVal dDay As Integer, ByVal dHours As Integer, ByVal dMinutes As Integer, Optional ByVal dSecounds As Integer = 0) As DateTime
        Dim dateConverted As New Global.System.DateTime(dYear, dMonth, dDay, dHours, dMinutes, dSecounds)
        Return dateConverted
    End Function

End Class