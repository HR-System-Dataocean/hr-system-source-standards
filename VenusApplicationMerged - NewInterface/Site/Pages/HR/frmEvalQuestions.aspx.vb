Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmEvalQuestions
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsEvalQuestions As ClsEval_Questions
    Private clsMainOtherFields As clsSys_MainOtherFields

    Private ClsEvalQuestionsDetail1 As ClsEval_QuestionsDetail1
    Private ClsEvalGroups As ClsEval_Groups
    Private ClsEvalGroupsQuestions As ClsEval_GroupsQuestions

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
        ClsEvalQuestions = New ClsEval_Questions(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsEvalQuestions.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsEvalQuestions.ConnectionString)
                ClsEvalQuestions.AddOnChangeEventToControls("frmEvalQuestions", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                Setsetting(0)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtAraQuestion, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsEvalQuestions.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsEvalQuestions.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEvalQuestions.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsEvalQuestions = New ClsEval_Questions(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvalQuestions.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                SavePart()
             
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                SavePart()
             
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
                If (ClsEvalQuestions.ID > 0) Then
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " Evaluation Question Not Found ")
                    Exit Sub
                End If
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGroups.Rows()
                    ClsEvalGroupsQuestions = New ClsEval_GroupsQuestions(Page)
                    Dim str As String = "delete from Eval_GroupsQuestions where QuestionID = " & ClsEvalQuestions.ID & " and GroupID = " & DGRow.Cells(1).Value
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvalGroupsQuestions.ConnectionString, Data.CommandType.Text, str)
                Next
                ClsEvalQuestions.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEvalQuestions.ID & "&TableName=" & ClsEvalQuestions.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEvalQuestions.ID & "&TableName=" & ClsEvalQuestions.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEvalQuestions.Table
                ClsEvalQuestions.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsEvalQuestions.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEvalQuestions.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEvalQuestions.Find(" Code= '" & txtCode.Text & "'")
                If ClsEvalQuestions.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsEvalQuestions.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsEvalQuestions.CheckDiff(ClsEvalQuestions, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsEvalQuestions.FirstRecord()
                GetValues()
            Case "Previous"
                ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
                If Not ClsEvalQuestions.previousRecord() Then
                    ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
                If Not ClsEvalQuestions.NextRecord() Then
                    ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsEvalQuestions.LastRecord()
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
            With ClsEvalQuestions
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .Question_Arb = txtAraQuestion.Text
                .Question_Eng = txtEngQuestion.Text
                .AnswerType = 1
                .Status = QStatus.Checked
                .QPower = 0
            End With

            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsEvalQuestions = New ClsEval_Questions(Page)
        ClsEvalQuestionsDetail1 = New ClsEval_QuestionsDetail1(Page)
        Try
            ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
            If ClsEvalQuestions.ID > 0 Then
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsEvalQuestions.Update("Code='" & txtCode.Text & "'")
                Dim str As String = "delete from Eval_QuestionsDetail1 where QuestionID = " & ClsEvalQuestions.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvalQuestions.ConnectionString, Data.CommandType.Text, str)
                If (SaveDG(ClsEvalQuestions.ID)) Then
                    ClsEvalQuestions = New ClsEval_Questions(Page)
                    clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                    ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
                    clsMainOtherFields.CollectDataAndSave(value.Text, ClsEvalQuestions.Table, ClsEvalQuestions.ID)
                    value.Text = ""

                End If
            Else
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsEvalQuestions.Save()
                ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
                If ClsEvalQuestions.ID > 0 Then
                    If (SaveDG(ClsEvalQuestions.ID)) Then
                        ClsEvalQuestions = New ClsEval_Questions(Page)
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsEvalQuestions.Table, ClsEvalQuestions.ID)
                        value.Text = ""

                    End If
                End If
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEvalQuestions.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SaveDG(ByVal QuestionID As Integer) As Boolean
        Try
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgQuestionAnswers.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If
                ClsEvalQuestionsDetail1 = New ClsEval_QuestionsDetail1(Page)
                ClsEvalQuestionsDetail1.QuestionID = QuestionID
                ClsEvalQuestionsDetail1.Answer_Eng = DGRow.Cells(1).Value
                ClsEvalQuestionsDetail1.Answer_Arb = DGRow.Cells(2).Value
                ClsEvalQuestionsDetail1.APower = DGRow.Cells(3).Value
                ClsEvalQuestionsDetail1.RegComputerID = DGRow.Cells(4).Value
                ClsEvalQuestionsDetail1.Save()
            Next

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGroups.Rows
                ClsEvalGroupsQuestions = New ClsEval_GroupsQuestions(Page)
                Dim str As String = "delete from Eval_GroupsQuestions where QuestionID = " & QuestionID & " and GroupID = " & DGRow.Cells(1).Value
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvalGroupsQuestions.ConnectionString, Data.CommandType.Text, str)
                If DGRow.Cells(0).Value = True Then
                    ClsEvalGroupsQuestions.QuestionID = QuestionID
                    ClsEvalGroupsQuestions.GroupID = DGRow.Cells(1).Value
                    ClsEvalGroupsQuestions.Save()
                End If
            Next
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvalQuestions.ConnectionString)
        Try
            SetToolBarDefaults()

            With ClsEvalQuestions
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtEngQuestion.Text = .Question_Eng
                txtAraQuestion.Text = .Question_Arb
                QStatus.Checked = .Status

                ClsEvalQuestionsDetail1 = New ClsEval_QuestionsDetail1(Me)
                ClsEvalQuestionsDetail1.Find("CancelDate is null and QuestionID = " & .ID)
                uwgQuestionAnswers.DataSource = ClsEvalQuestionsDetail1.DataSet.Tables(0).DefaultView
                uwgQuestionAnswers.DataBind()

                ClsEvalGroups = New ClsEval_Groups(Me)
                uwgGroups.Columns(2).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")

                Dim str As String = "Select CONVERT(BIT,IsNull((Select Top 1 GroupID From Eval_GroupsQuestions Where QuestionID = " & .ID & " And GroupID = Eval_Groups.ID) ,0)) AS IsCriteria, Eval_Groups.ID, Eval_Groups.EngName, Eval_Groups.ArbName From Eval_Groups Where Eval_Groups.CancelDate Is Null"
                Dim ds As New Data.DataSet
                ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvalGroups.ConnectionString, Data.CommandType.Text, str)

                uwgGroups.DataSource = ds.Tables(0).DefaultView
                uwgGroups.DataBind()

                ClsEvalGroupsQuestions.Find("QuestionID = " & .ID)
                Dim DT As Data.DataTable = ClsEvalGroupsQuestions.DataSet.Tables(0)
                Dim i As Integer
                For i = 0 To DT.Rows.Count - 1
                    For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGroups.Rows()
                        If DGRow.Cells(1).Value = DT.Rows(i)(2) Then
                            DGRow.Cells(0).Value = True
                        End If
                    Next
                Next
            End With



            If Not ClsEvalQuestions.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEvalQuestions.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsEvalQuestions.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsEvalQuestions.RegDate).Date
            End If
            If ClsEvalQuestions.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsEvalQuestions.CancelDate).Date
            End If

            Dim item As New System.Web.UI.WebControls.ListItem()


            If (ClsEvalQuestions.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsEvalQuestions.ConnectionString, ClsEvalQuestions.DataBaseUserRelatedID, ClsEvalQuestions.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEvalQuestions.ConnectionString, ClsEvalQuestions.DataBaseUserRelatedID, ClsEvalQuestions.GroupID, ClsEvalQuestions.Table, ClsEvalQuestions.ID)
            If Not ClsEvalQuestions.CancelDate = Nothing Then

            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsEvalQuestions.ID)
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

                    ImageButton_Print.Enabled = .Item("AllowPrint")
                    Select Case Mode
                        Case "N", "R"
                            ImageButton_Save.Enabled = .Item("AllowAdd")
                    
                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")
                    
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

                Case "D"
                    ClsEvalQuestions.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False

                Case "E"
                    ClsEvalQuestions.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True

            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEvalQuestions
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)

                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEvalQuestions = New ClsEval_Questions(Me)
        Try
            With ClsEvalQuestions
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
        ClsEvalQuestions = New ClsEval_Questions(Me)
        If IntId > 0 Then
            ClsEvalQuestions.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEvalQuestions = New ClsEval_Questions(Me)
        ClsEvalGroups = New ClsEval_Groups(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvalQuestions.ConnectionString)
        Try
            ClsEvalQuestions.Find("Code='" & txtCode.Text & "'")
            IntId = ClsEvalQuestions.ID
            txtEngName.Focus()
            If ClsEvalQuestions.ID > 0 Then
                GetValues()
                StrMode = "E"
                uwgQuestionAnswers.Rows.Add()
            Else
                If ClsEvalQuestions.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                uwgGroups.Columns(2).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                ClsEvalGroups.Find("CancelDate is null")
                uwgGroups.DataSource = ClsEvalGroups.DataSet.Tables(0).DefaultView
                uwgGroups.DataBind()
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsEvalQuestions.ConnectionString, ClsEvalQuestions.DataBaseUserRelatedID, ClsEvalQuestions.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEvalQuestions.ConnectionString, ClsEvalQuestions.DataBaseUserRelatedID, ClsEvalQuestions.GroupID, ClsEvalQuestions.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then

            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsEvalQuestions.Clear()
        GetValues()



        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        txtAraQuestion.Text = String.Empty
        txtEngQuestion.Text = String.Empty
        QStatus.Checked = False
        uwgQuestionAnswers.Rows.Clear()
        uwgGroups.Rows.Clear()
        uwgQuestionAnswers.Rows.Add()
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEvalQuestions = New ClsEval_Questions(Page)
        ClsEvalQuestions.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsEvalQuestions.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEvalQuestions.Table) = True Then
            Dim StrTablename As String
            ClsEvalQuestions = New ClsEval_Questions(Me)
            StrTablename = ClsEvalQuestions.Table
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
