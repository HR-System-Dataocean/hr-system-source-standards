Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports Venus.Shared

Partial Class frmEmployeesVacations
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees
    Dim ClsAppraisal As ClsHrs_AppraisalCreate
    Dim ClsAppraisalCriteria As ClsHrs_AppraisalCreate
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const csOtherFields = 11
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim recordID As Integer
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try

            ClsEmployeesVacations.AddNotificationOnChange(Page)
            ClsAppraisal = New ClsHrs_AppraisalCreate(Me)

            Dim csSearchID As Integer
            Dim ClsLevels As New Clshrs_LevelTypes(Page)
            Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
            Dim ClsDataHandler As New Venus.Shared.DataHandler
            Dim StrSerial As String = String.Empty
            ClsEmployees = New Clshrs_Employees(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
            Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
            Dim ClsObjects As New Clssys_Objects(Page)
            Dim ClsSearchs As New Clssys_Searchs(Page)
            Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
            'txtEmployee.Attributes.Add("onchange", "ChangeIsDataChanged()")
            ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
            ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim() & "'")
            ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
            csSearchID = ClsSearchs.ID
            If Not IsPostBack Then
                ClsAppraisal.GetDropDownList(ddlAppraisals, True)

                lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lage", lblLage.Text)
                Dim IntDimension As Integer = 510
                'Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployee.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtEmployee.ClientID & "'"
                'btnEmployee.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)



                Session.Add("EmpVacID", 0)
                Dim str As String = lbVactionID.Text
                'txtEmployee.Focus()
                ClsVacationTypes.GetList(uwgEmployeeAppraisal.Bands(0).Columns(0).ValueList)
                ClsVacationTypes.GetList(uwgEmployeeAppraisal.Bands(1).Columns(0).ValueList)

                ClsVacationTypes.Find("IsAnnual=1")
                If ClsVacationTypes.ID > 0 Then
                    hdnAnnualVacId.Value = ClsVacationTypes.ID
                Else
                    hdnAnnualVacId.Value = 0
                End If
            End If
            If (lbVactionID.Text <> "") Then
                ClsEmployeesVacations.Find("ID=" & lbVactionID.Text)
                recordID = ClsEmployeesVacations.ID
                EmpVacationId.Value = recordID

                If (recordID > 0) Then
                    SetScreenInformation("E")
                    SetToolBarRecordPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, ClsEmployeesVacations.Table, recordID)
                Else
                    SetScreenInformation("N")

                End If
            Else
                SetScreenInformation("N")

            End If
            If ClsObjects.Find(" Code='" & ClsEmployeesVacations.Table.ToString.Trim() & "'") Then
                ImageButton_Documents.Attributes.Add("onclick", "OpenModal3('frmAttachDocuments.aspx?OId=" & ClsObjects.ID & "&',400,600,true,''); return false;")
            End If

            If Not IsPostBack Then
                Dim DteStartDate As Date = Date.Now
                Dim DteEndDate As Date = Date.Now
                If Request.QueryString.Count > 0 Then
                    If Request.QueryString.Item("EmpCode") <> Nothing Then
                        'txtEmployee.Text = Request.QueryString.Item("EmpCode")
                        ' txtEmployee_TextChanged(Nothing, Nothing)
                    End If
                    If Request.QueryString.Item("StartDate") <> Nothing Then
                        DteStartDate = Request.QueryString.Item("StartDate")
                    End If
                    If Request.QueryString.Item("ToDate") <> Nothing Then
                        DteEndDate = Request.QueryString.Item("ToDate")
                    End If
                End If

            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GetAppraisalResult(ddlAppraisals.SelectedValue)
    End Sub

    'Protected Sub txtEmployee_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployee.TextChanged
    '    Try
    '        Session("EmpVacID") = 0
    '        CheckEmpCode()
    '        Dim ClsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
    '        ClsEmployees = New Clshrs_Employees(Page)
    '        Dim clsContract As New Clshrs_Contracts(Page)
    '        Dim clsEmployeesTransactions = New Clshrs_EmployeesTransactions(Page)
    '        Dim clsEmployeeClass As New Clshrs_EmployeeClasses(Page)
    '        Dim intEmpCalssStartHour As Integer = 9
    '        Dim intEmpCalssStartMinutes As Integer = 0
    '        Dim wHoursPerDay As Double = 9
    '        ClsEmployees.Find("Code='" & txtEmployee.Text & "'")




    '        clsEmployeeClass.Find("ID=" & IIf(clsContract.EmployeeClassID > 0, clsContract.EmployeeClassID, 0))
    '        If (lbVactionID.Text.Trim = "" Or lbVactionID.Text.Trim = "0") Then
    '            If clsEmployeeClass.ID > 0 Then
    '                If IsNothing(clsEmployeeClass.WorkHoursPerDay) Then
    '                    wHoursPerDay = 9
    '                Else
    '                    wHoursPerDay = clsEmployeeClass.WorkHoursPerDay
    '                End If
    '                hdnWorkingHoursPerDay.Value = wHoursPerDay
    '                If Not IsNothing(clsEmployeeClass.DefultStartTime) Then
    '                    intEmpCalssStartHour = CDate(clsEmployeeClass.DefultStartTime).Hour
    '                    intEmpCalssStartMinutes = CDate(clsEmployeeClass.DefultStartTime).Minute
    '                End If
    '                Dim chkIn As New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, intEmpCalssStartHour, intEmpCalssStartMinutes, 0)
    '                Dim chkOut As DateTime = chkIn.AddHours(wHoursPerDay)
    '            End If


    '        End If
    '    Catch ex As Exception
    '        mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
    '        Page.Session.Add("ErrorValue", ex)
    '        mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
    '        Page.Response.Redirect("ErrorPage.aspx")
    '    End Try
    'End Sub


    Protected Sub ddlApraisal_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAppraisals.SelectedIndexChanged


        ClsEmployees = New Clshrs_Employees(Page)
        Dim ObjDataset As DataSet
        Dim ObjDataset2 As DataSet

        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        Dim StrSelectCommand As String
        If ddlAppraisals.SelectedValue > 0 Then
            StrSelectCommand = "select App_Criteria.ID,EngName,ArbName from App_Criteria join APP_AppraisalCriterias on App_Criteria.id=APP_AppraisalCriterias.CriteriaID where APP_AppraisalCriterias.AppraisalID = " & ddlAppraisals.SelectedValue & ""
            Dim Item As Global.System.Web.UI.WebControls.ListItem


            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, StrSelectCommand)
            ddlCriteria.Items.Clear()


            Item = New Global.System.Web.UI.WebControls.ListItem
            Item.Text = ObjNavigationHandler.SetLanguage(Me, "[Select Your Choice]/[ برجاء الاختيار ]")
            Item.Value = "0"
            ddlCriteria.Items.Add(Item)

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjDataRow(ObjNavigationHandler.SetLanguage(Me, "EngName/ArbName"))
                If (Item.Text.Trim = "") Then
                    Item.Text = ObjDataRow(ObjNavigationHandler.SetLanguage(Me, "ArbName/EngName"))
                End If
                Item.Value = ObjDataRow("ID")
                ddlCriteria.Items.Add(Item)
            Next
            ''''''Fill Employees DDl
            '''
            Dim EmpName As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " dbo.fn_GetEmpName(Hrs_employees.Code,1) As ArbName "
            Else
                EmpName = " dbo.fn_GetEmpName(Hrs_employees.Code,0) As EngName "

            End If
            Dim StrSelectCommand2 As String = "select Hrs_employees.ID, " & EmpName & "  from Hrs_employees join App_Appraisals on Hrs_employees.id=App_Appraisals.EmployeeID where App_Appraisals.ID=" & ddlAppraisals.SelectedValue & " "

            Dim Item2 As Global.System.Web.UI.WebControls.ListItem


            ObjDataset2 = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, StrSelectCommand2)
            ddlEmployees.Items.Clear()


            Item2 = New Global.System.Web.UI.WebControls.ListItem
            Item2.Text = ObjNavigationHandler.SetLanguage(Me, "[Select Your Choice]/[ برجاء الاختيار ]")
            Item2.Value = "0"
            ddlEmployees.Items.Add(Item2)

            For Each ObjDataRow In ObjDataset2.Tables(0).Rows
                Item2 = New Global.System.Web.UI.WebControls.ListItem
                Item2.Text = ObjDataRow(ObjNavigationHandler.SetLanguage(Me, "EngName/ArbName"))
                If (Item2.Text.Trim = "") Then
                    Item2.Text = ObjDataRow(ObjNavigationHandler.SetLanguage(Me, "ArbName/EngName"))
                End If
                Item2.Value = ObjDataRow("ID")
                ddlEmployees.Items.Add(Item2)
            Next
        Else
            ddlCriteria.Items.Clear()
            ddlEmployees.Items.Clear()


        End If


    End Sub

    Protected Sub uwgEmployeeAppraisal_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgEmployeeAppraisal.InitializeRow
        Dim clsCompanies As New Clssys_Companies(Page)
        'With clsCompanies
        '    'e.Row.Cells.FromKey("actualstartdate").Value = .GetHigriDate(e.Row.Cells.FromKey("actualstartdate").Value) & IIf(Not IsNothing(e.Row.Cells.FromKey("actualstartdate").Value), " " & e.Row.Cells.FromKey("actualstartdate").Value.ToString().Split(" ")(1) & " " & e.Row.Cells.FromKey("actualstartdate").Value.ToString().Split(" ")(2), "")
        '    'If Not IsNothing(e.Row.Cells.FromKey("actualenddate").Value) Then
        '    '    e.Row.Cells.FromKey("actualenddate").Value = .GetHigriDate(e.Row.Cells.FromKey("actualenddate").Value) & IIf(Not IsNothing(e.Row.Cells.FromKey("actualenddate").Value), " " & e.Row.Cells.FromKey("actualenddate").Value.ToString().Split(" ")(1) & " " & e.Row.Cells.FromKey("actualenddate").Value.ToString().Split(" ")(2), "")
        '    'End If
        'End With
    End Sub


    Protected Sub uwgEmployeeAppraisal_SelectedRowsChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs) Handles uwgEmployeeAppraisal.SelectedRowsChange

        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeAppraisal.Rows
            row.Style.BackColor = System.Drawing.Color.Transparent
        Next
        If e.SelectedRows.Count > 0 Then
            e.SelectedRows.Item(0).Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#92c2fb")
        End If

        Try
            LinkButton_OverDueMessage.Visible = False




            Dim clsEmp As New Clshrs_Employees(Page)
            Dim clsEmpVac As New Clshrs_EmployeesVacations(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmpVac.ConnectionString)


        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Private Functions"
    Private Function SetNew() As Boolean

    End Function
    Private Sub AddOnChangeEventToControls(ByVal formName As String)
        Try
            Dim clsForms As New ClsSys_Forms(Page)
            clsForms.Find(" code = REPLACE('" & formName & "',' ','')")
            Dim clsFormsControls As New Clssys_FormsControls(Page)
            If clsForms.ID > 0 Then
                clsFormsControls.Find(" FormID=" & clsForms.ID)
                Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()
                For Each row As Data.DataRow In tab.Rows
                    Dim currCtrl As Control = Me.FindControl(row("Name"))
                    If TypeOf (currCtrl) Is TextBox Then
                    End If
                Next
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub







    Public Function SetToolBarRecordPermission(ByVal pgSender As System.Web.UI.Page, ByVal ConnectionString As String, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal StrTableName As String, ByVal RecordID As Integer) As Boolean
        Dim StrCommandStored As String
        Dim StrFormName As String
        Dim ObjDataSet As New Data.DataSet
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Try
            StrFormName = pgSender.Form.ID
            StrCommandStored = "hrs_GetRecordsPermissions"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, StrCommandStored, UserID, GroupID, Replace(StrTableName, " ", ""), RecordID)
            If Venus.Shared.DataHandler.CheckValidDataObject(ObjDataSet) Then
                With ObjDataSet.Tables(0).Rows(0)

                    If ImageButton_Save.Enabled = True And .Item("CanEdit") = True Then
                        ImageButton_Save.Enabled = Not .Item("CanEdit")

                    End If

                    If ImageButton_Delete.Enabled = True And .Item("CanDelete") = True Then
                        ImageButton_Delete.Enabled = Not .Item("CanDelete")
                    End If

                    If ImageButton_Print.Enabled = True And .Item("CanPrint") = True Then
                        ImageButton_Print.Enabled = Not .Item("CanPrint")
                    End If
                End With
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SetToolBarPermission(ByVal pgSender As System.Web.UI.Page, ByVal ConnectionString As String, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal Mode As String) As Boolean
        Dim StrCommandStored As String
        Dim StrFormName As String
        Dim ObjDataSet As New Data.DataSet
        Try
            StrFormName = pgSender.Form.ID
            StrCommandStored = "hrs_GetFormsPermissions"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, StrCommandStored, UserID, GroupID, StrFormName)
            If Venus.Shared.DataHandler.CheckValidDataObject(ObjDataSet) Then
                With ObjDataSet.Tables(0).Rows(0)
                    ImageButton_Delete.Enabled = .Item("AllowDelete")
                    ImageButton_Print.Enabled = .Item("AllowPrint")
                    Select Case Mode
                        Case "N", "R"
                            ImageButton_Save.Enabled = .Item("AllowAdd")

                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")

                    End Select
                End With
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType
                Case "N", "R"


                    ImageButton_Delete.Enabled = False
                    ImageButton_Properties.Visible = False
                    LinkButton_Properties.Visible = False
                    ImageButton_Remarks.Visible = False
                    LinkButton_Remarks.Visible = False

                Case "D"
                    ClsEmployeesVacations.Find("ID=" & intID)

                    ImageButton_Save.Visible = False

                Case "E"
                    ClsEmployeesVacations.Find("ID=" & intID)

                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEmployeesVacations
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Me.Page)
        Try
            With ClsEmployeesVacations
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Page, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
            End With
        Catch ex As Exception
        End Try
    End Function

    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployeesVacations.Table) = True Then
            Dim StrTablename As String
            ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
            StrTablename = ClsEmployeesVacations.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmRegions")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmRegions")
            End If
        End If
    End Function
    Public Function GetRecordInfoAjax(ByVal recordID As Integer) As Boolean
        Try
            Dim dsContractsTransactions As New DataSet
            Dim dsUser As New DataSet
            Dim clsUser As New Clssys_Users(Page)
            Dim clsContr As New Clshrs_ContractsTransactions(Page)
            clsContr.Find("ID=" & recordID)
            If clsContr.ID > 0 Then
                If Not clsContr.RegUserID = Nothing Then
                    clsUser.Find("ID=" & clsContr.RegUserID)

                End If
                If Convert.ToDateTime(clsContr.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = clsContr.RegDate
                End If

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


#End Region

#Region "Shared Function"
    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table
        Dim mSqlDataAdapter As New SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter & " And CancelDate IS Null", " Where CancelDate IS Null")
            StrSelectCommand = StrSelectCommand '& orderByStr
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, ConnStr)
            DataSet = New DataSet
            mSqlDataAdapter.Fill(DataSet)
            If DataSet.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        End Try
    End Function

#End Region

#Region "PageMethods"
    <System.Web.Services.WebMethod()>
    Public Shared Function GetEmployeeID(ByVal strEmpCode As String) As String
        Dim dsEmp As New DataSet
        If Find("hrs_Employees", "Code='" & strEmpCode & "'", dsEmp) Then
            Return dsEmp.Tables(0).Rows(0).Item("ID").ToString
        Else
            Return "0"
        End If
    End Function

    Private Function GetAppraisalResult(AppraisalID As Integer)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisal.ConnectionString)

        If ddlAppraisals.SelectedValue = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Appraisal /برجاء اختيار التقييم"))
        Else
            ClsEmployees = New Clshrs_Employees(Page)
            Dim ObjDataset As DataSet
            Dim ConnectionString As String
            ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()


            If ddlAppraisals.SelectedValue > 0 Then
                Dim Criteria As String = " Where 1=1 "
                Dim CriteriaNAme As String = ""
                Dim EmpName As String = ""
                If ProfileCls.CurrentLanguage = "Ar" Then
                    CriteriaNAme = " App_Criteria.ArbName "
                    EmpName = " dbo.fn_GetEmpName(Hrs_Employees.code,1)  "

                Else
                    CriteriaNAme = " App_Criteria.EngName "
                    EmpName = " dbo.fn_GetEmpName(Hrs_Employees.code,0)  "
                End If

                Dim str1 As String
                'str1 = " SELECT dbo.App_AppraisalResult.AppraisalID, " & EmpName & " AS EmployeeName, dbo.App_AppraisalResult.EmployeeID, dbo.App_AppraisalResult.CriteriaID, " & CriteriaNAme & " AS Criteria, dbo.APP_AppraisalCriterias.MinimumScore,dbo.APP_AppraisalCriterias.MaximumScore, SUM(cast(dbo.App_AppraisalResult.Score AS FLOAT))/count ([App_AppraisalConfigurations].ID) AS Result, dbo.APP_AppraisalCriterias.Weight, round( (SUM(CAST(dbo.App_AppraisalResult.Score AS FLOAT))/count ([App_AppraisalConfigurations].ID)/ dbo.APP_AppraisalCriterias.MaximumScore )*  dbo.APP_AppraisalCriterias.Weight,2) As AppCriteriaScore FROM     dbo.App_AppraisalResult INNER JOIN dbo.App_Criteria ON dbo.App_AppraisalResult.CriteriaID = dbo.App_Criteria.ID INNER JOIN dbo.APP_AppraisalCriterias ON dbo.APP_AppraisalCriterias.CriteriaID = dbo.App_Criteria.ID and APP_AppraisalCriterias.AppraisalID=App_AppraisalResult.AppraisalID Inner join [dbo].[App_AppraisalNotifications] on App_AppraisalResult.NotificationID= App_AppraisalNotifications.ID Inner Join [dbo].[App_AppraisalConfigurations] on App_AppraisalNotifications.ConfigurationID=App_AppraisalConfigurations.ID INNER JOIN dbo.hrs_Employees ON dbo.App_AppraisalResult.EmployeeID = dbo.hrs_Employees.ID GROUP BY dbo.App_Criteria.ArbName, dbo.APP_AppraisalCriterias.MinimumScore, dbo.APP_AppraisalCriterias.MaximumScore, dbo.APP_AppraisalCriterias.Weight, dbo.App_AppraisalResult.AppraisalID, dbo.App_AppraisalResult.CriteriaID,dbo.App_AppraisalResult.EmployeeID, dbo.App_AppraisalResult.CriteriaID, dbo.hrs_Employees.Code, dbo.hrs_Employees.EngName, dbo.App_AppraisalResult.AppraisalID,dbo.App_Criteria.EngName "
                str1 = " SELECT dbo.V_AppraisalResult.AppraisalID, " & EmpName & " AS EmployeeName, dbo.V_AppraisalResult.EmployeeID, dbo.V_AppraisalResult.CriteriaID, V_AppraisalResult.CriteriaName   AS Criteria, dbo.V_AppraisalResult.MinimumScore,dbo.V_AppraisalResult.MaximumScore, V_AppraisalResult.AppraisalScore AS Result FROM     dbo.V_AppraisalResult  INNER JOIN dbo.hrs_Employees ON dbo.V_AppraisalResult.EmployeeID = dbo.hrs_Employees.ID  "
                If ddlAppraisals.SelectedValue > 0 Then

                    Criteria &= " And V_AppraisalResult.AppraisalID = " & ddlAppraisals.SelectedValue & ""

                End If
                If ddlCriteria.SelectedValue > 0 Then
                    Criteria &= " And V_AppraisalResult.CriteriaID = " & ddlCriteria.SelectedValue & ""

                End If
                If ddlEmployees.SelectedValue > 0 Then
                    Criteria &= " And V_AppraisalResult.EmployeeID = " & ddlEmployees.SelectedValue & ""

                End If
                str1 &= Criteria
                ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, str1)
                If ObjDataset.Tables(0).Rows.Count > 0 Then
                    uwgEmployeeAppraisal.DataSource = ObjDataset.Tables(0)
                    uwgEmployeeAppraisal.DataBind()
                Else
                    uwgEmployeeAppraisal.DataSource = Nothing
                    uwgEmployeeAppraisal.DataBind()
                End If

            Else
                uwgEmployeeAppraisal.DataSource = Nothing

            End If
        End If
    End Function


#End Region





End Class
