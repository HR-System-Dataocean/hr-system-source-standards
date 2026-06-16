Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Text.RegularExpressions

Partial Class frmPreparedPosting
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

            Dim clsSponsor As New Clshrs_Sponsors(Page)
            If ClsObjects.Find(" Code='" & clsSponsor.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TextBox_Sponsor.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & TextBox_Sponsor.ClientID & "'"
                    WebImageButton_Sponsor.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            Dim clsContracttype As New Clshrs_ContractTypes(Page)
            If ClsObjects.Find(" Code='" & clsContracttype.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TextBox_Contract.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & TextBox_Contract.ClientID & "'"
                    WebImageButton_Cont.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

        End If

        If Not IsPostBack Then
            ddlDepartment.Attributes.Add("OnChange", "ddlDepartment_Change()")
            Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
            Dim ClsSectors As New ClsBasicFiles(Me.Page, "sys_Sectors")

            ClsDepartment.GetDropDownList(ddlDepartment, True)
            ddlDepartment.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Departments]/ [ جميع الإدارات]")

            ClsSectors.GetDropDownList(ddlsector, True)
            ddlsector.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Sectors]/ [ جميع القطاعات]")


            clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
            ddlBranche.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Branches]/ [ جميع الفروع]")
            ddlBranche.SelectedIndex = 1
            ClsFisicalYearsPeriods.GetDropDownList(DdlPeriods, IntModuleId, True, "")

            IntSelectedPeriod = ClsFisicalYearsPeriods.GetLastOpenedFiscalPieriod(IntModuleId)

            UwgSearchEmployees.Columns.FromKey("FullName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))
            DdlPeriods.SelectedIndex = 0

            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)

        End If
    End Sub

    Protected Sub DdlPeriods_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlPeriods.SelectedIndexChanged, ddlsector.SelectedIndexChanged, ddlsector.SelectedIndexChanged, TextBox_Contract.TextChanged


        GetData()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub

    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Lock.Command, LinkButton_UnLock.Command, ImageButton_Lock.Command, ImageButton_UnLock.Command, LinkButtonGLPost.Command, ImageButton_GLPost.Command, ImageButton_GLPostReview.Command, LinkButtonGLPostReview.Command

        Try
            Dim StrMode As String = Request.QueryString.Item("SM")
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Lock"
                    Dim ClsEmployeeTransactions As New Clshrs_EmployeesTransactions(Page)
                    Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)
                    Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)

                    If Not ValidateJournalEmployeeFields(ClsEmployeeTransactions.ConnectionString, ClsEmployeeTransactions.MainCompanyID, objNav) Then
                        Return
                    End If

                    Dim StrSelectCommand As String = "Select isnull(Max(TransactionID),0) + 1 from hrs_HrsTrans"
                    Dim str As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, StrSelectCommand)
                    Dim FilterID As String = ddlFilter.SelectedValue

                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("PrepareType").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        ClsEmployeeTransactions.Find("ID=" & row.Cells.FromKey("ID").Value)
                        If ClsEmployeeTransactions.ID > 0 Then
                            If row.Cells(1).Value = True And row.Cells(5).Value = 0 Then
                                ClsEmployeeTransactions.PostDate = DateTime.Now
                                Dim clsContract As New Clshrs_Contracts(Page)
                                clsContract.Find("EmployeeID = " & ClsEmployeeTransactions.EmployeeID & " order by StartDate DESC")
                                Dim intContractID As Integer = 0
                                If clsContract.DataSet.Tables.Count > 0 Then
                                    If clsContract.DataSet.Tables(0).Rows.Count > 0 Then
                                        intContractID = clsContract.DataSet.Tables(0).Rows(0)("ID")
                                    End If
                                End If
                                clsContract.Find(" ID =" & intContractID)
                                If clsContract.ID > 0 Then
                                    Dim clsCurrency As New ClsSys_Currencies(Page)
                                    clsCurrency.Find("ID = " & clsContract.CurrencyId)

                                    Dim clsemployee As New Clshrs_Employees(Page)
                                    clsemployee.Find("ID = " & ClsEmployeeTransactions.EmployeeID)

                                    Dim StrCommand = "insert into hrs_HrsTrans select '" & str & "',hrs_EmployeesTransactions.RegDate,hrs_EmployeesTransactions.EmployeeID,hrs_EmployeesTransactions.FiscalYearPeriodID,hrs_EmployeesTransactionsProjects.ProjectID,hrs_EmployeesTransactionsProjects.WorkingUnits,hrs_Employees.BranchID,hrs_Employees.LocationID,hrs_Employees.DepartmentID," &
                                                                                       "hrs_Employees.SectorID,hrs_EmployeesTransactions.CCost1,hrs_EmployeesTransactions.CCost2,hrs_EmployeesTransactions.CCost3,hrs_EmployeesTransactions.CCost4,hrs_EmployeesTransactionsDetails.TransactionTypeID,hrs_EmployeesTransactionsDetails.NumericValue,hrs_EmployeesTransactionsDetails.NumericValue * " & clsCurrency.Amount & "," &
                                                                                       clsCurrency.ID & "," & clsCurrency.Amount & ",hrs_EmployeesTransactions.PrepareType,NULL,NULL,'" & IIf(String.IsNullOrEmpty(clsemployee.BankID), "Cash", "Exchange") & "'," & clsemployee.BankID & "," & ClsEmployeeTransactions.MainCompanyID & "," & ClsEmployeeTransactions.DataBaseUserRelatedID & "," &
                                                                                       ClsEmployeeTransactions.ID & ",GetDate(),NULL,'',hrs_Employees.NationalityID,(select top 1 ContractTypeID from hrs_Contracts where EmployeeID = hrs_Employees.ID order by StartDate DESC) as ContractTypeID from hrs_EmployeesTransactions as hrs_EmployeesTransactions left outer join hrs_EmployeesTransactionsProjects as hrs_EmployeesTransactionsProjects on hrs_EmployeesTransactions.ID = hrs_EmployeesTransactionsProjects.EmployeeTransactionID" &
                                                                                       " left outer join hrs_EmployeesTransactionsDetails as hrs_EmployeesTransactionsDetails on  hrs_EmployeesTransactionsProjects.ID = hrs_EmployeesTransactionsDetails.EmpTransProjID" &
                                                                                       " left outer join hrs_Employees on hrs_EmployeesTransactions.EmployeeID =  hrs_Employees.ID left outer join hrs_TransactionsTypes AS hrs_TransactionsTypes on hrs_EmployeesTransactionsDetails.TransactionTypeID = hrs_TransactionsTypes.ID where isnull(hrs_TransactionsTypes.IsAllowPosting,0) <> 1 and hrs_EmployeesTransactions.ID = " & ClsEmployeeTransactions.ID
                                    If FilterID <> "N" Then
                                        StrCommand = "insert into hrs_HrsTrans select '" & str & "',hrs_EmployeesTransactions.RegDate,hrs_EmployeesTransactions.EmployeeID,hrs_EmployeesTransactions.FiscalYearPeriodID,hrs_EmployeesTransactionsProjects.ProjectID,hrs_EmployeesTransactionsProjects.WorkingUnits,hrs_Employees.BranchID,hrs_Employees.LocationID,hrs_Employees.DepartmentID," &
                                                                                       "hrs_Employees.SectorID,ISNULL(hrs_Employees.Cost1,0),ISNULL(hrs_Employees.Cost2,0),ISNULL(hrs_Employees.Cost3,0),ISNULL(hrs_Employees.Cost4,0),ISNULL(hrs_EmployeesTransactionsDetails.TransactionTypeID,0),ISNULL(hrs_EmployeesTransactionsDetails.NumericValue,0),ISNULL(hrs_EmployeesTransactionsDetails.NumericValue,0) * " & clsCurrency.Amount & "," &
                                                                                       clsCurrency.ID & "," & clsCurrency.Amount & ",hrs_EmployeesTransactions.PrepareType,NULL,NULL,'" & IIf(String.IsNullOrEmpty(clsemployee.BankID), "Cash", "Exchange") & "'," & clsemployee.BankID & "," & ClsEmployeeTransactions.MainCompanyID & "," & ClsEmployeeTransactions.DataBaseUserRelatedID & "," &
                                                                                       ClsEmployeeTransactions.ID & ",GetDate(),NULL,'',hrs_Employees.NationalityID,(select top 1 ContractTypeID from hrs_Contracts where EmployeeID = hrs_Employees.ID order by StartDate DESC) as ContractTypeID from hrs_EmployeesTransactions as hrs_EmployeesTransactions left outer join hrs_EmployeesTransactionsProjects as hrs_EmployeesTransactionsProjects on hrs_EmployeesTransactions.ID = hrs_EmployeesTransactionsProjects.EmployeeTransactionID" &
                                                                                       " left outer join hrs_EmployeesTransactionsDetails as hrs_EmployeesTransactionsDetails on  hrs_EmployeesTransactionsProjects.ID = hrs_EmployeesTransactionsDetails.EmpTransProjID" &
                                                                                       " left outer join hrs_Employees on hrs_EmployeesTransactions.EmployeeID =  hrs_Employees.ID left outer join hrs_TransactionsTypes AS hrs_TransactionsTypes on hrs_EmployeesTransactionsDetails.TransactionTypeID = hrs_TransactionsTypes.ID where isnull(hrs_TransactionsTypes.IsAllowPosting,0) <> 1 and hrs_EmployeesTransactions.ID = " & ClsEmployeeTransactions.ID

                                    End If
                                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, StrCommand)
                                    ClsEmployeeTransactions.Update("ID=" & row.Cells.FromKey("ID").Value)
                                End If
                            End If
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Lock Done!/تم الإغلاق"))
                Case "UnLock"
                    Dim ClsEmployeeTransactions As New Clshrs_EmployeesTransactions(Page)
                    Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)
                    Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)

                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("PrepareType").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        ClsEmployeeTransactions.Find("ID=" & row.Cells.FromKey("ID").Value)
                        If ClsEmployeeTransactions.ID > 0 Then
                            If row.Cells(1).Value = True And row.Cells(6).Value = 0 Then
                                Dim StrCommand = "Delete from hrs_HrsTrans where RegComputerID =" & ClsEmployeeTransactions.ID
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, StrCommand)
                                StrCommand = "update hrs_EmployeesTransactions set PostDate = null where ID =" & ClsEmployeeTransactions.ID
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, StrCommand)
                            End If
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "UnLock Done!/تم التحرير"))
                Case "GLPostReview"
                    Dim ClsEmployeeTransactions As New Clshrs_EmployeesTransactions(Page)
                    Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)
                    Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)

                    Dim strSystem = "Select UseCostCenter,MinimumCostCentersCount from sys_SystemConfig"
                    Dim dsActions As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployeeTransactions.ConnectionString, CommandType.Text, strSystem)
                    If dsActions.Tables(0).Rows.Count > 0 Then
                        If CBool(dsActions.Tables(0).Rows(0).Item("UseCostCenter")) Then
                            If CInt(dsActions.Tables(0).Rows(0).Item("MinimumCostCentersCount")) > 0 Then
                                Dim costCount As Integer = CInt(dsActions.Tables(0).Rows(0).Item("MinimumCostCentersCount"))
                                Dim Criteria As String = " "
                                For index = 1 To costCount
                                    Criteria += "  Cost" & index & " IS NULL OR"
                                Next

                                Criteria = Criteria.Remove(Criteria.Length - 2)
                                Dim stremp = "Select Code from hrs_Employees join hrs_Contracts  on hrs_Employees.id =hrs_Contracts.EmployeeID  Where (hrs_Contracts.EndDate is null or hrs_Contracts.EndDate>GETDATE()) and ( " & Criteria & ")"
                                Dim dsEmps As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployeeTransactions.ConnectionString, CommandType.Text, stremp)
                                If dsEmps.Tables(0).Rows.Count > 0 Then
                                    Dim empCodes As New List(Of String)
                                    empCodes.Add("Employee Code")
                                    For Each dr As DataRow In dsEmps.Tables(0).Rows
                                        empCodes.Add(dr("Code").ToString())
                                    Next

                                    Dim fileName As String = "MissingCostCenters_" & Now.Ticks.ToString() & ".txt"
                                    Dim filePath As String = Server.MapPath("~/tempReports/" & fileName)

                                    System.IO.File.WriteAllLines(filePath, empCodes)

                                    Dim downloadUrl As String = ResolveUrl("/tempReports/" & fileName)
                                    Dim message As String = objNav.SetLanguage(Page, "There are employees who do not have enough Cost Centters/يوجد موظفين ليس لديهم العدد الكافي من مراكز التكلفة")

                                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, message)
                                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, downloadUrl, 800, 600, False,
                    Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "DownloadWindow", False, True, False, False, False, False, False, False, False)
                                    Return
                                End If
                            End If
                        End If
                    End If




                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPostJournalPreview.aspx?PeriodId=" & DdlPeriods.SelectedValue & "", 1500, 1200, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
                Case "GLPost"
                    Dim ClsEmployeeTransactions As New Clshrs_EmployeesTransactions(Page)
                    Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)
                    Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeeTransactions.ConnectionString)

                    Dim StrIDArray As String = "0"
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("PrepareType").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        ClsEmployeeTransactions.Find("ID=" & row.Cells.FromKey("ID").Value)
                        If ClsEmployeeTransactions.ID > 0 Then
                            If row.Cells(1).Value = True And row.Cells(5).Value = 1 And row.Cells(6).Value = 0 Then
                                StrIDArray = StrIDArray & "," & ClsEmployeeTransactions.ID
                            End If
                        End If
                    Next
                    Dim StrCommand As String = "Select distinct TransactionID from hrs_HrsTrans where RegComputerID in (" & StrIDArray & ")"
                    Dim dsPatches As New Data.DataSet
                    dsPatches = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, StrCommand)

                    Dim FilterID As String = ddlFilter.SelectedValue



                    If FilterID <> "A" Then
                        If FilterID = "N" Then
                            For I As Integer = 0 To dsPatches.Tables(0).Rows.Count - 1
                                Dim ExecProfile As String = ""
                                If ConfigurationManager.AppSettings("PostingStatus") = "0" Then
                                    ExecProfile = " Exec SPhrs_Posting " & dsPatches.Tables(0).Rows(I)(0) & "," & ddlFilter.SelectedValue
                                Else

                                    ExecProfile = "Declare @Err	varchar(max)" &
                                         " Declare @ExecStatus	bit = 0" &
                                         " Exec fcs_InsertProfile " & dsPatches.Tables(0).Rows(I)(0)
                                End If
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, ExecProfile)
                            Next
                        End If
                        If FilterID = "V" Then
                            For I As Integer = 0 To dsPatches.Tables(0).Rows.Count - 1
                                Dim ExecProfile As String = ""
                                If ConfigurationManager.AppSettings("PostingStatus") = "0" Then
                                    ExecProfile = " Exec SPhrs_Posting_V " & dsPatches.Tables(0).Rows(I)(0) & "," & ddlFilter.SelectedValue
                                Else
                                    ExecProfile = "Declare @Err	varchar(max)" &
                                         " Declare @ExecStatus	bit = 0" &
                                         " Exec fcs_InsertProfile_V " & dsPatches.Tables(0).Rows(I)(0)
                                End If
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, ExecProfile)
                            Next
                        End If
                        If FilterID = "E" Then
                            For I As Integer = 0 To dsPatches.Tables(0).Rows.Count - 1
                                Dim ExecProfile As String = ""
                                If ConfigurationManager.AppSettings("PostingStatus") = "0" Then
                                    ExecProfile = " Exec SPhrs_Posting_E " & dsPatches.Tables(0).Rows(I)(0) & "," & ddlFilter.SelectedValue
                                Else
                                    ExecProfile = "Declare @Err	varchar(max)" &
                                         " Declare @ExecStatus	bit = 0" &
                                         " Exec fcs_InsertProfile_E " & dsPatches.Tables(0).Rows(I)(0)
                                End If
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, ExecProfile)
                            Next
                        End If
                        If FilterID = "L" Then
                            For I As Integer = 0 To dsPatches.Tables(0).Rows.Count - 1
                                Dim ExecProfile As String = ""
                                If ConfigurationManager.AppSettings("PostingStatus") = "0" Then
                                    ExecProfile = " Exec SPhrs_Posting_L " & dsPatches.Tables(0).Rows(I)(0) & "," & ddlFilter.SelectedValue
                                Else
                                    ExecProfile = "Declare @Err	varchar(max)" &
                                         " Declare @ExecStatus	bit = 0" &
                                         " Exec fcs_InsertProfile_L " & dsPatches.Tables(0).Rows(I)(0)
                                End If
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, ExecProfile)
                            Next
                        End If
                    Else
                        For I As Integer = 0 To dsPatches.Tables(0).Rows.Count - 1
                            Dim ExecProfile As String = ""
                            If ConfigurationManager.AppSettings("PostingStatus") = "0" Then
                                ExecProfile = " Exec SPhrs_Posting " & dsPatches.Tables(0).Rows(I)(0) & "," & ddlFilter.SelectedValue
                            Else

                                'ExecProfile = "Declare @Err	varchar(max)" & _
                                '              " Declare @ExecStatus	bit = 0" & _
                                '              " Exec fcs_InsertProfile 'Site\Pages\Hr',0,0, 0," & dsPatches.Tables(0).Rows(I)(0) & ",@Err output , @ExecStatus output" & _
                                '              " Select @Err ,@ExecStatus"

                                ExecProfile = "Declare @Err	varchar(max)" &
                                             " Declare @ExecStatus	bit = 0" &
                                             " Exec fcs_InsertProfile " & dsPatches.Tables(0).Rows(I)(0)
                            End If
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, ExecProfile)
                        Next
                    End If
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Post Done!/تم الترحيل"))
            End Select
            GetData()
        Catch ex As Exception
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ex.Message)
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
            Dim SectorID As Integer = ddlsector.SelectedValue
            Dim FilterID As String = ddlFilter.SelectedValue
            Dim sponsor As String = TextBox_Sponsor.Text
            UwgSearchEmployees.DataSource = Nothing
            UwgSearchEmployees.DataBind()

            If DdlPeriods.SelectedIndex = 0 Then
                Return False
            End If

            ClsFisicalPeriods.Find("ID=" & DdlPeriods.SelectedValue)

            Dim strFilter As String = " And e.Code like '%" & txtCode.Text & "%'"

            If BranchID > 0 Then
                strFilter &= " And e.BranchID = " & BranchID
        
            End If

            If DepartmentID > 0 Then
                strFilter &= " And e.DepartmentID = " & DepartmentID
            End If
            If SectorID > 0 Then
                strFilter &= " And e.SectorID = " & SectorID
            End If

            If Not String.IsNullOrWhiteSpace(sponsor) Then
                'strFilter &= " And e.SponsorID in (select ID from hrs_Sponsors where Code = '" & TextBox_Sponsor.Text & "') "
                strFilter &= " And e.SponsorID in (select ID from hrs_Sponsors where Code in (" & TextBox_Sponsor.Text & ")) "
            End If

            If TextBox_Contract.Text <> "" Then
                strFilter &= " and e.ID in (select EmployeeID from hrs_Contracts where ContractTypeID in (select ID from hrs_ContractsTypes where Code in (" & TextBox_Contract.Text & "))) "
            End If
            If FilterID <> "A" Then
                If FilterID = "N" Then
                    strFilter &= " And  et.PrepareType ='N' "
                End If
                If FilterID = "V" Then
                    strFilter &= " AND ( ( et.PrepareType = 'V' AND et.EmployeesVacationsID IS NULL ) OR ( et.PrepareType = 'ET' AND EXISTS ( SELECT 1 FROM hrs_EmployeesTransactions t WHERE t.ID = et.REGCOMPUTERID AND t.PrepareType = 'V' AND t.EmployeesVacationsID IS NULL ) ) "
                    strFilter &= " OR ( et.PrepareType IN ('V','NV') AND et.EmployeesVacationsID IS NOT NULL AND EXISTS ( SELECT 1 FROM hrs_EmployeesTransactions t WHERE t.PrepareType = 'NV' AND t.EmployeesVacationsID = et.EmployeesVacationsID ) ) "
                    strFilter &= " OR ( et.PrepareType = 'ET'  AND EXISTS ( SELECT 1 FROM hrs_EmployeesTransactions emt WHERE emt.ID = et.REGCOMPUTERID AND emt.PrepareType = 'V' AND emt.EmployeesVacationsID IS NOT NULL AND EXISTS ( SELECT 1 FROM hrs_EmployeesTransactions nv WHERE nv.PrepareType = 'NV' AND nv.EmployeesVacationsID = emt.EmployeesVacationsID )))) "
                End If
                If FilterID = "E" Then
                    strFilter &= " And  (et.PrepareType in('E','EN','EV','EL') or ( et.PrepareType = 'ET' AND EXISTS ( SELECT 1 FROM hrs_EmployeesTransactions t WHERE t.ID = et.REGCOMPUTERID AND t.PrepareType = 'E'  ) ) ) "
                End If
                If FilterID = "L" Then
                    strFilter &= " And  et.PrepareType in('L','LP') AND EXISTS (SELECT 1 FROM hrs_EmployeesTransactionsProjects etp INNER JOIN hrs_EmployeesTransactionsDetails etd ON etd.EmpTransProjID = etp.ID WHERE etp.EmployeeTransactionID = et.ID AND etd.TransactionTypeID = 185 )"
                End If
            End If

            Dim strCommand As String
            strCommand = " select et.ID,et.EmployeeID,et.PrepareType,e.Code as Code," & "dbo.fn_GetEmpName(e.Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName,(select isnull(count(hrs_EmployeesTransactions.ID),0) from hrs_EmployeesTransactions where hrs_EmployeesTransactions.ID = et.ID and hrs_EmployeesTransactions.PostDate is not null) as Locked,(select isnull(count(hrs_HrsTrans.ID),0) from hrs_HrsTrans where hrs_HrsTrans.RegComputerID = et.ID and hrs_HrsTrans.ProfileHeadID is not null) as GLPosted  from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID where e.CancelDate is null and  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID
            strCommand &= strFilter & " order by e.code"
            'If FilterID = "V" Then
            '    Dim strfilter2 As String = " And e.Code like '%" & txtCode.Text & "%'"

            '    If BranchID > 0 Then
            '        strfilter2 &= " And e.BranchID = " & BranchID
            '    End If

            '    If DepartmentID > 0 Then
            '        strfilter2 &= " And e.DepartmentID = " & DepartmentID
            '    End If
            '    strfilter2 &= " and et.id in(select RegComputerID  from hrs_EmployeesTransactions where PrepareType='V' and FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " )"
            '    strCommand &= " union all select et.ID,et.EmployeeID,et.PrepareType,e.Code as Code," & "dbo.fn_GetEmpName(e.Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName,(select isnull(count(hrs_EmployeesTransactions.ID),0) from hrs_EmployeesTransactions where hrs_EmployeesTransactions.ID = et.ID and hrs_EmployeesTransactions.PostDate is not null) as Locked,(select isnull(count(hrs_HrsTrans.ID),0) from hrs_HrsTrans where hrs_HrsTrans.RegComputerID = et.ID and hrs_HrsTrans.ProfileHeadID is not null) as GLPosted  from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID
            '    strCommand &= strfilter2
            '  End If
            Dim dsEmployee As New Data.DataSet
            dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, strCommand)


            If dsEmployee.Tables(0).Rows.Count > 0 Then
                Dim ds As New Data.DataSet
                ds.Tables.Add()
                ds.Tables(0).Columns.Add("PrepareType")
                ds.Tables(0).Rows.Add(ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل"))
                ds.Tables(0).Merge(dsEmployee.Tables(0))

                UwgSearchEmployees.DataSource = ds.Tables(0)
                UwgSearchEmployees.DataBind()
            End If

            ddlDepartment.SelectedValue = DepartmentID
            ddlsector.SelectedValue = SectorID

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

    Private Function GetValidJournalEmployeeFields(ByVal journalFields As String) As List(Of String)
        Dim fields As New List(Of String)
        If String.IsNullOrWhiteSpace(journalFields) Then
            Return fields
        End If

        For Each part As String In journalFields.Split(","c)
            Dim fieldName As String = part.Trim()
            If Not String.IsNullOrEmpty(fieldName) AndAlso Regex.IsMatch(fieldName, "^[a-zA-Z_][a-zA-Z0-9_]*$") Then
                fields.Add(fieldName)
            End If
        Next

        Return fields
    End Function

    Private Function ValidateJournalEmployeeFields(ByVal connectionString As String, ByVal companyId As Integer, ByVal objNav As Venus.Shared.Web.NavigationHandler) As Boolean
        Try
            Dim strSystem As String = "Select ShowPostingNotification, JournalEmployeeFields from sys_SystemConfig where CompanyId=" & companyId
            Dim dsConfig As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString, CommandType.Text, strSystem)
            If dsConfig.Tables(0).Rows.Count = 0 Then
                Return True
            End If

            Dim configRow As DataRow = dsConfig.Tables(0).Rows(0)
            If IsDBNull(configRow("ShowPostingNotification")) OrElse Not CBool(configRow("ShowPostingNotification")) Then
                Return True
            End If

            Dim journalFields As String = String.Empty
            If Not IsDBNull(configRow("JournalEmployeeFields")) Then
                journalFields = configRow("JournalEmployeeFields").ToString().Trim()
            End If
            If String.IsNullOrWhiteSpace(journalFields) Then
                Return True
            End If

            Dim fields As List(Of String) = GetValidJournalEmployeeFields(journalFields)
            If fields.Count = 0 Then
                Return True
            End If

            Dim emptyFieldCriteria As New System.Text.StringBuilder()
            Dim emptyFieldList As New System.Text.StringBuilder()
            For Each fieldName As String In fields
                emptyFieldCriteria.Append(" (ISNULL(LTRIM(RTRIM(CAST(e.[" & fieldName & "] AS nvarchar(max)))), '') = '') OR")
                emptyFieldList.Append(" CASE WHEN ISNULL(LTRIM(RTRIM(CAST(e.[" & fieldName & "] AS nvarchar(max)))), '') = '' THEN ', " & fieldName & "' ELSE '' END +")
            Next

            Dim criteria As String = emptyFieldCriteria.ToString().Remove(emptyFieldCriteria.Length - 2)
            Dim fieldListSql As String = emptyFieldList.ToString().Remove(emptyFieldList.Length - 1)
            Dim strEmployees As String = "Select e.Code, STUFF(" & fieldListSql & ", 1, 2, '') AS EmptyFields " &
                "from hrs_Employees e where e.CancelDate is null and (" & criteria & ") order by e.Code"

            Dim dsEmps As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connectionString, CommandType.Text, strEmployees)
            If dsEmps.Tables(0).Rows.Count = 0 Then
                Return True
            End If

            Dim reportLines As New List(Of String)
            reportLines.Add("Code" & vbTab & "Empty Fields")
            For Each dr As DataRow In dsEmps.Tables(0).Rows
                reportLines.Add(dr("Code").ToString() & vbTab & dr("EmptyFields").ToString())
            Next

            Dim fileName As String = "MissingJournalEmployeeFields_" & Now.Ticks.ToString() & ".txt"
            Dim filePath As String = Server.MapPath("~/tempReports/" & fileName)
            System.IO.File.WriteAllLines(filePath, reportLines, New System.Text.UTF8Encoding(True))

            Dim downloadUrl As String = ResolveUrl("/tempReports/" & fileName)
            Dim message As String = objNav.SetLanguage(Page, "There are employees with empty journal employee fields/يوجد موظفين لديهم حقول يومية فارغة")
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, message)
            Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, downloadUrl, 800, 600, False,
                Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "DownloadWindow", False, True, False, False, False, False, False, False, False)
            Return False
        Catch ex As Exception
            Return True
        End Try
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
            Dim dsSector As New Data.DataSet
            Dim StrSelectCommand As String
            Dim StrSelectSector As String
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

            'StrSelectSector = " Select s.ID, s." & strFieldName & " From sys_Sectors s Inner Join sys_SectorsDepartments sd ON sd.sectorID=s.id Where s.ID IN (" & strDeptID & ") "
            'dsSector = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr, Data.CommandType.Text, StrSelectSector)
            ''ClsSector.GetDropDownList(, True, "ID in (select SectorID from sys_SectorsDepartments where Checked = 1 and DepartmentID = " & ddlDepartment.SelectedValue & ")")

        Catch ex As Exception

        End Try
    End Function

#End Region

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetData()
    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartment.SelectedIndexChanged
        If ddlDepartment.SelectedValue <> 0 Then
            Dim ClsSector As New ClsSys_Sectors(Me.Page)
            ClsSector.GetDropDownList(ddlsector, True, "ID in (select SectorID from sys_SectorsDepartments where Checked = 1 and DepartmentID = " & ddlDepartment.SelectedValue & ")")
            ddlDepartment.Focus()
        End If
    End Sub

    Protected Sub ddlFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilter.SelectedIndexChanged
        GetData()
    End Sub
    Protected Sub ddlBranche_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranche.SelectedIndexChanged
        GetData()
    End Sub
End Class
