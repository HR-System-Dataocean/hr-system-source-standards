Imports System
Imports System.Configuration
Imports System.Collections.Specialized
Imports System.Globalization
Imports Venus.Application.SystemFiles.System

Public Class ClsDataAcessLayer2

#Region "Class Constructors"

    Public Sub New(ByVal page As Web.UI.Page)

        '========================================================'
        'Assign the main page and get the connection             '
        '========================================================'

        '-------------------------------0257 MODIFIED-----------------------------------------
        Dim IntGroupID As Integer
        '-------------------------------=============-----------------------------------------
        mErrorHandler = New Venus.Shared.ErrorsHandler(mConnectionString)

        Me.mPage = page
        'If mWebSecurityHandler.GetCurrentConnection(page) = "" Then
        '    mWebSecurityHandler.CreateConnection(page)
        'End If
        Me.mConnectionString = ConfigurationManager.AppSettings("Connstring2").ToString()


        Me.mGroupID = page.Session.Item("GroupID")
        Me.mMainCompanyID = page.Session.Item("CompanyID")


        If Me.mGroupID = 0 Then
            mWebHandler.GetCookies(page, "GroupID", IntGroupID)
            '-------------------------------0257 MODIFIED-----------------------------------------
            'If IntGroupID = 0 Or IntCompanyID = 0 Then
            '    mPage.Response.Redirect("frmLoginOnly.aspx")
            'End If

            '-------------------------------=============-----------------------------------------
            Me.mGroupID = IntGroupID

        End If

        '========================================================'
        'if the conneciton is not sutable go to the security page'
        '========================================================'

        If ConnectionString Is Nothing Then
            mPage.Response.Redirect(CONFIG_SECURITYPAGE)
            Exit Sub
            '-------------------------------0257 MODIFIED-----------------------------------------
        ElseIf ConnectionString.Trim = "" Then
            mPage.Response.Redirect("frmLoginOnly.aspx")
            '-------------------------------=============-----------------------------------------
        End If

        '========================================================'
        'Assign the conneciton parameter to class main variables '
        '========================================================'

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
    Protected mErrorHandler As Venus.Shared.ErrorsHandler
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
    Protected mNameSeparator As String = " "

#End Region

#Region "Public Enum"

    Public Enum ApplicationLogInType
        Session
        Cookies
    End Enum

    Public Enum OperationType
        Save
        Update
    End Enum

    Public Enum Language
        English
        Arbic
        SystemLanguage
    End Enum

    Public Enum DateType
        Gregorian
        Hijri
    End Enum
    Public Enum DateFormat
        DDMMYYYY
        MMDDYYYY
        YYYYMMDD
        YYYYDDMM
        DDMMYY
        MMDDYY
        YYMMDD
        YYDDMM
    End Enum
    Public Enum Directions
        Input
        Output
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

#End Region

    Public Function CountRelated(ByVal IntID As Integer, ByVal strTableName As String) As Integer
        Dim RelationDs As New DataSet
        Dim IntCounter As Integer = 0
        Dim ObjDataRow As DataRow
        RelationDs = GetRelatedTables(strTableName)
        For Each ObjDataRow In RelationDs.Tables(0).Rows
            IntCounter = IntCounter + CountTable(ObjDataRow.Item(6), IntID, ObjDataRow.Item(7))
        Next
        Return IntCounter
    End Function
    Private Function CountTable(ByVal strTable As String, ByVal IntID As Integer, ByVal strColumnName As String) As Integer
        Dim StrCmd As String = "Select * from " & strTable & " Where " & strColumnName & "= " & IntID
        Dim SqlDa As New SqlClient.SqlDataAdapter(StrCmd, ConnectionString)
        Dim dt As New DataTable
        Try
            SqlDa.Fill(dt)
            Return dt.Rows.Count
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetRelatedTables(ByVal strTableName As String)
        Dim StrCmd As String = "exec sp_fkeys '" & strTableName.Trim & "'"
        Dim SqlDa As New SqlClient.SqlDataAdapter(StrCmd, ConnectionString)
        Dim RelationDs As New DataSet
        Try
            SqlDa.Fill(RelationDs)
            Return RelationDs
        Catch ex As Exception
            Return False
        End Try
    End Function
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
    Public Sub AddOnChangeEventToControls(ByVal formName As String, ByRef currPage As Global.System.Web.UI.Page, ByRef TabCtrl As Infragistics.WebUI.UltraWebTab.UltraWebTab, Optional ByVal frmInterface As String = "")
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
                ElseIf TypeOf (currCtrl) Is Infragistics.WebUI.WebDataInput.WebMaskEdit Then
                    CType(currCtrl, Infragistics.WebUI.WebDataInput.WebMaskEdit).ClientSideEvents.TextChanged = "DataChanged"
                    If CType(currCtrl, Infragistics.WebUI.WebDataInput.WebMaskEdit).InputMask = "##/##/####" And
                       frmInterface <> "" Then
                        CType(currCtrl, Infragistics.WebUI.WebDataInput.WebMaskEdit).ClientSideEvents.ValueChange =
                           "wdcDate_TextChanged('" & CType(currCtrl, Infragistics.WebUI.WebDataInput.WebMaskEdit).ID & "', " & frmInterface & ")"
                    ElseIf row("Name").ToString.ToLower.IndexOf("code") = -1 Then
                        CType(currCtrl, Infragistics.WebUI.WebDataInput.WebMaskEdit).ClientSideEvents.TextChanged = "DataChanged"
                    End If
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

            If (sys_Fields.FieldName.Trim().ToLower = "code" Or row("Name").ToString.ToLower.IndexOf("code") > -1 Or sys_Fields.FieldName.Trim() = "Number" Or sys_Fields.FieldName.Trim() = "ID") Then
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
    Public Function GetHigriDate(ByVal dteDate As Object) As Object
        Dim ClsGeneralSetting As New Clshrs_GeneralSetting(mPage)
        ClsGeneralSetting.Find("ID=1")
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)

        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim strFormatDate As String = ConfigurationSettings.AppSettings("DATEFORMAT")


        If IsDBNull(dteDate) Then
            Return Nothing
        End If

        If IsNothing(dteDate) Then
            Return Nothing
        End If

        Dim dteDateIn As Date = dteDate
        Dim dteDateOut As Date
        If dteDateIn.Year = 1 Then
            Return Nothing
        End If


        If ClsGeneralSetting.IsHigry Then
            dteDateOut = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDate(dteDateIn, ClsGHCalender.DateType.Hijri, ClsGHCalender.Directions.Output), Global.System.Data.SqlDbType.DateTime)
        Else
            dteDateOut = mDataHandler.DataValue_Out(ClsGHCalender.GetRelativeDate(dteDateIn, ClsGHCalender.DateType.Gregorian, ClsGHCalender.Directions.Output), Global.System.Data.SqlDbType.DateTime)
        End If

        Return Format(dteDateOut, strFormatDate)

    End Function
    Public Function SetHigriDate(ByVal dteDate As Object) As Object
        Dim ClsGeneralSetting As New Clshrs_GeneralSetting(mPage)
        ClsGeneralSetting.Find("ID=1")
        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)

        Dim mDataHandler As New Venus.Shared.DataHandler

        If IsDBNull(dteDate) Then
            Return Nothing
        End If

        If IsNothing(dteDate) Then
            Return Nothing
        End If

        If dteDate.ToString.Trim = "" Then
            Return Nothing
        End If

        Dim dteDateIn As Date = dteDate
        Dim dteDateOut As Date
        If dteDateIn.Year = 1 Then
            Return Nothing
        End If

        If ClsGeneralSetting.IsHigry Then
            dteDateOut = ClsGHCalender.GetRelativeDate(dteDateIn, ClsGHCalender.DateType.Hijri, ClsGHCalender.Directions.Input)

        Else
            dteDateOut = ClsGHCalender.GetRelativeDate(dteDateIn, ClsGHCalender.DateType.Gregorian, ClsGHCalender.Directions.Input)
        End If
        Return dteDateOut

    End Function

    Public Function CheckMyDate(ByVal strDate As String) As String
        Dim objNav As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Dim strRet As String = objNav.SetLanguage(mPage, "0/1") & ","
        Dim strDiff As String = String.Empty
        If strDate = "__/__/____" Or strDate = "  /  /    " Then
            strRet &= "1"
            Return strRet
        End If
        If SetHigriDate2(strDate, strDiff) = Nothing Then
            strRet &= "0"
        Else
            strRet &= "1"
        End If
        Return strRet
    End Function
    Public Function CheckValidDate(ByVal dteDateIn As Object) As Boolean
        If IsDBNull(dteDateIn) Then
            Return False
        End If
        If IsNothing(dteDateIn) Then
            Return False
        End If
        If dteDateIn.ToString.Trim = "" Then
            Return False
        End If
        If TypeOf (dteDateIn) Is Date Then
            If dteDateIn.Year = 1 Then
                Return False
            End If
        ElseIf TypeOf (dteDateIn) Is String Then
            If dteDateIn.ToString = "  /  /    " Or dteDateIn.ToString = "__/__/____" Then
                Return False
            End If
            Dim strArr() As String = dteDateIn.ToString.Split(" ")(0).Split("/")
            If strArr.Length < 3 Then
                Return False
            End If
            Dim intDay As Integer = CInt(IIf(strArr(0).Trim = "" Or strArr(0).Trim = "__", 0, strArr(0).Trim("_").Trim))
            Dim intMonth As Integer = CInt(IIf(strArr(1).Trim = "" Or strArr(1).Trim = "__", 0, strArr(1).Trim("_").Trim))
            Dim intYear As Integer = CInt(IIf(strArr(2).Trim = "" Or strArr(2).Trim = "____", 0, strArr(2).Trim("_").Trim))
            If intDay <= 0 Or intDay > 31 Then
                Return False
            End If
            If intMonth <= 0 Or intMonth > 12 Then
                Return False
            End If
            If intYear <= 0 Or intYear < 1300 Or intYear > 2070 Then
                Return False
            End If
            If intYear > 1900 Then
                Try
                    Dim dte As Date = New Date(intYear, intMonth, intDay)
                Catch ex As Exception
                    Return False
                End Try
            Else
                If intDay > 30 Then
                    Return False
                End If
            End If
        End If
        Return True
    End Function
    Public Function GetHigriDate2(ByVal dteDate As Object, ByVal strDetails As String) As Object
        Dim isHijri As Int16
        Dim intDiff As Int16 = 0
        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim strFormatDate As String = ConfigurationManager.AppSettings("DATEFORMAT")
        Dim dteDateOut As Object

        Dim ClsGHCalender As New Clssys_GHCalendar(mPage)
        Dim ClsGeneralSetting = New Clshrs_GeneralSetting(mPage)
        ClsGeneralSetting.Find("ID=1")
        If Not CheckValidDate(dteDate) Then
            Return Nothing
        End If
        If strDetails.Trim = String.Empty Then
            If ClsGeneralSetting.IsHigry Then
                isHijri = 1
            Else
                isHijri = 0
            End If
        Else
            Dim strDetailsArr As String() = strDetails.Split(",")
            isHijri = CInt(strDetailsArr(0))
            intDiff = CInt(strDetailsArr(1))
        End If

        If isHijri = 1 Then
            dteDateOut = mDataHandler.DataValue_Out(GetRelativeDate(CDate(dteDate).AddDays(IIf(intDiff > 0, -intDiff, 0)), DateType.Hijri, Directions.Output), Global.System.Data.SqlDbType.DateTime)
        Else
            dteDateOut = mDataHandler.DataValue_Out(GetRelativeDate(dteDate, DateType.Gregorian, Directions.Output), Global.System.Data.SqlDbType.DateTime)
        End If

        dteDateOut = String.Format(dteDateOut, strFormatDate)
        If intDiff > 0 Then
            Dim strArr As String() = dteDateOut.ToString.Split(" ")(0).Split("/")
            Dim intDay As Int16 = CInt(strArr(0)) + intDiff
            dteDateOut = Format(intDay, "00") & "/" & Format(CInt(strArr(1)), "00") & "/" & strArr(2)
        End If

        Return dteDateOut
    End Function
    Public Function SetHigriDate2(ByVal dteDate As Object, ByRef strDetails As String) As Object
        Dim isHijri As Int16
        Dim intDiff As Int16 = 0
        Dim dteDateOut As Object

        If Not CheckValidDate(dteDate) Then
            strDetails = String.Empty
            Return Nothing
        End If

        Dim strArr() As String = dteDate.ToString.Split(" ")(0).Split("/")

        If CInt(strArr(2)) < 1900 Then
            isHijri = 1
            If CInt(strArr(1)) = 2 Then ' Month is 2
                If CInt(strArr(0)) > 28 Then
                    intDiff = CInt(strArr(0)) - 28
                    dteDate = "28/" & Format(CInt(strArr(1)), "00") & "/" & strArr(2)
                End If
            End If
        Else
            isHijri = 0
        End If

        strDetails = isHijri.ToString & "," & intDiff

        If isHijri = 1 Then
            dteDateOut = GetRelativeDate(dteDate, DateType.Hijri, Directions.Input)
        Else
            dteDateOut = GetRelativeDate(dteDate, DateType.Gregorian, Directions.Input)
        End If

        Return CDate(dteDateOut).AddDays(intDiff)
    End Function
    Public Function GetRelativeDate(ByVal iDate As Object, ByVal DateDisplayType As DateType, ByVal oDirection As Directions) As Object

        If IsNothing(iDate) And oDirection = Directions.Output Then
            Return Nothing
        ElseIf IsNothing(iDate) And oDirection = Directions.Input Then
            Return DBNull.Value
        End If

        If (oDirection = Directions.Output And (IsDBNull(iDate) Or IsNothing(iDate) Or iDate.ToString.Trim.ToLower = "null" Or iDate.ToString.Trim = "")) Then
            Return Nothing
        Else
            If (oDirection = Directions.Input And (IsDBNull(iDate) Or IsNothing(iDate) Or iDate.ToString.Trim.ToLower = "null" Or iDate.ToString.Trim = "")) Then
                Return DBNull.Value
            End If
        End If
        If (CDate(iDate).Year < 1300) Then
            Return Nothing
        End If

        If iDate = "1/1/1900" Then
            Return Nothing
        End If

        Dim oDate As Date = iDate

        Dim strFormatDate As String = ConfigurationSettings.AppSettings("DATEFORMAT")
        Dim StrSQLCommand As String = String.Empty
        Dim UmAlQuraCalender As New Global.System.Globalization.UmAlQuraCalendar
        Dim GregorianCalender As New Global.System.Globalization.GregorianCalendar
        Dim IntYear As Integer
        Dim IntMonth As Integer
        Dim IntDay As Integer

        Dim IntTempGYear As Integer
        Dim IntTempGMonth As Integer
        Dim IntTempGDay As Integer
        Dim StrTempGDate As String

        Dim IntTempHYear As Integer
        Dim IntTempHMonth As Integer
        Dim IntTempHDay As Integer
        Dim StrTempHDate As String

        Dim StrDate As String = String.Empty
        Dim StrReturnDate As String
        Dim CurrentDate As Date
        Dim GDate As Date
        Dim HDate As Date

        Dim nav As New Venus.Shared.Web.NavigationHandler(mConnectionString)

        Try

            '---------------------------------------------------------------------------'
            'This part is to prepare the data to get the relative date from the databese' 
            '---------------------------------------------------------------------------'
            IntYear = DatePart(DateInterval.Year, oDate)
            IntMonth = DatePart(DateInterval.Month, oDate)
            IntDay = DatePart(DateInterval.Day, oDate)
            StrDate = Format(IntDay, "00") & "/" & Format(IntMonth, "00") & "/" & IntYear.ToString

            '---------------------------------------------------------------------------'
            'This part to get the relative date according to microsoft standerd calender' 
            '---------------------------------------------------------------------------'
            If IntYear <= 1600 Then
                IntTempGYear = GregorianCalender.GetYear(oDate)
                IntTempGMonth = GregorianCalender.GetMonth(oDate)
                IntTempGDay = GregorianCalender.GetDayOfMonth(oDate)
                StrTempGDate = Format(IntTempGDay, "00") + "/" + Format(IntTempGMonth, "00") + "/" + IntTempGYear.ToString
            Else
                IntTempHYear = UmAlQuraCalender.GetYear(oDate)
                IntTempHMonth = UmAlQuraCalender.GetMonth(oDate)
                IntTempHDay = UmAlQuraCalender.GetDayOfMonth(oDate)
                StrTempHDate = Format(IntTempHDay, "00") + "/" + Format(IntTempHMonth, "00") + "/" + IntTempHYear.ToString
            End If

            '---------------------------------------------------------------------------'
            'Get the Relative Date according to the input paramters and depending on the'
            'display language                                                           '
            '---------------------------------------------------------------------------'
            Select Case DateDisplayType
                Case DateType.Hijri

                    Select Case oDirection
                        Case Directions.Input
                            If IntYear < 1600 Then
                                StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
                                mSqlCommand = New SqlClient.SqlCommand
                                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                                mSqlCommand.CommandType = CommandType.Text
                                mSqlCommand.CommandText = StrSQLCommand
                                mSqlCommand.Connection.Open()
                                StrReturnDate = mSqlCommand.ExecuteScalar
                                mSqlCommand.Connection.Close()
                                If StrReturnDate Is Nothing Then
                                    StrReturnDate = HijriToGreg(StrDate, strFormatDate)
                                    If Not IsNothing(StrReturnDate) Then
                                        '============================== Insert GH Date if not found [Start]
                                        Dim strInser As String = "Set Dateformat dmy  Insert Into sys_GHCalendar Values ('" & StrReturnDate & "','" & StrDate & "');"
                                        mSqlCommand.CommandText = strInser
                                        mSqlCommand.Connection.Open()
                                        mSqlCommand.ExecuteNonQuery()
                                        mSqlCommand.Connection.Close()
                                        '============================== Insert GH Date if not found [ END ]
                                    Else
                                        StrReturnDate = StrTempGDate
                                    End If

                                End If
                            Else
                                Return oDate
                            End If
                        Case Directions.Output
                            StrSQLCommand = "Set Dateformat dmy Select HDate from sys_GHCalendar Where GDate between '" & StrDate & " 00:00:00 " & "' And '" & StrDate & " 23:00:00 '"
                            mSqlCommand = New SqlClient.SqlCommand
                            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                            mSqlCommand.CommandType = CommandType.Text
                            mSqlCommand.CommandText = StrSQLCommand
                            mSqlCommand.Connection.Open()
                            StrReturnDate = mSqlCommand.ExecuteScalar
                            mSqlCommand.Connection.Close()
                            If StrReturnDate Is Nothing Then
                                Try
                                    '============================== Insert GH Date if not found [Start]
                                    Dim strInser As String = "Set Dateformat dmy  Insert Into sys_GHCalendar Values ('" & StrDate & "','" & StrTempHDate & "');"
                                    mSqlCommand.CommandText = strInser
                                    mSqlCommand.Connection.Open()
                                    mSqlCommand.ExecuteNonQuery()
                                    mSqlCommand.Connection.Close()
                                    '============================== Insert GH Date if not found [ END ]
                                    StrReturnDate = StrTempHDate
                                Catch ex As SqlClient.SqlException
                                    If ex.Number = 2601 Then
                                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(mPage, nav.SetLanguage(mPage, "Found invalid saved Dates/يوجد تواريخ محفوظة خطأ"))
                                        Return Nothing
                                    End If

                                End Try
                            End If
                    End Select

                Case DateType.Gregorian
                    Select Case oDirection
                        Case Directions.Input
                            If IntYear < 1600 Then

                                StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
                                mSqlCommand = New SqlClient.SqlCommand
                                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                                mSqlCommand.CommandType = CommandType.Text
                                mSqlCommand.CommandText = StrSQLCommand
                                mSqlCommand.Connection.Open()
                                StrReturnDate = mSqlCommand.ExecuteScalar
                                mSqlCommand.Connection.Close()
                                If StrReturnDate Is Nothing Then
                                    StrReturnDate = HijriToGreg(StrDate, strFormatDate)
                                    If Not IsNothing(StrReturnDate) Then
                                        '============================== Insert GH Date if not found [Start]
                                        Dim strInser As String = "Set Dateformat dmy  Insert Into " & Me.mTable & " Values ('" & StrReturnDate & "','" & StrDate & "');"
                                        mSqlCommand.CommandText = strInser
                                        mSqlCommand.Connection.Open()
                                        mSqlCommand.ExecuteNonQuery()
                                        mSqlCommand.Connection.Close()
                                        '============================== Insert GH Date if not found [ END ]
                                    Else
                                        StrReturnDate = StrTempGDate
                                    End If
                                End If

                            Else
                                Return oDate
                            End If
                        Case Directions.Output
                            Return oDate
                    End Select
            End Select
            Return StrReturnDate
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSQLCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
            'Finally
            '    mSqlCommand.Connection.Close()
        End Try
    End Function
    Public Shared Function IsHijri(ByVal Hijri As String) As Boolean
        Dim allFormats() As String = {"yyyy/MM/dd",
                                      "yyyy/M/d",
                                      "dd/MM/yyyy",
                                      "d/M/yyyy",
                                      "dd/M/yyyy",
                                      "d/MM/yyyy",
                                      "yyyy-MM-dd",
                                      "yyyy-M-d",
                                      "dd-MM-yyyy",
                                      "d-M-yyyy",
                                      "dd-M-yyyy",
                                      "d-MM-yyyy",
                                      "yyyy MM dd",
                                      "yyyy M d",
                                      "dd MM yyyy",
                                      "d M yyyy",
                                      "dd M yyyy",
                                      "d MM yyyy"}

        Dim arCul As CultureInfo
        Dim enCul As CultureInfo
        Dim h As UmAlQuraCalendar
        Dim g As GregorianCalendar
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New UmAlQuraCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

        arCul.DateTimeFormat.Calendar = h
        enCul.DateTimeFormat.Calendar = g

        If Hijri.Length > 0 Then
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(Hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                If tempDate.Year >= 1900 And tempDate.Year <= 2100 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function
    Public Shared Function IsGreg(ByVal Greg As String) As Boolean
        Dim allFormats() As String = {"yyyy/MM/dd",
                                      "yyyy/M/d",
                                      "dd/MM/yyyy",
                                      "d/M/yyyy",
                                      "dd/M/yyyy",
                                      "d/MM/yyyy",
                                      "yyyy-MM-dd",
                                      "yyyy-M-d",
                                      "dd-MM-yyyy",
                                      "d-M-yyyy",
                                      "dd-M-yyyy",
                                      "d-MM-yyyy",
                                      "yyyy MM dd",
                                      "yyyy M d",
                                      "dd MM yyyy",
                                      "d M yyyy",
                                      "dd M yyyy",
                                      "d MM yyyy"}

        Dim arCul As CultureInfo
        Dim enCul As CultureInfo
        Dim h As UmAlQuraCalendar
        Dim g As GregorianCalendar
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New UmAlQuraCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

        arCul.DateTimeFormat.Calendar = h
        enCul.DateTimeFormat.Calendar = g

        If Greg.Length > 0 Then
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(Greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                If tempDate.Year >= 1900 And tempDate.Year <= 2100 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function
    Public Shared Function HijriToGreg(ByVal hijri As String, ByVal format As String) As String
        Dim allFormats() As String = {"yyyy/MM/dd",
                                      "yyyy/M/d",
                                      "dd/MM/yyyy",
                                      "d/M/yyyy",
                                      "dd/M/yyyy",
                                      "d/MM/yyyy",
                                      "yyyy-MM-dd",
                                      "yyyy-M-d",
                                      "dd-MM-yyyy",
                                      "d-M-yyyy",
                                      "dd-M-yyyy",
                                      "d-MM-yyyy",
                                      "yyyy MM dd",
                                      "yyyy M d",
                                      "dd MM yyyy",
                                      "d M yyyy",
                                      "dd M yyyy",
                                      "d MM yyyy"}

        Dim arCul As CultureInfo
        Dim enCul As CultureInfo
        Dim h As UmAlQuraCalendar
        Dim g As GregorianCalendar
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New UmAlQuraCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

        arCul.DateTimeFormat.Calendar = h
        enCul.DateTimeFormat.Calendar = g

        If hijri.Length > 0 Then
            If IsHijri(hijri) = False Then
                hijri = GregToHijri(hijri, "dd/MM/yyyy")
            End If
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.ToString(format, enCul.DateTimeFormat)
            Catch ex As Exception
                Return HijriToGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            End Try
        Else
            Return HijriToGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        End If
    End Function
    Public Shared Function GregToHijri(ByVal Greg As String, ByVal format As String) As String
        Dim allFormats() As String = {"yyyy/MM/dd",
                              "yyyy/M/d",
                              "dd/MM/yyyy",
                              "d/M/yyyy",
                              "dd/M/yyyy",
                              "d/MM/yyyy",
                              "yyyy-MM-dd",
                              "yyyy-M-d",
                              "dd-MM-yyyy",
                              "d-M-yyyy",
                              "dd-M-yyyy",
                              "d-MM-yyyy",
                              "yyyy MM dd",
                              "yyyy M d",
                              "dd MM yyyy",
                              "d M yyyy",
                              "dd M yyyy",
                              "d MM yyyy"}

        Dim arCul As CultureInfo
        Dim enCul As CultureInfo
        Dim h As UmAlQuraCalendar
        Dim g As GregorianCalendar
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New UmAlQuraCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

        arCul.DateTimeFormat.Calendar = h
        enCul.DateTimeFormat.Calendar = g

        If Greg.Length > 0 Then
            If IsGreg(Greg) = False Then
                Greg = HijriToGreg(Greg, "dd/MM/yyyy")
            End If
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(Greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.ToString(format, arCul.DateTimeFormat)
            Catch ex As Exception
                Return GregToHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            End Try
        Else
            Return GregToHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        End If
    End Function
    Public Shared Function FormatGreg(ByVal Greg As String, ByVal format As String) As DateTime
        Dim allFormats() As String = {"yyyy/MM/dd",
                              "yyyy/M/d",
                              "dd/MM/yyyy",
                              "d/M/yyyy",
                              "dd/M/yyyy",
                              "d/MM/yyyy",
                              "yyyy-MM-dd",
                              "yyyy-M-d",
                              "dd-MM-yyyy",
                              "d-M-yyyy",
                              "dd-M-yyyy",
                              "d-MM-yyyy",
                              "yyyy MM dd",
                              "yyyy M d",
                              "dd MM yyyy",
                              "d M yyyy",
                              "dd M yyyy",
                              "d MM yyyy"}

        Dim arCul As CultureInfo
        Dim enCul As CultureInfo
        Dim h As UmAlQuraCalendar
        Dim g As GregorianCalendar
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New UmAlQuraCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

        arCul.DateTimeFormat.Calendar = h
        enCul.DateTimeFormat.Calendar = g

        If Greg.Length > 0 Then
            If IsGreg(Greg) = False Then
                Greg = HijriToGreg(Greg, "dd/MM/yyyy")
            End If
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(Greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate
            Catch ex As Exception
                Return FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            End Try
        Else
            Return FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        End If
    End Function
    Public Shared Function FormatGregString(ByVal Greg As String, ByVal format As String) As String
        Dim allFormats() As String = {"yyyy/MM/dd",
                              "yyyy/M/d",
                              "dd/MM/yyyy",
                              "d/M/yyyy",
                              "dd/M/yyyy",
                              "d/MM/yyyy",
                              "yyyy-MM-dd",
                              "yyyy-M-d",
                              "dd-MM-yyyy",
                              "d-M-yyyy",
                              "dd-M-yyyy",
                              "d-MM-yyyy",
                              "yyyy MM dd",
                              "yyyy M d",
                              "dd MM yyyy",
                              "d M yyyy",
                              "dd M yyyy",
                              "d MM yyyy"}

        Dim arCul As CultureInfo
        Dim enCul As CultureInfo
        Dim h As UmAlQuraCalendar
        Dim g As GregorianCalendar
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New UmAlQuraCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

        arCul.DateTimeFormat.Calendar = h
        enCul.DateTimeFormat.Calendar = g

        If Greg.Length > 0 Then
            If IsGreg(Greg) = False Then
                Greg = HijriToGreg(Greg, "dd/MM/yyyy")
            End If
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(Greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.ToString(format, enCul.DateTimeFormat)
            Catch ex As Exception
                Return FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            End Try
        Else
            Return FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        End If
    End Function
    Public Shared Function FormatHijri(ByVal Hijri As String, ByVal format As String) As String
        Dim allFormats() As String = {"yyyy/MM/dd",
                              "yyyy/M/d",
                              "dd/MM/yyyy",
                              "d/M/yyyy",
                              "dd/M/yyyy",
                              "d/MM/yyyy",
                              "yyyy-MM-dd",
                              "yyyy-M-d",
                              "dd-MM-yyyy",
                              "d-M-yyyy",
                              "dd-M-yyyy",
                              "d-MM-yyyy",
                              "yyyy MM dd",
                              "yyyy M d",
                              "dd MM yyyy",
                              "d M yyyy",
                              "dd M yyyy",
                              "d MM yyyy"}

        Dim arCul As CultureInfo
        Dim enCul As CultureInfo
        Dim h As UmAlQuraCalendar
        Dim g As GregorianCalendar
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New UmAlQuraCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

        arCul.DateTimeFormat.Calendar = h
        enCul.DateTimeFormat.Calendar = g

        If Hijri.Length > 0 Then
            If IsHijri(Hijri) = False Then
                Hijri = GregToHijri(Hijri, "dd/MM/yyyy")
            End If
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(Hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.ToString(format, arCul.DateTimeFormat)
            Catch ex As Exception
                Return FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            End Try
        Else
            Return FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        End If
    End Function

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
            mPage.Session.Item("UserID") = GetUserID(Me.mDataBaseUserID)
            'Me.mDataBaseUserRelatedID = mPage.Session.Item("UserID")
        Else
            Me.mDataBaseUserRelatedID = mPage.Session.Item("UserID")
        End If
    End Function

    Private Function GetUserID(ByVal UserCode As String) As Integer
        Dim StrSelectCommand As String = String.Empty
        Dim ObjSqlcommand As New SqlClient.SqlCommand
        Dim IntID As Integer
        Try
            StrSelectCommand = "Select ID From Sys_users where code='" & UserCode & "' And isNull(CancelDate,'')=''"
            ObjSqlcommand.Connection = New SqlClient.SqlConnection(Me.mConnectionString)
            ObjSqlcommand.CommandType = CommandType.Text
            ObjSqlcommand.CommandText = StrSelectCommand
            ObjSqlcommand.Connection.Open()
            IntID = ObjSqlcommand.ExecuteScalar()
            ObjSqlcommand.Connection.Close()
            Return IntID
        Catch ex As Exception
            Return 0
        Finally
            ObjSqlcommand.Connection.Close()
        End Try
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
        StrSelectCommand = " SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.Columns " &
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
    Protected Sub finalized()

    End Sub
#End Region

End Class
