Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmSendingSalaryNotification
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
    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Prepare.Command, ImageButton_Prepare.Command
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
                        If row.Cells(1).Value <> True Then
                            Continue For
                        End If
                        Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
                        ClsFisicalPeriods.Find("ID=" & DdlPeriods.SelectedValue)
                        Dim dsSalary As New Data.DataSet
                        dsSalary = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployeeTransactions.ConnectionString, Data.CommandType.Text, "Exec hrs_rptPayRegisterOfficial '" & row.Cells.FromKey("Code").Value & "','','','','" & ClsFisicalPeriods.EngName & "','','','','','','','',''")
                        If dsSalary.Tables(0).Rows.Count > 0 Then
                            Dim ClsEmployee As New Clshrs_Employees(Page)
                            If ClsEmployee.Find("Code = '" & row.Cells.FromKey("Code").Value & "'") Then
                                If ClsEmployee.E_Mail <> "" Then
                                    Dim SMTPHost As String = ConfigurationManager.AppSettings("SMTPHost").ToString()
                                    Dim SMTPPort As String = ConfigurationManager.AppSettings("SMTPPort").ToString()
                                    Dim SMTPUsername As String = ConfigurationManager.AppSettings("SMTPUsername").ToString()
                                    Dim SMTPPassword As String = ConfigurationManager.AppSettings("SMTPPassword").ToString()
                                    Dim SMTPFrom As String = ConfigurationManager.AppSettings("SMTPFrom").ToString()
                                    Dim smtp As New System.Net.Mail.SmtpClient()
                                    Dim message As New System.Net.Mail.MailMessage()
                                    Dim FromAddress As New System.Net.Mail.MailAddress(SMTPFrom, "إشعارات الموارد البشرية")
                                    smtp.Host = SMTPHost
                                    smtp.Port = SMTPPort
                                    Dim cred As New System.Net.NetworkCredential(SMTPUsername, SMTPPassword)
                                    message.From = FromAddress
                                    message.To.Clear()
                                    message.To.Add(ClsEmployee.E_Mail)
                                    message.Subject = "إشعار صرف راتب"
                                    message.Body = "<table style=""width: 100%; vertical-align: top; font-family: Arial;"" cellspacing=""0"">"
                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top""></td><td style=""width: 60%; vertical-align: top; text-align: center;border-bottom: 1px solid black"">إشعار صرف راتب</td><td style=""width: 20%; vertical-align: top""></td></tr>"
                                    message.Body = message.Body & "<tr><td colspan=""3""></td></tr>"
                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("EmployeesCode").ToString() & "</td><td style=""width: 20%; vertical-align: top"">الرقم الوظيفى</td></tr>"
                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("EmployeeArbName").ToString() & "</td><td style=""width: 20%; vertical-align: top"">الإسم</td></tr>"
                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("DepartmentArbName").ToString() & "</td><td style=""width: 20%; vertical-align: top"">القسم</td></tr>"
                                    message.Body = message.Body & "<tr><td colspan=""3""></td></tr>"

                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("FinancialWorkingUnits").ToString() & "</td><td style=""width: 20%; vertical-align: top"">أيام الدوام</td></tr>"
                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("TotalPackage").ToString() & "</td><td style=""width: 20%; vertical-align: top"">إجمالى الراتب</td></tr>"
                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("BasicSalary").ToString() & "</td><td style=""width: 20%; vertical-align: top"">الراتب الأساسى</td></tr>"
                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("TotalBenefits").ToString() & "</td><td style=""width: 20%; vertical-align: top"">إجمالى المستحق</td></tr>"
                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("LateDeduction").ToString() & "</td><td style=""width: 20%; vertical-align: top"">خصم التأخيرات</td></tr>"
                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("LonsDeduction").ToString() & "</td><td style=""width: 20%; vertical-align: top"">خصم السلف</td></tr>"
                                    If dsSalary.Tables(0).Rows(0)("SocialIsurance") > 0 Then
                                        message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("SocialIsurance").ToString() & "</td><td style=""width: 20%; vertical-align: top"">خصم التأمينات</td></tr>"
                                    End If
                                    message.Body = message.Body & "<tr><td style=""width: 100%; vertical-align: top; text-align: right; float: right;border-top: 1px solid black"" colspan=""3""></td></tr>"

                                    message.Body = message.Body & "<tr><td style=""width: 20%; vertical-align: top; text-align: right; float: right;""></td><td style=""width: 60%; vertical-align: top; text-align: center;"">" & dsSalary.Tables(0).Rows(0)("NetSalary").ToString() & "</td><td style=""width: 20%; vertical-align: top"">صافى الراتب</td></tr>"
                                    message.Body = message.Body & "<tr><td colspan=""3""></td></tr>"
                                    message.Body = message.Body & "<tr><td style=""width: 100%; vertical-align: top; text-align: right; float: right;border-top: 1px solid black"" colspan=""3"">تم إيداع صافى الراتب فى حسابكم البنكى . مع تحيات إدارة التطوير المؤسسى</td></tr>"
                                    message.Body = message.Body & "<tr><td style=""width: 100%; vertical-align: top; text-align: right; float: right;"" colspan=""3"">إخلاء مسئولية: إن المعلومات الواردة في هذه الرسالة الالكترونية وفي الملفات المرفقة معها قد تحتوي على مواد خاصة أو سرية موجهة إلى المُرسَـل إليه فقط، ويعد الحصول على هذا البريد الالكتروني من قٌبل أي شخص آخر عملا غير قانونيا، وفي حال تلقي هذه الرسالة بالخطأ يرجى حذف المواد المُرسَـلة والاتصال بالمُرسِـل فورا، علما بأنه يمنع منعا باتا إفشاء أو نسخ أو توزيع أيا من المواد المُرسَلة أو اتخاذ أي إجراء باستخدامها كما أن البيانات والآراء الموضحة في هذا البريد الالكتروني تعبر فقط عن رأي المُرسِـل ولا تعكس بالضرورة أراء جمعية ماجد بن عبدالعزيز للتنمية والخدمات الإجتماعية</td></tr>"
                                    message.Body = message.Body & "</table>"

                                    message.BodyEncoding = System.Text.Encoding.Unicode

                                    message.IsBodyHtml = True
                                    smtp.UseDefaultCredentials = False
                                    smtp.EnableSsl = False
                                    smtp.Credentials = cred
                                    smtp.Send(message)
                                End If
                            End If
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Send Done!/تم الإرسال"))
            End Select
            GetData()
        Catch ex As Exception
        End Try
    End Sub
    Private clsMainCurrency As ClsSys_Currencies
    Private ClsCountries As Clssys_Countries
    Private Function Get_NoDecimalPlaces() As Integer
        Try
            clsMainCurrency = New ClsSys_Currencies(Page)
            ClsCountries = New Clssys_Countries(Page)
            If ClsCountries.Find(" IsMainCountries = 1 ") Then
                clsMainCurrency.Find(" ID=" & ClsCountries.CurrencyID)
                If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                    Return clsMainCurrency.NoDecimalPlaces
                Else
                    Return 2
                End If
            End If
        Catch ex As Exception
            Return 2
        End Try
    End Function

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

            Dim strFilter As String = " And e.Code like '%" & txtCode.Text & "%' And dbo.fn_CheckEndOfService(e.ID)>0"

            If BranchID > 0 Then
                strFilter &= " And e.BranchID = " & BranchID
            End If

            If DepartmentID > 0 Then
                strFilter &= " And e.DepartmentID = " & DepartmentID
            End If

            Dim strCommand As String
            strCommand = "Set DateFormat DMY; select et.ID,et.EmployeeID ,e.Code as Code," & "dbo.fn_GetEmpName(e.Code," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS FullName, dbo.fn_GetNetSalary(e.ID," & ClsFisicalPeriods.ID & ") AS NetSalary from hrs_EmployeesTransactions et inner join hrs_Employees e on e.ID = et.EmployeeID INNER JOIN sys_FiscalYearsPeriodsModules as m ON m.FiscalYearPeriodID=et.FiscalYearPeriodID where  m.ModuleID=" & GetModuleID("frmPrepareSalaries") & " And IsNull(m.CloseDate,'')='' And et.FiscalYearPeriodID=" & ClsFisicalPeriods.ID & " And et.PrepareType ='N' "

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
