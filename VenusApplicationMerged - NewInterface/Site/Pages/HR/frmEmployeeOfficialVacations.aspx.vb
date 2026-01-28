Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeeOfficialVacations
    Inherits MainPage
#Region "Public Decleration"
    Dim clsHrsEmployeeOfficialVacations As Clshrs_OfficialVacations
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsFiscalYears As Clssys_FiscalYears
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)

        ClsFiscalYears = New Clssys_FiscalYears(Me.Page)
        Dim clsNavigation = New Venus.Shared.Web.NavigationHandler(ClsFiscalYears.ConnectionString)
        Try

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then

                Page.Session.Add("ConnectionString", clsHrsEmployeeOfficialVacations.ConnectionString)
                clsHrsEmployeeOfficialVacations.AddOnChangeEventToControls("frmEmployeeOfficialVacations", Page, UltraWebTab1)

                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-GB")
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-GB")

                ClsFiscalYears.GetDropDown(ddlFiscalYear)
                If (ddlFiscalYear.SelectedItem.Text <> "") Then
                    GetValues()

                Else
                    SetScreenInformation("N")
                End If
            End If
            Dim IntrecordID As Integer
            GetDropDownListGrid()

            CreateOtherFields(IntrecordID)
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, clsHrsEmployeeOfficialVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
        Dim SqlCommand As String = String.Empty
        Select Case e.CommandArgument
            Case "SaveNew"
                If ddlFiscalYear.SelectedItem.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Year /برجاء إدخال العام"))
                    Exit Sub
                End If
                If Not ValidateFromToDateValues() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " FromDate and ToDate are required. /يجب ادخال من تاريخ و الى تاريخ بكل المناسبات"))
                    Exit Sub
                End If
                Dim Deletecommand As String = "Delete from hrs_OfficialVacations where Year='" & ddlFiscalYear.SelectedItem.Text & "' "
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsHrsEmployeeOfficialVacations.ConnectionString, Data.CommandType.Text, Deletecommand)
                'clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                Dim LineNumber = 0
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                    If Not IsNothing(DGRow.Cells("3").Value) Then
                        LineNumber = LineNumber + 1
                        SqlCommand &= " Set DateFormat DMY Insert Into hrs_OfficialVacations " &
                         "(LineNum, VacationTypeID , FromDate, ToDate, Year)Values(" &
                         LineNumber & ", " &
                         "" & DGRow.Cells("3").Value & ", " &
                         "'" & CDate(DGRow.Cells("4").Text) & "', " &
                         "'" & CDate(DGRow.Cells("5").Text) & "', " &
                         "'" & ddlFiscalYear.SelectedItem.Text & "') ; " & vbNewLine
                    End If





                Next
                If SqlCommand <> "" Then
                    Dim cmd As New SqlClient.SqlCommand
                    cmd.CommandText = SqlCommand
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = New SqlClient.SqlConnection(clsHrsEmployeeOfficialVacations.ConnectionString)
                    cmd.Connection.Open()
                    cmd.ExecuteNonQuery()
                    cmd.Connection.Close()
                    AfterOperation()
                End If
            Case "Save"
                If ddlFiscalYear.SelectedItem.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Year /برجاء إدخال العام"))
                    Exit Sub
                End If
                If Not ValidateFromToDateValues() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " FromDate and ToDate are required. /يجب ادخال من تاريخ و الى تاريخ بكل المناسبات"))
                    Exit Sub
                End If
                Dim Deletecommand As String = "Delete from hrs_OfficialVacations where Year='" & ddlFiscalYear.SelectedItem.Text & "' "
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsHrsEmployeeOfficialVacations.ConnectionString, Data.CommandType.Text, Deletecommand)
                'clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                Dim LineNumber = 0
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                    If Not IsNothing(DGRow.Cells("3").Value) Then
                        LineNumber = LineNumber + 1
                        SqlCommand &= " Set DateFormat DMY Insert Into hrs_OfficialVacations " &
                         "(LineNum, VacationTypeID , FromDate, ToDate, Year)Values(" &
                         LineNumber & ", " &
                         "" & DGRow.Cells("3").Value & ", " &
                         "'" & CDate(DGRow.Cells("4").Text) & "', " &
                         "'" & CDate(DGRow.Cells("5").Text) & "', " &
                         "'" & ddlFiscalYear.SelectedItem.Text & "') ; " & vbNewLine
                    End If





                Next
                If SqlCommand <> "" Then
                    Dim cmd As New SqlClient.SqlCommand
                    cmd.CommandText = SqlCommand
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = New SqlClient.SqlConnection(clsHrsEmployeeOfficialVacations.ConnectionString)
                    cmd.Connection.Open()
                    cmd.ExecuteNonQuery()
                    cmd.Connection.Close()
                End If
            Case "New"
                AfterOperation()
            Case "Delete"
                clsHrsEmployeeOfficialVacations.Delete("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                AfterOperation()
            Case "Property"
                clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & clsHrsEmployeeOfficialVacations.ID & "&TableName=" & clsHrsEmployeeOfficialVacations.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & clsHrsEmployeeOfficialVacations.ID & "&TableName=" & clsHrsEmployeeOfficialVacations.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = clsHrsEmployeeOfficialVacations.Table
                clsHrsEmployeeOfficialVacations.Find(" year = '" & ddlFiscalYear.SelectedItem.Text & "'")
                Dim recordID As Integer = clsHrsEmployeeOfficialVacations.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & clsHrsEmployeeOfficialVacations.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                clsHrsEmployeeOfficialVacations.Find(" Year= '" & ddlFiscalYear.SelectedItem.Text & "'")
                If clsHrsEmployeeOfficialVacations.ID > 0 Then
                    Dim Ds As Data.DataSet = clsHrsEmployeeOfficialVacations.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If clsHrsEmployeeOfficialVacations.CheckDiff(clsHrsEmployeeOfficialVacations, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                clsHrsEmployeeOfficialVacations.FirstRecord()
                GetValues()
            Case "Previous"
                clsHrsEmployeeOfficialVacations.Find("year='" & ddlFiscalYear.SelectedItem.Text & "'")
                If Not clsHrsEmployeeOfficialVacations.previousRecord() Then
                    clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                If Not clsHrsEmployeeOfficialVacations.NextRecord() Then
                    clsHrsEmployeeOfficialVacations.Find("Year='" & ddlFiscalYear.SelectedItem.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                clsHrsEmployeeOfficialVacations.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFiscalYear.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function AssignValues() As Boolean
        Try
            With clsHrsEmployeeOfficialVacations
                .Year = ddlFiscalYear.SelectedItem.Text
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function

    Private Function ValidateFromToDateValues() As Boolean
        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If Not IsNothing(DGRow.Cells("3").Value) Then
                    If IsNothing(DGRow.Cells("4").Value) Or CStr(DGRow.Cells("4").Value) = "  /  /    " Then
                        Return False
                    End If
                    If IsNothing(DGRow.Cells("5").Value) Or CStr(DGRow.Cells("5").Value) = "  /  /    " Then
                        Return False
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Dim str = ex.ToString()
        End Try
    End Function
    Private Function GetValues() As Boolean
        GetDropDownListGrid()

        Dim ClsUser As New Clshrs_OfficialVacations(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            'ddlFiscalYear.SelectedItem.Text = clsHrsEmployeeOfficialVacations.Year
            'txtEngName.Text = clsHrsEmployeeOfficialVacations.EngName
            'txtArbName.Text = clsHrsEmployeeOfficialVacations.ArbName
            Dim mFindDataset As New DataSet
            mFindDataset = ClsUser.Find("Year=" & ddlFiscalYear.SelectedItem.Text)

            UwgSearchEmployees.DataSource = mFindDataset.Tables(0)
            UwgSearchEmployees.DataBind()
            UwgSearchEmployees.Rows.Add()
            Dim item As New System.Web.UI.WebControls.ListItem()
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If IsNothing(DGRow.Cells(1).Value) Then
                    Continue For
                End If
                If DGRow.Cells(3).Value <> "" Then
                    DGRow.Cells(3).Value = Convert.ToInt32(DGRow.Cells(3).Value)
                End If



            Next
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
            If (UwgSearchEmployees.Rows.Count > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
                'UwgSearchEmployees.Rows.Add()
                'UwgSearchEmployees.Rows(0).Cells("LineNum").Value = 1

            End If
            SetToolBarPermission(Me, clsHrsEmployeeOfficialVacations.ConnectionString, clsHrsEmployeeOfficialVacations.DataBaseUserRelatedID, clsHrsEmployeeOfficialVacations.GroupID, StrMode)
            SetToolBarRecordPermission(Me, clsHrsEmployeeOfficialVacations.ConnectionString, clsHrsEmployeeOfficialVacations.DataBaseUserRelatedID, clsHrsEmployeeOfficialVacations.GroupID, clsHrsEmployeeOfficialVacations.Table, clsHrsEmployeeOfficialVacations.ID)
            If Not clsHrsEmployeeOfficialVacations.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(clsHrsEmployeeOfficialVacations.ID)
            End If
            Return True
        Catch ex As Exception
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
                            ImageButton_SaveN.Enabled = .Item("AllowAdd")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")
                            ImageButton_SaveN.Enabled = .Item("AllowEdit")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                    End Select
                End With
            End If
            Return True
        Catch ex As Exception
            Return False
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
                        ImageButton_SaveN.Enabled = Not .Item("CanEdit")
                        LinkButton_SaveN.Enabled = Not .Item("CanEdit")
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
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType
                Case "N", "R"
                    ddlFiscalYear.SelectedItem.Text = String.Empty
                    ImageButton_First.Visible = False
                    ImageButton_Back.Visible = False
                    ImageButton_Next.Visible = False
                    ImageButton_Last.Visible = False
                    ImageButton_Delete.Enabled = False
                    ImageButton_Properties.Visible = False
                    LinkButton_Properties.Visible = False
                    ImageButton_Remarks.Visible = False
                    LinkButton_Remarks.Visible = False

                Case "D"
                    clsHrsEmployeeOfficialVacations.Find("ID=" & intID)
                    GetValues()
                    ddlFiscalYear.Enabled = False
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    clsHrsEmployeeOfficialVacations.Find("ID=" & intID)
                    GetValues()
                    ddlFiscalYear.Enabled = False
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With clsHrsEmployeeOfficialVacations
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
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        Try
            With clsHrsEmployeeOfficialVacations
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
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        If IntId > 0 Then
            clsHrsEmployeeOfficialVacations.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
        Try

            GetValues()

        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        clsHrsEmployeeOfficialVacations.Clear()
        GetValues()
        ImageButton_Delete.Enabled = False

        Venus.Shared.Web.ClientSideActions.SetFocus(Page, ddlFiscalYear, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean


        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Page)
        clsHrsEmployeeOfficialVacations.Find(" Year = '" & ddlFiscalYear.SelectedItem.Text & "'")
        Dim recordID As Integer = clsHrsEmployeeOfficialVacations.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, clsHrsEmployeeOfficialVacations.Table) = True Then
            Dim StrTablename As String
            clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
            StrTablename = clsHrsEmployeeOfficialVacations.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmDocumentsTypes")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmDocumentsTypes")
            End If
        End If
    End Function

    Protected Sub UwgSearchEmployees_DeleteRow(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UwgSearchEmployees.DeleteRow
        If e.Row.Cells.FromKey("ID").Value <> Nothing Then
            clsHrsEmployeeOfficialVacations = New Clshrs_OfficialVacations(Me)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsHrsEmployeeOfficialVacations.ConnectionString)
            Dim SqlCommand As String = String.Empty

            SqlCommand = "DELETE FROM [dbo].[hrs_OfficialVacations] WHERE ID=" & e.Row.Cells.FromKey("ID").Value
            Dim cmd As New SqlClient.SqlCommand
            cmd.CommandText = SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.Connection = New SqlClient.SqlConnection(clsHrsEmployeeOfficialVacations.ConnectionString)
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
        End If
    End Sub

    Public Function GetDropDownListGrid() As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

        Dim strselect2 As String
        strselect2 = "select ID, Code,EngName,ArbName from hrs_VacationsTypes where IsOfficial=1"
        Dim DSOfficialvacations As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)
        If DSOfficialvacations.Tables(0).Rows.Count > 0 Then
            UwgSearchEmployees.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Clear()
            For Each Row As Data.DataRow In DSOfficialvacations.Tables(0).Rows
                UwgSearchEmployees.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & ""))
            Next

        End If



    End Function

#End Region
End Class
