Public Class Clsmms_OperationsHead
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " mms_OperationsHead "
        mInsertParameter = "" & _
          "OperationTypeID," & _
          "OperationCategoryID," & _
          "SequenceID," & _
          "Number," & _
          "Revise," & _
          "ShortDescription," & _
          "ReferanceNumber," & _
          "OperationDate," & _
          "OperationExpireDate," & _
          "AcceptedDate," & _
          "ExpireDate," & _
          "PostUserDate," & _
          "PostDate," & _
          "DelivaryDate," & _
          "PostUserID," & _
          "AcceptedUserID," & _
          "RequiredFor," & _
          "SalespersonID," & _
          "DepartmentID," & _
          "ProjectID," & _
          "WorkOrderID," & _
          "CurrencyID," & _
          "CurrencyExchange," & _
          "CustomerID," & _
          "LocationID," & _
          "DocumentsStatus," & _
          "ModeOfDelivary," & _
          "Priority," & _
          "StatusID," & _
          "ShipperID," & _
          "CustomClearanceID," & _
          "DeliveryTermsTypeID," & _
          "AllowPartialShipments," & _
          "PaymentTermDays," & _
          "CashDiscountDays," & _
          "CashDiscountDiscPercent," & _
          "PerdiodCount," & _
          "NoOfPayments," & _
          "QtyCount," & _
          "LowestAmounTToPay," & _
          "PayBy," & _
          "IsSwitchBL," & _
          "HeaderRemarks," & _
          "FooterRemarks," & _
          "OperationParentID," & _
          "FiscalYearPeriodID," & _
          "Discount1," & _
          "Discount2," & _
          "Discount3," & _
          "OverrideUserID," & _
          "Remarks," & _
          "RegUserID," & _
          "RegComputerID," & _
          "CompanyID," & _
          "VendorCode," & _
          "VendorEngName," & _
          "VendorArbName," & _
          "VendorID," & _
          "CostCenter1ID," & _
          "CostCenter2ID," & _
          "CostCenter3ID," & _
          "CostCenter4ID," & _
          "Del_HeadID," & _
          "NetAmount," & _
          "GrossAmount," & _
          "Disc1Amount," & _
          "Disc2Amount," & _
          "Disc3Amount"
        mInsertParameterValues = "" & _
          " @OperationTypeID," & _
          " @OperationCategoryID," & _
          " @SequenceID," & _
          " @Number," & _
          " @Revise," & _
          " @ShortDescription," & _
          " @ReferanceNumber," & _
          " @OperationDate," & _
          " @OperationExpireDate," & _
          " @AcceptedDate," & _
          " @ExpireDate," & _
          " @PostUserDate," & _
          " @PostDate," & _
          " @DelivaryDate," & _
          " @PostUserID," & _
          " @AcceptedUserID," & _
          " @RequiredFor," & _
          " @SalespersonID," & _
          " @DepartmentID," & _
          " @ProjectID," & _
          " @WorkOrderID," & _
          " @CurrencyID," & _
          " @CurrencyExchange," & _
          " @CustomerID," & _
          " @LocationID," & _
          " @DocumentsStatus," & _
          " @ModeOfDelivary," & _
          " @Priority," & _
          " @StatusID," & _
          " @ShipperID," & _
          " @CustomClearanceID," & _
          " @DeliveryTermsTypeID," & _
          " @AllowPartialShipments," & _
          " @PaymentTermDays," & _
          " @CashDiscountDays," & _
          " @CashDiscountDiscPercent," & _
          " @PerdiodCount," & _
          " @NoOfPayments," & _
          " @QtyCount," & _
          " @LowestAmounTToPay," & _
          " @PayBy," & _
          " @IsSwitchBL," & _
          " @HeaderRemarks," & _
          " @FooterRemarks," & _
          " @OperationParentID," & _
          " @FiscalYearPeriodID," & _
          " @Discount1," & _
          " @Discount2," & _
          " @Discount3," & _
          " @OverrideUserID," & _
          " @Remarks," & _
          " @RegUserID," & _
          " @RegComputerID," & _
          " @CompanyID," & _
          " @VendorCode," & _
          " @VendorEngName," & _
          " @VendorArbName," & _
          " @VendorID," & _
          " @CostCenter1ID," & _
          " @CostCenter2ID," & _
          " @CostCenter3ID," & _
          " @CostCenter4ID," & _
          " @Del_HeadID," & _
          " @NetAmount," & _
          " @GrossAmount," & _
          " @Disc1Amount," & _
          " @Disc2Amount," & _
          " @Disc3Amount"
        mUpdateParameter = "" & _
          "OperationTypeID=@OperationTypeID," & _
          "OperationCategoryID=@OperationCategoryID," & _
          "SequenceID=@SequenceID," & _
          "Number=@Number," & _
          "Revise=@Revise," & _
          "ShortDescription=@ShortDescription," & _
          "ReferanceNumber=@ReferanceNumber," & _
          "OperationDate=@OperationDate," & _
          "OperationExpireDate=@OperationExpireDate," & _
          "AcceptedDate=@AcceptedDate," & _
          "ExpireDate=@ExpireDate," & _
          "PostUserDate=@PostUserDate," & _
          "PostDate=@PostDate," & _
          "DelivaryDate=@DelivaryDate," & _
          "PostUserID=@PostUserID," & _
          "AcceptedUserID=@AcceptedUserID," & _
          "RequiredFor=@RequiredFor," & _
          "SalespersonID=@SalespersonID," & _
          "DepartmentID=@DepartmentID," & _
          "ProjectID=@ProjectID," & _
          "WorkOrderID=@WorkOrderID," & _
          "CurrencyID=@CurrencyID," & _
          "CurrencyExchange=@CurrencyExchange," & _
          "CustomerID=@CustomerID," & _
          "LocationID=@LocationID," & _
          "DocumentsStatus=@DocumentsStatus," & _
          "ModeOfDelivary=@ModeOfDelivary," & _
          "Priority=@Priority," & _
          "StatusID=@StatusID," & _
          "ShipperID=@ShipperID," & _
          "CustomClearanceID=@CustomClearanceID," & _
          "DeliveryTermsTypeID=@DeliveryTermsTypeID," & _
          "AllowPartialShipments=@AllowPartialShipments," & _
          "PaymentTermDays=@PaymentTermDays," & _
          "CashDiscountDays=@CashDiscountDays," & _
          "CashDiscountDiscPercent=@CashDiscountDiscPercent," & _
          "PerdiodCount=@PerdiodCount," & _
          "NoOfPayments=@NoOfPayments," & _
          "QtyCount=@QtyCount," & _
          "LowestAmounTToPay=@LowestAmounTToPay," & _
          "PayBy=@PayBy," & _
          "IsSwitchBL=@IsSwitchBL," & _
          "HeaderRemarks=@HeaderRemarks," & _
          "FooterRemarks=@FooterRemarks," & _
          "OperationParentID=@OperationParentID," & _
          "FiscalYearPeriodID=@FiscalYearPeriodID," & _
          "Discount1=@Discount1," & _
          "Discount2=@Discount2," & _
          "Discount3=@Discount3," & _
          "OverrideUserID=@OverrideUserID," & _
          "Remarks=@Remarks," & _
          "VendorCode=@VendorCode," & _
          "VendorEngName=@VendorEngName," & _
          "VendorArbName=@VendorArbName," & _
          "VendorID=@VendorID," & _
          "CostCenter1ID=@CostCenter1ID," & _
          "CostCenter2ID=@CostCenter2ID," & _
          "CostCenter3ID=@CostCenter3ID," & _
          "CostCenter4ID=@CostCenter4ID," & _
          "Del_HeadID=@Del_HeadID," & _
          "NetAmount=@NetAmount," & _
          "GrossAmount=@GrossAmount," & _
          "Disc1Amount=@Disc1Amount," & _
          "Disc2Amount=@Disc2Amount," & _
          "Disc3Amount=@Disc3Amount"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mOperationTypeID As Integer
    Private mOperationCategoryID As Integer
    Private mSequenceID As Integer
    Private mNumber As Integer
    Private mRevise As Integer
    Private mShortDescription As String
    Private mReferanceNumber As String
    Private mOperationDate As DateTime
    Private mOperationExpireDate As DateTime
    Private mAcceptedDate As DateTime
    Private mExpireDate As DateTime
    Private mPostUserDate As DateTime
    Private mPostDate As DateTime
    Private mDelivaryDate As DateTime
    Private mPostUserID As Integer
    Private mAcceptedUserID As Integer
    Private mRequiredFor As String
    Private mSalespersonID As Integer
    Private mDepartmentID As Integer
    Private mProjectID As Integer
    Private mWorkOrderID As Integer
    Private mCurrencyID As Integer
    Private mCurrencyExchange As Decimal
    Private mCustomerID As Integer
    Private mLocationID As Integer
    Private mDocumentsStatus As String
    Private mModeOfDelivary As String
    Private mPriority As Object
    Private mStatusID As Integer
    Private mShipperID As String
    Private mCustomClearanceID As Integer
    Private mDeliveryTermsTypeID As Integer
    Private mAllowPartialShipments As Boolean
    Private mPaymentTermDays As Integer
    Private mCashDiscountDays As Integer
    Private mCashDiscountDiscPercent As Decimal
    Private mPerdiodCount As Integer
    Private mNoOfPayments As Integer
    Private mQtyCount As Integer
    Private mLowestAmounTToPay As Decimal
    Private mPayBy As Integer
    Private mIsSwitchBL As Boolean
    Private mHeaderRemarks As String
    Private mFooterRemarks As String
    Private mOperationParentID As Integer
    Private mFiscalYearPeriodID As Integer
    Private mDiscount1 As Decimal
    Private mDiscount2 As Decimal
    Private mDiscount3 As Decimal
    Private mOverrideUserID As Integer
    Private mRemarks As String
    Private mRegDate As DateTime
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mCancelDate As DateTime
    Private mCompanyID As Integer
    Private mVendorCode As String
    Private mVendorEngName As String
    Private mVendorArbName As String
    Private mVendorID As Integer
    Private mCostCenter1ID As Integer
    Private mCostCenter2ID As Integer
    Private mCostCenter3ID As Integer
    Private mCostCenter4ID As Integer
    Private mDel_HeadID As Integer
    Private mNetAmount As Decimal
    Private mGrossAmount As Decimal
    Private mDisc1Amount As Decimal
    Private mDisc2Amount As Decimal
    Private mDisc3Amount As Decimal

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
    Public Property OperationTypeID() As Integer
        Get
            Return mOperationTypeID
        End Get
        Set(ByVal Value As Integer)
            mOperationTypeID = Value
        End Set
    End Property
    Public Property OperationCategoryID() As Integer
        Get
            Return mOperationCategoryID
        End Get
        Set(ByVal Value As Integer)
            mOperationCategoryID = Value
        End Set
    End Property
    Public Property SequenceID() As Integer
        Get
            Return mSequenceID
        End Get
        Set(ByVal Value As Integer)
            mSequenceID = Value
        End Set
    End Property
    Public Property Number() As Integer
        Get
            Return mNumber
        End Get
        Set(ByVal Value As Integer)
            mNumber = Value
        End Set
    End Property
    Public Property Revise() As Integer
        Get
            Return mRevise
        End Get
        Set(ByVal Value As Integer)
            mRevise = Value
        End Set
    End Property
    Public Property ShortDescription() As String
        Get
            Return mShortDescription
        End Get
        Set(ByVal Value As String)
            mShortDescription = Value
        End Set
    End Property
    Public Property ReferanceNumber() As String
        Get
            Return mReferanceNumber
        End Get
        Set(ByVal Value As String)
            mReferanceNumber = Value
        End Set
    End Property
    Public Property OperationDate() As DateTime
        Get
            Return mOperationDate
        End Get
        Set(ByVal Value As DateTime)
            mOperationDate = Value
        End Set
    End Property
    Public Property OperationExpireDate() As DateTime
        Get
            Return mOperationExpireDate
        End Get
        Set(ByVal Value As DateTime)
            mOperationExpireDate = Value
        End Set
    End Property
    Public Property AcceptedDate() As DateTime
        Get
            Return mAcceptedDate
        End Get
        Set(ByVal Value As DateTime)
            mAcceptedDate = Value
        End Set
    End Property
    Public Property ExpireDate() As DateTime
        Get
            Return mExpireDate
        End Get
        Set(ByVal Value As DateTime)
            mExpireDate = Value
        End Set
    End Property
    Public Property PostUserDate() As DateTime
        Get
            Return mPostUserDate
        End Get
        Set(ByVal Value As DateTime)
            mPostUserDate = Value
        End Set
    End Property
    Public Property PostDate() As DateTime
        Get
            Return mPostDate
        End Get
        Set(ByVal Value As DateTime)
            mPostDate = Value
        End Set
    End Property
    Public Property DelivaryDate() As DateTime
        Get
            Return mDelivaryDate
        End Get
        Set(ByVal Value As DateTime)
            mDelivaryDate = Value
        End Set
    End Property
    Public Property PostUserID() As Integer
        Get
            Return mPostUserID
        End Get
        Set(ByVal Value As Integer)
            mPostUserID = Value
        End Set
    End Property
    Public Property AcceptedUserID() As Integer
        Get
            Return mAcceptedUserID
        End Get
        Set(ByVal Value As Integer)
            mAcceptedUserID = Value
        End Set
    End Property
    Public Property RequiredFor() As String
        Get
            Return mRequiredFor
        End Get
        Set(ByVal Value As String)
            mRequiredFor = Value
        End Set
    End Property
    Public Property SalespersonID() As Integer
        Get
            Return mSalespersonID
        End Get
        Set(ByVal Value As Integer)
            mSalespersonID = Value
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
    Public Property ProjectID() As Integer
        Get
            Return mProjectID
        End Get
        Set(ByVal Value As Integer)
            mProjectID = Value
        End Set
    End Property
    Public Property WorkOrderID() As Integer
        Get
            Return mWorkOrderID
        End Get
        Set(ByVal Value As Integer)
            mWorkOrderID = Value
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
    Public Property CurrencyExchange() As Decimal
        Get
            Return mCurrencyExchange
        End Get
        Set(ByVal Value As Decimal)
            mCurrencyExchange = Value
        End Set
    End Property
    Public Property CustomerID() As Integer
        Get
            Return mCustomerID
        End Get
        Set(ByVal Value As Integer)
            mCustomerID = Value
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
    Public Property DocumentsStatus() As String
        Get
            Return mDocumentsStatus
        End Get
        Set(ByVal Value As String)
            mDocumentsStatus = Value
        End Set
    End Property
    Public Property ModeOfDelivary() As String
        Get
            Return mModeOfDelivary
        End Get
        Set(ByVal Value As String)
            mModeOfDelivary = Value
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
    Public Property StatusID() As Integer
        Get
            Return mStatusID
        End Get
        Set(ByVal Value As Integer)
            mStatusID = Value
        End Set
    End Property
    Public Property ShipperID() As String
        Get
            Return mShipperID
        End Get
        Set(ByVal Value As String)
            mShipperID = Value
        End Set
    End Property
    Public Property CustomClearanceID() As Integer
        Get
            Return mCustomClearanceID
        End Get
        Set(ByVal Value As Integer)
            mCustomClearanceID = Value
        End Set
    End Property
    Public Property DeliveryTermsTypeID() As Integer
        Get
            Return mDeliveryTermsTypeID
        End Get
        Set(ByVal Value As Integer)
            mDeliveryTermsTypeID = Value
        End Set
    End Property
    Public Property AllowPartialShipments() As Boolean
        Get
            Return mAllowPartialShipments
        End Get
        Set(ByVal Value As Boolean)
            mAllowPartialShipments = Value
        End Set
    End Property
    Public Property PaymentTermDays() As Integer
        Get
            Return mPaymentTermDays
        End Get
        Set(ByVal Value As Integer)
            mPaymentTermDays = Value
        End Set
    End Property
    Public Property CashDiscountDays() As Integer
        Get
            Return mCashDiscountDays
        End Get
        Set(ByVal Value As Integer)
            mCashDiscountDays = Value
        End Set
    End Property
    Public Property CashDiscountDiscPercent() As Decimal
        Get
            Return mCashDiscountDiscPercent
        End Get
        Set(ByVal Value As Decimal)
            mCashDiscountDiscPercent = Value
        End Set
    End Property
    Public Property PerdiodCount() As Integer
        Get
            Return mPerdiodCount
        End Get
        Set(ByVal Value As Integer)
            mPerdiodCount = Value
        End Set
    End Property
    Public Property NoOfPayments() As Integer
        Get
            Return mNoOfPayments
        End Get
        Set(ByVal Value As Integer)
            mNoOfPayments = Value
        End Set
    End Property
    Public Property QtyCount() As Integer
        Get
            Return mQtyCount
        End Get
        Set(ByVal Value As Integer)
            mQtyCount = Value
        End Set
    End Property
    Public Property LowestAmounTToPay() As Decimal
        Get
            Return mLowestAmounTToPay
        End Get
        Set(ByVal Value As Decimal)
            mLowestAmounTToPay = Value
        End Set
    End Property
    Public Property PayBy() As Integer
        Get
            Return mPayBy
        End Get
        Set(ByVal Value As Integer)
            mPayBy = Value
        End Set
    End Property
    Public Property IsSwitchBL() As Boolean
        Get
            Return mIsSwitchBL
        End Get
        Set(ByVal Value As Boolean)
            mIsSwitchBL = Value
        End Set
    End Property
    Public Property HeaderRemarks() As String
        Get
            Return mHeaderRemarks
        End Get
        Set(ByVal Value As String)
            mHeaderRemarks = Value
        End Set
    End Property
    Public Property FooterRemarks() As String
        Get
            Return mFooterRemarks
        End Get
        Set(ByVal Value As String)
            mFooterRemarks = Value
        End Set
    End Property
    Public Property OperationParentID() As Integer
        Get
            Return mOperationParentID
        End Get
        Set(ByVal Value As Integer)
            mOperationParentID = Value
        End Set
    End Property
    Public Property FiscalYearPeriodID() As Integer
        Get
            Return mFiscalYearPeriodID
        End Get
        Set(ByVal Value As Integer)
            mFiscalYearPeriodID = Value
        End Set
    End Property
    Public Property Discount1() As Decimal
        Get
            Return mDiscount1
        End Get
        Set(ByVal Value As Decimal)
            mDiscount1 = Value
        End Set
    End Property
    Public Property Discount2() As Decimal
        Get
            Return mDiscount2
        End Get
        Set(ByVal Value As Decimal)
            mDiscount2 = Value
        End Set
    End Property
    Public Property Discount3() As Decimal
        Get
            Return mDiscount3
        End Get
        Set(ByVal Value As Decimal)
            mDiscount3 = Value
        End Set
    End Property
    Public Property OverrideUserID() As Integer
        Get
            Return mOverrideUserID
        End Get
        Set(ByVal Value As Integer)
            mOverrideUserID = Value
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
    Public Property RegDate() As DateTime
        Get
            Return mRegDate
        End Get
        Set(ByVal Value As DateTime)
            mRegDate = Value
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
    Public Property CancelDate() As DateTime
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As DateTime)
            mCancelDate = Value
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
    Public Property VendorCode() As String
        Get
            Return mVendorCode
        End Get
        Set(ByVal Value As String)
            mVendorCode = Value
        End Set
    End Property
    Public Property VendorEngName() As String
        Get
            Return mVendorEngName
        End Get
        Set(ByVal Value As String)
            mVendorEngName = Value
        End Set
    End Property
    Public Property VendorArbName() As String
        Get
            Return mVendorArbName
        End Get
        Set(ByVal Value As String)
            mVendorArbName = Value
        End Set
    End Property
    Public Property VendorID() As Integer
        Get
            Return mVendorID
        End Get
        Set(ByVal Value As Integer)
            mVendorID = Value
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
    Public Property Del_HeadID() As Integer
        Get
            Return mDel_HeadID
        End Get
        Set(ByVal Value As Integer)
            mDel_HeadID = Value
        End Set
    End Property
    Public Property NetAmount() As Decimal
        Get
            Return mNetAmount
        End Get
        Set(ByVal Value As Decimal)
            mNetAmount = Value
        End Set
    End Property
    Public Property GrossAmount() As Decimal
        Get
            Return mGrossAmount
        End Get
        Set(ByVal Value As Decimal)
            mGrossAmount = Value
        End Set
    End Property
    Public Property Disc1Amount() As Decimal
        Get
            Return mDisc1Amount
        End Get
        Set(ByVal Value As Decimal)
            mDisc1Amount = Value
        End Set
    End Property
    Public Property Disc2Amount() As Decimal
        Get
            Return mDisc2Amount
        End Get
        Set(ByVal Value As Decimal)
            mDisc2Amount = Value
        End Set
    End Property
    Public Property Disc3Amount() As Decimal
        Get
            Return mDisc3Amount
        End Get
        Set(ByVal Value As Decimal)
            mDisc3Amount = Value
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
    'Date Created   :30/11/2015 13:55:54
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
            mOperationTypeID = 0
            mOperationCategoryID = 0
            mSequenceID = 0
            mNumber = 0
            mRevise = 0
            mShortDescription = String.Empty
            mReferanceNumber = String.Empty
            mOperationDate = Nothing
            mOperationExpireDate = Nothing
            mAcceptedDate = Nothing
            mExpireDate = Nothing
            mPostUserDate = Nothing
            mPostDate = Nothing
            mDelivaryDate = Nothing
            mPostUserID = 0
            mAcceptedUserID = 0
            mRequiredFor = String.Empty
            mSalespersonID = 0
            mDepartmentID = 0
            mProjectID = 0
            mWorkOrderID = 0
            mCurrencyID = 0
            mCurrencyExchange = 0
            mCustomerID = 0
            mLocationID = 0
            mDocumentsStatus = String.Empty
            mModeOfDelivary = String.Empty
            mPriority = Nothing
            mStatusID = 0
            mShipperID = String.Empty
            mCustomClearanceID = 0
            mDeliveryTermsTypeID = 0
            mAllowPartialShipments = False
            mPaymentTermDays = 0
            mCashDiscountDays = 0
            mCashDiscountDiscPercent = 0
            mPerdiodCount = 0
            mNoOfPayments = 0
            mQtyCount = 0
            mLowestAmounTToPay = 0
            mPayBy = 0
            mIsSwitchBL = False
            mHeaderRemarks = String.Empty
            mFooterRemarks = String.Empty
            mOperationParentID = 0
            mFiscalYearPeriodID = 0
            mDiscount1 = 0
            mDiscount2 = 0
            mDiscount3 = 0
            mOverrideUserID = 0
            mRemarks = String.Empty
            mRegDate = Nothing
            mRegUserID = 0
            mRegComputerID = 0
            mCancelDate = Nothing
            mCompanyID = 0
            mVendorCode = String.Empty
            mVendorEngName = String.Empty
            mVendorArbName = String.Empty
            mVendorID = 0
            mCostCenter1ID = 0
            mCostCenter2ID = 0
            mCostCenter3ID = 0
            mCostCenter4ID = 0
            mDel_HeadID = 0
            mNetAmount = 0
            mGrossAmount = 0
            mDisc1Amount = 0
            mDisc2Amount = 0
            mDisc3Amount = 0
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Number ASC"
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 07/08/2007
    'Steps: 
    '               - Execute proc (StrSelectCommand) to find last row in sys_Currencies table
    '               - Fill dataset with result of sqlstatment
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:  Find Last row in sys_Currencies table
    '=====================================================================

    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Number DESC"
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

    '=====================================================================
    'Created by : DataOcean
    'Date : 07/08/2007
    'Steps: 
    '               - Execute proc (StrSelectCommand) to find Next row in sys_Currencies table
    '               - Fill dataset with result of the proc
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:   Find Next row in sys_Currencies table
    '=====================================================================

    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Number >'" & mNumber & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Number ASC"
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

    '==================================================================
    'Created by : DataOcean
    'Date : 07/08/2007
    'Steps: 
    '               - Execute proc (StrSelectCommand) to find previous row in sys_Currencies table
    '               - Fill dataset with result of the proc
    '               - Call Getparameter function to insert the results of dataset into the private members of the class
    'Description:   Find previous row in sys_Currencies table
    '==================================================================

    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Number < '" & mNumber & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Number DESC"
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
                mOperationTypeID = [Shared].DataHandler.DataValue_Out(.Item("OperationTypeID"), SqlDbType.Int, True)
                mOperationCategoryID = [Shared].DataHandler.DataValue_Out(.Item("OperationCategoryID"), SqlDbType.Int, True)
                mSequenceID = [Shared].DataHandler.DataValue_Out(.Item("SequenceID"), SqlDbType.Int, True)
                mNumber = [Shared].DataHandler.DataValue_Out(.Item("Number"), SqlDbType.Int, True)
                mRevise = [Shared].DataHandler.DataValue_Out(.Item("Revise"), SqlDbType.Int, True)
                mShortDescription = [Shared].DataHandler.DataValue_Out(.Item("ShortDescription"), SqlDbType.VarChar)
                mReferanceNumber = [Shared].DataHandler.DataValue_Out(.Item("ReferanceNumber"), SqlDbType.VarChar)
                mOperationDate = [Shared].DataHandler.DataValue_Out(.Item("OperationDate"), SqlDbType.DateTime)
                mOperationExpireDate = [Shared].DataHandler.DataValue_Out(.Item("OperationExpireDate"), SqlDbType.DateTime)
                mAcceptedDate = [Shared].DataHandler.DataValue_Out(.Item("AcceptedDate"), SqlDbType.DateTime)
                mExpireDate = [Shared].DataHandler.DataValue_Out(.Item("ExpireDate"), SqlDbType.DateTime)
                mPostUserDate = [Shared].DataHandler.DataValue_Out(.Item("PostUserDate"), SqlDbType.DateTime)
                mPostDate = [Shared].DataHandler.DataValue_Out(.Item("PostDate"), SqlDbType.DateTime)
                mDelivaryDate = [Shared].DataHandler.DataValue_Out(.Item("DelivaryDate"), SqlDbType.DateTime)
                mPostUserID = [Shared].DataHandler.DataValue_Out(.Item("PostUserID"), SqlDbType.Int, True)
                mAcceptedUserID = [Shared].DataHandler.DataValue_Out(.Item("AcceptedUserID"), SqlDbType.Int, True)
                mRequiredFor = [Shared].DataHandler.DataValue_Out(.Item("RequiredFor"), SqlDbType.VarChar)
                mSalespersonID = [Shared].DataHandler.DataValue_Out(.Item("SalespersonID"), SqlDbType.Int, True)
                mDepartmentID = [Shared].DataHandler.DataValue_Out(.Item("DepartmentID"), SqlDbType.Int, True)
                mProjectID = [Shared].DataHandler.DataValue_Out(.Item("ProjectID"), SqlDbType.Int, True)
                mWorkOrderID = [Shared].DataHandler.DataValue_Out(.Item("WorkOrderID"), SqlDbType.Int, True)
                mCurrencyID = [Shared].DataHandler.DataValue_Out(.Item("CurrencyID"), SqlDbType.Int, True)
                mCurrencyExchange = [Shared].DataHandler.DataValue_Out(.Item("CurrencyExchange"), SqlDbType.Decimal)
                mCustomerID = [Shared].DataHandler.DataValue_Out(.Item("CustomerID"), SqlDbType.Int, True)
                mLocationID = [Shared].DataHandler.DataValue_Out(.Item("LocationID"), SqlDbType.Int, True)
                mDocumentsStatus = [Shared].DataHandler.DataValue_Out(.Item("DocumentsStatus"), SqlDbType.VarChar)
                mModeOfDelivary = [Shared].DataHandler.DataValue_Out(.Item("ModeOfDelivary"), SqlDbType.VarChar)
                mPriority = [Shared].DataHandler.DataValue_Out(.Item("Priority"), SqlDbType.tinyint)
                mStatusID = [Shared].DataHandler.DataValue_Out(.Item("StatusID"), SqlDbType.Int, True)
                mShipperID = [Shared].DataHandler.DataValue_Out(.Item("ShipperID"), SqlDbType.VarChar)
                mCustomClearanceID = [Shared].DataHandler.DataValue_Out(.Item("CustomClearanceID"), SqlDbType.Int, True)
                mDeliveryTermsTypeID = [Shared].DataHandler.DataValue_Out(.Item("DeliveryTermsTypeID"), SqlDbType.Int, True)
                mAllowPartialShipments = [Shared].DataHandler.DataValue_Out(.Item("AllowPartialShipments"), SqlDbType.Bit)
                mPaymentTermDays = [Shared].DataHandler.DataValue_Out(.Item("PaymentTermDays"), SqlDbType.Int, True)
                mCashDiscountDays = [Shared].DataHandler.DataValue_Out(.Item("CashDiscountDays"), SqlDbType.Int, True)
                mCashDiscountDiscPercent = [Shared].DataHandler.DataValue_Out(.Item("CashDiscountDiscPercent"), SqlDbType.Decimal)
                mPerdiodCount = [Shared].DataHandler.DataValue_Out(.Item("PerdiodCount"), SqlDbType.Int, True)
                mNoOfPayments = [Shared].DataHandler.DataValue_Out(.Item("NoOfPayments"), SqlDbType.Int, True)
                mQtyCount = [Shared].DataHandler.DataValue_Out(.Item("QtyCount"), SqlDbType.Int, True)
                mLowestAmounTToPay = [Shared].DataHandler.DataValue_Out(.Item("LowestAmounTToPay"), SqlDbType.Decimal)
                mPayBy = [Shared].DataHandler.DataValue_Out(.Item("PayBy"), SqlDbType.Int, True)
                mIsSwitchBL = [Shared].DataHandler.DataValue_Out(.Item("IsSwitchBL"), SqlDbType.Bit)
                mHeaderRemarks = [Shared].DataHandler.DataValue_Out(.Item("HeaderRemarks"), SqlDbType.VarChar)
                mFooterRemarks = [Shared].DataHandler.DataValue_Out(.Item("FooterRemarks"), SqlDbType.VarChar)
                mOperationParentID = [Shared].DataHandler.DataValue_Out(.Item("OperationParentID"), SqlDbType.Int, True)
                mFiscalYearPeriodID = [Shared].DataHandler.DataValue_Out(.Item("FiscalYearPeriodID"), SqlDbType.Int, True)
                mDiscount1 = [Shared].DataHandler.DataValue_Out(.Item("Discount1"), SqlDbType.Decimal)
                mDiscount2 = [Shared].DataHandler.DataValue_Out(.Item("Discount2"), SqlDbType.Decimal)
                mDiscount3 = [Shared].DataHandler.DataValue_Out(.Item("Discount3"), SqlDbType.Decimal)
                mOverrideUserID = [Shared].DataHandler.DataValue_Out(.Item("OverrideUserID"), SqlDbType.Int, True)
                mRemarks = [Shared].DataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mCompanyID = [Shared].DataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int, True)
                mVendorCode = [Shared].DataHandler.DataValue_Out(.Item("VendorCode"), SqlDbType.VarChar)
                mVendorEngName = [Shared].DataHandler.DataValue_Out(.Item("VendorEngName"), SqlDbType.VarChar)
                mVendorArbName = [Shared].DataHandler.DataValue_Out(.Item("VendorArbName"), SqlDbType.VarChar)
                mVendorID = [Shared].DataHandler.DataValue_Out(.Item("VendorID"), SqlDbType.Int, True)
                mCostCenter1ID = [Shared].DataHandler.DataValue_Out(.Item("CostCenter1ID"), SqlDbType.Int, True)
                mCostCenter2ID = [Shared].DataHandler.DataValue_Out(.Item("CostCenter2ID"), SqlDbType.Int, True)
                mCostCenter3ID = [Shared].DataHandler.DataValue_Out(.Item("CostCenter3ID"), SqlDbType.Int, True)
                mCostCenter4ID = [Shared].DataHandler.DataValue_Out(.Item("CostCenter4ID"), SqlDbType.Int, True)
                mDel_HeadID = [Shared].DataHandler.DataValue_Out(.Item("Del_HeadID"), SqlDbType.Int, True)
                mNetAmount = [Shared].DataHandler.DataValue_Out(.Item("NetAmount"), SqlDbType.Decimal)
                mGrossAmount = [Shared].DataHandler.DataValue_Out(.Item("GrossAmount"), SqlDbType.Decimal)
                mDisc1Amount = [Shared].DataHandler.DataValue_Out(.Item("Disc1Amount"), SqlDbType.Decimal)
                mDisc2Amount = [Shared].DataHandler.DataValue_Out(.Item("Disc2Amount"), SqlDbType.Decimal)
                mDisc3Amount = [Shared].DataHandler.DataValue_Out(.Item("Disc3Amount"), SqlDbType.Decimal)
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
    'Date Created   : 30/11/2015 13:55:54
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OperationTypeID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mOperationTypeID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OperationCategoryID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mOperationCategoryID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SequenceID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mSequenceID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Number", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mNumber, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Revise", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mRevise, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ShortDescription", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mShortDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReferanceNumber", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mReferanceNumber, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OperationDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mOperationDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OperationExpireDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mOperationExpireDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AcceptedDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mAcceptedDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExpireDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mExpireDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PostUserDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mPostUserDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PostDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mPostDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DelivaryDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mDelivaryDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PostUserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPostUserID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AcceptedUserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mAcceptedUserID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RequiredFor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mRequiredFor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SalespersonID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mSalespersonID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DepartmentID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mDepartmentID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ProjectID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mProjectID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@WorkOrderID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mWorkOrderID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CurrencyID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCurrencyID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CurrencyExchange", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mCurrencyExchange, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CustomerID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCustomerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LocationID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mLocationID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DocumentsStatus", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mDocumentsStatus, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ModeOfDelivary", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mModeOfDelivary, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Priority", SqlDbType.tinyint)).Value = [Shared].DataHandler.DataValue_In(mPriority, SqlDbType.tinyint)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@StatusID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mStatusID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ShipperID", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mShipperID, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CustomClearanceID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCustomClearanceID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DeliveryTermsTypeID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mDeliveryTermsTypeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AllowPartialShipments", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mAllowPartialShipments, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PaymentTermDays", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPaymentTermDays, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CashDiscountDays", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCashDiscountDays, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CashDiscountDiscPercent", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mCashDiscountDiscPercent, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PerdiodCount", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPerdiodCount, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NoOfPayments", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mNoOfPayments, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@QtyCount", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mQtyCount, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LowestAmounTToPay", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mLowestAmounTToPay, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PayBy", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mPayBy, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsSwitchBL", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsSwitchBL, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderRemarks", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mHeaderRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterRemarks", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mFooterRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OperationParentID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mOperationParentID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FiscalYearPeriodID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mFiscalYearPeriodID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Discount1", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mDiscount1, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Discount2", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mDiscount2, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Discount3", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mDiscount3, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OverrideUserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mOverrideUserID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VendorCode", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mVendorCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VendorEngName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mVendorEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VendorArbName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mVendorArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VendorID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mVendorID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenter1ID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCostCenter1ID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenter2ID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCostCenter2ID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenter3ID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCostCenter3ID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenter4ID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCostCenter4ID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Del_HeadID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mDel_HeadID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NetAmount", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mNetAmount, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GrossAmount", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mGrossAmount, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Disc1Amount", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mDisc1Amount, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Disc2Amount", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mDisc2Amount, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Disc3Amount", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mDisc3Amount, SqlDbType.Decimal)
            If (strMode.Trim.ToUpper = "SAVE") Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
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
