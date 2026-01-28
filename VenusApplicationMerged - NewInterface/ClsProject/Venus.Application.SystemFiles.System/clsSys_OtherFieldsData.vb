'======================================================================
'Project name  : Venus V. 
'Program name  : clsSys_OtherFieldsData.vb
'Date Created  : 08-08-2007
'Issue #       :       
'Developer     : [0256] 
'Description   : 
'              : 
'              : 
'              : 
'Modifacations :
'======================================================================

Public Class clsSys_OtherFieldsData
    Inherits ClsDataAcessLayer
#Region "Class Constructor"
    '========================================================================
    'ProcedureName  :  new()
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Initialize Class Sql Related Members 
    'Developer      :   
    'Date Created   :  08-08-2007
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_OtherFieldsData "
        mInsertParameter = "  OtherFieldID , RecordID , Data "
        mInsertParameterValues = "  @OtherFieldID , @RecordID , @Data "
        mUpdateParameter = "OtherFieldID =@OtherFieldID ,RecordID =@RecordID ,Data=@Data   "
        'mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        'mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
        mSelectCommand = " Select sys_OtherFieldsData.ID, sys_OtherFieldsData.OtherFieldID ,sys_OtherFieldsData.RecordID  , " & _
                         " sys_OtherFieldsData.Data From sys_OtherFieldsData right Join  " & _
                         " sys_OtherFields on sys_OtherFieldsData.OtherFieldID = sys_OtherFields.ID  "
    End Sub
#End Region

#Region "Constants"

#End Region

#Region "Private Members"
    Private mID As Integer
    Private mOtherFieldID As Integer
    Private mRecordID As Integer
    Private mData As String
#End Region

#Region "Public Properties"
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal value As Integer)
            mID = value
        End Set
    End Property
    Public Property OtherFielID() As Integer
        Get
            Return mOtherFieldID
        End Get
        Set(ByVal value As Integer)
            mOtherFieldID = value
        End Set
    End Property

    Public Property RecordID() As Integer
        Get
            Return mRecordID
        End Get
        Set(ByVal value As Integer)
            mRecordID = value
        End Set
    End Property
    Public Property FieldData() As String
        Get
            Return mData
        End Get
        Set(ByVal value As String)
            mData = value
        End Set
    End Property

#End Region
#Region "Public Methods"
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter, "") 'sys_OtherFields.CancelDate is null
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
        Dim StrUpdateCommand As String = String.Empty
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
    Public Function Clear() As Boolean
        Try
            mID = 0
            mOtherFieldID = 0
            mRecordID = 0
            mData = String.Empty
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region

#Region "Private Methods"
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mOtherFieldID = mDataHandler.DataValue_Out(.Item("OtherFieldID"), SqlDbType.Int, True)
                mRecordID = mDataHandler.DataValue_Out(.Item("RecordID"), SqlDbType.Int, True)
                mData = mDataHandler.DataValue_Out(.Item("Data"), SqlDbType.VarChar)
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RecordID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRecordID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OtherFieldID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mOtherFieldID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Data", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mData, SqlDbType.VarChar)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region


End Class
