Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports OfficeOpenXml
Imports System.IO
Imports System.Diagnostics

Partial Class frmExcelExportReport
    Inherits MainPage
#Region "Public Decleration"
    Private ObjClssys_Groups As Clssys_Groups
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsEmployees As Clshrs_Employees
    Private _Sys_Report As Clssys_Reports
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ObjClssys_Groups = New Clssys_Groups(Me)
        Dim SearchID As Integer = 0

        Try

            If Not IsPostBack Then
                Dim User As String = String.Empty
                Dim WebHandler As New Venus.Shared.Web.WebHandler
                WebHandler.GetCookies(Page, "UserID", User)
                Dim _sys_User As New Clssys_Users(Page)
                _sys_User.Find("ID = '" & User & "'")
                'Page.Session.Add("ConnectionString", ObjClssys_Groups.ConnectionString)
                'ObjClssys_Groups.AddOnChangeEventToControls("frmExcelExportReport", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                _Sys_Report = New Clssys_Reports(Me)
                _Sys_Report.GetDropDownList(Me.ddlReport, True, "ReportGroupID = 200 and Id in (Select ReportID	From sys_ReportsPermissions  where UserID=" & User & "  and isnull(CanExport,0)=1 )")


                Dim clsBranch As New Clssys_Branches(Page)
                'clsBranch.GetDropDownList(ddlBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")

                Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
                ClsDepartment.GetDropDownList(ddlDepartment, True)

                Dim ClsProfission As New Clshrs_Professions(Me.Page)
                Dim ClsPosition As New Clshrs_Positions(Me.Page)
                'ClsProfission.GetDropDownList(ddlProfessions, True)
                'ClsPosition.GetDropDownList(ddlPosition, True)
                Dim Clslocation As New ClsBasicFiles(Me.Page, "sys_Locations")
                Clslocation.GetDropDownList(ddlUnit, 1)

                Dim ClsNationality As New ClsBasicFiles(Page, "sys_Nationalities")
                'ClsNationality.GetDropDownList(DdlNationality, True)

                Dim ClsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                'ClsProjects.GetDropDownList(ddlProject, True, "CancelDate is null and ID in (select ProjectID from Att_UserProjects where CancelDate is null and UserID = " & ClsProjects.DataBaseUserRelatedID & ")")
                'ClsProjects.GetDropDownList(ddlProject, True, "CancelDate is null")


                ClsEmployees = New Clshrs_Employees(Page)
                Dim csSearchID As Integer
                Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
                Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)


                Dim clsContracttype As New Clshrs_ContractTypes(Page)
                Dim clsSponsor As New Clshrs_Sponsors(Page)
                'Dim clsBranch As New Clssys_Branches(Page)

                Dim ClsObjects As New Clssys_Objects(Page)
                Dim ClsSearchs As New Clssys_Searchs(Page)
                Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language = ""javascript"">IntializeDataChanged()</script>")
                ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim & "'")
                ClsSearchs.Find(" ObjectID=" & ClsObjects.ID)
                csSearchID = ClsSearchs.ID
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtAlternativeUser.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,'" & txtAlternativeUser.ClientID & "'"
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                If ClsObjects.Find(" Code='" & clsContracttype.Table.Trim & "'") Then
                    If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                        IntDimension = 510
                        UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TextBox_Contract.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & TextBox_Contract.ClientID & "'"
                        WebImageButton_Cont.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                    End If
                End If

                If ClsObjects.Find(" Code='" & clsSponsor.Table.Trim & "'") Then
                    If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                        IntDimension = 510
                        UrlString = "' frmModalSearchScreen.aspx?TargetControl=" & TextBox_Sponsor.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & TextBox_Sponsor.ClientID & "'"
                        WebImageButton_Sponsor.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                    End If
                End If

                clsBranch.GetDropDownList(ddlBranche, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")

                'Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
                Dim strbranchfilter As String = ""
                'strbranchfilter = " and BranchID = " & ddlBranche.SelectedValue
                ClsProjects.GetDropDownList(DropDownList_Project, True, "isnull(BranchID,0) = 0 or (IsLocked = 1 and isnull(IsStoped,0) = 0 and CancelDate is null " & strbranchfilter & ")")

                Dim ClsNationalities As New ClsBasicFiles(Me.Page, "sys_Nationalities")
                ClsNationalities.GetDropDownList(ddlNationality, True)

                TxtAlternativeEmpName.Enabled = False
                Dim day As Integer = DateTime.Now.Day - 1
                txtFromDate.Text = DateTime.Now.AddDays(-day).ToString("yyyy/MM/dd")
                TxtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd")


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

    Protected Sub btnHR_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnHR.Command
        'Dim clsUpdate As ClsSys_UpdateDB
        'clsUpdate = New ClsSys_UpdateDB(Me)
        _Sys_Report = New Clssys_Reports(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(_Sys_Report.ConnectionString)
        If ddlReport.SelectedValue < 1 Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please..Select Report/برجاء اختيار التقرير"))
            Return
        End If
        If _Sys_Report.Find("ID = " & ddlReport.SelectedValue) Then
            Dim sql As String = _Sys_Report.DataSource
            'Dim BranchID As String = Nothing
            'If ddlBranch.SelectedValue > 0 Then
            '    BranchID = ddlBranch.SelectedValue
            'End If
            Dim DepartmentID As Integer = Nothing
            If ddlDepartment.SelectedValue > 0 Then
                DepartmentID = ddlDepartment.SelectedValue
            End If
            Dim UnitID As Integer = Nothing
            If ddlUnit.SelectedValue > 0 Then
                UnitID = ddlUnit.SelectedValue
            End If
            'Dim ProfissionID As Integer = Nothing
            'If ddlProfessions.SelectedValue > 0 Then
            '    ProfissionID = ddlProfessions.SelectedValue
            'End If
            'Dim PositionID As Integer = Nothing
            'If ddlPosition.SelectedValue > 0 Then
            '    PositionID = ddlPosition.SelectedValue
            'End If
            'Dim NationaltyID As Integer = Nothing
            'If DdlNationality.SelectedValue > 0 Then
            '    NationaltyID = DdlNationality.SelectedValue
            'End If
            Dim FromDate As Date = Nothing
            If txtFromDate.Text <> "" Then
                FromDate = Convert.ToDateTime(txtFromDate.Text).Date
            End If
            Dim ToDate As Date = Nothing
            If TxtToDate.Text <> "" Then
                ToDate = Convert.ToDateTime(TxtToDate.Text).Date
            End If
            Dim EmployeeID As String = Nothing
            If Not String.IsNullOrEmpty(txtAlternativeUser.Text) Then
                ClsEmployees = New Clshrs_Employees(Page)
                ClsEmployees.Find("Code='" & txtAlternativeUser.Text & "'")
                EmployeeID = ClsEmployees.ID
            End If
            Dim ContractTypeID As String = Nothing
            If Not String.IsNullOrEmpty(TextBox_Contract.Text) Then
                Dim clsContracttype As New Clshrs_ContractTypes(Page)
                clsContracttype.Find("Code='" & TextBox_Contract.Text & "'")
                ContractTypeID = clsContracttype.ID
            End If
            Dim SponsorID As String = Nothing
            If Not String.IsNullOrEmpty(TextBox_Sponsor.Text) Then
                Dim clsSponsor As New Clshrs_Sponsors(Page)
                clsSponsor.Find("Code='" & TextBox_Sponsor.Text & "'")
                SponsorID = clsSponsor.ID
            End If

            Dim BranchID As Integer = Nothing
            If ddlBranche.SelectedValue > 0 Then
                BranchID = ddlBranche.SelectedValue
            End If

            Dim ProjectID As Integer = Nothing
            If DropDownList_Project.SelectedValue > 0 Then
                ProjectID = DropDownList_Project.SelectedValue
            End If
            Dim NationalityID As Integer = Nothing
            If ddlNationality.SelectedValue > 0 Then
                NationalityID = ddlNationality.SelectedValue
            End If
            'Dim ProjectID As Integer = Nothing
            'If ddlProject.SelectedValue > 0 Then
            '    ProjectID = ddlProject.SelectedValue
            'End If
            'Dim ShiftID As Integer = Nothing
            'If DdlShift.SelectedValue > 0 Then
            '    ShiftID = DdlShift.SelectedValue
            'End If

            Dim Objparameter(10) As SqlClient.SqlParameter
            'Objparameter(0) = New SqlClient.SqlParameter("@BranchID", BranchID)
            If ProfileCls.CurrentLanguage = "Ar" Then
                Objparameter(0) = New SqlClient.SqlParameter("@Lang", 1)
            Else

                Objparameter(0) = New SqlClient.SqlParameter("@Lang", 0)

            End If

            Objparameter(1) = New SqlClient.SqlParameter("@DepartmentID", DepartmentID)
            Objparameter(2) = New SqlClient.SqlParameter("@UnitID", UnitID)
            'Objparameter(2) = New SqlClient.SqlParameter("@ProfissionID", ProfissionID)
            'Objparameter(3) = New SqlClient.SqlParameter("@PositionID", PositionID)
            'Objparameter(4) = New SqlClient.SqlParameter("@NationaltyID", NationaltyID)
            Objparameter(3) = New SqlClient.SqlParameter("@FromDate", FromDate)
            Objparameter(4) = New SqlClient.SqlParameter("@ToDate", ToDate)
            Objparameter(5) = New SqlClient.SqlParameter("@EmployeeID", EmployeeID)

            '           @ContractTypeId int=null,
            '@BranchId int=null,
            '@SponsorId int=null,
            '@ProjectId int=null,
            '@NationalityId int=null
            Objparameter(6) = New SqlClient.SqlParameter("@ContractTypeId", ContractTypeID)
            Objparameter(7) = New SqlClient.SqlParameter("@BranchId", BranchID)
            Objparameter(8) = New SqlClient.SqlParameter("@SponsorId", SponsorID)
            Objparameter(9) = New SqlClient.SqlParameter("@ProjectId", ProjectID)
            Objparameter(10) = New SqlClient.SqlParameter("@NationalityId", NationalityID)

            'Objparameter(8) = New SqlClient.SqlParameter("@ProjectID", ProjectID)
            'Objparameter(9) = New SqlClient.SqlParameter("@ShiftID", ShiftID)
            'Dim Objparameter(1) As SqlClient.SqlParameter
            'Objparameter(0) = New SqlClient.SqlParameter("@EmployeeID", EmployeeID)
            Dim ObjDataset As New DataSet
                ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(_Sys_Report.ConnectionString, CommandType.StoredProcedure, sql, Objparameter)

                CreateExcelFile(ObjDataset.Tables(0), ddlReport.SelectedItem.Text)
            End If


    End Sub
    Protected Sub txtAlternative_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAlternativeUser.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtAlternativeUser.Text) Then
                ClsEmployees = New Clshrs_Employees(Page)
                ClsEmployees.Find("Code='" & txtAlternativeUser.Text & "'")
                If ClsEmployees.ID > 0 Then

                    TxtAlternativeEmpName.Text = ClsEmployees.EnglishName
                Else
                    TxtAlternativeEmpName.Text = ""
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DdlRequestType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartment.SelectedIndexChanged

    End Sub
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
                Response.AddHeader("content-disposition", "attachment;    filename=" & ddlReport.SelectedItem.Text & "-" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls")
                Response.BinaryWrite(bytes)
                    Response.End()

            End Using
        End Using
    End Sub

#End Region
End Class
