Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Partial Class frmGrades
    Inherits MainPage
#Region "Public Decleration"

    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsEmployees As Clshrs_Employees
    Private ClsHmsCommissionCategories As Clshrs_HmsCommissionCategories
    Const CsPaidatVacation = 4
    Const CsOnceatPeriod = 5
    Const CsMaxValue = 3
    Const CsMinValue = 2
    Const CsIntervalID = 6
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        ClsEmployees = New Clshrs_Employees(Me.Page)
        ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim SearchID As Integer = 0

        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsHmsCommissionCategories.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsHmsCommissionCategories.ConnectionString)
                ClsHmsCommissionCategories.AddOnChangeEventToControls("frmGrades", Page, UltraWebTab1)
                lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lang", lblLage.Text)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                UWGEmployeesCommission.Bands(0).Columns(3).Hidden = True


            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsHmsCommissionCategories.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsHmsCommissionCategories.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsHmsCommissionCategories.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Me.Page)

        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsHmsCommissionCategories.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                SavePart()

                ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsHmsCommissionCategories.Table, ClsHmsCommissionCategories.ID)

                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                SavePart()

                ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsHmsCommissionCategories.Table, ClsHmsCommissionCategories.ID)

                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")

                ClsHmsCommissionCategories.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsHmsCommissionCategories.ID & "&TableName=" & ClsHmsCommissionCategories.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsHmsCommissionCategories.ID & "&TableName=" & ClsHmsCommissionCategories.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsHmsCommissionCategories.Table
                ClsHmsCommissionCategories.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsHmsCommissionCategories.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsHmsCommissionCategories.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsHmsCommissionCategories.Find(" Code= '" & txtCode.Text & "'")
                If ClsHmsCommissionCategories.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsHmsCommissionCategories.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If

                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsHmsCommissionCategories.FirstRecord()
                GetValues()
            Case "Previous"
                ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")
                If Not ClsHmsCommissionCategories.previousRecord() Then
                    ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")
                If Not ClsHmsCommissionCategories.NextRecord() Then
                    ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsHmsCommissionCategories.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"

    Private Function validateUserInputs() As Boolean
        'For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGradesTransactions.Rows
        '    If DGRow.Cells.FromKey("TransactionTypeID").Value > 0 Then
        '        If DGRow.Cells(CsMaxValue).Value = Nothing Or DGRow.Cells(CsMaxValue).Value = Nothing Then
        '            Return False
        '        End If
        '    End If
        'Next
        Return True
    End Function

    Private Function SaveDG(ByVal CommissiomCatID As Integer) As Boolean

        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsHmsCommissionCategories.ConnectionString)
            ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Page)
            Dim ClsHmsEmployeesClinicsMap As New Clshrs_HmsEmployeesClinicsMap(Page)

            Dim str As String = "Delete from hrs_HmsEmployeesClinicsMap where CommissionCategoriesID=" & CommissiomCatID
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsHmsCommissionCategories.ConnectionString, CommandType.Text, str)
            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UWGEmployeesCommission.Rows
                If IsNothing(row.Cells.FromKey("DoctorCode").Value) Or IsNothing(ClsHmsEmployeesClinicsMap.EmpCode = row.Cells.FromKey("EmployeeCode").Value) Then
                    Continue For
                End If
                ClsHmsEmployeesClinicsMap.CommissionCategoriesID = CommissiomCatID
                ClsHmsEmployeesClinicsMap.DoctoreCode = row.Cells.FromKey("DoctorCode").Value
                ClsHmsEmployeesClinicsMap.EmpCode = row.Cells.FromKey("EmployeeCode").Value
                ClsHmsEmployeesClinicsMap.CommissionPc = row.Cells.FromKey("CommissionPc").Value
                ClsHmsEmployeesClinicsMap.Save()
            Next
        Catch ex As Exception
            Return False
        End Try


    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")

        ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Page)


        Try
            ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")

            If ClsHmsCommissionCategories.ID > 0 Then
                If Not AssignValues() Then
                    Exit Function
                End If

                ClsHmsCommissionCategories.Update("Code='" & txtCode.Text & "'")

                'ClsGradesTransactions.DeleteAll("GradeID=" & ClsGrades.ID)
                If (SaveDG(ClsHmsCommissionCategories.ID)) Then
                    Return True
                End If
            Else
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsHmsCommissionCategories.Save()
                ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")
                If ClsHmsCommissionCategories.ID > 0 Then
                    If (SaveDG(ClsHmsCommissionCategories.ID)) Then
                        Return True
                    End If
                End If

            End If

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsHmsCommissionCategories.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function AssignValues() As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsHmsCommissionCategories.ConnectionString)
        Dim arr As New ArrayList()
        Try
            arr.Clear()
            'For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGradesTransactions.Rows
            '    If Not DGRow.Cells(1).Value > 0 Then
            '        Continue For
            '    End If
            '    If (arr.Contains(DGRow.Cells(1).Value)) Then
            '        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Can't enter duplicate Transaction to that Grade/لا يمكن إدخال بند مكررة في نفس الحزمة"))
            '        Return False
            '    End If
            '    arr.Add(DGRow.Cells(1).Value)
            'Next
            With ClsHmsCommissionCategories
                .Code = txtCode.Text
                .Engname = txtEngName.Text
                .Arbname = txtArbName.Text
                .IsLineCommission = ChkIsLineCommission.Checked
                .TotalCommision = txtTotalCommission.Text

                Try

                Catch ex As Exception
                    Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
                    Page.Session.Add("ErrorValue", ex)
                    mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsHmsCommissionCategories.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
                    Page.Response.Redirect("ErrorPage.aspx")
                End Try
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)

        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ClshrsHmsEmployeesClinicsMap As New Clshrs_HmsEmployeesClinicsMap(Page)
        Dim ds As DataSet
        Dim str As String
        'ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Page)

        Try
            SetToolBarDefaults()

            With ClsHmsCommissionCategories
                txtCode.Text = .Code
                txtEngName.Text = .Engname
                txtArbName.Text = .Arbname
                txtTotalCommission.Text = .TotalCommision
                ChkIsLineCommission.Checked = .IsLineCommission
                If .IsLineCommission Then
                    UWGEmployeesCommission.Bands(0).Columns(3).Hidden = False
                Else
                    UWGEmployeesCommission.Bands(0).Columns(3).Hidden = True
                End If



                ClshrsHmsEmployeesClinicsMap.Find(" CommissionCategoriesID =" & .ID)
                str = "Exec Get_EmpClinicMap " & ClsHmsCommissionCategories.ID
                ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsHmsCommissionCategories.ConnectionString, System.Data.CommandType.Text, str)


                UWGEmployeesCommission.DataSource = ds
                UWGEmployeesCommission.DataBind()


            End With
            'UWGEmployeesCommission.Rows.Add()
            UWGEmployeesCommission.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow)

            If Not ClsHmsCommissionCategories.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsHmsCommissionCategories.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If


            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsHmsCommissionCategories.ConnectionString)
            If (ClsHmsCommissionCategories.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsHmsCommissionCategories.ConnectionString, ClsHmsCommissionCategories.DataBaseUserRelatedID, ClsHmsCommissionCategories.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsHmsCommissionCategories.ConnectionString, ClsHmsCommissionCategories.DataBaseUserRelatedID, ClsHmsCommissionCategories.GroupID, ClsHmsCommissionCategories.Table, ClsHmsCommissionCategories.ID)
            If Not ClsHmsCommissionCategories.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsHmsCommissionCategories.ID)
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
                    ClsHmsCommissionCategories.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsHmsCommissionCategories.Find("ID=" & intID)
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
            With ClsHmsCommissionCategories
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
        ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Me)
        Try
            With ClsHmsCommissionCategories
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
        ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Me)
        If IntId > 0 Then
            ClsHmsCommissionCategories.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Me)
        Try
            ClsHmsCommissionCategories.Find("Code='" & txtCode.Text & "'")
            IntId = ClsHmsCommissionCategories.ID
            txtEngName.Focus()
            If ClsHmsCommissionCategories.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsHmsCommissionCategories.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsHmsCommissionCategories.ConnectionString, ClsHmsCommissionCategories.DataBaseUserRelatedID, ClsHmsCommissionCategories.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsHmsCommissionCategories.ConnectionString, ClsHmsCommissionCategories.DataBaseUserRelatedID, ClsHmsCommissionCategories.GroupID, ClsHmsCommissionCategories.Table, IntId)
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
        ClsHmsCommissionCategories.Clear()
        Clear()
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


        UWGEmployeesCommission.Rows.Clear()
        UWGEmployeesCommission.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow)

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Page)
        ClsHmsCommissionCategories.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsHmsCommissionCategories.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsHmsCommissionCategories.Table) = True Then
            Dim StrTablename As String
            ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Me)
            StrTablename = ClsHmsCommissionCategories.Table
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

#Region "Public Shared Function"
    <System.Web.Services.WebMethod()> _
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
#End Region

    Protected Sub ChkIsLineCommission_CheckedChanged(sender As Object, e As EventArgs) Handles ChkIsLineCommission.CheckedChanged
        If ChkIsLineCommission.Checked Then
            lblTotalCommission.Visible = False
            txtTotalCommission.Visible = False
            UWGEmployeesCommission.Bands(0).Columns(3).Hidden = False
        Else
            lblTotalCommission.Visible = True
            txtTotalCommission.Visible = True
            UWGEmployeesCommission.Bands(0).Columns(3).Hidden = True
        End If
    End Sub
    Protected Sub UWGEmployeesCommission_ClickCellButton(sender As Object, e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles UWGEmployeesCommission.ClickCellButton
        ClsHmsCommissionCategories = New Clshrs_HmsCommissionCategories(Page)
        Dim row As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        row = e.Cell.Row
        Dim ID As Object = row.Cells().FromKey("ID").Value

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsHmsCommissionCategories.ConnectionString)

        Dim strCmd As String = " Update hrs_HmsEmployeesClinicsMap set CancelDate =getdate() where ID=" & ID
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsHmsCommissionCategories.ConnectionString, CommandType.Text, strCmd)

        Venus.Shared.Web.ClientSideActions.ClosePage(Page, ObjNavigationHandler.SetLanguage(Page, "delete Complete Successfully / تم الحذف"))
        CheckCode()
    End Sub


End Class
