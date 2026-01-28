Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEmployeeExtraItems
    Inherits MainPage

#Region "Protected Sub"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(Page)
        Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim IntSelectedPeriod As Integer = 0
        Dim IntModuleId As Integer = GetModuleID("frmPrepareSalaries")
        Dim clsBranch As New Clssys_Branches(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
        If ClsObjects.Find(" Code='" & ClsEmployee.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                Dim IntDimension As Integer = 510
                Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & ClsSearchs.ID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
            End If
        End If
        If Not IsPostBack Then
            UwgSearchEmployees.Columns.FromKey("EmployeeName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))
            Dim ClsTransactionsTypes As New Clshrs_TransactionsTypes(Page)
            ClsTransactionsTypes.GetList(UwgSearchEmployees.DisplayLayout.Bands(0).Columns(5).ValueList, True, "Code", " and InputIsNumeric = 1 and IsPaid = 1 and IsDistributable = 0")
            ClsFisicalYearsPeriods.GetDropDownList(DdlPeriods, IntModuleId, True, "")
            IntSelectedPeriod = ClsFisicalYearsPeriods.GetLastOpenedFiscalPieriod(IntModuleId)
            lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
            Page.Session.Add("Lage", lblLage.Text)
            Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
        End If
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetData()
    End Sub
    Protected Sub Button_Click(sender As Object, e As System.EventArgs) Handles LinkButton_Save.Command, ImageButton_Save.Command, LinkButton_Delete.Command, ImageButton_Delete.Command, LinkButton_Transfeer.Command, ImageButton_Transfeer.Command, ImageButton_Refund.Command, LinkButton_Refund.Command
        Try
            Dim ClsEmployee As New Clshrs_Employees(Page)
            Dim ClsContract As New Clshrs_Contracts(Page)
            Dim ClsTransactionsType As New Clshrs_TransactionsTypes(Page)
            Dim ClsContractsTransaction As New Clshrs_ContractsTransactions(Page)

            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
            Select Case DirectCast(e, System.Web.UI.WebControls.CommandEventArgs).CommandArgument
                Case "Transfeer"
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        ClsContractsTransaction = New Clshrs_ContractsTransactions(Page)
                        If row.Cells.FromKey("EmployeeCode").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        If row.Cells.FromKey("RelEmployeeID").Value = 0 Or row.Cells.FromKey("RelTransactionID").Value = 0 Then
                            Continue For
                        End If
                        If row.Cells(1).Value = True Then
                            Dim strcammand As String = "update hrs_EmployeeExtraItems set Status = 1 where ID =" & row.Cells(0).Value
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Transfeer Done!/تم التحويل"))
                Case "Refund"
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("EmployeeCode").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        If row.Cells(1).Value = True Then
                            Dim ClsEmployeesTransaction As New Clshrs_EmployeesTransactions(Page)
                            ClsEmployeesTransaction.Find("PrepareType = 'N' and FiscalYearPeriodID = '" & DdlPeriods.SelectedValue & "' and EmployeeID = " & row.Cells.FromKey("RelEmployeeID").Value)
                            If ClsEmployeesTransaction.DataSet.Tables(0).Rows.Count = 0 Then
                                Dim strcammand As String = "update hrs_EmployeeExtraItems set Status = 0 where ID =" & row.Cells(0).Value
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                            End If
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Cancel Done!/تم الإلغاء"))
                Case "Save"
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("EmployeeCode").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        Dim strcammand As String = "set dateformat dmy; update hrs_EmployeeExtraItems set EmployeeCode = '" & row.Cells.FromKey("EmployeeCode").Value & "',TransactionCode = '" & row.Cells.FromKey("TransactionCode").Value & "',Amount = '" & row.Cells.FromKey("Amount").Value & "' where ID =" & row.Cells(0).Value
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Save Done!/تم الحفظ"))
                Case "Delete"
                    For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                        If row.Cells.FromKey("EmployeeCode").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                            Continue For
                        End If
                        If row.Cells(1).Value = True Then
                            Dim strcammand As String = "delete from hrs_EmployeeExtraItems where ID =" & row.Cells(0).Value
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                            If row.Cells.FromKey("Src").Value = "2" Then
                                strcammand = "delete from hrs_ProjectEmployeeOvertime where EmployeeID =" & row.Cells.FromKey("RelEmployeeID").Value & " and RefNo = '" & row.Cells.FromKey("TransactionNo").Value & "'"
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                            ElseIf row.Cells.FromKey("Src").Value = "3" Then
                                strcammand = "delete from hrs_ProjectEmployeeRewards where EmployeeID =" & row.Cells.FromKey("RelEmployeeID").Value & " and ID = '" & row.Cells.FromKey("TransactionNo").Value & "'"
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                            ElseIf row.Cells.FromKey("Src").Value = "4" Then
                                strcammand = "delete from hrs_ProjectEmployeePenalities where EmployeeID =" & row.Cells.FromKey("RelEmployeeID").Value & " and ID = '" & row.Cells.FromKey("TransactionNo").Value & "'"
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                            ElseIf row.Cells.FromKey("Src").Value = "5" Then
                                strcammand = "delete from hrs_EmployeesTransactions where EmployeeID =" & row.Cells.FromKey("RelEmployeeID").Value & " and ID = '" & row.Cells.FromKey("TransactionNo").Value & "'"
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployee.ConnectionString, Data.CommandType.Text, strcammand)
                            End If
                        End If
                    Next
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, objNav.SetLanguage(Page, "Delete Done!/تم الحذف"))
            End Select
            GetData()
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Private Function"

    Private Function GetData() As Boolean
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim strcommand As String = "select hrs_EmployeeExtraItems.ID,EmployeeCode,(isnull((select top 1 ID from hrs_Employees where code = hrs_EmployeeExtraItems.EmployeeCode),0)) as RelEmployeeID,(isnull((select dbo.fn_GetEmpName(hrs_Employees.Code, " & ClsNavigationHandler.SetLanguage(Me, "1/0") & ") from hrs_Employees where code = hrs_EmployeeExtraItems.EmployeeCode),hrs_EmployeeExtraItems.EmployeeName)) as EmployeeName,TransactionCode,(isnull((select top 1 ID from hrs_TransactionsTypes where InputIsNumeric = 1 and IsPaid = 1 and code = hrs_EmployeeExtraItems.TransactionCode),0)) as RelTransactionID,(isnull((select top 1 " & ClsNavigationHandler.SetLanguage(Me, "EngName/ArbName") & " from hrs_TransactionsTypes where code = hrs_EmployeeExtraItems.TransactionCode)," & ClsNavigationHandler.SetLanguage(Me, "'Un Defined'/'غير محدد'") & ")) as TransactionName,Amount,Status,UploadDate,Src,TransactionNo,ProjectID as ProjectCode ,hrs_projects.ArbName as ProjectName from hrs_EmployeeExtraItems  join hrs_projects on hrs_EmployeeExtraItems.ProjectID= hrs_projects.Code"
        Dim strFilter As String = " where EmployeeCode like '%" & txtCode.Text & "%' and Status =" & ddlFilter.SelectedValue & " and FiscalPeriodID =" & DdlPeriods.SelectedValue

        Dim dsEmployee As New Data.DataSet
        dsEmployee = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, Data.CommandType.Text, strcommand & strFilter & " order by UploadDate,EmployeeCode")

        UwgSearchEmployees.DataSource = Nothing
        UwgSearchEmployees.DataBind()
        If dsEmployee.Tables(0).Rows.Count > 0 Then
            Dim ds As New Data.DataSet
            ds.Tables.Add()
            ds.Tables(0).Columns.Add("EmployeeCode")
            ds.Tables(0).Rows.Add(ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل"))
            ds.Tables(0).Merge(dsEmployee.Tables(0))

            UwgSearchEmployees.DataSource = ds.Tables(0)
            UwgSearchEmployees.DataBind()

            For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In UwgSearchEmployees.Rows
                If row.Cells.FromKey("EmployeeCode").Value = ClsNavigationHandler.SetLanguage(Me, "Select All/تحديد الكل") Then
                    Continue For
                End If
                If row.Cells(3).Value = 0 Or row.Cells(8).Value = 0 Then
                    row.Style.BackColor = Drawing.Color.Bisque
                End If
            Next
        End If
        If ddlFilter.SelectedValue = 1 Then
            ImageButton_Delete.Visible = False
            LinkButton_Delete.Visible = False

            ImageButton_Save.Visible = False
            LinkButton_Save.Visible = False

            ImageButton_Transfeer.Visible = False
            LinkButton_Transfeer.Visible = False

            ImageButton_Refund.Visible = True
            LinkButton_Refund.Visible = True
        ElseIf ddlFilter.SelectedValue = 0 Then
            ImageButton_Delete.Visible = True
            LinkButton_Delete.Visible = True

            ImageButton_Save.Visible = True
            LinkButton_Save.Visible = True

            ImageButton_Transfeer.Visible = True
            LinkButton_Transfeer.Visible = True

            ImageButton_Refund.Visible = False
            LinkButton_Refund.Visible = False
        ElseIf ddlFilter.SelectedValue = 2 Then
            ImageButton_Delete.Visible = True
            LinkButton_Delete.Visible = True

            ImageButton_Save.Visible = False
            LinkButton_Save.Visible = False

            ImageButton_Transfeer.Visible = False
            LinkButton_Transfeer.Visible = False

            ImageButton_Refund.Visible = False
            LinkButton_Refund.Visible = False
        End If
    End Function

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

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetData()
    End Sub

    Protected Sub ddlFilter_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFilter.SelectedIndexChanged
        GetData()
    End Sub

    Protected Sub DdlPeriods_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DdlPeriods.SelectedIndexChanged
        GetData()
    End Sub
End Class
