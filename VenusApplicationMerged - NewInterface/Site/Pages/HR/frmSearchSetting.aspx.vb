Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmSearchSetting
    Inherits MainPage

#Region "Constant"
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const uwgCheck = 0
    Const uwgName = 1
    Const uwgLength = 2
    Const uwgType = 3
    Const uwgNavigationTable = 1
    Const TbrSave = 1
    Const InitialValue = 0
    Const uwgObjectID = 2
    Const uwgFieldID = 3
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsSearchSetting As New Clssys_Searchs(Page)
        Dim ClsBase As New ClsDataAcessLayer(Page)
        Dim ClsSecurityHandler As New Venus.Shared.Security.SQLSecurityHandler(ClsBase.ConnectionString)
        Dim ObjDs As New Data.DataSet
        Dim Arr(3) As String
        Arr(0) = "txtCode"
        Arr(1) = "txtEngName"
        Arr(2) = "txtArbName"
        Try
            If Not IsPostBack Then
                uwgNavigation.DataSource = ClsSearchSetting.fnGetTables().Tables(0)
                uwgNavigation.DataBind()
                ImageButton_Save.Enabled = False
            End If
            uwgcriteria.BorderWidth = 0
            uwgNavigation.BorderWidth = 0
            UwgResult.BorderWidth = 0
            Venus.Shared.Web.ClientSideActions.GridSelection(Page, uwgcriteria)
            Venus.Shared.Web.ClientSideActions.GridSelection(Page, UwgResult)
            Venus.Shared.Web.ClientSideActions.SetupTabOrder(Page, Arr, Drawing.Color.White, False)
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSearchSetting.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSearchSetting.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Refresh.Command, ImageButton_Save.Command
        Dim ClsSearchSetting As New Clssys_Searchs(Page)
        Dim ClsBase As New ClsDataAcessLayer(Page)
        Dim ClsColumn As New Clssys_SearchsColumns(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(Clsbase.ConnectionString)
        Select Case e.CommandArgument
            Case "Save"
                With ClsSearchSetting
                    .Find("Code='" & txtCode.Text & "'")
                    If .ID > InitialValue Then
                        .Code = txtCode.Text
                        .EngName = txtEngName.Text
                        .ArbName = txtArbName.Text
                        If .Update("Code='" & txtCode.Text & "'") Then
                            .Find("Code='" & txtCode.Text & "'")
                            If fnUpdateColumns(.ID, Clsbase.ConnectionString) Then
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                            End If
                        End If
                    Else
                        .Code = txtCode.Text
                        .EngName = txtEngName.Text
                        .ArbName = txtArbName.Text

                        .ObjectID = uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value
                        If .Save() Then
                            .Find("Code='" & txtCode.Text & "'")
                            If SaveColumns(.ID, Clsbase.ConnectionString) Then
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                            End If
                        End If
                    End If
                End With

                AfterOperation()
            Case "Refresh"
                AfterOperation()
        End Select
    End Sub
    Protected Sub uwgNavigation_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgNavigation.ActiveRowChange
        CheckView(e.Row.Cells(uwgNavigationTable).Value, e.Row.Cells(uwgObjectID).Value)
    End Sub
#End Region

#Region "Private Function"
    Private Function CheckView(ByVal Code As String, Optional ByVal objectid As Integer = 0) As Boolean
        Dim ClsBase As New ClsDataAcessLayer(Page)
        Dim ClsSecurityHandler As New Venus.Shared.Security.SQLSecurityHandler(ClsBase.ConnectionString)
        Dim ClsSearchSetting As New Clssys_Searchs(Page)
        Dim ClsColumn As New Clssys_SearchsColumns(Page)
        Dim ObjDs As New Data.DataSet
        Try
            ClsSearchSetting.Find("Code='" & Code & "'")
            If ClsSearchSetting.ID > InitialValue Then
                txtCode.Text = ClsSearchSetting.Code
                txtArbName.Text = ClsSearchSetting.ArbName
                txtEngName.Text = ClsSearchSetting.EngName
                lblUsed.Text = String.Empty
                lblUsed.Visible = True
            Else
                txtCode.Text = Code
                txtEngName.Text = Code
                txtArbName.Text = Code
                lblUsed.Visible = False
            End If
            ObjDs = ClsColumn.fnGetColumns(objectid)
            uwgcriteria.DataSource = ObjDs.Tables(0).DefaultView
            uwgcriteria.DataBind()
            UwgResult.DataSource = ObjDs.Tables(0).DefaultView
            UwgResult.DataBind()
            ImageButton_Save.Enabled = True
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSearchSetting.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSearchSetting.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SaveColumns(ByVal SearchID As Integer, ByVal ConnectionString As String) As Boolean
        Dim ObjDataRow As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Try
            For Each ObjDataRow In uwgcriteria.Rows
                Dim ClsColumn As New Clssys_SearchsColumns(Page)
                With ClsColumn
                    .FieldID = .fngetFieldID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, ObjDataRow.Cells(uwgName).Value)
                    .IsArabic = False
                    .EngName = ""
                    .ArbName = ""
                    .Alignment = 0
                    .IsCriteria = ObjDataRow.Cells(uwgCheck).Value
                    .SearchID = SearchID
                    .SubSearchID = .fnGetSubSearchID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, .FieldID)
                    If .SubSearchID = 0 Then .SubSearchID = Nothing
                    .Save()
                End With
            Next

            ObjDataRow = New Infragistics.WebUI.UltraWebGrid.UltraGridRow

            For Each ObjDataRow In UwgResult.Rows
                Dim ClsColumn As New Clssys_SearchsColumns(Page)
                With ClsColumn
                    .FieldID = .fngetFieldID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, ObjDataRow.Cells(uwgName).Value)
                    .IsArabic = False
                    .EngName = ""
                    .ArbName = ""
                    .Alignment = 0
                    .IsView = ObjDataRow.Cells(uwgCheck).Value
                    .SearchID = SearchID
                    .IsCriteria = .fnGetIsCriteria(.FieldID, .SearchID)
                    .SubSearchID = .fnGetSubSearchID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, .FieldID)
                    .fnUpdate(" sys_SearchsColumns.SearchID =  " & .SearchID & " And sys_SearchsColumns.FieldID =  " & .FieldID)
                End With
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function fnUpdateColumns(ByVal SearchID As Integer, ByVal ConnectionString As String) As Boolean
        Dim ObjDataRow As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Try
            For Each ObjDataRow In uwgcriteria.Rows
                Dim ClsColumn As New Clssys_SearchsColumns(Page)
                With ClsColumn
                    .Find(" sys_SearchsColumns.SearchID =  " & SearchID & " And sys_SearchsColumns.FieldID =  " & .fngetFieldID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, ObjDataRow.Cells(uwgName).Value))
                    .FieldID = .fngetFieldID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, ObjDataRow.Cells(uwgName).Value)
                    .IsArabic = False
                    .Alignment = 0
                    .IsCriteria = ObjDataRow.Cells(uwgCheck).Value
                    .IsView = ObjDataRow.Cells(4).Value
                    .SearchID = SearchID
                    .SubSearchID = .fnGetSubSearchID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, .FieldID)
                    If .SubSearchID = 0 Then .SubSearchID = Nothing

                    Dim ClsColumn1 As New Clssys_SearchsColumns(Page)
                    If ClsColumn1.Find("sys_SearchsColumns.SearchID =  " & .SearchID & " And sys_SearchsColumns.FieldID =  " & .FieldID) Then
                        .fnUpdate(" sys_SearchsColumns.SearchID =  " & .SearchID & " And sys_SearchsColumns.FieldID =  " & .FieldID)
                    Else
                        .EngName = ""
                        .ArbName = ""
                        .Save()
                    End If
                End With
            Next
            ObjDataRow = New Infragistics.WebUI.UltraWebGrid.UltraGridRow
            For Each ObjDataRow In UwgResult.Rows
                Dim ClsColumn As New Clssys_SearchsColumns(Page)
                With ClsColumn
                    .Find("sys_SearchsColumns.SearchID =  " & SearchID & " And sys_SearchsColumns.FieldID =  " & .fngetFieldID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, ObjDataRow.Cells(uwgName).Value))
                    .FieldID = .fngetFieldID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, ObjDataRow.Cells(uwgName).Value)
                    .IsArabic = 0
                    .Alignment = 0
                    .IsView = ObjDataRow.Cells(uwgCheck).Value
                    .SearchID = SearchID
                    .IsCriteria = .fnGetIsCriteria(.FieldID, .SearchID)
                    .SubSearchID = .fnGetSubSearchID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, .FieldID)
                    If .SubSearchID = 0 Then .SubSearchID = Nothing
                    .fnUpdate(" sys_SearchsColumns.SearchID =  " & .SearchID & " And sys_SearchsColumns.FieldID =  " & .FieldID)
                End With
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AfterOperation() As Boolean
        Dim ClsSearchSetting As New Clssys_Searchs(Page)
        Try
            uwgNavigation.DataSource = ClsSearchSetting.fnGetTables().Tables(0)
            uwgNavigation.DataBind()
            ImageButton_Save.Enabled = False
            uwgNavigation.DisplayLayout.SelectedRows.Clear()
            uwgNavigation.DisplayLayout.ActiveRow = Nothing
            txtArbName.Text = String.Empty
            txtCode.Text = String.Empty
            txtEngName.Text = String.Empty
            lblUsed.Text = String.Empty

            uwgcriteria.DataSource = Nothing
            uwgcriteria.DataBind()

            UwgResult.DataSource = Nothing
            UwgResult.DataBind()

        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSearchSetting.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSearchSetting.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region

End Class
