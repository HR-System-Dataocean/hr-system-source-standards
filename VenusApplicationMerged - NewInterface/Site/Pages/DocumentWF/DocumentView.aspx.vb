Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class DocumentView
    Inherits MainPage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            If Page.Request.QueryString("DocumentID") <> Nothing Then
                Dim _Dwf_Document As New ClsDwf_Documents(Me.Page)
                _Dwf_Document.Find("ID = " & Page.Request.QueryString("DocumentID"))
                If _Dwf_Document.ApplyWorkFlow = True Then
                    TBHEADER.Visible = False
                    TBHEADERWF.Visible = True
                    HiddenField_WFStage.Value = "1"
                    If Page.Request.QueryString("Stage") <> Nothing And Page.Request.QueryString("TrnsID") <> Nothing And Page.Request.QueryString("WF") <> Nothing Then
                        HiddenField_WFStage.Value = HiddenField_WFStage.Value & ";" & Page.Request.QueryString("Stage") & ";" & Page.Request.QueryString("TrnsID") & ";" & Page.Request.QueryString("WF")
                    End If
                    TBHEADERWF.Style("background-image") = IIf(ProfileCls.CurrentTheme().Contains("Blue") = True, "url('../../Common/Images/BlueHeader.png');", "url('../../Common/Images/SilverHeader.png');")
                Else
                    TBHEADERWF.Visible = False
                    TBHEADER.Visible = True
                    HiddenField_WFStage.Value = "0"
                    TBHEADER.Style("background-image") = IIf(ProfileCls.CurrentTheme().Contains("Blue") = True, "url('../../Common/Images/BlueHeader.png');", "url('../../Common/Images/SilverHeader.png');")
                End If
                HiddenField_Lang.Value = IIf(ProfileCls.CurrentLanguage() = "Ar", "Ar", "En")
            End If
            Dim _Clshrs_Positions As New Clshrs_Positions(Page)
            Dim StrCommand As String = "select ID,isnull((isnull(ArbName,'') + ' ' + isnull(FamilyArbName,'') + ' ' + isnull(FatherArbName,'') + ' ' + isnull(GrandArbName,'')) , (isnull(EngName,'') + ' ' + isnull(FamilyEngName,'') + ' ' + isnull(FatherEngName,'') + ' ' + isnull(GrandEngName,''))) AS ArbName ," & _
                                       "isnull((isnull(EngName,'') + ' ' + isnull(FamilyEngName,'') + ' ' + isnull(FatherEngName,'') + ' ' + isnull(GrandEngName,'')),(isnull(ArbName,'') + ' ' + isnull(FamilyArbName,'') + ' ' + isnull(FatherArbName,'') + ' ' + isnull(GrandArbName,''))) AS EngName " & _
                                       "from hrs_Employees where CancelDate is null and ExcludeDate is null"
            Dim EmployeeDataset As New Data.DataSet
            EmployeeDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Clshrs_Positions.ConnectionString, System.Data.CommandType.Text, StrCommand)
            RetDropDownList(Me.DropDownList_Employee, EmployeeDataset.Tables(0), "ID", IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")).DataBind()
            RetDropDownList(Me.DropDownList_PeopleNotify, EmployeeDataset.Tables(0), "ID", IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")).DataBind()
            For i As Integer = 0 To EmployeeDataset.Tables(0).Rows.Count - 1
                Try
                    UWG_Peoples.DisplayLayout.Bands(0).Columns(0).ValueList.ValueListItems.Add(EmployeeDataset.Tables(0).Rows(i)("ID"), EmployeeDataset.Tables(0).Rows(i)(IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")))
                    UWG_PeoplrNotify.DisplayLayout.Bands(0).Columns(0).ValueList.ValueListItems.Add(EmployeeDataset.Tables(0).Rows(i)("ID"), EmployeeDataset.Tables(0).Rows(i)(IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")))
                Catch ex As Exception
                    Continue For
                End Try
            Next
            Dim _Syscompany As New Clssys_Companies(Me.Page)
            _Syscompany = ProfileCls.RetCompany()
            If _Syscompany.IsHigry = True Then
                HiddenField_Cal.Value = "1"
            Else
                HiddenField_Cal.Value = "0"
            End If
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Request.QueryString.Count > 5 Then
            TBHEADERWF.Visible = False
            TBHEADER.Visible = False
            HiddenField1.Value = 0
            TBHEADERPRINT.Visible = True
        Else
            HiddenField1.Value = 1
            TBHEADERPRINT.Visible = False
        End If
    End Sub
    'Private Sub WfPrintCommand() Handles ImageButton_Printing.Click
    '    Dim StrCriteria As String = Request.QueryString.Item("Criteria")
    '    Dim StrReportCode As String = Request.QueryString.Item("ReportCode")
    '    Dim StrIncomingLanguage As String = Request.QueryString.Item("Language")
    '    Dim StrSqlNames As String = Request.QueryString.Item("sq0")
    '    Dim strV As String = Request.QueryString.Item("v")
    '    Dim StrNames() As String = StrCriteria.Split("|")
    '    Dim strSql() As String = StrSqlNames.Split("|")
    '    Dim strSqlv() As String = strV.Split("|")
    '    Dim intPreview As Integer = Request.QueryString.Item("preview")
    '    Dim IntReportID As Integer
    '    Dim dsTemp As New DataSet
    '    Dim WebHandler As New Venus.Shared.Web.WebHandler
    '    Dim StrLanguage As String = String.Empty
    '    Dim _culture As String = StrLanguage
    '    Dim ObjDtReportViewColumn As New DataTable
    '    Dim ObjDsReportProperties As New DataSet
    '    Dim ObjDtReportCriteriaColumn As New Data.DataTable

    '    'ClsReports = New ClsRpw_ReportsProperties(Page)
    '    'ClsReportsColumns = New ClsRpw_ReportColumnsProperties(Page)
    '    'ClsReportsCriterias = New ClsRpw_ReportsCriterias(Me.Page)
    '    'ClsReportsMain = New ClsRpw_ReportsMain(Me.Page)
    '    'StrSqlNames = StrSqlNames.Replace("|", " AND ")
    '    'StrSqlNames = StrSqlNames.Replace("$", "%")
    '    'WebHandler.GetCookies(Page, "Lang", StrLanguage)

    '    'ClsReports.Find("Code='" & StrReportCode & "'")
    '    'IntReportID = ClsReports.ID
    '    'ClsReportsCriterias.Find("ReportID = " & ClsReports.ID & " Order By Rank")
    '    'ClsReportsColumns.Find("ReportID = " & ClsReports.ID & " Order By Rank")
    '    'ClsReportsCriterias.CreateParametersForView(pnlCriterias, ClsReportsCriterias.DataSet.Tables(0), StrNames, strSqlv)
    '    'ObjDsReportProperties = ClsReports.CreateDataSet4Designer()
    '    'ObjDtReportCriteriaColumn = ClsReportsCriterias.DataSet.Tables(0)
    '    'If Not IsPostBack Then
    '    '    clearSession()
    '    '    ClsReports.GetReportsProperties(IntReportID, ObjDsReportProperties)
    '    '    ClsReports.GenerateFieldsDs(ObjDtReportViewColumn, IntReportID, True)
    '    'Else
    '    '    ObjDtReportViewColumn = Session("SessionView")
    '    '    ObjDsReportProperties = Session("SessionReportProperties")
    '    '    ObjDtReportCriteriaColumn = Session("SessionCriteria")
    '    'End If

    '    'Session("SessionReportProperties") = ObjDsReportProperties
    '    'Session("SessionView") = ObjDtReportViewColumn
    '    'Session("SessionCriteria") = ObjDtReportCriteriaColumn

    '    'If intPreview = 1 Then
    '    '    Response.Redirect("frmReportViewer.aspx?Language=" & IIf(StrIncomingLanguage = "true", 1, 0))
    '    'ElseIf intPreview = 2 Then
    '    '    Session("rpt") = "XLS"
    '    '    Response.Redirect("frmReportViewer.aspx?Language=" & IIf(StrIncomingLanguage = "true", 1, 0))
    'End Sub
    Public Shared Function RetDropDownList(ByRef DDL As DropDownList, ByRef DT As System.Data.DataTable, ByVal valueMember As String, ByVal DisMember As String) As DropDownList
        DDL.DataSource = DT
        DDL.DataValueField = valueMember
        DDL.DataTextField = DisMember
        Return (DDL)
    End Function

    Protected Sub WorkFlowPrint()
        Dim clsDocument As New ClsDwf_Documents(Page)
        Dim clsReport As New Clssys_Reports(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsDocument.ConnectionString)
        Dim strDocumentID As String = Request.QueryString.Item("DocumentID")
        Dim strTrnID As String = Request.QueryString.Item("TrnsID")
        Dim StrIncomingLanguage As String = Request.QueryString.Item("Language")
        Dim strfilename As String = strTrnID
        clsDocument.Find("ID=" & CInt(strDocumentID))
        If Not IsNumeric(clsDocument.ID) Or clsDocument.ID <= 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Document does not have report !/!المستند لا يوجد لديه تقرير"))
            Exit Sub
        End If
        Dim str_REPORT_CODE As String = "CF_" + clsDocument.Code
        If Not clsReport.Find("Code='" & str_REPORT_CODE & "'") Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Document does not have report !/!المستند لا يوجد لديه تقرير"))
            Exit Sub
        End If

        HiddenField1.Value = IIf(Request.QueryString.Item("Language") = 0, "0", "1")


        Dim ClsReports = New ClsRpw_ReportsProperties(Page)
        Dim ClsReportsColumns = New ClsRpw_ReportColumnsProperties(Page)
        Dim ClsReportsCriterias = New ClsRpw_ReportsCriterias(Me.Page)
        Dim ObjDtReportViewColumn As New DataTable
        Dim ObjDsReportProperties As New DataSet
        Dim ObjDtReportCriteriaColumn As New Data.DataTable

        ClsReports.Find("Code='" & str_REPORT_CODE & "'")
        Dim IntReportID = ClsReports.ID
        ClsReportsCriterias.Find("ReportID = " & ClsReports.ID & " Order By Rank")
        ClsReportsColumns.Find("ReportID = " & ClsReports.ID & " Order By Rank")

        ObjDsReportProperties = ClsReports.CreateDataSet4Designer()
        ClsReportsCriterias.DataSet.Tables(0).Rows(0).Item("DefaultValue") = strTrnID
        ObjDtReportCriteriaColumn = ClsReportsCriterias.DataSet.Tables(0)
        ClsReports.GetReportsProperties(IntReportID, ObjDsReportProperties)
        ClsReports.GenerateFieldsDs(ObjDtReportViewColumn, IntReportID, True)

        Session("SessionReportProperties") = ObjDsReportProperties
        Session("SessionView") = ObjDtReportViewColumn
        Session("SessionCriteria") = ObjDtReportCriteriaColumn

        Response.Redirect("../../Interfaces/frmReportViewer.aspx?Language=" & IIf(StrIncomingLanguage = "true", 1, 0))

        'Dim XDocument As New System.Xml.XmlDocument
        'Dim DsReprotContent As Data.DataSet = ObjDsReportProperties
        'Dim ObjReportCriteria As Data.DataTable = ObjDtReportCriteriaColumn
        'Dim ClsReportsDriver As New ReportViewer(HiddenField1.Value)
        'Dim ClsUsers As New Clssys_Users(Page)
        'Dim ClsCompanies As New Clssys_Companies(Page)
        'ClsReportsDriver.UserCode = ClsUsers.Code
        'ClsReportsDriver.UserName = IIf(HiddenField1.Value = 0, ClsUsers.EngName, ClsUsers.ArbName)
        'ClsReportsDriver.CompanyName = IIf(HiddenField1.Value = 0, ClsCompanies.EngName, ClsCompanies.ArbName)
        'ClsReportsDriver.CompanyHeader_1 = ConfigurationManager.AppSettings.Item("CompanyHeader_1")
        'ClsReportsDriver.CompanyHeader_2 = ConfigurationManager.AppSettings.Item("CompanyHeader_1")
        'ClsReportsDriver.CompanyHeader_3 = ConfigurationManager.AppSettings.Item("CompanyHeader_1")

        'ClsReportsDriver.CompanyID = ProfileCls.RetCompany.ID
        'Dim IntIncomingLanguage As Integer = IIf(HiddenField1.Value = 0 = "1", 1, 0)
        'If ClsReports.XMLReport = String.Empty Then
        '    Dim str_Path = Server.MapPath("ReportTempletes_Folder\" & ClsReports.Code & ".xml").Replace("Pages\DocumentWF", "Interfaces")
        '    XDocument = ClsReportsDriver.ViewReportDetails(str_Path, ClsReports, ObjReportCriteria)
        'Else
        '    XDocument = ClsReportsDriver.ViewReportDetailsxml(ClsReports.XMLReport, ClsReports, ObjReportCriteria, IntIncomingLanguage)
        'End If
        'Dim str_Save_Path = Server.MapPath("~/C1ReportXML/" & strfilename & ".xml")
        'XDocument.Save(Server.MapPath("~/C1ReportXML/" & strfilename & ".xml"))
        'C1ReportViewer1.FileName = "~/C1ReportXML/" & strfilename & ".xml"
    End Sub

    Protected Sub ImageButton_PrintReport_Click(sender As Object, e As ImageClickEventArgs)
        WorkFlowPrint()
    End Sub
End Class
