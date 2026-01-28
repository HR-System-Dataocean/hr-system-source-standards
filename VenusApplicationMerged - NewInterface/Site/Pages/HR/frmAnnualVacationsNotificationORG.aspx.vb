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
                FillDdlYear()
                FillMonth()
                GetAllEmployeeRequests()
                Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)
                ClsEmployeesVacations.AddNotificationOnChange(Page)
                Dim csSearchID As Integer
                Dim ClsLevels As New Clshrs_LevelTypes(Page)
                Dim ClsDataHandler As New Venus.Shared.DataHandler
                Dim StrSerial As String = String.Empty
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
                Dim ClsObjects As New Clssys_Objects(Page)
                Dim ClsSearchs As New Clssys_Searchs(Page)
                Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lage", lblLage.Text)
                Dim IntDimension As Integer = 510
                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)

            Catch ex As Exception
                mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
                Page.Session.Add("ErrorValue", ex)
                mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
                Page.Response.Redirect("ErrorPage.aspx")
            End Try

        End If
             End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Delete.Command
        Dim x As Integer
        x = 10

    End Sub

#End Region
#Region "Private Functions"
    Private Sub AddOnChangeEventToControls(ByVal formName As String)

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
    Private Function GetDateDiffAccordingWH(ByVal dteActualStartDate As Date, ByVal dteactualEndDate As Date, ByVal WorkingHoursPerDay As Double, ByVal dteStartTime As Date, ByVal dteEndTime As Date) As Double
        Dim NoOfWorkingHours As Double = WorkingHoursPerDay
        Dim NoOfNonWorkingHours As Double = 24 - NoOfWorkingHours
        Dim vacDays As Double = 0
        Dim dteThisStartDateStartTime As Date
        Dim dteThisStartDateEndTime As Date
        Dim dteThisEndDateStartTime As Date
        Dim dteThisEndDateEndTime As Date
        If dteactualEndDate < dteActualStartDate Then
            Return 0
        End If
        Dim dteTempActualStartDate As Date = New Date(dteActualStartDate.Year, dteActualStartDate.Month, dteActualStartDate.Day, dteStartTime.Hour, dteStartTime.Minute, dteStartTime.Second)
        Dim dteTempActualEndDate As Date = New Date(dteactualEndDate.Year, dteactualEndDate.Month, dteactualEndDate.Day, dteEndTime.Hour, dteEndTime.Minute, dteEndTime.Second)
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
            Return 0
        End If
        If DateDiff(DateInterval.Day, dteActualStartDate, dteactualEndDate) = 0 And
            (dteActualStartDate >= dteThisStartDateEndTime And dteactualEndDate <= dteThisEndDateStartTime) Then
            Return 0
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
        Dim diffH As Double = DateDiff(DateInterval.Minute, dteActualStartDate, dteactualEndDate) / 60
        Dim diffD As Double = DateDiff(DateInterval.Day, dteActualStartDate, dteactualEndDate)
        vacDays += (diffH - (diffD * NoOfNonWorkingHours)) / NoOfWorkingHours
        Return vacDays
    End Function
    Public Shared Function GetNDate_Shared(ByVal date1 As Object, ByVal dteDefault As Date) As Date
        If IsDBNull(date1) Then
            Return dteDefault
        Else
            Return CDate(date1)
        End If
    End Function
    Private Sub FillEmployeeVacations()
        Try
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Me)
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
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
                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
            Else
                EmpName = "[dbo].[fn_GetEmpName](hrs_Employees.Code,0)"
            End If
            Dim str1 As String = ""



            If DdlRequestType.SelectedValue = 1 Then
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID ," & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0011'  and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0011' and year(SS_VacationRequest.RequestDate)=" & DdlYear.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue > 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID ," & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0011'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0011' and year(SS_VacationRequest.RequestDate)=" & DdlYear.SelectedValue & "And Month(SS_VacationRequest.RequestDate)=" & DdlMonth.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue = 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID ," & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else  hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0011'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0011' Order By SS_VacationRequest.RequestDate desc"
                End If


            ElseIf DdlRequestType.SelectedValue = 2 Then
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0012'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0012' and year(SS_VacationRequest.RequestDate)=" & DdlYear.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue > 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID ," & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0012'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0011' and year(SS_VacationRequest.RequestDate)=" & DdlYear.SelectedValue & "And Month(SS_VacationRequest.RequestDate)=" & DdlMonth.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue = 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID ," & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0012'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0011' Order By SS_VacationRequest.RequestDate desc"
                End If
            ElseIf DdlRequestType.SelectedValue = 3 Then
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID ," & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0013'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0013' and year(SS_VacationRequest.RequestDate)=" & DdlYear.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue > 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID ," & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0013'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0013' and year(SS_VacationRequest.RequestDate)=" & DdlYear.SelectedValue & "And Month(SS_VacationRequest.RequestDate)=" & DdlMonth.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue = 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID ," & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0013'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0013' Order By SS_VacationRequest.RequestDate desc"

                End If
            ElseIf DdlRequestType.SelectedValue = 4 Then
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_ExecuseRequest.Code as RequestSerial, hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب استئذان'as RequestType from SS_RequestActions join SS_ExecuseRequest on SS_RequestActions.RequestSerial=SS_ExecuseRequest.ID    and SS_RequestActions.EmployeeID=SS_ExecuseRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0014' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(RequestDate)=" & DdlYear.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue > 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_ExecuseRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب استئذان'as RequestType from SS_RequestActions join SS_ExecuseRequest on SS_RequestActions.RequestSerial=SS_ExecuseRequest.ID    and SS_RequestActions.EmployeeID=SS_ExecuseRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0014' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(RequestDate)=" & DdlYear.SelectedValue & "And Month(RequestDate)=" & DdlMonth.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue = 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_ExecuseRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب استئذان'as RequestType from SS_RequestActions join SS_ExecuseRequest on SS_RequestActions.RequestSerial=SS_ExecuseRequest.ID    and SS_RequestActions.EmployeeID=SS_ExecuseRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0014' and  SS_EmployeeID=" & ClsEmployees.ID & " Order By SS_ExecuseRequest.RequestDate desc"

                End If

            ElseIf DdlRequestType.SelectedValue = 5 Then
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,SS_RequestActions.FormCode,ConfigID,SS_EndOfServiceRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب انهاء خدمة'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and SS_RequestActions.FormCode='SS_0015' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(RequestDate)=" & DdlYear.SelectedValue & " Order By SS_EndOfServiceRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue > 0 Then
                    str1 = "select RequestSerial as ID,SS_RequestActions.FormCode,ConfigID,SS_EndOfServiceRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب انهاء خدمة'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and SS_RequestActions.FormCode='SS_0015' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(RequestDate)=" & DdlYear.SelectedValue & "And Month(RequestDate)=" & DdlMonth.SelectedValue & " Order By SS_EndOfServiceRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue = 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,SS_RequestActions.FormCode,ConfigID,SS_EndOfServiceRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب انهاء خدمة'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and SS_RequestActions.FormCode='SS_0015' and  SS_EmployeeID=" & ClsEmployees.ID & " Order By SS_EndOfServiceRequest.RequestDate desc"
                End If

            ElseIf DdlRequestType.SelectedValue = 10 Then
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0018'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0018' and year(SS_VacationRequest.RequestDate)=" & DdlYear.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue > 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0018'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0018' and year(SS_VacationRequest.RequestDate)=" & DdlYear.SelectedValue & "And Month(SS_VacationRequest.RequestDate)=" & DdlMonth.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue = 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate , SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري' when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID     and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and FormCode='SS_0018'   and  SS_EmployeeID=" & ClsEmployees.ID & " and VacationType='SS_0018' Order By SS_VacationRequest.RequestDate desc"

                End If
            ElseIf DdlRequestType.SelectedValue = 11 Then
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,'SS_0015' as FormCode,ConfigID,SS_EndOfServiceRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب انهاء خدمة طبي'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and SS_EndOfServiceRequest.FormCode='SS_0019' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(RequestDate)=" & DdlYear.SelectedValue & " Order By SS_EndOfServiceRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue > 0 Then
                    str1 = "select RequestSerial as ID,'SS_0015' as FormCode,ConfigID,SS_EndOfServiceRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب انهاء خدمة طبي'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and SS_EndOfServiceRequest.FormCode='SS_0019' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(RequestDate)=" & DdlYear.SelectedValue & "And Month(RequestDate)=" & DdlMonth.SelectedValue & " Order By SS_EndOfServiceRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue = 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,'SS_0015' as FormCode,ConfigID,SS_EndOfServiceRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب انهاء خدمة طبي'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and SS_EndOfServiceRequest.FormCode='SS_0019' and  SS_EmployeeID=" & ClsEmployees.ID & " Order By SS_EndOfServiceRequest.RequestDate desc"
                End If
            ElseIf DdlRequestType.SelectedValue = 0 Then
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate ,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري'when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0) and (FormCode='SS_0011' or FormCode='SS_0012' or FormCode='SS_0013') and  SS_EmployeeID=" & ClsEmployees.ID & "  and year(SS_VacationRequest.RequestDate)=" & DdlYear.SelectedValue & " union select RequestSerial as ID,FormCode,ConfigID,SS_ExecuseRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب استئذان'as RequestType from SS_RequestActions join SS_ExecuseRequest on SS_RequestActions.RequestSerial=SS_ExecuseRequest.ID    and SS_RequestActions.EmployeeID=SS_ExecuseRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0014' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(SS_ExecuseRequest.RequestDate)=" & DdlYear.SelectedValue & " union Select RequestSerial As ID,FormCode,ConfigID,SS_EndOfServiceRequest.Code As RequestSerial, hrs_Employees.ID As EmployeeID , " & EmpName & " As EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب انهاء خدمة'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0015' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(SS_EndOfServiceRequest.RequestDate)=" & DdlYear.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue > 0 And DdlMonth.SelectedValue > 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial, hrs_Employees.ID As EmployeeID , hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate ,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري'when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0) and (FormCode='SS_0011' or FormCode='SS_0012' or FormCode='SS_0013') and  SS_EmployeeID=" & ClsEmployees.ID & "  and year(SS_VacationRequest.RequestDate)=" & DdlYear.SelectedValue & "And Month(SS_VacationRequest.RequestDate)=" & DdlMonth.SelectedValue & " union select RequestSerial as ID,FormCode,ConfigID,SS_ExecuseRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID ,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب استئذان'as RequestType from SS_RequestActions join SS_ExecuseRequest on SS_RequestActions.RequestSerial=SS_ExecuseRequest.ID    and SS_RequestActions.EmployeeID=SS_ExecuseRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0014' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(SS_ExecuseRequest.RequestDate)=" & DdlYear.SelectedValue & "And Month(SS_ExecuseRequest.RequestDate)=" & DdlMonth.SelectedValue & " Union Select RequestSerial As ID,FormCode,ConfigID,SS_EndOfServiceRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID , hrs_Employees.ID As EmployeeID ," & EmpName & " As EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب انهاء خدمة'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0015' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(SS_ExecuseRequest.RequestDate)=" & DdlYear.SelectedValue & "And Month(SS_ExecuseRequest.RequestDate)=" & DdlMonth.SelectedValue & " union Select RequestSerial As ID,FormCode,ConfigID,SS_EndOfServiceRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " As EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب انهاء خدمة'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0015' and  SS_EmployeeID=" & ClsEmployees.ID & " and year(SS_EndOfServiceRequest.RequestDate)=" & DdlYear.SelectedValue & "And Month(SS_EndOfServiceRequest.RequestDate)=" & DdlMonth.SelectedValue & " Order By SS_VacationRequest.RequestDate desc"
                End If
                If DdlYear.SelectedValue = 0 And DdlMonth.SelectedValue = 0 Then
                    str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial, hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate ,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='SS_0011' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي اداري'when SS_VacationRequest.VacationType='SS_0012' and SS_VacationRequest.VacationTypeID =1 then 'اجازة سنوي طبي' else   hrs_VacationsTypes.ArbName4S   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0) and (FormCode='SS_0011' or FormCode='SS_0012' or FormCode='SS_0013') and  SS_EmployeeID=" & ClsEmployees.ID & " union select RequestSerial as ID,FormCode,ConfigID,SS_ExecuseRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب استئذان'as RequestType from SS_RequestActions join SS_ExecuseRequest on SS_RequestActions.RequestSerial=SS_ExecuseRequest.ID    and SS_RequestActions.EmployeeID=SS_ExecuseRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0014' and  SS_EmployeeID=" & ClsEmployees.ID & " union Select RequestSerial As ID,FormCode,ConfigID,SS_EndOfServiceRequest.Code As RequestSerial, hrs_Employees.ID As EmployeeID , " & EmpName & " As EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , 'طلب انهاء خدمة'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0015' and  SS_EmployeeID=" & ClsEmployees.ID & " Order By SS_VacationRequest.RequestDate desc"


                End If
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
            Else
                uwgEmployeeVacations.DataSource = Nothing
                uwgEmployeeVacations.DataBind()


            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub GetAllEmployeeRequests()
        Try
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")
            Dim ClsEmployees As New Clshrs_Employees(Me)
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")
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
            Dim AnnualVacAdmission As String
            Dim AnnualVacMedical As String
            Dim VacationType As String
            Dim strexecuseRequest As String
            Dim strEndService As String

            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                AnnualVacAdmission = "اجازة سنوي اداري"
                AnnualVacMedical = "اجازة سنوي طبي"
                VacationType = " hrs_VacationsTypes.ArbName4S "
                strexecuseRequest = "طلب استئذان"
                strEndService = "طلب انهاء خدمة"
            Else
                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,0) "
                AnnualVacAdmission = "Annual Vacation Admission"
                AnnualVacMedical = "Annual Vacation Medical"
                VacationType = " hrs_VacationsTypes.EngName "
                strexecuseRequest = "Execuse Request"
                strEndService = " End Of Service Request"
            End If

            Dim str1 As String = ""
            str1 = "select RequestSerial as ID,FormCode,ConfigID,SS_VacationRequest.Code As RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_VacationRequest.RequestDate, 103) as RequestDate ,SS_VacationRequest.VacationType ,case when  SS_VacationRequest.VacationType='0011' and SS_VacationRequest.VacationTypeID =1 then '" & AnnualVacAdmission & "' when SS_VacationRequest.VacationType='0012' and SS_VacationRequest.VacationTypeID =1 then '" & AnnualVacMedical & "' else  " & VacationType & "   end as RequestType from SS_RequestActions join SS_VacationRequest on SS_RequestActions.RequestSerial=SS_VacationRequest.ID and SS_RequestActions.EmployeeID=SS_VacationRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID join hrs_VacationsTypes on SS_VacationRequest.VacationTypeID= hrs_VacationsTypes.ID where ( Seen is null or Seen=0)and (FormCode='SS_0011' or FormCode='SS_0012' or FormCode='SS_0013' or FormCode='SS_0018') and  SS_EmployeeID=" & ClsEmployees.ID & " union select RequestSerial as ID,FormCode,ConfigID,SS_ExecuseRequest.Code as RequestSerial,hrs_Employees.ID As EmployeeID , " & EmpName & " as EmployeeName,CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) as RequestDate ,'' as VacationType , ' " & strexecuseRequest & "' as RequestType from SS_RequestActions join SS_ExecuseRequest on SS_RequestActions.RequestSerial=SS_ExecuseRequest.ID    and SS_RequestActions.EmployeeID=SS_ExecuseRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and FormCode='SS_0014' and  SS_EmployeeID=" & ClsEmployees.ID & " Union select RequestSerial as ID,'SS_0015' as FormCode,ConfigID,SS_EndOfServiceRequest.Code as RequestSerial, hrs_Employees.ID As EmployeeID ," & EmpName & " as EmployeeName,CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) as RequestDate ,'' as VacationType , '  " & strEndService & "'as RequestType from SS_RequestActions join SS_EndOfServiceRequest on SS_RequestActions.RequestSerial=SS_EndOfServiceRequest.ID    and SS_RequestActions.EmployeeID=SS_EndOfServiceRequest.EmployeeID join hrs_Employees on SS_RequestActions.EmployeeID=hrs_Employees.ID where ( Seen is null or Seen=0) and (SS_RequestActions.FormCode='SS_0015' or SS_RequestActions.FormCode='SS_0019') and  SS_EmployeeID=" & ClsEmployees.ID & " Order By RequestDate desc"

            command = New Data.SqlClient.SqlCommand(str1, connection)
            adapter.SelectCommand = command
            adapter.Fill(DS1, "Table1")

            connection.Close()
            uwgEmployeeVacations.DataSource = Nothing
            uwgEmployeeVacations.DataBind()

            uwgEmployeeVacations.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
            uwgEmployeeVacations.DataSource = DS1
            uwgEmployeeVacations.DataBind()


        Catch ex As Exception

        End Try
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
        Dim strRequestName As String = "RequestEngName"
        If ProfileCls.CurrentLanguage = "Ar" Then
            strRequestName = "RequestArbName"
        End If
        Dim str1 As String = "select RequestID," & strRequestName & " as RequestArbName from SS_RequestTypes where NotActive =0 or NotActive  is null"
        command = New Data.SqlClient.SqlCommand(str1, connection)
        adapter.SelectCommand = command
        adapter.Fill(DS1, "Table1")

        connection.Close()
        If DS1.Tables(0).Rows.Count > 0 Then
            For Each Row In DS1.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Value = (Row("RequestID"))
                Item.Text = Row("RequestArbName")
                DdlRequestType.Items.Add(Item)

            Next
        End If

    End Sub
    Private Sub FillDdlYear()
        DdlYear.Items.Clear()
        ClsEmployees = New Clshrs_Employees(Page)

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        Item = New Global.System.Web.UI.WebControls.ListItem
        Item.Text = ObjNavigationHandler.SetLanguage(Page, "[Select Your Choice]/[ برجاء الاختيار ]")
        Item.Value = 0
        DdlYear.Items.Add(Item)
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

        Dim str1 As String = "select Code,ArbName from sys_FiscalYears"
        command = New Data.SqlClient.SqlCommand(str1, connection)
        adapter.SelectCommand = command
        adapter.Fill(DS1, "Table1")

        connection.Close()
        If DS1.Tables(0).Rows.Count > 0 Then
            For Each Row In DS1.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Value = (Row("Code"))
                Item.Text = Row("ArbName")
                DdlYear.Items.Add(Item)

            Next
        End If

    End Sub
    Private Sub FillMonth()
        DdlMonth.Items.Clear()
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        Item = New Global.System.Web.UI.WebControls.ListItem
        Item.Text = ObjNavigationHandler.SetLanguage(Page, "[Select Your Choice]/[ برجاء الاختيار ]")
        Item.Value = 0
        DdlMonth.Items.Add(Item)

        For x As Integer = 1 To 12
            Item = New Global.System.Web.UI.WebControls.ListItem
            Item.Value = x
            Item.Text = x.ToString()
            DdlMonth.Items.Add(Item)

        Next

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
    Protected Sub DdlRequestType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlRequestType.SelectedIndexChanged, DdlYear.SelectedIndexChanged, DdlMonth.SelectedIndexChanged

        FillEmployeeVacations()

    End Sub
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim RequestSerial As Integer

        'RequestSerial = uwgEmployeeVacations.DisplayLayout.SelectedRows.Item(0).GetCellValue()



        'Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPostJournalPreview.aspx?IDs=" & StrIDArray & "", 1500, 1200, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
    End Sub

    Private Sub LinkButton_Remarks_Load(sender As Object, e As EventArgs) Handles LinkButton_Remarks.Load

    End Sub
End Class
