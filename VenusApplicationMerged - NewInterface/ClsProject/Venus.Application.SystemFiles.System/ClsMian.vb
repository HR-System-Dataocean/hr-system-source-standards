Imports System
Imports System.Configuration
Imports System.Collections.Specialized

Public Class ClsMain

#Region "Class Constructors"

    Public Sub New(ByVal page As Web.UI.Page)
        Dim IntGroupID As Integer
        Dim IntCompanyID As Integer
        mErrorHandler = New Venus.Shared.Errors.ErrorsHandler(mConnectionString)
        Me.mPage = page
        Me.mConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Me.mGroupID = page.Session.Item("GroupID")
        Me.mMainCompanyID = page.Session.Item("CompanyID")
        If Me.mGroupID = 0 Or Me.MainCompanyID = 0 Then
            mWebHandler.GetCookies(page, "GroupID", IntGroupID)
            mWebHandler.GetCookies(page, "CompanyID", IntCompanyID)
            page.Session.Item("GroupID") = IntGroupID
            page.Session.Item("CompanyID") = IntCompanyID
            Me.mGroupID = IntGroupID
            Me.mMainCompanyID = IntCompanyID
        End If
        If ConnectionString Is Nothing Then
            mPage.Response.Redirect(CONFIG_SECURITYPAGE)
            Exit Sub
        ElseIf ConnectionString.Trim = "" Then
            mPage.Response.Redirect("frmLoginOnly.aspx")
        End If
        AssignConnectionParameter()
    End Sub

#End Region

#Region "Private Members"

    'Main Constant 
    Private Const CONFIG_CONNECTION_STRING As String = "CurrentConnectionString"
    Private Const CONFIG_ERRORPAGE As String = "ErrorPage.aspx"
    Private Const CONFIG_ERRORPARAMETER As String = "ErrorValue"
    Private Const CONFIG_ERRORRECORDINGWAY As Integer = 2
    Private Const CONFIG_SECURITYPAGE As String = "SecurityPage.aspx"
    Protected Const CONFIG_DEFULTIMAGEFOLDER As String = "Users_Archives_Folder"
    Protected Const CONFIG_DATEFORMAT As String = "  Set DateFormat DMY "
    'Protected Const CONFIG_DATEFORMAT As String = " Set DateFormat DMY "

    'SqlClient Handler 
    Protected mDataSet As Data.DataSet
    Protected mSqlDataAdapter As Data.SqlClient.SqlDataAdapter
    Protected mSqlCommand As Data.SqlClient.SqlCommand
    Protected mSqlDataReader As Data.SqlClient.SqlDataReader
    Protected mSqlConnection As Data.SqlClient.SqlConnection

    'General Handler 
    Protected mErrorHandler As Venus.Shared.Errors.ErrorsHandler
    Protected mDataHandler As New Venus.Shared.DataHandler
    Protected mStringHandler As New Venus.Shared.StringHandler
    Protected mWebHandler As New Venus.Shared.Web.WebHandler
    Protected mWebSecurityHandler As New Venus.Shared.Web.WebSecurity

    'Connectivity Handler 
    Protected mConnectionString As String
    Protected mPage As Web.UI.Page
    Protected mTable As String

    'Operation Handler 
    Protected mInsertParameter As String
    Protected mInsertParameterValues As String
    Protected mUpdateParameter As String
    Protected mSelectCommand As String
    Protected mInsertCommand As String
    Protected mUpdateCommand As String
    Protected mDeleteCommand As String


    'DataBase Handler
    Protected mDataBaseUserID As String
    Protected mDataBaseUserRelatedID As Integer
    Protected mGroupID As Integer
    Protected mMainCompanyID As Integer
    Protected mDataBasePassword As String
    Protected mDataBaseServerName As String
    Protected mDataBaseName As String


    'General Property 
    Protected mLangauge As Language = Language.English
    Protected mNameArrangment As String = "FR,FA,GR,FM"
    Protected mNameSeparator As String = " , "


    Protected mID As Integer

#End Region

#Region "Public Enum"

    Public Enum ApplicationLogInType
        Session
        Cookies
    End Enum

    Public Enum OperationType
        Save
        Update
        Delete
    End Enum

    Public Enum Language
        English
        Arbic
        SystemLanguage
    End Enum


#End Region

#Region "Public property"

    Public Property Table() As String
        Get
            Return mTable
        End Get
        Set(ByVal value As String)
            mTable = value
        End Set
    End Property

    Public Property DataSet() As DataSet
        Get
            Return mDataSet
        End Get
        Set(ByVal value As DataSet)
            mDataSet = value
        End Set
    End Property

    Public ReadOnly Property ConnectionString() As String
        Get
            Return mConnectionString
        End Get
    End Property

    Public Property DataBaseUserID() As String
        Get
            Return mDataBaseUserID
        End Get
        Set(ByVal value As String)
            mDataBaseUserID = value
        End Set
    End Property

    Public ReadOnly Property DataBaseUserRelatedID() As Integer
        Get
            Return mDataBaseUserRelatedID
        End Get
    End Property

    Public ReadOnly Property GroupID() As Integer
        Get
            Return mGroupID
        End Get
    End Property

    Public Property DataBasePassword() As String
        Get
            Return mDataBasePassword
        End Get
        Set(ByVal value As String)
            mDataBasePassword = value
        End Set
    End Property

    Public ReadOnly Property DataBaseServerName() As String
        Get
            Return mDataBaseServerName
        End Get
    End Property

    Public ReadOnly Property DataBaseName() As String
        Get
            Return mDataBaseName
        End Get
    End Property

    Public ReadOnly Property MainCompanyID() As Integer
        Get
            Return mMainCompanyID
        End Get
    End Property

    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal value As Integer)
            mID = value
        End Set
    End Property

#End Region

#Region "Helper Functions"

    '==================================================================
    'Created by : Mohammed Gad
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

    Public Function FixNull(ByVal obj As Object, ByVal DataColumn As Global.System.Data.DataColumn) As Object
        Try

            Select Case DataColumn.DataType.Name.ToUpper
                Case "DECIMAL", "DOUBLE", "SINGLE"
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
                Case "BOOLEAN"
                    If obj Is DBNull.Value Then
                        Return False
                    Else
                        Return obj
                    End If
                Case "INT16"
                    If obj Is DBNull.Value Then
                        Return 0
                    Else
                        Return obj
                    End If
                Case "BYTE"
                    If obj Is DBNull.Value Then
                        Return 0
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

    '==================================================================
    'Created by : MAZ
    'Date : 11/02/2008
    'Description: Set onChange event to all controls
    '==================================================================
    Public Sub AddOnChangeEventToControls(ByVal formName As String, ByRef currPage As Global.System.Web.UI.Page)
        Dim clsForms As New ClsSys_Forms(currPage)
        clsForms.Find(" code = REPLACE('" & formName & "',' ','')")

        Dim clsFormsControls As New Clssys_FormsControls(currPage)
        If clsForms.ID > 0 Then

            clsFormsControls.Find(" FormID=" & clsForms.ID)

            Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()

            For Each row As Data.DataRow In tab.Rows

                Dim currCtrl As Global.System.Web.UI.Control = currPage.FindControl(row("Name"))

                If TypeOf (currCtrl) Is Global.System.Web.UI.WebControls.TextBox And row("Name").ToString.ToLower.IndexOf("code") = -1 And row("Name").ToString.ToLower.IndexOf("invoiceno") = -1 Then
                    CType(currCtrl, Global.System.Web.UI.WebControls.TextBox).Attributes.Add("onChange", "DataChanged()")
                ElseIf TypeOf (currCtrl) Is Global.System.Web.UI.WebControls.DropDownList Then
                    CType(currCtrl, Global.System.Web.UI.WebControls.DropDownList).Attributes.Add("onChange", "DataChanged()")
                ElseIf TypeOf (currCtrl) Is Global.System.Web.UI.WebControls.CheckBox Then
                    CType(currCtrl, Global.System.Web.UI.WebControls.CheckBox).Attributes.Add("onClick", "DataChanged()")
                ElseIf TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser Then
                    'CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("ValueChanged", "DataChanged()")
                    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).ClientSideEvents.CalendarValueChanging = "DataChanged" 'Attributes.Add("ValueChanged", "DataChanged()")
                ElseIf TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebDateTimeEdit Then
                    'CType(currCtrl, Infragistics.WebUI.WebDataInput.WebDateTimeEdit).Attributes.Add("onChange", "DataChanged()")
                    CType(currCtrl, Infragistics.WebUI.WebDataInput.WebDateTimeEdit).ClientSideEvents.TextChanged = "DataChanged"
                ElseIf TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebNumericEdit Then
                    'CType(currCtrl, Infragistics.WebUI.WebDataInput.WebNumericEdit).Attributes.Add("onChange", "DataChanged()")
                    CType(currCtrl, Infragistics.WebUI.WebDataInput.WebNumericEdit).ClientSideEvents.TextChanged = "DataChanged"
                ElseIf TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebMaskEdit Then
                    CType(currCtrl, Infragistics.WebUI.WebDataInput.WebMaskEdit).ClientSideEvents.TextChanged = "DataChanged"

                End If
            Next

        End If
    End Sub

    '==================================================================
    'Created by : MAZ
    'Date : 11/02/2008
    'Description: Add notification on change
    '==================================================================
    Public Sub AddNotificationOnChange(ByRef currPage As Global.System.Web.UI.Page)
        currPage.ClientScript.RegisterClientScriptBlock(currPage.ClientScript.GetType, "", "<script for=""window"" event=""onbeforeunload"">if (isFormChanged()){return returnDiscardMsg();}</script>")
    End Sub

    Public Sub AddOnChangeEventToControls(ByVal formName As String, ByRef currPage As Global.System.Web.UI.Page, ByRef TabCtrl As Infragistics.WebUI.UltraWebTab.UltraWebTab)
        Dim clsForms As New ClsSys_Forms(currPage)
        clsForms.Find(" code = REPLACE('" & formName & "',' ','')")

        Dim clsFormsControls As New Clssys_FormsControls(currPage)
        If clsForms.ID > 0 Then

            clsFormsControls.Find(" FormID=" & clsForms.ID)

            Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()

            For Each row As Data.DataRow In tab.Rows

                Dim currCtrl As Global.System.Web.UI.Control = TabCtrl.FindControl(row("Name"))

                If TypeOf (currCtrl) Is Global.System.Web.UI.WebControls.TextBox And row("Name").ToString.ToLower.IndexOf("code") = -1 And row("Name").ToString.ToLower.IndexOf("invoiceno") = -1 Then
                    CType(currCtrl, Global.System.Web.UI.WebControls.TextBox).Attributes.Add("onChange", "DataChanged()")
                ElseIf TypeOf (currCtrl) Is Global.System.Web.UI.WebControls.DropDownList Then
                    CType(currCtrl, Global.System.Web.UI.WebControls.DropDownList).Attributes.Add("onChange", "DataChanged()")
                ElseIf TypeOf (currCtrl) Is Global.System.Web.UI.WebControls.CheckBox Then
                    CType(currCtrl, Global.System.Web.UI.WebControls.CheckBox).Attributes.Add("onClick", "DataChanged()")
                ElseIf TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser Then
                    'CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("ValueChanged", "DataChanged()")
                    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).ClientSideEvents.CalendarValueChanging = "DataChanged" 'Attributes.Add("ValueChanged", "DataChanged()")
                ElseIf TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebDateTimeEdit Then
                    'CType(currCtrl, Infragistics.WebUI.WebDataInput.WebDateTimeEdit).Attributes.Add("onChange", "DataChanged()")
                    CType(currCtrl, Infragistics.WebUI.WebDataInput.WebDateTimeEdit).ClientSideEvents.TextChanged = "DataChanged"
                ElseIf TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebNumericEdit And row("Name").ToString.ToLower.IndexOf("code") = -1 Then
                    'CType(currCtrl, Infragistics.WebUI.WebDataInput.WebNumericEdit).Attributes.Add("onChange", "DataChanged()")
                    CType(currCtrl, Infragistics.WebUI.WebDataInput.WebNumericEdit).ClientSideEvents.TextChanged = "DataChanged"
                ElseIf TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebMaskEdit And row("Name").ToString.ToLower.IndexOf("code") = -1 Then
                    CType(currCtrl, Infragistics.WebUI.WebDataInput.WebMaskEdit).ClientSideEvents.TextChanged = "DataChanged"

                End If
            Next

        End If
    End Sub

    Public Function AddDataUpdateSchedules(ByRef currPage As Global.System.Web.UI.Page, ByVal formName As String, ByVal recordID As Integer) As Boolean
        If recordID <= 0 Then
            Return False
        End If

        Dim clsForms As New ClsSys_Forms(currPage)
        clsForms.Find(" code = REPLACE('" & formName & "',' ','')")

        Dim clsFormsControls As New Clssys_FormsControls(currPage)
        clsFormsControls.Find(" FormID=" & clsForms.ID)

        Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()

        For Each row As Data.DataRow In tab.Rows


            clsFormsControls.Find(" FormID=" & clsForms.ID & " And Name='" & row("Name") & "'")
            Dim sys_Fields As New Clssys_Fields(currPage)
            If (clsFormsControls.FieldID = Nothing) Then
                Continue For
            End If
            sys_Fields.Find(" ID=" & clsFormsControls.FieldID)

            If (sys_Fields.FieldName.Trim() = "Code" Or sys_Fields.FieldName.Trim() = "Number" Or sys_Fields.FieldName.Trim() = "ID") Then
                Continue For
            End If
            Dim currCtrl As Web.UI.Control = currPage.FindControl(row("Name"))

            '-------------------------------0257 MODIFIED-----------------------------------------
            Dim bIsArabic As Boolean = IIf(IsDBNull(row("IsArabic")), False, row("IsArabic"))
            If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is Web.UI.WebControls.TextBox) Then
                CType(currCtrl, Web.UI.WebControls.TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                'If (TypeOf (currCtrl) Is TextBox) Then
            ElseIf (TypeOf (currCtrl) Is Web.UI.WebControls.TextBox) Then
                '-------------------------------=============-----------------------------------------


                CType(currCtrl, Web.UI.WebControls.TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")

            ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then

                CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")

            ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebNumericEdit) Then

                CType(currCtrl, Infragistics.WebUI.WebDataInput.WebNumericEdit).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
            ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebMaskEdit) Then

                CType(currCtrl, Infragistics.WebUI.WebDataInput.WebMaskEdit).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")

            End If

        Next

        Return True

    End Function

    Public Function AddDataUpdateSchedules(ByRef currPage As Global.System.Web.UI.Page, ByRef TabCtrl As Infragistics.WebUI.UltraWebTab.UltraWebTab, ByVal formName As String, ByVal recordID As Integer) As Boolean
        If recordID <= 0 Then
            Return False
        End If

        Dim clsForms As New ClsSys_Forms(currPage)
        clsForms.Find(" code = REPLACE('" & formName & "',' ','')")

        Dim clsFormsControls As New Clssys_FormsControls(currPage)
        clsFormsControls.Find(" FormID=" & clsForms.ID)

        Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()

        For Each row As Data.DataRow In tab.Rows


            clsFormsControls.Find(" FormID=" & clsForms.ID & " And Name='" & row("Name") & "'")
            Dim sys_Fields As New Clssys_Fields(currPage)
            If (clsFormsControls.FieldID = Nothing) Then
                Continue For
            End If
            sys_Fields.Find(" ID=" & clsFormsControls.FieldID)

            If (sys_Fields.FieldName.Trim() = "Code" Or sys_Fields.FieldName.Trim() = "Number" Or sys_Fields.FieldName.Trim() = "ID") Then
                Continue For
            End If
            Dim currCtrl As Web.UI.Control = TabCtrl.FindControl(row("Name"))

            '-------------------------------0257 MODIFIED-----------------------------------------
            Dim bIsArabic As Boolean = IIf(IsDBNull(row("IsArabic")), False, row("IsArabic"))
            If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is Web.UI.WebControls.TextBox) Then
                CType(currCtrl, Web.UI.WebControls.TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                'If (TypeOf (currCtrl) Is TextBox) Then
            ElseIf (TypeOf (currCtrl) Is Web.UI.WebControls.TextBox) Then
                '-------------------------------=============-----------------------------------------


                CType(currCtrl, Web.UI.WebControls.TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")

            ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then

                CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")

            ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebNumericEdit) Then

                CType(currCtrl, Infragistics.WebUI.WebDataInput.WebNumericEdit).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
            ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebMaskEdit) Then

                CType(currCtrl, Infragistics.WebUI.WebDataInput.WebMaskEdit).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")

            End If

        Next
        Return True
    End Function

    Public Function GetFieldDescription(ByVal StrCode As String, ByVal StrTableName As String) As String
        Dim StrReturnData As Object
        Try
            StrReturnData = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, CommandType.Text, " Select EngName + '/' + ArbName From " & StrTableName & " Where Code = '" & StrCode.ToString.TrimStart.TrimEnd & "'")
            If IsNothing(StrReturnData) Then Return "/"
            If IsDBNull(StrReturnData) Then Return "/"
            Return StrReturnData
        Catch ex As Exception
            Return "/"
        End Try

    End Function

    '==================================================================
    'Created by : [0260]
    'Date       : 26/05/2008
    'Description: Getting if the record if it is can be viewed or not.
    '==================================================================
    Public Function CheckRecordExistance(ByVal StrFilter As String) As Boolean
        Dim StrSelectCommand As String
        Dim ID As Integer = 0
        Try
            StrSelectCommand = "Select ID From " & mTable & IIf(StrFilter.Trim.Length > 0, " Where " & StrFilter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrSelectCommand
            mSqlCommand.Connection.Open()
            ID = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            If ID > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function

#End Region

#Region "Class Private Function"

    Protected Function RecordError(ByVal ex As Exception, ByVal ErrorNumber As Integer, Optional ByVal SqlCommand As String = "") As Boolean

        mPage.Session.Add(CONFIG_ERRORPARAMETER, ex)
        mErrorHandler.RecordExceptions_DataBase(SqlCommand, ex, ErrorNumber, mDataBaseUserID, CONFIG_ERRORRECORDINGWAY)
        mPage.Response.Redirect(CONFIG_ERRORPAGE)
    End Function
    Protected Function AssignConnectionParameter() As Boolean
        Dim conInfo As String() = New String(4) {}
        conInfo = Me.ConnectionString.Split(";"c)
        For x As Integer = 0 To conInfo.Length - 1
            Dim str As String() = New String(1) {}
            str = conInfo(x).Split("="c)
            Select Case str(0)
                Case "data source"
                    Me.mDataBaseServerName = str(1)
                    Exit Select
                Case "initial catalog"
                    Me.mDataBaseName = str(1)
                    Exit Select
                Case "user id"
                    Me.mDataBaseUserID = str(1)
                    Exit Select
                Case "password"
                    Me.mDataBasePassword = str(1)
                    Exit Select
            End Select
        Next
        If mPage.Session.Item("UserID") Is Nothing Then
            mWebHandler.GetCookies(mPage, "UserID", Me.mDataBaseUserRelatedID)
        Else
            Me.mDataBaseUserRelatedID = mPage.Session.Item("UserID")
        End If
    End Function
    '========================================================================
    'ProcedureName  :  fnGetColumnNames 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  This Function are used to return table column names in as arraylist
    'Developer      :  [0256] 
    'Date Created   :  10-7-2007
    'Modifacations  :
    '  
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Protected Function fnGetColumnNames() As ArrayList
        ' Declarations 
        Dim StrSelectCommand As String = String.Empty
        Dim ObjSqlcommand As New SqlClient.SqlCommand
        Dim ObjSqlDatareader As SqlClient.SqlDataReader
        Dim aryList As New ArrayList
        '
        StrSelectCommand = " SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.Columns " & _
        " WHERE UPPER(TABLE_NAME) = " & mTable.ToUpper & " AND COLUMN_NAMES <> 'ID' " ' Get all columns except ID column 

        mSqlConnection.ConnectionString = mConnectionString
        ObjSqlcommand.Connection = mSqlConnection
        ObjSqlcommand.CommandType = CommandType.Text
        ObjSqlcommand.CommandText = StrSelectCommand
        ' 
        If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
        mSqlConnection.Open()

        Try
            ObjSqlDatareader = ObjSqlcommand.ExecuteReader
            While ObjSqlDatareader.Read
                aryList.Add(ObjSqlDatareader.Item(0))
            End While
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

        Return aryList

    End Function

    '========================================================================
    'ProcedureName  :  fnStringColumName 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  This Function Converts an arrayList Items into one String Parameter 
    'Developer      :  0256
    'Date Created   :  10-7-2007
    'Modifacations  :
    ' 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'aryList            : String    : The array List Containing Column Names 
    '========================================================================
    Protected Function fnStringColumName(ByVal aryList As ArrayList) As String
        Dim strColumnNames As String = String.Empty
        Dim ColumnName As String
        For Each ColumnName In aryList
            strColumnNames &= ", " & ColumnName
        Next
        strColumnNames = strColumnNames.Substring(1, strColumnNames.Length - 1)
        Return strColumnNames
    End Function

#End Region

#Region "Class Destructors"
    '========================================================================
    'ProcedureName  :  finalized
    'Module         : DataAccessLayer
    'Project        :  Venus V.
    'Description    :  Distruct Object in memory 
    'Developer      :  DataOcean   
    'Date Created   :  02-11-2008
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region

#Region "Public Functions"

    '========================================================================
    'ProcedureName  :  Find
    'Module         : DataAccessLayer
    'Project        :  Venus V.
    'Description    :  Find record
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  02-11-2008
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameters(mDataSet)
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
    'Module         : DataAccessLayer
    'Project        :  Venus V.
    'Description    :  Save new record
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  02-11-2008
    'Modifacations  : 
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
            SetParameters(mSqlCommand, OperationType.Save)
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
    'Module         : DataAccessLayer
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  02-11-2008
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String = String.Empty
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameters(mSqlCommand, OperationType.Update)
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
    'Module         : DataAccessLayer
    'Project        :  Venus V.
    'Description    :  Delete record that match critera
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :  DataOcean   
    'Date Created   :  02-11-2008
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String = String.Empty
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameters(mSqlCommand, OperationType.Delete)
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
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         : DataAccessLayer
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
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameters(mDataSet)
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
    'Module         : DataAccessLayer
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
    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameters(mDataSet)
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
    'Module         : DataAccessLayer
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
    Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameters(mDataSet)
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
    'Module         : DataAccessLayer
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
    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameters(mDataSet)
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
    'ProcedureName  :  Clear 
    'Module         :  DataAccessLayer
    'Project        :  Venus V.
    'Description    :  Clear all class Content
    'Developer      :  DataOcean   
    'Date Created   :  02-11-2008
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Protected Overridable Function Clear() As Boolean

    End Function
    '========================================================================
    'ProcedureName  :  GetParameters 
    'Module         :  DataAccessLayer
    'Project        :  Venus V.
    'Description    :  GetAllClassParameters From the database 
    'Developer      :  DataOcean   
    'Date Created   :  02-11-2008
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Protected Overridable Function GetParameters(ByVal Ds As DataSet) As Boolean

    End Function
    '========================================================================
    'ProcedureName  :  SetParameters 
    'Module         :  DataAccessLayer
    'Project        :  Venus V.
    'Description    :  SetAllClassParameters From the database 
    'Developer      :  DataOcean   
    'Date Created   :  02-11-2008
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Protected Overridable Function SetParameters(ByRef Command As SqlClient.SqlCommand, ByVal operationType As OperationType) As Boolean

    End Function

#End Region

End Class