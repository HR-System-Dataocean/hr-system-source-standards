Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports Microsoft.VisualBasic
Imports System.Data

Partial Class frmProjectEmployeeTransfeer
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
                uwgLocationPositions.Visible = False
                Msg.Text = "هذا الموظف فى إجازة"
            Else
                uwgLocationPositions.Visible = True
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
            uwgLocationPositions.Visible = False
            Msg.Text = "هذا الموظف فى إجازة"
        Else
            uwgLocationPositions.Visible = True
            Msg.Text = ""
        End If
    End Sub
    Private Function GetData() As Boolean
        Try
            Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
            If hrsProjectPlacementEmployees.Find("EmployeeID = " & Request.QueryString.Item("ID") & " and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >=  convert(Datetime,'" & txtStartDate.Text & "'))") Then
                If clsProjects.Find("ID in (select ProjectID from hrs_ProjectPlacements where PlacementCode = '" & hrsProjectPlacementEmployees.PlacementCode & "')") Then
                    LinkButton_Copy.Enabled = True
                    LinkButton_Copy.ToolTip = "المشروع الحالى للموظف " & clsProjects.ArbName & "  " & hrsProjectPlacementEmployees.PlacementCode

                    LinkButton_Copy0.Enabled = True
                    LinkButton_Copy0.ToolTip = "المشروع الحالى للموظف " & clsProjects.ArbName & "  " & hrsProjectPlacementEmployees.PlacementCode
                End If
            Else
                LinkButton_Copy.Enabled = False
                LinkButton_Copy.ToolTip = ""

                LinkButton_Copy0.Enabled = False
                LinkButton_Copy0.ToolTip = ""
            End If

            Dim strPlacement As String = "0"
            Dim hrsProjectChanges As New Clshrs_ProjectChanges(Me)
            If hrsProjectChanges.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') order by ID desc") Then
                Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                If hrsProjectPlacements.Find("ProjectChangeID = " & hrsProjectChanges.ID) Then
                    Dim DT2 As DataTable = hrsProjectPlacements.DataSet.Tables(0)

                    For Each Dr2 As DataRow In DT2.Rows
                        hrsProjectPlacementEmployees = New Clshrs_ProjectPlacementEmployees(Me)
                        If Not hrsProjectPlacementEmployees.Find("PlacementCode = '" & Dr2("PlacementCode") & "' and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= convert(Datetime,'" & txtStartDate.Text & "'))") Then
                            strPlacement = strPlacement & "," & Dr2("ID")
                        End If
                    Next
                End If
            End If

            uwgLocationPositions.Rows.Clear()
            Dim strcommand As String = "set dateformat dmy; select A.ID,A.PlacementCode,A.LocationDetailID,(select ArbName from hrs_Positions where ID = B.PositionID) AS Position,B.InternalAmt,B.WeekDays,CONVERT(nvarchar(10), isnull((select top 1 ToDate from hrs_ProjectPlacementEmployees where PlacementCode = A.PlacementCode order by ToDate Desc),(select FromDate from hrs_ProjectChanges where ID = A.ProjectChangeID)),103) as LastDate," & _
                                       "case when (select COUNT(distinct AttendanceTableShiftID) from  hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID group by PlacementID) > 1 then '' ELSE (select top 1 TimeIn from hrs_AttendanceTableShifts where ID in (select AttendanceTableShiftID from hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID)) end AS TimeIn," & _
                                       "case when (select COUNT(distinct AttendanceTableShiftID) from  hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID group by PlacementID) > 1 then '' ELSE (select top 1 TimeOut from hrs_AttendanceTableShifts where ID in (select AttendanceTableShiftID from hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID)) end AS TimeOut " & _
                                       " from hrs_ProjectPlacements A inner join hrs_ProjectLocationDetails B on A.LocationDetailID = B.ID" & _
                                       " where Round([dbo].[fn_TotalPackageProject]((Select top 1 ID From hrs_Contracts Where EmployeeID = " & Request.QueryString.Item("ID") & " and CancelDate Is Null And (EndDate is null or EndDate > GETDATE())),(Select Top 1 ID From sys_FiscalYearsPeriods Where convert(Datetime,'" & txtStartDate.Text & "') Between DATEADD(Day,-1,FromDate) And Dateadd(Day,1,ToDate))),2) <= B.InternalAmt and A.ID in (select PlacementID from hrs_ProjectPlacementPlanning where isnull(ReferenceTo,0) in (0,1)) and A.ID in (" & strPlacement & ") and A.PlacementCode not in (select PlacementCode from hrs_ProjectPlacementEmployees where FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= convert(Datetime,'" & txtStartDate.Text & "'))) and A.LocationID = '" & ddlLocation.SelectedValue & "' order by A.ID"
            uwgLocationPositions.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsProjects.ConnectionString, System.Data.CommandType.Text, strcommand)
            uwgLocationPositions.DataBind()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub uwgLocationPositions_DblClick(sender As Object, e As Infragistics.WebUI.UltraWebGrid.ClickEventArgs) Handles uwgLocationPositions.DblClick
        HiddenField_SPlacement.Value = e.Row.Cells.FromKey("PlacementCode").Value
        Dim ClsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
        Dim Msg As String = ""
        If ClsProjectPlacementEmployees.Find("FromDate > convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= FromDate) and EmployeeID = " & Request.QueryString.Item("ID") & " order by FromDate ASC") Then
            Msg = "هذا الموظف مرتبط بإسناد فى موقع وظيفى بتاريخ لاحق "
        End If
        If ClsProjectPlacementEmployees.Find("FromDate > convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= FromDate) and PlacementCode = '" & e.Row.Cells.FromKey("PlacementCode").Value & "' order by FromDate ASC") Then
            If Msg <> "" Then
                Msg = Msg & " - "
            End If
            Msg = Msg & "هذا الموقع الوظيفى مرتبط بموظف بتاريخ لاحق "
        End If
        If Msg <> "" Then
            Msg = Msg & " - فهل ترغب فى الإستمرار ؟"
            Dim strconfirm As String = "<script>if(window.confirm('" & Msg & "')){document.getElementById(""Button_Confirm"").click();}</script>"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Confirm", strconfirm, False)
            Exit Sub
        End If
        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        Dim str As String = "set Dateformat DMY; update hrs_ProjectPlacementEmployees set ToDate = DATEADD(dAY,-1,convert(Datetime,'" & txtStartDate.Text & "')) where EmployeeID = " & Request.QueryString.Item("ID") & " and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= convert(Datetime,'" & txtStartDate.Text & "'))"
        str = str & " delete from Att_AttendTransactions where EmployeeID = " & Request.QueryString.Item("ID") & " and TrnsDatetime >= convert(Datetime,'" & txtStartDate.Text & "')"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsProjects.ConnectionString, CommandType.Text, str)

        ClsProjectPlacementEmployees = New Clshrs_ProjectPlacementEmployees(Me)
        ClsProjectPlacementEmployees.PlacementCode = e.Row.Cells.FromKey("PlacementCode").Value
        ClsProjectPlacementEmployees.FromDate = ClsProjectPlacementEmployees.SetHigriDate(txtStartDate.Text)
        ClsProjectPlacementEmployees.EmployeeID = Request.QueryString.Item("ID")
        ClsProjectPlacementEmployees.Save()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
    End Sub

    Protected Sub LinkButton_Copy_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Copy.Click
        Dim ClsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
        Dim NewToDate As DateTime = Nothing
        If ClsProjectPlacementEmployees.Find("FromDate > convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= FromDate) and EmployeeID = " & Request.QueryString.Item("ID") & " order by FromDate ASC") Then
            NewToDate = ClsProjectPlacementEmployees.FromDate.AddDays(-1)
        End If
        If NewToDate = Nothing Then
            Dim str As String = "set Dateformat DMY; update hrs_ProjectPlacementEmployees set ToDate = convert(Datetime,'" & txtStartDate.Text & "') where EmployeeID = " & Request.QueryString.Item("ID") & " and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >=  convert(Datetime,'" & txtStartDate.Text & "'))"
            str = str & " delete from Att_AttendTransactions where EmployeeID = " & Request.QueryString.Item("ID") & " and TrnsDatetime > convert(Datetime,'" & txtStartDate.Text & "')"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectPlacementEmployees.ConnectionString, CommandType.Text, str)
        Else
            Dim str As String = "set Dateformat DMY; update hrs_ProjectPlacementEmployees set ToDate = convert(Datetime,'" & txtStartDate.Text & "') where EmployeeID = " & Request.QueryString.Item("ID") & " and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >=  convert(Datetime,'" & txtStartDate.Text & "'))"
            str = str & " delete from Att_AttendTransactions where EmployeeID = " & Request.QueryString.Item("ID") & " and TrnsDatetime > convert(Datetime,'" & txtStartDate.Text & "') and TrnsDatetime <= convert(Datetime,'" & NewToDate.ToString("dd/MM/yyyy") & "')"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectPlacementEmployees.ConnectionString, CommandType.Text, str)
        End If
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Confirm", "CloseMe();", True)
    End Sub
    Protected Sub Button_Confirm_Click(sender As Object, e As System.EventArgs) Handles Button_Confirm.Click
        Dim ClsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
        Dim NewToDate As DateTime = Nothing
        If ClsProjectPlacementEmployees.Find("FromDate > convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= FromDate) and EmployeeID = " & Request.QueryString.Item("ID") & " order by FromDate ASC") Then
            NewToDate = ClsProjectPlacementEmployees.FromDate.AddDays(-1)
        End If
        If NewToDate = Nothing Then
            Dim str As String = "set Dateformat DMY; update hrs_ProjectPlacementEmployees set ToDate = DATEADD(dAY,-1,convert(Datetime,'" & txtStartDate.Text & "')) where EmployeeID = " & Request.QueryString.Item("ID") & " and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= convert(Datetime,'" & txtStartDate.Text & "'))"
            str = str & " delete from Att_AttendTransactions where EmployeeID = " & Request.QueryString.Item("ID") & " and TrnsDatetime >= convert(Datetime,'" & txtStartDate.Text & "')"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectPlacementEmployees.ConnectionString, CommandType.Text, str)
        Else
            Dim str As String = "set Dateformat DMY; update hrs_ProjectPlacementEmployees set ToDate = DATEADD(dAY,-1,convert(Datetime,'" & txtStartDate.Text & "')) where EmployeeID = " & Request.QueryString.Item("ID") & " and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= convert(Datetime,'" & txtStartDate.Text & "'))"
            str = str & " delete from Att_AttendTransactions where EmployeeID = " & Request.QueryString.Item("ID") & " and TrnsDatetime >= convert(Datetime,'" & txtStartDate.Text & "') and TrnsDatetime <= convert(Datetime,'" & NewToDate.ToString("dd/MM/yyyy") & "')"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectPlacementEmployees.ConnectionString, CommandType.Text, str)
        End If
        If ClsProjectPlacementEmployees.Find("FromDate > convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= FromDate) and PlacementCode = '" & HiddenField_SPlacement.Value & "' order by FromDate ASC") Then
            If NewToDate = Nothing Then
                NewToDate = ClsProjectPlacementEmployees.FromDate.AddDays(-1)
            Else
                NewToDate = IIf(NewToDate > ClsProjectPlacementEmployees.FromDate.AddDays(-1), ClsProjectPlacementEmployees.FromDate.AddDays(-1), NewToDate)
            End If
        End If

        ClsProjectPlacementEmployees = New Clshrs_ProjectPlacementEmployees(Me)
        ClsProjectPlacementEmployees.PlacementCode = HiddenField_SPlacement.Value
        ClsProjectPlacementEmployees.FromDate = ClsProjectPlacementEmployees.SetHigriDate(txtStartDate.Text)
        ClsProjectPlacementEmployees.ToDate = NewToDate
        ClsProjectPlacementEmployees.EmployeeID = Request.QueryString.Item("ID")
        ClsProjectPlacementEmployees.Save()
        HiddenField_SPlacement.Value = ""
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
    End Sub

    Protected Sub LinkButton_Copy0_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Copy0.Click
        Dim ClsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
        Dim NewToDate As DateTime = Nothing
        If ClsProjectPlacementEmployees.Find("FromDate > convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= FromDate) and EmployeeID = " & Request.QueryString.Item("ID") & " order by FromDate ASC") Then
            NewToDate = ClsProjectPlacementEmployees.FromDate.AddDays(-1)
        End If
        If NewToDate = Nothing Then
            Dim str As String = "set Dateformat DMY; delete from Att_AttendTransactions where EmployeeID = " & Request.QueryString.Item("ID") & " and TrnsDatetime >= (select Top 1 FromDate from hrs_ProjectPlacementEmployees where EmployeeID = " & Request.QueryString.Item("ID") & " and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= convert(Datetime,'" & txtStartDate.Text & "')) order by FromDate ASC)"
            str = str & " delete from hrs_ProjectPlacementEmployees where EmployeeID = " & Request.QueryString.Item("ID") & " and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= convert(Datetime,'" & txtStartDate.Text & "'))"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectPlacementEmployees.ConnectionString, CommandType.Text, str)
        Else
            Dim str As String = "set Dateformat DMY; delete from Att_AttendTransactions where EmployeeID = " & Request.QueryString.Item("ID") & " and TrnsDatetime >= (select Top 1 FromDate from hrs_ProjectPlacementEmployees where EmployeeID = " & Request.QueryString.Item("ID") & " and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= convert(Datetime,'" & txtStartDate.Text & "')) order by FromDate ASC) and TrnsDatetime <= convert(Datetime,'" & NewToDate.ToString("dd/MM/yyyy") & "')"
            str = str & " delete from hrs_ProjectPlacementEmployees where EmployeeID = " & Request.QueryString.Item("ID") & " and FromDate <= convert(Datetime,'" & txtStartDate.Text & "') and (ToDate is null or ToDate >= convert(Datetime,'" & txtStartDate.Text & "'))"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsProjectPlacementEmployees.ConnectionString, CommandType.Text, str)
        End If
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe();", True)
    End Sub
End Class
