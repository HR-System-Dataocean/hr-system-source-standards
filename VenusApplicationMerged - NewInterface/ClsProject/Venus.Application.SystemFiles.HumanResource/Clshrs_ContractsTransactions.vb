'                  : B#g0010 [0256] 23-06-2008 Add _Forgin arrgument to data_ValueIn method in SetParameters Function 
'                  :                           And _Forgin arrgument to data_value_Out Method in GetParameters Function 
'                  :                           To avoid saving non-existing forign key in any table 
'                  :                           And convert the DBnull values to zero in case of forign key fields only 
'=========================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_ContractsTransactionsBase
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    '==================================================================
    'Created by : [0258]
    'Date : 19/07/2007
    'Input : 
    'Description: In the constructor of the class set the table name and 
    '           sqlstatment of (Insert,Update,Delete,select) row from the table

    '==================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_ContractsTransactions "
        mInsertParameter = " ContractID,TransactionTypeID,Amount,Active,IntervalID,PaidAtVacation,OnceAtPeriod,Remarks,RegUserID,RegComputerID,ActiveDate,ActiveDate_D"
        mInsertParameterValues = " @ContractID,@TransactionTypeID,@Amount,@Active,@IntervalID,@PaidAtVacation,@OnceAtPeriod,@Remarks,@RegUserID,@RegComputerID,@ActiveDate,@ActiveDate_D "
        mUpdateParameter = " ContractID=@ContractID,TransactionTypeID=@TransactionTypeID,Amount=@Amount,Active=@Active,IntervalID=@IntervalID,PaidAtVacation=@PaidAtVacation,OnceAtPeriod=@OnceAtPeriod,Remarks=@Remarks,CancelDate=@CancelDate,ActiveDate=@ActiveDate,ActiveDate_D=@ActiveDate_D "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region

#Region "Private Members"
    Protected mID As Object
    Private mTransactionTypeID As Object
    Private mContractID As Object
    Private mAmount As Object
    Private mActive As Boolean
    Private mIntervalID As Object
    Private mPaidAtVacation As Integer
    Private mOnceAtPeriod As Boolean
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mActiveDate As Date
    Private mActiveDate_D As String
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
    Public Property TransactionTypeID() As Object
        Get
            Return mTransactionTypeID
        End Get
        Set(ByVal Value As Object)
            mTransactionTypeID = Value
        End Set
    End Property
    Public Property ContractID() As Object
        Get
            Return mContractID
        End Get
        Set(ByVal Value As Object)
            mContractID = Value
        End Set
    End Property

    Public Property Amount() As Object
        Get
            Return mAmount
        End Get
        Set(ByVal Value As Object)
            mAmount = Value
        End Set
    End Property


    Public Property Active() As Boolean
        Get
            Return mActive
        End Get
        Set(ByVal Value As Boolean)
            mActive = Value
        End Set
    End Property

    Public Property IntervalID() As Object
        Get
            Return mIntervalID
        End Get
        Set(ByVal value As Object)
            mIntervalID = value
        End Set
    End Property
    Public Property PaidAtVacation() As Integer
        Get
            Return mPaidAtVacation
        End Get
        Set(ByVal value As Integer)
            mPaidAtVacation = value
        End Set
    End Property
    Public Property OnceAtPeriod() As Boolean
        Get
            Return mOnceAtPeriod
        End Get
        Set(ByVal value As Boolean)
            mOnceAtPeriod = value
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
        Set(ByVal value As Object)
            mCancelDate = value
        End Set
    End Property
    Public Property ActiveDate() As Date
        Get
            Return mActiveDate
        End Get
        Set(ByVal value As Date)
            mActiveDate = value
        End Set
    End Property

    Public Property ActiveDate_D() As String
        Get
            Return mActiveDate_D
        End Get
        Set(ByVal value As String)
            mActiveDate_D = value
        End Set
    End Property
#End Region

#Region "Public Function"
    '==================================================================
    'Created by : [0258]
    'Date : 19/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from hrs_ContractTransaction table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    Public Function Find(ByVal intContractID As Integer, ByVal CToDate As Date, ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "set dateformat dmy; Select * From dbo.hrs_GetContractTransactions(" & intContractID & ",'" & CToDate & "') " & IIf(Len(Filter) > 0, " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    Public Function FindWithOrder(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = " Select hrs_ContractsTransactions.*,hrs_TransactionsTypes.* from hrs_ContractsTransactions INNER JOIN hrs_TransactionsTypes ON hrs_ContractsTransactions.TransactionTypeID = hrs_TransactionsTypes.ID  " & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(hrs_ContractsTransactions.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By hrs_TransactionsTypes.Code", "  Where  IsNull(dbo.hrs_GetRecordViewStatus(hrs_ContractsTransactions.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  OrderBy hrs_TransactionsTypes.Code")
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
    '-------------------------------0257 MODIFIED-----------------------------------------

    Public Function Find(ByVal Filter As String, ByVal NonCanceledOnly As Boolean) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            If NonCanceledOnly Then
                StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            Else
                StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            End If
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
    '-------------------------------=============-----------------------------------------

    '==================================================================
    'Created by : [0258]
    'Date : 19/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from hrs_ContractTransaction where filter 
    '       2-Check if ID > 0 this mean that row is already exist in hrs_ContractTransaction  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in hrs_ContractTransaction  table
    '==================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_ContractsTransactions Where " & Filter
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
    '==================================================================
    'Created by : [0258]
    'Date : 19/07/2007

    'Description: Save New Row in hrs_ContractTransaction  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in hrs_ContractTransaction  table

    '==================================================================
    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, OperationType.Save)
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
    '==================================================================
    'Created by : [0258]
    'Date : 19/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in hrs_ContractTransaction  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in hrs_ContractTransaction  table

    '==================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String = String.Empty
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, OperationType.Update)
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")

            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 19/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in hrs_ContractTransaction  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in hrs_ContractTransaction  table

    '==================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            'SetParameter(mSqlCommand, OperationType.Update)
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
    '==================================================================
    'Created by : [0258]
    'Date : 19/07/2007
    'Description: Clear all private members  of the class

    '==================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mTransactionTypeID = 0
            mContractID = 0
            mAmount = 0.0
            mActive = False
            mIntervalID = 0
            mOnceAtPeriod = False
            mPaidAtVacation = 0
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mActiveDate = Nothing
            ActiveDate_D = Nothing
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 19/07/2007
    'Description:  find first row in hrs_ContractTransaction table Contract of Select Employee
    'Steps: 
    '       1-execute sqlstatment to find first row in hrs_ContractTransaction  table for Selected Contract
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    'Date : 19/07/2007
    'Description:  find Last row in hrs_ContractTransaction  table for Selected Contract
    'Steps: 
    '       1-execute sqlstatment to find last row in hrs_ContractTransaction  table for Selected Contract
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    'Date : 19/07/2007
    'Description:  find Next row in hrs_ContractTransaction  table for Selected Contract
    'Steps: 
    '       1-execute sqlstatment to find Next row in hrs_ContractTransaction  table for Selected Contract
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
    'Date : 19/07/2007
    'Description:  find previous row in hrs_ContractTransaction  table for  for Selected Contract
    'Steps: 
    '       1-execute sqlstatment to find previous row in hrs_ContractTransaction  table for Selected Contract
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And " & Filter & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
    'Created by : [0259]
    'Date : 26/08/2007
    'Description: Getting if there are any difference between DataSet and Class's Properties
    '==================================================================

    Public Function CheckDiff(ByVal ClassObj As Object, ByVal DSData As DataSet, ByVal Filter As String) As Boolean

        Dim myPropertyInfo As Reflection.PropertyInfo() = CType(ClassObj.GetType, Type).GetProperties()
        Dim PropertyCounter As Integer
        Dim DSCounter As Integer
        Dim Index() As Object

        For PropertyCounter = 1 To myPropertyInfo.Length - 1
            For DSCounter = 1 To DSData.Tables(0).Columns.Count - 1

                With DSData.Tables(0).Columns(DSCounter)
                    Dim myPropInfo As Reflection.PropertyInfo = CType(myPropertyInfo(PropertyCounter), Reflection.PropertyInfo)

                    'Check column name (ex: CODE=CODE)
                    If myPropInfo.Name.ToString.ToUpper = .ColumnName.ToUpper Then

                        'Check column value (ex: 002=002)
                        If Not myPropInfo.GetValue(ClassObj, Index) = FixNull(DSData.Tables(0).Rows(0)(DSCounter), DSData.Tables(0).Columns(DSCounter)) Then
                            Return True

                            Exit For
                        End If

                        Exit For
                    End If

                End With

            Next
        Next

    End Function


#End Region

#Region "Class Private Function"
    '===================================================================
    'Created by : [0258]
    'Date : 19/07/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class

    '===================================================================
    Protected Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mContractID = mDataHandler.DataValue_Out(.Item("ContractID"), SqlDbType.Int, True)
                mTransactionTypeID = mDataHandler.DataValue_Out(.Item("TransactionTypeID"), SqlDbType.Int, True)
                mAmount = mDataHandler.DataValue_Out(.Item("Amount"), SqlDbType.Real)
                mActive = mDataHandler.DataValue_Out(.Item("Active"), SqlDbType.Bit)
                mIntervalID = mDataHandler.DataValue_Out(.Item("IntervalID"), SqlDbType.Int, True)
                mPaidAtVacation = mDataHandler.DataValue_Out(.Item("PaidAtVacation"), SqlDbType.Int)
                mOnceAtPeriod = mDataHandler.DataValue_Out(.Item("OnceAtPeriod"), SqlDbType.Bit)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mActiveDate = mDataHandler.DataValue_Out(.Item("ActiveDate"), SqlDbType.DateTime)
                mActiveDate_D = mDataHandler.DataValue_Out(.Item("ActiveDate_D"), SqlDbType.VarChar)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '===================================================================
    'Created by : [0258]
    'Date : 19/07/2007
    'Description:   Make the values of parameter equal values of private member  of the class

    '===================================================================
    Protected Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal operationtype As OperationType) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ContractID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mContractID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransactionTypeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTransactionTypeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Amount", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mAmount, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Active", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mActive, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IntervalID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mIntervalID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PaidAtVacation", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPaidAtVacation, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OnceAtPeriod", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mOnceAtPeriod, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ActiveDate", SqlDbType.SmallDateTime)).Value = mDataHandler.DataValue_In(mActiveDate, SqlDbType.SmallDateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ActiveDate_D", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mActiveDate_D, SqlDbType.VarChar)

            If operationtype = ClsDataAcessLayer.OperationType.Update Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.SmallDateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.SmallDateTime)
            End If
            Select Case operationtype
                Case ClsDataAcessLayer.OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End Select

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function


    Private Function FixNull(ByVal obj As Object, ByVal DataColumn As Global.System.Data.DataColumn) As Object
        Try

            Select Case DataColumn.DataType.Name.ToUpper
                Case "DECIMAL", "DOUBLE"
                    If obj Is DBNull.Value Then
                        Return 0.0
                    Else
                        Return obj
                    End If
                Case "INT32"
                    If obj Is DBNull.Value Then
                        Return 0
                    Else
                        Return obj
                    End If
                Case "DATETIME"
                    If obj Is DBNull.Value Then
                        Return Nothing
                    Else
                        Return obj
                    End If
                Case "STRING"
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
                Case Else
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
            End Select

        Catch ex As Exception
            Return ""
        End Try
    End Function
#End Region

#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region

End Class
Public Class Clshrs_ContractsTransactions
    Inherits Clshrs_ContractsTransactionsBase
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)

    End Sub
    '==================================================================
    'Created by : [0258]
    'Date : 09/07/2007
    'Input : DdlValues as DropDownList
    '       Condition to fill dropdownlist (if Wanted)
    '       NullNode to make attention to user by make first item ="Please Select"    
    'Description:  Fill DropDownList with Countries and Items
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language 
    '==================================================================
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
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

    '==================================================================
    'Created by : Dataocean
    'Clshrs_ContractsTransactions	
    'Date : 26/08/2007
    'Description: Check if the current transaction is valid for this particular period or not 
    '==================================================================
    Public Function ValidTransaction(ByVal EmployeeID As Integer, ByVal TransactionID As Integer, ByVal ToDate As Date, ByVal FiscalPeriodId As Integer) As Boolean
        Dim Clscontracts As New Clshrs_Contracts(mPage)
        Dim ClsTransactions As New Clshrs_TransactionsTypes(mPage)
        Dim ClsContractTransaction As New Clshrs_ContractsTransactions(mPage)
        Dim ClsEmployeeTransaction As New Clshrs_EmployeesTransactions(mPage)
        Dim ClsEmployeeTransactionDetails As New Clshrs_EmployeesTransactionsDetails(mPage)
        Dim ClsFiscalPeriods As New Clssys_FiscalYearsPeriods(mPage)
        Dim ClsIntervals As New Clshrs_Intervals(mPage)
        Dim DteLastTransactiondate As DateTime
        Dim IntContractID As Integer
        Dim IntIntervalID As Integer
        Dim IntNoofMonthes As Integer
        Dim IntDateDiffrance As Integer

        Try
            If Clscontracts.ContractValidatoinId(EmployeeID, FiscalPeriodId) > 0 Then
                IntContractID = Clscontracts.ID
                If ClsContractTransaction.Find(IntContractID, ToDate, " TransactionTypeID=" & TransactionID) Then
                    IntIntervalID = ClsContractTransaction.IntervalID
                    If ClsIntervals.Find("ID=" & IntIntervalID) Then
                        IntNoofMonthes = ClsIntervals.Number
                        IntDateDiffrance = ClsEmployeeTransactionDetails.FindNoOfPeriodsforLastOccurrences(EmployeeID, TransactionID, FiscalPeriodId)

                        If IntDateDiffrance = IntNoofMonthes Or (IntDateDiffrance Mod IntNoofMonthes = 0) Then
                            Return True
                        End If
                        Return False
                    End If
                End If
            End If
            Return False
        Catch ex As Exception

        Finally

        End Try
    End Function
    '===========================================
    'Function Name : GetContractsTransactions
    'Date Created  : 18-05-2008
    'Developer     : [0256]
    'Description   : Get All Contracts Transactions 
    '===========================================
    Public Function GetContractsTransactions(ByVal IntContractId As Integer, ByVal IntLanguage As Integer, ByRef DsTransactions As DataSet) As Boolean

        Try
            DsTransactions = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetContractsTransactions", IntContractId, IntLanguage)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetContractsLastTransactions(ByVal IntContractId As Integer, ByVal IntLanguage As Integer, ByRef DsTransactions As DataSet) As Boolean
        Try
            DsTransactions = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetContractsLastTransactions", IntContractId, IntLanguage)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetContractsLastTransactionsCost(ByVal IntContractId As Integer, ByVal IntLanguage As Integer, ByRef DsTransactions As DataSet) As Boolean
        Try
            DsTransactions = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetContractsLastTransactionsCost", IntContractId, IntLanguage)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '===========================================
    'Function Name : GetContractsTransactions
    'Date Created  : 18-05-2008
    'Developer     : [0256]
    'Description   : Get All Grades Steps Transactions 
    '===========================================
    Public Function GetGradesStepsTransactions(ByVal IntGradeStepId As Integer, ByVal IntLanguage As Integer, ByRef DsTransactions As Data.DataSet) As Boolean
        Try
            DsTransactions = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetGradesStepsTransactions", IntGradeStepId, IntLanguage)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function SaveContractTransacions(ByVal uwg As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal intContractID As Integer, ByVal DeleteAll As Boolean, ByVal IsSocialInsuranceIncluded As Boolean) As Integer
        Try
            Dim clsTransType As New Clshrs_TransactionsTypes(mPage)
            Dim clsFormula As Clshrs_FormulaSolver
            Dim clsCont As New Clshrs_Contracts(mPage)
            Dim strCmd As String = CONFIG_DATEFORMAT
            Dim Isvalid = False
            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwg.Rows
                If Not IsDBNull(row.Cells.FromKey("TransactionName").Value) AndAlso row.Cells.FromKey("TransactionName").Value <> 0 Then
                    TransactionTypeID = mDataHandler.DataValue_Out(row.Cells.FromKey("TransactionName").Value, SqlDbType.Int, True)
                    clsTransType.Find("ID=" & TransactionTypeID)
                    If clsTransType.IsBasicSalary = True Then
                        If mDataHandler.DataValue_Out(row.Cells.FromKey("Amount").Value, SqlDbType.Money) > 0 Then
                            Isvalid = True
                            Exit For
                        End If
                    End If
                End If
            Next
            If Isvalid = False Then
                Return -1
            End If

            If DeleteAll Then
                strCmd &= " Update " & mTable & " Set CancelDate = GetDate() where ContractID = " & intContractID & ";"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, CommandType.Text, strCmd)
            End If
            Dim bFirst As Boolean = True
            Dim strMode As String = "N"
            clsCont.Find("ID=" & intContractID)


            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwg.Rows
                If Not IsDBNull(row.Cells.FromKey("TransactionName").Value) AndAlso row.Cells.FromKey("TransactionName").Value <> 0 Then
                    If Not IsDBNull(row.Cells.FromKey("ID").Value) AndAlso row.Cells.FromKey("ID").Value <> 0 Then
                        If Find("ID=" & row.Cells.FromKey("ID").Value & " And ContractID= " & intContractID) Then
                            strMode = "E"
                        Else
                            strMode = "N"
                        End If
                    Else
                        strMode = "N"
                    End If
                    TransactionTypeID = mDataHandler.DataValue_Out(row.Cells.FromKey("TransactionName").Value, SqlDbType.Int, True)

                    clsTransType.Find("ID=" & TransactionTypeID)
                    Remarks = Amount
                    Amount = mDataHandler.DataValue_Out(row.Cells.FromKey("Amount").Value, SqlDbType.Money)
                    Active = row.Cells.FromKey("Active").Value
                    IntervalID = mDataHandler.DataValue_Out(row.Cells.FromKey("IntervalID").Value, SqlDbType.Int, True)
                    PaidAtVacation = row.Cells.FromKey("PaidAtVacation").Value
                    OnceAtPeriod = row.Cells.FromKey("OnceAtPeriod").Value
                    ActiveDate = SetHigriDate2(row.Cells.FromKey("ActiveDate").Value, ActiveDate_D)

                    If strMode = "E" Then
                        CancelDate = Nothing
                    End If
                    If strMode = "N" Then
                        ContractID = intContractID
                        Save()
                    ElseIf strMode = "E" Then
                        Update("ID=" & row.Cells.FromKey("ID").Value)
                    End If
                    'End If
                End If
            Next

            'Update-------------------------
            Dim DsContractsTransations As New Data.DataSet
            GetContractsLastTransactions(intContractID, 0, DsContractsTransations)
            uwg.DataSource = Nothing
            uwg.DataBind()

            uwg.DataSource = DsContractsTransations
            uwg.DataBind()


            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwg.Rows
                If Not IsDBNull(row.Cells.FromKey("TransactionName").Value) AndAlso row.Cells.FromKey("TransactionName").Value <> 0 Then
                    If Not IsDBNull(row.Cells.FromKey("ID").Value) AndAlso row.Cells.FromKey("ID").Value <> 0 Then
                        If Find("ID=" & row.Cells.FromKey("ID").Value & " And ContractID= " & intContractID) Then
                            strMode = "E"
                        Else
                            strMode = "N"
                        End If
                    Else
                        strMode = "N"
                    End If
                    TransactionTypeID = mDataHandler.DataValue_Out(row.Cells.FromKey("TransactionName").Value, SqlDbType.Int, True)

                    Remarks = Amount
                    clsTransType.Find("ID=" & TransactionTypeID)
                    If Not clsTransType.IsBasicSalary Then
                        Dim FormulaExpression As String = ""
                        If clsTransType.HasInsuranceTiers Then
                            If IsSocialInsuranceIncluded Then
                                Dim sTSql = "SELECT  TOP (1) BaseFormulaTiers FROM     hrs_TransactionsTypesTiers WHERE    (TransactionsTypesId = " & clsTransType.ID & ") AND ((MONTH(FinancialPeriodTiers) <= " & DateTime.Now.Month & " AND YEAR(FinancialPeriodTiers) = " & DateTime.Now.Year & ") or YEAR(FinancialPeriodTiers) < " & DateTime.Now.Year & " ) order by FinancialPeriodTiers desc"
                                Dim strFormula = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsTransType.ConnectionString, CommandType.Text, sTSql)
                                If strFormula <> "" Then
                                    FormulaExpression = strFormula
                                Else
                                    FormulaExpression = clsTransType.Formula
                                End If
                            Else
                                FormulaExpression = clsTransType.Formula
                            End If
                        Else
                            FormulaExpression = clsTransType.Formula
                        End If

                        If clsTransType.TransactionGroupID <> 3 Then
                            If FormulaExpression.Length > 0 Then

                                If IsNumeric(FormulaExpression) Then
                                    Amount = FormulaExpression
                                Else
                                    Dim fisPeriodID As Integer
                                    Dim fisFrom As DateTime
                                    Dim fisTo As DateTime

                                    Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(mPage)
                                    ClsFisicalPeriods.GetFisicalperiodInfo(SetHigriDate2(row.Cells.FromKey("ActiveDate").Value, ActiveDate_D), fisPeriodID, fisFrom, fisTo)

                                    clsFormula = New Clshrs_FormulaSolver(mConnectionString, mPage)
                                    clsFormula.Executedate = SetHigriDate2(row.Cells.FromKey("ActiveDate").Value, ActiveDate_D)
                                    clsFormula.FormulaCalculated = "N"
                                    clsFormula.EmployeeID = clsCont.EmployeeID
                                    clsFormula.BolBeginOfContract = True
                                    clsFormula.NoOfWorkingDays = 30
                                    clsFormula.NoOfDaysPerPeriod = 30
                                    clsFormula.FiscalPeriodID = fisPeriodID
                                    clsFormula.EvaluateExpression(FormulaExpression, 0)
                                    Amount = clsFormula.Output
                                    Amount = IIf(Amount < 0, 0, Amount)
                                End If
                            Else
                                Amount = mDataHandler.DataValue_Out(row.Cells.FromKey("Amount").Value, SqlDbType.Money)
                            End If
                        Else
                            If FormulaExpression.Length > 0 Then
                                If IsNumeric(FormulaExpression) Then
                                    Amount = FormulaExpression
                                Else
                                    Dim fisPeriodID As Integer
                                    Dim fisFrom As DateTime
                                    Dim fisTo As DateTime

                                    Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(mPage)
                                    ClsFisicalPeriods.GetFisicalperiodInfo(SetHigriDate2(row.Cells.FromKey("ActiveDate").Value, ActiveDate_D), fisPeriodID, fisFrom, fisTo)

                                    clsFormula = New Clshrs_FormulaSolver(mConnectionString, mPage)
                                    clsFormula.Executedate = SetHigriDate2(row.Cells.FromKey("ActiveDate").Value, ActiveDate_D)
                                    clsFormula.FormulaCalculated = "N"
                                    clsFormula.EmployeeID = clsCont.EmployeeID
                                    clsFormula.BolBeginOfContract = False
                                    clsFormula.NoOfWorkingDays = 30
                                    clsFormula.NoOfDaysPerPeriod = 30
                                    clsFormula.FiscalPeriodID = fisPeriodID
                                    clsFormula.EvaluateExpression(FormulaExpression, 0)
                                    Amount = clsFormula.Output
                                    Amount = IIf(Amount < 0, 0, Amount)
                                End If
                            Else
                                Amount = mDataHandler.DataValue_Out(row.Cells.FromKey("Amount").Value, SqlDbType.Money)
                            End If
                        End If

                        Active = row.Cells.FromKey("Active").Value
                        IntervalID = mDataHandler.DataValue_Out(row.Cells.FromKey("IntervalID").Value, SqlDbType.Int, True)
                        PaidAtVacation = row.Cells.FromKey("PaidAtVacation").Value
                        OnceAtPeriod = row.Cells.FromKey("OnceAtPeriod").Value
                        ActiveDate = SetHigriDate2(row.Cells.FromKey("ActiveDate").Value, ActiveDate_D)

                        If strMode = "E" Then
                            CancelDate = Nothing
                        End If
                        If strMode = "N" Then
                            ContractID = intContractID
                            Save()
                        ElseIf strMode = "E" Then
                            Update("ID=" & row.Cells.FromKey("ID").Value)
                        End If
                    End If
                End If
            Next

            Return 1
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
End Class