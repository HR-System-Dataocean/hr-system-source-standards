Imports System.Activities.Statements
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Reflection
Imports System.Security.Cryptography
Imports Microsoft.ApplicationBlocks.Data
Imports OfficeWebUI
Imports Resources
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System
Imports Venus.Shared

Partial Class _Default
    Inherits MainPage
    Dim MenuHandler As Venus.Shared.Web.NavigationHandler

#Region "Define Constructors"
    Public ReadOnly Property MainManager() As Manager
        Get
            Return Me.Manager1
        End Get
    End Property
    Public ReadOnly Property MainRibbon() As OfficeRibbon
        Get
            Return Me.OfficeRibbon1
        End Get
    End Property
#End Region

    Protected Sub Default_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Manager1.AddInitScript("OfficeWebUI.Ribbon.ToggleOffRibbon();")
        If Context.User.Identity.Name.ToString() <> "" Then
            Dim _Sys_User As New Clssys_Users(New Page)
            _Sys_User.Find("ID = " & Convert.ToInt64(Context.User.Identity.Name.ToString()))
            Dim WelString As String = ""
            WelString = WelString & "<b>"
            WelString = WelString & IIf(ProfileCls.CurrentLanguage() = "Ar", "مرحبا بك </b> المستخدم الحالى : ", "Welcome </b> The Current User Is : ")

            Dim sysusers As New Clssys_Users(Me)
            sysusers.Find("ID = " & _Sys_User.ID)
            If Convert.ToInt32(sysusers.RelEmployee) > 0 Then
                Dim str As String = "select dbo.fn_GetEmpName(Code," & IIf(ProfileCls.CurrentLanguage() = "Ar", "1", "0") & ") AS FullName from hrs_Employees where ID = '" & sysusers.RelEmployee & "'"
                Dim clshrs As New Clshrs_Employees(Me)
                Dim dsEmployee As New Data.DataSet
                dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clshrs.ConnectionString, Data.CommandType.Text, str)

                WelString = WelString & dsEmployee.Tables(0).Rows(0)(0).ToString()
            Else
                WelString = WelString & IIf(ProfileCls.CurrentLanguage() = "Ar", _Sys_User.ArbName, _Sys_User.EngName)
            End If
            WelString = WelString & "<br/>" & IIf(ProfileCls.CurrentLanguage() = "Ar", ProfileCls.RetCompany.ArbName, ProfileCls.RetCompany.EngName)
            Me.OfficeRibbon1.ExtraText = WelString
        End If
        Me.OfficeRibbon1.ApplicationMenuText = IIf(ProfileCls.CurrentLanguage() = "Ar", "الملف الشخصى والتنبيهات", "Personnel File & Notifications")
        Me.OfficeRibbon1.ApplicationMenuType = ApplicationMenuType.Backstage
        Me.RibbonContext1.ApplicationMenuDirection = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)

        Me.RibbonTab101.ApplicationMenuDirection = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)
        Me.RibbonTab101.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "شريط المهام", "Task Bar")
        Me.RibbonGroup2.ApplicationMenuDirection = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)
        Me.RibbonGroup2.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "لغة واجهة الاستخدام", "Interface Language")
        Me.RibbonGroup1.ApplicationMenuDirection = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)
        Me.RibbonGroup1.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "نمط عرض الواجهة", "Interface Layout")
        Me.RibbonGroup3.ApplicationMenuDirection = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)
        Me.RibbonGroup5.ApplicationMenuDirection = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)
        Me.RibbonGroup5.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "تنسيق واجهة الاستخدام", "Interface Theme")

        Me.RibbonGroup4.ApplicationMenuDirection = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.LTR, DocumentDirection.RTL)

        LargeItem1.ApplicationMenuDirection = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)
        LargeItem2.ApplicationMenuDirection = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)

        LargeItem1.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "خروج", "Signout")
        LargeItem2.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "إخفاء<br/>شريط الأدوات", "Hide<br/>Ribbon")
        LargeItem5.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "واجهة<br/>مطوية", "Collapsed<br/>Layout")
        LargeItem6.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "واجهة<br/>ممدودة", "Expanded<br/>Layout")

        LargeItem5.ImageUrl = IIf(ProfileCls.CurrentLanguage() = "Ar", "Common/Images/HomeTB/expand.png", "Common/Images/HomeTB/collapse.png")
        LargeItem6.ImageUrl = IIf(ProfileCls.CurrentLanguage() = "Ar", "Common/Images/HomeTB/collapse.png", "Common/Images/HomeTB/expand.png")


        MediumItem1.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "واجهة إستخدام عربى", "Arabic Interface")
        MediumItem2.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "واجهة إستخدام إنجليزى", "English Interface")
        MediumItem3.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "تنسيق أزرق", "Blue Theme")
        MediumItem4.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "تنسيق فضى", "Silver Theme")

        Label2.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "الألة الحاسبة", "Calculator")
        Label3.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "محول التواريخ", "Dates Converter")

        Dim _WSArea As New Workspace.Area
        Dim _WSSection As New Workspace.Section
        Dim _WSItem As New Workspace.Item

        Dim Cls_DataAcessLayer As New ClsDataAcessLayer(Page)
        Dim ClsModules As New Clssys_Modules(Page)
        Dim ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(Cls_DataAcessLayer.ConnectionString)

        Dim DsModules As New Data.DataSet
        Dim ObjDataRow As Data.DataRow

        DsModules = ClsModules.GetAvilableModules(Cls_DataAcessLayer.DataBaseUserRelatedID, Cls_DataAcessLayer.GroupID)
        If DsModules.Tables(0).Rows.Count > 0 Then
            For Each ObjDataRow In DsModules.Tables(0).Rows
                _WSArea = New Workspace.Area
                _WSArea.ID = ObjDataRow("ID")
                _WSArea.Text = ObjDataRow(ObjNavigationHandler.SetLanguage(Me, "EngName/ArbName"))
                _WSArea.ImageUrl = "Common/Images/Modules/" & ObjDataRow("Code") & ".png"
                _WSArea.SortColor = Drawing.Color.Beige
                OfficeWorkspace1.LAreas.Add(_WSArea)

                _WSSection = New Workspace.Section
                _WSSection.Text = _WSArea.Text
                _WSSection.SortColor = Drawing.Color.Beige

                _WSArea.Sections.Add(_WSSection)

                MenuHandler = New Venus.Shared.Web.NavigationHandler(Cls_DataAcessLayer.ConnectionString)

                _WSItem = New Workspace.Item
                _WSItem.ID = _WSArea.ID

                If ObjDataRow("Code") <> "Rep" Then
                    _WSItem.TV = MenuHandler.LoadMenu(Me, ObjDataRow("ID"), Cls_DataAcessLayer.DataBaseUserRelatedID, Cls_DataAcessLayer.GroupID, False)
                Else
                    _WSItem.TV = MenuHandler.LoadMenu(Me, ObjDataRow("ID"), Cls_DataAcessLayer.DataBaseUserRelatedID, Cls_DataAcessLayer.GroupID, True)
                End If

                _WSSection.Items.Add(_WSItem)
            Next

            Dim tb1 As New Infragistics.Web.UI.LayoutControls.ContentTabItem
            tb1.ContentUrl = "Pages/General/Dashboard.aspx"
            tb1.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", "لوح المعلومات", "Dashboard")
            WebTab1.Tabs.Add(tb1)
        Else
            Manager1.AddInitScript("OfficeWebUI.Ribbon.ToggleOnBackstage(0);")
            Manager1.AddInitScript("OfficeWebUI.Workspace.LPanelHide();")
            Me.OfficeRibbon1.LockToBackstage = True
        End If

        'Add Notifications
        Dim jScript As String = ""
        If ProfileCls.CurrentLanguage() = "Ar" Then
            jScript = jScript & " <script>function generate(type, text) {var n = noty({text: text,type: type,dismissQueue: false,layout: 'topLeft',closeWith: ['click'],theme: 'relax',maxVisible: 100,animation: {open: 'animated bounceInLeft',close: 'animated bounceOutLeft',easing: 'swing',speed: 500}});console.log('html: ' + n.options.id);}"
            jScript = jScript & " function generateLogo() {"
            jScript = jScript & " generate('information', '<div> <i class=""fa fa-check text-success""></i> <div class=""activity"">Venus Web Hr System</br><a href=""http://www.dataocean.info/"" target=""_blank""><img src=""CompanyLogo.png"" style=""border-style: none"" /></a></br></br></div></div>');"
            jScript = jScript & " }"
        Else
            jScript = jScript & " <script>function generate(type, text) {var n = noty({text: text,type: type,dismissQueue: false,layout: 'topRight',closeWith: ['click'],theme: 'relax',maxVisible: 100,animation: {open: 'animated bounceInRight',close: 'animated bounceOutRight',easing: 'swing',speed: 500}});console.log('html: ' + n.options.id);}"
            jScript = jScript & " function generateLogo() {"
            jScript = jScript & " generate('information', '<div> <i class=""fa fa-check text-success""></i> <div class=""activity"">Venus Web Hr System</br><a href=""http://www.dataocean.info/"" target=""_blank""><img src=""CompanyLogo.png"" style=""border-style: none"" /></a></br></br></div></div>');"
            jScript = jScript & " }"
        End If
        jScript = jScript & " $(document).ready(function () {setTimeout(function () {generateLogo();}, 4000);});</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Registered Script", jScript, False)
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Timer1_Tick(Nothing, Nothing)
        End If
    End Sub
    Protected Sub Default_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        Manager1.DirectionMode = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)
        Manager1.UITheme = IIf(ProfileCls.CurrentTheme().Contains("Blue") = True, OfficeWebUI.Theme.Office2010Blue, OfficeWebUI.Theme.Office2010Silver)
        OfficeRibbon1.ApplicationMenuDirection = IIf(ProfileCls.CurrentLanguage() = "Ar", DocumentDirection.RTL, DocumentDirection.LTR)
    End Sub
    Protected Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        Dim pws As New PoliciesWs
        If ProfileCls.CurrentLanguage = "Ar" Then
            MyMovingText.Attributes("direction") = "Right"
        Else
            MyMovingText.Attributes("direction") = "Left"
        End If
        Dim DT As Data.DataTable = pws.RetAnnounces()
        Label1.Text = ""
        For i As Integer = 0 To DT.Rows.Count - 1
            If Label1.Text <> "" Then
                Label1.Text += "&nbsp;&nbsp;<img src=""Common/Images/NewsImage.png"" >&nbsp;&nbsp;"
            End If
            Label1.Text += DT.Rows(i)(0)
        Next

        'Add Notifications
        Dim jScript As String = ""
        If ProfileCls.CurrentLanguage() = "Ar" Then
            jScript = jScript & " <script>function generateQueue(type, text) {var n = noty({text: text,type: type,dismissQueue: true,layout: 'topLeft',closeWith: ['click'],theme: 'relax',maxVisible: 100,animation: {open: 'animated bounceInLeft',close: 'animated bounceOutLeft',easing: 'swing',speed: 500}});console.log('html: ' + n.options.id);}"

            jScript = jScript & " function generateNotify() {"
            Dim ObjClsDocumentDetails As New Clssys_DocumentsInformations(Me.Page)
            DT = New System.Data.DataTable
            Dim clsForms As New ClsSys_Forms(Page)
            If clsForms.Find("Code='frmExpiredDocuments'") Then
                Dim clsFormsPermissions As New ClsSys_FormsPermissions(Page)
                clsFormsPermissions.Find("FormID=" & clsForms.ID & " And UserID=" & clsForms.DataBaseUserRelatedID)
                Dim clshrs As New Clshrs_Employees(Me)
                Dim Sys_User As New Clssys_Users(New Page)
                Sys_User.Find("ID=" & clsForms.DataBaseUserRelatedID & "")
                If Sys_User.Code.ToLower() <> "sa" Then
                    clshrs.Find("Code='" & Sys_User.Code & "'")
                    Dim connetionString As String
                    Dim connection As Data.SqlClient.SqlConnection
                    Dim command As Data.SqlClient.SqlCommand
                    Dim adapter As New Data.SqlClient.SqlDataAdapter
                    Dim DS1 As New Data.DataSet()
                    connetionString = clshrs.ConnectionString
                    connection = New Data.SqlClient.SqlConnection(connetionString)
                    Dim str1 As String = "select count(ActionSerial) as Count from SS_RequestActions  where ( Seen is null or Seen=0) and SS_EmployeeID=" & clshrs.ID & ""
                    command = New Data.SqlClient.SqlCommand(str1, connection)
                    adapter.SelectCommand = command
                    adapter.Fill(DS1, "Table1")
                    If DS1.Tables(0).Rows(0)("Count") > 0 Then
                        Dim Count As Integer
                        Count = CInt(DS1.Tables(0).Rows(0)("Count"))
                        connection.Close()

                        'jScript = jScript & " generateQueue('error', '<div style=""text-align:right;""> <font size=""1""> تذكر أن هناك عدد " & Count & " تنبيه للخدمة الذاتية </br><a href=""#"" onclick=""javascript:parent.ShowSelfServiceNotificationScreen();"">للإطلاع عليها إضغط هنا</a></font></div>');"
                        jScript = jScript & " generateQueue('error', '<div style=""text-align:right;""> <font size=""1""> تذكر أن هناك عدد " & Count & " تنبيه للخدمة الذاتية </br><a href=""#"" onclick=""javascript:parent.ShowReportProjectReportEng2();"">للإطلاع عليها إضغط هنا</a></font></div>');"



                        'jScript = jScript & " generateQueue('error', '<div style=""text-align:right;""> <font size=""1""> تذكر أن هناك عدد " & Count & " تنبيه للخدمة الذاتية </br><a href=""#"" onclick=""javascript:parent.ShowSelfServiceNotificationScreen();"">للإطلاع عليها إضغط هنا</a></font></div>');"
                    End If
                    'Appraisal Notifications
                    NotificationEngine()

                    Dim DS2 As New Data.DataSet()

                    connetionString = clshrs.ConnectionString
                    connection = New Data.SqlClient.SqlConnection(connetionString)
                    Dim str2 As String = "select count(id) as Count from [App_AppraisalNotifications] where App_EmployeeID=" & clshrs.ID & " and (completed is null or Completed=0) "
                    command = New Data.SqlClient.SqlCommand(str2, connection)
                    adapter.SelectCommand = command
                    adapter.Fill(DS2, "Table1")
                    If DS2.Tables(0).Rows(0)("Count") > 0 Then
                        Dim Count As Integer
                        Count = CInt(DS2.Tables(0).Rows(0)("Count"))
                        connection.Close()

                        'jScript = jScript & " generateQueue('error', '<div style=""text-align:right;""> <font size=""1""> تذكر أن هناك عدد " & Count & " تنبيه للخدمة الذاتية </br><a href=""#"" onclick=""javascript:parent.ShowSelfServiceNotificationScreen();"">للإطلاع عليها إضغط هنا</a></font></div>');"
                        jScript = jScript & " generateQueue('error', '<div style=""text-align:right;""> <font size=""1""> تذكر أن هناك عدد " & Count & " تنبيه للتقييمات </br><a href=""#"" onclick=""javascript:parent.ShowAppraisalNotifications1();"">للإطلاع عليها إضغط هنا</a></font></div>');"



                        'jScript = jScript & " generateQueue('error', '<div style=""text-align:right;""> <font size=""1""> تذكر أن هناك عدد " & Count & " تنبيه للخدمة الذاتية </br><a href=""#"" onclick=""javascript:parent.ShowSelfServiceNotificationScreen();"">للإطلاع عليها إضغط هنا</a></font></div>');"
                    End If


                End If




                If clsFormsPermissions.AllowView Then
                    Dim doctype As New Clssys_DocumentsTypes(Me)
                    doctype.Find("RegComputerID > 0")
                    Dim DTdoctype As New System.Data.DataTable
                    DTdoctype = doctype.DataSet.Tables(0)
                    Dim strCntDoc As String = "0"
                    For doc As Integer = 0 To DTdoctype.Rows.Count - 1
                        DT = ObjClsDocumentDetails.GetExpireDocuments("", "", "", DateTime.Now.AddYears(-50), DateTime.Now.AddDays(DTdoctype.Rows(doc)("RegComputerID")), 0, DTdoctype.Rows(doc)("Code")).Tables(0)
                        strCntDoc = Convert.ToInt32(strCntDoc) + DT.Rows.Count
                    Next
                    If Convert.ToInt32(strCntDoc) > 0 Then
                        jScript = jScript & " generateQueue('error', '<div style=""text-align:right;""> <font size=""1""> تذكر أن هناك عدد " & strCntDoc & " تنبيه للوثائق المنتهية </br><a href=""#"" onclick=""javascript:parent.OpenTargetTab(4);"">للإطلاع عليها إضغط هنا</a></font></div>');"
                    End If




                End If
            End If

            If clsForms.Find("Code='frmProjectsExtra'") Then
                Dim clsFormsPermissions As New ClsSys_FormsPermissions(Page)
                clsFormsPermissions.Find("FormID=" & clsForms.ID & " And UserID=" & clsForms.DataBaseUserRelatedID)
                If clsFormsPermissions.AllowView Then
                    Dim clsprojects As New Clshrs_Projects(Me, "hrs_Projects")
                    If clsprojects.Find("isnull(IsLocked,0) = 1 and isnull(IsStoped,0) = 0 and EndDate >= dateadd(Day,30,getdate())") Then
                        jScript = jScript & " generateQueue('information', '<div style=""text-align:right;""> <font size=""1""> تذكر أن هناك عدد " & clsprojects.DataSet.Tables(0).Rows.Count & " عقود مشاريع قاربت على الانتهاء </br><a href=""#"" onclick=""javascript:parent.ShowReportProjectReportArb();"">للإطلاع عليها إضغط هنا</a></font></div>');"
                    End If
                End If
            End If
            If clsForms.Find("Code='frmEmployeesItems'") Then
                Dim clsFormsPermissions As New ClsSys_FormsPermissions(Page)
                clsFormsPermissions.Find("FormID=" & clsForms.ID & " And UserID=" & clsForms.DataBaseUserRelatedID)
                If clsFormsPermissions.AllowView Then
                    Dim ClsEmployeesItems As New Clshrs_EmployeesItems(Me)
                    If ClsEmployeesItems.Find("canceldate is null and isnull(IsConfirmed,0) = 0") Then
                        jScript = jScript & " generateQueue('information', '<div style=""text-align:right;""> <font size=""1""> تذكر أن هناك عدد " & ClsEmployeesItems.DataSet.Tables(0).Rows.Count & " تنقلات عهد موظفين غير معتمدة </br>للإطلاع عليها من نموذج عهد الموظفين</font></div>');"
                    End If
                End If
            End If
            Dim _Dwf_DocumentWorkFow As New ClsDwf_DocumentWorkFow(Me.Page)
            _Dwf_DocumentWorkFow.Find("PeopleID = " & ProfileCls.RetRefPeople() & " and Status = 0")
            DT = _Dwf_DocumentWorkFow.DataSet.Tables(0)
            Dim strCntWFAlert As String = "0"
            For i As Integer = 0 To DT.Rows.Count - 1
                _Dwf_DocumentWorkFow = New ClsDwf_DocumentWorkFow(Me.Page)
                _Dwf_DocumentWorkFow.Find("ID = " & DT.Rows(i)("ID"))
                Dim _sys_Forms As New ClsSys_Forms(Me.Page)
                _sys_Forms.Find("Code = 'Cus_" & _Dwf_DocumentWorkFow.DocumentID & "'")
                _sys_Forms.Code = "Cus_" & _Dwf_DocumentWorkFow.DocumentID
                Dim DT01 As System.Data.DataTable = _sys_Forms.DataSet.Tables(0)
                If DT.Rows.Count > 0 Then
                    strCntWFAlert = Convert.ToInt32(strCntWFAlert) + 1
                End If
            Next
            If Convert.ToInt32(strCntWFAlert) > 0 Then
                jScript = jScript & " generateQueue('error', '<div style=""text-align:right;""> <font size=""1""> تذكر أن هناك عدد " & strCntWFAlert & " تنبيه لمستندات فى نظام الدورات المستندية </br><a href=""#"" onclick=""javascript:parent.OpenTargetTab(2);"">للإطلاع عليها إضغط هنا</a></font></div>');"
            End If
            jScript = jScript & " }"
        Else
            jScript = jScript & " <script>function generateQueue(type, text) {var n = noty({text: text,type: type,dismissQueue: true,layout: 'topRight',closeWith: ['click'],theme: 'relax',maxVisible: 100,animation: {open: 'animated bounceInRight',close: 'animated bounceOutRight',easing: 'swing',speed: 500}});console.log('html: ' + n.options.id);}"

            jScript = jScript & " function generateNotify() {"
            Dim ObjClsDocumentDetails As New Clssys_DocumentsInformations(Me.Page)
            DT = New System.Data.DataTable
            Dim clsForms As New ClsSys_Forms(Page)

            Dim clshrs As New Clshrs_Employees(Me)
            Dim Sys_User As New Clssys_Users(New Page)
            Sys_User.Find("ID=" & clsForms.DataBaseUserRelatedID & "")
            If Sys_User.Code.ToLower() <> "sa" Then
                clshrs.Find("Code='" & Sys_User.Code & "'")
                Dim connetionString As String
                Dim connection As Data.SqlClient.SqlConnection
                Dim command As Data.SqlClient.SqlCommand
                Dim adapter As New Data.SqlClient.SqlDataAdapter
                Dim DS1 As New Data.DataSet()
                connetionString = clshrs.ConnectionString
                connection = New Data.SqlClient.SqlConnection(connetionString)

                Dim str1 As String = "select count(ActionSerial) as Count from SS_RequestActions  where ( Seen is null or Seen=0) and SS_EmployeeID=" & clshrs.ID & ""
                command = New Data.SqlClient.SqlCommand(str1, connection)
                adapter.SelectCommand = command
                connection.Open()
                adapter.Fill(DS1, "Table1")
                connection.Close()
                If DS1.Tables(0).Rows(0)("Count") > 0 Then
                    Dim Count As Integer
                    Count = CInt(DS1.Tables(0).Rows(0)("Count"))
                    connection.Close()

                    jScript = jScript & " generateQueue('error', '<div style=""text-align:right;""> <font size=""1""> Remember There Is " & Count & " Self Service Notifications </br><a href=""#"" onclick=""javascript:parent.ShowReportProjectReportEng3();"">To Display Click Here</a></font></div>');"



                End If
                'Appraisal Notifications
                'NotificationEngine()
                'EscalationEngine(clshrs.ID)
                Dim DS2 As New Data.DataSet()
                connetionString = clshrs.ConnectionString
                connection = New Data.SqlClient.SqlConnection(connetionString)
                Dim str2 As String = "select count(id)  as Count from [App_AppraisalNotifications] where App_EmployeeID=" & clshrs.ID & " and (completed is null or Completed=0) "
                command = New Data.SqlClient.SqlCommand(str2, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS2, "Table1")
                If DS2.Tables(0).Rows(0)("Count") > 0 Then
                    Dim Count As Integer
                    Count = CInt(DS2.Tables(0).Rows(0)("Count"))
                    connection.Close()

                    jScript = jScript & " generateQueue('error', '<div style=""text-align:right;""> <font size=""1""> Remember There Is " & Count & " Appraisal Notifications </br><a href=""#"" onclick=""javascript:parent.ShowAppraisalNotifications2();"">To Display Click Here</a></font></div>');"



                End If

            End If
            If clsForms.Find("Code='frmExpiredDocuments'") Then
                Dim clsFormsPermissions As New ClsSys_FormsPermissions(Page)
                clsFormsPermissions.Find("FormID=" & clsForms.ID & " And UserID=" & clsForms.DataBaseUserRelatedID)
                If clsFormsPermissions.AllowView Then
                    Dim doctype As New Clssys_DocumentsTypes(Me)
                    doctype.Find("RegComputerID > 0")
                    Dim DTdoctype As New System.Data.DataTable
                    DTdoctype = doctype.DataSet.Tables(0)
                    Dim strCntDoc As String = "0"
                    For doc As Integer = 0 To DTdoctype.Rows.Count - 1
                        DT = ObjClsDocumentDetails.GetExpireDocuments("", "", "", DateTime.Now.AddYears(-50), DateTime.Now.AddDays(DTdoctype.Rows(doc)("RegComputerID")), 0, DTdoctype.Rows(doc)("Code")).Tables(0)
                        strCntDoc = Convert.ToInt32(strCntDoc) + DT.Rows.Count
                    Next
                    If Convert.ToInt32(strCntDoc) > 0 Then
                        jScript = jScript & " generateQueue('error', '<div style=""text-align:left;""> <font size=""1"">Remember There Is " & strCntDoc & " Expired Documents Alert</br><a href=""#"" onclick=""javascript:parent.OpenTargetTab(4);"">To Display Click Here</a></font></div>');"
                    End If

                End If
            End If
            If clsForms.Find("Code='frmProjectsExtra'") Then
                Dim clsFormsPermissions As New ClsSys_FormsPermissions(Page)
                clsFormsPermissions.Find("FormID=" & clsForms.ID & " And UserID=" & clsForms.DataBaseUserRelatedID)
                If clsFormsPermissions.AllowView Then
                    Dim clsprojects As New Clshrs_Projects(Me, "hrs_Projects")
                    If clsprojects.Find("isnull(IsLocked,0) = 1 and isnull(IsStoped,0) = 0 and EndDate >= dateadd(Day,30,getdate())") Then
                        jScript = jScript & " generateQueue('information', '<div style=""text-align:left;""> <font size=""1"">Remember There Is " & clsprojects.DataSet.Tables(0).Rows.Count & " Nearly Expired Projects </br><a href=""#"" onclick=""javascript:parent.ShowReportProjectReportEng();"">To Display Click Here</a></font></div>');"
                    End If
                End If
            End If
            If clsForms.Find("Code='frmEmployeesItems'") Then
                Dim clsFormsPermissions As New ClsSys_FormsPermissions(Page)
                clsFormsPermissions.Find("FormID=" & clsForms.ID & " And UserID=" & clsForms.DataBaseUserRelatedID)
                If clsFormsPermissions.AllowView Then
                    Dim ClsEmployeesItems As New Clshrs_EmployeesItems(Me)
                    If ClsEmployeesItems.Find("canceldate is null and isnull(IsConfirmed,0) = 0") Then
                        jScript = jScript & " generateQueue('information', '<div style=""text-align:left;""> <font size=""1"">Remember There Is " & ClsEmployeesItems.DataSet.Tables(0).Rows.Count & " Un Confirmed Custody Transfeers </br><a href=""#"">To Display Visit Employees Custody Form</a></font></div>');"
                    End If
                End If
            End If
            Dim _Dwf_DocumentWorkFow As New ClsDwf_DocumentWorkFow(Me.Page)
            _Dwf_DocumentWorkFow.Find("PeopleID = " & ProfileCls.RetRefPeople() & " and Status = 0")
            DT = _Dwf_DocumentWorkFow.DataSet.Tables(0)
            Dim strCntWFAlert As String = "0"
            For i As Integer = 0 To DT.Rows.Count - 1
                _Dwf_DocumentWorkFow = New ClsDwf_DocumentWorkFow(Me.Page)
                _Dwf_DocumentWorkFow.Find("ID = " & DT.Rows(i)("ID"))
                Dim _sys_Forms As New ClsSys_Forms(Me.Page)
                _sys_Forms.Find("Code = 'Cus_" & _Dwf_DocumentWorkFow.DocumentID & "'")
                _sys_Forms.Code = "Cus_" & _Dwf_DocumentWorkFow.DocumentID
                Dim DT01 As System.Data.DataTable = _sys_Forms.DataSet.Tables(0)
                If DT.Rows.Count > 0 Then
                    strCntWFAlert = Convert.ToInt32(strCntWFAlert) + 1
                End If
            Next
            If Convert.ToInt32(strCntWFAlert) > 0 Then
                jScript = jScript & " generateQueue('error', '<div style=""text-align:left;""> <font size=""1"">Remember There Is " & strCntWFAlert & " Document From work flow</br><a href=""#"" onclick=""javascript:parent.OpenTargetTab(2);"">To Display Click Here</a></font></div>');"
            End If
            jScript = jScript & " }"
        End If
        jScript = jScript & " $(document).ready(function () {setTimeout(function () {generateNotify();}, 11000);});</script>"
        ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "Registered Script", jScript, False)
    End Sub
    Protected Sub MediumItem1_Click()
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("ID = " & ProfileCls.RetUserID()) Then
            _Sys_User.InterfaceLang = "Ar"
            _Sys_User.InterfaceStyle = Page.Theme.Substring(0, Page.Theme.LastIndexOf("_")) & "_" & "Ar"
            _Sys_User.Update("ID = " & ProfileCls.RetUserID())
            ProfileCls.LoadProfile(_Sys_User.ID)
        Else
            HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey) = Convert.ToString("Ar")
            HttpContext.Current.Session(MetaData.SessionCurrentThemeKey) = Page.Theme.Substring(0, Page.Theme.LastIndexOf("_")) & "_" & "Ar"
        End If

        Dim Cookie As New HttpCookie("LoginCookies")
        Cookie.Values(MetaData.SessionCurrentLanguageKey) = HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey).ToString()
        Cookie.Values(MetaData.SessionCurrentThemeKey) = HttpContext.Current.Session(MetaData.SessionCurrentThemeKey).ToString()
        Cookie.Expires = DateTime.Now.AddYears(1)
        Response.Cookies.Add(Cookie)

        ' Old System Modifi
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim CurLanguage As String = IIf(ProfileCls.CurrentLanguage() = "Ar", "ar-EG", "en-US")
        ObjWebHandler.SetCookies(Page, "Lang", CurLanguage, True)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Response.Redirect(Request.Url.ToString())
    End Sub
    Protected Sub MediumItem2_Click()
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("ID = " & ProfileCls.RetUserID()) Then
            _Sys_User.InterfaceLang = "En"
            _Sys_User.InterfaceStyle = Page.Theme.Substring(0, Page.Theme.LastIndexOf("_")) & "_" & "En"
            _Sys_User.Update("ID = " & ProfileCls.RetUserID())
            ProfileCls.LoadProfile(_Sys_User.ID)
        Else
            HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey) = Convert.ToString("En")
            HttpContext.Current.Session(MetaData.SessionCurrentThemeKey) = Page.Theme.Substring(0, Page.Theme.LastIndexOf("_")) & "_" & "En"
        End If

        Dim Cookie As New HttpCookie("LoginCookies")
        Cookie.Values(MetaData.SessionCurrentLanguageKey) = HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey).ToString()
        Cookie.Values(MetaData.SessionCurrentThemeKey) = HttpContext.Current.Session(MetaData.SessionCurrentThemeKey).ToString()
        Cookie.Expires = DateTime.Now.AddYears(1)
        Response.Cookies.Add(Cookie)

        ' Old System Modifi
        Dim ObjWebHandler As New Venus.Shared.Web.WebHandler
        Dim CurLanguage As String = IIf(ProfileCls.CurrentLanguage() = "Ar", "ar-EG", "en-US")
        ObjWebHandler.SetCookies(Page, "Lang", CurLanguage, True)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Response.Redirect(Request.Url.ToString())
    End Sub
    Protected Sub MediumItem3_Click()
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("ID = " & ProfileCls.RetUserID()) Then
            _Sys_User.InterfaceStyle = Convert.ToString("Blue") & "_" & _Sys_User.InterfaceLang
            _Sys_User.Update("ID = " & ProfileCls.RetUserID())
            ProfileCls.LoadProfile(_Sys_User.ID)
        Else
            HttpContext.Current.Session(MetaData.SessionCurrentThemeKey) = Convert.ToString("Blue") & "_" & HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey).ToString()
        End If

        Dim Cookie As New HttpCookie("LoginCookies")
        Cookie.Values(MetaData.SessionCurrentLanguageKey) = HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey).ToString()
        Cookie.Values(MetaData.SessionCurrentThemeKey) = HttpContext.Current.Session(MetaData.SessionCurrentThemeKey).ToString()
        Cookie.Expires = DateTime.Now.AddYears(1)
        Response.Cookies.Add(Cookie)

        Response.Redirect(Request.Url.ToString())
    End Sub
    Protected Sub MediumItem4_Click()
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("ID = " & ProfileCls.RetUserID()) Then
            _Sys_User.InterfaceStyle = Convert.ToString("Silver") & "_" & _Sys_User.InterfaceLang
            _Sys_User.Update("ID = " & ProfileCls.RetUserID())
            ProfileCls.LoadProfile(_Sys_User.ID)
        Else
            HttpContext.Current.Session(MetaData.SessionCurrentThemeKey) = Convert.ToString("Silver") & "_" & HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey).ToString()
        End If

        Dim Cookie As New HttpCookie("LoginCookies")
        Cookie.Values(MetaData.SessionCurrentLanguageKey) = HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey).ToString()
        Cookie.Values(MetaData.SessionCurrentThemeKey) = HttpContext.Current.Session(MetaData.SessionCurrentThemeKey).ToString()
        Cookie.Expires = DateTime.Now.AddYears(1)
        Response.Cookies.Add(Cookie)

        Response.Redirect(Request.Url.ToString())
    End Sub
    Private Function SetDate(gData As Object, hDate As Object) As Date
        Try
            If gData <> "  /  /    " Then
                If ClsDataAcessLayer.IsGreg(gData) Then
                    Return ClsDataAcessLayer.FormatGreg(gData, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.HijriToGreg(gData, "dd/MM/yyyy")
                End If
            ElseIf hDate <> "  /  /    " Then
                If ClsDataAcessLayer.IsHijri(hDate) Then
                    Return ClsDataAcessLayer.HijriToGreg(hDate, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.FormatGreg(hDate, "dd/MM/yyyy")
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Function NotificationEngine() As Boolean
        Dim NotificationDue As Boolean = False
        Dim Cls_DataAcessLayer As New ClsDataAcessLayer(Page)
        Dim ConnectionString As String = Cls_DataAcessLayer.ConnectionString
        Dim strAllDirectManager As String = "select distinct( Managerid) as Managerid from hrs_Employees where ManagerID is not null"
        Dim DSAllManagers As DataSet = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strAllDirectManager)
        Dim EmpID As Integer = 0
        If DSAllManagers.Tables(0).Rows.Count > 0 Then
            For Each RowManager As DataRow In DSAllManagers.Tables(0).Rows
                EmpID = RowManager("Managerid")

                Dim StrCheckManager As String = " SELECT e.ID,e.Code, e.JoinDate, p.AppraisalTypeGroupID FROM hrs_Employees e JOIN hrs_Contracts c ON e.ID = c.EmployeeID AND (c.EndDate > GETDATE() OR c.EndDate IS NULL)  JOIN hrs_Positions p ON c.PositionID = p.ID WHERE e.ManagerID = '" & EmpID & "'"

                Dim dsSubEmployee As DataSet = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, StrCheckManager)
                If dsSubEmployee.Tables(0).Rows.Count > 0 Then

                    For Each row As DataRow In dsSubEmployee.Tables(0).Rows
                        Dim EmployeeID As Integer = row("ID")
                        Dim JoinDate As DateTime = If(IsDBNull(row("JoinDate")), Date.MinValue, Convert.ToDateTime(row("JoinDate")))
                        Dim GroupID As Integer = If(IsDBNull(row("AppraisalTypeGroupID")), 0, Convert.ToInt32(row("AppraisalTypeGroupID")))
                        Dim EmployeeCode As String = row("Code")
                        If GroupID <= 0 Then Continue For

                        Dim strAppraisalTypes As String = "SELECT * FROM App_AppraisalTypes WHERE AppraisalTypeGroupID = " & GroupID & " and CancelDate is null"
                        Dim dsAppraisalTypes As DataSet = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strAppraisalTypes)
                        If dsAppraisalTypes.Tables(0).Rows.Count = 0 Then Continue For

                        For Each AppTyperow As DataRow In dsAppraisalTypes.Tables(0).Rows
                            Dim AppraisalTypeID As Integer = AppTyperow("ID")
                            Dim NotificationPeriod As Integer = AppTyperow("NotificationPeriod")
                            Dim ArabName As String = AppTyperow("ArbName") & DateTime.Now.Year.ToString()
                            Dim EngName As String = AppTyperow("EngName") & DateTime.Now.Year.ToString()

                            'بتأكد ان الابريزال ده مرة واحدة في العمر
                            If Convert.ToBoolean(AppTyperow("IsOneTimeOnly")) Then
                                '1- بتأكد اذا كان اتعمله قبل كده ولا لاء
                                Dim strOneTimeAppraisal As String = "SELECT COUNT(ID) FROM APP_Appraisals WHERE AppraisalTypeID = " & AppraisalTypeID & " AND EmployeeID =" & EmployeeID & ""
                                Dim OneTimeCount As Integer
                                OneTimeCount = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strOneTimeAppraisal))
                                If OneTimeCount = 0 Then  'لو مكانش اتعمله قبل كده علي السيستم
                                    Dim OneTimePeriod As Integer = AppTyperow("OneTimePeriod")
                                    'هنا هنستبدل ال join date  بالتاريخ الاقتتاحي و 
                                    Dim openingdatestr = "select LastExternalAppraisalDate from APP_AppraisalEmployeesStartDate where AppraisalTypeID= " & AppraisalTypeID & " and EmployeeID = " & EmployeeID & ""
                                    Dim OpeningDate As DateTime = CDate(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, openingdatestr))
                                    Dim DueDate As DateTime
                                    If OpeningDate > JoinDate Then
                                        DueDate = OpeningDate.AddDays(OneTimePeriod - NotificationPeriod)
                                    Else
                                        DueDate = JoinDate.AddDays(OneTimePeriod - NotificationPeriod)

                                    End If
                                    If DueDate <= DateTime.Now Then
                                        Dim FromDate As DateTime = JoinDate
                                        Dim ToDate As DateTime = JoinDate.AddDays(OneTimePeriod)
                                        Dim InsertSQL As String
                                        InsertSQL = "set dateformat dmy INSERT INTO APP_Appraisals (Code,ArabName, EngName, AppraisalTypeID, FromDate, ToDate, EmployeeID, NotificationSent, AppraisalStatusID) VALUES ('" & AppraisalTypeID & "-" & EmployeeCode & "','" & ArabName & "', '" & EngName & "', " & AppraisalTypeID & ", '" & FromDate.ToString("dd/MM/yyyy") & "', '" & ToDate.ToString("dd/MM/yyyy") & "', " & EmployeeID & ", 1, 1); SELECT SCOPE_IDENTITY();"
                                        Dim AppraisalID As Integer = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, InsertSQL))
                                        SaveAppraisalFirstLeveNotification(AppraisalTypeID, AppraisalID, EmpID, EmployeeID)
                                        NotificationDue = True
                                        Cls_DataAcessLayer.SendEmail("FrmDefaultNotification", Me.Page, 1, "V_AppraisalDueNotification", EmployeeID)

                                    End If
                                End If
                            Else
                                Dim Frequency As Integer = AppTyperow("AppraisalFrequency")
                                Dim strLastAppraisal As String = "SELECT MAX(ToDate) FROM APP_Appraisals WHERE AppraisalTypeID = " & AppraisalTypeID & " AND EmployeeID = " & EmployeeID & ""
                                Dim resultObj As Object = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, strLastAppraisal)
                                Dim LastDate As DateTime = If(IsDBNull(resultObj) OrElse resultObj Is Nothing, Date.MinValue, Convert.ToDateTime(resultObj))
                                'Dim OneTimePeriod As Integer = CInt(AppTyperow("OneTimePeriod"))
                                Dim OneTimePeriod As Integer

                                If Not IsDBNull(AppTyperow("OneTimePeriod")) Then
                                    OneTimePeriod = CInt(AppTyperow("OneTimePeriod"))
                                Else
                                    OneTimePeriod = 0 ' أو أي قيمة افتراضية تناسب منطقك
                                End If
                                Dim openingdatestr = "select LastExternalAppraisalDate from APP_AppraisalEmployeesStartDate where AppraisalTypeID= " & AppraisalTypeID & " and EmployeeID = " & EmployeeID & ""
                                Dim OpeningDate As DateTime = CDate(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, openingdatestr))
                                Dim BaseDate As DateTime
                                If OpeningDate = Date.MinValue Then
                                    BaseDate = If(LastDate = Date.MinValue, JoinDate, LastDate).ToString("dd/MM/yyyy")
                                Else
                                    BaseDate = If(LastDate = Date.MinValue, OpeningDate, LastDate).ToString("dd/MM/yyyy")

                                End If
                                Dim DueDate As DateTime = BaseDate.AddDays(Frequency - NotificationPeriod).ToString("dd/MM/yyyy")
                                Dim Strcount As String = "select count(ID) from APP_Appraisals where AppraisalTypeID= " & AppraisalTypeID & " and EmployeeID= " & EmployeeID & " "

                                Dim Count As Integer = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, Strcount))

                                If DueDate <= DateTime.Now And Count = 0 Then
                                    Dim FromDate As DateTime = BaseDate.AddDays(1).ToString("dd/MM/yyyy")
                                    Dim ToDate As DateTime = BaseDate.AddDays(Frequency).ToString("dd/MM/yyyy")
                                    Dim InsertSQL As String = "set dateformat dmy INSERT INTO APP_Appraisals (Code,ArabName, EngName, AppraisalTypeID, FromDate, ToDate, EmployeeID, NotificationSent, AppraisalStatusID) VALUES ('" & AppraisalTypeID & "-" & EmployeeCode & "','" & ArabName & "', '" & EngName & "', " & AppraisalTypeID & ", '" & BaseDate.ToString("dd/MM/yyyy") & "', '" & DueDate.ToString("dd/MM/yyyy") & "', " & EmployeeID & ", 1, 1); SELECT SCOPE_IDENTITY();"
                                    Dim AppraisalID As Integer = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, InsertSQL))
                                    SaveAppraisalFirstLeveNotification(AppraisalTypeID, AppraisalID, EmpID, EmployeeID)
                                    Cls_DataAcessLayer.SendEmail("FrmDefaultNotification", Me.Page, 1, "V_AppraisalDueNotification", EmployeeID)

                                    NotificationDue = True
                                End If
                            End If
                        Next
                    Next
                End If

            Next
        End If

        Return NotificationDue
    End Function
    Private Function EscalationEngine(EmpID As Integer)
        Dim Cls_DataAcessLayer As New ClsDataAcessLayer(Page)
        Dim ConnectionString As String = Cls_DataAcessLayer.ConnectionString
        Dim Str As String = " SELECT * from V_AppraisalDueEscalation where EvaluatorID=" & EmpID & " "

        Dim dsDue As DataSet = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, Str)
        If dsDue.Tables(0).Rows.Count > 0 Then
            For Each row In dsDue.Tables(0).Rows
                Cls_DataAcessLayer.SendEmail("FrmDefault", Me.Page, 1, "V_AppraisalDueEscalation", EmpID)
                Dim strUpdate As String = "update APP_Appraisals set EscalationSent =1 where id=" & row("AppraisalID") & " "
                SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strUpdate)
            Next

        End If
    End Function
    Public Function SaveAppraisalFirstLeveNotification(AppraisalTypeID As Integer, AppraisalID As Integer, ManagerID As Integer, EmployeeID As Integer) As Boolean
        Try

            Dim SqlCommand As Data.SqlClient.SqlCommand
            Dim ClsEmployees As New Clshrs_Employees(Page)

            Dim ConnectionString As String
            ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

            Dim strselect As String
            strselect = "select * from App_AppraisalConfigurations where AppraisalTypeID=" & AppraisalTypeID & " and Rank=1"
            Dim DsFirstLevel As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
            If DsFirstLevel.Tables(0).Rows.Count > 0 Then
                For Each Row As Data.DataRow In DsFirstLevel.Tables(0).Rows
                    'DirectManager
                    If Row("UserTypeID") = 1 Then

                        Dim strAddAppNotification As String
                        strAddAppNotification = "insert into App_AppraisalNotifications(AppraisalID,APP_EmployeeID,EmployeeID,ConfigurationID,ConfigurationLevel)  values(" & AppraisalID & " , " & ManagerID & "," & EmployeeID & "," & Row("ID") & ",1)"
                        SqlCommand = New SqlClient.SqlCommand
                        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                        SqlCommand.CommandType = CommandType.Text
                        SqlCommand.CommandText = strAddAppNotification
                        SqlCommand.Connection.Open()
                        SqlCommand.ExecuteNonQuery()
                        SqlCommand.Connection.Close()
                    End If

                    'Position
                    If Row("UserTypeID") = 2 Then
                        Dim clshrspositions As New Clshrs_Positions(Page)


                        Dim strempinposition As String = "select  distinct EmployeeID from hrs_Contracts where PositionID=" & Row("PositionID") & " and CancelDate is null And (EndDate>=getdate() or EndDate  is null)"

                        Dim DsPositionEmployees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strempinposition)
                        If DsPositionEmployees.Tables(0).Rows.Count > 0 Then


                            For Each RW In DsPositionEmployees.Tables(0).Rows
                                Dim strinsert As String
                                strinsert = "insert into App_AppraisalNotifications(AppraisalID,APP_EmployeeID,EmployeeID,ConfigurationID,ConfigurationLevel)  values(" & AppraisalID & " , " & RW("EmployeeID") & "," & EmployeeID & "," & Row("ID") & ",1)"
                                SqlCommand = New SqlClient.SqlCommand
                                SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                                SqlCommand.CommandType = CommandType.Text
                                SqlCommand.CommandText = strinsert
                                SqlCommand.Connection.Open()
                                SqlCommand.ExecuteNonQuery()
                                SqlCommand.Connection.Close()

                            Next

                        End If
                    End If
                    If Row("UserTypeID") = 3 Then
                        Dim strinsert As String



                        strinsert = "insert into App_AppraisalNotifications(AppraisalID,APP_EmployeeID,EmployeeID,ConfigurationID,ConfigurationLevel)  values(" & AppraisalID & " , " & Row("EmployeeID") & "," & EmployeeID & "," & Row("ID") & ",1)"

                        SqlCommand = New SqlClient.SqlCommand
                        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                        SqlCommand.CommandType = CommandType.Text
                        SqlCommand.CommandText = strinsert
                        SqlCommand.Connection.Open()
                        SqlCommand.ExecuteNonQuery()
                        SqlCommand.Connection.Close()
                    End If
                    If Row("UserTypeID") = 4 Then
                        Dim strinsert As String



                        strinsert = "insert into App_AppraisalNotifications(AppraisalID,APP_EmployeeID,EmployeeID,ConfigurationID,ConfigurationLevel)  values(" & AppraisalID & " , " & EmployeeID & "," & EmployeeID & "," & Row("ID") & ",1)"

                        SqlCommand = New SqlClient.SqlCommand
                        SqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
                        SqlCommand.CommandType = CommandType.Text
                        SqlCommand.CommandText = strinsert
                        SqlCommand.Connection.Open()
                        SqlCommand.ExecuteNonQuery()
                        SqlCommand.Connection.Close()
                    End If
                Next
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry Can not proceed your request because there are no levels for this request ...Please contact system admin  / عفوا لايمكن تسجيل الطلب لعدم وجود مراحل ... يرجي مراجعة مدير النظام"))

            End If


        Catch ex As Exception

        End Try
    End Function

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        Try

            txtHDate.Value = ClsDataAcessLayer.GregToHijri(SetDate(txtGDate.Text, "  /  /    "), "ddMMyyyy")

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button3_Click(sender As Object, e As System.EventArgs) Handles Button3.Click
        txtGDate.Value = ClsDataAcessLayer.HijriToGreg(SetDate("  /  /    ", txtHDate.Text), "ddMMyyyy")
    End Sub

    Private Sub Timer1_Load(sender As Object, e As EventArgs) Handles Timer1.Load

    End Sub
End Class
