Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesSelector
    Inherits MainPage

    Const CPreparedData_TotalPenalty = 7

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim clsEmployee As New Clshrs_Employees(Page)
        Dim clsFiscalPeriods As New Venus.Application.SystemFiles.System.Clssys_FiscalYearsPeriods(Page)
        Dim clsVacationTypes As New Venus.Application.SystemFiles.HumanResource.Clshrs_VacationsTypes(Page)
        Dim clsAppraisalTypes As New Venus.Application.SystemFiles.HumanResource.Clshrs_AppraisalTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployee.ConnectionString)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim clsMainCurrency As New ClsSys_Currencies(Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Dim clsObjects As New Clssys_Objects(Page)
        Dim clsSearch As New Clssys_Searchs(Page)
        Dim ClsForms As New ClsSys_Forms(Page)
        Dim clsBranch As New Clssys_Branches(Page)
        Dim IntCurrentFiscalPeriod As Integer = 0
        Session("hdnFiscalDays") = hdnFiscalDays.Value
        If Not IsPostBack Then


            Page.Session.Add("ConnectionString", clsEmployee.ConnectionString)
            Page.Session.Add("Log", ObjNavigationHandler.SetLanguage(Page, "Eng/Arb"))
            UWGEmployeesProjects.Columns(3).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "FullEngName/FullArbName")
            If ClsCountries.Find(" IsMainCountries = 1 ") Then
                clsMainCurrency.Find(" ID=" & ClsCountries.CurrencyID)
                'If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                '    UWGEmployeesProjects.Columns.FromKey("VacationBalance").Format = clsMainCurrency.GetFormatOfDecimalPlaces(UWGEmployeesProjects.Columns.FromKey("VacationBalance").Format, clsMainCurrency.NoDecimalPlaces)
                'End If
            End If
            ddlDepartment.Attributes.Add("OnChange", "ddlDepartment_Change()")
            Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
            ClsDepartment.GetDropDownList(ddlDepartment, True)
            ddlDepartment.Items(0).Text = ObjNavigationHandler.SetLanguage(Page, "[All Departments]/ [ جميع الإدارات]")
            '  clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
            ' ddlBranche.Items(0).Text = ObjNavigationHandler.SetLanguage(Page, "[All Branches]/ [ جميع الفروع]")
            'ddlBranche.SelectedIndex = 1
            ClsForms.Find("EngName = '" & Page.Request.FilePath.Split("/")(3) & "'")
            'clsVacationTypes.GetDropDownList(DdlAppraisalType, True, "IsAnnual = 1")
            clsAppraisalTypes.GetDropDownList(DdlAppraisalType, True)

            DdlAppraisalType.SelectedIndex = 1
            IntCurrentFiscalPeriod = clsFiscalPeriods.GetLastOpenedFiscalPieriod(ClsForms.ModuleID)
            txtLang.Value = ObjNavigationHandler.SetLanguage(Page, "Eng/Arb")
            Page.Session.Add("Lage", ObjNavigationHandler.SetLanguage(Page, "0/1"))
            Page.Session.Add("ConnectionString", clsEmployee.ConnectionString)
        End If
        Venus.Shared.Web.ClientSideActions.GridSelection(Page, UWGEmployeesProjects)
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        Try

            ' Dim BranchID As Integer = ddlBranche.SelectedValue
            Dim DepartmentID As Integer = ddlDepartment.SelectedValue
            Dim strFilter As String = String.Empty


            If DepartmentID > 0 Then
                strFilter &= " And A.DepartmentID = " & DepartmentID
            End If


            If DdlAppraisalType.SelectedValue > 0 Then
                Dim clsFiscalPeriods As New Venus.Application.SystemFiles.System.Clssys_FiscalYearsPeriods(Page)
                Dim ExtraString = ""
                'If txtCode.Text <> "" Then
                '    ExtraString = " and A.Code = '" & txtCode.Text & "'"
                'End If
                Dim clsCompanies As New Clssys_Companies(Page)
                clsCompanies.Find("ID=" & clsCompanies.MainCompanyID)
                Dim clsEmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                Dim strcommand As String

                'strcommand = "set dateformat DMY; select (select top 1 ID from APP_AppraisalEmployeesStartDate where EmployeeID = A.ID ) AS ID,A.ID AS EmployeeID,A.Code AS EmployeeCode,IsNull( A.EngName,'') + ',' + IsNull( A.FatherEngName,'') + ',' + IsNull( A.GrandEngName,'') + ',' + IsNull( A.FamilyEngName,'') As FullEngName,IsNull( A.ArbName,'') + ',' + IsNull( A.FatherArbName,'') + ',' + IsNull( A.GrandArbName,'') + ',' + IsNull( A.FamilyArbName,'') As FullArbName,A.JoinDate,App.LastExternalAppraisalDate from Hrs_employees A left join APP_AppraisalEmployeesStartDate App on A.id=App.employeeID where ( AppraisalTypeID=" & DdlAppraisalType.SelectedValue & " or AppraisalTypeID is null) " & strFilter & ""
                'strcommand = "SET DATEFORMAT DMY; SELECT T1.ID,T1.EmployeeID,T1.EmployeeCode,T1.FullEngName,T1.FullArbName,CONVERT(VARCHAR(10), JoinDate, 103) AS JoinDate,CONVERT(VARCHAR(10), LastExternalAppraisalDate, 103)as LastExternalAppraisalDate FROM (SELECT (SELECT TOP 1 ID FROM APP_AppraisalEmployeesStartDate WHERE EmployeeID = A.ID) AS ID,A.ID AS EmployeeID,A.Code AS EmployeeCode,ISNULL(A.EngName, '') + ',' + ISNULL(A.FatherEngName, '') + ',' + ISNULL(A.GrandEngName, '') + ',' + ISNULL(A.FamilyEngName, '') AS FullEngName,ISNULL(A.ArbName, '') + ',' + ISNULL(A.FatherArbName, '') + ',' + ISNULL(A.GrandArbName, '') + ',' + ISNULL(A.FamilyArbName, '') AS FullArbName,A.JoinDate,App.LastExternalAppraisalDate FROM Hrs_employees A LEFT JOIN APP_AppraisalEmployeesStartDate App ON A.id = App.employeeID WHERE ( AppraisalTypeID=" & DdlAppraisalType.SelectedValue & " or AppraisalTypeID is null) " & strFilter & ") T1 INNER JOIN Hrs_contracts T2 ON T1.EmployeeID = T2.EmployeeID AND (T2.EndDate IS NULL OR T2.EndDate > GETDATE())"
                strcommand = "SET DATEFORMAT DMY; SELECT  A.ID AS EmployeeID,A.Code AS EmployeeCode,ISNULL(A.EngName, '') + ',' + ISNULL(A.FatherEngName, '') + ',' + ISNULL(A.GrandEngName, '') + ',' + ISNULL(A.FamilyEngName, '') AS FullEngName,ISNULL(A.ArbName, '') + ',' + ISNULL(A.FatherArbName, '') + ',' + ISNULL(A.GrandArbName, '') + ',' + ISNULL(A.FamilyArbName, '') AS FullArbName,A.JoinDate,App.LastExternalAppraisalDate ,App.AppraisalTypeID FROM Hrs_employees A INNER JOIN Hrs_contracts Cont ON A.ID = Cont.EmployeeID AND (Cont.EndDate IS NULL OR Cont.EndDate > GETDATE()) LEFT JOIN APP_AppraisalEmployeesStartDate App ON A.id = App.employeeID  And ( App.AppraisalTypeID=" & DdlAppraisalType.SelectedValue & " or App.AppraisalTypeID is null)  where 1=1 " & strFilter & ""

                Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsEmployeeVacationOpenBalance.ConnectionString, CommandType.Text, strcommand).Tables(0)
                UWGEmployeesProjects.Rows.Clear()
                UWGEmployeesProjects.DataSource = DT
                UWGEmployeesProjects.DataBind()
                CreateEmptyRows(0)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Delete.Command, LinkButton_Save.Command, ImageButton_Delete.Command, ImageButton_Save.Command
        Dim clsProjects As New Clshrs_Projects(Page, "hrs_Projects")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
        Try
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Save"
                    Try
                        If DdlAppraisalType.SelectedValue > 0 Then
                            Dim clsEmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                            Dim row As Infragistics.WebUI.UltraWebGrid.UltraGridRow
                            Dim str As String = "Delete from APP_AppraisalEmployeesStartDate where AppraisalTypeID=" & DdlAppraisalType.SelectedValue & ""

                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployeeVacationOpenBalance.ConnectionString, CommandType.Text, str)

                            Dim strInsert As String = ""

                            For intIndex = 0 To UWGEmployeesProjects.Rows.Count - 1
                                row = UWGEmployeesProjects.Rows(intIndex)

                                If row.Cells.FromKey("EmployeeID").Value <> Nothing Then
                                    strInsert = "Insert Into APP_AppraisalEmployeesStartDate (AppraisalTypeID,EmployeeID,JoinDate,LastExternalAppraisalDate) values(" & DdlAppraisalType.SelectedValue & "," & row.Cells.FromKey("EmployeeID").Value & ",'" & row.Cells.FromKey("JoinDate").Value & "','" & row.Cells.FromKey("LastExternalAppraisalDate").Value & "')"
                                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployeeVacationOpenBalance.ConnectionString, CommandType.Text, strInsert)

                                End If
                            Next



                        Else
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Must enter Appraisal Type/يجب إختيار نوع التقييم"))
                        End If
                    Catch ex As Exception
                    End Try
                Case "Delete"
                    'If txtCode.Text = "" Then
                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Must Enter Employee Code/يجب إختيار كود الموظف"))
                    '    Return
                    'End If

                    Dim clsEmp As New Clshrs_Employees(Page)
                    '      clsEmp.Find("Code='" & txtCode.Text & "'")
                    Dim strcommand As String = "Delete FROM hrs_EmployeeVacationOpenBalance Where EmployeeID=" & clsEmp.ID
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmp.ConnectionString, CommandType.Text, strcommand)
                    UWGEmployeesProjects.DataSource = Nothing
                    UWGEmployeesProjects.DataBind()
                    '      txtCode.Text = String.Empty
                    DdlAppraisalType.SelectedIndex = 0
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Delete Done/تم الحذف"))
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlAppraisalType.SelectedIndexChanged
        Dim clsEmpTrans As New Venus.Application.SystemFiles.HumanResource.Clshrs_EmployeesTransactions(Page)
        hdnFiscalDays.Value = 30
        UWGEmployeesProjects.Rows.Clear()
        CreateEmptyRows(40)
    End Sub

#End Region

#Region "Private Function"

    Private Function SwitchMsg(ByVal Active As Boolean, ByVal Days As Integer) As Boolean
        Dim ClsBaseClass As New ClsDataAcessLayer(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsBaseClass.ConnectionString)
        Try
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub CreateEmptyRows(ByVal intNoOfrows As Integer)
        If intNoOfrows > UWGEmployeesProjects.Rows.Count Then
            For i As Integer = 0 To (intNoOfrows - UWGEmployeesProjects.Rows.Count)
                UWGEmployeesProjects.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow())
            Next
        End If
    End Sub

    Private Function GetTwoDatesDiffByDay(ByVal date1 As DateTime, ByVal date2 As DateTime) As Integer
        Dim intDateDiff As Integer = DateDiff(DateInterval.Day, date1, date2) + 1
        If intDateDiff = 1 Then
            Return 0
        Else
            Return intDateDiff
        End If
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

        Dim str As String = IIf(intDeptID = 0, "", " And DB.DepartmentID = " & intDeptID)
        Try
            StrSelectCommand = " Declare @Branches as nvarchar(max)=''; " & _
                                "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " & _
                                "From sys_DepartmentsBranches DB Inner Join sys_Branches B On DB.BranchID = B.ID Where  DB.Checked  = 1 " & str & " And B.CancelDate Is Null; " & _
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
        btnSearch_Click(Nothing, Nothing)
    End Sub
End Class
