Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports Microsoft.VisualBasic
Imports System.Data

Partial Class frmProjectEmployeeExtension
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)

        ClsEmployee.Find("ID = " & Request.QueryString.Item("ID"))
        lblProjectCode.Text = ClsEmployee.Code
        lblProjectName.Text = ClsEmployee.FullName

        If Not IsPostBack Then
            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
            txtStartDate.Value = DateTime.Now.ToString("ddMMyyyy")
            Dim clsEmployeevacations As New Clshrs_EmployeesVacations(Me)
            If (clsEmployeevacations.FindEmployeeVacations(" hrs_EmployeesVacations.EmployeeID=" & Request.QueryString.Item("ID") & " And Convert(smalldatetime,Convert(varchar,ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,'" & txtStartDate.Text & "' ,103))	And	(ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,'" & txtStartDate.Text & "',103)))")) Then
                LinkButton_Absent.Visible = False
                LinkButton_OT.Visible = False
                LinkButton_Placement.Visible = False
                LinkButton_Leave.Visible = False
                Msg.Text = "هذا الموظف فى إجازة"
            Else
                LinkButton_Absent.Visible = True
                LinkButton_OT.Visible = True
                LinkButton_Placement.Visible = True
                LinkButton_Leave.Visible = True
                Msg.Text = ""
            End If
            Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            ClsProjects.GetDropDownList(ddlProject, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and BranchID = " & ClsEmployee.BranchID & " and convert(Datetime,'" & txtStartDate.Text & "') between StartDate and EndDate")
            ddlLocation.Items.Clear()
            Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
            If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') order by ID DESC") Then
                Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID)
            End If
            GetData()
            Dim hrs_employee As New Clshrs_Employees(Me)
            hrs_employee.GetDropDownList(ddlAbsent, True, "ID in (select EmployeeID from Att_AttendTransactions where ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtStartDate.Text & "' and TimeIn is null and TimeOut is null and Status in (0,3))")
            hrs_employee.GetDropDownList(ddlLeav, True, "ID in (select EmployeeID from Att_AttendTransactions where ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtStartDate.Text & "' and TimeIn is not null and TimeOut is not null and Status = 2)")

            Dim clsProjPlacement As New Clshrs_ProjectPlacements(Me)
            clsProjPlacement.GetDropDownList(ddlPlacement, True, "ProjectID = " & ddlProject.SelectedValue)
            clsProjPlacement.GetDropDownList(ddlOT, True, "ProjectID = " & ddlProject.SelectedValue)
        End If
    End Sub
#End Region

    Protected Sub ddlProject_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProject.SelectedIndexChanged
        ddlLocation.Items.Clear()
        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') order by ID DESC") Then
            Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
            cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID)
        End If
        GetData()
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        GetData()
    End Sub

    Protected Sub txtStartDate_ValueChange(sender As Object, e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtStartDate.ValueChange
        ddlProject.Items.Clear()
        ddlLocation.Items.Clear()
        Dim ClsEmployee As New Clshrs_Employees(Page)
        ClsEmployee.Find("ID = " & Request.QueryString.Item("ID"))
        Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        ClsProjects.GetDropDownList(ddlProject, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and BranchID = " & ClsEmployee.BranchID & " and convert(Datetime,'" & txtStartDate.Text & "') between StartDate and EndDate")
        ddlLocation.Items.Clear()
        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') order by ID DESC") Then
            Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
            cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID)
        End If
        GetData()
        Dim clsEmployeevacations As New Clshrs_EmployeesVacations(Me)
        If (clsEmployeevacations.FindEmployeeVacations(" hrs_EmployeesVacations.EmployeeID=" & Request.QueryString.Item("ID") & " And Convert(smalldatetime,Convert(varchar,ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,'" & txtStartDate.Text & "' ,103))	And	(ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,'" & txtStartDate.Text & "',103)))")) Then
            LinkButton_Absent.Visible = False
            LinkButton_OT.Visible = False
            LinkButton_Placement.Visible = False
            LinkButton_Leave.Visible = False
            Msg.Text = "هذا الموظف فى إجازة"
        Else
            LinkButton_Absent.Visible = True
            LinkButton_OT.Visible = True
            LinkButton_Placement.Visible = True
            LinkButton_Leave.Visible = True
            Msg.Text = ""
        End If
    End Sub
    Private Function GetData() As Boolean
        Try
            Dim hrs_employee As New Clshrs_Employees(Me)
            hrs_employee.GetDropDownList(ddlAbsent, True, "ID not in (select REmployeeID from hrs_ProjectEmployeeOvertime where convert(nvarchar(10),TrnsDate,103) = '" & txtStartDate.Text & "' and Flag = 1) and ID in (select EmployeeID from Att_AttendTransactions where ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtStartDate.Text & "' and TimeIn is null and TimeOut is null and Status in (0,3))")
            hrs_employee.GetDropDownList(ddlLeav, True, "ID not in (select REmployeeID from hrs_ProjectEmployeeOvertime where convert(nvarchar(10),TrnsDate,103) = '" & txtStartDate.Text & "' and Flag = 2) and ID in (select EmployeeID from Att_AttendTransactions where ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtStartDate.Text & "' and TimeIn is not null and TimeOut is not null and Status = 2)")
            Dim clsProjPlacement As New Clshrs_ProjectPlacements(Me)
            clsProjPlacement.GetDropDownList(ddlPlacement, True, "ProjectID = " & ddlProject.SelectedValue)
            clsProjPlacement.GetDropDownList(ddlOT, True, "ProjectID = " & ddlProject.SelectedValue)

            Dim strPlacement As String = "0"
            Dim hrsProjectChanges As New Clshrs_ProjectChanges(Me)
            If hrsProjectChanges.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') order by ID desc") Then
                Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                If hrsProjectPlacements.Find("ProjectChangeID = " & hrsProjectChanges.ID) Then
                    Dim DT2 As DataTable = hrsProjectPlacements.DataSet.Tables(0)

                    For Each Dr2 As DataRow In DT2.Rows
                        Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                        If Not hrsProjectPlacementEmployees.Find("PlacementCode = '" & Dr2("PlacementCode") & "' and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= convert(Datetime,'" & txtStartDate.Text & "'))") Then
                            strPlacement = strPlacement & "," & Dr2("ID")
                        End If
                    Next
                End If
                clsProjPlacement.GetDropDownListByDate(ddlPlacement, True, txtStartDate.Text, "hrs_ProjectPlacements.ID not in (select PlacementID from hrs_ProjectEmployeeOvertime where convert(nvarchar(10),TrnsDate,103) = '" & txtStartDate.Text & "' and Flag = 3) and (select count(ID) from hrs_ProjectPlacementPlanning where PlacementID = hrs_ProjectPlacements.ID and DayID = " & RetDayNumber(txtStartDate.Text) & " and AttendanceTableShiftID is not null) > 0 and hrs_ProjectPlacements.ID in (select PlacementID from hrs_ProjectPlacementPlanning where isnull(ReferenceTo,0) in (0,1)) and hrs_ProjectPlacements.ID in (" & strPlacement & ") and hrs_ProjectPlacements.PlacementCode not in (select hrs_ProjectPlacementEmployees.PlacementCode from hrs_ProjectPlacementEmployees where hrs_ProjectPlacementEmployees.FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (hrs_ProjectPlacementEmployees.ToDate is null or hrs_ProjectPlacementEmployees.ToDate >= convert(Datetime,'" & txtStartDate.Text & "'))) and hrs_ProjectPlacements.LocationID = " & ddlLocation.SelectedValue)
                clsProjPlacement.GetDropDownListByDateForOT(ddlOT, True, txtStartDate.Text, "hrs_ProjectPlacements.ID not in (select PlacementID from hrs_ProjectEmployeeOvertime where convert(nvarchar(10),TrnsDate,103) = '" & txtStartDate.Text & "' and Flag = 4) and hrs_ProjectPlacements.ID in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and DayID = " & RetDayNumber(txtStartDate.Text) & ") and hrs_ProjectPlacements.ID in (select PlacementID from hrs_ProjectPlacementPlanning where isnull(ReferenceTo,0) in (0,1)) and hrs_ProjectPlacements.LocationID = " & ddlLocation.SelectedValue)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function RetDayNumber(ByVal TrnsDate As DateTime) As Integer
        Dim DayNumber As Integer = 0
        Dim Dayidx As Integer = TrnsDate.DayOfWeek
        If Dayidx = 0 Then
            DayNumber = 2
        ElseIf Dayidx = 1 Then
            DayNumber = 3
        ElseIf Dayidx = 2 Then
            DayNumber = 4
        ElseIf Dayidx = 3 Then
            DayNumber = 5
        ElseIf Dayidx = 4 Then
            DayNumber = 6
        ElseIf Dayidx = 5 Then
            DayNumber = 7
        ElseIf Dayidx = 6 Then
            DayNumber = 1
        End If
        Return DayNumber
    End Function
    Protected Sub LinkButton_Absent_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Absent.Click
        Dim clsProjectEmployeesOT As New Clshrs_ProjectEmployeeOvertime(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsProjectEmployeesOT.ConnectionString)
        If ddlAbsent.SelectedValue > 0 Then
            With clsProjectEmployeesOT
                .TrnsDate = .SetHigriDate(txtStartDate.Text)
                .RefNo = txtrefno.Text
                .ProjectID = ddlProject.SelectedValue
                .LocationID = ddlLocation.SelectedValue
                .EmployeeID = Request.QueryString.Item("ID")
                .Flag = "1"
                .REmployeeID = ddlAbsent.SelectedValue
                .Save()

                Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                cls_ProjectLocations.Find("ID = " & ddlLocation.SelectedValue)

                Dim clsCompanies As New Clssys_Companies(Page)
                Dim clsBranch As New Clssys_Branches(Page)
                Dim clsemployee As New Clshrs_Employees(Page)
                clsemployee.Find("ID = " & Request.QueryString.Item("ID"))
                clsCompanies.Find("ID = " & .MainCompanyID)
                clsBranch.Find("ID=" & clsemployee.BranchID)
                Dim fiscalperiood As New Clssys_FiscalYearsPeriods(Me)
                fiscalperiood.Find("sys_FiscalYearsPeriods.CancelDate Is null And convert(datetime,'" & .TrnsDate & "') >= sys_FiscalYearsPeriods.FromDate and convert(datetime,'" & .TrnsDate & "') <= sys_FiscalYearsPeriods.ToDate")
                Dim fperiodid As Int32 = fiscalperiood.ID
                If clsBranch.PrepareDay > 0 Then
                    If .TrnsDate >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsBranch.PrepareDay) Then
                        fperiodid = fperiodid + 1
                    End If
                Else
                    If clsCompanies.PrepareDay > 0 Then
                        If .TrnsDate >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsCompanies.PrepareDay) Then
                            fperiodid = fperiodid + 1
                        End If
                    End If
                End If
                Dim strcommand As String = "set dateformat dmy; insert into hrs_EmployeeExtraItems values ((select Code from hrs_Employees where ID = " & Request.QueryString.Item("ID") & "),'','18',(Select isnull(InternalExtensionValue,0) from hrs_ProjectSetting where ProjectChangeID = (select ProjectChangeID from  hrs_ProjectLocations where hrs_ProjectLocations.ID = " & ddlLocation.SelectedValue & "))," & fperiodid & "," & IIf(CheckBox_IsSalary.Checked, "1", "2") & ",'" & .TrnsDate.ToString("dd/MM/yyyy") & "',2,'" & txtrefno.Text & "',' " & ddlProject.SelectedValue & "','" & cls_ProjectLocations.LinkedCS & "')"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(.ConnectionString, Data.CommandType.Text, strcommand)
            End With
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "No Employees Selected/لا يوجد موظفين تم إختيارهم"))
        End If
    End Sub
    Protected Sub LinkButton_Leave_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Leave.Click
        Dim clsProjectEmployeesOT As New Clshrs_ProjectEmployeeOvertime(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsProjectEmployeesOT.ConnectionString)
        If ddlLeav.SelectedValue > 0 Then
            With clsProjectEmployeesOT
                .TrnsDate = .SetHigriDate(txtStartDate.Text)
                .RefNo = txtrefno.Text
                .ProjectID = ddlProject.SelectedValue
                .LocationID = ddlLocation.SelectedValue
                .EmployeeID = Request.QueryString.Item("ID")
                .Flag = "2"
                .REmployeeID = ddlLeav.SelectedValue
                .Save()

                Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                cls_ProjectLocations.Find("ID = " & ddlLocation.SelectedValue)

                Dim clsCompanies As New Clssys_Companies(Page)
                Dim clsBranch As New Clssys_Branches(Page)
                Dim clsemployee As New Clshrs_Employees(Page)
                clsemployee.Find("ID = " & Request.QueryString.Item("ID"))
                clsCompanies.Find("ID = " & .MainCompanyID)
                clsBranch.Find("ID=" & clsemployee.BranchID)
                Dim fiscalperiood As New Clssys_FiscalYearsPeriods(Me)
                fiscalperiood.Find("sys_FiscalYearsPeriods.CancelDate Is null And convert(datetime,'" & .TrnsDate & "') >= sys_FiscalYearsPeriods.FromDate and convert(datetime,'" & .TrnsDate & "') <= sys_FiscalYearsPeriods.ToDate")
                Dim fperiodid As Int32 = fiscalperiood.ID
                If clsBranch.PrepareDay > 0 Then
                    If .TrnsDate >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsBranch.PrepareDay) Then
                        fperiodid = fperiodid + 1
                    End If
                Else
                    If clsCompanies.PrepareDay > 0 Then
                        If .TrnsDate >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsCompanies.PrepareDay) Then
                            fperiodid = fperiodid + 1
                        End If
                    End If
                End If
                Dim strcommand As String = "set dateformat dmy; insert into hrs_EmployeeExtraItems values ((select Code from hrs_Employees where ID = " & Request.QueryString.Item("ID") & "),'','18',(Select isnull(InternalExtensionValue,0) from hrs_ProjectSetting where ProjectChangeID = (select ProjectChangeID from  hrs_ProjectLocations where hrs_ProjectLocations.ID = " & ddlLocation.SelectedValue & "))," & fperiodid & "," & IIf(CheckBox_IsSalary.Checked, "1", "2") & ",'" & .TrnsDate.ToString("dd/MM/yyyy") & "',2,'" & txtrefno.Text & "','" & ddlProject.SelectedValue & "','" & cls_ProjectLocations.LinkedCS & "')"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(.ConnectionString, Data.CommandType.Text, strcommand)
            End With
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "No Employees Selected/لا يوجد موظفين تم إختيارهم"))
        End If
    End Sub
    Protected Sub LinkButton_Placement_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Placement.Click
        Dim clsProjectEmployeesOT As New Clshrs_ProjectEmployeeOvertime(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsProjectEmployeesOT.ConnectionString)
        If ddlPlacement.SelectedValue > 0 Then
            With clsProjectEmployeesOT
                .TrnsDate = .SetHigriDate(txtStartDate.Text)
                .RefNo = txtrefno.Text
                .ProjectID = ddlProject.SelectedValue
                .LocationID = ddlLocation.SelectedValue
                .EmployeeID = Request.QueryString.Item("ID")
                .Flag = "3"
                .PlacementID = ddlPlacement.SelectedValue
                .Save()

                Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                cls_ProjectLocations.Find("ID = " & ddlLocation.SelectedValue)

                Dim clsCompanies As New Clssys_Companies(Page)
                Dim clsBranch As New Clssys_Branches(Page)
                Dim clsemployee As New Clshrs_Employees(Page)
                clsemployee.Find("ID = " & Request.QueryString.Item("ID"))
                clsCompanies.Find("ID = " & .MainCompanyID)
                clsBranch.Find("ID=" & clsemployee.BranchID)
                Dim fiscalperiood As New Clssys_FiscalYearsPeriods(Me)
                fiscalperiood.Find("sys_FiscalYearsPeriods.CancelDate Is null And convert(datetime,'" & .TrnsDate & "') >= sys_FiscalYearsPeriods.FromDate and convert(datetime,'" & .TrnsDate & "') <= sys_FiscalYearsPeriods.ToDate")
                Dim fperiodid As Int32 = fiscalperiood.ID
                If clsBranch.PrepareDay > 0 Then
                    If .TrnsDate >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsBranch.PrepareDay) Then
                        fperiodid = fperiodid + 1
                    End If
                Else
                    If clsCompanies.PrepareDay > 0 Then
                        If .TrnsDate >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsCompanies.PrepareDay) Then
                            fperiodid = fperiodid + 1
                        End If
                    End If
                End If
                Dim strcommand As String = "set dateformat dmy; insert into hrs_EmployeeExtraItems values ((select Code from hrs_Employees where ID = " & Request.QueryString.Item("ID") & "),'','18',(Select isnull(InternalExtensionValue,0) from hrs_ProjectSetting where ProjectChangeID = (select ProjectChangeID from  hrs_ProjectLocations where hrs_ProjectLocations.ID = " & ddlLocation.SelectedValue & "))," & fperiodid & "," & IIf(CheckBox_IsSalary.Checked, "1", "2") & ",'" & .TrnsDate.ToString("dd/MM/yyyy") & "',2,'" & txtrefno.Text & "','" & ddlProject.SelectedValue & "','" & cls_ProjectLocations.LinkedCS & "')"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(.ConnectionString, Data.CommandType.Text, strcommand)
            End With
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "No Employees Selected/لا يوجد موظفين تم إختيارهم"))
        End If
    End Sub
    Protected Sub LinkButton_OT_Click(sender As Object, e As System.EventArgs) Handles LinkButton_OT.Click
        Dim clsProjectEmployeesOT As New Clshrs_ProjectEmployeeOvertime(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsProjectEmployeesOT.ConnectionString)
        If ddlOT.SelectedValue > 0 Then
            With clsProjectEmployeesOT
                .TrnsDate = .SetHigriDate(txtStartDate.Text)
                .RefNo = txtrefno.Text
                .ProjectID = ddlProject.SelectedValue
                .LocationID = ddlLocation.SelectedValue
                .EmployeeID = Request.QueryString.Item("ID")
                .Flag = "4"
                .PlacementID = ddlOT.SelectedValue
                .Save()

                Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                cls_ProjectLocations.Find("ID = " & ddlLocation.SelectedValue)

                Dim clsCompanies As New Clssys_Companies(Page)
                Dim clsBranch As New Clssys_Branches(Page)
                Dim clsemployee As New Clshrs_Employees(Page)
                clsemployee.Find("ID = " & Request.QueryString.Item("ID"))
                clsCompanies.Find("ID = " & .MainCompanyID)
                clsBranch.Find("ID=" & clsemployee.BranchID)
                Dim fiscalperiood As New Clssys_FiscalYearsPeriods(Me)
                fiscalperiood.Find("sys_FiscalYearsPeriods.CancelDate Is null And convert(datetime,'" & .TrnsDate & "') >= sys_FiscalYearsPeriods.FromDate and convert(datetime,'" & .TrnsDate & "') <= sys_FiscalYearsPeriods.ToDate")
                Dim fperiodid As Int32 = fiscalperiood.ID
                If clsBranch.PrepareDay > 0 Then
                    If .TrnsDate >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsBranch.PrepareDay) Then
                        fperiodid = fperiodid + 1
                    End If
                Else
                    If clsCompanies.PrepareDay > 0 Then
                        If .TrnsDate >= New DateTime(fiscalperiood.FromDate.Year, fiscalperiood.FromDate.Month, clsCompanies.PrepareDay) Then
                            fperiodid = fperiodid + 1
                        End If
                    End If
                End If
                Dim strcommand As String = "set dateformat dmy; insert into hrs_EmployeeExtraItems values ((select Code from hrs_Employees where ID = " & Request.QueryString.Item("ID") & "),'','18',(Select isnull(InternalExtensionValue,0) from hrs_ProjectSetting where ProjectChangeID = (select ProjectChangeID from  hrs_ProjectLocations where hrs_ProjectLocations.ID = " & ddlLocation.SelectedValue & "))," & fperiodid & "," & IIf(CheckBox_IsSalary.Checked, "1", "2") & ",'" & .TrnsDate.ToString("dd/MM/yyyy") & "',2,'" & txtrefno.Text & "','" & ddlProject.SelectedValue & "','" & cls_ProjectLocations.LinkedCS & "')"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(.ConnectionString, Data.CommandType.Text, strcommand)
            End With
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "No Employees Selected/لا يوجد موظفين تم إختيارهم"))
        End If
    End Sub
End Class
