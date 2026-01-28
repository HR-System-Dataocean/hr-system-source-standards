Public Class ClsSys_Search
    Inherits ClsDataAcessLayer

    Public Sub New(ByVal page As Web.UI.Page)
        MyBase.New(page)
    End Sub

    Private mStrSearchName As String
    Private mIntID As Integer
    Private mObjSearchViews As ClsSys_SearchViews
    Private mObjSearchViewsColumns As ClsSys_SearchViewsColumns

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

    Public Function ReadSearchName(ByVal SearchID As Integer) As Boolean
        mObjSearchViews = New ClsSys_SearchViews(mPage)
        Try
            mObjSearchViews.Find("ID=" & SearchID)
            SearchName = mObjSearchViews.EngDescription
            SearchID = mObjSearchViews.ID
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Function ReadSearchParameter(ByVal ptrSearchID As Integer, _
    ByRef Div1 As Web.UI.HtmlControls.HtmlGenericControl, _
    ByRef Div2 As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, _
    ByRef div3 As Web.UI.HtmlControls.HtmlTable, _
    ByVal ParameterNames As Web.UI.WebControls.Label, ByVal ParameterRealName As Web.UI.WebControls.Label) As Boolean
        mObjSearchViewsColumns = New ClsSys_SearchViewsColumns(mPage)
        Dim ObjDr As DataRow
        Dim ValueCounter As Integer
        Dim str As String
        Dim litral As New Web.UI.LiteralControl
        Dim Y_Pos As Integer = 80
        Dim X_Pos As Integer = 40
        Dim Width As Integer = 240
        Dim CurrentTextBox As String
        ParameterNames.Text = ""
        ParameterRealName.Text = ""
        Dim Arr() As String


        Try
            If mObjSearchViewsColumns.Find("SearchViewID=" & ptrSearchID & " And IsAvailable=1 and IsCriteria=1 Order by Rank") Then
                ReDim Arr(mObjSearchViewsColumns.DataSet.Tables(0).Rows.Count)
                For Each ObjDr In mObjSearchViewsColumns.DataSet.Tables(0).Rows

                    'CHECK ABOUT SEARCH FIELD VIA LETRAL ================

                    CurrentTextBox = ""
                    Select Case ObjDr.Item("ColumnType")

                        Case "varchar"
                            Dim ASPTXT As New Web.UI.WebControls.TextBox
                            ASPTXT.ID = "WV_" & ObjDr.Item("ColumnName")
                            CurrentTextBox = ObjDr.Item("ColumnName")
                            ASPTXT.Style.Item("POSITION") = " absolute"
                            ASPTXT.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                            ASPTXT.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                            ASPTXT.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPTXT.BackColor = Drawing.Color.Silver

                            ASPTXT.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPTXT.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPTXT.BorderColor = Drawing.Color.White
                            ASPTXT.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("ColumnLength"), SqlDbType.Int)
                            If mDataHandler.DataValue_Out(ObjDr.Item("ColumnLanguage"), SqlDbType.Bit) Then
                                Venus.Shared.Web.ClientSideActions.SetLanguage(mPage, ASPTXT, [Shared].Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                            End If
                            ParameterNames.Text &= "|" & "WV_" & ObjDr.Item("ColumnName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ColumnName")
                            Arr(ValueCounter) = "WV_" & ObjDr.Item("ColumnName")
                            If ValueCounter = 0 Then
                                Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPTXT, False)
                            End If
                            Div1.Controls.Add(ASPTXT)

                        Case "int", "money", "tinyint"
                            Dim ASPWN As New Web.UI.WebControls.TextBox
                            ASPWN.ID = "WN_" & ObjDr.Item("ColumnName")
                            CurrentTextBox = "WN_" & ObjDr.Item("ColumnName")
                            ASPWN.Style.Item("POSITION") = " absolute"
                            ASPWN.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                            ASPWN.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                            ASPWN.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPWN.BackColor = Drawing.Color.Silver

                            ASPWN.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWN.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWN.BorderColor = Drawing.Color.White
                            ASPWN.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("ColumnLength"), SqlDbType.Int)

                            ParameterNames.Text &= "|" & "WN_" & ObjDr.Item("ColumnName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ColumnName")
                            Venus.Shared.Web.ClientSideActions.SetupForNumberOnly(mPage, ASPWN, True)
                            Arr(ValueCounter) = "WN_" & ObjDr.Item("ColumnName")
                            If ValueCounter = 0 Then
                                Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWN, False)
                            End If
                            Div1.Controls.Add(ASPWN)

                        Case "smalldatetime"
                            Dim ASPWC As New Web.UI.WebControls.TextBox
                            ASPWC.ID = "WD_" & ObjDr.Item("ColumnName")
                            CurrentTextBox = "WD_" & ObjDr.Item("ColumnName")
                            ASPWC.Style.Item("POSITION") = " absolute"
                            ASPWC.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                            ASPWC.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                            ASPWC.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPWC.BackColor = Drawing.Color.Silver

                            ASPWC.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWC.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWC.BorderColor = Drawing.Color.White

                            ParameterNames.Text &= "|" & "WD_" & ObjDr.Item("ColumnName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ColumnName")
                            Venus.Shared.Web.ClientSideActions.MaskedEdit(mPage, ASPWC, "##/##/####")
                            Arr(ValueCounter) = "WD_" & ObjDr.Item("ColumnName")
                            If ValueCounter = 0 Then
                                Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWC, False)
                            End If
                            Div1.Controls.Add(ASPWC)
                        Case "bit"

                            Dim ASPWB As New Web.UI.WebControls.DropDownList
                            Dim ASPItem1 As New Web.UI.WebControls.ListItem
                            Dim ASPItem2 As New Web.UI.WebControls.ListItem
                            ASPItem1.Text = "True"
                            ASPItem1.Value = 0
                            ASPItem2.Text = "False"
                            ASPItem2.Value = 1
                            ASPWB.Items.Add(ASPItem1)
                            ASPWB.Items.Add(ASPItem2)
                            ASPWB.ID = "WB_" & ObjDr.Item("ColumnName")
                            CurrentTextBox = "WB_" & ObjDr.Item("ColumnName")
                            ASPWB.Style.Item("POSITION") = " absolute"
                            ASPWB.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                            ASPWB.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                            ASPWB.Style.Item("WIDTH") = CStr(Width) & "px"
                            ASPWB.BackColor = Drawing.Color.Silver

                            ASPWB.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                            ASPWB.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                            ASPWB.BorderColor = Drawing.Color.White

                            ParameterNames.Text &= "|" & "WB_" & ObjDr.Item("ColumnName")
                            ParameterRealName.Text &= "|" & ObjDr.Item("ColumnName")
                            Arr(ValueCounter) = "WB_" & ObjDr.Item("ColumnName")
                            If ValueCounter = 0 Then
                                Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWB, False)
                            End If
                            Div1.Controls.Add(ASPWB)

                    End Select


                    'UPDATE BUTTON=========================================



                    If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then

                        Dim SubSearchID As String = ObjDr.Item("SubSearchID")
                        str = "<INPUT style='Z-INDEX: 103; LEFT: " & CStr(X_Pos + 256) & "px; POSITION: absolute; TOP: " & CStr(ValueCounter * 25 + Y_Pos) & "px; HEIGHT: 20px' type='button' value='...' id='" & "btn" & ObjDr.Item("Name") & "' onclick=" & """" & "SearchOpenWin('" & CurrentTextBox & "','" & SubSearchID & "');" & """" & ">"
                        litral = New Web.UI.LiteralControl(str)
                        Div1.Controls.Add(litral)

                    End If


                    'UPDATE PAGE STYLE=====================================

                    If Not mPage.IsPostBack Then

                        Dim DV2Top As Integer
                        Dim DV2TopStr As String
                        DV2TopStr = Div2.Style.Item("TOP")
                        DV2Top = CInt(DV2TopStr.Substring(0, DV2TopStr.Length - 2))
                        Div2.Style.Item("TOP") = DV2Top + 22 & "px"

                        Dim DV3Top As Integer
                        Dim DV3TopStr As String
                        DV3TopStr = div3.Style.Item("TOP")
                        DV3Top = CInt(DV3TopStr.Substring(0, DV3TopStr.Length - 2))
                        div3.Style.Item("TOP") = DV3Top + 22 & "px"

                        Dim DV1Hight As Integer
                        Dim DV1HightStr As String

                        DV1HightStr = Div1.Style.Item("HEIGHT")
                        DV1Hight = CInt(DV1HightStr.Substring(0, DV1HightStr.Length - 2))

                        Div1.Style.Item("HEIGHT") = DV1Hight + 20 & "px"

                    End If

                    'RENDER NAMEING==============================

                    str = "<DIV style='DISPLAY: inline; font-size: small; Z-INDEX: 102; LEFT: " & CStr(X_Pos) & "px; WIDTH: 123px;border-right: white 1px outset; border-top: white 1px outset; border-left: white 1px outset; border-bottom: white 1px outset; POSITION: absolute; TOP: " & CStr(ValueCounter * 25 + Y_Pos) & "px; HEIGHT: 20px' ms_positioning='FlowLayout'>" & ObjDr.Item("ColumnEngDescription") & "</DIV>"
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

    Public Function ViewData(ByVal PtrSearchID As Integer, ByVal Where As String, ByRef Ds As DataSet) As Boolean
        Dim ObjDr As DataRow
        Dim SqlSelectCommandBegin As String = String.Empty
        Dim SqlSelectCommandCriteria As String = String.Empty
        Dim SqlSelectCommandTable As String = String.Empty
        Dim SqlSelectCommandWhere As String = Where
        Dim SqlSelectCommand As String = String.Empty
        Dim StrAlis As String = String.Empty

        mObjSearchViewsColumns = New ClsSys_SearchViewsColumns(mPage)
        mObjSearchViews = New ClsSys_SearchViews(mPage)

        Try
            mObjSearchViews.Find("ID=" & PtrSearchID)

            If mObjSearchViewsColumns.Find("SearchViewID=" & PtrSearchID & " And IsAvailable=1 and IsCriteria=0") Then

                SqlSelectCommandBegin = "Select "
                For Each ObjDr In mObjSearchViewsColumns.DataSet.Tables(0).Rows
                    StrAlis = IIf(ObjDr.Item("ColumnEngDescription") Is DBNull.Value, ObjDr.Item("ColumnName"), ObjDr.Item("ColumnEngDescription"))

                    SqlSelectCommandCriteria &= "," & ObjDr.Item("ColumnName") & " As [" & StrAlis & "]"
                Next
                SqlSelectCommandTable = mObjSearchViews.TableName
                SqlSelectCommand = SqlSelectCommandBegin & SqlSelectCommandCriteria.Substring(1) & " From " & SqlSelectCommandTable & IIf(Len(SqlSelectCommandWhere) > 1, " Where ", "") & SqlSelectCommandWhere

                mSqlDataAdapter = New SqlClient.SqlDataAdapter(SqlSelectCommand, mConnectionString)
                mSqlDataAdapter.Fill(Ds)
                If mDataHandler.CheckValidDataObject(Ds) Then
                    Return True
                End If

            End If

        Catch ex As Exception

        End Try
    End Function


End Class
