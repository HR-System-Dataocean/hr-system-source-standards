'======================================================================
'Project name  : Venus V. 
'Program name  : ClsSys_Reports_Main
'Date Created  : 
'Issue #       :       
'Developer     : [0256]
'Description   : This Class acts as the OG Layer for frmReportViewer
'              : This Class Recives the ReportID 
'Developer     : [0256]-[0257]
'              : 
'Modifacations :
'======================================================================
Imports System.Data
Imports System.Drawing
Imports System.Reflection
Imports System.Collections.Generic
Imports Drawing.Text
Imports System
Imports System.Drawing.Text

Public Class ClsRpw_ReportsMain
    Inherits ClsDataAcessLayer

#Region "Constants"

    Const csFontSizeDefault = 12
#End Region

#Region "Private Members"
    Private mStrReportName As String
    Private mReportID As Integer

    Private mObjReports As ClsRpw_ReportsProperties
    Private mObjReportsCriteria As ClsRpw_ReportsCriterias
    Private mObjReportsColumns As ClsRpw_ReportColumnsProperties

#End Region

#Region "Class Constructors"
    Public Sub New(ByVal page As Web.UI.Page)
        MyBase.New(page)
    End Sub
#End Region

#Region "Public Property"

    Public Property ReportName() As String
        Get
            Return mStrReportName
        End Get
        Set(ByVal value As String)
            mStrReportName = value
        End Set
    End Property

    Public Property ReportID() As Integer
        Get
            Return mReportID
        End Get
        Set(ByVal value As Integer)
            mReportID = value
        End Set
    End Property


#End Region

#Region "Public Methods"
    Public Function SetHorizontalAlignment(ByVal AlignType As Web.UI.WebControls.HorizontalAlign) As Int32
        If IsNothing(AlignType) Then Return 0
        Select Case AlignType
            Case Web.UI.WebControls.HorizontalAlign.NotSet
                Return 0
            Case Web.UI.WebControls.HorizontalAlign.Left
                Return 1
            Case Web.UI.WebControls.HorizontalAlign.Center
                Return 2
            Case Web.UI.WebControls.HorizontalAlign.Right
                Return 3
            Case Web.UI.WebControls.HorizontalAlign.Justify
                Return 4
        End Select
    End Function

    Public Function SetVerticalAlignment(ByVal AlignType As Web.UI.WebControls.VerticalAlign) As Int32
        If IsNothing(AlignType) Then Return 0
        Select Case AlignType
            Case Web.UI.WebControls.VerticalAlign.NotSet
                Return 0
            Case Web.UI.WebControls.VerticalAlign.Top
                Return 1
            Case Web.UI.WebControls.VerticalAlign.Middle
                Return 2
            Case Web.UI.WebControls.VerticalAlign.Bottom
                Return 3
        End Select
    End Function

    Public Function SetVerticalAlignmentString(ByVal StrAlignType As String) As Int32
        If IsNothing(StrAlignType) Then Return 0
        Select Case StrAlignType.ToUpper
            Case "NOTSET", "0"
                Return 0
            Case "TOP", "1"
                Return 1
            Case "MIDDLE", "2"
                Return 2
            Case "BOTTOM", "3"
                Return 3
            Case Else
                Return 0
        End Select
    End Function

    Public Function SetHorizontalAlignmentString(ByVal StrAlignType As String) As Int32
        If IsNothing(StrAlignType) Then Return 0
        Select Case StrAlignType.ToUpper
            Case "NOTSET", "0"
                Return 0
            Case "LEFT", "1"
                Return 1
            Case "CENTER", "2"
                Return 2
            Case "RIGHT", "3"
                Return 3
            Case "JUSTIFY", "4"
                Return 4
            Case Else
                Return 0
        End Select
    End Function


    Public Function GetBorderStyle(ByVal StrStyleName As String) As String
        If IsNothing(StrStyleName) Then Return "none"
        Select Case "StrStyleName"
            Case "0"
                Return "none"
            Case "1"
                Return "none"
            Case "2"
                Return "dotted"
            Case "3"
                Return "dashed"
            Case "4"
                Return "solid"
            Case "5"
                Return "double"
            Case "6"
                Return "groove"
            Case "7"
                Return "ridge"
            Case "8"
                Return "inset"
            Case "9"
                Return "outset"
            Case Else
                Return "none"
        End Select
    End Function

    Public Function SetBorderStyle(ByVal ObjBorderType As String) As Int32
        If IsNothing(ObjBorderType) Then Return 0
        Select Case ObjBorderType.ToUpper
            Case "NOTSET".ToString.ToUpper
                Return 0
            Case "NONE".ToString.ToUpper
                Return 1
            Case "DOTTED".ToString.ToUpper
                Return 2
            Case "Dashed".ToString.ToUpper
                Return 3
            Case "Solid".ToString.ToUpper
                Return 4
            Case "Double".ToString.ToUpper
                Return 5
            Case "Groove".ToString.ToUpper
                Return 6
            Case "Ridge".ToString.ToUpper
                Return 7
            Case "Inset".ToString.ToUpper
                Return 8
            Case "Outset".ToString.ToUpper
                Return 9
            Case "DASHED DOT"
                Return 4
            Case "DASHED DOT DOT"
                Return 5
            Case Else
                Return 0
        End Select
    End Function


    Public Function GetColorsDropDownList(ByVal DdlColors As Global.System.Web.UI.WebControls.DropDownList, ByVal blDefaultIsBlack As Boolean) As Boolean
        Try
            DdlColors.DataSource = finalColorList()
            DdlColors.DataBind()
            MainpulateColor(DdlColors, blDefaultIsBlack)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    Private Sub MainpulateColor(ByRef ddl As Global.System.Web.UI.WebControls.DropDownList, ByVal blDefaultBlack As Boolean)
        Dim index As Integer
        Dim IntTemp As Integer
        Dim IntColorDefault As Integer
        Dim StrDefaultColor As String
        If blDefaultBlack Then
            StrDefaultColor = "Black"
        Else
            StrDefaultColor = "White"
        End If

        For index = 0 To ddl.Items.Count - 1
            If ddl.Items(index).Text = "Transparent" Then
                IntTemp = index
                Continue For
            ElseIf ddl.Items(index).Text = StrDefaultColor Then
                IntColorDefault = index
                Continue For
            End If
            ddl.Items(index).Attributes.Add("style", "background-color:" & ddl.Items(index).Value)
        Next
        If IntTemp >= 0 Then ddl.Items.RemoveAt(IntTemp)
        ddl.Items.Insert(0, StrDefaultColor)
        ddl.Items.RemoveAt(IntColorDefault)
        ddl.Items(0).Attributes.Add("style", "background-color:White")
        ddl.BackColor = Color.FromName(ddl.SelectedItem.Text)
    End Sub

    Public Function GetKeysValues(ByVal StrToSearch As String, ByVal StrKey As String) As String
        Dim StrString As String = String.Empty
        Dim IntLocation As Integer = -1
        Dim DblLenght As Integer
        Dim StrRightPart As String
        Dim StrFinalResult As String
        Dim IntNextSeparator As String

        StrString = StrKey & "="
        IntLocation = StrToSearch.IndexOf(StrString)
        DblLenght = StrString.Length

        If IntLocation < 0 Then
            Return ""
        End If

        StrRightPart = StrToSearch.Substring(IntLocation + DblLenght)
        IntNextSeparator = StrRightPart.ToString.IndexOf(";")
        If IntNextSeparator >= 0 Then
            StrRightPart = StrRightPart.ToString.Substring(0, IntNextSeparator)
        End If

        StrFinalResult = StrRightPart

        Return StrFinalResult
    End Function

    Public Function GetKeysValues(ByVal arrList As ArrayList, ByVal StrKey As String) As String
        Dim StrString As String = String.Empty
        Dim IntLocation As Integer = -1
        StrString = StrKey
        IntLocation = arrList.IndexOf(StrString)
        If IntLocation >= 0 Then
            Return arrList(IntLocation + 1)
        Else
            Return Nothing
        End If

    End Function

    Public Function ReadGridColumnsData(ByVal StrColumnKeys As String, ByRef dtColumnsProperties As DataTable, ByVal uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid) As Integer

        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim drColDataRow As DataRow
        Dim IntRank As Integer
        Dim arrListData As New ArrayList
        Dim StrColumnName As String = String.Empty
        Dim StrTemp As String
        Dim intRowHeight As Integer
        PrepareColumnsData(StrColumnKeys, arrListData)
        Try
            For Each ugcGridColumn As Infragistics.WebUI.UltraWebGrid.UltraGridColumn In uwgGrid.Columns
                With uwgGrid
                    StrColumnName = uwgGrid.Columns(IntRank).BaseColumnName
                    If Not IsNothing(dtColumnsProperties.Select(" FieldName ='" & StrColumnName & "'")(0)) Then
                        drColDataRow = dtColumnsProperties.Select(" FieldName ='" & StrColumnName & "'")(0)
                    Else
                        Continue For
                    End If
                    intRowHeight = GetKeysValues(arrListData, StrColumnName & "_CCH")
                    drColDataRow.Item("Rank") = IntRank
                    drColDataRow.Item("BorderColor") = GetKeysValues(arrListData, StrColumnName & "_CCBC")
                    drColDataRow.Item("TopBorderColor") = GetKeysValues(arrListData, StrColumnName & "_CCBCT")
                    drColDataRow.Item("BottomBorderColor") = GetKeysValues(arrListData, StrColumnName & "_CCBCB")
                    drColDataRow.Item("LeftBorderColor") = GetKeysValues(arrListData, StrColumnName & "_CCBCL")
                    drColDataRow.Item("RightBorderColor") = GetKeysValues(arrListData, StrColumnName & "_CCBCR")
                    drColDataRow.Item("BorderStyle") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CCBS"))
                    drColDataRow.Item("TopBorderStyle") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CCBST"))
                    drColDataRow.Item("BottomBorderStyle") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CCBSB"))
                    drColDataRow.Item("LeftBorderStyle") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CCBSL"))
                    drColDataRow.Item("RightBorderStyle") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CCBSR"))

                    drColDataRow.Item("BorderWidth") = Val(GetKeysValues(arrListData, StrColumnName & "_CCBW"))
                    drColDataRow.Item("TopBorderWidth") = Val(GetKeysValues(arrListData, StrColumnName & "_CCBWT"))
                    drColDataRow.Item("BottomBorderWidth") = Val(GetKeysValues(arrListData, StrColumnName & "_CCBWB"))
                    drColDataRow.Item("LeftBorderWidth") = Val(GetKeysValues(arrListData, StrColumnName & "_CCBWL"))
                    drColDataRow.Item("RightBorderWidth") = Val(GetKeysValues(arrListData, StrColumnName & "_CCBWR"))

                    drColDataRow.Item("EngDescription") = GetKeysValues(arrListData, StrColumnName & "_CHTXT")
                    drColDataRow.Item("EngFooterCaption") = GetKeysValues(arrListData, StrColumnName & "_CFTXT")
                    StrTemp = GetKeysValues(arrListData, StrColumnName & "_CCW")
                    drColDataRow.Item("ColumnWidth") = IIf(StrTemp = "" Or StrTemp = "0", 100, StrTemp)
                    drColDataRow.Item("ColumnHalignment") = SetHorizontalAlignmentString(GetKeysValues(arrListData, StrColumnName & "_CCHA"))
                    drColDataRow.Item("ColumnValignment") = SetVerticalAlignmentString(GetKeysValues(arrListData, StrColumnName & "_CCVA"))
                    drColDataRow.Item("ColumnIsHidden") = False
                    drColDataRow.Item("ColumnForeColor") = GetKeysValues(arrListData, StrColumnName & "_CCFC")
                    drColDataRow.Item("ColumnBackColor") = GetKeysValues(arrListData, StrColumnName & "_CCBKC")
                    StrTemp = GetKeysValues(arrListData, StrColumnName & "_CCFN")
                    drColDataRow.Item("ColumnFont") = IIf(StrTemp = "", "Arail", StrTemp)
                    drColDataRow.Item("ColumnFontIsBold") = GetKeysValues(arrListData, StrColumnName & "_CCFIB")
                    drColDataRow.Item("ColumnFontIsItalic") = GetKeysValues(arrListData, StrColumnName & "_CCFII")
                    drColDataRow.Item("ColumnFontIsUnderLine") = GetKeysValues(arrListData, StrColumnName & "_CCFIU")
                    StrTemp = (Val(GetKeysValues(arrListData, StrColumnName & "_CCFS")))
                    drColDataRow.Item("ColumnFontSize") = IIf(StrTemp = "0", csFontSizeDefault, StrTemp)
                    drColDataRow.Item("TopPadding") = Val(GetKeysValues(arrListData, StrColumnName & "_CCP"))
                    drColDataRow.Item("BottomPadding") = Val(GetKeysValues(arrListData, StrColumnName & "_CCP"))
                    drColDataRow.Item("LeftPadding") = Val(GetKeysValues(arrListData, StrColumnName & "_CCP"))
                    drColDataRow.Item("RightPadding") = Val(GetKeysValues(arrListData, StrColumnName & "_CCP"))
                    drColDataRow.Item("TopMargin") = Val(GetKeysValues(arrListData, StrColumnName & "_CCM"))
                    drColDataRow.Item("BottomMargin") = Val(GetKeysValues(arrListData, StrColumnName & "_CCM"))
                    drColDataRow.Item("LeftMargin") = Val(GetKeysValues(arrListData, StrColumnName & "_CCM"))
                    drColDataRow.Item("RightMargin") = Val(GetKeysValues(arrListData, StrColumnName & "_CCM"))
                    drColDataRow.Item("Format") = GetKeysValues(arrListData, StrColumnName & "_CFORMAT")
                    drColDataRow.Item("FooterTotal") = SetFooterTotal(GetKeysValues(arrListData, StrColumnName & "_CFTOTAL"))
                    drColDataRow.Item("IsSorted") = Val(GetKeysValues(arrListData, StrColumnName & "_CSORT"))                    'drColDataRow.Item("IsGroupBy") = 0 'GetGroupBy()
                    drColDataRow.Item("ColumnHeaderHalignment") = SetHorizontalAlignmentString(GetKeysValues(arrListData, StrColumnName & "_CHHA"))
                    drColDataRow.Item("ColumnHeaderValignment") = SetVerticalAlignmentString(GetKeysValues(arrListData, StrColumnName & "_CHVA"))
                    StrTemp = GetKeysValues(arrListData, StrColumnName & "_CHBKC")
                    drColDataRow.Item("ColHeaderBackColor") = IIf(StrTemp = "0" Or StrTemp = "", "White", StrTemp)
                    StrTemp = GetKeysValues(arrListData, StrColumnName & "_CHFC")
                    drColDataRow.Item("ColHeaderForeColor") = IIf(StrTemp = "0" Or StrTemp = "", "Black", StrTemp)
                    StrTemp = GetKeysValues(arrListData, StrColumnName & "_CHFN")
                    drColDataRow.Item("ColHeaderFont") = IIf(StrTemp = "", "Arail", StrTemp)
                    StrTemp = GetKeysValues(arrListData, StrColumnName & "_CHFIB")
                    drColDataRow.Item("ColHeaderFontIsBold") = IIf(StrTemp = "True", True, False)
                    StrTemp = GetKeysValues(arrListData, StrColumnName & "_CHFII")
                    drColDataRow.Item("ColHeaderFontIsItalic") = IIf(StrTemp = "True", True, False)
                    GetKeysValues(arrListData, StrColumnName & "_CHFIU")
                    drColDataRow.Item("ColHeaderFontIsUnderline") = IIf(StrTemp = "True", True, False)
                    StrTemp = (Val(GetKeysValues(arrListData, StrColumnName & "_CHFS")))
                    drColDataRow.Item("ColHeaderFontSize") = IIf(StrTemp = "0", csFontSizeDefault, StrTemp)
                    drColDataRow.Item("ColHeaderBorderWidth") = Val(GetKeysValues(arrListData, StrColumnName & "_CHBW"))
                    drColDataRow.Item("ColHeaderBorderWidthTop") = Val(GetKeysValues(arrListData, StrColumnName & "_CHBWT"))
                    drColDataRow.Item("ColHeaderBorderWidthBottom") = Val(GetKeysValues(arrListData, StrColumnName & "_CHBWB"))
                    drColDataRow.Item("ColHeaderBorderWidthLeft") = Val(GetKeysValues(arrListData, StrColumnName & "_CHBWL"))
                    drColDataRow.Item("ColHeaderBorderWidthRight") = Val(GetKeysValues(arrListData, StrColumnName & "_CHBWR"))
                    drColDataRow.Item("ColHeaderBorderStyle") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CHBS"))
                    drColDataRow.Item("ColHeaderBorderStyleTop") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CHBST"))
                    drColDataRow.Item("ColHeaderBorderStyleBottom") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CHBSB"))
                    drColDataRow.Item("ColHeaderBorderStyleRight") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CHBSR"))
                    drColDataRow.Item("ColHeaderBorderStyleLeft") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CHBSL"))
                    drColDataRow.Item("ColHeaderBorderColor") = GetKeysValues(arrListData, StrColumnName & "_CHBC")
                    drColDataRow.Item("ColHeaderBorderColorTop") = GetKeysValues(arrListData, StrColumnName & "_CHBCT")
                    drColDataRow.Item("ColHeaderBorderColorBottom") = GetKeysValues(arrListData, StrColumnName & "_CHBCB")
                    drColDataRow.Item("ColHeaderBorderColorLeft") = GetKeysValues(arrListData, StrColumnName & "_CHBCL")
                    drColDataRow.Item("ColHeaderBorderColorRight") = GetKeysValues(arrListData, StrColumnName & "_CHBCR")
                    drColDataRow.Item("ColHeaderPaddingLeft") = Val(GetKeysValues(arrListData, StrColumnName & "_CHP"))
                    drColDataRow.Item("ColHeaderPaddingRight") = Val(GetKeysValues(arrListData, StrColumnName & "_CHP"))
                    drColDataRow.Item("ColHeaderPaddingTop") = Val(GetKeysValues(arrListData, StrColumnName & "_CHVP"))
                    drColDataRow.Item("ColHeaderPaddingBottom") = Val(GetKeysValues(arrListData, StrColumnName & "_CHP"))

                    drColDataRow.Item("ColFooterBackColor") = GetKeysValues(arrListData, StrColumnName & "_CFBKC")
                    drColDataRow.Item("ColFooterForeColor") = GetKeysValues(arrListData, StrColumnName & "_CFFC")
                    StrTemp = GetKeysValues(arrListData, StrColumnName & "_CFFN")
                    drColDataRow.Item("ColFooterFont") = IIf(StrTemp = "", "Arail", StrTemp)
                    drColDataRow.Item("ColFooterFontIsBold") = GetKeysValues(arrListData, StrColumnName & "_CFFIB")
                    drColDataRow.Item("ColFooterFontIsItalic") = GetKeysValues(arrListData, StrColumnName & "_CFFII")
                    drColDataRow.Item("ColFooterFontIsUnderline") = GetKeysValues(arrListData, StrColumnName & "_CFFIU")
                    StrTemp = (Val(GetKeysValues(arrListData, StrColumnName & "_CFFS")))
                    drColDataRow.Item("ColFooterFontSize") = IIf(StrTemp = "0", csFontSizeDefault, StrTemp)
                    drColDataRow.Item("ColFooterBorderWidth") = Val(GetKeysValues(arrListData, StrColumnName & "_CFBW"))
                    drColDataRow.Item("ColFooterBorderWidthTop") = Val(GetKeysValues(arrListData, StrColumnName & "_CFBWT"))
                    drColDataRow.Item("ColFooterBorderWidthBottom") = Val(GetKeysValues(arrListData, StrColumnName & "_CFBWB"))
                    drColDataRow.Item("ColFooterBorderWidthLeft") = Val(GetKeysValues(arrListData, StrColumnName & "_CFBWL"))
                    drColDataRow.Item("ColFooterBorderWidthRight") = Val(GetKeysValues(arrListData, StrColumnName & "_CFBWR"))
                    drColDataRow.Item("ColFooterBorderStyle") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CFBS"))
                    drColDataRow.Item("ColFooterBorderStyleTop") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CFBST"))
                    drColDataRow.Item("ColFooterBorderStyleBottom") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CFBSB"))
                    drColDataRow.Item("ColFooterBorderStyleRight") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CFBSR"))
                    drColDataRow.Item("ColFooterBorderStyleLeft") = SetBorderStyle(GetKeysValues(arrListData, StrColumnName & "_CFBSL"))
                    drColDataRow.Item("ColFooterBorderColor") = GetKeysValues(arrListData, StrColumnName & "_CFBC")
                    drColDataRow.Item("ColFooterBorderColorTop") = GetKeysValues(arrListData, StrColumnName & "_CFBCT")
                    drColDataRow.Item("ColFooterBorderColorBottom") = GetKeysValues(arrListData, StrColumnName & "_CFBCB")
                    drColDataRow.Item("ColFooterBorderColorLeft") = GetKeysValues(arrListData, StrColumnName & "_CFBCL")
                    drColDataRow.Item("ColFooterBorderColorRight") = GetKeysValues(arrListData, StrColumnName & "_CFBCR")
                    drColDataRow.Item("ColFooterPaddingLeft") = Val(GetKeysValues(arrListData, StrColumnName & "_CFP"))
                    drColDataRow.Item("ColFooterPaddingRight") = Val(GetKeysValues(arrListData, StrColumnName & "_CFP"))
                    drColDataRow.Item("ColFooterPaddingTop") = Val(GetKeysValues(arrListData, StrColumnName & "_CFVP"))
                    drColDataRow.Item("ColFooterPaddingBottom") = Val(GetKeysValues(arrListData, StrColumnName & "_CFP"))
                    drColDataRow.Item("ColumnFooterHalignment") = SetHorizontalAlignmentString(GetKeysValues(arrListData, StrColumnName & "_CFHA"))
                    drColDataRow.Item("ColumnFooterValignment") = SetVerticalAlignmentString(GetKeysValues(arrListData, StrColumnName & "_CFVA"))

                End With
                IntRank += 1
            Next
        Catch ex As Exception
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(mPage, ex.Message)
        End Try
        If intRowHeight > 0 Then
            Return intRowHeight
        Else
            Return 25
        End If
    End Function

    Public Function PrepareColumnsData(ByVal StrColumnData As String, ByRef arrListData As ArrayList) As Boolean
        Dim IntCounter As Integer
        Dim StrTempArray() As String = StrColumnData.Split(";")
        Dim IntTemp As Integer
        Try
            For IntCounter = 0 To StrTempArray.GetUpperBound(0)
                IntTemp = StrTempArray(IntCounter).IndexOf("=")
                arrListData.Add(StrTempArray(IntCounter).Substring(0, IntTemp))
                arrListData.Add(StrTempArray(IntCounter).Substring(IntTemp + 1))
            Next
        Catch ex As Exception
        End Try
    End Function

    Public Function GetColorsDropDownList(ByVal DdlColors As Global.System.Web.UI.WebControls.DropDownList) As Boolean
        Try
            DdlColors.DataSource = finalColorList()
            DdlColors.DataBind()
            MainpulateColor(DdlColors)
        Catch ex As Exception

        Finally

        End Try
    End Function

    Public Function finalColorList() As List(Of String)
        Dim allColors() As String = Global.System.Enum.GetNames(GetType(Drawing.KnownColor))
        Dim systemEnvironmentColors((GetType(Drawing.SystemColors)).GetProperties().Length) As String
        Dim index As Integer = 0
        Dim fCList As New List(Of String)
        For Each member As MemberInfo In ((GetType(Drawing.SystemColors)).GetProperties())
            systemEnvironmentColors(index) = member.Name
            index = index + 1
        Next
        For Each color As String In allColors
            If (Array.IndexOf(systemEnvironmentColors, color) < 0) Then
                fCList.Add(color)
            End If
        Next
        Return fCList
    End Function

    Public Sub MainpulateColor(ByRef ddl As Global.System.Web.UI.WebControls.DropDownList)
        Dim index As Integer
        For index = 0 To ddl.Items.Count - 1
            ddl.Items(index).Attributes.Add("style", "background-color:" & ddl.Items(index).Value)
        Next
        ddl.BackColor = Color.FromName(ddl.SelectedItem.Text)
    End Sub

    Public Function ManipulateColorsNames(ByVal StrColorName As String, ByVal blForeColor As Boolean) As String
        Const CsWhite = "White"
        Const CsBlack = "Black"
        Select Case StrColorName.ToString
            Case "0"
                Return IIf(blForeColor, CsBlack, CsWhite)
            Case ""
                Return IIf(blForeColor, CsBlack, CsWhite)
            Case "NotSet"
                Return IIf(blForeColor, CsBlack, CsWhite)
            Case Else
                Return StrColorName
        End Select
    End Function

    Public Function SetGridColumnsData(ByVal dtColumnsProperties As DataTable, ByVal uwgViewColumns As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, Optional ByVal intRowHeight As Integer = 25) As String

        Dim StrColId As String
        Dim StrStyleCode As String = String.Empty
        Try
            For Each drColDataRow As DataRow In dtColumnsProperties.Rows
                StrColId = drColDataRow.Item("FieldName")
                With drColDataRow

                    StrStyleCode &= ";" & StrColId & "_CCBC=" & .Item("BorderColor")
                    StrStyleCode &= ";" & StrColId & "_CCBCT=" & .Item("TopBorderColor")
                    StrStyleCode &= ";" & StrColId & "_CCBCB=" & .Item("BottomBorderColor")
                    StrStyleCode &= ";" & StrColId & "_CCBCL=" & .Item("LeftBorderColor")
                    StrStyleCode &= ";" & StrColId & "_CCBCR=" & .Item("RightBorderColor")


                    StrStyleCode &= ";" & StrColId & "_CCBS=" & GetBorderStyle(IIf(.Item("BorderStyle") Is DBNull.Value, "0", .Item("BorderStyle")))
                    StrStyleCode &= ";" & StrColId & "_CCBST=" & GetBorderStyle(IIf(.Item("TopBorderStyle") Is DBNull.Value, "0", .Item("TopBorderStyle")))
                    StrStyleCode &= ";" & StrColId & "_CCBSB=" & GetBorderStyle(IIf(.Item("BottomBorderStyle") Is DBNull.Value, "0", .Item("BottomBorderStyle")))
                    StrStyleCode &= ";" & StrColId & "_CCBSL=" & GetBorderStyle(IIf(.Item("LeftBorderStyle") Is DBNull.Value, "0", .Item("LeftBorderStyle")))
                    StrStyleCode &= ";" & StrColId & "_CCBSR=" & GetBorderStyle(IIf(.Item("RightBorderStyle") Is DBNull.Value, "0", .Item("RightBorderStyle")))

                    StrStyleCode &= ";" & StrColId & "_CCBW=" & .Item("TopBorderWidth")
                    StrStyleCode &= ";" & StrColId & "_CCBWT=" & .Item("TopBorderWidth")
                    StrStyleCode &= ";" & StrColId & "_CCBWB=" & .Item("BottomBorderWidth")
                    StrStyleCode &= ";" & StrColId & "_CCBWR=" & .Item("RightBorderWidth")
                    StrStyleCode &= ";" & StrColId & "_CCBWL=" & .Item("LeftBorderWidth")

                    StrStyleCode &= ";" & StrColId & "_CCFC=" & .Item("ColumnForeColor")
                    StrStyleCode &= ";" & StrColId & "_CCBKC=" & .Item("ColumnBackColor")
                    StrStyleCode &= ";" & StrColId & "_CCFN=" & .Item("ColumnFont")
                    StrStyleCode &= ";" & StrColId & "_CCFS=" & .Item("ColumnFontSize")

                    StrStyleCode &= ";" & StrColId & "_CCFII=" & IIf(IsDBNull(.Item("ColumnFontIsItalic")), False, .Item("ColumnFontIsItalic"))
                    StrStyleCode &= ";" & StrColId & "_CCFIU=" & IIf(IsDBNull(.Item("ColumnFontIsUnderLine")), False, .Item("ColumnFontIsUnderLine"))
                    StrStyleCode &= ";" & StrColId & "_CCFIB=" & IIf(IsDBNull(.Item("ColumnFontIsBold")), False, .Item("ColumnFontIsBold"))


                    StrStyleCode &= ";" & StrColId & "_CCHA=" & GetHorizontalAlignment(IIf(.Item("ColumnHalignment") Is DBNull.Value, "", .Item("ColumnHalignment")))
                    StrStyleCode &= ";" & StrColId & "_CCVA=" & GetVerticalAlignment(IIf(.Item("ColumnValignment") Is DBNull.Value, "", .Item("ColumnValignment")))

                    StrStyleCode &= ";" & StrColId & "_CCP=" & .Item("TopPadding")
                    StrStyleCode &= ";" & StrColId & "_CCM=" & .Item("TopMargin")

                    StrStyleCode &= ";" & StrColId & "_CCW=" & .Item("ColumnWidth")
                    StrStyleCode &= ";" & StrColId & "_CCH=" & intRowHeight
                    'Header Style
                    StrStyleCode &= ";" & StrColId & "_CHBC=" & .Item("ColHeaderBorderColor")
                    StrStyleCode &= ";" & StrColId & "_CHBCT=" & .Item("ColHeaderBorderColorTop")
                    StrStyleCode &= ";" & StrColId & "_CHBCB=" & .Item("ColHeaderBorderColorBottom")
                    StrStyleCode &= ";" & StrColId & "_CHBCL=" & .Item("ColHeaderBorderColorLeft")
                    StrStyleCode &= ";" & StrColId & "_CHBCR=" & .Item("ColHeaderBorderColorRight")


                    StrStyleCode &= ";" & StrColId & "_CHBS=" & GetBorderStyle(IIf(.Item("ColHeaderBorderStyle") Is DBNull.Value, "0", .Item("ColHeaderBorderStyle")))
                    StrStyleCode &= ";" & StrColId & "_CHBST=" & GetBorderStyle(IIf(.Item("ColHeaderBorderStyleTop") Is DBNull.Value, "0", .Item("ColHeaderBorderStyleTop")))
                    StrStyleCode &= ";" & StrColId & "_CHBSB=" & GetBorderStyle(IIf(.Item("ColHeaderBorderStyleBottom") Is DBNull.Value, "0", .Item("ColHeaderBorderStyleBottom")))
                    StrStyleCode &= ";" & StrColId & "_CHBSL=" & GetBorderStyle(IIf(.Item("ColHeaderBorderStyleLeft") Is DBNull.Value, "0", .Item("ColHeaderBorderStyleLeft")))
                    StrStyleCode &= ";" & StrColId & "_CHBSR=" & GetBorderStyle(IIf(.Item("ColHeaderBorderStyleRight") Is DBNull.Value, "0", .Item("ColHeaderBorderStyleRight")))

                    StrStyleCode &= ";" & StrColId & "_CHBW=" & .Item("ColHeaderBorderWidth")
                    StrStyleCode &= ";" & StrColId & "_CHBWT=" & .Item("ColHeaderBorderWidthTop")
                    StrStyleCode &= ";" & StrColId & "_CHBWB=" & .Item("ColHeaderBorderWidthBottom")
                    StrStyleCode &= ";" & StrColId & "_CHBWR=" & .Item("ColHeaderBorderWidthRight")
                    StrStyleCode &= ";" & StrColId & "_CHBWL=" & .Item("ColHeaderBorderWidthLeft")

                    StrStyleCode &= ";" & StrColId & "_CHFC=" & .Item("ColHeaderForeColor")
                    StrStyleCode &= ";" & StrColId & "_CHBKC=" & .Item("ColHeaderBackColor")
                    StrStyleCode &= ";" & StrColId & "_CHFN=" & .Item("ColHeaderFont")
                    StrStyleCode &= ";" & StrColId & "_CHFS=" & .Item("ColHeaderFontSize")

                    StrStyleCode &= ";" & StrColId & "_CHFII=" & IIf(IsDBNull(.Item("ColHeaderFontIsItalic")), False, .Item("ColHeaderFontIsItalic"))
                    StrStyleCode &= ";" & StrColId & "_CHFIU=" & IIf(IsDBNull(.Item("ColHeaderFontIsUnderline")), False, .Item("ColHeaderFontIsUnderline"))
                    StrStyleCode &= ";" & StrColId & "_CHFIB=" & IIf(IsDBNull(.Item("ColHeaderFontIsBold")), False, .Item("ColHeaderFontIsBold"))

                    StrStyleCode &= ";" & StrColId & "_CHHA=" & GetHorizontalAlignment(IIf(.Item("ColumnHeaderHalignment") Is DBNull.Value, "", .Item("ColumnHeaderHalignment")))
                    StrStyleCode &= ";" & StrColId & "_CHVA=" & GetVerticalAlignment(IIf(.Item("ColumnHeaderValignment") Is DBNull.Value, "", .Item("ColumnHeaderValignment")))

                    StrStyleCode &= ";" & StrColId & "_CHP=" & .Item("ColHeaderPaddingLeft")
                    StrStyleCode &= ";" & StrColId & "_CHM=0" & IIf(mPage.IsPostBack, "25", uwgViewColumns.DisplayLayout.HeaderStyleDefault.Margin.Top.ToString)

                    StrStyleCode &= ";" & StrColId & "_CHW=" & IIf(.Item("ColumnWidth") Is DBNull.Value, "100", .Item("ColumnWidth"))
                    StrStyleCode &= ";" & StrColId & "_CHH=" & IIf(mPage.IsPostBack, "25", uwgViewColumns.DisplayLayout.HeaderStyleDefault.Height.ToString)

                    'Footer  Style
                    StrStyleCode &= ";" & StrColId & "_CFBC=" & .Item("ColFooterBorderColor")
                    StrStyleCode &= ";" & StrColId & "_CFBCT=" & .Item("ColFooterBorderColorTop")
                    StrStyleCode &= ";" & StrColId & "_CFBCB=" & .Item("ColFooterBorderColorBottom")
                    StrStyleCode &= ";" & StrColId & "_CFBCL=" & .Item("ColFooterBorderColorLeft")
                    StrStyleCode &= ";" & StrColId & "_CFBCR=" & .Item("ColFooterBorderColorRight")


                    StrStyleCode &= ";" & StrColId & "_CFBS=" & GetBorderStyle(IIf(.Item("ColFooterBorderStyle") Is DBNull.Value, "0", .Item("ColFooterBorderStyle")))
                    StrStyleCode &= ";" & StrColId & "_CFBST=" & GetBorderStyle(IIf(.Item("ColFooterBorderStyleTop") Is DBNull.Value, "0", .Item("ColFooterBorderStyleTop")))
                    StrStyleCode &= ";" & StrColId & "_CFBSB=" & GetBorderStyle(IIf(.Item("ColFooterBorderStyleBottom") Is DBNull.Value, "0", .Item("ColFooterBorderStyleBottom")))
                    StrStyleCode &= ";" & StrColId & "_CFBSL=" & GetBorderStyle(IIf(.Item("ColFooterBorderStyleLeft") Is DBNull.Value, "0", .Item("ColFooterBorderStyleLeft")))
                    StrStyleCode &= ";" & StrColId & "_CFBSR=" & GetBorderStyle(IIf(.Item("ColFooterBorderStyleRight") Is DBNull.Value, "0", .Item("ColFooterBorderStyleRight")))


                    StrStyleCode &= ";" & StrColId & "_CFBW=" & .Item("ColFooterBorderWidth")
                    StrStyleCode &= ";" & StrColId & "_CFBWT=" & .Item("ColFooterBorderWidthTop")
                    StrStyleCode &= ";" & StrColId & "_CFBWB=" & .Item("ColFooterBorderWidthBottom")
                    StrStyleCode &= ";" & StrColId & "_CFBWR=" & .Item("ColFooterBorderWidthRight")
                    StrStyleCode &= ";" & StrColId & "_CFBWL=" & .Item("ColFooterBorderWidthLeft")

                    StrStyleCode &= ";" & StrColId & "_CFFC=" & .Item("ColFooterForeColor")
                    StrStyleCode &= ";" & StrColId & "_CFBKC=" & .Item("ColFooterBackColor")
                    StrStyleCode &= ";" & StrColId & "_CFFN=" & .Item("ColFooterFont")
                    StrStyleCode &= ";" & StrColId & "_CFFS=" & .Item("ColFooterFontSize")

                    StrStyleCode &= ";" & StrColId & "_CFFII=" & IIf(IsDBNull(.Item("ColFooterFontIsItalic")), False, .Item("ColFooterFontIsItalic"))
                    StrStyleCode &= ";" & StrColId & "_CFFIU=" & IIf(IsDBNull(.Item("ColFooterFontIsUnderline")), False, .Item("ColFooterFontIsUnderline"))
                    StrStyleCode &= ";" & StrColId & "_CFFIB=" & IIf(IsDBNull(.Item("ColFooterFontIsBold")), False, .Item("ColFooterFontIsBold"))

                    StrStyleCode &= ";" & StrColId & "_CFHA=" & GetHorizontalAlignment(IIf(.Item("ColumnFooterHalignment") Is DBNull.Value, "", .Item("ColumnFooterHalignment")))
                    StrStyleCode &= ";" & StrColId & "_CFVA=" & GetVerticalAlignment(IIf(.Item("ColumnFooterValignment") Is DBNull.Value, "", .Item("ColumnFooterValignment")))

                    StrStyleCode &= ";" & StrColId & "_CFP=" & IIf(mPage.IsPostBack, "0", .Item("ColFooterPaddingLeft"))
                    StrStyleCode &= ";" & StrColId & "_CFM=" & IIf(mPage.IsPostBack, "0", uwgViewColumns.DisplayLayout.FooterStyleDefault.Margin.Top.ToString)

                    StrStyleCode &= ";" & StrColId & "_CFW=" '& .Item("")
                    StrStyleCode &= ";" & StrColId & "_CFH=" & IIf(mPage.IsPostBack, "25", uwgViewColumns.DisplayLayout.FooterStyleDefault.Height.ToString)

                    StrStyleCode &= ";" & StrColId & "_CSORT=" & IIf(IsDBNull(.Item("IsSorted")), "0", .Item("IsSorted"))
                    StrStyleCode &= ";" & StrColId & "_CHTXT=" & .Item("EngDescription")
                    StrStyleCode &= ";" & StrColId & "_CFORMAT=" & .Item("Format")
                    StrStyleCode &= ";" & StrColId & "_CFTOTAL=" & .Item("FooterTotal")
                    StrStyleCode &= ";" & StrColId & "_CFTXT=" & .Item("EngFooterCaption")
                    StrStyleCode &= ";" & StrColId & "_CCCURR="

                End With
            Next
            Return StrStyleCode.Substring(1)
        Catch ex As Exception
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(mPage.Page, ex.Message)
        End Try

    End Function

    Public Function GetHorizontalAlignment(ByVal StrAlignType As String) As String
        Select Case StrAlignType.ToUpper
            Case "NOTSET", "0"
                Return "left"
            Case "LEFT", "1"
                Return "left"
            Case "CENTER", "2"
                Return "center"
            Case "RIGHT", "3"
                Return "right"
            Case Else
                Return "left"
        End Select
    End Function

    Public Function GetVerticalAlignment(ByVal StrAlignType As String) As String
        Select Case StrAlignType.ToUpper
            Case "NOTSET", "0"
                Return "top"
            Case "MIDDLE", "2"
                Return "middle"
            Case "BOTTOM", "3"
                Return "bottom"
            Case "TOP", "1"
                Return "top"
            Case Else
                Return "top"
        End Select
    End Function


    Public Function GetFontsDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList) As Boolean
        Try
            Dim fonts As New InstalledFontCollection
            For Each family As FontFamily In fonts.Families
                DdlValues.Items.Add(family.Name)
            Next
            DdlValues.SelectedValue = "Arial"
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    Public Function SetFontSizes(ByVal IntFontSize As Integer) As Web.UI.WebControls.FontSize
        Select Case IntFontSize
            Case 6
                Return Web.UI.WebControls.FontSize.Small
            Case 2
                Return Web.UI.WebControls.FontSize.Smaller
            Case 4
                Return Web.UI.WebControls.FontSize.XXSmall
            Case 10
                Return Web.UI.WebControls.FontSize.XXLarge
            Case 6
                Return Web.UI.WebControls.FontSize.Small
            Case 9
                Return Web.UI.WebControls.FontSize.XLarge
            Case 7
                Return Web.UI.WebControls.FontSize.Medium
            Case 3
                Return Web.UI.WebControls.FontSize.Larger
            Case 8
                Return Web.UI.WebControls.FontSize.Large
            Case 1
                Return Web.UI.WebControls.FontSize.AsUnit
            Case Else
                Return Web.UI.WebControls.FontSize.Large
        End Select
    End Function

    Public Function GetFontSizesfromStrings(ByVal strFontSize As String) As Integer
        Return CInt(strFontSize)
    End Function

    Public Function GetFooterTotal(ByVal IntTotalType As Integer) As Infragistics.WebUI.UltraWebGrid.SummaryInfo
        Select Case IntTotalType
            Case 0
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Text
            Case 1
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum
            Case 2
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Avg
            Case 3
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Min
            Case 4
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Max
            Case 5
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Count
            Case 6
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Formula

        End Select
    End Function

    Public Function SetFooterTotal(ByVal strTotalType As String) As Integer
        Select Case strTotalType
            Case Infragistics.WebUI.UltraWebGrid.SummaryInfo.Text.ToString
                Return 0
            Case Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum.ToString
                Return 1
            Case Infragistics.WebUI.UltraWebGrid.SummaryInfo.Avg.ToString
                Return 2
            Case Infragistics.WebUI.UltraWebGrid.SummaryInfo.Min.ToString
                Return 3
            Case Infragistics.WebUI.UltraWebGrid.SummaryInfo.Max.ToString
                Return 4
            Case Infragistics.WebUI.UltraWebGrid.SummaryInfo.Count.ToString
                Return 5
            Case Infragistics.WebUI.UltraWebGrid.SummaryInfo.Formula.ToString
                Return 6
        End Select
    End Function

    Public Function CustomizeStylez(ByVal StyleNo As Integer) As Data.DataTable
        Dim dtStyleTable As New Data.DataTable
        Dim dcColumn1 As New Data.DataColumn("FormatCaption", GetType(String))
        Dim dcColumn2 As New Data.DataColumn("FormatValue", GetType(String))
        Dim drDataRow As Data.DataRow

        dtStyleTable.Columns.Add(dcColumn1)
        dtStyleTable.Columns.Add(dcColumn2)

        Select Case StyleNo

            '"Date" Case
            Case 7, 111, 61
                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "Default Long Format"
                drDataRow.Item(1) = "dddd, dd MMMM yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "Default Short Format"
                drDataRow.Item(1) = "MM/dd/yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "3/1/99 13:20"
                drDataRow.Item(1) = "MM/dd/yy H:mm"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "3/1/99 1:20pm"
                drDataRow.Item(1) = "MM/dd/yy h:mm tt"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "01-Mar-1999 13:20"
                drDataRow.Item(1) = "dd-MMM-yyyy H:mm"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "01-Mar-1999 1:20pm"
                drDataRow.Item(1) = "dd-MMM-yyyy h:mm tt"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "March 1, 1999 13:20"
                drDataRow.Item(1) = "MMMM dd, yyyy H:mm"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "March 1, 1999 1:20pm"
                drDataRow.Item(1) = "MMMM dd, yyyy h:mm tt"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "13:20 3/1/99"
                drDataRow.Item(1) = "HH:mm MM/dd/yy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "1:20pm 01-Mar-1999"
                drDataRow.Item(1) = "h:mm tt dd-MMM-yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "13:20"
                drDataRow.Item(1) = "H:mm"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "1:20pm"
                drDataRow.Item(1) = "h:mm tt"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "13:20:45"
                drDataRow.Item(1) = "H:mm:ss"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "1:20:45pm"
                drDataRow.Item(1) = "h:mm:ss tt"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "3/1"
                drDataRow.Item(1) = "M/d"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "3/01"
                drDataRow.Item(1) = "M/dd"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "3/1/99"
                drDataRow.Item(1) = "M/d/yy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "03/01/1999"
                drDataRow.Item(1) = "MM/dd/yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "01 - Mar"
                drDataRow.Item(1) = "dd - MMM"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "1-Mar-99"
                drDataRow.Item(1) = "dd-MMM-yy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "1-Mar-1999"
                drDataRow.Item(1) = "d-MMM-yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "01-Mar-1999"
                drDataRow.Item(1) = "dd-MMM-yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "01-March-1999"
                drDataRow.Item(1) = "dd-MMMM-yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "Mar-99"
                drDataRow.Item(1) = "MMM-yy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "March 1999"
                drDataRow.Item(1) = "MMMM yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "March 01, 1999"
                drDataRow.Item(1) = "MMMM dd, yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "Monday, March 1, 1999"
                drDataRow.Item(1) = "dddd, MMMM dd, yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "Monday, 1 March, 1999"
                drDataRow.Item(1) = "dddd, dd MMMM, yyyy"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "3"
                drDataRow.Item(1) = "M"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "3-99"
                drDataRow.Item(1) = "M-yy"
                dtStyleTable.Rows.Add(drDataRow)

                '"Number" Case
            Case 3, 4, 14
                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "Default Number Format"
                drDataRow.Item(1) = "#,###.#"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "-1123"
                drDataRow.Item(1) = "####"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "-1,123"
                drDataRow.Item(1) = "#,###"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "-1123.00"
                drDataRow.Item(1) = "####.##"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "-1,123.00"
                drDataRow.Item(1) = "#,###.##"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "-1123.0000"
                drDataRow.Item(1) = "####.####"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "-1,123.0000"
                drDataRow.Item(1) = "#,###.####"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "(1123)"
                drDataRow.Item(1) = "(####)"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "(1,123)"
                drDataRow.Item(1) = "(#,###)"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "((1123.00))"
                drDataRow.Item(1) = "(####.##)"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "((1,123.00))"
                drDataRow.Item(1) = "(#,###.##)"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "((1123.0000))"
                drDataRow.Item(1) = "(####.####)"
                dtStyleTable.Rows.Add(drDataRow)

                drDataRow = dtStyleTable.NewRow
                drDataRow.Item(0) = "((1,123.0000))"
                drDataRow.Item(1) = "(#,###.##)"
                dtStyleTable.Rows.Add(drDataRow)

                'Standard Case "String"
            Case Else
                Return dtStyleTable
        End Select

        Return dtStyleTable
    End Function

    Public Function GetAllStoredProcedures(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem


        Try

            StrCommandString = "[sys_GetAllUserStoredProcedures]"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow("Name"), SqlDbType.VarChar)
                Item.Value = ObjDataRow("Name")
                DdlValues.Items.Add(Item)
            Next

            If DdlValues.Items.Count > 0 Then
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

    Public Function ExecuateStoredProcedure(ByVal strProcName As String) As Data.DataSet
        Dim dsProcDataset As New Data.DataSet

        dsProcDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, strProcName)

        If Not IsNothing(dsProcDataset) Then Return dsProcDataset

    End Function

    Public Function CheckQuery(ByVal strQuery As String) As Boolean
        Dim IntFromIndex As Integer
        strQuery = strQuery.ToUpper.Trim
        Dim Length As Integer

        IntFromIndex = strQuery.LastIndexOf("FROM") + 5
        Length = strQuery.Length
        If ((Length - IntFromIndex) > 5) Then
            Return True
        End If

    End Function

    Public Function NulllizeSPandExecuate(ByVal strProcName As String, ByVal IntParmetersNumbers As Integer) As Data.DataSet
        Dim Intcounter As Integer = 0
        Dim parmArray(IntParmetersNumbers - 1) As SqlClient.SqlParameter
        Dim objSQLparm As SqlClient.SqlParameter
        For Intcounter = 0 To IntParmetersNumbers - 1
            parmArray(Intcounter) = New SqlClient.SqlParameter
            parmArray(Intcounter).Value = DBNull.Value
        Next
        Dim dsProcDataset As New Data.DataSet
        Try
            dsProcDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, strProcName, parmArray)
            If Not IsNothing(dsProcDataset) Then Return dsProcDataset
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetStoredParameters(ByVal strProcName As String, ByVal strParmStoredName As String) As Data.DataSet
        Dim dsProcDataset As New Data.DataSet
        dsProcDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, strProcName, strParmStoredName)
        If Not IsNothing(dsProcDataset) Then Return dsProcDataset
    End Function

    Public Function GetDataSetFromDataSource(ByVal uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid) As DataSet
        Return CType(uwgGrid.DataSource, DataSet)
    End Function
#End Region

End Class

