'======================================================================
'Project name  : Venus V. 
'Program name  : clsFingerprintSettings.vb
'Date Created  : 08-07-2021
'Issue #       :       
'Developer     : Hassan Kurdi
'Description   : For fingerprint settings
'              : 
'              : 
'              : 
'Modifacations :
'======================================================================
Public Class clsFingerprintSettings
    Inherits ClsDataAcessLayer

#Region "Class Constructor"
    '========================================================================
    'ProcedureName  :  new()
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Initialize Class Sql Related Members 
    'Developer      :   
    'Date Created   :  08-07-2021
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_FingerprintSettings "
        mInsertParameter = " Code, IsDefault, EngName, ArbName, Remarks, FingerprintDatabaseName, FingerprintTableName, CheckInOutColumnName, RegUserId, CancelDate, FingerprintServerIP, UserIdColumnName, UsersTableName, UserMatchColumnName, UsersTableIdntity"
        mInsertParameterValues = " @Code, @IsDefault, @EngName, @ArbName, @Remarks, @FingerprintDatabaseName, @FingerprintTableName, @CheckInOutColumnName, @RegUserId, @CancelDate, @FingerprintServerIP, @UserIdColumnName, @UsersTableName, @UserMatchColumnName, @UsersTableIdntity"
        mUpdateParameter = " @Code, @IsDefault, @EngName, @ArbName, @Remarks, @FingerprintDatabaseName, @FingerprintTableName, @CheckInOutColumnName, @RegUserId, @CancelDate, @FingerprintServerIP, @UserIdColumnName, @UsersTableName, @UserMatchColumnName, @UsersTableIdntity"
        mSelectCommand = " SELECT * FROM  " & mTable
        mInsertCommand = " INSERT INTO " & mTable & "( " & mInsertParameter & ")VALUES(" & mInsertParameterValues & ")"
        mUpdateCommand = " UPDATE " & mTable & " SET " & mUpdateParameter
        mDeleteCommand = " UPDATE " & mTable & " SET CancelDate=GetDate()"

        
    End Sub
#End Region
#Region "Private Members"
    '=====================================================================
        'Description :  Private members declrations
    '=====================================================================
    Private mID As Integer
    Private mCode As String
    Private mIsDefault As Boolean
    Private mEngName As String
    Private mArbName As String
    Private mRemarks As String
    Private mFingerprintDatabaseName As String
    Private mFingerprintTableName As String
    Private mCheckInOutColumnName As String
    Private mRegUserId As Integer
    Private mRegDate As DateTime
    Private mCancelDate As DateTime
    Private mFingerprintServerIP As String
    Private mUserIdColumnName As String
    Private mUsersTableName As String
    Private mUserMatchColumnName As String
    Private mUsersTableIdntity As String
#End Region
#Region "Public Properties"
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal value As Integer)
            mID = value
        End Set
    End Property
    Public Property Code() As String
        Get
            Return mCode
        End Get
        Set(ByVal value As String)
            mCode = value
        End Set
    End Property
    Public Property IsDefault() As Boolean
        Get
            Return mIsDefault
        End Get
        Set(ByVal value As Boolean)
            mIsDefault = value
        End Set
    End Property
    Public Property EngName() As String
        Get
            Return mEngName
        End Get
        Set(ByVal value As String)
            mEngName = value
        End Set
    End Property
    Public Property ArbName() As String
        Get
            Return mArbName
        End Get
        Set(ByVal value As String)
            mArbName = value
        End Set
    End Property
    Public Property Remarks() As String
        Get
            Return mRemarks
        End Get
        Set(ByVal value As String)
            mRemarks = value
        End Set
    End Property
    Public Property FingerprintDatabaseName() As String
        Get
            Return mFingerprintDatabaseName
        End Get
        Set(ByVal value As String)
            mFingerprintDatabaseName = value
        End Set
    End Property
    Public Property FingerprintTableName() As String
        Get
            Return mFingerprintTableName
        End Get
        Set(ByVal value As String)
            mFingerprintTableName = value
        End Set
    End Property
    Public Property CheckInOutColumnName() As String
        Get
            Return mCheckInOutColumnName
        End Get
        Set(ByVal value As String)
            mCheckInOutColumnName = value
        End Set
    End Property
    Public Property RegUserId() As Integer
        Get
            Return mRegUserId
        End Get
        Set(ByVal value As Integer)
            mRegUserId = value
        End Set
    End Property
    Public Property RegDate() As DateTime
        Get
            Return mRegDate
        End Get
        Set(ByVal value As DateTime)
            mRegDate = value
        End Set
    End Property
    Public Property CancelDate() As DateTime
        Get
            Return mCancelDate
        End Get
        Set(ByVal value As DateTime)
            mCancelDate = value
        End Set
    End Property
    Public Property FingerprintServerIP() As String
        Get
            Return mFingerprintServerIP
        End Get
        Set(ByVal value As String)
            mFingerprintServerIP = value
        End Set
    End Property
    Public Property UserIdColumnName() As String
        Get
            Return mUserIdColumnName
        End Get
        Set(ByVal value As String)
            mUserIdColumnName = value
        End Set
    End Property
    Public Property UsersTableName() As String
        Get
            Return mUsersTableName
        End Get
        Set(ByVal value As String)
            mUsersTableName = value
        End Set
    End Property
    Public Property UserMatchColumnName() As String
        Get
            Return mUserMatchColumnName
        End Get
        Set(ByVal value As String)
            mUserMatchColumnName = value
        End Set
    End Property
    Public Property UsersTableIdntity() As String
        Get
            Return mUsersTableIdntity
        End Get
        Set(ByVal value As String)
            mUsersTableIdntity = value
        End Set
    End Property
#End Region
#Region "Public Function"
    '========================================================================
    'ProcedureName  :  GetList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :[0257]   
    'Date Created   :05-08-2007
    'Modifacations  :
    'Modified by    : MAz
    'Date           : 05-12-2007
    'Description    : Update method to match with Language  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

             StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function
    
    Public Function GetDropDown(ByRef DdlValues As Global.System.Web.UI.WebControls.DropDownList) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try

             StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Items.Clear()

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

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If
            'Filter = Filter.Replace("ص", " AM ").Replace("م", " PM ")
            '==================== Order By Modification [ End ]
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            '==================== Order By Modification [Start]
            StrSelectCommand = StrSelectCommand & orderByStr
            '==================== Order By Modification [ End ]
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
    'Date Created   :05-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmBloodGroups.aspx.vb)SavePart()
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
    'Date Created   :05-08-2007
    'Modifacations  :
    'Calls          :
    'From           :(frmBloodGroups.aspx.vb)SavePart()
    'To             :SetParameter()
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
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Delete record that match critera
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :[0257]   
    'Date Created   :05-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmBloodGroups.aspx.vb)TlbMainToolbar_ButtonClicked()
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
    'Date Created   :05-08-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mIsDefault = 0
            mEngName = String.Empty
            mArbName = String.Empty
            mFingerprintDatabaseName = String.Empty
            mFingerprintTableName = String.Empty
            mCheckInOutColumnName = String.Empty
            mRemarks = String.Empty
            mRegUserID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mFingerprintServerIP = Nothing
            mUserIdColumnName = String.Empty
            mUsersTableIdntity = String.Empty
            mUsersTableName = String.Empty
            mUserMatchColumnName = String.Empty

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :[0257]   
    'Date Created   :05-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmBloodGroups.aspx.vb)TlbMainNavigation_ButtonClicked()
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
             StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code ASC"
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
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :[0257]   
    'Date Created   :05-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmBloodGroups.aspx.vb)TlbMainNavigation_ButtonClicked()
    'To             :GetParameter()
    '               :Clear()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String
        Try
             StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code DESC"
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
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :[0257]   
    'Date Created   :05-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmBloodGroups.aspx.vb)TlbMainNavigation_ButtonClicked()
    'To             :GetParameter()
    '               :Clear()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String
        Try
             StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code ASC"
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
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :[0257]   
    'Date Created   :05-08-2007
    'Modifacations  : 
    'Calls          :
    'From           :(frmBloodGroups.aspx.vb)TlbMainNavigation_ButtonClicked()
    'To             :GetParameter()
    '               :Clear()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code < '" & mCode & "' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY Code DESC"
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
    Public Function GetFingerPrints(Code As String, FromDate As Date, ToDate As Date)
        Try
            Dim mSqlCommand As New SqlClient.SqlCommand
            mSqlCommand.Connection = New Data.SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = Data.CommandType.Text
            mSqlCommand.CommandText = "EXEC hrs_FingerprintForCurrntMonth " & Code & ", '" & FromDate.ToString("yyyy-MM-dd") & "', '" & ToDate.ToString("yyyy-MM-dd") & "'"
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region
#Region "Class Private Function"
    'Description:   Asign the Result of Ds to the private members of the class
    '=====================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mIsDefault = mDataHandler.DataValue_Out(.Item("IsDefault"), SqlDbType.Bit)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mFingerprintDatabaseName = mDataHandler.DataValue_Out(.Item("FingerprintDatabaseName"), SqlDbType.VarChar)
                mFingerprintTableName = mDataHandler.DataValue_Out(.Item("FingerprintTableName"), SqlDbType.VarChar)
                mCheckInOutColumnName = mDataHandler.DataValue_Out(.Item("CheckInOutColumnName"), SqlDbType.VarChar)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mFingerprintServerIP = mDataHandler.DataValue_Out(.Item("FingerprintServerIP"), SqlDbType.VarChar)
                mUserIdColumnName = mDataHandler.DataValue_Out(.Item("UserIdColumnName"), SqlDbType.VarChar)
                mUsersTableName = mDataHandler.DataValue_Out(.Item("UsersTableName"), SqlDbType.VarChar)
                mUserMatchColumnName = mDataHandler.DataValue_Out(.Item("UserMatchColumnName"), SqlDbType.VarChar)
                mUsersTableIdntity = mDataHandler.DataValue_Out(.Item("UsersTableIdntity"), SqlDbType.VarChar)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '=====================================================================
    'Description:   Make the values of parameter equal values of private member of the class
    '=====================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsDefault", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mIsDefault, SqlDbType.bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FingerprintDatabaseName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFingerprintDatabaseName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FingerprintTableName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFingerprintTableName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CheckInOutColumnName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCheckInOutColumnName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
           'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegDate", sqlDbType.Datetime)).Value = mDataHandler.DataValue_In(mRegDate, sqlDbType.Datetime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.MainCompanyID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FingerprintServerIP", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFingerprintServerIP, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UserIdColumnName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mUserIdColumnName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UsersTableName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mUsersTableName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UserMatchColumnName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mUserMatchColumnName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@UsersTableIdntity", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mUsersTableIdntity, SqlDbType.VarChar)
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
