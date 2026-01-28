Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmCalendar
    Inherits MainPage
#Region "Public Decleration"
    Dim clsEmployeesClassCalander As Clshrs_EmployeesClassCalander
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")


        Dim clsClasses As New Clshrs_EmployeeClasses(Page)
        clsEmployeesClassCalander = New Clshrs_EmployeesClassCalander(Me.Page)

        uwgDays.BorderWidth = 0
        If Not IsPostBack Then

            cldDateSelector.SelectedDate = clsClasses.GetHigriDate(Date.Now)

            SetDaysList()
            clsEmployeesClassCalander.GetDropDownList(ddlEmployeeClass, True)

        End If
        rbtnSelectedDateType.Attributes.Add("OnClick", "rbtnSelectedDateTypeChanged()")

    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_SaveN.Command, ImageButton_Print.Command, ImageButton_Delete.Command, LinkButton_SaveN.Command, LinkButton_Delete.Command, LinkButton_Print.Command
        clsEmployeesClassCalander = New Clshrs_EmployeesClassCalander(Page)
        Dim clsEmployeeCalenderSet As Clshrs_EmployeesClassesCalenderSet
        Dim intEmployeeClass As Integer
        Dim clsEmployeeClass As New Clshrs_EmployeeClasses(Page)
        Dim strFilter As String = String.Empty
        Dim dteSelectedDate As Date = clsEmployeeClass.SetHigriDate(cldDateSelector.SelectedDate.Date)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployeesClassCalander.ConnectionString)

        clsEmployeeClass.Find(" ID=" & ddlEmployeeClass.SelectedValue)
        If (clsEmployeeClass.ID <= 0) Then
            intEmployeeClass = 0
        Else
            intEmployeeClass = ddlEmployeeClass.SelectedValue
        End If

        Select Case e.CommandArgument
            Case "SaveNew"
                If ddlEmployeeClass.SelectedIndex = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please Select Employee Class/الرجاء تحديد فئة"))
                    Exit Sub
                End If

                SaveCalenderDaySet()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
            Case "Delete"
                Dim index As Integer = 0

                For index = 0 To uwgDays.Rows.Count - 1
                    Dim currRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow = uwgDays.Rows(index)
                    Dim dayVal As Integer = currRow.Cells(3).Value
                    If (Not currRow.Cells(0).Value) Then
                        Continue For
                    End If
                    clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                    clsEmployeeCalenderSet.Delete(" hrs_EmployeesClassesCalenderSet.DayNumber = " & dayVal & " AND hrs_EmployeesClassesCalenderSet.EmployeeClassID = " & intEmployeeClass & " ")

                Next

                '// User Will Delete current row(s) From DataBase 
                If (cldDateSelector.SelectedDate.Date <> Nothing) Then
                    Dim filter As String = String.Empty
                    filter = fnPrePareDate(dteSelectedDate)
                    clsEmployeesClassCalander.fnDelete(filter, ddlEmployeeClass.SelectedItem.Value)
                    SetReturnback()
                End If
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Deleted the settings for this class /تم حذف الاعدادات لهذه الفئة"))

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
        End Select
    End Sub
    Protected Sub cldDateSelector_ValueChanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebSchedule.ValueChangedEventArgs) Handles cldDateSelector.ValueChanged
        ClearAllShifts()
        clsEmployeesClassCalander = New Clshrs_EmployeesClassCalander(Page)
        Dim filter As String = String.Empty
        Dim dteSelectedDate As Date = clsEmployeesClassCalander.SetHigriDate(cldDateSelector.SelectedDate.Date)
        filter = fnPrePareDate(dteSelectedDate)

        clsEmployeesClassCalander.fnFind(filter, ddlEmployeeClass.SelectedItem.Value)
        fnGetValues(clsEmployeesClassCalander)
    End Sub
    Protected Sub ddlEmployeeClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEmployeeClass.SelectedIndexChanged
        SetReturnback()
    End Sub
    Protected Sub uwgDays_SelectedRowsChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs)
        SetSelectedDayInfo(e.SelectedRows(0))
    End Sub
    Protected Sub uwgDays_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs)
        SetSelectedDayInfo(e.Row)
    End Sub

#End Region

#Region "Private Functions"

    Private Sub SetDaysList()

        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)

        Dim clsEmployeeClassCalenderSet As New Clshrs_EmployeesClassesCalenderSet(Page)
        Dim intEmployeeClass As Integer = IIf(ddlEmployeeClass.SelectedValue = "", 0, ddlEmployeeClass.SelectedValue)

        uwgDays.Columns.Clear()
        uwgDays.Rows.Clear()

        Dim nCol As Infragistics.WebUI.UltraWebGrid.UltraGridColumn
        Dim nRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow

        Dim tab As New Data.DataTable()
        Dim nTCol As Data.DataColumn
        Dim nTRow As Data.DataRow
        Dim dayArr As String()

        If (StrLanguage <> "ar-EG") Then
            Dim dayArrEng As String() = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"}
            dayArr = dayArrEng
            uwgDays.DisplayLayout.HeaderStyleDefault.HorizontalAlign = HorizontalAlign.Left
            uwgDays.DisplayLayout.Bands(0).AllowSorting = Infragistics.WebUI.UltraWebGrid.AllowSorting.No
            '============First Col Checked =================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("Checked", "", Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(0)
            nCol.Hidden = True
            nCol.CellStyle.HorizontalAlign = HorizontalAlign.Center
            nCol.CellButtonDisplay = Infragistics.WebUI.UltraWebGrid.CellButtonDisplay.Always
            nCol.DataType = "System.Boolean"
            nCol.BaseColumnName = "Checked"
            '------------------
            nTCol = New Data.DataColumn("Checked", GetType(Boolean))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

            '============First Col Day=================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("Day", "Day", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(65)
            nCol.Width = System.Web.UI.WebControls.Unit.Percentage(100)
            nCol.CellStyle.HorizontalAlign = HorizontalAlign.Left
            nCol.BaseColumnName = "Day"
            '------------------
            nTCol = New Data.DataColumn("Day", GetType(String))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

            '============First Col Status=================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("Status", "Status", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(120)
            nCol.Hidden = True
            nCol.BaseColumnName = "Status"
            '------------------
            nTCol = New Data.DataColumn("Status", GetType(String))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

            '============First Col Val=================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("Val", "Val", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(0)
            nCol.Hidden = True
            nCol.DataType = "System.Int64"
            nCol.BaseColumnName = "Val"
            '------------------
            nTCol = New Data.DataColumn("Val", GetType(Integer))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

            '============First Col SVal=================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("SVal", "SVal", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(0)
            nCol.Hidden = True
            nCol.DataType = "System.Int64"
            nCol.BaseColumnName = "SVal"
            '------------------
            nTCol = New Data.DataColumn("SVal", GetType(Integer))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

            '============First Col Shifts=================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("Shifts", "Shifts", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(0)
            nCol.Hidden = True
            nCol.DataType = "System.String"
            nCol.BaseColumnName = "Shifts"
            '------------------
            nTCol = New Data.DataColumn("Shifts", GetType(String))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

        Else
            Dim dayArrArb As String() = {"الأحد", "الأثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت"}
            dayArr = dayArrArb

            uwgDays.DisplayLayout.HeaderStyleDefault.HorizontalAlign = HorizontalAlign.Right
            uwgDays.DisplayLayout.RowSelectorsDefault = Infragistics.WebUI.UltraWebGrid.RowSelectors.No
            uwgDays.DisplayLayout.Bands(0).AllowSorting = Infragistics.WebUI.UltraWebGrid.AllowSorting.No
            'uwgDays.Width = System.Web.UI.WebControls.Unit.Pixel(240)
            '============First Col Checked =================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("Checked", "", Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(0)
            nCol.Hidden = True
            nCol.CellStyle.HorizontalAlign = HorizontalAlign.Center
            nCol.CellButtonDisplay = Infragistics.WebUI.UltraWebGrid.CellButtonDisplay.Always
            nCol.DataType = "System.Boolean"
            nCol.BaseColumnName = "Checked"
            '------------------
            nTCol = New Data.DataColumn("Checked", GetType(Boolean))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

            '============First Col Day=================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("Day", "اليوم", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            nCol.Width = System.Web.UI.WebControls.Unit.Percentage(100)
            nCol.CellStyle.HorizontalAlign = HorizontalAlign.Right
            nCol.BaseColumnName = "Day"
            '------------------
            nTCol = New Data.DataColumn("Day", GetType(String))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

            '============First Col Status=================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("Status", "الحالة", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(120)
            nCol.Hidden = True
            nCol.BaseColumnName = "Status"
            '------------------
            nTCol = New Data.DataColumn("Status", GetType(String))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

            '============First Col Val=================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("Val", "Val", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(0)
            nCol.Hidden = True
            nCol.DataType = "System.Int64"
            nCol.BaseColumnName = "Val"
            '------------------
            nTCol = New Data.DataColumn("Val", GetType(Integer))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

            '============First Col SVal=================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("SVal", "SVal", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(0)
            nCol.Hidden = True
            nCol.DataType = "System.Int64"
            nCol.BaseColumnName = "SVal"
            '------------------
            nTCol = New Data.DataColumn("SVal", GetType(Integer))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

            '============First Col Shifts=================
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("Shifts", "Shifts", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            'nCol.Width = System.Web.UI.WebControls.Unit.Pixel(0)
            nCol.Hidden = True
            nCol.DataType = "System.String"
            nCol.BaseColumnName = "Shifts"
            '------------------
            nTCol = New Data.DataColumn("Shifts", GetType(String))


            uwgDays.Columns.Add(nCol)
            '------------------------
            tab.Columns.Add(nTCol)
            '============End===========================

        End If


        Dim i As Integer = 0
        For i = 0 To dayArr.Length - 1

            'Dim li As New ListItem(dayArr(i), i)
            'chkListDays.Items.Add(li)

            nTRow = tab.NewRow()
            clsEmployeeClassCalenderSet.Find(" e.DayNumber = " & i + 1 & " AND e.EmployeeClassID = " & intEmployeeClass & " ")
            If (clsEmployeeClassCalenderSet.ID > 0) Then
                nTRow("Checked") = True
                If (clsEmployeeClassCalenderSet.NonWorkingTime) Then
                    nTRow("SVal") = 1
                    nTRow("Status") = " Non Working Time "
                ElseIf ((Not clsEmployeeClassCalenderSet.NonWorkingTime) And (Not clsEmployeeClassCalenderSet.UseDefaultTime)) Then
                    nTRow("SVal") = 2
                    nTRow("Status") = " Non Default Time "

                    Dim strShifts As String = String.Empty
                    For Each row As Data.DataRow In clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows
                        strShifts = strShifts & row("FromTime") & "," & row("ToTime") & ","
                    Next
                    strShifts = strShifts.Trim(",")
                    nTRow("Shifts") = strShifts

                ElseIf (clsEmployeeClassCalenderSet.UseDefaultTime) Then
                    nTRow("SVal") = 0
                    nTRow("Status") = "Default Time "
                End If
            End If
            nTRow("Day") = dayArr(i)
            nTRow("Val") = i + 1
            '-------------------------------0257 MODIFIED-----------------------------------------
            If IsNothing(nTRow("SVal")) Or IsDBNull(nTRow("SVal")) Then
                nTRow("SVal") = 0
                nTRow("Status") = "Default Time "
            End If
            nTRow("Checked") = True

            '-------------------------------=============-----------------------------------------
            tab.Rows.Add(nTRow)
        Next
        uwgDays.DataSource = tab
        uwgDays.DataBind()
    End Sub
    Private Function SetReturnback() As Boolean
        ClearAllShifts()
        SetDaysList()

        clsEmployeesClassCalander = New Clshrs_EmployeesClassCalander(Page)
        Dim filter As String = String.Empty
        Dim dteSelectedDate As Date = clsEmployeesClassCalander.SetHigriDate(cldDateSelector.SelectedDate.Date)
        filter = fnPrePareDate(dteSelectedDate)

        clsEmployeesClassCalander.fnFind(filter, ddlEmployeeClass.SelectedItem.Value)
        fnGetValues(clsEmployeesClassCalander)
    End Function
    Private Function SaveCalenderDaySet() As Boolean

        Dim intEmployeeClass As Integer
        Dim clsEmployeeCalenderSet As Clshrs_EmployeesClassesCalenderSet

        Dim clsEmployeeClass As New Clshrs_EmployeeClasses(Page)
        clsEmployeeClass.Find(" ID=" & ddlEmployeeClass.SelectedValue)
        If (clsEmployeeClass.ID <= 0) Then
            Return False
        Else
            intEmployeeClass = ddlEmployeeClass.SelectedValue
        End If

        Dim index As Integer = 0
        'Dim dayItem As New ListItem()

        For index = 0 To uwgDays.Rows.Count - 1
            Dim currRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow = uwgDays.Rows(index)
            Dim dayVal As Integer = currRow.Cells(3).Value
            Dim statusVal As Integer = currRow.Cells(4).Value
            Dim shiftsVal As String = currRow.Cells(5).Value
            If (Not currRow.Cells(0).Value) Then
                Continue For
            End If

            Select Case statusVal 'rbtnSelectedDateType.SelectedItem.Value
                Case 0 'default time
                    clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                    clsEmployeeCalenderSet.DeleteAll(" hrs_EmployeesClassesCalenderSet.DayNumber = " & dayVal & " AND hrs_EmployeesClassesCalenderSet.EmployeeClassID = " & intEmployeeClass & " ")
                    AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal)
                    clsEmployeeCalenderSet.Save()


                Case 1 'non working time
                    clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                    clsEmployeeCalenderSet.DeleteAll(" hrs_EmployeesClassesCalenderSet.DayNumber = " & dayVal & " AND hrs_EmployeesClassesCalenderSet.EmployeeClassID = " & intEmployeeClass & " ")
                    AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal)
                    clsEmployeeCalenderSet.Save()

                Case 2
                    clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                    clsEmployeeCalenderSet.DeleteAll(" hrs_EmployeesClassesCalenderSet.DayNumber = " & dayVal & " AND hrs_EmployeesClassesCalenderSet.EmployeeClassID = " & intEmployeeClass & " ")

                    Dim shiftsArr As String() = SplitShifts(shiftsVal)
                    Select Case shiftsArr.Length
                        Case 0
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " Please Enter at least one shift value")
                            Return False
                        Case 2
                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(0), shiftsArr(1))
                            clsEmployeeCalenderSet.Save()


                        Case 4
                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(0), shiftsArr(1))
                            clsEmployeeCalenderSet.Save()

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(2), shiftsArr(3))
                            clsEmployeeCalenderSet.Save()

                        Case 6
                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(0), shiftsArr(1))
                            clsEmployeeCalenderSet.Save()

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(2), shiftsArr(3))
                            clsEmployeeCalenderSet.Save()

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(4), shiftsArr(5))
                            clsEmployeeCalenderSet.Save()

                        Case 8

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(0), shiftsArr(1))
                            clsEmployeeCalenderSet.Save()

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(2), shiftsArr(3))
                            clsEmployeeCalenderSet.Save()

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(4), shiftsArr(5))
                            clsEmployeeCalenderSet.Save()

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(6), shiftsArr(7))
                            clsEmployeeCalenderSet.Save()


                        Case 10
                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(0), shiftsArr(1))
                            clsEmployeeCalenderSet.Save()

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(2), shiftsArr(3))
                            clsEmployeeCalenderSet.Save()

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(4), shiftsArr(5))
                            clsEmployeeCalenderSet.Save()

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(6), shiftsArr(7))
                            clsEmployeeCalenderSet.Save()

                            clsEmployeeCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                            AssignCalenderDaySetValues(clsEmployeeCalenderSet, dayVal, statusVal, shiftsArr(8), shiftsArr(9))
                            clsEmployeeCalenderSet.Save()
                    End Select
            End Select
        Next
        Return True
    End Function
    Private Function SplitShifts(ByVal str As String) As String()
        str = str.Trim(",").Trim()
        Dim arr As String() = str.Split(",")
        Return arr
    End Function
    Private Function AssignCalenderDaySetValues(ByRef clsEmployeeCalenderSet As Clshrs_EmployeesClassesCalenderSet, ByVal dayNumber As Integer, ByVal Status As Integer, Optional ByVal fromTime As String = "", Optional ByVal toTime As String = "") As Boolean

        clsEmployeeCalenderSet.EmployeeClassID = ddlEmployeeClass.SelectedValue
        Dim clsEmployeeClass As New Clshrs_EmployeeClasses(Page)
        clsEmployeeClass.Find(" ID=" & ddlEmployeeClass.SelectedValue)
        If (clsEmployeeClass.ID <= 0) Then
            Return False
        End If
        clsEmployeeCalenderSet.DayNumber = dayNumber
        Select Case Status
            Case 0 'default time
                clsEmployeeCalenderSet.UseDefaultTime = True
                clsEmployeeCalenderSet.NonWorkingTime = False
                clsEmployeeCalenderSet.EmployeeClassID = clsEmployeeClass.ID
                clsEmployeeCalenderSet.FromTime = "9.0"
                clsEmployeeCalenderSet.ToTime = "9.0" 'IIf(clsEmployeeClass.ID > 0, (9 + clsEmployeeClass.WorkHoursPerDay).ToString, (9 + 8).ToString())
                Return True
            Case 1 'non working time
                clsEmployeeCalenderSet.UseDefaultTime = False
                clsEmployeeCalenderSet.NonWorkingTime = True
                clsEmployeeCalenderSet.EmployeeClassID = clsEmployeeClass.ID
                clsEmployeeCalenderSet.FromTime = "9.0"
                clsEmployeeCalenderSet.ToTime = "9.0" 'IIf(clsEmployeeClass.ID > 0, (9 + clsEmployeeClass.WorkHoursPerDay).ToString, (9 + 8).ToString())
                Return True
            Case 2 ' non default time
                clsEmployeeCalenderSet.UseDefaultTime = False
                clsEmployeeCalenderSet.NonWorkingTime = False
                clsEmployeeCalenderSet.EmployeeClassID = clsEmployeeClass.ID
                clsEmployeeCalenderSet.FromTime = fromTime
                clsEmployeeCalenderSet.ToTime = toTime
                Return True
        End Select


    End Function
    Private Sub ClearShfitTxt()
        WebDateTimeEdit1.Text = String.Empty
        WebDateTimeEdit2.Text = String.Empty
        WebDateTimeEdit3.Text = String.Empty
        WebDateTimeEdit4.Text = String.Empty
        WebDateTimeEdit5.Text = String.Empty
        WebDateTimeEdit6.Text = String.Empty
        WebDateTimeEdit7.Text = String.Empty
        WebDateTimeEdit8.Text = String.Empty
        WebDateTimeEdit9.Text = String.Empty
        WebDateTimeEdit10.Text = String.Empty
    End Sub
    Private Sub SetSelectedDayInfo(ByVal selectedRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow)
        ClearShfitTxt()

        Dim intEmployeeClass As Integer = IIf(ddlEmployeeClass.SelectedValue = "", 0, ddlEmployeeClass.SelectedValue)
        Dim clsEmployeeClassCalenderSet As New Clshrs_EmployeesClassesCalenderSet(Page)
        If (selectedRow.Cells(0).Value = True) Then
            clsEmployeeClassCalenderSet.Find(" e.DayNumber = " & selectedRow.Cells(3).Value & " AND e.EmployeeClassID = " & intEmployeeClass & " ")
            rbtnSelectedDateType.SelectedIndex = selectedRow.Cells(4).Value
            Select Case selectedRow.Cells(4).Value
                Case 2
                    Dim rowCount As Integer = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows.Count

                    Select Case rowCount
                        Case 1
                            WebDateTimeEdit1.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(0)("FromTime").ToString
                            WebDateTimeEdit2.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(0)("ToTime").ToString


                        Case 2
                            WebDateTimeEdit1.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(0)("FromTime").ToString
                            WebDateTimeEdit2.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(0)("ToTime").ToString

                            WebDateTimeEdit3.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(1)("FromTime").ToString
                            WebDateTimeEdit4.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(1)("ToTime").ToString


                        Case 3
                            WebDateTimeEdit1.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(0)("FromTime").ToString
                            WebDateTimeEdit2.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(0)("ToTime").ToString

                            WebDateTimeEdit3.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(1)("FromTime").ToString
                            WebDateTimeEdit4.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(1)("ToTime").ToString

                            WebDateTimeEdit5.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(2)("FromTime").ToString
                            WebDateTimeEdit6.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(2)("ToTime").ToString


                        Case 4
                            WebDateTimeEdit1.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(0)("FromTime").ToString
                            WebDateTimeEdit2.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(0)("ToTime").ToString

                            WebDateTimeEdit3.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(1)("FromTime").ToString
                            WebDateTimeEdit4.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(1)("ToTime").ToString

                            WebDateTimeEdit5.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(2)("FromTime").ToString
                            WebDateTimeEdit6.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(2)("ToTime").ToString

                            WebDateTimeEdit7.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(3)("FromTime").ToString
                            WebDateTimeEdit8.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(3)("ToTime").ToString


                        Case 5
                            WebDateTimeEdit1.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(0)("FromTime").ToString
                            WebDateTimeEdit2.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(0)("ToTime").ToString

                            WebDateTimeEdit3.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(1)("FromTime").ToString
                            WebDateTimeEdit4.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(1)("ToTime").ToString

                            WebDateTimeEdit5.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(2)("FromTime").ToString
                            WebDateTimeEdit6.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(2)("ToTime").ToString

                            WebDateTimeEdit7.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(3)("FromTime").ToString
                            WebDateTimeEdit8.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(3)("ToTime").ToString

                            WebDateTimeEdit9.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(4)("FromTime").ToString
                            WebDateTimeEdit10.Text = clsEmployeeClassCalenderSet.DataSet.Tables(0).Rows(4)("ToTime").ToString


                    End Select



            End Select

        End If


    End Sub
    Private Function fnPrepareToSave(Optional ByVal SaveType As Boolean = False, Optional ByVal Filter As String = "") As Boolean
        '-------------------------------------------------
        If Not fnValidateInputes() Then ' Validate Inputs 
            Exit Function
        End If
        '-------------------------------------------------
        Dim ClsDAL As New ClsDataAcessLayer(Page)
        Dim dDateFrom As Date = Nothing
        Dim dDateTo As Date = Nothing
        Dim dCalanderDate As Date = ClsDAL.SetHigriDate(cldDateSelector.SelectedDate.Date)
        Dim bUseDefault As Boolean = False
        Dim bNonWorking As Boolean = False
        '--------------------------------------------------
        clsEmployeesClassCalander = New Clshrs_EmployeesClassCalander(Page)

        '- Verfiy User Goal and according to it make the desire work 
        Select Case rbtnSelectedDateType.SelectedItem.Value
            Case 0 'user Sets selected dates to default 
                bUseDefault = True
                bNonWorking = False
                dDateFrom = dCalanderDate
                dDateTo = dCalanderDate
                If fnAssignvalue(clsEmployeesClassCalander, dDateFrom, dDateTo, bUseDefault, bNonWorking) Then
                    If Not SaveType Then                          'Save 
                        clsEmployeesClassCalander.fnSaveToDB()
                    Else                                          'Update 
                        clsEmployeesClassCalander.fnUpdate(Filter, ddlEmployeeClass.SelectedItem.Value)
                    End If
                End If
            Case 1 'user Sets selected dates as non-working dates

                bUseDefault = False
                bNonWorking = True
                dDateFrom = dCalanderDate
                dDateTo = dCalanderDate
                If fnAssignvalue(clsEmployeesClassCalander, dDateFrom, dDateTo, bUseDefault, bNonWorking) Then
                    If Not SaveType Then                          'Save 
                        clsEmployeesClassCalander.fnSaveToDB()
                    Else                                          'Update 

                        clsEmployeesClassCalander.fnUpdate(Filter, ddlEmployeeClass.SelectedItem.Value)
                    End If
                End If

            Case 2 'non-Default dates normal position 

                'If WebDateTimeEdit1.Text = "" Or WebDateTimeEdit2.Text = "" Then
                '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Time Is Missing Please Fill the Shift Time you Want Assign the Selected Date To !")
                '    Exit Function
                'End If

                'Check No of Shifts 
                Dim arrList As ArrayList
                Select Case fnCheckShifts()
                    Case 1
                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                    Case 2
                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                    Case 3
                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)
                    Case 4
                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                    Case 5
                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit1.Text, WebDateTimeEdit2.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit3.Text, WebDateTimeEdit4.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit5.Text, WebDateTimeEdit6.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit7.Text, WebDateTimeEdit8.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)

                        arrList = fnGetDates(dCalanderDate, WebDateTimeEdit9.Text, WebDateTimeEdit10.Text)
                        fnBelongTofnPrepareToSave(arrList, SaveType, bUseDefault, bNonWorking, Filter)
                End Select
        End Select
    End Function
    Private Sub fnBelongTofnPrepareToSave(ByRef arrlist As ArrayList, ByVal SaveType As Boolean, ByVal bUseDefault As Boolean, ByVal bNonWorking As Boolean, ByVal filter As String)
        If fnAssignvalue(clsEmployeesClassCalander, CDate(arrlist.Item(0)), CDate(arrlist.Item(1)), bUseDefault, bNonWorking) Then
            If Not SaveType Then                          'Save 
                clsEmployeesClassCalander.fnSaveToDB()
            Else                                          'Update 
                clsEmployeesClassCalander.fnUpdate(filter, ddlEmployeeClass.SelectedItem.Value)
            End If
        End If
        arrlist.Clear()
    End Sub
    Private Function fnCheckShifts() As Integer
        Dim nShifts As Integer = 0
        If Val(WebDateTimeEdit1.Text) > 0 And Val(WebDateTimeEdit2.Text) > 0 Then
            nShifts += 1
            If Val(WebDateTimeEdit3.Text) > 0 And Val(WebDateTimeEdit4.Text) > 0 Then
                nShifts += 1
                If Val(WebDateTimeEdit5.Text) > 0 And Val(WebDateTimeEdit6.Text) > 0 Then
                    nShifts += 1
                    If Val(WebDateTimeEdit7.Text) > 0 And Val(WebDateTimeEdit8.Text) > 0 Then
                        nShifts += 1
                        If Val(WebDateTimeEdit9.Text) > 0 And Val(WebDateTimeEdit10.Text) > 0 Then
                            nShifts += 1
                        End If
                    End If
                End If
            End If
        End If

        Return nShifts
    End Function
    Private Function fnValidateInputes() As Boolean
        'Validate Selected Dates Radio buttons 
        '---------------------------------------------
        If rbtnSelectedDateType.SelectedIndex < 0 Then
            'If Convert.ToInt32(WebDateTimeEdit1.Text) = 0 Or Convert.ToInt32(WebDateTimeEdit2.Text = 0) Then
            If WebDateTimeEdit1.Text = "" Or WebDateTimeEdit2.Text = "" Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " Please Select What You Want to Set the Selected Date(s) To!")
                Exit Function
            End If
            Exit Function
        End If
        '---------------------------------------------
        'Validate Employee Class Selection
        '---------------------------------------------
        If ddlEmployeeClass.SelectedIndex = 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " Please Select the Employee Class You Want to Assign Calander To.!")
            Exit Function
        End If
        '---------------------------------------------
        Return True
    End Function
    Private Function fnGetDates(ByVal dDate As Date, ByRef TxtShiftFrom As String, ByRef txtShiftTo As String) As ArrayList
        Dim arrListDates As New ArrayList
        Dim dDay As Integer = dDate.Day
        Dim dMonth As Integer = dDate.Month
        Dim dYear As Integer = dDate.Year

        Dim dHourFrom As Integer = TxtShiftFrom.Split(":")(0) 'Math.Truncate(CDbl(TxtShiftFrom))
        Dim dMinutesFrom As Integer = TxtShiftFrom.Split(":")(1).Split(" ")(0) 'Math.Round((CDbl(TxtShiftFrom) - Math.Truncate(CDbl(TxtShiftFrom))) * 100)

        Dim strShiftFrom As String = TxtShiftFrom.Split(":")(1).Split(" ")(1).Trim.ToUpper()
        Dim strShiftTo As String = txtShiftTo.Split(":")(1).Split(" ")(1).Trim.ToUpper()

        If (strShiftFrom = "PM" Or strShiftFrom = "م") Then
            If (dHourFrom <> 12) Then
                dHourFrom = dHourFrom + 12
            End If
        ElseIf ((strShiftFrom = "AM" Or strShiftFrom = "ص") And dHourFrom = 12) Then
            dHourFrom = 0
        End If

        Dim dHourTo As Integer = txtShiftTo.Split(":")(0) 'Math.Truncate(CDbl(txtShiftTo))
        Dim dMinutesTo As Integer = txtShiftTo.Split(":")(1).Split(" ")(0) 'Math.Round((CDbl(txtShiftTo) - Math.Truncate(CDbl(txtShiftTo))) * 100)

        If (strShiftTo = "PM" Or strShiftTo = "م") Then
            If (dHourTo <> 12) Then
                dHourTo = dHourTo + 12
            End If
        ElseIf ((strShiftTo = "AM" Or strShiftTo = "ص") And dHourTo = 12) Then
            dHourTo = 0

        End If
        '
        arrListDates.Add(fnMakeDateTime(dYear, dMonth, dDay, dHourFrom, dMinutesFrom, 0))
        arrListDates.Add(fnMakeDateTime(dYear, dMonth, dDay, dHourTo, dMinutesTo, 0))
        Return arrListDates
    End Function
    Private Function fnMakeDateTime(ByVal dYear As Integer, ByVal dMonth As Integer, ByVal dDay As Integer, ByVal dHours As Integer, ByVal dMinutes As Integer, Optional ByVal dSecounds As Integer = 0) As DateTime
        Dim dateConverted As New System.DateTime(dYear, dMonth, dDay, dHours, dMinutes, dSecounds)
        Return dateConverted
    End Function
    Private Function fnPrePareDate(ByVal dDate As Date) As String
        Dim d As Int16 = dDate.Day : Dim dStr As String = String.Empty
        Dim m As Int16 = dDate.Month : Dim mStr As String = String.Empty
        Dim y As Int32 = dDate.Year : Dim yStr As String = String.Empty
        If d < 10 Then
            dStr = "0" & d.ToString
        Else
            dStr = d.ToString
        End If
        If m < 10 Then
            mStr = "0" & m.ToString
        Else
            mStr = m.ToString
        End If
        If y < 100 Then
            yStr = "20" & y.ToString
        Else
            yStr = y.ToString
        End If

        Return mStr & "/" & dStr & "/" & yStr
    End Function
    Private Function fnAssignvalue(ByRef clsEmployeesClassCalander As Clshrs_EmployeesClassCalander, ByVal dTimeFrom As Date, ByVal dTimeTo As Date, ByVal bUseDefault As Boolean, ByVal bNonWorking As Boolean) As Boolean
        'clsEmployeesClassCalander = New  Clshrs_EmployeesClassCalander(Page)
        Try
            With clsEmployeesClassCalander
                .EmployeeClassID = ddlEmployeeClass.SelectedValue
                .FromTime = dTimeFrom
                .ToTime = dTimeTo
                .useDefaultTime = bUseDefault
                .nonWorkingTime = bNonWorking
            End With

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function fnGetValues(ByRef clsEmployeesClassCalander As Clshrs_EmployeesClassCalander) As Boolean

        Try
            With clsEmployeesClassCalander

                rbtnSelectedDateType.Items(0).Selected = .useDefaultTime
                rbtnSelectedDateType.Items(1).Selected = .nonWorkingTime
                If .useDefaultTime = False And .nonWorkingTime = False And .EmployeeClassID > 0 Then
                    rbtnSelectedDateType.Items(2).Selected = True
                    Dim shiftsCount As Integer = .DataSet.Tables(0).Rows.Count
                    EnableShifts(shiftsCount, .DataSet)

                Else
                    WebDateTimeEdit1.Text = ""
                    WebDateTimeEdit2.Text = ""
                    rbtnSelectedDateType.Items(2).Selected = False
                End If

            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub EnableShifts(ByVal shiftsCnt As Integer, ByVal ds As Data.DataSet)
        Select Case shiftsCnt
            Case 1
                WebDateTimeEdit1.Enabled = True
                WebDateTimeEdit2.Enabled = True
                WebDateTimeEdit1.Value = ds.Tables(0).Rows(0)("FromTime")
                WebDateTimeEdit2.Value = ds.Tables(0).Rows(0)("ToTime")


                WebDateTimeEdit3.Enabled = True
                WebDateTimeEdit4.Enabled = True
            Case 2
                WebDateTimeEdit1.Enabled = True
                WebDateTimeEdit2.Enabled = True
                WebDateTimeEdit3.Enabled = True
                WebDateTimeEdit4.Enabled = True

                WebDateTimeEdit1.Value = ds.Tables(0).Rows(0)("FromTime")
                WebDateTimeEdit2.Value = ds.Tables(0).Rows(0)("ToTime")
                WebDateTimeEdit3.Value = ds.Tables(0).Rows(1)("FromTime")
                WebDateTimeEdit4.Value = ds.Tables(0).Rows(1)("ToTime")

                WebDateTimeEdit5.Enabled = True
                WebDateTimeEdit6.Enabled = True
            Case 3

                WebDateTimeEdit1.Enabled = True
                WebDateTimeEdit2.Enabled = True
                WebDateTimeEdit3.Enabled = True
                WebDateTimeEdit4.Enabled = True
                WebDateTimeEdit5.Enabled = True
                WebDateTimeEdit6.Enabled = True

                WebDateTimeEdit1.Value = ds.Tables(0).Rows(0)("FromTime")
                WebDateTimeEdit2.Value = ds.Tables(0).Rows(0)("ToTime")
                WebDateTimeEdit3.Value = ds.Tables(0).Rows(1)("FromTime")
                WebDateTimeEdit4.Value = ds.Tables(0).Rows(1)("ToTime")
                WebDateTimeEdit5.Value = ds.Tables(0).Rows(2)("FromTime")
                WebDateTimeEdit6.Value = ds.Tables(0).Rows(2)("ToTime")



                WebDateTimeEdit7.Enabled = True
                WebDateTimeEdit8.Enabled = True
            Case 4

                WebDateTimeEdit1.Enabled = True
                WebDateTimeEdit2.Enabled = True
                WebDateTimeEdit3.Enabled = True
                WebDateTimeEdit4.Enabled = True
                WebDateTimeEdit5.Enabled = True
                WebDateTimeEdit6.Enabled = True
                WebDateTimeEdit7.Enabled = True
                WebDateTimeEdit8.Enabled = True

                WebDateTimeEdit1.Value = ds.Tables(0).Rows(0)("FromTime")
                WebDateTimeEdit2.Value = ds.Tables(0).Rows(0)("ToTime")
                WebDateTimeEdit3.Value = ds.Tables(0).Rows(1)("FromTime")
                WebDateTimeEdit4.Value = ds.Tables(0).Rows(1)("ToTime")
                WebDateTimeEdit5.Value = ds.Tables(0).Rows(2)("FromTime")
                WebDateTimeEdit6.Value = ds.Tables(0).Rows(2)("ToTime")
                WebDateTimeEdit7.Value = ds.Tables(0).Rows(3)("FromTime")
                WebDateTimeEdit8.Value = ds.Tables(0).Rows(3)("ToTime")

                WebDateTimeEdit9.Enabled = True
                WebDateTimeEdit10.Enabled = True
            Case 5
                WebDateTimeEdit1.Enabled = True
                WebDateTimeEdit2.Enabled = True
                WebDateTimeEdit3.Enabled = True
                WebDateTimeEdit4.Enabled = True
                WebDateTimeEdit5.Enabled = True
                WebDateTimeEdit6.Enabled = True
                WebDateTimeEdit7.Enabled = True
                WebDateTimeEdit8.Enabled = True
                WebDateTimeEdit9.Enabled = True
                WebDateTimeEdit10.Enabled = True

                WebDateTimeEdit1.Value = ds.Tables(0).Rows(0)("FromTime")
                WebDateTimeEdit2.Value = ds.Tables(0).Rows(0)("ToTime")
                WebDateTimeEdit3.Value = ds.Tables(0).Rows(1)("FromTime")
                WebDateTimeEdit4.Value = ds.Tables(0).Rows(1)("ToTime")
                WebDateTimeEdit5.Value = ds.Tables(0).Rows(2)("FromTime")
                WebDateTimeEdit6.Value = ds.Tables(0).Rows(2)("ToTime")
                WebDateTimeEdit7.Value = ds.Tables(0).Rows(3)("FromTime")
                WebDateTimeEdit8.Value = ds.Tables(0).Rows(3)("ToTime")
                WebDateTimeEdit9.Value = ds.Tables(0).Rows(4)("FromTime")
                WebDateTimeEdit10.Value = ds.Tables(0).Rows(4)("ToTime")

        End Select
    End Sub
    Private Sub ClearAllShifts()
        WebDateTimeEdit1.Text = ""
        WebDateTimeEdit2.Text = ""
        WebDateTimeEdit1.Enabled = False
        WebDateTimeEdit2.Enabled = False

        WebDateTimeEdit3.Text = ""
        WebDateTimeEdit4.Text = ""
        WebDateTimeEdit3.Enabled = False
        WebDateTimeEdit4.Enabled = False

        WebDateTimeEdit5.Text = ""
        WebDateTimeEdit6.Text = ""
        WebDateTimeEdit5.Enabled = False
        WebDateTimeEdit6.Enabled = False

        WebDateTimeEdit7.Text = ""
        WebDateTimeEdit8.Text = ""
        WebDateTimeEdit7.Enabled = False
        WebDateTimeEdit8.Enabled = False

        WebDateTimeEdit9.Text = ""
        WebDateTimeEdit10.Text = ""
        WebDateTimeEdit9.Enabled = False
        WebDateTimeEdit10.Enabled = False
    End Sub

#End Region

    Protected Sub LinkButton1_Click(sender As Object, e As System.EventArgs) Handles LinkButton1.Click
        Try
            Dim intEmployeeClass As Integer
            Dim clsEmployeeClass As New Clshrs_EmployeeClasses(Page)
            clsEmployeeClass.Find(" ID=" & ddlEmployeeClass.SelectedValue)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployeeClass.ConnectionString)
            If (clsEmployeeClass.ID <= 0) Then
                intEmployeeClass = 0
            Else
                intEmployeeClass = ddlEmployeeClass.SelectedValue
            End If
            If intEmployeeClass <> 0 Then
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmCalendarExceptions.aspx?Cls=" & intEmployeeClass, 700, 350, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " No Class Selected ! /إختر فئة وظيفية"))
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
