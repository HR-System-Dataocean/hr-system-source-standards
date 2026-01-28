Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmProjecWizard
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim StrProject As String = Request.QueryString.Item("ProjID")

                Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
                clsProjects.Find(" ID = " & StrProject)

                lblProjectCode.Text = clsProjects.Code
                lblProjectName.Text = IIf(ClsNavHandler.SetLanguage(Me, "0/1") = 0, clsProjects.EngName, clsProjects.ArbName)

                If clsProjects.IsLocked Then
                    CType(Me.UltraWebTab1.Tabs(0), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
                Else
                    CType(Me.UltraWebTab1.Tabs(0), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
                    CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
                End If

                ToNewChangeID.Value = "0"
                Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
                If clsprojectchange.Find("ProjectID = " & Request.QueryString.Item("ProjID") & " and RegComputerID = 1 order by ID Desc") Then
                    CCHangeID.Value = clsprojectchange.ID
                    NewChangeID.Value = clsprojectchange.ID
                    GetData()
                End If

                Dim clspanalities As New Clshrs_Penalties(Me)
                clspanalities.GetList(uwgPenalities.DisplayLayout.Bands(0).Columns(1).ValueList, True)

                Dim clsrewards As New Clshrs_Rewards(Me)
                clsrewards.GetList(uwgBenfits.DisplayLayout.Bands(0).Columns(1).ValueList, True)

                Dim clsenum As New Clshrs_Enum(Me)
                clsenum.GetList(uwgBenfits.DisplayLayout.Bands(0).Columns(4).ValueList, True, "Flag = '1'")
                clsenum.GetList(uwgBenfits.DisplayLayout.Bands(0).Columns(6).ValueList, True, "Flag = '1'")
                clsenum.GetList(uwgPenalities.DisplayLayout.Bands(0).Columns(4).ValueList, True, "Flag = '1'")
                clsenum.GetList(uwgPenalities.DisplayLayout.Bands(0).Columns(6).ValueList, True, "Flag = '1'")
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Function GetList_Data(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal ObjDataset As DataSet) As Boolean
        Dim ObjDataRow As DataRow
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim clsEmployees As New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Try
            DdlValues.ValueListItems.Clear()
            Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
            Item.DisplayText = ObjNavigationHandler.SetLanguage(Page, "[Select your choise]/[إختر أحد الأختيارات]")
            Item.DataValue = 0
            DdlValues.ValueListItems.Add(Item)
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = IIf(IsDBNull(ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName"))), _
                                       IIf(IsDBNull(ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "ArbName/EngName"))), _
                                       "", _
                                       ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "ArbName/EngName"))), _
                                       ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")))
                Item.DataValue = ObjDataRow("ID")
                DdlValues.ValueListItems.Add(Item)
            Next
            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    Private Function GetData() As Boolean
        Try
            Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
            clsprojectchange.Find("ProjectID = " & Request.QueryString.Item("ProjID") & " and RegComputerID = 1 order by ID Desc")
            uwgChanges.DataSource = clsprojectchange.DataSet.Tables(0)
            uwgChanges.DataBind()

            Dim clsprojectsetting As New Clshrs_ProjectSetting(Me)
            If clsprojectsetting.Find("ProjectChangeID = " & NewChangeID.Value) Then
                txtInternalOvertimeFactor.Value = clsprojectsetting.InternalOvertimeFactor
                txtExternalOvertimeFactor.Value = clsprojectsetting.ExternalOvertimeFactor
                txtInternalHolidayFactor.Value = clsprojectsetting.InternalDayOffOvertimeFactor
                txtExternalHolidayFactor.Value = clsprojectsetting.ExternalDayOffOvertimeFactor
                WebNumericEdit_InternalExtension.Value = clsprojectsetting.InternalExtensionValue
                WebNumericEdit_ExternalExtension.Value = clsprojectsetting.ExternalExtensionValue
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub ddlLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(cls_ProjectLocations.ConnectionString)
        cls_ProjectLocations.GetDropDownList(ddlSubLocation, True, "ProjectChangeID = " & NewChangeID.Value & " and MainLocationID = " & ddlLocation.SelectedValue)

        uwgLocationPositions.Rows.Clear()
        uwgLocationPositions.Rows.Add()
        Dim clsprojectlocationdetails As New Clshrs_ProjectLocationDetails(Me)
        If clsprojectlocationdetails.Find("LocationID = " & ddlLocation.SelectedValue) Then
            uwgLocationPositions.DataSource = clsprojectlocationdetails.DataSet.Tables(0)
            uwgLocationPositions.DataBind()

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgLocationPositions.Rows
                If Not IsNothing(DGRow.Cells.FromKey("OrgID").Value) Then
                    DGRow.Cells.FromKey("PositionID").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("Qty").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("ExternalAmt").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("InternalAmt").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("WeekDays").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("IsAlternative").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("IsInvoiced").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
            Next
        End If

        cls_ProjectLocations = New Clshrs_ProjectLocations(Me)
        cls_ProjectLocations.Find("ID = " & ddlLocation.SelectedValue)
        Label_Cnt.Text = ClsNavHandler.SetLanguage(Page, "Required/المطلوب") & " " & Convert.ToString(cls_ProjectLocations.Required)
    End Sub

    Protected Sub ddlSubLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSubLocation.SelectedIndexChanged
        Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(cls_ProjectLocations.ConnectionString)

        uwgLocationPositions.Rows.Clear()
        uwgLocationPositions.Rows.Add()
        Dim clsprojectlocationdetails As New Clshrs_ProjectLocationDetails(Me)
        If clsprojectlocationdetails.Find("LocationID = " & ddlSubLocation.SelectedValue) Then
            uwgLocationPositions.DataSource = clsprojectlocationdetails.DataSet.Tables(0)
            uwgLocationPositions.DataBind()

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgLocationPositions.Rows
                If Not IsNothing(DGRow.Cells.FromKey("OrgID").Value) Then
                    DGRow.Cells.FromKey("PositionID").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("Qty").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("ExternalAmt").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("InternalAmt").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("WeekDays").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("IsAlternative").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                    DGRow.Cells.FromKey("IsInvoiced").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
            Next
        End If

        cls_ProjectLocations = New Clshrs_ProjectLocations(Me)
        cls_ProjectLocations.Find("ID = " & ddlSubLocation.SelectedValue)
        Label_Cnt.Text = ClsNavHandler.SetLanguage(Page, "Required/المطلوب") & " " & Convert.ToString(cls_ProjectLocations.Required)
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Dim Cloc As Integer = 0
        If ddlLocation.SelectedValue > 0 Then
            Cloc = ddlLocation.SelectedValue
        End If
        If ddlSubLocation.SelectedValue > 0 Then
            Cloc = ddlSubLocation.SelectedValue
        End If
        If Cloc = 0 Then
            Exit Sub
        End If

        Dim cnt As Integer = 0
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgLocationPositions.Rows
            If DGRow.Cells.FromKey("PositionID").Value > 0 And Not Convert.ToBoolean(DGRow.Cells.FromKey("IsDel").Value) Then
                cnt = cnt + IIf(DGRow.Cells.FromKey("Qty").Value = Nothing, 0, DGRow.Cells.FromKey("Qty").Value)
            End If
        Next
        Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(cls_ProjectLocations.ConnectionString)
        cls_ProjectLocations.Find("ID = " & Cloc)
        If cls_ProjectLocations.Required <> cnt Or cnt = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "The Qty Not Fit The Contract/العدد لا يتناسب مع التعاقد"))
            Exit Sub
        End If

        Dim clsprojectlocationdetails As New Clshrs_ProjectLocationDetails(Me)
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgLocationPositions.Rows
            If Convert.ToBoolean(DGRow.Cells.FromKey("IsDel").Value) Then
                If Not IsNothing(DGRow.Cells.FromKey("OrgID").Value) Then
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocationdetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectLocationDetails where ID = " & DGRow.Cells.FromKey("OrgID").Value)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocationdetails.ConnectionString, Data.CommandType.Text, "update hrs_ProjectPlacements set PlacementCode = REPLACE(PlacementCode,Replace(SUBSTRING(PlacementCode,0,CHARINDEX ('-',PlacementCode,0)),'Alt',''),'Loc:'+CONVERT(varchar(100),LocationID)) where LocationDetailID = " & DGRow.Cells.FromKey("OrgID").Value)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocationdetails.ConnectionString, Data.CommandType.Text, "update hrs_ProjectPlacements set LocationDetailID = null where LocationDetailID = " & DGRow.Cells.FromKey("OrgID").Value)
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocationdetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & DGRow.Cells.FromKey("OrgID").Value & ")")
                Else
                    Continue For
                End If
            End If

            If DGRow.Cells.FromKey("PositionID").Value > 0 Then
                clsprojectlocationdetails = New Clshrs_ProjectLocationDetails(Me)
                clsprojectlocationdetails.LocationID = Cloc
                clsprojectlocationdetails.PositionID = DGRow.Cells.FromKey("PositionID").Value
                clsprojectlocationdetails.Qty = IIf(Convert.ToString(DGRow.Cells.FromKey("Qty").Value) = "", 0, DGRow.Cells.FromKey("Qty").Value)
                clsprojectlocationdetails.InternalAmt = IIf(Convert.ToString(DGRow.Cells.FromKey("InternalAmt").Value) = "", 0, DGRow.Cells.FromKey("InternalAmt").Value)
                clsprojectlocationdetails.ExternalAmt = IIf(Convert.ToString(DGRow.Cells.FromKey("ExternalAmt").Value) = "", 0, DGRow.Cells.FromKey("ExternalAmt").Value)
                clsprojectlocationdetails.WeekDays = IIf(Convert.ToString(DGRow.Cells.FromKey("WeekDays").Value) = "", 0, DGRow.Cells.FromKey("WeekDays").Value)
                clsprojectlocationdetails.IsAlternative = Convert.ToBoolean(IIf(DGRow.Cells.FromKey("IsAlternative").Value = Nothing, False, True))
                clsprojectlocationdetails.IsInvoiced = Convert.ToBoolean(IIf(DGRow.Cells.FromKey("IsInvoiced").Value = Nothing, False, True))
                clsprojectlocationdetails.Remarks = IIf(Convert.ToString(DGRow.Cells.FromKey("Remarks").Value) = "", 0, DGRow.Cells.FromKey("Remarks").Value)
                If IsNothing(DGRow.Cells.FromKey("OrgID").Value) Then
                    Dim CLocationDtls = clsprojectlocationdetails.Save()
                    Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                    If clsprojectplacement.Find("LocationID = " & Cloc & " and LocationDetailID is null") Then
                        Dim DT As DataTable = clsprojectplacement.DataSet.Tables(0)
                        For i As Integer = 0 To IIf(Convert.ToString(DGRow.Cells.FromKey("Qty").Value) = "", 0, DGRow.Cells.FromKey("Qty").Value - 1)
                            clsprojectplacement.Find("ID = " & DT.Rows(i)("ID").ToString())
                            clsprojectplacement.LocationDetailID = CLocationDtls
                            clsprojectplacement.Update("ID = " & DT.Rows(i)("ID").ToString())
                        Next
                    End If
                Else
                    clsprojectlocationdetails.Update("ID = " & DGRow.Cells.FromKey("ID").Value)
                End If
            End If
        Next

        ddlLocation.SelectedValue = 0
        ddlSubLocation.SelectedValue = 0
        uwgLocationPositions.Rows.Clear()
        uwgLocationPositions.Rows.Add()
        Label_Cnt.Text = ""
    End Sub

    Protected Sub ddlLocation1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlLocation1.SelectedIndexChanged
        Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(cls_ProjectLocations.ConnectionString)
        cls_ProjectLocations.GetDropDownList(ddlsublocation1, True, "ProjectChangeID = " & NewChangeID.Value & " and MainLocationID = " & ddlLocation1.SelectedValue)
        Dim clsPositions As New Clshrs_Positions(Me)
        clsPositions.GetDropDownListForProjects(ddlPosition, True, "B.LocationID = " & ddlLocation1.SelectedValue)
        uwglocationshift.Rows.Clear()

        Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        hrsProjectLocationDetails.Find("ID = " & ddlPosition.SelectedValue)
        LblCnt1.Text = ClsNavHandler.SetLanguage(Page, "Required/المطلوب") & " " & Convert.ToString(hrsProjectLocationDetails.Qty) & "    " & IIf(hrsProjectLocationDetails.IsAlternative = True, "وبدلاء الراحات على " & IIf(hrsProjectLocationDetails.IsInvoiced = True, "العميل", "الشركة"), "")
    End Sub

    Protected Sub ddlsublocation1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlsublocation1.SelectedIndexChanged
        Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(cls_ProjectLocations.ConnectionString)
        Dim clsPositions As New Clshrs_Positions(Me)
        clsPositions.GetDropDownListForProjects(ddlPosition, True, "B.LocationID = " & ddlsublocation1.SelectedValue)
        uwglocationshift.Rows.Clear()

        Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        hrsProjectLocationDetails.Find("ID = " & ddlPosition.SelectedValue)
        LblCnt1.Text = ClsNavHandler.SetLanguage(Page, "Required/المطلوب") & " " & Convert.ToString(hrsProjectLocationDetails.Qty) & "    " & IIf(hrsProjectLocationDetails.IsAlternative = True, "وبدلاء الراحات على " & IIf(hrsProjectLocationDetails.IsInvoiced = True, "العميل", "الشركة"), "")
    End Sub

    Protected Sub ddlPosition_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPosition.SelectedIndexChanged
        Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
        If hrsProjectLocationShifts.Find("LocationDetailID = " & ddlPosition.SelectedValue) Then
            Dim clsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
            If clsAttendanceTableShifts.Find("ID = " & hrsProjectLocationShifts.AttendanceTableShiftID) Then
                ddlAttendancetable.SelectedValue = clsAttendanceTableShifts.AttendanceTableID
                clsAttendanceTableShifts.Find("AttendanceTableID = " & ddlAttendancetable.SelectedValue)
                uwglocationshift.DataSource = clsAttendanceTableShifts.DataSet.Tables(0)
                uwglocationshift.DataBind()

                For Each row As DataRow In hrsProjectLocationShifts.DataSet.Tables(0).Rows
                    For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwglocationshift.Rows
                        If DGRow.Cells.FromKey("ID").Value = row("AttendanceTableShiftID") Then
                            DGRow.Cells.FromKey("Qty").Value = row("Qty")
                        End If
                    Next
                Next
            End If
        Else
            If ddlAttendancetable.SelectedValue > 0 Then
                Dim clsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
                If clsAttendanceTableShifts.Find("AttendanceTableID = " & ddlAttendancetable.SelectedValue) Then
                    uwglocationshift.DataSource = clsAttendanceTableShifts.DataSet.Tables(0)
                    uwglocationshift.DataBind()
                End If
            End If
        End If

        Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)
        hrsProjectLocationDetails.Find("ID = " & ddlPosition.SelectedValue)
        LblCnt1.Text = ClsNavHandler.SetLanguage(Page, "Required/المطلوب") & " " & Convert.ToString(hrsProjectLocationDetails.Qty) & "    " & IIf(hrsProjectLocationDetails.IsAlternative = True, "وبدلاء الراحات على " & IIf(hrsProjectLocationDetails.IsInvoiced = True, "العميل", "الشركة"), "")

        Sat.Checked = False
        Sun.Checked = False
        Mon.Checked = False
        Tue.Checked = False
        Wed.Checked = False
        Thu.Checked = False
        Fri.Checked = False
        Dim strarrDayoff As String = hrsProjectLocationDetails.Remarks
        Dim strAr As String() = strarrDayoff.Split(",")
        Dim srl As Integer = 0
        For aridx As Integer = 0 To strAr.Length - 1
            If strAr(srl) = 1 Then
                Sat.Checked = True
            End If
            If strAr(srl) = 2 Then
                Sun.Checked = True
            End If
            If strAr(srl) = 3 Then
                Mon.Checked = True
            End If
            If strAr(srl) = 4 Then
                Tue.Checked = True
            End If
            If strAr(srl) = 5 Then
                Wed.Checked = True
            End If
            If strAr(srl) = 6 Then
                Thu.Checked = True
            End If
            If strAr(srl) = 7 Then
                Fri.Checked = True
            End If
            srl = srl + 1
        Next aridx

        If hrsProjectLocationDetails.IsAlternative = False Then
            T1.Visible = True
        Else
            T1.Visible = False
        End If
    End Sub

    Protected Sub ddlAttendancetable_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAttendancetable.SelectedIndexChanged
        If ddlPosition.SelectedValue > 0 Then
            Dim clsAttendanceTableShifts As New Clshrs_AttendanceTableShifts(Me)
            If clsAttendanceTableShifts.Find("AttendanceTableID = " & ddlAttendancetable.SelectedValue) Then
                uwglocationshift.DataSource = clsAttendanceTableShifts.DataSet.Tables(0)
                uwglocationshift.DataBind()
            End If
        End If

        Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        hrsProjectLocationDetails.Find("ID = " & ddlPosition.SelectedValue)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)
        LblCnt1.Text = ClsNavHandler.SetLanguage(Page, "Required/المطلوب") & " " & Convert.ToString(hrsProjectLocationDetails.Qty) & "    " & IIf(hrsProjectLocationDetails.IsAlternative = True, "وبدلاء الراحات على " & IIf(hrsProjectLocationDetails.IsInvoiced = True, "العميل", "الشركة"), "")
    End Sub

    Protected Sub btnsave1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnsave1.Click
        If ddlAttendancetable.SelectedValue > 0 And ddlPosition.SelectedValue > 0 Then
            Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)
            If hrsProjectLocationDetails.Find("ID = " & ddlPosition.SelectedValue) Then
                Dim Cloc As Integer = 0
                If ddlLocation1.SelectedValue > 0 Then
                    Cloc = ddlLocation1.SelectedValue
                End If
                If ddlsublocation1.SelectedValue > 0 Then
                    Cloc = ddlsublocation1.SelectedValue
                End If
                If Cloc = 0 Then
                    Exit Sub
                End If
                Dim cnt As Integer = 0
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwglocationshift.Rows
                    cnt = cnt + IIf(DGRow.Cells.FromKey("Qty").Value = Nothing, 0, DGRow.Cells.FromKey("Qty").Value)
                Next

                hrsProjectLocationDetails.Find("ID = " & ddlPosition.SelectedValue)
                If hrsProjectLocationDetails.IsAlternative = False Or (hrsProjectLocationDetails.IsAlternative = True And hrsProjectLocationDetails.IsInvoiced = False) Then
                    If hrsProjectLocationDetails.Qty <> cnt Or cnt = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "The Qty Not Fit The Contract/العدد لا يتناسب مع التعاقد"))
                        Exit Sub
                    End If
                Else
                    If cnt = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "The Qty Not Fit The Contract/العدد لا يتناسب مع التعاقد"))
                        Exit Sub
                    End If


                    Dim totalcount As Integer = hrsProjectLocationDetails.WeekDays * cnt
                    Dim Fullcount As Integer = 7 * cnt
                    Dim GabEmployees As Integer = Math.Ceiling((Convert.ToDecimal(Fullcount) - Convert.ToDecimal(totalcount)) / Convert.ToDecimal(hrsProjectLocationDetails.WeekDays))

                    If hrsProjectLocationDetails.Qty < (cnt + GabEmployees) Or cnt = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "The Qty Not Fit The Contract/العدد لا يتناسب مع التعاقد"))
                        Exit Sub
                    End If
                End If
                Dim cntdayoff As Integer = 0
                Dim strarrDayoff As String = "0"
                If hrsProjectLocationDetails.IsAlternative = False Then

                    If Sat.Checked Then
                        cntdayoff = cntdayoff + 1
                        strarrDayoff = strarrDayoff & ",1"
                    End If
                    If Sun.Checked Then
                        cntdayoff = cntdayoff + 1
                        strarrDayoff = strarrDayoff & ",2"
                    End If
                    If Mon.Checked Then
                        cntdayoff = cntdayoff + 1
                        strarrDayoff = strarrDayoff & ",3"
                    End If
                    If Tue.Checked Then
                        cntdayoff = cntdayoff + 1
                        strarrDayoff = strarrDayoff & ",4"
                    End If
                    If Wed.Checked Then
                        cntdayoff = cntdayoff + 1
                        strarrDayoff = strarrDayoff & ",5"
                    End If
                    If Thu.Checked Then
                        cntdayoff = cntdayoff + 1
                        strarrDayoff = strarrDayoff & ",6"
                    End If
                    If Fri.Checked Then
                        cntdayoff = cntdayoff + 1
                        strarrDayoff = strarrDayoff & ",7"
                    End If

                    If hrsProjectLocationDetails.WeekDays + cntdayoff <> 7 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "The Dayoff Days Count Not Valid/عدد أيام العطلات الاسبوعية غير منضبط"))
                        Exit Sub
                    End If
                End If


                hrsProjectLocationDetails.Remarks = strarrDayoff
                hrsProjectLocationDetails.Update("ID = " & ddlPosition.SelectedValue)

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectLocationShifts where LocationDetailID = " & ddlPosition.SelectedValue)
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & ddlPosition.SelectedValue & ")")
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwglocationshift.Rows
                    Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
                    hrsProjectLocationShifts.LocationDetailID = ddlPosition.SelectedValue
                    hrsProjectLocationShifts.AttendanceTableShiftID = DGRow.Cells.FromKey("ID").Value
                    hrsProjectLocationShifts.Qty = DGRow.Cells.FromKey("Qty").Value
                    hrsProjectLocationShifts.Save()
                Next
            End If
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
            Exit Sub
        End If
    End Sub

    Protected Sub LinkButton_GenerateOne_Click(sender As Object, e As System.EventArgs) Handles LinkButton_GenerateOne.Click
        Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)
        If hrsProjectLocationDetails.Find("ID = " & ddlPosition.SelectedValue) Then
            If hrsProjectLocationDetails.IsAlternative = False Then
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                For Each PlaceDatarow As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                    Dim cinday As Integer = 0
                    For indys As Integer = 1 To 7
                        cinday = cinday + 1
                        Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                        hrsProjectPlacementPlanning.DayID = cinday
                        hrsProjectPlacementPlanning.PlacementID = PlaceDatarow("ID")
                        hrsProjectPlacementPlanning.Save()
                    Next indys
                Next

                Dim strarrDayoff As String = "0"
                strarrDayoff = hrsProjectLocationDetails.Remarks

                Dim strAr As String() = strarrDayoff.Split(",")

                Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
                If hrsProjectLocationShifts.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and Qty > 0") Then
                    For Each Datar As DataRow In hrsProjectLocationShifts.DataSet.Tables(0).Rows
                        For i As Integer = 1 To Datar("Qty")
                            hrsProjectPlacements = New Clshrs_ProjectPlacements(Me)
                            If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                For dy As Integer = 1 To 7
                                    Select Case dy
                                        Case 1
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 1 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 2
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 2 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 3
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 3 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 4
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 4 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 5
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 5 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 6
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 6 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                        Case 7
                                            Dim srl As Integer = 0
                                            For aridx As Integer = 0 To strAr.Length - 1
                                                If strAr(srl) = 7 Then
                                                    GoTo L1
                                                End If
                                                srl = srl + 1
                                            Next aridx
                                    End Select
                                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                    hrsProjectPlacementPlanning.Find("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & dy)
                                    hrsProjectPlacementPlanning.AttendanceTableShiftID = Datar("AttendanceTableShiftID")
                                    hrsProjectPlacementPlanning.Update("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & dy)
L1:
                                Next
                            End If
                        Next
                    Next
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                End If
            ElseIf hrsProjectLocationDetails.IsAlternative = True And hrsProjectLocationDetails.IsInvoiced = True Then
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                For Each PlaceDatarow As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                    Dim cinday As Integer = 0
                    For indys As Integer = 1 To 7
                        cinday = cinday + 1
                        Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                        hrsProjectPlacementPlanning.DayID = cinday
                        hrsProjectPlacementPlanning.PlacementID = PlaceDatarow("ID")
                        hrsProjectPlacementPlanning.Save()
                    Next indys
                Next

                Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
                If hrsProjectLocationShifts.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and Qty > 0") Then
                    Dim seq As Integer = 1
                    Dim serial As Integer = 0
                    For Each Datar As DataRow In hrsProjectLocationShifts.DataSet.Tables(0).Rows
                        For i As Integer = 1 To Datar("Qty")
                            serial = serial + 1
                            seq = serial - (Math.Floor(Convert.ToDecimal(serial) / Convert.ToDecimal(7)) * 7)
                            seq = IIf(seq = 0, 7, seq)
                            If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                For dy As Integer = 1 To hrsProjectLocationDetails.WeekDays
                                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                    hrsProjectPlacementPlanning.Find("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                    hrsProjectPlacementPlanning.AttendanceTableShiftID = Datar("AttendanceTableShiftID")
                                    hrsProjectPlacementPlanning.Update("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                    seq = seq + 1
                                    If seq > 7 Then seq = 1
                                Next
                            End If
                        Next i
                    Next

                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "update hrs_ProjectPlacements set RegComputerID = null where LocationDetailID = " & hrsProjectLocationDetails.ID & "; update hrs_ProjectPlacements set RegComputerID = 1 where LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)")
                    If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                        Dim strcommand As String = "select ID,PlacementID,AttendanceTableShiftID,DayID,(select MAX(B.AttendanceTableShiftID) from hrs_ProjectPlacementPlanning B where B.PlacementID = hrs_ProjectPlacementPlanning.PlacementID) AS RelShift from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & " and isnull(RegComputerID,0)= 0)"
                        Dim DTRest As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectPlacements.ConnectionString, Data.CommandType.Text, strcommand).Tables(0)

                        Dim totalcount As Integer = hrsProjectLocationDetails.WeekDays * DTRest.Rows.Count
                        Dim Fullcount As Integer = 7 * DTRest.Rows.Count
                        Dim GabEmployees As Integer = Math.Floor((Convert.ToDecimal(Fullcount) - Convert.ToDecimal(totalcount)) / Convert.ToDecimal(hrsProjectLocationDetails.WeekDays))
                        Dim altrcount = GabEmployees
                        Dim diminsions As Integer = 0
                        If altrcount > 0 Then
                            diminsions = Math.Ceiling(Convert.ToDecimal(DTRest.Rows.Count) / Convert.ToDecimal(altrcount))
                        End If

                        Dim array As Integer = 0

                        Dim plcDT As DataTable = hrsProjectPlacements.DataSet.Tables(0)
                        For i As Integer = 0 To plcDT.Rows.Count
                            If array + 1 > altrcount Then
                                Continue For
                            End If
                            Dim selected As Integer = array
                            For cnt1 As Integer = 1 To IIf(diminsions > hrsProjectLocationDetails.WeekDays, hrsProjectLocationDetails.WeekDays, diminsions)
                                Try
                                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                    hrsProjectPlacementPlanning.Find("PlacementID = " & plcDT.Rows(i)("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                    hrsProjectPlacementPlanning.AttendanceTableShiftID = DTRest.Rows(selected)("RelShift")
                                    hrsProjectPlacementPlanning.ReferenceTo = DTRest.Rows(selected)("ID")
                                    hrsProjectPlacementPlanning.Update("PlacementID = " & plcDT.Rows(i)("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                    selected = selected + altrcount
                                Catch ex As Exception
                                End Try
                            Next
                            array = array + 1
                        Next
                    End If
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and ID in (select ReferenceTo from hrs_ProjectPlacementPlanning); delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where isnull(RegComputerID,0) = 1); update hrs_ProjectPlacementPlanning set ReferenceTo = 1 where isnull(ReferenceTo,0) > 0 ;")
                End If
            ElseIf hrsProjectLocationDetails.IsAlternative = True And hrsProjectLocationDetails.IsInvoiced = False Then
                Dim totalcount As Integer = hrsProjectLocationDetails.WeekDays * hrsProjectLocationDetails.Qty
                Dim Fullcount As Integer = 7 * hrsProjectLocationDetails.Qty
                Dim GabEmployees As Integer = Math.Floor((Convert.ToDecimal(Fullcount) - Convert.ToDecimal(totalcount)) / Convert.ToDecimal(hrsProjectLocationDetails.WeekDays))
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                If hrsProjectPlacements.DataSet.Tables(0).Rows.Count <> (hrsProjectLocationDetails.Qty + GabEmployees) Then
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacements where RegComputerID = 1 and LocationDetailID = " & hrsProjectLocationDetails.ID)
                    Dim listarray As Integer = 1
                    Dim CPlacementArr As Integer = hrsProjectPlacements.DataSet.Tables(0).Rows.Count
                    For ins As Integer = 1 To GabEmployees
                        CPlacementArr = CPlacementArr + 1
                        hrsProjectPlacements = New Clshrs_ProjectPlacements(Me)
                        hrsProjectPlacements.PlacementCode = "AltLoc:" & hrsProjectLocationDetails.LocationID.ToString() & "-PlaceNo:" & CPlacementArr.ToString()
                        hrsProjectPlacements.LocationID = hrsProjectLocationDetails.LocationID.ToString()
                        hrsProjectPlacements.LocationDetailID = hrsProjectLocationDetails.ID
                        hrsProjectPlacements.ProjectID = Request.QueryString.Item("ProjID")
                        hrsProjectPlacements.ProjectChangeID = NewChangeID.Value
                        hrsProjectPlacements.RegComputerID = 1
                        hrsProjectPlacements.Save()
                        listarray = listarray + 1
                    Next ins
                End If

                hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                For Each PlaceDatarow As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                    Dim cinday As Integer = 0
                    For indys As Integer = 1 To 7
                        cinday = cinday + 1
                        Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                        hrsProjectPlacementPlanning.DayID = cinday
                        hrsProjectPlacementPlanning.PlacementID = PlaceDatarow("ID")
                        hrsProjectPlacementPlanning.Save()
                    Next indys
                Next

                Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
                If hrsProjectLocationShifts.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and Qty > 0") Then
                    Dim seq As Integer = 1
                    Dim serial As Integer = 0
                    For Each Datar As DataRow In hrsProjectLocationShifts.DataSet.Tables(0).Rows
                        For i As Integer = 1 To Datar("Qty")
                            serial = serial + 1
                            seq = serial - (Math.Floor(Convert.ToDecimal(serial) / Convert.ToDecimal(7)) * 7)
                            seq = IIf(seq = 0, 7, seq)
                            If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                For dy As Integer = 1 To hrsProjectLocationDetails.WeekDays
                                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                    hrsProjectPlacementPlanning.Find("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                    hrsProjectPlacementPlanning.AttendanceTableShiftID = Datar("AttendanceTableShiftID")
                                    hrsProjectPlacementPlanning.Update("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                    seq = seq + 1
                                    If seq > 7 Then seq = 1
                                Next
                            End If
                        Next i
                    Next

                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "update hrs_ProjectPlacements set RegComputerID = null where LocationDetailID = " & hrsProjectLocationDetails.ID & "; update hrs_ProjectPlacements set RegComputerID = 1 where LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)")
                    If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                        Dim strcommand As String = "select ID,PlacementID,AttendanceTableShiftID,DayID,(select MAX(B.AttendanceTableShiftID) from hrs_ProjectPlacementPlanning B where B.PlacementID = hrs_ProjectPlacementPlanning.PlacementID) AS RelShift from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & " and isnull(RegComputerID,0)= 0) order by PlacementID,DayID"
                        Dim DTRest As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectPlacements.ConnectionString, Data.CommandType.Text, strcommand).Tables(0)

                        Dim altrcount = hrsProjectPlacements.DataSet.Tables(0).Rows.Count
                        Dim diminsions As Integer = Math.Ceiling(Convert.ToDecimal(DTRest.Rows.Count) / Convert.ToDecimal(altrcount))
                        Dim array As Integer = 0

                        For Each drplacements As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                            Dim selected As Integer = array
                            For cnt1 As Integer = 1 To IIf(diminsions > hrsProjectLocationDetails.WeekDays, hrsProjectLocationDetails.WeekDays, diminsions)
                                Try
                                    Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                    hrsProjectPlacementPlanning.Find("PlacementID = " & drplacements("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                    hrsProjectPlacementPlanning.AttendanceTableShiftID = DTRest.Rows(selected)("RelShift")
                                    hrsProjectPlacementPlanning.ReferenceTo = DTRest.Rows(selected)("ID")
                                    hrsProjectPlacementPlanning.Update("PlacementID = " & drplacements("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                    selected = selected + altrcount
                                Catch ex As Exception

                                End Try
                            Next
                            array = array + 1
                        Next
                    End If
                End If
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and ID in (select ReferenceTo from hrs_ProjectPlacementPlanning); delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode like '%Alt%'); update hrs_ProjectPlacementPlanning set ReferenceTo = 1 where isnull(ReferenceTo,0) > 0 ;")
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myFunction", "OpenScreen();", True)
        End If
    End Sub

    Protected Sub LinkButton_Generate_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Generate.Click
        Dim AllProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(AllProjectLocationDetails.ConnectionString)
        If AllProjectLocationDetails.Find("LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & NewChangeID.Value & ")") Then
            Dim AllDT As DataTable = AllProjectLocationDetails.DataSet.Tables(0)
            For Each rw As DataRow In AllDT.Rows
                Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)

                If hrsProjectLocationDetails.Find("ID = " & rw("ID")) Then
                    If hrsProjectLocationDetails.IsAlternative = False Then
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                        Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                        hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                        For Each PlaceDatarow As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                            Dim cinday As Integer = 0
                            For indys As Integer = 1 To 7
                                cinday = cinday + 1
                                Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                hrsProjectPlacementPlanning.DayID = cinday
                                hrsProjectPlacementPlanning.PlacementID = PlaceDatarow("ID")
                                hrsProjectPlacementPlanning.Save()
                            Next indys
                        Next

                        Dim strarrDayoff As String = "0"
                        strarrDayoff = hrsProjectLocationDetails.Remarks

                        Dim strAr As String() = strarrDayoff.Split(",")

                        Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
                        If hrsProjectLocationShifts.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and Qty > 0") Then
                            For Each Datar As DataRow In hrsProjectLocationShifts.DataSet.Tables(0).Rows
                                For i As Integer = 1 To Datar("Qty")
                                    hrsProjectPlacements = New Clshrs_ProjectPlacements(Me)
                                    If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                        For dy As Integer = 1 To 7
                                            Select Case dy
                                                Case 1
                                                    Dim srl As Integer = 0
                                                    For aridx As Integer = 0 To strAr.Length - 1
                                                        If strAr(srl) = 1 Then
                                                            GoTo L1
                                                        End If
                                                        srl = srl + 1
                                                    Next aridx
                                                Case 2
                                                    Dim srl As Integer = 0
                                                    For aridx As Integer = 0 To strAr.Length - 1
                                                        If strAr(srl) = 2 Then
                                                            GoTo L1
                                                        End If
                                                        srl = srl + 1
                                                    Next aridx
                                                Case 3
                                                    Dim srl As Integer = 0
                                                    For aridx As Integer = 0 To strAr.Length - 1
                                                        If strAr(srl) = 3 Then
                                                            GoTo L1
                                                        End If
                                                        srl = srl + 1
                                                    Next aridx
                                                Case 4
                                                    Dim srl As Integer = 0
                                                    For aridx As Integer = 0 To strAr.Length - 1
                                                        If strAr(srl) = 4 Then
                                                            GoTo L1
                                                        End If
                                                        srl = srl + 1
                                                    Next aridx
                                                Case 5
                                                    Dim srl As Integer = 0
                                                    For aridx As Integer = 0 To strAr.Length - 1
                                                        If strAr(srl) = 5 Then
                                                            GoTo L1
                                                        End If
                                                        srl = srl + 1
                                                    Next aridx
                                                Case 6
                                                    Dim srl As Integer = 0
                                                    For aridx As Integer = 0 To strAr.Length - 1
                                                        If strAr(srl) = 6 Then
                                                            GoTo L1
                                                        End If
                                                        srl = srl + 1
                                                    Next aridx
                                                Case 7
                                                    Dim srl As Integer = 0
                                                    For aridx As Integer = 0 To strAr.Length - 1
                                                        If strAr(srl) = 7 Then
                                                            GoTo L1
                                                        End If
                                                        srl = srl + 1
                                                    Next aridx
                                            End Select
                                            Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                            hrsProjectPlacementPlanning.Find("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & dy)
                                            hrsProjectPlacementPlanning.AttendanceTableShiftID = Datar("AttendanceTableShiftID")
                                            hrsProjectPlacementPlanning.Update("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & dy)
L1:
                                        Next
                                    End If
                                Next
                            Next
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                        End If
                    ElseIf hrsProjectLocationDetails.IsAlternative = True And hrsProjectLocationDetails.IsInvoiced = True Then
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                        Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                        hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                        For Each PlaceDatarow As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                            Dim cinday As Integer = 0
                            For indys As Integer = 1 To 7
                                cinday = cinday + 1
                                Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                hrsProjectPlacementPlanning.DayID = cinday
                                hrsProjectPlacementPlanning.PlacementID = PlaceDatarow("ID")
                                hrsProjectPlacementPlanning.Save()
                            Next indys
                        Next

                        Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
                        If hrsProjectLocationShifts.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and Qty > 0") Then
                            Dim seq As Integer = 1
                            Dim serial As Integer = 0
                            For Each Datar As DataRow In hrsProjectLocationShifts.DataSet.Tables(0).Rows
                                For i As Integer = 1 To Datar("Qty")
                                    serial = serial + 1
                                    seq = serial - (Math.Floor(Convert.ToDecimal(serial) / Convert.ToDecimal(7)) * 7)
                                    seq = IIf(seq = 0, 7, seq)
                                    If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                        For dy As Integer = 1 To hrsProjectLocationDetails.WeekDays
                                            Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                            hrsProjectPlacementPlanning.Find("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                            hrsProjectPlacementPlanning.AttendanceTableShiftID = Datar("AttendanceTableShiftID")
                                            hrsProjectPlacementPlanning.Update("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                            seq = seq + 1
                                            If seq > 7 Then seq = 1
                                        Next
                                    End If
                                Next i
                            Next

                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "update hrs_ProjectPlacements set RegComputerID = null where LocationDetailID = " & hrsProjectLocationDetails.ID & "; update hrs_ProjectPlacements set RegComputerID = 1 where LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)")
                            If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                Dim strcommand As String = "select ID,PlacementID,AttendanceTableShiftID,DayID,(select MAX(B.AttendanceTableShiftID) from hrs_ProjectPlacementPlanning B where B.PlacementID = hrs_ProjectPlacementPlanning.PlacementID) AS RelShift from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & " and isnull(RegComputerID,0)= 0)"
                                Dim DTRest As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectPlacements.ConnectionString, Data.CommandType.Text, strcommand).Tables(0)

                                Dim totalcount As Integer = hrsProjectLocationDetails.WeekDays * DTRest.Rows.Count
                                Dim Fullcount As Integer = 7 * DTRest.Rows.Count
                                Dim GabEmployees As Integer = Math.Floor((Convert.ToDecimal(Fullcount) - Convert.ToDecimal(totalcount)) / Convert.ToDecimal(hrsProjectLocationDetails.WeekDays))
                                Dim altrcount = GabEmployees
                                Dim diminsions As Integer = 0
                                If altrcount > 0 Then
                                    diminsions = Math.Ceiling(Convert.ToDecimal(DTRest.Rows.Count) / Convert.ToDecimal(altrcount))
                                End If

                                Dim array As Integer = 0

                                Dim plcDT As DataTable = hrsProjectPlacements.DataSet.Tables(0)
                                For i As Integer = 0 To plcDT.Rows.Count
                                    If array + 1 > altrcount Then
                                        Continue For
                                    End If
                                    Dim selected As Integer = array
                                    For cnt1 As Integer = 1 To IIf(diminsions > hrsProjectLocationDetails.WeekDays, hrsProjectLocationDetails.WeekDays, diminsions)
                                        Try
                                            Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                            hrsProjectPlacementPlanning.Find("PlacementID = " & plcDT.Rows(i)("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                            hrsProjectPlacementPlanning.AttendanceTableShiftID = DTRest.Rows(selected)("RelShift")
                                            hrsProjectPlacementPlanning.ReferenceTo = DTRest.Rows(selected)("ID")
                                            hrsProjectPlacementPlanning.Update("PlacementID = " & plcDT.Rows(i)("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                            selected = selected + altrcount
                                        Catch ex As Exception
                                        End Try
                                    Next
                                    array = array + 1
                                Next
                            End If
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and ID in (select ReferenceTo from hrs_ProjectPlacementPlanning); delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where isnull(RegComputerID,0) = 1); update hrs_ProjectPlacementPlanning set ReferenceTo = 1 where isnull(ReferenceTo,0) > 0 ;")
                        End If
                    ElseIf hrsProjectLocationDetails.IsAlternative = True And hrsProjectLocationDetails.IsInvoiced = False Then
                        Dim totalcount As Integer = hrsProjectLocationDetails.WeekDays * hrsProjectLocationDetails.Qty
                        Dim Fullcount As Integer = 7 * hrsProjectLocationDetails.Qty
                        Dim GabEmployees As Integer = Math.Floor((Convert.ToDecimal(Fullcount) - Convert.ToDecimal(totalcount)) / Convert.ToDecimal(hrsProjectLocationDetails.WeekDays))
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & ")")
                        Dim hrsProjectPlacements As New Clshrs_ProjectPlacements(Me)
                        hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                        If hrsProjectPlacements.DataSet.Tables(0).Rows.Count <> (hrsProjectLocationDetails.Qty + GabEmployees) Then
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacements where RegComputerID = 1 and LocationDetailID = " & hrsProjectLocationDetails.ID)
                            Dim listarray As Integer = 1
                            Dim CPlacementArr As Integer = hrsProjectPlacements.DataSet.Tables(0).Rows.Count
                            For ins As Integer = 1 To GabEmployees
                                CPlacementArr = CPlacementArr + 1
                                hrsProjectPlacements = New Clshrs_ProjectPlacements(Me)
                                hrsProjectPlacements.PlacementCode = "AltLoc:" & hrsProjectLocationDetails.LocationID.ToString() & "-PlaceNo:" & CPlacementArr.ToString()
                                hrsProjectPlacements.LocationID = hrsProjectLocationDetails.LocationID.ToString()
                                hrsProjectPlacements.LocationDetailID = hrsProjectLocationDetails.ID
                                hrsProjectPlacements.ProjectID = Request.QueryString.Item("ProjID")
                                hrsProjectPlacements.ProjectChangeID = NewChangeID.Value
                                hrsProjectPlacements.RegComputerID = 1
                                hrsProjectPlacements.Save()
                                listarray = listarray + 1
                            Next ins
                        End If

                        hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID)
                        For Each PlaceDatarow As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                            Dim cinday As Integer = 0
                            For indys As Integer = 1 To 7
                                cinday = cinday + 1
                                Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                hrsProjectPlacementPlanning.DayID = cinday
                                hrsProjectPlacementPlanning.PlacementID = PlaceDatarow("ID")
                                hrsProjectPlacementPlanning.Save()
                            Next indys
                        Next

                        Dim hrsProjectLocationShifts As New Clshrs_ProjectLocationShifts(Me)
                        If hrsProjectLocationShifts.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and Qty > 0") Then
                            Dim seq As Integer = 1
                            Dim serial As Integer = 0
                            For Each Datar As DataRow In hrsProjectLocationShifts.DataSet.Tables(0).Rows
                                For i As Integer = 1 To Datar("Qty")
                                    serial = serial + 1
                                    seq = serial - (Math.Floor(Convert.ToDecimal(serial) / Convert.ToDecimal(7)) * 7)
                                    seq = IIf(seq = 0, 7, seq)
                                    If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                        For dy As Integer = 1 To hrsProjectLocationDetails.WeekDays
                                            Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                            hrsProjectPlacementPlanning.Find("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                            hrsProjectPlacementPlanning.AttendanceTableShiftID = Datar("AttendanceTableShiftID")
                                            hrsProjectPlacementPlanning.Update("PlacementID = " & hrsProjectPlacements.ID & " and DayID = " & seq)
                                            seq = seq + 1
                                            If seq > 7 Then seq = 1
                                        Next
                                    End If
                                Next i
                            Next

                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "update hrs_ProjectPlacements set RegComputerID = null where LocationDetailID = " & hrsProjectLocationDetails.ID & "; update hrs_ProjectPlacements set RegComputerID = 1 where LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)")
                            If hrsProjectPlacements.Find("LocationDetailID = " & hrsProjectLocationDetails.ID & " and ID not in (select PlacementID from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is not null)") Then
                                Dim strcommand As String = "select ID,PlacementID,AttendanceTableShiftID,DayID,(select MAX(B.AttendanceTableShiftID) from hrs_ProjectPlacementPlanning B where B.PlacementID = hrs_ProjectPlacementPlanning.PlacementID) AS RelShift from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where LocationDetailID = " & hrsProjectLocationDetails.ID & " and isnull(RegComputerID,0)= 0) order by DayID,PlacementID"
                                Dim DTRest As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectPlacements.ConnectionString, Data.CommandType.Text, strcommand).Tables(0)

                                Dim altrcount = hrsProjectPlacements.DataSet.Tables(0).Rows.Count
                                Dim diminsions As Integer = Math.Ceiling(Convert.ToDecimal(DTRest.Rows.Count) / Convert.ToDecimal(altrcount))
                                Dim array As Integer = 0

                                For Each drplacements As DataRow In hrsProjectPlacements.DataSet.Tables(0).Rows
                                    Dim selected As Integer = array
                                    For cnt1 As Integer = 1 To IIf(diminsions > hrsProjectLocationDetails.WeekDays, hrsProjectLocationDetails.WeekDays, diminsions)
                                        Try
                                            Dim hrsProjectPlacementPlanning As New Clshrs_ProjectPlacementPlanning(Me)
                                            hrsProjectPlacementPlanning.Find("PlacementID = " & drplacements("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                            hrsProjectPlacementPlanning.AttendanceTableShiftID = DTRest.Rows(selected)("RelShift")
                                            hrsProjectPlacementPlanning.ReferenceTo = DTRest.Rows(selected)("ID")
                                            hrsProjectPlacementPlanning.Update("PlacementID = " & drplacements("ID") & " and DayID = " & DTRest.Rows(selected)("DayID"))
                                            selected = selected + altrcount
                                        Catch ex As Exception

                                        End Try
                                    Next
                                    array = array + 1
                                Next
                            End If
                        End If
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and ID in (select ReferenceTo from hrs_ProjectPlacementPlanning); delete from hrs_ProjectPlacementPlanning where AttendanceTableShiftID is null and PlacementID in (select ID from hrs_ProjectPlacements where PlacementCode like '%Alt%'); update hrs_ProjectPlacementPlanning set ReferenceTo = 1 where isnull(ReferenceTo,0) > 0 ;")
                    End If
                End If
            Next
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Operation Done !/!تمت العملية"))
        End If
    End Sub

    Private Function SetDate(gData As Object, hDate As Object) As Date
        Try

            If gData <> "  /  /    " Then
                If ClsDataAcessLayer.IsGreg(gData) Then
                    Return ClsDataAcessLayer.FormatGreg(gData, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.HijriToGreg(gData, "dd/MM/yyyy")
                End If
            ElseIf hDate <> "  /  /    " Then
                If ClsDataAcessLayer.IsHijri(hDate) Then
                    Return ClsDataAcessLayer.HijriToGreg(hDate, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.FormatGreg(hDate, "dd/MM/yyyy")
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function SetToDate(NChange As Integer) As Boolean
        'Modification Details
        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        clsprojectchange.ProjectID = Request.QueryString.Item("ProjID")
        clsprojectchange.FromDate = SetDate(txtEndDate.Text, txtEndDate.Text)
        clsprojectchange.Remarks = txtCompanyConditions.Text
        NewChangeID.Value = clsprojectchange.Save()

        'Add hrs_ProjectRewards
        Dim clsprojectrewards As New Clshrs_ProjectRewards(Me)
        If clsprojectrewards.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectrewards.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectrewards = New Clshrs_ProjectRewards(Me)
                clsprojectrewards.ProjectChangeID = NewChangeID.Value
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
        If clsprojectpenalities.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectpenalities.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectpenalities = New Clshrs_ProjectPenalities(Me)
                clsprojectpenalities.ProjectChangeID = NewChangeID.Value
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
        If clsprojectsetting.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectsetting.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectsetting = New Clshrs_ProjectSetting(Me)
                clsprojectsetting.ProjectChangeID = NewChangeID.Value
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

        'Add hrs_ProjectLocations
        Dim clsprojectlocation As New Clshrs_ProjectLocations(Me)
        If clsprojectlocation.Find("MainLocationID is null and ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectlocation.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectlocation = New Clshrs_ProjectLocations(Me)
                clsprojectlocation.ProjectChangeID = NewChangeID.Value
                If Convert.ToString(row("MainLocationID")) <> "" Then
                    clsprojectlocation.MainLocationID = row("MainLocationID")
                End If
                clsprojectlocation.LocationDescription = row("LocationDescription")
                clsprojectlocation.LocationAddress = row("LocationAddress")
                clsprojectlocation.Required = row("Required")
                clsprojectlocation.LinkedCS = Convert.ToString(row("LinkedCS"))
                If Convert.ToString(row("Supervisor")) <> "" Then
                    clsprojectlocation.Supervisor = row("Supervisor")
                End If
                Dim CLocation As Integer = clsprojectlocation.Save()

                Dim MaininsUserEntry As String = "insert into hrs_ProjectLocationUsers (ProjectLocationID,UserID) select " & CLocation & ",UserID from hrs_ProjectLocationUsers where ProjectLocationID = " & row("ID")
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocation.ConnectionString, CommandType.Text, MaininsUserEntry)

                'Add hrs_ProjectLocationDetails
                Dim clsprojectlocationdetails As New Clshrs_ProjectLocationDetails(Me)
                If clsprojectlocationdetails.Find("LocationID = " & row("ID")) Then
                    Dim dt01 As DataTable = clsprojectlocationdetails.DataSet.Tables(0)
                    For Each row01 As DataRow In dt01.Rows
                        clsprojectlocationdetails = New Clshrs_ProjectLocationDetails(Me)
                        clsprojectlocationdetails.LocationID = CLocation
                        clsprojectlocationdetails.PositionID = row01("PositionID")
                        clsprojectlocationdetails.Qty = row01("Qty")
                        clsprojectlocationdetails.InternalAmt = row01("InternalAmt")
                        clsprojectlocationdetails.ExternalAmt = row01("ExternalAmt")
                        clsprojectlocationdetails.WeekDays = row01("WeekDays")
                        clsprojectlocationdetails.IsAlternative = row01("IsAlternative")
                        clsprojectlocationdetails.IsInvoiced = row01("IsInvoiced")
                        clsprojectlocationdetails.Remarks = row01("Remarks")
                        Dim CLocationDetails As Integer = clsprojectlocationdetails.Save()

                        'Add hrs_ProjectLocationShifts
                        Dim clsprojectlocationshift As New Clshrs_ProjectLocationShifts(Me)
                        If clsprojectlocationshift.Find("LocationDetailID = " & row01("ID")) Then
                            Dim dt02 As DataTable = clsprojectlocationshift.DataSet.Tables(0)
                            For Each row02 As DataRow In dt02.Rows
                                clsprojectlocationshift = New Clshrs_ProjectLocationShifts(Me)
                                clsprojectlocationshift.LocationDetailID = CLocationDetails
                                clsprojectlocationshift.AttendanceTableShiftID = row02("AttendanceTableShiftID")
                                clsprojectlocationshift.Qty = row02("Qty")
                                Dim CLocationShift As Integer = clsprojectlocationshift.Save()
                            Next
                        End If

                        'Add hrs_ProjectPlacements
                        Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                        If clsprojectplacement.Find("ProjectChangeID = " & CCHangeID.Value & " and LocationDetailID = " & row01("ID")) Then
                            Dim dt03 As DataTable = clsprojectplacement.DataSet.Tables(0)
                            For Each row03 As DataRow In dt03.Rows
                                clsprojectplacement = New Clshrs_ProjectPlacements(Me)
                                clsprojectplacement.PlacementCode = row03("PlacementCode")
                                clsprojectplacement.ProjectID = row03("ProjectID")
                                clsprojectplacement.ProjectChangeID = NewChangeID.Value
                                clsprojectplacement.LocationID = CLocation
                                clsprojectplacement.LocationDetailID = CLocationDetails
                                If Convert.ToString(row03("LoadPCT")) <> "" Then
                                    clsprojectplacement.LoadPCT = row03("LoadPCT")
                                End If
                                If Convert.ToString(row03("RegComputerID")) <> "" Then
                                    clsprojectplacement.RegComputerID = row03("RegComputerID")
                                End If
                                Dim CPlacementID As Integer = clsprojectplacement.Save()

                                'Add hrs_ProjectPlacementPlanning
                                Dim clsprojectplacementplanning As New Clshrs_ProjectPlacementPlanning(Me)
                                If clsprojectplacementplanning.Find("PlacementID = " & row03("ID")) Then
                                    Dim dt04 As DataTable = clsprojectplacementplanning.DataSet.Tables(0)
                                    For Each row04 As DataRow In dt04.Rows
                                        clsprojectplacementplanning = New Clshrs_ProjectPlacementPlanning(Me)
                                        If Convert.ToString(row04("AttendanceTableShiftID")) <> "" Then
                                            clsprojectplacementplanning.AttendanceTableShiftID = row04("AttendanceTableShiftID")
                                        End If
                                        clsprojectplacementplanning.PlacementID = CPlacementID
                                        clsprojectplacementplanning.DayID = row04("DayID")
                                        If Convert.ToString(row04("ReferenceTo")) <> "" Then
                                            clsprojectplacementplanning.ReferenceTo = row04("ReferenceTo")
                                        End If
                                        clsprojectplacementplanning.Save()
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If

                'Add hrs_ProjectLocationsDetails
                Dim clsprojectlocation3 As New Clshrs_ProjectLocations(Me)
                If clsprojectlocation3.Find("MainLocationID = " & row("ID") & " and ProjectChangeID = " & CCHangeID.Value) Then
                    Dim dt3 As DataTable = clsprojectlocation3.DataSet.Tables(0)
                    For Each row3 As DataRow In dt3.Rows
                        clsprojectlocation3 = New Clshrs_ProjectLocations(Me)
                        clsprojectlocation3.ProjectChangeID = NewChangeID.Value
                        If Convert.ToString(row3("MainLocationID")) <> "" Then
                            clsprojectlocation3.MainLocationID = CLocation
                        End If
                        clsprojectlocation3.LocationDescription = row3("LocationDescription")
                        clsprojectlocation3.LocationAddress = row3("LocationAddress")
                        clsprojectlocation3.Required = row3("Required")
                        clsprojectlocation3.LinkedCS = Convert.ToString(row3("LinkedCS"))
                        If Convert.ToString(row3("Supervisor")) <> "" Then
                            clsprojectlocation3.Supervisor = row3("Supervisor")
                        End If
                        Dim CLocation1 As Integer = clsprojectlocation3.Save()

                        Dim SubinsUserEntry As String = "insert into hrs_ProjectLocationUsers (ProjectLocationID,UserID) select " & CLocation1 & ",UserID from hrs_ProjectLocationUsers where ProjectLocationID = " & row3("ID")
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocation.ConnectionString, CommandType.Text, SubinsUserEntry)

                        'Add hrs_ProjectLocationDetails
                        Dim clsprojectlocation3details As New Clshrs_ProjectLocationDetails(Me)
                        If clsprojectlocation3details.Find("LocationID = " & row3("ID")) Then
                            Dim dt301 As DataTable = clsprojectlocation3details.DataSet.Tables(0)
                            For Each row301 As DataRow In dt301.Rows
                                clsprojectlocation3details = New Clshrs_ProjectLocationDetails(Me)
                                clsprojectlocation3details.LocationID = CLocation1
                                clsprojectlocation3details.PositionID = row301("PositionID")
                                clsprojectlocation3details.Qty = row301("Qty")
                                clsprojectlocation3details.InternalAmt = row301("InternalAmt")
                                clsprojectlocation3details.ExternalAmt = row301("ExternalAmt")
                                clsprojectlocation3details.WeekDays = row301("WeekDays")
                                clsprojectlocation3details.IsAlternative = row301("IsAlternative")
                                clsprojectlocation3details.IsInvoiced = row301("IsInvoiced")
                                clsprojectlocation3details.Remarks = row301("Remarks")
                                Dim CLocationDetails As Integer = clsprojectlocation3details.Save()

                                'Add hrs_ProjectLocationShifts
                                Dim clsprojectlocation3shift As New Clshrs_ProjectLocationShifts(Me)
                                If clsprojectlocation3shift.Find("LocationDetailID = " & row301("ID")) Then
                                    Dim dt302 As DataTable = clsprojectlocation3shift.DataSet.Tables(0)
                                    For Each row302 As DataRow In dt302.Rows
                                        clsprojectlocation3shift = New Clshrs_ProjectLocationShifts(Me)
                                        clsprojectlocation3shift.LocationDetailID = CLocationDetails
                                        clsprojectlocation3shift.AttendanceTableShiftID = row302("AttendanceTableShiftID")
                                        clsprojectlocation3shift.Qty = row302("Qty")
                                        Dim CLocationShift As Integer = clsprojectlocation3shift.Save()
                                    Next
                                End If

                                'Add hrs_ProjectPlacements
                                Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                                If clsprojectplacement.Find("ProjectChangeID = " & CCHangeID.Value & " and LocationDetailID = " & row301("ID")) Then
                                    Dim dt303 As DataTable = clsprojectplacement.DataSet.Tables(0)
                                    For Each row303 As DataRow In dt303.Rows
                                        clsprojectplacement = New Clshrs_ProjectPlacements(Me)
                                        clsprojectplacement.PlacementCode = row303("PlacementCode")
                                        clsprojectplacement.ProjectID = row303("ProjectID")
                                        clsprojectplacement.ProjectChangeID = NewChangeID.Value
                                        clsprojectplacement.LocationID = CLocation1
                                        clsprojectplacement.LocationDetailID = CLocationDetails
                                        If Convert.ToString(row303("LoadPCT")) <> "" Then
                                            clsprojectplacement.LoadPCT = row303("LoadPCT")
                                        End If
                                        If Convert.ToString(row303("RegComputerID")) <> "" Then
                                            clsprojectplacement.RegComputerID = row303("RegComputerID")
                                        End If
                                        Dim CPlacementID As Integer = clsprojectplacement.Save()

                                        'Add hrs_ProjectPlacementPlanning
                                        Dim clsprojectplacementplanning As New Clshrs_ProjectPlacementPlanning(Me)
                                        If clsprojectplacementplanning.Find("PlacementID = " & row303("ID")) Then
                                            Dim dt304 As DataTable = clsprojectplacementplanning.DataSet.Tables(0)
                                            For Each row304 As DataRow In dt304.Rows
                                                clsprojectplacementplanning = New Clshrs_ProjectPlacementPlanning(Me)
                                                If Convert.ToString(row304("AttendanceTableShiftID")) <> "" Then
                                                    clsprojectplacementplanning.AttendanceTableShiftID = row304("AttendanceTableShiftID")
                                                End If
                                                clsprojectplacementplanning.PlacementID = CPlacementID
                                                clsprojectplacementplanning.DayID = row304("DayID")
                                                If Convert.ToString(row304("ReferenceTo")) <> "" Then
                                                    clsprojectplacementplanning.ReferenceTo = row304("ReferenceTo")
                                                End If
                                                clsprojectplacementplanning.Save()
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If
        ToNewChangeID.Value = NewChangeID.Value
        NewChangeID.Value = NChange
        Return True
    End Function

    Protected Sub LinkButton2_Click(sender As Object, e As System.EventArgs) Handles LinkButton2.Click
        'Modification Details
        Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)

        Dim clsprojectchange123 As New Clshrs_ProjectChanges(Me)
        If clsprojectchange123.Find("ProjectID = " & Request.QueryString.Item("ProjID") & " and RegComputerID = 1 order by ID Desc") Then
            If SetDate(txtStartDate.Text, txtStartDate.Text) <= clsprojectchange123.FromDate Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Start Date Must Be More Than Last Change/تاريخ بداية التفعيل لابد وان يكون اكبر من تاريخ أخر تعديل "))
                Return
            End If
        End If
        If txtEndDate.Text <> "  /  /    " Then
            If SetDate(txtEndDate.Text, txtEndDate.Text) <= SetDate(txtStartDate.Text, txtStartDate.Text) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Invalid Dates/التواريخ غير منضبطة"))
                Return
            End If
        End If

        Dim clsprojectchange As New Clshrs_ProjectChanges(Me)
        clsprojectchange.ProjectID = Request.QueryString.Item("ProjID")
        clsprojectchange.FromDate = SetDate(txtStartDate.Text, txtStartDate.Text)
        clsprojectchange.Remarks = txtCompanyConditions.Text
        NewChangeID.Value = clsprojectchange.Save()

        'Add hrs_ProjectRewards
        Dim clsprojectrewards As New Clshrs_ProjectRewards(Me)
        If clsprojectrewards.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectrewards.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectrewards = New Clshrs_ProjectRewards(Me)
                clsprojectrewards.ProjectChangeID = NewChangeID.Value
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
        If clsprojectpenalities.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectpenalities.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectpenalities = New Clshrs_ProjectPenalities(Me)
                clsprojectpenalities.ProjectChangeID = NewChangeID.Value
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
        If clsprojectsetting.Find("ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectsetting.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectsetting = New Clshrs_ProjectSetting(Me)
                clsprojectsetting.ProjectChangeID = NewChangeID.Value
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

        'Add hrs_ProjectLocations
        Dim clsprojectlocation As New Clshrs_ProjectLocations(Me)
        If clsprojectlocation.Find("MainLocationID is null and ProjectChangeID = " & CCHangeID.Value) Then
            Dim dt As DataTable = clsprojectlocation.DataSet.Tables(0)
            For Each row As DataRow In dt.Rows
                clsprojectlocation = New Clshrs_ProjectLocations(Me)
                clsprojectlocation.ProjectChangeID = NewChangeID.Value
                If Convert.ToString(row("MainLocationID")) <> "" Then
                    clsprojectlocation.MainLocationID = row("MainLocationID")
                End If
                clsprojectlocation.LocationDescription = row("LocationDescription")
                clsprojectlocation.LocationAddress = row("LocationAddress")
                clsprojectlocation.Required = row("Required")
                clsprojectlocation.LinkedCS = Convert.ToString(row("LinkedCS"))
                If Convert.ToString(row("Supervisor")) <> "" Then
                    clsprojectlocation.Supervisor = row("Supervisor")
                End If
                Dim CLocation As Integer = clsprojectlocation.Save()

                Dim MaininsUserEntry As String = "insert into hrs_ProjectLocationUsers (ProjectLocationID,UserID) select " & CLocation & ",UserID from hrs_ProjectLocationUsers where ProjectLocationID = " & row("ID")
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocation.ConnectionString, CommandType.Text, MaininsUserEntry)

                'Add hrs_ProjectLocationDetails
                Dim clsprojectlocationdetails As New Clshrs_ProjectLocationDetails(Me)
                If clsprojectlocationdetails.Find("LocationID = " & row("ID")) Then
                    Dim dt01 As DataTable = clsprojectlocationdetails.DataSet.Tables(0)
                    For Each row01 As DataRow In dt01.Rows
                        clsprojectlocationdetails = New Clshrs_ProjectLocationDetails(Me)
                        clsprojectlocationdetails.LocationID = CLocation
                        clsprojectlocationdetails.PositionID = row01("PositionID")
                        clsprojectlocationdetails.Qty = row01("Qty")
                        clsprojectlocationdetails.InternalAmt = row01("InternalAmt")
                        clsprojectlocationdetails.ExternalAmt = row01("ExternalAmt")
                        clsprojectlocationdetails.WeekDays = row01("WeekDays")
                        clsprojectlocationdetails.IsAlternative = row01("IsAlternative")
                        clsprojectlocationdetails.IsInvoiced = row01("IsInvoiced")
                        clsprojectlocationdetails.Remarks = row01("Remarks")
                        Dim CLocationDetails As Integer = clsprojectlocationdetails.Save()

                        'Add hrs_ProjectLocationShifts
                        Dim clsprojectlocationshift As New Clshrs_ProjectLocationShifts(Me)
                        If clsprojectlocationshift.Find("LocationDetailID = " & row01("ID")) Then
                            Dim dt02 As DataTable = clsprojectlocationshift.DataSet.Tables(0)
                            For Each row02 As DataRow In dt02.Rows
                                clsprojectlocationshift = New Clshrs_ProjectLocationShifts(Me)
                                clsprojectlocationshift.LocationDetailID = CLocationDetails
                                clsprojectlocationshift.AttendanceTableShiftID = row02("AttendanceTableShiftID")
                                clsprojectlocationshift.Qty = row02("Qty")
                                Dim CLocationShift As Integer = clsprojectlocationshift.Save()
                            Next
                        End If

                        'Add hrs_ProjectPlacements
                        Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                        If clsprojectplacement.Find("ProjectChangeID = " & CCHangeID.Value & " and LocationDetailID = " & row01("ID")) Then
                            Dim dt03 As DataTable = clsprojectplacement.DataSet.Tables(0)
                            For Each row03 As DataRow In dt03.Rows
                                clsprojectplacement = New Clshrs_ProjectPlacements(Me)
                                clsprojectplacement.PlacementCode = row03("PlacementCode")
                                clsprojectplacement.ProjectID = row03("ProjectID")
                                clsprojectplacement.ProjectChangeID = NewChangeID.Value
                                clsprojectplacement.LocationID = CLocation
                                clsprojectplacement.LocationDetailID = CLocationDetails
                                If Convert.ToString(row03("LoadPCT")) <> "" Then
                                    clsprojectplacement.LoadPCT = row03("LoadPCT")
                                End If
                                If Convert.ToString(row03("RegComputerID")) <> "" Then
                                    clsprojectplacement.RegComputerID = row03("RegComputerID")
                                End If
                                Dim CPlacementID As Integer = clsprojectplacement.Save()

                                'Add hrs_ProjectPlacementPlanning
                                Dim clsprojectplacementplanning As New Clshrs_ProjectPlacementPlanning(Me)
                                If clsprojectplacementplanning.Find("PlacementID = " & row03("ID")) Then
                                    Dim dt04 As DataTable = clsprojectplacementplanning.DataSet.Tables(0)
                                    For Each row04 As DataRow In dt04.Rows
                                        clsprojectplacementplanning = New Clshrs_ProjectPlacementPlanning(Me)
                                        If Convert.ToString(row04("AttendanceTableShiftID")) <> "" Then
                                            clsprojectplacementplanning.AttendanceTableShiftID = row04("AttendanceTableShiftID")
                                        End If
                                        clsprojectplacementplanning.PlacementID = CPlacementID
                                        clsprojectplacementplanning.DayID = row04("DayID")
                                        If Convert.ToString(row04("ReferenceTo")) <> "" Then
                                            clsprojectplacementplanning.ReferenceTo = row04("ReferenceTo")
                                        End If
                                        clsprojectplacementplanning.Save()
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If

                'Add hrs_ProjectLocationsDetails
                Dim clsprojectlocation3 As New Clshrs_ProjectLocations(Me)
                If clsprojectlocation3.Find("MainLocationID = " & row("ID") & " and ProjectChangeID = " & CCHangeID.Value) Then
                    Dim dt3 As DataTable = clsprojectlocation3.DataSet.Tables(0)
                    For Each row3 As DataRow In dt3.Rows
                        clsprojectlocation3 = New Clshrs_ProjectLocations(Me)
                        clsprojectlocation3.ProjectChangeID = NewChangeID.Value
                        If Convert.ToString(row3("MainLocationID")) <> "" Then
                            clsprojectlocation3.MainLocationID = CLocation
                        End If
                        clsprojectlocation3.LocationDescription = row3("LocationDescription")
                        clsprojectlocation3.LocationAddress = row3("LocationAddress")
                        clsprojectlocation3.Required = row3("Required")
                        clsprojectlocation3.LinkedCS = Convert.ToString(row3("LinkedCS"))
                        If Convert.ToString(row3("Supervisor")) <> "" Then
                            clsprojectlocation3.Supervisor = row3("Supervisor")
                        End If
                        Dim CLocation1 As Integer = clsprojectlocation3.Save()

                        Dim SubinsUserEntry As String = "insert into hrs_ProjectLocationUsers (ProjectLocationID,UserID) select " & CLocation1 & ",UserID from hrs_ProjectLocationUsers where ProjectLocationID = " & row3("ID")
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocation.ConnectionString, CommandType.Text, SubinsUserEntry)

                        'Add hrs_ProjectLocationDetails
                        Dim clsprojectlocation3details As New Clshrs_ProjectLocationDetails(Me)
                        If clsprojectlocation3details.Find("LocationID = " & row3("ID")) Then
                            Dim dt301 As DataTable = clsprojectlocation3details.DataSet.Tables(0)
                            For Each row301 As DataRow In dt301.Rows
                                clsprojectlocation3details = New Clshrs_ProjectLocationDetails(Me)
                                clsprojectlocation3details.LocationID = CLocation1
                                clsprojectlocation3details.PositionID = row301("PositionID")
                                clsprojectlocation3details.Qty = row301("Qty")
                                clsprojectlocation3details.InternalAmt = row301("InternalAmt")
                                clsprojectlocation3details.ExternalAmt = row301("ExternalAmt")
                                clsprojectlocation3details.WeekDays = row301("WeekDays")
                                clsprojectlocation3details.IsAlternative = row301("IsAlternative")
                                clsprojectlocation3details.IsInvoiced = row301("IsInvoiced")
                                clsprojectlocation3details.Remarks = row301("Remarks")
                                Dim CLocationDetails As Integer = clsprojectlocation3details.Save()

                                'Add hrs_ProjectLocationShifts
                                Dim clsprojectlocation3shift As New Clshrs_ProjectLocationShifts(Me)
                                If clsprojectlocation3shift.Find("LocationDetailID = " & row301("ID")) Then
                                    Dim dt302 As DataTable = clsprojectlocation3shift.DataSet.Tables(0)
                                    For Each row302 As DataRow In dt302.Rows
                                        clsprojectlocation3shift = New Clshrs_ProjectLocationShifts(Me)
                                        clsprojectlocation3shift.LocationDetailID = CLocationDetails
                                        clsprojectlocation3shift.AttendanceTableShiftID = row302("AttendanceTableShiftID")
                                        clsprojectlocation3shift.Qty = row302("Qty")
                                        Dim CLocationShift As Integer = clsprojectlocation3shift.Save()
                                    Next
                                End If

                                'Add hrs_ProjectPlacements
                                Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                                If clsprojectplacement.Find("ProjectChangeID = " & CCHangeID.Value & " and LocationDetailID = " & row301("ID")) Then
                                    Dim dt303 As DataTable = clsprojectplacement.DataSet.Tables(0)
                                    For Each row303 As DataRow In dt303.Rows
                                        clsprojectplacement = New Clshrs_ProjectPlacements(Me)
                                        clsprojectplacement.PlacementCode = row303("PlacementCode")
                                        clsprojectplacement.ProjectID = row303("ProjectID")
                                        clsprojectplacement.ProjectChangeID = NewChangeID.Value
                                        clsprojectplacement.LocationID = CLocation1
                                        clsprojectplacement.LocationDetailID = CLocationDetails
                                        If Convert.ToString(row303("LoadPCT")) <> "" Then
                                            clsprojectplacement.LoadPCT = row303("LoadPCT")
                                        End If
                                        If Convert.ToString(row303("RegComputerID")) <> "" Then
                                            clsprojectplacement.RegComputerID = row303("RegComputerID")
                                        End If
                                        Dim CPlacementID As Integer = clsprojectplacement.Save()

                                        'Add hrs_ProjectPlacementPlanning
                                        Dim clsprojectplacementplanning As New Clshrs_ProjectPlacementPlanning(Me)
                                        If clsprojectplacementplanning.Find("PlacementID = " & row303("ID")) Then
                                            Dim dt304 As DataTable = clsprojectplacementplanning.DataSet.Tables(0)
                                            For Each row304 As DataRow In dt304.Rows
                                                clsprojectplacementplanning = New Clshrs_ProjectPlacementPlanning(Me)
                                                If Convert.ToString(row304("AttendanceTableShiftID")) <> "" Then
                                                    clsprojectplacementplanning.AttendanceTableShiftID = row304("AttendanceTableShiftID")
                                                End If
                                                clsprojectplacementplanning.PlacementID = CPlacementID
                                                clsprojectplacementplanning.DayID = row304("DayID")
                                                If Convert.ToString(row304("ReferenceTo")) <> "" Then
                                                    clsprojectplacementplanning.ReferenceTo = row304("ReferenceTo")
                                                End If
                                                clsprojectplacementplanning.Save()
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If
        If txtEndDate.Text <> "  /  /    " Then
            SetToDate(NewChangeID.Value)
        End If

        clsprojectsetting = New Clshrs_ProjectSetting(Me)
        If clsprojectsetting.Find("ProjectChangeID = " & NewChangeID.Value) Then
            txtInternalOvertimeFactor.Value = clsprojectsetting.InternalOvertimeFactor
            txtExternalOvertimeFactor.Value = clsprojectsetting.ExternalOvertimeFactor
            txtInternalHolidayFactor.Value = clsprojectsetting.InternalDayOffOvertimeFactor
            txtExternalHolidayFactor.Value = clsprojectsetting.ExternalDayOffOvertimeFactor
            WebNumericEdit_InternalExtension.Value = clsprojectsetting.InternalExtensionValue
            WebNumericEdit_ExternalExtension.Value = clsprojectsetting.ExternalExtensionValue
        End If

        CType(Me.UltraWebTab1.Tabs(0), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton4_Click(sender As Object, e As System.EventArgs) Handles LinkButton4.Click
        'Overtime Forward
        Dim clsprojectsetting As New Clshrs_ProjectSetting(Me)
        If clsprojectsetting.Find("ProjectChangeID = " & NewChangeID.Value) Then
            clsprojectsetting.InternalOvertimeFactor = txtInternalOvertimeFactor.Value
            clsprojectsetting.ExternalOvertimeFactor = txtExternalOvertimeFactor.Value
            clsprojectsetting.InternalDayOffOvertimeFactor = txtInternalHolidayFactor.Value
            clsprojectsetting.ExternalDayOffOvertimeFactor = txtExternalHolidayFactor.Value
            clsprojectsetting.InternalExtensionValue = WebNumericEdit_InternalExtension.Value
            clsprojectsetting.ExternalExtensionValue = WebNumericEdit_ExternalExtension.Value
            clsprojectsetting.Update("ProjectChangeID = " & NewChangeID.Value)
        Else
            clsprojectsetting = New Clshrs_ProjectSetting(Me)
            clsprojectsetting.ProjectChangeID = NewChangeID.Value
            clsprojectsetting.InternalOvertimeFactor = txtInternalOvertimeFactor.Value
            clsprojectsetting.ExternalOvertimeFactor = txtExternalOvertimeFactor.Value
            clsprojectsetting.InternalDayOffOvertimeFactor = txtInternalHolidayFactor.Value
            clsprojectsetting.ExternalDayOffOvertimeFactor = txtExternalHolidayFactor.Value
            clsprojectsetting.InternalExtensionValue = WebNumericEdit_InternalExtension.Value
            clsprojectsetting.ExternalExtensionValue = WebNumericEdit_ExternalExtension.Value
            clsprojectsetting.Save()
        End If

        clsprojectsetting = New Clshrs_ProjectSetting(Me)
        If clsprojectsetting.Find("ProjectChangeID = " & NewChangeID.Value) Then
            txtInternalAbsentFactor.Text = clsprojectsetting.InternalAbsentFactor
            txtExternalAbsentFactor.Text = clsprojectsetting.ExternalAbsentFactor
            txtInternalSickFactor.Text = clsprojectsetting.InternalSickFactor
            txtExternalSickFactor.Text = clsprojectsetting.ExternalSickFactor
            txtInternalLeavFactor.Text = clsprojectsetting.InternalLeavFactor
            txtExternalLeavFactor.Text = clsprojectsetting.ExternalLeavFactor
            txtInternalPermitDelayFactor.Text = clsprojectsetting.InternalPermitDelayFactor
            txtExternalPermitDelayFactor.Text = clsprojectsetting.ExternalPermitDelayFactor
            txtInternalDelayPunishFactor.Text = clsprojectsetting.InternalDelayPunishFactor
            txtExternalDelayPunishFactor.Text = clsprojectsetting.ExternalDelayPunishFactor
        End If
        CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton5_Click(sender As Object, e As System.EventArgs) Handles LinkButton5.Click
        'Punishment Back
        Dim clsprojectsetting As New Clshrs_ProjectSetting(Me)
        If clsprojectsetting.Find("ProjectChangeID = " & NewChangeID.Value) Then
            txtInternalOvertimeFactor.Value = clsprojectsetting.InternalOvertimeFactor
            txtExternalOvertimeFactor.Value = clsprojectsetting.ExternalOvertimeFactor
            txtInternalHolidayFactor.Value = clsprojectsetting.InternalDayOffOvertimeFactor
            txtExternalHolidayFactor.Value = clsprojectsetting.ExternalDayOffOvertimeFactor
            WebNumericEdit_InternalExtension.Value = clsprojectsetting.InternalExtensionValue
            WebNumericEdit_ExternalExtension.Value = clsprojectsetting.ExternalExtensionValue
        End If
        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(1), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton6_Click(sender As Object, e As System.EventArgs) Handles LinkButton6.Click
        'Punishment Forward
        Dim clsprojectsetting As New Clshrs_ProjectSetting(Me)
        If clsprojectsetting.Find("ProjectChangeID = " & NewChangeID.Value) Then
            clsprojectsetting.InternalAbsentFactor = txtInternalAbsentFactor.Text
            clsprojectsetting.ExternalAbsentFactor = txtExternalAbsentFactor.Text
            clsprojectsetting.InternalSickFactor = txtInternalSickFactor.Text
            clsprojectsetting.ExternalSickFactor = txtExternalSickFactor.Text
            clsprojectsetting.InternalLeavFactor = txtInternalLeavFactor.Text
            clsprojectsetting.ExternalLeavFactor = txtExternalLeavFactor.Text
            clsprojectsetting.InternalPermitDelayFactor = txtInternalPermitDelayFactor.Text
            clsprojectsetting.ExternalPermitDelayFactor = txtExternalPermitDelayFactor.Text
            clsprojectsetting.InternalDelayPunishFactor = txtInternalDelayPunishFactor.Text
            clsprojectsetting.ExternalDelayPunishFactor = txtExternalDelayPunishFactor.Text
            clsprojectsetting.Update("ProjectChangeID = " & NewChangeID.Value)
        Else
            clsprojectsetting = New Clshrs_ProjectSetting(Me)
            clsprojectsetting.ProjectChangeID = NewChangeID.Value
            clsprojectsetting.InternalAbsentFactor = txtInternalAbsentFactor.Text
            clsprojectsetting.ExternalAbsentFactor = txtExternalAbsentFactor.Text
            clsprojectsetting.InternalSickFactor = txtInternalSickFactor.Text
            clsprojectsetting.ExternalSickFactor = txtExternalSickFactor.Text
            clsprojectsetting.InternalLeavFactor = txtInternalLeavFactor.Text
            clsprojectsetting.ExternalLeavFactor = txtExternalLeavFactor.Text
            clsprojectsetting.InternalPermitDelayFactor = txtInternalPermitDelayFactor.Text
            clsprojectsetting.ExternalPermitDelayFactor = txtExternalPermitDelayFactor.Text
            clsprojectsetting.InternalDelayPunishFactor = txtInternalDelayPunishFactor.Text
            clsprojectsetting.ExternalDelayPunishFactor = txtExternalDelayPunishFactor.Text
            clsprojectsetting.Save()
        End If

        uwgBenfits.Rows.Clear()
        uwgBenfits.Rows.Add()
        Dim clsprojectrewards As New Clshrs_ProjectRewards(Me)
        If clsprojectrewards.Find("ProjectChangeID = " & NewChangeID.Value) Then
            uwgBenfits.DataSource = clsprojectrewards.DataSet.Tables(0)
            uwgBenfits.DataBind()
        End If

        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        'CType(Me.UltraWebTab1.Tabs(5), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
        LinkButton10_Click(Nothing, Nothing)
    End Sub

    Protected Sub LinkButton7_Click(sender As Object, e As System.EventArgs) Handles LinkButton7.Click
        'Rewards Back
        Dim clsprojectsetting As New Clshrs_ProjectSetting(Me)
        If clsprojectsetting.Find("ProjectChangeID = " & NewChangeID.Value) Then
            txtInternalAbsentFactor.Text = clsprojectsetting.InternalAbsentFactor
            txtExternalAbsentFactor.Text = clsprojectsetting.ExternalAbsentFactor
            txtInternalSickFactor.Text = clsprojectsetting.InternalSickFactor
            txtExternalSickFactor.Text = clsprojectsetting.ExternalSickFactor
            txtInternalLeavFactor.Text = clsprojectsetting.InternalLeavFactor
            txtExternalLeavFactor.Text = clsprojectsetting.ExternalLeavFactor
            txtInternalPermitDelayFactor.Text = clsprojectsetting.InternalPermitDelayFactor
            txtExternalPermitDelayFactor.Text = clsprojectsetting.ExternalPermitDelayFactor
            txtInternalDelayPunishFactor.Text = clsprojectsetting.InternalDelayPunishFactor
            txtExternalDelayPunishFactor.Text = clsprojectsetting.ExternalDelayPunishFactor
        End If

        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton8_Click(sender As Object, e As System.EventArgs) Handles LinkButton8.Click
        'Rewards Forward
        Dim clsprojectrewards As New Clshrs_ProjectRewards(Me)
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgBenfits.Rows
            Try
                clsprojectrewards = New Clshrs_ProjectRewards(Me)
                clsprojectrewards.ProjectChangeID = NewChangeID.Value
                clsprojectrewards.RewardID = DGRow.Cells.FromKey("RewardID").Value
                clsprojectrewards.Occurance = DGRow.Cells.FromKey("Occurance").Value
                clsprojectrewards.ExternalValue = DGRow.Cells.FromKey("ExternalValue").Value
                clsprojectrewards.ExternalFactor = DGRow.Cells.FromKey("ExternalFactor").Value
                clsprojectrewards.InternalValue = DGRow.Cells.FromKey("InternalValue").Value
                clsprojectrewards.InternalFactor = DGRow.Cells.FromKey("InternalFactor").Value
                If DGRow.Cells.FromKey("ID").Value <> Nothing Then
                    clsprojectrewards.Update("ID = " & DGRow.Cells.FromKey("ID").Value)
                Else
                    If DGRow.Cells.FromKey("RewardID").Value <> Nothing And DGRow.Cells.FromKey("ExternalFactor").Value <> Nothing And DGRow.Cells.FromKey("InternalFactor").Value <> Nothing Then
                        clsprojectrewards.Save()
                    End If
                End If
            Catch ex As Exception
            End Try
        Next

        uwgPenalities.Rows.Clear()
        uwgPenalities.Rows.Add()
        Dim clsprojectpenalities As New Clshrs_ProjectPenalities(Me)
        If clsprojectpenalities.Find("ProjectChangeID = " & NewChangeID.Value) Then
            uwgPenalities.DataSource = clsprojectpenalities.DataSet.Tables(0)
            uwgPenalities.DataBind()
        End If

        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(4), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton9_Click(sender As Object, e As System.EventArgs) Handles LinkButton9.Click
        'Penalties Back
        uwgBenfits.Rows.Clear()
        uwgBenfits.Rows.Add()
        Dim clsprojectrewards As New Clshrs_ProjectRewards(Me)
        If clsprojectrewards.Find("ProjectChangeID = " & NewChangeID.Value) Then
            uwgBenfits.DataSource = clsprojectrewards.DataSet.Tables(0)
            uwgBenfits.DataBind()
        End If

        CType(Me.UltraWebTab1.Tabs(4), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(3), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton10_Click(sender As Object, e As System.EventArgs) Handles LinkButton10.Click
        'Penalties Forward
        Dim clsprojectpenalities As New Clshrs_ProjectPenalities(Me)
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgPenalities.Rows
            Try
                clsprojectpenalities = New Clshrs_ProjectPenalities(Me)
                clsprojectpenalities.ProjectChangeID = NewChangeID.Value
                clsprojectpenalities.PenaltyID = DGRow.Cells.FromKey("PenaltyID").Value
                clsprojectpenalities.Occurance = DGRow.Cells.FromKey("Occurance").Value
                clsprojectpenalities.ExternalValue = DGRow.Cells.FromKey("ExternalValue").Value
                clsprojectpenalities.ExternalFactor = DGRow.Cells.FromKey("ExternalFactor").Value
                clsprojectpenalities.InternalValue = DGRow.Cells.FromKey("InternalValue").Value
                clsprojectpenalities.InternalFactor = DGRow.Cells.FromKey("InternalFactor").Value
                If DGRow.Cells.FromKey("ID").Value <> Nothing Then
                    clsprojectpenalities.Update("ID = " & DGRow.Cells.FromKey("ID").Value)
                Else
                    If DGRow.Cells.FromKey("PenaltyID").Value <> Nothing And DGRow.Cells.FromKey("ExternalFactor").Value <> Nothing And DGRow.Cells.FromKey("InternalFactor").Value <> Nothing Then
                        clsprojectpenalities.Save()
                    End If
                End If
            Catch ex As Exception
            End Try
        Next

        Dim str As String = "select ID,(convert(varchar(30),Code)+'-'+ArbName) AS ArbName,(convert(varchar(30),ID)+'-'+EngName) AS EngName from vw_Costcenters"
        GetList_Data(uwgLocations.DisplayLayout.Bands(0).Columns(4).ValueList, Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsprojectpenalities.ConnectionString, CommandType.Text, str))
        GetList_Data(uwgLocations.DisplayLayout.Bands(1).Columns(5).ValueList, Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsprojectpenalities.ConnectionString, CommandType.Text, str))
        uwgLocations.Rows.Clear()
        uwgLocations.Rows.Add()
        Dim clsprojectLocations As New Clshrs_ProjectLocations(Me)
        Dim clsprojectLocationsdtl As New Clshrs_ProjectLocations(Me)
        If clsprojectLocations.Find("ProjectChangeID = " & NewChangeID.Value & " and MainLocationID is null") Then
            clsprojectLocationsdtl.Find("ProjectChangeID = " & NewChangeID.Value & " and MainLocationID is not null")
            Dim DS As New DataSet()
            Dim DS2 As New DataSet()

            Dim DT1 As New DataTable("Table1")
            Dim DT2 As New DataTable("Table2")

            DT1 = clsprojectLocations.DataSet.Tables(0)
            DT2 = clsprojectLocationsdtl.DataSet.Tables(0)

            DS.Tables.Add(DT1.Copy())
            DS2.Tables.Add(DT2.Copy())

            Dim dt = DS2.Tables(0)
            DS2.Tables.Remove(dt)
            dt.TableName = "Table2"
            DS.Tables.Add(dt)

            Dim DataCol1 As Data.DataColumn
            Dim DataCol2 As Data.DataColumn
            DataCol1 = DS.Tables(0).Columns("ID")
            DataCol2 = DS.Tables(1).Columns("MainLocationID")
            Dim Rel2 As Data.DataRelation = New Data.DataRelation("Rel1", DataCol1, DataCol2, False)
            DS.Relations.Add(Rel2)

            uwgLocations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            uwgLocations.DataSource = DS
            uwgLocations.DataBind()

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgLocations.Rows
                If Not IsNothing(DGRow.Cells.FromKey("OrgID").Value) Then
                    DGRow.Cells.FromKey("Required").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
                If DGRow.HasChildRows Then
                    For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DGRow.Rows
                        If Not IsNothing(DGRow1.Cells.FromKey("OrgID").Value) Then
                            DGRow1.Cells.FromKey("Required").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                        End If
                    Next
                End If
            Next
        End If
        CType(Me.UltraWebTab1.Tabs(4), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(5), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton11_Click(sender As Object, e As System.EventArgs) Handles LinkButton11.Click
        'Locations Details Back
        uwgPenalities.Rows.Clear()
        uwgPenalities.Rows.Add()
        Dim clsprojectpenalities As New Clshrs_ProjectPenalities(Me)
        If clsprojectpenalities.Find("ProjectChangeID = " & NewChangeID.Value) Then
            uwgPenalities.DataSource = clsprojectpenalities.DataSet.Tables(0)
            uwgPenalities.DataBind()
        End If

        CType(Me.UltraWebTab1.Tabs(5), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        'CType(Me.UltraWebTab1.Tabs(2), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
        LinkButton7_Click(Nothing, Nothing)
    End Sub

    Protected Sub LinkButton12_Click(sender As Object, e As System.EventArgs) Handles LinkButton12.Click
        'Locations Details Forward
        Dim clsprojectlocation As New Clshrs_ProjectLocations(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsprojectlocation.ConnectionString)
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgLocations.Rows
            If Not Convert.ToBoolean(DGRow.Cells.FromKey("IsDel").Value) Then
                If IsNumeric(IIf(Convert.ToString(DGRow.Cells.FromKey("LinkedCS").Value) = "", "0", DGRow.Cells.FromKey("LinkedCS").Value)) Then
                    If DGRow.Cells.FromKey("Required").Value > 0 And Not Convert.ToInt32(IIf(Convert.ToString(DGRow.Cells.FromKey("LinkedCS").Value) = "", "0", DGRow.Cells.FromKey("LinkedCS").Value)) > 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Linked Accounts Required/برجاء إضافة حسابات الربط"))
                        Exit Sub
                    End If
                Else
                    If DGRow.Cells.FromKey("Required").Value > 0 And IIf(Convert.ToString(DGRow.Cells.FromKey("LinkedCS").Value) = "", "0", DGRow.Cells.FromKey("LinkedCS").Value) = "" Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Linked Accounts Required/برجاء إضافة حسابات الربط"))
                        Exit Sub
                    End If
                End If
            End If
            If DGRow.HasChildRows Then
                For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DGRow.Rows
                    If Not Convert.ToBoolean(DGRow1.Cells.FromKey("IsDel").Value) Then
                        If IsNumeric(IIf(Convert.ToString(DGRow1.Cells.FromKey("LinkedCS").Value) = "", "0", DGRow1.Cells.FromKey("LinkedCS").Value)) Then
                            If DGRow1.Cells.FromKey("Required").Value > 0 And Not Convert.ToInt32(IIf(Convert.ToString(DGRow1.Cells.FromKey("LinkedCS").Value) = "", "0", DGRow1.Cells.FromKey("LinkedCS").Value)) > 0 Then
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Linked Accounts Required/برجاء إضافة حسابات الربط"))
                                Exit Sub
                            End If
                        Else
                            If DGRow1.Cells.FromKey("Required").Value > 0 And IIf(Convert.ToString(DGRow1.Cells.FromKey("LinkedCS").Value) = "", "0", DGRow1.Cells.FromKey("LinkedCS").Value) = "" Then
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Linked Accounts Required/برجاء إضافة حسابات الربط"))
                                Exit Sub
                            End If
                        End If
                    End If
                Next
            End If
        Next
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgLocations.Rows
            If Convert.ToBoolean(DGRow.Cells.FromKey("IsDel").Value) Then
                If Not IsNothing(DGRow.Cells.FromKey("OrgID").Value) Then
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocation.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectLocations where ID = " & DGRow.Cells.FromKey("OrgID").Value & " or MainLocationID = " & DGRow.Cells.FromKey("OrgID").Value)
                    Continue For
                Else
                    Continue For
                End If
            End If

            Dim CLocation As Integer = 0
            clsprojectlocation = New Clshrs_ProjectLocations(Me)
            If Not IsNothing(DGRow.Cells.FromKey("OrgID").Value) Then
                clsprojectlocation.Find("ID = " & DGRow.Cells.FromKey("ID").Value)
            End If
            clsprojectlocation.ProjectChangeID = NewChangeID.Value
            clsprojectlocation.LocationDescription = DGRow.Cells.FromKey("LocationDescription").Value
            clsprojectlocation.LocationAddress = DGRow.Cells.FromKey("LocationAddress").Value
            clsprojectlocation.Required = DGRow.Cells.FromKey("Required").Value
            clsprojectlocation.LinkedCS = DGRow.Cells.FromKey("LinkedCS").Value
            If IsNothing(DGRow.Cells.FromKey("OrgID").Value) Then
                CLocation = clsprojectlocation.Save()
                For i As Integer = 1 To DGRow.Cells.FromKey("Required").Value
                    Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                    clsprojectplacement.PlacementCode = "Loc:" & CLocation.ToString() & "-PlaceNo:" & i.ToString()
                    clsprojectplacement.LocationID = CLocation
                    clsprojectplacement.ProjectID = Request.QueryString.Item("ProjID")
                    clsprojectplacement.ProjectChangeID = NewChangeID.Value
                    clsprojectplacement.Save()
                Next
            Else
                clsprojectlocation.Update("ID = " & DGRow.Cells.FromKey("ID").Value)
                CLocation = DGRow.Cells.FromKey("ID").Value
            End If

            If DGRow.HasChildRows Then
                For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DGRow.Rows
                    If Convert.ToBoolean(DGRow1.Cells.FromKey("IsDel").Value) Then
                        If Not IsNothing(DGRow1.Cells.FromKey("OrgID").Value) Then
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectlocation.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectLocations where ID = " & DGRow1.Cells.FromKey("OrgID").Value)
                        Else
                            Continue For
                        End If
                    End If

                    Dim CSubLocation As Integer = 0
                    clsprojectlocation = New Clshrs_ProjectLocations(Me)
                    If Not IsNothing(DGRow1.Cells.FromKey("OrgID").Value) Then
                        clsprojectlocation.Find("ID = " & DGRow1.Cells.FromKey("ID").Value)
                    End If
                    clsprojectlocation.ProjectChangeID = NewChangeID.Value
                    clsprojectlocation.MainLocationID = CLocation
                    clsprojectlocation.LocationDescription = DGRow1.Cells.FromKey("LocationDescription").Value
                    clsprojectlocation.LocationAddress = DGRow1.Cells.FromKey("LocationAddress").Value
                    clsprojectlocation.Required = DGRow1.Cells.FromKey("Required").Value
                    clsprojectlocation.LinkedCS = DGRow1.Cells.FromKey("LinkedCS").Value
                    If IsNothing(DGRow1.Cells.FromKey("OrgID").Value) Then
                        CSubLocation = clsprojectlocation.Save()
                        For i As Integer = 1 To DGRow1.Cells.FromKey("Required").Value
                            Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
                            clsprojectplacement.PlacementCode = "Loc:" & CSubLocation.ToString() & "-PlaceNo:" & i.ToString()
                            clsprojectplacement.LocationID = CSubLocation
                            clsprojectplacement.ProjectID = Request.QueryString.Item("ProjID")
                            clsprojectplacement.ProjectChangeID = NewChangeID.Value
                            clsprojectplacement.Save()
                        Next
                    Else
                        clsprojectlocation.Update("ID = " & DGRow1.Cells.FromKey("ID").Value)
                        CSubLocation = DGRow1.Cells.FromKey("ID").Value
                    End If
                Next
            End If
        Next

        Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
                cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & NewChangeID.Value & " and MainLocationID is null")
                cls_ProjectLocations.GetDropDownList(ddlSubLocation, True, "ProjectChangeID = " & NewChangeID.Value & " and MainLocationID = " & ddlLocation.SelectedValue)
                Dim clsPositions As New Clshrs_Positions(Me)
                clsPositions.GetList(uwgLocationPositions.DisplayLayout.Bands(0).Columns(2).ValueList)

                uwgLocationPositions.Rows.Clear()
                uwgLocationPositions.Rows.Add()
                Label_Cnt.Text = ""

                CType(Me.UltraWebTab1.Tabs(5), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(6), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton13_Click(sender As Object, e As System.EventArgs) Handles LinkButton13.Click
        'Positions Planning Back
        Dim clsprojectLocations As New Clshrs_ProjectLocations(Me)
        Dim str As String = "select ID,(convert(varchar(30),Code)+'-'+ArbName) AS ArbName,(convert(varchar(30),ID)+'-'+EngName) AS EngName from vw_Costcenters"
        GetList_Data(uwgLocations.DisplayLayout.Bands(0).Columns(4).ValueList, Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsprojectLocations.ConnectionString, CommandType.Text, str))
        GetList_Data(uwgLocations.DisplayLayout.Bands(1).Columns(5).ValueList, Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsprojectLocations.ConnectionString, CommandType.Text, str))
        uwgLocations.Rows.Clear()
        uwgLocations.Rows.Add()
        Dim clsprojectLocationsdtl As New Clshrs_ProjectLocations(Me)
        If clsprojectLocations.Find("ProjectChangeID = " & NewChangeID.Value & " and MainLocationID is null") Then
            clsprojectLocationsdtl.Find("ProjectChangeID = " & NewChangeID.Value & " and MainLocationID is not null")
            Dim DS As New DataSet()
            Dim DS2 As New DataSet()

            Dim DT1 As New DataTable("Table1")
            Dim DT2 As New DataTable("Table2")

            DT1 = clsprojectLocations.DataSet.Tables(0)
            DT2 = clsprojectLocationsdtl.DataSet.Tables(0)

            DS.Tables.Add(DT1.Copy())
            DS2.Tables.Add(DT2.Copy())

            Dim dt = DS2.Tables(0)
            DS2.Tables.Remove(dt)
            dt.TableName = "Table2"
            DS.Tables.Add(dt)

            Dim DataCol1 As Data.DataColumn
            Dim DataCol2 As Data.DataColumn
            DataCol1 = DS.Tables(0).Columns("ID")
            DataCol2 = DS.Tables(1).Columns("MainLocationID")
            Dim Rel2 As Data.DataRelation = New Data.DataRelation("Rel1", DataCol1, DataCol2, False)
            DS.Relations.Add(Rel2)

            uwgLocations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            uwgLocations.DataSource = DS
            uwgLocations.DataBind()

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgLocations.Rows
                If Not IsNothing(DGRow.Cells.FromKey("OrgID").Value) Then
                    DGRow.Cells.FromKey("Required").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                End If
                If DGRow.HasChildRows Then
                    For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DGRow.Rows
                        If Not IsNothing(DGRow1.Cells.FromKey("OrgID").Value) Then
                            DGRow1.Cells.FromKey("Required").AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
                        End If
                    Next
                End If
            Next
        End If

        CType(Me.UltraWebTab1.Tabs(6), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(5), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton14_Click(sender As Object, e As System.EventArgs) Handles LinkButton14.Click
        'Positions Planning Forward
        Dim clsprojectplacement As New Clshrs_ProjectPlacements(Me)
        Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(clsprojectplacement.ConnectionString)
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsprojectplacement.ConnectionString, Data.CommandType.Text, "delete from hrs_ProjectPlacements where RegComputerID = 1 and LocationDetailID is null")
        If clsprojectplacement.Find("ProjectChangeID = " & NewChangeID.Value & " and LocationDetailID is null and RegComputerID is null") Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Invalid Position Distribution/التوزيع غير ملائم"))
            Exit Sub
        End If

        Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
        Dim AttendanceTable As New Clshrs_AttendanceTable(Me)
        cls_ProjectLocations.GetDropDownList(ddlLocation1, True, "ProjectChangeID = " & NewChangeID.Value & " and MainLocationID is null")
        cls_ProjectLocations.GetDropDownList(ddlsublocation1, True, "ProjectChangeID = " & NewChangeID.Value & " and MainLocationID = " & ddlLocation1.SelectedValue)
        AttendanceTable.GetDropDownList(ddlAttendancetable, True)

        Dim Cloc As Integer = 0
        If ddlLocation1.SelectedValue > 0 Then
            Cloc = ddlLocation1.SelectedValue
        End If
        If ddlsublocation1.SelectedValue > 0 Then
            Cloc = ddlsublocation1.SelectedValue
        End If
        Dim clsPositions As New Clshrs_Positions(Me)
        clsPositions.GetDropDownListForProjects(ddlPosition, True, "B.LocationID = " & Cloc)
        uwglocationshift.Rows.Clear()
        LblCnt1.Text = ""

        CType(Me.UltraWebTab1.Tabs(6), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(7), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton15_Click(sender As Object, e As System.EventArgs) Handles LinkButton15.Click
        'Attendance Planning Back
        Dim cls_ProjectLocations As New Clshrs_ProjectLocations(Me)
        cls_ProjectLocations.GetDropDownList(ddlLocation, True, "ProjectChangeID = " & NewChangeID.Value & " and MainLocationID is null")
        cls_ProjectLocations.GetDropDownList(ddlSubLocation, True, "ProjectChangeID = " & NewChangeID.Value & " and MainLocationID = " & ddlLocation.SelectedValue)
        Dim clsPositions As New Clshrs_Positions(Me)
        clsPositions.GetList(uwgLocationPositions.DisplayLayout.Bands(0).Columns(2).ValueList)

        uwgLocationPositions.Rows.Clear()
        uwgLocationPositions.Rows.Add()
        Label_Cnt.Text = ""

        CType(Me.UltraWebTab1.Tabs(7), Infragistics.WebUI.UltraWebTab.Tab).Enabled = False
        CType(Me.UltraWebTab1.Tabs(6), Infragistics.WebUI.UltraWebTab.Tab).Enabled = True
    End Sub

    Protected Sub LinkButton16_Click(sender As Object, e As System.EventArgs) Handles LinkButton16.Click
        'Attendance Planning Finish
        Dim hrsProjectChanges As New Clshrs_ProjectChanges(Me)
        If hrsProjectChanges.Find("ID = " & NewChangeID.Value) Then
            Dim hrsProjectLocationDetails As New Clshrs_ProjectLocationDetails(Me)
            Dim ClsNavHandler As New Venus.Shared.Web.NavigationHandler(hrsProjectLocationDetails.ConnectionString)
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, "select ID,Qty,isnull((select sum(Qty) from hrs_ProjectLocationShifts where LocationDetailID = hrs_ProjectLocationDetails.ID),0) AS AttShifts,(select COUNT(ID) from hrs_ProjectPlacements where isnull(RegComputerID,0) = 0 and LocationDetailID = hrs_ProjectLocationDetails.ID and ID not in (select PlacementID from hrs_ProjectPlacementPlanning)) AS AttPlanning from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & NewChangeID.Value & ")")
            If ds.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In ds.Tables(0).Rows
                    If dr(2) = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Attendance Plan Uncompleted/مخطط الدوامات غير مكتمل"))
                        Exit Sub
                    ElseIf dr(3) > 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "Attendance Tables Uncompleted/جداول الدوامات غير مكتمل"))
                        Exit Sub
                    End If
                Next

                hrsProjectChanges.RegComputerID = 1
                Dim str As String = "update hrs_ProjectPlacementEmployees set ToDate = (select (select Dateadd(DAY,-1,FromDate) from hrs_ProjectChanges where ID = " & NewChangeID.Value & ")) where (ToDate is null or ToDate >= (select (select FromDate from hrs_ProjectChanges where ID = " & NewChangeID.Value & "))) and PlacementCode in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & CCHangeID.Value & "))) and PlacementCode not in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & NewChangeID.Value & ")))"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, str)
                str = "set dateformat dmy; delete from Att_AttendTransactions where (select PlacementCode from hrs_ProjectPlacements where hrs_ProjectPlacements.ID = Att_AttendTransactions.RegComputerID) in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & CCHangeID.Value & "))) and (select PlacementCode from hrs_ProjectPlacements where hrs_ProjectPlacements.ID = Att_AttendTransactions.RegComputerID) not in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & NewChangeID.Value & "))) and TrnsDatetime >= (select (select FromDate from hrs_ProjectChanges where ID = " & NewChangeID.Value & "))"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, str)


                hrsProjectChanges.Update("ID = " & NewChangeID.Value)
                If ToNewChangeID.Value <> "0" Then
                    hrsProjectChanges = New Clshrs_ProjectChanges(Me)
                    hrsProjectChanges.Find("ID = " & ToNewChangeID.Value)
                    hrsProjectChanges.RegComputerID = 1

                    str = "update hrs_ProjectPlacementEmployees set ToDate = (select (select Dateadd(DAY,-1,FromDate) from hrs_ProjectChanges where ID = " & ToNewChangeID.Value & ")) where (ToDate is null or ToDate >= (select (select FromDate from hrs_ProjectChanges where ID = " & ToNewChangeID.Value & "))) and PlacementCode in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & NewChangeID.Value & "))) and PlacementCode not in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & ToNewChangeID.Value & ")))"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, str)
                    str = "set dateformat dmy; delete from Att_AttendTransactions where (select PlacementCode from hrs_ProjectPlacements where hrs_ProjectPlacements.ID = Att_AttendTransactions.RegComputerID) in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & NewChangeID.Value & "))) and (select PlacementCode from hrs_ProjectPlacements where hrs_ProjectPlacements.ID = Att_AttendTransactions.RegComputerID) not in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & ToNewChangeID.Value & "))) and TrnsDatetime >= (select (select FromDate from hrs_ProjectChanges where ID = " & ToNewChangeID.Value & "))"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, str)

                    hrsProjectChanges.Update("ID = " & ToNewChangeID.Value)
                End If
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "The Process Done/تم تنفيذ العملية بنجاح"))
            Else
                If ToNewChangeID.Value <> "0" Then
                    hrsProjectChanges.RegComputerID = 1
                    Dim str As String = "update hrs_ProjectPlacementEmployees set ToDate = (select (select Dateadd(DAY,-1,FromDate) from hrs_ProjectChanges where ID = " & NewChangeID.Value & ")) where (ToDate is null or ToDate >= (select (select FromDate from hrs_ProjectChanges where ID = " & NewChangeID.Value & "))) and PlacementCode in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & CCHangeID.Value & "))) and PlacementCode not in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & NewChangeID.Value & ")))"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, str)
                    str = "set dateformat dmy; delete from Att_AttendTransactions where (select PlacementCode from hrs_ProjectPlacements where hrs_ProjectPlacements.ID = Att_AttendTransactions.RegComputerID) in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & CCHangeID.Value & "))) and (select PlacementCode from hrs_ProjectPlacements where hrs_ProjectPlacements.ID = Att_AttendTransactions.RegComputerID) not in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & NewChangeID.Value & "))) and TrnsDatetime >= (select (select FromDate from hrs_ProjectChanges where ID = " & NewChangeID.Value & "))"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, str)


                    hrsProjectChanges.Update("ID = " & NewChangeID.Value)
                    If ToNewChangeID.Value <> "0" Then
                        hrsProjectChanges = New Clshrs_ProjectChanges(Me)
                        hrsProjectChanges.Find("ID = " & ToNewChangeID.Value)
                        hrsProjectChanges.RegComputerID = 1

                        str = "update hrs_ProjectPlacementEmployees set ToDate = (select (select Dateadd(DAY,-1,FromDate) from hrs_ProjectChanges where ID = " & ToNewChangeID.Value & ")) where (ToDate is null or ToDate >= (select (select FromDate from hrs_ProjectChanges where ID = " & ToNewChangeID.Value & "))) and PlacementCode in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & NewChangeID.Value & "))) and PlacementCode not in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & ToNewChangeID.Value & ")))"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, str)
                        str = "set dateformat dmy; delete from Att_AttendTransactions where (select PlacementCode from hrs_ProjectPlacements where hrs_ProjectPlacements.ID = Att_AttendTransactions.RegComputerID) in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & NewChangeID.Value & "))) and (select PlacementCode from hrs_ProjectPlacements where hrs_ProjectPlacements.ID = Att_AttendTransactions.RegComputerID) not in (select PlacementCode from hrs_ProjectPlacements where LocationDetailID in (select ID from hrs_ProjectLocationDetails where LocationID in (select ID from hrs_ProjectLocations where ProjectChangeID = " & ToNewChangeID.Value & "))) and TrnsDatetime >= (select (select FromDate from hrs_ProjectChanges where ID = " & ToNewChangeID.Value & "))"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsProjectLocationDetails.ConnectionString, Data.CommandType.Text, str)

                        hrsProjectChanges.Update("ID = " & ToNewChangeID.Value)
                    End If
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavHandler.SetLanguage(Page, "The Process Done/تم تنفيذ العملية بنجاح"))
                End If
            End If
        End If
    End Sub
End Class
