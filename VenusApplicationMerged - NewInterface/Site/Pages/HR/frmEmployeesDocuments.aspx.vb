Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesDocuments
    Inherits MainPage

#Region "Public Decleration"

    Private ClsDocumentsInformations As Clssys_DocumentsInformations
    Private ClsDocumentAttachment As Clssys_DocumentsInformationAttachments
    Private ClsDocuments As Clssys_DocumentsTypes
    Private ClsEmployees As Clshrs_Employees
    Private ClsEmployeesDependants As Clshrs_EmployeesDependants
    Private ClsCompanies As Clssys_Companies

    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsEmployees As New Clshrs_Employees(Me.Page)
        Dim TableName As String = Request.QueryString.Item("TB")
        Dim ClsContract As New Clshrs_Contracts(Me.Page)
        Dim IntRecordId As Integer
        Dim DocNo As Object

        Try
        DocNo = Request.QueryString("DN")
        Catch ex As Exception
            DocNo = 0
        End Try
        If ClsEmployees.Find1("Code='" & lblDescEmployeeCode.Text & "'") Then
            IntRecordId = ClsEmployees.ID
        Else
            If Request.QueryString("SV") = Nothing Or Request.QueryString("SV") = "0" Then
                IntRecordId = Request.QueryString("EmpID")
            Else
                IntRecordId = Request.QueryString("SV")
            End If
        End If
        Dim IntProfiessionID As Integer
        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim ClsBirthCities As New ClsBasicFiles(Me.Page, "sys_Cities")
        Dim Clsobjects As New Clssys_Objects(Me.Page)
        Dim IntNationalityID As Integer
        Dim ClsDependant As New Clshrs_EmployeesDependants(Me.Page)
        Dim IntObjectId As Integer
        ClsDocuments = New Clssys_DocumentsTypes(Me.Page)
        ClsDocumentsInformations = New Clssys_DocumentsInformations(Me.Page)
        ClsDocumentAttachment = New Clssys_DocumentsInformationAttachments(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDocuments.ConnectionString)
        Try
            Clsobjects.Find(" Code = REPLACE('" & TableName & "',' ' ,'')")
            IntObjectId = Clsobjects.ID
            txtObjectId.Value = Clsobjects.ID
            txtEmpID.Value = IntRecordId
            txtRecordID.Value = IntRecordId
            If TableName = "hrs_Employees" Then
                Dim ClsSearchs As New Clssys_Searchs(Me.Page)
                If Clsobjects.Find(" Code='" & ClsEmployees.Table.Trim() & "'") Then
                    If ClsSearchs.Find(" ObjectID=" & Clsobjects.ID) Then
                        Dim csSearchID As Integer = ClsSearchs.ID
                        lblDescEmployeeCode.Attributes.Add("onKeyUp", "Open_Search_KeyDown(" & " " & csSearchID & " " & ",""lblDescEmployeeCode"")")
                    End If
                End If
                ClsEmployees.Find1("ID=" & IntRecordId)
                lblDescEmployeeCode.Text = ClsEmployees.Code
                lblDescEnglishName.Text = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsDocuments.ConnectionString, CommandType.Text, "Select dbo.fn_GetEmpName('" & ClsEmployees.Code & "'," & ObjNavigationHandler.SetLanguage(Page, "0/1") & ")")  'ClsEmployee.Name
                IntNationalityID = ClsEmployees.NationalityID
                If ClsContract.ContractValidatoinId(IntRecordId, Date.Now) Then
                    IntProfiessionID = ClsContract.ProfessionID
                End If
            ElseIf TableName = "hrs_EmployeesDependants" Then
                lblUser.Text = ObjNavigationHandler.SetLanguage(Page, "ID/الرقم")
                ClsDependant.Find("ID=" & IntRecordId)
                lblDescEmployeeCode.ReadOnly = True
                lblDescEmployeeCode.Text = ClsDependant.ID
                lblDescEnglishName.Text = ObjNavigationHandler.SetLanguage(Page, ClsDependant.EngName & "/" & ClsDependant.ArbName)
                IntNationalityID = ClsDependant.NationalityID
            End If

            Dim clsSearchsColumns = New Clssys_SearchsColumns(Me.Page)
            DdlAttachedFiles.Attributes.Add("onChange", "setlink();")
            If Not IsPostBack Then



                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
                '================================= Exit & Navigation Notification [ End ]
                Dim clsDocTypeGroups As New Clssys_DocumentTypesGroup(Me)
                clsDocTypeGroups.GetDropDownList(ddlDocumentTypesGroup, True)
                ClsBirthCities.GetDropDownList(DdlIssuedCity, True)
                If TableName = "hrs_Employees" Then
                    ClsEmployees.Find1("ID=" & IntRecordId)
                    ClsContract.ContractValidatoinId(IntRecordId, Date.Now)
                    ClsContract.Find("ID=" & ClsContract.ContractValidatoinId(IntRecordId, Date.Now))
                    ClsDocuments.GetDropDownList(ddlDocumentType, True, " isnull(IsForCompany,0) = 0", IntNationalityID, ClsContract.ProfessionID)
                ElseIf TableName = "hrs_EmployeesDependants" Then
                    ClsDependant.Find("ID=" & IntRecordId)
                    ClsDocuments.GetDropDownList(ddlDocumentType, True, ClsDependant.NationalityId, "")
                End If
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtDocumentArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                GetFormPermission("frmEmployeesDocuments")
                New_Part()
                If Convert.ToString(DocNo) <> "0" Or Convert.ToString(DocNo) <> "" Then
                    'txtDocumentNumber0.Text = DocNo
                    SetDocumentAutoSerial()

                    txtDocumentNumber_TextChanged(Nothing, Nothing)
                End If

            Else

            End If
            Dim IntrecID As Integer
            If txtID.Value <> "" Then
                ClsDocumentsInformations.Find(" ID=" & Val(txtID.Value))
                IntrecID = ClsDocumentsInformations.ID
                If (IntrecID > 0) Then
                    SetScreenInformation("E")
                Else
                    SetScreenInformation("N")
                End If
            Else
                SetScreenInformation("N")
            End If
            'Set Grid View Setting
            UwgEmployeeDocuments.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect
            UwgEmployeeDocuments.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            UwgEmployeeDocuments.DisplayLayout.AllowAddNewDefault = Infragistics.WebUI.UltraWebGrid.AllowAddNew.No
            UwgEmployeeDocuments.BorderWidth = 0
            CreateOtherFields(IntRecordId)
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsDocumentsInformations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsDocumentsInformations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_PopupCommand(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Delete.Command
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Select Case e.CommandArgument
            Case "Save"
                ClsDocuments = New Clssys_DocumentsTypes(Me.Page)
                If SavePart() Then
                    AfterOperation()
                    If ddlDocumentType.Items.Count > 1 Then
                        ddlDocumentType.SelectedIndex = 1
                    End If
                    If Request.QueryString.Count > 2 Then
                    Else
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                    End If
                End If


            Case "Delete"
                If txtID.Value <> String.Empty Then
                    ClsDocumentsInformations.Delete("ID=" & Val(txtID.Value))
                    AfterOperation()
                    If ddlDocumentType.Items.Count > 1 Then
                        ddlDocumentType.SelectedIndex = 1
                    End If
                End If
            Case "New"
                New_Part()
                If ddlDocumentType.Items.Count > 1 Then
                    ddlDocumentType.SelectedIndex = 1
                End If
        End Select
        SetDocumentAutoSerial()
    End Sub
    Private Sub ConfigureGridSafetySettings()
        ' إعدادات الأداء والسلامة
        UwgEmployeeDocuments.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet


        UwgEmployeeDocuments.DisplayLayout.StationaryMargins = Infragistics.WebUI.UltraWebGrid.StationaryMargins.Header

        ' ضبط تخطيط الصفحة
        UwgEmployeeDocuments.DisplayLayout.Pager.AllowPaging = True
        UwgEmployeeDocuments.DisplayLayout.Pager.PageSize = 29
    End Sub
    Protected Sub UwgEmployeeDocuments_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UwgEmployeeDocuments.ActiveRowChange
        'Rabie12-11-2025
        Try
            Dim documentNumber As String = TryCast(e.Row.Cells.FromKey("DocumentNumber").Value, String)
            If Not String.IsNullOrEmpty(documentNumber) Then
                txtDocumentNumber0.Text = e.Row.Cells.FromKey("DocumentNumber").Value
                txtDocumentNumber_TextChanged(Nothing, Nothing)
            End If
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub btnDeleteAttachment_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnDeleteAttachment.Click
        Dim TableName As String = Request.QueryString.Item("TB")
        Dim EmployeeID As Integer
        If TableName = "hrs_Employees" Then
            ClsEmployees = New Clshrs_Employees(Page)
            ClsEmployees.Find1("Code='" & lblDescEmployeeCode.Text & "'")
            EmployeeID = ClsEmployees.ID
        ElseIf TableName = "hrs_EmployeesDependants" Then
            ClsEmployeesDependants = New Clshrs_EmployeesDependants(Page)
            ClsEmployeesDependants.Find("ID=" & lblDescEmployeeCode.Text & "")
            EmployeeID = ClsEmployeesDependants.ID
        End If
        ClsDocumentAttachment = New Clssys_DocumentsInformationAttachments(Me.Page)
        ClsDocumentsInformations = New Clssys_DocumentsInformations(Me.Page)
        If DdlAttachedFiles.Items.Count <> 0 Then
            ClsDocumentAttachment.Delete(" Recordid=" & Val(txtID.Value) & " And ObjectID=" & GetDoc_DetailsObjectId() & " And FileName='" & DdlAttachedFiles.SelectedItem.Text & "'")
            DdlAttachedFiles.Items.Remove(DdlAttachedFiles.SelectedItem)
            If DdlAttachedFiles.Items.Count < 1 Then
                lnkDownload.Enabled = False
                btnDeleteAttachment.Enabled = False
            Else
                Dim StrRealPath As String = Request.PhysicalApplicationPath
                lnkDownload.NavigateUrl = "~/Uploads/" & DdlAttachedFiles.SelectedItem.Text
                lnkDownload.Enabled = True
                btnDeleteAttachment.Enabled = True
            End If
        End If
    End Sub
    Protected Sub ddlDocumentTypesGroup_SelectedChanged(sender As Object, e As System.EventArgs) Handles ddlDocumentTypesGroup.SelectedIndexChanged
        Dim ClsDependant As New Clshrs_EmployeesDependants(Me.Page)
        Dim IntObjectId As Integer
        ClsDocuments = New Clssys_DocumentsTypes(Me.Page)
        Dim TableName As String = Request.QueryString.Item("TB")
        Dim ClsContract As New Clshrs_Contracts(Me.Page)
        Dim ClsEmployees As New Clshrs_Employees(Me.Page)

        Dim IntNationalityID = ClsEmployees.NationalityID
        Dim IntRecordId As Integer = 0
        If ClsEmployees.Find1("Code='" & lblDescEmployeeCode.Text & "'") Then
            IntRecordId = ClsEmployees.ID
        Else
            If Request.QueryString("SV") = Nothing Or Request.QueryString("SV") = "0" Then
                IntRecordId = Request.QueryString("EmpID")
            Else
                IntRecordId = Request.QueryString("SV")
            End If
        End If

        Dim criteriaType As String = " 1=1 "
        If ddlDocumentTypesGroup.SelectedIndex > 0 Then
            criteriaType = " isnull(DocumentTypesGroupId,0)=" & ddlDocumentTypesGroup.SelectedValue
        End If

        If TableName = "hrs_Employees" Then
            ClsEmployees.Find1("ID=" & IntRecordId)
            ClsContract.ContractValidatoinId(IntRecordId, Date.Now)
            ClsContract.Find("ID=" & ClsContract.ContractValidatoinId(IntRecordId, Date.Now))
            ClsDocuments.GetDropDownList(ddlDocumentType, True, " isnull(IsForCompany,0) = 0 and " & criteriaType, IntNationalityID, ClsContract.ProfessionID)
        ElseIf TableName = "hrs_EmployeesDependants" Then
            ClsDependant.Find("ID=" & IntRecordId)
            IntNationalityID = ClsDependant.NationalityId
            ClsDocuments.GetDropDownList(ddlDocumentType, True, ClsDependant.NationalityId, criteriaType)
        End If
        AfterOperation()
    End Sub
    Protected Sub txtDocumentNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDocumentNumber0.TextChanged
        ClsDocumentsInformations = New Clssys_DocumentsInformations(Page)
        Dim TableName As String = Request.QueryString.Item("TB")
        Dim ClsContract As New Clshrs_Contracts(Me.Page)
        Dim ClsEmployees As New Clshrs_Employees(Me.Page)
        Dim EmployeeID As Integer
        If TableName = "hrs_Employees" Then
            ClsEmployees = New Clshrs_Employees(Page)
            ClsEmployees.Find1("Code='" & lblDescEmployeeCode.Text & "'")
            EmployeeID = ClsEmployees.ID
        ElseIf TableName = "hrs_EmployeesDependants" Then
            ClsEmployeesDependants = New Clshrs_EmployeesDependants(Page)
            ClsEmployeesDependants.Find("ID=" & lblDescEmployeeCode.Text & "")
            EmployeeID = ClsEmployeesDependants.ID
        End If
        Dim ClsDependant As New Clshrs_EmployeesDependants(Me.Page)
        Dim ClsUser As New Clssys_Users(Me.Page)
        If (txtObjectId.Value > 0 And txtRecordID.Value > 0 And txtDocumentNumber0.Text <> "") Then
            If ClsDocumentsInformations.Find(" DocumentNumber='" & txtDocumentNumber0.Text & "' And ObjectID=" & txtObjectId.Value & " And RecordID=" & EmployeeID, True) Then
                With ClsDocumentsInformations
                    txtDocumentNumber0.Text = .DocumentNumber
                    txtReferenceNumber.Text = .ReferenceNumber
                    txtIssueDate.Text = String.Empty
                    txtIssueDateH.Text = String.Empty
                    If .IssueDate <> Nothing Then
                        txtIssueDate.Text = ClsDataAcessLayer.FormatGreg(.IssueDate, "dd/MM/yyyy")
                        txtIssueDateH.Text = ClsDataAcessLayer.GregToHijri(.IssueDate, "dd/MM/yyyy")
                    End If

                    txtExpiryDate.Text = String.Empty
                    txtExpiryDateH.Text = String.Empty
                    If .ExpiryDate <> Nothing Then
                        txtExpiryDate.Text = ClsDataAcessLayer.FormatGreg(.ExpiryDate, "dd/MM/yyyy")
                        txtExpiryDateH.Text = ClsDataAcessLayer.GregToHijri(.ExpiryDate, "dd/MM/yyyy")
                    End If

                    txtLastRenewalDate.Text = String.Empty
                    txtLastRenewalDateH.Text = String.Empty
                    If .LastRenewalDate <> Nothing Then
                        txtLastRenewalDate.Text = ClsDataAcessLayer.FormatGreg(.LastRenewalDate, "dd/MM/yyyy")
                        txtLastRenewalDateH.Text = ClsDataAcessLayer.GregToHijri(.LastRenewalDate, "dd/MM/yyyy")
                    End If
                    txtID.Value = ClsDocumentsInformations.ID
                    txtDocumentID.Value = ClsDocumentsInformations.Document
                    ddlDocumentType.Enabled = False
                    Dim item As New System.Web.UI.WebControls.ListItem()
                    Dim ClsDocType As New Clssys_DocumentsTypes(Me.Page)
                    Dim ClsCities As New Clssys_Cities(Me.Page)
                    Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(.ConnectionString)
                    If TableName = "hrs_Employees" Then
                        ClsEmployees.Find1("ID=" & EmployeeID)
                        ClsContract.ContractValidatoinId(EmployeeID, Date.Now)
                        ClsContract.Find("ID=" & ClsContract.ContractValidatoinId(EmployeeID, Date.Now))
                        ClsDocType.GetDropDownList(ddlDocumentType, True, " isnull(IsForCompany,0) = 0", ClsEmployees.NationalityID, ClsContract.ProfessionID)
                    ElseIf TableName = "hrs_EmployeesDependants" Then
                        ClsDependant.Find("ID=" & EmployeeID)
                        ClsDocType.GetDropDownList(ddlDocumentType, True, ClsDependant.NationalityId, "")
                    End If
                    ddlDocumentType.SelectedValue = .Document
                    DdlIssuedCity.SelectedValue = .IssuedCityID
                    DdlAttachedFiles.Items.Clear()
                    If ClsDocumentAttachment.Find("RecordID=" & Val(txtID.Value) & " And ObjectID= " & GetDoc_DetailsObjectId() & " And Isnull(CancelDate , '') = '' ") Then
                        CheckPhiscalFilesExistance(DdlAttachedFiles, ClsDocumentAttachment.DataSet)
                    End If
                    If DdlAttachedFiles.Items.Count < 1 Then
                        lnkDownload.Enabled = False
                        btnDeleteAttachment.Enabled = False
                    Else

                        lnkDownload.NavigateUrl = "~/Uploads/" & DdlAttachedFiles.SelectedItem.Text 'ClsShared.GetValue(DdlAttachedFiles.SelectedItem.Value, "Name")
                        lnkDownload.Enabled = True
                        btnDeleteAttachment.Enabled = True
                    End If
                    txtIssueDate.Focus()
                End With
                ImageButton_Delete.Enabled = True
            Else
                If txtID.Value.ToString() = "" Then
                    ClearControls()
                End If
            End If
        End If
        DdlIssuedCity.Focus()
    End Sub
#End Region

#Region "Private Function"
    Private Function GetFormPermission(ByVal frmCode As String) As Boolean
        Dim ClsForms As New ClsSys_Forms(Page)
        Dim ClsFormPermission As New ClsSys_FormsPermissions(Page)
        Dim StrFormPermission As String = "1,1,1"
        If ClsForms.Find(" Code='" & frmCode & "'") Then
            ClsFormPermission.Find("FormID=" & ClsForms.ID)
            With ClsFormPermission
                If .ID > 0 Then
                    StrFormPermission = ""
                    If .AllowEdit Then
                        StrFormPermission = "0"
                    Else
                        StrFormPermission = "1"
                    End If
                    If .AllowDelete Then
                        StrFormPermission &= ",0"
                    Else
                        StrFormPermission &= ",1"
                    End If

                    If .AllowPrint Then
                        StrFormPermission &= ",0"
                    Else
                        StrFormPermission &= ",1"
                    End If
                End If
            End With
        End If
        txtFormPermission.Value = StrFormPermission
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Delete.Enabled = True
    End Function
    Private Function GetAttachedFilesData()
        Dim ClsObjects As New Clssys_Objects(Me.Page)
        Dim ClsEmployees As New Clshrs_Employees(Me.Page)
        Dim ClsDocumentDetails As New Clssys_DocumentsInformations(Me.Page)
        Dim ClsShared As New Venus.Shared.StringHandler
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsObjects.Find(" Code = REPLACE('" & ClsDocumentDetails.Table & "',' ' ,'')")
        Dim ObjectID As Integer = ClsObjects.ID
        ClsDocumentAttachment = New Clssys_DocumentsInformationAttachments(Me.Page)
        If ClsDocumentAttachment.Find("RecordID=" & ClsDocumentsInformations.ID & " And ObjectID=" & ObjectID & " And FileName='" & DdlAttachedFiles.SelectedItem.Value & "'") Then
            txtDocumentEngName.Text = ClsDocumentAttachment.FileName
            txtDocumentArbName.Text = ClsDocumentAttachment.FileName
        End If
    End Function
    Private Function AssignValue(ByRef ClsDocumentsInformations As Clssys_DocumentsInformations) As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim TableName As String = Request.QueryString.Item("TB")
        Dim EmployeeID As Integer
        If TableName = "hrs_Employees" Then
            ClsEmployees = New Clshrs_Employees(Page)
            ClsEmployees.Find1("Code='" & lblDescEmployeeCode.Text & "'")
            EmployeeID = ClsEmployees.ID
        ElseIf TableName = "hrs_EmployeesDependants" Then
            ClsEmployeesDependants = New Clshrs_EmployeesDependants(Page)
            ClsEmployeesDependants.Find("ID=" & lblDescEmployeeCode.Text & "")
            EmployeeID = ClsEmployeesDependants.ID
        End If
        Dim ClsDocumentDetails As New Clssys_DocumentsInformations(Me.Page)
        Dim Clsobjects As New Clssys_Objects(Me.Page)
        Dim ClsDocumentAttachment As New Clssys_DocumentsInformationAttachments(Me.Page)
        Clsobjects.Find(" Code = REPLACE('" & TableName & "',' ' ,'')")
        Try
            With ClsDocumentsInformations
                .DbObject = Clsobjects.ID
                .Record = EmployeeID
                .Document = ddlDocumentType.SelectedValue
                .DocumentNumber = txtDocumentNumber0.Text
                .ReferenceNumber = txtReferenceNumber.Text
                .IssuedCityID = DdlIssuedCity.SelectedItem.Value
                If SetDate("  /  /    ", txtIssueDateH.Text) = .IssueDate Then
                    .IssueDate = SetDate(txtIssueDate.Text, txtIssueDateH.Text)
                Else
                    .IssueDate = SetDate("  /  /    ", txtIssueDateH.Text)
                End If
                If SetDate("  /  /    ", txtExpiryDateH.Text) = .ExpiryDate Then
                    .ExpiryDate = SetDate(txtExpiryDate.Text, txtExpiryDateH.Text)
                Else
                    .ExpiryDate = SetDate("  /  /    ", txtExpiryDateH.Text)
                End If
                If SetDate("  /  /    ", txtLastRenewalDateH.Text) = .LastRenewalDate Then
                    .LastRenewalDate = SetDate(txtLastRenewalDate.Text, txtLastRenewalDateH.Text)
                Else
                    .LastRenewalDate = SetDate("  /  /    ", txtLastRenewalDateH.Text)
                End If
                .PostedFile = txtAttachedFile.PostedFile
                ClsDocumentAttachment.EngName = txtDocumentEngName.Text
                ClsDocumentAttachment.ArbName = txtDocumentArbName.Text
            End With

            'Edited by: Hassan Kurdi
            'Edit Date: 2021-06-24
            'Purpose: Add a validation for Expiry date must be greater than issue date
            If(ClsDocumentsInformations.IssueDate.ToString() <> Nothing And ClsDocumentsInformations.ExpiryDate.ToString() <>  Nothing)Then
                If(ClsDocumentsInformations.ExpiryDate <= ClsDocumentsInformations.IssueDate)Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Expiry date must be greater than issue date / تاريخ الانتهاء يجب أن يكون أكبر من تاريخ الإصدار"))
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SetDate(gData As Object, hDate As Object) As Object
        Try
            If gData <> "  /  /    " Then
                If ClsDataAcessLayer.IsGreg(gData) Then
                    Return ClsDataAcessLayer.FormatGreg(gData, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.HijriToGreg(gData, "dd/MM/yyyy")
                End If
            ElseIf hDate <> "  /  /    " Then
                If ClsDataAcessLayer.IsHijri(hDate) Then
                    Return ClsDataAcessLayer.HijriToGreg(hDate, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.FormatGreg(hDate, "dd/MM/yyyy")
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function CheckPhiscalFilesExistance(ByRef ddlControl As System.Web.UI.WebControls.DropDownList, ByVal ObjDataset As Data.DataSet)
        Dim Item As System.Web.UI.WebControls.ListItem
        Dim ObjDataRow As Data.DataRow
        Dim StrRealPath As String = Request.PhysicalApplicationPath
        ddlControl.Items.Clear()
        For Each ObjDataRow In ObjDataset.Tables(0).Rows
            If System.IO.File.Exists(StrRealPath & "\Uploads\" & ObjDataRow("FileName")) Then
                Item = New System.Web.UI.WebControls.ListItem
                Item.Text = ObjDataRow("FileName")
                Item.Value = ObjDataRow("ID")
                ddlControl.Items.Add(Item)
            End If
        Next
    End Function
    Private Function SavePart() As Boolean
        Dim ClsObjects As New Clssys_Objects(Me.Page)
        Dim ClsEmployees As New Clshrs_Employees(Me.Page)
        Dim ClsDocumentDetails As New Clssys_DocumentsInformations(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDocumentsInformations.ConnectionString)
        Dim TableName As String = Request.QueryString.Item("TB")
        Dim EmployeeID As Integer
        If TableName = "hrs_Employees" Then
            ClsEmployees = New Clshrs_Employees(Page)
            ClsEmployees.Find1("Code='" & lblDescEmployeeCode.Text & "'")
            EmployeeID = ClsEmployees.ID
        ElseIf TableName = "hrs_EmployeesDependants" Then
            ClsEmployeesDependants = New Clshrs_EmployeesDependants(Page)
            ClsEmployeesDependants.Find("ID=" & lblDescEmployeeCode.Text & "")
            EmployeeID = ClsEmployeesDependants.ID
        End If
        Dim IntId As Integer = Val(txtID.Value)
        Dim IntRecordId As Integer
        ClsObjects.Find(" Code = REPLACE('" & GetDoc_DetailsObjectId() & "',' ' ,'')")
        Dim ObjectID As Integer = ClsObjects.ID
        Dim StrRealPath As String = Request.PhysicalApplicationPath
        StrRealPath = StrRealPath + "Uploads\"
        ClsDocumentsInformations = New Clssys_DocumentsInformations(Me.Page)
        ClsDocumentAttachment = New Clssys_DocumentsInformationAttachments(Me.Page)
        ClsDocumentsInformations.Find("ID=" & Val(txtID.Value))
        If Not AssignValue(ClsDocumentsInformations) Then
            Exit Function
        End If
        If IntId > 0 Then
            If Not System.IO.Directory.Exists(StrRealPath) Then
                System.IO.Directory.CreateDirectory(Request.PhysicalApplicationPath + "Uploads\")
            End If
            If txtAttachedFile.FileName <> "" Then
                Dim arrTempFileName As String() = txtAttachedFile.FileName.Split(".")
                Dim strFileName = arrTempFileName(0) & "_" & DateTime.Now.ToString("ddMMyyyyHHmmsss") & "." & arrTempFileName(1)

                ''=================


                Dim fileName As String = txtAttachedFile.FileName
                Dim fileExtension As String = System.IO.Path.GetExtension(fileName)

                Dim allowedExtensions As String() = {
                                                    ".doc", ".docx", ".pdf", ".xls", ".xlsx",
                                                    ".jpg", ".jpeg", ".png", ".tiff",
                                                    ".ppt", ".pptx", ".txt", ".rtf"
                                                }

                If Not allowedExtensions.Any(Function(ext) _
                                                        String.Equals(ext, fileExtension, StringComparison.OrdinalIgnoreCase)) Then

                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(
                                                        Page,
                                                        ObjNavigationHandler.SetLanguage(Page,
                                                        " sorry... this The file format is invalid  /عفوا... هذا الملف غير  صالح")
                                                    )
                    Exit Function
                End If


                '===============

                If Not System.IO.File.Exists(StrRealPath & fileName) Then
                    ClsDocumentsInformations.Update(" ID=" & Val(txtID.Value), "", "")
                    ' If Not ClsDocumentAttachment.Find(" RecstrFileNameordid=" & Val(txtID.Value) & " And ObjectID= " & GetDoc_DetailsObjectId() & " And FileName = '" & txtAttachedFile.FileName & "'") Then
                    If Not ClsDocumentAttachment.Find(" ObjectID= " & GetDoc_DetailsObjectId() & " And FileName = '" & txtAttachedFile.FileName & "'") Then

                        AssignValueAttachment(ClsDocumentAttachment)
                        ClsDocumentAttachment.FileName = fileName
                        ClsDocumentAttachment.Save()
                        txtAttachedFile.SaveAs(StrRealPath & ClsDocumentAttachment.FileName)
                        AfterOperation()
                    Else
                        If ClsDocumentAttachment.Find(" Recordid=" & Val(txtID.Value) & " And ObjectID= " & GetDoc_DetailsObjectId() & " And FileName = '" & txtAttachedFile.FileName & "' And Canceldate is not null") Then
                            AssignValueAttachment(ClsDocumentAttachment)
                            ClsDocumentAttachment.FileName = txtAttachedFile.FileName
                            ClsDocumentAttachment.UpdateCanceled("Recordid=" & IntId & " And ObjectID=" & ObjectID)
                        Else
                            AssignValueAttachment(ClsDocumentAttachment)
                            ClsDocumentAttachment.FileName = DdlAttachedFiles.SelectedItem.Value
                            ClsDocumentAttachment.Update(" Recordid=" & Val(txtID.Value) & " And ObjectID= " & GetDoc_DetailsObjectId() & " And FileName='" & txtAttachedFile.FileName & "'")
                        End If
                    End If
                    Return True
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "This File Name already Existis")
                End If
            Else
                ClsDocumentsInformations.Update("ID=" & Val(txtID.Value), "", "")
                Return True
            End If
        Else
            'If txtAttachedFile.HasFile Then
            If txtAttachedFile.FileName <> "" Then


                ''=================


                Dim fileName As String = txtAttachedFile.FileName
                Dim fileExtension As String = System.IO.Path.GetExtension(fileName)

                Dim allowedExtensions As String() = {
    ".doc", ".docx", ".pdf", ".xls", ".xlsx",
    ".jpg", ".jpeg", ".png", ".tiff",
    ".ppt", ".pptx", ".txt", ".rtf"
}

                If Not allowedExtensions.Any(Function(ext) _
        String.Equals(ext, fileExtension, StringComparison.OrdinalIgnoreCase)) Then

                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(
        Page,
        ObjNavigationHandler.SetLanguage(Page,
        " sorry... this The file format is invalid  /عفوا... هذا الملف غير  صالح")
    )
                    Exit Function
                End If

                '===============
                If Not System.IO.Directory.Exists(StrRealPath) Then
                    System.IO.Directory.CreateDirectory(StrRealPath)
                End If
                'Dim arrTempFileName As String() = txtAttachedFile.FileName.Split(".")
                'Dim strFileName = arrTempFileName(0) & "_" & DateTime.Now.ToString("ddMMyyyyHHmmsss") & "." & arrTempFileName(1)
                Dim originalFileName As String = txtAttachedFile.FileName

                Dim fileNameWithoutExt As String = System.IO.Path.GetFileNameWithoutExtension(originalFileName)
                Dim extension As String = System.IO.Path.GetExtension(originalFileName)

                Dim newFileName As String =
    fileNameWithoutExt & "_" &
    DateTime.Now.ToString("ddMMyyyyHHmmssfff") &
    extension

                If Not System.IO.File.Exists(StrRealPath & newFileName) Then
                    IntRecordId = ClsDocumentsInformations.Save()
                    txtID.Value = IntRecordId
                    If txtAttachedFile.FileName <> "" Then
                        If Not ClsDocumentAttachment.Find("Recordid=" & IntRecordId & " And ObjectID=" & ObjectID & " And IsImageView Is Null ") Then
                            AssignValueAttachment(ClsDocumentAttachment)
                            ClsDocumentAttachment.RecordId = IntRecordId
                            ClsDocumentAttachment.FileName = newFileName
                            ClsDocumentAttachment.Save()
                            txtAttachedFile.SaveAs(StrRealPath & ClsDocumentAttachment.FileName)
                            AfterOperation()
                        Else
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Can not Add Document more than one time")
                        End If
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "This File Name already Existis")
                    Return False
                End If
            Else
                IntRecordId = ClsDocumentsInformations.Save()
                txtID.Value = IntRecordId
            End If
        End If
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        ClsDocumentsInformations.Find("ID=" & Val(txtID.Value))
        If ClsDocumentsInformations.ID > 0 Then
            clsMainOtherFields.CollectDataAndSave(value.Text, ClsDocumentsInformations.Table, ClsDocumentsInformations.ID)
            value.Text = ""
        End If
        Return True
    End Function

    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        ClsDocumentsInformations = New Clssys_DocumentsInformations(Me.Page)
        Try
            With ClsDocumentsInformations
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, "UltraWebTab1")
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = True
                    ddlDocumentType.Focus()
                End If
            End With
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsDocumentsInformations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsDocumentsInformations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function AfterOperation() As Boolean
        Load_Data_Grid(Val(txtObjectId.Value), Val(txtEmpID.Value))
        Clear()
        ClsDocumentsInformations.Clear()
        Dim DocNo = Request.QueryString("DN")
        If Convert.ToString(DocNo) <> "0" Or Convert.ToString(DocNo) <> "" Then
            'txtDocumentNumber0.Text = DocNo
            SetDocumentAutoSerial()

            txtDocumentNumber_TextChanged(Nothing, Nothing)
        End If
    End Function
    Private Function AssignValueAttachment(ByRef ClsDocumentAttachment As Clssys_DocumentsInformationAttachments) As Boolean
        ClsDocumentAttachment.DbObject = GetDoc_DetailsObjectId()
        ClsDocumentAttachment.RecordId = Val(txtID.Value)
        ClsDocumentAttachment.FolderName = "Uploads"
        ClsDocumentAttachment.FileName = txtAttachedFile.FileName
        ClsDocumentAttachment.EngName = txtAttachedFile.FileName.ToString.Substring(0, txtAttachedFile.FileName.ToString.IndexOf(".") - 1)
        ClsDocumentAttachment.ArbName = txtAttachedFile.FileName.ToString.Substring(0, txtAttachedFile.FileName.ToString.IndexOf(".") - 1)
        Return True
    End Function
    Private Function Clear() As Boolean
        txtID.Value = ""
        txtDocumentID.Value = ""
        txtDocumentArbName.Text = ""
        txtDocumentEngName.Text = ""
        txtDocumentNumber0.Text = ""
        txtReferenceNumber.Text = ""
        txtExpiryDate.Value = Nothing
        txtIssueDate.Value = Nothing
        txtLastRenewalDate.Value = Nothing
        txtExpiryDateH.Value = Nothing
        txtIssueDateH.Value = Nothing
        txtLastRenewalDateH.Value = Nothing
        ddlDocumentType.Enabled = True
        txtDocumentNumber0.Focus()
        DdlIssuedCity.SelectedIndex = 0
        lnkDownload.NavigateUrl = ""
        btnDeleteAttachment.Enabled = False
        DdlAttachedFiles.Items.Clear()

    End Function
    Private Function ClearControls() As Boolean
        txtID.Value = ""
        txtDocumentID.Value = ""
        txtDocumentArbName.Text = ""
        txtDocumentEngName.Text = ""
        txtExpiryDate.Value = Nothing
        txtIssueDate.Value = Nothing
        txtLastRenewalDate.Value = Nothing
        ddlDocumentType.Enabled = True
        txtDocumentNumber0.Focus()
        DdlIssuedCity.SelectedIndex = 0
        lnkDownload.NavigateUrl = ""
        btnDeleteAttachment.Enabled = False
        DdlAttachedFiles.Items.Clear()
        SetDocumentAutoSerial()

    End Function
    Private Function Load_Data_Grid(ByVal IntObjectId As Integer, ByVal IntRecordId As Integer) As Integer
        Dim ObjClsDocumentDetails As New Clssys_DocumentsInformations(Me.Page)
        Dim ClsDocumentType As New Clssys_DocumentsTypes(Me.Page)

        Dim criteria As String = ""
        Dim criteriaType As String = ""
        If ddlDocumentTypesGroup.SelectedIndex > 0 Then
            criteria = " And DocumentID In (Select ID From sys_Documents Where DocumentTypesGroupId=" & ddlDocumentTypesGroup.SelectedValue & ")"
            criteriaType = " isnull(DocumentTypesGroupId,0)=" & ddlDocumentTypesGroup.SelectedValue
        End If
        'تأكد من تحميل قائمة الأنواع أولاً
        ClsDocumentType.GetList(UwgEmployeeDocuments.Columns(1).ValueList, True, criteriaType)

        'تحميل البيانات مع التأكد من عدم وجود قيم عشرية
        ObjClsDocumentDetails.Find("ObjectId = " & IntObjectId & " And RecordID=" & IntRecordId & " And CancelDate Is Null " & criteria)

        'تأكد من أن مصدر البيانات صالح قبل الربط
        If ObjClsDocumentDetails.DataSet IsNot Nothing AndAlso ObjClsDocumentDetails.DataSet.Tables.Count > 0 Then
            'تحويل أي قيم عشرية في معرّفات الصفوف إلى نصوص
            For Each row As DataRow In ObjClsDocumentDetails.DataSet.Tables(0).Rows
                If row.Table.Columns.Contains("ID") Then
                    row("ID") = row("ID").ToString()
                End If
            Next

            UwgEmployeeDocuments.DataSource = ObjClsDocumentDetails.DataSet.Tables(0)
            UwgEmployeeDocuments.DataBind()
        End If
    End Function
    Private Function GetDoc_DetailsObjectId() As Object
        Dim ClsDocumentsDetails As New Clssys_DocumentsInformations(Me.Page)
        Dim clsObjects As New Clssys_Objects(Me.Page)
        clsObjects.Find(" Code = REPLACE('" & ClsDocumentsDetails.Table.Trim() & "',' ' ,'')")
        Return clsObjects.ID
    End Function
    Private Sub New_Part()
        Clear()
        ddlDocumentTypesGroup.SelectedIndex = 0
        ddlDocumentType.Enabled = True
        SetToolBarPermission(Me, ClsDocumentsInformations.ConnectionString, ClsDocumentsInformations.DataBaseUserRelatedID, ClsDocumentsInformations.GroupID, "N")
        AfterOperation()
    End Sub
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
                    Select Case Mode
                        Case "N", "R"
                            ImageButton_New.Enabled = .Item("AllowAdd")

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

                    If ImageButton_Delete.Enabled = True And .Item("CanDelete") = True Then
                        ImageButton_Delete.Enabled = Not .Item("CanDelete")
                    End If
                End With
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsDocumentsInformations.Table) = True Then
            Dim StrTablename As String
            ClsDocumentsInformations = New Clssys_DocumentsInformations(Me)
            StrTablename = ClsDocumentsInformations.Table
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

    Private Function SetDocumentAutoSerial()

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDocumentsInformations.ConnectionString)
        Dim AutoSerial As Boolean = False
        ClsCompanies = New Clssys_Companies(Page)
        ClsCompanies.Find("ID = " & ClsCompanies.MainCompanyID)
        AutoSerial = ClsCompanies.EmployeeDocumentsAutoSerial
        If AutoSerial Then

            Dim ConnString As String
            ConnString = ConfigurationManager.AppSettings("Connstring").ToString()

            Dim strselec As String
            strselec = "SELECT        MAX(CONVERT(bigint, DocumentNumber)) AS DocumentNumber FROM            sys_DocumentsDetails"
            Dim DsFirstLevel As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, CommandType.Text, strselec)
            If DsFirstLevel.Tables(0).Rows.Count > 0 Then
                With DsFirstLevel.Tables(0).Rows(0)
                    Dim serial As Int32 = Convert.ToInt32(.Item("DocumentNumber")) + 1
                    txtDocumentNumber0.Text = serial.ToString()
                    txtDocumentNumber0.Enabled = False
                End With

            Else
                txtDocumentNumber0.Text = "1"
                txtDocumentNumber0.Enabled = False

            End If
            DdlIssuedCity.SelectedValue = 3
        Else
            txtDocumentNumber0.Enabled = True
        End If
    End Function


#End Region

End Class
