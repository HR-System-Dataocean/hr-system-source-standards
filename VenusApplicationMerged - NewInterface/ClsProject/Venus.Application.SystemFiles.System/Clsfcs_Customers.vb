Public Class Clsfcs_Customers
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " fcs_Customers "
        mInsertParameter = "" & _
          "Code," & _
          "EngName," & _
          "ArbName," & _
          "ArbName4S," & _
          "ShortEngName," & _
          "ShortArbName," & _
          "ShortArbName4S," & _
          "CompanyName," & _
          "StartBusinessDate," & _
          "OnHold," & _
          "ParentID," & _
          "CustomerClassID," & _
          "Priority," & _
          "ShippingMethodID," & _
          "SalesPersonID," & _
          "TerritoryID," & _
          "Type," & _
          "Del_PaymentMethodID," & _
          "PriceLevelID," & _
          "ActiveDate," & _
          "HoldDate," & _
          "CostCenter1ID," & _
          "CostCenter2ID," & _
          "CostCenter3ID," & _
          "CostCenter4ID," & _
          "Del_StatementCycle," & _
          "AddLink," & _
          "CurrencyID," & _
          "UseLineDisc," & _
          "UseEndDisc," & _
          "PaymentSchedSettingID," & _
          "CreditLimitAmount," & _
          "MandatoryCreditLimit," & _
          "PaymentTermID," & _
          "CashDiscountID," & _
          "IsArabicLanguage," & _
          "StatementCycle," & _
          "StatementCondition," & _
          "SendStatmentByEmail," & _
          "OnTimeCustomer," & _
          "VednorID," & _
          "AgingStyleID," & _
          "NoofEmployees," & _
          "CompanyID," & _
          "Remarks," & _
          "RegUserID," & _
          "RegComputerID," & _
          "Del_Code"
        mInsertParameterValues = "" & _
          " @Code," & _
          " @EngName," & _
          " @ArbName," & _
          " @ArbName4S," & _
          " @ShortEngName," & _
          " @ShortArbName," & _
          " @ShortArbName4S," & _
          " @CompanyName," & _
          " @StartBusinessDate," & _
          " @OnHold," & _
          " @ParentID," & _
          " @CustomerClassID," & _
          " @Priority," & _
          " @ShippingMethodID," & _
          " @SalesPersonID," & _
          " @TerritoryID," & _
          " @Type," & _
          " @Del_PaymentMethodID," & _
          " @PriceLevelID," & _
          " @ActiveDate," & _
          " @HoldDate," & _
          " @CostCenter1ID," & _
          " @CostCenter2ID," & _
          " @CostCenter3ID," & _
          " @CostCenter4ID," & _
          " @Del_StatementCycle," & _
          " @AddLink," & _
          " @CurrencyID," & _
          " @UseLineDisc," & _
          " @UseEndDisc," & _
          " @PaymentSchedSettingID," & _
          " @CreditLimitAmount," & _
          " @MandatoryCreditLimit," & _
          " @PaymentTermID," & _
          " @CashDiscountID," & _
          " @IsArabicLanguage," & _
          " @StatementCycle," & _
          " @StatementCondition," & _
          " @SendStatmentByEmail," & _
          " @OnTimeCustomer," & _
          " @VednorID," & _
          " @AgingStyleID," & _
          " @NoofEmployees," & _
          " @CompanyID," & _
          " @Remarks," & _
          " @RegUserID," & _
          " @RegComputerID," & _
          " @Del_Code"
        mUpdateParameter = "" & _
          "Code=@Code," & _
          "EngName=@EngName," & _
          "ArbName=@ArbName," & _
          "ArbName4S=@ArbName4S," & _
          "ShortEngName=@ShortEngName," & _
          "ShortArbName=@ShortArbName," & _
          "ShortArbName4S=@ShortArbName4S," & _
          "CompanyName=@CompanyName," & _
          "StartBusinessDate=@StartBusinessDate," & _
          "OnHold=@OnHold," & _
          "ParentID=@ParentID," & _
          "CustomerClassID=@CustomerClassID," & _
          "Priority=@Priority," & _
          "ShippingMethodID=@ShippingMethodID," & _
          "SalesPersonID=@SalesPersonID," & _
          "TerritoryID=@TerritoryID," & _
          "Type=@Type," & _
          "Del_PaymentMethodID=@Del_PaymentMethodID," & _
          "PriceLevelID=@PriceLevelID," & _
          "ActiveDate=@ActiveDate," & _
          "HoldDate=@HoldDate," & _
          "CostCenter1ID=@CostCenter1ID," & _
          "CostCenter2ID=@CostCenter2ID," & _
          "CostCenter3ID=@CostCenter3ID," & _
          "CostCenter4ID=@CostCenter4ID," & _
          "Del_StatementCycle=@Del_StatementCycle," & _
          "AddLink=@AddLink," & _
          "CurrencyID=@CurrencyID," & _
          "UseLineDisc=@UseLineDisc," & _
          "UseEndDisc=@UseEndDisc," & _
          "PaymentSchedSettingID=@PaymentSchedSettingID," & _
          "CreditLimitAmount=@CreditLimitAmount," & _
          "MandatoryCreditLimit=@MandatoryCreditLimit," & _
          "PaymentTermID=@PaymentTermID," & _
          "CashDiscountID=@CashDiscountID," & _
          "IsArabicLanguage=@IsArabicLanguage," & _
          "StatementCycle=@StatementCycle," & _
          "StatementCondition=@StatementCondition," & _
          "SendStatmentByEmail=@SendStatmentByEmail," & _
          "OnTimeCustomer=@OnTimeCustomer," & _
          "VednorID=@VednorID," & _
          "AgingStyleID=@AgingStyleID," & _
          "NoofEmployees=@NoofEmployees," & _
          "Remarks=@Remarks," & _
          "Del_Code=@Del_Code"
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
    Private mShortEngName As String
    Private mShortArbName As String
    Private mShortArbName4S As String
    Private mCompanyName As String
    Private mStartBusinessDate As DateTime
    Private mOnHold As Boolean
    Private mParentID As Integer
    Private mCustomerClassID As Integer
    Private mPriority As Object
    Private mShippingMethodID As Integer
    Private mSalesPersonID As Integer
    Private mTerritoryID As Integer
    Private mType As String
    Private mDel_PaymentMethodID As Integer
    Private mPriceLevelID As Integer
    Private mActiveDate As DateTime
    Private mHoldDate As DateTime
    Private mCostCenter1ID As Integer
    Private mCostCenter2ID As Integer
    Private mCostCenter3ID As Integer
    Private mCostCenter4ID As Integer
    Private mDel_StatementCycle As Integer
    Private mAddLink As String
    Private mCurrencyID As Integer
    Private mUseLineDisc As Boolean
    Private mUseEndDisc As Boolean
    Private mPaymentSchedSettingID As Integer
    Private mCreditLimitAmount As Decimal
    Private mMandatoryCreditLimit As Boolean
    Private mPaymentTermID As Integer
    Private mCashDiscountID As Integer
    Private mIsArabicLanguage As Boolean
    Private mStatementCycle As Object
    Private mStatementCondition As Object
    Private mSendStatmentByEmail As Boolean
    Private mOnTimeCustomer As Boolean
    Private mVednorID As Integer
    Private mAgingStyleID As Integer
    Private mNoofEmployees As Integer
    Private mCompanyID As Integer
    Private mRemarks As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As DateTime
    Private mCancelDate As DateTime
    Private mDel_Code As String

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
    Public Property ShortEngName() As String
        Get
            Return mShortEngName
        End Get
        Set(ByVal Value As String)
            mShortEngName = Value
        End Set
    End Property
    Public Property ShortArbName() As String
        Get
            Return mShortArbName
        End Get
        Set(ByVal Value As String)
            mShortArbName = Value
        End Set
    End Property
    Public Property ShortArbName4S() As String
        Get
            Return mShortArbName4S
        End Get
        Set(ByVal Value As String)
            mShortArbName4S = Value
        End Set
    End Property
    Public Property CompanyName() As String
        Get
            Return mCompanyName
        End Get
        Set(ByVal Value As String)
            mCompanyName = Value
        End Set
    End Property
    Public Property StartBusinessDate() As DateTime
        Get
            Return mStartBusinessDate
        End Get
        Set(ByVal Value As DateTime)
            mStartBusinessDate = Value
        End Set
    End Property
    Public Property OnHold() As Boolean
        Get
            Return mOnHold
        End Get
        Set(ByVal Value As Boolean)
            mOnHold = Value
        End Set
    End Property
    Public Property ParentID() As Integer
        Get
            Return mParentID
        End Get
        Set(ByVal Value As Integer)
            mParentID = Value
        End Set
    End Property
    Public Property CustomerClassID() As Integer
        Get
            Return mCustomerClassID
        End Get
        Set(ByVal Value As Integer)
            mCustomerClassID = Value
        End Set
    End Property
    Public Property Priority() As Object
        Get
            Return mPriority
        End Get
        Set(ByVal Value As Object)
            mPriority = Value
        End Set
    End Property
    Public Property ShippingMethodID() As Integer
        Get
            Return mShippingMethodID
        End Get
        Set(ByVal Value As Integer)
            mShippingMethodID = Value
        End Set
    End Property
    Public Property SalesPersonID() As Integer
        Get
            Return mSalesPersonID
        End Get
        Set(ByVal Value As Integer)
            mSalesPersonID = Value
        End Set
    End Property
    Public Property TerritoryID() As Integer
        Get
            Return mTerritoryID
        End Get
        Set(ByVal Value As Integer)
            mTerritoryID = Value
        End Set
    End Property
    Public Property Type() As String
        Get
            Return mType
        End Get
        Set(ByVal Value As String)
            mType = Value
        End Set
    End Property
    Public Property Del_PaymentMethodID() As Integer
        Get
            Return mDel_PaymentMethodID
        End Get
        Set(ByVal Value As Integer)
            mDel_PaymentMethodID = Value
        End Set
    End Property
    Public Property PriceLevelID() As Integer
        Get
            Return mPriceLevelID
        End Get
        Set(ByVal Value As Integer)
            mPriceLevelID = Value
        End Set
    End Property
    Public Property ActiveDate() As DateTime
        Get
            Return mActiveDate
        End Get
        Set(ByVal Value As DateTime)
            mActiveDate = Value
        End Set
    End Property
    Public Property HoldDate() As DateTime
        Get
            Return mHoldDate
        End Get
        Set(ByVal Value As DateTime)
            mHoldDate = Value
        End Set
    End Property
    Public Property CostCenter1ID() As Integer
        Get
            Return mCostCenter1ID
        End Get
        Set(ByVal Value As Integer)
            mCostCenter1ID = Value
        End Set
    End Property
    Public Property CostCenter2ID() As Integer
        Get
            Return mCostCenter2ID
        End Get
        Set(ByVal Value As Integer)
            mCostCenter2ID = Value
        End Set
    End Property
    Public Property CostCenter3ID() As Integer
        Get
            Return mCostCenter3ID
        End Get
        Set(ByVal Value As Integer)
            mCostCenter3ID = Value
        End Set
    End Property
    Public Property CostCenter4ID() As Integer
        Get
            Return mCostCenter4ID
        End Get
        Set(ByVal Value As Integer)
            mCostCenter4ID = Value
        End Set
    End Property
    Public Property Del_StatementCycle() As Integer
        Get
            Return mDel_StatementCycle
        End Get
        Set(ByVal Value As Integer)
            mDel_StatementCycle = Value
        End Set
    End Property
    Public Property AddLink() As String
        Get
            Return mAddLink
        End Get
        Set(ByVal Value As String)
            mAddLink = Value
        End Set
    End Property
    Public Property CurrencyID() As Integer
        Get
            Return mCurrencyID
        End Get
        Set(ByVal Value As Integer)
            mCurrencyID = Value
        End Set
    End Property
    Public Property UseLineDisc() As Boolean
        Get
            Return mUseLineDisc
        End Get
        Set(ByVal Value As Boolean)
            mUseLineDisc = Value
        End Set
    End Property
    Public Property UseEndDisc() As Boolean
        Get
            Return mUseEndDisc
        End Get
        Set(ByVal Value As Boolean)
            mUseEndDisc = Value
        End Set
    End Property
    Public Property PaymentSchedSettingID() As Integer
        Get
            Return mPaymentSchedSettingID
        End Get
        Set(ByVal Value As Integer)
            mPaymentSchedSettingID = Value
        End Set
    End Property
    Public Property CreditLimitAmount() As Decimal
        Get
            Return mCreditLimitAmount
        End Get
        Set(ByVal Value As Decimal)
            mCreditLimitAmount = Value
        End Set
    End Property
    Public Property MandatoryCreditLimit() As Boolean
        Get
            Return mMandatoryCreditLimit
        End Get
        Set(ByVal Value As Boolean)
            mMandatoryCreditLimit = Value
        End Set
    End Property
    Public Property PaymentTermID() As Integer
        Get
            Return mPaymentTermID
        End Get
        Set(ByVal Value As Integer)
            mPaymentTermID = Value
        End Set
    End Property
    Public Property CashDiscountID() As Integer
        Get
            Return mCashDiscountID
        End Get
        Set(ByVal Value As Integer)
            mCashDiscountID = Value
        End Set
    End Property
    Public Property IsArabicLanguage() As Boolean
        Get
            Return mIsArabicLanguage
        End Get
        Set(ByVal Value As Boolean)
            mIsArabicLanguage = Value
        End Set
    End Property
    Public Property StatementCycle() As Object
        Get
            Return mStatementCycle
        End Get
        Set(ByVal Value As Object)
            mStatementCycle = Value
        End Set
    End Property
    Public Property StatementCondition() As Object
        Get
            Return mStatementCondition
        End Get
        Set(ByVal Value As Object)
            mStatementCondition = Value
        End Set
    End Property
    Public Property SendStatmentByEmail() As Boolean
        Get
            Return mSendStatmentByEmail
        End Get
        Set(ByVal Value As Boolean)
            mSendStatmentByEmail = Value
        End Set
    End Property
    Public Property OnTimeCustomer() As Boolean
        Get
            Return mOnTimeCustomer
        End Get
        Set(ByVal Value As Boolean)
            mOnTimeCustomer = Value
        End Set
    End Property
    Public Property VednorID() As Integer
        Get
            Return mVednorID
        End Get
        Set(ByVal Value As Integer)
            mVednorID = Value
        End Set
    End Property
    Public Property AgingStyleID() As Integer
        Get
            Return mAgingStyleID
        End Get
        Set(ByVal Value As Integer)
            mAgingStyleID = Value
        End Set
    End Property
    Public Property NoofEmployees() As Integer
        Get
            Return mNoofEmployees
        End Get
        Set(ByVal Value As Integer)
            mNoofEmployees = Value
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
    Public Property Del_Code() As String
        Get
            Return mDel_Code
        End Get
        Set(ByVal Value As String)
            mDel_Code = Value
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
    'Date Created   : 30/11/2015
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
    'Date Created   : 30/11/2015
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
    'Date Created   :30/11/2015
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
    'Date Created   :   30/11/2015
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
    'Date Created   :30/11/2015
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
    'Date Created   :30/11/2015 13:45:47
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
    'Date Created   :30/11/2015
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
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mShortEngName = String.Empty
            mShortArbName = String.Empty
            mShortArbName4S = String.Empty
            mCompanyName = String.Empty
            mStartBusinessDate = Nothing
            mOnHold = False
            mParentID = 0
            mCustomerClassID = 0
            mPriority = Nothing
            mShippingMethodID = 0
            mSalesPersonID = 0
            mTerritoryID = 0
            mType = String.Empty
            mDel_PaymentMethodID = 0
            mPriceLevelID = 0
            mActiveDate = Nothing
            mHoldDate = Nothing
            mCostCenter1ID = 0
            mCostCenter2ID = 0
            mCostCenter3ID = 0
            mCostCenter4ID = 0
            mDel_StatementCycle = 0
            mAddLink = String.Empty
            mCurrencyID = 0
            mUseLineDisc = False
            mUseEndDisc = False
            mPaymentSchedSettingID = 0
            mCreditLimitAmount = 0
            mMandatoryCreditLimit = False
            mPaymentTermID = 0
            mCashDiscountID = 0
            mIsArabicLanguage = False
            mStatementCycle = Nothing
            mStatementCondition = Nothing
            mSendStatmentByEmail = False
            mOnTimeCustomer = False
            mVednorID = 0
            mAgingStyleID = 0
            mNoofEmployees = 0
            mCompanyID = 0
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mDel_Code = String.Empty
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
    'Date Created   :30/11/2015
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
                mArbName4S = [Shared].DataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mShortEngName = [Shared].DataHandler.DataValue_Out(.Item("ShortEngName"), SqlDbType.VarChar)
                mShortArbName = [Shared].DataHandler.DataValue_Out(.Item("ShortArbName"), SqlDbType.VarChar)
                mShortArbName4S = [Shared].DataHandler.DataValue_Out(.Item("ShortArbName4S"), SqlDbType.VarChar)
                mCompanyName = [Shared].DataHandler.DataValue_Out(.Item("CompanyName"), SqlDbType.VarChar)
                mStartBusinessDate = [Shared].DataHandler.DataValue_Out(.Item("StartBusinessDate"), SqlDbType.DateTime)
                mOnHold = [Shared].DataHandler.DataValue_Out(.Item("OnHold"), SqlDbType.Bit)
                mParentID = [Shared].DataHandler.DataValue_Out(.Item("ParentID"), SqlDbType.Int, True)
                mCustomerClassID = [Shared].DataHandler.DataValue_Out(.Item("CustomerClassID"), SqlDbType.Int, True)
                mPriority = [Shared].DataHandler.DataValue_Out(.Item("Priority"), SqlDbType.tinyint)
                mShippingMethodID = [Shared].DataHandler.DataValue_Out(.Item("ShippingMethodID"), SqlDbType.Int, True)
                mSalesPersonID = [Shared].DataHandler.DataValue_Out(.Item("SalesPersonID"), SqlDbType.Int, True)
                mTerritoryID = [Shared].DataHandler.DataValue_Out(.Item("TerritoryID"), SqlDbType.Int, True)
                mType = [Shared].DataHandler.DataValue_Out(.Item("Type"), SqlDbType.VarChar)
                mDel_PaymentMethodID = [Shared].DataHandler.DataValue_Out(.Item("Del_PaymentMethodID"), SqlDbType.Int, True)
                mPriceLevelID = [Shared].DataHandler.DataValue_Out(.Item("PriceLevelID"), SqlDbType.Int, True)
                mActiveDate = [Shared].DataHandler.DataValue_Out(.Item("ActiveDate"), SqlDbType.DateTime)
                mHoldDate = [Shared].DataHandler.DataValue_Out(.Item("HoldDate"), SqlDbType.DateTime)
                mCostCenter1ID = [Shared].DataHandler.DataValue_Out(.Item("CostCenter1ID"), SqlDbType.Int, True)
                mCostCenter2ID = [Shared].DataHandler.DataValue_Out(.Item("CostCenter2ID"), SqlDbType.Int, True)
                mCostCenter3ID = [Shared].DataHandler.DataValue_Out(.Item("CostCenter3ID"), SqlDbType.Int, True)
                mCostCenter4ID = [Shared].DataHandler.DataValue_Out(.Item("CostCenter4ID"), SqlDbType.Int, True)
                mDel_StatementCycle = [Shared].DataHandler.DataValue_Out(.Item("Del_StatementCycle"), SqlDbType.Int, True)
                mAddLink = [Shared].DataHandler.DataValue_Out(.Item("AddLink"), SqlDbType.VarChar)
                mCurrencyID = [Shared].DataHandler.DataValue_Out(.Item("CurrencyID"), SqlDbType.Int, True)
                mUseLineDisc = [Shared].DataHandler.DataValue_Out(.Item("UseLineDisc"), SqlDbType.Bit)
                mUseEndDisc = [Shared].DataHandler.DataValue_Out(.Item("UseEndDisc"), SqlDbType.Bit)
                mPaymentSchedSettingID = [Shared].DataHandler.DataValue_Out(.Item("PaymentSchedSettingID"), SqlDbType.Int, True)
                mCreditLimitAmount = [Shared].DataHandler.DataValue_Out(.Item("CreditLimitAmount"), SqlDbType.Decimal)
                mMandatoryCreditLimit = [Shared].DataHandler.DataValue_Out(.Item("MandatoryCreditLimit"), SqlDbType.Bit)
                mPaymentTermID = [Shared].DataHandler.DataValue_Out(.Item("PaymentTermID"), SqlDbType.Int, True)
                mCashDiscountID = [Shared].DataHandler.DataValue_Out(.Item("CashDiscountID"), SqlDbType.Int, True)
                mIsArabicLanguage = [Shared].DataHandler.DataValue_Out(.Item("IsArabicLanguage"), SqlDbType.Bit)
                mStatementCycle = [Shared].DataHandler.DataValue_Out(.Item("StatementCycle"), SqlDbType.tinyint)
                mStatementCondition = [Shared].DataHandler.DataValue_Out(.Item("StatementCondition"), SqlDbType.tinyint)
                mSendStatmentByEmail = [Shared].DataHandler.DataValue_Out(.Item("SendStatmentByEmail"), SqlDbType.Bit)
                mOnTimeCustomer = [Shared].DataHandler.DataValue_Out(.Item("OnTimeCustomer"), SqlDbType.Bit)
                mVednorID = [Shared].DataHandler.DataValue_Out(.Item("VednorID"), SqlDbType.Int, True)
                mAgingStyleID = [Shared].DataHandler.DataValue_Out(.Item("AgingStyleID"), SqlDbType.Int, True)
                mNoofEmployees = [Shared].DataHandler.DataValue_Out(.Item("NoofEmployees"), SqlDbType.Int, True)
                mCompanyID = [Shared].DataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int, True)
                mRemarks = [Shared].DataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mDel_Code = [Shared].DataHandler.DataValue_Out(.Item("Del_Code"), SqlDbType.VarChar)
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
    'Date Created   : 30/11/2015 13:45:47
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ShortEngName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mShortEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ShortArbName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mShortArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ShortArbName4S", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mShortArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCompanyName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@StartBusinessDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mStartBusinessDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OnHold", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mOnHold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ParentID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mParentID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CustomerClassID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCustomerClassID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Priority", SqlDbType.tinyint)).Value = [Shared].DataHandler.DataValue_In(mPriority, SqlDbType.tinyint)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ShippingMethodID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mShippingMethodID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SalesPersonID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mSalesPersonID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TerritoryID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mTerritoryID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mType, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Del_PaymentMethodID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mDel_PaymentMethodID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PriceLevelID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPriceLevelID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ActiveDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mActiveDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HoldDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mHoldDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenter1ID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCostCenter1ID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenter2ID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCostCenter2ID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenter3ID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCostCenter3ID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenter4ID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCostCenter4ID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Del_StatementCycle", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mDel_StatementCycle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AddLink", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mAddLink, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CurrencyID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCurrencyID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UseLineDisc", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mUseLineDisc, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UseEndDisc", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mUseEndDisc, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PaymentSchedSettingID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPaymentSchedSettingID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CreditLimitAmount", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mCreditLimitAmount, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MandatoryCreditLimit", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mMandatoryCreditLimit, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PaymentTermID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPaymentTermID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CashDiscountID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCashDiscountID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsArabicLanguage", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsArabicLanguage, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@StatementCycle", SqlDbType.tinyint)).Value = [Shared].DataHandler.DataValue_In(mStatementCycle, SqlDbType.tinyint)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@StatementCondition", SqlDbType.tinyint)).Value = [Shared].DataHandler.DataValue_In(mStatementCondition, SqlDbType.tinyint)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SendStatmentByEmail", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mSendStatmentByEmail, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OnTimeCustomer", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mOnTimeCustomer, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VednorID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mVednorID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AgingStyleID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mAgingStyleID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NoofEmployees", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mNoofEmployees, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Del_Code", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mDel_Code, SqlDbType.VarChar)
            If (strMode.Trim.ToUpper = "SAVE") Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
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
    'Date Created   : 30/11/2015 13:45:47
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
