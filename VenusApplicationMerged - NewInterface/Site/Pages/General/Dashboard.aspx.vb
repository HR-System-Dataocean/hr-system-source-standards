Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class Dashboard
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.hdnIsPostback.Value = Me.IsPostBack
    End Sub
    Protected Sub LinkButton2_Click(sender As Object, e As System.EventArgs) Handles LinkButton2.Click
        Dim Pagescls As New Clsdsh_Pages(Me)
        If Pagescls.Find("canceldate is null and ID in (select PageID from dsh_Widgets where dsh_Widgets.ID in (select WidgetsID from dsh_WidgetsPermissions where UserID = " & Pagescls.DataBaseUserRelatedID & " and CanView = 1))") Then
            For Each rw As Data.DataRow In Pagescls.DataSet.Tables(0).Rows
                Dim tb1 As New Infragistics.Web.UI.LayoutControls.ContentTabItem
                Dim screenHeight As Integer = HiddenField_H.Value - 50
                Dim screenwidth As Integer = HiddenField_W.Value - 50
                tb1.Controls.Add(New LiteralControl("<table cellpadding=""2"" cellspacing=""2"" style=""height: " & screenHeight & "px; width: " & screenwidth & "px;"">"))
                Dim crows As Integer = 0
                For i As Integer = 1 To rw("Rws")
                    crows = crows + 1
                    tb1.Controls.Add(New LiteralControl("<tr>"))
                    Dim ccol As Integer = 0
                    For x As Integer = 1 To rw("Col")
                        ccol = ccol + 1
                        tb1.Controls.Add(New LiteralControl("<td style=""border: 1px solid silver;width: " & CInt(100 / rw("Col")) & "%; height: " & CInt(100 / rw("Rws")) & "%;"">"))
                        Dim Widgetcls As New Clsdsh_Widgets(Me)
                        If Widgetcls.Find("ID in (select WidgetsID from dsh_WidgetsPermissions where UserID = " & Pagescls.DataBaseUserRelatedID & " and CanView = 1) and canceldate is null and PageID = " & rw("ID") & " and AlocationRw = " & crows & " and AlocationCol = " & ccol) Then
                            tb1.Controls.Add(New LiteralControl("<table cellpadding=""0"" cellspacing=""0"" style=""height: 100%; width: 100%;"">"))
                            tb1.Controls.Add(New LiteralControl("<tr>"))
                            Dim intWcnt As Integer = Widgetcls.DataSet.Tables(0).Rows.Count
                            If intWcnt = 1 Then
                                tb1.Controls.Add(New LiteralControl("<td style=""width: 100%; height: 100%;"">"))
                            End If
                            If intWcnt > 1 And Convert.ToString(Widgetcls.DataSet.Tables(0).Rows(0)("Remarks")) = "V" Then
                                tb1.Controls.Add(New LiteralControl("<td style=""width: 100%; height: 100%;"">"))
                            End If
                            For Each rw1 As Data.DataRow In Widgetcls.DataSet.Tables(0).Rows
                                If intWcnt > 1 And Convert.ToString(rw1("Remarks")) = "H" Then
                                    tb1.Controls.Add(New LiteralControl("<td style=""width: " & CInt(100 / intWcnt) & "%; height: 100%;"">"))
                                End If
                                If rw1("ViewType") = 1 Then
                                    Try
                                        Dim DDatatable As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Pagescls.ConnectionString, System.Data.CommandType.StoredProcedure, rw1("Src")).Tables(0)
                                        tb1.Controls.Add(New LiteralControl("<script type=""text/javascript"">"))
                                        tb1.Controls.Add(New LiteralControl("$(function () {"))
                                        tb1.Controls.Add(New LiteralControl("$(document).ready(function () {"))
                                        tb1.Controls.Add(New LiteralControl("$('#" & rw1("Code") & "').highcharts({"))
                                        tb1.Controls.Add(New LiteralControl("chart: {plotBackgroundColor: null,plotBorderWidth: null,plotShadow: false},"))
                                        tb1.Controls.Add(New LiteralControl("title: {text: '" & IIf(ProfileCls.CurrentLanguage() = "Ar", rw1("ArbName"), rw1("EngName")) & "'},"))
                                        tb1.Controls.Add(New LiteralControl("tooltip: {pointFormat: '{series.name} <b>{point.percentage:.1f}%</b>'},"))
                                        tb1.Controls.Add(New LiteralControl("plotOptions: {"))
                                        tb1.Controls.Add(New LiteralControl("pie: {allowPointSelect: true,cursor: 'pointer',dataLabels: {enabled: true},showInLegend: true,style: {color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'},connectorColor: 'silver'}},"))
                                        tb1.Controls.Add(New LiteralControl("series: [{type: 'pie',name: ' ',data: ["))
                                        For Each drow As Data.DataRow In DDatatable.Rows
                                            tb1.Controls.Add(New LiteralControl("['" & IIf(ProfileCls.CurrentLanguage() = "Ar", drow("NameArb"), drow("NameEng")) & "',    " & CInt(drow("Rslt1")) & "],"))
                                        Next
                                        tb1.Controls.Add(New LiteralControl("]}]});});});"))
                                        tb1.Controls.Add(New LiteralControl("</script>"))
                                        tb1.Controls.Add(New LiteralControl("<div style=""width:" & CInt(screenwidth / rw("Col") / IIf(Convert.ToString(rw1("Remarks")) = "H", intWcnt, 1)) & "px; height: " & CInt(screenHeight / rw("Rws") / IIf(Convert.ToString(rw1("Remarks")) = "V", intWcnt, 1)) & "px;"" id=""" & rw1("Code") & """></div>"))
                                    Catch ex As Exception
                                    End Try
                                ElseIf rw1("ViewType") = 2 Then
                                    Try
                                        Dim DDatatable As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Pagescls.ConnectionString, System.Data.CommandType.StoredProcedure, rw1("Src")).Tables(0)
                                        tb1.Controls.Add(New LiteralControl("<script type=""text/javascript"">"))
                                        tb1.Controls.Add(New LiteralControl("$(function () {"))
                                        tb1.Controls.Add(New LiteralControl("$('#" & rw1("Code") & "').highcharts({"))
                                        tb1.Controls.Add(New LiteralControl("chart: {type: 'column'},"))
                                        tb1.Controls.Add(New LiteralControl("title: {text: '" & IIf(ProfileCls.CurrentLanguage() = "Ar", rw1("ArbName"), rw1("EngName")) & "'},"))
                                        tb1.Controls.Add(New LiteralControl("tooltip: {pointFormat: '{series.name} <b>{point.y:.1f}</b>'},"))
                                        tb1.Controls.Add(New LiteralControl("xAxis: {type: 'category',labels: {rotation: -10}},"))
                                        tb1.Controls.Add(New LiteralControl("yAxis: {min: 0,title: {text: ' '}},"))
                                        tb1.Controls.Add(New LiteralControl("legend: {enabled: false},"))
                                        tb1.Controls.Add(New LiteralControl("series: [{name: ' ',data: ["))
                                        For Each drow As Data.DataRow In DDatatable.Rows
                                            tb1.Controls.Add(New LiteralControl("['" & IIf(ProfileCls.CurrentLanguage() = "Ar", drow("NameArb"), drow("NameEng")) & "',    " & CInt(drow("Rslt1")) & "],"))
                                        Next
                                        tb1.Controls.Add(New LiteralControl("],dataLabels: {"))
                                        tb1.Controls.Add(New LiteralControl("enabled: true,rotation: 0,color: '#FFFFFF',align: 'right',format: '{point.y:.1f}',y: 0}}]});});"))
                                        tb1.Controls.Add(New LiteralControl("</script>"))
                                        tb1.Controls.Add(New LiteralControl("<div style=""width:" & CInt(screenwidth / rw("Col") / IIf(Convert.ToString(rw1("Remarks")) = "H", intWcnt, 1)) & "px; height: " & CInt(screenHeight / rw("Rws") / IIf(Convert.ToString(rw1("Remarks")) = "V", intWcnt, 1)) & "px;"" id=""" & rw1("Code") & """></div>"))
                                    Catch ex As Exception
                                    End Try
                                End If
                                If intWcnt > 1 And Convert.ToString(rw1("Remarks")) = "H" Then
                                    tb1.Controls.Add(New LiteralControl("</td>"))
                                End If
                            Next
                            If intWcnt = 1 Then
                                tb1.Controls.Add(New LiteralControl("</td>"))
                            End If
                            If intWcnt > 1 And Convert.ToString(Widgetcls.DataSet.Tables(0).Rows(0)("Remarks")) = "V" Then
                                tb1.Controls.Add(New LiteralControl("</td>"))
                            End If
                            tb1.Controls.Add(New LiteralControl("</tr>"))
                            tb1.Controls.Add(New LiteralControl("</table>"))
                        End If
                        tb1.Controls.Add(New LiteralControl("</td>"))
                    Next x
                    tb1.Controls.Add(New LiteralControl("</tr>"))
                Next i
                tb1.Controls.Add(New LiteralControl("</table>"))
                tb1.Text = IIf(ProfileCls.CurrentLanguage() = "Ar", rw("ArbName"), rw("EngName"))
                WebTab1.Tabs.Add(tb1)
            Next
        End If
    End Sub
End Class
