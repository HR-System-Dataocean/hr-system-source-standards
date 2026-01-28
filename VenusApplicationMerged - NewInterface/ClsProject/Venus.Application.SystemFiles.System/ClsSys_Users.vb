Public Class Clssys_Users
    Inherits ClsDataAcessLayer

#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        Me.mTable = " sys_Users "
        Me.mInsertParameter = " Code,EngName,ArbName,ArbName4S,Password,IsAdmin,IsArabic,CanChangePassword,ResetSearchCriteria,ResetReportCriteria,SessionIdleTime,EnforceAlphaNumericPwd,PasswordExpiry,PasswordChangedOn,DenyAccessforall,WorkasIndividualuser,Remarks,RegUserID,RegComputerID,RegDate,CancelDate,ChangeFormPermission,ChangeControlPermission,ChangeRecordPermission,ChangeReportPermission,ChangeModulePermission,CanChangeGroups,InterfaceLang,InterfaceStyle,RelEmployee "
        Me.mInsertParameterValues = " @Code,@EngName,@ArbName,@ArbName4S,@Password,@IsAdmin,@IsArabic,@CanChangePassword,@ResetSearchCriteria,@ResetReportCriteria,@SessionIdleTime,@EnforceAlphaNumericPwd,@PasswordExpiry,@PasswordChangedOn,@DenyAccessforall,@WorkasIndividualuser,@Remarks,@RegUserID,@RegComputerID,@RegDate,@CancelDate,@ChangeFormPermission,@ChangeControlPermission,@ChangeRecordPermission,@ChangeReportPermission,@ChangeModulePermission,@CanChangeGroups,@InterfaceLang,@InterfaceStyle,@RelEmployee "
        Me.mUpdateParameter = " Code=@Code,EngName=@EngName,ArbName=@ArbName,ArbName4S=@ArbName4S,Password=@Password,IsAdmin=@IsAdmin,IsArabic=@IsArabic,CanChangePassword=@CanChangePassword,ResetSearchCriteria=@ResetSearchCriteria,ResetReportCriteria=@ResetReportCriteria,SessionIdleTime=@SessionIdleTime,EnforceAlphaNumericPwd=@EnforceAlphaNumericPwd,PasswordExpiry=@PasswordExpiry,PasswordChangedOn=@PasswordChangedOn,DenyAccessforall=@DenyAccessforall,WorkasIndividualuser=@WorkasIndividualuser,Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID,RegDate=@RegDate,CancelDate=@CancelDate,ChangeFormPermission=@ChangeFormPermission,ChangeControlPermission=@ChangeControlPermission,ChangeRecordPermission=@ChangeRecordPermission,ChangeReportPermission=@ChangeReportPermission,ChangeModulePermission =@ChangeModulePermission,CanChangeGroups=@CanChangeGroups,InterfaceLang=@InterfaceLang,InterfaceStyle=@InterfaceStyle,RelEmployee=@RelEmployee "
        Me.mSelectCommand = " Select * From  " & mTable
        Me.mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        Me.mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        Me.mDeleteCommand = "Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Object
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mPassword As String
    Private mIsAdmin As Boolean
    Private mIsArabic As Boolean
    Private mCanChangePassword As Boolean
    Private mResetSearchCriteria As Boolean
    Private mResetReportCriteria As Boolean
    Private mSessionIdleTime As Int16
    Private mEnforceAlphaNumericPwd As Boolean
    Private mPasswordExpiry As Object
    Private mPasswordChangedOn As Object
    Private mDenyAccessforall As Boolean
    Private mWorkasIndividualuser As Boolean
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object

    Private mChangeFormPermission As Boolean
    Private mChangeControlPermission As Boolean
    Private mChangeRecordPermission As Boolean
    Private mChangeReportPermission As Boolean
    Private mChangeModulePermission As Boolean
    Private mCanChangeGroups As Boolean

    Private mInterfaceLang As String
    Private mInterfaceStyle As String
    Private mRelEmployee As Object
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
    Public Property Password() As String
        Get
            Return mPassword
        End Get
        Set(ByVal Value As String)
            mPassword = Value
        End Set
    End Property
    Public Property IsAdmin() As Boolean
        Get
            Return mIsAdmin
        End Get
        Set(ByVal Value As Boolean)
            mIsAdmin = Value
        End Set
    End Property
    Public Property IsArabic() As Boolean
        Get
            Return mIsArabic
        End Get
        Set(ByVal Value As Boolean)
            mIsArabic = Value
        End Set
    End Property
    Public Property CanChangePassword() As Boolean
        Get
            Return mCanChangePassword
        End Get
        Set(ByVal Value As Boolean)
            mCanChangePassword = Value
        End Set
    End Property
    Public Property ResetSearchCriteria() As Boolean
        Get
            Return mResetSearchCriteria
        End Get
        Set(ByVal Value As Boolean)
            mResetSearchCriteria = Value
        End Set
    End Property
    Public Property ResetReportCriteria() As Boolean
        Get
            Return mResetReportCriteria
        End Get
        Set(ByVal Value As Boolean)
            mResetReportCriteria = Value
        End Set
    End Property
    Public Property SessionIdleTime() As Int16
        Get
            Return mSessionIdleTime
        End Get
        Set(ByVal Value As Int16)
            mSessionIdleTime = Value
        End Set
    End Property
    Public Property EnforceAlphaNumericPwd() As Boolean
        Get
            Return mEnforceAlphaNumericPwd
        End Get
        Set(ByVal Value As Boolean)
            mEnforceAlphaNumericPwd = Value
        End Set
    End Property
    Public Property PasswordExpiry() As Object
        Get
            Return mPasswordExpiry
        End Get
        Set(ByVal Value As Object)
            mPasswordExpiry = Value
        End Set
    End Property
    Public Property PasswordChangedOn() As Object
        Get
            Return mPasswordChangedOn
        End Get
        Set(ByVal Value As Object)
            mPasswordChangedOn = Value
        End Set
    End Property
    Public Property DenyAccessforall() As Boolean
        Get
            Return mDenyAccessforall
        End Get
        Set(ByVal value As Boolean)
            mDenyAccessforall = value
        End Set
    End Property
    Public Property WorkasIndividualuser() As Boolean
        Get
            Return mWorkasIndividualuser
        End Get
        Set(ByVal value As Boolean)
            mWorkasIndividualuser = value
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

    Public Property CanChangeGroups() As Boolean
        Get
            Return mCanChangeGroups
        End Get
        Set(ByVal value As Boolean)
            mCanChangeGroups = value
        End Set
    End Property


    Public Property ChangeFormPermission() As Boolean
        Get
            Return mChangeFormPermission
        End Get
        Set(ByVal value As Boolean)
            mChangeFormPermission = value
        End Set
    End Property
    Public Property ChangeControlPermission() As Boolean
        Get
            Return mChangeControlPermission
        End Get
        Set(ByVal value As Boolean)
            mChangeControlPermission = value
        End Set
    End Property
    Public Property ChangeRecordPermission() As Boolean
        Get
            Return mChangeRecordPermission
        End Get
        Set(ByVal value As Boolean)
            mChangeRecordPermission = value
        End Set
    End Property
    Public Property ChangeReportPermission() As Boolean
        Get
            Return mChangeReportPermission
        End Get
        Set(ByVal value As Boolean)
            mChangeReportPermission = value
        End Set
    End Property
    Public Property ChangeModulePermission() As Boolean
        Get
            Return mChangeModulePermission
        End Get
        Set(ByVal value As Boolean)
            mChangeModulePermission = value
        End Set
    End Property
    Public Property InterfaceLang() As String
        Get
            Return mInterfaceLang
        End Get
        Set(ByVal Value As String)
            mInterfaceLang = Value
        End Set
    End Property
    Public Property InterfaceStyle() As String
        Get
            Return mInterfaceStyle
        End Get
        Set(ByVal Value As String)
            mInterfaceStyle = Value
        End Set
    End Property
    Public Property RelEmployee() As Object
        Get
            Return mRelEmployee
        End Get
        Set(ByVal Value As Object)
            mRelEmployee = Value
        End Set
    End Property
#End Region
#Region "Public Function"
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And " & Filter, "  Where IsNull(CancelDate,'')=''  ")
            StrSelectCommand = StrSelectCommand & orderByStr
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function Find(ByVal Filter As String, ByVal nonCanceld As Boolean) As Boolean
        Dim StrSelectCommand As String
        Try
            '==================== Order By Modification [Start]
            Dim orderByStr As String = ""
            If Filter.ToLower.IndexOf("order by") = -1 Then
                orderByStr = " Order By Code "
            End If
            'Filter = Filter.Replace("Õ", " AM ").Replace("ã", " PM ")
            '==================== Order By Modification [ End ]
            If nonCanceld Then
                StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And " & Filter, "  Where IsNull(CancelDate,'')=''  ")
            Else
                StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where  " & Filter, "  ")
            End If
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From sys_Users Where " & Filter
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
            mErrorHandler.RecordExceptions_DataBase(strSQL, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
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
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
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
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
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
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mPassword = String.Empty
            mIsAdmin = False
            mIsArabic = False
            mCanChangePassword = False
            mResetSearchCriteria = False
            mResetReportCriteria = False
            mSessionIdleTime = 0
            mEnforceAlphaNumericPwd = False
            mPasswordExpiry = Nothing
            mPasswordChangedOn = Nothing
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing

            mChangeFormPermission = False
            mChangeControlPermission = False
            mChangeRecordPermission = False
            mChangeReportPermission = False
            mChangeModulePermission = False
            mCanChangeGroups = False
            mInterfaceLang = Nothing
            mInterfaceStyle = Nothing
            mRelEmployee = 0


        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')=''  ORDER BY Code ASC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')=''  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')=''  ORDER BY Code DESC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And isNull(CancelDate,'')=''  ORDER BY Code ASC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID <" & mID & " And isNull(CancelDate,'')='' ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code < '" & mCode & "' And isNull(CancelDate,'')=''  ORDER BY Code DESC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    'Modification :  [0256] 5-12-2007 Add SetLanguage Function to Switch Between EngName , ArbName Fields 
    '             :                  According to Page Language 
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName ", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & " Order By EngName ")
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
    Public Function IsAuthenticated() As Integer
        Dim StrFormName As String = mPage.Request.Url.AbsolutePath.Substring(mPage.Request.Url.AbsolutePath.IndexOf("frm")).ToLower
        Dim Result As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, "GetUserFormPermissionStatus", mDataBaseUserRelatedID, StrFormName)
        If Result = 0 Then
            mPage.Response.Redirect("frmSecurityPage.aspx")
        End If
    End Function
#End Region
#Region "Class Private Function"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mPassword = mDataHandler.DataValue_Out(.Item("Password"), SqlDbType.VarChar)
                mIsAdmin = mDataHandler.DataValue_Out(.Item("IsAdmin"), SqlDbType.Bit)
                mIsArabic = mDataHandler.DataValue_Out(.Item("IsArabic"), SqlDbType.Bit)
                mCanChangePassword = mDataHandler.DataValue_Out(.Item("CanChangePassword"), SqlDbType.Bit)
                mResetSearchCriteria = mDataHandler.DataValue_Out(.Item("ResetSearchCriteria"), SqlDbType.Bit)
                mResetReportCriteria = mDataHandler.DataValue_Out(.Item("ResetReportCriteria"), SqlDbType.Bit)
                mSessionIdleTime = mDataHandler.DataValue_Out(.Item("SessionIdleTime"), SqlDbType.TinyInt)
                mEnforceAlphaNumericPwd = mDataHandler.DataValue_Out(.Item("EnforceAlphaNumericPwd"), SqlDbType.Bit)
                mPasswordExpiry = mDataHandler.DataValue_Out(.Item("PasswordExpiry"), SqlDbType.DateTime)
                mPasswordChangedOn = mDataHandler.DataValue_Out(.Item("PasswordChangedOn"), SqlDbType.DateTime)
                mDenyAccessforall = mDataHandler.DataValue_Out(.Item("DenyAccessforall"), SqlDbType.Bit)
                mWorkasIndividualuser = mDataHandler.DataValue_Out(.Item("WorkasIndividualuser"), SqlDbType.Bit)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mChangeFormPermission = mDataHandler.DataValue_Out(.Item("ChangeFormPermission"), SqlDbType.Bit)
                mChangeControlPermission = mDataHandler.DataValue_Out(.Item("ChangeControlPermission"), SqlDbType.Bit)
                mChangeRecordPermission = mDataHandler.DataValue_Out(.Item("ChangeRecordPermission"), SqlDbType.Bit)
                mChangeReportPermission = mDataHandler.DataValue_Out(.Item("ChangeReportPermission"), SqlDbType.Bit)
                mChangeModulePermission = mDataHandler.DataValue_Out(.Item("ChangeModulePermission"), SqlDbType.Bit)
                mCanChangeGroups = mDataHandler.DataValue_Out(.Item("CanChangeGroups"), SqlDbType.Bit)
                mInterfaceLang = mDataHandler.DataValue_Out(.Item("InterfaceLang"), SqlDbType.VarChar)
                mInterfaceStyle = mDataHandler.DataValue_Out(.Item("InterfaceStyle"), SqlDbType.VarChar)
                mRelEmployee = mDataHandler.DataValue_Out(.Item("RelEmployee"), SqlDbType.Int)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Password", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mPassword, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsAdmin", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsAdmin, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsArabic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsArabic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CanChangePassword", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCanChangePassword, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ResetSearchCriteria", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mResetSearchCriteria, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ResetReportCriteria", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mResetReportCriteria, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SessionIdleTime", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mSessionIdleTime, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EnforceAlphaNumericPwd", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mEnforceAlphaNumericPwd, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PasswordExpiry", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mPasswordExpiry, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PasswordChangedOn", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mPasswordChangedOn, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DenyAccessforall", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mDenyAccessforall, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@WorkasIndividualuser", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mWorkasIndividualuser, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mRegDate, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CancelDate", SqlDbType.DateTime)).Value = mDataHandler.DataValue_In(mCancelDate, SqlDbType.DateTime)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ChangeFormPermission", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mChangeFormPermission, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ChangeControlPermission", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mChangeControlPermission, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ChangeRecordPermission", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mChangeRecordPermission, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ChangeReportPermission", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mChangeReportPermission, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ChangeModulePermission", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mChangeModulePermission, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CanChangeGroups", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCanChangeGroups, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InterfaceLang", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mInterfaceLang, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@InterfaceStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mInterfaceStyle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RelEmployee", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRelEmployee, SqlDbType.Int, True)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, Me.DataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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

