Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports Infragistics.WebUI.WebDataInput

Partial Class frmEmployees
    Inherits MainPage

#Region "Public Decleration"
    Private ClsEmployees As Clshrs_Employees
    Private ClsPositions As Clshrs_Positions
    Private ClsDocumentsInformations As Clssys_DocumentsInformations
    Private ClsDocumentAttachment As Clssys_DocumentsInformationAttachments
    Private clsSearchsColumns As Clssys_SearchsColumns
    Private clsMainOtherFields As clsSys_MainOtherFields
    Private ClsEmployeeJoin As Clshrs_EmployeesJoins
    Private ClsEmployeesDependants As Clshrs_EmployeesDependants
    Private ObjNavigationHandler As Venus.Shared.Web.NavigationHandler
    Private ClsObjects As Clssys_Objects
    Private ClsSearchs As Clssys_Searchs
    Private clsContracts As Clshrs_Contracts
    Private ClsContractTransactions As Clshrs_ContractsTransactions

    Const uwgId = 0
    Const uwgEngDescription = 1
    Const uwgArbDescription = 2

    Const CSDependentID = 0
    Const CSDependentEngName = 1

    Const CSDependentArbName = 2
    Const CSDependentDependentType = 3
    Const CSDependentBirthDate = 4
    Const CSDependentNationality = 5
    Const CSDependentBirthCity = 6
    Const CSDependentSex = 7
    Const CSDependentInsuranceCovered = 8

    Const uwgEdit = 3
    Const uwgDestination = 4

    Const csOtherFields = 11
#End Region

#Region "Protected Sub"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ClsObjects As New Clssys_Objects(Me.Page)
        Dim ClsSearchs As New Clssys_Searchs(Me.Page)
        ClsEmployees = New Clshrs_Employees(Me.Page)
        clsSearchsColumns = New Clssys_SearchsColumns(Me.Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim csSearchID As Integer
        TxtCostName1.Enabled = False
        TxtCostName2.Enabled = False
        TxtCostName3.Enabled = False
        TxtCostName4.Enabled = False
        TxtPositionName.Enabled = False

        Try
            If ClsObjects.Find(" Code='" & ClsEmployees.Table.Trim() & "'") Then
                If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                    csSearchID = ClsSearchs.ID

                    '' add by tal3at for search
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtCode.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & txtCode.ClientID & "'"
                    btnSearchCode.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                    UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & txtManager.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & txtManager.ClientID & "'"
                    btnManager.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"
                End If
            End If


            CheckGeneralSetup()
            If ClsObjects.Find(" Code='hrs_Positions'") Then
                If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                    csSearchID = ClsSearchs.ID

                    '' add by tal3at for search
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtPositionCode.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & TxtPositionCode.ClientID & "'"
                    WebImageButton5.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                End If
            End If
            If ClsObjects.Find(" Code='fcs_CostCenters1'") Then
                If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                    csSearchID = ClsSearchs.ID

                    '' add by tal3at for search
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtCostCode1.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & TxtCostCode1.ClientID & "'"
                    WebImageButton1.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                End If
            End If
            If ClsObjects.Find(" Code='fcs_CostCenters2'") Then
                If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                    csSearchID = ClsSearchs.ID

                    '' add by tal3at for search
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtCostCode2.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & TxtCostCode2.ClientID & "'"
                    WebImageButton2.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                End If
            End If

            If ClsObjects.Find(" Code='fcs_CostCenters3'") Then
                If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                    csSearchID = ClsSearchs.ID

                    '' add by tal3at for search
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtCostCode3.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & TxtCostCode3.ClientID & "'"
                    WebImageButton3.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                End If
            End If
            If ClsObjects.Find(" Code='fcs_CostCenters4'") Then
                If ClsSearchs.Find(" ObjectID=" & ClsObjects.ID) Then
                    csSearchID = ClsSearchs.ID

                    '' add by tal3at for search
                    Dim IntDimension As Integer = 510
                    Dim UrlString = "'frmModalSearchScreen.aspx?TargetControl=" & TxtCostCode4.ID & "&SearchID=" & csSearchID & "&'," & IntDimension & ",720,false,false,'" & TxtCostCode4.ClientID & "'"
                    WebImageButton4.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                End If
            End If
            AddEventToControls()


            If Not IsPostBack Then
                LoadDataIntoControls()
                SetSecurity(ClsEmployees)
                Page.Session.Add("ConnectionString", ClsEmployees.ConnectionString)
                Page.Session.Add("Lage", objNav.SetLanguage(Page, "0/1"))
                hdnLang.Value = objNav.SetLanguage(Page, "0/1")
                New_Part()
            End If


            Dim IntrecordID As Integer
            If (txtCode.Text <> "") Then
                ClsEmployees.Find(" Code='" & txtCode.Text & "'")
                IntrecordID = ClsEmployees.ID
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsEmployees.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try

    End Sub
    Protected Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles ImageButton1_Save.Command, ImageButton_SaveN.Command, LinkButton_SaveN.Command, ImageButton_New.Command, ImageButton_Print.Command, ImageButton_Properties.Command, LinkButton_Properties.Command, ImageButton_Remarks.Command, LinkButton_Remarks.Command, ImageButton_Last.Command, ImageButton_Next.Command, ImageButton_Back.Command, ImageButton_First.Command, ImageButton_Delete.Command, ImageButton_History.Command, LinkButton_History.Command ', btnDelete.Command, btnNew.Command, btnSave.Command
        ClsEmployees = New Clshrs_Employees(Me)
        Dim StrMode As String = Request.QueryString("mode")
        Dim IntId As Integer = Request.QueryString("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Select Case e.CommandArgument
            Case "SaveNew"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues_Employees() Then
                    Exit Sub
                End If
                'If String.IsNullOrEmpty(txtWorkE_Mail.Text) Then
                '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to enter Work E-mail /عفوا...لابد من اخال بريد العمل"))
                '    Exit Sub
                'End If
                If DdlNationality.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select nationality /عفوا...لابد من تحديد الجنسية "))
                    Exit Sub
                End If
                If ddlBranch.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Branch /عفوا...لابد من تحديد الفرع "))
                    Exit Sub
                End If
                If ddlDepartment.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Department /عفوا...لابد من تحديد القسم "))
                    Exit Sub
                End If
                If ddlSponsor.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Sponsor /عفوا...لابد من تحديد الكفيل "))
                    Exit Sub
                End If
                If ddlContractType.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Contract Type /عفوا...لابد من تحديد نوع العقد "))
                    Exit Sub
                End If
                If ddlProfessions.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Profession   /عفوا...لابد من تحديد المهنة "))
                    Exit Sub
                End If
                If ddlGradeStep.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Grade step   /عفوا...لابد من تحديد الرتبة "))
                    Exit Sub
                End If
                If String.IsNullOrEmpty(txtPositionCode.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Position   /عفوا...لابد من تحديد الوظيفة "))
                    Exit Sub
                End If
                If ddlEmployeeClass.SelectedValue = 0 Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Employee Class   /عفوا...لابد من تحديد الفئة "))
                    Exit Sub
                End If
                If String.IsNullOrEmpty(JoinDate.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Join Date /عفوا...لابد من ادخال تاريخ المباشرة "))
                    Exit Sub
                End If
                If String.IsNullOrEmpty(txtIdentity.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Identity No  /عفوا...لابد من ادخال رقم الهوية  "))
                    Exit Sub
                End If
                If String.IsNullOrEmpty(txtStartDate.Text) Then
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Start Date   /عفوا...لابد من ادخال تاريخ البدء  "))
                    Exit Sub
                End If
                'Edited by: Hassan Kurdi
                'Edit Date: 2021-06-09
                'Purpose: Get the value of Taqat checkbox

                If (CheckBox1.Checked) Then
                    ClsEmployees.HasTaqat = True
                Else
                    ClsEmployees.HasTaqat = False
                End If

                'End of edit

                If ClsEmployees.ID > 0 Then

                    If txtBankAccount.Text <> "" Then
                        Dim checkCnt = "select top 1 Code from hrs_employees where BankAccountNumber = '" & txtBankAccount.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                        Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
                        If cnt <> "" Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Bank Account Used Before Employee No. /الحساب البنكى مستخدم سابقا للموظف رقم") & cnt)
                            Exit Sub
                        End If
                    End If
                    If txtIdentity.Text <> "" Then
                        Dim checkCnt = "select top 1 Code from hrs_employees where SSnNo = '" & txtIdentity.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                        Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
                        If cnt <> "" Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " ID NO Used Before Employee No. /رقم الهوية مستخدم سابقا للموظف رقم") & cnt)
                            Exit Sub
                        End If
                    End If
                    txtEmpId.Value = ClsEmployees.ID
                    lblEmpID.Text = ClsEmployees.ID
                    ClsEmployees.Update("code='" & txtCode.Text & "'")

                    'Dim clsEmp As New Clshrs_Employees(Page)
                    'clsEmp.Find(" Code='" & txtManager.Text & "'")
                    'If clsEmp.ID > 0 Then
                    '    Dim Mangcommand As String = "update hrs_employees set ManagerID = " & clsEmp.ID & " where ID = " & txtEmpId.Value
                    '    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Mangcommand)
                    'Else
                    '    Dim Mangcommand As String = "update hrs_employees set ManagerID = null where ID = " & txtEmpId.Value
                    '    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Mangcommand)
                    'End If
                Else
                    'Added By Mohannad
                    '2022-06-15
                    '-----------------------------------------------
                    If ClsEmployees.Find("SSnNo LIKE '" & txtIdentity.Text.Trim() & "'") Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " The SSNO is used /رقم الهوية مستخدم لموظف آخر"))
                        Exit Sub
                    End If
                    'If String.IsNullOrEmpty(txtWorkE_Mail.Text) Then
                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to enter Work E-mail /عفوا...لابد من اخال بريد العمل"))
                    '    Exit Sub
                    'End If
                    If DdlNationality.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select nationality /عفوا...لابد من تحديد الجنسية "))
                        Exit Sub
                    End If
                    If ddlBranch.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Branch /عفوا...لابد من تحديد الفرع "))
                        Exit Sub
                    End If
                    If ddlDepartment.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Department /عفوا...لابد من تحديد القسم "))
                        Exit Sub
                    End If
                    If ddlSponsor.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Sponsor /عفوا...لابد من تحديد الكفيل "))
                        Exit Sub
                    End If
                    If ddlContractType.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Contract Type /عفوا...لابد من تحديد نوع العقد "))
                        Exit Sub
                    End If
                    If ddlProfessions.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Profession   /عفوا...لابد من تحديد المهنة "))
                        Exit Sub
                    End If
                    If ddlGradeStep.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Grade step   /عفوا...لابد من تحديد الرتبة "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(TxtPositionCode.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Position   /عفوا...لابد من تحديد الوظيفة "))
                        Exit Sub
                    End If
                    If ddlEmployeeClass.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Employee Class   /عفوا...لابد من تحديد الفئة "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(JoinDate.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Join Date /عفوا...لابد من ادخال تاريخ المباشرة "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(txtIdentity.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Identity No  /عفوا...لابد من ادخال رقم الهوية  "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(txtStartDate.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Start Date   /عفوا...لابد من ادخال تاريخ البدء  "))
                        Exit Sub
                    End If
                    If Not AssignValues_Employees() Then
                        Exit Sub
                    End If
                    '-----------------------------------------------
                    If txtBankAccount.Text <> "" Then
                        Dim checkCnt = "select top 1 Code from hrs_employees where BankAccountNumber = '" & txtBankAccount.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                        Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
                        If cnt <> "" Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Bank Account Used Before Employee No. /الحساب البنكى مستخدم سابقا للموظف رقم") & cnt)
                            Exit Sub
                        End If
                    End If
                    If txtIdentity.Text <> "" Then
                        Dim checkCnt = "select top 1 Code from hrs_employees where SSnNo = '" & txtIdentity.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                        Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
                        If cnt <> "" Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " ID NO Used Before Employee No. /رقم الهوية مستخدم سابقا للموظف رقم") & cnt)
                            Exit Sub
                        End If
                    End If
                    If CheckPositionsCount(txtCode.Text, TxtPositionID.Value) = False Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Sorry...You Can't Exceed The maximum No Of Employees For This Position. /عفوا لا يمكن تجاوز الحد الاقصي للعدد الموظفين في هذه الوظيفة"))
                        Exit Sub
                    End If

                    'Get Next Code
                    Dim Cls_Companies As New Clssys_Companies(Page)
                    Dim IntSeqLength As Integer = 0
                    Dim Retcode As String = ""
                    If Cls_Companies.Find("ID=" & Cls_Companies.MainCompanyID) = True Then
                        If Cls_Companies.HasSequence = True Then
                            'If txtCode.Text = "" Or txtCode.Text = "Auto" Or txtCode.Text = "تلقائى" Then
                            IntSeqLength = Cls_Companies.SequenceLength
                            Dim strcommand As String = "select Max(isnull(convert(int,Substring(Code,CHARINDEX('" & Cls_Companies.Separator & "',Code)+1,LEN(Code))),0)) + 1 from Hrs_Employees where Substring(Code,CHARINDEX('" & Cls_Companies.Separator & "',Code)+1,LEN(Code)) NOT LIKE '%[a-z]%' and CompanyID = " & Cls_Companies.MainCompanyID
                            Dim strnextcode As String = ""
                            Try
                                strnextcode = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Cls_Companies.ConnectionString, Data.CommandType.Text, strcommand)
                            Catch ex As Exception
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
                                Exit Sub
                            End Try
                            Retcode = strnextcode
                            For I As Integer = 0 To IntSeqLength - strnextcode.Length - 1
                                Retcode = "0" + Retcode
                            Next
                            '' Prefix
                            Dim prfx As String = ""
                            If (Cls_Companies.Prefix = 0) Then
                                prfx = ""
                            ElseIf (Cls_Companies.Prefix = 1) Then
                                Dim cls As New Clssys_Branches(Me)
                                If cls.Find("ID = " & ddlBranch.SelectedValue) Then
                                    prfx = cls.Code
                                End If
                            ElseIf (Cls_Companies.Prefix = 2) Then
                                Dim cls As New Clssys_Departments(Me)
                                If cls.Find("ID = " & TxtPositionID.Value) Then
                                    prfx = cls.Code
                                End If
                            ElseIf (Cls_Companies.Prefix = 3) Then
                                Dim cls As New Clshrs_Positions(Me)
                                If cls.Find("ID = " & TxtPositionID.Value) Then
                                    prfx = cls.Code
                                End If
                            ElseIf (Cls_Companies.Prefix = 4) Then
                                Dim cls As New Clshrs_ContractTypes(Me)
                                If cls.Find("ID = " & ddlContractType.SelectedValue) Then
                                    prfx = cls.Code
                                End If
                            End If

                            txtCode.Text = prfx & IIf(prfx = "", "", Cls_Companies.Separator) & Retcode
                            TextBox_MachineCode.Text = txtCode.Text
                            ClsEmployees.Code = txtCode.Text
                            ClsEmployees.MachineCode = TextBox_MachineCode.Text
                            If txtCode.Text = "" Then
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                                Exit Sub
                            End If
                            ' End If
                        End If
                    End If
                    'End Get Code

                    AssignValues_Employees()
                    ClsEmployees.Save()
                    ClsEmployees.Find("Code='" & txtCode.Text & "'")

                    'New Vacation Balance
                    Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Me)
                    ClsEmployeeClasses.Find("ID=" & ddlEmployeeClass.SelectedValue)
                    If ClsEmployeeClasses.AdvanceBalance And ClsEmployeeClasses.AddBalanceInAddEmp Then
                        Dim ds As DataSet = ClsEmployeeClasses.GetEmployeeClassAnnualVacations(ClsEmployeeClasses.ID)
                        Dim allDays As Decimal = 1
                        Dim allVacDays As Decimal = 0
                        Dim moreOneYear As Boolean = False
                        If ds.Tables(0).Rows.Count > 0 Then
                            With ds.Tables(0).Rows(0)
                                allDays = .Item("RequiredWorkingMonths")
                                allVacDays = .Item("DurationDays")

                            End With
                        End If
                        moreOneYear = ClsEmployeeClasses.AccumulatedBalance

                        Dim dayValue = allVacDays / allDays
                        Dim currentYear As Integer = SetDate(txtStartDate.Text, txtStartDate.Text).Year
                        Dim endOfYear As DateTime = New DateTime(currentYear, 12, 31)
                        Dim myDays As Integer = DateDiff(DateInterval.Day, SetDate(txtStartDate.Text, txtStartDate.Text), endOfYear.Date)
                        Dim myBalance = myDays * dayValue
                        Dim expireDate As Date = endOfYear
                        If moreOneYear Then
                            myDays = DateDiff(DateInterval.Day, SetDate(txtStartDate.Text, txtStartDate.Text), SetDate(txtStartDate.Text, txtStartDate.Text).AddDays(allDays))
                            myBalance = myDays * dayValue
                            expireDate = SetDate(txtStartDate.Text, txtStartDate.Text).AddDays(allDays)
                        End If
                        Dim checkCnt = "INSERT INTO [dbo].[hrs_VacationsBalance] ([EmployeeID],[Year],[Balance],[Consumed],[Remaining],[BalanceTypeID],[ExpireDate],[Src],[Remarks],[Reguser],[RegDate],DueDate) VALUES (" & ClsEmployees.ID & "," & currentYear & "," & myBalance & ",0," & myBalance & ",1,'" & expireDate.ToString("yyyy-MM-dd") & "'," & "'frmEmployees'" & "," & "''" & ",'" & ClsEmployees.RegUserID & "',getdate(),'" & SetDate(txtStartDate.Text, txtStartDate.Text).ToString("yyyy-MM-dd") & "')"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)

                    End If


                        txtEmpId.Value = ClsEmployees.ID
                    lblEmpID.Text = ClsEmployees.ID
                    Dim clsEmp As New Clshrs_Employees(Page)
                    clsEmp.Find(" Code='" & txtManager.Text & "'")
                    If clsEmp.ID > 0 Then
                        Dim Mangcommand As String = "update hrs_employees set ManagerID = " & clsEmp.ID & " where ID = " & txtEmpId.Value
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Mangcommand)
                    Else
                        Dim Mangcommand As String = "update hrs_employees set ManagerID = null where ID = " & txtEmpId.Value
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Mangcommand)
                    End If
                End If

                If ddlLastEducations.SelectedIndex > 0 Then
                    Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
                    If clsEmpEducations.Find("EmployeeID=" & ClsEmployees.ID) Then
                        clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
                        clsEmpEducations.GraduationDate = SetDate(txtGraduationDate.Text, txtGraduationDate.Text)
                        clsEmpEducations.Update("EmployeeID=" & ClsEmployees.ID)
                    Else
                        clsEmpEducations.EmployeeID = ClsEmployees.ID
                        clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
                        clsEmpEducations.GraduationDate = SetDate(txtGraduationDate.Text, txtGraduationDate.Text)
                        clsEmpEducations.Save()
                    End If
                End If

                clsContracts = New Clshrs_Contracts(Page)
                Dim ObjDataHandler As New Venus.Shared.DataHandler
                Dim StrSerial As String = String.Empty
                If lblContractNo.Text = String.Empty Or lblContractNo.Text = "0" Or lblContractNo.Text = "Auto" Or lblContractNo.Text = "تلقائى" Then
                    ObjDataHandler.GetLastSerial(clsContracts.Table, "Number", StrSerial, clsContracts.ConnectionString)
                    lblContractNo.Text = StrSerial
                End If
                If lblContractNo.Text <> String.Empty Or lblContractNo.Text <> "0" Then
                    clsContracts = New Clshrs_Contracts(Page)
                    ClsContractTransactions = New Clshrs_ContractsTransactions(Page)
                    If Not txtStartDate.Value Is Nothing And lblContractNo.Text.Length > 0 Then
                        If SavePart_Contracts() Then
                            clsContracts.Find("Number=" & lblContractNo.Text)
                            txtContractId.Value = clsContracts.ID
                        Else
                            txtStartDate.Focus()
                            Exit Sub
                        End If
                    End If
                End If

                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployees.Table, ClsEmployees.ID)
                value.Text = ""
                New_Part()
                CreateOtherFields(0)
            Case "Save"

                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If Not AssignValues_Employees() Then
                    Exit Sub
                End If

                'Edited by: Hassan Kurdi
                'Edit Date: 2021-06-09
                'Purpose: Get the value of Taqat checkbox

                If (CheckBox1.Checked) Then
                    ClsEmployees.HasTaqat = True
                Else
                    ClsEmployees.HasTaqat = False
                End If

                'End of edit

                If ClsEmployees.ID > 0 Then
                    txtEmpId.Value = ClsEmployees.ID
                    lblEmpID.Text = ClsEmployees.ID
                    If txtBankAccount.Text <> "" Then
                        Dim checkCnt = "select top 1 Code from hrs_employees where BankAccountNumber = '" & txtBankAccount.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                        Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
                        If cnt <> "" Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Bank Account Used Before Employee No. /الحساب البنكى مستخدم سابقا للموظف رقم") & cnt)
                            Exit Sub
                        End If
                    End If
                    If txtIdentity.Text <> "" Then
                        Dim checkCnt = "select top 1 Code from hrs_employees where SSnNo = '" & txtIdentity.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                        Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
                        If cnt <> "" Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " ID NO Used Before Employee No. /رقم الهوية مستخدم سابقا للموظف رقم") & cnt)
                            Exit Sub
                        End If
                    End If
                    'If String.IsNullOrEmpty(txtWorkE_Mail.Text) Then
                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to enter Work E-mail /عفوا...لابد من اخال بريد العمل"))
                    '    Exit Sub
                    'End If
                    If DdlNationality.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select nationality /عفوا...لابد من تحديد الجنسية "))
                        Exit Sub
                    End If
                    If ddlBranch.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Branch /عفوا...لابد من تحديد الفرع "))
                        Exit Sub
                    End If
                    If ddlDepartment.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Department /عفوا...لابد من تحديد القسم "))
                        Exit Sub
                    End If
                    If ddlSponsor.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Sponsor /عفوا...لابد من تحديد الكفيل "))
                        Exit Sub
                    End If
                    If ddlContractType.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Contract Type /عفوا...لابد من تحديد نوع العقد "))
                        Exit Sub
                    End If
                    If ddlProfessions.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Profession   /عفوا...لابد من تحديد المهنة "))
                        Exit Sub
                    End If
                    If ddlGradeStep.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Grade step   /عفوا...لابد من تحديد الرتبة "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(TxtPositionCode.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Position   /عفوا...لابد من تحديد الوظيفة "))
                        Exit Sub
                    End If
                    If ddlEmployeeClass.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Employee Class   /عفوا...لابد من تحديد الفئة "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(JoinDate.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Join Date /عفوا...لابد من ادخال تاريخ المباشرة "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(txtIdentity.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Identity No  /عفوا...لابد من ادخال رقم الهوية  "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(txtStartDate.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Start Date   /عفوا...لابد من ادخال تاريخ البدء  "))
                        Exit Sub
                    End If
                    ClsEmployees.Update("code='" & txtCode.Text & "'")

                    'Dim clsEmp As New Clshrs_Employees(Page)
                    'clsEmp.Find(" Code='" & txtManager.Text & "'")
                    'If clsEmp.ID > 0 Then
                    '    Dim Mangcommand As String = "update hrs_employees set ManagerID = " & clsEmp.ID & " where ID = " & txtEmpId.Value
                    '    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Mangcommand)
                    'Else
                    '    Dim Mangcommand As String = "update hrs_employees set ManagerID = null where ID = " & txtEmpId.Value
                    '    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Mangcommand)
                    'End If
                Else

                    If txtBankAccount.Text <> "" Then
                        Dim checkCnt = "select top 1 Code from hrs_employees where BankAccountNumber = '" & txtBankAccount.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                        Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
                        If cnt <> "" Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Bank Account Used Before Employee No. /الحساب البنكى مستخدم سابقا للموظف رقم") & cnt)
                            Exit Sub
                        End If
                    End If
                    If txtIdentity.Text <> "" Then
                        Dim checkCnt = "select top 1 Code from hrs_employees where SSnNo = '" & txtIdentity.Text & "' and Code <> '" & txtCode.Text & "' and branchID = '" & ddlBranch.SelectedValue & "'"
                        Dim cnt As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)
                        If cnt <> "" Then
                            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " ID NO Used Before Employee No. /رقم الهوية مستخدم سابقا للموظف رقم") & cnt)
                            Exit Sub
                        End If
                    End If

                    'Added By Mohannad
                    '2022-06-15
                    '-----------------------------------------------
                    If ClsEmployees.Find("SSnNo LIKE '" & txtIdentity.Text.Trim() & "'") Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " The SSNO is used /رقم الهوية مستخدم لموظف آخر"))
                        Exit Sub
                    End If
                    'If String.IsNullOrEmpty(txtWorkE_Mail.Text) Then
                    '    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to enter Work E-mail /عفوا...لابد من اخال بريد العمل"))
                    '    Exit Sub
                    'End If
                    If DdlNationality.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select nationality /عفوا...لابد من تحديد الجنسية "))
                        Exit Sub
                    End If
                    If ddlBranch.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Branch /عفوا...لابد من تحديد الفرع "))
                        Exit Sub
                    End If
                    If ddlDepartment.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Department /عفوا...لابد من تحديد القسم "))
                        Exit Sub
                    End If
                    If ddlSponsor.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Sponsor /عفوا...لابد من تحديد الكفيل "))
                        Exit Sub
                    End If
                    If ddlContractType.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Contract Type /عفوا...لابد من تحديد نوع العقد "))
                        Exit Sub
                    End If
                    If ddlProfessions.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Profession   /عفوا...لابد من تحديد المهنة "))
                        Exit Sub
                    End If
                    If ddlGradeStep.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select Grade step   /عفوا...لابد من تحديد الرتبة "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(TxtPositionCode.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Position   /عفوا...لابد من تحديد الوظيفة "))
                        Exit Sub
                    End If
                    If ddlEmployeeClass.SelectedValue = 0 Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to select  Employee Class   /عفوا...لابد من تحديد الفئة "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(JoinDate.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Join Date /عفوا...لابد من ادخال تاريخ المباشرة "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(txtIdentity.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Identity No  /عفوا...لابد من ادخال رقم الهوية  "))
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(txtStartDate.Text) Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Sorry... you have to Enter Start Date   /عفوا...لابد من ادخال تاريخ البدء  "))
                        Exit Sub
                    End If
                    If CheckPositionsCount(txtCode.Text, TxtPositionID.Value) = False Then
                        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Sorry...You Can't Exceed The maximum No Of Employees For This Position. /عفوا لا يمكن تجاوز الحد الاقصي للعدد الموظفين في هذه الوظيفة"))
                        Exit Sub
                    End If
                    If Not AssignValues_Employees() Then
                        Exit Sub
                    End If
                    '-----------------------------------------------
                    'Get Next Code
                    Dim Cls_Companies As New Clssys_Companies(Page)
                    Dim IntSeqLength As Integer = 0
                    Dim Retcode As String = ""
                    If Cls_Companies.Find("ID=" & Cls_Companies.MainCompanyID) = True Then
                        If Cls_Companies.HasSequence = True Then
                            'If txtCode.Text = "" Or txtCode.Text = "Auto" Or txtCode.Text = "تلقائى" Then
                            IntSeqLength = Cls_Companies.SequenceLength
                            Dim strcommand As String = "select Max(isnull(convert(int,Substring(Code,CHARINDEX('" & Cls_Companies.Separator & "',Code)+1,LEN(Code))),0)) + 1 from Hrs_Employees where Substring(Code,CHARINDEX('" & Cls_Companies.Separator & "',Code)+1,LEN(Code)) NOT LIKE '%[a-z]%' and CompanyID = " & Cls_Companies.MainCompanyID
                            Dim strnextcode As String = ""
                            Try
                                strnextcode = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Cls_Companies.ConnectionString, Data.CommandType.Text, strcommand)
                            Catch ex As Exception
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
                                Exit Sub
                            End Try
                            Retcode = strnextcode
                            For I As Integer = 0 To IntSeqLength - strnextcode.Length - 1
                                Retcode = "0" + Retcode
                            Next
                            '' Prefix
                            Dim prfx As String = ""
                            If (Cls_Companies.Prefix = 0) Then
                                prfx = ""
                            ElseIf (Cls_Companies.Prefix = 1) Then
                                Dim cls As New Clssys_Branches(Me)
                                If cls.Find("ID = " & ddlBranch.SelectedValue) Then
                                    prfx = cls.Code
                                End If
                            ElseIf (Cls_Companies.Prefix = 2) Then
                                Dim cls As New Clssys_Departments(Me)
                                If cls.Find("ID = " & ddlDepartment.SelectedValue) Then
                                    prfx = cls.Code
                                End If
                            ElseIf (Cls_Companies.Prefix = 3) Then
                                Dim cls As New Clshrs_Positions(Me)
                                If cls.Find("ID = " & TxtPositionID.Value) Then
                                    prfx = cls.Code
                                End If
                            ElseIf (Cls_Companies.Prefix = 4) Then
                                Dim cls As New Clshrs_ContractTypes(Me)
                                If cls.Find("ID = " & ddlContractType.SelectedValue) Then
                                    prfx = cls.Code
                                End If
                            End If

                            txtCode.Text = prfx & IIf(prfx = "", "", Cls_Companies.Separator) & Retcode
                            TextBox_MachineCode.Text = txtCode.Text
                            ClsEmployees.Code = txtCode.Text
                            ClsEmployees.MachineCode = TextBox_MachineCode.Text
                            If txtCode.Text = "" Then
                                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Please Enter Code /برجاء إدخال الكود"))
                                Exit Sub
                            End If
                            '  End If
                        End If
                    End If
                    'End Get Code


                    ClsEmployees.Save()
                    ClsEmployees.Find("Code='" & txtCode.Text & "'")
                    txtEmpId.Value = ClsEmployees.ID
                    lblEmpID.Text = ClsEmployees.ID
                    'New Vacation Balance
                    Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Me)
                    ClsEmployeeClasses.Find("ID=" & ddlEmployeeClass.SelectedValue)
                    If ClsEmployeeClasses.AdvanceBalance And ClsEmployeeClasses.AddBalanceInAddEmp Then
                        Dim ds As DataSet = ClsEmployeeClasses.GetEmployeeClassAnnualVacations(ClsEmployeeClasses.ID)
                        Dim allDays As Decimal = 1
                        Dim allVacDays As Decimal = 0
                        If ds.Tables(0).Rows.Count > 0 Then
                            With ds.Tables(0).Rows(0)
                                allDays = .Item("RequiredWorkingMonths")
                                allVacDays = .Item("DurationDays")
                            End With
                        End If
                        Dim dayValue = allVacDays / allDays
                        Dim currentYear As Integer = SetDate(txtStartDate.Text, txtStartDate.Text).Year
                        Dim endOfYear As DateTime = New DateTime(currentYear, 12, 31)
                        Dim myDays As Integer = DateDiff(DateInterval.Day, SetDate(txtStartDate.Text, txtStartDate.Text), endOfYear.Date)
                        Dim myBalance = myDays * dayValue
                        Dim expireDate As Date = endOfYear
                        If ClsEmployeeClasses.AccumulatedBalance Then
                            myDays = DateDiff(DateInterval.Day, SetDate(txtStartDate.Text, txtStartDate.Text), SetDate(txtStartDate.Text, txtStartDate.Text).AddDays(allDays))
                            myBalance = myDays * dayValue
                            expireDate = SetDate(txtStartDate.Text, txtStartDate.Text).AddDays(allDays)
                        End If
                        Dim checkCnt = "INSERT INTO [dbo].[hrs_VacationsBalance] ([EmployeeID],[Year],[Balance],[Consumed],[Remaining],[BalanceTypeID],[ExpireDate],[Src],[Remarks],[Reguser],[RegDate],DueDate) VALUES (" & ClsEmployees.ID & "," & currentYear & "," & myBalance & ",0," & myBalance & ",1,'" & expireDate.ToString("yyyy-MM-dd") & "'," & "'frmEmployees'" & "," & "''" & ",'" & ClsEmployees.RegUserID & "',getdate(),'" & SetDate(txtStartDate.Text, txtStartDate.Text).ToString("yyyy-MM-dd") & "')"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, CommandType.Text, checkCnt)

                    End If

                    Dim clsEmp As New Clshrs_Employees(Page)
                    clsEmp.Find(" Code='" & txtManager.Text & "'")
                    If clsEmp.ID > 0 Then
                        Dim Mangcommand As String = "update hrs_employees set ManagerID = " & clsEmp.ID & " where ID = " & txtEmpId.Value
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Mangcommand)
                    Else
                        Dim Mangcommand As String = "update hrs_employees set ManagerID = null where ID = " & txtEmpId.Value
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Mangcommand)
                    End If
                End If

                If ddlLastEducations.SelectedIndex > 0 Then
                    Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
                    If clsEmpEducations.Find("EmployeeID=" & ClsEmployees.ID) Then
                        clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
                        clsEmpEducations.GraduationDate = SetDate(txtGraduationDate.Text, txtGraduationDate.Text)
                        clsEmpEducations.Update("EmployeeID=" & ClsEmployees.ID)
                    Else
                        clsEmpEducations.EmployeeID = ClsEmployees.ID
                        clsEmpEducations.EducationTypeID = ddlLastEducations.SelectedValue
                        clsEmpEducations.GraduationDate = SetDate(txtGraduationDate.Text, txtGraduationDate.Text)
                        clsEmpEducations.Save()
                    End If
                End If

                clsContracts = New Clshrs_Contracts(Page)
                Dim ObjDataHandler As New Venus.Shared.DataHandler
                Dim StrSerial As String = String.Empty
                If lblContractNo.Text = String.Empty Or lblContractNo.Text = "0" Or lblContractNo.Text = "Auto" Or lblContractNo.Text = "تلقائى" Then
                    ObjDataHandler.GetLastSerial(clsContracts.Table, "Number", StrSerial, clsContracts.ConnectionString)
                    lblContractNo.Text = StrSerial
                End If
                If lblContractNo.Text <> String.Empty Or lblContractNo.Text <> "0" Then
                    ClsContractTransactions = New Clshrs_ContractsTransactions(Page)
                    If Not txtStartDate.Value Is Nothing And lblContractNo.Text.Length > 0 Then
                        If SavePart_Contracts() Then
                            clsContracts.Find("Number=" & lblContractNo.Text)
                            txtContractId.Value = clsContracts.ID
                        Else
                            txtStartDate.Focus()
                            Exit Sub
                        End If
                    End If
                End If

                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                clsMainOtherFields.CollectDataAndSave(value.Text, ClsEmployees.Table, ClsEmployees.ID)
                value.Text = ""
                CheckCode()
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me.Page, ObjNavigationHandler.SetLanguage(Me.Page, " This Employee Saved With Code No : / هذا الموظف تم حفظه برقم كود : ") & ClsEmployees.Code)
            Case "New"
                New_Part()
                CreateOtherFields(0)
            Case "Delete"
                ClsEmployees.IsProjectRelated = False
                ClsEmployees.IsSpecialForce = False
                ClsEmployees.WHours = 0
                ClsEmployees.Delete("Code='" & txtCode.Text & "'")
                AfterOperation()
            Case "Property"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmPropertyScreen.aspx?ID=" & ClsEmployees.ID & "&TableName=" & ClsEmployees.Table, 800, 600, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Property", False)
            Case "Remarks"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmRemarks.aspx?ID=" & ClsEmployees.ID & "&TableName=" & ClsEmployees.Table, 410, 210, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
            Case "History"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If ClsEmployees.ID > 0 Then
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmEmployeeDecisionsHistoryPopUp.aspx?ID=" & txtCode.Text & "&TableName=" & ClsEmployees.Table, 1300, 840, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "Remarks", False)
                End If

            Case "Print"
                'Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If ClsEmployees.ID > 0 Then
                    'ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">OpenPrintedScreen(" & ClsEmployees.Code & ");</script>")
                    Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmployeeCode&preview=1&ReportCode=EmployeeDetails&sq0=''&v=" & ClsEmployees.Code, 700, 490, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
                End If


            Case "Other Fields"
                Dim clsObjOtherFields As New ClsSys_OtherFields(Page)
                Dim clsSysObjects As New Clssys_Objects(Page)
                Dim tablename As String = ClsEmployees.Table
                ClsEmployees.Find(" code = '" & txtCode.Text & "'")
                Dim recordID As Integer = ClsEmployees.ID
                clsSysObjects.Find(" Code = REPLACE('" & tablename & "',' ' ,'')")
                With clsObjOtherFields
                    If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                        Dim OtherFieldID As Integer = .ID
                        Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmOtherFieldsDynamic.aspx?tableName=" & tablename & "&ObjectId=" & .ObjectID & "&RecordId=" & ClsEmployees.ID, 602, 306, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "OtherFields", False)
                    End If
                End With
            Case "Exit"
                ClsEmployees.Find(" Code= '" & txtCode.Text & "'")
                If ClsEmployees.ID > 0 Then
                    Dim Ds As Data.DataSet = ClsEmployees.DataSet
                    If Not AssignValues_Employees() Then
                        Exit Sub
                    End If
                    If ClsEmployees.CheckDiff(ClsEmployees, Ds, "") Then
                        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Click", "<script language=""javascript""> CHeckDiff();</script>")
                    Else
                        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                    End If
                Else
                    Venus.Shared.Web.ClientSideActions.ClosePage(Page)
                End If
            Case "First"
                ClsEmployees.FirstRecord()
                txtCode.Text = ClsEmployees.Code
                lblEmpID.Text = ClsEmployees.ID
                CheckCode()
            Case "Previous"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If Not ClsEmployees.previousRecord() Then
                    ClsEmployees.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the first page /هذه أول صفحة"))
                End If
                txtCode.Text = ClsEmployees.Code
                lblEmpID.Text = ClsEmployees.ID
                CheckCode()
            Case "Next"
                ClsEmployees.Find("Code='" & txtCode.Text & "'")
                If Not ClsEmployees.NextRecord() Then
                    ClsEmployees.Find("Code='" & txtCode.Text & "'")
                    Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " This is the last page /هذه أخر صفحة"))
                End If
                txtCode.Text = ClsEmployees.Code
                lblEmpID.Text = ClsEmployees.ID
                CheckCode()
            Case "Last"
                ClsEmployees.LastRecord()
                txtCode.Text = ClsEmployees.Code
                lblEmpID.Text = ClsEmployees.ID
                CheckCode()
            Case "Import"
                Venus.Shared.Web.ClientSideActions.OpenWindowAdv(Page, "frmIqamaValid.aspx", 800, 170, False, Venus.Shared.Web.ClientSideActions.WINDOW_TARGET._Blank, "wWindO", False, True, False, False, False, False, False, False, False)
        End Select
    End Sub
    Protected Sub txtCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()

    End Sub
    'Protected Sub BTN_Signature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTN_Signature.Click
    '    If FileUpload1.HasFile Then
    '        FileUpload1.SaveAs(Server.MapPath("EmployeeSignature//" & txtCode.Text & ".png"))
    '    End If

    'End Sub
    Private Sub CheckGeneralSetup()
        Dim PreventChangeContractEndDate As Boolean
        Dim dt As DataSet
        Dim struseccenter As String = "select PreventChangeContractEndDate from sys_SystemConfig"
        dt = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsEmployees.ConnectionString, Data.CommandType.Text, struseccenter)
        If dt.Tables(0).Rows.Count > 0 Then
            PreventChangeContractEndDate = dt.Tables(0).Rows(0)("PreventChangeContractEndDate")
            If PreventChangeContractEndDate Then
                txtEndDate.Enabled = False
            End If
        Else
            txtEndDate.Enabled = True

        End If
    End Sub
    Protected Sub TxtPositionCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPositionCode.TextChanged, TxtPositionCode.DataBinding

        If Not String.IsNullOrEmpty(TxtPositionCode.Text) Then
            Dim str As String
            Dim PositionCode As String = TxtPositionCode.Text
            Dim PositionName As String
            Dim ClsPosition As New ClsBasicFiles(Me.Page, "hrs_Positions")
            ClsPosition.Find("Code = " & PositionCode & "")
            TxtPositionID.Value = ClsPosition.ID
            If ProfileCls.CurrentLanguage = "Ar" Then
                PositionName = ClsPosition.ArbName
            Else
                PositionName = ClsPosition.EngName
            End If

            TxtPositionName.Text = PositionName

        Else

            TxtPositionName.Text = String.Empty
            TxtPositionID.Value = 0
        End If
        If TxtPositionID.Value > 0 Then

            Dim PositionName As String
            Dim ClsPosition As New ClsBasicFiles(Me.Page, "hrs_Positions")
            ClsPosition.Find("ID = " & TxtPositionID.Value & "")
            TxtPositionCode.Text = ClsPosition.Code
            If ProfileCls.CurrentLanguage = "Ar" Then
                PositionName = ClsPosition.ArbName
            Else
                PositionName = ClsPosition.EngName
            End If

            TxtPositionName.Text = PositionName

        Else

            TxtPositionName.Text = String.Empty

        End If



    End Sub
    Protected Sub TxtCostCode1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCostCode1.TextChanged, TxtCostCode1.DataBinding

        If Not String.IsNullOrEmpty(TxtCostCode1.Text) Then
            Dim str As String

            Dim CostCode As String = TxtCostCode1.Text
            Dim CostCenterName As String

            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters1 where Code='" & CostCode & "' "
            Dim Cost1Name As String = ""
            Try
                Cost1Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                TxtCostName1.Text = Cost1Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try
        Else

            TxtCostName1.Text = String.Empty

        End If



    End Sub

    Protected Sub TxtCostCode2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCostCode2.TextChanged
        If Not String.IsNullOrEmpty(TxtCostCode2.Text) Then
            Dim str As String
            Dim CostCode As String = TxtCostCode2.Text
            Dim CostCenterName As String

            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters2 where Code='" & CostCode & "' "
            Dim Cost2Name As String = ""
            Try
                Cost2Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                TxtCostName2.Text = Cost2Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try
        Else

            TxtCostName2.Text = String.Empty
        End If


    End Sub
    Protected Sub TxtCostCode3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCostCode3.TextChanged
        If Not String.IsNullOrEmpty(TxtCostCode3.Text) Then
            Dim str As String
            Dim CostCode As String = TxtCostCode3.Text
            Dim CostCenterName As String

            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters3 where Code='" & CostCode & "' "
            Dim Cost3Name As String = ""
            Try
                Cost3Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                TxtCostName3.Text = Cost3Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try
        Else

            TxtCostName3.Text = String.Empty
        End If


    End Sub

    Protected Sub TxtCostCode4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCostCode4.TextChanged
        If Not String.IsNullOrEmpty(TxtCostCode4.Text) Then
            Dim str As String
            Dim CostCode As String = TxtCostCode4.Text
            Dim CostCenterName As String

            If ProfileCls.CurrentLanguage = "Ar" Then
                CostCenterName = "ArbName"
            Else
                CostCenterName = "EngName"
            End If
            str = "select " & CostCenterName & " From fcs_CostCenters4 where Code='" & CostCode & "' "
            Dim Cost4Name As String = ""
            Try
                Cost4Name = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                TxtCostName4.Text = Cost4Name
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try
        Else
            TxtCostName4.Text = String.Empty
        End If

    End Sub
    Protected Sub btnAddContract_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnAddContract.Click
        ClsEmployees = New Clshrs_Employees(Me)
        clsContracts = New Clshrs_Contracts(Me.Page)
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim StrSerial As String = String.Empty
        New_Part_Contracts()
        If ClsEmployees.Find("Code='" & txtCode.Text & "'") Then
            ObjDataHandler.GetLastSerial(clsContracts.Table, "Number", StrSerial, clsContracts.ConnectionString)
            lblContractNo.Text = StrSerial
        End If
    End Sub
#End Region

#Region "Private Functions Contract"

    Private Function SavePart_Contracts() As Boolean
        Dim IntEmployeeId As Integer = txtEmpId.Value
        Dim StrMode As String = String.Empty
        ClsEmployeeJoin = New Clshrs_EmployeesJoins(Me.Page)
        ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployeeJoin.ConnectionString)
        Dim ClsContracts As New Clshrs_Contracts(Me.Page)
        ClsContracts.Find("Number='" & lblContractNo.Text & "'")

        If Not txtEndDate.Text = "  /  /    " Then
            If SetDate(txtStartDate.Text, txtStartDate.Text) > SetDate(txtEndDate.Text, txtEndDate.Text) Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me.Page, "Data Can't be saved ,  Invalid Contract Dates ! Check Entered Dates ! ")
                Return False
            End If
        End If
        If ClsContracts.ID > 0 Then
            If CheckUpdateContract_New() = 0 Then
                If ClsEmployeeJoin.Find("EmployeeID=" & IntEmployeeId & " And JoinDate ='" & Format(ClsContracts.StartDate, "dd/MM/yyyy") & "'") Then
                    ClsEmployeeJoin.EmployeeId = IntEmployeeId
                    ClsEmployeeJoin.JoinDate = SetDate(txtStartDate.Text, txtStartDate.Text) 'IIf(txtStartDate.Text.Trim = "", Nothing, ClsContracts.SetHigriDate(txtStartDate.Value))
                    ClsEmployeeJoin.Update("ID=" & ClsEmployeeJoin.ID)
                End If
                If Not AssignValues_Contracts(ClsContracts) Then
                    Exit Function
                End If
                ClsContracts.Update("Number='" & lblContractNo.Text & "'")
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me.Page, "Data Can't be saved ,  Invalid Contract Dates ! Check Entered Dates ! ")
                txtStartDate.Value = Nothing
                txtEndDate.Value = Nothing
                txtStartDate.Focus()
                Return False
            End If
        Else
            '-------------------------------0257 MODIFIED-----------------------------------------
            If CheckUpdateContract_New() = 0 Then
                '-------------------------------=============-----------------------------------------
                If Not AssignValues_Contracts(ClsContracts) Then
                    Exit Function
                End If
                ClsContracts.Save()
                ClsContracts.Find("Number='" & lblContractNo.Text & "'")
                Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Me)
                Dim ds As DataSet = ClsEmployeeClasses.GetEmployeeClassVacations(ClsContracts.EmployeeClassID)
                ClsContracts.SetContractVacation(ClsContracts.ID, ds)

                Dim clsnationality As New Clssys_Nationality(Me.Page)
                If clsnationality.Find("ID = " & DdlNationality.SelectedValue) Then
                    Dim Str As String = "select * from hrs_TicketsPrices where canceldate is null and TicketsRoutesID = " & clsnationality.TravelRoute & " and TicketsClassesID = " & clsnationality.TravelClass
                    Dim dsPrices As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsContracts.ConnectionString, System.Data.CommandType.Text, Str)
                    If dsPrices.Tables(0).Rows.Count > 0 Then
                        Str = "delete from hrs_TicketsContarct where ContractID = " & ClsContracts.ID
                        Str &= Environment.NewLine & "insert into hrs_TicketsContarct (ContractID,TicketsRouteID,TicketsClassID,TotalCost,IsPaid,RegUserID,RegDate) values (" & ClsContracts.ID & "," & dsPrices.Tables(0).Rows(0)(2) & "," & dsPrices.Tables(0).Rows(0)(1) & "," & dsPrices.Tables(0).Rows(0)(3) & ",0," & ClsContracts.DataBaseUserRelatedID & ",getdate())"
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsContracts.ConnectionString, System.Data.CommandType.Text, Str)
                    End If
                End If

                If ClsEmployeeJoin.Find("EmployeeID=" & IntEmployeeId & " And JoinDate ='" & CDate(ClsContracts.StartDate).Date & "'") Then
                    ClsEmployeeJoin.EmployeeId = IntEmployeeId
                    ClsEmployeeJoin.JoinDate = SetDate(txtStartDate.Text, txtStartDate.Text)
                    ClsEmployeeJoin.Update("ID=" & ClsEmployeeJoin.ID)
                Else
                    ClsEmployeeJoin.EmployeeId = IntEmployeeId
                    ClsEmployeeJoin.JoinDate = SetDate(txtStartDate.Text, txtStartDate.Text)
                    ClsEmployeeJoin.Save()
                End If
                Dim Mangcommand As String = "update hrs_employees set ExcludeDate = null where ID = " & txtEmpId.Value
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ClsEmployees.ConnectionString, Data.CommandType.Text, Mangcommand)
            Else
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me.Page, "Invalid Contract Date! ")
                txtStartDate.Value = Nothing
                txtEndDate.Value = Nothing
                txtStartDate.Focus()
                Return False
            End If
        End If
        Return True
    End Function
    Private Function AssignValues_Contracts(ByRef ClsContracts As Clshrs_Contracts) As Boolean
        Dim IntEmployeeId As Integer = txtEmpId.Value
        Dim clsCompanies As New Clssys_Companies(Me.Page)
        Try
            With ClsContracts
                .Number = lblContractNo.Text
                .EmployeeID = IntEmployeeId
                Try
                    .ProfessionID = IIf(ddlProfessions.Items.Count = 0, 0, ddlProfessions.SelectedItem.Value)
                Catch ex As Exception
                    .ProfessionID = 0
                End Try
                Try
                    .ContractTypeId = IIf(ddlContractType.Items.Count = 0, 0, ddlContractType.SelectedItem.Value) 'DdlContractType.SelectedItem.Value

                Catch ex As Exception
                    .ContractTypeId = 0
                End Try
                Try
                    .PositionID = TxtPositionID.Value

                Catch ex As Exception
                    .PositionID = 0
                End Try
                Try
                    .EmployeeClassID = IIf(ddlEmployeeClass.Items.Count = 0, 0, ddlEmployeeClass.SelectedItem.Value) 'DdlEmployeeClass.SelectedItem.Value

                Catch ex As Exception
                    .EmployeeClassID = 0
                End Try
                Try
                    .CurrencyId = IIf(ddlCurrency.Items.Count = 0, 0, ddlCurrency.SelectedItem.Value) 'DdlCurrency.SelectedItem.Value

                Catch ex As Exception
                    .CurrencyId = 0
                End Try
                Try
                    .GradeStepId = IIf(ddlGradeStep.Items.Count = 0, 0, ddlGradeStep.SelectedItem.Value) 'DdlGradeStep.SelectedItem.Value
                Catch ex As Exception
                    .GradeStepId = 0
                End Try
                .StartDate = SetDate(txtStartDate.Text, txtStartDate.Text) 'IIf(txtStartDate.Value.Trim = "", Nothing, .SetHigriDate(txtStartDate.Text))
                .EndDate = SetDate(txtEndDate.Text, txtEndDate.Text) 'IIf(txtEndDate.Value.Trim = "", Nothing, .SetHigriDate(txtEndDate.Text))
                .ContractPeriod = wneContractDuration.Value

            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function CheckUpdateContract_New() As Integer
        Dim IntEmployeeId As Integer = txtEmpId.Value
        Dim ObjRow As Data.DataRow
        Dim Int As Integer = 0

        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim clsCompanies As New Clssys_Companies(Me.Page)
        clsCompanies.Find("ID = " & clsCompanies.MainCompanyID)
        Dim ClsGHCalender As New Clssys_GHCalendar(Me.Page)
        Dim GHStartDate As Date
        Dim GHEndDate As Date
        Dim ClsContracts As New Clshrs_Contracts(Page)

        GHStartDate = SetDate(txtStartDate.Text, txtStartDate.Text) ' IIf(txtStartDate.Text.Trim = "", Nothing, ClsGHCalender.SetHigriDate(txtStartDate.Value))
        GHEndDate = SetDate(txtEndDate.Text, txtEndDate.Text) ' IIf(txtEndDate.Text.Trim = "", Nothing, ClsGHCalender.SetHigriDate(txtEndDate.Value))

        ClsContracts.Find("employeeid=" & IntEmployeeId & "  And canceldate is null ")
        For Each ObjRow In ClsContracts.DataSet.Tables(0).Rows

            If (CInt(lblContractNo.Text) = CInt(ObjRow("Number"))) Then
                Continue For
            End If

            If IsDBNull(ObjRow.Item("EndDate")) Then
                If (GHEndDate.Year = 1) Then
                    Return 1
                End If
                If GHEndDate >= ObjRow.Item("StartDate") Then
                    Return 1
                End If
            Else
                If (GHEndDate.Year = 1) Then
                    If GHStartDate <= ObjRow.Item("EndDate") Then
                        Return 1
                    End If
                Else
                    If GHStartDate = ObjRow.Item("StartDate") Or GHEndDate = ObjRow.Item("EndDate") Then
                        Return 1
                    End If
                    If GHStartDate <= ObjRow.Item("EndDate") And GHEndDate > ObjRow.Item("EndDate") Then
                        Return 1
                    End If
                    If GHEndDate >= ObjRow.Item("StartDate") And GHStartDate < ObjRow.Item("StartDate") Then
                        Return 1
                    End If
                End If
            End If
        Next
        Return 0
    End Function
    Private Function GetContractsData(ByVal IntEmployeeId As Integer) As Boolean
        Dim ClsVacationsType As New Clshrs_VacationsTypes(Me.Page)
        ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsVacationsType.ConnectionString)
        Dim IntValidContract As Integer
        Dim blnFound As Boolean = False
        Dim ObjDs As Data.DataSet = Nothing
        Dim ClsContracts As New Clshrs_Contracts(Me.Page)
        ClsContracts.Find(" EmployeeId = " & IntEmployeeId & " And Isnull(CancelDate,'')= '' and (EndDate is null or EndDate > getdate()) order by ID desc")

        If IntEmployeeId > 0 Then
            If ClsContracts.ID = 0 Then
                btnAddContract.Visible = True
                lblNoContractNotify.Visible = True
                Dim StrSelectCommand = " Select TOP 1 EndofServiceDate from hrs_employeesJoins where employeeid = " & IntEmployeeId & " Order By EndofServiceDate Desc "
                Dim endServ = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, StrSelectCommand)
                If Not String.IsNullOrWhiteSpace(Convert.ToString(endServ)) Then
                    lblNoContractNotify.Text = ObjNavigationHandler.SetLanguage(Me.Page, " The current employee's service has been terminated! /  تم انهاء الخدمة للموظف الحالي")
                Else
                    lblNoContractNotify.Text = ObjNavigationHandler.SetLanguage(Me.Page, " This Employee has no contracts yet! / لا توجد أي تعاقدات بعد مع الموظف الحالي")
                End If

            Else
                lblNoContractNotify.Text = " "
                lblNoContractNotify.Visible = False
                btnAddContract.Visible = False
            End If
        End If
        If IntEmployeeId > 0 Then
            IntValidContract = ClsContracts.ID
            GetValidContractData(IntValidContract)
        Else
            New_Part_Contracts()
        End If
        Return True
    End Function
    Private Function GetValidContractData(ByVal intValidContract As Integer) As Boolean
        Dim IntContractId As Integer
        Dim IntEmployeeId As Integer = txtEmpId.Value
        Dim ObjDs As New Data.DataSet
        Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Page)

        Dim ClsContracts As New Clshrs_Contracts(Me.Page)
        If IntEmployeeId > 0 Then
            IntContractId = intValidContract
            ClsContracts.Find(" ID = " & IntContractId)
            GetValues_Contracts(ClsContracts)
        End If

    End Function
    Private Function New_Part_Contracts() As Boolean
        Dim ClsContracts As New Clshrs_Contracts(Me.Page)
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ClsVacationsType As New Clshrs_VacationsTypes(Me.Page)
        Dim ClsEmployeeClasses As New Clshrs_EmployeeClasses(Page)
        Dim Ds As New Data.DataSet
        Dim StrSerial As String = String.Empty
        ClsContracts.Clear()
        Clear_Contracts()
        GetValues_Contracts(ClsContracts)

        txtStartDate.Value = Date.Now.ToString("ddMMyyyy")
    End Function
    Private Function Clear_Contracts() As Boolean
        lblContractNo.Text = String.Empty
        txtStartDate.Value = String.Empty
        txtEndDate.Value = String.Empty
        'ddlPosition.SelectedIndex = -1
        TxtPositionID.Value = 0
        TxtPositionCode.Text = String.Empty
        TxtPositionName.Text = String.Empty
        ddlProfessions.SelectedIndex = -1
        ddlCurrency.SelectedIndex = -1
        ddlEmployeeClass.SelectedIndex = -1
        ddlGradeStep.SelectedIndex = -1
        ddlContractType.SelectedIndex = -1
        lblNoContractNotify.Text = " "
        ddlLastEducations.SelectedIndex = -1
        lblNoContractNotify.Visible = False
        btnAddContract.Visible = False
        wneContractDuration.Value = 365
    End Function
    Private Function GetValues_Contracts(ByRef ClsContracts As Clshrs_Contracts) As Boolean
        Dim ClsUser As New Clssys_Users(Me.Page)
        Try
            With ClsContracts
                txtContractId.Value = .ID
                txtCId.Text = .ID
                lblContractNo.Text = .Number

                If .StartDate <> Nothing Then
                    txtStartDate.Value = CDate(.GetHigriDate(.StartDate)).ToString("ddMMyyyy")
                End If
                If .EndDate <> Nothing Then
                    txtEndDate.Value = CDate(.GetHigriDate(.EndDate)).ToString("ddMMyyyy")
                End If
                wneContractDuration.Value = .ContractPeriod
            End With
            LoadEmp_ContractCanceledItems(ClsContracts)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function LoadEmp_ContractCanceledItems(ByRef ClsContracts As Clshrs_Contracts) As Boolean
        Dim ClsContractsTypes As New Clshrs_ContractTypes(Me.Page)
        Dim ClsProfission As New ClsBasicFiles(Me.Page, "hrs_Professions")
        Dim ClsPosition As New ClsBasicFiles(Me.Page, "hrs_Positions")
        Dim ClsEmployeeClass As New ClsBasicFiles(Me.Page, "hrs_EmployeesClasses")
        Dim ClsGradeStep As New Clshrs_GradesSteps(Me.Page)
        Dim ClsCurrencies As New ClsBasicFiles(Me, "sys_Currencies")
        ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Try
            With ClsContracts
                Dim item As New System.Web.UI.WebControls.ListItem()
                ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(.ConnectionString)
                ClsContractsTypes.Find(" ID= " & IIf(IsNothing(.ContractTypeId), 0, .ContractTypeId))
                If ClsContractsTypes.ID > 0 Then
                    item.Value = .ContractTypeId
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsContractsTypes.EngName & "/" & ClsContractsTypes.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsContractsTypes.ArbName & "/" & ClsContractsTypes.EngName)
                    End If
                    If Not ddlContractType.Items.Contains(item) Then
                        ddlContractType.Items.Add(item)
                        ddlContractType.SelectedValue = item.Value
                    Else
                        ddlContractType.SelectedValue = .ContractTypeId
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                ClsProfission.Find(" ID= " & IIf(IsNothing(.ProfessionID), 0, .ProfessionID))
                If ClsProfission.ID > 0 Then
                    item.Value = .ProfessionID
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsProfission.EngName & "/" & ClsProfission.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsProfission.ArbName & "/" & ClsProfission.EngName)
                    End If
                    If Not ddlProfessions.Items.Contains(item) Then
                        ddlProfessions.Items.Add(item)
                        ddlProfessions.SelectedValue = item.Value
                    Else
                        ddlProfessions.SelectedValue = .ProfessionID
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                ClsPosition.Find(" ID= " & IIf(IsNothing(.PositionID), 0, .PositionID))
                If ClsPosition.ID > 0 Then
                    item.Value = .PositionID
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsPosition.EngName & "/" & ClsPosition.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsPosition.ArbName & "/" & ClsPosition.EngName)
                    End If

                    'Rabie 30-12-2024
                    TxtPositionID.Value = .PositionID

                    TxtPositionCode.Text = ClsPosition.Code
                    'If Not ddlPosition.Items.Contains(item) Then
                    '    ddlPosition.Items.Add(item)
                    '    ddlPosition.SelectedValue = item.Value
                    'Else
                    '    ddlPosition.SelectedValue = .PositionID
                    'End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                ClsEmployeeClass.Find(" ID= " & IIf(IsNothing(.EmployeeClassID), 0, .EmployeeClassID))
                If ClsEmployeeClass.ID > 0 Then
                    item.Value = .EmployeeClassID
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsEmployeeClass.EngName & "/" & ClsEmployeeClass.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsEmployeeClass.ArbName & "/" & ClsEmployeeClass.EngName)
                    End If
                    If Not ddlEmployeeClass.Items.Contains(item) Then
                        ddlEmployeeClass.Items.Add(item)
                        ddlEmployeeClass.SelectedValue = item.Value
                    Else
                        ddlEmployeeClass.SelectedValue = .EmployeeClassID
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                ClsGradeStep.Find(" hrs_GradesSteps.ID= " & IIf(IsNothing(.GradeStepId), 0, .GradeStepId))
                If ClsGradeStep.ID > 0 Then
                    item.Value = .GradeStepId
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsGradeStep.EngName & "/" & ClsGradeStep.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsGradeStep.ArbName & "/" & ClsGradeStep.EngName)
                    End If
                    If Not ddlGradeStep.Items.Contains(item) Then
                        ddlGradeStep.Items.Add(item)
                        ddlGradeStep.SelectedValue = item.Value
                    Else
                        ddlGradeStep.SelectedValue = .GradeStepId
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                ClsCurrencies.Find(" ID= " & IIf(IsNothing(.CurrencyId), 0, .CurrencyId))
                If ClsCurrencies.ID > 0 Then
                    item.Value = .CurrencyId
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsCurrencies.EngName & "/" & ClsCurrencies.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsCurrencies.ArbName & "/" & ClsCurrencies.EngName)
                    End If
                    If Not ddlCurrency.Items.Contains(item) Then
                        ddlCurrency.Items.Add(item)
                        ddlCurrency.SelectedValue = item.Value
                    Else
                        ddlCurrency.SelectedValue = .CurrencyId
                    End If
                End If
            End With
        Catch ex As Exception

        End Try
    End Function
#End Region

#Region "Private Functions"
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
    Private Function New_Part()
        Dim ClsContracts As New Clshrs_Contracts(Page)
        UltraWebTab1.SelectedTab = 0
        Clear()
        Clear_Contracts()
        If Not IsNothing(ClsEmployees) Then ClsEmployees.Clear()
        If Not IsNothing(ClsContracts) Then ClsContracts.Clear()
        If Not IsNothing(ClsEmployeesDependants) Then ClsEmployeesDependants.Clear()

        GetContractsData(0)
        ImageButton_Delete.Enabled = False

        txtCode.Focus()
        'Get Next Code
        Dim Cls_Companies As New Clssys_Companies(Page)
        If Cls_Companies.Find("ID=" & Cls_Companies.MainCompanyID) = True Then
            If Cls_Companies.HasSequence = True Then
                txtCode.Text = ObjNavigationHandler.SetLanguage(Me.Page, "Auto/تلقائى")
            End If
        End If
        'End Get Next Code
    End Function
    Private Function AssignValues_Employees() As Boolean
        Try
            With ClsEmployees

                .Code = txtCode.Text
                .EngName = txtEngName.Text
                .ArbName = txtArbName.Text
                .FamilyEngName = txtEngFamilyName.Text
                .FamilyArbName = txtArbFamilyName.Text
                .FatherEngName = txtEngFathername.Text
                .FatherArbName = txtArbFatherName.Text
                .GrandEngName = txtEngGrandName.Text
                .GrandArbName = txtArbGrandName.Text
                .E_Mail = txtEmail.Text
                .WorkE_Mail = txtWorkE_Mail.Text
                .Phone = txtPhone.Text
                .Mobile = txtMobile.Text
                .MachineCode = TextBox_MachineCode.Text

                .BirthDate = SetDate(txtBirthDate.Text, txtBirthDateH.Text)

                .BirthCityID = DdlBirthCity.SelectedItem.Value
                .ReligionID = DdlReligion.SelectedItem.Value
                .MaritalStatusID = DdlMaritalStatus.SelectedItem.Value
                .MachineCode = IIf(TextBox_MachineCode.Text = "", txtCode.Text, TextBox_MachineCode.Text)
                '-------------------------------0257 MODIFIED-----------------------------------------
                If (DdlGender.SelectedValue = "M" Or DdlGender.SelectedValue = "1") Then
                    .Sex = "M"
                ElseIf (DdlGender.SelectedValue = "F" Or DdlGender.SelectedValue = "2") Then
                    .Sex = "F"
                End If
                '-------------------------------=============-----------------------------------------
                .BloodGroupID = DdlBloodGroups.SelectedItem.Value
                .BankID = ddlBank.SelectedItem.Value
                .SponsorID = ddlSponsor.SelectedItem.Value
                .NationalityID = DdlNationality.SelectedItem.Value
                .BankAccountNumber = txtBankAccount.Text.Trim()
                .DepartmentID = ddlDepartment.SelectedValue
                .LocationID = DropDownList_Location.SelectedValue
                .BranchID = ddlBranch.SelectedValue
                .GosiNumber = txtGosiNumber.Text.Trim()
                .GOSIJoinDate = SetDate(GOSIJoinDate.Text, GOSIJoinDate.Text) 'IIf(GOSIJoinDate.Value.Trim = "", Nothing, .SetHigriDate(GOSIJoinDate.Text))
                .JoinDate = SetDate(JoinDate.Text, JoinDate.Text) 'IIf(JoinDate.Value.Trim = "", NClsEmployees.HasTaqatothing, .SetHigriDate(JoinDate.Text))
                .GOSIExcludeDate = SetDate(GOSIExcludeDate.Text, GOSIExcludeDate.Text) '' IIf(GOSIExcludeDate.Value.Trim = "", Nothing, .SetHigriDate(GOSIExcludeDate.Text))
                .AddressAsPerContract = txtAddress.Text.Trim()
                .EntryNo = txtEntry.Text.Trim()
                .SSnNo = txtIdentity.Text.Trim()
                .SSNOIssueDate = txtSSNoIssueDate.Text
                .SSNOExpireDate = txtSSNoExpireDate.Text
                .PassPortNo = txtPassport.Text.Trim()
                .PassportIssueDate = txtPassportIssueDate.Text
                .PassportExpireDate = textPssportExpDate.Text
                '.Cost1 = ddlCost1.SelectedValue
                '.Cost2 = ddlCost2.SelectedValue
                '.Cost3 = ddlCost3.SelectedValue
                '.Cost4 = ddlCost4.SelectedValue

                .Cost1 = GetCostCenter1ID(TxtCostCode1.Text)
                .Cost2 = GetCostCenter2ID(TxtCostCode2.Text)
                .Cost3 = GetCostCenter3ID(TxtCostCode3.Text)
                .Cost4 = GetCostCenter4ID(TxtCostCode4.Text)
                .IsProjectRelated = .IsProjectRelated
                .IsSpecialForce = .IsSpecialForce
                .WHours = IIf(.WHours = Nothing, 0, .WHours)
                .SectorID = ddlSectors.SelectedValue
                .LedgerCode = txtLedgerCode.Text
                .HasTaqat = CheckBox1.Checked
                .Hasflexiblesalarydist = chkflexiblesalarydist.Checked
                .InsertRequestsForAnotherEmployee = chkInsertRequestsForAnotherEmployee.Checked
                .IsSocialInsuranceIncluded = chkIsSocialInsuranceIncluded.Checked
                .paymenttype = ddlLblPaymentType.SelectedValue


                .BankAccountType = IIf(ddlBankAccountType.SelectedValue <> "0", ddlBankAccountType.SelectedValue, "")

                Dim clsEmp As New Clshrs_Employees(Page)
                clsEmp.Find(" Code='" & txtManager.Text & "'")
                If clsEmp.ID > 0 Then
                    .ManagerID = clsEmp.ID
                Else
                    .ManagerID = Nothing
                End If
            End With
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Function GetValues_Employees(ClsEmployees As Clshrs_Employees) As Boolean
        Dim ClsUser As New Clssys_Users(Page)
        Dim ClsCountries As New Clssys_Countries(Page)
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim ClsObjects As New Clssys_Objects(Me.Page)
        ClsObjects.Find(" Code = REPLACE('" & ClsEmployees.Table & "',' ' ,'')")
        Dim ObjectID As Integer = ClsObjects.ID
        Dim employeePhotosPath As String = System.Configuration.ConfigurationManager.AppSettings("EmployeesPhotos")
        Try
            SetToolBarDefaults()
            With ClsEmployees
                txtEmpId.Value = .ID
                lblEmpID.Text = .ID
                txtCode.Text = .Code
                lblEmpID.Text = ClsEmployees.ID
                txtEngName.Text = .EngName
                txtArbName.Text = .ArbName
                txtEngFamilyName.Text = .FamilyEngName
                txtArbFamilyName.Text = .FamilyArbName
                txtEngFathername.Text = .FatherEngName
                txtArbFatherName.Text = .FatherArbName
                txtEngGrandName.Text = .GrandEngName
                txtArbGrandName.Text = .GrandArbName
                txtEmail.Text = .E_Mail
                txtWorkE_Mail.Text = .WorkE_Mail
                txtPhone.Text = .Phone
                txtMobile.Text = .Mobile
                TextBox_MachineCode.Text = .MachineCode
                txtLedgerCode.Text = .LedgerCode
                txtBirthDate.Value = Nothing
                txtBirthDateH.Value = Nothing
                Label_Age.Text = String.Empty
                If .BirthDate <> Nothing Then
                    txtBirthDate.Value = ClsDataAcessLayer.FormatGreg(.BirthDate, "dd/MM/yyyy").ToString("ddMMyyyy")
                    txtBirthDateH.Value = ClsDataAcessLayer.GregToHijri(.BirthDate, "ddMMyyyy")
                    Label_Age.Text = DateDiff(DateInterval.Year, ClsDataAcessLayer.FormatGreg(.BirthDate, "dd/MM/yyyy"), DateTime.Now) & " " & ObjNavigationHandler.SetLanguage(Me.Page, "Year/سنة")
                End If
                txtGosiNumber.Text = .GosiNumber
                If .NationalityID <> Nothing Then
                    DdlNationality.SelectedValue = .NationalityID
                End If
                If .SponsorID <> Nothing Then
                    ddlSponsor.SelectedValue = .SponsorID
                End If
                GOSIJoinDate.Value = Nothing
                If .GOSIJoinDate <> Nothing Then
                    GOSIJoinDate.Value = CDate(.GetHigriDate(.GOSIJoinDate)).ToString("ddMMyyyy")
                End If

                JoinDate.Value = Nothing
                If .JoinDate <> Nothing Then
                    JoinDate.Value = CDate(.GetHigriDate(.JoinDate)).ToString("ddMMyyyy")
                End If

                GOSIExcludeDate.Value = Nothing
                If .GOSIExcludeDate <> Nothing Then
                    GOSIExcludeDate.Value = CDate(.GetHigriDate(.GOSIExcludeDate)).ToString("ddMMyyyy")
                End If
                Try
                    ddlBank.SelectedValue = .BankID
                    txtBankAccount.Text = .BankAccountNumber
                Catch ex As Exception

                End Try
                DisplayImage(.ID, ObjectID, Image1)
                DisplayImageSignature(.ID, ObjectID, Image2)

                Label212.Text = ""
                If .Sex.ToString = "M" Then
                    DdlGender.SelectedIndex = 0
                ElseIf .Sex.ToString = "F" Then
                    DdlGender.SelectedIndex = 1
                End If


                txtEntry.Text = .EntryNo
                txtIdentity.Text = .SSnNo
                txtPassport.Text = .PassPortNo
                txtPassportIssueDate.Text = .PassportIssueDate
                textPssportExpDate.Text = .PassportExpireDate
                txtSSNoIssueDate.Text = .SSNOIssueDate
                txtSSNoExpireDate.Text = .SSNOExpireDate
                txtAddress.Text = .AddressAsPerContract
                CheckBox1.Checked = .HasTaqat
                chkflexiblesalarydist.Checked = .Hasflexiblesalarydist
                chkInsertRequestsForAnotherEmployee.Checked = .InsertRequestsForAnotherEmployee
                chkIsSocialInsuranceIncluded.Checked = .IsSocialInsuranceIncluded
                ddlLblPaymentType.SelectedValue = .paymenttype
                Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
                Dim ClsSectors As New ClsSys_Sectors(Page)
                ClsDepartment.GetDropDownList(ddlDepartment, True)
                'ClsSectors.GetDropDownList(ddlSectors, True)
                ddlBranch.SelectedValue = .BranchID
                ddlBranch_SelectedIndexChanged(Nothing, Nothing)
                ddlDepartment.SelectedValue = .DepartmentID
                ddlDepartment_SelectedIndexChanged(Nothing, Nothing)
                ddlSectors.SelectedValue = .SectorID
                'ddlCost1.SelectedValue = .Cost1
                'ddlCost2.SelectedValue = .Cost2
                'ddlCost3.SelectedValue = .Cost3
                'ddlCost4.SelectedValue = .Cost4
                TxtCostCode1.Text = GetCostCenter1Code(.Cost1)
                TxtCostCode2.Text = GetCostCenter2Code(.Cost2)
                TxtCostCode3.Text = GetCostCenter3Code(.Cost3)
                TxtCostCode4.Text = GetCostCenter4Code(.Cost4)
                'ddlBankAccountType.SelectedValue = IIf(Not IsDBNull(.BankAccountType), .BankAccountType, 0)
                If Not String.IsNullOrEmpty(.BankAccountType) Then
                    Try
                        ddlBankAccountType.SelectedValue = .BankAccountType

                    Catch ex As Exception

                    End Try
                End If

                DropDownList_Location.SelectedValue = .LocationID
                Dim clsEmp As New Clshrs_Employees(Page)
                clsEmp.Find("id=" & .ManagerID)
                txtManager.Text = Nothing
                If clsEmp.ID > 0 Then
                    txtManager.Text = clsEmp.Code
                End If
            End With

            'lblCurrentProject.Text = String.Empty
            Label_LocationDescription.Text = String.Empty

            Dim clsProjects As New Clshrs_Projects(Me, "hrs_Projects")
            Dim clsProjectLocations As New Clshrs_ProjectLocations(Me.Page)
            Dim hrsProjectPlacementEmployees As New Clshrs_ProjectPlacementEmployees(Me)
            If hrsProjectPlacementEmployees.Find("EmployeeID = " & ClsEmployees.ID & " and FromDate <= GETDATE() and (ToDate is null or ToDate >=  GETDATE())") Then
                If clsProjects.Find("ID in (select ProjectID from hrs_ProjectPlacements where PlacementCode = '" & hrsProjectPlacementEmployees.PlacementCode & "')") Then
                    '       lblCurrentProject.Text = clsProjects.ArbName & " " & hrsProjectPlacementEmployees.PlacementCode
                End If
                If clsProjectLocations.Find("ID in (select LocationID from hrs_ProjectPlacements where PlacementCode = '" & hrsProjectPlacementEmployees.PlacementCode & "')") Then
                    Label_LocationDescription.Text = clsProjectLocations.LocationDescription & " " & clsProjectLocations.LocationAddress
                End If
            End If

            LoadEmpCanceledItems(ClsEmployees)


            If Not ClsEmployees.RegUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEmployees.RegUserID)
            End If
            If ClsUser.EngName = Nothing Then
                lblRegUserValue.Text = ""
            Else
                lblRegUserValue.Text = ClsUser.EngName
            End If
            If Convert.ToDateTime(ClsEmployees.RegDate).Date = Nothing Then
                lblRegDateValue.Text = ""
            Else
                lblRegDateValue.Text = Convert.ToDateTime(ClsEmployees.RegDate).Date
            End If
            If ClsEmployees.CancelDate = Nothing Then
                lblCancelDateValue.Text = ""
            Else
                lblCancelDateValue.Text = Convert.ToDateTime(ClsEmployees.CancelDate).Date
            End If
            If Not ClsEmployees.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            Else
                ImageButton_Delete.Enabled = True
            End If
            Dim item As New System.Web.UI.WebControls.ListItem()

            If (ClsEmployees.ID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If
            SetToolBarPermission(Me, ClsEmployees.ConnectionString, ClsEmployees.DataBaseUserRelatedID, ClsEmployees.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployees.ConnectionString, ClsEmployees.DataBaseUserRelatedID, ClsEmployees.GroupID, ClsEmployees.Table, ClsEmployees.ID)
            If Not ClsEmployees.CancelDate = Nothing Then
                ImageButton_Delete.Enabled = False
            End If
            If Page.IsPostBack Then
                CreateOtherFields(ClsEmployees.ID)
            End If

            If Not ClsEmployees.UpdateUserID = Nothing Then
                ClsUser.Find("ID=" & ClsEmployees.UpdateUserID)
            End If

            LblLastUpdateByValue.Text = ClsUser.EngName
            lblLastUpdateDate.Text = ClsEmployees.UpdateDate
            Return True
        Catch ex As Exception
            Dim x As String = ex.Message
        End Try
    End Function
    Public Function GetDateDiff(d1 As DateTime, d2 As DateTime) As String
        If d1.Day = d2.Day Then
            Dim yearDiff As Integer = d2.Year - d1.Year
            If d1.Month = d2.Month Then
                'Only year differs
                Return yearDiff & " years"
            Else
                'Month and year differs
                Dim monthDiff As Integer = d2.Month - d1.Month
                Return (yearDiff * 12 + monthDiff) & " months"
            End If
        Else
            Return (d2 - d1).TotalDays & " days"
        End If
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
                            ImageButton1_Save.Enabled = .Item("AllowAdd")
                            ImageButton_SaveN.Enabled = .Item("AllowAdd")
                            LinkButton_SaveN.Enabled = .Item("AllowAdd")
                        Case "E"
                            ImageButton1_Save.Enabled = .Item("AllowEdit")
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

                    If ImageButton1_Save.Enabled = True And .Item("CanEdit") = True Then
                        ImageButton1_Save.Enabled = Not .Item("CanEdit")
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
                    ClsEmployees.Find("ID=" & intID)
                    GetValues_Employees(ClsEmployees)
                    txtCode.ReadOnly = True
                    ImageButton1_Save.Visible = False
                    ImageButton_SaveN.Visible = False
                    LinkButton_SaveN.Visible = False
                Case "E"
                    ClsEmployees.Find("ID=" & intID)
                    GetValues_Employees(ClsEmployees)
                    txtCode.ReadOnly = True
                    ImageButton_Delete.Enabled = False
            End Select
        Catch ex As Exception
        End Try
    End Function
    Private Function SetScreenInformation(ByVal StrMode As String) As Boolean
        Dim SearchColumns As New Clssys_SearchsColumns(Page)
        Try
            With ClsEmployees
                Venus.Shared.Web.ClientSideActions.SetPageControlFocus(Me, .ConnectionString, "UltraWebTab1")
                Venus.Shared.Web.ClientSideActions.SetPageCompulsory(Me, .ConnectionString, DIV)
                Venus.Shared.Web.ClientSideActions.SetPageControlSecurity(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID)
                If StrMode = "N" Then
                    SetToolBarPermission(Me, .ConnectionString, .DataBaseUserRelatedID, .GroupID, StrMode)
                    ImageButton_Delete.Enabled = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Function
    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        ClsEmployees = New Clshrs_Employees(Me)
        Try
            ClsEmployees.Find("Code='" & txtCode.Text & "'")
            IntId = ClsEmployees.ID
            txtEngName.Focus()
            If ClsEmployees.ID > 0 Then
                Clear_Contracts()
                Clear()

                GetValues_Employees(ClsEmployees)
                GetContractsData(IntId)
                StrMode = "E"
            Else
                If ClsEmployees.CheckRecordExistance(" Code='" & txtCode.Text & "'") Then
                    txtCode.Text = ""
                    txtCode.Focus()
                End If
                Dim PrevCode As String = ""
                PrevCode = txtCode.Text
                Clear()
                txtCode.Text = PrevCode
                Clear_Contracts()

                GetContractsData(0)
                ImageButton_Delete.Enabled = False
                StrMode = "N"
                CreateOtherFields(0)
            End If
            SetToolBarDefaults()
            SetToolBarPermission(Me, ClsEmployees.ConnectionString, ClsEmployees.DataBaseUserRelatedID, ClsEmployees.GroupID, StrMode)
            SetToolBarRecordPermission(Me, ClsEmployees.ConnectionString, ClsEmployees.DataBaseUserRelatedID, ClsEmployees.GroupID, ClsEmployees.Table, IntId)
            If Not lblCancelDateValue.Text = "" Or IntId = 0 Then
                ImageButton_Delete.Enabled = False
            End If
            TxtCostCode1_TextChanged(Nothing, Nothing)
            TxtCostCode2_TextChanged(Nothing, Nothing)
            TxtCostCode3_TextChanged(Nothing, Nothing)
            TxtCostCode4_TextChanged(Nothing, Nothing)
            TxtPositionCode_TextChanged(Nothing, Nothing)


        Catch ex As Exception
        End Try
    End Function
    Private Function GetCostCenter1ID(CostCenter1Code As String) As Integer
        If Not String.IsNullOrEmpty(CostCenter1Code) Then
            Dim str As String
            Dim CostCode As String = CostCenter1Code
            Dim CostCenterID As Integer

            str = "select ID From fcs_CostCenters1 where Code='" & CostCode & "' "
            Try
                CostCenterID = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str))
                Return CostCenterID
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try

        End If
    End Function
    Private Function GetCostCenter2ID(CostCenter1Code As String) As Integer
        If Not String.IsNullOrEmpty(CostCenter1Code) Then
            Dim str As String
            Dim CostCode As String = CostCenter1Code
            Dim CostCenterID As Integer

            str = "select ID From fcs_CostCenters2 where Code='" & CostCode & "' "
            Try
                CostCenterID = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str))
                Return CostCenterID
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try

        End If
    End Function

    Private Function GetCostCenter3ID(CostCenter1Code As String) As Integer
        If Not String.IsNullOrEmpty(CostCenter1Code) Then
            Dim str As String
            Dim CostCode As String = CostCenter1Code
            Dim CostCenterID As Integer

            str = "select ID From fcs_CostCenters3 where Code='" & CostCode & "' "
            Try
                CostCenterID = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str))
                Return CostCenterID
            Catch ex As Exception
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " Error Get Code /خطأ توليد الكود"))
            End Try

        End If
    End Function
    Private Function GetCostCenter4ID(CostCenter1Code As String) As Integer
        If Not String.IsNullOrEmpty(CostCenter1Code) Then
            Dim str As String
            Dim CostCode As String = CostCenter1Code
            Dim CostCenterID As Integer

            str = "select ID From fcs_CostCenters4 where Code='" & CostCode & "' "
            Try
                CostCenterID = CInt(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str))
                Return CostCenterID
            Catch ex As Exception
            End Try

        End If
    End Function

    Private Function GetCostCenter1Code(CostCenterID As String) As String
        If Not String.IsNullOrEmpty(CostCenterID) Then
            Dim str As String
            Dim CostCenterCode As String

            str = "select Code From fcs_CostCenters1 where ID='" & CostCenterID & "' "
            Try
                CostCenterCode = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                Return CostCenterCode
            Catch ex As Exception
            End Try

        End If
    End Function
    Private Function GetCostCenter2Code(CostCenterID As String) As String
        If Not String.IsNullOrEmpty(CostCenterID) Then
            Dim str As String
            Dim CostCenterCode As String

            str = "select Code From fcs_CostCenters2 where ID='" & CostCenterID & "' "
            Try
                CostCenterCode = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                Return CostCenterCode
            Catch ex As Exception
            End Try

        End If
    End Function
    Private Function GetCostCenter3Code(CostCenterID As String) As String
        If Not String.IsNullOrEmpty(CostCenterID) Then
            Dim str As String
            Dim CostCenterCode As String

            str = "select Code From fcs_CostCenters3 where ID='" & CostCenterID & "' "
            Try
                CostCenterCode = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                Return CostCenterCode
            Catch ex As Exception
            End Try

        End If
    End Function
    Private Function GetCostCenter4Code(CostCenterID As String) As String
        If Not String.IsNullOrEmpty(CostCenterID) Then
            Dim str As String
            Dim CostCenterCode As String

            str = "select Code From fcs_CostCenters4 where ID='" & CostCenterID & "' "
            Try
                CostCenterCode = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsEmployees.ConnectionString, Data.CommandType.Text, str)
                Return CostCenterCode
            Catch ex As Exception
            End Try

        End If
    End Function
    Private Function SetToolBarDefaults() As Boolean
        ImageButton1_Save.Enabled = True
        ImageButton_SaveN.Enabled = True
        LinkButton_SaveN.Enabled = True
        ImageButton_Delete.Enabled = True
        ImageButton_Print.Enabled = True
    End Function
    Private Function AfterOperation() As Boolean
        ClsEmployees.Clear()
        GetValues_Employees(ClsEmployees)
        value.Text = ""
        New_Part()
        CreateOtherFields(0)
        ImageButton_Delete.Enabled = False

        Venus.Shared.Web.ClientSideActions.SetFocus(Page, txtCode, True)
        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
        If Page.IsPostBack Then
            UltraWebTab1.SelectedTabIndex = 0
        End If
    End Function
    Private Function Clear() As Boolean
        txtCode.Text = String.Empty
        txtManager.Text = String.Empty
        txtArbFamilyName.Text = String.Empty
        txtArbFatherName.Text = String.Empty
        txtArbGrandName.Text = String.Empty
        txtEngFamilyName.Text = String.Empty
        txtEngFathername.Text = String.Empty
        txtEngGrandName.Text = String.Empty
        txtEmail.Text = String.Empty
        txtWorkE_Mail.Text = String.Empty
        txtMobile.Text = String.Empty
        TextBox_MachineCode.Text = String.Empty
        txtBankAccount.Text = String.Empty
        txtBirthDate.Value = Nothing
        txtBirthDateH.Value = Nothing
        Label_Age.Text = String.Empty

        DdlMaritalStatus.SelectedIndex = 0
        ddlBank.SelectedIndex = 0
        ddlSponsor.SelectedIndex = 0
        txtEngName.Text = ""
        txtArbName.Text = ""
        DdlGender.SelectedIndex = 0
        DdlReligion.SelectedIndex = 0
        DdlBirthCity.SelectedIndex = 0
        DdlBloodGroups.SelectedIndex = 0
        SelectBranchSector(0, 0, 0)
        DropDownList_Location.SelectedIndex = 0
        ddlDepartment.SelectedIndex = 0
        ddlSectors.SelectedIndex = 0
        ddlBranch.SelectedIndex = 0
        ClsEmployees.HasTaqat = 0
        ClsEmployees.Hasflexiblesalarydist = 0
        chkInsertRequestsForAnotherEmployee.Checked = False
        chkIsSocialInsuranceIncluded.Checked = False
        ddlLblPaymentType.SelectedValue = 0
        DropDownList_Location.SelectedIndex = -1
        ddlBranch.SelectedIndex = -1
        ddlSectors.SelectedIndex = -1
        DdlGender.SelectedIndex = 0
        DdlNationality.SelectedIndex = 0
        DdlReligion.SelectedIndex = 0

        DdlGender.SelectedIndex = 0
        DdlReligion.SelectedIndex = 0
        txtGosiNumber.Text = ""
        GOSIJoinDate.Value = Nothing
        JoinDate.Value = Nothing
        GOSIExcludeDate.Value = Nothing
        txtBirthDate.Value = Nothing

        Image1.ImageUrl = "./NoImage.jpg"
        txtEmpId.Value = 0
        lblEmpID.Text = ""
        txtEmpDependantId.Value = 0

        txtEntry.Text = String.Empty
        txtIdentity.Text = String.Empty
        txtPassport.Text = String.Empty
        txtPassportIssueDate.Value = Nothing
        ' textPssportExpDate.Value = Nothing
        txtSSNoIssueDate.Value = Nothing
        txtSSNoExpireDate.Value = Nothing
        txtAddress.Text = String.Empty
        'ddlCost1.SelectedIndex = 0
        'ddlCost2.SelectedIndex = 0
        'ddlCost3.SelectedIndex = 0
        'ddlCost4.SelectedIndex = 0
        TxtCostCode1.Text = String.Empty
        TxtCostName1.Text = String.Empty
        TxtCostCode2.Text = String.Empty
        TxtCostCode3.Text = String.Empty
        TxtCostCode4.Text = String.Empty
        TxtCostName1.Text = String.Empty
        TxtCostName2.Text = String.Empty
        TxtCostName3.Text = String.Empty
        TxtCostName4.Text = String.Empty
        ddlSectors.SelectedIndex = 0
        ddlLastEducations.SelectedIndex = -1
        ddlBankAccountType.SelectedIndex = 0
        'Rabie
        ddlBank.SelectedIndex = 0

        txtGraduationDate.Value = Nothing

        ImageButton_Delete.Enabled = False
        lblRegDateValue.Text = ""
        lblRegDateValue.Text = ""
        lblCancelDateValue.Text = ""
    End Function
    Private Sub LoadDataUpdateSchedules(ByVal formName As String)
        Dim controlName As String = String.Empty
        ClsEmployees = New Clshrs_Employees(Page)
        ClsEmployees.Find(" code = '" & txtCode.Text & "'")
        Dim recordID As Integer = ClsEmployees.ID
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
        If clsMainOtherFields.CheckOtherFieldsButton(Me.Page, ClsEmployees.Table) = True Then
            Dim StrTablename As String
            StrTablename = ClsEmployees.Table
            clsSysObjects.Find(" Code = REPLACE('" & StrTablename & "',' ' ,'')")
            Dim objDS As New Data.DataSet
            clsOtherFieldsData.Find(" sys_OtherFieldsData.RecordID = " & IntRecordID &
                                    " And Sys_OtherFields.ObjectID = " & clsSysObjects.ID &
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
    Private Function ApplaySecurity(ByVal blarrSecurity() As Boolean)
        If blarrSecurity(0) = False Then DivContract.Visible = False Else DivContract.Visible = True
        'If blarrSecurity(3) = False Then UltraWebTab1.Tabs.FromKey("Dependants").Visible = False
        If blarrSecurity(1) = False Then txtContractsTransactionsPermissions.Value = 1
        If blarrSecurity(2) = False Then txtDocumentsPermission.Value = 1
    End Function
    Private Function SetKeys(ByRef ObjHoldKeys As System.Web.UI.WebControls.HiddenField, ByRef arrKeys() As Integer)
        For I As Integer = 0 To arrKeys.GetUpperBound(0)
            ObjHoldKeys.Value &= "," & arrKeys(I)
        Next
        ObjHoldKeys.Value = ObjHoldKeys.Value.Substring(1)
        For I As Integer = 0 To arrKeys.GetUpperBound(0)
            arrKeys(I) = 1
        Next
    End Function
    Private Function SetToolBarPermissions(ByVal StrKeys As String, ByVal blContracts As Boolean)
        If StrKeys.Trim.Length > 0 Then
            Dim arrKeys() As String = StrKeys.Split(",")
            If blContracts Then
                If arrKeys(0) = "1" Then
                    DivContract.Visible = True
                End If
            Else
                If arrKeys(1) <> "1" Then ImageButton_New.Enabled = False
                If arrKeys(2) <> "1" Then ImageButton1_Save.Enabled = False : ImageButton_SaveN.Enabled = False
                If arrKeys(3) <> "1" Then ImageButton_Delete.Enabled = False
                If UltraWebTab1.SelectedTab <> 0 Then If arrKeys(4) <> "1" Then ImageButton_Print.Enabled = False
            End If
        End If
    End Function
    Private Function LoadDataIntoControls() As Boolean
        'Employees Info Tab  ================================ [Start]
        Dim ClsRelegion As New ClsBasicFiles(Me.Page, "hrs_Religions")
        Dim ClsMaretalStatus As New ClsBasicFiles(Me.Page, "hrs_MaritalStatus")
        Dim ClsBloodGroup As New ClsBasicFiles(Me.Page, "hrs_BloodGroups")
        Dim ClsNationality As New ClsBasicFiles(Me.Page, "sys_Nationalities")
        Dim ClsBanks As New ClsBasicFiles(Me.Page, "sys_Banks")
        Dim ClsCities As New Clssys_Cities(Me.Page)
        Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
        Dim Clslocation As New ClsBasicFiles(Me.Page, "sys_Locations")
        Dim clsBranch As New Clssys_Branches(Page)
        Dim ClsContractsTypes As New Clshrs_ContractTypes(Me.Page)
        Dim ClsProfission As New Clshrs_Professions(Me.Page)
        Dim ClsPosition As New Clshrs_Positions(Me.Page)
        Dim ClsEmployeeClass As New Clshrs_EmployeeClasses(Me.Page)
        Dim ClsGradeStep As New Clshrs_GradesSteps(Me.Page)
        Dim ClsCurrencies As New ClsSys_Currencies(Me.Page)
        Dim ClsSponsor As New Clshrs_Sponsors(Page)
        Dim objNav As New Venus.Shared.Web.NavigationHandler(ClsSponsor.ConnectionString)
        Dim ClsSectors As New ClsSys_Sectors(Page)
        Dim ClsDependantType As New ClsBasicFiles(Me.Page, "hrs_DependantsTypes")
        Dim ClsBirthCities As New ClsBasicFiles(Me.Page, "sys_Cities")
        Dim ClsLevels As New Clshrs_LevelTypes(Me.Page)
        Dim clsDependantsTypes As New Clshrs_DependantsTypes(Me.Page)
        ClsEmployeesDependants = New Clshrs_EmployeesDependants(Me.Page)
        ClsEmployees = New Clshrs_Employees(Me.Page)
        clsSearchsColumns = New Clssys_SearchsColumns(Me.Page)
        Dim Intrecordid As Integer
        Dim clsEducations As New Clshrs_Educations(Me, "hrs_Educations")

        txtCode.Focus()
        If Not IsPostBack Then
            clsEducations.GetDropDownList(ddlLastEducations, True)
            ClsRelegion.GetDropDownList(DdlReligion, True)
            ClsMaretalStatus.GetDropDownList(DdlMaritalStatus, True)
            ClsBloodGroup.GetDropDownList(DdlBloodGroups, True)
            ClsNationality.GetDropDownList(DdlNationality, True)
            ClsBanks.GetDropDownList(ddlBank, True)
            ClsSponsor.GetDropDownList(ddlSponsor, True)
            ClsCities.GetDropDownList(DdlBirthCity, True)
            ClsDepartment.GetDropDownList(ddlDepartment, True)
            Clslocation.GetDropDownList(DropDownList_Location, True)
            clsBranch.GetDropDownList(ddlBranch, True, "sys_CompaniesBranches.CompanyID=" & clsBranch.MainCompanyID & " And UserID=" & clsBranch.DataBaseUserRelatedID & " AND CanView= 1")
            ClsSectors.GetDropDownList(ddlSectors, True)
            ClsEmployees.GetDropDownList(ddlLblPaymentType, "Hrs_PaymentTypes", True, "")

            ClsEmployees.GetDropDownList(ddlBankAccountType, "hrs_BankAccountTypes", True, "")
            'ClsEmployees.GetDropDownList(ddlCost1, "fcs_CostCenters", True, " CostCenterLevelID in (Select ID From fcs_CostCentersLevels Where Rank=1)")
            'ClsEmployees.GetDropDownList(ddlCost1, "CostCenter", True)
            'ClsEmployees.GetDropDownList(ddlCost2, "fcs_CostCenters", True, " CostCenterLevelID in (Select ID From fcs_CostCentersLevels Where Rank=2)")
            'ClsEmployees.GetDropDownList(ddlCost3, "fcs_CostCenters", True, " CostCenterLevelID in (Select ID From fcs_CostCentersLevels Where Rank=3)")
            'ClsEmployees.GetDropDownList(ddlCost4, "fcs_CostCenters", True, " CostCenterLevelID in (Select ID From fcs_CostCentersLevels Where Rank=4)")
            'ClsEmployees.GetDropDownList(ddlCost4, "VW_LoanLedgers", True)
            'ClsEmployees.GetDropDownList(ddlCost1, "fcs_CostCenters1", True)
            'ClsEmployees.GetDropDownList(ddlCost2, "fcs_CostCenters2", True)
            'ClsEmployees.GetDropDownList(ddlCost3, "fcs_CostCenters3", True)
            'ClsEmployees.GetDropDownList(ddlCost4, "fcs_CostCenters4", True)

            'Contracts Tab       ================================ [Start]
            ClsContractsTypes.GetDropDownList(ddlContractType, True)
            ClsProfission.GetDropDownList(ddlProfessions, True)
            'ClsPosition.GetDropDownList(ddlPosition, True)
            ClsEmployeeClass.GetDropDownList(ddlEmployeeClass, False)
            ClsGradeStep.GetDropDownList(ddlGradeStep, True, "")
            ClsCurrencies.GetDropDownList(ddlCurrency, False)
        End If

    End Function
    Private Sub AddEventToControls()
        Image1.Attributes.Add("onclick", "DisplayImageScreenEmp('" & GetObjectId1(ClsEmployees) & "')")
        Image2.Attributes.Add("onclick", "DisplayImageScreenEmp2('" & GetObjectId1(ClsEmployees) & "')")
        ImageButton_Documents.Attributes.Add("onclick", "OpenModal1('frmEmployeesDocuments.aspx?TB=" & ClsEmployees.Table.ToString.Trim() & "&SV=" & ClsEmployees.ID & "&',700,800,true,false,''); return false;")
        'ImageButton_Documents.Attributes.Add("onclick", "OpenModal1('frmItemDocuments.aspx?TB=" & ClsEmployees.Table.ToString.Trim() & "&SV=" & ClsEmployees.ID & "&',495,800,true,false,''); return false;")
        ImageButton_Dependants.Attributes.Add("onclick", "OpenModal1('frmEmployeesDependants.aspx?',495,800,true,false,''); return false;")
        ImageButton_Transaction.Attributes.Add("onclick", "OpenModal1('frmEmployeePeriodicalTransactions.aspx?',495,800,true,true,''); return false;")

        ImageButton_Contracts.Attributes.Add("onclick", "OpenModal1('frmEmployeesContractsVacations.aspx?',495,800,true,false,''); return false;")
        ImageButton_HealthInsurance.Attributes.Add("onclick", "OpenModal1('frmEmployeesHealthInsurance.aspx?',495,800,true,false,''); return false;")
    End Sub
    Private Sub SetSecurity(ByVal ClsEmployees As Clshrs_Employees)
        Dim blarrSecurity() As Boolean = {True, True, True, True}
        Dim arrKeys() As Integer = {1, 1, 1, 1, 1}
        txtContractsTransactionsPermissions.Value = 0
        ClsEmployees.CheckTabsSecuirty(blarrSecurity)
        Page.Session.Add("blarrSecurity", blarrSecurity)
        ApplaySecurity(blarrSecurity)

        If blarrSecurity(0) = 0 Then ImageButton_Contracts.Enabled = False
        If blarrSecurity(1) = 0 Then ImageButton_Transaction.Enabled = False
        If blarrSecurity(2) = 0 Then ImageButton_Documents.Enabled = False
        If blarrSecurity(3) = 0 Then ImageButton_Dependants.Enabled = False
        SetToolBarPermissions(txtContractsF.Value, True)
    End Sub
    Private Function GetObjectId1(ByVal ClsEmployees As Clshrs_Employees)
        ClsEmployees = New Clshrs_Employees(Me.Page)
        Dim clsObjects As New Clssys_Objects(Me.Page)
        clsObjects.Find(" Code = REPLACE('" & ClsEmployees.Table & "',' ' ,'')")
        Return clsObjects.ID
    End Function
    Private Function DisplayImage(ByVal IntRecordId As Integer, ByVal IntObjectId As Integer, ByRef ImgImageControl As System.Web.UI.WebControls.Image) As String
        Dim ClsDocumentAttachment = New Clssys_DocumentsInformationAttachments(Me.Page)
        If ClsDocumentAttachment.Find(" RecordID=" & IntRecordId & " And ObjectID=" & IntObjectId & " And (IsDGSignature=0 or IsDGSignature is null) And IsProfilePicture=1 And IsImageView is not Null And IsNull(CancelDate , '') = '' Order By IsProfilePicture Desc ") Then
            If ClsDocumentAttachment.ExpiryDate > Date.Now Or ClsDocumentAttachment.ExpiryDate = Nothing Then
                ImgImageControl.ImageUrl = "~/Photos/" & IntObjectId.ToString & "_" & IntRecordId.ToString & "/" + ClsDocumentAttachment.FileName
            Else
                ImgImageControl.ImageUrl = "./NoImage.jpg"
            End If
        Else
            ImgImageControl.ImageUrl = "./NoImage.jpg"
        End If

    End Function
    Private Function DisplayImageSignature(ByVal IntRecordId As Integer, ByVal IntObjectId As Integer, ByRef ImgImageControl As System.Web.UI.WebControls.Image) As String
        Dim ClsDocumentAttachment = New Clssys_DocumentsInformationAttachments(Me.Page)
        If ClsDocumentAttachment.Find(" RecordID=" & IntRecordId & " And ObjectID=" & IntObjectId & "And (IsProfilePicture =0 or IsProfilePicture is null)And IsDGSignature=1 And IsImageView is not Null And IsNull(CancelDate , '') = '' Order By IsDGSignature Desc ") Then
            If ClsDocumentAttachment.ExpiryDate > Date.Now Or ClsDocumentAttachment.ExpiryDate = Nothing Then
                ImgImageControl.ImageUrl = "~/Photos/" & IntObjectId.ToString & "_" & IntRecordId.ToString & "/" + ClsDocumentAttachment.FileName
            Else
                ImgImageControl.ImageUrl = "./NoImage.jpg"
            End If
        Else
            ImgImageControl.ImageUrl = "./NoImage.jpg"
        End If

    End Function

    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBranch.SelectedIndexChanged
        If ddlBranch.SelectedValue <> 0 Then
            Dim ClsDepartment As New Clssys_Departments(Me.Page)
            ClsDepartment.GetDropDownList(ddlDepartment, True, "ID in (select DepartmentID from sys_DepartmentsBranches where Checked = 1 and BranchID = " & ddlBranch.SelectedValue & ")")
            ddlDepartment_SelectedIndexChanged(Nothing, Nothing)
            ddlBranch.Focus()
        End If
    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        If ddlDepartment.SelectedValue <> 0 Then
            Dim ClsSector As New ClsSys_Sectors(Me.Page)
            ' ClsSector.GetDropDownList(ddlSectors, True, "ID in (select SectorID from sys_SectorsDepartments where Checked = 1 and DepartmentID = " & ddlDepartment.SelectedValue & ")")

            'Dim Clslocation As New ClsBasicFiles(Me.Page, "sys_Locations")
            'Clslocation.GetDropDownList(DropDownList_Location, True, "ID in (select ID from sys_Locations where DepartmentID = " & ddlDepartment.SelectedValue & ")")

            ddlDepartment.Focus()
            'Else
            '    Dim Clslocation As New ClsBasicFiles(Me.Page, "sys_Locations")
            '    Clslocation.GetDropDownList(DropDownList_Location, True)
        End If
    End Sub
    Private Sub SelectBranchSector(ByVal DepartmentID As Integer, ByVal BranchID As Integer, ByVal SectorID As Integer)
        Dim clsBranches As New Clssys_Branches(Me)
        Dim clsDepartments As New Clssys_Departments(Me)
        Dim clsSectors As New ClsSys_Sectors(Me)
        Dim item As System.Web.UI.WebControls.ListItem
        ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(clsBranches.ConnectionString)

        item = New System.Web.UI.WebControls.ListItem()
        clsBranches.Find(" ID= " & IIf(IsNothing(BranchID), 0, BranchID))
        If clsBranches.ID > 0 Then
            item.Value = BranchID
            item.Text = ObjNavigationHandler.SetLanguage(Me.Page, clsBranches.EngName & "/" & clsBranches.ArbName)
            If (item.Text.Trim = "") Then
                item.Text = ObjNavigationHandler.SetLanguage(Me.Page, clsBranches.ArbName & "/" & clsBranches.EngName)
            End If
            If Not ddlBranch.Items.Contains(item) Then
                ddlBranch.Items.Add(item)
                ddlBranch.SelectedValue = item.Value
            Else
                ddlBranch.SelectedValue = BranchID
            End If
        End If

        item = New System.Web.UI.WebControls.ListItem()
        clsDepartments.Find(" ID= " & IIf(IsNothing(DepartmentID), 0, DepartmentID))
        If clsDepartments.ID > 0 Then
            item.Value = DepartmentID
            item.Text = ObjNavigationHandler.SetLanguage(Me.Page, clsDepartments.EngName & "/" & clsDepartments.ArbName)
            If (item.Text.Trim = "") Then
                item.Text = ObjNavigationHandler.SetLanguage(Me.Page, clsDepartments.ArbName & "/" & clsDepartments.EngName)
            End If
            If Not ddlDepartment.Items.Contains(item) Then
                ddlDepartment.Items.Add(item)
                ddlDepartment.SelectedValue = item.Value
            Else
                ddlDepartment.SelectedValue = DepartmentID
            End If
        End If

        item = New System.Web.UI.WebControls.ListItem()
        clsSectors.Find(" ID= " & IIf(IsNothing(SectorID), 0, SectorID))
        If clsSectors.ID > 0 Then
            item.Value = SectorID
            item.Text = ObjNavigationHandler.SetLanguage(Me.Page, clsSectors.EngName & "/" & clsSectors.ArbName)
            If (item.Text.Trim = "") Then
                item.Text = ObjNavigationHandler.SetLanguage(Me.Page, clsSectors.ArbName & "/" & clsSectors.EngName)
            End If
            If Not ddlSectors.Items.Contains(item) Then
                ddlSectors.Items.Add(item)
                ddlSectors.SelectedValue = item.Value
            Else
                ddlSectors.SelectedValue = SectorID
            End If
        End If
    End Sub
    Private Function LoadEmpCanceledItems(ByRef ClsEmployees As Clshrs_Employees) As Boolean
        Dim ClsUser As New Clssys_Users(Me.Page)
        Dim ClsRelegion As New ClsBasicFiles(Me.Page, "hrs_Religions")
        Dim ClsMaretalStatus As New ClsBasicFiles(Me.Page, "hrs_MaritalStatus")
        Dim ClsBloodGroup As New ClsBasicFiles(Me.Page, "hrs_BloodGroups")
        Dim ClsNationality As New ClsBasicFiles(Me.Page, "sys_Nationalities")
        Dim ClsBanks As New ClsBasicFiles(Me.Page, "sys_Banks")
        Dim ClsCities As New Clssys_Cities(Me.Page)
        Dim ClsDepartment As New ClsBasicFiles(Me.Page, "sys_Departments")
        Dim clsBranches As New Clssys_Branches(Page)
        Dim ClsObjects As New Clssys_Objects(Me.Page)
        Dim clsSponsor As New Clshrs_Sponsors(Page)
        Dim ClsSectors As New ClsSys_Sectors(Page)
        Dim clsEducations As New Clshrs_Educations(Me, "hrs_Educations")
        Dim clsEmpEducations As New Clshrs_EmployeesEducations(Me)
        Dim employeePhotosPath As String = System.Configuration.ConfigurationManager.AppSettings("EmployeesPhotos")
        ObjNavigationHandler = New Venus.Shared.Web.NavigationHandler(ClsEmployees.ConnectionString)
        Dim item As New System.Web.UI.WebControls.ListItem()
        Try
            With ClsEmployees
                ClsCities.Find("ID= " & IIf(IsNothing(.BirthCityID), 0, .BirthCityID))
                ClsMaretalStatus.Find(" ID= " & IIf(IsNothing(.MaritalStatusID), 0, .MaritalStatusID))
                ClsBloodGroup.Find(" ID= " & IIf(IsNothing(.BloodGroupID), 0, .BloodGroupID))
                ClsNationality.Find(" ID= " & IIf(IsNothing(.NationalityID), 0, .NationalityID))
                ClsBanks.Find(" ID= " & IIf(IsNothing(.BankID), 0, .BankID))
                ClsRelegion.Find(" ID= " & IIf(IsNothing(.ReligionID), 0, .ReligionID))
                clsSponsor.Find(" ID= " & IIf(IsNothing(.SponsorID), 0, .SponsorID))
                ClsSectors.Find(" ID= " & IIf(IsNothing(.SectorID), 0, .SectorID))
                clsEmpEducations.Find("EmployeeID=" & .ID)
                clsEducations.Find("ID=" & clsEmpEducations.EducationTypeID)

                txtGraduationDate.Value = Nothing
                If clsEmpEducations.GraduationDate <> Nothing Then
                    txtGraduationDate.Value = CDate(.GetHigriDate(clsEmpEducations.GraduationDate)).ToString("ddMMyyyy")
                End If

                item = New System.Web.UI.WebControls.ListItem()
                If clsEducations.ID > 0 Then
                    item.Value = clsEmpEducations.EducationTypeID
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, clsEducations.EngName & "/" & clsEducations.ArbName)

                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, clsEducations.ArbName & "/" & clsEducations.EngName)

                    End If
                    If Not ddlLastEducations.Items.Contains(item) Then
                        ddlLastEducations.Items.Add(item)
                        ddlLastEducations.SelectedValue = item.Value
                    Else
                        ddlLastEducations.SelectedValue = clsEmpEducations.EducationTypeID
                    End If
                End If


                item = New System.Web.UI.WebControls.ListItem()
                If ClsCities.ID > 0 Then
                    item.Value = .BirthCityID
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsCities.EngName & "/" & ClsCities.ArbName)

                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsCities.ArbName & "/" & ClsCities.EngName)

                    End If
                    If Not DdlBirthCity.Items.Contains(item) Then
                        DdlBirthCity.Items.Add(item)
                        DdlBirthCity.SelectedValue = item.Value
                    Else
                        DdlBirthCity.SelectedValue = .BirthCityID
                    End If
                End If

                item = New System.Web.UI.WebControls.ListItem()
                If ClsMaretalStatus.ID > 0 Then
                    item.Value = .MaritalStatusID
                    item.Text = ClsMaretalStatus.Code & " - " & ObjNavigationHandler.SetLanguage(Me.Page, ClsMaretalStatus.EngName & "/" & ClsMaretalStatus.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ClsMaretalStatus.Code & " - " & ObjNavigationHandler.SetLanguage(Me.Page, ClsMaretalStatus.ArbName & "/" & ClsMaretalStatus.EngName)
                    End If
                    If Not DdlMaritalStatus.Items.Contains(item) Then
                        DdlMaritalStatus.Items.Add(item)
                        DdlMaritalStatus.SelectedValue = item.Value
                    Else
                        DdlMaritalStatus.SelectedValue = .MaritalStatusID
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                If ClsBloodGroup.ID > 0 Then
                    item.Value = .BloodGroupID
                    item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsBloodGroup.EngName & "/" & ClsBloodGroup.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ObjNavigationHandler.SetLanguage(Me.Page, ClsBloodGroup.ArbName & "/" & ClsBloodGroup.EngName)
                    End If
                    If Not DdlBloodGroups.Items.Contains(item) Then
                        DdlBloodGroups.Items.Add(item)
                        DdlBloodGroups.SelectedValue = item.Value
                    Else
                        DdlBloodGroups.SelectedValue = .BloodGroupID
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                If ClsNationality.ID > 0 Then
                    item.Value = .NationalityID
                    item.Text = ClsNationality.Code & " - " & ObjNavigationHandler.SetLanguage(Me.Page, ClsNationality.EngName & "/" & ClsNationality.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ClsNationality.Code & " - " & ObjNavigationHandler.SetLanguage(Me.Page, ClsNationality.ArbName & "/" & ClsNationality.EngName)
                    End If
                    If Not DdlNationality.Items.Contains(item) Then
                        DdlNationality.Items.Add(item)
                        DdlNationality.SelectedValue = item.Value
                    Else
                        DdlNationality.SelectedValue = .NationalityID
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()

                If ClsBanks.ID > 0 Then
                    item.Value = .BankID
                    item.Text = ClsBanks.Code & " - " & ObjNavigationHandler.SetLanguage(Me.Page, ClsBanks.EngName & "/" & ClsBanks.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ClsBanks.Code & " - " & ObjNavigationHandler.SetLanguage(Me.Page, ClsBanks.ArbName & "/" & ClsBanks.EngName)
                    End If
                    If Not ddlBank.Items.Contains(item) Then
                        ddlBank.Items.Add(item)
                        ddlBank.SelectedValue = item.Value
                    Else
                        ddlBank.SelectedValue = .BankID
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                If clsSponsor.ID > 0 Then
                    item.Value = .SponsorID
                    item.Text = clsSponsor.Code & " - " & ObjNavigationHandler.SetLanguage(Me.Page, clsSponsor.EngName & "/" & clsSponsor.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = clsSponsor.Code & " - " & ObjNavigationHandler.SetLanguage(Me.Page, clsSponsor.ArbName & "/" & clsSponsor.EngName)
                    End If
                    If Not ddlSponsor.Items.Contains(item) Then
                        ddlSponsor.Items.Add(item)
                        ddlSponsor.SelectedValue = item.Value
                    Else
                        ddlSponsor.SelectedValue = .SponsorID
                    End If
                End If
                item = New System.Web.UI.WebControls.ListItem()
                If ClsRelegion.ID > 0 Then
                    item.Value = .ReligionID
                    item.Text = ClsRelegion.Code & " - " & ObjNavigationHandler.SetLanguage(Me.Page, ClsRelegion.EngName & "/" & ClsRelegion.ArbName)
                    If (item.Text.Trim = "") Then
                        item.Text = ClsRelegion.Code & " - " & ObjNavigationHandler.SetLanguage(Me.Page, ClsRelegion.ArbName & "/" & ClsRelegion.EngName)
                    End If
                    If Not DdlReligion.Items.Contains(item) Then
                        DdlReligion.Items.Add(item)
                        DdlReligion.SelectedValue = item.Value
                    Else
                        DdlReligion.SelectedValue = .ReligionID
                    End If
                End If

                Dim ds As New DataSet
                'If .FindByTable("CostCenter", "ID=" & .Cost1, ds) Then
                '    item = New System.Web.UI.WebControls.ListItem()
                '    With ds.Tables(0).Rows(0)
                '        If .Item("ID") > 0 Then
                '            item.Value = ClsEmployees.Cost1
                '            item.Text = .Item("Code") & " - " & ObjNavigationHandler.SetLanguage(Me.Page, .Item("EngName") & "/" & .Item("ArbName"))
                '            If (item.Text.Trim = "") Then
                '                item.Text = .Item("Code") & " - " & ObjNavigationHandler.SetLanguage(Me.Page, .Item("ArbName") & "/" & .Item("EngName"))
                '            End If
                '            If Not ddlCost1.Items.Contains(item) Then
                '                ddlCost1.Items.Add(item)
                '                ddlCost1.SelectedValue = item.Value
                '            Else
                '                ddlCost1.SelectedValue = ClsEmployees.Cost1
                '            End If
                '        End If
                '    End With
                'End If

                'ds = New DataSet
                'If .FindByTable("fcs_CostCenters", "ID=" & .Cost2 & " And CostCenterLevelID in (Select ID From fcs_CostCentersLevels Where Rank=2)", ds) Then
                '    item = New System.Web.UI.WebControls.ListItem()
                '    With ds.Tables(0).Rows(0)
                '        If .Item("ID") > 0 Then
                '            item.Value = ClsEmployees.Cost2
                '            item.Text = .Item("Code") & " - " & ObjNavigationHandler.SetLanguage(Me.Page, .Item("EngName") & "/" & .Item("ArbName"))
                '            If (item.Text.Trim = "") Then
                '                item.Text = .Item("Code") & " - " & ObjNavigationHandler.SetLanguage(Me.Page, .Item("ArbName") & "/" & .Item("EngName"))
                '            End If
                '            If Not ddlCost2.Items.Contains(item) Then
                '                ddlCost2.Items.Add(item)
                '                ddlCost2.SelectedValue = item.Value
                '            Else
                '                ddlCost2.SelectedValue = ClsEmployees.Cost2
                '            End If
                '        End If
                '    End With
                'End If

                'ds = New DataSet
                'If .FindByTable("fcs_CostCenters", "ID=" & .Cost3 & " And CostCenterLevelID in (Select ID From fcs_CostCentersLevels Where Rank=3)", ds) Then
                '    item = New System.Web.UI.WebControls.ListItem()
                '    With ds.Tables(0).Rows(0)
                '        If .Item("ID") > 0 Then
                '            item.Value = ClsEmployees.Cost3
                '            item.Text = .Item("Code") & " - " & ObjNavigationHandler.SetLanguage(Me.Page, .Item("EngName") & "/" & .Item("ArbName"))
                '            If (item.Text.Trim = "") Then
                '                item.Text = .Item("Code") & " - " & ObjNavigationHandler.SetLanguage(Me.Page, .Item("ArbName") & "/" & .Item("EngName"))
                '            End If
                '            If Not ddlSponsor.Items.Contains(item) Then
                '                ddlCost3.Items.Add(item)
                '                ddlCost3.SelectedValue = item.Value
                '            Else
                '                ddlCost3.SelectedValue = ClsEmployees.Cost3
                '            End If
                '        End If
                '    End With
                'End If

                'ds = New DataSet
                'If .FindByTable("fcs_CostCenters", "ID=" & .Cost4 & " And CostCenterLevelID in (Select ID From fcs_CostCentersLevels Where Rank=4)", ds) Then
                '    item = New System.Web.UI.WebControls.ListItem()
                '    With ds.Tables(0).Rows(0)
                '        If .Item("ID") > 0 Then
                '            item.Value = ClsEmployees.Cost4
                '            item.Text = .Item("Code") & " - " & ObjNavigationHandler.SetLanguage(Me.Page, .Item("EngName") & "/" & .Item("ArbName"))
                '            If (item.Text.Trim = "") Then
                '                item.Text = .Item("Code") & " - " & ObjNavigationHandler.SetLanguage(Me.Page, .Item("ArbName") & "/" & .Item("EngName"))
                '            End If
                '            If Not ddlSponsor.Items.Contains(item) Then
                '                ddlCost4.Items.Add(item)
                '                ddlCost4.SelectedValue = item.Value
                '            Else
                '                ddlCost4.SelectedValue = ClsEmployees.Cost4
                '            End If
                '        End If
                '    End With
                'End If
            End With
        Catch ex As Exception

        End Try
    End Function

    Public Function CheckPositionsCount(EmpCode As String, PositionID As Integer) As Boolean
        Dim clsPositions As New Clshrs_Positions(Me)
        Dim clsContracts As New Clshrs_Contracts(Me)
        ClsEmployees.Find(" Code='" & EmpCode & "'")
        If ClsEmployees.ID > 0 Then
            Return True
        Else
            clsPositions.Find("ID = " & PositionID)
            If clsPositions.ApplyValidation = True Then
                Dim NoOFEmployees As Integer
                NoOFEmployees = clsPositions.EmployeesNo
                clsContracts.Find("PositionID=" & PositionID & " and CancelDate Is Null And (EndDate is null OR EndDate > GetDate())")
                If clsContracts.DataSet.Tables(0).Rows.Count > NoOFEmployees Then
                    Return False
                Else
                    Return True
                End If
            Else

                Return True
            End If

        End If


    End Function
#End Region

End Class
