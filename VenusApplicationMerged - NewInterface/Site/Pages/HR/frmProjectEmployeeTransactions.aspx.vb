Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjectEmployeeTransactions
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim blarrSecurity() As Boolean = {True, True, True, True}
        CheckTabsSecuirty(blarrSecurity)

        If blarrSecurity(0) = 0 Then UwgSearchEmployees.Bands(0).Columns(4).Hidden = True
        If blarrSecurity(1) = 0 Then UwgSearchEmployees.Bands(0).Columns(3).Hidden = True
        If blarrSecurity(2) = 0 Then UwgSearchEmployees.Bands(0).Columns(5).Hidden = True
        If blarrSecurity(3) = 0 Then UwgSearchEmployees.Bands(0).Columns(6).Hidden = True

        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim IntSelectedPeriod As Integer = 0
        Dim clsBranch As New Clssys_Branches(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)

        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
        If ClsObjects.Find(" Code='" & ClsEmployee.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If

        If Not IsPostBack Then
            clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
            ddlBranche.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[Select Branches]/ [ إختر الفرع]")

            Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            clsProjects.GetDropDownList(DdlProjects, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and BranchID = " & ddlBranche.SelectedValue)

            UwgSearchEmployees.Columns.FromKey("EmployeeName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))
        End If
    End Sub
    Public Function CheckTabsSecuirty(ByRef blArraySecuirty() As Boolean) As Boolean
        Dim DS As DataSet
        Dim ClsEmployee As New Clshrs_Employees(Page)
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, "hrs_GetFormsPermissions", ClsEmployee.DataBaseUserRelatedID, ClsEmployee.GroupID, "frmProjectEmployeeExtension")
        If DS.Tables(0).Rows.Count <= 0 Then blArraySecuirty(0) = False
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, "hrs_GetFormsPermissions", ClsEmployee.DataBaseUserRelatedID, ClsEmployee.GroupID, "frmProjectEmployeeTransfeer")
        If DS.Tables(0).Rows.Count <= 0 Then blArraySecuirty(1) = False
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, "hrs_GetFormsPermissions", ClsEmployee.DataBaseUserRelatedID, ClsEmployee.GroupID, "frmProjectEmployeePenalities")
        If DS.Tables(0).Rows.Count <= 0 Then blArraySecuirty(2) = False
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, "hrs_GetFormsPermissions", ClsEmployee.DataBaseUserRelatedID, ClsEmployee.GroupID, "frmProjectEmployeeRewards")
        If DS.Tables(0).Rows.Count <= 0 Then blArraySecuirty(3) = False
    End Function
    Protected Sub DdlPeriods_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlProjects.SelectedIndexChanged
        GetData()
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub

#End Region

#Region "Private Function"

    Private Function GetData() As Boolean
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim strCommand As String
            strCommand = "Set DateFormat DMY; select ID,Code,dbo.fn_GetEmpName(Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS EmployeeName from hrs_Employees where isnull(IsProjectRelated,0) = 1 and isnull(RegComputerID,0) = 0 and isnull(IsSpecialForce,0) = 0"


            Dim strFilter As String = " And dbo.fn_CheckEndOfService(hrs_Employees.ID) > 0 "
            If txtCode.Text <> "" Then strFilter = strFilter + " and Code = '" & txtCode.Text & "'"

            strFilter = strFilter + " and BranchID = '" & ddlBranche.SelectedValue & "'"

            If DdlProjects.SelectedValue > 0 Then
                Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
                If clsprojectchange.Find("ProjectID = " & DdlProjects.SelectedValue & " and RegComputerID = 1 and FromDate <= getdate() order by ID DESC") Then
                    strFilter = strFilter & " and ID in (select EmployeeID from hrs_ProjectPlacementEmployees where (ToDate is null or ToDate > getdate()) and PlacementCode in (select PlacementCode from hrs_ProjectPlacements where ProjectChangeID = " & clsprojectchange.ID & "))"
                End If
            End If

            Dim dsEmployee As New Data.DataSet
            dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, strCommand & strFilter)
            UwgSearchEmployees.DataSource = dsEmployee.Tables(0)
            UwgSearchEmployees.DataBind()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetData()
    End Sub

    Protected Sub ddlBranche_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBranche.SelectedIndexChanged
        Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
        clsProjects.GetDropDownList(DdlProjects, True, "IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null and BranchID = " & ddlBranche.SelectedValue)
        GetData()
    End Sub
End Class
