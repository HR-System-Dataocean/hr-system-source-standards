Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Venus.Shared.Web
Imports OfficeOpenXml
Imports System.Security.Cryptography
Imports System.Collections.ObjectModel
Imports System.Activities.Expressions
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO



Partial Class frmVacationsBalanceShow
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
                'ddlFromDate.Value = ClsEmployeesVacations.GetHigriDate("01/01/" & DateTime.Now.Year.ToString())
                'ddlToDate.Value = ClsEmployeesVacations.GetHigriDate(DateTime.Now.ToString("dd/MM/yyyy"))

                Dim filter As String
                'filter = " and RegisterDate>='" & Convert.ToDateTime(ddlFromDate.Value.ToString()).ToString("yyyy-MM-dd") & "' and cast( RegisterDate as date)<='" & Convert.ToDateTime(ddlToDate.Value).ToString("yyyy-MM-dd") & "'"
                filter = ""

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
                Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")

                ClsDepartment.GetDropDownList(ddlDepartment, True)

                FillDdlYear()

                FillDdlRequestTypes()
                Dim firstDayOfYear As Date = New DateTime(DdlYear.SelectedValue, 1, 1)
                filter += " and (CONVERT(date,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN hrs_VacationsBalance.ExpireDate WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN hrs_VacationsBalance.ExpireDate END,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN 23 WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN 103 END)>='" & firstDayOfYear & "') "
                GetAllEmployeeRequests(filter)
            Catch ex As Exception
                mErrorHandler = New Venus.Shared.ErrorsHandler(ClsEmployeesVacations.ConnectionString)
                Page.Session.Add("ErrorValue", ex)
                mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployeesVacations.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
                Page.Response.Redirect("ErrorPage.aspx")
            End Try

        End If



    End Sub
    Protected Sub TxtDirectMngrCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDirectMngrCode.TextChanged
        Try
            ClsEmployees = New Clshrs_Employees(Page)

            If Not String.IsNullOrEmpty(TxtDirectMngrCode.Text) Then
                Dim ClsEmployees = New Clshrs_Employees(Page)
                Dim EmpName As String
                If ProfileCls.CurrentLanguage = "Ar" Then
                    EmpName = " isnull( hrs_Employees.arbname ,' ')+' '+ isnull(hrs_Employees.FatherArbName, ' ')+' '+ isnull(hrs_Employees.GrandArbName,' ')+' '+isnull(hrs_Employees.FamilyArbName,' ') "

                Else
                    EmpName = " isnull(hrs_Employees.EngName,' ')+' '+isnull(hrs_Employees.FatherEngName,' ')+' '+isnull(hrs_Employees.GrandEngName ,' ')+' '+isnull(hrs_Employees.FamilyEngName,' ')"

                End If
                'TxtEmpName.Text = EmpName
                Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

                Dim DS2 As New Data.DataSet()
                Dim connetionString2 As String
                Dim connection2 As Data.SqlClient.SqlConnection
                Dim command2 As Data.SqlClient.SqlCommand
                Dim adapter2 As New Data.SqlClient.SqlDataAdapter
                connetionString2 = ClsEmployees.ConnectionString
                connection2 = New Data.SqlClient.SqlConnection(connetionString2)
                Dim strselect As String
                strselect = "select " & EmpName & "  FROM  hrs_Employees where Code='" & TxtDirectMngrCode.Text & "'"
                'command2 = New Data.SqlClient.SqlCommand(strselect, connection2)

                Dim EmployeeName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connetionString2, Data.CommandType.Text, strselect)
                ClsEmployees.Find("Code='" & TxtDirectMngrCode.Text & "'")
                If ClsEmployees.ID > 0 Then

                    TxtDirectMngrName.Text = EmployeeName

                Else
                    TxtDirectMngrName.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Sorry there is no employee with this code !/!عفوا لا يوجد موظف مسجل بهذا الكود"))

                End If
            Else
                TxtDirectMngrName.Text = ""
            End If

            Dim filter As String
            filter = ""
            If DdlYear.SelectedValue > 0 Then
                Dim firstDayOfYear As Date = New DateTime(DdlYear.SelectedValue, 1, 1)
                filter += " and (CONVERT(date,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN hrs_VacationsBalance.ExpireDate WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN hrs_VacationsBalance.ExpireDate END,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN 23 WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN 103 END)>='" & firstDayOfYear & "') "
            End If
            If ddlBalanceType.SelectedValue > 0 Then
                filter += " and hrs_VacationsBalance.BalanceTypeID=" & ddlBalanceType.SelectedValue
            End If
            If ddlDepartment.SelectedIndex > 0 Then
                filter += "and sys_Departments.ID=" & ddlDepartment.SelectedValue & ""
            End If
            If txtEmpCode.Text <> "" Then
                filter += " and hrs_Employees.Code='" & txtEmpCode.Text & "'"
            End If
            If TxtDirectMngrCode.Text <> "" Then
                ClsEmployees.Find("Code='" & TxtDirectMngrCode.Text & "'")

                filter += " and hrs_Employees.ManagerID=" & ClsEmployees.ID
            End If
            GetAllEmployeeRequests(filter)

        Catch ex As Exception
            Dim s = ex.Data
        End Try
    End Sub

    Protected Sub txtEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmpCode.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtEmpCode.Text) Then
                Dim ClsEmployees = New Clshrs_Employees(Page)
                Dim EmpName As String
                If ProfileCls.CurrentLanguage = "Ar" Then
                    EmpName = " isnull( hrs_Employees.arbname ,' ')+' '+ isnull(hrs_Employees.FatherArbName, ' ')+' '+ isnull(hrs_Employees.GrandArbName,' ')+' '+isnull(hrs_Employees.FamilyArbName,' ') "

                Else
                    EmpName = " isnull(hrs_Employees.EngName,' ')+' '+isnull(hrs_Employees.FatherEngName,' ')+' '+isnull(hrs_Employees.GrandEngName ,' ')+' '+isnull(hrs_Employees.FamilyEngName,' ')"

                End If
                'TxtEmpName.Text = EmpName
                Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

                Dim DS2 As New Data.DataSet()
                Dim connetionString2 As String
                Dim connection2 As Data.SqlClient.SqlConnection
                Dim command2 As Data.SqlClient.SqlCommand
                Dim adapter2 As New Data.SqlClient.SqlDataAdapter
                connetionString2 = ClsEmployees.ConnectionString
                connection2 = New Data.SqlClient.SqlConnection(connetionString2)
                Dim strselect As String
                strselect = "select " & EmpName & "  FROM  hrs_Employees where Code='" & txtEmpCode.Text & "'"
                'command2 = New Data.SqlClient.SqlCommand(strselect, connection2)

                Dim EmployeeName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connetionString2, Data.CommandType.Text, strselect)
                Dim ClsEmployees2 = New Clshrs_Employees(Page)

                ClsEmployees2.Find("Code='" & txtEmpCode.Text & "'")
                If ClsEmployees2.ID > 0 Then

                    TxtEmpName.Text = EmployeeName

                Else
                    TxtEmpName.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Sorry there is no employee with this code !/!عفوا لا يوجد موظف مسجل بهذا الكود"))

                End If
            Else
                TxtEmpName.Text = ""
            End If

            Dim filter As String
            filter = ""
            If DdlYear.SelectedValue > 0 Then
                Dim firstDayOfYear As Date = New DateTime(DdlYear.SelectedValue, 1, 1)
                filter += " and (CONVERT(date,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN hrs_VacationsBalance.ExpireDate WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN hrs_VacationsBalance.ExpireDate END,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN 23 WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN 103 END)>='" & firstDayOfYear & "') "
            End If
            If ddlBalanceType.SelectedValue > 0 Then
                filter += " and hrs_VacationsBalance.BalanceTypeID=" & ddlBalanceType.SelectedValue
            End If
            If ddlDepartment.SelectedIndex > 0 Then
                filter += "and sys_Departments.ID=" & ddlDepartment.SelectedValue & ""
            End If
            If txtEmpCode.Text <> "" Then
                filter += " and hrs_Employees.Code='" & txtEmpCode.Text & "'"
            End If
            If TxtDirectMngrCode.Text <> "" Then
                Dim ClsEmployees2 = New Clshrs_Employees(Page)

                ClsEmployees2.Find("Code= '" & TxtDirectMngrCode.Text & "'")
                filter += " and hrs_Employees.ManagerID=" & ClsEmployees2.ID
            End If
            GetAllEmployeeRequests(filter)

        Catch ex As Exception
            Dim s = ex.Data
        End Try
    End Sub
    Protected Sub DdlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlYear.SelectedIndexChanged
        Dim filter As String
        filter = ""
        If DdlYear.SelectedValue > 0 Then
            Dim firstDayOfYear As Date = New DateTime(DdlYear.SelectedValue, 1, 1)
            filter += " and (CONVERT(date,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN hrs_VacationsBalance.ExpireDate WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN hrs_VacationsBalance.ExpireDate END,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN 23 WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN 103 END)>='" & firstDayOfYear & "') "
        End If
        If ddlBalanceType.SelectedValue > 0 Then
            filter += " and hrs_VacationsBalance.BalanceTypeID=" & ddlBalanceType.SelectedValue
        End If
        If txtEmpCode.Text <> "" Then
            filter += " and hrs_Employees.Code='" & txtEmpCode.Text & "'"
        End If
        If ddlDepartment.SelectedIndex > 0 Then
            filter += "and sys_Departments.ID=" & ddlDepartment.SelectedValue & ""
        End If
        If TxtDirectMngrCode.Text <> "" Then
            ClsEmployees.Find("Code= '" & TxtDirectMngrCode.Text & "'")
            filter += " and hrs_Employees.ManagerID=" & ClsEmployees.ID
        End If
        GetAllEmployeeRequests(filter)

    End Sub
    Protected Sub DdlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        Dim filter As String
        filter = ""
        If DdlYear.SelectedValue > 0 Then
            Dim firstDayOfYear As Date = New DateTime(DdlYear.SelectedValue, 1, 1)
            filter += " and (CONVERT(date,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN hrs_VacationsBalance.ExpireDate WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN hrs_VacationsBalance.ExpireDate END,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN 23 WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN 103 END)>='" & firstDayOfYear & "') "
        End If
        If ddlBalanceType.SelectedValue > 0 Then
            filter += " and hrs_VacationsBalance.BalanceTypeID=" & ddlBalanceType.SelectedValue
        End If
        If txtEmpCode.Text <> "" Then
            filter += " and hrs_Employees.Code='" & txtEmpCode.Text & "'"
        End If
        If ddlDepartment.SelectedIndex > 0 Then
            filter += "and sys_Departments.ID=" & ddlDepartment.SelectedValue & ""
        End If
        If TxtDirectMngrCode.Text <> "" Then
            ClsEmployees.Find("Code= '" & TxtDirectMngrCode.Text & "'")
            filter += " and hrs_Employees.ManagerID=" & ClsEmployees.ID
        End If
        GetAllEmployeeRequests(filter)

    End Sub

    Protected Sub ddlBalanceType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBalanceType.SelectedIndexChanged
        Dim filter As String
        filter = ""
        If DdlYear.SelectedValue > 0 Then
            Dim firstDayOfYear As Date = New DateTime(DdlYear.SelectedValue, 1, 1)
            filter += " and (CONVERT(date,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN hrs_VacationsBalance.ExpireDate WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN hrs_VacationsBalance.ExpireDate END,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN 23 WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN 103 END)>='" & firstDayOfYear & "') "
        End If
        If ddlBalanceType.SelectedValue > 0 Then
            filter += " and hrs_VacationsBalance.BalanceTypeID=" & ddlBalanceType.SelectedValue
        End If
        If txtEmpCode.Text <> "" Then
            filter += " and hrs_Employees.Code='" & txtEmpCode.Text & "'"
        End If
        If ddlDepartment.SelectedIndex > 0 Then
            filter += "and sys_Departments.ID=" & ddlDepartment.SelectedValue & ""
        End If
        GetAllEmployeeRequests(filter)

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



    Protected Sub ImageButton_Excel_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Excel.Command
        Dim filter As String
        filter = ""
        If DdlYear.SelectedValue > 0 Then
            filter += " and (hrs_VacationsBalance.Year=" & DdlYear.SelectedValue & " Or (Year= " & DdlYear.SelectedValue & "-1 and hrs_EmployeesClasses.AccumulatedBalance=1))"
        End If
        If ddlBalanceType.SelectedValue > 0 Then
            filter += " and hrs_VacationsBalance.BalanceTypeID=" & ddlBalanceType.SelectedValue
        End If
        If txtEmpCode.Text <> "" Then
            filter += " and hrs_Employees.Code='" & txtEmpCode.Text & "'"
        End If
        If ddlDepartment.SelectedIndex > 0 Then
            filter += "and sys_Departments.ID=" & ddlDepartment.SelectedValue & ""
        End If
        Dim ClsEmployees As New Clshrs_Employees(Me)

        If TxtDirectMngrCode.Text <> "" Then

            ClsEmployees.Find("Code= '" & TxtDirectMngrCode.Text & "'")
            filter += " and hrs_Employees.ManagerID=" & ClsEmployees.ID
        End If
        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")
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
        Dim typeName As String
        If ProfileCls.CurrentLanguage = "Ar" Then
            EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
            typeName = "hrs_VacationsBalanceType.ArbName"
        Else
            EmpName = "[dbo].[fn_GetEmpName](hrs_Employees.Code,0)"
            typeName = "hrs_VacationsBalanceType.EngName"
        End If

        Dim str1 As String = ""

        str1 = "SELECT  hrs_Employees.Code," & EmpName & " as EmployeeName ,hrs_Positions.EngName as Position, sys_sectors.EngName As Sector,sys_Locations.EngName As Unit,sys_Departments.EngName as Department,hrs_VacationsBalance.Year,Round((SELECT sum(Balance) from hrs_VacationsBalance as v where v.EmployeeID=hrs_VacationsBalance.EmployeeID and v.BalanceTypeID=2 and v.Year=hrs_VacationsBalance.Year),0) as BalanceTransfered,Round((SELECT sum(Balance) from hrs_VacationsBalance as v where v.EmployeeID=hrs_VacationsBalance.EmployeeID and v.BalanceTypeID=1 and v.Year=hrs_VacationsBalance.Year),0) as BalanceNew,Round(Sum(hrs_VacationsBalance.Balance),0) as Balance,Round((SELECT sum(Balance) from hrs_VacationsBalance as v where v.EmployeeID=hrs_VacationsBalance.EmployeeID and v.BalanceTypeID=3 and v.Year=hrs_VacationsBalance.Year),0) as Comp,Round(Sum(hrs_VacationsBalance.Consumed),0 )as Consumed,Round(Sum(hrs_VacationsBalance.Remaining),0) as Remaining  ,Round(Sum(case when ExpireDate > getdate() then hrs_VacationsBalance.Remaining end ),0) as Actualbalance FROM            hrs_VacationsBalance INNER JOIN  hrs_Employees ON hrs_VacationsBalance.EmployeeID = hrs_Employees.ID INNER JOIN hrs_VacationsBalanceType ON hrs_VacationsBalance.BalanceTypeID = hrs_VacationsBalanceType.ID  join hrs_Contracts on hrs_Contracts.EmployeeID=hrs_Employees.ID and (hrs_Contracts.EndDate>getdate() or hrs_Contracts.EndDate is null) left join hrs_Positions on hrs_Contracts.PositionID=hrs_Positions.ID left  join sys_Departments on hrs_Employees.DepartmentID=sys_Departments.ID left join sys_Sectors on sys_Sectors.ID=hrs_Employees.SectorID left join sys_Locations on sys_Locations.ID=hrs_Employees.LocationID left join hrs_EmployeesClasses on hrs_Contracts.EmployeeClassID=hrs_EmployeesClasses.ID where hrs_VacationsBalance.EndServiceDate IS NULL " + filter + " group by hrs_VacationsBalance.EmployeeID,hrs_Employees.Code,hrs_VacationsBalance.Year ,hrs_Positions.EngName , sys_Departments.EngName , sys_Sectors.EngName ,sys_Locations.EngName  "
        command = New Data.SqlClient.SqlCommand(str1, connection)
        adapter.SelectCommand = command
        adapter.Fill(DS1, "Table1")

        connection.Close()
        CreateExcelFile(DS1.Tables(0), "Vacation Balance")
    End Sub

    Public Sub CreateExcelFile(ByVal dt As DataTable, ByVal sheetName As String)
        ' Enable EPPlus to use non-commercial license
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial

        ' Create a new Excel package
        Using package As New ExcelPackage()
            ' Add a new worksheet
            Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets.Add(sheetName)

            ' Add DataTable content to the worksheet
            Dim startRow As Integer = 1
            Dim startCol As Integer = 1

            ' Add column headers
            For col As Integer = 0 To dt.Columns.Count - 1
                worksheet.Cells(startRow, startCol + col).Value = dt.Columns(col).ColumnName
                worksheet.Cells(startRow, startCol + col).Style.Font.Bold = True
            Next

            ' Add rows
            For row As Integer = 0 To dt.Rows.Count - 1
                For col As Integer = 0 To dt.Columns.Count - 1
                    worksheet.Cells(startRow + row + 1, startCol + col).Value = dt.Rows(row)(col)
                Next
            Next

            ' Auto-fit columns for better readability
            worksheet.Cells(worksheet.Dimension.Address).AutoFitColumns()

            ' Save the Excel package to a MemoryStream
            Using memoryStream As New MemoryStream()
                package.SaveAs(memoryStream)
                memoryStream.Seek(0, SeekOrigin.Begin)

                ' Write the Excel file to a temporary file
                Dim tempFilePath As String = Path.GetTempFileName() & ".xlsx"
                File.WriteAllBytes(tempFilePath, memoryStream.ToArray())

                ' Open the Excel file
                Process.Start("explorer.exe", tempFilePath)


                Dim bytes As Byte() = memoryStream.ToArray()

                memoryStream.Close()

                Response.Clear()
                Response.ContentType = "application/force-download"
                Response.AddHeader("content-disposition", "attachment;    filename=" & sheetName & "-" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls")
                Response.BinaryWrite(bytes)
                Response.End()

            End Using
        End Using
    End Sub




#End Region
#Region "Private Functions"

    Private Sub FillDdlYear()
        DdlYear.Items.Clear()
        Dim ClsEmployees = New Clshrs_Employees(Page)

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
        DdlYear.SelectedValue = DateTime.Now.Year
    End Sub
    Private Sub FillDdlRequestTypes()
        ddlBalanceType.Items.Clear()
        ClsEmployees = New Clshrs_Employees(Page)

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        Item = New Global.System.Web.UI.WebControls.ListItem
        Item.Text = ObjNavigationHandler.SetLanguage(Page, "[Select Your Choice]/[ برجاء الاختيار ]")
        Item.Value = 0
        ddlBalanceType.Items.Add(Item)
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
            RequestName = "ArbName"
        Else
            RequestName = "EngName"
        End If

        Dim str1 As String = "select ID," & RequestName & " as RequestName from hrs_VacationsBalanceType "
        command = New Data.SqlClient.SqlCommand(str1, connection)
        adapter.SelectCommand = command
        adapter.Fill(DS1, "Table1")

        connection.Close()
        If DS1.Tables(0).Rows.Count > 0 Then
            For Each Row In DS1.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Value = (Row("ID"))
                Item.Text = Row("RequestName")
                ddlBalanceType.Items.Add(Item)

            Next
        End If

    End Sub
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



    Public Sub GetAllEmployeeRequests(ByVal filter As String)
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
            Dim typeName As String
            If ProfileCls.CurrentLanguage = "Ar" Then
                EmpName = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                typeName = "hrs_VacationsBalanceType.ArbName"
            Else
                EmpName = "[dbo].[fn_GetEmpName](hrs_Employees.Code,0)"
                typeName = "hrs_VacationsBalanceType.EngName"
            End If

            Dim str1 As String = ""

            str1 = "SELECT  hrs_Employees.Code," & EmpName & " as EmployeeName,hrs_Positions.EngName as Position, sys_Departments.EngName as Department,Round((SELECT sum(Balance) from hrs_VacationsBalance as v where v.EmployeeID=hrs_VacationsBalance.EmployeeID and v.BalanceTypeID=2 and CONVERT(date,CASE WHEN v.ExpireDate LIKE '____-__-__' THEN v.ExpireDate WHEN v.ExpireDate LIKE '__/__/____' THEN v.ExpireDate END,CASE WHEN v.ExpireDate LIKE '____-__-__' THEN 23 WHEN v.ExpireDate LIKE '__/__/____' THEN 103 END)>=getdate() and v.CancelDate is NULL and ISNULL(v.Posted,0)=0),0) as BalanceTransfered,Round((SELECT sum(Balance) from hrs_VacationsBalance as v where v.EmployeeID=hrs_VacationsBalance.EmployeeID and v.BalanceTypeID=1 and CONVERT(date,CASE WHEN v.ExpireDate LIKE '____-__-__' THEN v.ExpireDate WHEN v.ExpireDate LIKE '__/__/____' THEN v.ExpireDate END,CASE WHEN v.ExpireDate LIKE '____-__-__' THEN 23 WHEN v.ExpireDate LIKE '__/__/____' THEN 103 END)>=getdate() and v.CancelDate is NULL and ISNULL(v.Posted,0)=0),0) as BalanceNew,Round((SELECT sum(Balance) from hrs_VacationsBalance as v where v.EmployeeID=hrs_VacationsBalance.EmployeeID and v.BalanceTypeID=3 and CONVERT(date,CASE WHEN v.ExpireDate LIKE '____-__-__' THEN v.ExpireDate WHEN v.ExpireDate LIKE '__/__/____' THEN v.ExpireDate END,CASE WHEN v.ExpireDate LIKE '____-__-__' THEN 23 WHEN v.ExpireDate LIKE '__/__/____' THEN 103 END)>=getdate() and v.CancelDate is NULL and ISNULL(v.Posted,0)=0),0) as Comp,Round(Sum(hrs_VacationsBalance.Balance),0) as Balance,Round(Sum(hrs_VacationsBalance.Consumed),0) as Consumed,Round(Sum(hrs_VacationsBalance.Remaining),0) as Remaining  ,Round(Sum(case when CONVERT(date,CASE WHEN ExpireDate LIKE '____-__-__' THEN ExpireDate WHEN ExpireDate LIKE '__/__/____' THEN ExpireDate END,CASE WHEN ExpireDate LIKE '____-__-__' THEN 23 WHEN ExpireDate LIKE '__/__/____' THEN 103 END)>getdate() then hrs_VacationsBalance.Remaining end ),0) as Actualbalance FROM            hrs_VacationsBalance INNER JOIN  hrs_Employees ON hrs_VacationsBalance.EmployeeID = hrs_Employees.ID INNER JOIN hrs_VacationsBalanceType ON hrs_VacationsBalance.BalanceTypeID = hrs_VacationsBalanceType.ID left join hrs_Contracts on hrs_Contracts.EmployeeID=hrs_Employees.ID and (hrs_Contracts.EndDate>getdate() or hrs_Contracts.EndDate is null) join hrs_Positions on hrs_Contracts.PositionID=hrs_Positions.ID left join sys_Departments on hrs_Employees.DepartmentID=sys_Departments.ID left join hrs_EmployeesClasses on hrs_Contracts.EmployeeClassID=hrs_EmployeesClasses.ID where hrs_VacationsBalance.EndServiceDate IS NULL and hrs_VacationsBalance.CancelDate is NULL and ISNULL(Posted,0)=0 " + filter + " group by hrs_VacationsBalance.EmployeeID,hrs_Employees.Code ,hrs_Positions.EngName , sys_Departments.EngName  "
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
    Protected Sub ImageButton_Refresh_Click(sender As Object, e As System.EventArgs) Handles ImageButton_Refresh.Click
        Dim filter As String
        filter = ""
        If DdlYear.SelectedValue > 0 Then
            Dim firstDayOfYear As Date = New DateTime(DdlYear.SelectedValue, 1, 1)
            filter += " and (CONVERT(date,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN hrs_VacationsBalance.ExpireDate WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN hrs_VacationsBalance.ExpireDate END,CASE WHEN hrs_VacationsBalance.ExpireDate LIKE '____-__-__' THEN 23 WHEN hrs_VacationsBalance.ExpireDate LIKE '__/__/____' THEN 103 END)>='" & firstDayOfYear & "') "
        End If
        If ddlBalanceType.SelectedValue > 0 Then
            filter += " and hrs_VacationsBalance.BalanceTypeID=" & ddlBalanceType.SelectedValue
        End If
        If txtEmpCode.Text <> "" Then
            filter += " and hrs_Employees.Code='" & txtEmpCode.Text & "'"
        End If

        GetAllEmployeeRequests(filter)
    End Sub
End Class
