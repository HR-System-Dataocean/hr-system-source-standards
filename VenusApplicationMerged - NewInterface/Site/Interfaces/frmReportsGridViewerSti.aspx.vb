Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Infragistics.WebUI.UltraWebGrid
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class Interfaces_frmReportsGridViewerSti
    Inherits System.Web.UI.Page

#Region "Constant"
    Private ClsReports As ClsRpw_ReportsProperties
    Private ClsReportsCriterias As ClsRpw_ReportsCriterias
    Private ClsReportsColumns As ClsRpw_ReportColumnsProperties
    Private ClsReportsMain As ClsRpw_ReportsMain

    Const uwgCheck = 0
    Const uwgName = 1
    Const uwgID = 3
    Const uwgEngName = 2
    Const uwgArbName = 4
    Const uwgSubSearchID = 5
    Const uwgFieldType = 6

    Const CSave = 1
    Const CDelete = 3
    Const CRefresh = 5

    Const RegUser = 2
    Const RegDate = 6
    Const CancelDate = 10
#End Region

#Region "Protected Sub"
    '========================================================================
    'ProcedureName  :  InitializeCulture 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  
    '                1-Get language form Cookies 
    '                2-Create Calture from the incoming language 
    '                3-Apply the calture on the current form
    '                4-
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

        If (_culture <> "en-US") Then
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
        Dim StrCriteria As String = Request.QueryString.Item("Criteria")
        Dim StrReportCode As String = Request.QueryString.Item("ReportCode")
        Dim StrIncomingLanguage As String = Request.QueryString.Item("Language")
        Dim StrSqlNames As String = Request.QueryString.Item("sq0")
        Dim strV As String = Request.QueryString.Item("v")
        Dim StrNames() As String = StrCriteria.Split("|")
        Dim strSql() As String = StrSqlNames.Split("|")
        Dim strSqlv() As String = strV.Split("|")
        Dim intPreview As Integer = Request.QueryString.Item("preview")
        Dim IntReportID As Integer
        Dim dsTemp As New DataSet
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        Dim _culture As String = StrLanguage
        Dim ObjDtReportViewColumn As New DataTable
        Dim ObjDsReportProperties As New DataSet
        Dim ObjDtReportCriteriaColumn As New Data.DataTable

        'ClsReports = New ClsRpw_ReportsProperties(Page)
        'ClsReportsColumns = New ClsRpw_ReportColumnsProperties(Page)
        'ClsReportsCriterias = New ClsRpw_ReportsCriterias(Me.Page)
        'ClsReportsMain = New ClsRpw_ReportsMain(Me.Page)
        'StrSqlNames = StrSqlNames.Replace("|", " AND ")
        'StrSqlNames = StrSqlNames.Replace("$", "%")
        'WebHandler.GetCookies(Page, "Lang", StrLanguage)

        'ClsReports.Find("Code='" & StrReportCode & "'")
        'IntReportID = ClsReports.ID
        'ClsReportsCriterias.Find("ReportID = " & ClsReports.ID & " Order By Rank")
        'ClsReportsColumns.Find("ReportID = " & ClsReports.ID & " Order By Rank")
        'ClsReportsCriterias.CreateParametersForView(pnlCriterias, ClsReportsCriterias.DataSet.Tables(0), StrNames, strSqlv)
        'ObjDsReportProperties = ClsReports.CreateDataSet4Designer()
        'ObjDtReportCriteriaColumn = ClsReportsCriterias.DataSet.Tables(0)
        'If Not IsPostBack Then
        '    clearSession()
        '    ClsReports.GetReportsProperties(IntReportID, ObjDsReportProperties)
        '    ClsReports.GenerateFieldsDs(ObjDtReportViewColumn, IntReportID, True)
        'Else
        '    ObjDtReportViewColumn = Session("SessionView")
        '    ObjDsReportProperties = Session("SessionReportProperties")
        '    ObjDtReportCriteriaColumn = Session("SessionCriteria")
        'End If

        'Session("SessionReportProperties") = ObjDsReportProperties
        'Session("SessionView") = ObjDtReportViewColumn
        'Session("SessionCriteria") = ObjDtReportCriteriaColumn

        If intPreview = 1 Then
            Response.Redirect("frmReportViewerSti.aspx?Language=" & IIf(StrIncomingLanguage = "true", 1, 0))
        ElseIf intPreview = 2 Then
            Session("rpt") = "XLS"
            Response.Redirect("frmReportViewerSti.aspx?Language=" & IIf(StrIncomingLanguage = "true", 1, 0))
        End If
    End Sub
#End Region

#Region "Private Function"
    Public Function GenerateGridColumns(ByRef uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal dtDataTable As Data.DataTable) As Boolean
        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim ClsReports As New Clssys_Reports(Page)
        Dim mErrorHandler As New Venus.Shared.Errors.ErrorsHandler(ClsReports.ConnectionString)
        Dim ugcColumn As Infragistics.WebUI.UltraWebGrid.UltraGridColumn
        Dim Webhandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        Dim TempObj As Object
        uwgGrid.Columns.Clear()
        Dim strDataType As String
        Dim _culture As String
        Webhandler.GetCookies(Page, "Lang", StrLanguage)
        _culture = StrLanguage
        Try
            uwgGrid.Columns.Clear()
            dtDataTable.DefaultView.Sort = " Rank "
            dtDataTable = dtDataTable.DefaultView.ToTable()
            For Each drRow As Data.DataRow In dtDataTable.Rows
                strDataType = mDataHandler.DataValue_Out(drRow.Item("DataType"), Data.SqlDbType.VarChar)
                ugcColumn = New Infragistics.WebUI.UltraWebGrid.UltraGridColumn
                ugcColumn.BaseColumnName = mDataHandler.DataValue_Out(drRow.Item("FieldName"), Data.SqlDbType.VarChar)
                ugcColumn.Key = mDataHandler.DataValue_Out(drRow.Item("FieldName"), Data.SqlDbType.VarChar)
                ugcColumn.DataType = mDataHandler.DataValue_Out(drRow.Item("DataType"), Data.SqlDbType.VarChar)
                If (_culture <> "en-US") Then
                    TempObj = mDataHandler.DataValue_Out(drRow.Item("ArbDescription"), Data.SqlDbType.VarChar)
                Else
                    TempObj = mDataHandler.DataValue_Out(drRow.Item("EngDescription"), Data.SqlDbType.VarChar)
                End If
                ugcColumn.SortIndicator = IIf(IsDBNull(drRow.Item("IsSorted")), 0, drRow.Item("IsSorted"))
                ugcColumn.Header.Caption = IIf(TempObj Is DBNull.Value Or IsNothing(TempObj) Or TempObj.ToString.Length > 0, TempObj, mDataHandler.DataValue_Out(drRow.Item("FieldName"), Data.SqlDbType.VarChar))
                ugcColumn.Width = System.Web.UI.WebControls.Unit.Pixel(IIf(IsDBNull(drRow.Item("ColumnWidth")), 100, drRow.Item("ColumnWidth")))
                uwgGrid.Columns.Add(ugcColumn)
            Next
        Catch ex As Exception
            Me.Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ClsReports.DataBaseUserRelatedID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Me.Page.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
    Private Function SetGridData(ByRef uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal strCriteria As String, ByVal ReportSourceType As Integer, ByVal DataSource As String, ByVal DtCriteria As Data.DataTable) As Data.DataSet
        Try
            Dim strSQLquery As String = String.Empty
            Dim dSDataSet As New Data.DataSet
            Dim dsWorkingDs As New Data.DataSet
            Dim strFieldsArray(,) As String = {}
            Dim dsWorkingDataSet As New Data.DataSet
            'Replace Column Name with its orignal names 

            Select Case ReportSourceType
                Case 0
                    If DataSource.IndexOf("Where") > 0 Then
                        DataSource &= IIf(strCriteria.Length > 0, " And " & strCriteria, " ")
                    Else
                        DataSource &= IIf(strCriteria.Length > 0, " Where " & strCriteria, " ")
                    End If
                    dsWorkingDataSet = FillDataSet(DataSource)
                Case 1
                    'Filling Stored Procedure Parameters 
                    dsWorkingDataSet = FillSPandExecuate(DataSource, DtCriteria)
            End Select
            uwgGrid.DataSource = dsWorkingDataSet
            uwgGrid.DataBind()
            Return dsWorkingDataSet
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function FillDataSet(ByVal strSqlQuery As String) As Data.DataSet
        Dim clsdata As New ClsDataAcessLayer(Page)
        Dim dsTempDsCarryingFields As New Data.DataSet
        dsTempDsCarryingFields = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsdata.ConnectionString, Data.CommandType.Text, strSqlQuery)
        If dsTempDsCarryingFields.Tables(0).Rows.Count > 0 Then
            Return dsTempDsCarryingFields
        Else
            Return Nothing
        End If
    End Function

    Private Function AddNewColumnToGrid(ByRef uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByRef dTable As Data.DataTable, ByVal strFormula As String, ByVal IsEditMode As Boolean, Optional ByVal strColumnCaption As String = "") As Boolean
        Try
            ClsReportsColumns = New ClsRpw_ReportColumnsProperties(Me.Page)
            Dim StrOrginalFormula As String = ClsReportsColumns.PrepareFormulaString(strFormula, New TextBox)
            strFormula = ClsReportsColumns.PrepareFormulaStringDT(strFormula, New TextBox)

            Dim IntCalcNo As Integer
            If Not IsNothing(Session("CalcFieldsNo")) Then
                IntCalcNo = CType(Session("CalcFieldsNo"), Integer) + 1
            Else
                IntCalcNo = 1
            End If

            Dim masterTable As DataTable
            If Not IsNothing(Session("CreatedFields")) Then
                masterTable = CType(Session("CreatedFields"), DataTable)
            Else
                masterTable = New DataTable
                masterTable.Columns.AddRange(New DataColumn() {New DataColumn("Rank", GetType(Integer)), New DataColumn("ColumnName", GetType(String)), New DataColumn("Expression", GetType(String)), New DataColumn("IsCanceled", GetType(Boolean))})
            End If
            Dim dcol As DataColumn = New DataColumn("ColumnName", GetType(String))

            Dim IntColumnIndex As Integer
            If Not IsEditMode Then
                Dim dgColumn As New Infragistics.WebUI.UltraWebGrid.UltraGridColumn
                IntColumnIndex = uwgViewColumns.Columns.Add(dgColumn)
                Dim col As UltraGridColumn = uwgViewColumns.Bands(0).Columns(IntColumnIndex)
                col.BaseColumnName = "CalcField" & IntCalcNo.ToString
                col.Header.Caption = IIf(strColumnCaption = "", "CalcField" & IntCalcNo.ToString, strColumnCaption)
                col.DataType = "Double"
                col.Key = "CalcField" & IntCalcNo.ToString
            End If

            '////////////////
            Dim drow As DataRow = masterTable.NewRow
            drow.Item("Rank") = masterTable.Rows.Count
            drow.Item("ColumnName") = "CalcField" & IntCalcNo.ToString ' strColumnCaption

            'drow.Item("Expression") = strFormula
            drow.Item("Expression") = StrOrginalFormula
            drow.Item("IsCanceled") = False
            masterTable.Rows.Add(drow)
            '////////////

            Dim dcNewColumn As New Data.DataColumn("CalcField" & IntCalcNo.ToString, GetType(Single))
            dcNewColumn.ColumnName = "CalcField" & IntCalcNo.ToString 'strColumnCaption
            dcNewColumn.Expression = strFormula
            dTable.Columns.Add(dcNewColumn)

            uwgGrid.DataSource = dTable
            uwgGrid.DataBind()

            'Add Field To Columns Just not In case of Editting mode 
            If Not IsEditMode Then
                Dim dsColumns As DataSet = CType(Session("Step1"), DataSet)

                Dim drDataRow As DataRow = dsColumns.Tables("ColumnsProperties").NewRow
                drDataRow.Item("ColumnIndex") = IntColumnIndex
                'Header Captions 
                drDataRow.Item("ColumnName") = "CalcField" & IntCalcNo.ToString 'strColumnCaption
                drDataRow.Item("EngHeaderCaption") = IIf(strColumnCaption = "", "CalcField" & IntCalcNo.ToString, strColumnCaption)
                drDataRow.Item("BorderStyle") = 0
                drDataRow.Item("Iscalculated") = True
                drDataRow.Item("ColumnFontSize") = 8

                'drDataRow.Item("Formula") = strFormula
                drDataRow.Item("Formula") = StrOrginalFormula

                dsColumns.Tables(2).Rows.Add(drDataRow)
                Session("Step1") = dsColumns
            End If

            Session("CreatedFields") = masterTable
            Session("CalcFieldsNo") = IntCalcNo
            'Update NumericFields 
            If Not IsNothing(Session("Formula")) Then
                Dim drFormulaRows As DataRow = CType(Session("Formula"), DataTable).NewRow()
                drFormulaRows.Item(0) = "CalcField" & IntCalcNo.ToString
                drFormulaRows.Item(1) = "CalcField" & IntCalcNo.ToString
                CType(Session("Formula"), DataTable).Rows.Add(drFormulaRows)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function CreateCalculatedFields(ByVal dtTable As Data.DataTable, ByRef dtMainTableToBeBinded As Data.DataTable) As Boolean
        For Each drDataRow As Data.DataRow In dtTable.Rows
            If drDataRow.Item("Iscalculated") = True Then
                AddNewColumnToGrid(uwgViewColumns, dtMainTableToBeBinded, drDataRow.Item("Formula"), True, drDataRow.Item("EngHeaderCaption"))
            End If
        Next

    End Function

    Private Function FillSPandExecuate(ByVal strProcName As String, ByVal dtCriteriaTable As Data.DataTable) As DataSet
        'Remove this 
        Dim clsmReports As New ClsRpw_ReportColumnsProperties(Page)
        Dim mDataHandeler As New Venus.Shared.DataHandler

        Dim IntParmetersNumbers As Integer = dtCriteriaTable.Rows.Count
        If IntParmetersNumbers > 0 Then
            Dim Intcounter As Integer = 0
            Dim parmArray As SqlClient.SqlParameter
            'For Each drParameter As Data.DataRow In dtCriteriaTable.Rows
            '    parmArray(Intcounter) = New SqlClient.SqlParameter
            '    parmArray(Intcounter).Value = GetParameterValue(mDataHandeler, IIf(drParameter.Item("DefaultValue") Is DBNull.Value, "", drParameter.Item("DefaultValue")), drParameter.Item("DataType"))
            '    Intcounter += 1
            'Next

            Dim dsProcDataset As New Data.DataSet
            Dim dr As SqlClient.SqlDataReader
            Dim cn As New SqlClient.SqlConnection(clsmReports.ConnectionString)
            Dim cm As New SqlClient.SqlCommand(strProcName, cn)
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If
            cm.Parameters.Clear()
            For Each drParameter As Data.DataRow In dtCriteriaTable.Rows
                parmArray = New SqlClient.SqlParameter("@" & Convert.ToString((drParameter.Item("FieldName"))), GetParameterValue(mDataHandeler, IIf(drParameter.Item("DefaultValue") Is DBNull.Value, "", drParameter.Item("DefaultValue")), drParameter.Item("DataType")))
                cm.Parameters.Add(parmArray)
            Next
            cm.CommandType = CommandType.StoredProcedure
            cm.CommandTimeout = 600
            dr = cm.ExecuteReader(CommandBehavior.CloseConnection)
            Dim dt As New DataTable
            dt.Load(dr)
            Dim cnt As Int32 = dt.Rows.Count
            dsProcDataset.Tables.Add(dt)
            If Not IsNothing(dsProcDataset) Then Return dsProcDataset
        End If

    End Function

    Private Function GetParameterValue(ByRef mDataHandeler As Venus.Shared.DataHandler, ByVal strParameterValue As String, ByVal strDataType As String)
        Select Case strDataType
            Case "String", "VarChar" ' VarChar 
                Return IIf(mDataHandeler.DataValue_In(strParameterValue, SqlDbType.VarChar) Is DBNull.Value, "", strParameterValue)
            Case "int", "Int32", "Integer", "Int16", "Decimal", "Double", "Single", "Numeric" ' Numeric Values (TinyInt-Int-SmallInt-Real-Money)
                Return IIf(mDataHandeler.DataValue_In(strParameterValue, SqlDbType.Real) Is DBNull.Value, DBNull.Value, strParameterValue)
            Case "Gdate", "Adate", "Date", "DateTime" ' DateTime  
                If strParameterValue = "" Then
                    Return DBNull.Value
                Else
                    Return Convert.ToDateTime(strParameterValue)
                End If
            Case "Boolean", "Bit" 'Bit -Boolean Values 
                Return IIf(mDataHandeler.DataValue_In(Convert.ToBoolean(CInt(strParameterValue)), SqlDbType.Bit) Is DBNull.Value, False, Convert.ToBoolean(CInt(strParameterValue)))
        End Select
    End Function
    Private Function SeparateArrays(ByVal Arr() As Object, ByRef ArrayParameters() As Object, ByRef ArrayValues() As Object) As Boolean
        Dim IntCounter As Integer = 0
        Dim IntEqualindex As Integer
        Try
            For IntCounter = 0 To Arr.GetUpperBound(0)
                IntEqualindex = Arr(IntCounter).IndexOf("=")
                ArrayParameters(IntCounter) = Arr(IntCounter).Substring(3, IntEqualindex - 3)
                ArrayValues(IntCounter) = Arr(IntCounter).Substring(IntEqualindex + 1)
            Next
        Catch ex As Exception

        End Try
    End Function
    Public Function CheckStoredParameters(ByVal ArrParameters() As String, ByVal TargetStored As String, ByRef ArrNames() As Object, ByRef ArrValue() As Object) As Boolean
        Dim dsObjectDescription As New Data.DataSet
        Dim Intcounter As Integer
        Dim ClsReports As New ClsRpw_ReportsProperties(Page)

        dsObjectDescription = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsReports.ConnectionString, "sys_GetObjectDescription", TargetStored)
        ReDim ArrValue(dsObjectDescription.Tables(0).Rows.Count - 1)
        ReDim ArrNames(dsObjectDescription.Tables(0).Rows.Count - 1)

        For Intcounter = 0 To dsObjectDescription.Tables(0).Rows.Count - 1
            With dsObjectDescription.Tables(0).Rows(Intcounter)
                If Not CheckIfExist(ArrParameters, .Item("ObjectField"), Intcounter, ArrValue) Then
                    ArrValue(Intcounter) = CheckaboutNull(.Item("FieldType"))
                End If
                ArrNames(Intcounter) = .Item("ObjectField")
            End With
        Next
        CheckCurrentValueNull(ArrValue, dsObjectDescription)
    End Function
    Private Function CheckIfExist(ByVal Arr_of_Values() As String, ByVal item As String, ByVal index As Integer, ByRef Value() As Object) As Boolean
        Dim counter As Integer
        Dim pos As Integer
        Dim Name As String
        Dim itemValue As String
        Dim found As Boolean = False

        For counter = 0 To Arr_of_Values.GetUpperBound(0)

            pos = Arr_of_Values(counter).IndexOf("=")
            Name = Arr_of_Values(counter).Substring(0, pos)
            itemValue = Arr_of_Values(counter).Substring(pos + 1)


            If Name.ToUpper = item.ToUpper Or Name.Substring(3).ToUpper = item.ToUpper Then
                Value(index) = itemValue
                found = True
                Exit For
            End If

        Next

        If found Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function CheckCurrentValueNull(ByRef Arr_of_Values() As Object, ByVal DS As Data.DataSet) As Boolean
        Dim counter As Integer
        For counter = 0 To DS.Tables(0).Rows.Count - 1
            With DS.Tables(0).Rows(counter)
                Select Case .Item("FieldType").Toupper
                    Case "DATETIME"
                        If CStr(Arr_of_Values(counter)) = "  /  /    " Then
                            Arr_of_Values(counter) = Nothing
                        End If
                    Case "SMALLDATETIME"
                        If CStr(Arr_of_Values(counter)) = "  /  /    " Then
                            Arr_of_Values(counter) = Nothing
                        End If
                    Case "VARCHAR"
                        If CStr(Arr_of_Values(counter)) = String.Empty Then
                            Arr_of_Values(counter) = String.Empty
                        End If
                    Case "SMALLINT"
                        If CStr(Arr_of_Values(counter)) = String.Empty Then
                            Arr_of_Values(counter) = 0
                        End If
                    Case "INT"
                        If CStr(Arr_of_Values(counter)) = String.Empty Then
                            Arr_of_Values(counter) = 0
                        End If
                    Case "NCHAR"
                        If CStr(Arr_of_Values(counter)) = String.Empty Then
                            Arr_of_Values(counter) = String.Empty
                        End If
                    Case "NUMERIC"
                        If CStr(Arr_of_Values(counter)) = String.Empty Then
                            Arr_of_Values(counter) = 0
                        End If
                    Case "REAL"
                        If CStr(Arr_of_Values(counter)) = String.Empty Then
                            Arr_of_Values(counter) = 0.0
                        End If
                    Case "NVARCHAR"
                        If CStr(Arr_of_Values(counter)) = String.Empty Then
                            Arr_of_Values(counter) = String.Empty
                        End If
                End Select
            End With
        Next
    End Function

    Private Function CheckaboutNull(ByVal fieldType As String) As Object
        Select Case fieldType
            Case "varchar"
                Return CStr("")
            Case "smallint"
                Return 0
            Case "int"
                Return 0
            Case "datetime"
                Return Nothing
            Case "smalldatetime"
                Return Nothing
            Case "nchar"
                Return ""
            Case "numeric"
                Return 0
            Case "real"
                Return 0.0
            Case "nvarchar"
                Return ""
            Case Else
                Return ""
        End Select

    End Function

    Public Function Exec_stord(ByVal Target_stord As String, ByRef ResultDS As Data.DataSet, ByVal Value() As Object) As Boolean
        Dim ClsReports As New ClsRpw_ReportsProperties(Page)
        Try
            ResultDS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsReports.ConnectionString, Target_stord, Value)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub TraverseAllRowsHelper(ByVal rows As RowsCollection, ByRef blFinished As Boolean)
        If blFinished Then Exit Sub
        Dim blEnd As Boolean = False
        Dim strTemp As String = String.Empty
        Dim row As UltraGridRow = Nothing
        For Each row In rows
            If TypeOf row Is GroupByRow And Not blFinished Then
                row.Expand(True)
            Else
                blEnd = True
                blFinished = True
            End If
        Next

    End Sub

    Private Function SetColumnsStylesData(ByVal uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid)
        Dim ClsReportsMain As New ClsRpw_ReportsMain(Me.Page)
        Dim StrStyleCode As String = String.Empty
        Dim StrColId As String = String.Empty
        Dim IntColumnIndex As Integer = 0

        For Each uwgGridColumn As Infragistics.WebUI.UltraWebGrid.UltraGridColumn In uwgGrid.Columns

            If Not uwgGridColumn.Hidden Then
                'Cell Style
                StrColId = uwgGridColumn.BaseColumnName
                StrStyleCode &= ";" & StrColId & "_CCBC=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BorderColor.Name
                StrStyleCode &= ";" & StrColId & "_CCBCT=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.ColorTop.Name
                StrStyleCode &= ";" & StrColId & "_CCBCB=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.ColorBottom.Name
                StrStyleCode &= ";" & StrColId & "_CCBCL=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.ColorLeft.Name
                StrStyleCode &= ";" & StrColId & "_CCBCR=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.ColorRight.Name


                StrStyleCode &= ";" & StrColId & "_CCBS=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).CellStyle.BorderStyle).ToString
                StrStyleCode &= ";" & StrColId & "_CCBST=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleTop).ToString
                StrStyleCode &= ";" & StrColId & "_CCBSB=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleBottom).ToString
                StrStyleCode &= ";" & StrColId & "_CCBSL=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleLeft).ToString
                StrStyleCode &= ";" & StrColId & "_CCBSR=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleRight).ToString

                StrStyleCode &= ";" & StrColId & "_CCBW=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BorderWidth.Value
                StrStyleCode &= ";" & StrColId & "_CCBWT=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.WidthTop.Value
                StrStyleCode &= ";" & StrColId & "_CCBWB=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.WidthBottom.Value
                StrStyleCode &= ";" & StrColId & "_CCBWR=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.WidthRight.Value
                StrStyleCode &= ";" & StrColId & "_CCBWL=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.WidthLeft.Value

                StrStyleCode &= ";" & StrColId & "_CCFC=" & uwgGrid.Columns(IntColumnIndex).CellStyle.ForeColor.Name
                StrStyleCode &= ";" & StrColId & "_CCBKC=" & uwgGrid.Columns(IntColumnIndex).CellStyle.BackColor.Name
                StrStyleCode &= ";" & StrColId & "_CCFN=" & uwgGrid.Columns(IntColumnIndex).CellStyle.Font.Name
                StrStyleCode &= ";" & StrColId & "_CCFS=" & uwgGrid.Columns(IntColumnIndex).CellStyle.Font.Size.ToString

                StrStyleCode &= ";" & StrColId & "_CCFII=" & uwgGrid.Columns(IntColumnIndex).CellStyle.Font.Italic
                StrStyleCode &= ";" & StrColId & "_CCFIU=" & uwgGrid.Columns(IntColumnIndex).CellStyle.Font.Underline
                StrStyleCode &= ";" & StrColId & "_CCFIB=" & uwgGrid.Columns(IntColumnIndex).CellStyle.Font.Underline


                StrStyleCode &= ";" & StrColId & "_CCHA=" & uwgGrid.Columns(IntColumnIndex).CellStyle.HorizontalAlign.ToString
                StrStyleCode &= ";" & StrColId & "_CCVA=" & uwgGrid.Columns(IntColumnIndex).CellStyle.VerticalAlign.ToString

                StrStyleCode &= ";" & StrColId & "_CCP=" & uwgGrid.Columns(IntColumnIndex).CellStyle.Padding.Top.Value
                StrStyleCode &= ";" & StrColId & "_CCM=" & uwgGrid.Columns(IntColumnIndex).CellStyle.Margin.Top.Value

                StrStyleCode &= ";" & StrColId & "_CCW=" & uwgGrid.Columns(IntColumnIndex).CellStyle.Width.Value
                StrStyleCode &= ";" & StrColId & "_CCH=" & uwgGrid.Columns(IntColumnIndex).CellStyle.Height.Value

                'Header Style
                StrStyleCode &= ";" & StrColId & "_CHBC=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderColor.Name
                StrStyleCode &= ";" & StrColId & "_CHBCT=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.ColorTop.Name
                StrStyleCode &= ";" & StrColId & "_CHBCB=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.ColorBottom.Name
                StrStyleCode &= ";" & StrColId & "_CHBCL=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.ColorLeft.Name
                StrStyleCode &= ";" & StrColId & "_CHBCR=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.ColorRight.Name


                StrStyleCode &= ";" & StrColId & "_CHBS=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderStyle).ToString
                StrStyleCode &= ";" & StrColId & "_CHBST=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleTop).ToString
                StrStyleCode &= ";" & StrColId & "_CHBSB=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleBottom).ToString
                StrStyleCode &= ";" & StrColId & "_CHBSL=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleLeft).ToString
                StrStyleCode &= ";" & StrColId & "_CHBSR=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleRight).ToString

                StrStyleCode &= ";" & StrColId & "_CHBW=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderWidth.Value
                StrStyleCode &= ";" & StrColId & "_CHBWT=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.WidthTop.Value
                StrStyleCode &= ";" & StrColId & "_CHBWB=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.WidthBottom.Value
                StrStyleCode &= ";" & StrColId & "_CHBWR=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.WidthRight.Value
                StrStyleCode &= ";" & StrColId & "_CHBWL=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.WidthLeft.Value

                StrStyleCode &= ";" & StrColId & "_CHFC=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.ForeColor.Name
                StrStyleCode &= ";" & StrColId & "_CHBKC=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BackColor.Name
                StrStyleCode &= ";" & StrColId & "_CHFN=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Name
                StrStyleCode &= ";" & StrColId & "_CHFS=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Size.ToString

                StrStyleCode &= ";" & StrColId & "_CHFII=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Italic
                StrStyleCode &= ";" & StrColId & "_CHFIU=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Underline
                StrStyleCode &= ";" & StrColId & "_CHFIB=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Bold

                StrStyleCode &= ";" & StrColId & "_CHHA=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.HorizontalAlign.ToString
                StrStyleCode &= ";" & StrColId & "_CHVA=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.VerticalAlign.ToString

                StrStyleCode &= ";" & StrColId & "_CHP=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Padding.Top.Value
                StrStyleCode &= ";" & StrColId & "_CHM=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Margin.Top.Value

                StrStyleCode &= ";" & StrColId & "_CHW=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Width.Value
                StrStyleCode &= ";" & StrColId & "_CHH=" & uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Height.Value

                'Footer  Style
                StrStyleCode &= ";" & StrColId & "_CFBC=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderColor.Name
                StrStyleCode &= ";" & StrColId & "_CFBCT=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.ColorTop.Name
                StrStyleCode &= ";" & StrColId & "_CFBCB=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.ColorBottom.Name
                StrStyleCode &= ";" & StrColId & "_CFBCL=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.ColorLeft.Name
                StrStyleCode &= ";" & StrColId & "_CFBCR=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.ColorRight.Name


                StrStyleCode &= ";" & StrColId & "_CFBS=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderStyle).ToString
                StrStyleCode &= ";" & StrColId & "_CFBST=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleTop).ToString
                StrStyleCode &= ";" & StrColId & "_CFBSB=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleBottom).ToString
                StrStyleCode &= ";" & StrColId & "_CFBSL=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleLeft).ToString
                StrStyleCode &= ";" & StrColId & "_CFBSR=" & ClsReportsMain.GetBorderStyle(uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleRight).ToString

                StrStyleCode &= ";" & StrColId & "_CFBW=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderWidth.Value
                StrStyleCode &= ";" & StrColId & "_CFBWT=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.WidthTop.Value
                StrStyleCode &= ";" & StrColId & "_CFBWB=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.WidthBottom.Value
                StrStyleCode &= ";" & StrColId & "_CFBWR=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.WidthRight.Value
                StrStyleCode &= ";" & StrColId & "_CFBWL=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.WidthLeft.Value

                StrStyleCode &= ";" & StrColId & "_CFFC=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.ForeColor.Name
                StrStyleCode &= ";" & StrColId & "_CFBKC=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BackColor.Name
                StrStyleCode &= ";" & StrColId & "_CFFN=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Name
                StrStyleCode &= ";" & StrColId & "_CFFS=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Size.ToString

                StrStyleCode &= ";" & StrColId & "_CFFII=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Italic
                StrStyleCode &= ";" & StrColId & "_CFFIU=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Underline
                StrStyleCode &= ";" & StrColId & "_CFFIB=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Bold

                StrStyleCode &= ";" & StrColId & "_CFHA=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.HorizontalAlign.ToString
                StrStyleCode &= ";" & StrColId & "_CFVA=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.VerticalAlign.ToString

                StrStyleCode &= ";" & StrColId & "_CFP=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Padding.Top.Value
                StrStyleCode &= ";" & StrColId & "_CFM=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Margin.Top.Value

                StrStyleCode &= ";" & StrColId & "_CFW=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Width.Value
                StrStyleCode &= ";" & StrColId & "_CFH=" & uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Height.Value

                StrStyleCode &= ";" & StrColId & "_CSORT=" & uwgGrid.Columns(IntColumnIndex).SortIndicator
                StrStyleCode &= ";" & StrColId & "_CHTXT=" & uwgGrid.Columns(IntColumnIndex).Header.Caption.ToString
                StrStyleCode &= ";" & StrColId & "_CFORMAT=" & uwgGrid.Columns(IntColumnIndex).Format
                StrStyleCode &= ";" & StrColId & "_CFTOTAL=" & uwgGrid.Columns(IntColumnIndex).Footer.Total
                StrStyleCode &= ";" & StrColId & "_CFTXT=" & uwgGrid.Columns(IntColumnIndex).Footer.Caption
                StrStyleCode &= ";" & StrColId & "_CCCURR="







            End If
            IntColumnIndex += 1
        Next
        Return StrStyleCode.Substring(1)
    End Function

    Private Function clearSession() As Boolean
        Session("SessionReportProperties") = Nothing
        Session("SessionView") = Nothing
        Session("SessionCriteria") = Nothing
        Session("CreatedFields") = Nothing
        Session("CalcFieldsNo") = Nothing
    End Function

    Private Sub SetGridRanktxt(ByRef ObjDtReportViewColumn)
        ClsReportsMain = New ClsRpw_ReportsMain(Me.Page)
        Try
            For Each objRow As Data.DataRow In ObjDtReportViewColumn.Rows
                hfRank.Value &= objRow.Item("FieldName") & "_Rank=" & objRow.Item("Rank") & ";"
            Next

        Catch ex As Exception
            'mErrorHandler = New Venus.Shared.ErrorsHandler
            'Me.Page.Session.Add("ErrorValue", ex)
            'mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, .RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Me.Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

    Private Sub applyGridRank(ByRef ObjDtReportViewColumn As Data.DataTable)
        ClsReportsMain = New ClsRpw_ReportsMain(Me.Page)
        Dim arrListData As New ArrayList
        Dim StrColumnKeys As String
        Try
            StrColumnKeys = hfRank.Value
            ClsReportsMain.PrepareColumnsData(StrColumnKeys, arrListData)
            For Each strColKey As String In arrListData
                For Each objRow As Data.DataRow In ObjDtReportViewColumn.Rows
                    objRow.Item("Rank") = ClsReportsMain.GetKeysValues(arrListData, objRow.Item("FieldName") & "_Rank")
                    Exit For
                Next
            Next
        Catch ex As Exception
            Me.Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub

#End Region

End Class
