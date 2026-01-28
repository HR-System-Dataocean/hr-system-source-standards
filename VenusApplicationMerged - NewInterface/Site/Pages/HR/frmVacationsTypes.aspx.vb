Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmVacationsTypes
    Inherits MainPage
#Region "Public Decleration"
    Private ClsVacationType As Clshrs_VacationsTypes
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsVacationType = New Clshrs_VacationsTypes(Me.Page)
        Dim ClsTransactions As New Clshrs_TransactionsTypes(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsVacationType.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsVacationType.ConnectionString)
                ClsVacationType.AddOnChangeEventToControls("frmVacationsTypes", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                chkAnnual.Attributes.Add("onclick", "CheckAnnualVaction()")
                chkSick.Attributes.Add("onclick", "CheckSickVacation()")
                ClsVacationType.GetDropDownList(ddlOverDueVac, True, "IsPaid=-1")

                ClsTransactions.GetDropDownList(ddlOpenBalanceTrans, True, "Sign= 1 and InputIsNumeric = 1 and IsPaid = 1 and isnull(Formula,'') = '' and IsDistributable = 0")
                ClsTransactions.GetDropDownList(ddlclearancetransaction, True, "Sign= 1 and InputIsNumeric = 1 and IsPaid = 1 and isnull(Formula,'') = '' and IsDistributable = 0")
                ClsTransactions.GetDropDownList(ddlDeductTransaction, True, "Sign= -1 and InputIsNumeric = 1 and IsPaid = 1 and isnull(Formula,'') = '' and IsDistributable = 0")
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsVacationType.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsVacationType.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsVacationType.RegUserId, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsVacationType = New Clshrs_VacationsTypes(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsVacationType.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                SavePart()
                AfterOperation()
                GetAnnualSick()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If SavePart() Then
                    GetAnnualSick()
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"
                AfterOperation()
                GetAnnualSick()
            Case "Delete"
                ClsVacationType.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
                GetAnnualSick()
            Case "Property"
                If ClsVacationType.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsVacationType.ID & "&TableName=" & ClsVacationType.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If
            Case "Remarks"
                If ClsVacationType.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsVacationType.ID & "&TableName=" & ClsVacationType.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsVacationType.Find("Code='" & txtCode.Text & "'")
                ClsVacationType.FirstRecord()
                GetValues(ClsVacationType)
                GetAnnualSick()
            Case "Previous"
                ClsVacationType.Find("Code='" & txtCode.Text & "'")
                If Not ClsVacationType.previousRecord() Then
                    ClsVacationType.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))

                End If
                GetValues(ClsVacationType)
                GetAnnualSick()
            Case "Next"
                ClsVacationType.Find("Code='" & txtCode.Text & "'")
                If Not ClsVacationType.NextRecord() Then
                    ClsVacationType.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))

                End If
                GetValues(ClsVacationType)
                GetAnnualSick()
            Case "Last"
                ClsVacationType.Find("Code='" & txtCode.Text & "'")
                ClsVacationType.LastRecord()
                GetValues(ClsVacationType)
                GetAnnualSick()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Sub GetAnnualSick()
        ClsVacationType = New Clshrs_VacationsTypes(Page)
        Dim retStr As String = ClsVacationType.CountAnnual() & "," & ClsVacationType.CountSick()

        If txtCode.Text.Trim <> "" Then
            ClsVacationType.Find("Code='" & txtCode.Text & "'")
            If ClsVacationType.ID > 0 Then
                If ClsVacationType.IsAnnual Then
                    retStr &= ",1"
                Else
                    retStr &= ",0"
                End If

                If ClsVacationType.IsSickVacation Then
                    retStr &= ",1"
                Else
                    retStr &= ",0"
                End If


            Else
                retStr &= ",0,0"
            End If

        Else
            retStr &= ",0,0"
        End If

        retStr &= "," & ClsVacationType.CountAnnual() & "," & ClsVacationType.CountSick()

        txtHidden.Value = retStr

    End Sub
    Private Function SavePart() As Boolean

        Try

            Dim StrMode As String = Request.QueryString.Item("Mode")


            If (chkAnnual.Checked) Then
                UpdateAnnual()
            End If
            If (chkSick.Checked) Then
                UpdateSick()
            End If

            ClsVacationType = New Clshrs_VacationsTypes(Page)

            ClsVacationType.Find("Code='" & txtCode.Text & "'")

            If Not AssignValue(ClsVacationType) Then
                Exit Function
            End If
            If ClsVacationType.ID > 0 Then
                ClsVacationType.Update("Code='" & txtCode.Text & "'")
            Else
                ClsVacationType.Save()
            End If

            ClsVacationType.Find("Code='" & txtCode.Text & "'")

            clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
            clsMainOtherFields.CollectDataAndSave(value.Text, ClsVacationType.Table, ClsVacationType.ID)
            value.Text = ""

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AssignValue(ByRef ClsVacationType As Clshrs_VacationsTypes) As Boolean
        Try
            With ClsVacationType

                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .Stage1PCT = txt1ststage.Value
                .Stage2PCT = txt2ndststage.Value
                .Stage3PCT = txt3edststage.Value
                If (DdlSex.SelectedItem.Value = "0" Or DdlSex.SelectedItem.Value = "M") Then
                    .Sex = "M"
                ElseIf (DdlSex.SelectedItem.Value = "1" Or DdlSex.SelectedItem.Value = "F") Then

                    .Sex = "F"
                Else
                    .Sex = " "
                End If
                .Religion = ddlreligion.SelectedValue
                .IsPaid = CType(DdlIsPaid.SelectedValue, Integer)
                .OBalanceTransactionID = ddlOpenBalanceTrans.SelectedValue
                .OverDueVacationID = ddlOverDueVac.SelectedValue
                .ForSalaryTransaction = ddlclearancetransaction.SelectedValue
                .ForDeductionTransaction = ddlDeductTransaction.SelectedValue
                .ExceededDaysType = Convert.ToInt32(ddlExceededDays.SelectedValue)
                .TimesNoInYear = Convert.ToInt32(txtTimesNoInYear.Text)
                .AllowedDaysNo = Convert.ToInt32(txtAllowedDaysNo.Text)

                If chkConsiderAllowedDays.Checked = True Then
                    ClsVacationType.ConsiderAllowedDays = True
                Else
                    ClsVacationType.ConsiderAllowedDays = False
                End If
                If chkExcluded.Checked = True Then
                    ClsVacationType.ExcludedFromSSRequests = True
                Else
                    ClsVacationType.ExcludedFromSSRequests = False
                End If
                If chkAnnual.Checked = True Then
                    ClsVacationType.IsAnnual = True
                    If ChkRoundAnnualVacBalance.Checked = True Then
                        ClsVacationType.RoundAnnualVacBalance = True
                    Else
                        ClsVacationType.RoundAnnualVacBalance = False
                    End If
                Else
                    ClsVacationType.IsAnnual = False
                End If


                If CheckBox_AffectEOS.Checked = True Then
                    ClsVacationType.AffectEOS = True
                Else
                    ClsVacationType.AffectEOS = False
                End If
                If ChkHasPayment.Checked = True Then
                    ClsVacationType.HasPayment = True
                Else
                    ClsVacationType.HasPayment = False
                End If


                If chkSick.Checked = True Then
                    ClsVacationType.IsSickVacation = True
                Else
                    ClsVacationType.IsSickVacation = False
                End If
                If CHKIsOfficial.Checked = True Then
                    ClsVacationType.IsOfficial = True
                Else
                    ClsVacationType.IsOfficial = False
                End If

                ClsVacationType.OverlapWithAnotherVac = chkOverlap.Checked
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues(ByVal ClsVacationType As Clshrs_VacationsTypes) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsVacationType

                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txt1ststage.Value = .Stage1PCT
                txt2ndststage.Value = .Stage2PCT
                txt3edststage.Value = .Stage3PCT
                txtTimesNoInYear.Text = .TimesNoInYear
                txtAllowedDaysNo.Text = .AllowedDaysNo
                If ClsVacationType.ConsiderAllowedDays = True Then
                    chkConsiderAllowedDays.Checked = True
                Else
                    chkConsiderAllowedDays.Checked = False
                End If
                If ClsVacationType.ExcludedFromSSRequests = True Then
                    chkExcluded.Checked = True
                Else
                    chkExcluded.Checked = False
                End If
                If ClsVacationType.IsAnnual = True Then
                    chkAnnual.Checked = True
                    If ClsVacationType.RoundAnnualVacBalance = True Then
                        ChkRoundAnnualVacBalance.Checked = True
                    Else
                        ChkRoundAnnualVacBalance.Checked = False
                    End If
                Else
                    chkAnnual.Checked = False
                End If

                If ClsVacationType.IsSickVacation = True Then
                    chkSick.Checked = True
                Else
                    chkSick.Checked = False
                End If

                If ClsVacationType.AffectEOS = True Then
                    CheckBox_AffectEOS.Checked = True
                Else
                    CheckBox_AffectEOS.Checked = False
                End If
                If ClsVacationType.HasPayment = True Then
                    ChkHasPayment.Checked = True
                Else
                    ChkHasPayment.Checked = False
                End If
                If ClsVacationType.IsOfficial = True Then
                    CHKIsOfficial.Checked = True
                Else
                    CHKIsOfficial.Checked = False
                End If

                If ClsVacationType.OverlapWithAnotherVac Then
                    chkOverlap.Checked = True
                Else
                    chkOverlap.Checked = False
                End If
                DdlIsPaid.SelectedValue = .IsPaid
                ddlOverDueVac.SelectedValue = .OverDueVacationID
                ddlOpenBalanceTrans.SelectedValue = .OBalanceTransactionID
                ddlclearancetransaction.SelectedValue = .ForSalaryTransaction
                ddlDeductTransaction.SelectedValue = .ForDeductionTransaction
                ddlExceededDays.SelectedValue = .ExceededDaysType
                If .Sex = " " Then
                    DdlSex.SelectedIndex = 0
                ElseIf .Sex = "M" Then
                    DdlSex.SelectedIndex = 1
                ElseIf .Sex = "F" Then
                    DdlSex.SelectedIndex = 2
                End If
                ddlreligion.SelectedValue = .Religion
                If Not ClsVacationType.RegUserID = Nothing Then
                    ClsUser.Find("ID=" & ClsVacationType.RegUserID)
                End If
                If ClsUser.EngName = Nothing Then
                    lblRegUserValue.Text = ""
                Else
                    lblRegUserValue.Text = ClsUser.EngName
                End If
                If Convert.ToDateTime(.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = Convert.ToDateTime(.RegDate).Date
                End If
                If .CancelDate = Nothing Then
                    lblCancelDateValue.Text = ""
                Else
                    lblCancelDateValue.Text = Convert.ToDateTime(.CancelDate).Date
                End If
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                Else
                    ImageButton_Delete.Enabled = True
                End If
                Dim item As New System.Web.UI.WebControls.ListItem()


                If (.ID > 0) Then
                    StrMode = "E"
                Else
                    StrMode = "N"
                End If
                SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                SetToolBarRecordPermission(Me, ClsVacationType.ConnectionString, ClsVacationType.DataBaseUserRelatedID, ClsVacationType.GroupID, ClsVacationType.Table, IntId)
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If
                If Page.IsPostBack Then
                    CreateOtherFields(ClsVacationType.ID)
                End If
            End With


            If chkSick.Checked Then
                txt1ststage.Enabled = True
                txt2ndststage.Enabled = True
                txt3edststage.Enabled = True
                ddlDeductTransaction.Enabled = True
            Else
                txt1ststage.Enabled = False
                txt2ndststage.Enabled = False
                txt3edststage.Enabled = False
                ddlDeductTransaction.Enabled = False
            End If
            If chkAnnual.Checked = False Then
                ddlOpenBalanceTrans.Enabled = False
                ddlOverDueVac.Enabled = False
                ddlclearancetransaction.Enabled = False
                ddlExceededDays.Enabled = False
            Else
                ddlOpenBalanceTrans.Enabled = True
                ddlOverDueVac.Enabled = True
                ddlclearancetransaction.Enabled = True
                ddlExceededDays.Enabled = True
            End If
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
                    ClsVacationType.Find("ID=" & intID)
                    GetValues(ClsVacationType)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsVacationType.Find("ID=" & intID)
                    GetValues(ClsVacationType)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsVacationType
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
        ClsVacationType = New Clshrs_VacationsTypes(Me.Page)
        Try
            With ClsVacationType
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
        ClsVacationType = New Clshrs_VacationsTypes(Me.Page)
        If IntId > 0 Then
            ClsVacationType.Find("ID=" & IntId)
            GetValues(ClsVacationType)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsVacationType = New Clshrs_VacationsTypes(Me.Page)

        Try
            ClsVacationType.Find("Code='" & txtCode.Text & "'")
            IntId = ClsVacationType.ID
            If ClsVacationType.ID > 0 Then
                GetValues(ClsVacationType)

                StrMode = "E"
                txtEngName.Focus()
            Else
                If ClsVacationType.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False

                StrMode = "N"
                txtEngName.Focus()
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsVacationType.ConnectionString, ClsVacationType.DataBaseUserRelatedID, ClsVacationType.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsVacationType.ConnectionString, ClsVacationType.DataBaseUserRelatedID, ClsVacationType.GroupID, ClsVacationType.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
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
        ClsVacationType.Clear()
        GetValues(ClsVacationType)
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty

        chkAnnual.Checked = False
        chkSick.Checked = False
        CheckBox_AffectEOS.Checked = False
        ChkHasPayment.Checked = False
        DdlIsPaid.SelectedIndex = 0
        DdlSex.SelectedIndex = 0
        ddlreligion.SelectedValue = "All"

        ddlOpenBalanceTrans.SelectedIndex = 0
        ddlOverDueVac.SelectedIndex = 0
        ddlclearancetransaction.SelectedIndex = 0
        ddlDeductTransaction.SelectedIndex = 0

        txt1ststage.Value = 100
        txt2ndststage.Value = 75
        txt3edststage.Value = 0

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""

        ddlOpenBalanceTrans.Enabled = False
        ddlOverDueVac.Enabled = False
        ddlclearancetransaction.Enabled = False

        txt1ststage.Enabled = False
        txt2ndststage.Enabled = False
        txt3edststage.Enabled = False
        ddlDeductTransaction.Enabled = False
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsVacationType = New Clshrs_VacationsTypes(Me.Page)
        ClsVacationType.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsVacationType.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsVacationType.Table) = True Then
            Dim StrTablename As String
            ClsVacationType = New Clshrs_VacationsTypes(Page)
            StrTablename = ClsVacationType.Table
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
    <System.Web.Services.WebMethod()> _
    Public Shared Function Get_Searched_Description(ByVal IntSearchId As Integer, ByVal strCode As String) As String
        Dim dsSearchs As New Data.DataSet
        Find("sys_Searchs", " sys_Searchs.Id = " & IntSearchId, dsSearchs)
        Dim dsObjects As New Data.DataSet
        Find("sys_Objects", " sys_Objects.Id = " & dsSearchs.Tables(0).Rows(0).Item("ObjectID"), dsObjects)
        Return GetFieldDescription(strCode, dsObjects.Tables(0).Rows(0).Item("Code"))
    End Function
    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As Data.DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table
        Dim mSqlDataAdapter As New Data.SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter & " And CancelDate IS Null", " Where CancelDate IS Null")
            StrSelectCommand = StrSelectCommand '& orderByStr
            mSqlDataAdapter = New Data.SqlClient.SqlDataAdapter(StrSelectCommand, ConnStr)
            DataSet = New Data.DataSet
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

#Region "Ajax Functions"
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)> _
    Public Function CheckVacationCount() As String
        Dim strCount As String = String.Empty
        ClsVacationType = New Clshrs_VacationsTypes(Me.Page)
        strCount = ClsVacationType.CountAnnual() & "/" & ClsVacationType.CountSick()
        Return strCount
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)> _
    Public Function UpdateAnnual()
        ClsVacationType = New Clshrs_VacationsTypes(Me.Page)
        ClsVacationType.UpdateAnnual()
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)> _
    Public Function UpdateSick()
        ClsVacationType = New Clshrs_VacationsTypes(Me.Page)
        ClsVacationType.UpdateSick()
    End Function

#End Region
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub chkSick_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkSick.CheckedChanged
        If chkSick.Checked Then
            txt1ststage.Enabled = True
            txt2ndststage.Enabled = True
            txt3edststage.Enabled = True
            ddlDeductTransaction.Enabled = True
        Else
            txt1ststage.Enabled = False
            txt2ndststage.Enabled = False
            txt3edststage.Enabled = False
            ddlDeductTransaction.Enabled = False
        End If
    End Sub

    Protected Sub chkAnnual_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAnnual.CheckedChanged
        If chkAnnual.Checked Then
            ddlOpenBalanceTrans.Enabled = True
            ddlOverDueVac.Enabled = True
            ddlclearancetransaction.Enabled = True
        Else
            ddlOpenBalanceTrans.Enabled = False
            ddlOverDueVac.Enabled = False
            ddlclearancetransaction.Enabled = False
        End If
    End Sub
End Class
