Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmEmployeesItems
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsEmployeesItems As Clshrs_EmployeesItems
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsItems As Clshrs_Items
    Private ClsEmployees As Clshrs_Employees

    Dim Item As New System.Web.UI.WebControls.ListItem()

    Const intEmpSearchID = 90
    Const intItemSearchID = 82
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployeesItems = New Clshrs_EmployeesItems(Me)
        ClsEmployees = New Clshrs_Employees(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Dim clsItems As New Clshrs_Items(Me)
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & clsItems.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtItem.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtItem.ClientID & "'"
                    btnSearchItem.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            If ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployee.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtEmployee.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            txtReceivedDate.ReadOnly = False
            txtReturnedDate.ReadOnly = False

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsEmployeesItems.ConnectionString)
                ClsEmployeesItems.AddOnChangeEventToControls("frmEmployeesItems", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                Setsetting(0)
                
                txtItem.ReadOnly = True
                btnSearchItem.Enabled = False
                txtEmployee.Focus()
            End If
            uwgEmployeeItems.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect
            uwgEmployeeItems.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            uwgEmployeeItems.DisplayLayout.AllowAddNewDefault = Infragistics.WebUI.UltraWebGrid.AllowAddNew.No
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtID.Value <> "") Then
                ClsEmployeesItems.Find(" ID=" & txtID.Value & "")
                IntrecordID = ClsEmployeesItems.ID
                If (IntrecordID > 0) Then
                    SetScreenInformation("E")
                    AddEventToControls()
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesItems.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsEmployeesItems = New Clshrs_EmployeesItems(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ClsItems As New Clshrs_Items(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesItems.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                Dim tempItemCode As String = txtItem.Text
                Dim tempReceivingItemstatus As String = txtReceivingItemstatus.Text
                Dim tempReturnedItemstatus As String = txtReturnedItemstatus.Text
                If Not txtItem.Text = "" Then
                    If SavePart() = True Then
                        AfterOperation()
                        CheckCode()
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                    Else
                        txtItem.ReadOnly = False
                        txtItem.Enabled = True
                        btnSearchItem.Enabled = True
                        txtReceivedDate.Focus()
                        ImageButton_Delete.Enabled = False
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "Please choose item/الرجاء إختيار العهدة"))
                    txtItem.ReadOnly = False
                    txtItem.Enabled = True
                    btnSearchItem.Enabled = True
                    txtItem.Focus()
                End If
            Case "New"
                NewMode()
                SetToolBarPermission(Me, ClsEmployeesItems.ConnectionString, ClsEmployeesItems.DataBaseUserRelatedID, ClsEmployeesItems.GroupID, "N")
                ImageButton_New.Enabled = False
            Case "Delete"
                If txtID.Value <> String.Empty Then
                    ClsEmployeesItems.Delete("ID=" & Val(txtID.Value))
                    CheckCode()
                End If
            Case "Property"
                If txtID.Value <> String.Empty Then
                    If ClsEmployeesItems.Find("ID=" & Val(txtID.Value)) Then
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEmployeesItems.ID & "&TableName=" & ClsEmployeesItems.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
                    End If
                End If
            Case "Remarks"
                If txtID.Value <> String.Empty Then
                    If ClsEmployeesItems.Find("ID=" & Val(txtID.Value)) Then
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEmployeesItems.ID & "&TableName=" & ClsEmployeesItems.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                    End If
                End If
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEmployeesItems.Table
                ClsEmployeesItems.Find("ID=" & Val(txtID.Value))
                Dim recordID As Integer = ClsEmployeesItems.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEmployeesItems.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEmployeesItems.Find("ID=" & Val(txtID.Value))
                If ClsEmployeesItems.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsEmployeesItems.DataSet
                    If Not AssignValue(ClsEmployeesItems) Then
                        Exit Sub
                    End If
                    If ClsEmployeesItems.CheckDiff(ClsEmployeesItems, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsEmployees.Find(" ID = " & Val(txtEmpID.Value))
                ClsEmployees.FirstRecord("", Date.Now)
                txtEmployee.Text = ClsEmployees.Code
                txtEngName.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsEmployees.EngName & "/" & ClsEmployees.ArbName)
                txtEmpID.Value = ClsEmployees.ID
                CheckCode()
            Case "Previous"
                ClsEmployees.Find(" Code ='" & txtEmployee.Text & "'")
                If Not ClsEmployees.previousRecord(" AND hrs_employees.Code < '" & txtEmployee.Text & "'", Date.Now) Then
                    ClsEmployees.Find(" Code ='" & txtEmployee.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                txtEmployee.Text = ClsEmployees.Code
                txtEngName.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsEmployees.EngName & "\" & ClsEmployees.ArbName)
                txtEmpID.Value = ClsEmployees.ID
                CheckCode()
            Case "Next"
                ClsEmployees.Find(" Code ='" & txtEmployee.Text & "'")
                If Not ClsEmployees.NextRecord(" AND hrs_employees.Code > '" & txtEmployee.Text & "'", Date.Now) Then
                    ClsEmployees.Find(" Code ='" & txtEmployee.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                txtEmployee.Text = ClsEmployees.Code
                txtEngName.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsEmployees.EngName & "/" & ClsEmployees.ArbName)
                txtEmpID.Value = ClsEmployees.ID
                CheckCode()
            Case "Last"
                ClsEmployees.Find(" Code ='" & txtEmployee.Text & "'")
                ClsEmployees.LastRecord("", Date.Now)
                txtEmployee.Text = ClsEmployees.Code
                txtEngName.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsEmployees.EngName & "/" & ClsEmployees.ArbName)
                txtEmpID.Value = ClsEmployees.ID
                CheckCode()
        End Select
    End Sub
    Protected Sub txtEmployee_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployee.TextChanged
        Dim ClsItems As New Clshrs_Items(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsItems.ConnectionString)
        If CheckEmpContract() Then
            CheckCode()
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "This Employee Has No Valid Contract!/هذا الموظف غير موجود"))
            Clear()
            AfterOperation()
            txtEmployee.Text = ""
            txtEngName.Text = ""
            txtEmpID.Value = "0"
            txtEmployee.Focus()
        End If

    End Sub
    Protected Sub txtItem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItem.TextChanged
        Dim ClsItems As New Clshrs_Items(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsItems.ConnectionString)
        If CheckEmpContract() Then
            If ClsItems.Find("Code='" & txtItem.Text & "'") Then
                txtItem.Text = ClsItems.Code
                txtItemName.Text = ObjNavigationHandler.SetLanguage(Me, ClsItems.EngName & "/" & ClsItems.ArbName)
                txtReceivedDate.Focus()
            Else
                txtItem.Text = ""
                txtItemName.Text = ""
                txtItem.Focus()
            End If
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "Please Select Employee/الرجاء إختيار الموظف"))
            txtItem.Text = ""
            txtItemName.Text = ""
            txtEmployee.Focus()
        End If
    End Sub
    Private Function CheckEmpContract() As Boolean
        Dim clsContracts As New Clshrs_Contracts(Me.Page)
        ClsEmployees = New Clshrs_Employees(Me.Page)
        ClsEmployees.Find(" Code = '" & txtEmployee.Text.Trim & "'")
        Dim EmployeeID As Integer = ClsEmployees.ID
        If Not ClsEmployees.ID > 0 Then
            Return True
        End If
        clsContracts.ContractValidatoinId(EmployeeID, Date.Now)
        If clsContracts.ID > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub uwgEmployeeItems_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgEmployeeItems.InitializeRow
        ClsItems = New Clshrs_Items(Page)
        e.Row.Cells.FromKey("ReceivedDate").Value = ClsItems.GetHigriDate(e.Row.Cells.FromKey("ReceivedDate").Value)
        e.Row.Cells.FromKey("ReturnedDate").Value = ClsItems.GetHigriDate(e.Row.Cells.FromKey("ReturnedDate").Value)
    End Sub
    Protected Sub uwgEmployeeItems_ActiveRowChange(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgEmployeeItems.ActiveRowChange
        If e.Row.Cells.FromKey("ID").Value <> Nothing Then
            ClsEmployeesItems = New Clshrs_EmployeesItems(Page)
            If ClsEmployeesItems.Find("ID=" & e.Row.Cells.FromKey("ID").Value) Then
                GetValues()
            End If
        End If
    End Sub

#End Region

#Region "Private Functions"
    Private Sub AddEventToControls()
        ImageButton_Documents.Attributes.Add("onclick", "OpenModalNew('frmItemDocuments.aspx?TB=" & ClsEmployeesItems.Table.ToString.Trim() & "&SV=" & ClsEmployeesItems.ID & "&combId=" & "EmployeeID#" & ClsEmployeesItems.EmployeeID & "#" & "ItemID#" & ClsEmployeesItems.ItemID & "&',495,800); return false;")
    End Sub
    Private Function CheckItemDate(ByVal ItemID As Integer) As Integer
        Dim ObjRow As Data.DataRow
        Dim x As Integer = 0
        ClsEmployeesItems.Find("ItemID=" & ItemID & " And Canceldate is null " & IIf(Val(txtID.Value) > 0, " And ID <> " & Val(txtID.Value), ""))
        For Each ObjRow In ClsEmployeesItems.DataSet.Tables(0).Rows
            If Not IsDBNull(ObjRow.Item("ReturnedDate")) Then
                If txtReturnedDate.Value Is Nothing Then
                    If txtReceivedDate.Value >= ObjRow.Item("ReceivedDate") And txtReceivedDate.Value <= ObjRow.Item("ReturnedDate") Then
                        Return 1
                    ElseIf txtReceivedDate.Value < ObjRow.Item("ReceivedDate") Then
                        Return 1
                    End If
                    '
                ElseIf Not txtReturnedDate.Value Is Nothing Then
                    If txtReceivedDate.Value > ObjRow.Item("ReceivedDate") And txtReceivedDate.Value < ObjRow.Item("ReturnedDate") Or txtReturnedDate.Value > ObjRow.Item("ReceivedDate") And txtReturnedDate.Value < ObjRow.Item("ReturnedDate") Then
                        x += 1
                        Exit For
                    End If
                End If
            Else
            End If
        Next
        Return (x)
    End Function
    Private Function NewCheckItemDate(ByVal ItemID As Integer) As Integer
        Dim ObjRow As Data.DataRow
        Dim x As Integer = 0
        Dim ReceivedDate As Date
        Dim ReturnedDate As Date
        ReceivedDate = IIf(txtReceivedDate.Text.Trim = "", Nothing, ClsItems.SetHigriDate(txtReceivedDate.Value))
        ReturnedDate = IIf(txtReturnedDate.Text.Trim = "", Nothing, ClsItems.SetHigriDate(txtReturnedDate.Value))
        ClsEmployeesItems.Find("ItemID=" & ItemID & " And Canceldate is null " & IIf(Val(txtID.Value) > 0, " And ID <> " & Val(txtID.Value), ""))
        For Each ObjRow In ClsEmployeesItems.DataSet.Tables(0).Rows
            If Not IsDBNull(ObjRow.Item("ReturnedDate")) Then
                If ReturnedDate.Year = 1 Then
                    If ReceivedDate >= ObjRow.Item("ReceivedDate") And ReceivedDate <= ObjRow.Item("ReturnedDate") Then
                        Return 1
                    ElseIf ReceivedDate <= ObjRow.Item("ReceivedDate") Then
                        Return 1
                    End If
                ElseIf Not ReturnedDate.Year = 1 Then
                    If ReceivedDate >= ObjRow.Item("ReceivedDate") And ReceivedDate <= ObjRow.Item("ReturnedDate") Or ReturnedDate > ObjRow.Item("ReceivedDate") And ReturnedDate < ObjRow.Item("ReturnedDate") Then
                        x += 1
                        Exit For
                    End If
                End If
            Else
                If ReturnedDate.Year = 1 Then
                    Return 1
                ElseIf Not ReturnedDate.Year = 1 Then
                    If ReceivedDate >= ObjRow.Item("ReceivedDate") Or ReturnedDate >= ObjRow.Item("ReceivedDate") Then
                        Return 1
                    End If
                End If
            End If
        Next
        Return (x)
    End Function
    Private Function AssignValue(ByRef ClsEmployeesItems As Clshrs_EmployeesItems) As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        ClsItems = New Clshrs_Items(Page)
        ClsItems.Find(" Code ='" & txtItem.Text & "'")
        If (ClsItems.ID > 0) Then
            If DateDiff(DateInterval.Day, ClsItems.PurchaseDate, CDate(txtReceivedDate.Text)) < 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " Recieved Date must greater than purchase date")
                Return False
            End If
        End If
        Try
            With ClsEmployeesItems
                .EmployeeID = Val(txtEmpID.Value)
                .ItemID = ClsItems.ID
                .ReceivedDate = IIf(txtReceivedDate.Text.Trim = "", Nothing, .SetHigriDate(txtReceivedDate.Value))
                .ReturnedDate = IIf(txtReturnedDate.Text.Trim = "", Nothing, .SetHigriDate(txtReturnedDate.Value))
                .ReceivingItemstatus = txtReceivingItemstatus.Text
                .ReturningItemstatus = txtReturnedItemstatus.Text
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues() As Boolean
        ClsEmployees = New Clshrs_Employees(Page)
        ClsItems = New Clshrs_Items(Page)
        Dim ClsUser As New Clssys_Users(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsItems.ConnectionString)
        Dim ObjClsEmployeeItems As New Clshrs_EmployeesItems(Me.Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsEmployeesItems
                txtID.Value = .ID
                txtReceivedDate.Value = .GetHigriDate(.ReceivedDate)
                txtReceivingItemstatus.Text = .ReceivingItemstatus
                txtReturnedDate.Value = .GetHigriDate(.ReturnedDate)
                txtReturnedItemstatus.Text = .ReturningItemstatus
                txtEmpID.Value = .EmployeeID
                ClsItems.Find(" ID =" & .ItemID)
                txtItem.Text = ClsItems.Code
                txtItemID.Value = .ItemID
                CheckBox_IsConfirmed.Checked = .IsConfirmed
                CheckBox_IsFromAssets.Checked = .IsFromAssets
                txtItemName.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsItems.EngName & "/" & ClsItems.ArbName)
            End With
            If Not ClsEmployeesItems.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEmployeesItems.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsEmployeesItems.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsEmployeesItems.RegDate).Date
            End If
            If ClsEmployeesItems.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsEmployeesItems.CancelDate).Date
            End If
            If Not ClsEmployeesItems.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If

            If (ClsEmployeesItems.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsEmployeesItems.ConnectionString, ClsEmployeesItems.DataBaseUserRelatedID, ClsEmployeesItems.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployeesItems.ConnectionString, ClsEmployeesItems.DataBaseUserRelatedID, ClsEmployeesItems.GroupID, ClsEmployeesItems.Table, ClsEmployeesItems.ID)
            If Not ClsEmployeesItems.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsEmployeesItems.ID)
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim clsContracts As New Clshrs_Contracts(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        ClsEmployeesItems = New Clshrs_EmployeesItems(Page)
        ClsItems = New Clshrs_Items(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsItems.ConnectionString)

        Dim EmployeeID As Integer = Convert.ToInt32(txtEmpID.Value)
        If txtID.Value = String.Empty Then txtID.Value = 0
        ClsEmployeesItems.Find("ID=" & Val(txtID.Value))
        Try
            Dim ReceivedDate As Date
            Dim ReturnedDate As Date
            ReceivedDate = IIf(txtReceivedDate.Text.Trim = "", Nothing, ClsItems.SetHigriDate(txtReceivedDate.Value))
            ReturnedDate = IIf(txtReturnedDate.Text.Trim = "", Nothing, ClsItems.SetHigriDate(txtReturnedDate.Value))

            clsContracts.ContractValidatoinId(EmployeeID, ReceivedDate)
            If Not clsContracts.ID > 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "Invalid recieved date, Check employee contract /خطأ في تاريخ الاستلام الرجاء التأكد من عقد الموظف"))
                Return False
            End If
            If txtReturnedDate.Text <> "" Then
                clsContracts.ContractValidatoinId(EmployeeID, ReturnedDate)
                If Not clsContracts.ID > 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "Invalid return date, Check employee contract /خطأ في تاريخ الاسترجاع الرجاء التأكد من عقد الموظف"))
                    Return False
                End If
            End If

            ClsItems.Find(" Code = '" & txtItem.Text & "'")
            If Not ReturnedDate.Year = 1 Then
            Else
                If ClsItems.ExpiryDate = Nothing Then
                    ClsItems.ExpiryDate = CDate("01/01/2079")
                End If
                ReturnedDate = CDate(ClsItems.ExpiryDate)
            End If

            If ClsItems.PurchaseDate <> Nothing Then
                If DateTime.Compare(ReceivedDate, CDate(ClsItems.PurchaseDate)) >= 0 Then
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "Invalid recieved date ,Check purchase date of this item /خطأ في تاريخ الاستلام الرجاء التأكد من عقد الموظف"))
                    Return False
                End If
            End If
            ClsItems.Find(" Code = '" & txtItem.Text & "'")

            If ClsEmployeesItems.ChecckTrainingDate(EmployeeID, ClsItems.ID, ReceivedDate, Val(txtID.Value)) = 1 Then
                If NewCheckItemDate(ClsItems.ID) = 0 Then
                    ClsEmployeesItems.Find("ID=" & Val(txtID.Value))
                    If Not AssignValue(ClsEmployeesItems) Then
                        Exit Function
                    End If
                    If ClsEmployeesItems.ID > 0 Then
                        ClsEmployeesItems.Update("ID=" & IIf(txtID.Value.Trim = String.Empty, "0", Val(txtID.Value)))
                    Else
                        ClsEmployeesItems.IsConfirmed = True
                        ClsEmployeesItems.Save()
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "This Item is given To Another Employee In This Period/هذه العهدة تم تسليمها لموظف آخر في داخل الفترة"))
                    Return False
                End If
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "Invalid Date/خطأ في التاريخ"))
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

                            ImageButton_SaveN.Enabled = .Item("AllowAdd")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                        Case "E"

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

                    If ImageButton_SaveN.Enabled = True And .Item("CanEdit") = True Then

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
                    txtID.Value = String.Empty
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
                    ClsEmployeesItems.Find("ID=" & intID)
                    GetValues()
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsEmployeesItems.Find("ID=" & intID)
                    GetValues()
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEmployeesItems
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
        ClsEmployeesItems = New Clshrs_EmployeesItems(Me)
        If IntId > 0 Then
            ClsEmployeesItems.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        ClsEmployeesItems = New Clshrs_EmployeesItems(Me)
        ClsEmployees = New Clshrs_Employees(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesItems.ConnectionString)
        Try
            ClsEmployees.Find("Code='" & txtEmployee.Text & "'")
            If ClsEmployees.ID > 0 Then
                txtEmpID.Value = ClsEmployees.ID
                txtEngName.Text = ClsEmployees.FullName
                Clear()
                ClsEmployeesItems.Find(" EmployeeID = " & ClsEmployees.ID)
                If ClsEmployeesItems.ID > 0 Then
                Else
                    SetToolBarDefaults()
                    SetToolBarPermission(Me, ClsEmployeesItems.ConnectionString, ClsEmployeesItems.DataBaseUserRelatedID, ClsEmployeesItems.GroupID, "N")
                    ImageButton_Delete.Enabled = False
                End If
                Load_DataGrid(Val(txtEmpID.Value))
                NewMode()
            Else
                txtEmployee.Text = ""
                txtEngName.Text = ""
                txtEmpID.Value = "0"
                Clear()
                SetToolBarDefaults()
                AfterOperation()
                SetToolBarPermission(Me, ClsEmployeesItems.ConnectionString, ClsEmployeesItems.DataBaseUserRelatedID, ClsEmployeesItems.GroupID, "N")
                ImageButton_Delete.Enabled = False
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Me, "This Employee Not Found! /هذا الموظف غير موجود"))
                txtEmployee.Focus()
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsEmployeesItems.Clear()
        GetValues()
        Load_DataGrid(Val(txtEmpID.Value))
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtItem, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtReceivedDate.Value = Nothing
        txtReceivingItemstatus.Text = String.Empty
        txtReturnedDate.Value = Nothing
        txtReturnedItemstatus.Text = String.Empty
        txtID.Value = String.Empty
        txtItem.Text = String.Empty
        txtItemName.Text = String.Empty
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEmployeesItems = New Clshrs_EmployeesItems(Page)
        ClsEmployeesItems.Find("ID=" & Val(txtID.Value))
        Dim recordID As Integer = ClsEmployeesItems.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployeesItems.Table) = True Then
            Dim StrTablename As String
            ClsEmployeesItems = New Clshrs_EmployeesItems(Me)
            StrTablename = ClsEmployeesItems.Table
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
    Private Function Load_DataGrid(ByVal intEmpID As Integer) As Boolean
        ClsItems = New Clshrs_Items(Me.Page)
        ClsEmployeesItems = New Clshrs_EmployeesItems(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsItems.ConnectionString)

        uwgEmployeeItems.DataSource = Nothing
        uwgEmployeeItems.DataBind()

        ClsItems.GetListItemCode(uwgEmployeeItems.Columns(2).ValueList)
        ClsItems.GetList(uwgEmployeeItems.Columns(3).ValueList)

        ClsEmployeesItems.Find("EmployeeID=" & intEmpID & " And CancelDate Is Null")
        uwgEmployeeItems.DataSource = ClsEmployeesItems.DataSet.Tables(0).DefaultView
        uwgEmployeeItems.DataBind()
    End Function
    Private Function NewMode() As Boolean
        Clear()
        txtItem.ReadOnly = False
        txtItem.Enabled = True
        btnSearchItem.Enabled = True
        txtItem.Focus()
        txtItem.Text = ""
        SetToolBarDefaults()
        ImageButton_Delete.Enabled = False
        ImageButton_SaveN.Enabled = False
    End Function

#End Region

End Class
