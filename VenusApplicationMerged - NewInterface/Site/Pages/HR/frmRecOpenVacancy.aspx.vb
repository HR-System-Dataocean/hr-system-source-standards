Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class Pages_HR_frmRecOpenVacancy
    Inherits MainPage


#Region "Public Decleration"

    Private ClsOpenVacancy As ClsRec_OpenVacancy
    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim mDataHandler As New Venus.Shared.DataHandler
        ClsOpenVacancy = New ClsRec_OpenVacancy(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
        Dim clsLocations = New Clssys_Locations(Page)
        Dim clsPositions = New Clshrs_Positions(Page)
        Dim clsEducationDegree = New ClsRec_EducationDegree(Page)
        Dim clsContractTypes = New Clshrs_ContractTypes(Page)
        Dim clsGrades = New Clshrs_Grades(Page)
        Dim clsGradesSteps = New Clshrs_GradesSteps(Page)
        Dim SearchID As Integer = 0

        Try

            ClsOpenVacancy.AddNotificationOnChange(Page)
            If ClsObjects.Find(" Code='" & ClsOpenVacancy.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID

                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If


            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsOpenVacancy.ConnectionString)

                ClsOpenVacancy.AddOnChangeEventToControls("frmRecOpenVacancy", Page, UltraWebTab1)
                Setsetting(IntId)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                clsPositions.GetDropDownList(ddlPosition, True)
                clsLocations.GetDropDownList(ddlLocations, True)
                clsEducationDegree.GetDropDownList(ddlEDegree, True)
                clsContractTypes.GetDropDownList(ddlConTypes, True)
                clsGrades.GetDropDown(ddlGrade, True)
                clsGradesSteps.GetDropDownList(ddlGeadeStep, True)
                ddlGeadeStep.SelectedIndex = 0
            End If
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsOpenVacancy.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsOpenVacancy.ID
                If (IntrecordID > 0) Then
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsOpenVacancy.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsOpenVacancy = New ClsRec_OpenVacancy(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsOpenVacancy.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                Dim Str As String = "select * from Rec_Positions where ID = " & ddlPosition.SelectedValue & " and PositionQTY  - " & _
                    "convert(int, (select ISNULL(count(ID),0) from hrs_Employees where CancelDate is null and ExcludeDate is null and ID in (select EmployeeID from dbo.hrs_Contracts where CancelDate is null and PositionID = Rec_Positions.Positions_ID))) - " & _
                    "convert(int, (select ISNULL(count(ID),0) from Rec_OpenVacancy where CancelDate is null and IsOpen = 1 and Code <> '" & txtCode.Text & "')) > 0"
                Dim DS As Data.DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsOpenVacancy.ConnectionString, Data.CommandType.Text, Str)
                If txtCode.Text.Length = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                    Return
                ElseIf ddlPosition.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Position /لابد من إدخال الوظيفة"))
                    Return
                ElseIf ddlEDegree.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Eduction Degree /لابد من إدخال المستوى التعليمى"))
                    Return
                ElseIf ddlGrade.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Grade /لابد من إدخال الدرجة"))
                    Return
                ElseIf ddlGeadeStep.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Grade Step /لابد من إدخال خطوات الدرجة"))
                    Return
                    'ElseIf DS.Tables(0).Rows.Count < 1 Then
                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This Positions Has No Limit In Company Position Structure /هذه الوظيفة لا توجد بها اماكن شاغرة بالهيكل الإدارى للشركة"))
                    '    Return
                End If
                SavePart()
                AfterOperation()


            Case "Save"
                Dim Str As String = "select * from Rec_Positions where ID = " & ddlPosition.SelectedValue & " and PositionQTY  - " & _
                                    "convert(int, (select ISNULL(count(ID),0) from hrs_Employees where CancelDate is null and ExcludeDate is null and ID in (select EmployeeID from dbo.hrs_Contracts where CancelDate is null and PositionID = Rec_Positions.Positions_ID))) - " & _
                                    "convert(int, (select ISNULL(count(ID),0) from Rec_OpenVacancy where CancelDate is null and IsOpen = 1 and Code <> '" & txtCode.Text & "')) > 0"
                Dim DS As Data.DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsOpenVacancy.ConnectionString, Data.CommandType.Text, Str)
                If txtCode.Text.Length = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                    Return
                ElseIf ddlPosition.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Position /لابد من إدخال الوظيفة"))
                    Return
                ElseIf ddlEDegree.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Eduction Degree /لابد من إدخال المستوى التعليمى"))
                    Return
                ElseIf ddlGrade.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Grade /لابد من إدخال الدرجة"))
                    Return
                ElseIf ddlGeadeStep.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Grade Step /لابد من إدخال خطوات الدرجة"))
                    Return
                    'ElseIf DS.Tables(0).Rows.Count < 1 Then
                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This Positions Has No Limit In Company Position Structure /هذه الوظيفة لا توجد بها اماكن شاغرة بالهيكل الإدارى للشركة"))
                    '    Return
                End If
                SavePart()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done /تم الحفظ"))

            Case "New"
                AfterOperation()

            Case "Delete"
                ClsOpenVacancy.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                If ClsOpenVacancy.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsOpenVacancy.ID & "&TableName=" & ClsOpenVacancy.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Remarks"
                If ClsOpenVacancy.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsOpenVacancy.ID & "&TableName=" & ClsOpenVacancy.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsOpenVacancy.Find("Code='" & txtCode.Text & "'")
                ClsOpenVacancy.FirstRecord()
                GetValues(ClsOpenVacancy)
            Case "Previous"
                ClsOpenVacancy.Find("Code='" & txtCode.Text & "'")
                If Not ClsOpenVacancy.previousRecord() Then
                    ClsOpenVacancy.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues(ClsOpenVacancy)
            Case "Next"
                ClsOpenVacancy.Find("Code='" & txtCode.Text & "'")
                If Not ClsOpenVacancy.NextRecord() Then
                    ClsOpenVacancy.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues(ClsOpenVacancy)
            Case "Last"
                ClsOpenVacancy.Find("Code='" & txtCode.Text & "'")
                ClsOpenVacancy.LastRecord()
                GetValues(ClsOpenVacancy)
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub btnSearchCode_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSearchCode.Click
        ClsOpenVacancy = New ClsRec_OpenVacancy(Me.Page)
        ClsOpenVacancy.Find("Code='" & txtCode.Text & "'")
        GetValues(ClsOpenVacancy)
    End Sub
    Protected Sub ddlGrade_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGrade.SelectedIndexChanged
        Dim clsGradesSteps = New Clshrs_GradesSteps(Page)
        clsGradesSteps.GetDropDownList(ddlGeadeStep, True, "GradeID = " & ddlGrade.SelectedValue & " and CancelDate is Null")
    End Sub
    Protected Sub txtGStartDate_ValueChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtGStartDate.ValueChange
        Dim GDate As String = txtGStartDate.Value
        If ClsDataAcessLayer.IsGreg(GDate) = False Then
            GDate = ClsDataAcessLayer.FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            txtGStartDate.Value = GDate
        End If
        txtHStartDate.Value = ClsDataAcessLayer.GregToHijri(GDate, "dd/MM/yyyy")
    End Sub
    Protected Sub txtGEndDate_ValueChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtGEndDate.ValueChange
        Dim GDate As String = txtGEndDate.Value
        If ClsDataAcessLayer.IsGreg(GDate) = False Then
            GDate = ClsDataAcessLayer.FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            txtGEndDate.Value = GDate
        End If
        txtHEndDate.Value = ClsDataAcessLayer.GregToHijri(GDate, "dd/MM/yyyy")
    End Sub
    Protected Sub txtHStartDate_ValueChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtHStartDate.ValueChange
        Dim HDate As String = txtHStartDate.Value
        If ClsDataAcessLayer.IsHijri(HDate) = False Then
            HDate = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            txtHStartDate.Value = HDate
        End If
        txtGStartDate.Value = ClsDataAcessLayer.HijriToGreg(HDate, "dd/MM/yyyy")
    End Sub
    Protected Sub txtHEndDate_ValueChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtHEndDate.ValueChange
        Dim HDate As String = txtHEndDate.Value
        If ClsDataAcessLayer.IsHijri(HDate) = False Then
            HDate = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            txtHEndDate.Value = HDate
        End If
        txtGEndDate.Value = ClsDataAcessLayer.HijriToGreg(HDate, "dd/MM/yyyy")
    End Sub

#End Region

#Region "Private Function"

    Private Function AssignValue(ByRef ClsOpenVacancy As ClsRec_OpenVacancy) As Boolean
        Try
            With ClsOpenVacancy
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                If ddlPosition.SelectedValue <> 0 Then
                    .Position_ID = ddlPosition.SelectedValue
                Else
                    .Position_ID = Nothing
                End If
                If ddlLocations.SelectedValue <> 0 Then
                    .Location_ID = ddlLocations.SelectedValue
                Else
                    .Location_ID = Nothing
                End If
                If ddlConTypes.SelectedValue <> 0 Then
                    .ApplicantType = ddlConTypes.SelectedValue
                Else
                    .ApplicantType = Nothing
                End If
                If ddlEDegree.SelectedValue <> 0 Then
                    .EDegree_ID = ddlEDegree.SelectedValue
                Else
                    .EDegree_ID = Nothing
                End If
                If ddlGrade.SelectedValue <> 0 Then
                    .Grade_ID = ddlGrade.SelectedValue
                Else
                    .Grade_ID = Nothing
                End If
                If ddlGeadeStep.SelectedValue <> 0 Then
                    .GradeStep_ID = ddlGeadeStep.SelectedValue
                Else
                    .GradeStep_ID = Nothing
                End If
                .Skills = txtskills.Text
                .GStartDate = ClsDataAcessLayer.FormatGreg(txtGStartDate.Value, "dd/MM/yyyy")
                .HStartDate = txtHStartDate.Value
                .GEndDate = ClsDataAcessLayer.FormatGreg(txtGEndDate.Value, "dd/MM/yyyy")
                .HEndDate = txtHEndDate.Value
                .YOExperience = txtExperionces.Value
                .IsOpen = VStatus.Checked
                .IsExternal = IsExternal.Checked
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetValues(ByRef ClsOpenVacancy As ClsRec_OpenVacancy) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsOpenVacancy
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                ddlEDegree.SelectedValue = .EDegree_ID
                ddlLocations.SelectedValue = .Location_ID
                ddlPosition.SelectedValue = .Position_ID
                ddlConTypes.SelectedValue = .ApplicantType
                ddlGrade.SelectedValue = .Grade_ID
                ddlGeadeStep.SelectedValue = .GradeStep_ID
                txtGStartDate.Value = .GStartDate
                txtHStartDate.Value = .HStartDate
                txtGEndDate.Value = .GEndDate
                txtHEndDate.Value = .HEndDate
                txtExperionces.Value = .YOExperience
                VStatus.Checked = .IsOpen
                IsExternal.Checked = .IsExternal
                txtskills.Text = .Skills
                If Not ClsOpenVacancy.RegUserID = Nothing Then
                    ClsUser.Find("ID=" & ClsOpenVacancy.RegUserID)
                End If
                If ClsUser.EngName = Nothing Then
                    lblRegUserValue.Text = ""
                Else
                    lblRegUserValue.Text = ClsUser.EngName
                End If
                If Convert.ToDateTime(ClsOpenVacancy.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = Convert.ToDateTime(ClsOpenVacancy.RegDate).Date
                End If
                If ClsOpenVacancy.CancelDate = Nothing Then
                    lblCancelDateValue.Text = ""
                    ImageButton_Delete.Enabled = True
                Else
                    lblCancelDateValue.Text = Convert.ToDateTime(ClsOpenVacancy.CancelDate).Date
                    ImageButton_Delete.Enabled = False
                End If
            End With


            If (ClsOpenVacancy.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If

            SetToolBarPermission(Me, ClsOpenVacancy.ConnectionString, ClsOpenVacancy.DataBaseUserRelatedID, ClsOpenVacancy.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsOpenVacancy.ConnectionString, ClsOpenVacancy.DataBaseUserRelatedID, ClsOpenVacancy.GroupID, ClsOpenVacancy.Table, ClsOpenVacancy.ID)
            If Not ClsOpenVacancy.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If

            If Page.IsPostBack Then CreateOtherFields(ClsOpenVacancy.ID)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsOpenVacancy = New ClsRec_OpenVacancy(Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Page)

        ClsOpenVacancy.Find("Code='" & txtCode.Text & "'")
        If Not AssignValue(ClsOpenVacancy) Then
            Exit Function
        End If
        If ClsOpenVacancy.ID > 0 Then
            ClsOpenVacancy.Update("Code='" & txtCode.Text & "'")
        Else
            ClsOpenVacancy.Save()
        End If

        ClsOpenVacancy.Find("Code='" & txtCode.Text & "'")
        clsMainOtherFields.CollectDataAndSave(value.Text, ClsOpenVacancy.Table, ClsOpenVacancy.ID)
        value.Text = ""

        Return True
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsOpenVacancy
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                'Venus.Shared.Web.ClientSideActions.SetPageToolTips(Me, .ConnectionString, "UltraWebTab1")
                'Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    'Venus.Shared.Web.ClientSideActions.SetToolBarPermission(Me, .ConnectionString, TlbMainToolbar, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
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
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Dim ClsTransactionGroups As New Clshrs_TransactionGroups(Page)
        Select Case ptrType
            Case "N", "R"
                ImageButton_First.Visible = False
                ImageButton_Back.Visible = False
                ImageButton_Next.Visible = False
                ImageButton_Last.Visible = False
                ImageButton_Delete.Enabled = False
                ImageButton_Properties.Visible = False
                ImageButton_Print.Visible = False
                ImageButton_Remarks.Visible = False
            Case "D"
                ClsTransactionGroups.Find("ID=" & intID)
                GetValues(ClsOpenVacancy)
                txtCode.ReadOnly = True
                ImageButton_Save.Visible = False
            Case "E"
                ClsTransactionGroups.Find("ID=" & intID)
                GetValues(ClsOpenVacancy)
                txtCode.ReadOnly = True
                ImageButton_Delete.Enabled = False
        End Select
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsOpenVacancy = New ClsRec_OpenVacancy(Page)
        If IntId > 0 Then
            ClsOpenVacancy.Find("ID=" & IntId)
            GetValues(ClsOpenVacancy)
        Else
            Clear()
        End If
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsOpenVacancy = New ClsRec_OpenVacancy(Page)
        Try
            ClsOpenVacancy.Find("Code='" & txtCode.Text & "'")
            IntId = ClsOpenVacancy.ID
            If ClsOpenVacancy.ID > 0 Then
                GetValues(ClsOpenVacancy)
                ImageButton_Delete.Enabled = True
                StrMode = "E"
            Else
                If ClsOpenVacancy.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
                txtEngName.Focus()
            End If

            SetToolBarDefaults()
            'Venus.Shared.Web.ClientSideActions.SetToolBarPermission(Me, ClsOpenVacancy.ConnectionString, TlbMainToolbar, ClsOpenVacancy.DataBaseUserRelatedID, ClsOpenVacancy.GroupID, StrMode)
            'Venus.Shared.Web.ClientSideActions.SetToolBarRecordPermission(Me, ClsOpenVacancy.ConnectionString, TlbMainToolbar, ClsOpenVacancy.DataBaseUserRelatedID, ClsOpenVacancy.GroupID, ClsOpenVacancy.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If


        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsOpenVacancy.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsOpenVacancy.Clear()
        GetValues(ClsOpenVacancy)
        txtGStartDate.Value = ClsDataAcessLayer.FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtHStartDate.Value = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtGEndDate.Value = ClsDataAcessLayer.FormatGreg(DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtHEndDate.Value = ClsDataAcessLayer.FormatHijri(DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        ImageButton_Delete.Enabled = False
        UltraWebTab1.SelectedTab = 0
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        ddlLocations.SelectedValue = 0
        ddlPosition.SelectedValue = 0
        ddlConTypes.SelectedValue = 0
        ddlEDegree.SelectedValue = 0
        ddlGrade.SelectedValue = 0
        ddlGeadeStep.SelectedValue = 0
        txtGStartDate.Value = ClsDataAcessLayer.FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtHStartDate.Value = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtGEndDate.Value = ClsDataAcessLayer.FormatGreg(DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtHEndDate.Value = ClsDataAcessLayer.FormatHijri(DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtExperionces.Value = 0
        txtskills.Text = ""
        VStatus.Checked = True
        IsExternal.Checked = False
        ImageButton_Delete.Enabled = False
        lblRegUserValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
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
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsOpenVacancy = New ClsRec_OpenVacancy(Me.Page)
        ClsOpenVacancy.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsOpenVacancy.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsOpenVacancy.Table) = True Then
            Dim StrTablename As String
            ClsOpenVacancy = New ClsRec_OpenVacancy(Me)
            StrTablename = ClsOpenVacancy.Table
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

#End Region

 
End Class
