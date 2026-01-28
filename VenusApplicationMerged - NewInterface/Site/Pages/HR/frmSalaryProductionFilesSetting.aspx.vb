Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmSalaryProductionFilesSetting
    Inherits MainPage
#Region "Public Decleration"
    Private SalaryProductionFiles As Clshrs_SalaryProductionFiles
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        SalaryProductionFiles = New Clshrs_SalaryProductionFiles(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & SalaryProductionFiles.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", SalaryProductionFiles.ConnectionString)
                SalaryProductionFiles.AddOnChangeEventToControls("frmVacationsTypes", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Dim ClsSalaryProductionfields As New Clshrs_SalaryProductionColumns(Me.Page)
                ClsSalaryProductionfields.Find("CancelDate IS NULL")
                uwgSalaryProductionFilesFields.Rows.Add()
                GetList_Data(uwgSalaryProductionFilesFields.Bands(0).Columns.FromKey("Name").ValueList, ClsSalaryProductionfields.DataSet)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                SalaryProductionFiles.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = SalaryProductionFiles.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, SalaryProductionFiles.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        SalaryProductionFiles = New Clshrs_SalaryProductionFiles(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(SalaryProductionFiles.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                SavePart()
                AfterOperation()

            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If SavePart() Then

                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"
                AfterOperation()
            Case "Delete"
                SalaryProductionFiles.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()

            Case "Property"
                If SalaryProductionFiles.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & SalaryProductionFiles.ID & "&TableName=" & SalaryProductionFiles.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                End If
            Case "Remarks"
                If SalaryProductionFiles.Find("Code='" & txtCode.Text & "'") Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & SalaryProductionFiles.ID & "&TableName=" & SalaryProductionFiles.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                'SalaryProductionFiles.Find("Code='" & txtCode.Text & "'")
                SalaryProductionFiles.FirstRecord()
                GetValues(SalaryProductionFiles)

            Case "Previous"
                SalaryProductionFiles.Find("Code='" & txtCode.Text & "'")
                If Not SalaryProductionFiles.previousRecord() Then
                    SalaryProductionFiles.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))

                End If
                GetValues(SalaryProductionFiles)

            Case "Next"
                SalaryProductionFiles.Find("Code='" & txtCode.Text & "'")
                If Not SalaryProductionFiles.NextRecord() Then
                    SalaryProductionFiles.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))

                End If
                GetValues(SalaryProductionFiles)

            Case "Last"
                'SalaryProductionFiles.Find("Code='" & txtCode.Text & "'")
                SalaryProductionFiles.LastRecord()
                GetValues(SalaryProductionFiles)

        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function SaveDG(ByVal SalaryProductionFileID As Integer) As Boolean
        Try
            If SalaryProductionFileID > 0 Then
                Dim str As String
                str = "delete from hrs_SalaryProductionFilesColumns where SalaryProductionFileId = " & SalaryProductionFileID & ";"
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgSalaryProductionFilesFields.Rows()
                    If DGRow.Cells.FromKey("Name").Value <> Nothing Then
                        str &= " INSERT INTO hrs_SalaryProductionFilesColumns (SalaryProductionFileId,SalaryProductionColumnId,Name) VALUES (" & SalaryProductionFileID & ",'" & DGRow.Cells.FromKey("Name").Value & "','" & DGRow.Cells.FromKey("AliasName").Value & "') ;"
                    End If
                Next

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(SalaryProductionFiles.ConnectionString, Data.CommandType.Text, str)
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function SavePart() As Boolean

        Try

            Dim StrMode As String = Request.QueryString.Item("Mode")
            SalaryProductionFiles = New Clshrs_SalaryProductionFiles(Page)

            SalaryProductionFiles.Find("Code='" & txtCode.Text & "'")

            If Not AssignValue(SalaryProductionFiles) Then
                Exit Function
            End If
            If SalaryProductionFiles.ID > 0 Then
                SalaryProductionFiles.Update("Code='" & txtCode.Text & "'")
            Else
                SalaryProductionFiles.Save()
            End If
            SalaryProductionFiles.Find("Code='" & txtCode.Text & "'")
            If SalaryProductionFiles.ID > 0 Then
                SaveDG(SalaryProductionFiles.ID)
            End If



            'clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
            'clsMainOtherFields.CollectDataAndSave(value.Text, SalaryProductionFiles.Table, SalaryProductionFiles.ID)
            value.Text = ""

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AssignValue(ByRef SalaryProductionFiles As Clshrs_SalaryProductionFiles) As Boolean
        Try
            With SalaryProductionFiles

                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .ISTextFile = ChkISTextFile.Checked
                .ShowEmpWitoutIBN = ChkEmpWitoutIBN.Checked
                .ShowEmpWithEOS = ChkEmpWithEOS.Checked
                .CompanyID = Convert.ToInt32(HttpContext.Current.Session("CompanyID"))
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues(ByVal SalaryProductionFiles As Clshrs_SalaryProductionFiles) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With SalaryProductionFiles

                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                ChkISTextFile.Checked = .ISTextFile
                ChkEmpWitoutIBN.Checked = .ShowEmpWitoutIBN
                ChkEmpWithEOS.Checked = .ShowEmpWithEOS




                If Not SalaryProductionFiles.RegUserID = Nothing Then
                    ClsUser.Find("ID=" & SalaryProductionFiles.RegUserID)
                End If
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
                SetToolBarRecordPermission(Me, SalaryProductionFiles.ConnectionString, SalaryProductionFiles.DataBaseUserRelatedID, SalaryProductionFiles.GroupID, SalaryProductionFiles.Table, IntId)
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If
                If Page.IsPostBack Then
                    CreateOtherFields(SalaryProductionFiles.ID)
                End If
            End With


            '  get Fileds
            Dim ds As New Data.DataSet
            Dim str = "select SPC.Id as Id, SPFC.Name as AliasName,SPC.Name as Name from hrs_SalaryProductionFilesColumns SPFC inner join hrs_SalaryProductionColumns SPC on SPC.Id=SPFC.SalaryProductionColumnId  where SPFC.SalaryProductionFileId=" & SalaryProductionFiles.ID
            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(SalaryProductionFiles.ConnectionString, Data.CommandType.Text, str)
            If ds.Tables(0).Rows.Count > 0 Then
                uwgSalaryProductionFilesFields.DataSource = ds.Tables(0)
                uwgSalaryProductionFilesFields.DataBind()
            Else
                uwgSalaryProductionFilesFields.DataSource = Nothing
                uwgSalaryProductionFilesFields.DataBind()

            End If
            uwgSalaryProductionFilesFields.Rows.Add()

            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        SalaryProductionFiles = New Clshrs_SalaryProductionFiles(Me.Page)

        Try
            SalaryProductionFiles.Find("Code='" & txtCode.Text & "'")
            IntId = SalaryProductionFiles.ID
            If SalaryProductionFiles.ID > 0 Then
                GetValues(SalaryProductionFiles)

                StrMode = "E"
                txtEngName.Focus()
            Else
                If SalaryProductionFiles.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False

                StrMode = "N"
                txtEngName.Focus()
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, SalaryProductionFiles.ConnectionString, SalaryProductionFiles.DataBaseUserRelatedID, SalaryProductionFiles.GroupID, StrMode)
            SetToolBarRecordPermission(Me, SalaryProductionFiles.ConnectionString, SalaryProductionFiles.DataBaseUserRelatedID, SalaryProductionFiles.GroupID, SalaryProductionFiles.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function AfterOperation() As Boolean
        SalaryProductionFiles.Clear()
        uwgSalaryProductionFilesFields.Clear()
        GetValues(SalaryProductionFiles)
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
        ChkISTextFile.Checked = False
        ChkEmpWitoutIBN.Checked = False
        ChkEmpWithEOS.Checked = False
        uwgSalaryProductionFilesFields.Rows.Clear()
        uwgSalaryProductionFilesFields.Rows.Add()


        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""


    End Function
    Public Function GetList_Data(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal ObjDataset As DataSet) As Boolean
        Dim ObjDataRow As DataRow
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(SalaryProductionFiles.ConnectionString)
        Try

            DdlValues.ValueListItems.Clear()

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem

                Item.DisplayText = ObjDataRow("Name")


                'IIf(IsDBNull(ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName"))), _
                '                       IIf(IsDBNull(ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "ArbName/EngName"))), _
                '                           "", _
                '                           ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "ArbName/EngName"))), _
                '                       ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")))

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
                    SalaryProductionFiles.Find("ID=" & intID)
                    GetValues(SalaryProductionFiles)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    SalaryProductionFiles.Find("ID=" & intID)
                    GetValues(SalaryProductionFiles)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With SalaryProductionFiles
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
        SalaryProductionFiles = New Clshrs_SalaryProductionFiles(Me.Page)
        Try
            With SalaryProductionFiles
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


    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function

    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, SalaryProductionFiles.Table) = True Then
            Dim StrTablename As String
            SalaryProductionFiles = New Clshrs_SalaryProductionFiles(Page)
            StrTablename = SalaryProductionFiles.Table
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
    <System.Web.Services.WebMethod()>
    Public Shared Function Get_Searched_Description(ByVal IntSearchId As Integer, ByVal strCode As String) As String
        Dim dsSearchs As New Data.DataSet
        Find("sys_Searchs", " sys_Searchs.Id = " & IntSearchId, dsSearchs)
        Dim dsObjects As New Data.DataSet
        Find("sys_Objects", " sys_Objects.Id = " & dsSearchs.Tables(0).Rows(0).Item("ObjectID"), dsObjects)
        Return GetFieldDescription(strCode, dsObjects.Tables(0).Rows(0).Item("Code"))
    End Function
    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As Data.DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table
        Dim mSqlDataAdapter As New Data.SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter & " And CancelDate IS Null", " Where CancelDate IS Null")
            StrSelectCommand = StrSelectCommand '& orderByStr
            mSqlDataAdapter = New Data.SqlClient.SqlDataAdapter(StrSelectCommand, ConnStr)
            DataSet = New Data.DataSet
            mSqlDataAdapter.Fill(DataSet)
            If DataSet.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function GetFieldDescription(ByVal StrCode As String, ByVal StrTableName As String) As String
        Dim StrReturnData As Object
        Try
            StrReturnData = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, " Select EngName + '/' + ArbName From " & StrTableName & " Where Code = '" & StrCode.ToString.TrimStart.TrimEnd & "'")
            If IsNothing(StrReturnData) Then Return "/"
            If IsDBNull(StrReturnData) Then Return "/"
            Return StrReturnData
        Catch ex As Exception
            Return "/"
        End Try
    End Function

#End Region


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub




End Class
