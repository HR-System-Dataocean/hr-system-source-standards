Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Partial Class frmProfession
    Inherits MainPage
#Region "Public Decleration"
    Private ClsAppraisalTypes As Clshrs_AppraisalTypes
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsAppraisalTypes = New Clshrs_AppraisalTypes(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsAppraisalTypes.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID

                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsAppraisalTypes.ConnectionString)
                ClsAppraisalTypes.AddOnChangeEventToControls("frmProfession", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]

                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Dim AppraisalTypeGroup As New Clshrs_AppraisalTypeGroup(Page)
                AppraisalTypeGroup.GetDropDownList(ddlAppraisalTypeGroup, True)
                GetDropDownListGrid()
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsAppraisalTypes.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsAppraisalTypes.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsAppraisalTypes.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsAppraisalTypes = New Clshrs_AppraisalTypes(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAppraisalTypes.ConnectionString)
        Dim TotalWeight As Integer = 0

        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgAppraisalTypeConfiguration.Rows
            If Not String.IsNullOrEmpty(DGRow.Cells.FromKey("UserType").Value) Then

                TotalWeight += DGRow.Cells.FromKey("StageWeight").Value

            End If
        Next
        If TotalWeight <> 100 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Total Stager  Weight should Equal to 100 Percent / عفوا...لابد ان يكون اجمالي الوزن النسبي  يساوي 100 نقطة"))
            Exit Sub
        End If
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If SavePart() Then
                    ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
                    If ClsAppraisalTypes.ID > 0 Then
                        SaveDG(ClsAppraisalTypes.ID)
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                        AfterOperation()

                        Clear()
                    End If

                End If

            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                If SavePart() Then
                    ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
                    If ClsAppraisalTypes.ID > 0 Then
                        SaveDG(ClsAppraisalTypes.ID)
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                    End If
                End If
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsAppraisalTypes.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsAppraisalTypes.ID & "&TableName=" & ClsAppraisalTypes.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsAppraisalTypes.ID & "&TableName=" & ClsAppraisalTypes.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
                ClsAppraisalTypes.FirstRecord()
                GetValues(ClsAppraisalTypes)
            Case "Previous"
                ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
                If Not ClsAppraisalTypes.previousRecord() Then
                    ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))

                End If
                GetValues(ClsAppraisalTypes)
            Case "Next"
                ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
                If Not ClsAppraisalTypes.NextRecord() Then
                    ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))

                End If
                GetValues(ClsAppraisalTypes)
            Case "Last"
                ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
                ClsAppraisalTypes.LastRecord()
                GetValues(ClsAppraisalTypes)
        End Select
    End Sub

    Private Function SaveDG(AppraisalID As Integer) As Boolean
        Try

            'Dim AppraisalID As String = "" & ddlAppraisals.SelectedValue & ""
            Dim ConnectionString As String = ConfigurationManager.AppSettings("Connstring").ToString()

            Dim Deletecommand As String = "Delete from App_AppraisalConfigurations where AppraisalTypeID='" & AppraisalID & "' "
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, Deletecommand)



            Dim user As String
            Dim WebHandler As New Venus.Shared.Web.WebHandler

            WebHandler.GetCookies(Page, "UserID", user)

            Dim str As String
            Dim HasObjection
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgAppraisalTypeConfiguration.Rows


                If DGRow.Cells(1).Value > 0 Then

                    If DGRow.Cells(6).Value Then
                    End If
                    str = "INSERT INTO [dbo].[App_AppraisalConfigurations] ([AppraisalTypeID],[UserTypeID],[PositionID],[EmployeeID],[Rank],StageWeight,RegDate,RegUserID,HasObjection,NoOfObjections,IsToConfirmOnly,CreateEscalation,EscalationPeriod,EscalationMail) VALUES ('" & AppraisalID & "','" & DGRow.Cells(1).Value & "','" & DGRow.Cells(2).Value & "','" & DGRow.Cells(3).Value & "','" & DGRow.Cells(4).Value & "','" & DGRow.Cells(5).Value & "',GetDate()," & user & ",'" & DGRow.Cells(6).Value & "','" & DGRow.Cells(7).Value & "','" & DGRow.Cells(8).Value & "','" & DGRow.Cells(9).Value & "','" & DGRow.Cells(10).Value & "','" & DGRow.Cells(11).Value & "')"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, Data.CommandType.Text, str)



                End If


            Next



        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function GetDropDownListGrid() As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem

        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnectionString)

        Dim strselect2 As String
        strselect2 = "select * from hrs_Positions"
        Dim DSPositions As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect2)
        If DSPositions.Tables(0).Rows.Count > 0 Then
            uwgAppraisalTypeConfiguration.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Clear()
            For Each Row As Data.DataRow In DSPositions.Tables(0).Rows
                uwgAppraisalTypeConfiguration.DisplayLayout.Bands(0).Columns(2).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & ObjNavigationHandler.SetLanguage(Page, "" & Row("EngName") & "/ " & Row("ArbName") & ""))
            Next

        End If

        Dim EmpName As String

        If ProfileCls.CurrentLanguage = "Ar" Then
            EmpName = "ISNULL( hrs_Employees.arbname,' ')+' '+ISNULL(FatherArbName,' ')+' '+isnull(GrandArbName,' ')+' '+isnull(FamilyArbName,' ')"
        Else
            EmpName = " ISNULL(hrs_Employees.EngName,' ')+' '+ISNULL(FatherEngName,' ')+' '+ISNULL(GrandEngName,' ')+' '+ISNULL(FamilyEngName,' ')"
        End If
        Dim strselect3 As String
        strselect3 = "select ID,Code," & EmpName & " as EmpName from hrs_Employees where ExcludeDate is null"
        Dim DSEmployees As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect3)
        If DSEmployees.Tables(0).Rows.Count > 0 Then
            uwgAppraisalTypeConfiguration.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Clear()
            For Each Row As Data.DataRow In DSEmployees.Tables(0).Rows
                uwgAppraisalTypeConfiguration.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Add(Row("ID"), Row("Code") & " - " & Row("EmpName"))
            Next

        End If

    End Function
    Protected Sub uwgSSConfiguration_Cellchanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgAppraisalTypeConfiguration.UpdateCell

        If e.Cell.Row.Cells(1).Value = "1" Then
            e.Cell.Row.Cells(2).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(2).Value = Nothing
            e.Cell.Row.Cells(3).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(3).Value = Nothing
            e.Cell.Row.Cells(5).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes

            e.Cell.Row.Cells(6).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(6).Value = Nothing
            e.Cell.Row.Cells(7).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(7).Value = Nothing
            e.Cell.Row.Cells(8).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(8).Value = Nothing
        End If

        If e.Cell.Row.Cells(1).Value = "2" Then
            e.Cell.Row.Cells(2).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes

            e.Cell.Row.Cells(3).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(3).Value = Nothing
            e.Cell.Row.Cells(5).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes

            e.Cell.Row.Cells(6).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(6).Value = Nothing
            e.Cell.Row.Cells(7).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(7).Value = Nothing
            e.Cell.Row.Cells(8).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes


        End If
        If e.Cell.Row.Cells(1).Value = "3" Then
            e.Cell.Row.Cells(2).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(2).Value = Nothing
            e.Cell.Row.Cells(3).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes
            e.Cell.Row.Cells(5).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes

            e.Cell.Row.Cells(6).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(6).Value = Nothing
            e.Cell.Row.Cells(7).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(7).Value = Nothing
            e.Cell.Row.Cells(8).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes


        End If
        If e.Cell.Row.Cells(1).Value = "4" Then
            e.Cell.Row.Cells(2).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(2).Value = Nothing
            e.Cell.Row.Cells(3).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(3).Value = Nothing

            e.Cell.Row.Cells(5).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(5).Value = Nothing
            e.Cell.Row.Cells(6).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes

            e.Cell.Row.Cells(7).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.Yes

            e.Cell.Row.Cells(8).AllowEditing = Infragistics.WebUI.UltraWebGrid.AllowEditing.No
            e.Cell.Row.Cells(8).Value = Nothing
        End If

    End Sub
    Private Function GetAppraisalConfiguration(AppraisalTypeID As Integer)
        Dim ConnectionString As String
        ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strselect As String
        strselect = "select ID, UserTypeID as UserType,Positionid as Position,EmployeeID as Employee,Rank,stageweight,HasObjection,NoOfObjections,IsToConfirmOnly,CreateEscalation,EscalationPeriod,EscalationMail  from [App_AppraisalConfigurations] where AppraisalTypeID='" & AppraisalTypeID & "'"
        Dim DSCofig As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, strselect)
        If DSCofig.Tables(0).Rows.Count > 0 Then

            GetDropDownListGrid()
            uwgAppraisalTypeConfiguration.DataSource = DSCofig
            uwgAppraisalTypeConfiguration.DataBind()
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgAppraisalTypeConfiguration.Rows
                If IsNothing(DGRow.Cells(1).Value) Then
                    Continue For
                End If
                If DGRow.Cells(2).Value > 0 Then
                    DGRow.Cells(2).Value = Convert.ToInt32(DGRow.Cells(2).Value)
                Else
                    DGRow.Cells(2).Value = ""
                End If
                If DGRow.Cells(3).Value > 0 Then
                    DGRow.Cells(3).Value = Convert.ToInt32(DGRow.Cells(3).Value)
                Else
                    DGRow.Cells(3).Value = ""
                End If



            Next
        End If
    End Function

    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub ChkOneTimeOnly_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkIsOneTimeOnlye.CheckedChanged
        If ChkIsOneTimeOnlye.Checked Then
            txtAppraisalFrequency.Text = String.Empty
            txtAppraisalFrequency.Enabled = False
            TxtOneTimePeriod.Enabled = True
        Else
            txtAppraisalFrequency.Enabled = True
            TxtOneTimePeriod.Text = String.Empty
            TxtOneTimePeriod.Enabled = False
        End If
    End Sub
#End Region

#Region "Private Functions"
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsAppraisalTypes = New Clshrs_AppraisalTypes(Page)

        Try
            ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")

            If ClsAppraisalTypes.ID > 0 Then
                If Not AssignValue(ClsAppraisalTypes) Then
                    Exit Function
                End If
                ClsAppraisalTypes.Update("Code='" & txtCode.Text & "'")

            Else
                If Not AssignValue(ClsAppraisalTypes) Then
                    Exit Function
                End If
                ClsAppraisalTypes.Save()

            End If

            ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")

            clsMainOtherFields.CollectDataAndSave(value.Text, ClsAppraisalTypes.Table, ClsAppraisalTypes.ID)
            value.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function AssignValue(ByRef ClsAppraisalTypes As Clshrs_AppraisalTypes) As Boolean
        Try
            With ClsAppraisalTypes
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .IsOneTimeOnly = ChkIsOneTimeOnlye.Checked
                .OneTimePeriod = TxtOneTimePeriod.Text
                .AppraisalFrequency = txtAppraisalFrequency.Text
                .NotificationPeriod = TxtNotificationPeriod.Text
                '.EscalationMail = TxtEscelationMail.Text
                '.EscalationPeriod = TXtEscelationPeriod.Text
                .MinimumImprovementPerc = TxtMinPrecImprove.Text
                .KPIsWeight = TxtKPIsWeight.Text
                .AppraisalTypeGroupID = ddlAppraisalTypeGroup.SelectedItem.Value


                .RegDate = DateTime.Now

            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues(ByVal ClsAppraisalTypes As Clshrs_AppraisalTypes) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsAppraisalTypes
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                ChkIsOneTimeOnlye.Checked = .IsOneTimeOnly
                TxtOneTimePeriod.Text = .OneTimePeriod
                txtAppraisalFrequency.Text = .AppraisalFrequency
                TxtNotificationPeriod.Text = .NotificationPeriod
                'TxtEscelationMail.Text = .EscalationMail
                'TXtEscelationPeriod.Text = .EscalationPeriod
                TxtMinPrecImprove.Text = .MinimumImprovementPerc
                TxtKPIsWeight.Text = .KPIsWeight
                ddlAppraisalTypeGroup.SelectedValue = .AppraisalTypeGroupID
                If ClsUser.EngName = Nothing Then
                    lblRegUserValue.Text = ""
                Else
                    lblRegUserValue.Text = ClsUser.EngName
                End If
                If Convert.ToDateTime(.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = Convert.ToDateTime(.RegDate).Date
                End If
                If .CancelDate = Nothing Then
                    lblCancelDateValue.Text = ""
                Else
                    lblCancelDateValue.Text = Convert.ToDateTime(.CancelDate).Date
                End If
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                Else
                    ImageButton_Delete.Enabled = True
                End If
                Dim item As New System.Web.UI.WebControls.ListItem()


                If (.ID > 0) Then
                    StrMode = "E"
                Else
                    StrMode = "N"
                End If
                SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                SetToolBarRecordPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, .Table, .ID)
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If
                If Page.IsPostBack Then
                    CreateOtherFields(ClsAppraisalTypes.ID)
                End If
            End With
            uwgAppraisalTypeConfiguration.Rows.Clear()
            GetAppraisalConfiguration(ClsAppraisalTypes.ID)
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
                    ClsAppraisalTypes.Find("ID=" & intID)
                    GetValues(ClsAppraisalTypes)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsAppraisalTypes.Find("ID=" & intID)
                    GetValues(ClsAppraisalTypes)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsAppraisalTypes
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
        ClsAppraisalTypes = New Clshrs_AppraisalTypes(Me.Page)
        Try
            With ClsAppraisalTypes
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
        ClsAppraisalTypes = New Clshrs_AppraisalTypes(Me.Page)
        If IntId > 0 Then
            ClsAppraisalTypes.Find("ID=" & IntId)
            GetValues(ClsAppraisalTypes)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsAppraisalTypes = New Clshrs_AppraisalTypes(Me.Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Try
            ClsAppraisalTypes.Find("Code='" & txtCode.Text & "'")
            IntId = ClsAppraisalTypes.ID
            txtEngName.Focus()
            If ClsAppraisalTypes.ID > 0 Then
                GetValues(ClsAppraisalTypes)
                StrMode = "E"
            Else
                If ClsAppraisalTypes.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If

                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsAppraisalTypes.ConnectionString, ClsAppraisalTypes.DataBaseUserRelatedID, ClsAppraisalTypes.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsAppraisalTypes.ConnectionString, ClsAppraisalTypes.DataBaseUserRelatedID, ClsAppraisalTypes.GroupID, ClsAppraisalTypes.Table, IntId)
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
        ClsAppraisalTypes.Clear()
        '  GetValues(ClsAppraisalTypes)

        Clear()
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
        ChkIsOneTimeOnlye.Checked = False
        TxtOneTimePeriod.Text = String.Empty
        txtAppraisalFrequency.Text = String.Empty
        TxtNotificationPeriod.Text = String.Empty
        TxtMinPrecImprove.Text = String.Empty
        TxtKPIsWeight.Text = String.Empty
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        ddlAppraisalTypeGroup.SelectedValue = 0
        uwgAppraisalTypeConfiguration.DataSource = Nothing
        uwgAppraisalTypeConfiguration.DataBind()
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsAppraisalTypes = New Clshrs_AppraisalTypes(Me.Page)
        ClsAppraisalTypes.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsAppraisalTypes.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsAppraisalTypes.Table) = True Then
            Dim StrTablename As String
            ClsAppraisalTypes = New Clshrs_AppraisalTypes(Me)
            StrTablename = ClsAppraisalTypes.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
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

#End Region
End Class
