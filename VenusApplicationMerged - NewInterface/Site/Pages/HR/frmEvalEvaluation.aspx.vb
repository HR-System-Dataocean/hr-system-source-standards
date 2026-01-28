Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class Interfaces_frmEvalEvaluation
    Inherits System.Web.UI.Page

#Region "Public Decleration"

#End Region

#Region "Protected Sub"

    '========================================================================
    'ProcedureName  :  InitializeCulture 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  
    '                1-Get language form Cookies 
    '                2-Create Calture from the incoming language 
    '                3-Apply the calture on the current form
    '                4-
    'Developer         : DataOcean
    'Date Created      : 01-08-07
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'sender             :
    'e                  :
    '========================================================================

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

    '========================================================================
    'Created by : Tamer Gouda
    'Date : 11/07/2007
    'Description :  
    '               -Call SetSetting function to Get the values of selected ID
    '               -Call SetScreenInfromation function
    '========================================================================

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ConnString As String = ConfigurationManager.AppSettings(0).ToString()
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnString)
        Venus.Shared.Web.ClientSideActions.SetLanguage(Page, TextBox_ArNotes, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
        Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
        Try
            If IsPostBack = False Then
                HiddenField_EvaluationID.Value = Request.QueryString("EvalID").ToString()
                Dim FillEval As String = "select A.ID,A.EvalTypeID,A.Dest_EmployeeID,A.Target_EmployeeID,A.EngNotes,A.ArbNotes,ISNULL(A.OverAllResult,0) AS OverAllResult," & _
                                         "       B.EngName,B.ArbName,B.EPower,A.RegComputerID,GEndDate," & _
                                         "       isnull((C.ArbName + ' ' + C.FamilyArbName + ' ' + C.FatherArbName + ' ' + C.GrandArbName) , (C.EngName + ' ' + C.FamilyEngName + ' ' + C.FatherEngName + ' ' + C.GrandEngName))AS DestAName ," & _
                                         "       isnull((C.EngName + ' ' + C.FamilyEngName + ' ' + C.FatherEngName + ' ' + C.GrandEngName),(C.ArbName + ' ' + C.FamilyArbName + ' ' + C.FatherArbName + ' ' + C.GrandArbName))AS DestEName ," & _
                                         "       isnull((D.ArbName + ' ' + D.FamilyArbName + ' ' + D.FatherArbName + ' ' + D.GrandArbName) , (D.EngName + ' ' + D.FamilyEngName + ' ' + D.FatherEngName + ' ' + D.GrandEngName))AS TargAName ," & _
                                         "       isnull((D.EngName + ' ' + D.FamilyEngName + ' ' + D.FatherEngName + ' ' + D.GrandEngName),(D.ArbName + ' ' + D.FamilyArbName + ' ' + D.FatherArbName + ' ' + D.GrandArbName))AS TargEName, " & _
                                         "       isnull((E.ArbName + ' ' + E.FamilyArbName + ' ' + E.FatherArbName + ' ' + E.GrandArbName) , (E.EngName + ' ' + E.FamilyEngName + ' ' + E.FatherEngName + ' ' + E.GrandEngName))AS TargANameBio ," & _
                                         "       isnull((E.EngName + ' ' + E.FamilyEngName + ' ' + E.FatherEngName + ' ' + E.GrandEngName),(E.ArbName + ' ' + E.FamilyArbName + ' ' + E.FatherArbName + ' ' + E.GrandArbName))AS TargENameBio " & _
                                         " from  Eval_Evaluation A " & _
                                         "       left outer join Eval_EvaluationTypes B on A.EvalTypeID = B.ID" & _
                                         "       left outer join hrs_Employees C on A.Dest_EmployeeID = C.ID " & _
                                         "       left outer join hrs_Employees D on A.Target_EmployeeID = D.ID" & _
                                         "       left outer join Rec_BioGraphies E on A.Target_EmployeeID = E.ID" & _
                                         " where A.ID = " & HiddenField_EvaluationID.Value

                Dim DTFillEval As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, FillEval).Tables(0)
                If Convert.ToDateTime(DTFillEval.Rows(0)("GEndDate").ToString()).AddDays(1) < Convert.ToDateTime(DateTime.Now.ToString()) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "This Evaluation Is Not Valid !/!åÐÇ ÇáÊÞííã áã íÚÏ ÕÇáÍ"))
                    Button_Save.Enabled = False
                    Button_Reload.Enabled = False
                    uwgEvaluation.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                    DropDownList_Recomm.Enabled = False
                    DropDownList_SPoints.Enabled = False
                    DropDownList_WPoints.Enabled = False
                End If

                Label_Evaluationtype.Text = DTFillEval.Rows(0)(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")).ToString()
                Label_Dest_EmployeeID.Text = DTFillEval.Rows(0)(ObjNavigationHandler.SetLanguage(Page, "DestEName/DestAName")).ToString()

                If DTFillEval.Rows(0)("RegComputerID").ToString() = "2" Then
                    Label_Target_EmployeeID.Text = DTFillEval.Rows(0)(ObjNavigationHandler.SetLanguage(Page, "TargENameBio/TargANameBio")).ToString()
                ElseIf DTFillEval.Rows(0)("RegComputerID").ToString() = "3" Then
                    Dim strgetExter As String = "select * from Eval_EvaluationCompaignDetail1 where Evaluator_ID = " & DTFillEval.Rows(0)("Dest_EmployeeID").ToString()
                    Dim DTgetExter As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, strgetExter).Tables(0)
                    Label_Dest_EmployeeID.Text = DTgetExter.Rows(0)(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")).ToString()
                    Label_Target_EmployeeID.Text = DTFillEval.Rows(0)(ObjNavigationHandler.SetLanguage(Page, "TargEName/TargAName")).ToString()
                Else
                    Label_Target_EmployeeID.Text = DTFillEval.Rows(0)(ObjNavigationHandler.SetLanguage(Page, "TargEName/TargAName")).ToString()
                End If
                TextBox_EnNotes.Text = DTFillEval.Rows(0)("EngNotes").ToString()
                TextBox_ArNotes.Text = DTFillEval.Rows(0)("ArbNotes").ToString()
                Label_OvResult.Text = DTFillEval.Rows(0)("OverAllResult").ToString()
                Label_EvalPower.Text = DTFillEval.Rows(0)("EPower").ToString()
                HiddenField_EvalTypeID.Value = DTFillEval.Rows(0)("EvalTypeID").ToString()

                Dim FillScale As String = "select EngName,ArbName from Eval_EvalScales where EvalTypeID = " & HiddenField_EvalTypeID.Value & " and " & DTFillEval.Rows(0)("OverAllResult").ToString() & " between DFrom and DTo"
                Dim DTFillScale As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, FillScale).Tables(0)
                Label_ResultScale.Text = ""
                If DTFillScale.Rows.Count > 0 Then
                    If Label_OvResult.Text <> 0 Then
                        Label_ResultScale.Text = DTFillScale.Rows(0)(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")).ToString()
                    Else
                        Label_ResultScale.Text = ""
                    End If
                End If

                Dim str As String = " select A.ID AS MainID,A.Dest_EmployeeID,A.Target_EmployeeID,A.EvalTypeID," & _
                        " B.ID AS AnotherID,B.Group_ID,B.GPower,B.RegComputerID AS Rank1," & _
                        " C.ArbName  AS GroupAName,C.EngName AS GroupEName," & _
                        " E.QuestionID,E.QPower AS QPower,E.RegComputerID AS Rank2," & _
                        " F.Question_Arb AS QuestionAName,F.Question_Eng AS QuestionEName,F.QPower,F.AnswerType,F.[Status]," & _
                        " G.ID,G.Answer_Arb AS AnswerAName,G.Answer_Eng AS AnswerEName,G.APower,G.RegComputerID AS Rank3" & _
                        " from                   Eval_Evaluation A " & _
                        " left outer join Eval_EvalTypeGroups B on A.EvaltypeID = B.EvaltypeID" & _
                        " left outer join Eval_Groups C on B.Group_ID = C.ID" & _
                        "  left outer join Eval_EvalTypeGroupQuestions E on B.Group_ID = E.Group_ID and A.EvaltypeID = E.EvaltypeID" & _
                        " left outer join Eval_Questions F on E.QuestionID = F.ID" & _
                        " left outer join Eval_QuestionsDetail1 G on E.QuestionID = G.QuestionID" & _
                        " where G.ID is not null and A.ID = " & HiddenField_EvaluationID.Value & " and F.[Status] = 1 Order by Rank1,Rank2,Rank3 Asc"

                uwgEvaluation.Columns(5).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "GroupEName/GroupAName")
                uwgEvaluation.Columns(6).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "QuestionEName/QuestionAName")
                uwgEvaluation.Columns(7).BaseColumnName = ObjNavigationHandler.SetLanguage(Page, "AnswerEName/AnswerAName")
                Dim DtStr As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, str).Tables(0)
                uwgEvaluation.DataSource = DtStr.DefaultView
                uwgEvaluation.DataBind()
                uwgEvaluation.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.OutlookGroupBy

                Dim FillHis As String = "select GroupID,QuestionID,AnswerID from Eval_EvalQuestions where EvalID = " & HiddenField_EvaluationID.Value
                Dim DtFillHis As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, FillHis).Tables(0)
                If DtFillHis.Rows.Count > 0 Then
                    For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEvaluation.Rows
                        Dim i As Integer = 0
                        For i = 0 To DtFillHis.Rows.Count - 1
                            If (DtFillHis.Rows(i)(0).ToString() = DGRow.Cells(0).Value.ToString()) And (DtFillHis.Rows(i)(1).ToString() = DGRow.Cells(1).Value.ToString()) And (DtFillHis.Rows(i)(2).ToString() = DGRow.Cells(2).Value.ToString()) Then
                                DGRow.Cells.FromKey("Check").Value = 1
                            End If
                        Next i
                    Next
                End If

                uwgEvaluation.DisplayLayout.Bands(0).Columns(5).IsGroupByColumn = True
                uwgEvaluation.DisplayLayout.Bands(0).Columns(6).IsGroupByColumn = True

                Dim FillRecomm As String = "select PointsEnglish,PointsArabic from Eval_EvaluationPoints where Type = 1 and EvalID = " & HiddenField_EvaluationID.Value
                Dim DtFillRecomm As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, FillRecomm).Tables(0)
                uwgPointsRecomm.DataSource = DtFillRecomm
                uwgPointsRecomm.DataBind()

                Dim FillSPoints As String = "select PointsEnglish,PointsArabic from Eval_EvaluationPoints where Type = 2 and EvalID = " & HiddenField_EvaluationID.Value
                Dim DtFillSPoints As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, FillSPoints).Tables(0)
                uwgSPoints.DataSource = DtFillSPoints
                uwgSPoints.DataBind()

                Dim FillWPoints As String = "select PointsEnglish,PointsArabic from Eval_EvaluationPoints where Type = 3 and EvalID = " & HiddenField_EvaluationID.Value
                Dim DtFillWPoints As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, FillWPoints).Tables(0)
                uwgWPoints.DataSource = DtFillWPoints
                uwgWPoints.DataBind()


                Dim GetRecomm As String = "select ID,EngName,ArbName from Eval_Recommendations where CancelDate is null"
                Dim DtGetRecomm As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, GetRecomm).Tables(0)
                Dim newRow As Data.DataRow = DtGetRecomm.NewRow()
                newRow("ID") = 0
                newRow("EngName") = "[Select your choise]"
                newRow("ArbName") = "[برجاء الاختيار]"
                DtGetRecomm.Rows.InsertAt(newRow, 0)
                DropDownList_Recomm.DataSource = DtGetRecomm.DefaultView
                DropDownList_Recomm.DataTextField = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                DropDownList_Recomm.DataValueField = "ID"
                DropDownList_Recomm.DataBind()

                Dim GetSPoints As String = "select ID,EngName,ArbName from Eval_SPointsList where CancelDate is null"
                Dim DtGetSPoints As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, GetSPoints).Tables(0)
                newRow = DtGetSPoints.NewRow()
                newRow("ID") = 0
                newRow("EngName") = "[Select your choise]"
                newRow("ArbName") = "[برجاء الاختيار]"
                DtGetSPoints.Rows.InsertAt(newRow, 0)
                DropDownList_SPoints.DataSource = DtGetSPoints.DefaultView
                DropDownList_SPoints.DataTextField = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                DropDownList_SPoints.DataValueField = "ID"
                DropDownList_SPoints.DataBind()

                Dim GetWPoints As String = "select ID,EngName,ArbName from Eval_WPointsList where CancelDate is null"
                Dim DtGetWPoints As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, GetWPoints).Tables(0)
                newRow = DtGetWPoints.NewRow()
                newRow("ID") = 0
                newRow("EngName") = "[Select your choise]"
                newRow("ArbName") = "[برجاء الاختيار]"
                DtGetWPoints.Rows.InsertAt(newRow, 0)
                DropDownList_WPoints.DataSource = DtGetWPoints.DefaultView
                DropDownList_WPoints.DataTextField = ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")
                DropDownList_WPoints.DataValueField = "ID"
                DropDownList_WPoints.DataBind()
            End If
        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, "", Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Protected Sub UltraWebGrid1_InitializeGroupByRow(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.RowEventArgs) Handles uwgEvaluation.InitializeGroupByRow
        e.Row.Expand(True)
    End Sub

#End Region

    Protected Sub Button_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_Save.Click
        Dim ConnString As String = ConfigurationManager.AppSettings(0).ToString()
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnString)
        Dim UPSTRING As String = "update Eval_Evaluation set EngNotes = '" & TextBox_EnNotes.Text & "',ArbNotes = '" & TextBox_ArNotes.Text & "',OverAllResult = " & Label_OvResult.Text & " where ID = " & HiddenField_EvaluationID.Value
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnString, Data.CommandType.Text, UPSTRING)

        Dim UPSTRINGBio As String = "update Rec_InterviewsDetail2 set Garde = " & Label_OvResult.Text & " where Evaluation = " & HiddenField_EvaluationID.Value
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnString, Data.CommandType.Text, UPSTRINGBio)

        Dim UPSTRINGCompa As String = "update Eval_EvaluationCompaignDetail2 set Garde = " & Label_OvResult.Text & " where Evaluation = " & HiddenField_EvaluationID.Value
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnString, Data.CommandType.Text, UPSTRINGCompa)

        Dim DeletePoints As String = "Delete From Eval_EvaluationPoints where EvalID = " & HiddenField_EvaluationID.Value
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnString, Data.CommandType.Text, DeletePoints)

        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgPointsRecomm.Rows
            If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                Continue For
            End If
            Dim InserRecomm As String = "insert into Eval_EvaluationPoints (EvalID,PointsArabic,PointsEnglish,Type) values (" & HiddenField_EvaluationID.Value & ",'" & DGRow.Cells(2).Value & "','" & DGRow.Cells(1).Value & "',1)"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnString, Data.CommandType.Text, InserRecomm)
        Next

        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgSPoints.Rows
            If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                Continue For
            End If
            Dim InserSPoints As String = "insert into Eval_EvaluationPoints (EvalID,PointsArabic,PointsEnglish,Type) values (" & HiddenField_EvaluationID.Value & ",'" & DGRow.Cells(2).Value & "','" & DGRow.Cells(1).Value & "',2)"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnString, Data.CommandType.Text, InserSPoints)
        Next

        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgWPoints.Rows
            If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                Continue For
            End If
            Dim InserWPoints As String = "insert into Eval_EvaluationPoints (EvalID,PointsArabic,PointsEnglish,Type) values (" & HiddenField_EvaluationID.Value & ",'" & DGRow.Cells(2).Value & "','" & DGRow.Cells(1).Value & "',3)"
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnString, Data.CommandType.Text, InserWPoints)
        Next

        Dim DeleteEvalQuestions As String = "Delete From Eval_EvalQuestions where EvalID = " & HiddenField_EvaluationID.Value
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnString, Data.CommandType.Text, DeleteEvalQuestions)
        Dim InsertEvalQuestions As String = "insert into Eval_EvalQuestions (GroupID,QuestionID,EvalID,AnswerID) select B.Group_ID,E.QuestionID,A.ID,null " & _
                                            " from                   Eval_Evaluation A " & _
                                            " left outer join Eval_EvalTypeGroups B on A.EvaltypeID = B.EvaltypeID" & _
                                            " left outer join Eval_Groups C on B.Group_ID = C.ID" & _
                                            " left outer join Eval_EvalTypeGroupQuestions E on B.Group_ID = E.Group_ID and A.EvaltypeID = E.EvaltypeID" & _
                                            " left outer join Eval_Questions F on E.QuestionID = F.ID" & _
                                            " where A.ID = " & HiddenField_EvaluationID.Value & " and F.[Status] = 1"
        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnString, Data.CommandType.Text, InsertEvalQuestions)
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEvaluation.Rows
            If DGRow.HasChildRows Then
                For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DGRow.Rows
                    InsertEvalQuestions = InsertEvalQuestions & Environment.NewLine & " insert into Eval_EvalQuestions (GroupID,QuestionID,EvalID,AnswerID) values (" & DGRow1.Cells(0).Value & "," & DGRow1.Cells(1).Value & "," & HiddenField_EvaluationID.Value & "," & DGRow1.Cells(2).Value & ")"
                    If DGRow1.HasChildRows Then
                        For Each DGRow2 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DGRow1.Rows
                            If DGRow2.Cells.FromKey("Check").Value = 1 Then
                                Dim CGrp As String = DGRow2.Cells(0).Value
                                Dim CQst As String = DGRow2.Cells(1).Value
                                Dim CAns As String = DGRow2.Cells(2).Value
                                Dim UpdateEvalQuestions As String = "update Eval_EvalQuestions set AnswerID = " & CAns & " where GroupID = " & CGrp & " and QuestionID = " & CQst & " and EvalID = " & HiddenField_EvaluationID.Value
                                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnString, Data.CommandType.Text, UpdateEvalQuestions)
                            End If
                        Next
                    End If
                Next
            End If
        Next
        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Done !/!Êã ÇáÍÝÙ"))
    End Sub
    Protected Sub DropDownList_Recomm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList_Recomm.SelectedIndexChanged
        If DropDownList_Recomm.SelectedIndex > 0 Then
            Dim ConnString As String = ConfigurationManager.AppSettings(0).ToString()
            Dim GetRecomm As String = "select ID,EngName,ArbName from Eval_Recommendations where ID = " & DropDownList_Recomm.SelectedValue
            Dim DtGetRecomm As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, GetRecomm).Tables(0)
            If DtGetRecomm.Rows.Count > 0 Then
                uwgPointsRecomm.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {"0", DtGetRecomm.Rows(0)("EngName"), DtGetRecomm.Rows(0)("ArbName")}))
            End If
        End If
        DropDownList_Recomm.SelectedIndex = 0
    End Sub
    Protected Sub DropDownList_SPoints_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList_Recomm.SelectedIndexChanged, DropDownList_SPoints.SelectedIndexChanged
        If DropDownList_SPoints.SelectedIndex > 0 Then
            Dim ConnString As String = ConfigurationManager.AppSettings(0).ToString()
            Dim GetRecomm As String = "select ID,EngName,ArbName from Eval_SPointsList where ID = " & DropDownList_SPoints.SelectedValue
            Dim DtGetRecomm As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, GetRecomm).Tables(0)
            If DtGetRecomm.Rows.Count > 0 Then
                uwgSPoints.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {"0", DtGetRecomm.Rows(0)("EngName"), DtGetRecomm.Rows(0)("ArbName")}))
            End If
        End If
        DropDownList_SPoints.SelectedIndex = 0
    End Sub
    Protected Sub DropDownList_WPoints_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList_Recomm.SelectedIndexChanged, DropDownList_WPoints.SelectedIndexChanged
        If DropDownList_WPoints.SelectedIndex > 0 Then
            Dim ConnString As String = ConfigurationManager.AppSettings(0).ToString()
            Dim GetRecomm As String = "select ID,EngName,ArbName from Eval_WPointsList where ID = " & DropDownList_WPoints.SelectedValue
            Dim DtGetRecomm As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, GetRecomm).Tables(0)
            If DtGetRecomm.Rows.Count > 0 Then
                uwgWPoints.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {"0", DtGetRecomm.Rows(0)("EngName"), DtGetRecomm.Rows(0)("ArbName")}))
            End If
        End If
        DropDownList_WPoints.SelectedIndex = 0
    End Sub

    Protected Sub uwgEvaluation_ActiveCellChange(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgEvaluation.ActiveCellChange
        Dim ConnString As String = ConfigurationManager.AppSettings(0).ToString()
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ConnString)
        If e.Cell.Key = "Check" Then
            Dim Grp As String = e.Cell.Row.Cells.FromKey("Group_ID").Value()
            Dim Qst As String = e.Cell.Row.Cells.FromKey("QuestionID").Value
            Dim Ans As String = e.Cell.Row.Cells.FromKey("ID").Value

            Dim CurrPower As Double = 0
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgEvaluation.Rows
                If DGRow.HasChildRows Then
                    For Each DGRow1 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DGRow.Rows
                        If DGRow1.HasChildRows Then
                            For Each DGRow2 As Infragistics.WebUI.UltraWebGrid.UltraGridRow In DGRow1.Rows
                                Dim CGrp As String = DGRow2.Cells(0).Value
                                Dim CQst As String = DGRow2.Cells(1).Value
                                Dim CAns As String = DGRow2.Cells(2).Value
                                Dim Queststr As String = "select ISNULL(SUM(QPower),0) from Eval_EvalTypeGroupQuestions where EvaltypeID = " & HiddenField_EvalTypeID.Value & " and Group_ID = " & CGrp
                                Dim AllQuestions As Double = Convert.ToDouble(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnString, Data.CommandType.Text, Queststr))
                                Dim Grpstr As String = "select ISNULL(SUM(GPower),0) from Eval_EvalTypeGroups where EvaltypeID = " & HiddenField_EvalTypeID.Value
                                Dim AllGroups As Double = Convert.ToDouble(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnString, Data.CommandType.Text, Grpstr))

                                If Grp <> CGrp Or Qst <> CQst Then
                                    If DGRow2.Cells.FromKey("Check").Value = 1 Then
                                        Dim AGroupPower As Double = DGRow2.Cells.FromKey("GPower").Value / AllGroups
                                        Dim AQuestionPower As Double = DGRow2.Cells.FromKey("QPower").Value / AllQuestions * AGroupPower
                                        Dim AAnswerPower As Double = AQuestionPower * DGRow2.Cells.FromKey("APower").Value / 100
                                        CurrPower = CurrPower + (AAnswerPower * Convert.ToInt32(Label_EvalPower.Text))
                                    End If
                                    Continue For
                                ElseIf Ans = CAns Then
                                    If DGRow2.Cells.FromKey("Check").Value = 1 Then
                                        Dim AGroupPower As Double = DGRow2.Cells.FromKey("GPower").Value / AllGroups
                                        Dim AQuestionPower As Double = DGRow2.Cells.FromKey("QPower").Value / AllQuestions * AGroupPower
                                        Dim AAnswerPower As Double = AQuestionPower * DGRow2.Cells.FromKey("APower").Value / 100
                                        CurrPower = CurrPower + (AAnswerPower * Convert.ToInt32(Label_EvalPower.Text))
                                    End If
                                    Continue For
                                Else
                                    DGRow2.Cells.FromKey("Check").Value = 0
                                End If
                            Next
                        End If
                    Next
                End If
            Next
            Try
                Label_OvResult.Text = Convert.ToInt32(CurrPower).ToString()
            Catch ex As Exception
                Label_OvResult.Text = "0"
            End Try

            Dim FillScale As String = "select EngName,ArbName from Eval_EvalScales where EvalTypeID = " & HiddenField_EvalTypeID.Value & " and " & Label_OvResult.Text & " between DFrom and DTo"
            Dim DTFillScale As Data.DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnString, Data.CommandType.Text, FillScale).Tables(0)
            Label_ResultScale.Text = ""
            If DTFillScale.Rows.Count > 0 Then
                If Label_OvResult.Text <> 0 Then
                    Label_ResultScale.Text = DTFillScale.Rows(0)(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName")).ToString()
                Else
                    Label_ResultScale.Text = ""
                End If
            End If
            e.Cell.Row.Cells.FromKey("AnswerEName").Activated = True
        End If
    End Sub
End Class
