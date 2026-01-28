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
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & clsEmployee.Table.Trim & "'") Then
                If clsSearch.Find(" ObjectID='" & clsObjects.ID & "'") Then
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & clsSearch.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            Page.Session.Add("ConnectionString", clsEmployee.ConnectionString)
            Page.Session.Add("Log", ObjNavigationHandler.SetLanguage(Page, "Eng/Arb"))
            UWGEmployeesProjects.Columns(3).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "FullEngName/FullArbName")
            If ClsCountries.Find(" IsMainCountries = 1 ") Then
                clsMainCurrency.Find(" ID=" & ClsCountries.CurrencyID)
                If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                    UWGEmployeesProjects.Columns.FromKey("VacationBalance").Format = clsMainCurrency.GetFormatOfDecimalPlaces(UWGEmployeesProjects.Columns.FromKey("VacationBalance").Format, clsMainCurrency.NoDecimalPlaces)
                End If
            End If
            ddlDepartment.Attributes.Add("OnChange", "ddlDepartment_Change()")
            Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
            ClsDepartment.GetDropDownList(ddlDepartment, True)
            ddlDepartment.Items(0).Text = ObjNavigationHandler.SetLanguage(Page, "[All Departments]/ [ جميع الإدارات]")
            clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
            ddlBranche.Items(0).Text = ObjNavigationHandler.SetLanguage(Page, "[All Branches]/ [ جميع الفروع]")
            ddlBranche.SelectedIndex = 1
            ClsForms.Find("EngName = '" & Page.Request.FilePath.Split("/")(3) & "'")
            clsVacationTypes.GetDropDownList(DdlVacationType, True, "IsAnnual = 1")
            DdlVacationType.SelectedIndex = 1
            IntCurrentFiscalPeriod = clsFiscalPeriods.GetLastOpenedFiscalPieriod(ClsForms.ModuleID)
            txtLang.Value = ObjNavigationHandler.SetLanguage(Page, "Eng/Arb")
            Page.Session.Add("Lage", ObjNavigationHandler.SetLanguage(Page, "0/1"))
            Page.Session.Add("ConnectionString", clsEmployee.ConnectionString)
        End If
        Venus.Shared.Web.ClientSideActions.GridSelection(Page, UWGEmployeesProjects)
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        Try

            Dim BranchID As Integer = ddlBranche.SelectedValue
            Dim DepartmentID As Integer = ddlDepartment.SelectedValue
            Dim strFilter As String = String.Empty

            If BranchID = 0 Then
                strFilter &= " And A.BranchID in (" & IIf(CheckBranchPermission(DepartmentID) = "", "0", CheckBranchPermission(DepartmentID)) & ")"
            Else
                strFilter &= " And A.BranchID = " & BranchID
            End If

            If DepartmentID > 0 Then
                strFilter &= " And A.DepartmentID = " & DepartmentID
            End If

            strFilter &= " And dbo.fn_CheckEndOfService(A.ID)>0"

            If DdlVacationType.SelectedValue > 0 Then
                Dim clsFiscalPeriods As New Venus.Application.SystemFiles.System.Clssys_FiscalYearsPeriods(Page)
                Dim ExtraString = ""
                If txtCode.Text <> "" Then
                    ExtraString = " and A.Code = '" & txtCode.Text & "'"
                End If
                Dim clsCompanies As New Clssys_Companies(Page)
                clsCompanies.Find("ID=" & clsCompanies.MainCompanyID)
                Dim clsEmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                Dim strcommand As String
                If clsCompanies.IsHigry Then
                    strcommand = "set dateformat DMY; select (select top 1 ID from hrs_EmployeeVacationOpenBalance where EmployeeID = A.ID and VacationTypeID=" & DdlVacationType.SelectedValue & ") AS ID,A.ID AS EmployeeID,A.Code AS EmployeeCode," & _
                                                        "IsNull( A.EngName,'') + ',' + IsNull( A.FatherEngName,'') + ',' + IsNull( A.GrandEngName,'') + ',' + IsNull( A.FamilyEngName,'') As FullEngName," & _
                                                        "IsNull( A.ArbName,'') + ',' + IsNull( A.FatherArbName,'') + ',' + IsNull( A.GrandArbName,'') + ',' + IsNull( A.FamilyArbName,'') As FullArbName," & _
                                                        "(select top 1 [HBalanceDate] from hrs_EmployeeVacationOpenBalance where EmployeeID = A.ID and VacationTypeID=" & DdlVacationType.SelectedValue & ") AS PeriodID," & _
                                                        "isnull((select top 1 [Days] from hrs_EmployeeVacationOpenBalance where EmployeeID = A.ID and VacationTypeID=" & DdlVacationType.SelectedValue & "),0) AS [Days]," & _
                                                        "isnull((select top 1 OverDue from hrs_EmployeeVacationOpenBalance where EmployeeID = A.ID and VacationTypeID=" & DdlVacationType.SelectedValue & "),0) AS OverDue," & _
                                                        "isnull((select top 1 VacationBalance from hrs_EmployeeVacationOpenBalance where EmployeeID = A.ID and VacationTypeID=" & DdlVacationType.SelectedValue & "),0) AS VacationBalance" & _
                                                        " from   hrs_Employees A where A.CancelDate Is null" & ExtraString & strFilter & " order by Case When IsNumeric(A.Code) = 1 then Right(Replicate('0',51) + A.Code, 50) When IsNumeric(A.Code) = 0 then Left(A.Code + Replicate('',51), 50) Else A.Code End"
                Else
                    strcommand = "set dateformat DMY; select (select top 1 ID from hrs_EmployeeVacationOpenBalance where EmployeeID = A.ID and VacationTypeID=" & DdlVacationType.SelectedValue & ") AS ID,A.ID AS EmployeeID,A.Code AS EmployeeCode," & _
                                "IsNull( A.EngName,'') + ',' + IsNull( A.FatherEngName,'') + ',' + IsNull( A.GrandEngName,'') + ',' + IsNull( A.FamilyEngName,'') As FullEngName," & _
                                "IsNull( A.ArbName,'') + ',' + IsNull( A.FatherArbName,'') + ',' + IsNull( A.GrandArbName,'') + ',' + IsNull( A.FamilyArbName,'') As FullArbName," & _
                                "Convert(varchar(10),(select top 1 [GBalanceDate] from hrs_EmployeeVacationOpenBalance where EmployeeID = A.ID and VacationTypeID=" & DdlVacationType.SelectedValue & "),103) AS PeriodID," & _
                                "isnull((select top 1 [Days] from hrs_EmployeeVacationOpenBalance where EmployeeID = A.ID and VacationTypeID=" & DdlVacationType.SelectedValue & "),0) AS [Days]," & _
                                "isnull((select top 1 OverDue from hrs_EmployeeVacationOpenBalance where EmployeeID = A.ID and VacationTypeID=" & DdlVacationType.SelectedValue & "),0) AS OverDue," & _
                                "isnull((select top 1 VacationBalance from hrs_EmployeeVacationOpenBalance where EmployeeID = A.ID and VacationTypeID=" & DdlVacationType.SelectedValue & "),0) AS VacationBalance" & _
                                " from   hrs_Employees A where A.CancelDate Is null" & ExtraString & strFilter & " order by Case When IsNumeric(A.Code) = 1 then Right(Replicate('0',51) + A.Code, 50) When IsNumeric(A.Code) = 0 then Left(A.Code + Replicate('',51), 50) Else A.Code End"
                End If


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
                        If DdlVacationType.SelectedValue > 0 Then
                            Dim clsEmployeeVacationOpenBalance As New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                            Dim row As Infragistics.WebUI.UltraWebGrid.UltraGridRow
                            For intIndex = 0 To UWGEmployeesProjects.Rows.Count - 1
                                row = UWGEmployeesProjects.Rows(intIndex)
                                If row.Cells.FromKey("PeriodID").Value <> Nothing Then
                                    Dim clsemployeetransactions As New Clshrs_EmployeesTransactions(Me.Page)
                                    'If clsemployeetransactions.Find("EmployeeID = " & row.Cells.FromKey("EmployeeID").Value & " and PrepareType = 'V'") Then
                                    '    If UWGEmployeesProjects.Rows.Count = 1 Then
                                    '        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Unable To Set Vacation Open Balance After Enter Annual Vacation Payments Transactions/لا يمكن تعيين رصيد إفتتاحى بعد إدخال حركات تصفية مستحقات إجازة سنوية"))
                                    '        Continue For
                                    '    Else
                                    '        Continue For
                                    '    End If
                                    'End If
                                    Dim clsemployeevacations As New Clshrs_EmployeesVacations(Me.Page)
                                    'If clsemployeevacations.Find("EmployeeID = " & row.Cells.FromKey("EmployeeID").Value & " and VacationTypeID = " & DdlVacationType.SelectedValue) Then
                                    '    If UWGEmployeesProjects.Rows.Count = 1 Then
                                    '        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Unable To Set Vacation Open Balance After Enter Annual Vacation Transaction/لا يمكن تعيين رصيد إفتتاحى بعد إدخال حركة إجازات سنوية"))
                                    '        Continue For
                                    '    Else
                                    '        Continue For
                                    '    End If
                                    'End If
                                    If row.Cells.FromKey("ID").Value = Nothing Then
                                        clsEmployeeVacationOpenBalance = New Clshrs_EmployeeVacationOpenBalance(Me.Page)
                                        clsEmployeeVacationOpenBalance.EmployeeID = row.Cells.FromKey("EmployeeID").Value
                                        clsEmployeeVacationOpenBalance.VacationTypeID = DdlVacationType.SelectedValue
                                        Dim DatePer As Date = CDate(row.Cells.FromKey("PeriodID").Text).ToString("dd/MM/yyyy")

                                        If ClsDataAcessLayer.IsGreg(DatePer) Then
                                            clsEmployeeVacationOpenBalance.GBalanceDate = ClsDataAcessLayer.FormatGreg(DatePer, "dd/MM/yyyy")
                                        Else
                                            clsEmployeeVacationOpenBalance.GBalanceDate = ClsDataAcessLayer.HijriToGreg(DatePer, "dd/MM/yyyy")
                                        End If

                                        If ClsDataAcessLayer.IsHijri(DatePer) Then
                                            clsEmployeeVacationOpenBalance.HBalanceDate = ClsDataAcessLayer.FormatHijri(DatePer, "dd/MM/yyyy")
                                        Else
                                            clsEmployeeVacationOpenBalance.HBalanceDate = ClsDataAcessLayer.GregToHijri(DatePer, "dd/MM/yyyy")
                                        End If

                                        clsEmployeeVacationOpenBalance.Days = row.Cells.FromKey("Days").Value
                                        clsEmployeeVacationOpenBalance.OverDue = row.Cells.FromKey("OverDue").Value
                                        clsEmployeeVacationOpenBalance.VacationBalance = row.Cells.FromKey("VacationBalance").Value
                                        clsEmployeeVacationOpenBalance.RegDate = DateTime.Now
                                        clsEmployeeVacationOpenBalance.Save()
                                    Else
                                        Dim DatePer As Date = CDate(row.Cells.FromKey("PeriodID").Text).ToString("dd/MM/yyyy")

                                        clsEmployeeVacationOpenBalance.Find("ID=" & row.Cells.FromKey("ID").Value)
                                        clsEmployeeVacationOpenBalance.EmployeeID = row.Cells.FromKey("EmployeeID").Value
                                        clsEmployeeVacationOpenBalance.VacationTypeID = DdlVacationType.SelectedValue

                                        If ClsDataAcessLayer.IsGreg(DatePer) Then
                                            clsEmployeeVacationOpenBalance.GBalanceDate = ClsDataAcessLayer.FormatGreg(DatePer, "dd/MM/yyyy")
                                        Else
                                            clsEmployeeVacationOpenBalance.GBalanceDate = ClsDataAcessLayer.HijriToGreg(DatePer, "dd/MM/yyyy")
                                        End If

                                        If ClsDataAcessLayer.IsHijri(DatePer) Then
                                            clsEmployeeVacationOpenBalance.HBalanceDate = ClsDataAcessLayer.FormatHijri(DatePer, "dd/MM/yyyy")
                                        Else
                                            clsEmployeeVacationOpenBalance.HBalanceDate = ClsDataAcessLayer.GregToHijri(DatePer, "dd/MM/yyyy")
                                        End If

                                        clsEmployeeVacationOpenBalance.Days = row.Cells.FromKey("Days").Value
                                        clsEmployeeVacationOpenBalance.OverDue = row.Cells.FromKey("OverDue").Value
                                        clsEmployeeVacationOpenBalance.VacationBalance = row.Cells.FromKey("VacationBalance").Value
                                        clsEmployeeVacationOpenBalance.RegDate = DateTime.Now
                                        clsEmployeeVacationOpenBalance.Update("ID=" & row.Cells.FromKey("ID").Value)
                                    End If
                                End If
                            Next
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done!/تم الحفظ"))
                            UWGEmployeesProjects.DataSource = Nothing
                            UWGEmployeesProjects.DataBind()
                            txtCode.Text = String.Empty
                            DdlVacationType.SelectedIndex = 0
                        Else
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Must enter Vacation Type/يجب إختيار نوع أجازة"))
                        End If
                    Catch ex As Exception
                    End Try
                Case "Delete"
                    If txtCode.Text = "" Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Must Enter Employee Code/يجب إختيار كود الموظف"))
                        Return
                    End If

                    Dim clsEmp As New Clshrs_Employees(Page)
                    clsEmp.Find("Code='" & txtCode.Text & "'")
                    Dim strcommand As String = "Delete FROM hrs_EmployeeVacationOpenBalance Where EmployeeID=" & clsEmp.ID
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmp.ConnectionString, CommandType.Text, strcommand)
                    UWGEmployeesProjects.DataSource = Nothing
                    UWGEmployeesProjects.DataBind()
                    txtCode.Text = String.Empty
                    DdlVacationType.SelectedIndex = 0
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Delete Done/تم الحذف"))
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlVacationType.SelectedIndexChanged
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
