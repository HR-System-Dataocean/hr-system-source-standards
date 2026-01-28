Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmRecPositions
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsPositions As ClsRec_Positions
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsPositions = New ClsRec_Positions(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim clsBranches = New Clssys_Branches(Page)
        Dim clsDepartments = New Clssys_Departments(Page)
        Dim clshrsPositions = New Clshrs_Positions(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsPositions.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsPositions.ConnectionString)
                ClsPositions.AddOnChangeEventToControls("frmRecPositions", Page, UltraWebTab1)
                clsDepartments.GetDropDownList(ddlDepartment, True)
                clsBranches.GetDropDownList(ddlBranch, True)
                ddlBranch.SelectedIndex = 0
                clshrsPositions.GetDropDownList(ddlRefPosition, True)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsPositions.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsPositions.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsPositions.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsPositions = New ClsRec_Positions(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsPositions.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text.Length = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                    Return
                ElseIf ddlDepartment.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Departtment /لابد من إدخال الإدارة"))
                    Return
                ElseIf ddlBranch.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Branch /لابد من إدخال الفرع"))
                    Return
                ElseIf ddlRefPosition.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Reference Position /لابد من إدخال المهنه المرتبطه"))
                    Return
                End If
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsPositions.ID > 0 Then
                    ClsPositions.Update("code='" & txtCode.Text & "'")
                Else
                    ClsPositions.Save()
                End If
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsPositions.Table, ClsPositions.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text.Length = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                    Return
                ElseIf ddlDepartment.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Departtment /لابد من إدخال الإدارة"))
                    Return
                ElseIf ddlBranch.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Branch /لابد من إدخال الفرع"))
                    Return
                ElseIf ddlRefPosition.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Reference Position /لابد من إدخال المهنه المرتبطه"))
                    Return
                End If
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsPositions.ID > 0 Then
                    ClsPositions.Update("code='" & txtCode.Text & "'")
                Else
                    ClsPositions.Save()
                End If
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsPositions.Table, ClsPositions.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsPositions.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsPositions.ID & "&TableName=" & ClsPositions.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsPositions.ID & "&TableName=" & ClsPositions.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsPositions.Table
                ClsPositions.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsPositions.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsPositions.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsPositions.Find(" Code= '" & txtCode.Text & "'")
                If ClsPositions.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsPositions.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsPositions.CheckDiff(ClsPositions, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsPositions.FirstRecord()
                GetValues()
            Case "Previous"
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                If Not ClsPositions.previousRecord() Then
                    ClsPositions.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                If Not ClsPositions.NextRecord() Then
                    ClsPositions.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsPositions.LastRecord()
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
            With ClsPositions
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .RefCode = txtRefCode.Text
                If ddlDepartment.SelectedValue <> 0 Then
                    .Department_ID = ddlDepartment.SelectedValue
                Else
                    .Department_ID = Nothing
                End If
                If ddlBranch.SelectedValue <> 0 Then
                    .Branch_ID = ddlBranch.SelectedValue
                Else
                    .Branch_ID = Nothing
                End If
                If ddlRefPosition.SelectedValue <> 0 Then
                    .Positions_ID = ddlRefPosition.SelectedValue
                Else
                    .Positions_ID = Nothing
                End If
                .GStartDate = ClsDataAcessLayer.FormatGreg(txtGStartDate.Value, "dd/MM/yyyy")
                .HStartDate = txtHStartDate.Value
                .PositionQTY = txtPositionQTY.Value
                .Status = chkStatus.Checked
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

            With ClsPositions
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtRefCode.Text = .RefCode
                ddlBranch.SelectedValue = .Branch_ID
                ddlDepartment.SelectedValue = .Department_ID
                ddlRefPosition.SelectedValue = .Positions_ID
                txtGStartDate.Value = .GStartDate
                txtHStartDate.Value = .HStartDate
                txtPositionQTY.Value = .PositionQTY
                chkStatus.Checked = .Status
            End With

            If Not ClsPositions.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsPositions.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsPositions.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsPositions.RegDate).Date
            End If
            If ClsPositions.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsPositions.CancelDate).Date
            End If
            If Not ClsPositions.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()

            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsPositions.ConnectionString)
            If (ClsPositions.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsPositions.ConnectionString, ClsPositions.DataBaseUserRelatedID, ClsPositions.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsPositions.ConnectionString, ClsPositions.DataBaseUserRelatedID, ClsPositions.GroupID, ClsPositions.Table, ClsPositions.ID)
            If Not ClsPositions.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsPositions.ID)
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
                    ClsPositions.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsPositions.Find("ID=" & intID)
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
            With ClsPositions
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
        ClsPositions = New ClsRec_Positions(Me)
        Try
            With ClsPositions
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
        ClsPositions = New ClsRec_Positions(Me)
        If IntId > 0 Then
            ClsPositions.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsPositions = New ClsRec_Positions(Me)
        Try
            ClsPositions.Find("Code='" & txtCode.Text & "'")
            IntId = ClsPositions.ID
            txtEngName.Focus()
            If ClsPositions.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsPositions.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsPositions.ConnectionString, ClsPositions.DataBaseUserRelatedID, ClsPositions.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsPositions.ConnectionString, ClsPositions.DataBaseUserRelatedID, ClsPositions.GroupID, ClsPositions.Table, IntId)
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
        ClsPositions.Clear()
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
        txtRefCode.Text = String.Empty
        ddlBranch.SelectedValue = 0
        ddlDepartment.SelectedValue = 0
        ddlRefPosition.SelectedValue = 0
        txtGStartDate.Value = ClsDataAcessLayer.FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtHStartDate.Value = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtPositionQTY.Value = 0
        chkStatus.Checked = True

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsPositions = New ClsRec_Positions(Page)
        ClsPositions.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsPositions.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsPositions.Table) = True Then
            Dim StrTablename As String
            ClsPositions = New ClsRec_Positions(Me)
            StrTablename = ClsPositions.Table
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
