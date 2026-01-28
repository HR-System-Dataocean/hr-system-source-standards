Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.IO
Imports Excel


Partial Class Interfaces_frmIncreasesLoad
    Inherits System.Web.UI.Page

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields
    Dim ClsEmployee As Clshrs_Employees
    Dim ClsContracts As Clshrs_Contracts
    Dim ClsContractTransactions As Clshrs_ContractsTransactions
#End Region

#Region "Protected Sub"
    Protected Overrides Sub InitializeCulture()
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage
        'Use this
        If (_culture <> "Auto") Then
            Dim ci As New System.Globalization.CultureInfo(_culture)
            Dim StrDateFormat As String = System.Configuration.ConfigurationManager.AppSettings("DATEFORMAT")
            Dim myDateTimePatterns() As String = {StrDateFormat, StrDateFormat}
            Dim DateTimeFormat As New System.Globalization.DateTimeFormatInfo
            DateTimeFormat = ci.DateTimeFormat
            DateTimeFormat.SetAllDateTimePatterns(myDateTimePatterns, "d"c)
            System.Threading.Thread.CurrentThread.CurrentCulture = ci
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci
        End If
        MyBase.InitializeCulture()

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsProjects As New Clshrs_Projects(Page, "hrs_Projects")
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
        If Not IsPostBack Then
            If Request.QueryString(0) = "FI" Then
                Label4.Visible = True
                DdlPeriods.Visible = True

                Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
                Dim IntModuleId As Integer = GetModuleID("frmPrepareSalaries")
                ClsFisicalYearsPeriods.GetDropDownList(DdlPeriods, IntModuleId, True, "")
            End If
        End If
        Dim objNav As New Venus.Shared.Web.NavigationHandler(clsProjects.ConnectionString)
        If objNav.SetLanguage(Page, "Eng/Arb") = "Eng" Then
            Session.Add("Lang", True)
        Else
            Session.Add("Lang", False)
        End If
    End Sub
    Private Function GetModuleID(ByVal TableName As String) As Integer
        Dim ClsForms As New ClsSys_Forms(Me.Page)
        Dim IntModuleID As Integer
        ClsForms.Find(" Code = '" & TableName & "'")
        If ClsForms.ID > 0 Then
            IntModuleID = ClsForms.ModuleID
        End If
        Return IntModuleID
    End Function
#End Region

    Private Function LoadAttendFile() As Boolean
        Dim clsDAL As New ClsDataAcessLayer(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(clsDAL.ConnectionString)
        If Request.QueryString(0) = "FI" Then
            If DdlPeriods.SelectedValue = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Must Select Fiscal Period/لابد من اختيار الفترة المالية"))
            End If
        End If
        Try
            Dim strFileName As String = FileUpload1.FileName
            Dim fileExt As String
            Dim dwlPath As String = Request.PhysicalApplicationPath & "EditContracts"
            Dim strFinalPath As String = String.Empty

            If strFileName.Trim <> String.Empty Then
                fileExt = System.IO.Path.GetExtension(strFileName).ToLower()
                If fileExt.ToLower = ".xls" Or fileExt.ToLower = ".xlsx" Then
                    If Not Directory.Exists(dwlPath) Then Directory.CreateDirectory(dwlPath)
                    Dim DisFile As String = DateTime.Now.ToString("ddMMyyyyHHmmss") & "_" & strFileName
                    FileUpload1.SaveAs(dwlPath & "\" & DisFile)
                    strFinalPath = dwlPath & "\" & DisFile
                    Dim dsResult As New Data.DataSet

                    Dim oledbConn As New Data.OleDb.OleDbConnection()
                    If fileExt = ".xls" Then
                        oledbConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strFinalPath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
                    ElseIf fileExt = ".xlsx" Then
                        oledbConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strFinalPath & ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"""
                    End If
                    oledbConn.Open()
                    Dim cmd As New Data.OleDb.OleDbCommand()
                    cmd.CommandType = System.Data.CommandType.Text
                    Dim oleda As New Data.OleDb.OleDbDataAdapter()
                    Dim dt As Data.DataTable = oledbConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, Nothing)
                    Dim workSheetName As String = DirectCast(dt.Rows(0)("TABLE_NAME"), String)
                    cmd.Connection = oledbConn
                    cmd.CommandType = Data.CommandType.Text
                    cmd.CommandText = "SELECT * FROM [" & workSheetName & "]"
                    oleda = New Data.OleDb.OleDbDataAdapter(cmd)
                    oleda.Fill(dsResult)
                    oledbConn.Close()
                    Dim strcommand As String = "set dateformat dmy"
                    Dim PatchDate As DateTime = DateTime.Now
                    For Each iRow As Data.DataRow In dsResult.Tables(0).Rows
                        If Request.QueryString(0) = "SI" Then
                            Dim EmployeeCode As String = ""
                            Try
                                EmployeeCode = iRow("EmployeeCode").ToString()
                            Catch ex As Exception
                                EmployeeCode = ""
                            End Try
                            If EmployeeCode <> "" Then


                                Dim EmployeeName As String = ""
                                Try
                                    EmployeeName = iRow("EmployeeName").ToString()
                                Catch ex As Exception
                                    EmployeeName = ""
                                End Try
                                Dim TransactionCode As String = ""
                                Try
                                    TransactionCode = iRow("TransactionCode").ToString()
                                Catch ex As Exception
                                    TransactionCode = ""
                                End Try
                                Dim Amount As Decimal = 0
                                Try
                                    Amount = Convert.ToDecimal(iRow("Amount"))
                                Catch ex As Exception
                                    Amount = ""
                                End Try
                                Dim ActiveDate As DateTime
                                Try
                                    ActiveDate = Convert.ToDateTime(iRow("ActiveDate"))
                                Catch ex As Exception
                                    ActiveDate = DateTime.MaxValue
                                End Try
                                strcommand = strcommand & Environment.NewLine & "insert into hrs_EmployeeIncreases values ('" & EmployeeCode & "','" & EmployeeName & "','" & TransactionCode & "'," & Amount & ",'" & ActiveDate & "',0,'" & PatchDate.ToString("dd/MM/yyyy HH:mm:ss") & "')"
                            End If
                        ElseIf Request.QueryString(0) = "FI" Then
                            Dim EmployeeCode As String = ""
                            If iRow("EmployeeCode").ToString() <> "" Then
                                Try
                                    EmployeeCode = iRow("EmployeeCode").ToString()
                                Catch ex As Exception
                                    EmployeeCode = ""
                                End Try
                                Dim EmployeeName As String = ""
                                Try
                                    EmployeeName = iRow("EmployeeName").ToString()
                                Catch ex As Exception
                                    EmployeeName = ""
                                End Try
                                Dim ProjectCode As String = ""
                                Try
                                    ProjectCode = iRow("ProjectCode").ToString()
                                Catch ex As Exception
                                    ProjectCode = ""
                                End Try
                                Dim TransactionCode As String = ""
                                Try
                                    TransactionCode = iRow("TransactionCode").ToString()
                                Catch ex As Exception
                                    TransactionCode = ""
                                End Try
                                Dim Amount As Decimal = 0
                                Try
                                    Amount = Convert.ToDecimal(iRow("Amount"))
                                Catch ex As Exception
                                    Amount = "0"
                                End Try
                                Dim Fiscl As Int32 = 0
                                Try
                                    Fiscl = Convert.ToInt32(DdlPeriods.SelectedValue)
                                Catch ex As Exception
                                    Fiscl = 0
                                End Try
                                strcommand = strcommand & Environment.NewLine & "Delete from hrs_EmployeeExtraItems where EmployeeCode=" & EmployeeCode & " And TransactionCode=" & TransactionCode & " and FiscalPeriodID=" & Fiscl & " and ProjectID= " & ProjectCode & ""
                                strcommand = strcommand & Environment.NewLine & "insert into hrs_EmployeeExtraItems values ('" & EmployeeCode & "','" & EmployeeName & "','" & TransactionCode & "'," & Amount & "," & Fiscl & ",0,'" & PatchDate.ToString("dd/MM/yyyy HH:mm:ss") & "',1,'','" & ProjectCode & "','')"
                            End If

                        ElseIf Request.QueryString(0) = "UP" Then
                                Dim SSno As String = ""
                                Dim LaborOffice As String = ""
                                Try
                                    SSno = iRow("SSno").ToString()
                                Catch ex As Exception
                                    SSno = ""
                                End Try
                                Try
                                    LaborOffice = iRow("LaborOffice").ToString()
                                Catch ex As Exception
                                    LaborOffice = ""
                                End Try
                                strcommand = strcommand & Environment.NewLine & "update  hrs_employees set LaborOfficeNo='" & LaborOffice & "' where SSnNo ='" & SSno & "';"


                            End If
                    Next
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsDAL.ConnectionString, Data.CommandType.Text, strcommand)
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Done/تمت العملية"))
                    lblProgress.Text = IIf(Session("Lang"), "Operation Done", "تمت العملية ")
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
                Else
                    lblProgress.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                             objNav.SetLanguage(Page, "File Format Error/خطأ في صيغة الملف"))
                End If
            Else
                lblProgress.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                            objNav.SetLanguage(Page, "Select File/اختر الملف"))
            End If
        Catch ex As Exception
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Failled/فشلت العملية"))
            lblProgress.Text = IIf(Session("Lang"), "Operation Failled", "فشلت العملية " & ex.Message.ToString())
        End Try
    End Function
    Protected Sub ImageButton_Print_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton.Click
        Try
            lblProgress.Text = IIf(Session("Lang"), "Loading...", "جاري التحميل ....")
        Catch ex As Exception
            lblProgress.Text = "Loading..."
        End Try
        LoadAttendFile()
    End Sub
End Class
