
'=========================================================================
'Created by : DataOcean
'Date : 08/08/2007
'                   Class: EndOfServicesRules
'                   Table: hrs_EndOfServiceRules
'                   Relations:
'                               hrs_EndOfServiceRules.EndOfServiceID ->> hrs_EndOfServices.ID
'=========================================================================

'Public Class Clshrs_EndOfServicesRules
'    Inherits ClsDataAcessLayer

'#Region "Class Constructors"
'    Public Sub New(ByVal Page As Web.UI.Page)
'        MyBase.New(Page)
'        mTable = " hrs_EndOfServicesRules "
'        mInsertParameter = " EndOfServiceId,FromWorkingMonths,ToWorkingMonths,AmountPercent,Formula,Remarks,RegUserID,RegComputerID "
'        mInsertParameterValues = " @EndOfServiceId,@FromWorkingMonths,@ToWorkingMonths,@AmountPercent,@Formula,@Remarks,@RegUserID,@RegComputerID "
'        mUpdateParameter = " EndOfServiceId=@EndOfServiceId,FromWorkingMonths=@FromWorkingMonths,ToWorkingMonths=@ToWorkingMonths,AmountPercent=@AmountPercent,Formula=@Formula,Remarks=@Remarks "
'        mSelectCommand = " Select * From  " & mTable
'        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
'        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
'        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
'    End Sub
'#End Region

'#Region "Private Members"
'    Private mId As Object
'    Private mEndOfServiceId As Object
'    Private mFromWorkingMonths As Object
'    Private mToWorkingMonths As Object
'    Private mAmountPercent As Object
'    Private mFormula As String
'    Private mRemarks As String
'    Private mRegUserID As Object
'    Private mRegComputerID As Object
'    Private mRegDate As Object
'    Private mCancelDate As Object


'#End Region

'#Region "Public property"
'    Public Property Id() As Object
'        Get
'            Return mId
'        End Get
'        Set(ByVal Value As Object)
'            mId = Value
'        End Set
'    End Property
'    Public Property EndOfServiceTypeId() As Object
'        Get
'            Return mEndOfServiceId
'        End Get
'        Set(ByVal Value As Object)
'            mEndOfServiceId = Value
'        End Set
'    End Property
'    Public Property FromWorkingMonths() As Object
'        Get
'            Return mFromWorkingMonths
'        End Get
'        Set(ByVal Value As Object)
'            mFromWorkingMonths = Value
'        End Set
'    End Property
'    Public Property ToWorkingMonths() As Object
'        Get
'            Return mToWorkingMonths
'        End Get
'        Set(ByVal Value As Object)
'            mToWorkingMonths = Value
'        End Set
'    End Property
'    Public Property AmountPercent() As Object
'        Get
'            Return mAmountPercent
'        End Get
'        Set(ByVal Value As Object)
'            mAmountPercent = Value
'        End Set
'    End Property
'    Public Property Formula() As String
'        Get
'            Return mFormula
'        End Get
'        Set(ByVal Value As String)
'            mFormula = Value
'        End Set
'    End Property
'    Public Property Remarks() As String
'        Get
'            Return mRemarks
'        End Get
'        Set(ByVal Value As String)
'            mRemarks = Value
'        End Set
'    End Property
'    Public Property RegUserID() As Object
'        Get
'            Return mRegUserID
'        End Get
'        Set(ByVal Value As Object)
'            mRegUserID = Value
'        End Set
'    End Property
'    Public Property RegComputerID() As Object
'        Get
'            Return mRegComputerID
'        End Get
'        Set(ByVal Value As Object)
'            mRegComputerID = Value
'        End Set
'    End Property
'    Public Property RegDate() As Object
'        Get
'            Return mRegDate
'        End Get
'        Set(ByVal Value As Object)
'            mRegDate = Value
'        End Set
'    End Property
'    Public Property CancelDate() As Object
'        Get
'            Return mCancelDate
'        End Get
'        Set(ByVal Value As Object)
'            mCancelDate = Value
'        End Set
'    End Property
'#End Region

'#Region "Public Function"
'    Public Function Find(ByVal Filter As String) As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
'            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
'            mDataSet = New DataSet
'            mSqlDataAdapter.Fill(mDataSet)
'            If mDataHandler.CheckValidDataObject(mDataSet) Then
'                GetParameter(mDataSet)
'            Else
'                Clear()
'            End If
'            If mId > 0 Then
'                Return True
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'    Public Function SaveUpdate(ByVal Filter As String) As Boolean
'        Dim strSQL As String
'        Dim Value As Integer
'        Try
'            strSQL = "Select ID From hrs_EndOfServicesRules Where " & Filter
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = strSQL
'            mSqlCommand.Connection.Open()
'            Value = mSqlCommand.ExecuteScalar()
'            mSqlCommand.Connection.Close()
'            If Value > 0 Then
'                Update(Filter)
'            Else
'                Save()
'            End If
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(strSQL, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'    Public Function Save() As Boolean
'        Try
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = mInsertCommand
'            SetParameter(mSqlCommand)
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'    Public Function Update(ByVal Filter As String) As Boolean
'        Dim StrUpdateCommand As String
'        Try
'            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = StrUpdateCommand
'            SetParameter(mSqlCommand)
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'    Public Function Delete(ByVal Filter As String) As Boolean
'        Dim StrDeleteCommand As String
'        Try
'            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
'            mSqlCommand = New SqlClient.SqlCommand
'            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
'            mSqlCommand.CommandType = CommandType.Text
'            mSqlCommand.CommandText = StrDeleteCommand
'            SetParameter(mSqlCommand)
'            mSqlCommand.Connection.Open()
'            mSqlCommand.ExecuteNonQuery()
'            mSqlCommand.Connection.Close()
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'    Public Function Clear() As Boolean
'        Try
'            mId = 0
'            mEndOfServiceId = 0
'            mFromWorkingMonths = 0
'            mToWorkingMonths = 0
'            mAmountPercent = 0.0
'            mFormula = String.Empty
'            mRemarks = String.Empty
'            mRegUserID = 0
'            mRegComputerID = 0
'            mRegDate = Nothing
'            mCancelDate = Nothing

'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'    Public Function FirstRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
'            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
'            mDataSet = New DataSet
'            mSqlDataAdapter.Fill(mDataSet)
'            If mDataHandler.CheckValidDataObject(mDataSet) Then
'                GetParameter(mDataSet)
'                Return True
'            Else
'                Clear()
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'    Public Function LastRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
'            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
'            mDataSet = New DataSet
'            mSqlDataAdapter.Fill(mDataSet)
'            If mDataHandler.CheckValidDataObject(mDataSet) Then
'                GetParameter(mDataSet)
'                Return True
'            Else
'                Clear()
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'    Public Function NextRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mId & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
'            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
'            mDataSet = New DataSet
'            mSqlDataAdapter.Fill(mDataSet)
'            If mDataHandler.CheckValidDataObject(mDataSet) Then
'                GetParameter(mDataSet)
'                Return True
'            Else
'                Clear()
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'    Public Function previousRecord() As Boolean
'        Dim StrSelectCommand As String
'        Try
'            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mId & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
'            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
'            mDataSet = New DataSet
'            mSqlDataAdapter.Fill(mDataSet)
'            If mDataHandler.CheckValidDataObject(mDataSet) Then
'                GetParameter(mDataSet)
'                Return True
'            Else
'                Clear()
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function

'    '=====================================================================
'    'Created by : [0259]
'    'Date : 09/08/2007
'    'Steps: 
'    '               - Execute StrSelectCommand to determine the required recoed data from the table
'    '               - Excute CheckValidDataObject
'    '               - Return true
'    '
'    'Description:   Get the rules equivelant to the record
'    '=====================================================================

'    Public Function GetRules(ByVal EndOfServiceID As Integer, ByRef ObjDs As DataSet) As Boolean
'        Dim StrSelectCommand As String = String.Empty
'        Try
'            ObjDs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, "hrs_GetEnfOfServiceRules", EndOfServiceID)
'            If mDataHandler.CheckValidDataObject(ObjDs) Then
'                Return True
'            End If
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function


'#End Region

'#Region "Class Private Function"
'    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
'        Try
'            With Ds.Tables(0).Rows(0)
'                mId = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int,true )
'                mEndOfServiceId = mDataHandler.DataValue_Out(.Item("EndOfServiceID"), SqlDbType.Int,true )
'                mFromWorkingMonths = mDataHandler.DataValue_Out(.Item("FromWorkingMonths"), SqlDbType.Int)
'                mToWorkingMonths = mDataHandler.DataValue_Out(.Item("ToWorkingMonths"), SqlDbType.Int)
'                mAmountPercent = mDataHandler.DataValue_Out(.Item("AmountPercent"), SqlDbType.Real)
'                mFormula = mDataHandler.DataValue_Out(.Item("Formula"), SqlDbType.VarChar)
'                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
'                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int,true )
'                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int,true )
'                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
'                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
'            End With
'            Return True
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
'        Try
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EndOfServiceTypeId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEndOfServiceID, SqlDbType.Int , True )
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FromWorkingMonths", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFromWorkingMonths, SqlDbType.Int)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ToWorkingMonths", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mToWorkingMonths, SqlDbType.Int)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AmountPercent", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mAmountPercent, SqlDbType.Real)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Formula", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFormula, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegUserID, SqlDbType.Int , True )
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int , True )
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mRegDate, SqlDbType.DateTime)
'            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)
'        Catch ex As Exception
'            mPage.Session.Add("ErrorValue", ex)
'            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
'            mPage.Response.Redirect("ErrorPage.aspx")
'        End Try
'    End Function
'#End Region

'#Region "Class Destructors"
'    Public Sub finalized()
'        mDataSet.Dispose()
'    End Sub
'#End Region

'End Class

