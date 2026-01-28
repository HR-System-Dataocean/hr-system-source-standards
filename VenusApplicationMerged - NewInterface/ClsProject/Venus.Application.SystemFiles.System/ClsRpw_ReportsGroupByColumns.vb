
'=========================================================================
'Created by : 0258
'Date : 06/01/2007
'                   Class: ReportsGroupByColumns
'                   Table: 
'=========================================================================

Public Class ClsRpw_ReportsGroupByColumns
    Inherits ClsDataAcessLayer

#Region "Class Constructor"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " rpw_ColumnsGroupByFields "
        mInsertParameter = " ReportID,ReportColumnID,Rank"
        mInsertParameterValues = " @ReportID,@ReportColumnID,@Rank"
        mUpdateParameter = " ReportID = @ReportID , ReportColumnID=@ReportColumnID,Rank=@Rank "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        'mDeleteCommand = " Delete  " & mTable
    End Sub

#End Region

#Region "Private Members"

    Private mID As Integer
    Private mReportID As Integer
    Private mReportColumnID As Integer
    'Private mEngDescription As String
    'Private mArbDescription As String
    'Private mIsSorted As Byte
    Private mRank As Integer

#End Region

#Region "Public Property"

    Public Property ReportID() As Integer
        Get
            Return mReportID
        End Get
        Set(ByVal value As Integer)
            mReportID = value
        End Set
    End Property
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal Value As Integer)
            mID = Value
        End Set
    End Property

    Public Property ReportColumnID() As Integer
        Get
            Return mReportColumnID
        End Get
        Set(ByVal Value As Integer)
            mReportColumnID = Value
        End Set
    End Property

    'Public Property EngDescription() As String
    '    Get
    '        Return mEngDescription
    '    End Get
    '    Set(ByVal Value As String)
    '        mEngDescription = Value
    '    End Set
    'End Property

    'Public Property ArbDescription() As String
    '    Get
    '        Return mArbDescription
    '    End Get
    '    Set(ByVal Value As String)
    '        mArbDescription = Value
    '    End Set
    'End Property

    'Public Property IsSorted() As Byte
    '    Get
    '        Return mIsSorted
    '    End Get
    '    Set(ByVal Value As Byte)
    '        mIsSorted = Value
    '    End Set
    'End Property

    Public Property Rank() As Integer
        Get
            Return mRank
        End Get
        Set(ByVal value As Integer)
            mRank = value
        End Set
    End Property
#End Region

#Region "Public Functions"

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
            Else
                clear()
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

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = " Select ID From " & Me.mTable & " Where " & Filter
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
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
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

    Public Function clear()
        Try
            mID = 0
            mReportColumnID = 0
            mReportID = 0
            mRank = 0
            'mEngDescription = String.Empty
            'mArbDescription = String.Empty
            'mIsSorted = Nothing
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SetRepGroupByProperties(ByVal IntReportID As Integer, ByVal dtGroupsProperties As Data.DataTable) As Boolean
        Try
            Dim ClsReportGroupBy As New ClsRpw_ReportsGroupByColumns(mPage)
            ClsReportGroupBy.clear()
            With ClsReportGroupBy
                If dtGroupsProperties.Rows.Count > 0 Then
                    .ID = mDataHandler.DataValue_Out(dtGroupsProperties.Rows(0).Item("ID"), SqlDbType.Int)
                    .ReportID = mDataHandler.DataValue_Out(dtGroupsProperties.Rows(0).Item("ReportID"), SqlDbType.Int)
                    .ReportColumnID = mDataHandler.DataValue_Out(dtGroupsProperties.Rows(0).Item("ReportColumnID"), SqlDbType.Int)
                    .Rank = mDataHandler.DataValue_Out(dtGroupsProperties.Rows(0).Item("Rank"), SqlDbType.Int)

                End If
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function GetRepGroupByProperties(ByVal IntReportID As Integer, ByRef dtTargetTable As Data.DataTable) As Boolean
        Dim ClsReportGroupBy As New ClsRpw_ReportsGroupByColumns(mPage)
        Try
            ClsReportGroupBy.Find(" ReportID= " & IntReportID)
            With ClsReportGroupBy
                If dtTargetTable.Rows.Count > 0 Then
                    dtTargetTable.Rows(0).Item("ID") = mDataHandler.DataValue_In(.ID, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ReportColumnID") = mDataHandler.DataValue_In(.ReportColumnID, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ReportID") = mDataHandler.DataValue_In(.ReportID, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("Rank") = mDataHandler.DataValue_In(.Rank, SqlDbType.Int)
                    'dtTargetTable.Rows(0).Item("EngDescription") = mDataHandler.DataValue_In(.EngDescription, SqlDbType.VarChar)
                    'dtTargetTable.Rows(0).Item("ArbDescription") = mDataHandler.DataValue_In(.ArbDescription, SqlDbType.VarChar)
                    'dtTargetTable.Rows(0).Item("IsSorted") = mDataHandler.DataValue_In(.IsSorted, SqlDbType.TinyInt)
                End If
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

#End Region

#Region "Class Private Functions"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mReportColumnID = mDataHandler.DataValue_Out(.Item("ReportColumnID"), SqlDbType.Int)
                mReportID = mDataHandler.DataValue_Out(.Item("ReportID"), SqlDbType.Int)
                'mEngDescription = mDataHandler.DataValue_Out(.Item("EngDescription"), SqlDbType.VarChar)
                'mArbDescription = mDataHandler.DataValue_Out(.Item("ArbDescription"), SqlDbType.VarChar)
                'mIsSorted = mDataHandler.DataValue_Out(.Item("IsSorted"), SqlDbType.TinyInt)
                mRank = mDataHandler.DataValue_Out(.Item("Rank"), SqlDbType.Int)
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportColumnID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportColumnID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportID, SqlDbType.Int)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngDescription, SqlDbType.VarChar)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbDescription, SqlDbType.VarChar)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsSorted", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mIsSorted, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRank, SqlDbType.Int)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function


#End Region

#Region "Class Destructor"

    Protected Overloads Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class
