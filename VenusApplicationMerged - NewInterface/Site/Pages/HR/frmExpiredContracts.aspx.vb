Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports Venus.Application.SystemFiles.System.ClsDataAcessLayer

Partial Class frmExpiredContracts
    Inherits MainPage

#Region "Public Decleration"
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Try
            Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
            Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim IntSelectedPeriod As Integer = 0
            Dim clsBranch As New Clssys_Branches(Page)
            Dim ClsObjects As New Clssys_Objects(Page)
            Dim ClsSearchs As New Clssys_Searchs(Page)
            Dim clsDocType As New Clshrs_ContractTypes(Page)

            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsEmployee.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If

            If Not IsPostBack Then
                ddlDepartment.Attributes.Add("OnChange", "ddlDepartment_Change()")
                Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")

                ClsDepartment.GetDropDownList(ddlDepartment, True)
                ddlDepartment.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Departments]/ [ جميع الإدارات]")
                clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
                ddlBranche.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Branches]/ [ جميع الفروع]")

                uwgExpiredDocuments.Columns.FromKey("EmployeeName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))

                Page.Session.Add("Lage", ClsNavigationHandler.SetLanguage(Page, "0/1"))
                Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
                hdnLang.Value = ClsNavigationHandler.SetLanguage(Page, "0/1")


                clsDocType.GetDropDownList(ddlContractType, True)
                ddlContractType.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Contracts Types]/ [ جميع أنواع التعاقد]")
                'wdcFromDate.Value = ClsEmployee.GetHigriDate2(Date.Now, "")
                wdcToDate.Value = ClsEmployee.GetHigriDate2(Date.Now.AddMonths(1), "")
                GetExpiredContracts()
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployee.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Print.Command, ImageButton_Delete.Command, LinkButton_Delete.Command, LinkButton_Print.Command
        Select Case e.CommandArgument
            Case "Delete"
                Dim clsContract As New Clshrs_Contracts(Page)
                Dim clsEmployee As New Clshrs_Employees(Page)
                Dim objNav As New Venus.Shared.Web.NavigationHandler(clsEmployee.ConnectionString)
                For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgExpiredDocuments.Rows
                    If row.Cells.FromKey("ID").Value <> Nothing Then
                        If row.Cells.FromKey("Check").Value Then
                            If CDate(row.Cells.FromKey("ToDate").Value) <= DateTime.Now.Date Then
                                clsContract.RenewContract(row.Cells.FromKey("EmpCode").Value, row.Cells.FromKey("ID").Value)
                            End If
                        End If
                    End If
                Next
                GetExpiredContracts()
            Case "Print"
                Dim clsBranch As New Clssys_Branches(Page)
                Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
                Dim clsDAL As New ClsDataAcessLayer(Page)
                clsBranch.Find("ID=" & ddlBranche.SelectedValue)
                ClsDepartment.Find("ID=" & ddlDepartment.SelectedValue)
                Dim contype As New Clshrs_ContractTypes(Me)
                contype.Find("ID = " & ddlContractType.SelectedValue)
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">OpenEXDOCReport('" & hdnLang.Value & "','" & txtCode.Text.Trim & "|" & clsDAL.SetHigriDate2(wdcToDate.Text, "") & "|" & ClsDepartment.Code.Trim & "|" & clsBranch.Code.Trim & "|" & contype.Code & "');</script>")
        End Select
        '"|" & clsDAL.SetHigriDate2(wdcFromDate.Text, "") &
    End Sub
    Protected Sub btnFind_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetExpiredContracts()
    End Sub

#End Region

#Region "Private Functions"
    Private Sub GetExpiredContracts()
        Dim clsContract As New Clshrs_Contracts(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(clsContract.ConnectionString)
        Dim clsBranch As New Clssys_Branches(Page)
        Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
        clsBranch.Find("ID=" & ddlBranche.SelectedValue)
        ClsDepartment.Find("ID=" & ddlDepartment.SelectedValue)
        Dim contype As New Clshrs_ContractTypes(Me)
        contype.Find("ID = " & ddlContractType.SelectedValue)
        Dim fromDate = "1900-01-01"
        Dim toDate = wdcToDate.Text
        If toDate = "" Then
            toDate = "3000-01-01"
        End If
        Dim dsResult As Data.DataSet = clsContract.GetExpiredContracts(txtCode.Text,
                                                                       ClsDepartment.Code.Trim,
                                                                       clsBranch.Code.Trim,
                                                                       CDate(fromDate),
                                                                       CDate(wdcToDate.Text),
                                                                       contype.Code)
        If dsResult.Tables(0).Rows.Count > 0 Then
            Dim ds As New Data.DataSet
            ds.Tables.Add()
            ds.Tables(0).Columns.Add("EmpCode")
            ds.Tables(0).Rows.Add(objNav.SetLanguage(Me, "Select All/تحديد الكل"))
            ds.Tables(0).Merge(dsResult.Tables(0))

            uwgExpiredDocuments.DataSource = ds.Tables(0)
            uwgExpiredDocuments.DataBind()
        Else
            uwgExpiredDocuments.DataSource = Nothing
            uwgExpiredDocuments.DataBind()
        End If
    End Sub
#End Region

#Region "Public Shared Function"

    Public Shared Function CheckBranchPermission(ByVal intDeptID As Integer) As String
        Try
            Dim str As String = ""
            Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim mCompanyID As Integer = CType(HttpContext.Current.Session("CompanyID"), Integer)
            Dim mUserID As Integer = CType(HttpContext.Current.Session("UserID"), Integer)
            Dim BranchesIDs As String = GetRelatedDept(intDeptID)
            BranchesIDs = IIf(BranchesIDs = "", "0", BranchesIDs)

            Dim StrSelectCommand As String = _
                    "Declare @Branches as nvarchar(max)='';" & _
                    "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " & _
                    "From sys_Branches B Inner Join sys_CompaniesBranches CB ON CB.BrancheID=B.ID Where B.ID IN (" & BranchesIDs & ")  And CB.CompanyID=" & mCompanyID & " And CB.UserID=" & mUserID & " AND CanView= 1" & _
                    "Select @Branches  = STUFF(@Branches,1,1,''); " & _
                    "Select IsNull(@Branches,'')"

            str = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, StrSelectCommand)

            Return IIf(str = "", "0", str)

        Catch ex As Exception
            Return "0"
        End Try
    End Function
    Public Shared Function GetRelatedDept(ByVal intDeptID As Integer) As String
        Dim StrSelectCommand As String
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Dim dsBranches As New Data.DataSet

        Dim str As String = IIf(intDeptID = 0, "", " And DB.DepartmentID = " & intDeptID)
        Try
            StrSelectCommand = " Declare @Branches as nvarchar(max)=''; " & _
                                "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " & _
                                "From sys_DepartmentsBranches DB Inner Join sys_Branches B On DB.BranchID = B.ID Where  DB.Checked  = 1 " & str & " And B.CancelDate Is Null; " & _
                                "Select @Branches  = STUFF(@Branches,1,1,''); " & _
                                "Select IsNull(@Branches,'')"

            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, StrSelectCommand)

        Catch ex As Exception

        End Try
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetRelatedDepartment(ByVal strDeptID As String) As String
        Try
            Dim dsBranches As New Data.DataSet
            Dim StrSelectCommand As String
            Dim strResultBranches As String = CheckBranchPermission(strDeptID)
            Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim Lage As String = CType(HttpContext.Current.Session("Lage"), String)
            Dim mCompanyID As Integer = CType(HttpContext.Current.Session("CompanyID"), Integer)
            Dim mUserID As Integer = CType(HttpContext.Current.Session("UserID"), Integer)
            Dim strFieldName As String = IIf(Lage = "0", "EngName", "ArbName")
            Dim strAllName As String = IIf(Lage = "0", "[All branches]", "[ جميع الفروع]")
            Dim str As String = String.Empty

            StrSelectCommand = " Select B.ID, B." & strFieldName & " From sys_Branches B Inner Join sys_CompaniesBranches CB ON CB.BrancheID=B.ID Where B.ID IN (" & IIf(strResultBranches = "", 0, strResultBranches) & ") And CB.CompanyID=" & mCompanyID & " And CB.UserID=" & mUserID & " AND CanView= 1"

            dsBranches = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr, Data.CommandType.Text, StrSelectCommand)

            If dsBranches.Tables(0).Rows.Count > 0 Then

                str = "<select style='border: 1px solid rgb(204, 204, 204); width: 100%; height: 20px; color: black; font-family: Tahoma; font-size: 8pt; font-weight: normal;' id='UltraWebTab1__ctl0_ddlBranche' name='UltraWebTab1$_ctl0$ddlBranche'><option value=0>" & strAllName & "</option>"

                For I As Integer = 0 To dsBranches.Tables(0).Rows.Count - 1
                    str &= "<option value=" & dsBranches.Tables(0).Rows(I).Item("ID") & ">" & dsBranches.Tables(0).Rows(I).Item(strFieldName) & "</option>"
                Next

                str &= "</select>"

                Return str
            Else
                Return "<select style='border: 1px solid rgb(204, 204, 204); width: 100%; height: 20px; color: black; font-family: Tahoma; font-size: 8pt; font-weight: normal;' id='UltraWebTab1__ctl0_ddlBranche' name='UltraWebTab1$_ctl0$ddlBranche'><option value=0>" & strAllName & "</option></select>"
            End If

        Catch ex As Exception

        End Try
    End Function
    Public Shared Function CheckMyDate(ByVal strDate As String) As String
        Try

            Dim strRet As String = IIf(CType(HttpContext.Current.Session("Log"), String) = "Eng", "0", "1") & ","
            Dim strDiff As String = String.Empty
            If strDate = "__/__/____" Or strDate = "  /  /    " Then
                strRet &= "1"
                Return strRet
            End If
            If SetHigriDate2(strDate, strDiff) = Nothing Then
                strRet &= "0"
            Else
                strRet &= "1"
            End If
            Return strRet

        Catch ex As Exception

        End Try
    End Function
    Public Shared Function SetHigriDate2(ByVal dteDate As Object, ByRef strDetails As String) As Object
        Try

            Dim isHijri As Int16
            Dim intDiff As Int16 = 0
            Dim dteDateOut As Object

            If Not CheckValidDate(dteDate) Then
                strDetails = String.Empty
                Return Nothing
            End If

            Dim strArr() As String = dteDate.ToString.Split(" ")(0).Split("/")

            If CInt(strArr(2)) < 1900 Then
                isHijri = 1
                If CInt(strArr(1)) = 2 Then ' Month is 2
                    If CInt(strArr(0)) > 28 Then
                        intDiff = CInt(strArr(0)) - 28
                        dteDate = "28/" & Format(CInt(strArr(1)), "00") & "/" & strArr(2)
                    End If
                End If
            Else
                isHijri = 0
            End If

            strDetails = isHijri.ToString & "," & intDiff

            If isHijri = 1 Then
                dteDateOut = GetRelativeDate(dteDate, DateType.Hijri, Directions.Input)
            Else
                dteDateOut = GetRelativeDate(dteDate, DateType.Gregorian, Directions.Input)
            End If

            Return CDate(dteDateOut).AddDays(intDiff)

        Catch ex As Exception

        End Try
    End Function
    Public Shared Function CheckValidDate(ByVal dteDateIn As Object) As Boolean
        Try

            If IsDBNull(dteDateIn) Then
                Return False
            End If
            If IsNothing(dteDateIn) Then
                Return False
            End If
            If dteDateIn.ToString.Trim = "" Then
                Return False
            End If
            If TypeOf (dteDateIn) Is Date Then
                If dteDateIn.Year = 1 Then
                    Return False
                End If
            ElseIf TypeOf (dteDateIn) Is String Then
                If dteDateIn.ToString = "  /  /    " Or dteDateIn.ToString = "__/__/____" Then
                    Return False
                End If
                Dim strArr() As String = dteDateIn.ToString.Split(" ")(0).Split("/")
                If strArr.Length < 3 Then
                    Return False
                End If
                Dim intDay As Integer = CInt(IIf(strArr(0).Trim = "" Or strArr(0).Trim = "__", 0, strArr(0).Trim("_").Trim))
                Dim intMonth As Integer = CInt(IIf(strArr(1).Trim = "" Or strArr(1).Trim = "__", 0, strArr(1).Trim("_").Trim))
                Dim intYear As Integer = CInt(IIf(strArr(2).Trim = "" Or strArr(2).Trim = "____", 0, strArr(2).Trim("_").Trim))
                If intDay <= 0 Or intDay > 31 Then
                    Return False
                End If
                If intMonth <= 0 Or intMonth > 12 Then
                    Return False
                End If
                If intYear <= 0 Or intYear < 1300 Or intYear > 2070 Then
                    Return False
                End If
                If intYear > 1900 Then
                    Try
                        Dim dte As Date = New Date(intYear, intMonth, intDay)
                    Catch ex As Exception
                        Return False
                    End Try
                Else
                    If intDay > 30 Then
                        Return False
                    End If
                End If
            End If

            Return True

        Catch ex As Exception

        End Try
    End Function
    Public Shared Function GetRelativeDate(ByVal iDate As Object, ByVal DateDisplayType As DateType, ByVal oDirection As Directions) As Object
        Try

            If IsNothing(iDate) And oDirection = Directions.Output Then
                Return Nothing
            ElseIf IsNothing(iDate) And oDirection = Directions.Input Then
                Return DBNull.Value
            End If

            If (oDirection = Directions.Output And (IsDBNull(iDate) Or IsNothing(iDate) Or iDate.ToString.Trim.ToLower = "null" Or iDate.ToString.Trim = "")) Then
                Return Nothing
            Else
                If (oDirection = Directions.Input And (IsDBNull(iDate) Or IsNothing(iDate) Or iDate.ToString.Trim.ToLower = "null" Or iDate.ToString.Trim = "")) Then
                    Return DBNull.Value
                End If
            End If
            If (CDate(iDate).Year < 1300) Then
                Return Nothing
            End If

            If iDate = "1/1/1900" Then
                Return Nothing
            End If

            Dim oDate As Date = iDate

            Dim strFormatDate As String = "dd/MM/yyyy"
            Dim StrSQLCommand As String = String.Empty
            Dim UmAlQuraCalender As New Global.System.Globalization.UmAlQuraCalendar
            Dim GregorianCalender As New Global.System.Globalization.GregorianCalendar
            Dim IntYear As Integer
            Dim IntMonth As Integer
            Dim IntDay As Integer

            Dim IntTempGYear As Integer
            Dim IntTempGMonth As Integer
            Dim IntTempGDay As Integer
            Dim StrTempGDate As String

            Dim IntTempHYear As Integer
            Dim IntTempHMonth As Integer
            Dim IntTempHDay As Integer
            Dim StrTempHDate As String

            Dim StrDate As String = String.Empty
            Dim StrReturnDate As String
            Dim CurrentDate As Date
            Dim GDate As Date
            Dim HDate As Date

            Dim mSqlCommand As SqlClient.SqlCommand
            Dim mConnectionString As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim mLog As String = CType(HttpContext.Current.Session("Log"), String)
            Dim HostPage As System.Web.UI.Page


            Dim nav As New Venus.Shared.Web.NavigationHandler(mConnectionString)

            '---------------------------------------------------------------------------'
            'This part is to prepare the data to get the relative date from the databese' 
            '---------------------------------------------------------------------------'
            IntYear = DatePart(DateInterval.Year, oDate)
            IntMonth = DatePart(DateInterval.Month, oDate)
            IntDay = DatePart(DateInterval.Day, oDate)
            StrDate = Format(IntDay, "00") & "/" & Format(IntMonth, "00") & "/" & IntYear.ToString

            '---------------------------------------------------------------------------'
            'This part to get the relative date according to microsoft standerd calender' 
            '---------------------------------------------------------------------------'
            If IntYear <= 1600 Then
                IntTempGYear = GregorianCalender.GetYear(oDate)
                IntTempGMonth = GregorianCalender.GetMonth(oDate)
                IntTempGDay = GregorianCalender.GetDayOfMonth(oDate)
                StrTempGDate = Format(IntTempGDay, "00") + "/" + Format(IntTempGMonth, "00") + "/" + IntTempGYear.ToString
            Else
                IntTempHYear = UmAlQuraCalender.GetYear(oDate)
                IntTempHMonth = UmAlQuraCalender.GetMonth(oDate)
                IntTempHDay = UmAlQuraCalender.GetDayOfMonth(oDate)
                StrTempHDate = Format(IntTempHDay, "00") + "/" + Format(IntTempHMonth, "00") + "/" + IntTempHYear.ToString
            End If


            '---------------------------------------------------------------------------'
            'Get the Relative Date according to the input paramters and depending on the'
            'display language                                                           '
            '---------------------------------------------------------------------------'
            Select Case DateDisplayType
                Case DateType.Hijri

                    Select Case oDirection
                        Case Directions.Input
                            If IntYear < 1600 Then
                                StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
                                mSqlCommand = New SqlClient.SqlCommand
                                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                                mSqlCommand.CommandType = CommandType.Text
                                mSqlCommand.CommandText = StrSQLCommand
                                mSqlCommand.Connection.Open()
                                StrReturnDate = mSqlCommand.ExecuteScalar
                                mSqlCommand.Connection.Close()
                                If StrReturnDate Is Nothing Then
                                    StrReturnDate = HijriToGreg(StrDate, strFormatDate)
                                    If Not IsNothing(StrReturnDate) Then
                                        '============================== Insert GH Date if not found [Start]
                                        Dim strInser As String = "Set Dateformat dmy  Insert Into sys_GHCalendar Values ('" & StrReturnDate & "','" & StrDate & "');"
                                        mSqlCommand.CommandText = strInser
                                        mSqlCommand.Connection.Open()
                                        mSqlCommand.ExecuteNonQuery()
                                        mSqlCommand.Connection.Close()
                                        '============================== Insert GH Date if not found [ END ]
                                    Else
                                        StrReturnDate = StrTempGDate
                                    End If

                                End If
                            Else
                                Return oDate
                            End If
                        Case Directions.Output
                            StrSQLCommand = "Set Dateformat dmy Select HDate from sys_GHCalendar Where GDate between '" & StrDate & " 00:00:00 " & "' And '" & StrDate & " 23:00:00 '"
                            mSqlCommand = New SqlClient.SqlCommand
                            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                            mSqlCommand.CommandType = CommandType.Text
                            mSqlCommand.CommandText = StrSQLCommand
                            mSqlCommand.Connection.Open()
                            StrReturnDate = mSqlCommand.ExecuteScalar
                            mSqlCommand.Connection.Close()
                            If StrReturnDate Is Nothing Then
                                Try
                                    '============================== Insert GH Date if not found [Start]
                                    Dim strInser As String = "Set Dateformat dmy  Insert Into sys_GHCalendar Values ('" & StrDate & "','" & StrTempHDate & "');"
                                    mSqlCommand.CommandText = strInser
                                    mSqlCommand.Connection.Open()
                                    mSqlCommand.ExecuteNonQuery()
                                    mSqlCommand.Connection.Close()
                                    '============================== Insert GH Date if not found [ END ]
                                    StrReturnDate = StrTempHDate
                                Catch ex As SqlClient.SqlException
                                    If ex.Number = 2601 Then

                                        HostPage.ClientScript.RegisterClientScriptBlock(HostPage.GetType(), "", "<script language=""javascript"">alert('" & IIf(mLog = "Eng", "Found invalid saved Dates", "يوجد تواريخ محفوظة خطأ") & "');</script>")

                                        Return Nothing
                                    End If

                                End Try
                            End If
                    End Select

                Case DateType.Gregorian
                    Select Case oDirection
                        Case ClsDataAcessLayer.Directions.Input
                            If IntYear < 1600 Then

                                StrSQLCommand = "Set Dateformat dmy Select GDate from sys_GHCalendar Where HDate ='" & StrDate & "'"
                                mSqlCommand = New SqlClient.SqlCommand
                                mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
                                mSqlCommand.CommandType = CommandType.Text
                                mSqlCommand.CommandText = StrSQLCommand
                                mSqlCommand.Connection.Open()
                                StrReturnDate = mSqlCommand.ExecuteScalar
                                mSqlCommand.Connection.Close()
                                If StrReturnDate Is Nothing Then
                                    StrReturnDate = ClsDataAcessLayer.HijriToGreg(StrDate, strFormatDate)
                                    If Not IsNothing(StrReturnDate) Then
                                        '============================== Insert GH Date if not found [Start]
                                        Dim strInser As String = "Set Dateformat dmy  Insert Into sys_GHCalendar Values ('" & StrReturnDate & "','" & StrDate & "');"
                                        mSqlCommand.CommandText = strInser
                                        mSqlCommand.Connection.Open()
                                        mSqlCommand.ExecuteNonQuery()
                                        mSqlCommand.Connection.Close()
                                        '============================== Insert GH Date if not found [ END ]
                                    Else
                                        StrReturnDate = StrTempGDate
                                    End If
                                End If

                            Else
                                Return oDate
                            End If
                        Case ClsDataAcessLayer.Directions.Output
                            Return oDate
                    End Select
            End Select
            Return StrReturnDate
        Catch ex As Exception

        End Try
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function CheckDate(ByVal strDate As String) As String

        Return CheckMyDate(strDate)

    End Function

#End Region

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetExpiredContracts()
    End Sub
End Class
