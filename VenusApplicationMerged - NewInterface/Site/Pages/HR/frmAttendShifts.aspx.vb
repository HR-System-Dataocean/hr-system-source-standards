Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmAttendShifts
    Inherits MainPage
#Region "Public Decleration"
    Private ClsAttendShifts As ClsAtt_AttendShifts
    Private clsMainOtherFields As clsSys_MainOtherFields

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsAttendShifts = New ClsAtt_AttendShifts(Me)
        Dim ClsObjects As New Clssys_Objects(Page)
        Dim ClsSearchs As New Clssys_Searchs(Page)
        Dim SearchID As Integer = 0
        Try
            Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
            If ClsObjects.Find(" Code='" & ClsAttendShifts.Table.Trim & "'") Then
                If ClsSearchs.Find(" ObjectID='" & ClsObjects.ID & "'") Then
                    SearchID = ClsSearchs.ID
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & SearchID & "&'," & IntDimension & ",720,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If
            '===================================== Exit & Navigation Notification [Start]
            If Not IsPostBack Then
                Page.Session.Add("ConnectionString", ClsAttendShifts.ConnectionString)
                ClsAttendShifts.AddOnChangeEventToControls("frmAttendShifts", Page, UltraWebTab1)
                '================================= Exit & Navigation Notification [ End ]
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, txtArbName, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, WebTextEdit1, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)

                Dim clsenum As New Clshrs_Enum(Me)
                clsenum.GetList(uwgAttendShiftsDays.DisplayLayout.Bands(0).Columns(2).ValueList, False, "Flag = 4")
            End If
            '================================== Add DateUpdateSchedules [Start]
            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsAttendShifts.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsAttendShifts.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsAttendShifts.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command
        ClsAttendShifts = New ClsAtt_AttendShifts(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAttendShifts.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsAttendShifts.Find("Code='" & txtCode.Text & "'")
                If TimeValidation() = False Then
                    Exit Sub
                End If
                If CheckTimesBlank() = False Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Shift Time is Blank /توقيت بداية أو نهاية الدوام الأول لايمكن ان يكون فارغ لأيام الدوام"))
                    Exit Sub
                End If
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsAttendShifts.ID > 0 Then
                    ClsAttendShifts.Update("code='" & txtCode.Text & "'")
                Else
                    ClsAttendShifts.Save()
                End If
                ClsAttendShifts.Find("Code='" & txtCode.Text & "'")
                If ClsAttendShifts.ID > 0 Then
                    If (SaveDG(ClsAttendShifts.ID)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsAttendShifts.Table, ClsAttendShifts.ID)
                        value.Text = ""
                    End If
                End If
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsAttendShifts.Table, ClsAttendShifts.ID)
                value.Text = ""
                AfterOperation()
            Case "Save"
                If txtCode.Text = "" Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                    Exit Sub
                End If
                ClsAttendShifts.Find("Code='" & txtCode.Text & "'")
                If TimeValidation() = False Then
                    Exit Sub
                End If
                If CheckTimesBlank() = False Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Shift Time is Blank /توقيت بداية أو نهاية الدوام الأول لايمكن ان يكون فارغ لأيام الدوام"))
                    Exit Sub
                End If
                If Not AssignValues() Then
                    Exit Sub
                End If
                If ClsAttendShifts.ID > 0 Then
                    ClsAttendShifts.Update("code='" & txtCode.Text & "'")
                Else
                    ClsAttendShifts.Save()
                End If

                ClsAttendShifts.Find("Code='" & txtCode.Text & "'")

                If ClsAttendShifts.ID > 0 Then
                    If (SaveDG(ClsAttendShifts.ID)) Then
                        clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                        clsMainOtherFields.CollectDataAndSave(value.Text, ClsAttendShifts.Table, ClsAttendShifts.ID)
                        value.Text = ""
                    End If
                End If
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Save Done /تم الحفظ"))
            Case "New"
                AfterOperation()
            Case "Delete"
                ClsAttendShifts.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsAttendShifts.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsAttendShifts.ID & "&TableName=" & ClsAttendShifts.Table, 477, 313, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsAttendShifts.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsAttendShifts.ID & "&TableName=" & ClsAttendShifts.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "Print"
                Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsAttendShifts.Table
                ClsAttendShifts.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsAttendShifts.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsAttendShifts.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsAttendShifts.Find(" Code= '" & txtCode.Text & "'")
                If ClsAttendShifts.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsAttendShifts.DataSet
                    If Not AssignValues() Then
                        Exit Sub
                    End If
                    If ClsAttendShifts.CheckDiff(ClsAttendShifts, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsAttendShifts.FirstRecord()
                GetValues()
            Case "Previous"
                ClsAttendShifts.Find("Code='" & txtCode.Text & "'")
                If Not ClsAttendShifts.previousRecord() Then
                    ClsAttendShifts.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                GetValues()
            Case "Next"
                ClsAttendShifts.Find("Code='" & txtCode.Text & "'")
                If Not ClsAttendShifts.NextRecord() Then
                    ClsAttendShifts.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                GetValues()
            Case "Last"
                ClsAttendShifts.LastRecord()
                GetValues()
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

#End Region

#Region "Private Functions"
    Private Function SaveDG(ByVal ClsAttendShiftsID As Integer) As Boolean
        Try
            If ClsAttendShiftsID > 0 Then
                Dim str As String
                str = "delete from Att_AttendShiftDays where AttendShiftID = " & ClsAttendShiftsID & ";"
                For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgAttendShiftsDays.Rows()
                    str &= " INSERT INTO Att_AttendShiftDays (AttendShiftID,DayNo,TimeIn,TimeOut,TimeIn2nd,TimeOut2nd,IsDayOff,RegDate,RegUserID) VALUES (" & ClsAttendShiftsID & "," & DGRow.Cells.FromKey("DayNo").Value & ",'" & DGRow.Cells.FromKey("TimeIn").Value & "','" & DGRow.Cells.FromKey("TimeOut").Value & "','" & DGRow.Cells.FromKey("TimeIn2nd").Value & "','" & DGRow.Cells.FromKey("TimeOut2nd").Value & "','" & IIf(CBool(DGRow.Cells.FromKey("IsDayOff").Value) = True, 1, 0) & "',getdate()," & ClsAttendShifts.DataBaseUserRelatedID & ") ;"
                Next
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsAttendShifts.ConnectionString, Data.CommandType.Text, str)
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Private Function AssignValues() As Boolean
        Try
            With ClsAttendShifts
                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .FirstShiftTimeInFingerprintStart = firstShiftFingerprintStartTextBox.Text
                .FirstShiftEntryTimeInClose = firstShiftEntryTimeInCloseTextBox.Text
                .FirstShiftTimeOutFingerprintClose = firstShiftFingerprintEndTextBox.Text
                .SecondShiftTimeInFingerprintStart = secondShiftFingerprintStartTextBox.Text
                .SecondShiftEntryTimeInClose = SecondShiftEntryTimeInCloseTextBox.Text
                .SecondShiftTimeOutFingerprintClose = secondShiftFingerprintEndTextBox.Text
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetValues() As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsUser.ConnectionString)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            SetToolBarDefaults()
            With ClsAttendShifts
                txtCode.Text = .Code
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                firstShiftFingerprintStartTextBox.Text = .FirstShiftTimeInFingerprintStart
                firstShiftEntryTimeInCloseTextBox.Text = .FirstShiftEntryTimeInClose
                firstShiftFingerprintEndTextBox.Text = .FirstShiftTimeOutFingerprintClose
                secondShiftFingerprintStartTextBox.Text = .SecondShiftTimeInFingerprintStart
                SecondShiftEntryTimeInCloseTextBox.Text = .SecondShiftEntryTimeInClose
                secondShiftFingerprintEndTextBox.Text = .SecondShiftTimeOutFingerprintClose

                Dim Cls_AttendShiftDays As New ClsAtt_AttendShiftDays(Me)
                Cls_AttendShiftDays.Find("AttendShiftID = " & ClsAttendShifts.ID)
                uwgAttendShiftsDays.DataSource = Cls_AttendShiftDays.DataSet.Tables(0)
                uwgAttendShiftsDays.DataBind()
            End With

            If Not ClsAttendShifts.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsAttendShifts.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsAttendShifts.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsAttendShifts.RegDate).Date
            End If
            If ClsAttendShifts.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsAttendShifts.CancelDate).Date
            End If
            If Not ClsAttendShifts.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()


            If (ClsAttendShifts.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsAttendShifts.ConnectionString, ClsAttendShifts.DataBaseUserRelatedID, ClsAttendShifts.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsAttendShifts.ConnectionString, ClsAttendShifts.DataBaseUserRelatedID, ClsAttendShifts.GroupID, ClsAttendShifts.Table, ClsAttendShifts.ID)
            If Not ClsAttendShifts.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsAttendShifts.ID)
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function
    'Added By: Hassan Kurdi
    'Date: 2021-12-13
    'Purpose: Check if the no blank field for first shift
    Private Function CheckTimesBlank() As Boolean
        Try
            Dim EmptyShiftCount As Integer = 0

            For Each DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow In uwgAttendShiftsDays.Rows()
                If CBool(DGRow.Cells.FromKey("IsDayOff").Value = False) Then
                    Dim TimeIn1 = IIf(DGRow.Cells.FromKey("TimeIn").Value <> Nothing, DGRow.Cells.FromKey("TimeIn").Value, Date.MinValue)
                    Dim TimeIn2 = IIf(DGRow.Cells.FromKey("TimeOut").Value <> Nothing, DGRow.Cells.FromKey("TimeOut").Value, Date.MinValue)

                    If TimeIn1 = Date.MinValue Or TimeIn2 = Date.MinValue Then
                        EmptyShiftCount = EmptyShiftCount + 1
                    End If
                End If

            Next
            If (EmptyShiftCount = 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    'Added By: Hassan Kurdi
    'Date: 2021-12-13
    'Purpose: Check if the time formate is correct
    Private Function TimeValidation() As Boolean
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAttendShifts.ConnectionString)
        Dim regex As Regex = New Regex("^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$")
        regex.IsMatch(firstShiftFingerprintStartTextBox.Text)

        If firstShiftFingerprintStartTextBox.Text <> "" Then
            If regex.IsMatch(firstShiftFingerprintStartTextBox.Text) = False Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " First Shift Fingerprint Start is not in vaild Format /بداية عمل البصمة للدوام الأول بصيغة غير صحيحة"))
                Return False
            End If
        End If

        If firstShiftEntryTimeInCloseTextBox.Text <> "" Then
            If regex.IsMatch(firstShiftEntryTimeInCloseTextBox.Text) = False Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " First Shift Entry Time In Close is not in vaild Format /نهايةالدخول للدوام الأول بصيغة غير صحيحة"))
                Return False
            End If
        End If

        If firstShiftFingerprintEndTextBox.Text <> "" Then
            If regex.IsMatch(firstShiftFingerprintEndTextBox.Text) = False Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " First Shift Fingerprint End is not in vaild Format /نهاية عمل البصمة للدوام الأول بصيغة غير صحيحة"))
                Return False
            End If
        End If

        If secondShiftFingerprintStartTextBox.Text <> "" Then
            If regex.IsMatch(secondShiftFingerprintStartTextBox.Text) = False Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Second Shift Fingerprint Start is not in vaild Format /بداية عمل البصمة للدوام الثاني بصيغة غير صحيحة"))
                Return False
            End If
        End If

        If SecondShiftEntryTimeInCloseTextBox.Text <> "" Then
            If regex.IsMatch(SecondShiftEntryTimeInCloseTextBox.Text) = False Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Second Shift Fingerprint Start is not in vaild Format /بداية عمل البصمة للدوام الثاني بصيغة غير صحيحة"))
                Return False
            End If
        End If

        If secondShiftFingerprintEndTextBox.Text <> "" Then
            If regex.IsMatch(secondShiftFingerprintEndTextBox.Text) = False Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Second Shift Fingerprint Start is in not vaild Format /بداية عمل البصمة للدوام الثاني بصيغة غير صحيحة"))
                Return False
            End If
        End If

        Return True
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
                    ClsAttendShifts.Find("ID=" & intID)
                    GetValues()
                    txtCode.ReadOnly = True
                    ImageButton_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsAttendShifts.Find("ID=" & intID)
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
            With ClsAttendShifts
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
        ClsAttendShifts = New ClsAtt_AttendShifts(Me)
        Try
            With ClsAttendShifts
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
        ClsAttendShifts = New ClsAtt_AttendShifts(Me)
        If IntId > 0 Then
            ClsAttendShifts.Find("ID=" & IntId)
            GetValues()
        End If
        Dim clsSearchsColumns As New Clssys_SearchsColumns(Page)
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsAttendShifts = New ClsAtt_AttendShifts(Me)

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsAttendShifts.ConnectionString)
        Try
            ClsAttendShifts.Find("Code='" & txtCode.Text & "'")
            IntId = ClsAttendShifts.ID
            txtEngName.Focus()
            If ClsAttendShifts.ID > 0 Then
                GetValues()
                StrMode = "E"
            Else
                If ClsAttendShifts.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Clear()
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                txtEngName.Focus()
                CreateOtherFields(0)

                uwgAttendShiftsDays.Rows.Clear()
                For i As Integer = 1 To 7
                    uwgAttendShiftsDays.Rows.Add(New Infragistics.WebUI.UltraWebGrid.UltraGridRow(New Object() {Nothing, Nothing, i}))
                Next i
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsAttendShifts.ConnectionString, ClsAttendShifts.DataBaseUserRelatedID, ClsAttendShifts.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsAttendShifts.ConnectionString, ClsAttendShifts.DataBaseUserRelatedID, ClsAttendShifts.GroupID, ClsAttendShifts.Table, IntId)
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
        ClsAttendShifts.Clear()
        uwgAttendShiftsDays.Clear()
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
        firstShiftFingerprintStartTextBox.Text = String.Empty
        firstShiftEntryTimeInCloseTextBox.Text = String.Empty
        firstShiftFingerprintEndTextBox.Text = String.Empty
        secondShiftFingerprintStartTextBox.Text = String.Empty
        SecondShiftEntryTimeInCloseTextBox.Text = String.Empty
        secondShiftFingerprintEndTextBox.Text = String.Empty
        uwgAttendShiftsDays.Rows.Clear()

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsAttendShifts = New ClsAtt_AttendShifts(Page)
        ClsAttendShifts.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsAttendShifts.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsAttendShifts.Table) = True Then
            Dim StrTablename As String
            ClsAttendShifts = New ClsAtt_AttendShifts(Me)
            StrTablename = ClsAttendShifts.Table
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
