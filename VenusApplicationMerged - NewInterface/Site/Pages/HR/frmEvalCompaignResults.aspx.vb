Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmEvalCompaignResults
    Inherits MainPage

#Region "Public Decleration"

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsEvaluationCompaign As New ClsEval_EvaluationCompaign(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEvaluationCompaign.ConnectionString)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim clsSearchsColumns = New Clssys_SearchsColumns(Page)
        Dim SearchID As Integer = 0
        If ClsObjects.Find(" Code='" & clsEvaluationCompaign.Table.Trim & "'") Then
            If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                SearchID = ClsSearchs.ID
            End If
        End If
        If Not IsPostBack Then
            clsEvaluationCompaign.GetDropDownList(ddlCompaign, True)
        End If
    End Sub
    Protected Sub ImageButton_Print_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton_Print.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub
    Protected Sub ddlCompaign_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompaign.SelectedIndexChanged
        uwgInterviewsDetail0.Rows.Clear()
        If ddlCompaign.SelectedValue <> 0 Then
            Dim clsEvaluationCompaign As New ClsEval_EvaluationCompaign(Page)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEvaluationCompaign.ConnectionString)

            Dim DS1 As New Data.DataSet()
            DS1.Clear()
            For x As Integer = 0 To DS1.Tables.Count - 1
                DS1.Tables(x).Constraints.Clear()
            Next
            DS1.Relations.Clear()
            DS1.Tables.Clear()
            Dim connetionString As String
            Dim connection As Data.SqlClient.SqlConnection
            Dim command As Data.SqlClient.SqlCommand
            Dim adapter As New Data.SqlClient.SqlDataAdapter
            connetionString = clsEvaluationCompaign.ConnectionString
            connection = New Data.SqlClient.SqlConnection(connetionString)
            clsEvaluationCompaign.Find("ID = " & ddlCompaign.SelectedValue)
            If clsEvaluationCompaign.CountType <> "Avg" Then
                Dim Str As String = "select distinct Evaluated_ID,EngName,ArbName,EvalCompaign_ID,(isnull((select SUM(Garde) from Eval_EvaluationCompaignDetail2 B where B.EvalCompaign_ID = A.EvalCompaign_ID and B.Evaluated_ID = A.Evaluated_ID),0)/ isnull((select count(ID) from Eval_EvaluationCompaignDetail2 B where B.EvalCompaign_ID = A.EvalCompaign_ID and B.Evaluated_ID = A.Evaluated_ID),0)) AS Grad from Eval_EvaluationCompaignDetail2 A" & _
                                    " where EvalCompaign_ID = " & clsEvaluationCompaign.ID & " Order By Evaluated_ID ASC"
                command = New Data.SqlClient.SqlCommand(Str, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS1, "Table1")
                adapter.Dispose()
                command.Dispose()
                connection.Close()

                uwgInterviewsDetail0.Bands(0).Columns(1).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                uwgInterviewsDetail0.Bands(0).AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgInterviewsDetail0.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
                uwgInterviewsDetail0.DataSource = DS1
                uwgInterviewsDetail0.DataBind()
            Else
                Dim Str As String = "select distinct Evaluated_ID,EngName,ArbName,EvalCompaign_ID,isnull((select SUM(Garde) from Eval_EvaluationCompaignDetail2 B where B.EvalCompaign_ID = A.EvalCompaign_ID and B.Evaluated_ID = A.Evaluated_ID),0) AS Grad from Eval_EvaluationCompaignDetail2 A" & _
                                    " where EvalCompaign_ID = " & clsEvaluationCompaign.ID & " Order By Evaluated_ID ASC"

                Dim Str1 As String = "select distinct B.Evaluated_ID,A.Evaluator_ID,A.EngName,A.ArbName,B.Garde from Eval_EvaluationCompaignDetail1 A left outer join Eval_EvaluationCompaignDetail2 B on A.EvalCompaign_ID = B.EvalCompaign_ID and A.Evaluator_ID = B.Evaluator_ID" & _
                                     " where A.EvalCompaign_ID = " & clsEvaluationCompaign.ID & " Order By Evaluated_ID ASC"

                command = New Data.SqlClient.SqlCommand(Str, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS1, "Table1")
                command = New Data.SqlClient.SqlCommand(Str1, connection)
                adapter.SelectCommand = command
                adapter.Fill(DS1, "Table2")
                adapter.Dispose()
                command.Dispose()
                connection.Close()
                Dim DataCol1 As Data.DataColumn
                Dim DataCol2 As Data.DataColumn
                DataCol1 = DS1.Tables(0).Columns("Evaluated_ID")
                DataCol2 = DS1.Tables(1).Columns("Evaluated_ID")
                Dim Rel1 As Data.DataRelation = New Data.DataRelation("Rel1", DataCol1, DataCol2, False)
                DS1.Relations.Add(Rel1)

                uwgInterviewsDetail0.Bands(0).Columns(1).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                uwgInterviewsDetail0.Bands(1).Columns(2).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                uwgInterviewsDetail0.Bands(0).AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgInterviewsDetail0.Bands(1).AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgInterviewsDetail0.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
                uwgInterviewsDetail0.DataSource = DS1
                uwgInterviewsDetail0.DataBind()
            End If
        End If
    End Sub
    Protected Sub uwgInterviewsDetail0_InitializeGroupByRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgInterviewsDetail0.InitializeGroupByRow
        e.Row.Expand(True)
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        If txtCode.Text = "" Then
            Return
        End If
        Dim clsEvaluationCompaign As New ClsEval_EvaluationCompaign(Page)
        clsEvaluationCompaign.Find(" Code='" & txtCode.Text & "'")
        ddlCompaign.SelectedValue = Convert.ToString(clsEvaluationCompaign.ID)
        ddlCompaign_SelectedIndexChanged(Nothing, Nothing)
        txtCode.Text = ""
    End Sub

#End Region

End Class
