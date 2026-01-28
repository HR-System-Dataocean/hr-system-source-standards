Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjectWizardRequirements
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim clsBranch As New Clssys_Branches(Page)
                Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsBranch.ConnectionString)

                clsBranch.GetDropDownList(ddlbranches, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
                ddlbranches.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[Please Select Branch]/ [ برجاء إختيار الفرع]")
                Dim Clslocation As New ClsBasicFiles(Me.Page, "sys_Locations")
                Clslocation.GetDropDownList(ddllocation, True)
                txtFromDate.Value = DateTime.Now.AddDays(7).ToString("ddMMyyyy")
                LoadProjectsDDL()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function LoadProjectsDDL() As Boolean
        Try
            ddlproject.Items.Clear()
            Dim strbranchfilter As String = ""
            strbranchfilter = " and BranchID = " & ddlbranches.SelectedValue
            strbranchfilter = strbranchfilter & " and LocationID = " & ddllocation.SelectedValue
            Dim hrsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            If hrsProjects.Find("IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and isnull(EndDate,getdate()) > convert(Datetime,'" & txtFromDate.Text & "')" & strbranchfilter) Then
                Dim DT1 As DataTable = hrsProjects.DataSet.Tables(0)
                For Each Dr1 As DataRow In DT1.Rows
                    Dim hrsProjectChanges As New Clshrs_ProjectChanges(Me)
                    If hrsProjectChanges.Find("ProjectID = " & Dr1("ID") & " and RegComputerID = 1 and FromDate < convert(Datetime,'" & txtFromDate.Text & "') order by ID desc") Then
                        Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                        If hrsProjectPlacements.Find("ProjectChangeID = " & hrsProjectChanges.ID & " and ID in (select PlacementID from hrs_ProjectPlacementPlanning where isnull(ReferenceTo,0) in (0,1))") Then
                            Dim DT2 As DataTable = hrsProjectPlacements.DataSet.Tables(0)
                            Dim strPlacement As String = "0"
                            For Each Dr2 As DataRow In DT2.Rows
                                Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                                If Not hrsProjectPlacementEmployees.Find("PlacementCode = '" & Dr2("PlacementCode") & "' and convert(Datetime,'" & txtFromDate.Text & "') between FromDate and isnull(ToDate, convert(Datetime,'01/01/2070'))") Then
                                    strPlacement = strPlacement & "," & Dr2("ID")
                                End If
                            Next
                            If strPlacement <> "0" Then
                                ddlproject.Items.Add(New System.Web.UI.WebControls.ListItem((Dr1("Code") & " - " & Dr1("ArbName") & " : " & hrsProjectChanges.Remarks), Dr1("ID")))
                            End If
                        End If
                    End If
                Next
            End If
            GetData()
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetData() As Boolean
        Try
            uwgLocationPositions.Rows.Clear()
            Dim hrsProjectChanges As New Clshrs_ProjectChanges(Me)
            Dim str As String = "delete from hrs_employees where isnull(RegComputerID,0) = 1 and ID not in (select EmployeeID from hrs_ProjectPlacementEmployees)"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectChanges.ConnectionString, CommandType.Text, str)

            If hrsProjectChanges.Find("ProjectID = '" & ddlproject.SelectedValue & "' and RegComputerID = 1 and FromDate < convert(Datetime,'" & txtFromDate.Text & "') order by ID desc") Then
                Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                If hrsProjectPlacements.Find("ProjectChangeID = " & hrsProjectChanges.ID & " and ID in (select PlacementID from hrs_ProjectPlacementPlanning where isnull(ReferenceTo,0) in (0,1))") Then
                    Dim DT2 As DataTable = hrsProjectPlacements.DataSet.Tables(0)
                    Dim strPlacement As String = "0"
                    For Each Dr2 As DataRow In DT2.Rows
                        Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                        If Not hrsProjectPlacementEmployees.Find("PlacementCode = '" & Dr2("PlacementCode") & "' and convert(Datetime,'" & txtFromDate.Text & "') between FromDate and isnull(ToDate, convert(Datetime,'01/01/2070'))") Then
                            strPlacement = strPlacement & "," & Dr2("ID")
                        End If
                    Next
                    If strPlacement <> "0" Then
                        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                        Dim strcommand As String = "set dateformat dmy; select A.ID,A.PlacementCode,A.LocationDetailID,(select ArbName from hrs_Positions where ID = B.PositionID) AS Position,B.InternalAmt,B.WeekDays,CONVERT(nvarchar(10), isnull((select top 1 ToDate from hrs_ProjectPlacementEmployees where PlacementCode = A.PlacementCode order by ToDate Desc),(select FromDate from hrs_ProjectChanges where ID = A.ProjectChangeID)),103) as LastDate," & _
                                                   "case when (select COUNT(distinct AttendanceTableShiftID) from  hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID group by PlacementID) > 1 then '' ELSE (select top 1 TimeIn from hrs_AttendanceTableShifts where ID in (select AttendanceTableShiftID from hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID)) end AS TimeIn," & _
                                                   "case when (select COUNT(distinct AttendanceTableShiftID) from  hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID group by PlacementID) > 1 then '' ELSE (select top 1 TimeOut from hrs_AttendanceTableShifts where ID in (select AttendanceTableShiftID from hrs_ProjectPlacementPlanning where hrs_ProjectPlacementPlanning.PlacementID = A.ID)) end AS TimeOut " & _
                                                   " from hrs_ProjectPlacements A inner join hrs_ProjectLocationDetails B on A.LocationDetailID = B.ID" & _
                                                   " where A.ID in (select PlacementID from hrs_ProjectPlacementPlanning where isnull(ReferenceTo,0) in (0,1)) and A.ID in (" & strPlacement & ") and A.PlacementCode not in (select PlacementCode from hrs_ProjectPlacementEmployees where ToDate is null or ToDate >= convert(Datetime,'" & txtFromDate.Text & "'))"

                        uwgLocationPositions.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsProjects.ConnectionString, System.Data.CommandType.Text, strcommand)
                        uwgLocationPositions.DataBind()
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Protected Sub btnPrint_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnPrint.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        GetData()
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub

    Protected Sub ddlbranches_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlbranches.SelectedIndexChanged
        LoadProjectsDDL()
        GetData()
    End Sub

    Protected Sub ddllocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddllocation.SelectedIndexChanged
        LoadProjectsDDL()
        GetData()
    End Sub

    Protected Sub ddlproject_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlproject.SelectedIndexChanged
        GetData()
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As System.EventArgs) Handles LinkButton1.Click
        Dim intgr As Integer = ddlproject.SelectedValue
        LoadProjectsDDL()
        ddlproject.SelectedValue = intgr
        GetData()
    End Sub
End Class
