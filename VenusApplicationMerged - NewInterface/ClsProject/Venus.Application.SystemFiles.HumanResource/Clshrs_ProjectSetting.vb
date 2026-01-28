Imports Venus.Application.SystemFiles.System
Public Class Clshrs_ProjectSetting
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_ProjectSetting "
        mInsertParameter = "" & _
          "ProjectChangeID," & _
          "InternalOvertimeFactor," & _
          "ExternalOvertimeFactor," & _
          "InternalDayOffOvertimeFactor," & _
          "ExternalDayOffOvertimeFactor," & _
          "InternalExtensionValue," & _
          "ExternalExtensionValue," & _
          "InternalAbsentFactor," & _
          "ExternalAbsentFactor," & _
          "InternalSickFactor," & _
          "ExternalSickFactor," & _
          "InternalLeavFactor," & _
          "ExternalLeavFactor," & _
          "InternalPermitDelayFactor," & _
          "ExternalPermitDelayFactor," & _
          "InternalDelayPunishFactor," & _
          "ExternalDelayPunishFactor," & _
          "Remarks," & _
          "RegUserID," & _
          "RegComputerID"
        mInsertParameterValues = "" & _
          " @ProjectChangeID," & _
          " @InternalOvertimeFactor," & _
          " @ExternalOvertimeFactor," & _
          " @InternalDayOffOvertimeFactor," & _
          " @ExternalDayOffOvertimeFactor," & _
          " @InternalExtensionValue," & _
          " @ExternalExtensionValue," & _
          " @InternalAbsentFactor," & _
          " @ExternalAbsentFactor," & _
          " @InternalSickFactor," & _
          " @ExternalSickFactor," & _
          " @InternalLeavFactor," & _
          " @ExternalLeavFactor," & _
          " @InternalPermitDelayFactor," & _
          " @ExternalPermitDelayFactor," & _
          " @InternalDelayPunishFactor," & _
          " @ExternalDelayPunishFactor," & _
          " @Remarks," & _
          " @RegUserID," & _
          " @RegComputerID"
        mUpdateParameter = "" & _
          "ProjectChangeID=@ProjectChangeID," & _
          "InternalOvertimeFactor=@InternalOvertimeFactor," & _
          "ExternalOvertimeFactor=@ExternalOvertimeFactor," & _
          "InternalDayOffOvertimeFactor=@InternalDayOffOvertimeFactor," & _
          "ExternalDayOffOvertimeFactor=@ExternalDayOffOvertimeFactor," & _
          "InternalExtensionValue=@InternalExtensionValue," & _
          "ExternalExtensionValue=@ExternalExtensionValue," & _
          "InternalAbsentFactor=@InternalAbsentFactor," & _
          "ExternalAbsentFactor=@ExternalAbsentFactor," & _
          "InternalSickFactor=@InternalSickFactor," & _
          "ExternalSickFactor=@ExternalSickFactor," & _
          "InternalLeavFactor=@InternalLeavFactor," & _
          "ExternalLeavFactor=@ExternalLeavFactor," & _
          "InternalPermitDelayFactor=@InternalPermitDelayFactor," & _
          "ExternalPermitDelayFactor=@ExternalPermitDelayFactor," & _
          "InternalDelayPunishFactor=@InternalDelayPunishFactor," & _
          "ExternalDelayPunishFactor=@ExternalDelayPunishFactor," & _
          "Remarks=@Remarks"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mProjectChangeID As Integer
    Private mInternalOvertimeFactor As Object
    Private mExternalOvertimeFactor As Object
    Private mInternalDayOffOvertimeFactor As Object
    Private mExternalDayOffOvertimeFactor As Object
    Private mInternalExtensionValue As Object
    Private mExternalExtensionValue As Object
    Private mInternalAbsentFactor As String
    Private mExternalAbsentFactor As String
    Private mInternalSickFactor As String
    Private mExternalSickFactor As String
    Private mInternalLeavFactor As String
    Private mExternalLeavFactor As String
    Private mInternalPermitDelayFactor As String
    Private mExternalPermitDelayFactor As String
    Private mInternalDelayPunishFactor As String
    Private mExternalDelayPunishFactor As String
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
    Public Property ProjectChangeID() As Integer
        Get
            Return mProjectChangeID
        End Get
        Set(ByVal Value As Integer)
            mProjectChangeID = Value
        End Set
    End Property
    Public Property InternalOvertimeFactor() As Object
        Get
            Return mInternalOvertimeFactor
        End Get
        Set(ByVal Value As Object)
            mInternalOvertimeFactor = Value
        End Set
    End Property
    Public Property ExternalOvertimeFactor() As Object
        Get
            Return mExternalOvertimeFactor
        End Get
        Set(ByVal Value As Object)
            mExternalOvertimeFactor = Value
        End Set
    End Property
    Public Property InternalDayOffOvertimeFactor() As Object
        Get
            Return mInternalDayOffOvertimeFactor
        End Get
        Set(ByVal Value As Object)
            mInternalDayOffOvertimeFactor = Value
        End Set
    End Property
    Public Property ExternalDayOffOvertimeFactor() As Object
        Get
            Return mExternalDayOffOvertimeFactor
        End Get
        Set(ByVal Value As Object)
            mExternalDayOffOvertimeFactor = Value
        End Set
    End Property
    Public Property InternalExtensionValue() As Object
        Get
            Return mInternalExtensionValue
        End Get
        Set(ByVal Value As Object)
            mInternalExtensionValue = Value
        End Set
    End Property
    Public Property ExternalExtensionValue() As Object
        Get
            Return mExternalExtensionValue
        End Get
        Set(ByVal Value As Object)
            mExternalExtensionValue = Value
        End Set
    End Property
    Public Property InternalAbsentFactor() As String
        Get
            Return mInternalAbsentFactor
        End Get
        Set(ByVal Value As String)
            mInternalAbsentFactor = Value
        End Set
    End Property
    Public Property ExternalAbsentFactor() As String
        Get
            Return mExternalAbsentFactor
        End Get
        Set(ByVal Value As String)
            mExternalAbsentFactor = Value
        End Set
    End Property
    Public Property InternalSickFactor() As String
        Get
            Return mInternalSickFactor
        End Get
        Set(ByVal Value As String)
            mInternalSickFactor = Value
        End Set
    End Property
    Public Property ExternalSickFactor() As String
        Get
            Return mExternalSickFactor
        End Get
        Set(ByVal Value As String)
            mExternalSickFactor = Value
        End Set
    End Property
    Public Property InternalLeavFactor() As String
        Get
            Return mInternalLeavFactor
        End Get
        Set(ByVal Value As String)
            mInternalLeavFactor = Value
        End Set
    End Property
    Public Property ExternalLeavFactor() As String
        Get
            Return mExternalLeavFactor
        End Get
        Set(ByVal Value As String)
            mExternalLeavFactor = Value
        End Set
    End Property
    Public Property InternalPermitDelayFactor() As String
        Get
            Return mInternalPermitDelayFactor
        End Get
        Set(ByVal Value As String)
            mInternalPermitDelayFactor = Value
        End Set
    End Property
    Public Property ExternalPermitDelayFactor() As String
        Get
            Return mExternalPermitDelayFactor
        End Get
        Set(ByVal Value As String)
            mExternalPermitDelayFactor = Value
        End Set
    End Property
    Public Property InternalDelayPunishFactor() As String
        Get
            Return mInternalDelayPunishFactor
        End Get
        Set(ByVal Value As String)
            mInternalDelayPunishFactor = Value
        End Set
    End Property
    Public Property ExternalDelayPunishFactor() As String
        Get
            Return mExternalDelayPunishFactor
        End Get
        Set(ByVal Value As String)
            mExternalDelayPunishFactor = Value
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
    'Date Created   : 28/12/2014
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
    'Date Created   : 28/12/2014
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
    'Date Created   :28/12/2014
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
    'Date Created   :   28/12/2014
    'Modifacations  :   
    'fn. Arguments  :   
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Save() As Integer
        Dim Value As Integer
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, "Save")
            mSqlCommand.Connection.Open()
            Value = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            Return Value
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
    'Date Created   :28/12/2014
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
    'Date Created   :28/12/2014 17:26:55
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
    'Date Created   :28/12/2014
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
            mProjectChangeID = 0
            mInternalOvertimeFactor = 0
            mExternalOvertimeFactor = 0
            mInternalDayOffOvertimeFactor = 0
            mExternalDayOffOvertimeFactor = 0
            mInternalExtensionValue = 0
            mExternalExtensionValue = 0
            mInternalAbsentFactor = "0"
            mExternalAbsentFactor = "0"
            mInternalSickFactor = "0"
            mExternalSickFactor = "0"
            mInternalLeavFactor = "0"
            mExternalLeavFactor = "0"
            mInternalPermitDelayFactor = "0"
            mExternalPermitDelayFactor = "0"
            mInternalDelayPunishFactor = "0"
            mExternalDelayPunishFactor = "0"
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
    'Date Created   :28/12/2014
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
                mProjectChangeID = [Shared].DataHandler.DataValue_Out(.Item("ProjectChangeID"), SqlDbType.Int, True)
                mInternalOvertimeFactor = [Shared].DataHandler.DataValue_Out(.Item("InternalOvertimeFactor"), SqlDbType.Real)
                mExternalOvertimeFactor = [Shared].DataHandler.DataValue_Out(.Item("ExternalOvertimeFactor"), SqlDbType.Real)
                mInternalDayOffOvertimeFactor = [Shared].DataHandler.DataValue_Out(.Item("InternalDayOffOvertimeFactor"), SqlDbType.Real)
                mExternalDayOffOvertimeFactor = [Shared].DataHandler.DataValue_Out(.Item("ExternalDayOffOvertimeFactor"), SqlDbType.Real)
                mInternalExtensionValue = [Shared].DataHandler.DataValue_Out(.Item("InternalExtensionValue"), SqlDbType.Real)
                mExternalExtensionValue = [Shared].DataHandler.DataValue_Out(.Item("ExternalExtensionValue"), SqlDbType.Real)
                mInternalAbsentFactor = [Shared].DataHandler.DataValue_Out(.Item("InternalAbsentFactor"), SqlDbType.VarChar)
                mExternalAbsentFactor = [Shared].DataHandler.DataValue_Out(.Item("ExternalAbsentFactor"), SqlDbType.VarChar)
                mInternalSickFactor = [Shared].DataHandler.DataValue_Out(.Item("InternalSickFactor"), SqlDbType.VarChar)
                mExternalSickFactor = [Shared].DataHandler.DataValue_Out(.Item("ExternalSickFactor"), SqlDbType.VarChar)
                mInternalLeavFactor = [Shared].DataHandler.DataValue_Out(.Item("InternalLeavFactor"), SqlDbType.VarChar)
                mExternalLeavFactor = [Shared].DataHandler.DataValue_Out(.Item("ExternalLeavFactor"), SqlDbType.VarChar)
                mInternalPermitDelayFactor = [Shared].DataHandler.DataValue_Out(.Item("InternalPermitDelayFactor"), SqlDbType.VarChar)
                mExternalPermitDelayFactor = [Shared].DataHandler.DataValue_Out(.Item("ExternalPermitDelayFactor"), SqlDbType.VarChar)
                mInternalDelayPunishFactor = [Shared].DataHandler.DataValue_Out(.Item("InternalDelayPunishFactor"), SqlDbType.VarChar)
                mExternalDelayPunishFactor = [Shared].DataHandler.DataValue_Out(.Item("ExternalDelayPunishFactor"), SqlDbType.VarChar)
                mRemarks = [Shared].DataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
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
    'Date Created   : 28/12/2014 17:26:55
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ProjectChangeID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mProjectChangeID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InternalOvertimeFactor", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mInternalOvertimeFactor, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExternalOvertimeFactor", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mExternalOvertimeFactor, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InternalDayOffOvertimeFactor", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mInternalDayOffOvertimeFactor, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExternalDayOffOvertimeFactor", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mExternalDayOffOvertimeFactor, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InternalExtensionValue", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mInternalExtensionValue, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExternalExtensionValue", SqlDbType.Real)).Value = [Shared].DataHandler.DataValue_In(mExternalExtensionValue, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InternalAbsentFactor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mInternalAbsentFactor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExternalAbsentFactor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mExternalAbsentFactor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InternalSickFactor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mInternalSickFactor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExternalSickFactor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mExternalSickFactor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InternalLeavFactor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mInternalLeavFactor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExternalLeavFactor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mExternalLeavFactor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InternalPermitDelayFactor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mInternalPermitDelayFactor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExternalPermitDelayFactor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mExternalPermitDelayFactor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InternalDelayPunishFactor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mInternalDelayPunishFactor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ExternalDelayPunishFactor", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mExternalDelayPunishFactor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
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
        mDataset.Dispose()
    End Sub
#End Region
End Class
