Imports System.Data
Imports System.Configuration
Imports System.Collections.Specialized
Imports System.Globalization
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class frmRecBioGraphy
    Inherits MainPage
#Region "Public Decleration"
    Private ClsBioGraphies As ClsRec_BioGraphies
    Private ClsBioGraphiesDetail1 As ClsRec_BioGraphiesDetail1
    Private ClsBioGraphiesDetail2 As ClsRec_BioGraphiesDetail2
    Private ClsBioGraphiesDetail3 As ClsRec_BioGraphiesDetail3
    Private ClsBioGraphiesDetail4 As ClsRec_BioGraphiesDetail4
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsPositions As Clshrs_Positions

    Const CsPaidatVacation = 4
    Const CsOnceatPeriod = 5
    Const CsMaxValue = 3
    Const CsMinValue = 2
    Const CsIntervalID = 6
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        If IntId = 0 Then ImageButton_Delete.Enabled = False
        Dim ClsNationality = New Clssys_Nationality(Page)
        Dim ClsReligions = New Clshrs_Religions(Page, "hrs_Religions")
        Dim ClsPositions = New Clshrs_Positions(Page)
        Dim ClsLanguages = New Clssys_Languages(Page)
        Dim ClsEducationDegree = New ClsRec_EducationDegree(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsLanguages.ConnectionString)
        Dim clsIntervals As New Clshrs_Intervals(Page)
        ClsBioGraphies = New ClsRec_BioGraphies(Page)
        Dim SearchID As Integer = 0

        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsBioGraphies.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsBioGraphies.ConnectionString)
                ClsBioGraphies.AddOnChangeEventToControls("frmRecBioGraphy", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Setsetting(0)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbFatherName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbFamilyName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbGrandName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                ClsNationality.GetDropDownList(ddlNationality, True, "")
                ClsPositions.GetDropDownList(ddlPosition, True, "")
                ClsReligions.GetDropDownList(ddlRelegion, True, "")
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                ClsPositions.GetList(uwgHistory.DisplayLayout.Bands(0).Columns(8).ValueList)
                ClsLanguages.GetList(uwgLanguage.DisplayLayout.Bands(0).Columns(1).ValueList, False)
                ClsEducationDegree.GetList(uwgCertifications.DisplayLayout.Bands(0).Columns(1).ValueList, False)

                With ObjNavigationHandler
                    AddValueListGrid(uwgLanguage, "SLevel_ID", .SetLanguage(Page, "BAD/ضعيف"), .SetLanguage(Page, "GOOD/جيد"), .SetLanguage(Page, "EXCELLENT/ممتاز"))
                    AddValueListGrid(uwgLanguage, "WLevel_ID", .SetLanguage(Page, "BAD/ضعيف"), .SetLanguage(Page, "GOOD/جيد"), .SetLanguage(Page, "EXCELLENT/ممتاز"))
                    txtLang.Value = .SetLanguage(Page, "Eng/Arb")
                End With

                AddRows(uwgReferences)
                AddRows(uwgLanguage)
                AddRows(uwgCertifications)
                AddRows(uwgHistory)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsBioGraphies.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsBioGraphies.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsBioGraphies.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsBioGraphies = New ClsRec_BioGraphies(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsBioGraphies.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text.Length = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                    Return
                ElseIf ddlRelegion.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Relegion /لابد من إدخال الديانة"))
                    Return
                ElseIf ddlNationality.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Nationality /لابد من إدخال الجنسية"))
                    Return
                ElseIf ddlPosition.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Suit Position /لابد من إدخال الوظيفة المناسبة"))
                    Return
                End If

                SavePart()
                AfterOperation()
            Case "Save"
                If txtCode.Text.Length = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                    Return
                ElseIf ddlRelegion.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Relegion /لابد من إدخال الديانة"))
                    Return
                ElseIf ddlNationality.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Nationality /لابد من إدخال الجنسية"))
                    Return
                ElseIf ddlPosition.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Suit Position /لابد من إدخال الوظيفة المناسبة"))
                    Return
                End If

                SavePart()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsBioGraphies.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsBioGraphies.ID & "&TableName=" & ClsBioGraphies.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsBioGraphies.ID & "&TableName=" & ClsBioGraphies.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsBioGraphies.Table
                ClsBioGraphies.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsBioGraphies.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsBioGraphies.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsBioGraphies.Find(" Code= '" & txtCode.Text & "'")
                If ClsBioGraphies.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsBioGraphies.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsBioGraphies.CheckDiff(ClsBioGraphies, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsBioGraphies.FirstRecord()
                GetValues()
            Case "Previous"
                ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
                If Not ClsBioGraphies.previousRecord() Then
                    ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
                If Not ClsBioGraphies.NextRecord() Then
                    ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsBioGraphies.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub txtHBirthDate_ValueChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtHBirthDate.ValueChange
        Dim HDate As String = txtHBirthDate.Value
        If ClsDataAcessLayer.IsHijri(HDate) = False Then
            HDate = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            txtHBirthDate.Value = HDate
        End If
        txtGBirthDate.Value = ClsDataAcessLayer.HijriToGreg(HDate, "dd/MM/yyyy")
    End Sub
    Protected Sub txtGBirthDate_ValueChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ValueChangeEventArgs) Handles txtGBirthDate.ValueChange
        Dim GDate As String = txtGBirthDate.Value
        If ClsDataAcessLayer.IsGreg(GDate) = False Then
            GDate = ClsDataAcessLayer.FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            txtGBirthDate.Value = GDate
        End If
        txtHBirthDate.Value = ClsDataAcessLayer.GregToHijri(GDate, "dd/MM/yyyy")
    End Sub
    Protected Sub HasSConditions_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles HasSConditions.CheckedChanged
        If HasSConditions.Checked = True Then
            txtSConditions.Enabled = True
            txtSConditions.Focus()
        Else
            txtSConditions.Enabled = False
            txtSConditions.Text = ""
        End If
    End Sub

#End Region

#Region "Private Functions"
    Private Sub AddRows(ByVal gridName As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, Optional ByVal Count As Integer = 2)
        For Index As Integer = 0 To Count - 1
            gridName.Rows.Add()
        Next
    End Sub
    Private Sub AddValueListGrid(ByVal GridName As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal Columns As Object, ByVal ParamArray ValueText() As String)
        If IsNumeric(Columns) Then
            GridName.DisplayLayout.Bands(0).Columns(Columns).ValueList.ValueListItems.Clear()
        Else
            GridName.DisplayLayout.Bands(0).Columns.FromKey(Columns).ValueList.ValueListItems.Clear()
        End If

        For I As Integer = 0 To ValueText.Length - 1
            If IsNumeric(Columns) Then
                GridName.DisplayLayout.Bands(0).Columns(Columns).ValueList.ValueListItems.Add(I, ValueText(I))
            Else
                GridName.DisplayLayout.Bands(0).Columns.FromKey(Columns).ValueList.ValueListItems.Add(I, ValueText(I))
            End If
        Next
    End Sub
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsBioGraphies = New ClsRec_BioGraphies(Page)
        Try
            ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
            If ClsBioGraphies.ID > 0 Then
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsBioGraphies.Update("Code='" & txtCode.Text & "'")
                Dim str As String = "delete from Rec_BioGraphiesDetail1 where BioGraphy_ID = " & ClsBioGraphies.ID & " ;" & _
                                    "delete from Rec_BioGraphiesDetail2 where BioGraphy_ID = " & ClsBioGraphies.ID & " ;" & _
                                    "delete from Rec_BioGraphiesDetail3 where BioGraphy_ID = " & ClsBioGraphies.ID & " ;" & _
                                    "delete from Rec_BioGraphiesDetail4 where BioGraphy_ID = " & ClsBioGraphies.ID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsBioGraphies.ConnectionString, CommandType.Text, str)
                If (SaveDG(ClsBioGraphies.ID)) Then
                    ClsBioGraphies = New ClsRec_BioGraphies(Page)
                    clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                    ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
                    clsMainOtherFields.CollectDataAndSave(value.Text, ClsBioGraphies.Table, ClsBioGraphies.ID)
                    value.Text = ""

                End If
            Else
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsBioGraphies.Save()
                ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
                If ClsBioGraphies.ID > 0 Then
                    If (SaveDG(ClsBioGraphies.ID)) Then
                        ClsBioGraphies = New ClsRec_BioGraphies(Page)
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsBioGraphies.Table, ClsBioGraphies.ID)
                        value.Text = ""

                    End If
                End If
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsBioGraphies.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SaveDG(ByVal BiographyID As Integer) As Boolean
        Dim DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Try
            For Each DGRow In uwgReferences.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If
                ClsBioGraphiesDetail1 = New ClsRec_BioGraphiesDetail1(Page)
                ClsBioGraphiesDetail1.BioGraphy_ID = BiographyID
                ClsBioGraphiesDetail1.EngName = DGRow.Cells(1).Value
                ClsBioGraphiesDetail1.ArbName = DGRow.Cells(2).Value
                ClsBioGraphiesDetail1.Phone = DGRow.Cells(3).Value
                ClsBioGraphiesDetail1.Fax = DGRow.Cells(4).Value
                ClsBioGraphiesDetail1.E_Mail = DGRow.Cells(5).Value
                ClsBioGraphiesDetail1.Save()
            Next

            For Each DGRow In uwgLanguage.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If
                ClsBioGraphiesDetail2 = New ClsRec_BioGraphiesDetail2(Page)
                ClsBioGraphiesDetail2.BioGraphy_ID = BiographyID
                ClsBioGraphiesDetail2.Language_ID = DGRow.Cells(1).Value
                ClsBioGraphiesDetail2.SLevel_ID = DGRow.Cells(2).Value
                ClsBioGraphiesDetail2.WLevel_ID = DGRow.Cells(3).Value
                ClsBioGraphiesDetail2.Save()
            Next

            For Each DGRow In uwgCertifications.Rows
                If IsNothing(DGRow.Cells(1).Value) Then
                    Continue For
                End If
                ClsBioGraphiesDetail3 = New ClsRec_BioGraphiesDetail3(Page)
                ClsBioGraphiesDetail3.BioGraphy_ID = BiographyID
                ClsBioGraphiesDetail3.EDegree_ID = DGRow.Cells(1).Value
                ClsBioGraphiesDetail3.GDateFrom = ClsDataAcessLayer.FormatGreg(DGRow.Cells(2).Value, "dd/MM/yyyy")
                ClsBioGraphiesDetail3.HDateFrom = DGRow.Cells(3).Value
                ClsBioGraphiesDetail3.GDateTo = ClsDataAcessLayer.FormatGreg(DGRow.Cells(4).Value, "dd/MM/yyyy")
                ClsBioGraphiesDetail3.HDateTo = DGRow.Cells(5).Value
                ClsBioGraphiesDetail3.Save()
            Next

            For Each DGRow In uwgHistory.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If
                ClsBioGraphiesDetail4 = New ClsRec_BioGraphiesDetail4(Page)
                ClsBioGraphiesDetail4.BioGraphy_ID = BiographyID
                ClsBioGraphiesDetail4.EngName = DGRow.Cells(1).Value
                ClsBioGraphiesDetail4.ArbName = DGRow.Cells(2).Value
                ClsBioGraphiesDetail4.GDateFrom = ClsDataAcessLayer.FormatGreg(DGRow.Cells(3).Value, "dd/MM/yyyy")
                ClsBioGraphiesDetail4.HDateFrom = DGRow.Cells(4).Value
                ClsBioGraphiesDetail4.GDateTo = ClsDataAcessLayer.FormatGreg(DGRow.Cells(5).Value, "dd/MM/yyyy")
                ClsBioGraphiesDetail4.HDateTo = DGRow.Cells(6).Value
                ClsBioGraphiesDetail4.Years = DGRow.Cells(7).Value
                ClsBioGraphiesDetail4.Position_ID = DGRow.Cells(8).Value
                ClsBioGraphiesDetail4.Save()
            Next

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function AssignValues() As Boolean
        Try
            With ClsBioGraphies
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .GDate = DateTime.Now
                .FamilyEngName = txtEngFamilyName.Text
                .FamilyArbName = txtArbFamilyName.Text
                .FatherEngName = txtEngFathername.Text
                .FatherArbName = txtArbFatherName.Text
                .GrandEngName = txtEngGrandName.Text
                .GrandArbName = txtArbGrandName.Text
                .E_Mail = txtEmail.Text
                If ddlPosition.SelectedValue <> 0 Then
                    .Position_ID = ddlPosition.SelectedValue
                End If
                If ddlRelegion.SelectedValue <> 0 Then
                    .ReligionID = ddlRelegion.SelectedValue
                End If
                .MaritalStatus = ddlMaritalStatus.SelectedValue
                .Sex = ddlGender.SelectedValue
                .NODependant = Convert.ToInt32(txtNODependancies.Value)
                .LastJob = txtLastJob.Text
                .LastSalary = Convert.ToDecimal(txtLastSalary.Value)
                .ExpectedSalary = Convert.ToDecimal(txtExpSalary.Value)
                .Address = txtAddress.Text
                .HasDLicense = HasDriverLic.Checked
                If ddlNationality.SelectedValue <> 0 Then
                    .Nationality_ID = ddlNationality.SelectedValue
                End If
                .IqamaNo = txtiqamano.Value
                .PassportNo = txtpassportno.Value
                .HasTIqama = HasTransIqama.Checked
                .NOFSponser = HasNOSponsor.Checked
                .HSpConditions = HasSConditions.Checked
                .SpecialConditions = txtSConditions.Text
                .GBirthDate = ClsDataAcessLayer.FormatGreg(txtGBirthDate.Value, "dd/MM/yyyy")
                .HBirthDate = txtHBirthDate.Value
                .Phone = txtPhone.Text
                .Mobile = txtMobile.Text
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsBioGraphiesDetail1 = New ClsRec_BioGraphiesDetail1(Page)
        ClsBioGraphiesDetail2 = New ClsRec_BioGraphiesDetail2(Page)
        ClsBioGraphiesDetail3 = New ClsRec_BioGraphiesDetail3(Page)
        ClsBioGraphiesDetail4 = New ClsRec_BioGraphiesDetail4(Page)
        Dim ClsOpenVacancy As New ClsRec_OpenVacancy(Page)
        ClsPositions = New Clshrs_Positions(Page)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsBioGraphies.ConnectionString)
        Try
            SetToolBarDefaults()

            With ClsBioGraphies
                ClsPositions.GetDropDownList(ddlPosition, True, "")
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtEngFamilyName.Text = .FamilyEngName
                txtArbFamilyName.Text = .FamilyArbName
                txtEngFathername.Text = .FatherEngName
                txtArbFatherName.Text = .FatherArbName
                txtEngGrandName.Text = .GrandEngName
                txtArbGrandName.Text = .GrandArbName
                txtEmail.Text = .E_Mail
                ddlPosition.SelectedValue = .Position_ID
                ddlPosition.Enabled = True
                If .Position_ID <> 0 Then
                    ClsOpenVacancy.Find("IsOpen = 1 and CancelDate is null and getdate() <= GEndDate and Position_ID = " & .Position_ID)
                    If ClsOpenVacancy.DataSet.Tables(0).Rows.Count > 0 Then
                        ddlPosition.Enabled = True
                    Else
                        ddlPosition.Enabled = False
                    End If
                End If
                ddlMaritalStatus.SelectedValue = .MaritalStatus
                ddlGender.SelectedValue = .Sex
                txtNODependancies.Value = .NODependant
                txtLastJob.Text = .LastJob
                txtLastSalary.Value = .LastSalary
                txtExpSalary.Value = .ExpectedSalary
                txtAddress.Text = .Address
                HasDriverLic.Checked = .HasDLicense
                ddlNationality.SelectedValue = .Nationality_ID
                ddlRelegion.SelectedValue = .ReligionID
                txtiqamano.Value = .IqamaNo
                txtpassportno.Value = .PassportNo
                HasTransIqama.Checked = .HasTIqama
                HasNOSponsor.Checked = .NOFSponser

                HasSConditions.Checked = .HSpConditions
                txtSConditions.Text = .SpecialConditions
                txtSConditions.Enabled = .HSpConditions

                txtGBirthDate.Value = .GBirthDate
                txtHBirthDate.Value = .HBirthDate

                txtPhone.Text = .Phone
                txtMobile.Text = .Mobile

                ClsBioGraphiesDetail1.Find("CancelDate is null and BioGraphy_ID = " & .ID)
                uwgReferences.DataSource = ClsBioGraphiesDetail1.DataSet.Tables(0).DefaultView
                uwgReferences.DataBind()

                ClsBioGraphiesDetail2.Find("CancelDate is null and BioGraphy_ID = " & .ID)
                uwgLanguage.DataSource = ClsBioGraphiesDetail2.DataSet.Tables(0).DefaultView
                uwgLanguage.DataBind()

                ClsBioGraphiesDetail3.Find("CancelDate is null and BioGraphy_ID = " & .ID)
                uwgCertifications.DataSource = ClsBioGraphiesDetail3.DataSet.Tables(0).DefaultView
                uwgCertifications.DataBind()

                ClsBioGraphiesDetail4.Find("CancelDate is null and BioGraphy_ID = " & .ID)
                uwgHistory.DataSource = ClsBioGraphiesDetail4.DataSet.Tables(0).DefaultView
                uwgHistory.DataBind()
            End With

            AddRows(uwgReferences)
            AddRows(uwgLanguage)
            AddRows(uwgCertifications)
            AddRows(uwgHistory)

            If Not ClsBioGraphies.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsBioGraphies.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsBioGraphies.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsBioGraphies.RegDate).Date
            End If
            If ClsBioGraphies.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsBioGraphies.CancelDate).Date
            End If
            If Not ClsBioGraphies.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()

            If (ClsBioGraphies.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsBioGraphies.ConnectionString, ClsBioGraphies.DataBaseUserRelatedID, ClsBioGraphies.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsBioGraphies.ConnectionString, ClsBioGraphies.DataBaseUserRelatedID, ClsBioGraphies.GroupID, ClsBioGraphies.Table, ClsBioGraphies.ID)
            If Not ClsBioGraphies.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsBioGraphies.ID)
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
                    ClsBioGraphies.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsBioGraphies.Find("ID=" & intID)
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
            With ClsBioGraphies
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
        ClsBioGraphies = New ClsRec_BioGraphies(Me)
        Try
            With ClsBioGraphies
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
        ClsBioGraphies = New ClsRec_BioGraphies(Me)
        If IntId > 0 Then
            ClsBioGraphies.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsBioGraphies = New ClsRec_BioGraphies(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsBioGraphies.ConnectionString)
        ddlPosition.Enabled = True
        txtSConditions.Enabled = True
        Try
            ClsBioGraphies.Find("Code='" & txtCode.Text & "'")
            IntId = ClsBioGraphies.ID
            txtEngName.Focus()
            If ClsBioGraphies.ID > 0 Then
                If ClsBioGraphies.IsUsed = True Then
                    Label1.Text = ObjNavigationHandler.SetLanguage(Page, "This Applicant Already Used/هذا المتقدم تم توظيفه")
                Else
                    Label1.Text = ""
                End If
                GetValues()
                StrMode = "E"
            Else
                If ClsBioGraphies.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsBioGraphies.ConnectionString, ClsBioGraphies.DataBaseUserRelatedID, ClsBioGraphies.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsBioGraphies.ConnectionString, ClsBioGraphies.DataBaseUserRelatedID, ClsBioGraphies.GroupID, ClsBioGraphies.Table, IntId)
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
        ClsBioGraphies.Clear()
        GetValues()
        ClsPositions = New Clshrs_Positions(Page)
        txtGBirthDate.Value = ClsDataAcessLayer.FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtHBirthDate.Value = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        ClsPositions.GetDropDownList(ddlPosition, True, "")
        UltraWebTab1.SelectedTab = 0
        UltraWebTab2.SelectedTab = 0
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        Label1.Text = ""
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        txtEngFamilyName.Text = String.Empty
        txtArbFamilyName.Text = String.Empty
        txtEngFathername.Text = String.Empty
        txtArbFatherName.Text = String.Empty
        txtEngGrandName.Text = String.Empty
        txtArbGrandName.Text = String.Empty
        txtEmail.Text = String.Empty
        ddlPosition.SelectedValue = 0
        ddlRelegion.SelectedValue = 0
        ddlMaritalStatus.SelectedValue = "S"
        ddlGender.SelectedValue = "M"
        txtNODependancies.Value = 0
        txtLastJob.Text = String.Empty
        txtLastSalary.Value = 0
        txtExpSalary.Value = 0
        txtAddress.Text = String.Empty
        HasDriverLic.Checked = False
        ddlNationality.SelectedValue = 0
        txtiqamano.Value = String.Empty
        txtpassportno.Value = String.Empty
        HasTransIqama.Checked = False
        HasNOSponsor.Checked = False
        HasSConditions.Checked = False
        txtSConditions.Text = String.Empty
        txtSConditions.Enabled = False
        txtGBirthDate.Value = ClsDataAcessLayer.FormatGreg(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtHBirthDate.Value = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        txtPhone.Text = String.Empty
        txtMobile.Text = String.Empty
        uwgReferences.Rows.Clear()
        uwgLanguage.Rows.Clear()
        uwgCertifications.Rows.Clear()
        uwgHistory.Rows.Clear()

        AddRows(uwgReferences)
        AddRows(uwgLanguage)
        AddRows(uwgCertifications)
        AddRows(uwgHistory)

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsBioGraphies = New ClsRec_BioGraphies(Page)
        ClsBioGraphies.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsBioGraphies.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsBioGraphies.Table) = True Then
            Dim StrTablename As String
            ClsBioGraphies = New ClsRec_BioGraphies(Me)
            StrTablename = ClsBioGraphies.Table
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

#Region "PageMethods"
    <System.Web.Services.WebMethod()> _
    Public Shared Function Greg2Hijri(ByVal DateValue As String) As Object
        Dim GDate As String = DateValue
        'If ClsDataAcessLayer.IsGreg(GDate) = False Then
        '    GDate = ClsDataAcessLayer.FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        'End If
        Return GDate & "|" & ClsDataAcessLayer.GregToHijri(GDate, "dd/MM/yyyy")
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function Hijri2Greg(ByVal DateValue As String) As Object
        Dim HDate As String = DateValue
        'If ClsDataAcessLayer.IsHijri(HDate) = False Then
        '    HDate = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        'End If
        Return ClsDataAcessLayer.HijriToGreg(HDate, "dd/MM/yyyy") & "|" & HDate

    End Function
#End Region


End Class
