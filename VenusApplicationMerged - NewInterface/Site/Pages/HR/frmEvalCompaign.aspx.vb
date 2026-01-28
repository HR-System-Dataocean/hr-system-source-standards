Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEvalCompaign
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEvaluationCompaign As ClsEval_EvaluationCompaign
    Private ClsEvaluationCompaignDetail1 As ClsEval_EvaluationCompaignDetail1
    Private ClsEvaluationCompaignDetail2 As ClsEval_EvaluationCompaignDetail2
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
        Dim ClsEvaluationCompainTypes As New ClsEval_EvaluationCompainTypes(Page)
        Dim ClsEvaluationTypes As New ClsEval_EvaluationTypes(Page)
        Dim ClsDepartments As New Clssys_Departments(Page)
        Dim ClsPositions As New Clshrs_Positions(Page)
        Dim ClsBioGraphies As New ClsRec_BioGraphies(Page)
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim clsIntervals As New Clshrs_Intervals(Page)
        ClsEvaluationCompaign = New ClsEval_EvaluationCompaign(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvaluationCompaign.ConnectionString)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsEvaluationCompaign.Table.Trim & "'") Then
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
                Page.Session.Add("ConnectionString", ClsEvaluationCompaign.ConnectionString)
                ClsEvaluationCompaign.AddOnChangeEventToControls("frmEvalCompaign", Page, UltraWebTab1)

                ClsEvaluationCompainTypes.GetDropDownList(ddlCompaType, True)

                ClsDepartments.GetDropDownList(ddlfromdepartment, True)
                ClsDepartments.GetDropDownList(ddltodepartment, True)
                ClsPositions.GetDropDownList(ddlFromposition, True)
                ClsPositions.GetDropDownList(ddltoposition, True)

                ddltypeofcompagn.Items.Add(New ListItem(ObjNavigationHandler.SetLanguage(Page, "Select Type/إختر النوع"), 0))
                ddltypeofcompagn.Items.Add(New ListItem(ObjNavigationHandler.SetLanguage(Page, "Down Level/المستوى الأدنى"), 1))
                ddltypeofcompagn.Items.Add(New ListItem(ObjNavigationHandler.SetLanguage(Page, "Up Level/المستوى الأعلى"), 2))
                ddltypeofcompagn.Items.Add(New ListItem(ObjNavigationHandler.SetLanguage(Page, "Same Level/المستوى المتماثل"), 3))
                ddltypeofcompagn.Items.Add(New ListItem(ObjNavigationHandler.SetLanguage(Page, "Rondom Level/التقييم العشوائى"), 4))
                ddltypeofcompagn.Items.Add(New ListItem(ObjNavigationHandler.SetLanguage(Page, "External Level/التقييم الخارجى"), 5))

                ClsEvaluationTypes.GetDropDownList(ddlEvalType, True, "")

                ClsEvaluationTypes.GetList(uwgApplicants.DisplayLayout.Bands(0).Columns(5).ValueList, False, "ID in (select EvalType_ID from Eval_EvaluationTypesModule where Module_ID = '" & ConfigurationManager.AppSettings("EvalModuleMenuID").ToString() & "')")
                uwgApplicants.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Add(1, ObjNavigationHandler.SetLanguage(Page, "Down Level/المستوى الأدنى"))
                uwgApplicants.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Add(2, ObjNavigationHandler.SetLanguage(Page, "Up Level/المستوى الأعلى"))
                uwgApplicants.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Add(3, ObjNavigationHandler.SetLanguage(Page, "Same Level/المستوى المتماثل"))
                uwgApplicants.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Add(4, ObjNavigationHandler.SetLanguage(Page, "Rondom Level/التقييم العشوائى"))
                uwgApplicants.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Add(5, ObjNavigationHandler.SetLanguage(Page, "External Level/التقييم الخارجى"))
                txtLang.Value = ObjNavigationHandler.SetLanguage(Page, "Eng/Arb")
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsEvaluationCompaign.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsEvaluationCompaign.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEvaluationCompaign.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsEvaluationCompaign = New ClsEval_EvaluationCompaign(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvaluationCompaign.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text.Length = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                    Return
                ElseIf ddlCompaType.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter compaign Type /لابد من إدخال نوع التقييم"))
                    Return
                ElseIf ddlEvalType.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Evaluation Type /لابد من إدخال التقييم المطلوب"))
                    Return
                End If
                If uwgApplicants.Rows.Count < 1 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There Is No Evaluators /لا يوجد الذين سيقومون بالتقييم"))
                    Return
                End If
                If uwgInterviewer.Rows.Count < 1 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There Is No Whome Will Evaluated /لا يوجد الذين سيتم تقييمهم  "))
                    Return
                End If
                If ChkApplicantSend.Checked Then
                    For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
                        If DGRow.Cells(6).Value <> True Then
                            Continue For
                        End If
                        Dim regex As New System.Text.RegularExpressions.Regex("^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                        If IsNothing(DGRow.Cells(3).Value) Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Evaluators Mail Doesn't Exist /بريد أحد الموظفين الذين سيقومون بالتقييم غير موجود"))
                            Return
                        End If
                        If regex.Match(DGRow.Cells(3).Value).Success = False Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Evaluators Mail Not Correct /بريد أحد الموظفين الذين سيقومون بالتقييم ليس صحيح"))
                            Return
                        End If
                    Next
                End If
                SavePart()
                AfterOperation()
            Case "Save"
                If txtCode.Text.Length = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                    Return
                ElseIf ddlCompaType.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter compaign Type /لابد من إدخال نوع التقييم"))
                    Return
                ElseIf ddlEvalType.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Evaluation Type /لابد من إدخال التقييم المطلوب"))
                    Return
                End If
                If uwgApplicants.Rows.Count < 1 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There Is No Evaluators /لا يوجد الذين سيقومون بالتقييم"))
                    Return
                End If
                If uwgInterviewer.Rows.Count < 1 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "There Is No Whome Will Evaluated /لا يوجد الذين سيتم تقييمهم  "))
                    Return
                End If
                If ChkApplicantSend.Checked Then
                    For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
                        If DGRow.Cells(6).Value <> True Then
                            Continue For
                        End If
                        Dim regex As New System.Text.RegularExpressions.Regex("^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                        If IsNothing(DGRow.Cells(3).Value) Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Evaluators Mail Doesn't Exist /بريد أحد الموظفين الذين سيقومون بالتقييم غير موجود"))
                            Return
                        End If
                        If regex.Match(DGRow.Cells(3).Value).Success = False Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "One Of Evaluators Mail Not Correct /بريد أحد الموظفين الذين سيقومون بالتقييم ليس صحيح"))
                            Return
                        End If
                    Next
                End If
                SavePart()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsEvaluationCompaign.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEvaluationCompaign.ID & "&TableName=" & ClsEvaluationCompaign.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEvaluationCompaign.ID & "&TableName=" & ClsEvaluationCompaign.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEvaluationCompaign.Table
                ClsEvaluationCompaign.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsEvaluationCompaign.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEvaluationCompaign.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEvaluationCompaign.Find(" Code= '" & txtCode.Text & "'")
                If ClsEvaluationCompaign.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsEvaluationCompaign.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsEvaluationCompaign.CheckDiff(ClsEvaluationCompaign, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsEvaluationCompaign.FirstRecord()
                GetValues()
            Case "Previous"
                ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
                If Not ClsEvaluationCompaign.previousRecord() Then
                    ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
                If Not ClsEvaluationCompaign.NextRecord() Then
                    ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsEvaluationCompaign.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFilter.Click
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
            row.Hidden = False
            row.Cells(6).Value = True
            If ddltypeofcompagn.SelectedValue <> 0 Then
                If row.Cells(4).Value = ddltypeofcompagn.SelectedValue Then
                    row.Hidden = False
                    row.Cells(6).Value = True
                Else
                    row.Hidden = True
                    row.Cells(6).Value = False
                End If
            End If
        Next
        For Each row1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows
            row1.Hidden = False
            row1.Cells(6).Value = True
            If ddltypeofcompagn.SelectedValue <> 0 Then
                If row1.Cells(2).Value = ddltypeofcompagn.SelectedValue Then
                    row1.Hidden = False
                    row1.Cells(6).Value = True
                Else
                    row1.Hidden = True
                    row1.Cells(6).Value = False
                End If
            End If
        Next

        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
            If row.Hidden = False Then
                If row.Cells(4).Value <> 5 Then
                    If CheckEmployeeByFilter(row.Cells(1).Value.ToString()) = False Then
                        row.Hidden = True
                        row.Cells(6).Value = False
                    End If
                End If
            End If
        Next
        For Each row1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows
            If row1.Hidden = False Then
                If row1.Cells(2).Value <> 5 Then
                    If CheckEmployeeByFilter(row1.Cells(1).Value.ToString()) = False Then
                        row1.Hidden = True
                        row1.Cells(6).Value = False
                    End If
                End If
            End If
        Next
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
            If row.Hidden = False Then
                uwgApplicants.DisplayLayout.ActiveRow = row
                row.Selected = True
                HideDetailsRows(row.Cells.FromKey("Evaluator_ID").Value, row.Cells.FromKey("CompaignType").Value)
                Exit For
            End If
        Next
        ddlFromposition.SelectedValue = 0
        ddlfromdepartment.SelectedValue = 0
        ddltodepartment.SelectedValue = 0
        ddltoposition.SelectedValue = 0
        ddltypeofcompagn.SelectedValue = 0
        txtFromCode.Text = String.Empty
        txtToCode.Text = String.Empty
    End Sub
    Protected Sub txtReturned_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReturned.TextChanged
        If txtCode.Text = "" And ddlEvalType.SelectedValue = 0 And ddlCompaType.SelectedValue = 0 Then
            Return
        End If
        Dim arg As String() = txtReturned.Text.Split("|")
        Dim i As Integer = 0
        For i = 0 To arg.Length - 1
            Dim EvalID As String = DateTime.Now.ToString("HHmmss")
            If i = 0 Then
                Dim arg1 As String() = arg(i).ToString().Split(",")
                uwgApplicants.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, EvalID, arg1(0).ToString(), IIf(arg1(1).ToString() = "", "No Mail", arg1(1).ToString()), 5, ddlEvalType.SelectedValue, True}))
            Else
                Dim EvaluatedStr As String = GetEmployeeData(arg(i).ToString())
                Dim arg1 As String() = EvaluatedStr.Split("|")
                uwgInterviewer.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, EvalID, 5, arg1(1).ToString(), arg1(0).ToString(), arg1(2).ToString(), True}))
            End If
        Next i
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
            If row.Hidden = False Then
                uwgApplicants.DisplayLayout.ActiveRow = row
                row.Selected = True
                HideDetailsRows(row.Cells.FromKey("Evaluator_ID").Value, row.Cells.FromKey("CompaignType").Value)
                Exit For
            End If
        Next
    End Sub
    Protected Sub txtReturned1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReturned1.TextChanged
        If txtCode.Text = "" And ddlEvalType.SelectedValue = 0 And ddlCompaType.SelectedValue = 0 Then
            Return
        End If
        Dim arg As String() = txtReturned1.Text.Split("|")
        Dim i As Integer = 0
        For i = 0 To arg.Length - 1
            Dim EvalID As String = DateTime.Now.ToString("HHmmss")
            Dim EvaluatorID As String = arg(0).ToString()
            If i = 0 Then
                Dim EvaluatedStr As String = GetEmployeeData(arg(i).ToString())
                Dim arg1 As String() = EvaluatedStr.Split("|")
                uwgApplicants.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {EvaluatorID, EvalID, arg1(0).ToString(), IIf(arg1(2).ToString() = "", "No Mail", arg1(2).ToString()), 4, ddlEvalType.SelectedValue, True}))
            Else
                Dim EvaluatedStr As String = GetEmployeeData(arg(i).ToString())
                Dim arg1 As String() = EvaluatedStr.Split("|")
                uwgInterviewer.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {EvaluatorID, EvalID, 4, arg1(1).ToString(), arg1(0).ToString(), arg1(2).ToString(), True}))
            End If
        Next i
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
            If row.Hidden = False Then
                uwgApplicants.DisplayLayout.ActiveRow = row
                row.Selected = True
                HideDetailsRows(row.Cells.FromKey("Evaluator_ID").Value, row.Cells.FromKey("CompaignType").Value)
                Exit For
            End If
        Next
    End Sub
    Protected Sub ddlopenvacancy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompaType.SelectedIndexChanged
        If ddlCompaType.SelectedValue <> 0 Then
            Dim ClsEvaluationCompainTypes As New ClsEval_EvaluationCompainTypes(Page)
            If txtCode.Text = "" Then
                Return
            End If
            If ddlEvalType.SelectedValue = 0 Then
                ddlCompaType.SelectedValue = 0
                Return
            End If
            ClsEvaluationCompainTypes.Find("ID = " & ddlCompaType.SelectedValue)
            uwgApplicants.Rows.Clear()
            uwgInterviewer.Rows.Clear()
            If ClsEvaluationCompainTypes.DownLevel = True Then
                Dim Str As String = "select Distinct ManagerID from hrs_Employees where ManagerID <> 0 or ManagerID is not null and CancelDate is null and ExcludeDate is null"
                Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvaluationCompainTypes.ConnectionString, CommandType.Text, Str)
                For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
                    Dim EvaluatorStr As String = GetEmployeeData(DS.Tables(0).Rows(i)("ManagerID").ToString())
                    Dim EvaluatedStr As String = GetEmployeeDataByManager(DS.Tables(0).Rows(i)("ManagerID").ToString())
                    Dim Arg As String() = EvaluatorStr.Split("|")
                    If Arg.Length / 3 > 0 Then
                        uwgApplicants.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Arg(1).ToString(), Arg(0).ToString(), Arg(2).ToString(), 1, ddlEvalType.SelectedValue, True}))
                        Dim Arg1 As String() = EvaluatedStr.Split("|")
                        If Arg1.Length / 3 > 0 Then
                            For i1 As Integer = 1 To Arg1.Length / 3
                                uwgInterviewer.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Arg(1).ToString(), 1, Arg1((i1 * 3) - 2).ToString(), Arg1((i1 * 3) - 3).ToString(), Arg1((i1 * 3) - 1).ToString(), True}))
                            Next i1
                        End If
                    End If
                Next i
            End If
            If ClsEvaluationCompainTypes.UpLevel = True Then
                Dim Str As String = "select ID,ManagerID from hrs_Employees where ManagerID <> 0 or ManagerID is not null and CancelDate is null and ExcludeDate is null"
                Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvaluationCompainTypes.ConnectionString, CommandType.Text, Str)
                For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
                    Dim EvaluatorStr As String = GetEmployeeData(DS.Tables(0).Rows(i)("ID").ToString())
                    Dim EvaluatedStr As String = GetEmployeeData(DS.Tables(0).Rows(i)("ManagerID").ToString())
                    Dim Arg As String() = EvaluatorStr.Split("|")
                    If Arg.Length / 3 > 0 Then
                        uwgApplicants.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Arg(1).ToString(), Arg(0).ToString(), Arg(2).ToString(), 2, ddlEvalType.SelectedValue, True}))
                        Dim Arg1 As String() = EvaluatedStr.Split("|")
                        If Arg1.Length / 3 > 0 Then
                            For i1 As Integer = 1 To Arg1.Length / 3
                                uwgInterviewer.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Arg(1).ToString(), 2, Arg1((i1 * 3) - 2).ToString(), Arg1((i1 * 3) - 3).ToString(), Arg1((i1 * 3) - 1).ToString(), True}))
                            Next i1
                        End If
                    End If
                Next i
            End If
            If ClsEvaluationCompainTypes.SameLevel = True Then
                Dim Str As String = "select ID from hrs_Employees where CancelDate is null and ExcludeDate is null"
                Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvaluationCompainTypes.ConnectionString, CommandType.Text, Str)
                For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
                    Dim EvaluatorStr As String = GetEmployeeData(DS.Tables(0).Rows(i)("ID").ToString())
                    Dim EvaluatedStr As String = GetAllEmployeeData(DS.Tables(0).Rows(i)("ID").ToString())
                    Dim Arg As String() = EvaluatorStr.Split("|")
                    If Arg.Length / 3 > 0 Then
                        uwgApplicants.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Arg(1).ToString(), Arg(0).ToString(), Arg(2).ToString(), 3, ddlEvalType.SelectedValue, True}))
                        Dim Arg1 As String() = EvaluatedStr.Split("|")
                        If Arg1.Length / 3 > 0 Then
                            For i1 As Integer = 1 To Arg1.Length / 3
                                uwgInterviewer.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Arg(1).ToString(), 3, Arg1((i1 * 3) - 2).ToString(), Arg1((i1 * 3) - 3).ToString(), Arg1((i1 * 3) - 1).ToString(), True}))
                            Next i1
                        End If
                    End If
                Next i
            End If
            If ClsEvaluationCompainTypes.IsOutSide = True Then
                btnaddExternal.Enabled = True
            Else
                btnaddExternal.Enabled = False
            End If
            If ClsEvaluationCompainTypes.IsRondome = True Then
                AddRondom.Enabled = True
            Else
                AddRondom.Enabled = False
            End If
        Else
            uwgApplicants.Rows.Clear()
            uwgInterviewer.Rows.Clear()
        End If
        Try
            uwgApplicants.DisplayLayout.ActiveRow = uwgApplicants.Rows(0)
            uwgApplicants.Rows(0).Selected = True
            HideDetailsRows(uwgApplicants.Rows(0).Cells.FromKey("Evaluator_ID").Value, uwgApplicants.Rows(0).Cells.FromKey("CompaignType").Value)
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Private Functions"

    Private Function AssignValues() As Boolean
        Try
            With ClsEvaluationCompaign
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .CompaignType_ID = ddlCompaType.SelectedValue
                .DefaultEvalType = ddlEvalType.SelectedValue
                .CountType = ddlCalcuType.SelectedValue
                .SendEvaluator = True
                .SendEvaluated = True
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvaluationCompaign.ConnectionString)
        Try
            SetToolBarDefaults()
            With ClsEvaluationCompaign
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                ddlCompaType.SelectedValue = .CompaignType_ID
                ddlEvalType.SelectedValue = .DefaultEvalType
                ddlCalcuType.SelectedValue = .CountType
                uwgApplicants.Columns(2).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                Dim strgetApplicants As String = " select * from Eval_EvaluationCompaignDetail1 where EvalCompaign_ID = " & .ID
                Dim DTgetApplicants As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(.ConnectionString, CommandType.Text, strgetApplicants).Tables(0)
                uwgApplicants.DataSource = DTgetApplicants
                uwgApplicants.DataBind()

                uwgInterviewer.Columns(4).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                Dim strgetInterviewers As String = " select * from Eval_EvaluationCompaignDetail2 where EvalCompaign_ID = " & .ID
                Dim DTgetInterviewers As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(.ConnectionString, CommandType.Text, strgetInterviewers).Tables(0)
                uwgInterviewer.DataSource = DTgetInterviewers
                uwgInterviewer.DataBind()

                Try
                    uwgApplicants.DisplayLayout.ActiveRow = uwgApplicants.Rows(0)
                    uwgApplicants.Rows(0).Selected = True
                    HideDetailsRows(uwgApplicants.Rows(0).Cells.FromKey("Evaluator_ID").Value, uwgApplicants.Rows(0).Cells.FromKey("CompaignType").Value)
                Catch ex As Exception
                End Try

            End With
            If Not ClsEvaluationCompaign.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEvaluationCompaign.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsEvaluationCompaign.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsEvaluationCompaign.RegDate).Date
            End If
            If ClsEvaluationCompaign.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsEvaluationCompaign.CancelDate).Date
            End If
            If Not ClsEvaluationCompaign.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()


            If (ClsEvaluationCompaign.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsEvaluationCompaign.ConnectionString, ClsEvaluationCompaign.DataBaseUserRelatedID, ClsEvaluationCompaign.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEvaluationCompaign.ConnectionString, ClsEvaluationCompaign.DataBaseUserRelatedID, ClsEvaluationCompaign.GroupID, ClsEvaluationCompaign.Table, ClsEvaluationCompaign.ID)
            If Not ClsEvaluationCompaign.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsEvaluationCompaign.ID)
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsEvaluationCompaign = New ClsEval_EvaluationCompaign(Page)
        ClsEvaluationCompaignDetail1 = New ClsEval_EvaluationCompaignDetail1(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvaluationCompaign.ConnectionString)
        Try
            ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
            If ClsEvaluationCompaign.ID > 0 Then
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsEvaluationCompaign.Update("Code='" & txtCode.Text & "'")

                Dim str As String = "delete from Eval_EvaluationCompaignDetail1 where EvalCompaign_ID = " & ClsEvaluationCompaign.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvaluationCompaign.ConnectionString, Data.CommandType.Text, str)

                Dim str2 As String = "delete from Eval_Evaluation where ID in (select Evaluation from Eval_EvaluationCompaignDetail2 where EvalCompaign_ID = '" & ClsEvaluationCompaign.ID & "')"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvaluationCompaign.ConnectionString, Data.CommandType.Text, str2)

                Dim str1 As String = "delete from Eval_EvaluationCompaignDetail2 where EvalCompaign_ID = " & ClsEvaluationCompaign.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvaluationCompaign.ConnectionString, Data.CommandType.Text, str1)

                If (SaveDG(ClsEvaluationCompaign.ID)) Then
                    ClsEvaluationCompaign = New ClsEval_EvaluationCompaign(Page)
                    clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                    ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
                    clsMainOtherFields.CollectDataAndSave(value.Text, ClsEvaluationCompaign.Table, ClsEvaluationCompaign.ID)
                    value.Text = ""

                End If
            Else
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsEvaluationCompaign.IsOpen = True
                ClsEvaluationCompaign.Save()
                ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
                If ClsEvaluationCompaign.ID > 0 Then
                    If (SaveDG(ClsEvaluationCompaign.ID)) Then
                        ClsEvaluationCompaign = New ClsEval_EvaluationCompaign(Page)
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsEvaluationCompaign.Table, ClsEvaluationCompaign.ID)
                        value.Text = ""

                    End If
                End If
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEvaluationCompaign.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SaveDG(ByVal InterviewID As Integer) As Boolean
        Try
            Dim ClsEvaluation As New ClsEval_Evaluation(Page)
            Dim ClsCompanies As New Clssys_Companies(Page)
            ClsEvaluationCompaignDetail1 = New ClsEval_EvaluationCompaignDetail1(Page)
            ClsEvaluationCompaignDetail2 = New ClsEval_EvaluationCompaignDetail2(Page)
            ClsCompanies.Find("ID > 0")
            
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgApplicants.Rows
                If DGRow.Cells(6).Value = True Then
                    ClsEvaluationCompaignDetail1 = New ClsEval_EvaluationCompaignDetail1(Page)
                    ClsEvaluationCompaignDetail1.EvalCompaign_ID = InterviewID
                    ClsEvaluationCompaignDetail1.Evaluator_ID = DGRow.Cells(1).Value
                    ClsEvaluationCompaignDetail1.CompaignType = DGRow.Cells(4).Value
                    If ClsEvaluationCompaignDetail1.CompaignType <> 5 Then
                        ClsEvaluationCompaignDetail1.EngName = GetEmployeeData(ClsEvaluationCompaignDetail1.Evaluator_ID.ToString(), "Eng")
                        ClsEvaluationCompaignDetail1.ArbName = GetEmployeeData(ClsEvaluationCompaignDetail1.Evaluator_ID.ToString(), "Arb")
                    Else
                        ClsEvaluationCompaignDetail1.EngName = DGRow.Cells(2).Value
                        ClsEvaluationCompaignDetail1.ArbName = DGRow.Cells(2).Value
                    End If
                    ClsEvaluationCompaignDetail1.DefaultEvalType = DGRow.Cells(5).Value
                    ClsEvaluationCompaignDetail1.E_Mail = DGRow.Cells(3).Value
                    ClsEvaluationCompaignDetail1.Save()
                End If
            Next
            For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows
                If DGRow1.Cells(6).Value = True Then
                    ClsEvaluationCompaignDetail2 = New ClsEval_EvaluationCompaignDetail2(Page)
                    ClsEvaluationCompaignDetail2.EvalCompaign_ID = InterviewID
                    ClsEvaluationCompaignDetail2.Evaluator_ID = DGRow1.Cells(1).Value
                    ClsEvaluationCompaignDetail2.CompaignType = DGRow1.Cells(2).Value
                    ClsEvaluationCompaignDetail2.Evaluated_ID = DGRow1.Cells(3).Value
                    ClsEvaluationCompaignDetail2.EngName = GetEmployeeData(ClsEvaluationCompaignDetail2.Evaluated_ID.ToString(), "Eng")
                    ClsEvaluationCompaignDetail2.ArbName = GetEmployeeData(ClsEvaluationCompaignDetail2.Evaluated_ID.ToString(), "Arb")
                    ClsEvaluationCompaignDetail2.E_Mail = DGRow1.Cells(5).Value
                    ClsEvaluationCompaignDetail1 = New ClsEval_EvaluationCompaignDetail1(Page)
                    ClsEvaluationCompaignDetail1.Find("EvalCompaign_ID = " & InterviewID & " and Evaluator_ID = " & ClsEvaluationCompaignDetail2.Evaluator_ID & " and CompaignType = " & ClsEvaluationCompaignDetail2.CompaignType)
                    If ClsEvaluationCompaignDetail1.DataSet.Tables(0).Rows.Count > 0 Then
                        ClsEvaluation = New ClsEval_Evaluation(Page)
                        ClsEvaluation.EvalTypeID = ClsEvaluationCompaignDetail1.DefaultEvalType
                        ClsEvaluation.Dest_EmployeeID = ClsEvaluationCompaignDetail2.Evaluator_ID
                        ClsEvaluation.Target_EmployeeID = ClsEvaluationCompaignDetail2.Evaluated_ID
                        ClsEvaluation.GStartDate = DateTime.Now()
                        ClsEvaluation.HStartDate = ClsDataAcessLayer.GregToHijri(ClsDataAcessLayer.FormatGregString(ClsEvaluation.GStartDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy"), "dd/MM/yyyy")
                        ClsEvaluation.GEndDate = DateTime.Now().AddMonths(1)
                        ClsEvaluation.HEndDate = ClsDataAcessLayer.GregToHijri(ClsDataAcessLayer.FormatGregString(ClsEvaluation.GEndDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy"), "dd/MM/yyyy")
                        ClsEvaluation.RegComputerID = 3
                        ClsEvaluationCompaignDetail2.Evaluation = ClsEvaluation.Save()
                        ClsEvaluationCompaignDetail2.Save()
                    End If
                End If
            Next
            If ChkApplicantSend.Checked Then
                Dim SMTPHost As String = ConfigurationManager.AppSettings("SMTPHost").ToString()
                Dim SMTPPort As String = ConfigurationManager.AppSettings("SMTPPort").ToString()
                Dim SMTPUsername As String = ConfigurationManager.AppSettings("SMTPUsername").ToString()
                Dim SMTPPassword As String = ConfigurationManager.AppSettings("SMTPPassword").ToString()
                Dim SMTPFrom As String = ConfigurationManager.AppSettings("SMTPFrom").ToString()
                Dim ApplicationURL As String = ConfigurationManager.AppSettings("ApplicationURL").ToString()

                Dim smtp As New System.Net.Mail.SmtpClient()
                Dim message As New System.Net.Mail.MailMessage()
                Dim FromAddress As New System.Net.Mail.MailAddress(SMTPFrom, ClsCompanies.ArbName & " " & ClsCompanies.EngName)
                smtp.Host = SMTPHost
                smtp.Port = SMTPPort
                Dim cred As New System.Net.NetworkCredential(SMTPUsername, SMTPPassword)
                message.From = FromAddress

                ClsEvaluationCompaignDetail2.Find("EvalCompaign_ID = " & InterviewID)
                Dim DS As DataSet = ClsEvaluationCompaignDetail2.DataSet
                For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
                    ClsEvaluationCompaignDetail1 = New ClsEval_EvaluationCompaignDetail1(Page)
                    ClsEvaluationCompaignDetail2 = New ClsEval_EvaluationCompaignDetail2(Page)
                    ClsEvaluationCompaignDetail2.Find("ID=" & DS.Tables(0).Rows(i)("ID").ToString())
                    ClsEvaluationCompaignDetail1.Find("EvalCompaign_ID = " & InterviewID & " and Evaluator_ID = " & ClsEvaluationCompaignDetail2.Evaluator_ID & " and CompaignType = " & ClsEvaluationCompaignDetail2.CompaignType)
                    Try
                        message.To.Clear()
                        message.To.Add(ClsEvaluationCompaignDetail1.E_Mail)
                        message.Subject = "تقييم Evaluation"
                        message.IsBodyHtml = True
                        Dim EngHeader As String = "<H2 style=""background-color:white;"" align=center> This Mail Is From " & ClsCompanies.EngName & "</H2></br>" & _
                                                  "<H1 style=""background-color:white;""> We are honored to inform you that You Are Invited To the Evaluate</H1></br>"
                        Dim EngDetail As String = "<H1 style=""background-color:white;""> Of Mr/Mrs " & ClsEvaluationCompaignDetail2.EngName & " </H1></br>" & _
                                                  "<H1> <a href='" & ApplicationURL & "/Pages/Hr/frmEvalEvaluation.aspx?EvalID=" & ClsEvaluationCompaignDetail2.Evaluation & "'>The Evaluation Link URL</a></H1></br>"
                        Dim EngFinish As String = "<H2 style=""background-color:white;""> Regards" & "</H2> </br>"

                        Dim ArbHeader As String = "<H2 style=""background-color:white;"" align=center> هذا البريد الإلكترونى من " & ClsCompanies.ArbName & "</H2></br>" & _
                                                  "<H1 style=""background-color:white;""> يسعدنا ان نعلمكم بأنكم مدعوون للمشاركة فى تقييم </H1></br>"
                        Dim ArbDetail As String = "<H1 style=""background-color:white;"">السيد/السيدة " & ClsEvaluationCompaignDetail2.ArbName & "</H1></br>" & _
                                                  "<H1> <a href='" & ApplicationURL & "/Pages/Hr/frmEvalEvaluation.aspx?EvalID=" & ClsEvaluationCompaignDetail2.Evaluation & "'>رابط التقييم</a></H1></br>"
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
                Next
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
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
                    ClsEvaluationCompaign.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsEvaluationCompaign.Find("ID=" & intID)
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
            With ClsEvaluationCompaign
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
        ClsEvaluationCompaign = New ClsEval_EvaluationCompaign(Me)
        Try
            With ClsEvaluationCompaign
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
        ClsEvaluationCompaign = New ClsEval_EvaluationCompaign(Me)
        If IntId > 0 Then
            ClsEvaluationCompaign.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEvaluationCompaign = New ClsEval_EvaluationCompaign(Me)
        Try
            ClsEvaluationCompaign.Find("Code='" & txtCode.Text & "'")
            IntId = ClsEvaluationCompaign.ID
            txtEngName.Focus()
            If ClsEvaluationCompaign.ID > 0 Then
                GetValues()

                Dim ClsEvaluationCompainTypes As New ClsEval_EvaluationCompainTypes(Page)
                ClsEvaluationCompainTypes.Find("ID = " & ddlCompaType.SelectedValue)
                If ClsEvaluationCompainTypes.IsOutSide = True Then
                    btnaddExternal.Enabled = True
                Else
                    btnaddExternal.Enabled = False
                End If
                If ClsEvaluationCompainTypes.IsRondome = True Then
                    AddRondom.Enabled = True
                Else
                    AddRondom.Enabled = False
                End If

                StrMode = "E"
            Else
                If ClsEvaluationCompaign.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsEvaluationCompaign.ConnectionString, ClsEvaluationCompaign.DataBaseUserRelatedID, ClsEvaluationCompaign.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEvaluationCompaign.ConnectionString, ClsEvaluationCompaign.DataBaseUserRelatedID, ClsEvaluationCompaign.GroupID, ClsEvaluationCompaign.Table, IntId)
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
        ClsEvaluationCompaign.Clear()
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
        ddlCompaType.SelectedValue = 0
        ddlEvalType.SelectedValue = 0
        ddlCalcuType.SelectedValue = "Acu"
        uwgApplicants.Rows.Clear()
        uwgInterviewer.Rows.Clear()
        btnaddExternal.Enabled = False
        AddRondom.Enabled = False
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEvaluationCompaign = New ClsEval_EvaluationCompaign(Page)
        ClsEvaluationCompaign.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsEvaluationCompaign.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEvaluationCompaign.Table) = True Then
            Dim StrTablename As String
            ClsEvaluationCompaign = New ClsEval_EvaluationCompaign(Me)
            StrTablename = ClsEvaluationCompaign.Table
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

#Region "Others Functions"
    Public Function CheckEmployeeByFilter(ByVal EmployeeeID As String) As Boolean
        Dim ClsEvaluationCompainTypes As New ClsEval_EvaluationCompainTypes(Page)
        Dim Whr As String = "hrs_Employees.CancelDate Is Null And hrs_contracts.CancelDate Is Null And hrs_contracts.ID = (Select Top 1 Cont.ID  From hrs_Contracts Cont Where Cont.EmployeeID = hrs_Contracts.EmployeeID Order by IsNull(Cont.EndDate,30/12/2070) Desc) and hrs_Employees.ID = " & EmployeeeID
        If ddlfromdepartment.SelectedValue <> 0 And ddltodepartment.SelectedValue <> 0 Then
            Whr = Whr & " and hrs_Employees.DepartmentID between " & ddlfromdepartment.SelectedValue & " and " & ddltodepartment.SelectedValue
        End If
        If ddlFromposition.SelectedValue <> 0 And ddltoposition.SelectedValue <> 0 Then
            Whr = Whr & " and hrs_Contracts.PositionID between " & ddlFromposition.SelectedValue & " and " & ddltoposition.SelectedValue
        End If
        If txtFromCode.Text <> "" And txtToCode.Text <> "" Then
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ID1 As Integer = 0
            Dim ID2 As Integer = 0
            ClsEmployee.Find("Code = '" & txtFromCode.Text & "'")
            ID1 = ClsEmployee.ID
            ClsEmployee = New Clshrs_Employees(Page)
            ClsEmployee.Find("Code = '" & txtToCode.Text & "'")
            ID2 = ClsEmployee.ID
            If ID1 <> 0 And ID2 <> 0 Then
                Whr = Whr & " and hrs_Employees.ID between " & ID1 & " and " & ID2
            End If
        End If
        Dim sqlstr As String = "select hrs_Employees.ID As ID,hrs_contracts.ID As ContractID from hrs_Employees Inner Join hrs_contracts On hrs_contracts.EmployeeID = hrs_Employees.ID where " & Whr
        Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvaluationCompainTypes.ConnectionString, CommandType.Text, sqlstr)
        If DS.Tables(0).Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function
    Private Sub HideDetailsRows(ByVal intEvaluator As Integer, ByVal intCompaignType As Integer)
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgInterviewer.Rows()
            If row.Cells(1).Value = intEvaluator And row.Cells(2).Value = intCompaignType Then
                row.Hidden = False
            Else
                row.Hidden = True
            End If
        Next
    End Sub
    Public Function GetEmployeeData(ByVal EmployeeeID As String) As String
        Dim ClsEvaluationCompainTypes As New ClsEval_EvaluationCompainTypes(Page)
        Dim StrArray As String = ""
        Dim strEmpName As String = ""
        Dim strEngName As String = ""
        Dim strArbName As String = ""

        Dim sqlstr As String = "select ID,Code,EngName,ArbName,FamilyEngName,FamilyArbName,FatherEngName,FatherArbName,GrandEngName,GrandArbName,E_Mail from hrs_Employees where ID = '" & EmployeeeID & "'"
        Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvaluationCompainTypes.ConnectionString, CommandType.Text, sqlstr)
        If DS.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
                strEngName = DS.Tables(0).Rows(i)("EngName").ToString() & "," & DS.Tables(0).Rows(i)("FatherEngName").ToString() & "," & DS.Tables(0).Rows(i)("GrandEngName").ToString() & "," & DS.Tables(0).Rows(i)("FamilyEngName").ToString()
                strArbName = DS.Tables(0).Rows(i)("ArbName").ToString() & "," & DS.Tables(0).Rows(i)("FatherArbName").ToString() & "," & DS.Tables(0).Rows(i)("GrandArbName").ToString() & "," & DS.Tables(0).Rows(i)("FamilyArbName").ToString()
                If strArbName = ",,," Then
                    strArbName = strEngName
                End If
                If strEngName = ",,," Then
                    strEngName = strArbName
                End If
                If txtLang.Value = "Arb" Then
                    strEmpName = strArbName
                Else
                    strEmpName = strEngName
                End If
                If StrArray <> String.Empty Then
                    StrArray = StrArray & "|"
                End If
                StrArray = StrArray & strEmpName & "|" & DS.Tables(0).Rows(i)("ID").ToString() & "|" & IIf(DS.Tables(0).Rows(i)("E_Mail").ToString() = "", "No Mail", DS.Tables(0).Rows(i)("E_Mail").ToString())
            Next i
        End If
        Return StrArray
    End Function
    Public Function GetAllEmployeeData(ByVal EmployeeeID As String) As String
        Dim ClsEvaluationCompainTypes As New ClsEval_EvaluationCompainTypes(Page)
        Dim StrArray As String = ""
        Dim strEmpName As String = ""
        Dim strEngName As String = ""
        Dim strArbName As String = ""

        Dim sqlstr As String = "select ID,Code,EngName,ArbName,FamilyEngName,FamilyArbName,FatherEngName,FatherArbName,GrandEngName,GrandArbName,E_Mail from hrs_Employees where CancelDate is null and ExcludeDate is null and ID <> " & EmployeeeID
        Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvaluationCompainTypes.ConnectionString, CommandType.Text, sqlstr)
        If DS.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
                strEngName = DS.Tables(0).Rows(i)("EngName").ToString() & "," & DS.Tables(0).Rows(i)("FatherEngName").ToString() & "," & DS.Tables(0).Rows(i)("GrandEngName").ToString() & "," & DS.Tables(0).Rows(i)("FamilyEngName").ToString()
                strArbName = DS.Tables(0).Rows(i)("ArbName").ToString() & "," & DS.Tables(0).Rows(i)("FatherArbName").ToString() & "," & DS.Tables(0).Rows(i)("GrandArbName").ToString() & "," & DS.Tables(0).Rows(i)("FamilyArbName").ToString()
                If strArbName = ",,," Then
                    strArbName = strEngName
                End If
                If strEngName = ",,," Then
                    strEngName = strArbName
                End If
                If txtLang.Value = "Arb" Then
                    strEmpName = strArbName
                Else
                    strEmpName = strEngName
                End If
                If StrArray <> String.Empty Then
                    StrArray = StrArray & "|"
                End If
                StrArray = StrArray & strEmpName & "|" & DS.Tables(0).Rows(i)("ID").ToString() & "|" & IIf(DS.Tables(0).Rows(i)("E_Mail").ToString() = "", "No Mail", DS.Tables(0).Rows(i)("E_Mail").ToString())
            Next i
        End If
        Return StrArray
    End Function
    Public Function GetEmployeeDataByManager(ByVal ManagerID As String) As String
        Dim ClsEvaluationCompainTypes As New ClsEval_EvaluationCompainTypes(Page)
        Dim StrArray As String = ""
        Dim strEmpName As String = ""
        Dim strEngName As String = ""
        Dim strArbName As String = ""

        Dim sqlstr As String = "select ID,Code,EngName,ArbName,FamilyEngName,FamilyArbName,FatherEngName,FatherArbName,GrandEngName,GrandArbName,E_Mail from hrs_Employees where CancelDate is null and ExcludeDate is null and ManagerID = '" & ManagerID & "'"
        Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvaluationCompainTypes.ConnectionString, CommandType.Text, sqlstr)
        If DS.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
                strEngName = DS.Tables(0).Rows(i)("EngName").ToString() & "," & DS.Tables(0).Rows(i)("FatherEngName").ToString() & "," & DS.Tables(0).Rows(i)("GrandEngName").ToString() & "," & DS.Tables(0).Rows(i)("FamilyEngName").ToString()
                strArbName = DS.Tables(0).Rows(i)("ArbName").ToString() & "," & DS.Tables(0).Rows(i)("FatherArbName").ToString() & "," & DS.Tables(0).Rows(i)("GrandArbName").ToString() & "," & DS.Tables(0).Rows(i)("FamilyArbName").ToString()
                If strArbName = ",,," Then
                    strArbName = strEngName
                End If
                If strEngName = ",,," Then
                    strEngName = strArbName
                End If
                If txtLang.Value = "Arb" Then
                    strEmpName = strArbName
                Else
                    strEmpName = strEngName
                End If
                If StrArray <> String.Empty Then
                    StrArray = StrArray & "|"
                End If
                StrArray = StrArray & strEmpName & "|" & DS.Tables(0).Rows(i)("ID").ToString() & "|" & IIf(DS.Tables(0).Rows(i)("E_Mail").ToString() = "", "No Mail", DS.Tables(0).Rows(i)("E_Mail").ToString())
            Next i
        End If
        Return StrArray
    End Function
    Public Function GetEmployeeData(ByVal EmployeeeID As String, ByVal Lang As String) As String
        Dim ClsEvaluationCompainTypes As New ClsEval_EvaluationCompainTypes(Page)
        Dim strEmpName As String = ""
        Dim strEngName As String = ""
        Dim strArbName As String = ""

        Dim sqlstr As String = "select ID,Code,EngName,ArbName,FamilyEngName,FamilyArbName,FatherEngName,FatherArbName,GrandEngName,GrandArbName,E_Mail from hrs_Employees where ID = '" & EmployeeeID & "'"
        Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvaluationCompainTypes.ConnectionString, CommandType.Text, sqlstr)
        If DS.Tables(0).Rows.Count > 0 Then
            strEngName = DS.Tables(0).Rows(0)("EngName").ToString() & "," & DS.Tables(0).Rows(0)("FatherEngName").ToString() & "," & DS.Tables(0).Rows(0)("GrandEngName").ToString() & "," & DS.Tables(0).Rows(0)("FamilyEngName").ToString()
            strArbName = DS.Tables(0).Rows(0)("ArbName").ToString() & "," & DS.Tables(0).Rows(0)("FatherArbName").ToString() & "," & DS.Tables(0).Rows(0)("GrandArbName").ToString() & "," & DS.Tables(0).Rows(0)("FamilyArbName").ToString()
            If strArbName = ",,," Then
                strArbName = strEngName
            End If
            If strEngName = ",,," Then
                strEngName = strArbName
            End If
            If Lang = "Arb" Then
                strEmpName = strArbName
            Else
                strEmpName = strEngName
            End If
            Return strEmpName
        Else
            Return ""
        End If
    End Function
#End Region

    Protected Sub uwgApplicants_ActiveRowChange(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgApplicants.ActiveRowChange
        HideDetailsRows(e.Row.Cells.FromKey("Evaluator_ID").Value, e.Row.Cells.FromKey("CompaignType").Value)
    End Sub
End Class
