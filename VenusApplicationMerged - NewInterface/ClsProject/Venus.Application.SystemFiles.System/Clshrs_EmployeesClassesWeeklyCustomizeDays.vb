Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesClassesWeeklyCustomizeDays
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesClassesWeeklyCustomizeDays "
        mInsertParameter = "" &
                "CustomizeID," &
                "DayNo," &
                "TimeIn1," &
                "TimeOut1," &
                "TimeIn2," &
                "TimeOut2," &
                "FirstShiftTimeInFingerprintStart," &
                "FirstShiftEntryTimeInClose," &
                "FirstShiftTimeOutFingerprintClose," &
                "SecondShiftTimeInFingerprintStart," &
                "SecondShiftEntryTimeInClose," &
                "SecondShiftTimeOutFingerprintClose," &
                "IsDayOff," &
                "RegUserID," &
                "RegComputerID"
        mInsertParameterValues = "" &
                " @CustomizeID," &
                " @DayNo," &
                " @TimeIn1," &
                " @TimeOut1," &
                " @TimeIn2," &
                " @TimeOut2," &
                " @FirstShiftTimeInFingerprintStart," &
                " @FirstShiftEntryTimeInClose," &
                " @FirstShiftTimeOutFingerprintClose," &
                " @SecondShiftTimeInFingerprintStart," &
                " @SecondShiftEntryTimeInClose," &
                " @SecondShiftTimeOutFingerprintClose," &
                " @IsDayOff," &
                " @RegUserID," &
                " @RegComputerID"
        mUpdateParameter = "" &
                "CustomizeID=@CustomizeID," &
                "DayNo=@DayNo," &
                "TimeIn1=@TimeIn1," &
                "TimeOut1=@TimeOut1," &
                "TimeIn2=@TimeIn2," &
                "TimeOut2=@TimeOut2," &
                "FirstShiftTimeInFingerprintStart=@FirstShiftTimeInFingerprintStart," &
                "FirstShiftEntryTimeInClose=@FirstShiftEntryTimeInClose," &
                "FirstShiftTimeOutFingerprintClose=@FirstShiftTimeOutFingerprintClose," &
                "SecondShiftTimeInFingerprintStart=@SecondShiftTimeInFingerprintStart," &
                "SecondShiftEntryTimeInClose=@SecondShiftEntryTimeInClose," &
                "SecondShiftTimeOutFingerprintClose=@SecondShiftTimeOutFingerprintClose," &
                "IsDayOff=@IsDayOff"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mCustomizeID As Integer
    Private mDayNo As Integer
    Private mTimeIn1 As String
    Private mTimeOut1 As String
    Private mTimeIn2 As String
    Private mTimeOut2 As String
    Private mFirstShiftTimeInFingerprintStart As String
    Private mFirstShiftEntryTimeInClose As String
    Private mFirstShiftTimeOutFingerprintClose As String
    Private mSecondShiftTimeInFingerprintStart As String
    Private mSecondShiftEntryTimeInClose As String
    Private mSecondShiftTimeOutFingerprintClose As String
    Private mIsDayOff As Boolean
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
    Public Property CustomizeID() As Integer
        Get
            Return mCustomizeID
        End Get
        Set(ByVal Value As Integer)
            mCustomizeID = Value
        End Set
    End Property
    Public Property DayNo() As Integer
        Get
            Return mDayNo
        End Get
        Set(ByVal Value As Integer)
            mDayNo = Value
        End Set
    End Property
    Public Property TimeIn1() As String
        Get
            Return mTimeIn1
        End Get
        Set(ByVal Value As String)
            mTimeIn1 = Value
        End Set
    End Property
    Public Property TimeOut1() As String
        Get
            Return mTimeOut1
        End Get
        Set(ByVal Value As String)
            mTimeOut1 = Value
        End Set
    End Property
    Public Property TimeIn2() As String
        Get
            Return mTimeIn2
        End Get
        Set(ByVal Value As String)
            mTimeIn2 = Value
        End Set
    End Property
    Public Property TimeOut2() As String
        Get
            Return mTimeOut2
        End Get
        Set(ByVal Value As String)
            mTimeOut2 = Value
        End Set
    End Property
    Public Property IsDayOff() As Boolean
        Get
            Return mIsDayOff
        End Get
        Set(ByVal Value As Boolean)
            mIsDayOff = Value
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
    Public Property FirstShiftTimeInFingerprintStart() As String
        Get
            Return mFirstShiftTimeInFingerprintStart
        End Get
        Set(ByVal Value As String)
            mFirstShiftTimeInFingerprintStart = Value
        End Set
    End Property
    Public Property FirstShiftEntryTimeInClose() As String
        Get
            Return mFirstShiftEntryTimeInClose
        End Get
        Set(ByVal Value As String)
            mFirstShiftEntryTimeInClose = Value
        End Set
    End Property
    Public Property FirstShiftTimeOutFingerprintClose() As String
        Get
            Return mFirstShiftTimeOutFingerprintClose
        End Get
        Set(ByVal Value As String)
            mFirstShiftTimeOutFingerprintClose = Value
        End Set
    End Property
    Public Property SecondShiftTimeInFingerprintStart() As String
        Get
            Return mSecondShiftTimeInFingerprintStart
        End Get
        Set(ByVal Value As String)
            mSecondShiftTimeInFingerprintStart = Value
        End Set
    End Property
    Public Property SecondShiftEntryTimeInClose() As String
        Get
            Return mSecondShiftEntryTimeInClose
        End Get
        Set(ByVal Value As String)
            mSecondShiftEntryTimeInClose = Value
        End Set
    End Property
    Public Property SecondShiftTimeOutFingerprintClose() As String
        Get
            Return mSecondShiftTimeOutFingerprintClose
        End Get
        Set(ByVal Value As String)
            mSecondShiftTimeOutFingerprintClose = Value
        End Set
    End Property
#End Region
#Region "Public Function"
    '========================================================================
    'ProcedureName  :  GetList
    'Project        :  Fisalia Module
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean  
    'Date Created   : 18/08/2015
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
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
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
    'Project        :  Fisalia Module
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean  
    'Date Created   : 18/08/2015
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
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
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
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :18/08/2015
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :   Save 
    'Module         :   (Fisalia Module)
    'Project        :   Fisalia Module
    'Description    :   Save new record and return true if operation done otherwise report errors in ErrorPage
    'Developer      :   DataOcean   
    'Date Created   :   18/08/2015
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
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :18/08/2015
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
            SetParameter(mSqlCommand, "Update")
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Delete 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Delete Table row (set Cancel Date)
    'Developer      :  DataOcean   
    'Date Created   :18/08/2015 3:40:13 PM
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
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Clear Table Columns
    'Developer      :  DataOcean   
    'Date Created   :18/08/2015
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCustomizeID = 0
            mDayNo = 0
            mTimeIn1 = String.Empty
            mTimeOut1 = String.Empty
            mTimeIn2 = String.Empty
            mTimeOut2 = String.Empty
            mIsDayOff = False
            mRegUserID = 0
            mRegComputerID = 0
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
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :18/08/2015
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
                mID = [Shared].DataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCustomizeID = [Shared].DataHandler.DataValue_Out(.Item("CustomizeID"), SqlDbType.Int, True)
                mDayNo = [Shared].DataHandler.DataValue_Out(.Item("DayNo"), SqlDbType.Int, True)
                mTimeIn1 = [Shared].DataHandler.DataValue_Out(.Item("TimeIn1"), SqlDbType.VarChar)
                mTimeOut1 = [Shared].DataHandler.DataValue_Out(.Item("TimeOut1"), SqlDbType.VarChar)
                mTimeIn2 = [Shared].DataHandler.DataValue_Out(.Item("TimeIn2"), SqlDbType.VarChar)
                mTimeOut2 = [Shared].DataHandler.DataValue_Out(.Item("TimeOut2"), SqlDbType.VarChar)
                mIsDayOff = [Shared].DataHandler.DataValue_Out(.Item("IsDayOff"), SqlDbType.Bit)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mFirstShiftTimeInFingerprintStart = [Shared].DataHandler.DataValue_Out(.Item("FirstShiftTimeInFingerprintStart"), SqlDbType.VarChar)
                mFirstShiftEntryTimeInClose = [Shared].DataHandler.DataValue_Out(.Item("FirstShiftEntryTimeInClose"), SqlDbType.VarChar)
                mFirstShiftTimeOutFingerprintClose = [Shared].DataHandler.DataValue_Out(.Item("FirstShiftTimeOutFingerprintClose"), SqlDbType.VarChar)
                mSecondShiftTimeInFingerprintStart = [Shared].DataHandler.DataValue_Out(.Item("SecondShiftTimeInFingerprintStart"), SqlDbType.VarChar)
                mSecondShiftEntryTimeInClose = [Shared].DataHandler.DataValue_Out(.Item("SecondShiftEntryTimeInClose"), SqlDbType.VarChar)
                mSecondShiftTimeOutFingerprintClose = [Shared].DataHandler.DataValue_Out(.Item("SecondShiftTimeOutFingerprintClose"), SqlDbType.VarChar)
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
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   : 18/08/2015 3:40:13 PM
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CustomizeID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mCustomizeID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DayNo", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mDayNo, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TimeIn1", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mTimeIn1, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TimeOut1", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mTimeOut1, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TimeIn2", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mTimeIn2, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TimeOut2", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mTimeOut2, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsDayOff", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsDayOff, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FirstShiftTimeInFingerprintStart", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mFirstShiftTimeInFingerprintStart, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FirstShiftEntryTimeInClose", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mFirstShiftEntryTimeInClose, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FirstShiftTimeOutFingerprintClose", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mFirstShiftTimeOutFingerprintClose, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SecondShiftTimeInFingerprintStart", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mSecondShiftTimeInFingerprintStart, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SecondShiftEntryTimeInClose", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mSecondShiftEntryTimeInClose, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SecondShiftTimeOutFingerprintClose", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mSecondShiftTimeOutFingerprintClose, SqlDbType.VarChar)
            If (strMode.Trim.ToUpper = "SAVE") Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End If
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
