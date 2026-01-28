'Module            : Hrs (Human Resource Module)
'Date Created      : 22-08-2007
'Developer         : Data Ocean
'                  : [MAZ]
'Description       : 1-Implement Data Acess Layer of hrs_EmployeesPayabilities table with fields 
'                    2-Allow searching
'                    3-Get list with all codes
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                    5-Implement functions first(),last(),next() and previous() to allow navigation between 
'                       records
'Modifications     : ADD fUN
'==========================================================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesPayability
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    : Initialize insert ,update and delete commands
    'Developer      :[0257]   
    'Date Created   :22-08-2007
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesPayabilities "
        mInsertParameter = " Number,TransactionTypeID,EmployeeID,TransactionDate,TransactionComment,SalaryLink,Remarks,RegUserID,RegComputerID,Src,RequestID "
        mInsertParameterValues = " @Number,@TransactionTypeID,@EmployeeID,@TransactionDate,@TransactionComment,@SalaryLink,@Remarks,@RegUserID,@RegComputerID,@Src,@RequestID  "
        mUpdateParameter = " Number=@Number,TransactionTypeID=@TransactionTypeID,EmployeeID=@EmployeeID,TransactionDate=@TransactionDate,TransactionComment=@TransactionComment,SalaryLink=@SalaryLink,Remarks=@Remarks,Src=@Src,RequestID=@RequestID  "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region

#Region "Private Members"

    Private mID As Object
    Private mNumber As Object
    Private mTransactionTypeID As Object
    Private mEmployeeID As Object
    Private mTransactionDate As Object
    Private mTransactionComment As String
    Private mSalaryLink As Object
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mCompanyId As Integer
    Public Grid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid
    Private mSrc As String
    Private mRequestID As String

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
    Public Property Number() As Object
        Get
            Return mNumber
        End Get
        Set(ByVal Value As Object)
            mNumber = Value
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
    Public Property EmployeeID() As Object
        Get
            Return mEmployeeID
        End Get
        Set(ByVal Value As Object)
            mEmployeeID = Value
        End Set
    End Property

    Public Property TransactionDate() As Object
        Get
            Return mTransactionDate
        End Get
        Set(ByVal Value As Object)
            mTransactionDate = Value
        End Set
    End Property

    Public Property TransactionComment() As String
        Get
            Return mTransactionComment
        End Get
        Set(ByVal Value As String)
            mTransactionComment = Value
        End Set
    End Property
    Public Property SalaryLink() As String
        Get
            Return mSalaryLink
        End Get
        Set(ByVal Value As String)
            mSalaryLink = Value
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
    Public Property Src() As String
        Get
            Return mSrc
        End Get
        Set(ByVal Value As String)
            mSrc = Value
        End Set
    End Property
    Public Property RequestID() As String
        Get
            Return mRequestID
        End Get
        Set(ByVal Value As String)
            mRequestID = Value
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

    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  [0257]   
    'Date Created   :  22-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Number "
            End If
            Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
            '==================== Order By Modification [ End ]

            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    '========================================================================
    'ProcedureName  :  FindEmployee 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find employee certain to employee payability
    'Developer      :  [0257]   
    'Date Created   :  22-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function FindEmployee(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "Select hrs_EmployeesPayabilities.Number as PayabilityCode,hrs_EmployeesPayabilities.Id  as PayabilityID,hrs_Employees.Code as PayrollId,* ,case when isNull((Select sum(amount) From	hrs_EmployeesPayabilitiesSchedules inner join hrs_EmployeesPayabilitiesSchedulesSettlement on hrs_EmployeesPayabilitiesSchedules.id = hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID Where EmployeePayabilityId=hrs_EmployeesPayabilities.id),0) = ( Select sum(dueAmount) From	hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId=hrs_EmployeesPayabilities.id) then 'Paid' else 'Not Paid' end As Status From hrs_EmployeesPayabilities Inner Join hrs_Employees on hrs_Employees.id = hrs_EmployeesPayabilities.Employeeid  " & IIf(Len(Filter) > 0, " Where IsNull(hrs_EmployeesPayabilities.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesPayabilities.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(hrs_EmployeesPayabilities.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesPayabilities.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
    Public Function FindEmployeePayabilitesDetails(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Dim clsEmp As New Clshrs_Employees(mPage)
        Try
            'StrSelectCommand = "Select hrs_EmployeesPayabilities.Number as PayabilityCode,hrs_EmployeesPayabilities.Id  as PayabilityID,hrs_Employees.Code as PayrollId,* ,case when isNull((Select sum(amount) From	hrs_EmployeesPayabilitiesSchedules inner join hrs_EmployeesPayabilitiesSchedulesSettlement on hrs_EmployeesPayabilitiesSchedules.id = hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID Where EmployeePayabilityId=hrs_EmployeesPayabilities.id),0) = ( Select sum(dueAmount) From	hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId=hrs_EmployeesPayabilities.id) then 'Paid' else 'Not Paid' end As Status From hrs_EmployeesPayabilities Inner Join hrs_Employees on hrs_Employees.id = hrs_EmployeesPayabilities.Employeeid  " & IIf(Len(Filter) > 0, " Where IsNull(hrs_EmployeesPayabilities.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesPayabilities.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(hrs_EmployeesPayabilities.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesPayabilities.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = "Select hrs_EmployeesPayabilities.Number as PayabilityCode,hrs_EmployeesPayabilities.Id  as PayabilityID,hrs_Employees.Code as PayrollId," & clsEmp.ArrangeIncomingName(Me.mLangauge) & " as FullName," & IIf(Me.mLangauge = Language.Arbic, "hrs_TransactionsTypes.ArbName", "hrs_TransactionsTypes.EngName") & " as TransactionType,TransactionDate,case when isNull((Select sum(amount) From	hrs_EmployeesPayabilitiesSchedules inner join hrs_EmployeesPayabilitiesSchedulesSettlement on hrs_EmployeesPayabilitiesSchedules.id = hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID Where EmployeePayabilityId=hrs_EmployeesPayabilities.id),0) = ( Select sum(dueAmount) From	hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId=hrs_EmployeesPayabilities.id) then 'Paid' else 'Not Paid' end As Status,(select Top 1 DueDate from hrs_EmployeesPayabilitiesSchedules  where EmployeePayabilityId=hrs_EmployeesPayabilities.id  order by DueDate Asc ) as StartDate,( select Top 1 DueDate from hrs_EmployeesPayabilitiesSchedules  where EmployeePayabilityId=hrs_EmployeesPayabilities.id  order by DueDate Desc ) as EndDate, ( Select  sum(dueAmount) From	 hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId=hrs_EmployeesPayabilities.id ) as [Value] , ( 	( Select  sum(dueAmount) From hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId=hrs_EmployeesPayabilities.id  ) -  IsNull( 	(Select sum(amount)  From hrs_EmployeesPayabilitiesSchedules inner join hrs_EmployeesPayabilitiesSchedulesSettlement on hrs_EmployeesPayabilitiesSchedules.id = hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID Where EmployeePayabilityId=hrs_EmployeesPayabilities.id  ),0)  ) as Balance  From hrs_EmployeesPayabilities Inner Join hrs_Employees on hrs_Employees.id = hrs_EmployeesPayabilities.Employeeid Inner Join hrs_TransactionsTypes on hrs_TransactionsTypes.ID = hrs_EmployeesPayabilities.TransactionTypeID  " & IIf(Len(Filter) > 0, " Where IsNull(hrs_EmployeesPayabilities.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesPayabilities.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(hrs_EmployeesPayabilities.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesPayabilities.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                'GetParameter(mDataSet)
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

    '========================================================================
    'ProcedureName  :  FindEmployee 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find total transaction amount specific to emlloyee payability
    'Developer      :  [0257]   
    'Date Created   :  22-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function GetTransactionAmount(ByVal TransId As Integer) As Double
        Dim StrSelectCommand As String
        Dim DblValue As Double
        Dim SqlConnection As New SqlClient.SqlConnection(mConnectionString)
        Try
            StrSelectCommand = "Select IsNull(Sum(DueAmount),0) From hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId=" & TransId
            mSqlCommand = New SqlClient.SqlCommand(StrSelectCommand, SqlConnection)
            mSqlCommand.Connection.Open()
            DblValue = mSqlCommand.ExecuteScalar
            mSqlCommand.Connection.Close()
            Return DblValue
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetSettlementsAmount 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find total Settlements Amount specific to emlloyee payability
    'Developer      :  [0257]   
    'Date Created   :  22-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function GetSettlementsAmount(ByVal TransId As Integer) As Double
        Dim StrSelectCommand As String
        Dim DblValue As Double
        Dim SqlConnection As New SqlClient.SqlConnection(mConnectionString)
        Try
            StrSelectCommand = "Select IsNull(Sum(hrs_EmployeesPayabilitiesSchedulesSettlement.Amount),0) From hrs_EmployeesPayabilitiesSchedulesSettlement INNER JOIN hrs_EmployeesPayabilitiesSchedules  ON hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.ID  Where hrs_EmployeesPayabilitiesSchedules.EmployeePayabilityId=" & TransId
            mSqlCommand = New SqlClient.SqlCommand(StrSelectCommand, SqlConnection)
            mSqlCommand.Connection.Open()
            DblValue = mSqlCommand.ExecuteScalar
            mSqlCommand.Connection.Close()
            Return DblValue
        Catch ex As Exception
            Return DblValue
        End Try
    End Function

    Public Function GetSettlementAmount(ByVal TransId As Integer) As Double
        Dim StrSelectCommand As String
        Dim DblValue As Double
        Dim SqlConnection As New SqlClient.SqlConnection(mConnectionString)
        Try
            StrSelectCommand = "Select IsNull(Sum(Amount),0) From hrs_EmployeesPayabilitiesSchedulesSettlement Where EmployeePayabilityScheduleID in(Select ID from hrs_EmployeesPayabilitiesSchedules where EmployeePayabilityId = " & TransId & " ) "
            'StrSelectCommand = "Select Sum(hrs_EmployeesPayabilitiesSchedulesSettlement.Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement INNER JOIN hrs_EmployeesPayabilitiesSchedules  ON hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.ID  Where hrs_EmployeesPayabilitiesSchedules.EmployeePayabilityId=" & TransId
            mSqlCommand = New SqlClient.SqlCommand(StrSelectCommand, SqlConnection)
            mSqlCommand.Connection.Open()
            DblValue = mSqlCommand.ExecuteScalar
            mSqlCommand.Connection.Close()
            Return DblValue
        Catch ex As Exception
            Return DblValue
            'mPage.Session.Add("ErrorValue", ex)
            'mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            'mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetInstalmentAmount 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find first Instalment Amount specific to emlloyee payability
    'Developer      :  [0257]   
    'Date Created   :  22-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'TransId             :int       :EmployeePayabilityId
    '========================================================================
    Public Function GetInstalmentAmount(ByVal TransId As Integer) As Double
        Dim StrSelectCommand As String
        Dim DblValue As Double
        Dim SqlConnection As New SqlClient.SqlConnection(mConnectionString)
        Try
            StrSelectCommand = "Select Top 1 DueAmount From hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId=" & TransId & " And DueAmount not in (0) "
            mSqlCommand = New SqlClient.SqlCommand(StrSelectCommand, SqlConnection)
            mSqlCommand.Connection.Open()
            DblValue = mSqlCommand.ExecuteScalar
            mSqlCommand.Connection.Close()
            Return DblValue
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  GetInstalmentAmountNotPaid 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find first Instalment Amount specific to emloyee payability not equal 0(not paid)
    'Developer      :  [0260]   
    'Date Created   :  19-10-2008
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'TransId             :int       :EmployeePayabilityId
    '========================================================================
    Public Function GetInstalmentAmountNotPaid(ByVal TransId As Integer) As Double
        Dim StrSelectCommand As String
        Dim DblValue As Double
        Dim SqlConnection As New SqlClient.SqlConnection(mConnectionString)
        Try
            StrSelectCommand = "Select Top 1 DueAmount From hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId=" & TransId & " And DueAmount not in (0) And ID not in (select EmployeePayabilityScheduleID from hrs_EmployeesPayabilitiesSchedulesSettlement)"
            mSqlCommand = New SqlClient.SqlCommand(StrSelectCommand, SqlConnection)
            mSqlCommand.Connection.Open()
            DblValue = mSqlCommand.ExecuteScalar
            mSqlCommand.Connection.Close()
            Return DblValue
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetInstalmentDate 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find first Instalment date specific to emlloyee payability
    'Developer      :  [0257]   
    'Date Created   :  22-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'TransId             :int       :EmployeePayabilityId
    '========================================================================
    Public Function GetInstalmentDate(ByVal TransId As Integer) As Date
        Dim StrSelectCommand As String
        Dim DblValue As Date
        Dim SqlConnection As New SqlClient.SqlConnection(mConnectionString)
        Try
            StrSelectCommand = "Select Top 1 DueDate From hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId=" & TransId
            mSqlCommand = New SqlClient.SqlCommand(StrSelectCommand, SqlConnection)
            mSqlCommand.Connection.Open()
            DblValue = mSqlCommand.ExecuteScalar
            mSqlCommand.Connection.Close()
            Return DblValue
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetNoofInstalmentInstalment 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find  No of InstalmentInstalment specific to emlloyee payability
    'Developer      :  [0257]   
    'Date Created   :  22-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'TransId             :int       :EmployeePayabilityId
    '========================================================================
    Public Function GetNoofInstalmentInstalment(ByVal TransId As Integer) As Double
        Dim StrSelectCommand As String
        Dim DblValue As Double
        Dim SqlConnection As New SqlClient.SqlConnection(mConnectionString)
        Try
            StrSelectCommand = "Select count(DueAmount) From hrs_EmployeesPayabilitiesSchedules Where EmployeePayabilityId=" & TransId
            mSqlCommand = New SqlClient.SqlCommand(StrSelectCommand, SqlConnection)
            mSqlCommand.Connection.Open()
            DblValue = mSqlCommand.ExecuteScalar
            mSqlCommand.Connection.Close()
            Return DblValue
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  SaveUpdate
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save Or Update Row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [0257]   
    'Date Created   :  22-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '    Filter         : String    : used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_EmployeesPayabilities Where " & Filter
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

    '========================================================================
    'ProcedureName  :  Save
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save new record
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :22-08-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Save() As Integer
        Dim IntRecordId As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            IntRecordId = mSqlCommand.ExecuteScalar
            mSqlCommand.Connection.Close()

            Return IntRecordId

            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Update
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :22-08-2007
    'Modifacations  :
    'Calls          :
    'From           :(frmDependenceType.aspx.vb)SavePart()
    'To             :SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Dim IntRecordId As Integer
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

    '========================================================================
    'ProcedureName  :  FixNull
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fix the incoming null coming or going to the database 
    '                  
    'Developer      :[0257]   
    'Date Created   :22-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmDependenceType.aspx.vb)TlbMainToolbar_ButtonClicked()
    'To             : SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
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

    '========================================================================
    'ProcedureName  :  Delete
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Delete record that match critera
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :[0257]   
    'Date Created   :22-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmDependenceType.aspx.vb)TlbMainToolbar_ButtonClicked()
    'To             :SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
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
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Clear
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Clear all private attributes in class
    'Developer      :[0257]   
    'Date Created   :22-08-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mNumber = 0
            mTransactionTypeID = 0
            mEmployeeID = 0
            mSalaryLink = False
            mTransactionDate = Nothing
            mTransactionComment = String.Empty
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mSrc = String.Empty
            mRequestID = String.Empty
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '!'=!=!==========================================================================
    ' Method Name   : GetPayabilitiesInfo()
    ' Date Created  : 2-9-2007  
    ' Date Modified : 
    ' Developer     : 
    ' Description   : This Function is used to get all payabilities infromation for any employee
    '               : The Function execues the stored procedure  hrs_GetEmployeeSettlementAmount
    '               : which accepts employeeId and optionally may take transaction type id  and fiscalPeriod Dates 
    '               : and returns an arrayy consistes of payability Amount , paid amount and balance 
    ' Calling       :
    '               : Generic Methid 
    ' Fn Arrgumants : EmployeeID , TransactionTypeID , FisicalPeriodId
    '!'=!=!==========================================================================
    Public Function GetPayabilitiesInfo(ByVal EmployeeID As Integer, ByVal TransactionTypeID As Integer, ByVal FisicalPeriodId As Integer) As Single()
        Dim LoansInfo(2) As Single

        Try
            Dim StartDate As Date = Nothing
            Dim EndDate As Date = Nothing
            Dim sqlReader As SqlClient.SqlDataReader
            Dim StrSelectCommand As String = String.Empty

            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.CommandType = CommandType.StoredProcedure
            mSqlCommand.CommandText = "hrs_GetEmployeeSettlementAmount"
            mSqlConnection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.Connection = mSqlConnection
            If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
            mSqlConnection.Open()
            mSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeId", SqlDbType.Int)).Value = EmployeeID
            If TransactionTypeID > 0 Then mSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@TransactionTypeID", SqlDbType.Int)).Value = TransactionTypeID
            If FisicalPeriodId > 0 Then
                Dim clsFiscalPeriod As New Clssys_FiscalYearsPeriods(mPage)
                clsFiscalPeriod.Find(" sys_FiscalYearsPeriods = " & FisicalPeriodId)
                StartDate = clsFiscalPeriod.FromDate
                EndDate = clsFiscalPeriod.ToDate
                mSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@StartDate", SqlDbType.Int)).Value = StartDate
                mSqlCommand.Parameters.Add(New SqlClient.SqlParameter("@EndDate", SqlDbType.Int)).Value = EndDate
            End If

            sqlReader = mSqlCommand.ExecuteReader()
            While sqlReader.Read
                LoansInfo(0) = sqlReader.Item("DueAmount")
                LoansInfo(1) = sqlReader.Item("Paid")
                LoansInfo(2) = sqlReader.Item("Balance")
            End While

        Catch ex As Exception

        End Try
        mSqlConnection.Close()
        Return LoansInfo
    End Function

    '!'=!=!==========================================================================
    ' Method Name   : GetEmployeesPayabilitiesInfo()
    ' Date Created  : 03-09-2007  
    ' Date Modified : 
    ' Developer     :  
    ' Description   : This Function is used to get all payabilities infromation for any employee
    '               : If SavePayabilities Arrgument sent to function by true value the function will save payabilities infromation 
    '               : into hrs_employeesTransactionsDetails and hrs_EmployeesScheduleSettlments Files 
    '               : Function will accepts employeeId and optionally may accepts transaction type id  and fiscalPeriod Dates 
    '               : and returns The Net Amount Of Payabilities OverDue Amounts.  
    ' Calling       :
    '               : Generic Methid 
    ' Fn Arrgumants : EmployeeID , TransactionTypeID , FisicalPeriodId
    '!'=!=!==========================================================================
    Public Function GetEmployeesPayabilitiesAmount(ByVal IntEmployeeID As Integer, ByVal intFiscalPeriodId As Integer, ByVal intTransactionTypeID As Integer, ByVal IntEmployeeTransactionId As Integer, Optional ByVal SavePaybilities As Boolean = True) As Single(,)
        Dim ArrayPayabiltiesInfo(3, 1) As Single
        Dim FiscalPFromDate As Date = Nothing
        Dim FiscalPToDate As Date = Nothing
        If intFiscalPeriodId > 0 Then
            Dim ClsFiscalPeriod As New Clssys_FiscalYearsPeriods(mPage)
            ClsFiscalPeriod.Find(" sys_FiscalYearsPeriods.ID =" & intFiscalPeriodId)
            '-------------------------------0257 MODIFIED-----------------------------------------
            Dim clsCompanies As New Clssys_Companies(mPage)
            clsCompanies.Find("ID = " & clsCompanies.MainCompanyID)
            Dim Clssys_GHCalendar As New Clssys_GHCalendar(mPage)
            '------------------------------=============------------------------------------------
            FiscalPFromDate = ClsFiscalPeriod.FromDate
            FiscalPToDate = ClsFiscalPeriod.ToDate

            '-------------------------------0257 MODIFIED-----------------------------------------
            If clsCompanies.IsHigry Then
                FiscalPFromDate = Clssys_GHCalendar.GetRelativeDate(FiscalPFromDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
                FiscalPToDate = Clssys_GHCalendar.GetRelativeDate(FiscalPToDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
            Else
                FiscalPFromDate = Clssys_GHCalendar.GetRelativeDate(FiscalPFromDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input)
                FiscalPToDate = Clssys_GHCalendar.GetRelativeDate(FiscalPToDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input)

            End If
            '------------------------------=============-----------------------------------------

            ClsFiscalPeriod.finalized()
        End If

        Dim clsEmployeeTransactionsDet As New Clshrs_EmployeesTransactionsDetails(mPage)
        Dim clsTransactionsTypes As New Clshrs_TransactionsTypes(mPage)

        Dim TransactionTypesDS As New DataSet
        clsTransactionsTypes.Find("")
        TransactionTypesDS = clsTransactionsTypes.DataSet

        Dim IntCounter1 As Integer = 0 : Dim IntCounter2 As Integer = 0 : Dim IntCounter3 As Integer = 0

        Dim dueAmount As Single = 0
        Dim Settlments As Single = 0
        Dim NetAmount As Single = 0
        Dim PyabilitiesAmount As Single = 0
        Dim TransactionType As String = String.Empty

        'Dim intTransactionTypeID As Integer = 0
        Dim Sign As Integer

        Dim ClsEmpPaySchedule As New Clshrs_EmployeesPayabilitySchedules(mPage)
        Dim ClsEmpPaySchSettlments As New Clshrs_EmployeesPayabilitySchedulesSettlement(mPage)
        Dim DSsettlments As New Data.DataSet
        Try
            If Find(" EmployeeID = " & IntEmployeeID & " And SalaryLink = 1 " & IIf(intTransactionTypeID > 0, " And TransactionTypeID = " & intTransactionTypeID, " ")) Then
                For IntCounter1 = 0 To DataSet.Tables(0).Rows.Count - 1
                    'Check Payablities Schedule For employee
                    ClsEmpPaySchedule.Find(" EmployeePayabilityId = " & DataSet.Tables(0).Rows(IntCounter1).Item("ID"))
                    If ClsEmpPaySchedule.DataSet.Tables(0).Rows.Count > 0 Then

                        For IntCounter2 = 0 To ClsEmpPaySchedule.DataSet.Tables(0).Rows.Count - 1
                            '//Check if there any payabliteies secheduled for  the employee in the working period 
                            dueAmount = 0
                            Settlments = 0
                            intTransactionTypeID = 0
                            'If intFiscalPeriodId > 0 Then ' User Wana Get Payabilities for given Fiscal Period 
                            If IIf(intFiscalPeriodId > 0, ClsEmpPaySchedule.DataSet.Tables(0).Rows(IntCounter2).Item("DueDate") >= FiscalPFromDate And ClsEmpPaySchedule.DataSet.Tables(0).Rows(IntCounter2).Item("DueDate") <= FiscalPToDate, 1 = 1) Then
                                'If ClsEmpPaySchedule.DataSet.Tables(0).Rows(IntCounter2).Item("DueDate") >= FiscalPFromDate And ClsEmpPaySchedule.DataSet.Tables(0).Rows(IntCounter2).Item("DueDate") <= FiscalPToDate Then
                                If ClsEmpPaySchSettlments.Find(" EmployeePayabilityScheduleID = " & ClsEmpPaySchedule.DataSet.Tables(0).Rows(IntCounter2)(0)) = True Then
                                    DSsettlments = ClsEmpPaySchSettlments.DataSet
                                    For IntCounter3 = 0 To DSsettlments.Tables(0).Rows.Count - 1
                                        Settlments += DSsettlments.Tables(0).Rows(IntCounter3).Item("Amount")
                                    Next
                                Else
                                    Settlments = 0
                                End If

                                For Each RR As Data.DataRow In TransactionTypesDS.Tables(0).Rows
                                    If RR.Item("ID") = DataSet.Tables(0).Rows(IntCounter1).Item("TransactionTypeID") Then
                                        TransactionType = RR.Item("EngName")
                                        intTransactionTypeID = RR.Item("ID")
                                        Sign = CInt(RR.Item("Sign"))
                                        Exit For
                                    End If
                                Next

                                dueAmount = ClsEmpPaySchedule.DataSet.Tables(0).Rows(IntCounter2)(3)
                                If Sign = -1 Then
                                    ArrayPayabiltiesInfo(0, 1) += dueAmount
                                    ArrayPayabiltiesInfo(1, 1) += Settlments
                                Else
                                    ArrayPayabiltiesInfo(0, 0) += dueAmount
                                    ArrayPayabiltiesInfo(1, 0) += Settlments
                                End If
                                NetAmount = dueAmount - Settlments


                                If NetAmount > 0 Then
                                    If SavePaybilities Then
                                        '\\ Save Payabilities To Employee Transaction Detailes (hrs_EmployeesTransactionsDetails) Table 
                                        clsEmployeeTransactionsDet.TransactionTypeID = intTransactionTypeID
                                        clsEmployeeTransactionsDet.EmployeeTransactionID = IntEmployeeTransactionId
                                        clsEmployeeTransactionsDet.NumericValue = NetAmount
                                        clsEmployeeTransactionsDet.TextValue = ""
                                        clsEmployeeTransactionsDet.Save()
                                        'Save Settlments 
                                        ClsEmpPaySchSettlments.Amount = NetAmount
                                        ClsEmpPaySchSettlments.EmployeePayabilityScheduleID = ClsEmpPaySchedule.DataSet.Tables(0).Rows(IntCounter2)(0)
                                        ClsEmpPaySchSettlments.DDate = Now.Date
                                        ClsEmpPaySchSettlments.Save()
                                    End If
                                    PyabilitiesAmount += NetAmount * Sign
                                End If
                            End If
                        Next

                    End If
                Next
            End If
        Catch ex As Exception
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(mPage, "Falied To save Employees Paybilities Data ! ")
        End Try
        ArrayPayabiltiesInfo(2, 0) = ArrayPayabiltiesInfo(0, 0) - ArrayPayabiltiesInfo(1, 0)
        ArrayPayabiltiesInfo(2, 1) = ArrayPayabiltiesInfo(0, 1) - ArrayPayabiltiesInfo(1, 1)
        ArrayPayabiltiesInfo(3, 0) = PyabilitiesAmount

        Return ArrayPayabiltiesInfo
    End Function

    '========================================================================
    'ProcedureName  : Collect Payability
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : Collect all the payability for spacific period  
    'Developer      : DataOcean 
    'Date Created   : 22-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmDependenceType.aspx.vb)TlbMainToolbar_ButtonClicked()
    'To             : SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function GetEmployeesPayabilitySettled(ByVal EmployeeID As Integer, ByVal FiscalYearPeriodID As Integer, ByRef ptrDs As DataSet) As Boolean
        Dim StrSelectCommand As String
        Dim ClsFiscalyearsPeriods As New Clssys_FiscalYearsPeriods(mPage)
        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        '------------------------------=============-----------------------------------------
        Try
            ClsFiscalyearsPeriods.Find("ID=" & FiscalYearPeriodID)
            '-------------------------------0257 MODIFIED-----------------------------------------
            Dim fFromDate As Date
            Dim fToDate As Date
            If clsCompanies.IsHigry Then
                fFromDate = ClsGHCalender.GetRelativeDate(ClsFiscalyearsPeriods.FromDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
                fToDate = ClsGHCalender.GetRelativeDate(ClsFiscalyearsPeriods.ToDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
            Else
                fFromDate = ClsGHCalender.GetRelativeDate(ClsFiscalyearsPeriods.FromDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input)
                fToDate = ClsGHCalender.GetRelativeDate(ClsFiscalyearsPeriods.ToDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input)

            End If
            '------------------------------=============-----------------------------------------

            StrSelectCommand = CONFIG_DATEFORMAT & " Select " & _
                                 "TT.ID     TransactionTypeID, " & _
                                 "dueAmount -IsNull((Select Amount From hrs_EmployeesPayabilitiesSchedulesSettlement HEPSS where  HEPS.ID = HEPSS.EmployeePayabilityScheduleID),0) As Amount, " & _
                                 "'' As Description,sign,isNull(IsPaid,1) as IsPaid " & _
                                 "from " & _
                                 "hrs_EmployeesPayabilities  HEP " & _
                                 "Left	Join hrs_EmployeesPayabilitiesSchedules HEPS On HEP.ID = HEPS.EmployeePayabilityID " & _
                                 "Inner	Join hrs_TransactionsTypes				TT	 On TT.ID  = HEP.TransactionTypeID " & _
                                 "Where " & _
                                 "EmployeeID = " & EmployeeID & _
                                 "And " & _
                                 "DueDate between '" & Format(fFromDate, "dd/MM/yyyy") & "' And '" & Format(fToDate, "dd/MM/yyyy") & "' " & _
                                 "And " & _
                                 "SalaryLink = 1"

            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            ptrDs = New DataSet
            mSqlDataAdapter.Fill(ptrDs)
            If mDataHandler.CheckValidDataObject(ptrDs) Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  : Collect Payability
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : Collect all the payability for spacific period  
    'Developer      : DataOcean 
    'Date Created   : 22-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmDependenceType.aspx.vb)TlbMainToolbar_ButtonClicked()
    'To             : SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function GetEmployeesPayabilityunSettled(ByVal EmployeeID As Integer, ByVal FiscalYearPeriodID As Integer, ByRef ptrDs As DataSet) As Boolean
        Dim StrSelectCommand As String
        Dim ClsFiscalyearsPeriods As New Clssys_FiscalYearsPeriods(mPage)
        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        '------------------------------=============-----------------------------------------

        Try
            ClsFiscalyearsPeriods.Find("ID=" & FiscalYearPeriodID)

            '-------------------------------0257 MODIFIED-----------------------------------------
            Dim fFromDate As Date
            Dim fToDate As Date
            If clsCompanies.IsHigry Then
                fFromDate = ClsGHCalender.GetRelativeDate(ClsFiscalyearsPeriods.FromDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
                fToDate = ClsGHCalender.GetRelativeDate(ClsFiscalyearsPeriods.ToDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
            Else
                fFromDate = ClsGHCalender.GetRelativeDate(ClsFiscalyearsPeriods.FromDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input)
                fToDate = ClsGHCalender.GetRelativeDate(ClsFiscalyearsPeriods.ToDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input)
            End If
            '------------------------------=============-----------------------------------------

            StrSelectCommand = CONFIG_DATEFORMAT & " Select " & _
                                 " TT.ID        As TransactionTypeID, " & _
                                 " dueAmount    As Amount, " & _
                                 " ''           As Description,sign,isNull(IsPaid,1) as IsPaid " & _
                                " From " & _
                                " hrs_EmployeesPayabilities  HEP " & _
                                " Inner	Join hrs_TransactionsTypes				TT	 On TT.ID  = HEP.TransactionTypeID " & _
                                " inner	Join hrs_EmployeesPayabilitiesSchedules		 On hrs_EmployeesPayabilitiesSchedules.EmployeePayabilityid=hep.id " & _
                                " Where Isnull(HEP.CancelDate,'')='' " & _
                                " And " & _
                                " EmployeeID = " & EmployeeID & _
                                " And " & _
                                " DueDate between '" & Format(fFromDate, "dd/MM/yyyy") & "' And '" & Format(fToDate, "dd/MM/yyyy") & "' " & _
                                " And " & _
                                " SalaryLink = 1 "

            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            ptrDs = New DataSet
            mSqlDataAdapter.Fill(ptrDs)
            If mDataHandler.CheckValidDataObject(ptrDs) Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  : Collect Payability
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : Collect all the payability for spacific period  
    'Developer      : DataOcean 
    'Date Created   : 22-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmDependenceType.aspx.vb)TlbMainToolbar_ButtonClicked()
    'To             : SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function GetEmployeesPayabilities(ByVal EmployeeID As Integer, ByVal FiscalYearPeriodID As Integer, ByRef ptrDs As DataSet, ByVal PrepType As String) As Boolean
        Try
            ptrDs = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, "GetEmployeesDuePayablities", EmployeeID, FiscalYearPeriodID, PrepType)
            If mDataHandler.CheckValidDataObject(ptrDs) Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("GetEmployeesDuePayablities", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetEmployeePayabilityBalance(ByVal EmployeeID As Integer) As Double
        Dim retVal As Decimal
        Try
            '-------------------------------0257 MODIFIED-----------------------------------------
            retVal = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, "hrs_GetPayabilitiesBalance", EmployeeID)
            If (IsDBNull(retVal) Or IsNothing(retVal)) Then
                Return 0
            Else
                Return retVal
            End If
            '-------------------------------=============-----------------------------------------


            Return mDataHandler.DataValue_Out(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, "hrs_GetPayabilitiesBalance", EmployeeID), SqlDbType.Decimal)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("hrs_GetPayabilitiesBalance,EmployeeID=" & EmployeeID, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetAllPayabilities(ByVal EmployeeID As Integer) As Double
        Try
            Return mDataHandler.DataValue_Out(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, "hrs_GetAllPayabilities", EmployeeID), SqlDbType.Decimal)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("hrs_GetAllPayabilities,EmployeeID=" & EmployeeID, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetAllPayabilitiesDetails(ByVal EmployeeID As Integer, ByRef Ds As Data.DataSet) As Double
        Try
            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetAllPayabilitiesDetails", EmployeeID)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("hrs_GetAllPayabilities,EmployeeID=" & EmployeeID, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetAllFinalizedPayabilities(ByVal EmployeeID As Integer) As Double
        Try
            Return mDataHandler.DataValue_Out(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, "hrs_GetAllFinalizedPayabilities", EmployeeID), SqlDbType.Decimal)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("hrs_GetAllFinalizedPayabilities,EmployeeID=" & EmployeeID, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :[0257]   
    'Date Created   :22-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmDependenceType.aspx.vb)TlbMainNavigation_ButtonClicked()
    'To             :GetParameter()
    '               :Clear()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' " & IIf(Len(Filter) > 0, " And " & Filter & " ", "") & " and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' " & IIf(Len(Filter) > 0, " And " & Filter & " ", "") & " and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Number ASC"
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
    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')=''  " & IIf(Len(Filter) > 0, " And " & Filter & " ", "") & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')=''  " & IIf(Len(Filter) > 0, " And " & Filter & " ", "") & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Number DESC"
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
    Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' " & IIf(Len(Filter) > 0, " And " & Filter & " ", "") & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Number >" & mNumber & " And isNull(CancelDate,'')='' " & IIf(Len(Filter) > 0, " And " & Filter & " ", "") & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Number ASC"
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
    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' " & IIf(Len(Filter) > 0, " And " & Filter & " ", "") & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Number < " & mNumber & " And isNull(CancelDate,'')='' " & IIf(Len(Filter) > 0, " And " & Filter & " ", "") & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Number DESC"
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

#End Region

#Region "Class Private Function"

    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :22-08-2007
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
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean

        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mNumber = mDataHandler.DataValue_Out(.Item("Number"), SqlDbType.Int)
                mTransactionTypeID = mDataHandler.DataValue_Out(.Item("TransactionTypeID"), SqlDbType.Int, True)
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mSalaryLink = mDataHandler.DataValue_Out(.Item("SalaryLink"), SqlDbType.Bit)
                mTransactionDate = mDataHandler.DataValue_Out(.Item("TransactionDate"), SqlDbType.DateTime)
                mTransactionComment = mDataHandler.DataValue_Out(.Item("TransactionComment"), SqlDbType.VarChar)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mSrc = mDataHandler.DataValue_Out(.Item("Src"), SqlDbType.VarChar)
                mRequestID = mDataHandler.DataValue_Out(.Item("RequestID"), SqlDbType.VarChar)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :22-08-2007
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
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Number", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mNumber, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransactionTypeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTransactionTypeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SalaryLink", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mSalaryLink, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransactionDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mTransactionDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TransactionComment", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTransactionComment, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Src", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSrc, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RequestID", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRequestID, SqlDbType.VarChar)


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

