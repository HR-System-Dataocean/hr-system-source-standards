Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmForms
    Inherits MainPage
#Region "Public Decleration"
    Private ClsForms As ClsSys_Forms
    Private ClsUser As Clssys_Users
    Private ClsModules As Clssys_Modules
    Private ClsObjects As Clssys_Objects
    Private ClsSearchs As Clssys_Searchs
    Private mErrorHandler As New Venus.Shared.ErrorsHandler
    Private clsMainOtherFields As clsSys_MainOtherFields

    Const NoofEmptyRows = 5
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Dim ObjUsers As New Venus.Application.SystemFiles.System.Clssys_Users(Page)
        ObjUsers.IsAuthenticated()

        ClsModules = New Clssys_Modules(Me.Page)
        ClsForms = New ClsSys_Forms(Me)

        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsForms.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsForms.ConnectionString)
                ClsForms.AddOnChangeEventToControls("frmForms", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbDescription, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                ClsForms.GetDropDownList(ddlForm, True)
                ClsModules.GetDropDownList(ddlModule, True)
                Setsetting(IntId)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsForms.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsForms.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsForms.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsForms = New ClsSys_Forms(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsForms.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                If ddlModule.SelectedIndex > 0 Then
                    ClsForms.Find("code='" & txtCode.Text & "'")
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsForms.ID > 0 Then
                        ClsForms.Update("Code='" & txtCode.Text & "'")
                    Else
                        ClsForms.Save()
                    End If

                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please Choose Module/برجاء إختيار البرنامج"))
                End If

                ClsForms.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsForms.Table, ClsForms.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                If ddlModule.SelectedIndex > 0 Then
                    ClsForms.Find("code='" & txtCode.Text & "'")
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsForms.ID > 0 Then
                        ClsForms.Update("Code='" & txtCode.Text & "'")
                    Else
                        ClsForms.Save()
                    End If

                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please Choose Module/برجاء إختيار البرنامج"))
                End If
                ClsForms.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsForms.Table, ClsForms.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsForms.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsForms.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsForms.ID & "&TableName=" & ClsForms.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsForms.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsForms.ID & "&TableName=" & ClsForms.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsForms.Table
                ClsForms.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsForms.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsForms.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsForms.Find(" Code= '" & txtCode.Text & "'")
                If ClsForms.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsForms.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsForms.CheckDiff(ClsForms, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsForms.FirstRecord()
                GetValues()
            Case "Previous"
                ClsForms.Find("Code='" & txtCode.Text & "'")
                If Not ClsForms.previousRecord() Then
                    ClsForms.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsForms.Find("Code='" & txtCode.Text & "'")
                If Not ClsForms.NextRecord() Then
                    ClsForms.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsForms.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function LoadGridData(ByVal IntFormID As Integer) As Boolean
        Dim TempDS As New Data.DataSet
        Dim objClsForms As New ClsSys_Forms(Me.Page)
        Try
            objClsForms.GetFormParameters(IntFormID, TempDS)
            uwgFormParameters.DataSource = TempDS
            uwgFormParameters.DataBind()
            If (TempDS.Tables(0).Rows.Count = 0) Then
                CreateEmptyRows(uwgFormParameters, NoofEmptyRows, True)
            Else
                CreateEmptyRows(uwgFormParameters, NoofEmptyRows, False)
            End If
        Catch ex As Exception
            uwgFormParameters.DataSource = Nothing
            uwgFormParameters.DataBind()
        End Try
    End Function
    Private Function CreateEmptyRows(ByRef uwgObj As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal NoofEmptyRows As Integer, ByVal blnClear As Boolean) As Boolean
        Dim IntCount As Integer
        If (blnClear) Then
            uwgObj.Rows.Clear()
            For IntCount = 0 To NoofEmptyRows - 1
                uwgObj.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow)
            Next
        Else
            For IntCount = uwgObj.Rows.Count To NoofEmptyRows - 1
                uwgObj.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow)
            Next
        End If
    End Function
    Private Function LoadCanceledItems(ByRef ClsForms As ClsSys_Forms) As Boolean
        ClsUser = New Clssys_Users(Me.Page)
        ClsModules = New Clssys_Modules(Me.Page)
        Dim objClsForms As New ClsSys_Forms(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsForms.ConnectionString)
        Dim item As New System.Web.UI.WebControls.ListItem()
        Try
            With ClsForms
                ddlModule.Items.Clear()
                ddlForm.Items.Clear()

                objClsForms.GetDropDownList(ddlForm, True)
                ClsModules.GetDropDownList(ddlModule, True)

                ClsModules.Find(" ID= " & IIf(IsNothing(.ModuleID), 0, .ModuleID))
                objClsForms.Find(" ID= " & IIf(IsNothing(.SearchFormID), 0, .SearchFormID))

                If ClsModules.ID > 0 Then
                    item.Value = .ModuleID
                    item.Text = ObjNavigationHandler.SetLanguage(Page, ClsModules.EngName & "/" & ClsModules.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Page, ClsModules.ArbName & "/" & ClsModules.EngName)
                    End If

                    If Not ddlModule.Items.Contains(item) Then
                        ddlModule.Items.Add(item)
                        ddlModule.SelectedValue = item.Value
                    Else
                        ddlModule.SelectedValue = .ModuleID
                    End If

                End If
                item = New System.Web.UI.WebControls.ListItem()
                If objClsForms.ID > 0 Then
                    item.Value = .SearchFormID
                    item.Text = ObjNavigationHandler.SetLanguage(Page, objClsForms.EngName & "/" & objClsForms.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Page, objClsForms.ArbName & "/" & objClsForms.EngName)
                    End If

                    If Not ddlForm.Items.Contains(item) Then
                        ddlForm.Items.Add(item)
                        ddlForm.SelectedValue = item.Value
                    Else
                        ddlForm.SelectedValue = .SearchFormID
                    End If

                End If
            End With
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsForms.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsForms.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function AssignValues() As Boolean
        Try
            Dim validFormat As Boolean = False
            Dim dotIndex As Integer
            Dim formName As String = ""
            With ClsForms
                dotIndex = txtEngName.Text.LastIndexOf(".")
                If (dotIndex > -1) Then
                    Dim str As String = txtEngName.Text.Substring(dotIndex + 1, txtEngName.Text.Length - (dotIndex + 1))
                    If Not str = "aspx" Then
                        formName = txtEngName.Text.Substring(0, dotIndex) & ".aspx"
                    Else
                        formName = txtEngName.Text
                    End If
                Else
                    formName = txtEngName.Text & ".aspx"
                End If
                .Code = txtCode.Text
                .EngName = formName 'txtEngName.Text
                .ArbName = txtArbName.Text
                .EngDescription = txtEngDescription.Text
                .ArbDescription = txtArbDescription.Text
                .Rank = txtRank.Text
                .Height = txtHeight.Text
                .Width = txtWidth.Text
                .ModuleID = ddlModule.SelectedValue
                .SearchFormID = ddlForm.SelectedValue
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsForms
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtEngDescription.Text = .EngDescription
                txtArbDescription.Text = .ArbDescription
                txtRank.Text = .Rank
                txtHeight.Text = .Height
                txtWidth.Text = .Width
                LoadGridData(.ID)
            End With
            LoadCanceledItems(ClsForms)
            If Not ClsForms.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsForms.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsForms.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsForms.RegDate).Date
            End If
            If ClsForms.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsForms.CancelDate).Date
            End If
            If Not ClsForms.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()

            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsForms.ConnectionString)
            If (ClsForms.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsForms.ConnectionString, ClsForms.DataBaseUserRelatedID, ClsForms.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsForms.ConnectionString, ClsForms.DataBaseUserRelatedID, ClsForms.GroupID, ClsForms.Table, ClsForms.ID)
            If Not ClsForms.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsForms.ID)
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
                    ClsForms.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsForms.Find("ID=" & intID)
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
            With ClsForms
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
        ClsForms = New ClsSys_Forms(Me)
        Try
            With ClsForms
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
        ClsForms = New ClsSys_Forms(Me)
        If IntId > 0 Then
            ClsForms.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsForms = New ClsSys_Forms(Me)
        Try
            ClsForms.Find("Code='" & txtCode.Text & "'")
            IntId = ClsForms.ID
            txtEngName.Focus()
            If ClsForms.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsForms.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsForms.ConnectionString, ClsForms.DataBaseUserRelatedID, ClsForms.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsForms.ConnectionString, ClsForms.DataBaseUserRelatedID, ClsForms.GroupID, ClsForms.Table, IntId)
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
        ClsForms.Clear()
        GetValues()
        ImageButton_Delete.Enabled = False
        CreateEmptyRows(uwgFormParameters, NoofEmptyRows, True)
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        txtEngDescription.Text = String.Empty
        txtArbDescription.Text = String.Empty

        uwgFormParameters.DataSource = Nothing
        uwgFormParameters.DataBind()

        txtRank.Text = String.Empty
        txtWidth.Text = 0
        txtHeight.Text = 0
        ddlModule.SelectedIndex = 0

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsForms = New ClsSys_Forms(Page)
        ClsForms.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsForms.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsForms.Table) = True Then
            Dim StrTablename As String
            ClsForms = New ClsSys_Forms(Me)
            StrTablename = ClsForms.Table
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
End Class
