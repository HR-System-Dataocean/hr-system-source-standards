Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeesVacationsAdvanceFix
    Inherits MainPage
#Region "Public Decleration"
    Private ObjClssys_Groups As Clssys_Groups
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsEmployees As Clshrs_Employees
    Private mErrorHandler As Venus.Shared.ErrorsHandler
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ObjClssys_Groups = New Clssys_Groups(Me)
        Dim SearchID As Integer = 0
        Try

            If Not IsPostBack Then
                'Page.Session.Add("ConnectionString", ObjClssys_Groups.ConnectionString)
                'ObjClssys_Groups.AddOnChangeEventToControls("frmEmployeesVacationsAdvanceFix", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                ClsEmployees = New Clshrs_Employees(Page)
                Dim csSearchID As Integer
                Dim ClsLevels As New Clshrs_LevelTypes(Page)
                Dim ClsDataHandler As New Venus.Shared.DataHandler
                Dim StrSerial As String = String.Empty
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
                Dim ClsObjects As New Clssys_Objects(Page)
                Dim ClsSearchs As New Clssys_Searchs(Page)
                Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)

                ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'")
                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                csSearchID = ClsSearchs.ID
                lblLage.Text = ObjNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lage", lblLage.Text)
                Dim IntDimension As Integer = 510
                Dim UrlStringEmp = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmployee.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtEmployee.ClientID & "'"
                btnSearchCodeEmp.ClientSideEvents.Click = "OpenModal1(" & UrlStringEmp & ")"

                Dim ClsEmployeeClass As New Clshrs_EmployeeClasses(Me.Page)
                ClsEmployeeClass.GetDropDownList(ddlClass, True)
                txtYear.Text = DateTime.Now.Year.ToString()
            End If


            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ObjClssys_Groups.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub


#End Region

#Region "Private Functions"
    Protected Sub ChkAllEmployee_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAllEmployee.CheckedChanged
        If ChkAllEmployee.Checked Then
            txtEmployee.Enabled = False
            ddlClass.Enabled = False
            txtEmployee.Text = Nothing
            ddlClass.SelectedIndex = -1
        Else
            txtEmployee.Enabled = True
            ddlClass.Enabled = True
        End If

    End Sub
    Protected Sub btnHR_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnHR.Command

        AdjustNewEmployeesBalance()
        Dim DS1 As New Data.DataSet()
        DS1.Clear()
        For x As Integer = 0 To DS1.Tables.Count - 1
            DS1.Tables(x).Constraints.Clear()
        Next
        DS1.Relations.Clear()
        DS1.Tables.Clear()
        Dim ClsEmployees As New Clshrs_Employees(Me)
        Dim connetionString As String
        Dim connection As Data.SqlClient.SqlConnection
        Dim command As Data.SqlClient.SqlCommand
        Dim adapter As New Data.SqlClient.SqlDataAdapter
        connetionString = ClsEmployees.ConnectionString
        connection = New Data.SqlClient.SqlConnection(connetionString)
        Dim str1 As String = ""
        Dim Filter = ""
        str1 = "SELECT hrs_EmployeesVacations.id,hrs_EmployeesVacations.EmployeeID, hrs_EmployeesVacations.ActualStartDate, hrs_EmployeesVacations.ActualEndDate,  vactiondays FROM hrs_EmployeesVacations inner join hrs_Contracts on hrs_EmployeesVacations.EmployeeID=hrs_Contracts.EmployeeID inner join hrs_EmployeesClasses on hrs_Contracts.EmployeeClassID=hrs_EmployeesClasses.ID WHERE      hrs_EmployeesVacations.CancelDate is null and VacationTypeID=1 and hrs_EmployeesClasses.AdvanceBalance=1  "
        If txtEmployee.Text <> "" Then
            ClsEmployees.Find(" Code='" & txtEmployee.Text & "'")
            str1 += " and hrs_EmployeesVacations.EmployeeID=" & ClsEmployees.ID
        End If
        If txtYear.Text <> "" Then
            str1 += " and YEAR(hrs_EmployeesVacations.ActualStartDate)=" & txtYear.Text
        End If
        If ddlClass.SelectedIndex > 0 Then
            str1 += " and hrs_EmployeesClasses.ID=" & ddlClass.SelectedValue
        End If

        str1 += " order by hrs_EmployeesVacations.EmployeeID,hrs_EmployeesVacations.ActualEndDate "
        command = New Data.SqlClient.SqlCommand(str1, connection)
        adapter.SelectCommand = command
        adapter.Fill(DS1, "Table1")

        connection.Close()
        Dim previousEmpId = 0
        Dim previousBalance As Double = 0
        Dim previousAllVacDays As Double = 0
        Dim PerviousExpireDate As Date = "1900-01-01"
        For i = 0 To DS1.Tables(0).Rows.Count - 1
            Dim VacDate As Date = DS1.Tables(0).Rows(i).Item("ActualStartDate")

            If previousEmpId <> DS1.Tables(0).Rows(i).Item("EmployeeID") Or PerviousExpireDate < VacDate Then
                previousEmpId = DS1.Tables(0).Rows(i).Item("EmployeeID")
                previousBalance = 0
                previousAllVacDays = 0

                Dim strUpdate As String = "UPDATE [dbo].[hrs_VacationsBalance]  SET Consumed = 0 ,Remaining = Balance where  EmployeeID=" & previousEmpId & " and ExpireDate >='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "')"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(connection, Data.CommandType.Text, strUpdate)
            End If
            Dim StrNew = "SELECT    Balance FROM hrs_VacationsBalance where hrs_VacationsBalance.EndServiceDate IS NULL and ISNULL(Posted,0)=0 and BalanceTypeID=2 and EmployeeID=" & previousEmpId & " and ExpireDate >='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "')"
            Dim TransferBalance As Decimal
            TransferBalance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connection, Data.CommandType.Text, StrNew)

            StrNew = "SELECT    Balance FROM hrs_VacationsBalance where hrs_VacationsBalance.EndServiceDate IS NULL and ISNULL(Posted,0)=0 and BalanceTypeID<>2 and EmployeeID=" & previousEmpId & " and ExpireDate >='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "'  and DueDate<='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "')"
            Dim NewBalance As Decimal
            NewBalance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connection, Data.CommandType.Text, StrNew)

            StrNew = "SELECT    ExpireDate FROM hrs_VacationsBalance where hrs_VacationsBalance.EndServiceDate IS NULL and ISNULL(Posted,0)=0 and BalanceTypeID<>2 and EmployeeID=" & previousEmpId & " and ExpireDate >='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "'  and DueDate<='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "')"

            Dim exDate = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connection, Data.CommandType.Text, StrNew)
            If exDate <> Nothing Then
                PerviousExpireDate = Convert.ToDateTime(exDate).Date
            End If
            Dim AllBalance = NewBalance + TransferBalance
            Dim CurrentBallance = previousBalance

            If previousBalance > AllBalance Then
                CurrentBallance = AllBalance
            End If
            If previousBalance = 0 And AllBalance > 0 Then
                CurrentBallance = AllBalance
            End If
            'If previousBalance > 0 And AllBalance > 0 Then
            '    CurrentBallance = NewBalance + previousBalance
            'End If
            Dim remaining = CurrentBallance - DS1.Tables(0).Rows(i).Item("vactiondays")
            Dim Diffe As Integer
            Diffe = (DateDiff(DateInterval.Day, DS1.Tables(0).Rows(i).Item("ActualStartDate"), DS1.Tables(0).Rows(i).Item("ActualEndDate")))
            Dim OfficialVac = GetOverlappingOfficialVacationDays(DS1.Tables(0).Rows(i).Item("ActualStartDate"), CDate(DS1.Tables(0).Rows(i).Item("ActualEndDate")).AddDays(-1))
            If OfficialVac > 0 Then
                Diffe = Diffe - OfficialVac
            End If
            remaining = CurrentBallance - Diffe

            Dim UpdateCommand = "Update  hrs_EmployeesVacations  set TotalDays =" & CurrentBallance & ", RemainingDays =" & remaining & ", ConsumDays =" & (Diffe + OfficialVac) & ", TotalBalance =" & CurrentBallance & ", PaidFromBalance =" & CurrentBallance & ", RemainingBalance =" & remaining & " where ID=" & DS1.Tables(0).Rows(i).Item("ID") & " "
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(connection, Data.CommandType.Text, UpdateCommand)
            previousBalance = remaining
            previousAllVacDays = previousAllVacDays + DS1.Tables(0).Rows(i).Item("vactiondays")

            Dim str = "SELECT    Remaining FROM hrs_VacationsBalance where BalanceTypeID=2 and EmployeeID=" & previousEmpId & " and ExpireDate >='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "') and ISNULL(Posted,0)=0 "
            Dim remainTrans As Decimal
            remainTrans = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connection, Data.CommandType.Text, str)


            If remainTrans > DS1.Tables(0).Rows(i).Item("vactiondays") Then
                'Dim strUpdateTrans As String = "UPDATE [dbo].[hrs_VacationsBalance]  SET Consumed = Consumed+" & DS1.Tables(0).Rows(i).Item("vactiondays") & " ,Remaining = Remaining-" & DS1.Tables(0).Rows(i).Item("vactiondays") & " where BalanceTypeID=2 and EmployeeID=" & previousEmpId & " and ExpireDate >='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "')"
                Dim strUpdateTrans As String = "UPDATE [dbo].[hrs_VacationsBalance]  SET Consumed = Consumed+" & DS1.Tables(0).Rows(i).Item("vactiondays") & " ,Remaining = " & remainTrans & " -" & DS1.Tables(0).Rows(i).Item("vactiondays") & " where BalanceTypeID=2 and EmployeeID=" & previousEmpId & " and ExpireDate >='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "')"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(connection, Data.CommandType.Text, strUpdateTrans)
            Else
                If remainTrans > 0 Then
                    Dim strUpdateTrans As String = "UPDATE [dbo].[hrs_VacationsBalance]  SET Consumed = Consumed+" & remainTrans & " ,Remaining = 0 where BalanceTypeID=2 and EmployeeID=" & previousEmpId & " and ExpireDate >='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "')"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(connection, Data.CommandType.Text, strUpdateTrans)
                End If
                'Dim strUpdateNew As String = "UPDATE [dbo].[hrs_VacationsBalance]  SET Consumed = Consumed+" & (DS1.Tables(0).Rows(i).Item("vactiondays") - remainTrans) & " ,Remaining = Remaining-" & (DS1.Tables(0).Rows(i).Item("vactiondays") - remainTrans) & " where BalanceTypeID<>2 and EmployeeID=" & previousEmpId & " and ExpireDate >='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "')"
                Dim strUpdateNew As String = "UPDATE [dbo].[hrs_VacationsBalance]  SET Consumed = Consumed+" & (DS1.Tables(0).Rows(i).Item("vactiondays") - remainTrans) & " ,Remaining = " & remaining & "    where BalanceTypeID<>2 And EmployeeID=" & previousEmpId & " And ExpireDate >='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and DueDate<='" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "' and (CancelDate is null or CancelDate>'" & DateTime.Parse(VacDate).ToString("yyyy-MM-dd") & "')"
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(connection, Data.CommandType.Text, strUpdateNew)
            End If
        Next

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)

        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Employees Vacations Balances Fixed/تم ضبط ارصدة اجازات الموظفين"))
    End Sub

    Public Function GetOverlappingOfficialVacationDays(startDate As Date, endDate As Date) As Integer
        Dim totalDays As Integer = 0
        ClsEmployees = New Clshrs_Employees(Page)
        Dim OfficialVacsSTR As String = "set dateformat  dmy;SELECT FromDate, ToDate FROM hrs_OfficialVacations WHERE (FromDate <= '" & endDate & "' AND ToDate >= '" & startDate & "')"
        Dim OfficiaVacationsDS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, CommandType.Text, OfficialVacsSTR)
        If OfficiaVacationsDS.Tables(0).Rows.Count > 0 Then
            For Each Row In OfficiaVacationsDS.Tables(0).Rows

                Dim overlapStart As Date = If(startDate > Row("FromDate"), startDate, Row("FromDate"))
                Dim overlapEnd As Date = If(endDate < Row("ToDate"), endDate, Row("ToDate"))
                totalDays += (overlapEnd - overlapStart).Days + 1
            Next
        End If

        Return totalDays
    End Function

    Private Sub AdjustNewEmployeesBalance()
        Dim DS1 As New Data.DataSet()
        DS1.Clear()
        For x As Integer = 0 To DS1.Tables.Count - 1
            DS1.Tables(x).Constraints.Clear()
        Next
        DS1.Relations.Clear()
        DS1.Tables.Clear()
        Dim ClsEmployees As New Clshrs_Employees(Me)
        Dim connetionString As String
        Dim connection As Data.SqlClient.SqlConnection
        Dim command As Data.SqlClient.SqlCommand
        Dim adapter As New Data.SqlClient.SqlDataAdapter
        connetionString = ClsEmployees.ConnectionString
        connection = New Data.SqlClient.SqlConnection(connetionString)
        Dim str1 As String = ""
        Dim Filter = ""
        str1 = "select * from hrs_Employees join hrs_Contracts on hrs_Employees.ID=hrs_Contracts.EmployeeID and (hrs_contracts.EndDate is null or hrs_contracts.EndDate>getdate()) where hrs_Employees.id not in (select EmployeeID from hrs_VacationsBalance where BalanceTypeID=1)  "
        If txtEmployee.Text <> "" Then
            str1 += " and hrs_Employees.Code='" & txtEmployee.Text & "'"
        End If

        If ddlClass.SelectedIndex > 0 Then
            str1 += " and hrs_Contracts.EmployeeClassID=" & ddlClass.SelectedValue
        End If

        command = New Data.SqlClient.SqlCommand(str1, connection)
        adapter.SelectCommand = command
        adapter.Fill(DS1, "Table1")

        connection.Close()

        Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Me)
        Dim ClsContracts As New Clshrs_Contracts(Me.Page)
        If DS1.Tables(0).Rows.Count > 0 Then

            For Each row In DS1.Tables(0).Rows



                ClsContracts.Find("EmployeeID=" & row("ID") & " and (EndDate is null or  EndDate>getdate()) ")
                ClsEmployeeClasses.Find(" ID= " & ClsContracts.EmployeeClassID)
                If ClsEmployeeClasses.AdvanceBalance And ClsEmployeeClasses.AddBalanceInAddEmp Then
                    Dim dsNew As Data.DataSet = ClsEmployeeClasses.GetEmployeeClassAnnualVacations(ClsEmployeeClasses.ID)
                    Dim allDays As Decimal = 1
                    Dim allVacDays As Decimal = 0
                    Dim moreOneYear As Boolean = False

                    If dsNew.Tables(0).Rows.Count > 0 Then
                        With dsNew.Tables(0).Rows(0)
                            allDays = .Item("RequiredWorkingMonths")
                            allVacDays = .Item("DurationDays")

                        End With
                    End If
                    moreOneYear = ClsEmployeeClasses.AccumulatedBalance
                    'Rabie 20-07-2025
                    Dim dayValue = allVacDays / allDays
                    'Dim currentYear As Integer = SetDate(txtStartDate.Text, txtStartDate.Text).Year
                    Dim currentYear As Integer = CDate(row("StartDate")).Year
                    Dim endOfYear As DateTime = New DateTime(currentYear, 12, 31)
                    'Dim myDays As Integer = DateDiff(DateInterval.Day, SetDate(txtStartDate.Text, txtStartDate.Text), endOfYear.Date)
                    Dim myDays As Integer = DateDiff(DateInterval.Day, CDate(row("StartDate")), endOfYear.Date)
                    Dim myBalance = myDays * dayValue
                    Dim expireDate As Date = endOfYear
                    If moreOneYear Then
                        'myDays = DateDiff(DateInterval.Day, SetDate(txtStartDate.Text, txtStartDate.Text), SetDate(txtStartDate.Text, txtStartDate.Text).AddDays(allDays))
                        myDays = DateDiff(DateInterval.Day, CDate(row("StartDate")), CDate(row("StartDate")).AddDays(allDays))
                        myBalance = myDays * dayValue
                        'expireDate = SetDate(txtStartDate.Text, txtStartDate.Text).AddDays(allDays)
                        expireDate = CDate(row("StartDate")).AddDays(allDays)
                    End If
                    Dim checkCnt = "INSERT INTO [dbo].[hrs_VacationsBalance] ([EmployeeID],[Year],[Balance],[Consumed],[Remaining],[BalanceTypeID],[ExpireDate],[Src],[Remarks],[Reguser],[RegDate],DueDate) VALUES (" & row("ID") & "," & currentYear & "," & myBalance & ",0," & myBalance & ",1,'" & expireDate.ToString("yyyy-MM-dd") & "'," & "'frmEmployeeWizard'" & "," & "''" & ",'" & ClsContracts.RegUserID & "',getdate(),'" & CDate(row("StartDate")).ToString("yyyy-MM-dd") & "')"
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployeeClasses.ConnectionString, System.Data.CommandType.Text, checkCnt)

                End If

            Next
        End If
    End Sub


#End Region
End Class
