'ToDo 1-Change the way of drawing line around each cell sholud be replaced by virtical and herzontal line 
'ToDo 2-Test Sent parameters 
'ToDo 3-Change Design accourding to new design 

Imports System.Xml
Imports System.Text
Public Class ReportViewer

    Const CFirst = 0
    Public Enum eDataSource
        Stored
        TSQL
    End Enum
    Public Enum Lang
        Arabic = 1
        English = 0
    End Enum
    Public Enum eAlign
        Left
        Middle
        Right
    End Enum

    Dim DTSectionInformation As New Data.DataTable
    Dim LineWidth As Double
    Dim mLangauge As Lang
    Dim Header1 As String
    Dim Header2 As String
    Dim Header3 As String
    Dim ReportWidthLandScape As Double
    Dim ReportWidthPortrate As Double
    Dim XMLName As String

    Private mStrReportname As String
    Private mStrUserCode As String
    Private mStrUserName As String
    Private mStrCompanyName As String
    Private mStrCompanyHeader_1 As String
    Private mStrCompanyHeader_2 As String
    Private mStrCompanyHeader_3 As String
    Private mCompanyID As Object

    Public Property ReportName() As String
        Get
            Return mStrReportname
        End Get
        Set(ByVal value As String)
            mStrReportname = value
        End Set
    End Property

    Public Property UserCode() As String
        Get
            Return mStrUserCode
        End Get
        Set(ByVal value As String)
            mStrUserCode = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return mStrUserName
        End Get
        Set(ByVal value As String)
            mStrUserName = value
        End Set
    End Property

    Public Property CompanyName() As String
        Get
            Return mStrCompanyName
        End Get
        Set(ByVal value As String)
            mStrCompanyName = value
        End Set
    End Property

    Public Property CompanyHeader_1() As String
        Get
            Return mStrCompanyHeader_1
        End Get
        Set(ByVal value As String)
            mStrCompanyHeader_1 = value
        End Set
    End Property

    Public Property CompanyHeader_2() As String
        Get
            Return mStrCompanyHeader_2
        End Get
        Set(ByVal value As String)
            mStrCompanyHeader_2 = value
        End Set
    End Property

    Public Property CompanyHeader_3() As String
        Get
            Return mStrCompanyHeader_3
        End Get
        Set(ByVal value As String)
            mStrCompanyHeader_3 = value
        End Set
    End Property

    Public Property CompanyID As Object
        Get
            Return mCompanyID
        End Get
        Set(value As Object)
            mCompanyID = value
        End Set
    End Property

    Public Sub New(ByVal language As Lang)
        mLangauge = language
    End Sub

    '==============(Reports Main Functions)================
    Public Function ViewReportDetails(ByVal StrFolderMapPath As String, ByVal ClsReportProperties As ClsRpw_ReportsProperties, _
    ByVal DTCriteria As Data.DataTable) As Xml.XmlDocument
        Dim oXS As Global.System.Xml.Serialization.XmlSerializer = New Global.System.Xml.Serialization.XmlSerializer(GetType(Reports))
        Dim Reports As New Reports
        Dim oStmW As IO.StreamWriter
        Dim oStmR As IO.StreamReader
        Dim xDocument As New Xml.XmlDocument
        Dim xElements As Xml.XmlNodeList
        Dim xElement As Xml.XmlNode
        Dim xAttribute As Xml.XmlAttribute
        Dim ObjMemoryStream As New IO.MemoryStream
        Dim strString As String
        Dim Objsb As New Text.StringBuilder(strString)
        Dim oStrW As New IO.StringWriter(Objsb)
        Try
            oStmR = New IO.StreamReader(StrFolderMapPath, Encoding.UTF8)
            Reports = oXS.Deserialize(oStmR)
            oStmR.Dispose()

            SetVisibilityAccordingToDesign(Reports.Report(0))

            With Reports.Report(0)
                .DataSource.ConnectionString = "Provider=SQLOLEDB;Persist Security Info=False;" & ClsReportProperties.ConnectionString
                If ClsReportProperties.ReportSource = 1 Then
                    .DataSource.RecordSource = ClsReportProperties.DataSource & CreateParameters(DTCriteria)
                Else
                    .DataSource.RecordSource = ClsReportProperties.DataSource
                End If
                CreateSentParameters(DTCriteria, Reports.Report(0))
            End With

            oXS = New Global.System.Xml.Serialization.XmlSerializer(GetType(Reports))
            oXS.Serialize(oStrW, Reports)
            Dim oStrR As New IO.StringReader(Objsb.ToString)

            xDocument.Load(oStrR)

            xElements = xDocument.GetElementsByTagName("Picture")
            xAttribute = xDocument.CreateAttribute("encoding")
            xAttribute.Value = "base64"

            For Each xElement In xElements
                xAttribute = xDocument.CreateAttribute("encoding")
                xAttribute.Value = "base64"
                xElement.Attributes.Append(xAttribute)
            Next
            Return xDocument
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ViewReportDetailsxml(ByVal XMLReports As String, ByVal ClsReportProperties As ClsRpw_ReportsProperties, _
ByVal DTCriteria As Data.DataTable, ByVal language As Lang) As Xml.XmlDocument
        Dim oXS As Global.System.Xml.Serialization.XmlSerializer = New Global.System.Xml.Serialization.XmlSerializer(GetType(Reports))
        Dim Reports As New Reports
        Dim oStmW As IO.StreamWriter
        Dim oStmR As IO.StringReader
        Dim xDocument As New Xml.XmlDocument
        Dim xElements As Xml.XmlNodeList
        Dim xElement As Xml.XmlNode
        Dim xAttribute As Xml.XmlAttribute
        Dim ObjMemoryStream As New IO.MemoryStream
        Dim strString As String
        Dim Objsb As New Text.StringBuilder(strString)
        Dim oStrW As New IO.StringWriter(Objsb)
        Try
            oStmR = New IO.StringReader(XMLReports)
            Reports = oXS.Deserialize(oStmR)
            oStmR.Dispose()

            With Reports.Report(0)
                .DataSource.ConnectionString = "Provider=SQLOLEDB;Persist Security Info=False;" & ClsReportProperties.ConnectionString
                If ClsReportProperties.ReportSource = 1 Then
                    .DataSource.RecordSource = ClsReportProperties.DataSource & CreateParameters(DTCriteria)
                Else
                    .DataSource.RecordSource = ClsReportProperties.DataSource
                End If
                CreateSentParameters(DTCriteria, Reports.Report(0))
            End With

            Select Case language
                Case Lang.Arabic
                    SetLanguagePosition(Reports.Report(0), Lang.Arabic)
            End Select

            oXS = New Global.System.Xml.Serialization.XmlSerializer(GetType(Reports))
            oXS.Serialize(oStrW, Reports)
            Dim oStrR As New IO.StringReader(Objsb.ToString)

            xDocument.Load(oStrR)

            xElements = xDocument.GetElementsByTagName("Picture")
            xAttribute = xDocument.CreateAttribute("encoding")
            xAttribute.Value = "base64"

            For Each xElement In xElements
                xAttribute = xDocument.CreateAttribute("encoding")
                xAttribute.Value = "base64"
                xElement.Attributes.Append(xAttribute)
            Next
            Return xDocument
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CreateReportDetails(ByVal ClsReportProperties As ClsRpw_ReportsProperties, _
    ByVal DTColumnProperties As Data.DataTable, ByVal DTRowProperties As Data.DataTable, ByVal DTCriteria As Data.DataTable, _
    ByVal StrFolderMapPath As String) As Xml.XmlDocument
        Dim oXS As Global.System.Xml.Serialization.XmlSerializer = New Global.System.Xml.Serialization.XmlSerializer(GetType(Reports))
        Dim Reports As New Reports
        Dim oStmW As IO.StreamWriter
        Dim oStmR As IO.StreamReader
        Dim xDocument As New Xml.XmlDocument
        Dim xElements As Xml.XmlNodeList
        Dim xElement As Xml.XmlNode
        Dim xAttribute As Xml.XmlAttribute
        Try
            CreateReport(Reports, ClsReportProperties, DTCriteria)
            CreateMainSections(Reports.Report(CFirst), ClsReportProperties)

            CreateFieldsHead(Reports.Report(CFirst), ClsReportProperties, DTColumnProperties)
            CreateFieldsDetails(Reports.Report(CFirst), ClsReportProperties, DTColumnProperties)
            CreateFieldsFooter(Reports.Report(CFirst), ClsReportProperties, DTColumnProperties)

            'CreateRowProperities(Reports.Report(CFirst), ClsReportProperties, DTRowProperties)
            CreateCriteria(Reports.Report(CFirst), ClsReportProperties, DTCriteria)
            CreateBoxHead(Reports.Report(CFirst))
            CreateBoxTitle(Reports.Report(CFirst))
            CreateBoxObject(Reports.Report(CFirst))
            CreateDateTimeFields(Reports.Report(CFirst), ClsReportProperties)

            oXS = New Global.System.Xml.Serialization.XmlSerializer(GetType(Reports))
            oStmW = New IO.StreamWriter(StrFolderMapPath)
            oXS.Serialize(oStmW, Reports)
            oStmW.Close()

            xDocument.Load(StrFolderMapPath)
            xElements = xDocument.GetElementsByTagName("Picture")
            xAttribute = xDocument.CreateAttribute("encoding")
            xAttribute.Value = "base64"
            For Each xElement In xElements
                xAttribute = xDocument.CreateAttribute("encoding")
                xAttribute.Value = "base64"
                xElement.Attributes.Append(xAttribute)
            Next
            xDocument.Save(StrFolderMapPath)
            Return xDocument
        Catch ex As Exception
            Return Nothing
        End Try
    End Function     'Waiting for QC.

    Public Function UpdateReportDetails(ByVal ClsReportProperties As ClsRpw_ReportsProperties, _
    ByVal DTColumnProperties As Data.DataTable, ByVal DTRowProperties As Data.DataTable, ByVal DTCriteria As Data.DataTable, _
    ByVal StrFolderMapPath As String, ByVal Overwrite As Boolean) As Xml.XmlDocument
        Dim oXS As Global.System.Xml.Serialization.XmlSerializer = New Global.System.Xml.Serialization.XmlSerializer(GetType(Reports))
        Dim Reports As New Reports
        Dim oStmW As IO.StreamWriter
        Dim oStmR As IO.StreamReader
        Dim xDocument As New Xml.XmlDocument
        Dim xElements As Xml.XmlNodeList
        Dim xElement As Xml.XmlNode
        Dim xAttribute As Xml.XmlAttribute
        Try
            oStmR = New IO.StreamReader(StrFolderMapPath)
            Reports = oXS.Deserialize(oStmR)
            oStmR.Dispose()
            oXS = Nothing
            UpdateReport(Reports, ClsReportProperties, DTCriteria, Overwrite)
            'UpdateMainSections(Reports.Report(CFirst), ClsReportProperties)
            ClearAllBorders(Reports.Report(CFirst))
            UpdateFieldsHead(Reports.Report(CFirst), ClsReportProperties, DTColumnProperties)
            UpdateFieldsDetails(Reports.Report(CFirst), ClsReportProperties, DTColumnProperties)
            UpdateFieldsFooter(Reports.Report(CFirst), ClsReportProperties, DTColumnProperties)
            'CreateRowProperities(Reports.Report(CFirst), ClsReportProperties, DTRowProperties)
            UpdateCriteriaCaption(Reports.Report(CFirst), ClsReportProperties, DTCriteria)
            UpdateCriteriaValue(Reports.Report(CFirst), ClsReportProperties, DTCriteria)
            UpdateBoxObject(Reports.Report(CFirst))

            oXS = New Global.System.Xml.Serialization.XmlSerializer(GetType(Reports))
            oStmW = New IO.StreamWriter(StrFolderMapPath)
            oXS.Serialize(oStmW, Reports)
            oStmW.Close()

            xDocument.Load(StrFolderMapPath)
            xElements = xDocument.GetElementsByTagName("Picture")
            xAttribute = xDocument.CreateAttribute("encoding")
            xAttribute.Value = "base64"

            For Each xElement In xElements
                xAttribute = xDocument.CreateAttribute("encoding")
                xAttribute.Value = "base64"
                xElement.Attributes.Append(xAttribute)
            Next
            xDocument.Save(StrFolderMapPath)
            Return xDocument
        Catch ex As Exception
            Return Nothing
        End Try
    End Function     'Waiting for QC.

    Public Function AttachReportDetails()

    End Function     'Not yet started

    Public Function VolatileReports(ByVal ClsReportProperties As ClsRpw_ReportsProperties, _
    ByVal DTColumnProperties As Data.DataTable, ByVal DTRowProperties As Data.DataTable, ByVal DTCriteria As Data.DataTable) As Xml.XmlDocument
        Dim oXS As Global.System.Xml.Serialization.XmlSerializer = New Global.System.Xml.Serialization.XmlSerializer(GetType(Reports))
        Dim Reports As New Reports
        Dim xDocument As New Xml.XmlDocument
        Dim xElements As Xml.XmlNodeList
        Dim xElement As Xml.XmlNode
        Dim xAttribute As Xml.XmlAttribute
        Dim strString As String
        Dim Objsb As New Text.StringBuilder(strString)
        Dim oStrW As New IO.StringWriter(Objsb)
        Try

            CreateReport(Reports, ClsReportProperties, DTCriteria)
            CreateMainSections(Reports.Report(CFirst), ClsReportProperties)

            CreateFieldsHead(Reports.Report(CFirst), ClsReportProperties, DTColumnProperties, True)
            CreateFieldsDetails(Reports.Report(CFirst), ClsReportProperties, DTColumnProperties)
            CreateFieldsFooter(Reports.Report(CFirst), ClsReportProperties, DTColumnProperties)

            'CreateRowProperities(Reports.Report(CFirst), ClsReportProperties, DTRowProperties)
            CreateCriteria(Reports.Report(CFirst), ClsReportProperties, DTCriteria)
            CreateBoxHead(Reports.Report(CFirst))
            CreateBoxTitle(Reports.Report(CFirst))
            CreateBoxObject(Reports.Report(CFirst))
            CreateDateTimeFields(Reports.Report(CFirst), ClsReportProperties)

            With Reports.Report(CFirst)
                .DataSource.ConnectionString = "Provider=SQLNCLI;Persist Security Info=False;" & ClsReportProperties.ConnectionString
                If ClsReportProperties.ReportSource = 1 Then
                    .DataSource.RecordSource = ClsReportProperties.DataSource & CreateParameters(DTCriteria)
                Else
                    .DataSource.RecordSource = ClsReportProperties.DataSource
                End If
            End With

            SetVisibilityAccordingToDesign(Reports.Report(CFirst))

            oXS = New Global.System.Xml.Serialization.XmlSerializer(GetType(Reports))
            oXS.Serialize(oStrW, Reports)
            Dim oStrR As New IO.StringReader(Objsb.ToString)

            xDocument.Load(oStrR)
            xElements = xDocument.GetElementsByTagName("Picture")
            xAttribute = xDocument.CreateAttribute("encoding")
            xAttribute.Value = "base64"

            For Each xElement In xElements
                xAttribute = xDocument.CreateAttribute("encoding")
                xAttribute.Value = "base64"
                xElement.Attributes.Append(xAttribute)
            Next
            Return xDocument
        Catch ex As Exception
            Return Nothing
        End Try
    End Function         'Waiting for QC.

    '===============(Reports Main Sections)================

    Private Function CreateReport(ByRef Reports As Reports, ByVal ClsReportProperties As ClsRpw_ReportsProperties, ByVal DTCriteria As Data.DataTable) As Boolean
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim Report As New Report
        Try
            With Report
                .Name = ClsReportProperties.Code
                .DataSource.ConnectionString = ""
                If ClsReportProperties.ReportSource = 1 Then
                    .DataSource.RecordSource = ClsReportProperties.DataSource & CreateParameters(DTCriteria)
                Else
                    .DataSource.RecordSource = ClsReportProperties.DataSource
                End If
                .Font.Name = ClsReportProperties.ReportFont
                .Font.Size = ClsReportProperties.ReportFontSize
                .Font.Bold = ClsReportProperties.ReportFontIsBold
                .Font.Italic = ClsReportProperties.ReportFontIsItalic
                .Layout.Orientation = IIf(ClsReportProperties.IsLandscape = True, 2, 1)
                .Layout.Width = IIf(ClsReportProperties.IsLandscape = True, 16274, 11384)
                .Layout.MarginLeft = 200 'ClsReportProperties.ReportLeftMargins
                .Layout.MarginRight = 200 'ClsReportProperties.ReportRightMargins
                .Layout.MarginBottom = 200 'ClsReportProperties.ReportBottomMargins
                .Layout.MarginTop = 200 'ClsReportProperties.ReportTopMargins
                .Layout.BackColor = ObjColorConvertor.ConvertFromString(IIf(ClsReportProperties.ReportBackColor = "", "White", ClsReportProperties.ReportBackColor)).ToArgb
                .Layout.PaperSize = 9
            End With
            Reports.AddReport(Report)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function           'Waiting for QC.

    Private Function UpdateReport(ByRef Reports As Reports, _
    ByVal ClsReportProperties As ClsRpw_ReportsProperties, _
    ByVal DTCriteria As Data.DataTable, ByVal Overwrite As Boolean) As Boolean
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim Report As New Report
        Dim StringHandler As New Venus.Shared.StringHandler
        Report = Reports.Report(CFirst)
        Try
            With Report
                If ClsReportProperties.ReportSource = 1 Then
                    .DataSource.RecordSource = ClsReportProperties.DataSource & CreateParameters(DTCriteria)
                Else
                    .DataSource.RecordSource = ClsReportProperties.DataSource
                End If
                If Overwrite Then
                    .Name = ClsReportProperties.Code
                    .DataSource.ConnectionString = ""
                    .Font.Name = ClsReportProperties.ReportFont
                    .Font.Size = ClsReportProperties.ReportFontSize
                    .Font.Bold = ClsReportProperties.ReportFontIsBold
                    .Font.Italic = ClsReportProperties.ReportFontIsItalic
                    .Layout.Orientation = IIf(ClsReportProperties.IsLandscape = True, 2, 1)
                    .Layout.Width = IIf(ClsReportProperties.IsLandscape = True, 16274, 11384)
                    .Layout.MarginLeft = ClsReportProperties.ReportLeftMargins
                    .Layout.MarginRight = ClsReportProperties.ReportRightMargins
                    .Layout.MarginBottom = ClsReportProperties.ReportBottomMargins
                    .Layout.MarginTop = ClsReportProperties.ReportTopMargins
                    .Layout.BackColor = ObjColorConvertor.ConvertFromString(IIf(ClsReportProperties.ReportBackColor = "", "White", ClsReportProperties.ReportBackColor)).ToArgb
                End If
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function           'Waiting for QC.

    Private Function CreateMainSections(ByRef Report As Report, ByVal ClsReportProperties As ClsRpw_ReportsProperties) As Boolean
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim Section As Section
        Try
            With Report.Sections
                Section = New Section
                With Section
                    .Name = "Detail"
                    .Height = 345
                    .Type = 0
                    .BackColor = Report.Layout.BackColor
                End With
                .AddSection(Section)

                Section = New Section
                With Section
                    .Name = "ReportHeader"
                    .Height = 2385 + Report.Layout.MarginTop
                    .Type = 1
                    .BackColor = Report.Layout.BackColor
                End With
                .AddSection(Section)

                Section = New Section
                With Section
                    .Name = "ReportFooter"
                    .Type = 2
                    .BackColor = Report.Layout.BackColor
                End With
                .AddSection(Section)

                Section = New Section
                With Section
                    .Name = "PageHeader"
                    .Height = 375
                    .Type = 3
                    .BackColor = Report.Layout.BackColor
                End With
                .AddSection(Section)

                Section = New Section
                With Section
                    .Name = "PageFooter"
                    .Height = 345
                    .Type = 4
                    .BackColor = Report.Layout.BackColor
                End With
                .AddSection(Section)

            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function     'Waiting for QC.

    Private Function UpdateMainSections(ByRef Report As Report, _
    ByVal ClsReportProperties As ClsRpw_ReportsProperties, ByVal Overwrite As Boolean) As Boolean
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim Section As Section
        Dim StringHandler As New Venus.Shared.StringHandler
        Try
            With Report.Sections
                Section = New Section
                Section = .GetSectionByName("Detail")
                If Not Section Is Nothing Then
                    With Section
                        If Overwrite Then
                            .Height = 345
                            .BackColor = Report.Layout.BackColor
                        End If
                    End With
                End If

                Section = New Section
                Section = .GetSectionByName("ReportHeader")
                If Not Section Is Nothing Then
                    With Section
                        If Overwrite Then
                            .Height = 2385 + Report.Layout.MarginTop
                            .BackColor = Report.Layout.BackColor
                        End If
                    End With
                End If

                Section = New Section
                Section = .GetSectionByName("ReportFooter")
                If Not Section Is Nothing Then
                    With Section
                        If Overwrite Then
                            .BackColor = Report.Layout.BackColor
                        End If
                    End With
                End If

                Section = New Section
                Section = .GetSectionByName("PageHeader")
                If Not Section Is Nothing Then
                    With Section
                        If Overwrite Then
                            .Height = 345
                            .BackColor = Report.Layout.BackColor
                        End If
                    End With
                End If

                Section = New Section
                Section = .GetSectionByName("PageFooter")
                If Not Section Is Nothing Then
                    With Section
                        If Overwrite Then
                            .Height = 345
                            .BackColor = Report.Layout.BackColor
                        End If
                    End With
                End If

            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function     'Waiting for QC.

    '===================(Fields Section)===================

    Private Function CreateFieldsHead(ByRef Report As Report, _
    ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtColumns As Data.DataTable, Optional ByVal Volatile As Boolean = False) As Boolean
        Dim ObjDataRow As Data.DataRow
        Dim IntForeColor As Integer = 0
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim intCurrentWidth As Integer
        Dim ObjField As Field
        Dim bolNotVisable As Boolean
        Dim BolPerVisable As Boolean
        Dim IntColumnIndex As Integer
        Dim IntFieldLanguage As Integer
        Dim IntTotalWidthEnglish As Integer = Report.Layout.MarginLeft
        Dim IntTotalWidthArabic As Integer = Report.Layout.Width - Report.Layout.MarginRight
        Dim ObjLine As New Field
        Try
            With Report.Fields
                For Each ObjDataRow In DtColumns.Select("FieldName <> ''", "Rank")
                    ObjField = New Field
                    bolNotVisable = CType(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnIsHidden"), Data.SqlDbType.Bit), Boolean)
                    intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                    IntColumnIndex = ObjDataHandler.DataValue_Out(ObjDataRow("Rank"), Data.SqlDbType.Int)
                    IntFieldLanguage = ObjDataHandler.DataValue_Out(ObjDataRow("FieldLanguage"), Data.SqlDbType.Int)
                    If IntFieldLanguage = 1 Or IntFieldLanguage = 3 Or IntFieldLanguage = 0 Then
                        With ObjField
                            .Name = "HdrE" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntColumnIndex
                            .Section = 3
                            .Text = ObjDataHandler.DataValue_Out(ObjDataRow("EngDescription"), Data.SqlDbType.VarChar)
                            .Calculated = 0
                            .Top = 0
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Left = IntTotalWidthEnglish
                            .Height = 330
                            .Font.Bold = IIf(ReportsProperties.HeaderFontIsBold = False, -1, 1)
                            .Font.Italic = IIf(ReportsProperties.HeaderFontIsItalic = False, -1, 1)
                            .Font.Name = ObjDataRow("ColHeaderFont")
                            .Font.Size = ObjDataRow("ColHeaderFontSize")
                            .ForeColor = DefaultBackColor(ObjDataRow("ColHeaderForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                            .BorderStyle = ReportsProperties.HeaderBorderStyle
                            .BorderColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                            .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHeaderVAlignment"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHeaderHAlignment"), Data.SqlDbType.VarChar))
                            .Visible = IIf(Not bolNotVisable, 1, 0)
                            .Tag = "Language=E;"
                            IntTotalWidthEnglish += .Width + 10
                        End With
                        .AddField(ObjField)
                    End If
                    ObjField = New Field
                    If IntFieldLanguage = 2 Or IntFieldLanguage = 3 Or IntFieldLanguage = 0 Then
                        With ObjField
                            .Name = "HdrA" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntColumnIndex
                            .Section = 3
                            .Text = ObjDataHandler.DataValue_Out(ObjDataRow("ArbDescription"), Data.SqlDbType.VarChar)
                            .Calculated = 0
                            .Top = 0
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Left = IntTotalWidthArabic - IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Height = 330
                            .Font.Bold = IIf(ReportsProperties.HeaderFontIsBold = False, -1, 1)
                            .Font.Italic = IIf(ReportsProperties.HeaderFontIsItalic = False, -1, 1)
                            .Font.Name = ObjDataRow("ColHeaderFont")
                            .Font.Size = ObjDataRow("ColHeaderFontSize")
                            .ForeColor = DefaultBackColor(ObjDataRow("ColHeaderForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                            .BorderStyle = ReportsProperties.HeaderBorderStyle
                            .BorderColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                            .Align = 8
                            .Visible = IIf(Not bolNotVisable, 1, 0)
                            .Tag = "Language=A;"
                            IntTotalWidthArabic -= .Width - 10
                        End With
                        .AddField(ObjField)
                    End If
                    'ToDo CreateFieldsHeadBorders(Report, ObjField.Name, ObjDataRow)
                Next
            End With
            ObjLine = New Field
            ObjLine.Name = "Line_Box_Top_Details"
            ObjLine.Width = Report.Layout.Width - (Report.Layout.MarginLeft + Report.Layout.MarginLeft)
            ObjLine.Top = 0
            ObjLine.Left = Report.Layout.MarginLeft
            ObjLine.Height = 345
            ObjLine.LineWidth = 0
            ObjLine.BorderColor = DefaultBackColor("", False)
            ObjLine.BorderStyle = 1
            ObjLine.Section = 3
            If Volatile Then
                ObjLine.BackColor = DefaultBackColor("", True)
            Else
                ObjLine.BackColor = 16119285
            End If
            ObjLine.BackStyle = 1
            Report.Fields.AddField(ObjLine)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function       'Waiting For QC.

    Private Function UpdateFieldsHead(ByRef Report As Report, _
    ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtColumns As Data.DataTable) As Boolean
        Dim ObjDataRow As Data.DataRow
        Dim IntForeColor As Integer = 0
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim intCurrentWidth As Integer
        Dim ObjFieldE As Field
        Dim ObjFieldA As Field
        Dim bolNotVisable As Boolean
        Dim BolPerVisable As Boolean
        Dim IntColumnIndex As Integer
        Dim IntFieldLanguage As Integer
        Dim IntTotalLeftEnglish As Integer = Report.Layout.MarginLeft
        Dim IntTotalLeftArabic As Integer = Report.Layout.Width - Report.Layout.MarginRight
        Dim StringHandler As New Venus.Shared.StringHandler
        Try
            With Report.Fields
                For Each ObjDataRow In DtColumns.Select("FieldName <> ''", "Rank")
                    ObjFieldE = New Field
                    IntColumnIndex = ObjDataHandler.DataValue_Out(ObjDataRow("Rank"), Data.SqlDbType.Int)
                    ObjFieldE = .GetFieldByName("HdrE" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntColumnIndex)
                    ObjFieldA = .GetFieldByName("HdrA" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntColumnIndex)
                    IntFieldLanguage = ObjDataHandler.DataValue_Out(ObjDataRow("FieldLanguage"), Data.SqlDbType.Int)

                    If Not ObjFieldE Is Nothing Then
                        If StringHandler.GetValue(ObjFieldE.Tag, "Status").ToUpper <> "FIX" Then
                            bolNotVisable = CType(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnIsHidden"), Data.SqlDbType.Bit), Boolean)
                            intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                            With ObjFieldE
                                .Section = 3
                                .Text = ObjDataHandler.DataValue_Out(ObjDataRow("EngDescription"), Data.SqlDbType.VarChar) & "." 'ArbHeaderCaption
                                .Calculated = -1
                                .Top = 0
                                .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                                .Height = 330
                                .Font.Bold = IIf(ReportsProperties.HeaderFontIsBold = False, -1, 1)
                                .Font.Italic = IIf(ReportsProperties.HeaderFontIsItalic = False, -1, 1)
                                .Font.Name = ReportsProperties.HeaderFont
                                .Font.Size = ReportsProperties.HeaderFontSize
                                .ForeColor = DefaultBackColor(ObjDataRow("ColHeaderForeColor"), False)
                                .BackColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                                .BorderStyle = ReportsProperties.HeaderBorderStyle
                                .BorderColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                                .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHeaderVAlignment"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHeaderHAlignment"), Data.SqlDbType.VarChar))
                                .Visible = IIf(Not bolNotVisable, 1, 0)
                            End With
                        End If
                    Else
                        ObjFieldE = New Field
                        bolNotVisable = CType(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnIsHidden"), Data.SqlDbType.Bit), Boolean)
                        intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                        With ObjFieldE
                            .Name = "HdrE" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntColumnIndex
                            .Section = 3
                            .Text = ObjDataHandler.DataValue_Out(ObjDataRow("EngDescription"), Data.SqlDbType.VarChar) & "."
                            .Calculated = 0
                            .Top = 0
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.5
                            .Left = 0
                            .Height = 330
                            .Font.Bold = IIf(ReportsProperties.HeaderFontIsBold = False, -1, 1)
                            .Font.Italic = IIf(ReportsProperties.HeaderFontIsItalic = False, -1, 1)
                            .Font.Name = ReportsProperties.HeaderFont
                            .Font.Size = ReportsProperties.HeaderFontSize
                            .ForeColor = DefaultBackColor(ObjDataRow("ColHeaderForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                            .BorderStyle = ReportsProperties.HeaderBorderStyle
                            .BorderColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                            .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHeaderVAlignment"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHeaderHAlignment"), Data.SqlDbType.VarChar))
                            .Visible = IIf(Not bolNotVisable, 1, 0)
                        End With
                        If IntFieldLanguage = 1 Or IntFieldLanguage = 3 Then
                            .AddField(ObjFieldE)
                        End If
                    End If
                    If Not ObjFieldA Is Nothing Then
                        If StringHandler.GetValue(ObjFieldA.Tag, "Status").ToUpper <> "FIX" Then
                            bolNotVisable = CType(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnIsHidden"), Data.SqlDbType.Bit), Boolean)
                            intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                            With ObjFieldA
                                .Section = 3
                                .Text = ObjDataHandler.DataValue_Out(ObjDataRow("EngDescription"), Data.SqlDbType.VarChar) & "." 'ArbHeaderCaption
                                .Calculated = -1
                                .Top = 0
                                .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                                .Height = 330
                                .Font.Bold = IIf(ReportsProperties.HeaderFontIsBold = False, -1, 1)
                                .Font.Italic = IIf(ReportsProperties.HeaderFontIsItalic = False, -1, 1)
                                .Font.Name = ReportsProperties.HeaderFont
                                .Font.Size = ReportsProperties.HeaderFontSize
                                .ForeColor = DefaultBackColor(ObjDataRow("ColHeaderForeColor"), False)
                                .BackColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                                .BorderStyle = ReportsProperties.HeaderBorderStyle
                                .BorderColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                                .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHeaderVAlignment"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHeaderHAlignment"), Data.SqlDbType.VarChar))
                                .Visible = IIf(Not bolNotVisable, 1, 0)
                            End With
                        End If
                    Else
                        ObjFieldA = New Field
                        bolNotVisable = CType(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnIsHidden"), Data.SqlDbType.Bit), Boolean)
                        intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                        With ObjFieldA
                            .Name = "HdrA" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntColumnIndex
                            .Section = 3
                            .Text = ObjDataHandler.DataValue_Out(ObjDataRow("EngDescription"), Data.SqlDbType.VarChar) & "."
                            .Calculated = 0
                            .Top = 0
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.5
                            .Left = 0
                            .Height = 330
                            .Font.Bold = IIf(ReportsProperties.HeaderFontIsBold = False, -1, 1)
                            .Font.Italic = IIf(ReportsProperties.HeaderFontIsItalic = False, -1, 1)
                            .Font.Name = ReportsProperties.HeaderFont
                            .Font.Size = ReportsProperties.HeaderFontSize
                            .ForeColor = DefaultBackColor(ObjDataRow("ColHeaderForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                            .BorderStyle = ReportsProperties.HeaderBorderStyle
                            .BorderColor = DefaultBackColor(ObjDataRow("ColHeaderBackColor"), True)
                            .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHeaderVAlignment"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHeaderHAlignment"), Data.SqlDbType.VarChar))
                            .Visible = IIf(Not bolNotVisable, 1, 0)
                        End With
                        If IntFieldLanguage = 2 Or IntFieldLanguage = 3 Then
                            .AddField(ObjFieldA)
                        End If
                    End If
                    'ToDo CreateFieldsHeadBorders(Report, ObjField.Name, ObjDataRow)
                Next
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function       'Waiting For QC.

    Private Function CreateFieldsDetails(ByRef Report As Report, _
    ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtColumns As Data.DataTable) As Boolean
        Dim ObjDataRow As Data.DataRow
        Dim IntForeColor As Integer = 0
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim intCurrentWidth As Integer
        Dim ObjField As Field
        Dim BolNotVisable As Boolean
        Dim BolPerVisable As Boolean
        Dim BolGroupby As Boolean
        Dim IntSectionNo As Integer = 0
        Dim StrFieldName As String = String.Empty
        Dim StrFormula As String = String.Empty
        Dim Sorted As Integer = 0
        Dim IntColumnIndex As Integer
        Dim IntFieldLanguage As Integer
        Dim IntTotalWidthEnglish As Integer = Report.Layout.MarginLeft
        Dim IntTotalWidthArabic As Integer = Report.Layout.Width - Report.Layout.MarginRight
        Try
            With Report.Fields
                For Each ObjDataRow In DtColumns.Select("FieldName <> ''", "Rank")
                    ObjField = New Field
                    BolNotVisable = CType(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnIsHidden"), Data.SqlDbType.Bit), Boolean)
                    intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                    BolGroupby = ObjDataHandler.DataValue_Out(ObjDataRow("IsGroupBy"), Data.SqlDbType.Bit)
                    StrFieldName = ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                    StrFormula = ObjDataHandler.DataValue_Out(ObjDataRow("Formula"), Data.SqlDbType.VarChar)
                    Sorted = ObjDataHandler.DataValue_Out(ObjDataRow("IsSorted"), Data.SqlDbType.Int)
                    IntColumnIndex = ObjDataHandler.DataValue_Out(ObjDataRow("Rank"), Data.SqlDbType.Int)
                    IntFieldLanguage = ObjDataHandler.DataValue_Out(ObjDataRow("FieldLanguage"), Data.SqlDbType.Int)
                    If BolGroupby Then
                        CreateGroup(Report, IIf(StrFormula <> "", StrFormula, StrFieldName), Sorted, 1)
                        IntSectionNo = CreateSection(Report, StrFieldName, 345)
                    ElseIf Sorted > 0 Then
                        CreateGroup(Report, IIf(StrFormula <> "", StrFormula, StrFieldName), Sorted, 1)
                        CreateSection(Report, StrFieldName, 0)
                        IntSectionNo = 0
                    Else
                        IntSectionNo = 0
                    End If
                    If IntFieldLanguage = 1 Or IntFieldLanguage = 3 Or IntFieldLanguage = 0 Then
                        With ObjField
                            .Name = "DtlE" & StrFieldName & IntColumnIndex
                            .Section = IntSectionNo
                            .Text = IIf(StrFormula <> "", StrFormula, StrFieldName)
                            .Format = ObjDataHandler.DataValue_Out(ObjDataRow("Format"), Data.SqlDbType.VarChar)
                            .Calculated = 1
                            .Top = 5
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Left = IntTotalWidthEnglish
                            .Height = 330
                            .Font.Bold = IIf(CType(ObjDataRow("ColumnFontIsBold"), Boolean) = False, -1, 1)
                            .Font.Italic = IIf(CType(ObjDataRow("ColumnFontIsItalic"), Boolean) = False, -1, 1)
                            .Font.Name = ObjDataRow("ColumnFont")
                            .Font.Size = ObjDataRow("ColumnFontSize")
                            .ForeColor = DefaultBackColor(ObjDataRow("ColumnForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                            .BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("BorderStyle"), Data.SqlDbType.Int)
                            .BorderColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                            .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnValignment"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHalignment"), Data.SqlDbType.VarChar))
                            .Visible = IIf(Not BolNotVisable, 1, 0)
                            .Tag = "Language=E;"
                            IntTotalWidthEnglish += .Width + 10
                            .ZOrder = -1
                        End With
                        .AddField(ObjField)
                    End If
                    ObjField = New Field
                    If IntFieldLanguage = 2 Or IntFieldLanguage = 3 Or IntFieldLanguage = 0 Then
                        With ObjField
                            .Name = "DtlA" & StrFieldName & IntColumnIndex
                            .Section = IntSectionNo
                            .Text = IIf(StrFormula <> "", StrFormula, StrFieldName)
                            .Format = ObjDataHandler.DataValue_Out(ObjDataRow("Format"), Data.SqlDbType.VarChar)
                            .Calculated = 1
                            .Top = 5
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Left = IntTotalWidthArabic - IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Height = 330
                            .Font.Bold = IIf(CType(ObjDataRow("ColumnFontIsBold"), Boolean) = False, -1, 1)
                            .Font.Italic = IIf(CType(ObjDataRow("ColumnFontIsItalic"), Boolean) = False, -1, 1)
                            .Font.Name = ObjDataRow("ColumnFont")
                            .Font.Size = ObjDataRow("ColumnFontSize")
                            .ForeColor = DefaultBackColor(ObjDataRow("ColumnForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                            .BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("BorderStyle"), Data.SqlDbType.Int)
                            .BorderColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                            .Align = 8
                            .Visible = IIf(Not BolNotVisable, 1, 0)
                            .Tag = "Language=A;"
                            IntTotalWidthArabic -= .Width - 10
                            .ZOrder = -1
                        End With
                        .AddField(ObjField)
                    End If
                    'ToDo CreateFieldsBorders(Report, ObjField.Name, ObjDataRow)
                Next
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function    'Waiting For QC.

    Private Function UpdateFieldsDetails(ByRef Report As Report, _
ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtColumns As Data.DataTable) As Boolean

        Dim ObjDataRow As Data.DataRow
        Dim IntForeColor As Integer = 0
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim intCurrentWidth As Integer
        Dim ObjFieldE As Field
        Dim ObjFieldA As Field
        Dim BolNotVisable As Boolean
        Dim BolGroupby As Boolean
        Dim IntSectionNo As Integer = 0
        Dim StrFieldName As String = String.Empty
        Dim StrFormula As String = String.Empty
        Dim Sorted As Integer = 0
        Dim IntColumnIndex As Integer
        Dim IntFieldLanguage As Integer
        Dim IntLanguage As Integer = mLangauge
        Dim IntTotalWidth As Integer = IIf(IntLanguage = 0, Report.Layout.MarginLeft, Report.Layout.Width - Report.Layout.MarginRight)
        Dim StringHandler As New Venus.Shared.StringHandler

        Try
            With Report.Fields
                For Each ObjDataRow In DtColumns.Select("FieldName <> ''", "Rank")
                    ObjFieldE = New Field
                    ObjFieldA = New Field
                    BolNotVisable = CType(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnIsHidden"), Data.SqlDbType.Bit), Boolean)
                    intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                    BolGroupby = ObjDataHandler.DataValue_Out(ObjDataRow("IsGroupBy"), Data.SqlDbType.Bit)
                    StrFieldName = ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                    StrFormula = ObjDataHandler.DataValue_Out(ObjDataRow("Formula"), Data.SqlDbType.VarChar)
                    Sorted = ObjDataHandler.DataValue_Out(ObjDataRow("IsSorted"), Data.SqlDbType.Int)
                    IntColumnIndex = ObjDataHandler.DataValue_Out(ObjDataRow("Rank"), Data.SqlDbType.Int)
                    ObjFieldE = .GetFieldByName("DtlE" & StrFieldName & IntColumnIndex)
                    ObjFieldA = .GetFieldByName("DtlA" & StrFieldName & IntColumnIndex)
                    IntFieldLanguage = ObjDataHandler.DataValue_Out(ObjDataRow("FieldLanguage"), Data.SqlDbType.Int)
                    If Not ObjFieldE Is Nothing Then
                        If StringHandler.GetValue(ObjFieldE.Tag, "Status").ToUpper <> "FIX" Then
                            If BolGroupby Then
                                updateGroup(Report, IIf(StrFormula <> "", StrFormula, StrFieldName), Sorted, 1)
                                UpdateSection(Report, StrFieldName, 345)
                            ElseIf Sorted > 0 Then
                                updateGroup(Report, IIf(StrFormula <> "", StrFormula, StrFieldName), Sorted, 1)
                                UpdateSection(Report, StrFieldName, 0)
                                IntSectionNo = 0
                            Else
                                IntSectionNo = 0
                            End If
                            With ObjFieldE
                                .Text = IIf(StrFormula <> "", StrFormula, StrFieldName)
                                .Format = ObjDataHandler.DataValue_Out(ObjDataRow("Format"), Data.SqlDbType.VarChar)
                                .Calculated = 1
                                .Top = 5
                                .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                                .Height = 330
                                .Font.Bold = IIf(CType(ObjDataRow("ColumnFontIsBold"), Boolean) = False, -1, 1)
                                .Font.Italic = IIf(CType(ObjDataRow("ColumnFontIsItalic"), Boolean) = False, -1, 1)
                                .Font.Name = ObjDataRow("ColumnFont")
                                .Font.Size = ObjDataRow("ColumnFontSize")
                                .ForeColor = DefaultBackColor(ObjDataRow("ColumnForeColor"), False)
                                .BackColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                                .BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("BorderStyle"), Data.SqlDbType.Int)
                                .BorderColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                                .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnValignment"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHalignment"), Data.SqlDbType.VarChar))
                                .Visible = IIf(Not BolNotVisable, 1, 0)
                                .ZOrder = -1
                            End With
                        End If
                    Else
                        ObjFieldE = New Field
                        If BolGroupby Then
                            CreateGroup(Report, IIf(StrFormula <> "", StrFormula, StrFieldName), Sorted, 1)
                            IntSectionNo = CreateSection(Report, StrFieldName, 345)
                        ElseIf Sorted > 0 Then
                            CreateGroup(Report, IIf(StrFormula <> "", StrFormula, StrFieldName), Sorted, 1)
                            CreateSection(Report, StrFieldName, 0)
                            IntSectionNo = 0
                        Else
                            IntSectionNo = 0
                        End If
                        With ObjFieldE
                            .Name = "DtlE" & StrFieldName & IntColumnIndex
                            .Section = IntSectionNo
                            .Text = IIf(StrFormula <> "", StrFormula, StrFieldName)
                            .Format = ObjDataHandler.DataValue_Out(ObjDataRow("Format"), Data.SqlDbType.VarChar)
                            .Calculated = 1
                            .Top = 5
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Left = 0
                            .Height = 330
                            .Font.Bold = IIf(CType(ObjDataRow("ColumnFontIsBold"), Boolean) = False, -1, 1)
                            .Font.Italic = IIf(CType(ObjDataRow("ColumnFontIsItalic"), Boolean) = False, -1, 1)
                            .Font.Name = ObjDataRow("ColumnFont")
                            .Font.Size = ObjDataRow("ColumnFontSize")
                            .ForeColor = DefaultBackColor(ObjDataRow("ColumnForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                            .BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("BorderStyle"), Data.SqlDbType.Int)
                            .BorderColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                            .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnValignment"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHalignment"), Data.SqlDbType.VarChar))
                            .Visible = IIf(Not BolNotVisable, 1, 0)
                            .ZOrder = -1
                        End With
                        If IntFieldLanguage = 1 Or IntFieldLanguage = 3 Then
                            .AddField(ObjFieldE)
                        End If
                    End If
                    '===========================================================
                    If Not ObjFieldA Is Nothing Then
                        If StringHandler.GetValue(ObjFieldA.Tag, "Status").ToUpper <> "FIX" Then
                            If BolGroupby Then
                                updateGroup(Report, IIf(StrFormula <> "", StrFormula, StrFieldName), Sorted, 1)
                                UpdateSection(Report, StrFieldName, 345)
                            ElseIf Sorted > 0 Then
                                updateGroup(Report, IIf(StrFormula <> "", StrFormula, StrFieldName), Sorted, 1)
                                UpdateSection(Report, StrFieldName, 0)
                                IntSectionNo = 0
                            Else
                                IntSectionNo = 0
                            End If
                            With ObjFieldA
                                .Text = IIf(StrFormula <> "", StrFormula, StrFieldName)
                                .Format = ObjDataHandler.DataValue_Out(ObjDataRow("Format"), Data.SqlDbType.VarChar)
                                .Calculated = 1
                                .Top = 5
                                .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                                .Height = 330
                                .Font.Bold = IIf(CType(ObjDataRow("ColumnFontIsBold"), Boolean) = False, -1, 1)
                                .Font.Italic = IIf(CType(ObjDataRow("ColumnFontIsItalic"), Boolean) = False, -1, 1)
                                .Font.Name = ObjDataRow("ColumnFont")
                                .Font.Size = ObjDataRow("ColumnFontSize")
                                .ForeColor = DefaultBackColor(ObjDataRow("ColumnForeColor"), False)
                                .BackColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                                .BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("BorderStyle"), Data.SqlDbType.Int)
                                .BorderColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                                .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnValignment"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHalignment"), Data.SqlDbType.VarChar))
                                .Visible = IIf(Not BolNotVisable, 1, 0)
                                .ZOrder = -1
                            End With
                        End If
                    Else
                        ObjFieldA = New Field
                        If BolGroupby Then
                            CreateGroup(Report, IIf(StrFormula <> "", StrFormula, StrFieldName), Sorted, 1)
                            IntSectionNo = CreateSection(Report, StrFieldName, 345)
                        ElseIf Sorted > 0 Then
                            CreateGroup(Report, IIf(StrFormula <> "", StrFormula, StrFieldName), Sorted, 1)
                            CreateSection(Report, StrFieldName, 0)
                            IntSectionNo = 0
                        Else
                            IntSectionNo = 0
                        End If
                        With ObjFieldA
                            .Name = "DtlA" & StrFieldName & IntColumnIndex
                            .Section = IntSectionNo
                            .Text = IIf(StrFormula <> "", StrFormula, StrFieldName)
                            .Format = ObjDataHandler.DataValue_Out(ObjDataRow("Format"), Data.SqlDbType.VarChar)
                            .Calculated = 1
                            .Top = 5
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Left = 0
                            .Height = 330
                            .Font.Bold = IIf(CType(ObjDataRow("ColumnFontIsBold"), Boolean) = False, -1, 1)
                            .Font.Italic = IIf(CType(ObjDataRow("ColumnFontIsItalic"), Boolean) = False, -1, 1)
                            .Font.Name = ObjDataRow("ColumnFont")
                            .Font.Size = ObjDataRow("ColumnFontSize")
                            .ForeColor = DefaultBackColor(ObjDataRow("ColumnForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                            .BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("BorderStyle"), Data.SqlDbType.Int)
                            .BorderColor = DefaultBackColor(ObjDataRow("ColumnBackColor"), True)
                            .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnValignment"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnHalignment"), Data.SqlDbType.VarChar))
                            .Visible = IIf(Not BolNotVisable, 1, 0)
                            .ZOrder = -1
                        End With
                        If IntFieldLanguage = 2 Or IntFieldLanguage = 3 Then
                            .AddField(ObjFieldA)
                        End If
                    End If
                    'ToDo CreateFieldsBorders(Report, ObjField.Name, ObjDataRow)
                Next
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function    'Waiting For QC.

    Private Function CreateFieldsFooter(ByRef Report As Report, _
    ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtColumns As Data.DataTable) As Boolean

        Dim ObjDataRow As Data.DataRow
        Dim IntForeColor As Integer = 0
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim intCurrentWidth As Integer
        Dim ObjField As Field
        Dim bolNotVisable As Boolean
        Dim BolPerVisable As Boolean
        Dim StrFieldText As String = String.Empty
        Dim IntcolumnIndex As Integer
        Dim IntFieldLanguage As Integer
        Dim IntTotalWidthEnglish As Integer = Report.Layout.MarginLeft
        Dim IntTotalWidthArabic As Integer = Report.Layout.Width - Report.Layout.MarginRight
        Try
            With Report.Fields
                For Each ObjDataRow In DtColumns.Select("FieldName <> ''", "Rank")
                    ObjField = New Field
                    bolNotVisable = CType(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnIsHidden"), Data.SqlDbType.Bit), Boolean)
                    intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                    StrFieldText = ObjDataHandler.DataValue_Out(ObjDataRow("Formula"), Data.SqlDbType.VarChar)
                    IntcolumnIndex = ObjDataHandler.DataValue_Out(ObjDataRow("Rank"), Data.SqlDbType.Int)
                    IntFieldLanguage = ObjDataHandler.DataValue_Out(ObjDataRow("FieldLanguage"), Data.SqlDbType.Int)
                    If IntFieldLanguage = 1 Or IntFieldLanguage = 3 Or IntFieldLanguage = 0 Then
                        With ObjField
                            .Name = "FtrE" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntcolumnIndex
                            .Section = 4
                            .Text = IIf(StrFieldText <> "", StrFieldText, ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar))
                            .Calculated = 1
                            .Top = 0
                            intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Left = IntTotalWidthEnglish
                            .Height = 330
                            .Font.Bold = IIf(ReportsProperties.FooterFontIsBold = False, -1, 1)
                            .Font.Italic = IIf(ReportsProperties.FooterFontIsItalic = False, -1, 1)
                            .Font.Name = ReportsProperties.FooterFont
                            .Font.Size = ReportsProperties.HeaderFontSize
                            .ForeColor = DefaultBackColor(ObjDataRow("ColFooterForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                            .BorderStyle = ReportsProperties.FooterBorderStyle
                            .BorderColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                            .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnFooterVAlignmnet"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnFooterHAlignment"), Data.SqlDbType.VarChar))
                            .Visible = IIf(Not bolNotVisable, 1, 0)
                            .Tag = "Language=E;"
                            IntTotalWidthEnglish += .Width + 10
                        End With
                        .AddField(ObjField)
                    End If
                    ObjField = New Field
                    If IntFieldLanguage = 2 Or IntFieldLanguage = 3 Or IntFieldLanguage = 0 Then
                        With ObjField
                            .Name = "FtrA" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntcolumnIndex
                            .Section = 4
                            .Text = IIf(StrFieldText <> "", StrFieldText, ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar))
                            .Calculated = 1
                            .Top = 0
                            intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Left = IntTotalWidthArabic - IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Height = 330
                            .Font.Bold = IIf(ReportsProperties.FooterFontIsBold = False, -1, 1)
                            .Font.Italic = IIf(ReportsProperties.FooterFontIsItalic = False, -1, 1)
                            .Font.Name = ReportsProperties.FooterFont
                            .Font.Size = ReportsProperties.HeaderFontSize
                            .ForeColor = DefaultBackColor(ObjDataRow("ColFooterForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                            .BorderStyle = ReportsProperties.FooterBorderStyle
                            .BorderColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                            .Align = 8
                            .Visible = IIf(Not bolNotVisable, 1, 0)
                            .Tag = "Language=A;"
                            IntTotalWidthArabic -= .Width - 10
                        End With
                        .AddField(ObjField)
                    End If
                    'ToDo CreateFieldsFooterBorders(Report, ObjField.Name, ObjDataRow)
                Next

            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function     'Waiting For QC.

    Private Function UpdateFieldsFooter(ByRef Report As Report, _
ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtColumns As Data.DataTable) As Boolean

        Dim ObjDataRow As Data.DataRow
        Dim IntForeColor As Integer = 0
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim intCurrentWidth As Integer
        Dim ObjFieldE As Field
        Dim ObjFieldA As Field
        Dim bolNotVisable As Boolean
        Dim BolPerVisable As Boolean
        Dim StrFieldText As String = String.Empty
        Dim IntColumnIndex As Integer
        Dim IntFieldLanguage As Integer
        Dim StringHandler As New Venus.Shared.StringHandler
        Try
            With Report.Fields
                For Each ObjDataRow In DtColumns.Select("FieldName <> ''", "Rank")
                    ObjFieldE = New Field
                    ObjFieldA = New Field
                    bolNotVisable = CType(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnIsHidden"), Data.SqlDbType.Bit), Boolean)
                    intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                    StrFieldText = ObjDataHandler.DataValue_Out(ObjDataRow("Formula"), Data.SqlDbType.VarChar)
                    IntColumnIndex = ObjDataHandler.DataValue_Out(ObjDataRow("Rank"), Data.SqlDbType.Int)
                    ObjFieldE = .GetFieldByName("FtrE" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntColumnIndex)
                    ObjFieldA = .GetFieldByName("FtrA" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntColumnIndex)
                    IntFieldLanguage = ObjDataHandler.DataValue_Out(ObjDataRow("FieldLanguage"), Data.SqlDbType.Int)
                    If Not ObjFieldE Is Nothing Then
                        If StringHandler.GetValue(ObjFieldE.Tag, "Status").ToUpper <> "FIX" Then
                            With ObjFieldE
                                .Section = 4
                                .Text = IIf(StrFieldText <> "", StrFieldText, ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar))
                                .Calculated = 1
                                .Top = 0
                                intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                                .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                                .Height = 330
                                .Font.Bold = IIf(ReportsProperties.FooterFontIsBold = False, -1, 1)
                                .Font.Italic = IIf(ReportsProperties.FooterFontIsItalic = False, -1, 1)
                                .Font.Name = ReportsProperties.FooterFont
                                .Font.Size = ReportsProperties.HeaderFontSize
                                .ForeColor = DefaultBackColor(ObjDataRow("ColFooterForeColor"), False)
                                .BackColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                                .BorderStyle = ReportsProperties.FooterBorderStyle
                                .BorderColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                                .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnFooterVAlignmnet"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnFooterHAlignment"), Data.SqlDbType.VarChar))
                                .Visible = IIf(Not bolNotVisable, 1, 0)
                            End With
                        End If
                    Else
                        ObjFieldE = New Field
                        With ObjFieldE
                            .Name = "FtrE" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntColumnIndex
                            .Section = 4
                            .Text = IIf(StrFieldText <> "", StrFieldText, ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar))
                            .Calculated = 1
                            .Top = 0
                            intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Left = 0
                            .Height = 330
                            .Font.Bold = IIf(ReportsProperties.FooterFontIsBold = False, -1, 1)
                            .Font.Italic = IIf(ReportsProperties.FooterFontIsItalic = False, -1, 1)
                            .Font.Name = ReportsProperties.FooterFont
                            .Font.Size = ReportsProperties.HeaderFontSize
                            .ForeColor = DefaultBackColor(ObjDataRow("ColFooterForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                            .BorderStyle = ReportsProperties.FooterBorderStyle
                            .BorderColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                            .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnFooterVAlignmnet"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnFooterHAlignment"), Data.SqlDbType.VarChar))
                            .Visible = IIf(Not bolNotVisable And BolPerVisable, 1, 0)
                        End With
                        If IntFieldLanguage = 1 And IntFieldLanguage = 3 Then
                            .AddField(ObjFieldE)
                        End If
                    End If
                    '==================================================
                    If Not ObjFieldA Is Nothing Then
                        If StringHandler.GetValue(ObjFieldA.Tag, "Status").ToUpper <> "FIX" Then
                            With ObjFieldE
                                .Section = 4
                                .Text = IIf(StrFieldText <> "", StrFieldText, ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar))
                                .Calculated = 1
                                .Top = 0
                                intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                                .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                                .Height = 330
                                .Font.Bold = IIf(ReportsProperties.FooterFontIsBold = False, -1, 1)
                                .Font.Italic = IIf(ReportsProperties.FooterFontIsItalic = False, -1, 1)
                                .Font.Name = ReportsProperties.FooterFont
                                .Font.Size = ReportsProperties.HeaderFontSize
                                .ForeColor = DefaultBackColor(ObjDataRow("ColFooterForeColor"), False)
                                .BackColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                                .BorderStyle = ReportsProperties.FooterBorderStyle
                                .BorderColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                                .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnFooterVAlignmnet"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnFooterHAlignment"), Data.SqlDbType.VarChar))
                                .Visible = IIf(Not bolNotVisable, 1, 0)
                            End With
                        End If
                    Else
                        ObjFieldA = New Field
                        With ObjFieldA
                            .Name = "FtrA" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar) & IntColumnIndex
                            .Section = 4
                            .Text = IIf(StrFieldText <> "", StrFieldText, ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar))
                            .Calculated = 1
                            .Top = 0
                            intCurrentWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColumnWidth"), Data.SqlDbType.Int)
                            .Width = IIf(intCurrentWidth = 0, 100, intCurrentWidth) * 13.6
                            .Left = 0
                            .Height = 330
                            .Font.Bold = IIf(ReportsProperties.FooterFontIsBold = False, -1, 1)
                            .Font.Italic = IIf(ReportsProperties.FooterFontIsItalic = False, -1, 1)
                            .Font.Name = ReportsProperties.FooterFont
                            .Font.Size = ReportsProperties.HeaderFontSize
                            .ForeColor = DefaultBackColor(ObjDataRow("ColFooterForeColor"), False)
                            .BackColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                            .BorderStyle = ReportsProperties.FooterBorderStyle
                            .BorderColor = DefaultBackColor(ObjDataRow("ColFooterBackColor"), True)
                            .Align = MapColumnAligment(ObjDataHandler.DataValue_Out(ObjDataRow("ColumnFooterVAlignmnet"), Data.SqlDbType.VarChar), ObjDataHandler.DataValue_Out(ObjDataRow("ColumnFooterHAlignment"), Data.SqlDbType.VarChar))
                            .Visible = IIf(Not bolNotVisable And BolPerVisable, 1, 0)
                        End With
                        If IntFieldLanguage = 2 And IntFieldLanguage = 3 Then
                            .AddField(ObjFieldA)
                        End If
                    End If
                    'ToDo CreateFieldsFooterBorders(Report, ObjField.Name, ObjDataRow)
                Next

            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function     'Waiting For QC.

    '==============(Row's Properties Section)==============

    Private Function CreateRowProperities(ByRef Report As Report, _
    ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtRows As Data.DataTable) As Boolean
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim IntCounter As Integer
        Dim IntBackColor As Integer
        Dim IntForeColor As Integer
        Dim IntRowHeight As Integer
        Dim StrFont As String
        Dim BolFontIsItalic As Boolean
        Dim BolFontIsBold As Boolean
        Dim IntTopBorderColor As Integer
        Dim IntTopBorderStyle As Integer
        Dim IntBottomBorderColor As Integer
        Dim IntBottomBorderStyle As Integer
        Dim IntLeftBorderColor As Integer
        Dim IntLeftBorderStyle As Integer
        Dim IntRightBorderColor As Integer
        Dim IntRightBorderStyle As Integer

        Try

            If DtRows.Rows.Count < 1 Then
                Exit Function
            End If

            IntBackColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(IIf(DtRows.Rows(0)("BackColor") = "", "White", DtRows.Rows(0)("BackColor")), Data.SqlDbType.VarChar)).ToArgb
            IntForeColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("ForeColor"), Data.SqlDbType.VarChar)).ToArgb
            IntRowHeight = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("RowHieght"), Data.SqlDbType.Int)
            StrFont = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("Font"), Data.SqlDbType.VarChar)
            BolFontIsItalic = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("FontIsItalic"), Data.SqlDbType.Bit)
            BolFontIsBold = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("FontIsBold"), Data.SqlDbType.Bit)
            IntTopBorderColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("TopBorderColor"), Data.SqlDbType.VarChar)).ToArgb
            IntTopBorderStyle = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("TopBorderStyle"), Data.SqlDbType.Int)
            IntBottomBorderColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("BottomBorderColor"), Data.SqlDbType.VarChar)).ToArgb
            IntBottomBorderStyle = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("BottomBorderStyle"), Data.SqlDbType.Int)
            IntLeftBorderColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("LeftBorderColor"), Data.SqlDbType.VarChar)).ToArgb
            IntLeftBorderStyle = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("LeftBorderStyle"), Data.SqlDbType.Int)
            IntRightBorderColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("RightBorderColor"), Data.SqlDbType.VarChar)).ToArgb
            IntRightBorderStyle = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("RightBorderStyle"), Data.SqlDbType.Int)

            For IntCounter = 0 To Report.Fields.Count - 1
                With Report.Fields.Field(IntCounter)
                    If .Section = 0 Then
                        If .BackColor = -1 Then
                            .BackColor = IntBackColor
                        End If
                        If .ForeColor = 0 Then
                            .ForeColor = IntForeColor
                        End If
                        If .Height = 330 And IntRowHeight <> 0 Then
                            .Height = IntRowHeight * 10
                        End If
                        If .Font.Name = "" Then
                            .Font.Name = StrFont
                        End If
                        If .BorderStyle = -1 And .Visible <> 0 Then
                            Dim ObjLine As New Field
                            ObjLine.Name = "Line_" & .Name & "_Top"
                            ObjLine.Width = .Width + 5
                            ObjLine.Top = .Top
                            ObjLine.Left = .Left
                            ObjLine.Height = 0
                            ObjLine.LineSlant = 1
                            ObjLine.LineWidth = 0
                            ObjLine.BorderColor = IntTopBorderColor + 65791
                            ObjLine.BorderStyle = IntTopBorderStyle
                            ObjLine.Section = 0
                            Report.Fields.AddField(ObjLine)
                            ObjLine = New Field
                            ObjLine.Name = "Line_" & .Name & "_Bottom"
                            ObjLine.Width = .Width + 5
                            ObjLine.Top = .Top + .Height
                            ObjLine.Left = .Left
                            ObjLine.Height = 0
                            ObjLine.LineSlant = 1
                            ObjLine.LineWidth = 0
                            ObjLine.BorderColor = IntBottomBorderColor + 65791
                            ObjLine.BorderStyle = IntBottomBorderStyle
                            ObjLine.Section = 0
                            Report.Fields.AddField(ObjLine)
                            ObjLine = New Field
                            ObjLine.Name = "Line_" & .Name & "_Left"
                            ObjLine.Width = 0
                            ObjLine.Top = .Top
                            ObjLine.Left = .Left
                            ObjLine.Height = .Height + 10
                            ObjLine.LineSlant = 1
                            ObjLine.LineWidth = 0
                            ObjLine.BorderColor = IntLeftBorderColor + 65791
                            ObjLine.BorderStyle = IntLeftBorderStyle
                            ObjLine.Section = 0
                            Report.Fields.AddField(ObjLine)
                            ObjLine = New Field
                            ObjLine.Name = "Line_" & .Name & "_Right"
                            ObjLine.Width = 0
                            ObjLine.Top = .Top
                            ObjLine.Left = .Left + .Width
                            ObjLine.Height = .Height + 10
                            ObjLine.LineSlant = 1
                            ObjLine.LineWidth = 0
                            ObjLine.BorderColor = IntRightBorderColor + 65791
                            ObjLine.BorderStyle = IntRightBorderStyle
                            ObjLine.Section = 0
                            Report.Fields.AddField(ObjLine)
                        End If
                    End If
                End With
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function UpdateRowProperities(ByRef Report As Report, _
ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtRows As Data.DataTable) As Boolean
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim IntCounter As Integer
        Dim IntBackColor As Integer
        Dim IntForeColor As Integer
        Dim IntRowHeight As Integer
        Dim StrFont As String
        Dim BolFontIsItalic As Boolean
        Dim BolFontIsBold As Boolean
        Dim IntTopBorderColor As Integer
        Dim IntTopBorderStyle As Integer
        Dim IntBottomBorderColor As Integer
        Dim IntBottomBorderStyle As Integer
        Dim IntLeftBorderColor As Integer
        Dim IntLeftBorderStyle As Integer
        Dim IntRightBorderColor As Integer
        Dim IntRightBorderStyle As Integer

        Try

            If DtRows.Rows.Count < 1 Then
                Exit Function
            End If

            IntBackColor = ObjColorConvertor.ConvertFromString(IIf(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("BackColor"), Data.SqlDbType.VarChar) = "", "White", ObjDataHandler.DataValue_Out(DtRows.Rows(0)("BackColor"), Data.SqlDbType.VarChar))).ToArgb
            IntForeColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("ForeColor"), Data.SqlDbType.VarChar)).ToArgb
            IntRowHeight = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("RowHieght"), Data.SqlDbType.Int)
            StrFont = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("Font"), Data.SqlDbType.VarChar)
            BolFontIsItalic = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("FontIsItalic"), Data.SqlDbType.Bit)
            BolFontIsBold = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("FontIsBold"), Data.SqlDbType.Bit)
            IntTopBorderColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("TopBorderColor"), Data.SqlDbType.VarChar)).ToArgb
            IntTopBorderStyle = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("TopBorderStyle"), Data.SqlDbType.Int)
            IntBottomBorderColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("BottomBorderColor"), Data.SqlDbType.VarChar)).ToArgb
            IntBottomBorderStyle = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("BottomBorderStyle"), Data.SqlDbType.Int)
            IntLeftBorderColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("LeftBorderColor"), Data.SqlDbType.VarChar)).ToArgb
            IntLeftBorderStyle = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("LeftBorderStyle"), Data.SqlDbType.Int)
            IntRightBorderColor = ObjColorConvertor.ConvertFromString(ObjDataHandler.DataValue_Out(DtRows.Rows(0)("RightBorderColor"), Data.SqlDbType.VarChar)).ToArgb
            IntRightBorderStyle = ObjDataHandler.DataValue_Out(DtRows.Rows(0)("RightBorderStyle"), Data.SqlDbType.Int)

            For IntCounter = 0 To Report.Fields.Count - 1
                With Report.Fields.Field(IntCounter)
                    If .Section = 0 Then
                        If .BackColor = -1 Then
                            .BackColor = IntBackColor
                        End If
                        If .ForeColor = 0 Then
                            .ForeColor = IntForeColor
                        End If
                        If .Height = 330 And IntRowHeight <> 0 Then
                            .Height = IntRowHeight * 10
                        End If
                        If .Font.Name = "" Then
                            .Font.Name = StrFont
                        End If
                        If .BorderStyle = -1 And .Visible <> 0 Then
                            Dim ObjLine As New Field
                            ObjLine = Report.Fields.GetFieldByName("Line_" & .Name & "_Top")
                            If Not ObjLine Is Nothing Then
                                ObjLine.Width = .Width + 5
                                ObjLine.Top = .Top
                                ObjLine.Left = .Left
                                ObjLine.Height = 0
                                ObjLine.LineSlant = 1
                                ObjLine.LineWidth = 0
                                ObjLine.BorderColor = IntTopBorderColor + 65791
                                ObjLine.BorderStyle = IntTopBorderStyle
                                ObjLine.Section = .Section
                            Else
                                ObjLine = New Field
                                ObjLine.Name = "Line_" & .Name & "_Top"
                                ObjLine.Width = .Width + 5
                                ObjLine.Top = .Top
                                ObjLine.Left = .Left
                                ObjLine.Height = 0
                                ObjLine.LineSlant = 1
                                ObjLine.LineWidth = 0
                                ObjLine.BorderColor = IntTopBorderColor + 65791
                                ObjLine.BorderStyle = IntTopBorderStyle
                                ObjLine.Section = .Section
                                Report.Fields.AddField(ObjLine)
                            End If

                            ObjLine = New Field
                            ObjLine = Report.Fields.GetFieldByName("Line_" & .Name & "_Bottom")
                            If Not ObjLine Is Nothing Then
                                ObjLine.Width = .Width + 5
                                ObjLine.Top = .Top + .Height
                                ObjLine.Left = .Left
                                ObjLine.Height = 0
                                ObjLine.LineSlant = 1
                                ObjLine.LineWidth = 0
                                ObjLine.BorderColor = IntBottomBorderColor + 65791
                                ObjLine.BorderStyle = IntBottomBorderStyle
                                ObjLine.Section = .Section
                            Else
                                ObjLine = New Field
                                ObjLine.Name = "Line_" & .Name & "_Bottom"
                                ObjLine.Width = .Width + 5
                                ObjLine.Top = .Top + .Height
                                ObjLine.Left = .Left
                                ObjLine.Height = 0
                                ObjLine.LineSlant = 1
                                ObjLine.LineWidth = 0
                                ObjLine.BorderColor = IntBottomBorderColor + 65791
                                ObjLine.BorderStyle = IntBottomBorderStyle
                                ObjLine.Section = .Section
                                Report.Fields.AddField(ObjLine)
                            End If

                            ObjLine = New Field
                            ObjLine = Report.Fields.GetFieldByName("Line_" & .Name & "_Left")
                            If Not ObjLine Is Nothing Then
                                ObjLine.Width = 0
                                ObjLine.Top = .Top
                                ObjLine.Left = .Left
                                ObjLine.Height = .Height + 10
                                ObjLine.LineSlant = 1
                                ObjLine.LineWidth = 0
                                ObjLine.BorderColor = IntLeftBorderColor + 65791
                                ObjLine.BorderStyle = IntLeftBorderStyle
                                ObjLine.Section = .Section
                            Else
                                ObjLine = New Field
                                ObjLine.Name = "Line_" & .Name & "_Left"
                                ObjLine.Width = 0
                                ObjLine.Top = .Top
                                ObjLine.Left = .Left
                                ObjLine.Height = .Height + 10
                                ObjLine.LineSlant = 1
                                ObjLine.LineWidth = 0
                                ObjLine.BorderColor = IntLeftBorderColor + 65791
                                ObjLine.BorderStyle = IntLeftBorderStyle
                                ObjLine.Section = .Section
                                Report.Fields.AddField(ObjLine)
                            End If

                            ObjLine = New Field
                            ObjLine = Report.Fields.GetFieldByName("Line_" & .Name & "_Right")
                            If Not ObjLine Is Nothing Then
                                ObjLine.Width = 0
                                ObjLine.Top = .Top
                                ObjLine.Left = .Left + .Width
                                ObjLine.Height = .Height + 10
                                ObjLine.LineSlant = 1
                                ObjLine.LineWidth = 0
                                ObjLine.BorderColor = IntRightBorderColor + 65791
                                ObjLine.BorderStyle = IntRightBorderStyle
                                ObjLine.Section = .Section
                            Else
                                ObjLine = New Field
                                ObjLine.Name = "Line_" & .Name & "_Right"
                                ObjLine.Width = 0
                                ObjLine.Top = .Top
                                ObjLine.Left = .Left + .Width
                                ObjLine.Height = .Height + 10
                                ObjLine.LineSlant = 1
                                ObjLine.LineWidth = 0
                                ObjLine.BorderColor = IntRightBorderColor + 65791
                                ObjLine.BorderStyle = IntRightBorderStyle
                                ObjLine.Section = .Section
                                Report.Fields.AddField(ObjLine)
                            End If
                        End If
                    End If
                End With
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    '===================(Report Groups)====================

    Private Function CreateGroup(ByRef Report As Report, ByVal FieldName As String, ByVal Sorted As Integer, ByVal KeepTogether As Integer) As Boolean
        Dim ObjGroup As Group
        Try
            With Report.Groups
                ObjGroup = New Group
                With ObjGroup
                    '.Name = "Group" & Report.Groups.Count
                    .Name = "Group" & FieldName
                    .GroupBy = FieldName
                    .Sort = Sorted
                    .KeepTogether = KeepTogether
                End With
                .AddGroup(ObjGroup)
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function updateGroup(ByRef Report As Report, ByVal FieldName As String, ByVal Sorted As Integer, ByVal KeepTogether As Integer) As Boolean
        Dim ObjGroup As Group
        Dim StringHandler As New Venus.Shared.StringHandler
        Try
            With Report.Groups
                ObjGroup = New Group
                ObjGroup = .GetGroupByName("Group" & FieldName)
                If Not ObjGroup Is Nothing Then
                    If StringHandler.GetValue(ObjGroup.Tag, "Status").ToUpper <> "FIX" Then
                        ObjGroup.Sort = Sorted
                        ObjGroup.KeepTogether = KeepTogether
                    End If
                Else
                    ObjGroup = New Group
                    With ObjGroup
                        .Name = "Group" & FieldName
                        .GroupBy = FieldName
                        .Sort = Sorted
                        .KeepTogether = KeepTogether
                    End With
                    .AddGroup(ObjGroup)
                End If
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CreateSection(ByVal Report As Report, ByVal FieldName As String, ByVal SectionHeight As Integer) As Integer
        Dim ObjSection As Section
        Dim IntSectionNumber As Integer = 0
        Try
            With Report.Sections
                ObjSection = New Section
                With ObjSection
                    .Name = "Section_" & FieldName & "_Header"
                    .Height = SectionHeight
                    .Type = Report.Sections.Count
                    .BackColor = Report.Layout.BackColor
                End With
                IntSectionNumber = .AddSection(ObjSection)
                ObjSection = New Section
                With ObjSection
                    .Name = "Section_" & FieldName & "_Footer"
                    .Height = 0
                    .Type = Report.Sections.Count
                    .BackColor = Report.Layout.BackColor
                End With
                .AddSection(ObjSection)
                Return IntSectionNumber
            End With
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function UpdateSection(ByVal Report As Report, ByVal FieldName As String, ByVal SectionHeight As Integer) As Integer
        Dim ObjSection As Section
        Dim StringHandler As New Venus.Shared.StringHandler
        Dim IntSectionNumber As Integer = 0
        Try
            With Report.Sections
                ObjSection = New Section
                ObjSection = .GetSectionByName("Section_" & FieldName & "_Header")
                If Not ObjSection Is Nothing Then
                    If StringHandler.GetValue(ObjSection.Tag, "Status").ToUpper <> "FIX" Then
                        ObjSection.Height = SectionHeight
                        ObjSection.BackColor = Report.Layout.BackColor
                    End If
                Else
                    ObjSection = New Section
                    With ObjSection
                        .Name = "Section_" & FieldName & "_Header"
                        .Height = SectionHeight
                        .Type = Report.Sections.Count
                        .BackColor = Report.Layout.BackColor
                    End With
                    IntSectionNumber = .AddSection(ObjSection)
                End If

                ObjSection = New Section
                ObjSection = .GetSectionByName("Section_" & FieldName & "_Footer")
                If Not ObjSection Is Nothing Then
                    If StringHandler.GetValue(ObjSection.Tag, "Status").ToUpper <> "FIX" Then
                        ObjSection.Height = 0
                        ObjSection.BackColor = Report.Layout.BackColor
                    End If
                Else
                    ObjSection = New Section
                    With ObjSection
                        .Name = "Section_" & FieldName & "_Footer"
                        .Height = 0
                        .Type = Report.Sections.Count
                        .BackColor = Report.Layout.BackColor
                    End With
                    .AddSection(ObjSection)
                End If
                Return IntSectionNumber
            End With
        Catch ex As Exception
            Return 0
        End Try
    End Function

    '==================(Criteria Section)===================

    Private Function CreateCriteria(ByRef Report As Report, _
    ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtColumns As Data.DataTable) As Boolean
        Dim ObjDataRow As Data.DataRow
        Dim IntForeColor As Integer = 0
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ObjField As Field
        Dim bolVisable As Boolean
        Dim StrTextEnglish As String = String.Empty
        Dim StrTextArabic As String = String.Empty
        Dim StrFieldName As String = String.Empty
        Dim IntSection As Integer = 0
        Dim IntNoofControlsPerSection As Integer = 0
        Dim intCurrentTop As Integer = Report.Layout.MarginTop + 1500 + 105
        Dim IntCurrentLeftEnglish As Integer = Report.Layout.MarginLeft + 105
        Dim IntCurrentRightArabic As Integer = Report.Layout.Width - Report.Layout.MarginRight - 2145
        Try
            With Report.Fields
                For Each ObjDataRow In DtColumns.Select("Status = true", "Rank")
                    ObjField = New Field
                    StrTextEnglish = ObjDataHandler.DataValue_Out(ObjDataRow("EngDescription"), Data.SqlDbType.VarChar)
                    StrTextArabic = ObjDataHandler.DataValue_Out(ObjDataRow("ArbDescription"), Data.SqlDbType.VarChar)
                    StrFieldName = ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                    With ObjField
                        .Name = "CriCapE" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                        .Section = 1
                        .Text = IIf(StrTextEnglish = "", StrFieldName, StrTextEnglish) & " :"
                        .Calculated = 0
                        .Top = intCurrentTop
                        .Left = IntCurrentLeftEnglish
                        .Width = 2040
                        .Height = 330
                        .Font.Bold = 1
                        .Font.Italic = 1
                        .Font.Name = ReportsProperties.HeaderFont
                        .Font.Size = 8
                        .ForeColor = DefaultBackColor("", False)
                        .BackColor = 16119285 'This Represent the white smoke color
                        .BackStyle = 1
                        .BorderStyle = 1
                        .BorderColor = DefaultBackColor("", False)
                        .Align = 6
                        .Tag = "Language=E;"
                        .Visible = IIf(bolVisable, 0, 1)
                    End With
                    .AddField(ObjField)
                    ObjField = New Field
                    With ObjField
                        .Name = "CriCapA" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                        .Section = 1
                        .Text = IIf(StrTextArabic = "", StrFieldName, StrTextArabic) & " :"
                        .Calculated = 0
                        .Top = intCurrentTop
                        .Left = IntCurrentRightArabic
                        .Width = 2040
                        .Height = 330
                        .Font.Bold = 1
                        .Font.Italic = 1
                        .Font.Name = ReportsProperties.HeaderFont
                        .Font.Size = 8
                        .ForeColor = DefaultBackColor("", False)
                        .BackColor = 16119285
                        .BackStyle = 1
                        .BorderStyle = 1
                        .BorderColor = DefaultBackColor("", False)
                        .Align = 8
                        .Tag = "Language=A;"
                        .Visible = IIf(bolVisable, 0, 1)
                    End With
                    .AddField(ObjField)

                    '===================Create Value======================
                    ObjField = New Field
                    With ObjField
                        .Name = "CriValE" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                        .Section = 1
                        .Text = "CriVal" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                        .Calculated = 1
                        .Top = intCurrentTop
                        .Left = IntCurrentLeftEnglish + 2160
                        .Width = 2265
                        .Height = 330
                        .Font.Bold = 1
                        .Font.Italic = 1
                        .Font.Name = ReportsProperties.HeaderFont
                        .Font.Size = 8
                        .ForeColor = DefaultBackColor("", False)
                        .BackColor = DefaultBackColor("", True)
                        .BorderStyle = 1
                        .BorderColor = DefaultBackColor("", False)
                        .Align = 6
                        .Tag = "Language=E;"
                        .Visible = IIf(bolVisable, 0, 1)
                    End With
                    .AddField(ObjField)
                    ObjField = New Field
                    With ObjField
                        .Name = "CriValA" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                        .Section = 1
                        .Text = "CriVal" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                        .Calculated = 1
                        .Top = intCurrentTop
                        .Left = IntCurrentRightArabic - 2160 - 225
                        .Width = 2265
                        .Height = 330
                        .Font.Bold = 1
                        .Font.Italic = 1
                        .Font.Name = ReportsProperties.HeaderFont
                        .Font.Size = 8
                        .ForeColor = DefaultBackColor("", False)
                        .BackColor = DefaultBackColor("", True)
                        .BorderStyle = 1
                        .BorderColor = DefaultBackColor("", False)
                        .Align = 8
                        .Tag = "Language=A;"
                        .Visible = IIf(bolVisable, 0, 1)
                    End With
                    .AddField(ObjField)

                    intCurrentTop += ObjField.Height + 90
                    IntNoofControlsPerSection += 1

                    If IntNoofControlsPerSection = 5 Then
                        IntNoofControlsPerSection = 0
                        intCurrentTop = Report.Layout.MarginTop + 1500 + 105
                        IntCurrentRightArabic -= 2370 - 2160 - 225
                        IntCurrentLeftEnglish += 2370 + 2160
                    End If
                Next
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function             'Waiting for QC.

    Private Function UpdateCriteriaCaption(ByRef Report As Report, _
    ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtColumns As Data.DataTable) As Boolean
        Dim ObjDataRow As Data.DataRow
        Dim IntTotalWidth As Integer = Report.Layout.MarginLeft
        Dim IntForeColor As Integer = 0
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ObjFieldE As Field
        Dim ObjFieldA As Field
        Dim StrTextE As String = String.Empty
        Dim StrTextA As String = String.Empty
        Dim StrFieldName As String = String.Empty
        Dim IntSection As Integer = 0
        Dim IntNoofControlsPerSection As Integer = 0
        Dim intCurrentTop As Integer = Report.Layout.MarginTop + 105
        Dim IntCurrentLeftEnglsih As Integer = Report.Layout.MarginLeft + 105
        Dim IntCurrentLeftArabic As Integer = Report.Layout.Width - Report.Layout.MarginRight - 2145
        Dim StringHandler As New Venus.Shared.StringHandler
        Try
            With Report.Fields
                For Each ObjDataRow In DtColumns.Select("Status = true", "Rank")
                    ObjFieldE = New Field
                    ObjFieldA = New Field

                    ObjFieldE = .GetFieldByName("CriCapE" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar))
                    ObjFieldA = .GetFieldByName("CriCapA" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar))

                    StrTextE = ObjDataHandler.DataValue_Out(ObjDataRow("EngDescription"), Data.SqlDbType.VarChar)
                    StrTextA = ObjDataHandler.DataValue_Out(ObjDataRow("ArbDescription"), Data.SqlDbType.VarChar)

                    StrFieldName = ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)

                    If Not ObjFieldE Is Nothing Then
                        If StringHandler.GetValue(ObjFieldE.Tag, "Status").ToUpper <> "FIX" Then
                            With ObjFieldE
                                .Section = 1
                                .Text = IIf(StrTextE = "", StrFieldName, StrTextE) & " :"
                                .Calculated = -1
                                .Top = intCurrentTop
                                .Width = 2040
                                .Height = 330
                                .Font.Bold = 1
                                .Font.Italic = 1
                                .Font.Name = ReportsProperties.HeaderFont
                                .Font.Size = 8
                                .ForeColor = DefaultBackColor("", False)
                                .BackColor = DefaultBackColor("", True)
                                .BorderStyle = 1
                                .BorderColor = DefaultBackColor("", False)
                                .Align = 0
                            End With
                        End If
                    Else
                        ObjFieldE = New Field
                        With ObjFieldE
                            .Name = "CriCapE" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                            .Section = 1
                            .Text = IIf(StrTextE = "", StrFieldName, StrTextE) & " :"
                            .Calculated = -1
                            .Top = intCurrentTop
                            .Left = 0
                            .Width = 2040
                            .Height = 330
                            .Font.Bold = 1
                            .Font.Italic = 1
                            .Font.Name = ReportsProperties.HeaderFont
                            .Font.Size = 8
                            .ForeColor = DefaultBackColor("", False)
                            .BackColor = DefaultBackColor("", True)
                            .BorderStyle = 1
                            .BorderColor = DefaultBackColor("", False)
                            .Align = 0
                        End With
                        .AddField(ObjFieldE)
                    End If
                    '===========================Arabic============================
                    If Not ObjFieldA Is Nothing Then
                        If StringHandler.GetValue(ObjFieldA.Tag, "Status").ToUpper <> "FIX" Then
                            With ObjFieldA
                                .Section = 1
                                .Text = IIf(StrTextA = "", StrFieldName, StrTextA) & " :"
                                .Calculated = -1
                                .Top = intCurrentTop
                                .Width = 2040
                                .Height = 330
                                .Font.Bold = 1
                                .Font.Italic = 1
                                .Font.Name = ReportsProperties.HeaderFont
                                .Font.Size = 8
                                .ForeColor = DefaultBackColor("", False)
                                .BackColor = DefaultBackColor("", True)
                                .BorderStyle = 1
                                .BorderColor = DefaultBackColor("", False)
                                .Align = 0
                            End With
                        End If
                    Else
                        ObjFieldA = New Field
                        With ObjFieldA
                            .Name = "CriCapA" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                            .Section = 1
                            .Text = IIf(StrTextA = "", StrFieldName, StrTextA) & " :"
                            .Calculated = -1
                            .Top = intCurrentTop
                            .Left = 0
                            .Width = 2040
                            .Height = 330
                            .Font.Bold = 1
                            .Font.Italic = 1
                            .Font.Name = ReportsProperties.HeaderFont
                            .Font.Size = 8
                            .ForeColor = DefaultBackColor("", False)
                            .BackColor = DefaultBackColor("", True)
                            .BorderStyle = 1
                            .BorderColor = DefaultBackColor("", False)
                            .Align = 0
                        End With
                        .AddField(ObjFieldA)
                    End If


                    intCurrentTop += ObjFieldE.Height + 90
                    IntNoofControlsPerSection += 1
                    If IntNoofControlsPerSection = 5 Then
                        IntNoofControlsPerSection = 0
                        intCurrentTop = 180
                    End If
                Next
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function      'Waiting for QC.

    Private Function UpdateCriteriaValue(ByRef Report As Report, _
        ByVal ReportsProperties As ClsRpw_ReportsProperties, ByVal DtColumns As Data.DataTable) As Boolean
        Dim ObjDataRow As Data.DataRow
        Dim IntTotalWidth As Integer = Report.Layout.MarginLeft
        Dim IntForeColor As Integer = 0
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim ObjField As Field
        Dim StrText As String = String.Empty
        Dim StrFieldName As String = String.Empty
        Dim IntSection As Integer = 0
        Dim IntNoofControlsPerSection As Integer = 0
        Dim intCurrentTop As Integer = Report.Layout.MarginTop + 105
        Dim IntCurrentLeft As Integer = Report.Layout.MarginLeft + 105
        Dim IntLanguage As Integer = mLangauge
        Dim StringHandler As New Venus.Shared.StringHandler
        Try
            If IntLanguage > 0 Then
                IntCurrentLeft = Report.Layout.Width - Report.Layout.MarginRight - 2145
            End If
            With Report.Fields
                For Each ObjDataRow In DtColumns.Select("Status = true", "Rank")
                    ObjField = New Field
                    ObjField = .GetFieldByName_Language("CriVal" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar), IntLanguage)
                    StrText = ObjDataHandler.DataValue_Out(ObjDataRow("DefaultValue"), Data.SqlDbType.VarChar)
                    If Not ObjField Is Nothing Then
                        If StringHandler.GetValue(ObjField.Tag, "Status").ToUpper <> "FIX" Then
                            With ObjField
                                .Section = 1
                                .Text = StrText
                                .Calculated = -1
                                .Top = intCurrentTop
                                If IntLanguage > 0 Then
                                    .Left = IntCurrentLeft - 2160 - 225
                                Else
                                    .Left = IntCurrentLeft + 2160
                                End If
                                .Width = 2265
                                .Height = 330
                                .Font.Bold = IIf(ReportsProperties.HeaderFontIsBold = False, -1, 1)
                                .Font.Italic = IIf(ReportsProperties.HeaderFontIsItalic = False, -1, 1)
                                .Font.Name = ReportsProperties.HeaderFont
                                .Font.Size = ReportsProperties.HeaderFontSize
                                .ForeColor = DefaultBackColor("", False)
                                .BackColor = DefaultBackColor("", True)
                                .BorderStyle = 1
                                .BorderColor = DefaultBackColor("", False)
                                .Align = MapColumnAligment(ReportsProperties.HeaderValignment, ReportsProperties.HeaderHalignment)
                            End With
                        Else
                            ObjField.Text = StrText
                        End If
                    Else
                        ObjField = New Field
                        With ObjField
                            .Name = "CriVal" & ObjDataHandler.DataValue_Out(ObjDataRow("FieldName"), Data.SqlDbType.VarChar)
                            .Section = 1
                            .Text = StrText
                            .Calculated = -1
                            .Top = intCurrentTop
                            If IntLanguage > 0 Then
                                .Left = IntCurrentLeft - 2160 - 225
                            Else
                                .Left = IntCurrentLeft + 2160
                            End If
                            .Width = 2265
                            .Height = 330
                            .Font.Bold = IIf(ReportsProperties.HeaderFontIsBold = False, -1, 1)
                            .Font.Italic = IIf(ReportsProperties.HeaderFontIsItalic = False, -1, 1)
                            .Font.Name = ReportsProperties.HeaderFont
                            .Font.Size = ReportsProperties.HeaderFontSize
                            .ForeColor = DefaultBackColor("", False)
                            .BackColor = DefaultBackColor("", True)
                            .BorderStyle = 1
                            .BorderColor = DefaultBackColor("", False)
                            .Align = MapColumnAligment(ReportsProperties.HeaderValignment, ReportsProperties.HeaderHalignment)
                        End With
                        .AddField(ObjField)
                    End If
                    intCurrentTop += ObjField.Height + 90
                    IntNoofControlsPerSection += 1
                    If IntNoofControlsPerSection = 5 Then
                        IntNoofControlsPerSection = 0
                        intCurrentTop = 180
                        If IntLanguage > 0 Then
                            IntCurrentLeft -= 2370 - 2160 - 225
                        Else
                            IntCurrentLeft += 2370 + 2160
                        End If
                    End If
                Next
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function        'ToDo Should be canceld 

    '==================(Helper Functions)====================

    Private Function MapFooterSymbols(ByVal Number As String, ByVal Field As String, Optional ByVal Formula As String = "") As String
        Try
            Select Case Number
                Case "0"
                    Return Field
                Case "1"
                    Return "Sum(" & Field & ")"
                Case "2"
                    Return "Avg(" & Field & ")"
                Case "3"
                    Return "Min(" & Field & ")"
                Case "4"
                    Return "Max(" & Field & ")"
                Case "5"
                    Return "Count(" & Field & ")"
                Case "6"
                    Return Formula
                Case Else
                    Return Field
            End Select
        Catch ex As Exception
            Return Field
        End Try
    End Function

    Private Function MapColumnAligment(ByVal VAlignment As String, ByVal HAlignment As String) As String
        Try
            Select Case HAlignment
                Case "1"
                    Select Case VAlignment
                        Case "1"
                            Return "0"
                        Case "2"
                            Return "6"
                        Case "3"
                            Return "3"
                        Case Else
                            Return "0"
                    End Select
                Case "2"
                    Select Case VAlignment
                        Case "1"
                            Return "1"
                        Case "2"
                            Return "7"
                        Case "3"
                            Return "4"
                        Case Else
                            Return "1"
                    End Select
                Case "3"
                    Select Case VAlignment
                        Case "1"
                            Return "2"
                        Case "2"
                            Return "8"
                        Case "3"
                            Return "5"
                        Case Else
                            Return "2"
                    End Select
                Case Else
                    Select Case VAlignment
                        Case "1"
                            Return "0"
                        Case "2"
                            Return "6"
                        Case "3"
                            Return "3"
                        Case Else
                    End Select
            End Select
            Return "0"
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    Private Function CreateFieldsBorders(ByRef Report As Report, ByVal FieldName As String, ByVal ObjDataRow As Data.DataRow) As Boolean
        Dim ObjFiled As Field
        Dim ObjLine As New Field
        Dim ObjDataHandler As New Venus.Shared.DataHandler

        ObjFiled = Report.Fields.GetFieldByName(FieldName)
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Top"
        ObjLine.Width = ObjFiled.Width + 5
        ObjLine.Top = ObjFiled.Top
        ObjLine.Left = ObjFiled.Left
        ObjLine.Height = 0
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("TopBorderWidth"), Data.SqlDbType.Int)

        ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("TopBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
        DefaultBackColor("", True), DefaultBackColor(ObjDataRow("TopBorderColor"), True))

        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("TopBorderStyle"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
        ObjLine = New Field
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Bottom"
        ObjLine.Width = ObjFiled.Width + 5
        ObjLine.Top = ObjFiled.Top + ObjFiled.Height
        ObjLine.Left = ObjFiled.Left
        ObjLine.Height = 0
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("BottomBorderWidth"), Data.SqlDbType.Int)

        ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("BottomBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
        DefaultBackColor("", True), DefaultBackColor(ObjDataRow("BottomBorderColor"), True))

        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("BottomBorderStyle"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
        ObjLine = New Field
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Left"
        ObjLine.Width = 0
        ObjLine.Top = ObjFiled.Top
        ObjLine.Left = ObjFiled.Left
        ObjLine.Height = ObjFiled.Height
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("LeftBorderWidth"), Data.SqlDbType.Int)

        ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("LeftBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
        DefaultBackColor("", True), DefaultBackColor(ObjDataRow("LeftBorderColor"), True))

        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("LeftBorderStyle"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
        ObjLine = New Field
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Right"
        ObjLine.Width = 0
        ObjLine.Top = ObjFiled.Top
        ObjLine.Left = ObjFiled.Left + ObjFiled.Width
        ObjLine.Height = ObjFiled.Height
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("RightBorderWidth"), Data.SqlDbType.Int)

        ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("RightBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
        DefaultBackColor("", True), DefaultBackColor(ObjDataRow("RightBorderColor"), True))

        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("RightBorderStyle"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
    End Function

    Private Function UpdateFieldsBorders(ByRef Report As Report, ByVal FieldName As String, ByVal ObjDataRow As Data.DataRow) As Boolean
        Dim ObjFiled As Field
        Dim ObjLine As New Field
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim StringHandler As New Venus.Shared.StringHandler
        ObjFiled = Report.Fields.GetFieldByName(FieldName)

        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Top")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = ObjFiled.Width + 5
                ObjLine.Top = ObjFiled.Top
                ObjLine.Left = ObjFiled.Left
                ObjLine.Height = 0
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("TopBorderWidth"), Data.SqlDbType.Int)

                ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("TopBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
                DefaultBackColor("", True), DefaultBackColor(ObjDataRow("TopBorderColor"), True))

                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("TopBorderStyle"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Top"
            ObjLine.Width = ObjFiled.Width + 5
            ObjLine.Top = ObjFiled.Top
            ObjLine.Left = ObjFiled.Left
            ObjLine.Height = 0
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("TopBorderWidth"), Data.SqlDbType.Int)

            ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("TopBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
            DefaultBackColor("", True), DefaultBackColor(ObjDataRow("TopBorderColor"), True))

            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("TopBorderStyle"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If

        ObjLine = New Field
        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Bottom")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = ObjFiled.Width + 5
                ObjLine.Top = ObjFiled.Top + ObjFiled.Height
                ObjLine.Left = ObjFiled.Left
                ObjLine.Height = 0
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("BottomBorderWidth"), Data.SqlDbType.Int)

                ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("BottomBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
                DefaultBackColor("", True), DefaultBackColor(ObjDataRow("BottomBorderColor"), True))

                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("BottomBorderStyle"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Bottom"
            ObjLine.Width = ObjFiled.Width + 5
            ObjLine.Top = ObjFiled.Top + ObjFiled.Height
            ObjLine.Left = ObjFiled.Left
            ObjLine.Height = 0
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("BottomBorderWidth"), Data.SqlDbType.Int)

            ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("BottomBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
            DefaultBackColor("", True), DefaultBackColor(ObjDataRow("BottomBorderColor"), True))

            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("BottomBorderStyle"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If

        ObjLine = New Field
        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Left")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = 0
                ObjLine.Top = ObjFiled.Top
                ObjLine.Left = ObjFiled.Left
                ObjLine.Height = ObjFiled.Height
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("LeftBorderWidth"), Data.SqlDbType.Int)

                ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("LeftBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
                DefaultBackColor("", True), DefaultBackColor(ObjDataRow("LeftBorderColor"), True))

                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("LeftBorderStyle"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Left"
            ObjLine.Width = 0
            ObjLine.Top = ObjFiled.Top
            ObjLine.Left = ObjFiled.Left
            ObjLine.Height = ObjFiled.Height
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("LeftBorderWidth"), Data.SqlDbType.Int)

            ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("LeftBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
            DefaultBackColor("", True), DefaultBackColor(ObjDataRow("LeftBorderColor"), True))

            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("LeftBorderStyle"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If

        ObjLine = New Field
        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Right")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = 0
                ObjLine.Top = ObjFiled.Top
                ObjLine.Left = ObjFiled.Left + ObjFiled.Width
                ObjLine.Height = ObjFiled.Height
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("RightBorderWidth"), Data.SqlDbType.Int)

                ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("RightBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
                DefaultBackColor("", True), DefaultBackColor(ObjDataRow("RightBorderColor"), True))

                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("RightBorderStyle"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Right"
            ObjLine.Width = 0
            ObjLine.Top = ObjFiled.Top
            ObjLine.Left = ObjFiled.Left + ObjFiled.Width
            ObjLine.Height = ObjFiled.Height
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("RightBorderWidth"), Data.SqlDbType.Int)

            ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("RightBorderStyle"), Data.SqlDbType.Int) - 3 < 0, _
            DefaultBackColor("", True), DefaultBackColor(ObjDataRow("RightBorderColor"), True))

            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("RightBorderStyle"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If
    End Function

    Private Function CreateFieldsHeadBorders(ByRef Report As Report, ByVal FieldName As String, ByVal ObjDataRow As Data.DataRow) As Boolean
        Dim ObjFiled As Field
        Dim ObjLine As New Field
        Dim ObjDataHandler As New Venus.Shared.DataHandler

        ObjFiled = Report.Fields.GetFieldByName(FieldName)
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Top"
        ObjLine.Width = ObjFiled.Width + 5
        ObjLine.Top = ObjFiled.Top
        ObjLine.Left = ObjFiled.Left
        ObjLine.Height = 0
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthTop"), Data.SqlDbType.Int)

        ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleTop"), Data.SqlDbType.Int) - 3 < 0, _
        DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorTop"), True))

        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleTop"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
        ObjLine = New Field
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Bottom"
        ObjLine.Width = ObjFiled.Width + 5
        ObjLine.Top = ObjFiled.Top + ObjFiled.Height
        ObjLine.Left = ObjFiled.Left
        ObjLine.Height = 0
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthBottom"), Data.SqlDbType.Int)

        ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleBottom"), Data.SqlDbType.Int) - 3 < 0, _
        DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorBottom"), True))

        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleBottom"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
        ObjLine = New Field
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Left"
        ObjLine.Width = 0
        ObjLine.Top = ObjFiled.Top
        ObjLine.Left = ObjFiled.Left
        ObjLine.Height = ObjFiled.Height
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthLeft"), Data.SqlDbType.Int)

        ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColheaderBorderStyleLeft"), Data.SqlDbType.Int) - 3 < 0, _
        DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorLeft"), True))

        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColheaderBorderStyleLeft"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
        ObjLine = New Field
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Right"
        ObjLine.Width = 0
        ObjLine.Top = ObjFiled.Top
        ObjLine.Left = ObjFiled.Left + ObjFiled.Width
        ObjLine.Height = ObjFiled.Height
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthRight"), Data.SqlDbType.Int)

        ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleRight"), Data.SqlDbType.Int) - 3 < 0, _
        DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorRight"), True))

        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleRight"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
    End Function

    Private Function UpdateFieldsHeadBorders(ByRef Report As Report, ByVal FieldName As String, ByVal ObjDataRow As Data.DataRow) As Boolean
        Dim ObjFiled As Field
        Dim ObjLine As New Field
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim StringHandler As New Venus.Shared.StringHandler
        ObjFiled = Report.Fields.GetFieldByName(FieldName)

        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Top")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = ObjFiled.Width + 5
                ObjLine.Top = ObjFiled.Top
                ObjLine.Left = ObjFiled.Left
                ObjLine.Height = 0
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthTop"), Data.SqlDbType.Int)

                ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleTop"), Data.SqlDbType.Int) - 3 < 0, _
                DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorTop"), True))

                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleTop"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Top"
            ObjLine.Width = ObjFiled.Width + 5
            ObjLine.Top = ObjFiled.Top
            ObjLine.Left = ObjFiled.Left
            ObjLine.Height = 0
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthTop"), Data.SqlDbType.Int)

            ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleTop"), Data.SqlDbType.Int) - 3 < 0, _
            DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorTop"), True))

            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleTop"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If

        ObjLine = New Field
        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Bottom")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = ObjFiled.Width + 5
                ObjLine.Top = ObjFiled.Top + ObjFiled.Height
                ObjLine.Left = ObjFiled.Left
                ObjLine.Height = 0
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthBottom"), Data.SqlDbType.Int)

                ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleBottom"), Data.SqlDbType.Int) - 3 < 0, _
                DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorBottom"), True))

                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleBottom"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Bottom"
            ObjLine.Width = ObjFiled.Width + 5
            ObjLine.Top = ObjFiled.Top + ObjFiled.Height
            ObjLine.Left = ObjFiled.Left
            ObjLine.Height = 0
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthBottom"), Data.SqlDbType.Int)

            ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleBottom"), Data.SqlDbType.Int) - 3 < 0, _
            DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorBottom"), True))

            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleBottom"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If

        ObjLine = New Field
        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Left")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = 0
                ObjLine.Top = ObjFiled.Top
                ObjLine.Left = ObjFiled.Left
                ObjLine.Height = ObjFiled.Height
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthLeft"), Data.SqlDbType.Int)

                ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColheaderBorderStyleLeft"), Data.SqlDbType.Int) - 3 < 0, _
                DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorLeft"), True))

                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColheaderBorderStyleLeft"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Left"
            ObjLine.Width = 0
            ObjLine.Top = ObjFiled.Top
            ObjLine.Left = ObjFiled.Left
            ObjLine.Height = ObjFiled.Height
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthLeft"), Data.SqlDbType.Int)

            ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColheaderBorderStyleLeft"), Data.SqlDbType.Int) - 3 < 0, _
            DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorLeft"), True))

            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColheaderBorderStyleLeft"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If

        ObjLine = New Field
        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Right")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = 0
                ObjLine.Top = ObjFiled.Top
                ObjLine.Left = ObjFiled.Left + ObjFiled.Width
                ObjLine.Height = ObjFiled.Height
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthRight"), Data.SqlDbType.Int)

                ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleRight"), Data.SqlDbType.Int) - 3 < 0, _
                DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorRight"), True))

                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleRight"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Right"
            ObjLine.Width = 0
            ObjLine.Top = ObjFiled.Top
            ObjLine.Left = ObjFiled.Left + ObjFiled.Width
            ObjLine.Height = ObjFiled.Height
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderWidthRight"), Data.SqlDbType.Int)

            ObjLine.BorderColor = IIf(ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleRight"), Data.SqlDbType.Int) - 3 < 0, _
            DefaultBackColor("", True), DefaultBackColor(ObjDataRow("ColHeaderBorderColorRight"), True))

            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColHeaderBorderStyleRight"), Data.SqlDbType.Int) - 3
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If
    End Function

    Private Function CreateFieldsFooterBorders(ByRef Report As Report, ByVal FieldName As String, ByVal ObjDataRow As Data.DataRow) As Boolean
        Dim ObjFiled As Field
        Dim ObjLine As New Field
        Dim ObjDataHandler As New Venus.Shared.DataHandler

        ObjFiled = Report.Fields.GetFieldByName(FieldName)
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Top"
        ObjLine.Width = ObjFiled.Width + 5
        ObjLine.Top = ObjFiled.Top
        ObjLine.Left = ObjFiled.Left
        ObjLine.Height = 0
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthTop"), Data.SqlDbType.Int)
        ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorTop"), True)
        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleTop"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
        ObjLine = New Field
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Bottom"
        ObjLine.Width = ObjFiled.Width + 5
        ObjLine.Top = ObjFiled.Top + ObjFiled.Height
        ObjLine.Left = ObjFiled.Left
        ObjLine.Height = 0
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthBottom"), Data.SqlDbType.Int)
        ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorBottom"), True)
        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleBottom"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
        ObjLine = New Field
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Left"
        ObjLine.Width = 0
        ObjLine.Top = ObjFiled.Top
        ObjLine.Left = ObjFiled.Left
        ObjLine.Height = ObjFiled.Height
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthLeft"), Data.SqlDbType.Int)
        ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorLeft"), True)
        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleLeft"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
        ObjLine = New Field
        ObjLine.Name = "Line_" & ObjFiled.Name & "_Right"
        ObjLine.Width = 0
        ObjLine.Top = ObjFiled.Top
        ObjLine.Left = ObjFiled.Left + ObjFiled.Width
        ObjLine.Height = ObjFiled.Height
        ObjLine.LineSlant = 1
        ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthRight"), Data.SqlDbType.Int)
        ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorRight"), True)
        ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleRight"), Data.SqlDbType.Int) - 3
        ObjLine.Section = ObjFiled.Section
        ObjLine.ZOrder = 1
        Report.Fields.AddField(ObjLine)
    End Function

    Private Function UpdateFieldsFooterBorders(ByRef Report As Report, ByVal FieldName As String, ByVal ObjDataRow As Data.DataRow) As Boolean
        Dim ObjFiled As Field
        Dim ObjLine As New Field
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim StringHandler As New Venus.Shared.StringHandler
        ObjFiled = Report.Fields.GetFieldByName(FieldName)

        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Top")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = ObjFiled.Width + 5
                ObjLine.Top = ObjFiled.Top
                ObjLine.Left = ObjFiled.Left
                ObjLine.Height = 0
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthTop"), Data.SqlDbType.Int)
                ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorTop"), True)
                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleTop"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Top"
            ObjLine.Width = ObjFiled.Width + 5
            ObjLine.Top = ObjFiled.Top
            ObjLine.Left = ObjFiled.Left
            ObjLine.Height = 0
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthTop"), Data.SqlDbType.Int)
            ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorTop"), True)
            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleTop"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If

        ObjLine = New Field
        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Bottom")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = ObjFiled.Width + 5
                ObjLine.Top = ObjFiled.Top + ObjFiled.Height
                ObjLine.Left = ObjFiled.Left
                ObjLine.Height = 0
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthBottom"), Data.SqlDbType.Int)
                ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorBottom"), True)
                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleBottom"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Bottom"
            ObjLine.Width = ObjFiled.Width + 5
            ObjLine.Top = ObjFiled.Top + ObjFiled.Height
            ObjLine.Left = ObjFiled.Left
            ObjLine.Height = 0
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthBottom"), Data.SqlDbType.Int)
            ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorBottom"), True)
            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleBottom"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If

        ObjLine = New Field
        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Left")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = 0
                ObjLine.Top = ObjFiled.Top
                ObjLine.Left = ObjFiled.Left
                ObjLine.Height = ObjFiled.Height
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthLeft"), Data.SqlDbType.Int)
                ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorLeft"), True)
                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleLeft"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Left"
            ObjLine.Width = 0
            ObjLine.Top = ObjFiled.Top
            ObjLine.Left = ObjFiled.Left
            ObjLine.Height = ObjFiled.Height
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthLeft"), Data.SqlDbType.Int)
            ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorLeft"), True)
            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleLeft"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If

        ObjLine = New Field
        ObjLine = Report.Fields.GetFieldByName("Line_" & ObjFiled.Name & "_Right")
        If Not ObjLine Is Nothing Then
            If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                ObjLine.Width = 0
                ObjLine.Top = ObjFiled.Top
                ObjLine.Left = ObjFiled.Left + ObjFiled.Width
                ObjLine.Height = ObjFiled.Height
                ObjLine.LineSlant = 1
                ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthRight"), Data.SqlDbType.Int)
                ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorRight"), True)
                ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleRight"), Data.SqlDbType.Int) - 3
                ObjLine.Section = ObjFiled.Section
                ObjLine.ZOrder = 1
            End If
        Else
            ObjLine = New Field
            ObjLine.Name = "Line_" & ObjFiled.Name & "_Right"
            ObjLine.Width = 0
            ObjLine.Top = ObjFiled.Top
            ObjLine.Left = ObjFiled.Left + ObjFiled.Width
            ObjLine.Height = ObjFiled.Height
            ObjLine.LineSlant = 1
            ObjLine.LineWidth = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderWidthRight"), Data.SqlDbType.Int)
            ObjLine.BorderColor = DefaultBackColor(ObjDataRow("ColFooterBorderColorRight"), True)
            ObjLine.BorderStyle = ObjDataHandler.DataValue_Out(ObjDataRow("ColFooterBorderStyleRight"), Data.SqlDbType.Int) - 3
            ObjLine.Section = ObjFiled.Section
            ObjLine.ZOrder = 1
            Report.Fields.AddField(ObjLine)
        End If
    End Function

    Private Function CreateBoxHead(ByRef Report As Report) As Boolean
        Dim ObjLine As New Field
        Try
            ObjLine.Name = "Line_Box_Top_Head"
            ObjLine.Width = Report.Layout.Width - (Report.Layout.MarginLeft + Report.Layout.MarginLeft)
            ObjLine.Top = Report.Layout.MarginTop
            ObjLine.Left = Report.Layout.MarginLeft
            ObjLine.Height = 615
            ObjLine.LineWidth = 0
            ObjLine.BorderColor = DefaultBackColor("", False)
            ObjLine.BorderStyle = 1
            ObjLine.Section = 1
            Report.Fields.AddField(ObjLine)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CreateBoxTitle(ByRef Report As Report) As Boolean
        Dim ObjLine As New Field
        Try
            ObjLine.Name = "Line_Box_Top_Title"
            ObjLine.Width = Report.Layout.Width - (Report.Layout.MarginLeft + Report.Layout.MarginLeft)
            ObjLine.Top = Report.Layout.MarginTop + 735
            ObjLine.Left = Report.Layout.MarginLeft
            ObjLine.Height = 615
            ObjLine.LineWidth = 0
            ObjLine.BorderColor = DefaultBackColor("", False)
            ObjLine.BorderStyle = 1
            ObjLine.Section = 1
            ObjLine.BackColor = 16119285
            ObjLine.BackStyle = 1
            Report.Fields.AddField(ObjLine)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CreateBoxObject(ByRef Report As Report) As Boolean
        Dim ObjLine As New Field
        Try
            ObjLine.Name = "Line_Box_Top"
            ObjLine.Width = Report.Layout.Width - (Report.Layout.MarginLeft + Report.Layout.MarginLeft)
            ObjLine.Top = Report.Layout.MarginTop + 1470
            ObjLine.Left = Report.Layout.MarginLeft
            ObjLine.Height = 2205
            ObjLine.LineWidth = 0
            ObjLine.BorderColor = DefaultBackColor("", False)
            ObjLine.BorderStyle = 1
            ObjLine.Section = 1
            Report.Fields.AddField(ObjLine)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CreateDateTimeFields(ByRef Report As Report, _
    ByVal ReportsProperties As ClsRpw_ReportsProperties)
        Dim IntCurrentLeftEnglish As Integer = Report.Layout.MarginLeft + 105
        Dim IntCurrentRightArabic As Integer = Report.Layout.Width - Report.Layout.MarginRight - 2145
        Dim ObjLine As New Field
        Dim ObjField As New Field
        With ObjField
            .Name = "HdrDateCapE"
            .Section = 1
            .Text = "Print Date"
            .Calculated = 0
            .Top = 3810 + Report.Layout.MarginTop
            .Left = IntCurrentLeftEnglish
            .Width = 2040
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 8
            .ForeColor = DefaultBackColor("", False)
            .BackColor = 16119285 'This Represent the white smoke color
            .BackStyle = 1
            .BorderStyle = 1
            .BorderColor = DefaultBackColor("", False)
            .Align = 6
            .Tag = "Language=E;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrDateCapA"
            .Section = 1
            .Text = "  «—ÌŒ «·ÿ»«⁄… "
            .Calculated = 0
            .Top = 3810 + Report.Layout.MarginTop
            .Left = IntCurrentRightArabic
            .Width = 2040
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 8
            .ForeColor = DefaultBackColor("", False)
            .BackColor = 16119285 'This Represent the white smoke color
            .BackStyle = 1
            .BorderStyle = 1
            .BorderColor = DefaultBackColor("", False)
            .Align = 8
            .Tag = "Language=A;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrTimeCapE"
            .Section = 1
            .Text = "Print Time"
            .Calculated = 0
            .Top = 4230 + Report.Layout.MarginTop
            .Left = IntCurrentLeftEnglish
            .Width = 2040
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 8
            .ForeColor = DefaultBackColor("", False)
            .BackColor = 16119285 'This Represent the white smoke color
            .BackStyle = 1
            .BorderStyle = 1
            .BorderColor = DefaultBackColor("", False)
            .Align = 6
            .Tag = "Language=E;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrTimeCapA"
            .Section = 1
            .Text = " Êﬁ  «·ÿ»«⁄… "
            .Calculated = 0
            .Top = 4230 + Report.Layout.MarginTop
            .Left = IntCurrentRightArabic
            .Width = 2040
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 8
            .ForeColor = DefaultBackColor("", False)
            .BackColor = 16119285 'This Represent the white smoke color
            .BackStyle = 1
            .BorderStyle = 1
            .BorderColor = DefaultBackColor("", False)
            .Align = 8
            .Tag = "Language=A;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrPageofE"
            .Section = 1
            .Text = """Page "" & [Page] & "" of "" & [Pages]"
            .Calculated = 1
            .Top = 4725 + Report.Layout.MarginTop
            .Left = IntCurrentLeftEnglish
            .Width = 2040
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 8
            .ForeColor = DefaultBackColor("", False)
            .BackColor = DefaultBackColor("", False)
            .BorderStyle = 0
            .BorderColor = DefaultBackColor("", False)
            .Align = 6
            .Tag = "Language=E;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrPageofA"
            .Section = 1
            .Text = """Page "" & [Page] & "" of "" & [Pages]"
            .Calculated = 1
            .Top = 4725 + Report.Layout.MarginTop
            .Left = IntCurrentRightArabic
            .Width = 2040
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 8
            .ForeColor = DefaultBackColor("", False)
            .BackColor = DefaultBackColor("", False)
            .BorderStyle = 0
            .BorderColor = DefaultBackColor("", False)
            .Align = 8
            .Tag = "Language=A;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrDateValueE"
            .Section = 1
            .Text = "Format(Now, ""Medium Date"")"
            .Calculated = 1
            .Top = 3870 + Report.Layout.MarginTop
            .Left = IntCurrentLeftEnglish + 2160
            .Width = 2040
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 8
            .ForeColor = DefaultBackColor("", False)
            .BackColor = DefaultBackColor("", False)
            .BorderStyle = 0
            .BorderColor = DefaultBackColor("", False)
            .Align = 6
            .Tag = "Language=E;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrDateValueA"
            .Section = 1
            .Text = "Format(Now, ""Medium Date"")"
            .Calculated = 1
            .Top = 3870 + Report.Layout.MarginTop
            .Left = IntCurrentRightArabic - 2160
            .Width = 2040
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 8
            .ForeColor = DefaultBackColor("", False)
            .BackColor = DefaultBackColor("", False)
            .BorderStyle = 0
            .BorderColor = DefaultBackColor("", False)
            .Align = 8
            .Tag = "Language=A;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrTimeValueE"
            .Section = 1
            .Text = "Format(Now, ""Medium Time"")"
            .Calculated = 1
            .Top = 4305 + Report.Layout.MarginTop
            .Left = IntCurrentLeftEnglish + 2160
            .Width = 2040
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 8
            .ForeColor = DefaultBackColor("", False)
            .BackColor = DefaultBackColor("", False)
            .BorderStyle = 0
            .BorderColor = DefaultBackColor("", False)
            .Align = 6
            .Tag = "Language=E;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrTimeValueA"
            .Section = 1
            .Text = "Format(Now, ""Medium Time"")"
            .Calculated = 1
            .Top = 4305 + Report.Layout.MarginTop
            .Left = IntCurrentRightArabic - 2160
            .Width = 2040
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 8
            .ForeColor = DefaultBackColor("", False)
            .BackColor = DefaultBackColor("", False)
            .BorderStyle = 0
            .BorderColor = DefaultBackColor("", False)
            .Align = 8
            .Tag = "Language=A;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrTitleValueE"
            .Section = 1
            .Text = ReportsProperties.EngTitle
            .Calculated = 1
            .Top = 900 + Report.Layout.MarginTop
            .Left = 3660
            .Width = 3630
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 12
            .ForeColor = DefaultBackColor("", False)
            .BackColor = DefaultBackColor("", False)
            .BorderStyle = 0
            .BorderColor = DefaultBackColor("", False)
            .Align = 1
            .Tag = "Language=E;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjField = New Field
        With ObjField
            .Name = "HdrTitleValueA"
            .Section = 1
            .Text = ReportsProperties.ArbTitle
            .Calculated = 1
            .Top = 900 + Report.Layout.MarginTop
            .Left = 3660
            .Width = 3630
            .Height = 330
            .Font.Bold = 1
            .Font.Italic = 1
            .Font.Name = ReportsProperties.HeaderFont
            .Font.Size = 12
            .ForeColor = DefaultBackColor("", False)
            .BackColor = DefaultBackColor("", False)
            .BorderStyle = 0
            .BorderColor = DefaultBackColor("", False)
            .Align = 1
            .Tag = "Language=A;"
            .Visible = 1
        End With
        Report.Fields.AddField(ObjField)
        ObjLine = New Field
        With ObjLine
            .Name = "Line_Head_Page_separator"
            .Width = 0
            .Top = 3900
            .Left = 165
            .Height = 0
            .LineSlant = 1
            .LineWidth = 0
            .BorderColor = DefaultBackColor("", False)
            .BorderStyle = 1
            .Section = 1
            .ZOrder = 1
        End With
        Report.Fields.AddField(ObjLine)
    End Function

    Private Function UpdateBoxObject(ByRef Report As Report) As Boolean
        Dim ObjLine As New Field
        Dim StringHandler As New Venus.Shared.StringHandler
        ObjLine = Report.Fields.GetFieldByName("Line_Box_Top")
        Try
            If Not ObjLine Is Nothing Then
                With ObjLine
                    If StringHandler.GetValue(ObjLine.Tag, "Status").ToUpper <> "FIX" Then
                        .Width = Report.Layout.Width - (Report.Layout.MarginLeft + Report.Layout.MarginLeft)
                        .Top = Report.Layout.MarginTop
                        .Left = Report.Layout.MarginLeft
                        .Height = 2205
                        .LineWidth = 0
                        .ForeColor = DefaultBackColor("", False)
                        .BorderStyle = 1
                        .Section = 1
                    End If
                End With
            Else
                ObjLine = New Field
                With ObjLine
                    .Name = "Line_Box_Top"
                    .Width = Report.Layout.Width - (Report.Layout.MarginLeft + Report.Layout.MarginLeft)
                    .Top = Report.Layout.MarginTop
                    .Left = Report.Layout.MarginLeft
                    .Height = 2205
                    .LineWidth = 0
                    .ForeColor = DefaultBackColor("", False)
                    .BorderStyle = 1
                    .Section = 1
                End With
                Report.Fields.AddField(ObjLine)
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DefaultBackColor(ByVal Color As String, ByVal IsBackColor As Boolean) As Double
        Dim ObjColorConvertor As New Drawing.ColorConverter
        Try
            If IsBackColor Then
                If Color = "0" Or Color = "" Then
                    Return Drawing.ColorTranslator.ToOle(ObjColorConvertor.ConvertFromString("White"))
                Else
                    Return Drawing.ColorTranslator.ToOle(ObjColorConvertor.ConvertFromString(Color))
                End If
            Else
                If Color = "0" Or Color = "" Then
                    Return Drawing.ColorTranslator.ToOle(ObjColorConvertor.ConvertFromString("Black"))
                Else
                    Return Drawing.ColorTranslator.ToOle(ObjColorConvertor.ConvertFromString(Color))
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Private Function GetStringPart(ByVal InputString As String, ByVal Sector As Integer, Optional ByVal Identifier As Char = "/") As String
        Dim InputArry() As String
        Try
            InputArry = InputString.Split(Identifier)
            If Sector > -1 And Sector <= InputArry.GetUpperBound(0) And InputArry.GetUpperBound(0) <> 0 Then
                Return InputArry(Sector)
            Else
                Return "Sector is Not Exist"
            End If
        Catch ex As Exception
            Return "Sector is Not Exist"
        End Try
    End Function
    Private Function SetVisibilityAccordingToDesign(ByRef report As Report) As Boolean
        Dim ObjField As New Field
        Dim StringHandler As New Venus.Shared.StringHandler
        Dim IntLanguage As Integer = mLangauge
        Dim IntCounter As Integer
        Try
            For IntCounter = 0 To report.Fields.Count - 1
                ObjField = New Field
                ObjField = report.Fields.Field(IntCounter)
                If StringHandler.GetValue(ObjField.Tag, "Language").ToUpper = "E" Then
                    If IntLanguage = 1 Then
                        ObjField.Visible = 0
                    ElseIf IntLanguage = 0 Then
                        ObjField.Visible = 1
                    End If
                ElseIf StringHandler.GetValue(ObjField.Tag, "Language").ToUpper = "A" Then
                    If IntLanguage = 0 Then
                        ObjField.Visible = 0
                    ElseIf IntLanguage = 1 Then
                        ObjField.Visible = 1
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Function
    Private Function SetLanguagePosition(ByRef report As Report, ByVal language As Lang) As Boolean
        Dim ObjField As New Field
        Dim StringHandler As New Venus.Shared.StringHandler
        Dim IntLanguage As Integer = mLangauge
        Dim IntCounter As Integer
        Dim StrArabicCaption As String = String.Empty
        Dim ObjSringHandler As New Venus.Shared.StringHandler
        Try
            For IntCounter = 0 To report.Fields.Count - 1
                ObjField = New Field
                ObjField = report.Fields.Field(IntCounter)
                If language = Lang.Arabic Then
                    ObjField.Left = report.Layout.Width - report.Layout.MarginRight - ObjField.Left - ObjField.Width

                    Select Case ObjSringHandler.GetValue(ObjField.Tag, "ArabicAlignment").Replace(" ", "")
                        Case "0"
                            ObjField.Align = 6
                        Case "1"
                            ObjField.Align = 6
                        Case "2"
                            ObjField.Align = 7
                        Case "3"
                            ObjField.Align = 8
                        Case Else
                            ObjField.Align = 8
                    End Select

                    StrArabicCaption = ObjSringHandler.GetValue(ObjField.Tag, "ArbCaption")
                    If Len(StrArabicCaption.Replace(" ", "")) > 0 Then
                        ObjField.Text = StrArabicCaption
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Function
    Private Function CreateParameters(ByVal DTCriteria As Data.DataTable) As String
        Dim ObjRow As Data.DataRow
        Dim ObjDataHandler As New Venus.Shared.DataHandler
        Dim StrParameters As String = String.Empty
        Dim StrDataType As String = String.Empty
        Try
            For Each ObjRow In DTCriteria.Rows
                StrDataType = ObjDataHandler.DataValue_Out(ObjRow("DataType"), Data.SqlDbType.VarChar)
                Select Case StrDataType
                    Case "String", "VarChar" ' VarChar 
                        If ObjDataHandler.DataValue_Out(ObjRow("FieldName"), Data.SqlDbType.VarChar) = "P_UserCode" Then
                            StrParameters &= ",'" & ObjDataHandler.DataValue_Out(Me.UserCode, Data.SqlDbType.VarChar) & "'"
                        ElseIf ObjDataHandler.DataValue_Out(ObjRow("FieldName"), Data.SqlDbType.VarChar) = "P_ReportName" Then
                            StrParameters &= ",'" & ObjDataHandler.DataValue_Out(Me.ReportName, Data.SqlDbType.VarChar) & "'"
                        Else
                            StrParameters &= ",'" & ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar) & "'"
                        End If



                    Case "Mask"
                        Dim retvalue As String = ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar)
                        retvalue = retvalue.Substring(0, 2) & "/" & retvalue.Substring(2, 2) & "/" & retvalue.Substring(4, 4)
                        StrParameters &= ",'" & retvalue & "'"
                    Case "Int32", "Integer", "Int16", "Decimal", "Double", "Single", "Numeric" ' Numeric Values (TinyInt-Int-SmallInt-Real-Money)

                        If ObjDataHandler.DataValue_Out(ObjRow("FieldName"), Data.SqlDbType.VarChar) = "CompanyID" Then
                            StrParameters &= ",'" & ObjDataHandler.DataValue_Out(Me.CompanyID, Data.SqlDbType.VarChar) & "'"
                        ElseIf ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar) = "" Then
                            StrParameters &= ",Null"
                        Else
                            StrParameters &= "," & ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar)
                        End If

                        'If ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar) = "" Then
                        '    StrParameters &= ",Null"
                        'Else
                        'StrParameters &= "," & ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar)
                        'End If



                    Case "Gdate", "Adate", "Date", "DateTime" ' DateTime  
                        If ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar) = "" Then
                            StrParameters &= ",Null"
                        Else
                            If ClsDataAcessLayer.IsHijri(ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar)) Then
                                StrParameters &= ",'" & Format(CDate(ClsDataAcessLayer.HijriToGreg(ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar), "dd/MM/yyyy")), "MM/dd/yyyy") & "'"
                            Else
                                StrParameters &= ",'" & Format(CDate(ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar)), "MM/dd/yyyy") & "'"
                            End If
                        End If
                    Case "Boolean", "Bit" 'Bit -Boolean Values 
                        StrParameters &= "," & ObjDataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar)
                End Select
            Next
            Return "(" & StrParameters.Substring(1) & ")"
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function CreateSentParameters(ByVal DTCriteria As Data.DataTable, ByRef report As Report) As String
        Dim ObjField As New Field
        Dim IntCounter As Integer
        Dim ObjRow As Data.DataRow
        Try
            For IntCounter = 0 To report.Fields.Count - 1
                ObjField = New Field
                ObjField = report.Fields.Field(IntCounter)
                If ObjField.Text = "P_UserCode" Then
                    ObjField.Text = mStrUserCode
                ElseIf ObjField.Text = "P_UserName" Then
                    ObjField.Text = mStrUserName
                ElseIf ObjField.Text = "P_CompanyName" Then
                    ObjField.Text = mStrCompanyName
                ElseIf ObjField.Text = "P_CompanyHeader_1" Then
                    ObjField.Text = mStrCompanyHeader_1
                ElseIf ObjField.Text = "P_CompanyHeader_2" Then
                    ObjField.Text = mStrCompanyHeader_2
                ElseIf ObjField.Text = "P_CompanyHeader_3" Then
                    ObjField.Text = mStrCompanyHeader_3
                Else
                    For Each ObjRow In DTCriteria.Rows
                        If ObjField.Text = "CriVal" & Venus.Shared.DataHandler.DataValue_Out(ObjRow("FieldName"), Data.SqlDbType.VarChar) Then
                            ObjField.Text = Venus.Shared.DataHandler.DataValue_Out(ObjRow("DefaultValue"), Data.SqlDbType.VarChar)
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
        End Try
    End Function

    Public Sub ClearAllBorders(ByRef Report As Report)
        Dim StringHandler As New Venus.Shared.StringHandler
        Dim intIndex As Integer = 0
        Dim arrIndex() As Integer
        Dim IntCounter As Integer = 0
        For intIndex = 0 To Report.Fields.Count - 1
            If Report.Fields.Field(intIndex).Name.Length > 5 AndAlso Report.Fields.Field(intIndex).Name.Substring(0, 5).ToUpper = "LINE_" Then
                If StringHandler.GetValue(Report.Fields.Field(intIndex).Tag, "Status").ToUpper <> "FIX" Then
                    ReDim Preserve arrIndex(IntCounter)
                    arrIndex(IntCounter) = intIndex
                    IntCounter += 1
                End If
            End If
        Next

        For intIndex = 0 To IntCounter - 1
            Report.Fields.RemoveField(arrIndex(intIndex))
        Next
    End Sub

End Class

Public Class ReportHelper
    Public Function AssignReport(ByVal Report As Report, ByVal Dr As DataRow) As Report

    End Function

    Public Function AssignLayout(ByVal layout As Layout, ByVal Dr As DataRow) As Layout

    End Function

    Public Function AssignDataSource(ByVal DataSource As DataSource, ByVal Dr As DataRow) As DataSource

    End Function

    Public Function AssignSection(ByVal Section As Section, ByVal Dr As DataRow) As Section

    End Function

    Public Function AssignGroup(ByVal Group As Group, ByVal Dr As DataRow) As Group

    End Function

    Public Function AssignField(ByVal Field As Field, ByVal Dr As DataRow) As Field

    End Function

    Public Function AssignFont(ByVal Font As Font, ByVal Dr As DataRow) As Font

    End Function

    Public Function AssignPicture(ByVal Picture As Picture, ByVal Dr As DataRow) As Picture

    End Function
End Class

<Xml.Serialization.XmlRoot(elementName:="Reports")> _
Public Class Reports

    Private mObjReport() As Report
    Private mIntNoofReports As Integer = 0

    <Xml.Serialization.XmlElement(elementName:="Report")> _
    <ComponentModel.Category("Reports")> _
    Public Property Report() As Report()
        Get
            Return mObjReport
        End Get
        Set(ByVal value As Report())
            mObjReport = value
        End Set
    End Property

    Public Function GetReportByName(ByVal Name As String) As Report
        Dim ObjReport As New Report
        Try
            For Each ObjReport In mObjReport
                If Len(Name) > 0 AndAlso Name.ToUpper = ObjReport.Name.ToUpper Then
                    Return ObjReport
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetReportByIndex(ByVal Index As Integer) As Report
        Try
            Return mObjReport(Index)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AddReport(ByVal Report As Report) As Integer
        Try
            ReDim Preserve mObjReport(mIntNoofReports)
            mObjReport(mIntNoofReports) = Report
            mIntNoofReports += 1
            Return mIntNoofReports - 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function RemoveReport(ByVal Index As Integer) As Boolean
        Try
            mObjReport(Index) = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Count() As Integer
        Try
            Return mIntNoofReports
        Catch ex As Exception
            Return -1
        End Try
    End Function

End Class

<Xml.Serialization.XmlRoot(elementName:="Report")> _
 Public Class Report
    Private mName As String
    Private mReportInfo As String
    Private mDataSource As New DataSource
    Private mLayout As New Layout
    Private mFont As New Font
    Private mGroups As New Groups
    Private mSections As New Sections
    Private mFields As New Fields
    Private mStrTag As String

    <Xml.Serialization.XmlElement(elementName:="Name")> _
    <ComponentModel.Category("Report")> _
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="ReportInfo")> _
    <ComponentModel.Category("Report")> _
    Public Property ReportInfo() As String
        Get
            Return mReportInfo
        End Get
        Set(ByVal value As String)
            mReportInfo = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="DataSource")> _
    <ComponentModel.Category("Report")> _
    Public Property DataSource() As DataSource
        Get
            Return mDataSource
        End Get
        Set(ByVal value As DataSource)
            mDataSource = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Layout")> _
    <ComponentModel.Category("Report")> _
    Public Property Layout() As Layout
        Get
            Return mLayout
        End Get
        Set(ByVal value As Layout)
            mLayout = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Font")> _
    <ComponentModel.Category("Report")> _
    Public Property Font() As Font
        Get
            Return mFont
        End Get
        Set(ByVal value As Font)
            mFont = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Groups")> _
    <ComponentModel.Category("Report")> _
    Public Property Groups() As Groups
        Get
            Return mGroups
        End Get
        Set(ByVal value As Groups)
            mGroups = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Sections")> _
    <ComponentModel.Category("Report")> _
    Public Property Sections() As Sections
        Get
            Return mSections
        End Get
        Set(ByVal value As Sections)
            mSections = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Fields")> _
    <ComponentModel.Category("Report")> _
    Public Property Fields() As Fields
        Get
            Return mFields
        End Get
        Set(ByVal value As Fields)
            mFields = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Tag")> _
     Public Property Tag() As String
        Get
            Return mStrTag
        End Get
        Set(ByVal value As String)
            mStrTag = value
        End Set
    End Property

End Class

<Xml.Serialization.XmlRoot(elementName:="DataSource")> _
Public Class DataSource

    Private mStrConnectionString As String = String.Empty
    Private mStrRecordSource As String = "Report_1"

    <Xml.Serialization.XmlElement(elementName:="ConnectionString")> _
    <ComponentModel.Category("DataSource")> _
    Public Property ConnectionString() As String
        Get
            Return mStrConnectionString
        End Get
        Set(ByVal value As String)
            mStrConnectionString = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="RecordSource")> _
    <ComponentModel.Category("DataSource")> _
    Public Property RecordSource() As String
        Get
            Return mStrRecordSource
        End Get
        Set(ByVal value As String)
            mStrRecordSource = value
        End Set
    End Property
End Class

<Xml.Serialization.XmlRoot(elementName:="Layout")> _
Public Class Layout
    Private mIntWidth As Double
    Private mIntMarginLeft As Integer
    Private mIntMarginTop As Integer
    Private mIntMarginRight As Integer
    Private mIntMarginBottom As Integer
    Private mIntPaperSize As Integer
    Private mIntPageHeader As Integer
    Private mIntPageFooter As Integer
    Private mIntColumns As Integer
    Private mIntColumnLayout As Integer
    Private mIntOrientation As Integer
    Private mIntCustomWidth As Integer
    Private mIntCustomHeight As Integer
    Private mIntBackColor As Integer
    'Private mbytPicture As IO.Stream

    <Xml.Serialization.XmlElement(elementName:="Width")> _
   <ComponentModel.Category("Margin")> _
   Public Property Width() As Double
        Get
            Return mIntWidth
        End Get
        Set(ByVal value As Double)
            mIntWidth = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="MarginLeft")> _
    <ComponentModel.Category("Margin")> _
    Public Property MarginLeft() As Integer
        Get
            Return mIntMarginLeft
        End Get
        Set(ByVal value As Integer)
            mIntMarginLeft = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="MarginTop")> _
    <ComponentModel.Category("Margin")> _
    Public Property MarginTop() As Integer
        Get
            Return mIntMarginTop
        End Get
        Set(ByVal value As Integer)
            mIntMarginTop = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="MarginRight")> _
    <ComponentModel.Category("Margin")> _
    Public Property MarginRight() As Integer
        Get
            Return mIntMarginRight
        End Get
        Set(ByVal value As Integer)
            mIntMarginRight = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="MarginBottom")> _
    <ComponentModel.Category("Margin")> _
    Public Property MarginBottom() As Integer
        Get
            Return mIntMarginBottom
        End Get
        Set(ByVal value As Integer)
            mIntMarginBottom = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="PaperSize")> _
    <ComponentModel.Category("Margin")> _
    Public Property PaperSize() As Integer
        Get
            Return mIntPaperSize
        End Get
        Set(ByVal value As Integer)
            mIntPaperSize = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="PageHeader")> _
    <ComponentModel.Category("Margin")> _
    Public Property PageHeader() As Integer
        Get
            Return mIntPageHeader
        End Get
        Set(ByVal value As Integer)
            mIntPageHeader = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="PageFooter")> _
   <ComponentModel.Category("Margin")> _
   Public Property PageFooter() As Integer
        Get
            Return mIntPageFooter
        End Get
        Set(ByVal value As Integer)
            mIntPageFooter = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Columns")> _
    <ComponentModel.Category("Margin")> _
    Public Property Columns() As Integer
        Get
            Return mIntColumns
        End Get
        Set(ByVal value As Integer)
            mIntColumns = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="ColumnLayout")> _
    <ComponentModel.Category("Margin")> _
    Public Property ColumnLayout() As Integer
        Get
            Return mIntColumnLayout
        End Get
        Set(ByVal value As Integer)
            mIntColumnLayout = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Orientation")> _
    <ComponentModel.Category("Orientation")> _
    Public Property Orientation() As Integer
        Get
            Return mIntOrientation
        End Get
        Set(ByVal value As Integer)
            mIntOrientation = value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore()> _
    Public Property BackColor() As Integer
        Get
            Return mIntBackColor
        End Get
        Set(ByVal value As Integer)
            mIntBackColor = value
        End Set
    End Property

End Class

<Xml.Serialization.XmlRoot(elementName:="Font")> _
Public Class Font
    Private mStrName As String
    Private mIntSize As Double
    Private mIntBold As Integer
    Private mIntItalic As Integer

    <Xml.Serialization.XmlElement(elementName:="Name")> _
    <ComponentModel.Category("Font")> _
    Public Property Name() As String
        Get
            Return mStrName
        End Get
        Set(ByVal value As String)
            mStrName = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Size")> _
    <ComponentModel.Category("Font")> _
    Public Property Size() As Double
        Get
            Return mIntSize
        End Get
        Set(ByVal value As Double)
            mIntSize = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Bold")> _
    <ComponentModel.Category("Font")> _
    Public Property Bold() As Integer
        Get
            Return mIntBold
        End Get
        Set(ByVal value As Integer)
            mIntBold = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Italic")> _
    <ComponentModel.Category("Font")> _
    Public Property Italic() As Integer
        Get
            Return mIntItalic
        End Get
        Set(ByVal value As Integer)
            mIntItalic = value
        End Set
    End Property

End Class

<Xml.Serialization.XmlRoot(elementName:="Groups")> _
Public Class Groups

    Private mObjGroup() As Group
    Private mIntNoofGroups As Integer = 0

    <Xml.Serialization.XmlElement(elementName:="Group")> _
    <ComponentModel.Category("Groups")> _
    Public Property Group() As Group()
        Get
            Return mObjGroup
        End Get
        Set(ByVal value As Group())
            mObjGroup = value
        End Set
    End Property

    Public Function GetGroupByName(ByVal Name As String) As Group
        Dim ObjGroup As New Group
        Try
            For Each ObjGroup In mObjGroup
                If Len(Name) > 0 AndAlso Name.ToUpper = ObjGroup.Name.ToUpper Then
                    Return ObjGroup
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetGroupByIndex(ByVal Index As Integer) As Group
        Try
            Return mObjGroup(Index)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AddGroup(ByVal Group As Group) As Integer
        Try
            ReDim Preserve mObjGroup(mIntNoofGroups)
            mObjGroup(mIntNoofGroups) = Group
            mIntNoofGroups += 1
            Return mIntNoofGroups - 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function RemoveGroup(ByVal Index As Integer) As Boolean
        Try
            mObjGroup(Index) = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Count() As Integer
        Try
            Return mIntNoofGroups
        Catch ex As Exception
            Return -1
        End Try
    End Function

End Class

<Xml.Serialization.XmlRoot(elementName:="Group")> _
Public Class Group

    Private mStrName As String = String.Empty
    Private mStrGroupBy As String = String.Empty
    Private mIntSort As Int16
    Private mIntKeepTogether As Integer
    Private mStrTag As String

    <Xml.Serialization.XmlElement(elementName:="Name")> _
    <ComponentModel.Category("Group")> _
    Public Property Name() As String
        Get
            Return mStrName
        End Get
        Set(ByVal value As String)
            mStrName = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="GroupBy")> _
    <ComponentModel.Category("Group")> _
    Public Property GroupBy() As String
        Get
            Return mStrGroupBy
        End Get
        Set(ByVal value As String)
            mStrGroupBy = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Sort")> _
    <ComponentModel.Category("Group")> _
    Public Property Sort() As Int16
        Get
            Return mIntSort
        End Get
        Set(ByVal value As Int16)
            mIntSort = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="KeepTogether")> _
    <ComponentModel.Category("Group")> _
    Public Property KeepTogether() As Integer
        Get
            Return mIntKeepTogether
        End Get
        Set(ByVal value As Integer)
            mIntKeepTogether = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Tag")> _
 Public Property Tag() As String
        Get
            Return mStrTag
        End Get
        Set(ByVal value As String)
            mStrTag = value
        End Set
    End Property

End Class

<Xml.Serialization.XmlRoot(elementName:="Sections")> _
Public Class Sections

    Private mObjSection() As Section
    Private mIntNoofSections As Integer = 0

    <Xml.Serialization.XmlElement(elementName:="Section")> _
    <ComponentModel.Category("Sections")> _
    Public Property Section() As Section()
        Get
            Return mObjSection
        End Get
        Set(ByVal value As Section())
            mObjSection = value
        End Set
    End Property

    Public Function GetSectionByName(ByVal Name As String) As Section
        Dim ObjSection As New Section
        Try
            For Each ObjSection In mObjSection
                If Len(Name) > 0 AndAlso Name.ToUpper = ObjSection.Name.ToUpper Then
                    Return ObjSection
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetSectionByIndex(ByVal Index As Integer) As Section
        Try
            Return mObjSection(Index)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AddSection(ByVal Section As Section) As Integer
        Try
            ReDim Preserve mObjSection(mIntNoofSections)
            mObjSection(mIntNoofSections) = Section
            mIntNoofSections += 1
            Return mIntNoofSections - 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function RemoveSection(ByVal Index As Integer) As Boolean
        Try
            mObjSection(Index) = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Count() As Integer
        Try
            Return mIntNoofSections
        Catch ex As Exception
            Return -1
        End Try
    End Function

End Class

<Xml.Serialization.XmlRoot(elementName:="Section")> _
Public Class Section
    Private mStrName As String = String.Empty
    Private mIntType As Int16
    Private mIntHeight As Integer
    Private mIntVisible As Int16 = 1
    Private mintRepeat As Integer
    Private mIntForcePageBreak As Integer
    Private mIntBackColor As Integer = -1
    Private mStrOnPrint As String = String.Empty
    Private mStrOnFormat As String = String.Empty
    Private mStrTag As String = String.Empty

    <Xml.Serialization.XmlElement(elementName:="BackColor")> _
    <ComponentModel.Category("Appearance")> _
    Public Property BackColor() As Integer
        Get
            Return mIntBackColor
        End Get
        Set(ByVal value As Integer)
            mIntBackColor = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Name")> _
    <ComponentModel.Category("Design")> _
    Public Property Name() As String
        Get
            Return mStrName
        End Get
        Set(ByVal value As String)
            mStrName = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Tag")> _
    <ComponentModel.Category("Design")> _
    Public Property Tag() As String
        Get
            Return mStrTag
        End Get
        Set(ByVal value As String)
            mStrTag = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Visible")> _
    <ComponentModel.Category("Design")> _
    Public Property Visible() As Integer
        Get
            Return mIntVisible
        End Get
        Set(ByVal value As Integer)
            mIntVisible = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Type")> _
    <ComponentModel.Category("Section")> _
    Public Property Type() As Integer
        Get
            Return mIntType
        End Get
        Set(ByVal value As Integer)
            mIntType = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Height")> _
    <ComponentModel.Category("Section")> _
    Public Property Height() As Integer
        Get
            Return mIntHeight
        End Get
        Set(ByVal value As Integer)
            mIntHeight = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Repeat")> _
    <ComponentModel.Category("Section")> _
    Public Property Repeat() As Integer
        Get
            Return mintRepeat
        End Get
        Set(ByVal value As Integer)
            mintRepeat = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="ForcePageBreak")> _
    <ComponentModel.Category("Section")> _
    Public Property ForcePageBreak() As Integer
        Get
            Return mIntForcePageBreak
        End Get
        Set(ByVal value As Integer)
            mIntForcePageBreak = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="OnPrint")> _
    <ComponentModel.Category("Section")> _
    Public Property OnPrint() As String
        Get
            Return mStrOnPrint
        End Get
        Set(ByVal value As String)
            mStrOnPrint = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="OnFormat")> _
    <ComponentModel.Category("Section")> _
    Public Property OnFormat() As String
        Get
            Return mStrOnFormat
        End Get
        Set(ByVal value As String)
            mStrOnFormat = value
        End Set
    End Property

End Class

<Xml.Serialization.XmlRoot(elementName:="Fields")> _
Public Class Fields

    Private mObjField() As Field
    Private mIntNoofFields As Integer = 0

    <Xml.Serialization.XmlElement(elementName:="Field")> _
    <ComponentModel.Category("Fields")> _
    Public Property Field() As Field()
        Get
            Return mObjField
        End Get
        Set(ByVal value As Field())
            mObjField = value
        End Set
    End Property

    Public Function GetFieldByName(ByVal Name As String) As Field
        Dim ObjField As New Field
        Try
            For Each ObjField In mObjField
                If Not ObjField Is Nothing Then
                    If Len(Name) > 0 AndAlso Name.ToUpper = ObjField.Name.ToUpper Then
                        Return ObjField
                    End If
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetFieldByName_Language(ByVal Name As String, ByVal language As Integer) As Field
        Dim ObjField As New Field
        Dim StringHandler As New Venus.Shared.StringHandler
        Try
            For Each ObjField In mObjField
                If Not ObjField Is Nothing Then
                    If Len(Name) > 0 AndAlso Name.ToUpper = ObjField.Name.ToUpper Then
                        If StringHandler.GetValue(ObjField.Tag, "Language").ToUpper = "E" And language = 0 Then
                            Return ObjField
                        ElseIf StringHandler.GetValue(ObjField.Tag, "Language").ToUpper = "A" And language = 1 Then
                            Return ObjField
                        End If
                    End If
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetFieldByIndex(ByVal Index As Integer) As Field
        Try
            Return mObjField(Index)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AddField(ByVal Field As Field) As Integer
        Try
            mIntNoofFields = Count()
            ReDim Preserve mObjField(mIntNoofFields)
            mObjField(mIntNoofFields) = Field
            mIntNoofFields += 1
            Return mIntNoofFields - 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function RemoveField(ByVal Index As Integer) As Boolean
        Try
            mObjField(Index) = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Count() As Integer
        Try
            If mObjField Is Nothing Then
                Return 0
            Else
                Return mObjField.Length
            End If
            Return mObjField.Length
        Catch ex As Exception
            Return 0
        End Try
    End Function

End Class

<Xml.Serialization.XmlRoot(elementName:="Field")> _
Public Class Field
    Private mStrName As String = String.Empty
    Private mIntSection As Integer
    Private mStrText As String = String.Empty
    Private mIntCalculated As Integer
    Private mDblTop As Double
    Private mDblWidth As Double
    Private mDblHeight As Double
    Private mIntBorderStyle As Integer
    Private mIntLineSlant As Integer
    Private mDblLeft As Double
    Private mIntBorderColor As Integer = 0
    Private mIntLineWidth As Integer
    Private mIntAlign As Integer
    Private mIntForeColor As Integer = 0
    Private mIntBackColor As Integer = -1
    Private mIntBackStyle As Integer
    Private mIntForcePageBreak As Integer
    Private mIntCanGrow As Integer
    Private mIntVisible As Integer = 1
    Private mIntWordWrap As Integer
    Private mObjFont As New Font
    Private mStrFormat As String = String.Empty
    Private mIntSort As Integer = -1
    Private mIntKeepTogether As Integer = -1
    Private mIntZorder As Integer = 0
    Private mStrTag As String
    Private mIntPictureAlign As Integer = -1
    Private mIntPictureScale As Integer = -1
    'Private mStmPicture As Drawing.Bitmap
    'Private mStrEncoding As String = "base64"
    Private mObjPicture As New Picture

    <Xml.Serialization.XmlElement(elementName:="Name")> _
    <ComponentModel.Category("Field")> _
    Public Property Name() As String
        Get
            Return mStrName
        End Get
        Set(ByVal value As String)
            If value.IndexOf(" ") <> -1 Then
                mStrName = "[" & value & "]"
            Else
                mStrName = value
            End If
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Section")> _
    <ComponentModel.Category("Field")> _
    Public Property Section() As Integer
        Get
            Return mIntSection
        End Get
        Set(ByVal value As Integer)
            mIntSection = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Text")> _
    <ComponentModel.Category("Field")> _
    Public Property Text() As String
        Get
            Return mStrText
        End Get
        Set(ByVal value As String)
            mStrText = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Calculated")> _
    <ComponentModel.Category("Field")> _
    Public Property Calculated() As Integer
        Get
            Return mIntCalculated
        End Get
        Set(ByVal value As Integer)
            mIntCalculated = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Top")> _
    <ComponentModel.Category("Field")> _
    Public Property Top() As Double
        Get
            Return mDblTop
        End Get
        Set(ByVal value As Double)
            mDblTop = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Left")> _
    <ComponentModel.Category("Field")> _
    Public Property Left() As Double
        Get
            Return mDblLeft
        End Get
        Set(ByVal value As Double)
            mDblLeft = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Width")> _
    <ComponentModel.Category("Field")> _
    Public Property Width() As Double
        Get
            Return mDblWidth
        End Get
        Set(ByVal value As Double)
            mDblWidth = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Height")> _
    <ComponentModel.Category("Field")> _
    Public Property Height() As Double
        Get
            Return mDblHeight
        End Get
        Set(ByVal value As Double)
            mDblHeight = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="BorderStyle")> _
    <ComponentModel.Category("Field")> _
    Public Property BorderStyle() As Integer
        Get
            Return mIntBorderStyle
        End Get
        Set(ByVal value As Integer)
            mIntBorderStyle = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="LineSlant")> _
    <ComponentModel.Category("Field")> _
    Public Property LineSlant() As Integer
        Get
            Return mIntLineSlant
        End Get
        Set(ByVal value As Integer)
            mIntLineSlant = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="BorderColor")> _
    <ComponentModel.Category("Field")> _
    Public Property BorderColor() As Integer
        Get
            Return mIntBorderColor
        End Get
        Set(ByVal value As Integer)
            mIntBorderColor = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="LineWidth")> _
    <ComponentModel.Category("Field")> _
    Public Property LineWidth() As Integer
        Get
            Return mIntLineWidth
        End Get
        Set(ByVal value As Integer)
            mIntLineWidth = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Align")> _
    <ComponentModel.Category("Field")> _
    Public Property Align() As Integer
        Get
            Return mIntAlign
        End Get
        Set(ByVal value As Integer)
            mIntAlign = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="ForeColor")> _
    <ComponentModel.Category("Field")> _
    Public Property ForeColor() As Integer
        Get
            Return mIntForeColor
        End Get
        Set(ByVal value As Integer)
            mIntForeColor = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="BackColor")> _
    <ComponentModel.Category("Field")> _
    Public Property BackColor() As Integer
        Get
            Return mIntBackColor
        End Get
        Set(ByVal value As Integer)
            mIntBackColor = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="BackStyle")> _
<ComponentModel.Category("Field")> _
Public Property BackStyle() As Integer
        Get
            Return mIntBackStyle
        End Get
        Set(ByVal value As Integer)
            mIntBackStyle = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="ForcePageBreak")> _
    <ComponentModel.Category("Field")> _
    Public Property ForcePageBreak() As Integer
        Get
            Return mIntForcePageBreak
        End Get
        Set(ByVal value As Integer)
            mIntForcePageBreak = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="CanGrow")> _
    <ComponentModel.Category("Field")> _
    Public Property CanGrow() As Integer
        Get
            Return mIntCanGrow
        End Get
        Set(ByVal value As Integer)
            mIntCanGrow = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Visible")> _
    <ComponentModel.Category("Field")> _
    Public Property Visible() As Integer
        Get
            Return mIntVisible
        End Get
        Set(ByVal value As Integer)
            mIntVisible = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="WordWrap")> _
    <ComponentModel.Category("Field")> _
    Public Property WordWrap() As Integer
        Get
            Return mIntWordWrap
        End Get
        Set(ByVal value As Integer)
            mIntWordWrap = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Font")> _
    <ComponentModel.Category("Field")> _
    Public Property Font() As Font
        Get
            Return mObjFont
        End Get
        Set(ByVal value As Font)
            mObjFont = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Format")> _
    <ComponentModel.Category("Field")> _
    Public Property Format() As String
        Get
            Return mStrFormat
        End Get
        Set(ByVal value As String)
            mStrFormat = value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore()> _
    Public Property Sort() As Integer
        Get
            Return mIntSort
        End Get
        Set(ByVal value As Integer)
            mIntSort = value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore()> _
    Public Property KeepTogether() As Integer
        Get
            Return mIntKeepTogether
        End Get
        Set(ByVal value As Integer)
            mIntKeepTogether = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="ZOrder")> _
    <ComponentModel.Category("Field")> _
    Public Property ZOrder() As Integer
        Get
            Return mIntZorder
        End Get
        Set(ByVal value As Integer)
            mIntZorder = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Tag")> _
     <ComponentModel.Category("Field")> _
 Public Property Tag() As String
        Get
            Return mStrTag
        End Get
        Set(ByVal value As String)
            mStrTag = value
        End Set
    End Property

    '  <Xml.Serialization.XmlIgnore()> _
    'Public Property Picture() As Drawing.Bitmap
    '      Get
    '          Return mStmPicture
    '      End Get
    '      Set(ByVal value As Drawing.Bitmap)
    '          mStmPicture = value
    '      End Set
    '  End Property

    '  <Xml.Serialization.XmlElement(elementName:="Picture")> _
    '  <ComponentModel.Category("Field")> _
    '  Public Property PictureByteArray() As Byte()
    '      Get
    '          If Not mStmPicture Is Nothing Then
    '              Dim ObjStreem As New System.IO.MemoryStream
    '              Dim ObjByte() As Byte
    '              Try
    '                  mStmPicture.Save(ObjStreem, Drawing.Imaging.ImageFormat.Jpeg)
    '                  ObjByte = ObjStreem.GetBuffer
    '                  Return ObjByte
    '              Catch ex As Exception
    '                  Stop
    '              End Try
    '          Else
    '              Return Nothing
    '          End If
    '      End Get
    '      Set(ByVal value As Byte())
    '          If Not value Is Nothing Then
    '              mStmPicture = New Drawing.Bitmap(New IO.MemoryStream(value))
    '          Else
    '              mStmPicture = Nothing
    '          End If
    '      End Set
    '  End Property

    '  <Xml.Serialization.XmlAttribute(AttributeName:="encoding")> _
    '  Public Property engcoding() As String
    '      Get
    '          Return mStrEncoding
    '      End Get
    '      Set(ByVal value As String)
    '          mStrEncoding = value
    '      End Set
    '  End Property
    <Xml.Serialization.XmlElement(elementName:="Picture")> _
    <ComponentModel.Category("Field")> _
    Public Property Picture() As Byte()
        Get
            Return mObjPicture.PictureByteArray
        End Get
        Set(ByVal value As Byte())
            mObjPicture.PictureByteArray = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="PictureAlign")> _
    <ComponentModel.Category("Field")> _
    Public Property PictureAlign() As Integer
        Get
            Return mIntPictureAlign
        End Get
        Set(ByVal value As Integer)
            mIntPictureAlign = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="PictureScale")> _
    <ComponentModel.Category("Field")> _
    Public Property PictureScale() As Integer
        Get
            Return mIntPictureScale
        End Get
        Set(ByVal value As Integer)
            mIntPictureScale = value
        End Set
    End Property



End Class

<Xml.Serialization.XmlRoot(elementName:="Picture")> _
Public Class Picture
    Private mStmPicture As Drawing.Bitmap
    Private mStrEncoding As String = "base64"

    <Xml.Serialization.XmlAttribute(AttributeName:="encoding")> _
     Public Property engcoding() As String
        Get
            Return mStrEncoding
        End Get
        Set(ByVal value As String)
            mStrEncoding = value
        End Set
    End Property

    <Xml.Serialization.XmlIgnore()> _
  Public Property Picture() As Drawing.Bitmap
        Get
            Return mStmPicture
        End Get
        Set(ByVal value As Drawing.Bitmap)
            mStmPicture = value
        End Set
    End Property

    <Xml.Serialization.XmlElement(elementName:="Picture")> _
    <ComponentModel.Category("Field")> _
    Public Property PictureByteArray() As Byte()
        Get
            If Not mStmPicture Is Nothing Then
                Dim ObjStreem As New Global.System.IO.MemoryStream
                Dim ObjByte() As Byte
                Try
                    mStmPicture.Save(ObjStreem, Drawing.Imaging.ImageFormat.Jpeg)
                    ObjByte = ObjStreem.GetBuffer
                    Return ObjByte
                Catch ex As Exception
                    Stop
                End Try
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Byte())
            If Not value Is Nothing Then
                mStmPicture = New Drawing.Bitmap(New IO.MemoryStream(value))
            Else
                mStmPicture = Nothing
            End If
        End Set
    End Property

End Class



