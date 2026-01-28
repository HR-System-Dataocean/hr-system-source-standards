Imports Venus.Application.SystemFiles.System
Public Class ClsDwf_DocumentElements
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " Dwf_DocumentElements "
        mInsertParameter = "" & _
          "Code," & _
          "DocumentID," & _
          "ElementType," & _
          "FColor," & _
          "BColor," & _
          "Loc_Top," & _
          "Loc_Left," & _
          "Size_Width," & _
          "Size_Hight," & _
          "TabIndex," & _
          "Image_Src," & _
          "MaxLength," & _
          "Title," & _
          "Align," & _
          "Dir," & _
          "Value," & _
          "ListID," & _
          "FontSize," & _
          "IsMainCode," & _
          "IsRequired," & _
          "SearchID," & _
          "KeyColumn," & _
          "KeyTable," & _
          "KeyRelated," & _
          "CtrlFormat," & _
          "Date_Calendar," & _
          "ZoomingForm," & _
          "IsEnabled," & _
          "FriendlyName," & _
          "Remarks," & _
          "RegUserID," & _
          "RegComputerID"
        mInsertParameterValues = "" & _
          " @Code," & _
          " @DocumentID," & _
          " @ElementType," & _
          " @FColor," & _
          " @BColor," & _
          " @Loc_Top," & _
          " @Loc_Left," & _
          " @Size_Width," & _
          " @Size_Hight," & _
          " @TabIndex," & _
          " @Image_Src," & _
          " @MaxLength," & _
          " @Title," & _
          " @Align," & _
          " @Dir," & _
          " @Value," & _
          " @ListID," & _
          " @FontSize," & _
          " @IsMainCode," & _
          " @IsRequired," & _
          " @SearchID," & _
          " @KeyColumn," & _
          " @KeyTable," & _
          " @KeyRelated," & _
          " @CtrlFormat," & _
          " @Date_Calendar," & _
          " @ZoomingForm," & _
          " @IsEnabled," & _
          " @FriendlyName," & _
          " @Remarks," & _
          " @RegUserID," & _
          " @RegComputerID"
        mUpdateParameter = "" & _
          "Code=@Code," & _
          "DocumentID=@DocumentID," & _
          "ElementType=@ElementType," & _
          "FColor=@FColor," & _
          "BColor=@BColor," & _
          "Loc_Top=@Loc_Top," & _
          "Loc_Left=@Loc_Left," & _
          "Size_Width=@Size_Width," & _
          "Size_Hight=@Size_Hight," & _
          "TabIndex=@TabIndex," & _
          "Image_Src=@Image_Src," & _
          "MaxLength=@MaxLength," & _
          "Title=@Title," & _
          "Align=@Align," & _
          "Dir=@Dir," & _
          "Value=@Value," & _
          "ListID=@ListID," & _
          "FontSize=@FontSize," & _
          "IsMainCode=@IsMainCode," & _
          "IsRequired=@IsRequired," & _
          "SearchID=@SearchID," & _
          "KeyColumn=@KeyColumn," & _
          "KeyTable=@KeyTable," & _
          "KeyRelated=@KeyRelated," & _
          "CtrlFormat=@CtrlFormat," & _
          "Date_Calendar=@Date_Calendar," & _
          "ZoomingForm=@ZoomingForm," & _
          "IsEnabled=@IsEnabled," & _
          "FriendlyName=@FriendlyName," & _
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
    Private mDocumentID As Integer
    Private mElementType As String
    Private mFColor As String
    Private mBColor As String
    Private mLoc_Top As String
    Private mLoc_Left As String
    Private mSize_Width As String
    Private mSize_Hight As String
    Private mTabIndex As String
    Private mImage_Src As String
    Private mMaxLength As String
    Private mTitle As String
    Private mAlign As String
    Private mDir As String
    Private mValue As String
    Private mListID As Integer
    Private mFontSize As Integer
    Private mIsMainCode As Boolean
    Private mIsRequired As Boolean
    Private mSearchID As Integer
    Private mKeyColumn As String
    Private mKeyTable As String
    Private mKeyRelated As String
    Private mCtrlFormat As String
    Private mDate_Calendar As String
    Private mZoomingForm As String
    Private mIsEnabled As Boolean
    Private mFriendlyName As String
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
    Public Property DocumentID() As Integer
        Get
            Return mDocumentID
        End Get
        Set(ByVal Value As Integer)
            mDocumentID = Value
        End Set
    End Property
    Public Property ElementType() As String
        Get
            Return mElementType
        End Get
        Set(ByVal Value As String)
            mElementType = Value
        End Set
    End Property
    Public Property FColor() As String
        Get
            Return mFColor
        End Get
        Set(ByVal Value As String)
            mFColor = Value
        End Set
    End Property
    Public Property BColor() As String
        Get
            Return mBColor
        End Get
        Set(ByVal Value As String)
            mBColor = Value
        End Set
    End Property
    Public Property Loc_Top() As String
        Get
            Return mLoc_Top
        End Get
        Set(ByVal Value As String)
            mLoc_Top = Value
        End Set
    End Property
    Public Property Loc_Left() As String
        Get
            Return mLoc_Left
        End Get
        Set(ByVal Value As String)
            mLoc_Left = Value
        End Set
    End Property
    Public Property Size_Width() As String
        Get
            Return mSize_Width
        End Get
        Set(ByVal Value As String)
            mSize_Width = Value
        End Set
    End Property
    Public Property Size_Hight() As String
        Get
            Return mSize_Hight
        End Get
        Set(ByVal Value As String)
            mSize_Hight = Value
        End Set
    End Property
    Public Property TabIndex() As String
        Get
            Return mTabIndex
        End Get
        Set(ByVal Value As String)
            mTabIndex = Value
        End Set
    End Property
    Public Property Image_Src() As String
        Get
            Return mImage_Src
        End Get
        Set(ByVal Value As String)
            mImage_Src = Value
        End Set
    End Property
    Public Property MaxLength() As String
        Get
            Return mMaxLength
        End Get
        Set(ByVal Value As String)
            mMaxLength = Value
        End Set
    End Property
    Public Property Title() As String
        Get
            Return mTitle
        End Get
        Set(ByVal Value As String)
            mTitle = Value
        End Set
    End Property
    Public Property Align() As String
        Get
            Return mAlign
        End Get
        Set(ByVal Value As String)
            mAlign = Value
        End Set
    End Property
    Public Property Dir() As String
        Get
            Return mDir
        End Get
        Set(ByVal Value As String)
            mDir = Value
        End Set
    End Property
    Public Property Value() As String
        Get
            Return mValue
        End Get
        Set(ByVal Value As String)
            mValue = Value
        End Set
    End Property
    Public Property ListID() As Integer
        Get
            Return mListID
        End Get
        Set(ByVal Value As Integer)
            mListID = Value
        End Set
    End Property
    Public Property FontSize() As Integer
        Get
            Return mFontSize
        End Get
        Set(ByVal Value As Integer)
            mFontSize = Value
        End Set
    End Property
    Public Property IsMainCode() As Boolean
        Get
            Return mIsMainCode
        End Get
        Set(ByVal Value As Boolean)
            mIsMainCode = Value
        End Set
    End Property
    Public Property IsRequired() As Boolean
        Get
            Return mIsRequired
        End Get
        Set(ByVal Value As Boolean)
            mIsRequired = Value
        End Set
    End Property
    Public Property SearchID() As Integer
        Get
            Return mSearchID
        End Get
        Set(ByVal Value As Integer)
            mSearchID = Value
        End Set
    End Property
    Public Property KeyColumn() As String
        Get
            Return mKeyColumn
        End Get
        Set(ByVal Value As String)
            mKeyColumn = Value
        End Set
    End Property
    Public Property KeyTable() As String
        Get
            Return mKeyTable
        End Get
        Set(ByVal Value As String)
            mKeyTable = Value
        End Set
    End Property
    Public Property KeyRelated() As String
        Get
            Return mKeyRelated
        End Get
        Set(ByVal Value As String)
            mKeyRelated = Value
        End Set
    End Property
    Public Property CtrlFormat() As String
        Get
            Return mCtrlFormat
        End Get
        Set(ByVal Value As String)
            mCtrlFormat = Value
        End Set
    End Property
    Public Property Date_Calendar() As String
        Get
            Return mDate_Calendar
        End Get
        Set(ByVal Value As String)
            mDate_Calendar = Value
        End Set
    End Property
    Public Property ZoomingForm() As String
        Get
            Return mZoomingForm
        End Get
        Set(ByVal Value As String)
            mZoomingForm = Value
        End Set
    End Property
    Public Property IsEnabled() As Boolean
        Get
            Return mIsEnabled
        End Get
        Set(ByVal Value As Boolean)
            mIsEnabled = Value
        End Set
    End Property
    Public Property FriendlyName() As String
        Get
            Return mFriendlyName
        End Get
        Set(ByVal Value As String)
            mFriendlyName = Value
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
    'Date Created   : 14/09/2015
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
    'Date Created   : 14/09/2015
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
    'Date Created   :14/09/2015
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
    'Date Created   :   14/09/2015
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
    'Date Created   :14/09/2015
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
    'Date Created   :14/09/2015 11:48:17
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
    'Date Created   :14/09/2015
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
            mDocumentID = 0
            mElementType = String.Empty
            mFColor = String.Empty
            mBColor = String.Empty
            mLoc_Top = String.Empty
            mLoc_Left = String.Empty
            mSize_Width = String.Empty
            mSize_Hight = String.Empty
            mTabIndex = String.Empty
            mImage_Src = String.Empty
            mMaxLength = String.Empty
            mTitle = String.Empty
            mAlign = String.Empty
            mDir = String.Empty
            mValue = String.Empty
            mListID = 0
            mFontSize = 0
            mIsMainCode = False
            mIsRequired = False
            mSearchID = 0
            mKeyColumn = String.Empty
            mKeyTable = String.Empty
            mKeyRelated = String.Empty
            mCtrlFormat = String.Empty
            mDate_Calendar = String.Empty
            mZoomingForm = String.Empty
            mIsEnabled = False
            mFriendlyName = String.Empty
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
    'Date Created   :14/09/2015
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
                mDocumentID = [Shared].DataHandler.DataValue_Out(.Item("DocumentID"), SqlDbType.Int, True)
                mElementType = [Shared].DataHandler.DataValue_Out(.Item("ElementType"), SqlDbType.VarChar)
                mFColor = [Shared].DataHandler.DataValue_Out(.Item("FColor"), SqlDbType.VarChar)
                mBColor = [Shared].DataHandler.DataValue_Out(.Item("BColor"), SqlDbType.VarChar)
                mLoc_Top = [Shared].DataHandler.DataValue_Out(.Item("Loc_Top"), SqlDbType.VarChar)
                mLoc_Left = [Shared].DataHandler.DataValue_Out(.Item("Loc_Left"), SqlDbType.VarChar)
                mSize_Width = [Shared].DataHandler.DataValue_Out(.Item("Size_Width"), SqlDbType.VarChar)
                mSize_Hight = [Shared].DataHandler.DataValue_Out(.Item("Size_Hight"), SqlDbType.VarChar)
                mTabIndex = [Shared].DataHandler.DataValue_Out(.Item("TabIndex"), SqlDbType.VarChar)
                mImage_Src = [Shared].DataHandler.DataValue_Out(.Item("Image_Src"), SqlDbType.VarChar)
                mMaxLength = [Shared].DataHandler.DataValue_Out(.Item("MaxLength"), SqlDbType.VarChar)
                mTitle = [Shared].DataHandler.DataValue_Out(.Item("Title"), SqlDbType.VarChar)
                mAlign = [Shared].DataHandler.DataValue_Out(.Item("Align"), SqlDbType.VarChar)
                mDir = [Shared].DataHandler.DataValue_Out(.Item("Dir"), SqlDbType.VarChar)
                mValue = [Shared].DataHandler.DataValue_Out(.Item("Value"), SqlDbType.VarChar)
                mListID = [Shared].DataHandler.DataValue_Out(.Item("ListID"), SqlDbType.Int, True)
                mFontSize = [Shared].DataHandler.DataValue_Out(.Item("FontSize"), SqlDbType.Int, True)
                mIsMainCode = [Shared].DataHandler.DataValue_Out(.Item("IsMainCode"), SqlDbType.Bit)
                mIsRequired = [Shared].DataHandler.DataValue_Out(.Item("IsRequired"), SqlDbType.Bit)
                mSearchID = [Shared].DataHandler.DataValue_Out(.Item("SearchID"), SqlDbType.Int, True)
                mKeyColumn = [Shared].DataHandler.DataValue_Out(.Item("KeyColumn"), SqlDbType.VarChar)
                mKeyTable = [Shared].DataHandler.DataValue_Out(.Item("KeyTable"), SqlDbType.VarChar)
                mKeyRelated = [Shared].DataHandler.DataValue_Out(.Item("KeyRelated"), SqlDbType.VarChar)
                mCtrlFormat = [Shared].DataHandler.DataValue_Out(.Item("CtrlFormat"), SqlDbType.VarChar)
                mDate_Calendar = [Shared].DataHandler.DataValue_Out(.Item("Date_Calendar"), SqlDbType.VarChar)
                mZoomingForm = [Shared].DataHandler.DataValue_Out(.Item("ZoomingForm"), SqlDbType.VarChar)
                mIsEnabled = [Shared].DataHandler.DataValue_Out(.Item("IsEnabled"), SqlDbType.Bit)
                mFriendlyName = [Shared].DataHandler.DataValue_Out(.Item("FriendlyName"), SqlDbType.VarChar)
                mRemarks = [Shared].DataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
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
    'Date Created   : 14/09/2015 11:48:17
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DocumentID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mDocumentID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ElementType", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mElementType, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FColor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mFColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BColor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mBColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Loc_Top", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mLoc_Top, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Loc_Left", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mLoc_Left, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Size_Width", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mSize_Width, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Size_Hight", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mSize_Hight, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TabIndex", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mTabIndex, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Image_Src", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mImage_Src, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaxLength", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mMaxLength, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Title", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mTitle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Align", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mAlign, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Dir", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mDir, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Value", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mValue, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ListID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mListID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FontSize", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsMainCode", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsMainCode, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsRequired", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsRequired, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mSearchID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@KeyColumn", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mKeyColumn, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@KeyTable", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mKeyTable, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@KeyRelated", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mKeyRelated, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CtrlFormat", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mCtrlFormat, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Date_Calendar", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mDate_Calendar, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ZoomingForm", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mZoomingForm, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsEnabled", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsEnabled, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FriendlyName", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mFriendlyName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            If (strMode.Trim.ToUpper = "SAVE") Then
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
    'Date Created   : 14/09/2015 11:48:17
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
