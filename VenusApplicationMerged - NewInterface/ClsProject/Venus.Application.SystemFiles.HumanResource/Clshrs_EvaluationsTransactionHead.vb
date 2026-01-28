Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EvaluationsTransactionHead
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    '========================================================================
    'ProcedureName  : Constractor 
    'Module         : (Module)
    'Project        : Venus V.
    'Description    : Initialize insert ,update and delete commands
    'Developer      : DataOcean   
    'Date Created   : 18/02/2009
    'fn. Arguments  : 
    'Parmeter Name  : Description
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EvaluationsTransactionHead "
        mInsertParameter = "  FiscalYearPeriodID,  EmployeeID,EvaluationTypeID, ManagementComment, EmpComment, AdminComment, GeneralManagerComment, Remarks, RegUserID, RegComputerID"
        mInsertParameterValues = " @FiscalYearPeriodID, @EmployeeID,@EvaluationTypeID, @ManagementComment, @EmpComment, @AdminComment, @GeneralManagerComment, @Remarks, @RegUserID, @RegComputerID"
        mUpdateParameter = " FiscalYearPeriodID=@FiscalYearPeriodID, EmployeeID=@EmployeeID,EvaluationTypeID=@EvaluationTypeID,ManagementComment=@ManagementComment,EmpComment=@EmpComment,AdminComment=@AdminComment,GeneralManagerComment=@GeneralManagerComment,Remarks=@Remarks "
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mFiscalYearPeriodID As Integer
    Private mEmployeeID As Integer
    Private mEvaluationTypeID As Integer
    Private mManagementComment As String
    Private mEmpComment As String
    Private mAdminComment As String
    Private mGeneralManagerComment As String
    Private mRemarks As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As DateTime
    Private mCancelDate As DateTime

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
    Public Property FiscalYearPeriodID() As Integer
        Get
            Return mFiscalYearPeriodID
        End Get
        Set(ByVal Value As Integer)
            mFiscalYearPeriodID = Value
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
    Public Property EvaluationTypeID() As Integer
        Get
            Return mEvaluationTypeID
        End Get
        Set(ByVal Value As Integer)
            mEvaluationTypeID = Value
        End Set
    End Property

    Public Property ManagementComment() As String
        Get
            Return mManagementComment
        End Get
        Set(ByVal Value As String)
            mManagementComment = Value
        End Set
    End Property
    Public Property EmpComment() As String
        Get
            Return mEmpComment
        End Get
        Set(ByVal Value As String)
            mEmpComment = Value
        End Set
    End Property
    Public Property AdminComment() As String
        Get
            Return mAdminComment
        End Get
        Set(ByVal Value As String)
            mAdminComment = Value
        End Set
    End Property
    Public Property GeneralManagerComment() As String
        Get
            Return mGeneralManagerComment
        End Get
        Set(ByVal Value As String)
            mGeneralManagerComment = Value
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
#End Region
#Region "Public Function"
    '========================================================================
    'ProcedureName  :  GetList
    'Project        :  Venus V.
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DateOcean  
    'Date Created   : 18/02/2009
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()
            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/ [ إختر أحد الإختيارات ] ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                Item.DataValue = ObjDataRow("ID")
                DdlValues.ValueListItems.Add(Item)
            Next
            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  GetDropDownList
    'Project        :  Venus V.
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DateOcean  
    'Date Created   : 18/02/2009
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Order By EngName ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()
            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/ [ إختر أحد الإختيارات ]")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                Item.Value = ObjDataRow(ID)
                DdlValues.Items.Add(Item)
            Next
            If DdlValues.Items.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Find 
    'Module         : (Account Module)
    'Project        :  Venus V.
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :18/02/2009
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
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataset = New DataSet
            mSqlDataAdapter.Fill(mDataset)
            If mDataHandler.CheckValidDataObject(mDataset) Then
                GetParameter(mDataset)
            Else
                Clear()
            End If
            If mID > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Save 
    'Module         : (Account Module)
    'Project        :  Venus V.
    'Description    :  Save new record and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :18/02/2009
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, "Save")
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Update 
    'Module         : (Account Module)
    'Project        :  Venus V.
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :18/02/2009
    'Modifacations  :
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
            SetParameter(mSqlCommand, "update")
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Delete 
    'Module         : (Account Module)
    'Project        :  Venus V.
    'Description    :  Delete Table row (set Cancel Date)
    'Developer      :  DataOcean   
    'Date Created   :18/02/2009 11:07:28 ص
    'Modifacations  :
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
            SetParameter(mSqlCommand, "Delete")
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  Clear 
    'Module         : (Account Module)
    'Project        :  Venus V.
    'Description    :  Clear Table Columns
    'Developer      :  DataOcean   
    'Date Created   :18/02/2009
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = Nothing
            mFiscalYearPeriodID = 0
            mEmployeeID = Nothing
            mEvaluationTypeID = 0
            mManagementComment = String.Empty
            mEmpComment = String.Empty
            mAdminComment = String.Empty
            mGeneralManagerComment = String.Empty
            mRemarks = String.Empty
            mRegUserID = Nothing
            mRegComputerID = Nothing
            mRegDate = Nothing
            mCancelDate = Nothing

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Class Private Function"
    '========================================================================
    'ProcedureName  :  GetParameter 
    'Module         : (Account Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :18/02/2009
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mFiscalYearPeriodID = mDataHandler.DataValue_Out(.Item("FiscalYearPeriodID"), SqlDbType.Int)
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mEvaluationTypeID = mDataHandler.DataValue_Out(.Item("EvaluationTypeID"), SqlDbType.Int, True)
                mManagementComment = mDataHandler.DataValue_Out(.Item("ManagementComment"), SqlDbType.VarChar)
                mEmpComment = mDataHandler.DataValue_Out(.Item("EmpComment"), SqlDbType.VarChar)
                mAdminComment = mDataHandler.DataValue_Out(.Item("AdminComment"), SqlDbType.VarChar)
                mGeneralManagerComment = mDataHandler.DataValue_Out(.Item("GeneralManagerComment"), SqlDbType.VarChar)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)

            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         : (Account Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   : 18/02/2009 11:07:28 ص
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FiscalYearPeriodID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFiscalYearPeriodID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EvaluationTypeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEvaluationTypeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ManagementComment", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mManagementComment, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmpComment", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEmpComment, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AdminComment", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mAdminComment, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GeneralManagerComment", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mGeneralManagerComment, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         : (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  02-11-2008
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY FiscalYearPeriodID ASC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY FiscalYearPeriodID DESC "
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE FiscalYearPeriodID >'" & mFiscalYearPeriodID & "' And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY FiscalYearPeriodID ASC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE FiscalYearPeriodID <'" & mFiscalYearPeriodID & "' And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY FiscalYearPeriodID DESC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Class Custom Function"
    '<Your Custom Code Here>
#End Region
#Region "Related Tables Functions"
    Public Function SaveEvaluationTransactionDetails(ByVal uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal intEvaluationID As Integer, ByVal intEvaluationTypeID As Integer) As Boolean
        Dim StrSaveCommand As String = " Set DateFormat dmy ; " & vbNewLine
        Dim ClsEvaluationHead As New Clshrs_EvaluationsTransactionHead(mPage)
        Dim dblEvaluationDegree As Double
        Dim intElementID As String
        Dim intGradeID As Integer
        Dim intMainElementID As Integer
        Try

            'StrSaveCommand &= "Delete From hrs_EvaluationsTransactionDetail" & _
            '" Where ID In (Select ET.ID hrs_EvaluationsTransactionDetail ETD " & _
            '" Inner Join hrs_EvaluationMainElementSubElements EMSE " & _
            '" On EMSE.EvaluationSubElementID = ETD.EvaluationSubElementID And EMSE.EvaluationMainElementID = ETD.EvaluationMainElementID " & _
            '" Inner join hrs_EvaluationsSubElements ES On ES.ID = ETD.EvaluationSubElementID " & _
            '" Inner join dbo.hrs_EvaluationsMainElements EM On EM.ID = EMSE.EvaluationMainElementID " & _
            '" Where " & _
            '" ETD.EvaluationTransactionHeadID =  " & intEvaluationID & _
            '" And ES.EvaluationTypeID = " & intEvaluationTypeID & _
            '" And EM.EvaluationTypeID = " & intEvaluationTypeID & ");" & vbNewLine

            StrSaveCommand &= " Delete From hrs_EvaluationsTransactionDetail Where EvaluationTransactionHeadID= " & intEvaluationID & ";" & vbNewLine

            For Each objRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGrid.Rows
                dblEvaluationDegree = objRow.Cells.FromKey("Degree").Value
                intElementID = objRow.Cells.FromKey("EvaluationSubElement").Value
                intGradeID = objRow.Cells.FromKey("Grade").Value
                intMainElementID = objRow.Cells.FromKey("EvaluationMainElementID").Value
                If Not IsNothing(intElementID) AndAlso intElementID > 0 Then
                    StrSaveCommand &= " Insert Into hrs_EvaluationsTransactionDetail (EvaluationTransactionHeadID,Degree,EvaluationSubElementID,GradeID,EvaluationMainElementID)  select " & intEvaluationID & _
                         ", " & IIf(IsNothing(dblEvaluationDegree), 0, dblEvaluationDegree) & _
                         ", " & intElementID & "," & IIf(intGradeID > 0, intGradeID, "Null") & " , " & intMainElementID & " ; " & vbNewLine
                End If
            Next
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvaluationHead.ConnectionString, Global.System.Data.CommandType.Text, StrSaveCommand)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveEvaluationDetails(ByVal uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal csSubBandIndex As Integer, ByVal intEvaluationID As Integer, ByVal intEvaluationTypeID As Integer) As Boolean
        Dim StrSaveCommand As String = " Set DateFormat dmy ; " & vbNewLine
        Dim ClsEvaluationHead As New Clshrs_EvaluationsTransactionHead(mPage)
        Dim dblEvaluationDegree As Double
        Dim intElementID As String
        Dim intGradeID As Integer
        Dim intMainElementID As Integer
        Try

            'StrSaveCommand &= "Delete From hrs_EvaluationsTransactionDetail" & _
            '" Where ID In (Select ET.ID hrs_EvaluationsTransactionDetail ETD " & _
            '" Inner Join hrs_EvaluationMainElementSubElements EMSE " & _
            '" On EMSE.EvaluationSubElementID = ETD.EvaluationSubElementID And EMSE.EvaluationMainElementID = ETD.EvaluationMainElementID " & _
            '" Inner join hrs_EvaluationsSubElements ES On ES.ID = ETD.EvaluationSubElementID " & _
            '" Inner join dbo.hrs_EvaluationsMainElements EM On EM.ID = EMSE.EvaluationMainElementID " & _
            '" Where " & _
            '" ETD.EvaluationTransactionHeadID =  " & intEvaluationID & _
            '" And ES.EvaluationTypeID = " & intEvaluationTypeID & _
            '" And EM.EvaluationTypeID = " & intEvaluationTypeID & ");" & vbNewLine

            StrSaveCommand &= " Delete From hrs_EvaluationsTransactionDetail Where EvaluationTransactionHeadID= " & intEvaluationID & ";" & vbNewLine

            Dim rowEnumerator As Infragistics.WebUI.UltraWebGrid.UltraGridRowsEnumerator = uwgGrid.Bands(csSubBandIndex).GetRowsEnumerator()
            Dim objRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
            While rowEnumerator.MoveNext()
                objRow = rowEnumerator.Current
                dblEvaluationDegree = objRow.Cells.FromKey("Degree").Value
                intElementID = objRow.Cells.FromKey("EvaluationSubElement").Value
                intGradeID = objRow.Cells.FromKey("GradeID").Value
                intMainElementID = objRow.Cells.FromKey("EvaluationMainElementID").Value
                If Not IsNothing(intElementID) AndAlso intElementID > 0 Then
                    StrSaveCommand &= " Insert Into hrs_EvaluationsTransactionDetail (EvaluationTransactionHeadID,Degree,EvaluationSubElementID,GradeID,EvaluationMainElementID)  select " & intEvaluationID & _
                         ", " & IIf(IsNothing(dblEvaluationDegree), 0, dblEvaluationDegree) & _
                         ", " & intElementID & "," & IIf(intGradeID > 0, intGradeID, "Null") & " , " & intMainElementID & " ; " & vbNewLine
                End If
            End While
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvaluationHead.ConnectionString, Global.System.Data.CommandType.Text, StrSaveCommand)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
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
