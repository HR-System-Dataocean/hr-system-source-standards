Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System

Partial Class frmEmployeeLocationGeoPoints
    Inherits MainPage
#Region "Public Decleration"
    Dim clsLocationGeoPoints As Clshrs_LocationGeoPoints
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsLocations As Clssys_Locations
    Private ClsEmployees As Clshrs_Employees
    Private StrMode As String
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        StrMode = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        clsLocationGeoPoints = New Clshrs_LocationGeoPoints(Me)
        ClsLocations = New Clssys_Locations(Me.Page)
        ClsEmployees = New Clshrs_Employees(Me.Page)

        Try
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", clsLocationGeoPoints.ConnectionString)
                clsLocationGeoPoints.AddOnChangeEventToControls("frmEmployeeLocationGeoPoints", Page, UltraWebTab1)

                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-GB")
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-GB")

                SetupSearchButtons()

                If (txtLocationCode.Text <> "") Then
                    GetValues()
                Else
                    SetScreenInformation("N")
                End If
            End If

            CreateOtherFields(0)
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, ex.HResult, clsLocationGeoPoints.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Protected Sub UwgGeoPoints_UpdateRow(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UwgGeoPoints.UpdateRow

    End Sub

    Protected Sub UwgGeoPoints_InitializeRow(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles UwgGeoPoints.InitializeRow

    End Sub

    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        clsLocationGeoPoints = New Clshrs_LocationGeoPoints(Me)
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsLocationGeoPoints.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtLocationCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Location Code /برجاء إدخال كود الموقع"))
                    Exit Sub
                End If
                If txtEmployeeCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Employee Code /برجاء إدخال كود الموظف"))
                    Exit Sub
                End If

                SaveGeoPointsData()
                AfterOperation()
                Clear()
            Case "Save"
                If txtLocationCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Location Code /برجاء إدخال كود الموقع"))
                    Exit Sub
                End If
                If txtEmployeeCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Employee Code /برجاء إدخال كود الموظف"))
                    Exit Sub
                End If

                SaveGeoPointsData()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
            Case "New"
                AfterOperation()
                Clear()
            Case "Delete"
                DeleteGeoPointsData()
                AfterOperation()
                Clear()
            Case "Property"
                clsLocationGeoPoints.Find("LocationID='" & hfLocationID.Value & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & clsLocationGeoPoints.ID & "&TableName=" & clsLocationGeoPoints.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                clsLocationGeoPoints.Find("LocationID='" & hfLocationID.Value & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & clsLocationGeoPoints.ID & "&TableName=" & clsLocationGeoPoints.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = clsLocationGeoPoints.Table
                clsLocationGeoPoints.Find(" LocationID = '" & hfLocationID.Value & "'")
                Dim recordID As Integer = clsLocationGeoPoints.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & clsLocationGeoPoints.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                Venus.Shared.Web.ClientSideActions.ClosePage(Page)
            Case "First"
                FirstRecord()
            Case "Previous"
                PreviousRecord()
            Case "Next"
                NextRecord()
            Case "Last"
                LastRecord()
        End Select
    End Sub

    Protected Sub txtLocationCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLocationCode.TextChanged
        CheckCode()
    End Sub

    Protected Sub txtEmployeeCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmployeeCode.TextChanged
        CheckEmployeeCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function GetGridCellValue(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow, ByVal columnKey As String) As Object
        Try
            If row Is Nothing Then Return Nothing
            Dim cell As Infragistics.WebUI.UltraWebGrid.UltraGridCell = row.Cells.FromKey(columnKey)
            If cell Is Nothing OrElse cell.Value Is Nothing OrElse cell.Value Is DBNull.Value Then
                Return Nothing
            End If
            Return cell.Value
        Catch
            Return Nothing
        End Try
    End Function

    Private Function GetGridCellString(ByVal row As Infragistics.WebUI.UltraWebGrid.UltraGridRow, ByVal columnKey As String, Optional ByVal defaultValue As String = "") As String
        Dim val As Object = GetGridCellValue(row, columnKey)
        If val Is Nothing Then Return defaultValue
        Return val.ToString()
    End Function

    Private Sub AssignValues()
        Try
            With clsLocationGeoPoints
                .LocationID = hfLocationID.Value
                .EmployeeID = hfEmployeeID.Value
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetValues() As Boolean
        Try
            SetToolBarDefaults()

            ' Get location details
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsLocationGeoPoints.ConnectionString)
            If ClsLocations.Find("Code='" & txtLocationCode.Text & "'") Then
                hfLocationID.Value = ClsLocations.ID
                txtLocationName.Text = ObjNavigationHandler.SetLanguage(Page, ClsLocations.EngName & "/" & ClsLocations.ArbName)
            Else
                hfLocationID.Value = "0"
                txtLocationName.Text = ""
            End If

            UpdateEmployeeNameLabel()

            ' Load geo points for this location and employee
            LoadGeoPointsData()

            If (UwgGeoPoints.Rows.Count > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If

            SetToolBarPermission(Me, clsLocationGeoPoints.ConnectionString, clsLocationGeoPoints.DataBaseUserRelatedID, clsLocationGeoPoints.GroupID, StrMode)
            If Not clsLocationGeoPoints.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(clsLocationGeoPoints.ID)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function LoadGeoPointsData() As Boolean
        Try
            Dim str As String = ""
            Dim LocationID As Integer = 0
            Dim EmployeeID As Integer = 0

            If hfLocationID.Value <> "" Then
                LocationID = Convert.ToInt32(hfLocationID.Value)
            End If
            If hfEmployeeID.Value <> "" Then
                EmployeeID = Convert.ToInt32(hfEmployeeID.Value)
            End If

            str = "SELECT ID, LocationID, LineNum, Code, EngName, ArbName, Latitude, Longitude, AllowedRadius, Address, Active " &
                  "FROM hrs_LocationGeoPoints " &
                  "WHERE LocationID = " & LocationID & " AND EmployeeID = " & EmployeeID & " " &
                  "ORDER BY LineNum"

            Dim ds As New DataSet
            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsLocationGeoPoints.ConnectionString, Data.CommandType.Text, str)

            UwgGeoPoints.DataSource = ds.Tables(0)
            UwgGeoPoints.DataBind()
            UwgGeoPoints.Rows.Add()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveGeoPointsData() As Boolean
        Try
            Dim LocationID As Integer = 0
            Dim EmployeeID As Integer = 0

            If hfLocationID.Value <> "" Then
                LocationID = Convert.ToInt32(hfLocationID.Value)
            End If
            If hfEmployeeID.Value <> "" Then
                EmployeeID = Convert.ToInt32(hfEmployeeID.Value)
            End If

            ' Delete existing geo points for this location and employee
            Dim DeleteCommand As String = "DELETE FROM hrs_LocationGeoPoints WHERE LocationID = " & LocationID & " AND EmployeeID = " & EmployeeID
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsLocationGeoPoints.ConnectionString, Data.CommandType.Text, DeleteCommand)

            ' Insert new geo points
            Dim LineNumber As Integer = 0
            Dim SqlCommandString As String = ""

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgGeoPoints.Rows
                Dim Code As String = GetGridCellString(DGRow, "Code")
                If Code <> "" Then
                    LineNumber = LineNumber + 1

                    Code = Code.Replace("'", "''")
                    Dim EngName As String = GetGridCellString(DGRow, "EngName").Replace("'", "''")
                    Dim ArbName As String = GetGridCellString(DGRow, "ArbName").Replace("'", "''")
                    Dim Latitude As String = GetGridCellString(DGRow, "Latitude", "0")
                    Dim Longitude As String = GetGridCellString(DGRow, "Longitude", "0")
                    Dim AllowedRadius As String = GetGridCellString(DGRow, "AllowedRadius", "0")
                    Dim Address As String = GetGridCellString(DGRow, "Address").Replace("'", "''")
                    Dim ActiveVal As Object = GetGridCellValue(DGRow, "Active")
                    Dim Active As Integer = If(ActiveVal IsNot Nothing AndAlso Convert.ToBoolean(ActiveVal), 1, 0)

                    SqlCommandString &= "INSERT INTO hrs_LocationGeoPoints " &
                                  "(LocationID, EmployeeID, LineNum, Code, EngName, ArbName, Latitude, Longitude, AllowedRadius, Address, Active) VALUES (" &
                                  LocationID & ", " &
                                  EmployeeID & ", " &
                                  LineNumber & ", " &
                                  "'" & Code & "', " &
                                  "'" & EngName & "', " &
                                  "'" & ArbName & "', " &
                                  Latitude & ", " &
                                  Longitude & ", " &
                                  AllowedRadius & ", " &
                                  "'" & Address & "', " &
                                  Active & "); " & Environment.NewLine
                End If
            Next

            If SqlCommandString <> "" Then
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsLocationGeoPoints.ConnectionString, CommandType.Text, SqlCommandString)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DeleteGeoPointsData() As Boolean
        Try
            Dim LocationID As Integer = 0
            Dim EmployeeID As Integer = 0

            If hfLocationID.Value <> "" Then
                LocationID = Convert.ToInt32(hfLocationID.Value)
            End If
            If hfEmployeeID.Value <> "" Then
                EmployeeID = Convert.ToInt32(hfEmployeeID.Value)
            End If

            Dim DeleteCommand As String = "DELETE FROM hrs_LocationGeoPoints WHERE LocationID = " & LocationID & " AND EmployeeID = " & EmployeeID
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsLocationGeoPoints.ConnectionString, Data.CommandType.Text, DeleteCommand)

            Return True
        Catch ex As Exception
            Return False
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
                    ImageButton_Delete.Enabled = .Item("AllowDelete")
                    ImageButton_Print.Enabled = .Item("AllowPrint")
                    Select Case Mode
                        Case "N", "R"
                            ImageButton_Save.Enabled = .Item("AllowAdd")
                            ImageButton_SaveN.Enabled = .Item("AllowAdd")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")
                            ImageButton_SaveN.Enabled = .Item("AllowEdit")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                    End Select
                End With
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With clsLocationGeoPoints
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function

    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim IntId As Integer = Request.QueryString.Item("ID")
        clsLocationGeoPoints = New Clshrs_LocationGeoPoints(Me)
        Try
            With clsLocationGeoPoints
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

    Private Sub CheckCode()
        Try
            GetValues()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CheckEmployeeCode()
        Try
            If Not String.IsNullOrEmpty(txtEmployeeCode.Text) Then
                ClsEmployees = New Clshrs_Employees(Page)
                If ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'") Then
                    hfEmployeeID.Value = ClsEmployees.ID
                    txtEmployeeName.Text = ClsEmployees.FullName
                Else
                    hfEmployeeID.Value = "0"
                    txtEmployeeName.Text = ""
                    Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Employee not found /الموظف غير موجود"))
                End If
                LoadGeoPointsData()
            Else
                hfEmployeeID.Value = "0"
                txtEmployeeName.Text = ""
                LoadGeoPointsData()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub UpdateEmployeeNameLabel()
        Try
            If String.IsNullOrEmpty(txtEmployeeCode.Text) Then
                txtEmployeeName.Text = ""
                Return
            End If
            ClsEmployees = New Clshrs_Employees(Page)
            If ClsEmployees.Find("Code='" & txtEmployeeCode.Text & "'") Then
                hfEmployeeID.Value = ClsEmployees.ID
                txtEmployeeName.Text = ClsEmployees.FullName
            Else
                hfEmployeeID.Value = "0"
                txtEmployeeName.Text = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetToolBarDefaults()
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Sub

    Private Sub AfterOperation()
        clsLocationGeoPoints.Clear()
        GetValues()
        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtLocationCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Sub

    Private Sub Clear()
        txtLocationCode.Text = ""
        txtEmployeeCode.Text = ""
        txtLocationName.Text = ""
        txtEmployeeName.Text = ""
        hfLocationID.Value = "0"
        hfEmployeeID.Value = "0"
        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegUserValue.Text = ""
        lblCancelDateValue.Text = ""
        UwgGeoPoints.Rows.Clear()
        UwgGeoPoints.Rows.Add()
    End Sub

    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, clsLocationGeoPoints.Table) = True Then
            Dim StrTablename As String
            clsLocationGeoPoints = New Clshrs_LocationGeoPoints(Me)
            StrTablename = clsLocationGeoPoints.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmEmployeeLocationGeoPoints")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmEmployeeLocationGeoPoints")
            End If
        End If
    End Function

    Private Sub SetupSearchButtons()
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0

        ' Setup Location search button
        If ClsObjects.Find(" Code='sys_Locations'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                SearchID = ClsSearchs.ID
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtLocationCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtLocationCode.ClientID & "'"
                btnSearchLocation.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If

        ' Setup Employee search button
        ClsEmployees = New Clshrs_Employees(Page)
        If ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                SearchID = ClsSearchs.ID
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployeeCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtEmployeeCode.ClientID & "'"
                btnSearchEmployee.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If
    End Sub

    Private Sub FirstRecord()
        Dim str As String = "SELECT TOP 1 LocationID FROM hrs_LocationGeoPoints ORDER BY LocationID ASC"
        Dim FirstID As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsLocationGeoPoints.ConnectionString, Data.CommandType.Text, str)
        If FirstID IsNot Nothing AndAlso Not Convert.IsDBNull(FirstID) Then
            ClsLocations.Find("ID=" & FirstID)
            txtLocationCode.Text = ClsLocations.Code
            GetValues()
        End If
    End Sub

    Private Sub PreviousRecord()
        Dim str As String = "SELECT TOP 1 LocationID FROM hrs_LocationGeoPoints WHERE LocationID < " & hfLocationID.Value & " ORDER BY LocationID DESC"
        Dim PrevID As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsLocationGeoPoints.ConnectionString, Data.CommandType.Text, str)
        If PrevID IsNot Nothing AndAlso Not Convert.IsDBNull(PrevID) Then
            ClsLocations.Find("ID=" & PrevID)
            txtLocationCode.Text = ClsLocations.Code
            GetValues()
        Else
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsLocationGeoPoints.ConnectionString)
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
        End If
    End Sub

    Private Sub NextRecord()
        Dim str As String = "SELECT TOP 1 LocationID FROM hrs_LocationGeoPoints WHERE LocationID > " & hfLocationID.Value & " ORDER BY LocationID ASC"
        Dim NextID As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsLocationGeoPoints.ConnectionString, Data.CommandType.Text, str)
        If NextID IsNot Nothing AndAlso Not Convert.IsDBNull(NextID) Then
            ClsLocations.Find("ID=" & NextID)
            txtLocationCode.Text = ClsLocations.Code
            GetValues()
        Else
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsLocationGeoPoints.ConnectionString)
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
        End If
    End Sub

    Private Sub LastRecord()
        Dim str As String = "SELECT TOP 1 LocationID FROM hrs_LocationGeoPoints ORDER BY LocationID DESC"
        Dim LastID As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsLocationGeoPoints.ConnectionString, Data.CommandType.Text, str)
        If LastID IsNot Nothing AndAlso Not Convert.IsDBNull(LastID) Then
            ClsLocations.Find("ID=" & LastID)
            txtLocationCode.Text = ClsLocations.Code
            GetValues()
        End If
    End Sub

#End Region
End Class
