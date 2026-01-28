Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Partial Class frmOpHead1
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsmmsOperationsHead As Clsmms_OperationsHead
    Private clsMainOtherFields As clsSys_MainOtherFields

    Const CsPaidatVacation = 4
    Const CsOnceatPeriod = 5
    Const CsMaxValue = 3
    Const CsMinValue = 2
    Const CsIntervalID = 6
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsmmsOperationsHead = New Clsmms_OperationsHead(Me)
        Dim ClsCustomers As New Clsfcs_Customers(Page)
        Dim ClsLocations As New Clssys_Locations(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsmmsOperationsHead.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "',''"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            If ClsObjects.Find(" Code='" & ClsCustomers.Table.Trim.Replace(" ", "") & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TextBox_Customer.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & TextBox_Customer.ClientID & "',''"
                    WebImageButton_Customer.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            If ClsObjects.Find(" Code='" & ClsLocations.Table.Trim.Replace(" ", "") & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TextBox_Location.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & TextBox_Location.ClientID & "',''"
                    WebImageButton_Location.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsmmsOperationsHead.ConnectionString)
                ClsmmsOperationsHead.AddOnChangeEventToControls("frmOpHead1", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsmmsOperationsHead.ConnectionString)
                txtCode.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Auto/تلقائى")
                Setsetting(0)

                If ClsObjects.Find(" Code='mms_Items'") Then
                    If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                        HiddenField_ItemSearchID.Value = ClsSearchs.ID
                    Else
                        HiddenField_ItemSearchID.Value = 0
                    End If
                End If
                HiddenField_Language.Value = ObjNavigationHandler.SetLanguage(Page, "Eng/Arb")
                uwgItems.Rows.Clear()
                uwgItems.Rows.Add()
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsmmsOperationsHead.Find(" Number='" & IIf(IsNumeric(txtCode.Text), txtCode.Text, 0) & "'")
                IntrecordID = ClsmmsOperationsHead.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsmmsOperationsHead.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsmmsOperationsHead = New Clsmms_OperationsHead(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsmmsOperationsHead.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                SavePart()
                AfterOperation()
            Case "Save"
                SavePart()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
                If (ClsmmsOperationsHead.ID > 0) Then
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " List Item Not Found ")
                    Exit Sub
                End If
                ClsmmsOperationsHead.Delete("Number='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsmmsOperationsHead.ID & "&TableName=" & ClsmmsOperationsHead.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsmmsOperationsHead.ID & "&TableName=" & ClsmmsOperationsHead.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsmmsOperationsHead.Table
                ClsmmsOperationsHead.Find(" Number = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsmmsOperationsHead.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsmmsOperationsHead.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsmmsOperationsHead.Find(" Number= '" & txtCode.Text & "'")
                If ClsmmsOperationsHead.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsmmsOperationsHead.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsmmsOperationsHead.CheckDiff(ClsmmsOperationsHead, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsmmsOperationsHead.FirstRecord()
                GetValues()
            Case "Previous"
                ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
                If Not ClsmmsOperationsHead.previousRecord() Then
                    ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
                If Not ClsmmsOperationsHead.NextRecord() Then
                    ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsmmsOperationsHead.LastRecord()
                GetValues()
        End Select
    End Sub
    Public Function GetLastSequences(ByVal ObjectName As String, _
                                            ByVal TransactionObjectName As String, _
                                            ByVal OperationTypeID As Integer, ByVal StoreID As Integer, _
                                            Optional ByVal TransDate As Date = Nothing, _
                                            Optional ByVal SaveLast As Boolean = False, Optional ByVal FieldName As String = "Number", Optional ByVal LocationID As Integer = 0) As Integer
        Try
            Dim objObject As New Clssys_Objects(Me.Page)
            Dim objTransactionObject As New Clssys_Objects(Me.Page)
            Dim objField As New Clssys_Fields(Me.Page)
            objObject.Find("Code = '" & ObjectName.Trim & "'")
            objTransactionObject.Find("Code = '" & TransactionObjectName.Trim & "'")
            objField.Find(String.Format(" ObjectID = {0} AND FieldName = '{1}' ", objObject.ID, FieldName))
            Dim ID As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(objObject.ConnectionString, "sys_GetLastSequence", objObject.ID, objField.ID, objTransactionObject.ID, OperationTypeID, TransDate, StoreID, LocationID, 0, SaveLast, objObject.MainCompanyID, objObject.DataBaseUserRelatedID)
            Return CInt(ID)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function AssignValues() As Boolean
        Try
            With ClsmmsOperationsHead
                Dim Locations As New Clssys_Locations(Me.Page)
                If Locations.Find("Code = '" & TextBox_Location.Text & "'") Then
                    .LocationID = Locations.ID
                End If
                txtCode.Text = "102" 'GetLastSequences("mms_OperationsHead", "mms_OperationsTypes", 6, 4, Convert.ToDateTime(txtStartDate.Text), True, "Number", .LocationID)
                .Number = txtCode.Text
                Dim Customers As New Clsfcs_Customers(Me.Page)
                If Customers.Find("Code = '" & TextBox_Customer.Text & "'") Then
                    .CustomerID = Customers.ID
                End If
                .OperationDate = .SetHigriDate(txtStartDate.Text)
                .OperationTypeID = 6
                .SequenceID = 1
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsmmsOperationsHead = New Clsmms_OperationsHead(Page)
        Try
            ClsmmsOperationsHead.Find("Number='" & IIf(IsNumeric(txtCode.Text), txtCode.Text, 0) & "'")
            If ClsmmsOperationsHead.ID > 0 Then
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsmmsOperationsHead.Update("Number='" & txtCode.Text & "'")
                'Dim str As String = "delete from mms_OperationsHeadValues where ListItemID = " & ClsmmsOperationsHead.ID
                'Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsmmsOperationsHead.ConnectionString, Data.CommandType.Text, str)
                If (SaveDG(ClsmmsOperationsHead.ID)) Then
                    ClsmmsOperationsHead = New Clsmms_OperationsHead(Page)
                    clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                    ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
                    clsMainOtherFields.CollectDataAndSave(value.Text, ClsmmsOperationsHead.Table, ClsmmsOperationsHead.ID)
                    value.Text = ""
                End If
            Else
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsmmsOperationsHead.Save()
                ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
                If ClsmmsOperationsHead.ID > 0 Then
                    If (SaveDG(ClsmmsOperationsHead.ID)) Then
                        ClsmmsOperationsHead = New Clsmms_OperationsHead(Page)
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsmmsOperationsHead.Table, ClsmmsOperationsHead.ID)
                        value.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsmmsOperationsHead.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SaveDG(ByVal ListItemID As Integer) As Boolean
        ClsmmsOperationsHead = New Clsmms_OperationsHead(Page)
        Try
            'For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgItems.Rows
            '    If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
            '        Continue For
            '    End If
            '    Dim str As String = "insert into mms_OperationsHeadValues (Code,EngName,ArbName,ListItemID,RegUserID,RegDate) values ('" & DGRow.Index.ToString() + ListItemID.ToString() & "','" & DGRow.Cells(1).Value & "','" & DGRow.Cells(2).Value & "'," & ListItemID & "," & ClsmmsOperationsHead.DataBaseUserRelatedID & ",getdate())"
            '    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsmmsOperationsHead.ConnectionString, Data.CommandType.Text, str)
            'Next
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsmmsOperationsHead.ConnectionString)
        Try
            SetToolBarDefaults()

            With ClsmmsOperationsHead
                txtCode.Text = .Number

                TextBox_Customer.Text = ""
                Label_CustomerName.Text = ""
                Dim Customers As New Clsfcs_Customers(Me.Page)
                If Customers.Find("ID = " & .CustomerID) Then
                    TextBox_Customer.Text = Customers.Code
                    Label_CustomerName.Text = If(ProfileCls.CurrentLanguage = "Ar", Customers.ArbName, Customers.EngName)
                End If

                TextBox_Location.Text = ""
                Label_LocationName.Text = ""
                Dim Locations As New Clssys_Locations(Me.Page)
                If Locations.Find("ID = " & .LocationID) Then
                    TextBox_Location.Text = Locations.Code
                    Label_LocationName.Text = If(ProfileCls.CurrentLanguage = "Ar", Locations.ArbName, Locations.EngName)
                End If
                txtStartDate.Text = IIf(.OperationDate = DateTime.MinValue, "", .OperationDate)

                'Dim str As String = "select * from mms_OperationsHeadValues where ListItemID = " & ClsmmsOperationsHead.ID
                'uwgItems.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsmmsOperationsHead.ConnectionString, Data.CommandType.Text, str).Tables(0).DefaultView
                'uwgItems.DataBind()

            End With
            If Not ClsmmsOperationsHead.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsmmsOperationsHead.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsmmsOperationsHead.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsmmsOperationsHead.RegDate).Date
            End If
            If ClsmmsOperationsHead.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsmmsOperationsHead.CancelDate).Date
            End If

            If (ClsmmsOperationsHead.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
                txtCode.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Auto/تلقائى")
            End If
            SetToolBarPermission(Me, ClsmmsOperationsHead.ConnectionString, ClsmmsOperationsHead.DataBaseUserRelatedID, ClsmmsOperationsHead.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsmmsOperationsHead.ConnectionString, ClsmmsOperationsHead.DataBaseUserRelatedID, ClsmmsOperationsHead.GroupID, ClsmmsOperationsHead.Table, ClsmmsOperationsHead.ID)
            If Not ClsmmsOperationsHead.CancelDate = Nothing Then

            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsmmsOperationsHead.ID)
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
                    ImageButton_Print.Enabled = .Item("AllowPrint")
                    Select Case Mode
                        Case "N", "R"
                            ImageButton_Save.Enabled = .Item("AllowAdd")

                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")
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
                Case "D"
                    ClsmmsOperationsHead.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                Case "E"
                    ClsmmsOperationsHead.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsmmsOperationsHead
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsmmsOperationsHead = New Clsmms_OperationsHead(Me)
        If IntId > 0 Then
            ClsmmsOperationsHead.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsmmsOperationsHead = New Clsmms_OperationsHead(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsmmsOperationsHead.ConnectionString)
        Try
            ClsmmsOperationsHead.Find("Number='" & txtCode.Text & "'")
            IntId = ClsmmsOperationsHead.ID
            If ClsmmsOperationsHead.ID > 0 Then
                GetValues()
                StrMode = "E"
                uwgItems.Rows.Add()
            Else
                If ClsmmsOperationsHead.CheckRecordExistance(" Number='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsmmsOperationsHead.ConnectionString, ClsmmsOperationsHead.DataBaseUserRelatedID, ClsmmsOperationsHead.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsmmsOperationsHead.ConnectionString, ClsmmsOperationsHead.DataBaseUserRelatedID, ClsmmsOperationsHead.GroupID, ClsmmsOperationsHead.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then

            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsmmsOperationsHead.Clear()
        GetValues()
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        uwgItems.Rows.Clear()
        uwgItems.Rows.Add()
        txtCode.Text = ""
        TextBox_Customer.Text = ""
        Label_CustomerName.Text = ""
        TextBox_Location.Text = ""
        Label_LocationName.Text = ""
        txtStartDate.Text = ""
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsmmsOperationsHead.Table) = True Then
            Dim StrTablename As String
            ClsmmsOperationsHead = New Clsmms_OperationsHead(Me)
            StrTablename = ClsmmsOperationsHead.Table
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

#Region "Page Method"
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetItemName(ByVal Args As String) As String
        Dim ConnString As String = ConfigurationManager.AppSettings("Connstring").ToString()
        Dim strID As String = ""
        Dim strEngName As String = ""
        Dim strArbName As String = ""
        Dim arg As String() = Args.Split("|")

        Dim sqlstr As String = "select ID,EngName,ArbName from mms_Items where CancelDate is null and Code = '" & arg(0).ToString() & "'"
        Dim Dt As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, sqlstr).Tables(0)
        If Dt.Rows.Count > 0 Then
            strID = Dt.Rows(0)("ID").ToString()
            strEngName = Dt.Rows(0)("EngName").ToString()
            strArbName = Dt.Rows(0)("ArbName").ToString()
            Return strID & "|" & strEngName & "|" & strArbName
        Else
            Return String.Empty
        End If
    End Function
#End Region

    Protected Sub TextBox_Customer_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox_Customer.TextChanged
        Dim Customers As New Clsfcs_Customers(Me.Page)
        If Customers.Find("Code = '" & TextBox_Customer.Text & "'") Then
            Label_CustomerName.Text = If(ProfileCls.CurrentLanguage = "Ar", Customers.ArbName, Customers.EngName)
        Else
            TextBox_Customer.Text = ""
            Label_CustomerName.Text = ""
        End If
    End Sub
    Protected Sub TextBox_Location_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox_Location.TextChanged
        Dim Locations As New Clssys_Locations(Me.Page)
        If Locations.Find("Code = '" & TextBox_Location.Text & "'") Then
            Label_LocationName.Text = If(ProfileCls.CurrentLanguage = "Ar", Locations.ArbName, Locations.EngName)
        Else
            TextBox_Location.Text = ""
            Label_LocationName.Text = ""
        End If
    End Sub
End Class
