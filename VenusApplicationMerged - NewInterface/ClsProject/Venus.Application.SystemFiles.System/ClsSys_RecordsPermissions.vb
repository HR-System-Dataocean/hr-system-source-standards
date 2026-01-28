Public Class ClsSys_RecordsPermissions
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_RecordsPermissions "
        mInsertParameter = " GroupID,UserID,RecordID,ObjectID,CanEdit,CanDelete,CanView,CanPrint,IsSpacific,RegUserID,RegComputerID,RegDate,CancelDate "
        mInsertParameterValues = " @GroupID,@UserID,@RecordID,@ObjectID,@CanEdit,@CanDelete,@CanView,@CanPrint,@IsSpacific,@RegUserID,@RegComputerID,@RegDate,@CancelDate "
        mUpdateParameter = " GroupID=@GroupID,UserID=@UserID,RecordID=@RecordID,ObjectID=@ObjectID,CanEdit=@CanEdit,CanDelete=@CanDelete,CanView=@CanView,CanPrint=@CanPrint,IsSpacific=@IsSpacific,RegUserID=@RegUserID,RegComputerID=@RegComputerID,RegDate=@RegDate,CancelDate=@CancelDate "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    Private mID As Object
    Private mGroupID As Object
    Private mUserID As Object
    Private mRecordID As String
    Private mObjectID As Integer
    Private mCanEdit As Boolean
    Private mCanDelete As Boolean
    Private mCanView As Boolean
    Private mCanPrint As Boolean
    Private mIsSpacific As Boolean
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object

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
    Public Property GroupID() As Object
        Get
            Return mGroupID
        End Get
        Set(ByVal Value As Object)
            mGroupID = Value
        End Set
    End Property
    Public Property UserID() As Object
        Get
            Return mUserID
        End Get
        Set(ByVal Value As Object)
            mUserID = Value
        End Set
    End Property
    Public Property RecordID() As String
        Get
            Return mRecordID
        End Get
        Set(ByVal Value As String)
            mRecordID = Value
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
    Public Property CanEdit() As Boolean
        Get
            Return mCanEdit
        End Get
        Set(ByVal Value As Boolean)
            mCanEdit = Value
        End Set
    End Property
    Public Property CanDelete() As Boolean
        Get
            Return mCanDelete
        End Get
        Set(ByVal Value As Boolean)
            mCanDelete = Value
        End Set
    End Property
    Public Property CanView() As Boolean
        Get
            Return mCanView
        End Get
        Set(ByVal Value As Boolean)
            mCanView = Value
        End Set
    End Property
    Public Property CanPrint() As Boolean
        Get
            Return mCanPrint
        End Get
        Set(ByVal Value As Boolean)
            mCanPrint = Value
        End Set
    End Property

    Public Property IsSpacific() As Boolean
        Get
            Return mIsSpacific
        End Get
        Set(ByVal value As Boolean)
            mIsSpacific = value
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
#End Region

#Region "Public Function"

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And " & Filter, "  Where IsNull(CancelDate,'')=''  ")
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

    Public Function FindTables(ByRef TableList As Web.UI.WebControls.DropDownList) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim ObjListItem As Web.UI.WebControls.ListItem
        Dim ObjDataRow As DataRow
        Try

            StrSelectCommand = "Select * From hrs_vwAllTables"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                TableList.Items.Clear()
                For Each ObjDataRow In mDataSet.Tables(0).Rows
                    ObjListItem = New Web.UI.WebControls.ListItem
                    ObjListItem.Text = ObjDataRow.Item("TableName")
                    ObjListItem.Value = ObjDataRow.Item("TableName")
                    TableList.Items.Add(ObjListItem)
                Next
            End If
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FindUsersRecordsPermission(ByVal UserID As Integer, ByVal TableID As Integer, ByVal Code As String, ByVal EngName As String, ByVal ArbName As String, ByRef Ds As DataSet) As Boolean
        Dim StrSelectCommand As String = "hrs_GetUsersRecordsPermissions"
        Try
            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, StrSelectCommand, UserID, TableID, Code, EngName, ArbName)
            If Me.mDataHandler.CheckValidDataObject(Ds) Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '-------------------------------0257 MODIFIED-----------------------------------------
    Public Function FindUsersRecordsPermission(ByVal UserID As Integer, ByVal TableID As Integer, ByVal Filter As String, ByRef Ds As DataSet) As Boolean
        Dim StrSelectCommand As String = ""
        Try

            Dim ClsObjects As New Clssys_Objects(mPage)
            ClsObjects.Find("ID=" & TableID)
            Dim ClsFields As New Clssys_Fields(mPage)
            ClsFields.Find(" ObjectID=" & TableID & " And IsNull(CancelDate,'')=''")

            'StrSelectCommand = "Select * from " & ClsObjects.Code
            StrSelectCommand = "Select distinct " & ClsObjects.Code & ".*,isNull((Select CanEdit		From sys_RecordsPermissions  where userid=sys_users.id  and ObjectID=" & TableID & "And RecordID=" & ClsObjects.Code & ".ID),0) 				As CantEdit," & _
                              "isNull((Select CanDelete	From sys_RecordsPermissions  where userid=sys_users.id  and ObjectID= " & TableID & " And RecordID=" & ClsObjects.Code & ".ID),0) 				As CantDelete," & _
                              "isNull((Select CanView		From sys_RecordsPermissions  where userid=sys_users.id  and ObjectID= " & TableID & "  And RecordID=" & ClsObjects.Code & ".ID),0) 				As CantView, " & _
                              "isNull((Select CanPrint		From sys_RecordsPermissions  where userid=sys_users.id  and ObjectID= " & TableID & " And RecordID=" & ClsObjects.Code & ".ID),0) 				As CantPrint, " & _
                              "isNull((Select IsSpacific	From sys_RecordsPermissions  where userid=sys_users.id  and ObjectID= " & TableID & " And RecordID=" & ClsObjects.Code & ".ID),0) 				As IsSpacific, " & _
                              "isNull((Select Convert(Int,CanEdit) * Convert(Int,CanDelete) * Convert(Int,CanView) *  Convert(Int,CanPrint) * Convert(Int,IsSpacific) 	From sys_RecordsPermissions  where userid=sys_users.id " & _
                              "and ObjectID= " & TableID & " And RecordID= " & ClsObjects.Code & ".ID),0) 				As CheckAll    from " & ClsObjects.Code & _
                              " Cross Join sys_users "

            StrSelectCommand = StrSelectCommand & " Where sys_users.id= " & UserID
            If Not Filter.Trim = String.Empty Then
                StrSelectCommand = StrSelectCommand & " AND "
                Dim count As Integer = 1
                For Each row As DataRow In ClsFields.DataSet.Tables(0).Rows
                    StrSelectCommand = StrSelectCommand & " " & ClsObjects.Code & "." & row("FieldName") & " like '%" & Filter & "%'"
                    If count = ClsFields.DataSet.Tables(0).Rows.Count Then
                        Exit For
                    Else
                        StrSelectCommand = StrSelectCommand & " OR "
                    End If
                    count = count + 1
                Next
            End If
            StrSelectCommand = StrSelectCommand & " Order By " & ClsObjects.Code & ".ID"
            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, StrSelectCommand)
            If Me.mDataHandler.CheckValidDataObject(Ds) Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function FindGroupsRecordsPermission(ByVal GroupID As Integer, ByVal TableID As Integer, ByVal Filter As String, ByRef Ds As DataSet) As Boolean
        Dim StrSelectCommand As String = ""
        Try

            Dim ClsObjects As New Clssys_Objects(mPage)
            ClsObjects.Find("ID=" & TableID)
            Dim ClsFields As New Clssys_Fields(mPage)
            ClsFields.Find(" ObjectID=" & TableID & " And IsNull(CancelDate,'')=''")

            StrSelectCommand = "Select distinct " & ClsObjects.Code & ".*,isNull((Select CanEdit		From sys_RecordsPermissions  where Groupid=Sys_Groups.id  and ObjectID=" & TableID & "And RecordID=" & ClsObjects.Code & ".ID),0) 				As CantEdit," & _
                               "isNull((Select CanDelete	From sys_RecordsPermissions  where Groupid=Sys_Groups.id  and ObjectID= " & TableID & " And RecordID=" & ClsObjects.Code & ".ID),0) 				As CantDelete," & _
                               "isNull((Select CanView		From sys_RecordsPermissions  where Groupid=Sys_Groups.id  and ObjectID= " & TableID & "  And RecordID=" & ClsObjects.Code & ".ID),0) 				As CantView, " & _
                               "isNull((Select CanPrint		From sys_RecordsPermissions  where Groupid=Sys_Groups.id  and ObjectID= " & TableID & " And RecordID=" & ClsObjects.Code & ".ID),0) 				As CantPrint, " & _
                               "isNull((Select IsSpacific	From sys_RecordsPermissions  where Groupid=Sys_Groups.id  and ObjectID= " & TableID & " And RecordID=" & ClsObjects.Code & ".ID),0) 				As IsSpacific , " & _
                               "isNull((Select Convert(Int,CanEdit) * Convert(Int,CanDelete) * Convert(Int,CanView) *  Convert(Int,CanPrint) * Convert(Int,IsSpacific) 	From sys_RecordsPermissions  where groupid=sys_Groups.id " & _
                               "and ObjectID= " & TableID & " And RecordID= " & ClsObjects.Code & ".ID),0) 				As CheckAll    from " & ClsObjects.Code & _
                               " Cross Join sys_Groups "

            StrSelectCommand = StrSelectCommand & " Where sys_Groups.id= " & GroupID
            Dim count As Integer = 1
            If Not Filter.Trim = String.Empty Then
                StrSelectCommand = StrSelectCommand & " And "
                For Each row As DataRow In ClsFields.DataSet.Tables(0).Rows
                    StrSelectCommand = StrSelectCommand & " " & ClsObjects.Code & "." & row("FieldName") & " like '%" & Filter & "%'"
                    If count = ClsFields.DataSet.Tables(0).Rows.Count Then
                        Exit For
                    Else
                        StrSelectCommand = StrSelectCommand & " OR "
                    End If
                    count = count + 1
                Next

            End If
            StrSelectCommand = StrSelectCommand & " Order By " & ClsObjects.Code & ".ID"

            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, CommandType.Text, StrSelectCommand)
            If Me.mDataHandler.CheckValidDataObject(Ds) Then
                Return True
            End If


        Catch ex As Exception
            Return False
        End Try


    End Function
    '------------------------------=============-----------------------------------------

    Public Function FindGroupsRecordsPermission(ByVal GroupID As Integer, ByVal TableID As Integer, ByVal Code As String, ByVal EngName As String, ByVal ArbName As String, ByRef Ds As DataSet) As Boolean
        Dim StrSelectCommand As String = "hrs_GetGroupsRecordsPermissions"
        Try
            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, StrSelectCommand, GroupID, TableID, Code, EngName, ArbName)
            If Me.mDataHandler.CheckValidDataObject(Ds) Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From sys_RecordsPermissions Where " & Filter
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

    Public Function Update(ByVal Filter As String) As Boolean
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
            mGroupID = 0
            mUserID = 0
            mRecordID = String.Empty
            mObjectID = 0
            mCanEdit = False
            mCanDelete = False
            mCanView = False
            mCanPrint = False
            mIsSpacific = False
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' ORDER BY ID ASC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')=''  ORDER BY ID DESC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' ORDER BY ID ASC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID <" & mID & " And isNull(CancelDate,'')='' ORDER BY ID DESC"
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

#End Region

#Region "Class Private Function"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mGroupID = mDataHandler.DataValue_Out(.Item("GroupID"), SqlDbType.Int, True)
                mUserID = mDataHandler.DataValue_Out(.Item("UserID"), SqlDbType.Int, True)
                mRecordID = mDataHandler.DataValue_Out(.Item("RecordID"), SqlDbType.VarChar)
                mObjectID = mDataHandler.DataValue_Out(.Item("ObjectID"), SqlDbType.Int, True)
                mCanEdit = mDataHandler.DataValue_Out(.Item("CanEdit"), SqlDbType.Bit)
                mCanDelete = mDataHandler.DataValue_Out(.Item("CanDelete"), SqlDbType.Bit)
                mCanView = mDataHandler.DataValue_Out(.Item("CanView"), SqlDbType.Bit)
                mCanPrint = mDataHandler.DataValue_Out(.Item("CanPrint"), SqlDbType.Bit)
                mIsSpacific = mDataHandler.DataValue_Out(.Item("IsSpacific"), SqlDbType.Bit)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GroupID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mGroupID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mUserID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RecordID", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRecordID, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ObjectID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mObjectID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CanEdit", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCanEdit, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CanDelete", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCanDelete, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CanView", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCanView, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CanPrint", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCanPrint, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsSpacific", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsSpacific, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mRegDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)
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

