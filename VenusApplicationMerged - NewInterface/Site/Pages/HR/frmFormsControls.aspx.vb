Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmFormsControls
    Inherits MainPage

#Region "Public Decleration"
    Private ClsFormsControls As Clssys_FormsControls
    Private clsMainOtherFields As clsSys_MainOtherFields

    Const CSave = 1
    Const CID = 0
    Const CName = 1
    Const CField = 2
    Const CEngDesc = 3
    Const CArbDesc = 4
    Const CCompuslary = 5
    Const CIsArabic = 6
    Const CFormat = 7
    Const CArabicFormat = 8
    Const CTooltip = 9
    Const CArabicTooltip = 10
    Const CMaxLenght = 11
    Const CIsNumeric = 12
    Const CIsHide = 13
    Const CFocusOnStart = 14
    Const CRank = 15
    Const CMinValue = 16
    Const CMaxValue = 17
    Const CSearchID = 18
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsForms As New ClsSys_Forms(Me.Page)
        Dim ClsObject As New Clssys_Objects(Me.Page)
        Try
            SetScreenInformation()
            If Not IsPostBack Then
                ClsForms.GetDropDownList(ddlForms, True)
                ClsObject.GetDropDownList(ddlObjects, True)
            End If
            UwgFormControls.BorderWidth = 0
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsFormsControls.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command
        Dim ClsForms As New ClsSys_Forms(Me.Page)
        Dim ClsObject As New Clssys_Objects(Me.Page)
        Dim ClsField As New Clssys_Fields(Me.Page)
        ClsFormsControls = New Clssys_FormsControls(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsFormsControls.ConnectionString)
        Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
        Try

            Select Case e.CommandArgument
                Case "Save"
                    If ddlForms.SelectedIndex = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Form /برجاء إختيار الشاشة"))
                        Exit Sub
                    End If
                    Dim dtValidControls As New Infragistics.WebUI.UltraWebGrid.UltraWebGrid()

                    '---------------------------
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgFormControls.Rows
                        dtValidControls.Rows.Add(row)
                    Next
                    '---------------------------
                    If dtValidControls.Rows.Count > 0 Then
                        ClsFormsControls.SaveFormsControls(dtValidControls, ddlForms.SelectedValue, True)
                    End If

                    UwgFormControls.DataSource = Nothing
                    UwgFormControls.DataBind()
                    ddlForms.SelectedIndex = 0
                    ddlObjects.SelectedIndex = 0
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))

            End Select
        Catch ex As Exception
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsFormsControls.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ddlForms_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlForms.SelectedIndexChanged
        SetObject(ddlForms.SelectedValue)
    End Sub
    Protected Sub ddlObjects_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlObjects.SelectedIndexChanged
        LoadFields(ddlObjects.SelectedValue)
    End Sub
    Protected Sub WebImageButton1_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles WebImageButton1.Click
        Dim ClsObject As New Clssys_Objects(Me.Page)
        Dim ClsForms As New ClsSys_Forms(Me.Page)
        ClsFormsControls = New Clssys_FormsControls(Me.Page)
        Dim ObjMenuHandler As New Venus.Shared.Web.NavigationHandler(ClsObject.ConnectionString)
        Dim intFormId As Integer = Val(ddlForms.SelectedValue)
        Dim TempDT As New Data.DataTable()
        Dim strResource As String = String.Empty
        Dim objDT As New Data.DataTable()
        Try
            '-----------------------
            ClsFormsControls.Find(" FormID = " & intFormId)
            If (ClsFormsControls.ID > 0) Then
                TempDT = ClsFormsControls.DataSet.Tables(0)
            End If
            '-----------------------
            ClsForms.Find(" ID = " & intFormId)
            Dim strScreenPath As String = Request.PhysicalApplicationPath & "Pages\HR\" & ClsForms.EngName
            If System.IO.File.Exists(strScreenPath) = True Then
                '******
                Dim Stream As New System.IO.StreamReader(strScreenPath)
                strResource = Stream.ReadToEnd()
                Stream.Close()
                objDT = ClsFormsControls.GetParameters(ClsFormsControls.ParsText(strResource))
                TempDT = ValidateGrid(TempDT, objDT)
                '******
                If TempDT.Rows.Count > 0 Then
                    Dim tempObjRow As Data.DataRow
                    Dim ObjRow As Data.DataRow
                    If objDT.Rows.Count > 0 Then
                        For Each ObjRow In objDT.Rows
                            If Not IsDBNull(ObjRow.Item("Name")) Then

                                If Not ClsFormsControls.Find(" FormID = " & intFormId & " And Name = '" & ObjRow.Item("Name") & "'") Then
                                    tempObjRow = TempDT.NewRow()

                                    tempObjRow.Item("Name") = ObjRow.Item("Name")
                                    tempObjRow.Item("Rank") = 0

                                    tempObjRow.Item("SearchID") = 0
                                    tempObjRow.Item("FieldID") = 0

                                    tempObjRow.Item("RegComputerID") = ClsFormsControls.RegComputerID
                                    tempObjRow.Item("RegUserID") = ClsFormsControls.RegUserID

                                    TempDT.Rows.Add(tempObjRow)
                                End If
                            End If
                        Next
                        UwgFormControls.DataSource = TempDT.DefaultView
                        UwgFormControls.DataBind()
                    End If
                Else
                    If objDT.Rows.Count > 0 Then
                        UwgFormControls.DataSource = objDT.DefaultView
                        UwgFormControls.DataBind()
                    Else
                        UwgFormControls.Rows.Clear()
                    End If
                End If
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsFormsControls.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
#End Region

#Region "Private Functions"

    Private Sub SetObject(ByVal formID As Integer)
        Dim ClsObject As New Clssys_Objects(Me.Page)
        Dim ClsForms As New ClsSys_Forms(Me.Page)
        ClsFormsControls = New Clssys_FormsControls(Me.Page)
        Dim ObjMenuHandler As New Venus.Shared.Web.NavigationHandler(ClsObject.ConnectionString)
        Try
            ObjMenuHandler.Find(" FormID=" & IIf(IsNothing(formID), "0", formID))
            If (ObjMenuHandler.ID > 0) Then
                If ddlObjects.Items.FindByValue(ObjMenuHandler.ObjectID) IsNot Nothing Then
                    ddlObjects.SelectedValue = ObjMenuHandler.ObjectID
                    LoadFields(ObjMenuHandler.ObjectID)
                Else
                    ddlObjects.SelectedValue = 0
                    LoadFields(0)
                End If
            Else
                ddlObjects.SelectedValue = 0
                LoadFields(0)
            End If
            If (Not IsNothing(formID)) And formID > 0 Then

                ClsFormsControls.Find(" FormID = " & formID)
                If (ClsFormsControls.ID > 0) Then
                    UwgFormControls.DataSource = ClsFormsControls.DataSet.Tables(0).DefaultView
                    UwgFormControls.DataBind()
                Else
                    UwgFormControls.Rows.Clear()
                End If
            Else
                UwgFormControls.Rows.Clear()
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsFormsControls.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Private Sub LoadFields(ByVal objectID As Integer)
        UwgFormControls.Columns(CField).ValueList.ValueListItems.Clear()
        Dim ClsField As New Clssys_Fields(Me.Page)
        Dim ClsSearch As New Clssys_Searchs(Me.Page)
        If (objectID > 0) Then
            ClsField.GetList(UwgFormControls.Columns(CField).ValueList, True, " ObjectID=" & objectID)
            ClsSearch.GetList(UwgFormControls.Columns(CSearchID).ValueList, True, "ID > 0 ")
        End If
    End Sub
    Private Function ValidateGrid(ByVal TempDT As Data.DataTable, ByVal AspxTable As Data.DataTable) As Data.DataTable
        ClsFormsControls = New Clssys_FormsControls(Me.Page)
        Dim AspxObjRow As Data.DataRow
        Dim TempDBTable As New Data.DataTable()
        Dim strControlName As String = String.Empty
        Dim row As Data.DataRow
        Dim bExsit As Boolean = False
        Dim strIndex As String = ""
        Dim strIndices As String = ""
        Dim intIndex As Integer = 0
        Dim arrIndices As String()
        Dim strSQL As String = String.Empty
        Try
            If TempDT.Rows.Count > 0 Then
                For Each row In TempDT.Rows
                    strControlName = row.Item("Name")
                    If AspxTable.Rows.Count > 0 Then
                        For Each AspxObjRow In AspxTable.Rows
                            If (AspxObjRow.Item("Name") = strControlName) Then
                                strIndices &= row.Item("Name") & "_"
                            End If
                        Next
                        intIndex += 1
                    End If
                Next
                arrIndices = strIndices.Split("_")
                For Each strIndex In arrIndices
                    If Not strIndex = "" Then
                        strSQL &= "Name = '" & strIndex & "' Or "
                    End If
                Next
            End If
            If Not (strSQL = "") Then
                Dim index As Integer = strSQL.LastIndexOf("Or")
                strSQL = strSQL.Remove(index)
                ClsFormsControls.Find(" FormID = " & Val(ddlForms.SelectedValue) & " And ( " & strSQL & ")")
                TempDT = ClsFormsControls.DataSet.Tables(0)
            Else
                TempDT.Rows.Clear()
            End If
            Return TempDT
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsFormsControls.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
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
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsFormsControls = New Clssys_FormsControls(Me)
        Try
            With ClsFormsControls
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
#End Region

End Class
