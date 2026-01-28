Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmGrades
    Inherits MainPage
#Region "Public Decleration"
    Dim ClsGrades As Clshrs_Grades
    Private ClsVactionTypes As Clshrs_VacationsTypes
    Private ClsGradesVactions As Clshrs_GradesVacations
    Private ClsTransactionTypes As Clshrs_TransactionsTypes
    Private ClsGradesTransactions As Clshrs_GradesTransactions
    Private clsMainOtherFields As clsSys_MainOtherFields

    Const CsPaidatVacation = 4
    Const CsOnceatPeriod = 5
    Const CsMaxValue = 3
    Const CsMinValue = 2
    Const CsIntervalID = 6
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsGrades = New Clshrs_Grades(Me)
        ClsVactionTypes = New Clshrs_VacationsTypes(Page)
        ClsTransactionTypes = New Clshrs_TransactionsTypes(Page)
        Dim clsIntervals As New Clshrs_Intervals(Page)

        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0

        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsGrades.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsGrades.ConnectionString)
                ClsGrades.AddOnChangeEventToControls("frmGrades", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                Dim clsMainCountry As New Clssys_Countries(Page)
                Dim clsMainCurrency As New ClsSys_Currencies(Page)
                clsMainCountry.Find(" IsMainCountries = 1 ")
                If clsMainCountry.ID > 0 Then
                    clsMainCurrency.Find(" ID=" & clsMainCountry.CurrencyID)
                    If Not IsNothing(clsMainCurrency.NoDecimalPlaces) Then
                        uwgGradesTransactions.Columns(2).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgGradesTransactions.Columns(2).Format, clsMainCurrency.NoDecimalPlaces)
                        uwgGradesTransactions.Columns(3).Format = clsMainCurrency.GetFormatOfDecimalPlaces(uwgGradesTransactions.Columns(3).Format, clsMainCurrency.NoDecimalPlaces)
                    End If
                End If
                clsIntervals.GetList(uwgGradesTransactions.Columns(CsIntervalID).ValueList)
                GetList()
                ClsTransactionTypes.GetList(uwgGradesTransactions.Columns(1).ValueList, True)

                uwgGradesTransactions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow)
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsGrades.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsGrades.ID
                If (IntrecordID > 0) Then
                    SetScreenInformation("E")
                Else
                    SetScreenInformation("N")
                End If
            Else
                SetScreenInformation("N")
            End If
            CreateOtherFields(IntrecordID)
            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsGrades.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsGrades = New Clshrs_Grades(Me)
        ClsGradesVactions = New Clshrs_GradesVacations(Page)
        ClsGradesTransactions = New Clshrs_GradesTransactions(Page)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsGrades.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                SavePart()

                ClsGrades.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsGrades.Table, ClsGrades.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If

                SavePart()

                ClsGrades.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsGrades.Table, ClsGrades.ID)
                value.Text = ""
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                ClsGradesVactions.Delete(" GradeID=" & ClsGrades.ID.ToString())
                ClsGradesTransactions.Delete(" GradeID=" & ClsGrades.ID.ToString())
                ClsGrades.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsGrades.ID & "&TableName=" & ClsGrades.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsGrades.ID & "&TableName=" & ClsGrades.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsGrades.Table
                ClsGrades.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsGrades.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsGrades.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsGrades.Find(" Code= '" & txtCode.Text & "'")
                If ClsGrades.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsGrades.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsGrades.CheckDiff(ClsGrades, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsGrades.FirstRecord()
                GetValues()
            Case "Previous"
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                If Not ClsGrades.previousRecord() Then
                    ClsGrades.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                If Not ClsGrades.NextRecord() Then
                    ClsGrades.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsGrades.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"

    Private Function validateUserInputs() As Boolean
        For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGradesTransactions.Rows
            If DGRow.Cells.FromKey("TransactionTypeID").Value > 0 Then
                If DGRow.Cells(CsMaxValue).Value = Nothing Or DGRow.Cells(CsMaxValue).Value = Nothing Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function
    Private Function GetList() As Boolean
        Dim ObjNavigation As New Venus.Shared.Web.NavigationHandler(ClsGrades.ConnectionString)
        uwgGradesTransactions.Columns.FromKey("PaidAtVacation").ValueList.ValueListItems.Add(New Infragistics.WebUI.UltraWebGrid.ValueListItem(ObjNavigation.SetLanguage(Page, "No/لا"), 0))
        uwgGradesTransactions.Columns.FromKey("PaidAtVacation").ValueList.ValueListItems.Add(New Infragistics.WebUI.UltraWebGrid.ValueListItem(ObjNavigation.SetLanguage(Page, "Include in vac /تشمل فى الأجازة "), 1))
        uwgGradesTransactions.Columns.FromKey("PaidAtVacation").ValueList.ValueListItems.Add(New Infragistics.WebUI.UltraWebGrid.ValueListItem(ObjNavigation.SetLanguage(Page, "Include in vac only/تشمل فى الأجازة فقط"), 2))
    End Function
    Private Function SaveDG(ByVal GradeID As Integer) As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsGrades.ConnectionString)

        Try
            ClsGradesTransactions = New Clshrs_GradesTransactions(Page)
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsGradesTransactions.ConnectionString, Data.CommandType.Text, _
                                                                       "UPDATE " & ClsGradesTransactions.Table & " SET [CancelDate]=GetDate() WHERE GradeID=" & GradeID)
  
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGradesTransactions.Rows
                If IsNothing(DGRow.Cells(1).Value) OrElse (Not DGRow.Cells(1).Value > 0) Then
                    Continue For
                End If
                ClsGradesTransactions = New Clshrs_GradesTransactions(Page)
                ClsGradesTransactions.GradeID = GradeID
                ClsGradesTransactions.TransactionTypeID = DGRow.Cells(1).Value

                '[AGL] 11-09-2007 [Begin]
                ClsGradesTransactions.PaidatVacation = DGRow.Cells(CsPaidatVacation).Value
                ClsGradesTransactions.OnceatPeriod = DGRow.Cells(CsOnceatPeriod).Value

                '[AGL] 11-09-2007 [End]
                If (DGRow.Cells(CsMinValue).Value = Nothing) Then
                    ClsGradesTransactions.MinValue = 0
                Else
                    ClsGradesTransactions.MinValue = DGRow.Cells(CsMinValue).Value
                End If

                If (DGRow.Cells(CsMaxValue).Value = Nothing) Then

                    ClsGradesTransactions.MaxValue = 1
                Else
                    ClsGradesTransactions.MaxValue = DGRow.Cells(CsMaxValue).Value
                End If

                If (DGRow.Cells(CsIntervalID).Value = Nothing) Then

                    ClsGradesTransactions.IntervalID = 0
                Else
                    ClsGradesTransactions.IntervalID = DGRow.Cells(CsIntervalID).Value
                End If


                If (DGRow.Cells(0).Value <> Nothing) Then
                    Dim ClsGradesStepsTransactions As New Clshrs_GradesStepsTransactions(Page)
                    'Dim currMax As Double
                    'Dim currMin As Double
                    ClsGradesStepsTransactions.Find("hrs_GradesStepsTransactions.GradeTransactionID=" & DGRow.Cells(0).Value & "  Order By hrs_GradesStepsTransactions.Amount Asc")
                    If (ClsGradesStepsTransactions.ID > 0 And DGRow.Cells(2).Value > ClsGradesStepsTransactions.Amount) Then
                        '[AGL] 11-09-2007  --------Begin
                        'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, " Minimum value of " & DGRow.Cells(1).Text & " \n must less than or equal " & ClsGradesStepsTransactions.Amount.ToString() & "\n because it is used already in Grade Step Transactions")
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Update Can not be Completed because \n It will affect Grade steps limits of Transaction : " & DGRow.Cells(1).Text & "/يتعذر استكمال الحفظ لأنه سوف يؤثر على الحزم الماية بحدود البند : " & DGRow.Cells(1).Text))
                        '[AGL]             --------End
                        Return False
                    End If

                    ClsGradesStepsTransactions.Find(" hrs_GradesStepsTransactions.GradeTransactionID=" & DGRow.Cells(0).Value & " Order By hrs_GradesStepsTransactions.Amount Desc ")
                    If (ClsGradesStepsTransactions.ID > 0 And DGRow.Cells(3).Value < ClsGradesStepsTransactions.Amount) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Maximum value of " & DGRow.Cells(1).Text & " \n must greater than or equal " & ClsGradesStepsTransactions.Amount.ToString() & "\n because it is used already in Grade Step Transactions/يجب أن تكون القيمة أكبر من أو يساوي " & ClsGradesStepsTransactions.Amount.ToString() & " للحد الأقصى للبند " & DGRow.Cells(1).Text))
                        Return False '"
                    End If
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsGradesTransactions.ConnectionString, Data.CommandType.Text, _
                                                                              "UPDATE " & ClsGradesTransactions.Table & " SET [CancelDate]=Null WHERE ID=" & DGRow.Cells(0).Value)
                    ClsGradesTransactions.Update("hrs_GradesTransactions.ID=" & DGRow.Cells(0).Value)
                Else
                    ClsGradesTransactions.Save()
                End If
            Next
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function
    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")

        ClsGrades = New Clshrs_Grades(Page)
        ClsGradesVactions = New Clshrs_GradesVacations(Page)
        ClsGradesTransactions = New Clshrs_GradesTransactions(Page)

        Try
            ClsGrades.Find("Code='" & txtCode.Text & "'")

            If ClsGrades.ID > 0 Then
                If Not AssignValues() Then
                    Exit Function
                End If

                ClsGrades.Update("Code='" & txtCode.Text & "'")
                ClsGradesVactions.DeleteAll("GradeID=" & ClsGrades.ID)
                'ClsGradesTransactions.DeleteAll("GradeID=" & ClsGrades.ID)
                If (SaveDG(ClsGrades.ID)) Then
                    Return True
                End If
            Else
                If Not AssignValues() Then
                    Exit Function
                End If
                ClsGrades.Save()
                ClsGrades.Find("Code='" & txtCode.Text & "'")
                If ClsGrades.ID > 0 Then
                    If (SaveDG(ClsGrades.ID)) Then
                        Return True
                    End If
                End If

            End If

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsGrades.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function AssignValues() As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsGrades.ConnectionString)
        Dim arr As New ArrayList()
        Try
            arr.Clear()
            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgGradesTransactions.Rows
                If Not DGRow.Cells(1).Value > 0 Then
                    Continue For
                End If
                If (arr.Contains(DGRow.Cells(1).Value)) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Can't enter duplicate Transaction to that Grade/لا يمكن إدخال بند مكررة في نفس الحزمة"))
                    Return False
                End If
                arr.Add(DGRow.Cells(1).Value)
            Next
            With ClsGrades
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .ArbName4S = txtArbName.Text
                Try
                    .GradeLevel = CInt(txtGradeLevel.Text)
                    .RegularHours = Convert.ToSingle(txtRegularHours.Text)
                Catch ex As Exception
                    Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
                    Page.Session.Add("ErrorValue", ex)
                    mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsGrades.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
                    Page.Response.Redirect("ErrorPage.aspx")
                End Try
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsGradesVactions = New Clshrs_GradesVacations(Page)
        ClsGradesTransactions = New Clshrs_GradesTransactions(Page)
        Try
            SetToolBarDefaults()

            With ClsGrades
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtGradeLevel.Text = .GradeLevel
                txtRegularHours.Text = .RegularHours


                ClsGradesTransactions.Find(" GradeID =" & .ID & " And hrs_GradesTransactions.CancelDate Is Null")
                uwgGradesTransactions.DataSource = ClsGradesTransactions.DataSet.Tables(0).DefaultView
                uwgGradesTransactions.DataBind()

            End With

            uwgGradesTransactions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow)

            If Not ClsGrades.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsGrades.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsGrades.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsGrades.RegDate).Date
            End If
            If ClsGrades.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsGrades.CancelDate).Date
            End If
            If Not ClsGrades.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()

            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsGrades.ConnectionString)
            If (ClsGrades.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsGrades.ConnectionString, ClsGrades.DataBaseUserRelatedID, ClsGrades.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsGrades.ConnectionString, ClsGrades.DataBaseUserRelatedID, ClsGrades.GroupID, ClsGrades.Table, ClsGrades.ID)
            If Not ClsGrades.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsGrades.ID)
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
                    ClsGrades.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsGrades.Find("ID=" & intID)
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
            With ClsGrades
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
        ClsGrades = New Clshrs_Grades(Me)
        Try
            With ClsGrades
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
        ClsGrades = New Clshrs_Grades(Me)
        If IntId > 0 Then
            ClsGrades.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsGrades = New Clshrs_Grades(Me)
        Try
            ClsGrades.Find("Code='" & txtCode.Text & "'")
            IntId = ClsGrades.ID
            txtEngName.Focus()
            If ClsGrades.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsGrades.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsGrades.ConnectionString, ClsGrades.DataBaseUserRelatedID, ClsGrades.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsGrades.ConnectionString, ClsGrades.DataBaseUserRelatedID, ClsGrades.GroupID, ClsGrades.Table, IntId)
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
        ClsGrades.Clear()
        Clear()
        GetValues()
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
        txtGradeLevel.Text = String.Empty
        txtRegularHours.Text = String.Empty
        uwgGradesTransactions.Rows.Clear()
        uwgGradesTransactions.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow)

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsGrades = New Clshrs_Grades(Page)
        ClsGrades.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsGrades.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsGrades.Table) = True Then
            Dim StrTablename As String
            ClsGrades = New Clshrs_Grades(Me)
            StrTablename = ClsGrades.Table
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

End Class
