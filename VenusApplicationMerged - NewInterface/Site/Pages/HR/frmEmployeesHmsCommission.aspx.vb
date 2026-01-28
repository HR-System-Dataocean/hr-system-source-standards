Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesSelector
    Inherits MainPage

    Const CPreparedData_TotalPenalty = 7

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim clsEmployee As New Clshrs_Employees(Page)

        Dim clsVacationTypes As New Venus.Application.SystemFiles.HumanResource.Clshrs_VacationsTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployee.ConnectionString)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim clsMainCurrency As New ClsSys_Currencies(Page)

        Dim clsObjects As New Clssys_Objects(Page)
        Dim clsSearch As New Clssys_Searchs(Page)
        Dim ClsForms As New ClsSys_Forms(Page)

        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim ClsHmsCommissionCategories As New Clshrs_HmsCommissionCategories(Page)
        Dim IntModuleId As Integer = GetModuleID("frmEmployeesHmsCommission")
        Dim IntCurrentFiscalPeriod As Integer = 0

        Session("hdnFiscalDays") = hdnFiscalDays.Value
        If Not IsPostBack Then
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            'If ClsObjects.Find(" Code='" & clsEmployee.Table.Trim & "'") Then
            '    If clsSearch.Find(" ObjectID='" & clsObjects.ID & "'") Then
            '        Dim IntDimension As Integer = 510
            '        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & clsSearch.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
            '        btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            '    End If
            'End If

            Page.Session.Add("ConnectionString", clsEmployee.ConnectionString)
            Page.Session.Add("Log", ObjNavigationHandler.SetLanguage(Page, "Eng/Arb"))
            'UWGEmployeesProjects.Columns(3).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "FullEngName/FullArbName")


            ClsFisicalYearsPeriods.GetDropDownList(DdlPeriods, IntModuleId, True, "")
            ClsHmsCommissionCategories.GetDropDownList(DdlCommissionCat, True, "IsLineCommission =1")

            txtLang.Value = ObjNavigationHandler.SetLanguage(Page, "Eng/Arb")
            Page.Session.Add("Lage", ObjNavigationHandler.SetLanguage(Page, "0/1"))
            Page.Session.Add("ConnectionString", clsEmployee.ConnectionString)
        End If
        Venus.Shared.Web.ClientSideActions.GridSelection(Page, UWGEmployeesCommission)
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub
    Public Sub GetData()
        Try
            Dim ClsHmsCommissionCategories As New Clshrs_HmsCommissionCategories(Page)
            Dim CommissionCatID As Integer = DdlCommissionCat.SelectedValue
            ClsHmsCommissionCategories.Find("ID = " & CommissionCatID)
            Label_Title1.Text = ClsHmsCommissionCategories.Remarks
            Label_Title1.Visible = True
            Dim PeriodID As Integer = DdlPeriods.SelectedValue
            Dim strFilter As String = String.Empty
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsHmsCommissionCategories.ConnectionString)
            Dim FromDate As DateTime = txtFromdate.Text
            Dim ToDate As DateTime = txtTodate.Text

            Dim ClsFiscalPerid As New Clssys_FiscalYearsPeriods(Page)
            ClsFiscalPerid.Find("ID = " & PeriodID)
            If CommissionCatID = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please select Commision catagery!/برجاء إختيار فئة العمولة"))

                Return
            End If
            Dim StrSelectCommand As String = "set dateformat dmy  exec sp_Call_rptCustom_NAHADADoctorIncomeByGroup '','" & ClsHmsCommissionCategories.Code & "','" & FromDate.ToString("yyyyMMdd") & "','" & ToDate.ToString("yyyyMMdd") & "','" & ClsFiscalPerid.EngName & "'"

            Dim DsEmp As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsHmsCommissionCategories.ConnectionString, Data.CommandType.Text, StrSelectCommand)
            UWGEmployeesCommission.Rows.Clear()
            If DsEmp.Tables(0).Rows.Count > 0 Then
                Dim ds As New Data.DataSet
                ds.Tables.Add()
                ds.Tables(0).Columns.Add("Code")
                ds.Tables(0).Rows.Add(ObjNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل"))
                ds.Tables(0).Merge(DsEmp.Tables(0))

                UWGEmployeesCommission.DataSource = ds.Tables(0)
                UWGEmployeesCommission.DataBind()
            End If





        Catch ex As Exception
        End Try
    End Sub
    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Delete.Command, LinkButton_Save.Command, ImageButton_Delete.Command, ImageButton_Save.Command
        Dim clsProjects As New Clshrs_Projects(Page, "hrs_Projects")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim clsFicalPeriod As New Clssys_FiscalYearsPeriods(Page)
        Dim clsCommissionTransfer As New Clshrs_HmsCommissionTransfer(Page)
        Dim CommissionCatageryID = DdlCommissionCat.SelectedValue
        clsFicalPeriod.Find("ID = " & DdlPeriods.SelectedValue)
        Dim FiscalPerisId As Integer = clsFicalPeriod.ID
        Dim ClsEmployeesTransaction As New Clshrs_EmployeesTransactions(Page)
        Try
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Save"
                    Try


                        If FiscalPerisId = 0 Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "please select fiscal period/برجاء اختيار الفترة المالية"))
                            Return
                        End If
                        Dim Employeeid As Integer = 0
                        Dim Amount As Double
                        Dim Deduct1 As Double
                        Dim Deduction2 As Double
                        Dim NetAmount As Double
                        Dim CommissionPc As Double
                        Dim DueAmount As Double

                        Dim row As Infragistics.WebUI.UltraWebGrid.UltraGridRow
                        For intIndex = 0 To UWGEmployeesCommission.Rows.Count - 1
                            clsCommissionTransfer = New Clshrs_HmsCommissionTransfer(Page)
                            row = UWGEmployeesCommission.Rows(intIndex)
                            If row.Cells.FromKey("selected").Value Then


                                ClsEmployee.Find("code= '" & row.Cells.FromKey("EmployeeCode").Value & "'")
                                Employeeid = ClsEmployee.ID
                                clsCommissionTransfer.Find("CommissionCatageryID = " & CommissionCatageryID & " and FiscalPeridID= " & FiscalPerisId & " and  EmployeeId = " & Employeeid)
                                If clsCommissionTransfer.ID > 0 Then
                                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done!/الموظف لديه عمولة مسجلة لنفس الشهر  " & ClsEmployee.FullName))
                                    Return

                                End If

                                Amount = row.Cells.FromKey("TotalCash").Value
                                Deduct1 = row.Cells.FromKey("Deduct1").Value
                                Deduction2 = row.Cells.FromKey("Deduction2").Value
                                NetAmount = row.Cells.FromKey("NetAmount").Value
                                CommissionPc = row.Cells.FromKey("CommissionPc").Value
                                DueAmount = row.Cells.FromKey("DueAmount").Value
                                If DueAmount > 0 And Employeeid > 0 And FiscalPerisId > 0 Then
                                    clsCommissionTransfer = New Clshrs_HmsCommissionTransfer(Page)
                                    clsCommissionTransfer.FiscalPeridID = FiscalPerisId
                                    clsCommissionTransfer.CommissionCatageryID = CommissionCatageryID
                                    clsCommissionTransfer.EmployeeId = Employeeid
                                    clsCommissionTransfer.Amount = Amount
                                    clsCommissionTransfer.Dedcution1 = Deduct1
                                    clsCommissionTransfer.Deduction2 = Deduction2
                                    clsCommissionTransfer.NetAmount = NetAmount
                                    clsCommissionTransfer.CommissionPc = CommissionPc
                                    clsCommissionTransfer.DueAmount = DueAmount
                                    clsCommissionTransfer.Save()
                                End If
                            End If
                        Next
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done!/تم الحفظ"))
                        UWGEmployeesCommission.DataSource = Nothing
                        UWGEmployeesCommission.DataBind()



                    Catch ex As Exception

                    End Try
                Case "Delete"
                    Dim row As Infragistics.WebUI.UltraWebGrid.UltraGridRow
                    Dim str As String = ""
                    For intIndex = 0 To UWGEmployeesCommission.Rows.Count - 1
                        row = UWGEmployeesCommission.Rows(intIndex)
                        ClsEmployee.Find("code= '" & row.Cells.FromKey("EmployeeCode").Value & "'")

                        If row.Cells.FromKey("selected").Value And ClsEmployee.ID > 0 Then
                            ClsEmployeesTransaction = New Clshrs_EmployeesTransactions(Page)
                            ClsEmployeesTransaction.Find("PrepareType = 'N' and FiscalYearPeriodID = '" & DdlPeriods.SelectedValue & "' and EmployeeID = " & row.Cells.FromKey("Employeeid").Value)
                            If ClsEmployeesTransaction.DataSet.Tables(0).Rows.Count > 0 Then
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "you have to refund the salary before refund the commission to !/يجب الغاء الراتب للموظف قبل الغاء العمولة  " & ClsEmployee.FullName))
                                Return
                            End If

                            If row.Cells.FromKey("TransferrdID").Value > 0 Then
                                clsCommissionTransfer.Find("ID = " & row.Cells.FromKey("TransferrdID").Value)
                                str &= "delete from hrs_hmsCommissionTransfer where id=" & clsCommissionTransfer.ID & ";"
                            End If
                        End If
                    Next
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsCommissionTransfer.ConnectionString, Data.CommandType.Text, str)
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done!/تم التحرير"))

                    GetData()

            End Select
        Catch ex As Exception

        End Try
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
        If intNoOfrows > UWGEmployeesCommission.Rows.Count Then
            For i As Integer = 0 To (intNoOfrows - UWGEmployeesCommission.Rows.Count)
                UWGEmployeesCommission.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow())
            Next
        End If
    End Sub

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
    Protected Sub UWGEmployeesCommission_InitializeRow(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UWGEmployeesCommission.InitializeRow
        If e.Row.Cells.FromKey("DueAmount").Value < 0 Then
            e.Row.Style.BackColor = System.Drawing.Color.Red
        End If
    End Sub
End Class
