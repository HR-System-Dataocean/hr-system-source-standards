Imports Venus.Application.SystemFiles.System
Public Class ClsRec_BioGraphiesDetail4
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " Rec_BioGraphiesDetail4 "
        mInsertParameter = "" & _
          "BioGraphy_ID," & _
          "EngName," & _
          "ArbName," & _
          "ArbName4S," & _
          "GDateFrom," & _
          "HDateFrom," & _
          "GDateTo," & _
          "HDateTo," & _
          "Years," & _
          "Position_ID," & _
          "Remarks," & _
          "RegUserID," & _
          "RegComputerID"
        mInsertParameterValues = "" & _
          " @BioGraphy_ID," & _
          " @EngName," & _
          " @ArbName," & _
          " @ArbName4S," & _
          " @GDateFrom," & _
          " @HDateFrom," & _
          " @GDateTo," & _
          " @HDateTo," & _
          " @Years," & _
          " @Position_ID," & _
          " @Remarks," & _
          " @RegUserID," & _
          " @RegComputerID"
        mUpdateParameter = "" & _
          "BioGraphy_ID=@BioGraphy_ID," & _
          "EngName=@EngName," & _
          "ArbName=@ArbName," & _
          "ArbName4S=@ArbName4S," & _
          "GDateFrom=@GDateFrom," & _
          "HDateFrom=@HDateFrom," & _
          "GDateTo=@GDateTo," & _
          "HDateTo=@HDateTo," & _
          "Years=@Years," & _
          "Position_ID=@Position_ID," & _
          "Remarks=@Remarks"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mBioGraphy_ID As Integer
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mGDateFrom As DateTime
    Private mHDateFrom As String
    Private mGDateTo As DateTime
    Private mHDateTo As String
    Private mYears As Integer
    Private mPosition_ID As Integer
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
    Public Property BioGraphy_ID() As Integer
        Get
            Return mBioGraphy_ID
        End Get
        Set(ByVal Value As Integer)
            mBioGraphy_ID = Value
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
    Public Property ArbName() As String
        Get
            Return mArbName
        End Get
        Set(ByVal Value As String)
            mArbName = Value
        End Set
    End Property
    Public Property ArbName4S() As String
        Get
            Return mArbName4S
        End Get
        Set(ByVal Value As String)
            mArbName4S = Value
        End Set
    End Property
    Public Property GDateFrom() As DateTime
        Get
            Return mGDateFrom
        End Get
        Set(ByVal Value As DateTime)
            mGDateFrom = Value
        End Set
    End Property
    Public Property HDateFrom() As String
        Get
            Return mHDateFrom
        End Get
        Set(ByVal Value As String)
            mHDateFrom = Value
        End Set
    End Property
    Public Property GDateTo() As DateTime
        Get
            Return mGDateTo
        End Get
        Set(ByVal Value As DateTime)
            mGDateTo = Value
        End Set
    End Property
    Public Property HDateTo() As String
        Get
            Return mHDateTo
        End Get
        Set(ByVal Value As String)
            mHDateTo = Value
        End Set
    End Property
    Public Property Years() As Integer
        Get
            Return mYears
        End Get
        Set(ByVal Value As Integer)
            mYears = Value
        End Set
    End Property
    Public Property Position_ID() As Integer
        Get
            Return mPosition_ID
        End Get
        Set(ByVal Value As Integer)
            mPosition_ID = Value
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
    'Project        :  Fisalia Module
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean  
    'Date Created   : 14/05/2011
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
    'Date Created   : 14/05/2011
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
    'Date Created   :14/05/2011
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
    'ProcedureName  :   Save 
    'Module         :   (Fisalia Module)
    'Project        :   Fisalia Module
    'Description    :   Save new record and return true if operation done otherwise report errors in ErrorPage
    'Developer      :   DataOcean   
    'Date Created   :   14/05/2011
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
    'Date Created   :14/05/2011
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
    'Date Created   :14/05/2011 14:44:49
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
    'Date Created   :14/05/2011
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
            mBioGraphy_ID = 0
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mGDateFrom = Nothing
            mHDateFrom = String.Empty
            mGDateTo = Nothing
            mHDateTo = String.Empty
            mYears = 0
            mPosition_ID = 0
            mRemarks = String.Empty
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
    'Date Created   :14/05/2011
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
                mBioGraphy_ID = mDataHandler.DataValue_Out(.Item("BioGraphy_ID"), SqlDbType.Int, True)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mGDateFrom = mDataHandler.DataValue_Out(.Item("GDateFrom"), SqlDbType.DateTime)
                mHDateFrom = mDataHandler.DataValue_Out(.Item("HDateFrom"), SqlDbType.VarChar)
                mGDateTo = mDataHandler.DataValue_Out(.Item("GDateTo"), SqlDbType.DateTime)
                mHDateTo = mDataHandler.DataValue_Out(.Item("HDateTo"), SqlDbType.VarChar)
                mYears = mDataHandler.DataValue_Out(.Item("Years"), SqlDbType.Int, True)
                mPosition_ID = mDataHandler.DataValue_Out(.Item("Position_ID"), SqlDbType.Int, True)
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
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   : 14/05/2011 14:44:49
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BioGraphy_ID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBioGraphy_ID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GDateFrom", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mGDateFrom, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HDateFrom", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHDateFrom, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GDateTo", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mGDateTo, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HDateTo", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHDateTo, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Years", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mYears, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Position_ID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mPosition_ID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            If (strMode.Trim.ToUpper = "SAVE") Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
            End If
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
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   : 14/05/2011 14:44:49
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataset = New DataSet
            mSqlDataAdapter.Fill(mDataset)
            If mDataHandler.CheckValidDataObject(mDataset) Then
                GetParameter(mDataset)
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code DESC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >'" & mID & "' And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code ASC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID <'" & mID & "' And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code DESC"
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
#Region "Class Destructors"
    Public Sub finalized()
        mDataset.Dispose()
    End Sub
#End Region
End Class
