Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesTransactionsDetails
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        Me.Table = " hrs_EmployeesTransactionsDetails "
        Me.mInsertParameter = " EmpTransProjID,TransactionTypeID,NumericValue,TextValue,RegUserID,RegComputerID "
        Me.mInsertParameterValues = " @EmpTransProjID,@TransactionTypeID,@NumericValue,@TextValue,@RegUserID,@RegComputerID "
        Me.mUpdateParameter = " EmpTransProjID=@EmpTransProjID,TransactionTypeID=@TransactionTypeID,NumericValue=@NumericValue,TextValue=@TextValue "
        Me.mSelectCommand = " Select * From  " & mTable
        Me.mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        Me.mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        Me.mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"

    End Sub
#End Region

#Region "Private Members"

    Private mID As Object
    Private mEmpTransProjID As Integer
    Private mTransactionTypeID As Integer
    Private mNumericValue As Object
    Private mTextValue As Object
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Public EmployeeTransactionID As Integer
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

    Public Property EmpTransProjID() As Integer
        Get
            Return mEmpTransProjID
        End Get
        Set(ByVal Value As Integer)
            mEmpTransProjID = Value
        End Set
    End Property

    Public Property TransactionTypeID() As Integer
        Get
            Return mTransactionTypeID
        End Get
        Set(ByVal Value As Integer)
            mTransactionTypeID = Value
        End Set
    End Property

    Public Property NumericValue() As Double
        Get
            Return mNumericValue
        End Get
        Set(ByVal Value As Double)
            mNumericValue = Value
        End Set
    End Property

    Public Property TextValue() As Object
        Get
            Return mTextValue
        End Get
        Set(ByVal Value As Object)

            mTextValue = Value
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

    '==================================================================
    'Created by : [0258]
    'Date : 15/8/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from hrs_EmployeesTransactionsDetails table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================

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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 15/8/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from hrs_EmployeesTransactionsDetails where filter 
    '       2-Check if ID > 0 this mean that row is already exist in hrs_EmployeesTransactionsDetails  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in hrs_EmployeesTransactionsDetails  table
    '==================================================================

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

    '==================================================================
    'Created by : [0258]
    'Date : 15/8/2007

    'Description: Save New Row in hrs_EmployeesTransactionsDetails  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in hrs_EmployeesTransactionsDetails  table

    '==================================================================

    Public Function Save() As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, OperationType.Save)
            mSqlCommand.Connection.Open()
            Return mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveTrans() As Integer
        Try
            Dim recordId As Integer
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand & " ; select @@identity "
            SetParameter(mSqlCommand, OperationType.Save)
            mSqlCommand.Connection.Open()
            recordId = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            Return recordId
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 15/8/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in hrs_EmployeesTransactionsDetails  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in hrs_EmployeesTransactionsDetails  table

    '==================================================================

    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, OperationType.Update)
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

    '==================================================================
    'Created by : [0258]
    'Date : 15/8/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in hrs_EmployeesTransactionsDetails  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in hrs_EmployeesTransactionsDetails  table

    '==================================================================

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
            StrDeleteCommand = "Delete from " & mTable & " Where ID In ( Select hrs_EmployeesTransactionsDetails.ID From hrs_EmployeesTransactionsDetails Inner join hrs_EmployeesTransactionsProjects On  EmpTransProjID = hrs_EmployeesTransactionsProjects.ID Where " & Filter & ")"
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

    '==================================================================
    'Created by : [0258]
    'Date : 15/8/2007
    'Description: Clear all private members  of the class

    '==================================================================

    Public Function Clear() As Boolean
        Try
            mID = 0
            mEmpTransProjID = 0
            mTransactionTypeID = 0
            mNumericValue = 0
            mTextValue = String.Empty
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

    '==================================================================
    'Created by : [0258]
    'Date : 15/8/2007
    'Description:  find first row in hrs_EmployeesTransactionsDetails table
    'Steps: 
    '       1-execute sqlstatment to find first row in hrs_EmployeesTransactionsDetails  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

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

    '==================================================================
    'Created by : [0258]
    'Date : 15/8/2007
    'Description:  find Last row in hrs_EmployeesTransactionsDetails  table
    'Steps: 
    '       1-execute sqlstatment to find last row in hrs_EmployeesTransactionsDetails  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

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

    '==================================================================
    'Created by : [0258]
    'Date : 15/8/2007
    'Description:  find Next row in hrs_EmployeesTransactionsDetails  table
    'Steps: 
    '       1-execute sqlstatment to find Next row in hrs_EmployeesTransactionsDetails  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

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

    '==================================================================
    'Created by : [0258]
    'Date : 15/8/2007
    'Description:  find previous row in hrs_EmployeesTransactionsDetails  table
    'Steps: 
    '       1-execute sqlstatment to find previous row in hrs_EmployeesTransactionsDetails  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================

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


    '==================================================================
    'Created by : DataOcean
    'Date : 15/8/2007
    'Input : Filter as string (ex. ID=2)
    'Clshrs_EmployeesTransactionsDetails	
    'Description: Find all columns from hrs_EmployeesTransactionsDetails table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================

    Public Function FindNoOfPeriodsforLastOccurrences(ByVal EmployeeId As Integer, ByVal TransactionTypeID As Integer, ByVal FiscalYearPeriodID As Integer) As Integer
        Dim IntNoOfUnits As Integer
        Try
            IntNoOfUnits = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Me.ConnectionString, "hrs_GetLastTransactionOccurrences", EmployeeId, TransactionTypeID, FiscalYearPeriodID)
            Return IntNoOfUnits
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FindRelatedToProjects(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = " Select " & _
            " hrs_EmployeesTransactionsDetails.TransactionTypeID,Sum(hrs_EmployeesTransactionsDetails.NumericValue) NumericValue,max(hrs_EmployeesTransactionsDetails.TextValue) TextValue ,ISNULL(hrs_EmployeesTransactionsDetails.EmployeePayabilityScheduleID,0) as EmployeePayabilityScheduleID, (select isnull((select DueAmount from hrs_EmployeesPayabilitiesSchedules where ID = hrs_EmployeesTransactionsDetails.EmployeePayabilityScheduleID),0) - isnull(sum(Amount),0) from hrs_EmployeesPayabilitiesSchedulesSettlement where EmployeePayabilityScheduleID = hrs_EmployeesTransactionsDetails.EmployeePayabilityScheduleID and EmployeeTransactionID <> max(hrs_EmployeesTransactionsProjects.ID)) AS OrgAmount,'' AS DescriptionSign " & _
            " From " & _
            " hrs_EmployeesTransactionsDetails  " & _
            " Inner Join hrs_EmployeesTransactionsProjects On EmpTransProjID = hrs_EmployeesTransactionsProjects.ID " & _
            " Where " & _
            IIf(Len(Filter) > 0, " IsNull(hrs_EmployeesTransactionsDetails.CancelDate,'')=''  And " & Filter, "  Where IsNull(hrs_EmployeesTransactionsDetails.CancelDate,'')='' ") & _
            " Group By " & _
            " hrs_EmployeesTransactionsDetails.TransactionTypeID ,hrs_EmployeesTransactionsDetails.EmployeePayabilityScheduleID"
            '-------------------------------------------------------

            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataSet.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FindRelatedToProjectsD(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = " Select " & _
            " hrs_EmployeesTransactionsDetails.TransactionTypeID,(hrs_EmployeesTransactionsDetails.NumericValue) NumericValue,(hrs_EmployeesTransactionsDetails.TextValue) TextValue " & _
            " From " & _
            " hrs_EmployeesTransactionsDetails  " & _
            " Inner Join hrs_EmployeesTransactionsProjects On EmpTransProjID = hrs_EmployeesTransactionsProjects.ID " & _
            " Where " & IIf(Len(Filter) > 0, " IsNull(hrs_EmployeesTransactionsDetails.CancelDate,'')=''  And " & Filter, "  Where IsNull(hrs_EmployeesTransactionsDetails.CancelDate,'')='' ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataSet.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region

#Region "Class Private Function"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mEmpTransProjID = mDataHandler.DataValue_Out(.Item("EmpTransProjID"), SqlDbType.Int, True)
                mTransactionTypeID = mDataHandler.DataValue_Out(.Item("TransactionTypeID"), SqlDbType.Int, True)
                mNumericValue = mDataHandler.DataValue_Out(.Item("NumericValue"), SqlDbType.Money)
                mTextValue = mDataHandler.DataValue_Out(.Item("TextValue"), SqlDbType.VarChar)
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

    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal ptrOperationType As OperationType) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmpTransProjID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmpTransProjID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransactionTypeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTransactionTypeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NumericValue", SqlDbType.Money)).Value = mDataHandler.DataValue_In(mNumericValue, SqlDbType.Money)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TextValue", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTextValue, SqlDbType.VarChar)
            Select Case ptrOperationType
                Case OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End Select
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
