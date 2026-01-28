Imports System.Data
Imports System.Web.UI.WebControls
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmSalaryDist
    Inherits MainPage
#Region "Public Decleration"
    Private ClsSalaryDist As ClsHrs_SalaryDist
    Private ClsProjects As Clshrs_Projects
    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsSalaryDist = New ClsHrs_SalaryDist(Me)
        'Dim ClsProjects As New Clshrs_Projects(Me)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsSalaryDist.ConnectionString)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsSalaryDist.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsSalaryDist.ConnectionString)
                ClsSalaryDist.AddOnChangeEventToControls("frmSalaryDist", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)


                'Dim clsenum As New Clshrs_Enum(Me)
                'clsenum.GetList(UwgSearchEmployees.DisplayLayout.Bands(0).Columns(2).ValueList, False, "Flag = 4")

                Dim clsProjects As New Clshrs_Projects(Me, "")
                clsProjects.GetDropDownList(ddlProjects, False)

                'Page.ViewState("PlanProjects") = Nothing

                Dim tbl_Projects As New DataTable()
                tbl_Projects.Columns.Add("ProjectID", GetType(Integer))
                tbl_Projects.Columns.Add("ProjectName", GetType(String))
                tbl_Projects.Columns.Add("ProjectPercentage", GetType(Decimal))

                ViewState("PlanProjects") = tbl_Projects
                grdProjects.DataSource = DirectCast(ViewState("PlanProjects"), DataTable)
                grdProjects.DataBind()
                'Page.ViewState.Add("PlanProjects", tbl_Projects)
                'ViewState("PlanProjects") = tbl_Projects
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsSalaryDist.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsSalaryDist.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSalaryDist.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsSalaryDist = New ClsHrs_SalaryDist(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsSalaryDist.ConnectionString)
        Dim tbl_Projects As New DataTable()
        tbl_Projects = DirectCast(ViewState("PlanProjects"), DataTable)


        Select Case e.CommandArgument
            Case "SaveNew"
                If tbl_Projects.Rows.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Projects /برجاء إدخال المشاريع"))
                    Exit Sub
                End If
                Dim result As Decimal
                result = tbl_Projects.Compute("SUM(ProjectPercentage)", "")
                If result <> 100 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Projects Total Percentage must be 100% /مجموع نسب المشاريع يجب أن تكون 100%"))
                    Exit Sub
                End If
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsSalaryDist.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsSalaryDist.ID > 0 Then
                    ClsSalaryDist.Update("code='" & txtCode.Text & "'")
                Else
                    ClsSalaryDist.Save()
                End If
                ClsSalaryDist.Find("Code='" & txtCode.Text & "'")
                If ClsSalaryDist.ID > 0 Then
                    If (SaveDG(ClsSalaryDist.ID, tbl_Projects)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsSalaryDist.Table, ClsSalaryDist.ID)
                        value.Text = ""
                    End If
                End If
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsSalaryDist.Table, ClsSalaryDist.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If tbl_Projects.Rows.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Projects /برجاء إدخال المشاريع"))
                    Exit Sub
                End If
                Dim result As Decimal
                result = tbl_Projects.Compute("SUM(ProjectPercentage)", "")
                If result <> 100 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Projects Total Percentage must be 100% /مجموع نسب المشاريع يجب أن تكون 100%"))
                    Exit Sub
                End If
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsSalaryDist.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsSalaryDist.ID > 0 Then
                    ClsSalaryDist.Update("code='" & txtCode.Text & "'")
                Else
                    ClsSalaryDist.Save()
                End If

                ClsSalaryDist.Find("Code='" & txtCode.Text & "'")

                If ClsSalaryDist.ID > 0 Then
                    If (SaveDG(ClsSalaryDist.ID, tbl_Projects)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsSalaryDist.Table, ClsSalaryDist.ID)
                        value.Text = ""
                    End If
                End If
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsSalaryDist.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsSalaryDist.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsSalaryDist.ID & "&TableName=" & ClsSalaryDist.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsSalaryDist.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsSalaryDist.ID & "&TableName=" & ClsSalaryDist.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsSalaryDist.Table
                ClsSalaryDist.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsSalaryDist.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsSalaryDist.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsSalaryDist.Find(" Code= '" & txtCode.Text & "'")
                If ClsSalaryDist.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsSalaryDist.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsSalaryDist.CheckDiff(ClsSalaryDist, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsSalaryDist.FirstRecord()
                GetValues()
            Case "Previous"
                ClsSalaryDist.Find("Code='" & txtCode.Text & "'")
                If Not ClsSalaryDist.previousRecord() Then
                    ClsSalaryDist.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsSalaryDist.Find("Code='" & txtCode.Text & "'")
                If Not ClsSalaryDist.NextRecord() Then
                    ClsSalaryDist.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsSalaryDist.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub btnAddProject_Click(sender As Object, e As System.EventArgs) Handles btnAddProject.Click
        If IsPostBack Then
            If ViewState("PlanProjects") IsNot Nothing Then
                Dim PlanProjects = DirectCast(ViewState("PlanProjects"), DataTable)
                Dim dr() = PlanProjects.Select("ProjectID='" & ddlProjects.SelectedItem.Value & "'")
                If IsNumeric(txtProjectPercentage.Text) Then
                    If CDec(txtProjectPercentage.Text) > 0 Then
                        If dr.Length > 0 Then
                            dr(0)("ProjectPercentage") = txtProjectPercentage.Text
                        Else
                            PlanProjects.Rows.Add(ddlProjects.SelectedItem.Value, ddlProjects.SelectedItem.Text, txtProjectPercentage.Text)
                        End If
                    Else
                        If dr.Length > 0 Then
                            PlanProjects.Rows.Remove(dr(0))
                        End If
                    End If

                End If

                txtProjectPercentage.Text = ""

                ViewState("PlanProjects") = PlanProjects
                grdProjects.DataSource = DirectCast(ViewState("PlanProjects"), DataTable)
                grdProjects.DataBind()
            End If
        End If

    End Sub
    Protected Sub grdProjects_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim PlanProjects = DirectCast(ViewState("PlanProjects"), DataTable)
        Dim dr() = PlanProjects.Select("ProjectID=" & e.CommandArgument)
        If e.CommandName = "EdirRow" Then
            ddlProjects.SelectedValue = e.CommandArgument.ToString()
            txtProjectPercentage.Text = dr(0)("ProjectPercentage")
        ElseIf e.CommandName = "Remove" Then
            PlanProjects.Rows.remove(dr(0))
            ViewState("PlanProjects") = PlanProjects
            grdProjects.DataSource = Nothing
            grdProjects.DataBind()
            grdProjects.DataSource = DirectCast(ViewState("PlanProjects"), DataTable)
            grdProjects.DataBind()
        End If

    End Sub


#End Region

#Region "Private Functions"
    Private Function SaveDG(ByVal ClsSalaryDistID As Integer, ByVal tbl_Projects As DataTable) As Boolean
        Try
            If ClsSalaryDistID > 0 Then
                Dim str As String
                str = "delete from Hrs_SalaryDistProjects where SalaryDistributionID = " & ClsSalaryDistID & ";"
                For Each DGRow As DataRow In tbl_Projects.Rows()
                    str &= " INSERT INTO Hrs_SalaryDistProjects (SalaryDistributionID,ProjectID,Percentage,RegDate,RegUserID) VALUES (" & ClsSalaryDistID & "," & DGRow("ProjectID") & "," & DGRow("ProjectPercentage") & ",getdate()," & ClsSalaryDist.DataBaseUserRelatedID & ") ;"
                Next
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsSalaryDist.ConnectionString, Data.CommandType.Text, str)
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function AssignValues() As Boolean
        Try
            With ClsSalaryDist
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
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
            With ClsSalaryDist
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName

                Dim Cls_SalaryDistProjects As New ClsHrs_SalaryDistProjects(Me)
                Cls_SalaryDistProjects.Find("SalaryDistributionID = " & ClsSalaryDist.ID)
                Dim tbl_Projects As New DataTable()
                tbl_Projects = DirectCast(ViewState("PlanProjects"), DataTable)
                tbl_Projects.Rows.Clear()

                For Each row As DataRow In Cls_SalaryDistProjects.DataSet.Tables(0).Rows
                    Dim cls_Projects As New Clshrs_Projects(Me, "")
                    cls_Projects.Find("ID = " & row("ProjectID"))
                    tbl_Projects.Rows.Add(row("ProjectID"), cls_Projects.ArbName, row("Percentage"))
                Next
                grdProjects.DataSource = tbl_Projects
                grdProjects.DataBind()

                ViewState("PlanProjects") = tbl_Projects
            End With

            If Not ClsSalaryDist.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsSalaryDist.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsSalaryDist.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsSalaryDist.RegDate).Date
            End If
            If ClsSalaryDist.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsSalaryDist.CancelDate).Date
            End If
            If Not ClsSalaryDist.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()


            If (ClsSalaryDist.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsSalaryDist.ConnectionString, ClsSalaryDist.DataBaseUserRelatedID, ClsSalaryDist.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsSalaryDist.ConnectionString, ClsSalaryDist.DataBaseUserRelatedID, ClsSalaryDist.GroupID, ClsSalaryDist.Table, ClsSalaryDist.ID)
            If Not ClsSalaryDist.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsSalaryDist.ID)
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function
    'Added By: Hassan Kurdi
    'Date: 2021-12-13
    'Purpose: Check if the no blank field for first shift
    'Added By: Hassan Kurdi
    'Date: 2021-12-13
    'Purpose: Check if the time formate is correct
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
                    ClsSalaryDist.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsSalaryDist.Find("ID=" & intID)
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
            With ClsSalaryDist
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
        ClsSalaryDist = New ClsHrs_SalaryDist(Me)
        Try
            With ClsSalaryDist
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
        ClsSalaryDist = New ClsHrs_SalaryDist(Me)
        If IntId > 0 Then
            ClsSalaryDist.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsSalaryDist = New ClsHrs_SalaryDist(Me)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsSalaryDist.ConnectionString)
        Try
            ClsSalaryDist.Find("Code='" & txtCode.Text & "'")
            IntId = ClsSalaryDist.ID
            txtEngName.Focus()
            If ClsSalaryDist.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsSalaryDist.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                txtEngName.Focus()
                CreateOtherFields(0)

                'UwgSearchEmployees.Rows.Clear()
                'For i As Integer = 1 To 7
                '    UwgSearchEmployees.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Nothing, i}))
                'Next i
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsSalaryDist.ConnectionString, ClsSalaryDist.DataBaseUserRelatedID, ClsSalaryDist.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsSalaryDist.ConnectionString, ClsSalaryDist.DataBaseUserRelatedID, ClsSalaryDist.GroupID, ClsSalaryDist.Table, IntId)
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
        ClsSalaryDist.Clear()
        'UwgSearchEmployees.Clear()
        GetValues()
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
        Dim tbl_Projects As New DataTable()
        tbl_Projects = DirectCast(ViewState("PlanProjects"), DataTable)
        tbl_Projects.Rows.Clear()
        ViewState("PlanProjects") = tbl_Projects
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        'UwgSearchEmployees.Rows.Clear()

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsSalaryDist = New ClsHrs_SalaryDist(Page)
        ClsSalaryDist.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsSalaryDist.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsSalaryDist.Table) = True Then
            Dim StrTablename As String
            ClsSalaryDist = New ClsHrs_SalaryDist(Me)
            StrTablename = ClsSalaryDist.Table
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
End Class
