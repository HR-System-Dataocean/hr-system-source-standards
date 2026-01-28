Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmRecInterviewsSchedule
    Inherits MainPage
#Region "Public Decleration"
    Private ClsInterviews As ClsRec_Interviews
    Private ClsInterviewsDetail1 As ClsRec_InterviewsDetail1
    Private ClsInterviewsDetail2 As ClsRec_InterviewsDetail2
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsInterviews = New ClsRec_Interviews(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim ClsOpenVacancy As New ClsRec_OpenVacancy(Page)
        Dim ClsEvaluationTypes As New ClsEval_EvaluationTypes(Page)
        Dim ClsBioGraphies As New ClsRec_BioGraphies(Page)
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsInterviews.ConnectionString)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsInterviews.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID

                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            If ClsObjects.Find(" Code='" & ClsBioGraphies.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    HiddenField_ApplicantSearch.Value = ClsSearchs.ID
                Else
                    HiddenField_ApplicantSearch.Value = 0
                End If
            End If
            If ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    HiddenField_InterviewerSearch.Value = ClsSearchs.ID
                Else
                    HiddenField_InterviewerSearch.Value = 0
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsInterviews.ConnectionString)
                ClsInterviews.AddOnChangeEventToControls("frmRecInterviewsSchedule", Page, UltraWebTab1)
                ClsOpenVacancy.GetDropDownList(ddlopenvacancy, True, "IsOpen = 1 and CancelDate is null and getdate() <= GEndDate")
                ClsEvaluationTypes.GetDropDownList(ddlEvalType, True, "ID in (select EvalType_ID from Eval_EvaluationTypesModule where Module_ID = '" & ConfigurationManager.AppSettings("RecModuleMenuID").ToString() & "') or ID in (select EvalRecruitmentID from hrs_Positions where ID in (select Position_ID from Rec_OpenVacancy where ID = " & ddlopenvacancy.SelectedValue & "))")
                ClsEvaluationTypes.GetList(uwgInterviewer.DisplayLayout.Bands(0).Columns(6).ValueList, False, "ID in (select EvalType_ID from Eval_EvaluationTypesModule where Module_ID = '" & ConfigurationManager.AppSettings("RecModuleMenuID").ToString() & "') or ID in (select EvalRecruitmentID from hrs_Positions where ID in (select Position_ID from Rec_OpenVacancy where ID = " & ddlopenvacancy.SelectedValue & "))")
                txtLang.Value = ObjNavigationHandler.SetLanguage(Page, "Eng/Arb")
                uwgApplicants.Rows.Add()
                uwgInterviewer.Rows.Add()
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsInterviews.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsInterviews.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsInterviews.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsInterviews = New ClsRec_Interviews(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsInterviews.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                ClsInterviews = New ClsRec_Interviews(Page)
                ClsInterviewsDetail1 = New ClsRec_InterviewsDetail1(Page)

                If ddlopenvacancy.Enabled = False Then
                    Return
                End If
                If txtCode.Text.Length = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                    Return
                ElseIf ddlopenvacancy.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Open Vacancy /لابد من إدخال الفرصة المتاحة"))
                    Return
                ElseIf ddlEvalType.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Evaluation Type /لابد من إدخال التقييم المطلوب"))
                    Return
                End If
                If uwgApplicants.Rows.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There Is No Applicants /لا يوجد متقدمين للوظيفة"))
                    Return
                End If
                If uwgInterviewer.Rows.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There Is No Interviewers /لا يوجد متقدمين موظفين للمقابلات الشخصية"))
                    Return
                End If
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
                    If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) And IsNothing(DGRow.Cells(5).Value) Then
                        Continue For
                    End If
                    Dim regex As New System.Text.RegularExpressions.Regex("^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                    If IsNothing(DGRow.Cells(4).Value) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Applicant Mail Doesn't Exist /بريد أحد المتقدمين غير موجود"))
                        Return
                    End If
                    If regex.Match(DGRow.Cells(4).Value).Success = False Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Applicant Mail Not Correct /بريد أحد المتقدمين ليس صحيح"))
                        Return
                    End If
                Next
                For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows
                    If IsNothing(DGRow1.Cells(2).Value) And IsNothing(DGRow1.Cells(3).Value) Then
                        Continue For
                    End If
                    Dim regex As New System.Text.RegularExpressions.Regex("^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                    If IsNothing(DGRow1.Cells(5).Value) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Interviewer Mail Doesn't Exist /بريد أحد الوظفين غير موجود"))
                        Return
                    End If
                    If regex.Match(DGRow1.Cells(5).Value).Success = False Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Interviewer Mail Not Correct /بريد أحد الوظفين ليس صحيح"))
                        Return
                    End If
                Next
                Dim SumPCT As Integer = 0
                For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows
                    If IsNothing(DGRow1.Cells(2).Value) And IsNothing(DGRow1.Cells(3).Value) Then
                        Continue For
                    End If
                    SumPCT = SumPCT + DGRow1.Cells(7).Value
                Next
                If SumPCT <> 100 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "it Must Be Evaluation Power Equal 100 /لابد وان تكون مجموع قوى التقييمات يساوى 100"))
                    Return
                End If

                ClsInterviews.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsInterviews.ID > 0 Then
                    ClsInterviews.Update("Code='" & txtCode.Text & "'")
                    Dim str As String = "delete from Rec_InterviewsDetail1 where InterView_ID = " & ClsInterviews.ID
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsInterviews.ConnectionString, Data.CommandType.Text, str)

                    Dim str2 As String = "delete from Eval_Evaluation where ID in (select Evaluation from Rec_InterviewsDetail2 where InterView_ID = '" & ClsInterviews.ID & "')"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsInterviews.ConnectionString, Data.CommandType.Text, str2)

                    Dim str1 As String = "delete from Rec_InterviewsDetail2 where InterView_ID = " & ClsInterviews.ID
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsInterviews.ConnectionString, Data.CommandType.Text, str1)

                    If (SaveDG(ClsInterviews.ID)) Then
                        ClsInterviews = New ClsRec_Interviews(Page)
                        ClsInterviews.Find("Code='" & txtCode.Text & "'")
                    End If
                Else

                    ClsInterviews.IsOpen = True
                    ClsInterviews.Save()
                    ClsInterviews.Find("Code='" & txtCode.Text & "'")
                    If ClsInterviews.ID > 0 Then
                        If (SaveDG(ClsInterviews.ID)) Then
                            ClsInterviews = New ClsRec_Interviews(Page)
                            clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                            ClsInterviews.Find("Code='" & txtCode.Text & "'")
                        End If
                    End If
                End If

                ClsInterviews.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsInterviews.Table, ClsInterviews.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                ClsInterviews = New ClsRec_Interviews(Page)
                ClsInterviewsDetail1 = New ClsRec_InterviewsDetail1(Page)

                If ddlopenvacancy.Enabled = False Then
                    Return
                End If
                If txtCode.Text.Length = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                    Return
                ElseIf ddlopenvacancy.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Open Vacancy /لابد من إدخال الفرصة المتاحة"))
                    Return
                ElseIf ddlEvalType.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Evaluation Type /لابد من إدخال التقييم المطلوب"))
                    Return
                End If
                If uwgApplicants.Rows.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There Is No Applicants /لا يوجد متقدمين للوظيفة"))
                    Return
                End If
                If uwgInterviewer.Rows.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There Is No Interviewers /لا يوجد متقدمين موظفين للمقابلات الشخصية"))
                    Return
                End If
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
                    If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) And IsNothing(DGRow.Cells(5).Value) Then
                        Continue For
                    End If
                    Dim regex As New System.Text.RegularExpressions.Regex("^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                    If IsNothing(DGRow.Cells(4).Value) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Applicant Mail Doesn't Exist /بريد أحد المتقدمين غير موجود"))
                        Return
                    End If
                    If regex.Match(DGRow.Cells(4).Value).Success = False Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Applicant Mail Not Correct /بريد أحد المتقدمين ليس صحيح"))
                        Return
                    End If
                Next
                For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows
                    If IsNothing(DGRow1.Cells(2).Value) And IsNothing(DGRow1.Cells(3).Value) Then
                        Continue For
                    End If
                    Dim regex As New System.Text.RegularExpressions.Regex("^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                    If IsNothing(DGRow1.Cells(5).Value) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Interviewer Mail Doesn't Exist /بريد أحد الوظفين غير موجود"))
                        Return
                    End If
                    If regex.Match(DGRow1.Cells(5).Value).Success = False Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Interviewer Mail Not Correct /بريد أحد الوظفين ليس صحيح"))
                        Return
                    End If
                Next
                Dim SumPCT As Integer = 0
                For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows
                    If IsNothing(DGRow1.Cells(2).Value) And IsNothing(DGRow1.Cells(3).Value) Then
                        Continue For
                    End If
                    SumPCT = SumPCT + DGRow1.Cells(7).Value
                Next
                If SumPCT <> 100 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "it Must Be Evaluation Power Equal 100 /لابد وان تكون مجموع قوى التقييمات يساوى 100"))
                    Return
                End If

                ClsInterviews.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If

                If ClsInterviews.ID > 0 Then
                    ClsInterviews.Update("Code='" & txtCode.Text & "'")
                    Dim str As String = "delete from Rec_InterviewsDetail1 where InterView_ID = " & ClsInterviews.ID
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsInterviews.ConnectionString, Data.CommandType.Text, str)

                    Dim str2 As String = "delete from Eval_Evaluation where ID in (select Evaluation from Rec_InterviewsDetail2 where InterView_ID = '" & ClsInterviews.ID & "')"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsInterviews.ConnectionString, Data.CommandType.Text, str2)

                    Dim str1 As String = "delete from Rec_InterviewsDetail2 where InterView_ID = " & ClsInterviews.ID
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsInterviews.ConnectionString, Data.CommandType.Text, str1)

                    If (SaveDG(ClsInterviews.ID)) Then
                        ClsInterviews = New ClsRec_Interviews(Page)
                        ClsInterviews.Find("Code='" & txtCode.Text & "'")
                    End If
                Else

                    ClsInterviews.IsOpen = True
                    ClsInterviews.Save()
                    ClsInterviews.Find("Code='" & txtCode.Text & "'")
                    If ClsInterviews.ID > 0 Then
                        If (SaveDG(ClsInterviews.ID)) Then
                            ClsInterviews = New ClsRec_Interviews(Page)
                            clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                            ClsInterviews.Find("Code='" & txtCode.Text & "'")
                        End If
                    End If
                End If

                ClsInterviews.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsInterviews.Table, ClsInterviews.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsInterviews.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsInterviews.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsInterviews.ID & "&TableName=" & ClsInterviews.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsInterviews.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsInterviews.ID & "&TableName=" & ClsInterviews.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsInterviews.Table
                ClsInterviews.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsInterviews.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsInterviews.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsInterviews.Find(" Code= '" & txtCode.Text & "'")
                If ClsInterviews.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsInterviews.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsInterviews.CheckDiff(ClsInterviews, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsInterviews.FirstRecord()
                GetValues()
            Case "Previous"
                ClsInterviews.Find("Code='" & txtCode.Text & "'")
                If Not ClsInterviews.previousRecord() Then
                    ClsInterviews.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsInterviews.Find("Code='" & txtCode.Text & "'")
                If Not ClsInterviews.NextRecord() Then
                    ClsInterviews.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsInterviews.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub ddlopenvacancy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlopenvacancy.SelectedIndexChanged
        uwgApplicants.Rows.Clear()
        uwgInterviewer.Rows.Clear()
        uwgApplicants.Rows.Add()
        uwgInterviewer.Rows.Add()
        Dim ClsEvaluationTypes As New ClsEval_EvaluationTypes(Page)
        ClsEvaluationTypes.GetDropDownList(ddlEvalType, True, "ID in (select EvalType_ID from Eval_EvaluationTypesModule where Module_ID = '" & ConfigurationManager.AppSettings("RecModuleMenuID").ToString() & "') or ID in (select EvalRecruitmentID from hrs_Positions where ID in (select Position_ID from Rec_OpenVacancy where ID = " & ddlopenvacancy.SelectedValue & "))")
        ClsEvaluationTypes.GetList(uwgInterviewer.DisplayLayout.Bands(0).Columns(6).ValueList, False, "ID in (select EvalType_ID from Eval_EvaluationTypesModule where Module_ID = '" & ConfigurationManager.AppSettings("RecModuleMenuID").ToString() & "') or ID in (select EvalRecruitmentID from hrs_Positions where ID in (select Position_ID from Rec_OpenVacancy where ID = " & ddlopenvacancy.SelectedValue & "))")
    End Sub
    Protected Sub ddlEvalType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEvalType.SelectedIndexChanged
        uwgInterviewer.Rows.Clear()
        uwgInterviewer.Rows.Add()
    End Sub
    Protected Sub txtReturned_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReturned.TextChanged
        If txtCode.Text = "" And ddlopenvacancy.SelectedValue = 0 Then
            Return
        End If
        Dim arg As String() = txtReturned.Text.Split("|")
        Dim PositionID As String = GetPosition(ddlopenvacancy.SelectedValue)
        Dim i As Integer = 0
        If arg.Length > 0 Then
            uwgApplicants.Rows.Clear()
        End If
        For i = 0 To arg.Length - 1
            If arg(i).ToString() = "0" Then
                Continue For
            End If
            Dim arg1 As String() = GetApplicantName(arg(i).ToString() & "|0|" & PositionID).Split("|")
            If arg1.Length > 0 Then
                uwgApplicants.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, arg1(1), arg1(2).ToString(), arg1(0).ToString(), arg1(3).ToString(), ClsDataAcessLayer.FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy").ToString("dd/MM/yyyy"), ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy"), "1200"}))
            End If

        Next i
    End Sub
#End Region

#Region "Private Functions"
    Private Function SaveDG(ByVal InterviewID As Integer) As Boolean
        Try
            Dim ClsEvaluation As New ClsEval_Evaluation(Page)
            Dim ClsCompanies As New Clssys_Companies(Page)
            Dim ClsOpenVacancy As New ClsRec_OpenVacancy(Page)
            ClsOpenVacancy.Find("ID = " & ddlopenvacancy.SelectedValue)
            Dim ClsPositions As New ClsRec_Positions(Page)
            Dim ClsBioGraphies As New ClsRec_BioGraphies(Page)
            Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsPositions.Find("ID = " & ClsOpenVacancy.Position_ID)
            ClsCompanies.Find("ID > 0")

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) And IsNothing(DGRow.Cells(5).Value) Then
                    Continue For
                End If
                ClsInterviewsDetail1 = New ClsRec_InterviewsDetail1(Page)
                ClsInterviewsDetail1.InterView_ID = InterviewID
                ClsInterviewsDetail1.Applicant_ID = DGRow.Cells(1).Value
                ClsInterviewsDetail1.GStartDate = DGRow.Cells(5).Value
                ClsInterviewsDetail1.HStartDate = DGRow.Cells(6).Value
                ClsInterviewsDetail1.Houre = DGRow.Cells(7).Value
                ClsInterviewsDetail1.IsSelected = False
                ClsInterviewsDetail1.Save()
                ClsBioGraphies.Find("ID = " & DGRow.Cells(1).Value)
                ClsBioGraphies.E_Mail = DGRow.Cells(4).Value
                ClsBioGraphies.Update("ID = " & DGRow.Cells(1).Value)

                For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows
                    If IsNothing(DGRow1.Cells(2).Value) And IsNothing(DGRow1.Cells(3).Value) And IsNothing(DGRow1.Cells(5).Value) Then
                        Continue For
                    End If
                    ClsInterviewsDetail2 = New ClsRec_InterviewsDetail2(Page)
                    ClsInterviewsDetail2.InterView_ID = InterviewID
                    ClsInterviewsDetail2.Applicant_ID = DGRow.Cells(1).Value
                    ClsInterviewsDetail2.Interviewer_ID = DGRow1.Cells(2).Value
                    ClsInterviewsDetail2.DefaultEvalType = DGRow1.Cells(6).Value
                    If IsNothing(DGRow1.Cells(7).Value) Then
                        ClsInterviewsDetail2.PowerPCT = 0
                    Else
                        ClsInterviewsDetail2.PowerPCT = DGRow1.Cells(7).Value
                    End If

                    ClsEvaluation = New ClsEval_Evaluation(Page)
                    ClsEvaluation.EvalTypeID = ClsInterviewsDetail2.DefaultEvalType
                    ClsEvaluation.Dest_EmployeeID = ClsInterviewsDetail2.Interviewer_ID
                    ClsEvaluation.Target_EmployeeID = ClsInterviewsDetail2.Applicant_ID
                    ClsEvaluation.GStartDate = ClsInterviewsDetail1.GStartDate
                    ClsEvaluation.HStartDate = ClsInterviewsDetail1.HStartDate
                    ClsEvaluation.GEndDate = ClsInterviewsDetail1.GStartDate
                    ClsEvaluation.HEndDate = ClsInterviewsDetail1.HStartDate
                    ClsEvaluation.RegComputerID = 2
                    ClsInterviewsDetail2.Evaluation = ClsEvaluation.Save()
                    ClsInterviewsDetail2.Save()

                    ClsEmployees.Find("ID = " & DGRow1.Cells(2).Value)
                    ClsEmployees.E_Mail = DGRow1.Cells(5).Value
                    ClsEmployees.Update("ID = " & DGRow1.Cells(2).Value)


                    If DGRow1.Cells(8).Value = True And ChkInterViewserSend.Checked = True Then
                        Try
                            Dim SMTPHost As String = ConfigurationManager.AppSettings("SMTPHost").ToString()
                            Dim SMTPPort As String = ConfigurationManager.AppSettings("SMTPPort").ToString()
                            Dim SMTPUsername As String = ConfigurationManager.AppSettings("SMTPUsername").ToString()
                            Dim SMTPPassword As String = ConfigurationManager.AppSettings("SMTPPassword").ToString()
                            Dim SMTPFrom As String = ConfigurationManager.AppSettings("SMTPFrom").ToString()
                            Dim ApplicationURL As String = ConfigurationManager.AppSettings("ApplicationURL").ToString()

                            Dim smtp As New System.Net.Mail.SmtpClient()
                            Dim message As New System.Net.Mail.MailMessage()
                            Dim FromAddress As New System.Net.Mail.MailAddress(Convert.ToString(SMTPFrom), ClsCompanies.ArbName & " " & ClsCompanies.EngName)
                            smtp.Host = SMTPHost
                            smtp.Port = SMTPPort
                            Dim cred As New System.Net.NetworkCredential(SMTPUsername, SMTPPassword)
                            message.From = FromAddress

                            message.To.Clear()
                            message.To.Add(DGRow1.Cells(5).Value)
                            message.Subject = "Interview مقابلة شخصية"
                            message.IsBodyHtml = True

                            Dim EngHeader As String = "<H2 style=""background-color:white;"" align=center> This Mail Is From " & ClsCompanies.EngName & "</H2></br>" & _
                                                      "<H1 style=""background-color:white;""> We are honored to inform you that date of your interview for a Position " & ClsPositions.EngName & "</H1></br>"
                            Dim ApplicantNameEn As String() = GetApplicantName(DGRow.Cells(2).Value & "|Eng|" & ClsPositions.ID).Split("|")
                            Dim EngDetail As String = "<H1 style=""background-color:white;""> IS " & DGRow.Cells(5).Value & " Time " & DGRow.Cells(7).Value & " And The Interview With Mr/Mrs " & ApplicantNameEn(0).ToString() & " </H1></br>" & _
                                                      "<H1> <a href='" & ApplicationURL & "/Pages/Hr/frmRecCVOnline.aspx?CVcode=" & DGRow.Cells(2).Value & "'>The CV URL</a></H1></br>" & _
                                                      "<H1> <a href='" & ApplicationURL & "/Pages/Hr/frmEvalEvaluation.aspx?EvalID=" & ClsInterviewsDetail2.Evaluation & "'>The Evaluation Link URL</a></H1></br>"

                            Dim EngFinish As String = "<H2 style=""background-color:white;""> Regards" & "</H2> </br>"
                            Dim ArbHeader As String = "<H2 style=""background-color:white;"" align=center> هذا البريد الإلكترونى من " & ClsCompanies.ArbName & "</H2></br>" & _
                                                      "<H1 style=""background-color:white;""> يسعدنا ان نعلمكم بأن موعد مقابلتكم الشخصية لوظيفة " & ClsPositions.ArbName & "</H1></br>"
                            Dim ApplicantNameAr As String() = GetApplicantName(DGRow.Cells(2).Value & "|Arb|" & ClsPositions.ID).Split("|")
                            Dim ArbDetail As String = "<H1 style=""background-color:white;""> سيكون " & DGRow.Cells(6).Value & " الساعة " & DGRow.Cells(7).Value & "وستكون المقابلة مع السيد/السيدة " & ApplicantNameAr(0).ToString() & "</H1></br>" & _
                                                      "<H1> <a href='" & ApplicationURL & "/Pages/Hr/frmRecCVOnline.aspx?CVcode=" & DGRow.Cells(2).Value & "'>رابط السيرة الذاتية</a></H1></br>" & _
                                                      "<H1> <a href='" & ApplicationURL & "/Pages/Hr/frmEvalEvaluation.aspx?EvalID=" & ClsInterviewsDetail2.Evaluation & "'>رابط التقييم</a></H1></br>"
                            Dim ArbFinish As String = "<H2 style=""background-color:white;""> تحياتنا" & "</H2> </br>"

                            message.Body = EngHeader & EngDetail & EngFinish & _
                                               "<H1 style=""background-color:white;""> **********************************************" & "</H1> </br>" & _
                                               "<H1 style=""background-color:white;""> **********************************************" & "</H1> </br>" & _
                                               ArbHeader & ArbDetail & ArbFinish
                            smtp.UseDefaultCredentials = False
                            smtp.EnableSsl = True
                            smtp.Credentials = cred
                            smtp.Send(message)
                        Catch ex As Exception
                        End Try
                    End If
                Next
                If DGRow.Cells(8).Value = True And ChkApplicantSend.Checked = True Then
                    Try
                        Dim SMTPHost As String = ConfigurationManager.AppSettings("SMTPHost").ToString()
                        Dim SMTPPort As String = ConfigurationManager.AppSettings("SMTPPort").ToString()
                        Dim SMTPUsername As String = ConfigurationManager.AppSettings("SMTPUsername").ToString()
                        Dim SMTPPassword As String = ConfigurationManager.AppSettings("SMTPPassword").ToString()
                        Dim SMTPFrom As String = ConfigurationManager.AppSettings("SMTPFrom").ToString()
                        Dim ApplicationURL As String = ConfigurationManager.AppSettings("ApplicationURL").ToString()

                        Dim smtp As New System.Net.Mail.SmtpClient()
                        Dim message As New System.Net.Mail.MailMessage()
                        Dim FromAddress As New System.Net.Mail.MailAddress(Convert.ToString(SMTPFrom), ClsCompanies.ArbName & " " & ClsCompanies.EngName)
                        smtp.Host = SMTPHost
                        smtp.Port = SMTPPort
                        Dim cred As New System.Net.NetworkCredential(SMTPUsername, SMTPPassword)
                        message.From = FromAddress

                        message.To.Clear()
                        message.To.Add(DGRow.Cells(4).Value)
                        message.Subject = "Interview مقابلة شخصية"
                        message.IsBodyHtml = True
                        Dim EngHeader As String = "<H2 style=""background-color:white;"" align=center> This Mail Is From " & ClsCompanies.EngName & "</H2></br>" & _
                                                  "<H1 style=""background-color:white;""> We are honored to inform you that date of your interview for a Position " & ClsPositions.EngName & "</H1></br>"
                        EngHeader = EngHeader & "<H1 style=""background-color:white;""> IS " & DGRow.Cells(5).Value & " Time " & DGRow.Cells(7).Value & "</H1></br>"
                        Dim EngDetail As String = ""
                        Dim EngFinish As String = ""

                        For Each DGRow2 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows
                            If IsNothing(DGRow2.Cells(2).Value) And IsNothing(DGRow2.Cells(3).Value) Then
                                Continue For
                            End If
                            Dim InterviewerName As String() = GetInterviewerName(DGRow2.Cells(3).Value & "|Eng").Split("|")
                            EngDetail = EngDetail & "<H1 style=""background-color:white;""> The Interview With Mr/Mrs " & InterviewerName(0).ToString() & " </H1></br>"
                        Next
                        EngFinish = "<H2 style=""background-color:white;""> Regards" & "</H2> </br>"

                        Dim ArbHeader As String = "<H2 style=""background-color:white;"" align=center> هذا البريد الإلكترونى من " & ClsCompanies.ArbName & "</H2></br>" & _
                                                  "<H1 style=""background-color:white;""> يسعدنا ان نعلمكم بأن موعد مقابلتكم الشخصية لوظيفة " & ClsPositions.ArbName & "</H1></br>"
                        ArbHeader = ArbHeader & "<H1 style=""background-color:white;""> سيكون " & DGRow.Cells(6).Value & " الساعة " & DGRow.Cells(7).Value & "</H1></br>"
                        Dim ArbDetail As String = ""
                        Dim ArbFinish As String = ""
                        For Each DGRow3 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows
                            If IsNothing(DGRow3.Cells(2).Value) And IsNothing(DGRow3.Cells(3).Value) Then
                                Continue For
                            End If
                            Dim InterviewerName As String() = GetInterviewerName(DGRow3.Cells(3).Value & "|Arb").Split("|")
                            ArbDetail = ArbDetail & "<H1 style=""background-color:white;""> والمقابلة مع السيد/السيدة " & InterviewerName(0).ToString() & "</H1></br>"
                        Next
                        ArbFinish = "<H2 style=""background-color:white;""> تحياتنا" & "</H2> </br>"
                        message.Body = EngHeader & EngDetail & EngFinish & _
                                           "<H1 style=""background-color:white;""> **********************************************" & "</H1> </br>" & _
                                           "<H1 style=""background-color:white;""> **********************************************" & "</H1> </br>" & _
                                           ArbHeader & ArbDetail & ArbFinish
                        smtp.UseDefaultCredentials = False
                        smtp.EnableSsl = True
                        smtp.Credentials = cred
                        smtp.Send(message)
                    Catch ex As Exception

                    End Try
                End If
            Next
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function AssignValues() As Boolean
        Try
            With ClsInterviews
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .OpenVacancy_ID = ddlopenvacancy.SelectedValue
                .DefaultEvalType = ddlEvalType.SelectedValue
                .SendInterviwer = ChkInterViewserSend.Checked
                .SendApplicant = ChkApplicantSend.Checked
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsInterviewsDetail1 = New ClsRec_InterviewsDetail1(Page)
        Dim ClsOpenVacancy As New ClsRec_OpenVacancy(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsInterviewsDetail1.ConnectionString)
        Try
            SetToolBarDefaults()
            With ClsInterviews
                ClsOpenVacancy.GetDropDownList(ddlopenvacancy, True, "")
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                ddlopenvacancy.SelectedValue = .OpenVacancy_ID
                ddlEvalType.SelectedValue = .DefaultEvalType
                ddlopenvacancy.Enabled = True
                If .OpenVacancy_ID <> 0 Then
                    ClsOpenVacancy.Find("IsOpen = 1 and CancelDate is null and getdate() <= GEndDate and ID = " & .OpenVacancy_ID)
                    If ClsOpenVacancy.DataSet.Tables(0).Rows.Count > 0 Then
                        ddlopenvacancy.Enabled = True
                    Else
                        ddlopenvacancy.Enabled = False
                    End If
                End If
                ChkApplicantSend.Checked = .SendApplicant
                ChkInterViewserSend.Checked = .SendInterviwer
                uwgApplicants.Columns(3).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                Dim strgetApplicants As String = " select A.Applicant_ID,(ISNULL(B.EngName,'') + ' ' + ISNULL(B.FatherEngName,'') + ' ' + ISNULL(B.FamilyEngName,'') + ' ' + ISNULL(B.GrandEngName,'')) AS EngName," & _
                              " (ISNULL(B.ArbName,'') + ' ' + ISNULL(B.FatherArbName,'') + ' ' + ISNULL(B.FamilyArbName,'') + ' ' + ISNULL(B.GrandArbName,'')) AS ArbName," & _
                              " B.E_Mail,Convert(Varchar(10),A.GStartDate,103) GStartDate,A.HStartDate,A.Houre,B.Code,A.ID" & _
                              " from Rec_InterviewsDetail1 A left outer join Rec_BioGraphies B on A.Applicant_ID = B.ID" & _
                              " where A.CancelDate is null and A.InterView_ID = " & .ID
                Dim DTgetApplicants As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(.ConnectionString, Data.CommandType.Text, strgetApplicants).Tables(0)
                uwgApplicants.DataSource = DTgetApplicants
                uwgApplicants.DataBind()

                uwgInterviewer.Columns(4).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                Dim strgetInterviewers As String = " select Distinct A.Interviewer_ID,(ISNULL(B.EngName,'') + ' ' + ISNULL(B.FatherEngName,'') + ' ' + ISNULL(B.FamilyEngName,'') + ' ' + ISNULL(B.GrandEngName,'')) AS EngName," & _
                              " (ISNULL(B.ArbName,'') + ' ' + ISNULL(B.FatherArbName,'') + ' ' + ISNULL(B.FamilyArbName,'') + ' ' + ISNULL(B.GrandArbName,'')) AS ArbName," & _
                              " B.E_Mail,B.Code,A.InterView_ID,A.PowerPCT,A.DefaultEvalType" & _
                              " from Rec_InterviewsDetail2 A left outer join hrs_Employees B on A.Interviewer_ID = B.ID" & _
                              " where A.CancelDate is null and A.InterView_ID = " & .ID
                Dim DTgetInterviewers As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(.ConnectionString, Data.CommandType.Text, strgetInterviewers).Tables(0)
                uwgInterviewer.DataSource = DTgetInterviewers
                uwgInterviewer.DataBind()
            End With
            uwgApplicants.Rows.Add()
            uwgInterviewer.Rows.Add()
            If Not ClsInterviews.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsInterviews.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsInterviews.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsInterviews.RegDate).Date
            End If
            If ClsInterviews.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsInterviews.CancelDate).Date
            End If
            If Not ClsInterviews.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()


            If (ClsInterviews.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsInterviews.ConnectionString, ClsInterviews.DataBaseUserRelatedID, ClsInterviews.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsInterviews.ConnectionString, ClsInterviews.DataBaseUserRelatedID, ClsInterviews.GroupID, ClsInterviews.Table, ClsInterviews.ID)
            If Not ClsInterviews.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsInterviews.ID)
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
                    ClsInterviews.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsInterviews.Find("ID=" & intID)
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
            With ClsInterviews
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
        ClsInterviews = New ClsRec_Interviews(Me)
        Try
            With ClsInterviews
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
        ClsInterviews = New ClsRec_Interviews(Me)
        If IntId > 0 Then
            ClsInterviews.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsInterviews = New ClsRec_Interviews(Me)
        Try
            ClsInterviews.Find("Code='" & txtCode.Text & "'")
            IntId = ClsInterviews.ID
            txtEngName.Focus()
            If ClsInterviews.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsInterviews.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
                uwgApplicants.Rows.Add()
                uwgInterviewer.Rows.Add()
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsInterviews.ConnectionString, ClsInterviews.DataBaseUserRelatedID, ClsInterviews.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsInterviews.ConnectionString, ClsInterviews.DataBaseUserRelatedID, ClsInterviews.GroupID, ClsInterviews.Table, IntId)
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
        Dim ClsOpenVacancy As New ClsRec_OpenVacancy(Page)
        ClsInterviews.Clear()
        GetValues()

        ClsOpenVacancy.GetDropDownList(ddlopenvacancy, True, "IsOpen = 1 and CancelDate is null and getdate() <= GEndDate")
        UltraWebTab1.SelectedTab = 0

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
        ddlopenvacancy.SelectedValue = 0
        ddlEvalType.SelectedValue = 0
        ChkApplicantSend.Checked = False
        ChkInterViewserSend.Checked = False
        uwgApplicants.Rows.Clear()
        uwgInterviewer.Rows.Clear()
        uwgApplicants.Rows.Add()
        uwgInterviewer.Rows.Add()
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsInterviews = New ClsRec_Interviews(Page)
        ClsInterviews.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsInterviews.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsInterviews.Table) = True Then
            Dim StrTablename As String
            ClsInterviews = New ClsRec_Interviews(Me)
            StrTablename = ClsInterviews.Table
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

#Region "Page Method"
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetApplicantName(ByVal Args As String) As String
        Dim ConnString As String = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strEmpName As String = ""
        Dim strEngName As String = ""
        Dim strArbName As String = ""
        Dim arg As String() = Args.Split("|")

        Dim sqlstr As String = "select ID,Code,EngName,ArbName,FamilyEngName,FamilyArbName,FatherEngName,FatherArbName,GrandEngName,GrandArbName,E_Mail from Rec_BioGraphies where CancelDate is null and IsUsed <> 1 and Code = '" & arg(0).ToString() & "' and Position_ID = '" & arg(2).ToString() & "'"
        Dim Dt As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, sqlstr).Tables(0)
        If Dt.Rows.Count > 0 Then
            strEngName = Dt.Rows(0)("EngName").ToString() & "," & Dt.Rows(0)("FatherEngName").ToString() & "," & Dt.Rows(0)("GrandEngName").ToString() & "," & Dt.Rows(0)("FamilyEngName").ToString()
            strArbName = Dt.Rows(0)("ArbName").ToString() & "," & Dt.Rows(0)("FatherArbName").ToString() & "," & Dt.Rows(0)("GrandArbName").ToString() & "," & Dt.Rows(0)("FamilyArbName").ToString()
            If strArbName = ",,," Then
                strArbName = strEngName
            End If
            If strEngName = ",,," Then
                strEngName = strArbName
            End If
            If arg(1).ToString() = "Arb" Then
                strEmpName = strArbName
            Else
                strEmpName = strEngName
            End If
            Return strEmpName & "|" & Dt.Rows(0)("ID").ToString() & "|" & Dt.Rows(0)("Code").ToString() & "|" & IIf(Dt.Rows(0)("E_Mail").ToString() = "", "No Mail", Dt.Rows(0)("E_Mail").ToString())
        Else
            Return String.Empty
        End If
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetInterviewerName(ByVal Args As String) As String
        Dim ConnString As String = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strEmpName As String = ""
        Dim strEngName As String = ""
        Dim strArbName As String = ""
        Dim arg As String() = Args.Split("|")

        Dim sqlstr As String = "select ID,Code,EngName,ArbName,FamilyEngName,FamilyArbName,FatherEngName,FatherArbName,GrandEngName,GrandArbName,E_Mail from hrs_Employees where CancelDate is null and Code = '" & arg(0).ToString() & "'"
        Dim Dt As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, sqlstr).Tables(0)
        If Dt.Rows.Count > 0 Then
            strEngName = Dt.Rows(0)("EngName").ToString() & "," & Dt.Rows(0)("FatherEngName").ToString() & "," & Dt.Rows(0)("GrandEngName").ToString() & "," & Dt.Rows(0)("FamilyEngName").ToString()
            strArbName = Dt.Rows(0)("ArbName").ToString() & "," & Dt.Rows(0)("FatherArbName").ToString() & "," & Dt.Rows(0)("GrandArbName").ToString() & "," & Dt.Rows(0)("FamilyArbName").ToString()
            If strArbName = ",,," Then
                strArbName = strEngName
            End If
            If strEngName = ",,," Then
                strEngName = strArbName
            End If
            If arg(1).ToString() = "Arb" Then
                strEmpName = strArbName
            Else
                strEmpName = strEngName
            End If
            Return strEmpName & "|" & Dt.Rows(0)("ID").ToString() & "|" & Dt.Rows(0)("Code").ToString() & "|" & IIf(Dt.Rows(0)("E_Mail").ToString() = "", "No Mail", Dt.Rows(0)("E_Mail").ToString())
        Else
            Return String.Empty
        End If
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetPosition(ByVal Args As String) As String
        Dim ConnString As String = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim arg As String() = Args.Split("|")
        Dim sqlstr As String = "select * from Rec_OpenVacancy where CancelDate is null and ID = '" & arg(0).ToString() & "'"
        Dim Dt As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, sqlstr).Tables(0)
        If Dt.Rows.Count > 0 Then
            Return Dt.Rows(0)("Position_ID").ToString()
        Else
            Return String.Empty
        End If
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetPosition1(ByVal Args As String) As String
        Dim ConnString As String = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim arg As String() = Args.Split("|")
        Dim sqlstr As String = "select * from Rec_OpenVacancy where CancelDate is null and ID = '" & arg(0).ToString() & "'"
        Dim Dt As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, sqlstr).Tables(0)
        If Dt.Rows.Count > 0 Then
            Return Dt.Rows(0)("Position_ID").ToString()
        Else
            Return String.Empty
        End If
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function Greg2Hijri(ByVal DateValue As String) As Object
        Dim GDate As String = DateValue
        If ClsDataAcessLayer.IsGreg(GDate) = False Then
            GDate = ClsDataAcessLayer.FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        End If
        Return GDate & "|" & ClsDataAcessLayer.GregToHijri(GDate, "dd/MM/yyyy")
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function Hijri2Greg(ByVal DateValue As String) As Object
        Dim HDate As String = DateValue
        If ClsDataAcessLayer.IsHijri(HDate) = False Then
            HDate = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        End If
        Return ClsDataAcessLayer.HijriToGreg(HDate, "dd/MM/yyyy") & "|" & HDate
    End Function
#End Region
End Class
