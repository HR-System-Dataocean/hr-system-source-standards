Imports System.Data
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System
Imports Venus.Shared.Web
Partial Class frmAttendAppointment
    Inherits MainPage
#Region "Public Decleration"
    Private Cls_AttendAppointment As Clshrs_Att_AttendAppointment
    Private ss As Clshrs_Att_AttendAppointment
    Private clsMainOtherFields As clsSys_MainOtherFields


#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Cls_AttendAppointment = New Clshrs_Att_AttendAppointment(Me.Page)
        Dim ClsAttendShifts = New ClsAtt_AttendShifts(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim clsBranch As New Clssys_Branches(Page)
        Dim SearchID As Integer = 0
        TxtAttendShiftName.Enabled = False
        TxtEmpName.Enabled = False
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & Cls_AttendAppointment.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            If ClsObjects.Find(" Code='" & ClsAttendShifts.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtAttendShifts.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & TxtAttendShifts.ClientID & "'"
                    btnSearchShift.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler

            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Page)

            Dim Cls_Employees As New Clshrs_Employees(Me)


            Dim clsCompanies = New Clssys_Companies(Me.Page)
            clsCompanies.Find("ID = " & Cls_Employees.MainCompanyID)
            If clsCompanies.UserDepartmentsPermissions Then
                If ClsObjects.Find("Code='Hrs_V_Employees'") Then
                    ClsEmployees.Find("Code='" & _sys_User.Code & "'")
                    ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtEmpCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & TxtEmpCode.ClientID & "'"
                    BtnEmpSearch.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            Else
                If ClsObjects.Find("Code='Hrs_Employees'") Then

                    ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtEmpCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & TxtEmpCode.ClientID & "'"
                    BtnEmpSearch.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            If ClsObjects.Find("Code='Hrs_Employees'") Then

                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                SearchID = ClsSearchs.ID
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtEmpCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & TxtEmpCode.ClientID & "'"
                BtnEmpSearch.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", Cls_AttendAppointment.ConnectionString)
                Cls_AttendAppointment.AddOnChangeEventToControls("AttendAppointment", Page, UltraWebTab1)

                clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
                '================================= Exit & Navigation Notification [ End ]
                'Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                'Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtEngName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                'Dim ClsAttendShifts As New ClsAtt_AttendShifts(Me)
                'ClsAttendShifts.GetDropDownList(ddlAttendShifts, True)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "" And txtCode.Text <> "تلقائى") Then
                Cls_AttendAppointment.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = Cls_AttendAppointment.ID
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
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(Cls_AttendAppointment.ConnectionString)
            txtCode.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Auto/تلقائى")
            If ProfileCls.CurrentLanguage = "Ar" Then
                Dim mLeft As New System.Web.UI.WebControls.Unit(100, UnitType.Percentage)
                UltraWebTree1.Padding.Left = mLeft
            Else
                Dim mRight As New System.Web.UI.WebControls.Unit(100, UnitType.Percentage)
                UltraWebTree1.Padding.Right = mRight
            End If


            If Not IsPostBack Then
                Dim DteStartDate As Date = Date.Now
                Dim DteEndDate As Date = Date.Now
                Dim EmpCode As String = ""
                If Request.QueryString.Count > 0 Then
                    Dim ClsLocations As New Clssys_Locations(Me)
                    Dim ObjNavigationHandle As New Venus.Shared.Web.NavigationHandler(Cls_AttendAppointment.ConnectionString)
                    If Request.QueryString.Item("EmpCode") <> Nothing Then
                        EmpCode = Request.QueryString.Item("EmpCode")
                        Cls_Employees.Find("Code = " & EmpCode)

                        '=======
                        ClsLocations.Find("ID=" & Cls_Employees.LocationID & " and CancelDate is null")
                        For i As Integer = 0 To ClsLocations.DataSet.Tables(0).Rows.Count - 1
                            Dim ObjTreeNode As New Infragistics.WebUI.UltraWebNavigator.Node
                            ObjTreeNode.Text = ClsLocations.DataSet.Tables(0).Rows(i)((ObjNavigationHandle.SetLanguage(Me, "EngName/ArbName")))
                            If (ObjTreeNode.Text.Trim = String.Empty) Then
                                ObjTreeNode.Text = ClsLocations.DataSet.Tables(0).Rows(i)((ObjNavigationHandle.SetLanguage(Me, "ArbName/EngName")))
                            End If

                            Dim ObjTreeNodeSub As New Infragistics.WebUI.UltraWebNavigator.Node
                            ObjTreeNodeSub.Text = Cls_Employees.Code + " " + Cls_Employees.FullName
                            ObjTreeNodeSub.Tag = Cls_Employees.ID
                            ObjTreeNode.Nodes.Add(ObjTreeNodeSub)
                            UltraWebTree1.Nodes.Add(ObjTreeNode)
                            UltraWebTree1.ExpandAll()
                        Next


                        '=============



                        'Dim ObjTreeNode As New Infragistics.WebUI.UltraWebNavigator.Node

                        'ObjTreeNode.Text = Cls_Employees.Code + " " + Cls_Employees.FullName
                        'ObjTreeNode.Tag = Cls_Employees.ID

                        'ObjTreeNode.Checked = True
                        'UltraWebTree1.Nodes.Add(ObjTreeNode)
                        'UltraWebTree1.ExpandAll()

                    End If
                    If Request.QueryString.Item("StartDate") <> Nothing Then
                        DteStartDate = Request.QueryString.Item("StartDate")
                        StartDate.Value = Cls_AttendAppointment.GetHigriDate(DteStartDate)
                    End If
                    If Request.QueryString.Item("ToDate") <> Nothing Then
                        DteEndDate = Request.QueryString.Item("ToDate")
                        EndDate.Value = Cls_AttendAppointment.GetHigriDate(DteEndDate)
                    End If





                End If


            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, Cls_AttendAppointment.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        Cls_AttendAppointment = New Clshrs_Att_AttendAppointment(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(Cls_AttendAppointment.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If StartDate.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in Plan Dates Count  / أدخل تاريح بداية الإسناد"))
                    Exit Sub
                End If
                If EndDate.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in Plan Dates Count  /أدخل تاريخ نهاية الإسناد"))
                    Exit Sub
                End If
                If CheckDates(CDate(StartDate.Value), CDate(EndDate.Value)) = False Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in Plan Dates Count  /تاريخ نهاية الإسناد يجب ان يكون أكبر من تاريخ البداية"))
                    Exit Sub
                End If
                If String.IsNullOrEmpty(TxtAttendShifts.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in First Shift Select  / حدد الحطة التي تريد إسنادها"))
                    Exit Sub
                End If
                'If ddlAttendShifts.SelectedIndex = 0 Then
                '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in First Shift Select  / حدد الحطة التي تريد إسنادها"))
                '    Exit Sub
                'End If
                If DGSelect() = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in First Shift Select  / اختر موظف على الأقل "))
                    Exit Sub
                End If
                If Cls_AttendAppointment.ID > 0 Then
                    Cls_AttendAppointment.Update("code='" & txtCode.Text & "'")
                Else
                    Cls_AttendAppointment.Save()
                End If
                Cls_AttendAppointment.Find("Code='" & txtCode.Text & "'")
                If Cls_AttendAppointment.ID > 0 Then
                    If (SaveDG(Cls_AttendAppointment.ID)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, Cls_AttendAppointment.Table, Cls_AttendAppointment.ID)
                        value.Text = ""
                    End If
                End If
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, Cls_AttendAppointment.Table, Cls_AttendAppointment.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If StartDate.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in Plan Dates Count  / أدخل تاريح بداية الإسناد"))
                    Exit Sub
                End If
                If EndDate.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in Plan Dates Count  /أدخل تاريخ نهاية الإسناد"))
                    Exit Sub
                End If
                If CheckDates(CDate(StartDate.Value), CDate(EndDate.Value)) = False Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in Plan Dates Count  /تاريخ نهاية الإسناد يجب ان يكون أكبر من تاريخ البداية"))
                    Exit Sub
                End If
                If String.IsNullOrEmpty(TxtAttendShifts.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in First Shift Select  / حدد الخطة المسندة "))
                    Exit Sub
                End If
                If DGSelect() = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error in First Shift Select  / اختر موظف على الأقل "))
                    Exit Sub
                End If
                Cls_AttendAppointment.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If Cls_AttendAppointment.ID > 0 Then
                    Cls_AttendAppointment.Update("code='" & txtCode.Text & "'")
                Else
                    'Cls_AttendAppointment.Save()
                    Cls_AttendAppointment.Find("id=" & Cls_AttendAppointment.Save())
                End If
                'Cls_AttendAppointment.Find("Code='" & txtCode.Text & "'")
                If Cls_AttendAppointment.ID > 0 Then
                    If (SaveDG(Cls_AttendAppointment.ID)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, Cls_AttendAppointment.Table, Cls_AttendAppointment.ID)
                        value.Text = ""
                    End If
                End If
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                AfterOperation()
            Case "New"
                AfterOperation()

                GetEmployees()


            Case "Delete"
                Cls_AttendAppointment.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                Cls_AttendAppointment.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & Cls_AttendAppointment.ID & "&TableName=" & Cls_AttendAppointment.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                Cls_AttendAppointment.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & Cls_AttendAppointment.ID & "&TableName=" & Cls_AttendAppointment.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = Cls_AttendAppointment.Table
                Cls_AttendAppointment.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = Cls_AttendAppointment.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & Cls_AttendAppointment.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                Cls_AttendAppointment.Find(" Code= '" & txtCode.Text & "'")
                If Cls_AttendAppointment.ID > 0 Then
                    Dim Ds As Data.DataSet = Cls_AttendAppointment.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If Cls_AttendAppointment.CheckDiff(Cls_AttendAppointment, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                Cls_AttendAppointment.FirstRecord()
                GetValues()
            Case "Previous"
                Cls_AttendAppointment.Find("Code='" & txtCode.Text & "'")
                If Not Cls_AttendAppointment.previousRecord() Then
                    Cls_AttendAppointment.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                Cls_AttendAppointment.Find("Code='" & txtCode.Text & "'")
                If Not Cls_AttendAppointment.NextRecord() Then
                    Cls_AttendAppointment.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                Cls_AttendAppointment.LastRecord()
                GetValues()
        End Select
    End Sub

    Private Sub GetEmployees()
        UltraWebTree1.Nodes.Clear()

        Dim ObjNavigationHandle As New Venus.Shared.Web.NavigationHandler(Cls_AttendAppointment.ConnectionString)
        Dim clsDepartments As New Clssys_Departments(Me)


        Dim Cls_Employees As New Clshrs_Employees(Me)
        Dim clsCompanies = New Clssys_Companies(Me.Page)
        clsCompanies.Find("ID = " & Cls_Employees.MainCompanyID)
        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler

        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")
        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & _sys_User.Code & "'")



        Dim departsPermission = ""
        Dim LocsPermission = ""
        If clsCompanies.UserDepartmentsPermissions Then
            Dim dsDepPermission As DataSet
            Dim str As String = "SELECT [DepartmentId] FROM [dbo].[hrs_EmployeeDepartments] where [EmpId]='" & ClsEmployees.ID & "'"
            dsDepPermission = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Cls_Employees.ConnectionString, CommandType.Text, str)
            departsPermission = "(-1"
            For i As Integer = 0 To dsDepPermission.Tables(0).Rows.Count - 1
                departsPermission = departsPermission & "," & Convert.ToString(dsDepPermission.Tables(0).Rows(i)("DepartmentId"))
            Next
            departsPermission = departsPermission & ")"

            Dim dsLocPermission As DataSet
            Dim strLoc As String = "SELECT [LocationId] FROM [dbo].[hrs_EmployeeLocations] where [EmpId]='" & ClsEmployees.ID & "'"
            dsLocPermission = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Cls_Employees.ConnectionString, CommandType.Text, strLoc)
            LocsPermission = "(-1"
            For i As Integer = 0 To dsLocPermission.Tables(0).Rows.Count - 1
                LocsPermission = LocsPermission & "," & Convert.ToString(dsLocPermission.Tables(0).Rows(i)("LocationId"))
            Next
            LocsPermission = LocsPermission & ")"

        End If
        Dim strbranchdepartment = ""
        Dim dsDepartments As DataSet
        If ddlBranche.SelectedValue > 0 Then
            strbranchdepartment = "Select * from sys_DepartmentsBranches join sys_Departments on sys_DepartmentsBranches.DepartmentID=sys_Departments.ID where Checked = 1 and BranchID =" & ddlBranche.SelectedValue & ""
            If departsPermission <> "" Then
                strbranchdepartment = strbranchdepartment & " and sys_Departments.ID in " & departsPermission
            End If

            dsDepartments = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Cls_Employees.ConnectionString, CommandType.Text, strbranchdepartment)
        Else
            strbranchdepartment = "Select * from sys_Departments where CancelDate is null"
            If departsPermission <> "" Then
                strbranchdepartment = strbranchdepartment & " and sys_Departments.ID in " & departsPermission
            End If
            dsDepartments = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Cls_Employees.ConnectionString, CommandType.Text, strbranchdepartment)

        End If



        clsDepartments.Find("CancelDate is null")
        'For i As Integer = 0 To clsDepartments.DataSet.Tables(0).Rows.Count - 1
        For i As Integer = 0 To dsDepartments.Tables(0).Rows.Count - 1


            Dim ObjTreeNode As New Infragistics.WebUI.UltraWebNavigator.Node
            ObjTreeNode.Text = dsDepartments.Tables(0).Rows(i)((ObjNavigationHandle.SetLanguage(Me, "EngName/ArbName")))
            If (ObjTreeNode.Text.Trim = String.Empty) Then
                ObjTreeNode.Text = dsDepartments.Tables(0).Rows(i)((ObjNavigationHandle.SetLanguage(Me, "ArbName/EngName")))
            End If

            'Cls_Employees.Find("LocationID = " & ClsLocations.DataSet.Tables(0).Rows(i)("ID"))
            Dim branchfilter As String = ""
            If ddlBranche.SelectedValue > 0 Then
                branchfilter = " and e.branchID=" & ddlBranche.SelectedValue
            End If
            Dim Deptfilter As String = ""
            'If ddlDepartment.SelectedValue > 0 Then
            '    Deptfilter = " and e.DepartmentID=" & ddlDepartment.SelectedValue
            'End If

            Dim locFilter = ""
            If LocsPermission <> "" Then
                locFilter = " and LocationId in " & LocsPermission
            End If
            Dim empName = "dbo.fn_GetEmpName (e.code,1)"
            If ProfileCls.CurrentLanguage = "Ar" Then

                empName = "dbo.fn_GetEmpName (e.code,1)"
            Else
                empName = "dbo.fn_GetEmpName (e.code,0)"
            End If
            Dim strselect = ""
            If ddlBranche.SelectedValue > 0 And ddlDepartment.SelectedValue = 0 Then
                strselect = "select e.id,e.code," & empName & " as FullName from hrs_employees e inner join hrs_contracts c on e.id=c.employeeid where e.excludedate is null and e.canceldate is null and  (c.enddate >getdate() or c.EndDate is null) and e.BranchID=" & ddlBranche.SelectedValue & " " & branchfilter & " and e.DepartmentID=" & dsDepartments.Tables(0).Rows(i)("DepartmentID") & locFilter & "  order by Case When IsNumeric(e.Code) = 1 Then Right(Replicate('0',51) + e.Code, 50) When IsNumeric(e.Code) = 0 then Left(e.Code + Replicate('',51), 50) Else e.Code End"
            ElseIf ddlBranche.SelectedValue > 0 And ddlDepartment.SelectedValue > 0 Then
                strselect = "select e.id,e.code," & empName & " as FullName from hrs_employees e inner join hrs_contracts c on e.id=c.employeeid where e.excludedate is null and e.canceldate is null and  (c.enddate >getdate() or c.EndDate is null)  and e.departmentID=" & ddlDepartment.SelectedValue & branchfilter & locFilter & "  order by Case When IsNumeric(e.Code) = 1 then Right(Replicate('0',51) + e.Code, 50) When IsNumeric(e.Code) = 0 then Left(e.Code + Replicate('',51), 50) Else e.Code End"
            ElseIf ddlBranche.SelectedValue = 0 And ddlDepartment.SelectedValue = 0 Then
                strselect = "select e.id,e.code," & empName & " as FullName from hrs_employees e inner join hrs_contracts c on e.id=c.employeeid where e.excludedate is null and e.canceldate is null and  (c.enddate >getdate() or c.EndDate is null) " & locFilter & "   order by Case When IsNumeric(e.Code) = 1 then Right(Replicate('0',51) + e.Code, 50) When IsNumeric(e.Code) = 0 then Left(e.Code + Replicate('',51), 50) Else e.Code End"

            End If
            'strselect = "select e.id,e.code,dbo.fn_GetEmpName (e.code,1) FullName from hrs_employees e inner join hrs_contracts c on e.id=c.employeeid where e.excludedate is null and e.canceldate is null and  (c.enddate >getdate() or c.EndDate is null)  and e.departmentID=" & clsDepartments.DataSet.Tables(0).Rows(i)("ID") & branchfilter & "  order by Case When IsNumeric(e.Code) = 1 then Right(Replicate('0',51) + e.Code, 50) When IsNumeric(e.Code) = 0 then Left(e.Code + Replicate('',51), 50) Else e.Code End"
            Dim dsemployees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Cls_Employees.ConnectionString, CommandType.Text, strselect)
            For Each rw As Data.DataRow In dsemployees.Tables(0).Rows
                Dim ObjTreeNodeSub As New Infragistics.WebUI.UltraWebNavigator.Node
                ObjTreeNodeSub.Text = rw("Code") + " " + rw("FullName")
                ObjTreeNodeSub.Tag = rw("ID")
                ObjTreeNode.Nodes.Add(ObjTreeNodeSub)
            Next
            UltraWebTree1.Nodes.Add(ObjTreeNode)
            UltraWebTree1.ExpandAll()
        Next i
    End Sub
    Private Sub GetEmployeeData()
        UltraWebTree1.Nodes.Clear()
        Dim Cls_Employees As New Clshrs_Employees(Me)
        Cls_Employees.Find("Code = '" & TxtEmpCode.Text & "'")
        Dim ObjNavigationHandle As New Venus.Shared.Web.NavigationHandler(Cls_AttendAppointment.ConnectionString)
        Dim clsDepartments As New Clssys_Departments(Me)

        Dim clsCompanies = New Clssys_Companies(Me.Page)
        clsCompanies.Find("ID = " & Cls_Employees.MainCompanyID)

        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler

        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")
        Dim ClsEmployees As New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & _sys_User.Code & "'")

        Dim departsPermission = ""
        If clsCompanies.UserDepartmentsPermissions Then
            Dim dsDepPermission As DataSet
            Dim str As String = "SELECT [DepartmentId] FROM [dbo].[hrs_EmployeeDepartments] where [EmpId]='" & ClsEmployees.ID & "'"
            dsDepPermission = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, str)
            departsPermission = "(-1"
            For i As Integer = 0 To dsDepPermission.Tables(0).Rows.Count - 1
                departsPermission = departsPermission & "," & Convert.ToString(dsDepPermission.Tables(0).Rows(i)("DepartmentId"))
            Next
            departsPermission = departsPermission & ")"
        End If
        Dim deprtPermFilter = ""
        If departsPermission <> "" Then
            deprtPermFilter = " and ID in " & departsPermission
        End If

        clsDepartments.Find("ID=" & Cls_Employees.DepartmentID & "" & deprtPermFilter)
        If clsDepartments.DataSet.Tables(0).Rows.Count > 0 Then


            Dim ObjTreeNode As New Infragistics.WebUI.UltraWebNavigator.Node
            ObjTreeNode.Text = clsDepartments.DataSet.Tables(0).Rows(0)((ObjNavigationHandle.SetLanguage(Me, "EngName/ArbName")))
            If (ObjTreeNode.Text.Trim = String.Empty) Then
                ObjTreeNode.Text = clsDepartments.DataSet.Tables(0).Rows(0)((ObjNavigationHandle.SetLanguage(Me, "ArbName/EngName")))
            End If
            '

            Dim ObjTreeNodeSub As New Infragistics.WebUI.UltraWebNavigator.Node
            ObjTreeNodeSub.Text = Cls_Employees.ArabicName
            ObjTreeNodeSub.Tag = Cls_Employees.ID
            ObjTreeNode.Nodes.Add(ObjTreeNodeSub)

            UltraWebTree1.Nodes.Add(ObjTreeNode)
            UltraWebTree1.ExpandAll()
        End If
    End Sub

    Private Sub GetEmployeesByDepartment()
        UltraWebTree1.Nodes.Clear()

        Dim ObjNavigationHandle As New Venus.Shared.Web.NavigationHandler(Cls_AttendAppointment.ConnectionString)
        Dim clsDepartments As New Clssys_Departments(Me)
        If ddlDepartment.SelectedValue > 0 Then


            clsDepartments.Find("ID = " & ddlDepartment.SelectedValue)
            For i As Integer = 0 To clsDepartments.DataSet.Tables(0).Rows.Count - 1
                Dim ObjTreeNode As New Infragistics.WebUI.UltraWebNavigator.Node
                ObjTreeNode.Text = clsDepartments.DataSet.Tables(0).Rows(i)((ObjNavigationHandle.SetLanguage(Me, "EngName/ArbName")))
                If (ObjTreeNode.Text.Trim = String.Empty) Then
                    ObjTreeNode.Text = clsDepartments.DataSet.Tables(0).Rows(i)((ObjNavigationHandle.SetLanguage(Me, "ArbName/EngName")))
                End If
                Dim clsCompanies = New Clssys_Companies(Me.Page)
                Dim User As String = String.Empty
                Dim WebHandler As New Venus.Shared.Web.WebHandler

                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")
                Dim ClsEmployees As New Clshrs_Employees(Page)
                ClsEmployees.Find("Code='" & _sys_User.Code & "'")
                clsCompanies.Find("ID = " & ClsEmployees.MainCompanyID)
                'Cls_Employees.Find("LocationID = " & ClsLocations.DataSet.Tables(0).Rows(i)("ID"))
                Dim branchfilter As String = ""
                If ddlBranche.SelectedValue > 0 Then
                    branchfilter = " and e.branchID=" & ddlBranche.SelectedValue
                End If
                Dim Deptfilter As String = ""
                'If ddlDepartment.SelectedValue > 0 Then
                '    Deptfilter = " and e.DepartmentID=" & ddlDepartment.SelectedValue
                'End If
                Dim LocsPermission = ""
                If clsCompanies.UserDepartmentsPermissions Then
                    Dim dsLocPermission As DataSet
                    Dim strLoc As String = "SELECT [LocationId] FROM [dbo].[hrs_EmployeeLocations] where [EmpId]='" & ClsEmployees.ID & "'"
                    dsLocPermission = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strLoc)
                    LocsPermission = "(-1"
                    For h As Integer = 0 To dsLocPermission.Tables(0).Rows.Count - 1
                        LocsPermission = LocsPermission & "," & Convert.ToString(dsLocPermission.Tables(0).Rows(h)("LocationId"))
                    Next
                    LocsPermission = LocsPermission & ")"
                End If
                Dim locFilter = ""
                If LocsPermission <> "" Then
                    locFilter = " and e.LocationId in " & LocsPermission
                End If
                Dim empName = "dbo.fn_GetEmpName (e.code,1)"
                If ProfileCls.CurrentLanguage = "Ar" Then

                    empName = "dbo.fn_GetEmpName (e.code,1)"
                Else
                    empName = "dbo.fn_GetEmpName (e.code,0)"
                End If
                Dim strselect = "select e.id,e.code," & empName & " FullName from hrs_employees e inner join hrs_contracts c on e.id=c.employeeid where e.excludedate is null and e.canceldate is null and  (c.enddate >getdate() or c.EndDate is null)  and e.departmentID=" & clsDepartments.DataSet.Tables(0).Rows(i)("ID") & branchfilter & locFilter & "  order by Case When IsNumeric(e.Code) = 1 then Right(Replicate('0',51) + e.Code, 50) When IsNumeric(e.Code) = 0 then Left(e.Code + Replicate('',51), 50) Else e.Code End"
                Dim dsemployees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strselect)
                For Each rw As Data.DataRow In dsemployees.Tables(0).Rows
                    Dim ObjTreeNodeSub As New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjTreeNodeSub.Text = rw("Code") + " " + rw("FullName")
                    ObjTreeNodeSub.Tag = rw("ID")
                    ObjTreeNode.Nodes.Add(ObjTreeNodeSub)
                Next
                UltraWebTree1.Nodes.Add(ObjTreeNode)
                UltraWebTree1.ExpandAll()
            Next i
        Else
            GetEmployees()
        End If

    End Sub

    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub TxtAttendShifts_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAttendShifts.TextChanged
        If Not String.IsNullOrEmpty(TxtAttendShifts.Text) Then
            Dim ClsAttendShifts = New ClsAtt_AttendShifts(Me)
            ClsAttendShifts.Find("Code='" & TxtAttendShifts.Text & "'")
            If Not String.IsNullOrEmpty(ClsAttendShifts.ArbName.ToString()) Then
                TxtAttendShiftName.Text = ClsAttendShifts.ArbName
            Else
                TxtAttendShiftName.Text = ""
            End If
        End If

    End Sub
    Protected Sub TxtEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtEmpCode.TextChanged
        If Not String.IsNullOrEmpty(TxtEmpCode.Text) Then
            Dim ClsEmployees = New Clshrs_Employees(Me)
            ClsEmployees.Find("Code='" & TxtEmpCode.Text & "'")
            If Not String.IsNullOrEmpty(ClsEmployees.ArabicName.ToString()) Then
                Dim clsCompanies = New Clssys_Companies(Me.Page)
                Dim Cls_Employeees As New Clshrs_Employees(Me)

                clsCompanies.Find("ID = " & Cls_Employeees.MainCompanyID)
                If clsCompanies.UserDepartmentsPermissions Then
                    Dim User As String = String.Empty
                    Dim WebHandler As New Venus.Shared.Web.WebHandler

                    WebHandler.GetCookies(Page, "UserID", User)
                    Dim _sys_User As New Clssys_Users(Page)
                    _sys_User.Find("ID = '" & User & "'")
                    Cls_Employeees.Find("Code='" & _sys_User.Code & "'")
                    Dim dsLocPermission As DataSet
                    Dim LocsPermission = ""

                    Dim strLoc As String = "SELECT [LocationId] FROM [dbo].[hrs_EmployeeLocations] where [EmpId]='" & Cls_Employeees.ID & "'"
                    dsLocPermission = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strLoc)
                    LocsPermission = "(-1"
                    For h As Integer = 0 To dsLocPermission.Tables(0).Rows.Count - 1
                        LocsPermission = LocsPermission & "," & Convert.ToString(dsLocPermission.Tables(0).Rows(h)("LocationId"))
                    Next
                    LocsPermission = LocsPermission & ")"
                    Dim strchecklocationEMP As String
                    Dim DSAuthEmployees As DataSet
                    strchecklocationEMP = "select * from hrs_Employees where code='" & TxtEmpCode.Text & "' and hrs_Employees.LocationID in " & LocsPermission & ""
                    DSAuthEmployees = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, strchecklocationEMP)
                    If DSAuthEmployees.Tables(0).Rows.Count > 0 Then
                        If ProfileCls.CurrentLanguage = "Ar" Then

                            TxtEmpName.Text = ClsEmployees.ArabicName
                        Else
                            TxtEmpName.Text = ClsEmployees.EnglishName
                        End If

                        GetEmployeeData()
                    Else
                        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(Cls_AttendAppointment.ConnectionString)

                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry you don't have a permission for this employee   / عفوا ليس لديك صلاحية علي هذا الموظف"))
                        TxtEmpName.Text = ""
                        UltraWebTree1.Nodes.Clear()

                    End If


                Else


                    If ProfileCls.CurrentLanguage = "Ar" Then

                        TxtEmpName.Text = ClsEmployees.ArabicName
                    Else
                        TxtEmpName.Text = ClsEmployees.EnglishName
                    End If

                    GetEmployeeData()

                End If


            Else
                TxtEmpName.Text = ""
            End If
        Else
            TxtEmpName.Text = ""
        End If

    End Sub

#End Region

#Region "Private Functions"
    Private Function DGSelect() As Integer
        Dim Count As Integer = 0
        For Each ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node In UltraWebTree1.Nodes

            For Each ObjTreeNodesub As Infragistics.WebUI.UltraWebNavigator.Node In ObjTreeNode.Nodes
                If ObjTreeNodesub.Checked = True Then
                    Count = Count + 1
                End If
            Next
        Next
        Return Count
    End Function
    Private Function SaveDG(ByVal Cls_AttendAppointmentID As Integer) As Boolean
        Try
            If Cls_AttendAppointmentID > 0 Then
                Dim str As String
                str = "delete from Att_AttendAppointmentMembers where AppointID = " & Cls_AttendAppointmentID & ";"
                For Each ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node In UltraWebTree1.Nodes

                    For Each ObjTreeNodesub As Infragistics.WebUI.UltraWebNavigator.Node In ObjTreeNode.Nodes
                        If ObjTreeNodesub.Checked = True Then
                            str &= " INSERT INTO Att_AttendAppointmentMembers (AppointID,EmployeeID,RegDate,RegUserID) VALUES (" & Cls_AttendAppointmentID & "," & ObjTreeNodesub.Tag & ",getdate()," & Cls_AttendAppointment.DataBaseUserRelatedID & ") ;"
                        End If
                    Next
                Next
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(Cls_AttendAppointment.ConnectionString, Data.CommandType.Text, str)
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function HigiriToGreg(ByRef date1 As DateTime) As Boolean
        Try
            If (ClsDataAcessLayer.IsHijri(date1.Date)) Then
                date1 = CDate(ClsDataAcessLayer.HijriToGreg(date1.ToShortDateString(), "dd/MM/yyyy"))
            End If

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function CheckDates(ByVal date1 As DateTime, ByVal date2 As DateTime) As Boolean
        Try
            If (date1.Date > date2.Date) Then
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function AssignValues() As Boolean
        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(Cls_AttendAppointment.ConnectionString)
            Dim strnextcode As String = ""
            'If txtCode.Text = "" Or txtCode.Text = "Auto" Or txtCode.Text = "تلقائى" Then

            Dim strcommand As String = "select isnull(max(convert(int,code)+1),1) from Att_AttendAppointment "

            Try
                strnextcode = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Cls_AttendAppointment.ConnectionString, Data.CommandType.Text, strcommand)
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
                Exit Function
            End Try

            ' End If
            Dim strshiftID As String
            Dim ShiftID As Integer
            strshiftID = "select ID from Att_AttendShifts  where code='" & TxtAttendShifts.Text & "' "
            ShiftID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Cls_AttendAppointment.ConnectionString, Data.CommandType.Text, strshiftID)

            With Cls_AttendAppointment
                .Code = strnextcode
                .AttendaceShiftID = ShiftID
                .FromDate = StartDate.Value
                .ToDate = EndDate.Value
                .RegDate = DateTime.Now
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsUser.ConnectionString)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With Cls_AttendAppointment
                txtCode.Text = .Code
                If Not String.IsNullOrEmpty(.AttendaceShiftID.ToString()) Then

                    Dim ClsAttendShifts = New ClsAtt_AttendShifts(Me)
                    ClsAttendShifts.Find("ID=" & .AttendaceShiftID & "")
                    If ClsAttendShifts.ID > 0 Then
                        TxtAttendShifts.Text = ClsAttendShifts.Code
                    End If
                End If
                'ddlAttendShifts.SelectedValue = .AttendaceShiftID
                'TxtAttendShifts.Text = .AttendaceShiftID
                StartDate.Value = .FromDate
                EndDate.Value = .ToDate
            End With

            If Not Cls_AttendAppointment.RegUserID = Nothing Then
                ClsUser.Find("ID=" & Cls_AttendAppointment.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(Cls_AttendAppointment.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(Cls_AttendAppointment.RegDate).Date
            End If
            If Cls_AttendAppointment.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(Cls_AttendAppointment.CancelDate).Date
            End If
            If Not String.IsNullOrEmpty(TxtAttendShifts.Text) Then
                TxtAttendShifts_TextChanged(Nothing, Nothing)
            End If
            If Not String.IsNullOrEmpty(TxtEmpCode.Text) Then
                TxtEmpCode_TextChanged(Nothing, Nothing)
            End If
            If Not Cls_AttendAppointment.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
                StrMode = "E"
                '''''''''''''''''''''''''''''''''''''''''''''''''''''
                UltraWebTree1.ClearAll()
                Dim ClsLocations As New Clssys_Locations(Me)
                ClsLocations.Find("CancelDate is null")
                For i As Integer = 0 To ClsLocations.DataSet.Tables(0).Rows.Count - 1
                    Dim ObjTreeNode As New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjTreeNode.Text = ClsLocations.DataSet.Tables(0).Rows(i)((ObjNavigationHandler.SetLanguage(Me, "EngName/ArbName")))
                    If (ObjTreeNode.Text.Trim = String.Empty) Then
                        ObjTreeNode.Text = ClsLocations.DataSet.Tables(0).Rows(i)((ObjNavigationHandler.SetLanguage(Me, "ArbName/EngName")))
                    End If
                    Dim Cls_Employees As New Clshrs_Employees(Me)
                    'Dim EmployeeValidContractDS As Data.DataSet
                    'Cls_Employees.GetAllEmployeeValidContractshift(EmployeeValidContractDS)
                    Cls_Employees.Find("LocationID = " & ClsLocations.DataSet.Tables(0).Rows(i)("ID"))
                    For Each rw As Data.DataRow In Cls_Employees.DataSet.Tables(0).Rows
                        Dim ObjTreeNodeSub As New Infragistics.WebUI.UltraWebNavigator.Node
                        ObjTreeNodeSub.Text = rw("Code") + " " + rw("FullName")
                        ObjTreeNodeSub.Tag = rw("ID")
                        ObjTreeNode.Nodes.Add(ObjTreeNodeSub)
                    Next
                    UltraWebTree1.Nodes.Add(ObjTreeNode)
                    UltraWebTree1.ExpandAll()
                Next i
                '''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim Cls_AttendAppointmentMembers As New Clshrs_Att_AttendAppointmentMembers(Me)
                Cls_AttendAppointmentMembers.Find("AppointID = " & Cls_AttendAppointment.ID)

                For Each rw As Data.DataRow In Cls_AttendAppointmentMembers.DataSet.Tables(0).Rows
                    For Each ObjTreeNode In UltraWebTree1.Nodes

                        For Each ObjTreeNodesub As Infragistics.WebUI.UltraWebNavigator.Node In ObjTreeNode.Nodes
                            If ObjTreeNodesub.Tag = rw("EmployeeID") Then
                                ObjTreeNodesub.Checked = True
                            End If
                        Next

                    Next
                Next

                StrMode = "N"
            End If
            SetToolBarPermission(Me, Cls_AttendAppointment.ConnectionString, Cls_AttendAppointment.DataBaseUserRelatedID, Cls_AttendAppointment.GroupID, StrMode)
            SetToolBarRecordPermission(Me, Cls_AttendAppointment.ConnectionString, Cls_AttendAppointment.DataBaseUserRelatedID, Cls_AttendAppointment.GroupID, Cls_AttendAppointment.Table, Cls_AttendAppointment.ID)
            If Not Cls_AttendAppointment.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(Cls_AttendAppointment.ID)
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
                    Cls_AttendAppointment.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    Cls_AttendAppointment.Find("ID=" & intID)
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
            With Cls_AttendAppointment
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
        Cls_AttendAppointment = New Clshrs_Att_AttendAppointment(Me)
        Try
            With Cls_AttendAppointment
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
        Cls_AttendAppointment = New Clshrs_Att_AttendAppointment(Me)
        If IntId > 0 Then
            Cls_AttendAppointment.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Cls_AttendAppointment = New Clshrs_Att_AttendAppointment(Me)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(Cls_AttendAppointment.ConnectionString)
        Try
            Cls_AttendAppointment.Find("Code='" & txtCode.Text & "'")
            IntId = Cls_AttendAppointment.ID
            'txtEngName.Focus()
            If Cls_AttendAppointment.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If Cls_AttendAppointment.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                'txtEngName.Focus()
                CreateOtherFields(0)
                Dim ClsLocations As New Clssys_Locations(Me)
                ClsLocations.Find("CancelDate is null")
                For i As Integer = 0 To ClsLocations.DataSet.Tables(0).Rows.Count - 1
                    Dim ObjTreeNode As New Infragistics.WebUI.UltraWebNavigator.Node
                    ObjTreeNode.Text = ClsLocations.DataSet.Tables(0).Rows(i)((ObjNavigationHandler.SetLanguage(Me, "EngName/ArbName")))
                    If (ObjTreeNode.Text.Trim = String.Empty) Then
                        ObjTreeNode.Text = ClsLocations.DataSet.Tables(0).Rows(i)((ObjNavigationHandler.SetLanguage(Me, "ArbName/EngName")))
                    End If
                    Dim Cls_Employees As New Clshrs_Employees(Me)
                    'Dim EmployeeValidContractDS As Data.DataSet
                    Cls_Employees.Find("LocationID = " & ClsLocations.DataSet.Tables(0).Rows(i)("ID"))
                    'Cls_Employees.GetAllEmployeeValidContractshift(EmployeeValidContractDS)
                    For Each rw As Data.DataRow In Cls_Employees.DataSet.Tables(0).Rows
                        Dim ObjTreeNodeSub As New Infragistics.WebUI.UltraWebNavigator.Node
                        ObjTreeNodeSub.Text = rw("Code") + " " + rw("FullName")
                        ObjTreeNodeSub.Tag = rw("ID")
                        ObjTreeNode.Nodes.Add(ObjTreeNodeSub)
                    Next
                    UltraWebTree1.Nodes.Add(ObjTreeNode)
                    UltraWebTree1.ExpandAll()
                Next i

            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, Cls_AttendAppointment.ConnectionString, Cls_AttendAppointment.DataBaseUserRelatedID, Cls_AttendAppointment.GroupID, StrMode)
            SetToolBarRecordPermission(Me, Cls_AttendAppointment.ConnectionString, Cls_AttendAppointment.DataBaseUserRelatedID, Cls_AttendAppointment.GroupID, Cls_AttendAppointment.Table, IntId)
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
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(Cls_AttendAppointment.ConnectionString)
        Cls_AttendAppointment.Clear()
        'UltraWebTree1.Nodes.Clear()
        'ddlAttendShifts.SelectedValue = 0
        TxtAttendShifts.Text = ""
        StartDate.Value = ""
        EndDate.Value = ""
        TxtAttendShiftName.Text = ""
        For Each ObjTreeNode As Infragistics.WebUI.UltraWebNavigator.Node In UltraWebTree1.Nodes

            For Each ObjTreeNodesub As Infragistics.WebUI.UltraWebNavigator.Node In ObjTreeNode.Nodes
                If ObjTreeNodesub.Checked = True Then
                    ObjTreeNodesub.Checked = False
                End If
            Next
        Next
        'GetValues()
        ImageButton_Delete.Enabled = False
        txtCode.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Auto/تلقائى")
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        'txtEngName.Text = String.Empty
        'txtArbName.Text = String.Empty

        'ddlAttendShifts.SelectedValue = 0
        TxtAttendShifts.Text = ""

        StartDate.Value = ""
        EndDate.Value = ""
        UltraWebTree1.Nodes.Clear()
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        TxtAttendShiftName.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        Cls_AttendAppointment = New Clshrs_Att_AttendAppointment(Page)
        Cls_AttendAppointment.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = Cls_AttendAppointment.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, Cls_AttendAppointment.Table) = True Then
            Dim StrTablename As String
            Cls_AttendAppointment = New Clshrs_Att_AttendAppointment(Me)
            StrTablename = Cls_AttendAppointment.Table
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



    Protected Sub ddlBranche_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranche.SelectedIndexChanged

        If ddlBranche.SelectedValue <> 0 Then
            Dim ClsDepartment As New Clssys_Departments(Me.Page)
            Dim clsCompanies = New Clssys_Companies(Me.Page)
            Dim Cls_Employees As New Clshrs_Employees(Me)
            clsCompanies.Find("ID = " & Cls_Employees.MainCompanyID)
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler

            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Cls_Employees.Find("Code='" & _sys_User.Code & "'")
            Dim departsPermission = ""
            Dim LocsPermission = ""
            If clsCompanies.UserDepartmentsPermissions Then
                Dim dsDepPermission As DataSet
                Dim str As String = "SELECT [DepartmentId] FROM [dbo].[hrs_EmployeeDepartments] where [EmpId]='" & Cls_Employees.ID & "'"
                dsDepPermission = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Cls_Employees.ConnectionString, CommandType.Text, str)
                departsPermission = "(-1"
                For i As Integer = 0 To dsDepPermission.Tables(0).Rows.Count - 1
                    departsPermission = departsPermission & "," & Convert.ToString(dsDepPermission.Tables(0).Rows(i)("DepartmentId"))
                Next
                departsPermission = departsPermission & ")"

                Dim dsLocPermission As DataSet
                Dim strLoc As String = "SELECT [LocationId] FROM [dbo].[hrs_EmployeeLocations] where [EmpId]='" & Cls_Employees.ID & "'"
                dsLocPermission = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Cls_Employees.ConnectionString, CommandType.Text, strLoc)
                LocsPermission = "(-1"
                For i As Integer = 0 To dsLocPermission.Tables(0).Rows.Count - 1
                    LocsPermission = LocsPermission & "," & Convert.ToString(dsLocPermission.Tables(0).Rows(i)("LocationId"))
                Next
                LocsPermission = LocsPermission & ")"
            End If
            Dim deprtPermFilter = ""
            If departsPermission <> "" Then
                deprtPermFilter = " and ID in " & departsPermission
            End If
            Dim locFilter = ""
            If LocsPermission <> "" Then
                locFilter = " and ID in " & LocsPermission
            End If
            ClsDepartment.GetDropDownList(ddlDepartment, True, "ID in (select DepartmentID from sys_DepartmentsBranches where Checked = 1 and BranchID = " & ddlBranche.SelectedValue & ") " & deprtPermFilter)
            ddlDepartment_SelectedIndexChanged(Nothing, Nothing)
            ' ddlBranche.Focus()
        Else
            ddlDepartment.SelectedIndex = -1
        End If


        Dim clsDept As New Clssys_Departments(Page)
        GetEmployees()
        'clsDept.GetDropDownListWithBranches(ddlDepartment, True, " sys_DepartmentsBranches.BranchID = " & ddlBranche.SelectedValue)
    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartment.SelectedIndexChanged
        GetEmployeesByDepartment()
    End Sub
End Class
