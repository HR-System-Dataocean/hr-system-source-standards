Public Class ClsReportsCustomizations
    Inherits ClsDataAcessLayer
    Public Sub New(ByVal page As Global.System.Web.UI.Page)
        MyBase.New(page)
        mTable = " sys_ReportsCustomizations"
        mInsertParameter = "ReportID,GroupID,UserID,ReportDesign"
        mInsertParameterValues = " @ReportID,@GroupID,@UserID,@ReportDesign"
        mUpdateParameter = "ReportID=@ReportID,GroupID=@GroupID,UserID=@UserID,ReportDesign=@ReportDesign"
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Delete From " & mTable & " "
    End Sub

#Region "private members"
    Private mID As Integer
    Private mReportID As Integer
    'Private mGroupID As Integer
    Private mUserID As Integer
    Private mReportDesign As String
#End Region
#Region "Public property"
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal value As Integer)
            mID = value
        End Set
    End Property
    Public Property ReportID() As Integer
        Get
            Return mReportID
        End Get
        Set(ByVal value As Integer)
            mReportID = value
        End Set
    End Property
    Public Property GroupID() As Integer
        Get
            Return mGroupID
        End Get
        Set(ByVal value As Integer)
            mGroupID = value
        End Set
    End Property
    Public Property UserID() As Integer
        Get
            Return mUserID
        End Get
        Set(ByVal value As Integer)
            mUserID = value
        End Set
    End Property
    Public Property ReportDesign() As String
        Get
            Return mReportDesign
        End Get
        Set(ByVal value As String)
            mReportDesign = value
        End Set
    End Property
#End Region
#Region "Public Function"
    '========================================================================
    'ProcedureName  :  Update
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By ReportID "
            End If
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            StrSelectCommand = StrSelectCommand & orderByStr
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, ConnectionString)
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Update
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Save() As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, "Save")
            mSqlCommand.Connection.Open()
            Return mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Return 0
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Update
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Update(ByVal Filter As String) As Integer
        Dim StrUpdateCommand As String = String.Empty

        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, "Update")
            'CLsWebHandlar.Add2History(ConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            mSqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Delete
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Delete record that match critera
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :  DataOcean  
    'Date Created   :  04-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand, "Delete")
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & mGroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, ConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & mGroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, ConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >'" & mID & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & mGroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, ConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  04-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < '" & mID & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & mGroupID & "),0) <> 1  " & IIf(Filter.Trim <> "", " And " & Filter, "") & "  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, ConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, [Shared].Errors.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Function
#End Region
#Region "Private Function"
    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         :  Sys (Report Writer Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[Mohammed Al-Janazrh]   
    'Date Created   :21-06-2009
    'Modifacations  :
    'Calls          :
    'From           :Find()
    '               :FirstRecord()
    '               :LastRecord()
    '               :PreviousRecord()
    '               :NextRecord()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Ds             :DataSet     :used its attributes to assign them to private attributes
    '========================================================================
    Protected Sub GetParameter(ByVal Ds As DataSet)
        Try
            With Ds.Tables(0).Rows(0)
                mID = [Shared].DataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mReportID = [Shared].DataHandler.DataValue_Out(.Item("ReportID"), SqlDbType.Int, True)
                mGroupID = [Shared].DataHandler.DataValue_Out(.Item("GroupID"), SqlDbType.Int, True)
                mUserID = [Shared].DataHandler.DataValue_Out(.Item("UserID"), SqlDbType.Int, True)
                mReportDesign = [Shared].DataHandler.DataValue_Out(.Item("ReportDesign"), SqlDbType.Xml)
            End With
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Sub
    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         :  Sys (Report Writer Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[Mohammed Al-Janazrh]   
    'Date Created   :21-06-2009
    'Modifacations  :
    'Calls          :
    'From           :Save()
    '               :Update()
    '               :Delete()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Protected Sub SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String)
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mReportID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GroupID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mGroupID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mUserID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportDesign", SqlDbType.Xml)).Value = [Shared].DataHandler.DataValue_In(mReportDesign, SqlDbType.Xml)
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Sub
    '========================================================================
    'ProcedureName  :  Clear
    'Module         :  Sys (Report Writer Module)
    'Project        :  Venus V.
    'Description    :  Clear all private attributes in class
    'Developer      :[Mohammed Al-Janazrh]   
    'Date Created   :21-06-2009
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Sub Clear()
        Try
            mID = 0
            mReportID = 0
            mGroupID = 0
            mUserID = 0
            mReportDesign = String.Empty
        Catch ex As Exception
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
        End Try
    End Sub
#End Region
#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region

End Class
