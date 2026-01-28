Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Venus.Shared.Web
Imports System.Security.Cryptography
Imports System.Collections.ObjectModel
Imports System.Activities.Expressions
Imports C1.Web.Wijmo.Controls

Partial Class frmEmployeesVacations
    Inherits MainPage
#Region "Public Decleration"
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees
    Dim ClsUsers As Clssys_Users
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private mErrorHandler As Venus.Shared.ErrorsHandler
    Const csOtherFields = 11
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim recordID As Integer
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)

        If Not IsPostBack Then
            Try

                Dim WebHandler As New Venus.Shared.Web.WebHandler
                Dim User As String = String.Empty
                ClsEmployees = New Clshrs_Employees(Page)
                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")
                FillDdlRequestTypes()
                ddlFromDate.Value = ClsEmployeesVacations.GetHigriDate(DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy"))
                ddlToDate.Value = ClsEmployeesVacations.GetHigriDate(DateTime.Now.ToString("dd/MM/yyyy"))


                FillStatus()
                GetAllEmployeeRequests()
                Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
                ClsEmployeesVacations.AddNotificationOnChange(Page)
                'Dim csSearchID As Integer
                'Dim ClsLevels As New Clshrs_LevelTypes(Page)
                'Dim ClsDataHandler As New Venus.Shared.DataHandler
                'Dim StrSerial As String = String.Empty
                'Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                'Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
                'Dim ClsObjects As New Clssys_Objects(Page)
                'Dim ClsSearchs As New Clssys_Searchs(Page)
                'Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                'lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
                'Page.Session.Add("Lage", lblLage.Text)
                'Dim IntDimension As Integer = 510
                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
                Dim ClsObjects As New Clssys_Objects(Page)
                Dim ClsSearchs As New Clssys_Searchs(Page)
                Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
                ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'")
                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                Dim csSearchID As Integer
                csSearchID = ClsSearchs.ID
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmpCode.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtEmpCode.ClientID & "'"
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
            Catch ex As Exception
                mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
                Page.Session.Add("ErrorValue", ex)
                mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
                Page.Response.Redirect("ErrorPage.aspx")
            End Try

        End If



    End Sub
    Protected Sub DdlRequestType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlRequestType.SelectedIndexChanged, ddlFromDate.ValueChanged, ddlToDate.ValueChanged
        FillEmployeeVacations()
        FillEmployeeRequestsByStatusNew()

    End Sub
    Protected Sub txtEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmpCode.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtEmpCode.Text) Then
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
                strselect = "select " & EmpName & "  FROM  Hrs_Employees where Code='" & txtEmpCode.Text & "'"
                'command2 = New Data.SqlClient.SqlCommand(strselect, connection2)

                Dim EmployeeName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(CType(HttpContext.Current.Session("ConnectionString"), String), Data.CommandType.Text, strselect)
                ClsEmployees.Find("Code='" & txtEmpCode.Text & "'")
                If ClsEmployees.ID > 0 Then

                    TxtEmpName.Text = EmployeeName

                Else
                    TxtEmpName.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Sorry there is no employee with this code !/!عفوا لا يوجد موظف مسجل بهذا الكود"))

                End If
            Else
                TxtEmpName.Text = ""
            End If
            FillEmployeeVacations()
            FillEmployeeRequestsByStatusNew()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        FillEmployeeRequestsByStatusNew()

    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Delete.Command
        Dim IntId As Integer
        Dim strMode As String
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        ClsEmployees = New Clshrs_Employees(Page)
        Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)
        'If uwgEmployeeVacations.Rows.Count > 0 Then
        '    IntId = uwgEmployeeVacations.Rows(0).Cells.FromKey("ID").Value
        '    strMode = "E"
        'Else
        '    strMode = "N"
        '    IntId = 0
        'End If

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
    Public Shared Function GetNDate_Shared(ByVal date1 As Object, ByVal dteDefault As Date) As Date
        If IsDBNull(date1) Then
            Return dteDefault
        Else
            Return CDate(date1)
        End If
    End Function

    Private Sub FillEmployeeRequestsByStatus()
        Try
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Me)
            'ClsEmployees.Find("Code='" & _sys_User.Code & "'")
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
            Dim vacationName0 As String = ""
            Dim vacationName As String = ""
            Dim vacationName2 As String = ""
            Dim permName As String = ""
            Dim ServiceName As String = ""
            Dim mServiceName As String = ""

            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                vacationName0 = "اجازة سنوي اداري"
                vacationName = "اجازة سنوي طبي"
                vacationName2 = "hrs_VacationsTypes.ArbName4S"
                permName = "طلب استئذان"
                ServiceName = "طلب انهاء خدمة"
                mServiceName = "طلب انهاء خدمة طبي"
            Else
                EmpName = "[dbo].[fn_GetEmpName](hrs_Employees.Code,0)"
                vacationName0 = "Annual administrative Vacation"
                vacationName = "Annual medical Vacation"
                vacationName2 = "hrs_VacationsTypes.EngName"
                permName = "Execuse Request"
                ServiceName = "Request for termination of service"
                mServiceName = "Request for medical termination of service"
            End If
            Dim str1 As String = ""
            Dim Where As String = ""
            _sys_User.Find("ID = '" & User & "'")
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            If ClsEmployees.ID > 0 Then
                Where &= " And SS_RequestActions.EmployeeID in (select ID from hrs_employees where ManagerID='" & ClsEmployees.ID & "')"

            End If

            If Not String.IsNullOrEmpty(txtEmpCode.Text) Then
                ClsEmployees.Find("Code='" & txtEmpCode.Text & "'")
                Where = " And EmployeeID= '" & ClsEmployees.ID & "'"
            End If


            If ddlStatus.SelectedValue = 2 Then  'رفض
                If DdlRequestType.SelectedValue.Trim() = "SS_0011" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=2 and VacationType='SS_0011' " & Where & " Order By SS_VacationRequest.RequestDate desc"

                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0012" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=2 and VacationType='SS_0012' " & Where & " Order By SS_VacationRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0013" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)   and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=2 and VacationType='SS_0013' " & Where & " Order By SS_VacationRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0014" Then
                    str1 = "select Distinct(SS_ExecuseRequest.ID),'4' as VacationType,SS_ExecuseRequest.Code as RequestSerial ,SS_ExecuseRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_ExecuseRequest.RequestDate,'', '" & permName & "'  as RequestType ,'SS_0014' as FormCode from SS_ExecuseRequest join hrs_Employees on SS_ExecuseRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_ExecuseRequest.ID and (SS_RequestActions.FormCode='SS_0014') where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=2  " & Where & " Order By SS_ExecuseRequest.RequestDate desc"



                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0015" Then
                    str1 = "select SS_EndOfServiceRequest.ID,'5' as VacationType,SS_EndOfServiceRequest.Code as RequestSerial ,SS_EndOfServiceRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_EndOfServiceRequest.RequestDate,'', case when SS_EndOfServiceRequest.FormCode='SS_0015' then '" & ServiceName & "' else '" & mServiceName & "' end  as RequestType ,SS_EndOfServiceRequest.FormCode as FormCode from SS_EndOfServiceRequest join hrs_Employees on SS_EndOfServiceRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_EndOfServiceRequest.ID and (SS_RequestActions.FormCode='SS_0015')  where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=2 " & Where & " Order By SS_EndOfServiceRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue = 0 Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=2 " & Where & " union select SS_ExecuseRequest.ID,'4' as VacationType,SS_ExecuseRequest.Code as RequestSerial ,SS_ExecuseRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_ExecuseRequest.RequestDate,'', '" & permName & "'  as RequestType ,'SS_0014' as FormCode from SS_ExecuseRequest join hrs_Employees on SS_ExecuseRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_ExecuseRequest.ID and (SS_RequestActions.FormCode='SS_0014') where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=2  " & Where & " union select SS_EndOfServiceRequest.ID,'5' as VacationType,SS_EndOfServiceRequest.Code as RequestSerial ,SS_EndOfServiceRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_EndOfServiceRequest.RequestDate,'', case when SS_EndOfServiceRequest.FormCode='SS_0015' then '" & ServiceName & "' else '" & mServiceName & "' end  as RequestType ,SS_EndOfServiceRequest.FormCode as FormCode from SS_EndOfServiceRequest join hrs_Employees on SS_EndOfServiceRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_EndOfServiceRequest.ID and (SS_RequestActions.FormCode='SS_0015')  where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=2 " & Where & " Order By SS_VacationRequest.RequestDate desc"
                End If
            ElseIf ddlStatus.SelectedValue = 1 Then  'موافقة    
                If DdlRequestType.SelectedValue.Trim() = "SS_0011" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where  VacationType='SS_0011' and Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_VacationRequest.ID not in (select RequestSerial from SS_RequestActions where FormCode='SS_0011'  and( ActionID =2 or ActionID is null)) " & Where & " Order By SS_VacationRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0012" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where  VacationType='SS_0012' and Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_VacationRequest.ID not in (select RequestSerial from SS_RequestActions where FormCode='SS_0012'  and( ActionID =2 or ActionID is null)) " & Where & " Order By SS_VacationRequest.RequestDate desc"

                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0013" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0013' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0013' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where  VacationType='SS_0012' and Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_VacationRequest.ID not in (select RequestSerial from SS_RequestActions where FormCode='SS_0012'  and( ActionID =2 or ActionID is null)) " & Where & " Order By SS_VacationRequest.RequestDate desc"

                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0014" Then
                    str1 = " select distinct(SS_ExecuseRequest.ID),'4' as VacationType,SS_ExecuseRequest.Code as RequestSerial ,SS_ExecuseRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_ExecuseRequest.RequestDate,'', '" & permName & "'  as RequestType ,'SS_0014' as FormCode from SS_ExecuseRequest join hrs_Employees on SS_ExecuseRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_ExecuseRequest.ID and (SS_RequestActions.FormCode='SS_0014') where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_ExecuseRequest.ID not in (select RequestSerial from SS_RequestActions where FormCode='SS_0014'  and( ActionID =2 or ActionID is null)) " & Where & "  Order By SS_ExecuseRequest.RequestDate desc"

                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0015" Then
                    str1 = "select distinct(SS_EndOfServiceRequest.ID),'5' as VacationType,SS_EndOfServiceRequest.Code as RequestSerial ,SS_EndOfServiceRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_EndOfServiceRequest.RequestDate,'', case when SS_EndOfServiceRequest.FormCode='SS_0015' then '" & ServiceName & "' else '" & mServiceName & "' end  as RequestType ,SS_EndOfServiceRequest.FormCode as FormCode from SS_EndOfServiceRequest join hrs_Employees on SS_EndOfServiceRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_EndOfServiceRequest.ID and (SS_RequestActions.FormCode='SS_0015')  where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_EndOfServiceRequest.ID not in (select RequestSerial from SS_RequestActions where FormCode='SS_0015'  and( ActionID =2 or ActionID is null)) " & Where & " Order By SS_EndOfServiceRequest.RequestDate desc"

                ElseIf DdlRequestType.SelectedValue = 0 Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_VacationRequest.ID not in (select RequestSerial from SS_RequestActions where (FormCode='SS_0011' or FormCode='SS_0012' or FormCode='SS_0013') and( ActionID =2 or ActionID is null)) " & Where & " union select distinct(SS_ExecuseRequest.ID),'4' as VacationType,SS_ExecuseRequest.Code as RequestSerial ,SS_ExecuseRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_ExecuseRequest.RequestDate,'', '" & permName & "'  as RequestType ,'SS_0014' as FormCode from SS_ExecuseRequest join hrs_Employees on SS_ExecuseRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_ExecuseRequest.ID and (SS_RequestActions.FormCode='SS_0014') where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_ExecuseRequest.ID not in (select RequestSerial from SS_RequestActions where FormCode='SS_0014'  and( ActionID =2 or ActionID is null)) " & Where & " union select distinct(SS_EndOfServiceRequest.ID),'5' as VacationType,SS_EndOfServiceRequest.Code as RequestSerial ,SS_EndOfServiceRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_EndOfServiceRequest.RequestDate,'', case when SS_EndOfServiceRequest.FormCode='SS_0015' then '" & ServiceName & "' else '" & mServiceName & "' end  as RequestType ,SS_EndOfServiceRequest.FormCode as FormCode from SS_EndOfServiceRequest join hrs_Employees on SS_EndOfServiceRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_EndOfServiceRequest.ID and (SS_RequestActions.FormCode='SS_0015')  where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_EndOfServiceRequest.ID not in (select RequestSerial from SS_RequestActions where FormCode='SS_0015'   and( ActionID =2 or ActionID is null)) " & Where & " Order By SS_VacationRequest.RequestDate desc"

                End If
            ElseIf ddlStatus.SelectedValue = 4 Then 'تحت التنفيذ
                If DdlRequestType.SelectedValue.Trim() = "SS_0011" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where FormCode='SS_0011' And Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID =1  and SS_VacationRequest.ID  in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1    and ActionID is null and (SS_RequestActions.FormCode='SS_0011' )) and  SS_VacationRequest.ID  not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID having count (RequestSerial) =1    and  ActionID=2) and (SS_RequestActions.FormCode='SS_0011') " & Where & " Order By SS_VacationRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0012" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where FormCode='SS_0012' And Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)   and SS_RequestActions.ActionID =1  and SS_VacationRequest.ID  in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1    and ActionID is null and (SS_RequestActions.FormCode='SS_0012')) and  SS_VacationRequest.ID  not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID having count (RequestSerial) =1    and  ActionID=2) and (SS_RequestActions.FormCode='SS_0012') " & Where & " Order By SS_VacationRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0013" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where FormCode='SS_0013' And Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)   and SS_RequestActions.ActionID =1  and SS_VacationRequest.ID  in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1    and ActionID is null and (SS_RequestActions.FormCode='SS_0013')) and  SS_VacationRequest.ID  not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID having count (RequestSerial) =1    and  ActionID=2) and (SS_RequestActions.FormCode='SS_0013') " & Where & " Order By SS_VacationRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0014" Then
                    str1 = " select distinct(SS_ExecuseRequest.ID),'4' as VacationType,SS_ExecuseRequest.Code as RequestSerial ,SS_ExecuseRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_ExecuseRequest.RequestDate,'', '" & permName & "'  as RequestType ,'SS_0014' as FormCode from SS_ExecuseRequest join hrs_Employees on SS_ExecuseRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_ExecuseRequest.ID and (SS_RequestActions.FormCode='SS_0014') where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_ExecuseRequest.ID  in  (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1   and ActionID is null and (SS_RequestActions.FormCode='SS_0014'))  and  SS_ExecuseRequest.ID  not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1    and  ActionID=2 and (SS_RequestActions.FormCode='SS_0014') ) " & Where & " Order By SS_ExecuseRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0015" Then
                    str1 = "select distinct(SS_EndOfServiceRequest.ID),'5' as VacationType,SS_EndOfServiceRequest.Code as RequestSerial ,SS_EndOfServiceRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_EndOfServiceRequest.RequestDate,'', case when SS_EndOfServiceRequest.FormCode='SS_0015' then '" & ServiceName & "' else '" & mServiceName & "' end  as RequestType ,SS_EndOfServiceRequest.FormCode as FormCode from SS_EndOfServiceRequest join hrs_Employees on SS_EndOfServiceRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_EndOfServiceRequest.ID and (SS_RequestActions.FormCode='SS_0015')  where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_EndOfServiceRequest.ID in  (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID having count (RequestSerial) =1  and ActionID is null) and  SS_EndOfServiceRequest.ID  not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1    and  ActionID=2  and (SS_RequestActions.FormCode='SS_0015') ) " & Where & "  Order By SS_EndOfServiceRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue = 0 Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where (SS_RequestActions.FormCode='SS_0011' Or SS_RequestActions.FormCode='SS_0012' Or  FormCode='SS_0013') And Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)   and SS_RequestActions.ActionID =1  and SS_VacationRequest.ID  in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1    and ActionID is null and  (SS_RequestActions.FormCode='SS_0011' Or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013')) and  SS_VacationRequest.ID  not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1    and  ActionID=2 and (SS_RequestActions.FormCode='SS_0011' Or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013')) " & Where & " union select distinct(SS_ExecuseRequest.ID),'4' as VacationType,SS_ExecuseRequest.Code as RequestSerial ,SS_ExecuseRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_ExecuseRequest.RequestDate,'', '" & permName & "'  as RequestType ,'SS_0014' as FormCode from SS_ExecuseRequest join hrs_Employees on SS_ExecuseRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_ExecuseRequest.ID and (SS_RequestActions.FormCode='SS_0014') where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_ExecuseRequest.ID  in  (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1   and ActionID is null and (SS_RequestActions.FormCode='SS_0014'))  and  SS_ExecuseRequest.ID  not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1    and  ActionID=2 and (SS_RequestActions.FormCode='SS_0014') ) " & Where & " union select distinct(SS_EndOfServiceRequest.ID),'5' as VacationType,SS_EndOfServiceRequest.Code as RequestSerial ,SS_EndOfServiceRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_EndOfServiceRequest.RequestDate,'', case when SS_EndOfServiceRequest.FormCode='SS_0015' then '" & ServiceName & "' else '" & mServiceName & "' end  as RequestType ,SS_EndOfServiceRequest.FormCode as FormCode from SS_EndOfServiceRequest join hrs_Employees on SS_EndOfServiceRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_EndOfServiceRequest.ID and (SS_RequestActions.FormCode='SS_0015')  where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is not null and SS_RequestActions.ActionID=1 and SS_EndOfServiceRequest.ID in  (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1  and ActionID is null and (SS_RequestActions.FormCode='SS_0015') ) and  SS_EndOfServiceRequest.ID  not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID,FormCode having count (RequestSerial) =1    and  ActionID=2  and (SS_RequestActions.FormCode='SS_0015') ) " & Where & " Order By SS_VacationRequest.RequestDate desc"
                End If

            ElseIf ddlStatus.SelectedValue = 3 Then 'جديد
                If DdlRequestType.SelectedValue.Trim() = "SS_0011" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where FormCode='SS_0011' And Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is null  and SS_VacationRequest.ID not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID having count (RequestSerial) >1  )  " & Where & " Order By SS_VacationRequest.RequestDate desc"



                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0012" Then

                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where FormCode='SS_0012' And Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is null  and SS_VacationRequest.ID not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID having count (RequestSerial) >1 ) " & Where & " Order By SS_VacationRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0013" Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where FormCode='SS_0013' And Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID is null  and SS_VacationRequest.ID not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID having count (RequestSerial) >1  ) " & Where & " Order By SS_VacationRequest.RequestDate desc"



                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0014" Then

                    str1 = "select distinct(SS_ExecuseRequest.ID),'4' as VacationType,SS_ExecuseRequest.Code as RequestSerial ,SS_ExecuseRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_ExecuseRequest.RequestDate,'', '" & permName & "'  as RequestType ,'SS_0014' as FormCode from SS_ExecuseRequest join hrs_Employees on SS_ExecuseRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_ExecuseRequest.ID and (SS_RequestActions.FormCode='SS_0014') where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is  null  and SS_ExecuseRequest.ID  in  (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID,ActionID having count (RequestSerial) =1 and ActionID is null  ) " & Where & " Order By SS_ExecuseRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue.Trim() = "SS_0015" Then

                    str1 = "  select distinct(SS_EndOfServiceRequest.ID),'5' as VacationType,SS_EndOfServiceRequest.Code as RequestSerial ,SS_EndOfServiceRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_EndOfServiceRequest.RequestDate,'', case when SS_EndOfServiceRequest.FormCode='SS_0015' then '" & ServiceName & "' else '" & mServiceName & "' end  as RequestType ,SS_EndOfServiceRequest.FormCode as FormCode from SS_EndOfServiceRequest join hrs_Employees on SS_EndOfServiceRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_EndOfServiceRequest.ID and (SS_RequestActions.FormCode='SS_0015')  where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is null  and SS_EndOfServiceRequest.ID not in  (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID having count (RequestSerial) >1 ) " & Where & " Order By SS_EndOfServiceRequest.RequestDate desc"


                ElseIf DdlRequestType.SelectedValue = 0 Then
                    str1 = "select distinct(SS_VacationRequest.ID),SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_RequestActions.RequestSerial= SS_VacationRequest.ID and (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode='SS_0013') where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is null  and SS_VacationRequest.ID not in (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID having count (RequestSerial) >1  ) " & Where & " union select distinct(SS_ExecuseRequest.ID),'4' as VacationType,SS_ExecuseRequest.Code as RequestSerial ,SS_ExecuseRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_ExecuseRequest.RequestDate,'', '" & permName & "'  as RequestType ,'SS_0014' as FormCode from SS_ExecuseRequest join hrs_Employees on SS_ExecuseRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_ExecuseRequest.ID and (SS_RequestActions.FormCode='SS_0014') where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)  and SS_RequestActions.ActionID is  null  and SS_ExecuseRequest.ID  not in  (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID having count (RequestSerial) >1   ) " & Where & "  union select distinct(SS_EndOfServiceRequest.ID),'5' as VacationType,SS_EndOfServiceRequest.Code as RequestSerial ,SS_EndOfServiceRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_EndOfServiceRequest.RequestDate,'', case when SS_EndOfServiceRequest.FormCode='SS_0015' then '" & ServiceName & "' else '" & mServiceName & "' end  as RequestType ,SS_EndOfServiceRequest.FormCode as FormCode from SS_EndOfServiceRequest join hrs_Employees on SS_EndOfServiceRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_RequestActions.RequestSerial= SS_EndOfServiceRequest.ID and (SS_RequestActions.FormCode='SS_0015')  where Requestdate>= convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103) and SS_RequestActions.ActionID is null  and SS_EndOfServiceRequest.ID not in  (select  RequestSerial from SS_RequestActions group by RequestSerial,EmployeeID having count (RequestSerial) >1 ) " & Where & " Order By SS_VacationRequest.RequestDate desc"
                End If
            ElseIf ddlStatus.SelectedValue = 0 Then 'برجاء الاختيار
                FillEmployeeVacations()

            End If
            If str1 <> "" Then
                command = New Data.SqlClient.SqlCommand(str1, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS1, "Table1")

                connection.Close()
                uwgEmployeeVacations.DataSource = Nothing
                uwgEmployeeVacations.DataBind()

                uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
                uwgEmployeeVacations.DataSource = DS1
                uwgEmployeeVacations.DataBind()

                If uwgEmployeeVacations.Rows.Count > 0 Then

                    FillRowColors()

                End If
            Else
                If ddlStatus.SelectedValue > 0 Then
                    uwgEmployeeVacations.DataSource = Nothing
                    uwgEmployeeVacations.DataBind()
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FillEmployeeRequestsByStatusNew()
        Try
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Me)
            'ClsEmployees.Find("Code='" & _sys_User.Code & "'")
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
            Dim RequestType As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " EmployeeArbName  "
                RequestType = "RequestArbName "

            Else
                EmpName = " EmployeeEngName "
                RequestType = "RequestEngName "

            End If





            Dim str1 As String = ""
            Dim Where As String = ""
            _sys_User.Find("ID = '" & User & "'")
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            If ClsEmployees.ID > 0 Then
                Where &= " And SS_VFollowup.EmployeeID in (select ID from hrs_employees where ManagerID='" & ClsEmployees.ID & "')"

            End If

            If Not String.IsNullOrEmpty(txtEmpCode.Text) Then
                ClsEmployees.Find("Code='" & txtEmpCode.Text & "'")
                Where = " And EmployeeID= '" & ClsEmployees.ID & "'"
            End If


            If ddlStatus.SelectedValue = 2 Then  'رفض
                If DdlRequestType.SelectedIndex <> 0 Then
                    str1 = "select distinct(SS_VFollowup.ID),SS_VFollowup.FormCode as VacationType ,SS_VFollowup.RequestSerial as RequestSerial ,SS_VFollowup.EmployeeID ," & EmpName & " as EmployeeName,SS_VFollowup.RequestDate,SS_VFollowup.VacationType ," & RequestType & " as RequestType ,SS_VFollowup.FormCode as FormCode from SS_VFollowup where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and RequestStautsTypeID is not null and RequestStautsTypeID=2 and FormCode='" & DdlRequestType.SelectedValue.Trim() & "' " & Where & " Order By SS_VFollowup.RequestDate desc"
                Else
                    str1 = "select distinct(SS_VFollowup.ID),SS_VFollowup.FormCode as VacationType ,SS_VFollowup.RequestSerial as RequestSerial ,SS_VFollowup.EmployeeID ," & EmpName & " as EmployeeName,SS_VFollowup.RequestDate,SS_VFollowup.VacationType ," & RequestType & " as RequestType ,SS_VFollowup.FormCode as FormCode from SS_VFollowup where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and RequestStautsTypeID is not null and RequestStautsTypeID=2 " & Where & " Order By SS_VFollowup.RequestDate desc"
                End If

            ElseIf ddlStatus.SelectedValue = 1 Then  'موافقة    
                If DdlRequestType.SelectedIndex <> 0 Then
                    str1 = "select distinct(SS_VFollowup.ID),SS_VFollowup.FormCode as VacationType ,SS_VFollowup.RequestSerial as RequestSerial ,SS_VFollowup.EmployeeID ," & EmpName & " as EmployeeName,SS_VFollowup.RequestDate,SS_VFollowup.VacationType ," & RequestType & " as RequestType ,SS_VFollowup.FormCode as FormCode from SS_VFollowup where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and RequestStautsTypeID is not null and RequestStautsTypeID=1 and FormCode='" & DdlRequestType.SelectedValue.Trim() & "' " & Where & " Order By SS_VFollowup.RequestDate desc"
                Else
                    str1 = "select distinct(SS_VFollowup.ID),SS_VFollowup.FormCode as VacationType ,SS_VFollowup.RequestSerial as RequestSerial ,SS_VFollowup.EmployeeID ," & EmpName & " as EmployeeName,SS_VFollowup.RequestDate,SS_VFollowup.VacationType ," & RequestType & " as RequestType ,SS_VFollowup.FormCode as FormCode from SS_VFollowup where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and RequestStautsTypeID is not null and RequestStautsTypeID=1 " & Where & " Order By SS_VFollowup.RequestDate desc"
                End If
            ElseIf ddlStatus.SelectedValue = 4 Then 'تحت التنفيذ
                If DdlRequestType.SelectedIndex <> 0 Then
                    str1 = "select distinct(SS_VFollowup.ID),SS_VFollowup.FormCode as VacationType ,SS_VFollowup.RequestSerial as RequestSerial ,SS_VFollowup.EmployeeID ," & EmpName & " as EmployeeName,SS_VFollowup.RequestDate,SS_VFollowup.VacationType ," & RequestType & " as RequestType ,SS_VFollowup.FormCode as FormCode from SS_VFollowup where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and RequestStautsTypeID is not null and RequestStautsTypeID=4 and FormCode='" & DdlRequestType.SelectedValue.Trim() & "' " & Where & " Order By SS_VFollowup.RequestDate desc"
                Else
                    str1 = "select distinct(SS_VFollowup.ID),SS_VFollowup.FormCode as VacationType ,SS_VFollowup.RequestSerial as RequestSerial ,SS_VFollowup.EmployeeID ," & EmpName & " as EmployeeName,SS_VFollowup.RequestDate,SS_VFollowup.VacationType ," & RequestType & " as RequestType ,SS_VFollowup.FormCode as FormCode from SS_VFollowup where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and RequestStautsTypeID is not null and RequestStautsTypeID=4 " & Where & " Order By SS_VFollowup.RequestDate desc"
                End If

            ElseIf ddlStatus.SelectedValue = 3 Then 'جديد
                If DdlRequestType.SelectedIndex <> 0 Then
                    str1 = "select distinct(SS_VFollowup.ID),SS_VFollowup.FormCode as VacationType ,SS_VFollowup.RequestSerial as RequestSerial ,SS_VFollowup.EmployeeID ," & EmpName & " as EmployeeName,SS_VFollowup.RequestDate,SS_VFollowup.VacationType ," & RequestType & " as RequestType ,SS_VFollowup.FormCode as FormCode from SS_VFollowup where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and RequestStautsTypeID is not null and RequestStautsTypeID=3 and FormCode='" & DdlRequestType.SelectedValue.Trim() & "' " & Where & " Order By SS_VFollowup.RequestDate desc"
                Else
                    str1 = "select distinct(SS_VFollowup.ID),SS_VFollowup.FormCode as VacationType ,SS_VFollowup.RequestSerial as RequestSerial ,SS_VFollowup.EmployeeID ," & EmpName & " as EmployeeName,SS_VFollowup.RequestDate,SS_VFollowup.VacationType ," & RequestType & " as RequestType ,SS_VFollowup.FormCode as FormCode from SS_VFollowup where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and RequestStautsTypeID is not null and RequestStautsTypeID=3 " & Where & " Order By SS_VFollowup.RequestDate desc"
                End If
            ElseIf ddlStatus.SelectedValue = 5 Then 'ملغي
                If DdlRequestType.SelectedIndex <> 0 Then
                    str1 = "select distinct(SS_VFollowup.ID),SS_VFollowup.FormCode as VacationType ,SS_VFollowup.RequestSerial as RequestSerial ,SS_VFollowup.EmployeeID ," & EmpName & " as EmployeeName,SS_VFollowup.RequestDate,SS_VFollowup.VacationType ," & RequestType & " as RequestType ,SS_VFollowup.FormCode as FormCode from SS_VFollowup where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and RequestStautsTypeID is not null and RequestStautsTypeID=5 and FormCode='" & DdlRequestType.SelectedValue.Trim() & "' " & Where & " Order By SS_VFollowup.RequestDate desc"
                Else
                    str1 = "select distinct(SS_VFollowup.ID),SS_VFollowup.FormCode as VacationType ,SS_VFollowup.RequestSerial as RequestSerial ,SS_VFollowup.EmployeeID ," & EmpName & " as EmployeeName,SS_VFollowup.RequestDate,SS_VFollowup.VacationType ," & RequestType & " as RequestType ,SS_VFollowup.FormCode as FormCode from SS_VFollowup where Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  And Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)    and RequestStautsTypeID is not null and RequestStautsTypeID=5 " & Where & " Order By SS_VFollowup.RequestDate desc"
                End If
            ElseIf ddlStatus.SelectedValue = 0 Then 'برجاء الاختيار
                FillEmployeeVacations()

            End If
            If str1 <> "" Then
                command = New Data.SqlClient.SqlCommand(str1, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS1, "Table1")

                connection.Close()
                uwgEmployeeVacations.DataSource = Nothing
                uwgEmployeeVacations.DataBind()

                uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
                uwgEmployeeVacations.DataSource = DS1
                uwgEmployeeVacations.DataBind()

                If uwgEmployeeVacations.Rows.Count > 0 Then

                    FillRowColors()

                End If
            Else
                If ddlStatus.SelectedValue > 0 Then
                    uwgEmployeeVacations.DataSource = Nothing
                    uwgEmployeeVacations.DataBind()
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FillEmployeeVacations()
        Try
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Me)
            'ClsEmployees.Find("Code='" & _sys_User.Code & "'")
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
            Dim RequestType As String
            Dim vacationName0 As String = ""
            Dim vacationName As String = ""
            Dim vacationName2 As String = ""
            Dim permName As String = ""
            Dim ServiceName As String = ""
            Dim mServiceName As String = ""

            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " EmployeeArbName as EmployeeName "
                vacationName0 = "اجازة سنوي اداري"
                vacationName = "اجازة سنوي طبي"
                vacationName2 = "hrs_VacationsTypes.ArbName4S"
                permName = "طلب استئذان"
                ServiceName = "طلب انهاء خدمة"
                mServiceName = "طلب انهاء خدمة طبي"
                RequestType = "SS_VFollowup.RequestArbName as RequestType"

            Else
                EmpName = " EmployeeEngName as EmployeeName"
                vacationName0 = "Annual administrative Vacation"
                vacationName = "Annual medical Vacation"
                vacationName2 = "hrs_VacationsTypes.EngName"
                permName = "Execuse Request"
                ServiceName = "Request for termination of service"
                mServiceName = "Request for medical termination of service"
                RequestType = "SS_VFollowup.RequestEngName as RequestType"

            End If


            Dim str1 As String

            str1 = "select SS_VFollowup.ID,SS_VFollowup.VacationType,SS_VFollowup.RequestSerial,SS_VFollowup.EmployeeID," & EmpName & ",SS_VFollowup.RequestDate,SS_VFollowup.VacationType," & RequestType & ",SS_VFollowup.FormCode from SS_VFollowup "

            Dim Criteria As String = " Where  Requestdate>=convert(datetime,'" & ddlFromDate.Value & "',103)  and Requestdate<=convert(datetime,'" & ddlToDate.Value & "',103)"
            _sys_User.Find("ID = '" & User & "'")
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            If ClsEmployees.ID > 0 Then
                Criteria &= " AND  EmployeeID in (select ID from hrs_employees where ManagerID='" & ClsEmployees.ID & "')"
            End If
            If DdlRequestType.SelectedValue <> "" And DdlRequestType.SelectedValue <> "0" Then
                Criteria &= " and SS_VFollowup.FormCode='" & DdlRequestType.SelectedValue.Trim() & "'"
            End If
            If Not String.IsNullOrEmpty(txtEmpCode.Text) Then
                ClsEmployees.Find("Code='" & txtEmpCode.Text & "'")
                Criteria &= " And SS_VFollowup.EmployeeID= '" & ClsEmployees.ID & "'"
            End If

            If str1 <> "" Then
                str1 &= Criteria
                str1 &= " Order By RequestDate desc"

                command = New Data.SqlClient.SqlCommand(str1, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS1, "Table1")

                connection.Close()
                uwgEmployeeVacations.DataSource = Nothing
                uwgEmployeeVacations.DataBind()

                uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
                uwgEmployeeVacations.DataSource = DS1
                uwgEmployeeVacations.DataBind()

                If uwgEmployeeVacations.Rows.Count > 0 Then

                    FillRowColors()

                End If
            Else
                uwgEmployeeVacations.DataSource = Nothing
                uwgEmployeeVacations.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetAllEmployeeRequests()
        Try
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Me)
            'ClsEmployees.Find("Code='" & _sys_User.Code & "'")
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
            Dim RequestType As String
            Dim vacationName0 As String = ""
            Dim vacationName As String = ""
            Dim vacationName2 As String = ""
            Dim permName As String = ""
            Dim ServiceName As String = ""
            Dim mServiceName As String = ""

            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " EmployeeArbName as EmployeeName "
                vacationName0 = "اجازة سنوي اداري"
                vacationName = "اجازة سنوي طبي"
                vacationName2 = "hrs_VacationsTypes.ArbName4S"
                permName = "طلب استئذان"
                ServiceName = "طلب انهاء خدمة"
                mServiceName = "طلب انهاء خدمة طبي"
                RequestType = "SS_VFollowup.RequestArbName as RequestType"

            Else
                EmpName = " EmployeeEngName as EmployeeName"
                vacationName0 = "Annual administrative Vacation"
                vacationName = "Annual medical Vacation"
                vacationName2 = "hrs_VacationsTypes.EngName"
                permName = "Execuse Request"
                ServiceName = "Request for termination of service"
                mServiceName = "Request for medical termination of service"
                RequestType = "SS_VFollowup.RequestEngName as RequestType"

            End If
            Dim str1 As String = ""

            Dim Where As String = ""
            _sys_User.Find("ID = '" & User & "'")
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            If ClsEmployees.ID > 0 Then
                Where &= " And  EmployeeID in (select ID from hrs_employees where ManagerID='" & ClsEmployees.ID & "')"

            End If

            'str1 = "select SS_VacationRequest.ID,SS_VacationRequest.VacationType ,SS_VacationRequest.Code as RequestSerial ,SS_VacationRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_VacationRequest.RequestDate,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName0 & "' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then '" & vacationName & "' else  " & vacationName2 & "  end as RequestType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'SS_0011' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'SS_0012' when SS_VacationRequest.VacationType='SS_0018' then 'SS_0018' else  'SS_0013'  end as FormCode from SS_VacationRequest join hrs_Employees on SS_VacationRequest.EmployeeID=hrs_Employees.id join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID join SS_RequestActions on SS_VacationRequest.ID=SS_RequestActions.RequestSerial and    SS_VacationRequest.VacationType=SS_RequestActions.FormCode where year(Requestdate)=year(GETDATE()) " & Where & "  union select SS_ExecuseRequest.ID,'4' as VacationType,SS_ExecuseRequest.Code as RequestSerial ,SS_ExecuseRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_ExecuseRequest.RequestDate,'', '" & permName & "'  as RequestType ,'SS_0014' as FormCode from SS_ExecuseRequest join hrs_Employees on SS_ExecuseRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_ExecuseRequest.ID=SS_RequestActions.RequestSerial and SS_RequestActions.FormCode='SS_0014' where year(Requestdate)=year(GETDATE()) " & Where & " union select SS_EndOfServiceRequest.ID,'5' as VacationType,SS_EndOfServiceRequest.Code as RequestSerial ,SS_EndOfServiceRequest.EmployeeID ," & EmpName & " as EmployeeName,SS_EndOfServiceRequest.RequestDate,'', case when SS_RequestActions.FormCode='SS_0015' then '" & ServiceName & "' else '" & mServiceName & "' end  as RequestType ,SS_RequestActions.FormCode from SS_EndOfServiceRequest join hrs_Employees on SS_EndOfServiceRequest.EmployeeID=hrs_Employees.id join SS_RequestActions on SS_EndOfServiceRequest.ID=SS_RequestActions.RequestSerial and SS_RequestActions.FormCode='SS_0015' where year(Requestdate)=year(GETDATE()) " & Where & " Order By SS_VacationRequest.RequestDate desc"
            str1 = "select SS_VFollowup.ID,SS_VFollowup.VacationType,SS_VFollowup.RequestSerial,SS_VFollowup.EmployeeID," & EmpName & ",SS_VFollowup.RequestDate,SS_VFollowup.VacationType," & RequestType & ",SS_VFollowup.FormCode from SS_VFollowup "
            _sys_User.Find("ID = '" & User & "'")
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
            If ClsEmployees.ID > 0 Then
                str1 &= " Where  SS_VFollowup.EmployeeID in (select ID from hrs_employees where ManagerID='" & ClsEmployees.ID & "')"
            End If

            command = New Data.SqlClient.SqlCommand(str1, connection)
            adapter.SelectCommand = command
            adapter.Fill(DS1, "Table1")

            connection.Close()
            uwgEmployeeVacations.DataSource = Nothing
            uwgEmployeeVacations.DataBind()

            uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            uwgEmployeeVacations.DataSource = DS1
            uwgEmployeeVacations.DataBind()
            If uwgEmployeeVacations.Rows.Count > 0 Then

                FillRowColors()

            End If

            'FillRowColors
        Catch ex As Exception

        End Try
    End Sub
    Public Sub FillRowColors()
        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")
        Dim ClsEmployees As New Clshrs_Employees(Me)
        'ClsEmployees.Find("Code='" & _sys_User.Code & "'")
        Dim connetionString As String
        Dim connection As Data.SqlClient.SqlConnection
        Dim command As Data.SqlClient.SqlCommand
        Dim adapter As New Data.SqlClient.SqlDataAdapter
        connetionString = ClsEmployees.ConnectionString
        connection = New Data.SqlClient.SqlConnection(connetionString)

        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeVacations.Rows
            Dim RequestSerial As String = row.Cells().FromKey("RequestSerial").Value
            Dim FormCode As String = row.Cells().FromKey("FormCode").Value
            Dim ID As Integer = row.Cells().FromKey("ID").Value


            Dim DS3 As New Data.DataSet()
            DS3.Clear()
            For x As Integer = 0 To DS3.Tables.Count - 1
                DS3.Tables(x).Constraints.Clear()
            Next
            DS3.Relations.Clear()
            DS3.Tables.Clear()
            Dim command3 As Data.SqlClient.SqlCommand
            Dim adapter3 As New Data.SqlClient.SqlDataAdapter
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim str3 As String
            'str3 = "Select * from SS_RequestActions where RequestSerial=" & ID & " and SS_RequestActions.EmployeeID=" & ClsEmployees.ID & " And FormCode='" & FormCode & "' and (IsHidden<>1 or IsHidden is null) and ActionID Is Null"
            str3 = "Select * from SS_RequestActions where RequestSerial=" & ID & "  And FormCode='" & FormCode & "' and (IsHidden<>1 or IsHidden is null) and ActionID Is Null"

            command3 = New Data.SqlClient.SqlCommand(str3, connection)
            adapter3.SelectCommand = command3
            adapter3.Fill(DS3, "Table1")
            If DS3.Tables(0).Rows.Count = 0 Then
                'Accepted Request
                row.Style.BackColor = System.Drawing.Color.FromArgb(223, 240, 216)
            End If

            Dim DS4 As New Data.DataSet()
            DS4.Clear()
            For x As Integer = 0 To DS4.Tables.Count - 1
                DS4.Tables(x).Constraints.Clear()
            Next
            DS4.Relations.Clear()
            DS4.Tables.Clear()
            Dim command4 As Data.SqlClient.SqlCommand
            Dim adapter4 As New Data.SqlClient.SqlDataAdapter
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim str4 As String
            str4 = "Select * from SS_RequestActions where RequestSerial=" & ID & "  And FormCode='" & FormCode & "' and (IsHidden<>1 or IsHidden is null) and ActionID=2"

            command4 = New Data.SqlClient.SqlCommand(str4, connection)
            adapter4.SelectCommand = command4
            adapter4.Fill(DS4, "Table1")
            If DS4.Tables(0).Rows.Count > 0 Then
                'Rejrcted Request
                row.Style.BackColor = System.Drawing.Color.FromArgb(242, 222, 222)

            End If
            Dim DS5 As New Data.DataSet()
            DS5.Clear()
            For x As Integer = 0 To DS5.Tables.Count - 1
                DS5.Tables(x).Constraints.Clear()
            Next
            DS5.Relations.Clear()
            DS5.Tables.Clear()
            Dim command5 As Data.SqlClient.SqlCommand
            Dim adapter5 As New Data.SqlClient.SqlDataAdapter
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim str5 As String
            str5 = "Select * from SS_RequestActions where RequestSerial=" & ID & "  And FormCode='" & FormCode & "' And (IsHidden<>1 or IsHidden is null) and ActionID Is Null"

            command5 = New Data.SqlClient.SqlCommand(str5, connection)
            adapter5.SelectCommand = command5
            adapter5.Fill(DS5, "Table1")
            If DS5.Tables(0).Rows.Count > 0 Then
                'row.Style.BackColor = System.Drawing.Color.FromArgb(254, 216, 93)
                'Action token but not yet finish
                row.Style.BackColor = System.Drawing.Color.FromArgb(217, 237, 247)


            End If

            Dim DS6 As New Data.DataSet()
            DS6.Clear()
            For x As Integer = 0 To DS6.Tables.Count - 1
                DS6.Tables(x).Constraints.Clear()
            Next
            DS6.Relations.Clear()
            DS6.Tables.Clear()
            Dim command6 As Data.SqlClient.SqlCommand
            Dim adapter6 As New Data.SqlClient.SqlDataAdapter
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim str6 As String
            str6 = "select hrs_Employees.ArbName4S +' ' +FatherArbName+' '+ FamilyArbName as EmployeeName,( case when SS_UserActions.ActionAraName is not null then SS_UserActions.ActionAraName else 'Pending ...' end) As Action,ConfirmedNoOfdays ,convert(varchar, ActionDate,101) as ActionDate,ActionRemarks  from  SS_RequestActions  join hrs_Employees on hrs_Employees.ID= SS_RequestActions.SS_EmployeeID left join SS_UserActions on SS_RequestActions.ActionID=SS_UserActions.ID where RequestSerial=" & ID & " And FormCode='" & FormCode & "' and (IsHidden<>1 or IsHidden is null)  "

            command6 = New Data.SqlClient.SqlCommand(str6, connection)
            adapter6.SelectCommand = command6
            adapter6.Fill(DS6, "Table1")
            'If DS6.Tables(0).Rows.Count = 1 Then
            '    For Each row6 In DS6.Tables(0).Rows
            '        If CStr(row6("Action")) = "Pending ..." Then
            '            'No Action yet
            '            row.Style.BackColor = System.Drawing.Color.FromArgb(252, 248, 227)

            '        End If
            '    Next


            'End If

            If DS6.Tables(0).Rows.Count > 0 Then
                If CStr(DS6.Tables(0).Rows(0).Item("Action")) = "Pending ..." Then
                    row.Style.BackColor = System.Drawing.Color.FromArgb(252, 248, 227)
                End If
            End If


            Dim DS7 As New Data.DataSet()
            DS7.Clear()
            For x As Integer = 0 To DS7.Tables.Count - 1
                DS7.Tables(x).Constraints.Clear()
            Next
            DS7.Relations.Clear()
            DS7.Tables.Clear()
            Dim command7 As Data.SqlClient.SqlCommand
            Dim adapter7 As New Data.SqlClient.SqlDataAdapter
            connection = New Data.SqlClient.SqlConnection(connetionString)
            Dim str7 As String
            str7 = "Select * from SS_RequestActions where RequestSerial=" & ID & "  And FormCode='" & FormCode & "' and (IsHidden<>1 or IsHidden is null) and ActionID=4"

            command7 = New Data.SqlClient.SqlCommand(str7, connection)
            adapter7.SelectCommand = command7
            adapter7.Fill(DS7, "Table1")
            If DS7.Tables(0).Rows.Count > 0 Then
                For Each row6 In DS7.Tables(0).Rows
                    'Cancel Request
                    row.Style.BackColor = System.Drawing.Color.FromArgb(255, 165, 0)

                Next


            End If

        Next

    End Sub
    Private Sub FillDdlRequestTypes()
        DdlRequestType.Items.Clear()
        ClsEmployees = New Clshrs_Employees(Page)

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        Item = New Global.System.Web.UI.WebControls.ListItem
        Item.Text = ObjNavigationHandler.SetLanguage(Page, "[Select Your Choice]/[ برجاء الاختيار ]")
        Item.Value = 0
        DdlRequestType.Items.Add(Item)
        '==============================

        Dim WebHandler As New Venus.Shared.Web.WebHandler
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

        Dim RequestName As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            RequestName = "RequestArbName"
        Else
            RequestName = "RequestEngName"
        End If

        Dim str1 As String = "select RequestCode," & RequestName & " as RequestName from SS_RequestTypes where NotActive is null or NotActive = 0"
        command = New Data.SqlClient.SqlCommand(str1, connection)
        adapter.SelectCommand = command
        adapter.Fill(DS1, "Table1")

        connection.Close()
        If DS1.Tables(0).Rows.Count > 0 Then
            For Each Row In DS1.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Value = (Row("RequestCode"))
                Item.Text = Row("RequestName")
                DdlRequestType.Items.Add(Item)

            Next
        End If

    End Sub


    Private Sub FillStatus()
        ddlStatus.Items.Clear()
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        Item = New Global.System.Web.UI.WebControls.ListItem
        Item.Text = ObjNavigationHandler.SetLanguage(Page, "[Select Your Choice]/[ برجاء الاختيار ]")
        Item.Value = 0
        ddlStatus.Items.Add(Item)

        Dim WebHandler As New Venus.Shared.Web.WebHandler
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

        Dim RequestName As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            RequestName = "AraName"
        Else
            RequestName = "EngName"
        End If

        Dim str1 As String = "select ID," & RequestName & " as statuesName from SS_RequestStatuesTypes "
        command = New Data.SqlClient.SqlCommand(str1, connection)
        adapter.SelectCommand = command
        adapter.Fill(DS1, "Table1")

        connection.Close()
        If DS1.Tables(0).Rows.Count > 0 Then
            For Each Row In DS1.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Value = (Row("ID"))
                Item.Text = Row("statuesName")
                ddlStatus.Items.Add(Item)

            Next
        End If

    End Sub
    Private Sub setOverDueDays(ClsVacationTypes As Clshrs_VacationsTypes, dat_NEW_RETURN As Date)
        ClsEmployeesVacations.ConsumDays = dat_NEW_RETURN.Subtract(ClsEmployeesVacations.ActualStartDate).Days
        If ClsVacationTypes.ExceededDaysType = 0 Then

            If Convert.ToInt32(ClsEmployeesVacations.ConsumDays) > Convert.ToInt32(Math.Round(CDec(ClsEmployeesVacations.TotalDays))) Then
                ClsEmployeesVacations.OverdueDays = Convert.ToInt32(ClsEmployeesVacations.ConsumDays) - Convert.ToInt32(ClsEmployeesVacations.TotalDays)
            Else
                ClsEmployeesVacations.OverdueDays = 0
            End If
        ElseIf ClsVacationTypes.ExceededDaysType = 1 Then

            If Convert.ToInt32(ClsEmployeesVacations.ConsumDays) > Convert.ToInt32(ClsEmployeesVacations.vactiondays) Then
                ClsEmployeesVacations.OverdueDays = Convert.ToInt32(ClsEmployeesVacations.ConsumDays) - Convert.ToInt32(ClsEmployeesVacations.vactiondays)
            End If
        End If
    End Sub
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
                dteactualEndDate = GetNDate_Shared(row("ActualEndDate"), IIf(Date.Now > dateEndDate, dateEndDate, Date.Now))
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
    'Protected Sub WebDateChooser3_ValueChanged(sender As Object, e As Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs) Handles WebDateChooser3.ValueChanged
    '    If lbVactionID.Text = "" Then
    '        txtVactiondays.Text = CDate(WebDateChooser3.Value).Subtract(CDate(WebDateChooser1.Value)).Days
    '        txtVactiondays_ValueChange(Nothing, Nothing)
    '    End If
    'End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim RequestSerial As Integer

        'RequestSerial = uwgEmployeeVacations.DisplayLayout.SelectedRows.Item(0).GetCellValue()



        'Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPostJournalPreview.aspx?IDs=" & StrIDArray & "", 1500, 1200, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
    End Sub

    Private Sub LinkButton_Remarks_Load(sender As Object, e As EventArgs) Handles LinkButton_Remarks.Load

    End Sub
End Class
