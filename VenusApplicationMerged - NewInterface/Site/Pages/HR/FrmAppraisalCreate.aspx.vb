Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Partial Class frmHIPolicy
    Inherits MainPage

#Region "Public Decleration"
    Dim ClsAppraisal As ClsHrs_AppraisalCreate
    Dim clsEmployees As Clshrs_Employees
    Dim clsContracts As Clshrs_Contracts
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsAppraisal = New ClsHrs_AppraisalCreate(Me)
        Dim clsEmployees As New Clshrs_Employees(Me)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then

                Page.Session.Add("ConnectionString", ClsAppraisal.ConnectionString)

                UwgSearchEmployees.Columns.FromKey("FullName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))

                lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lang", lblLage.Text)
                Page.Session.Add("ConnectionString", clsEmployees.ConnectionString)
                Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
                If ClsObjects.Find(" Code='" & ClsAppraisal.Table.Trim & "'") Then
                    If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                        SearchID = ClsSearchs.ID
                        Dim IntDimension As Integer = 510
                        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & TxtCode.ClientID & "'"
                        btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                    End If
                End If

                If ClsObjects.Find(" Code='" & clsEmployees.Table.Trim & "'") Then
                    If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                        SearchID = ClsSearchs.ID

                        Dim IntDimension As Integer = 510
                        Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtEmpCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & TxtEmpCode.ClientID & "'"
                        BtnSearchEmp.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                    End If
                End If
                Dim clsAppraisalType As New Clshrs_AppraisalTypes(Me)
                Dim clsBranches As New Clssys_Branches(Me)
                Dim clsSectors As New ClsSys_Sectors(Me)
                Dim clsDepartments As New Clssys_Departments(Me)
                Dim clsProjects As New Clshrs_Projects(Page, "hrs_Projects")
                Dim clsPositions As New Clshrs_Positions(Me)


                clsAppraisalType.GetDropDownList(ddlappraisalType, True)
                clsBranches.GetDropDownList(ddlBranch, True)
                clsPositions.GetDropDownList(ddlPosition, True)
                clsSectors.GetDropDownList(ddlSector, True)
                clsDepartments.GetDropDownList(ddldepartment, True)
                clsProjects.GetDropDownList(ddlProject, True)
                'txtFromDate.Value = DateTime.Now.ToString("dd/MM/yyyy")
                'txtToDate.Value = DateTime.Now.ToString("dd/MM/yyyy")
                Dim CriteriaName As String
                If ProfileCls.CurrentLanguage = "Ar" Then
                    CriteriaName = "ArbName"
                Else
                    CriteriaName = "EngName"

                End If
                Dim strselect2 As String
                strselect2 = "select ID," & CriteriaName & " as CriteriaName  from App_Criteria"
                Dim DSCriterias As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsAppraisal.ConnectionString, CommandType.Text, strselect2)
                If DSCriterias.Tables(0).Rows.Count > 0 Then
                    UwgCriteria.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Clear()
                    For Each Row As Data.DataRow In DSCriterias.Tables(0).Rows
                        UwgCriteria.DisplayLayout.Bands(0).Columns(1).ValueList.ValueListItems.Add(Row("ID"), Row("CriteriaName"))
                    Next
                End If
                '================================= Exit & Navigation Notification [ End ]
                'Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (TxtCode.Text <> "") Then
                ClsAppraisal.Find(" Code='" & TxtCode.Text & "'")
                IntrecordID = ClsAppraisal.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsAppraisal.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsAppraisal = New ClsHrs_AppraisalCreate(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisal.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"

                If String.IsNullOrEmpty(TxtCode.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If String.IsNullOrEmpty(TxtArbName.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Arab Name /برجاء إدخال الاسم عربي"))
                    Exit Sub
                End If
                If String.IsNullOrEmpty(txtEngName.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter English Name /برجاء إدخال الاسم بالانجليزية"))
                    Exit Sub
                End If
                If ddlappraisalType.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Appraisal Type /برجاء اختيار نوع التقييم"))
                    Exit Sub
                End If
                If UwgCriteria.Rows.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Sorry...Can't Save without Appraisal Criteria/عفوا ... لايمكن الحفظ بدون وجود معايير للتقييم"))
                    Exit Sub
                End If
                If UwgSearchEmployees.Rows.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Can't Save without Appraisal Employees /عفوا ... لايمكن الحفظ بدون وجود موظفين للتقييم"))
                End If
                Dim TotalWeight As Integer = 0

                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgCriteria.Rows
                    If Not String.IsNullOrEmpty(DGRow.Cells.FromKey("CriteriaName").Value) Then

                        TotalWeight += DGRow.Cells.FromKey("Weight").Value

                    End If
                Next
                If TotalWeight <> 100 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Total Appraisal Criterias Weight should Equal to 100 Points / عفوا...لابد ان يكون اجمالي نقاط المعايير يساوي 100 نقطة"))
                    Exit Sub
                End If

                Dim SelectedEmployees As Integer = 0


                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                    If Not String.IsNullOrEmpty(DGRow.Cells.FromKey("FullName").Value) And DGRow.Cells.FromKey("Select").Value Then

                        SelectedEmployees += 1

                    End If
                Next
                If SelectedEmployees < 1 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...You Should Apply at least one employee to this appraisal / عفوا...لابد من اختيار موظف واحد علي الاقل لحفظ التقييم"))
                    Exit Sub
                End If
                ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsAppraisal.ID > 0 Then
                    If CheckAppraisalNotificationsExists(ClsAppraisal.ID) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Editing is not possible because there are settings configured for this Appraisal  / عفوا...لا يمكن التعديل لانه يوجد اعدادات لهذا التقييم"))
                        Exit Sub

                    End If

                    ClsAppraisal.Update("ID='" & ClsAppraisal.ID & "'")
                    SaveAppraisalCriteria(ClsAppraisal.ID)
                    SaveAppraisalEmployees(ClsAppraisal.ID)
                Else
                    If ClsAppraisal.Save() Then
                        ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                        SaveAppraisalCriteria(ClsAppraisal.ID)
                        SaveAppraisalEmployees(ClsAppraisal.ID)
                    End If
                End If

                ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsAppraisal.Table, ClsAppraisal.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"

                If String.IsNullOrEmpty(TxtCode.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If String.IsNullOrEmpty(TxtArbName.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Arab Name /برجاء إدخال الاسم عربي"))
                    Exit Sub
                End If
                If String.IsNullOrEmpty(txtEngName.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter English Name /برجاء إدخال الاسم بالانجليزية"))
                    Exit Sub
                End If
                If ddlappraisalType.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Appraisal Type /برجاء اختيار نوع التقييم"))
                    Exit Sub
                End If
                If UwgCriteria.Rows.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Sorry...Can't Save without Appraisal Criteria/عفوا ... لايمكن الحفظ بدون وجود معايير للتقييم"))
                    Exit Sub
                End If
                If UwgSearchEmployees.Rows.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Can't Save without Appraisal Employees /عفوا ... لايمكن الحفظ بدون وجود موظفين للتقييم"))
                End If
                Dim TotalWeight As Integer = 0

                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgCriteria.Rows
                    If Not String.IsNullOrEmpty(DGRow.Cells.FromKey("CriteriaName").Value) Then

                        TotalWeight += DGRow.Cells.FromKey("Weight").Value

                    End If
                Next
                If TotalWeight <> 100 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Total Appraisal Criterias Weight should Equal to 100 Points / عفوا...لابد ان يكون اجمالي نقاط المعايير يساوي 100 نقطة"))
                    Exit Sub
                End If

                Dim SelectedEmployees As Integer = 0

                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                    If Not String.IsNullOrEmpty(DGRow.Cells.FromKey("FullName").Value) And DGRow.Cells.FromKey("Select").Value Then

                        SelectedEmployees += 1

                    End If
                Next
                If SelectedEmployees < 1 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...You Should Apply at least one employee to this appraisal / عفوا...لابد من اختيار موظف واحد علي الاقل لحفظ التقييم"))
                    Exit Sub
                End If
                ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsAppraisal.ID > 0 Then
                    If CheckAppraisalNotificationsExists(ClsAppraisal.ID) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Editing is not possible because there are settings configured for this Appraisal  / عفوا...لا يمكن التعديل لانه يوجد اعدادات لهذا التقييم"))
                        Exit Sub

                    End If

                    ClsAppraisal.Update("ID='" & ClsAppraisal.ID & "'")
                    SaveAppraisalCriteria(ClsAppraisal.ID)
                    SaveAppraisalEmployees(ClsAppraisal.ID)
                Else
                    If ClsAppraisal.Save() Then
                        ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                        SaveAppraisalCriteria(ClsAppraisal.ID)
                        SaveAppraisalEmployees(ClsAppraisal.ID)
                    End If
                End If
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsAppraisal.Table, ClsAppraisal.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                If ClsAppraisal.ID > 0 Then
                    If CheckAppraisalNotificationsExists(ClsAppraisal.ID) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...You can't delete because there are settings configured for this Appraisal  / عفوا...لا يمكن الحذف لوجود اعدادات لهذا التقييم"))
                        Exit Sub

                    End If
                End If


                ClsAppraisal.Delete("Code='" & TxtCode.Text & "'")
                AfterOperation()

            Case "Property"
                ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsAppraisal.ID & "&TableName=" & ClsAppraisal.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsAppraisal.ID & "&TableName=" & ClsAppraisal.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsAppraisal.Table
                ClsAppraisal.Find(" code = '" & TxtCode.Text & "'")
                Dim recordID As Integer = ClsAppraisal.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsAppraisal.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsAppraisal.Find(" Code= '" & TxtCode.Text & "'")
                If ClsAppraisal.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsAppraisal.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsAppraisal.CheckDiff(ClsAppraisal, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsAppraisal.FirstRecord()
                GetValues()
            Case "Previous"
                ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                If Not ClsAppraisal.PreviousRecord() Then
                    ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                If Not ClsAppraisal.NextRecord() Then
                    ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsAppraisal.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub TxtEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtEmpCode.TextChanged
        If Not String.IsNullOrEmpty(TxtEmpCode.Text) Then
            clsEmployees = New Clshrs_Employees(Me)
            clsEmployees.Find("Code='" & TxtEmpCode.Text & "'")
            LblEmpName.Text = clsEmployees.FullName
        Else
            LblEmpName.Text = ""

        End If

    End Sub
    Protected Sub ddl_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBranch.SelectedIndexChanged, ddlSector.SelectedIndexChanged, ddldepartment.SelectedIndexChanged, ddlPosition.SelectedIndexChanged, ddlProject.SelectedIndexChanged

        UwgSearchEmployees.DataSource = Nothing
        UwgSearchEmployees.DataBind()
        UwgCriteria.DataSource = Nothing
        UwgCriteria.DataBind()
    End Sub
    Protected Sub Chk_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDepartment.CheckedChanged, ChkPosition.CheckedChanged

        UwgSearchEmployees.DataSource = Nothing
        UwgSearchEmployees.DataBind()
        UwgCriteria.DataSource = Nothing
        UwgCriteria.DataBind()
    End Sub
    Protected Sub UwgCriteria_Cellchanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles UwgCriteria.UpdateCell

        If e.Cell.Column.Index = 2 Then
            If Convert.ToBoolean(e.Cell.Row.Cells(2).Value) = True Then
                e.Cell.Row.Cells(3).Value = 0
            End If
        End If
        If e.Cell.Column.Index = 3 Then
            If Convert.ToBoolean(e.Cell.Row.Cells(3).Value) = True Then
                e.Cell.Row.Cells(2).Value = 0
            End If
        End If


    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ClsAppraisal = New ClsHrs_AppraisalCreate(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisal.ConnectionString)


        If String.IsNullOrEmpty(TxtCode.Text) Then

            Exit Sub
        Else
            ClsAppraisal.Find("Code=" & TxtCode.Text & "")
        End If

        If ClsAppraisal.ID > 0 Then
            Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim Lang As String = CType(HttpContext.Current.Session("Lang"), String)
            Dim str As String = "Select count (ID) from App_AppraisalNotifications where AppraisalID=" & ClsAppraisal.ID & ""
            Dim NotificationCount As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, str)
            If NotificationCount > 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Can't Modify, This Appraisal was Configured before /  عفوا...لا يمكن التعديل هذا التقييم تم تفعيله من قبل)"))
                Exit Sub
            ElseIf chkDepartment.Checked = False And ChkPosition.Checked = False Then


                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Plase Select Search Criteria (Department / Position) /  برجاء تحديد معيار البحث (القسم / الوظيفة)"))
                Exit Sub

            Else
                GetDepartmentorPositionsCriteriasToGrid()
                GetEmployeesToGrid()
            End If
        Else
            If chkDepartment.Checked = False And ChkPosition.Checked = False Then


                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Plase Select Search Criteria (Department / Position) /  برجاء تحديد معيار البحث (القسم / الوظيفة)"))
                Exit Sub

            Else
                GetDepartmentorPositionsCriteriasToGrid()
                GetEmployeesToGrid()
            End If
        End If





    End Sub

#End Region

#Region "Private Functions"
    Private Function AssignValues() As Boolean
        Try
            Dim clsEmployees As New Clshrs_Employees(Me)

            clsEmployees.Find(" Code = '" & TxtEmpCode.Text & "'")
            With ClsAppraisal
                .Code = TxtCode.Text
                .EngName = txtEngName.Text
                .ArabName = TxtArbName.Text
                '.FromDate = txtFromDate.Value
                '.ToDate = txtToDate.Value
                .AppraisalTypeID = ddlappraisalType.SelectedValue
                .ByDepartment = chkDepartment.Checked
                .ByPosition = ChkPosition.Checked
                .BranchID = ddlBranch.SelectedItem.Value
                .SectorID = ddlSector.SelectedValue
                .DepartmentID = ddldepartment.SelectedValue
                .PositionID = ddlPosition.SelectedValue
                .UnitID = ddlProject.SelectedValue
                .EmployeeID = clsEmployees.ID
                .Notes = txtNotes.Text
                .RegDate = DateTime.Now
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            'SetToolBarDefaults()
            TxtCode.Text = ClsAppraisal.Code
            txtEngName.Text = ClsAppraisal.EngName
            TxtArbName.Text = ClsAppraisal.ArabName
            'txtFromDate.Value = ClsAppraisal.FromDate
            'txtToDate.Value = ClsAppraisal.ToDate
            ddlappraisalType.SelectedValue = ClsAppraisal.AppraisalTypeID
            ddlPosition.SelectedValue = ClsAppraisal.PositionID
            ddlBranch.SelectedValue = ClsAppraisal.BranchID
            ddlSector.SelectedValue = ClsAppraisal.SectorID
            ddldepartment.SelectedValue = ClsAppraisal.DepartmentID
            txtNotes.Text = ClsAppraisal.Notes
            chkDepartment.Checked = ClsAppraisal.ByDepartment
            ChkPosition.Checked = ClsAppraisal.ByPosition
            UwgSearchEmployees.Rows.Add()
            UwgCriteria.DataSource = GetAppraisalCriteria(ClsAppraisal.ID)
            UwgCriteria.DataBind()
            UwgSearchEmployees.DataSource = GetAppraisalEmployees(ClsAppraisal.ID)
            UwgSearchEmployees.DataBind()
            If Not ClsAppraisal.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsAppraisal.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsAppraisal.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsAppraisal.RegDate).Date
            End If
            If ClsAppraisal.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsAppraisal.CancelDate).Date
            End If
            If Not ClsAppraisal.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()

            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisal.ConnectionString)
            If (ClsAppraisal.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            'SetToolBarPermission(Me, ClsAppraisal.ConnectionString, ClsAppraisal.DataBaseUserRelatedID, ClsAppraisal.GroupID, StrMode)
            'SetToolBarRecordPermission(Me, ClsAppraisal.ConnectionString, ClsAppraisal.DataBaseUserRelatedID, ClsAppraisal.GroupID, ClsAppraisal.Table, ClsAppraisal.ID)
            If Not ClsAppraisal.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsAppraisal.ID)
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
                    TxtCode.Text = String.Empty
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
                    ClsAppraisal.Find("ID=" & intID)
                    GetValues()
                    TxtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsAppraisal.Find("ID=" & intID)
                    GetValues()
                    TxtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsAppraisal
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    'SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
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
        ClsAppraisal = New ClsHrs_AppraisalCreate(Me)
        Try
            With ClsAppraisal
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Page, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                'SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsAppraisal = New ClsHrs_AppraisalCreate(Me)
        If IntId > 0 Then
            ClsAppraisal.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsAppraisal = New ClsHrs_AppraisalCreate(Me)
        Try
            ClsAppraisal.Find("Code='" & TxtCode.Text & "'")
            IntId = ClsAppraisal.ID
            'txtEngName.Focus()
            If ClsAppraisal.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsAppraisal.CheckRecordExistance(" Code='" & TxtCode.Text & "'") Then
                    TxtCode.Text = ""
                    TxtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)

            End If
            'SetToolBarDefaults()
            'SetToolBarPermission(Me, ClsAppraisal.ConnectionString, ClsAppraisal.DataBaseUserRelatedID, ClsAppraisal.GroupID, StrMode)
            'SetToolBarRecordPermission(Me, ClsAppraisal.ConnectionString, ClsAppraisal.DataBaseUserRelatedID, ClsAppraisal.GroupID, ClsAppraisal.Table, IntId)
            'If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
            '    ImageButton_Delete.Enabled = False
            'End If
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
        ClsAppraisal.Clear()
        Clear()
        GetValues()
        ImageButton_Delete.Enabled = False
        UwgSearchEmployees.Rows.Clear()
        UwgSearchEmployees.Rows.Add()


        Venus.Shared.Web.ClientSideActions.SetFocus(Page, TxtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        TxtArbName.Text = String.Empty
        'txtFromDate.Value = DateTime.Now.ToString("dd/MM/yyyy")
        'txtToDate.Value = DateTime.Now.ToString("dd/MM/yyyy")
        ddlappraisalType.SelectedValue = 0
        ddlPosition.SelectedValue = 0
        ddlBranch.SelectedValue = 0
        ddlSector.SelectedValue = 0
        ddldepartment.SelectedValue = 0
        txtNotes.Text = String.Empty
        UwgSearchEmployees.DataSource = Nothing
        UwgSearchEmployees.DataBind()
        UwgCriteria.DataSource = Nothing
        UwgCriteria.DataBind()
        UwgSearchEmployees.DataBind()
        UwgCriteria.DataBind()
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function

    Protected Sub UwgSearchEmployees_Cellchanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles UwgSearchEmployees.UpdateCell
        'If e.Cell.Key = "Code" Then
        '    Dim EmpCode As String = e.Cell.Row.Cells.FromKey("Code").Value
        '    Dim EmpName As String = GetEmpName(EmpCode)
        '    e.Cell.Row.Cells.FromKey("FullName").Value = EmpName
        'End If

    End Sub

    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsAppraisal.Table) = True Then
            Dim StrTablename As String
            ClsAppraisal = New ClsHrs_AppraisalCreate(Me)
            StrTablename = ClsAppraisal.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
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

#Region "Public Shared Function"

    <System.Web.Services.WebMethod()>
    Public Shared Function GetEmpName(ByVal mCode As String) As String
        Try
            Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim Lang As String = CType(HttpContext.Current.Session("Lang"), String)

            Dim EmpName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, "Select dbo.fn_GetEmpName('" & mCode & "'," & Lang & ")")

            Return EmpName
        Catch ex As Exception
            Return ""
        End Try
    End Function
#Region "Appraisal Criteria Methods"

    '========================================================================
    'ProcedureName  :  SaveAppraisalCriteria
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Save data to APP_AppraisalCriterias table
    '========================================================================
    Public Function SaveAppraisalCriteria(ByVal appraisalID As Integer) As Boolean
        ClsAppraisal = New ClsHrs_AppraisalCreate(Me)
        ClsAppraisal.Find("ID=" & appraisalID & "")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisal.ConnectionString)
        Dim connection As New SqlConnection(ClsAppraisal.ConnectionString)
        Dim transaction As SqlTransaction = Nothing

        Try



            Dim ConnectionString As String
            ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
            Dim deleteCommand As String
            deleteCommand = "Delete from APP_AppraisalCriterias WHERE AppraisalID = " & appraisalID & ""
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, deleteCommand)


            Dim insertCommand As String = ""

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgCriteria.Rows
                If Not String.IsNullOrEmpty(DGRow.Cells.FromKey("CriteriaName").Value) Then

                    insertCommand = "INSERT INTO APP_AppraisalCriterias  (AppraisalID, CriteriaID, ByValue,ByPercentage,MinimumScore,MaximumScore, Weight, RegUserID, RegDate)  VALUES (" & appraisalID & ", " & DGRow.Cells.FromKey("CriteriaName").Value & ", '" & DGRow.Cells.FromKey("ByValue").Value & "' , '" & DGRow.Cells.FromKey("ByPercentage").Value & "' , '" & DGRow.Cells.FromKey("MinimumScore").Value & "' , '" & DGRow.Cells.FromKey("MaximumScore").Value & "', '" & DGRow.Cells.FromKey("Weight").Value & "', '" & ClsAppraisal.RegUserID & "', GETDATE())"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, insertCommand)

                End If
            Next

            Return True

        Catch ex As Exception
            If transaction IsNot Nothing Then
                transaction.Rollback()
            End If
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsAppraisal.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function
    Public Function SaveAppraisalEmployees(ByVal appraisalID As Integer) As Boolean
        ClsAppraisal = New ClsHrs_AppraisalCreate(Me)
        ClsAppraisal.Find("ID=" & appraisalID & "")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisal.ConnectionString)
        Dim connection As New SqlConnection(ClsAppraisal.ConnectionString)
        Dim transaction As SqlTransaction = Nothing

        Try
            Dim ConnectionString As String
            ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
            Dim deleteCommand As String
            deleteCommand = "Delete from APP_AppraisalEmployees WHERE AppraisalID = " & appraisalID & ""
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, deleteCommand)


            Dim insertCommand As String = ""
            Dim Include As Integer = False
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                Include = DGRow.Cells.FromKey("Select").Value
                If Not String.IsNullOrEmpty(DGRow.Cells.FromKey("Code").Value) And Include Then

                    insertCommand = "INSERT INTO APP_AppraisalEmployees  (AppraisalID, EmployeeID)  VALUES (" & appraisalID & ", " & DGRow.Cells.FromKey("ID").Value & ")"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, insertCommand)

                End If
            Next

            Return True

        Catch ex As Exception
            If transaction IsNot Nothing Then
                transaction.Rollback()
            End If
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsAppraisal.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function


    Public Function GetAppraisalCriteria(ByVal appraisalID As Integer) As DataTable
        Try
            Dim query As String = "SELECT ID, CriteriaID as CriteriaName , ByValue,ByPercentage,MinimumScore,MaximumScore, Weight " &
                             "FROM APP_AppraisalCriterias " &
                             "WHERE AppraisalID = @AppraisalID   " &
                             "ORDER BY CriteriaID"

            Dim parameters As SqlParameter() = {
            New SqlParameter("@AppraisalID", appraisalID)
        }

            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(
            ClsAppraisal.ConnectionString,
            CommandType.Text,
            query,
            parameters).Tables(0)

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsAppraisal.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetAppraisalEmployees(ByVal appraisalID As Integer) As DataTable
        Try
            Dim ConnectionString As String
            ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
            Dim strselect As String
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisal.ConnectionString)
            Dim EmpName As String
            Dim DepName As String
            Dim criteria As String = "Where 1=1 "
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = "dbo.fn_getempName(hrs_Employees.Code,1)"
                DepName = "sys_Departments.ArbName"
            Else
                EmpName = "dbo.fn_getempName(hrs_Employees.Code,0)"
                DepName = "sys_Departments.EngName"


            End If
            strselect = "select Hrs_Employees.ID, Hrs_Employees.Code , " & EmpName & " As FullName," & DepName & " As Department from hrs_Employees join APP_AppraisalEmployees on hrs_employees.ID=APP_AppraisalEmployees.EmployeeID join sys_Departments on hrs_Employees.DepartmentID=sys_Departments.id   where AppraisalID=" & appraisalID & ""

            Dim DSEmployees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
            Return DSEmployees.Tables(0)
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsAppraisal.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function



#End Region
    Private Sub GetDepartmentorPositionsCriteriasToGrid()
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strselect As String
        Dim CriteriaName As String
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisal.ConnectionString)

        If ProfileCls.CurrentLanguage = "Ar" Then
            CriteriaName = "ArbName"
        Else
            CriteriaName = "EngName"

        End If
        If chkDepartment.Checked Then
            If ddldepartment.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Department /برجاء إختيار القسم"))
                Exit Sub
            End If
            strselect = "select App_Criteria.id,App_Criteria.id as CriteriaName ,ByValue,ByPercentage,App_DepartmentCriteria.MinimumScore,App_DepartmentCriteria.MaximumScore,1 as Include from App_DepartmentCriteria join App_Criteria on App_DepartmentCriteria.CriteriaID=App_Criteria.ID where App_DepartmentCriteria.DepartmentID= " & ddldepartment.SelectedValue & ""
        ElseIf ChkPosition.Checked Then
            If ddlPosition.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Position /برجاء إختيار الوظيفة"))
                Exit Sub
            End If
            strselect = "select App_Criteria.id,App_Criteria.id as CriteriaName ,ByValue,ByPercentage,App_PositionCriteria.MinimumScore,App_PositionCriteria.MaximumScore,1 as Include from App_PositionCriteria join App_Criteria on App_PositionCriteria.CriteriaID=App_Criteria.ID where App_PositionCriteria.PositionID= " & ddlPosition.SelectedValue & ""

        End If


        Dim DSDepartmentCriteria As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
        UwgCriteria.DataSource = Nothing

        If DSDepartmentCriteria.Tables(0).Rows.Count > 0 Then
            UwgCriteria.DataSource = DSDepartmentCriteria
            UwgCriteria.DataBind()

        End If
    End Sub

    Private Function CheckAppraisalNotificationsExists(ByVal appraisalID As Integer) As Boolean
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strselect As String
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisal.ConnectionString)


        strselect = "select count(ID) from App_AppraisalNotifications where AppraisalID=" & appraisalID & " "

        Dim CountNotifications As Integer
        CountNotifications = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnectionString, Data.CommandType.Text, strselect)

        If CountNotifications > 0 Then
            Return True
        Else
            Return False
        End If


    End Function

    Private Function GetEmployeesToGrid()
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strselect As String
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisal.ConnectionString)
        Dim EmpName As String
        Dim DepName As String
        Dim criteria As String = "Where 1=1 "
        If ProfileCls.CurrentLanguage = "Ar" Then
            EmpName = "dbo.fn_getempName(hrs_Employees.Code,1)"
            DepName = "sys_Departments.ArbName"
        Else
            EmpName = "dbo.fn_getempName(hrs_Employees.Code,0)"
            DepName = "sys_Departments.EngName"


        End If
        strselect = "select Hrs_Employees.ID, Hrs_Employees.Code , " & EmpName & " As FullName," & DepName & " As Department from hrs_Employees join sys_Departments on hrs_employees.DepartmentID=sys_Departments.ID join hrs_Contracts on hrs_Employees.ID=hrs_Contracts.EmployeeID and hrs_Contracts.CancelDate is null and hrs_employees.canceldate is  null and (hrs_Contracts.EndDate>getdate() or hrs_Contracts.EndDate is null)"
        If ddlBranch.SelectedValue > 0 Then
            criteria &= " And hrs_Employees.BranchID= " & ddlBranch.SelectedValue & ""
        End If
        If ddlSector.SelectedValue > 0 Then
            criteria &= " And hrs_Employees.SectorID= " & ddlSector.SelectedValue & ""

        End If
        If ddldepartment.SelectedValue > 0 Then
            criteria &= " And hrs_Employees.DepartmentID= " & ddldepartment.SelectedValue & ""

        End If
        If ddlPosition.SelectedValue > 0 Then
            criteria &= " And hrs_Contracts.PositionID= " & ddlPosition.SelectedValue & ""

        End If
        If ddlProject.SelectedValue > 0 Then
            criteria &= " And hrs_employees.LocationID=" & ddlProject.SelectedValue & ""
        End If
        If Not String.IsNullOrEmpty(TxtEmpCode.Text) Then
            criteria &= " And Hrs_Employees.Code='" & TxtEmpCode.Text & "'"
        End If
        strselect &= criteria
        Dim DSEmployees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
        UwgSearchEmployees.DataSource = Nothing
        If DSEmployees.Tables(0).Rows.Count > 0 Then
            UwgSearchEmployees.DataSource = DSEmployees
            UwgSearchEmployees.DataBind()
        End If
    End Function

#End Region

End Class
