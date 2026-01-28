Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmEvalGroups
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEvalGroups As ClsEval_Groups
    Private ClsEvalGroupsQuestions As ClsEval_GroupsQuestions
    Private ClsEvalQuestions As ClsEval_Questions
    Private clsMainOtherFields As clsSys_MainOtherFields

    Const CsPaidatVacation = 4
    Const CsOnceatPeriod = 5
    Const CsMaxValue = 3
    Const CsMinValue = 2
    Const CsIntervalID = 6
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEvalGroups = New ClsEval_Groups(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsEvalGroups.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsEvalGroups.ConnectionString)
                ClsEvalGroups.AddOnChangeEventToControls("frmEvalGroups", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsEvalGroups.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsEvalGroups.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEvalGroups.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsEvalGroups = New ClsEval_Groups(Me)
        ClsEvalGroupsQuestions = New ClsEval_GroupsQuestions(Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvalGroups.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsEvalGroups.ID > 0 Then
                    ClsEvalGroups.Update("code='" & txtCode.Text & "'")
                Else
                    ClsEvalGroups.Save()
                End If
                ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                If ClsEvalGroups.ID > 0 Then
                    If (SaveDG(ClsEvalGroups.ID)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsEvalGroups.Table, ClsEvalGroups.ID)
                        value.Text = ""
                    End If
                End If
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEvalGroups.Table, ClsEvalGroups.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsEvalGroups.ID > 0 Then
                    ClsEvalGroups.Update("code='" & txtCode.Text & "'")
                Else
                    ClsEvalGroups.Save()
                End If

                ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                If ClsEvalGroups.ID > 0 Then
                    If (SaveDG(ClsEvalGroups.ID)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsEvalGroups.Table, ClsEvalGroups.ID)
                        value.Text = ""
                    End If
                End If
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                If (ClsEvalGroups.ID > 0) Then
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " Groups Question Not Found ")
                    Exit Sub
                End If
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgQuestions.Rows()
                    ClsEvalGroupsQuestions = New ClsEval_GroupsQuestions(Page)
                    Dim str As String = "delete from Eval_GroupsQuestions where GroupID = " & ClsEvalGroups.ID & " and QuestionID = " & DGRow.Cells(1).Value
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvalGroups.ConnectionString, Data.CommandType.Text, str)
                Next

                ClsEvalGroups.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEvalGroups.ID & "&TableName=" & ClsEvalGroups.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEvalGroups.ID & "&TableName=" & ClsEvalGroups.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEvalGroups.Table
                ClsEvalGroups.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsEvalGroups.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEvalGroups.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEvalGroups.Find(" Code= '" & txtCode.Text & "'")
                If ClsEvalGroups.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsEvalGroups.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsEvalGroups.CheckDiff(ClsEvalGroups, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsEvalGroups.FirstRecord()
                GetValues()
            Case "Previous"
                ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                If Not ClsEvalGroups.previousRecord() Then
                    ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                If Not ClsEvalGroups.NextRecord() Then
                    ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsEvalGroups.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function SaveDG(ByVal groupID As Integer) As Boolean
        Try
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgQuestions.Rows()
                ClsEvalGroupsQuestions = New ClsEval_GroupsQuestions(Page)
                Dim str As String = "delete from Eval_GroupsQuestions where QuestionID = " & DGRow.Cells(1).Value & " and GroupID = " & groupID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvalGroupsQuestions.ConnectionString, Data.CommandType.Text, str)
                If DGRow.Cells(0).Value = True Then
                    ClsEvalGroupsQuestions.QuestionID = DGRow.Cells(1).Value
                    ClsEvalGroupsQuestions.GroupID = groupID
                    ClsEvalGroupsQuestions.Save()
                End If
            Next
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function AssignValues() As Boolean
        Try
            With ClsEvalGroups
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsUser.ConnectionString)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsEvalGroups
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName

                uwgQuestions.Columns(2).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "Question_Eng/Question_Arb")
                ClsEvalQuestions = New ClsEval_Questions(Me)
                ClsEvalQuestions.Find("CancelDate is null")
                uwgQuestions.DataSource = ClsEvalQuestions.DataSet.Tables(0).DefaultView
                uwgQuestions.DataBind()

                Dim ClsEvalGroupsQuestions As New ClsEval_GroupsQuestions(Me)

                ClsEvalGroupsQuestions.Find("GroupID = " & .ID)
                Dim DT As Data.DataTable = ClsEvalGroupsQuestions.DataSet.Tables(0)
                Dim i As Integer
                For i = 0 To DT.Rows.Count - 1
                    For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgQuestions.Rows()
                        If DGRow.Cells(1).Value = DT.Rows(i)(1) Then
                            DGRow.Cells(0).Value = True
                        End If
                    Next
                Next

            End With

            If Not ClsEvalGroups.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEvalGroups.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsEvalGroups.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsEvalGroups.RegDate).Date
            End If
            If ClsEvalGroups.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsEvalGroups.CancelDate).Date
            End If
            If Not ClsEvalGroups.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()


            If (ClsEvalGroups.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsEvalGroups.ConnectionString, ClsEvalGroups.DataBaseUserRelatedID, ClsEvalGroups.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEvalGroups.ConnectionString, ClsEvalGroups.DataBaseUserRelatedID, ClsEvalGroups.GroupID, ClsEvalGroups.Table, ClsEvalGroups.ID)
            If Not ClsEvalGroups.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsEvalGroups.ID)
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
                    ClsEvalGroups.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsEvalGroups.Find("ID=" & intID)
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
            With ClsEvalGroups
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
        ClsEvalGroups = New ClsEval_Groups(Me)
        Try
            With ClsEvalGroups
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
        ClsEvalGroups = New ClsEval_Groups(Me)
        If IntId > 0 Then
            ClsEvalGroups.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEvalGroups = New ClsEval_Groups(Me)
        ClsEvalQuestions = New ClsEval_Questions(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvalGroups.ConnectionString)
        Try
            ClsEvalGroups.Find("Code='" & txtCode.Text & "'")
            IntId = ClsEvalGroups.ID
            txtEngName.Focus()
            If ClsEvalGroups.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsEvalGroups.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                txtEngName.Focus()
                CreateOtherFields(0)
                uwgQuestions.Columns(2).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "Question_Eng/Question_Arb")
                ClsEvalQuestions.Find("CancelDate is null")
                uwgQuestions.DataSource = ClsEvalQuestions.DataSet.Tables(0).DefaultView
                uwgQuestions.DataBind()
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsEvalGroups.ConnectionString, ClsEvalGroups.DataBaseUserRelatedID, ClsEvalGroups.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEvalGroups.ConnectionString, ClsEvalGroups.DataBaseUserRelatedID, ClsEvalGroups.GroupID, ClsEvalGroups.Table, IntId)
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
        ClsEvalGroups.Clear()
        uwgQuestions.Clear()
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
        uwgQuestions.Rows.Clear()
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEvalGroups = New ClsEval_Groups(Page)
        ClsEvalGroups.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsEvalGroups.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEvalGroups.Table) = True Then
            Dim StrTablename As String
            ClsEvalGroups = New ClsEval_Groups(Me)
            StrTablename = ClsEvalGroups.Table
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
