Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports Venus.Application.SystemFiles.System.ClsDataAcessLayer

Partial Class frmEmployeesItemsPending
    Inherits MainPage

#Region "Public Decleration"
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Try
            Dim ClsWebHandler As New Venus.Shared.Web.WebHandler
            Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
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
                uwgPendingItems.Columns.FromKey("EmployeeName").CellStyle.HorizontalAlign = CInt(ClsNavigationHandler.SetLanguage(Page, "1/3"))

                Page.Session.Add("Lage", ClsNavigationHandler.SetLanguage(Page, "0/1"))
                Page.Session.Add("ConnectionString", ClsEmployee.ConnectionString)
                hdnLang.Value = ClsNavigationHandler.SetLanguage(Page, "0/1")

                wdcFromDate.Value = Date.Now
                wdcToDate.Value = Date.Now.AddMonths(1)
                GetPendingItems()
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
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, LinkButton_Save.Command
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Select Case e.CommandArgument
            Case "Save"
                Dim Cls As New Clshrs_EmployeesItems(Me.Page)
                For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgPendingItems.Rows
                    If row.Cells.FromKey("ID").Value <> Nothing Then
                        If row.Cells.FromKey("Check").Value Then
                            Cls.Find("ID = " & row.Cells.FromKey("ID").Value)
                            Cls.IsConfirmed = True
                            Cls.Update("ID = " & row.Cells.FromKey("ID").Value)
                        End If
                    End If
                Next
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ClsNavigationHandler.SetLanguage(Page, "Opreration Done / تمت العملية "))
                GetPendingItems()
        End Select
    End Sub
    Protected Sub btnFind_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnFind.Click
        GetPendingItems()
    End Sub

#End Region

#Region "Private Functions"

    Private Sub GetPendingItems()
        Dim ClsEmployee As New Clshrs_Employees(Page)
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployee.ConnectionString)
        Dim strfltr As String = ""
        If txtCode.Text <> "" Then
            strfltr &= " and (select Code from hrs_Employees where ID = EmployeeID) = '" & txtCode.Text & "'"
        End If
        If wdcFromDate.Text <> "" And wdcToDate.Text <> "" Then
            strfltr &= " and ReceivedDate between '" & Convert.ToDateTime(wdcFromDate.Value).ToString("dd/MM/yyyy") & "' and '" & Convert.ToDateTime(wdcToDate.Value).ToString("dd/MM/yyyy") & "'"
        End If


        Dim str As String = "set Dateformat DMY; select ID,(select Code from hrs_Employees where ID = EmployeeID) AS EmpCode,dbo.fn_GetEmpName((select Code from hrs_Employees where ID = EmployeeID)," & ClsNavigationHandler.SetLanguage(Page, "0/1") & ") AS EmployeeName," & _
                            "(select " & ClsNavigationHandler.SetLanguage(Page, "EngName/ArbName") & " from hrs_Items where ID = ItemID) AS ItemName,(select LicenseNumber from hrs_Items where ID = ItemID) AS LicenseNumber,convert(Varchar(10),ReceivedDate,103) AS ReceivedDate" & _
                            " from hrs_EmployeesItems where cancelDate is null and isnull(IsConfirmed,0) = 0" & strfltr
        Dim dsResult As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployee.ConnectionString, CommandType.Text, str)
        If dsResult.Tables(0).Rows.Count > 0 Then
            uwgPendingItems.DataSource = dsResult.Tables(0)
            uwgPendingItems.DataBind()
        Else
            uwgPendingItems.DataSource = Nothing
            uwgPendingItems.DataBind()
        End If
    End Sub

#End Region

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        GetPendingItems()
    End Sub
End Class
