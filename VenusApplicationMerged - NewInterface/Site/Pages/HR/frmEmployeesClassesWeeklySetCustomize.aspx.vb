Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Partial Class frmEmployeesClassesWeeklySetCustomize
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesClassesWeeklyCustomize As Clshrs_EmployeesClassesWeeklyCustomize
    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployeesClassesWeeklyCustomize = New Clshrs_EmployeesClassesWeeklyCustomize(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsEmployeesClassesWeeklyCustomize.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            Dim clsEmployee As New Clshrs_Employees(Page)
            Dim ClsObjects2 As New Clssys_Objects(Page)
            Dim ClsSearchs2 As New Clssys_Searchs(Page)
            If ClsObjects2.Find(" Code='" & clsEmployee.Table.Trim & "'") Then
                If ClsSearchs2.Find(" ObjectID='" & ClsObjects2.ID & "'") Then
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & EmployeeCodeTextBox.ID & "&SearchID=" & ClsSearchs2.ID & "&'," & IntDimension & ",720,false,'" & EmployeeCodeTextBox.ClientID & "'"
                    EmployeeCodeImageButton.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsEmployeesClassesWeeklyCustomize.ConnectionString)
                ClsEmployeesClassesWeeklyCustomize.AddOnChangeEventToControls("frmAttendShifts", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                Dim clsAttendShifts As New ClsAtt_AttendShifts(Page)
                clsAttendShifts.GetDropDownList(ddlEmployeeClass, True)

                Dim clsenum As New Clshrs_Enum(Me)
                clsenum.GetList(uwgEmployeesClassesCalenderSetCustomizeDays.DisplayLayout.Bands(0).Columns(2).ValueList, False, "Flag = 4")

                ddlEmployeeClass.Enabled = False
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsEmployeesClassesWeeklyCustomize.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsEmployeesClassesWeeklyCustomize.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesClassesWeeklyCustomize.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsEmployeesClassesWeeklyCustomize = New Clshrs_EmployeesClassesWeeklyCustomize(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesClassesWeeklyCustomize.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                If CheckEmptyDateValidation() = False Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Start date and end date must be selected / يجب تحديد تاريخ البداية وتاريخ النهاية "))
                    Exit Sub
                End If

                If CheckDateValidation() = False Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " End date must be more than start date / تاريخ النهاية يجب أن يكون أكبر من تاريخ البداية "))
                    Exit Sub
                End If

                If CheckTimesBlank() = False Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Shift Time is Blank /توقيت بداية أو نهاية الدوام الأول لايمكن ان يكون فارغ لأيام الدوام"))
                    Exit Sub
                End If

                If PlanRadioButton.Checked = True And ddlEmployeeClass.SelectedIndex = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Select The Attendance Plan  /حدد خطة الدوام"))
                    Exit Sub
                End If

                If EmployeeRadioButton.Checked = True And EmployeeCodeTextBox.Text = Nothing Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You must select an employee  /يجب اختيار موظف"))
                    Exit Sub
                End If

                ClsEmployeesClassesWeeklyCustomize.Find("Code='" & txtCode.Text & "'")
                If CheckCustomizeValid(ddlEmployeeClass.SelectedValue, StartDate.Value, EndDate.Value) <> ClsEmployeesClassesWeeklyCustomize.ID Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in Shift Times  /يوجد تخصيص للتقويم اخر للفئة بنفس الفترة"))
                    Exit Sub
                End If
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsEmployeesClassesWeeklyCustomize.ID > 0 Then
                    ClsEmployeesClassesWeeklyCustomize.Update("code='" & txtCode.Text & "'")
                Else
                    ClsEmployeesClassesWeeklyCustomize.Save()
                End If
                ClsEmployeesClassesWeeklyCustomize.Find("Code='" & txtCode.Text & "'")
                If ClsEmployeesClassesWeeklyCustomize.ID > 0 Then
                    If (SaveDG(ClsEmployeesClassesWeeklyCustomize.ID)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployeesClassesWeeklyCustomize.Table, ClsEmployeesClassesWeeklyCustomize.ID)
                        value.Text = ""
                    End If
                End If
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployeesClassesWeeklyCustomize.Table, ClsEmployeesClassesWeeklyCustomize.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                If CheckEmptyDateValidation() = False Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Start date and end date must be selected / يجب تحديد تاريخ البداية وتاريخ النهاية "))
                    Exit Sub
                End If

                If CheckDateValidation() = False Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " End date must be more than start date / تاريخ النهاية يجب أن يكون أكبر من تاريخ البداية "))
                    Exit Sub
                End If

                If CheckTimesBlank() = False Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Shift Time is Blank /توقيت بداية أو نهاية الدوام الأول لايمكن ان يكون فارغ لأيام الدوام"))
                    Exit Sub
                End If

                If PlanRadioButton.Checked = True And ddlEmployeeClass.SelectedIndex = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Select The Attendance Plan  /حدد خطة الدوام"))
                    Exit Sub
                End If

                If EmployeeRadioButton.Checked = True And EmployeeCodeTextBox.Text = Nothing Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You must select an employee  /يجب اختيار موظف"))
                    Exit Sub
                End If
                ClsEmployeesClassesWeeklyCustomize.Find("Code='" & txtCode.Text & "'")
                If CheckCustomizeValid(ddlEmployeeClass.SelectedValue, StartDate.Value, EndDate.Value) <> ClsEmployeesClassesWeeklyCustomize.ID Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in Shift Times  /يوجد تخصيص للتقويم اخر للفئة بنفس الفترة"))
                    Exit Sub
                End If


                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsEmployeesClassesWeeklyCustomize.ID > 0 Then
                    ClsEmployeesClassesWeeklyCustomize.Update("code='" & txtCode.Text & "'")
                Else
                    ClsEmployeesClassesWeeklyCustomize.Save()
                End If

                ClsEmployeesClassesWeeklyCustomize.Find("Code='" & txtCode.Text & "'")

                If ClsEmployeesClassesWeeklyCustomize.ID > 0 Then
                    If (SaveDG(ClsEmployeesClassesWeeklyCustomize.ID)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployeesClassesWeeklyCustomize.Table, ClsEmployeesClassesWeeklyCustomize.ID)
                        value.Text = ""
                    End If
                End If
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsEmployeesClassesWeeklyCustomize.Delete("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Current Record has cancel  /تم إلغاء السجل الحالي "))
                AfterOperation()
            Case "Property"
                ClsEmployeesClassesWeeklyCustomize.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEmployeesClassesWeeklyCustomize.ID & "&TableName=" & ClsEmployeesClassesWeeklyCustomize.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsEmployeesClassesWeeklyCustomize.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEmployeesClassesWeeklyCustomize.ID & "&TableName=" & ClsEmployeesClassesWeeklyCustomize.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEmployeesClassesWeeklyCustomize.Table
                ClsEmployeesClassesWeeklyCustomize.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsEmployeesClassesWeeklyCustomize.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEmployeesClassesWeeklyCustomize.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEmployeesClassesWeeklyCustomize.Find(" Code= '" & txtCode.Text & "'")
                If ClsEmployeesClassesWeeklyCustomize.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsEmployeesClassesWeeklyCustomize.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsEmployeesClassesWeeklyCustomize.CheckDiff(ClsEmployeesClassesWeeklyCustomize, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsEmployeesClassesWeeklyCustomize.FirstRecord()
                GetValues()
            Case "Previous"
                ClsEmployeesClassesWeeklyCustomize.Find(" code = '" & txtCode.Text & "'")
                If Not ClsEmployeesClassesWeeklyCustomize.previousRecord() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                Else
                    GetValues()
                End If
            Case "Next"
                ClsEmployeesClassesWeeklyCustomize.Find(" code = '" & txtCode.Text & "'")
                If Not ClsEmployeesClassesWeeklyCustomize.NextRecord() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                Else
                    GetValues()
                End If
            Case "Last"
                ClsEmployeesClassesWeeklyCustomize.Find(" code = '" & txtCode.Text & "'")
                ClsEmployeesClassesWeeklyCustomize.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub EmployeeCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmployeeCodeTextBox.TextChanged
        CheckEmployeeCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function SaveDG(ByVal CalenderSetCustomizeID As Integer) As Boolean
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsEmployeesClassesWeeklyCustomizeDays As New Clshrs_EmployeesClassesWeeklyCustomizeDays(Page)
        Dim Parameter As String
        Dim ParameterValues As String
        Try
            If CalenderSetCustomizeID > 0 Then
                Dim str As String
                Dim TimeIn1 As Object
                Dim TimeOut1 As Object
                Dim TimeIn2 As Object
                Dim TimeOut2 As Object
                Dim DayNo As Object
                Dim IsDayOff As Object
                Dim RegUserID As Object

                str = "DELETE FROM hrs_EmployeesClassesWeeklyCustomizeDays WHERE CustomizeID = " & CalenderSetCustomizeID & ";"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeesClassesWeeklyCustomize.ConnectionString, Data.CommandType.Text, str)

                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeesClassesCalenderSetCustomizeDays.Rows()
                    If (DGRow.Cells.FromKey("TimeIn1").Value <> Nothing And DGRow.Cells.FromKey("TimeOut1").Value <> Nothing) Then
                        ClsEmployeesClassesWeeklyCustomizeDays.TimeIn1 = DGRow.Cells.FromKey("TimeIn1").Value.ToString()
                        ClsEmployeesClassesWeeklyCustomizeDays.TimeOut1 = DGRow.Cells.FromKey("TimeOut1").Value.ToString()
                        If DGRow.Cells.FromKey("FirstShiftTimeInFingerprintStart").Value = Nothing Then
                            Dim x = DGRow.Cells.FromKey("FirstShiftTimeInFingerprintStart").Value
                        End If

                        ClsEmployeesClassesWeeklyCustomizeDays.FirstShiftTimeInFingerprintStart = IIf(DGRow.Cells.FromKey("FirstShiftTimeInFingerprintStart").Value <> Nothing, DGRow.Cells.FromKey("FirstShiftTimeInFingerprintStart").Value, Nothing)
                        ClsEmployeesClassesWeeklyCustomizeDays.FirstShiftEntryTimeInClose = IIf(DGRow.Cells.FromKey("FirstShiftEntryTimeInClose").Value <> Nothing, DGRow.Cells.FromKey("FirstShiftEntryTimeInClose").Value, Nothing)
                        ClsEmployeesClassesWeeklyCustomizeDays.FirstShiftTimeOutFingerprintClose = IIf(DGRow.Cells.FromKey("FirstShiftTimeOutFingerprintClose").Value <> Nothing, DGRow.Cells.FromKey("FirstShiftTimeOutFingerprintClose").Value, Nothing)

                        If (DGRow.Cells.FromKey("TimeIn2").Value <> Nothing And DGRow.Cells.FromKey("TimeOut2").Value <> Nothing) Then
                                ClsEmployeesClassesWeeklyCustomizeDays.TimeIn2 = DGRow.Cells.FromKey("TimeIn2").Value.ToString()
                                ClsEmployeesClassesWeeklyCustomizeDays.TimeOut2 = DGRow.Cells.FromKey("TimeOut2").Value.ToString()
                            ClsEmployeesClassesWeeklyCustomizeDays.SecondShiftTimeInFingerprintStart = IIf(DGRow.Cells.FromKey("SecondShiftTimeInFingerprintStart").Value <> Nothing, DGRow.Cells.FromKey("SecondShiftTimeInFingerprintStart").Value, Nothing)
                            ClsEmployeesClassesWeeklyCustomizeDays.SecondShiftEntryTimeInClose = IIf(DGRow.Cells.FromKey("SecondShiftEntryTimeInClose").Value <> Nothing, DGRow.Cells.FromKey("SecondShiftEntryTimeInClose").Value, Nothing)
                            ClsEmployeesClassesWeeklyCustomizeDays.SecondShiftTimeOutFingerprintClose = IIf(DGRow.Cells.FromKey("SecondShiftTimeOutFingerprintClose").Value <> Nothing, DGRow.Cells.FromKey("SecondShiftTimeOutFingerprintClose").Value, Nothing)

                        End If

                            ClsEmployeesClassesWeeklyCustomizeDays.DayNo = Convert.ToInt32(DGRow.Cells.FromKey("DayNo").Value)
                            ClsEmployeesClassesWeeklyCustomizeDays.IsDayOff = IIf(CBool(DGRow.Cells.FromKey("IsDayOff").Value) = True, 1, 0)
                            ClsEmployeesClassesWeeklyCustomizeDays.RegUserID = ClsEmployeesClassesWeeklyCustomize.DataBaseUserRelatedID
                            ClsEmployeesClassesWeeklyCustomizeDays.CustomizeID = ClsEmployeesClassesWeeklyCustomize.ID

                            ClsEmployeesClassesWeeklyCustomizeDays.Save()
                        ElseIf DGRow.Cells.FromKey("IsDayOff").Value = True Then
                            ClsEmployeesClassesWeeklyCustomizeDays.TimeIn1 = ""
                        ClsEmployeesClassesWeeklyCustomizeDays.TimeOut1 = ""
                        ClsEmployeesClassesWeeklyCustomizeDays.DayNo = Convert.ToInt32(DGRow.Cells.FromKey("DayNo").Value)
                        ClsEmployeesClassesWeeklyCustomizeDays.IsDayOff = IIf(CBool(DGRow.Cells.FromKey("IsDayOff").Value) = True, 1, 0)
                        ClsEmployeesClassesWeeklyCustomizeDays.RegUserID = ClsEmployeesClassesWeeklyCustomize.DataBaseUserRelatedID
                        ClsEmployeesClassesWeeklyCustomizeDays.CustomizeID = ClsEmployeesClassesWeeklyCustomize.ID
                        ClsEmployeesClassesWeeklyCustomizeDays.Save()
                    End If
                Next

                Return True
            End If

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function CheckTimesValid() As Boolean
        Try
            Dim ErrorCount As Integer = 0

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeesClassesCalenderSetCustomizeDays.Rows()
                If CBool(DGRow.Cells.FromKey("IsDayOff").Value = False) Then
                    If CDate(DGRow.Cells.FromKey("TimeIn1").Value) > (DGRow.Cells.FromKey("TimeOut1").Value) Then
                        ErrorCount = ErrorCount + 1
                    End If
                    If DGRow.Cells.FromKey("TimeIn2").Value = "" And DGRow.Cells.FromKey("TimeOut2").Value <> "" Then
                        ErrorCount = ErrorCount + 1
                    End If
                    If DGRow.Cells.FromKey("BreakFrom").Value <> "" And DGRow.Cells.FromKey("BreakTo").Value = "" Then
                        ErrorCount = ErrorCount + 1
                    End If
                    If DGRow.Cells.FromKey("BreakFrom").Value <> "" And DGRow.Cells.FromKey("BreakTo").Value <> "" Then
                        If CDate(DGRow.Cells.FromKey("BreakFrom").Value) > (DGRow.Cells.FromKey("BreakTo").Value) Then
                            ErrorCount = ErrorCount + 1
                        End If

                        If CDate(DGRow.Cells.FromKey("BreakFrom").Value) < (DGRow.Cells.FromKey("TimeIn").Value) Then
                            ErrorCount = ErrorCount + 1
                        End If
                        If CDate(DGRow.Cells.FromKey("BreakTo").Value) > (DGRow.Cells.FromKey("TimeOut").Value) Then
                            ErrorCount = ErrorCount + 1
                        End If
                    End If

                End If
            Next
            If (ErrorCount = 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function CheckCustomizeValid(ByVal ClassID As Integer, ByVal Fromdate As Date, ByVal Todate As Date) As Integer
        Dim ClsEmployeesClassesWeeklyCustomize As New Clshrs_EmployeesClassesWeeklyCustomize(Page)
        Dim ErrorCount As Integer = 0
        If ClsEmployeesClassesWeeklyCustomize.Find("EmployeeClassID = " & ClassID & " AND  ((CONVERT(date,'" & Fromdate.ToString("dd/MM/yyyy") & "',103) Between  CONVERT(date,beginDate,103) and CONVERT(date,EndDate,103)) Or(CONVERT(date,'" & Todate.ToString("dd/MM/yyyy") & "',103) Between  CONVERT(date,beginDate,103) and CONVERT(date,EndDate,103)))") Then
            Return ClsEmployeesClassesWeeklyCustomize.ID
        Else
            Return 0
        End If
    End Function
    Private Function CheckTimesBlank() As Boolean
        Try
            Dim ShiftCount As Integer = 0
            Dim EmptyShiftCount As Integer = 0

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeesClassesCalenderSetCustomizeDays.Rows()
                If CBool(DGRow.Cells.FromKey("IsDayOff").Value = False) Then
                    ShiftCount = ShiftCount + 1

                    Dim TimeIn1 = IIf(DGRow.Cells.FromKey("TimeIn1").Value <> Nothing, DGRow.Cells.FromKey("TimeIn1").Value, Date.MinValue)
                    Dim TimeIn2 = IIf(DGRow.Cells.FromKey("TimeOut1").Value <> Nothing, DGRow.Cells.FromKey("TimeOut1").Value, Date.MinValue)

                    If TimeIn1 = Date.MinValue Or TimeIn2 = Date.MinValue Then
                        EmptyShiftCount = EmptyShiftCount + 1
                    End If
                End If

            Next
            If (ShiftCount > EmptyShiftCount) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function CheckEmptyDateValidation() As Boolean
        If (StartDate.Value = Nothing Or EndDate.Value = Nothing) Then
            Return False
        End If

        Return True
    End Function
    Private Function CheckDateValidation() As Boolean
        If (StartDate.Value > EndDate.Value) Then
            Return False
        End If

        Return True
    End Function
    Private Function AssignValues() As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Try
            If (EmployeeCodeTextBox.Text <> Nothing) Then
                ClsEmployees.Find("Code = " & EmployeeCodeTextBox.Text)
            End If

            With ClsEmployeesClassesWeeklyCustomize
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .BeginDate = StartDate.Value
                .EndDate = EndDate.Value
                If ddlEmployeeClass.SelectedValue > 0 Then
                    .EmployeeClassID = ddlEmployeeClass.SelectedValue
                End If
                .EmployeeID = ClsEmployees.ID

            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsUser.ConnectionString)
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsEmployeesClassesWeeklyCustomize
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                StartDate.Value = .BeginDate
                EndDate.Value = .EndDate
                ddlEmployeeClass.SelectedValue = .EmployeeClassID
                EmployeeCodeTextBox.Text = ClsEmployees.Code
                EmpEngNameTextBox.Text = ClsEmployees.EngName
                EmpArbNameTextBox.Text = ClsEmployees.ArbName
                Dim ClsEmployeesClassesWeeklyCustomizeDays As New Clshrs_EmployeesClassesWeeklyCustomizeDays(Me)
                ClsEmployeesClassesWeeklyCustomizeDays.Find("CustomizeID = " & ClsEmployeesClassesWeeklyCustomize.ID)
                uwgEmployeesClassesCalenderSetCustomizeDays.DataSource = ClsEmployeesClassesWeeklyCustomizeDays.DataSet.Tables(0)
                uwgEmployeesClassesCalenderSetCustomizeDays.DataBind()
            End With

            If Not ClsEmployeesClassesWeeklyCustomize.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEmployeesClassesWeeklyCustomize.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsEmployeesClassesWeeklyCustomize.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsEmployeesClassesWeeklyCustomize.RegDate).Date
            End If
            If ClsEmployeesClassesWeeklyCustomize.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsEmployeesClassesWeeklyCustomize.CancelDate).Date
            End If
            If Not ClsEmployeesClassesWeeklyCustomize.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()


            If (ClsEmployeesClassesWeeklyCustomize.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsEmployeesClassesWeeklyCustomize.ConnectionString, ClsEmployeesClassesWeeklyCustomize.DataBaseUserRelatedID, ClsEmployeesClassesWeeklyCustomize.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployeesClassesWeeklyCustomize.ConnectionString, ClsEmployeesClassesWeeklyCustomize.DataBaseUserRelatedID, ClsEmployeesClassesWeeklyCustomize.GroupID, ClsEmployeesClassesWeeklyCustomize.Table, ClsEmployeesClassesWeeklyCustomize.ID)
            If Not ClsEmployeesClassesWeeklyCustomize.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsEmployeesClassesWeeklyCustomize.ID)
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
                    ClsEmployeesClassesWeeklyCustomize.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsEmployeesClassesWeeklyCustomize.Find("ID=" & intID)
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
            With ClsEmployeesClassesWeeklyCustomize
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
        ClsEmployeesClassesWeeklyCustomize = New Clshrs_EmployeesClassesWeeklyCustomize(Me)
        Try
            With ClsEmployeesClassesWeeklyCustomize
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
        ClsEmployeesClassesWeeklyCustomize = New Clshrs_EmployeesClassesWeeklyCustomize(Me)
        If IntId > 0 Then
            ClsEmployeesClassesWeeklyCustomize.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployeesClassesWeeklyCustomize = New Clshrs_EmployeesClassesWeeklyCustomize(Me)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesClassesWeeklyCustomize.ConnectionString)
        Try
            ClsEmployeesClassesWeeklyCustomize.Find("Code='" & txtCode.Text & "'")
            IntId = ClsEmployeesClassesWeeklyCustomize.ID
            txtEngName.Focus()
            If ClsEmployeesClassesWeeklyCustomize.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsEmployeesClassesWeeklyCustomize.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                txtEngName.Focus()
                CreateOtherFields(0)
                uwgEmployeesClassesCalenderSetCustomizeDays.Rows.Clear()
                For i As Integer = 1 To 7
                    uwgEmployeesClassesCalenderSetCustomizeDays.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Nothing, i}))
                Next i
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsEmployeesClassesWeeklyCustomize.ConnectionString, ClsEmployeesClassesWeeklyCustomize.DataBaseUserRelatedID, ClsEmployeesClassesWeeklyCustomize.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployeesClassesWeeklyCustomize.ConnectionString, ClsEmployeesClassesWeeklyCustomize.DataBaseUserRelatedID, ClsEmployeesClassesWeeklyCustomize.GroupID, ClsEmployeesClassesWeeklyCustomize.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function CheckEmployeeCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = 0
        Dim ClsEmployees = New Clshrs_Employees(Me)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try
            ClsEmployees.Find("Code='" & EmployeeCodeTextBox.Text & "'")
            IntId = ClsEmployees.ID
            EmpEngNameTextBox.Focus()
            If ClsEmployees.ID > 0 Then
                EmpEngNameTextBox.Text = ClsEmployees.EngName
                EmpArbNameTextBox.Text = ClsEmployees.ArbName
            Else
                If ClsEmployees.CheckRecordExistance(" Code='" & EmpEngNameTextBox.Text & "'") Then
                    EmpEngNameTextBox.Text = ""
                    EmpArbNameTextBox.Text = ""
                    EmpEngNameTextBox.Focus()
                End If
                EmployeeCodeTextBox.Text = ""
                EmpEngNameTextBox.Text = ""
                EmpArbNameTextBox.Text = ""
                EmpEngNameTextBox.Focus()
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
        ClsEmployeesClassesWeeklyCustomize.Clear()
        uwgEmployeesClassesCalenderSetCustomizeDays.Clear()
        GetValues()
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
        EmpEngNameTextBox.Text = String.Empty
        EmpArbNameTextBox.Text = String.Empty
        EmployeeCodeTextBox.Text = String.Empty
        uwgEmployeesClassesCalenderSetCustomizeDays.Rows.Clear()

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEmployeesClassesWeeklyCustomize = New Clshrs_EmployeesClassesWeeklyCustomize(Page)
        ClsEmployeesClassesWeeklyCustomize.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsEmployeesClassesWeeklyCustomize.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployeesClassesWeeklyCustomize.Table) = True Then
            Dim StrTablename As String
            ClsEmployeesClassesWeeklyCustomize = New Clshrs_EmployeesClassesWeeklyCustomize(Me)
            StrTablename = ClsEmployeesClassesWeeklyCustomize.Table
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
    Protected Sub AttCustomizeOpts_CheckedChanged(sender As Object, e As EventArgs)
        If (EmployeeRadioButton.Checked) Then
            ddlEmployeeClass.Enabled = False
            EmployeeCodeTextBox.Enabled = True
            EmpEngNameTextBox.Enabled = True
            EmpArbNameTextBox.Enabled = True

        End If

        If (PlanRadioButton.Checked) Then
            EmployeeCodeTextBox.Enabled = False
            EmpEngNameTextBox.Enabled = False
            EmpArbNameTextBox.Enabled = False
            ddlEmployeeClass.Enabled = True
        End If
    End Sub
#End Region
End Class
