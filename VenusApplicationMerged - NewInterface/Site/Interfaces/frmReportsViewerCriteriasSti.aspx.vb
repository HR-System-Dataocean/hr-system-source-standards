'================================================
' Project      : Venus 
' Module       : Report Writer
' Developer    : [0256],[0258]
' Date Created : 
'================================================
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class Interfaces_frmReportsViewerCriteriasSti
    Inherits System.Web.UI.Page

#Region "Private Members"
    Private ClsReports As ClsRpw_ReportsProperties
    Private ClsReportsCriterias As ClsRpw_ReportsCriterias
    Private ClsReportsColumns As ClsRpw_ReportColumnsProperties
    Private ObjNumericOperation() As String = {"Equal", "Not Equal", "Grater Than", "Greater Than Or Equal", "Less Than", "Less Than Or Equal "}
    Private ObjCharactersOperations() As String = {"Like", "Not Like", "Equal", "No Equal", "Greater Than", "Greater Than Or Equal", "Less Than", "Less Than Or Equal "}
    Private ObjDatesOperations() As String = {"Equal", "No Equal", "Greater Than", "Greater Than Or Equal", "Less Than", "Less Than Or Equal "}
    Private ObjBooleansOperations() As String = {"Equal", "Not Equal"}

#End Region

#Region "Protected Methods"

    '========================================================================
    'ProcedureName  :  InitializeCulture 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  
    '                1-Get language form Cookies 
    '                2-Create Calture from the incoming language 
    '                3-Apply the calture on the current form
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StrReportCode As String = Request.QueryString.Item("Code")

        ClsReports = New ClsRpw_ReportsProperties(Page)
        ClsReportsCriterias = New ClsRpw_ReportsCriterias(Page)
        If Not IsPostBack Then
            Page.Session("rpt") = ""
        End If
        PrepareSessions()

        ClsReports.Find("Code='" & StrReportCode & "'")
        If ClsReports.ID > 0 Then
            ClsReportsCriterias.Find("ReportID = " & ClsReports.ID & " Order By Rank ")
            CreateParameters(ClsReportsCriterias, StrReportCode, ClsReports)
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Me.Page, " Error Creating Report Criteria Fields !")
        End If
    End Sub

#End Region

#Region "Private Methods"
    Public Function CreateParameters(ByVal ClsReportsParameters As ClsRpw_ReportsCriterias, ByVal ReportCode As String, ByVal ClsReports As ClsRpw_ReportsProperties) As Boolean
        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim ClsSearchs As New ClsSys_Search_Main(Me.Page)

        Dim DefaultValueEditor As New Clshrs_DefaultValuesSolver(ClsSearchs.ConnectionString, Me.Page)


        Dim ObjRow As Data.DataRow
        Dim IntY_Pos As Integer = 20
        Dim StrX_Pos As String = "26%"
        Dim StrO_Pos As String = "23%"
        Dim StrlblX_Pos As String = "20px"
        Dim StrbtnX_Pos As String = "44%"
        Dim StrParametersNames As String = String.Empty
        Dim StrParametersSQLNames As String = String.Empty
        Dim StrOperations As String = String.Empty
        Dim IntCounterSectionOne As Integer = 0
        Dim IntCounterSectionTwo As Integer = 0
        Dim IntCounterSectionThree As Integer = 0
        Dim IntCounter As Integer = 0
        Dim SectionID As Integer = 1
        Dim StrLangLbl As String = String.Empty
        Dim StrFontName As String = String.Empty
        Dim StrDeafultName As String = String.Empty

        '//Get Current Culture 
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        Dim _culture As String = String.Empty
        Dim blArCulture As Boolean
        Dim literalName As String = String.Empty

        Dim AspOpLBL As System.Web.UI.WebControls.Label
        Dim ASPTXT As New System.Web.UI.WebControls.TextBox
        Dim ASPBTN As Infragistics.WebUI.WebDataInput.WebImageButton
        Dim ASPDATE As Infragistics.WebUI.WebSchedule.WebDateChooser
        Dim ASPMsk As Infragistics.WebUI.WebDataInput.WebMaskEdit
        Dim ASPLBL As System.Web.UI.WebControls.Label
        Dim ASPNNUM As Web.UI.WebControls.TextBox
        Dim ASPMONY As Infragistics.WebUI.WebDataInput.WebNumericEdit
        Dim ASPDRD As System.Web.UI.WebControls.DropDownList
        Dim ObjItem As System.Web.UI.WebControls.ListItem
        Dim FieldName As String = String.Empty
        Dim StrObject As String = String.Empty
        Dim StrOperationValue As String

        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        _culture = StrLanguage

        If (_culture <> "en-US") Then

            blArCulture = True
            StrLangLbl = "ArbDescription"
            StrFontName = "Tahoma"
            StrDeafultName = "FieldArbDefaultSet"
            UltraWebTab1.TabOrientation = Infragistics.WebUI.UltraWebTab.TabOrientation.TopRight
            ChkArabicView.Checked = True
        Else

            StrLangLbl = "EngDescription"
            StrFontName = "Arail"
            StrDeafultName = "FieldEngDefaultSet"
            ChkArabicView.Checked = False
        End If

        Try
            For Each ObjRow In ClsReportsParameters.DataSet.Tables(0).Rows
                If ObjRow.Item("Status") = True Then
                    If blArCulture Then
                        '// Prepare For Arabic Layout
                        If mDataHandler.DataValue_Out(ObjRow.Item("FieldLanguage"), Data.SqlDbType.Int) = 1 Then
                            Continue For
                        Else
                            If IntCounterSectionTwo > 10 Then SectionID = 2
                            If IntCounterSectionOne > 10 Then SectionID = 3
                            If SectionID = 2 Then
                                StrX_Pos = "36%"
                                StrO_Pos = "52%"
                                StrlblX_Pos = "50%"
                                StrbtnX_Pos = "32%"
                                IntCounter = IntCounterSectionOne
                            ElseIf SectionID = 3 Then
                                StrX_Pos = "5%"
                                StrO_Pos = "21%"
                                StrlblX_Pos = "18%"
                                StrbtnX_Pos = "1%"
                                IntCounter = IntCounterSectionThree
                            Else
                                StrX_Pos = "69%"
                                StrO_Pos = "86%"
                                StrlblX_Pos = "84%"
                                StrbtnX_Pos = "64%"
                                IntCounter = IntCounterSectionTwo
                            End If
                        End If
                    Else
                        '// Prepare For English Layout
                        If mDataHandler.DataValue_Out(ObjRow.Item("FieldLanguage"), Data.SqlDbType.Int) <> 2 Then
                            If IntCounterSectionTwo > 10 Then SectionID = 2
                            If IntCounterSectionOne > 10 Then SectionID = 3
                            If SectionID = 2 Then
                                StrX_Pos = "53%"
                                StrO_Pos = "50%"
                                StrlblX_Pos = "38%"
                                StrbtnX_Pos = "68%"
                                IntCounter = IntCounterSectionOne
                            ElseIf SectionID = 3 Then
                                StrX_Pos = "82%"
                                StrO_Pos = "79%"
                                StrlblX_Pos = "72%"
                                StrbtnX_Pos = "88%"
                                IntCounter = IntCounterSectionThree
                            Else
                                StrX_Pos = "18%"
                                StrO_Pos = "15%"
                                StrlblX_Pos = "10px"
                                StrbtnX_Pos = "35%"
                                IntCounter = IntCounterSectionTwo
                            End If

                        Else
                            Continue For
                        End If
                    End If


                    If ObjRow.Item(StrLangLbl) Is DBNull.Value Then
                        literalName = ObjRow.Item("FieldName")
                    Else
                        literalName = ObjRow.Item(StrLangLbl)
                    End If
                    ASPLBL = New System.Web.UI.WebControls.Label
                    ASPLBL.ID = "lbl" & ObjRow.Item("FieldName")
                    ASPLBL.Style.Item("POSITION") = " absolute"
                    ASPLBL.Style.Item("LEFT") = StrlblX_Pos
                    ASPLBL.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                    If blArCulture Then
                        ASPLBL.Style.Item("TEXT-ALIGN") = "right"
                        ASPLBL.Style.Item("DIRECTION") = "rtl"
                    End If
                    ASPLBL.Width = Unit.Pixel(100)
                    ASPLBL.Text = literalName
                    ASPLBL.Font.Name = StrFontName
                    ASPLBL.Font.Size = FontSize.Large
                    pnlCriterias.Controls.Add(ASPLBL)
                    AspOpLBL = New System.Web.UI.WebControls.Label
                    AspOpLBL.Style.Item("POSITION") = " absolute "
                    AspOpLBL.Style.Item("LEFT") = StrO_Pos

                    AspOpLBL.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                    AspOpLBL.Width = System.Web.UI.WebControls.Unit.Percentage(21)

                    AspOpLBL.Font.Name = "Arial"
                    AspOpLBL.Font.Size = FontSize.Medium
                    AspOpLBL.ForeColor = Drawing.Color.Blue


                    Select Case ObjRow.Item("DataType")
                        Case "String", "varchar", "char"
                            StrOperationValue = mDataHandler.DataValue_Out(ObjRow.Item("Operation"), Data.SqlDbType.VarChar)
                            If IsNumeric(StrOperationValue) Then
                                AspOpLBL.Text = ObjCharactersOperations(StrOperationValue)
                            Else
                                AspOpLBL.Text = StrOperationValue
                            End If
                            ASPTXT = New System.Web.UI.WebControls.TextBox
                            ASPTXT.ID = "txt" & mDataHandler.DataValue_Out(ObjRow.Item("FieldName"), Data.SqlDbType.VarChar)
                            ASPTXT.Style.Item("POSITION") = " absolute"
                            ASPTXT.Style.Item("LEFT") = StrX_Pos
                            ASPTXT.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            If (IIf(String.IsNullOrEmpty(Convert.ToString(ObjRow.Item("IsActive"))), True, ObjRow.Item("IsActive"))) = False Then
                                ASPTXT.Enabled = False
                            End If
                            If blArCulture Then
                                ASPTXT.Style.Item("TEXT-ALIGN") = "right"
                                ASPTXT.Style.Item("DIRECTION") = "rtl"
                            End If

                            ASPTXT.Text = mDataHandler.DataValue_Out(ObjRow.Item("Defaultvalue"), Data.SqlDbType.VarChar)
                            ASPTXT.Width = System.Web.UI.WebControls.Unit.Pixel(100)
                            ASPTXT.CssClass = "TextBoxSearchCriteria"
                            ASPTXT.BackColor = Drawing.Color.White

                            pnlCriterias.Controls.Add(ASPTXT)
                            If mDataHandler.DataValue_Out(ObjRow.Item("FieldLanguage"), Data.SqlDbType.Int) = 2 Then
                                Venus.Shared.Web.ClientSideActions.SetLanguage(Page, ASPTXT, Venus.Shared.Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                            End If
                            If Not ObjRow.Item("SearchID") Is DBNull.Value And mDataHandler.DataValue_Out(ObjRow.Item("SearchID"), Data.SqlDbType.Int) > 0 Then
                                ASPBTN = New Infragistics.WebUI.WebDataInput.WebImageButton
                                ASPBTN.ID = "btn" & ObjRow.Item("FieldName")
                                ASPBTN.Style.Item("POSITION") = " absolute"
                                ASPBTN.Style.Item("LEFT") = StrbtnX_Pos
                                ASPBTN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                                ASPBTN.Style.Item("BACKGROUND-COLOR") = "BLUE"
                                ASPBTN.Width = System.Web.UI.WebControls.Unit.Pixel(23)
                                ASPBTN.Height = System.Web.UI.WebControls.Unit.Pixel(14)
                                ASPBTN.Text = "...."
                                ASPBTN.Appearance.Image.Url = "../Interfaces/forum_search.gif"
                                ASPBTN.AutoSubmit = False
                                If (IIf(String.IsNullOrEmpty(Convert.ToString(ObjRow.Item("IsActive"))), True, ObjRow.Item("IsActive"))) = False Then
                                    ASPBTN.Enabled = False
                                End If
                                Dim UrlString = "'../Pages/HR/frmModalSearchScreen.aspx?TargetControl=" & ASPTXT.ID & "&SearchID=" & ObjRow.Item("SearchID") & "&',510,720,false,'" & ASPTXT.ClientID & "'"
                                ASPBTN.ClientSideEvents.Click = "OpenModal1(" & UrlString & ")"

                                'Venus.Shared.Web.ClientSideActions.SetSearchButton(Page, ASPBTN, ASPTXT, ObjRow.Item("SearchID"), " and canceldate is null")
                                pnlCriterias.Controls.Add(ASPBTN)
                                StrObject = ClsSearchs.fnGetTableName(ObjRow.Item("SearchID"))
                                FieldName = " Select ID from  " & StrObject & " WHERE CODE "
                                StrParametersNames &= "/" & "txt" & ObjRow.Item("FieldName")

                                If ClsReports.ReportSource = 0 Then StrParametersSQLNames &= "/" & ObjRow.Item("SqlName") & "=(" & FieldName
                                StrOperations &= "/" & IIf(ObjRow.Item("Operation") Is DBNull.Value, "=", ObjRow.Item("Operation"))
                            Else
                                StrParametersNames &= "/" & "txt" & ObjRow.Item("FieldName")
                                If ClsReports.ReportSource = 0 Then StrParametersSQLNames &= "/" & ObjRow.Item("SqlName")
                                StrOperations &= "/" & IIf(ObjRow.Item("Operation") Is DBNull.Value, "=", ObjRow.Item("Operation"))
                            End If


                        Case "Mask"
                            StrOperationValue = mDataHandler.DataValue_Out(ObjRow.Item("Operation"), Data.SqlDbType.VarChar)
                            If IsNumeric(StrOperationValue) Then
                                AspOpLBL.Text = ObjDatesOperations(StrOperationValue)
                            Else
                                AspOpLBL.Text = StrOperationValue
                            End If
                            ASPMsk = New Infragistics.WebUI.WebDataInput.WebMaskEdit
                            ASPMsk.ID = "Msk" & ObjRow.Item("FieldName")
                            ASPMsk.Style.Item("POSITION") = " absolute"
                            ASPMsk.Style.Item("LEFT") = StrX_Pos
                            ASPMsk.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"

                            ASPMsk.CssClass = "TextBoxSearchCriteria"
                            ASPMsk.Width = System.Web.UI.WebControls.Unit.Pixel(100)
                            ASPMsk.InputMask = "##/##/####"
                            ASPMsk.Text = mDataHandler.DataValue_Out(ObjRow.Item("Defaultvalue"), Data.SqlDbType.VarChar)
                            If (IIf(String.IsNullOrEmpty(Convert.ToString(ObjRow.Item("IsActive"))), True, ObjRow.Item("IsActive"))) = False Then
                                ASPMsk.Enabled = False
                            End If
                            If blArCulture Then
                                ASPMsk.Style.Item("TEXT-ALIGN") = "right"
                                ASPMsk.Style.Item("DIRECTION") = "rtl"
                            End If
                            StrParametersNames &= "/" & "Msk" & ObjRow.Item("FieldName")
                            If ClsReports.ReportSource = 0 Then StrParametersSQLNames &= "/" & ObjRow.Item("SqlName")
                            StrOperations &= "/" & IIf(ObjRow.Item("Operation") Is DBNull.Value, "=", ObjRow.Item("Operation"))

                            pnlCriterias.Controls.Add(ASPMsk)

                        Case "DateTime", "smalldatetime", "Date"
                            StrOperationValue = mDataHandler.DataValue_Out(ObjRow.Item("Operation"), Data.SqlDbType.VarChar)
                            If IsNumeric(StrOperationValue) Then
                                AspOpLBL.Text = ObjDatesOperations(StrOperationValue)
                            Else
                                AspOpLBL.Text = StrOperationValue
                            End If
                            ASPDATE = New Infragistics.WebUI.WebSchedule.WebDateChooser
                            ASPDATE.ID = "Dte" & ObjRow.Item("FieldName")
                            ASPDATE.Style.Item("POSITION") = " absolute"
                            ASPDATE.Style.Item("LEFT") = StrX_Pos
                            ASPDATE.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            'ASPDATE.Value = ObjRow.Item("DefaultValue")
                            'ASPDATE.Value = DefaultValueEditor.EvaluateExpressionDates(ObjRow.Item("DefaultValue"))
                            If Not ObjRow.Item("DefaultValue") Is DBNull.Value Then
                                ASPDATE.Value = DefaultValueEditor.EvaluateExpressionDates(ObjRow.Item("DefaultValue"))
                            End If

                            ASPDATE.CssClass = "DateBoxSearchCriteria"
                            ASPDATE.Width = System.Web.UI.WebControls.Unit.Pixel(100)
                            ASPDATE.Format = Infragistics.WebUI.WebSchedule.DateFormat.Short

                            If (IIf(String.IsNullOrEmpty(Convert.ToString(ObjRow.Item("IsActive"))), True, ObjRow.Item("IsActive"))) = False Then
                                ASPDATE.Enabled = False
                            End If
                            If blArCulture Then
                                ASPDATE.Style.Item("TEXT-ALIGN") = "right"
                                ASPDATE.Style.Item("DIRECTION") = "rtl"
                            End If
                            StrParametersNames &= "/" & "Dte" & ObjRow.Item("FieldName")
                            If ClsReports.ReportSource = 0 Then StrParametersSQLNames &= "/" & ObjRow.Item("SqlName")
                            StrOperations &= "/" & IIf(ObjRow.Item("Operation") Is DBNull.Value, "=", ObjRow.Item("Operation"))
                            ASPDATE.CalendarLayout.DayHeaderStyle.BackColor = Drawing.Color.AliceBlue
                            ASPDATE.CalendarLayout.DayHeaderStyle.ForeColor = Drawing.Color.Black
                            ASPDATE.CalendarLayout.DayHeaderStyle.Font.Name = "Tahoma"
                            ASPDATE.CalendarLayout.DayHeaderStyle.Font.Size = FontSize.Large
                            ASPDATE.CalendarLayout.DayStyle.BackColor = Drawing.Color.White
                            ASPDATE.CalendarLayout.DayStyle.Font.Name = "Arial"
                            ASPDATE.CalendarLayout.DayStyle.Font.Size = FontSize.Large
                            ASPDATE.CalendarLayout.OtherMonthDayStyle.ForeColor = Drawing.Color.Chocolate
                            ASPDATE.CalendarLayout.SelectedDayStyle.BackColor = Drawing.Color.DarkKhaki
                            ASPDATE.CalendarLayout.ShowFooter = False
                            ASPDATE.CalendarLayout.ShowNextPrevMonth = False
                            ASPDATE.CalendarLayout.ShowTitle = False
                            ASPDATE.CalendarLayout.TitleStyle.BackColor = Drawing.Color.LightSteelBlue
                            ASPDATE.ExpandEffects.ShadowColor = Drawing.Color.LightGray
                            ASPDATE.ExpandEffects.Type = Infragistics.WebUI.WebDropDown.ExpandEffectType.Slide
                            ASPDATE.Height = Unit.Pixel(18)
                            ASPDATE.Width = Unit.Pixel(105)
                            ASPDATE.NullDateLabel = ""
                            ASPDATE.Value = mDataHandler.DataValue_Out(ObjRow.Item("DefaultValue"), Data.SqlDbType.DateTime)


                            'Need To BE tESTED well [0256]
                            Dim DateTimeFormat As New System.Globalization.DateTimeFormatInfo
                            DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
                            DateTimeFormat.LongDatePattern = "yyyy/MM/dd"

                            'ASPDATE.CalendarLayout.Culture.DateTimeFormat = DateTimeFormat

                            ASPDATE.ClientSideEvents.ValueChanged = "CheckDateEntryValue('" & ASPDATE.ID & "','" & ObjRow.Item("MinimumValue") & "','" & ObjRow.Item("MaximumValue") & "')"

                            pnlCriterias.Controls.Add(ASPDATE)

                        Case "Int32", "Int64", "Integer", "Single", "Int", "Real", "Float"
                            StrOperationValue = mDataHandler.DataValue_Out(ObjRow.Item("Operation"), Data.SqlDbType.VarChar)
                            If IsNumeric(StrOperationValue) Then
                                AspOpLBL.Text = ObjNumericOperation(StrOperationValue)
                            Else
                                AspOpLBL.Text = StrOperationValue
                            End If
                            ASPNNUM = New Web.UI.WebControls.TextBox
                            ASPNNUM.ID = "Num" & ObjRow.Item("FieldName")
                            ASPNNUM.Style.Item("POSITION") = " absolute"
                            ASPNNUM.Style.Item("LEFT") = StrX_Pos
                            ASPNNUM.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPNNUM.Text = mDataHandler.DataValue_Out(ObjRow.Item("DefaultValue"), Data.SqlDbType.Int)
                            If Not ObjRow.Item("DefaultValue") Is DBNull.Value Then
                                ASPNNUM.Text = DefaultValueEditor.EvaluateExpression(mDataHandler.DataValue_Out(ObjRow.Item("DefaultValue"), Data.SqlDbType.Int))
                            Else

                            End If
                            ASPNNUM.Width = System.Web.UI.WebControls.Unit.Pixel(100)
                            ASPNNUM.CssClass = "NumericBoxSearchCriteria"
                            ASPNNUM.BackColor = Drawing.Color.White
                            Venus.Shared.Web.ClientSideActions.SetupForNumberOnly(Me.Page, ASPNNUM, True)
                            If (IIf(String.IsNullOrEmpty(Convert.ToString(ObjRow.Item("IsActive"))), True, ObjRow.Item("IsActive"))) = False Then
                                ASPNNUM.Enabled = False
                            End If
                            If blArCulture Then
                                ASPNNUM.Style.Item("TEXT-ALIGN") = "right"
                                ASPNNUM.Style.Item("DIRECTION") = "rtl"
                            End If
                            ASPNNUM.Attributes.Add("onblur", "CheckTxtEntryValue('" & ASPNNUM.ID & "','" & ObjRow.Item("MinimumValue") & "','" & ObjRow.Item("MaximumValue") & "')")
                            pnlCriterias.Controls.Add(ASPNNUM)
                            If Not ObjRow.Item("SearchID") Is DBNull.Value Then
                                ASPBTN = New Infragistics.WebUI.WebDataInput.WebImageButton
                                ASPBTN.ID = "btn" & ObjRow.Item("FieldName")
                                ASPBTN.Style.Item("POSITION") = " absolute"
                                ASPBTN.Style.Item("LEFT") = StrbtnX_Pos
                                ASPBTN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                                ASPBTN.Style.Item("BACKGROUND-COLOR") = "BLUE"
                                ASPBTN.Width = System.Web.UI.WebControls.Unit.Pixel(23)
                                ASPBTN.Height = System.Web.UI.WebControls.Unit.Pixel(14)
                                ASPBTN.Text = "...."
                                ASPBTN.Appearance.Image.Url = "../Interfaces/forum_search.gif"
                                Venus.Shared.Web.ClientSideActions.SetSearchButton(Page, ASPBTN, ASPNNUM, ObjRow.Item("SearchID"), " and canceldate is null")
                                pnlCriterias.Controls.Add(ASPBTN)
                                If (IIf(String.IsNullOrEmpty(Convert.ToString(ObjRow.Item("IsActive"))), True, ObjRow.Item("IsActive"))) = False Then
                                    ASPBTN.Enabled = False
                                End If
                                StrObject = ClsSearchs.fnGetTableName(ObjRow.Item("SearchID"))
                                FieldName = " Select ID from  " & StrObject & " WHERE CODE "
                                StrParametersNames &= "/" & "Num" & ObjRow.Item("FieldName") & "=(" & FieldName
                                If ClsReports.ReportSource = 0 Then StrParametersSQLNames &= "/" & ObjRow.Item("SqlName") & "=(" & FieldName
                            Else
                                StrParametersNames &= "/" & "Num" & ObjRow.Item("FieldName")
                                If ClsReports.ReportSource = 0 Then StrParametersSQLNames &= "/" & ObjRow.Item("SqlName")
                            End If
                            StrOperations &= "/" & IIf(ObjRow.Item("Operation") Is DBNull.Value, "=", ObjRow.Item("Operation"))

                        Case "Double", "Money", "Numeric"
                            StrOperationValue = mDataHandler.DataValue_Out(ObjRow.Item("Operation"), Data.SqlDbType.VarChar)
                            If IsNumeric(StrOperationValue) Then
                                AspOpLBL.Text = ObjNumericOperation(StrOperationValue)
                            Else
                                AspOpLBL.Text = StrOperationValue
                            End If

                            ASPMONY = New Infragistics.WebUI.WebDataInput.WebNumericEdit
                            ASPMONY.ID = "Cur" & ObjRow.Item("FieldName")
                            ASPMONY.Style.Item("POSITION") = " absolute"
                            ASPMONY.Style.Item("LEFT") = StrX_Pos
                            ASPMONY.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPMONY.Value = ObjRow.Item("DefaultValue")
                            ASPMONY.CssClass = "MonyBoxSearchCriteria"
                            ASPMONY.BackColor = Drawing.Color.White
                            ASPMONY.Width = System.Web.UI.WebControls.Unit.Pixel(100)
                            StrParametersNames &= "/" & "Cur" & ObjRow.Item("FieldName")
                            If ClsReports.ReportSource = 0 Then StrParametersSQLNames &= "/" & ObjRow.Item("SqlName")
                            StrOperations &= "/" & IIf(ObjRow.Item("Operation") Is DBNull.Value, "=", ObjRow.Item("Operation"))
                            ASPMONY.Text = mDataHandler.DataValue_Out(ObjRow.Item("DefaultValue"), Data.SqlDbType.Money)
                            If (IIf(String.IsNullOrEmpty(Convert.ToString(ObjRow.Item("IsActive"))), True, ObjRow.Item("IsActive"))) = False Then
                                ASPMONY.Enabled = False
                            End If
                            If blArCulture Then
                                ASPMONY.Style.Item("TEXT-ALIGN") = "right"
                                ASPMONY.Style.Item("DIRECTION") = "rtl"
                            End If
                            ASPMONY.Attributes.Add("onblur", "CheckTxtEntryValue('" & ASPMONY.ID & "','" & ObjRow.Item("MinimumValue") & "','" & ObjRow.Item("MaximumValue") & "')")
                            pnlCriterias.Controls.Add(ASPMONY)

                        Case "Boolean", "Bit"
                            StrOperationValue = mDataHandler.DataValue_Out(ObjRow.Item("Operation"), Data.SqlDbType.VarChar)
                            If IsNumeric(StrOperationValue) Then
                                AspOpLBL.Text = ObjBooleansOperations(StrOperationValue)
                            Else
                                AspOpLBL.Text = StrOperationValue
                            End If


                            ASPDRD = New System.Web.UI.WebControls.DropDownList
                            ObjItem = New System.Web.UI.WebControls.ListItem
                            ObjItem.Text = IIf(blArCulture, "äÚã", " Yes ")
                            ObjItem.Value = 1
                            ASPDRD.Items.Add(ObjItem)
                            ObjItem = New System.Web.UI.WebControls.ListItem
                            ObjItem.Text = IIf(blArCulture, "áÇ", " No  ")
                            ObjItem.Value = 0
                            ASPDRD.Items.Add(ObjItem)
                            ASPDRD.ID = "Drd" & ObjRow.Item("FieldName")
                            ASPDRD.Style.Item("POSITION") = " absolute"
                            ASPDRD.Style.Item("LEFT") = StrX_Pos
                            ASPDRD.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPDRD.SelectedValue = mDataHandler.DataValue_Out(ObjRow.Item("DefaultValue"), Data.SqlDbType.Int)
                            ASPDRD.CssClass = "DdlSearchCriteria"
                            ASPDRD.BackColor = Drawing.Color.White
                            ASPDRD.Width = System.Web.UI.WebControls.Unit.Pixel(100)
                            ASPDRD.SelectedValue = mDataHandler.DataValue_Out(ObjRow.Item("DefaultValue"), Data.SqlDbType.Int)
                            If (IIf(String.IsNullOrEmpty(Convert.ToString(ObjRow.Item("IsActive"))), True, ObjRow.Item("IsActive"))) = False Then
                                ASPDRD.Enabled = False
                            End If
                            If blArCulture Then
                                ASPDRD.Style.Item("TEXT-ALIGN") = "right"
                                ASPDRD.Style.Item("DIRECTION") = "rtl"
                            End If
                            StrParametersNames &= "/" & "Drd" & ObjRow.Item("FieldName")
                            If ClsReports.ReportSource = 0 Then StrParametersSQLNames &= "/" & ObjRow.Item("SqlName")
                            StrOperations &= "/" & IIf(ObjRow.Item("Operation") Is DBNull.Value, "=", ObjRow.Item("Operation"))
                            pnlCriterias.Controls.Add(ASPDRD)

                        Case "Int16", "Byte", "Smallint", "Tinyint"
                            StrOperationValue = mDataHandler.DataValue_Out(ObjRow.Item("Operation"), Data.SqlDbType.VarChar)
                            If IsNumeric(StrOperationValue) Then
                                AspOpLBL.Text = ObjNumericOperation(StrOperationValue)
                            Else
                                AspOpLBL.Text = StrOperationValue
                            End If

                            ASPDRD = New System.Web.UI.WebControls.DropDownList
                            ASPDRD.ID = "Drl" & ObjRow.Item("FieldName")
                            ASPDRD.Style.Item("POSITION") = " absolute"
                            ASPDRD.Style.Item("LEFT") = StrX_Pos
                            ASPDRD.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPDRD.CssClass = "DdlSearchCriteria"
                            ASPDRD.Width = System.Web.UI.WebControls.Unit.Pixel(100)
                            ASPDRD.BackColor = Drawing.Color.White
                            If (IIf(String.IsNullOrEmpty(Convert.ToString(ObjRow.Item("IsActive"))), True, ObjRow.Item("IsActive"))) = False Then
                                ASPDRD.Enabled = False
                            End If
                            If blArCulture Then
                                ASPDRD.Style.Item("TEXT-ALIGN") = "right"
                                ASPDRD.Style.Item("DIRECTION") = "rtl"
                            End If

                            ClsReportsCriterias.FillDefualts(ASPDRD, ObjRow.Item(StrDeafultName), ObjRow.Item("FieldDefaultSetValues"))

                            StrParametersNames &= "/" & "Drl" & ObjRow.Item("FieldName")
                            If ClsReports.ReportSource = 0 Then StrParametersSQLNames &= "/" & ObjRow.Item("SqlName")
                            StrOperations &= "/" & IIf(ObjRow.Item("Operation") Is DBNull.Value, "=", ObjRow.Item("Operation"))
                            ASPDRD.Text = mDataHandler.DataValue_Out(ObjRow.Item("DefaultValue"), Data.SqlDbType.SmallInt)
                            pnlCriterias.Controls.Add(ASPDRD)
                    End Select

                    pnlCriterias.Controls.Add(AspOpLBL)
                    If SectionID = 2 Then
                        IntCounterSectionOne += 1
                    ElseIf SectionID = 3 Then
                        IntCounterSectionThree += 1
                    Else
                        IntCounterSectionTwo += 1
                    End If
                End If
            Next

            If StrParametersNames.Length > 0 Then
                If StrParametersSQLNames.Length = 0 Then
                    For IntI As Int16 = 0 To IntCounter - 1
                        StrParametersSQLNames &= "/" & "zzzzz"
                    Next
                End If
                txtOperations.Value = StrOperations.Substring(1)
                txtReportCode.Value = ReportCode
                If Len(StrParametersSQLNames) > 0 Then
                    txtSqlNames.Value = StrParametersSQLNames.Substring(1)
                Else
                    txtSqlNames.Value = ""
                End If
                If Len(StrParametersNames) > 0 Then
                    txtRealNames.Value = StrParametersNames.Substring(1)
                Else
                    txtRealNames.Value = ""
                End If
            Else
                txtOperations.Value = StrOperations
                txtReportCode.Value = ReportCode
                txtSqlNames.Value = StrParametersSQLNames
                txtRealNames.Value = StrParametersNames
            End If

        Catch ex As Exception

        End Try
    End Function

    Private Function PrepareSessions() As Boolean

        Session("Step1") = Nothing
        Session("Step3_1") = Nothing
        Session("Step3_2") = Nothing
        If Not IsNothing(Session("CreatedFields")) Then
            Session("CreatedFields") = Nothing
        End If
        If Not IsNothing(Session("GroupByColumns")) Then
            Session("GroupByColumns") = Nothing
        End If
        If Not IsNothing(Session("DefaultDs")) Then
            Session("DefaultDs") = Nothing
        End If

        If Not IsNothing(Session("CalcFieldsNo")) Then
            Session("CalcFieldsNo") = Nothing
        End If

        If Not IsNothing(Session("Formula")) Then
            Session("Formula") = Nothing
        End If
    End Function
#End Region

    <System.Web.Services.WebMethod()>
    Public Shared Function SetSessione(ByVal str As String) As String
        HttpContext.Current.Session("rpt") = str
        Return ""
    End Function

End Class