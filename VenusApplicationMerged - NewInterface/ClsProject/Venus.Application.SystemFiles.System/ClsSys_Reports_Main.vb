'======================================================================
'Project name  : Venus V. 
'Program name  : ClsSys_Reports_Main
'Date Created  : 
'Issue #       :       
'Developer     : [0256]
'Description   : This Class acts as the OG Layer for frmReportViewer
'              : This Class Recives the ReportID 
'              : 
'              : 
'Modifacations :
'======================================================================
Imports System.Data
Imports System.Drawing
Imports System.Reflection
Imports System.Collections.Generic
Imports Drawing.Text
Imports System

Public Class ClsSys_Reports_Main
    Inherits ClsDataAcessLayer

#Region "Constants"


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

#Region "public Methods"
    ''==================================================================================
    ''ProcedureName  : FillGridColumns()
    ''Module         : System Module -Report Writer 
    ''Project        : Venus V.
    ''Description    : 
    ''Developer      : [Maz]Mah AbdelAziz
    ''Date Created   : 18-11-2007
    ''==================================================================================
    'Public Function FillGridColumns(ByRef grid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal ReportID As Integer, ByVal MPage As Web.UI.Page) As Boolean
    '    grid.Columns.Clear()
    '    Dim clsReportColumns As New Clssys_ReportsColumns(MPage)
    '    Dim strSelect As String = "Select "

    '    clsReportColumns.Find(" ReportID=" & ReportID & " And IsNull(CancelDate,'')='' ")
    '    If (clsReportColumns.DataSet.Tables(0).Rows.Count > 0) Then
    '        For Each row As DataRow In clsReportColumns.DataSet.Tables(0).Rows
    '            Dim nCol As New Infragistics.WebUI.UltraWebGrid.UltraGridColumn()

    '            If row("EngHeaderCaption") Is DBNull.Value Then
    '                nCol.Header.Caption = Nothing
    '            Else
    '                nCol.Header.Caption = row("EngHeaderCaption")
    '            End If
    '            If row("EngFooterCaption") Is DBNull.Value Then
    '                nCol.Footer.Caption = Nothing
    '            Else
    '                nCol.Footer.Caption = row("EngFooterCaption")
    '            End If

    '            If row("Width") Is DBNull.Value Then
    '                nCol.Width = Nothing
    '            Else
    '                nCol.Width = Global.System.Web.UI.WebControls.Unit.Pixel(row("Width"))
    '            End If

    '            If row("BaseColumnName") Is DBNull.Value Then
    '                nCol.BaseColumnName = Nothing
    '            Else
    '                nCol.BaseColumnName = row("BaseColumnName")
    '            End If

    '            If row("cs_Font") Is DBNull.Value Then
    '                nCol.CellStyle.Font.Name = Nothing
    '            Else
    '                nCol.CellStyle.Font.Name = row("cs_Font")
    '            End If

    '            If row("cs_BackColor") Is DBNull.Value Then
    '                nCol.CellStyle.BackColor = Nothing
    '            Else
    '                nCol.CellStyle.BackColor = Drawing.Color.FromName(row("cs_BackColor"))
    '            End If

    '            If row("cs_BorderColor") Is DBNull.Value Then
    '                nCol.CellStyle.BorderColor = Nothing
    '            Else
    '                nCol.CellStyle.BorderColor = Drawing.Color.FromName(row("cs_BorderColor"))
    '            End If

    '            If row("cs_BorderBottomColor") Is DBNull.Value Then
    '                nCol.CellStyle.BorderDetails.ColorBottom = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.ColorBottom = Drawing.Color.FromName(row("cs_BorderBottomColor"))
    '            End If

    '            If row("cs_BorderLeftColor") Is DBNull.Value Then
    '                nCol.CellStyle.BorderDetails.ColorLeft = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.ColorLeft = Drawing.Color.FromName(row("cs_BorderLeftColor"))
    '            End If
    '            If row("cs_BorderRightColor") Is DBNull.Value Then
    '                nCol.CellStyle.BorderDetails.ColorRight = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.ColorRight = Drawing.Color.FromName(row("cs_BorderRightColor"))
    '            End If
    '            If row("cs_BorderTopColor") Is DBNull.Value Then
    '                nCol.CellStyle.BorderDetails.ColorTop = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.ColorTop = Drawing.Color.FromName(row("cs_BorderTopColor"))
    '            End If

    '            If (row("cs_BorderBottomStyle") Is DBNull.Value) Then
    '                nCol.CellStyle.BorderDetails.StyleBottom = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.StyleBottom = GetBorderStyle(row("cs_BorderBottomStyle"))
    '            End If

    '            If row("cs_BorderLeftStyle") Is DBNull.Value Then
    '                nCol.CellStyle.BorderDetails.StyleLeft = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.StyleLeft = GetBorderStyle(row("cs_BorderLeftStyle"))
    '            End If

    '            If row("cs_BorderRightStyle") Is DBNull.Value Then
    '                nCol.CellStyle.BorderDetails.StyleRight = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.StyleRight = GetBorderStyle(row("cs_BorderRightStyle"))
    '            End If

    '            If row("cs_BorderTopStyle") Is DBNull.Value Then
    '                nCol.CellStyle.BorderDetails.StyleTop = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.StyleTop = GetBorderStyle(row("cs_BorderTopStyle"))
    '            End If
    '            If row("cs_BorderBottomWidth") Is DBNull.Value Then
    '                nCol.CellStyle.BorderDetails.WidthBottom = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.WidthBottom = Global.System.Web.UI.WebControls.Unit.Pixel(row("cs_BorderBottomWidth"))
    '            End If

    '            If (row("cs_BorderLeftWidth") Is DBNull.Value) Then
    '                nCol.CellStyle.BorderDetails.WidthLeft = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.WidthLeft = Global.System.Web.UI.WebControls.Unit.Pixel(row("cs_BorderLeftWidth"))
    '            End If

    '            If row("cs_BorderRightWidth") Is DBNull.Value Then
    '                nCol.CellStyle.BorderDetails.WidthRight = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.WidthRight = Global.System.Web.UI.WebControls.Unit.Pixel(row("cs_BorderRightWidth"))
    '            End If

    '            If (row("cs_BorderTopWidth") Is DBNull.Value) Then
    '                nCol.CellStyle.BorderDetails.WidthTop = Nothing
    '            Else
    '                nCol.CellStyle.BorderDetails.WidthTop = Global.System.Web.UI.WebControls.Unit.Pixel(row("cs_BorderTopWidth"))
    '            End If

    '            If (row("cs_BorderStyle") Is DBNull.Value) Then
    '                nCol.CellStyle.BorderStyle = Nothing
    '            Else
    '                nCol.CellStyle.BorderStyle = GetBorderStyle(row("cs_BorderStyle"))
    '            End If

    '            If row("cs_BorderWidth") Is DBNull.Value Then
    '                nCol.CellStyle.BorderWidth = Nothing
    '            Else
    '                nCol.CellStyle.BorderWidth = Global.System.Web.UI.WebControls.Unit.Pixel(row("cs_BorderWidth"))
    '            End If

    '            If row("cs_CssClass") Is DBNull.Value Then
    '                nCol.CellStyle.CssClass = Nothing
    '            Else
    '                nCol.CellStyle.CssClass = row("cs_CssClass")
    '            End If

    '            If row("cs_ForeColor") Is DBNull.Value Then
    '                nCol.CellStyle.ForeColor = Nothing
    '            Else
    '                nCol.CellStyle.ForeColor = Drawing.Color.FromName(row("cs_ForeColor"))
    '            End If

    '            If row("cs_Width") Is DBNull.Value Then
    '                nCol.CellStyle.Width = Nothing
    '            Else
    '                nCol.CellStyle.Width = Global.System.Web.UI.WebControls.Unit.Pixel(row("cs_Width"))
    '            End If

    '            If row("cs_Height") Is DBNull.Value Then
    '                nCol.CellStyle.Height = Nothing
    '            Else
    '                nCol.CellStyle.Height = Global.System.Web.UI.WebControls.Unit.Pixel(row("cs_Height"))
    '            End If

    '            If row("cs_HorizontalAlignment") Is DBNull.Value Then
    '                nCol.CellStyle.HorizontalAlign = Nothing
    '            Else
    '                nCol.CellStyle.HorizontalAlign = GetHorizontalAlignment(row("cs_HorizontalAlignment"))
    '            End If

    '            If row("cs_VerticalAlignment") Is DBNull.Value Then
    '                nCol.CellStyle.VerticalAlign = Nothing
    '            Else
    '                nCol.CellStyle.VerticalAlign = GetVerticalAlignment(row("cs_VerticalAlignment"))
    '            End If
    '            If row("cs_BottomMargins") Is DBNull.Value Then
    '                nCol.CellStyle.Margin.Bottom = Nothing
    '            Else
    '                nCol.CellStyle.Margin.Bottom = Web.UI.WebControls.Unit.Pixel(row("cs_BottomMargins"))
    '            End If
    '            If row("cs_LeftMargins") Is DBNull.Value Then
    '                nCol.CellStyle.Margin.Left = Nothing
    '            Else
    '                nCol.CellStyle.Margin.Left = Web.UI.WebControls.Unit.Pixel(row("cs_LeftMargins"))
    '            End If
    '            If row("cs_RightMargins") Is DBNull.Value Then
    '                nCol.CellStyle.Margin.Right = Nothing
    '            Else
    '                nCol.CellStyle.Margin.Right = Web.UI.WebControls.Unit.Pixel(row("cs_RightMargins"))
    '            End If

    '            If row("cs_TopMargins") Is DBNull.Value Then
    '                nCol.CellStyle.Margin.Top = Nothing
    '            Else
    '                nCol.CellStyle.Margin.Top = Web.UI.WebControls.Unit.Pixel(row("cs_TopMargins"))
    '            End If
    '            If row("cs_BottomPadding") Is DBNull.Value Then
    '                nCol.CellStyle.Padding.Bottom = Nothing
    '            Else
    '                nCol.CellStyle.Padding.Bottom = Web.UI.WebControls.Unit.Pixel(row("cs_BottomPadding"))
    '            End If

    '            If row("cs_LeftPadding") Is DBNull.Value Then
    '                nCol.CellStyle.Padding.Left = Nothing
    '            Else
    '                nCol.CellStyle.Padding.Left = Web.UI.WebControls.Unit.Pixel(row("cs_LeftPadding"))
    '            End If

    '            If row("cs_RightPadding") Is DBNull.Value Then
    '                nCol.CellStyle.Padding.Right = Nothing
    '            Else
    '                nCol.CellStyle.Padding.Right = Web.UI.WebControls.Unit.Pixel(row("cs_RightPadding"))
    '            End If

    '            If row("cs_TopPadding") Is DBNull.Value Then
    '                nCol.CellStyle.Padding.Top = Nothing
    '            Else
    '                nCol.CellStyle.Padding.Top = Web.UI.WebControls.Unit.Pixel(row("cs_TopPadding"))
    '            End If

    '            If row("cs_Textoverflow") Is DBNull.Value Then
    '                nCol.CellStyle.TextOverflow = Nothing
    '            Else
    '                nCol.CellStyle.TextOverflow = GetTextOverflow(row("cs_Textoverflow"))
    '            End If

    '            If row("cs_Wrap") Is DBNull.Value Then
    '                nCol.CellStyle.Wrap = Nothing
    '            Else
    '                nCol.CellStyle.Wrap = row("cs_Wrap")
    '            End If

    '            If row("cs_ChangLinksColor") Is DBNull.Value Then
    '                nCol.ChangeLinksColor = Nothing
    '            Else
    '                nCol.ChangeLinksColor = row("cs_ChangLinksColor")
    '            End If

    '            If row("SortIndicator") Is DBNull.Value Then
    '                nCol.SortIndicator = Nothing
    '            Else
    '                nCol.SortIndicator = GetSortIndicator(row("SortIndicator"))
    '            End If

    '            If row("AllowGroupBy") Is DBNull.Value Then
    '                nCol.AllowGroupBy = Nothing
    '            Else
    '                nCol.AllowGroupBy = GetAllowGroupBy(row("AllowGroupBy"))
    '            End If

    '            If row("AllowNull") Is DBNull.Value Then
    '                nCol.AllowNull = Nothing
    '            Else
    '                nCol.AllowNull = row("AllowNull")
    '            End If

    '            If row("AllowResize") Is DBNull.Value Then
    '                nCol.AllowResize = Nothing
    '            Else
    '                nCol.AllowResize = GetAllowResize(row("AllowResize"))
    '            End If

    '            If row("AllowRowFiltering") Is DBNull.Value Then
    '                nCol.AllowRowFiltering = Nothing
    '            Else
    '                nCol.AllowRowFiltering = row("AllowRowFiltering")
    '            End If

    '            If row("AllowUpdate") Is DBNull.Value Then
    '                nCol.AllowUpdate = Nothing
    '            Else
    '                nCol.AllowUpdate = GetAllowUpdate(row("AllowUpdate"))
    '            End If

    '            If row("Case") Is DBNull.Value Then
    '                nCol.Case = Nothing
    '            Else
    '                nCol.Case = GetCase(row("Case"))
    '            End If

    '            If row("CaseButtonDisplay") Is DBNull.Value Then
    '                nCol.CellButtonDisplay = Nothing
    '            Else
    '                nCol.CellButtonDisplay = GetCaseButtonDisplay(row("CaseButtonDisplay"))
    '            End If


    '            If row("CellMultiline") Is DBNull.Value Then
    '                nCol.CellMultiline = Nothing
    '            Else
    '                nCol.CellMultiline = GetCellMultilines(row("CellMultiline"))
    '            End If

    '            If row("Format") Is DBNull.Value Then
    '                nCol.Format = Nothing
    '            Else
    '                nCol.Format = row("Format")
    '            End If

    '            If row("GatherFilterDate") Is DBNull.Value Then
    '                nCol.GatherFilterData = Nothing
    '            Else
    '                nCol.GatherFilterData = GetGatherFilter(row("GatherFilterDate"))
    '            End If

    '            If row("Hidden") Is DBNull.Value Then
    '                nCol.Hidden = Nothing
    '            Else
    '                nCol.Hidden = row("Hidden")
    '            End If

    '            If row("MergeCells") Is DBNull.Value Then
    '                nCol.MergeCells = Nothing
    '            Else
    '                nCol.MergeCells = row("MergeCells")
    '            End If

    '            If row("Type") Is DBNull.Value Then
    '                nCol.Type = Nothing
    '            Else
    '                nCol.Type = GetColumnType(row("Type"))
    '            End If

    '            If row("DataType") Is DBNull.Value Then
    '                nCol.DataType = Nothing
    '            Else
    '                nCol.DataType = GetDataType(row("DataType"))
    '            End If

    '            If row("FieldLen") Is DBNull.Value Then
    '                nCol.FieldLen = Nothing
    '            Else
    '                nCol.FieldLen = Convert.ToInt64(row("FieldLen"))
    '            End If

    '            If row("IsBound") Is DBNull.Value Then
    '                nCol.IsBound = Nothing
    '            Else
    '                nCol.IsBound = row("IsBound")
    '            End If

    '            If row("Key") Is DBNull.Value Then
    '                nCol.Key = Nothing
    '            Else
    '                nCol.Key = row("Key")
    '            End If

    '            If row("NullText") Is DBNull.Value Then
    '                nCol.NullText = Nothing
    '            Else
    '                nCol.NullText = row("NullText")
    '            End If


    '            strSelect += row("BaseColumnName") + " ,"
    '            grid.Columns.Add(nCol)
    '        Next
    '        strSelect = strSelect.Trim(",")
    '        'Dim dgRow As New Infragistics.WebUI.UltraWebGrid.UltraGridRow()
    '        'Dim clsBloodGroups As New Clshrs_BloodGroups(MPage)
    '        'clsBloodGroups.Find("ID>0")
    '        'grid.DataSource = clsBloodGroups.DataSet.Tables(0).DefaultView
    '        'grid.DataBind()

    '    End If


    'End Function
    Private Function GetTextOverflow(ByVal intVal As Boolean) As Infragistics.WebUI.UltraWebGrid.TextOverflow
        Select Case intVal
            Case False
                Return Infragistics.WebUI.UltraWebGrid.TextOverflow.Clip
            Case True
                Return Infragistics.WebUI.UltraWebGrid.TextOverflow.Ellipsis
        End Select

    End Function
    Private Function GetSortIndicator(ByVal intVal As Integer) As Infragistics.WebUI.UltraWebGrid.SortIndicator
        Select Case intVal
            Case 0
                Return Infragistics.WebUI.UltraWebGrid.SortIndicator.Ascending
            Case 1
                Return Infragistics.WebUI.UltraWebGrid.SortIndicator.Descending
            Case 2
                Return Infragistics.WebUI.UltraWebGrid.SortIndicator.None
            Case 3
                Return Infragistics.WebUI.UltraWebGrid.SortIndicator.Disabled
        End Select
    End Function
    Private Function GetAllowGroupBy(ByVal intVal As Boolean) As Infragistics.WebUI.UltraWebGrid.AllowGroupBy
        Select Case intVal
            Case False
                Return Infragistics.WebUI.UltraWebGrid.AllowGroupBy.No
            Case True
                Return Infragistics.WebUI.UltraWebGrid.AllowGroupBy.Yes
            Case Else
                Return Infragistics.WebUI.UltraWebGrid.AllowGroupBy.NotSet

        End Select
    End Function
    Private Function GetAllowResize(ByVal intVal As Boolean) As Infragistics.WebUI.UltraWebGrid.AllowSizing
        Select Case intVal
            Case False
                Return Infragistics.WebUI.UltraWebGrid.AllowSizing.Fixed
            Case True
                Return Infragistics.WebUI.UltraWebGrid.AllowSizing.Free
            Case Else
                Return Infragistics.WebUI.UltraWebGrid.AllowSizing.NotSet
        End Select
    End Function
    Private Function GetAllowUpdate(ByVal intVal As Integer) As Infragistics.WebUI.UltraWebGrid.AllowUpdate
        Select Case intVal
            Case 0
                Return Infragistics.WebUI.UltraWebGrid.AllowUpdate.NotSet
            Case 1
                Return Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes
            Case 2
                Return Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
            Case 3
                Return Infragistics.WebUI.UltraWebGrid.AllowUpdate.RowTemplateOnly
        End Select
    End Function
    Private Function GetCase(ByVal intVal As Integer) As Infragistics.WebUI.UltraWebGrid.Case
        Select Case intVal
            Case 0
                Return Infragistics.WebUI.UltraWebGrid.Case.Unchanged
            Case 1
                Return Infragistics.WebUI.UltraWebGrid.Case.Lower
            Case 2
                Return Infragistics.WebUI.UltraWebGrid.Case.Upper
        End Select
    End Function
    Private Function GetCaseButtonDisplay(ByVal intVal As Integer) As Infragistics.WebUI.UltraWebGrid.CellButtonDisplay
        Select Case intVal
            Case 0
                Return Infragistics.WebUI.UltraWebGrid.CellButtonDisplay.OnMouseEnter
            Case 1
                Return Infragistics.WebUI.UltraWebGrid.CellButtonDisplay.Always
        End Select
    End Function
    Private Function GetCellMultilines(ByVal intVal As Boolean) As Infragistics.WebUI.UltraWebGrid.CellMultiline
        Select Case intVal
            Case False
                Return Infragistics.WebUI.UltraWebGrid.CellMultiline.No
            Case True
                Return Infragistics.WebUI.UltraWebGrid.CellMultiline.Yes
            Case Else
                Return Infragistics.WebUI.UltraWebGrid.CellMultiline.NotSet

        End Select
    End Function
    Private Function GetGatherFilter(ByVal intVal As Boolean) As Infragistics.WebUI.Shared.DefaultableBoolean
        Select Case intVal
            Case False
                Return Infragistics.WebUI.Shared.DefaultableBoolean.False
            Case True
                Return Infragistics.WebUI.Shared.DefaultableBoolean.True
            Case Else
                Return Infragistics.WebUI.Shared.DefaultableBoolean.NotSet
        End Select
    End Function
    Private Function GetColumnType(ByVal intVal As Integer) As Infragistics.WebUI.UltraWebGrid.ColumnType
        Select Case intVal
            Case 0
                Return Infragistics.WebUI.UltraWebGrid.ColumnType.NotSet
            Case 1
                Return Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox
            Case 2
                Return Infragistics.WebUI.UltraWebGrid.ColumnType.DropDownList
            Case 3
                Return Infragistics.WebUI.UltraWebGrid.ColumnType.Button
            Case 4
                Return Infragistics.WebUI.UltraWebGrid.ColumnType.HyperLink
            Case 5
                Return Infragistics.WebUI.UltraWebGrid.ColumnType.Custom

        End Select
    End Function
    Private Function GetDataType(ByVal intVal As Integer) As String 'Infragistics.WebUI.UltraWebGrid.ColumnDataType
        Select Case intVal
            Case 0
                Return "Boolean"
            Case 1
                Return "Byte"
            Case 2
                Return "Char"
            Case 3
                Return "DateTime"
            Case 4
                Return "Decimal"
            Case 5
                Return "Double"
            Case 6
                Return "Guid"
            Case 7
                Return "Int16"
            Case 8
                Return "Int32"
            Case 9
                Return "Int64"
            Case 10
                Return "Object"
            Case 11
                Return "SByte"
            Case 12
                Return "Single"
            Case 13
                Return "String"
            Case 14
                Return "UInt16"
            Case 15
                Return "UInt32"
            Case 16
                Return "UInt64"

        End Select
    End Function
    '==================================================================================
    'ProcedureName  : ReadReportName()
    'Module         : System Module -Report Writer 
    'Project        : Venus V.
    'Description    : 
    'Developer      : Data Ocean
    '               : [AGR] Abdul Jaleel R. Ossman
    'Date Created   : DataOcean 
    'Date Modified  : 
    'Modifacations  : 
    'Calling       *:*
    'Function Calls : 
    '               : 
    'Called From    : 
    '               : 
    '               : 
    'Ex.            : ReadReportName(2)
    '==================================================================================
    Public Function ReadReportName(ByVal ReportID As Integer) As Boolean
        mObjReports = New ClsRpw_ReportsProperties(mPage)
        Try
            mObjReports.Find("ID=" & ReportID)
            ReportName = mObjReports.EngName
            ReportID = mObjReports.ID
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '==================================================================================
    'ProcedureName  : ReadReportParameter - Criteria Fields 
    'Module         : Report Writer 
    'Project        : Venus V.
    'Description    : This Function Read the Report paramters - Report Criteria - and according to those Parameters 
    '               : Data Type it Handels The Following 
    '               : 1- Read Cretiria Fields from sys_ReportsCriteria and creat dynamically 
    '               :    Equavillent controls 
    '               : 2- Store Parameter names in an array - that array will be sent to JS file to iterate through created 
    '               :    Controls and dealing with user data entered in them 
    '               : 3- SetFoucs For first created control 
    '               : 4- SetupTabOrder 
    'Developer      : [AGR] Abdul Jaleel R. Ossman
    'Date Created   : 10-11-2007
    'Date Modified  :  
    'Modifacations  : 
    '               : Modification related to same modification in ClsSys_searchs AND ClsSys_SearchsColumns Class 
    '               :
    'Calling       *:*
    'Function Calls : ClentSideAction Methids  
    'Called From    : frmReportViewer.Page_Load() Event
    '==================================================================================
    Public Function ReadReportCriteria(ByVal ReportID As Integer, _
    ByRef Div1 As Web.UI.HtmlControls.HtmlGenericControl, _
    ByRef div3 As Web.UI.HtmlControls.HtmlTable, _
    ByVal ParameterNames As Web.UI.WebControls.Label, ByVal ParameterRealName As Web.UI.WebControls.Label) As Boolean ', Optional ByVal TestStatment As Web.UI.WebControls.Label = Nothing

        mObjReportsCriteria = New ClsRpw_ReportsCriterias(mPage)
        Dim ObjDr As DataRow = Nothing
        Dim ValueCounter As Integer = 0
        Dim str As String = String.Empty
        Dim litral As New Web.UI.LiteralControl
        Dim Y_Pos As Integer = 80
        Dim X_Pos As Integer = 40
        Dim Width As Integer = 240
        Dim CurrentTextBox As String = String.Empty
        ParameterNames.Text = String.Empty
        ParameterRealName.Text = String.Empty
        Dim Arr() As String
        Try

            If mObjReportsCriteria.Find(" ReportID =" & ReportID) Then

                ReDim Arr(mObjReportsCriteria.DataSet.Tables(0).Rows.Count)
                '        For Each ObjDr In mObjReportsCriteria.DataSet.Tables(0).Rows

                CurrentTextBox = " "
                Select Case ObjDr.Item("ParameterType")
                    Case 0 ' VarChar 
                        Dim ASPTXT As New Web.UI.WebControls.TextBox
                        ASPTXT.ID = "WV_" & ObjDr.Item("ParameterField")
                        CurrentTextBox = ObjDr.Item("ParameterField")
                        ASPTXT.Style.Item("POSITION") = " absolute"
                        ASPTXT.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                        ASPTXT.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                        ASPTXT.Style.Item("WIDTH") = CStr(Width) & "px"
                        ASPTXT.BackColor = Drawing.Color.Silver

                        ASPTXT.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                        ASPTXT.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                        ASPTXT.BorderColor = Drawing.Color.White
                        ASPTXT.MaxLength = mDataHandler.DataValue_Out(ObjDr.Item("InputLength"), SqlDbType.Int)

                        'If mDataHandler.DataValue_Out(ObjDr.Item("IsArabic"), SqlDbType.Bit) = True Then
                        '    Venus.Shared.Web.ClientSideActions.SetLanguage(mPage, ASPTXT, [Shared].Web.ClientSideActions.LANGUAGE_TYPE.ARABIC)
                        'End If

                        ParameterNames.Text &= "|" & "WV_" & ObjDr.Item("ParameterField")
                        ParameterRealName.Text &= "|" & ObjDr.Item("ParameterField")

                        Arr(ValueCounter) = "WV_" & ObjDr.Item("ParameterField")
                        If ValueCounter = 0 Then
                            Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPTXT, False)
                        End If
                        Div1.Controls.Add(ASPTXT)

                    Case 1 ' Numeric Values (TinyInt-Int-SmallInt-Real-Money)
                        Dim ASPWN As New Web.UI.WebControls.TextBox

                        ASPWN.ID = "WN_" & ObjDr.Item("ParameterField")
                        CurrentTextBox = "WN_" & ObjDr.Item("ParameterField")
                        ASPWN.Style.Item("POSITION") = " absolute"
                        ASPWN.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                        ASPWN.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                        ASPWN.Style.Item("WIDTH") = CStr(Width) & "px"
                        ASPWN.BackColor = Drawing.Color.Silver

                        ASPWN.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                        ASPWN.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                        ASPWN.BorderColor = Drawing.Color.White

                        ParameterNames.Text &= "|" & "WN_" & ObjDr.Item("ParameterField")
                        Dim FieldName As String = String.Empty

                        'If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then

                        '    Dim StrObject As String = fnGetTableName(ObjDr.Item("SubSearchID"))
                        '    FieldName = " Select ID from  " & StrObject & " WHERE CODE "
                        '    'TestStatment.Text &= "|" & FieldName
                        '    ParameterRealName.Text &= "|" & ObjDr.Item("FieldName") & "=(" & FieldName
                        'Else
                        '    'TestStatment.Text &= "|" & ObjDr.Item("FieldName")
                        '    ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
                        'End If

                        Venus.Shared.Web.ClientSideActions.SetupForNumberOnly(mPage, ASPWN, True)

                        Arr(ValueCounter) = "WN_" & ObjDr.Item("ParameterField")
                        If ValueCounter = 0 Then
                            Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWN, False)
                        End If
                        Div1.Controls.Add(ASPWN)

                    Case 2 'Bit -Boolean Values 
                        Dim ASPWB As New Web.UI.WebControls.DropDownList
                        Dim ASPItem1 As New Web.UI.WebControls.ListItem
                        Dim ASPItem2 As New Web.UI.WebControls.ListItem
                        ASPItem1.Text = "True"
                        ASPItem1.Value = 0
                        ASPItem2.Text = "False"
                        ASPItem2.Value = 1
                        ASPWB.Items.Add(ASPItem1)
                        ASPWB.Items.Add(ASPItem2)

                        ASPWB.ID = "WB_" & ObjDr.Item("ParameterField")
                        CurrentTextBox = "WB_" & ObjDr.Item("ParameterField")
                        ASPWB.Style.Item("POSITION") = " absolute"
                        ASPWB.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                        ASPWB.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                        ASPWB.Style.Item("WIDTH") = CStr(Width) & "px"
                        ASPWB.BackColor = Drawing.Color.Silver

                        ASPWB.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                        ASPWB.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                        ASPWB.BorderColor = Drawing.Color.White

                        ParameterNames.Text &= "|" & "WB_" & ObjDr.Item("ParameterField")
                        ParameterRealName.Text &= "|" & ObjDr.Item("ParameterField")

                        Arr(ValueCounter) = "WB_" & ObjDr.Item("ParameterField")
                        If ValueCounter = 0 Then
                            Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWB, False)
                        End If
                        Div1.Controls.Add(ASPWB)

                    Case "Gdate", "Adate" ' DateTime  
                        Dim ASPWC As New Web.UI.WebControls.TextBox

                        ASPWC.ID = "WD_" & ObjDr.Item("ParameterField")
                        CurrentTextBox = "WD_" & ObjDr.Item("ParameterField")
                        ASPWC.Style.Item("POSITION") = " absolute"
                        ASPWC.Style.Item("LEFT") = CStr(X_Pos + 143) & "px"
                        ASPWC.Style.Item("TOP") = CStr(ValueCounter * 25 + Y_Pos) & "px"
                        ASPWC.Style.Item("WIDTH") = CStr(Width) & "px"
                        ASPWC.BackColor = Drawing.Color.Silver

                        ASPWC.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
                        ASPWC.BorderWidth = New Global.System.Web.UI.WebControls.Unit(1)
                        ASPWC.BorderColor = Drawing.Color.White

                        ParameterNames.Text &= "|" & "WD_" & ObjDr.Item("ParameterField")
                        ParameterRealName.Text &= "|" & ObjDr.Item("ParameterField")

                        Venus.Shared.Web.ClientSideActions.MaskedEdit(mPage, ASPWC, "##/##/####")

                        Arr(ValueCounter) = "WD_" & ObjDr.Item("ParameterField")
                        If ValueCounter = 0 Then
                            Venus.Shared.Web.ClientSideActions.SetFocus(mPage, ASPWC, False)
                        End If
                        Div1.Controls.Add(ASPWC)

                    Case "Elist"


                    Case "Alist"


                End Select
                'UPDATE BUTTON=========================================
                'If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then

                '    Dim SubSearchID As String = ObjDr.Item("SubSearchID")
                '    str = "<INPUT style='Z-INDEX: 103; LEFT: " & CStr(X_Pos + 390) & "px; POSITION: absolute; TOP: " & CStr(ValueCounter * 25 + Y_Pos) & "px; HEIGHT: 20px' type='button' value='...' id='" & "btn" & ObjDr.Item("EngName") & "' onclick=" & """" & "MainSearch_OpenWin('" & CurrentTextBox & "','" & SubSearchID & "');" & """" & ">"
                '    litral = New Web.UI.LiteralControl(str)
                '    Div1.Controls.Add(litral)

                'End If


                'UPDATE PAGE STYLE=====================================

                'If Not mPage.IsPostBack Then

                '    Dim DV2Top As Integer
                '    Dim DV2TopStr As String
                '    DV2TopStr = Div2.Style.Item("TOP")
                '    DV2Top = CInt(DV2TopStr.Substring(0, DV2TopStr.Length - 2))
                '    Div2.Style.Item("TOP") = DV2Top + 22 & "px"

                '    Dim DV3Top As Integer
                '    Dim DV3TopStr As String
                '    DV3TopStr = div3.Style.Item("TOP")
                '    DV3Top = CInt(DV3TopStr.Substring(0, DV3TopStr.Length - 2))
                '    div3.Style.Item("TOP") = DV3Top + 22 & "px"

                '    Dim DV1Hight As Integer
                '    Dim DV1HightStr As String

                '    DV1HightStr = Div1.Style.Item("HEIGHT")
                '    DV1Hight = CInt(DV1HightStr.Substring(0, DV1HightStr.Length - 2))

                '    Div1.Style.Item("HEIGHT") = DV1Hight + 20 & "px"

                'End If

                'RENDER NAMEING==============================
                Dim literalName As String = String.Empty
                If ObjDr.Item("EngCaption") Is DBNull.Value Then
                    literalName = ObjDr.Item("ParameterField")
                Else
                    literalName = ObjDr.Item("EngCaption")
                End If
                str = "<DIV style='DISPLAY: inline; font-size: small; Z-INDEX: 102; LEFT: " & CStr(X_Pos) & "px; WIDTH: 123px;border-right: white 1px outset; border-top: white 1px outset; border-left: white 1px outset; border-bottom: white 1px outset; POSITION: absolute; TOP: " & CStr(ValueCounter * 25 + Y_Pos) & "px; HEIGHT: 20px' ms_positioning='FlowLayout'>" & literalName & "</DIV>"
                litral = New Web.UI.LiteralControl(str)
                Div1.Controls.Add(litral)

                ValueCounter += 1

                If ParameterNames.Text <> "" Then
                    ParameterNames.Text = ParameterNames.Text.Substring(1)
                    ParameterRealName.Text = ParameterRealName.Text.Substring(1)
                End If
                Venus.Shared.Web.ClientSideActions.SetupTabOrder(mPage, Arr, Drawing.Color.White, False)
            End If


        Catch ex As Exception

        End Try
    End Function
    '==================================================================================
    'ProcedureName  : ViewData()
    'Module         : Hrs (Human Resource Module)
    'Project        : Venus V.
    'Description    : The Function Accepts DataSet and Fill with IsView Rows from sys_searchColumns Table 
    '               : according to searchID Sent to the function and Where String 
    'Developer      : Data Ocean
    '               : [AGR] Abdul Jaleel R. Ossman
    'Date Created   : DataOcean 
    'Date Modified  : 22-07-2007 
    'Modifacations  : 
    '               : Modification related to same modification in ClsSys_searchs AND ClsSys_SearchsColumns Class 
    '               :
    'Calling       *:*
    'Function Calls : 
    'Called From    : 
    '==================================================================================
    Public Function ViewData(ByVal PtrSearchID As Integer, ByVal Where As String, ByRef Ds As DataSet) As Boolean
        'Dim ObjDr As DataRow
        'Dim SqlSelectCommandBegin As String = String.Empty
        'Dim SqlSelectCommandCriteria As String = String.Empty
        'Dim SqlSelectCommandTable As String = String.Empty
        'Dim SqlSelectCommandWhere As String = Where
        'Dim SqlSelectCommand As String = String.Empty
        'Dim StrAlis As String = String.Empty
        'Dim ObjObjects As New Clssys_Objects(Me.mPage)

        'mObjSearchViewsColumns = New Clssys_SearchsColumns(mPage)
        'mObjSearchViews = New Clssys_Searchs(mPage)

        'Try
        '    mObjSearchViews.Find("ID=" & PtrSearchID)
        '    Dim SearchString As String = String.Empty
        '    'If mObjSearchViewsColumns.Find("SearchID=" & PtrSearchID & " And IsAvailable=1 and IsCriteria=0") Then
        '    If mObjSearchViewsColumns.Find("SearchID=" & PtrSearchID & " and ISVIEW =1 ") Then

        '        SqlSelectCommandBegin = "Select "
        '        For Each ObjDr In mObjSearchViewsColumns.DataSet.Tables(0).Rows
        '            'StrAlis = IIf(ObjDr.Item("ColumnEngDescription") Is DBNull.Value, ObjDr.Item("ColumnName"), ObjDr.Item("ColumnEngDescription"))
        '            StrAlis = IIf(ObjDr.Item("EngName") Is DBNull.Value, ObjDr.Item("FieldName"), ObjDr.Item("EngName"))
        '            'To Be Completed Later on 
        '            'If ObjDr.Item("SubSearchID") Is DBNull.Value Then
        '            '    SearchString = ObjDr.Item("FieldName")
        '            'Else
        '            '    'If Not ObjDr.Item("SubSearchID") Is DBNull.Value Then
        '            '    '    Dim StrObject As String = fnGetTableName(ObjDr.Item("SubSearchID"))
        '            '    '    FieldName = " Select engname from  " & StrObject & " WHERE id "
        '            '    '    'TestStatment.Text &= "|" & FieldName
        '            '    '    ParameterRealName.Text &= "|" & ObjDr.Item("FieldName") & "=(" & FieldName
        '            '    'Else
        '            '    '    'TestStatment.Text &= "|" & ObjDr.Item("FieldName")
        '            '    '    ParameterRealName.Text &= "|" & ObjDr.Item("FieldName")
        '            '    'End If
        '            'End If
        '            SqlSelectCommandCriteria &= "," & ObjDr.Item("FieldName") & " As [" & StrAlis & "]"
        '            'SqlSelectCommandCriteria &= "," & SearchString & " As [" & StrAlis & "]"
        '        Next

        '        ObjObjects.Find("ID=" & mObjSearchViews.ObjectID)
        '        SqlSelectCommandTable = ObjObjects.Code
        '        SqlSelectCommand = SqlSelectCommandBegin & SqlSelectCommandCriteria.Substring(1) & " From  " & SqlSelectCommandTable & IIf(Len(SqlSelectCommandWhere) > 1, " Where ", "") & SqlSelectCommandWhere

        '        mSqlDataAdapter = New SqlClient.SqlDataAdapter(SqlSelectCommand, mConnectionString)
        '        mSqlDataAdapter.Fill(Ds)
        '        If mDataHandler.CheckValidDataObject(Ds) Then
        '            Return True
        '        End If

        '    End If

        'Catch ex As Exception

        'End Try
    End Function
    '////////////////////////////////////////////////////////////////////////////////
    'GAD Fill Classes DataSets From Session
    Private Function SetReportsProperties(ByVal dtReportsProperties As Data.DataTable) As Boolean
        Try
            Dim ClsReportProperties As New ClsRpw_ReportsProperties(mPage)
            ClsReportProperties.Clear()
            With ClsReportProperties
                If dtReportsProperties.Rows.Count > 0 Then
                    '.ID = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ID"), SqlDbType.Int)
                    .Code = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("Code"), SqlDbType.VarChar)
                    .EngName = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("EngName"), SqlDbType.VarChar)
                    .ArbName = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ArbName"), SqlDbType.VarChar)
                    .EngTitle = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("EngTitle"), SqlDbType.VarChar)
                    .ArbTitle = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ArbTitle"), SqlDbType.VarChar)
                    .EngDescription = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("EngDescription"), SqlDbType.VarChar)
                    .ArbDescription = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ArbDescription"), SqlDbType.VarChar)
                    .ReportGroupID = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportGroupID"), SqlDbType.Int)
                    .ReportSource = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportSource"), SqlDbType.Int)
                    .RefreshTimeInterval = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("RefreshTimeInterval"), SqlDbType.Int)
                    .Rank = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("Rank"), SqlDbType.Int)
                    .CompanyLogo = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("CompanyLogo"), SqlDbType.Bit)
                    .CompanyText = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("CompanyText"), SqlDbType.Bit)
                    .CompanyHeader = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("CompanyHeader"), SqlDbType.Bit)
                    .IsLandscape = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("IsLandscape"), SqlDbType.Bit)
                    .IsRightToLeft = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("IsRightToLeft"), SqlDbType.Bit)
                    .ReportHeader = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportHeader"), SqlDbType.Bit)
                    .ReportFooter = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportFooter"), SqlDbType.Bit)
                    .PageHeader = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("PageHeader"), SqlDbType.Bit)
                    .PageFooter = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("PageFooter"), SqlDbType.Bit)
                    .DataSource = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("DataSource"), SqlDbType.VarChar)
                    .CRWName = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("CRWName"), SqlDbType.VarChar)
                    .HeaderBackColor = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("HeaderBackColor"), SqlDbType.VarChar)
                    .HeaderForeColor = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("HeaderForeColor"), SqlDbType.VarChar)
                    .HeaderFont = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("HeaderFont"), SqlDbType.VarChar)
                    .HeaderBorderColor = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("HeaderBorderColor"), SqlDbType.VarChar)
                    .HeaderBorderWidth = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("HeaderBorderWidth"), SqlDbType.VarChar)
                    .ReportTopMargins = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportTopMargins"), SqlDbType.Int)
                    .ReportBottomMargins = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportBottomMargins"), SqlDbType.Int)
                    .ReportLeftMargins = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportLeftMargins"), SqlDbType.Int)
                    .ReportRightMargins = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportRightMargins"), SqlDbType.Int)
                    .ReportBackColor = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportBackColor"), SqlDbType.VarChar)
                    .ReportForeColor = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportForeColor"), SqlDbType.VarChar)
                    .ReportAlternatingColor = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportAlternatingColor"), SqlDbType.VarChar)
                    .ReportFont = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("ReportFont"), SqlDbType.VarChar)
                    .ReportFontIsItalic = IIf(dtReportsProperties.Rows(0).Item("ReportFontIsItalic") = "Yes", True, False)
                    .ReportFontIsBold = IIf(dtReportsProperties.Rows(0).Item("ReportFontIsBold") = "Yes", True, False)
                    .ReportFontIsUnderLine = IIf(dtReportsProperties.Rows(0).Item("ReportFontIsUnderLine") = "Yes", True, False)
                    .ReportFontSize = GetFontSizesfromStrings(dtReportsProperties.Rows(0).Item("ReportFontSize"))
                    .HeaderFontIsItalic = IIf(dtReportsProperties.Rows(0).Item("HeaderFontIsItalic") = "Yes", True, False)
                    .HeaderFontIsBold = IIf(dtReportsProperties.Rows(0).Item("HeaderFontIsBold") = "Yes", True, False)
                    .HeaderFontIsUnderLine = IIf(dtReportsProperties.Rows(0).Item("HeaderFontIsUnderLine") = "Yes", True, False)
                    .HeaderFontSize = GetFontSizesfromStrings(dtReportsProperties.Rows(0).Item("HeaderFontSize"))
                    .RowsBorderColor = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("RowsBorderColor"), SqlDbType.VarChar)
                    .HeaderBorderStyle = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("BorderStyle"), SqlDbType.Int)
                    .HeaderValignment = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("HeaderValignment"), SqlDbType.Int)
                    .HeaderHalignment = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("HeaderHalignment"), SqlDbType.Int)
                    .FooterFont = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("FooterFont"), SqlDbType.VarChar)
                    .FooterFontIsItalic = IIf(dtReportsProperties.Rows(0).Item("FooterFontIsItalic") = "Yes", True, False)
                    .FooterFontIsBold = IIf(dtReportsProperties.Rows(0).Item("FooterFontIsBold") = "Yes", True, False)
                    .FooterFontIsUnderLine = IIf(dtReportsProperties.Rows(0).Item("FooterFontIsUnderLine") = "Yes", True, False)
                    .FooterFontSize = GetFontSizesfromStrings(dtReportsProperties.Rows(0).Item("FooterFontSize"))
                    .FooterHalignment = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("FooterHalignment"), SqlDbType.Int)
                    .FooterValignment = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("FooterValignment"), SqlDbType.Int)
                    .FooterBackColor = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("FooterBackColor"), SqlDbType.VarChar)
                    .FooterForeColor = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("FooterForeColor"), SqlDbType.VarChar)
                    .FooterBorderColor = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("FooterBorderColor"), SqlDbType.VarChar)
                    .FooterBorderStyle = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("FooterBorderStyle"), SqlDbType.Int)
                    .FooterBorderWidth = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("FooterBorderWidth"), SqlDbType.Int)
                    .HeaderHeight = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("HeaderHeight"), SqlDbType.Int)
                    .FooterHeight = mDataHandler.DataValue_Out(dtReportsProperties.Rows(0).Item("FooterHeight"), SqlDbType.Int)
                End If
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function SetRepColumnsProperties(ByVal IntReportID As Integer, ByVal dtColumnsProperties As Data.DataTable) As Boolean
        Try
            Dim ClsColumnProperties As New ClsRpw_ReportColumnsProperties(mPage)


            ClsColumnProperties.Clear()
            With ClsColumnProperties
                If dtColumnsProperties.Rows.Count > 0 Then
                    '.ID = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ID"), SqlDbType.Int)
                    '.ReportID = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ReportID"), SqlDbType.Int)
                    .ReportID = IntReportID
                    .Rank = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("Rank"), SqlDbType.Int)
                    .BorderColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("BorderColor"), SqlDbType.VarChar)
                    .TopBorderColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("TopBorderColor"), SqlDbType.VarChar)
                    .BottomBorderColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("BottomBorderColor"), SqlDbType.VarChar)
                    .LeftBorderColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("LeftBorderColor"), SqlDbType.VarChar)
                    .RightBorderColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("RightBorderColor"), SqlDbType.VarChar)
                    .BorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("BorderStyle"), SqlDbType.Int)
                    .TopBorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("TopBorderStyle"), SqlDbType.Int)
                    .BottomBorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("BottomBorderStyle"), SqlDbType.Int)
                    .LeftBorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("LeftBorderStyle"), SqlDbType.Int)
                    .RightBorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("RightBorderStyle"), SqlDbType.Int)
                    .BorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("BorderWidth"), SqlDbType.Int)
                    .TopBorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("TopBorderWidth"), SqlDbType.Int)
                    .BottomBorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("BottomBorderWidth"), SqlDbType.Int)
                    .LeftBorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("LeftBorderWidth"), SqlDbType.Int)
                    .RightBorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("RightBorderWidth"), SqlDbType.Int)

                    .EngHeaderCaption = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("EngHeaderCaption"), SqlDbType.VarChar)
                    .EngFooterCaption = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("EngFooterCaption"), SqlDbType.VarChar)
                    .ArbHeaderCaption = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ArbHeaderCaption"), SqlDbType.VarChar)
                    .ArbFooterCaption = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ArbFooterCaption"), SqlDbType.VarChar)

                    .ColumnWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ColumnWidth"), SqlDbType.Int)
                    .ColumnName = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ColumnName"), SqlDbType.VarChar)
                    .ColumnHalignment = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ColumnHalignment"), SqlDbType.Int)
                    .ColumnValignment = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ColumnValignment"), SqlDbType.Int)
                    .ColumnIsHidden = IIf(dtColumnsProperties.Rows(0).Item("ColumnIsHidden") = "Yes", True, False)
                    .ColumnIsOverView = IIf(dtColumnsProperties.Rows(0).Item("ColumnIsOverView") = "Yes", True, False)
                    .ColumnIsWrap = IIf(dtColumnsProperties.Rows(0).Item("ColumnIsWrap") = "Yes", True, False)
                    .ColumnForeColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ColumnForeColor"), SqlDbType.VarChar)
                    .ColumnBackColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ColumnBackColor"), SqlDbType.VarChar)
                    .ColumnFont = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("ColumnFont"), SqlDbType.VarChar)
                    .ColumnFontIsBold = IIf(dtColumnsProperties.Rows(0).Item("ColumnFontIsBold") = "Yes", True, False)
                    .ColumnFontIsItalic = IIf(dtColumnsProperties.Rows(0).Item("ColumnFontIsItalic") = "Yes", True, False)
                    .ColumnFontIsUnderLine = IIf(dtColumnsProperties.Rows(0).Item("ColumnFontIsUnderLine") = "Yes", True, False)
                    .ColumnFontSize = GetFontSizesfromStrings(dtColumnsProperties.Rows(0).Item("ColumnFontSize"))
                    .TopPadding = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("TopPadding"), SqlDbType.Int)
                    .BottomPadding = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("BottomPadding"), SqlDbType.Int)
                    .LeftPadding = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("LeftPadding"), SqlDbType.Int)
                    .RightPadding = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("RightPadding"), SqlDbType.Int)
                    .TopMargin = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("TopMargin"), SqlDbType.Int)
                    .BottomMargin = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("BottomMargin"), SqlDbType.Int)
                    .LeftMargin = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("LeftMargin"), SqlDbType.Int)
                    .RightMargin = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("RightMargin"), SqlDbType.Int)
                    .DefaultValue = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("DefaultValue"), SqlDbType.VarChar)
                    .Format = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("Format"), SqlDbType.VarChar)
                    .FooterTotal = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("FooterTotal"), SqlDbType.VarChar)
                    .IsSorted = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("IsSorted"), SqlDbType.Bit)
                    .IsGroupBy = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(0).Item("IsGroupBy"), SqlDbType.Bit)
                End If
            End With

        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    Private Function SetRepRowsProperties(ByVal IntReportID As Integer, ByVal dtRowsProperties As Data.DataTable) As Boolean
        Try
            Dim ClsRowProperties As New ClsRpw_ReportRowsProperties(mPage)

            ClsRowProperties.Clear()

            With ClsRowProperties
                If dtRowsProperties.Rows.Count > 0 Then
                    '.ID = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("ID"), SqlDbType.Int)
                    '.ReportID = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("ReportID"), SqlDbType.Int)
                    .ReportID = IntReportID
                    .ForeColor = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("ForeColor"), SqlDbType.VarChar)
                    .BackColor = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("BackColor"), SqlDbType.VarChar)
                    .Font = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("Font"), SqlDbType.VarChar)
                    .FontIsBold = IIf(dtRowsProperties.Rows(0).Item("FontIsBold") = "Yes", True, False)
                    .FontIsItalic = IIf(dtRowsProperties.Rows(0).Item("FontIsItalic") = "Yes", True, False)
                    .FontIsUnderLine = IIf(dtRowsProperties.Rows(0).Item("FontIsUnderLine") = "Yes", True, False)
                    .FontSize = GetFontSizesfromStrings(dtRowsProperties.Rows(0).Item("FontSize"))
                    .RowHieght = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("RowHieght"), SqlDbType.Int)
                    .BorderColor = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("BorderColor"), SqlDbType.VarChar)
                    .TopBorderColor = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("TopBorderColor"), SqlDbType.VarChar)
                    .BottomBorderColor = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("BottomBorderColor"), SqlDbType.VarChar)
                    .LeftBorderColor = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("LeftBorderColor"), SqlDbType.VarChar)
                    .RightBorderColor = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("RightBorderColor"), SqlDbType.VarChar)
                    .BorderStyle = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("BorderStyle"), SqlDbType.Int)
                    .TopBorderStyle = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("TopBorderStyle"), SqlDbType.Int)
                    .BottomBorderStyle = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("BottomBorderStyle"), SqlDbType.Int)
                    .LeftBorderStyle = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("LeftBorderStyle"), SqlDbType.Int)
                    .RightBorderStyle = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("RightBorderStyle"), SqlDbType.Int)
                    .BorderWidth = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("BorderWidth"), SqlDbType.Int)
                    .TopBorderWidth = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("TopBorderWidth"), SqlDbType.Int)
                    .BottomBorderWidth = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("BottomBorderWidth"), SqlDbType.Int)
                    .LeftBorderWidth = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("LeftBorderWidth"), SqlDbType.Int)
                    .RightBorderWidth = mDataHandler.DataValue_Out(dtRowsProperties.Rows(0).Item("RightBorderWidth"), SqlDbType.Int)
                End If
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function


    '//////////////////////////////////////////////////////////////////
    Private Function GetReportsProperties(ByVal IntReportID As Integer, ByRef dtTargetTable As Data.DataTable) As Boolean
        Dim ClsReportProperties As New ClsRpw_ReportsProperties(mPage)
        Try
            ClsReportProperties.Find(" ID= " & IntReportID)
            With ClsReportProperties
                If dtTargetTable.Rows.Count > 0 Then
                    dtTargetTable.Rows(0).Item("ID") = mDataHandler.DataValue_In(.ID, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("Code") = mDataHandler.DataValue_In(.Code, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("EngName") = mDataHandler.DataValue_In(.EngName, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ArbName") = mDataHandler.DataValue_In(.ArbName, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("EngTitle") = mDataHandler.DataValue_In(.EngTitle, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ArbTitle") = mDataHandler.DataValue_In(.ArbTitle, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("EngDescription") = mDataHandler.DataValue_In(.EngDescription, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ArbDescription") = mDataHandler.DataValue_In(.ArbDescription, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ReportGroupID") = mDataHandler.DataValue_In(.ReportGroupID, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ReportSource") = mDataHandler.DataValue_In(.ReportSource, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("RefreshTimeInterval") = mDataHandler.DataValue_In(.RefreshTimeInterval, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("Rank") = mDataHandler.DataValue_In(.Rank, SqlDbType.Int)

                    dtTargetTable.Rows(0).Item("CompanyLogo") = mDataHandler.DataValue_In(.CompanyLogo, SqlDbType.Bit)
                    dtTargetTable.Rows(0).Item("CompanyText") = mDataHandler.DataValue_In(.CompanyText, SqlDbType.Bit)
                    dtTargetTable.Rows(0).Item("CompanyHeader") = mDataHandler.DataValue_In(.CompanyHeader, SqlDbType.Bit)
                    dtTargetTable.Rows(0).Item("IsLandscape") = mDataHandler.DataValue_In(.IsLandscape, SqlDbType.Bit)
                    dtTargetTable.Rows(0).Item("IsRightToLeft") = mDataHandler.DataValue_In(.IsRightToLeft, SqlDbType.Bit)
                    dtTargetTable.Rows(0).Item("ReportHeader") = mDataHandler.DataValue_In(.ReportHeader, SqlDbType.Bit)
                    dtTargetTable.Rows(0).Item("ReportFooter") = mDataHandler.DataValue_In(.ReportFooter, SqlDbType.Bit)
                    dtTargetTable.Rows(0).Item("PageHeader") = mDataHandler.DataValue_In(.PageHeader, SqlDbType.Bit)
                    dtTargetTable.Rows(0).Item("PageFooter") = mDataHandler.DataValue_In(.PageFooter, SqlDbType.Bit)

                    dtTargetTable.Rows(0).Item("DataSource") = mDataHandler.DataValue_In(.DataSource, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("CRWName") = mDataHandler.DataValue_In(.CRWName, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("HeaderBackColor") = mDataHandler.DataValue_In(.HeaderBackColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("HeaderForeColor") = mDataHandler.DataValue_In(.HeaderForeColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("HeaderFont") = mDataHandler.DataValue_In(.HeaderFont, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("HeaderBorderColor") = mDataHandler.DataValue_In(.HeaderBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("HeaderBorderWidth") = mDataHandler.DataValue_In(.HeaderBorderWidth, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ReportTopMargins") = mDataHandler.DataValue_In(.ReportTopMargins, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ReportBottomMargins") = mDataHandler.DataValue_In(.ReportBottomMargins, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ReportLeftMargins") = mDataHandler.DataValue_In(.ReportLeftMargins, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ReportRightMargins") = mDataHandler.DataValue_In(.ReportRightMargins, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ReportBackColor") = mDataHandler.DataValue_In(.ReportBackColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ReportForeColor") = mDataHandler.DataValue_In(.ReportForeColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ReportAlternatingColor") = mDataHandler.DataValue_In(.ReportAlternatingColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ReportFont") = mDataHandler.DataValue_In(.ReportFont, SqlDbType.VarChar)

                    dtTargetTable.Rows(0).Item("ReportFontIsItalic") = IIf(.ReportFontIsItalic = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("ReportFontIsBold") = IIf(.ReportFontIsBold = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("ReportFontIsUnderLine") = IIf(.ReportFontIsUnderLine = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("ReportFontSize") = SetFontSizes(.ReportFontSize)

                    dtTargetTable.Rows(0).Item("HeaderFontIsItalic") = IIf(.HeaderFontIsItalic = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("HeaderFontIsBold") = IIf(.HeaderFontIsBold = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("HeaderFontIsUnderLine") = IIf(.HeaderFontIsUnderLine = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("HeaderFontSize") = SetFontSizes(.HeaderFontSize)

                    dtTargetTable.Rows(0).Item("RowsBorderColor") = mDataHandler.DataValue_In(.RowsBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("BorderStyle") = mDataHandler.DataValue_In(.HeaderBorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("HeaderValignment") = mDataHandler.DataValue_In(.HeaderValignment, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("HeaderHalignment") = mDataHandler.DataValue_In(.HeaderHalignment, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("FooterFont") = mDataHandler.DataValue_In(.FooterFont, SqlDbType.VarChar)

                    dtTargetTable.Rows(0).Item("FooterFontIsItalic") = IIf(.FooterFontIsItalic = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("FooterFontIsBold") = IIf(.FooterFontIsBold = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("FooterFontIsUnderLine") = IIf(.FooterFontIsUnderLine = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("FooterFontSize") = SetFontSizes(.FooterFontSize)

                    dtTargetTable.Rows(0).Item("FooterHalignment") = mDataHandler.DataValue_In(.FooterHalignment, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("FooterValignment") = mDataHandler.DataValue_In(.FooterValignment, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("FooterBackColor") = mDataHandler.DataValue_In(.FooterBackColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("FooterForeColor") = mDataHandler.DataValue_In(.FooterForeColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("FooterBorderColor") = mDataHandler.DataValue_In(.FooterBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("FooterBorderStyle") = mDataHandler.DataValue_In(.FooterBorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("FooterBorderWidth") = mDataHandler.DataValue_In(.FooterBorderWidth, SqlDbType.Int)
                End If
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function GetRepColumnsProperties(ByVal IntReportID As Integer, ByRef dtTargetTable As Data.DataTable) As Boolean
        Dim ClsColumnProperties As New ClsRpw_ReportColumnsProperties(mPage)
        Try
            ClsColumnProperties.Find(" ReportID= " & IntReportID)
            With ClsColumnProperties
                If dtTargetTable.Rows.Count > 0 Then
                    dtTargetTable.Rows(0).Item("ID") = mDataHandler.DataValue_In(.ID, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ReportID") = mDataHandler.DataValue_In(.ReportID, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("Rank") = mDataHandler.DataValue_In(.Rank, SqlDbType.Int)

                    dtTargetTable.Rows(0).Item("BorderColor") = mDataHandler.DataValue_In(.BorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("TopBorderColor") = mDataHandler.DataValue_In(.TopBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("BottomBorderColor") = mDataHandler.DataValue_In(.BottomBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("LeftBorderColor") = mDataHandler.DataValue_In(.LeftBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("RightBorderColor") = mDataHandler.DataValue_In(.RightBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("BorderStyle") = mDataHandler.DataValue_In(.BorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("TopBorderStyle") = mDataHandler.DataValue_In(.TopBorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("BottomBorderStyle") = mDataHandler.DataValue_In(.BottomBorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("LeftBorderStyle") = mDataHandler.DataValue_In(.LeftBorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("RightBorderStyle") = mDataHandler.DataValue_In(.RightBorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("BorderWidth") = mDataHandler.DataValue_In(.BorderWidth, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("TopBorderWidth") = mDataHandler.DataValue_In(.TopBorderWidth, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("BottomBorderWidth") = mDataHandler.DataValue_In(.BottomBorderWidth, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("LeftBorderWidth") = mDataHandler.DataValue_In(.LeftBorderWidth, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("RightBorderWidth") = mDataHandler.DataValue_In(.RightBorderWidth, SqlDbType.Int)

                    dtTargetTable.Rows(0).Item("EngHeaderCaption") = mDataHandler.DataValue_In(.EngHeaderCaption, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("EngFooterCaption") = mDataHandler.DataValue_In(.EngFooterCaption, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ArbHeaderCaption") = mDataHandler.DataValue_In(.ArbHeaderCaption, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ArbFooterCaption") = mDataHandler.DataValue_In(.ArbFooterCaption, SqlDbType.VarChar)

                    dtTargetTable.Rows(0).Item("ColumnWidth") = mDataHandler.DataValue_In(.ColumnWidth, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ColumnName") = mDataHandler.DataValue_In(.ColumnName, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ColumnHalignment") = mDataHandler.DataValue_In(.ColumnHalignment, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ColumnValignment") = mDataHandler.DataValue_In(.ColumnValignment, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ColumnIsHidden") = IIf(.ColumnIsHidden = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("ColumnIsOverView") = IIf(.ColumnIsOverView = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("ColumnIsWrap") = IIf(.ColumnIsWrap = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("ColumnForeColor") = mDataHandler.DataValue_In(.ColumnForeColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ColumnBackColor") = mDataHandler.DataValue_In(.ColumnBackColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ColumnFont") = mDataHandler.DataValue_In(.ColumnFont, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("ColumnFontIsBold") = IIf(.ColumnFontIsBold = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("ColumnFontIsItalic") = IIf(.ColumnFontIsItalic = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("ColumnFontIsUnderLine") = IIf(.ColumnFontIsUnderLine = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("ColumnFontSize") = SetFontSizes(.ColumnFontSize)

                    dtTargetTable.Rows(0).Item("TopPadding") = mDataHandler.DataValue_In(.TopPadding, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("BottomPadding") = mDataHandler.DataValue_In(.BottomPadding, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("LeftPadding") = mDataHandler.DataValue_In(.LeftPadding, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("RightPadding") = mDataHandler.DataValue_In(.RightPadding, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("TopMargin") = mDataHandler.DataValue_In(.TopMargin, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("BottomMargin") = mDataHandler.DataValue_In(.BottomMargin, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("LeftMargin") = mDataHandler.DataValue_In(.LeftMargin, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("RightMargin") = mDataHandler.DataValue_In(.RightMargin, SqlDbType.Int)

                    dtTargetTable.Rows(0).Item("DefaultValue") = mDataHandler.DataValue_In(.DefaultValue, SqlDbType.Variant)
                    dtTargetTable.Rows(0).Item("Format") = mDataHandler.DataValue_In(.Format, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("FooterTotal") = mDataHandler.DataValue_In(.FooterTotal, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("IsSorted") = mDataHandler.DataValue_In(.IsSorted, SqlDbType.Bit)
                    dtTargetTable.Rows(0).Item("IsGroupBy") = mDataHandler.DataValue_In(.IsGroupBy, SqlDbType.Bit)
                End If
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function GetRepRowsProperties(ByVal IntReportID As Integer, ByRef dtTargetTable As Data.DataTable) As Boolean
        Dim ClsRowProperties As New ClsRpw_ReportRowsProperties(mPage)
        Try
            ClsRowProperties.Find(" ReportID= " & IntReportID)
            With ClsRowProperties
                If dtTargetTable.Rows.Count > 0 Then
                    dtTargetTable.Rows(0).Item("ID") = mDataHandler.DataValue_In(.ID, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ReportID") = mDataHandler.DataValue_In(.ReportID, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("ForeColor") = mDataHandler.DataValue_In(.ForeColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("BackColor") = mDataHandler.DataValue_In(.BackColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("Font") = mDataHandler.DataValue_In(.Font, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("FontIsItalic") = IIf(.FontIsItalic = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("FontIsBold") = IIf(.FontIsBold = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("FontIsUnderLine") = IIf(.FontIsUnderLine = True, "Yes", "No")
                    dtTargetTable.Rows(0).Item("FontSize") = SetFontSizes(.FontSize)
                    dtTargetTable.Rows(0).Item("RowHieght") = mDataHandler.DataValue_In(.RowHieght, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("BorderColor") = mDataHandler.DataValue_In(.BorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("TopBorderColor") = mDataHandler.DataValue_In(.TopBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("BottomBorderColor") = mDataHandler.DataValue_In(.BottomBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("LeftBorderColor") = mDataHandler.DataValue_In(.LeftBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("RightBorderColor") = mDataHandler.DataValue_In(.RightBorderColor, SqlDbType.VarChar)
                    dtTargetTable.Rows(0).Item("BorderStyle") = mDataHandler.DataValue_In(.BorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("TopBorderStyle") = mDataHandler.DataValue_In(.TopBorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("BottomBorderStyle") = mDataHandler.DataValue_In(.BottomBorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("LeftBorderStyle") = mDataHandler.DataValue_In(.LeftBorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("RightBorderStyle") = mDataHandler.DataValue_In(.RightBorderStyle, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("BorderWidth") = mDataHandler.DataValue_In(.BorderWidth, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("TopBorderWidth") = mDataHandler.DataValue_In(.TopBorderWidth, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("BottomBorderWidth") = mDataHandler.DataValue_In(.BottomBorderWidth, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("LeftBorderWidth") = mDataHandler.DataValue_In(.LeftBorderWidth, SqlDbType.Int)
                    dtTargetTable.Rows(0).Item("RightBorderWidth") = mDataHandler.DataValue_In(.RightBorderWidth, SqlDbType.Int)
                End If
            End With
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function



#Region "Public Set Methods"

    Public Function SetHorizontalAlignment(ByVal AlignType As Web.UI.WebControls.HorizontalAlign) As Int32
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

    Public Function GetHorizontalAlignment(ByVal intVal As Integer) As Web.UI.WebControls.HorizontalAlign
        Select Case intVal
            Case 0
                Return Web.UI.WebControls.HorizontalAlign.NotSet
            Case 1
                Return Web.UI.WebControls.HorizontalAlign.Left
            Case 2
                Return Web.UI.WebControls.HorizontalAlign.Center
            Case 3
                Return Web.UI.WebControls.HorizontalAlign.Right
            Case 4
                Return Web.UI.WebControls.HorizontalAlign.Justify

        End Select
    End Function
    Public Function GetVerticalAlignment(ByVal intVal As Integer) As Web.UI.WebControls.VerticalAlign
        Select Case intVal
            Case 0
                Return Web.UI.WebControls.VerticalAlign.NotSet
            Case 1
                Return Web.UI.WebControls.VerticalAlign.Top
            Case 2
                Return Web.UI.WebControls.VerticalAlign.Middle
            Case 3
                Return Web.UI.WebControls.VerticalAlign.Bottom
        End Select
    End Function

    Public Function GetBorderStyle(ByVal intVal As Integer) As Web.UI.WebControls.BorderStyle
        Select Case intVal
            Case 0
                Return Web.UI.WebControls.BorderStyle.NotSet
            Case 1
                Return Web.UI.WebControls.BorderStyle.None
            Case 2
                Return Web.UI.WebControls.BorderStyle.Dotted
            Case 3
                Return Web.UI.WebControls.BorderStyle.Dashed
            Case 4
                Return Web.UI.WebControls.BorderStyle.Solid
            Case 5
                Return Web.UI.WebControls.BorderStyle.Double
            Case 6
                Return Web.UI.WebControls.BorderStyle.Groove
            Case 7
                Return Web.UI.WebControls.BorderStyle.Ridge
            Case 8
                Return Web.UI.WebControls.BorderStyle.Inset
            Case 9
                Return Web.UI.WebControls.BorderStyle.Outset
        End Select
    End Function

    Public Function SetBorderStyle(ByVal ObjBorderType As Web.UI.WebControls.BorderStyle) As Int32
        Select Case ObjBorderType
            Case Web.UI.WebControls.BorderStyle.NotSet
                Return 0
            Case Web.UI.WebControls.BorderStyle.None
                Return 1
            Case Web.UI.WebControls.BorderStyle.Dotted
                Return 2
            Case Web.UI.WebControls.BorderStyle.Dashed
                Return 3
            Case Web.UI.WebControls.BorderStyle.Solid
                Return 4
            Case Web.UI.WebControls.BorderStyle.Double
                Return 5
            Case Web.UI.WebControls.BorderStyle.Groove
                Return 6
            Case Web.UI.WebControls.BorderStyle.Ridge
                Return 7
            Case Web.UI.WebControls.BorderStyle.Inset
                Return 8
            Case Web.UI.WebControls.BorderStyle.Outset
                Return 9
        End Select
    End Function
#End Region

#End Region

#Region "Public Methods"

    Public Function GetFontSizes(ByVal strFontSize As Web.UI.WebControls.FontSize) As Integer
        If IsNothing(strFontSize) Then Return 0
        Select Case strFontSize
            Case Web.UI.WebControls.FontSize.Small
                Return 6
            Case Web.UI.WebControls.FontSize.Smaller
                Return 2
            Case Web.UI.WebControls.FontSize.XXSmall
                Return 4
            Case Web.UI.WebControls.FontSize.XXLarge
                Return 10
            Case Web.UI.WebControls.FontSize.XSmall
                Return 5
            Case Web.UI.WebControls.FontSize.XLarge
                Return 9
            Case Web.UI.WebControls.FontSize.Medium
                Return 7
            Case Web.UI.WebControls.FontSize.Larger
                Return 3
            Case Web.UI.WebControls.FontSize.Large
                Return 8
            Case Web.UI.WebControls.FontSize.AsUnit
                Return 1
            Case Else
                Return 0
        End Select
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
                Return 0
        End Select
    End Function

    Public Function GetFontSizesfromStrings(ByVal strFontSize As String) As Integer
        Select Case strFontSize.ToUpper
            Case "SMALL"
                Return 6
            Case "SMALLER"
                Return 2
            Case "XX-SMALL"
                Return 4
            Case "XX-LARGE"
                Return 10
            Case "X-SMALL"
                Return 5
            Case "X-LARGE"
                Return 9
            Case "MEDUIM"
                Return 7
            Case "LARGER"
                Return 3
            Case "LARGE"
                Return 8
            Case "AS UNIT"
                Return 1
            Case Else
                Return 8
        End Select
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

    Public Function GetFooterTotalStrings(ByVal IntTotalType As Integer) As String
        Select Case IntTotalType
            Case 0
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Text.ToString
            Case 1
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum.ToString
            Case 2
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Avg.ToString
            Case 3
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Min.ToString
            Case 4
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Max.ToString
            Case 5
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Count.ToString
            Case 6
                Return Infragistics.WebUI.UltraWebGrid.SummaryInfo.Formula.ToString

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

    'Private Function finalColorList() As List(Of String)
    ' Dim allSummaries() As String = System.Enum.GetNames(GetType(Infragistics.WebUI.UltraWebGrid.SummaryInfo))

    '    Dim allColors() As String = System.Enum.GetNames(GetType(Drawing.KnownColor))
    '    Dim systemEnvironmentColors((GetType(Drawing.SystemColors)).GetProperties().Length) As String
    '    Dim index As Integer = 0
    '    Dim fCList As New List(Of String)
    '    For Each member As MemberInfo In ((GetType(Drawing.SystemColors)).GetProperties())
    '        systemEnvironmentColors(index) = member.Name
    '        index = index + 1
    '    Next
    '    For Each color As String In allColors
    '        If (Array.IndexOf(systemEnvironmentColors, color) < 0) Then
    '            fCList.Add(color)
    '        End If
    '    Next
    '    Return fCList

    'End Function

    Public Function CustomizeStyle(ByVal StyleNo As Integer) As Data.DataTable
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

    '========================================================================
    'ProcedureName  :  GetStordList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill Value List with stored English name column 
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  04-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try

    End Function

    'GAD
    Public Function ExecuateStoredProcedure(ByVal strProcName As String) As Data.DataSet
        Dim dsProcDataset As New Data.DataSet

        dsProcDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, strProcName)

        If Not IsNothing(dsProcDataset) Then Return dsProcDataset

    End Function
    'GAD
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
        dsProcDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, strProcName, parmArray)
        If Not IsNothing(dsProcDataset) Then Return dsProcDataset
    End Function


    Public Function GetStoredParameters(ByVal strProcName As String, ByVal strParmStoredName As String) As Data.DataSet
        Dim dsProcDataset As New Data.DataSet
        dsProcDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, strProcName, strParmStoredName)
        If Not IsNothing(dsProcDataset) Then Return dsProcDataset
    End Function






#End Region



End Class

