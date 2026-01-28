Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjectMonitorStatus
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim clsBranch As New Clssys_Branches(Page)
                Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsBranch.ConnectionString)

                clsBranch.GetDropDownList(ddlbranches, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
                ddlbranches.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Branches]/ [ جميع الفروع]")

                txtFromDate.Value = New DateTime(DateTime.Now.AddYears(-2).Year, 1, 1).ToString("ddMMyyyy")
                txtToDate.Value = New DateTime(DateTime.Now.AddYears(2).Year, 12, 31).ToString("ddMMyyyy")
                GetData()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetData() As Boolean
        If ddlSattus.SelectedValue = 0 Then
            Try
                uwgLocationPositions.Rows.Clear()
                uwgLocationPositions.Columns(4).Hidden = True
                uwgLocationPositions.Columns(5).Hidden = True
                uwgLocationPositions.Columns(6).Hidden = True
                uwgLocationPositions.Columns(7).Hidden = True
                uwgLocationPositions.Columns(8).Hidden = False
                Dim strbranchfilter As String = ""
                If ddlbranches.SelectedValue > 0 Then
                    strbranchfilter = " and BranchID = " & ddlbranches.SelectedValue
                End If
                Dim hrsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                If hrsProjects.Find("isnull(IsLocked,0) = 0 and isnull(IsStoped,0) = 0 and CancelDate is null and (isnull(EndDate,getdate()) <= convert(Datetime,'" & txtToDate.Text & "') and StartDate >= convert(Datetime,'" & txtFromDate.Text & "'))" & strbranchfilter) Then
                    Dim DT1 As DataTable = hrsProjects.DataSet.Tables(0)
                    For Each Dr1 As DataRow In DT1.Rows
                        uwgLocationPositions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Dr1("ID"), Dr1("Code") & " - " & Dr1("ArbName"), Convert.ToDateTime(Dr1("StartDate")).ToString("dd/MM/yyyy"), Convert.ToDateTime(Dr1("EndDate")).ToString("dd/MM/yyyy")}))
                    Next
                End If
            Catch ex As Exception
            End Try
        ElseIf ddlSattus.SelectedValue = 1 Then
            Try
                uwgLocationPositions.Rows.Clear()
                uwgLocationPositions.Columns(4).Hidden = False
                uwgLocationPositions.Columns(5).Hidden = False
                uwgLocationPositions.Columns(6).Hidden = False
                uwgLocationPositions.Columns(7).Hidden = False
                uwgLocationPositions.Columns(8).Hidden = False

                Dim strbranchfilter As String = ""
                If ddlbranches.SelectedValue > 0 Then
                    strbranchfilter = " and BranchID = " & ddlbranches.SelectedValue
                End If
                Dim hrsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                If hrsProjects.Find("isnull(IsLocked,0) = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and (isnull(EndDate,getdate()) <= convert(Datetime,'" & txtToDate.Text & "') and StartDate >= convert(Datetime,'" & txtFromDate.Text & "'))" & strbranchfilter) Then
                    Dim DT1 As DataTable = hrsProjects.DataSet.Tables(0)
                    For Each Dr1 As DataRow In DT1.Rows
                        uwgLocationPositions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Dr1("ID"), Dr1("Code") & " - " & Dr1("ArbName"), Convert.ToDateTime(Dr1("StartDate")).ToString("dd/MM/yyyy"), Convert.ToDateTime(Dr1("EndDate")).ToString("dd/MM/yyyy")}))
                    Next
                End If
            Catch ex As Exception
            End Try
        ElseIf ddlSattus.SelectedValue = 2 Then
            Try
                uwgLocationPositions.Rows.Clear()
                uwgLocationPositions.Columns(4).Hidden = True
                uwgLocationPositions.Columns(5).Hidden = True
                uwgLocationPositions.Columns(6).Hidden = True
                uwgLocationPositions.Columns(7).Hidden = True
                uwgLocationPositions.Columns(8).Hidden = False
                Dim strbranchfilter As String = ""
                If ddlbranches.SelectedValue > 0 Then
                    strbranchfilter = " and BranchID = " & ddlbranches.SelectedValue
                End If
                Dim hrsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                If hrsProjects.Find("isnull(IsStoped,0) = 1 and CancelDate is null and (isnull(EndDate,getdate()) <= convert(Datetime,'" & txtToDate.Text & "') and StartDate >= convert(Datetime,'" & txtFromDate.Text & "'))" & strbranchfilter) Then
                    Dim DT1 As DataTable = hrsProjects.DataSet.Tables(0)
                    For Each Dr1 As DataRow In DT1.Rows
                        uwgLocationPositions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Dr1("ID"), Dr1("Code") & " - " & Dr1("ArbName"), Convert.ToDateTime(Dr1("StartDate")).ToString("dd/MM/yyyy"), Convert.ToDateTime(Dr1("EndDate")).ToString("dd/MM/yyyy")}))
                    Next
                End If
            Catch ex As Exception
            End Try
        End If
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
        GetData()
    End Sub

    Protected Sub ddlSattus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSattus.SelectedIndexChanged
        GetData()
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As System.EventArgs) Handles LinkButton2.Click
        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        clsProjects.Find("ID = " & HiddenField_ProjID.Value)
        clsProjects.IsStoped = True
        clsProjects.Update("ID = " & HiddenField_ProjID.Value)

        Dim closeall As String = "update hrs_ProjectPlacementEmployees set ToDate = getdate() where PlacementCode in (select PlacementCode from hrs_ProjectPlacements where ProjectID = " & HiddenField_ProjID.Value & ")"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsProjects.ConnectionString, CommandType.Text, closeall)
        GetData()
    End Sub
End Class
