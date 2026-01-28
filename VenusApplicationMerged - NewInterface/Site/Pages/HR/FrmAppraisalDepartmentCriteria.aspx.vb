Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Venus.Shared.Web
Imports Infragistics.Documents.Excel.Serialization
Imports Venus.Shared
Imports Infragistics.WebUI.UltraWebGrid
Imports Infragistics.Web.UI.ListControls
Imports System.Activities.Statements

Partial Class frmEmployeesVacations
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const csOtherFields = 11
    Private deletedString As String = ""
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim recordID As Integer
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try

            If Not IsPostBack Then


                Dim WebHandler As New Venus.Shared.Web.WebHandler
                Dim User As String = String.Empty
                ClsEmployees = New Clshrs_Employees(Page)
                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")
                uwgDepartmentcriteria.Rows.Clear()

                'uwgDepartmentcriteria.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Nothing, Nothing}))
                GetDropDownList()
                ' GetDropDownListGrid()

                ClsEmployeesVacations.AddNotificationOnChange(Page)
                Dim csSearchID As Integer
                Dim ClsLevels As New Clshrs_LevelTypes(Page)
                Dim ClsDataHandler As New Venus.Shared.DataHandler
                Dim StrSerial As String = String.Empty
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
                Dim ClsObjects As New Clssys_Objects(Page)
                Dim ClsSearchs As New Clssys_Searchs(Page)
                Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
                lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lage", lblLage.Text)
                If (recordID > 0) Then
                    SetScreenInformation("E")
                    SetToolBarRecordPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, ClsEmployeesVacations.Table, recordID)
                Else
                    SetScreenInformation("N")
                    If Not IsPostBack Then
                        SetTime()
                    End If
                End If
            Else

            End If



        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Delete.Command
        Dim IntId As Integer
        Dim strMode As String
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Page)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)
        Select Case e.CommandArgument

            Case "SaveNew"
                SavePart()

            Case "Save"
                'If txtEmployee.Text = "" Then
                '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                '    Exit Sub
                'End If
                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                    'ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">OpenPrintedScreen(" & ClsEmployees.ID & ");</script>")
                    SetNew()
                End If
            Case "New"
                SetNew()
            Case "Delete"

                SetNew()


            Case "First"
                'ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                'ClsEmployees.FirstRecord()
                'txtEmployee.Text = ClsEmployees.Code
                'txtEmployee_TextChanged(Nothing, Nothing)
            Case "Previous"
                'ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                'If Not ClsEmployees.previousRecord() Then
                '    ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                'End If
                'txtEmployee.Text = ClsEmployees.Code
                'txtEmployee_TextChanged(Nothing, Nothing)
            Case "Next"
                'ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                'If Not ClsEmployees.NextRecord() Then
                '    ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                'End If
                'txtEmployee.Text = ClsEmployees.Code
                'txtEmployee_TextChanged(Nothing, Nothing)
            Case "Last"
                'ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                'ClsEmployees.LastRecord()
                'txtEmployee.Text = ClsEmployees.Code
                'txtEmployee_TextChanged(Nothing, Nothing)
        End Select

        SetToolBarPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, strMode)
        SetToolBarRecordPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, ClsEmployeesVacations.Table, IntId)
        If strMode = "N" Then
            'ImageButton_reelete.Enabled = False
        End If
        deletedString = ""
        hdDeletedStr.Value = ""
    End Sub
    Protected Sub ddlDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartment.SelectedIndexChanged
        uwgDepartmentcriteria.Rows.Clear()
        GetDeprtmentCriteria()
    End Sub

#End Region

#Region "Private Functions"

    Public Function GetDropDownList() As Boolean
        Dim Item As Global.System.Web.UI.WebControls.ListItem

        Dim ConnectionString As String
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strselect As String
        strselect = "select * from sys_Departments where CancelDate is null  "
        Dim DSRequest As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
        If DSRequest.Tables(0).Rows.Count > 0 Then



            ddlDepartment.Items.Clear()
            Item = New Global.System.Web.UI.WebControls.ListItem
            Item.Text = ObjNavigationHandler.SetLanguage(Page, "[Select Your Choice]/[ برجاء الاختيار ]")
            Item.Value = 0
            ddlDepartment.Items.Add(Item)
            For Each Row As Data.DataRow In DSRequest.Tables(0).Rows

                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & "")
                Item.Value = Row("ID")

                ddlDepartment.Items.Add(Item)
            Next
        End If
        Dim CriteriaName As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            CriteriaName = "ArbName"
        Else
            CriteriaName = "EngName"

        End If
        Dim strselect2 As String
        strselect2 = "select ID," & CriteriaName & " as CriteriaName  from App_Criteria"
        Dim DSCriterias As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)
        If DSCriterias.Tables(0).Rows.Count > 0 Then
            uwgDepartmentcriteria.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Clear()
            For Each Row As Data.DataRow In DSCriterias.Tables(0).Rows
                uwgDepartmentcriteria.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Add(Row("ID"), Row("CriteriaName"))
            Next

        End If


        Return True
    End Function
    'Protected Sub uwgDepartmentcriteria_Cellchanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgDepartmentcriteria.UpdateCell

    '    'If e.Cell.Row.Cells(1).Value = "1" Then
    '    '    e.Cell.Row.Cells(2).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No



    '    'End If
    '    'If e.Cell.Row.Cells(1).Value = "2" Then
    '    '    e.Cell.Row.Cells(2).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes


    '    'End If

    'End Sub
    'Public Function GetDropDownListGrid() As Boolean

    '    Dim Item As Global.System.Web.UI.WebControls.ListItem

    '    Dim ConnectionString As String
    '    ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
    '    Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

    '    Dim strselect2 As String
    '    strselect2 = "select * from hrs_Positions"
    '    Dim DSPositions As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)
    '    If DSPositions.Tables(0).Rows.Count > 0 Then
    '        uwgDepartmentcriteria.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Clear()
    '        For Each Row As Data.DataRow In DSPositions.Tables(0).Rows
    '            uwgDepartmentcriteria.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & ""))
    '        Next

    '    End If

    '    Dim EmpName As String

    '    If ProfileCls.CurrentLanguage = "Ar" Then
    '        EmpName = "ISNULL( hrs_Employees.arbname,' ')+' '+ISNULL(FatherArbName,' ')+' '+isnull(GrandArbName,' ')+' '+isnull(FamilyArbName,' ')"
    '    Else
    '        EmpName = " ISNULL(hrs_Employees.EngName,' ')+' '+ISNULL(FatherEngName,' ')+' '+ISNULL(GrandEngName,' ')+' '+ISNULL(FamilyEngName,' ')"
    '    End If
    '    Dim strselect3 As String
    '    strselect3 = "select ID,Code," & EmpName & " as EmpName from hrs_Employees where ExcludeDate is null"
    '    Dim DSEmployees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect3)
    '    If DSEmployees.Tables(0).Rows.Count > 0 Then
    '        uwgDepartmentcriteria.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Clear()
    '        For Each Row As Data.DataRow In DSEmployees.Tables(0).Rows
    '            uwgDepartmentcriteria.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & Row("EmpName"))
    '        Next

    '    End If

    'End Function

    Private Function SetNew() As Boolean
        Dim ClsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
        Try
            ddlDepartment.SelectedValue = 0
            uwgDepartmentcriteria.Rows.Clear()

            uwgDepartmentcriteria.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Nothing, Nothing}))

            'GetDropDownListGrid()
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
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
                    'If TypeOf (currCtrl) Is TextBox Then
                    'End If
                Next
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Private Function SetTime() As Boolean
        Dim clsCompanies As New Clssys_Companies(Page)
        Dim dteNow As Date = Format(Date.Now, "dd/MM/yyyy")
        Try
            With clsCompanies
            End With
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function


    Private Function SavePart() As Boolean
        If ddlDepartment.SelectedValue <> "0" Then
            SaveDG()
            Return True
        Else
            Dim ClsEmployeeVacation As New Clshrs_EmployeesVacations(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeeVacation.ConnectionString)

            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please select Department ! /برجاء اختيار القسم ! "))

            Return False

        End If



        Try
            'SetNew()
            Return True
        Catch ex As Exception
            Return False
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function GetDeprtmentCriteria()
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strselect As String
        Dim CriteriaName As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            CriteriaName = "ArbName"
        Else
            CriteriaName = "EngName"

        End If

        strselect = "select App_Criteria.id,App_Criteria.id as CriteriaName ,App_DepartmentCriteria.ByValue,ByPercentage,MinimumScore,MaximumScore,1 as Include from App_DepartmentCriteria join App_Criteria on App_DepartmentCriteria.CriteriaID=App_Criteria.ID where App_DepartmentCriteria.DepartmentID= " & ddlDepartment.SelectedValue & ""
        Dim DSDepartmentCriteria As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
        If DSDepartmentCriteria.Tables(0).Rows.Count > 0 Then
            'uwgDepartmentcriteria.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Clear()
            'For Each Row As Data.DataRow In DSDepartmentCriteria.Tables(0).Rows
            '    uwgDepartmentcriteria.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Add(Row("ID"), Row("CriteriaName"))
            'Next
            'For Each Row As Data.DataRow In DSDepartmentCriteria.Tables(0).Rows
            '    uwgDepartmentcriteria.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Row(1), Row(2)}))

            'Next
            'GetDropDownListGrid()
            uwgDepartmentcriteria.DataSource = DSDepartmentCriteria
            uwgDepartmentcriteria.DataBind()
            'For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgDepartmentcriteria.Rows
            '    If IsNothing(DGRow.Cells(1).Value) Then
            '        Continue For
            '    End If




            'Next
        End If
    End Function

    Private Function SaveDG() As Boolean
        Try


            Dim DepartmentID As String = "" & ddlDepartment.SelectedValue & ""
            Dim ConnectionString As String = ConfigurationManager.AppSettings("Connstring").ToString()
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            Dim User As String = String.Empty
            WebHandler.GetCookies(Page, "UserID", User)
            Dim Deletecommand As String = "delete from App_DepartmentCriteria  where DepartmentID= " & DepartmentID & ""
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, Deletecommand)



            Dim Include As Boolean = False
            Dim ByValue As Boolean = False
            Dim ByPercentage As Boolean = False
            Dim str As String
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgDepartmentcriteria.Rows
                Include = DGRow.Cells(6).Value
                ByValue = DGRow.Cells(2).Value
                ByPercentage = DGRow.Cells(3).Value
                If Include Then

                    str = "INSERT INTO [dbo].[App_DepartmentCriteria] ([DepartmentID],[CriteriaID],[ByValue],ByPercentage,MinimumScore,MaximumScore,[RegUserId],[RegDate]) VALUES (" & DepartmentID & ",'" & DGRow.Cells(1).Value & "','" & ByValue & "','" & ByPercentage & "' ," & DGRow.Cells(4).Value & ",'" & DGRow.Cells("5").Value & "','" & User & "',GetDate())"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, str)
                End If

            Next




        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function GetValues(ByRef ClsEmployeesVacations As Clshrs_EmployeesVacations) As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ClsNationality As New ClsBasicFiles(Page, "sys_Nationalities")
        Dim ClsUser As New Clssys_Users(Page)
        Try

            SetToolBarDefaults()

            Return True
        Catch ex As Exception
        End Try
    End Function
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
                    GetValues(ClsEmployeesVacations)

                    ImageButton_Save.Visible = False

                Case "E"
                    ClsEmployeesVacations.Find("ID=" & intID)
                    GetValues(ClsEmployeesVacations)

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
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Me.Page)
        If IntId > 0 Then
            ClsEmployeesVacations.Find("ID=" & IntId)
            GetValues(ClsEmployeesVacations)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
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
                    If clsUser.ID > 0 Then
                        lblRegUserValue.Text = clsUser.EngName
                    Else
                        lblRegUserValue.Text = ""
                    End If
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

    Private Sub UltraGrid1_BeforeRowsDeleted(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgDepartmentcriteria.DeleteRow
        Dim ConnectionString As String = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim str = "delete App_DepartmentCriteria  where ID='" & e.Row.Cells(0).Value & "'; "
        deletedString = hdDeletedStr.Value.ToString() + str
        hdDeletedStr.Value = deletedString
    End Sub

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

    Public Shared Function GetFieldDescription(ByVal StrCode As String, ByVal StrTableName As String) As String
        Dim StrReturnData As Object
        Try
            StrReturnData = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, " Select EngName + '/' + ArbName From " & StrTableName & " Where Code = '" & StrCode.ToString.TrimStart.TrimEnd & "'")
            If IsNothing(StrReturnData) Then Return "/"
            If IsDBNull(StrReturnData) Then Return "/"
            Return StrReturnData
        Catch ex As Exception
            Return "/"
        End Try
    End Function

#End Region

#Region "PageMethods"


    <System.Web.Services.WebMethod()>
    Public Shared Function RetTable(ByVal strTableName As String) As Object
        Dim tbl As New Data.DataTable(strTableName)
        tbl.Columns.Add(New Data.DataColumn("EmpID", GetType(Integer)))
        tbl.Columns.Add(New Data.DataColumn("EmpName", GetType(String)))
        For i As Int16 = 1 To 4
            Dim nRow As Data.DataRow = tbl.NewRow()
            nRow(0) = i
            nRow(1) = "Employee " + i.ToString()
            tbl.Rows.Add(nRow)
        Next
        Return tbl
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function Get_Searched_Description(ByVal IntSearchId As Integer, ByVal strCode As String) As String
        Dim dsSearchs As New Data.DataSet
        Find("sys_Searchs", " sys_Searchs.Id = " & IntSearchId, dsSearchs)
        Dim dsObjects As New Data.DataSet
        Find("sys_Objects", " sys_Objects.Id = " & dsSearchs.Tables(0).Rows(0).Item("ObjectID"), dsObjects)
        Return GetFieldDescription(strCode, dsObjects.Tables(0).Rows(0).Item("Code"))
    End Function
#End Region





End Class
