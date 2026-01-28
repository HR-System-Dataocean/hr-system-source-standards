Public Class SearchViews

#Region "Class Constructors"
    Public Sub New(ByVal ConnectionStr As String, ByVal Page As System.Web.UI.Page)
        mConnectionString = ConnectionStr
        mPage = Page
        mErrorHandler = New Venus.Shared.ErrorsHandler(mConnectionString)
    End Sub

#End Region
#Region "Private Members"
    Private mDataSet As System.Data.DataSet
    Private mSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Private mSqlCommand As System.Data.SqlClient.SqlCommand
    Private mSqlDataReader As System.Data.SqlClient.SqlDataReader
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Private mDataHandler As New Venus.Shared.DataHandler
    Private mConnectionString As String
    Private mPage As System.Web.UI.Page
    Private mID As Object
    Private mCode As String
    Private mEngDescription As String
    Private mArbDescription As String
    Private mTableName As String
    Private mRegDate As Object
    Private mRegUserID As Object
    Private mCancelDate As Object
    Private mTable As String = " sys_Searchs "
    Private mInsertParameter As String = " Code,EngDescription,ArbDescription,TableName,RegDate,RegUserID,CancelDate "
    Private mInsertParameterValues As String = " @Code,@EngDescription,@ArbDescription,@TableName,@RegDate,@RegUserID,@CancelDate "
    Private mUpdateParameter As String = " Code=@Code,EngDescription=@EngDescription,ArbDescription=@ArbDescription,TableName=@TableName,RegDate=@RegDate,RegUserID=@RegUserID,CancelDate=@CancelDate "
    Private mSelectCommand As String = " Select * From  " & mTable
    Private mInsertCommand As String = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
    Private mUpdateCommand As String = " Update " & mTable & " Set " & mUpdateParameter
    Private mDeleteCommand As String = "Update " & mTable & " SET CancelDate=GetDate()"
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
    Public Property Code() As String
        Get
            Return mCode
        End Get
        Set(ByVal Value As String)
            mCode = Value
        End Set
    End Property
    Public Property EngDescription() As String
        Get
            Return mEngDescription
        End Get
        Set(ByVal Value As String)
            mEngDescription = Value
        End Set
    End Property
    Public Property ArbDescription() As String
        Get
            Return mArbDescription
        End Get
        Set(ByVal Value As String)
            mArbDescription = Value
        End Set
    End Property
    Public Property TableName() As String
        Get
            Return mTableName
        End Get
        Set(ByVal Value As String)
            mTableName = Value
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
    Public Property RegUserID() As Object
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Object)
            mRegUserID = Value
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

    Public ReadOnly Property MyDataSet() As DataSet
        Get
            Return mDataSet
        End Get
    End Property

#End Region
#Region "Public Function"
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
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
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Try
            Dim strSQL As String
            Dim Value As Integer
            strSQL = "Select ID From sys_Searchs Where " & Filter
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
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
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
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
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
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
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
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEngDescription = String.Empty
            mArbDescription = String.Empty
            mTableName = String.Empty
            mRegDate = Nothing
            mRegUserID = 0
            mCancelDate = Nothing

        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function FirstRecord() As Boolean
        Try
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function LastRecord() As Boolean
        Try
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function NextRecord() As Boolean
        Try
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function previousRecord() As Boolean
        Try
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function UpdateDescription(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String = "Update " & Me.mTable & " Set EngDescription='" & Me.EngDescription & "',ArbDescription='" & Me.ArbDescription & "' "
        Try
            StrUpdateCommand = StrUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Class Private Function"
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngDescription = mDataHandler.DataValue_Out(.Item("EngDescription"), SqlDbType.VarChar)
                mArbDescription = mDataHandler.DataValue_Out(.Item("ArbDescription"), SqlDbType.VarChar)
                mTableName = mDataHandler.DataValue_Out(.Item("TableName"), SqlDbType.VarChar)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
            End With
            Return True
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TableName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTableName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mRegDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegUserID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region

End Class

Public Class SearchViewsColumns

#Region "Class Constructors"
    Public Sub New(ByVal ConnectionStr As String, ByVal Page As System.Web.UI.Page)
        mConnectionString = ConnectionStr
        mPage = Page
        mErrorHandler = New Venus.Shared.ErrorsHandler(mConnectionString)
    End Sub
#End Region
#Region "Private Members"
    Private mDataSet As System.Data.DataSet
    Private mSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Private mSqlCommand As System.Data.SqlClient.SqlCommand
    Private mSqlDataReader As System.Data.SqlClient.SqlDataReader
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Private mDataHandler As New Venus.Shared.DataHandler
    Private mConnectionString As String
    Private mPage As System.Web.UI.Page
    Private mID As Object
    Private mSearchID As Object
    Private mColumnName As String
    Private mColumnEngDescription As String
    Private mColumnArbDescription As String
    Private mIsCriteria As Boolean
    Private mColumnType As String
    Private mColumnActualLength As String
    Private mColumnLength As String
    Private mColumnLanguage As Boolean
    Private mColumnAlignment As Object
    Private mIsAvailable As Object
    Private mRegDate As Object
    Private mRegUserID As Object
    Private mCancelDate As Object
    Private mTable As String = " sys_SearchsColumns "
    Private mInsertParameter As String = " SearchID,ColumnName,ColumnEngDescription,ColumnArbDescription,IsCriteria,ColumnType,ColumnActualLength,ColumnLength,ColumnLanguage,ColumnAlignment,IsAvailable,RegDate,RegUserID,CancelDate "
    Private mInsertParameterValues As String = " @SearchID,@ColumnName,@ColumnEngDescription,@ColumnArbDescription,@IsCriteria,@ColumnType,@ColumnActualLength,@ColumnLength,@ColumnLanguage,@ColumnAlignment,@IsAvailable,@RegDate,@RegUserID,@CancelDate "
    Private mUpdateParameter As String = " SearchID=@SearchID,ColumnName=@ColumnName,ColumnEngDescription=@ColumnEngDescription,ColumnArbDescription=@ColumnArbDescription,IsCriteria=@IsCriteria,ColumnType=@ColumnType,ColumnActualLength=@ColumnActualLength,ColumnLength=@ColumnLength,ColumnLanguage=@ColumnLanguage,ColumnAlignment=@ColumnAlignment,IsAvailable=@IsAvailable,RegDate=@RegDate,RegUserID=@RegUserID,CancelDate=@CancelDate "
    Private mSelectCommand As String = " Select * From  " & mTable
    Private mInsertCommand As String = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
    Private mUpdateCommand As String = " Update " & mTable & " Set " & mUpdateParameter
    Private mDeleteCommand As String = "Update " & mTable & " SET CancelDate=GetDate()"
#End Region
#Region "Public Enum"
    Public Enum Mode
        Save
        Delete
        Update
    End Enum
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
    Public Property ColumnName() As String
        Get
            Return mColumnName
        End Get
        Set(ByVal Value As String)
            mColumnName = Value
        End Set
    End Property
    Public Property ColumnEngDescription() As String
        Get
            Return mColumnEngDescription
        End Get
        Set(ByVal Value As String)
            mColumnEngDescription = Value
        End Set
    End Property
    Public Property ColumnArbDescription() As String
        Get
            Return mColumnArbDescription
        End Get
        Set(ByVal Value As String)
            mColumnArbDescription = Value
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
    Public Property ColumnType() As String
        Get
            Return mColumnType
        End Get
        Set(ByVal Value As String)
            mColumnType = Value
        End Set
    End Property
    Public Property ColumnActualLength() As String
        Get
            Return mColumnActualLength
        End Get
        Set(ByVal Value As String)
            mColumnActualLength = Value
        End Set
    End Property
    Public Property ColumnLength() As String
        Get
            Return mColumnLength
        End Get
        Set(ByVal Value As String)
            mColumnLength = Value
        End Set
    End Property
    Public Property ColumnLanguage() As Boolean
        Get
            Return mColumnLanguage
        End Get
        Set(ByVal Value As Boolean)
            mColumnLanguage = Value
        End Set
    End Property
    Public Property ColumnAlignment() As Object
        Get
            Return mColumnAlignment
        End Get
        Set(ByVal Value As Object)
            mColumnAlignment = Value
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
    Public Property RegUserID() As Object
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Object)
            mRegUserID = Value
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
    Public Property IsAvailable() As Boolean
        Get
            Return mIsAvailable
        End Get
        Set(ByVal value As Boolean)
            mIsAvailable = value
        End Set
    End Property
    Public ReadOnly Property MyTable() As DataTable
        Get
            Return Me.mDataSet.Tables(0)
        End Get
    End Property

#End Region
#Region "Public Function"
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
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
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Try
            Dim strSQL As String
            Dim Value As Integer
            strSQL = "Select ID From sys_SearchsColumns Where " & Filter
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
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
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
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
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
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
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
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function Clear() As Boolean
        Try
            mID = 0
            mSearchID = 0
            mColumnName = String.Empty
            mColumnEngDescription = String.Empty
            mColumnArbDescription = String.Empty
            mIsCriteria = False
            mColumnType = String.Empty
            mColumnActualLength = String.Empty
            mColumnLength = String.Empty
            mColumnLanguage = False
            mColumnAlignment = 0
            IsAvailable = False
            mRegDate = Nothing
            mRegUserID = 0
            mCancelDate = Nothing

        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function FirstRecord() As Boolean
        Try
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function LastRecord() As Boolean
        Try
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function NextRecord() As Boolean
        Try
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function previousRecord() As Boolean
        Try
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Public Function UpdateIsAvailable(ByVal ViewID As Integer, ByVal ColumnName As String, ByVal IsCriteria As Boolean) As Boolean
        Dim StrUpdateCommand As String
        Try
            StrUpdateCommand = "Update " & Me.mTable & " Set IsAvailable= " & IIf(Me.IsAvailable, 1, 0) & " Where SearchID=" & ViewID & " And ColumnName='" & ColumnName & "' And IsCriteria=" & IIf(IsCriteria, 1, 0)
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try

    End Function
    Public Function UpdateDescription(ByVal ColumnID As Integer) As Boolean
        Dim StrUpdateCommand As String
        Try
            StrUpdateCommand = "Update " & Me.mTable & " Set ColumnEngDescription='" & Me.ColumnEngDescription & "',ColumnArbDescription='" & Me.ColumnArbDescription & "',ColumnLength=" & Me.ColumnLength & ",ColumnLanguage=" & Me.ColumnLanguage & " Where ID = " & ColumnID
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try

    End Function
#End Region
#Region "Class Private Function"
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mSearchID = mDataHandler.DataValue_Out(.Item("SearchID"), SqlDbType.Int)
                mColumnName = mDataHandler.DataValue_Out(.Item("ColumnName"), SqlDbType.VarChar)
                mColumnEngDescription = mDataHandler.DataValue_Out(.Item("ColumnEngDescription"), SqlDbType.VarChar)
                mColumnArbDescription = mDataHandler.DataValue_Out(.Item("ColumnArbDescription"), SqlDbType.VarChar)
                mIsCriteria = mDataHandler.DataValue_Out(.Item("IsCriteria"), SqlDbType.Bit)
                mColumnType = mDataHandler.DataValue_Out(.Item("ColumnType"), SqlDbType.VarChar)
                mColumnActualLength = mDataHandler.DataValue_Out(.Item("ColumnActualLength"), SqlDbType.VarChar)
                mColumnLength = mDataHandler.DataValue_Out(.Item("ColumnLength"), SqlDbType.VarChar)
                mColumnLanguage = mDataHandler.DataValue_Out(.Item("ColumnLanguage"), SqlDbType.Bit)
                mColumnAlignment = mDataHandler.DataValue_Out(.Item("ColumnAlignment"), SqlDbType.Int)
                mIsAvailable = mDataHandler.DataValue_Out(.Item("IsAvailable"), SqlDbType.Bit)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
            End With
            Return True
        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSearchID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnName, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnEngDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnEngDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnArbDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnArbDescription, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsCriteria", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsCriteria, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnType", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnType, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnActualLength", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnActualLength, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnLength", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnLength, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnLanguage", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColumnLanguage, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnAlignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColumnAlignment, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsAvailable", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsAvailable, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mRegDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegUserID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)

        Catch ex As Exception
            ''''mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            '''mPage.Response.Redirect("../Sys/ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region

End Class

