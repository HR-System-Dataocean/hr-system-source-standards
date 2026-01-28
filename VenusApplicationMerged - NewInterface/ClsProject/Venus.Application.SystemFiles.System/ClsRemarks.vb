Public Class ClsRemarks
    Inherits ClsDataAcessLayer

    Private mID As Object
    Private mTableName As String
    Private mObjectID As Integer
    Public Property ID() As Object
        Get
            Return mID
        End Get
        Set(ByVal value As Object)
            mID = value
        End Set
    End Property
    Public Property TableName() As String
        Get
            Return mTableName
        End Get
        Set(ByVal value As String)
            mTableName = value
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

    Public Sub New(ByVal page As Web.UI.Page, ByVal TableName As String, ByVal ID As Object, ByVal ObjectID As Integer)
        MyBase.New(page)
    End Sub

    Public Function UpdateRemarks(ByVal Remarks As String) As Boolean
        Dim ObjSqlCommand As New SqlClient.SqlCommand
        Try
            Me.mUpdateCommand = "Update " & mTableName & " Set Remarks='" & mStringHandler.StripQuotation(Remarks) & "' Where ID= " & ID
            ObjSqlCommand.Connection = New SqlClient.SqlConnection(Me.ConnectionString)
            ObjSqlCommand.CommandText = Me.mUpdateCommand
            ObjSqlCommand.CommandType = CommandType.Text
            ObjSqlCommand.Connection.Open()
            ObjSqlCommand.ExecuteNonQuery()
            ObjSqlCommand.Connection.Close()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function ReadRemarks(ByVal ID As Object, ByVal TableName As String, ByRef Remarks As String) As Boolean
        Dim StrSqlCommand As String = String.Empty
        Try
            StrSqlCommand = "Select Remarks from " & TableName & " where id =" & ID
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSqlCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Remarks = mDataHandler.DataValue_Out(mDataSet.Tables(0).Rows(0).Item(0), SqlDbType.VarChar)
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function NewUpdateRemarks(ByVal Remarks As String) As Boolean
        Dim ObjSqlCommand As New SqlClient.SqlCommand
        Try
            Me.mUpdateCommand = "Delete from sys_ObjectsRemarks where ObjectID = " & ObjectID & " and RecordID = " & ID & ";  insert into sys_ObjectsRemarks values (" & ObjectID & "," & ID & ",'" & Remarks & "'," & Me.DataBaseUserRelatedID & ",null,getdate())"
            ObjSqlCommand.Connection = New SqlClient.SqlConnection(Me.ConnectionString)
            ObjSqlCommand.CommandText = Me.mUpdateCommand
            ObjSqlCommand.CommandType = CommandType.Text
            ObjSqlCommand.Connection.Open()
            ObjSqlCommand.ExecuteNonQuery()
            ObjSqlCommand.Connection.Close()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function NewReadRemarks(ByVal ID As Object, ByVal TableName As String, ByRef Remarks As String, ByVal ObjectID As Integer) As Boolean
        Dim StrSqlCommand As String = String.Empty
        Try
            StrSqlCommand = "Select Remarks from sys_ObjectsRemarks where ObjectID = " & ObjectID & " and RecordID =" & ID
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSqlCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Remarks = mDataHandler.DataValue_Out(mDataSet.Tables(0).Rows(0).Item(0), SqlDbType.VarChar)
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function


End Class
