'======================================================================
'Project name  : Venus V. 
'Program name  : ClsSys_Search_Main.vb
'Date Created  : 19-07-2007
'Issue #       :       
'Developer     : Data Ocean
'              : [0256] 
'Description   : This Class acts as the OG Layer for frmSearchScreen 
'              : This Class Recives the SearchID  -Screen to be searched - And prepare the Parameters according to Thier types
'              : 
'              : 
'Modifacations :
'              : [0256] 03-04-2008
'              : Code QC 
'              : 
'======================================================================

Public Class ClsSys_Search_Main
    Inherits ClsDataAcessLayer

#Region "Constants"
    

#End Region

#Region "Class Constructors"
    Public Sub New(ByVal page As Web.UI.Page)
        MyBase.New(page)
    End Sub
#End Region

#Region "Private Members"
    Private mStrSearchName As String
    Private mIntID As Integer
    Private mObjSearchViews As Clssys_Searchs
    Private mObjSearchViewsColumns As Clssys_SearchsColumns
#End Region

#Region "Public Property"
    Public Property SearchName() As String
        Get
            Return mStrSearchName
        End Get
        Set(ByVal value As String)
            mStrSearchName = value
        End Set
    End Property

    Public Property SearchID() As Integer
        Get
            Return mIntID
        End Get
        Set(ByVal value As Integer)
            mIntID = value
        End Set
    End Property
#End Region

#Region "public Methods"
    '==================================================================================
    'ProcedureName  : ReadSearchName()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : The Function is used to check if the screen being searched already exisit in sys_searchs table or not 
    '               : and Fill the class prameters with data according to searchID -The Screen Being Searched -
    '               : if the function find a row for the screen being searched  
    'Developer      : Data Ocean
    '               : [AGR] Abdul Jaleel R. Ossman
    'Date Created   : DataOcean 
    'Date Modified  : 19-07-2007 
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : mObjSearchViews.Find() : Check if there's any row in sys_searchs file has the ID -SarchID 
    '               : 
    'Called From    : frmSearchSetting.aspx.vb  
    '               : frmSearchScreen.Page_Load() -- Event 
    '               : frmSearchScreen.SetData() -- Method 
    'Ex.            : ReadSearchName(13) 
    '==================================================================================
    Public Function ReadSearchName(ByVal SearchID As Integer, ByVal strLanguage As String) As String
        Dim StrReturnedName As String
        Dim objNavigation As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        mObjSearchViews = New Clssys_Searchs(mPage)
        Try
            mObjSearchViews.Find("ID=" & SearchID)
            If (strLanguage = "en-US") Then
                StrReturnedName = mObjSearchViews.EngName
                If (StrReturnedName = "") Then
                    StrReturnedName = mObjSearchViews.ArbName
                End If
            Else
                StrReturnedName = mObjSearchViews.ArbName
                If (StrReturnedName = "") Then
                    StrReturnedName = mObjSearchViews.EngName
                End If
            End If
            SearchName = mObjSearchViews.EngName
            SearchID = mObjSearchViews.ID
            Return StrReturnedName
        Catch ex As Exception

        End Try
    End Function

    '==================================================================================
    'ProcedureName  : ReadSearchParameter
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This Function Read the Search paramters and according to those Parameters 
    '               : Data Type it Handels The Following 
    '               : 1- Read Cretiria Fields from sys_searchsColumns and creat dynamically 
    '               :    Equavillent controls 
    '               : 2- Store Parameter names in an array - that array will be sent to JS file to iterate through created 
    '               :    Controls and dealing with user data entered in them 
    '               : 3- SetFoucs For first created control 
    '               : 4- SetupTabOrder 
    '               : 5- 
    'Developer      : Data Ocean
    '               : [AGR] Abdul Jaleel R. Ossman
    'Date Created   : DataOcean 
    'Date Modified  : 22-07-2007 
    'Modifacations  : 
    '               : Modification related to same modification in ClsSys_searchs AND ClsSys_SearchsColumns Class 
    '               :
    'Calling       *:*
    'Function Calls : ClentSideAction Methids  
    'Called From    : frmSearchScreen.Page_Load() Event
    '==================================================================================
    Public Function ReadSearchParameter(ByVal ptrSearchID As Integer, _
    ByRef pnlCriterias As Web.UI.WebControls.Panel, _
    ByVal ParameterNames As Web.UI.WebControls.Label, ByVal ParameterRealName As Web.UI.WebControls.Label, _
    ByVal StrGridId As String, ByRef btnSearch As Infragistics.WebUI.WebDataInput.WebImageButton, ByVal StrLanguage As String) As Boolean
        mObjSearchViewsColumns = New Clssys_SearchsColumns(mPage)
        Dim ObjDr As DataRow
        Dim ValueCounter As Integer
        Dim str As String
        Dim litral As New Web.UI.LiteralControl
        Dim Y_Pos As Integer = 80
        Dim X_Pos As Integer = 40
        Dim Width As Integer = 100
        Dim CurrentTextBox As String
        Dim IntNoOfFields As Integer
        Dim blArCulture As Boolean = IIf(StrLanguage <> "en-US", True, False)
        Dim literalName As String = String.Empty
        Dim StrFontName As String = String.Empty
        Dim StrLangLbl As String = String.Empty
        Dim IntY_Pos As Integer = 10
        Dim StrX_Pos As String = "26%"
        Dim StrO_Pos As String = "23%"
        Dim StrlblX_Pos As String = "20px"
        Dim StrbtnX_Pos As String = "44%"
        Dim IntCounterSectionOne As Integer = 0
        Dim IntCounterSectionTwo As Integer = 0
        Dim IntCounterSectionThree As Integer = 0
        Dim IntCounter As Integer = 0
        Dim SectionID As Integer = 1
        Dim ASPLBL As Global.System.Web.UI.WebControls.Label
        Dim ASPTXT As Global.System.Web.UI.WebControls.TextBox
        Dim ASPDATE As Infragistics.WebUI.WebSchedule.WebDateChooser
        Dim ASPBTN As Infragistics.WebUI.WebDataInput.WebImageButton
        Dim ObjSearchText As Object
        ParameterNames.Text = ""
        ParameterRealName.Text = ""
        Dim Arr() As String
        Try
            If mObjSearchViewsColumns.Find("SearchID=" & ptrSearchID & " and IsCriteria=1 and (sys_SearchsColumns.RegComputerID is null or sys_SearchsColumns.RegComputerID in (2," & IIf(StrLanguage = "en-US", 0, 1) & ")) ORDER BY ISNULL(RankCriteria, 100) ") Then
                If (blArCulture) Then
                    blArCulture = True
                    StrLangLbl = "ArbName"
                    StrFontName = "Tahoma"
                Else
                    StrLangLbl = "EngName"
                    StrFontName = "Arail"
                End If
                '=================Language Setup [End]
                IntNoOfFields = mObjSearchViewsColumns.DataSet.Tables(0).Rows.Count
                ReDim Arr(IntNoOfFields)
                For Each ObjDr In mObjSearchViewsColumns.DataSet.Tables(0).Rows
                    If blArCulture Then
                        '// Prepare For Arabic Layout
                        If IntCounterSectionTwo > 3 Then SectionID = 2
                        If IntCounterSectionOne > 3 Then SectionID = 3
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
                    Else
                        If IntCounterSectionTwo > 3 Then SectionID = 2
                        If IntCounterSectionOne > 3 Then SectionID = 3
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

                    End If
                    If ObjDr.Item(StrLangLbl) Is DBNull.Value Then
                        literalName = ObjDr.Item("FieldName")
                    Else
                        literalName = ObjDr.Item(StrLangLbl)
                    End If
                    CurrentTextBox = " "
                    ASPLBL = New Global.System.Web.UI.WebControls.Label
                    ASPLBL.ID = "lbl" & ObjDr.Item("FieldName")
                    ASPLBL.Style.Item("POSITION") = " absolute"
                    ASPLBL.Style.Item("LEFT") = StrlblX_Pos
                    ASPLBL.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                    If blArCulture Then
                        ASPLBL.Style.Item("TEXT-ALIGN") = "right"
                        ASPLBL.Style.Item("DIRECTION") = "rtl"
                    End If
                    ASPLBL.Width = Global.System.Web.UI.WebControls.Unit.Pixel(100)
                    ASPLBL.Text = literalName
                    ASPLBL.Font.Name = StrFontName
                    ASPLBL.Font.Size = Web.UI.WebControls.FontSize.Large
                    pnlCriterias.Controls.Add(ASPLBL)
                    Select Case ObjDr.Item("FieldType")
                        Case 167, 239, 231
                            ASPTXT = New Web.UI.WebControls.TextBox
                            ASPTXT.ID = "WV_" & ObjDr.Item("FieldName")
                            CurrentTextBox = ObjDr.Item("FieldName")
                            ASPTXT.Style.Item("POSITION") = " absolute"
                            ASPTXT.Style.Item("LEFT") = StrX_Pos
                            ASPTXT.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPTXT.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPTXT.BackColor = Drawing.Color.White
                            ASPTXT.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPTXT.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPTXT.BorderColor = Drawing.Color.White
                            ASPTXT.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("InputLength"), SqlDbType.Int)
                            If blArCulture Then
                                ASPTXT.Style.Item("TEXT-ALIGN") = "right"
                                ASPTXT.Style.Item("DIRECTION") = "rtl"
                            End If
                            If mDataHandler.DataValue_Out(ObjDr.Item("IsArabic"), SqlDbType.Bit) = True Then
                                Venus.Shared.Web.ClientSideActions.SetLanguage(mPage, ASPTXT, [Shared].Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                            End If
                            ParameterNames.Text &= "|" & "WV_" & ObjDr.Item("FieldName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
                            Arr(ValueCounter) = "WV_" & ObjDr.Item("FieldName")
                            If ValueCounter = 0 Then
                                Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPTXT, False)
                            End If
                            'If ValueCounter = IntNoOfFields - 1 Then
                            '    ASPTXT.Attributes.Add("onBlur", "ctrlLastSearchCriteria('" & StrGridId & "');")
                            'End If
                            ObjSearchText = ASPTXT
                            pnlCriterias.Controls.Add(ASPTXT)
                        Case 56, 59, 60, 62 '"int", "money", "tinyint"
                            Dim ASPWN As New Web.UI.WebControls.TextBox
                            ASPWN.ID = "WN_" & ObjDr.Item("FieldName")
                            CurrentTextBox = "WN_" & ObjDr.Item("FieldName")
                            ASPWN.Style.Item("POSITION") = " absolute"
                            ASPWN.Style.Item("LEFT") = StrX_Pos
                            ASPWN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPWN.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPWN.BackColor = Drawing.Color.White
                            ASPWN.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWN.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWN.BorderColor = Drawing.Color.White
                            ASPWN.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("InputLength"), SqlDbType.Int)
                            ParameterNames.Text &= "|" & "WN_" & ObjDr.Item("FieldName")
                            Dim FieldName As String = String.Empty
                            If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then
                                Dim StrObject As String = fnGetTableName(ObjDr.Item("SubSearchID"))
                                FieldName = " Select ID from  " & StrObject & " WHERE CODE "
                                ParameterRealName.Text &= "|" & ObjDr.Item("FieldName") & "=(" & FieldName
                            Else
                                ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
                            End If
                            Venus.Shared.Web.ClientSideActions.SetupForNumberOnly(mPage, ASPWN, True)
                            Arr(ValueCounter) = "WN_" & ObjDr.Item("FieldName")
                            If ValueCounter = 0 Then
                                Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWN, False)
                            End If
                            'If ValueCounter = IntNoOfFields - 1 Then
                            '    ASPWN.Attributes.Add("onBlur", "ctrlLastSearchCriteria('" & StrGridId & "');")
                            'End If
                            ObjSearchText = ASPWN
                            pnlCriterias.Controls.Add(ASPWN)
                        Case 58 ' "smalldatetime"
                            ASPDATE = New Infragistics.WebUI.WebSchedule.WebDateChooser
                            ASPDATE.ID = "WD_" & ObjDr.Item("FieldName")
                            ASPDATE.Style.Item("POSITION") = " absolute"
                            ASPDATE.Style.Item("LEFT") = StrX_Pos
                            ASPDATE.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPDATE.CssClass = "DateBoxSearchCriteria"
                            ASPDATE.Width = Global.System.Web.UI.WebControls.Unit.Pixel(100)
                            ASPDATE.Format = Infragistics.WebUI.WebSchedule.DateFormat.Short
                            If blArCulture Then
                                ASPDATE.Style.Item("TEXT-ALIGN") = "right"
                                ASPDATE.Style.Item("DIRECTION") = "rtl"
                            End If
                            ASPDATE.BackColor = Drawing.Color.White
                            ASPDATE.CalendarLayout.DayHeaderStyle.BackColor = Drawing.Color.AliceBlue
                            ASPDATE.CalendarLayout.DayHeaderStyle.ForeColor = Drawing.Color.Black
                            ASPDATE.CalendarLayout.DayHeaderStyle.Font.Name = "Tahoma"
                            ASPDATE.CalendarLayout.DayHeaderStyle.Font.Size = Global.System.Web.UI.WebControls.FontSize.Large
                            ASPDATE.CalendarLayout.DayStyle.BackColor = Drawing.Color.White
                            ASPDATE.CalendarLayout.DayStyle.Font.Name = "Arial"
                            ASPDATE.CalendarLayout.DayStyle.Font.Size = Global.System.Web.UI.WebControls.FontSize.Large
                            ASPDATE.CalendarLayout.OtherMonthDayStyle.ForeColor = Drawing.Color.Chocolate
                            ASPDATE.CalendarLayout.SelectedDayStyle.BackColor = Drawing.Color.DarkKhaki
                            ASPDATE.CalendarLayout.ShowFooter = False
                            ASPDATE.CalendarLayout.ShowNextPrevMonth = False
                            ASPDATE.CalendarLayout.ShowTitle = False
                            ASPDATE.CalendarLayout.TitleStyle.BackColor = Drawing.Color.LightSteelBlue
                            ASPDATE.ExpandEffects.ShadowColor = Drawing.Color.LightGray
                            ASPDATE.ExpandEffects.Type = Infragistics.WebUI.WebDropDown.ExpandEffectType.Slide
                            ASPDATE.Height = Global.System.Web.UI.WebControls.Unit.Pixel(18)
                            ASPDATE.Width = Global.System.Web.UI.WebControls.Unit.Pixel(105)
                            ASPDATE.NullDateLabel = ""
                            ParameterNames.Text &= "|" & "WD_" & ObjDr.Item("FieldName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
                            Arr(ValueCounter) = "WD_" & ObjDr.Item("FieldName")

                            If ValueCounter = 0 Then
                                ASPDATE.Focus()
                            End If

                            'If ValueCounter = IntNoOfFields - 1 Then
                            '    ASPDATE.ClientSideEvents.OnBlur = "ctrlLastSearchCriteria('" & StrGridId & "');"
                            'End If
                            pnlCriterias.Controls.Add(ASPDATE)
                        Case 104 ' "bit"
                            Dim ASPWB As New Web.UI.WebControls.DropDownList
                            Dim ASPItem1 As New Web.UI.WebControls.ListItem
                            Dim ASPItem2 As New Web.UI.WebControls.ListItem
                            ASPItem1.Text = "True"
                            ASPItem1.Value = 0
                            ASPItem2.Text = "False"
                            ASPItem2.Value = 1
                            ASPWB.Items.Add(ASPItem1)
                            ASPWB.Items.Add(ASPItem2)
                            ASPWB.ID = "WB_" & ObjDr.Item("FieldName")
                            CurrentTextBox = "WB_" & ObjDr.Item("FieldName")
                            ASPWB.Style.Item("POSITION") = " absolute"
                            ASPWB.Style.Item("LEFT") = StrX_Pos ' CStr(X_Pos + 143) & "px"
                            ASPWB.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"
                            ASPWB.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPWB.BackColor = Drawing.Color.White

                            ASPWB.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWB.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWB.BorderColor = Drawing.Color.White

                            ParameterNames.Text &= "|" & "WB_" & ObjDr.Item("FieldName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
                            Arr(ValueCounter) = "WB_" & ObjDr.Item("FieldName")
                            If ValueCounter = 0 Then
                                Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWB, False)
                            End If
                            pnlCriterias.Controls.Add(ASPWB)

                    End Select

                    If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then
                        ASPBTN = New Infragistics.WebUI.WebDataInput.WebImageButton
                        ASPBTN.ID = "btn" & ObjDr.Item("FieldName").ToString.Replace(" ", "_")
                        ASPBTN.Style.Item("POSITION") = " absolute"
                        ASPBTN.Style.Item("LEFT") = StrbtnX_Pos
                        ASPBTN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                        ASPBTN.Style.Item("BACKGROUND-COLOR") = "BLUE"
                        ASPBTN.Width = Global.System.Web.UI.WebControls.Unit.Pixel(23)
                        ASPBTN.Height = Global.System.Web.UI.WebControls.Unit.Pixel(14)
                        Venus.Shared.Web.ClientSideActions.SetSearchButton(mPage, ASPBTN, ObjSearchText, ObjDr.Item("SubSearchID"), 615, 726)
                        pnlCriterias.Controls.Add(ASPBTN)
                    End If
                    ValueCounter += 1
                    If SectionID = 2 Then
                        IntCounterSectionOne += 1
                    ElseIf SectionID = 3 Then
                        IntCounterSectionThree += 1
                    Else
                        IntCounterSectionTwo += 1
                    End If

                Next
                If ParameterNames.Text <> "" Then
                    ParameterNames.Text = ParameterNames.Text.Substring(1)
                    ParameterRealName.Text = ParameterRealName.Text.Substring(1)
                End If
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function ReadModalSearchParameter(ByVal ptrSearchID As Integer, _
  ByRef pnlCriterias As Web.UI.WebControls.Panel, _
  ByVal ParameterNames As Web.UI.WebControls.Label, ByVal ParameterRealName As Web.UI.WebControls.Label, _
  ByVal StrGridId As String, ByVal StrLanguage As String) As Boolean
        mObjSearchViewsColumns = New Clssys_SearchsColumns(mPage)
        Dim ObjDr As DataRow
        Dim ValueCounter As Integer
        Dim str As String
        Dim litral As New Web.UI.LiteralControl
        Dim Y_Pos As Integer = 80
        Dim X_Pos As Integer = 40
        Dim Width As Integer = 100
        Dim CurrentTextBox As String
        Dim IntNoOfFields As Integer
        Dim blArCulture As Boolean = IIf(StrLanguage <> "en-US", True, False)
        Dim literalName As String = String.Empty
        Dim StrFontName As String = String.Empty
        Dim StrLangLbl As String = String.Empty
        Dim IntY_Pos As Integer = 80
        Dim StrX_Pos As String = "26%"
        Dim StrO_Pos As String = "23%"
        Dim StrlblX_Pos As String = "20px"
        Dim StrbtnX_Pos As String = "44%"
        Dim IntCounterSectionOne As Integer = 0
        Dim IntCounterSectionTwo As Integer = 0
        Dim IntCounterSectionThree As Integer = 0
        Dim IntCounter As Integer = 0
        Dim SectionID As Integer = 1
        Dim ASPLBL As Global.System.Web.UI.WebControls.Label
        Dim ASPTXT As Global.System.Web.UI.WebControls.TextBox
        Dim ASPDATE As Infragistics.WebUI.WebSchedule.WebDateChooser
        Dim ASPBTN As Infragistics.WebUI.WebDataInput.WebImageButton
        Dim ObjSearchText As Object
        ParameterNames.Text = ""
        ParameterRealName.Text = ""
        Dim Arr() As String
        Try
            If mObjSearchViewsColumns.Find("SearchID=" & ptrSearchID & " and IsCriteria=1 and (sys_SearchsColumns.RegComputerID is null or sys_SearchsColumns.RegComputerID in (2," & IIf(StrLanguage = "en-US", 0, 1) & ")) ORDER BY ISNULL(RankCriteria, 100) ") Then
                If (blArCulture) Then
                    blArCulture = True
                    StrLangLbl = "ArbName"
                    StrFontName = "Tahoma"
                Else
                    StrLangLbl = "EngName"
                    StrFontName = "Arail"
                End If
                '=================Language Setup [End]
                IntNoOfFields = mObjSearchViewsColumns.DataSet.Tables(0).Rows.Count
                ReDim Arr(IntNoOfFields)
                For Each ObjDr In mObjSearchViewsColumns.DataSet.Tables(0).Rows
                    If blArCulture Then
                        '// Prepare For Arabic Layout
                        If IntCounterSectionTwo > 3 Then SectionID = 2
                        If IntCounterSectionOne > 3 Then SectionID = 3
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
                    Else
                        If IntCounterSectionTwo > 3 Then SectionID = 2
                        If IntCounterSectionOne > 3 Then SectionID = 3
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

                    End If
                    If ObjDr.Item(StrLangLbl) Is DBNull.Value Then
                        literalName = ObjDr.Item("FieldName")
                    Else
                        literalName = ObjDr.Item(StrLangLbl)
                    End If
                    CurrentTextBox = " "
                    ASPLBL = New Global.System.Web.UI.WebControls.Label
                    ASPLBL.ID = "lbl" & ObjDr.Item("FieldName")
                    ASPLBL.Style.Item("POSITION") = " absolute"
                    ASPLBL.Style.Item("LEFT") = StrlblX_Pos
                    ASPLBL.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                    If blArCulture Then
                        ASPLBL.Style.Item("TEXT-ALIGN") = "right"
                        ASPLBL.Style.Item("DIRECTION") = "rtl"
                    End If
                    ASPLBL.Width = Global.System.Web.UI.WebControls.Unit.Pixel(100)
                    ASPLBL.Text = literalName
                    ASPLBL.Font.Name = StrFontName
                    ASPLBL.Font.Size = Web.UI.WebControls.FontSize.Large
                    pnlCriterias.Controls.Add(ASPLBL)
                    Select Case ObjDr.Item("FieldType")
                        Case 167, 239, 231
                            ASPTXT = New Web.UI.WebControls.TextBox
                            ASPTXT.ID = "WV_" & ObjDr.Item("FieldName")
                            CurrentTextBox = ObjDr.Item("FieldName")
                            ASPTXT.Style.Item("POSITION") = " absolute"
                            ASPTXT.Style.Item("LEFT") = StrX_Pos
                            ASPTXT.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPTXT.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPTXT.BackColor = Drawing.Color.White
                            ASPTXT.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPTXT.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPTXT.BorderColor = Drawing.Color.White
                            ASPTXT.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("InputLength"), SqlDbType.Int)
                            If blArCulture Then
                                ASPTXT.Style.Item("TEXT-ALIGN") = "right"
                                ASPTXT.Style.Item("DIRECTION") = "rtl"
                            End If
                            If mDataHandler.DataValue_Out(ObjDr.Item("IsArabic"), SqlDbType.Bit) = True Then
                                Venus.Shared.Web.ClientSideActions.SetLanguage(mPage, ASPTXT, [Shared].Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                            End If
                            ParameterNames.Text &= "|" & "WV_" & ObjDr.Item("FieldName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
                            Arr(ValueCounter) = "WV_" & ObjDr.Item("FieldName")
                            If ValueCounter = 0 Then
                                Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPTXT, False)
                            End If
                            'If ValueCounter = IntNoOfFields - 1 Then
                            '    ASPTXT.Attributes.Add("onBlur", "ctrlLastSearchCriteria('" & StrGridId & "');")
                            'End If
                            ObjSearchText = ASPTXT
                            pnlCriterias.Controls.Add(ASPTXT)
                        Case 56, 59, 60, 62 '"int", "money", "tinyint"
                            Dim ASPWN As New Web.UI.WebControls.TextBox
                            ASPWN.ID = "WN_" & ObjDr.Item("FieldName")
                            CurrentTextBox = "WN_" & ObjDr.Item("FieldName")
                            ASPWN.Style.Item("POSITION") = " absolute"
                            ASPWN.Style.Item("LEFT") = StrX_Pos
                            ASPWN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPWN.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPWN.BackColor = Drawing.Color.White
                            ASPWN.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWN.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWN.BorderColor = Drawing.Color.White
                            ASPWN.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("InputLength"), SqlDbType.Int)
                            ParameterNames.Text &= "|" & "WN_" & ObjDr.Item("FieldName")
                            Dim FieldName As String = String.Empty
                            If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then
                                Dim StrObject As String = fnGetTableName(ObjDr.Item("SubSearchID"))
                                FieldName = " Select ID from  " & StrObject & " WHERE CODE "
                                ParameterRealName.Text &= "|" & ObjDr.Item("FieldName") & "=(" & FieldName
                            Else
                                ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
                            End If
                            Venus.Shared.Web.ClientSideActions.SetupForNumberOnly(mPage, ASPWN, True)
                            Arr(ValueCounter) = "WN_" & ObjDr.Item("FieldName")
                            If ValueCounter = 0 Then
                                Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWN, False)
                            End If
                            'If ValueCounter = IntNoOfFields - 1 Then
                            '    ASPWN.Attributes.Add("onBlur", "ctrlLastSearchCriteria('" & StrGridId & "');")
                            'End If
                            ObjSearchText = ASPWN
                            pnlCriterias.Controls.Add(ASPWN)
                        Case 58 ' "smalldatetime"
                            ASPDATE = New Infragistics.WebUI.WebSchedule.WebDateChooser
                            ASPDATE.ID = "WD_" & ObjDr.Item("FieldName")
                            ASPDATE.Style.Item("POSITION") = " absolute"
                            ASPDATE.Style.Item("LEFT") = StrX_Pos
                            ASPDATE.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPDATE.CssClass = "DateBoxSearchCriteria"
                            ASPDATE.Width = Global.System.Web.UI.WebControls.Unit.Pixel(100)
                            ASPDATE.Format = Infragistics.WebUI.WebSchedule.DateFormat.Short
                            If blArCulture Then
                                ASPDATE.Style.Item("TEXT-ALIGN") = "right"
                                ASPDATE.Style.Item("DIRECTION") = "rtl"
                            End If
                            ASPDATE.BackColor = Drawing.Color.White
                            ASPDATE.CalendarLayout.DayHeaderStyle.BackColor = Drawing.Color.AliceBlue
                            ASPDATE.CalendarLayout.DayHeaderStyle.ForeColor = Drawing.Color.Black
                            ASPDATE.CalendarLayout.DayHeaderStyle.Font.Name = "Tahoma"
                            ASPDATE.CalendarLayout.DayHeaderStyle.Font.Size = Global.System.Web.UI.WebControls.FontSize.Large
                            ASPDATE.CalendarLayout.DayStyle.BackColor = Drawing.Color.White
                            ASPDATE.CalendarLayout.DayStyle.Font.Name = "Arial"
                            ASPDATE.CalendarLayout.DayStyle.Font.Size = Global.System.Web.UI.WebControls.FontSize.Large
                            ASPDATE.CalendarLayout.OtherMonthDayStyle.ForeColor = Drawing.Color.Chocolate
                            ASPDATE.CalendarLayout.SelectedDayStyle.BackColor = Drawing.Color.DarkKhaki
                            ASPDATE.CalendarLayout.ShowFooter = False
                            ASPDATE.CalendarLayout.ShowNextPrevMonth = False
                            ASPDATE.CalendarLayout.ShowTitle = False
                            ASPDATE.CalendarLayout.TitleStyle.BackColor = Drawing.Color.LightSteelBlue
                            ASPDATE.ExpandEffects.ShadowColor = Drawing.Color.LightGray
                            ASPDATE.ExpandEffects.Type = Infragistics.WebUI.WebDropDown.ExpandEffectType.Slide
                            ASPDATE.Height = Global.System.Web.UI.WebControls.Unit.Pixel(18)
                            ASPDATE.Width = Global.System.Web.UI.WebControls.Unit.Pixel(105)
                            ASPDATE.NullDateLabel = ""
                            ParameterNames.Text &= "|" & "WD_" & ObjDr.Item("FieldName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
                            Arr(ValueCounter) = "WD_" & ObjDr.Item("FieldName")

                            If ValueCounter = 0 Then
                                ASPDATE.Focus()
                            End If

                            'If ValueCounter = IntNoOfFields - 1 Then
                            '    ASPDATE.ClientSideEvents.OnBlur = "ctrlLastSearchCriteria('" & StrGridId & "');"
                            'End If
                            pnlCriterias.Controls.Add(ASPDATE)
                        Case 104 ' "bit"
                            Dim ASPWB As New Web.UI.WebControls.DropDownList
                            Dim ASPItem1 As New Web.UI.WebControls.ListItem
                            Dim ASPItem2 As New Web.UI.WebControls.ListItem
                            ASPItem1.Text = "True"
                            ASPItem1.Value = 0
                            ASPItem2.Text = "False"
                            ASPItem2.Value = 1
                            ASPWB.Items.Add(ASPItem1)
                            ASPWB.Items.Add(ASPItem2)
                            ASPWB.ID = "WB_" & ObjDr.Item("FieldName")
                            CurrentTextBox = "WB_" & ObjDr.Item("FieldName")
                            ASPWB.Style.Item("POSITION") = " absolute"
                            ASPWB.Style.Item("LEFT") = StrX_Pos ' CStr(X_Pos + 143) & "px"
                            ASPWB.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"
                            ASPWB.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPWB.BackColor = Drawing.Color.White

                            ASPWB.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWB.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWB.BorderColor = Drawing.Color.White

                            ParameterNames.Text &= "|" & "WB_" & ObjDr.Item("FieldName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
                            Arr(ValueCounter) = "WB_" & ObjDr.Item("FieldName")
                            If ValueCounter = 0 Then
                                Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWB, False)
                            End If
                            pnlCriterias.Controls.Add(ASPWB)

                    End Select

                    If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then
                        ASPBTN = New Infragistics.WebUI.WebDataInput.WebImageButton
                        ASPBTN.ID = "btn" & ObjDr.Item("FieldName").ToString.Replace(" ", "_")
                        ASPBTN.Style.Item("POSITION") = " absolute"
                        ASPBTN.Style.Item("LEFT") = StrbtnX_Pos
                        ASPBTN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                        ASPBTN.Style.Item("BACKGROUND-COLOR") = "BLUE"
                        ASPBTN.Width = Global.System.Web.UI.WebControls.Unit.Pixel(23)
                        ASPBTN.Height = Global.System.Web.UI.WebControls.Unit.Pixel(14)
                        Venus.Shared.Web.ClientSideActions.SetSearchButton(mPage, ASPBTN, ObjSearchText, ObjDr.Item("SubSearchID"), 615, 726)
                        pnlCriterias.Controls.Add(ASPBTN)
                    End If
                    ValueCounter += 1
                    If SectionID = 2 Then
                        IntCounterSectionOne += 1
                    ElseIf SectionID = 3 Then
                        IntCounterSectionThree += 1
                    Else
                        IntCounterSectionTwo += 1
                    End If

                Next
                If ParameterNames.Text <> "" Then
                    ParameterNames.Text = ParameterNames.Text.Substring(1)
                    ParameterRealName.Text = ParameterRealName.Text.Substring(1)
                End If
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function ViewData(ByVal PtrSearchID As Integer, ByVal Where As String, ByRef Ds As DataSet, ByVal StrLanguage As String, ByVal Cond As String, ByVal cncl As Boolean) As Boolean
        Dim ObjDr As DataRow
        Dim SqlSelectCommandBegin As String = String.Empty
        Dim SqlSelectCommandCriteria As String = String.Empty
        Dim SqlSelectCommandTable As String = String.Empty
        Dim SqlSelectCommandWhere As String = Where
        Dim SqlSelectCommand As String = String.Empty
        Dim StrAlis As String = String.Empty
        Dim StrCaption As String = IIf(StrLanguage <> "en-US", "ArbName", "EngName")
        Dim StrCompanyDependant As String = " And CompanyID=" & Me.MainCompanyID
        Dim StrCancelDependant As String = " And CancelDate is null"
        Dim ObjObjects As New Clssys_Objects(Me.mPage)
        Dim ObjFields As New Clssys_Fields(mPage)
        mObjSearchViewsColumns = New Clssys_SearchsColumns(mPage)
        mObjSearchViews = New Clssys_Searchs(mPage)
        Try
            mObjSearchViews.Find("ID=" & PtrSearchID)
            Dim SearchString As String = String.Empty
            If mObjSearchViewsColumns.Find("SearchID=" & PtrSearchID & " and ISVIEW =1 and (sys_SearchsColumns.RegComputerID is null or sys_SearchsColumns.RegComputerID in (2," & IIf(StrLanguage = "en-US", 0, 1) & ")) Order By RankView") Then   'ToDo Add Orderby View Rank 
                SqlSelectCommandBegin = "Select "
                For Each ObjDr In mObjSearchViewsColumns.DataSet.Tables(0).Rows
                    StrAlis = IIf(ObjDr.Item(StrCaption) Is DBNull.Value, ObjDr.Item("FieldName"), ObjDr.Item(StrCaption))
                    SqlSelectCommandCriteria &= "," & ObjDr.Item("FieldName") & " As [" & StrAlis & "]"
                Next
                ObjObjects.Find("ID=" & mObjSearchViews.ObjectID)
                If Not ObjFields.Find("ObjectID= " & ObjObjects.ID & " And FieldName='CompanyID'") Then
                    StrCompanyDependant = ""
                End If
                If Not ObjFields.Find("ObjectID= " & ObjObjects.ID & " And FieldName='CancelDate'") Then
                    StrCancelDependant = ""
                End If
                SqlSelectCommandTable = ObjObjects.Code
                Dim FilterBranches As String = String.Empty
                If SqlSelectCommandTable = "hrs_Employees" Then
                    FilterBranches = " and isnull(RegComputerID,0) = 0 And BranchID IN(Select BrancheID From sys_CompaniesBranches Where UserID=" & DataBaseUserRelatedID & " And CanView=1) "
                    Dim ClsUsers As New Clssys_Users(mPage)
                    ClsUsers.Find("ID=" & DataBaseUserRelatedID)
                    Dim strcommand As String = "select * from Sys_UsersViewDomain where UserCode = '" & ClsUsers.Code & "'"
                    Dim DT As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(MyBase.ConnectionString, CommandType.Text, strcommand).Tables(0)
                    If (DT.Rows.Count > 0) Then
                        If Convert.ToString(DT.Rows(0)("Departments")) <> "" Then
                            FilterBranches = FilterBranches & " And DepartmentID in (" & Convert.ToString(DT.Rows(0)("Departments")) & ")"
                        End If
                        If Convert.ToString(DT.Rows(0)("Cost1")) <> "" Then
                            FilterBranches = FilterBranches & " And Cost1 in (" & Convert.ToString(DT.Rows(0)("Cost1")) & ")"
                        End If
                        If Convert.ToString(DT.Rows(0)("Cost2")) <> "" Then
                            FilterBranches = FilterBranches & " And Cost2 in (" & Convert.ToString(DT.Rows(0)("Cost2")) & ")"
                        End If
                        If Convert.ToString(DT.Rows(0)("Cost3")) <> "" Then
                            FilterBranches = FilterBranches & " And Cost3 in (" & Convert.ToString(DT.Rows(0)("Cost3")) & ")"
                        End If
                        If Convert.ToString(DT.Rows(0)("Cost4")) <> "" Then
                            FilterBranches = FilterBranches & " And Cost4 in (" & Convert.ToString(DT.Rows(0)("Cost4")) & ")"
                        End If
                    End If
                ElseIf SqlSelectCommandTable = "sys_Branches" Then
                    FilterBranches = " And ID IN(Select BrancheID From sys_CompaniesBranches Where UserID=" & DataBaseUserRelatedID & " And CanView=1) "
                End If
                SqlSelectCommand = SqlSelectCommandBegin & SqlSelectCommandCriteria.Substring(1) & " From  " & SqlSelectCommandTable & IIf(Len(SqlSelectCommandWhere) > 1, " Where (" & SqlSelectCommandWhere & ") " & FilterBranches & StrCompanyDependant & IIf(cncl = True, "", StrCancelDependant) & Cond, " Where 1=1 " & FilterBranches & StrCompanyDependant & IIf(cncl = True, "", StrCancelDependant) & Cond)
                Try
                    mSqlDataAdapter = New SqlClient.SqlDataAdapter(SqlSelectCommand & " Order By Case When IsNumeric(Code) = 1 then Right(Replicate('0',51) + Code, 50) When IsNumeric(Code) = 0 then Left(Code + Replicate('',51), 50) Else Code End ASC", mConnectionString)
                    mSqlDataAdapter.Fill(Ds)
                Catch ex As Exception
                    mSqlDataAdapter = New SqlClient.SqlDataAdapter(SqlSelectCommand & " Order By ID ASC", mConnectionString)
                    mSqlDataAdapter.Fill(Ds)
                End Try
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function fnGetTableName(ByVal ObjectID As String) As String
        Dim tableName As String = String.Empty
        mSqlConnection = New SqlClient.SqlConnection(MyBase.mConnectionString)
        mSqlCommand = New SqlClient.SqlCommand
        mSqlCommand.CommandType = CommandType.Text
        mSqlCommand.CommandText = " select sys_objects.code from sys_objects inner join sys_searchs on sys_objects.id= sys_searchs.objectid where sys_searchs.id = " & ObjectID
        mSqlCommand.Connection = mSqlConnection
        If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
        mSqlConnection.Open()
        Try
            tableName = mSqlCommand.ExecuteScalar
        Catch ex As Exception
            Return False
        End Try
        Return tableName
    End Function
#End Region

End Class
