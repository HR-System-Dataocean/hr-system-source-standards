Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports OfficeOpenXml
Imports System.IO
Imports System.Diagnostics

Partial Class frmVacationsBalanceTransferring
    Inherits MainPage
#Region "Public Decleration"
    Private ObjClssys_Groups As Clssys_Groups
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsEmployees As Clshrs_Employees
    Private ClsUsers As Clssys_Users
    Private _Sys_Report As Clssys_Reports
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ObjClssys_Groups = New Clssys_Groups(Me)
        Dim SearchID As Integer = 0
        Try

            If Not IsPostBack Then
                'Page.Session.Add("ConnectionString", ObjClssys_Groups.ConnectionString)
                'ObjClssys_Groups.AddOnChangeEventToControls("frmVacationsBalanceTransferring", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                '_Sys_Report = New Clssys_Reports(Me)
                '_Sys_Report.GetDropDownList(Me.DdlYear, True, "ReportGroupID = 101")


                'Dim clsBranch As New Clssys_Branches(Page)
                'clsBranch.GetDropDownList(ddlBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")

                'Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
                'ClsDepartment.GetDropDownList(ddlDepartment, True)

                'Dim ClsProfission As New Clshrs_Professions(Me.Page)
                'Dim ClsPosition As New Clshrs_Positions(Me.Page)
                'ClsProfission.GetDropDownList(ddlProfessions, True)


                'Dim ClsNationality As New ClsBasicFiles(Page, "sys_Nationalities")
                'ClsNationality.GetDropDownList(DdlNationality, True)

                'Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                ''ClsProjects.GetDropDownList(ddlProject, True, "CancelDate is null and ID in (select ProjectID from Att_UserProjects where CancelDate is null and UserID = " & ClsProjects.DataBaseUserRelatedID & ")")
                'ClsProjects.GetDropDownList(ddlProject, True, "CancelDate is null")


                ClsEmployees = New Clshrs_Employees(Page)
                Dim csSearchID As Integer
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
                Dim ClsObjects As New Clssys_Objects(Page)
                Dim ClsSearchs As New Clssys_Searchs(Page)
                Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
                ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'")
                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                csSearchID = ClsSearchs.ID
                Dim IntDimension As Integer = 510

                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtEmpCode.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtEmpCode.ClientID & "'"
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                FillDdlYear()

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

    End Sub

    Protected Sub txtEmpCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmpCode.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtEmpCode.Text) Then
                ClsEmployees = New Clshrs_Employees(Page)
                ClsEmployees.Find("Code='" & txtEmpCode.Text & "'")
                If ClsEmployees.ID > 0 Then

                    LblEmpName.Text = ClsEmployees.EnglishName
                Else
                    LblEmpName.Text = ""
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnHR_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnHR.Command
        Dim clsContracts As Clshrs_Contracts = New Clshrs_Contracts(Page)

        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsContracts.ConnectionString)
        If DdlYear.SelectedValue < 1 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please..Select Year/برجاء اختيار السنة"))
            Return
        End If
        If txtNewBalanceExpireDate.Text = "" Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please..Insert New Balance Expire Date/برجاء ادخال تاريخ انتهاء الرصيد الجديد"))
            Return
        End If
        If chkTransfer.Checked Then
            If txtExpirDate.Text = "" Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please..Insert Transfer Balance Expire Date/برجاء ادخال تاريخ انتهاء الرصيد المرحل"))
                Return
            End If
        End If

        ClsEmployees = New Clshrs_Employees(Page)

        Dim NewYear = (Integer.Parse(DdlYear.SelectedValue) + 1).ToString()
        Dim startDate = New Date(NewYear, 1, 1).ToString("yyyy-MM-dd")
        Dim endOfYear = New Date(NewYear, 12, 31).ToString("yyyy-MM-dd")
        If txtNewBalanceExpireDate.Text <> "" Then
            endOfYear = CDate(txtNewBalanceExpireDate.Text).ToString("yyyy-MM-dd")
        End If
        If Not String.IsNullOrEmpty(txtEmpCode.Text) Then
            ClsEmployees.Find("Code=" & txtEmpCode.Text & "")
        End If
        Dim mystrExist As String

        If ClsEmployees.ID > 0 Then
            mystrExist = "SELECT    Count(EmployeeID) as ExistRec FROM hrs_VacationsBalance where Year=" & Integer.Parse(DdlYear.SelectedValue) + 1 & "and EmployeeID=" & ClsEmployees.ID & " and (CancelDate is null or CancelDate>'" & DateTime.Now.Date.ToString("yyyy-MM-dd") & "')"
        Else
            mystrExist = "SELECT    Count(EmployeeID) as ExistRec FROM hrs_VacationsBalance where Year=" & Integer.Parse(DdlYear.SelectedValue) + 1 & " and (CancelDate is null or CancelDate>'" & DateTime.Now.Date.ToString("yyyy-MM-dd") & "')"

        End If
        Dim myExist = 0
        myExist = Integer.Parse(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsContracts.ConnectionString, Data.CommandType.Text, mystrExist))
        If myExist > 0 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "this year already transfered/هذا العام مرحل بالفعل"))
            Return
        End If
        If ClsEmployees.ID > 0 Then
            clsContracts.Find(" EmployeeID= " & ClsEmployees.ID & " And CancelDate IS NULL AND EndDate IS NULL ")

        Else
            clsContracts.Find(" CancelDate IS NULL AND EndDate IS NULL ")

        End If
        If clsContracts.DataSet.Tables(0).Rows.Count > 0 Then
            For Each row As Data.DataRow In clsContracts.DataSet.Tables(0).Rows
                Dim classID As Int32 = Integer.Parse(row.Item("EmployeeClassID"))
                Dim EmpID As Int32 = Integer.Parse(row.Item("EmployeeID"))
                'select vacation Balance where year=ddlYear
                Dim str = "SELECT    Remaining FROM hrs_VacationsBalance where BalanceTypeID<>2 and EmployeeID=" & EmpID & " and Year=" & DdlYear.SelectedValue & " and (CancelDate is null or CancelDate>'" & DateTime.Now.Date.ToString("yyyy-MM-dd") & "') and ISNULL(Posted,0)=0"
                Dim remain As Decimal
                remain = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsContracts.ConnectionString, Data.CommandType.Text, str)

                str = "SELECT    Balance FROM hrs_VacationsBalance where BalanceTypeID<>2 and EmployeeID=" & EmpID & " and Year=" & DdlYear.SelectedValue & " and (CancelDate is null or CancelDate>'" & DateTime.Now.Date.ToString("yyyy-MM-dd") & "') and ISNULL(Posted,0)=0"
                Dim Balance As Decimal
                Balance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsContracts.ConnectionString, Data.CommandType.Text, str)
                Dim clsClssification As Clshrs_EmployeeClasses = New Clshrs_EmployeeClasses(Page)
                clsClssification.Find(" ID =" & classID)

                Dim transDays As Decimal = 0

                If Not clsClssification.AccumulatedBalance Then

                    If chkTransfer.Checked Then
                        If clsClssification.VacationTrans Then
                            If remain > 0 Then
                                If clsClssification.VactionTransType = 1 Then 'days
                                    If remain > clsClssification.TransValue Then
                                        transDays = clsClssification.TransValue
                                    Else
                                        transDays = remain
                                    End If
                                End If
                                If clsClssification.VactionTransType = 2 Then 'precent
                                    Dim transPrec As Decimal = Balance * clsClssification.TransValue / 100
                                    If remain > transPrec Then
                                        transDays = transPrec
                                    Else
                                        transDays = remain
                                    End If
                                End If

                                Dim updateBefore = "UPDATE [dbo].[hrs_VacationsBalance] SET Posted=1 ,Remarks=' Transfered By/ " & _sys_User.EngName & " at " & DateTime.Now & "'  where BalanceTypeID<>2 and EmployeeID=" & EmpID & " and Year=" & DdlYear.SelectedValue & " and (CancelDate is null or CancelDate>'" & DateTime.Now.Date.ToString("yyyy-MM-dd") & "')"
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsContracts.ConnectionString, CommandType.Text, updateBefore)

                                Dim strExist = "SELECT    Count(EmployeeID) as ExistRec FROM hrs_VacationsBalance where BalanceTypeID=2 and EmployeeID=" & EmpID & " and Year=" & Integer.Parse(DdlYear.SelectedValue) + 1 & " and (CancelDate is null or CancelDate>'" & DateTime.Now.Date.ToString("yyyy-MM-dd") & "')"
                                Dim Exist As Integer = 0
                                Exist = Integer.Parse(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsContracts.ConnectionString, Data.CommandType.Text, strExist))

                                If Exist = 0 Then
                                    Dim checkCnt = "INSERT INTO [dbo].[hrs_VacationsBalance] ([EmployeeID],[Year],[Balance],[Consumed],[Remaining],[BalanceTypeID],[ExpireDate],[Src],[Remarks],[Reguser],[RegDate],DueDate) VALUES (" & EmpID & "," & Integer.Parse(DdlYear.SelectedValue) + 1 & "," & transDays & ",0," & transDays & ",2,'" & Convert.ToDateTime(txtExpirDate.Text).ToString("yyyy-MM-dd") & "'," & "'frmVactionBalanceTransferring'" & "," & "''" & ",'" & User & "',getdate(),'" & startDate & "')"
                                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsContracts.ConnectionString, CommandType.Text, checkCnt)


                                End If

                            End If

                        End If

                    End If


                    If clsClssification.AdvanceBalance Then
                        If chkNewYearBalance.Checked Then

                            Dim ds As DataSet = clsClssification.GetEmployeeClassAnnualVacations(clsClssification.ID)
                            Dim allVacDays As Decimal = 0
                            If ds.Tables(0).Rows.Count > 0 Then
                                With ds.Tables(0).Rows(0)
                                    allVacDays = .Item("DurationDays")
                                End With
                            End If

                            Dim strExist = "SELECT    Count(EmployeeID) as ExistRec FROM hrs_VacationsBalance where BalanceTypeID=1 and EmployeeID=" & EmpID & " and Year=" & Integer.Parse(DdlYear.SelectedValue) + 1 & " and (CancelDate is null or CancelDate>'" & DateTime.Now.Date.ToString("yyyy-MM-dd") & "')"
                            Dim Exist As Integer = 0
                            Exist = Integer.Parse(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsContracts.ConnectionString, Data.CommandType.Text, strExist))

                            If Exist = 0 Then

                                Dim checkCnt = "INSERT INTO [dbo].[hrs_VacationsBalance] ([EmployeeID],[Year],[Balance],[Consumed],[Remaining],[BalanceTypeID],[ExpireDate],[Src],[Remarks],[Reguser],[RegDate],DueDate) VALUES (" & EmpID & "," & Integer.Parse(DdlYear.SelectedValue) + 1 & "," & allVacDays & ",0," & allVacDays & ",1,'" & endOfYear & "'," & "'frmVactionBalanceTransferring'" & "," & "''" & ",'" & User & "',getdate(),'" & startDate & "')"
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsClssification.ConnectionString, CommandType.Text, checkCnt)
                            End If

                        End If
                    End If

                End If
            Next

            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Vaction Balance Transfer Success/تم ترحيل الاجازات بنجاح"))
            txtEmpCode.Text = ""
            LblEmpName.Text = ""
        End If

        DdlYear.SelectedIndex = -1
        chkTransfer.Checked = False
        chkNewYearBalance.Checked = False
        txtExpirDate.Value = Nothing
    End Sub

    Protected Sub btnCancel_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnCancel.Command
        ClsEmployees = New Clshrs_Employees(Page)





        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        If Not String.IsNullOrEmpty(txtEmpCode.Text) Then
            ClsEmployees.Find("Code=" & txtEmpCode.Text & "")
        End If
        If ClsEmployees.ID > 0 Then
            Dim str As String = "Update hrs_VacationsBalance set Posted=0  where EmployeeID=" & ClsEmployees.ID & " And Year=" & Integer.Parse(DdlYear.SelectedValue) & "  And BalanceTypeID=1 "
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, CommandType.Text, str)

            Dim checkCnt = "Delete [dbo].[hrs_VacationsBalance] where EmployeeID=" & ClsEmployees.ID & " And Year=" & Integer.Parse(DdlYear.SelectedValue) + 1 & " and [Src]='frmVactionBalanceTransferring'"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Vaction Balance Cancel Transfer Success/تم إلغاء الترحيل بنجاح"))
            txtEmpCode.Text = ""
            LblEmpName.Text = ""

        Else
            Dim str As String = "Update hrs_VacationsBalance set Posted=0  where  Year=" & Integer.Parse(DdlYear.SelectedValue) & "  And BalanceTypeID=1 "
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, CommandType.Text, str)

            Dim checkCnt = "Delete [dbo].[hrs_VacationsBalance] where Year=" & Integer.Parse(DdlYear.SelectedValue) + 1 & " and [Src]='frmVactionBalanceTransferring'"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Vaction Balance Cancel Transfer Success/تم إلغاء الترحيل بنجاح"))
            txtEmpCode.Text = ""
            LblEmpName.Text = ""
        End If

    End Sub

    Protected Sub ddlYear_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlYear.SelectedIndexChanged
        ClsEmployees = New Clshrs_Employees(Page)

        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")

        Dim strExist = "SELECT    Count(EmployeeID) as ExistRec FROM hrs_VacationsBalance where Year=" & Integer.Parse(DdlYear.SelectedValue) + 2 & " and (CancelDate is null or CancelDate>'" & DateTime.Now.Date.ToString("yyyy-MM-dd") & "')"
        Dim Exist As Integer = 0
        Exist = Integer.Parse(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, strExist))
        If Exist > 0 Then
            btnCancel.Visible = False
            Return
        End If

        strExist = "SELECT    Count(EmployeeID) as ExistRec FROM hrs_VacationsBalance where Year=" & Integer.Parse(DdlYear.SelectedValue) + 1 & " and (CancelDate is null or CancelDate>'" & DateTime.Now.Date.ToString("yyyy-MM-dd") & "')"
        Exist = 0
        Exist = Integer.Parse(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, strExist))
        If Exist > 0 Then
            If _sys_User.Code = "sa" Then
                btnCancel.Visible = True
            Else
                btnCancel.Visible = False

            End If

            Return
        End If

        btnCancel.Visible = False
    End Sub

#End Region
End Class
