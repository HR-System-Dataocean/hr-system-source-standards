Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmSystemUserSearch
    Inherits MainPage

#Region "Public Decleration"
    Private ObjClsSys_Users As ClsSys_Users
    Private ObjClsSecurity As Venus.Shared.Web.WebSecurity
#End Region

#Region "constant"

    Const indFormPermission = 3
    Const indControlPermission = 4
    Const indRecordPermission = 5
    Const indGroupPermission = 6
    Const indReportPermission = 7
    Const indModulePermission = 8
    Const indWidgetPermission = 12

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FormHight(8) As Integer
        Dim FormWidth(8) As Integer
        Dim Forms(8) As String
        Dim ButtonIndex(8) As Integer
        Dim SendValue(8) As String

        FormHight(0) = 500
        FormHight(1) = 500
        FormHight(2) = 500
        FormHight(3) = 500
        FormHight(4) = 500
        FormHight(5) = 500
        FormHight(6) = 500
        FormHight(7) = 500
        FormHight(8) = 500

        FormWidth(0) = 787
        FormWidth(1) = 787
        FormWidth(2) = 787
        FormWidth(3) = 787
        FormWidth(4) = 787
        FormWidth(5) = 787
        FormWidth(6) = 787
        FormWidth(7) = 787
        FormWidth(8) = 787

        Forms(0) = "frmFormsPermission.aspx"
        Forms(1) = "frmControlsPermission.aspx"
        Forms(2) = "frmRecordsPermission.aspx"
        Forms(3) = "frmGroupsSelector.aspx"
        Forms(4) = "frmReportPermission.aspx"
        Forms(4) = "frmReportPermission.aspx"
        Forms(5) = "frmModulesPermission.aspx"
        Forms(6) = "frmUsers.aspx"
        Forms(7) = "frmUsers.aspx"
        Forms(8) = "frmWidgetsPermission.aspx"

        ButtonIndex(0) = 3
        ButtonIndex(1) = 4
        ButtonIndex(2) = 5
        ButtonIndex(3) = 6
        ButtonIndex(4) = 7
        ButtonIndex(5) = 8
        ButtonIndex(6) = 9
        ButtonIndex(7) = 10
        ButtonIndex(8) = 12

        SendValue(0) = "U"
        SendValue(1) = "U"
        SendValue(2) = "U"
        SendValue(3) = "E"
        SendValue(4) = "U"
        SendValue(5) = "U"
        SendValue(6) = "E"
        SendValue(7) = "D"
        SendValue(8) = "U"

        Venus.Shared.Web.ClientSideActions.GridSelection(Page, UwgSearchUsers)
        ToolBarButtonClickAction(Page, "frmUsers.aspx", 665, 787)
        Venus.Shared.Web.ClientSideActions.GridMultiClickAction(Page, UwgSearchUsers, FormHight, FormWidth, ButtonIndex, 11, Forms, SendValue)

        SetData()
        lblCount.Text = " Page " & UwgSearchUsers.DisplayLayout.Pager.CurrentPageIndex & " From " & UwgSearchUsers.DisplayLayout.Pager.PageCount
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_New.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_Refresh.Command

        Select Case e.CommandArgument

            Case "Refresh"

            Case "Next"
                If UwgSearchUsers.DisplayLayout.Pager.CurrentPageIndex < UwgSearchUsers.DisplayLayout.Pager.PageCount Then
                    UwgSearchUsers.DisplayLayout.Pager.CurrentPageIndex += 1
                    SetData()
                    lblCount.Text = " Page " & UwgSearchUsers.DisplayLayout.Pager.CurrentPageIndex & " From " & UwgSearchUsers.DisplayLayout.Pager.PageCount
                End If

            Case "Back"
                If UwgSearchUsers.DisplayLayout.Pager.CurrentPageIndex > 1 Then
                    UwgSearchUsers.DisplayLayout.Pager.CurrentPageIndex -= 1
                    lblCount.Text = " Page " & UwgSearchUsers.DisplayLayout.Pager.CurrentPageIndex & " From " & UwgSearchUsers.DisplayLayout.Pager.PageCount
                    SetData()
                End If

        End Select
    End Sub
    Protected Sub UwgSearchUsers_InitializeDataSource(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs) Handles UwgSearchUsers.InitializeDataSource
        SetData()
    End Sub
    Protected Sub UwgSearchUsers_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs) Handles UwgSearchUsers.InitializeLayout

        'Me.UwgSearchUsers.Browser = Infragistics.WebUI.UltraWebGrid.BrowserLevel.Xml
        'Me.UwgSearchUsers.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.Xml
        Me.UwgSearchUsers.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Flat
        Me.UwgSearchUsers.Bands(0).FilterOptions.AllowRowFiltering = Infragistics.WebUI.UltraWebGrid.RowFiltering.OnClient
        Me.UwgSearchUsers.Bands(0).FilterOptions.RowFilterMode = Infragistics.WebUI.UltraWebGrid.RowFilterMode.SiblingRowsOnly
        Me.UwgSearchUsers.Bands(0).FilterOptions.ShowAllCondition = Infragistics.WebUI.UltraWebGrid.ShowFilterString.Yes
        Me.UwgSearchUsers.Bands(0).FilterOptions.ShowEmptyCondition = Infragistics.WebUI.UltraWebGrid.ShowFilterString.Yes
        Me.UwgSearchUsers.Bands(0).FilterOptions.ShowNonEmptyCondition = Infragistics.WebUI.UltraWebGrid.ShowFilterString.Yes

    End Sub
#End Region

#Region "Private Functions"
    Private Sub SetData()

        ObjClsSys_Users = New Clssys_Users(Me.Page)

        '=============== 0257 [Start]
        Dim ClsCurrUser As New Clssys_Users(Page)
        Dim strFilter As String = " ID <>0 "
        If ClsCurrUser.Find(" ID =" & ObjClsSys_Users.DataBaseUserRelatedID) Then
            If ClsCurrUser.IsAdmin Then
                strFilter = " ID <>0 "
                ImageButton_New.Visible = True
            Else
                strFilter = " ID=" & ClsCurrUser.ID
                ImageButton_New.Visible = False

                With ClsCurrUser

                    If .ChangeFormPermission Then
                        UwgSearchUsers.Columns(indFormPermission).Hidden = False
                    Else
                        UwgSearchUsers.Columns(indFormPermission).Hidden = True
                    End If

                    If .ChangeControlPermission Then
                        UwgSearchUsers.Columns(indControlPermission).Hidden = False
                    Else
                        UwgSearchUsers.Columns(indControlPermission).Hidden = True
                    End If

                    If .ChangeRecordPermission Then
                        UwgSearchUsers.Columns(indRecordPermission).Hidden = False
                    Else
                        UwgSearchUsers.Columns(indRecordPermission).Hidden = True
                    End If

                    If .ChangeReportPermission Then
                        UwgSearchUsers.Columns(indReportPermission).Hidden = False
                    Else
                        UwgSearchUsers.Columns(indReportPermission).Hidden = True
                    End If

                    If .ChangeModulePermission Then
                        UwgSearchUsers.Columns(indModulePermission).Hidden = False
                    Else
                        UwgSearchUsers.Columns(indModulePermission).Hidden = True
                    End If

                    If .CanChangeGroups Then
                        UwgSearchUsers.Columns(indGroupPermission).Hidden = False
                    Else
                        UwgSearchUsers.Columns(indGroupPermission).Hidden = True
                    End If

                End With

            End If
        End If

        '=============== 0257 [ End ]

        ObjClsSys_Users.Find(" " & strFilter & " Order By Code")
        UwgSearchUsers.DataSource = ObjClsSys_Users.DataSet.Tables(0).DefaultView
        UwgSearchUsers.DataBind()

    End Sub
    Private Function ToolBarButtonClickAction(ByVal pgSender As System.Web.UI.Page, ByVal TargetForm As String, ByVal Height As Integer, ByVal Width As Integer, Optional ByVal SendValue As String = "") As Boolean
        Dim StrFunction As String = String.Empty
        Dim strSendValue As String = IIf(SendValue = "", "", "&Sendvalue=" & SendValue)
        StrFunction &= "function ShowScreenNewUser()" & vbNewLine
        StrFunction &= "{" & vbNewLine
        StrFunction &= "var hight =window.screen.availHeight -35;" & vbNewLine
        StrFunction &= "var width =window.screen.availWidth -10;" & vbNewLine
        StrFunction &= "var winopen = window.open(""" & TargetForm & "?Mode=N" & strSendValue & """,""popFrameless"",""height=" & Height & ",width=" & Width & ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=Yes"");" & vbNewLine
        StrFunction &= "winopen.document.focus();" & vbNewLine
        StrFunction &= "}" & vbNewLine
        Venus.Shared.Web.ClientSideActions.WriteJavaFunction(pgSender, StrFunction, "Comman3")
        ImageButton_New.OnClientClick = "ShowScreenNewUser()"
    End Function
#End Region

End Class
