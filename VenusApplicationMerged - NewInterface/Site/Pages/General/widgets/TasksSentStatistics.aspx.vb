Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class TasksSentStatistics
    Inherits MainPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Literal1.Text = "<table cellpadding=""2"" cellspacing=""2"" style=""height: 240px; width:100%;"">"
        Literal1.Text += "<tr>"
        Literal1.Text += "<td style=""border: 1px solid silver;width: 100%; height: 240px;"">"

        Dim Pagescls As New Clsdsh_Pages(Me)
        Dim DDatatable As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Pagescls.ConnectionString, System.Data.CommandType.Text, "select COUNT(distinct A.ID) Rslt1,(select ArbName from hrs_Enum where Flag = 5 and Code = C.StatusID) NameArb,(select EngName from hrs_Enum where Flag = 5 and Code = C.StatusID) NameENg from sys_Tasks A inner join sys_TasksInvolve B on A.ID = B.TaskID inner join sys_TasksInvolveSteps C on B.ID = C.TaskInvolveID where C.StatusID in (5,6,7) and A.RegUserID = " & Pagescls.DataBaseUserRelatedID & " group by C.StatusID").Tables(0)
        Literal1.Text += "<script type=""text/javascript"">"
        Literal1.Text += "$(function () {"
        Literal1.Text += "$(document).ready(function () {"
        Literal1.Text += "$('#TaskSta').highcharts({"
        Literal1.Text += "chart: {plotBackgroundColor: null,plotBorderWidth: null,plotShadow: false},"
        Literal1.Text += "title: {text: '" & IIf(ProfileCls.CurrentLanguage() = "Ar", "إحصائيات الأوامر", "Tasks Chart") & "'},"
        Literal1.Text += "tooltip: {pointFormat: '{series.name} <b>{point.percentage:.1f}%</b>'},"
        Literal1.Text += "plotOptions: {"
        Literal1.Text += "pie: {allowPointSelect: true,cursor: 'pointer',dataLabels: {enabled: false},showInLegend: true,style: {color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'},connectorColor: 'silver'}},"
        Literal1.Text += "series: [{type: 'pie',name: ' ',data: ["
        For Each drow As Data.DataRow In DDatatable.Rows
            Literal1.Text += "['" & IIf(ProfileCls.CurrentLanguage() = "Ar", drow("NameArb"), drow("NameEng")) & "',    " & CInt(drow("Rslt1")) & "],"
        Next
        Literal1.Text += "]}]});});});"
        Literal1.Text += "</script>"
        Literal1.Text += "<div style=""width:100%; height: 240px;"" id=""TaskSta""></div>"

        Literal1.Text += "</td>"
        Literal1.Text += "</tr>"
        Literal1.Text += "</table>"
    End Sub
End Class
