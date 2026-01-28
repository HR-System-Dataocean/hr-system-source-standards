Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.IO
Imports System.Data.OleDb
Imports System.Data

Partial Class Interfaces_frmMaxCodeInfo
    Inherits System.Web.UI.Page

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields

    Dim ClsEmployee As Clshrs_Employees

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsBranch As New Clssys_Branches(Page)
        'Me.RightToLeft = Windows.Forms.RightToLeft.Yes
        'Me.RightToLeftLayout = True
        If Not IsPostBack Then
            clsBranch.GetDropDownList(ddlBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsBranch.ConnectionString)
            lblBranch.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Branch/«·›—⁄")
            lblEmp.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Employee/«·„ÊŸ›")
            lblPrefix.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Numbering according/«· —ﬁÌ„ Õ”»")
            lblSearch.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Employee Code Start/ﬂÊœ «·„ÊŸ› Ì»œ√ »‹")
            btnSearch.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Search/»ÕÀ")
            'lblSearch.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Search/»ÕÀ")
            Dim empRow As DataRow = GetEmployeeWithMaxCode("")
            If empRow IsNot Nothing Then
                Dim code As String = empRow("Code").ToString()
                Dim name As String = empRow("Name").ToString()

                TxtCode.Text = code
                TxtName.Text = name

            End If
        End If
    End Sub

    Protected Sub ddlprefix_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlprefix.SelectedIndexChanged

        If ddlprefix.SelectedValue = 0 Then
            Dim clsBranch As New Clssys_Branches(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsBranch.ConnectionString)

            lblBranch.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Branch/«·›—⁄")

            clsBranch.GetDropDownList(ddlBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")


            ddlBranch.Focus()
        End If

        If ddlprefix.SelectedValue = 1 Then
            Dim ClsDepartment As New Clssys_Departments(Me.Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDepartment.ConnectionString)

            lblBranch.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Department/«·ﬁ”„")

            ClsDepartment.GetDropDownList(ddlBranch, True)

            ddlBranch.Focus()
        End If

        If ddlprefix.SelectedValue = 2 Then
            Dim ClsPosition As New Clshrs_Positions(Me.Page)

            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsPosition.ConnectionString)

            lblBranch.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Position/«·ÊŸÌ›…")

            ClsPosition.GetDropDownList(ddlBranch, True)
            ddlBranch.Focus()
        End If

        Dim empRow As DataRow = GetEmployeeWithMaxCode("")
        If empRow IsNot Nothing Then
            Dim code As String = empRow("Code").ToString()
            Dim name As String = empRow("Name").ToString()

            TxtCode.Text = code
            TxtName.Text = name

        End If

    End Sub

    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBranch.SelectedIndexChanged
        If ddlBranch.SelectedValue <> 0 Then
            Dim ClsDepartment As New Clssys_Departments(Me.Page)
            Dim criteria = ""
            If ddlprefix.SelectedValue = 0 Then

                criteria = " and BranchID=" & ddlBranch.SelectedValue
            End If

            If ddlprefix.SelectedValue = 1 Then

                criteria = " and DepartmentID=" & ddlBranch.SelectedValue
            End If

            If ddlprefix.SelectedValue = 2 Then

                criteria = " and ID in (Select EmployeeID from hrs_Contracts where  StartDate <= '" & Format(DateTime.Now, "dd/MM/yyyy") & "' And (enddate is null or '" & Format(DateTime.Now, "dd/MM/yyyy") & "' Between StartDate and EndDate)  and  PositionID=" & ddlBranch.SelectedValue & " ) "
            End If
            Dim empRow As DataRow = GetEmployeeWithMaxCode(criteria)

            If empRow IsNot Nothing Then
                Dim code As String = empRow("Code").ToString()
                Dim name As String = empRow("Name").ToString()

                TxtCode.Text = code
                TxtName.Text = name

            End If

            ddlBranch.Focus()
        End If
    End Sub

    Protected Sub btnSearch_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSearch.Command
        Dim clsBranch As New Clssys_Branches(Page)
        Dim EmpName As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "

        Else
            EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,0) "

        End If
        ' Step 2: get employee row with that max code
        Dim empSql As String = "SELECT  Code," & EmpName & " as EmpName " &
                           "FROM hrs_Employees " &
                           "WHERE 1=1 "
        If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
            empSql += " and  Code LIKE '" & txtSearch.Text & "%'"
        End If
        empSql += " ORDER BY TRY_CAST(SUBSTRING(CODE, PATINDEX('%[0-9]%', CODE), LEN(CODE)) AS INT) ASC "
        Dim dt As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsBranch.ConnectionString, Data.CommandType.Text, empSql).Tables(0)
        UwgSearchEmployees.DataSource = Nothing
        UwgSearchEmployees.DataBind()

        UwgSearchEmployees.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
        UwgSearchEmployees.DataSource = dt
        UwgSearchEmployees.DataBind()
    End Sub
#End Region



    Public Function GetEmployeeWithMaxCode(criteria As String) As DataRow
        Dim clsBranch As New Clssys_Branches(Page)
        ' Base query for max code
        Dim maxCodeSql As String = "SELECT TOP 1 CODE FROM hrs_Employees WHERE CODE LIKE '%[0-9]%' " & criteria &
                           " ORDER BY TRY_CAST(SUBSTRING(CODE, PATINDEX('%[0-9]%', CODE), LEN(CODE)) AS INT) DESC"

        ' Step 1: get max code
        Dim maxCode As String = Convert.ToString(
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsBranch.ConnectionString, CommandType.Text, maxCodeSql)
    )

        If String.IsNullOrEmpty(maxCode) Then
            Return Nothing
        End If
        Dim EmpName As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "

        Else
            EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,0) "

        End If
        ' Step 2: get employee row with that max code
        Dim empSql As String = "SELECT TOP 1 Code," & EmpName & " as Name " &
                           "FROM hrs_Employees " &
                           "WHERE CONVERT(NVARCHAR, Code) LIKE '%" & maxCode & "'"

        Dim dt As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsBranch.ConnectionString, Data.CommandType.Text, empSql).Tables(0)

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)   ' return first match
        End If

        Return Nothing
    End Function

End Class
