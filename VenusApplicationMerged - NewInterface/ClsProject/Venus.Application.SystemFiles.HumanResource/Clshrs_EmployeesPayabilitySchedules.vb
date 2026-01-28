'Module            : Hrs (Human Resource Module)
'Date Created      : 22-08-2007
'Developer         : [0257]
'Description       : 1-Implement Data Acess Layer of hrs_EmployeesPayabilitiesSchdules table with fields 
'                    2-Allow searching
'                    3-Get list with all codes
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                    5-Implement functions first(),last(),next() and previous() to allow navigation between 
'                       records
'==========================================================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesPayabilitySchedules
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
        mTable = " hrs_EmployeesPayabilitiesSchedules "
        mInsertParameter = " EmployeePayabilityId,DueDate,DueAmount,Rank,RegDate,RegUserId,RegComputerId,CancelDate,CompanyId "
        mInsertParameterValues = " @EmployeePayabilityId,@DueDate,@DueAmount,@Rank,@RegDate,@RegUserId,@RegComputerId,@CancelDate,@CompanyId "
        mUpdateParameter = " EmployeePayabilityId=@EmployeePayabilityId,DueDate=@DueDate,DueAmount=@DueAmount,Rank=@Rank,RegDate=@RegDate,RegUserId=@RegUserId,RegComputerId=@RegComputerId,CancelDate=@CancelDate,CompanyId=@CompanyId "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region

#Region "Private Members"
    Private mId As Object
    Private mEmployeePayabilityId As Object
    Private mDueDate As Object
    Private mDueAmount As Object
    Private mRank As Object
    Private mRegDate As Object
    Private mRegUserId As Object
    Private mRegComputerId As Object
    Private mCancelDate As Object
    Private mCompanyId As Object
#End Region

#Region "Public property"
    Public Property Id() As Object
        Get
            Return mId
        End Get
        Set(ByVal Value As Object)
            mId = Value
        End Set
    End Property
    Public Property EmployeePayabilityId() As Object
        Get
            Return mEmployeePayabilityId
        End Get
        Set(ByVal Value As Object)
            mEmployeePayabilityId = Value
        End Set
    End Property
    Public Property DueDate() As Object
        Get
            Return mDueDate
        End Get
        Set(ByVal Value As Object)
            mDueDate = Value
        End Set
    End Property
    Public Property DueAmount() As Object
        Get
            Return mDueAmount
        End Get
        Set(ByVal Value As Object)
            mDueAmount = Value
        End Set
    End Property

    Public Property Rank() As Object
        Get
            Return mRank
        End Get
        Set(ByVal Value As Object)
            mRank = Value
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
    Public Property RegUserId() As Object
        Get
            Return mRegUserId
        End Get
        Set(ByVal Value As Object)
            mRegUserId = Value
        End Set
    End Property
    Public Property RegComputerId() As Object
        Get
            Return mRegComputerId
        End Get
        Set(ByVal Value As Object)
            mRegComputerId = Value
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
    Public Property CompanyId() As Object
        Get
            Return mCompanyId
        End Get
        Set(ByVal Value As Object)
            mCompanyId = Value
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
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
            Else
                Clear()
            End If
            If mId > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  FindPayments 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find all Employees Payabilities Schedules certain to employee Payabilitiy
    'Developer      :  [0257]   
    'Date Created   :  22-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function FindPayments(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = " Select hrs_EmployeesPayabilitiesSchedules.Id,hrs_EmployeesPayabilitiesSchedules.dueAmount - IsNull((Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement Where hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.id),0)	As dueAmount,hrs_EmployeesPayabilitiesSchedules.dueDate ,'' as SettlmentID,'' as DocumentNO,'' as ObjectID,'' as objectFileName  from hrs_EmployeesPayabilities inner join hrs_EmployeesPayabilitiesSchedules on hrs_EmployeesPayabilities.Id = hrs_EmployeesPayabilitiesSchedules.EmployeePayabilityId " & IIf(Len(Filter) > 0, " Where " & Filter, " ")
            StrSelectCommand = " Select hrs_EmployeesPayabilitiesSchedules.Id,  hrs_EmployeesPayabilitiesSchedules.dueAmount - IsNull((Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement Where hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.id),0)	 As dueAmount,hrs_EmployeesPayabilitiesSchedules.dueDate,pss.id as SettlmentID,pss.DocumentNO,ott.ObjectID ,ott.FileName objectFileName  from hrs_EmployeesPayabilities inner join hrs_EmployeesPayabilitiesSchedules on hrs_EmployeesPayabilities.Id = hrs_EmployeesPayabilitiesSchedules.EmployeePayabilityId left join hrs_EmployeesPayabilitiesSchedulesSettlement pss on pss.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.id and DocumentNO is null  left join sys_ObjectsAttachments Ott on ott.RecordID =pss.ID and ObjectID=14" & IIf(Len(Filter) > 0, " Where " & Filter, " ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)

            mDataSet = New DataSet

            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FindPayments(ByVal EmployeePayabilityId As Integer) As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = " Select hrs_EmployeesPayabilitiesSchedules.Id,hrs_EmployeesPayabilitiesSchedules.dueAmount - IsNull((Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement Where hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.id),0)	As dueAmount,hrs_EmployeesPayabilitiesSchedules.dueDate ,'' as SettlmentID,'' as DocumentNO,'' as ObjectID,'' as objectFileName  from hrs_EmployeesPayabilities inner join hrs_EmployeesPayabilitiesSchedules on hrs_EmployeesPayabilities.Id = hrs_EmployeesPayabilitiesSchedules.EmployeePayabilityId " & IIf(Len(Filter) > 0, " Where " & Filter, " ")
            'StrSelectCommand = " Select hrs_EmployeesPayabilitiesSchedules.Id,  hrs_EmployeesPayabilitiesSchedules.dueAmount - IsNull((Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement Where hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.id),0)	 As dueAmount,hrs_EmployeesPayabilitiesSchedules.dueDate,pss.id as SettlmentID,pss.DocumentNO,ott.ObjectID ,ott.FileName objectFileName  from hrs_EmployeesPayabilities inner join hrs_EmployeesPayabilitiesSchedules on hrs_EmployeesPayabilities.Id = hrs_EmployeesPayabilitiesSchedules.EmployeePayabilityId left join hrs_EmployeesPayabilitiesSchedulesSettlement pss on pss.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.id and DocumentNO is null  left join sys_ObjectsAttachments Ott on ott.RecordID =pss.ID and ObjectID=14" & IIf(Len(Filter) > 0, " Where " & Filter(), " ")
            'mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            Dim StrCommandStored As String = "loanPayment"

            mDataSet = New DataSet
            mDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, StrCommandStored, EmployeePayabilityId)
            'mDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, "loanPayment", EmployeePayabilityId)
            ' mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  FindCurrentPayment 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find CurrentPayment certain to employee Payabilitiy
    'Developer      :  [0257]   
    'Date Created   :  22-08-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function FindCurrentPayment(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = " Select * from hrs_vwEmployeePayability " & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If Not mDataHandler.CheckValidDataObject(mDataSet) Then
                Clear()
            End If
            If mId > 0 Then
                Return True
            End If
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
            strSQL = "Select ID From hrs_EmployeesPayabilitiesSchedules Where " & Filter
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
    'Calls          :
    'From           :(frmDependenceType.aspx.vb)SavePart()
    'To             :SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
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
            mId = 0
            mEmployeePayabilityId = 0
            mDueDate = Nothing
            mDueAmount = 0.0
            mRank = 0
            mRegDate = Nothing
            mRegUserId = 0
            mRegComputerId = 0
            mCancelDate = Nothing
            mCompanyId = 0

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mId & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mId & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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

    '========================================================================
    'ProcedureName  :  GetTotalAmount
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get total amount to specific EmployeePayablility
    'Developer      :[0257]   
    'Date Created   :22-08-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'EmployeePayableId  :int        
    '========================================================================
    Public Function GetTotalAmount(ByVal EmployeePayableId As Integer) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement Where hrs_EmployeesPayabilitiesSchedulesSettlement.EmployeePayabilityScheduleID=hrs_EmployeesPayabilitiesSchedules.id"
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
                mId = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mEmployeePayabilityId = mDataHandler.DataValue_Out(.Item("EmployeePayabilityID"), SqlDbType.Int, True)
                mDueDate = mDataHandler.DataValue_Out(.Item("DueDate"), SqlDbType.DateTime)
                mDueAmount = mDataHandler.DataValue_Out(.Item("DueAmount"), SqlDbType.Real)
                mRank = mDataHandler.DataValue_Out(.Item("Rank"), SqlDbType.Int)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mRegUserId = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerId = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mCompanyId = mDataHandler.DataValue_Out(.Item("CompanyID"), SqlDbType.Int, True)
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeePayabilityId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeePayabilityId, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DueDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mDueDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DueAmount", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mDueAmount, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRank, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mRegDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegUserId, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerId, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyId", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mMainCompanyID, SqlDbType.Int, True)
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

