
Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class frmGradesSteps
    Inherits MainPage

#Region "Public Decleration"
    Private ClsGradesSteps As Clshrs_GradesSteps
    Private ClsGrades As Clshrs_Grades
    Private ClsGradesTransactions As Clshrs_GradesTransactions
    Private ClsGradesStepsTransactions As Clshrs_GradesStepsTransactions
    Private clsMainOtherFields As clsSys_MainOtherFields

    Const cNoOfEmptyRows = 30
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim GId As Integer = Session.Item("ID")

        ClsGrades = New Clshrs_Grades(Me.Page)
        ClsGradesSteps = New Clshrs_GradesSteps(Me.Page)
        ClsGradesTransactions = New Clshrs_GradesTransactions(Page)
        Dim clsNavigation = New Venus.Shared.Web.NavigationHandler(ClsGradesSteps.ConnectionString)

        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsGradesSteps.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsGrades.ConnectionString)
                ClsGrades.AddOnChangeEventToControls("frmGradesSteps", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                uwgGradesSteps.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect
                uwgGradesSteps.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgGradesSteps.DisplayLayout.AllowAddNewDefault = Infragistics.WebUI.UltraWebGrid.AllowAddNew.No

                Dim clsMainCountry As New Clssys_Countries(Page)
                Dim clsMainCurrency As New ClsSys_Currencies(Page)
                clsMainCountry.Find(" IsMainCountries = 1 ")
                If clsMainCountry.ID > 0 Then
                    clsMainCurrency.Find(" ID=" & clsMainCountry.CurrencyID)
                    If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                        'Columns
                        uwgGradesStepsTransactions.Columns(2).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgGradesStepsTransactions.Columns(2).Format, clsMainCurrency.NoDecimalPlaces)
                        '
                    End If
                End If

                '' Add by Abdulrahman
                Dim lag As String = clsNavigation.SetLanguage(Page, "Eng/Arb")
                If lag = "Eng" Then
                    uwgGradesSteps.DisplayLayout.Bands(0).Columns.FromKey("EngName").Hidden = False
                    uwgGradesSteps.DisplayLayout.Bands(0).Columns.FromKey("ArbName").Hidden = True
                Else
                    uwgGradesSteps.DisplayLayout.Bands(0).Columns.FromKey("EngName").Hidden = True
                    uwgGradesSteps.DisplayLayout.Bands(0).Columns.FromKey("ArbName").Hidden = False
                End If
                '' 


                ClsGrades.GetDropDown(ddlGrades)

                If ddlGrades.Items.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, clsNavigation.SetLanguage(Page, "You must enter at least one grade/يجب أن تدخل على الأقل درجة وظيفية"))
                    Exit Sub
                End If

                SetToolbarSetting(StrMode, ClsGradesSteps, IntId)

                ClsGradesTransactions.GetList(uwgGradesStepsTransactions.Columns(1).ValueList, "hrs_GradesTransactions.GradeID=" & ddlGrades.SelectedValue)
                ApplyNoofDecimalPlaces()
                ClsGradesTransactions.GetDropDownWithMaxAndMin(ddlMinMax, "hrs_GradesTransactions.GradeID=" & ddlGrades.SelectedValue)
                ClsGradesSteps = New Clshrs_GradesSteps(Page)
                ClsGradesSteps.Find(" hrs_GradesSteps.CancelDate is null and hrs_GradesSteps.GradeID=" & ddlGrades.SelectedValue)
                uwgGradesSteps.DataSource = ClsGradesSteps.DataSet
                uwgGradesSteps.DataBind()
                If uwgGradesSteps.Rows.Count > 0 Then
                End If
            End If
            'Setsetting(ClsGradesSteps.ID)
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If ClsGradesSteps.Find(" hrs_GradesSteps.Code = '" & txtCode.Text & "' And hrs_GradesSteps.GradeID=" & ddlGrades.SelectedValue) Then

                IntrecordID = ClsGradesSteps.ID
                If (IntrecordID > 0) Then
                    ClsGradesSteps.AddDataUpdateSchedules(Page, "frmGradesSteps", IntrecordID)
                    SetScreenInformation("E")
                Else
                    SetScreenInformation("N")
                End If
            Else
                SetScreenInformation("N")
            End If
            CreateOtherFields(IntrecordID)
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsGrades.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsGrades = New Clshrs_Grades(Me)
        ClsGradesSteps = New Clshrs_GradesSteps(Page)
        ClsGradesStepsTransactions = New Clshrs_GradesStepsTransactions(Page)
        Dim StrSerial As String = ""
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsGrades.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                SavePart()
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsGrades.Table, ClsGrades.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                SavePart()
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsGrades.Table, ClsGrades.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsGradesSteps.Find(" hrs_GradesSteps.ID=" & txtID.Text)
                If (ClsGradesSteps.ID > 0) Then
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " Grade Step Not Found ")
                    Exit Sub
                End If
                ClsGradesStepsTransactions.Delete("hrs_GradesStepsTransactions.GradeStepID=" & txtID.Text)
                ClsGradesSteps.Delete("hrs_GradesSteps.ID=" & txtID.Text)
                AfterOperation()
            Case "Property"
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsGrades.ID & "&TableName=" & ClsGrades.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsGrades.ID & "&TableName=" & ClsGrades.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsGrades.Table
                ClsGrades.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsGrades.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsGrades.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsGrades.Find(" Code= '" & txtCode.Text & "'")
                If ClsGrades.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsGrades.DataSet
                    If Not AssignValue() Then
                        Exit Sub
                    End If
                    If ClsGrades.CheckDiff(ClsGrades, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsGrades.FirstRecord()
                GetValues()
            Case "Previous"
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                If Not ClsGrades.previousRecord() Then
                    ClsGrades.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                If Not ClsGrades.NextRecord() Then
                    ClsGrades.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsGrades.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub ddlGrades_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGrades.SelectedIndexChanged
        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim StrSerial As String = String.Empty
        GetValuesGrades()
        If uwgGradesSteps.Rows.Count = 0 Then
            txtCode.Text = StrSerial
        End If
    End Sub
    Protected Sub uwgGradesSteps_ActiveCellChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgGradesSteps.ActiveCellChange
        If Not e.Cell.Row.Cells.FromKey("ID").Text = Nothing Then
            ClsGradesStepsTransactions = New Clshrs_GradesStepsTransactions(Page)
            txtID.Text = IIf(e.Cell.Row.Cells.FromKey("ID").Text Is Nothing, 0, e.Cell.Row.Cells.FromKey("ID").Text)
            txtCode.Text = IIf(e.Cell.Row.Cells.FromKey("Code").Text Is Nothing, "", e.Cell.Row.Cells.FromKey("Code").Text)
            txtEngName.Text = IIf(e.Cell.Row.Cells.FromKey("EngName").Text Is Nothing, "", e.Cell.Row.Cells.FromKey("EngName").Text)
            txtArbName.Text = IIf(e.Cell.Row.Cells.FromKey("ArbName").Text Is Nothing, "", e.Cell.Row.Cells.FromKey("ArbName").Text)

            ClsGradesStepsTransactions.Find("hrs_GradesStepsTransactions.GradeStepID=" & txtID.Text)
            uwgGradesStepsTransactions.DataSource = ClsGradesStepsTransactions.DataSet.Tables(0).DefaultView
            uwgGradesStepsTransactions.DataBind()
            AddToGridEmptyRows()
        End If
    End Sub
    Protected Sub uwgGradesSteps_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgGradesSteps.ActiveRowChange

        If Not e.Row.Cells.FromKey("ID").Text = Nothing Then

            txtID.Text = IIf(e.Row.Cells.FromKey("ID").Text Is Nothing, 0, e.Row.Cells.FromKey("ID").Text)
            txtCode.Text = IIf(e.Row.Cells.FromKey("Code").Text Is Nothing, "", e.Row.Cells.FromKey("Code").Text)
            txtEngName.Text = IIf(e.Row.Cells.FromKey("EngName").Text Is Nothing, "", e.Row.Cells.FromKey("EngName").Text)
            txtArbName.Text = IIf(e.Row.Cells.FromKey("ArbName").Text Is Nothing, "", e.Row.Cells.FromKey("ArbName").Text)

            ''Update by Abdulrahman

            'ClsGradesStepsTransactions = New Clshrs_GradesStepsTransactions(Page)
            'ClsGradesStepsTransactions.Find("hrs_GradesStepsTransactions.GradeStepID=" & txtID.Text.ToString)
            'uwgGradesStepsTransactions.DataSource = ClsGradesStepsTransactions.DataSet.Tables(0).DefaultView
            'uwgGradesStepsTransactions.DataBind()
            'AddToGridEmptyRows()

            ClsGradesSteps = New Clshrs_GradesSteps(Me)
            ClsGradesSteps.Find(" hrs_GradesSteps.ID=" & txtID.Text)
            GetValues()
            SetToolBarPermission(Me, ClsGradesStepsTransactions.ConnectionString, ClsGradesStepsTransactions.DataBaseUserRelatedID, ClsGradesStepsTransactions.GroupID, "N")
        End If
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        Dim StrSerial As String = txtCode.Text.Trim
        If Not CheckCodeinGrid(txtCode.Text.Trim) Then
            Clear()
            txtCode.Text = StrSerial
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsGradesSteps.ConnectionString, ClsGradesSteps.DataBaseUserRelatedID, ClsGradesSteps.GroupID, "N")
            'ImageButton_Delete.Enabled = False

        End If
    End Sub

#End Region

#Region "Private Functions"
    Private Function GetValuesGrades() As Boolean
        ClsGradesSteps = New Clshrs_GradesSteps(Page)
        If (ClsGradesSteps.Find(" GradeID=" & ddlGrades.SelectedValue)) Then
            GetValues()
        End If
        ClsGradesTransactions = New Clshrs_GradesTransactions(Page)
        If (ClsGradesSteps.ID > 0) Then
            ClsGradesStepsTransactions.Find("hrs_GradesStepsTransactions.GradeStepID=" & ClsGradesSteps.ID.ToString())
            uwgGradesStepsTransactions.DataSource = ClsGradesStepsTransactions.DataSet.Tables(0).DefaultView
            uwgGradesStepsTransactions.DataBind()
            AddToGridEmptyRows()
            ClsGradesTransactions.GetList(uwgGradesStepsTransactions.Columns(1).ValueList, "hrs_GradesTransactions.GradeID=" & ddlGrades.SelectedValue)
            ApplyNoofDecimalPlaces()
            ClsGradesTransactions.GetDropDownWithMaxAndMin(ddlMinMax, "hrs_GradesTransactions.GradeID=" & ddlGrades.SelectedValue)
        Else
            Clear()
            uwgGradesStepsTransactions.DataSource = Nothing
            uwgGradesStepsTransactions.DataBind()
            AddToGridEmptyRows()
            uwgGradesSteps.DataSource = ClsGradesSteps.DataSet.Tables(0).DefaultView
            uwgGradesSteps.DataBind()
            ClsGradesTransactions.GetList(uwgGradesStepsTransactions.Columns(1).ValueList, "hrs_GradesTransactions.GradeID=" & ddlGrades.SelectedValue)
            ApplyNoofDecimalPlaces()
            ClsGradesTransactions.GetDropDownWithMaxAndMin(ddlMinMax, "hrs_GradesTransactions.GradeID=" & ddlGrades.SelectedValue)
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsGradesSteps.ConnectionString, ClsGradesSteps.DataBaseUserRelatedID, ClsGradesSteps.GroupID, "N")
            ImageButton_Delete.Enabled = False
            Return False
        End If
        uwgGradesSteps.DataSource = ClsGradesSteps.DataSet.Tables(0).DefaultView
        uwgGradesSteps.DataBind()
        If uwgGradesSteps.Rows.Count > 0 Then
            uwgGradesSteps.Rows(0).Activate()
            uwgGradesSteps.Rows(0).Selected = True
        End If
        Return True
    End Function
    Private Function ApplyNoofDecimalPlaces() As Boolean
        Dim clsMainCountry As New Clssys_Countries(Page)
        Dim clsMainCurrency As New ClsSys_Currencies(Page)
        clsMainCountry.Find(" IsMainCountries = 1 ")
        If clsMainCountry.ID > 0 Then
            clsMainCurrency.Find(" ID=" & clsMainCountry.CurrencyID)
        End If
        Dim StrMinMax As String = ""
        If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
            'For Each valuelistitem In uwgGradesStepsTransactions.Columns(1).ValueList.ValueListItems
            '    StrMinMax = valuelistitem.DisplayText.Split("(")(1).Split(",")(0) & "," & valuelistitem.DisplayText.Split("(")(1).Split(",")(1).Split(")")(0)
            '    StrMinMax = Math.Round(Convert.ToDouble(StrMinMax.Split(",")(0)), clsMainCurrency.NoDecimalPlaces).ToString & "," & Math.Round(Convert.ToDouble(StrMinMax.Split(",")(1)), clsMainCurrency.NoDecimalPlaces).ToString
            '    valuelistitem.DisplayText = valuelistitem.DisplayText.Split("Between(")(0) & "Between(" & StrMinMax & ")"
            'Next
        End If

    End Function
    Private Sub AddToGridEmptyRows()
        If uwgGradesStepsTransactions.Rows.Count < cNoOfEmptyRows Then
            For i As Integer = 0 To (cNoOfEmptyRows - uwgGradesStepsTransactions.Rows.Count)
                uwgGradesStepsTransactions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow())
            Next
        End If
    End Sub
    Private Sub SetEmptyEmptyRows()
        uwgGradesStepsTransactions.Rows.Clear()
        For i As Integer = 0 To cNoOfEmptyRows
            uwgGradesStepsTransactions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow())
        Next

    End Sub
    Private Function SetReturnback() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim StrTargetControl As String = Request.QueryString.Item("TargetControl")
        Select Case StrMode
            Case "R"
                Venus.Shared.Web.ClientSideActions.DoReturnBack(Page, StrTargetControl)
            Case Else
                Venus.Shared.Web.ClientSideActions.DoRefresh(Page)
        End Select
    End Function
    Private Function GetFormPermission(ByVal frmCode As String) As Boolean
        Dim ClsForms As New ClsSys_Forms(Page)
        Dim ClsFormPermission As New ClsSys_FormsPermissions(Page)
        Dim StrFormPermission As String = "1,1,1"
        If ClsForms.Find(" Code='" & frmCode & "'") Then
            ClsFormPermission.Find("FormID=" & ClsForms.ID & " and UserID =" & ClsForms.DataBaseUserRelatedID)
            With ClsFormPermission
                If .ID > 0 Then
                    StrFormPermission = ""
                    If .AllowEdit Then
                        StrFormPermission = "0"
                    Else
                        StrFormPermission = "1"
                    End If
                    If .AllowDelete Then
                        StrFormPermission &= ",0"
                    Else
                        StrFormPermission &= ",1"
                    End If

                    If .AllowPrint Then
                        StrFormPermission &= ",0"
                    Else
                        StrFormPermission &= ",1"
                    End If
                End If
            End With
        End If
        txtFormPermission.Value = StrFormPermission
    End Function

    Private Function CheckCodeinGrid(ByVal strCode) As Boolean
        Try
            If uwgGradesSteps.Rows.Count > 0 Then
                ClsGradesStepsTransactions = New Clshrs_GradesStepsTransactions(Page)
                For Each objDeactiveRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGradesSteps.Rows
                    objDeactiveRow.Activated = False
                    objDeactiveRow.Selected = False
                Next

                For Each objrow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGradesSteps.Rows
                    If Not IsNothing(objrow.Cells.FromKey("Code").Value) Then
                        If strCode = objrow.Cells.FromKey("Code").Value Then
                            objrow.Activate()
                            objrow.Activated = True
                            objrow.Selected = True
                            txtID.Text = IIf(objrow.Cells.FromKey("ID").Text Is Nothing, 0, objrow.Cells.FromKey("ID").Text)
                            txtCode.Text = IIf(objrow.Cells.FromKey("Code").Text Is Nothing, "", objrow.Cells.FromKey("Code").Text)
                            txtEngName.Text = IIf(objrow.Cells.FromKey("EngName").Text Is Nothing, "", objrow.Cells.FromKey("EngName").Text)
                            txtArbName.Text = IIf(objrow.Cells.FromKey("ArbName").Text Is Nothing, "", objrow.Cells.FromKey("ArbName").Text)
                            ClsGradesStepsTransactions.Find("hrs_GradesStepsTransactions.GradeStepID=" & txtID.Text)
                            uwgGradesStepsTransactions.DataSource = ClsGradesStepsTransactions.DataSet.Tables(0).DefaultView
                            uwgGradesStepsTransactions.DataBind()
                            AddToGridEmptyRows()

                            Return True
                        End If
                    End If
                Next
                Return False
            Else
                Return False
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsGradesSteps.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function AssignValue() As Boolean
        Dim GId As Integer = CInt(ddlGrades.SelectedValue)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsGradesSteps.ConnectionString)

        If uwgGradesStepsTransactions.Rows.Count = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "At least one transaction must found/يجب أن يكون بند واحد على الأقل"))
            Return False
        End If
        Try

            Dim arr As New ArrayList()
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGradesStepsTransactions.Rows
                If ((Not IsNothing(DGRow.Cells(1).Value)) AndAlso arr.Contains(DGRow.Cells(1).Value)) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Cannot Add same grade step transactions to this grade step")
                    Return False
                End If
                arr.Add(DGRow.Cells(1).Value)
            Next

            ClsGradesSteps.Find(" hrs_GradesSteps.GradeID=" & ddlGrades.SelectedValue)
            Dim cnt As Integer = ClsGradesSteps.DataSet.Tables(0).Rows.Count
            '-------------------------------0257 MODIFIED-----------------------------------------
            ClsGradesSteps.Find(" hrs_GradesSteps.ID=" & IIf(txtID.Text = "", "0", txtID.Text))
            '-------------------------------=============-----------------------------------------

            With ClsGradesSteps
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .ArbName4S = txtArbName.Text
                .StepNo = cnt + 1
                .GradeID = ddlGrades.SelectedValue
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsGradesStepsTransactions = New Clshrs_GradesStepsTransactions(Page)
        Try
            SetToolBarDefaults()
            With ClsGradesSteps
                txtID.Text = .ID
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName


                ddlGrades.Items.Clear()
                Dim ClsGrd As New Clshrs_Grades(Page)
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(.ConnectionString)
                Dim item As New System.Web.UI.WebControls.ListItem()
                ClsGrd.GetDropDown(ddlGrades)


                ClsGrd.Find(" ID= " & IIf(IsNothing(.GradeID), 0, .GradeID))
                If ClsGrd.ID > 0 Then

                    item.Value = .GradeID
                    item.Text = ObjNavigationHandler.SetLanguage(Page, ClsGrd.EngName & "/" & ClsGrd.ArbName)

                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Page, ClsGrd.ArbName & "/" & ClsGrd.EngName)

                    End If

                    If Not ddlGrades.Items.Contains(item) Then
                        ddlGrades.Items.Add(item)
                        ddlGrades.SelectedValue = item.Value
                    Else
                        ddlGrades.SelectedValue = .GradeID
                    End If

                End If
                '-------------------------------=============-----------------------------------------

                ClsGradesStepsTransactions.Find("hrs_GradesStepsTransactions.GradeStepID=" & .ID.ToString())
                uwgGradesStepsTransactions.DataSource = ClsGradesStepsTransactions.DataSet.Tables(0).DefaultView
                uwgGradesStepsTransactions.DataBind()
                AddToGridEmptyRows()

            End With
            If Not ClsGrades.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsGrades.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsGrades.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsGrades.RegDate).Date
            End If
            If ClsGrades.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsGrades.CancelDate).Date
            End If
            If Not ClsGrades.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If

            If (ClsGrades.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsGrades.ConnectionString, ClsGrades.DataBaseUserRelatedID, ClsGrades.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsGrades.ConnectionString, ClsGrades.DataBaseUserRelatedID, ClsGrades.GroupID, ClsGrades.Table, ClsGrades.ID)
            If Not ClsGrades.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsGrades.ID)
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim StrSerial As String = String.Empty
        ClsGradesSteps = New Clshrs_GradesSteps(Page)
        ClsGradesStepsTransactions = New Clshrs_GradesStepsTransactions(Page)

        Try
            StrSerial = txtCode.Text
            If (txtID.Text = "0") Then
                If Not AssignValue() Then
                    Exit Function
                End If
                ClsGradesSteps.Save()
            Else
                ClsGradesSteps.Find(" hrs_GradesSteps.ID = " & IIf(txtID.Text = "", 0, txtID.Text))
                If ClsGradesSteps.ID > 0 Then
                    If Not AssignValue() Then
                        Exit Function
                    End If
                    ClsGradesSteps.Update("hrs_GradesSteps.ID=" & IIf(txtID.Text = "", 0, txtID.Text))
                    'AfterOperation()
                Else
                    If Not AssignValue() Then
                        Exit Function
                    End If
                    ClsGradesSteps.Save()
                End If
            End If
            ClsGradesSteps.Find(" hrs_GradesSteps.Code='" & txtCode.Text & "' And hrs_GradesSteps.GradeID=" & ddlGrades.SelectedValue)
            SaveDG(ClsGradesSteps.ID)

            'AfterOperation()

            txtCode.Text = StrSerial
            ClsGradesSteps = New Clshrs_GradesSteps(Page)
            ClsGradesSteps.Find("GradeID=" & ddlGrades.SelectedValue)
            uwgGradesSteps.DataSource = ClsGradesSteps.DataSet.Tables(0).DefaultView
            uwgGradesSteps.DataBind()
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsGradesSteps.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SaveDG(ByVal StepID As Integer) As Boolean
        ClsGradesStepsTransactions = New Clshrs_GradesStepsTransactions(Page)
        ClsGradesStepsTransactions.DeleteAll("hrs_GradesStepsTransactions.GradeStepID=" & StepID)
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGradesStepsTransactions.Rows
            If IsNothing(DGRow.Cells(1).Value) Then
                Continue For
            End If
            ClsGradesStepsTransactions = New Clshrs_GradesStepsTransactions(Page)
            ClsGradesStepsTransactions.GradeStepID = StepID
            ClsGradesStepsTransactions.GradeTransactionID = DGRow.Cells(1).Value
            ClsGradesStepsTransactions.Amount = DGRow.Cells(2).Value
            ClsGradesStepsTransactions.Active = DGRow.Cells.FromKey("Active").Value
            ClsGradesStepsTransactions.Save()
        Next
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
                    txtCode.Text = String.Empty
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
                    ClsGrades.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsGrades.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsGrades
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
        ClsGrades = New Clshrs_Grades(Me)
        Try
            With ClsGrades
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
        ClsGrades = New Clshrs_Grades(Me)
        If IntId > 0 Then
            ClsGrades.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        Clear()
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtID.Text = "0"
        txtCode.Text = ""
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        uwgGradesStepsTransactions.DataSource = Nothing
        uwgGradesStepsTransactions.DataBind()
        AddToGridEmptyRows()

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsGrades = New Clshrs_Grades(Page)
        ClsGrades.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsGrades.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsGrades.Table) = True Then
            Dim StrTablename As String
            ClsGrades = New Clshrs_Grades(Me)
            StrTablename = ClsGrades.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID & _
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID & _
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

#End Region

End Class