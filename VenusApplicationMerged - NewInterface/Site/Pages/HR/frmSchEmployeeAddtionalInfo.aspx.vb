Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesOthersVacations
    Inherits MainPage
#Region "Public Decleration"
    Private ClsSchEmpAddtionalInfo As Clshrs_SchEmpAddtionalInfo
    Private ClsEmployees As Clshrs_Employees
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Private clsEmpTransaction As Clshrs_EmployeesTransactions
    Const csOtherFields = 11
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim recordID As Integer

        ClsSchEmpAddtionalInfo = New Clshrs_SchEmpAddtionalInfo(Page)
        Try
            ClsSchEmpAddtionalInfo.AddNotificationOnChange(Page)
            Dim csSearchID As Integer
            Dim ClsLevels As New Clshrs_LevelTypes(Page)
            Dim ClsSchEducationalStages As New Clshrs_SchEducationalStages(Page)
            Dim ClsSchSchSpecialization As New Clshrs_SchSpecialization(Page)
            Dim ClsSchSubjects As New Clshrs_SchSubjects(Page)
            Dim ClsDataHandler As New Venus.Shared.DataHandler
            Dim StrSerial As String = String.Empty
            ClsEmployees = New Clshrs_Employees(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
            Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
            Dim ClsObjects As New Clssys_Objects(Page)
            Dim ClsSearchs As New Clssys_Searchs(Page)
            Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)

            txtEmployee.Attributes.Add("onchange", "ChangeIsDataChanged()")
            ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")

            ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim() & "'")
            ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
            csSearchID = ClsSearchs.ID

            Dim IntDimension As Integer = 510
            Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployee.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtEmployee.ClientID & "'"
            btnEmployee.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"


            Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
            If Not IsPostBack Then



                txtEmployee.Focus()

                ClsSchEducationalStages.GetDropDownList(DdlEducationalStage, True)
                ClsSchSchSpecialization.GetDropDownList(DdlSpecializtion, True)
                ClsSchSubjects.GetDropDownList(ddlSubjects, True)
                'ClsSchEducationalStages.Find("IsAnnual=0")
                'If ClsSchEducationalStages.ID > 0 Then
                '    hdnAnnualVacId.Value = ClsSchEducationalStages.ID
                'Else
                '    hdnAnnualVacId.Value = 0
                ' End If
            End If
            'If (lblRecordID.Text <> "") Then
            '    ClsSchEmpAddtionalInfo.Find("ID=" & lblRecordID.Text)
            '    recordID = ClsSchEmpAddtionalInfo.ID
            '    If (recordID > 0) Then
            '        SetScreenInformation("E")
            '        SetToolBarRecordPermission(Me, ClsSchEmpAddtionalInfo.ConnectionString, ClsSchEmpAddtionalInfo.DataBaseUserRelatedID, ClsSchEmpAddtionalInfo.GroupID, ClsSchEmpAddtionalInfo.Table, recordID)
            '    Else
            '        SetScreenInformation("N")
            '        If Not IsPostBack Then
            '            SetTime()
            '        End If
            '    End If
            'Else
            '    SetScreenInformation("N")
            '    If Not IsPostBack Then
            '        SetTime()
            '    End If
            'End If
            If ClsObjects.Find(" Code='" & ClsSchEmpAddtionalInfo.Table.ToString.Trim() & "'") Then
                ImageButton_Documents.Attributes.Add("onclick", "OpenModal3('frmAttachDocuments.aspx?OId=" & ClsObjects.ID & "&',400,600,true,''); return false;")
            End If


        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSchEmpAddtionalInfo.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSchEmpAddtionalInfo.ReguserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Delete.Command
        Dim IntId As Integer
        Dim strMode As String
        ClsSchEmpAddtionalInfo = New Clshrs_SchEmpAddtionalInfo(Page)
        ClsEmployees = New Clshrs_Employees(Page)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsSchEmpAddtionalInfo.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtEmployee.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                SavePart()
            Case "Save"
                If txtEmployee.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"
                SetNew()
            Case "Delete"
                If CheckEmployee() Then
                    ClsSchEmpAddtionalInfo.Find("EmployeeidID=" & ClsEmployees.ID)
                    If ClsSchEmpAddtionalInfo.ID > 0 Then
                        ClsSchEmpAddtionalInfo.Delete("EmployeeidID=" & ClsEmployees.ID)
                    End If


                End If
                CheckEmpCode()
                SetNew()
            Case "Property"
                If CheckEmployee() And CheckVacation() Then
                    If ClsSchEmpAddtionalInfo.Find("ID=" & lblRecordID.Text) Then
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsSchEmpAddtionalInfo.ID & "&TableName=" & ClsSchEmpAddtionalInfo.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                    End If
                End If
            Case "Remarks"
                If CheckEmployee() And CheckVacation() Then
                    If ClsSchEmpAddtionalInfo.Find("ID=" & lblRecordID.Text) Then
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsSchEmpAddtionalInfo.ID & "&TableName=" & ClsSchEmpAddtionalInfo.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                    End If
                End If
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
            Case "Exit"
            Case "First"
                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                ClsEmployees.FirstRecord()
                txtEmployee.Text = ClsEmployees.Code
                txtEmployee_TextChanged(Nothing, Nothing)
            Case "Previous"
                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                If Not ClsEmployees.previousRecord() Then
                    ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                txtEmployee.Text = ClsEmployees.Code
                txtEmployee_TextChanged(Nothing, Nothing)
            Case "Next"
                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                If Not ClsEmployees.NextRecord() Then
                    ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                txtEmployee.Text = ClsEmployees.Code
                txtEmployee_TextChanged(Nothing, Nothing)
            Case "Last"
                ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
                ClsEmployees.LastRecord()
                txtEmployee.Text = ClsEmployees.Code
                txtEmployee_TextChanged(Nothing, Nothing)
        End Select
        If uwgEmployeeSubjects.Rows.Count > 0 Then
            IntId = uwgEmployeeSubjects.Rows(0).Cells.FromKey("ID").Value
            strMode = "E"
        Else
            strMode = "N"
            IntId = 0
        End If
        SetToolBarPermission(Me, ClsSchEmpAddtionalInfo.ConnectionString, ClsSchEmpAddtionalInfo.DataBaseUserRelatedID, ClsSchEmpAddtionalInfo.GroupID, strMode)
        SetToolBarRecordPermission(Me, ClsSchEmpAddtionalInfo.ConnectionString, ClsSchEmpAddtionalInfo.DataBaseUserRelatedID, ClsSchEmpAddtionalInfo.GroupID, ClsSchEmpAddtionalInfo.Table, IntId)
        If strMode = "N" Then
            ImageButton_Delete.Enabled = False
        End If
    End Sub
    Protected Sub txtEmployee_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployee.TextChanged
        Try
            CheckEmpCode()



        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSchEmpAddtionalInfo.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSchEmpAddtionalInfo.ReguserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub



#End Region

#Region "Private Functions"

    Private Function SetNew() As Boolean
        Dim clsSchEmpAddtionalInfo As New Clshrs_SchEmpAddtionalInfo(Page)
        Try
            txtEmployee.Text = ""
            DdlEducationalStage.SelectedIndex = 0
            DdlSpecializtion.SelectedIndex = 0
            ddlSubjects.SelectedIndex = 0

            SetTime()
            If CheckEmployee() Then
                FillEmployeeSubject(ClsEmployees.ID)
            End If

            lblRegUserValue.Text = ""
            lblRegDateValue.Text = ""
            SetToolBarPermission(Me, clsSchEmpAddtionalInfo.ConnectionString, clsSchEmpAddtionalInfo.DataBaseUserRelatedID, clsSchEmpAddtionalInfo.GroupID, "N")
            ImageButton_Delete.Enabled = False
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(clsSchEmpAddtionalInfo.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, clsSchEmpAddtionalInfo.ReguserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
                    If TypeOf (currCtrl) Is TextBox Then
                    End If
                Next
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSchEmpAddtionalInfo.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSchEmpAddtionalInfo.ReguserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Private Function CheckEmployee() As Boolean
        Dim BolExist As Boolean
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        If txtEmployee.Text <> "" Then
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            If (ClsEmployees.ID > 0) Then
                BolExist = True
            Else
                BolExist = False
            End If
        Else
            BolExist = False
        End If
        If (Not BolExist) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Employee not found/هذا الموظف غير موجود"))
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CheckVacation() As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        If lblRecordID.Text.Trim = "" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "You must select vacation/يجب أن تختار أجازة"))
            Return False
        Else
            Return True
        End If
    End Function


    Private Function SetTime() As Boolean
        Dim clsCompanies As New Clssys_Companies(Page)
        Dim dteNow As Date = Format(Date.Now, "dd/MM/yyyy")
        Try
            With clsCompanies
            End With
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSchEmpAddtionalInfo.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSchEmpAddtionalInfo.ReguserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function


    Function GetNDate(ByVal date1 As Object, ByVal dteDefault As Date) As Date
        If IsDBNull(date1) Then
            Return dteDefault
        Else
            Return CDate(date1)
        End If
    End Function
    Public Shared Function GetNDate_Shared(ByVal date1 As Object, ByVal dteDefault As Date) As Date
        If IsDBNull(date1) Then
            Return dteDefault
        Else
            Return CDate(date1)
        End If
    End Function
    Private Function CheckEmpCode() As Boolean
        ClsEmployees = New Clshrs_Employees(Page)
        ClsSchEmpAddtionalInfo = New Clshrs_SchEmpAddtionalInfo(Page)
        Dim ClsNationality = New Clssys_Nationality(Page)
        Dim BolExist As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try

            lblRecordID.Text = ""
            DdlEducationalStage.SelectedIndex = 0
            DdlSpecializtion.SelectedIndex = 0
            ddlSubjects.SelectedIndex = 0
            txtClassesCount.Text = ""
            txtHomePhone.Text = ""
            txtAddtionalTasks.Text = ""

            SetScreenInformation("N")
            If (txtEmployee.Text.Trim <> "") Then
                ClsEmployees.Find("Code ='" & txtEmployee.Text & "'")
                If ClsEmployees.ID > 0 Then
                    lblRecordID.Text = ClsEmployees.ID
                    txtEmployee.Text = ClsEmployees.Code
                    lblDescEnglishName.Text = ClsEmployees.EnglishName
                    ClsSchEmpAddtionalInfo.Find("Employeeid = " & ClsEmployees.ID)
                    DdlEducationalStage.SelectedValue = ClsSchEmpAddtionalInfo.EducationalStageID
                    DdlSpecializtion.SelectedValue = ClsSchEmpAddtionalInfo.SpecializtionID
                    ddlSubjects.SelectedValue = ClsSchEmpAddtionalInfo.EmpSubjectsID
                    txtClassesCount.Text = ClsSchEmpAddtionalInfo.ClassesCount
                    txtHomePhone.Text = ClsSchEmpAddtionalInfo.HomePhone

                    txtAddtionalTasks.Text = ClsSchEmpAddtionalInfo.AddtionalTasks


                    ' FillEmployeeVacations(ClsEmployees.ID, False)
                    'GetEmpContractVac()
                    BolExist = True
                Else
                    BolExist = False
                End If
            Else
                BolExist = False
            End If
            If (Not BolExist) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Employee not found/هذا الموظف غير موجود"))
                lblDescEnglishName.Text = ""

                txtEmployee.Text = ""
                lblRecordID.Text = ""
                uwgEmployeeSubjects.Clear()
                txtEmployee.Focus()
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSchEmpAddtionalInfo.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSchEmpAddtionalInfo.ReguserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Sub FillEmployeeSubject(ByVal EmployeeID As Integer, Optional ByVal showFirstRecord As Boolean = False)
        Try
            uwgEmployeeSubjects.Clear()
            ClsSchEmpAddtionalInfo = New Clshrs_SchEmpAddtionalInfo(Page)
            ClsSchEmpAddtionalInfo.Find(" EmployeeID=" & EmployeeID & " and VacationTypeID not in (select id from hrs_VacationsTypes where IsAnnual = 1)")
            uwgEmployeeSubjects.DataSource = ClsSchEmpAddtionalInfo.DataSet.Tables(0).DefaultView
            uwgEmployeeSubjects.DataBind()

            '======================================== Show First Record [ Start ]

            '======================================== Show First Record [ End ]
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSchEmpAddtionalInfo.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSchEmpAddtionalInfo.ReguserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Private Function SavePart() As Boolean
        ClsSchEmpAddtionalInfo = New Clshrs_SchEmpAddtionalInfo(Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Page)
        'ClsEmployees = New Clshrs_Employees(Page)
        Dim recordId As Integer
        Try
            If lblRecordID.Text.Trim <> "" Then
                If CheckEmployee() Then
                    ClsSchEmpAddtionalInfo.Find("Employeeid=" & ClsEmployees.ID)
                    If Not AssignValue(ClsSchEmpAddtionalInfo) Then
                        Exit Function
                    End If
                    If ClsSchEmpAddtionalInfo.ID > 0 Then
                        ClsSchEmpAddtionalInfo.Update("ID=" & ClsSchEmpAddtionalInfo.ID)
                    Else
                        ClsSchEmpAddtionalInfo.Save()
                    End If


                Else
                    Exit Function
                End If
            Else
                If CheckEmployee() Then
                    If Not AssignValue(ClsSchEmpAddtionalInfo) Then
                        Exit Function
                    End If
                    ClsSchEmpAddtionalInfo.Save()
                Else
                    Exit Function
                End If
            End If
            clsMainOtherFields.CollectDataAndSave(value.Text, ClsSchEmpAddtionalInfo.Table, recordId)
            value.Text = ""
            CheckEmpCode()


            Return True
        Catch ex As Exception
            Return False
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSchEmpAddtionalInfo.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSchEmpAddtionalInfo.ReguserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function AssignValue(ByRef ClsSchEmpAddtionalInfo As Clshrs_SchEmpAddtionalInfo) As Boolean
        'Dim ClsEmployees As New Clshrs_Employees(Page)


        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsSchEmpAddtionalInfo.ConnectionString)
        Dim strErrorMsg As String = String.Empty
        Dim bExceed As Boolean = False
        Dim bErorr As Boolean = False




        Try
            ClsSchEmpAddtionalInfo.EmployeeID = ClsEmployees.ID
            ClsSchEmpAddtionalInfo.EducationalStageID = DdlEducationalStage.SelectedValue
            ClsSchEmpAddtionalInfo.SpecializtionID = DdlSpecializtion.SelectedValue
            ClsSchEmpAddtionalInfo.ClassesCount = txtClassesCount.Text
            ClsSchEmpAddtionalInfo.HomePhone = txtHomePhone.Text
            ClsSchEmpAddtionalInfo.EmpSubjectsID = ddlSubjects.SelectedValue
            ClsSchEmpAddtionalInfo.AddtionalTasks = txtAddtionalTasks.Text
            Return True

        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSchEmpAddtionalInfo.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSchEmpAddtionalInfo.ReguserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function GetValues(ByRef ClsSchEmpAddtionalInfo As Clshrs_SchEmpAddtionalInfo) As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)

        Dim ClsUser As New Clssys_Users(Page)
        Try

            SetToolBarDefaults()
            With ClsSchEmpAddtionalInfo

                Dim StrMode As String = String.Empty
                If (.ID > 0) Then
                    StrMode = "E"
                Else
                    StrMode = "N"
                End If
                SetToolBarPermission(Me, ClsSchEmpAddtionalInfo.ConnectionString, ClsSchEmpAddtionalInfo.DataBaseUserRelatedID, ClsSchEmpAddtionalInfo.GroupID, StrMode)
                SetToolBarRecordPermission(Me, ClsSchEmpAddtionalInfo.ConnectionString, ClsSchEmpAddtionalInfo.DataBaseUserRelatedID, ClsSchEmpAddtionalInfo.GroupID, ClsSchEmpAddtionalInfo.Table, .ID)
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If
                If Page.IsPostBack Then
                    CreateOtherFields(.ID)
                End If
            End With
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
                    ClsSchEmpAddtionalInfo.Find("ID=" & intID)
                    GetValues(ClsSchEmpAddtionalInfo)

                    ImageButton_Save.Visible = False

                Case "E"
                    ClsSchEmpAddtionalInfo.Find("ID=" & intID)
                    GetValues(ClsSchEmpAddtionalInfo)

                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsSchEmpAddtionalInfo
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
        ClsSchEmpAddtionalInfo = New Clshrs_SchEmpAddtionalInfo(Me.Page)
        Try
            With ClsSchEmpAddtionalInfo
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
        ClsSchEmpAddtionalInfo = New Clshrs_SchEmpAddtionalInfo(Me.Page)
        If IntId > 0 Then
            ClsSchEmpAddtionalInfo.Find("ID=" & IntId)
            GetValues(ClsSchEmpAddtionalInfo)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsSchEmpAddtionalInfo = New Clshrs_SchEmpAddtionalInfo(Me.Page)
        ClsSchEmpAddtionalInfo.Find(" code = '" & txtEmployee.Text & "'")
        Dim recordID As Integer = ClsSchEmpAddtionalInfo.ID
        If (recordID > 0) Then
            Dim clsForms As New ClsSys_Forms(Page)
            clsForms.Find(" code = REPLACE('" & formName & "',' ','')")
            Dim clsFormsControls As New Clssys_FormsControls(Page)
            clsFormsControls.Find(" FormID=" & clsForms.ID)
            Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()
            For Each row As Data.DataRow In tab.Rows
                clsFormsControls.Find(" FormID=" & clsForms.ID & " And Name='" & row("Name") & "'")
                Dim sys_Fields As New Clssys_Fields(Page)
                sys_Fields.Find(" ID=" & clsFormsControls.FieldID)
                If (sys_Fields.FieldName.Trim() = "Code" Or sys_Fields.FieldName.Trim() = "Number" Or sys_Fields.FieldName.Trim() = "ID") Then
                    Continue For
                End If
                Dim currCtrl As Control = Me.FindControl(row("Name"))
                Dim bIsArabic As Boolean = IIf(IsDBNull(row("IsArabic")), False, row("IsArabic"))
                If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then
                    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                End If
            Next
        End If
    End Sub
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsSchEmpAddtionalInfo.Table) = True Then
            Dim StrTablename As String
            ClsSchEmpAddtionalInfo = New Clshrs_SchEmpAddtionalInfo(Page)
            StrTablename = ClsSchEmpAddtionalInfo.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID & _
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID & _
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
    Private Function CheckDateBetween2Dates(ByVal d As Date, ByVal d1 As Date, ByVal d2 As Date) As Boolean
        If (d1 = Nothing Or d2 = Nothing) Then
            Return False
        End If
        If (Date.Compare(d, d1) >= 0 And Date.Compare(d, d2) <= 0) Then
            Return True
        Else
            Return False
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
    Public Shared Function Contracts_ContractValidatoinId(ByVal EmployeeID As Integer, ByVal CurrentDate As Date) As Integer
        Dim dsContracts As New DataSet
        Dim DateNow As String
        If Not ClsDataAcessLayer.IsGreg(DateTime.Now.ToString("dd/MM/yyyy")) Then
            DateNow = ClsDataAcessLayer.FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        Else
            DateNow = DateTime.Now.ToString("dd/MM/yyyy")
        End If

        If Find("hrs_Contracts", "Employeeid=" & EmployeeID & "  And StartDate <= '" & DateNow & "' And (enddate is null or '" & DateNow & "' Between StartDate and EndDate)", dsContracts) Then
            Return dsContracts.Tables(0).Rows(0).Item("ID")
        Else
            Return 0
        End If
    End Function
    Public Shared Function Contracts_ContractValidatoinId(ByVal EmployeeID As Integer) As Integer
        Try
            Dim dsContracts As New DataSet
            Dim DateNow As String
            If Not ClsDataAcessLayer.IsGreg(DateTime.Now.ToString("dd/MM/yyyy")) Then
                DateNow = ClsDataAcessLayer.FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            Else
                DateNow = DateTime.Now.ToString("dd/MM/yyyy")
            End If
            If Find("hrs_Contracts", "Employeeid=" & EmployeeID & "  And StartDate <= '" & DateNow & "' And (enddate is null or '" & DateNow & "' Between StartDate and EndDate)", dsContracts) Then
                Return dsContracts.Tables(0).Rows(0).Item("ID")
            Else
                Return 0
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function GetEmployeeVacSum(ByVal intEmployeeID As Integer, ByVal intVacTypeID As Integer, ByVal dateStartDate As Date, ByVal dateEndDate As Date, ByVal intContractId As Integer) As Double
        Dim wHoursPerDay As Double = 0
        Dim dteActualStartDate As Date
        Dim dteactualEndDate As Date
        Dim dteTempActualStartDate As Date
        Dim dteTempActualEndDate As Date
        Dim dblDiffHours As Double
        Dim dblDiffDays As Double
        Dim dteStartTime As Date
        Dim dteEndTime As Date
        Dim dteThisStartDateStartTime As Date
        Dim dteThisStartDateEndTime As Date
        Dim dteThisEndDateStartTime As Date
        Dim dteThisEndDateEndTime As Date
        Dim dsEmpVac As New DataSet
        Dim dsContracts As New DataSet
        Dim dsEmployeesClasses As New DataSet
        Try
            Find("hrs_Contracts", " ID =" & intContractId, dsContracts)

            If Find("hrs_EmployeesClasses", "ID=" & IIf(dsContracts.Tables(0).Rows(0).Item("EmployeeClassID") > 0, dsContracts.Tables(0).Rows(0).Item("EmployeeClassID"), 0), dsEmployeesClasses) Then
                With dsEmployeesClasses.Tables(0).Rows(0)
                    If IsNothing(.Item("WorkHoursPerDay")) Then
                        wHoursPerDay = 9
                        dteStartTime = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 9, 0, 0)
                        dateStartDate = New Date(dateStartDate.Year, dateStartDate.Month, dateStartDate.Day, 9, 0, 0)
                        dteEndTime = dteStartTime.AddHours(wHoursPerDay)
                        dateEndDate = New Date(dateEndDate.Year, dateEndDate.Month, dateEndDate.Day, 9, 0, 0).AddHours(wHoursPerDay)
                    Else
                        wHoursPerDay = .Item("WorkHoursPerDay")
                        dteStartTime = .Item("DefultStartTime")
                        dateStartDate = New Date(dateStartDate.Year, dateStartDate.Month, dateStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, 0)
                        dteEndTime = CDate(.Item("DefultStartTime")).AddHours(wHoursPerDay)
                        dateEndDate = New Date(dateEndDate.Year, dateEndDate.Month, dateEndDate.Day, dteStartTime.Hour, dteStartTime.Minute, 0).AddHours(wHoursPerDay)
                    End If
                End With
            End If
            Dim NoOfWorkingHours As Double = wHoursPerDay
            Dim NoOfNonWorkingHours As Double = 24 - NoOfWorkingHours
            Find("hrs_EmployeesVacations", "EmployeeID=" & intEmployeeID & " And VacationTypeID=" & intVacTypeID & " And (ActualStartDate Between '" & dateStartDate & "' And '" & dateEndDate & "' OR IsNull(ActualEndDate,'" & IIf(Date.Now > dateEndDate, dateEndDate, Date.Now) & "') Between '" & dateStartDate & "' And '" & dateEndDate & "' OR  '" & dateStartDate & "' Between ActualStartDate AND IsNull(ActualEndDate,'" & IIf(Date.Now > dateEndDate, dateEndDate, Date.Now) & "') ) ", dsEmpVac)
            Dim vacDays As Double = 0
            For Each row As DataRow In dsEmpVac.Tables(0).Rows
                If IsDBNull(row("ActualStartDate")) Then
                    row("ActualStartDate") = CDate(Nothing)
                End If
                dteActualStartDate = IIf(CDate(row("ActualStartDate")) < dateStartDate, dateStartDate, row("ActualStartDate"))
                dteactualEndDate = GetNDate_Shared(row("ActualEndDate"), IIf(Date.Now > dateEndDate, dateEndDate, Date.Now))
                dteactualEndDate = IIf(dteactualEndDate > dateEndDate, dateEndDate, dteactualEndDate)
                If dteactualEndDate < dteActualStartDate Then
                    Continue For
                End If
                dteThisStartDateStartTime = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                dteThisStartDateEndTime = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                dteThisEndDateStartTime = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                dteThisEndDateEndTime = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                dteTempActualStartDate = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                dteTempActualEndDate = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                If dteActualStartDate.Day = dteactualEndDate.Day And _
                   dteActualStartDate.Month = dteactualEndDate.Month And _
                   dteActualStartDate.Year = dteactualEndDate.Year And _
                   ((dteActualStartDate > dteTempActualEndDate And dteactualEndDate > dteTempActualEndDate) Or _
                     (dteActualStartDate < dteTempActualStartDate And dteactualEndDate < dteTempActualStartDate)) Then
                    Continue For
                End If
                If DateDiff(DateInterval.Day, dteActualStartDate, dteactualEndDate) = 0 And _
                    (dteActualStartDate >= dteThisStartDateEndTime And dteactualEndDate <= dteThisEndDateStartTime) Then
                    Continue For
                End If
                If dteActualStartDate < dteTempActualStartDate Then
                    dteActualStartDate = dteTempActualStartDate
                ElseIf dteActualStartDate > dteTempActualStartDate And dteActualStartDate > dteThisStartDateEndTime Then
                    Dim dteActualStartDatePlusDay As Date = dteActualStartDate.AddDays(1)
                    dteActualStartDate = New Date(dteActualStartDatePlusDay.Year, dteActualStartDatePlusDay.Month, dteActualStartDatePlusDay.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                End If
                If dteactualEndDate > dteTempActualEndDate Then
                    dteactualEndDate = dteTempActualEndDate
                ElseIf dteactualEndDate < dteTempActualEndDate And dteactualEndDate < dteThisEndDateStartTime Then
                    Dim dteActualEndDateMiunsDay As Date = dteactualEndDate.AddDays(-1)
                    dteactualEndDate = New Date(dteActualEndDateMiunsDay.Year, dteActualEndDateMiunsDay.Month, dteActualEndDateMiunsDay.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                End If
                dblDiffHours = DateDiff(DateInterval.Minute, dteActualStartDate, dteactualEndDate) / 60
                dblDiffDays = DateDiff(DateInterval.Day, dteActualStartDate, dteactualEndDate)
                vacDays += (dblDiffHours - (dblDiffDays * NoOfNonWorkingHours)) / NoOfWorkingHours
            Next
            Return vacDays
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function Contract_GetDurationDaysForPeriod(ByVal intContractID As Integer, ByVal intVacType As Integer, ByVal dteStartDate As DateTime) As DataSet
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(CType(HttpContext.Current.Session("ConnectionString"), String), "GetDurationDaysForPeriod", intContractID, intVacType, dteStartDate)
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
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetEmployeeID(ByVal strEmpCode As String) As String
        Dim dsEmp As New DataSet
        If Find("hrs_Employees", "Code='" & strEmpCode & "'", dsEmp) Then
            Return dsEmp.Tables(0).Rows(0).Item("ID").ToString
        Else
            Return "0"
        End If
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function CheckEmployeeContract(ByVal IntEmployeeId As Integer, ByVal dteDateToCheck As Date) As String
        If Contracts_ContractValidatoinId(IntEmployeeId, dteDateToCheck) > 0 Then
            Return "1"
        Else
            Return "0"
        End If
    End Function
    <System.Web.Services.WebMethod()> _
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
    <System.Web.Services.WebMethod()> _
    Public Shared Function Get_Searched_Description(ByVal IntSearchId As Integer, ByVal strCode As String) As String
        Dim dsSearchs As New Data.DataSet
        Find("sys_Searchs", " sys_Searchs.Id = " & IntSearchId, dsSearchs)
        Dim dsObjects As New Data.DataSet
        Find("sys_Objects", " sys_Objects.Id = " & dsSearchs.Tables(0).Rows(0).Item("ObjectID"), dsObjects)
        Return GetFieldDescription(strCode, dsObjects.Tables(0).Rows(0).Item("Code"))
    End Function
#End Region


End Class
