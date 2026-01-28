Imports Venus.Application.SystemFiles.System
Public Class Clshrs_TransactionsTypesBase
    Inherits ClsDataAcessLayer
    '==================================================================
    'Created by : [0258]
    'Date : 11/07/2007

    'Description: This Class Related to TransactionGroups class 

    '==================================================================
#Region "Class Constructors"
    '==================================================================
    'Created by : [0258]
    'Date : 11/07/2007
    'Input : 
    'Description: In the constructor of the class set the table name and 
    '           sqlstatment of (Insert,Update,Delete,select) row from the table

    '==================================================================
    Public Sub New(ByVal Page As Web.UI.Page)

        MyBase.New(Page)
        mTable = " hrs_TransactionsTypes "
        mInsertParameter = " Code,EngName,ShortEngName,ArbName,ShortArbName,ArbName4S,ShortArbName4S,TransactionGroupID,Sign,DebitAccountCode,CreditAccountCode,IsPaid,Formula,BeginContractFormula,EndContractFormula,InputIsNumeric,Remarks,RegUserID,RegComputerID,CompanyID ,IsEndOfService,IsBasicSalary,IsSalaryEOSExeclude,IsProjectRelatedItem,IsDistributable,IsAllowPosting,HasInsuranceTiers "
        mInsertParameterValues = " @Code,@EngName,@ShortEngName,@ArbName,@ShortArbName,@ArbName4S,@ShortArbName4S,@TransactionGroupID,@Sign,@DebitAccountCode,@CreditAccountCode,@IsPaid,@Formula,@BeginContractFormula,@EndContractFormula,@InputIsNumeric,@Remarks,@RegUserID,@RegComputerID,@CompanyID,@IsEndOfService,@IsBasicSalary,@IsSalaryEOSExeclude,@IsProjectRelatedItem,@IsDistributable,@IsAllowPosting,@HasInsuranceTiers"
        mUpdateParameter = " Code=@Code,EngName=@EngName,ShortEngName=@ShortEngName,ArbName=@ArbName,ShortArbName=@ShortArbName,ArbName4S=@ArbName4S,ShortArbName4S=@ShortArbName4S,TransactionGroupID=@TransactionGroupID,Sign=@Sign,DebitAccountCode=@DebitAccountCode,CreditAccountCode=@CreditAccountCode,IsPaid=@IsPaid,Formula=@Formula,BeginContractFormula=@BeginContractFormula,EndContractFormula=@EndContractFormula,InputIsNumeric=@InputIsNumeric,IsEndOfService=@IsEndOfService,IsBasicSalary=@IsBasicSalary,IsSalaryEOSExeclude=@IsSalaryEOSExeclude,IsProjectRelatedItem=@IsProjectRelatedItem,Remarks=@Remarks,IsDistributable=@IsDistributable,IsAllowPosting=@IsAllowPosting,HasInsuranceTiers=@HasInsuranceTiers "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"

    End Sub

#End Region

#Region "Private Members"

    Protected mID As Object
    Private mCode As String
    Private mEngName As String
    Private mShortEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mShortArbName As String
    Private mShortArbName4S As String
    Private mTransactionGroupID As Object
    Private mSign As Object
    Private mDebitAccountCode As String
    Private mCreditAccountCode As String
    Private mIsPaid As Boolean
    Private mFormula As String
    Private mBeginContractFormula As String
    Private mEndContractFormula As String
    Private mInputIsNumeric As Boolean
    Private mIsAllowPosting As Boolean
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mCompanyID As Object
    Private mIsEndOfService As Boolean
    Private mIsBasicSalary As Boolean
    Private mIsSalaryEOSExeclude As Boolean
    Private mIsProjectRelatedItem As Boolean
    Private mIsDistributable As Boolean
    Private mHasInsuranceTiers As Boolean
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

    Public Property EngName() As String
        Get
            Return mEngName
        End Get
        Set(ByVal Value As String)
            mEngName = Value
        End Set
    End Property

    Public Property ShortEngName() As String
        Get
            Return mShortEngName
        End Get
        Set(ByVal Value As String)
            mShortEngName = Value

        End Set
    End Property

    Public Property ArbName() As String
        Get
            Return mArbName
        End Get
        Set(ByVal Value As String)
            mArbName = Value
            mArbName4S = mStringHandler.ReplaceHamza(Value)
        End Set
    End Property

    Public Property ShortArbName() As String
        Get
            Return mShortArbName
        End Get
        Set(ByVal Value As String)
            mShortArbName = Value
            mShortArbName4S = mStringHandler.ReplaceHamza(Value)
        End Set
    End Property

    Public Property TransactionGroupID() As Object
        Get
            Return mTransactionGroupID
        End Get
        Set(ByVal Value As Object)
            mTransactionGroupID = Value
        End Set
    End Property

    Public Property Sign() As Object
        Get
            Return mSign
        End Get
        Set(ByVal Value As Object)
            mSign = Value
        End Set
    End Property

    Public Property DebitAccountCode() As String
        Get
            Return mDebitAccountCode
        End Get
        Set(ByVal Value As String)
            mDebitAccountCode = Value
        End Set
    End Property

    Public Property CreditAccountCode() As String
        Get
            Return mCreditAccountCode
        End Get
        Set(ByVal Value As String)
            mCreditAccountCode = Value
        End Set
    End Property

    Public Property IsPaid() As Boolean
        Get
            Return mIsPaid
        End Get
        Set(ByVal Value As Boolean)
            mIsPaid = Value
        End Set
    End Property

    Public Property Formula() As String
        Get
            Return mFormula
        End Get
        Set(ByVal Value As String)
            mFormula = Value
        End Set
    End Property
    Public Property BeginContractFormula() As String
        Get
            Return mBeginContractFormula
        End Get
        Set(ByVal Value As String)
            mBeginContractFormula = Value
        End Set
    End Property
    Public Property EndContractFormula() As String
        Get
            Return mEndContractFormula
        End Get
        Set(ByVal Value As String)
            mEndContractFormula = Value
        End Set
    End Property

    Public Property InputIsNumeric() As Boolean
        Get
            Return mInputIsNumeric
        End Get
        Set(ByVal Value As Boolean)
            mInputIsNumeric = Value
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
        Set(ByVal Value As Object)
            mCancelDate = Value
        End Set
    End Property

    Public Property CompanyID() As Object
        Get
            Return mCompanyID
        End Get
        Set(ByVal Value As Object)
            mCompanyID = Value
        End Set
    End Property
    Public Property IsEndOfService() As Boolean
        Get
            Return mIsEndOfService
        End Get
        Set(ByVal value As Boolean)
            mIsEndOfService = value
        End Set
    End Property
    Public Property IsBasicSalary() As Boolean
        Get
            Return mIsBasicSalary
        End Get
        Set(ByVal value As Boolean)
            mIsBasicSalary = value
        End Set
    End Property
    Public Property IsSalaryEOSExeclude() As Boolean
        Get
            Return mIsSalaryEOSExeclude
        End Get
        Set(ByVal value As Boolean)
            mIsSalaryEOSExeclude = value
        End Set
    End Property
    Public Property IsProjectRelatedItem() As Boolean
        Get
            Return mIsProjectRelatedItem
        End Get
        Set(ByVal value As Boolean)
            mIsProjectRelatedItem = value
        End Set
    End Property

    Public Property HasInsuranceTiers() As Boolean
        Get
            Return mHasInsuranceTiers
        End Get
        Set(ByVal value As Boolean)
            mHasInsuranceTiers = value
        End Set
    End Property
    Public Property IsDistributable() As Boolean
        Get
            Return mIsDistributable
        End Get
        Set(ByVal value As Boolean)
            mIsDistributable = value
        End Set
    End Property

    Public Property IsAllowPosting() As Boolean
        Get
            Return mIsAllowPosting
        End Get
        Set(ByVal value As Boolean)
            mIsAllowPosting = value
        End Set
    End Property

#End Region

#Region "Public Function"
    '==================================================================
    'Created by : [0258]
    'Date : 11/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from hrs_TransactionsTypes table where filter and canceldate = null  
    'Steps: 
    '       1-Fill Dataset with the results of sqldataAdapter
    '       2-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '       3-Clear all private members of the class
    '       4-Return true if ID of Filteration >0 (Is Found)
    '==================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If
            'Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
            '==================== Order By Modification [ End ]
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            '==================== Order By Modification [Start]
            StrSelectCommand = StrSelectCommand & orderByStr
            '==================== Order By Modification [ End ]
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
    '==================================================================
    'Created by : [0258]
    'Date : 11/07/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from hrs_TransactionsTypes where filter 
    '       2-Check if ID > 0 this mean that row is already exist in hrs_TransactionsTypes  table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in hrs_TransactionsTypes  table
    '==================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_TransactionsTypes Where " & Filter
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
    'Date : 11/07/2007

    'Description: Save New Row in hrs_TransactionsTypes  table
    'Steps: 
    '       1-execute sqlstatment to insert new row in hrs_TransactionTypes  table

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
    'Date : 11/07/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in hrs_TransactionsTypes  table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in hrs_TransactionsTypes  table

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
    'Created by : [0259]
    'Date : 26/08/2007
    'Description: Getting if there are any difference between DataSet and Class's Properties
    '==================================================================

    Public Function CheckDiff(ByVal ClassObj As Object, ByVal DSData As DataSet, ByVal Filter As String) As Boolean

        Dim myPropertyInfo As Reflection.PropertyInfo() = CType(ClassObj.GetType, Type).GetProperties()
        Dim PropertyCounter As Integer
        Dim DSCounter As Integer
        Dim Index() As Object
        Try

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


        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function FixNull(ByVal obj As Object, ByVal DataColumn As Global.System.Data.DataColumn) As Object
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
    '==================================================================
    'Created by : [0258]
    'Date : 11/07/2007
    'Inptut : Filter as string (ex.ID=2)
    'Description: Delete existing Row in hrs_TransactionsTypes  table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in hrs_TransactionsTypes  table

    '==================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand, OperationType.Update)
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
    'Date : 11/07/2007
    'Description: Clear all private members  of the class

    '==================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEngName = String.Empty
            mShortEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mShortArbName = String.Empty
            mShortArbName4S = String.Empty
            mTransactionGroupID = 0
            mSign = 1
            mIsBasicSalary = False
            mIsSalaryEOSExeclude = False
            mIsProjectRelatedItem = False
            mHasInsuranceTiers = False
            mIsEndOfService = False
            mDebitAccountCode = String.Empty
            mCreditAccountCode = String.Empty
            mIsPaid = False
            mFormula = String.Empty
            mBeginContractFormula = String.Empty
            mEndContractFormula = String.Empty
            mInputIsNumeric = False
            mIsAllowPosting = False
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mCompanyID = 0

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : [0258]
    'Date : 11/07/2007
    'Description:  find first row in hrs_TransactionsTypes table
    'Steps: 
    '       1-execute sqlstatment to find first row in hrs_TransactionsTypes  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And CompanyID= " & Me.MainCompanyID & " ORDER BY Code ASC"
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
    'Date : 11/07/2007
    'Description:  find Last row in hrs_TransactionsTypes  table
    'Steps: 
    '       1-execute sqlstatment to find last row in hrs_TransactionsTypes  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And CompanyID=" & Me.MainCompanyID & " ORDER BY Code DESC"
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
    'Date : 11/07/2007
    'Description:  find Next row in hrs_TransactionsTypes  table
    'Steps: 
    '       1-execute sqlstatment to find Next row in hrs_TransactionsTypes  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID & " ORDER BY Code ASC"
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
    'Date : 11/07/2007
    'Description:  find previous row in hrs_TransactionsTypes  table
    'Steps: 
    '       1-execute sqlstatment to find previous row in hrs_TransactionsTypes  table
    '       2-Fill dataset with result of sqlstatment
    '       3-call Getparameter function to insert the result of dataset into private members of the class
    '==================================================================
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code < '" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And CompanyID= " & Me.MainCompanyID & " ORDER BY Code DESC"
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
    '===================================================================
    'Created by : [0258]
    'Date : 11/07/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class

    '===================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mShortEngName = mDataHandler.DataValue_Out(.Item("ShortEngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mShortArbName = mDataHandler.DataValue_Out(.Item("ShortArbName"), SqlDbType.VarChar)
                mShortArbName4S = mDataHandler.DataValue_Out(.Item("ShortArbName4S"), SqlDbType.VarChar)
                mTransactionGroupID = mDataHandler.DataValue_Out(.Item("TransactionGroupID"), SqlDbType.Int, True)
                mSign = mDataHandler.DataValue_Out(.Item("Sign"), SqlDbType.Int)
                mDebitAccountCode = mDataHandler.DataValue_Out(.Item("DebitAccountCode"), SqlDbType.VarChar)
                mCreditAccountCode = mDataHandler.DataValue_Out(.Item("CreditAccountCode"), SqlDbType.VarChar)
                mIsPaid = mDataHandler.DataValue_Out(.Item("IsPaid"), SqlDbType.Bit)
                mFormula = mDataHandler.DataValue_Out(.Item("Formula"), SqlDbType.VarChar)
                mBeginContractFormula = mDataHandler.DataValue_Out(.Item("BeginContractFormula"), SqlDbType.VarChar)
                mEndContractFormula = mDataHandler.DataValue_Out(.Item("EndContractFormula"), SqlDbType.VarChar)
                mInputIsNumeric = mDataHandler.DataValue_Out(.Item("InputIsNumeric"), SqlDbType.Bit)
                '[AGL]
                mIsEndOfService = mDataHandler.DataValue_Out(.Item("IsEndOfService"), SqlDbType.Bit)
                mIsBasicSalary = mDataHandler.DataValue_Out(.Item("IsBasicSalary"), SqlDbType.Bit)
                mIsSalaryEOSExeclude = mDataHandler.DataValue_Out(.Item("IsSalaryEOSExeclude"), SqlDbType.Bit)
                mIsProjectRelatedItem = mDataHandler.DataValue_Out(.Item("IsProjectRelatedItem"), SqlDbType.Bit)
                mHasInsuranceTiers = mDataHandler.DataValue_Out(.Item("HasInsuranceTiers"), SqlDbType.Bit)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mCompanyID = mDataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int, True)
                mIsDistributable = mDataHandler.DataValue_Out(.Item("IsDistributable"), SqlDbType.Bit)
                mIsAllowPosting = mDataHandler.DataValue_Out(.Item("IsAllowPosting"), SqlDbType.Bit)
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
    'Date : 11/07/2007
    'Description:   Make the values of parameter equal values of private member  of the class

    '===================================================================
    Protected Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal operationtype As OperationType) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ShortEngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mShortEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ShortArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mShortArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ShortArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mShortArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransactionGroupID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTransactionGroupID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Sign", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSign, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DebitAccountCode", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDebitAccountCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CreditAccountCode", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCreditAccountCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsPaid", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsPaid, SqlDbType.Bit)
            'AGL
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsEndOfService", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsEndOfService, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsBasicSalary", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsBasicSalary, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsSalaryEOSExeclude", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsSalaryEOSExeclude, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsProjectRelatedItem", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsProjectRelatedItem, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HasInsuranceTiers", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHasInsuranceTiers, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Formula", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFormula, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BeginContractFormula", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBeginContractFormula, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EndContractFormula", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEndContractFormula, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InputIsNumeric", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mInputIsNumeric, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Select Case operationtype
                Case ClsDataAcessLayer.OperationType.Save
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
                    Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
            End Select
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsDistributable", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsDistributable, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsAllowPosting", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsAllowPosting, SqlDbType.Bit)
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



Public Class Clshrs_TransactionsTypes
    Inherits Clshrs_TransactionsTypesBase

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
    End Sub

    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 "
            'StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
                Item.DataValue = ObjDataRow("ID")
                DdlValues.ValueListItems.Add(Item)
            Next

            If DdlValues.ValueListItems.Count > 0 Then
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

    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()


            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                Item.DisplayText = " "
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
                Item.DataValue = ObjDataRow("ID")
                DdlValues.ValueListItems.Add(Item)
            Next

            If DdlValues.ValueListItems.Count > 0 Then
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

    Public Function GetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, ByVal Valuemember As String, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID & Filter
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()


            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                Item.DisplayText = " "
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
                Item.DataValue = ObjDataRow(Valuemember)
                DdlValues.ValueListItems.Add(Item)
            Next

            If DdlValues.ValueListItems.Count > 0 Then
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
    'Created by : [0258]
    'Date : 11/07/2007
    'Input : DdlValues as DropDownList
    '       Condition to fill dropdownlist (if Wanted)
    '       NullNode to make attention to user by make first item ="Please Select"    
    'Description:  Fill DropDownList with Countries 
    'Steps: 1- Exceute sql statement to get all columns from sys_region table where filter parameter(if spcefied)
    '       2- if NullNode is true --then make first text item is  "Please Select" and it's value is 0
    '       3- fill DtatText of Dropdownlist with English Nameand DataValue with ID 
    '==================================================================
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CompanyID=" & Me.MainCompanyID
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Items.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem

                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                'Item.Text = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
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
    'Created by : [0258]
    'Date : 10/9/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update all rows set Isbasicsalay = False

    '==================================================================
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  And " & Filter & " Order By EngName", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Order By EngName")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
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
    'Created by : [0258]
    'Date : 10/9/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update all rows set Isbasicsalay = False

    '==================================================================
    Public Function UpdateIsBasicSalary(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String = String.Empty
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = " Update " & mTable & " Set IsBasicSalary = 0 Where " & Filter
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
End Class