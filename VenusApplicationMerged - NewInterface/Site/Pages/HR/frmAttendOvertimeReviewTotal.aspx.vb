Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.IO
Imports System.Data.OleDb

Partial Class frmAttendOvertimeReview
    Inherits MainPage
#Region "Public Decleration"
    Private ClsOvertimeReview As ClsAtt_OvertimeReview
    Private ClsEmployee As Clshrs_Employees
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsOvertimeReview = New ClsAtt_OvertimeReview(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
        Dim ClsNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsOvertimeReview.ConnectionString)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsOvertimeReview.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsOvertimeReview.ConnectionString)
                lblLage.Text = ClsNavigationHandler.SetLanguage(Page, "0/1")
                Page.Session.Add("Lang", lblLage.Text)

                ClsOvertimeReview.AddOnChangeEventToControls("frmAttendShifts", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                'Dim clsenum As New Clshrs_Enum(Me)
                'clsenum.GetList(uwgOvertimeReview.DisplayLayout.Bands(0).Columns(2).ValueList, False, "Flag = 2")
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsOvertimeReview.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsOvertimeReview.ID
                If (IntrecordID > 0) Then
                    SetScreenInformation("E")
                Else
                    SetScreenInformation("N")
                End If
            Else
                SetScreenInformation("N")
            End If
            CreateOtherFields(IntrecordID)
            If Not IsPostBack Then
                UltraWebTab1.SelectedTab = 0
                WebDateChooserFromdate.Value = Date.Now
            End If


        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsOvertimeReview.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsOvertimeReview = New ClsAtt_OvertimeReview(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsOvertimeReview.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsOvertimeReview.ID > 0 Then
                    ClsOvertimeReview.Update("code='" & txtCode.Text & "'")
                Else
                    ClsOvertimeReview.Save()
                End If
                ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")
                If ClsOvertimeReview.ID > 0 Then
                    If (SaveDG(ClsOvertimeReview.ID)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsOvertimeReview.Table, ClsOvertimeReview.ID)
                        value.Text = ""
                    End If
                End If
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsOvertimeReview.Table, ClsOvertimeReview.ID)
                value.Text = ""
                'AfterOperation()
                GetValues()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsOvertimeReview.ID > 0 Then
                    ClsOvertimeReview.Update("code='" & txtCode.Text & "'")
                Else
                    ClsOvertimeReview.Save()
                End If

                ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")

                If ClsOvertimeReview.ID > 0 Then
                    If (SaveDG(ClsOvertimeReview.ID)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsOvertimeReview.Table, ClsOvertimeReview.ID)
                        value.Text = ""
                    End If
                End If
                ' AfterOperation()
                GetValues()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))

            Case "New"
                AfterOperation()
            Case "Delete"
                ClsOvertimeReview.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsOvertimeReview.ID & "&TableName=" & ClsOvertimeReview.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsOvertimeReview.ID & "&TableName=" & ClsOvertimeReview.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsOvertimeReview.Table
                ClsOvertimeReview.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsOvertimeReview.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsOvertimeReview.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsOvertimeReview.Find(" Code= '" & txtCode.Text & "'")
                If ClsOvertimeReview.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsOvertimeReview.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsOvertimeReview.CheckDiff(ClsOvertimeReview, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsOvertimeReview.FirstRecord()
                GetValues()
            Case "Previous"
                ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")
                If Not ClsOvertimeReview.previousRecord() Then
                    ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")
                If Not ClsOvertimeReview.NextRecord() Then
                    ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsOvertimeReview.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function SaveDG(ByVal OvertimeReviewID As Integer) As Boolean
        Try

            Dim ClsAtt_OvertimeReviewDetail As New ClsAtt_OvertimeReviewDetail(Page)
            ClsEmployee = New Clshrs_Employees(Me)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsOvertimeReview.ConnectionString)
            'If WebDateChooserFromdate.Value > WebDateChooserTodate.Value Then
            '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /لا يمكن ان يكون تاريخ النهاية اكبر من تاريخ البداية"))
            '    Exit Function
            'End If
            If FileUpload1.HasFile = True Then
                LoadOverTime(OvertimeReviewID)


                GetValues()
                Return True
            Else
                If OvertimeReviewID > 0 Then
                    ClsEmployee.Find("Code='" & txtEmpCode.Text & "'")
                    If ClsEmployee.ID > 0 Then
                        If lblovertimedetailsid.Text <> "" Then
                            ClsAtt_OvertimeReviewDetail.Find("id=" & lblovertimedetailsid.Text)
                        End If
                        ClsAtt_OvertimeReviewDetail.OvertimeReviewID = OvertimeReviewID
                        ClsAtt_OvertimeReviewDetail.EmployeeID = ClsEmployee.ID
                        ClsAtt_OvertimeReviewDetail.TransDate = WebDateChooserFromdate.Value
                        ' ClsAtt_OvertimeReviewDetail.TransToDate = WebDateChooserTodate.Value
                        ClsAtt_OvertimeReviewDetail.Overtime = txtovertimeHours.Text
                        If ClsAtt_OvertimeReviewDetail.ID > 0 Then

                            ClsAtt_OvertimeReviewDetail.Update("id=" & ClsAtt_OvertimeReviewDetail.ID)
                        Else
                            ClsAtt_OvertimeReviewDetail.Save()
                        End If
                    End If

                    GetValues()
                    Return True
                End If
            End If

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function LoadOverTime(OvertimeReviewID As Integer) As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsOvertimeReview.ConnectionString)
        Dim ingr As Integer = 1
        Dim ClsAtt_OvertimeReviewDetail As New ClsAtt_OvertimeReviewDetail(Page)
        Try

            ClsEmployee = New Clshrs_Employees(Page)


            Dim strFileName As String = FileUpload1.FileName
            Dim fileExt As String
            Dim dwlPath As String = Request.PhysicalApplicationPath & "AttendFolder"
            Dim strFinalPath As String = String.Empty

            If strFileName.Trim <> String.Empty Then
                fileExt = System.IO.Path.GetExtension(strFileName).ToLower()
                If (fileExt.ToLower = ".xls" Or fileExt.ToLower = ".xlsx") Then
                    If Not Directory.Exists(dwlPath) Then Directory.CreateDirectory(dwlPath)
                    Dim DisFile As String = DateTime.Now.ToString("ddMMyyyyHHmmss") & "_" & strFileName
                    FileUpload1.SaveAs(dwlPath & "\" & DisFile)
                    strFinalPath = dwlPath & "\" & DisFile
                    Dim dsResult As New Data.DataSet

                    Dim oledbConn As New OleDbConnection()
                    If fileExt = ".xls" Then
                        oledbConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strFinalPath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
                    ElseIf fileExt = ".xlsx" Then
                        oledbConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strFinalPath & ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"""
                    End If
                    oledbConn.Open()
                    Dim cmd As New OleDbCommand()
                    cmd.CommandType = System.Data.CommandType.Text
                    Dim oleda As New OleDbDataAdapter()
                    Dim dt As Data.DataTable = oledbConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, Nothing)
                    Dim workSheetName As String = DirectCast(dt.Rows(0)("TABLE_NAME"), String)
                    cmd.Connection = oledbConn
                    cmd.CommandType = Data.CommandType.Text
                    cmd.CommandText = "SELECT * FROM [" & workSheetName & "]"
                    oleda = New OleDbDataAdapter(cmd)
                    oleda.Fill(dsResult)
                    oledbConn.Close()

                    For Each Row As Data.DataRow In dsResult.Tables(0).Rows
                        Try
                            ingr = ingr + 1
                            Dim commandString = ""
                            If Row("Code").ToString = "" Then
                                Continue For
                            End If

                            ClsEmployee.Find("Code= '" & Row("Code").ToString & "'")
                            If ClsEmployee.ID <> 0 Then
                                ClsAtt_OvertimeReviewDetail.Find("Employeeid=" & ClsEmployee.ID & " and TransDate=" & "'" & CDate(Row("DATE").ToString()) & "'")
                                If ClsAtt_OvertimeReviewDetail.ID > 0 Then
                                    ClsAtt_OvertimeReviewDetail.Delete("ID=" & ClsAtt_OvertimeReviewDetail.ID)
                                End If
                                ClsAtt_OvertimeReviewDetail = New ClsAtt_OvertimeReviewDetail(Page)
                                ClsAtt_OvertimeReviewDetail.OvertimeReviewID = OvertimeReviewID
                                ClsAtt_OvertimeReviewDetail.EmployeeID = ClsEmployee.ID
                                ClsAtt_OvertimeReviewDetail.TransDate = CDate(Row("DATE"))
                                ClsAtt_OvertimeReviewDetail.Overtime = Convert.ToDouble(Row("Overtime"))


                                'If ClsAtt_OvertimeReviewDetail.ID > 0 And ClsAtt_OvertimeReviewDetail.CancelDate = "#12:00:00 AM#" Then
                                '    ClsAtt_OvertimeReviewDetail.Update("id=" & ClsAtt_OvertimeReviewDetail.ID)
                                'Else
                                ClsAtt_OvertimeReviewDetail.Save()
                                'End If
                            End If
                        Catch ex As Exception
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /فشلت العملية فى السطر رقم " & ingr))
                            Exit Function
                        End Try
                    Next




                End If
            End If

        Catch ex As Exception


            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ex.Message)
        End Try
    End Function
    Private Function GetEmployee(ByVal Code As String, ByRef clsEmp As Clshrs_Employees) As Boolean
        Try

            clsEmp.Find("code= '" & code & "'")
            If clsEmp.ID <> 0 Then
                
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function AssignValues() As Boolean
        Try
            With ClsOvertimeReview
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues(Optional ByVal isdelete As Boolean = False) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsUser.ConnectionString)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim strcommand As String
        Try
            SetToolBarDefaults()
            With ClsOvertimeReview
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName

            End With
            If txtEmpCode.Text <> "" And isdelete = False Then
                ClsEmployee = New Clshrs_Employees(Page)
                ClsEmployee.Find("code='" & txtEmpCode.Text & "'")
                If ClsEmployee.ID > 0 Then
                    strcommand = "set dateformat DMY; select A.id,A.OvertimeReviewID,Convert(varchar(10),A.TransDate,103) as TransDate,Convert(varchar(10),A.TransToDate,103) as TransToDate,A.Overtime,B.code as EmployeeCode,dbo.fn_GetEmpName(b.code,1) as FullName from Att_OvertimeReviewDetail A inner join hrs_employees B on A.EmployeeID=B.id  where A.CancelDate is null and OvertimeReviewID=" & ClsOvertimeReview.ID & "and EmployeeID=" & ClsEmployee.ID & " order by B.code,A.TransDate"
                    txtEmpname.Text = ClsEmployee.ArbName & " " & ClsEmployee.FatherArbName & " " & ClsEmployee.FatherArbName
                End If

            Else
                strcommand = "set dateformat DMY; select A.id,A.OvertimeReviewID,Convert(varchar(10),A.TransDate,103) as TransDate,Convert(varchar(10),A.TransToDate,103) as TransToDate,A.Overtime,B.code as EmployeeCode,dbo.fn_GetEmpName(b.code,1) as FullName from Att_OvertimeReviewDetail A inner join hrs_employees B on A.EmployeeID=B.id  where A.CancelDate is Null and   OvertimeReviewID=" & ClsOvertimeReview.ID & " order by B.code,A.TransDate"
            End If
            Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsOvertimeReview.ConnectionString, CommandType.Text, strcommand).Tables(0)
            uwgOvertimeReview.DataSource = DT
            uwgOvertimeReview.DataBind()

            If Not ClsOvertimeReview.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsOvertimeReview.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsOvertimeReview.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsOvertimeReview.RegDate).Date
            End If
            If ClsOvertimeReview.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsOvertimeReview.CancelDate).Date
            End If
            If Not ClsOvertimeReview.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()


            If (ClsOvertimeReview.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsOvertimeReview.ConnectionString, ClsOvertimeReview.DataBaseUserRelatedID, ClsOvertimeReview.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsOvertimeReview.ConnectionString, ClsOvertimeReview.DataBaseUserRelatedID, ClsOvertimeReview.GroupID, ClsOvertimeReview.Table, ClsOvertimeReview.ID)
            If Not ClsOvertimeReview.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsOvertimeReview.ID)
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function
    Public Function SetToolBarPermission(ByVal pgSender As System.Web.UI.Page, ByVal ConnectionString As String, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal Mode As String) As Boolean
        Dim StrCommandStored As String
        Dim StrFormName As String
        Dim ObjDataSet As New Data.DataSet
        Try
            StrFormName = pgSender.Form.ID
            StrCommandStored = "hrs_GetFormsPermissions"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, StrCommandStored, UserID, GroupID, StrFormName)
            If Venus.Shared.DataHandler.CheckValidDataObject(ObjDataSet) Then
                With ObjDataSet.Tables(0).Rows(0)
                    ImageButton_Delete.Enabled = .Item("AllowDelete")
                    ImageButton_Print.Enabled = .Item("AllowPrint")
                    Select Case Mode
                        Case "N", "R"
                            ImageButton_Save.Enabled = .Item("AllowAdd")
                            ImageButton_SaveN.Enabled = .Item("AllowAdd")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                        Case "E"
                            ImageButton_Save.Enabled = .Item("AllowEdit")
                            ImageButton_SaveN.Enabled = .Item("AllowEdit")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                    End Select
                End With
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function SetToolBarRecordPermission(ByVal pgSender As System.Web.UI.Page, ByVal ConnectionString As String, ByVal UserID As Integer, ByVal GroupID As Integer, ByVal StrTableName As String, ByVal RecordID As Integer) As Boolean
        Dim StrCommandStored As String
        Dim StrFormName As String
        Dim ObjDataSet As New Data.DataSet
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Try
            StrFormName = pgSender.Form.ID
            StrCommandStored = "hrs_GetRecordsPermissions"
            ObjDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, StrCommandStored, UserID, GroupID, Replace(StrTableName, " ", ""), RecordID)
            If Venus.Shared.DataHandler.CheckValidDataObject(ObjDataSet) Then
                With ObjDataSet.Tables(0).Rows(0)

                    If ImageButton_Save.Enabled = True And .Item("CanEdit") = True Then
                        ImageButton_Save.Enabled = Not .Item("CanEdit")
                        ImageButton_SaveN.Enabled = Not .Item("CanEdit")
                        LinkButton_SaveN.Enabled = Not .Item("CanEdit")
                    End If

                    If ImageButton_Delete.Enabled = True And .Item("CanDelete") = True Then
                        ImageButton_Delete.Enabled = Not .Item("CanDelete")
                    End If

                    If ImageButton_Print.Enabled = True And .Item("CanPrint") = True Then
                        ImageButton_Print.Enabled = Not .Item("CanPrint")
                    End If
                End With
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SetToolbarSetting(ByVal ptrType As String, ByVal ClsClass As Object, ByVal intID As Integer) As Boolean
        Try
            Select Case ptrType
                Case "N", "R"
                    txtCode.Text = String.Empty
                    ImageButton_First.Visible = False
                    ImageButton_Back.Visible = False
                    ImageButton_Next.Visible = False
                    ImageButton_Last.Visible = False
                    ImageButton_Delete.Enabled = False
                    ImageButton_Properties.Visible = False
                    LinkButton_Properties.Visible = False
                    ImageButton_Remarks.Visible = False
                    LinkButton_Remarks.Visible = False

                Case "D"
                    ClsOvertimeReview.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsOvertimeReview.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsOvertimeReview
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation() As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsOvertimeReview = New ClsAtt_OvertimeReview(Me)
        Try
            With ClsOvertimeReview
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageIsNumeric(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageMaxLength(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageTabOrder(Page, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageMaskEdit(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function Setsetting(ByVal IntId As Integer) As Boolean
        ClsOvertimeReview = New ClsAtt_OvertimeReview(Me)
        If IntId > 0 Then
            ClsOvertimeReview.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        FileUpload1.Enabled = True
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsOvertimeReview = New ClsAtt_OvertimeReview(Me)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsOvertimeReview.ConnectionString)
        Try
            ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")
            IntId = ClsOvertimeReview.ID
            txtEngName.Focus()
            If ClsOvertimeReview.ID > 0 Then

                txtEmpCode.Enabled = True
                txtEmpname.Enabled = True
                WebDateChooserFromdate.Enabled = True
                txtovertimeHours.Enabled = True

                GetValues()
                StrMode = "E"
            Else

                txtEmpCode.Enabled = False
                txtEmpname.Enabled = False
                WebDateChooserFromdate.Enabled = False
                txtovertimeHours.Enabled = False


                If ClsOvertimeReview.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()


                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                txtEngName.Focus()
                CreateOtherFields(0)

                'uwgOvertimeReview.Rows.Clear()
                'For i As Integer = 1 To 7
                'uwgOvertimeReview.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Nothing, i}))
                'Next i
            End If

            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsOvertimeReview.ConnectionString, ClsOvertimeReview.DataBaseUserRelatedID, ClsOvertimeReview.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsOvertimeReview.ConnectionString, ClsOvertimeReview.DataBaseUserRelatedID, ClsOvertimeReview.GroupID, ClsOvertimeReview.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Function

    Private Function SetToolBarDefaults() As Boolean
        ImageButton_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        FileUpload1.Enabled = False
        txtEmpCode.Enabled = False
        txtEmpname.Enabled = False
        WebDateChooserFromdate.Enabled = False
        txtovertimeHours.Enabled = False
        
        ClsOvertimeReview.Clear()
        uwgOvertimeReview.Clear()
        WebDateChooserFromdate.Value = Nothing
        '  WebDateChooserTodate.Value = Nothing
        txtEmpCode.Text = ""
        txtEmpname.Text = ""
        txtovertimeHours.Text = ""
        lblovertimedetailsid.Text = ""

        ImageButton_Delete.Enabled = False
        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If

   
    End Function
    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        uwgOvertimeReview.Rows.Clear()

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
        txtEmpCode.Text = ""
        txtEmpname.Text = ""
        WebDateChooserFromdate.Value = Nothing
        '   WebDateChooserTodate.Value = Nothing
        lblovertimedetailsid.Text = ""
        txtovertimeHours.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsOvertimeReview = New ClsAtt_OvertimeReview(Me)
        ClsOvertimeReview.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsOvertimeReview.ID
        If (recordID > 0) Then
            Dim clsForms As New ClsSys_Forms(Page)
            clsForms.Find(" code = REPLACE('" & formName & "',' ','')")
            Dim clsFormsControls As New Clssys_FormsControls(Page)
            clsFormsControls.Find(" FormID=" & clsForms.ID)
            Dim tab As Data.DataTable = clsFormsControls.DataSet.Tables(0).Copy()
            For Each row As Data.DataRow In tab.Rows
                clsFormsControls.Find(" FormID=" & clsForms.ID & " And Name='" & row("Name") & "'")
                Dim sys_Fields As New Clssys_Fields(Page)
                sys_Fields.Find(" ID=" & clsFormsControls.FieldID)
                If (sys_Fields.FieldName.Trim() = "Code" Or sys_Fields.FieldName.Trim() = "Number" Or sys_Fields.FieldName.Trim() = "ID") Then
                    Continue For
                End If
                Dim currCtrl As Control = Me.FindControl(row("Name"))
                Dim bIsArabic As Boolean = IIf(IsDBNull(row("IsArabic")), False, row("IsArabic"))
                If (bIsArabic Or row("Name").ToString.ToLower.IndexOf("arb") > -1) And (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedulesForArabicText(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is TextBox) Then
                    CType(currCtrl, TextBox).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                ElseIf (TypeOf (currCtrl) Is Infragistics.WebUI.WebSchedule.WebDateChooser) Then
                    CType(currCtrl, Infragistics.WebUI.WebSchedule.WebDateChooser).Attributes.Add("onKeyPress", "LoadDataUpdateSchedules(e,'" & formName & "','" & row("Name") & "'," & recordID & ")")
                End If
            Next
        End If
    End Sub
    Private Function CreateOtherFields(ByVal IntRecordID As Integer)
        Dim dsOtherFields As New Data.DataSet
        Dim clsSysObjects As New Clssys_Objects(Me.Page)
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(Me.Page)
        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsOvertimeReview.Table) = True Then
            Dim StrTablename As String
            ClsOvertimeReview = New ClsAtt_OvertimeReview(Me)
            StrTablename = ClsOvertimeReview.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID & _
                  " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID & _
                  " And sys_OtherFields.CancelDate is Null ")
            objDS = clsOtherFieldsData.DataSet
            name.Text = ""
            realname.Text = ""
            If objDS.Tables(0).Rows.Count > 0 Then
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "U", objDS, "Interfaces_frmDocumentsTypes")
            Else
                clsMainOtherFields.CreateOtherFieldsTabs(clsSysObjects.ID, dsOtherFields, UltraWebTab1, Me.name, Me.realname, "A", objDS, "Interfaces_frmDocumentsTypes")
            End If
        End If
    End Function

#End Region
#Region "Public Shared Function"

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetEmpName(ByVal mCode As String) As String
        Try
            Dim ConnStr As String = CType(HttpContext.Current.Session("ConnectionString"), String)
            Dim Lang As String = CType(HttpContext.Current.Session("Lang"), String)

            Dim EmpName As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConnStr, Data.CommandType.Text, "Select dbo.fn_GetEmpName('" & mCode & "'," & Lang & ")")

            Return EmpName
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region

    Protected Sub uwgOvertimeReview_SelectedRowsChange(sender As Object, e As Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs) Handles uwgOvertimeReview.SelectedRowsChange
        If e.SelectedRows.Count > 0 Then
            txtEmpCode.Enabled = True
            txtEmpname.Enabled = True
            WebDateChooserFromdate.Enabled = True
            txtovertimeHours.Enabled = True


            txtEmpCode.Text = e.SelectedRows.Item(0).Cells.FromKey("EmployeeCode").Value
            txtEmpname.Text = e.SelectedRows.Item(0).Cells.FromKey("FullName").Value
            WebDateChooserFromdate.Value = e.SelectedRows.Item(0).Cells.FromKey("TransDate").Value
            ' WebDateChooserTodate.Value = e.SelectedRows.Item(0).Cells.FromKey("TransToDate").Value
            txtovertimeHours.Text = e.SelectedRows.Item(0).Cells.FromKey("Overtime").Value
            lblovertimedetailsid.Text = e.SelectedRows.Item(0).Cells.FromKey("id").Value
        End If

    End Sub

    Protected Sub txtEmpCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpCode.TextChanged
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsOvertimeReview.ConnectionString)
        If String.IsNullOrEmpty(txtCode.Text) Then
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال كود النشرة"))
            Exit Sub
        End If
        Dim strcommand As String
        ClsEmployee = New Clshrs_Employees(Me)
        Dim ClsAtt_OvertimeReviewDetail As New ClsAtt_OvertimeReviewDetail(Page)
        ClsOvertimeReview = New ClsAtt_OvertimeReview(Me)
        ClsOvertimeReview.Find("Code='" & txtCode.Text & "'")

        If ClsOvertimeReview.ID > 0 Then
            If txtEmpCode.Text = "" Then
                strcommand = "set dateformat DMY; select A.id,A.OvertimeReviewID,Convert(varchar(10),A.TransDate,103) as TransDate,Convert(varchar(10),A.TransToDate,103) as TransToDate,A.Overtime,B.code as EmployeeCode,dbo.fn_GetEmpName(b.code,1) as FullName from Att_OvertimeReviewDetail A inner join hrs_employees B on A.EmployeeID=B.id  where A.CancelDate is  null  and OvertimeReviewID=" & ClsOvertimeReview.ID & " order by B.code ,A.TransDate"
            Else
                ClsEmployee.Find("code='" & txtEmpCode.Text & "'")
                If ClsEmployee.ID > 0 Then
                    strcommand = "set dateformat DMY; select A.id,A.OvertimeReviewID,Convert(varchar(10),A.TransDate,103) as TransDate,Convert(varchar(10),A.TransToDate,103) as TransToDate,A.Overtime,B.code as EmployeeCode,dbo.fn_GetEmpName(b.code,1) as FullName from Att_OvertimeReviewDetail A inner join hrs_employees B on A.EmployeeID=B.id  where A.CancelDate is  null  and  OvertimeReviewID=" & ClsOvertimeReview.ID & " and EmployeeID=" & ClsEmployee.ID & " order by B.code,A.TransDate"
                    txtEmpname.Text = ClsEmployee.ArbName & " " & ClsEmployee.FatherArbName & " " & ClsEmployee.FatherArbName

                Else
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /كود الموظف غير صحيح"))
                    uwgOvertimeReview.DataSource = Nothing
                    uwgOvertimeReview.DataBind()
                    txtEmpname.Text = ""
                    txtEmpname.Text = ""

                    Exit Sub
                End If
                WebDateChooserFromdate.Value = Nothing
                ' WebDateChooserTodate.Value = Nothing
            End If
            Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsOvertimeReview.ConnectionString, CommandType.Text, strcommand).Tables(0)
            uwgOvertimeReview.DataSource = DT
            uwgOvertimeReview.DataBind()
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /كود النشرة غير موجود وسوف يتم اضافة نشرة جديدة عند الحفظ"))
            If txtEmpCode.Text <> "" Then
                ClsEmployee.Find("code='" & txtEmpCode.Text & "'")
                txtEmpname.Text = ClsEmployee.ArbName & " " & ClsEmployee.FatherArbName & " " & ClsEmployee.FatherArbName
            End If


            Exit Sub
        End If


    End Sub

   
    Protected Sub uwgOvertimeReview_ClickCellButton(sender As Object, e As Infragistics.WebUI.UltraWebGrid.CellEventArgs) Handles uwgOvertimeReview.ClickCellButton
        Dim ID As Integer = Convert.ToInt32(e.Cell.Row.Cells(0).Value)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsOvertimeReview.ConnectionString)
        Dim ClsAtt_OvertimeReviewDetail As New ClsAtt_OvertimeReviewDetail(Page)
        ClsAtt_OvertimeReviewDetail.Find("ID=" & ID)

        ClsAtt_OvertimeReviewDetail.Delete("ID=" & ClsAtt_OvertimeReviewDetail.ID)
        GetValues(True)

        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Record Deleted /تم الحذف"))
    End Sub
End Class
