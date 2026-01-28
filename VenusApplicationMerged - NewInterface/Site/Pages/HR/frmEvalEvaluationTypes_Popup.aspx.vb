Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data

Partial Class frmEvalEvaluationTypes_Popup
    Inherits MainPage

#Region "Public Decleration"
    Private ClsEvalEvaluationTypes As ClsEval_EvaluationTypes
    Private ClsEvalGroups As ClsEval_Groups
    Private ClsEvalEvalScales As ClsEval_EvalScales
    Private ClsEvalEvalTypeGroups As ClsEval_EvalTypeGroups
    Private ClsEvalTypeGroupQuestions As ClsEval_EvalTypeGroupQuestions
    Private ClsEvaluationTypesModule As ClsEval_EvaluationTypesModule
#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim StrMode As String = Request.QueryString.Item("MD")
            If Not IsPostBack Then
                If StrMode = "SC" Then
                    Div1.Visible = True
                ElseIf StrMode = "MO" Then
                    Div2.Visible = True
                ElseIf StrMode = "CH" Then
                    Div3.Visible = True
                End If
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                CheckID()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Try
            ClsEvalEvaluationTypes = New ClsEval_EvaluationTypes(Me)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvalEvaluationTypes.ConnectionString)
            Dim StrEvalTypeID As String = Request.QueryString.Item("ETID")
            SaveDG(StrEvalTypeID)
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub uwgGroups_ActiveRowChange(sender As Object, e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgGroups.ActiveRowChange
        HideDetailsRows(e.Row.Cells(1).Value)
    End Sub

#Region "Private Functions"
    Private Sub HideDetailsRows(ByVal intGroupID As Integer)
        For Each row As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGroupsQuestions.Rows()
            If row.Cells(1).Value = intGroupID Then
                row.Hidden = False
            Else
                row.Hidden = True
            End If
        Next
    End Sub
    Private Function CheckID() As Boolean
        Dim StrEvalTypeID As String = Request.QueryString.Item("ETID")
        ClsEvalEvaluationTypes = New ClsEval_EvaluationTypes(Me)
        ClsEvalGroups = New ClsEval_Groups(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvalEvaluationTypes.ConnectionString)
        Try
            ClsEvalEvaluationTypes.Find("ID=" & StrEvalTypeID & "")
            If ClsEvalEvaluationTypes.ID > 0 Then
                GetValues()
            Else
                uwgGroupsQuestions.Columns(3).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "Question_Eng/Question_Arb")
                uwgGroups.Columns(2).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                ClsEvalGroups.Find("CancelDate is null and ID in (select GroupID from Eval_GroupsQuestions where QuestionID in (select ID from Eval_Questions where CancelDate is null and Status = 1))")
                uwgGroups.DataSource = ClsEvalGroups.DataSet.Tables(0).DefaultView
                uwgGroups.DataBind()

                uwgModules.Columns(1).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                Dim strgetmenu As String = "select * from sys_Modules where CancelDate is null"
                Dim DTMenu As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvalGroups.ConnectionString, Data.CommandType.Text, strgetmenu).Tables(0)
                uwgModules.DataSource = DTMenu
                uwgModules.DataBind()
                Dim GroupsArr As String = "0"
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGroups.Rows()
                    GroupsArr = GroupsArr & "," & DGRow.Cells(1).Value
                Next
                Dim str As String = "select A.ID,A.GroupID,A.QuestionID,B.EngName,B.ArbName,B.Question_Eng,B.Question_Arb,B.QPower from " & _
                                    "Eval_GroupsQuestions A left outer join  Eval_Questions B on A.QuestionID = B.ID and B.CancelDate is null and A.GroupID in (" & GroupsArr & " ) "
                uwgGroupsQuestions.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvalGroups.ConnectionString, Data.CommandType.Text, str)
                uwgGroupsQuestions.DataBind()
                uwgGroups.DisplayLayout.ActiveRow = uwgGroups.Rows(0)
                uwgGroups.Rows(0).Selected = True
                HideDetailsRows(uwgGroups.Rows(0).Cells.FromKey("ID").Value)
                uwgEvalScales.Rows.Add()
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim clsSysMainOtherFields As New clsSys_MainOtherFields(Page)
        Dim ClsUser As New Clssys_Users(Page)
        ClsEvalEvalScales = New ClsEval_EvalScales(Page)
        ClsEvalGroups = New ClsEval_Groups(Page)
        ClsEvalEvalTypeGroups = New ClsEval_EvalTypeGroups(Page)
        ClsEvaluationTypesModule = New ClsEval_EvaluationTypesModule(Page)
        ClsEvalTypeGroupQuestions = New ClsEval_EvalTypeGroupQuestions(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEvalEvalScales.ConnectionString)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            With ClsEvalEvaluationTypes
                lblDescEmployeeCode.Text = .Code
                lblDescEnglishName.Text = ObjNavigationHandler.SetLanguage(Me, .EngName & "/" & .ArbName)

                ClsEvalEvalScales.Find("CancelDate is null and EvaltypeID = " & .ID)
                uwgEvalScales.DataSource = ClsEvalEvalScales.DataSet.Tables(0).DefaultView
                uwgEvalScales.DataBind()

                uwgGroupsQuestions.Columns(3).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "Question_Eng/Question_Arb")
                uwgGroups.Columns(2).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                ClsEvalGroups.Find("CancelDate is null and ID in (select GroupID from Eval_GroupsQuestions where QuestionID in (select ID from Eval_Questions where CancelDate is null and Status = 1))")
                uwgGroups.DataSource = ClsEvalGroups.DataSet.Tables(0).DefaultView
                uwgGroups.DataBind()

                ClsEvalEvalTypeGroups.Find("EvaltypeID = " & .ID)
                Dim DT As Data.DataTable = ClsEvalEvalTypeGroups.DataSet.Tables(0)
                Dim i As Integer
                For i = 0 To DT.Rows.Count - 1
                    For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGroups.Rows()
                        If DGRow.Cells(1).Value = DT.Rows(i)(2) Then
                            DGRow.Cells(0).Value = True
                            DGRow.Cells(3).Value = DT.Rows(i)(3)
                            DGRow.Cells(4).Value = DT.Rows(i)("RegComputerID")
                        End If
                    Next
                Next
                Dim GroupsArr As String = "0"
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGroups.Rows()
                    GroupsArr = GroupsArr & "," & DGRow.Cells(1).Value
                Next
                Dim str As String = "select A.ID,A.GroupID,A.QuestionID,B.EngName,B.ArbName,B.Question_Eng,B.Question_Arb,B.QPower from " & _
                    "Eval_GroupsQuestions A left outer join  Eval_Questions B on A.QuestionID = B.ID and B.CancelDate is null and A.GroupID in (" & GroupsArr & " ) "
                uwgGroupsQuestions.DataSource = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvalEvalTypeGroups.ConnectionString, Data.CommandType.Text, str)
                uwgGroupsQuestions.DataBind()
                ClsEvalTypeGroupQuestions.Find("EvaltypeID = " & .ID)
                Dim DT1 As Data.DataTable = ClsEvalTypeGroupQuestions.DataSet.Tables(0)
                Dim i1 As Integer
                For i1 = 0 To DT1.Rows.Count - 1
                    For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGroupsQuestions.Rows()
                        If DGRow.Cells(1).Value = DT1.Rows(i1)(2) And DGRow.Cells(2).Value = DT1.Rows(i1)(3) Then
                            DGRow.Cells(4).Value = DT1.Rows(i1)(4)
                            DGRow.Cells(5).Value = DT1.Rows(i1)("RegComputerID")
                        End If
                    Next
                Next
                uwgModules.Columns(1).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                Dim strgetmenu As String = "select * from sys_Modules where CancelDate is null"
                Dim DTMenu As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEvalGroups.ConnectionString, Data.CommandType.Text, strgetmenu).Tables(0)
                uwgModules.DataSource = DTMenu
                uwgModules.DataBind()
                ClsEvaluationTypesModule.Find("EvalType_ID = " & .ID)
                DT = ClsEvaluationTypesModule.DataSet.Tables(0)
                For i = 0 To DT.Rows.Count - 1
                    For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgModules.Rows
                        If DGRow.Cells(0).Value = DT.Rows(i)(2) Then
                            DGRow.Cells(2).Value = True
                        End If
                    Next
                Next
                uwgGroups.DisplayLayout.ActiveRow = uwgGroups.Rows(0)
                uwgGroups.Rows(0).Selected = True
                HideDetailsRows(uwgGroups.Rows(0).Cells.FromKey("ID").Value)
            End With
            uwgEvalScales.Rows.Add()
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function SaveDG(ByVal EvalTypeID As Integer) As Boolean
        Try
            ClsEvalTypeGroupQuestions = New ClsEval_EvalTypeGroupQuestions(Page)
            Dim str1 As String = "delete from Eval_EvalScales where EvalTypeID = " & EvalTypeID
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvalTypeGroupQuestions.ConnectionString, Data.CommandType.Text, str1)
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEvalScales.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If
                ClsEvalEvalScales = New ClsEval_EvalScales(Page)
                ClsEvalEvalScales.EvalTypeID = EvalTypeID
                ClsEvalEvalScales.EngName = DGRow.Cells(1).Value
                ClsEvalEvalScales.ArbName = DGRow.Cells(2).Value
                ClsEvalEvalScales.DFrom = DGRow.Cells(3).Value
                ClsEvalEvalScales.DTo = DGRow.Cells(4).Value
                ClsEvalEvalScales.Save()
            Next
            str1 = "delete from Eval_EvalTypeGroupQuestions where EvaltypeID = " & EvalTypeID
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvalTypeGroupQuestions.ConnectionString, Data.CommandType.Text, str1)
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGroups.Rows()
                ClsEvalEvalTypeGroups = New ClsEval_EvalTypeGroups(Page)
                Dim str As String = "delete from Eval_EvalTypeGroups where EvaltypeID = " & EvalTypeID & " and Group_ID = " & DGRow.Cells(1).Value
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvalEvalTypeGroups.ConnectionString, Data.CommandType.Text, str)
                If DGRow.Cells(0).Value = True Then
                    ClsEvalEvalTypeGroups.EvaltypeID = EvalTypeID
                    ClsEvalEvalTypeGroups.Group_ID = DGRow.Cells(1).Value
                    ClsEvalEvalTypeGroups.GPower = DGRow.Cells(3).Value
                    If DGRow.Cells.FromKey("RegComputerID").Value IsNot DBNull.Value Then
                        ClsEvalEvalTypeGroups.RegComputerID = DGRow.Cells(4).Value
                    End If
                    ClsEvalEvalTypeGroups.Save()
                    For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGroupsQuestions.Rows()
                        If DGRow.Cells(1).Value = DGRow1.Cells(1).Value Then
                            ClsEvalTypeGroupQuestions = New ClsEval_EvalTypeGroupQuestions(Page)
                            ClsEvalTypeGroupQuestions.EvaltypeID = EvalTypeID
                            ClsEvalTypeGroupQuestions.Group_ID = DGRow1.Cells(1).Value
                            ClsEvalTypeGroupQuestions.QuestionID = DGRow1.Cells(2).Value
                            ClsEvalTypeGroupQuestions.QPower = DGRow1.Cells(4).Value
                            If DGRow1.Cells.FromKey("RegComputerID").Value IsNot DBNull.Value Then
                                ClsEvalTypeGroupQuestions.RegComputerID = DGRow1.Cells.FromKey("RegComputerID").Value
                            End If
                            ClsEvalTypeGroupQuestions.Save()
                        End If
                    Next
                End If
            Next
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgModules.Rows()
                ClsEvaluationTypesModule = New ClsEval_EvaluationTypesModule(Page)
                Dim str As String = "delete from Eval_EvaluationTypesModule where Module_ID = " & DGRow.Cells(0).Value & " and EvalType_ID = " & EvalTypeID
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEvaluationTypesModule.ConnectionString, Data.CommandType.Text, str)
                If DGRow.Cells(2).Value = True Then
                    ClsEvaluationTypesModule.Module_ID = DGRow.Cells(0).Value
                    ClsEvaluationTypesModule.EvalType_ID = EvalTypeID
                    ClsEvaluationTypesModule.Save()
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
#End Region

End Class
