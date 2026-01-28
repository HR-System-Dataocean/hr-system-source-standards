'===================================================================================
'Program Name     :  ClsSys_OtherFields.vb 
'Application Name : Venus 
'Module           : HRS
'Developer        :   
'Date Created     : 08-08-2007 
'Description      : Acts as BO Layer for both otherfields screen and otherfields viewer screen 
'Modifications    :  B#00001 13-08-2007 OtherFields Screen returns All Data Canceled Data
'                 :  E#00001 07-08-2007 Change the way Loding data types it was from system types tables 
'                 :                          and became fixed to 4 types  loaded from Infterface Layer 
'
'=====================================================================================
Public Class ClsSys_OtherFields
    Inherits ClsDataAcessLayer
#Region "Private Members"
    Private mID As Object
    Private mObjectID As String
    Private mOtherFieldGroupID As Integer
    Private mRank As Integer

    Private mWidth As Integer

    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String

    Private mFieldType As Char

    Private mViewObjectID As Integer
    Private mViewEngFieldID As Integer
    Private mViewArbFieldID As Integer

    Private mDataType As Integer
    Private mDataLength As Integer

    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mCancelDate As Object
    Private mRegDate As Object
#End Region
#Region "Class Constructors"
    '=================================================================================
    'ProcedureName  :  New()
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Initialize Table name,table fields,parametrs and select,delete and update statments
    'Developer      :  [0256]
    'Date Created   :  05-08-2007 
    '================================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_otherFields "
        mInsertParameter = " ObjectID,OtherFieldGroupID,Rank,Width,EngName,ArbName,ArbName4S, " & _
                           " FieldType,ViewObjectID,ViewEngFieldID,ViewArbFieldID,DataType, " & _
                           " DataLength,Remarks,RegUserID,RegComputerID "
        mInsertParameterValues = " @ObjectID,@OtherFieldGroupID ,@Rank,@Width, @EngName,@ArbName,@ArbName4S," & _
                                 " @FieldType,@ViewObjectID,@ViewEngFieldID,@ViewArbFieldID,@DataType, " & _
                                 " @DataLength,@Remarks,@RegUserID,@RegComputerID "

        mUpdateParameter = " ObjectID=@ObjectID,OtherFieldGroupID =@OtherFieldGroupID ,Rank = @Rank,Width = @Width , " & _
                           " EngName=@EngName,ArbName=@ArbName,ArbName4S=@ArbName4S,FieldType=@FieldType," & _
                           " ViewObjectID =@ViewObjectID ,ViewEngFieldID = @ViewEngFieldID,ViewArbFieldID=@ViewArbFieldID, " & _
                           " DataType = @DataType ,DataLength =@DataLength,Remarks=@Remarks "

        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        'mSelectCommand = " Select ID, ObjectID, OtherFieldGroupID, Rank, EngName, ArbName, ArbName4S, " & _
        '                 " FieldType,ViewObjectID,ViewEngFieldID,ViewArbFieldID,DataType, " & _
        '                 " DataLength,Remarks,RegUserID,RegComputerID ,RegDate,RegUserID ,CancelDate  From  " & mTable

        mSelectCommand = " Select sys_OtherFields.ID ,sys_OtherFields.ObjectID, sys_OtherFields.OtherFieldGroupID, sys_OtherFields.Rank,sys_OtherFields.Width, sys_OtherFields.EngName, sys_OtherFields.ArbName, sys_OtherFields.ArbName4S, " & _
                         " Case When sys_OtherFields.FieldType = 'A' THEN 'Addresses' When sys_OtherFields.FieldType = 'B' Then 'Contacts' else 'Other Types' End as FieldTypeName ,sys_OtherFields.FieldType ,  " & _
                         " sys_OtherFields.ViewObjectID,sys_OtherFields.ViewEngFieldID,sys_OtherFields.ViewArbFieldID, " & _
                         " Case When sys_OtherFields.DataType = 1 then 'Charachters' When sys_OtherFields.DataType = 2 Then 'Numeric' When sys_OtherFields.DataType = 3 Then 'Boolean' Else 'DateTime' End as DataTypeName ,sys_OtherFields.datatype , " & _
                         " sys_OtherFields.DataLength,sys_OtherFields.Remarks,sys_OtherFields.RegUserID,sys_OtherFields.RegComputerID ,sys_OtherFields.RegDate,sys_OtherFields.CancelDate,  " & _
                         " sys_OtherFieldsGroups.EngName as OtherFieldsGroupName ,sys_Objects.code ViewObjectName ,sys_F1.FieldName as VwEngName ,sys_F2.FieldName as VwArbName , " & _
                         " sys_Searchs.ID AS SubSearchID ," & _
                         " Case when sys_otherFields.canceldate is null then 0 else 1 end as Canceled " & _
                         " From  sys_OtherFields " & _
                         " inner Join sys_OtherFieldsGroups on sys_OtherFields.OtherFieldGroupID = sys_OtherFieldsGroups.ID " & _
                         " Left Join sys_Objects on sys_OtherFields.ViewObjectID = sys_Objects.ID " & _
                         " Left Join sys_Searchs on sys_objects.ID = sys_searchs.ObjectID " & _
                         " left Join sys_Fields sys_F1 on sys_OtherFields.ViewEngFieldID = sys_F1.ID " & _
                         " left Join sys_Fields sys_F2 on sys_OtherFields.ViewArbFieldID = sys_F2.ID "

        'sys.sysTypes.name as DataTypeName ,
        '" Left Join sys.sysTypes on sys_OtherFields.dataType = sys.sysTypes.xuserType " & _
        'mSelectCommand = " Select ID,Code,EngName,ArbName,ArbName4S,CompanyID,Remarks,RegUserID,RegComputerID ,RegDate,RegUserID ,CancelDate  From  " & mTable
        mInsertCommand = " Insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
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
    Public Property ObjectID() As Integer
        Get
            Return mObjectID
        End Get
        Set(ByVal Value As Integer)
            mObjectID = Value
        End Set
    End Property
    Public Property OtherFieldGroupID() As Integer
        Get
            Return mOtherFieldGroupID
        End Get
        Set(ByVal value As Integer)
            mOtherFieldGroupID = value
        End Set
    End Property
    Public Property Rank() As Integer
        Get
            Return mRank
        End Get
        Set(ByVal Value As Integer)
            mRank = Value
        End Set
    End Property

    Public Property Width() As Integer
        Get
            Return mWidth
        End Get
        Set(ByVal Value As Integer)
            mWidth = Value
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
    Public Property FieldType() As Char
        Get
            Return mFieldType
        End Get
        Set(ByVal Value As Char)
            mFieldType = Value
        End Set
    End Property
    Public Property ViewObjectID() As Integer
        Get
            Return mViewObjectID
        End Get
        Set(ByVal value As Integer)
            mViewObjectID = value
        End Set
    End Property
    Public Property ViewEngFieldID() As Integer
        Get
            Return mViewEngFieldID
        End Get
        Set(ByVal value As Integer)
            mViewEngFieldID = value
        End Set
    End Property
    Public Property ViewArbFieldID() As Integer
        Get
            Return mViewArbFieldID
        End Get
        Set(ByVal Value As Integer)
            mViewArbFieldID = Value
        End Set
    End Property
    Public Property DataType() As Integer
        Get
            Return mDataType
        End Get
        Set(ByVal value As Integer)
            mDataType = value
        End Set
    End Property
    Public Property DataLength() As Integer
        Get
            Return mDataLength
        End Get
        Set(ByVal value As Integer)
            mDataLength = value
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
    Public Property CancelDate() As Object
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As Object)
            mCancelDate = Value
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
#End Region
#Region "Public Methods"
    '==================================================================
    'Procedure name : Find()
    'Developer      : 
    '               :
    'Date Created   : 08-08-2007 
    'Description    : It fills class dataset with data which may be filtered or not 
    'Modifications  : 
    '==================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(Sys_OtherFields.CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(Sys_OtherFields.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And " & Filter, "  Where IsNull(Sys_OtherFields.CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(sys_OtherFields.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ") 'And sys_OtherFields.CancelDate is Null
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
    'Procedure name : Find()
    'Developer      : [0260]
    '               :
    'Date Created   : 21-05-2008 
    'Description    : It fills class dataset with data (Canceled,Non-Caceled) which may be filtered or not 
    'Modifications  : 
    '==================================================================
    Public Function Find(ByVal Filter As String, ByVal blnCanceled As Boolean) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & IIf(blnCanceled = True, "", " IsNull(Sys_OtherFields.CancelDate,'')=''  ) And ") & "  IsNull(dbo.hrs_GetRecordViewStatus(Sys_OtherFields.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And " & Filter, "  Where IsNull(Sys_OtherFields.CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(sys_OtherFields.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ") 'And sys_OtherFields.CancelDate is Null

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(Sys_OtherFields.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And " & Filter, "  Where IsNull(Sys_OtherFields.CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(sys_OtherFields.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ") 'And sys_OtherFields.CancelDate is Null

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
    'Procedure name : SaveUpdate()
    'Developer      : 
    'Date Created   : 08-08-2007 
    'Description    : 
    'Modifications  : 
    '==================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From sys_Countries Where " & Filter
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
    'Procedure name : Save()
    'Developer      : 
    'Date Created   : 08-08-2007 
    'Description    : Save Other Fields data
    'Modifications  : 
    '==================================================================
    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, OperationType.Save)
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
    '==================================================================
    'Procedure name : Update()
    'Developer      : 
    'Date Created   : 08-08-2007 
    'Description    : Update Other Fields data
    'Modifications  : 
    '==================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, OperationType.Update)
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
    '==================================================================
    'Procedure name : Delete()
    'Developer      : 
    'Date Created   : 08-08-2007 
    'Description    : Delete Other Fields Row(s)
    'Modifications  : 
    '==================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
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
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Procedure name : Clear()
    'Developer      : 
    'Date Created   : 08-08-2007 calss data 
    'Modifications  : 
    '==================================================================
    Public Function Clear() As Boolean
        Try

            mID = 0
            mObjectID = 0
            mOtherFieldGroupID = 0
            mRank = 0
            mWidth = 0
            mEngName = String.Empty
            mArbName = String.Empty
            mFieldType = String.Empty
            mViewObjectID = 0
            mViewEngFieldID = 0
            mViewArbFieldID = 0
            mArbName4S = String.Empty
            mDataType = 0
            mDataLength = 0
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mCancelDate = Nothing
            mRegDate = Nothing
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================

    '==================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    '==================================================================
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    '==================================================================
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID > " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    '==================================================================
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    'Created By   : 
    'Date Created : 08-08-2007 
    'Modification :  5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '             :                 According to Page Language 
    '==================================================================
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName ", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & " Order By EngName ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")

                Item.Value = 0
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

#End Region
#Region "Private Methods"
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mObjectID = mDataHandler.DataValue_Out(.Item("ObjectID"), SqlDbType.Int, True)
                mOtherFieldGroupID = mDataHandler.DataValue_Out(.Item("OtherFieldGroupID"), SqlDbType.Int, True)
                mRank = mDataHandler.DataValue_Out(.Item("Rank"), SqlDbType.Int)
                mWidth = mDataHandler.DataValue_Out(.Item("Width"), SqlDbType.Int)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mFieldType = mDataHandler.DataValue_Out(.Item("FieldType"), SqlDbType.Char)
                mViewObjectID = mDataHandler.DataValue_Out(.Item("ViewObjectID"), SqlDbType.Int, True)
                mViewEngFieldID = mDataHandler.DataValue_Out(.Item("ViewEngFieldID"), SqlDbType.Int, True)
                mViewArbFieldID = mDataHandler.DataValue_Out(.Item("ViewArbFieldID"), SqlDbType.Int, True)
                mDataType = mDataHandler.DataValue_Out(.Item("DataType"), SqlDbType.Int)
                'mDataType = mDataHandler.DataValue_Out(.Item("DataType"), SqlDbType.Char)
                mDataLength = mDataHandler.DataValue_Out(.Item("DataLength"), SqlDbType.Int)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal ptrOperationType As OperationType) As Boolean
        Try

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ObjectID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mObjectID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OtherFieldGroupID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mOtherFieldGroupID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRank, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Width", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mWidth, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldType", SqlDbType.Char)).Value = mDataHandler.DataValue_In(mFieldType, SqlDbType.Char)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ViewObjectID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mViewObjectID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ViewEngFieldID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mViewEngFieldID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ViewArbFieldID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mViewArbFieldID, SqlDbType.Int, True)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DataType", SqlDbType.Char)).Value = mDataHandler.DataValue_In(mDataType, SqlDbType.Char)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DataType", SqlDbType.Char)).Value = mDataHandler.DataValue_In(mDataType, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DataLength", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDataLength, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Select Case ptrOperationType
                Case OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End Select
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function

    'Public Function fnGetDataTypes()

    'End Function

#End Region
#Region "Class Destructor"

#End Region

End Class