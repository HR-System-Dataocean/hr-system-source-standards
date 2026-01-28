Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports Infragistics.WebUI.UltraWebGrid
Imports System.Data

Partial Class frmPositions
    Inherits MainPage
#Region "Public Decleration"
    Private ClsPositions As Clshrs_Positions
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsPositions = New Clshrs_Positions(Me.Page)
        Dim ClsPositionLevels As New Clshrs_PositionsLevels(Page)

        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
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
            Dim IntrecordID As Integer = 0
            Try
                IntrecordID = Request.QueryString.Item("ID")
            Catch ex As Exception
                IntrecordID = 0
            End Try
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsPositions.ConnectionString)
                ClsPositions.AddOnChangeEventToControls("frmCities", Page, UltraWebTab1)
                ClsPositionLevels.GetDropDownList(DdlPositionLevel, False)
                ClsPositions.GetDropDownList(DdlParentPosition, True, " ID <> " & IntrecordID & " And ParentID is null Or ParentID <> " & IntrecordID)
                Dim clsIntervals As New Clshrs_Intervals(Page)
                clsIntervals.GetList(uwgContact.DisplayLayout.Bands(0).Columns(3).ValueList)
                ClsPositions.GetList(uwgSuperVisors.DisplayLayout.Bands(0).Columns(1).ValueList)

                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Dim AppraisalTypeGroup As New Clshrs_AppraisalTypeGroup(Page)
                AppraisalTypeGroup.GetDropDownList(ddlappraisaltypegroup, True)
                GetDropDownListGriduwgAccountability1()
                GetDropDownListGriduwgAccountability2()
                GetDropDownListCompetences()
            End If

            IntrecordID = 0
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
        ClsPositions = New Clshrs_Positions(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsPositions.ConnectionString)
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
                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                If (ClsPositions.ID > 0) Then
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " Blood Group Not Found ")
                    Exit Sub
                End If
                ClsPositions.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                If ClsPositions.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsPositions.ID & "&TableName=" & ClsPositions.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If
            Case "Remarks"
                If ClsPositions.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsPositions.ID & "&TableName=" & ClsPositions.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                ClsPositions.FirstRecord()
                GetValues(ClsPositions)
            Case "Previous"
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                If Not ClsPositions.previousRecord() Then
                    ClsPositions.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))

                End If
                GetValues(ClsPositions)
            Case "Next"
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                If Not ClsPositions.NextRecord() Then
                    ClsPositions.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))

                End If
                GetValues(ClsPositions)
            Case "Last"
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                ClsPositions.LastRecord()
                GetValues(ClsPositions)
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function SavePart() As Boolean
        Try

            Dim StrMode As String = Request.QueryString.Item("Mode")
            ClsPositions = New Clshrs_Positions(Page)

            ClsPositions.Find("Code='" & txtCode.Text & "'")
            If Not AssignValue(ClsPositions) Then
                Exit Function
            End If

            If ClsPositions.ID > 0 Then
                ClsPositions.Update("Code='" & txtCode.Text & "'")
                Dim DelCommand As String = "delete from hrs_PositionSupervisors where PositionID = " & ClsPositions.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsPositions.ConnectionString, Data.CommandType.Text, DelCommand)
                DelCommand = "delete from hrs_PositionContacts where PositionID = " & ClsPositions.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsPositions.ConnectionString, Data.CommandType.Text, DelCommand)
                DelCommand = "delete from hrs_PositionCompetences where PositionID = " & ClsPositions.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsPositions.ConnectionString, Data.CommandType.Text, DelCommand)
                DelCommand = "delete from hrs_PositionQualifications where PositionID = " & ClsPositions.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsPositions.ConnectionString, Data.CommandType.Text, DelCommand)
                DelCommand = "delete from hrs_PositionAccountabilities where PositionID = " & ClsPositions.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsPositions.ConnectionString, Data.CommandType.Text, DelCommand)
                SaveDG(ClsPositions.ID)
            Else
                ClsPositions.Save()
                SaveDG(ClsPositions.ID)
            End If
            ClsPositions = New Clshrs_Positions(Page)
            clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)

            ClsPositions.Find("Code='" & txtCode.Text & "'")

            clsMainOtherFields.CollectDataAndSave(value.Text, ClsPositions.Table, ClsPositions.ID)
            value.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SaveDG(ByVal PositionID As Integer) As Boolean
        Try
            Dim intSetGroup As Integer = 0
            Dim GrpCompetencescode As String = ""
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgCompetences.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If
                intSetGroup = intSetGroup + 1
                Dim ClsPositionCompetences = New Clshrs_PositionCompetences(Page)
                ClsPositionCompetences.PositionID = PositionID
                ClsPositionCompetences.criteriaGroupID = DGRow.Cells(1).Value
                ClsPositionCompetences.CriteriaID = DGRow.Cells(2).Value
                ClsPositionCompetences.MinimumScore = DGRow.Cells(3).Value
                ClsPositionCompetences.MaximumScore = DGRow.Cells(4).Value
                ClsPositionCompetences.Weight = DGRow.Cells(5).Value
                ClsPositionCompetences.EngName = DGRow.Cells(6).Value
                ClsPositionCompetences.ArbName = DGRow.Cells(7).Value
                ClsPositionCompetences.Remarks = DGRow.Cells(8).Value
                ClsPositionCompetences.RegDate = DateTime.Now
                ClsPositionCompetences.Save()
                Dim EvalGroups As New ClsEval_Groups(Page)
                If intSetGroup = 1 Then
                    GrpCompetencescode = "Comp-" & DateTime.Now.ToString("HHmmssss")
                    EvalGroups.Code = GrpCompetencescode
                    EvalGroups.ArbName = "الكفاءة"
                    EvalGroups.EngName = "Competences"
                    EvalGroups.RegDate = DateTime.Now
                    EvalGroups.Save()
                End If
                EvalGroups.Find("Code='" & GrpCompetencescode & "'")

                Dim EvalQuestions As New ClsEval_Questions(Page)
                EvalQuestions.Code = GrpCompetencescode & DGRow.Index.ToString()
                EvalQuestions.ArbName = DGRow.Cells(2).Value
                EvalQuestions.EngName = DGRow.Cells(1).Value
                EvalQuestions.Question_Arb = "IS : " & DGRow.Cells(2).Value
                EvalQuestions.Question_Eng = "هل : " & DGRow.Cells(1).Value
                EvalQuestions.QPower = 0
                EvalQuestions.Status = True
                EvalQuestions.RegDate = DateTime.Now
                EvalQuestions.Save()

                EvalQuestions.Find("Code='" & GrpCompetencescode & DGRow.Index.ToString() & "'")

                Dim EvalQuestionDetails As New ClsEval_QuestionsDetail1(Page)
                EvalQuestionDetails.QuestionID = EvalQuestions.ID
                EvalQuestionDetails.Answer_Eng = "Yes"
                EvalQuestionDetails.Answer_Arb = "نعم"
                EvalQuestionDetails.APower = 100
                EvalQuestionDetails.RegDate = DateTime.Now
                EvalQuestionDetails.Save()

                EvalQuestionDetails = New ClsEval_QuestionsDetail1(Page)
                EvalQuestionDetails.QuestionID = EvalQuestions.ID
                EvalQuestionDetails.Answer_Eng = "No"
                EvalQuestionDetails.Answer_Arb = "لا"
                EvalQuestionDetails.APower = 0
                EvalQuestionDetails.RegDate = DateTime.Now
                EvalQuestionDetails.Save()

                Dim GroupsQuestions As New ClsEval_GroupsQuestions(Page)
                GroupsQuestions.GroupID = EvalGroups.ID
                GroupsQuestions.QuestionID = EvalQuestions.ID
                GroupsQuestions.RegDate = DateTime.Now
                GroupsQuestions.Save()
            Next

            intSetGroup = 0
            Dim GrpQualificationscode As String = ""
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgQualifications.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If
                intSetGroup = intSetGroup + 1
                Dim ClsPositionQUalifications = New Clshrs_PositionQualifications(Page)
                ClsPositionQUalifications.PositionID = PositionID
                ClsPositionQUalifications.EngName = DGRow.Cells(1).Value
                ClsPositionQUalifications.ArbName = DGRow.Cells(2).Value
                ClsPositionQUalifications.RegDate = DateTime.Now
                ClsPositionQUalifications.Save()

                Dim EvalGroups As New ClsEval_Groups(Page)
                If intSetGroup = 1 Then
                    GrpQualificationscode = "Qual-" & DateTime.Now.ToString("HHmmssss")
                    EvalGroups.Code = GrpQualificationscode
                    EvalGroups.ArbName = "المؤهلات"
                    EvalGroups.EngName = "Qualifications"
                    EvalGroups.RegDate = DateTime.Now
                    EvalGroups.Save()
                End If
                EvalGroups.Find("Code='" & GrpQualificationscode & "'")

                Dim EvalQuestions As New ClsEval_Questions(Page)
                EvalQuestions.Code = GrpQualificationscode & DGRow.Index.ToString()
                EvalQuestions.ArbName = DGRow.Cells(2).Value
                EvalQuestions.EngName = DGRow.Cells(1).Value
                EvalQuestions.Question_Arb = "IS : " & DGRow.Cells(2).Value
                EvalQuestions.Question_Eng = "هل : " & DGRow.Cells(1).Value
                EvalQuestions.QPower = 0
                EvalQuestions.Status = True
                EvalQuestions.RegDate = DateTime.Now
                EvalQuestions.Save()

                EvalQuestions.Find("Code='" & GrpQualificationscode & DGRow.Index.ToString() & "'")

                Dim EvalQuestionDetails As New ClsEval_QuestionsDetail1(Page)
                EvalQuestionDetails.QuestionID = EvalQuestions.ID
                EvalQuestionDetails.Answer_Eng = "Yes"
                EvalQuestionDetails.Answer_Arb = "نعم"
                EvalQuestionDetails.APower = 100
                EvalQuestionDetails.RegDate = DateTime.Now
                EvalQuestionDetails.Save()

                EvalQuestionDetails = New ClsEval_QuestionsDetail1(Page)
                EvalQuestionDetails.QuestionID = EvalQuestions.ID
                EvalQuestionDetails.Answer_Eng = "No"
                EvalQuestionDetails.Answer_Arb = "لا"
                EvalQuestionDetails.APower = 0
                EvalQuestionDetails.RegDate = DateTime.Now
                EvalQuestionDetails.Save()

                Dim GroupsQuestions As New ClsEval_GroupsQuestions(Page)
                GroupsQuestions.GroupID = EvalGroups.ID
                GroupsQuestions.QuestionID = EvalQuestions.ID
                GroupsQuestions.RegDate = DateTime.Now
                GroupsQuestions.Save()
            Next
            If GrpCompetencescode <> "" Or GrpQualificationscode <> "" Then
                Dim Eval_EvaluationTypes As New ClsEval_EvaluationTypes(Page)
                Dim Evalcode As String = "Eval-" & DateTime.Now.ToString("HHmmssss")
                Eval_EvaluationTypes.Code = Evalcode
                Eval_EvaluationTypes.EngName = "Recruitment Evaluation For : " & txtEngName.Text
                Eval_EvaluationTypes.ArbName = "تقييم توظيف ل : " & txtArbName.Text
                Eval_EvaluationTypes.EPower = 100
                Eval_EvaluationTypes.HasSlider = False
                Eval_EvaluationTypes.RegDate = DateTime.Now
                Eval_EvaluationTypes.Save()

                Eval_EvaluationTypes.Find("Code='" & Evalcode & "'")
                ClsPositions.Find("Code='" & txtCode.Text & "'")
                ClsPositions.EvalRecruitmentID = Eval_EvaluationTypes.ID
                ClsPositions.Update("Code='" & txtCode.Text & "'")
                Dim EvalGroups As New ClsEval_Groups(Page)
                If GrpCompetencescode <> "" Then
                    EvalGroups.Find("Code='" & GrpCompetencescode & "'")
                    Dim DelCommand As String = "insert into Eval_EvalTypeGroups (EvaltypeID,Group_ID,GPower,RegDate) values (" & Eval_EvaluationTypes.ID & "," & EvalGroups.ID & ",50,getdate())"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsPositions.ConnectionString, Data.CommandType.Text, DelCommand)
                    DelCommand = "insert into Eval_EvalTypeGroupQuestions (EvaltypeID,Group_ID,QuestionID,QPower,RegDate) select " & Eval_EvaluationTypes.ID & "," & EvalGroups.ID & ",ID,100,getdate() from Eval_Questions where ID in (select QuestionID from Eval_GroupsQuestions where GroupID = " & EvalGroups.ID & ")"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsPositions.ConnectionString, Data.CommandType.Text, DelCommand)
                End If
                If GrpQualificationscode <> "" Then
                    EvalGroups.Find("Code='" & GrpQualificationscode & "'")
                    Dim DelCommand As String = "insert into Eval_EvalTypeGroups (EvaltypeID,Group_ID,GPower,RegDate) values (" & Eval_EvaluationTypes.ID & "," & EvalGroups.ID & ",50,getdate())"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsPositions.ConnectionString, Data.CommandType.Text, DelCommand)
                    DelCommand = "insert into Eval_EvalTypeGroupQuestions (EvaltypeID,Group_ID,QuestionID,QPower,RegDate) select " & Eval_EvaluationTypes.ID & "," & EvalGroups.ID & ",ID,100,getdate() from Eval_Questions where ID in (select QuestionID from Eval_GroupsQuestions where GroupID = " & EvalGroups.ID & ")"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsPositions.ConnectionString, Data.CommandType.Text, DelCommand)
                End If

            End If
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgContact.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If
                If DGRow.Cells(3).Value <> 0 Then
                    Dim ClsPositionContacts = New Clshrs_PositionContacts(Page)
                    ClsPositionContacts.PositionID = PositionID
                    ClsPositionContacts.EngName = DGRow.Cells(1).Value
                    ClsPositionContacts.ArbName = DGRow.Cells(2).Value
                    ClsPositionContacts.IntervalID = DGRow.Cells(3).Value
                    ClsPositionContacts.IsExternal = DGRow.Cells(4).Value
                    ClsPositionContacts.RegDate = DateTime.Now
                    ClsPositionContacts.Save()
                End If
            Next
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgSuperVisors.Rows
                If DGRow.Cells(1).Value <> 0 Then
                    Dim ClsPositionSuperVisor = New Clshrs_PositionSupervisors(Page)
                    ClsPositionSuperVisor.PositionID = PositionID
                    ClsPositionSuperVisor.SuperVisorID = DGRow.Cells(1).Value
                    ClsPositionSuperVisor.RegDate = DateTime.Now
                    ClsPositionSuperVisor.Save()
                End If
            Next
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgAccountability1.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If
                Dim ClsPositionAccountabilities = New Clshrs_PositionAccountabilities(Page)
                ClsPositionAccountabilities.PositionID = PositionID
                ClsPositionAccountabilities.CriteriaGroupID = DGRow.Cells(1).Value
                ClsPositionAccountabilities.CriteriaID = DGRow.Cells(2).Value
                ClsPositionAccountabilities.MinimumScore = DGRow.Cells(3).Value
                ClsPositionAccountabilities.MaximumScore = DGRow.Cells(4).Value
                ClsPositionAccountabilities.Weight = DGRow.Cells(5).Value
                ClsPositionAccountabilities.RegDate = DateTime.Now
                Dim ID As Integer = ClsPositionAccountabilities.SaveWithID()
                'For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgAccountability2.Rows
                '    If IsNothing(DGRow1.Cells(2).Value) And IsNothing(DGRow1.Cells(3).Value) Then
                '        Continue For
                '    End If
                '    If DGRow1.Cells(1).Value.ToString() = DGRow.Cells(0).Value.ToString() Then
                '        Dim ClsPositionAccountabilitiesDtl = New Clshrs_PositionAccountabilitiesDtl(Page)
                '        ClsPositionAccountabilitiesDtl.PosAccountabilityID = ID
                '        ClsPositionAccountabilitiesDtl.EngName = DGRow1.Cells(2).Value
                '        ClsPositionAccountabilitiesDtl.ArbName = DGRow1.Cells(3).Value
                '        ClsPositionAccountabilitiesDtl.RegDate = DateTime.Now
                '        ClsPositionAccountabilitiesDtl.Save()
                '    End If
                'Next
            Next
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function AssignValue(ByRef ClsPositions As Clshrs_Positions) As Boolean
        Try
            With ClsPositions

                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .ParentID = DdlParentPosition.SelectedItem.Value
                If DdlParentPosition.SelectedItem.Value > 0 Then
                    .PositionLevelID = DdlPositionLevel.SelectedItem.Value
                End If
                .EmployeesNo = TxtNoOfEmployees.Text
                .ApplyValidation = chkApplyValidation.Checked
                .PositionBudget = TxtPositionBudget.Text
                .AppraisalTypeGroupID = ddlappraisaltypegroup.SelectedValue


            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues(ByVal ClsPositions As Clshrs_Positions) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsPositions
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                TxtNoOfEmployees.Text = .EmployeesNo
                chkApplyValidation.Checked = .ApplyValidation
                TxtPositionBudget.Text = .PositionBudget
                ddlappraisaltypegroup.SelectedValue = .AppraisalTypeGroupID
                Dim item As New System.Web.UI.WebControls.ListItem()

                Dim ClsPos As New Clshrs_PositionsLevels(Page)
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(.ConnectionString)

                DdlPositionLevel.Items.Clear()
                ClsPos.GetDropDownList(DdlPositionLevel, False)

                ClsPos.FindAll(" ID= " & IIf(IsNothing(.PositionLevelID), 0, .PositionLevelID))
                If ClsPos.ID > 0 Then
                    item.Value = .PositionLevelID
                    item.Text = ObjNavigationHandler.SetLanguage(Page, ClsPos.EngName & "/" & ClsPos.ArbName)

                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Page, ClsPos.ArbName & "/" & ClsPos.EngName)
                    End If

                    If Not DdlPositionLevel.Items.Contains(item) Then
                        DdlPositionLevel.Items.Add(item)
                        DdlPositionLevel.SelectedValue = item.Value
                    Else
                        DdlPositionLevel.SelectedValue = .PositionLevelID
                    End If

                End If
                DdlParentPosition.Items.Clear()
                Dim str As String = ""
                GetChildPositions(.ID, .ConnectionString, str)
                str = str.Trim(",")
                If str.Trim <> String.Empty Then
                    .GetDropDownList(DdlParentPosition, True, " ID <> " & .ID & " And ID  not in ( " & str & " )")
                Else
                    .GetDropDownList(DdlParentPosition, True, " ID <> " & .ID)
                End If
                DdlParentPosition.SelectedValue = IIf(.ParentID Is Nothing, 0, .ParentID)

                If ClsUser.EngName = Nothing Then
                    lblRegUserValue.Text = ""
                Else
                    lblRegUserValue.Text = ClsUser.EngName
                End If
                If Convert.ToDateTime(.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = Convert.ToDateTime(.RegDate).Date
                End If
                If .CancelDate = Nothing Then
                    lblCancelDateValue.Text = ""
                Else
                    lblCancelDateValue.Text = Convert.ToDateTime(.CancelDate).Date
                End If
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                Else
                    ImageButton_Delete.Enabled = True
                End If

                If (.ID > 0) Then
                    StrMode = "E"
                Else
                    StrMode = "N"
                End If
                SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                SetToolBarRecordPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, .Table, .ID)
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If
                If Page.IsPostBack Then
                    CreateOtherFields(.ID)
                End If

                Dim ClsPositionCompetences = New Clshrs_PositionCompetences(Page)
                ClsPositionCompetences.Find("canceldate is null and PositionID=" & .ID)
                uwgCompetences.DataSource = ClsPositionCompetences.DataSet.Tables(0)
                uwgCompetences.DataBind()

                Dim ClsPositionQualifications = New Clshrs_PositionQualifications(Page)
                ClsPositionQualifications.Find("canceldate is null and PositionID=" & .ID)
                uwgQualifications.DataSource = ClsPositionQualifications.DataSet.Tables(0)
                uwgQualifications.DataBind()

                Dim ClsPositionContacts = New Clshrs_PositionContacts(Page)
                ClsPositionContacts.Find("canceldate is null and PositionID=" & .ID)
                uwgContact.DataSource = ClsPositionContacts.DataSet.Tables(0)
                uwgContact.DataBind()

                Dim ClsPositionSuperVisor = New Clshrs_PositionSupervisors(Page)
                ClsPositionSuperVisor.Find("canceldate is null and PositionID=" & .ID)
                uwgSuperVisors.DataSource = ClsPositionSuperVisor.DataSet.Tables(0)
                uwgSuperVisors.DataBind()

                Dim ClsPositionAccountabilities = New Clshrs_PositionAccountabilities(Page)
                ClsPositionAccountabilities.Find("canceldate is null and PositionID=" & .ID)
                Dim Dt0 As Data.DataTable = ClsPositionAccountabilities.DataSet.Tables(0)
                uwgAccountability1.Rows.Clear()
                For i As Integer = 0 To Dt0.Rows.Count - 1
                    uwgAccountability1.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Dt0.Rows(i)(0), Dt0.Rows(i)(2), Dt0.Rows(i)(3), Dt0.Rows(i)(4), Dt0.Rows(i)(5), Dt0.Rows(i)(6)}))
                Next
                CreateEmptyRows(1, uwgAccountability1)



                Dim clspositioncompetences1 = New Clshrs_PositionCompetences(Page)
                clspositioncompetences1.Find("canceldate is null and PositionID=" & .ID)
                Dim Dt1 As Data.DataTable = clspositioncompetences1.DataSet.Tables(0)
                'uwgCompetences.Rows.Clear()
                'For i As Integer = 0 To Dt0.Rows.Count - 1
                '    uwgCompetences.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Dt0.Rows(i)(0), Dt0.Rows(i)(2), Dt0.Rows(i)(3), Dt0.Rows(i)(4), Dt0.Rows(i)(5), Dt0.Rows(i)(6)}))
                'Next
                'CreateEmptyRows(1, uwgCompetences)

                Dim ClsPositionAccountabilitiesDtl = New Clshrs_PositionAccountabilitiesDtl(Page)
                ClsPositionAccountabilitiesDtl.Find("canceldate is null and PosAccountabilityID in (select ID from hrs_PositionAccountabilities where PositionID=" & .ID & ")")
                'uwgAccountability2.Rows.Clear()
                'Dim Dt As Data.DataTable = ClsPositionAccountabilitiesDtl.DataSet.Tables(0)
                'For i As Integer = 0 To Dt.Rows.Count - 1
                '    uwgAccountability2.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Dt.Rows(i)(0), Dt.Rows(i)(1), Dt.Rows(i)(2), Dt.Rows(i)(3)}))
                'Next
                'If uwgAccountability1.Rows.Count > 0 Then
                '    Try


                '        uwgAccountability1.DisplayLayout.ActiveRow = uwgAccountability1.Rows(0)
                '        uwgAccountability1.Rows(0).Selected = True
                '        HideDetailsRows(uwgAccountability1.Rows(0).Cells.FromKey("ID").Value)
                '    Catch ex As Exception
                '    End Try
                'End If

            End With
            ' CreateEmptyRows(1, uwgAccountability1)
            'CreateEmptyRows(1, uwgAccountability2)
            CreateEmptyRows(1, uwgCompetences)
            CreateEmptyRows(1, uwgContact)
            CreateEmptyRows(1, uwgQualifications)
            CreateEmptyRows(1, uwgSuperVisors)
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
                    GetValues(ClsPositions)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsPositions.Find("ID=" & intID)
                    GetValues(ClsPositions)
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
        ClsPositions = New Clshrs_Positions(Me.Page)
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
        ClsPositions = New Clshrs_Positions(Me.Page)
        If IntId > 0 Then
            ClsPositions.Find("ID=" & IntId)
            GetValues(ClsPositions)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsPositions = New Clshrs_Positions(Me.Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Try
            ClsPositions.Find("Code='" & txtCode.Text & "'")
            IntId = ClsPositions.ID
            txtEngName.Focus()
            If ClsPositions.ID > 0 Then
                GetValues(ClsPositions)
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
        GetValues(ClsPositions)

        If (DdlParentPosition.Items.Count > 0) Then
            DdlParentPosition.SelectedIndex = 0
        End If

        If (DdlPositionLevel.Items.Count > 0) Then
            DdlPositionLevel.SelectedIndex = 0
        End If


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
        DdlParentPosition.SelectedIndex = 0
        DdlPositionLevel.SelectedIndex = 0
        TxtNoOfEmployees.Text = String.Empty
        chkApplyValidation.Checked = False
        TxtPositionBudget.Text = String.Empty
        ddlappraisaltypegroup.SelectedValue = 0
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsPositions = New Clshrs_Positions(Me.Page)
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
    Private Function GetChildPositions(ByVal BranchID As Integer, ByVal Connection As String, ByRef RetStr As String) As Boolean
        Dim strSelect As String = " Select ID from hrs_Positions where ParentID = " & BranchID
        Dim DS As New Data.DataSet
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, Data.CommandType.Text, strSelect)
        If (DS.Tables(0).Rows.Count > 0) Then
            For Each row As Data.DataRow In DS.Tables(0).Rows
                RetStr = RetStr & row("ID") & ","
                GetChildPositions(row("ID"), Connection, RetStr)
            Next
        Else
            Return True
        End If
    End Function
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsPositions.Table) = True Then
            Dim StrTablename As String
            ClsPositions = New Clshrs_Positions(Me)
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
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmRegions")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmRegions")
            End If
        End If
    End Function
    Private Sub CreateEmptyRows(ByVal intNoOfrows As Integer, ByVal objGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid)
        For i As Integer = 0 To intNoOfrows
            objGrid.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow())
        Next
    End Sub
    'Private Sub HideDetailsRows(ByVal intAccountabilityID As String)
    '    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgAccountability2.Rows
    '        If row.Cells(1).Value = intAccountabilityID Then
    '            row.Hidden = False
    '        Else
    '            row.Hidden = True
    '        End If
    '    Next
    '    CreateEmptyRows(1, uwgAccountability2)
    'End Sub
#End Region
    Public Function GetDropDownListGriduwgAccountability1() As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        CreateEmptyRows(1, uwgAccountability1)
        CreateEmptyRows(1, uwgCompetences)
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

        Dim strselect2 As String
        strselect2 = "select App_CriteriaGroups.* from App_CriteriaGroups join App_CriteriaGroupTypes on App_CriteriaGroups.CriteriaGroupTypeID=App_CriteriaGroupTypes.id where App_CriteriaGroupTypes.IsResponsibilities=1"
        Dim DSPositions As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)

        uwgAccountability1.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Clear()
        For Each Row As Data.DataRow In DSPositions.Tables(0).Rows
            uwgAccountability1.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & ""))
        Next


        Dim strselect3 As String
        strselect3 = "select App_CriteriaGroups.* from App_CriteriaGroups join App_CriteriaGroupTypes on App_CriteriaGroups.CriteriaGroupTypeID=App_CriteriaGroupTypes.id where App_CriteriaGroupTypes.IsCompetences=1"
        Dim DSCompetecies As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect3)

        uwgCompetences.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Clear()
        For Each Row As Data.DataRow In DSCompetecies.Tables(0).Rows
            uwgCompetences.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & ""))
        Next


    End Function

    Public Function GetDropDownListGriduwgAccountability2() As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem

        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

        Dim strselect2 As String
        strselect2 = " select * from App_Criteria join App_CriteriaGroups on App_Criteria.CriteriaGroupID =App_CriteriaGroups.ID join App_CriteriaGroupTypes on App_CriteriaGroups.CriteriaGroupTypeID=App_CriteriaGroupTypes.id where App_CriteriaGroupTypes.IsResponsibilities=1 "
        Dim DSPositions As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)

        uwgAccountability1.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Clear()
        For Each Row As Data.DataRow In DSPositions.Tables(0).Rows
            uwgAccountability1.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & ""))
        Next




    End Function
    Public Function GetDropDownListCompetences() As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem

        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

        Dim strselect2 As String
        strselect2 = "select * from App_Criteria join App_CriteriaGroups on App_Criteria.CriteriaGroupID =App_CriteriaGroups.ID join App_CriteriaGroupTypes on App_CriteriaGroups.CriteriaGroupTypeID=App_CriteriaGroupTypes.id where App_CriteriaGroupTypes.IsCompetences=1"
        Dim DSPositions As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)

        uwgCompetences.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Clear()
        For Each Row As Data.DataRow In DSPositions.Tables(0).Rows
            uwgCompetences.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & ""))
        Next




    End Function

    'Public Function GetDropDownListGriduwgAccountability2ByGroupID(CriteriaGroupID As Integer) As Boolean

    '    Dim Item As Global.System.Web.UI.WebControls.ListItem
    '    uwgAccountability2.Rows.Clear()

    '    CreateEmptyRows(1, uwgAccountability2)

    '    Dim ConnectionString As String
    '    ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
    '    Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

    '    Dim strselect2 As String
    '    strselect2 = "select * from App_Criteria where CriteriaGroupID=" & CriteriaGroupID & ""
    '    Dim DSPositions As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)

    '    uwgAccountability2.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Clear()
    '    For Each Row As Data.DataRow In DSPositions.Tables(0).Rows
    '        uwgAccountability2.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & ""))
    '    Next




    'End Function

    Protected Sub uwgAccountability1_ActiveRowChange(sender As Object, e As CellEventArgs) Handles uwgAccountability1.ActiveCellChange
        '  HideDetailsRows(e.Row.Cells(0).Value)
        ClsPositions = New Clshrs_Positions(Me.Page)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsPositions.ConnectionString)
        If Not String.IsNullOrEmpty(e.Cell.Value) Then
            'GetDropDownListGriduwgAccountability2ByGroupID(e.Cell.Value)
        End If

    End Sub
End Class
