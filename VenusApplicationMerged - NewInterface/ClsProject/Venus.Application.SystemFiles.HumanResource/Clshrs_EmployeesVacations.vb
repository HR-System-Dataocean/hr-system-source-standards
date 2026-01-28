Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesVacations
    Inherits ClsDataAcessLayer

    Public Enum ePrepareType
        BeginOfContract
        EndOfContract
        Normal
    End Enum

    Public Enum ePrepareStage
        Normal
        Vacation
    End Enum
#Region "Class Constructors"

    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  To create new object from class
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifications  : [0257] [16-07-2007]
    '                 1-Replace field EstimatedStartDate with ActualStartDate
    '                 2-Delete Company Id field
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesVacations "
        mInsertParameter = " EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,EmployeeRequestRemarks,IsContracial,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks,RegUserID,RegComputerID,HijriExpectedStartDate,HijriExpectedEndDate,HijriActualStartDate,HijriActualEndDate,OverDueVacation ,TotalBalance,PaidFromBalance,RemainingBalance,vactiondays,OverdueDays,ParentVacationID,ZeroingBalance,Src,VacationRequestID"
        mInsertParameterValues = " @EmployeeID,@VacationTypeID,@ExpectedStartDate,@ExpectedEndDate,@EmployeeRequestRemarks,@IsContracial,@ActualStartDate,@ActualEndDate,@TotalDays,@RemainingDays,@ConsumDays,@Remarks,@RegUserID,@RegComputerID,@HijriExpectedStartDate,@HijriExpectedEndDate,@HijriActualStartDate,@HijriActualEndDate,@OverDueVacation,@TotalBalance,@PaidFromBalance,@RemainingBalance,@vactiondays,@OverdueDays,@ParentVacationID,@ZeroingBalance,@Src,@VacationRequestID "
        mUpdateParameter = " EmployeeID=@EmployeeID,VacationTypeID=@VacationTypeID,ExpectedStartDate=@ExpectedStartDate,ExpectedEndDate=@ExpectedEndDate,EmployeeRequestRemarks=@EmployeeRequestRemarks,IsContracial=@IsContracial,ActualStartDate=@ActualStartDate,ActualEndDate=@ActualEndDate,TotalDays=@TotalDays,RemainingDays=@RemainingDays,ConsumDays=@ConsumDays,Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID,HijriExpectedStartDate=@HijriExpectedStartDate,HijriExpectedEndDate=@HijriExpectedEndDate,HijriActualStartDate=@HijriActualStartDate,HijriActualEndDate=@HijriActualEndDate,OverDueVacation=@OverDueVacation,TotalBalance=@TotalBalance,PaidFromBalance=@PaidFromBalance,RemainingBalance=@RemainingBalance,vactiondays=@vactiondays,OverdueDays=@OverdueDays,ParentVacationID=@ParentVacationID,ZeroingBalance=@ZeroingBalance,Src=@Src,VacationRequestID=@VacationRequestID "

        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    Private mID As Object
    Private mCode As String
    Private mEmployeeID As Object
    Private mVacationTypeID As Object

    Private mExpectedStartDate As Date
    Private mExpectedEndDate As Date
    Private mEmployeeRequestRemarks As String

    Private mIsContracial As Boolean

    Private mActualStartDate As Date
    Private mActualEndDate As Date

    Private mTotalDays As Double
    Private mRemainingDays As Double
    Private mConsumDays As Double

    Private mHijriExpectedStartDate As String
    Private mHijriExpectedEndDate As String
    Private mHijriActualStartDate As String
    Private mHijriActualEndDate As String

    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object

    Private mOverDueVacation As Object
    Private mTotalBalance As Double
    Private mPaidFromBalance As Double
    Private mRemainingBalance As Double

    Private mvactiondays As Double
    Private mOverdueDays As Double
    Private mPaymentTrnID As Object

    Private mParentVacationID As Int16

    Private mZeroingBalance As Boolean

    Private mSrc As String
    Private mVacationRequestID As Object

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
    Public Property EmployeeID() As Object
        Get
            Return mEmployeeID
        End Get
        Set(ByVal Value As Object)
            mEmployeeID = Value
        End Set
    End Property
    Public Property VacationTypeID() As Object
        Get
            Return mVacationTypeID
        End Get
        Set(ByVal Value As Object)
            mVacationTypeID = Value
        End Set
    End Property
    Public Property ActualStartDate() As Date
        Get
            Return mActualStartDate
        End Get
        Set(ByVal Value As Date)
            mActualStartDate = Value
        End Set
    End Property
    Public Property ExpectedStartDate() As Date
        Get
            Return mExpectedStartDate
        End Get
        Set(ByVal Value As Date)
            mExpectedStartDate = Value
        End Set
    End Property
    Public Property ExpectedEndDate() As Date
        Get
            Return mExpectedEndDate
        End Get
        Set(ByVal Value As Date)
            mExpectedEndDate = Value
        End Set
    End Property
    Public Property ActualEndDate() As Date
        Get
            Return mActualEndDate
        End Get
        Set(ByVal Value As Date)
            mActualEndDate = Value
        End Set
    End Property
    Public Property EmployeeRequestRemarks() As String
        Get
            Return mEmployeeRequestRemarks
        End Get
        Set(ByVal Value As String)
            mEmployeeRequestRemarks = Value
        End Set
    End Property
    Public Property TotalDays() As String
        Get
            Return mTotalDays
        End Get
        Set(ByVal Value As String)
            mTotalDays = Value
        End Set
    End Property
    Public Property RemainingDays() As Double
        Get
            Return mRemainingDays
        End Get
        Set(ByVal Value As Double)
            mRemainingDays = Value
        End Set
    End Property
    Public Property ConsumDays() As String
        Get
            Return mConsumDays
        End Get
        Set(ByVal Value As String)
            mConsumDays = Value
        End Set
    End Property
    Public Property IsContracial() As Boolean
        Get
            Return mIsContracial
        End Get
        Set(ByVal value As Boolean)
            mIsContracial = value
        End Set
    End Property
    Public Property HijriExpectedStartDate() As String
        Get
            Return mHijriExpectedStartDate
        End Get
        Set(ByVal Value As String)
            mHijriExpectedStartDate = Value
        End Set
    End Property
    Public Property HijriExpectedEndDate() As String
        Get
            Return mHijriExpectedEndDate
        End Get
        Set(ByVal Value As String)
            mHijriExpectedEndDate = Value
        End Set
    End Property
    Public Property HijriActualStartDate() As String
        Get
            Return mHijriActualStartDate
        End Get
        Set(ByVal Value As String)
            mHijriActualStartDate = Value
        End Set
    End Property
    Public Property HijriActualEndDate() As String
        Get
            Return mHijriActualEndDate
        End Get
        Set(ByVal Value As String)
            mHijriActualEndDate = Value
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
    Public Property OverDueVacation() As Object
        Get
            Return mOverDueVacation
        End Get
        Set(ByVal Value As Object)
            mOverDueVacation = Value
        End Set
    End Property
    Public Property PaidFromBalance() As String
        Get
            Return mPaidFromBalance
        End Get
        Set(ByVal Value As String)
            mPaidFromBalance = Value
        End Set
    End Property
    Public Property RemainingBalance() As Double
        Get
            Return mRemainingBalance
        End Get
        Set(ByVal Value As Double)
            mRemainingBalance = Value
        End Set
    End Property
    Public Property TotalBalance() As String
        Get
            Return mTotalBalance
        End Get
        Set(ByVal Value As String)
            mTotalBalance = Value
        End Set
    End Property
    Public Property vactiondays() As String
        Get
            Return mvactiondays
        End Get
        Set(ByVal Value As String)
            mvactiondays = Value
        End Set
    End Property
    Public Property OverdueDays() As String
        Get
            Return mOverdueDays
        End Get
        Set(ByVal Value As String)
            mOverdueDays = Value
        End Set
    End Property
    Public Property PaymentTrnsID() As Object
        Get
            Return mPaymentTrnID
        End Get
        Set(ByVal Value As Object)
            mPaymentTrnID = Value
        End Set
    End Property

    Public Property ParentVacationID() As Integer
        Get
            Return mParentVacationID
        End Get
        Set(ByVal Value As Integer)
            mParentVacationID = Value
        End Set
    End Property

    Public Property ZeroingBalance() As Boolean
        Get
            Return mZeroingBalance
        End Get
        Set(ByVal Value As Boolean)
            mZeroingBalance = Value
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
    Public Property VacationRequestID() As Integer
        Get
            Return mVacationRequestID
        End Get
        Set(ByVal Value As Integer)
            mVacationRequestID = Value
        End Set
    End Property


#End Region

#Region "Public Function"

    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find all rows in hrs_EmployeesVacations that match criteria or filter 
    '                   and canceldate = null
    '               Steps:
    '                   1-Set Select Statment
    '                   2-Intialize DataSet And Adapter with Select statment and connection
    '                   3-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '                   4-Clear all private members of the class if data not valid
    '                   5-Return true if ID of Filteration >0 (Is Found)
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifications  : [0257] [16-07-2007]
    '                 1-Replace field EstimatedStartDate with ActualStartDate
    '                 2-Delete Company Id field
    '                   from select statment
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter:String:used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = StrSelectCommand.Replace("ã", " PM ")
            StrSelectCommand = StrSelectCommand.Replace("Õ", " AM ")
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
    Public Function GetExcepectedVacStartDate(ByVal intEmployeeID As Integer, ByVal intVactionTypeID As Integer) As DateTime
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "select 	top 1*  from hrs_Employeesvacations where IsNull(CancelDate,'')='' " & _
            "	And EmployeeID = " & intEmployeeID & "	And VacationTypeID = " & intVactionTypeID & " order by ActualStartDate desc"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
            Else
                Clear()
            End If
            If mID > 0 Then
                If mActualEndDate.Year = 1 Then
                    Return mExpectedEndDate
                Else
                    If Date.Compare(mActualEndDate, mExpectedEndDate) = -1 Then
                        Return mExpectedEndDate
                    Else
                        Return mActualEndDate
                    End If
                End If

            Else
                Return Nothing
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function


    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find all rows in hrs_EmployeesVacations that match criteria or filter 
    '                   and canceldate = null
    '               Steps:
    '                   1-Set Select Statment
    '                   2-Intialize DataSet And Adapter with Select statment and connection
    '                   3-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '                   4-Clear all private members of the class if data not valid
    '                   5-Return true if ID of Filteration >0 (Is Found)
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifications  : [0257] [16-07-2007]
    '                 1-Replace field EstimatedStartDate with ActualStartDate
    '                 2-Delete Company Id field
    '                   from select statment
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter:String:used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function FindVacations(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = CONFIG_DATEFORMAT & " Select hrs_EmployeesVacations.ActualStartDate,hrs_EmployeesVacations.ActualEndDate,hrs_EmployeesVacations.Id  As VacationId, hrs_EmployeesVacations.ZeroingBalance ,hrs_Employees.* from hrs_Employees inner join hrs_EmployeesVacations on hrs_Employees.id = hrs_EmployeesVacations.Employeeid    " & IIf(Len(Filter) > 0, " Where IsNull(hrs_EmployeesVacations.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesVacations.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(hrs_EmployeesVacations.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesVacations.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                Return True
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
    'Add by: Hassan Kurdi
    'Date: 2021-09-01

    Public Function GetEmployeeLastVacation(EmployeeID As Integer) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = " SELECT TOP 1 * FROM " & mTable & " WHERE EmployeeID = " & EmployeeID &
                                 " AND CancelDate IS NULL  AND VacationTypeID IN (SELECT ID FROM hrs_VacationsTypes WHERE IsAnnual = 1)  ORDER BY ActualStartDate DESC"

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
    'ProcedureName  :  FindEmployeeVacations 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find all rows in hrs_EmployeesVacations inner join with vacations types that match criteria or filter 
    '                   and canceldate = null
    '               Steps:
    '                   1-Set Select Statment
    '                   2-Intialize DataSet And Adapter with Select statment and connection
    '                   3-Set the results(Columns) of Dataset in Private members of the class by calling Getparameter Function
    '                   4-Clear all private members of the class if data not valid
    '                   5-Return true if ID of Filteration >0 (Is Found)
    'Developer      :  DataOcean
    'Date Created   :  [29-07-2007]
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter:String:used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function FindEmployeeVacations(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            Filter = Filter.Replace("Õ", "AM").Replace("ã", "PM")
            StrSelectCommand = CONFIG_DATEFORMAT & " SELECT  hrs_EmployeesVacations.ID, hrs_EmployeesVacations.EmployeeID, hrs_EmployeesVacations.VacationTypeID, hrs_EmployeesVacations.ExpectedStartDate,hrs_EmployeesVacations.ExpectedEndDate ,hrs_EmployeesVacations.ActualStartDate, hrs_EmployeesVacations.ZeroingBalance, hrs_EmployeesVacations.ActualEndDate, hrs_VacationsTypes.EngName FROM hrs_EmployeesVacations INNER JOIN   hrs_VacationsTypes ON hrs_EmployeesVacations.VacationTypeID = hrs_VacationsTypes.ID " & IIf(Len(Filter) > 0, " Where IsNull(hrs_EmployeesVacations.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesVacations.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(hrs_EmployeesVacations.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesVacations.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    '========================================================================
    'ProcedureName  :  SaveUpdate
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save Or Update Row that match with critera in hrs_EmployeesVacations table 
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '    Filter         : String    : used as critera in select statment like 'ID=2'
    ' 
    '========================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_EmployeesVacations Where " & Filter
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
    'Description    :  Save new row into hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

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
    'ProcedureName  :  SaveVacation
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save new row into hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [08-05-2008]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    Public Function SaveVacation() As Integer

        Dim insertCommand As String = CONFIG_DATEFORMAT & "  insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ") ; select @@identity "

        Try
            Dim recordId As Integer
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = insertCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            recordId = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            Return recordId
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
    'Description    :  update row that match with critera into hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
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
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
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
                Case "BIT"
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
                Case "BOOLEAN"
                    If obj Is DBNull.Value Then
                        Return False
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
    'Description    :  Delete new row that match critera into hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
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
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEmployeeID = 0
            mVacationTypeID = 0
            mExpectedStartDate = Nothing
            mExpectedEndDate = Nothing
            mActualStartDate = Nothing
            mActualEndDate = Nothing
            mEmployeeRequestRemarks = String.Empty
            mIsContracial = False
            mTotalDays = 0
            mRemainingDays = 0
            mConsumDays = 0
            mHijriExpectedStartDate = String.Empty
            mHijriExpectedEndDate = String.Empty
            mHijriActualStartDate = String.Empty
            mHijriActualEndDate = String.Empty
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mTotalBalance = 0
            mPaidFromBalance = 0
            mRemainingBalance = 0
            mvactiondays = 0
            mOverdueDays = 0
            mZeroingBalance = False
            mSrc = String.Empty
            mVacationRequestID = 0

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  FirstRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get first record in hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
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

    '========================================================================
    'ProcedureName  :  LastRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get last record in hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
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

    '========================================================================
    'ProcedureName  :  NextRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get next record  from hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
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

    '========================================================================
    'ProcedureName  :  previousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get previous record from hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
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


    '========================================================================
    'ProcedureName  :  FirstRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get first record in hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function FirstRecord(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & IIf(Len(Filter) > 0, " Where " & Filter & " And isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC", " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC")
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

    '========================================================================
    'ProcedureName  :  LastRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get last record in hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function LastRecord(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & IIf(Len(Filter) > 0, " Where " & Filter & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC", " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC")
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

    '========================================================================
    'ProcedureName  :  NextRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get next record  from hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function NextRecord(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & IIf(Len(Filter) > 0, " Where " & Filter & " AND ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC", " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC")
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

    '========================================================================
    'ProcedureName  :  previousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get previous record from hrs_EmployeesVacations table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name  : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function previousRecord(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & IIf(Len(Filter) > 0, " Where " & Filter & " AND ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC", " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC")
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

    '========================================================================
    'ProcedureName  :  GetVacationsDays
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  
    'Developer      :   
    'Date Created   :  24-08-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name  : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function GetVacationsDays(ByVal EmployeeId As Integer, Optional ByVal FisicalPeriodId As Integer = 0) As Single
        Dim FisicalStartDate As Date = Nothing
        Dim FisicalEndDate As Date = Nothing
        Dim VacationDays As Single
        Dim VacationEndDate As Date = Nothing
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(mPage)
        '!=! Get Fisical Period Dates 
        If FisicalPeriodId = 0 Then
            ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")
        Else
            ClsFisicalYearsPeriods.Find(" sys_FiscalYearsPeriods.Id = " & FisicalPeriodId)
        End If
        If ClsFisicalYearsPeriods.ID <= 0 Then
            Return 0
        End If

        FisicalStartDate = ClsFisicalYearsPeriods.FromDate
        FisicalEndDate = ClsFisicalYearsPeriods.ToDate
        '===========================================================================================
        'Get AllVacations For Current Employee 
        If Find(" EmployeeId = " & EmployeeId & " And ActualStartDate Is Not Null ") Then
            If ID > 0 Then
                For Each row As Data.DataRow In mDataSet.Tables(0).Rows
                    If row.Item("ActualEndDate") Is DBNull.Value Then
                        VacationEndDate = Now.Date.ToShortDateString
                    Else
                        VacationEndDate = row.Item("ActualEndDate")
                    End If

                    If row.Item("ActualStartDate") <= FisicalStartDate And VacationEndDate >= FisicalEndDate Then
                        VacationDays += DateDiff(DateInterval.Day, FisicalStartDate, FisicalEndDate.AddDays(1))

                        'OutSide Left 
                    ElseIf VacationEndDate < FisicalStartDate Then
                        VacationDays = VacationDays
                    ElseIf row.Item("ActualStartDate") > FisicalEndDate Then
                        VacationDays = VacationDays
                        'Within Period 
                    ElseIf row.Item("ActualStartDate") > FisicalStartDate And (row.Item("ActualStartDate") < FisicalEndDate _
                       And VacationEndDate < FisicalEndDate) Then
                        VacationDays += DateDiff(DateInterval.Day, row.Item("ActualStartDate"), VacationEndDate.AddDays(1))
                        'Intersect Including Start Period Date
                    ElseIf row.Item("ActualStartDate") < FisicalStartDate And VacationEndDate > FisicalStartDate _
                       And VacationEndDate < FisicalEndDate Then
                        VacationDays += DateDiff(DateInterval.Day, FisicalStartDate, VacationEndDate.AddDays(1))
                        'Intersect Including End Period Date
                    ElseIf (row.Item("ActualStartDate") > FisicalStartDate And row.Item("ActualStartDate") < FisicalEndDate) _
                       And (VacationEndDate > FisicalStartDate And VacationEndDate > FisicalEndDate) Then
                        VacationDays += DateDiff(DateInterval.Day, row.Item("ActualStartDate"), FisicalEndDate.AddDays(1))
                    End If
                Next
            Else
                Return 0
            End If
        End If
        Return VacationDays
    End Function

    '========================================================================
    'ProcedureName  :  GetVacationsDaysDetailed
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  
    'Developer      :   
    'Date Created   :  24-08-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name  : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function GetVacationsDaysDetailed(ByVal EmployeeId As Integer, Optional ByVal FisicalPeriodId As Integer = 0) As ArrayList
        Dim DateArrList As New ArrayList
        DateArrList.Clear()
        Dim FisicalStartDate As Date = Nothing
        Dim FisicalEndDate As Date = Nothing
        Dim VacationDays As Single
        Dim VacationEndDate As Date = Nothing
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(mPage)
        '!=! Get Fisical Period Dates 
        If FisicalPeriodId = 0 Then
            ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")
        Else
            ClsFisicalYearsPeriods.Find(" sys_FiscalYearsPeriods.Id = " & FisicalPeriodId)
        End If
        If ClsFisicalYearsPeriods.ID <= 0 Then
            'Return False
            Exit Function
        End If
        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & clsCompanies.MainCompanyID)
        Dim Clssys_GHCalendar As New Clssys_GHCalendar(mPage)
        '------------------------------=============-----------------------------------------
        FisicalStartDate = ClsFisicalYearsPeriods.FromDate
        FisicalEndDate = ClsFisicalYearsPeriods.ToDate

        '-------------------------------0257 MODIFIED-----------------------------------------

        If clsCompanies.IsHigry Then
            FisicalStartDate = Clssys_GHCalendar.GetRelativeDate(FisicalStartDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
            FisicalEndDate = Clssys_GHCalendar.GetRelativeDate(FisicalEndDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
        Else
            FisicalStartDate = Clssys_GHCalendar.GetRelativeDate(FisicalStartDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input)
            FisicalEndDate = Clssys_GHCalendar.GetRelativeDate(FisicalEndDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input)

        End If
        '------------------------------=============----------------------------------------

        '===========================================================================================
        'Get AllVacations For Current Employee 
        If Find(" EmployeeId = " & EmployeeId & " And ActualStartDate Is Not Null ") Then
            If ID > 0 Then
                For Each row As Data.DataRow In mDataSet.Tables(0).Rows
                    If row.Item("ActualEndDate") Is DBNull.Value Then
                        VacationEndDate = Now.Date.ToShortDateString
                    Else
                        VacationEndDate = row.Item("ActualEndDate")
                    End If

                    If row.Item("ActualStartDate") <= FisicalStartDate And VacationEndDate >= FisicalEndDate Then
                        VacationDays += DateDiff(DateInterval.Day, FisicalStartDate, FisicalEndDate)
                        Dim dateVar1 As Date = FisicalStartDate
                        Dim dateVar2 As Date = FisicalEndDate
                        While dateVar2 >= dateVar1
                            If DateArrList.IndexOf(dateVar1) > 0 Then
                                dateVar1 = dateVar1.AddDays(1)
                                Continue While
                            Else
                                DateArrList.Add(dateVar1)
                                dateVar1 = dateVar1.AddDays(1)
                            End If
                        End While
                        'OutSide Left 
                    ElseIf VacationEndDate < FisicalStartDate Then
                        VacationDays = VacationDays
                    ElseIf row.Item("ActualStartDate") > FisicalEndDate Then
                        VacationDays = VacationDays
                        'Within Period 
                    ElseIf row.Item("ActualStartDate") > FisicalStartDate And (row.Item("ActualStartDate") < FisicalEndDate _
                       And VacationEndDate < FisicalEndDate) Then
                        VacationDays += DateDiff(DateInterval.Day, row.Item("ActualStartDate"), VacationEndDate)

                        Dim dateVar1 As Date = row.Item("ActualStartDate")
                        Dim dateVar2 As Date = VacationEndDate
                        While dateVar2 >= dateVar1
                            If DateArrList.IndexOf(dateVar1) > 0 Then
                                dateVar1 = dateVar1.AddDays(1)
                                Continue While
                            Else
                                DateArrList.Add(dateVar1)
                                dateVar1 = dateVar1.AddDays(1)
                            End If
                        End While

                        'Intersect Including Start Period Date
                    ElseIf row.Item("ActualStartDate") < FisicalStartDate And VacationEndDate > FisicalStartDate _
                       And VacationEndDate < FisicalEndDate Then
                        VacationDays += DateDiff(DateInterval.Day, FisicalStartDate, VacationEndDate)

                        Dim dateVar1 As Date = FisicalStartDate
                        Dim dateVar2 As Date = VacationEndDate
                        While dateVar2 >= dateVar1
                            If DateArrList.IndexOf(dateVar1) > 0 Then
                                dateVar1 = dateVar1.AddDays(1)
                                Continue While
                            Else
                                DateArrList.Add(dateVar1)
                                dateVar1 = dateVar1.AddDays(1)
                            End If
                        End While
                        'Intersect Including End Period Date
                    ElseIf (row.Item("ActualStartDate") > FisicalStartDate And row.Item("ActualStartDate") < FisicalEndDate) _
                       And (VacationEndDate > FisicalStartDate And VacationEndDate > FisicalEndDate) Then
                        VacationDays += DateDiff(DateInterval.Day, row.Item("ActualStartDate"), FisicalEndDate)

                        Dim dateVar1 As Date = row.Item("ActualStartDate")
                        Dim dateVar2 As Date = FisicalEndDate
                        While dateVar2 >= dateVar1
                            If DateArrList.IndexOf(dateVar1) > 0 Then
                                dateVar1 = dateVar1.AddDays(1)
                                Continue While
                            Else
                                DateArrList.Add(dateVar1)
                                dateVar1 = dateVar1.AddDays(1)
                            End If
                        End While
                    End If
                Next
            Else
                Exit Function
            End If
        End If
        Return DateArrList
    End Function

    '-------------------------------0257 MODIFIED-----------------------------------------
    Public Function GetEmployeeVacationsDaysDetails(ByVal EmployeeId As Integer, ByVal arrList As ArrayList, ByRef VacHours As ArrayList, Optional ByVal FisicalPeriodId As Integer = 0) As ArrayList
        Dim DateArrList As New ArrayList
        DateArrList.Clear()
        Dim FisicalStartDate As Date = Nothing
        Dim FisicalEndDate As Date = Nothing
        Dim VacationDays As Single
        Dim VacationEndDate As Date = Nothing
        Dim VacationStartDate As Date = Nothing
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(mPage)
        '!=! Get Fisical Period Dates 
        If FisicalPeriodId = 0 Then
            ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")
        Else
            ClsFisicalYearsPeriods.Find(" sys_FiscalYearsPeriods.Id = " & FisicalPeriodId)
        End If
        If ClsFisicalYearsPeriods.ID <= 0 Then
            'Return False
            Exit Function
        End If
        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & clsCompanies.MainCompanyID)
        Dim Clssys_GHCalendar As New Clssys_GHCalendar(mPage)

        Dim clsContract As New Clshrs_Contracts(mPage)
        clsContract.Find("ID=" & arrList(1))

        Dim EmployeeClassID As Integer = clsContract.EmployeeClassID
        Dim ClsEmployeeClass As New Clshrs_EmployeeClasses(mPage)
        ClsEmployeeClass.Find(" ID= " & EmployeeClassID)
        Dim StartDayH As Integer = CDate(ClsEmployeeClass.DefultStartTime).Hour
        Dim StartDayM As Integer = CDate(ClsEmployeeClass.DefultStartTime).Minute
        Dim WorkingHours As Single = ClsEmployeeClass.WorkHoursPerDay
        '------------------------------=============-----------------------------------------
        FisicalStartDate = ClsFisicalYearsPeriods.FromDate
        FisicalEndDate = ClsFisicalYearsPeriods.ToDate

        '-------------------------------0257 MODIFIED-----------------------------------------

        If clsCompanies.IsHigry Then
            FisicalStartDate = Clssys_GHCalendar.GetRelativeDate(FisicalStartDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
            FisicalEndDate = Clssys_GHCalendar.GetRelativeDate(FisicalEndDate, Clssys_GHCalendar.DateType.Hijri, Clssys_GHCalendar.Directions.Input)
        Else
            FisicalStartDate = Clssys_GHCalendar.GetRelativeDate(FisicalStartDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input)
            FisicalEndDate = Clssys_GHCalendar.GetRelativeDate(FisicalEndDate, Clssys_GHCalendar.DateType.Gregorian, Clssys_GHCalendar.Directions.Input)

        End If
        '------------------------------=============----------------------------------------

        '===========================================================================================
        'Get AllVacations For Current Employee 
        If Find(" EmployeeId = " & EmployeeId & " And ActualStartDate Is Not Null ") Then
            If ID > 0 Then
                For Each row As Data.DataRow In mDataSet.Tables(0).Rows
                    If row.Item("ActualEndDate") Is DBNull.Value Then
                        VacationEndDate = Now.Date.ToShortDateString
                    Else
                        VacationEndDate = row.Item("ActualEndDate")
                    End If
                    VacationStartDate = row.Item("ActualStartDate")
                    If VacationStartDate <= FisicalStartDate And VacationEndDate >= FisicalEndDate Then
                        VacationDays += DateDiff(DateInterval.Day, FisicalStartDate, FisicalEndDate)
                        Dim dateVar1 As Date = FisicalStartDate
                        Dim dateVar2 As Date = FisicalEndDate
                        While dateVar2 >= dateVar1 Or IsTwoDatesEqual(dateVar2, dateVar1)
                            If DateArrList.IndexOf(dateVar1) > 0 Then
                                dateVar1 = dateVar1.AddDays(1)
                                Continue While
                            Else
                                DateArrList.Add(dateVar1)
                                VacHours.Add(dateVar1 & "_" & WorkingHours)


                                dateVar1 = dateVar1.AddDays(1)
                            End If
                        End While
                        'OutSide Left 
                        'ElseIf VacationEndDate < FisicalStartDate Then
                        '    VacationDays = VacationDays
                        'ElseIf VacationStartDate > FisicalEndDate Then
                        '    VacationDays = VacationDays
                        '    'Within Period 
                    ElseIf (VacationStartDate > FisicalStartDate And VacationStartDate < FisicalEndDate _
                       And VacationEndDate < FisicalEndDate) Then
                        VacationDays += DateDiff(DateInterval.Day, VacationStartDate, VacationEndDate)

                        Dim dateVar1 As Date = VacationStartDate.AddDays(1)
                        Dim dateVar2 As Date = VacationEndDate.AddDays(-1)
                        Dim equalSDate As Date
                        Dim equalEDate As Date

                        Dim equalSDate1 As Date
                        Dim equalEDate1 As Date

                        If IsTwoDatesEqual(VacationStartDate, VacationEndDate) Then
                            DateArrList.Add(VacationStartDate)

                            VacHours.Add(VacationStartDate & "_" & Math.Abs(DateDiff(DateInterval.Minute, VacationStartDate, VacationEndDate)) / 60)
                        Else
                            With VacationStartDate
                                equalSDate = New Date(.Year, .Month, .Day, StartDayH, StartDayM, 0)
                                equalEDate = New Date(.Year, .Month, .Day, StartDayH + WorkingHours, StartDayM, 0)
                            End With

                            With VacationEndDate
                                equalSDate1 = New Date(.Year, .Month, .Day, StartDayH, StartDayM, 0)
                                equalEDate1 = New Date(.Year, .Month, .Day, StartDayH + WorkingHours, StartDayM, 0)
                            End With


                            If (VacationStartDate <= equalSDate) Then
                                DateArrList.Add(equalSDate)
                                VacHours.Add(equalSDate & "_" & WorkingHours)

                            ElseIf (VacationStartDate > equalSDate) Then
                                DateArrList.Add(VacationStartDate)
                                VacHours.Add(VacationStartDate & "_" & Math.Abs(DateDiff(DateInterval.Minute, equalSDate, VacationStartDate)) / 60)

                            End If

                            If VacationEndDate >= equalEDate1 Then
                                DateArrList.Add(VacationEndDate)
                                VacHours.Add(VacationEndDate & "_" & WorkingHours)
                            ElseIf VacationEndDate < equalEDate1 And VacationEndDate > equalSDate1 Then
                                DateArrList.Add(VacationEndDate)
                                VacHours.Add(VacationEndDate & "_" & WorkingHours - (Math.Abs(DateDiff(DateInterval.Minute, equalEDate1, VacationEndDate)) / 60))
                            End If

                            While dateVar2 >= dateVar1 Or IsTwoDatesEqual(dateVar2, dateVar1)

                                If DateArrList.IndexOf(dateVar1) > 0 Then
                                    dateVar1 = dateVar1.AddDays(1)
                                    Continue While
                                Else
                                    DateArrList.Add(dateVar1)
                                    VacHours.Add(dateVar1 & "_" & WorkingHours)
                                    dateVar1 = dateVar1.AddDays(1)
                                End If
                            End While
                        End If
                        'Intersect Including Start Period Date
                    ElseIf VacationStartDate < FisicalStartDate And VacationEndDate > FisicalStartDate _
                       And VacationEndDate < FisicalEndDate Then
                        VacationDays += DateDiff(DateInterval.Day, FisicalStartDate, VacationEndDate)

                        Dim dateVar1 As Date = FisicalStartDate
                        Dim dateVar2 As Date = VacationEndDate.AddDays(-1)
                        Dim equalSDate As Date
                        Dim equalEDate As Date

                        Dim equalSDate1 As Date
                        Dim equalEDate1 As Date


                        With VacationStartDate
                            equalSDate = New Date(.Year, .Month, .Day, StartDayH, StartDayM, 0)
                            equalEDate = New Date(.Year, .Month, .Day, StartDayH + WorkingHours, StartDayM, 0)
                        End With

                        With VacationEndDate
                            equalSDate1 = New Date(.Year, .Month, .Day, StartDayH, StartDayM, 0)
                            equalEDate1 = New Date(.Year, .Month, .Day, StartDayH + WorkingHours, StartDayM, 0)
                        End With

                        If VacationEndDate >= equalEDate1 Then
                            DateArrList.Add(VacationEndDate)
                            VacHours.Add(VacationEndDate & "_" & WorkingHours)
                        ElseIf VacationEndDate < equalEDate1 And VacationEndDate > equalSDate1 Then
                            DateArrList.Add(VacationEndDate)
                            VacHours.Add(VacationEndDate & "_" & WorkingHours - (Math.Abs(DateDiff(DateInterval.Minute, equalEDate1, VacationEndDate)) / 60))
                        End If

                        While dateVar2 >= dateVar1 Or IsTwoDatesEqual(dateVar2, dateVar1)
                            If DateArrList.IndexOf(dateVar1) > 0 Then
                                dateVar1 = dateVar1.AddDays(1)
                                Continue While
                            Else
                                DateArrList.Add(dateVar1)
                                VacHours.Add(dateVar1 & "_" & WorkingHours)
                                dateVar1 = dateVar1.AddDays(1)
                            End If
                        End While
                        'Intersect Including End Period Date
                    ElseIf (VacationStartDate > FisicalStartDate And VacationStartDate < FisicalEndDate) _
                       And (VacationEndDate > FisicalStartDate And VacationEndDate > FisicalEndDate) Then
                        VacationDays += DateDiff(DateInterval.Day, VacationStartDate, FisicalEndDate)

                        Dim dateVar1 As Date = VacationStartDate.AddDays(1)
                        Dim dateVar2 As Date = FisicalEndDate
                        Dim equalSDate As Date
                        Dim equalEDate As Date

                        Dim equalSDate1 As Date
                        Dim equalEDate1 As Date

                        With VacationStartDate
                            equalSDate = New Date(.Year, .Month, .Day, StartDayH, StartDayM, 0)
                            equalEDate = New Date(.Year, .Month, .Day, StartDayH + WorkingHours, StartDayM, 0)
                        End With

                        With VacationEndDate
                            equalSDate1 = New Date(.Year, .Month, .Day, StartDayH, StartDayM, 0)
                            equalEDate1 = New Date(.Year, .Month, .Day, StartDayH + WorkingHours, StartDayM, 0)
                        End With

                        If (VacationStartDate <= equalSDate) Then
                            DateArrList.Add(equalSDate)
                            VacHours.Add(equalSDate & "_" & WorkingHours)

                        ElseIf (VacationStartDate > equalSDate) Then
                            DateArrList.Add(VacationStartDate)
                            VacHours.Add(VacationStartDate & "_" & Math.Abs(DateDiff(DateInterval.Minute, equalSDate, VacationStartDate)) / 60)

                        End If
                        While dateVar2 >= dateVar1 Or IsTwoDatesEqual(dateVar2, dateVar1)
                            If DateArrList.IndexOf(dateVar1) > 0 Then
                                dateVar1 = dateVar1.AddDays(1)
                                Continue While
                            Else
                                DateArrList.Add(dateVar1)
                                VacHours.Add(dateVar1 & "_" & WorkingHours)
                                dateVar1 = dateVar1.AddDays(1)
                            End If
                        End While
                    End If
                Next
            Else
                Exit Function
            End If
        End If
        Return DateArrList
    End Function
    Private Function IsTwoDatesEqual(ByVal Date1 As Date, ByVal Date2 As Date) As Boolean
        If Date1.Year = Date2.Year Then
            If Date1.Month = Date2.Month Then
                If Date1.Day = Date2.Day Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    '-------------------------------=============-----------------------------------------

    '========================================================================
    'ProcedureName  :  GeAnnualVacationDays
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  
    'Developer      :   
    'Date Created   :  02-09-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name  : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function GeAnnualVacationDays(ByVal IntEmployeeId As Integer, ByRef IntFiscalYearId As Integer, Optional ByVal FisicalPeriodId As Integer = 0) As Single
        Dim FisicalStartDate As Date = Nothing
        Dim FisicalEndDate As Date = Nothing
        Dim VacationDays As Single
        Dim StrSelectCommand As String = String.Empty
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(mPage)
        If FisicalPeriodId > 0 Then
            ClsFisicalYearsPeriods.Find(" sys_FiscalYearsPeriods.Id = " & FisicalPeriodId)
        Else
            ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")
        End If
        FisicalEndDate = ClsFisicalYearsPeriods.ToDate
        IntFiscalYearId = ClsFisicalYearsPeriods.FiscalYearID
        ClsFisicalYearsPeriods.Find(" sys_FiscalYearsPeriods.FiscalYearID = " & IntFiscalYearId & " Order By FromDate ")
        FisicalStartDate = ClsFisicalYearsPeriods.FromDate
        '=
        StrSelectCommand = " Select IsNull(Sum (DateDiff(dd,ActualStartDate ,IsNull(ActualEndDate,getDate()))),0) From hrs_employeesvacations Inner Join hrs_vacationsTypes On hrs_EmployeesVacations.VacationTypeId = hrs_VacationsTypes.Id Where hrs_VacationsTypes.IsAnnual = 1 And EmployeeID = " & IntEmployeeId
        VacationDays = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, StrSelectCommand)
        Return VacationDays
    End Function


    Public Function GeAllVacationDaysByVacType(ByVal IntEmployeeId As Integer, ByVal IntFiscalYearId As Integer, ByVal intVacType As Integer, ByVal fiscalEndDate As Date) As Single
        Dim FisicalStartDate As Date = Nothing
        Dim FisicalEndDate As Date = Nothing
        Dim VacationDays As Single
        Dim StrSelectCommand As String = String.Empty
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(mPage)


        ClsFisicalYearsPeriods.Find(" sys_FiscalYearsPeriods.FiscalYearID = " & IntFiscalYearId & " Order By FromDate ")
        FisicalStartDate = ClsFisicalYearsPeriods.FromDate
        '=
        StrSelectCommand = CONFIG_DATEFORMAT & " Select IsNull(Sum (DateDiff(dd,ActualStartDate ,IsNull(ActualEndDate,'" & fiscalEndDate & "'))),0) From hrs_employeesvacations Inner Join hrs_vacationsTypes On hrs_EmployeesVacations.VacationTypeId = hrs_VacationsTypes.Id Where  hrs_VacationsTypes.ID = " & intVacType & " And hrs_employeesvacations.CancelDate is Null And EmployeeID = " & IntEmployeeId & "  And ActualStartDate Between '" & FisicalStartDate & "' and '" & fiscalEndDate & "' and IsNull(ActualEndDate,'" & fiscalEndDate & "') Between '" & FisicalStartDate & "' and '" & fiscalEndDate & "' "

        VacationDays = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, StrSelectCommand)

        Return VacationDays
    End Function

    Public Function GeAllWorkingDaysVacationDaysByVacType(ByVal IntEmployeeId As Integer, ByVal intVacType As Integer, ByVal startDate As Date, ByVal endDate As Date) As Single
        Dim FisicalStartDate As Date = Nothing
        Dim FisicalEndDate As Date = Nothing
        Dim VacationDays As Single
        Dim StrSelectCommand As String = String.Empty

        StrSelectCommand = CONFIG_DATEFORMAT & " Select IsNull(Sum (DateDiff(dd,ActualStartDate ,IsNull(ActualEndDate,'" & endDate & "'))),0) From hrs_employeesvacations Inner Join hrs_vacationsTypes On hrs_EmployeesVacations.VacationTypeId = hrs_VacationsTypes.Id Where hrs_VacationsTypes.ID = " & intVacType & " And EmployeeID = " & IntEmployeeId & "  And ActualStartDate Between '" & startDate & "' and '" & endDate & "' and IsNull(ActualEndDate,'" & endDate & "') Between '" & startDate & "' and '" & endDate & "' "

        VacationDays = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, StrSelectCommand)

        Return VacationDays
    End Function

    Public Function GetTotalWorkingUnitsAndPenalty(ByVal intEmpID As Integer, _
                                                   ByVal intContractID As Integer, _
                                                   ByVal dteFromDate As Date, _
                                                   ByVal dteToDate As Date, _
                                                   ByRef dblWorkingUnit As Double, _
                                                   ByRef dblVacationOverDue As Double, _
                                                   ByRef dblPenaltyDays As Double, _
                                                   ByRef dblDelayDays As Double)

        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, _
                                                                                      "GetEmployeeWorkingUnitsAndPenalty", _
                                                                                        intEmpID, _
                                                                                        intContractID, _
                                                                                        dteFromDate, _
                                                                                        dteToDate)

        If ds.Tables(0).Rows.Count > 0 Then
            dblWorkingUnit = ds.Tables(0).Rows(0)("WorkingUnits")
            dblVacationOverDue = ds.Tables(0).Rows(0)("OverDueVacations")
            dblPenaltyDays = ds.Tables(0).Rows(0)("PenaltyDays")
            dblDelayDays = ds.Tables(0).Rows(0)("DelayDays")
        Else
            dblWorkingUnit = 0
            dblVacationOverDue = 0
            dblPenaltyDays = 0
            dblDelayDays = 0
        End If

    End Function

    Public Function GetAllVacationSlices(ByVal intEmpID As Integer, _
                                           ByVal intContractID As Integer, _
                                           ByVal dteVacationDate As Date) As Data.DataSet

        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, _
                                                                                      "GetAllVacationSlices", _
                                                                                        intEmpID, _
                                                                                        intContractID, _
                                                                                        dteVacationDate)
    End Function

    Public Function GetAllEmployeePreviousVacations(ByVal intEmpID As Integer,
                                           ByVal intContractID As Integer,
                                           ByVal dteVacationDate As Date) As Data.DataSet

        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString,
                                                                                      "GetAllEmployeePreviousVacations",
                                                                                        intEmpID)

    End Function
    'Added by: Hassan kurdi
    'Date: 2021-11-09
    'Purpose: Get Employee Last Vacation Settlement
    Public Function GetEmployeeLastVacationSettlement(ByVal intEmpID As Integer) As Data.DataSet

        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString,
                                                                                      "GetEmployeeLastVacationSettlement",
                                                                                        intEmpID)

    End Function
    'Added: Hassan Kurdi
    'Date: 2022-04-04
    'Purpose: Return vacation per year for an employee
    'Modified by Rabie to get the salary year not current year 
    Public Function GetEmployeeVacationPerYear(PeriodYear As Integer, EmployeeId As Integer, VacationTypeId As Integer) As Data.DataSet
        Dim strcommand As String = "SELECT * FROM hrs_EmployeesVacations WHERE EmployeeID = " & EmployeeId & " AND VacationTypeID = " & VacationTypeId &
                                    " AND CancelDate IS NULL AND (YEAR(ActualStartDate) = " & PeriodYear & " OR YEAR(ActualEndDate) = " & PeriodYear & ")"
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, strcommand)
    End Function
    Public Function GetDefaultVacation(ByVal intEmpID As Integer, _
                                           ByVal intContractID As Integer, ByVal ResetDate As Date?, ByVal UseAddDays As Boolean) As Date
        Try
            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, "GetDefaultVacation", intEmpID, intContractID, ResetDate, UseAddDays)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetDefaultVacationPayment(ByVal intEmpID As Integer, _
                                       ByVal intContractID As Integer, ByVal ResetDate As Date?, ByVal UseAddDays As Boolean) As Date
        Try
            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, _
                                                                                      "GetDefaultVacationPayment", _
                                                                                        intEmpID, intContractID, ResetDate, UseAddDays)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetLastPaidEndDateVacationPayment(ByVal intEmpID As Integer,
                                   ByVal intContractID As Integer, ByVal ResetDate As Date?, ByVal UseAddDays As Boolean) As Date
        Try
            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString,
                                                                                      "GetLastPaidEndDateVacationPayment",
                                                                                        intEmpID, intContractID, ResetDate, UseAddDays)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAllEmployeePreviousVacationsForPeriod(ByVal PeriodID As Integer, ByVal EmployeeID As Integer) As Data.DataSet
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, "GetAllEmployeePreviousVacationsForPeriod", EmployeeID, PeriodID)
    End Function
#End Region

#Region "Class Private Function"

    Private Function CreateDataTable(ByVal DtTable As Data.DataTable, ByVal PtrTableName As String) As Boolean
        Dim ObjDataColumn As New Data.DataColumn
        Try
            DtTable.TableName = PtrTableName

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "TransactionTypeID"
            ObjDataColumn.DataType = Type.GetType("System.Int32")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Amount"
            ObjDataColumn.DataType = Type.GetType("System.Double")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Description"
            ObjDataColumn.DataType = Type.GetType("System.String")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "ID"
            ObjDataColumn.DataType = Type.GetType("System.Int32")
            DtTable.Columns.Add(ObjDataColumn)

        Catch ex As Exception

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 
    '                   
    '               
    '  
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
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mVacationTypeID = mDataHandler.DataValue_Out(.Item("VacationTypeID"), SqlDbType.Int, True)
                mEmployeeRequestRemarks = mDataHandler.DataValue_Out(.Item("EmployeeRequestRemarks"), SqlDbType.VarChar)
                mIsContracial = mDataHandler.DataValue_Out(.Item("IsContracial"), SqlDbType.Bit)
                mExpectedStartDate = mDataHandler.DataValue_Out(.Item("ExpectedStartDate"), SqlDbType.DateTime)
                mExpectedEndDate = mDataHandler.DataValue_Out(.Item("ExpectedEndDate"), SqlDbType.DateTime)
                mActualStartDate = mDataHandler.DataValue_Out(.Item("ActualStartDate"), SqlDbType.DateTime)
                mActualEndDate = mDataHandler.DataValue_Out(.Item("ActualEndDate"), SqlDbType.DateTime)
                mTotalDays = IIf(IsNumeric(.Item("TotalDays")) = True, mDataHandler.DataValue_Out(.Item("TotalDays"), SqlDbType.Decimal), 0)
                mRemainingDays = IIf(IsNumeric(.Item("RemainingDays")) = True, mDataHandler.DataValue_Out(.Item("RemainingDays"), SqlDbType.Decimal), 0)
                mConsumDays = IIf(IsNumeric(.Item("ConsumDays")) = True, mDataHandler.DataValue_Out(.Item("ConsumDays"), SqlDbType.Decimal), 0)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)

                mHijriExpectedStartDate = mDataHandler.DataValue_Out(.Item("HijriExpectedStartDate"), SqlDbType.VarChar)
                mHijriExpectedEndDate = mDataHandler.DataValue_Out(.Item("HijriExpectedEndDate"), SqlDbType.VarChar)
                mHijriActualStartDate = mDataHandler.DataValue_Out(.Item("HijriActualStartDate"), SqlDbType.VarChar)
                mHijriActualEndDate = mDataHandler.DataValue_Out(.Item("HijriActualEndDate"), SqlDbType.VarChar)

                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mOverDueVacation = mDataHandler.DataValue_Out(.Item("OverDueVacation"), SqlDbType.Int, True)
                mTotalBalance = IIf(IsNumeric(.Item("TotalBalance")) = True, mDataHandler.DataValue_Out(.Item("TotalBalance"), SqlDbType.Decimal), 0)
                mPaidFromBalance = IIf(IsNumeric(.Item("PaidFromBalance")) = True, mDataHandler.DataValue_Out(.Item("PaidFromBalance"), SqlDbType.Decimal), 0)
                mRemainingBalance = IIf(IsNumeric(.Item("RemainingBalance")) = True, mDataHandler.DataValue_Out(.Item("RemainingBalance"), SqlDbType.Decimal), 0)
                mvactiondays = IIf(IsNumeric(.Item("vactiondays")) = True, mDataHandler.DataValue_Out(.Item("vactiondays"), SqlDbType.Decimal), 0)
                mOverdueDays = IIf(IsNumeric(.Item("OverdueDays")) = True, mDataHandler.DataValue_Out(.Item("OverdueDays"), SqlDbType.Decimal), 0)
                mPaymentTrnID = mDataHandler.DataValue_Out(.Item("PaymentTrnID"), SqlDbType.Int, True)

                mParentVacationID = mDataHandler.DataValue_Out(.Item("ParentVacationID"), SqlDbType.Int, True)

                mZeroingBalance = mDataHandler.DataValue_Out(.Item("ZeroingBalance"), SqlDbType.Bit, True)

                mSrc = mDataHandler.DataValue_Out(.Item("Src"), SqlDbType.VarChar)
                mVacationRequestID = mDataHandler.DataValue_Out(.Item("VacationRequestID"), SqlDbType.Int, True)

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
    'Description    :  Assign parameters of sqlcommand values with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [16-07-2007]
    'Modifacations  : 

    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean

        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VacationTypeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mVacationTypeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeRequestRemarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEmployeeRequestRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsContracial", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsContracial, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExpectedStartDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mExpectedStartDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExpectedEndDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mExpectedEndDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ActualStartDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mActualStartDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ActualEndDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mActualEndDate, SqlDbType.DateTime)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TotalDays", SqlDbType.Decimal)).Value = mDataHandler.DataValue_In(mTotalDays, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RemainingDays", SqlDbType.Decimal)).Value = mDataHandler.DataValue_In(mRemainingDays, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ConsumDays", SqlDbType.Decimal)).Value = mDataHandler.DataValue_In(mConsumDays, SqlDbType.Decimal)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HijriExpectedStartDate", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHijriExpectedStartDate, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HijriExpectedEndDate", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHijriExpectedEndDate, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HijriActualStartDate", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHijriActualStartDate, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HijriActualEndDate", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHijriActualEndDate, SqlDbType.VarChar)


            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OverDueVacation", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mOverDueVacation, SqlDbType.Int, True)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TotalBalance", SqlDbType.Decimal)).Value = mDataHandler.DataValue_In(mTotalBalance, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PaidFromBalance", SqlDbType.Decimal)).Value = mDataHandler.DataValue_In(mPaidFromBalance, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RemainingBalance", SqlDbType.Decimal)).Value = mDataHandler.DataValue_In(mRemainingBalance, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@vactiondays", SqlDbType.Decimal)).Value = mDataHandler.DataValue_In(mvactiondays, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OverdueDays", SqlDbType.Decimal)).Value = mDataHandler.DataValue_In(mOverdueDays, SqlDbType.Decimal)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ParentVacationID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mParentVacationID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ZeroingBalance", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mZeroingBalance, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Src", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mSrc, SqlDbType.VarChar)


            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@VacationRequestID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mVacationRequestID, SqlDbType.Int)

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

