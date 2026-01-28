'Project           : Venus V.
'Module            : Hrs (Human Resource Module)
'Date Created      : 05-08-20007
'Developer         : [0257]
'Description       : 1-Implement Data Acess Layer of hrs_TimeAttendants table 
'                    2-Allow searching
'                    3-Get list with all codes
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                   
'==========================================================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_TimeAttendants
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    : Initialize insert ,update and delete commands
    'Developer      :[0257]   
    'Date Created   :04-09-2007
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_TimeAttendants "
        mInsertParameter = " EmployeeID,CheckIn,CheckOut,Delay,OverTime,Minus,CostCenterID,IsOverTime,Remarks,RegUserID,RegComputerID "
        mInsertParameterValues = " @EmployeeID,@CheckIn,@CheckOut,@Delay,@OverTime,@Minus,@CostCenterID,@IsOverTime,@Remarks,@RegUserID,@RegComputerID "
        mUpdateParameter = " EmployeeID=@EmployeeID,CheckIn=@CheckIn,CheckOut=@CheckOut,Delay=@Delay,OverTime=@OverTime,Minus=@Minus,CostCenterID=@CostCenterID,IsOverTime=@IsOverTime,Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID"
        mSelectCommand = CONFIG_DATEFORMAT & " Select hrs_TimeAttendants.ID,hrs_TimeAttendants.EmployeeID,hrs_TimeAttendants.CheckIn,hrs_TimeAttendants.CheckOut,hrs_TimeAttendants.Delay,hrs_TimeAttendants.OverTime,hrs_TimeAttendants.Minus,hrs_TimeAttendants.CostCenterID,hrs_TimeAttendants.IsOverTime,hrs_TimeAttendants.Remarks,hrs_TimeAttendants.RegUserID,hrs_TimeAttendants.RegComputerID,hrs_TimeAttendants.RegDate,hrs_TimeAttendants.CancelDate From  " & mTable '& " INNER JOIN hrs_Employees ON hrs_TimeAttendants.EmployeeID=hrs_Employees.ID"
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
        'mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        'mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        'mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Object

    Private mEmployeeID As Integer
    Private mCheckIn As DateTime
    Private mCheckOut As DateTime
    Private mDelay As Integer
    Private mOverTime As Integer
    Private mMinus As Integer
    Private mCostCenterID As Integer
    Private mIsOverTime As Boolean
    'Private mEmployeeName As String


    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
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
    Public Property CheckIn() As DateTime
        Get
            Return mCheckIn
        End Get
        Set(ByVal Value As DateTime)
            mCheckIn = Value
        End Set
    End Property
    Public Property CheckOut() As DateTime
        Get
            Return mCheckOut
        End Get
        Set(ByVal Value As DateTime)
            mCheckOut = Value
        End Set
    End Property
    Public Property Delay() As Integer
        Get
            Return mDelay
        End Get
        Set(ByVal Value As Integer)
            mDelay = Value
        End Set
    End Property
    Public Property OverTime() As Integer
        Get
            Return mOverTime
        End Get
        Set(ByVal Value As Integer)
            mOverTime = Value
        End Set
    End Property
    Public Property Minus() As Integer
        Get
            Return mMinus
        End Get
        Set(ByVal Value As Integer)
            mMinus = Value
        End Set
    End Property
    Public Property CostCenterID() As Integer
        Get
            Return mCostCenterID
        End Get
        Set(ByVal Value As Integer)
            mCostCenterID = Value
        End Set
    End Property
    Public Property IsOverTime() As Boolean
        Get
            Return mIsOverTime
        End Get
        Set(ByVal Value As Boolean)
            mIsOverTime = Value
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
#End Region
#Region "Public Function"

    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  [0257]   
    'Date Created   :  04-09-2007
    'Modifacations  :
    'Calls          :
    'From           :(frmEmployeesAttendance.aspx.vb)LoadDG
    'To             :GetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean


        '-------------------------------0257 MODIFIED-----------------------------------------
        While (Filter.IndexOf("ã") > -1)

            Filter = Filter.Replace("ã", " PM ")
        End While

        While Filter.IndexOf("Õ") > -1
            Filter = Filter.Replace("Õ", " AM ")
        End While

        '-------------------------------=============-----------------------------------------




        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    Public Function FindInnerJoinEmployees(ByVal Filter As String) As Boolean


        '-------------------------------0257 MODIFIED-----------------------------------------
        While (Filter.IndexOf("ã") > -1)

            Filter = Filter.Replace("ã", " PM ")
        End While

        While Filter.IndexOf("Õ") > -1
            Filter = Filter.Replace("Õ", " AM ")
        End While

        '-------------------------------=============-----------------------------------------




        Dim StrSelectCommand As String
        Try
            StrSelectCommand = CONFIG_DATEFORMAT & " Select hrs_TimeAttendants.ID,hrs_TimeAttendants.EmployeeID,hrs_TimeAttendants.CheckIn,hrs_TimeAttendants.CheckOut,hrs_TimeAttendants.CostCenterID,hrs_TimeAttendants.IsOverTime,hrs_TimeAttendants.Remarks,hrs_TimeAttendants.RegUserID,hrs_TimeAttendants.RegComputerID,hrs_TimeAttendants.RegDate,hrs_TimeAttendants.CancelDate From  " & mTable & " INNER JOIN hrs_Employees ON hrs_TimeAttendants.EmployeeID=hrs_Employees.ID "
            StrSelectCommand = StrSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(hrs_TimeAttendants.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_TimeAttendants.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(hrs_TimeAttendants.CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(hrs_TimeAttendants.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Order by hrs_Employees.Code")
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
    'ProcedureName  :  Save
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save new record
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :04-09-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmEmployeesAttendance.aspx.vb)fnSaveFromDG()
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

            'Dim par = "EmployeeID,CheckIn"
            'Dim val = " " & mEmployeeID & ",'" & mCheckIn & "'"

            'If (mCheckOut <> Nothing) Then
            '    par = par & ", CheckOut"
            '    val = val & ",'" & mCheckOut & "'"
            'End If

            'If (mCostCenterID <> Nothing) Then
            '    par = par & ", CostCenterID"
            '    val = val & "," & mCostCenterID & ""
            'End If

            'If (mIsOverTime = True) Then
            '    par = par & ", IsOverTime"
            '    val = val & "," & 1 & ""
            'Else
            '    par = par & ", IsOverTime"
            '    val = val & "," & 0 & ""
            'End If


            'If (mRemarks <> Nothing) Then
            '    par = par & ", Remarks"
            '    val = val & ",'" & mRemarks & "'"
            'End If
            'par = par & ", Remarks"
            'val = val & ",'Loa'y'"

            'If (mRegUserID <> Nothing) Then
            '    par = par & ", RegUserID"
            '    val = val & "," & mRegUserID & ""
            'End If

            'If (mRegComputerID <> Nothing) Then
            '    par = par & ", RegComputerID"
            '    val = val & "," & mRegComputerID & ""
            'End If



            'Dim nInsertCmd = "Set DateFormat dmy  insert into  hrs_TimeAttendants (  EmployeeID,CheckIn,CheckOut,CostCenterID,IsOverTime,Remarks,RegUserID,RegComputerID )Values( " & mEmployeeID & ",'" & mCheckIn & "','" & IIf(mCheckOut = Nothing, Nothing, mCheckOut) & "'," & IIf(mCostCenterID = Nothing, Nothing, mCostCenterID) & "," & IIf(mIsOverTime = True, 1, 0) & "," & IIf(mRemarks = Nothing, Nothing, mRemarks) & "," & IIf(mRemarks = Nothing, Nothing, mRemarks) & "," & IIf(mRegComputerID = Nothing, Nothing, mRegComputerID) & " )"
            'Dim nInsertCmd = "Set DateFormat dmy  insert into  hrs_TimeAttendants (" & par & ") values ( " & val & ")"
            'mSqlCommand.CommandText = nInsertCmd

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
    'Date Created   :04-09-2007
    'Modifacations  :
    'Calls          :
    'From           :
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
    'Date Created   :04-09-2007
    'Modifacations  : 
    'Calls          :
    'From           :
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
    'ProcedureName  :  DeleteAll
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Delete record that match critera without set cancel date with value
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :[0257]   
    'Date Created   :04-09-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmEmployeesAttendance.aspx.vb) fnSaveFromDG()
    'To             :SetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function DeleteAll(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = CONFIG_DATEFORMAT & "Delete from " & mTable & IIf(Len(Filter) > 0, " Where " & Filter, "")
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
    'ProcedureName  :  GetAllValidEmployess
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get All employees(emloyeeID and his working hours dependant on his class) have valid contracts in input date 
    'Developer      :[0257]   
    'Date Created   :04-09-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmEmployeesAttendance.aspx.vb) LoadDG()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'currDate             :DateTime     :Input date to compare with it
    '========================================================================
    Public Function GetAllValidEmployess(ByVal currDate As DateTime) As Boolean


        Dim startDay As New DateTime(currDate.Year, currDate.Month, currDate.Day, 0, 0, 0)
        Dim endDay As New DateTime(currDate.Year, currDate.Month, currDate.Day, 23, 59, 59)

        Dim StrSelectCommand As String = CONFIG_DATEFORMAT & " select hrs_Contracts.EmployeeID,hrs_EmployeesClasses.WorkHoursPerDay from hrs_Contracts INNER JOIN hrs_EmployeesClasses ON hrs_Contracts.EmployeeClassID=hrs_EmployeesClasses.ID   where IsNull(hrs_Contracts.CancelDate,'')='' And (( IsNull(hrs_Contracts.EndDate,'')='' AND '" & currDate & "' between hrs_Contracts.StartDate AND getDate() ) OR ('" & currDate & "' between hrs_Contracts.StartDate and hrs_Contracts.EndDate) )"
        mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
        mDataSet = New DataSet
        mSqlDataAdapter.Fill(mDataSet)
        If mDataHandler.CheckValidDataObject(mDataSet) Then
            Return True
        Else
            Return False
        End If
    End Function
    '========================================================================
    'ProcedureName  :  GetAllValidEmployess
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get All employees(emloyeeID and his working hours dependant on his class) have valid contracts in input date 
    'Developer      :[0257]   
    'Date Created   :04-09-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmEmployeesAttendance.aspx.vb) LoadDG()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'currDate             :DateTime     :Input date to compare with it
    '========================================================================
    Public Function GetAllValidEmployess(ByVal fisaclFromDate As Date, ByVal fisaclToDate As Date) As Boolean



        Dim StrSelectCommand As String = CONFIG_DATEFORMAT & " Exec hrs_getvalidemployees '" & Format(fisaclFromDate, "dd/MM/yyyy") & "','" & Format(fisaclToDate, "dd/MM/yyyy") & "' "
        mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
        mDataSet = New DataSet
        mSqlDataAdapter.Fill(mDataSet)
        If mDataHandler.CheckValidDataObject(mDataSet) Then
            Return True
        Else
            Return False
        End If
    End Function

    '========================================================================
    'ProcedureName  :  Clear
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Clear all private attributes in class
    'Developer      :[0257]   
    'Date Created   :04-09-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0

            mEmployeeID = Nothing
            mCheckIn = Nothing
            mCheckOut = Nothing
            mDelay = 0
            mOverTime = 0
            mMinus = 0
            mCostCenterID = Nothing
            'mEmployeeName = String.Empty

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

#End Region
#Region "Class Private Function"

    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :04-09-2007
    'Modifacations  :
    'Calls          :
    'From           :
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
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mCheckIn = mDataHandler.DataValue_Out(.Item("CheckIn"), SqlDbType.DateTime)
                mCheckOut = mDataHandler.DataValue_Out(.Item("CheckOut"), SqlDbType.DateTime)
                mDelay = mDataHandler.DataValue_Out(.Item("Delay"), SqlDbType.Int)
                mOverTime = mDataHandler.DataValue_Out(.Item("OverTime"), SqlDbType.Int)
                mMinus = mDataHandler.DataValue_Out(.Item("Minus"), SqlDbType.Int)
                mCostCenterID = mDataHandler.DataValue_Out(.Item("CostCenterID"), SqlDbType.Int, True)
                mIsOverTime = mDataHandler.DataValue_Out(.Item("IsOverTime"), SqlDbType.Bit)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
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

    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :04-09-2007
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CheckIn", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(Format(mCheckIn, "dd/MM/yyyy hh:mm tt"), SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CheckOut", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(Format(mCheckOut, "dd/MM/yyyy hh:mm tt"), SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Delay", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDelay, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OverTime", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mOverTime, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Minus", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mMinus, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CostCenterID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mCostCenterID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsOverTime", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsOverTime, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
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

