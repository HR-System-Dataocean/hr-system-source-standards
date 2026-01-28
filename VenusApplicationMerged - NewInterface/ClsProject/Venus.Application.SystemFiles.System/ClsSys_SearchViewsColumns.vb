'Modifications 
' [0256] B#000001 Table hrs_SearchViewsColumns  Become sys_SearchsColumns
' [0256] B#000002 ColumnLanguage Data Type is changed from Bit data Type to Char data Type with length 1 bit --> char(1)
' [0256] B#000003 ColumnName ---> Has been removed from sys_SearchsColumns Table 
' [0256] B#000004 FieldID added to DB Table instead of columnName 
' [0256] B#000005 Filed IsAvailable Removed From  sys_SearchsColumns Table 
' [0256] B#000006 SearchViewID ---> SearchID 
' [0256] B#000007 ColumnType Has been  Removed From sys_SearchsColumns Table 
' [0256] B#000008 ColumnActualLength has been Removed From sys_SearchsColumns Table 
Public Class ClsSys_SearchViewsColumns
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        '[0256] Search Table Name was changed  
        'mTable = " hrs_SearchViewsColumns "
        mTable = " sys_SearchsColumns "

        'mInsertParameter = " SearchViewID,ColumnName,ColumnEngDescription,ColumnArbDescription,IsCriteria,ColumnType,ColumnActualLength,ColumnLength,ColumnLanguage,ColumnAlignment,IsAvailable,RegDate,RegUserID,CancelDate "
        'mInsertParameterValues = " @SearchViewID,@ColumnName,@ColumnEngDescription,@ColumnArbDescription,@IsCriteria,@ColumnType,@ColumnActualLength,@ColumnLength,@ColumnLanguage,@ColumnAlignment,@IsAvailable,@RegDate,@RegUserID,@CancelDate "
        'mUpdateParameter = " SearchViewID=@SearchViewID,ColumnName=@ColumnName,ColumnEngDescription=@ColumnEngDescription,ColumnArbDescription=@ColumnArbDescription,IsCriteria=@IsCriteria,ColumnType=@ColumnType,ColumnActualLength=@ColumnActualLength,ColumnLength=@ColumnLength,ColumnLanguage=@ColumnLanguage,ColumnAlignment=@ColumnAlignment,IsAvailable=@IsAvailable,RegDate=@RegDate,RegUserID=@RegUserID,CancelDate=@CancelDate "
        'mSelectCommand = " Select * From  " & mTable
        'mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        'mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        'mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"

        '
        mInsertParameter = " SearchID,fieldid,ColumnEngDescription,ColumnArbDescription,IsCriteria,ColumnLength,ColumnLanguage,ColumnAlignment,RegDate,RegUserID,CancelDate "
        mInsertParameterValues = " @SearchID,@fieldID,@ColumnEngDescription,@ColumnArbDescription,@IsCriteria,@ColumnLength,@ColumnLanguage,@ColumnAlignment,@RegDate,@RegUserID,@CancelDate "
        mUpdateParameter = " SearchID=@SearchID,Fieldid=@Fieldid,ColumnEngDescription=@ColumnEngDescription,ColumnArbDescription=@ColumnArbDescription,IsCriteria=@IsCriteria,ColumnLength=@ColumnLength,ColumnLanguage=@ColumnLanguage,ColumnAlignment=@ColumnAlignment,RegDate=@RegDate,RegUserID=@RegUserID,CancelDate=@CancelDate "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"

        '
        myTable = New DataTable
    End Sub

#End Region

#Region "Private Members"

    Private mID As Object
    Private mSearchID As Object
    Private mSearchViewID As Object
    'Added Instead of mColumnName 
    Private mFieldID As Object
    'Private mColumnName As String
    Private mColumnEngDescription As String
    Private mColumnArbDescription As String
    Private mIsCriteria As Boolean
    Private mColumnType As String
    Private mColumnActualLength As String
    Private mColumnLength As String
    Private mColumnLanguage As Boolean
    Private mColumnAlignment As Object
    Private mIsAvailable As Boolean
    Private mRegDate As Object
    Private mRegUserID As Object
    Private mCancelDate As Object
    'Just For Test Purpose 
    Public myTable As DataTable
#End Region

#Region "Public property"
    'Just For Test Purpose 
    'Public Property MyTable() As Object
    '    Get
    '        mMyTable = MyTable
    '    End Get
    '    Set(ByVal value As Object)
    '        MyTable = value
    '    End Set
    'End Property
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
    'Public Property SearchViewID() As Object
    '    Get
    '        Return mSearchViewID
    '    End Get
    '    Set(ByVal Value As Object)
    '        mSearchViewID = Value
    '    End Set
    'End Property


    'Public Property ColumnName() As String
    '    Get
    '        Return mColumnName
    '    End Get
    '    Set(ByVal Value As String)
    '        mColumnName = Value
    '    End Set
    'End Property

    Public Property FieldID() As String
        Get
            Return mFieldID
        End Get
        Set(ByVal Value As String)
            mFieldID = Value
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

    Public Property IsAvailable() As Boolean
        Get
            Return mIsAvailable
        End Get
        Set(ByVal Value As Boolean)
            mIsAvailable = Value
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
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function CountColumns(ByVal SearchViewID As Integer) As Integer
        Try
            Find("SearchViewID=" & SearchViewID & " And IsCriteria=1 And IsAvailable=1")
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Return mDataSet.Tables(0).Rows.Count
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_SearchViewsColumns Where " & Filter
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
            'mSearchViewID = 0
            mSearchID = 0
            'mColumnName = String.Empty
            mFieldID = 0
            mColumnEngDescription = String.Empty
            mColumnArbDescription = String.Empty
            mIsCriteria = False
            mColumnType = String.Empty
            mColumnActualLength = String.Empty
            mColumnLength = String.Empty
            mColumnLanguage = False
            mColumnAlignment = 0
            mIsAvailable = False
            mRegDate = Nothing
            mRegUserID = 0
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
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                '[0256] Column SearchViewID Has Been Changed To SearchID 
                '      mSearchViewID has Been Changed to mSearchID 
                'mSearchViewID = mDataHandler.DataValue_Out(.Item("SearchViewID"), SqlDbType.Int)
                mSearchID = mDataHandler.DataValue_Out(.Item("SearchID"), SqlDbType.Int)

                'mColumnName = mDataHandler.DataValue_Out(.Item("ColumnName"), SqlDbType.VarChar)

                mFieldID = mDataHandler.DataValue_Out(.Item("FieldID"), SqlDbType.Int)
                mColumnEngDescription = mDataHandler.DataValue_Out(.Item("ColumnEngDescription"), SqlDbType.VarChar)
                mColumnArbDescription = mDataHandler.DataValue_Out(.Item("ColumnArbDescription"), SqlDbType.VarChar)
                mIsCriteria = mDataHandler.DataValue_Out(.Item("IsCriteria"), SqlDbType.Bit)
                '[0256] 150707 B#000007     
                'mColumnType = mDataHandler.DataValue_Out(.Item("ColumnType"), SqlDbType.VarChar)

                'mColumnActualLength = mDataHandler.DataValue_Out(.Item("ColumnActualLength"), SqlDbType.VarChar)
                mColumnLength = mDataHandler.DataValue_Out(.Item("ColumnLength"), SqlDbType.VarChar)

                ' [0256] B#000002
                'mColumnLanguage = mDataHandler.DataValue_Out(.Item("ColumnLanguage"), SqlDbType.Bit)
                mColumnLanguage = mDataHandler.DataValue_Out(.Item("ColumnLanguage"), SqlDbType.Char)

                mColumnAlignment = mDataHandler.DataValue_Out(.Item("ColumnAlignment"), SqlDbType.Int)
                ' [0256] B#000005
                'mIsAvailable = mDataHandler.DataValue_Out(.Item("IsAvailable"), SqlDbType.Bit)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int)
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
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchViewID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSearchViewID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SearchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSearchID, SqlDbType.Int)
            ' [0256] B#0003  
            ' Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldID", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFieldID, SqlDbType.Int)

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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int)
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

