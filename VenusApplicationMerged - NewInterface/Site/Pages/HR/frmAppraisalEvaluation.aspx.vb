Imports System.Activities.Statements
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.ServiceModel.Activities.Configuration
Imports Infragistics.WebUI.WebSchedule
Imports Microsoft.ApplicationBlocks.Data
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System
Imports Venus.Shared.Web

Partial Class frmAppraisalEvaluation
    Inherits MainPage

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields
    Private dbOTSalary As Double = 0
    Private dbHOTSalary As Double = 0
    Private ClsClasses As Clshrs_EmployeeClasses
    Private ClsEmployeesVacations As Clshrs_EmployeesVacations
    Private ClsEmployees As Clshrs_Employees


#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
        Dim AppraisalID As Integer = Request.QueryString.Item("AppraisalID")
        Dim NotificationID As Integer = Request.QueryString.Item("NotificationID")
        Dim ConfigurationLevel As Integer = Request.QueryString.Item("ConfigurationLevel")
        Dim ClsVacationTypes As New Clshrs_VacationsTypes(Page)

        Dim clsEmployees As New Clshrs_Employees(Page)





        Dim ClsVacationsTypes As New Clshrs_VacationsTypes(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler

        If Not IsPostBack Then

            txtEmployee.Enabled = False

            txtDescEnglishName.Enabled = False

            TxtPosition.Enabled = False
            TxtDepartment.Enabled = False
            'DdlAppraisalType.Enabled = False
            txtFromDate.Enabled = False
            txtToDate.Enabled = False
            txtLastAppDate.Enabled = False
            'txtTotalPercent.Enabled = False
            'lblObjectionDetais.Visible = False
            'txtObjectionDetais.Visible = False
            UwgSearchEmployees.Rows.Add()

            Dim ClsAppraisalTypes = New Clshrs_AppraisalTypes(Page)
            'ClsAppraisalTypes.GetDropDownList(DdlAppraisalType, True)

            FillEmployeeData()




        End If
        Dim ClsCountries As New Clssys_Countries(Me.Page)
        Dim clsMainCurrency As New ClsSys_Currencies(Me.Page)

    End Sub


    Private Sub FillEmployeeData()
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)
        Try
            Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
            Dim AppraisalID As Integer = Request.QueryString.Item("AppraisalID")
            Dim NotificationID As Integer = Request.QueryString.Item("NotificationID")
            Dim ConfigurationLevel As Integer = Request.QueryString.Item("ConfigurationLevel")

            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
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
            Dim Position As String
            Dim Department As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                Position = " dbo.hrs_Positions.ArbName "
                Department = " sys_Departments.ArbName "
            Else

                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,0) "
                Position = " dbo.hrs_Positions.EngName "
                Department = " sys_Departments.EngName "
            End If



            Dim DS2 As New Data.DataSet()
            Dim connetionString2 As String
            Dim connection2 As Data.SqlClient.SqlConnection
            Dim command2 As Data.SqlClient.SqlCommand
            Dim adapter2 As New Data.SqlClient.SqlDataAdapter
            connetionString2 = ClsEmployees.ConnectionString
            connection2 = New Data.SqlClient.SqlConnection(connetionString2)
            Dim strselect As String
            strselect = "select hrs_Employees.id,hrs_Employees.Code as EmployeeCode, " & EmpName & " as EmployeeName," & Department & " as DepartmentName, " & Position & " as PositionName from  hrs_Employees join sys_Departments on hrs_Employees.DepartmentID=sys_Departments.ID join hrs_Contracts on hrs_Contracts.EmployeeID =hrs_Employees.ID and (hrs_Contracts.EndDate is null or hrs_Contracts.EndDate > getdate()) join hrs_Positions on hrs_contracts.PositionID=hrs_Positions.ID where hrs_Employees.ID=" & EmployeeID & ""
            command2 = New Data.SqlClient.SqlCommand(strselect, connection2)
            adapter2.SelectCommand = command2
            adapter2.Fill(DS2, "Table1")
            adapter2.Dispose()
            command2.Dispose()
            connection2.Close()
            txtEmployee.Text = DS2.Tables(0).Rows(0)("EmployeeCode").ToString()
            txtDescEnglishName.Text = DS2.Tables(0).Rows(0)("EmployeeName").ToString()
            TxtPosition.Text = DS2.Tables(0).Rows(0)("PositionName").ToString()
            TxtDepartment.Text = DS2.Tables(0).Rows(0)("DepartmentName").ToString()


            Dim str1 As String = ""
            str1 = "SELECT AppraisalTypeID, FromDate, ToDate FROM     APP_Appraisals where ID=" & AppraisalID & " and EmployeeID=" & EmployeeID
            Dim DSt As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connetionString2, CommandType.Text, str1)
            If DSt.Tables(0).Rows.Count > 0 Then
                '  DdlAppraisalType.SelectedValue = DSt.Tables(0).Rows(0)("AppraisalTypeID").ToString()
                txtFromDate.Text = Convert.ToDateTime(DSt.Tables(0).Rows(0)("FromDate"))
                txtToDate.Text = Convert.ToDateTime(DSt.Tables(0).Rows(0)("ToDate"))
                txtLastAppDate.Text = Convert.ToDateTime(DSt.Tables(0).Rows(0)("ToDate"))

            End If
            ''''''''''''''==================Get Objection Details======================================
            Dim str As String = ""
            str = "select * from App_AppraisalNotifications where AppraisalID=" & AppraisalID & " and EmployeeID=" & EmployeeID & "  and APP_EmployeeID=" & EmployeeID & " and isobjection=1 and ConfigurationLevel=" & ConfigurationLevel & " + 1"
            Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(connetionString2, CommandType.Text, str)
            If DS.Tables(0).Rows.Count > 0 Then
                'lblObjectionDetais.Visible = True
                'txtObjectionDetais.Visible = True
                'txtObjectionDetais.Text = DS.Tables(0).Rows(0)("ObjectionDetails").ToString()
                GetPreviousAppraisalEvaluation()
            Else
                FillAppraisalCriteria()

            End If


        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Private Sub FillAppraisalCriteria()
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)

        Try
            Dim EmployeeID As Integer = Convert.ToInt32(Request.QueryString.Item("EmployeeID"))
            Dim AppraisalID As Integer = Convert.ToInt32(Request.QueryString.Item("AppraisalID"))
            Dim NotificationID As Integer = Convert.ToInt32(Request.QueryString.Item("NotificationID"))

            ' Get logged user
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)

            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")

            Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")

            ' Load employee contract
            Dim clsContract = New Clshrs_Contracts(Page)
            clsContract.Find("EmployeeId=" & EmployeeID)

            ' Build SQL query
            Dim strSelect As New System.Text.StringBuilder()
            Dim CriteriaName As String = If(ProfileCls.CurrentLanguage = "Ar", "crt.ArbName", "crt.EngName")
            Dim CriteriaGroupName As String = If(ProfileCls.CurrentLanguage = "Ar", "CG.ArbName", "CG.EngName")
            Dim KPIGroupName As String = If(ProfileCls.CurrentLanguage = "Ar", " ArbName", "EngName")
            strSelect.AppendLine("SELECT ")
            strSelect.AppendLine("    (SELECT TOP 1 App_CriteriaGroups.ID FROM App_CriteriaGroups join App_CriteriaGroupTypes on App_CriteriaGroupTypes.ID=App_CriteriaGroups.CriteriaGroupTypeID WHERE IsKPIGroup = 1) AS CriteriaGroupID,")
            strSelect.AppendLine("    (SELECT TOP 1 App_CriteriaGroups.EngName FROM App_CriteriaGroups join App_CriteriaGroupTypes on App_CriteriaGroupTypes.ID=App_CriteriaGroups.CriteriaGroupTypeID WHERE IsKPIGroup = 1) AS CriteriaGroupName,")
            strSelect.AppendLine("    D.LineNum AS CriteriaID,")
            strSelect.AppendLine("    D.KPIName AS CriteriaName,")
            strSelect.AppendLine("    D.MinScore AS MinimumScore,")
            strSelect.AppendLine("    D.MaxScore AS MaximumScore , ")
            strSelect.AppendLine("    D.Weight AS Weight")
            strSelect.AppendLine("FROM App_AppraisalEmployeesKPIs_H H")
            strSelect.AppendLine("INNER JOIN App_AppraisalEmployeesKPIs_D D ON H.ID = D.KPIsHeaderID")
            strSelect.AppendLine("WHERE H.AppraisalTypeID = @AppraisalTypeID AND H.EmployeeID = @EmployeeID")

            strSelect.AppendLine("UNION ALL")

            strSelect.AppendLine("SELECT ")
            strSelect.AppendLine("    PA.CriteriaGroupID,")
            strSelect.AppendLine("    " & CriteriaGroupName & " AS CriteriaGroupName,")
            strSelect.AppendLine("    CAST(PA.CriteriaID AS VARCHAR(100)) AS CriteriaIDID,")
            strSelect.AppendLine("    CAST( " & CriteriaName & " AS VARCHAR(100)) AS CriteriaName,")
            strSelect.AppendLine("    PA.MinimumScore,")
            strSelect.AppendLine("    PA.MaximumScore , ")
            strSelect.AppendLine("    PA.Weight")
            strSelect.AppendLine("FROM hrs_PositionAccountabilities PA")
            strSelect.AppendLine("LEFT JOIN App_CriteriaGroups CG ON PA.CriteriaGroupID = CG.ID")
            strSelect.AppendLine("LEFT JOIN App_Criteria crt ON PA.CriteriaID = crt.ID ")
            strSelect.AppendLine("WHERE PA.PositionID = @PositionID")

            strSelect.AppendLine("UNION ALL")

            strSelect.AppendLine("SELECT ")
            strSelect.AppendLine("    COmp.CriteriaGroupID,")
            strSelect.AppendLine("    " & CriteriaGroupName & "  AS CriteriaGroupName,")
            strSelect.AppendLine("    CAST(comp.CriteriaID AS VARCHAR(100)) AS CriteriaID,")

            strSelect.AppendLine("    CAST( " & CriteriaName & " AS VARCHAR(100)) AS CriteriaName,")
            strSelect.AppendLine("    COmp.MinimumScore,")
            strSelect.AppendLine("    COmp.MaximumScore ,")
            strSelect.AppendLine("    COmp.Weight")
            strSelect.AppendLine("FROM hrs_PositionCompetences Comp")
            strSelect.AppendLine("LEFT JOIN App_CriteriaGroups CG ON Comp.CriteriaGroupID = CG.ID")
            strSelect.AppendLine("LEFT JOIN App_Criteria crt ON Comp.CriteriaID = crt.ID ")

            strSelect.AppendLine("WHERE Comp.PositionID = @PositionID")
            ' Execute SQL safely
            Dim DS As New Data.DataSet()
            Using connection As New Data.SqlClient.SqlConnection(ClsEmployees.ConnectionString),
              command As New Data.SqlClient.SqlCommand(strSelect.ToString(), connection),
              adapter As New Data.SqlClient.SqlDataAdapter(command)

                ' Add parameters
                command.Parameters.AddWithValue("@AppraisalTypeID", 0)
                command.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                command.Parameters.AddWithValue("@PositionID", clsContract.PositionID)

                adapter.Fill(DS, "Table1")
            End Using

            ' Bind to grid
            uwgEmployeeAppraisal.DataSource = DS.Tables(0)
            uwgEmployeeAppraisal.DataBind()
            uwgEmployeeAppraisal.Columns.FromKey("HasObjection").Hidden = True
            uwgEmployeeAppraisal.Columns.FromKey("ObjectionDetails").Hidden = True



        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Private Sub GetPreviousAppraisalEvaluation()
        ClsEmployeesVacations = New Clshrs_EmployeesVacations(Page)

        Try
            Dim EmployeeID As Integer = Convert.ToInt32(Request.QueryString.Item("EmployeeID"))
            Dim AppraisalID As Integer = Convert.ToInt32(Request.QueryString.Item("AppraisalID"))
            Dim NotificationID As Integer = Convert.ToInt32(Request.QueryString.Item("NotificationID"))

            ' Get logged in user
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)

            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")

            Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")

            ' Pick criteria name based on language
            Dim CriteriaName As String = If(ProfileCls.CurrentLanguage = "Ar", "CriteriaArbName", "CriteriaEngName")
            Dim CriteriaGroupName As String = If(ProfileCls.CurrentLanguage = "Ar", "CritriaGroupArbName", "CritriaGroupEngName")

            ' SQL query (use StringBuilder to avoid multiline string literal issues)


            Dim sb As New System.Text.StringBuilder()
            sb.AppendLine("SELECT AppraisalID, App_EmployeeID,CriteriaGroupID," & CriteriaGroupName & " as CriteriaGroupName ,CriteriaID," & CriteriaName & " CriteriaName,MinimumScore,MaximumScore,AppraisalScore,HasObjection,ObjectionDetails from V_PreviousAppraisalEvaluation  ")

            sb.AppendLine("WHERE  AppraisalID = " & AppraisalID & "")
            sb.AppendLine(" And  App_EmployeeID = " & ClsEmployees.ID & "")
            sb.AppendLine("  And EmployeeID = " & EmployeeID & "")

            Dim strSelect As String = sb.ToString()

            ' Fill dataset safely
            Dim DS As New Data.DataSet()
            Using connection As New Data.SqlClient.SqlConnection(ClsEmployees.ConnectionString),
              command As New Data.SqlClient.SqlCommand(strSelect, connection),
              adapter As New Data.SqlClient.SqlDataAdapter(command)


                adapter.Fill(DS, "Table1")
            End Using

            ' Bind to grid
            uwgEmployeeAppraisal.DataSource = DS.Tables(0)
            uwgEmployeeAppraisal.DataBind()
            uwgEmployeeAppraisal.Columns.FromKey("HasObjection").AllowUpdate = False
            uwgEmployeeAppraisal.Columns.FromKey("ObjectionDetails").AllowUpdate = False
        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
        Dim AppraisalID As Integer = Request.QueryString.Item("AppraisalID")
        Dim NotificationID As Integer = Request.QueryString.Item("NotificationID")
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim WebHandler As New Venus.Shared.Web.WebHandler

        Dim User As String = String.Empty


        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("Code = '" & User & "'")




        WebHandler.GetCookies(Page, "UserID", User)

        _sys_User.Find("ID = '" & User & "'")

        ClsEmployees.Find("Code='" & _sys_User.Code & "'")


        Dim SqlCommand As Data.SqlClient.SqlCommand
        Dim UpdateCommand As String = ""



        Dim MinScore As Integer
        Dim MaxScore As Integer
        Dim AppraisalScore As Integer
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        For Each DGRow In uwgEmployeeAppraisal.Rows
            MinScore = DGRow.Cells.FromKey("MinimumScore").Value
            MaxScore = DGRow.Cells.FromKey("MaximumScore").Value
            AppraisalScore = DGRow.Cells.FromKey("AppraisalScore").Value
            If AppraisalScore < MinScore Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Appraisal Score Should be Greater Than Or Equal To  Minimum Score /  عفوا...لابد ان يكون درجة التقييم اكبر او يساوي الحد الادني للتقييم"))
                Exit Sub
            End If
            If AppraisalScore > MaxScore Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry...Appraisal Score Should be Less Than Or Equal To Maximum Score /  عفوا...لابد ان يكون درجة التقييم اقل من او يساوي الحد الاقصي للتقييم"))
                Exit Sub
            End If
        Next

        If SaveDG() Then
            Using cn As New SqlClient.SqlConnection(ClsEmployees.ConnectionString)
                Using cmd As New SqlClient.SqlCommand("UPDATE App_AppraisalNotifications SET Completed=1, RegDate=GETDATE(), CompleteDate=GETDATE(), RegUserID=@RegUserID WHERE Id=@NotificationID AND EmployeeID=@EmployeeID", cn)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.AddWithValue("@RegUserID", _sys_User.ID)
                    cmd.Parameters.AddWithValue("@NotificationID", NotificationID)
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            SendNextLevelNotification()
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done successfully /  تم الحفظ بنجاح)"))
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)


        End If
    End Sub
    Private Function SaveDG() As Boolean
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim connection As New SqlConnection(ClsEmployees.ConnectionString)
        Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
        Dim AppraisalID As Integer = Request.QueryString.Item("AppraisalID")
        Dim ConfigurationLevel As Integer = Request.QueryString.Item("ConfigurationLevel")
        Dim NotificationID As Integer = Request.QueryString.Item("NotificationID")
        Dim _sys_User As New Clssys_Users(Page)
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim User As String = String.Empty


        WebHandler.GetCookies(Page, "UserID", User)

        _sys_User.Find("ID = '" & User & "'")

        ClsEmployees.Find("Code='" & _sys_User.Code & "'")
        Try


            Dim str As String = ""
            str = "select * from App_AppraisalNotifications where AppraisalID=" & AppraisalID & " and EmployeeID=" & EmployeeID & "  and APP_EmployeeID=" & EmployeeID & " and isobjection=1 and ConfigurationLevel=" & ConfigurationLevel & " + 1"
            Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, str)
            If DS.Tables(0).Rows.Count > 0 Then
                ' Insert current grid rows
                Dim cs As String = ClsEmployees.ConnectionString
                Using cn As New SqlConnection(cs)
                    cn.Open()
                    Using trn = cn.BeginTransaction()
                        Using insCmd As New SqlCommand("INSERT INTO App_AppraisalResult (AppraisalID, NotificationID, EmployeeID, CriteriaGroupID, CriteriaID, App_EmployeeID, Score, Remarks, RegUserID, RegDate) VALUES (@AppraisalID, @NotificationID, @EmployeeID,@CriteriaGroupID,@CriteriaID, @AppEmployeeID, @Score, @Remarks, @RegUserID, GETDATE())", cn, trn)
                            insCmd.Parameters.Add("@AppraisalID", SqlDbType.Int)
                            insCmd.Parameters.Add("@NotificationID", SqlDbType.Int)
                            insCmd.Parameters.Add("@EmployeeID", SqlDbType.Int)
                            insCmd.Parameters.Add("@CriteriaGroupID", SqlDbType.Int)
                            insCmd.Parameters.Add("@CriteriaID", SqlDbType.Int)
                            insCmd.Parameters.Add("@AppEmployeeID", SqlDbType.Int)
                            insCmd.Parameters.Add("@Score", SqlDbType.Int)
                            insCmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 2000)
                            insCmd.Parameters.Add("@RegUserID", SqlDbType.Int)
                            insCmd.Parameters.Add("@RegDate", SqlDbType.Date)

                            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeAppraisal.Rows
                                If DGRow.Cells.FromKey("HasObjection").Value Then

                                    Dim critVal = DGRow.Cells.FromKey("CriteriaID").Value
                                Dim critGroupVal = DGRow.Cells.FromKey("CriteriaGroupID").Value
                                    If critVal IsNot Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(critVal)) Then
                                        insCmd.Parameters("@AppraisalID").Value = AppraisalID
                                        insCmd.Parameters("@NotificationID").Value = NotificationID
                                        insCmd.Parameters("@EmployeeID").Value = EmployeeID
                                        insCmd.Parameters("@CriteriaGroupID").Value = critGroupVal
                                        insCmd.Parameters("@CriteriaID").Value = Convert.ToInt32(critVal)
                                        insCmd.Parameters("@AppEmployeeID").Value = ClsEmployees.ID
                                        Dim scoreObj = DGRow.Cells.FromKey("AppraisalScore").Value
                                        insCmd.Parameters("@Score").Value = If(scoreObj Is Nothing OrElse Convert.ToString(scoreObj) = "", DBNull.Value, Convert.ToInt32(scoreObj))
                                        Dim REMa As String = String.Empty
                                        If DGRow.Cells.FromKey("AppraisalRemarks") IsNot Nothing AndAlso DGRow.Cells.FromKey("AppraisalRemarks").Value IsNot Nothing Then
                                            REMa = Convert.ToString(DGRow.Cells.FromKey("AppraisalRemarks").Value)
                                        End If
                                        insCmd.Parameters("@Remarks").Value = If(String.IsNullOrWhiteSpace(REMa), CType(DBNull.Value, Object), REMa)
                                        insCmd.Parameters("@RegUserID").Value = _sys_User.ID
                                        insCmd.Parameters("@RegDate").Value = DateTime.Now
                                        insCmd.ExecuteNonQuery()
                                    End If

                                End If
                            Next
                        End Using

                        trn.Commit()
                    End Using
                End Using


            Else
                Dim cs As String = ClsEmployees.ConnectionString
                Using cn As New SqlConnection(cs)
                    cn.Open()
                    Using trn = cn.BeginTransaction()
                        ' Delete previous results
                        Using delCmd As New SqlCommand("DELETE FROM App_AppraisalResult WHERE AppraisalID=@AppraisalID AND App_EmployeeID=@AppEmployeeID AND EmployeeID=@EmployeeID", cn, trn)
                            delCmd.Parameters.AddWithValue("@AppraisalID", AppraisalID)
                            delCmd.Parameters.AddWithValue("@AppEmployeeID", ClsEmployees.ID)
                            delCmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                            delCmd.ExecuteNonQuery()
                        End Using


                        ' Insert current grid rows



                        Using insCmd As New SqlCommand("INSERT INTO App_AppraisalResult (AppraisalID, NotificationID, EmployeeID, CriteriaGroupID, CriteriaID, App_EmployeeID, Score, Remarks, RegUserID, RegDate) VALUES (@AppraisalID, @NotificationID, @EmployeeID,@CriteriaGroupID,@CriteriaID, @AppEmployeeID, @Score, @Remarks, @RegUserID, GETDATE())", cn, trn)
                            insCmd.Parameters.Add("@AppraisalID", SqlDbType.Int)
                            insCmd.Parameters.Add("@NotificationID", SqlDbType.Int)
                            insCmd.Parameters.Add("@EmployeeID", SqlDbType.Int)
                            insCmd.Parameters.Add("@CriteriaGroupID", SqlDbType.Int)
                            insCmd.Parameters.Add("@CriteriaID", SqlDbType.Int)
                            insCmd.Parameters.Add("@AppEmployeeID", SqlDbType.Int)
                            insCmd.Parameters.Add("@Score", SqlDbType.Int)
                            insCmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 2000)
                            insCmd.Parameters.Add("@RegUserID", SqlDbType.Int)
                            insCmd.Parameters.Add("@RegDate", SqlDbType.Date)

                            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeAppraisal.Rows
                                Dim critVal = DGRow.Cells.FromKey("CriteriaID").Value
                                Dim critGroupVal = DGRow.Cells.FromKey("CriteriaGroupID").Value
                                If critVal IsNot Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(critVal)) Then
                                    insCmd.Parameters("@AppraisalID").Value = AppraisalID
                                    insCmd.Parameters("@NotificationID").Value = NotificationID
                                    insCmd.Parameters("@EmployeeID").Value = EmployeeID
                                    insCmd.Parameters("@CriteriaGroupID").Value = critGroupVal
                                    insCmd.Parameters("@CriteriaID").Value = Convert.ToInt32(critVal)
                                    insCmd.Parameters("@AppEmployeeID").Value = ClsEmployees.ID
                                    Dim scoreObj = DGRow.Cells.FromKey("AppraisalScore").Value
                                    insCmd.Parameters("@Score").Value = If(scoreObj Is Nothing OrElse Convert.ToString(scoreObj) = "", DBNull.Value, Convert.ToInt32(scoreObj))
                                    Dim REMa As String = String.Empty
                                    If DGRow.Cells.FromKey("AppraisalRemarks") IsNot Nothing AndAlso DGRow.Cells.FromKey("AppraisalRemarks").Value IsNot Nothing Then
                                        REMa = Convert.ToString(DGRow.Cells.FromKey("AppraisalRemarks").Value)
                                    End If
                                    insCmd.Parameters("@Remarks").Value = If(String.IsNullOrWhiteSpace(REMa), CType(DBNull.Value, Object), REMa)
                                    insCmd.Parameters("@RegUserID").Value = _sys_User.ID
                                    insCmd.Parameters("@RegDate").Value = DateTime.Now
                                    insCmd.ExecuteNonQuery()
                                End If
                            Next
                        End Using

                        trn.Commit()
                    End Using
                End Using

                SaveAppraisalCriteriasFromGrid()

            End If













            SaveEmployeePoints(AppraisalID, EmployeeID, ConfigurationLevel, txtStrengthPoints.Text, txtWeaknessPoints.Text)
            InsertEmployeeImprovements(AppraisalID, EmployeeID, ConfigurationLevel)

            Return True

        Catch ex As Exception

            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployees.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

    Private Sub SaveAppraisalCriteriasFromGrid()
        Try
            ' Get logged user
            Dim User As String = String.Empty
            Dim WebHandler As New Venus.Shared.Web.WebHandler
            WebHandler.GetCookies(Page, "UserID", User)

            ' Employee/user info
            Dim _sys_User As New Clssys_Users(Page)
            _sys_User.Find("ID = '" & User & "'")

            Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
            Dim AppraisalID As Integer = Request.QueryString.Item("AppraisalID")

            Dim ClsEmployees As New Clshrs_Employees(Page)
            ClsEmployees.Find("Code='" & _sys_User.Code & "'")


            ' Loop grid rows
            Dim connStr As String = ClsEmployees.ConnectionString

            Using conn As New SqlConnection(connStr)
                conn.Open()
                For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEmployeeAppraisal.Rows

                    ' 1. Insert into App_Criteria
                    Dim criteriaId As Integer
                    'Dim sql1 As String = "INSERT INTO App_Criteria (Code, EngName, ArbName, Score, CriteriaGroupID, RegUserID, RegDate) " &
                    '                     "VALUES (@Code, @EngName, @ArbName, @Score, @CriteriaGroupID, @RegUserID, GETDATE()); SELECT SCOPE_IDENTITY();"
                    'Using cmd As New SqlCommand(sql1, conn)
                    '    cmd.Parameters.AddWithValue("@Code", DBNull.Value)
                    '    cmd.Parameters.AddWithValue("@EngName", row.Cells.FromKey("CriteriaName").Text)
                    '    cmd.Parameters.AddWithValue("@ArbName", row.Cells.FromKey("CriteriaName").Text)
                    '    cmd.Parameters.AddWithValue("@Score", row.Cells.FromKey("AppraisalScore").Text)
                    '    cmd.Parameters.AddWithValue("@CriteriaGroupID", row.Cells.FromKey("CriteriaGroupID").Value)
                    '    cmd.Parameters.AddWithValue("@RegUserID", User)
                    '    criteriaId = Convert.ToInt32(cmd.ExecuteScalar())
                    'End Using

                    ' 2. Insert into APP_AppraisalCriterias
                    Dim sql2 As String = "INSERT INTO APP_AppraisalCriterias (AppraisalID,CriteriaGroupID, CriteriaID, ByValue, ByPercentage, MinimumScore, MaximumScore, Weight, RegUserID, RegDate) " &
                                         "VALUES (@AppraisalID,@CriteriaGroupID, @CriteriaID, @ByValue, @ByPercentage, @MinimumScore, @MaximumScore, @Weight, @RegUserID, GETDATE());"
                    Using cmd2 As New SqlCommand(sql2, conn)
                        cmd2.Parameters.AddWithValue("@AppraisalID", AppraisalID)
                        cmd2.Parameters.AddWithValue("@CriteriaID", row.Cells.FromKey("criteriaId").Value)
                        cmd2.Parameters.AddWithValue("@CriteriaGroupID", row.Cells.FromKey("CriteriaGroupID").Value)
                        cmd2.Parameters.AddWithValue("@ByValue", True)
                        cmd2.Parameters.AddWithValue("@ByPercentage", False)
                        cmd2.Parameters.AddWithValue("@MinimumScore", row.Cells.FromKey("MinimumScore").Value)
                        cmd2.Parameters.AddWithValue("@MaximumScore", row.Cells.FromKey("MaximumScore").Value)
                        cmd2.Parameters.AddWithValue("@Weight", row.Cells.FromKey("Weight").Value)
                        cmd2.Parameters.AddWithValue("@RegUserID", User)
                        cmd2.ExecuteNonQuery()
                    End Using

                Next

            End Using

        Catch ex As Exception
            mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Private Sub SaveEmployeePoints(appraisalId As Integer,
                                    employeeId As Integer,
                                    configurationLevel As Integer,
                                    strength As String,
                                    weakness As String)
        Dim cs As String = ConfigurationManager.AppSettings("Connstring")

        Dim sql As String =
        "SET NOCOUNT ON; " &
        "UPDATE dbo.App_AppraisalEmployeePoints " &
        "   SET StrengthPoints = @StrengthPoints, " &
        "       WeaknessPoints = @WeaknessPoints, " &
        "       ConfigurationLevel = @ConfigurationLevel " &
        " WHERE AppraisalId = @AppraisalId AND EmployeeId = @EmployeeId; " &
        "IF @@ROWCOUNT = 0 " &
        "BEGIN " &
        "   INSERT INTO dbo.App_AppraisalEmployeePoints " &
        "       (AppraisalId, EmployeeId, ConfigurationLevel, StrengthPoints, WeaknessPoints) " &
        "   VALUES (@AppraisalId, @EmployeeId, @ConfigurationLevel, @StrengthPoints, @WeaknessPoints); " &
        "END;"

        Using cn As New SqlConnection(cs)
            Using cmd As New SqlCommand(sql, cn)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add("@AppraisalId", SqlDbType.Int).Value = appraisalId
                cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employeeId
                cmd.Parameters.Add("@ConfigurationLevel", SqlDbType.Int).Value = configurationLevel
                cmd.Parameters.Add("@StrengthPoints", SqlDbType.NVarChar, 2000).Value =
                If(String.IsNullOrWhiteSpace(strength), CType(DBNull.Value, Object), strength.Trim())
                cmd.Parameters.Add("@WeaknessPoints", SqlDbType.NVarChar, 2000).Value =
                If(String.IsNullOrWhiteSpace(weakness), CType(DBNull.Value, Object), weakness.Trim())

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function InsertEmployeeImprovements(appraisalId As Integer, employeeId As Integer, configurationLevel As Integer) As Integer
        ' Requirements:
        ' - Grid: uwgImprovements with columns GroupName, ImproveName, Remarks
        ' - Only non-empty ImproveName rows are inserted
        If appraisalId <= 0 OrElse employeeId <= 0 Then Return 0
        If UwgSearchEmployees Is Nothing OrElse UwgSearchEmployees.Rows Is Nothing Then Return 0

        Dim inserted As Integer = 0
        Dim connStr As String = System.Configuration.ConfigurationManager.AppSettings("Connstring").ToString()

        Using con As New SqlClient.SqlConnection(connStr)
            con.Open()
            Using trn = con.BeginTransaction()
                Try
                    Const sql As String = "INSERT INTO [dbo].[App_AppraisalEmployeeImprovements] " &
                                      "([AppraisalId],[EmployeeId],[ConfigurationLevel],[GroupName],[ImproveName],[Remarks]) " &
                                      "VALUES (@AppraisalId,@EmployeeId,@ConfigurationLevel,@GroupName,@ImproveName,@Remarks);"

                    For Each r As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        Dim improveName As String = ""
                        If r.Cells.FromKey("ImproveName") IsNot Nothing AndAlso r.Cells.FromKey("ImproveName").Value IsNot Nothing Then
                            improveName = Convert.ToString(r.Cells.FromKey("ImproveName").Value).Trim()
                        End If
                        ' Skip rows without an improve name
                        If String.IsNullOrEmpty(improveName) Then Continue For

                        Dim groupName As String = ""
                        If r.Cells.FromKey("GroupName") IsNot Nothing AndAlso r.Cells.FromKey("GroupName").Value IsNot Nothing Then
                            groupName = Convert.ToString(r.Cells.FromKey("GroupName").Value).Trim()
                        End If

                        Dim remarks As String = ""
                        If r.Cells.FromKey("Remarks") IsNot Nothing AndAlso r.Cells.FromKey("Remarks").Value IsNot Nothing Then
                            remarks = Convert.ToString(r.Cells.FromKey("Remarks").Value).Trim()
                        End If

                        Using cmd As New SqlClient.SqlCommand(sql, con, trn)
                            cmd.CommandType = CommandType.Text
                            cmd.Parameters.AddWithValue("@AppraisalId", appraisalId)
                            cmd.Parameters.AddWithValue("@EmployeeId", employeeId)
                            cmd.Parameters.AddWithValue("@ConfigurationLevel", configurationLevel)
                            cmd.Parameters.AddWithValue("@GroupName", If(String.IsNullOrEmpty(groupName), CType(DBNull.Value, Object), groupName))
                            cmd.Parameters.AddWithValue("@ImproveName", improveName)
                            cmd.Parameters.AddWithValue("@Remarks", If(String.IsNullOrEmpty(remarks), CType(DBNull.Value, Object), remarks))
                            inserted += cmd.ExecuteNonQuery()
                        End Using
                    Next

                    trn.Commit()
                Catch
                    trn.Rollback()
                    Throw
                End Try
            End Using
        End Using

        Return inserted
    End Function
    Private Function SendNextLevelNotification()
        Dim ClsEmployees As New Clshrs_Employees(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim connection As New SqlConnection(ClsEmployees.ConnectionString)
        Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
        Dim AppraisalID As Integer = Request.QueryString.Item("AppraisalID")
        Dim NotificationID As Integer = Request.QueryString.Item("NotificationID")
        Dim _sys_User As New Clssys_Users(Page)
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim User As String = String.Empty

        WebHandler.GetCookies(Page, "UserID", User)

        _sys_User.Find("ID = '" & User & "'")

        ClsEmployees.Find("Code='" & _sys_User.Code & "'")
        Try

            Dim NotificationDue As Boolean = False
            Dim Cls_DataAcessLayer As New ClsDataAcessLayer(Page)
            Dim connectionString As String = ConfigurationManager.AppSettings("Connstring").ToString()

            Dim StrCheckManager As String = " select Appraisaltypeid from App_Appraisals where id=" & AppraisalID & " "

            Dim Appraisaltypeid As Integer = SqlHelper.ExecuteScalar(connectionString, CommandType.Text, StrCheckManager)


            Dim currentLevel As Integer = 0

            Using cn As New SqlConnection(connectionString)
                cn.Open()

                Using cmdLevel As New SqlCommand("SELECT ConfigurationLevel FROM App_AppraisalNotifications WHERE Id=@NotificationID", cn)
                    cmdLevel.Parameters.AddWithValue("@NotificationID", NotificationID)
                    Dim obj = cmdLevel.ExecuteScalar()
                    If obj IsNot Nothing AndAlso obj IsNot DBNull.Value Then
                        currentLevel = Convert.ToInt32(obj)
                    End If
                End Using

                Dim dsConfig As New DataSet()
                Using cmdCfg As New SqlCommand("SELECT * FROM App_AppraisalConfigurations WHERE Appraisaltypeid=@Appraisaltypeid AND Rank=@NextRank", cn)
                    cmdCfg.Parameters.AddWithValue("@Appraisaltypeid", Appraisaltypeid)
                    cmdCfg.Parameters.AddWithValue("@NextRank", currentLevel + 1)
                    Using adp As New SqlDataAdapter(cmdCfg)
                        adp.Fill(dsConfig)
                    End Using
                End Using

                If dsConfig.Tables.Count > 0 AndAlso dsConfig.Tables(0).Rows.Count > 0 Then
                    Using trn = cn.BeginTransaction()
                        Try
                            Using insertCmd As New SqlCommand("INSERT INTO [dbo].[App_AppraisalNotifications] ([AppraisalID],[APP_EmployeeID],[EmployeeID],[ConfigurationLevel],ConfigurationID,RegDate,RegUserID) VALUES (@AppraisalID,@AppEmployeeID,@EmployeeID,@ConfigurationLevel,@ConfigurationID,GETDATE(),@RegUserID)", cn, trn)
                                insertCmd.Parameters.Add("@AppraisalID", SqlDbType.Int)
                                insertCmd.Parameters.Add("@AppEmployeeID", SqlDbType.Int)
                                insertCmd.Parameters.Add("@EmployeeID", SqlDbType.Int)
                                insertCmd.Parameters.Add("@ConfigurationLevel", SqlDbType.Int)
                                insertCmd.Parameters.Add("@ConfigurationID", SqlDbType.Int)
                                insertCmd.Parameters.Add("@RegUserID", SqlDbType.Int)

                                For Each row In dsConfig.Tables(0).Rows
                                    Dim userType As Integer = Convert.ToInt32(row("UserTypeID"))
                                    Dim rank As Integer = Convert.ToInt32(row("Rank"))
                                    Dim configId As Integer = Convert.ToInt32(row("ID"))

                                    If userType = 1 Then
                                        Dim dsEmployees As DataTable = GetAppraisalEmployeesIthDirectManager(AppraisalID)
                                        For Each er In dsEmployees.Rows
                                            insertCmd.Parameters("@AppraisalID").Value = AppraisalID
                                            insertCmd.Parameters("@AppEmployeeID").Value = Convert.ToInt32(er("ManagerID"))
                                            insertCmd.Parameters("@EmployeeID").Value = Convert.ToInt32(er("EmployeeID"))
                                            insertCmd.Parameters("@ConfigurationLevel").Value = rank
                                            insertCmd.Parameters("@ConfigurationID").Value = configId
                                            insertCmd.Parameters("@RegUserID").Value = _sys_User.ID
                                            insertCmd.ExecuteNonQuery()
                                        Next
                                    ElseIf userType = 2 Then
                                        Dim dsPos As DataTable = GetAppraisalEmployeesInPosition(Convert.ToInt32(row("PositionID")))
                                        For Each er In dsPos.Rows
                                            insertCmd.Parameters("@AppraisalID").Value = AppraisalID
                                            insertCmd.Parameters("@AppEmployeeID").Value = Convert.ToInt32(er("EmployeeID"))
                                            insertCmd.Parameters("@EmployeeID").Value = EmployeeID
                                            insertCmd.Parameters("@ConfigurationLevel").Value = rank
                                            insertCmd.Parameters("@ConfigurationID").Value = configId
                                            insertCmd.Parameters("@RegUserID").Value = _sys_User.ID
                                            insertCmd.ExecuteNonQuery()
                                        Next
                                    ElseIf userType = 3 Then
                                        insertCmd.Parameters("@AppraisalID").Value = AppraisalID

                                        insertCmd.Parameters("@AppEmployeeID").Value = Convert.ToInt32(row("EmployeeID"))
                                        insertCmd.Parameters("@EmployeeID").Value = EmployeeID
                                        insertCmd.Parameters("@ConfigurationLevel").Value = rank
                                        insertCmd.Parameters("@ConfigurationID").Value = configId
                                        insertCmd.Parameters("@RegUserID").Value = _sys_User.ID
                                        insertCmd.ExecuteNonQuery()
                                    ElseIf userType = 4 Then
                                        insertCmd.Parameters("@AppraisalID").Value = AppraisalID
                                        insertCmd.Parameters("@AppEmployeeID").Value = EmployeeID
                                        insertCmd.Parameters("@EmployeeID").Value = EmployeeID
                                        insertCmd.Parameters("@ConfigurationLevel").Value = rank
                                        insertCmd.Parameters("@ConfigurationID").Value = configId
                                        insertCmd.Parameters("@RegUserID").Value = _sys_User.ID
                                        insertCmd.ExecuteNonQuery()

                                    End If
                                Next
                            End Using
                            trn.Commit()
                        Catch
                            trn.Rollback()
                            Throw
                        End Try
                    End Using
                End If
            End Using
            Cls_DataAcessLayer.SendEmail("FrmAppraisalEvaluation", Me.Page, 1, "V_AppraisalDueNotification", EmployeeID)

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployees.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Function

#End Region

#Region "Private Function"


    Private Function GetAppraisalEmployeesIthDirectManager(AppraisalID As Integer) As DataTable
        Dim EmployeeID As Integer = Request.QueryString.Item("EmployeeID")
        Dim ConnectionString As String = ConfigurationManager.AppSettings("Connstring").ToString()
        Using cn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand("SELECT  id AS EmployeeID,  ManagerID FROM hrs_Employees  WHERE  id=@EmployeeID ", cn)
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                Using adp As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    adp.Fill(ds)
                    If ds.Tables.Count > 0 Then Return ds.Tables(0)
                End Using
            End Using
        End Using
        Return New DataTable()
    End Function

    Private Function GetAppraisalEmployeesInPosition(PositionID As Integer) As DataTable
        Dim ConnectionString As String = ConfigurationManager.AppSettings("Connstring").ToString()
        Using cn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand("SELECT EmployeeID FROM Hrs_Contracts WHERE CancelDate IS NULL AND (EndDate IS NULL OR EndDate > GETDATE()) AND PositionID=@PositionID", cn)
                cmd.Parameters.AddWithValue("@PositionID", PositionID)
                Using adp As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    adp.Fill(ds)
                    If ds.Tables.Count > 0 Then Return ds.Tables(0)
                End Using
            End Using
        End Using
        Return New DataTable()
    End Function

    Private Function GetAppraisalEmployees(AppraisalID As Integer) As DataTable
        Dim ConnectionString As String = ConfigurationManager.AppSettings("Connstring").ToString()
        Using cn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand("SELECT EmployeeID FROM APP_AppraisalEmployees WHERE AppraisalID=@AppraisalID", cn)
                cmd.Parameters.AddWithValue("@AppraisalID", AppraisalID)
                Using adp As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    adp.Fill(ds)
                    If ds.Tables.Count > 0 Then Return ds.Tables(0)
                End Using
            End Using
        End Using
        Return New DataTable()
    End Function

#End Region

End Class
