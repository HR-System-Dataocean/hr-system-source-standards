'======================================================================
'Program Name : ClsSys_MainOtherFields
'Application  : Venus - Human Resource Module 
'Developer    : [0256] Abdul Jaleel R. Ossman 
'Date Created : 08-08-2007 
'Description  : This Class Represents the BO Layer for creatting and Viewing other Fields for a given Screen (Object)
'Modifications: 
'             : B#00001 [0256] 08-08-2007 Changing js Code that opens search screen by sending _Parent+ModuleID 
'             :                          Instead of _Parent which made any child search screen opened in same window 
'             : E#00002 [0256] 13-08-2007 Change ASPcontrol for DateTime Fields t be dateChooser instead of Textbox
'             : E#00001 [0256] 08-08-2007 Change the way other fields button be displayed in master screens 
'             :                          From being displayed dynamicaaly to be fixed in form ToolBar 
'             : B#00002 [0256] 08-08-2007 Charecters Fields created without peffix WV_ 
'             : B#00003 [0256] 13-08-2007 Newly Created Or Added Fields Didn't be Displayed in Other Fields Viewer 
'             : E#00002 [0256] 17-04-2008 Change the ID's of 
'======================================================================
Imports Infragistics.WebUI.WebSchedule.WebDateChooser
Public Class clsSys_MainOtherFields
    Inherits ClsDataAcessLayer

#Region "Constants"

    Private mErrorHandler As New Venus.Shared.ErrorsHandler
#End Region

#Region "Class Constructors"
    Public Sub New(ByVal page As Web.UI.Page)
        MyBase.New(page)
    End Sub
#End Region

#Region "Private Members"
    Private mObjOtherFields As ClsSys_OtherFields
#End Region

#Region "Public Property"
#End Region

#Region "public Methods"
    '==================================================================================
    'ProcedureName  : CheckOtherFieldsButton()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : This function checks if the screen being opened its main table has 
    '               : Other fields or not and creates a button on the screen leads user to other fields screen   
    'Developer      : Data Ocean
    '               : [AGR] Abdul Jaleel R. Ossman
    'Date Created   : DataOcean 
    'Date Modified  : 08-08-2007
    'Modifacations  : 
    '               : Modification related to same modification in ClsSys_searchs AND ClsSys_SearchsColumns Class 
    '               :
    'Calling       *:*
    'Function Calls : 
    'Called From    : 
    '==================================================================================
    Public Function CheckOtherFieldsButton(ByVal MyPage As Web.UI.Page, _
                                            ByVal TableName As String) As Boolean

        mObjOtherFields = New ClsSys_OtherFields(MyPage)
        Dim clsSysObjects As New Clssys_Objects(MyPage)

        clsSysObjects.Find(" Code = REPLACE('" & TableName & "',' ' ,'')")
        With mObjOtherFields
            If .Find(" sys_OtherFields.ObjectID = " & clsSysObjects.ID) = True Then
                Dim OtherFieldID As Integer = .ID
                Return True
            End If
        End With
    End Function
    '==========================================================================
    'Procedure Name : fnGetRecordID 
    'Developer      : [0256]
    'Date Created   : 08-07-2007
    'Description    : Returns Identity Fields FROM a given Table 
    '==========================================================================
    Public Function fnGetRecordID(ByVal tableName As String) As Integer
        Dim Identity As Integer = 0
        Try
            mSqlConnection = New SqlClient.SqlConnection
            mSqlConnection.ConnectionString = MyBase.mConnectionString
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = mSqlConnection
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = "Select Top 1 ID  From  " & tableName & " ORDER BY ID Desc   "
            If mSqlConnection.State = ConnectionState.Open Then mSqlConnection.Close()
            mSqlConnection.Open()
            Identity = mSqlCommand.ExecuteScalar()
        Catch ex As Exception
            Identity = -1
        End Try
        mSqlConnection.Close()
        Return Identity
    End Function
    '==========================================================================
    'Procedure Name : fnGetTableName 
    'Developer      : [0256]
    'Date Created   : 08-07-2007
    'Description    : Returns Object name using its ID 
    '==========================================================================
    Private Function fnGetTableName(ByVal ObjectID As String) As String
        Dim tableName As String = String.Empty
        mSqlConnection = New SqlClient.SqlConnection(MyBase.mConnectionString)
        mSqlCommand = New SqlClient.SqlCommand
        mSqlCommand.CommandType = CommandType.Text
        mSqlCommand.CommandText = " Select sys_objects.code From sys_objects Inner join sys_searchs on sys_objects.id= sys_searchs.objectid where sys_searchs.id = " & ObjectID
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
    '==================================================================================
    'ProcedureName  : ReadNwriteOtherFieldsValues
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : Based on the the open status 'Mode',
    '               : This Function Read the Other Fields paramters and according to those Parameters 
    '               : Data Type it Handels The Following 
    '               : 1- Read Cretiria Fields from sys_searchsColumns and creat dynamically 
    '               :    Equavillent controls 
    '               : 2- Store Parameter names in an array - that array will be sent to JS file to iterate through created 
    '               :    Controls and dealing with user data entered in them 
    '               : 3- SetFoucs For first created control 
    '               : 4- SetupTabOrder 
    '               : 
    'Developer      : [AGR] Abdul Jaleel R. Ossman
    'Date Created   : DataOcean 
    'Date Modified  : 08-08-2007 
    'Modifacations  : 

    'Calling       *:*
    'Function Calls : ClentSideAction Methods  
    'Called From    : frmOtherFieldsDynamic.Page_Load() Event
    '==================================================================================
    Public Function ReadNwriteOtherFieldsValuesOld(ByVal ObjectID As Integer, _
   ByRef Div1 As Web.UI.HtmlControls.HtmlGenericControl, _
   ByVal ParameterNames As Web.UI.WebControls.Label, ByVal ParameterRealName As Web.UI.WebControls.Label, Optional ByRef objDS As Data.DataSet = Nothing, Optional ByVal OpenFor As String = "A") As Boolean

        mObjOtherFields = New ClsSys_OtherFields(mPage)
        Dim ObjDr As DataRow
        Dim ValueCounter As Integer
        Dim str As String
        Dim litral As New Web.UI.LiteralControl
        Dim Y_Pos As Integer = 80
        Dim X_Pos As Integer = 40
        Dim Width As Integer = 240
        Dim VwWidth As Integer = 80
        Dim CurrentTextBox As String
        ParameterNames.Text = ""
        ParameterRealName.Text = ""
        Dim Arr() As String
        Dim subSearchID As Integer = 0
        Try
            If mObjOtherFields.Find("sys_OtherFields.ObjectID= " & ObjectID & " And sys_OtherFields.CancelDate is Null ") Then

                '
                ReDim Arr(mObjOtherFields.DataSet.Tables(0).Rows.Count)
                For Each ObjDr In mObjOtherFields.DataSet.Tables(0).Rows
                    CurrentTextBox = " "
                    Select Case ObjDr.Item("DataType")

                        Case 1
                            Dim ASPTXT As New Web.UI.WebControls.TextBox
                            ASPTXT.ID = "WV_" & ObjDr.Item("EngName")

                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then
                                    If objDS.Tables(0).Rows(ValueCounter).Item(3) Is Nothing Then
                                        ASPTXT.Text = ""
                                    Else
                                        ASPTXT.Text = objDS.Tables(0).Rows(ValueCounter).Item(3)
                                    End If

                                End If
                            Catch ex As Exception
                            End Try
                            'If OpenFor = "U" Then ASPTXT.Text = objDS.Tables(0).Rows(ValueCounter).Item(3)
                            'B#0001 Object Created without prefix result in Object Not Found in Search Screen  
                            'CurrentTextBox = ObjDr.Item("EngName")
                            CurrentTextBox = "WV_" & ObjDr.Item("EngName")
                            ASPTXT.Style.Item("POSITION") = " absolute"
                            ASPTXT.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                            ASPTXT.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPTXT.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            Else
                                ASPTXT.Style.Item("WIDTH") = CStr(Width) & "px"
                            End If

                            ASPTXT.BackColor = Drawing.Color.Silver

                            ASPTXT.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPTXT.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPTXT.BorderColor = Drawing.Color.White
                            ASPTXT.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("DataLength"), SqlDbType.Int)
                            ParameterNames.Text &= "|" & "WV_" & ObjDr.Item("EngName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")
                            'Mezo
                            Arr(ValueCounter) = "WV_" & ObjDr.Item("EngName")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPTXT, False)
                            End If
                            Div1.Controls.Add(ASPTXT)

                        Case 2 '"int", "money", "tinyint"

                            Dim ASPWN As New Web.UI.WebControls.TextBox
                            ASPWN.ID = "WN_" & ObjDr.Item("EngName")

                            Try
                                If OpenFor = "U" Then
                                    If objDS.Tables(0).Rows(ValueCounter).Item(3) Is Nothing Then
                                        ASPWN.Text = ""
                                    Else
                                        ASPWN.Text = objDS.Tables(0).Rows(ValueCounter).Item(3)
                                    End If

                                End If
                            Catch ex As Exception
                            End Try

                            CurrentTextBox = "WN_" & ObjDr.Item("EngName")

                            ASPWN.Style.Item("POSITION") = " absolute"
                            ASPWN.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                            ASPWN.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPWN.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            Else
                                ASPWN.Style.Item("WIDTH") = CStr(Width) & "px"
                            End If
                            ASPWN.BackColor = Drawing.Color.Silver

                            ASPWN.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWN.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWN.BorderColor = Drawing.Color.White
                            ASPWN.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("DataLength"), SqlDbType.Int)

                            ParameterNames.Text &= "|" & "WN_" & ObjDr.Item("EngName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            'Dim FieldName As String = String.Empty
                            'If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then

                            '    Dim StrObject As String = fnGetTableName(ObjDr.Item("ViewObjectID"))
                            '    FieldName = " Select ID from  " & StrObject & " WHERE CODE "
                            '    ParameterRealName.Text &= "|" & ObjDr.Item("EngName") & "=(" & FieldName
                            'Else
                            '    ParameterRealName.Text &= "|" & ObjDr.Item("ID")
                            'End If
                            Venus.Shared.Web.ClientSideActions.SetupForNumberOnly(mPage, ASPWN, True)
                            Arr(ValueCounter) = "WN_" & ObjDr.Item("EngName")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWN, False)
                            End If
                            Div1.Controls.Add(ASPWN)

                        Case 4 ' "smalldatetime"
                            'Dim ASPWC As New Web.UI.WebControls.TextBox
                            Dim ASPWC_Infra As New Infragistics.WebUI.WebSchedule.WebDateChooser
                            'DIM ASPWC_Infra as

                            ' Dim s As New Infragistics.WebUI.WebSchedule.WebDateChooser
                            'ASPWC.ID = "WD_" & ObjDr.Item("EngName")
                            ASPWC_Infra.ID = "WD_" & ObjDr.Item("EngName")
                            'CurrentTextBox = "WD_" & ObjDr.Item("EngName")
                            CurrentTextBox = "WD_" & ObjDr.Item("EngName")
                            ASPWC_Infra.Style.Item("POSITION") = " absolute"
                            ASPWC_Infra.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                            ASPWC_Infra.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                            'ASPWC.Style.Item("POSITION") = " absolute"
                            'ASPWC.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                            'ASPWC.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                            'If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                            '    ASPWC.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            'Else
                            '    ASPWC.Style.Item("WIDTH") = CStr(Width) & "px"
                            'End If

                            'If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                            'ASPWC.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            '  Else
                            ASPWC_Infra.Style.Item("WIDTH") = CStr(Width) & "px"
                            ' End If

                            'ASPWC.BackColor = Drawing.Color.Silver
                            'ASPWC.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            'ASPWC.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            'ASPWC.BorderColor = Drawing.Color.White

                            ASPWC_Infra.BackColor = Drawing.Color.Silver
                            ASPWC_Infra.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWC_Infra.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWC_Infra.BorderColor = Drawing.Color.White

                            ParameterNames.Text &= "|" & "WD_" & ObjDr.Item("EngName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            'Mezo
                            'Venus.Shared.Web.ClientSideActions.MaskedEdit(mPage, ASPWC, "##/##/####")
                            Arr(ValueCounter) = "WD_" & ObjDr.Item("EngName")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWC_Infra, False)
                            End If
                            If OpenFor = "U" Then ASPWC_Infra.Value = objDS.Tables(0).Rows(ValueCounter).Item(3)
                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then
                                    If objDS.Tables(0).Rows(ValueCounter).Item(3) Is Nothing Then
                                        ASPWC_Infra.Value = Nothing
                                    Else
                                        ASPWC_Infra.Value = objDS.Tables(0).Rows(ValueCounter).Item(3)
                                    End If

                                End If
                            Catch ex As Exception
                            End Try
                            Div1.Controls.Add(ASPWC_Infra)
                        Case 3 ' "bit"

                            Dim ASPWB As New Web.UI.WebControls.DropDownList
                            Dim ASPItem1 As New Web.UI.WebControls.ListItem
                            Dim ASPItem2 As New Web.UI.WebControls.ListItem
                            ASPItem1.Text = "True"
                            ASPItem1.Value = 0
                            ASPItem2.Text = "False"
                            ASPItem2.Value = 1
                            ASPWB.Items.Add(ASPItem1)
                            ASPWB.Items.Add(ASPItem2)
                            ASPWB.ID = "WB_" & ObjDr.Item("EngName")
                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then
                                    If CInt(objDS.Tables(0).Rows(ValueCounter).Item(3)) = 1 Then
                                        ASPWB.SelectedIndex = 1
                                    Else
                                        ASPWB.SelectedIndex = 0
                                    End If
                                End If
                            Catch ex As Exception

                            End Try

                            CurrentTextBox = "WB_" & ObjDr.Item("EngName")
                            'CurrentTextBox = "WB_" & ObjDr.Item("EngName")
                            ASPWB.Style.Item("POSITION") = " absolute"
                            ASPWB.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                            ASPWB.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"

                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPWB.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            Else
                                ASPWB.Style.Item("WIDTH") = CStr(Width) & "px"
                            End If

                            ASPWB.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPWB.BackColor = Drawing.Color.Silver
                            ASPWB.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWB.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWB.BorderColor = Drawing.Color.White
                            ParameterNames.Text &= "|" & "WB_" & ObjDr.Item("EngName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            'Mezo
                            Arr(ValueCounter) = "WB_" & ObjDr.Item("EngName")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWB, False)
                            End If
                            Div1.Controls.Add(ASPWB)

                    End Select


                    If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then
                        Dim ViewObjectID As String = ObjDr.Item("SubSearchID")
                        str = "<INPUT style='Z-INDEX: 103; LEFT: " & CStr(X_Pos + 230) & "px; POSITION: absolute; TOP: " & CStr(ValueCounter * 25 + Y_Pos) & "px; HEIGHT: 20px' type='button' value='...' id='" & "btn" & ObjDr.Item("EngName") & "' onclick=" & """" & "MainSearch_OpenWin('" & CurrentTextBox & "','" & ViewObjectID & "');" & """" & ">"
                        litral = New Web.UI.LiteralControl(str)
                        Div1.Controls.Add(litral)
                    End If


                    If Not mPage.IsPostBack Then
                        Dim DV1Hight As Integer
                        Dim DV1HightStr As String

                        DV1HightStr = Div1.Style.Item("HEIGHT")
                        DV1Hight = CInt(DV1HightStr.Substring(0, DV1HightStr.Length - 2))

                        Div1.Style.Item("HEIGHT") = DV1Hight + 20 & "px"

                    End If

                    str = "<DIV style='DISPLAY: inline; font-size: small; Z-INDEX: 102; LEFT: " & CStr(X_Pos) & "px; WIDTH: 123px;border-right: white 1px outset; border-top: white 1px outset; border-left: white 1px outset; border-bottom: white 1px outset; POSITION: absolute; TOP: " & CStr(ValueCounter * 25 + Y_Pos) & "px; HEIGHT: 20px' ms_positioning='FlowLayout'>" & ObjDr.Item("EngName") & "</DIV>"
                    litral = New Web.UI.LiteralControl(str)
                    Div1.Controls.Add(litral)

                    ValueCounter += 1
                Next

                If ParameterNames.Text <> "" Then
                    ParameterNames.Text = ParameterNames.Text.Substring(1)
                    ParameterRealName.Text = ParameterRealName.Text.Substring(1)
                End If
                Venus.Shared.Web.ClientSideActions.SetupTabOrder(mPage, Arr, Drawing.Color.White, False)
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function ReadNwriteOtherFieldsValues(ByVal ObjectID As Integer, _
   ByRef pnlFields As Web.UI.WebControls.Panel, _
   ByVal ParameterNames As Web.UI.WebControls.Label, ByVal ParameterRealName As Web.UI.WebControls.Label, _
   ByVal StrLanguage As String, ByVal StrGroupKey As String, Optional ByRef objDS As Data.DataSet = Nothing, Optional ByVal OpenFor As String = "A") As Boolean

        mObjOtherFields = New ClsSys_OtherFields(mPage)
        Dim ObjDr As DataRow
        Dim ValueCounter As Integer
        Dim str As String
        Dim litral As New Web.UI.LiteralControl
        Dim Y_Pos As Integer = 80
        Dim X_Pos As Integer = 40
        Dim Width As Integer = 240
        Dim VwWidth As Integer = 80
        Dim CurrentTextBox As String
        ParameterNames.Text = ""
        ParameterRealName.Text = ""
        Dim Arr() As String
        Dim subSearchID As Integer = 0

        ' [0256] ===== begin
        Dim IntNoOfFields As Integer

        Dim blArCulture As Boolean = IIf(StrLanguage = "ar-EG", True, False)
        Dim literalName As String = String.Empty
        Dim StrFontName As String = String.Empty
        Dim StrLangLbl As String = String.Empty


        Dim IntY_Pos As Integer = 10
        Dim StrX_Pos As String = "26%"
        Dim StrO_Pos As String = "23%"
        Dim StrlblX_Pos As String = "20px"
        Dim StrbtnX_Pos As String = "44%"
        Dim StrLblDesc_Pos As String

        Dim IntCounterSectionOne As Integer = 0
        Dim IntCounterSectionTwo As Integer = 0
        Dim IntCounterSectionThree As Integer = 0
        Dim IntCounter As Integer = 0
        Dim SectionID As Integer = 1

        Dim ASPLBL As Global.System.Web.UI.WebControls.Label
        Dim ASPTXT As Global.System.Web.UI.WebControls.TextBox
        Dim ASPDATE As Infragistics.WebUI.WebSchedule.WebDateChooser

        ' [0256] ===== end
        Try
            If mObjOtherFields.Find("sys_OtherFields.ObjectID= " & ObjectID & " And sys_OtherFields.CancelDate is Null ") Then

                '[0256] Begin Here 
                If (blArCulture) Then

                    blArCulture = True
                    StrLangLbl = "ArbName"
                    StrFontName = "Tahoma"
                Else
                    StrLangLbl = "EngName"
                    StrFontName = "Arail"
                End If

                '[0256] End Here
                IntNoOfFields = mObjOtherFields.DataSet.Tables(0).Rows.Count
                ReDim Arr(IntNoOfFields)
                For Each ObjDr In mObjOtherFields.DataSet.Tables(0).Rows
                    CurrentTextBox = " "
                    '[0256] ================ Begin
                    If blArCulture Then
                        '// Prepare For Arabic Layout
                        If IntCounterSectionTwo > 5 Then SectionID = 2
                        If IntCounterSectionOne > 5 Then SectionID = 3
                        If SectionID = 2 Then
                            StrX_Pos = "28%"
                            'StrO_Pos = "52%"
                            StrlblX_Pos = "41%"
                            StrbtnX_Pos = "23%"
                            StrLblDesc_Pos = "15%"
                            IntCounter = IntCounterSectionOne
                        ElseIf SectionID = 3 Then
                            StrX_Pos = "10%"
                            'StrO_Pos = "21%"
                            StrlblX_Pos = "18%"
                            StrbtnX_Pos = "5%"
                            StrLblDesc_Pos = "1%"
                            IntCounter = IntCounterSectionThree
                        Else
                            StrX_Pos = "60%"
                            'StrO_Pos = "86%"
                            StrlblX_Pos = "80%"
                            StrbtnX_Pos = "55%"
                            StrLblDesc_Pos = "45%"
                            IntCounter = IntCounterSectionTwo
                        End If
                    Else
                        If IntCounterSectionTwo > 5 Then SectionID = 2
                        If IntCounterSectionOne > 5 Then SectionID = 3
                        If SectionID = 2 Then
                            StrX_Pos = "58%"
                            'StrO_Pos = "50%"
                            StrlblX_Pos = "44%"
                            StrbtnX_Pos = "70%"
                            StrLblDesc_Pos = "73%"
                            IntCounter = IntCounterSectionOne
                        ElseIf SectionID = 3 Then
                            StrX_Pos = "82%"
                            'StrO_Pos = "79%"
                            StrlblX_Pos = "72%"
                            StrbtnX_Pos = "88%"
                            StrLblDesc_Pos = "93%"
                            IntCounter = IntCounterSectionThree
                        Else
                            StrX_Pos = "18%"
                            'StrO_Pos = "15%"
                            StrlblX_Pos = "10px"
                            StrbtnX_Pos = "35%"
                            StrLblDesc_Pos = "39%"
                            IntCounter = IntCounterSectionTwo
                        End If

                    End If
                    If ObjDr.Item(StrLangLbl) Is DBNull.Value Then
                        literalName = ObjDr.Item("EngName")
                    Else
                        literalName = ObjDr.Item(StrLangLbl)
                    End If

                    CurrentTextBox = " "
                    ASPLBL = New Global.System.Web.UI.WebControls.Label
                    ASPLBL.ID = "lbl" & ObjDr.Item("EngName").ToString.Replace(" ", "")
                    ASPLBL.Style.Item("POSITION") = "absolute"
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
                    pnlFields.Controls.Add(ASPLBL)
                    '=========== [0256] End
                    Select Case ObjDr.Item("DataType")

                        Case 1
                            ASPTXT = New Web.UI.WebControls.TextBox
                            ASPTXT.ID = "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")

                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then
                                    If objDS.Tables(0).Rows(ValueCounter).Item(3) Is Nothing Then
                                        ASPTXT.Text = ""
                                    Else
                                        ASPTXT.Text = objDS.Tables(0).Rows(ValueCounter).Item(3)
                                    End If
                                End If
                            Catch ex As Exception

                            End Try

                            'If OpenFor = "U" Then ASPTXT.Text = objDS.Tables(0).Rows(ValueCounter).Item(3)
                            'B#0001 Object Created without prefix result in Object Not Found in Search Screen  
                            'CurrentTextBox = ObjDr.Item("EngName")

                            CurrentTextBox = "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ASPTXT.Style.Item("POSITION") = " absolute"
                            ASPTXT.Style.Item("LEFT") = StrX_Pos 'CStr(X_Pos + 143) & "px"
                            ASPTXT.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"

                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPTXT.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            Else
                                ASPTXT.Style.Item("WIDTH") = CStr(104) & "px"
                            End If

                            If blArCulture Then
                                ASPTXT.Style.Item("Text-Align") = "Right"
                                ASPTXT.Style.Item("Direction") = "RTL"
                            End If

                            ASPTXT.BackColor = Drawing.Color.Silver
                            ASPTXT.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPTXT.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPTXT.BorderColor = Drawing.Color.White
                            ASPTXT.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("DataLength"), SqlDbType.Int)
                            ParameterNames.Text &= "|" & "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")
                            'Mezo

                            Arr(ValueCounter) = "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPTXT, False)
                            End If
                            pnlFields.Controls.Add(ASPTXT)

                        Case 2 '"int", "money", "tinyint"

                            Dim ASPWN As New Web.UI.WebControls.TextBox
                            ASPWN.ID = "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")

                            Try
                                If OpenFor = "U" Then
                                    If objDS.Tables(0).Rows(ValueCounter).Item(3) Is Nothing Then
                                        ASPWN.Text = ""
                                    Else
                                        ASPWN.Text = objDS.Tables(0).Rows(ValueCounter).Item(3)
                                    End If

                                End If
                            Catch ex As Exception
                            End Try

                            CurrentTextBox = "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")

                            ASPWN.Style.Item("POSITION") = " absolute"
                            ASPWN.Style.Item("LEFT") = StrX_Pos 'CStr(X_Pos + 143) & "px"
                            ASPWN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"
                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPWN.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            Else
                                ASPWN.Style.Item("WIDTH") = CStr(104) & "px"
                            End If

                            If blArCulture Then
                                ASPWN.Style.Item("Text-Align") = "Right"
                                ASPWN.Style.Item("Direction") = "RTL"
                            End If

                            ASPWN.BackColor = Drawing.Color.Silver

                            ASPWN.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWN.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWN.BorderColor = Drawing.Color.White
                            ASPWN.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("DataLength"), SqlDbType.Int)

                            ParameterNames.Text &= "|" & "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            'Dim FieldName As String = String.Empty
                            'If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then

                            '    Dim StrObject As String = fnGetTableName(ObjDr.Item("ViewObjectID"))
                            '    FieldName = " Select ID from  " & StrObject & " WHERE CODE "
                            '    ParameterRealName.Text &= "|" & ObjDr.Item("EngName") & "=(" & FieldName
                            'Else
                            '    ParameterRealName.Text &= "|" & ObjDr.Item("ID")
                            'End If
                            Venus.Shared.Web.ClientSideActions.SetupForNumberOnly(mPage, ASPWN, True)
                            Arr(ValueCounter) = "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWN, False)
                            End If
                            pnlFields.Controls.Add(ASPWN)

                        Case 4 ' "smalldatetime"

                            ASPDATE = New Infragistics.WebUI.WebSchedule.WebDateChooser
                            ASPDATE.ID = "WD_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
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
                            ASPDATE.BackColor = Drawing.Color.Silver

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
                            ASPDATE.Width = Global.System.Web.UI.WebControls.Unit.Pixel(110)
                            ASPDATE.NullDateLabel = ""

                            ParameterNames.Text &= "|" & "WD_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            Arr(ValueCounter) = "WD_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                ASPDATE.Focus()
                            End If

                            If OpenFor = "U" Then ASPDATE.Value = objDS.Tables(0).Rows(ValueCounter).Item(3)
                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then
                                    If objDS.Tables(0).Rows(ValueCounter).Item(3) Is Nothing Then
                                        ASPDATE.Value = Nothing
                                    Else
                                        ASPDATE.Value = objDS.Tables(0).Rows(ValueCounter).Item(3)
                                    End If
                                End If
                            Catch ex As Exception

                            End Try
                            pnlFields.Controls.Add(ASPDATE)

                        Case 3 ' "bit"

                            Dim ASPWB As New Web.UI.WebControls.DropDownList
                            Dim ASPItem1 As New Web.UI.WebControls.ListItem
                            Dim ASPItem2 As New Web.UI.WebControls.ListItem


                            If blArCulture Then
                                ASPWB.Style.Item("TEXT-ALIGN") = "right"
                                ASPWB.Style.Item("DIRECTION") = "rtl"
                                ASPItem1.Text = "äÚã"
                                ASPItem2.Text = "áÇ"
                            Else
                                ASPItem1.Text = "Yes"
                                ASPItem2.Text = "No"
                            End If


                            ASPItem1.Value = 0

                            ASPItem2.Value = 1
                            ASPWB.Items.Add(ASPItem1)
                            ASPWB.Items.Add(ASPItem2)
                            ASPWB.ID = "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then
                                    If CInt(objDS.Tables(0).Rows(ValueCounter).Item(3)) = 1 Then
                                        ASPWB.SelectedIndex = 1
                                    Else
                                        ASPWB.SelectedIndex = 0
                                    End If
                                End If
                            Catch ex As Exception

                            End Try

                            CurrentTextBox = "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'CurrentTextBox = "WB_" & ObjDr.Item("EngName")
                            ASPWB.Style.Item("POSITION") = " absolute"
                            ASPWB.Style.Item("LEFT") = StrX_Pos 'CStr(X_Pos + 143) & "px"
                            ASPWB.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"

                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPWB.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            Else
                                ASPWB.Style.Item("WIDTH") = CStr(110) & "px"
                            End If

                            ASPWB.Style.Item("WIDTH") = CStr(110) & "px"
                            ASPWB.BackColor = Drawing.Color.Silver
                            ASPWB.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWB.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWB.BorderColor = Drawing.Color.White
                            ParameterNames.Text &= "|" & "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            'Mezo
                            Arr(ValueCounter) = "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWB, False)
                            End If
                            pnlFields.Controls.Add(ASPWB)
                    End Select


                    If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then
                        Dim ViewObjectID As String = ObjDr.Item("SubSearchID")
                        str = "<INPUT style='Z-INDEX: 103; LEFT: " & StrbtnX_Pos & "px; POSITION: absolute; TOP: " & CStr(IntY_Pos + (32 * IntCounter)) & "px; HEIGHT: 20px' type='button' value='...' id='" & "btn" & ObjDr.Item("EngName") & "' onclick=" & """" & "MainSearch_OpenWin('" & CurrentTextBox & "','" & ViewObjectID & "');" & """" & ">"
                        litral = New Web.UI.LiteralControl(str)
                        pnlFields.Controls.Add(litral)
                    End If


                    'Dim FieldName As String = String.Empty
                    'If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then

                    '    Dim StrObject As String = fnGetTableName(ObjDr.Item("SubSearchID"))
                    '    FieldName = " Select ID from  " & StrObject & " WHERE CODE "
                    '    ParameterRealName.Text &= "|" & ObjDr.Item("FieldName") & "=(" & FieldName
                    'Else
                    '    ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
                    'End If



                    'If Not mPage.IsPostBack Then
                    '    Dim DV1Hight As Integer
                    '    Dim DV1HightStr As String

                    '    DV1HightStr = pnlFields.Style.Item("HEIGHT")
                    '    DV1Hight = CInt(DV1HightStr.Substring(0, DV1HightStr.Length - 2))

                    '    pnlFields.Style.Item("HEIGHT") = DV1Hight + 20 & "px"

                    'End If

                    'str = "<DIV style='DISPLAY: inline; font-size: small; Z-INDEX: 102; LEFT: " & CStr(X_Pos) & "px; WIDTH: 123px;border-right: white 1px outset; border-top: white 1px outset; border-left: white 1px outset; border-bottom: white 1px outset; POSITION: absolute; TOP: " & CStr(ValueCounter * 25 + Y_Pos) & "px; HEIGHT: 20px' ms_positioning='FlowLayout'>" & ObjDr.Item("EngName") & "</DIV>"
                    'litral = New Web.UI.LiteralControl(str)
                    'pnlFields.Controls.Add(litral)

                    If SectionID = 2 Then
                        IntCounterSectionOne += 1
                    ElseIf SectionID = 3 Then
                        IntCounterSectionThree += 1
                    Else
                        IntCounterSectionTwo += 1
                    End If


                    ValueCounter += 1
                Next

                If ParameterNames.Text <> "" Then
                    ParameterNames.Text = ParameterNames.Text.Substring(1)
                    ParameterRealName.Text = ParameterRealName.Text.Substring(1)
                End If
                Venus.Shared.Web.ClientSideActions.SetupTabOrder(mPage, Arr, Drawing.Color.White, False)
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function ReadNwriteOtherFieldsValues_NewStyle(ByVal ObjectID As Integer, ByVal IntGroupId As Integer, _
 ByRef pnlFields As Infragistics.WebUI.UltraWebTab.Tab, _
 ByRef ParameterNames As Web.UI.WebControls.Label, ByRef ParameterRealName As Web.UI.WebControls.Label, ByVal StrLanguage As String, _
    ByVal StrGroupKey As String, ByVal StrFormName As String, Optional ByRef objDS As Data.DataSet = Nothing, Optional ByVal OpenFor As String = "A") As Boolean

        mObjOtherFields = New ClsSys_OtherFields(mPage)
        Dim clsSearchs As New Clssys_Searchs(mPage)
        Dim clsObjects As New Clssys_Objects(mPage)



        Dim ObjDr As DataRow
        Dim ValueCounter As Integer
        Dim str As String
        Dim litral As New Web.UI.LiteralControl
        Dim Y_Pos As Integer = 80
        Dim X_Pos As Integer = 40
        Dim Width As Integer = 240
        Dim VwWidth As Integer = 80
        Dim CurrentTextBox As String
        'ParameterNames.Text = ""
        'ParameterRealName.Text = ""
        Dim Arr() As String
        Dim subSearchID As Integer = 0

        ' [0256] ===== begin
        Dim IntNoOfFields As Integer

        Dim blArCulture As Boolean = IIf(StrLanguage = "ar-EG", True, False)
        Dim literalName As String = String.Empty
        Dim StrFontName As String = String.Empty
        Dim StrLangLbl As String = String.Empty


        Dim IntY_Pos As Integer = 40
        Dim StrX_Pos As String = "26%"
        Dim StrO_Pos As String = "23%"
        Dim StrlblX_Pos As String = "20px"
        Dim StrbtnX_Pos As String = "44%"
        Dim StrLblDesc_Pos As String

        Dim IntCounterSectionOne As Integer = 0
        Dim IntCounterSectionTwo As Integer = 0
        Dim IntCounterSectionThree As Integer = 0
        Dim IntCounter As Integer = 0
        Dim SectionID As Integer = 1

        Dim ASPLBL As Global.System.Web.UI.WebControls.Label
        Dim ASPTXT As Global.System.Web.UI.WebControls.TextBox
        Dim ASPDATE As Infragistics.WebUI.WebSchedule.WebDateChooser
        Dim ASPBTN As Infragistics.WebUI.WebDataInput.WebImageButton
        Dim ASPLBLdESC As Global.System.Web.UI.WebControls.Label
        Dim ObjSearchText As Global.System.Web.UI.WebControls.TextBox
        ' [0256] ===== end
        Try
            'If mObjOtherFields.Find("sys_OtherFields.ObjectID= " & ObjectID & " And sys_OtherFields.CancelDate is Null ") Then
            If mObjOtherFields.Find("sys_OtherFields.ObjectID= " & ObjectID & " And sys_OtherFields.OtherFieldGroupID= " & IntGroupId & " And sys_OtherFields.CancelDate is Null ") Then

                '[0256] Begin Here 
                If (blArCulture) Then

                    blArCulture = True
                    StrLangLbl = "ArbName"
                    StrFontName = "Tahoma"
                Else
                    StrLangLbl = "EngName"
                    StrFontName = "Arail"
                End If

                '[0256] End Here
                IntNoOfFields = mObjOtherFields.DataSet.Tables(0).Rows.Count
                ReDim Arr(IntNoOfFields)
                For Each ObjDr In mObjOtherFields.DataSet.Tables(0).Rows
                    CurrentTextBox = " "
                    '[0256] ================ Begin
                    If blArCulture Then
                        '// Prepare For Arabic Layout
                        If IntCounterSectionTwo > 5 Then SectionID = 2
                        If IntCounterSectionOne > 5 Then SectionID = 3
                        If SectionID = 2 Then
                            StrX_Pos = "28%"
                            'StrO_Pos = "52%"
                            StrlblX_Pos = "41%"
                            StrbtnX_Pos = "23%"
                            StrLblDesc_Pos = "15%"
                            IntCounter = IntCounterSectionOne
                        ElseIf SectionID = 3 Then
                            StrX_Pos = "11%"
                            'StrO_Pos = "21%"
                            StrlblX_Pos = "18%"
                            StrbtnX_Pos = "8%"
                            StrLblDesc_Pos = "1%"
                            IntCounter = IntCounterSectionThree
                        Else
                            StrX_Pos = "60%"
                            'StrO_Pos = "86%"
                            StrlblX_Pos = "80%"
                            StrbtnX_Pos = "55%"
                            StrLblDesc_Pos = "45%"
                            IntCounter = IntCounterSectionTwo
                        End If
                    Else
                        If IntCounterSectionTwo > 5 Then SectionID = 2
                        If IntCounterSectionOne > 5 Then SectionID = 3
                        If SectionID = 2 Then
                            StrX_Pos = "58%"
                            'StrO_Pos = "50%"
                            StrlblX_Pos = "20"
                            ''StrlblX_Pos = "44%"
                            StrbtnX_Pos = "68%"
                            StrLblDesc_Pos = "71%"
                            IntCounter = IntCounterSectionOne
                        ElseIf SectionID = 3 Then
                            StrX_Pos = "82%"
                            'StrO_Pos = "79%"
                            StrlblX_Pos = "72%"
                            StrbtnX_Pos = "88%"
                            StrLblDesc_Pos = "91%"
                            IntCounter = IntCounterSectionThree
                        Else
                            StrX_Pos = "18%"
                            'StrO_Pos = "15%"
                            StrlblX_Pos = "10px"
                            StrbtnX_Pos = "30%"
                            StrLblDesc_Pos = "34%"
                            IntCounter = IntCounterSectionTwo
                        End If

                    End If
                    If ObjDr.Item(StrLangLbl) Is DBNull.Value Then
                        literalName = ObjDr.Item("EngName")
                    Else
                        literalName = ObjDr.Item(StrLangLbl)
                    End If

                    CurrentTextBox = " "
                    ASPLBL = New Global.System.Web.UI.WebControls.Label
                    ASPLBL.ID = "lbl" & ObjDr.Item("EngName").ToString.Replace(" ", "")
                    ASPLBL.Style.Item("POSITION") = "absolute"
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
                    pnlFields.ContentPane.Controls.Add(ASPLBL)
                    '=========== [0256] End
                    Select Case ObjDr.Item("DataType")

                        Case 1
                            ASPTXT = New Web.UI.WebControls.TextBox
                            ASPTXT.ID = "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")

                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then
                                    Dim DR As DataRow() = objDS.Tables(0).Select(" OtherFieldId = " & ObjDr.Item("ID"))
                                    If DR(0).Item("Data") Is Nothing Then
                                        ASPTXT.Text = ""
                                    Else
                                        ASPTXT.Text = DR(0).Item("Data")
                                    End If
                                Else
                                    ASPTXT.Text = ""
                                End If
                            Catch ex As Exception

                            End Try


                            'B#0001 Object Created without prefix result in Object Not Found in Search Screen  
                            CurrentTextBox = "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ASPTXT.Style.Item("POSITION") = " absolute"
                            ASPTXT.Style.Item("LEFT") = StrX_Pos 'CStr(X_Pos + 143) & "px"
                            ASPTXT.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"

                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPTXT.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            Else
                                ASPTXT.Style.Item("WIDTH") = CStr(104) & "px"
                            End If

                            If blArCulture Then
                                ASPTXT.Style.Item("Text-Align") = "Right"
                                ASPTXT.Style.Item("Direction") = "RTL"
                            End If

                            ASPTXT.BackColor = Drawing.Color.Silver
                            ASPTXT.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPTXT.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPTXT.BorderColor = Drawing.Color.White
                            ASPTXT.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("DataLength"), SqlDbType.Int)
                            ParameterNames.Text &= "|" & "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'ParameterNames.Text &= "|" & StrGroupKey & "$WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            'Mezo

                            Arr(ValueCounter) = "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPTXT, False)
                            End If
                            ObjSearchText = ASPTXT
                            pnlFields.ContentPane.Controls.Add(ASPTXT)
                            If OpenFor = "A" Then ASPTXT.Text = ""

                        Case 2 '"int", "money", "tinyint"

                            Dim ASPWN As New Web.UI.WebControls.TextBox
                            ASPWN.ID = "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")

                            Try
                                If OpenFor = "U" Then
                                    Dim DR As DataRow() = objDS.Tables(0).Select(" OtherFieldId = " & ObjDr.Item("ID"))

                                    If DR(0).Item("Data") Is Nothing Then
                                        ASPWN.Text = ""
                                    Else
                                        ASPWN.Text = DR(0).Item("Data")
                                    End If
                                Else
                                    ASPWN.Text = ""
                                End If
                            Catch ex As Exception
                            End Try

                            CurrentTextBox = "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")

                            ASPWN.Style.Item("POSITION") = " absolute"
                            ASPWN.Style.Item("LEFT") = StrX_Pos 'CStr(X_Pos + 143) & "px"
                            ASPWN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"
                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPWN.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            Else
                                ASPWN.Style.Item("WIDTH") = CStr(104) & "px"
                            End If

                            If blArCulture Then
                                ASPWN.Style.Item("Text-Align") = "Right"
                                ASPWN.Style.Item("Direction") = "RTL"
                            End If

                            ASPWN.BackColor = Drawing.Color.Silver

                            ASPWN.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWN.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWN.BorderColor = Drawing.Color.White
                            ASPWN.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("DataLength"), SqlDbType.Int)

                            ParameterNames.Text &= "|" & "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")
                            If ObjDr.Item("SubSearchID") Is DBNull.Value Then Venus.Shared.Web.ClientSideActions.SetupForNumberOnly(mPage, ASPWN, True)
                            Arr(ValueCounter) = "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWN, False)
                            End If
                            ObjSearchText = ASPWN
                            pnlFields.ContentPane.Controls.Add(ASPWN)
                            If OpenFor = "A" Then ASPWN.Text = ""
                        Case 4 ' "smalldatetime"

                            ASPDATE = New Infragistics.WebUI.WebSchedule.WebDateChooser
                            ASPDATE.ID = "WD_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
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
                            ASPDATE.BackColor = Drawing.Color.Silver

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
                            ASPDATE.Width = Global.System.Web.UI.WebControls.Unit.Pixel(110)
                            ASPDATE.NullDateLabel = ""

                            ASPDATE.CalendarLayout.CalendarStyle.BorderColor = Drawing.Color.White
                            ParameterNames.Text &= "|" & "WD_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'ParameterNames.Text &= "|" & StrGroupKey & "$WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            Arr(ValueCounter) = "WD_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                ASPDATE.Focus()
                            End If

                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then
                                    Dim DR As DataRow() = objDS.Tables(0).Select(" OtherFieldId = " & ObjDr.Item("ID"))

                                    If DR(0).Item("Data") Is Nothing Then
                                        ASPDATE.Value = Nothing
                                    Else
                                        ASPDATE.Value = DR(0).Item("Data")
                                    End If
                                Else
                                    ASPDATE.Value = Nothing
                                End If
                            Catch ex As Exception

                            End Try
                            pnlFields.ContentPane.Controls.Add(ASPDATE)
                            If OpenFor = "A" Then ASPDATE.Value = Nothing
                        Case 3 ' "bit"

                            Dim ASPWB As New Web.UI.WebControls.DropDownList
                            Dim ASPItem1 As New Web.UI.WebControls.ListItem
                            Dim ASPItem2 As New Web.UI.WebControls.ListItem


                            If blArCulture Then
                                ASPWB.Style.Item("TEXT-ALIGN") = "right"
                                ASPWB.Style.Item("DIRECTION") = "rtl"
                                ASPItem1.Text = "äÚã"
                                ASPItem2.Text = "áÇ"
                            Else
                                ASPItem1.Text = "Yes"
                                ASPItem2.Text = "No"
                            End If


                            ASPItem1.Value = 0

                            ASPItem2.Value = 1
                            ASPWB.Items.Add(ASPItem1)
                            ASPWB.Items.Add(ASPItem2)
                            ASPWB.ID = "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then

                                    Dim DR As DataRow() = objDS.Tables(0).Select(" OtherFieldId = " & ObjDr.Item("ID"))

                                    If DR(0).Item("Data") = 1 Then
                                        ASPWB.SelectedIndex = 1
                                    Else
                                        ASPWB.SelectedIndex = 0
                                    End If


                                    'If CInt(objDS.Tables(0).Rows(ValueCounter).Item(3)) = 1 Then
                                    '    ASPWB.SelectedIndex = 1
                                    'Else
                                    '    ASPWB.SelectedIndex = 0
                                    'End If
                                Else
                                    ASPWB.SelectedIndex = 0
                                End If
                            Catch ex As Exception

                            End Try

                            CurrentTextBox = "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'CurrentTextBox = "WB_" & ObjDr.Item("EngName")
                            ASPWB.Style.Item("POSITION") = " absolute"
                            ASPWB.Style.Item("LEFT") = StrX_Pos 'CStr(X_Pos + 143) & "px"
                            ASPWB.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"

                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPWB.Style.Item("WIDTH") = CStr(VwWidth) & "px"
                            Else
                                ASPWB.Style.Item("WIDTH") = CStr(110) & "px"
                            End If

                            ASPWB.Style.Item("WIDTH") = CStr(110) & "px"
                            ASPWB.BackColor = Drawing.Color.Silver
                            ASPWB.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWB.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWB.BorderColor = Drawing.Color.White
                            ParameterNames.Text &= "|" & "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'ParameterNames.Text &= "|" & StrGroupKey & "$WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            'Mezo
                            Arr(ValueCounter) = "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWB, False)
                            End If
                            pnlFields.ContentPane.Controls.Add(ASPWB)
                    End Select


                    If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then
                        Dim StrDataDesc As String
                        ASPBTN = New Infragistics.WebUI.WebDataInput.WebImageButton
                        ASPLBLdESC = New Global.System.Web.UI.WebControls.Label

                        ASPBTN.ID = "btn" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                        ASPBTN.Style.Item("POSITION") = " absolute"
                        ASPBTN.Style.Item("LEFT") = StrbtnX_Pos
                        ASPBTN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                        ASPBTN.Style.Item("BACKGROUND-COLOR") = "BLUE"
                        ASPBTN.Width = Global.System.Web.UI.WebControls.Unit.Pixel(23)
                        ASPBTN.Height = Global.System.Web.UI.WebControls.Unit.Pixel(14)
                        'ASPBTN.Text = "...."

                        '==== Description Label 
                        ASPLBLdESC.ID = "lblDesc" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                        ASPLBLdESC.Style.Item("POSITION") = " absolute"
                        ASPLBLdESC.Style.Item("LEFT") = StrLblDesc_Pos
                        ASPLBLdESC.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"

                        ASPLBLdESC.Width = Global.System.Web.UI.WebControls.Unit.Pixel(100)
                        ASPLBLdESC.Height = Global.System.Web.UI.WebControls.Unit.Pixel(14)

                        'ASPLBLdESC.Text = "Here Will be EngDesc"
                        clsSearchs.Find(" sys_Searchs.Id = " & ObjDr.Item("SubSearchID"))
                        clsObjects.Find(" sys_Objects.Id = " & clsSearchs.ObjectID)

                        Dim drFiltered As DataRow() = objDS.Tables(0).Select(" OtherFieldId = " & ObjDr.Item("ID"))
                        If drFiltered.Length > 0 Then
                            If Not drFiltered(0).Item("Data") Is Nothing And Not IsDBNull(drFiltered(0).Item("Data")) Then
                                StrDataDesc = GetFieldDescription(drFiltered(0).Item("Data"), clsObjects.Code)
                                If Not IsDBNull(StrDataDesc) Then
                                    If StrDataDesc.Length > 0 And StrDataDesc <> "/" Then
                                        Dim IntPostion As Integer = StrDataDesc.IndexOf("/")
                                        If blArCulture Then
                                            ASPLBLdESC.Text = StrDataDesc.Substring(IntPostion + 1)
                                        Else
                                            ASPLBLdESC.Text = StrDataDesc.Substring(0, IntPostion - 1)
                                        End If

                                    End If

                                End If

                            End If
                        End If


                        'ASPBTN.Appearance.Image.Url = "../Interfaces/forum_search.gif"
                        ObjSearchText.Attributes.Add("onblur", "GetSearchDescription(" & StrFormName & ",'" & ObjDr.Item("SubSearchID") & "','" & ObjSearchText.ID & "','" & ASPLBLdESC.ID & "');")
                        Venus.Shared.Web.ClientSideActions.SetSearchButton(mPage, ASPBTN, ObjSearchText, ObjDr.Item("SubSearchID"), 615, 726)

                        pnlFields.ContentPane.Controls.Add(ASPBTN)
                        pnlFields.ContentPane.Controls.Add(ASPLBLdESC)



                    End If
                    If SectionID = 2 Then
                        IntCounterSectionOne += 1
                    ElseIf SectionID = 3 Then
                        IntCounterSectionThree += 1
                    Else
                        IntCounterSectionTwo += 1
                    End If

                    ValueCounter += 1
                Next
                Venus.Shared.Web.ClientSideActions.SetupTabOrder(mPage, Arr, Drawing.Color.White, False)
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")

        End Try
    End Function

    '============================================================================
    'Method Name  : AssignValues()
    'Developer    :  [0256] 
    'Date Created : 08-08-2007 
    'Description  : Assigns values to BO Layer clssys_OtherFieldsData Class
    '============================================================================
    Public Function ReadNwriteOtherFieldsValues(ByVal ObjectID As Integer, ByVal IntGroupId As Integer, _
 ByRef pnlFields As Infragistics.WebUI.UltraWebTab.Tab, _
 ByRef ParameterNames As Web.UI.WebControls.Label, ByRef ParameterRealName As Web.UI.WebControls.Label, ByVal StrLanguage As String, _
    ByVal StrGroupKey As String, ByVal StrFormName As String, Optional ByRef objDS As Data.DataSet = Nothing, Optional ByVal OpenFor As String = "A") As Boolean

        mObjOtherFields = New ClsSys_OtherFields(mPage)
        Dim clsSearchs As New Clssys_Searchs(mPage)
        Dim clsObjects As New Clssys_Objects(mPage)



        Dim ObjDr As DataRow
        Dim ValueCounter As Integer
        Dim str As String
        Dim litral As New Web.UI.LiteralControl
        'Dim Y_Pos As Integer = 40
        'Dim X_Pos As Integer = 20
        'Dim Width As Integer = 244

        Dim VwWidth As Integer = 80
        Dim CurrentTextBox As String
        'ParameterNames.Text = ""
        'ParameterRealName.Text = ""
        Dim Arr() As String
        Dim subSearchID As Integer = 0

        ' [0256] ===== begin
        Dim IntNoOfFields As Integer

        Dim blArCulture As Boolean = IIf(StrLanguage = "ar-EG", True, False)
        Dim literalName As String = String.Empty
        Dim StrFontName As String = String.Empty
        Dim StrLangLbl As String = String.Empty

        Dim IntY_Pos As Integer = 110
        Dim StrX_Pos As String = 60
        'Dim StrO_Pos As String = "23%"
        Dim StrlblX_Pos As String = 20
        Dim StrbtnX_Pos As String = 320
        Dim StrLblDesc_Pos As String

        Dim IntCounterSectionOne As Integer = 0
        Dim IntCounterSectionTwo As Integer = 0
        Dim IntCounterSectionThree As Integer = 0
        Dim IntCounter As Integer = 0
        Dim SectionID As Integer = 1

        Dim ASPLBL As Global.System.Web.UI.WebControls.Label
        Dim ASPTXT As Global.System.Web.UI.WebControls.TextBox
        Dim ASPDATE As Infragistics.WebUI.WebSchedule.WebDateChooser
        Dim ASPBTN As Infragistics.WebUI.WebDataInput.WebImageButton
        Dim ASPLBLdESC As Global.System.Web.UI.WebControls.Label
        Dim ObjSearchText As Global.System.Web.UI.WebControls.TextBox
        VwWidth = 244
        ' [0256] ===== end
        Try
            'If mObjOtherFields.Find("sys_OtherFields.ObjectID= " & ObjectID & " And sys_OtherFields.CancelDate is Null ") Then
            If mObjOtherFields.Find("sys_OtherFields.ObjectID= " & ObjectID & " And sys_OtherFields.OtherFieldGroupID= " & IntGroupId & " And sys_OtherFields.CancelDate is Null ") Then

                '[0256] Begin Here 
                If (blArCulture) Then

                    blArCulture = True
                    StrLangLbl = "ArbName"
                    StrFontName = "Tahoma"
                Else
                    StrLangLbl = "EngName"
                    StrFontName = "Arail"
                End If

                '[0256] End Here
                IntNoOfFields = mObjOtherFields.DataSet.Tables(0).Rows.Count
                ReDim Arr(IntNoOfFields)
                For Each ObjDr In mObjOtherFields.DataSet.Tables(0).Rows
                    CurrentTextBox = " "
                    '[0256] ================ Begin
                    If blArCulture Then
                        If IntCounterSectionTwo > 11 Then SectionID = 2
                        If IntCounterSectionOne > 11 Then SectionID = 3
                        If SectionID = 2 Then
                            StrX_Pos = "6%"
                            StrlblX_Pos = "33%"
                            StrbtnX_Pos = "22%"
                            StrLblDesc_Pos = "2.5%"
                            IntCounter = IntCounterSectionOne
                        ElseIf SectionID = 3 Then
                            StrX_Pos = "11%"
                            StrlblX_Pos = "18%"
                            StrbtnX_Pos = "8%"
                            StrLblDesc_Pos = "1%"
                            IntCounter = IntCounterSectionThree
                        Else
                            IntCounter = IntCounterSectionTwo
                            StrX_Pos = "55.5%"
                            StrlblX_Pos = "82.5%"
                            StrbtnX_Pos = "78.5%"
                            StrLblDesc_Pos = "51.5%"

                        End If
                    Else
                        If IntCounterSectionTwo > 11 Then SectionID = 2
                        If IntCounterSectionOne > 11 Then SectionID = 3
                        If SectionID = 2 Then
                            StrX_Pos = 545 & "px"
                            StrlblX_Pos = 415 & "px"
                            StrbtnX_Pos = 601 & "px"
                            StrLblDesc_Pos = 628 & "px"
                            IntCounter = IntCounterSectionOne
                        ElseIf SectionID = 3 Then
                            StrX_Pos = "82%"
                            StrlblX_Pos = "72%"
                            StrbtnX_Pos = "88%"
                            StrLblDesc_Pos = "91%"
                            IntCounter = IntCounterSectionThree
                        Else
                            StrX_Pos = 150 & "px"
                            StrlblX_Pos = 20 & "px"
                            StrbtnX_Pos = 206 & "px"
                            StrLblDesc_Pos = 233 & "px"
                            IntCounter = IntCounterSectionTwo
                        End If

                    End If
                    If ObjDr.Item(StrLangLbl) Is DBNull.Value Then
                        literalName = ObjDr.Item("EngName")
                    Else
                        literalName = ObjDr.Item(StrLangLbl)
                    End If

                    If blArCulture And Not ObjDr.Item("SubSearchID") Is DBNull.Value Then
                        If SectionID = 2 Then
                            StrX_Pos = "25.5%"
                        Else
                            StrX_Pos = "71.5%"
                        End If
                    End If

                    CurrentTextBox = " "
                    ASPLBL = New Global.System.Web.UI.WebControls.Label
                    ASPLBL.ID = "lbl" & ObjDr.Item("EngName").ToString.Replace(" ", "")
                    ASPLBL.Style.Item("POSITION") = "absolute"
                    ASPLBL.Style.Item("LEFT") = StrlblX_Pos
                    ASPLBL.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                    If blArCulture Then
                        ASPLBL.Style.Item("TEXT-ALIGN") = "right"
                        ASPLBL.Style.Item("DIRECTION") = "rtl"
                    End If
                    ASPLBL.Width = Global.System.Web.UI.WebControls.Unit.Pixel(123)
                    ASPLBL.Text = literalName
                    ASPLBL.CssClass = "OtherFieldsLabel"
                    'ASPLBL.Font.Name = StrFontName
                    'ASPLBL.Font.Size = Web.UI.WebControls.FontSize.Large
                    pnlFields.ContentPane.Controls.Add(ASPLBL)
                    '=========== [0256] End
                    Select Case ObjDr.Item("DataType")

                        Case 1
                            ASPTXT = New Web.UI.WebControls.TextBox
                            ASPTXT.ID = "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")

                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then
                                    Dim DR As DataRow() = objDS.Tables(0).Select(" OtherFieldId = " & ObjDr.Item("ID"))
                                    If DR(0).Item("Data") Is Nothing Then
                                        ASPTXT.Text = ""
                                    Else
                                        ASPTXT.Text = DR(0).Item("Data")
                                    End If
                                Else
                                    ASPTXT.Text = ""
                                End If
                            Catch ex As Exception

                            End Try


                            'B#0001 Object Created without prefix result in Object Not Found in Search Screen  
                            CurrentTextBox = "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ASPTXT.Style.Item("POSITION") = " absolute"

                            ASPTXT.Style.Item("LEFT") = StrX_Pos 'CStr(X_Pos + 143) & "px"
                            ASPTXT.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"

                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPTXT.Style.Item("WIDTH") = CStr(VwWidth - 194) & "px"
                            Else
                                ASPTXT.Style.Item("WIDTH") = CStr(VwWidth - 40) & "px"
                            End If

                            If blArCulture Then
                                ASPTXT.Style.Item("Text-Align") = "Right"
                                ASPTXT.Style.Item("Direction") = "RTL"
                            End If

                            ASPTXT.BackColor = Drawing.Color.Silver
                            ASPTXT.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPTXT.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPTXT.BorderColor = Drawing.Color.White
                            ASPTXT.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("DataLength"), SqlDbType.Int)
                            ParameterNames.Text &= "|" & "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'ParameterNames.Text &= "|" & StrGroupKey & "$WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            'Mezo

                            Arr(ValueCounter) = "WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPTXT, False)
                            End If
                            ASPTXT.Attributes.Add("onChange", "DataChanged()")
                            ObjSearchText = ASPTXT
                            pnlFields.ContentPane.Controls.Add(ASPTXT)
                            If OpenFor = "A" Then ASPTXT.Text = ""

                        Case 2 '"int", "money", "tinyint"

                            Dim ASPWN As New Web.UI.WebControls.TextBox
                            ASPWN.ID = "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")

                            Try
                                If OpenFor = "U" Then
                                    Dim DR As DataRow() = objDS.Tables(0).Select(" OtherFieldId = " & ObjDr.Item("ID"))

                                    If DR(0).Item("Data") Is Nothing Then
                                        ASPWN.Text = ""
                                    Else
                                        ASPWN.Text = DR(0).Item("Data")
                                    End If
                                Else
                                    ASPWN.Text = ""
                                End If
                            Catch ex As Exception
                            End Try

                            CurrentTextBox = "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")

                            ASPWN.Style.Item("POSITION") = " absolute"
                            ASPWN.Style.Item("LEFT") = StrX_Pos 'CStr(X_Pos + 143) & "px"
                            ASPWN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"
                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPWN.Style.Item("WIDTH") = CStr(VwWidth - 194) & "px"
                            Else
                                ASPWN.Style.Item("WIDTH") = CStr(VwWidth - 40) & "px"
                            End If

                            If blArCulture Then
                                ASPWN.Style.Item("Text-Align") = "Right"
                                ASPWN.Style.Item("Direction") = "RTL"
                            End If

                            ASPWN.BackColor = Drawing.Color.Silver

                            ASPWN.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWN.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWN.BorderColor = Drawing.Color.White
                            ASPWN.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("DataLength"), SqlDbType.Int)

                            ParameterNames.Text &= "|" & "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")
                            If ObjDr.Item("SubSearchID") Is DBNull.Value Then Venus.Shared.Web.ClientSideActions.SetupForNumberOnly(mPage, ASPWN, True)
                            Arr(ValueCounter) = "WN_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWN, False)
                            End If
                            ASPWN.Attributes.Add("onChange", "DataChanged()")
                            ObjSearchText = ASPWN
                            pnlFields.ContentPane.Controls.Add(ASPWN)
                            If OpenFor = "A" Then ASPWN.Text = ""
                        Case 4 ' "smalldatetime"

                            ASPDATE = New Infragistics.WebUI.WebSchedule.WebDateChooser
                            ASPDATE.ID = "WD_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ASPDATE.Style.Item("POSITION") = " absolute"
                            ASPDATE.Style.Item("LEFT") = StrX_Pos
                            ASPDATE.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                            ASPDATE.CssClass = "DateBoxSearchCriteria"
                            ASPDATE.Width = Global.System.Web.UI.WebControls.Unit.Pixel(VwWidth - 43)
                            ASPDATE.Format = Infragistics.WebUI.WebSchedule.DateFormat.Short
                            If blArCulture Then
                                ASPDATE.Style.Item("TEXT-ALIGN") = "right"
                                ASPDATE.Style.Item("DIRECTION") = "rtl"
                            End If
                            ASPDATE.BackColor = Drawing.Color.Silver

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

                            ASPDATE.NullDateLabel = ""

                            ASPDATE.CalendarLayout.CalendarStyle.BorderColor = Drawing.Color.White
                            ParameterNames.Text &= "|" & "WD_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'ParameterNames.Text &= "|" & StrGroupKey & "$WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            Arr(ValueCounter) = "WD_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                ASPDATE.Focus()
                            End If

                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then
                                    Dim DR As DataRow() = objDS.Tables(0).Select(" OtherFieldId = " & ObjDr.Item("ID"))

                                    If DR(0).Item("Data") Is Nothing Then
                                        ASPDATE.Value = Nothing
                                    Else
                                        ASPDATE.Value = DR(0).Item("Data")
                                    End If
                                Else
                                    ASPDATE.Value = Nothing
                                End If
                            Catch ex As Exception

                            End Try
                            ASPDATE.ClientSideEvents.CalendarValueChanging = "DataChanged"
                            pnlFields.ContentPane.Controls.Add(ASPDATE)
                            If OpenFor = "A" Then ASPDATE.Value = Nothing
                        Case 3 ' "bit"

                            Dim ASPWB As New Web.UI.WebControls.DropDownList
                            Dim ASPItem1 As New Web.UI.WebControls.ListItem
                            Dim ASPItem2 As New Web.UI.WebControls.ListItem


                            If blArCulture Then
                                ASPWB.Style.Item("TEXT-ALIGN") = "right"
                                ASPWB.Style.Item("DIRECTION") = "rtl"
                                ASPItem1.Text = "äÚã"
                                ASPItem2.Text = "áÇ"
                            Else
                                ASPItem1.Text = "Yes"
                                ASPItem2.Text = "No"
                            End If


                            ASPItem1.Value = 0

                            ASPItem2.Value = 1
                            ASPWB.Items.Add(ASPItem1)
                            ASPWB.Items.Add(ASPItem2)
                            ASPWB.ID = "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'B#0003 Newly Created Other Fields didn't be Displayed in Other Field Screen for already created record
                            Try
                                If OpenFor = "U" Then

                                    Dim DR As DataRow() = objDS.Tables(0).Select(" OtherFieldId = " & ObjDr.Item("ID"))

                                    If DR(0).Item("Data") = 1 Then
                                        ASPWB.SelectedIndex = 1
                                    Else
                                        ASPWB.SelectedIndex = 0
                                    End If


                                    'If CInt(objDS.Tables(0).Rows(ValueCounter).Item(3)) = 1 Then
                                    '    ASPWB.SelectedIndex = 1
                                    'Else
                                    '    ASPWB.SelectedIndex = 0
                                    'End If
                                Else
                                    ASPWB.SelectedIndex = 0
                                End If
                            Catch ex As Exception

                            End Try

                            CurrentTextBox = "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'CurrentTextBox = "WB_" & ObjDr.Item("EngName")
                            ASPWB.Style.Item("POSITION") = " absolute"
                            ASPWB.Style.Item("LEFT") = StrX_Pos 'CStr(X_Pos + 143) & "px"
                            ASPWB.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px" 'CStr(ValueCounter * 25 + Y_Pos) & "px"

                            If Not ObjDr.Item("ViewObjectID") Is DBNull.Value Then
                                ASPWB.Style.Item("WIDTH") = CStr(VwWidth - 132) & "px"
                            Else
                                ASPWB.Style.Item("WIDTH") = CStr(VwWidth - 37) & "px"
                            End If

                            ASPWB.BackColor = Drawing.Color.Silver
                            ASPWB.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWB.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWB.BorderColor = Drawing.Color.White
                            ParameterNames.Text &= "|" & "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            'ParameterNames.Text &= "|" & StrGroupKey & "$WV_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ID")

                            'Mezo
                            Arr(ValueCounter) = "WB_" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                            If ValueCounter = 0 Then
                                'Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWB, False)
                            End If
                            ASPWB.Attributes.Add("onChange", "DataChanged()")
                            pnlFields.ContentPane.Controls.Add(ASPWB)
                    End Select


                    If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then
                        Dim StrDataDesc As String
                        ASPBTN = New Infragistics.WebUI.WebDataInput.WebImageButton
                        ASPLBLdESC = New Global.System.Web.UI.WebControls.Label

                        ASPBTN.ID = "btn" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                        ASPBTN.Style.Item("POSITION") = " absolute"
                        ASPBTN.Style.Item("LEFT") = StrbtnX_Pos
                        ASPBTN.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"
                        'ASPBTN.Style.Item("BACKGROUND-COLOR") = "BLUE"
                        ASPBTN.Width = Global.System.Web.UI.WebControls.Unit.Pixel(25)
                        ASPBTN.Height = Global.System.Web.UI.WebControls.Unit.Pixel(14)
                        'ASPBTN.AutoSubmit = False
                        'ASPBTN.Text = "...."

                        '==== Description Label 
                        ASPLBLdESC.ID = "lblDesc" & ObjDr.Item("EngName").ToString.Replace(" ", "_")
                        ASPLBLdESC.Style.Item("POSITION") = " absolute"
                        ASPLBLdESC.Style.Item("LEFT") = StrLblDesc_Pos
                        ASPLBLdESC.Style.Item("TOP") = CStr(IntY_Pos + (32 * IntCounter)) & "px"

                        'ASPLBLdESC.Width = Global.System.Web.UI.WebControls.Unit.Pixel(100)
                        'ASPLBLdESC.Height = Global.System.Web.UI.WebControls.Unit.Pixel(14)
                        ASPLBLdESC.CssClass = "OtherFieldsSearchDesc"
                        'ASPLBLdESC.Text = "Here Will be EngDesc"
                        clsSearchs.Find(" sys_Searchs.Id = " & ObjDr.Item("SubSearchID"))
                        clsObjects.Find(" sys_Objects.Id = " & clsSearchs.ObjectID)

                        Dim drFiltered As DataRow() = objDS.Tables(0).Select(" OtherFieldId = " & ObjDr.Item("ID"))
                        If drFiltered.Length > 0 Then
                            If Not drFiltered(0).Item("Data") Is Nothing And Not IsDBNull(drFiltered(0).Item("Data")) Then
                                StrDataDesc = GetFieldDescription(drFiltered(0).Item("Data"), clsObjects.Code)
                                If Not IsDBNull(StrDataDesc) Then
                                    If StrDataDesc.Length > 0 And StrDataDesc <> "/" Then
                                        Dim IntPostion As Integer = StrDataDesc.IndexOf("/")
                                        If blArCulture Then
                                            ASPLBLdESC.Text = StrDataDesc.Substring(IntPostion + 1)
                                        Else
                                            ASPLBLdESC.Text = StrDataDesc.Substring(0, IntPostion - 1)
                                        End If

                                    End If

                                End If

                            End If
                        End If


                        'ASPBTN.Appearance.Image.Url = "../Interfaces/forum_search.gif"
                        ObjSearchText.Attributes.Add("onblur", "GetSearchDescription(" & StrFormName & ",'" & ObjDr.Item("SubSearchID") & "','" & ObjSearchText.ID & "','" & ASPLBLdESC.ID & "');")
                        Venus.Shared.Web.ClientSideActions.SetSearchButton(mPage, ASPBTN, ObjSearchText, ObjDr.Item("SubSearchID"), 615, 726)

                        pnlFields.ContentPane.Controls.Add(ASPBTN)
                        pnlFields.ContentPane.Controls.Add(ASPLBLdESC)



                    End If
                    If SectionID = 2 Then
                        IntCounterSectionOne += 1
                    ElseIf SectionID = 3 Then
                        IntCounterSectionThree += 1
                    Else
                        IntCounterSectionTwo += 1
                    End If

                    ValueCounter += 1
                Next
                Venus.Shared.Web.ClientSideActions.SetupTabOrder(mPage, Arr, Drawing.Color.White, False)
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")

        End Try
    End Function


    Public Function CreateGridColumns(ByVal IntObjectId As Integer, ByRef uwtGridControl As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByRef pnlFields As Infragistics.WebUI.UltraWebTab.Tab, ByVal lblNames As Web.UI.WebControls.Label, ByVal lblRealNames As Web.UI.WebControls.Label, ByVal StrMode As String, ByVal ObjDS As DataSet, ByVal strFormName As String) As Boolean
        Dim clsSearchs As New Clssys_Searchs(mPage)
        Dim clsObjects As New Clssys_Objects(mPage)
        mObjOtherFields = New ClsSys_OtherFields(mPage)
        mObjOtherFields.Find(" sys_OtherFields.ObjectID =" & IntObjectId)
        Dim uwgColumn As Infragistics.WebUI.UltraWebGrid.UltraGridColumn
        Dim uwgTempColumn As Infragistics.WebUI.UltraWebGrid.UltraGridColumn
        Dim IntTemp As Integer
        Try
            '------------------------------------------------  
            'Crete Controls   
            If IsNothing(pnlFields.ContentPane.FindControl("WD_wdcDateTime1")) Then
                Dim wdcDateTime1 As New Infragistics.WebUI.WebSchedule.WebDateChooser()
                wdcDateTime1.ID = "WD_wdcDateTime1"
                wdcDateTime1.Style.Item("POSITION") = " absolute"
                wdcDateTime1.Style.Item("LEFT") = "999px"
                wdcDateTime1.Style.Item("TOP") = "40px"
                wdcDateTime1.Style.Item("WIDTH") = "80px"

                wdcDateTime1.MinDate = "01/01/1900"
                wdcDateTime1.MaxDate = "01/01/2079"
                wdcDateTime1.ImageDirectory = "/ig_common/20071CLR20/Styles/Office2007Blue/WebDateChooser/"
                wdcDateTime1.CssClass = "igWebDateChooserMainBlue2k7"
                wdcDateTime1.DropDownStyle.CssClass = "igwebDateChooserDropdownBlue2k7"
                wdcDateTime1.EditStyle.CssClass = "igwebDateChooserDropdownEditBlue2k7"

                wdcDateTime1.DropButton.ImageUrl2 = "..\Interfaces\Styles\Office2007Blue\WebDateChooser\igWebDateChooser_Dropdown_Clicked.jpg"
                wdcDateTime1.DropButton.ImageUrl1 = "..\Interfaces\Styles\Office2007Blue\WebDateChooser\igWebDateChooser_Dropdown_Normal.jpg"
                wdcDateTime1.DropButton.HoverStyle.CssClass = "igwebDateChooserDropdownBtHoverBlue2k7"

                wdcDateTime1.CalendarLayout.PrevMonthImageUrl = "..\Interfaces\Styles\Office2007Blue\WebDateChooser\igWebDateChooser_Prev.gif"
                wdcDateTime1.CalendarLayout.NextMonthImageUrl = "..\Interfaces\Styles\Office2007Blue\WebDateChooser\igWebDateChooser_Next.gif"
                wdcDateTime1.CalendarLayout.ShowYearDropDown = "False"
                wdcDateTime1.CalendarLayout.CellPadding = "0"
                wdcDateTime1.CalendarLayout.FooterFormat = "Today"
                wdcDateTime1.CalendarLayout.ShowMonthDropDown = "False"
                wdcDateTime1.CalendarLayout.TodayDayStyle.CssClass = "igwebDateChooserDropdownToydayDayBlue2k7"
                wdcDateTime1.CalendarLayout.FooterStyle.CssClass = "igwebDateChooserDropdownFooterBlue2k7"
                wdcDateTime1.CalendarLayout.SelectedDayStyle.CssClass = "igwebDateChooserDropdownSelectedDayBlue2k7"
                wdcDateTime1.CalendarLayout.DayStyle.CssClass = "igwebDateChooserDropdownDayBlue2k7"
                wdcDateTime1.CalendarLayout.NextPrevStyle.CssClass = "igwebDateChooserDropdownNextPrevBlue2k7"
                wdcDateTime1.CalendarLayout.OtherMonthDayStyle.CssClass = "igwebDateChooserDropdownOtherMonthDayBlue2k7"
                wdcDateTime1.CalendarLayout.DayHeaderStyle.CssClass = "igwebDateChooserDropdownDayheaderBlue2k7"
                wdcDateTime1.CalendarLayout.TitleStyle.CssClass = "igwebDateChooserDropdownTitleBlue2k7"
                wdcDateTime1.CalendarLayout.DropDownStyle.CssClass = "igwebDateChooserDropdownCalendarBlue2k7"

                wdcDateTime1.Format = Infragistics.WebUI.WebSchedule.DateFormat.Short
                wdcDateTime1.AllowNull = True
                wdcDateTime1.NullDateLabel = ""
                wdcDateTime1.TabIndex = -1
                pnlFields.ContentPane.Controls.Add(wdcDateTime1)
            End If
            '------------------------------------------------
            If IsNothing(pnlFields.ContentPane.FindControl("wne_Numeric1")) Then
                Dim wneNumeric1 As New Infragistics.WebUI.WebDataInput.WebNumericEdit()
                wneNumeric1.ID = "wne_Numeric1"
                wneNumeric1.Style.Item("POSITION") = " absolute"
                wneNumeric1.Style.Item("LEFT") = "999px"
                wneNumeric1.Style.Item("TOP") = "70px"
                wneNumeric1.Style.Item("WIDTH") = "65px"

                wneNumeric1.AutoPostBack = False
                wneNumeric1.TabIndex = -1
                pnlFields.ContentPane.Controls.Add(wneNumeric1)
            End If
            '------------------------------------------------
            If IsNothing(pnlFields.ContentPane.FindControl("wte_String1")) Then
                Dim wteText1 As New Infragistics.WebUI.WebDataInput.WebTextEdit()
                wteText1.ID = "wte_String1"
                wteText1.Style.Item("POSITION") = " absolute"
                wteText1.Style.Item("LEFT") = "999px"
                wteText1.Style.Item("TOP") = "100px"
                wteText1.Style.Item("WIDTH") = "80px"

                wteText1.AutoPostBack = False
                wteText1.TabIndex = -1
                pnlFields.ContentPane.Controls.Add(wteText1)
            End If
            '------------------------------------------------
            '************************************************
            'If mPage.IsPostBack Then
            Do
                uwgTempColumn = uwtGridControl.Columns(IntTemp)
                If Not IsNothing(uwgTempColumn.Key) Then
                    If (uwgTempColumn.Key.Length < 3) Then
                        IntTemp += 1
                    Else
                        If uwgTempColumn.Key.Substring(0, 3) = "OF_" Then
                            uwtGridControl.Columns.Remove(uwgTempColumn)
                        Else
                            IntTemp += 1
                        End If
                    End If
                Else
                    IntTemp += 1
                End If
            Loop While uwtGridControl.Columns.Count - IntTemp <> 0
            'End If
            '************************************************
            If mObjOtherFields.DataSet.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In mObjOtherFields.DataSet.Tables(0).Rows
                    uwgColumn = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn()
                    uwgColumn.Header.Caption = row.Item("EngName")
                    uwgColumn.Key = "OF_" & row.Item("ID")
                    uwgColumn.BaseColumnName = "OF_" & row.Item("EngName")
                    uwgColumn.Hidden = False
                    uwgColumn.Width = Val(row.Item("Width"))
                    'uwgColumn.Width = 85
                    uwgColumn.AllowNull = True
                    '+++++++++++++++++++++++++++++++
                    If IsDBNull(row.Item("ViewObjectID")) Then
                        Select Case Val(row.Item("DataType"))
                            Case 1
                                uwgColumn.DataType = "System.String"
                                uwgColumn.Type = Infragistics.WebUI.UltraWebGrid.ColumnType.Custom
                                uwgColumn.EditorControlID = "wte_String1"
                            Case 2
                                uwgColumn.DataType = "System.Int32"
                                uwgColumn.Type = Infragistics.WebUI.UltraWebGrid.ColumnType.Custom
                                uwgColumn.EditorControlID = "wne_Numeric1"
                            Case 3
                                uwgColumn.DataType = "System.Boolean"
                                uwgColumn.Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox
                            Case 4
                                uwgColumn.DataType = "System.DateTime"
                                uwgColumn.Type = Infragistics.WebUI.UltraWebGrid.ColumnType.Custom
                                uwgColumn.EditorControlID = "WD_wdcDateTime1"
                        End Select
                    Else
                        If row.Item("ViewObjectID") > 0 Then
                            clsObjects.Find(" sys_Objects.Id = " & row.Item("ViewObjectID"))
                            If (clsObjects.ID > 0) Then
                                uwgColumn.BaseColumnName = "ID"
                                GlobalGetList(uwgColumn.ValueList, clsObjects.EngName, True)
                                uwgColumn.DataType = "System.Int32"
                                uwgColumn.Type = Infragistics.WebUI.UltraWebGrid.ColumnType.DropDownList
                            End If
                        End If

                    End If
                    '+++++++++++++++++++++++++++++++
                    uwtGridControl.Columns.Add(uwgColumn)
                Next
            End If
            '************************************************
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function


    Public Function GlobalGetList(ByVal DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal strTableName As String, ByVal NullNode As Boolean) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrCommandString = "Select * From " & strTableName & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngName"), SqlDbType.VarChar)
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                Item.DataValue = ObjDataRow("ID")
                DdlValues.ValueListItems.Add(Item)
            Next
            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")

        Finally
            ObjDataset.Dispose()
        End Try

    End Function


    Public Function SaveGridOtherFields(ByVal IntRecordID As Integer, ByVal drRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow, ByRef DG As Infragistics.WebUI.UltraWebGrid.UltraWebGrid) As Boolean
        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(mPage)
        Dim OfID As Integer
        Dim OfData As Object
        Dim IntLenght As Integer
        Try
            For Each column As Infragistics.WebUI.UltraWebGrid.UltraGridColumn In DG.Columns
                If Not IsNothing(column.Key) Then
                    If (column.Key.Length > 3) Then
                        If column.Key.Substring(0, 3) = "OF_" Then
                            OfData = drRow.Cells.FromKey(column.Key).Value
                            IntLenght = column.Key.Length - 3
                            OfID = column.Key.Substring(3, IntLenght)
                            clsOtherFieldsData.Find(" OtherFieldID = " & OfID & " And RecordID = " & IntRecordID)
                            AssignValues(clsOtherFieldsData, IntRecordID, OfID, OfData)
                            If clsOtherFieldsData.ID > 0 Then
                                clsOtherFieldsData.Update(" ID = " & clsOtherFieldsData.ID)
                            Else
                                clsOtherFieldsData.Save()
                            End If
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '============================================================================
    'Method Name  : AssignValues()
    'Developer    : [0256] [0261]
    'Date Created : 08-08-2007 
    'Description  : Assigns values to BO Layer clssys_OtherFieldsData Class
    '============================================================================
    Public Function CreateOtherFieldsTabs(ByVal IntObjectId As Integer, ByRef dsDataSet As DataSet, ByRef uwtTabControl As Infragistics.WebUI.UltraWebTab.UltraWebTab, ByVal lblNames As Web.UI.WebControls.Label, ByVal lblRealNames As Web.UI.WebControls.Label, ByVal StrMode As String, ByVal ObjDS As DataSet, ByVal strFormName As String)
        Try
            Dim uwtTab As Infragistics.WebUI.UltraWebTab.Tab
            Dim IntGroupId As Integer
            Dim IntTempGroupId As Integer
            Dim StrGroupName As String = String.Empty
            Dim StrGroupKey As String = String.Empty
            Dim ClsObjectNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
            dsDataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "Sys_GetOtherFieldsData", IntObjectId)

            '[0261]===============================[Begin]
            'Dim tabTemp As New Infragistics.WebUI.UltraWebTab.Tab
            'tabTemp = uwtTabControl.Tabs.GetTab(0)
            'tabTemp.Style.BackColor = Drawing.Color.LightSteelBlue
            'uwtTabControl.Tabs.Clear()
            'uwtTabControl.Tabs.Add(tabTemp)
            '[0261]===============================[End]
            '[0256]===============================[Begin]
            Dim IntTemp As Integer
            Dim tabTAB As Infragistics.WebUI.UltraWebTab.Tab
            If mPage.IsPostBack Then
                Do
                    tabTAB = uwtTabControl.Tabs(IntTemp)
                    If Not tabTAB.Key Is Nothing Then
                        If tabTAB.Key.Substring(0, 3) = "OF_" Then
                            uwtTabControl.Tabs.Remove(tabTAB)
                        Else
                            IntTemp += 1
                        End If
                    Else
                        IntTemp += 1
                    End If

                Loop While uwtTabControl.Tabs.Count - IntTemp <> 0
            End If
            '[0256]===============================[End]

            If dsDataSet.Tables(0).Rows.Count > 0 Then
                For Each drRow As DataRow In dsDataSet.Tables(0).Rows
                    IntTempGroupId = drRow.Item("OtherFieldGroupID")
                    If IntGroupId <> IntTempGroupId Then
                        StrGroupName = Venus.Shared.DataHandler.DataValue_Out(drRow.Item(ClsObjectNavigationHandler.SetLanguage(mPage, "EngGroupName/ArbGroupName")), SqlDbType.VarChar)
                        StrGroupKey = "OF_" & drRow.Item("EngGroupName").ToString.Replace(" ", "_")
                        uwtTab = New Infragistics.WebUI.UltraWebTab.Tab(StrGroupName)
                        IntGroupId = IntTempGroupId
                        uwtTab.Key = StrGroupKey
                        'uwtTab.Style.BackColor = Drawing.Color.LightSteelBlue
                        uwtTabControl.Tabs.Add(uwtTab)
                        ReadNwriteOtherFieldsValues(IntObjectId, IntTempGroupId, uwtTab, lblNames, lblRealNames, ClsObjectNavigationHandler.SetLanguage(mPage, "en-US/ar-EG"), StrGroupKey, strFormName, ObjDS, StrMode)
                    End If
                Next
            End If
            If lblNames.Text <> "" Then
                lblNames.Text = lblNames.Text.Substring(1)
                lblRealNames.Text = lblRealNames.Text.Substring(1)
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try


    End Function




    '=========================================================================
    ' Mtethod Name : CollectDataAndSave
    ' Date Created : 08-08-2007 
    ' Developer    : [0256] Abdul elJaleel R. Ossman 
    ' Description  : This Function Recives tablename and recordid of the screen being manipulated by user 
    '              : and prepare the insert ot update SQL statments from a string holded in textbox object 
    '              : that textbox is filled from JS function SaveOtherFieldsData()
    '=========================================================================
    Public Sub CollectDataAndSave(ByVal strValues As String, ByVal TableName As String, ByVal RecordID As Integer)

        Dim clsOtherFieldsData As New clsSys_OtherFieldsData(mPage)
        If Len(strValues) > 0 Then
            Dim strArr As String() = strValues.Split("@")
            Dim i As Integer
            For i = 0 To strArr.Length - 1
                If i > 0 Then
                    Dim ParmStrArr() As String = strArr(i).Split("%")
                    If ParmStrArr.Length > 0 Then 'Nglect  "" "" 
                        Dim OfID As Integer = ParmStrArr(0)
                        Dim OfData As String = ParmStrArr(1)
                        clsOtherFieldsData.Find(" OtherFieldID = " & OfID & " And RecordID = " & RecordID)
                        AssignValues(clsOtherFieldsData, RecordID, OfID, OfData)
                        If clsOtherFieldsData.ID > 0 Then
                            clsOtherFieldsData.Update(" ID = " & clsOtherFieldsData.ID)
                        Else
                            clsOtherFieldsData.Save()
                        End If
                End If
                End If
            Next
        End If
    End Sub

    '============================================================================
    'Method Name  : AssignValues()
    'Developer    : [0256]
    'Date Created : 08-08-2007 
    'Description  : Assigns values to BO Layer clssys_OtherFieldsData Class
    '============================================================================
    Public Function AssignValues(ByRef clsOtherFieldsData As clsSys_OtherFieldsData, ByVal RecordID As Integer, ByVal OtherFieldID As Integer, ByVal Data As String) As Boolean
        With clsOtherFieldsData
            .RecordID = RecordID
            .OtherFielID = OtherFieldID
            .FieldData = Data
        End With
    End Function


#End Region

End Class
