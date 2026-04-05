Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.ServiceModel.Activities.Configuration
Imports Infragistics.Documents.Excel.Serialization
Imports Infragistics.WebUI.WebSchedule
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System
Imports Venus.Shared.Web

Partial Class frmAttendancePreparation
    Inherits MainPage

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields
    Private dbOTSalary As Double = 0
    Private dbHOTSalary As Double = 0
    Private ClsClasses As Clshrs_EmployeeClasses
    Private ClsEmployeesExcuses As Clshrs_EmployeesExcuses

    Private ClsEmployees As Clshrs_Employees


#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim TrnsID As Integer = Request.QueryString.Item("TrnsID")
        Dim VacationRequestID As Integer = Request.QueryString.Item("VacationRequestID")

        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)

        Dim clsEmployees As New Clshrs_Employees(Page)







        If Not IsPostBack Then



            Dim ClsObjects As New Clssys_Objects(Page)
            Dim ClsSearchs As New Clssys_Searchs(Page)
            Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
            ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
            ClsObjects.Find(" Code='" & clsEmployees.Table.Trim & "'")
            ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
            Dim csSearchID As Integer
            csSearchID = ClsSearchs.ID
            Dim IntDimension As Integer = 510


        End If
        Dim ClsCountries As New Clssys_Countries(Me.Page)
        Dim clsMainCurrency As New ClsSys_Currencies(Me.Page)

    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command
        Dim ClsEmployeesVacations As New Clshrs_EmployeesVacations(Page)

        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployeesVacations.ConnectionString)

        Select Case e.CommandArgument

            Case "Delete"
                If String.IsNullOrEmpty(TxtDeleteReason.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, " Sorry... Delete Operation Can't be complete without Delete Reason !/عفوا لا يمكن اتمام عملية الحذف بدون ذكر السبب"))

                Else
                    Try
                        Dim ClsEmployees As New Clshrs_Employees(Page)


                        Dim TrnsID As Integer = Request.QueryString.Item("TrnsID")
                        Dim RequestID As Integer = Request.QueryString.Item("RequestID")
                        Dim FormCode As String = Request.QueryString.Item("FormCode")
                        Dim User As String = String.Empty
                        Dim WebHandler As New Venus.Shared.Web.WebHandler
                        WebHandler.GetCookies(Page, "UserID", User)
                        Dim _sys_User As New Clssys_Users(Page)
                        _sys_User.Find("ID = '" & User & "'")
                        WebHandler.GetCookies(Page, "UserID", User)
                        ClsEmployeesExcuses.Delete("ID=" & TrnsID)





                        If CancelRequest(RequestID, FormCode, ClsEmployeesExcuses.EmployeeID) Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, " Delete Done !/!تم الحذف"))
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                        End If

                    Catch ex As Exception
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, " Delete Operation Failed !/!فشل عملية الحذف"))

                    End Try


                End If




        End Select




    End Sub


#End Region

#Region "Private Function"
    Private Function CancelRequest(RequestSerial As String, FormCode As String, EmployeeID As Integer) As Boolean
        Dim ConfigID As Integer = 0
        Dim CanbeCanceled As Boolean = False
        Dim TrnsID As Integer = Request.QueryString.Item("TrnsID")
        Dim ClsEmployeesVacations As New Clshrs_EmployeesVacations(Page)

        ClsEmployeesExcuses.Find("ID=" & TrnsID & "")
        '1-
        'Get the Current Pending Action
        Dim StrSelectCommand As String = String.Empty
        Dim mSelectCommand = " select ConfigID from SS_RequestActions where ActionSerial=(select max(Actionserial) from SS_RequestActions  where FormCode='" & FormCode & "' and RequestSerial=" & RequestSerial & "  and IsHidden is null) "
        Dim mSqlDataAdapter As New SqlClient.SqlDataAdapter
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnStr)



        'Add New Record In SS_RequestAction
        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")
        Dim ClsEmployees As New Clshrs_Employees(Me)
        Dim ClsEmployeesExcuses As New Clshrs_EmployeesExcuses(Me)
        ClsEmployees.Find("Code='" & _sys_User.Code & "'")
        Dim InsertCommand As String
        Dim SqlCommand As Data.SqlClient.SqlCommand

        InsertCommand = "Insert Into SS_RequestActions (RequestSerial,SS_EmployeeID,FormCode,EmployeeID,Seen,ActionID,ActionDate,ActionRemarks)  values(" & RequestSerial & " , " & ClsEmployees.ID & ",'" & FormCode & "'," & EmployeeID & ",1,4,GetDate(),'" & TxtDeleteReason.Text & "')"
        SqlCommand = New SqlClient.SqlCommand
        SqlCommand.Connection = New SqlClient.SqlConnection(ConnStr)
        SqlCommand.CommandType = CommandType.Text
        SqlCommand.CommandText = InsertCommand
        SqlCommand.Connection.Open()
        SqlCommand.ExecuteNonQuery()
        SqlCommand.Connection.Close()
        'Update Request Status in request table to cancel
        Dim SqlCommandRank As Data.SqlClient.SqlCommand
        Dim UpdateCommandRank As String = ""
        UpdateCommandRank = "UPDATE SS_ExecuseRequest SET [RequestStautsTypeID] = 5 WHERE ID=" & RequestSerial & ""
        SqlCommandRank = New SqlClient.SqlCommand
        SqlCommandRank.Connection = New SqlClient.SqlConnection(ClsEmployeesExcuses.ConnectionString)
        SqlCommandRank.CommandType = CommandType.Text
        SqlCommandRank.CommandText = UpdateCommandRank
        SqlCommandRank.Connection.Open()
        SqlCommandRank.ExecuteNonQuery()
        SqlCommandRank.Connection.Close()
        '4- Mark Prvioues Actions as seen to prevent notifications to others
        Dim UpdateCommand As String

        UpdateCommand = "update SS_RequestActions set seen=1 where ConfigID=" & ConfigID & " and RequestSerial=" & RequestSerial & " And FormCode='" & FormCode & "'"
        SqlCommand = New SqlClient.SqlCommand
        SqlCommand.Connection = New SqlClient.SqlConnection(ConnStr)
        SqlCommand.CommandType = CommandType.Text
        SqlCommand.CommandText = UpdateCommand
        SqlCommand.Connection.Open()
        SqlCommand.ExecuteNonQuery()
        SqlCommand.Connection.Close()
        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Delete Operation Has been canceled Successfully./تمت عملية الحذف بنجاح "))



        Return True


    End Function

#End Region

End Class
