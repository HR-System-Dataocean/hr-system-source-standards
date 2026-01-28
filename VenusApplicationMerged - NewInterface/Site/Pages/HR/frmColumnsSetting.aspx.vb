Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class Interfaces_frmColumnsSetting
    Inherits System.Web.UI.Page

    '========================================================================
    'ProcedureName  :  InitializeCulture 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  
    '                1-Get language form Cookies 
    '                2-Create Calture from the incoming language 
    '                3-Apply the calture on the current form
    '                4-
    'Developer         : DataOcean
    'Date Created      : 01-08-07
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'sender             :
    'e                  :
    '========================================================================

    Protected Overrides Sub InitializeCulture()
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage
        'Use this
        If (_culture <> "Auto") Then

            Dim ci As New System.Globalization.CultureInfo(_culture)
            Dim StrDateFormat As String = System.Configuration.ConfigurationManager.AppSettings("DATEFORMAT")
            Dim myDateTimePatterns() As String = {StrDateFormat, StrDateFormat}
            Dim DateTimeFormat As New System.Globalization.DateTimeFormatInfo
            DateTimeFormat = ci.DateTimeFormat
            DateTimeFormat.SetAllDateTimePatterns(myDateTimePatterns, "d"c)
            System.Threading.Thread.CurrentThread.CurrentCulture = ci
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci

        End If
        MyBase.InitializeCulture()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim IntID As Integer = Request.QueryString.Item("ID")
        Dim StrColumnsType As String = Request.QueryString.Item("ColumnsType")
        Dim Clsbase As New ClsDataAcessLayer(Page)
        Dim ClsSearchSettingColumns As New Clssys_SearchsColumns(Me.Page)
        Dim Arr(3) As String
        Arr(0) = "txtEngName"
        Arr(1) = "txtArbName"
        Arr(2) = "txtLenght"
        Arr(3) = "DdlLanguage"
        If Not IsPostBack Then
            With ClsSearchSettingColumns
                .fnFind("sys_searchscolumns.ID=" & IntID)
                txtCode.Text = .FieldName
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtLenght.Text = .InputLength
                If StrColumnsType = "Criteria" Then
                    txtRank.Text = .RankCriteria
                Else
                    txtRank.Text = .RankView
                End If
                If .IsArabic = 0 Then
                    DdlLanguage.SelectedValue = ""
                ElseIf .IsArabic = 1 Then
                    DdlLanguage.SelectedValue = 1
                Else
                    DdlLanguage.SelectedValue = Nothing
                End If
                DdlLanguage.SelectedValue = .RegComputerID
            End With
        End If
        Venus.Shared.Web.ClientSideActions.SetupTabOrder(Page, Arr, Drawing.Color.White, False)
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtEngName, False)
        Venus.Shared.Web.ClientSideActions.SetupForNumberOnly(Page, txtLenght, False)
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnCancel.Click
        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Dim IntID As Integer = Request.QueryString.Item("ID")
        Dim StrColumnsType As String = Request.QueryString.Item("ColumnsType")
        Dim Clsbase As New ClsDataAcessLayer(Page)
        Dim ClsSearchSettingColumns As New Clssys_SearchsColumns(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsSearchSettingColumns.ConnectionString)
        With ClsSearchSettingColumns
            .fnFind("sys_searchscolumns.ID=" & IntID)
            If Val(txtLenght.Text) > .ActualLength Or Val(txtLenght.Text) = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Length must equal original length/ÿÊ· «·Õﬁ· ·«»œ √‰ ÌﬂÊ‰ „”«Ê ·ÿÊ· «·Õﬁ· «·√’·Ï"))
                txtLenght.Text = ""
                Exit Sub
            End If
            .Alignment = 0
            .ArbName = txtArbName.Text
            .EngName = txtEngName.Text

            .RegComputerID = 0
            If DdlLanguage.SelectedValue <> 2 Then
                .IsArabic = IIf(DdlLanguage.SelectedValue = 1, 1, 0)
                .RegComputerID = IIf(DdlLanguage.SelectedValue = 1, 1, 0)
            Else
                .IsArabic = Nothing
                .RegComputerID = 2
            End If

            .InputLength = Val(txtLenght.Text)
            If StrColumnsType = "Criteria" Then
                .RankCriteria = Val(txtRank.Text)
            Else
                .RankView = Val(txtRank.Text)
            End If
            .FieldID = .fngetFieldIDSearchsColumns("Fieldid", " ID = " & IntID)
            .RegUserID = Clsbase.DataBaseUserID
            .RegDate = Date.Now
            .fnUpdate("ID=" & IntID)
        End With
        DoRefresh(Page)
    End Sub

    Public Shared Function DoRefresh(ByRef pgSender As System.Web.UI.Page) As Boolean
        Dim StrFunction As String = String.Empty
        StrFunction &= " function doRefresh() " & vbNewLine
        StrFunction &= "{" & vbNewLine
        'StrFunction &= "window.opener.document.form1.submit();" & vbNewLine
        StrFunction &= "window.close();" & vbNewLine
        StrFunction &= "}" & vbNewLine
        Venus.Shared.Web.ClientSideActions.WriteJavaFunction(pgSender, StrFunction, "Comman0")
        pgSender.RegisterStartupScript("Load", "<script language=""javascript"" id=""CommanZero"">doRefresh();</script>")
    End Function

End Class
