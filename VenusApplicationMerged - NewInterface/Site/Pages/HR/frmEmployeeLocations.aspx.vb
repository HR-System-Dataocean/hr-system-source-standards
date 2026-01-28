Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmEmployeeLocations
    Inherits MainPage
#Region "Public Decleration"
    Private ClsDelegationSChedule As Clshrs_Employees
    Private ClsSCheduleRequests As ClsSS_DelegationSCheduleRequests
    Private mErrorHandler As Venus.Shared.ErrorsHandler

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")

        ClsDelegationSChedule = New Clshrs_Employees(Me.Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)

            If ClsObjects.Find(" Code='" & ClsDelegationSChedule.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID


                End If
            End If
            TxtDelegatorName.Enabled = False

            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then


                Page.Session.Add("ConnectionString", ClsDelegationSChedule.ConnectionString)
                ClsDelegationSChedule.AddOnChangeEventToControls("frmEmployeeLocations", Page, UltraWebTab1)
                GetData(0)
                'AddNewRow()

                Dim WebHandler As New Venus.Shared.Web.WebHandler
                'Dim User As String = String.Empty
                'Dim clsEmployees As New Clshrs_Employees(Page)
                'WebHandler.GetCookies(Page, "UserID", User)
                'Dim _sys_User As New Clssys_Users(Page)
                '_sys_User.Find("ID = '" & User & "'")
                'txtDelegatorEmployeeID.Text = _sys_User.Code
                'txtDelegatorEmployeeID_TextChanged(Nothing, Nothing)
                'ClsObjects.Find(" Code='" & clsEmployees.Table.Trim & "'")
                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                Dim csSearchID As Integer
                csSearchID = ClsSearchs.ID
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtDelegatorEmployeeID.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtDelegatorEmployeeID.ClientID & "'"
                btnDelegatorSearch.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"



                '================================= Exit & Navigation Notification [ End ]

                'Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                ' Ensure it's an integer and assign to txtCode.Text
                FillEmployeeVacations()
                ' Store the ClientID and UniqueID in hidden fields

            End If

            'Page.ClientScript.GetPostBackEventReference(Me, "")
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer


            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, "", Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsDelegationSChedule = New Clshrs_Employees(Me.Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDelegationSChedule.ConnectionString)
        Dim ClsEmployees As Clshrs_Employees = New Clshrs_Employees(Page)
        ClsEmployees.Find("Code='" & txtDelegatorEmployeeID.Text & "'")
        Select Case e.CommandArgument
            Case "SaveNew"

                SaveGrid(ClsEmployees.ID)
                AfterOperation()
                Clear()
            Case "Save"

                If SaveGrid(ClsEmployees.ID) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done/تم الحفظ"))
                End If
            Case "New"

                txtDelegatorEmployeeID.Text = ""
                txtDelegatorEmployeeID_TextChanged(Nothing, Nothing)
                AfterOperation()
                'Dim clsEmployees As New Clshrs_Employees(Page)
                'Dim MaxAppIDStr As String = "SELECT ISNULL(MAX(Code), 0) + 1 FROM SS_DelegationSChedule"
                'Dim MaxAppointCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, MaxAppIDStr)

                ' Ensure it's an integer and assign to txtCode.Text

            Case "Delete"
                'ClsDelegationSChedule.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"

            Case "Remarks"


            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"

            Case "Exit"

            Case "First"

            Case "Previous"

            Case "Next"

            Case "Last"

        End Select
        FillEmployeeVacations()
    End Sub


#End Region

#Region "Private Functions"
    Private Function AddNewRow() As Boolean
        Try


            UwgSearchEmployees.DataSource = Nothing
            UwgSearchEmployees.DataBind()

            UwgSearchEmployees.Rows.Add()


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetData(EmpId As Integer) As Boolean
        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDelegationSChedule.ConnectionString)
            Dim EmpName As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " sys_Locations.ArbName "
            Else
                EmpName = " sys_Locations.EngName "
            End If


            Dim str As String = "SELECT  sys_Locations.Id as LocationId, sys_Locations.Code as LocationCode," & EmpName & " as LocationName,(Case when (select hrs_EmployeeLocations.ID from hrs_EmployeeLocations where hrs_EmployeeLocations.LocationId=sys_Locations.Id and EmpId=" & EmpId & ") IS NULL then 0 else 1 end) as [Select]  FROM sys_Locations  where CancelDate is null Order By sys_Locations.Id  "



            Dim ds As New Data.DataSet
            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsDelegationSChedule.ConnectionString, Data.CommandType.Text, str)

            UwgSearchEmployees.DataSource = Nothing
            UwgSearchEmployees.DataBind()
            UwgSearchEmployees.DataSource = ds.Tables(0)
            UwgSearchEmployees.DataBind()

            'UwgSearchEmployees.Rows.Add()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SaveGrid(EmpId As Integer) As Boolean
        Try
            Dim ds As Data.DataSet
            Dim str As String = String.Empty
            Dim str1 As String = String.Empty
            Dim clsEmployees = New Clshrs_Employees(Me)

            'str = "set dateformat DMY; UPDATE hrs_HIPolicyContract SET CancelDate = GetDate() Where [PolicyID]=" & HIPolicyID & ";" & vbNewLine
            'Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsDelegationSChedule.ConnectionString, Data.CommandType.Text, str)

            str = "set dateformat DMY; "
            str &=
                                    " DELETE FROM [dbo].[hrs_EmployeeLocations] where " &
                                    " EmpId = " & EmpId & " ;"

            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, Data.CommandType.Text, str)


            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows

                If row.Cells.FromKey("Select").Value <> Nothing Then
                    clsEmployees = New Clshrs_Employees(Me)

                    str = "set dateformat DMY; "
                    str &= " INSERT INTO [dbo].[hrs_EmployeeLocations] ([EmpId],[LocationId]) VALUES " &
                                    "(" & EmpId &
                                    ",'" & row.Cells.FromKey("LocationId").Value &
                                    "');" & vbNewLine

                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, Data.CommandType.Text, str)


                End If



            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        ClsDelegationSChedule = New Clshrs_Employees(Page)

        Try


            If ClsDelegationSChedule.ID > 0 Then
                If Not AssignValues(ClsDelegationSChedule) Then
                    Exit Function
                End If


            Else
                If Not AssignValues(ClsDelegationSChedule) Then
                    Exit Function
                End If
                ClsDelegationSChedule.Save()

            End If


            value.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AssignValues(ByRef ClsBasicFiles As Object) As Boolean
        Try
            Dim CLSEMP As Clshrs_Employees = New Clshrs_Employees(Page)
            CLSEMP.Find("Code='" & txtDelegatorEmployeeID.Text & "'")

            Dim ClsEmployees As Clshrs_Employees = New Clshrs_Employees(Page)
            'ClsEmployees.Find("Code='" & txtDelegated.Text & "'")
            'With ClsDelegationSChedule
            '    .DelegatorEmployeeID = CLSEMP.ID
            '    .DelegatedEmployeeID = ClsEmployees.ID



            'End With
            Return True
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsDelegationSChedule.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, "", Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
            Return False
        End Try
    End Function
    Private Function GetValues(ByVal ClsDelegationSChedule As Clshrs_Employees) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()

            Dim CLSEMP As Clshrs_Employees = New Clshrs_Employees(Page)
            CLSEMP.Find("ID='" & ClsDelegationSChedule.ID & "'")

            Dim ClsEmployees As Clshrs_Employees = New Clshrs_Employees(Page)
            ClsEmployees.Find("ID='" & ClsDelegationSChedule.ID & "'")
            With ClsDelegationSChedule
                txtDelegatorEmployeeID.Text = CLSEMP.Code

                'chkSaveSend.Checked = .SendAfterSave
                'chkDeleteSend.Checked = .SendAfterDelete
                'chkUpdateSend.Checked = .SendAfterUpdate
                'txtFromEmail.Text = .FromEmail
                'txtToEmail.Text = .ToEmailField
                'txtToTable.Text = .ToTable
                'txtToCondition.Text = .ToFixedCondition
                'txtFormCondition.Text = .ToFormCondition
                'txtTitle.Text = .EmailTitle
                'txtSubject.Text = .EmailBody

                If Not ClsDelegationSChedule.ID = Nothing Then
                    txtDelegatorEmployeeID_TextChanged(Nothing, Nothing)
                End If

                If Not ClsDelegationSChedule.RegUserID = Nothing Then
                    ClsUser.Find("ID=" & ClsDelegationSChedule.RegUserID)
                End If
                If ClsUser.EngName = Nothing Then
                    lblRegUserValue.Text = ""
                Else
                    lblRegUserValue.Text = ClsUser.EngName
                End If
                If Convert.ToDateTime(.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = Convert.ToDateTime(.RegDate).Date
                End If
                If .CancelDate = Nothing Then
                    lblCancelDateValue.Text = ""
                Else
                    lblCancelDateValue.Text = Convert.ToDateTime(.CancelDate).Date
                End If
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                Else
                    ImageButton_Delete.Enabled = True
                End If
                Dim item As New System.Web.UI.WebControls.ListItem()


                If (.ID > 0) Then
                    StrMode = "E"
                Else
                    StrMode = "N"
                End If
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If

            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType
                Case "N", "R"
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
                    ClsDelegationSChedule.Find("ID=" & intID)
                    GetValues(ClsDelegationSChedule)
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsDelegationSChedule.Find("ID=" & intID)
                    GetValues(ClsDelegationSChedule)
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsDelegationSChedule
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)

            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsDelegationSChedule = New Clshrs_Employees(Me.Page)
        Try
            With ClsDelegationSChedule
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
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsDelegationSChedule = New Clshrs_Employees(Me.Page)
        If IntId > 0 Then
            ClsDelegationSChedule.Find("ID=" & IntId)
            GetValues(ClsDelegationSChedule)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsDelegationSChedule = New Clshrs_Employees(Me.Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Try

            IntId = ClsDelegationSChedule.ID
            'txtEngName.Focus()
            If ClsDelegationSChedule.ID > 0 Then
                GetValues(ClsDelegationSChedule)
                StrMode = "E"
            Else


                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"

            End If
            SetToolBarDefaults()
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsDelegationSChedule.Clear()
        GetValues(ClsDelegationSChedule)
        ImageButton_Delete.Enabled = False
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        'txtEngName.Text = String.Empty
        'txtArbName.Text = String.Empty
        txtDelegatorEmployeeID.Text = String.Empty
        TxtDelegatorName.Text = String.Empty


        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        'Dim User As String = String.Empty
        'Dim ClsEmployees = New Clshrs_Employees(Page)
        'WebHandler.GetCookies(Page, "UserID", User)
        'Dim _sys_User As New Clssys_Users(Page)
        '_sys_User.Find("ID = '" & User & "'")
        'txtDelegatorEmployeeID.Text = _sys_User.Code
        'txtDelegatorEmployeeID_TextChanged(Nothing, Nothing)
        UwgSearchEmployees.Rows.Clear()
        'Dim MaxAppIDStr As String = "SELECT ISNULL(MAX(Code), 0) + 1 FROM SS_DelegationSChedule"
        'Dim MaxAppointCode As Object = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, MaxAppIDStr)

        ' Ensure it's an integer and assign to txtCode.Text
        FillEmployeeVacations()
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsDelegationSChedule = New Clshrs_Employees(Me.Page)
        Dim recordID As Integer = ClsDelegationSChedule.ID
        If (recordID > 0) Then
            Dim clsForms As New ClsSys_Forms(Page)
            clsForms.Find(" code = REPLACE('" & formName & "',' ','')")
            Dim clsFormsControls As New Clssys_FormsControls(Page)
            clsFormsControls.Find(" FormID=" & clsForms.ID)
            Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()
            For Each row As Data.DataRow In tab.Rows
                clsFormsControls.Find(" FormID=" & clsForms.ID & " And Name='" & row("Name") & "'")
                Dim sys_Fields As New Clssys_Fields(Page)
                sys_Fields.Find(" ID=" & clsFormsControls.FieldID)
                If (sys_Fields.FieldName.Trim() = "Code" Or sys_Fields.FieldName.Trim() = "Number" Or sys_Fields.FieldName.Trim() = "ID") Then
                    Continue For
                End If
                Dim currCtrl As Control = Me.FindControl(row("Name"))
                Dim bIsArabic As Boolean = IIf(IsDBNull(row("IsArabic")), False, row("IsArabic"))
                If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then
                    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                End If
            Next
        End If
    End Sub

    Protected Sub chkCheckAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkCheckAll.CheckedChanged
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows()
            row.Cells.FromKey("Select").Value = chkCheckAll.Checked
        Next
    End Sub

    Protected Sub txtDelegatorEmployeeID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDelegatorEmployeeID.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtDelegatorEmployeeID.Text) Then
                Dim ClsEmployees As Clshrs_Employees
                ClsEmployees = New Clshrs_Employees(Page)
                Dim EmpName As String
                If ProfileCls.CurrentLanguage = "Ar" Then
                    EmpName = " isnull( hrs_Employees.arbname ,' ')+' '+ isnull(hrs_Employees.FatherArbName, ' ')+' '+ isnull(hrs_Employees.GrandArbName,' ')+' '+isnull(hrs_Employees.FamilyArbName,' ') "

                Else
                    EmpName = " isnull(hrs_Employees.EngName,' ')+' '+isnull(hrs_Employees.FatherEngName,' ')+' '+isnull(hrs_Employees.GrandEngName ,' ')+' '+isnull(hrs_Employees.FamilyEngName,' ')"

                End If
                Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

                Dim DS2 As New Data.DataSet()
                Dim connetionString2 As String
                Dim connection2 As Data.SqlClient.SqlConnection
                Dim command2 As Data.SqlClient.SqlCommand
                Dim adapter2 As New Data.SqlClient.SqlDataAdapter
                connetionString2 = ClsEmployees.ConnectionString
                connection2 = New Data.SqlClient.SqlConnection(connetionString2)
                Dim strselect As String
                strselect = "select " & EmpName & "  FROM  Hrs_Employees where Code='" & txtDelegatorEmployeeID.Text & "'"
                'command2 = New Data.SqlClient.SqlCommand(strselect, connection2)

                Dim AlternativeName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strselect)
                ClsEmployees.Find("Code='" & txtDelegatorEmployeeID.Text & "'")
                If ClsEmployees.ID > 0 Then

                    TxtDelegatorName.Text = AlternativeName
                Else
                    TxtDelegatorName.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Sorry there is no employee with this code !/!عفوا لا يوجد موظف مسجل بهذا الكود"))

                End If

                FillEmployeeVacations()
                GetData(ClsEmployees.ID)
            Else
                TxtDelegatorName.Text = ""
                GetData(0)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillEmployeeVacations()
        Try
            'Dim User As String = String.Empty
            'Dim WebHandler As New Venus.Shared.Web.WebHandler
            'WebHandler.GetCookies(Page, "UserID", User)
            'Dim _sys_User As New Clssys_Users(Page)
            '_sys_User.Find("ID = '" & User & "'")
            If txtDelegatorEmployeeID.Text = "" Then
                Return
            End If
            Dim ClsEmployees As New Clshrs_Employees(Me)
            ClsEmployees.Find("Code='" & txtDelegatorEmployeeID.Text & "'")
            Dim DS1 As New Data.DataSet()
            DS1.Clear()
            For x As Integer = 0 To DS1.Tables.Count - 1
                DS1.Tables(x).Constraints.Clear()
            Next
            DS1.Relations.Clear()
            DS1.Tables.Clear()
            Dim connetionString As String
            Dim connection As Data.SqlClient.SqlConnection
            Dim command As Data.SqlClient.SqlCommand
            Dim adapter As New Data.SqlClient.SqlDataAdapter
            connetionString = ClsEmployees.ConnectionString
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim EmpName As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " sys_Locations.ArbName "
            Else
                EmpName = " sys_Locations.EngName "
            End If


            Dim str1 As String = "SELECT  hrs_EmployeeLocations.LocationId, hrs_EmployeeLocations.LocationCode," & EmpName & " as LocationName,(Case when hrs_EmployeeLocations.Id IS NULL then 0 else 1 end) as Select  FROM hrs_EmployeeLocations Right join sys_Locations on sys_Locations.ID= hrs_EmployeeLocations.LocationId where EmpId=" & ClsEmployees.ID & " Order By hrs_EmployeeLocations.LocationId "



            If str1 <> "" Then
                command = New Data.SqlClient.SqlCommand(str1, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS1, "Table1")

                connection.Close()



            Else

            End If

        Catch ex As Exception

        End Try
    End Sub




#End Region
End Class
