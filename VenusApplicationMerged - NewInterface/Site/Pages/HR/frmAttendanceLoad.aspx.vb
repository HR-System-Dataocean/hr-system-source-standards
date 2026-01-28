Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.IO
Imports System.Data.OleDb
Imports System.Threading
Imports System.Data
Partial Class Interfaces_frmAttendanceLoad
    Inherits System.Web.UI.Page

#Region "Public Decleration"
    Dim mErrorHandler As Venus.Shared.ErrorsHandler
    Dim clsMainOtherFields As clsSys_MainOtherFields
    Dim AttendanceTransactions As ClsAtt_AttendTransactions
    Dim ClsEmployee As Clshrs_Employees
    Dim ClsContracts As Clshrs_Contracts
    Dim ClsEmployeeClasses As Clshrs_EmployeeClasses
    Dim ClsEmployeesClassesCalenderSet As Clshrs_EmployeesClassesCalenderSet
    Dim ClsEmployeesClassesCalender As Clshrs_EmployeesClassCalander
#End Region

#Region "Protected Sub"

    Protected Overrides Sub InitializeCulture()
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage
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
            clsProjects.GetDropDownList(DropDownList2, True)
            DropDownList3.SelectedValue = DateTime.Now.Year
        End If

        ddlFileExtension.SelectedValue = ConfigurationManager.AppSettings("FileType")
        lblCounter.Text = "0 of  0"
    End Sub

#End Region

    Private Function CheckTime(ByVal mTime As String) As Date
        If mTime.Length = 0 Then
            Return Nothing
        Else
            Return Convert.ToDateTime(mTime).ToString("HH:mm")
        End If
    End Function
    Private Function RetDayNumber(ByVal TrnsDate As DateTime) As Integer
        Dim DayNumber As Integer = 0
        Dim Dayidx As Integer = TrnsDate.DayOfWeek
        If Dayidx = 0 Then
            DayNumber = 1
        ElseIf Dayidx = 1 Then
            DayNumber = 2
        ElseIf Dayidx = 2 Then
            DayNumber = 3
        ElseIf Dayidx = 3 Then
            DayNumber = 4
        ElseIf Dayidx = 4 Then
            DayNumber = 5
        ElseIf Dayidx = 5 Then
            DayNumber = 6
        ElseIf Dayidx = 6 Then
            DayNumber = 7
        End If
        Return DayNumber
    End Function
    Enum FileType
        Excel = 0
        Majed_CSV = 1
        NMK_Excel = 2
        Elkhiriji_Excel = 3
        Carlo = 4
        ExcelTotal = 5
        ExcelKenaaz = 6
        ExcelTadawi = 7
        ExcelByProject = 8
        ExcelByProjectandSSnNo = 9
        ExcelByProjectandSSnNoAndNationlityAndDep = 10

    End Enum
    Private Const BaseDate As DateTime = #1/1/1900#
    Private Const BaseDays As Integer = 36525
    Public Function ToFileDateTime(ByVal value As DateTime) As Integer
        Dim ts As TimeSpan = value.Date.Subtract(BaseDate)
        Return CInt(ts.TotalDays) + BaseDays
    End Function
    Public Function FromFileDateTime(ByVal days As Integer) As DateTime
        Return BaseDate.AddDays(days)
    End Function
    Private Function LoadAttendFile(ByVal Type As FileType) As Boolean
        Dim clsDAL As New ClsDataAcessLayer(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(clsDAL.ConnectionString)
        Dim ingr = 1
        Try
            Dim AttendanceTransactions As New ClsAtt_AttendTransactions(Page)
            ClsEmployee = New Clshrs_Employees(Page)
            ClsContracts = New Clshrs_Contracts(Page)
            ClsEmployeeClasses = New Clshrs_EmployeeClasses(Page)
            Dim clsProject As New Clshrs_Projects(Page, "hrs_Projects")
            Dim strFileName As String = FileUpload1.FileName
            Dim fileExt As String
            Dim dwlPath As String = Request.PhysicalApplicationPath & "AttendFolder"
            Dim strFinalPath As String = String.Empty
            If strFileName.Trim <> String.Empty Then
                fileExt = System.IO.Path.GetExtension(strFileName).ToLower()
                If (fileExt.ToLower = ".xls" Or fileExt.ToLower = ".xlsx" Or fileExt.ToLower = ".txt" Or fileExt.ToLower = ".csv") Then
                    If Not Directory.Exists(dwlPath) Then Directory.CreateDirectory(dwlPath)
                    Dim DisFile As String = DateTime.Now.ToString("ddMMyyyyHHmmss") & "_" & strFileName
                    FileUpload1.SaveAs(dwlPath & "\" & DisFile)
                    strFinalPath = dwlPath & "\" & DisFile
                    Dim dsResult As New Data.DataSet
                    If Type = FileType.Excel Or Type = FileType.NMK_Excel Or Type = FileType.Elkhiriji_Excel Or Type = FileType.Carlo Or FileType.ExcelTotal Then
                        Dim oledbConn As New OleDbConnection()
                        'If fileExt = ".xls" Then
                        '    oledbConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strFinalPath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
                        'ElseIf fileExt = ".xlsx" Then
                        '    oledbConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strFinalPath & ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"""
                        '    '     oledbConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strFinalPath & ";Extended Properties="Excel 12.0 Xml;HDR=YES";IMEX=1"""
                        'End If
                        'If fileExt = ".xlsx" Then
                        '    oledbConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strFinalPath & ";Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text"""
                        'ElseIf fileExt = ".xls" Then
                        '    oledbConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strFinalPath & ";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text"""
                        'End If

                        If fileExt = ".xlsx" Then
                            oledbConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strFinalPath & ";Extended Properties=""Excel 12.0;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text"""
                        ElseIf fileExt = ".xls" Then
                            oledbConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strFinalPath & ";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text"""
                        End If

                        oledbConn.Open()
                        Dim cmd As New OleDbCommand()
                        cmd.CommandType = System.Data.CommandType.Text
                        Dim oleda As New OleDbDataAdapter()
                        Dim dt As Data.DataTable = oledbConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, Nothing)
                        Dim workSheetName As String = DirectCast(dt.Rows(0)("TABLE_NAME"), String)
                        cmd.Connection = oledbConn
                        cmd.CommandType = Data.CommandType.Text
                        cmd.CommandText = "Select * FROM [Sheet1$]"
                        oleda = New OleDbDataAdapter(cmd)
                        oleda.Fill(dsResult)
                        oledbConn.Close()
                    End If
                    Select Case Type
                        Case FileType.Excel
                            dsResult.Tables(0).Columns(0).ColumnName = "ID"
                            For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                                Try
                                    ingr = ingr + 1
                                    'Dim Date AS String = 
                                    Dim Datestr As String = Row("Date")
                                    Dim commandString = ""
                                    If ClsDataAcessLayer.IsHijri(Row("Date")) Then
                                        Datestr = ClsDataAcessLayer.HijriToGreg(Row("Date"), "dd/MM/yyyy")
                                        commandString = "Select CONVERT(VARCHAR(10),'" & Datestr & "'),"
                                    ElseIf ClsDataAcessLayer.IsGreg(Row("DATE")) Then
                                        Datestr = Row("DATE")
                                        commandString = "select CONVERT(VARCHAR(10),'" & Datestr & "'),"
                                    End If
                                    Dim chcint As Integer
                                    If Convert.ToString(Row("IN")) <> "" And Convert.ToString(Row("IN")) <> "0" Then
                                        commandString = commandString & "CONVERT(VARCHAR(5), cast(CAST(cast(cast('" & Convert.ToDateTime(Convert.ToString(IIf(Integer.TryParse(Row("IN"), chcint), Row("IN") & ":00", Row("IN"))).Replace("Õ", " AM ").Replace("ã", " PM ").Replace(".", ":")).ToString("HH:mm") & "' as datetime) as time) AS smalldatetime) as time)),"
                                    Else
                                        commandString = commandString & "CONVERT(VARCHAR(5), cast(CAST(cast(cast(0 as datetime) as time) AS smalldatetime) as time)),"
                                    End If

                                    If Convert.ToString(Row("OUT")) <> "" And Convert.ToString(Row("OUT")) <> "0" Then
                                        commandString = commandString & "CONVERT(VARCHAR(5), cast(CAST(cast(cast('" & Convert.ToDateTime(Convert.ToString(IIf(Integer.TryParse(Row("OUT"), chcint), Row("OUT") & ":00", Row("OUT"))).Replace("Õ", " AM ").Replace("ã", " PM ").Replace(".", ":")).ToString("HH:mm") & "' as datetime) as time) AS smalldatetime) as time))"
                                    Else
                                        commandString = commandString & "CONVERT(VARCHAR(5), cast(CAST(cast(cast(0 as datetime) as time) AS smalldatetime) as time))"
                                    End If

                                    Dim ConverSrc As Data.DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, System.Data.CommandType.Text, commandString)
                                    ClsEmployee = New Clshrs_Employees(Page)
                                    If GetEmployee(Row("ID").ToString, ClsEmployee) Then
                                        Dim ProjID As String = "0"
                                        If ClsEmployee.ID <> 0 Then
                                            If DropDownList2.SelectedValue = 0 Then
                                                Try
                                                    ProjID = Row("Project").ToString()
                                                    clsProject.Find(" Code='" & ProjID & "'")
                                                    ProjID = clsProject.ID
                                                Catch ex As Exception
                                                    ProjID = ClsEmployeeClasses.DefaultProjectID
                                                End Try
                                                If ProjID = "0" Then
                                                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                                                    objNav.SetLanguage(Page, "Select Project/ÇÎÊÑ ÇáãÔÑæÚ"))
                                                    Return False
                                                End If
                                            Else
                                                ProjID = DropDownList2.SelectedValue
                                            End If
                                            AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                            AttendanceTransactions.TrnsDatetime = CDate(ConverSrc.Tables(0).Rows(0)(0))
                                            AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                            AttendanceTransactions.ProjectID = ProjID
                                            If Convert.ToString(Row("IN")) <> "" Then
                                                AttendanceTransactions.TimeIn = ConverSrc.Tables(0).Rows(0)(1)
                                            End If
                                            If Convert.ToString(Row("OUT")) <> "" Then
                                                AttendanceTransactions.TimeOut = ConverSrc.Tables(0).Rows(0)(2)
                                            End If
                                            AttendanceTransactions.TotalHours = 0
                                            If Convert.ToString(Row("IN")) <> "" And Convert.ToString(Row("OUT")) <> "" Then
                                                If (Convert.ToDateTime(ConverSrc.Tables(0).Rows(0)(2)) < Convert.ToDateTime(ConverSrc.Tables(0).Rows(0)(1))) Then
                                                    AttendanceTransactions.TotalHours = (Convert.ToDateTime(ConverSrc.Tables(0).Rows(0)(2)).AddDays(1) - Convert.ToDateTime(ConverSrc.Tables(0).Rows(0)(1))).TotalHours
                                                Else
                                                    AttendanceTransactions.TotalHours = (Convert.ToDateTime(ConverSrc.Tables(0).Rows(0)(2)) - Convert.ToDateTime(ConverSrc.Tables(0).Rows(0)(1))).TotalHours
                                                End If
                                            End If
                                            Dim Isstatus = 0
                                            For Each col As Data.DataColumn In dsResult.Tables(0).Columns
                                                If col.ColumnName = "STATUS" Then
                                                    Isstatus = IIf(Convert.ToString(Row("STATUS")) = "", -1, Row("STATUS"))
                                                End If
                                            Next
                                            AttendanceTransactions.Status = Isstatus
                                            AttendanceTransactions.Src = 1
                                            AttendanceTransactions.RegDate = DateTime.Now
                                            AttendanceTransactions.RegUserID = ClsEmployee.DataBaseUserRelatedID
                                            Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                            If AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and ProjectID = " & AttendanceTransactions.ProjectID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)") Then
                                                AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                            Else
                                                AttendanceTransactions.Save()
                                            End If
                                        End If
                                    End If
                                Catch ex As Exception
                                    Continue For
                                End Try
                            Next
                        Case FileType.NMK_Excel
                            dsResult.Tables(0).Columns(0).ColumnName = "EmpNo"
                            dsResult.Tables(0).Columns(2).ColumnName = "TrnsDate"
                            dsResult.Tables(0).Columns(3).ColumnName = "FrstIn"
                            dsResult.Tables(0).Columns(4).ColumnName = "FrstOut"
                            dsResult.Tables(0).Columns(5).ColumnName = "ScndIn"
                            dsResult.Tables(0).Columns(6).ColumnName = "ScndOut"
                            dsResult.Tables(0).Columns(7).ColumnName = "ThrdIn"
                            dsResult.Tables(0).Columns(8).ColumnName = "ThrdOut"

                            For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                                Try
                                    ClsEmployee = New Clshrs_Employees(Page)
                                    If Row("EmpNo").ToString = "" Then
                                        Exit For
                                    End If
                                    If GetEmployee(Row("EmpNo").ToString, ClsEmployee) Then
                                        Dim ProjID As String = "0"
                                        If ClsEmployee.ID <> 0 Then
                                            If DropDownList2.SelectedValue = 0 Then
                                                Try
                                                    ProjID = Row("Project").ToString()
                                                    clsProject.Find(" Code='" & ProjID & "'")
                                                    ProjID = clsProject.ID
                                                Catch ex As Exception
                                                    ProjID = ClsEmployeeClasses.DefaultProjectID
                                                End Try
                                                If ProjID = "0" Then
                                                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                                                    objNav.SetLanguage(Page, "Select Project/ÇÎÊÑ ÇáãÔÑæÚ"))
                                                    Return False
                                                End If
                                            Else
                                                ProjID = DropDownList2.SelectedValue
                                            End If
                                            If ClsEmployee.ID = 202 Then
                                                Dim strdfgfg As String = ""
                                            End If
                                            If Row("FrstIn").ToString() <> "" Then
                                                AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                                AttendanceTransactions.TrnsDatetime = CDate(Row("TrnsDate") & " " & Row("FrstIn"))
                                                AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                                AttendanceTransactions.ProjectID = 1073
                                                Dim Dtstin As DateTime = CDate(Row("TrnsDate") & " " & Row("FrstIn"))
                                                AttendanceTransactions.TimeIn = Dtstin.ToString("HH:mm")
                                                If Row("FrstOut").ToString() <> "" Then
                                                    Dim Dtstout As DateTime = CDate(Row("TrnsDate") & " " & Row("FrstOut"))
                                                    AttendanceTransactions.TimeOut = Dtstout.ToString("HH:mm")
                                                    If Dtstout < Dtstin Then
                                                        AttendanceTransactions.TotalHours = (Dtstout.AddDays(1) - Dtstin).TotalHours
                                                    Else
                                                        AttendanceTransactions.TotalHours = (Dtstout - Dtstin).TotalHours
                                                    End If
                                                End If

                                                AttendanceTransactions.Status = -1
                                                ClsEmployeesClassesCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                                                ClsEmployeesClassesCalender = New Clshrs_EmployeesClassCalander(Page)
                                                If ClsEmployeesClassesCalender.Find("EmployeeClassID = " & ClsEmployeeClasses.ID & " and CONVERT(date,FromTime,103) = CONVERT(date,'" & AttendanceTransactions.TrnsDatetime.ToString("dd/MM/yyyy") & "',103)") Then
                                                    If ClsEmployeesClassesCalender.nonWorkingTime Then
                                                        AttendanceTransactions.Status = 1
                                                    End If
                                                End If
                                                If (ClsEmployeesClassesCalenderSet.Find("EmployeeClassID = " & ClsEmployeeClasses.ID & " and DayNumber = " & RetDayNumber(AttendanceTransactions.TrnsDatetime)) = True) Then
                                                    If ClsEmployeesClassesCalenderSet.NonWorkingTime Then
                                                        AttendanceTransactions.Status = 1
                                                    End If
                                                End If

                                                AttendanceTransactions.Src = 1
                                                AttendanceTransactions.RegDate = DateTime.Now
                                                AttendanceTransactions.RegUserID = ClsEmployee.DataBaseUserRelatedID
                                                Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                                If AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and ProjectID = " & AttendanceTransactions.ProjectID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)") Then
                                                    AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                                Else
                                                    AttendanceTransactions.Save()
                                                End If
                                            End If
                                            If Row("ScndIn").ToString() <> "" Then
                                                AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                                AttendanceTransactions.TrnsDatetime = CDate(Row("TrnsDate") & " " & Row("ScndIn"))
                                                AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                                AttendanceTransactions.ProjectID = 1074

                                                Dim Dtsndin As DateTime = CDate(Row("TrnsDate") & " " & Row("ScndIn"))
                                                AttendanceTransactions.TimeIn = Dtsndin.ToString("HH:mm")
                                                AttendanceTransactions.TotalHours = 0
                                                If Row("ScndOut").ToString() <> "" Then
                                                    Dim Dtsndout As DateTime = CDate(Row("TrnsDate") & " " & Row("ScndOut"))
                                                    AttendanceTransactions.TimeOut = Dtsndout.ToString("HH:mm")
                                                    If Dtsndout < Dtsndin Then
                                                        AttendanceTransactions.TotalHours = (Dtsndout.AddDays(1) - Dtsndin).TotalHours
                                                    Else
                                                        AttendanceTransactions.TotalHours = (Dtsndout - Dtsndin).TotalHours
                                                    End If
                                                End If

                                                AttendanceTransactions.Status = -1
                                                ClsEmployeesClassesCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                                                ClsEmployeesClassesCalender = New Clshrs_EmployeesClassCalander(Page)
                                                If ClsEmployeesClassesCalender.Find("EmployeeClassID = " & ClsEmployeeClasses.ID & " and CONVERT(date,FromTime,103) = CONVERT(date,'" & AttendanceTransactions.TrnsDatetime.ToString("dd/MM/yyyy") & "',103)") Then
                                                    If ClsEmployeesClassesCalender.nonWorkingTime Then
                                                        AttendanceTransactions.Status = 1
                                                    End If
                                                End If
                                                If (ClsEmployeesClassesCalenderSet.Find("EmployeeClassID = " & ClsEmployeeClasses.ID & " and DayNumber = " & RetDayNumber(AttendanceTransactions.TrnsDatetime)) = True) Then
                                                    If ClsEmployeesClassesCalenderSet.NonWorkingTime Then
                                                        AttendanceTransactions.Status = 1
                                                    End If
                                                End If

                                                AttendanceTransactions.Src = 1
                                                AttendanceTransactions.RegDate = DateTime.Now
                                                AttendanceTransactions.RegUserID = ClsEmployee.DataBaseUserRelatedID
                                                Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                                If AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and ProjectID = " & AttendanceTransactions.ProjectID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)") Then
                                                    AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                                Else
                                                    AttendanceTransactions.Save()
                                                End If
                                            End If
                                            If Row("ThrdIn").ToString() <> "" Then
                                                AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                                AttendanceTransactions.TrnsDatetime = CDate(Row("TrnsDate") & " " & Row("ThrdIn"))
                                                AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                                AttendanceTransactions.ProjectID = 1075

                                                Dim Dtsndin As DateTime = CDate(Row("TrnsDate") & " " & Row("ThrdIn"))
                                                AttendanceTransactions.TimeIn = Dtsndin.ToString("HH:mm")
                                                AttendanceTransactions.TotalHours = 0
                                                If Row("ThrdOut").ToString() <> "" Then
                                                    Dim Dtsndout As DateTime = CDate(Row("TrnsDate") & " " & Row("ThrdOut"))
                                                    AttendanceTransactions.TimeOut = Dtsndout.ToString("HH:mm")
                                                    If Dtsndout < Dtsndin Then
                                                        AttendanceTransactions.TotalHours = (Dtsndout.AddDays(1) - Dtsndin).TotalHours
                                                    Else
                                                        AttendanceTransactions.TotalHours = (Dtsndout - Dtsndin).TotalHours
                                                    End If
                                                End If

                                                AttendanceTransactions.Status = -1
                                                ClsEmployeesClassesCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                                                ClsEmployeesClassesCalender = New Clshrs_EmployeesClassCalander(Page)
                                                If ClsEmployeesClassesCalender.Find("EmployeeClassID = " & ClsEmployeeClasses.ID & " and CONVERT(date,FromTime,103) = CONVERT(date,'" & AttendanceTransactions.TrnsDatetime.ToString("dd/MM/yyyy") & "',103)") Then
                                                    If ClsEmployeesClassesCalender.nonWorkingTime Then
                                                        AttendanceTransactions.Status = 1
                                                    End If
                                                End If
                                                If (ClsEmployeesClassesCalenderSet.Find("EmployeeClassID = " & ClsEmployeeClasses.ID & " and DayNumber = " & RetDayNumber(AttendanceTransactions.TrnsDatetime)) = True) Then
                                                    If ClsEmployeesClassesCalenderSet.NonWorkingTime Then
                                                        AttendanceTransactions.Status = 1
                                                    End If
                                                End If

                                                AttendanceTransactions.Src = 1
                                                AttendanceTransactions.RegDate = DateTime.Now
                                                AttendanceTransactions.RegUserID = ClsEmployee.DataBaseUserRelatedID
                                                Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                                If AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and ProjectID = " & AttendanceTransactions.ProjectID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)") Then
                                                    AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                                Else
                                                    AttendanceTransactions.Save()
                                                End If
                                            End If
                                        End If
                                    End If
                                Catch ex As Exception
                                    Continue For
                                End Try
                            Next
                        Case FileType.Majed_CSV

                            Dim dsExcelSheet As New Data.DataSet
                            dsExcelSheet = LoadExcelSheet(strFinalPath, Companies.Majed)

                            Dim mTrnsDate As String
                            Dim mEmpMachineCode As String
                            Dim mTime As String = ""
                            Dim mTimeIn As String = ""
                            Dim mTimeOut As String = ""
                            Dim mDirection As String
                            Dim mDirectionIN As Boolean
                            Dim mDirectionOUT As Boolean
                            Dim CountTrnsDate As Integer
                            Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                            CountTrnsDate = dsExcelSheet.Tables("TrnsDate").Rows.Count - 1
                            For Idate As Integer = 0 To CountTrnsDate
                                mTrnsDate = dsExcelSheet.Tables("TrnsDate").Rows(Idate).Item("TrnsDate")

                                For Iemp As Integer = 0 To dsExcelSheet.Tables("EmpMachineCode").Select("TrnsDate='" & mTrnsDate & "'", "EmpMachineCode").Length - 1
                                    mEmpMachineCode = dsExcelSheet.Tables("EmpMachineCode").Select("TrnsDate='" & mTrnsDate & "'", "EmpMachineCode")(Iemp).Item("EmpMachineCode")

                                    If ClsEmployee.Find("MachineCode='" & mEmpMachineCode & "'") Then
                                        AttendanceTransactionsFind.Delete("EmployeeID = " & ClsEmployee.ID & " and ProjectID = " & DropDownList2.SelectedValue & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & mTrnsDate & "',103)")
                                    End If
                                    mDirectionIN = False
                                    mDirectionOUT = True
                                    mTimeIn = ""
                                    mTimeOut = ""
                                    For Itime As Integer = 0 To dsExcelSheet.Tables("Time").Select("EmpMachineCode='" & mEmpMachineCode & "' And TrnsDate='" & mTrnsDate & "'").Length - 1
                                        With dsExcelSheet.Tables("Time").Select("EmpMachineCode='" & mEmpMachineCode & "' And TrnsDate='" & mTrnsDate & "'")(Itime)
                                            mDirection = .Item("Direction").ToString.Replace("""", "")
                                            mTime = .Item("Time")
                                            If mDirection = "in" And mDirectionOUT Then
                                                mTimeIn = mTime
                                                mDirectionIN = True
                                                mDirectionOUT = False
                                            End If
                                            If mDirection = "out" And mDirectionIN Then
                                                mTimeOut = mTime
                                                SaveAttendance(mEmpMachineCode, mTrnsDate, mTimeIn, mTimeOut, 0)
                                                mDirectionIN = False
                                                mDirectionOUT = True

                                            End If
                                        End With
                                    Next
                                    If mDirectionIN Then
                                        mTimeOut = "16:00:00"
                                        SaveAttendance(mEmpMachineCode, mTrnsDate, mTimeIn, mTimeOut, 0)
                                        mDirectionIN = False
                                        mDirectionOUT = True
                                    End If
                                Next
                            Next
                            '-------------------------------------------------------------------------------------------------------------------------------------------
                        Case FileType.Elkhiriji_Excel
                            For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                                AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                If GetEmployee(Row("Number").ToString, ClsEmployee) Then
                                    Dim ProjID As String = "0"
                                    If ClsEmployee.ID <> 0 Then
                                        If DropDownList2.SelectedValue = 0 Then
                                            Try
                                                ProjID = Row("Project").ToString()
                                                clsProject.Find(" Code='" & ProjID & "'")
                                                ProjID = clsProject.ID
                                            Catch ex As Exception
                                                ProjID = ClsEmployeeClasses.DefaultProjectID
                                            End Try
                                            If ProjID = "0" Then
                                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                                                objNav.SetLanguage(Page, "Select Project/اختر المشروع"))
                                                Return False
                                            End If
                                        Else
                                            ProjID = DropDownList2.SelectedValue
                                        End If
                                        AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                        AttendanceTransactions.ProjectID = ProjID
                                        For Each Col As Data.DataColumn In Row.Table.Columns
                                            If Col.ColumnName.Contains("/") Then
                                                Dim Strarray As String() = Col.ColumnName.Split("/")
                                                Dim TrnsDate As New DateTime(Convert.ToInt32(DropDownList3.SelectedValue), Convert.ToInt32(Strarray(0)), Convert.ToInt32(Strarray(1)), 0, 0, 0, 0)
                                                AttendanceTransactions.TrnsDatetime = TrnsDate
                                                If Row(Col).ToString() = "0" Or Row(Col).ToString() = "" Then
                                                    AttendanceTransactions.TotalHours = 0
                                                Else
                                                    AttendanceTransactions.TotalHours = Row(Col)
                                                End If
                                                AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                                AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                                AttendanceTransactions.Status = -1
                                                ClsEmployeesClassesCalenderSet = New Clshrs_EmployeesClassesCalenderSet(Page)
                                                ClsEmployeesClassesCalender = New Clshrs_EmployeesClassCalander(Page)
                                                If ClsEmployeesClassesCalender.Find("EmployeeClassID = " & ClsEmployeeClasses.ID & " and CONVERT(date,FromTime,103) = CONVERT(date,'" & TrnsDate.ToString("dd/MM/yyyy") & "',103)") Then
                                                    If ClsEmployeesClassesCalender.nonWorkingTime Then
                                                        AttendanceTransactions.Status = 1
                                                    End If
                                                End If
                                                If (ClsEmployeesClassesCalenderSet.Find("EmployeeClassID = " & ClsEmployeeClasses.ID & " and DayNumber = " & RetDayNumber(TrnsDate)) = True) Then
                                                    If ClsEmployeesClassesCalenderSet.NonWorkingTime Then
                                                        AttendanceTransactions.Status = 1
                                                    End If
                                                End If
                                                If String.IsNullOrEmpty(Convert.ToString(Row(Col))) And AttendanceTransactions.Status <> 1 Then
                                                    Continue For
                                                End If
                                                AttendanceTransactions.Src = 1
                                                AttendanceTransactions.RegDate = DateTime.Now
                                                AttendanceTransactions.RegUserID = ClsEmployee.DataBaseUserRelatedID
                                                Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                                AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and ProjectID = " & AttendanceTransactions.ProjectID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                                If AttendanceTransactionsFind.ID <> 0 Then
                                                    AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                                Else
                                                    AttendanceTransactions.Save()
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        Case FileType.Carlo
                            dsResult.Tables(0).Columns("f2").ColumnName = "Name"
                            dsResult.Tables(0).Columns("f4").ColumnName = "Project"
                            dsResult.Tables(0).Columns("f3").ColumnName = "Code"

                            dsResult.Tables(0).Columns("f7").ColumnName = "1"
                            dsResult.Tables(0).Columns("f8").ColumnName = "2"
                            dsResult.Tables(0).Columns("f9").ColumnName = "3"
                            dsResult.Tables(0).Columns("f10").ColumnName = "4"
                            dsResult.Tables(0).Columns("f11").ColumnName = "5"
                            dsResult.Tables(0).Columns("f12").ColumnName = "6"
                            dsResult.Tables(0).Columns("f13").ColumnName = "7"
                            dsResult.Tables(0).Columns("f14").ColumnName = "8"
                            dsResult.Tables(0).Columns("f15").ColumnName = "9"
                            dsResult.Tables(0).Columns("f16").ColumnName = "10"

                            dsResult.Tables(0).Columns("f17").ColumnName = "11"
                            dsResult.Tables(0).Columns("f18").ColumnName = "12"
                            dsResult.Tables(0).Columns("f19").ColumnName = "13"
                            dsResult.Tables(0).Columns("f20").ColumnName = "14"
                            dsResult.Tables(0).Columns("f21").ColumnName = "15"
                            dsResult.Tables(0).Columns("f22").ColumnName = "16"
                            dsResult.Tables(0).Columns("f23").ColumnName = "17"
                            dsResult.Tables(0).Columns("f24").ColumnName = "18"
                            dsResult.Tables(0).Columns("f25").ColumnName = "19"
                            dsResult.Tables(0).Columns("f26").ColumnName = "20"
                            dsResult.Tables(0).Columns("f27").ColumnName = "21"
                            dsResult.Tables(0).Columns("f28").ColumnName = "22"
                            dsResult.Tables(0).Columns("f29").ColumnName = "23"
                            dsResult.Tables(0).Columns("f30").ColumnName = "24"
                            dsResult.Tables(0).Columns("f31").ColumnName = "25"
                            dsResult.Tables(0).Columns("f32").ColumnName = "26"
                            dsResult.Tables(0).Columns("f33").ColumnName = "27"

                            dsResult.Tables(0).Columns("f34").ColumnName = "28"
                            dsResult.Tables(0).Columns("f35").ColumnName = "29"
                            dsResult.Tables(0).Columns("f36").ColumnName = "30"
                            dsResult.Tables(0).Columns("f37").ColumnName = "31"

                            dsResult.Tables(0).Rows.RemoveAt(0)
                            dsResult.Tables(0).Rows.RemoveAt(0)
                            dsResult.Tables(0).Rows.RemoveAt(0)

                            If CheckCodeName(dsResult) = False Then
                                Return False
                            End If
                            For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                                AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                ClsEmployee.Find("MachineCode = '" & Row("Code") & "'")
                                Dim ProjID As String = "0"
                                If ClsEmployee.ID <> 0 Then
                                    Try
                                        ProjID = Row("Project").ToString()
                                        clsProject.Find(" Code='" & ProjID & "'")
                                        ProjID = clsProject.ID
                                    Catch ex As Exception
                                        ProjID = "0"
                                    End Try
                                    If DropDownList2.SelectedValue = 0 Then
                                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page,
                                            objNav.SetLanguage(Page, "Select Project/ÇÎÊÑ ÇáãÔÑæÚ"))
                                        Return False
                                    End If
                                    ProjID = IIf(ProjID = "0", DropDownList2.SelectedValue, ProjID)
                                    AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                    ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))
                                    ClsEmployeeClasses.Find("ID = " & ClsContracts.EmployeeClassID)
                                    AttendanceTransactions.ProjectID = ProjID
                                    Dim Numberss() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"}
                                    Dim LastEnteredDate As Date = Nothing
                                    For Each Col As Data.DataColumn In Row.Table.Columns
                                        If (Array.IndexOf(Numberss, Col.ColumnName) >= 0) Then
                                            Dim Strarray As String = Col.ColumnName
                                            Dim TrnsDate As DateTime
                                            Dim RowIndex As Integer = dsResult.Tables(0).Rows.IndexOf(Row)
                                            Dim OvDataRow As Data.DataRow = dsResult.Tables(0).Rows(RowIndex + 1)
                                            Try
                                                TrnsDate = New DateTime(Convert.ToInt32(DropDownList3.SelectedValue), Convert.ToInt32(DropDownListM.SelectedValue), Convert.ToInt32(Col.ColumnName), 0, 0, 0, 0)
                                                AttendanceTransactions.TrnsDatetime = TrnsDate
                                                AttendanceTransactions.TotalHours = 0
                                                AttendanceTransactions.Status = -1
                                                If Row(Col).ToString().ToLower() = "p" Or Row(Col).ToString().ToLower() = "f" Or Row(Col).ToString().ToLower() = "s" Or Row(Col).ToString() = "8" Or Row(Col).ToString() = "10" Or Row(Col).ToString().ToLower() = "t" Or Row(Col).ToString().ToLower() = "h" Then
                                                    AttendanceTransactions.Overtime = 0
                                                    LastEnteredDate = TrnsDate
                                                    AttendanceTransactions.Status = -1
                                                    If Row(Col).ToString().ToLower() = "f" Then
                                                        AttendanceTransactions.Status = 1
                                                        If OvDataRow(Col).ToString() <> "" And IsNumeric(OvDataRow(Col)) Then
                                                            Dim OV As Decimal = Convert.ToDecimal(OvDataRow(Col))
                                                            AttendanceTransactions.TotalHours = OV
                                                            AttendanceTransactions.Overtime = OV
                                                        End If
                                                    Else
                                                        AttendanceTransactions.TotalHours = (Convert.ToDateTime(ClsEmployeeClasses.DefultEndTime) - Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime)).TotalHours
                                                        If OvDataRow(Col).ToString() <> "" And IsNumeric(OvDataRow(Col)) Then
                                                            Dim OV As Decimal = Convert.ToDecimal(OvDataRow(Col))
                                                            AttendanceTransactions.TotalHours = AttendanceTransactions.TotalHours + OV
                                                            AttendanceTransactions.Overtime = OV
                                                        End If
                                                    End If
                                                End If
                                            Catch ex As Exception
                                                If OvDataRow(Col).ToString() <> "" And IsNumeric(OvDataRow(Col)) Then
                                                    If Convert.ToDecimal(OvDataRow(Col)) > 0 Then
                                                        Dim AttendanceTransactionsFind1 = New ClsAtt_AttendTransactions(Page)
                                                        AttendanceTransactionsFind1.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and ProjectID = " & AttendanceTransactions.ProjectID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & LastEnteredDate & "',103)")
                                                        If AttendanceTransactionsFind1.ID <> 0 Then
                                                            AttendanceTransactionsFind1.TotalHours = AttendanceTransactionsFind1.TotalHours + Convert.ToDecimal(OvDataRow(Col))
                                                            AttendanceTransactionsFind1.Overtime = AttendanceTransactionsFind1.Overtime + Convert.ToDecimal(OvDataRow(Col))
                                                            AttendanceTransactionsFind1.Update("ID =" & AttendanceTransactionsFind1.ID)
                                                        End If
                                                    End If
                                                End If
                                                Continue For
                                            End Try

                                            If AttendanceTransactions.TotalHours = 0 Then
                                                If OvDataRow(Col).ToString() <> "" And IsNumeric(OvDataRow(Col)) Then
                                                    If Convert.ToDecimal(OvDataRow(Col)) > 0 Then
                                                        Dim AttendanceTransactionsFind1 = New ClsAtt_AttendTransactions(Page)
                                                        AttendanceTransactionsFind1.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and ProjectID = " & AttendanceTransactions.ProjectID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & LastEnteredDate & "',103)")
                                                        If AttendanceTransactionsFind1.ID <> 0 Then
                                                            AttendanceTransactionsFind1.TotalHours = AttendanceTransactionsFind1.TotalHours + Convert.ToDecimal(OvDataRow(Col))
                                                            AttendanceTransactionsFind1.Overtime = AttendanceTransactionsFind1.Overtime + Convert.ToDecimal(OvDataRow(Col))
                                                            AttendanceTransactionsFind1.Update("ID =" & AttendanceTransactionsFind1.ID)
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            If Row(Col).ToString().ToLower() = "" Then
                                                Continue For
                                            End If
                                            If LastEnteredDate = Nothing Then
                                                Continue For
                                            End If
                                            AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                            AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                            AttendanceTransactions.TrnsType = 0
                                            AttendanceTransactions.Src = 1
                                            Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                            AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and ProjectID = " & AttendanceTransactions.ProjectID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                            If AttendanceTransactionsFind.ID <> 0 Then
                                                AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                            Else
                                                AttendanceTransactions.Save()
                                            End If
                                        End If
                                    Next
                                End If
                            Next
                            Try
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Done/تمت العملية بنجاح "))
                            Catch ex As Exception
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Failled/فشلت العملية"))
                            End Try
                        Case FileType.ExcelTotal
                            Get_FromToDate(New Date(DropDownList3.SelectedValue, DropDownListM.SelectedValue, 1))

                            dsResult.Tables(0).Columns(1).ColumnName = "Code"
                            dsResult.Tables(0).Columns(2).ColumnName = "Name"
                            dsResult.Tables(0).Columns(3).ColumnName = "Delay"
                            dsResult.Tables(0).Columns(4).ColumnName = "OT"


                            dsResult.Tables(0).Columns(5).ColumnName = "Unpaid"
                            dsResult.Tables(0).Columns(6).ColumnName = "Paid"
                            dsResult.Tables(0).Columns(7).ColumnName = "Sick"
                            dsResult.Tables(0).Columns(8).ColumnName = "ExcuseAbsent"
                            dsResult.Tables(0).Columns(9).ColumnName = "Absent"
                            dsResult.Tables(0).Columns(10).ColumnName = "WDays"
                            dsResult.Tables(0).Columns(11).ColumnName = "Total"
                            dsResult.Tables(0).Columns(12).ColumnName = "StartFrom"
                            dsResult.Tables(0).Columns(13).ColumnName = "Project"
                            If CheckEmpCode(dsResult) = False Then
                                Return False
                            End If
                            For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                                ClsEmployee.Find("Code = '" & Row("Code") & "'")
                                Dim ProjID As String = "0"
                                If ClsEmployee.ID <> 0 Then
                                    Try
                                        ProjID = Row("Project").ToString()
                                        clsProject.Find(" Code='" & ProjID & "'")
                                        ProjID = clsProject.ID
                                    Catch ex As Exception
                                        ProjID = "0"
                                    End Try
                                    'If DropDownList2.SelectedValue = 0 And ProjID = 0 Then
                                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, _
                                    '        objNav.SetLanguage(Page, "Select Project/ÇÎÊÑ ÇáãÔÑæÚ"))
                                    '    Return False
                                    'End If
                                    ProjID = IIf(ProjID = "0", DropDownList2.SelectedValue, ProjID)

                                    'ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))

                                    'Edited By: Hassan Kuedi
                                    'Date: 2022-01-12
                                    'Purpose: Get Contract by date
                                    ClsContracts.Find("ID = " & ClsContracts.GetContractIDDate(ClsEmployee.ID, Row("StartFrom").ToString()))
                                    If ClsContracts.ID <= 0 Then
                                        ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))
                                    End If
                                    'End

                                    ClsEmployeeClasses.Find("ID = " & ClsContracts.EmployeeClassID)

                                    Dim Delays As Decimal = Row(3)
                                    Dim TOT As Decimal = Row(4)
                                    Dim cndupVacations As Integer = Row(5)
                                    Dim cndpVacations As Integer = Row(6)
                                    Dim cndSVacations As Integer = Row(7)
                                    Dim cndAbVacations As Integer = Row(8) + Row(9)
                                    Dim cndwdays As Integer = Row(10)



                                    Dim Datestr As String = Row("StartFrom")
                                    Dim commandString = ""
                                    If ClsDataAcessLayer.IsHijri(Row("StartFrom")) Then
                                        Datestr = ClsDataAcessLayer.HijriToGreg(Row("StartFrom"), "dd/MM/yyyy")
                                    ElseIf ClsDataAcessLayer.IsGreg(Row("StartFrom")) Then
                                        Datestr = Row("StartFrom")
                                    End If
                                    Dim basedate As Date = Convert.ToDateTime(Datestr)
                                    Dim strscript As String = ""
                                    Dim days As Integer = Row("Total")

                                    'If Row("Total") = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 Then
                                    '    days = 31

                                    'End If

                                    While basedate < Convert.ToDateTime(Datestr).AddDays(Row("Total"))
                                        'While basedate < Convert.ToDateTime(Datestr).AddDays(days)
                                        AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                        AttendanceTransactions.ProjectID = ProjID

                                        If ClsContracts.StartDate > basedate Then
                                            basedate = basedate.AddDays(1)
                                            Continue While
                                        End If
                                        If ClsContracts.EndDate <> Nothing And ClsContracts.EndDate < basedate Then
                                            basedate = basedate.AddDays(1)
                                            Continue While
                                        End If
                                        AttendanceTransactions.TrnsDatetime = basedate
                                        AttendanceTransactions.TotalHours = 0
                                        AttendanceTransactions.Status = -1
                                        Dim hrsemployeevacations As New Clshrs_EmployeesVacations(Me.Page)
                                        Dim hrsvacationstypes As New Clshrs_VacationsTypes(Me.Page)

                                        If cndpVacations > 0 Then
                                            If hrsvacationstypes.Find("IsPaid = 0 and isnull(IsSickVacation,0) = 0") Then
                                                AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndpVacations = cndpVacations - 1
                                                Else
                                                    'strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndpVacations = cndpVacations - 1
                                                End If
                                            Else
                                                cndpVacations = cndpVacations - 1
                                            End If
                                        ElseIf cndupVacations > 0 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If hrsvacationstypes.Find("IsPaid = -1") Then
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndupVacations = cndupVacations - 1
                                                Else
                                                    '   strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndupVacations = cndupVacations - 1
                                                End If
                                            Else
                                                cndupVacations = cndupVacations - 1
                                            End If

                                        ElseIf cndSVacations > 0 Then
                                            If hrsvacationstypes.Find("isnull(IsSickVacation,0) = 1") Then
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndSVacations = cndSVacations - 1
                                                Else
                                                    ' strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndSVacations = cndSVacations - 1
                                                End If
                                            Else
                                                cndSVacations = cndSVacations - 1
                                            End If
                                            'Edited By: Hassan Kuedi
                                            'Date: 2022-01-26
                                            'Purpose: Add a vacation check to the condition
                                        ElseIf cndAbVacations > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                            cndAbVacations = cndAbVacations - 1
                                        ElseIf cndwdays > 0 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If Delays > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                AttendanceTransactions.TotalLate = Delays / 60
                                                Delays = 0
                                            End If
                                            If TOT > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                AttendanceTransactions.Overtime = TOT
                                                TOT = 0
                                            End If
                                            'End
                                            cndwdays = cndwdays - 1

                                        ElseIf Row(10) = 30 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If Delays > 0 Then
                                                AttendanceTransactions.TotalLate = Delays / 60
                                                Delays = 0
                                            End If
                                            If TOT > 0 Then
                                                AttendanceTransactions.Overtime = TOT
                                                TOT = 0
                                            End If
                                            'cndwdays = cndwdays - 1
                                        End If

                                        AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                        AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                        AttendanceTransactions.TrnsType = 0
                                        AttendanceTransactions.Src = 1
                                        Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                        If AttendanceTransactionsFind.ID <> 0 Then
                                            AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                        Else
                                            AttendanceTransactions.Save()
                                        End If
                                        basedate = basedate.AddDays(1)
                                    End While

                                    Dim TotalDays As Integer
                                    TotalDays = dsResult.Tables(0).Compute("sum(Total)", "Code =" & Row("code"))
                                    If TotalDays = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 Then
                                        AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                        AttendanceTransactions.TrnsDatetime = basedate
                                        AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                        AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                        AttendanceTransactions.TrnsType = 0
                                        AttendanceTransactions.Src = 1
                                        Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                        If AttendanceTransactionsFind.ID <> 0 Then
                                            AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                        Else
                                            AttendanceTransactions.Save()
                                        End If

                                    End If
                                    If strscript <> "" Then
                                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(AttendanceTransactions.ConnectionString, System.Data.CommandType.Text, strscript)
                                    End If
                                End If
                            Next
                            Try
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Done/تمت العملية بنجاح "))
                            Catch ex As Exception
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Failled/فشلت العملية"))
                            End Try
                            'Get_FromToDate(New Date(DropDownList3.SelectedValue, DropDownListM.SelectedValue, 1))

                            'dsResult.Tables(0).Columns(1).ColumnName = "Code"
                            'dsResult.Tables(0).Columns(2).ColumnName = "Name"
                            'dsResult.Tables(0).Columns(3).ColumnName = "Delay"
                            'dsResult.Tables(0).Columns(4).ColumnName = "OT"


                            'dsResult.Tables(0).Columns(5).ColumnName = "Unpaid"
                            'dsResult.Tables(0).Columns(6).ColumnName = "Paid"
                            'dsResult.Tables(0).Columns(7).ColumnName = "Sick"
                            'dsResult.Tables(0).Columns(8).ColumnName = "ExcuseAbsent"
                            'dsResult.Tables(0).Columns(9).ColumnName = "Absent"
                            'dsResult.Tables(0).Columns(10).ColumnName = "WDays"
                            'dsResult.Tables(0).Columns(11).ColumnName = "Total"
                            'dsResult.Tables(0).Columns(12).ColumnName = "Month"
                            'dsResult.Tables(0).Columns(13).ColumnName = "Year"
                            'dsResult.Tables(0).Columns(14).ColumnName = "Project"
                            'If CheckEmpCode(dsResult) = False Then
                            '    Return False
                            'End If

                            'Dim EmpCode As String = ""
                            'Dim LastEntryDate As DateTime
                            'Dim SameEmployee As Boolean = False
                            'For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                            '    If EmpCode = Row("Code").ToString() Then
                            '        SameEmployee = True
                            '    End If

                            '    ClsEmployee.Find("Code = '" & Row("Code") & "'")
                            '    Dim ProjID As String = "0"
                            '    If ClsEmployee.ID <> 0 Then
                            '        Try
                            '            ProjID = Row("Project").ToString()
                            '            clsProject.Find(" Code='" & ProjID & "'")
                            '            ProjID = clsProject.ID
                            '        Catch ex As Exception
                            '            ProjID = "0"
                            '        End Try



                            '        Dim PerparDay As Integer = clsCompanies.PrepareDay
                            '        Dim StartMonth As Integer = DropDownListM.SelectedItem.Text
                            '        Dim StartYear As Integer = DropDownList3.SelectedItem.Text
                            '        If StartMonth = 1 Then
                            '            StartMonth = 12
                            '            StartYear = StartYear - 1
                            '        Else
                            '            StartMonth = StartMonth - 1
                            '        End If
                            '        Dim StartDate As String = PerparDay & "/" & StartMonth & "/" & StartYear
                            '        If SameEmployee Then
                            '            StartDate = LastEntryDate.ToString()
                            '        End If
                            '        'If DropDownList2.SelectedValue = 0 And ProjID = 0 Then
                            '        '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, _
                            '        '        objNav.SetLanguage(Page, "Select Project/ÇÎÊÑ ÇáãÔÑæÚ"))
                            '        '    Return False
                            '        'End If
                            '        ProjID = IIf(ProjID = "0", DropDownList2.SelectedValue, ProjID)

                            '            'ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))

                            '            'Edited By: Hassan Kuedi
                            '            'Date: 2022-01-12
                            '            'Purpose: Get Contract by date
                            '            'Rabie 12-11-2023 Remove StartDate From ExcelSheet To be only Month and Year
                            '            'ClsContracts.Find("ID = " & ClsContracts.GetContractIDDate(ClsEmployee.ID, Row("StartFrom").ToString()))
                            '            ClsContracts.Find("ID = " & ClsContracts.GetContractIDDate(ClsEmployee.ID, StartDate.ToString()))
                            '            If ClsContracts.ID <= 0 Then
                            '                ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))
                            '            End If
                            '            'End

                            '            ClsEmployeeClasses.Find("ID = " & ClsContracts.EmployeeClassID)

                            '            Dim Delays As Decimal = Row(3)
                            '            Dim TOT As Decimal = Row(4)
                            '            Dim cndupVacations As Integer = Row(5)
                            '            Dim cndpVacations As Integer = Row(6)
                            '            Dim cndSVacations As Integer = Row(7)
                            '            Dim cndAbVacations As Integer = Row(8) + Row(9)
                            '            Dim cndwdays As Integer = Row(10)
                            '            Dim Datestr As String = StartDate
                            '            'Dim Datestr As String = Row("StartFrom")
                            '            Dim commandString = ""
                            '            If ClsDataAcessLayer.IsHijri(StartDate) Then
                            '                Datestr = ClsDataAcessLayer.HijriToGreg(StartDate, "dd/MM/yyyy")
                            '            ElseIf ClsDataAcessLayer.IsGreg(StartDate) Then
                            '                Datestr = StartDate
                            '            End If
                            '            Dim basedate As Date = Convert.ToDateTime(Datestr)
                            '            Dim strscript As String = ""
                            '            Dim days As Integer = Row("Total")

                            '            'If Row("Total") = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 Then
                            '            '    days = 31

                            '            'End If

                            '            While basedate < Convert.ToDateTime(Datestr).AddDays(Row("Total"))
                            '                'While basedate < Convert.ToDateTime(Datestr).AddDays(days)
                            '                AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                            '                AttendanceTransactions.EmployeeID = ClsEmployee.ID
                            '                AttendanceTransactions.ProjectID = ProjID

                            '                If ClsContracts.StartDate > basedate Then
                            '                    basedate = basedate.AddDays(1)
                            '                    Continue While
                            '                End If
                            '                If ClsContracts.EndDate <> Nothing And ClsContracts.EndDate < basedate Then
                            '                    basedate = basedate.AddDays(1)
                            '                    Continue While
                            '                End If
                            '                AttendanceTransactions.TrnsDatetime = basedate
                            '                AttendanceTransactions.TotalHours = 0
                            '                AttendanceTransactions.Status = -1
                            '                Dim hrsemployeevacations As New Clshrs_EmployeesVacations(Me.Page)
                            '                Dim hrsvacationstypes As New Clshrs_VacationsTypes(Me.Page)

                            '                If cndpVacations > 0 Then
                            '                    If hrsvacationstypes.Find("IsPaid = 0 and isnull(IsSickVacation,0) = 0 Then") Then
                            '                        AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                            '                        If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " And VacationTypeID = " & hrsvacationstypes.ID & " And ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                            '                            cndpVacations = cndpVacations - 1
                            '                        Else
                            '                            'strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                            '                            cndpVacations = cndpVacations - 1
                            '                        End If
                            '                    Else
                            '                        cndpVacations = cndpVacations - 1
                            '                    End If
                            '                ElseIf cndupVacations > 0 Then
                            '                    AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                            '                    If hrsvacationstypes.Find("IsPaid = -1") Then
                            '                        If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                            '                            cndupVacations = cndupVacations - 1
                            '                        Else
                            '                            '   strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                            '                            cndupVacations = cndupVacations - 1
                            '                        End If
                            '                    Else
                            '                        cndupVacations = cndupVacations - 1
                            '                    End If

                            '                ElseIf cndSVacations > 0 Then
                            '                    If hrsvacationstypes.Find("isnull(IsSickVacation,0) = 1") Then
                            '                        If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                            '                            cndSVacations = cndSVacations - 1
                            '                        Else
                            '                            ' strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                            '                            cndSVacations = cndSVacations - 1
                            '                        End If
                            '                    Else
                            '                        cndSVacations = cndSVacations - 1
                            '                    End If
                            '                    'Edited By: Hassan Kuedi
                            '                    'Date: 2022-01-26
                            '                    'Purpose: Add a vacation check to the condition
                            '                ElseIf cndAbVacations > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                            '                    cndAbVacations = cndAbVacations - 1
                            '                ElseIf cndwdays > 0 Then
                            '                    AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                            '                    If Delays > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                            '                        AttendanceTransactions.TotalLate = Delays / 60
                            '                        Delays = 0
                            '                    End If
                            '                    If TOT > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                            '                        AttendanceTransactions.Overtime = TOT
                            '                        TOT = 0
                            '                    End If
                            '                    'End
                            '                    cndwdays = cndwdays - 1

                            '                ElseIf Row(10) = 30 Then
                            '                    AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                            '                    If Delays > 0 Then
                            '                        AttendanceTransactions.TotalLate = Delays / 60
                            '                        Delays = 0
                            '                    End If
                            '                    If TOT > 0 Then
                            '                        AttendanceTransactions.Overtime = TOT
                            '                        TOT = 0
                            '                    End If
                            '                    'cndwdays = cndwdays - 1
                            '                End If

                            '                AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                            '                AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                            '                AttendanceTransactions.TrnsType = 0
                            '                AttendanceTransactions.Src = 1
                            '                Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                            '                AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                            '                If AttendanceTransactionsFind.ID <> 0 Then
                            '                    AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                            '                Else
                            '                    AttendanceTransactions.Save()
                            '                End If
                            '                basedate = basedate.AddDays(1)
                            '            End While

                            '            Dim TotalDays As Integer
                            '            TotalDays = dsResult.Tables(0).Compute("sum(Total)", "Code =" & Row("code"))
                            '        If TotalDays = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 Then
                            '            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                            '            AttendanceTransactions.TrnsDatetime = basedate
                            '            AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                            '            AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                            '            AttendanceTransactions.TrnsType = 0
                            '            AttendanceTransactions.Src = 1
                            '            Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                            '            AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                            '            If AttendanceTransactionsFind.ID <> 0 Then
                            '                AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                            '            Else
                            '                AttendanceTransactions.Save()
                            '            End If

                            '        End If
                            '        If strscript <> "" Then
                            '            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(AttendanceTransactions.ConnectionString, System.Data.CommandType.Text, strscript)
                            '        End If
                            '        EmpCode = Row("Code").ToString()
                            '        LastEntryDate = basedate
                            '    End If

                            'Next
                            'Try
                            '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Done/تمت العملية بنجاح "))
                            'Catch ex As Exception
                            '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Failled/فشلت العملية"))
                            'End Try

                        Case FileType.ExcelKenaaz
                            Get_FromToDate(New Date(DropDownList3.SelectedValue, DropDownListM.SelectedValue, 1))
                            dsResult.Tables(0).Columns(1).ColumnName = "Iqama"
                            dsResult.Tables(0).Columns(2).ColumnName = "Code"
                            dsResult.Tables(0).Columns(3).ColumnName = "Name"
                            dsResult.Tables(0).Columns(4).ColumnName = "Delay"
                            dsResult.Tables(0).Columns(5).ColumnName = "OT"


                            dsResult.Tables(0).Columns(6).ColumnName = "Unpaid"
                            dsResult.Tables(0).Columns(7).ColumnName = "Paid"
                            dsResult.Tables(0).Columns(8).ColumnName = "Sick"
                            dsResult.Tables(0).Columns(9).ColumnName = "ExcuseAbsent"
                            dsResult.Tables(0).Columns(10).ColumnName = "Absent"
                            dsResult.Tables(0).Columns(11).ColumnName = "WDays"
                            dsResult.Tables(0).Columns(12).ColumnName = "Total"
                            dsResult.Tables(0).Columns(13).ColumnName = "StartFrom"
                            dsResult.Tables(0).Columns(14).ColumnName = "Project"
                            If CheckEmpCodeandIqama(dsResult) = False Then
                                Return False
                            End If

                            If CheckProject(dsResult) = False Then
                                Return False
                            End If

                            For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                                ClsEmployee.Find("Code = '" & Row("Code") & "'")
                                Dim ProjID As String = "0"
                                If ClsEmployee.ID <> 0 Then
                                    Try
                                        ProjID = Row("Project").ToString()
                                        clsProject.Find(" Code='" & ProjID & "'")
                                        ProjID = clsProject.ID
                                    Catch ex As Exception
                                        ProjID = "0"
                                    End Try
                                    'If DropDownList2.SelectedValue = 0 And ProjID = 0 Then
                                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, _
                                    '        objNav.SetLanguage(Page, "Select Project/ÇÎÊÑ ÇáãÔÑæÚ"))
                                    '    Return False
                                    'End If
                                    ProjID = IIf(ProjID = "0", DropDownList2.SelectedValue, ProjID)

                                    ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))
                                    ClsEmployeeClasses.Find("ID = " & ClsContracts.EmployeeClassID)

                                    Dim Delays As Decimal = Row(4)
                                    Dim TOT As Decimal = Row(5)
                                    Dim cndupVacations As Integer = Row(6)
                                    Dim cndpVacations As Integer = Row(7)
                                    Dim cndSVacations As Integer = Row(8)
                                    Dim cndAbVacations As Integer = Row(9) + Row(10)
                                    Dim cndwdays As Integer = Row(11)



                                    Dim Datestr As String = Row("StartFrom")
                                    Dim commandString = ""
                                    If ClsDataAcessLayer.IsHijri(Row("StartFrom")) Then
                                        Datestr = ClsDataAcessLayer.HijriToGreg(Row("StartFrom"), "dd/MM/yyyy")
                                    ElseIf ClsDataAcessLayer.IsGreg(Row("StartFrom")) Then
                                        Datestr = Row("StartFrom")
                                    End If
                                    Dim basedate As Date = Convert.ToDateTime(Datestr)
                                    Dim strscript As String = ""
                                    Dim days As Integer = Row("Total")


                                    While basedate < Convert.ToDateTime(Datestr).AddDays(Row("Total"))
                                        'While basedate < Convert.ToDateTime(Datestr).AddDays(days)
                                        AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                        AttendanceTransactions.ProjectID = ProjID

                                        If ClsContracts.StartDate > basedate Then
                                            basedate = basedate.AddDays(1)
                                            Continue While
                                        End If
                                        If ClsContracts.EndDate <> Nothing And ClsContracts.EndDate < basedate Then
                                            basedate = basedate.AddDays(1)
                                            Continue While
                                        End If
                                        AttendanceTransactions.TrnsDatetime = basedate
                                        AttendanceTransactions.TotalHours = 0
                                        AttendanceTransactions.Status = -1
                                        Dim hrsemployeevacations As New Clshrs_EmployeesVacations(Me.Page)
                                        Dim hrsvacationstypes As New Clshrs_VacationsTypes(Me.Page)

                                        If cndpVacations > 0 Then
                                            If hrsvacationstypes.Find("IsPaid = 0 and isnull(IsSickVacation,0) = 0") Then
                                                AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndpVacations = cndpVacations - 1
                                                Else
                                                    'strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndpVacations = cndpVacations - 1
                                                End If
                                            Else
                                                cndpVacations = cndpVacations - 1
                                            End If
                                        ElseIf cndupVacations > 0 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If hrsvacationstypes.Find("IsPaid = -1") Then
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndupVacations = cndupVacations - 1
                                                Else
                                                    '   strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndupVacations = cndupVacations - 1
                                                End If
                                            Else
                                                cndupVacations = cndupVacations - 1
                                            End If

                                        ElseIf cndSVacations > 0 Then
                                            If hrsvacationstypes.Find("isnull(IsSickVacation,0) = 1") Then
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndSVacations = cndSVacations - 1
                                                Else
                                                    ' strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndSVacations = cndSVacations - 1
                                                End If
                                            Else
                                                cndSVacations = cndSVacations - 1
                                            End If
                                        ElseIf cndAbVacations > 0 Then
                                            cndAbVacations = cndAbVacations - 1
                                        ElseIf cndwdays > 0 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If Delays > 0 Then
                                                AttendanceTransactions.TotalLate = Delays / 60
                                                Delays = 0
                                            End If
                                            If TOT > 0 Then
                                                AttendanceTransactions.Overtime = TOT
                                                TOT = 0
                                            End If
                                            cndwdays = cndwdays - 1

                                            'ElseIf Row(10) = 30 Then
                                            '    AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            '    If Delays > 0 Then
                                            '        AttendanceTransactions.TotalLate = Delays / 60
                                            '        Delays = 0
                                            '    End If
                                            '    If TOT > 0 Then
                                            '        AttendanceTransactions.Overtime = TOT
                                            '        TOT = 0
                                            '    End If
                                            'cndwdays = cndwdays - 1
                                        End If

                                        AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                        AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                        AttendanceTransactions.TrnsType = 0
                                        AttendanceTransactions.Src = 1
                                        Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                        If AttendanceTransactionsFind.ID <> 0 Then
                                            AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                        Else
                                            AttendanceTransactions.Save()
                                        End If
                                        basedate = basedate.AddDays(1)
                                    End While

                                    Dim TotalDays As Integer = dsResult.Tables(0).Compute("sum(Total)", "Code =" & Row("code"))
                                    If TotalDays = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 Then

                                        AttendanceTransactions.TrnsDatetime = basedate
                                        AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                        AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                        AttendanceTransactions.TrnsType = 0
                                        AttendanceTransactions.Src = 1
                                        Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                        If AttendanceTransactionsFind.ID <> 0 Then
                                            AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                        Else
                                            AttendanceTransactions.Save()
                                        End If

                                    End If
                                    If strscript <> "" Then
                                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(AttendanceTransactions.ConnectionString, System.Data.CommandType.Text, strscript)
                                    End If
                                End If
                            Next
                            Try
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Done/تمت العملية بنجاح"))
                            Catch ex As Exception
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Failled/فشلت العملية"))
                            End Try
                        Case FileType.ExcelTadawi
                            Dim ReArrangeDs As New Data.DataSet
                            ReArrangeDs.Tables(0).Columns.Add("MachineCode", GetType(String))
                            ReArrangeDs.Tables(0).Columns.Add("Time", GetType(DateTime))
                            Dim AttendanceDate As Date
                            Dim Hours As Integer
                            Dim minutes As Integer
                            Dim second As Integer
                            Dim time As Date
                            For Each row In dsResult.Tables(0).Rows
                                If row("Time").ToString().Contains("/") Then
                                    AttendanceDate = New Date(DropDownList3.SelectedValue, row("Time").ToString().Split("/")(0), row("Time").ToString().Split("/")(1))
                                    Continue For
                                End If
                                Hours = row("Time").ToString().Split(":")(0)
                                minutes = row("Time").ToString().Split(":")(1)
                                second = row("Time").ToString().Split(":")(2)
                                time = New Date(AttendanceDate.Year, AttendanceDate.Month, AttendanceDate.Day, Hours, minutes, second)
                                ReArrangeDs.Tables(0).Rows.Add(row("Num").ToString(), time)
                            Next
                            Dim View As New Data.DataView(dsResult.Tables(0))
                            Dim distinctValues As Data.DataTable = View.ToTable(True, "Num")
                            For Each row In dsResult.Tables(0).Select("Time like '%/%'")

                            Next
                        Case FileType.ExcelByProject
                            Get_FromToDate(New Date(DropDownList3.SelectedValue, DropDownListM.SelectedValue, 1))

                            dsResult.Tables(0).Columns(1).ColumnName = "Code"
                            dsResult.Tables(0).Columns(2).ColumnName = "Name"
                            dsResult.Tables(0).Columns(3).ColumnName = "Delay"
                            dsResult.Tables(0).Columns(4).ColumnName = "OT"


                            dsResult.Tables(0).Columns(5).ColumnName = "Unpaid"
                            dsResult.Tables(0).Columns(6).ColumnName = "Paid"
                            dsResult.Tables(0).Columns(7).ColumnName = "Sick"
                            dsResult.Tables(0).Columns(8).ColumnName = "ExcuseAbsent"
                            dsResult.Tables(0).Columns(9).ColumnName = "Absent"
                            dsResult.Tables(0).Columns(10).ColumnName = "WDays"
                            dsResult.Tables(0).Columns(11).ColumnName = "Total"
                            dsResult.Tables(0).Columns(12).ColumnName = "Month"
                            dsResult.Tables(0).Columns(13).ColumnName = "Year"
                            dsResult.Tables(0).Columns(14).ColumnName = "Project"
                            If CheckEmpCode(dsResult) = False Then
                                Return False
                            End If

                            Dim EmpCode As String = ""
                            Dim LastEntryDate As DateTime
                            Dim SameEmployee As Boolean = False
                            For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                                If EmpCode = Row("Code").ToString() Then
                                    SameEmployee = True
                                Else
                                    SameEmployee = False
                                End If

                                ClsEmployee.Find("Code = '" & Row("Code") & "'")
                                Dim ProjID As String = "0"
                                If ClsEmployee.ID <> 0 Then
                                    Try
                                        ProjID = Row("Project").ToString()
                                        clsProject.Find(" Code='" & ProjID & "'")
                                        ProjID = clsProject.ID
                                    Catch ex As Exception
                                        ProjID = "0"
                                    End Try


                                    If ProjID = 0 Then
                                        ProjID = 1
                                    End If
                                    Dim PerparDay As Integer = clsCompanies.PrepareDay
                                    If PerparDay = 0 Then
                                        PerparDay = 1
                                    End If
                                    Dim StartMonth As Integer = DropDownListM.SelectedItem.Text
                                    Dim StartYear As Integer = DropDownList3.SelectedItem.Text
                                    If StartMonth = 1 And PerparDay <> 1 Then
                                        StartMonth = 12
                                        StartYear = StartYear - 1
                                    Else
                                        If PerparDay <> 1 Then
                                            StartMonth = StartMonth - 1
                                        End If

                                    End If
                                    Dim StartDate As String = PerparDay & "/" & StartMonth & "/" & StartYear
                                    If SameEmployee Then
                                        StartDate = LastEntryDate.ToString()
                                    End If
                                    'If DropDownList2.SelectedValue = 0 And ProjID = 0 Then
                                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, _
                                    '        objNav.SetLanguage(Page, "Select Project/ÇÎÊÑ ÇáãÔÑæÚ"))
                                    '    Return False
                                    'End If
                                    ProjID = IIf(ProjID = "0", DropDownList2.SelectedValue, ProjID)

                                    'ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))

                                    'Edited By: Hassan Kuedi
                                    'Date: 2022-01-12
                                    'Purpose: Get Contract by date
                                    'Rabie 12-11-2023 Remove StartDate From ExcelSheet To be only Month and Year
                                    'ClsContracts.Find("ID = " & ClsContracts.GetContractIDDate(ClsEmployee.ID, Row("StartFrom").ToString()))
                                    ClsContracts.Find("ID = " & ClsContracts.GetContractIDDate(ClsEmployee.ID, StartDate.ToString()))
                                    If ClsContracts.ID <= 0 Then
                                        ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))
                                    End If
                                    'End

                                    ClsEmployeeClasses.Find("ID = " & ClsContracts.EmployeeClassID)

                                    Dim Delays As Decimal = Row(3)
                                    Dim TOT As Decimal = Row(4)
                                    Dim cndupVacations As Integer = Row(5)
                                    Dim cndpVacations As Integer = Row(6)
                                    Dim cndSVacations As Integer = Row(7)
                                    Dim cndAbVacations As Integer = Row(8) + Row(9)
                                    Dim cndwdays As Integer = Row(10)
                                    Dim Datestr As String = StartDate
                                    'Dim Datestr As String = Row("StartFrom")
                                    Dim commandString = ""
                                    If ClsDataAcessLayer.IsHijri(StartDate) Then
                                        Datestr = ClsDataAcessLayer.HijriToGreg(StartDate, "dd/MM/yyyy")
                                    ElseIf ClsDataAcessLayer.IsGreg(StartDate) Then
                                        Datestr = StartDate
                                    End If
                                    Dim basedate As Date = Convert.ToDateTime(Datestr)
                                    Dim strscript As String = ""
                                    Dim days As Integer = Row("Total")

                                    'If Row("Total") = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 Then
                                    '    days = 31

                                    'End If

                                    While basedate < Convert.ToDateTime(Datestr).AddDays(Row("Total"))
                                        'While basedate < Convert.ToDateTime(Datestr).AddDays(days)
                                        AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                        If ProjID = 0 Then
                                            ProjID = 1
                                        End If
                                        AttendanceTransactions.ProjectID = ProjID

                                        If ClsContracts.StartDate > basedate Then
                                            basedate = basedate.AddDays(1)
                                            Continue While
                                        End If
                                        If ClsContracts.EndDate <> Nothing And ClsContracts.EndDate < basedate Then
                                            basedate = basedate.AddDays(1)
                                            Continue While
                                        End If
                                        AttendanceTransactions.TrnsDatetime = basedate
                                        AttendanceTransactions.TotalHours = 0
                                        AttendanceTransactions.Status = -1
                                        Dim hrsemployeevacations As New Clshrs_EmployeesVacations(Me.Page)
                                        Dim hrsvacationstypes As New Clshrs_VacationsTypes(Me.Page)

                                        If cndpVacations > 0 Then
                                            If hrsvacationstypes.Find("IsPaid = 0 and isnull(IsSickVacation,0) = 0") Then
                                                AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " And VacationTypeID = " & hrsvacationstypes.ID & " And ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndpVacations = cndpVacations - 1
                                                Else
                                                    'strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndpVacations = cndpVacations - 1
                                                End If
                                            Else
                                                cndpVacations = cndpVacations - 1
                                            End If
                                        ElseIf cndupVacations > 0 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If hrsvacationstypes.Find("IsPaid = -1") Then
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndupVacations = cndupVacations - 1
                                                Else
                                                    '   strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndupVacations = cndupVacations - 1
                                                End If
                                            Else
                                                cndupVacations = cndupVacations - 1
                                            End If

                                        ElseIf cndSVacations > 0 Then
                                            If hrsvacationstypes.Find("isnull(IsSickVacation,0) = 1") Then
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndSVacations = cndSVacations - 1
                                                Else
                                                    ' strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndSVacations = cndSVacations - 1
                                                End If
                                            Else
                                                cndSVacations = cndSVacations - 1
                                            End If
                                            'Edited By: Hassan Kuedi
                                            'Date: 2022-01-26
                                            'Purpose: Add a vacation check to the condition
                                        ElseIf cndAbVacations > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                            cndAbVacations = cndAbVacations - 1
                                        ElseIf cndwdays > 0 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If Delays > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                AttendanceTransactions.TotalLate = Delays / 60
                                                Delays = 0
                                            End If
                                            If TOT > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                AttendanceTransactions.Overtime = TOT
                                                TOT = 0
                                            Else
                                                AttendanceTransactions.Overtime = 0

                                            End If
                                            'End
                                            cndwdays = cndwdays - 1

                                        ElseIf Row(10) = 30 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If Delays > 0 Then
                                                AttendanceTransactions.TotalLate = Delays / 60
                                                Delays = 0
                                            End If
                                            If TOT > 0 Then
                                                AttendanceTransactions.Overtime = TOT
                                                TOT = 0
                                            End If
                                            'cndwdays = cndwdays - 1
                                        End If

                                        AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                        AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                        AttendanceTransactions.TrnsType = 0
                                        AttendanceTransactions.Src = 1
                                        Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                        If AttendanceTransactionsFind.ID <> 0 Then
                                            AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                        Else
                                            AttendanceTransactions.Save()
                                        End If
                                        AttendanceTransactions.Overtime = 0

                                        basedate = basedate.AddDays(1)
                                    End While

                                    Dim TotalDays As Integer
                                    TotalDays = dsResult.Tables(0).Compute("sum(Total)", "Code='" & Row("Code") & "'")


                                    Dim TotalCount As Integer = dsResult.Tables(0).Compute("Count(Total)", "Code ='" & Row("Code") & "'")
                                    Dim relatedRows() As DataRow = dsResult.Tables(0).Select("Code = '" & Row("code") & "'")

                                    Dim currentIndex As Integer = Array.IndexOf(relatedRows, Row) + 1

                                    If TotalDays = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 And currentIndex = TotalCount Then
                                        AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                        AttendanceTransactions.TrnsDatetime = basedate
                                        AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                        AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                        AttendanceTransactions.TrnsType = 0
                                        AttendanceTransactions.Src = 1
                                        AttendanceTransactions.Overtime = 0
                                        Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                        If AttendanceTransactionsFind.ID <> 0 Then
                                            AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                        Else
                                            AttendanceTransactions.Save()
                                        End If

                                    End If
                                    If strscript <> "" Then
                                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(AttendanceTransactions.ConnectionString, System.Data.CommandType.Text, strscript)
                                    End If
                                    EmpCode = Row("Code").ToString()
                                    LastEntryDate = basedate
                                End If

                            Next

                        Case FileType.ExcelByProjectandSSnNo
                            Get_FromToDate(New Date(DropDownList3.SelectedValue, DropDownListM.SelectedValue, 1))

                            dsResult.Tables(0).Columns(1).ColumnName = "SSnNo"
                            dsResult.Tables(0).Columns(2).ColumnName = "Name"
                            dsResult.Tables(0).Columns(3).ColumnName = "Delay"
                            dsResult.Tables(0).Columns(4).ColumnName = "OT"


                            dsResult.Tables(0).Columns(5).ColumnName = "Unpaid"
                            dsResult.Tables(0).Columns(6).ColumnName = "Paid"
                            dsResult.Tables(0).Columns(7).ColumnName = "Sick"
                            dsResult.Tables(0).Columns(8).ColumnName = "ExcuseAbsent"
                            dsResult.Tables(0).Columns(9).ColumnName = "Absent"
                            dsResult.Tables(0).Columns(10).ColumnName = "WDays"
                            dsResult.Tables(0).Columns(11).ColumnName = "Total"
                            dsResult.Tables(0).Columns(12).ColumnName = "Month"
                            dsResult.Tables(0).Columns(13).ColumnName = "Year"
                            dsResult.Tables(0).Columns(14).ColumnName = "Project"
                            If CheckEmpSSnNo(dsResult) = False Then
                                Return False
                            End If

                            Dim SSnNo As String = ""
                            Dim LastEntryDate As DateTime
                            Dim SameEmployee As Boolean = False
                            For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                                If SSnNo = Row("SSnNo").ToString() Then
                                    SameEmployee = True
                                Else
                                    SameEmployee = False
                                End If

                                ClsEmployee.Find("SSnNo = '" & Row("SSnNo") & "'")
                                Dim ProjID As String = "0"
                                If ClsEmployee.ID <> 0 Then
                                    Try
                                        ProjID = Row("Project").ToString()
                                        clsProject.Find(" Code='" & ProjID & "'")
                                        ProjID = clsProject.ID
                                    Catch ex As Exception
                                        ProjID = "0"
                                    End Try



                                    Dim PerparDay As Integer = clsCompanies.PrepareDay
                                    Dim StartMonth As Integer = DropDownListM.SelectedItem.Text
                                    Dim StartYear As Integer = DropDownList3.SelectedItem.Text
                                    If StartMonth = 1 Then
                                        StartMonth = 12
                                        StartYear = StartYear - 1
                                    Else
                                        StartMonth = StartMonth - 1
                                    End If
                                    Dim StartDate As String = PerparDay & "/" & StartMonth & "/" & StartYear
                                    If SameEmployee Then
                                        StartDate = LastEntryDate.ToString()
                                    End If
                                    'If DropDownList2.SelectedValue = 0 And ProjID = 0 Then
                                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, _
                                    '        objNav.SetLanguage(Page, "Select Project/ÇÎÊÑ ÇáãÔÑæÚ"))
                                    '    Return False
                                    'End If
                                    ProjID = IIf(ProjID = "0", DropDownList2.SelectedValue, ProjID)

                                    'ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))

                                    'Edited By: Hassan Kuedi
                                    'Date: 2022-01-12
                                    'Purpose: Get Contract by date
                                    'Rabie 12-11-2023 Remove StartDate From ExcelSheet To be only Month and Year
                                    'ClsContracts.Find("ID = " & ClsContracts.GetContractIDDate(ClsEmployee.ID, Row("StartFrom").ToString()))
                                    ClsContracts.Find("ID = " & ClsContracts.GetContractIDDate(ClsEmployee.ID, StartDate.ToString()))
                                    If ClsContracts.ID <= 0 Then
                                        ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))
                                    End If
                                    'End

                                    ClsEmployeeClasses.Find("ID = " & ClsContracts.EmployeeClassID)

                                    Dim Delays As Decimal = Row(3)
                                    Dim TOT As Decimal = Row(4)
                                    Dim cndupVacations As Integer = Row(5)
                                    Dim cndpVacations As Integer = Row(6)
                                    Dim cndSVacations As Integer = Row(7)
                                    Dim cndAbVacations As Integer = Row(8) + Row(9)
                                    Dim cndwdays As Integer = Row(10)
                                    Dim Datestr As String = StartDate
                                    'Dim Datestr As String = Row("StartFrom")
                                    Dim commandString = ""
                                    If ClsDataAcessLayer.IsHijri(StartDate) Then
                                        Datestr = ClsDataAcessLayer.HijriToGreg(StartDate, "dd/MM/yyyy")
                                    ElseIf ClsDataAcessLayer.IsGreg(StartDate) Then
                                        Datestr = StartDate
                                    End If
                                    Dim basedate As Date = Convert.ToDateTime(Datestr)
                                    Dim strscript As String = ""
                                    Dim days As Integer = Row("Total")

                                    'If Row("Total") = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 Then
                                    '    days = 31

                                    'End If

                                    While basedate < Convert.ToDateTime(Datestr).AddDays(Row("Total"))
                                        'While basedate < Convert.ToDateTime(Datestr).AddDays(days)
                                        AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                        AttendanceTransactions.ProjectID = ProjID

                                        If ClsContracts.StartDate > basedate Then
                                            basedate = basedate.AddDays(1)
                                            Continue While
                                        End If
                                        If ClsContracts.EndDate <> Nothing And ClsContracts.EndDate < basedate Then
                                            basedate = basedate.AddDays(1)
                                            Continue While
                                        End If
                                        AttendanceTransactions.TrnsDatetime = basedate
                                        AttendanceTransactions.TotalHours = 0
                                        AttendanceTransactions.Status = -1
                                        Dim hrsemployeevacations As New Clshrs_EmployeesVacations(Me.Page)
                                        Dim hrsvacationstypes As New Clshrs_VacationsTypes(Me.Page)

                                        If cndpVacations > 0 Then
                                            If hrsvacationstypes.Find("IsPaid = 0 and isnull(IsSickVacation,0) = 0 Then") Then
                                                AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " And VacationTypeID = " & hrsvacationstypes.ID & " And ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndpVacations = cndpVacations - 1
                                                Else
                                                    'strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndpVacations = cndpVacations - 1
                                                End If
                                            Else
                                                cndpVacations = cndpVacations - 1
                                            End If
                                        ElseIf cndupVacations > 0 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If hrsvacationstypes.Find("IsPaid = -1") Then
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndupVacations = cndupVacations - 1
                                                Else
                                                    '   strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndupVacations = cndupVacations - 1
                                                End If
                                            Else
                                                cndupVacations = cndupVacations - 1
                                            End If

                                        ElseIf cndSVacations > 0 Then
                                            If hrsvacationstypes.Find("isnull(IsSickVacation,0) = 1") Then
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndSVacations = cndSVacations - 1
                                                Else
                                                    ' strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndSVacations = cndSVacations - 1
                                                End If
                                            Else
                                                cndSVacations = cndSVacations - 1
                                            End If
                                            'Edited By: Hassan Kuedi
                                            'Date: 2022-01-26
                                            'Purpose: Add a vacation check to the condition
                                        ElseIf cndAbVacations > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                            cndAbVacations = cndAbVacations - 1
                                        ElseIf cndwdays > 0 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If Delays > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                AttendanceTransactions.TotalLate = Delays / 60
                                                Delays = 0
                                            End If
                                            If TOT > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                AttendanceTransactions.Overtime = TOT
                                                TOT = 0
                                            End If
                                            'End
                                            cndwdays = cndwdays - 1

                                        ElseIf Row(10) = 30 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If Delays > 0 Then
                                                AttendanceTransactions.TotalLate = Delays / 60
                                                Delays = 0
                                            End If
                                            If TOT > 0 Then
                                                AttendanceTransactions.Overtime = TOT
                                                TOT = 0
                                            End If
                                            'cndwdays = cndwdays - 1
                                        End If

                                        AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                        AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                        AttendanceTransactions.TrnsType = 0
                                        AttendanceTransactions.Src = 1
                                        Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                        If AttendanceTransactionsFind.ID <> 0 Then
                                            AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                        Else
                                            AttendanceTransactions.Save()
                                        End If
                                        basedate = basedate.AddDays(1)
                                    End While

                                    Dim TotalDays As Integer

                                    TotalDays = dsResult.Tables(0).Compute("sum(Total)", "")
                                    TotalDays = dsResult.Tables(0).Compute("sum(Total)", "SSnNo =" & Row("SSnNo"))

                                    If TotalDays = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 Then
                                        AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                        AttendanceTransactions.TrnsDatetime = basedate
                                        AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                        AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                        AttendanceTransactions.TrnsType = 0
                                        AttendanceTransactions.Src = 1
                                        Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                        If AttendanceTransactionsFind.ID <> 0 Then
                                            AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                        Else
                                            AttendanceTransactions.Save()
                                        End If

                                    End If
                                    If strscript <> "" Then
                                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(AttendanceTransactions.ConnectionString, System.Data.CommandType.Text, strscript)
                                    End If
                                    SSnNo = Row("SSnNo").ToString()
                                    LastEntryDate = basedate
                                End If

                            Next

                        Case FileType.ExcelByProjectandSSnNoAndNationlityAndDep
                            Get_FromToDate(New Date(DropDownList3.SelectedValue, DropDownListM.SelectedValue, 1))

                            dsResult.Tables(0).Columns(1).ColumnName = "SSnNo"
                            dsResult.Tables(0).Columns(2).ColumnName = "Name"
                            dsResult.Tables(0).Columns(6).ColumnName = "Delay"
                            dsResult.Tables(0).Columns(7).ColumnName = "OT"
                            dsResult.Tables(0).Columns(8).ColumnName = "Unpaid"
                            dsResult.Tables(0).Columns(9).ColumnName = "Paid"
                            dsResult.Tables(0).Columns(10).ColumnName = "Sick"
                            dsResult.Tables(0).Columns(11).ColumnName = "ExcuseAbsent"
                            dsResult.Tables(0).Columns(12).ColumnName = "Absent"
                            dsResult.Tables(0).Columns(13).ColumnName = "WDays"
                            dsResult.Tables(0).Columns(14).ColumnName = "Total"
                            dsResult.Tables(0).Columns(15).ColumnName = "Month"
                            dsResult.Tables(0).Columns(16).ColumnName = "Year"
                            dsResult.Tables(0).Columns(17).ColumnName = "Project"
                            If CheckEmpSSnNo(dsResult) = False Then
                                Return False
                            End If

                            Dim SSnNo As String = ""
                            Dim LastEntryDate As DateTime
                            Dim SameEmployee As Boolean = False
                            For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                                If SSnNo = Row("SSnNo").ToString() Then
                                    SameEmployee = True
                                Else
                                    SameEmployee = False
                                End If

                                ClsEmployee.Find("SSnNo = '" & Row("SSnNo") & "'")
                                Dim ProjID As String = "0"
                                If ClsEmployee.ID <> 0 Then
                                    Try
                                        ProjID = Row("Project").ToString()
                                        clsProject.Find(" Code='" & ProjID & "'")
                                        ProjID = clsProject.ID
                                    Catch ex As Exception
                                        ProjID = "0"
                                    End Try



                                    Dim PerparDay As Integer = clsCompanies.PrepareDay
                                    Dim StartMonth As Integer = DropDownListM.SelectedItem.Text
                                    Dim StartYear As Integer = DropDownList3.SelectedItem.Text
                                    If StartMonth = 1 Then
                                        StartMonth = 12
                                        StartYear = StartYear - 1
                                    Else
                                        StartMonth = StartMonth - 1
                                    End If
                                    Dim StartDate As String = PerparDay & "/" & StartMonth & "/" & StartYear
                                    If SameEmployee Then
                                        StartDate = LastEntryDate.ToString()
                                    End If
                                    'If DropDownList2.SelectedValue = 0 And ProjID = 0 Then
                                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, _
                                    '        objNav.SetLanguage(Page, "Select Project/ÇÎÊÑ ÇáãÔÑæÚ"))
                                    '    Return False
                                    'End If
                                    ProjID = IIf(ProjID = "0", DropDownList2.SelectedValue, ProjID)

                                    'ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))

                                    'Edited By: Hassan Kuedi
                                    'Date: 2022-01-12
                                    'Purpose: Get Contract by date
                                    'Rabie 12-11-2023 Remove StartDate From ExcelSheet To be only Month and Year
                                    'ClsContracts.Find("ID = " & ClsContracts.GetContractIDDate(ClsEmployee.ID, Row("StartFrom").ToString()))
                                    ClsContracts.Find("ID = " & ClsContracts.GetContractIDDate(ClsEmployee.ID, StartDate.ToString()))
                                    If ClsContracts.ID <= 0 Then
                                        ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(ClsEmployee.ID))
                                    End If
                                    'End

                                    ClsEmployeeClasses.Find("ID = " & ClsContracts.EmployeeClassID)

                                    Dim Delays As Decimal = Row(6)
                                    Dim TOT As Decimal = Row(7)
                                    Dim cndupVacations As Integer = Row(8)
                                    Dim cndpVacations As Integer = Row(9)
                                    Dim cndSVacations As Integer = Row(10)
                                    Dim cndAbVacations As Integer = Row(11) + Row(12)
                                    Dim cndwdays As Integer = Row(13)
                                    Dim Datestr As String = StartDate
                                    'Dim Datestr As String = Row("StartFrom")
                                    Dim commandString = ""
                                    If ClsDataAcessLayer.IsHijri(StartDate) Then
                                        Datestr = ClsDataAcessLayer.HijriToGreg(StartDate, "dd/MM/yyyy")
                                    ElseIf ClsDataAcessLayer.IsGreg(StartDate) Then
                                        Datestr = StartDate
                                    End If
                                    Dim basedate As Date = Convert.ToDateTime(Datestr)
                                    Dim strscript As String = ""
                                    Dim days As Integer = Row("Total")

                                    'If Row("Total") = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 Then
                                    '    days = 31

                                    'End If

                                    While basedate < Convert.ToDateTime(Datestr).AddDays(Row("Total"))
                                        'While basedate < Convert.ToDateTime(Datestr).AddDays(days)
                                        AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactions.EmployeeID = ClsEmployee.ID
                                        AttendanceTransactions.ProjectID = ProjID

                                        If ClsContracts.StartDate > basedate Then
                                            basedate = basedate.AddDays(1)
                                            Continue While
                                        End If
                                        If ClsContracts.EndDate <> Nothing And ClsContracts.EndDate < basedate Then
                                            basedate = basedate.AddDays(1)
                                            Continue While
                                        End If
                                        AttendanceTransactions.TrnsDatetime = basedate
                                        AttendanceTransactions.TotalHours = 0
                                        AttendanceTransactions.Status = -1
                                        Dim hrsemployeevacations As New Clshrs_EmployeesVacations(Me.Page)
                                        Dim hrsvacationstypes As New Clshrs_VacationsTypes(Me.Page)

                                        If cndpVacations > 0 Then
                                            If hrsvacationstypes.Find("IsPaid = 0 and isnull(IsSickVacation,0) = 0 Then") Then
                                                AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " And VacationTypeID = " & hrsvacationstypes.ID & " And ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndpVacations = cndpVacations - 1
                                                Else
                                                    'strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndpVacations = cndpVacations - 1
                                                End If
                                            Else
                                                cndpVacations = cndpVacations - 1
                                            End If
                                        ElseIf cndupVacations > 0 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If hrsvacationstypes.Find("IsPaid = -1") Then
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndupVacations = cndupVacations - 1
                                                Else
                                                    '   strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndupVacations = cndupVacations - 1
                                                End If
                                            Else
                                                cndupVacations = cndupVacations - 1
                                            End If

                                        ElseIf cndSVacations > 0 Then
                                            If hrsvacationstypes.Find("isnull(IsSickVacation,0) = 1") Then
                                                If hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " and VacationTypeID = " & hrsvacationstypes.ID & " and ActualStartDate = '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                    cndSVacations = cndSVacations - 1
                                                Else
                                                    ' strscript &= Environment.NewLine & "insert into hrs_EmployeesVacations (EmployeeID,VacationTypeID,ExpectedStartDate,ExpectedEndDate,ActualStartDate,ActualEndDate,TotalDays,RemainingDays,ConsumDays,Remarks) VALUES (" & ClsEmployee.ID & "," & hrsvacationstypes.ID & ",'" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "','" & basedate.ToString("yyyy-MM-dd") & "','" & basedate.AddDays(1).ToString("yyyy-MM-dd") & "',1,0,0,'Auto')"
                                                    cndSVacations = cndSVacations - 1
                                                End If
                                            Else
                                                cndSVacations = cndSVacations - 1
                                            End If
                                            'Edited By: Hassan Kuedi
                                            'Date: 2022-01-26
                                            'Purpose: Add a vacation check to the condition
                                        ElseIf cndAbVacations > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                            cndAbVacations = cndAbVacations - 1
                                        ElseIf cndwdays > 0 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If Delays > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                AttendanceTransactions.TotalLate = Delays / 60
                                                Delays = 0
                                            End If
                                            If TOT > 0 And Not hrsemployeevacations.Find("EmployeeID = " & ClsEmployee.ID & " AND ActualStartDate <= '" & basedate.ToString("dd/MM/yyyy") & "' AND ActualEndDate > '" & basedate.ToString("dd/MM/yyyy") & "'") Then
                                                AttendanceTransactions.Overtime = TOT
                                                TOT = 0
                                            End If
                                            'End
                                            cndwdays = cndwdays - 1

                                        ElseIf Row(13) = 30 Then
                                            AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                            If Delays > 0 Then
                                                AttendanceTransactions.TotalLate = Delays / 60
                                                Delays = 0
                                            End If
                                            If TOT > 0 Then
                                                AttendanceTransactions.Overtime = TOT
                                                TOT = 0
                                            End If
                                            'cndwdays = cndwdays - 1
                                        End If

                                        AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                        AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                        AttendanceTransactions.TrnsType = 0
                                        AttendanceTransactions.Src = 1
                                        Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                        If AttendanceTransactionsFind.ID <> 0 Then
                                            AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                        Else
                                            AttendanceTransactions.Save()
                                        End If
                                        basedate = basedate.AddDays(1)
                                    End While

                                    Dim TotalDays As Integer

                                    TotalDays = dsResult.Tables(0).Compute("sum(Total)", "")
                                    TotalDays = dsResult.Tables(0).Compute("sum(Total)", "SSnNo =" & Row("SSnNo"))

                                    If TotalDays = 30 And System.DateTime.DaysInMonth(FromDate.Year, FromDate.Month) = 31 Then
                                        AttendanceTransactions.TotalHours = ClsEmployeeClasses.WorkHoursPerDay
                                        AttendanceTransactions.TrnsDatetime = basedate
                                        AttendanceTransactions.TimeIn = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).ToString("HH:mm")
                                        AttendanceTransactions.TimeOut = Convert.ToDateTime(ClsEmployeeClasses.DefultStartTime).AddHours(AttendanceTransactions.TotalHours).ToString("HH:mm")
                                        AttendanceTransactions.TrnsType = 0
                                        AttendanceTransactions.Src = 1
                                        Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
                                        AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)")
                                        If AttendanceTransactionsFind.ID <> 0 Then
                                            AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
                                        Else
                                            AttendanceTransactions.Save()
                                        End If

                                    End If
                                    If strscript <> "" Then
                                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(AttendanceTransactions.ConnectionString, System.Data.CommandType.Text, strscript)
                                    End If
                                    SSnNo = Row("SSnNo").ToString()
                                    LastEntryDate = basedate
                                End If

                            Next

                            Try
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Done/تمت العملية بنجاح "))
                            Catch ex As Exception
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Failled/فشلت العملية"))
                            End Try


                    End Select
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Done/تمت العملية بنجاح"))
                    lblProgress.Text = objNav.SetLanguage(Page, "Operation Done/تمت العملية بنجاح")
                Else
                    lblProgress.Text = ""
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "File Format Error/الملف غير صالح"))
                End If
            Else
                lblProgress.Text = "برجاء اختيار الملف"
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Select File/برجاء اختيار الملف"))
            End If
        Catch ex As Exception
            If ingr = 1 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Failled/فشلت العملية"))
                lblProgress.Text = objNav.SetLanguage(Page, "Operation Failled/فشلت العملية")
            Else
                lblProgress.Text = objNav.SetLanguage(Page, "Operation Failled Line No :/فشل العملية في السطر رقم :") & ingr.ToString()
            End If
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ex.Message)
        End Try
    End Function
    Enum Companies
        Majed
    End Enum
    Private FromDate As DateTime
    Private ToDate As DateTime
    Private FiscFromDate As DateTime
    Private FiscToDate As DateTime
    Private clsCompanies As Clssys_Companies
    Private clsBranch As Clssys_Branches
    Private ClsFisicalPeriods As Clssys_FiscalYearsPeriods
    Private IntFisicalPeriod As Integer
    Private Function Get_FromToDate(ByVal Basedate As Date) As Boolean
        Try
            ClsFisicalPeriods = New Clssys_FiscalYearsPeriods(Me.Page)
            ClsFisicalPeriods.GetFisicalperiodInfo(Basedate, IntFisicalPeriod, FromDate, ToDate)
            clsCompanies = New Clssys_Companies(Me.Page)
            clsCompanies.Find("ID = " & ClsFisicalPeriods.MainCompanyID)
            FiscFromDate = ClsFisicalPeriods.FromDate
            FiscToDate = ClsFisicalPeriods.ToDate

            If clsCompanies.PrepareDay > 0 Then
                If clsCompanies.IsHigry = True Then
                    Dim strarr As String() = ClsFisicalPeriods.HFromDate.Split("/")
                    Dim FrmHDate As String = clsCompanies.PrepareDay & "/" & IIf(strarr(1) = "01", "12", strarr(1) - 1) & "/" & IIf(strarr(1) = "01", strarr(2) - 1, strarr(2))
                    ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy")
                    FromDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy"), "dd/MM/yyyy")

                    Dim strarr1 As String() = FrmHDate.Split("/")
                    Dim ToHDate As String = clsCompanies.PrepareDay - 1 & "/" & IIf(strarr1(1) = "12", "01", strarr1(1) + 1) & "/" & IIf(strarr1(1) = "12", strarr1(2) + 1, strarr1(2))
                    ToDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(ToHDate, "dd/MM/yyyy"), "dd/MM/yyyy")
                Else
                    FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsCompanies.PrepareDay)
                    ToDate = FromDate.AddMonths(1).AddDays(-1)
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function CheckEmpCode(ByVal ds As System.Data.DataSet) As Boolean
        Dim strerrors As String = ""
        Dim ms As New MemoryStream()
        Dim tw As New StreamWriter(ms)

        Dim HASERRORS As Integer = 0
        Dim clsProject As New Clshrs_Projects(Page, "hrs_Projects")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsProject.ConnectionString)
        Dim TotalRow As Integer
        Dim FinishedRow As Integer
        TotalRow = ds.Tables(0).Rows.Count


        lblCounter.Text = "0 of " & TotalRow
        ClsFisicalPeriods = New Clssys_FiscalYearsPeriods(Me.Page)
        ClsFisicalPeriods.GetFisicalperiodInfo(BaseDate, IntFisicalPeriod, FromDate, ToDate)
        clsCompanies = New Clssys_Companies(Me.Page)
        clsCompanies.Find("ID = " & ClsFisicalPeriods.MainCompanyID)
        FiscFromDate = ClsFisicalPeriods.FromDate
        FiscToDate = ClsFisicalPeriods.ToDate
        Dim PerparDay As Integer = clsCompanies.PrepareDay
        Dim StartMonth As Integer = DropDownListM.SelectedItem.Text
        Dim StartYear As Integer = DropDownList3.SelectedItem.Text

        Dim StartDate As String = PerparDay & "/" & StartMonth & "/" & StartYear

        FinishedRow = 0
        For Each Row As Data.DataRow In ds.Tables(0).Rows
            'If Row("Month").ToString().Trim() <> StartMonth And Not String.IsNullOrEmpty(Row("Code")) Then
            '    tw.WriteLine("Entered MOnth " & Row("Month") & "in Line " & FinishedRow + 1 & " Is Not Compatible with Current Month/ الشهر  رقم " & Row("Month") & "   في السطر رقم " & FinishedRow + 1 & " غير متوافق مع الشهر ")
            '    HASERRORS = HASERRORS + 1
            'End If
            'If Row("Year").ToString().Trim() <> StartYear And Not String.IsNullOrEmpty(Row("Code")) Then
            '    tw.WriteLine("Entered Year " & Row("Year") & "in Line " & FinishedRow + 1 & " Is Not Compatible with Current Year/ السنه  رقم " & Row("Year") & "   في السطر رقم " & FinishedRow + 1 & " غير متوافق مع السنه ")
            '    HASERRORS = HASERRORS + 1
            'End If
            If Row("Code").ToString().Trim() <> "" Then

                ClsEmployee.Find("code = '" & Row("Code") & "'")
                If ClsEmployee.ID = 0 Then

                    'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Employee Code " & Row("Code") & " dosen't exist / الموظف رقم " & Row("Code") & " غير موجود     "))

                    tw.WriteLine("Employee Code " & Row("Code") & " dosen't exist /الموظف رقم " & Row("Code") & " غير موجود   ")
                    HASERRORS = HASERRORS + 1
                    'Exit Function
                End If
            End If
            If Row("Project").ToString().Trim() <> "" Then
                clsProject.Find("code = '" & Row("Project") & "'")
                If clsProject.ID = 0 Then
                    'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Project Code " & Row("Project") & " dosen't exist / ßæÏ ÇáãÔÑæÚ " & Row("Project") & " ÛíÑ ãæÌæÏ "))
                    'Exit Function
                    tw.WriteLine("Project Code " & Row("Project") & " dosen't exist / المشروع رقم " & Row("Project") & "  غير موجود ")
                    HASERRORS = HASERRORS + 1
                End If
            End If
            FinishedRow += 1
            lblCounter.Text = FinishedRow & " of " & TotalRow
        Next
        tw.Flush()
        Dim bytes As Byte() = ms.ToArray()
        ms.Close()

        If HASERRORS > 0 Then
            Response.Clear()
            Response.ContentType = "application/force-download"
            Response.AddHeader("content-disposition", "attachment;    filename=EmployeesOrProjectsWithWrongCode" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".txt")
            'Response.AddHeader("content-disposition", ";    filename=EmployeesOrProjectsWithWrongCode" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".txt")
            Response.BinaryWrite(bytes)
            'HttpContext.Current.ApplicationInstance.CompleteRequest()
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
            'Response.End()
            Return False
        End If
        Return True
    End Function

    Public Function CheckEmpSSnNo(ByVal ds As System.Data.DataSet) As Boolean
        Dim strerrors As String = ""
        Dim ms As New MemoryStream()
        Dim tw As New StreamWriter(ms)

        Dim HASERRORS As Integer = 0
        Dim clsProject As New Clshrs_Projects(Page, "hrs_Projects")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsProject.ConnectionString)
        Dim TotalRow As Integer
        Dim FinishedRow As Integer
        TotalRow = ds.Tables(0).Rows.Count


        lblCounter.Text = "0 of " & TotalRow
        ClsFisicalPeriods = New Clssys_FiscalYearsPeriods(Me.Page)
        ClsFisicalPeriods.GetFisicalperiodInfo(BaseDate, IntFisicalPeriod, FromDate, ToDate)
        clsCompanies = New Clssys_Companies(Me.Page)
        clsCompanies.Find("ID = " & ClsFisicalPeriods.MainCompanyID)
        FiscFromDate = ClsFisicalPeriods.FromDate
        FiscToDate = ClsFisicalPeriods.ToDate
        Dim PerparDay As Integer = clsCompanies.PrepareDay
        Dim StartMonth As Integer = DropDownListM.SelectedItem.Text
        Dim StartYear As Integer = DropDownList3.SelectedItem.Text

        Dim StartDate As String = PerparDay & "/" & StartMonth & "/" & StartYear

        FinishedRow = 0
        For Each Row As Data.DataRow In ds.Tables(0).Rows
            'If Row("Month").ToString().Trim() <> StartMonth And Not String.IsNullOrEmpty(Row("Code")) Then
            '    tw.WriteLine("Entered MOnth " & Row("Month") & "in Line " & FinishedRow + 1 & " Is Not Compatible with Current Month/ الشهر  رقم " & Row("Month") & "   في السطر رقم " & FinishedRow + 1 & " غير متوافق مع الشهر ")
            '    HASERRORS = HASERRORS + 1
            'End If
            'If Row("Year").ToString().Trim() <> StartYear And Not String.IsNullOrEmpty(Row("Code")) Then
            '    tw.WriteLine("Entered Year " & Row("Year") & "in Line " & FinishedRow + 1 & " Is Not Compatible with Current Year/ السنه  رقم " & Row("Year") & "   في السطر رقم " & FinishedRow + 1 & " غير متوافق مع السنه ")
            '    HASERRORS = HASERRORS + 1
            'End If
            If Row("SSnNo").ToString().Trim() <> "" Then

                ClsEmployee.Find("SSnNo = '" & Row("SSnNo") & "'")
                If ClsEmployee.ID = 0 Then
                    'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Employee Code " & Row("Code") & " dosen't exist /غير موجود " & Row("Code") & " ÛíÑ ãæÌæÏ  "))
                    ''Exit Function
                    tw.WriteLine("Employee with SSnNo " & Row("SSnNo") & " dosen't exist /الموظف بالاقامة رقم " & Row("SSnNo") & " غير موجود   ")
                    HASERRORS = HASERRORS + 1
                End If
            End If
            If Row("Project").ToString().Trim() <> "" Then
                clsProject.Find("code = '" & Row("Project") & "'")
                If clsProject.ID = 0 Then
                    'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Project Code " & Row("Project") & " dosen't exist / ßæÏ ÇáãÔÑæÚ " & Row("Project") & " ÛíÑ ãæÌæÏ "))
                    'Exit Function
                    tw.WriteLine("Project Code " & Row("Project") & " dosen't exist / المشروع رقم " & Row("Project") & "  غير موجود ")
                    HASERRORS = HASERRORS + 1
                End If
            End If
            FinishedRow += 1
            lblCounter.Text = FinishedRow & " of " & TotalRow
        Next
        tw.Flush()
        Dim bytes As Byte() = ms.ToArray()
        ms.Close()

        If HASERRORS > 0 Then
            Response.Clear()
            Response.ContentType = "application/force-download"
            Response.AddHeader("content-disposition", "attachment;    filename=EmployeesOrProjectsWithWrongCode" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".txt")
            'Response.AddHeader("content-disposition", ";    filename=EmployeesOrProjectsWithWrongCode" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".txt")
            Response.BinaryWrite(bytes)
            'HttpContext.Current.ApplicationInstance.CompleteRequest()
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
            'Response.End()
            Return False
        End If
        Return True
    End Function
    Public Function CheckEmpCodeandIqama(ByVal ds As System.Data.DataSet) As Boolean
        Dim strerrors As String = ""
        Dim ms As New MemoryStream()
        Dim tw As New StreamWriter(ms)

        Dim HASERRORS As Integer = 0
        Dim clsProject As New Clshrs_Projects(Page, "hrs_Projects")
        For Each Row As Data.DataRow In ds.Tables(0).Rows
            If Row("Code").ToString().Trim() <> "" And Row("Iqama").ToString().Trim() <> "" Then
                ClsEmployee.Find("code = '" & Row("Code") & "' And ssnno='" & Row("Iqama") & "'")
                If ClsEmployee.ID = 0 Then
                    tw.WriteLine("Employee Code " & Row("Code") & " or Iqama " & Row("Iqama") & " dosen't exist")
                    HASERRORS = HASERRORS + 1
                End If
            End If
        Next
        tw.Flush()
        Dim bytes As Byte() = ms.ToArray()
        ms.Close()
        If HASERRORS > 0 Then
            Response.Clear()
            Response.ContentType = "application/force-download"
            Response.AddHeader("content-disposition", "attachment;    filename=ERRORSLOGFILE" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".txt")
            Response.BinaryWrite(bytes)
            Response.End()
            Return False
        End If
        Return True
    End Function

    Public Function CheckProject(ByVal ds As System.Data.DataSet) As Boolean
        Dim strerrors As String = ""
        Dim ms As New MemoryStream()
        Dim tw As New StreamWriter(ms)

        Dim HASERRORS As Integer = 0
        Dim clsProject As New Clshrs_Projects(Page, "hrs_Projects")
        For Each Row As Data.DataRow In ds.Tables(0).Rows
            If Row("Project").ToString().Trim() <> "" Then
                clsProject.Find("code = '" & Row("Project") & "'")
                If clsProject.ID = 0 Then
                    tw.WriteLine("Project Code " & Row("Project") & " dosen't exist")
                    HASERRORS = HASERRORS + 1
                End If
            End If
        Next
        tw.Flush()
        Dim bytes As Byte() = ms.ToArray()
        ms.Close()
        If HASERRORS > 0 Then
            Response.Clear()
            Response.ContentType = "application/force-download"
            Response.AddHeader("content-disposition", "attachment;    filename=ERRORSLOGFILE" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".txt")
            Response.BinaryWrite(bytes)
            Response.End()
            Return False
        End If
        Return True
    End Function
    Public Function CheckCodeName(ByVal ds As System.Data.DataSet) As Boolean
        Dim strerrors As String = ""
        Dim ms As New MemoryStream()
        Dim tw As New StreamWriter(ms)

        Dim HASERRORS As Integer = 0
        Dim clsProject As New Clshrs_Projects(Page, "hrs_Projects")
        For Each Row As Data.DataRow In ds.Tables(0).Rows
            If Row("Code").ToString().Trim() <> "" Then
                ClsEmployee.Find("code = '" & Row("Code") & "'")
                If ClsEmployee.ID = 0 Then
                    tw.WriteLine("Employee Code " & Row("Code") & " dosen't exist")
                HASERRORS = HASERRORS + 1
                End If
                'If ClsEmployee.EngName.ToLower().Trim() <> Row("Name").ToString().ToLower().Trim() Then
                '    tw.WriteLine("For Employee Code " & Row("Code") & ", The employee name " & Row("Name") & " doesn't matches")
                '    HASERRORS = HASERRORS + 1
                'End If
                If Not clsProject.Find(" Code='" & Row("Project").ToString().Trim() & "'") Then
                    tw.WriteLine("For Employee Code " & Row("Code") & ", The Project Code " & Row("Project") & " doesn't Exist")
                    HASERRORS = HASERRORS + 1
                End If
            End If
        Next
        tw.Flush()
        Dim bytes As Byte() = ms.ToArray()
        ms.Close()
        If HASERRORS > 0 Then
            Response.Clear()
            Response.ContentType = "application/force-download"
            Response.AddHeader("content-disposition", "attachment;    filename=ERRORSLOGFILE" & DateTime.Now.ToString("ddMMyyyyHHmmss") & ".txt")
            Response.BinaryWrite(bytes)
            Response.End()
            Return False
        End If
        Return True
    End Function
    Private Function LoadExcelSheet(ByVal strFinalPath As String, ByVal Comp As Companies) As Data.DataSet
        Try
            Dim mStreamReader As New IO.StreamReader(strFinalPath)
            Dim mLine As Object '= mStreamReader.ReadLine
            Dim ds As New Data.DataSet
            Dim mCell() As String
            Dim mTime As String = ""
            Dim mEmpMachineCode As String = ""
            Dim mSplit As Char
            ds.Tables.Add("TrnsDate").Columns.Add("TrnsDate")
            ds.Tables.Add("EmpMachineCode")
            ds.Tables("EmpMachineCode").Columns.Add("EmpMachineCode")
            ds.Tables("EmpMachineCode").Columns.Add("TrnsDate")
            ds.Tables.Add("Time")
            ds.Tables("Time").Columns.Add("Time")
            ds.Tables("Time").Columns.Add("Direction")
            ds.Tables("Time").Columns.Add("EmpMachineCode")
            ds.Tables("Time").Columns.Add("TrnsDate")
            Select Case Comp
                Case Companies.Majed
                    Dim mDate() As String
                    Dim mDirection As String = ""
                    mSplit = ";"
                    Do
                        Try
                            mLine = mStreamReader.ReadLine

                            If IsNothing(mLine) Then
                                Exit Do
                            End If
                            mCell = mLine.Split(mSplit)
                            If Not mCell(3).Contains("Reachable") Then
                                mDate = mCell(0).Split(" ")(0).Split("/")
                                Dim TrnsDate As New DateTime(CInt(mDate(2)), CInt(mDate(1)), CInt(mDate(0)), 0, 0, 0, 0)
                                If ds.Tables("TrnsDate").Select("TrnsDate='" & CStr(TrnsDate) & "'").Length = 0 Then
                                    ds.Tables("TrnsDate").Rows.Add(CStr(TrnsDate))
                                End If
                                mEmpMachineCode = mCell(3).Split(" ")(1)
                                If ds.Tables("EmpMachineCode").Select("EmpMachineCode='" & mEmpMachineCode & "' And TrnsDate='" & CStr(TrnsDate) & "'").Length = 0 Then
                                    ds.Tables("EmpMachineCode").Rows.Add(mEmpMachineCode, CStr(TrnsDate))
                                End If
                                mTime = mCell(0).Split(" ")(1)
                                mDirection = mCell(mCell.Length - 1).Split(" ")(mCell(mCell.Length - 1).Split(" ").Length - 1).Replace("direction:", "")

                                ds.Tables("Time").Rows.Add(mTime, mDirection, mEmpMachineCode, CStr(TrnsDate))
                            End If
                        Catch ex As Exception
                        End Try
                    Loop
            End Select
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub SaveAttendance(ByVal mEmpMachineCode As String, ByVal mTrnsDate As String, ByVal mTimeIn As String, ByVal mTimeOut As String, ByVal ProjectID As Integer)
        If mEmpMachineCode.Length = 0 Then Return
        If mTrnsDate.Length = 0 Then Return
        If mTimeIn.Length = 0 Then Return

        If mTimeOut.Length = 0 Then
            GetEmployee(mEmpMachineCode, ClsEmployee)
            mTimeOut = CDate(ClsEmployeeClasses.DefultEndTime).ToString("HH:mm")
        End If

        If ClsEmployee.ID = 0 Then
            GetEmployee(mEmpMachineCode, ClsEmployee)
        End If
        If ClsEmployee.ID > 0 Then
            AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
            AttendanceTransactions.TrnsDatetime = mTrnsDate
            AttendanceTransactions.EmployeeID = ClsEmployee.ID

            If ProjectID > 0 Then
                AttendanceTransactions.ProjectID = ProjectID
            Else
                If DropDownList2.SelectedValue > 0 Then
                    AttendanceTransactions.ProjectID = DropDownList2.SelectedValue
                Else
                    AttendanceTransactions.ProjectID = ClsEmployeeClasses.DefaultProjectID
                End If
            End If

            AttendanceTransactions.TotalHours = DateDiff(DateInterval.Minute, CDate(mTrnsDate & " " & mTimeIn), CDate(mTrnsDate & " " & mTimeOut)) / 60
            AttendanceTransactions.TimeIn = Convert.ToDateTime(mTimeIn).ToString("HH:mm")
            AttendanceTransactions.TimeOut = Convert.ToDateTime(mTimeOut).ToString("HH:mm")
            AttendanceTransactions.TrnsType = 0
            Dim AttendanceTransactionsFind = New ClsAtt_AttendTransactions(Page)
            If AttendanceTransactionsFind.Find("EmployeeID = " & AttendanceTransactions.EmployeeID & " and ProjectID = " & AttendanceTransactions.ProjectID & " and Convert(Datetime,TrnsDatetime,103) = Convert(Datetime,'" & AttendanceTransactions.TrnsDatetime & "',103)") Then
                AttendanceTransactions.Update("ID =" & AttendanceTransactionsFind.ID)
            Else
                AttendanceTransactions.Save()
            End If
        End If
    End Sub
    Public Function IsTime(sTime As String, RetTime As String) As Boolean
        If Left(Trim(sTime), 1) Like "#" Then
            If IsDate(sTime.Replace(".", ":")) Then
                RetTime = sTime.Replace(".", ":")
                IsTime = True
            Else
                RetTime = DateTime.Now.ToString("dd/MM/yyyy") & " " & sTime.Replace(".", ":") & ":00"
                IsTime = IsDate(DateTime.Now.ToString("dd/MM/yyyy") & " " & sTime.Replace(".", ":") & ":00")
            End If
        End If
    End Function
    Private Function GetEmployee(ByVal mEmpMachineCode As String, ByRef clsEmp As Clshrs_Employees) As Boolean
        Try
            ClsContracts = New Clshrs_Contracts(Page)
            ClsEmployeeClasses = New Clshrs_EmployeeClasses(Page)
            clsEmp.Find("convert(int,dbo.fnRemoveNonNumericCharacters(MachineCode)) = '" & Convert.ToInt32(mEmpMachineCode) & "'")
            If clsEmp.ID <> 0 Then
                ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(clsEmp.ID))
                ClsEmployeeClasses.Find("ID = " & ClsContracts.EmployeeClassID)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Protected Sub ImageButton_Print_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton.Click
        Dim clsDAL As New ClsDataAcessLayer(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(clsDAL.ConnectionString)
        Try

            'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Select Project/ اختر المشروع"))

            lblProgress.Text = objNav.SetLanguage(Page, "Loading.../جاري التحميل ...")
            lblCounter.Text = "Working"
        Catch ex As Exception
            lblProgress.Text = "Loading..."
        End Try

        If Not LoadAttendFile(ddlFileExtension.SelectedValue) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Operation Failled/فشلت العملية"))

        End If

    End Sub
End Class
