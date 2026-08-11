Imports System.Data
Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System

Partial Class frmRetroactiveSalaryReview
    Inherits MainPage

    Private Const RetroSrc As Integer = 6

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
            SetPageDirection()

            If Not IsPostBack Then
                Dim currentPeriodId As Integer = 0
                Integer.TryParse(Convert.ToString(Request.QueryString("PeriodID")), currentPeriodId)
                If currentPeriodId <= 0 Then
                    ShowMessage(GetNav().SetLanguage(Page, "Current period is required./الفترة الحالية مطلوبة."), True)
                    btnSaveProcess.Enabled = False
                    Return
                End If

                hdnCurrentPeriodID.Value = currentPeriodId.ToString()
                hdnSelectedEmpIDs.Value = ""
                ApplyLabels()
                BindFilterLists()
                ResolvePeriodsAndLoad()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, True)
        End Try
    End Sub

    Private Function GetNav() As Venus.Shared.Web.NavigationHandler
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Return New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
    End Function

    Private Function ConnStr() As String
        If Session("ConnectionString") IsNot Nothing Then
            Return Session("ConnectionString").ToString()
        End If
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Return ClsEmployee.ConnectionString
    End Function

    Private Function IsArabic() As Boolean
        Try
            If ProfileCls.CurrentLanguage = "Ar" Then Return True
        Catch
        End Try
        Try
            If Session("Lage") IsNot Nothing AndAlso Session("Lage").ToString() = "1" Then Return True
        Catch
        End Try
        Return False
    End Function

    Private Sub SetPageDirection()
        Try
            DIV.Attributes("dir") = If(IsArabic(), "rtl", "ltr")
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "pageDir",
                "document.documentElement.dir='" & If(IsArabic(), "rtl", "ltr") & "';document.body.dir='" & If(IsArabic(), "rtl", "ltr") & "';", True)
        Catch
        End Try
    End Sub

    Private Sub ApplyLabels()
        Dim nav As Venus.Shared.Web.NavigationHandler = GetNav()
        lblTitle.Text = nav.SetLanguage(Page, "Retroactive Processing of Unprepared Salaries/معالجة الرواتب غير المجهزة بأثر رجعي")
        lblAccrualCaption.Text = nav.SetLanguage(Page, "Accrual Period/فترة الاستحقاق")
        lblPaymentCaption.Text = nav.SetLanguage(Page, "Current Payment Period/فترة الصرف الحالية")
        lblUnpreparedCaption.Text = nav.SetLanguage(Page, "Unprepared Employees/موظفين غير مجهزين")
        txtSearch.Attributes("placeholder") = nav.SetLanguage(Page, "Search by employee code or name/بحث بالكود أو الاسم")
        btnFilter.Text = nav.SetLanguage(Page, "Filter/تصفية")
        btnSaveProcess.Text = "✓ " & nav.SetLanguage(Page, "Save and Process/حفظ ومعالجة")
        btnCancel.Text = nav.SetLanguage(Page, "Cancel and Return/إلغاء والعودة")
        lblEmpty.Text = nav.SetLanguage(Page, "No unprepared employees found for the previous period./لا يوجد موظفون غير مجهزين في الفترة السابقة.")
    End Sub

    Private Sub BindFilterLists()
        Dim nav As Venus.Shared.Web.NavigationHandler = GetNav()
        Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
        ClsDepartment.GetDropDownList(ddlDepartment, True)
        If ddlDepartment.Items.Count > 0 Then
            ddlDepartment.Items(0).Text = nav.SetLanguage(Page, "All Departments/[ جميع الإدارات]")
        End If

        FillLookupDropdown(ddlSponsor, "SELECT ID, CASE WHEN " & If(IsArabic(), "1", "0") & "=1 THEN ArbName ELSE EngName END AS Name FROM hrs_Sponsors WHERE ISNULL(CancelDate,'')='' ORDER BY Name",
                           nav.SetLanguage(Page, "All Sponsors/[ جميع الكفلاء]"))
        FillLookupDropdown(ddlContractType, "SELECT ID, CASE WHEN " & If(IsArabic(), "1", "0") & "=1 THEN ArbName ELSE EngName END AS Name FROM hrs_ContractsTypes WHERE ISNULL(CancelDate,'')='' ORDER BY Name",
                           nav.SetLanguage(Page, "All Contract Types/[ جميع أنواع التعاقد]"))
    End Sub

    Private Sub FillLookupDropdown(ByVal ddl As DropDownList, ByVal sql As String, ByVal allText As String)
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem(allText, "0"))
        Try
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr(), CommandType.Text, sql)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    ddl.Items.Add(New ListItem(Convert.ToString(row("Name")), Convert.ToString(row("ID"))))
                Next
            End If
        Catch
        End Try
    End Sub

    Private Function GetPreviousPeriodId(ByVal currentPeriodId As Integer) As Integer
        Dim sql As String =
            "SELECT TOP 1 ID FROM sys_FiscalYearsPeriods " &
            "WHERE ISNULL(CancelDate,'') = '' " &
            "AND FromDate < (SELECT FromDate FROM sys_FiscalYearsPeriods WHERE ID = " & currentPeriodId & ") " &
            "ORDER BY FromDate DESC"
        Dim obj As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr(), CommandType.Text, sql)
        If obj IsNot Nothing AndAlso Not IsDBNull(obj) Then
            Return CInt(obj)
        End If
        Return 0
    End Function

    Private Function GetPeriodName(ByVal periodId As Integer) As String
        Dim fp As New Clssys_FiscalYearsPeriods(Page)
        If fp.Find("ID=" & periodId) Then
            Return If(IsArabic(), fp.ArbName, fp.EngName)
        End If
        Return "—"
    End Function

    Private Sub ResolvePeriodsAndLoad()
        Dim currentPeriodId As Integer = CInt(hdnCurrentPeriodID.Value)
        Dim accrualPeriodId As Integer = GetPreviousPeriodId(currentPeriodId)
        hdnAccrualPeriodID.Value = accrualPeriodId.ToString()

        lblPaymentPeriod.Text = GetPeriodName(currentPeriodId)
        If accrualPeriodId <= 0 Then
            lblAccrualPeriod.Text = "—"
            lblInfoBanner.Text = GetNav().SetLanguage(Page, "No previous period was found for the selected payment period./لا توجد فترة سابقة للفترة المحددة.")
            pnlEmpty.Visible = True
            btnSaveProcess.Enabled = False
            Return
        End If

        lblAccrualPeriod.Text = GetPeriodName(accrualPeriodId)
        Dim nav As Venus.Shared.Web.NavigationHandler = GetNav()
        lblInfoBanner.Text = nav.SetLanguage(Page,
            "This window shows eligible employees from " & lblAccrualPeriod.Text &
            " who were not prepared. They can be processed in " & lblPaymentPeriod.Text &
            " using separate retroactive payroll items, or permanently excluded. No period earlier than " &
            lblAccrualPeriod.Text & " is displayed./" &
            "تعرض هذه النافذة الموظفين المؤهلين من " & lblAccrualPeriod.Text &
            " الذين لم يتم تجهيزهم. يمكن معالجتهم في " & lblPaymentPeriod.Text &
            " عبر بنود رواتب بأثر رجعي، أو استبعادهم نهائياً. لا تعرض فترات أقدم من " &
            lblAccrualPeriod.Text & ".")

        BindEmployees()

        If Not HasActiveRetroMappings() Then
            ShowMessage(GetMissingRetroMappingMessage(), True)
        End If
    End Sub

    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As EventArgs)
        BindEmployees()
    End Sub

    Private Sub BindEmployees()
        Dim dt As DataTable = LoadUnpreparedEmployees()
        lblEmpty.Text = GetNav().SetLanguage(Page,
            "No unprepared employees found for the previous period./لا يوجد موظفون غير مجهزين في الفترة السابقة.")

        lblUnpreparedCount.Text = dt.Rows.Count.ToString()

        rptEmployees.DataSource = dt
        rptEmployees.DataBind()
        pnlEmpty.Visible = (dt.Rows.Count = 0)
        btnSaveProcess.Enabled = (dt.Rows.Count > 0)
    End Sub

    Private Function LoadUnpreparedEmployees() As DataTable
        Dim currentPeriodId As Integer = CInt(hdnCurrentPeriodID.Value)
        Dim accrualPeriodId As Integer = CInt(Val(hdnAccrualPeriodID.Value))
        Dim lang As Integer = If(IsArabic(), 1, 0)
        Dim deptName As String = If(IsArabic(), "d.ArbName", "d.EngName")
        Dim sponsorName As String = If(IsArabic(), "s.ArbName", "s.EngName")
        Dim contractName As String = If(IsArabic(), "ct.ArbName", "ct.EngName")
        Dim txnName As String = If(IsArabic(), "ArbName", "EngName")
        Dim retroSuffix As String = If(IsArabic(), " - أثر رجعي", " - Retroactive")

        Dim sql As New System.Text.StringBuilder()
        sql.AppendLine("SET DATEFORMAT DMY;")
        sql.AppendLine("SELECT e.ID AS EmployeeID, e.Code AS EmployeeCode,")
        sql.AppendLine(" dbo.fn_GetEmpName(e.Code," & lang & ") AS EmployeeName,")
        sql.AppendLine(" ISNULL(" & deptName & ",'') AS DepartmentName,")
        sql.AppendLine(" ISNULL(" & sponsorName & ",'') AS SponsorName,")
        sql.AppendLine(" ISNULL(" & contractName & ",'') AS ContractTypeName,")
        sql.AppendLine(" ISNULL((SELECT TOP 1 retro.Code")
        sql.AppendLine("   FROM hrs_TransactionsTypes orig")
        sql.AppendLine("   INNER JOIN hrs_RetroTransactionMapping m ON m.TransactionTypeID = orig.ID")
        sql.AppendLine("   INNER JOIN hrs_TransactionsTypes retro ON retro.ID = m.RetroTransactionTypeID")
        sql.AppendLine("   WHERE ISNULL(orig.IsBasicSalary,0)=1 AND ISNULL(orig.CancelDate,'')=''")
        sql.AppendLine("     AND ISNULL(m.IsActive,0)=1 AND ISNULL(m.RetroTransactionTypeID,0)>0")
        sql.AppendLine("     AND ISNULL(retro.CancelDate,'')=''), '') AS TransactionCode,")
        sql.AppendLine(" ISNULL((SELECT TOP 1 retro." & txnName)
        sql.AppendLine("   FROM hrs_TransactionsTypes orig")
        sql.AppendLine("   INNER JOIN hrs_RetroTransactionMapping m ON m.TransactionTypeID = orig.ID")
        sql.AppendLine("   INNER JOIN hrs_TransactionsTypes retro ON retro.ID = m.RetroTransactionTypeID")
        sql.AppendLine("   WHERE ISNULL(orig.IsBasicSalary,0)=1 AND ISNULL(orig.CancelDate,'')=''")
        sql.AppendLine("     AND ISNULL(m.IsActive,0)=1 AND ISNULL(m.RetroTransactionTypeID,0)>0")
        sql.AppendLine("     AND ISNULL(retro.CancelDate,'')=''), '') + N'" & retroSuffix.Replace("'", "''") & "' AS PayrollItemName,")
        sql.AppendLine(" CAST(ISNULL((")
        sql.AppendLine("   SELECT TOP 1 dbo.fn_GetBasicSalary(hc.ID, CONVERT(varchar(10), fp.ToDate, 103))")
        sql.AppendLine("   FROM hrs_Contracts hc")
        sql.AppendLine("   WHERE hc.EmployeeID = e.ID AND hc.StartDate <= fp.ToDate")
        sql.AppendLine("     AND (hc.EndDate IS NULL OR fp.ToDate BETWEEN hc.StartDate AND hc.EndDate)")
        sql.AppendLine("     AND ISNULL(hc.CancelDate,'') = ''")
        sql.AppendLine("   ORDER BY hc.StartDate DESC")
        sql.AppendLine(" ),0) AS decimal(18,2)) AS Amount")
        sql.AppendLine("FROM hrs_Employees e")
        sql.AppendLine("INNER JOIN sys_FiscalYearsPeriods fp ON fp.ID = " & accrualPeriodId)
        sql.AppendLine("LEFT JOIN sys_Departments d ON d.ID = e.DepartmentID")
        sql.AppendLine("LEFT JOIN hrs_Sponsors s ON s.ID = e.SponsorID")
        sql.AppendLine("OUTER APPLY (")
        sql.AppendLine("  SELECT TOP 1 ContractTypeID FROM hrs_Contracts")
        sql.AppendLine("  WHERE EmployeeID = e.ID AND ISNULL(CancelDate,'') = ''")
        sql.AppendLine("  ORDER BY StartDate DESC")
        sql.AppendLine(") lc")
        sql.AppendLine("LEFT JOIN hrs_ContractsTypes ct ON ct.ID = lc.ContractTypeID")
        sql.AppendLine("WHERE ISNULL(e.RegComputerID,0) = 0")
        sql.AppendLine(" AND dbo.fn_CheckEndOfServiceByPeriod(e.ID," & accrualPeriodId & ") > 0")
        sql.AppendLine(" AND NOT EXISTS (")
        sql.AppendLine("   SELECT 1 FROM hrs_EmployeesTransactions t")
        sql.AppendLine("   WHERE t.EmployeeID = e.ID AND t.FiscalYearPeriodID = " & accrualPeriodId & " AND t.PrepareType = 'N'")
        sql.AppendLine(" )")
        sql.AppendLine(" AND NOT EXISTS (")
        sql.AppendLine("   SELECT 1 FROM hrs_RetroactiveSalaryExclusions x")
        sql.AppendLine("   WHERE x.EmployeeID = e.ID AND x.AccrualPeriodID = " & accrualPeriodId)
        sql.AppendLine(" )")
        sql.AppendLine(" AND NOT EXISTS (")
        sql.AppendLine("   SELECT 1 FROM hrs_EmployeeExtraItems ei")
        sql.AppendLine("   WHERE ei.EmployeeCode = e.Code AND ei.FiscalPeriodID = " & currentPeriodId)
        sql.AppendLine("     AND ei.Src = " & RetroSrc & " AND ei.TransactionNo = '" & accrualPeriodId & "'")
        sql.AppendLine(" )")

        Dim search As String = txtSearch.Text.Trim().Replace("'", "''")
        If search <> "" Then
            sql.AppendLine(" AND (e.Code LIKE '%" & search & "%' OR dbo.fn_GetEmpName(e.Code," & lang & ") LIKE N'%" & search & "%')")
        End If
        If ddlDepartment.SelectedValue <> "" AndAlso Val(ddlDepartment.SelectedValue) > 0 Then
            sql.AppendLine(" AND e.DepartmentID = " & Val(ddlDepartment.SelectedValue))
        End If
        If ddlSponsor.SelectedValue <> "" AndAlso Val(ddlSponsor.SelectedValue) > 0 Then
            sql.AppendLine(" AND e.SponsorID = " & Val(ddlSponsor.SelectedValue))
        End If
        If ddlContractType.SelectedValue <> "" AndAlso Val(ddlContractType.SelectedValue) > 0 Then
            sql.AppendLine(" AND lc.ContractTypeID = " & Val(ddlContractType.SelectedValue))
        End If
        sql.AppendLine(" ORDER BY e.Code")

        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr(), CommandType.Text, sql.ToString())
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        End If
        Return New DataTable()
    End Function

    Protected Sub rptEmployees_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        Dim nav As Venus.Shared.Web.NavigationHandler = GetNav()

        If e.Item.ItemType = ListItemType.Header Then
            SetHeaderLiteral(e.Item, "litNo", nav.SetLanguage(Page, "No./الرقم"))
            SetHeaderLiteral(e.Item, "litCode", nav.SetLanguage(Page, "Employee Code/كود الموظف"))
            SetHeaderLiteral(e.Item, "litDept", nav.SetLanguage(Page, "Department/الإدارة"))
            SetHeaderLiteral(e.Item, "litSponsor", nav.SetLanguage(Page, "Sponsor/الكفيل"))
            SetHeaderLiteral(e.Item, "litContract", nav.SetLanguage(Page, "Contract Type/نوع العقد"))
            SetHeaderLiteral(e.Item, "litItem", nav.SetLanguage(Page, "Retroactive Payroll Item/بند الراتب بأثر رجعي"))
            SetHeaderLiteral(e.Item, "litAction", nav.SetLanguage(Page, "Action/الإجراء"))
            SetHeaderLiteral(e.Item, "litReason", nav.SetLanguage(Page, "Exclusion Reason/سبب الاستبعاد"))
            Return
        End If

        If e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem Then Return
        Dim ddlAction As DropDownList = TryCast(e.Item.FindControl("ddlAction"), DropDownList)
        Dim txtReason As TextBox = TryCast(e.Item.FindControl("txtExclusionReason"), TextBox)
        If ddlAction IsNot Nothing Then
            ddlAction.Items.Clear()
            ddlAction.Items.Add(New ListItem(nav.SetLanguage(Page, "Prepare Retroactively/تجهيز بأثر رجعي"), "1"))
            ddlAction.Items.Add(New ListItem(nav.SetLanguage(Page, "Permanently Exclude/استبعاد نهائي"), "2"))
            ddlAction.Attributes("onchange") = "syncExclusionReason(this); return true;"
        End If
        If txtReason IsNot Nothing Then
            txtReason.Attributes("placeholder") = nav.SetLanguage(Page, "Exclusion reason is required/سبب الاستبعاد مطلوب")
            ' IMPORTANT: do NOT set TextBox.ReadOnly = True — ASP.NET ignores posted values for ReadOnly textboxes.
            ' Client-side readonly attribute is used only for UX locking.
            txtReason.ReadOnly = False
            txtReason.Enabled = True
            txtReason.Attributes("readonly") = "readonly"
            txtReason.Style("background-color") = "#f0f3f7"
            txtReason.Style("cursor") = "not-allowed"
        End If
    End Sub

    Private Sub SetHeaderLiteral(ByVal item As RepeaterItem, ByVal controlId As String, ByVal text As String)
        Dim lit As Literal = TryCast(item.FindControl(controlId), Literal)
        If lit IsNot Nothing Then lit.Text = text
    End Sub

    Private Function GetPostedExclusionReason(ByVal item As RepeaterItem) As String
        Dim hdnReason As HiddenField = TryCast(item.FindControl("hdnReasonPosted"), HiddenField)
        If hdnReason IsNot Nothing AndAlso Not String.IsNullOrEmpty(hdnReason.Value) Then
            Return hdnReason.Value.Trim()
        End If
        Try
            If hdnReason IsNot Nothing Then
                Dim postedHdn As String = Request.Form(hdnReason.UniqueID)
                If Not String.IsNullOrEmpty(postedHdn) Then Return postedHdn.Trim()
            End If
        Catch
        End Try

        Dim txtReason As TextBox = TryCast(item.FindControl("txtExclusionReason"), TextBox)
        If txtReason Is Nothing Then Return ""
        Dim reason As String = If(txtReason.Text, "").Trim()
        If reason <> "" Then Return reason
        Try
            Dim posted As String = Request.Form(txtReason.UniqueID)
            If posted IsNot Nothing Then Return posted.Trim()
        Catch
        End Try
        Return ""
    End Function

    Private Sub EnsureExclusionTable()
        Dim sql As String =
            "IF OBJECT_ID(N'dbo.hrs_RetroactiveSalaryExclusions', N'U') IS NULL " &
            "CREATE TABLE dbo.hrs_RetroactiveSalaryExclusions (" &
            "ID int IDENTITY(1,1) NOT NULL, " &
            "EmployeeID int NOT NULL, " &
            "AccrualPeriodID int NOT NULL, " &
            "PaymentPeriodID int NULL, " &
            "ExclusionReason nvarchar(500) NOT NULL, " &
            "RegUserID int NULL, " &
            "RegDate datetime NOT NULL CONSTRAINT DF_hrs_RetroactiveSalaryExclusions_RegDate DEFAULT(GETDATE()), " &
            "CONSTRAINT PK_hrs_RetroactiveSalaryExclusions PRIMARY KEY CLUSTERED (ID), " &
            "CONSTRAINT UQ_hrs_RetroactiveSalaryExclusions UNIQUE (EmployeeID, AccrualPeriodID))"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnStr(), CommandType.Text, sql)
    End Sub

    Private Function ResolveUserId() As Integer
        Try
            If Session("UserID") IsNot Nothing AndAlso IsNumeric(Session("UserID")) Then
                Return CInt(Session("UserID"))
            End If
        Catch
        End Try
        Try
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            Dim user As String = String.Empty
            WebHandler.GetCookies(Page, "UserID", user)
            Dim id As Integer = 0
            Integer.TryParse(user, id)
            Return id
        Catch
        End Try
        Return 0
    End Function

    Private Function HasActiveRetroMappings() As Boolean
        Try
            Dim sql As String =
                "IF OBJECT_ID(N'dbo.hrs_RetroTransactionMapping', N'U') IS NULL SELECT 0 ELSE " &
                "SELECT COUNT(1) FROM hrs_RetroTransactionMapping m " &
                "INNER JOIN hrs_TransactionsTypes retro ON retro.ID = m.RetroTransactionTypeID " &
                "WHERE ISNULL(m.IsActive,0)=1 " &
                "AND ISNULL(m.RetroTransactionTypeID,0)>0 " &
                "AND ISNULL(retro.CancelDate,'')=''"
            Dim obj As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr(), CommandType.Text, sql)
            If obj IsNot Nothing AndAlso Not IsDBNull(obj) Then
                Return CInt(obj) > 0
            End If
        Catch
        End Try
        Return False
    End Function

    Private Function GetMissingRetroMappingMessage() As String
        Return GetNav().SetLanguage(Page,
            "Please configure and activate retro difference items first from (Linking differences items retrospectively), then try again./يجب إعداد وربط بنود الفروقات بأثر رجعي أولاً من شاشة (ربط بنود الفروقات بأثر رجعي) وتفعيلها، ثم أعد المحاولة.")
    End Function

    Protected Sub btnSaveProcess_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim currentPeriodId As Integer = CInt(Val(hdnCurrentPeriodID.Value))
            Dim accrualPeriodId As Integer = CInt(Val(hdnAccrualPeriodID.Value))
            If currentPeriodId <= 0 OrElse accrualPeriodId <= 0 Then
                ShowMessage(GetNav().SetLanguage(Page, "Period information is missing. Close and reopen this window./بيانات الفترة غير موجودة. أغلق النافذة وأعد فتحها."), True)
                Return
            End If

            EnsureExclusionTable()

            Dim preparedCount As Integer = 0
            Dim excludedCount As Integer = 0
            Dim skippedCount As Integer = 0
            Dim nav As Venus.Shared.Web.NavigationHandler = GetNav()
            Dim uploadDate As String = DateTime.Now.ToString("dd/MM/yyyy")
            Dim userId As Integer = ResolveUserId()

            If rptEmployees.Items.Count = 0 Then
                ShowMessage(nav.SetLanguage(Page, "No employees to process. Please reopen the window./لا يوجد موظفون للمعالجة. أعد فتح النافذة."), True)
                Return
            End If

            ' Detect if any row is set to Prepare Retroactively
            Dim hasPrepareAction As Boolean = False
            For Each item As RepeaterItem In rptEmployees.Items
                If item.ItemType <> ListItemType.Item AndAlso item.ItemType <> ListItemType.AlternatingItem Then Continue For
                Dim ddlAction As DropDownList = TryCast(item.FindControl("ddlAction"), DropDownList)
                If ddlAction Is Nothing Then Continue For
                Dim actionValue As String = ddlAction.SelectedValue
                Try
                    Dim postedAction As String = Request.Form(ddlAction.UniqueID)
                    If Not String.IsNullOrEmpty(postedAction) Then actionValue = postedAction
                Catch
                End Try
                If actionValue = "1" Then
                    hasPrepareAction = True
                    Exit For
                End If
            Next

            If hasPrepareAction AndAlso Not HasActiveRetroMappings() Then
                ShowMessage(GetMissingRetroMappingMessage(), True)
                Return
            End If

            For Each item As RepeaterItem In rptEmployees.Items
                If item.ItemType <> ListItemType.Item AndAlso item.ItemType <> ListItemType.AlternatingItem Then Continue For

                Dim hdnEmpID As HiddenField = TryCast(item.FindControl("hdnEmpID"), HiddenField)
                Dim hdnEmpCode As HiddenField = TryCast(item.FindControl("hdnEmpCode"), HiddenField)
                Dim hdnAmount As HiddenField = TryCast(item.FindControl("hdnAmount"), HiddenField)
                Dim hdnTxnCode As HiddenField = TryCast(item.FindControl("hdnTxnCode"), HiddenField)
                Dim ddlAction As DropDownList = TryCast(item.FindControl("ddlAction"), DropDownList)
                If hdnEmpID Is Nothing OrElse ddlAction Is Nothing Then Continue For

                Dim empId As Integer = CInt(Val(hdnEmpID.Value))
                If empId <= 0 Then Continue For
                Dim empCode As String = If(hdnEmpCode Is Nothing, "", hdnEmpCode.Value).Replace("'", "''")
                Dim actionValue As String = ddlAction.SelectedValue
                Try
                    Dim postedAction As String = Request.Form(ddlAction.UniqueID)
                    If Not String.IsNullOrEmpty(postedAction) Then actionValue = postedAction
                Catch
                End Try

                If actionValue = "2" Then
                    Dim reason As String = GetPostedExclusionReason(item)
                    If reason = "" Then
                        ShowMessage(nav.SetLanguage(Page, "Exclusion reason is required for permanently excluded employees./سبب الاستبعاد مطلوب للموظفين المستبعدين."), True)
                        Return
                    End If
                    SaveExclusion(empId, accrualPeriodId, currentPeriodId, reason, userId)
                    excludedCount += 1
                Else
                    Dim fallbackAmount As Decimal = 0D
                    Dim fallbackTxn As String = ""
                    If hdnAmount IsNot Nothing Then Decimal.TryParse(Convert.ToString(hdnAmount.Value), fallbackAmount)
                    If hdnTxnCode IsNot Nothing Then fallbackTxn = Convert.ToString(hdnTxnCode.Value)
                    Dim inserted As Integer = InsertRetroactiveExtraItems(empId, empCode, accrualPeriodId, currentPeriodId, uploadDate, fallbackAmount, fallbackTxn)
                    If inserted > 0 Then
                        preparedCount += 1
                    Else
                        skippedCount += 1
                    End If
                End If
            Next

            If preparedCount = 0 AndAlso excludedCount = 0 Then
                If skippedCount > 0 Then
                    If Not HasActiveRetroMappings() Then
                        ShowMessage(GetMissingRetroMappingMessage(), True)
                    Else
                        ShowMessage(nav.SetLanguage(Page,
                            "No changes were saved. " & skippedCount & " employee(s) had no payable mapped amount./لم يتم حفظ أي تغييرات. " & skippedCount & " موظف/موظفين بدون مبلغ مستحق مربوط."), True)
                    End If
                Else
                    ShowMessage(nav.SetLanguage(Page, "No changes were saved./لم يتم حفظ أي تغييرات."), True)
                End If
                Return
            End If

            ShowMessage(nav.SetLanguage(Page,
                "Processed successfully. Prepared: " & preparedCount & ", Excluded: " & excludedCount &
                "./تمت المعالجة بنجاح. تجهيز: " & preparedCount & "، استبعاد: " & excludedCount & "."), False)

            ClientScript.RegisterStartupScript(Me.GetType(), "closeAfterSave",
                "setTimeout(function(){ closePopup(false); }, 400);", True)
            BindEmployees()
        Catch ex As Exception
            ShowMessage(ex.Message, True)
        End Try
    End Sub

    Private Sub SaveExclusion(ByVal empId As Integer, ByVal accrualPeriodId As Integer, ByVal paymentPeriodId As Integer, ByVal reason As String, ByVal userId As Integer)
        Dim sql As String =
            "IF NOT EXISTS (SELECT 1 FROM hrs_RetroactiveSalaryExclusions WHERE EmployeeID=" & empId & " AND AccrualPeriodID=" & accrualPeriodId & ") " &
            "INSERT INTO hrs_RetroactiveSalaryExclusions (EmployeeID, AccrualPeriodID, PaymentPeriodID, ExclusionReason, RegUserID, RegDate) " &
            "VALUES (" & empId & "," & accrualPeriodId & "," & paymentPeriodId & ",N'" & reason.Replace("'", "''") & "'," & userId & ",GETDATE())"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnStr(), CommandType.Text, sql)
    End Sub

    Private Function InsertRetroactiveExtraItems(ByVal empId As Integer, ByVal empCode As String, ByVal accrualPeriodId As Integer, ByVal paymentPeriodId As Integer, ByVal uploadDate As String, ByVal fallbackAmount As Decimal, ByVal fallbackTxnCode As String) As Integer
        Dim inserted As Integer = 0
        Dim delSql As String =
            "DELETE FROM hrs_EmployeeExtraItems WHERE EmployeeCode='" & empCode & "' AND FiscalPeriodID=" & paymentPeriodId &
            " AND Src=" & RetroSrc & " AND TransactionNo='" & accrualPeriodId & "'"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnStr(), CommandType.Text, delSql)

        Dim fp As New Clssys_FiscalYearsPeriods(Page)
        fp.Find("ID=" & accrualPeriodId)
        Dim asOfDate As String = fp.ToDate.ToString("dd/MM/yyyy")

        Dim ClsContracts As New Clshrs_Contracts(Page)
        Dim contractId As Integer = ClsContracts.ContractValidatoinId(empId, fp.ToDate)
        If contractId > 0 Then
            ' Use mapped retro transaction Code from hrs_RetroTransactionMapping.RetroTransactionTypeID
            Dim sqlItems As String =
                "SET DATEFORMAT DMY; " &
                "SELECT ISNULL(retro.Code, '') AS TransactionCode, ISNULL(ct.Amount,0) AS Amount " &
                "FROM hrs_ContractsTransactions ct " &
                "INNER JOIN hrs_TransactionsTypes orig ON orig.ID = ct.TransactionTypeID " &
                "INNER JOIN hrs_RetroTransactionMapping m ON m.TransactionTypeID = orig.ID " &
                "INNER JOIN hrs_TransactionsTypes retro ON retro.ID = m.RetroTransactionTypeID " &
                "WHERE ct.ContractID = " & contractId &
                " AND ISNULL(ct.Active,0)=1 " &
                " AND ISNULL(m.IsActive,0)=1 " &
                " AND ISNULL(m.RetroTransactionTypeID,0) > 0 " &
                " AND ISNULL(orig.IsPaid,0)=1 " &
                " AND ISNULL(orig.IsProjectRelatedItem,0)=0 " &
                " AND ISNULL(orig.CancelDate,'')='' " &
                " AND ISNULL(retro.CancelDate,'')='' " &
                " AND ISNULL(ct.CancelDate,'')='' " &
                " AND ISNULL(orig.Sign,1) > 0"

            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr(), CommandType.Text, sqlItems)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    Dim txnCode As String = Convert.ToString(row("TransactionCode")).Replace("'", "''")
                    Dim amount As Decimal = 0D
                    Decimal.TryParse(Convert.ToString(row("Amount")), amount)
                    If amount <= 0D OrElse txnCode = "" Then Continue For
                    If InsertExtraItemRow(empCode, txnCode, amount, paymentPeriodId, uploadDate, accrualPeriodId) Then
                        inserted += 1
                    End If
                Next
            End If
        End If

        If inserted = 0 Then
            If InsertSingleBasicExtraItem(empId, empCode, accrualPeriodId, paymentPeriodId, uploadDate, asOfDate, fallbackAmount, fallbackTxnCode) Then
                inserted = 1
            End If
        End If
        Return inserted
    End Function

    Private Function ResolveRetroTransactionCode(ByVal originalTxnCode As String) As String
        Dim code As String = If(originalTxnCode, "").Trim().Replace("'", "''")
        If code = "" Then Return ""

        Dim sql As String =
            "SELECT TOP 1 ISNULL(retro.Code, '') " &
            "FROM hrs_TransactionsTypes orig " &
            "INNER JOIN hrs_RetroTransactionMapping m ON m.TransactionTypeID = orig.ID " &
            "INNER JOIN hrs_TransactionsTypes retro ON retro.ID = m.RetroTransactionTypeID " &
            "WHERE (orig.Code = '" & code & "' OR CAST(orig.Code AS varchar(50)) = '" & code & "') " &
            "AND ISNULL(m.IsActive,0)=1 " &
            "AND ISNULL(m.RetroTransactionTypeID,0) > 0 " &
            "AND ISNULL(retro.CancelDate,'')=''"
        Dim obj As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr(), CommandType.Text, sql)
        If obj IsNot Nothing AndAlso Not IsDBNull(obj) Then
            Dim mapped As String = Convert.ToString(obj).Trim()
            If mapped <> "" Then Return mapped.Replace("'", "''")
        End If
        Return ""
    End Function

    Private Function ResolveBasicSalaryRetroCode() As String
        Dim sql As String =
            "SELECT TOP 1 ISNULL(retro.Code, '') " &
            "FROM hrs_TransactionsTypes orig " &
            "INNER JOIN hrs_RetroTransactionMapping m ON m.TransactionTypeID = orig.ID " &
            "INNER JOIN hrs_TransactionsTypes retro ON retro.ID = m.RetroTransactionTypeID " &
            "WHERE ISNULL(orig.IsBasicSalary,0)=1 " &
            "AND ISNULL(orig.CancelDate,'')='' " &
            "AND ISNULL(m.IsActive,0)=1 " &
            "AND ISNULL(m.RetroTransactionTypeID,0) > 0 " &
            "AND ISNULL(retro.CancelDate,'')=''"
        Dim obj As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr(), CommandType.Text, sql)
        If obj IsNot Nothing AndAlso Not IsDBNull(obj) Then
            Dim mapped As String = Convert.ToString(obj).Trim()
            If mapped <> "" Then Return mapped.Replace("'", "''")
        End If
        Return ""
    End Function

    Private Function InsertExtraItemRow(ByVal empCode As String, ByVal txnCode As String, ByVal amount As Decimal, ByVal paymentPeriodId As Integer, ByVal uploadDate As String, ByVal accrualPeriodId As Integer) As Boolean
        Dim ins As String =
            "SET DATEFORMAT DMY; INSERT INTO hrs_EmployeeExtraItems " &
            "VALUES ('" & empCode & "','','" & txnCode.Replace("'", "''") & "'," & amount.ToString(System.Globalization.CultureInfo.InvariantCulture) &
            "," & paymentPeriodId & ",1,'" & uploadDate & "'," & RetroSrc & ",'" & accrualPeriodId & "','101','')"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnStr(), CommandType.Text, ins)
        Return True
    End Function

    Private Function InsertSingleBasicExtraItem(ByVal empId As Integer, ByVal empCode As String, ByVal accrualPeriodId As Integer, ByVal paymentPeriodId As Integer, ByVal uploadDate As String, ByVal asOfDate As String, ByVal fallbackAmount As Decimal, ByVal fallbackTxnCode As String) As Boolean
        ' Prefer mapped retro code for basic salary; then mapped code for fallback txn; never use unmapped original
        Dim txnCode As String = ResolveBasicSalaryRetroCode()
        If txnCode = "" AndAlso Not String.IsNullOrEmpty(fallbackTxnCode) Then
            txnCode = ResolveRetroTransactionCode(fallbackTxnCode)
        End If
        If txnCode = "" Then Return False

        Dim amount As Decimal = fallbackAmount
        If amount <= 0D Then
            Dim ClsContracts As New Clshrs_Contracts(Page)
            Dim asOf As Date = Date.MinValue
            Date.TryParseExact(asOfDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
                               System.Globalization.DateTimeStyles.None, asOf)
            If asOf = Date.MinValue Then Date.TryParse(asOfDate, asOf)
            Dim contractId As Integer = 0
            If asOf <> Date.MinValue Then
                contractId = ClsContracts.ContractValidatoinId(empId, asOf)
            End If
            If contractId > 0 Then
                Dim amtObj As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr(), CommandType.Text,
                    "SET DATEFORMAT DMY; SELECT ISNULL(dbo.fn_GetBasicSalary(" & contractId & ",'" & asOfDate & "'),0)")
                If amtObj IsNot Nothing AndAlso Not IsDBNull(amtObj) Then
                    Decimal.TryParse(Convert.ToString(amtObj), amount)
                End If
            End If
        End If
        If amount <= 0D Then Return False

        Return InsertExtraItemRow(empCode, txnCode, amount, paymentPeriodId, uploadDate, accrualPeriodId)
    End Function

    Private Sub ShowMessage(ByVal msg As String, ByVal isError As Boolean)
        lblMessage.Visible = True
        lblMessage.Text = msg
        lblMessage.CssClass = If(isError, "msg msg-err", "msg msg-ok")
    End Sub
End Class
