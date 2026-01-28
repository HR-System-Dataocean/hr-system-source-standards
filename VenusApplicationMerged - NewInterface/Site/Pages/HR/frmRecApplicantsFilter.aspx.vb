Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class Pages_HR_frmRecApplicantsFilter
    Inherits MainPage

#Region "Public Decleration"

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Description :  Public Form Declration
    '=====================================================================

    Private ClsRec_ApplicantsFilter As ClsRec_ApplicantsFilter

    'Private clsMainOtherFields As clsSys_MainOtherFields
    'Const CSave = 1
    'Const CDelete = 3
    'Const CProperty = 5
    'Const Cprint = 7
    'Const CRemarks = 9

    'Const CFirst = 0
    'Const CPrevios = 1
    'Const CNext = 2
    'Const CLast = 3

    'Const RegUser = 2
    'Const RegDate = 6
    'Const CancelDate = 10

    'Const csOtherFields = 11


#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsRec_ApplicantsFilter = New ClsRec_ApplicantsFilter(Me.Page)
        Dim clsObjects As New Clssys_Objects(Page)
        Dim clsSearch As New Clssys_Searchs(Page)

        ''Search
        ''========================================================================
        Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)


        Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)

        SetScreenInformation()

        'ClsBranches.AddNotificationOnChange(Page)
        If Not IsPostBack Then
            Page.Session.Add("ConnectionString", ClsRec_ApplicantsFilter.ConnectionString)
            ClsRec_ApplicantsFilter.AddOnChangeEventToControls("frmRecApplicantsFilter", Page, UltraWebTab1)
            Setsetting(IntId)
            Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
        End If
        If clsObjects.Find("code='" & ClsRec_ApplicantsFilter.Table.Trim & "'") Then
            If clsSearch.Find("ObjectID=" & clsObjects.ID) Then
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & clsSearch.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If

        Dim IntrecordID As Integer = 0
        If (txtCode.Text <> "") Then
            ClsRec_ApplicantsFilter.Find(" Code = '" & txtCode.Text & "'")
            IntrecordID = ClsRec_ApplicantsFilter.ID
            If (IntrecordID > 0) Then
                'ClsBranches.AddDataUpdateSchedules(Page, UltraWebTab1, "frmBranches", IntrecordID)
            End If
        End If

        Ajax.Utility.RegisterTypeForAjax(GetType(Pages_HR_frmRecApplicantsFilter))
        CreateOtherFields(IntrecordID)
        If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

    End Sub

    Protected Sub txtCode_TextChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsRec_ApplicantsFilter = New ClsRec_ApplicantsFilter(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsRec_ApplicantsFilter.ConnectionString)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim ObjDS As New Data.DataSet

        Select Case e.CommandArgument
            Case "New"
                AfterOperation()
            Case "SaveNew"
                SavePart()
                AfterOperation()
            Case "Save"
                SavePart()
            Case "Delete"
                ClsRec_ApplicantsFilter.Find("Code='" & txtCode.Text & "'")
                ClsRec_ApplicantsFilter.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()

            Case "Property"
                If ClsRec_ApplicantsFilter.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsRec_ApplicantsFilter.ID & "&TableName=" & ClsRec_ApplicantsFilter.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Remarks"
                If ClsRec_ApplicantsFilter.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsRec_ApplicantsFilter.ID & "&TableName=" & ClsRec_ApplicantsFilter.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                ClsRec_ApplicantsFilter = New ClsRec_ApplicantsFilter(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsRec_ApplicantsFilter.Table
                ClsRec_ApplicantsFilter.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsRec_ApplicantsFilter.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsRec_ApplicantsFilter.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")

            Case "Exit"
                ClsRec_ApplicantsFilter.Find(" Code= '" & txtCode.Text & "'")

                If ClsRec_ApplicantsFilter.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsRec_ApplicantsFilter.DataSet
                    If Not AssignValue(ClsRec_ApplicantsFilter) Then
                        Exit Sub
                    End If

                    'Excute .CheckDeff to check for any change had happened in the Screen data
                    If ClsRec_ApplicantsFilter.CheckDiff(ClsRec_ApplicantsFilter, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If

            Case "First"

                ClsRec_ApplicantsFilter.FirstRecord()
                GetValues(ClsRec_ApplicantsFilter)

            Case "Previous"
                ClsRec_ApplicantsFilter.Find("Code='" & txtCode.Text & "'")
                If Not ClsRec_ApplicantsFilter.previousRecord() Then
                    ClsRec_ApplicantsFilter.Find("Code='" & txtCode.Text & "'")
                    'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " This is the first page ")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues(ClsRec_ApplicantsFilter)

            Case "Next"
                ClsRec_ApplicantsFilter.Find("Code='" & txtCode.Text & "'")
                If Not ClsRec_ApplicantsFilter.NextRecord() Then
                    ClsRec_ApplicantsFilter.Find("Code='" & txtCode.Text & "'")
                    'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " This is the last page ")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues(ClsRec_ApplicantsFilter)

            Case "Last"

                ClsRec_ApplicantsFilter.LastRecord()
                GetValues(ClsRec_ApplicantsFilter)

        End Select


    End Sub

#End Region

#Region "Private Function"

    '=============================================================
    'Project      : Venus 
    'Module       : Payroll
    'Developer    : [0256]
    'Date Created : 22-04-2008
    'Description  : Checks for other fields created for current screen (Object) and generats them 
    '=============================================================

    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        'Dim dsOtherFields As New Data.DataSet
        'Dim clsSysObjects As New Clssys_Objects(Me.Page)
        'Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        'clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)

        'If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsBranches.Table) = True Then
        '    Dim StrTablename As String


        '    ClsBranches = New Clssys_Branches(Page)
        '    StrTablename = ClsBranches.Table

        '    clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")


        '    Dim objDS As New Data.DataSet
        '    clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID & _
        '                            " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID & _
        '                            " And sys_OtherFields.CancelDate is Null ")

        '    objDS = clsOtherFieldsData.DataSet


        '    name.Text = ""
        '    realname.Text = ""

        '    If objDS.Tables(0).Rows.Count > 0 Then
        '        clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmBranches")
        '    Else
        '        clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmBranches")
        '    End If
        '    'clsMainOtherFields.CollectDataAndSave(value.Text, ClsTransactions.Table, IntRecordID)
        'End If
    End Function

    '================================== CreateOtherFields [End]



    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: Assign value to ClsProjects properties from Textboxes
    '=====================================================================

    Private Function AssignValue(ByRef ClsRec_ApplicantsFilter As ClsRec_ApplicantsFilter) As Boolean
        Dim ClsCities As New Clssys_Cities(Page)
        Try
            With ClsRec_ApplicantsFilter
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .SalaryPowerPCT = txtSalaryPower.Value
                .ExpPowerPCT = txtExpPower.Value
                .EvaluationPowerPCT = txtEvaluaPower.Value

            End With
            Return True

        Catch ex As Exception
            Return False

        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: Get Values from ClsProjects properties to Textboxs
    '=====================================================================

    Public Function GetValues(ByRef ClsRec_ApplicantsFilter As ClsRec_ApplicantsFilter) As Boolean
        Dim ClsUser As New Clssys_Users(Page)

        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        'Dim ClsBranches As New  Clssys_Branches(Page)
        Try
            SetToolBarDefaults()
            With ClsRec_ApplicantsFilter

                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtSalaryPower.Value = .SalaryPowerPCT
                txtExpPower.Value = .ExpPowerPCT
                txtEvaluaPower.Value = .EvaluationPowerPCT


                If Not ClsRec_ApplicantsFilter.RegUserID = Nothing Then
                    ClsUser.Find("ID=" & ClsRec_ApplicantsFilter.RegUserID)
                End If
                If ClsUser.EngName = Nothing Then
                    lblRegUserValue.Text = ""
                Else
                    lblRegUserValue.Text = ClsUser.EngName
                End If

                If Convert.ToDateTime(ClsRec_ApplicantsFilter.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = Convert.ToDateTime(ClsRec_ApplicantsFilter.RegDate).Date
                End If

                If ClsRec_ApplicantsFilter.CancelDate = Nothing Then
                    lblCancelDateValue.Text = ""
                    lblCancelDateValue.Enabled = True
                Else
                    lblCancelDateValue.Text = Convert.ToDateTime(ClsRec_ApplicantsFilter.CancelDate).Date
                    lblCancelDateValue.Enabled = False
                End If
                '-------------------------------0257 MODIFIED-----------------------------------------
                Dim str As String = ""
                GetChildBranches(.ID, .ConnectionString, str)
                str = str.Trim(",")

            End With
            If (ClsRec_ApplicantsFilter.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If

            'Venus.Shared.Web.ClientSideActions.SetToolBarPermission(Me, ClsBranches.ConnectionString, TlbMainToolbar, ClsBranches.DataBaseUserRelatedID, ClsBranches.GroupID, StrMode)
            'Venus.Shared.Web.ClientSideActions.SetToolBarRecordPermission(Me, ClsBranches.ConnectionString, TlbMainToolbar, ClsBranches.DataBaseUserRelatedID, ClsBranches.GroupID, ClsBranches.Table, ClsBranches.ID)
            If Not ClsRec_ApplicantsFilter.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False

            End If


            If Page.IsPostBack Then CreateOtherFields(ClsRec_ApplicantsFilter.ID)

            Return True

        Catch ex As Exception
            Return False

        End Try
    End Function
    '-------------------------------0257 MODIFIED-----------------------------------------
    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps: Get Child Branches
    '=====================================================================

    Private Function GetChildBranches(ByVal BranchID As Integer, ByVal Connection As String, ByRef RetStr As String) As Boolean


        'Dim strSelect As String = " Select ID from hrs_Professions where ParentID = " & BranchID

        'Dim DS As New Data.DataSet

        'DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, Data.CommandType.Text, strSelect)

        'If (DS.Tables(0).Rows.Count > 0) Then
        '    For Each row As Data.DataRow In DS.Tables(0).Rows
        '        RetStr = RetStr & row("ID") & ","
        '        GetChildBranches(row("ID"), Connection, RetStr)
        '    Next

        'Else
        '    Return True

        'End If



    End Function
    '------------------------------=============-----------------------------------------


    '=====================================================================
    'Created by : DataOcean
    'Date : 16/07/2007
    'Steps:         
    '               - Determine function direction (Update/Save)
    '=====================================================================

    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsRec_ApplicantsFilter = New ClsRec_ApplicantsFilter(Page)

        ClsRec_ApplicantsFilter.Find("Rec_ApplicantsFilter.Code='" & txtCode.Text & "'")
        If Not AssignValue(ClsRec_ApplicantsFilter) Then
            Exit Function
        End If

        If ClsRec_ApplicantsFilter.ID > 0 Then
            ClsRec_ApplicantsFilter.Update("Rec_ApplicantsFilter.Code='" & txtCode.Text & "'")
        Else
            ClsRec_ApplicantsFilter.Save()
        End If
        ClsRec_ApplicantsFilter = New ClsRec_ApplicantsFilter(Page)
        'clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)

        ClsRec_ApplicantsFilter.Find(" Code='" & txtCode.Text & "'")

        'clsMainOtherFields.CollectDataAndSave(value.Text, ClsBranches.Table, ClsBranches.ID)
        'value.Text = ""

    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 09/07/2007
    'Description : This function set the Screen Information such as (Security,max length of textboxs,page toolbar) 
    '=====================================================================

    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")

        Try
            With ClsRec_ApplicantsFilter
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                'Venus.Shared.Web.ClientSideActions.SetPageToolTips(Me, .ConnectionString, "UltraWebTab1")
                'Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)

            End With

        Catch ex As Exception

        End Try
    End Function

    '=====================================================================
    'Created by : DataOcean
    'Date : 10/07/2007
    'Steps:
    '               - Exit from current screen 
    '               - Return to Basicsearch screen
    '               - Refresh Basicsearch screen
    '=====================================================================

    Private Function SetReturnback() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim StrTargetControl As String = Request.QueryString.Item("TargetControl")
        Select Case StrMode
            Case "R"
                Venus.Shared.Web.ClientSideActions.DoReturnBack(Page, StrTargetControl)
            Case Else
                Venus.Shared.Web.ClientSideActions.DoRefresh(Page)
        End Select
    End Function

    '========================================================================
    'Created by : 
    'Date : 31/7/2007
    'Description :  Get Values of selected row
    '========================================================================
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsRec_ApplicantsFilter = New ClsRec_ApplicantsFilter(Page)
        If IntId > 0 Then
            ClsRec_ApplicantsFilter.Find("ID=" & IntId)
            GetValues(ClsRec_ApplicantsFilter)
        Else
            ImageButton_Delete.Enabled = False
        End If
    End Function

    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsRec_ApplicantsFilter = New ClsRec_ApplicantsFilter(Me)



        Try
            ClsRec_ApplicantsFilter.Find("Code='" & txtCode.Text & "'")
            IntId = ClsRec_ApplicantsFilter.ID
            txtEngName.Focus()
            If ClsRec_ApplicantsFilter.ID > 0 Then
                GetValues(ClsRec_ApplicantsFilter)
                StrMode = "E"
            Else
                If ClsRec_ApplicantsFilter.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
            End If

            SetToolBarDefaults()
            'Venus.Shared.Web.ClientSideActions.SetToolBarPermission(Me, ClsBranches.ConnectionString, TlbMainToolbar, ClsBranches.DataBaseUserRelatedID, ClsBranches.GroupID, StrMode)
            'Venus.Shared.Web.ClientSideActions.SetToolBarRecordPermission(Me, ClsBranches.ConnectionString, TlbMainToolbar, ClsBranches.DataBaseUserRelatedID, ClsBranches.GroupID, ClsBranches.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If

        Catch ex As Exception

        End Try

    End Function

    Private Function SetToolBarDefaults() As Boolean
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function


    Private Function AfterOperation() As Boolean
        ClsRec_ApplicantsFilter.Clear()
        GetValues(ClsRec_ApplicantsFilter)
        UltraWebTab1.SelectedTab = 0
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ' ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
    End Function

    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        txtSalaryPower.Value = String.Empty
        txtExpPower.Text = String.Empty
        txtEvaluaPower.Text = String.Empty


        ImageButton_Delete.Enabled = False
        lblRegUserValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        txtEngName.Focus()
    End Function




    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        '=====================================Load Data Update Schedules========================

        '--------------Variable Attributes-----------------
        'Dim formName As String = FormName
        '--------------------------------------------------

        Dim controlName As String = String.Empty
        ClsRec_ApplicantsFilter = New ClsRec_ApplicantsFilter(Page)
        ClsRec_ApplicantsFilter.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsRec_ApplicantsFilter.ID

        If (recordID > 0) Then


            Dim clsForms As New ClsSys_Forms(Page)
            clsForms.Find(" code = REPLACE('" & formName & "',' ','')")

            Dim clsFormsControls As New Clssys_FormsControls(Page)
            clsFormsControls.Find(" FormID=" & clsForms.ID)

            Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()

            For Each row As Data.DataRow In tab.Rows


                clsFormsControls.Find(" FormID=" & clsForms.ID & " And Name='" & row("Name") & "'")
                Dim sys_Fields As New Clssys_Fields(Page)
                sys_Fields.Find(" ID=" & clsFormsControls.FieldID)
                If (sys_Fields.FieldName.Trim() = "Code" Or sys_Fields.FieldName.Trim() = "Number" Or sys_Fields.FieldName.Trim() = "ID") Then
                    Continue For
                End If
                Dim currCtrl As Control = Me.FindControl(row("Name"))

                '-------------------------------0257 MODIFIED-----------------------------------------
                Dim bIsArabic As Boolean = IIf(IsDBNull(row("IsArabic")), False, row("IsArabic"))
                If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                    'If (TypeOf (currCtrl) Is TextBox) Then
                ElseIf (TypeOf (currCtrl) Is TextBox) Then
                    '-------------------------------=============-----------------------------------------


                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")

                ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then

                    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")

                End If

                'txtLicenseNumber.Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e," & fieldID & "," & recordID & ")")
            Next
        End If
        '=======================================End Load Data Update Schedules=====================
    End Sub
#End Region

#Region "Shared Functions"

    <System.Web.Services.WebMethod()> _
    Public Shared Function Get_Searched_Description(ByVal IntSearchId As Integer, ByVal strCode As String) As String

        Dim dsSearchs As New Data.DataSet
        Find("sys_Searchs", " sys_Searchs.Id = " & IntSearchId, dsSearchs)

        Dim dsObjects As New Data.DataSet
        Find("sys_Objects", " sys_Objects.Id = " & dsSearchs.Tables(0).Rows(0).Item("ObjectID"), dsObjects)

        Return GetFieldDescription(strCode, dsObjects.Tables(0).Rows(0).Item("Code"))
    End Function

    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As Data.DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table
        Dim mSqlDataAdapter As New Data.SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter & " And CancelDate IS Null", " Where CancelDate IS Null")
            StrSelectCommand = StrSelectCommand '& orderByStr
            mSqlDataAdapter = New Data.SqlClient.SqlDataAdapter(StrSelectCommand, ConnStr)
            DataSet = New Data.DataSet
            mSqlDataAdapter.Fill(DataSet)
            If DataSet.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Shared Function GetFieldDescription(ByVal StrCode As String, ByVal StrTableName As String) As String
        Dim StrReturnData As Object
        Try
            StrReturnData = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, " Select EngName + '/' + ArbName From " & StrTableName & " Where Code = '" & StrCode.ToString.TrimStart.TrimEnd & "'")
            If IsNothing(StrReturnData) Then Return "/"
            If IsDBNull(StrReturnData) Then Return "/"
            Return StrReturnData
        Catch ex As Exception
            Return "/"
        End Try

    End Function

#End Region

End Class
