
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class Interfaces_frmExitReEntryVisa
    Inherits System.Web.UI.Page

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields
    Const CSave As Integer = 1
#End Region

#Region "Protected Sub"

    Protected Overrides Sub InitializeCulture()
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage
        'Use this
        If (_culture <> "Auto") Then

            Dim ci As New System.Globalization.CultureInfo(_culture)
            Dim StrDateFormat As String = System.Configuration.ConfigurationManager.AppSettings("DATEFORMAT")
            Dim myDateTimePatterns() As String = {StrDateFormat, StrDateFormat}
            Dim DateTimeFormat As New System.Globalization.DateTimeFormatInfo
            DateTimeFormat = ci.DateTimeFormat
            DateTimeFormat.SetAllDateTimePatterns(myDateTimePatterns, "d"c)
            System.Threading.Thread.CurrentThread.CurrentCulture = ci
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci

        End If
        MyBase.InitializeCulture()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsERVisa As New Clshrs_ExitReEntryVisa(Page)
        Dim ClsEmployee As New Clshrs_Employees(Page)

        Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
       
        Dim ClsCities As New Clssys_Cities(Page)
        Dim csSearchID As Integer = 0
        Dim ClsRelation As New Clshrs_DependantsTypes(Page)
        Try
            ClsERVisa.AddNotificationOnChange(Page)

            Dim ClsObjects As New Clssys_Objects(Me.Page)
            Dim ClsSearchs As New Clssys_Searchs(Me.Page)
            ClsEmployee = New Clshrs_Employees(Me.Page)
            If ClsObjects.Find(" Code='" & ClsEmployee.Table.Trim() & "'") Then
                If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                    csSearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmpCode.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtEmpCode.ClientID & "'"
                    btnSearchEmpCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            If Not IsPostBack Then
                ClsCities.GetDropDownList(ddlIssueCountry, True)
                ClsRelation.GetList(uwgDependents.Columns.FromKey("RelationID").ValueList)
                CreateEmptyRows(10)

                rbtnResidentIssue.Attributes.Add("onClick", "VisaTypeChecked('rbtnResidentIssue')")
                chkNew.Attributes.Add("onClick", "VisaTypeChecked('chkNew')")
                chkLost.Attributes.Add("onClick", "VisaTypeChecked('chkLost')")
                chkReNew.Attributes.Add("onClick", "VisaTypeChecked('chkReNew')")
                chkDamaged.Attributes.Add("onClick", "VisaTypeChecked('chkDamaged')")

                rbtnTravelVisa.Attributes.Add("onClick", "VisaTypeChecked('rbtnTravelVisa')")
                chkTravelOnce.Attributes.Add("onClick", "VisaTypeChecked('chkTravelOnce')")
                chkTravelMore.Attributes.Add("onClick", "VisaTypeChecked('chkTravelMore')")
                chkFinalExit.Attributes.Add("onClick", "VisaTypeChecked('chkFinalExit')")

                rbtnTransferBail.Attributes.Add("onClick", "VisaTypeChecked('rbtnTransferBail')")
                chkFirstTime.Attributes.Add("onClick", "VisaTypeChecked('chkFirstTime')")
                chkSecondTime.Attributes.Add("onClick", "VisaTypeChecked('chkSecondTime')")
                chkThirdTime.Attributes.Add("onClick", "VisaTypeChecked('chkThirdTime')")

                rbtnAddDependant.Attributes.Add("onClick", "VisaTypeChecked('rbtnAddDependant')")

                chkResidentGovernment.Attributes.Add("onClick", "chkResident('chkResidentGovernment')")
                chkResidentInstitutions.Attributes.Add("onClick", "chkResident('chkResidentInstitutions')")
                chkResidentCompanies.Attributes.Add("onClick", "chkResident('chkResidentCompanies')")
                chkResidentPersons.Attributes.Add("onClick", "chkResident('chkResidentPersons')")

                chkTransferBailGovernment.Attributes.Add("onClick", "chkTransferBail('chkTransferBailGovernment')")
                chkTransferBailInstitutions.Attributes.Add("onClick", "chkTransferBail('chkTransferBailInstitutions')")
                chkTransferBailCompanies.Attributes.Add("onClick", "chkTransferBail('chkTransferBailCompanies')")
                chkTransferBailPersons.Attributes.Add("onClick", "chkTransferBail('chkTransferBailPersons')")

                ' ClsERVisa.AddOnChangeEventToControls("frmExitReEntryVisa", Page, UltraWebTab1, "Interfaces_frmExitReEntryVisa")
                'ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "ViceSelect", "<Script Language=""javascript"" type=""text/javascript"">VisaTypeChecked('rbtnTravelVisa')</script>")


            End If
            txtPassportNo.ReadOnly = False
            CreateOtherFields(0)
            Ajax.Utility.RegisterTypeForAjax(GetType(Interfaces_frmExitReEntryVisa))
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler
            Me.Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsERVisa.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Me.Page.Response.Redirect("ErrorPage.aspx")
        End Try

    End Sub

    Protected Sub TlbMainToolbar_ButtonClicked(ByVal sender As Object, ByVal be As Infragistics.WebUI.UltraWebToolbar.ButtonEvent) Handles TlbMainToolbar.ButtonClicked
        Dim ClsERVisa As New Clshrs_ExitReEntryVisa(Page)
        Dim ObjNavigation As New Venus.Shared.Web.NavigationHandler(ClsERVisa.ConnectionString)
        Try
            Select Case be.Button.Key
                Case "Save"
                    SavePart()
                    'ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Print", "<Script language=""javascript""> PrintReport('ExitReEntryVisa','ExitReEntryVisa','PARAMETERS ID int 0;hrs_rptExitReEntryVisa(" & 3 & ")') </Script>")
            End Select
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler
            Me.Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsERVisa.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Me.Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Protected Sub txtEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmpCode.TextChanged
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim clsObject As New Clssys_Objects(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim ClsERVisa As New Clshrs_ExitReEntryVisa(Page)
        Clear()
        If txtEmpCode.Text.Trim <> String.Empty Then
            clsObject.Find("code='" & ClsEmployee.Table.Trim & "'")
            If ClsEmployee.Find("Code='" & txtEmpCode.Text.Trim & "'") Then
                With ClsEmployee
                    hdnEmpID.Value = .ID
                    txtName.Text = .FullName
                    txtBirthDate.Text = .GetHigriDate(.BirthDate)
                    txtBorderEntNo.Text = .EntryNo
                    ClsERVisa.GetEmployeeData(.ID, txtNationality.Text, txtProfession.Text, txtReligion.Text, txtPassportNo.Text, wdcPassportIssueDate.Value, wdcPassportExpireDate.Value, ddlIssueCountry, txtResidentNo.Text, wdcResidentExpireDate.Value)
                    DisplayImage(.ID, clsObject.ID, Image1)
                End With
            End If
        End If
    End Sub

#End Region

#Region "Private Function"

    Private Function CreateEmptyRows(ByVal NoofEmptyRows As Integer) As Boolean
        Dim IntCount As Integer
        For IntCount = 0 To NoofEmptyRows - 1
            Dim nR As New Infragistics.WebUI.UltraWebGrid.UltraGridRow()
            uwgDependents.Rows.Add(nR)
            nR.Cells.FromKey("No").Value = IntCount + 1
        Next
    End Function

    Private Function CreateOtherFields(ByVal IntRecordID As Integer) As Boolean
        Dim ClsFollowers As New Clshrs_Followers(Page)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        Dim StrTablename As String
        Dim objDS As New Data.DataSet
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsFollowers.Table) = True Then
            StrTablename = ClsFollowers.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID & _
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID & _
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmExitReEntryVisa")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmExitReEntryVisa")
            End If
        End If
    End Function

    Private Function AssignValue(ByRef ClsERVisa As Clshrs_ExitReEntryVisa, ByRef errorMsg As String) As Boolean
        Dim retVal As Boolean = True
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsERVisa.ConnectionString)
        With ClsERVisa

            If rbtnResidentIssue.Checked Then
                .VisaType = 1
            ElseIf rbtnTravelVisa.Checked Then
                .VisaType = 2
            ElseIf rbtnTransferBail.Checked Then
                .VisaType = 3
            ElseIf rbtnAddDependant.Checked Then
                .VisaType = 4
            End If

            If chkNew.Checked Then
                .ResidentIssueType = 1
            ElseIf chkLost.Checked Then
                .ResidentIssueType = 2
            ElseIf chkReNew.Checked Then
                .ResidentIssueType = 3
            ElseIf chkDamaged.Checked Then
                .ResidentIssueType = 4
            End If
            .ResidentIssueMonthsPeriod = wneMonthPeriod.Value

            If chkTravelOnce.Checked Then
                .TravelVisaType = 1
            ElseIf chkTravelMore.Checked Then
                .TravelVisaType = 2
            ElseIf chkFinalExit.Checked Then
                .TravelVisaType = 3
            End If
            .TravelVisaDaysPeriod = wneDays.Value

            If chkFirstTime.Checked Then
                .TransferBailType = 1
            ElseIf chkSecondTime.Checked Then
                .TransferBailType = 2
            ElseIf chkThirdTime.Checked Then
                .TransferBailType = 3
            End If
            .TransferBailOther = txtOther.Text

            If hdnEmpID.Value > 0 Then
                .EmployeeID = hdnEmpID.Value
            Else
                retVal = False
                errorMsg = objNav.SetLanguage(Page, "Choose Employee/ÇÎÊÑ ãæÙÝ") & " "
            End If
            If txtPassportNo.Text.Trim = String.Empty Then
                retVal = False
                errorMsg &= objNav.SetLanguage(Page, "Enter Passport No/ÇÏÎá ÑÞã ÇáÌæÇÒ ") & " "
            Else
                .PassportNo = txtPassportNo.Text
            End If

            .PassportIssueDate = .SetHigriDate2(wdcPassportIssueDate.Text, .PassportIssueDate_D)
            .PassportExpireDate = .SetHigriDate2(wdcPassportExpireDate.Text, .PassportExpireDate_D)
            .IssueCountryID = ddlIssueCountry.SelectedValue
            .ResidentNo = txtResidentNo.Text
            .ResidentExpireDate = .SetHigriDate2(wdcResidentExpireDate.Text, .ResidentExpireDate_D)
            .ResidentSponsorNo = txtResidentSponsorNo.Text

            If chkResidentGovernment.Checked Then
                .ResidentSponsorOrganizationType = 1
            ElseIf chkResidentInstitutions.Checked Then
                .ResidentSponsorOrganizationType = 2
            ElseIf chkResidentCompanies.Checked Then
                .ResidentSponsorOrganizationType = 3
            ElseIf chkResidentPersons.Checked Then
                .ResidentSponsorOrganizationType = 4
            End If

            .SponsorName = txtResidentSponsorName.Text
            .SponsorAddress = txtResidentSponsorAddress.Text
            .SponsorTel = txtResidentSponsorTel.Text
            .BorderEntNo = txtBorderEntNo.Text
            .EntDate = .SetHigriDate2(wdcEntDate.Text, .EntDate_D)
            .EntOutlet = txtEntOutlet.Text

            If chkTransferBailGovernment.Checked Then
                .TransferBailOrganizationType = 1
            ElseIf chkTransferBailInstitutions.Checked Then
                .TransferBailOrganizationType = 2
            ElseIf chkTransferBailCompanies.Checked Then
                .TransferBailOrganizationType = 3
            ElseIf chkTransferBailPersons.Checked Then
                .TransferBailOrganizationType = 4
            End If

            .TransferBailNewSponsorName = txtTransferBailNewSponsorName.Text
            .TransferBailNewSponsorNo = txtTransferBailNewSponsorNo.Text
            .TransferBailNewSponsorNo2 = txtTransferBailNewSponsorNo2.Text
            .TransferBailNewSponsorAddress = txtTransferBailSponsorAddress.Text
            .TransferorSponsorName = txtTransferorSponsor.Text
            .TransferorSponsorNo = txTransferorSponsorNo.Text
        End With
        Return retVal
    End Function

    Private Function SavePart() As Boolean
        Dim ClsERVisa As New Clshrs_ExitReEntryVisa(Page)
        Dim errorMsg As String = String.Empty
        Dim intID As Integer
        If Not AssignValue(ClsERVisa, errorMsg) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, errorMsg)
            Exit Function
        Else
            intID = ClsERVisa.Save()
            If intID > 0 Then
                ClsERVisa.SaveDependant(intID, uwgDependents)
            End If
            ' ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Print", "<Script language=""javascript""> PrintReport('ExitReEntryVisa','ExitReEntryVisa','PARAMETERS ID int 0;hrs_rptExitReEntryVisa(" & intID & ")') </Script>")
            ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">OpenPrintedScreen(" & intID & ");</script>")
            AfterOperation()
            'ToDo Print
        End If
    End Function

    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim ClsDocFollow As New Clshrs_DocumentsFollowup(Page)
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsDocFollow
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageToolTips(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    Venus.Shared.Web.ClientSideActions.SetToolBarPermission(Me, .ConnectionString, TlbMainToolbar, .DataBaseUserRelatedID, .GroupID, StrMode)
                End If
            End With
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler
            Me.Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsDocFollow.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Me.Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function SetToolBarDefaults() As Boolean
        TlbMainToolbar.Items.FromKey("Save").Enabled = True
    End Function

    Private Function AfterOperation() As Boolean
        txtEmpCode.Text = String.Empty
        Clear()
        UltraWebTab1.SelectedTab = 0
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtEmpCode, True)
    End Function

    Private Function Clear() As Boolean
        Image1.ImageUrl = String.Empty
        hdnEmpID.Value = 0

        'txtEmpCode.Text = String.Empty
        txtName.Text = String.Empty
        txtNationality.Text = String.Empty
        txtProfession.Text = String.Empty
        txtReligion.Text = String.Empty
        txtBirthDate.Text = String.Empty

        wneMonthPeriod.Text = String.Empty
        wneDays.Text = String.Empty
        txtOther.Text = String.Empty
        txtPassportNo.Text = String.Empty
        wdcPassportIssueDate.Text = String.Empty
        wdcPassportExpireDate.Text = String.Empty
        ddlIssueCountry.SelectedValue = 0
        txtResidentNo.Text = String.Empty
        wdcResidentExpireDate.Text = String.Empty
        txtResidentSponsorNo.Text = String.Empty

        chkResidentGovernment.Checked = False
        chkResidentInstitutions.Checked = False
        chkResidentCompanies.Checked = False
        chkResidentPersons.Checked = False


        txtResidentSponsorName.Text = String.Empty
        txtResidentSponsorAddress.Text = String.Empty
        txtResidentSponsorTel.Text = String.Empty
        txtBorderEntNo.Text = String.Empty
        wdcEntDate.Text = String.Empty
        txtEntOutlet.Text = String.Empty

        chkTransferBailGovernment.Checked = False
        chkTransferBailInstitutions.Checked = False
        chkTransferBailCompanies.Checked = False
        chkTransferBailPersons.Checked = False

        txtTransferBailNewSponsorName.Text = String.Empty
        txtTransferBailNewSponsorNo.Text = String.Empty
        txtTransferBailNewSponsorNo2.Text = String.Empty
        txtTransferBailSponsorAddress.Text = String.Empty
        txtTransferorSponsor.Text = String.Empty
        txTransferorSponsorNo.Text = String.Empty

        uwgDependents.DataSource = Nothing
        uwgDependents.DataBind()

        CreateEmptyRows(10)

        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Clear", "VisaTypeChecked('rbtnTravelVisa')")
    End Function

    Private Function DisplayImage(ByVal IntRecordId As Integer, ByVal IntObjectId As Integer, ByRef ImgImageControl As System.Web.UI.WebControls.Image) As String
        Dim ClsDocumentAttachment = New Clssys_DocumentsInformationAttachments(Me.Page)
        If ClsDocumentAttachment.Find(" RecordID=" & IntRecordId & " And ObjectID=" & IntObjectId & " And IsImageView is not Null And IsNull(CancelDate , '') = '' Order By IsProfilePicture Desc ") Then
            If ClsDocumentAttachment.ExpiryDate > Date.Now Or ClsDocumentAttachment.ExpiryDate = Nothing Then
                ImgImageControl.ImageUrl = "../Photos/" & IntObjectId.ToString & "_" & IntRecordId.ToString & "/" + ClsDocumentAttachment.FileName
            Else
                ImgImageControl.ImageUrl = "./NoImage.jpg"
            End If
        Else
            ImgImageControl.ImageUrl = "./NoImage.jpg"
        End If

    End Function

#End Region

#Region "Ajax Methods"
    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)> _
   Public Function CheckDate(ByVal strDate As String) As String
        Dim clsDAL As New ClsDataAcessLayer(Page)
        Return clsDAL.CheckMyDate(strDate)
    End Function
#End Region

  
End Class
