Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesDependants
    Inherits MainPage

#Region "Public Decleration"

    Private ClsEmployees As Clshrs_Employees
    Private ClsEmployeesDependants As Clshrs_EmployeesDependants

    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsEmployees As New Clshrs_Employees(Me.Page)
        Dim IntRecordId As Integer
        If ClsEmployees.Find("Code='" & lblDescEmployeeCode.Text & "'") Then
            IntRecordId = ClsEmployees.ID
        Else
            IntRecordId = Request.QueryString("EmpID")
        End If
        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim ClsCities As New Clssys_Cities(Me.Page)
        Dim ClsNationality As New Clssys_Nationality(Me.Page)
        Dim ClsDependantType As New ClsBasicFiles(Me.Page, "hrs_DependantsTypes")
        Dim Clsobjects As New Clssys_Objects(Me.Page)
        Dim ClsDependant As New Clshrs_EmployeesDependants(Me.Page)
        Dim IntObjectId As Integer
        ClsEmployeesDependants = New Clshrs_EmployeesDependants(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesDependants.ConnectionString)
        Try
            Clsobjects.Find(" Code = REPLACE('" & ClsEmployeesDependants.Table.Trim() & "',' ' ,'')")
            IntObjectId = Clsobjects.ID
            txtObjectId.Value = Clsobjects.ID
            txtEmpID.Value = IntRecordId
            txtRecordID.Value = IntRecordId
            Dim ClsSearchs As New Clssys_Searchs(Me.Page)
            If Clsobjects.Find(" Code='" & ClsEmployees.Table.Trim() & "'") Then
                If ClsSearchs.Find(" ObjectID=" & Clsobjects.ID) Then
                    Dim csSearchID As Integer = ClsSearchs.ID
                    lblDescEmployeeCode.Attributes.Add("onKeyUp", "Open_Search_KeyDown(" & " " & csSearchID & " " & ",""lblDescEmployeeCode"")")
                End If
            End If
            ClsEmployees.Find("ID=" & IntRecordId)
            lblDescEmployeeCode.Text = ClsEmployees.Code
            lblDescEnglishName.Text = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployeesDependants.ConnectionString, CommandType.Text, "Select dbo.fn_GetEmpName('" & ClsEmployees.Code & "'," & ObjNavigationHandler.SetLanguage(Page, "0/1") & ")")  'ClsEmployee.Name
            ImgDependantImage.Attributes.Add("onclick", "DisplayImageScreen('" & IntObjectId & "')")
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
                '================================= Exit & Navigation Notification [ End ]
                ClsDependantType.GetDropDownList(DdlDependantTypeID, False)
                ClsNationality.GetDropDownList(DdlNationalityId, True)
                ClsCities.GetDropDownList(DdlBirthCity2, True)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                GetFormPermission("frmEmployeesDependants")
                New_Part()
            End If
            Dim IntrecID As Integer
            If txtID.Value <> "" Then
                ClsEmployeesDependants.Find(" ID=" & Val(txtID.Value))
                IntrecID = ClsEmployeesDependants.ID
                If (IntrecID > 0) Then
                    SetScreenInformation("E")
                Else
                    SetScreenInformation("N")
                End If
            Else
                SetScreenInformation("N")
            End If
            uwgDependents.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect
            uwgDependents.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            uwgDependents.DisplayLayout.AllowAddNewDefault = Infragistics.WebUI.UltraWebGrid.AllowAddNew.No
            uwgDependents.BorderWidth = 0
            CreateOtherFields(IntRecordId)
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesDependants.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesDependants.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_PopupCommand(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Delete.Command
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Select Case e.CommandArgument
            Case "Save"
                If SavePart() Then
                    AfterOperation()
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                End If
            Case "Delete"
                If txtID.Value <> String.Empty Then
                    ClsEmployeesDependants.Delete("ID=" & Val(txtID.Value))
                    AfterOperation()
                End If
            Case "New"
                New_Part()
        End Select
    End Sub
    Protected Sub UwgEmployeeDocuments_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgDependents.ActiveRowChange
        txtID.Value = e.Row.Cells.FromKey("ID").Value
        txtDocumentNumber_TextChanged()
    End Sub
    Protected Sub txtDocumentNumber_TextChanged()
        ClsEmployeesDependants = New Clshrs_EmployeesDependants(Page)
        Dim ClsDependant As New Clshrs_EmployeesDependants(Me.Page)
        Dim ClsUser As New Clssys_Users(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Me.Page)
        If (txtID.Value > 0) Then
            If ClsEmployeesDependants.Find(" ID =" & txtID.Value) Then
                ImgDependantImage.ImageUrl = ""
                With ClsEmployeesDependants
                    txtID.Value = .ID
                    ClsObjects.Find(" Code = REPLACE('" & ClsEmployeesDependants.Table & "',' ' ,'')")
                    txtEngName.Text = .EngName
                    txtArbName.Text = .ArbName
                    txtBirthDate.Value = Nothing
                    txtBirthDateH.Value = Nothing
                    If .BirthDate <> Nothing Then
                        txtBirthDate.Value = ClsDataAcessLayer.FormatGreg(.BirthDate, "dd/MM/yyyy").ToString("ddMMyyyy")
                        txtBirthDateH.Value = ClsDataAcessLayer.GregToHijri(.BirthDate, "ddMMyyyy")
                    End If
                    DdlDependantTypeID.SelectedValue = .DependantTypeID
                    DdlBirthCity2.SelectedValue = .BirthCityID
                    DdlNationalityId.SelectedValue = .NationalityID
                    If .Sex = "F" Then
                        DdlSex.SelectedIndex = 1
                    Else
                        DdlSex.SelectedIndex = 0
                    End If
                    DdlInsuranceCovered.SelectedValue = IIf(.InsuranceCovered, 1, 0)
                    DdlTicketCovered.SelectedValue = IIf(.TicketCovered, 1, 0)
                    txtPercentageInsurance.Value = .InsurancePercentage
                    txtPercentageTicket.Value = .TicketPercentage
                    TxtNationalIDORIqamaNo.Text = .NationalIDORIqamano
                    DisplayImage(.ID, ClsObjects.ID, ImgDependantImage)
                End With
                ImageButton_Delete.Enabled = True
            Else
                If txtID.Value.ToString() = "" Then
                    Clear()
                End If
            End If
        End If
    End Sub
    Public Function DisplayImage(ByVal IntRecordId As Integer, ByVal IntObjectId As Integer, ByRef ImgImageControl As System.Web.UI.WebControls.Image) As String
        Dim ClsDocumentAttachment = New Clssys_DocumentsInformationAttachments(Me.Page)
        If ClsDocumentAttachment.Find(" RecordID=" & IntRecordId & " And ObjectID=" & IntObjectId & " And IsImageView is not Null And IsNull(CancelDate , '') = '' Order By IsProfilePicture Desc ") Then
            If ClsDocumentAttachment.ExpiryDate > Date.Now Or ClsDocumentAttachment.ExpiryDate = Nothing Then
                ImgImageControl.ImageUrl = "../Photos/" & IntObjectId.ToString & "_" & IntRecordId.ToString & "/" + ClsDocumentAttachment.FileName
            Else
                ImgImageControl.ImageUrl = "./NoImage.jpg"
            End If
        Else
            ImgImageControl.ImageUrl = "./NoImage.jpg"
        End If
    End Function
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
    Private Function AssignValue(ByRef ClsEmployeesDependants As Clshrs_EmployeesDependants) As Boolean
        ClsEmployees = New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & lblDescEmployeeCode.Text & "'")
        Dim IntSendValue As Integer = ClsEmployees.ID
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesDependants.ConnectionString)
        Try
            With ClsEmployeesDependants
                .EmployeeID = IntSendValue
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .DependantTypeID = DdlDependantTypeID.SelectedValue
                .BirthDate = SetDate(txtBirthDate.Text, txtBirthDateH.Text)
                .BirthCityID = DdlBirthCity2.SelectedValue
                .Sex = DdlSex.SelectedValue
                .NationalityID = DdlNationalityId.SelectedValue
                .InsuranceCovered = IIf(DdlInsuranceCovered.SelectedValue = 1, True, False)
                .InsurancePercentage = txtPercentageInsurance.Value
                .TicketCovered = IIf(DdlTicketCovered.SelectedValue = 1, True, False)
                .TicketPercentage = txtPercentageTicket.Value
                .NationalIDORIqamano = TxtNationalIDORIqamaNo.Text
            End With
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
    Private Function SavePart() As Boolean
        Dim IntId As Integer = Val(txtID.Value)
        ClsEmployeesDependants = New Clshrs_EmployeesDependants(Me.Page)
        ClsEmployeesDependants.Find("ID=" & Val(txtID.Value))
        If Not AssignValue(ClsEmployeesDependants) Then
            Exit Function
        End If
        If IntId > 0 Then
            ClsEmployeesDependants.Update("ID=" & Val(txtID.Value))
        Else
            ClsEmployeesDependants.Save()
            txtID.Value = ClsEmployeesDependants.ID
        End If
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        ClsEmployeesDependants.Find("ID=" & Val(txtID.Value))
        If ClsEmployeesDependants.ID > 0 Then
            clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployeesDependants.Table, ClsEmployeesDependants.ID)
            value.Text = ""
        End If
        Return True
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        ClsEmployeesDependants = New Clshrs_EmployeesDependants(Me.Page)
        Try
            With ClsEmployeesDependants
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, "UltraWebTab1")
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = True
                    txtEngName.Focus()
                End If
            End With
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesDependants.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesDependants.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function AfterOperation() As Boolean
        Load_Data_Grid(Val(txtObjectId.Value), Val(txtEmpID.Value))
        Clear()
        ClsEmployeesDependants.Clear()
    End Function
    Private Function Clear() As Boolean
        txtID.Value = String.Empty
        txtArbName.Text = String.Empty
        txtEngName.Text = String.Empty
        DdlSex.SelectedIndex = 0
        DdlDependantTypeID.SelectedIndex = 0
        DdlNationalityId.SelectedIndex = 0
        DdlBirthCity2.SelectedIndex = 0
        txtBirthDate.Value = Nothing
        txtBirthDateH.Value = Nothing
        DdlInsuranceCovered.SelectedIndex = 0
        DdlTicketCovered.SelectedIndex = 0
        txtPercentageInsurance.Value = 0
        txtPercentageTicket.Value = 0
        ImgDependantImage.ImageUrl = "./NoImage.jpg"
        TxtNationalIDORIqamaNo.Text = String.Empty

    End Function
    Private Function Load_Data_Grid(ByVal IntObjectId As Integer, ByVal IntRecordId As Integer) As Integer
        Dim dsDependants As New Data.DataSet
        ClsEmployees = New Clshrs_Employees(Me.Page)
        ClsEmployees.GetEmployeesDependantsData(IntRecordId, IntObjectId, dsDependants)
        uwgDependents.DataSource = Nothing
        uwgDependents.DataBind()
        uwgDependents.DataSource = dsDependants.Tables(0).DefaultView
        uwgDependents.DataBind()
    End Function
    Private Sub New_Part()
        Clear()
        SetToolBarPermission(Me, ClsEmployeesDependants.ConnectionString, ClsEmployeesDependants.DataBaseUserRelatedID, ClsEmployeesDependants.GroupID, "N")
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployeesDependants.Table) = True Then
            Dim StrTablename As String
            ClsEmployeesDependants = New Clshrs_EmployeesDependants(Me)
            StrTablename = ClsEmployeesDependants.Table
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

End Class
