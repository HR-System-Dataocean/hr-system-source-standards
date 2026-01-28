Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmSystemConfig
    Inherits MainPage
#Region "Public Decleration"
    Private ClsTicketsAgencies As Clshrs_TicketsAgencies
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsTicketsAgencies = New Clshrs_TicketsAgencies(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsTicketsAgencies.ConnectionString)
                ClsTicketsAgencies.AddOnChangeEventToControls("frmSystemConfig", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                'GetValues()

                Dim clsCompanies As New Clssys_Companies(Page)

                clsCompanies.GetDropDownList(DdlCompany, True)

            End If

            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0


        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsTicketsAgencies.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_Print.Command
        ClsTicketsAgencies = New Clshrs_TicketsAgencies(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsTicketsAgencies.ConnectionString)
        Select Case e.CommandArgument

            Case "Save"
                If DdlCompany.SelectedIndex = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Select Company /برجاء اختيار الشركة"))
                    Exit Sub
                End If
                If SavePart() Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"
                AfterOperation()


            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"


        End Select
    End Sub

    Protected Sub DdlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlCompany.SelectedIndexChanged
        If DdlCompany.SelectedIndex > 0 Then
            GetValues()
        End If


    End Sub
#End Region

#Region "Private Functions"
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsTicketsAgencies = New Clshrs_TicketsAgencies(Page)
        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler

        WebHandler.GetCookies(Page, "UserID", User)
        Try

            Dim count As Integer = CInt(txtMinimumCostCentersCount.Text)
            Dim Str = "delete from sys_SystemConfig where CompanyId=" & DdlCompany.SelectedValue & "; "
            Str &= " INSERT INTO [dbo].[sys_SystemConfig] ([UseCostCenter],[MinimumCostCentersCount],PreventChangeContractEndDate,RegUserID,MultiBranchedPosition,CompanyId) VALUES(" & If(chkUseCostCenter.Checked, "1", "0") & "," & count & "," & If(ChkPreventChangeEndDate.Checked, "1", "0") & ",'" & User & "'," & If(chkMultiBranchedPosition.Checked, "1", "0") & "," & DdlCompany.SelectedValue & ")"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsTicketsAgencies.ConnectionString, System.Data.CommandType.Text, Str)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            Dim strselectAction = "Select * from sys_SystemConfig where CompanyId=" & DdlCompany.SelectedValue
            Dim dsActions As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsUser.ConnectionString, CommandType.Text, strselectAction)
            For Each dr As DataRow In dsActions.Tables(0).Rows

                If Not IsDBNull(dr("UseCostCenter")) Then
                    chkUseCostCenter.Checked = CBool(dr("UseCostCenter"))
                End If

                If Not IsDBNull(dr("MultiBranchedPosition")) Then
                    chkMultiBranchedPosition.Checked = CBool(dr("MultiBranchedPosition"))
                End If

                If Not IsDBNull(dr("PreventChangeContractEndDate")) Then
                    ChkPreventChangeEndDate.Checked = CBool(dr("PreventChangeContractEndDate"))
                End If

                If Not IsDBNull(dr("MinimumCostCentersCount")) Then
                    txtMinimumCostCentersCount.Text = dr("MinimumCostCentersCount").ToString()
                Else
                    txtMinimumCostCentersCount.Text = ""
                End If

            Next
            Return True
        Catch ex As Exception
            Dim x = ex.ToString()
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
                    ImageButton_Print.Enabled = .Item("AllowPrint")
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



                    If ImageButton_Print.Enabled = True And .Item("CanPrint") = True Then
                        ImageButton_Print.Enabled = Not .Item("CanPrint")
                    End If
                End With
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType


                Case "D"
                    ClsTicketsAgencies.Find("ID=" & intID)
                    GetValues()
                    ImageButton_Save.Visible = False
                Case "E"
                    ClsTicketsAgencies.Find("ID=" & intID)
                    GetValues()
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsTicketsAgencies
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsTicketsAgencies = New Clshrs_TicketsAgencies(Me.Page)
        Try
            With ClsTicketsAgencies
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
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsTicketsAgencies = New Clshrs_TicketsAgencies(Me.Page)
        If IntId > 0 Then
            ClsTicketsAgencies.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function

    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True

        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsTicketsAgencies.Clear()
        GetValues()
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
        Return True
    End Function




#End Region
End Class
