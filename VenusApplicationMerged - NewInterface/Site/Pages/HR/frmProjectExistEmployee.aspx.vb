Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjectExistEmployee
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim sysnationalities As New Clssys_Nationality(Me)
                sysnationalities.GetDropDownList(ddlnationality, True)
                GetData()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetData() As Boolean
        Try
            Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
            hrsProjectPlacements.Find("ID = " & Request.QueryString.Item("ID"))
            Dim hrsprojects As New Clshrs_Projects(Me, "hrs_Projects")
            hrsprojects.Find("ID = " & hrsProjectPlacements.ProjectID)
            lblProjectCode.Text = hrsProjectPlacements.PlacementCode
            lblProjectName.Text = Request.QueryString.Item("LastDate")
            Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
            hrsProjectLocationDetails.Find("ID = " & hrsProjectPlacements.LocationDetailID)

            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)

            Dim filter As String = " And dbo.fn_CheckEndOfServiceByPeriod(hrs_Employees.ID,(select top 1 ID from sys_FiscalYearsPeriods where ToDate >= convert(Datetime,'" & IIf(CDate(Request.QueryString.Item("LastDate")) > DateTime.Now, Request.QueryString.Item("LastDate"), DateTime.Now.ToString("dd/MM/yyyy")) & "') order by ToDate DESC)) > 0 "
            If ddlnationality.SelectedValue > 0 Then
                filter = filter & " and hrs_Employees.NationalityID = " & ddlnationality.SelectedValue
            End If
            If CheckBox_RelatedPosition.Checked Then
                filter = filter & " and Contracts.PositionID = " & hrsProjectLocationDetails.PositionID
            End If
            If CheckBox_IsOld.Checked Then
                Dim hrsProjectChanges As New Clshrs_ProjectChanges(Me)
                If hrsProjectChanges.Find("RegComputerID = 1 and ProjectID = " & hrsProjectPlacements.ProjectID & " and ID < " & hrsProjectPlacements.ProjectChangeID & " order by ID desc") Then
                    filter = filter & " and hrs_Employees.ID in (select EmployeeID from hrs_ProjectPlacementEmployees where PlacementCode in (select PlacementCode from hrs_ProjectPlacements where ProjectChangeID = " & hrsProjectChanges.ID & "))"
                Else
                    filter = filter & " and hrs_Employees.ID = 0"
                End If
            End If
            uwgLocationPositions.Rows.Clear()
            Dim strcommand As String = "set dateformat dmy; select hrs_Employees.ID,hrs_Employees.Code,dbo.fn_GetEmpName(Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS EmployeeName,Round([dbo].[fn_TotalPackage](Contracts.ID,(Select Top 1 ID From sys_FiscalYearsPeriods Where GETDATE() Between DATEADD(Day,-1,FromDate) And Dateadd(Day,1,ToDate))),2) As Salary,(select ArbName from hrs_Positions where ID = Contracts.PositionID) AS Position" & _
                                       " from hrs_Employees Inner Join  (Select ID,EmployeeID,PositionID From hrs_Contracts Where CancelDate Is Null And (EndDate is null or EndDate > GETDATE())) Contracts On hrs_Employees.ID = Contracts.EmployeeID" & _
                                       " where " & IIf(txtCode.Text <> "", "hrs_Employees.Code like '" & txtCode.Text & "%' and ", "") & "  hrs_Employees.BranchID = " & hrsprojects.BranchID & " and isnull(Round([dbo].[fn_TotalPackageProject](Contracts.ID,(Select Top 1 ID From sys_FiscalYearsPeriods Where GETDATE() Between DATEADD(Day,-1,FromDate) And Dateadd(Day,1,ToDate))),2),0) > 0 and Round([dbo].[fn_TotalPackageProject](Contracts.ID,(Select Top 1 ID From sys_FiscalYearsPeriods Where GETDATE() Between DATEADD(Day,-1,FromDate) And Dateadd(Day,1,ToDate))),2) <= " & hrsProjectLocationDetails.InternalAmt & " and hrs_Employees.IsSpecialForce = 0 and hrs_Employees.IsProjectRelated = 1 and hrs_Employees.ID not in (select EmployeeID from hrs_ProjectPlacementEmployees where (ToDate is null or ToDate > convert(Datetime,'" & IIf(CDate(Request.QueryString.Item("LastDate")) > DateTime.Now, Request.QueryString.Item("LastDate"), DateTime.Now.ToString("dd/MM/yyyy")) & "')) and PlacementCode in (select PlacementCode from hrs_ProjectPlacements)) and hrs_Employees.ID not in (select EmployeeID from hrs_EmployeesVacations where VacationTypeID = (select top 1 ID from hrs_VacationsTypes where IsAnnual = 1) and (ActualEndDate is null or ActualEndDate > convert(Datetime,'" & IIf(CDate(Request.QueryString.Item("LastDate")) > DateTime.Now, Request.QueryString.Item("LastDate"), DateTime.Now.ToString("dd/MM/yyyy")) & "')))"

            uwgLocationPositions.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectLocationDetails.ConnectionString, System.Data.CommandType.Text, strcommand & filter)
            uwgLocationPositions.DataBind()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlnationality.SelectedIndexChanged
        GetData()
    End Sub

    Protected Sub CheckBox_RelatedPosition_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBox_RelatedPosition.CheckedChanged
        GetData()
    End Sub

    Protected Sub uwgLocationPositions_ClickCellButton(sender As Object, e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgLocationPositions.ClickCellButton
        Dim StrID As String = Request.QueryString.Item("ID")
        Dim ClsProjectProjectPlacements As New Clshrs_ProjectPlacements(Me)
        ClsProjectProjectPlacements.Find("ID = " & StrID)

        Dim ClsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        ClsProjectLocationDetails.Find("ID = " & ClsProjectProjectPlacements.LocationDetailID)

        Dim ClsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
        ClsProjectPlacementEmployees.PlacementCode = ClsProjectProjectPlacements.PlacementCode
        ClsProjectPlacementEmployees.FromDate = IIf(CDate(Request.QueryString.Item("LastDate")) > DateTime.Now, Request.QueryString.Item("LastDate"), DateTime.Now.ToString("dd/MM/yyyy"))
        ClsProjectPlacementEmployees.EmployeeID = e.Cell.Row.Cells.FromKey("ID").Value
        ClsProjectPlacementEmployees.Save()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "window.close();", True)
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        GetData()
    End Sub

    Protected Sub CheckBox_IsOld_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBox_IsOld.CheckedChanged
        GetData()
    End Sub
End Class