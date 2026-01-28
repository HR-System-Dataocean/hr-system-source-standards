Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Venus.Shared.Web

Partial Class frmEmployeesVacations
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const csOtherFields = 11
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim recordID As Integer
        Dim RequestSerial As Integer = Request.QueryString.Item("RequestSerial")
        Dim FormCode As String = Request.QueryString.Item("FormCode")
        Dim Type As String = Request.QueryString.Item("Type")

        Dim CanBeCanceled As Boolean = Request.QueryString.Item("CanBeCanceled")
        If CanBeCanceled Then
            btnCancelRequest.Visible = True
        Else
            btnCancelRequest.Visible = False
        End If


        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            Dim User As String = String.Empty
            ClsEmployees = New Clshrs_Employees(Page)
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
            If Not IsPostBack Then



                Dim csSearchID As Integer
                Dim ClsLevels As New Clshrs_LevelTypes(Page)
                Dim ClsDataHandler As New Venus.Shared.DataHandler
                Dim StrSerial As String = String.Empty
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
                Dim ClsObjects As New Clssys_Objects(Page)
                Dim ClsSearchs As New Clssys_Searchs(Page)
                Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                'txtEmployee.Attributes.Add("onchange", "ChangeIsDataChanged()")
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
                ClsObjects.Find(" Code='SS_AdvanceHousingRequest'")
                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                csSearchID = ClsSearchs.ID
                lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lage", lblLage.Text)
                Dim IntDimension As Integer = 510

                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
                FillEmployeeVacations(ClsEmployees.ID, False)
                If (lbVactionID.Text <> "") Then
                    ClsEmployeesVacations.Find("ID=" & lbVactionID.Text)
                    recordID = ClsEmployeesVacations.ID
                    EmpVacationId.Value = recordID

                    If (recordID > 0) Then
                        SetScreenInformation("E")
                        SetToolBarRecordPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, ClsEmployeesVacations.Table, recordID)
                    Else
                        SetScreenInformation("N")
                        If Not IsPostBack Then
                            SetTime()
                        End If
                    End If
                Else
                    SetScreenInformation("N")

                    SetTime()

                End If
                'If ClsObjects.Find(" Code='" & ClsEmployeesVacations.Table.ToString.Trim() & "'") Then
                '    ImageButton_Documents.Attributes.Add("onclick", "OpenModal3('frmAttachDocuments.aspx?OId=" & ClsObjects.ID & "&',400,600,true,''); return false;")
            Else
                'txtEmployee_TextChanged(Nothing, Nothing)
                'GetEmpContractVac()
            End If

            If Not IsPostBack Then
                Dim DteStartDate As Date = Date.Now
                Dim DteEndDate As Date = Date.Now
                If Request.QueryString.Count > 0 Then

                    If Request.QueryString.Item("StartDate") <> Nothing Then
                        DteStartDate = Request.QueryString.Item("StartDate")
                    End If
                    If Request.QueryString.Item("ToDate") <> Nothing Then
                        DteEndDate = Request.QueryString.Item("ToDate")
                    End If
                End If

            End If

        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub


    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command
        Dim RequestSerial As Integer = Request.QueryString.Item("RequestSerial")
        Session("RequestSerial") = 0
        Session("RequestSerial") = RequestSerial
        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=RequestSerial&preview=1&ReportCode=CF_131&sq0=''&v=" & Session("RequestSerial"), 700, 490, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)

    End Sub




#End Region

#Region "Private Functions"

    Private Sub AddOnChangeEventToControls(ByVal formName As String)
        Try
            Dim clsForms As New ClsSys_Forms(Page)
            clsForms.Find(" code = REPLACE('" & formName & "',' ','')")
            Dim clsFormsControls As New Clssys_FormsControls(Page)
            If clsForms.ID > 0 Then
                clsFormsControls.Find(" FormID=" & clsForms.ID)
                Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()
                For Each row As Data.DataRow In tab.Rows
                    Dim currCtrl As Control = Me.FindControl(row("Name"))
                    'If TypeOf (currCtrl) Is TextBox Then
                    'End If
                Next
            End If
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Private Function CheckDateBetween2Dates(ByVal d As Date, ByVal d1 As Date, ByVal d2 As Date) As Boolean
        If (d1 = Nothing Or d2 = Nothing) Then
            Return False
        End If
        If (Date.Compare(d, d1) >= 0 And Date.Compare(d, d2) <= 0) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckDateBetween2DatesNew(ByVal d As Date, ByVal d1 As Date, ByVal d2 As Date) As Boolean
        If (d1 = Nothing Or d2 = Nothing) Then
            Return False
        End If
        If (Date.Compare(d, d1) >= 0 And Date.Compare(d, d2) < 0) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckDateBetween2DatesNew2(ByVal d As Date, ByVal d1 As Date, ByVal d2 As Date) As Boolean
        If (d1 = Nothing Or d2 = Nothing) Then
            Return False
        End If
        If (Date.Compare(d, d1) > 0 And Date.Compare(d, d2) < 0) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function CheckVacation() As Boolean

        Return True
        'End If
    End Function
    Private Function SetTime() As Boolean
        Dim clsCompanies As New Clssys_Companies(Page)
        Dim dteNow As Date = Format(Date.Now, "dd/MM/yyyy")
        Try
            With clsCompanies
            End With
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function





    Private Sub FillEmployeeVacations(ByVal EmployeeID As Integer, Optional ByVal showFirstRecord As Boolean = False)

        Dim RequestSerial As Integer = Request.QueryString.Item("RequestSerial")
        Dim FormCode As String = Request.QueryString.Item("FormCode")
        Dim Type As String = Request.QueryString.Item("Type")
        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        If Type = 2 Then
            Try

                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")
                Dim ClsEmployees As New Clshrs_Employees(Page)
                'ClsEmployees.Find("Code='" & _sys_User.Code & "'")
                Dim DS1 As New Data.DataSet()
                Dim connetionString As String
                Dim connection As Data.SqlClient.SqlConnection
                Dim command As Data.SqlClient.SqlCommand
                Dim adapter As New Data.SqlClient.SqlDataAdapter
                connetionString = ClsEmployees.ConnectionString
                connection = New Data.SqlClient.SqlConnection(connetionString)
                Dim EmpName As String
                Dim ActionName As String

                If ProfileCls.CurrentLanguage = "Ar" Then
                    EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                    ActionName = " SS_UserActions.ActionAraName "
                Else
                    EmpName = "[dbo].[fn_GetEmpName](hrs_Employees.Code,0)"
                    ActionName = " SS_UserActions.ActionEngName "
                End If


                Dim str1 As String
                'str1 = "select " & EmpName & " as EmployeeName,( case when SS_UserActions.ActionAraName is not null then SS_UserActions.ActionAraName else 'Pending ...' end) As Action ,convert(varchar, ActionDate,101) as ActionDate,ActionRemarks  from SS_AdvanceHousingRequest join SS_RequestActions on SS_AdvanceHousingRequest.ID=SS_RequestActions.RequestSerial and SS_AdvanceHousingRequest.EmployeeID=SS_RequestActions.EmployeeID join hrs_Employees on hrs_Employees.ID= SS_RequestActions.SS_EmployeeID left join SS_UserActions on SS_RequestActions.ActionID=SS_UserActions.ID where RequestSerial=" & RequestSerial & " And SS_RequestActions.FormCode='" & FormCode & "' and ( SS_RequestActions.IsHidden is null or SS_RequestActions.IsHidden=0 ) and SS_AdvanceHousingRequest.EmployeeID=" & ClsEmployees.ID & " "
                str1 = "select " & EmpName & " as EmployeeName,( case when " & ActionName & " is not null then " & ActionName & " else 'Pending ...' end) As Action ,convert(varchar, ActionDate,101) as ActionDate,ActionRemarks ,SS_RequestActions.ConfirmedNoOfdays  from SS_AdvanceHousingRequest join SS_RequestActions on SS_AdvanceHousingRequest.ID=SS_RequestActions.RequestSerial and SS_AdvanceHousingRequest.EmployeeID=SS_RequestActions.EmployeeID join hrs_Employees on hrs_Employees.ID= SS_RequestActions.SS_EmployeeID left join SS_UserActions on SS_RequestActions.ActionID=SS_UserActions.ID where RequestSerial=" & RequestSerial & " And SS_RequestActions.FormCode='" & FormCode & "' and ( SS_RequestActions.IsHidden is null or SS_RequestActions.IsHidden=0 )  "

                command = New Data.SqlClient.SqlCommand(str1, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS1, "Table1")
                adapter.Dispose()
                command.Dispose()
                connection.Close()
                Dim DataCol1 As Data.DataColumn
                DataCol1 = DS1.Tables(0).Columns(0)
                uwgEmployeeVacations.DataSource = Nothing
                uwgEmployeeVacations.DataBind()

                uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
                uwgEmployeeVacations.DataSource = DS1
                uwgEmployeeVacations.DataBind()




                Dim DS2 As New Data.DataSet()
                Dim connetionString2 As String
                Dim connection2 As Data.SqlClient.SqlConnection
                Dim command2 As Data.SqlClient.SqlCommand
                Dim adapter2 As New Data.SqlClient.SqlDataAdapter
                connetionString2 = ClsEmployees.ConnectionString
                connection2 = New Data.SqlClient.SqlConnection(connetionString)
                Dim strselect As String

                Dim firstshifName As String
                Dim secondshifName As String
                If ProfileCls.CurrentLanguage = "Ar" Then
                    firstshifName = "الوردية الاولى"
                    secondshifName = "الوردية الثانية"
                Else
                    firstshifName = "first shif"
                    secondshifName = "second shift"
                End If

                strselect = "select SS_AdvanceHousingRequest.Code as RequestSerial ,Convert(date,RequestDate) as RequestDate,Remarks,InstallmentDate,InstallmentsCount from SS_AdvanceHousingRequest  where  SS_AdvanceHousingRequest.ID=" & RequestSerial & ""
                command2 = New Data.SqlClient.SqlCommand(strselect, connection2)
                adapter2.SelectCommand = command2
                adapter2.Fill(DS2, "Table1")
                adapter2.Dispose()
                command2.Dispose()
                connection2.Close()
                If DS2.Tables(0).Rows.Count > 0 Then


                    TxtRequestSerial.Text = DS2.Tables(0).Rows(0)("RequestSerial").ToString()
                    TxtRequestDate.Text = CDate(DS2.Tables(0).Rows(0)("RequestDate").ToString()).ToShortDateString()
                    If Convert.ToString(DS2.Tables(0).Rows(0)("InstallmentDate")) <> "" Then
                        InstallmentDate.Value = CDate(DS2.Tables(0).Rows(0)("InstallmentDate")).Date
                    End If
                    If Convert.ToString(DS2.Tables(0).Rows(0)("InstallmentsCount")) <> "" Then
                        txtInstallmentsNo.Text = (DS2.Tables(0).Rows(0)("InstallmentsCount"))
                    Else
                        txtInstallmentsNo.Text = "0"
                    End If
                    TxtRemarks.Text = DS2.Tables(0).Rows(0)("Remarks").ToString()
                End If


                Dim SelectRequestType As String = ""
                Dim RequestType As String = ""
                If ProfileCls.CurrentLanguage = "Ar" Then
                    SelectRequestType = "RequestArbName"
                Else
                    SelectRequestType = "RequestEngName"

                End If
                'Fill Request type
                Dim strRequester As String = "select distinct Employeeid from SS_RequestActions where RequestSerial=" & RequestSerial & " and FormCode='" & FormCode & "'"
                Dim requesterid As Integer = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, strRequester))
                Dim RequesterData As String = "select Code," & EmpName & " as EmployeeName From hrs_Employees where ID=" & requesterid & ""
                Dim DsRequesterData As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(
    ClsEmployees.ConnectionString,
    CommandType.Text,
    RequesterData,
    New SqlClient.SqlParameter("@EmployeeID", requesterid)
)
                If DsRequesterData.Tables(0).Rows.Count > 0 Then
                    TxtEmpCode.Text = DsRequesterData.Tables(0).Rows(0)("Code")
                    TxtEmpName.Text = DsRequesterData.Tables(0).Rows(0)("EmployeeName")
                End If


                Dim requesttypestr As String
                requesttypestr = "Select " & SelectRequestType & " from SS_RequestTypes where RequestCode='" & FormCode & "'"
                RequestType = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, requesttypestr)

                txtRequestType.Text = RequestType.ToString()

                Dim strEmpName As String = "select " & EmpName & " as EmployeeName from hrs_Employees where ID=" & EmployeeID & ""
                Dim EmployeeName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, strEmpName)
            Catch ex As Exception
                mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
                Page.Session.Add("ErrorValue", ex)
                mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
                Page.Response.Redirect("ErrorPage.aspx")
            End Try
        Else
            Try

                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")
                Dim ClsEmployees As New Clshrs_Employees(Page)
                ClsEmployees.Find("Code='" & _sys_User.Code & "'")
                Dim DS1 As New Data.DataSet()
                Dim connetionString As String
                Dim connection As Data.SqlClient.SqlConnection
                Dim command As Data.SqlClient.SqlCommand
                Dim adapter As New Data.SqlClient.SqlDataAdapter
                connetionString = ClsEmployees.ConnectionString
                connection = New Data.SqlClient.SqlConnection(connetionString)
                Dim EmpName As String
                Dim ActionName As String

                If ProfileCls.CurrentLanguage = "Ar" Then
                    EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                    ActionName = " SS_UserActions.ActionAraName "
                Else
                    EmpName = "[dbo].[fn_GetEmpName](hrs_Employees.Code,0)"
                    ActionName = " SS_UserActions.ActionEngName "
                End If

                Dim str1 As String
                'str1 = "select " & EmpName & " as EmployeeName,( case when SS_UserActions.ActionAraName is not null then SS_UserActions.ActionAraName else 'Pending ...' end) As Action ,convert(varchar, ActionDate,101) as ActionDate,ActionRemarks  from SS_AdvanceHousingRequest join SS_RequestActions on SS_AdvanceHousingRequest.ID=SS_RequestActions.RequestSerial and SS_AdvanceHousingRequest.EmployeeID=SS_RequestActions.EmployeeID join hrs_Employees on hrs_Employees.ID= SS_RequestActions.SS_EmployeeID left join SS_UserActions on SS_RequestActions.ActionID=SS_UserActions.ID where RequestSerial=" & RequestSerial & " And SS_RequestActions.FormCode='" & FormCode & "' and ( SS_RequestActions.IsHidden is null or SS_RequestActions.IsHidden=0 ) and SS_AdvanceHousingRequest.EmployeeID=" & ClsEmployees.ID & " "
                str1 = "select " & EmpName & " as EmployeeName,( case when " & ActionName & " is not null then " & ActionName & " else 'Pending ...' end) As Action ,convert(varchar, ActionDate,101) as ActionDate,ActionRemarks  from SS_AdvanceHousingRequest join SS_RequestActions on SS_AdvanceHousingRequest.ID=SS_RequestActions.RequestSerial and SS_AdvanceHousingRequest.EmployeeID=SS_RequestActions.EmployeeID join hrs_Employees on hrs_Employees.ID= SS_RequestActions.SS_EmployeeID left join SS_UserActions on SS_RequestActions.ActionID=SS_UserActions.ID where RequestSerial=" & RequestSerial & " And SS_RequestActions.FormCode='" & FormCode & "' and ( SS_RequestActions.IsHidden is null or SS_RequestActions.IsHidden=0 ) "


                command = New Data.SqlClient.SqlCommand(str1, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS1, "Table1")
                adapter.Dispose()
                command.Dispose()
                connection.Close()
                Dim DataCol1 As Data.DataColumn
                DataCol1 = DS1.Tables(0).Columns(0)
                uwgEmployeeVacations.DataSource = Nothing
                uwgEmployeeVacations.DataBind()

                uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
                uwgEmployeeVacations.DataSource = DS1
                uwgEmployeeVacations.DataBind()




                Dim DS2 As New Data.DataSet()
                Dim connetionString2 As String
                Dim connection2 As Data.SqlClient.SqlConnection
                Dim command2 As Data.SqlClient.SqlCommand
                Dim adapter2 As New Data.SqlClient.SqlDataAdapter
                connetionString2 = ClsEmployees.ConnectionString
                connection2 = New Data.SqlClient.SqlConnection(connetionString)
                Dim strselect As String

                Dim firstshifName As String
                Dim secondshifName As String
                If ProfileCls.CurrentLanguage = "Ar" Then
                    firstshifName = "الوردية الاولى"
                    secondshifName = "الوردية الثانية"
                Else
                    firstshifName = "first shif"
                    secondshifName = "second shift"
                End If
                strselect = "select SS_AdvanceHousingRequest.Code as RequestSerial ,Convert(date,RequestDate) as RequestDate,Remarks from SS_AdvanceHousingRequest  where  SS_AdvanceHousingRequest.ID=" & RequestSerial & ""
                command2 = New Data.SqlClient.SqlCommand(strselect, connection2)
                adapter2.SelectCommand = command2
                adapter2.Fill(DS2, "Table1")
                adapter2.Dispose()
                command2.Dispose()
                connection2.Close()
                If DS2.Tables(0).Rows.Count > 0 Then


                    TxtRequestSerial.Text = DS2.Tables(0).Rows(0)("RequestSerial").ToString()
                    TxtRequestDate.Text = CDate(DS2.Tables(0).Rows(0)("RequestDate").ToString()).ToShortDateString()

                    TxtRemarks.Text = DS2.Tables(0).Rows(0)("Remarks").ToString()
                End If
                Dim SelectRequestType As String = ""
                Dim RequestType As String = ""
                If ProfileCls.CurrentLanguage = "Ar" Then
                    SelectRequestType = "RequestArbName"
                Else
                    SelectRequestType = "RequestEngName"

                End If
                'Fill Request type
                Dim strRequester As String = "select distinct Employeeid from SS_RequestActions where RequestSerial=" & RequestSerial & " and FormCode='" & FormCode & "'"
                Dim requesterid As Integer = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, strRequester))
                Dim RequesterData As String = "select Code," & EmpName & " as EmployeeName From hrs_Employees where ID=" & requesterid & ""
                Dim DsRequesterData As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(
        ClsEmployees.ConnectionString,
        CommandType.Text,
        RequesterData,
        New SqlClient.SqlParameter("@EmployeeID", requesterid)
    )
                If DsRequesterData.Tables(0).Rows.Count > 0 Then
                    TxtEmpCode.Text = DsRequesterData.Tables(0).Rows(0)("Code")
                    TxtEmpName.Text = DsRequesterData.Tables(0).Rows(0)("EmployeeName")
                End If
                Dim requesttypestr As String
                requesttypestr = "Select " & SelectRequestType & " from SS_RequestTypes where RequestCode='" & FormCode & "'"
                RequestType = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, requesttypestr)

                txtRequestType.Text = RequestType.ToString()
            Catch ex As Exception
                mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
                Page.Session.Add("ErrorValue", ex)
                mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
                Page.Response.Redirect("ErrorPage.aspx")
            End Try

        End If

    End Sub
    Private Sub GetEmployeeVacationRequest()


        Dim ConnectionString As String
            ConnectionString = ConfigurationManager.AppSettings("Connstring").ToString()

    End Sub

    Protected Sub CanelTequest_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnCancelRequest.Command
        Dim RequestSerial As Integer = Request.QueryString.Item("RequestSerial")
        Dim FormCode As String = Request.QueryString.Item("FormCode")
        CancelRequest(FormCode, RequestSerial)
        FillEmployeeVacations(ClsEmployees.ID, False)

    End Sub
    Private Sub CancelRequest(FormCode As String, RequestSerial As String)
        Dim ConfigID As Integer = 0
        Dim CanbeCanceled As Boolean = False


        '1-
        'Get the Current Pending Action
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " select ConfigID from SS_RequestActions where ActionSerial=(select max(Actionserial) from SS_RequestActions  where FormCode='" & FormCode & "' and RequestSerial=" & RequestSerial & "  and IsHidden is null) "
        Dim mSqlDataAdapter As New SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnStr)
        Try

            ConfigID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, mSelectCommand)

        Catch ex As Exception
        End Try
        '2-
        'CheckIf Request Can be Canceled with this ConfigID Or Not 
        If ConfigID > 0 Then
            Dim StrCheckCanbeCanceled As String = String.Empty
            StrCheckCanbeCanceled = " Select isnull(CanBeCanceledInThisLevel,0) From SS_Configuration where ID=" & ConfigID & ""

            Try
                CanbeCanceled = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, StrCheckCanbeCanceled)


            Catch ex As Exception
            End Try
        End If

        If CanbeCanceled Then
            Dim canceledbefor As Integer
            Dim mSelectCommand2 = " select count(Actionserial) from SS_RequestActions  where FormCode='" & FormCode & "' and RequestSerial=" & RequestSerial & " and ActionID =4 "
            Dim mSqlDataAdapter2 As New SqlClient.SqlDataAdapter
            Try

                canceledbefor = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, mSelectCommand2)
                If canceledbefor > 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This request Has been canceled Before./تم الغاء هذا الطلب من قبل "))
                    Exit Sub
                End If
            Catch ex As Exception

            End Try
            '3
            'Add New Record In SS_RequestAction
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Me)
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            Dim InsertCommand As String
            Dim SqlCommand As Data.SqlClient.SqlCommand

            InsertCommand = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ActionID,ActionDate)  values(" & RequestSerial & " , " & ClsEmployees.ID & ",'" & FormCode & "'," & ClsEmployees.ID & ",1,4,GetDate())"
            SqlCommand = New SqlClient.SqlCommand
            SqlCommand.Connection = New SqlClient.SqlConnection(ConnStr)
            SqlCommand.CommandType = CommandType.Text
            SqlCommand.CommandText = InsertCommand
            SqlCommand.Connection.Open()
            SqlCommand.ExecuteNonQuery()
            SqlCommand.Connection.Close()

            Dim SqlCommandRank As Data.SqlClient.SqlCommand
            Dim UpdateCommandRank As String = ""
            UpdateCommandRank = "UPDATE SS_AdvanceHousingRequest SET [RequestStautsTypeID] = 5 WHERE ID=" & RequestSerial & ""
            SqlCommandRank = New SqlClient.SqlCommand
            SqlCommandRank.Connection = New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
            SqlCommandRank.CommandType = CommandType.Text
            SqlCommandRank.CommandText = UpdateCommandRank
            SqlCommandRank.Connection.Open()
            SqlCommandRank.ExecuteNonQuery()
            SqlCommandRank.Connection.Close()
            '4- Mark Prvioues Actions as seen to prevent notifications to othes
            Dim UpdateCommand As String

            UpdateCommand = "update SS_RequestActions set seen=1 where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " And FormCode='" & FormCode & "'"
            SqlCommand = New SqlClient.SqlCommand
            SqlCommand.Connection = New SqlClient.SqlConnection(ConnStr)
            SqlCommand.CommandType = CommandType.Text
            SqlCommand.CommandText = UpdateCommand
            SqlCommand.Connection.Open()
            SqlCommand.ExecuteNonQuery()
            SqlCommand.Connection.Close()
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Your request Has been canceled Successfully./تم الغاء الطلب بنجاح "))

        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Sorry... Your request can't be canceled in this stage/عفوا... لايمكن الغاء الطلب في هذه المرحلة "))

        End If
    End Sub
    Private Function CheckEmpCode() As Boolean
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsNationality = New Clssys_Nationality(Page)
        Dim BolExist As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try
            SetTime()

        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function SavePart() As Boolean


        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Dim ClsEmployeesVacations2 = New Clshrs_EmployeesVacations(Page)

        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)
        Dim cls_Company = New Clssys_Companies(Page)
        Dim cls_Employee = New Clshrs_Employees(Page)

        Dim recordId As Integer
        Dim RemVal As Integer = 0
        Try

            Return True
        Catch ex As Exception
            Return False
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function



    Private Function GetValues(ByRef ClsEmployeesVacations As Clshrs_EmployeesVacations) As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ClsNationality As New ClsBasicFiles(Page, "sys_Nationalities")
        Dim ClsUser As New Clssys_Users(Page)
        Try

            SetToolBarDefaults()
            With ClsEmployeesVacations
                If ClsEmployees.Find("ID=" & .EmployeeID) Then
                    'txtEmployee.Text = ClsEmployees.Code
                    'lblDescEnglishName.Text = ClsEmployees.EnglishName
                    If Not IsNothing(ClsEmployees.NationalityID) Then
                        ClsNationality.Find("Id=" & ClsEmployees.NationalityID)
                    End If

                End If
                lbVactionID.Text = .ID.ToString()

                If Not .ActualEndDate.Year = 1 Then
                    'WebDateChooser1.Enabled = False
                    'WebDateChooser2.Enabled = False
                    'txtVactiondays.Enabled = False
                Else
                    'WebDateChooser1.Enabled = True
                    'WebDateChooser2.Enabled = True
                    'txtVactiondays.Enabled = True
                End If
                If Not .RegUserID = Nothing Then
                    ClsUser.Find("ID=" & .RegUserID)
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

                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                Else
                    ImageButton_Delete.Enabled = True
                End If
                Dim item As New System.Web.UI.WebControls.ListItem()
                Dim ClsVacType As New Clshrs_VacationsTypes(Page)
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(.ConnectionString)
                'ClsVacType.GetDropDownList(DdlVacationType)
                ClsVacType.Find(" ID= " & IIf(IsNothing(.VacationTypeID), 0, .VacationTypeID))
                If ClsVacType.ID > 0 Then
                    item.Value = .VacationTypeID
                    item.Text = ObjNavigationHandler.SetLanguage(Page, ClsVacType.EngName & "/" & ClsVacType.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Page, ClsVacType.ArbName & "/" & ClsVacType.EngName)
                    End If
                    'If Not DdlVacationType.Items.Contains(item) Then
                    '    DdlVacationType.Items.Add(item)
                    '    DdlVacationType.SelectedValue = item.Value
                    'Else
                    '    DdlVacationType.SelectedValue = .VacationTypeID
                    'End If
                End If

                Dim StrMode As String = String.Empty
                If (.ID > 0) Then
                    StrMode = "E"
                Else
                    StrMode = "N"
                End If
                SetToolBarPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, StrMode)
                SetToolBarRecordPermission(Me, ClsEmployeesVacations.ConnectionString, ClsEmployeesVacations.DataBaseUserRelatedID, ClsEmployeesVacations.GroupID, ClsEmployeesVacations.Table, .ID)
                If Not .CancelDate = Nothing Then
                    ImageButton_Delete.Enabled = False
                End If
                If Page.IsPostBack Then
                    CreateOtherFields(.ID)
                End If
            End With
            Return True
        Catch ex As Exception
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

                    If ImageButton_Delete.Enabled = True And .Item("CanDelete") = True Then
                        ImageButton_Delete.Enabled = Not .Item("CanDelete")
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
            Select Case ptrType
                Case "N", "R"


                    ImageButton_Delete.Enabled = False
                    ImageButton_Properties.Visible = False
                    LinkButton_Properties.Visible = False
                    ImageButton_Remarks.Visible = False
                    LinkButton_Remarks.Visible = False

                Case "D"
                    ClsEmployeesVacations.Find("ID=" & intID)
                    GetValues(ClsEmployeesVacations)

                    ImageButton_Save.Visible = False

                Case "E"
                    ClsEmployeesVacations.Find("ID=" & intID)
                    GetValues(ClsEmployeesVacations)

                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEmployeesVacations
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
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Me.Page)
        Try
            With ClsEmployeesVacations
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
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Me.Page)
        If IntId > 0 Then
            ClsEmployeesVacations.Find("ID=" & IntId)
            GetValues(ClsEmployeesVacations)
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Me.Page)
        'ClsEmployeesVacations.Find(" code = '" & txtEmployee.Text & "'")
        Dim recordID As Integer = ClsEmployeesVacations.ID
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
                'If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is TextBox) Then
                '    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                'ElseIf (TypeOf (currCtrl) Is TextBox) Then
                '    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                'ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then
                '    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                'End If
            Next
        End If
    End Sub
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployeesVacations.Table) = True Then
            Dim StrTablename As String
            ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
            StrTablename = ClsEmployeesVacations.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
                                    " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmRegions")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmRegions")
            End If
        End If
    End Function
    Public Function GetRecordInfoAjax(ByVal recordID As Integer) As Boolean
        Try
            Dim dsContractsTransactions As New DataSet
            Dim dsUser As New DataSet
            Dim clsUser As New Clssys_Users(Page)
            Dim clsContr As New Clshrs_ContractsTransactions(Page)
            clsContr.Find("ID=" & recordID)
            If clsContr.ID > 0 Then
                If Not clsContr.RegUserID = Nothing Then
                    clsUser.Find("ID=" & clsContr.RegUserID)
                    If clsUser.ID > 0 Then
                        lblRegUserValue.Text = clsUser.EngName
                    Else
                        lblRegUserValue.Text = ""
                    End If
                End If
                If Convert.ToDateTime(clsContr.RegDate).Date = Nothing Then
                    lblRegDateValue.Text = ""
                Else
                    lblRegDateValue.Text = clsContr.RegDate
                End If

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Shared Function"
    Public Shared Function Find(ByVal Table As String, ByVal Filter As String, ByRef DataSet As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " Select * From " & Table
        Dim mSqlDataAdapter As New SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where " & Filter & " And CancelDate IS Null", " Where CancelDate IS Null")
            StrSelectCommand = StrSelectCommand '& orderByStr
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, ConnStr)
            DataSet = New DataSet
            mSqlDataAdapter.Fill(DataSet)
            If DataSet.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function Contracts_ContractValidatoinId(ByVal EmployeeID As Integer, ByVal CurrentDate As Date) As Integer
        Dim dsContracts As New DataSet
        Dim DateNow As String
        If Not ClsDataAcessLayer.IsGreg(DateTime.Now.ToString("dd/MM/yyyy")) Then
            DateNow = ClsDataAcessLayer.FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        Else
            DateNow = DateTime.Now.ToString("dd/MM/yyyy")
        End If

        If Find("hrs_Contracts", "Employeeid=" & EmployeeID & "  And StartDate <= '" & DateNow & "' And (enddate is null or '" & DateNow & "' Between StartDate and EndDate)", dsContracts) Then
            Return dsContracts.Tables(0).Rows(0).Item("ID")
        Else
            Return 0
        End If
    End Function
    Public Shared Function Contracts_ContractValidatoinId(ByVal EmployeeID As Integer) As Integer
        Try
            Dim dsContracts As New DataSet
            Dim DateNow As String
            If Not ClsDataAcessLayer.IsGreg(DateTime.Now.ToString("dd/MM/yyyy")) Then
                DateNow = ClsDataAcessLayer.FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
            Else
                DateNow = DateTime.Now.ToString("dd/MM/yyyy")
            End If
            If Find("hrs_Contracts", "Employeeid=" & EmployeeID & "  And StartDate <= '" & DateNow & "' And (enddate is null or '" & DateNow & "' Between StartDate and EndDate)", dsContracts) Then
                Return dsContracts.Tables(0).Rows(0).Item("ID")
            Else
                Return 0
            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function GetEmployeeVacSum(ByVal intEmployeeID As Integer, ByVal intVacTypeID As Integer, ByVal dateStartDate As Date, ByVal dateEndDate As Date, ByVal intContractId As Integer) As Double
        Dim wHoursPerDay As Double = 0
        Dim dteActualStartDate As Date
        Dim dteactualEndDate As Date
        Dim dteTempActualStartDate As Date
        Dim dteTempActualEndDate As Date
        Dim dblDiffHours As Double
        Dim dblDiffDays As Double
        Dim dteStartTime As Date
        Dim dteEndTime As Date
        Dim dteThisStartDateStartTime As Date
        Dim dteThisStartDateEndTime As Date
        Dim dteThisEndDateStartTime As Date
        Dim dteThisEndDateEndTime As Date
        Dim dsEmpVac As New DataSet
        Dim dsContracts As New DataSet
        Dim dsEmployeesClasses As New DataSet
        Try
            Find("hrs_Contracts", " ID =" & intContractId, dsContracts)

            If Find("hrs_EmployeesClasses", "ID=" & IIf(dsContracts.Tables(0).Rows(0).Item("EmployeeClassID") > 0, dsContracts.Tables(0).Rows(0).Item("EmployeeClassID"), 0), dsEmployeesClasses) Then
                With dsEmployeesClasses.Tables(0).Rows(0)
                    If IsNothing(.Item("WorkHoursPerDay")) Then
                        wHoursPerDay = 9
                        dteStartTime = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 9, 0, 0)
                        dateStartDate = New Date(dateStartDate.Year, dateStartDate.Month, dateStartDate.Day, 9, 0, 0)
                        dteEndTime = dteStartTime.AddHours(wHoursPerDay)
                        dateEndDate = New Date(dateEndDate.Year, dateEndDate.Month, dateEndDate.Day, 9, 0, 0).AddHours(wHoursPerDay)
                    Else
                        wHoursPerDay = .Item("WorkHoursPerDay")
                        dteStartTime = .Item("DefultStartTime")
                        dateStartDate = New Date(dateStartDate.Year, dateStartDate.Month, dateStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, 0)
                        dteEndTime = CDate(.Item("DefultStartTime")).AddHours(wHoursPerDay)
                        dateEndDate = New Date(dateEndDate.Year, dateEndDate.Month, dateEndDate.Day, dteStartTime.Hour, dteStartTime.Minute, 0).AddHours(wHoursPerDay)
                    End If
                End With
            End If
            Dim NoOfWorkingHours As Double = wHoursPerDay
            Dim NoOfNonWorkingHours As Double = 24 - NoOfWorkingHours
            Find("hrs_EmployeesVacations", "EmployeeID=" & intEmployeeID & " And VacationTypeID=" & intVacTypeID & " And (ActualStartDate Between '" & dateStartDate & "' And '" & dateEndDate & "' OR IsNull(ActualEndDate,'" & IIf(Date.Now > dateEndDate, dateEndDate, Date.Now) & "') Between '" & dateStartDate & "' And '" & dateEndDate & "' OR  '" & dateStartDate & "' Between ActualStartDate AND IsNull(ActualEndDate,'" & IIf(Date.Now > dateEndDate, dateEndDate, Date.Now) & "') ) ", dsEmpVac)
            Dim vacDays As Double = 0
            For Each row As DataRow In dsEmpVac.Tables(0).Rows
                If IsDBNull(row("ActualStartDate")) Then
                    row("ActualStartDate") = CDate(Nothing)
                End If
                dteActualStartDate = IIf(CDate(row("ActualStartDate")) < dateStartDate, dateStartDate, row("ActualStartDate"))
                'dteactualEndDate = GetNDate_Shared(row("ActualEndDate"), IIf(Date.Now > dateEndDate, dateEndDate, Date.Now))
                dteactualEndDate = IIf(dteactualEndDate > dateEndDate, dateEndDate, dteactualEndDate)
                If dteactualEndDate < dteActualStartDate Then
                    Continue For
                End If
                dteThisStartDateStartTime = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                dteThisStartDateEndTime = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                dteThisEndDateStartTime = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                dteThisEndDateEndTime = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                dteTempActualStartDate = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                dteTempActualEndDate = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                If dteActualStartDate.Day = dteactualEndDate.Day And
                   dteActualStartDate.Month = dteactualEndDate.Month And
                   dteActualStartDate.Year = dteactualEndDate.Year And
                   ((dteActualStartDate > dteTempActualEndDate And dteactualEndDate > dteTempActualEndDate) Or
                     (dteActualStartDate < dteTempActualStartDate And dteactualEndDate < dteTempActualStartDate)) Then
                    Continue For
                End If
                If DateDiff(DateInterval.Day, dteActualStartDate, dteactualEndDate) = 0 And
                    (dteActualStartDate >= dteThisStartDateEndTime And dteactualEndDate <= dteThisEndDateStartTime) Then
                    Continue For
                End If
                If dteActualStartDate < dteTempActualStartDate Then
                    dteActualStartDate = dteTempActualStartDate
                ElseIf dteActualStartDate > dteTempActualStartDate And dteActualStartDate > dteThisStartDateEndTime Then
                    Dim dteActualStartDatePlusDay As Date = dteActualStartDate.AddDays(1)
                    dteActualStartDate = New Date(dteActualStartDatePlusDay.Year, dteActualStartDatePlusDay.Month, dteActualStartDatePlusDay.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
                End If
                If dteactualEndDate > dteTempActualEndDate Then
                    dteactualEndDate = dteTempActualEndDate
                ElseIf dteactualEndDate < dteTempActualEndDate And dteactualEndDate < dteThisEndDateStartTime Then
                    Dim dteActualEndDateMiunsDay As Date = dteactualEndDate.AddDays(-1)
                    dteactualEndDate = New Date(dteActualEndDateMiunsDay.Year, dteActualEndDateMiunsDay.Month, dteActualEndDateMiunsDay.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
                End If
                dblDiffHours = DateDiff(DateInterval.Minute, dteActualStartDate, dteactualEndDate) / 60
                dblDiffDays = DateDiff(DateInterval.Day, dteActualStartDate, dteactualEndDate)
                vacDays += (dblDiffHours - (dblDiffDays * NoOfNonWorkingHours)) / NoOfWorkingHours
            Next
            Return vacDays
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function Contract_GetDurationDaysForPeriod(ByVal intContractID As Integer, ByVal intVacType As Integer, ByVal dteStartDate As DateTime) As DataSet
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(CType(HttpContext.Current.Session("ConnectionString"), String), "GetDurationDaysForPeriod", intContractID, intVacType, dteStartDate)
    End Function
    Public Shared Function GetFieldDescription(ByVal StrCode As String, ByVal StrTableName As String) As String
        Dim StrReturnData As Object
        Try
            StrReturnData = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, " Select EngName + '/' + ArbName From " & StrTableName & " Where Code = '" & StrCode.ToString.TrimStart.TrimEnd & "'")
            If IsNothing(StrReturnData) Then Return "/"
            If IsDBNull(StrReturnData) Then Return "/"
            Return StrReturnData
        Catch ex As Exception
            Return "/"
        End Try
    End Function

#End Region

#Region "PageMethods"
    <System.Web.Services.WebMethod()>
    Public Shared Function GetEmployeeID(ByVal strEmpCode As String) As String
        Dim dsEmp As New DataSet
        If Find("hrs_Employees", "Code='" & strEmpCode & "'", dsEmp) Then
            Return dsEmp.Tables(0).Rows(0).Item("ID").ToString
        Else
            Return "0"
        End If
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function CheckEmployeeContract(ByVal IntEmployeeId As Integer, ByVal dteDateToCheck As Date) As String
        If Contracts_ContractValidatoinId(IntEmployeeId, dteDateToCheck) > 0 Then
            Return "1"
        Else
            Return "0"
        End If
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function RetTable(ByVal strTableName As String) As Object
        Dim tbl As New Data.DataTable(strTableName)
        tbl.Columns.Add(New Data.DataColumn("EmpID", GetType(Integer)))
        tbl.Columns.Add(New Data.DataColumn("EmpName", GetType(String)))
        For i As Int16 = 1 To 4
            Dim nRow As Data.DataRow = tbl.NewRow()
            nRow(0) = i
            nRow(1) = "Employee " + i.ToString()
            tbl.Rows.Add(nRow)
        Next
        Return tbl
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function Get_Searched_Description(ByVal IntSearchId As Integer, ByVal strCode As String) As String
        Dim dsSearchs As New Data.DataSet
        Find("sys_Searchs", " sys_Searchs.Id = " & IntSearchId, dsSearchs)
        Dim dsObjects As New Data.DataSet
        Find("sys_Objects", " sys_Objects.Id = " & dsSearchs.Tables(0).Rows(0).Item("ObjectID"), dsObjects)
        Return GetFieldDescription(strCode, dsObjects.Tables(0).Rows(0).Item("Code"))
    End Function
#End Region

    Private Sub LinkButton_Remarks_Load(sender As Object, e As EventArgs) Handles LinkButton_Remarks.Load

    End Sub
End Class
