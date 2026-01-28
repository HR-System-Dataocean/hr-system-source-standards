Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Partial Class frmProjectsExtra
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsProjects As Clshrs_Projects
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsProjects = New Clshrs_Projects(Me, "hrs_Projects")
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsProjects.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID

                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsProjects.ConnectionString)
                ClsProjects.AddOnChangeEventToControls("frmProjectsExtra", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                Dim clsBranch As New Clssys_Branches(Page)
                clsBranch.GetDropDownList(ddlBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")

                Dim Clslocation As New ClsBasicFiles(Me.Page, "sys_Locations")
                Clslocation.GetDropDownList(DropDownList_Location, True)


                Dim clsTransactionsTypes = New Clshrs_TransactionsTypes(Page)
                clsTransactionsTypes.GetDropDownList(DropDownList_Absent, True, " CancelDate is null and Sign = -1 and IsPaid = 1 and IsProjectRelatedItem = 1")
                clsTransactionsTypes.GetDropDownList(DropDownList_Sick, True, " CancelDate is null and Sign = -1 and IsPaid = 1 and IsProjectRelatedItem = 1")
                clsTransactionsTypes.GetDropDownList(DropDownList_Late, True, " CancelDate is null and Sign = -1 and IsPaid = 1 and IsProjectRelatedItem = 1")
                clsTransactionsTypes.GetDropDownList(DropDownList_Leave, True, " CancelDate is null and Sign = -1 and IsPaid = 1 and IsProjectRelatedItem = 1")
                clsTransactionsTypes.GetDropDownList(DropDownList_OT, True, " CancelDate is null and Sign = 1 and IsPaid = 1 and IsProjectRelatedItem = 1")
                clsTransactionsTypes.GetDropDownList(DropDownList_HOT, True, " CancelDate is null and Sign = 1 and IsPaid = 1 and IsProjectRelatedItem = 1")
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsProjects.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsProjects.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsProjects.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsProjects = New Clshrs_Projects(Me, "hrs_Projects")
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsProjects.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If CheckBox_IsLock.Checked Then
                    ClsProjects.Find("Code='" & txtCode.Text & "'")
                    With ClsProjects
                        .CompanyConditions = txtCompanyConditions.Text
                        .ClientConditions = txtClientConditions.Text
                        .WorkConditions = TextBox_WorkConditions.Text
                    End With
                    ClsProjects.Update("code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                    Exit Sub
                End If
                If IsCopy.Value = "1" Then
                    ClsProjects.Find("Code='" & OldCode.Value & "'")
                    ClsProjects.IsStoped = False
                    CheckBox_IsLock.Checked = False
                    If Not AssignValues() Then
                        Exit Sub
                    End If

                    Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
                    clsprojectchange.Find("ProjectID = " & ClsProjects.ID & " and RegComputerID = 1 order by ID ASC")

                    ClsProjects.Save()
                    ClsProjects.Find("Code='" & txtCode.Text & "'")

                    Dim projectchange As New Clshrs_ProjectChanges(Me)
                    projectchange.FromDate = ClsProjects.StartDate
                    projectchange.Remarks = "إنشاء المشروع"
                    projectchange.RegComputerID = 1
                    projectchange.ProjectID = ClsProjects.ID
                    Dim NewCHangeID As Integer = projectchange.Save()

                    Dim clsprojectrewards As New Clshrs_ProjectRewards(Me)
                    If clsprojectrewards.Find("ProjectChangeID = " & clsprojectchange.ID) Then
                        Dim dt As DataTable = clsprojectrewards.DataSet.Tables(0)
                        For Each row As DataRow In dt.Rows
                            clsprojectrewards = New Clshrs_ProjectRewards(Me)
                            clsprojectrewards.ProjectChangeID = NewCHangeID
                            clsprojectrewards.RewardID = row("RewardID")
                            clsprojectrewards.Occurance = row("Occurance")
                            clsprojectrewards.ExternalValue = row("ExternalValue")
                            clsprojectrewards.ExternalFactor = row("ExternalFactor")
                            clsprojectrewards.InternalValue = row("InternalValue")
                            clsprojectrewards.InternalFactor = row("InternalFactor")
                            clsprojectrewards.Save()
                        Next
                    End If

                    'Add hrs_ProjectPenalities
                    Dim clsprojectpenalities As New Clshrs_ProjectPenalities(Me)
                    If clsprojectpenalities.Find("ProjectChangeID = " & clsprojectchange.ID) Then
                        Dim dt As DataTable = clsprojectpenalities.DataSet.Tables(0)
                        For Each row As DataRow In dt.Rows
                            clsprojectpenalities = New Clshrs_ProjectPenalities(Me)
                            clsprojectpenalities.ProjectChangeID = NewCHangeID
                            clsprojectpenalities.PenaltyID = row("PenaltyID")
                            clsprojectpenalities.Occurance = row("Occurance")
                            clsprojectpenalities.ExternalValue = row("ExternalValue")
                            clsprojectpenalities.ExternalFactor = row("ExternalFactor")
                            clsprojectpenalities.InternalValue = row("InternalValue")
                            clsprojectpenalities.InternalFactor = row("InternalFactor")
                            clsprojectpenalities.Save()
                        Next
                    End If

                    'Add hrs_ProjectSetting
                    Dim clsprojectsetting As New Clshrs_ProjectSetting(Me)
                    If clsprojectsetting.Find("ProjectChangeID = " & clsprojectchange.ID) Then
                        Dim dt As DataTable = clsprojectsetting.DataSet.Tables(0)
                        For Each row As DataRow In dt.Rows
                            clsprojectsetting = New Clshrs_ProjectSetting(Me)
                            clsprojectsetting.ProjectChangeID = NewCHangeID
                            clsprojectsetting.InternalOvertimeFactor = row("InternalOvertimeFactor")
                            clsprojectsetting.ExternalOvertimeFactor = row("ExternalOvertimeFactor")
                            clsprojectsetting.InternalDayOffOvertimeFactor = row("InternalDayOffOvertimeFactor")
                            clsprojectsetting.ExternalDayOffOvertimeFactor = row("ExternalDayOffOvertimeFactor")
                            clsprojectsetting.InternalExtensionValue = row("InternalExtensionValue")
                            clsprojectsetting.ExternalExtensionValue = row("ExternalExtensionValue")
                            clsprojectsetting.InternalAbsentFactor = row("InternalAbsentFactor")
                            clsprojectsetting.ExternalAbsentFactor = row("ExternalAbsentFactor")
                            clsprojectsetting.InternalSickFactor = row("InternalSickFactor")
                            clsprojectsetting.ExternalSickFactor = row("ExternalSickFactor")
                            clsprojectsetting.InternalLeavFactor = row("InternalLeavFactor")
                            clsprojectsetting.ExternalLeavFactor = row("ExternalLeavFactor")
                            clsprojectsetting.InternalPermitDelayFactor = row("InternalPermitDelayFactor")
                            clsprojectsetting.ExternalPermitDelayFactor = row("ExternalPermitDelayFactor")
                            clsprojectsetting.InternalDelayPunishFactor = row("InternalDelayPunishFactor")
                            clsprojectsetting.ExternalDelayPunishFactor = row("ExternalDelayPunishFactor")
                            clsprojectsetting.Save()
                        Next
                    End If

                    ClsProjects.Find("Code='" & txtCode.Text & "'")
                    clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                    clsMainOtherFields.CollectDataAndSave(value.Text, ClsProjects.Table, ClsProjects.ID)
                    value.Text = ""
                    AfterOperation()
                Else
                    ClsProjects.Find("Code='" & txtCode.Text & "'")
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsProjects.ID > 0 Then
                        ClsProjects.Update("code='" & txtCode.Text & "'")
                    Else
                        ClsProjects.Save()
                        ClsProjects.Find("Code='" & txtCode.Text & "'")

                        Dim projectchange As New Clshrs_ProjectChanges(Me)
                        projectchange.FromDate = ClsProjects.StartDate
                        projectchange.Remarks = "إنشاء المشروع"
                        projectchange.RegComputerID = 1
                        projectchange.ProjectID = ClsProjects.ID
                        projectchange.Save()

                    End If
                    ClsProjects.Find("Code='" & txtCode.Text & "'")
                    clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                    clsMainOtherFields.CollectDataAndSave(value.Text, ClsProjects.Table, ClsProjects.ID)
                    value.Text = ""
                    AfterOperation()
                End If
                IsCopy.Value = ""
            Case "Save"
                If CheckBox_IsLock.Checked Then
                    ClsProjects.Find("Code='" & txtCode.Text & "'")
                    With ClsProjects
                        .CompanyConditions = txtCompanyConditions.Text
                        .ClientConditions = txtClientConditions.Text
                        .WorkConditions = TextBox_WorkConditions.Text
                    End With
                    ClsProjects.Update("code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                    Exit Sub
                End If
                If IsCopy.Value = "1" Then
                    ClsProjects.Find("Code='" & OldCode.Value & "'")
                    ClsProjects.IsStoped = False
                    CheckBox_IsLock.Checked = False
                    If Not AssignValues() Then
                        Exit Sub
                    End If

                    Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
                    clsprojectchange.Find("ProjectID = " & ClsProjects.ID & " and RegComputerID = 1 order by ID ASC")

                    ClsProjects.Save()
                    ClsProjects.Find("Code='" & txtCode.Text & "'")

                    Dim projectchange As New Clshrs_ProjectChanges(Me)
                    projectchange.FromDate = ClsProjects.StartDate
                    projectchange.Remarks = "إنشاء المشروع"
                    projectchange.RegComputerID = 1
                    projectchange.ProjectID = ClsProjects.ID
                    Dim NewCHangeID As Integer = projectchange.Save()

                    Dim clsprojectrewards As New Clshrs_ProjectRewards(Me)
                    If clsprojectrewards.Find("ProjectChangeID = " & clsprojectchange.ID) Then
                        Dim dt As DataTable = clsprojectrewards.DataSet.Tables(0)
                        For Each row As DataRow In dt.Rows
                            clsprojectrewards = New Clshrs_ProjectRewards(Me)
                            clsprojectrewards.ProjectChangeID = NewCHangeID
                            clsprojectrewards.RewardID = row("RewardID")
                            clsprojectrewards.Occurance = row("Occurance")
                            clsprojectrewards.ExternalValue = row("ExternalValue")
                            clsprojectrewards.ExternalFactor = row("ExternalFactor")
                            clsprojectrewards.InternalValue = row("InternalValue")
                            clsprojectrewards.InternalFactor = row("InternalFactor")
                            clsprojectrewards.Save()
                        Next
                    End If

                    'Add hrs_ProjectPenalities
                    Dim clsprojectpenalities As New Clshrs_ProjectPenalities(Me)
                    If clsprojectpenalities.Find("ProjectChangeID = " & clsprojectchange.ID) Then
                        Dim dt As DataTable = clsprojectpenalities.DataSet.Tables(0)
                        For Each row As DataRow In dt.Rows
                            clsprojectpenalities = New Clshrs_ProjectPenalities(Me)
                            clsprojectpenalities.ProjectChangeID = NewCHangeID
                            clsprojectpenalities.PenaltyID = row("PenaltyID")
                            clsprojectpenalities.Occurance = row("Occurance")
                            clsprojectpenalities.ExternalValue = row("ExternalValue")
                            clsprojectpenalities.ExternalFactor = row("ExternalFactor")
                            clsprojectpenalities.InternalValue = row("InternalValue")
                            clsprojectpenalities.InternalFactor = row("InternalFactor")
                            clsprojectpenalities.Save()
                        Next
                    End If

                    'Add hrs_ProjectSetting
                    Dim clsprojectsetting As New Clshrs_ProjectSetting(Me)
                    If clsprojectsetting.Find("ProjectChangeID = " & clsprojectchange.ID) Then
                        Dim dt As DataTable = clsprojectsetting.DataSet.Tables(0)
                        For Each row As DataRow In dt.Rows
                            clsprojectsetting = New Clshrs_ProjectSetting(Me)
                            clsprojectsetting.ProjectChangeID = NewCHangeID
                            clsprojectsetting.InternalOvertimeFactor = row("InternalOvertimeFactor")
                            clsprojectsetting.ExternalOvertimeFactor = row("ExternalOvertimeFactor")
                            clsprojectsetting.InternalDayOffOvertimeFactor = row("InternalDayOffOvertimeFactor")
                            clsprojectsetting.ExternalDayOffOvertimeFactor = row("ExternalDayOffOvertimeFactor")
                            clsprojectsetting.InternalExtensionValue = row("InternalExtensionValue")
                            clsprojectsetting.ExternalExtensionValue = row("ExternalExtensionValue")
                            clsprojectsetting.InternalAbsentFactor = row("InternalAbsentFactor")
                            clsprojectsetting.ExternalAbsentFactor = row("ExternalAbsentFactor")
                            clsprojectsetting.InternalSickFactor = row("InternalSickFactor")
                            clsprojectsetting.ExternalSickFactor = row("ExternalSickFactor")
                            clsprojectsetting.InternalLeavFactor = row("InternalLeavFactor")
                            clsprojectsetting.ExternalLeavFactor = row("ExternalLeavFactor")
                            clsprojectsetting.InternalPermitDelayFactor = row("InternalPermitDelayFactor")
                            clsprojectsetting.ExternalPermitDelayFactor = row("ExternalPermitDelayFactor")
                            clsprojectsetting.InternalDelayPunishFactor = row("InternalDelayPunishFactor")
                            clsprojectsetting.ExternalDelayPunishFactor = row("ExternalDelayPunishFactor")
                            clsprojectsetting.Save()
                        Next
                    End If

                    ClsProjects.Find("Code='" & txtCode.Text & "'")
                    GetValues()
                    clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                    clsMainOtherFields.CollectDataAndSave(value.Text, ClsProjects.Table, ClsProjects.ID)
                    value.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                Else
                    ClsProjects.Find("Code='" & txtCode.Text & "'")
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsProjects.ID > 0 Then
                        ClsProjects.Update("code='" & txtCode.Text & "'")
                    Else
                        ClsProjects.Save()
                        ClsProjects.Find("Code='" & txtCode.Text & "'")

                        Dim projectchange As New Clshrs_ProjectChanges(Me)
                        projectchange.FromDate = ClsProjects.StartDate
                        projectchange.Remarks = "إنشاء المشروع"
                        projectchange.RegComputerID = 1
                        projectchange.ProjectID = ClsProjects.ID
                        projectchange.Save()

                    End If
                    ClsProjects.Find("Code='" & txtCode.Text & "'")
                    GetValues()
                    clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                    clsMainOtherFields.CollectDataAndSave(value.Text, ClsProjects.Table, ClsProjects.ID)
                    value.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                End If
                IsCopy.Value = ""
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsProjects.Find("Code='" & txtCode.Text & "'")
                If ClsProjects.IsLocked <> True Then
                    ClsProjects.Delete("Code='" & txtCode.Text & "'")
                    AfterOperation()
                End If
            Case "Property"
                ClsProjects.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsProjects.ID & "&TableName=" & ClsProjects.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsProjects.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsProjects.ID & "&TableName=" & ClsProjects.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsProjects.Table
                ClsProjects.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsProjects.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsProjects.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsProjects.Find(" Code= '" & txtCode.Text & "'")
                If ClsProjects.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsProjects.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsProjects.CheckDiff(ClsProjects, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsProjects.FirstRecord()
                GetValues()
            Case "Previous"
                ClsProjects.Find("Code='" & txtCode.Text & "'")
                If Not ClsProjects.previousRecord() Then
                    ClsProjects.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsProjects.Find("Code='" & txtCode.Text & "'")
                If Not ClsProjects.NextRecord() Then
                    ClsProjects.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsProjects.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function AssignValues() As Boolean
        Try
            With ClsProjects
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .ArbName4S = txtArbName.Text
                .Phone = txtPhone.Text
                .Mobile = txtMobile.Text
                .Fax = txtFax.Text
                .Email = txtMail.Text
                .Adress = txtAdress.Text
                .ContactPerson = txtContactPerson.Text
                .ProjectPeriod = txtProjectPeriod.Text
                .ClaimDuration = txtClaimDuration.Text
                .StartDate = .SetHigriDate(txtStartDate.Text)
                .EndDate = .SetHigriDate(txtEndDate.Text)
                .CreditLimit = txtCreditLimit.Text
                .CreditPeriod = txtCreditPeriod.Text
                .IsAdvance = CheckBox_IsAdvance.Checked
                .IsHijri = CheckBox_IsHijri.Checked
                .NotifyPeriod = txtNotifyPeriod.Text
                .CompanyConditions = txtCompanyConditions.Text
                .ClientConditions = txtClientConditions.Text
                .WorkConditions = TextBox_WorkConditions.Text
                .IsLocked = CheckBox_IsLock.Checked
                .BranchID = ddlBranch.SelectedValue
                .LocationID = DropDownList_Location.SelectedValue
                .SickTransaction = IIf(DropDownList_Sick.SelectedValue = 0, 254, DropDownList_Sick.SelectedValue)
                .LateTransaction = IIf(DropDownList_Late.SelectedValue = 0, 255, DropDownList_Late.SelectedValue)
                .LeaveTransaction = IIf(DropDownList_Leave.SelectedValue = 0, 257, DropDownList_Leave.SelectedValue)
                .AbsentTransaction = IIf(DropDownList_Absent.SelectedValue = 0, 256, DropDownList_Absent.SelectedValue)
                .OTTransaction = IIf(DropDownList_OT.SelectedValue = 0, 243, DropDownList_OT.SelectedValue)
                .HOTTransaction = IIf(DropDownList_HOT.SelectedValue = 0, 244, DropDownList_HOT.SelectedValue)
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            txtCode.Text = ClsProjects.Code
            txtEngName.Text = ClsProjects.EngName
            txtArbName.Text = ClsProjects.ArbName
            txtPhone.Text = ClsProjects.Phone
            txtMobile.Text = ClsProjects.Mobile
            txtFax.Text = ClsProjects.Fax
            txtMail.Text = ClsProjects.Email
            txtAdress.Text = ClsProjects.Adress
            txtContactPerson.Text = ClsProjects.ContactPerson
            txtProjectPeriod.Text = ClsProjects.ProjectPeriod
            txtClaimDuration.Text = ClsProjects.ClaimDuration
            txtStartDate.Text = ClsProjects.StartDate
            txtEndDate.Text = ClsProjects.EndDate
            txtCreditLimit.Text = ClsProjects.CreditLimit
            txtCreditPeriod.Text = ClsProjects.CreditPeriod
            CheckBox_IsAdvance.Checked = ClsProjects.IsAdvance
            CheckBox_IsHijri.Checked = ClsProjects.IsHijri
            txtNotifyPeriod.Text = ClsProjects.NotifyPeriod
            txtCompanyConditions.Text = ClsProjects.CompanyConditions
            txtClientConditions.Text = ClsProjects.ClientConditions
            CheckBox_IsLock.Checked = ClsProjects.IsLocked
            ddlBranch.SelectedValue = ClsProjects.BranchID
            DropDownList_Location.SelectedValue = ClsProjects.LocationID
            Try
                DropDownList_Absent.SelectedValue = ClsProjects.AbsentTransaction
                DropDownList_Leave.SelectedValue = ClsProjects.LeaveTransaction
                DropDownList_Sick.SelectedValue = ClsProjects.SickTransaction
                DropDownList_Late.SelectedValue = ClsProjects.LateTransaction
                DropDownList_OT.SelectedValue = ClsProjects.OTTransaction
                DropDownList_HOT.SelectedValue = ClsProjects.HOTTransaction
            Catch ex As Exception
            End Try
            TextBox_WorkConditions.Text = ClsProjects.WorkConditions
            If Not ClsProjects.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsProjects.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsProjects.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsProjects.RegDate).Date
            End If
            If ClsProjects.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsProjects.CancelDate).Date
            End If
            If Not ClsProjects.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()

            If (ClsProjects.ID > 0) Then
                StrMode = "E"
                If ClsProjects.IsLocked <> True Then
                    LinkButton_Wizard.Enabled = True
                    Dim UrlString1 = "'frmProjecWizard.aspx?ProjID=" & ClsProjects.ID & "',450,900,true,''"
                    LinkButton_Wizard.Attributes.Add("onclick", "OpenModal1(" & UrlString1 & "); return false;")
                Else
                    LinkButton_Wizard.Enabled = False
                    LinkButton_Wizard.Attributes.Remove("onclick")
                End If
            Else
                StrMode = "N"
                LinkButton_Wizard.Enabled = False
                LinkButton_Wizard.Attributes.Remove("onclick")
            End If
            SetToolBarPermission(Me, ClsProjects.ConnectionString, ClsProjects.DataBaseUserRelatedID, ClsProjects.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsProjects.ConnectionString, ClsProjects.DataBaseUserRelatedID, ClsProjects.GroupID, ClsProjects.Table, ClsProjects.ID)
            If Not ClsProjects.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsProjects.ID)
            End If
            IsCopy.Value = ""
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
                    ClsProjects.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsProjects.Find("ID=" & intID)
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
            With ClsProjects
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsProjects = New Clshrs_Projects(Me, "hrs_Projects")
        If IntId > 0 Then
            ClsProjects.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsProjects = New Clshrs_Projects(Me, "hrs_Projects")
        Dim ClsCountries As New Clssys_Countries(Page)
        Try
            If IsCopy.Value = "" Then
                ClsProjects.Find("Code='" & txtCode.Text & "'")
                IntId = ClsProjects.ID
                txtEngName.Focus()
                If ClsProjects.ID > 0 Then
                    GetValues()
                    StrMode = "E"
                Else
                    If ClsProjects.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                        txtCode.Text = ""
                        txtCode.Focus()
                    End If
                    Clear()
                    ImageButton_Delete.Enabled = False
                    StrMode = "N"
                    CreateOtherFields(0)
                End If
                SetToolBarDefaults()
                SetToolBarPermission(Me, ClsProjects.ConnectionString, ClsProjects.DataBaseUserRelatedID, ClsProjects.GroupID, StrMode)
                SetToolBarRecordPermission(Me, ClsProjects.ConnectionString, ClsProjects.DataBaseUserRelatedID, ClsProjects.GroupID, ClsProjects.Table, IntId)
                If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                    ImageButton_Delete.Enabled = False
                End If
            Else
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
                SetToolBarDefaults()
                SetToolBarPermission(Me, ClsProjects.ConnectionString, ClsProjects.DataBaseUserRelatedID, ClsProjects.GroupID, StrMode)
                SetToolBarRecordPermission(Me, ClsProjects.ConnectionString, ClsProjects.DataBaseUserRelatedID, ClsProjects.GroupID, ClsProjects.Table, IntId)
                If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                    ImageButton_Delete.Enabled = False
                End If
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
        ClsProjects.Clear()
        GetValues()
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = ""
        txtArbName.Text = ""
        txtPhone.Text = ""
        txtMobile.Text = ""
        txtFax.Text = ""
        txtMail.Text = ""
        txtAdress.Text = ""
        txtContactPerson.Text = ""
        txtProjectPeriod.Text = ""
        txtClaimDuration.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""
        txtCreditLimit.Text = ""
        txtCreditPeriod.Text = ""
        CheckBox_IsAdvance.Checked = False
        CheckBox_IsHijri.Checked = False
        txtNotifyPeriod.Text = ""
        txtCompanyConditions.Text = ""
        txtClientConditions.Text = ""
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        CheckBox_IsLock.Checked = False
        ddlBranch.SelectedValue = 0
        LinkButton_Wizard.Enabled = False
        DropDownList_Location.SelectedValue = 0
        DropDownList_Absent.SelectedValue = 0
        DropDownList_Leave.SelectedValue = 0
        DropDownList_Sick.SelectedValue = 0
        DropDownList_Late.SelectedValue = 0
        DropDownList_OT.SelectedValue = 0
        DropDownList_HOT.SelectedValue = 0
        TextBox_WorkConditions.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsProjects = New Clshrs_Projects(Page, "hrs_Projects")
        ClsProjects.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsProjects.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsProjects.Table) = True Then
            Dim StrTablename As String
            ClsProjects = New Clshrs_Projects(Me, "hrs_Projects")
            StrTablename = ClsProjects.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID & _
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID & _
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmProjectsExtra")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmProjectsExtra")
            End If
        End If
    End Function

#End Region

    Protected Sub LinkButton_Lock_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Lock.Click
        ClsProjects = New Clshrs_Projects(Me, "hrs_Projects")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsProjects.ConnectionString)
        If ClsProjects.Find("Code='" & txtCode.Text & "'") Then
            Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
            If hrsProjectPlacements.Find("ProjectID = " & ClsProjects.ID) Then
                If Not hrsProjectPlacements.Find("RegComputerID is null and ID not in (select PlacementID from hrs_ProjectPlacementPlanning) and ProjectID = " & ClsProjects.ID) Then
                    ClsProjects.IsLocked = True
                    ClsProjects.Update("Code='" & txtCode.Text & "'")
                    CheckBox_IsLock.Checked = True
                End If
            End If
        End If
    End Sub
    Protected Sub LinkButton_Copy_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Copy.Click
        If ClsProjects.Find("Code='" & txtCode.Text & "'") Then
            CheckBox_IsLock.Checked = False
            IsCopy.Value = "1"
            OldCode.Value = txtCode.Text
            txtCode.Text = ""
            txtCode.Focus()
        End If
    End Sub
End Class
