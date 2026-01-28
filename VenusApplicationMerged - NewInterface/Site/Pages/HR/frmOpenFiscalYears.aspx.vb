Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Globalization

Partial Class frmOpenFiscalYears
    Inherits MainPage
#Region "Public Decleration"
    Private ClsFiscalYearsPeriods As Clssys_FiscalYearsPeriods
    Private ClsFiscalYears As Clssys_FiscalYears
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Me.Page)
        ClsFiscalYears = New Clssys_FiscalYears(Me.Page)
        Dim clsNavigation = New Venus.Shared.Web.NavigationHandler(ClsFiscalYears.ConnectionString)
        Try
            lblGabNotify.Text = ""
            Session("uwgFiscalYearsPeriods") = uwgFiscalYearsPeriods
            If Not IsPostBack Then
                Page.Session.Add("UserID", ClsFiscalYears.DataBaseUserRelatedID)
                Page.Session.Add("ConnectionString", ClsFiscalYears.ConnectionString)
                ClsFiscalYearsPeriods.AddOnChangeEventToControls("frmFiscalYearsPeriods", Page, UltraWebTab1)
                ClsFiscalYears.GetDropDown(ddlFiscalYear)
                ClsFiscalYears.GetDropDown(ddlGuidFisical)
                If ddlFiscalYear.Items.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, clsNavigation.SetLanguage(Page, "You must enter at least one fiscal year/يجب أن تدخل على الأقل سنة مالية"))
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    Exit Sub
                End If
                ddlFiscalYear.SelectedIndex = 0
                Load_DataGrid(ddlFiscalYear.SelectedValue)
                txtCode.Value = ""
                txtRowIndex.Value = "-1"
                txtRowID.Value = "-1"
                SetToolBarPermission(Page, ClsFiscalYears.ConnectionString, ClsFiscalYears.DataBaseUserRelatedID, ClsFiscalYears.GroupID, "")


                Dim Clscompanies As New Clssys_Companies(Page)
                Clscompanies.Find("ID = " & Clscompanies.MainCompanyID)
                If (Clscompanies.IsHigry = True) Then
                    uwgFiscalYearsPeriods.Columns(4).Hidden = True
                    uwgFiscalYearsPeriods.Columns(5).Hidden = True

                    uwgFiscalYearsPeriods.Columns(11).Hidden = False
                    uwgFiscalYearsPeriods.Columns(12).Hidden = False

                Else
                    uwgFiscalYearsPeriods.Columns(4).Hidden = False
                    uwgFiscalYearsPeriods.Columns(5).Hidden = False

                    uwgFiscalYearsPeriods.Columns(11).Hidden = True
                    uwgFiscalYearsPeriods.Columns(12).Hidden = True
                End If
            End If

            If ddlFiscalYear.SelectedValue > 0 Then
                SetScreenInformation("E")
            Else
                SetScreenInformation("N")
            End If

            uwgFiscalYearsPeriods.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect
            uwgFiscalYearsPeriods.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsFiscalYearsPeriods.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsFiscalYearsPeriods.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsFiscalYears = New Clssys_FiscalYears(Me.Page)
        ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsFiscalYears.ConnectionString)

        Select Case e.CommandArgument
            Case "SaveNew"
            Case "Save"
            Case "New"
                Dim FYPID As Integer
                If uwgFiscalYearsPeriods.Rows.Count > 0 Then
                    Dim rowFYP As Object = uwgFiscalYearsPeriods.Rows.GetItem(0)
                    FYPID = rowFYP.Cells.All(0).Text
                    Dim FId As Integer = rowFYP.Cells.All(1).Text
                    ImageButton_Delete.Enabled = False
                End If
                If (Val(txtRowIndex.Value) >= 0) Then
                    uwgFiscalYearsPeriods.Rows(Val(txtRowIndex.Value)).Selected = False
                End If
                uwgFiscalYearsPeriods.Rows(0).Selected = False
                txtRowIndex.Value = "-1"
                txtRowID.Value = "-1"
                Clear(ddlFiscalYear.SelectedValue)
                SetToolBarPermission(Me, ClsFiscalYearsPeriods.ConnectionString, ClsFiscalYearsPeriods.DataBaseUserRelatedID, ClsFiscalYearsPeriods.GroupID, "N")
                ImageButton_Delete.Enabled = False
            Case "Delete"
                If txtCode.Value.Trim <> "" Then
                    If CanDeleteFiscalPeriod(CInt(txtCode.Value)) Then
                        ClsFiscalYearsPeriods.Delete("sys_FiscalYearsPeriods.ID=" & CInt(txtCode.Value))
                        AfterOperation()
                    Else
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "an not Delete Fiscal Period / غير مسموح بالغاء الفترة المالية"))
                    End If
                End If
            Case "Property"
                If txtCode.Value.Trim <> "" Then
                    If ClsFiscalYearsPeriods.Find("sys_FiscalYearsPeriods.ID=" & CInt(txtCode.Value)) Then
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsFiscalYearsPeriods.ID & "&TableName=" & ClsFiscalYearsPeriods.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                    End If
                End If
            Case "Remarks"
                If txtCode.Value.Trim <> "" Then
                    If ClsFiscalYearsPeriods.Find("sys_FiscalYearsPeriods.ID=" & CInt(txtCode.Value)) Then
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsFiscalYearsPeriods.ID & "&TableName=" & ClsFiscalYearsPeriods.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                    End If
                End If

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsFiscalYears.Find(" ID= " & CInt(ddlFiscalYear.SelectedValue))
                ClsFiscalYears.FirstRecord(" ")
                ddlFiscalYear.SelectedValue = ClsFiscalYears.ID
                Change_ddlFiscalYear()
            Case "Previous"
                ClsFiscalYears.Find(" ID = " & CInt(ddlFiscalYear.SelectedValue))
                If Not ClsFiscalYears.NextRecord(" AND ID > " & CInt(ddlFiscalYear.SelectedValue)) Then
                    ClsFiscalYears.Find(" ID = " & CInt(ddlFiscalYear.SelectedValue))
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                ddlFiscalYear.SelectedValue = ClsFiscalYears.ID
                Change_ddlFiscalYear()
            Case "Next"
                ClsFiscalYears.Find(" ID = " & CInt(ddlFiscalYear.SelectedValue))
                If Not ClsFiscalYears.NextRecord(" AND ID > " & CInt(ddlFiscalYear.SelectedValue)) Then
                    ClsFiscalYears.Find(" ID = " & CInt(ddlFiscalYear.SelectedValue))
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                ddlFiscalYear.SelectedValue = ClsFiscalYears.ID
                Change_ddlFiscalYear()
            Case "Last"
                ClsFiscalYears.Find(" ID = " & CInt(ddlFiscalYear.SelectedValue))
                ClsFiscalYears.LastRecord(" ")
                ddlFiscalYear.SelectedValue = ClsFiscalYears.ID
                Change_ddlFiscalYear()
        End Select
        SetToolBarPermission(Page, ClsFiscalYears.ConnectionString, ClsFiscalYears.DataBaseUserRelatedID, ClsFiscalYears.GroupID, "")
    End Sub
    Protected Sub ddlFiscalYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFiscalYear.SelectedIndexChanged
        Change_ddlFiscalYear()
        SetToolBarPermission(Page, ClsFiscalYears.ConnectionString, ClsFiscalYears.DataBaseUserRelatedID, ClsFiscalYears.GroupID, "")
    End Sub
    Protected Sub btnAddPeriod_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnAddPeriod.Click

        Dim Grego As New GregorianCalendar()
        Dim hijri As New UmAlQuraCalendar()
        Dim Gstartdate As DateTime
        Dim Genddate As DateTime
        Dim Hstartdate As String
        Dim Henddate As String
        Dim GetStartDateprepare As DateTime
        Dim GetEndDateprepare As DateTime
        Dim CompanyPrepareDay As Integer
        Dim ClsFiscalYearPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim ClsFiscalYear As New Clssys_FiscalYears(Page)
        Dim Clscompanies As New Clssys_Companies(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(Clscompanies.ConnectionString)

        Try

            ClsFiscalYear.Find("ID = " & ddlFiscalYear.SelectedValue)
            Clscompanies.Find("ID = " & Clscompanies.MainCompanyID)
            'If uwgFiscalYearsPeriods.Rows.Count = 0 Then
            CompanyPrepareDay = Clscompanies.PrepareDay
            Try
                Dim strCommand As String = "Delete From sys_FiscalYearsPeriodsModules Where FiscalYearPeriodID In (Select ID From sys_FiscalYearsPeriods Where FiscalYearID = " & ddlFiscalYear.SelectedValue & ");" & _
                                           "Delete From sys_FiscalYearsPeriodsObjects Where FiscalYearPeriodID In (Select ID From sys_FiscalYearsPeriods Where FiscalYearID = " & ddlFiscalYear.SelectedValue & ");" & _
                                           "Delete From sys_FiscalYearsPeriods Where FiscalYearID = " & ddlFiscalYear.SelectedValue

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsFiscalYear.ConnectionString, CommandType.Text, strCommand)
            Catch ex As Exception
                lblGabNotify.Text = ObjNavigationHandler.SetLanguage(Page, "Can't open the financial periods for this year because it used / لا يمكن فتح فترات مالية لهذه السنة لأنها مستخدمة")
                Exit Sub
            End Try

            Dim cFiscalYear As Integer
            If Clscompanies.IsHigry = True Then
                If ClsDataAcessLayer.IsHijri(CStr("1/6/" & Convert.ToInt32(ClsFiscalYear.EngName))) Then
                    cFiscalYear = Convert.ToInt32(ClsFiscalYear.EngName)
                Else
                    cFiscalYear = ClsDataAcessLayer.GregToHijri(CStr("1/6/" & Convert.ToInt32(ClsFiscalYear.EngName)), "dd/MM/yyyy").Split("/")(2)
                End If
            Else
                If ClsDataAcessLayer.IsGreg(CStr("1/6/" & Convert.ToInt32(ClsFiscalYear.EngName))) Then
                    cFiscalYear = Convert.ToInt32(ClsFiscalYear.EngName)
                Else
                    cFiscalYear = ClsDataAcessLayer.HijriToGreg(CStr("1/6/" & Convert.ToInt32(ClsFiscalYear.EngName)), "dd/MM/yyyy").Split("/")(2)
                End If
            End If



            If RadioButton_IsFormal.Checked = True Then
                Dim prepareyear As Integer
                Dim preparemonth As Integer
                For i As Int32 = 1 To 12
                    ClsFiscalYearPeriods = New Clssys_FiscalYearsPeriods(Page)
                    If Clscompanies.IsHigry = True Then
                        Hstartdate = "01/" & i & "/" & cFiscalYear
                        Dim days As Int32 = hijri.GetDaysInMonth(cFiscalYear, i)
                        Henddate = days & "/" & i & "/" & cFiscalYear
                        Gstartdate = ClsDataAcessLayer.HijriToGreg(Hstartdate, "dd/MM/yyyy")
                        Genddate = ClsDataAcessLayer.HijriToGreg(Henddate, "dd/MM/yyyy")
                    Else
                        Gstartdate = Grego.ToDateTime(cFiscalYear, i, 1, 0, 0, 0, 0)
                        Dim days As Int32 = Grego.GetDaysInMonth(cFiscalYear, i)
                        Genddate = Grego.ToDateTime(cFiscalYear, i, days, 0, 0, 0, 0)
                        Hstartdate = ClsDataAcessLayer.GregToHijri(Gstartdate.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
                        Henddate = ClsDataAcessLayer.GregToHijri(Genddate.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
                        If i = 1 Then
                            prepareyear = cFiscalYear - 1
                            preparemonth = 12
                            GetStartDateprepare = CompanyPrepareDay & "/" & preparemonth & "/" & prepareyear
                            GetEndDateprepare = CompanyPrepareDay - 1 & "/1/" & prepareyear + 1
                        Else
                            prepareyear = cFiscalYear
                            preparemonth = i
                            GetStartDateprepare = CompanyPrepareDay & "/" & preparemonth - 1 & "/" & prepareyear
                            GetEndDateprepare = CompanyPrepareDay - 1 & "/" & preparemonth & "/" & prepareyear
                        End If


                    End If

                    ClsFiscalYearPeriods.FiscalYearID = ddlFiscalYear.SelectedValue
                    ClsFiscalYearPeriods.EngName = RetPeriodNE(i.ToString(), cFiscalYear, Clscompanies.IsHigry)
                    ClsFiscalYearPeriods.ArbName = RetPeriodNA(i.ToString(), cFiscalYear, Clscompanies.IsHigry)
                    ClsFiscalYearPeriods.FromDate = Gstartdate
                    ClsFiscalYearPeriods.ToDate = Genddate
                    ClsFiscalYearPeriods.HFromDate = ClsDataAcessLayer.FormatHijri(Hstartdate, "dd/MM/yyyy")
                    ClsFiscalYearPeriods.HToDate = ClsDataAcessLayer.FormatHijri(Henddate, "dd/MM/yyyy")
                    ClsFiscalYearPeriods.PrepareFromDate = GetStartDateprepare
                    ClsFiscalYearPeriods.PrepareToDate = GetEndDateprepare
                    If Not ClsFiscalYearPeriods.Save() Then
                        ClsFiscalYearPeriods.Update("FiscalYearID = " & ddlFiscalYear.SelectedValue & " and EngName = '" & ClsFiscalYearPeriods.EngName & "' and ArbName = '" & ClsFiscalYearPeriods.ArbName & "'")
                    End If
                Next i
            Else
                If ddlFiscalYear.SelectedValue > 0 Then
                    ClsFiscalYearPeriods = New Clssys_FiscalYearsPeriods(Page)
                    ClsFiscalYearPeriods.Find("FiscalYearID = " & ddlGuidFisical.SelectedValue)
                    Dim Dt As New DataTable
                    Dt = ClsFiscalYearPeriods.DataSet.Tables(0)
                    Dim OldFYear As String = Dt.Rows(0)(14).ToString()
                    Dim NewFYear As String = ClsFiscalYear.EngName
                    For i As Int32 = 0 To Dt.Rows.Count - 1
                        ClsFiscalYearPeriods = New Clssys_FiscalYearsPeriods(Page)

                        Gstartdate = ClsDataAcessLayer.FormatGreg(Convert.ToDateTime(Dt.Rows(i)(5).ToString().Replace(OldFYear, NewFYear)).ToString("dd/MM/yyyy"), "dd/MM/yyyy")
                        Genddate = ClsDataAcessLayer.FormatGreg(Convert.ToDateTime(Dt.Rows(i)(6).ToString().Replace(OldFYear, NewFYear)).ToString("dd/MM/yyyy"), "dd/MM/yyyy")
                        Hstartdate = ClsDataAcessLayer.GregToHijri(Gstartdate.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
                        Henddate = ClsDataAcessLayer.GregToHijri(Genddate.ToString("dd/MM/yyyy"), "dd/MM/yyyy")

                        ClsFiscalYearPeriods.FiscalYearID = ddlFiscalYear.SelectedValue
                        ClsFiscalYearPeriods.EngName = Dt.Rows(i)(2).ToString().Replace(OldFYear, NewFYear)
                        ClsFiscalYearPeriods.ArbName = Dt.Rows(i)(3).ToString().Replace(OldFYear, NewFYear)
                        ClsFiscalYearPeriods.FromDate = Gstartdate
                        ClsFiscalYearPeriods.ToDate = Genddate
                        ClsFiscalYearPeriods.HFromDate = Hstartdate
                        ClsFiscalYearPeriods.HToDate = Henddate

                        If ClsFiscalYearPeriods.Save() = False Then
                            ClsFiscalYearPeriods.Update("FiscalYearID = " & ddlFiscalYear.SelectedValue & " and EngName = '" & ClsFiscalYearPeriods.EngName & "' and ArbName = '" & ClsFiscalYearPeriods.ArbName & "'")
                        End If
                    Next i
                End If
            End If
            Load_DataGrid(ddlFiscalYear.SelectedValue)
            AfterOperation()
            SetToolBarPermission(Page, ClsFiscalYears.ConnectionString, ClsFiscalYears.DataBaseUserRelatedID, ClsFiscalYears.GroupID, "")
            'lblGabNotify.Text = ObjNavigationHandler.SetLanguage(Page, "Save Done / تم الحفظ")


        Catch ex As Exception

        End Try
    End Sub
    Private Function RetPeriodNA(ByVal Period As String, ByVal Year As String, ByVal IsHijri As Boolean) As String
        If Period = "1" Then
            Return IIf(IsHijri = True, "المحرم " & Year, "يناير " & Year)
        ElseIf Period = "2" Then
            Return IIf(IsHijri = True, "صفر " & Year, "فبراير " & Year)
        ElseIf Period = "3" Then
            Return IIf(IsHijri = True, "ربيع أول " & Year, "مارس " & Year)
        ElseIf Period = "4" Then
            Return IIf(IsHijri = True, "ربيع أخر " & Year, "إبريل " & Year)
        ElseIf Period = "5" Then
            Return IIf(IsHijri = True, "جماد أول " & Year, "مايو " & Year)
        ElseIf Period = "6" Then
            Return IIf(IsHijri = True, "جماد أخر " & Year, "يونيه " & Year)
        ElseIf Period = "7" Then
            Return IIf(IsHijri = True, "رجب " & Year, "يوليو " & Year)
        ElseIf Period = "8" Then
            Return IIf(IsHijri = True, "شعبان " & Year, "أغسطس " & Year)
        ElseIf Period = "9" Then
            Return IIf(IsHijri = True, "رمضان " & Year, "سبتمبر " & Year)
        ElseIf Period = "10" Then
            Return IIf(IsHijri = True, "شوال " & Year, "أكتوبر " & Year)
        ElseIf Period = "11" Then
            Return IIf(IsHijri = True, "ذى القعده " & Year, "نوفمبر " & Year)
        ElseIf Period = "12" Then
            Return IIf(IsHijri = True, "ذى الحجة " & Year, "ديسمبر " & Year)
        Else
            Return ""
        End If
    End Function
    Private Function RetPeriodNE(ByVal Period As String, ByVal Year As String, ByVal IsHijri As Boolean) As String
        If Period = "1" Then
            Return IIf(IsHijri = True, "Moharm " & Year, "January " & Year)
        ElseIf Period = "2" Then
            Return IIf(IsHijri = True, "Safar " & Year, "February " & Year)
        ElseIf Period = "3" Then
            Return IIf(IsHijri = True, "RabieAwal " & Year, "March " & Year)
        ElseIf Period = "4" Then
            Return IIf(IsHijri = True, "RabiA7er " & Year, "Aprel " & Year)
        ElseIf Period = "5" Then
            Return IIf(IsHijri = True, "GomadAwal " & Year, "May " & Year)
        ElseIf Period = "6" Then
            Return IIf(IsHijri = True, "GomadA7er " & Year, "June " & Year)
        ElseIf Period = "7" Then
            Return IIf(IsHijri = True, "Ragab " & Year, "July " & Year)
        ElseIf Period = "8" Then
            Return IIf(IsHijri = True, "Shaban " & Year, "August " & Year)
        ElseIf Period = "9" Then
            Return IIf(IsHijri = True, "Ramadan " & Year, "September " & Year)
        ElseIf Period = "10" Then
            Return IIf(IsHijri = True, "Shawal " & Year, "October " & Year)
        ElseIf Period = "11" Then
            Return IIf(IsHijri = True, "ZeKe3da " & Year, "Novomber " & Year)
        ElseIf Period = "12" Then
            Return IIf(IsHijri = True, "ZeHeja " & Year, "December " & Year)
        Else
            Return ""
        End If
    End Function
    Protected Sub uwgFiscalYearsPeriods_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgFiscalYearsPeriods.ActiveRowChange
        txtRowIndex.Value = e.Row.Index
        txtRowID.Value = e.Row.Cells(0).Value
        lblGabNotify.Text = ""
    End Sub
    Protected Sub uwgFiscalYearsPeriods_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgFiscalYearsPeriods.InitializeRow
        Dim ClsFiscalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
    End Sub

#End Region

#Region "Private Functions"
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
                    btnAddPeriod.Enabled = .Item("AllowAdd")
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
                    txtCode.Value = String.Empty
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
                    ClsFiscalYears.Find("ID=" & intID)

                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsFiscalYears.Find("ID=" & intID)

                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsFiscalYears
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
        ClsFiscalYears = New Clssys_FiscalYears(Me.Page)
        Try
            With ClsFiscalYears
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
        ClsFiscalYears = New Clssys_FiscalYears(Me.Page)
        If IntId > 0 Then
            ClsFiscalYears.Find("ID=" & IntId)

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
        Dim intFYID As Integer = ddlFiscalYear.SelectedValue
        ClsFiscalYearsPeriods.Clear()
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        Clear(intFYID)
        If intFYID > 0 Then
            Load_DataGrid(intFYID)
        End If
    End Function
    Private Function Clear(ByVal intFYID As Integer) As Boolean
        txtCode.Value = String.Empty
        lblGabNotify.Text = ""
        If uwgFiscalYearsPeriods.Rows.Count > 0 Then
            uwgFiscalYearsPeriods.Rows(0).Selected = False
        End If
        txtRowIndex.Value = "-1"
        txtRowID.Value = "-1"

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsFiscalYears = New Clssys_FiscalYears(Me.Page)
        ClsFiscalYears.Find(" code = '" & txtCode.Value & "'")
        Dim recordID As Integer = ClsFiscalYears.ID
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
    Private Function Load_DataGrid(ByVal intFYID As Integer) As Boolean
        uwgFiscalYearsPeriods.DataSource = Nothing
        uwgFiscalYearsPeriods.DataBind()
        ClsFiscalYearsPeriods.Find(" sys_FiscalYearsPeriods.FiscalYearID = " & intFYID & " And IsNull(sys_FiscalYearsPeriods.CancelDate,'')=''   Order by FromDate ")
        uwgFiscalYearsPeriods.DataSource = ClsFiscalYearsPeriods.DataSet.Tables(0).DefaultView
        uwgFiscalYearsPeriods.DataBind()
        lblGabNotify.Text = ""
    End Function
    'Private Function Get_New_Dates(ByVal intFYID As Integer) As Boolean
    '    Dim ClsFiscalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
    '    Dim strDates As String = GetLastDate(intFYID)
    '    If strDates <> String.Empty Then
    '        Dim arrDates As String() = strDates.Split("$")
    '        WebDateChooser1.Value = arrDates(0)
    '        WebDateChooser2.Value = arrDates(1)
    '    Else
    '        WebDateChooser1.Value = ClsFiscalYearsPeriods.GetHigriDate(Date.Now)
    '        WebDateChooser2.Value = ClsFiscalYearsPeriods.GetHigriDate(Date.Now.AddDays(1))
    '    End If
    'End Function
    Private Function Change_ddlFiscalYear() As Boolean
        ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Me.Page)
        If ddlFiscalYear.SelectedValue > 0 Then
            Load_DataGrid(ddlFiscalYear.SelectedValue)
        End If
        Clear(ddlFiscalYear.SelectedValue)
        txtRowIndex.Value = "-1"
        txtRowID.Value = "-1"
    End Function
    Private Function CanDeleteFiscalPeriod(ByVal IntFiscalPeriodID As Integer) As Boolean
        Dim ClsFiscalYearPeriodModules As New Clssys_FiscalYearsPeriodsModules(Page)
        Dim ClsFiscalYearPeriodObjects As New Clssys_FiscalYearsPeriodsObjects(Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Page)
        If ClsEmployeesTransactions.Find(" FiscalYearPeriodID= " & IntFiscalPeriodID) Then
            Return False
        End If
        Return True
    End Function
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsFiscalYearsPeriods.Table) = True Then
            Dim StrTablename As String
            ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Me)
            StrTablename = ClsFiscalYearsPeriods.Table
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
    '<System.Web.Services.WebMethod()> _
    'Public Shared Function Get_Searched_Description(ByVal IntSearchId As Integer, ByVal strCode As String) As String
    '    Dim dsSearchs As New Data.DataSet
    '    Find("sys_Searchs", " sys_Searchs.Id = " & IntSearchId, dsSearchs)
    '    Dim dsObjects As New Data.DataSet
    '    Find("sys_Objects", " sys_Objects.Id = " & dsSearchs.Tables(0).Rows(0).Item("ObjectID"), dsObjects)
    '    Return GetFieldDescription(strCode, dsObjects.Tables(0).Rows(0).Item("Code"))
    'End Function
    'Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As Data.DataSet) As Boolean
    '    Dim StrSelectCommand As String = String.Empty
    '    Dim mSelectCommand = " Select * From " & Table
    '    Dim mSqlDataAdapter As New Data.SqlClient.SqlDataAdapter
    '    Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
    '    Try
    '        StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter & " And CancelDate IS Null", " Where CancelDate IS Null")
    '        StrSelectCommand = StrSelectCommand '& orderByStr
    '        mSqlDataAdapter = New Data.SqlClient.SqlDataAdapter(StrSelectCommand, ConnStr)
    '        DataSet = New Data.DataSet
    '        mSqlDataAdapter.Fill(DataSet)
    '        If DataSet.Tables(0).Rows.Count > 0 Then
    '            Return True
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Function
    'Public Shared Function GetFieldDescription(ByVal StrCode As String, ByVal StrTableName As String) As String
    '    Dim StrReturnData As Object
    '    Try
    '        StrReturnData = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, " Select EngName + '/' + ArbName From " & StrTableName & " Where Code = '" & StrCode.ToString.TrimStart.TrimEnd & "'")
    '        If IsNothing(StrReturnData) Then Return "/"
    '        If IsDBNull(StrReturnData) Then Return "/"
    '        Return StrReturnData
    '    Catch ex As Exception
    '        Return "/"
    '    End Try
    'End Function
    '<System.Web.Services.WebMethod()> _
    'Public Shared Function GetControlsStatus(ByVal intFYID As Integer, ByVal intFYPID As Integer) As String

    '    Dim blast As Boolean
    '    Dim bfirst As Boolean
    '    Dim WebDateChooser1Enabled As Boolean
    '    Dim WebDateChooser2Enabled As Boolean

    '    Dim dsFiscalYearsPeriods As New DataSet

    '    Find("sys_FiscalYearsPeriods", " FiscalYearID=" & intFYID & "  ORDER BY ID DESC", dsFiscalYearsPeriods)

    '    If (dsFiscalYearsPeriods.Tables(0).Rows(0).Item("ID") = intFYPID) Then
    '        blast = True
    '    End If
    '    Find("sys_FiscalYearsPeriods", " FiscalYearID=" & intFYID & "  ORDER BY ID ASC", dsFiscalYearsPeriods)

    '    If (dsFiscalYearsPeriods.Tables(0).Rows(0).Item("ID") = intFYPID) Then
    '        bfirst = True
    '    End If
    '    If (Not bfirst And Not blast) Then
    '        WebDateChooser1Enabled = False
    '        WebDateChooser2Enabled = False
    '    ElseIf (bfirst And blast) Then
    '        WebDateChooser1Enabled = True
    '        WebDateChooser2Enabled = True
    '    ElseIf bfirst Then
    '        WebDateChooser1Enabled = True
    '        WebDateChooser2Enabled = False
    '    ElseIf blast Then
    '        WebDateChooser1Enabled = False
    '        WebDateChooser2Enabled = True
    '    End If

    '    Return WebDateChooser1Enabled & "/" & WebDateChooser2Enabled

    'End Function
    '<System.Web.Services.WebMethod()> _
    'Public Shared Function GetLastDate(ByVal intFYID As Integer) As String
    '    Dim uwg As New Infragistics.WebUI.UltraWebGrid.UltraWebGrid
    '    uwg = HttpContext.Current.Session("uwgFiscalYearsPeriods")

    '    If uwg.Rows.Count > 0 Then
    '        Dim dtFromDate As Date = CDate(uwg.Rows(uwg.Rows.Count - 1).Cells(5).Value).AddDays(1)
    '        Dim dtToDate As Date = CDate(uwg.Rows(uwg.Rows.Count - 1).Cells(5).Value).AddDays(2)
    '        Return dtFromDate & "$" & dtToDate
    '    End If
    '    Return String.Empty
    'End Function
    '<System.Web.Services.WebMethod()> _
    'Public Shared Function GetRecordInfoAjax(ByVal recordID As Integer) As String
    '    Dim dsContractsTransactions As New DataSet
    '    Dim dsUser As New DataSet
    '    Dim retStr As String = ",,"

    '    Find("hrs_ContractsTransactions", "ID=" & recordID, dsContractsTransactions)

    '    With dsContractsTransactions.Tables(0).Rows(0)

    '        If .Item("ID") > 0 Then
    '            retStr = ""
    '            If Not .Item("RegUserID") = Nothing Then

    '                Find("sys_Users", "ID=" & .Item("RegUserID"), dsUser)

    '                If dsUser.Tables(0).Rows.Count > 0 Then
    '                    retStr = dsUser.Tables(0).Rows(0).Item("EngName")
    '                Else
    '                    retStr = ""
    '                End If
    '            End If
    '            If Convert.ToDateTime(.Item("RegDate")).Date = Nothing Then
    '                retStr &= ","
    '            Else
    '                retStr &= "," & CDate(.Item("RegDate")).ToShortDateString()
    '            End If
    '            If IsDBNull(.Item("CancelDate")) Then
    '                retStr &= ","
    '            Else
    '                retStr &= "," & CDate(.Item("CancelDate")).ToShortDateString()
    '            End If
    '        End If
    '    End With

    '    Return retStr
    'End Function
    '<System.Web.Services.WebMethod()> _
    'Public Shared Function GetRecordPermissionAjax(ByVal recordID As Integer) As String
    '    Dim StrRetStr As String = "1,1,1"
    '    Dim dsObjects As New DataSet
    '    Dim dsRecordsPermissions As New DataSet
    '    Dim ObjectsID As Int64

    '    Find("sys_Objects", "Code='hrs_ContractsTransactions'", dsObjects)

    '    ObjectsID = dsObjects.Tables(0).Rows(0).Item("ID")

    '    If ObjectsID > 0 And recordID > 0 Then

    '        Find("sys_RecordsPermissions", "ObjectID=" & ObjectsID & " And RecordID=" & recordID & " And UserID=" & HttpContext.Current.Session("UserID"), dsRecordsPermissions)

    '        If dsRecordsPermissions.Tables(0).Rows.Count > 0 Then

    '            With dsRecordsPermissions.Tables(0).Rows(0)
    '                If .Item("ID") > 0 Then
    '                    StrRetStr = ""
    '                    StrRetStr = IIf(.Item("CanEdit"), "0", "1")
    '                    StrRetStr &= IIf(.Item("CanDelete"), ",0", ",1")
    '                    StrRetStr &= IIf(.Item("CanPrint"), ",0", ",1")
    '                End If
    '            End With
    '        End If
    '    End If
    '    Return StrRetStr
    'End Function
    '<System.Web.Services.WebMethod()> _
    'Private Shared Function GetFormPermission(ByVal frmCode As String) As Boolean
    '    Dim StrFormPermission As String = "1,1,1"
    '    Dim dsForms As New DataSet
    '    Dim dsFormsPermissions As New DataSet

    '    If Find("sys_Forms", " Code='" & frmCode & "'", dsForms) Then
    '        Find("sys_FormsPermissions", "FormID=" & dsForms.Tables(0).Rows(0).Item("ID") & " and UserID=" & HttpContext.Current.Session("UserID"), dsFormsPermissions)

    '        With dsFormsPermissions.Tables(0).Rows(0)
    '            If .Item("ID") > 0 Then
    '                StrFormPermission = ""
    '                If .Item("AllowEdit") Then
    '                    StrFormPermission = "0"
    '                Else
    '                    StrFormPermission = "1"
    '                End If
    '                If .Item("AllowDelete") Then
    '                    StrFormPermission &= ",0"
    '                Else
    '                    StrFormPermission &= ",1"
    '                End If

    '                If .Item("AllowPrint") Then
    '                    StrFormPermission &= ",0"
    '                Else
    '                    StrFormPermission &= ",1"
    '                End If
    '            End If
    '        End With
    '    End If
    'End Function


#End Region
End Class
