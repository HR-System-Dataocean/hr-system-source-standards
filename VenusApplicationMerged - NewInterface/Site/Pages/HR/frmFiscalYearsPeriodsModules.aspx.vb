Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmFiscalYearsPeriodsModules
    Inherits MainPage
#Region "Public Decleration"
    Private ClsProfession As Clshrs_Professions
    Private ClsModules As Clssys_Modules
    Private ClsFiscalYearsPeriods As Clssys_FiscalYearsPeriods
    Private ClsFiscalYearsPeriodsModules As Clssys_FiscalYearsPeriodsModules
    Private ClsFiscalYears As Clssys_FiscalYears
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")

        SetScreenInformation()
        ClsFiscalYears = New Clssys_FiscalYears(Page)
        Dim clsFP As New Clssys_FiscalYearsPeriods(Page)
        Dim clsNavigation = New Venus.Shared.Web.NavigationHandler(ClsFiscalYears.ConnectionString)
        Try
            If Not IsPostBack Then
                ClsFiscalYears.GetDropDown(ddlFiscalYear)

                If ddlFiscalYear.Items.Count = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, clsNavigation.SetLanguage(Page, "You must enter at least one fiscal year/يجب أن تدخل على الأقل سنة مالية"))
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    Exit Sub
                End If

                clsFP.Find(" GetDate() between sys_FiscalYearsPeriods.FromDate And sys_FiscalYearsPeriods.ToDate ")
                If clsFP.ID > 0 Then
                    ddlFiscalYear.SelectedValue = clsFP.FiscalYearID
                End If
                ImageButton_Save.Enabled = True
                LoadDDLs()
                InitializeDG()
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsFiscalYears.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_Print.Command

        Select Case e.CommandArgument
            Case "Save"
                SavePart()
                InitializeDG()
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)

        End Select
    End Sub
    Protected Sub ddlFiscalYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFiscalYear.SelectedIndexChanged
        LoadDDLs()
        InitializeDG()
    End Sub

#End Region

#Region "Private Functions"

    Private Sub LoadDDLs()
        ClsModules = New Clssys_Modules(Page)
        ClsFiscalYearsPeriods = New Clssys_FiscalYearsPeriods(Page)
        ClsModules.GetDropDown(ddlModules, " FiscalYearDependant=1 ")
        If (ddlFiscalYear.Items.Count > 0) Then
            ClsFiscalYearsPeriods.GetDropDown(ddlFiscalYearsPeriods, "sys_FiscalYearsPeriods.FiscalYearID=" & ddlFiscalYear.SelectedValue)
        End If
    End Sub
    Private Function InitializeDG() As Boolean

        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage

        ClsFiscalYearsPeriodsModules = New Clssys_FiscalYearsPeriodsModules(Page)

        uwgData.Columns.Clear()
        uwgData.Rows.Clear()

        Dim colCnt As Integer = ddlModules.Items.Count
        Dim rwsCnt As Integer = ddlFiscalYearsPeriods.Items.Count

        Dim nCol As Infragistics.WebUI.UltraWebGrid.UltraGridColumn
        Dim nRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow

        Dim tab As New DataTable()
        Dim nTCol As DataColumn
        Dim nTRow As DataRow


        '============First Col ID=================
        nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("ID", "ID", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)
        nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
        nCol.Width = System.Web.UI.WebControls.Unit.Pixel(0)
        nCol.BaseColumnName = "ID"
        nCol.Hidden = True
        '------------------
        nTCol = New DataColumn("ID", GetType(Integer))


        uwgData.Columns.Add(nCol)
        '------------------------
        tab.Columns.Add(nTCol)
        '============End===========================

        '============Second Col FiscalPeriods======
        If _culture = "ar-EG" Then
            uwgData.DisplayLayout.HeaderStyleDefault.HorizontalAlign = HorizontalAlign.Right
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("FYPM", "الفترات الزمنية", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)

        Else
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn("FYPM", "Periods", Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet, Nothing)

        End If
        nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
        nCol.Width = 200 ' System.Web.UI.WebControls.Unit.Pixel(100)
        nCol.CellButtonDisplay = Infragistics.WebUI.UltraWebGrid.CellButtonDisplay.Always
        nCol.BaseColumnName = "FiscalYearsPeriods"
        '------------------
        nTCol = New DataColumn("FiscalYearsPeriods", GetType(String))


        uwgData.Columns.Add(nCol)
        '------------------------
        tab.Columns.Add(nTCol)
        '=============END=========================


        Dim colWidth = (uwgData.Width.Value - 170) / colCnt

        For Each li As ListItem In ddlModules.Items
            nCol = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn(li.Text, li.Text, Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox, False)
            nCol.AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes
            nCol.Width = 100 ' System.Web.UI.WebControls.Unit.Pixel(colWidth)
            nCol.CellStyle.HorizontalAlign = HorizontalAlign.Center
            nCol.CellButtonDisplay = Infragistics.WebUI.UltraWebGrid.CellButtonDisplay.Always
            nCol.DataType = "System.Boolean"
            Dim wdt As Integer = 100 \ ddlModules.Items.Count
            nCol.Width = New Unit(wdt & "%")
            nCol.BaseColumnName = li.Text
            uwgData.Columns.Add(nCol)
            uwgData.Columns(2).Hidden = False
            nTCol = New DataColumn(li.Text, GetType(Boolean))
            tab.Columns.Add(nTCol)


        Next

        For Each li As ListItem In ddlFiscalYearsPeriods.Items


            nTRow = tab.NewRow()
            nTRow(1) = li.Text
            tab.Rows.Add(nTRow)

        Next


        ClsFiscalYearsPeriodsModules = New Clssys_FiscalYearsPeriodsModules(Page)
        ClsFiscalYearsPeriodsModules.Find("sys_FiscalYearsPeriods.FiscalYearID=" & ddlFiscalYear.SelectedValue & " AND sys_Modules.FiscalYearDependant=1 ")

        Dim tabFYPM As DataTable = ClsFiscalYearsPeriodsModules.DataSet.Tables(0).Copy()

        For Each r As DataRow In tabFYPM.Rows

            Dim openDate As Date
            Try
                openDate = r("OpenDate")
            Catch ex As Exception
                openDate = Nothing
            End Try

            Dim arr() As Integer = GetColRowIndices(CInt(r("ModuleID")), CInt(r("FiscalYearPeriodID")))
            Dim intID As Integer = r("ID")
            If (Not openDate = Nothing) Then
                If arr(0) <> -1 And arr(1) <> -1 Then
                    tab.Rows(arr(1))(arr(0) + 2) = True
                End If
            End If
        Next
        uwgData.DisplayLayout.AddNewRowDefault.Visible = Infragistics.WebUI.UltraWebGrid.AddNewRowVisible.No
        uwgData.DataSource = tab
        uwgData.DataBind()
        ImageButton_Save.Enabled = True
        If Not IsNothing(tab) Then
            If tab.Rows.Count <= 0 Then
                ImageButton_Save.Enabled = False
            End If
        End If
    End Function
    Private Function GetColRowIndices(ByVal moduleID As Integer, ByVal FiscalYearPeriods As Integer) As Integer()
        Dim arr(1) As Integer

        arr(0) = ddlModules.Items.IndexOf(ddlModules.Items.FindByValue(moduleID.ToString()))
        arr(1) = ddlFiscalYearsPeriods.Items.IndexOf(ddlFiscalYearsPeriods.Items.FindByValue(FiscalYearPeriods.ToString()))
        Return arr

    End Function
    Private Function GetColRowIDs(ByVal ColNo As Integer, ByVal RowNo As Integer) As Integer()
        Dim arr(1) As Integer

        arr(0) = CInt(ddlModules.Items(ColNo).Value)
        arr(1) = CInt(ddlFiscalYearsPeriods.Items(RowNo).Value)
        Return arr

    End Function
    Private Function SetReturnback() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim StrTargetControl As String = Request.QueryString.Item("TargetControl")
        Select Case StrMode
            Case "R"
                Venus.Shared.Web.ClientSideActions.DoReturnBack(Page, StrTargetControl)
            Case Else
                Venus.Shared.Web.ClientSideActions.DoRefresh(Page)
        End Select
    End Function

    Private Function SavePart() As Boolean
        ClsFiscalYearsPeriodsModules = New Clssys_FiscalYearsPeriodsModules(Page)
        Dim ClsNav As New Venus.Shared.Web.NavigationHandler(ClsFiscalYearsPeriodsModules.ConnectionString)
        Try
            Dim rInd As Integer
            Dim cInd As Integer
            For rInd = 0 To uwgData.Rows.Count - 1
                Dim currDGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow = uwgData.Rows(rInd)
                For cInd = 2 To uwgData.Columns.Count - 1

                    Dim bOpenDate As Object = Nothing
                    If (currDGRow.Cells(cInd).Value = True) Then
                        bOpenDate = Date.Now()
                    End If
                    Dim arr() As Integer = GetColRowIDs(cInd - 2, rInd)
                    ClsFiscalYearsPeriodsModules.Find(" FiscalYearPeriodID=" & arr(1).ToString() & " AND ModuleID=" & arr(0).ToString() & " ")

                    If (ClsFiscalYearsPeriodsModules.ID > 0) Then
                        Dim currID As Integer = ClsFiscalYearsPeriodsModules.ID
                        If (ClsFiscalYearsPeriodsModules.OpenDate = Nothing And currDGRow.Cells(cInd).Value = True) Then
                            bOpenDate = Date.Now()
                        ElseIf ((Not ClsFiscalYearsPeriodsModules.OpenDate = Nothing) And currDGRow.Cells(cInd).Value = True) Then
                            bOpenDate = ClsFiscalYearsPeriodsModules.OpenDate
                        Else
                            bOpenDate = Nothing
                        End If
                        If (AssignValue(ClsFiscalYearsPeriodsModules, arr(1), arr(0), bOpenDate, Nothing)) Then
                            ClsFiscalYearsPeriodsModules.Update(" ID=" & currID)
                        End If
                    Else
                        If (AssignValue(ClsFiscalYearsPeriodsModules, arr(1), arr(0), bOpenDate, Nothing)) Then
                            ClsFiscalYearsPeriodsModules.Save()
                        End If
                    End If

                Next
            Next
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNav.SetLanguage(Page, "Save Done !/تم الحفظ"))
        Catch ex As Exception
            Dim str As String = ex.Message
            Dim str2 As String = ex.Message
        End Try
    End Function
    Private Function AssignValue(ByRef ClsFiscalYearsPeriodsModules As Clssys_FiscalYearsPeriodsModules, ByVal FiscalYearPeriodID As Integer, ByVal ModuleID As Integer, ByVal OpenDate As Object, ByVal CloseDate As Date) As Boolean
        Try
            With ClsFiscalYearsPeriodsModules
                .FiscalYearPeriodID = FiscalYearPeriodID
                .ModuleID = ModuleID
                .OpenDate = OpenDate
                .CloseDate = CloseDate
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetValues(ByRef ClsFiscalYearsPeriodsModules As Clssys_FiscalYearsPeriodsModules) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Try
            With ClsFiscalYearsPeriodsModules
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With SearchColumns
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
        ClsFiscalYears = New Clssys_FiscalYears(Page)
        Try
            With ClsFiscalYears
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
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            ClsFiscalYearsPeriodsModules = New Clssys_FiscalYearsPeriodsModules(Page)
            Select Case ptrType
                Case "N", "R"

                Case "D"
                    ClsFiscalYearsPeriodsModules.Find("ID=" & intID)
                    GetValues(ClsFiscalYearsPeriodsModules)
                    ImageButton_Save.Visible = False

                Case "E"
                    ClsFiscalYearsPeriodsModules.Find("ID=" & intID)
                    GetValues(ClsFiscalYearsPeriodsModules)

            End Select
        Catch ex As Exception
        End Try
    End Function
    
#End Region
End Class
