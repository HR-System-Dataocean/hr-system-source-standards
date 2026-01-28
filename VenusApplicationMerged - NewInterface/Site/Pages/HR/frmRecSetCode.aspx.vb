Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmRecSetCode
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
 
        Try
            clsInterviews = New ClsRec_Interviews(Page)
            Dim clsOpenVacancy As New ClsRec_OpenVacancy(Page)
            If Not IsPostBack Then
                clsOpenVacancy.GetDropDownList(ddlVacancy, True, "IsOpen = 0 and ID in (select OpenVacancy_ID from Rec_Interviews where CancelDate is null)")
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, clsInterviews.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_Print.Command
        Select Case e.CommandArgument
            Case "Save"
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsInterviews.ConnectionString)
                Dim ClsEmployee As New Clshrs_Employees(Page)
                Dim ClsBioGraphies As New ClsRec_BioGraphies(Page)

                If ddlVacancy.SelectedIndex = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please select open vacancy /الرجاء اختيار فرصة وظيفية "))
                    Return
                End If

                Dim row As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
                For Each row In uwgInterviewsDetail.Rows
                    If row.Cells(2).Value <> Nothing Then
                        ClsEmployee.Find("Code = '" & row.Cells(2).Value & "'")
                        If ClsEmployee.DataSet.Tables(0).Rows.Count > 0 Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "The Code For Applicant " & row.Cells(1).Value & " IS Already Used/الكود المستخدم للمتقدم " & row.Cells(1).Value & " مستخدم بالفعل"))
                            btnGetVacationDefault_Click(Nothing, Nothing)
                            Return
                        End If
                        ClsBioGraphies.Find("ID = " & row.Cells(0).Value)
                        ClsEmployee.Code = row.Cells(2).Value
                        ClsEmployee.EngName = ClsBioGraphies.EngName
                        ClsEmployee.ArbName = ClsBioGraphies.ArbName
                        ClsEmployee.FamilyEngName = ClsBioGraphies.FamilyEngName
                        ClsEmployee.FamilyArbName = ClsBioGraphies.FamilyArbName
                        ClsEmployee.FatherEngName = ClsBioGraphies.FatherEngName
                        ClsEmployee.FatherArbName = ClsBioGraphies.FatherArbName
                        ClsEmployee.GrandEngName = ClsBioGraphies.GrandEngName
                        ClsEmployee.GrandArbName = ClsBioGraphies.GrandArbName
                        ClsEmployee.BirthDate = ClsBioGraphies.GBirthDate
                        ClsEmployee.ReligionID = ClsBioGraphies.ReligionID
                        ClsEmployee.MaritalStatusID = ClsBioGraphies.MaritalStatus
                        ClsEmployee.NationalityID = ClsBioGraphies.Nationality_ID
                        ClsEmployee.Phone = ClsBioGraphies.Phone
                        ClsEmployee.Mobile = ClsBioGraphies.Mobile

                        ClsEmployee.BranchID = 1

                        ClsEmployee.E_Mail = ClsBioGraphies.E_Mail
                        ClsEmployee.Save()
                        ClsBioGraphies.IsUsed = True
                        ClsBioGraphies.Update("ID = " & row.Cells(0).Value)
                    End If
                Next
                btnGetVacationDefault_Click(Nothing, Nothing)
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done /تم الحفظ"))

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
        End Select
    End Sub
    Protected Sub btnGetVacationDefault_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnGetVacationDefault.Click
        Try
            uwgInterviewsDetail.Rows.Clear()
            clsInterviews = New ClsRec_Interviews(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsInterviews.ConnectionString)
            Dim strSQL As String = " select Distinct A.Applicant_ID,A.InterView_ID," & _
                                   " (B.ArbName + ' ' + B.FamilyArbName + ' ' + B.FatherArbName + ' ' + B.GrandArbName) AS ArbName ," & _
                                   " (B.EngName + ' ' + B.FamilyEngName + ' ' + B.FatherEngName + ' ' + B.GrandEngName) AS EngName ," & _
                                   " C.OpenVacancy_ID from Rec_InterviewsDetail1 A" & _
                                   " left outer join Rec_BioGraphies B on A.Applicant_ID = B.ID" & _
                                   " left outer join Rec_Interviews C on A.InterView_ID = C.ID" & _
                                   " where A.IsSelected = 1 and B.IsUsed = 0"

            Dim Filterstr As String = ""
            If ddlVacancy.SelectedValue <> 0 Then
                Filterstr = Filterstr & "OpenVacancy_ID = " & ddlVacancy.SelectedValue
            End If
            Dim DT As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsInterviews.ConnectionString, Data.CommandType.Text, strSQL).Tables(0)

            Dim DR As Data.DataRow() = DT.Select(Filterstr)
            Dim i As Integer = 0
            For i = 0 To DR.Length
                uwgInterviewsDetail.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {DR(i)(0).ToString(), DR(i)(Convert.ToInt32(ObjNavigationHandler.SetLanguage(Page, "3/2"))).ToString(), "", Nothing, DR(i)(1).ToString(), DR(i)(4).ToString()}))
            Next
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub uwgInterviewsDetail_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgInterviewsDetail.ClickCellButton
        Dim ClsInterviewDetail1 As New ClsRec_InterviewsDetail1(Page)
        Dim clsOpenVacancy As New ClsRec_OpenVacancy(Page)
        Dim row As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        row = uwgInterviewsDetail.Rows(e.Cell.Row.Index)
        ClsInterviewDetail1.Find("Applicant_ID = '" & row.Cells.FromKey("Applicant_ID").Value & "' and InterView_ID = '" & row.Cells.FromKey("InterView_ID").Value & "'")
        ClsInterviewDetail1.IsSelected = False
        ClsInterviewDetail1.Update("Applicant_ID = '" & row.Cells.FromKey("Applicant_ID").Value & "' and InterView_ID = '" & row.Cells.FromKey("InterView_ID").Value & "'")

        Dim str As String = "update Rec_OpenVacancy set IsOpen = 1 where ID = " & row.Cells.FromKey("OpenVacancy_ID").Value
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsInterviewDetail1.ConnectionString, Data.CommandType.Text, str)

        clsOpenVacancy.GetDropDownList(ddlVacancy, True, "IsOpen = 0 and ID in (select OpenVacancy_ID from Rec_Interviews where CancelDate is null)")
        btnGetVacationDefault_Click(Nothing, Nothing)
    End Sub
#End Region


End Class
