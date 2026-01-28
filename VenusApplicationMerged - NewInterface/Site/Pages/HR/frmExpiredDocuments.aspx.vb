Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView
Imports Infragistics.WebUI
Imports Infragistics.WebUI.UltraWebGrid
Imports OfficeOpenXml
Imports Venus.Application.SystemFiles.HumanResource
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.System.ClsDataAcessLayer

Partial Class frmExpiredDocuments
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
            Dim clsDocType As New Clssys_DocumentsTypes(Page)
            If GetFormPermission("frmEmployeesDocuments") Then
                uwgExpiredDocuments.Bands(0).Columns.FromKey("EmpDocs").Hidden = False
            Else
                uwgExpiredDocuments.Bands(0).Columns.FromKey("EmpDocs").Hidden = True
            End If

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

                Dim clsDocTypeGroups As New Clssys_DocumentTypesGroup(Me)
                clsDocTypeGroups.GetDropDownList(ddlDocumentTypesGroup, True)

                ClsDepartment.GetDropDownList(ddlDepartment, True)
                ddlDepartment.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Departments]/ [ جميع الإدارات]")
                clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
                ddlBranche.Items(0).Text = ClsNavigationHandler.SetLanguage(Page, "[All Branches]/ [ جميع الفروع]")
                ddlBranche.SelectedIndex = 1
                uwgExpiredDocuments.Columns.FromKey("EmployeeName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))

                Page.Session.Add("Lage", ClsNavigationHandler.SetLanguage(Page, "0/1"))
                Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
                hdnLang.Value = ClsNavigationHandler.SetLanguage(Page, "0/1")


                clsDocType.GetDropDownList(ddlDocumentType, True, " isnull(IsForCompany,0) = 0", 0, 0)
                wdcFromDate.Value = Date.Now
                wdcToDate.Value = Date.Now.AddMonths(1)
                GetExpiredDocuments()


            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployee.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Private Function SetDate(gData As Object, hDate As Object) As Date
        Try

            If gData <> "  /  /    " Then
                If ClsDataAcessLayer.IsGreg(gData) Then
                    Return ClsDataAcessLayer.FormatGreg(gData, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.HijriToGreg(gData, "dd/MM/yyyy")
                End If
            ElseIf hDate <> "  /  /    " Then
                If ClsDataAcessLayer.IsHijri(hDate) Then
                    Return ClsDataAcessLayer.HijriToGreg(hDate, "dd/MM/yyyy")
                Else
                    Return ClsDataAcessLayer.FormatGreg(hDate, "dd/MM/yyyy")
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Print.Command, ImageButton_Delete.Command, LinkButton_Delete.Command, LinkButton_Print.Command, ImageButton_Excel.Command, LinkButton_Excel.Command

        Select Case e.CommandArgument
            Case "Delete"
                Dim clsDocInf As New Clssys_DocumentsInformations(Page)

                For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgExpiredDocuments.Rows
                    If row.Cells.FromKey("ID").Value <> Nothing Then
                        If row.Cells.FromKey("Check").Value Then
                            clsDocInf.Delete("ID=" & row.Cells.FromKey("ID").Value)
                        End If
                    End If
                Next
                GetExpiredDocuments()

            Case "Print"
                Dim clsBranch As New Clssys_Branches(Page)
                Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
                Dim clsDAL As New ClsDataAcessLayer(Page)
                clsBranch.Find("ID=" & ddlBranche.SelectedValue)
                ClsDepartment.Find("ID=" & ddlDepartment.SelectedValue)
                Dim Doctype As New Clssys_DocumentsTypes(Me)
                Doctype.Find("ID = " & ddlDocumentType.SelectedValue)
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">OpenEXDOCReport('" & hdnLang.Value & "','" & txtCode.Text.Trim & "|" & clsDAL.SetHigriDate2(wdcFromDate.Text, "") & "|" & clsDAL.SetHigriDate2(wdcToDate.Text, "") & "|" & ClsDepartment.Code.Trim & "|" & clsBranch.Code.Trim & "|" & "0" & "|" & hdnLang.Value & "|" & Doctype.Code & "');</script>")
            Case "Excel"
                Dim clsBranch As New Clssys_Branches(Page)
                clsBranch.Find("ID=" & ddlBranche.SelectedValue)

                Dim sql As String = "hrs_GetAllExpiredDocumentsExcel"
                Dim Objparameter(8) As SqlClient.SqlParameter

                Objparameter(0) = New SqlClient.SqlParameter("@EmpCode", txtCode.Text)
                Objparameter(1) = New SqlClient.SqlParameter("@ExpireFromDate", wdcFromDate.Value)
                Objparameter(2) = New SqlClient.SqlParameter("@ExpireToDate", wdcToDate.Value)
                If ddlDepartment.SelectedIndex > 0 Then

                    Objparameter(3) = New SqlClient.SqlParameter("@DeptCode", ddlDepartment.SelectedValue)
                Else

                    Objparameter(3) = New SqlClient.SqlParameter("@DeptCode", "")
                End If
                If True Then

                End If
                Objparameter(4) = New SqlClient.SqlParameter("@BranchCode", ddlBranche.SelectedValue)

                If ddlDocumentType.SelectedIndex > 0 Then
                    Objparameter(5) = New SqlClient.SqlParameter("@DocumentTypeID", ddlDocumentType.SelectedValue)
                Else
                    Objparameter(5) = New SqlClient.SqlParameter("@DocumentTypeID", "")
                End If
                If ddlDocumentTypesGroup.SelectedIndex > 0 Then
                    Objparameter(6) = New SqlClient.SqlParameter("@DocumentTypesGroupID", ddlDocumentTypesGroup.SelectedValue)
                Else
                    Objparameter(6) = New SqlClient.SqlParameter("@DocumentTypesGroupID", 0)
                End If

                Objparameter(7) = New SqlClient.SqlParameter("@FilterType", 0)

                If ProfileCls.CurrentLanguage = "Ar" Then
                    Objparameter(8) = New SqlClient.SqlParameter("@Lang", 1)
                Else
                    Objparameter(8) = New SqlClient.SqlParameter("@Lang", 0)
                End If

                Dim ObjDataset As New DataSet
                ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsBranch.ConnectionString, CommandType.StoredProcedure, Sql, Objparameter)

                CreateExcelFile(ObjDataset.Tables(0), "ExpiredDocs")
        End Select
    End Sub
    Protected Sub btnFind_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetExpiredDocuments()
    End Sub
    Protected Sub ddlDocumentTypesGroup_SelectedChanged(sender As Object, e As System.EventArgs) Handles ddlDocumentTypesGroup.SelectedIndexChanged

        Dim ClsDocuments = New Clssys_DocumentsTypes(Me.Page)

        Dim criteriaType As String = " 1=1 "
        If ddlDocumentTypesGroup.SelectedIndex > 0 Then
            criteriaType = " isnull(DocumentTypesGroupId,0)=" & ddlDocumentTypesGroup.SelectedValue
        End If
        ClsDocuments.GetDropDownList(ddlDocumentType, True, " isnull(IsForCompany,0) = 0 and " & criteriaType, 0, 0)

    End Sub
#End Region

#Region "Private Functions"
    Private Sub GetExpiredDocuments()
        Dim clsDocInf As New Clssys_DocumentsInformations(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(clsDocInf.ConnectionString)
        Dim Doctype As New Clssys_DocumentsTypes(Me)
        Doctype.Find("ID = " & ddlDocumentType.SelectedValue)

        Dim docTypeGroup = 0
        If ddlDocumentTypesGroup.SelectedIndex > 0 Then
            docTypeGroup = ddlDocumentTypesGroup.SelectedValue
        End If
        Dim dsResult As System.Data.DataSet = GetExpireDocuments(txtCode.Text, ddlDepartment.SelectedValue _
                                                                , IIf(CheckBranchPermission(ddlDepartment.SelectedValue) = "", "0", CheckBranchPermission(ddlDepartment.SelectedValue)), clsDocInf.SetHigriDate2(wdcFromDate.Text, ""),
                                                                clsDocInf.SetHigriDate2(wdcToDate.Text, ""),
                                                                0, Doctype.Code, docTypeGroup)
        If dsResult.Tables(0).Rows.Count > 0 Then
            Dim ds As New System.Data.DataSet
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

    Public Function GetExpireDocuments(ByVal strEmpCode As String, ByVal strDeptCode As String, ByVal strBranchCode As String, ByVal dteFromDate As Date, ByVal dteToDate As Date, ByVal intFilterType As Integer, ByVal intDocumentTypeID As String, ByVal docTypeGroup As Integer) As DataSet
        Dim ObjDataset As New DataSet
        Dim clsDAL As New ClsDataAcessLayer(Page)
        Dim ObjNav As New Venus.Shared.Web.NavigationHandler(clsDAL.ConnectionString)
        ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsDAL.ConnectionString, "[hrs_GetAllExpiredDocuments]", strEmpCode.Trim, IIf(dteFromDate.Year = 1, "", dteFromDate.ToString("dd/MM/yyyy")), IIf(dteToDate.Year = 1, "", dteToDate.ToString("dd/MM/yyyy")), strDeptCode.Trim, strBranchCode.Trim, intDocumentTypeID, docTypeGroup, intFilterType, ObjNav.SetLanguage(Page, "0/1"))
        Return ObjDataset
    End Function

    Private Function GetFormPermission(ByVal frmCode As String) As Boolean
        Dim ClsForms As New ClsSys_Forms(Page)
        Dim ClsFormPermission As New ClsSys_FormsPermissions(Page)
        Dim StrFormPermission As String = "1,1,1"
        If ClsForms.Find(" Code='" & frmCode & "'") Then
            ClsFormPermission.Find("FormID=" & ClsForms.ID)
            With ClsFormPermission
                If .ID > 0 Then
                    StrFormPermission = ""
                    If .AllowEdit Then
                        StrFormPermission = "0"
                    Else
                        StrFormPermission = "1"
                    End If
                    If .AllowDelete Then
                        StrFormPermission &= ",0"
                    Else
                        StrFormPermission &= ",1"
                    End If

                    If .AllowPrint Then
                        StrFormPermission &= ",0"
                    Else
                        StrFormPermission &= ",1"
                    End If
                End If
            End With
        End If
    End Function

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

            str = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, StrSelectCommand)

            Return IIf(str = "", "0", str)

        Catch ex As Exception
            Return "0"
        End Try
    End Function
    Public Shared Function GetRelatedDept(ByVal intDeptID As Integer) As String
        Dim StrSelectCommand As String
        Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
        Dim dsBranches As New DataSet

        Dim str As String = IIf(intDeptID = 0, "", " And DB.DepartmentID = " & intDeptID)
        Try
            StrSelectCommand = " Declare @Branches as nvarchar(max)=''; " & _
                                "Select  @Branches = @Branches + N',' + Cast(B.ID As varchar(200)) " & _
                                "From sys_DepartmentsBranches DB Inner Join sys_Branches B On DB.BranchID = B.ID Where  DB.Checked  = 1 " & str & " And B.CancelDate Is Null; " & _
                                "Select @Branches  = STUFF(@Branches,1,1,''); " & _
                                "Select IsNull(@Branches,'')"

            Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, StrSelectCommand)

        Catch ex As Exception

        End Try
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetRelatedDepartment(ByVal strDeptID As String) As String
        Try

            Dim dsBranches As New DataSet
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

            dsBranches = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, StrSelectCommand)

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
    <System.Web.Services.WebMethod()>
    Public Shared Function CheckDate(ByVal strDate As String) As String

        Return CheckMyDate(strDate)

    End Function
    Public Sub CreateExcelFile(ByVal dt As DataTable, ByVal sheetName As String)
        ' Enable EPPlus to use non-commercial license
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial

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
                Response.AddHeader("content-disposition", "attachment;    filename=ExpiredDocs-" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls")
                Response.BinaryWrite(bytes)
                Response.End()

            End Using
        End Using
    End Sub

#End Region

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetExpiredDocuments()
    End Sub
End Class
