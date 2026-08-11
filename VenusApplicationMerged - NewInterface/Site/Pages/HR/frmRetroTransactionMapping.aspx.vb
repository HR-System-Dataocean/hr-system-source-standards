Imports System.Data
Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System

Partial Class frmRetroTransactionMapping
    Inherits MainPage

    Private _retroOptions As DataTable
    Private _configuredText As String = "Configured"
    Private _needsText As String = "Needs Mapping"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
            SetPageDirection()

            If Not IsPostBack Then
                EnsureMappingTable()
                ApplyLabels()
                BindFilterLists()
                BindMappings()
            Else
                ApplyLabels()
                LoadRetroOptions()
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
        _configuredText = nav.SetLanguage(Page, "Configured/تم الإعداد")
        _needsText = nav.SetLanguage(Page, "Needs Mapping/يحتاج ربط")

        lblTitle.Text = nav.SetLanguage(Page,
            "Linking differences items retrospectively/ربط بنود الفروقات بأثر رجعي")
        lblDescription.Text = nav.SetLanguage(Page,
            "Select the payroll items included in retrospective difference calculations, and link each item to its corresponding difference item./حدد بنود الرواتب المشمولة في احتساب الفروقات بأثر رجعي، واربط كل بند ببند الفروقات المقابل له.")
        lblInfoBanner.Text = nav.SetLanguage(Page,
            "The value of the differences will be recorded on the specified differences item without modifying the original historical payroll item transaction./سيتم تسجيل قيمة الفروقات على بند الفروقات المحدد دون تعديل حركة بند الرواتب التاريخية الأصلية.")

        lblSearchCaption.Text = nav.SetLanguage(Page, "Search Transaction/بحث عن بند")
        txtSearch.Attributes("placeholder") = nav.SetLanguage(Page,
            "Code or transaction name.../بحث عن بند (كود أو اسم البند)")
        lblGroupCaption.Text = nav.SetLanguage(Page, "Transaction Group/مجموعة البند")
        lblStatusCaption.Text = nav.SetLanguage(Page, "Retro Status/حالة الفروقات")
        btnClearFilter.Text = nav.SetLanguage(Page, "Clear Filters/مسح التصفية")

        litActiveSuffix.Text = nav.SetLanguage(Page, " transactions included/ بنود مفعلة")
        litMappedSuffix.Text = nav.SetLanguage(Page, " transactions mapped/ بنود مربوطة")
        litLegendConfigured.Text = _configuredText
        litLegendNeeds.Text = _needsText

        litColInclude.Text = nav.SetLanguage(Page, "Include/تفعيل")
        litColPayroll.Text = nav.SetLanguage(Page, "Payroll Transaction/بند الرواتب")
        litColGroup.Text = nav.SetLanguage(Page, "Transaction Group/مجموعة البند")
        litColRetro.Text = nav.SetLanguage(Page, "Retro Transaction/بند الفروقات")
        litColStatus.Text = nav.SetLanguage(Page, "Status/الحالة")

        lblFooterNote.Text = nav.SetLanguage(Page,
            "Only included transactions will be considered when the system calculates retroactive payroll differences./سيتم احتساب الفروقات بأثر رجعي فقط للبنود المفعلة في هذه الشاشة.")
        btnSave.Text = nav.SetLanguage(Page, "Save Configuration/حفظ الإعدادات")
        btnReset.Text = nav.SetLanguage(Page, "Reset/إعادة تعيين")
        lblEmpty.Text = nav.SetLanguage(Page, "No payroll transactions found./لا توجد بنود رواتب.")
        lblFilterEmpty.Text = nav.SetLanguage(Page, "No transactions match the current filters./لا توجد بنود مطابقة للتصفية الحالية.")

        Page.Title = "* Venus Payroll * ~" & lblTitle.Text
    End Sub

    Private Sub BindFilterLists()
        Dim nav As Venus.Shared.Web.NavigationHandler = GetNav()
        Dim ClsGroups As New Clshrs_TransactionGroups(Page)
        ClsGroups.GetDropDownList(ddlGroup, True)
        If ddlGroup.Items.Count > 0 Then
            ddlGroup.Items(0).Text = nav.SetLanguage(Page, "All/الكل")
            ddlGroup.Items(0).Value = "0"
        Else
            ddlGroup.Items.Add(New ListItem(nav.SetLanguage(Page, "All/الكل"), "0"))
        End If

        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem(nav.SetLanguage(Page, "All/الكل"), "0"))
        ddlStatus.Items.Add(New ListItem(nav.SetLanguage(Page, "Configured/تم الإعداد"), "1"))
        ddlStatus.Items.Add(New ListItem(nav.SetLanguage(Page, "Needs Mapping/يحتاج ربط"), "2"))
    End Sub

    Private Sub LoadRetroOptions()
        Dim nameCol As String = If(IsArabic(), "tt.ArbName", "tt.EngName")
        Dim altCol As String = If(IsArabic(), "tt.EngName", "tt.ArbName")
        Dim sql As String =
            "SELECT tt.ID, tt.Code, " &
            " ISNULL(NULLIF(LTRIM(RTRIM(" & nameCol & ")), ''), " & altCol & ") AS TxnName " &
            "FROM hrs_TransactionsTypes tt " &
            "WHERE ISNULL(tt.CancelDate,'') = '' " &
            "ORDER BY tt.Code"
        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr(), CommandType.Text, sql)
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            _retroOptions = ds.Tables(0)
        Else
            _retroOptions = New DataTable()
        End If
    End Sub

    Private Sub BindMappings()
        Dim nav As Venus.Shared.Web.NavigationHandler = GetNav()
        Dim nameCol As String = If(IsArabic(), "tt.ArbName", "tt.EngName")
        Dim altCol As String = If(IsArabic(), "tt.EngName", "tt.ArbName")
        Dim groupNameCol As String = If(IsArabic(), "tg.ArbName", "tg.EngName")
        Dim groupAltCol As String = If(IsArabic(), "tg.EngName", "tg.ArbName")

        Dim sql As String =
            "SELECT tt.ID AS TransactionTypeID, tt.Code, " &
            " ISNULL(NULLIF(LTRIM(RTRIM(" & nameCol & ")), ''), " & altCol & ") AS TxnName, " &
            " ISNULL(tt.TransactionGroupID, 0) AS TransactionGroupID, " &
            " ISNULL(tg.Code, '') AS GroupCode, " &
            " ISNULL(NULLIF(LTRIM(RTRIM(" & groupNameCol & ")), ''), ISNULL(" & groupAltCol & ", '')) AS GroupName, " &
            " ISNULL(m.RetroTransactionTypeID, 0) AS RetroTransactionTypeID, " &
            " CAST(ISNULL(m.IsActive, 0) AS bit) AS IsActive " &
            "FROM hrs_TransactionsTypes tt " &
            "LEFT JOIN hrs_TransactionsGroups tg ON tg.ID = tt.TransactionGroupID AND ISNULL(tg.CancelDate,'') = '' " &
            "LEFT JOIN hrs_RetroTransactionMapping m ON m.TransactionTypeID = tt.ID " &
            "WHERE ISNULL(tt.CancelDate,'') = '' " &
            "AND ISNULL(tt.TransactionGroupID,0) = 1 " &
            "ORDER BY tt.Code"

        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr(), CommandType.Text, sql)
        Dim dt As DataTable
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            dt = ds.Tables(0)
        Else
            dt = New DataTable()
        End If

        If Not dt.Columns.Contains("GroupDisplay") Then dt.Columns.Add("GroupDisplay", GetType(String))
        If Not dt.Columns.Contains("ConfiguredText") Then dt.Columns.Add("ConfiguredText", GetType(String))
        If Not dt.Columns.Contains("NeedsText") Then dt.Columns.Add("NeedsText", GetType(String))

        Dim activeCount As Integer = 0
        Dim mappedCount As Integer = 0
        For Each row As DataRow In dt.Rows
            Dim gCode As String = Convert.ToString(row("GroupCode"))
            Dim gName As String = Convert.ToString(row("GroupName"))
            If gCode <> "" OrElse gName <> "" Then
                row("GroupDisplay") = "(" & gCode & ") " & gName
            Else
                row("GroupDisplay") = "—"
            End If
            row("ConfiguredText") = _configuredText
            row("NeedsText") = _needsText
            If Convert.ToBoolean(row("IsActive")) Then activeCount += 1
            If Convert.ToInt32(row("RetroTransactionTypeID")) > 0 Then mappedCount += 1
        Next

        lblActiveCount.Text = activeCount.ToString()
        lblMappedCount.Text = mappedCount.ToString()

        LoadRetroOptions()
        rptMappings.DataSource = dt
        rptMappings.DataBind()

        pnlEmpty.Visible = (dt.Rows.Count = 0)
    End Sub

    Protected Sub rptMappings_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem Then Return

        Dim rowView As DataRowView = TryCast(e.Item.DataItem, DataRowView)
        If rowView Is Nothing Then Return

        Dim litArrow As Literal = TryCast(e.Item.FindControl("litArrow"), Literal)
        If litArrow IsNot Nothing Then
            litArrow.Text = If(IsArabic(), "←", "→")
        End If

        Dim litStatus As Literal = TryCast(e.Item.FindControl("litStatus"), Literal)
        Dim retroId As Integer = Convert.ToInt32(rowView("RetroTransactionTypeID"))
        If litStatus IsNot Nothing Then
            Dim configured As String = Convert.ToString(rowView("ConfiguredText"))
            Dim needs As String = Convert.ToString(rowView("NeedsText"))
            If retroId > 0 Then
                litStatus.Text = "<span class=""status-label"" data-configured=""" & Server.HtmlEncode(configured) &
                    """ data-needs=""" & Server.HtmlEncode(needs) &
                    """><span class=""dot dot-green""></span>" & Server.HtmlEncode(configured) & "</span>"
            Else
                litStatus.Text = "<span class=""status-label"" data-configured=""" & Server.HtmlEncode(configured) &
                    """ data-needs=""" & Server.HtmlEncode(needs) &
                    """><span class=""dot dot-orange""></span>" & Server.HtmlEncode(needs) & "</span>"
            End If
        End If

        Dim ddlRetro As DropDownList = TryCast(e.Item.FindControl("ddlRetro"), DropDownList)
        If ddlRetro Is Nothing Then Return

        Dim nav As Venus.Shared.Web.NavigationHandler = GetNav()
        ddlRetro.Items.Clear()
        ddlRetro.Items.Add(New ListItem(nav.SetLanguage(Page, "[Select difference item]/[اختر بند الفروقات]"), "0"))

        If _retroOptions IsNot Nothing Then
            For Each opt As DataRow In _retroOptions.Rows
                Dim optId As Integer = Convert.ToInt32(opt("ID"))
                Dim text As String = "(" & Convert.ToString(opt("Code")) & ") " & Convert.ToString(opt("TxnName"))
                ddlRetro.Items.Add(New ListItem(text, optId.ToString()))
            Next
        End If

        Dim selected As ListItem = ddlRetro.Items.FindByValue(retroId.ToString())
        If selected IsNot Nothing Then
            ddlRetro.ClearSelection()
            selected.Selected = True
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            EnsureMappingTable()
            Dim nav As Venus.Shared.Web.NavigationHandler = GetNav()
            Dim userId As Integer = ResolveUserId()
            Dim saved As Integer = 0

            For Each item As RepeaterItem In rptMappings.Items
                If item.ItemType <> ListItemType.Item AndAlso item.ItemType <> ListItemType.AlternatingItem Then Continue For

                Dim hdnTxnID As HiddenField = TryCast(item.FindControl("hdnTxnID"), HiddenField)
                Dim chkActive As CheckBox = TryCast(item.FindControl("chkActive"), CheckBox)
                Dim ddlRetro As DropDownList = TryCast(item.FindControl("ddlRetro"), DropDownList)
                If hdnTxnID Is Nothing OrElse chkActive Is Nothing OrElse ddlRetro Is Nothing Then Continue For

                Dim txnId As Integer = 0
                Integer.TryParse(hdnTxnID.Value, txnId)
                If txnId <= 0 Then Continue For

                Dim isActive As Boolean = chkActive.Checked
                Try
                    Dim postedChk As String = Request.Form(chkActive.UniqueID)
                    isActive = Not String.IsNullOrEmpty(postedChk)
                Catch
                End Try

                Dim retroId As Integer = 0
                Integer.TryParse(ddlRetro.SelectedValue, retroId)
                Try
                    Dim postedRetro As String = Request.Form(ddlRetro.UniqueID)
                    If Not String.IsNullOrEmpty(postedRetro) Then Integer.TryParse(postedRetro, retroId)
                Catch
                End Try

                If isActive AndAlso retroId <= 0 Then
                    ShowMessage(nav.SetLanguage(Page,
                        "Please map a retro transaction for every included payroll item before saving./يرجى ربط بند فروقات لكل بند رواتب مفعل قبل الحفظ."), True)
                    Return
                End If

                SaveMappingRow(txnId, retroId, isActive, userId)
                saved += 1
            Next

            ShowMessage(nav.SetLanguage(Page,
                "Settings saved successfully./تم حفظ الإعدادات بنجاح."), False)
            BindMappings()
        Catch ex As Exception
            ShowMessage(ex.Message, True)
        End Try
    End Sub

    Private Sub SaveMappingRow(ByVal txnId As Integer, ByVal retroId As Integer, ByVal isActive As Boolean, ByVal userId As Integer)
        Dim retroSql As String = If(retroId > 0, retroId.ToString(), "NULL")
        Dim activeBit As Integer = If(isActive, 1, 0)
        Dim sql As String =
            "IF EXISTS (SELECT 1 FROM hrs_RetroTransactionMapping WHERE TransactionTypeID=" & txnId & ") " &
            "UPDATE hrs_RetroTransactionMapping SET RetroTransactionTypeID=" & retroSql &
            ", IsActive=" & activeBit & ", RegUserID=" & userId & ", RegDate=GETDATE() " &
            "WHERE TransactionTypeID=" & txnId & "; " &
            "ELSE " &
            "INSERT INTO hrs_RetroTransactionMapping (TransactionTypeID, RetroTransactionTypeID, IsActive, RegUserID, RegDate) " &
            "VALUES (" & txnId & "," & retroSql & "," & activeBit & "," & userId & ",GETDATE());"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnStr(), CommandType.Text, sql)
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs)
        txtSearch.Text = ""
        If ddlGroup.Items.Count > 0 Then ddlGroup.SelectedIndex = 0
        If ddlStatus.Items.Count > 0 Then ddlStatus.SelectedIndex = 0
        BindMappings()
        ShowMessage(GetNav().SetLanguage(Page, "Values reloaded from saved settings./تم إعادة تحميل القيم من الإعدادات المحفوظة."), False)
    End Sub

    Private Sub EnsureMappingTable()
        Dim sql As String =
            "IF OBJECT_ID(N'dbo.hrs_RetroTransactionMapping', N'U') IS NULL " &
            "CREATE TABLE dbo.hrs_RetroTransactionMapping (" &
            "ID int IDENTITY(1,1) NOT NULL, " &
            "TransactionTypeID int NOT NULL, " &
            "RetroTransactionTypeID int NULL, " &
            "IsActive bit NOT NULL CONSTRAINT DF_hrs_RetroTransactionMapping_IsActive DEFAULT(0), " &
            "RegUserID int NULL, " &
            "RegDate datetime NOT NULL CONSTRAINT DF_hrs_RetroTransactionMapping_RegDate DEFAULT(GETDATE()), " &
            "CONSTRAINT PK_hrs_RetroTransactionMapping PRIMARY KEY CLUSTERED (ID), " &
            "CONSTRAINT UQ_hrs_RetroTransactionMapping_Txn UNIQUE (TransactionTypeID))"
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

    Private Sub ShowMessage(ByVal message As String, ByVal isError As Boolean)
        lblMessage.Visible = True
        lblMessage.Text = message
        lblMessage.CssClass = If(isError, "msg msg-err", "msg msg-ok")
    End Sub
End Class
