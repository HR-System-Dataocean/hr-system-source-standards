Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmAttendanceEntry
    Inherits MainPage

#Region "Protected Sub"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim IntSelectedPeriod As Integer = 0
        Dim clsBranch As New Clssys_Branches(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        If Not IsPostBack Then
            DdlPeriods.Items.Clear()
            ClsEmployee.GetDropDownList(DdlPeriods, True, "CancelDate is null and ExcludeDate is null and ManagerID = " & ProfileCls.RetRefPeople())
            Dim Projcts As New Clshrs_Projects(Me, "hrs_Projects")
            Projcts.GetDropDownList(DropDownListProjects, False, "")

            If ClsEmployee.Find("ID = " & ProfileCls.RetRefPeople()) Then
                lblDesc.Text = ClsEmployee.FullName
            End If

            Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
            Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
            Item.DisplayText = " "
            Item.DataValue = -1

            UwgSearchEmployees.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Clear()
            UwgSearchEmployees.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Add(Item)

            UwgSearchEmployees.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Clear()
            UwgSearchEmployees.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Add(Item)

            UwgSearchEmployees.DisplayLayout.Bands(0).Columns(6).ValueList.ValueListItems.Clear()
            UwgSearchEmployees.DisplayLayout.Bands(0).Columns(6).ValueList.ValueListItems.Add(Item)

            UwgSearchEmployees.DisplayLayout.Bands(0).Columns(7).ValueList.ValueListItems.Clear()
            UwgSearchEmployees.DisplayLayout.Bands(0).Columns(7).ValueList.ValueListItems.Add(Item)

            For i As Integer = 0 To 23
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Dim Hrs As String = ""
                If i < 10 Then
                    Hrs = "0" & i
                Else
                    Hrs = i
                End If
                Item.DisplayText = Hrs
                Item.DataValue = i
                UwgSearchEmployees.DisplayLayout.Bands(0).Columns(3).ValueList.ValueListItems.Add(Item)
                UwgSearchEmployees.DisplayLayout.Bands(0).Columns(6).ValueList.ValueListItems.Add(Item)
            Next
            For i As Integer = 0 To 59
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Dim Hrs As String = ""
                If i < 10 Then
                    Hrs = "0" & i
                Else
                    Hrs = i
                End If
                Item.DisplayText = Hrs
                Item.DataValue = i
                UwgSearchEmployees.DisplayLayout.Bands(0).Columns(4).ValueList.ValueListItems.Add(Item)
                UwgSearchEmployees.DisplayLayout.Bands(0).Columns(7).ValueList.ValueListItems.Add(Item)
            Next
            GetData()
            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
        End If
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub
    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Prepare.Command, ImageButton_Prepare.Command
        Try
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Prepare"
                    Dim AttendanceTransactions As New ClsAtt_AttendTransactions(Page)
                    Dim objNav As New Venus.Shared.Web.NavigationHandler(AttendanceTransactions.ConnectionString)
                    Dim StringCommand As String = ""
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        AttendanceTransactions = New ClsAtt_AttendTransactions(Page)
                        Dim ClsContracts As New Clshrs_Contracts(Page)
                        Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Page)
                        ClsContracts.Find("ID = " & ClsContracts.GetLastContractID(DdlPeriods.SelectedValue))
                        ClsEmployeeClasses.Find("ID = " & ClsContracts.EmployeeClassID)
                        AttendanceTransactions.TrnsDatetime = CDate(row.Cells.FromKey("TrnsDate").Text)
                        AttendanceTransactions.EmployeeID = DdlPeriods.SelectedValue
                        AttendanceTransactions.ProjectID = ClsEmployeeClasses.DefaultProjectID
                        If row.Cells.FromKey("InHour").Value <> -1 And row.Cells.FromKey("InMinutes").Value <> -1 Then
                            AttendanceTransactions.TimeIn = row.Cells.FromKey("InHour").Text & ":" & row.Cells.FromKey("InMinutes").Text
                        End If
                        If row.Cells.FromKey("OutHour").Value <> -1 And row.Cells.FromKey("OutMinutes").Value <> -1 Then
                            AttendanceTransactions.TimeOut = row.Cells.FromKey("OutHour").Text & ":" & row.Cells.FromKey("OutMinutes").Text
                        End If
                        AttendanceTransactions.TotalHours = 0
                        If AttendanceTransactions.TimeIn <> "" And AttendanceTransactions.TimeOut <> "" Then
                            If Convert.ToDateTime(AttendanceTransactions.TrnsDatetime.ToString("dd/MM/yyyy") & " " & AttendanceTransactions.TimeOut) < Convert.ToDateTime(AttendanceTransactions.TrnsDatetime.ToString("dd/MM/yyyy") & " " & AttendanceTransactions.TimeIn) Then
                                AttendanceTransactions.TotalHours = (Convert.ToDateTime(AttendanceTransactions.TrnsDatetime.ToString("dd/MM/yyyy") & " " & AttendanceTransactions.TimeOut).AddDays(1) - Convert.ToDateTime(AttendanceTransactions.TrnsDatetime.ToString("dd/MM/yyyy") & " " & AttendanceTransactions.TimeIn)).TotalHours
                            Else
                                AttendanceTransactions.TotalHours = (Convert.ToDateTime(AttendanceTransactions.TrnsDatetime.ToString("dd/MM/yyyy") & " " & AttendanceTransactions.TimeOut) - Convert.ToDateTime(AttendanceTransactions.TrnsDatetime.ToString("dd/MM/yyyy") & " " & AttendanceTransactions.TimeIn)).TotalHours
                            End If
                        End If
                        AttendanceTransactions.Status = IIf(Convert.ToBoolean(row.Cells.FromKey("DayOff").Value) = True, 1, -1)
                        AttendanceTransactions.Src = 1
                        AttendanceTransactions.RegDate = DateTime.Now
                        AttendanceTransactions.RegUserID = AttendanceTransactions.DataBaseUserRelatedID
                        If row.Cells.FromKey("TrnsID").Value = 0 Then
                            AttendanceTransactions.Save()
                        Else
                            AttendanceTransactions.Update("ID =" & row.Cells.FromKey("TrnsID").Value)
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Update Done!/تم التحديث"))
                    GetData()
            End Select
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Private Function"

    Private Function GetData() As Boolean
        Try
            UwgSearchEmployees.Rows.Clear()

            If DdlPeriods.SelectedValue = 0 Then
                Return False
            End If
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(Page)
            ClsFisicalPeriods.Find("GETDATE() between DATEADD(Day,-1,FromDate) And DATEADD(Day,1,ToDate)")

            Dim clsCompanies As New Clssys_Companies(Page)
            clsCompanies.Find("ID=" & ClsFisicalPeriods.MainCompanyID)
            Dim FromDate As DateTime = ClsFisicalPeriods.FromDate
            Dim ToDate As DateTime = ClsFisicalPeriods.ToDate
            If clsCompanies.PrepareDay > 0 Then
                If clsCompanies.IsHigry = True Then
                    Dim strarr As String() = ClsFisicalPeriods.HFromDate.Split("/")
                    Dim FrmHDate As String = clsCompanies.PrepareDay & "/" & IIf(strarr(1) = "01", "12", strarr(1) - 1) & "/" & IIf(strarr(1) = "01", strarr(2) - 1, strarr(2))
                    ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy")
                    FromDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy"), "dd/MM/yyyy")
                Else
                    FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsCompanies.PrepareDay)
                End If
            End If
            Dim CntDays As Integer = ToDate.Subtract(FromDate).Days
            For CounDays As Integer = 0 To CntDays
                Dim OperDate As DateTime = FromDate.AddDays(CounDays)
                Dim strCommand As String
                strCommand = "Set DateFormat DMY; select isnull(ID,0) AS TrnsID,'" & OperDate.ToString("dd/MM/yyyy") & "' AS TrnsDate,isnull(DatePart(Hour,TimeIn),-1) AS InHour,isnull(DatePart(MINUTE,TimeIn),-1) AS InMinutes,isnull(DatePart(Hour,TimeOut),-1) AS OutHour,isnull(DatePart(MINUTE,TimeOut),-1) AS OutMinutes,Case When isnull(Status,-1) = 1 then 1 else 0 end AS DayOff from Att_AttendTransactions where ProjectID = " & DropDownListProjects.SelectedValue & " and EmployeeID = " & DdlPeriods.SelectedValue & " and CONVERT(Varchar,TrnsDatetime,103) = CONVERT(Varchar,'" & OperDate & "',103)"
                Dim dsEmployee As New Data.DataSet
                dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, strCommand)
                If dsEmployee.Tables(0).Rows.Count > 0 Then
                    UwgSearchEmployees.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {dsEmployee.Tables(0).Rows(0)(0), WeekdayName(Weekday(OperDate)), OperDate.ToString("dd/MM/yyyy"), dsEmployee.Tables(0).Rows(0)(2), dsEmployee.Tables(0).Rows(0)(3), "", dsEmployee.Tables(0).Rows(0)(4), dsEmployee.Tables(0).Rows(0)(5), dsEmployee.Tables(0).Rows(0)(6)}))
                Else
                    UwgSearchEmployees.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {0, WeekdayName(Weekday(OperDate)), OperDate.ToString("dd/MM/yyyy"), -1, -1, "", -1, -1, 0}))
                End If
            Next CounDays
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetData()
    End Sub

    Protected Sub DdlPeriods_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DdlPeriods.SelectedIndexChanged
        GetData()
    End Sub

    Protected Sub DropDownListProjects_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListProjects.SelectedIndexChanged
        GetData()
    End Sub
End Class
