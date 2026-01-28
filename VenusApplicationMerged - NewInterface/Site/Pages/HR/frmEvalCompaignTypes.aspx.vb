Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmEvalCompaignTypes
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsEvaluationCompainTypes As ClsEval_EvaluationCompainTypes
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEvaluationCompainTypes = New ClsEval_EvaluationCompainTypes(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsEvaluationCompainTypes.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsEvaluationCompainTypes.ConnectionString)
                ClsEvaluationCompainTypes.AddOnChangeEventToControls("frmEvalCompaignTypes", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsEvaluationCompainTypes.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsEvaluationCompainTypes.ID
                If (IntrecordID > 0) Then
                    SetScreenInformation("E")
                Else
                    SetScreenInformation("N")
                End If
            Else
                SetScreenInformation("N")
            End If
            CreateOtherFields(IntrecordID)
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEvaluationCompainTypes.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsEvaluationCompainTypes = New ClsEval_EvaluationCompainTypes(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvaluationCompainTypes.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsEvaluationCompainTypes.ID > 0 Then
                    ClsEvaluationCompainTypes.Update("code='" & txtCode.Text & "'")
                Else
                    ClsEvaluationCompainTypes.Save()
                End If
                ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEvaluationCompainTypes.Table, ClsEvaluationCompainTypes.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsEvaluationCompainTypes.ID > 0 Then
                    ClsEvaluationCompainTypes.Update("code='" & txtCode.Text & "'")
                Else
                    ClsEvaluationCompainTypes.Save()
                End If
                ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEvaluationCompainTypes.Table, ClsEvaluationCompainTypes.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsEvaluationCompainTypes.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEvaluationCompainTypes.ID & "&TableName=" & ClsEvaluationCompainTypes.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEvaluationCompainTypes.ID & "&TableName=" & ClsEvaluationCompainTypes.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEvaluationCompainTypes.Table
                ClsEvaluationCompainTypes.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsEvaluationCompainTypes.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEvaluationCompainTypes.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEvaluationCompainTypes.Find(" Code= '" & txtCode.Text & "'")
                If ClsEvaluationCompainTypes.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsEvaluationCompainTypes.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsEvaluationCompainTypes.CheckDiff(ClsEvaluationCompainTypes, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsEvaluationCompainTypes.FirstRecord()
                GetValues()
            Case "Previous"
                ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
                If Not ClsEvaluationCompainTypes.previousRecord() Then
                    ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
                If Not ClsEvaluationCompainTypes.NextRecord() Then
                    ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsEvaluationCompainTypes.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function AssignValues() As Boolean
        Try
            With ClsEvaluationCompainTypes
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .DownLevel = CheckBox_DownLevel.Checked
                .UpLevel = CheckBox_UpLevel.Checked
                .SameLevel = CheckBox_SameLevel.Checked
                .IsRondome = CheckBox_AllLevel.Checked
                .IsOutSide = CheckBox_ExternalLevel.Checked
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsEvaluationCompainTypes

                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                CheckBox_DownLevel.Checked = .DownLevel
                CheckBox_UpLevel.Checked = .UpLevel
                CheckBox_SameLevel.Checked = .SameLevel
                CheckBox_AllLevel.Checked = .IsRondome
                CheckBox_ExternalLevel.Checked = .IsOutSide
                CheckBox_DownLevel_CheckedChanged(Nothing, Nothing)
                CheckBox_UpLevel_CheckedChanged(Nothing, Nothing)
                CheckBox_SameLevel_CheckedChanged(Nothing, Nothing)
                CheckBox_AllLevel_CheckedChanged(Nothing, Nothing)
                CheckBox_ExternalLevel_CheckedChanged(Nothing, Nothing)
            End With

            If Not ClsEvaluationCompainTypes.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEvaluationCompainTypes.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsEvaluationCompainTypes.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsEvaluationCompainTypes.RegDate).Date
            End If
            If ClsEvaluationCompainTypes.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsEvaluationCompainTypes.CancelDate).Date
            End If
            If Not ClsEvaluationCompainTypes.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()

            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvaluationCompainTypes.ConnectionString)
            If (ClsEvaluationCompainTypes.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsEvaluationCompainTypes.ConnectionString, ClsEvaluationCompainTypes.DataBaseUserRelatedID, ClsEvaluationCompainTypes.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEvaluationCompainTypes.ConnectionString, ClsEvaluationCompainTypes.DataBaseUserRelatedID, ClsEvaluationCompainTypes.GroupID, ClsEvaluationCompainTypes.Table, ClsEvaluationCompainTypes.ID)
            If Not ClsEvaluationCompainTypes.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsEvaluationCompainTypes.ID)
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function
    Public Function SetToolBarPermission(ByVal pgSender As System.Web.UI.Page, ByVal ConnectionString As String, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal Mode As String) As Boolean
        Dim StrCommandStored As String
        Dim StrFormName As String
        Dim ObjDataSet As New Data.DataSet
        Try
            StrFormName = pgSender.Form.ID
            StrCommandStored = "hrs_GetFormsPermissions"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, StrCommandStored, UserID, GroupID, StrFormName)
            If Venus.Shared.DataHandler.CheckValidDataObject(ObjDataSet) Then
                With ObjDataSet.Tables(0).Rows(0)
                    ImageButton_Delete.Enabled = .Item("AllowDelete")
                    ImageButton_Print.Enabled = .Item("AllowPrint")
                    Select Case Mode
                        Case "N", "R"
                            ImageButton_Save.Enabled = .Item("AllowAdd")
                            ImageButton_SaveN.Enabled = .Item("AllowAdd")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")
                            ImageButton_SaveN.Enabled = .Item("AllowEdit")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                    End Select
                End With
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SetToolBarRecordPermission(ByVal pgSender As System.Web.UI.Page, ByVal ConnectionString As String, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal StrTableName As String, ByVal RecordID As Integer) As Boolean
        Dim StrCommandStored As String
        Dim StrFormName As String
        Dim ObjDataSet As New Data.DataSet
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Try
            StrFormName = pgSender.Form.ID
            StrCommandStored = "hrs_GetRecordsPermissions"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, StrCommandStored, UserID, GroupID, Replace(StrTableName, " ", ""), RecordID)
            If Venus.Shared.DataHandler.CheckValidDataObject(ObjDataSet) Then
                With ObjDataSet.Tables(0).Rows(0)

                    If ImageButton_Save.Enabled = True And .Item("CanEdit") = True Then
                        ImageButton_Save.Enabled = Not .Item("CanEdit")
                        ImageButton_SaveN.Enabled = Not .Item("CanEdit")
                        LinkButton_SaveN.Enabled = Not .Item("CanEdit")
                    End If

                    If ImageButton_Delete.Enabled = True And .Item("CanDelete") = True Then
                        ImageButton_Delete.Enabled = Not .Item("CanDelete")
                    End If

                    If ImageButton_Print.Enabled = True And .Item("CanPrint") = True Then
                        ImageButton_Print.Enabled = Not .Item("CanPrint")
                    End If
                End With
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType
                Case "N", "R"
                    txtCode.Text = String.Empty
                    ImageButton_First.Visible = False
                    ImageButton_Back.Visible = False
                    ImageButton_Next.Visible = False
                    ImageButton_Last.Visible = False
                    ImageButton_Delete.Enabled = False
                    ImageButton_Properties.Visible = False
                    LinkButton_Properties.Visible = False
                    ImageButton_Remarks.Visible = False
                    LinkButton_Remarks.Visible = False

                Case "D"
                    ClsEvaluationCompainTypes.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsEvaluationCompainTypes.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEvaluationCompainTypes
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEvaluationCompainTypes = New ClsEval_EvaluationCompainTypes(Me)
        Try
            With ClsEvaluationCompainTypes
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Page, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsEvaluationCompainTypes = New ClsEval_EvaluationCompainTypes(Me)
        If IntId > 0 Then
            ClsEvaluationCompainTypes.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEvaluationCompainTypes = New ClsEval_EvaluationCompainTypes(Me)
        Try
            ClsEvaluationCompainTypes.Find("Code='" & txtCode.Text & "'")
            IntId = ClsEvaluationCompainTypes.ID
            txtEngName.Focus()
            If ClsEvaluationCompainTypes.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsEvaluationCompainTypes.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsEvaluationCompainTypes.ConnectionString, ClsEvaluationCompainTypes.DataBaseUserRelatedID, ClsEvaluationCompainTypes.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEvaluationCompainTypes.ConnectionString, ClsEvaluationCompainTypes.DataBaseUserRelatedID, ClsEvaluationCompainTypes.GroupID, ClsEvaluationCompainTypes.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsEvaluationCompainTypes.Clear()
        GetValues()
        ImageButton_Delete.Enabled = False

        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        CheckBox_DownLevel.Checked = False
        CheckBox_UpLevel.Checked = False
        CheckBox_SameLevel.Checked = False
        CheckBox_AllLevel.Checked = False
        CheckBox_ExternalLevel.Checked = False
        CheckBox_DownLevel_CheckedChanged(Nothing, Nothing)
        CheckBox_UpLevel_CheckedChanged(Nothing, Nothing)
        CheckBox_SameLevel_CheckedChanged(Nothing, Nothing)
        CheckBox_AllLevel_CheckedChanged(Nothing, Nothing)
        CheckBox_ExternalLevel_CheckedChanged(Nothing, Nothing)

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEvaluationCompainTypes = New ClsEval_EvaluationCompainTypes(Page)
        ClsEvaluationCompainTypes.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsEvaluationCompainTypes.ID
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
                Dim bIsArabic As Boolean = IIf(IsDBNull(row("IsArabic")), False, row("IsArabic"))
                If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then
                    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                End If
            Next
        End If
    End Sub
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEvaluationCompainTypes.Table) = True Then
            Dim StrTablename As String
            ClsEvaluationCompainTypes = New ClsEval_EvaluationCompainTypes(Me)
            StrTablename = ClsEvaluationCompainTypes.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID & _
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID & _
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmDocumentsTypes")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmDocumentsTypes")
            End If
        End If
    End Function

#End Region

#Region "Toogle Images"
    Protected Sub ImageButton_DownLevel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_DownLevel.Click
        If CheckBox_DownLevel.Checked = True Then
            CheckBox_DownLevel.Checked = False
        Else
            CheckBox_DownLevel.Checked = True
        End If
        CheckBox_DownLevel_CheckedChanged(Nothing, Nothing)
    End Sub
    Protected Sub ImageButton_UpLevel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_UpLevel.Click
        If CheckBox_UpLevel.Checked = True Then
            CheckBox_UpLevel.Checked = False
        Else
            CheckBox_UpLevel.Checked = True
        End If
        CheckBox_UpLevel_CheckedChanged(Nothing, Nothing)
    End Sub
    Protected Sub ImageButton_SameLevel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_SameLevel.Click
        If CheckBox_SameLevel.Checked = True Then
            CheckBox_SameLevel.Checked = False
        Else
            CheckBox_SameLevel.Checked = True
        End If
        CheckBox_SameLevel_CheckedChanged(Nothing, Nothing)
    End Sub

    Protected Sub ImageButton_AllLevel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_AllLevel.Click
        If CheckBox_AllLevel.Checked = True Then
            CheckBox_AllLevel.Checked = False
        Else
            CheckBox_AllLevel.Checked = True
        End If
        CheckBox_AllLevel_CheckedChanged(Nothing, Nothing)
    End Sub

    Protected Sub ImageButton_ExternalLevel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_ExternalLevel.Click
        If CheckBox_ExternalLevel.Checked = True Then
            CheckBox_ExternalLevel.Checked = False
        Else
            CheckBox_ExternalLevel.Checked = True
        End If
        CheckBox_ExternalLevel_CheckedChanged(Nothing, Nothing)
    End Sub

    Protected Sub CheckBox_DownLevel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox_DownLevel.CheckedChanged
        If CheckBox_DownLevel.Checked = True Then
            ImageButton_DownLevel.BorderStyle = BorderStyle.Outset
        Else
            ImageButton_DownLevel.BorderStyle = BorderStyle.None
        End If
    End Sub

    Protected Sub CheckBox_UpLevel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox_UpLevel.CheckedChanged
        If CheckBox_UpLevel.Checked = True Then
            ImageButton_UpLevel.BorderStyle = BorderStyle.Outset
        Else
            ImageButton_UpLevel.BorderStyle = BorderStyle.None
        End If
    End Sub

    Protected Sub CheckBox_SameLevel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox_SameLevel.CheckedChanged
        If CheckBox_SameLevel.Checked = True Then
            ImageButton_SameLevel.BorderStyle = BorderStyle.Outset
        Else
            ImageButton_SameLevel.BorderStyle = BorderStyle.None
        End If
    End Sub

    Protected Sub CheckBox_AllLevel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox_AllLevel.CheckedChanged
        If CheckBox_AllLevel.Checked = True Then
            ImageButton_AllLevel.BorderStyle = BorderStyle.Outset
        Else
            ImageButton_AllLevel.BorderStyle = BorderStyle.None
        End If
    End Sub

    Protected Sub CheckBox_ExternalLevel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox_ExternalLevel.CheckedChanged
        If CheckBox_ExternalLevel.Checked = True Then
            ImageButton_ExternalLevel.BorderStyle = BorderStyle.Outset
        Else
            ImageButton_ExternalLevel.BorderStyle = BorderStyle.None
        End If
    End Sub

#End Region

End Class
