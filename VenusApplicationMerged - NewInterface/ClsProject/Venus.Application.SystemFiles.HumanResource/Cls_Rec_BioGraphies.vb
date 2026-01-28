Imports Venus.Application.SystemFiles.System
Public Class ClsRec_BioGraphies
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " Rec_BioGraphies "
        mInsertParameter = "" & _
          "Code," & _
          "GDate," & _
          "HDate," & _
          "EngName," & _
          "ArbName," & _
          "ArbName4S," & _
          "FamilyEngName," & _
          "FamilyArbName," & _
          "FamilyArbName4S," & _
          "FatherEngName," & _
          "FatherArbName," & _
          "FatherArbName4S," & _
          "GrandEngName," & _
          "GrandArbName," & _
          "GrandArbName4S," & _
          "E_Mail," & _
          "Phone," & _
          "Mobile," & _
          "Position_ID," & _
          "MaritalStatus," & _
          "NODependant," & _
          "LastJob," & _
          "LastSalary," & _
          "ExpectedSalary," & _
          "GBirthDate," & _
          "HBirthDate," & _
          "BirthCityID," & _
          "ReligionID," & _
          "Sex," & _
          "Address," & _
          "HasDLicense," & _
          "Nationality_ID," & _
          "IqamaNo," & _
          "PassportNo," & _
          "HasTIqama," & _
          "NOFSponser," & _
          "HSpConditions," & _
          "SpecialConditions," & _
          "IsUsed," & _
          "Remarks," & _
          "RegUserID," & _
          "RegComputerID"
        mInsertParameterValues = "" & _
          " @Code," & _
          " @GDate," & _
          " @HDate," & _
          " @EngName," & _
          " @ArbName," & _
          " @ArbName4S," & _
          " @FamilyEngName," & _
          " @FamilyArbName," & _
          " @FamilyArbName4S," & _
          " @FatherEngName," & _
          " @FatherArbName," & _
          " @FatherArbName4S," & _
          " @GrandEngName," & _
          " @GrandArbName," & _
          " @GrandArbName4S," & _
          " @E_Mail," & _
          " @Phone," & _
          " @Mobile," & _
          " @Position_ID," & _
          " @MaritalStatus," & _
          " @NODependant," & _
          " @LastJob," & _
          " @LastSalary," & _
          " @ExpectedSalary," & _
          " @GBirthDate," & _
          " @HBirthDate," & _
          " @BirthCityID," & _
          " @ReligionID," & _
          " @Sex," & _
          " @Address," & _
          " @HasDLicense," & _
          " @Nationality_ID," & _
          " @IqamaNo," & _
          " @PassportNo," & _
          " @HasTIqama," & _
          " @NOFSponser," & _
          " @HSpConditions," & _
          " @SpecialConditions," & _
          " @IsUsed," & _
          " @Remarks," & _
          " @RegUserID," & _
          " @RegComputerID"
        mUpdateParameter = "" & _
          "Code=@Code," & _
          "GDate=@GDate," & _
          "HDate=@HDate," & _
          "EngName=@EngName," & _
          "ArbName=@ArbName," & _
          "ArbName4S=@ArbName4S," & _
          "FamilyEngName=@FamilyEngName," & _
          "FamilyArbName=@FamilyArbName," & _
          "FamilyArbName4S=@FamilyArbName4S," & _
          "FatherEngName=@FatherEngName," & _
          "FatherArbName=@FatherArbName," & _
          "FatherArbName4S=@FatherArbName4S," & _
          "GrandEngName=@GrandEngName," & _
          "GrandArbName=@GrandArbName," & _
          "GrandArbName4S=@GrandArbName4S," & _
          "E_Mail=@E_Mail," & _
          "Phone=@Phone," & _
          "Mobile=@Mobile," & _
          "Position_ID=@Position_ID," & _
          "MaritalStatus=@MaritalStatus," & _
          "NODependant=@NODependant," & _
          "LastJob=@LastJob," & _
          "LastSalary=@LastSalary," & _
          "ExpectedSalary=@ExpectedSalary," & _
          "GBirthDate=@GBirthDate," & _
          "HBirthDate=@HBirthDate," & _
          "BirthCityID=@BirthCityID," & _
          "ReligionID=@ReligionID," & _
          "Sex=@Sex," & _
          "Address=@Address," & _
          "HasDLicense=@HasDLicense," & _
          "Nationality_ID=@Nationality_ID," & _
          "IqamaNo=@IqamaNo," & _
          "PassportNo=@PassportNo," & _
          "HasTIqama=@HasTIqama," & _
          "NOFSponser=@NOFSponser," & _
          "HSpConditions=@HSpConditions," & _
          "SpecialConditions=@SpecialConditions," & _
          "IsUsed=@IsUsed," & _
          "Remarks=@Remarks"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mCode As String
    Private mGDate As DateTime
    Private mHDate As String
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mFamilyEngName As String
    Private mFamilyArbName As String
    Private mFamilyArbName4S As String
    Private mFatherEngName As String
    Private mFatherArbName As String
    Private mFatherArbName4S As String
    Private mGrandEngName As String
    Private mGrandArbName As String
    Private mGrandArbName4S As String
    Private mE_Mail As String
    Private mPhone As String
    Private mMobile As String
    Private mPosition_ID As Integer
    Private mMaritalStatus As String
    Private mNODependant As Integer
    Private mLastJob As String
    Private mLastSalary As Decimal
    Private mExpectedSalary As Decimal
    Private mGBirthDate As DateTime
    Private mHBirthDate As String
    Private mBirthCityID As Integer
    Private mReligionID As Integer
    Private mSex As String
    Private mAddress As String
    Private mHasDLicense As Boolean
    Private mNationality_ID As Integer
    Private mIqamaNo As String
    Private mPassportNo As String
    Private mHasTIqama As Boolean
    Private mNOFSponser As Boolean
    Private mHSpConditions As Boolean
    Private mSpecialConditions As String
    Private mIsUsed As Boolean
    Private mRemarks As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As DateTime
    Private mCancelDate As DateTime

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
    Public Property GDate() As DateTime
        Get
            Return mGDate
        End Get
        Set(ByVal Value As DateTime)
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
    Public Property FamilyArbName4S() As String
        Get
            Return mFamilyArbName4S
        End Get
        Set(ByVal Value As String)
            mFamilyArbName4S = Value
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
    Public Property FatherArbName4S() As String
        Get
            Return mFatherArbName4S
        End Get
        Set(ByVal Value As String)
            mFatherArbName4S = Value
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
    Public Property GrandArbName4S() As String
        Get
            Return mGrandArbName4S
        End Get
        Set(ByVal Value As String)
            mGrandArbName4S = Value
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
    Public Property Position_ID() As Integer
        Get
            Return mPosition_ID
        End Get
        Set(ByVal Value As Integer)
            mPosition_ID = Value
        End Set
    End Property
    Public Property MaritalStatus() As String
        Get
            Return mMaritalStatus
        End Get
        Set(ByVal Value As String)
            mMaritalStatus = Value
        End Set
    End Property
    Public Property NODependant() As Integer
        Get
            Return mNODependant
        End Get
        Set(ByVal Value As Integer)
            mNODependant = Value
        End Set
    End Property
    Public Property LastJob() As String
        Get
            Return mLastJob
        End Get
        Set(ByVal Value As String)
            mLastJob = Value
        End Set
    End Property
    Public Property LastSalary() As Decimal
        Get
            Return mLastSalary
        End Get
        Set(ByVal Value As Decimal)
            mLastSalary = Value
        End Set
    End Property
    Public Property ExpectedSalary() As Decimal
        Get
            Return mExpectedSalary
        End Get
        Set(ByVal Value As Decimal)
            mExpectedSalary = Value
        End Set
    End Property
    Public Property GBirthDate() As DateTime
        Get
            Return mGBirthDate
        End Get
        Set(ByVal Value As DateTime)
            mGBirthDate = Value
        End Set
    End Property
    Public Property HBirthDate() As String
        Get
            Return mHBirthDate
        End Get
        Set(ByVal Value As String)
            mHBirthDate = Value
        End Set
    End Property
    Public Property BirthCityID() As Integer
        Get
            Return mBirthCityID
        End Get
        Set(ByVal Value As Integer)
            mBirthCityID = Value
        End Set
    End Property
    Public Property ReligionID() As Integer
        Get
            Return mReligionID
        End Get
        Set(ByVal Value As Integer)
            mReligionID = Value
        End Set
    End Property
    Public Property Sex() As String
        Get
            Return mSex
        End Get
        Set(ByVal Value As String)
            mSex = Value
        End Set
    End Property
    Public Property Address() As String
        Get
            Return mAddress
        End Get
        Set(ByVal Value As String)
            mAddress = Value
        End Set
    End Property
    Public Property HasDLicense() As Boolean
        Get
            Return mHasDLicense
        End Get
        Set(ByVal Value As Boolean)
            mHasDLicense = Value
        End Set
    End Property
    Public Property Nationality_ID() As Integer
        Get
            Return mNationality_ID
        End Get
        Set(ByVal Value As Integer)
            mNationality_ID = Value
        End Set
    End Property
    Public Property IqamaNo() As String
        Get
            Return mIqamaNo
        End Get
        Set(ByVal Value As String)
            mIqamaNo = Value
        End Set
    End Property
    Public Property PassportNo() As String
        Get
            Return mPassportNo
        End Get
        Set(ByVal Value As String)
            mPassportNo = Value
        End Set
    End Property
    Public Property HasTIqama() As Boolean
        Get
            Return mHasTIqama
        End Get
        Set(ByVal Value As Boolean)
            mHasTIqama = Value
        End Set
    End Property
    Public Property NOFSponser() As Boolean
        Get
            Return mNOFSponser
        End Get
        Set(ByVal Value As Boolean)
            mNOFSponser = Value
        End Set
    End Property
    Public Property HSpConditions() As Boolean
        Get
            Return mHSpConditions
        End Get
        Set(ByVal Value As Boolean)
            mHSpConditions = Value
        End Set
    End Property
    Public Property SpecialConditions() As String
        Get
            Return mSpecialConditions
        End Get
        Set(ByVal Value As String)
            mSpecialConditions = Value
        End Set
    End Property
    Public Property IsUsed() As Boolean
        Get
            Return mIsUsed
        End Get
        Set(ByVal Value As Boolean)
            mIsUsed = Value
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
#End Region
#Region "Public Function"
    '========================================================================
    'ProcedureName  :  GetList
    'Project        :  Fisalia Module
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean  
    'Date Created   : 06/06/2011
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()
            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/ [ إختر أحد الإختيارات ] ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetDropDownList
    'Project        :  Fisalia Module
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean  
    'Date Created   : 06/06/2011
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Order By EngName ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()
            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/ [ إختر أحد الإختيارات ]")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow(ID)
                DdlValues.Items.Add(Item)
            Next
            If DdlValues.Items.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Find 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :06/06/2011
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataset = New DataSet
            mSqlDataAdapter.Fill(mDataset)
            If mDataHandler.CheckValidDataObject(mDataset) Then
                GetParameter(mDataset)
            Else
                Clear()
            End If
            If mID > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :   Save 
    'Module         :   (Fisalia Module)
    'Project        :   Fisalia Module
    'Description    :   Save new record and return true if operation done otherwise report errors in ErrorPage
    'Developer      :   DataOcean   
    'Date Created   :   06/06/2011
    'Modifacations  :   
    'fn. Arguments  :   
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, "Save")
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Update 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :06/06/2011
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, "Update")
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Delete 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Delete Table row (set Cancel Date)
    'Developer      :  DataOcean   
    'Date Created   :06/06/2011 16:00:05
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand, "Delete")
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Clear 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Clear Table Columns
    'Developer      :  DataOcean   
    'Date Created   :06/06/2011
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mGDate = Nothing
            mHDate = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mFamilyEngName = String.Empty
            mFamilyArbName = String.Empty
            mFamilyArbName4S = String.Empty
            mFatherEngName = String.Empty
            mFatherArbName = String.Empty
            mFatherArbName4S = String.Empty
            mGrandEngName = String.Empty
            mGrandArbName = String.Empty
            mGrandArbName4S = String.Empty
            mE_Mail = String.Empty
            mPhone = String.Empty
            mMobile = String.Empty
            mPosition_ID = 0
            mMaritalStatus = "S"
            mNODependant = 0
            mLastJob = String.Empty
            mLastSalary = 0
            mExpectedSalary = 0
            mGBirthDate = Nothing
            mHBirthDate = String.Empty
            mBirthCityID = 0
            mReligionID = 0
            mSex = "M"
            mAddress = String.Empty
            mHasDLicense = False
            mNationality_ID = 0
            mIqamaNo = String.Empty
            mPassportNo = String.Empty
            mHasTIqama = False
            mNOFSponser = False
            mHSpConditions = False
            mSpecialConditions = String.Empty
            mIsUsed = False
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
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
    'Date Created   :06/06/2011
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
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mGDate = mDataHandler.DataValue_Out(.Item("GDate"), SqlDbType.DateTime)
                mHDate = mDataHandler.DataValue_Out(.Item("HDate"), SqlDbType.VarChar)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mFamilyEngName = mDataHandler.DataValue_Out(.Item("FamilyEngName"), SqlDbType.VarChar)
                mFamilyArbName = mDataHandler.DataValue_Out(.Item("FamilyArbName"), SqlDbType.VarChar)
                mFamilyArbName4S = mDataHandler.DataValue_Out(.Item("FamilyArbName4S"), SqlDbType.VarChar)
                mFatherEngName = mDataHandler.DataValue_Out(.Item("FatherEngName"), SqlDbType.VarChar)
                mFatherArbName = mDataHandler.DataValue_Out(.Item("FatherArbName"), SqlDbType.VarChar)
                mFatherArbName4S = mDataHandler.DataValue_Out(.Item("FatherArbName4S"), SqlDbType.VarChar)
                mGrandEngName = mDataHandler.DataValue_Out(.Item("GrandEngName"), SqlDbType.VarChar)
                mGrandArbName = mDataHandler.DataValue_Out(.Item("GrandArbName"), SqlDbType.VarChar)
                mGrandArbName4S = mDataHandler.DataValue_Out(.Item("GrandArbName4S"), SqlDbType.VarChar)
                mE_Mail = mDataHandler.DataValue_Out(.Item("E_Mail"), SqlDbType.VarChar)
                mPhone = mDataHandler.DataValue_Out(.Item("Phone"), SqlDbType.VarChar)
                mMobile = mDataHandler.DataValue_Out(.Item("Mobile"), SqlDbType.VarChar)
                mPosition_ID = mDataHandler.DataValue_Out(.Item("Position_ID"), SqlDbType.Int, True)
                mMaritalStatus = mDataHandler.DataValue_Out(.Item("MaritalStatus"), SqlDbType.VarChar)
                mNODependant = mDataHandler.DataValue_Out(.Item("NODependant"), SqlDbType.Int, True)
                mLastJob = mDataHandler.DataValue_Out(.Item("LastJob"), SqlDbType.VarChar)
                mLastSalary = mDataHandler.DataValue_Out(.Item("LastSalary"), SqlDbType.Money)
                mExpectedSalary = mDataHandler.DataValue_Out(.Item("ExpectedSalary"), SqlDbType.Money)
                mGBirthDate = mDataHandler.DataValue_Out(.Item("GBirthDate"), SqlDbType.DateTime)
                mHBirthDate = mDataHandler.DataValue_Out(.Item("HBirthDate"), SqlDbType.VarChar)
                mBirthCityID = mDataHandler.DataValue_Out(.Item("BirthCityID"), SqlDbType.Int, True)
                mReligionID = mDataHandler.DataValue_Out(.Item("ReligionID"), SqlDbType.Int, True)
                mSex = mDataHandler.DataValue_Out(.Item("Sex"), SqlDbType.VarChar)
                mAddress = mDataHandler.DataValue_Out(.Item("Address"), SqlDbType.VarChar)
                mHasDLicense = mDataHandler.DataValue_Out(.Item("HasDLicense"), SqlDbType.Bit)
                mNationality_ID = mDataHandler.DataValue_Out(.Item("Nationality_ID"), SqlDbType.Int, True)
                mIqamaNo = mDataHandler.DataValue_Out(.Item("IqamaNo"), SqlDbType.VarChar)
                mPassportNo = mDataHandler.DataValue_Out(.Item("PassportNo"), SqlDbType.VarChar)
                mHasTIqama = mDataHandler.DataValue_Out(.Item("HasTIqama"), SqlDbType.Bit)
                mNOFSponser = mDataHandler.DataValue_Out(.Item("NOFSponser"), SqlDbType.Bit)
                mHSpConditions = mDataHandler.DataValue_Out(.Item("HSpConditions"), SqlDbType.Bit)
                mSpecialConditions = mDataHandler.DataValue_Out(.Item("SpecialConditions"), SqlDbType.VarChar)
                mIsUsed = mDataHandler.DataValue_Out(.Item("IsUsed"), SqlDbType.Bit)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
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
    'Date Created   : 06/06/2011 16:00:05
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mGDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HDate", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHDate, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FamilyEngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFamilyEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FamilyArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFamilyArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FamilyArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFamilyArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FatherEngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFatherEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FatherArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFatherArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FatherArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFatherArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GrandEngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mGrandEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GrandArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mGrandArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GrandArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mGrandArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@E_Mail", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mE_Mail, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Phone", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPhone, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Mobile", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mMobile, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Position_ID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPosition_ID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaritalStatus", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mMaritalStatus, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NODependant", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mNODependant, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LastJob", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mLastJob, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LastSalary", SqlDbType.Money)).Value = mDataHandler.DataValue_In(mLastSalary, SqlDbType.Money)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExpectedSalary", SqlDbType.Money)).Value = mDataHandler.DataValue_In(mExpectedSalary, SqlDbType.Money)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GBirthDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mGBirthDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HBirthDate", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHBirthDate, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BirthCityID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBirthCityID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReligionID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReligionID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Sex", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSex, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Address", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mAddress, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HasDLicense", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHasDLicense, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Nationality_ID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mNationality_ID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IqamaNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mIqamaNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PassportNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPassportNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HasTIqama", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHasTIqama, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NOFSponser", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mNOFSponser, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HSpConditions", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHSpConditions, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SpecialConditions", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSpecialConditions, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsUsed", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsUsed, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            If (strMode.Trim.ToUpper = "SAVE") Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region
#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   : 06/06/2011 16:00:05
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataset = New DataSet
            mSqlDataAdapter.Fill(mDataset)
            If mDataHandler.CheckValidDataObject(mDataset) Then
                GetParameter(mDataset)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code DESC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code ASC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code <'" & mCode & "' And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code DESC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Class Destructors"
    Public Sub finalized()
        mDataset.Dispose()
    End Sub
#End Region
End Class
