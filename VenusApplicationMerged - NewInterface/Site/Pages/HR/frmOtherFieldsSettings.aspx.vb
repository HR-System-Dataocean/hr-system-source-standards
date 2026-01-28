Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmOtherFieldsSettings
    Inherits MainPage 

#Region "Constants"
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const uwgObjectID = 2
    Const uwgTableName = 1
    Const uwgButtonUpate = 10
    Const uwgPrimaryKey = 11
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Clsbase As New ClsDataAcessLayer(Page)
        Dim clsOtherFieldsGroups As New ClsSys_OtherFieldsGroups(Me.Page)
        Dim clsOtherFields As New ClsSys_OtherFields(Me.Page)
        Dim clsSearchSettings As New Clssys_Searchs(Me.Page)
        If Not IsPostBack Then
            uwgNavigation.DataSource = clsSearchSettings.fnGetTables().Tables(0)
            uwgNavigation.DataBind()
            If Not uwgNavigation.DisplayLayout.ActiveRow Is Nothing Then
                With clsOtherFields
                    .Find(" sys_OtherFields.ObjectID = " & uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value)
                    uwgOtherFields.DataSource = .DataSet.Tables(0).DefaultView
                    uwgOtherFields.DataBind()
                End With
            End If
        End If
        
        Venus.Shared.Web.ClientSideActions.GridSelection(Page, uwgOtherFields)
        Venus.Shared.Web.ClientSideActions.GridButtonClickAction(Page, uwgOtherFields, 460, 485, uwgButtonUpate, uwgPrimaryKey, "frmOtherFields.aspx", "E")
        Venus.Shared.Web.ClientSideActions.GridDClickAction(Page, uwgOtherFields, 460, 485, uwgPrimaryKey, "frmOtherFields.aspx", "E")
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Refresh.Command
        Dim Clsbase As New ClsDataAcessLayer(Page)
        Dim clsSysOtherFields As New ClsSys_OtherFields(Me.Page)
        Dim ClsSearchSetting As New Clssys_Searchs(Me.Page)
        Select Case e.CommandArgument
            Case "Refresh"
                AfterOperation()
        End Select
    End Sub
    'Protected Sub btnAddField_Click1(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnAddField.Click
    '    If Not uwgNavigation.DisplayLayout.ActiveRow Is Nothing Then
    '        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFields.aspx?ObjID=" & uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value & "&TableName=" & uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgTableName).Value & "&MODE=" & "N", 740, 530, , , "WindowName", False, False, False, False, False, False, False, False, False)
    '    Else
    '        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, "Please Select Target Table")
    '    End If
    'End Sub
    Protected Sub uwgNavigation_ActiveRowChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgNavigation.ActiveRowChange
        Dim clsOtherFields As New ClsSys_OtherFields(Page)
        If Not uwgNavigation.DisplayLayout.ActiveRow Is Nothing Then
            With clsOtherFields
                .Find(" Sys_OtherFields.ObjectID = " & uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value)
                uwgOtherFields.DataSource = .DataSet.Tables(0).DefaultView
                uwgOtherFields.DataBind()
                Session("ObjID_ActiveRow") = uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value & "|" & uwgNavigation.DisplayLayout.ActiveRow.Index

            End With
        End If
    End Sub
#End Region

#Region "Private Function"
    Private Function fnPrepareToUpdate(ByRef clsOtherFields As ClsSys_OtherFields) As Boolean
        Dim ObjDataRow As New Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Try
            For Each ObjDataRow In uwgOtherFields.Rows
                With clsOtherFields
                    .ID = CType(ObjDataRow.Cells(11).Text, Integer)
                    .ObjectID = uwgNavigation.DisplayLayout.ActiveRow.Cells(uwgObjectID).Value
                    .OtherFieldGroupID = CType(ObjDataRow.Cells(12).Text, Integer)
                    .Rank = CType(ObjDataRow.Cells(3).Text, Integer)
                    .EngName = CType(ObjDataRow.Cells(0).Text, String)
                    .ArbName = CType(ObjDataRow.Cells(14).Text, String)
                    .FieldType = CType(ObjDataRow.Cells(15).Text, String)
                    .ViewObjectID = CType(ObjDataRow.Cells(16).Text, Integer)
                    .ViewEngFieldID = CType(ObjDataRow.Cells(17).Text, Integer)
                    .ViewArbFieldID = CType(ObjDataRow.Cells(18).Text, Integer)
                    .DataType = CType(ObjDataRow.Cells(19).Text, Int16)
                    .DataLength = CType(ObjDataRow.Cells(5).Text, Integer)
                    If ObjDataRow.Cells(10).Value = True Then
                        .CancelDate = Now()
                    End If
                    .Update(" sys_OtherFields.ID =  " & .ID)
                End With
            Next
        Catch

        End Try
    End Function
    Private Function AfterOperation() As Boolean
        Dim clsSearchSettings As New Clssys_Searchs(Me.Page)
        Dim clsOtherFields As New ClsSys_OtherFields(Page)

        Try
            Dim intObjectID As Integer = CStr(Session("ObjID_ActiveRow")).Split("|")(0)
            Dim intActiveRow As Integer = CStr(Session("ObjID_ActiveRow")).Split("|")(1)

            uwgNavigation.DataSource = Nothing
            uwgNavigation.DataBind()
            uwgNavigation.DataSource = clsSearchSettings.fnGetTables().Tables(0)
            uwgNavigation.DataBind()
            uwgNavigation.DisplayLayout.ActiveRow = uwgNavigation.Rows(intActiveRow)

            uwgOtherFields.DataSource = Nothing
            uwgOtherFields.DataBind()
            clsOtherFields.Find(" Sys_OtherFields.ObjectID = " & intObjectID)
            uwgOtherFields.DataSource = clsOtherFields.DataSet.Tables(0).DefaultView
            uwgOtherFields.DataBind()
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(clsSearchSettings.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, clsSearchSettings.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region

End Class
