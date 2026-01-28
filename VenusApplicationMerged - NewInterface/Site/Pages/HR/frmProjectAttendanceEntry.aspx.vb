Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjectAttendanceEntry
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsUser As New Clssys_Users(Me)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)

        Dim ClsObjects As New Clssys_Objects(Page)
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        If ClsObjects.Find(" Code='" & ClsEmployee.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If

        If Not IsPostBack Then
            CProject.Value = 0
            CLocation.Value = 0
            CShift.Value = 0

            If ClsEmployee.Find("ID = " & ProfileCls.RetRefPeople()) Then
                lblDesc.Text = ClsEmployee.FullName
            Else
                ClsUser.Find("ID = " & ClsEmployee.DataBaseUserRelatedID)
                If ClsNavigationHandler.SetLanguage(Page, "0/1") = "0" Then
                    lblDesc.Text = ClsUser.EngName
                Else
                    lblDesc.Text = ClsUser.ArbName
                End If
            End If
            txtDate.Value = DateTime.Now.ToString("ddMMyyyy")

            Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            ClsProjects.GetDropDownList(ddlProject, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and convert(Datetime,'" & txtDate.Text & "') between StartDate and EndDate and ID in (select ProjectID from hrs_ProjectChanges where ID in (select ProjectChangeID from hrs_ProjectLocations where ID in (select ProjectLocationID from hrs_ProjectLocationUsers where UserID = " & ClsProjects.DataBaseUserRelatedID & ")))")

            DropDownList_Shift.Items.Clear()
            ddlLocation.Items.Clear()

            Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
            If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtDate.Text & "') order by ID DESC") Then
                Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID & " and ID in (select ProjectLocationID from hrs_ProjectLocationUsers where UserID = " & clsprojectchange.DataBaseUserRelatedID & ")")

                Dim ClsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
                ClsAttendanceTableShifts.GetDropDownList(DropDownList_Shift, True, "ID in (select AttendanceTableShiftID from hrs_ProjectLocationShifts where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID = " & ddlLocation.SelectedValue & "))")
            End If
        End If
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub
    Private Function Confirm() As Boolean
        Dim AttendanceTransactions As New ClsAtt_AttendTransactions(Page)
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
            If row.Cells.FromKey("Marked").Value = 1 Then
                AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                AttendanceTransactions.TrnsDatetime = CDate(txtDate.Text)
                AttendanceTransactions.EmployeeID = row.Cells.FromKey("ID").Text
                AttendanceTransactions.ProjectID = ddlProject.SelectedValue

                If row.Cells.FromKey("Attend").Value = 1 Then
                    Dim ClsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Page)

                    If Not ClsAttendanceTableShifts.Find("ID = '" & row.Cells.FromKey("ShiftID").Value & "'") Then
                        ClsAttendanceTableShifts.Find("ID = '" & DropDownList_Shift.SelectedValue & "'")
                    End If

                    AttendanceTransactions.TimeIn = ClsAttendanceTableShifts.TimeIn
                    AttendanceTransactions.TimeOut = ClsAttendanceTableShifts.TimeOut
                    AttendanceTransactions.Status = -1

                    Dim dttimefrom As DateTime = Convert.ToDateTime(Date.Now.ToString("dd/MM/yyyy") & " " & ClsAttendanceTableShifts.TimeIn)
                    Dim dttimeout As DateTime = Convert.ToDateTime(Date.Now.ToString("dd/MM/yyyy") & " " & ClsAttendanceTableShifts.TimeOut)
                    If dttimefrom > dttimeout Then
                        dttimeout = dttimeout.AddDays(1)
                    End If
                    AttendanceTransactions.TotalHours = (dttimeout - dttimefrom).TotalHours
                End If
                If row.Cells.FromKey("Absent").Value = 1 Then
                    AttendanceTransactions.Status = 0
                    AttendanceTransactions.TotalHours = 0
                End If
                If row.Cells.FromKey("Sick").Value = 1 Then
                    AttendanceTransactions.Status = 3
                End If
                If row.Cells.FromKey("DayOff").Value = 1 Then
                    AttendanceTransactions.Status = 1
                Else

                End If
                If row.Cells.FromKey("Leave").Value = 1 Then
                    AttendanceTransactions.Status = 2
                End If

                AttendanceTransactions.Overtime = row.Cells.FromKey("Overtime").Value
                AttendanceTransactions.TotalLate = row.Cells.FromKey("Delay").Value
                AttendanceTransactions.Src = 3
                AttendanceTransactions.RegDate = DateTime.Now
                AttendanceTransactions.RegUserID = AttendanceTransactions.DataBaseUserRelatedID
                AttendanceTransactions.RegComputerID = row.Cells.FromKey("RefID").Text
                Dim AttendanceTransactions1 As New ClsAtt_AttendTransactions(Page)

                If AttendanceTransactions1.Find("EmployeeID = " & row.Cells.FromKey("ID").Text & " and Src = 3 and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and ProjectID = " & ddlProject.SelectedValue) Then
                    AttendanceTransactions.Update("ID =" & AttendanceTransactions1.ID)
                Else
                    AttendanceTransactions.Save()
                End If
            Else
                Dim AttendanceTransactions1 As New ClsAtt_AttendTransactions(Page)
                If AttendanceTransactions1.Find("EmployeeID = " & row.Cells.FromKey("ID").Text & " and Src = 3 and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and ProjectID = " & ddlProject.SelectedValue) Then
                    AttendanceTransactions.Delete("ID =" & AttendanceTransactions1.ID)
                End If
            End If
        Next
    End Function
    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Prepare.Command, ImageButton_Prepare.Command
        Try
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Prepare"
                    Dim AttendanceTransactions As New ClsAtt_AttendTransactions(Page)
                    Dim objNav As New Venus.Shared.Web.NavigationHandler(AttendanceTransactions.ConnectionString)
                    Try
                        Dim dsAudit As New Data.DataSet
                        Dim str As String = "select * from hrs_ProjectLocationsAuditing where IsActive = 1 and ProjectID = '" & ddlProject.SelectedValue & "' and LocationID = '" & ddlLocation.SelectedValue & "' and TrnsDate = '" & Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") & "'"
                        dsAudit = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(AttendanceTransactions.ConnectionString, Data.CommandType.Text, str)
                        If dsAudit.Tables(0).Rows.Count > 0 Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Already Confirmed !/معتمد سابقا "))
                            Return
                        End If
                    Catch ex As Exception
                    End Try
                    Confirm()
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Update Done!/تم التحديث"))
                    GetData()
            End Select
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Private Function"

    Private Function GetData() As Boolean
        Try
            UwgSearchEmployees.Rows.Clear()
            Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
            If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtDate.Text & "') order by ID DESC") Then
                Dim ClsEmployee As New Clshrs_Employees(Page)
                Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
                Dim strCommand As String = "Set DateFormat DMY; select * from (Select hrs_Employees.ID, hrs_Employees.Code, dbo.fn_GetEmpName(hrs_Employees.Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS EmployeeName" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "'),0) AS Marked" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is null and TimeOut is null and Status = 0),0) AS Absent" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is not null and TimeOut is not null),0) AS Attend" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is null and TimeOut is null and Status = 3),0) AS Sick" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is not null and TimeOut is not null and Status = 2),0) AS Leave" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is null and TimeOut is null and Status = 1),0) AS DayOff" & _
                                           ",isnull((select TotalLate from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is not null and TimeOut is not null),0) AS Delay" & _
                                           ",isnull((select Overtime from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "'),0) AS Overtime,hrs_ProjectPlacements.ID AS RefID,(select AttendanceTableShiftID from hrs_ProjectPlacementPlanning where PlacementID = hrs_ProjectPlacements.ID and DayID = " & RetDayNumber(Convert.ToDateTime(txtDate.Text)) & ") AS ShiftID " & _
                                           "From hrs_Employees inner join hrs_ProjectPlacementEmployees on hrs_Employees.ID = hrs_ProjectPlacementEmployees.EmployeeID inner join hrs_ProjectPlacements on hrs_ProjectPlacements.PlacementCode = hrs_ProjectPlacementEmployees.PlacementCode " & _
                                           "where isnull(hrs_Employees.IsProjectRelated,0) = 1 and isnull(hrs_Employees.RegComputerID,0) = 0 and hrs_ProjectPlacements.ProjectID = " & ddlProject.SelectedValue & " and hrs_ProjectPlacements.LocationID = " & ddlLocation.SelectedValue & " and hrs_ProjectPlacements.ID in (select PlacementID from hrs_ProjectPlacementPlanning where " & IIf(DropDownList_Shift.SelectedValue = 0, "", "AttendanceTableShiftID = " & DropDownList_Shift.SelectedValue & " and ") & "ReferenceTo is null) and hrs_ProjectPlacements.ProjectChangeID = " & clsprojectchange.ID & " and CONVERT(Datetime,'" & txtDate.Text & "') between hrs_ProjectPlacementEmployees.FromDate and ISNULL(hrs_ProjectPlacementEmployees.ToDate,convert(Datetime,'01/01/2050'))" & _
                                           " UNION ALL " & _
                                           "Select hrs_Employees.ID, hrs_Employees.Code, dbo.fn_GetEmpName(hrs_Employees.Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS EmployeeName" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "'),0) AS Marked" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is null and TimeOut is null and Status = 0),0) AS Absent" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is not null and TimeOut is not null),0) AS Attend" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is null and TimeOut is null and Status = 3),0) AS Sick" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is not null and TimeOut is not null and Status = 2),0) AS Leave" & _
                                           ",isnull((select count(ID) from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is null and TimeOut is null and Status = 1),0) AS DayOff" & _
                                           ",isnull((select TotalLate from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "' and TimeIn is not null and TimeOut is not null),0) AS Delay" & _
                                           ",isnull((select Overtime from Att_AttendTransactions where EmployeeID = hrs_Employees.ID and (select Mstr.PlacementCode from hrs_ProjectPlacements Mstr where Mstr.ID = Att_AttendTransactions.RegComputerID) = hrs_ProjectPlacements.PlacementCode and ProjectID = " & ddlProject.SelectedValue & " and convert(nvarchar(10),TrnsDatetime,103) = '" & txtDate.Text & "'),0) AS Overtime,hrs_ProjectPlacements.ID AS RefID,(select AttendanceTableShiftID from hrs_ProjectPlacementPlanning where PlacementID = hrs_ProjectPlacements.ID and DayID = " & RetDayNumber(Convert.ToDateTime(txtDate.Text)) & ") AS ShiftID " & _
                                           "From hrs_Employees inner join hrs_ProjectPlacementEmployees on hrs_Employees.ID = hrs_ProjectPlacementEmployees.EmployeeID inner join hrs_ProjectPlacements on hrs_ProjectPlacements.PlacementCode = hrs_ProjectPlacementEmployees.PlacementCode " & _
                                           "where isnull(hrs_Employees.IsProjectRelated,0) = 1 and isnull(hrs_Employees.RegComputerID,0) = 0 and hrs_ProjectPlacements.ProjectID = " & ddlProject.SelectedValue & " and hrs_ProjectPlacements.LocationID = " & ddlLocation.SelectedValue & " and hrs_ProjectPlacements.ID in (select PlacementID from hrs_ProjectPlacementPlanning where ReferenceTo = 1 and ((select COUNT(ID) from hrs_ProjectPlacementPlanning where PlacementID = hrs_ProjectPlacements.ID and DayID = " & RetDayNumber(Convert.ToDateTime(txtDate.Text)) & ") = 0 or " & IIf(DropDownList_Shift.SelectedValue = 0, "", "AttendanceTableShiftID = " & DropDownList_Shift.SelectedValue & " and ") & " DayID = " & RetDayNumber(Convert.ToDateTime(txtDate.Text)) & ")) and hrs_ProjectPlacements.ProjectChangeID = " & clsprojectchange.ID & " and CONVERT(Datetime,'" & txtDate.Text & "') between hrs_ProjectPlacementEmployees.FromDate and ISNULL(hrs_ProjectPlacementEmployees.ToDate,convert(Datetime,'01/01/2050'))) A " & IIf(txtCode.Text <> "", " where A.Code like '%" & txtCode.Text & "%'", "") & " order by A.Code"
                Dim dsEmployee As New Data.DataSet
                dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, strCommand)
                UwgSearchEmployees.DataSource = dsEmployee.Tables(0)
                UwgSearchEmployees.DataBind()

                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                    If DGRow.Cells.FromKey("Marked").Value = 1 Then
                        DGRow.Style.BackColor = Drawing.Color.LightBlue
                    End If

                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                    If Not hrsProjectPlacementPlanning.Find("PlacementID = " & DGRow.Cells.FromKey("RefID").Text & " and AttendanceTableShiftID = '" & DGRow.Cells.FromKey("ShiftID").Value & "' and DayID = " & RetDayNumber(Convert.ToDateTime(txtDate.Text))) Then
                        DGRow.Cells.FromKey("DayOff").Value = 1
                        If DropDownList_Shift.SelectedValue = 0 Then
                            DGRow.Cells.FromKey("Attend").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                            DGRow.Cells.FromKey("Absent").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                            DGRow.Cells.FromKey("Sick").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                            DGRow.Cells.FromKey("Leave").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                            DGRow.Cells.FromKey("DayOff").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                            DGRow.Cells.FromKey("Delay").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                            DGRow.Cells.FromKey("Overtime").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                            DGRow.Style.BackColor = Drawing.Color.OrangeRed
                        Else
                            DGRow.Cells.FromKey("Attend").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes
                            DGRow.Cells.FromKey("Absent").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                            DGRow.Cells.FromKey("Sick").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                            DGRow.Cells.FromKey("Leave").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes
                            DGRow.Cells.FromKey("DayOff").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                            DGRow.Cells.FromKey("Delay").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes
                            DGRow.Cells.FromKey("Overtime").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes
                        End If
                    End If

                    Dim AttAttendancePreparationProjects As New ClsAtt_AttendancePreparationProjects(Me)
                    If AttAttendancePreparationProjects.Find("ProjectID = " & ddlProject.SelectedValue & " and TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & DGRow.Cells.FromKey("ID").Value & " and convert(nvarchar(10),GAttendDate,103) = '" & txtDate.Text & "')") Then
                        DGRow.Cells.FromKey("Attend").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                        DGRow.Cells.FromKey("Absent").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                        DGRow.Cells.FromKey("Sick").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                        DGRow.Cells.FromKey("Leave").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                        DGRow.Cells.FromKey("DayOff").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                        DGRow.Cells.FromKey("Delay").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                        DGRow.Cells.FromKey("Overtime").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                        DGRow.Cells.FromKey("Marked").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    End If
                Next
            End If
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

#End Region

    Protected Sub ddlProject_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProject.SelectedIndexChanged
        DropDownList_Shift.Items.Clear()
        ddlLocation.Items.Clear()

        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtDate.Text & "') order by ID DESC") Then
            Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
            cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID & " and ID in (select ProjectLocationID from hrs_ProjectLocationUsers where UserID = " & clsprojectchange.DataBaseUserRelatedID & ")")

            Dim ClsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
            ClsAttendanceTableShifts.GetDropDownList(DropDownList_Shift, True, "ID in (select AttendanceTableShiftID from hrs_ProjectLocationShifts where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID = " & ddlLocation.SelectedValue & "))")
        End If
        GetData()
        CProject.Value = ddlProject.SelectedValue
    End Sub
    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        Dim ClsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
        ClsAttendanceTableShifts.GetDropDownList(DropDownList_Shift, True, "ID in (select AttendanceTableShiftID from hrs_ProjectLocationShifts where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID = " & ddlLocation.SelectedValue & "))")
        GetData()
        CLocation.Value = ddlLocation.SelectedValue
    End Sub
    Protected Sub DropDownList_Shift_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList_Shift.SelectedIndexChanged
        GetData()
        CShift.Value = DropDownList_Shift.SelectedValue
    End Sub
    Protected Sub UwgSearchEmployees_UpdateCell(sender As Object, e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles UwgSearchEmployees.UpdateCell

        If e.Cell.Row.Cells.FromKey("Sick").Value = 1 Then
            e.Cell.Row.Cells.FromKey("Attend").Value = 0
            e.Cell.Row.Cells.FromKey("Absent").Value = 0
            e.Cell.Row.Cells.FromKey("Leave").Value = 0
            e.Cell.Row.Cells.FromKey("Delay").Value = 0
            e.Cell.Row.Cells.FromKey("Overtime").Value = 0
            e.Cell.Row.Cells.FromKey("Marked").Value = 1
        End If
        If e.Cell.Row.Cells.FromKey("Attend").Value = 1 Then
            e.Cell.Row.Cells.FromKey("Sick").Value = 0
            e.Cell.Row.Cells.FromKey("Absent").Value = 0
            e.Cell.Row.Cells.FromKey("Marked").Value = 1
        ElseIf e.Cell.Row.Cells.FromKey("Absent").Value = 1 Then
            e.Cell.Row.Cells.FromKey("Attend").Value = 0
            e.Cell.Row.Cells.FromKey("Sick").Value = 0
            e.Cell.Row.Cells.FromKey("Leave").Value = 0
            e.Cell.Row.Cells.FromKey("Delay").Value = 0
            e.Cell.Row.Cells.FromKey("Overtime").Value = 0
            e.Cell.Row.Cells.FromKey("Marked").Value = 1
        End If
        If e.Cell.Row.Cells.FromKey("Attend").Value = 0 Then
            e.Cell.Row.Cells.FromKey("Leave").Value = 0
            e.Cell.Row.Cells.FromKey("Delay").Value = 0
            e.Cell.Row.Cells.FromKey("Overtime").Value = 0
        End If
        If e.Cell.Row.Cells.FromKey("Leave").Value = 1 Then
            e.Cell.Row.Cells.FromKey("Absent").Value = 0
            e.Cell.Row.Cells.FromKey("Attend").Value = 1
            e.Cell.Row.Cells.FromKey("Sick").Value = 0
            e.Cell.Row.Cells.FromKey("Overtime").Value = 0
            e.Cell.Row.Cells.FromKey("Marked").Value = 1
        End If
    End Sub
    Protected Sub txtDate_ValueChange(sender As Object, e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtDate.ValueChange
        DropDownList_Shift.Items.Clear()
        ddlLocation.Items.Clear()

        Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        ClsProjects.GetDropDownList(ddlProject, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and convert(Datetime,'" & txtDate.Text & "') between StartDate and EndDate and ID in (select ProjectID from hrs_ProjectChanges where ID in (select ProjectChangeID from hrs_ProjectLocations where ID in (select ProjectLocationID from hrs_ProjectLocationUsers where UserID = " & ClsProjects.DataBaseUserRelatedID & ")))")

        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        If clsprojectchange.Find("ProjectID = " & ddlProject.SelectedValue & " and RegComputerID = 1 and FromDate <= convert(Datetime,'" & txtDate.Text & "') order by ID DESC") Then
            Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
            cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & clsprojectchange.ID & " and ID in (select ProjectLocationID from hrs_ProjectLocationUsers where UserID = " & clsprojectchange.DataBaseUserRelatedID & ")")

            Dim ClsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
            ClsAttendanceTableShifts.GetDropDownList(DropDownList_Shift, True, "ID in (select AttendanceTableShiftID from hrs_ProjectLocationShifts where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID = " & ddlLocation.SelectedValue & "))")
        End If
        GetData()
        If CProject.Value <> 0 Then
            Try
                ddlProject.SelectedValue = CProject.Value
                ddlProject_SelectedIndexChanged(Nothing, Nothing)
            Catch ex As Exception
                CLocation.Value = 0
                CShift.Value = 0
            End Try
        End If
        If CProject.Value <> 0 And CLocation.Value <> 0 Then
            Try
                ddlLocation.SelectedValue = CLocation.Value
                ddlLocation_SelectedIndexChanged(Nothing, Nothing)
            Catch ex As Exception
                CShift.Value = 0
            End Try
        End If

        If CProject.Value <> 0 And CLocation.Value <> 0 And CShift.Value <> 0 Then
            Try
                DropDownList_Shift.SelectedValue = CShift.Value
                DropDownList_Shift_SelectedIndexChanged(Nothing, Nothing)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        GetData()
    End Sub
End Class
