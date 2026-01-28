Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmPreparedPeriods
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim IntSelectedPeriod As Integer = 0
        Dim IntModuleId As Integer = GetModuleID("frmPrepareSalaries")
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
            ddlDepartment.Attributes.Add("OnChange", "ddlDepartment_Change()")
            Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")

            ClsDepartment.GetDropDownList(ddlDepartment, True)
            ddlDepartment.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Departments]/ [ جميع الإدارات]")
            clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")

            ddlBranche.SelectedIndex = 1
            ddlBranche.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Branches]/ [ جميع الفروع]")

            ClsFisicalYearsPeriods.GetDropDownList(DdlPeriods, IntModuleId, True, "")

            IntSelectedPeriod = ClsFisicalYearsPeriods.GetLastOpenedFiscalPieriod(IntModuleId)

            UwgSearchEmployees.Columns.FromKey("FullName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))
            DdlPeriods.SelectedIndex = 0

            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
        End If
    End Sub

    Protected Sub DdlPeriods_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlPeriods.SelectedIndexChanged
        GetData()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub

    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Prepare.Command, LinkButton_Refund.Command, ImageButton_Prepare.Command, ImageButton_Refund.Command
        Try
            Dim StrMode As String = Request.QueryString.Item("SM")
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Prepare"
                    Dim ClsEmployeeTransactions As New Clshrs_EmployeesTransactions(Page)
                    Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)
                    Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("Code").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        ClsEmployeeTransactions.Find("PostDate is null and ID not in (select isnull(RegComputerID,0) from hrs_EmployeesTransactions) and ID=" & row.Cells.FromKey("ID").Value)
                        If ClsEmployeeTransactions.ID > 0 Then
                            If row.Cells(1).Value = True Then
                                ClsEmployeeTransactions.Applyed = True
                            End If
                            ClsEmployeeTransactions.Update("ID=" & row.Cells.FromKey("ID").Value)
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done!/تم الحفظ"))
                Case "Refund"
                    Dim ClsEmployeeTransactions As New Clshrs_EmployeesTransactions(Page)
                    Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)
                    Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("Code").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        ClsEmployeeTransactions.Find("PostDate is null and ID not in (select isnull(RegComputerID,0) from hrs_EmployeesTransactions) and ID=" & row.Cells.FromKey("ID").Value)
                        If ClsEmployeeTransactions.ID > 0 Then
                            If row.Cells(1).Value = True Then
                                ClsEmployeeTransactions.Applyed = False
                                ClsEmployeeTransactions.Update("ID=" & row.Cells.FromKey("ID").Value)
                            End If
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Done!/تم العملية"))
            End Select
            GetData()
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Private Function"

    Private Function GetData() As Boolean
        Try

            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)

            Dim BranchID As Integer = ddlBranche.SelectedValue
            Dim DepartmentID As Integer = ddlDepartment.SelectedValue

            UwgSearchEmployees.DataSource = Nothing
            UwgSearchEmployees.DataBind()

            If DdlPeriods.SelectedIndex = 0 Then
                Return False
            End If

            ClsFisicalPeriods.Find("ID=" & DdlPeriods.SelectedValue)

            Dim strFilter As String = " And e.Code like '%" & txtCode.Text & "%' And dbo.fn_CheckEndOfServiceByPeriod(e.ID," & ClsFisicalPeriods.ID & ")>0"

            If BranchID > 0 Then
                strFilter &= " And e.BranchID = " & BranchID
            End If

            If DepartmentID > 0 Then
                strFilter &= " And e.DepartmentID = " & DepartmentID
            End If
            If ChkHasBankNo.Checked = True Then
                strFilter &= " And e.BankAccountNumber is null"

            End If

            Dim strCommand As String
            strCommand = " select et.ID,et.EmployeeID ,e.Code as Code," & "dbo.fn_GetEmpName(e.Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName,et.Applyed as Prepared from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' "

            strCommand &= strFilter

            Dim dsEmployee As New Data.DataSet
            dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, strCommand)

            If dsEmployee.Tables(0).Rows.Count > 0 Then
                Dim ds As New Data.DataSet
                ds.Tables.Add()
                ds.Tables(0).Columns.Add("Code")
                ds.Tables(0).Rows.Add(ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل"))
                ds.Tables(0).Merge(dsEmployee.Tables(0))

                UwgSearchEmployees.DataSource = ds.Tables(0)
                UwgSearchEmployees.DataBind()
            End If

            ddlDepartment.SelectedValue = DepartmentID
            ddlBranche.SelectedValue = BranchID

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetModuleID(ByVal TableName As String) As Integer
        Dim ClsForms As New ClsSys_Forms(Me.Page)
        Dim IntModuleID As Integer
        ClsForms.Find(" Code = '" & TableName & "'")
        If ClsForms.ID > 0 Then
            IntModuleID = ClsForms.ModuleID
        End If
        Return IntModuleID
    End Function

#End Region

#Region "Public Shared Function"

    Public Shared Function CheckBranchPermission(ByVal intDeptID As Integer) As String
        Try
            Dim str As String = ""
            Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim mCompanyID As Integer = CType(HttpContext.Current.Session("CompanyID"), Integer)
            Dim mUserID As Integer = CType(HttpContext.Current.Session("UserID"), Integer)
            Dim BranchesIDs As String = GetRelatedDept(intDeptID)
            BranchesIDs = IIf(BranchesIDs = "", "0", BranchesIDs)

            Dim StrSelectCommand As String = _
                    "Declare @Branches as nvarchar(max)='';" & _
                    "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " & _
                    "From sys_Branches B Inner Join sys_CompaniesBranches CB ON CB.BrancheID=B.ID Where B.ID IN (" & BranchesIDs & ")  And CB.CompanyID=" & mCompanyID & " And CB.UserID=" & mUserID & " AND CanView= 1" & _
                    "Select @Branches  = STUFF(@Branches,1,1,''); " & _
                    "Select IsNull(@Branches,'')"

            str = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, StrSelectCommand)

            Return IIf(str = "", "0", str)

        Catch ex As Exception
            Return "0"
        End Try
    End Function
    Public Shared Function GetRelatedDept(ByVal intDeptID As Integer) As String
        Dim StrSelectCommand As String
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Dim dsBranches As New Data.DataSet

        Try
            StrSelectCommand = " Declare @Branches as nvarchar(max)=''; " & _
                                "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " & _
                                "From sys_DepartmentsBranches DB Inner Join sys_Branches B On DB.BranchID = B.ID Where DB.DepartmentID = " & intDeptID & " And DB.Checked  = 1 And B.CancelDate Is Null; " & _
                                "Select @Branches  = STUFF(@Branches,1,1,''); " & _
                                "Select IsNull(@Branches,'')"

            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, StrSelectCommand)

        Catch ex As Exception

        End Try
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetRelatedDepartment(ByVal strDeptID As String) As String
        Try

            Dim dsBranches As New Data.DataSet
            Dim StrSelectCommand As String
            Dim strResultBranches As String = CheckBranchPermission(strDeptID)
            Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim Lage As String = CType(HttpContext.Current.Session("Lage"), String)
            Dim mCompanyID As Integer = CType(HttpContext.Current.Session("CompanyID"), Integer)
            Dim mUserID As Integer = CType(HttpContext.Current.Session("UserID"), Integer)
            Dim strFieldName As String = IIf(Lage = "0", "EngName", "ArbName")
            Dim strAllName As String = IIf(Lage = "0", "[All branches]", "[ جميع الفروع]")
            Dim str As String = String.Empty

            StrSelectCommand = " Select B.ID, B." & strFieldName & " From sys_Branches B Inner Join sys_CompaniesBranches CB ON CB.BrancheID=B.ID Where B.ID IN (" & IIf(strResultBranches = "", 0, strResultBranches) & ") And CB.CompanyID=" & mCompanyID & " And CB.UserID=" & mUserID & " AND CanView= 1"

            dsBranches = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr, Data.CommandType.Text, StrSelectCommand)

            If dsBranches.Tables(0).Rows.Count > 0 Then

                str = "<select style='border: 1px solid rgb(204, 204, 204); width: 100%; height: 20px; color: black; font-family: Tahoma; font-size: 8pt; font-weight: normal;' id='UltraWebTab1__ctl0_ddlBranche' name='UltraWebTab1$_ctl0$ddlBranche'><option value=0>" & strAllName & "</option>"

                For I As Integer = 0 To dsBranches.Tables(0).Rows.Count - 1
                    str &= "<option value=" & dsBranches.Tables(0).Rows(I).Item("ID") & ">" & dsBranches.Tables(0).Rows(I).Item(strFieldName) & "</option>"
                Next

                str &= "</select>"

                Return str
            Else
                Return "<select style='border: 1px solid rgb(204, 204, 204); width: 100%; height: 20px; color: black; font-family: Tahoma; font-size: 8pt; font-weight: normal;' id='UltraWebTab1__ctl0_ddlBranche' name='UltraWebTab1$_ctl0$ddlBranche'><option value=0>" & strAllName & "</option></select>"
            End If

        Catch ex As Exception
        End Try
    End Function

#End Region

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetData()
    End Sub
End Class
