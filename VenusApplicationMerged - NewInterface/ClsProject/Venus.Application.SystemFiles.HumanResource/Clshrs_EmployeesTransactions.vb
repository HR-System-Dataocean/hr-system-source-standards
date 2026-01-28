Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesTransactionsBase
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page, ByVal TableName As String)

        MyBase.New(Page)
        Me.Table = TableName
        Me.mInsertParameter = " EmployeeID,FinancialWorkingUnits,FiscalYearPeriodID,PostDate,PaidDate,Remarks,RegUserID,RegComputerID ,PrepareType,Applyed,CBranchID,CDepartmetnID,CSectorID,CCost1,CCost2,CCost3,CCost4,CMainProjectID,EmployeesVacationsID, TotalVacDaySettlement, RemainVacDaySettlement, LastPaidDate, RemainVacSettlement "
        Me.mInsertParameterValues = " @EmployeeID,@FinancialWorkingUnits,@FiscalYearPeriodID,@PostDate,@PaidDate,@Remarks,@RegUserID,@RegComputerID ,@PrepareType,@Applyed,@CBranchID,@CDepartmetnID,@CSectorID,@CCost1,@CCost2,@CCost3,@CCost4,@CMainProjectID,@EmployeesVacationsID,@TotalVacDaySettlement,@RemainVacDaySettlement,@LastPaidDate,@RemainVacSettlement "
        Me.mUpdateParameter = " EmployeeID=@EmployeeID,FinancialWorkingUnits=@FinancialWorkingUnits,FiscalYearPeriodID=@FiscalYearPeriodID,PostDate=@PostDate,PaidDate=@PaidDate,Remarks=@Remarks ,PrepareType =@PrepareType,Applyed=@Applyed "
        Me.mSelectCommand = " Select * From  " & mTable
        Me.mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        Me.mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        Me.mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"

    End Sub
#End Region

#Region "Private Members"

    Private mID As Object
    Private mEmployeeID As Integer
    Private mFinancialWorkingUnits As Double
    Private mFiscalYearPeriodID As Integer
    Private mPostDate As Object
    Private mPaidDate As Object
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mPrepareType As String
    Private mApplyed As Boolean

    Private mCBranchID As Object
    Private mCDepartmetnID As Object
    Private mCSectorID As Object
    Private mCCost1 As Object
    Private mCCost2 As Object
    Private mCCost3 As Object
    Private mCCost4 As Object
    Private mCMainProjectID As Object
    Private mEmployeesVacationsID As Integer
    Private mTotalVacDaySettlement As Integer
    Private mRemainVacDaySettlement As Double
    Private mLastPaidDate As Object
    Private mRemainVacSettlement As Double
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

    Public Property EmployeeID() As Integer
        Get
            Return mEmployeeID
        End Get
        Set(ByVal Value As Integer)
            mEmployeeID = Value
        End Set
    End Property

    Public Property FinancialWorkingUnits() As Double
        Get
            Return mFinancialWorkingUnits
        End Get
        Set(ByVal Value As Double)
            mFinancialWorkingUnits = Value
        End Set
    End Property

    Public Property FiscalYearPeriodID() As Integer
        Get
            Return mFiscalYearPeriodID
        End Get
        Set(ByVal Value As Integer)
            mFiscalYearPeriodID = Value
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

    Public Property PaidDate() As Object
        Get
            Return mPaidDate
        End Get
        Set(ByVal value As Object)
            mPaidDate = value
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

    Public ReadOnly Property RegUserID() As Object
        Get
            Return mRegUserID
        End Get
    End Property

    Public Property RegComputerID() As Object
        Get
            Return mRegComputerID
        End Get
        Set(ByVal Value As Object)
            mRegComputerID = Value
        End Set
    End Property

    Public ReadOnly Property RegDate() As Object
        Get
            Return mRegDate
        End Get
    End Property

    Public ReadOnly Property CancelDate() As Object
        Get
            Return mCancelDate
        End Get
    End Property

    Public Property PrepareType() As String
        Get
            Return mPrepareType
        End Get
        Set(ByVal value As String)
            mPrepareType = value
        End Set
    End Property

    Public Property Applyed() As Boolean
        Get
            Return mApplyed
        End Get
        Set(ByVal value As Boolean)
            mApplyed = value
        End Set
    End Property

    Public Property CBranchID() As Integer
        Get
            Return mCBranchID
        End Get
        Set(ByVal Value As Integer)
            mCBranchID = Value
        End Set
    End Property
    Public Property CDepartmetnID() As Integer
        Get
            Return mCDepartmetnID
        End Get
        Set(ByVal Value As Integer)
            mCDepartmetnID = Value
        End Set
    End Property
    Public Property CSectorID() As Integer
        Get
            Return mCSectorID
        End Get
        Set(ByVal Value As Integer)
            mCSectorID = Value
        End Set
    End Property
    Public Property CCost1() As Integer
        Get
            Return mCCost1
        End Get
        Set(ByVal Value As Integer)
            mCCost1 = Value
        End Set
    End Property
    Public Property CCost2() As Integer
        Get
            Return mCCost2
        End Get
        Set(ByVal Value As Integer)
            mCCost2 = Value
        End Set
    End Property
    Public Property CCost3() As Integer
        Get
            Return mCCost3
        End Get
        Set(ByVal Value As Integer)
            mCCost3 = Value
        End Set
    End Property
    Public Property CCost4() As Integer
        Get
            Return mCCost4
        End Get
        Set(ByVal Value As Integer)
            mCCost4 = Value
        End Set
    End Property
    Public Property CMainProjectID() As Integer
        Get
            Return mCMainProjectID
        End Get
        Set(ByVal Value As Integer)
            mCMainProjectID = Value
        End Set
    End Property
    Public Property EmployeesVacationsID() As Integer
        Get
            Return mEmployeesVacationsID
        End Get
        Set(ByVal Value As Integer)
            mEmployeesVacationsID = Value
        End Set
    End Property
    Public Property TotalVacDaySettlement() As Integer
        Get
            Return mTotalVacDaySettlement
        End Get
        Set(ByVal Value As Integer)
            mTotalVacDaySettlement = Value
        End Set
    End Property
    Public Property RemainVacDaySettlement() As Double
        Get
            Return mRemainVacDaySettlement
        End Get
        Set(ByVal Value As Double)
            mRemainVacDaySettlement = Value
        End Set
    End Property
    Public Property LastPaidDate() As Object
        Get
            Return mLastPaidDate
        End Get
        Set(ByVal value As Object)
            mLastPaidDate = value
        End Set
    End Property
    Public Property RemainVacSettlement() As Double

        Get
            Return mRemainVacSettlement
        End Get
        Set(ByVal value As Double)
            mRemainVacSettlement = value
        End Set
    End Property

#End Region

#Region "Public Function"

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = CONFIG_DATEFORMAT & mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Try
            Dim strSQL As String
            Dim Value As Integer
            strSQL = "Select ID From sys_Cities Where " & Filter
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Save() As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, OperationType.Save)
            mSqlCommand.Connection.Open()
            Return mSqlCommand.ExecuteScalar()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            mSqlCommand.Connection.Close()
        End Try
    End Function

    Public Function Update(ByVal Filter As String) As Integer
        Dim StrUpdateCommand As String
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, OperationType.Update)
            mSqlCommand.Connection.Open()
            Return mSqlCommand.ExecuteScalar()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function DeleteAll(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = "Delete from " & mTable & IIf(Len(Filter) > 0, " Where " & Filter, "")
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Clear() As Boolean
        Try
            mID = 0
            mEmployeeID = 0
            mFinancialWorkingUnits = 0
            mFiscalYearPeriodID = 0
            mPostDate = Nothing
            mPaidDate = Nothing
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mPrepareType = String.Empty
            mApplyed = False

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
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
        Dim StrSelectCommand As String = String.Empty
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
        Dim StrSelectCommand As String = String.Empty
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
        Dim StrSelectCommand As String = String.Empty
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
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mFinancialWorkingUnits = mDataHandler.DataValue_Out(.Item("FinancialWorkingUnits"), SqlDbType.Real)
                mFiscalYearPeriodID = mDataHandler.DataValue_Out(.Item("FiscalYearPeriodID"), SqlDbType.Int, True)
                mPostDate = mDataHandler.DataValue_Out(.Item("PostDate"), SqlDbType.DateTime)
                mPaidDate = mDataHandler.DataValue_Out(.Item("PaidDate"), SqlDbType.DateTime)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mPrepareType = mDataHandler.DataValue_Out(.Item("PrepareType"), SqlDbType.VarChar)
                mApplyed = mDataHandler.DataValue_Out(.Item("Applyed"), SqlDbType.Bit)

                mCBranchID = mDataHandler.DataValue_Out(.Item("CBranchID"), SqlDbType.Int, True)
                mCDepartmetnID = mDataHandler.DataValue_Out(.Item("CDepartmetnID"), SqlDbType.Int, True)
                mCSectorID = mDataHandler.DataValue_Out(.Item("CSectorID"), SqlDbType.Int, True)
                mCCost1 = mDataHandler.DataValue_Out(.Item("CCost1"), SqlDbType.Int, True)
                mCCost2 = mDataHandler.DataValue_Out(.Item("CCost2"), SqlDbType.Int, True)
                mCCost3 = mDataHandler.DataValue_Out(.Item("CCost3"), SqlDbType.Int, True)
                mCCost4 = mDataHandler.DataValue_Out(.Item("CCost4"), SqlDbType.Int, True)
                mCMainProjectID = mDataHandler.DataValue_Out(.Item("CMainProjectID"), SqlDbType.Int, True)
                mEmployeesVacationsID = mDataHandler.DataValue_Out(.Item("EmployeesVacationsID"), SqlDbType.Int, True)
                mTotalVacDaySettlement = mDataHandler.DataValue_Out(.Item("TotalVacDaySettlement"), SqlDbType.Int)
                mRemainVacDaySettlement = mDataHandler.DataValue_Out(.Item("RemainVacDaySettlement"), SqlDbType.Float)
                mLastPaidDate = mDataHandler.DataValue_Out(.Item("LastPaidDate"), SqlDbType.DateTime)
                mRemainVacSettlement = mDataHandler.DataValue_Out(.Item("RemainVacSettlement"), SqlDbType.Float
                                                                  )
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FinancialWorkingUnits", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mFinancialWorkingUnits, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FiscalYearPeriodID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFiscalYearPeriodID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PrepareType", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPrepareType, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PostDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mPostDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PaidDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mPaidDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Applyed", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mApplyed, SqlDbType.Bit)
            Select Case ptrOperationType
                Case OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CBranchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCBranchID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CDepartmetnID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCDepartmetnID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CSectorID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCSectorID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CCost1", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCCost1, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CCost2", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCCost2, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CCost3", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCCost3, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CCost4", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCCost4, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CMainProjectID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCMainProjectID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeesVacationsID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeesVacationsID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TotalVacDaySettlement", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTotalVacDaySettlement, SqlDbType.Int)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RemainVacDaySettlement", SqlDbType.Decimal)).Value = mDataHandler.DataValue_In(mRemainVacDaySettlement, SqlDbType.Decimal)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LastPaidDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mLastPaidDate, SqlDbType.DateTime)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RemainVacSettlement", SqlDbType.Decimal)).Value = mDataHandler.DataValue_In(mRemainVacSettlement, SqlDbType.Decimal)

            End Select
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region

#Region "Class Destructors"

    Protected Overloads Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class

Public Class Clshrs_EmployeesTransactions
    Inherits Clshrs_EmployeesTransactionsBase

    Public Sub New(ByVal Page As Web.UI.Page)

        MyBase.New(Page, "hrs_EmployeesTransactions")
    End Sub
    Public Function GetEmployeeTransactions(ByVal ContractID As Integer) As DataSet
        '-------------------------------0257 MODIFIED-----------------------------------------
        'Dim cn As New SqlClient.SqlConnection("data source=.;integrated security=true;initial catalog=Venus")
        Dim cn As New SqlClient.SqlConnection(mConnectionString)
        '-------------------------------=============-----------------------------------------
        Dim ObjSqlDataAdapter As New SqlClient.SqlDataAdapter("hrs_GetEmployeesTransactions", cn)
        mDataSet = New DataSet
        ObjSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        ObjSqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ContractID", ContractID)
        ObjSqlDataAdapter.Fill(mDataSet)
        Return mDataSet
    End Function
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            Dim Objparameter(1) As SqlClient.SqlParameter
            Objparameter(0) = New SqlClient.SqlParameter("@EmployeeID", Filter)
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.StoredProcedure, "hrs_GetWorkingPeriod", Objparameter)
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

    '!=!=========================================================================
    'Procedure Name  : DeleteTransactions
    'Developer       :   
    'Date Created    :  04-09-2007
    '
    '!=!=========================================================================
    Public Function DeleteTransactions(ByVal filter As String, Optional ByVal IntemployeeTrannsactionId As Integer = 0)
        Dim clsemplTransDet As New Clshrs_EmployeesTransactionsDetails(mPage)
        Try
            clsemplTransDet.Find(" EmployeeTransactionId = " & IntemployeeTrannsactionId)
            If clsemplTransDet.ID = 0 Then
                Dim StrDeleteCommand As String = " Delete " & mTable & IIf(Len(filter) > 0, " Where " & filter, "")
                mSqlCommand = New SqlClient.SqlCommand
                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                mSqlCommand.CommandType = CommandType.Text
                mSqlCommand.CommandText = StrDeleteCommand
                mSqlCommand.Connection.Open()
                mSqlCommand.ExecuteNonQuery()
                mSqlCommand.Connection.Close()
            End If
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '!=!=========================================================================
    'Procedure Name  : GetAnnualVacationsTransactions
    'Developer       :   
    'Date Created    :  05-09-2007
    'Description     : This Method Used to Saves No Annual Vacations Transactions Of Given Employee 
    '!=!=========================================================================
    Public Function GetAnnualVacationsTransactions(ByVal IntEmployeeID As Integer, ByVal IntContractId As Integer, ByVal IntFisicalYearPeriod As Integer, ByVal IntEmployeeTransactioID As Integer, ByVal TransactionTypesDS As DataSet, ByVal clsSolver As Clshrs_FormulaSolver)
        Dim FormulaExpression As String
        Dim ClsContractTransactions As New Clshrs_ContractsTransactions(mPage)
        Dim clsGradeSteps As New Clshrs_GradesStepsTransactions(mPage)
        Dim clsEmployeeTransactionsDet As New Clshrs_EmployeesTransactionsDetails(mPage)
        Dim clsfiscalPeriod As New Clssys_FiscalYearsPeriods(mPage)

        'Find("EmployeeID=" & IntEmployeeID & " And FiscalYearPeriodID = " & IntFisicalYearPeriod)
        'TransactionId = ID
        'Dim ClsContract As New Clshrs_Contracts(mPage)
        'Dim IntContractid As Integer = ClsContract.ContractValidatoinId(IntEmployeeID, IntFisicalYearPeriod)
        'Dim nMonthlyTrans As Single
        If ClsContractTransactions.Find("ContractID= " & IntContractId & " And Active = 1 " & " And PaidAtVacation =1 ") Then
            For Each row As Data.DataRow In ClsContractTransactions.DataSet.Tables(0).Rows

                'Check Once At Period  
                '===================== [Begin]
                If CBool(row.Item("OnceAtPeriod")) = True Then
                    'Get No Of Times This Transaction Employee Earned 
                    If GetPaidTransactionsNosPerPeriod(IntEmployeeID, CInt(row.Item(2)), IntFisicalYearPeriod) >= 1 Then
                        Continue For
                    End If
                End If

                '===================== [End]

                clsEmployeeTransactionsDet.TransactionTypeID = row.Item(2)
                clsEmployeeTransactionsDet.EmployeeTransactionID = IntEmployeeTransactioID
                'Check If Transaction Type being Caculated Has Formula or Not And Calculate It  
                For Each rowTransType As Data.DataRow In TransactionTypesDS.Tables(0).Rows
                    If rowTransType.Item(0) = row.Item(2) Then
                        If rowTransType.Item("Formula") Is DBNull.Value Then
                            clsEmployeeTransactionsDet.NumericValue = row.Item(3)
                        Else
                            FormulaExpression = rowTransType.Item("Formula")
                            clsSolver.EmployeeID = IntEmployeeID
                            clsSolver.FiscalPeriodID = IntFisicalYearPeriod
                            clsSolver.EvaluateExpression(FormulaExpression)
                            clsEmployeeTransactionsDet.NumericValue = clsSolver.Output
                        End If
                        clsEmployeeTransactionsDet.TextValue = ""
                        clsEmployeeTransactionsDet.Save()
                        Exit For
                    End If
                Next

            Next
        Else

            Dim clsContracts As New Clshrs_Contracts(mPage)
            clsContracts.Find(" ID =" & IntContractId)
            clsGradeSteps.Find(" GradeStepId = " & clsContracts.GradeStepId)

            For Each row As Data.DataRow In clsGradeSteps.DataSet.Tables(0).Rows

                'Check Once At Period  
                '===================== [Begin]
                If CBool(row.Item("OnceAtPeriod")) = True Then
                    'Get No Of Times This Transaction Employee Earned 
                    If GetPaidTransactionsNosPerPeriod(IntEmployeeID, CInt(row.Item("TransactionTypeID")), IntFisicalYearPeriod) >= 1 Then
                        Continue For
                    End If
                End If

                '===================== [End]

                clsEmployeeTransactionsDet.TransactionTypeID = row.Item("TransactionTypeID")
                clsEmployeeTransactionsDet.EmployeeTransactionID = ID

                'Check If Transaction Type being Caculated Has Formula or Not And Calculate It  
                For Each rowTransType As Data.DataRow In TransactionTypesDS.Tables(0).Rows
                    If rowTransType.Item(0) = row.Item("TransactionTypeID") Then
                        If rowTransType.Item("Formula") Is DBNull.Value Then
                            clsEmployeeTransactionsDet.NumericValue = row.Item("Amount")
                        Else
                            FormulaExpression = rowTransType.Item("Formula")
                            clsSolver.EmployeeID = IntEmployeeID
                            clsSolver.FiscalPeriodID = IntFisicalYearPeriod
                            clsSolver.EvaluateExpression(FormulaExpression)
                            clsEmployeeTransactionsDet.NumericValue = clsSolver.Output
                        End If
                        clsEmployeeTransactionsDet.TextValue = ""
                        clsEmployeeTransactionsDet.Save()
                        Exit For
                    End If
                Next
            Next
        End If
    End Function
    Public Function GetPreviousAnnualVacations(ByVal IntEmployeeId As Integer, ByVal IntFiscalYearId As Integer) As Single
        Dim snglVcationsDays As Single
        Dim strSelectCommand As String = " Select Sum(FinancialWorkingUnits) From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id and Sys_FiscalYearsPeriods.FiscalYearId = " & IntFiscalYearId & " And EmployeeId =" & IntEmployeeId & " And PrepareType = 'V' "
        'Dim strSelectCommand As String = " Select Top 1 financialworkingunits From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where EmployeeId =" & IntEmployeeId & " And PrepareType = 'V' order by todate desc"
        snglVcationsDays = IIf(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand) Is DBNull.Value, 0, Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand))

        Return snglVcationsDays
    End Function
    Public Function GetPreviousAnnualVacationsID(ByVal IntEmployeeId As Integer, Optional ByVal IntFiscalYearId As Integer = 0) As Single
        Dim intEmpTransID As Single
        Dim strSelectCommand As String
        If IntFiscalYearId = 0 Then
            strSelectCommand = " Select Top(1) hrs_EmployeesTransactions.ID  From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where EmployeeId =" & IntEmployeeId & " And PrepareType = 'V' and IsNull(hrs_EmployeesTransactions.CancelDate,'')='' order by todate desc"
        Else
            strSelectCommand = " Select Top(1) hrs_EmployeesTransactions.ID  From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id and Sys_FiscalYearsPeriods.FiscalYearId = " & IntFiscalYearId & " And EmployeeId =" & IntEmployeeId & " And PrepareType = 'V' and IsNull(hrs_EmployeesTransactions.CancelDate,'')='' order by PaidDate desc "
        End If

        intEmpTransID = IIf(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand) Is DBNull.Value, 0, Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand))

        Return intEmpTransID
    End Function
    Public Function GetPreviousAnnualVacationsDate(ByVal IntEmployeeId As Integer) As Date
        Dim DteVcationsDate As Date
        'Dim strSelectCommand As String = " Select Sum(FinancialWorkingUnits) From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id and Sys_FiscalYearsPeriods.FiscalYearId = " & IntFiscalYearId & " And EmployeeId =" & IntEmployeeId & " And PrepareType = 'V' "
        Dim strSelectCommand As String = " Select Top 1 ToDate From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where EmployeeId =" & IntEmployeeId & " And PrepareType = 'V' order by todate desc"
        DteVcationsDate = IIf(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand) Is DBNull.Value, #1/1/1900#, Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand))

        Return DteVcationsDate
    End Function
    Public Function GetPreviousVacationPaidDate(ByVal IntEmployeeId As Integer) As Object
        Dim DteVcationsDate As Object
        Dim strSelectCommand As String = " Select Top 1 PaidDate From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where EmployeeId =" & IntEmployeeId & " And PrepareType = 'V' order by todate desc"
        DteVcationsDate = IIf(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand) Is DBNull.Value, Nothing, Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand))
        Return DteVcationsDate
    End Function
    Public Function GetVacationOpenBalanceDate(ByVal IntEmployeeId As Integer, ByVal VType As Integer) As Object
        Dim DteVcationsDate As Object
        Dim Cls_EmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(mPage)
        If Cls_EmployeeVacationOpenBalance.Find("EmployeeID=" & IntEmployeeId & " and VacationTypeID = " & VType) Then
            DteVcationsDate = Cls_EmployeeVacationOpenBalance.GBalanceDate
        End If
        Return DteVcationsDate
    End Function
    Public Function GetVacationOpenBalanceDays(ByVal IntEmployeeId As Integer, ByVal VType As Integer) As Object
        Dim DteVcationsDays As Object
        Dim Cls_EmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(mPage)
        If Cls_EmployeeVacationOpenBalance.Find("EmployeeID=" & IntEmployeeId & " and VacationTypeID = " & VType) Then
            DteVcationsDays = Cls_EmployeeVacationOpenBalance.Days
        End If
        Return DteVcationsDays
    End Function
    Public Function GetPreviousVacationPaidDate(ByVal IntEmployeeId As Integer, ByRef FinancialWorkingUnits As Single, ByRef relatedVactionDays As Integer) As Object
        Dim DteVcationsDate As Object
        Dim ActualStartDate As DateTime

        Dim ActualEndDate As DateTime
        Dim Ds As DataSet
        Dim strSelectCommand As String = " Select Top 1 PaidDate,FinancialWorkingUnits,ev.ActualStartDate,ev.ActualEndDate From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id left join hrs_EmployeesVacations ev on ev.PaymentTrnID=Hrs_EmployeesTransactions.id Where Hrs_EmployeesTransactions.EmployeeId =" & IntEmployeeId & " And PrepareType = 'V' order by todate desc ,hrs_EmployeesTransactions.id desc"
        Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, strSelectCommand)
        If Ds.Tables(0).Rows.Count > 0 Then
            If IsDBNull(Ds.Tables(0).Rows(0)("PaidDate")) Then
                DteVcationsDate = Nothing
            Else
                DteVcationsDate = Ds.Tables(0).Rows(0)("PaidDate")
            End If
            If IsDBNull(Ds.Tables(0).Rows(0)("FinancialWorkingUnits")) Then
                FinancialWorkingUnits = 0
            Else
                FinancialWorkingUnits = Ds.Tables(0).Rows(0)("FinancialWorkingUnits")
            End If

            If Not IsDBNull(Ds.Tables(0).Rows(0)("ActualStartDate")) And Not IsDBNull(Ds.Tables(0).Rows(0)("ActualEndDate")) Then
                ActualStartDate = Ds.Tables(0).Rows(0)("ActualStartDate")
                ActualEndDate = Ds.Tables(0).Rows(0)("ActualEndDate")
                relatedVactionDays = ActualEndDate.Subtract(ActualStartDate).Days

            End If
        Else
            DteVcationsDate = Nothing
            FinancialWorkingUnits = 0
        End If

        Return DteVcationsDate
    End Function
    Public Function GetPreviousVacationData(ByVal IntEmployeeId As Integer) As Boolean
        Dim strSelectCommand As String = " SELECT TOP 1 * " _
                                       & " FROM hrs_EmployeesTransactions" _
                                       & " INNER JOIN Sys_FiscalYearsPeriods ON Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id" _
                                       & " WHERE Hrs_EmployeesTransactions.EmployeeId = " & IntEmployeeId & " And PrepareType = 'V' AND EmployeesVacationsID > 0 " _
                                       & " ORDER BY hrs_EmployeesTransactions.ID DESC,todate DESC "

        mDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, strSelectCommand)

        If mDataSet.Tables(0).Rows.Count > 0 Then
            Find("ID=" & mDataSet.Tables(0).Rows(0).Item("ID"))
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetPreviousAnnualVacationsReturnDate(ByVal IntEmployeeId As Integer) As Object
        Dim DteVcationsDate As Object
        Dim strSelectCommand As String = " Select Top 1 ActualEndDate From hrs_EmployeesVacations Inner Join hrs_VacationsTypes	On hrs_VacationsTypes.id = hrs_EmployeesVacations.VacationTypeID Where hrs_VacationsTypes.isAnnual = 1 And hrs_EmployeesVacations.Employeeid = " & IntEmployeeId & " and hrs_EmployeesVacations.CancelDate is null and PaymentTrnID is not null Order by ActualEndDate desc "
        DteVcationsDate = IIf(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand) Is DBNull.Value, Nothing, Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand))

        Return DteVcationsDate
    End Function
    Public Function GetPaidTransactionsNosPerPeriod(ByVal IntEmployeeId As Integer, ByVal IntTransactionTypeId As Integer, ByVal IntFiscalPeriodId As Integer) As Integer
        Dim IntPaidTimes As Integer = 0
        Dim strSelectCommand As String = " Select Count(*) From hrs_EmployeesTransactions  INNER JOIN hrs_EmployeesTransactionsProjects On EmployeeTransactionID = hrs_EmployeesTransactions.ID INNER JOIN  hrs_EmployeesTransactionsDetails ON EmpTransProjID = hrs_EmployeesTransactionsProjects.ID Where EmployeeId = " & IntEmployeeId & " And FiscalYearPeriodID = " & IntFiscalPeriodId & " And TransactionTypeID =  " & IntTransactionTypeId & ""
        IntPaidTimes = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand)
        Return IntPaidTimes
    End Function
    Public Function CheckVacationsPrepared(ByVal IntEmployeeId As Integer, ByVal IntFiscalYearId As Integer) As Single
        Dim IntPreparedTimes As Single
        Dim strSelectCommand As String = " Select Count(*) From hrs_EmployeesTransactions Inner Join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where Sys_FiscalYearsPeriods.FiscalYearId = " & IntFiscalYearId & " And EmployeeId = " & IntEmployeeId & " And PrepareType = 'V' "
        IntPreparedTimes = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, strSelectCommand)
        Return IntPreparedTimes
    End Function
    '===============================
    'Added By     :  [0256]  
    'Date Created : 16-04-2008
    '===============================
    Public Function DeleteTransactionsDetails(ByVal IntEmployeeTrnascationId As Integer) As Boolean
        Dim StrSelectCommand As String = " Delete hrs_employeesTransactionsDetails Where employeetransactionid = " & IntEmployeeTrnascationId
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, CommandType.Text, StrSelectCommand)
    End Function
    '===============================
    'Added By     :  [0256]  
    'Date Created : 16-04-2008
    '===============================
    Public Function DeleteTransactionsProjects(ByVal IntEmployeeTrnascationId As Integer) As Boolean
        Dim StrSelectCommand As String = " Delete hrs_EmployeesTransactionsProjects Where EmployeeTransactionID = " & IntEmployeeTrnascationId
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, CommandType.Text, StrSelectCommand)
    End Function
    '===============================
    'Added By     :  [0256]  
    'Date Created : 13-04-2008
    '===============================
    Public Function GetAllPaidEmployees(ByVal IntFiscalPeriodId As Integer, ByVal dteFiscalPeriodStart As Date, ByVal dteFiscalPeriodEnd As Date, ByRef dsDataSource As DataSet) As Boolean
        Try
            Dim parmArray() As Object = {IntFiscalPeriodId, dteFiscalPeriodStart, dteFiscalPeriodEnd}
            dsDataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetPaidEmployeesList", parmArray)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '===============================
    'Added By     :  [0256]  
    'Date Created : 13-04-2008
    '===============================
    Public Function GetFinancialWorkingUnits(ByVal IntEmployeeId As Integer, ByVal IntFiscalPeriodID As Integer) As Single
        Dim StrSelectCommand As String = "Select IsNull(FinancialWorkingUnits,0) From hrs_EmployeesTransactions Where EmployeeID = " & _
                                          IntEmployeeId & " And FiscalYearPeriodID= " & IntFiscalPeriodID & " And PrepareType='N' "
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, StrSelectCommand)
    End Function
    '===============================
    'Added By     :  [0256]  
    'Date Created : 29-04-2008
    '===============================
    Public Function GetOverTimeWorkingUnits(ByVal IntEmployeeId As Integer, ByVal IntFiscalPeriodID As Integer) As Single
        Dim StrSelectCommand As String = " Select Isnull(Sum(OvertimeHours),0) From hrs_EmployeesTransactionsProjects Inner Join hrs_EmployeesTransactions  " & _
                                         " On hrs_EmployeesTransactionsProjects.EmployeeTransactionID = hrs_EmployeesTransactions.ID  Where EmployeeID = " & _
                                           IntEmployeeId & " And FiscalYearPeriodID= " & IntFiscalPeriodID & " And PrepareType='N' "
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, StrSelectCommand)
    End Function
    Public Function GetAllEmployeePreparedByFiscalPeriod(ByVal intFiscalPeriodID As Integer, ByVal intModuleID As Integer, ByVal strArrangeName As String, ByRef dsEmployees As DataSet) As Boolean
        Dim strCommand As String
        strCommand = " select et.ID,et.EmployeeID,e.Code as EmployeeCode," & strArrangeName & " as EmployeeName,et.Applyed from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID where  m.ModuleID=" & intModuleID & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & intFiscalPeriodID & " And et.PrepareType ='N' "
        Try
            dsEmployees = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, strCommand)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetAllDepartmentProjectTransactions(ByVal intFiscalPeriodID As Integer, ByVal intProjectID As Integer, ByVal strLang As String) As DataSet
        Dim strCommand As String
        Dim dsResult As DataSet
        Try
            strCommand = "GetAllProjectEmployeesTransactions"
            dsResult = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, strCommand, intFiscalPeriodID, intProjectID, strLang)
            Return dsResult
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetAllDepartmentEmployeesTransactions(ByVal intEmpTrans As Integer, ByVal strLang As String) As DataSet
        Dim strCommand As String
        Dim dsResult As DataSet
        Try
            strCommand = "GetAllDepartmentEmployeesTransactions"
            dsResult = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, strCommand, intEmpTrans, strLang)
            Return dsResult
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function



End Class