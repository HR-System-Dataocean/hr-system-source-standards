'Project           : Venus V.
'Module            : Hrs (Human Resource Module)
'Date Created      : 15-08-2007
'Developer         : [0257]
'Description       : 1-Implement Data Acess Layer of hrs_EmployeesTransactionsProjects table with fields code,English name,
'                       Arabic name,Grade level and Regular hours
'                    2-Allow searching
'                    3-Get list with all codes
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                    5-Implement functions first(),last(),next() and previous() to allow navigation between 
'                       records
'==========================================================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesTransactionsProjects
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    : Initialize insert ,update and delete commands
    'Developer      :[0257]   
    'Date Created   :15-08-2007
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesTransactionsProjects "
        mInsertParameter = "" & _
          "EmployeeTransactionID," & _
          "ProjectID," & _
          "WorkingDate," & _
          "WorkingUnits," & _
          "RegUserID," & _
          "RegComputerID," & _
          "OvertimeHours," & _
          "HoliDayOvertimeHours," & _
          "AbsentDays," & _
          "LatHours," & _
          "LinkedCS"
        mInsertParameterValues = "" & _
          " @EmployeeTransactionID," & _
          " @ProjectID," & _
          " @WorkingDate," & _
          " @WorkingUnits," & _
          " @RegUserID," & _
          " @RegComputerID," & _
          " @OvertimeHours," & _
          " @HoliDayOvertimeHours," & _
          " @AbsentDays," & _
          " @LatHours," & _
          " @LinkedCS"
        mUpdateParameter = "" & _
          "EmployeeTransactionID=@EmployeeTransactionID," & _
          "ProjectID=@ProjectID," & _
          "WorkingDate=@WorkingDate," & _
          "WorkingUnits=@WorkingUnits," & _
          "OvertimeHours=@OvertimeHours," & _
          "HoliDayOvertimeHours=@HoliDayOvertimeHours," & _
          "AbsentDays=@AbsentDays," & _
          "LatHours=@LatHours," & _
          "LinkedCS=@LinkedCS"
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mEmployeeTransactionID As Integer
    Private mProjectID As Integer
    Private mWorkingDate As DateTime
    Private mWorkingUnits As Object
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As DateTime
    Private mCancelDate As DateTime
    Private mOvertimeHours As Object
    Private mHoliDayOvertimeHours As Object
    Private mAbsentDays As Object
    Private mLatHours As Object
    Private mLinkedCS As String
#End Region
#Region "Public property"
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal Value As Integer)
            mID = Value
        End Set
    End Property
    Public Property EmployeeTransactionID() As Integer
        Get
            Return mEmployeeTransactionID
        End Get
        Set(ByVal Value As Integer)
            mEmployeeTransactionID = Value
        End Set
    End Property
    Public Property ProjectID() As Integer
        Get
            Return mProjectID
        End Get
        Set(ByVal Value As Integer)
            mProjectID = Value
        End Set
    End Property
    Public Property WorkingDate() As DateTime
        Get
            Return mWorkingDate
        End Get
        Set(ByVal Value As DateTime)
            mWorkingDate = Value
        End Set
    End Property
    Public Property WorkingUnits() As Object
        Get
            Return mWorkingUnits
        End Get
        Set(ByVal Value As Object)
            mWorkingUnits = Value
        End Set
    End Property
    Public Property RegUserID() As Integer
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Integer)
            mRegUserID = Value
        End Set
    End Property
    Public Property RegComputerID() As Integer
        Get
            Return mRegComputerID
        End Get
        Set(ByVal Value As Integer)
            mRegComputerID = Value
        End Set
    End Property
    Public Property RegDate() As DateTime
        Get
            Return mRegDate
        End Get
        Set(ByVal Value As DateTime)
            mRegDate = Value
        End Set
    End Property
    Public Property CancelDate() As DateTime
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As DateTime)
            mCancelDate = Value
        End Set
    End Property
    Public Property OvertimeHours() As Object
        Get
            Return mOvertimeHours
        End Get
        Set(ByVal Value As Object)
            mOvertimeHours = Value
        End Set
    End Property
    Public Property HoliDayOvertimeHours() As Object
        Get
            Return mHoliDayOvertimeHours
        End Get
        Set(ByVal Value As Object)
            mHoliDayOvertimeHours = Value
        End Set
    End Property
    Public Property AbsentDays() As Object
        Get
            Return mAbsentDays
        End Get
        Set(ByVal Value As Object)
            mAbsentDays = Value
        End Set
    End Property
    Public Property LatHours() As Object
        Get
            Return mLatHours
        End Get
        Set(ByVal Value As Object)
            mLatHours = Value
        End Set
    End Property
    Public Property LinkedCS() As String
        Get
            Return mLinkedCS
        End Get
        Set(ByVal Value As String)
            mLinkedCS = Value
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
    'Date Created   :  15-08-2007
    'Modifacations  :
    'Calls          :
    'From           :
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesTransactionsProjects.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  And IsNull(dbo.hrs_GetRecordViewStatus(hrs_EmployeesTransactionsProjects.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
    'ProcedureName  :  SaveUpdate
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save Or Update Row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [0257]   
    'Date Created   :  15-08-2007
    'Modifacations  :
    'Calls          :
    'From           :
    'To             :
    '               :
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
            strSQL = "Select hrs_EmployeesTransactionsProjects.ID From hrs_EmployeesTransactionsProjects Where " & Filter
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
    'Date Created   :15-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :
    'To             :
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
    Public Function Save1() As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            Return mSqlCommand.ExecuteScalar()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            mSqlCommand.Connection.Close()
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Update
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :15-08-2007
    'Modifacations  :
    'Calls          :
    'From           :
    'To             :
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
    'Date Created   :15-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :
    'To             :
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
    'Description    :  Delete record that match critera
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :[0257]   
    'Date Created   :15-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function DeleteAll(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = "Delete from " & mTable & IIf(Len(Filter) > 0, " Where " & Filter, "")
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
    'Date Created   :15-08-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mEmployeeTransactionID = 0
            mProjectID = 0
            mWorkingDate = Nothing
            mWorkingUnits = Nothing
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mOvertimeHours = Nothing
            mHoliDayOvertimeHours = Nothing
            mAbsentDays = Nothing
            mLatHours = Nothing
            mLinkedCS = String.Empty
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
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
    'Date Created   :15-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :
    'To             :
    '               :Clear()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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

    Private Function CheckNumericDBNull(ByVal rowInfo As DataRow, ByVal strKey As String) As Object
        If IsDBNull(rowInfo(strKey)) Then
            Return 0
        Else
            Return rowInfo(strKey)
        End If
    End Function

    Private Function GetTransTypeDetExceptThisProjByTransHead(ByVal TransHeadID As Integer, ByVal TransTypeID As Integer, ByVal ExceptProjectID As Integer) As DataSet
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetTransTypeDetExceptThisProjByTransHead", TransHeadID, TransTypeID, ExceptProjectID)
    End Function

    Private Function GetTransHeadTransTypeSum(ByVal TransHeadID As Integer, ByVal TransTypeID As Integer) As Double
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, "GetTransHeadTransTypeSum", TransHeadID, TransTypeID)
    End Function

    Private Function CreateDataTable(ByVal DtTable As Data.DataTable, ByVal PtrTableName As String) As Boolean

        Dim ObjDataColumn As New Data.DataColumn
        Try
            DtTable.TableName = PtrTableName

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "TransactionTypeID"
            ObjDataColumn.DataType = Global.System.Type.GetType("System.Int32")       'GetType("System.Int32")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Amount"
            ObjDataColumn.DataType = Global.System.Type.GetType("System.Double")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "Description"
            ObjDataColumn.DataType = Global.System.Type.GetType("System.String")
            DtTable.Columns.Add(ObjDataColumn)

            ObjDataColumn = New Data.DataColumn
            ObjDataColumn.ColumnName = "EmpSchID"
            ObjDataColumn.DataType = Global.System.Type.GetType("System.Int32")
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
    'Developer      :[0257]   
    'Date Created   :15-08-2007
    'Modifacations  :
    'Calls          :
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Ds             :DataSet     :used its attributes to assign them to private attributes
    '========================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Try
            With Ds.Tables(0).Rows(0)
                mID = [Shared].DataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mEmployeeTransactionID = [Shared].DataHandler.DataValue_Out(.Item("EmployeeTransactionID"), SqlDbType.Int, True)
                mProjectID = [Shared].DataHandler.DataValue_Out(.Item("ProjectID"), SqlDbType.Int, True)
                mWorkingDate = [Shared].DataHandler.DataValue_Out(.Item("WorkingDate"), SqlDbType.DateTime)
                mWorkingUnits = [Shared].DataHandler.DataValue_Out(.Item("WorkingUnits"), SqlDbType.Real)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mOvertimeHours = [Shared].DataHandler.DataValue_Out(.Item("OvertimeHours"), SqlDbType.Real)
                mHoliDayOvertimeHours = [Shared].DataHandler.DataValue_Out(.Item("HoliDayOvertimeHours"), SqlDbType.Real)
                mAbsentDays = [Shared].DataHandler.DataValue_Out(.Item("AbsentDays"), SqlDbType.Real)
                mLatHours = [Shared].DataHandler.DataValue_Out(.Item("LatHours"), SqlDbType.Real)
                mLinkedCS = [Shared].DataHandler.DataValue_Out(.Item("LinkedCS"), SqlDbType.VarChar)
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
    'Date Created   :15-08-2007
    'Modifacations  :
    'Calls          :
    'From           :
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Dim clsCompanies As New Clssys_Companies(mPage)
        clsCompanies.Find("ID = " & Me.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeTransactionID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mEmployeeTransactionID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ProjectID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mProjectID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@WorkingDate", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mWorkingDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@WorkingUnits", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mWorkingUnits, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OvertimeHours", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mOvertimeHours, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HoliDayOvertimeHours", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mHoliDayOvertimeHours, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AbsentDays", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mAbsentDays, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LatHours", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mLatHours, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LinkedCS", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mLinkedCS, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
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

