Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmRecApplicantSearch
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
        clsInterviews = New ClsRec_Interviews(Page)
        Dim clsOpenVacancy As New ClsRec_OpenVacancy(Page)
        Dim clsNationality As New Clssys_Nationality(Page)
        Dim clsEducationDegree As New ClsRec_EducationDegree(Page)
        Dim ClsFilter As New ClsRec_ApplicantsFilter(Page)
        If Not IsPostBack Then
            clsNationality.GetDropDownList(ddlNationality, True)
            clsEducationDegree.GetDropDownList(ddlEDegree, True)
        End If

    End Sub
    Protected Sub btnGetVacationDefault_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnGetVacationDefault.Click
        Try
            uwgInterviewsDetail0.Rows.Clear()
            clsInterviews = New ClsRec_Interviews(Page)
            Dim filterString As String = "EXSalary ASC"
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsInterviews.ConnectionString)
            Dim strSQL As String = " select A.ID,A.Code," & _
                                   " (A.ArbName + ' ' + A.FamilyArbName + ' ' + A.FatherArbName + ' ' + A.GrandArbName) AS ArbName ," & _
                                   " (A.EngName + ' ' + A.FamilyEngName + ' ' + A.FatherEngName + ' ' + A.GrandEngName) AS EngName ," & _
                                   " A.Nationality_ID,A.Sex,ISNULL(A.ExpectedSalary,0) AS EXSalary," & _
                                   " (select ISNULL(SUM(Years),0) from Rec_BioGraphiesDetail4 where BioGraphy_ID = A.ID) As Experiounces," & _
                                   " (select top 1 ID from Rec_EducationDegree where ID in(select EDegree_ID from Rec_BioGraphiesDetail3 where BioGraphy_ID = A.ID) order by [Rank] Desc) AS HighDegree" & _
                                   " from Rec_BioGraphies A where " & Page.Request.QueryString(0).ToString()

            Dim Filterstr As String = "Sex = '" & ddlGender.SelectedValue & "'"
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
                uwgInterviewsDetail0.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {DR(i)(1).ToString(), DR(i)(Convert.ToInt32(ObjNavigationHandler.SetLanguage(Page, "3/2"))).ToString(), DR(i)(7).ToString(), DR(i)(6).ToString(), False}))
            Next
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub ImageButton_Print_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Print.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub
#End Region


End Class
