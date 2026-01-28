Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmSystemUserSearch
    Inherits MainPage
#Region "Public Decleration"
    Private ObjClssys_Groups As Clssys_Groups
    Private ObjClsSecurity As Venus.Shared.Web.WebSecurity

    Const uwgGroupSymbol = "G"
    Const uwgEditSymbol = "E"
    Const uwgDeleteSymbol = "D"

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FormHight(7) As Integer
        Dim FormWidth(7) As Integer
        Dim Forms(7) As String
        Dim ButtonIndex(7) As Integer
        Dim SendValue(7) As String

        FormHight(0) = 470
        FormHight(1) = 470
        FormHight(2) = 470
        FormHight(3) = 470
        FormHight(4) = 470
        FormHight(5) = 470
        FormHight(6) = 470
        FormHight(7) = 470

        FormWidth(0) = 726
        FormWidth(1) = 726
        FormWidth(2) = 726
        FormWidth(3) = 726
        FormWidth(4) = 726
        FormWidth(5) = 726
        FormWidth(6) = 787
        FormWidth(7) = 787

        Forms(0) = "frmFormsPermission.aspx"
        Forms(1) = "frmControlsPermission.aspx"
        Forms(2) = "frmRecordsPermission.aspx"
        Forms(3) = "frmGroupsSelector.aspx"
        Forms(4) = "frmReportPermission.aspx"
        Forms(4) = "frmReportPermission.aspx"
        Forms(5) = "frmModulesPermission.aspx"
        Forms(6) = "frmSystemGroups.aspx"
        Forms(7) = "frmSystemGroups.aspx"

        ButtonIndex(0) = 3
        ButtonIndex(1) = 4
        ButtonIndex(2) = 5
        ButtonIndex(3) = 6
        ButtonIndex(4) = 7
        ButtonIndex(5) = 8
        ButtonIndex(6) = 9
        ButtonIndex(7) = 10

        SendValue(0) = uwgGroupSymbol
        SendValue(1) = uwgGroupSymbol
        SendValue(2) = uwgGroupSymbol
        SendValue(3) = uwgEditSymbol
        SendValue(4) = uwgGroupSymbol
        SendValue(5) = uwgGroupSymbol
        SendValue(6) = uwgEditSymbol
        SendValue(7) = uwgDeleteSymbol


        Venus.Shared.Web.ClientSideActions.GridSelection(Page, UwgSearchUsers)
        ToolBarButtonClickAction(Page, "frmSystemGroups.aspx", 665, 787)
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

        ObjClssys_Groups = New Clssys_Groups(Me.Page)
        ObjClssys_Groups.Find("ID <>0 Order By Code")
        UwgSearchUsers.DataSource = ObjClssys_Groups.DataSet.Tables(0).DefaultView
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
