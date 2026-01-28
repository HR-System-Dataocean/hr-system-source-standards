Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmHIPolicy
    Inherits MainPage

#Region "Public Decleration"
    Dim ClsHIPolicy As Clshrs_HIPolicy
    Dim clsEmployees As Clshrs_Employees
    Dim clsContracts As Clshrs_Contracts
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsHIPolicy = New Clshrs_HIPolicy(Me)
        Dim clsEmployees As New Clshrs_Employees(Me)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsHIPolicy.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            If ClsObjects.Find(" Code='" & clsEmployees.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    lblSearchID.Text = ClsSearchs.ID
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsHIPolicy.ConnectionString)
                ClsHIPolicy.AddOnChangeEventToControls("frmHIPolicy", Page, UltraWebTab1)

                UwgSearchEmployees.Columns.FromKey("FullName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))

                lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lang", lblLage.Text)
                Page.Session.Add("ConnectionString", clsEmployees.ConnectionString)

                Dim clsHICompanies As New Clshrs_HICompanies(Me)
                clsHICompanies.GetDropDownList(ddlHICompany, False)

                GetData(0)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsHIPolicy.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsHIPolicy.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsHIPolicy.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsHIPolicy = New Clshrs_HIPolicy(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsHIPolicy.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsHIPolicy.ID > 0 Then
                    ClsHIPolicy.Update("code='" & txtCode.Text & "'")
                Else
                    ClsHIPolicy.Save()
                End If

                ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
                SaveGrid(ClsHIPolicy.ID)
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsHIPolicy.Table, ClsHIPolicy.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsHIPolicy.ID > 0 Then
                    ClsHIPolicy.Update("code='" & txtCode.Text & "'")
                Else
                    ClsHIPolicy.Save()
                End If
                ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
                SaveGrid(ClsHIPolicy.ID)
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsHIPolicy.Table, ClsHIPolicy.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsHIPolicy.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsHIPolicy.ID & "&TableName=" & ClsHIPolicy.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsHIPolicy.ID & "&TableName=" & ClsHIPolicy.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsHIPolicy.Table
                ClsHIPolicy.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsHIPolicy.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsHIPolicy.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsHIPolicy.Find(" Code= '" & txtCode.Text & "'")
                If ClsHIPolicy.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsHIPolicy.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsHIPolicy.CheckDiff(ClsHIPolicy, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsHIPolicy.FirstRecord()
                GetValues()
            Case "Previous"
                ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
                If Not ClsHIPolicy.previousRecord() Then
                    ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
                If Not ClsHIPolicy.NextRecord() Then
                    ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsHIPolicy.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub
    Protected Sub ddlHICompany_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlHICompany.SelectedIndexChanged
        ClsHIPolicy = New Clshrs_HIPolicy(Me)
        ClsHIPolicy.Find("Code ='" & txtCode.Text & "'")

        GetData(ClsHIPolicy.ID)

    End Sub
#End Region

#Region "Private Functions"
    Private Function GetData(HIPolicyID As Integer) As Boolean
        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsHIPolicy.ConnectionString)

            Dim str As String = " Select Emp.Code,dbo.fn_GetEmpName(Emp.Code," & ObjNavigationHandler.SetLanguage(Me, "0/1") & ") as FullName, HIP.ID,HIP.PolicyID,HIP.ContractID,HIP.HICompanyClasses,HIP.CompanyAmt,HIP.EmployeeAmt,HIP.ActiveDate  From dbo.hrs_HICompanyClasses inner join hrs_HIPolicyContract HIP on HIP.HICompanyClasses = hrs_HICompanyClasses.id inner join hrs_Employees Emp On Emp.ID=(Select Top 1 hrs_Contracts.EmployeeID From hrs_Contracts Where hrs_Contracts.ID=HIP.ContractID) Where HICompanyID = " & ddlHICompany.SelectedValue & " And HIP.PolicyID = " & HIPolicyID & " And HIP.CancelDate Is null Order By Emp.Code"

            Dim ds As New Data.DataSet
            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsHIPolicy.ConnectionString, Data.CommandType.Text, str)

            UwgSearchEmployees.DataSource = Nothing
            UwgSearchEmployees.DataBind()

            Dim clsHICompanies As New Clshrs_HICompanies(Me)
            clsHICompanies.GetList(UwgSearchEmployees.DisplayLayout.Bands(0).Columns.FromKey("HICompanyClasses").ValueList, "hrs_HICompanyClasses", " HICompanyID =" & ddlHICompany.SelectedValue)
            lblList.Text = UwgSearchEmployees.DisplayLayout.Bands(0).Columns.FromKey("HICompanyClasses").ValueList.ValueListItems(0).DataValue

            UwgSearchEmployees.DataSource = ds.Tables(0)
            UwgSearchEmployees.DataBind()

            UwgSearchEmployees.Rows.Add()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SaveGrid(HIPolicyID As Integer) As Boolean
        Try
            Dim ds As Data.DataSet
            Dim str As String = String.Empty
            Dim str1 As String = String.Empty
            clsEmployees = New Clshrs_Employees(Me)

            str = "set dateformat DMY; UPDATE hrs_HIPolicyContract SET CancelDate = GetDate() Where [PolicyID]=" & HIPolicyID & ";" & vbNewLine
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, Data.CommandType.Text, str)

            If hfDeletedEmployees.Value <> "" Then
                str = " Delete hrs_HIPolicyContract where ID In(" & hfDeletedEmployees.Value & ");" & vbNewLine
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, Data.CommandType.Text, str)
            End If
            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If row.Cells.FromKey("Code").Value <> Nothing Then
                    If row.Cells.FromKey("HICompanyClasses").Value <> Nothing Then
                        clsEmployees = New Clshrs_Employees(Me)
                        If clsEmployees.Find("Code='" & row.Cells.FromKey("Code").Value & "'") Then
                            clsContracts = New Clshrs_Contracts(Me)
                            If clsContracts.Find("EmployeeID=" & clsEmployees.ID) Then
                                str1 = " SELECT * FROM hrs_HIPolicyContract WHERE [PolicyID]=" & HIPolicyID & " And [ContractID]=" & clsContracts.ID & " And [HICompanyClasses]=" & row.Cells.FromKey("HICompanyClasses").Value & " And [CancelDate] Is Null"
                                ds = New Data.DataSet
                                ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsEmployees.ConnectionString, Data.CommandType.Text, str1)
                                str = "set dateformat DMY; "
                                If ds.Tables(0).Rows.Count > 0 Then
                                    str &= _
                                        " UPDATE hrs_HIPolicyContract SET" & _
                                        " [CompanyAmt] = " & IIf(row.Cells.FromKey("CompanyAmt").Value = Nothing, 0, row.Cells.FromKey("CompanyAmt").Value) & _
                                        ",[EmployeeAmt] = " & IIf(row.Cells.FromKey("EmployeeAmt").Value = Nothing, 0, row.Cells.FromKey("EmployeeAmt").Value) & _
                                        ",[ActiveDate] = " & IIf(row.Cells.FromKey("ActiveDate").Value = Nothing, "NULL", "'" & CDate(row.Cells.FromKey("ActiveDate").Value).ToString("dd/MM/yyyy") & "'") & _
                                        ",[CancelDate] = NULL" & _
                                        " WHERE [ID] = " & ds.Tables(0).Rows(0).Item("ID") & " ;" & vbNewLine
                                Else
                                    str &= " INSERT INTO hrs_HIPolicyContract ([ContractID], [PolicyID], [HICompanyClasses],[CompanyAmt],[EmployeeAmt],[ActiveDate])VALUES" & _
                                    "(" & clsContracts.ID & _
                                    "," & HIPolicyID & _
                                    "," & row.Cells.FromKey("HICompanyClasses").Value & _
                                    "," & IIf(row.Cells.FromKey("CompanyAmt").Value = Nothing, 0, row.Cells.FromKey("CompanyAmt").Value) & _
                                    "," & IIf(row.Cells.FromKey("EmployeeAmt").Value = Nothing, 0, row.Cells.FromKey("EmployeeAmt").Value) & _
                                    "," & IIf(row.Cells.FromKey("ActiveDate").Value = Nothing, "NULL", "'" & CDate(row.Cells.FromKey("ActiveDate").Value).ToString("dd/MM/yyyy") & "'") & _
                                    ");" & vbNewLine
                                End If
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, Data.CommandType.Text, str)
                            End If
                        End If
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AssignValues() As Boolean
        Try
            With ClsHIPolicy
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .PolicyNo = txtPolicyNo.Text
                .FromDate = txtFromDate.Value
                .ToDate = txtToDate.Value
                .HICompanyID = ddlHICompany.SelectedItem.Value
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
            SetToolBarDefaults()
            txtCode.Text = ClsHIPolicy.Code
            txtEngName.Text = ClsHIPolicy.EngName
            txtArbName.Text = ClsHIPolicy.ArbName
            txtPolicyNo.Text = ClsHIPolicy.PolicyNo
            txtFromDate.Value = ClsHIPolicy.FromDate
            txtToDate.Value = ClsHIPolicy.ToDate
            ddlHICompany.SelectedValue = ClsHIPolicy.HICompanyID
            GetData(ClsHIPolicy.ID)

            UwgSearchEmployees.Rows.Add()

            If Not ClsHIPolicy.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsHIPolicy.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsHIPolicy.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsHIPolicy.RegDate).Date
            End If
            If ClsHIPolicy.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsHIPolicy.CancelDate).Date
            End If
            If Not ClsHIPolicy.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()

            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsHIPolicy.ConnectionString)
            If (ClsHIPolicy.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsHIPolicy.ConnectionString, ClsHIPolicy.DataBaseUserRelatedID, ClsHIPolicy.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsHIPolicy.ConnectionString, ClsHIPolicy.DataBaseUserRelatedID, ClsHIPolicy.GroupID, ClsHIPolicy.Table, ClsHIPolicy.ID)
            If Not ClsHIPolicy.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsHIPolicy.ID)
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
                    ClsHIPolicy.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsHIPolicy.Find("ID=" & intID)
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
            With ClsHIPolicy
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
        ClsHIPolicy = New Clshrs_HIPolicy(Me)
        Try
            With ClsHIPolicy
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
        ClsHIPolicy = New Clshrs_HIPolicy(Me)
        If IntId > 0 Then
            ClsHIPolicy.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsHIPolicy = New Clshrs_HIPolicy(Me)
        Try
            ClsHIPolicy.Find("Code='" & txtCode.Text & "'")
            IntId = ClsHIPolicy.ID
            txtEngName.Focus()
            If ClsHIPolicy.ID > 0 Then
                GetValues()
                GetData(ClsHIPolicy.ID)
                StrMode = "E"
            Else
                If ClsHIPolicy.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)

            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsHIPolicy.ConnectionString, ClsHIPolicy.DataBaseUserRelatedID, ClsHIPolicy.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsHIPolicy.ConnectionString, ClsHIPolicy.DataBaseUserRelatedID, ClsHIPolicy.GroupID, ClsHIPolicy.Table, IntId)
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
        ClsHIPolicy.Clear()
        GetValues()
        ImageButton_Delete.Enabled = False
        UwgSearchEmployees.Rows.Clear()
        UwgSearchEmployees.Rows.Add()


        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        txtPolicyNo.Text = String.Empty
        txtFromDate.Value = String.Empty
        txtToDate.Value = String.Empty
        UwgSearchEmployees.Rows.Clear()
        UwgSearchEmployees.Rows.Add()


        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsHIPolicy = New Clshrs_HIPolicy(Page)
        ClsHIPolicy.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsHIPolicy.ID
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
    Protected Sub UwgSearchEmployees_Cellchanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles UwgSearchEmployees.UpdateCell

        '--------- Check Duplicate (EmployeeCode + ClassID) ---------
        Dim EmpCode As String = e.Cell.Row.Cells.FromKey("Code").Value
        Dim ClassID As Integer = e.Cell.Row.Cells.FromKey("HICompanyClasses").Value

        If Not String.IsNullOrEmpty(EmpCode) AndAlso ClassID > 0 Then

            Dim currentRowIndex As Integer = e.Cell.Row.Index
            Dim duplicateRowIndex As Integer = -1

            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If row.Index <> currentRowIndex Then  ' skip current row
                    If row.Cells.FromKey("Code").Value = EmpCode AndAlso
                   row.Cells.FromKey("HICompanyClasses").Value = ClassID Then

                        duplicateRowIndex = row.Index
                        Exit For
                    End If
                End If
            Next

            If duplicateRowIndex >= 0 Then
                Dim msg As String
                If lblLage.Text = "0" Then
                    msg = "This employee and class already exist in line " & (duplicateRowIndex + 1).ToString()
                Else
                    msg = "هذا الموظف وهذه الفئة موجودين بالفعل في السطر " & (duplicateRowIndex + 1).ToString()
                End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "dupMsg", "alert('" & msg & "');", True)

                ' Return focus to class cell
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "focusCell",
            "var row = igtbl_getActiveRow('" & UwgSearchEmployees.ClientID & "'); if(row) row.getCellFromKey('HICompanyClasses').activate();",
            True)

                Exit Sub
            End If
        End If

        '--------- Existing Code ---------
        If e.Cell.Key = "Code" Then
            EmpCode = e.Cell.Row.Cells.FromKey("Code").Value
            Dim EmpName As String = GetEmpName(EmpCode)
            e.Cell.Row.Cells.FromKey("FullName").Value = EmpName
        End If

        If e.Cell.Key = "HICompanyClasses" Then
            ClassID = e.Cell.Row.Cells.FromKey("HICompanyClasses").Value
            Dim HICompnayID As Integer = ddlHICompany.SelectedValue
            Dim str As String = "Select CompanyAmount,EmployeeAmount From hrs_HICompanyClasses where id=" & ClassID & " and HICompanyID=" & HICompnayID

            Dim ds As Data.DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsHIPolicy.ConnectionString, Data.CommandType.Text, str)
            If ds.Tables(0).Rows.Count > 0 Then
                e.Cell.Row.Cells.FromKey("CompanyAmt").Value = ds.Tables(0).Rows(0)(0)
                e.Cell.Row.Cells.FromKey("EmployeeAmt").Value = ds.Tables(0).Rows(0)(1)
            End If
        End If
    End Sub

    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsHIPolicy.Table) = True Then
            Dim StrTablename As String
            ClsHIPolicy = New Clshrs_HIPolicy(Me)
            StrTablename = ClsHIPolicy.Table
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

End Class
