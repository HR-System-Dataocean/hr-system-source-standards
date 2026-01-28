Imports Venus.Application.SystemFiles.System
Public Class Clshrs_PayrollHead
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_PayrollHead "
        mInsertParameter = " EmployeeID,WorkingPeriodID,Date,PostDate,Remarks,RegUserID,RegComputerID,CompanyId "
        mInsertParameterValues = " @EmployeeID,@WorkingPeriodID,@Date,@PostDate,@Remarks,@RegUserID,@RegComputerID,@CompanyId "
        mUpdateParameter = " EmployeeID=@EmployeeID,WorkingPeriodID=@WorkingPeriodID,Date=@Date,PostDate=@PostDate,Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID,CompanyId=@CompanyId "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    Private mID As Object
    Private mEmployeeID As Object
    Private mWorkingPeriodID As Object
    Private mDate As Object
    Private mPostDate As Object
    Private mGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mCompanyId As Integer

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

    Public Property EmployeeID() As Object
        Get
            Return mEmployeeID
        End Get
        Set(ByVal Value As Object)
            mEmployeeID = Value
        End Set
    End Property

    Public Property WorkingPeriodID()
        Get
            Return mWorkingPeriodID
        End Get
        Set(ByVal value)
            mWorkingPeriodID = value
        End Set
    End Property

    Public Property dbDate() As Object
        Get
            Return mDate
        End Get
        Set(ByVal Value As Object)
            mDate = Value
        End Set
    End Property
    Public Property PostDate() As Object
        Get
            Return mPostDate
        End Get
        Set(ByVal Value As Object)
            mPostDate = Value
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
    Public Property Grid() As Infragistics.WebUI.UltraWebGrid.UltraWebGrid
        Get
            Return mGrid
        End Get
        Set(ByVal value As Infragistics.WebUI.UltraWebGrid.UltraWebGrid)
            mGrid = value
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

    Public Property CompanyId() As Integer
        Get
            Return mCompanyId
        End Get
        Set(ByVal value As Integer)
            mCompanyId = value
        End Set
    End Property

#End Region

#Region "Public Function"
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_PayrollHead Where " & Filter
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
        Dim IntRecordId As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand()
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()

            IntRecordId = mSqlCommand.ExecuteScalar()
            SaveTransactions(IntRecordId, mSqlCommand)
            SavePayability(IntRecordId, mSqlCommand)
            mSqlCommand.Connection.Close()

            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            mSqlCommand.Connection.Close()
        End Try
    End Function
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Dim IntRecordId As Integer
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            IntRecordId = mSqlCommand.ExecuteScalar()
            SaveTransactions(IntRecordId, mSqlCommand)
            SavePayability(IntRecordId, mSqlCommand)
            mSqlCommand.Connection.Close()

            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            mSqlCommand.Connection.Close()
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
            mEmployeeID = 0
            mDate = Nothing
            mPostDate = Nothing
            mRemarks = String.Empty
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
#End Region

#Region "Class Private Function"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int)
                mWorkingPeriodID = mDataHandler.DataValue_Out(.Item("WorkingPeriodID"), SqlDbType.Int)
                mDate = mDataHandler.DataValue_Out(.Item("Date"), SqlDbType.DateTime)
                mPostDate = mDataHandler.DataValue_Out(.Item("PostDate"), SqlDbType.DateTime)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mCompanyId = mDataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int)
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@WorkingPeriodID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mWorkingPeriodID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Date", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PostDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mPostDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int)

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function SaveTransactions(ByVal RecordId As Integer, ByVal SqlCommand As SqlClient.SqlCommand) As Boolean
        Dim ClsContract As New Clshrs_Contracts(mPage)
        Dim ClsContractTransaction As New Clshrs_ContractsTransactions(mPage)
        Dim ClsTransactionTypes As New Clshrs_TransactionsTypes(mPage)
        Dim ObjTransactionDs As New DataSet
        Dim ObjDataRow As DataRow
        Dim StrSqlCommand As String = String.Empty
        Dim IntContractTransactionId As Integer
        Dim IntContractTransactionWorkingPeriodId As Integer
        Dim IntContractTransactionTimeIntervalId As Integer
        Dim IntEmployeeId As Integer
        Dim IntPeriodId As Integer
        Dim IntTransactionTypeId As Integer
        Dim BolStatus As Boolean
        Dim BolRepeated As Boolean

        Try

            ClsContract.Find("EmployeeId=" & Me.EmployeeID & " And GetDate() between StartDate and IsNull(EndDate,dateadd(hh,1,GetDate()))")
            ClsContractTransaction.Find("ContractID=" & ClsContract.ID)
            StrSqlCommand = "Delete From hrs_PayrollDetail Where PayrollHeadID=" & RecordId & ";" & vbNewLine
            ObjTransactionDs = ClsContractTransaction.DataSet
            For Each ObjDataRow In ObjTransactionDs.Tables(0).Rows

                IntContractTransactionId = Me.mDataHandler.DataValue_Out(ObjDataRow.Item("ID"), SqlDbType.Int)
                IntContractTransactionWorkingPeriodId = Me.mDataHandler.DataValue_Out(ObjDataRow.Item("WorkingPeriodId"), SqlDbType.Int)
                IntContractTransactionTimeIntervalId = Me.mDataHandler.DataValue_Out(ObjDataRow.Item("TimeIntervalTypeID"), SqlDbType.Int)
                BolStatus = IIf(Me.mDataHandler.DataValue_Out(ObjDataRow.Item("Status"), SqlDbType.Int) = 0, False, True)
                IntTransactionTypeId = Me.mDataHandler.DataValue_Out(ObjDataRow.Item("TransactionTypeID"), SqlDbType.Int)
                IntEmployeeId = Me.EmployeeID
                IntPeriodId = Me.WorkingPeriodID
                ClsTransactionTypes.Find("Id=" & IntTransactionTypeId)
                BolRepeated = True

                'If ClsContractTransaction.CheckValidTransaction(IntContractTransactionId, IntContractTransactionWorkingPeriodId, IntContractTransactionTimeIntervalId, IntEmployeeId, IntPeriodId, BolRepeated, BolStatus) Then
                '    StrSqlCommand &= "Insert Into hrs_PayrollDetail(PayrollHeadID,ContractTransactionID,RegUserId,CompanyId)Select " & _
                '    "PayrollHeadID=" & RecordId & "," & _
                '    "ContractTransactionID=" & ObjDataRow.Item("ID") & "," & _
                '    "RegUserId=" & Me.mDataBaseUserRelatedID & "," & _
                '    "CompanyId=" & Me.MainCompanyID & ";" & vbNewLine
                'End If

            Next
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSqlCommand
            If Len(StrSqlCommand) > 0 Then
                mSqlCommand.ExecuteNonQuery()
            End If


        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function SavePayability(ByVal RecordId As Integer, ByVal SqlCommand As SqlClient.SqlCommand) As Boolean
        Dim ClsEmployeePayability As New Clshrs_EmployeesPayabilitySchedules(mPage)
        Dim ClsWorkingPeriod As New Clshrs_WorkingPeriods(mPage)
        Dim IntEmployeeId As Integer
        Dim IntPeriodId As Integer
        Dim ObjDs As New DataSet
        Dim ObjDataRow As DataRow
        Dim StrSqlCommand As String = String.Empty
        Try
            IntEmployeeId = Me.EmployeeID
            IntPeriodId = Me.WorkingPeriodID
            ClsWorkingPeriod.Find("Id=" & IntPeriodId)
            ClsEmployeePayability.FindCurrentPayment("DueDate between '" & ClsWorkingPeriod.FromDate & "' And '" & ClsWorkingPeriod.ToDate & "' And EmployeeId=" & IntEmployeeId)
            ObjDs = ClsEmployeePayability.DataSet
            If mDataHandler.CheckValidDataObject(ObjDs) Then
                For Each ObjDataRow In ObjDs.Tables(0).Rows
                    StrSqlCommand &= "Insert Into hrs_PayrollDetail(PayrollHeadID,EmployeePayabilityScheduleID,RegUserId,CompanyId)Select " & _
                                      "PayrollHeadID=" & RecordId & "," & _
                                      "EmployeePayabilityScheduleID=" & ObjDataRow.Item("ID") & "," & _
                                      "RegUserId=" & Me.mDataBaseUserRelatedID & "," & _
                                      "CompanyId=" & Me.MainCompanyID & ";" & vbNewLine
                Next
            End If
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSqlCommand
            If Len(StrSqlCommand) > 0 Then
                mSqlCommand.ExecuteNonQuery()
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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

