Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports Microsoft.VisualBasic
Imports System.Data

Partial Class frmProjectReserveforceTransfeer
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
                LinkButton_Transfeer.Visible = False
                Msg.Text = "هذا الموظف فى إجازة"
            Else
                LinkButton_Transfeer.Visible = True
                Msg.Text = ""
            End If

            Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            ClsProjects.GetDropDownList(ddlProject, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and convert(Datetime,'" & txtStartDate.Text & "') between StartDate and EndDate")
            ddlLocation.Items.Clear()
            DropDownList_Shift.Items.Clear()
            Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
            If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') order by ID DESC") Then
                Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID)
                Dim ClsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
                ClsAttendanceTableShifts.GetDropDownList(DropDownList_Shift, True, "ID in (select AttendanceTableShiftID from hrs_ProjectLocationShifts where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID = " & ddlLocation.SelectedValue & "))")
            End If
        End If
    End Sub
#End Region

    Protected Sub ddlProject_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProject.SelectedIndexChanged
        ddlLocation.Items.Clear()
        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') order by ID DESC") Then
            Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
            cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID)
            Dim ClsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
            ClsAttendanceTableShifts.GetDropDownList(DropDownList_Shift, True, "ID in (select AttendanceTableShiftID from hrs_ProjectLocationShifts where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID = " & ddlLocation.SelectedValue & "))")
        End If
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        Dim ClsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
        ClsAttendanceTableShifts.GetDropDownList(DropDownList_Shift, True, "ID in (select AttendanceTableShiftID from hrs_ProjectLocationShifts where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID = " & ddlLocation.SelectedValue & "))")
    End Sub

    Protected Sub txtStartDate_ValueChange(sender As Object, e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtStartDate.ValueChange
        ddlProject.Items.Clear()
        ddlLocation.Items.Clear()
        Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        ClsProjects.GetDropDownList(ddlProject, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and convert(Datetime,'" & txtStartDate.Text & "') between StartDate and EndDate")
        ddlLocation.Items.Clear()
        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') order by ID DESC") Then
            Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
            cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID)
            Dim ClsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
            ClsAttendanceTableShifts.GetDropDownList(DropDownList_Shift, True, "ID in (select AttendanceTableShiftID from hrs_ProjectLocationShifts where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID = " & ddlLocation.SelectedValue & "))")
        End If

        Dim clsEmployeevacations As New Clshrs_EmployeesVacations(Me)
        If (clsEmployeevacations.FindEmployeeVacations(" hrs_EmployeesVacations.EmployeeID=" & Request.QueryString.Item("ID") & " And Convert(smalldatetime,Convert(varchar,ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,'" & txtStartDate.Text & "' ,103))	And	(ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,'" & txtStartDate.Text & "',103)))")) Then
            LinkButton_Transfeer.Visible = False
            Msg.Text = "هذا الموظف فى إجازة"
        Else
            LinkButton_Transfeer.Visible = True
            Msg.Text = ""
        End If
    End Sub
    Protected Sub LinkButton_Transfeer_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Transfeer.Click
        Dim clsProjectReserverForces As New Clshrs_ProjectEmployeeReserve(Me)
        With clsProjectReserverForces
            .TrnsDate = .SetHigriDate(txtStartDate.Text)
            .RefNo = txtrefno.Text
            .ProjectID = ddlProject.SelectedValue
            .LocationID = ddlLocation.SelectedValue
            .EmployeeID = Request.QueryString.Item("ID")
            .Save()
        End With
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
    End Sub
End Class
