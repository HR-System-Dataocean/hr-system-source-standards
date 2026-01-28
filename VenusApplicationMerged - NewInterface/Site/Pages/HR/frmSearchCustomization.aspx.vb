Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmSearchCustomization
    Inherits MainPage

#Region "CONSTANTS"
    Const uwgColumnName = 0
    Const uwgEngDesc = 1
    Const uwgArbDesc = 2
    Const uwgOrgLength = 3
    Const uwgLength = 4
    Const uwgLanguage = 5
    Const uwgID = 7
    Const uwgButton = 6
    Const uwgIsView = 8
    Const uwgIsCriteria = 8
    Const Csave = 1
    Const uwgObjectID = 3

    Dim mErrorHandler As Venus.Shared.ErrorsHandler
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Clsbase As New ClsDataAcessLayer(Page)
        Dim ClsSearchSetting As New Clssys_Searchs(Me.Page)
        Dim ClsSearchSettingColumns As New Clssys_SearchsColumns(Me.Page)
        Dim Arr(2) As String
        Arr(0) = "txtCode"
        Arr(1) = "txtEngName"
        Arr(2) = "txtArbName"
        If Not IsPostBack Then

            ClsSearchSetting.Find(" CODE <> ''  ORDER BY CODE ")
            uwgNavigation.DataSource = ClsSearchSetting.DataSet.Tables(0).DefaultView
            uwgNavigation.DataBind()
            If txtCode.Text = String.Empty Then
                ImageButton_Save.Enabled = False
            Else
                ImageButton_Save.Enabled = True
            End If
        End If
        If txtReload.Value = "true" Then
            ActiveRowChange(uwgNavigation.DisplayLayout.ActiveRow)
            txtReload.Value = "false"
        End If
        Venus.Shared.Web.ClientSideActions.SetupTabOrder(Page, Arr, Drawing.Color.White, False)
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtEngName, False)
        Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

        uwgcriteria.BorderWidth = 0
        uwgNavigation.BorderWidth = 0
        uwgViewColumns.BorderWidth = 0
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Refresh.Command, ImageButton_Save.Command
        Dim Clsbase As New ClsDataAcessLayer(Page)
        Dim ClsSearchSetting As New Clssys_Searchs(Me.Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(Clsbase.ConnectionString)
        Select Case e.CommandArgument
            Case "Save"
                With ClsSearchSetting
                    .ID = uwgNavigation.DisplayLayout.ActiveRow.Cells(2).Value
                    .EngName = txtEngName.Text
                    .ArbName = txtArbName.Text
                    .ObjectID = uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value
                    .Code = txtCode.Text
                    If .Update("ID = " & .ID) Then
                        fnUpdateColumns(.ID, Clsbase.ConnectionString)
                    End If

                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
                End With
                AfterOperation()
            Case "Refresh"
                AfterOperation()
        End Select
    End Sub
    Protected Sub uwgNavigation_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgNavigation.ActiveRowChange
        ActiveRowChange(e.Row)
    End Sub
#End Region

#Region "Private Function"
    Private Function ActiveRowChange(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow) As Boolean
        Dim Clsbase As New ClsDataAcessLayer(Page)
        Dim ClsSearchSetting As New Clssys_Searchs(Me.Page)
        Dim ClsSearchSettingColumns As New Clssys_SearchsColumns(Me.Page)
        If IsNothing(row) Then
            Return False
        End If
        ClsSearchSetting.Find("ID=" & row.Cells(2).Value)
        txtCode.Text = ClsSearchSetting.Code
        txtEngName.Text = ClsSearchSetting.EngName
        txtArbName.Text = ClsSearchSetting.ArbName
        With ClsSearchSettingColumns
            .fnFind("SearchID=" & row.Cells(2).Value & " And IsCriteria=1 ", uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value)
            uwgcriteria.DataSource = .DataSet.Tables(0).DefaultView
            uwgcriteria.DataBind()
        End With
        With ClsSearchSettingColumns
            .fnFind("SearchID=" & row.Cells(2).Value & " And IsView = 1", uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value)
            uwgViewColumns.DataSource = .DataSet.Tables(0).DefaultView
            uwgViewColumns.DataBind()
        End With
        If txtCode.Text = String.Empty Then
            ImageButton_Save.Enabled = False
        Else
            ImageButton_Save.Enabled = True
        End If
    End Function
    Private Sub fnPrepareGridToUpdate(ByRef uwg As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, Optional ByVal FirstColumn As Boolean = False)
        Dim uwgColumn As Infragistics.WebUI.UltraWebGrid.UltraGridColumn
        For Each uwgColumn In uwg.Columns
            uwgColumn.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes
        Next
        If FirstColumn Then uwg.Columns(0).AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
    End Sub
    Private Function fnUpdateColumns(ByVal SearchID As Integer, ByVal ConnectionString As String) As Boolean
        Dim ObjDataRow As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim IntFieldID As Integer
        Try
            For Each ObjDataRow In uwgViewColumns.Rows
                Dim ClsColumn As New Clssys_SearchsColumns(Page)
                With ClsColumn
                    .Find(" sys_SearchsColumns.SearchID =  " & SearchID & " And sys_SearchsColumns.FieldID =  " & .fngetFieldID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, ObjDataRow.Cells(uwgColumnName).Value))
                    .FieldID = .fngetFieldID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, ObjDataRow.Cells(uwgColumnName).Value)
                    .InputLength = CType(ObjDataRow.Cells(uwgLength).Text, Integer)
                    .EngName = CType(ObjDataRow.Cells(uwgEngDesc).Text, String)
                    .ArbName = CType(ObjDataRow.Cells(uwgArbDesc).Text, String)
                    .RankView = ObjDataRow.Cells.FromKey("Rank").Text
                    .IsTarget = ObjDataHandler.DataValue_In(ObjDataRow.Cells.FromKey("IsTarget").Value, Data.SqlDbType.Bit)
                    If .IsTarget = True Then
                        IntFieldID = .FieldID
                    End If
                    .Alignment = 0
                    .IsCriteria = ObjDataRow.Cells(uwgIsCriteria).Value
                    .IsView = True
                    .SearchID = SearchID
                    .fnUpdate(" sys_SearchsColumns.SearchID =  " & .SearchID & " And sys_SearchsColumns.FieldID =  " & .FieldID)
                End With
            Next
            ObjDataRow = New Infragistics.WebUI.UltraWebGrid.UltraGridRow
            For Each ObjDataRow In uwgcriteria.Rows
                Dim ClsColumn As New Clssys_SearchsColumns(Page)
                With ClsColumn
                    .Find(" sys_SearchsColumns.SearchID =  " & SearchID & " And sys_SearchsColumns.FieldID =  " & .fngetFieldID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, ObjDataRow.Cells(uwgColumnName).Value))
                    .FieldID = .fngetFieldID(uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value, ObjDataRow.Cells(uwgColumnName).Value)
                    .InputLength = CType(ObjDataRow.Cells.FromKey("InputLength").Text, Integer)
                    .EngName = CType(ObjDataRow.Cells.FromKey("EngName").Text, String)
                    .ArbName = CType(ObjDataRow.Cells.FromKey("ArbName").Text, String)
                    .RankCriteria = ObjDataRow.Cells.FromKey("Rank").Text 'CType(ObjDataRow.Cells.FromKey("Rank").Text, Integer)
                    .Alignment = 0
                    .IsCriteria = True
                    .IsView = ObjDataRow.Cells(uwgIsView).Value
                    .SearchID = SearchID
                    If (.FieldID = IntFieldID) Then
                        .IsTarget = True
                    End If
                    .fnUpdate(" sys_SearchsColumns.SearchID =  " & .SearchID & " And sys_SearchsColumns.FieldID =  " & .FieldID)
                End With
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AfterOperation() As Boolean
        Dim Clsbase As New ClsDataAcessLayer(Page)
        Dim ClsSearchSetting As New Clssys_Searchs(Me.Page)
        Try
            ClsSearchSetting.Find(" CODE <> ''  ORDER BY CODE ")
            uwgNavigation.DataSource = ClsSearchSetting.DataSet.Tables(0).DefaultView
            uwgNavigation.DataBind()
            ImageButton_Save.Enabled = False
            uwgNavigation.DisplayLayout.SelectedRows.Clear()
            uwgNavigation.DisplayLayout.ActiveRow = Nothing
            txtArbName.Text = String.Empty
            txtCode.Text = String.Empty
            txtEngName.Text = String.Empty

            uwgcriteria.DataSource = Nothing
            uwgcriteria.DataBind()

            uwgViewColumns.DataSource = Nothing
            uwgViewColumns.DataBind()

        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsSearchSetting.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsSearchSetting.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
#End Region

End Class
