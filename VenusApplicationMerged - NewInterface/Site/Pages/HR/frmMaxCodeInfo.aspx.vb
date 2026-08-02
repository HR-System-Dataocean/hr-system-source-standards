Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class Interfaces_frmMaxCodeInfo
    Inherits System.Web.UI.Page

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields
    Dim ClsEmployee As Clshrs_Employees
    Private ObjNavigationHandler As Venus.Shared.Web.NavigationHandler
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsBranch As New Clssys_Branches(Page)
        ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(clsBranch.ConnectionString)

        ' تعيين اللغة
        SetLanguage()

        If Not IsPostBack Then
            clsBranch.GetDropDownList(ddlBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")

            ' تعيين النصوص
            lblPrefix.Text = GetText("Numbering according", "الترقيم حسب")
            lblBranch.Text = GetText("Branch", "الفرع")
            lblEmp.Text = GetText("Employee", "الموظف")
            lblSearch.Text = GetText("Employee Code Start", "كود الموظف يبدأ بـ")
            btnSearch.Text = GetText("Search", "بحث")

            ddlprefix.Items(0).Text = GetText("Branch", "الفرع")
            ddlprefix.Items(1).Text = GetText("Department", "الإدارة")
            ddlprefix.Items(2).Text = GetText("Position", "الوظيفة")

            Dim empRow As DataRow = GetEmployeeWithMaxCode("")
            If empRow IsNot Nothing Then
                Dim code As String = empRow("Code").ToString()
                Dim name As String = empRow("Name").ToString()
                TxtCode.Text = code
                TxtName.Text = name
            End If
        End If
    End Sub

    Private Sub SetLanguage()
        Try
            Dim lang As String = Request.QueryString("Lang")

            If Not String.IsNullOrEmpty(lang) Then
                If lang = "Ar" Then
                    System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("ar-EG")
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("ar-EG")
                    DIV1.Attributes("dir") = "rtl"
                Else
                    System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US")
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
                    DIV1.Attributes("dir") = "ltr"
                End If
            ElseIf ProfileCls.CurrentLanguage = "Ar" Then
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("ar-EG")
                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("ar-EG")
                DIV1.Attributes("dir") = "rtl"
            Else
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US")
                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
                DIV1.Attributes("dir") = "ltr"
            End If
        Catch
        End Try
    End Sub

    Private Function GetText(ByVal english As String, ByVal arabic As String) As String
        Try
            If ProfileCls.CurrentLanguage = "Ar" Then
                Return arabic
            Else
                Return english
            End If
        Catch
            Return english
        End Try
    End Function

    Protected Sub ddlprefix_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlprefix.SelectedIndexChanged
        If ddlprefix.SelectedValue = 0 Then
            Dim clsBranch As New Clssys_Branches(Page)
            clsBranch.GetDropDownList(ddlBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
            ddlBranch.Focus()
        End If

        If ddlprefix.SelectedValue = 1 Then
            Dim ClsDepartment As New Clssys_Departments(Me.Page)
            ClsDepartment.GetDropDownList(ddlBranch, True)
            ddlBranch.Focus()
        End If

        If ddlprefix.SelectedValue = 2 Then
            Dim ClsPosition As New Clshrs_Positions(Me.Page)
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

        ' تعيين عناوين الأعمدة
        Try
            Dim colCode As Infragistics.WebUI.UltraWebGrid.UltraGridColumn = UwgSearchEmployees.Bands(0).Columns.FromKey("Code")
            If colCode IsNot Nothing Then
                colCode.Header.Caption = GetText("Employee Code", "كود الموظف")
            End If

            Dim colEmpName As Infragistics.WebUI.UltraWebGrid.UltraGridColumn = UwgSearchEmployees.Bands(0).Columns.FromKey("EmpName")
            If colEmpName IsNot Nothing Then
                colEmpName.Header.Caption = GetText("Employee Name", "اسم الموظف")
            End If
        Catch
        End Try
    End Sub
#End Region

    Public Function GetEmployeeWithMaxCode(criteria As String) As DataRow
        Dim clsBranch As New Clssys_Branches(Page)
        Dim maxCodeSql As String = "SELECT TOP 1 CODE FROM hrs_Employees WHERE CODE LIKE '%[0-9]%' " & criteria &
                           " ORDER BY TRY_CAST(SUBSTRING(CODE, PATINDEX('%[0-9]%', CODE), LEN(CODE)) AS INT) DESC"

        Dim maxCode As String = Convert.ToString(
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsBranch.ConnectionString, CommandType.Text, maxCodeSql))

        If String.IsNullOrEmpty(maxCode) Then
            Return Nothing
        End If

        Dim EmpName As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
        Else
            EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,0) "
        End If

        Dim empSql As String = "SELECT TOP 1 Code," & EmpName & " as Name " &
                           "FROM hrs_Employees " &
                           "WHERE CONVERT(NVARCHAR, Code) LIKE '%" & maxCode & "'"

        Dim dt As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsBranch.ConnectionString, Data.CommandType.Text, empSql).Tables(0)

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        End If

        Return Nothing
    End Function

End Class