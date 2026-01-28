Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesJoins
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Input : 
    'Description: In the constructor of the class set the table name and 
    '           sqlstatment of (Insert,Update,Delete,select) row from the table
    '==================================================================

    Public Sub New(ByVal Page As Web.UI.Page)

        MyBase.New(Page)
        Me.Table = " hrs_EmployeesJoins "
        Me.mInsertParameter = " EmployeeID,JoinDate,EndofServiceID,EndofServiceDate,EndofServiceReson,RegUserID,RegComputerID,EndOfServiceDateText,EOSDays,EOSMonths,EOSYears,EosTotalDays,EosTotalMonths,EosTotalYears,EosOverDueDays,EosOverDueMonths,EosOverDueYears "
        Me.mInsertParameterValues = " @EmployeeID,@JoinDate,@EndofServiceID,@EndofServiceDate,@EndofServiceReson,@RegUserID,@RegComputerID,@EndOfServiceDateText,@EOSDays,@EOSMonths,@EOSYears ,@EosTotalDays,@EosTotalMonths,@EosTotalYears,@EosOverDueDays,@EosOverDueMonths,@EosOverDueYears"
        Me.mUpdateParameter = " EmployeeID=@EmployeeID,JoinDate=@JoinDate,EndofServiceID=@EndofServiceID,EndofServiceDate=@EndofServiceDate,EndofServiceReson=@EndofServiceReson,CancelDate=null,EndOfServiceDateText=@EndOfServiceDateText,EOSDays=@EOSDays,EOSMonths=@EOSMonths,EOSYears=@EOSYears,EosTotalDays=@EosTotalDays,EosTotalMonths=@EosTotalMonths,EosTotalYears=@EosTotalYears,EosOverDueDays=@EosOverDueDays,EosOverDueMonths=@EosOverDueMonths,EosOverDueYears=@EosOverDueYears "
        Me.mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        Me.mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        Me.mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        Me.mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"

    End Sub
#End Region

#Region "Private Members"

    Private mID As Object
    Private mJoinDate As Object
    Private mEmployeeID As Object
    Private mEndofServiceID As Integer
    Private mEndofServiceDate As Object
    Private mEndofServiceReson As String
    Private mRemarks As Object
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mEndOfServiceDateText As String
    Private mEOSDays As Object
    Private mEOSMonths As Object
    Private mEOSYears As Object

    Private mEosTotalDays As Object
    Private mEosTotalMonths As Object
    Private mEosTotalYears As Object

    Private mEosOverDueDays As Object
    Private mEosOverDueMonths As Object
    Private mEosOverDueYears As Object

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

    Public Property JoinDate() As Object
        Get
            Return mJoinDate
        End Get
        Set(ByVal Value As Object)
            mJoinDate = Value
        End Set
    End Property

    Public Property EndofServiceType() As Integer
        Get
            Return mEndofServiceID
        End Get
        Set(ByVal Value As Integer)
            mEndofServiceID = Value
        End Set
    End Property

    Public Property EndofServiceDate() As Object
        Get
            Return mEndofServiceDate
        End Get
        Set(ByVal Value As Object)
            mEndofServiceDate = Value
        End Set
    End Property

    Public Property EndofServiceReson() As String
        Get
            Return mEndofServiceReson
        End Get
        Set(ByVal Value As String)

            mEndofServiceReson = Value
        End Set
    End Property
    Public Property EndOfServiceDateText() As String
        Get
            Return mEndOfServiceDateText
        End Get
        Set(ByVal Value As String)

            mEndOfServiceDateText = Value
        End Set
    End Property

    Public Property EmployeeId() As Integer
        Get
            Return mEmployeeID
        End Get
        Set(ByVal value As Integer)
            mEmployeeID = value
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
    Public Property EOSDays() As Object
        Get
            Return mEOSDays
        End Get
        Set(ByVal Value As Object)
            mEOSDays = Value
        End Set
    End Property
    Public Property EOSMonths() As Object
        Get
            Return mEOSMonths
        End Get
        Set(ByVal Value As Object)
            mEOSMonths = Value
        End Set
    End Property
    Public Property EOSYears() As Object
        Get
            Return mEOSYears
        End Get
        Set(ByVal Value As Object)
            mEOSYears = Value
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


    Public Property EosTotalDays() As Object
        Get
            Return mEosTotalDays
        End Get
        Set(ByVal Value As Object)
            mEosTotalDays = Value
        End Set
    End Property
    Public Property EosTotalMonths() As Object
        Get
            Return mEosTotalMonths
        End Get
        Set(ByVal Value As Object)
            mEosTotalMonths = Value
        End Set
    End Property
    Public Property EosTotalYears() As Object
        Get
            Return mEosTotalYears
        End Get
        Set(ByVal Value As Object)
            mEosTotalYears = Value
        End Set
    End Property



    Public Property EosOverDueDays() As Object
        Get
            Return mEosOverDueDays
        End Get
        Set(ByVal Value As Object)
            mEosOverDueDays = Value
        End Set
    End Property
    Public Property EosOverDueMonths() As Object
        Get
            Return mEosOverDueMonths
        End Get
        Set(ByVal Value As Object)
            mEosOverDueMonths = Value
        End Set
    End Property
    Public Property EosOverDueYears() As Object
        Get
            Return mEosOverDueYears
        End Get
        Set(ByVal Value As Object)
            mEosOverDueYears = Value
        End Set
    End Property

#End Region

#Region "Public Function"
    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Find all columns from FormControl table where filter and canceldate = null  
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
            StrSelectCommand = StrSelectCommand.Replace("Õ", "AM")
            StrSelectCommand = StrSelectCommand.Replace("ã", "PM")
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
    'Date : 26/6/2007
    'Input : Filter as string (ex. ID=2)
    'Description: Save or Update row 
    'Steps: 
    '       1-Execute sql statment to get ID from sys_FormsControls where filter 
    '       2-Check if ID > 0 this mean that row is already exist in sys_FormsControls table 
    '       the make Update to this row
    '           IF ID =0 this mean that row is new row Then Insert the row in sys_FormsControls table
    '==================================================================

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Try
            Dim strSQL As String
            Dim Value As Integer
            strSQL = "Select ID From sys_FormsControls Where " & Filter
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
    'Date : 26/6/2007
    'Input : ID as integer (FormID from sys_Forms table)
    'Description : Execute hrs_SelectEmployeeInfo Stored procedure to Get information about the Employee (Code of Employee, English Name of Employee,Family Name of Employee)
    '              
    '==================================================================
    Public Function GetEmployeeName(ByVal ID As Integer) As DataTable
        Dim ObjSqldataadapter As New SqlClient.SqlDataAdapter("hrs_SelectEmployeeInfo", MyBase.ConnectionString)
        ObjSqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure
        ObjSqldataadapter.SelectCommand.Parameters.AddWithValue("@ID", ID)
        Dim ObjTable As New DataTable()
        Try
            ObjSqldataadapter.Fill(ObjTable)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
        Return ObjTable
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007

    'Description: Save New Row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to insert new row in sys_FormsControls table

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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Update existing Row in sys_FormsControls table where filter
    'Steps: 
    '       1-execute sqlstatment to Update existing row in sys_FormsControls table

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
    'Date : 26/6/2007
    'Inptu : Filter as string (ex.ID=2)
    'Description: Delete existing Row in sys_FormsControls table where filter
    'Steps: 
    '       1-execute sqlstatment to Delete existing row in sys_FormsControls table

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

    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Description: Clear all private members  of the class

    '==================================================================

    Public Function Clear() As Boolean
        Try
            mID = 0
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mEndOfServiceDateText = String.Empty
            mEOSDays = 0
            mEOSMonths = 0
            mEOSYears = 0

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Description:  find first row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to find first row in sys_FormsControls table
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
    'Date : 26/6/2007
    'Description:  find Last row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to find last row in sys_FormsControls table
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
    'Date : 26/6/2007
    'Description:  find Next row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to find Next row in sys_FormsControls table
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
    'Date : 26/6/2007
    'Description:  find previous row in sys_FormsControls table
    'Steps: 
    '       1-execute sqlstatment to find previous row in sys_FormsControls table
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

#End Region

#Region "Class Private Function"
    '===================================================================
    'Created by : [0258]
    'Date : 26/6/2007
    'Input : Ds as Dataset 
    'Description:   Asign the Result of Ds to the private members of the class

    '===================================================================

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean

        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mJoinDate = mDataHandler.DataValue_Out(.Item("JoinDate"), SqlDbType.DateTime)
                mEndofServiceDate = mDataHandler.DataValue_Out(.Item("EndofServiceDate"), SqlDbType.DateTime)
                mEndofServiceID = mDataHandler.DataValue_Out(.Item("EndofServiceID"), SqlDbType.Int, True)
                mEndofServiceReson = mDataHandler.DataValue_Out(.Item("EndofServiceReson"), SqlDbType.VarChar)
                mEndOfServiceDateText = mDataHandler.DataValue_Out(.Item("EndOfServiceDateText"), SqlDbType.VarChar)
                mEOSDays = mDataHandler.DataValue_Out(.Item("EOSDays"), SqlDbType.Int, True)
                mEOSMonths = mDataHandler.DataValue_Out(.Item("EOSMonths"), SqlDbType.Int, True)
                mEOSYears = mDataHandler.DataValue_Out(.Item("EOSYears"), SqlDbType.Int, True)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)

                mEosTotalDays = mDataHandler.DataValue_Out(.Item("EosTotalDays"), SqlDbType.Int, True)
                mEosTotalMonths = mDataHandler.DataValue_Out(.Item("EosTotalMonths"), SqlDbType.Int, True)
                mEosTotalYears = mDataHandler.DataValue_Out(.Item("EosTotalYears"), SqlDbType.Int, True)

                mEosOverDueDays = mDataHandler.DataValue_Out(.Item("EosOverDueDays"), SqlDbType.Int, True)
                mEosOverDueMonths = mDataHandler.DataValue_Out(.Item("EosOverDueMonths"), SqlDbType.Int, True)
                mEosOverDueYears = mDataHandler.DataValue_Out(.Item("EosOverDueYears"), SqlDbType.Int, True)
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
    'Date : 26/6/2007
    'Description:   Make the values of parameter equal values of private member  of the class

    '===================================================================

    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal ptrOperationType As OperationType) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EndofServiceID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEndofServiceID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@JoinDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mJoinDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EndofServiceDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mEndofServiceDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EndofServiceReson", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEndofServiceReson, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EndOfServiceDateText", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEndOfServiceDateText, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EOSDays", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEOSDays, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EOSMonths", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEOSMonths, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EOSYears", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEOSYears, SqlDbType.Int, True)


            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EosTotalDays", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEosTotalDays, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EosTotalMonths", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEosTotalMonths, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EosTotalYears", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEosTotalYears, SqlDbType.Int, True)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EosOverDueDays", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEosOverDueDays, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EosOverDueMonths", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEosOverDueMonths, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EosOverDueYears", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEosOverDueYears, SqlDbType.Int, True)
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

    Protected Overloads Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class
