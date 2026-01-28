Imports System.Web.UI.WebControls
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_ExitReEntryVisa
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_ExitReEntryVisa "
        mInsertParameter = "" & _
          "VisaType," & _
          "ResidentIssueType," & _
          "ResidentIssueMonthsPeriod," & _
          "TravelVisaType," & _
          "TravelVisaDaysPeriod," & _
          "TransferBailType," & _
          "TransferBailOther," & _
          "EmployeeID," & _
          "PassportNo," & _
          "PassportIssueDate," & _
          "PassportIssueDate_D," & _
          "PassportExpireDate," & _
          "PassportExpireDate_D," & _
          "IssueCountryID," & _
          "ResidentNo," & _
          "ResidentExpireDate," & _
          "ResidentExpireDate_D," & _
          "ResidentSponsorNo," & _
          "ResidentSponsorOrganizationType," & _
          "SponsorName," & _
          "SponsorAddress," & _
          "SponsorTel," & _
          "BorderEntNo," & _
          "EntDate," & _
          "EntDate_D," & _
          "EntOutlet," & _
          "TransferBailOrganizationType," & _
          "TransferBailNewSponsorName," & _
          "TransferBailNewSponsorNo," & _
          "TransferBailNewSponsorNo2," & _
          "TransferBailNewSponsorAddress," & _
          "TransferorSponsorName," & _
          "TransferorSponsorNo," & _
          "Remarks," & _
          "RegUserID," & _
          "RegComputerID," & _
          "CompanyID"
        mInsertParameterValues = "" & _
          " @VisaType," & _
          " @ResidentIssueType," & _
          " @ResidentIssueMonthsPeriod," & _
          " @TravelVisaType," & _
          " @TravelVisaDaysPeriod," & _
          " @TransferBailType," & _
          " @TransferBailOther," & _
          " @EmployeeID," & _
          " @PassportNo," & _
          " @PassportIssueDate," & _
          " @PassportIssueDate_D," & _
          " @PassportExpireDate," & _
          " @PassportExpireDate_D," & _
          " @IssueCountryID," & _
          " @ResidentNo," & _
          " @ResidentExpireDate," & _
          " @ResidentExpireDate_D," & _
          " @ResidentSponsorNo," & _
          " @ResidentSponsorOrganizationType," & _
          " @SponsorName," & _
          " @SponsorAddress," & _
          " @SponsorTel," & _
          " @BorderEntNo," & _
          " @EntDate," & _
          " @EntDate_D," & _
          " @EntOutlet," & _
          " @TransferBailOrganizationType," & _
          " @TransferBailNewSponsorName," & _
          " @TransferBailNewSponsorNo," & _
          " @TransferBailNewSponsorNo2," & _
          " @TransferBailNewSponsorAddress," & _
          " @TransferorSponsorName," & _
          " @TransferorSponsorNo," & _
          " @Remarks," & _
          " @RegUserID," & _
          " @RegComputerID," & _
          " @CompanyID"
        mUpdateParameter = "" & _
          "VisaType=@VisaType," & _
          "ResidentIssueType=@ResidentIssueType," & _
          "ResidentIssueMonthsPeriod=@ResidentIssueMonthsPeriod," & _
          "TravelVisaType=@TravelVisaType," & _
          "TravelVisaDaysPeriod=@TravelVisaDaysPeriod," & _
          "TransferBailType=@TransferBailType," & _
          "TransferBailOther=@TransferBailOther," & _
          "EmployeeID=@EmployeeID," & _
          "PassportNo=@PassportNo," & _
          "PassportIssueDate=@PassportIssueDate," & _
          "PassportIssueDate_D=@PassportIssueDate_D," & _
          "PassportExpireDate=@PassportExpireDate," & _
          "PassportExpireDate_D=@PassportExpireDate_D," & _
          "IssueCountryID=@IssueCountryID," & _
          "ResidentNo=@ResidentNo," & _
          "ResidentExpireDate=@ResidentExpireDate," & _
          "ResidentExpireDate_D=@ResidentExpireDate_D," & _
          "ResidentSponsorNo=@ResidentSponsorNo," & _
          "ResidentSponsorOrganizationType=@ResidentSponsorOrganizationType," & _
          "SponsorName=@SponsorName," & _
          "SponsorAddress=@SponsorAddress," & _
          "SponsorTel=@SponsorTel," & _
          "BorderEntNo=@BorderEntNo," & _
          "EntDate=@EntDate," & _
          "EntDate_D=@EntDate_D," & _
          "EntOutlet=@EntOutlet," & _
          "TransferBailOrganizationType=@TransferBailOrganizationType," & _
          "TransferBailNewSponsorName=@TransferBailNewSponsorName," & _
          "TransferBailNewSponsorNo=@TransferBailNewSponsorNo," & _
          "TransferBailNewSponsorNo2=@TransferBailNewSponsorNo2," & _
          "TransferBailNewSponsorAddress=@TransferBailNewSponsorAddress," & _
          "TransferorSponsorName=@TransferorSponsorName," & _
          "TransferorSponsorNo=@TransferorSponsorNo," & _
          "Remarks=@Remarks"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " Insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ");Select IDENT_CURRENT('" & mTable.Trim & "');"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mVisaType As Integer
    Private mResidentIssueType As Integer
    Private mResidentIssueMonthsPeriod As Single
    Private mTravelVisaType As Integer
    Private mTravelVisaDaysPeriod As Single
    Private mTransferBailType As Integer
    Private mTransferBailOther As String
    Private mEmployeeID As Integer
    Private mPassportNo As String
    Private mPassportIssueDate As DateTime
    Private mPassportIssueDate_D As String
    Private mPassportExpireDate As DateTime
    Private mPassportExpireDate_D As String
    Private mIssueCountryID As Integer
    Private mResidentNo As String
    Private mResidentExpireDate As DateTime
    Private mResidentExpireDate_D As String
    Private mResidentSponsorNo As String
    Private mResidentSponsorOrganizationType As Integer
    Private mSponsorName As String
    Private mSponsorAddress As String
    Private mSponsorTel As String
    Private mBorderEntNo As String
    Private mEntDate As DateTime
    Private mEntDate_D As String
    Private mEntOutlet As String
    Private mTransferBailOrganizationType As Integer
    Private mTransferBailNewSponsorName As String
    Private mTransferBailNewSponsorNo As String
    Private mTransferBailNewSponsorNo2 As String
    Private mTransferBailNewSponsorAddress As String
    Private mTransferorSponsorName As String
    Private mTransferorSponsorNo As String
    Private mRemarks As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mCompanyID As Integer
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
    Public Property VisaType() As Integer
        Get
            Return mVisaType
        End Get
        Set(ByVal Value As Integer)
            mVisaType = Value
        End Set
    End Property
    Public Property ResidentIssueType() As Integer
        Get
            Return mResidentIssueType
        End Get
        Set(ByVal Value As Integer)
            mResidentIssueType = Value
        End Set
    End Property
    Public Property ResidentIssueMonthsPeriod() As Single
        Get
            Return mResidentIssueMonthsPeriod
        End Get
        Set(ByVal Value As Single)
            mResidentIssueMonthsPeriod = Value
        End Set
    End Property
    Public Property TravelVisaType() As Integer
        Get
            Return mTravelVisaType
        End Get
        Set(ByVal Value As Integer)
            mTravelVisaType = Value
        End Set
    End Property
    Public Property TravelVisaDaysPeriod() As Single
        Get
            Return mTravelVisaDaysPeriod
        End Get
        Set(ByVal Value As Single)
            mTravelVisaDaysPeriod = Value
        End Set
    End Property
    Public Property TransferBailType() As Integer
        Get
            Return mTransferBailType
        End Get
        Set(ByVal Value As Integer)
            mTransferBailType = Value
        End Set
    End Property
    Public Property TransferBailOther() As String
        Get
            Return mTransferBailOther
        End Get
        Set(ByVal Value As String)
            mTransferBailOther = Value
        End Set
    End Property
    Public Property EmployeeID() As Integer
        Get
            Return mEmployeeID
        End Get
        Set(ByVal Value As Integer)
            mEmployeeID = Value
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
    Public Property PassportIssueDate() As DateTime
        Get
            Return mPassportIssueDate
        End Get
        Set(ByVal Value As DateTime)
            mPassportIssueDate = Value
        End Set
    End Property
    Public Property PassportIssueDate_D() As String
        Get
            Return mPassportIssueDate_D
        End Get
        Set(ByVal Value As String)
            mPassportIssueDate_D = Value
        End Set
    End Property
    Public Property PassportExpireDate() As DateTime
        Get
            Return mPassportExpireDate
        End Get
        Set(ByVal Value As DateTime)
            mPassportExpireDate = Value
        End Set
    End Property
    Public Property PassportExpireDate_D() As String
        Get
            Return mPassportExpireDate_D
        End Get
        Set(ByVal Value As String)
            mPassportExpireDate_D = Value
        End Set
    End Property
    Public Property IssueCountryID() As Integer
        Get
            Return mIssueCountryID
        End Get
        Set(ByVal Value As Integer)
            mIssueCountryID = Value
        End Set
    End Property
    Public Property ResidentNo() As String
        Get
            Return mResidentNo
        End Get
        Set(ByVal Value As String)
            mResidentNo = Value
        End Set
    End Property
    Public Property ResidentExpireDate() As DateTime
        Get
            Return mResidentExpireDate
        End Get
        Set(ByVal Value As DateTime)
            mResidentExpireDate = Value
        End Set
    End Property
    Public Property ResidentExpireDate_D() As String
        Get
            Return mResidentExpireDate_D
        End Get
        Set(ByVal Value As String)
            mResidentExpireDate_D = Value
        End Set
    End Property
    Public Property ResidentSponsorNo() As String
        Get
            Return mResidentSponsorNo
        End Get
        Set(ByVal Value As String)
            mResidentSponsorNo = Value
        End Set
    End Property
    Public Property ResidentSponsorOrganizationType() As Integer
        Get
            Return mResidentSponsorOrganizationType
        End Get
        Set(ByVal Value As Integer)
            mResidentSponsorOrganizationType = Value
        End Set
    End Property
    Public Property SponsorName() As String
        Get
            Return mSponsorName
        End Get
        Set(ByVal Value As String)
            mSponsorName = Value
        End Set
    End Property
    Public Property SponsorAddress() As String
        Get
            Return mSponsorAddress
        End Get
        Set(ByVal Value As String)
            mSponsorAddress = Value
        End Set
    End Property
    Public Property SponsorTel() As String
        Get
            Return mSponsorTel
        End Get
        Set(ByVal Value As String)
            mSponsorTel = Value
        End Set
    End Property
    Public Property BorderEntNo() As String
        Get
            Return mBorderEntNo
        End Get
        Set(ByVal Value As String)
            mBorderEntNo = Value
        End Set
    End Property
    Public Property EntDate() As DateTime
        Get
            Return mEntDate
        End Get
        Set(ByVal Value As DateTime)
            mEntDate = Value
        End Set
    End Property
    Public Property EntDate_D() As String
        Get
            Return mEntDate_D
        End Get
        Set(ByVal Value As String)
            mEntDate_D = Value
        End Set
    End Property
    Public Property EntOutlet() As String
        Get
            Return mEntOutlet
        End Get
        Set(ByVal Value As String)
            mEntOutlet = Value
        End Set
    End Property
    Public Property TransferBailOrganizationType() As Integer
        Get
            Return mTransferBailOrganizationType
        End Get
        Set(ByVal Value As Integer)
            mTransferBailOrganizationType = Value
        End Set
    End Property
    Public Property TransferBailNewSponsorName() As String
        Get
            Return mTransferBailNewSponsorName
        End Get
        Set(ByVal Value As String)
            mTransferBailNewSponsorName = Value
        End Set
    End Property
    Public Property TransferBailNewSponsorNo() As String
        Get
            Return mTransferBailNewSponsorNo
        End Get
        Set(ByVal Value As String)
            mTransferBailNewSponsorNo = Value
        End Set
    End Property
    Public Property TransferBailNewSponsorNo2() As String
        Get
            Return mTransferBailNewSponsorNo2
        End Get
        Set(ByVal Value As String)
            mTransferBailNewSponsorNo2 = Value
        End Set
    End Property
    Public Property TransferBailNewSponsorAddress() As String
        Get
            Return mTransferBailNewSponsorAddress
        End Get
        Set(ByVal Value As String)
            mTransferBailNewSponsorAddress = Value
        End Set
    End Property
    Public Property TransferorSponsorName() As String
        Get
            Return mTransferorSponsorName
        End Get
        Set(ByVal Value As String)
            mTransferorSponsorName = Value
        End Set
    End Property
    Public Property TransferorSponsorNo() As String
        Get
            Return mTransferorSponsorNo
        End Get
        Set(ByVal Value As String)
            mTransferorSponsorNo = Value
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
    Public Property CompanyID() As Integer
        Get
            Return mCompanyID
        End Get
        Set(ByVal Value As Integer)
            mCompanyID = Value
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
    'Project        :  Human Resources Module
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean  
    'Date Created   : 17/05/2010
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
    'Project        :  Human Resources Module
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean  
    'Date Created   : 17/05/2010
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetDropDownList(ByVal DdlValues As DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Order By EngName ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()
            If NullNode Then
                Item = New ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/ [ إختر أحد الإختيارات ]")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New ListItem
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
    'Module         : (Human Resources Module)
    'Project        :  Human Resources Module
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :17/05/2010
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :   Save 
    'Module         :   (Human Resources Module)
    'Project        :   Human Resources Module
    'Description    :   Save new record and return true if operation done otherwise report errors in ErrorPage
    'Developer      :   DataOcean   
    'Date Created   :   17/05/2010
    'Modifacations  :   
    'fn. Arguments  :   
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Save() As Integer
        Dim intID As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, "Save")
            mSqlCommand.Connection.Open()
            intID = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            Return intID
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveDependant(ByVal intID As Integer, ByVal uwg As Infragistics.WebUI.UltraWebGrid.UltraWebGrid) As Boolean
        Dim strCmd As String = " Set DateFormat dmy;"
        Dim dteBirthDate As String
        Dim strBirthDate_D As String
        Dim intRelationID As String
        Dim strPassportNo As String
        Dim dteExpiryDate As String
        Dim strExpiryDate_D As String
        Dim strEntOutlet As String
        Dim strBorderEntNo As String
        For Each dr As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwg.Rows
            If (Not IsNothing(dr.Cells.FromKey("Name").Value)) AndAlso dr.Cells.FromKey("Name").Value.ToString.Trim <> String.Empty Then
                If IsNothing(dr.Cells.FromKey("CBirthDate").Value) OrElse dr.Cells.FromKey("CBirthDate").Value.ToString.Trim = String.Empty Then
                    dteBirthDate = "Null"
                    strBirthDate_D = "Null"
                Else
                    dteBirthDate = "'" & SetHigriDate2(Format(CInt(dr.Cells.FromKey("CBirthDate").Value), "##/##/####"), strBirthDate_D) & "'"
                    strBirthDate_D = "'" & strBirthDate_D & "'"
                End If

                If IsNothing(dr.Cells.FromKey("CExpiryDate").Value) OrElse dr.Cells.FromKey("CExpiryDate").Value.ToString.Trim = String.Empty Then
                    dteExpiryDate = "Null"
                    strExpiryDate_D = "Null"
                Else
                    dteExpiryDate = "'" & SetHigriDate2(Format(CInt(dr.Cells.FromKey("CExpiryDate").Value), "##/##/####"), strExpiryDate_D) & "'"
                    strExpiryDate_D = "'" & strExpiryDate_D & "'"
                End If

                If IsNothing(dr.Cells.FromKey("RelationID").Value) OrElse _
                    dr.Cells.FromKey("RelationID").Value = 0 OrElse _
                    dr.Cells.FromKey("RelationID").Value.ToString.Trim = String.Empty Then

                    intRelationID = "Null"
                Else
                    intRelationID = dr.Cells.FromKey("RelationID").Value
                End If

                If IsNothing(dr.Cells.FromKey("PassportNo").Value) OrElse _
                  dr.Cells.FromKey("PassportNo").Value.ToString.Trim = String.Empty Then
                    strPassportNo = "Null"
                Else
                    strPassportNo = "'" & dr.Cells.FromKey("PassportNo").Value & "'"
                End If

                If IsNothing(dr.Cells.FromKey("EntOutlet").Value) OrElse _
                  dr.Cells.FromKey("EntOutlet").Value.ToString.Trim = String.Empty Then
                    strEntOutlet = "Null"
                Else
                    strEntOutlet = "'" & dr.Cells.FromKey("EntOutlet").Value & "'"
                End If

                If IsNothing(dr.Cells.FromKey("BorderEntNo").Value) OrElse _
                  dr.Cells.FromKey("BorderEntNo").Value.ToString.Trim = String.Empty Then
                    strBorderEntNo = "Null"
                Else
                    strBorderEntNo = "'" & dr.Cells.FromKey("BorderEntNo").Value & "'"
                End If

                strCmd &= " Insert Into hrs_ExitReEntryVisaDependants( " & _
                                        "ExitReEntryVisaID," & _
                                        "[No]," & _
                                        "Name," & _
                                        "BrthDate," & _
                                        "BirthDate_D," & _
                                        "RelationID," & _
                                        "PassportNo," & _
                                        "ExpiryDate," & _
                                        "ExpiryDate_D," & _
                                        "EntOutlet," & _
                                        "BorderEntNo)" & _
                               " Values(" & intID & "," & _
                                        "" & dr.Cells.FromKey("No").Value.ToString & "," & _
                                        "'" & dr.Cells.FromKey("Name").Value.ToString & "'," & _
                                        "" & dteBirthDate & "," & _
                                        "" & strBirthDate_D & "," & _
                                        "" & intRelationID & "," & _
                                        "" & strPassportNo & "," & _
                                        "" & dteExpiryDate & "," & _
                                        "" & strExpiryDate_D & "," & _
                                        "" & strEntOutlet & "," & _
                                        "" & strBorderEntNo & ");"

            End If
        Next
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, CommandType.Text, strCmd)
        Return True
    End Function

    '========================================================================
    'ProcedureName  :  Update 
    'Module         : (Human Resources Module)
    'Project        :  Human Resources Module
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :17/05/2010
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
    'Module         : (Human Resources Module)
    'Project        :  Human Resources Module
    'Description    :  Delete Table row (set Cancel Date)
    'Developer      :  DataOcean   
    'Date Created   :17/05/2010 11:11:53 ص
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
    'Module         : (Human Resources Module)
    'Project        :  Human Resources Module
    'Description    :  Clear Table Columns
    'Developer      :  DataOcean   
    'Date Created   :17/05/2010
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
            mVisaType = 0
            mResidentIssueType = 0
            mResidentIssueMonthsPeriod = Nothing
            mTravelVisaType = 0
            mTravelVisaDaysPeriod = Nothing
            mTransferBailType = 0
            mTransferBailOther = String.Empty
            mEmployeeID = 0
            mPassportNo = String.Empty
            mPassportIssueDate = Nothing
            mPassportIssueDate_D = String.Empty
            mPassportExpireDate = Nothing
            mPassportExpireDate_D = String.Empty
            mIssueCountryID = 0
            mResidentNo = String.Empty
            mResidentExpireDate = Nothing
            mResidentExpireDate_D = String.Empty
            mResidentSponsorNo = String.Empty
            mResidentSponsorOrganizationType = 0
            mSponsorName = String.Empty
            mSponsorAddress = String.Empty
            mSponsorTel = String.Empty
            mBorderEntNo = String.Empty
            mEntDate = Nothing
            mEntDate_D = String.Empty
            mEntOutlet = String.Empty
            mTransferBailOrganizationType = 0
            mTransferBailNewSponsorName = String.Empty
            mTransferBailNewSponsorNo = String.Empty
            mTransferBailNewSponsorNo2 = String.Empty
            mTransferBailNewSponsorAddress = String.Empty
            mTransferorSponsorName = String.Empty
            mTransferorSponsorNo = String.Empty
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mCompanyID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetEmployeeData(ByVal intEmpID As Integer, _
                                    ByRef strNationality As String, _
                                    ByRef strProfession As String, _
                                    ByRef strReligion As String, _
                                    ByRef PassportNumber As String, _
                                    ByRef PassportIssueDate As String, _
                                    ByRef PassportExpireDate As String, _
                                    ByRef PassportIssueCityID As DropDownList, _
                                    ByRef ResidentNumber As String, _
                                    ByRef ResidentExpireDate As String) As Boolean
        Dim objNav As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Dim dsResult As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, _
                                                            "hrs_GetEmployeeVisaDetails", intEmpID, objNav.SetLanguage(mPage, "0/1"))
        If dsResult.Tables(0).Rows.Count > 0 Then
            strNationality = IIf(IsDBNull(dsResult.Tables(0).Rows(0)("Nationality")), "", dsResult.Tables(0).Rows(0)("Nationality"))
            strProfession = IIf(IsDBNull(dsResult.Tables(0).Rows(0)("Profession")), "", dsResult.Tables(0).Rows(0)("Profession"))
            strReligion = IIf(IsDBNull(dsResult.Tables(0).Rows(0)("Religion")), "", dsResult.Tables(0).Rows(0)("Religion"))
            If IsDBNull(dsResult.Tables(0).Rows(0)("PassportNumber")) Then
                PassportNumber = ""
            Else
                PassportNumber = dsResult.Tables(0).Rows(0)("PassportNumber")
            End If

            If IsDBNull(dsResult.Tables(0).Rows(0)("PassportIssueDate")) Then
                PassportIssueDate = Nothing
            Else
                '' PassportIssueDate = GetHigriDate2(dsResult.Tables(0).Rows(0)("PassportIssueDate"), _
                ''                       dsResult.Tables(0).Rows(0)("PassportIssueDate_D"))

                PassportIssueDate = ClsDataAcessLayer.GregToHijri(dsResult.Tables(0).Rows(0)("PassportIssueDate"), "ddMMyyyy")
            End If

            If IsDBNull(dsResult.Tables(0).Rows(0)("PassportExpireDate")) Then
                PassportExpireDate = Nothing
            Else
                'PassportExpireDate = GetHigriDate2(dsResult.Tables(0).Rows(0)("PassportExpireDate"), _
                '                           dsResult.Tables(0).Rows(0)("PassportExpireDate_D"))
                PassportExpireDate = ClsDataAcessLayer.GregToHijri(dsResult.Tables(0).Rows(0)("PassportExpireDate"), "ddMMyyyy")
            End If

            If IsDBNull(dsResult.Tables(0).Rows(0)("PassportIssueCityID")) Then
                PassportIssueCityID.SelectedValue = "0"
            Else
                PassportIssueCityID.SelectedValue = dsResult.Tables(0).Rows(0)("PassportIssueCityID")
            End If

            If IsDBNull(dsResult.Tables(0).Rows(0)("ResidentNumber")) Then
                ResidentNumber = Nothing
            Else
                ResidentNumber = dsResult.Tables(0).Rows(0)("ResidentNumber")
            End If

            If IsDBNull(dsResult.Tables(0).Rows(0)("ResidentExpireDate")) Then
                ResidentExpireDate = Nothing
            Else
                'ResidentExpireDate = GetHigriDate2(dsResult.Tables(0).Rows(0)("ResidentExpireDate"), _
                '                          dsResult.Tables(0).Rows(0)("ResidentExpireDate_D"))

                ResidentExpireDate = ClsDataAcessLayer.GregToHijri(dsResult.Tables(0).Rows(0)("ResidentExpireDate"), "ddMMyyyy")
            End If

        End If
        Return True
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
    'Date Created   :17/05/2010
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
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mVisaType = mDataHandler.DataValue_Out(.Item("VisaType"), SqlDbType.Int)
                mResidentIssueType = mDataHandler.DataValue_Out(.Item("ResidentIssueType"), SqlDbType.Int)
                mResidentIssueMonthsPeriod = mDataHandler.DataValue_Out(.Item("ResidentIssueMonthsPeriod"), SqlDbType.Float)
                mTravelVisaType = mDataHandler.DataValue_Out(.Item("TravelVisaType"), SqlDbType.Int)
                mTravelVisaDaysPeriod = mDataHandler.DataValue_Out(.Item("TravelVisaDaysPeriod"), SqlDbType.Float)
                mTransferBailType = mDataHandler.DataValue_Out(.Item("TransferBailType"), SqlDbType.Int)
                mTransferBailOther = mDataHandler.DataValue_Out(.Item("TransferBailOther"), SqlDbType.VarChar)
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mPassportNo = mDataHandler.DataValue_Out(.Item("PassportNo"), SqlDbType.VarChar)
                mPassportIssueDate = mDataHandler.DataValue_Out(.Item("PassportIssueDate"), SqlDbType.DateTime)
                mPassportIssueDate_D = mDataHandler.DataValue_Out(.Item("PassportIssueDate_D"), SqlDbType.VarChar)
                mPassportExpireDate = mDataHandler.DataValue_Out(.Item("PassportExpireDate"), SqlDbType.DateTime)
                mPassportExpireDate_D = mDataHandler.DataValue_Out(.Item("PassportExpireDate_D"), SqlDbType.VarChar)
                mIssueCountryID = mDataHandler.DataValue_Out(.Item("IssueCountryID"), SqlDbType.Int, True)
                mResidentNo = mDataHandler.DataValue_Out(.Item("ResidentNo"), SqlDbType.VarChar)
                mResidentExpireDate = mDataHandler.DataValue_Out(.Item("ResidentExpireDate"), SqlDbType.DateTime)
                mResidentExpireDate_D = mDataHandler.DataValue_Out(.Item("ResidentExpireDate_D"), SqlDbType.VarChar)
                mResidentSponsorNo = mDataHandler.DataValue_Out(.Item("ResidentSponsorNo"), SqlDbType.VarChar)
                mResidentSponsorOrganizationType = mDataHandler.DataValue_Out(.Item("ResidentSponsorOrganizationType"), SqlDbType.Int)
                mSponsorName = mDataHandler.DataValue_Out(.Item("SponsorName"), SqlDbType.VarChar)
                mSponsorAddress = mDataHandler.DataValue_Out(.Item("SponsorAddress"), SqlDbType.VarChar)
                mSponsorTel = mDataHandler.DataValue_Out(.Item("SponsorTel"), SqlDbType.VarChar)
                mBorderEntNo = mDataHandler.DataValue_Out(.Item("BorderEntNo"), SqlDbType.VarChar)
                mEntDate = mDataHandler.DataValue_Out(.Item("EntDate"), SqlDbType.DateTime)
                mEntDate_D = mDataHandler.DataValue_Out(.Item("EntDate_D"), SqlDbType.VarChar)
                mEntOutlet = mDataHandler.DataValue_Out(.Item("EntOutlet"), SqlDbType.VarChar)
                mTransferBailOrganizationType = mDataHandler.DataValue_Out(.Item("TransferBailOrganizationType"), SqlDbType.Int)
                mTransferBailNewSponsorName = mDataHandler.DataValue_Out(.Item("TransferBailNewSponsorName"), SqlDbType.VarChar)
                mTransferBailNewSponsorNo = mDataHandler.DataValue_Out(.Item("TransferBailNewSponsorNo"), SqlDbType.VarChar)
                mTransferBailNewSponsorNo2 = mDataHandler.DataValue_Out(.Item("TransferBailNewSponsorNo2"), SqlDbType.VarChar)
                mTransferBailNewSponsorAddress = mDataHandler.DataValue_Out(.Item("TransferBailNewSponsorAddress"), SqlDbType.VarChar)
                mTransferorSponsorName = mDataHandler.DataValue_Out(.Item("TransferorSponsorName"), SqlDbType.VarChar)
                mTransferorSponsorNo = mDataHandler.DataValue_Out(.Item("TransferorSponsorNo"), SqlDbType.VarChar)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mCompanyID = mDataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int, True)
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
    'Date Created   : 17/05/2010 11:11:53 ص
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VisaType", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mVisaType, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ResidentIssueType", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mResidentIssueType, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ResidentIssueMonthsPeriod", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mResidentIssueMonthsPeriod, SqlDbType.Float)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TravelVisaType", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTravelVisaType, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TravelVisaDaysPeriod", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mTravelVisaDaysPeriod, SqlDbType.Float)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransferBailType", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTransferBailType, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransferBailOther", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTransferBailOther, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PassportNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPassportNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PassportIssueDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mPassportIssueDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PassportIssueDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPassportIssueDate_D, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PassportExpireDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mPassportExpireDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PassportExpireDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPassportExpireDate_D, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IssueCountryID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mIssueCountryID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ResidentNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mResidentNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ResidentExpireDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mResidentExpireDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ResidentExpireDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mResidentExpireDate_D, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ResidentSponsorNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mResidentSponsorNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ResidentSponsorOrganizationType", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mResidentSponsorOrganizationType, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SponsorName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSponsorName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SponsorAddress", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSponsorAddress, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SponsorTel", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSponsorTel, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BorderEntNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBorderEntNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EntDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mEntDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EntDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEntDate_D, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EntOutlet", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEntOutlet, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransferBailOrganizationType", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTransferBailOrganizationType, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransferBailNewSponsorName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTransferBailNewSponsorName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransferBailNewSponsorNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTransferBailNewSponsorNo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransferBailNewSponsorNo2", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTransferBailNewSponsorNo2, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransferBailNewSponsorAddress", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTransferBailNewSponsorAddress, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransferorSponsorName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTransferorSponsorName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransferorSponsorNo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTransferorSponsorNo, SqlDbType.VarChar)
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
#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region
End Class
