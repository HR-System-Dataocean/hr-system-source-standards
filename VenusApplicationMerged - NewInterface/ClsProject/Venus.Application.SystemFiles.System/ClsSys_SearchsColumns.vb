'======================================================================
'Project name  : Venus V. 
'Program name  : Clssys_SearchsColumns.vb 
'Date Created  : DatOcean  
'Date Modified : 16-07-2007  
'Issue #       :       
'Developer     : DataOcean 
'              : [0256]
'Description   : The Class Tier for frmSearchSetting.aspx.vb Page   
'Modifacations: 
'              :  B#000001  160707 Change mTable to sys_SearchsColumns. 
'              :  E#000001  170707 The SQL Select Chnaged due to DataBase Normalization . 
'              :  E#000003  170707 IsView Column is added to sys_seachsColumns DataBase Table To Hold IsView Value For a given Field.  
'              :  E#000004  170707 Add IsView Property and mIsView Private Member. 
'              :  E#000005  170707 Add Field IsView to Update and Insert SQL Statments. 
'              :  B#000002  160707 Field IsAvailable Is Removed From sys_SearchsColumns. 
'              :  E#000006  180707 Use sys_Fields.FieldLength as Actual Field Lenght 
'              :  E#000007  180707 Add sys_FIELDS.Fieldname  to Query String and add a property for it
'              :  B#000003  190707 Change QueryString that retrieves languageColumn   with '' CASE  WHEN clause '' 
'              : B#g0010 [0256] 23-06-2008 Add _Forgin arrgument to data_ValueIn method in SetParameters Function 
'              :                           And _Forgin arrgument to data_value_Out Method in GetParameters Function 
'              :                           To avoid saving non-existing forign key in any table 
'              :                           And convert the DBnull values to zero in case of forign key fields only 
'=========================================================================


Public Class Clssys_SearchsColumns
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_SearchsColumns "
        'mInsertParameter = " SearchID,FieldID,ColumnEngDescription,ColumnArbDescription,IsCriteria,ColumnLength,ColumnLanguage,ColumnAlignment,Rank,SubSearchID,RegUserID ,IsView"
        'mInsertParameterValues = " @SearchID,@FieldID,@ColumnEngDescription,@ColumnArbDescription,@IsCriteria,@ColumnLength,@ColumnLanguage,@ColumnAlignment,@Rank,@SubSearchID,@RegUserID ,@IsView "
        'mUpdateParameter = " SearchID=@SearchID,FieldID=@FieldID,ColumnEngDescription=@ColumnEngDescription,ColumnArbDescription=@ColumnArbDescription,IsCriteria=@IsCriteria,ColumnLength=@ColumnLength,ColumnLanguage=@ColumnLanguage,ColumnAlignment=@ColumnAlignment,Rank=@Rank,SubSearchID=@SubSearchID,RegUserID=@RegUserID ,IsView = @IsView "
        mInsertParameter = " SearchID,FieldID,Engname,Arbname,IsCriteria,InputLength,IsArabic,Alignment,RankCriteria,RankView,IsTarget,SubSearchID,RegUserID ,IsView"
        mInsertParameterValues = " @SearchID,@FieldID,@Engname,@Arbname,@IsCriteria,@InputLength,@IsArabic,@Alignment,@RankCriteria,@RankView,@IsTarget,@SubSearchID,@RegUserID ,@IsView "
        mUpdateParameter = " SearchID=@SearchID,FieldID=@FieldID,Engname=@Engname,Arbname=@Arbname,IsCriteria=@IsCriteria,InputLength=@InputLength,IsArabic=@IsArabic,Alignment=@Alignment,RankCriteria=@RankCriteria,RankView=@RankView,IsTarget=@IsTarget,SubSearchID=@SubSearchID,RegUserID=@RegUserID ,IsView = @IsView,RegComputerID=@RegComputerID "
        'mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
        ' 180707 Use sys_Fields.FieldLength as Actual Field Lenght
        'mSelectCommand = " select sys_SearchsColumns.id , sys_SearchsColumns.searchID , sys_SearchsColumns.fieldid ,sys_SearchsColumns.ColumnEngDescription ,sys_SearchsColumns.ColumnArbDescription , sys_SearchsColumns.IsCriteria , sys_SearchsColumns.ColumnLength ,  sys_SearchsColumns.ColumnLanguage ,  sys_SearchsColumns.ColumnAlignment , sys_SearchsColumns.Rank , sys_SearchsColumns.SubSearchID ,  sys_SearchsColumns.RegUserID ,sys_SearchsColumns.Regdate ,sys_SearchsColumns.cancelDate ,sys_SearchsColumns.IsView , sys_Fields.FieldName from sys_SearchsColumns " & " inner join sys_Searchs on sys_searchscolumns.searchid = sys_Searchs.id inner join sys_Fields on sys_searchs.objectid = sys_Fields.objectid  and  sys_searchscolumns.FieldID = sys_fields.id "
        'mSelectCommand = " select sys_Fields.objectid,sys_SearchsColumns.id , sys_SearchsColumns.searchID , sys_SearchsColumns.fieldid ,sys_SearchsColumns.ColumnEngDescription ,sys_SearchsColumns.ColumnArbDescription , sys_SearchsColumns.IsCriteria , sys_SearchsColumns.ColumnLength ,sys_FIELDS.FieldLength ,  sys_SearchsColumns.ColumnLanguage ,  sys_SearchsColumns.ColumnAlignment , sys_SearchsColumns.Rank , sys_SearchsColumns.SubSearchID ,  sys_SearchsColumns.RegUserID ,sys_SearchsColumns.Regdate ,sys_SearchsColumns.cancelDate ,sys_SearchsColumns.IsView , sys_Fields.FieldName from sys_SearchsColumns " & " inner join sys_Searchs on sys_searchscolumns.searchid = sys_Searchs.id inner join sys_Fields on sys_searchs.objectid = sys_Fields.objectid  and  sys_searchscolumns.FieldID = sys_fields.id "
        ' B#000010 Add columnType to QueryString  (sys_fields.FieldType)
        'mSelectCommand = " select sys_Fields.objectid,sys_SearchsColumns.id , sys_SearchsColumns.searchID , sys_SearchsColumns.fieldid ,sys_SearchsColumns.ColumnEngDescription ,sys_SearchsColumns.ColumnArbDescription , sys_SearchsColumns.IsCriteria , sys_SearchsColumns.ColumnLength ,sys_FIELDS.FieldLength , case when  sys_SearchsColumns.ColumnLanguage = '0' then 'English' WHEN sys_SearchsColumns.ColumnLanguage = '1' THEN 'Arabic' ELSE 'Both' END  As ColumnLanguage ,  sys_SearchsColumns.ColumnAlignment , sys_SearchsColumns.Rank , sys_SearchsColumns.SubSearchID ,  sys_SearchsColumns.RegUserID ,sys_SearchsColumns.Regdate ,sys_SearchsColumns.cancelDate ,sys_SearchsColumns.IsView , sys_Fields.FieldName from sys_SearchsColumns " & " inner join sys_Searchs on sys_searchscolumns.searchid = sys_Searchs.id inner join sys_Fields on sys_searchs.objectid = sys_Fields.objectid  and  sys_searchscolumns.FieldID = sys_fields.id "
        'mSelectCommand = " select sys_Fields.objectid,sys_fields.FieldType , sys_SearchsColumns.id , sys_SearchsColumns.searchID , sys_SearchsColumns.fieldid ,sys_SearchsColumns.ColumnEngDescription ,sys_SearchsColumns.ColumnArbDescription , sys_SearchsColumns.IsCriteria , sys_SearchsColumns.ColumnLength ,sys_FIELDS.FieldLength , case when  sys_SearchsColumns.ColumnLanguage = '0' then 'English' WHEN sys_SearchsColumns.ColumnLanguage = '1' THEN 'Arabic' ELSE 'Both' END  As ColumnLanguage ,  sys_SearchsColumns.ColumnAlignment , sys_SearchsColumns.Rank , sys_SearchsColumns.SubSearchID ,  sys_SearchsColumns.RegUserID ,sys_SearchsColumns.Regdate ,sys_SearchsColumns.cancelDate ,sys_SearchsColumns.IsView , sys_Fields.FieldName from sys_SearchsColumns " & " inner join sys_Searchs on sys_searchscolumns.searchid = sys_Searchs.id inner join sys_Fields on sys_searchs.objectid = sys_Fields.objectid  and  sys_searchscolumns.FieldID = sys_fields.id "
        mSelectCommand = " select sys_Fields.objectid,sys_fields.FieldType , sys_SearchsColumns.id ,sys_SearchsColumns.isArabic, sys_SearchsColumns.searchID , sys_SearchsColumns.fieldid ,sys_SearchsColumns.EngName,sys_SearchsColumns.ArbName, sys_SearchsColumns.IsCriteria , sys_SearchsColumns.InputLength ,sys_FIELDS.FieldLength , case when  sys_SearchsColumns.IsArabic = '0' then 'English' WHEN sys_SearchsColumns.IsArabic = '1' THEN 'Arabic' ELSE 'Both' END  As ColumnLanguage ,  sys_SearchsColumns.Alignment , sys_SearchsColumns.RankCriteria,sys_SearchsColumns.RankView ,sys_SearchsColumns.IsTarget, sys_SearchsColumns.SubSearchID ,  sys_SearchsColumns.RegUserID ,sys_SearchsColumns.Regdate ,sys_SearchsColumns.cancelDate ,sys_SearchsColumns.IsView , sys_Fields.FieldName,sys_SearchsColumns.RegComputerID from sys_SearchsColumns " & " inner join sys_Searchs on sys_searchscolumns.searchid = sys_Searchs.id inner join sys_Fields on sys_searchs.objectid = sys_Fields.objectid  and  sys_searchscolumns.FieldID = sys_fields.id "

    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mSearchID As Integer
    Private mFieldID As Integer
    Private mEngName As String
    Private mArbName As String
    Private mIsCriteria As Boolean
    Private mInputLength As String
    'Private mColumnLanguage As Boolean
    Private mIsArabic As Boolean
    Private mAlignment As Object
    Private mRankCriteria As Object
    Private mRankView As Object
    Private mIsTarget As Boolean
    Private mSubSearchID As Object
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Date
    Private mCancelDate As Object
    Private mActualLength As Integer
    '  170707  
    Private mIsView As Boolean
    '   E#00008 180707
    Private mFieldName As String
    '   B#00008 190707 
    Private mObjectID As Integer
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
    Public Property SearchID() As Object
        Get
            Return mSearchID
        End Get
        Set(ByVal Value As Object)
            mSearchID = Value
        End Set
    End Property
    Public Property FieldID() As Object
        Get
            Return mFieldID
        End Get
        Set(ByVal Value As Object)
            mFieldID = Value
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
    Public Property IsCriteria() As Boolean
        Get
            Return mIsCriteria
        End Get
        Set(ByVal Value As Boolean)
            mIsCriteria = Value
        End Set
    End Property
    Public Property InputLength() As Object
        Get
            Return mInputLength
        End Get
        Set(ByVal Value As Object)
            mInputLength = Value
        End Set
    End Property
    Public Property IsArabic() As Boolean
        Get
            Return mIsArabic
        End Get
        Set(ByVal Value As Boolean)
            mIsArabic = Value
        End Set
    End Property
    Public Property Alignment() As Object
        Get
            Return mAlignment
        End Get
        Set(ByVal Value As Object)
            mAlignment = Value
        End Set
    End Property
    Public Property RankCriteria() As Object
        Get
            Return mRankCriteria
        End Get
        Set(ByVal Value As Object)
            mRankCriteria = Value
        End Set
    End Property
    Public Property RankView() As Object
        Get
            Return mRankView
        End Get
        Set(ByVal Value As Object)
            mRankView = Value
        End Set
    End Property
    Public Property IsTarget() As Boolean
        Get
            Return mIsTarget
        End Get
        Set(ByVal Value As Boolean)
            mIsTarget = Value
        End Set
    End Property
    Public Property SubSearchID() As Object
        Get
            Return mSubSearchID
        End Get
        Set(ByVal Value As Object)
            mSubSearchID = Value
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
    Public Property IsView() As Object
        Get
            Return mIsView
        End Get
        Set(ByVal Value As Object)
            mIsView = Value
        End Set
    End Property

    Public Property ActualLength() As Integer
        Get
            Return mActualLength
        End Get
        Set(ByVal value As Integer)
            mActualLength = value
        End Set
    End Property
    '
    Public Property FieldName() As String
        Get
            Return mFieldName
        End Get
        Set(ByVal value As String)
            mFieldName = value
        End Set
    End Property

    Public Property ObjectID() As Integer
        Get
            Return mObjectID
        End Get
        Set(ByVal value As Integer)
            mObjectID = value
        End Set
    End Property

#End Region
#Region "Public Function"
    '==================================================================================
    'ProcedureName  : Find()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is used to look for any given query represented by Filter Parameter and returns 
    '               : True if the searched data exist or false if not 
    'Developer      :  [0256]
    'Date Created   : DataOcean 
    'Date Modified  : 16-07-2007 
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : GetParameter(mDataSet) : To create and set values for msqlcommand parameters 
    '               : mDataHandler.CheckValidDataObject(mDataSet) 
    'Called From    : frmSearchSetting.aspx.vb  
    '               : TbrMainToolbar_ButtonClicked 
    '               : CheckView 
    '==================================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(sys_SearchsColumns.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(sys_SearchsColumns.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & "  ", "  Where IsNull(sys_SearchsColumns.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(sys_SearchsColumns.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & "  ")
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



    '==================================================================================
    'ProcedureName  : fnFind()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is used to look for any given query represented by Filter Parameter and returns 
    '               : True if the searched data exist or false if not 
    'Developer      :  [0256]
    'Date Created   : 16-07-2007 
    'Date Modified  : 16-07-2007 
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : GetParameter(mDataSet) : To create and set values for msqlcommand parameters 
    '               : mDataHandler.CheckValidDataObject(mDataSet) 
    'Called From    : frmSearchSetting.aspx.vb  
    '               : TbrMainToolbar_ButtonClicked 
    '               : CheckView 
    '==================================================================================
    Public Function fnFind(ByVal Filter As String, Optional ByVal objectID As Integer = 0, Optional ByVal OrderByString As String = " order by SysColumns_OrderID  ") As Boolean
        Dim StrSelectCommand As String
        Try
            'If objectID <> 0 Then
            '    StrSelectCommand = mSelectCommand & " Where IsNull(sys_SearchsColumns.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(sys_SearchsColumns.id,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And  SYS_SEARCHS.objectid = " & objectID & " and " & Filter & OrderByString
            'Else
            '    StrSelectCommand = mSelectCommand & " Where IsNull(sys_SearchsColumns.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(sys_SearchsColumns.id,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And  " & IIf(objectID > 0, " SYS_SEARCHS.objectid = " & objectID, " ") & " And " & Filter & OrderByString
            'End If

            StrSelectCommand = mSelectCommand & " Where sys_fields.canceldate is null and IsNull(sys_SearchsColumns.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(sys_SearchsColumns.id,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(objectID > 0, " And SYS_SEARCHS.objectid = " & objectID, " ") & " And " & Filter & OrderByString
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

    '==================================================================================
    'ProcedureName  : fnGetColumns()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function Used To Retun A dataset With Columns [Fields] names Of a given Table  
    '               : Tis is done from sys_Fields Table 
    'Developer      :  [0256]
    'Date Created   : 16-07-2007 
    'Date Modified  : 
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : 
    '               : 
    'Called From    : frmSearchSetting.aspx.vb
    '               :     CheckView()
    '==================================================================================
    Public Function fnGetColumns(ByVal objectID As Integer) As DataSet
        Dim StrSelectCommand As String = String.Empty
        'Dim SqlsTRING As String = " select sys_SearchsColumns.id , sys_SearchsColumns.searchID , sys_SearchsColumns.fieldid ,sys_SearchsColumns.ColumnEngDescription ,sys_SearchsColumns.ColumnArbDescription , sys_SearchsColumns.IsCriteria , sys_SearchsColumns.ColumnLength ,  sys_SearchsColumns.ColumnLanguage ,  sys_SearchsColumns.ColumnAlignment , sys_SearchsColumns.Rank , sys_SearchsColumns.SubSearchID ,  sys_SearchsColumns.RegUserID ,sys_SearchsColumns.Regdate ,sys_SearchsColumns.cancelDate ,sys_SearchsColumns.IsView , sys_Fields.FieldName from  sys_fields left join sys_searchscolumns on sys_fields.id =sys_searchscolumns.fieldid  "
        Dim SqlsTRING As String = " select sys_SearchsColumns.id , sys_SearchsColumns.searchID , sys_SearchsColumns.fieldid ,sys_SearchsColumns.EngName ,sys_SearchsColumns.Arbname , sys_SearchsColumns.IsCriteria , sys_SearchsColumns.InputLength ,  sys_SearchsColumns.isarabic ,  sys_SearchsColumns.Alignment , sys_SearchsColumns.RankCriteria,sys_SearchsColumns.RankView , sys_SearchsColumns.SubSearchID ,  sys_SearchsColumns.RegUserID ,sys_SearchsColumns.Regdate ,sys_SearchsColumns.cancelDate ,sys_SearchsColumns.IsView , sys_Fields.FieldName from  sys_fields left join sys_searchscolumns on sys_fields.id =sys_searchscolumns.fieldid  "
        Try
            StrSelectCommand = SqlsTRING & " Where IsNull(sys_SearchsColumns.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(sys_SearchsColumns.id,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And  sys_fields.canceldate is null and SYS_Fields.objectid = " & objectID & " order by SysColumns_OrderID "
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            'If mDataHandler.CheckValidDataObject(mDataSet) Then
            '    GetParameter(mDataSet)
            'Else
            '    Clear()
            'End If
            'If mID > 0 Then
            'Return True
            'End If
            Return mDataSet
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    'Function Retuns The ObjectID 
    Public Function fnGetObjectID(ByVal Filter As String) As Integer
        Dim IDfIled As Integer
        Try
            mSqlConnection = New SqlClient.SqlConnection(mConnectionString)
            Dim sqlString As String = "Select top 1 objectID FROM SYS_SearchsColumns " & Filter
            Dim SQLCMD As New SqlClient.SqlCommand(sqlString, mSqlConnection)
            If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
            mSqlConnection.Open()
            IDfIled = SQLCMD.ExecuteScalar()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(" ", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
        mSqlConnection.Close()
        Return IDfIled
    End Function
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From sys_SearchsColumns Where " & Filter
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = strSQL
            mSqlCommand.Connection.Open()
            Value = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            If Value > 0 Then
                fnUpdate(Filter)
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
  

    Public Function fnUpdate(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand)
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
    Public Function Clear() As Boolean
        Try
            mID = 0
            mSearchID = 0
            mFieldID = 0
            mEngName = String.Empty
            mArbName = String.Empty
            mIsCriteria = False
            mInputLength = 0
            'mColumnLanguage = False
            mIsArabic = Nothing
            mAlignment = 0
            mRankCriteria = 0
            mRankView = 0
            mIsTarget = False
            mSubSearchID = 0
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            ' E#00006 180707 
            mActualLength = 0
            ' E#000008 
            mFieldName = String.Empty
            '
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    Public Function CountColumns(ByVal SearchViewID As Integer) As Integer
        Try
            Find("SearchID=" & SearchViewID & " And IsCriteria=1")
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Return mDataSet.Tables(0).Rows.Count
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function fngetFieldID(ByVal ObjectID As Integer, ByVal FieldName As String) As Integer
        Dim IDfIled As Integer
        Try
            mSqlConnection = New SqlClient.SqlConnection(mConnectionString)
            Dim sqlString As String = "Select ID FROM SYS_Fields where FieldName = '" & FieldName & "' And ObjectID = " & ObjectID
            Dim SQLCMD As New SqlClient.SqlCommand(sqlString, mSqlConnection)
            If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
            mSqlConnection.Open()
            IDfIled = SQLCMD.ExecuteScalar()
        Catch ex As Exception

        End Try
        mSqlConnection.Close()
        Return IDfIled
    End Function
    Public Function fnGetSubSearchID(ByVal ObjectID As Integer, ByVal FieldID As Integer) As Integer
        Dim ParentID As Integer
        Try
            mSqlConnection = New SqlClient.SqlConnection(mConnectionString)
            'Dim SQLCMD As New SqlClient.SqlCommand(sqlString, mSqlConnection)
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = mSqlConnection
            mSqlCommand.Parameters.Clear()
            mSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@ObjectID", SqlDbType.Int)).Value = ObjectID
            mSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@FieldID", SqlDbType.Int)).Value = FieldID
            mSqlCommand.CommandType = CommandType.StoredProcedure
            mSqlCommand.CommandText = "hrs_GetFormSearchID"
            If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
            mSqlConnection.Open()
            ParentID = mSqlCommand.ExecuteScalar()
        Catch ex As Exception
            ParentID = Nothing
        End Try
        mSqlConnection.Close()
        Return ParentID
    End Function
    Public Function fngetFieldIDSearchsColumns(ByVal FieldName As String, ByVal Filter As String) As Integer
        Dim fIled As Integer
        Try
            mSqlConnection = New SqlClient.SqlConnection(mConnectionString)
            Dim sqlString As String = "Select  " & FieldName & " FROM sys_SearchsColumns Where " & Filter
            Dim SQLCMD As New SqlClient.SqlCommand(sqlString, mSqlConnection)
            If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
            mSqlConnection.Open()
            fIled = SQLCMD.ExecuteScalar()
        Catch ex As Exception

        End Try
        mSqlConnection.Close()
        Return fIled
    End Function
    Public Function fnGetIsCriteria(ByVal FieldID As Integer, ByVal SearchID As String) As Boolean
        Dim Tag As Boolean
        Try
            mSqlConnection = New SqlClient.SqlConnection(mConnectionString)
            Dim sqlString As String = "Select IsCriteria FROM SYS_SEARCHSCOLUMNS where Fieldid = '" & FieldID & "' And SearchID = " & SearchID
            Dim SQLCMD As New SqlClient.SqlCommand(sqlString, mSqlConnection)
            If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
            mSqlConnection.Open()
            Tag = SQLCMD.ExecuteScalar()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
        mSqlConnection.Close()
        Return Tag
    End Function

#End Region
#Region "Class Private Function"
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mSearchID = mDataHandler.DataValue_Out(.Item("SearchID"), SqlDbType.Int, True)
                mFieldID = mDataHandler.DataValue_Out(.Item("FieldID"), SqlDbType.Int, True)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mIsCriteria = mDataHandler.DataValue_Out(.Item("IsCriteria"), SqlDbType.Bit)
                mInputLength = mDataHandler.DataValue_Out(.Item("InputLength"), SqlDbType.VarChar)
                mIsArabic = mDataHandler.DataValue_Out(.Item("IsArabic"), SqlDbType.Bit)
                mAlignment = mDataHandler.DataValue_Out(.Item("Alignment"), SqlDbType.Int)
                mRankCriteria = mDataHandler.DataValue_Out(.Item("RankCriteria"), SqlDbType.Int)
                mRankView = mDataHandler.DataValue_Out(.Item("RankView"), SqlDbType.Int)
                mIsTarget = mDataHandler.DataValue_Out(.Item("IsTarget"), SqlDbType.Bit)
                mSubSearchID = mDataHandler.DataValue_Out(.Item("SubSearchID"), SqlDbType.Int, True)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                ' E#000003 
                mIsView = mDataHandler.DataValue_Out(.Item("IsView"), SqlDbType.Bit)
                ' E#000006 
                mActualLength = mDataHandler.DataValue_Out(.Item("FieldLength"), SqlDbType.Int)
                ' E#000008 
                mFieldName = mDataHandler.DataValue_Out(.Item("FieldName"), SqlDbType.VarChar)
                ' B#000008  
                mObjectID = mDataHandler.DataValue_Out(.Item("ObjectID"), SqlDbType.Int, True)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSearchID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFieldID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsCriteria", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsCriteria, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InputLength", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mInputLength, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsArabic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsArabic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Alignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mAlignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RankCriteria", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRankCriteria, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RankView", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRankView, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsTarget", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsTarget, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SubSearchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSubSearchID, SqlDbType.Int, True)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SubSearchID", SqlDbType.Int)).Value = 
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int)
            ' E#000004
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsView", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsView, SqlDbType.Bit)
            ' E#000006
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@", SqlDbType.Int)).Value = mDataHandler.DataValue_Out(mActualLength, SqlDbType.Int)
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

