Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmReligions
    Inherits MainPage
#Region "Public Decleration"
    Private ClsReligions As Clshrs_VariousRequestTypes
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsReligions = New Clshrs_VariousRequestTypes(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsReligions.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    'btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsReligions.ConnectionString)
                ClsReligions.AddOnChangeEventToControls("frmReligions", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]

                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsReligions.Find(" Code='" & txtCode.Text & "'")

            Else
                SetScreenInformation("N")
            End If
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsReligions = New Clshrs_VariousRequestTypes(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsReligions.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                SavePart()
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
                Dim StrSql As String = ""
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsReligions.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsReligions.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsReligions.ID & "&TableName=" & ClsReligions.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsReligions.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsReligions.ID & "&TableName=" & ClsReligions.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"
                Dim StrSql As String = ""
                StrSql = "SELECT TOP 1 * FROM SS_VariousRequestTypes Order By Code ASC"
                GetValues(ClsReligions)
            Case "Previous"
                Dim StrSql As String = ""
                StrSql = "SELECT TOP 1 * FROM   SS_VariousRequestTypes  WHERE Code < '" & txtCode.Text & "' ORDER BY Code DESC"
                If Not ClsReligions.previousRecord() Then
                    ClsReligions.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))

                End If
                GetValues(ClsReligions)
            Case "Next"
                ClsReligions.Find("Code='" & txtCode.Text & "'")
                If Not ClsReligions.NextRecord() Then
                    ClsReligions.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))

                End If
                GetValues(ClsReligions)
            Case "Last"
                ClsReligions.Find("Code='" & txtCode.Text & "'")
                ClsReligions.LastRecord()
                GetValues(ClsReligions)
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsReligions = New Clshrs_VariousRequestTypes(Me.Page)

        Try
            ClsReligions.Find("Code='" & txtCode.Text & "'")

            If Not String.IsNullOrEmpty(ClsReligions.Code) Then
                If Not AssignValue(ClsReligions) Then
                    Exit Function

                End If
                ClsReligions.Update("Code='" & txtCode.Text & "'")

            Else
                If Not AssignValue(ClsReligions) Then
                    Exit Function

                End If
                ClsReligions.Save()
            End If

            ClsReligions.Find("Code='" & txtCode.Text & "'")

            'clsMainOtherFields.CollectDataAndSave(value.Text, ClsReligions.Table, ClsReligions.VariousRequestTypeID)
            value.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function AssignValue(ByRef ClsReligions As Clshrs_VariousRequestTypes) As Boolean
        Try
            With ClsReligions
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues(ByVal ClsReligions As Clshrs_VariousRequestTypes) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsReligions
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName


                ImageButton_Delete.Enabled = True

                Dim item As New System.Web.UI.WebControls.ListItem()


                If (.ID > 0) Then
                    StrMode = "E"
                Else
                    StrMode = "N"
                End If


                ImageButton_Delete.Enabled = False

            End With
            Return True
        Catch ex As Exception
        End Try
        Return True

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
                    ClsReligions.Find("ID=" & intID)
                    GetValues(ClsReligions)
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsReligions.Find("ID=" & intID)
                    GetValues(ClsReligions)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
        Return True

    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsReligions
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
        Return True

    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsReligions = New Clshrs_VariousRequestTypes(Me.Page)
        Try
            With ClsReligions
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Page, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
            End With
        Catch ex As Exception
        End Try
        Return True

    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsReligions = New Clshrs_VariousRequestTypes(Me.Page)
        If IntId > 0 Then
            ClsReligions.Find("ID=" & IntId)
            GetValues(ClsReligions)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
        Return True

    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsReligions = New Clshrs_VariousRequestTypes(Me.Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Try
            ClsReligions.Find("Code='" & txtCode.Text & "'")
            'IntId = ClsReligions.VariousRequestTypeID
            txtEngName.Focus()
            If Not String.IsNullOrEmpty(ClsReligions.Code) Then
                GetValues(ClsReligions)
                StrMode = "E"
            Else

                txtCode.Focus()


                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"

            End If

            'If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
            '    ImageButton_Delete.Enabled = False
            'End If
        Catch ex As Exception
        End Try
        Return True

    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
        Return True

    End Function
    Private Function AfterOperation() As Boolean
        ClsReligions.Clear()
        GetValues(ClsReligions)
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
        Return True
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        'txtCode.Text = String.Empty
        TxtReportName.Text = String.Empty
        ImageButton_Delete.Enabled = False
        Return True

    End Function

#End Region
End Class
