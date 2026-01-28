Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmRecApplyApplicant
    Inherits MainPage
    
#Region "Public Decleration"

    Private clsInterviews As ClsRec_Interviews
    Private clsInterviewsDetail1 As ClsRec_InterviewsDetail1
    Private clsInterviewsDetail2 As ClsRec_InterviewsDetail2
    Private clsApplicant As ClsRec_BioGraphies
    Private clsOpenVacancy As ClsRec_OpenVacancy

    Private ClsEmp As Clshrs_Employees
    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region

#Region "Protected Sub"
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            clsInterviews = New ClsRec_Interviews(Page)
            Dim clsOpenVacancy As New ClsRec_OpenVacancy(Page)
            Dim clsNationality As New Clssys_Nationality(Page)
            Dim clsEducationDegree As New ClsRec_EducationDegree(Page)
            Dim ClsFilter As New ClsRec_ApplicantsFilter(Page)
            If Not IsPostBack Then
                clsOpenVacancy.GetDropDownList(ddlVacancy, True, "IsOpen = 1 and ID in (select OpenVacancy_ID from Rec_Interviews where CancelDate is null)")
                clsNationality.GetDropDownList(ddlNationality, True)
                clsEducationDegree.GetDropDownList(ddlEDegree, True)
                ClsFilter.GetDropDownList(ddlFilter, True)
            End If
        End If
    End Sub
    Protected Sub btnGetVacationDefault_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnGetVacationDefault.Click
        Try
            uwgInterviewsDetail0.Rows.Clear()
            clsInterviews = New ClsRec_Interviews(Page)
            Dim ClsFilter As New ClsRec_ApplicantsFilter(Page)
            Dim SalaryPower As Integer = 1
            Dim ExperPower As Integer = 1
            Dim EvalPower As Integer = 1
            Dim FilterSum As Integer = 1
            Dim filterString As String = "AVGGrade DESC"
            If ddlFilter.SelectedValue <> 0 Then
                ClsFilter.Find("ID = " & ddlFilter.SelectedValue)
                If ClsFilter.DataSet.Tables(0).Rows.Count > 0 Then
                    SalaryPower = ClsFilter.DataSet.Tables(0).Rows(0)(6)
                    ExperPower = ClsFilter.DataSet.Tables(0).Rows(0)(5)
                    EvalPower = ClsFilter.DataSet.Tables(0).Rows(0)(7)
                    FilterSum = SalaryPower + ExperPower + EvalPower
                    If SalaryPower > ExperPower And SalaryPower > EvalPower Then
                        filterString = "EXSalaryFilter ASC"
                    ElseIf ExperPower > SalaryPower And ExperPower > EvalPower Then
                        filterString = "ExperiouncesFilter DESC"
                    ElseIf EvalPower > SalaryPower And EvalPower > ExperPower Then
                        filterString = "AVGGradeFilter DESC"
                    End If
                End If
            End If
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsInterviews.ConnectionString)
            Dim strSQL As String = " select Distinct A.Applicant_ID," & _
                                   " (B.ArbName + ' ' + B.FamilyArbName + ' ' + B.FatherArbName + ' ' + B.GrandArbName) AS ArbName ," & _
                                   " (B.EngName + ' ' + B.FamilyEngName + ' ' + B.FatherEngName + ' ' + B.GrandEngName) AS EngName ," & _
                                   " B.Nationality_ID,B.Sex,ISNULL(B.ExpectedSalary,0) AS EXSalary,(ISNULL(B.ExpectedSalary,0) * " & SalaryPower / FilterSum & ") AS EXSalaryFilter,C.OpenVacancy_ID,(select ISNULL(SUM(Garde*PowerPCT/100),0) from Rec_InterviewsDetail2 where Applicant_ID = A.Applicant_ID and InterView_ID in(select ID from Rec_Interviews where CancelDate is null and OpenVacancy_ID = C.OpenVacancy_ID)) As AVGGrade,((select ISNULL(SUM(Garde*PowerPCT/100),0) from Rec_InterviewsDetail2 where Applicant_ID = A.Applicant_ID and InterView_ID in(select ID from Rec_Interviews where CancelDate is null and OpenVacancy_ID = C.OpenVacancy_ID)) * " & EvalPower / FilterSum & " ) As AVGGradeFilter," & _
                                   " (select ISNULL(SUM(Years),0) from Rec_BioGraphiesDetail4 where BioGraphy_ID = A.Applicant_ID) As Experiounces,((select ISNULL(SUM(Years),0) from Rec_BioGraphiesDetail4 where BioGraphy_ID = A.Applicant_ID) * " & ExperPower / FilterSum & ") As ExperiouncesFilter," & _
                                   " (select top 1 ID from Rec_EducationDegree where ID in(select EDegree_ID from Rec_BioGraphiesDetail3 where BioGraphy_ID = A.Applicant_ID) order by [Rank] Desc) AS HighDegree" & _
                                   " from Rec_InterviewsDetail1 A" & _
                                   " left outer join Rec_BioGraphies B on A.Applicant_ID = B.ID" & _
                                   " left outer join Rec_Interviews C on A.InterView_ID = C.ID"
            Dim Filterstr As String = "OpenVacancy_ID =" & ddlVacancy.SelectedValue & " and Sex = '" & ddlGender.SelectedValue & "'"
            If ddlNationality.SelectedValue <> 0 Then
                Filterstr = Filterstr & " and Nationality_ID = " & ddlNationality.SelectedValue
            End If
            If ddlEDegree.SelectedValue <> 0 Then
                Filterstr = Filterstr & " and HighDegree = " & ddlEDegree.SelectedValue
            End If
            If txtYOExp.Value > 0 Then
                Filterstr = Filterstr & " and Experiounces >= " & txtYOExp.Value
            End If
            If txtLastSalaryFrom.Value > 0 And txtLastSalaryTo.Value > 0 Then
                Filterstr = Filterstr & " and EXSalary >= " & txtLastSalaryFrom.Value & " and EXSalary <= " & txtLastSalaryTo.Value
            End If
            Dim DT As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsInterviews.ConnectionString, Data.CommandType.Text, strSQL).Tables(0)

            Dim DR As Data.DataRow() = DT.Select(Filterstr, filterString)
            Dim i As Integer = 0
            For i = 0 To DR.Length
                uwgInterviewsDetail0.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {DR(i)(0).ToString(), DR(i)(Convert.ToInt32(ObjNavigationHandler.SetLanguage(Page, "2/1"))).ToString(), DR(i)(8).ToString(), DR(i)(10).ToString(), DR(i)(5).ToString(), False}))
            Next
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub ImageButton_SaveN_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_SaveN.Click
        clsInterviews = New ClsRec_Interviews(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsInterviews.ConnectionString)
        If ddlVacancy.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Select Open Vacancy /إختر فرصة وظيفية"))
            Return
        ElseIf uwgInterviewsDetail0.Rows.Count = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "No Applicants Founds /لا يوجد متقدمين"))
            Return
        End If
        Dim row As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
        For Each row In uwgInterviewsDetail0.Rows
            If row.Cells.FromKey("Apply").Value <> "False" Then
                Dim str As String = "update Rec_OpenVacancy set IsOpen = 0 where ID =" & ddlVacancy.SelectedValue
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsInterviews.ConnectionString, Data.CommandType.Text, str)

                Dim str1 As String = "update Rec_InterviewsDetail1 set IsSelected = 1 where InterView_ID in (select ID from Rec_Interviews where CancelDate is null and OpenVacancy_ID = " & ddlVacancy.SelectedValue & ") and Applicant_ID = " & row.Cells.FromKey("Applicant_ID").Value
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsInterviews.ConnectionString, Data.CommandType.Text, str1)

                uwgInterviewsDetail0.Rows.Clear()
                ddlEDegree.SelectedValue = 0
                ddlGender.SelectedValue = "M"
                ddlNationality.SelectedValue = 0
                ddlVacancy.SelectedValue = 0
                txtLastSalaryFrom.Value = 0
                txtLastSalaryTo.Value = 0
                txtYOExp.Value = 0
                Dim clsOpenVacancy As New ClsRec_OpenVacancy(Page)
                clsOpenVacancy.GetDropDownList(ddlVacancy, True, "IsOpen = 1 and ID in (select OpenVacancy_ID from Rec_Interviews where CancelDate is null)")
            End If
        Next
    End Sub
    Protected Sub ImageButton_Print_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Print.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub

#End Region

End Class
