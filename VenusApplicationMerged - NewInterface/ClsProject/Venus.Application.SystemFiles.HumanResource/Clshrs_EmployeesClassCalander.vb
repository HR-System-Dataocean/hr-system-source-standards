Imports Venus.Application.SystemFiles.System
'======================================================================
'Project name  : Venus V. 
'Program name  : Clshrs_EmployeesClassCalandeEmployee Class Calender
'Date Created  : 10-07-2007
'Issue #       :       
'Developer     : [0256] 
'Description   : Implementation Layer for Update_frmCalendar Screen (Page)
'Modifacations :
'              : [0256] E00001 120707  Adding two Boolean Fields in hrs_EmployeesClassesCalender Table 
'              :                      nonWorkingTime Column : Used to mark a specified date for an employee class as non-working date period    
'              :                      UseDefaultTime Column : Used to mark a specified date for an employee class as Default date period    
'              : [0256] B00002 150707  Adding nonworkingTime and UseDefaultTime to Update Satament 
'              : 
'Parameters 
'---------------------------------------------------------------------------------------------------------------------
' Name             :Data Type     :Decription 
'---------------------------------------------------------------------------------------------------------------------
' mID As           :integer       : class member represents ID                  column in hrs_EmployeesClassesCalender 
' mEmployeeClassID :Integer       : class member represents mEmployeeClassID    column in hrs_EmployeesClassesCalender 
' mFromTime        :Date          : class member represents mFromTime           column in hrs_EmployeesClassesCalender 
' mToTime          :Date          : class member represents mToTime             column in hrs_EmployeesClassesCalender 
' mRemarks         :String        : class member represents mRemarks            column in hrs_EmployeesClassesCalender 
' mRegUserID       :integer       : class member represents mRegUserID          column in hrs_EmployeesClassesCalender 
' mRegComputerID   :integer       : class member represents mRegComputerID      column in hrs_EmployeesClassesCalender 
' mRegDate         :Date          : class member represents mRegDate            column in hrs_EmployeesClassesCalender 
' mCancelDate      :Object        : class member represents mCancelDate         column in hrs_EmployeesClassesCalender 
' mUseDefaultTime  :Boolean       : class member represents mUseDefaultTime     column in hrs_EmployeesClassesCalender 
' mNonWorkingTime  :Boolean       : class member represents mNonWorkingTime     column in hrs_EmployeesClassesCalender 
'======================================================================

Public Class Clshrs_EmployeesClassCalander
    Inherits ClsDataAcessLayer

#Region "Class Constructors"

    'ProcedureName  : New()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This procedure is the Class constructor 
    '                 Which Initialize all objects used within the class scope , and 
    '               : Initialize dataacssecclayer  constructor which will be used as Data layer during implementation 
    'Developer      : [0256] 
    'Date Created   : 12-07-07
    'Modifacations  : 
    'Calling       *:*
    'From           : Update_frmCalendar.aspx.vb Class File 
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesClassesCalender "
        mInsertParameter = " EmployeeClassID ,FromTime ,ToTime ,useDefaultTime,nonWorkingTime , Remarks ,RegUserID ,RegComputerID "
        mInsertParameterValues = " @EmployeeClassID ,@FromTime ,@ToTime,@useDefaultTime,@nonWorkingTime , @Remarks,@RegUserID,@RegComputerID "
        mUpdateParameter = " EmployeeClassID =@EmployeeClassID ,FromTime =@FromTime ,ToTime =@ToTime ,nonworkingtime = @nonWorkingTime ,useDefaultTime=@useDefaultTime, Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID "
        'mSelectCommand = " Select " & " ID , EmployeeClassID ,FromTime ,ToTime ,Remarks ,RegUserID ,RegComputerID ,RegDate ,CancelDate " & " From  " & mTable
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " Insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"

    End Sub

#End Region

#Region "Private Members"

    Private mID As Object
    Private mEmployeeClassID As Integer
    Private mFromTime As Date
    Private mToTime As Date
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Private mUseDefaultTime As Boolean
    Private mNonWorkingTime As Boolean
    '
#End Region

#Region "Public property"

    Public Property id() As Object
        Get
            Return mID
        End Get
        Set(ByVal value As Object)
            mID = value
        End Set
    End Property

    Public Property EmployeeClassID() As Integer
        Get
            Return mEmployeeClassID
        End Get
        Set(ByVal Value As Integer)
            mEmployeeClassID = Value
        End Set
    End Property
    Public Property FromTime() As Date
        Get
            Return mFromTime
        End Get
        Set(ByVal Value As Date)
            mFromTime = Value
        End Set
    End Property
    Public Property ToTime() As Date
        Get
            Return mToTime
        End Get
        Set(ByVal Value As Date)
            mToTime = Value
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
    '
    Public Property useDefaultTime() As Boolean
        Get
            Return mUseDefaultTime
        End Get
        Set(ByVal value As Boolean)
            mUseDefaultTime = value
        End Set
    End Property
    '
    Public Property nonWorkingTime() As Boolean
        Get
            Return mNonWorkingTime
        End Get
        Set(ByVal value As Boolean)
            mNonWorkingTime = value
        End Set
    End Property
#End Region

#Region "Public Function"
    '==================================================================================
    'ProcedureName  : GetDropDownList()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : Fill a DropDownList with data  
    'Developer      : [0256] 
    'Date Created   : 11-07-07
    'Modifacations  : 
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language
    'Calling       *:*
    'Function Calls : 
    'Called From    : Any Associated Class can call this Function 
    '==================================================================================

    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

            'This Code Will be Removed 
            ' I used it coz of some secuirty Considerations 
            Me.mTable = "hrs_EmployeesClasses"
            mSelectCommand = " select * from " & mTable
            ' End Of Code That Will BE REMOVED 

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
    '==================================================================================
    'ProcedureName  : fnSaveToDB()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is Used to Save a New Employeeclass Calender Date(s)
    '               : Into hrs_Employeesclassescalender Table 
    'Developer      : [0256] 
    'Date Created   : 12-07-07
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : fnSetParameter()  : To create and set values for msqlcommand parameters 
    'Called From    : fnPrepareToSave() : In Update_frmCalendar.aspx.vb 
    '==================================================================================
    Public Function fnSaveToDB() As Boolean
        mSqlCommand = New SqlClient.SqlCommand       ' Inaitialize SqlCommand 
        fnSetParameter(mSqlCommand)                  ' Calls fnSetParameter with mSqlcommand  
        mSqlConnection = New SqlClient.SqlConnection
        mSqlConnection.ConnectionString = mConnectionString
        mSqlCommand.Connection = mSqlConnection
        mSqlCommand.CommandType = CommandType.Text
        mSqlCommand.CommandText = mInsertCommand
        If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
        mSqlConnection.Open()

        Try
            mSqlCommand.ExecuteNonQuery()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function


    '==================================================================================
    'ProcedureName  : fnUpdate()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is Used to update an existing  Employeeclass Calender Date(s)
    '               : In hrs_Employeesclassescalender Table 
    'Developer      : [0256] 
    'Date Created   : 12-07-07
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : fnSetParameter()  : To create and set values for msqlcommand parameters 
    'Called From    : fnPrepareToSave() : In Update_frmCalendar.aspx.vb 
    '------------------------------------------------------------------------------------
    'Fn. Arrguments:------------------------------------------------------------------------------------------------------
    '---------------
    ' Name             :Data Type     :Decription 
    '---------------------------------------------------------------------------------------------------------------------
    'strDayFilter      :String        : Holds Date Filter (FromTime) 
    'ClassID           :Integer       : Holds Employee Class ID The Function will Update its Calender Date(s)
    '==================================================================================
    Public Function fnUpdate(ByVal strDayFilter As String, Optional ByVal classID As Integer = 0) As Boolean
        Dim StrUpdateCommand As String = String.Empty
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            mSqlCommand = New SqlClient.SqlCommand
            StrUpdateCommand = mUpdateCommand & "  where  CONVERT(varchar,FROMTIME,101) = '" & strDayFilter & "' and employeeclassid = " & classID
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            fnSetParameter(mSqlCommand)
            'mSqlCommand.Connection.Open()
            'mSqlCommand.ExecuteNonQuery()
            'mSqlCommand.Connection.Close()
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function fngetDefaultDates(ByVal classID As Integer) As ArrayList
        Dim aryList As New ArrayList
        mSqlCommand = New SqlClient.SqlCommand
        mSqlConnection = New SqlClient.SqlConnection
        mSelectCommand = " select DefultStartTime , DefultEndTime from hrs_EmployeesClasses where id = " & classID

        mSqlCommand.CommandType = CommandType.Text
        mSqlCommand.CommandText = mSelectCommand
        mSqlConnection.ConnectionString = mConnectionString
        mSqlCommand.Connection = mSqlConnection

        If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
        mSqlConnection.Open()

        Try
            mSqlDataReader = mSqlCommand.ExecuteReader()
            While mSqlDataReader.Read
                aryList.Add(mSqlDataReader.Item("DefultStartTime"))
                aryList.Add(mSqlDataReader.Item("DefultEndTime"))
            End While
        Catch ex As Exception
        End Try
        Return aryList

    End Function

    '==================================================================================
    'ProcedureName  : fnDelete()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is Used Delete an existing  Employeeclass Calender Date(s)
    '               : From hrs_Employeesclassescalender Table
    '               : By Settting value or update CancelDate Field in hrs_EmployeesClassesCalender Table
    'Developer      : [0256] 
    'Date Created   : 12-07-07
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : fnSetParameter()  : To create and set values for msqlcommand parameters 
    'Called From    : TlbMainToolbar_ButtonClicked   : In Update_frmCalendar.aspx.vb 
    '------------------------------------------------------------------------------------
    'Fn. Arrguments:------------------------------------------------------------------------------------------------------
    '---------------
    ' Name             :Data Type     :Decription 
    '---------------------------------------------------------------------------------------------------------------------
    'strDayFilter      :String        : Holds Date Filter (FromTime) 
    'ClassID           :Integer       : Holds Employee Class ID The Function will Update its Calender Date(s)
    '==================================================================================
    Public Function fnDelete(ByVal strDayFilter As String, ByVal classid As Integer) As Boolean

        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & "  where  CONVERT(varchar,FROMTIME,101) = '" & strDayFilter & "' and employeeclassid = " & classid
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            'fnSetParameter(mSqlCommand)
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

    '==================================================================================
    'ProcedureName  : Delete()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is Used Delete an existing  Employeeclass Calender Date(s)
    '               : From hrs_Employeesclassescalender Table
    '               : By Settting value or update CancelDate Field in hrs_EmployeesClassesCalender Table
    'Developer      : Maz
    'Date Created   : 09-01-2008
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : fnSetParameter()  : To create and set values for msqlcommand parameters 
    'Called From    : TlbMainToolbar_ButtonClicked   : In Update_frmCalendar.aspx.vb 
    '------------------------------------------------------------------------------------
    'Fn. Arrguments:------------------------------------------------------------------------------------------------------
    '---------------
    ' Name             :Data Type     :Decription 
    '---------------------------------------------------------------------------------------------------------------------
    'strDayFilter      :String        : Holds Date Filter (FromTime) 
    'ClassID           :Integer       : Holds Employee Class ID The Function will Update its Calender Date(s)
    '==================================================================================
    Public Function fnDeleteAll(ByVal strDayFilter As String, ByVal classid As Integer) As Boolean

        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = "Delete from " & mTable & "  where  CONVERT(varchar,FROMTIME,101) = '" & strDayFilter & "' and employeeclassid = " & classid
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            'fnSetParameter(mSqlCommand)
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

    '==================================================================================
    'ProcedureName  : fnClear()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is Used To Clear All Class Member Values and
    '               : Reset Them to Initial values 
    'Developer      : [0256] 
    'Date Created   : 12-07-07
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : 
    'Called From    : fnFillDataSet 
    '               : fnFirstRecord() 
    '               : fnFirstRecord()
    '               : LastRecord()
    '               : NextRecord()
    '               : previousRecord()
    '               : fnFillDataSet()
    '               : fnFind()
    '==================================================================================
    Public Function fnClear() As Boolean
        Try
            mID = 0
            mEmployeeClassID = 0
            mFromTime = Nothing
            mToTime = Nothing
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

#Region "Navagiators"
    Public Function fnFirstRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                fnGetParameter(mDataSet)
                Return True
            Else
                fnClear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                fnGetParameter(mDataSet)
                Return True
            Else
                fnClear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                fnGetParameter(mDataSet)
                Return True
            Else
                fnClear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                fnGetParameter(mDataSet)
                Return True
            Else
                fnClear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region
    '==================================================================================
    'ProcedureName  : fnFillDataSet()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is Used To Clear All Class Member Values and
    '               : Reset Them to Initial values 
    'Developer      : [0256]
    'Date Created   : 12-07-07
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : mDataHandler.CheckValidDataObject() with mDataSet as Paramter 
    '               : fnGetParameter() with mDataSet as Paramter
    'Called From    : 

    '==================================================================================
    Public Function fnFillDataSet(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                fnGetParameter(mDataSet)
                Return True
            Else
                fnClear()
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

    '==================================================================================
    'ProcedureName  : fnGetParameter()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is Used to Fill Class members with values 
    '               : In hrs_Employeesclassescalender Table 
    'Developer      : [0256] 
    'Date Created   : 12-07-07
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : 
    'Called From    : fnFillDataSet 
    '               : fnFirstRecord() 
    '               : fnFirstRecord()
    '               : LastRecord()
    '               : NextRecord()
    '               : previousRecord()
    '               : fnFillDataSet()
    '               : fnFind()
    '------------------------------------------------------------------------------------
    'Fn. Arrguments:------------------------------------------------------------------------------------------------------
    '---------------
    ' Name             :Data Type     :Decription 
    '---------------------------------------------------------------------------------------------------------------------
    'Ds                :DataSet       :The DataSet that will be used to set values to class members from it.  
    '==================================================================================
    Private Function fnGetParameter(ByVal Ds As DataSet) As Boolean

        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mEmployeeClassID = mDataHandler.DataValue_Out(.Item("EmployeeClassID"), SqlDbType.Int, True)

                mFromTime = mDataHandler.DataValue_Out(.Item("FromTime"), SqlDbType.SmallDateTime)
                mToTime = mDataHandler.DataValue_Out(.Item("ToTime"), SqlDbType.SmallDateTime)
                mUseDefaultTime = mDataHandler.DataValue_Out(.Item("usedefaultTime"), SqlDbType.Bit)
                mNonWorkingTime = mDataHandler.DataValue_Out(.Item("nonworkingtime"), SqlDbType.Bit)
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

    'Modification 
    '[0256] 11-7-2007 


    '==================================================================================
    'ProcedureName  : fnSetParameter()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is Used to Fill Class members with values 
    '               : In hrs_Employeesclassescalender Table 
    'Developer      : [0256] 
    'Date Created   : 12-07-07
    'Modifacations  : [0256] 120707 : useDefaultTime,nonworkingtime  Fields  are added to hrs_EmployeesClassesCalender Table 
    'Calling       *:*
    'Function Calls : 
    'Called From    : fnSaveToDB()
    '               : fnUpdate()
    '               : fnDelete()
    '------------------------------------------------------------------------------------
    'Fn. Arrguments:------------------------------------------------------------------------------------------------------
    '---------------
    ' Name             :Data Type     :Decription 
    '---------------------------------------------------------------------------------------------------------------------
    'Sqlcommand        :Sqlcommand       :The SqlCommand  The Function will Create Its Parameters and set Values to Them.
    '==================================================================================

    Private Function fnSetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean

        Try
            Sqlcommand.Parameters.Clear()
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeClassID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeClassID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FromTime", SqlDbType.SmallDateTime)).Value = mDataHandler.DataValue_In(mFromTime, SqlDbType.SmallDateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ToTime", SqlDbType.SmallDateTime)).Value = mDataHandler.DataValue_In(mToTime, SqlDbType.SmallDateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@useDefaultTime", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mUseDefaultTime, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@nonWorkingTime", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mNonWorkingTime, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function



    '==================================================================================
    'ProcedureName  : fnFind()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function is Used to update an existing  Employeeclass Calender Date(s)
    '               : In hrs_Employeesclassescalender Table 
    'Developer      : [0256] 
    'Date Created   : 12-07-07
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : fnGetParameter()  
    '               : mDataHandler.CheckValidDataObject()
    '               : fnClear() 
    'Called From    : fnPrepareToSave() : In Update_frmCalendar.aspx.vb 
    '               : TlbMainToolbar_ButtonClicked()
    '               : cldDateSelector_ValueChanged()
    '               : ddlEmployeeClass_SelectedIndexChanged()
    '------------------------------------------------------------------------------------
    'Fn. Arrguments:------------------------------------------------------------------------------------------------------
    '---------------
    ' Name             :Data Type     :Decription 
    '---------------------------------------------------------------------------------------------------------------------
    'strDayFilter      :String        : Holds Date Filter (FromTime) 
    'ClassID           :Integer       : Holds Employee Class ID The Function will Update its Calender Date(s)
    '==================================================================================
    Public Function fnFind(ByVal strDayFilter As String, Optional ByVal classID As Integer = 0) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(strDayFilter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CONVERT(varchar,FROMTIME,101) = '" & strDayFilter & "' and employeeclassid = " & classID, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                fnGetParameter(mDataSet)
            Else
                fnClear()
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

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = mSelectCommand & IIf(Len(strDayFilter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And CONVERT(varchar,FROMTIME,101) = '" & strDayFilter & "' and employeeclassid = " & classID, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")

            StrSelectCommand = CONFIG_DATEFORMAT & mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")

            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                fnGetParameter(mDataSet)
            Else
                fnClear()
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

#End Region

End Class

