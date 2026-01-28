Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjectMonitorRequirements
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim clsBranch As New Clssys_Branches(Page)
                Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsBranch.ConnectionString)

                clsBranch.GetDropDownList(ddlbranches, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
                ddlbranches.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Branches]/ [ جميع الفروع]")

                txtFromDate.Value = DateTime.Now.ToString("ddMMyyyy")
                txtToDate.Value = DateTime.Now.AddMonths(1).ToString("ddMMyyyy")

                GetData()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetData() As Boolean
        Try
            uwgLocationPositions.Rows.Clear()
            Dim strbranchfilter As String = ""
            If ddlbranches.SelectedValue > 0 Then
                strbranchfilter = " and BranchID = " & ddlbranches.SelectedValue
            End If
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
                                uwgLocationPositions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {hrsProjectChanges.ID, (Dr1("Code") & " - " & Dr1("ArbName") & " : " & hrsProjectChanges.Remarks), hrsProjectChanges.FromDate.ToString("dd/MM/yyyy"), strPlacement}))
                            End If
                        End If
                    End If
                    If hrsProjectChanges.Find("ProjectID = " & Dr1("ID") & " and RegComputerID = 1 and FromDate between convert(Datetime,'" & txtFromDate.Text & "') and convert(Datetime,'" & txtToDate.Text & "') order by ID Asc") Then
                        Dim DT3 As DataTable = hrsProjectChanges.DataSet.Tables(0)
                        For Each Dr3 As DataRow In DT3.Rows
                            Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                            If hrsProjectPlacements.Find("ProjectChangeID = " & Dr3("ID") & " and ID in (select PlacementID from hrs_ProjectPlacementPlanning where isnull(ReferenceTo,0) in (0,1))") Then
                                Dim DT4 As DataTable = hrsProjectPlacements.DataSet.Tables(0)
                                Dim strPlacement As String = "0"
                                For Each Dr4 As DataRow In DT4.Rows
                                    Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
                                    If Not hrsProjectPlacementEmployees.Find("PlacementCode = '" & Dr4("PlacementCode") & "' and convert(Datetime,'" & txtToDate.Text & "') between FromDate and isnull(ToDate, convert(Datetime,'01/01/2070'))") Then
                                        strPlacement = strPlacement & "," & Dr4("ID")
                                    End If
                                Next
                                If strPlacement <> "0" Then
                                    uwgLocationPositions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Dr3("ID"), (Dr1("Code") & " - " & Dr1("ArbName") & " : " & Dr3("Remarks")), Convert.ToDateTime(Dr3("FromDate")).ToString("dd/MM/yyyy"), strPlacement}))
                                End If
                            End If
                        Next
                    End If
                Next
            End If
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
        GetData()
    End Sub
End Class
