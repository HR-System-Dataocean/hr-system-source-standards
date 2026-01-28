
'=========================================================================
'Created by : 0256
'Date : 30/12/2007
'                   Class: ReportsProperties
'                   Table: 
'=========================================================================

Public Class ClsRpw_ReportsProperties
    Inherits ClsDataAcessLayer

#Region "Class Constructor"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_Reports "
        mInsertParameter = " Code,EngName,ArbName,ArbName4S,EngDescription,ArbDescription,EngTitle,ArbTitle,ReportGroupID,ReportSource,DataSource,CRWName," & _
        "RefreshTimeInterval,Rank,CompanyLogo,CompanyHeader,CompanyText,IsLandScape,IsRightToLeft,ReportHeader,ReportFooter,PageHeader,PageFooter , " & _
        "HeaderBackColor,HeaderForeColor,HeaderFont,HeaderBorderColor,HeaderBorderWidth,ReportTopMargins,ReportBottomMargins,ReportLeftMargins,ReportRightMargins," & _
        "ReportBackColor,ReportForeColor,ReportAlternatingColor,ReportFont,ReportFontIsItalic,ReportFontIsBold,ReportFontIsUnderLine,ReportFontSize,HeaderFontIsItalic," & _
        "ViewForm,HeaderFontIsBold,HeaderFontIsUnderLine,HeaderFontSize,RowsBorderColor,HeaderBorderStyle,HeaderValignment,HeaderHalignment,FooterFont,FooterFontIsItalic,FooterFontIsBold," & _
        "FooterFontIsUnderLine,FooterFontSize,FooterValignment,FooterHalignment,FooterBackColor,FooterForeColor,FooterBorderColor,FooterBorderStyle,FooterBorderWidth," & _
        "HeaderHeight,FooterHeight, RowsForeColor,RowsBackColor,RowsFont,RowsFontIsItalic,RowsFontIsBold,RowsFontIsUnderLine,RowsFontSize,RowsHieght," & _
        "RowsTopBorderColor,RowsBottomBorderColor,RowsLeftBorderColor,RowsRightBorderColor,RowsBorderStyle,RowsTopBorderStyle," & _
        "RowsBottomBorderStyle,RowsLeftBorderStyle,RowsRightBorderStyle,RowsBorderWidth,RowsTopBorderWidth,RowsBottomBorderWidth,RowsLeftBorderWidth," & _
        "RowsRightBorderWidth,ScaleFactor,XMLReport,RegUserID,RegComputerID "


        mInsertParameterValues = " @Code,@EngName,@ArbName,@ArbName4S,@EngDescription,@ArbDescription,@EngTitle,@ArbTitle,@ReportGroupID,@ReportSource,@DataSource,@CRWName," & _
        "@RefreshTimeInterval,@Rank,@CompanyLogo,@CompanyHeader,@CompanyText,@IsLandScape,@IsRightToLeft,@ReportHeader,@ReportFooter,@PageHeader,@PageFooter," & _
        "@HeaderBackColor,@HeaderForeColor,@HeaderFont,@HeaderBorderColor,@HeaderBorderWidth,@ReportTopMargins,@ReportBottomMargins,@ReportLeftMargins,@ReportRightMargins," & _
        "@ReportBackColor,@ReportForeColor,@ReportAlternatingColor,@ReportFont,@ReportFontIsItalic,@ReportFontIsBold,@ReportFontIsUnderLine,@ReportFontSize,@HeaderFontIsItalic," & _
        "@ViewForm,@HeaderFontIsBold,@HeaderFontIsUnderLine,@HeaderFontSize,@RowsBorderColor,@HeaderBorderStyle,@HeaderValignment,@HeaderHalignment,@FooterFont,@FooterFontIsItalic,@FooterFontIsBold," & _
        "@FooterFontIsUnderLine,@FooterFontSize,@FooterValignment,@FooterHalignment,@FooterBackColor,@FooterForeColor,@FooterBorderColor,@FooterBorderStyle,@FooterBorderWidth," & _
        "@HeaderHeight,@FooterHeight,@RowsForeColor,@RowsBackColor,@RowsFont,@RowsFontIsItalic,@RowsFontIsBold,@RowsFontIsUnderLine,@RowsFontSize,@RowsHieght," & _
        "@RowsTopBorderColor,@RowsBottomBorderColor,@RowsLeftBorderColor,@RowsRightBorderColor,@RowsBorderStyle,@RowsTopBorderStyle," & _
        "@RowsBottomBorderStyle,@RowsLeftBorderStyle,@RowsRightBorderStyle,@RowsBorderWidth,@RowsTopBorderWidth,@RowsBottomBorderWidth,@RowsLeftBorderWidth," & _
        "@RowsRightBorderWidth,@ScaleFactor,@XMLReport,@RegUserID,@RegComputerID "

        mUpdateParameter = " Code=@Code, EngName=@EngName, ArbName=@ArbName, ArbName4S=@ArbName4S, EngDescription=@EngDescription, ArbDescription=@ArbDescription, EngTitle=@EngTitle, ArbTitle=@ArbTitle, " & _
        "ReportGroupID=@ReportGroupID, ReportSource=@ReportSource, DataSource=@DataSource, CRWName=@CRWName,RefreshTimeInterval=@RefreshTimeInterval, Rank=@Rank, CompanyLogo=@CompanyLogo, CompanyHeader=@CompanyHeader, " & _
        "CompanyText=@CompanyText, IsLandScape=@IsLandScape, IsRightToLeft=@IsRightToLeft, ReportHeader=@ReportHeader, ReportFooter=@ReportFooter, PageHeader=@PageHeader, PageFooter=@PageFooter, HeaderBackColor=@HeaderBackColor, " & _
        "ViewForm=@ViewForm,HeaderForeColor=@HeaderForeColor, HeaderFont=@HeaderFont, HeaderBorderColor=@HeaderBorderColor, HeaderBorderWidth=@HeaderBorderWidth, ReportTopMargins=@ReportTopMargins, ReportBottomMargins=@ReportBottomMargins, " & _
        "ReportLeftMargins=@ReportLeftMargins, ReportRightMargins=@ReportRightMargins, ReportBackColor=@ReportBackColor, ReportForeColor=@ReportForeColor, ReportAlternatingColor=@ReportAlternatingColor, " & _
        "ReportFont=@ReportFont, ReportFontIsItalic=@ReportFontIsItalic, ReportFontIsBold=@ReportFontIsBold, ReportFontIsUnderLine=@ReportFontIsUnderLine, ReportFontSize=@ReportFontSize, HeaderFontIsItalic=@HeaderFontIsItalic, " & _
        "HeaderFontIsBold=@HeaderFontIsBold, HeaderFontIsUnderLine=@HeaderFontIsUnderLine, HeaderFontSize=@HeaderFontSize, RowsBorderColor=@RowsBorderColor, HeaderBorderStyle=@HeaderBorderStyle, HeaderValignment=@HeaderValignment, " & _
        "HeaderHalignment=@HeaderHalignment, FooterFont=@FooterFont, FooterFontIsItalic=@FooterFontIsItalic, FooterFontIsBold=@FooterFontIsBold, FooterFontIsUnderLine=@FooterFontIsUnderLine, FooterFontSize=@FooterFontSize, " & _
        "FooterValignment=@FooterValignment, FooterHalignment=@FooterHalignment, FooterBackColor=@FooterBackColor, FooterForeColor=@FooterForeColor, FooterBorderColor=@FooterBorderColor, FooterBorderStyle=@FooterBorderStyle, " & _
        "FooterBorderWidth=@FooterBorderWidth, HeaderHeight=@HeaderHeight, FooterHeight=@FooterHeight,RowsForeColor=@RowsForeColor, RowsBackColor=@RowsBackColor, RowsFont=@RowsFont, RowsFontIsItalic=@RowsFontIsItalic, " & _
        "RowsFontIsBold=@RowsFontIsBold , RowsFontIsUnderLine=@RowsFontIsUnderLine, RowsFontSize=@RowsFontSize, RowsHieght=@RowsHieght, " & _
        "RowsTopBorderColor=@RowsTopBorderColor, RowsBottomBorderColor=@RowsBottomBorderColor, RowsLeftBorderColor=@RowsLeftBorderColor, RowsRightBorderColor=@RowsRightBorderColor, " & _
        "RowsBorderStyle=@RowsBorderStyle, RowsTopBorderStyle=@RowsTopBorderStyle, RowsBottomBorderStyle=@RowsBottomBorderStyle, RowsLeftBorderStyle=@RowsLeftBorderStyle, " & _
        "RowsRightBorderStyle=@RowsRightBorderStyle, RowsBorderWidth=@RowsBorderWidth, RowsTopBorderWidth=@RowsTopBorderWidth, RowsBottomBorderWidth=@RowsBottomBorderWidth, " & _
        "RowsLeftBorderWidth=@RowsLeftBorderWidth, RowsRightBorderWidth=@RowsRightBorderWidth, ScaleFactor=@ScaleFactor,XMLReport=@XMLReport,RegUserID=@RegUserID, RegComputerID=@RegComputerID "

        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    Private mID As Integer
    Private mCode As String
    Private mEngName As String
    Private mArbName As String
    Private mArbName4S As String
    Private mEngDescription As String
    Private mArbDescription As String
    Private mReportGroupID As Integer
    Private mReportSource As Integer
    Private mEngTitle As String
    Private mArbTitle As String
    Private mRefreshTimeInterval As Integer
    Private mRank As Integer
    Private mCompanyLogo As Boolean
    Private mCompanyText As Boolean
    Private mCompanyHeader As Boolean
    Private mIsLandscape As Boolean
    Private mIsRightToLeft As Boolean
    Private mReportHeader As Boolean
    Private mReportFooter As Boolean
    Private mPageHeader As Boolean
    Private mPageFooter As Boolean
    Private mDataSource As String
    Private mCRWName As String
    Private mHeaderBackColor As String
    Private mHeaderForeColor As String
    Private mHeaderFont As String
    Private mHeaderBorderColor As String
    Private mHeaderBorderWidth As Integer
    Private mReportTopMargins As Integer
    Private mReportBottomMargins As Integer
    Private mReportLeftMargins As Integer
    Private mReportRightMargins As Integer
    Private mReportBackColor As String
    Private mReportForeColor As String
    Private mReportAlternatingColor As String
    Private mReportFont As String
    Private mReportFontIsItalic As Boolean
    Private mReportFontIsBold As Boolean
    Private mReportFontIsUnderLine As Boolean
    Private mReportFontSize As Integer
    Private mHeaderFontIsItalic As Boolean
    Private mHeaderFontIsBold As Boolean
    Private mHeaderFontIsUnderLine As Boolean
    Private mHeaderFontSize As Integer
    Private mHeaderBorderStyle As Integer
    Private mHeaderValignment As Integer
    Private mHeaderHalignment As Integer
    Private mFooterFont As String
    Private mFooterFontIsItalic As Boolean
    Private mFooterFontIsBold As Boolean
    Private mFooterFontIsUnderLine As Boolean
    Private mFooterFontSize As Integer
    Private mFooterValignment As Integer
    Private mFooterHalignment As Integer
    Private mFooterBackColor As String
    Private mFooterForeColor As String
    Private mFooterBorderColor As String
    Private mFooterBorderStyle As Integer
    Private mFooterBorderWidth As Integer
    Private mHeaderHeight As Integer
    Private mFooterHeight As Integer

    Private mRowsForeColor As String
    Private mRowsBackColor As String
    Private mRowsFont As String
    Private mRowsFontIsItalic As Boolean
    Private mRowsFontIsBold As Boolean
    Private mRowsFontIsUnderLine As Boolean
    Private mRowsFontSize As Integer
    Private mRowsHieght As Integer
    Private mRowsBorderColor As String
    Private mRowsBorderStyle As Integer
    Private mRowsBorderWidth As Integer
    Private mRowsTopBorderColor As String
    Private mRowsBottomBorderColor As String
    Private mRowsLeftBorderColor As String
    Private mRowsRightBorderColor As String
    Private mRowsTopBorderStyle As Integer
    Private mRowsBottomBorderStyle As Integer
    Private mRowsLeftBorderStyle As Integer
    Private mRowsRightBorderStyle As Integer
    Private mRowsTopBorderWidth As Integer
    Private mRowsBottomBorderWidth As Integer
    Private mRowsLeftBorderWidth As Integer
    Private mRowsRightBorderWidth As Integer
    Private mXMLReport As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As Object
    Private mCancelDate As Object

    Private mScaleFactor As Object
    Private mViewForm As String

#End Region

#Region "Public Property"

    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal Value As Integer)
            mID = Value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return mCode
        End Get
        Set(ByVal Value As String)
            mCode = Value
        End Set
    End Property

    Public Property EngName() As String
        Get
            Return mEngName
        End Get
        Set(ByVal Value As String)
            mEngName = Value
        End Set
    End Property

    Public Property ArbName() As String
        Get
            Return mArbName
        End Get
        Set(ByVal Value As String)
            mArbName = Value
            mArbName4S = mStringHandler.ReplaceHamza(Value)
        End Set
    End Property

    Public Property EngDescription() As String
        Get
            Return mEngDescription
        End Get
        Set(ByVal Value As String)
            mEngDescription = Value
        End Set
    End Property

    Public Property ArbDescription() As String
        Get
            Return mArbDescription
        End Get
        Set(ByVal Value As String)
            mArbDescription = Value
        End Set
    End Property

    Public Property ReportGroupID() As Integer
        Get
            Return mReportGroupID
        End Get
        Set(ByVal Value As Integer)
            mReportGroupID = Value
        End Set
    End Property

    Public Property ReportSource() As Integer
        Get
            Return mReportSource
        End Get
        Set(ByVal Value As Integer)
            mReportSource = Value
        End Set
    End Property

    Public Property ArbTitle() As String
        Get
            Return mArbTitle
        End Get
        Set(ByVal Value As String)
            mArbTitle = Value
        End Set
    End Property

    Public Property EngTitle() As String
        Get
            Return mEngTitle
        End Get
        Set(ByVal Value As String)
            mEngTitle = Value
        End Set
    End Property

    Public Property RefreshTimeInterval() As Integer
        Get
            Return mRefreshTimeInterval
        End Get
        Set(ByVal Value As Integer)
            mRefreshTimeInterval = Value
        End Set
    End Property

    Public Property Rank() As Integer
        Get
            Return mRank
        End Get
        Set(ByVal Value As Integer)
            mRank = Value
        End Set
    End Property

    Public Property CompanyLogo() As Boolean
        Get
            Return mCompanyLogo
        End Get
        Set(ByVal Value As Boolean)
            mCompanyLogo = Value
        End Set
    End Property

    Public Property CompanyText() As Boolean
        Get
            Return mCompanyText
        End Get
        Set(ByVal Value As Boolean)
            mCompanyText = Value
        End Set
    End Property

    Public Property CompanyHeader() As Boolean
        Get
            Return mCompanyHeader
        End Get
        Set(ByVal Value As Boolean)
            mCompanyHeader = Value
        End Set
    End Property

    Public Property IsLandscape() As Boolean
        Get
            Return mIsLandscape
        End Get
        Set(ByVal Value As Boolean)
            mIsLandscape = Value
        End Set
    End Property

    Public Property IsRightToLeft() As Boolean
        Get
            Return mIsRightToLeft
        End Get
        Set(ByVal Value As Boolean)
            mIsRightToLeft = Value
        End Set
    End Property

    Public Property ReportHeader() As Boolean
        Get
            Return mReportHeader
        End Get
        Set(ByVal Value As Boolean)
            mReportHeader = Value
        End Set
    End Property

    Public Property ReportFooter() As Boolean
        Get
            Return mReportFooter
        End Get
        Set(ByVal Value As Boolean)
            mReportFooter = Value
        End Set
    End Property

    Public Property PageHeader() As Boolean
        Get
            Return mPageHeader
        End Get
        Set(ByVal Value As Boolean)
            mPageHeader = Value
        End Set
    End Property

    Public Property PageFooter() As Boolean
        Get
            Return mPageFooter
        End Get
        Set(ByVal Value As Boolean)
            mPageFooter = Value
        End Set
    End Property

    Public Property DataSource() As String
        Get
            Return mDataSource
        End Get
        Set(ByVal Value As String)
            mDataSource = Value
        End Set
    End Property

    Public Property CRWName() As String
        Get
            Return mCRWName
        End Get
        Set(ByVal Value As String)
            mCRWName = Value
        End Set
    End Property


    Public Property ViewForm() As String
        Get
            Return mViewForm
        End Get
        Set(ByVal value As String)
            mViewForm = value
        End Set
    End Property
    Public Property HeaderBackColor() As String
        Get
            Return mHeaderBackColor
        End Get
        Set(ByVal Value As String)
            mHeaderBackColor = Value
        End Set
    End Property

    Public Property HeaderForeColor() As String
        Get
            Return mHeaderForeColor
        End Get
        Set(ByVal Value As String)
            mHeaderForeColor = Value
        End Set
    End Property

    Public Property HeaderFont() As String
        Get
            Return mHeaderFont
        End Get
        Set(ByVal Value As String)
            mHeaderFont = Value
        End Set
    End Property

    Public Property HeaderBorderColor() As String
        Get
            Return mHeaderBorderColor
        End Get
        Set(ByVal Value As String)
            mHeaderBorderColor = Value
        End Set
    End Property

    Public Property HeaderBorderWidth() As Integer
        Get
            Return mHeaderBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mHeaderBorderWidth = Value
        End Set
    End Property

    Public Property ReportTopMargins() As Integer
        Get
            Return mReportTopMargins
        End Get
        Set(ByVal Value As Integer)
            mReportTopMargins = Value
        End Set
    End Property

    Public Property ReportBottomMargins() As Integer
        Get
            Return mReportBottomMargins
        End Get
        Set(ByVal Value As Integer)
            mReportBottomMargins = Value
        End Set
    End Property

    Public Property ReportLeftMargins() As Integer
        Get
            Return mReportLeftMargins
        End Get
        Set(ByVal Value As Integer)
            mReportLeftMargins = Value
        End Set
    End Property

    Public Property ReportRightMargins() As Integer
        Get
            Return mReportRightMargins
        End Get
        Set(ByVal Value As Integer)
            mReportRightMargins = Value
        End Set
    End Property

    Public Property ReportBackColor() As String
        Get
            Return mReportBackColor
        End Get
        Set(ByVal Value As String)
            mReportBackColor = Value
        End Set
    End Property

    Public Property ReportForeColor() As String
        Get
            Return mReportForeColor
        End Get
        Set(ByVal Value As String)
            mReportForeColor = Value
        End Set
    End Property

    Public Property ReportAlternatingColor() As String
        Get
            Return mReportAlternatingColor
        End Get
        Set(ByVal Value As String)
            mReportAlternatingColor = Value
        End Set
    End Property

    Public Property ReportFont() As String
        Get
            Return mReportFont
        End Get
        Set(ByVal Value As String)
            mReportFont = Value
        End Set
    End Property

    Public Property ReportFontIsItalic() As Boolean
        Get
            Return mReportFontIsItalic
        End Get
        Set(ByVal Value As Boolean)
            mReportFontIsItalic = Value
        End Set
    End Property

    Public Property ReportFontIsBold() As Boolean
        Get
            Return mReportFontIsBold
        End Get
        Set(ByVal Value As Boolean)
            mReportFontIsBold = Value
        End Set
    End Property

    Public Property ReportFontIsUnderLine() As Boolean
        Get
            Return mReportFontIsUnderLine
        End Get
        Set(ByVal Value As Boolean)
            mReportFontIsUnderLine = Value
        End Set
    End Property

    Public Property ReportFontSize() As Integer
        Get
            Return mReportFontSize
        End Get
        Set(ByVal Value As Integer)
            mReportFontSize = Value
        End Set
    End Property

    Public Property HeaderFontIsItalic() As Boolean
        Get
            Return mHeaderFontIsItalic
        End Get
        Set(ByVal Value As Boolean)
            mHeaderFontIsItalic = Value
        End Set
    End Property

    Public Property HeaderFontIsBold() As Boolean
        Get
            Return mHeaderFontIsBold
        End Get
        Set(ByVal Value As Boolean)
            mHeaderFontIsBold = Value
        End Set
    End Property

    Public Property HeaderFontIsUnderLine() As Boolean
        Get
            Return mHeaderFontIsUnderLine
        End Get
        Set(ByVal Value As Boolean)
            mHeaderFontIsUnderLine = Value
        End Set
    End Property

    Public Property HeaderFontSize() As Integer
        Get
            Return mHeaderFontSize
        End Get
        Set(ByVal Value As Integer)
            mHeaderFontSize = Value
        End Set
    End Property

    Public Property HeaderBorderStyle() As Integer
        Get
            Return mHeaderBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mHeaderBorderStyle = Value
        End Set
    End Property

    Public Property HeaderValignment() As Integer
        Get
            Return mHeaderValignment
        End Get
        Set(ByVal Value As Integer)
            mHeaderValignment = Value
        End Set
    End Property

    Public Property HeaderHalignment() As Integer
        Get
            Return mHeaderHalignment
        End Get
        Set(ByVal Value As Integer)
            mHeaderHalignment = Value
        End Set
    End Property

    Public Property FooterFont() As String
        Get
            Return mFooterFont
        End Get
        Set(ByVal Value As String)
            mFooterFont = Value
        End Set
    End Property

    Public Property FooterFontIsItalic() As Boolean
        Get
            Return mFooterFontIsItalic
        End Get
        Set(ByVal Value As Boolean)
            mFooterFontIsItalic = Value
        End Set
    End Property

    Public Property FooterFontIsBold() As Boolean
        Get
            Return mFooterFontIsBold
        End Get
        Set(ByVal Value As Boolean)
            mFooterFontIsBold = Value
        End Set
    End Property

    Public Property FooterFontIsUnderLine() As Boolean
        Get
            Return mFooterFontIsUnderLine
        End Get
        Set(ByVal Value As Boolean)
            mFooterFontIsUnderLine = Value
        End Set
    End Property

    Public Property FooterFontSize() As Integer
        Get
            Return mFooterFontSize
        End Get
        Set(ByVal Value As Integer)
            mFooterFontSize = Value
        End Set
    End Property

    Public Property FooterValignment() As Integer
        Get
            Return mFooterValignment
        End Get
        Set(ByVal Value As Integer)
            mFooterValignment = Value
        End Set
    End Property

    Public Property FooterHalignment() As Integer
        Get
            Return mFooterHalignment
        End Get
        Set(ByVal Value As Integer)
            mFooterHalignment = Value
        End Set
    End Property

    Public Property FooterBackColor() As String
        Get
            Return mFooterBackColor
        End Get
        Set(ByVal Value As String)
            mFooterBackColor = Value
        End Set
    End Property

    Public Property FooterForeColor() As String
        Get
            Return mFooterForeColor
        End Get
        Set(ByVal Value As String)
            mFooterForeColor = Value
        End Set
    End Property

    Public Property FooterBorderColor() As String
        Get
            Return mFooterBorderColor
        End Get
        Set(ByVal Value As String)
            mFooterBorderColor = Value
        End Set
    End Property

    Public Property FooterBorderStyle() As Integer
        Get
            Return mFooterBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mFooterBorderStyle = Value
        End Set
    End Property

    Public Property FooterBorderWidth() As Integer
        Get
            Return mFooterBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mFooterBorderWidth = Value
        End Set
    End Property

    Public Property HeaderHeight() As Integer
        Get
            Return mHeaderHeight
        End Get
        Set(ByVal Value As Integer)
            mHeaderHeight = Value
        End Set
    End Property

    Public Property FooterHeight() As Integer
        Get
            Return mFooterHeight
        End Get
        Set(ByVal Value As Integer)
            mFooterHeight = Value
        End Set
    End Property


    '///Rows
    Public Property RowsForeColor() As String
        Get
            Return mRowsForeColor
        End Get
        Set(ByVal Value As String)
            mRowsForeColor = Value
        End Set
    End Property

    Public Property RowsBackColor() As String
        Get
            Return mRowsBackColor
        End Get
        Set(ByVal Value As String)
            mRowsBackColor = Value
        End Set
    End Property

    Public Property RowsFont() As String
        Get
            Return mRowsFont
        End Get
        Set(ByVal Value As String)
            mRowsFont = Value
        End Set
    End Property

    Public Property RowsFontIsItalic() As Boolean
        Get
            Return mRowsFontIsItalic
        End Get
        Set(ByVal Value As Boolean)
            mRowsFontIsItalic = Value
        End Set
    End Property

    Public Property RowsFontIsBold() As Boolean
        Get
            Return mRowsFontIsBold
        End Get
        Set(ByVal Value As Boolean)
            mRowsFontIsBold = Value
        End Set
    End Property

    Public Property RowsFontIsUnderLine() As Boolean
        Get
            Return mRowsFontIsUnderLine
        End Get
        Set(ByVal Value As Boolean)
            mRowsFontIsUnderLine = Value
        End Set
    End Property

    Public Property RowsFontSize() As Integer
        Get
            Return mRowsFontSize
        End Get
        Set(ByVal Value As Integer)
            mRowsFontSize = Value
        End Set
    End Property

    Public Property RowsHieght() As Integer
        Get
            Return mRowsHieght
        End Get
        Set(ByVal Value As Integer)
            mRowsHieght = Value
        End Set
    End Property

    Public Property RowsBorderColor() As String
        Get
            Return mRowsBorderColor
        End Get
        Set(ByVal Value As String)
            mRowsBorderColor = Value
        End Set
    End Property

    Public Property RowsBorderStyle() As Integer
        Get
            Return mRowsBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mRowsBorderStyle = Value
        End Set
    End Property

    Public Property RowsBorderWidth() As Integer
        Get
            Return mRowsBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mRowsBorderWidth = Value
        End Set
    End Property

    Public Property RowsTopBorderColor() As String
        Get
            Return mRowsTopBorderColor
        End Get
        Set(ByVal Value As String)
            mRowsTopBorderColor = Value
        End Set
    End Property

    Public Property RowsBottomBorderColor() As String
        Get
            Return mRowsBottomBorderColor
        End Get
        Set(ByVal Value As String)
            mRowsBottomBorderColor = Value
        End Set
    End Property

    Public Property RowsLeftBorderColor() As String
        Get
            Return mRowsLeftBorderColor
        End Get
        Set(ByVal Value As String)
            mRowsLeftBorderColor = Value
        End Set
    End Property

    Public Property RowsRightBorderColor() As String
        Get
            Return mRowsRightBorderColor
        End Get
        Set(ByVal Value As String)
            mRowsRightBorderColor = Value
        End Set
    End Property

    Public Property RowsTopBorderStyle() As Integer
        Get
            Return mRowsTopBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mRowsTopBorderStyle = Value
        End Set
    End Property

    Public Property RowsBottomBorderStyle() As Integer
        Get
            Return mRowsBottomBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mRowsBottomBorderStyle = Value
        End Set
    End Property

    Public Property RowsLeftBorderStyle() As Integer
        Get
            Return mRowsLeftBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mRowsLeftBorderStyle = Value
        End Set
    End Property

    Public Property RowsRightBorderStyle() As Integer
        Get
            Return mRowsRightBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mRowsRightBorderStyle = Value
        End Set
    End Property

    Public Property RowsTopBorderWidth() As Integer
        Get
            Return mRowsTopBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mRowsTopBorderWidth = Value
        End Set
    End Property

    Public Property RowsBottomBorderWidth() As Integer
        Get
            Return mRowsBottomBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mRowsBottomBorderWidth = Value
        End Set
    End Property

    Public Property RowsLeftBorderWidth() As Integer
        Get
            Return mRowsLeftBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mRowsLeftBorderWidth = Value
        End Set
    End Property

    Public Property RowsRightBorderWidth() As Integer
        Get
            Return mRowsRightBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mRowsRightBorderWidth = Value
        End Set
    End Property

    Public Property ScaleFactor() As Integer
        Get
            Return mScaleFactor
        End Get
        Set(ByVal value As Integer)
            mScaleFactor = value
        End Set
    End Property

    Public Property XMLReport() As String
        Get
            Return mXMLReport
        End Get
        Set(ByVal value As String)
            mXMLReport = value
        End Set
    End Property

    Public Property RegUserID() As Integer
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Integer)
            mRegUserID = Value
        End Set
    End Property

    Public Property RegComputerID() As Integer
        Get
            Return mRegComputerID
        End Get
        Set(ByVal Value As Integer)
            mRegComputerID = Value
        End Set
    End Property

    Public ReadOnly Property RegDate() As Object
        Get
            Return mRegDate
        End Get
    End Property

    Public Property CancelDate() As Object
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As Object)
            mCancelDate = Value
        End Set
    End Property

#End Region

#Region "Public Functions"

    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
            Else
                Clear()
            End If
            If mID > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = " Select ID From " & Me.mTable & " Where " & Filter
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = strSQL
            mSqlCommand.Connection.Open()
            Value = mSqlCommand.ExecuteScalar()
            mSqlCommand.Connection.Close()
            If Value > 0 Then
                Update(Filter)
            Else
                Save()
            End If
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(strSQL, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function Clear() As Boolean
        Try
            mID = 0
            mCode = String.Empty
            mEngName = String.Empty
            mArbName = String.Empty
            mArbName4S = String.Empty
            mEngDescription = String.Empty
            mArbDescription = String.Empty
            mEngTitle = String.Empty
            mArbTitle = String.Empty
            mDataSource = String.Empty
            mCRWName = String.Empty

            mReportGroupID = 0
            mReportSource = 0
            mRefreshTimeInterval = 0
            mRank = 0
            mCompanyHeader = False
            mCompanyLogo = False
            mCompanyText = False
            mIsLandscape = False
            mIsRightToLeft = False
            mPageHeader = False
            mPageFooter = False

            mHeaderBackColor = String.Empty
            mHeaderForeColor = String.Empty
            mHeaderFont = String.Empty
            mHeaderBorderColor = String.Empty
            mHeaderBorderWidth = 0
            mHeaderFontIsItalic = False
            mHeaderFontIsBold = False
            mHeaderFontIsUnderLine = False
            mHeaderFontSize = 0
            mHeaderHalignment = 0
            mHeaderValignment = 0

            mReportHeader = False
            mReportFooter = False
            mReportTopMargins = 0
            mReportBottomMargins = 0
            mReportLeftMargins = 0
            mReportRightMargins = 0
            mReportBackColor = String.Empty
            mReportForeColor = String.Empty
            mReportAlternatingColor = String.Empty
            mReportFont = String.Empty
            mReportFontIsItalic = False
            mReportFontIsBold = False
            mReportFontIsUnderLine = False
            mReportFontSize = 0

            mRowsBorderColor = String.Empty
            mHeaderBorderStyle = 0

            mFooterFont = String.Empty
            mFooterFontIsItalic = False
            mFooterFontIsBold = False
            mFooterFontIsUnderLine = False
            mFooterFontSize = 0
            mFooterHalignment = 0
            mFooterValignment = 0
            mFooterBackColor = String.Empty
            mFooterForeColor = String.Empty
            mFooterBorderColor = String.Empty
            mFooterBorderStyle = 0
            mFooterBorderWidth = 0
            mHeaderHeight = 0
            mFooterHeight = 0
            mXMLReport = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function LastRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function NextRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function previousRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataSet = New DataSet
            mSqlDataAdapter.Fill(mDataSet)
            If mDataHandler.CheckValidDataObject(mDataSet) Then
                GetParameter(mDataSet)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function CheckDiff(ByVal ClassObj As Object, ByVal DSData As DataSet, ByVal Filter As String) As Boolean

        Dim myPropertyInfo As Reflection.PropertyInfo() = CType(ClassObj.GetType, Type).GetProperties()
        Dim PropertyCounter As Integer
        Dim DSCounter As Integer
        Dim Index() As Object

        For PropertyCounter = 1 To myPropertyInfo.Length - 1
            For DSCounter = 1 To DSData.Tables(0).Columns.Count - 1

                With DSData.Tables(0).Columns(DSCounter)
                    Dim myPropInfo As Reflection.PropertyInfo = CType(myPropertyInfo(PropertyCounter), Reflection.PropertyInfo)

                    'Check column name (ex: CODE=CODE)
                    If myPropInfo.Name.ToString.ToUpper = .ColumnName.ToUpper Then

                        'Check column value (ex: 002=002)
                        If Not myPropInfo.GetValue(ClassObj, Index) = FixNull(DSData.Tables(0).Rows(0)(DSCounter), DSData.Tables(0).Columns(DSCounter)) Then
                            Return True

                            Exit For
                        End If

                        Exit For
                    End If

                End With

            Next
        Next

    End Function

    Public Function FixNull(ByVal obj As Object, ByVal DataColumn As Global.System.Data.DataColumn) As Object
        Try

            Select Case DataColumn.DataType.Name.ToUpper
                Case "DECIMAL", "DOUBLE"
                    If obj Is DBNull.Value Then
                        Return 0.0
                    Else
                        Return obj
                    End If
                Case "INT32"
                    If obj Is DBNull.Value Then
                        Return 0
                    Else
                        Return obj
                    End If
                Case "DATETIME"
                    If obj Is DBNull.Value Then
                        Return Nothing
                    Else
                        Return obj
                    End If
                Case "STRING"
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
                Case "BIT"
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
                Case Else
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
            End Select

        Catch ex As Exception
            Return ""
        End Try
    End Function


    Public Function GetReportsProperties(ByVal IntReportID As Integer, ByRef dsReportsProperties As Data.DataSet) As Boolean

        Dim ClsReportMain As New ClsRpw_ReportsMain(mPage)
        Try
            Find(" ID= " & IntReportID)
            If dsReportsProperties.Tables(0).Rows.Count = 0 Then
                dsReportsProperties.Tables(0).Rows.Add()
            End If
            dsReportsProperties.Tables(0).Rows(0).Item("ID") = mDataHandler.DataValue_Out(ID, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("Code") = mDataHandler.DataValue_Out(Code, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("EngName") = mDataHandler.DataValue_Out(EngName, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("ArbName") = mDataHandler.DataValue_Out(ArbName, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("EngTitle") = mDataHandler.DataValue_Out(EngTitle, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("ArbTitle") = mDataHandler.DataValue_Out(ArbTitle, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("EngDescription") = mDataHandler.DataValue_Out(EngDescription, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("ArbDescription") = mDataHandler.DataValue_Out(ArbDescription, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportGroupID") = mDataHandler.DataValue_Out(ReportGroupID, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportSource") = mDataHandler.DataValue_Out(ReportSource, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("RefreshTimeInterval") = mDataHandler.DataValue_Out(RefreshTimeInterval, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("Rank") = mDataHandler.DataValue_Out(Rank, SqlDbType.Int)

            dsReportsProperties.Tables(0).Rows(0).Item("CompanyLogo") = mDataHandler.DataValue_Out(CompanyLogo, SqlDbType.Bit)
            dsReportsProperties.Tables(0).Rows(0).Item("CompanyText") = mDataHandler.DataValue_Out(CompanyText, SqlDbType.Bit)
            dsReportsProperties.Tables(0).Rows(0).Item("CompanyHeader") = mDataHandler.DataValue_Out(CompanyHeader, SqlDbType.Bit)
            dsReportsProperties.Tables(0).Rows(0).Item("IsLandscape") = mDataHandler.DataValue_Out(IsLandscape, SqlDbType.Bit)
            dsReportsProperties.Tables(0).Rows(0).Item("IsRightToLeft") = mDataHandler.DataValue_Out(IsRightToLeft, SqlDbType.Bit)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportHeader") = mDataHandler.DataValue_Out(ReportHeader, SqlDbType.Bit)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportFooter") = mDataHandler.DataValue_Out(ReportFooter, SqlDbType.Bit)
            dsReportsProperties.Tables(0).Rows(0).Item("PageHeader") = mDataHandler.DataValue_Out(PageHeader, SqlDbType.Bit)
            dsReportsProperties.Tables(0).Rows(0).Item("PageFooter") = mDataHandler.DataValue_Out(PageFooter, SqlDbType.Bit)

            dsReportsProperties.Tables(0).Rows(0).Item("DataSource") = mDataHandler.DataValue_Out(DataSource, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("CRWName") = mDataHandler.DataValue_Out(CRWName, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderBackColor") = mDataHandler.DataValue_Out(HeaderBackColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderForeColor") = mDataHandler.DataValue_Out(HeaderForeColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderFont") = mDataHandler.DataValue_Out(HeaderFont, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderBorderColor") = mDataHandler.DataValue_Out(HeaderBorderColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderBorderWidth") = mDataHandler.DataValue_Out(HeaderBorderWidth, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportTopMargins") = mDataHandler.DataValue_Out(ReportTopMargins, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportBottomMargins") = mDataHandler.DataValue_Out(ReportBottomMargins, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportLeftMargins") = mDataHandler.DataValue_Out(ReportLeftMargins, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportRightMargins") = mDataHandler.DataValue_Out(ReportRightMargins, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportBackColor") = mDataHandler.DataValue_Out(ReportBackColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportForeColor") = mDataHandler.DataValue_Out(ReportForeColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportAlternatingColor") = mDataHandler.DataValue_Out(ReportAlternatingColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportFont") = mDataHandler.DataValue_Out(ReportFont, SqlDbType.VarChar)

            dsReportsProperties.Tables(0).Rows(0).Item("ReportFontIsItalic") = IIf(ReportFontIsItalic = True, "Yes", "No")
            dsReportsProperties.Tables(0).Rows(0).Item("ReportFontIsBold") = IIf(ReportFontIsBold = True, "Yes", "No")
            dsReportsProperties.Tables(0).Rows(0).Item("ReportFontIsUnderLine") = IIf(ReportFontIsUnderLine = True, "Yes", "No")
            dsReportsProperties.Tables(0).Rows(0).Item("ReportFontSize") = ReportFontSize

            dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontIsItalic") = IIf(HeaderFontIsItalic = True, "Yes", "No")
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontIsBold") = IIf(HeaderFontIsBold = True, "Yes", "No")
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontIsUnderLine") = IIf(HeaderFontIsUnderLine = True, "Yes", "No")
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontSize") = HeaderFontSize

            'dsReportsProperties.Tables(0).Rows(0).Item("RowsBorderColor") = mDataHandler.DataValue_Out(RowsBorderColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("BorderStyle") = mDataHandler.DataValue_Out(HeaderBorderStyle, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderValignment") = mDataHandler.DataValue_Out(HeaderValignment, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderHalignment") = mDataHandler.DataValue_Out(HeaderHalignment, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("FooterFont") = mDataHandler.DataValue_Out(FooterFont, SqlDbType.VarChar)

            dsReportsProperties.Tables(0).Rows(0).Item("FooterFontIsItalic") = IIf(FooterFontIsItalic = True, "Yes", "No")
            dsReportsProperties.Tables(0).Rows(0).Item("FooterFontIsBold") = IIf(FooterFontIsBold = True, "Yes", "No")
            dsReportsProperties.Tables(0).Rows(0).Item("FooterFontIsUnderLine") = IIf(FooterFontIsUnderLine = True, "Yes", "No")
            dsReportsProperties.Tables(0).Rows(0).Item("FooterFontSize") = FooterFontSize

            dsReportsProperties.Tables(0).Rows(0).Item("FooterHalignment") = mDataHandler.DataValue_Out(FooterHalignment, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("FooterValignment") = mDataHandler.DataValue_Out(FooterValignment, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("FooterBackColor") = mDataHandler.DataValue_Out(FooterBackColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("FooterForeColor") = mDataHandler.DataValue_Out(FooterForeColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("FooterBorderColor") = mDataHandler.DataValue_Out(FooterBorderColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("FooterBorderStyle") = mDataHandler.DataValue_Out(FooterBorderStyle, SqlDbType.Int)
            dsReportsProperties.Tables(0).Rows(0).Item("FooterBorderWidth") = mDataHandler.DataValue_Out(FooterBorderWidth, SqlDbType.Int)

            dsReportsProperties.Tables(0).Rows(0).Item("ViewForm") = mDataHandler.DataValue_Out(ViewForm, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("ScaleFactor") = mDataHandler.DataValue_Out(ScaleFactor, SqlDbType.Int)
            '// Rows
            If dsReportsProperties.Tables(1).Rows.Count = 0 Then
                dsReportsProperties.Tables(1).Rows.Add()
            End If

            dsReportsProperties.Tables(1).Rows(0).Item("ForeColor") = mDataHandler.DataValue_Out(RowsForeColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(1).Rows(0).Item("BackColor") = mDataHandler.DataValue_Out(RowsBackColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(1).Rows(0).Item("Font") = mDataHandler.DataValue_Out(RowsFont, SqlDbType.VarChar)
            dsReportsProperties.Tables(1).Rows(0).Item("FontIsItalic") = mDataHandler.DataValue_Out(RowsFontIsItalic, SqlDbType.Bit)
            dsReportsProperties.Tables(1).Rows(0).Item("FontIsBold") = mDataHandler.DataValue_Out(RowsFontIsBold, SqlDbType.Bit)
            dsReportsProperties.Tables(1).Rows(0).Item("FontIsUnderLine") = mDataHandler.DataValue_Out(RowsFontIsUnderLine, SqlDbType.Bit)
            dsReportsProperties.Tables(1).Rows(0).Item("FontSize") = mDataHandler.DataValue_Out(RowsFontSize, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("RowHieght") = mDataHandler.DataValue_Out(RowsHieght, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("BorderColor") = mDataHandler.DataValue_Out(RowsBorderColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(1).Rows(0).Item("TopBorderColor") = mDataHandler.DataValue_Out(RowsTopBorderColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(1).Rows(0).Item("BottomBorderColor") = mDataHandler.DataValue_Out(RowsBottomBorderColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(1).Rows(0).Item("LeftBorderColor") = mDataHandler.DataValue_Out(RowsLeftBorderColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(1).Rows(0).Item("RightBorderColor") = mDataHandler.DataValue_Out(RowsRightBorderColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(1).Rows(0).Item("BorderStyle") = mDataHandler.DataValue_Out(RowsBorderStyle, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("TopBorderStyle") = mDataHandler.DataValue_Out(RowsTopBorderStyle, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("BottomBorderStyle") = mDataHandler.DataValue_Out(RowsBottomBorderStyle, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("LeftBorderStyle") = mDataHandler.DataValue_Out(RowsLeftBorderStyle, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("RightBorderStyle") = mDataHandler.DataValue_Out(RowsRightBorderStyle, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("BorderWidth") = mDataHandler.DataValue_Out(RowsBorderWidth, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("TopBorderWidth") = mDataHandler.DataValue_Out(RowsTopBorderWidth, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("BottomBorderWidth") = mDataHandler.DataValue_Out(RowsBottomBorderWidth, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("LeftBorderWidth") = mDataHandler.DataValue_Out(RowsLeftBorderWidth, SqlDbType.Int)
            dsReportsProperties.Tables(1).Rows(0).Item("RightBorderWidth") = mDataHandler.DataValue_Out(RowsRightBorderWidth, SqlDbType.Int)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function GetDefaultReportsProperties(ByVal IntReportID As Integer, ByVal strDataSource As String, ByVal strReportName As String, ByVal strReportCode As String, ByRef dsReportsProperties As Data.DataSet) As Boolean

        Dim ClsReportMain As New ClsRpw_ReportsMain(mPage)
        Try

            If dsReportsProperties.Tables(0).Rows.Count = 0 Then
                dsReportsProperties.Tables(0).Rows.Add()
            End If
            dsReportsProperties.Tables(0).Rows(0).Item("ID") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("Code") = strReportCode
            dsReportsProperties.Tables(0).Rows(0).Item("EngName") = strReportName
            dsReportsProperties.Tables(0).Rows(0).Item("ArbName") = strReportName
            dsReportsProperties.Tables(0).Rows(0).Item("EngTitle") = strReportName
            dsReportsProperties.Tables(0).Rows(0).Item("ArbTitle") = strReportName
            dsReportsProperties.Tables(0).Rows(0).Item("EngDescription") = strReportName
            dsReportsProperties.Tables(0).Rows(0).Item("ArbDescription") = strReportName
            dsReportsProperties.Tables(0).Rows(0).Item("ReportGroupID") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("ReportSource") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("RefreshTimeInterval") = 0
            'dsReportsProperties.Tables(0).Rows(0).Item("Rank") = mDataHandler.DataValue_Out(Rank, SqlDbType.Int)

            dsReportsProperties.Tables(0).Rows(0).Item("CompanyLogo") = False
            dsReportsProperties.Tables(0).Rows(0).Item("CompanyText") = False
            dsReportsProperties.Tables(0).Rows(0).Item("CompanyHeader") = False
            dsReportsProperties.Tables(0).Rows(0).Item("IsLandscape") = False
            dsReportsProperties.Tables(0).Rows(0).Item("IsRightToLeft") = False
            dsReportsProperties.Tables(0).Rows(0).Item("ReportHeader") = False
            dsReportsProperties.Tables(0).Rows(0).Item("ReportFooter") = False
            dsReportsProperties.Tables(0).Rows(0).Item("PageHeader") = False
            dsReportsProperties.Tables(0).Rows(0).Item("PageFooter") = False

            dsReportsProperties.Tables(0).Rows(0).Item("DataSource") = strDataSource
            'dsReportsProperties.Tables(0).Rows(0).Item("CRWName") = mDataHandler.DataValue_Out(CRWName, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderBackColor") = "White"
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderForeColor") = "Black"
            'dsReportsProperties.Tables(0).Rows(0).Item("HeaderFont") = mDataHandler.DataValue_Out(HeaderFont, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderBorderColor") = "White"
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderBorderWidth") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("ReportTopMargins") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("ReportBottomMargins") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("ReportLeftMargins") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("ReportRightMargins") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("ReportBackColor") = "White"
            dsReportsProperties.Tables(0).Rows(0).Item("ReportForeColor") = "Black"
            'dsReportsProperties.Tables(0).Rows(0).Item("ReportAlternatingColor") = mDataHandler.DataValue_Out(ReportAlternatingColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("ReportFont") = "Arial"

            dsReportsProperties.Tables(0).Rows(0).Item("ReportFontIsItalic") = "No"
            dsReportsProperties.Tables(0).Rows(0).Item("ReportFontIsBold") = "No"
            dsReportsProperties.Tables(0).Rows(0).Item("ReportFontIsUnderLine") = "No"
            dsReportsProperties.Tables(0).Rows(0).Item("ReportFontSize") = ReportFontSize

            dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontIsItalic") = "No"
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontIsBold") = "No"
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontIsUnderLine") = "No"
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontSize") = HeaderFontSize

            'dsReportsProperties.Tables(0).Rows(0).Item("RowsBorderColor") = mDataHandler.DataValue_Out(RowsBorderColor, SqlDbType.VarChar)
            dsReportsProperties.Tables(0).Rows(0).Item("BorderStyle") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderValignment") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("HeaderHalignment") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("FooterFont") = "Arial"

            dsReportsProperties.Tables(0).Rows(0).Item("FooterFontIsItalic") = "No"
            dsReportsProperties.Tables(0).Rows(0).Item("FooterFontIsBold") = "No"
            dsReportsProperties.Tables(0).Rows(0).Item("FooterFontIsUnderLine") = "No"
            dsReportsProperties.Tables(0).Rows(0).Item("FooterFontSize") = FooterFontSize

            dsReportsProperties.Tables(0).Rows(0).Item("FooterHalignment") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("FooterValignment") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("FooterBackColor") = "White"
            dsReportsProperties.Tables(0).Rows(0).Item("FooterForeColor") = "Black"
            dsReportsProperties.Tables(0).Rows(0).Item("FooterBorderColor") = "White"
            dsReportsProperties.Tables(0).Rows(0).Item("FooterBorderStyle") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("FooterBorderWidth") = 0

            dsReportsProperties.Tables(0).Rows(0).Item("ViewForm") = 0
            dsReportsProperties.Tables(0).Rows(0).Item("ScaleFactor") = 0
            '// Rows
            If dsReportsProperties.Tables(1).Rows.Count = 0 Then
                dsReportsProperties.Tables(1).Rows.Add()
            End If

            dsReportsProperties.Tables(1).Rows(0).Item("ForeColor") = "Black"
            dsReportsProperties.Tables(1).Rows(0).Item("BackColor") = "White"
            dsReportsProperties.Tables(1).Rows(0).Item("Font") = "Arial"
            dsReportsProperties.Tables(1).Rows(0).Item("FontIsItalic") = False
            dsReportsProperties.Tables(1).Rows(0).Item("FontIsBold") = False
            dsReportsProperties.Tables(1).Rows(0).Item("FontIsUnderLine") = False
            dsReportsProperties.Tables(1).Rows(0).Item("FontSize") = ReportFontSize
            dsReportsProperties.Tables(1).Rows(0).Item("RowHieght") = 20
            dsReportsProperties.Tables(1).Rows(0).Item("BorderColor") = "White"
            dsReportsProperties.Tables(1).Rows(0).Item("TopBorderColor") = "White"
            dsReportsProperties.Tables(1).Rows(0).Item("BottomBorderColor") = "White"
            dsReportsProperties.Tables(1).Rows(0).Item("LeftBorderColor") = "White"
            dsReportsProperties.Tables(1).Rows(0).Item("RightBorderColor") = "White"
            dsReportsProperties.Tables(1).Rows(0).Item("BorderStyle") = 0
            dsReportsProperties.Tables(1).Rows(0).Item("TopBorderStyle") = 0
            dsReportsProperties.Tables(1).Rows(0).Item("BottomBorderStyle") = 0
            dsReportsProperties.Tables(1).Rows(0).Item("LeftBorderStyle") = 0
            dsReportsProperties.Tables(1).Rows(0).Item("RightBorderStyle") = 0
            dsReportsProperties.Tables(1).Rows(0).Item("BorderWidth") = 0
            dsReportsProperties.Tables(1).Rows(0).Item("TopBorderWidth") = 0
            dsReportsProperties.Tables(1).Rows(0).Item("BottomBorderWidth") = 0
            dsReportsProperties.Tables(1).Rows(0).Item("LeftBorderWidth") = 0
            dsReportsProperties.Tables(1).Rows(0).Item("RightBorderWidth") = 0
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function SetReportsProperties(ByVal dsReportsProperties As Data.DataSet) As Boolean
        Try
            Dim ClsReportMain As New ClsRpw_ReportsMain(mPage)
            Dim StrTempColor As String = String.Empty
            Clear()

            If dsReportsProperties.Tables(0).Rows.Count > 0 Then
                Code = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("Code"), SqlDbType.VarChar)
                EngName = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("EngName"), SqlDbType.VarChar)
                ArbName = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ArbName"), SqlDbType.VarChar)
                EngTitle = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("EngTitle"), SqlDbType.VarChar)
                ArbTitle = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ArbTitle"), SqlDbType.VarChar)
                EngDescription = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("EngDescription"), SqlDbType.VarChar)
                ArbDescription = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ArbDescription"), SqlDbType.VarChar)
                ReportGroupID = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportGroupID"), SqlDbType.Int)
                ReportSource = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportSource"), SqlDbType.Int)
                RefreshTimeInterval = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("RefreshTimeInterval"), SqlDbType.Int)
                Rank = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("Rank"), SqlDbType.Int)
                CompanyLogo = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("CompanyLogo"), SqlDbType.Bit)
                CompanyText = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("CompanyText"), SqlDbType.Bit)
                CompanyHeader = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("CompanyHeader"), SqlDbType.Bit)
                IsLandscape = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("IsLandscape"), SqlDbType.Bit)
                IsRightToLeft = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("IsRightToLeft"), SqlDbType.Bit)
                ReportHeader = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportHeader"), SqlDbType.Bit)
                ReportFooter = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportFooter"), SqlDbType.Bit)
                PageHeader = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("PageHeader"), SqlDbType.Bit)
                PageFooter = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("PageFooter"), SqlDbType.Bit)
                DataSource = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("DataSource"), SqlDbType.VarChar)
                CRWName = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("CRWName"), SqlDbType.VarChar)
                ReportTopMargins = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportTopMargins"), SqlDbType.Int)
                ReportBottomMargins = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportBottomMargins"), SqlDbType.Int)
                ReportLeftMargins = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportLeftMargins"), SqlDbType.Int)
                ReportRightMargins = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportRightMargins"), SqlDbType.Int)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportBackColor"), SqlDbType.VarChar)
                ReportBackColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportForeColor"), SqlDbType.VarChar)
                ReportForeColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportAlternatingColor"), SqlDbType.VarChar)
                ReportAlternatingColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                ReportFont = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportFont"), SqlDbType.VarChar)
                ReportFontIsItalic = IIf(mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportFontIsItalic"), SqlDbType.VarChar) = "Yes", True, False)
                ReportFontIsBold = IIf(mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportFontIsBold"), SqlDbType.VarChar) = "Yes", True, False)
                ReportFontIsUnderLine = IIf(mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ReportFontIsUnderLine"), SqlDbType.VarChar) = "Yes", True, False)
                ReportFontSize = ClsReportMain.GetFontSizesfromStrings(dsReportsProperties.Tables(0).Rows(0).Item("ReportFontSize"))


                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderBackColor"), SqlDbType.VarChar)
                HeaderBackColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderForeColor"), SqlDbType.VarChar)
                HeaderForeColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                HeaderFont = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderFont"), SqlDbType.VarChar)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderBorderColor"), SqlDbType.VarChar)
                HeaderBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                HeaderBorderWidth = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderBorderWidth"), SqlDbType.Int)
                HeaderFontIsItalic = IIf(mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontIsItalic"), SqlDbType.VarChar) = "Yes", True, False)
                HeaderFontIsBold = IIf(mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontIsBold"), SqlDbType.VarChar) = "Yes", True, False)
                HeaderFontIsUnderLine = IIf(mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontIsUnderLine"), SqlDbType.VarChar) = "Yes", True, False)
                HeaderFontSize = ClsReportMain.GetFontSizesfromStrings(dsReportsProperties.Tables(0).Rows(0).Item("HeaderFontSize"))
                HeaderBorderStyle = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("BorderStyle"), SqlDbType.Int)
                HeaderValignment = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderValignment"), SqlDbType.Int)
                HeaderHalignment = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderHalignment"), SqlDbType.Int)
                HeaderHeight = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("HeaderHeight"), SqlDbType.Int)

                'RowsBorderColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("RowsBorderColor"), SqlDbType.VarChar)
                FooterFont = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterFont"), SqlDbType.VarChar)
                FooterFontIsItalic = IIf(mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterFontIsItalic"), SqlDbType.VarChar) = "Yes", True, False)
                FooterFontIsBold = IIf(mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterFontIsBold"), SqlDbType.VarChar) = "Yes", True, False)
                FooterFontIsUnderLine = IIf(mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterFontIsUnderLine"), SqlDbType.VarChar) = "Yes", True, False)
                FooterFontSize = ClsReportMain.GetFontSizesfromStrings(dsReportsProperties.Tables(0).Rows(0).Item("FooterFontSize"))
                FooterHalignment = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterHalignment"), SqlDbType.Int)
                FooterValignment = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterValignment"), SqlDbType.Int)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterBackColor"), SqlDbType.VarChar)
                FooterBackColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterForeColor"), SqlDbType.VarChar)
                FooterForeColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterBorderColor"), SqlDbType.VarChar)
                FooterBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                FooterBorderStyle = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterBorderStyle"), SqlDbType.Int)
                FooterBorderWidth = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterBorderWidth"), SqlDbType.Int)

                FooterHeight = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("FooterHeight"), SqlDbType.Int)
                ScaleFactor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ScaleFactor"), SqlDbType.Int)
                '===== Add ViewForm 
                ViewForm = mDataHandler.DataValue_Out(dsReportsProperties.Tables(0).Rows(0).Item("ViewForm"), SqlDbType.VarChar)
                '/// Rows
            End If
            If dsReportsProperties.Tables(1).Rows.Count > 0 Then
                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("ForeColor"), SqlDbType.VarChar)
                RowsForeColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("BackColor"), SqlDbType.VarChar)
                RowsBackColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                RowsFont = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("Font"), SqlDbType.VarChar)
                RowsFontIsBold = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("FontIsBold"), SqlDbType.Bit)
                RowsFontIsItalic = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("FontIsItalic"), SqlDbType.Bit)
                RowsFontIsUnderLine = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("FontIsUnderLine"), SqlDbType.Bit)

                RowsFontSize = ClsReportMain.GetFontSizesfromStrings(dsReportsProperties.Tables(1).Rows(0).Item("FontSize"))
                RowsHieght = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("RowHieght"), SqlDbType.Int)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("BorderColor"), SqlDbType.VarChar)
                RowsBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("TopBorderColor"), SqlDbType.VarChar)
                RowsTopBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("BottomBorderColor"), SqlDbType.VarChar)
                RowsBottomBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("LeftBorderColor"), SqlDbType.VarChar)
                RowsLeftBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                StrTempColor = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("RightBorderColor"), SqlDbType.VarChar)
                RowsRightBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                RowsBorderStyle = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("BorderStyle"), SqlDbType.Int)
                RowsTopBorderStyle = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("TopBorderStyle"), SqlDbType.Int)
                RowsBottomBorderStyle = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("BottomBorderStyle"), SqlDbType.Int)
                RowsLeftBorderStyle = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("LeftBorderStyle"), SqlDbType.Int)
                RowsRightBorderStyle = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("RightBorderStyle"), SqlDbType.Int)
                RowsBorderWidth = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("BorderWidth"), SqlDbType.Int)
                RowsTopBorderWidth = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("TopBorderWidth"), SqlDbType.Int)
                RowsBottomBorderWidth = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("BottomBorderWidth"), SqlDbType.Int)
                RowsLeftBorderWidth = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("LeftBorderWidth"), SqlDbType.Int)
                RowsRightBorderWidth = mDataHandler.DataValue_Out(dsReportsProperties.Tables(1).Rows(0).Item("RightBorderWidth"), SqlDbType.Int)

            End If

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function


    '==================================================================
    'Created by :  0256 
    'Date : 06/02/2008
    'Description:  
    'Steps: 
    '==================================================================
    Public Function GetUsersReportPermission(ByVal Code As String, ByRef Ds As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, "hrs_GetUsersReportsPermissions", Code)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    '==================================================================
    'Created by : 0256 
    'Date : 06/02/2008
    'Description:  
    'Steps: 
    '==================================================================
    Public Function GetGroupsReportPermission(ByVal Code As String, ByRef Ds As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, "hrs_GetGroupsReportsPermissions", Code)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function CreateDataSet() As DataSet
        Dim dsPosts As Data.DataSet
        dsPosts = New Data.DataSet()
        dsPosts.Tables.Add("ReportsProperties")
        dsPosts.Tables.Add("RowsProperties")
        dsPosts.Tables.Add("ColumnsProperties")

        'Add Report Properites Table
        '#####################################
        Dim dtReports As Data.DataTable
        dtReports = dsPosts.Tables(0)

        'Main Info
        dtReports.Columns.Add("ID", GetType(Integer))
        dtReports.Columns.Add("Code", GetType(String))
        dtReports.Columns.Add("EngName", GetType(String))
        dtReports.Columns.Add("ArbName", GetType(String))
        dtReports.Columns.Add("EngDescription", GetType(String))
        dtReports.Columns.Add("ArbDescription", GetType(String))
        dtReports.Columns.Add("ReportGroupID", GetType(Integer))
        dtReports.Columns.Add("ReportSource", GetType(Integer))

        'Additional Info
        dtReports.Columns.Add("ArbTitle", GetType(String))
        dtReports.Columns.Add("EngTitle", GetType(String))
        dtReports.Columns.Add("RefreshTimeInterval", GetType(Integer))
        dtReports.Columns.Add("Rank", GetType(Integer))

        'Printing Options
        dtReports.Columns.Add("CompanyLogo", GetType(Boolean))
        dtReports.Columns.Add("CompanyText", GetType(Boolean))
        dtReports.Columns.Add("CompanyHeader", GetType(Boolean))

        'Page Orientation
        dtReports.Columns.Add("IsLandscape", GetType(Boolean))
        dtReports.Columns.Add("IsRightToLeft", GetType(Boolean))
        dtReports.Columns.Add("ReportHeader", GetType(Boolean))
        dtReports.Columns.Add("ReportFooter", GetType(Boolean))
        dtReports.Columns.Add("PageHeader", GetType(Boolean))
        dtReports.Columns.Add("PageFooter", GetType(Boolean))

        'DataSouce String
        dtReports.Columns.Add("DataSource", GetType(String))
        dtReports.Columns.Add("CRWName", GetType(String))
        dtReports.Columns.Add("ViewForm", GetType(String))

        'ReportStyle 
        'CRfile
        dtReports.Columns.Add("HeaderBackColor", GetType(String))
        dtReports.Columns.Add("HeaderForeColor", GetType(String))
        dtReports.Columns.Add("HeaderFont", GetType(String))
        dtReports.Columns.Add("HeaderBorderColor", GetType(String))
        dtReports.Columns.Add("HeaderBorderWidth", GetType(Integer))
        dtReports.Columns("HeaderBorderWidth").DefaultValue = 0

        'Margins
        dtReports.Columns.Add("ReportTopMargins", GetType(Integer))
        dtReports.Columns("ReportTopMargins").DefaultValue = 0
        dtReports.Columns.Add("ReportBottomMargins", GetType(Integer))
        dtReports.Columns("ReportBottomMargins").DefaultValue = 0
        dtReports.Columns.Add("ReportLeftMargins", GetType(Integer))
        dtReports.Columns("ReportLeftMargins").DefaultValue = 0
        dtReports.Columns.Add("ReportRightMargins", GetType(Integer))
        dtReports.Columns("ReportRightMargins").DefaultValue = 0
        '

        'Colors
        dtReports.Columns.Add("ReportBackColor", GetType(String))
        dtReports.Columns.Add("ReportForeColor", GetType(String))
        dtReports.Columns.Add("ReportAlternatingColor", GetType(String))

        'Font and Font Properties
        dtReports.Columns.Add("ReportFont", GetType(String))
        dtReports.Columns.Add("ReportFontIsItalic", GetType(String))
        dtReports.Columns.Add("ReportFontIsBold", GetType(String))
        dtReports.Columns.Add("ReportFontIsUnderLine", GetType(String))
        dtReports.Columns.Add("ReportFontSize", GetType(String))
        'dtReports.Columns("ReportFontSize").DefaultValue = "Large"


        'Added Fields
        '0256 - 17-12-2007 Add the following field
        '================= [Begin]
        dtReports.Columns.Add("HeaderFontIsItalic", GetType(String))

        dtReports.Columns.Add("HeaderFontIsBold", GetType(String))
        dtReports.Columns.Add("HeaderFontIsUnderLine", GetType(String))
        dtReports.Columns.Add("HeaderFontSize", GetType(String))
        'dtReports.Columns("HeaderFontSize").DefaultValue = "Large"
        dtReports.Columns.Add("RowsBorderColor", GetType(String))

        dtReports.Columns.Add("BorderStyle", GetType(String))

        dtReports.Columns.Add("HeaderValignment", GetType(Integer))
        dtReports.Columns.Add("HeaderHalignment", GetType(Integer))


        ' 0256 - 23-12-2007 Add the following field
        '================= [Begin]
        dtReports.Columns.Add("FooterFont", GetType(String))
        dtReports.Columns.Add("FooterFontIsItalic", GetType(String))
        dtReports.Columns.Add("FooterFontIsBold", GetType(String))
        dtReports.Columns.Add("FooterFontIsUnderLine", GetType(String))
        dtReports.Columns.Add("FooterFontSize", GetType(String))
        'dtReports.Columns("FooterFontSize").DefaultValue = "Large"

        dtReports.Columns.Add("FooterValignment", GetType(Integer))
        dtReports.Columns.Add("FooterHalignment", GetType(Integer))

        dtReports.Columns.Add("FooterBackColor", GetType(String))
        dtReports.Columns.Add("FooterForeColor", GetType(String))
        dtReports.Columns.Add("FooterBorderColor", GetType(String))
        dtReports.Columns.Add("FooterBorderWidth", GetType(Integer))
        dtReports.Columns.Add("FooterBorderStyle", GetType(String))
        '
        '================= [End]

        ' 0256 - 30-12-2007 Add the following field
        '===================== [Begin]
        dtReports.Columns.Add("HeaderHeight", GetType(Integer))
        dtReports.Columns.Add("FooterHeight", GetType(Integer))
        '===================== [End]
        dtReports.Columns.Add("ScaleFactor", GetType(Integer))


        dtReports.Columns.Add("HeaderBorderColorLeft", GetType(String))
        dtReports.Columns.Add("HeaderBorderColorRight", GetType(String))
        dtReports.Columns.Add("HeaderBorderColorTop", GetType(String))
        dtReports.Columns.Add("HeaderBorderColorBottom", GetType(String))

        dtReports.Columns.Add("HeaderStyleTop", GetType(String))
        dtReports.Columns.Add("HeaderStyleBottom", GetType(String))
        dtReports.Columns.Add("HeaderStyleLeft", GetType(String))
        dtReports.Columns.Add("HeaderStyleRight", GetType(String))

        dtReports.Columns.Add("HeaderBorderWidthTop", GetType(Integer))
        dtReports.Columns.Add("HeaderBorderWidthBottom", GetType(Integer))
        dtReports.Columns.Add("HeaderBorderWidthRight", GetType(Integer))
        dtReports.Columns.Add("HeaderBorderWidthLeft", GetType(Integer))


        dtReports.Columns.Add("FooterBorderColorLeft", GetType(String))
        dtReports.Columns.Add("FooterBorderColorRight", GetType(String))
        dtReports.Columns.Add("FooterBorderColorTop", GetType(String))
        dtReports.Columns.Add("FooterBorderColorBottom", GetType(String))

        dtReports.Columns.Add("FooterStyleTop", GetType(String))
        dtReports.Columns.Add("FooterStyleBottom", GetType(String))
        dtReports.Columns.Add("FooterStyleLeft", GetType(String))
        dtReports.Columns.Add("FooterStyleRight", GetType(String))

        dtReports.Columns.Add("FooterBorderWidthTop", GetType(Integer))
        dtReports.Columns.Add("FooterBorderWidthBottom", GetType(Integer))
        dtReports.Columns.Add("FooterBorderWidthRight", GetType(Integer))
        dtReports.Columns.Add("FooterBorderWidthLeft", GetType(Integer))




        'Add Rows Properites Table
        '#####################################

        Dim dtRows As Data.DataTable
        dtRows = dsPosts.Tables(1)
        'Main Info
        dtRows.Columns.Add("ForeColor", GetType(String))
        dtRows.Columns.Add("BackColor", GetType(String))
        dtRows.Columns.Add("Font", GetType(String))

        dtRows.Columns.Add("FontIsItalic", GetType(Boolean))
        dtRows.Columns("FontIsItalic").DefaultValue = False
        dtRows.Columns.Add("FontIsBold", GetType(Boolean))
        dtRows.Columns("FontIsBold").DefaultValue = False
        dtRows.Columns.Add("FontIsUnderLine", GetType(Boolean))
        dtRows.Columns("FontIsUnderLine").DefaultValue = False
        dtRows.Columns.Add("FontSize", GetType(String))
        '
        dtRows.Columns.Add("RowHieght", GetType(Integer))
        dtRows.Columns("RowHieght").DefaultValue = 20

        'Border Colors and styles
        '=================================================
        dtRows.Columns.Add("BorderColor", GetType(String))
        dtRows.Columns.Add("BorderStyle", GetType(Integer))
        dtRows.Columns("BorderStyle").DefaultValue = 0
        dtRows.Columns.Add("BorderWidth", GetType(Integer))
        dtRows.Columns("BorderWidth").DefaultValue = 0


        'Border Corners Colors
        dtRows.Columns.Add("TopBorderColor", GetType(String))
        dtRows.Columns.Add("BottomBorderColor", GetType(String))
        dtRows.Columns.Add("LeftBorderColor", GetType(String))
        dtRows.Columns.Add("RightBorderColor", GetType(String))

        'Border Style
        dtRows.Columns.Add("TopBorderStyle", GetType(Integer))
        dtRows.Columns.Add("BottomBorderStyle", GetType(Integer))
        dtRows.Columns.Add("LeftBorderStyle", GetType(Integer))
        dtRows.Columns.Add("RightBorderStyle", GetType(Integer))

        'Border Width
        dtRows.Columns.Add("TopBorderWidth", GetType(Integer))
        dtRows.Columns("TopBorderWidth").DefaultValue = 0
        dtRows.Columns.Add("BottomBorderWidth", GetType(Integer))
        dtRows.Columns("BottomBorderWidth").DefaultValue = 0
        dtRows.Columns.Add("LeftBorderWidth", GetType(Integer))
        dtRows.Columns("LeftBorderWidth").DefaultValue = 0
        dtRows.Columns.Add("RightBorderWidth", GetType(Integer))
        dtRows.Columns("RightBorderWidth").DefaultValue = 0

        dtRows.Columns.Add("RowWidth", GetType(Integer))
        dtRows.Columns.Add("RowPadding", GetType(Integer))
        dtRows.Columns.Add("RowMargin", GetType(Integer))
        dtRows.Columns.Add("RowVerticalAlignment", GetType(Integer))
        dtRows.Columns.Add("RowHorizontalAlignment", GetType(Integer))




        '======================

        Dim dtColumns As Data.DataTable
        dtColumns = dsPosts.Tables(2)

        dtColumns.Columns.Add(New Data.DataColumn("ColumnIndex", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("BorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("TopBorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("BottomBorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("LeftBorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("RightBorderColor", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColumnName", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("GroupByRank", GetType(Integer)))


        'Border Style
        dtColumns.Columns.Add(New Data.DataColumn("BorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("TopBorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("BottomBorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("LeftBorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("RightBorderStyle", GetType(String)))

        'Border Width
        dtColumns.Columns.Add(New Data.DataColumn("BorderWidth", GetType(Integer)))
        dtColumns.Columns("BorderWidth").DefaultValue = 0

        dtColumns.Columns.Add(New Data.DataColumn("TopBorderWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("BottomBorderWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("LeftBorderWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("RightBorderWidth", GetType(Integer)))

        dtColumns.Columns("TopBorderWidth").DefaultValue = 0
        dtColumns.Columns("BottomBorderWidth").DefaultValue = 0
        dtColumns.Columns("LeftBorderWidth").DefaultValue = 0
        dtColumns.Columns("RightBorderWidth").DefaultValue = 0




        'Columns Captions 
        dtColumns.Columns.Add(New Data.DataColumn("EngHeaderCaption", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("EngFooterCaption", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ArbHeaderCaption", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ArbFooterCaption", GetType(String)))

        '
        dtColumns.Columns.Add(New Data.DataColumn("ColumnWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnValignment", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnHalignment", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnIsHidden", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnIsWrap", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnIsOverView", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnForeColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnBackColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFont", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFontIsBold", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFontIsItalic", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFontIsUnderLine", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFontSize", GetType(Integer)))
        'Pading
        dtColumns.Columns.Add(New Data.DataColumn("TopPadding", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("BottomPadding", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("LeftPadding", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("RightPadding", GetType(Integer)))
        dtColumns.Columns("TopPadding").DefaultValue = 0
        dtColumns.Columns("BottomPadding").DefaultValue = 0
        dtColumns.Columns("LeftPadding").DefaultValue = 0
        dtColumns.Columns("RightPadding").DefaultValue = 0
        'Marging
        dtColumns.Columns.Add(New Data.DataColumn("TopMargin", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("BottomMargin", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("LeftMargin", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("RightMargin", GetType(Integer)))
        dtColumns.Columns("TopMargin").DefaultValue = 0
        dtColumns.Columns("BottomMargin").DefaultValue = 0
        dtColumns.Columns("LeftMargin").DefaultValue = 0
        dtColumns.Columns("RightMargin").DefaultValue = 0
        dtColumns.Columns.Add(New Data.DataColumn("FooterTotal", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("Format", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("IsSorted", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("IsGroupBy", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("DefaultValue", GetType(Object)))
        dtColumns.Columns.Add(New Data.DataColumn("Iscalculated", GetType(Boolean)))
        dtColumns.Columns("Iscalculated").DefaultValue = False
        dtColumns.Columns.Add(New Data.DataColumn("IsCanceled", GetType(Boolean)))
        dtColumns.Columns("IsCanceled").DefaultValue = False
        dtColumns.Columns.Add(New Data.DataColumn("ID", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ReportID", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("Formula", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnHeaderHalignment", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnHeadervalignment", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("FieldLanguage", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("DataType", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBackColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderForeColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFont", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontSize", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontIsItalic", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontIsBold", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontIsUnderline", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontIsStrikeOut", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontIsOverLine", GetType(Boolean)))

        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderWidthTop", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderWidthBottom", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderWidthLeft", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderWidthRight", GetType(Integer)))


        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderStyleTop", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderStyleBottom", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderStyleLeft", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderStyleRight", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderColorTop", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderColorBottom", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderColorLeft", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderColorRight", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderPaddingLeft", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderPaddingRight", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderPaddingTop", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderPaddingBottom", GetType(Integer)))

        'Footer

        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBackColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterForeColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFont", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontSize", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontIsItalic", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontIsBold", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontIsUnderline", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontIsStrikeOut", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontIsOverLine", GetType(Boolean)))

        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderWidthTop", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderWidthBottom", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderWidthLeft", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderWidthRight", GetType(Integer)))


        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderStyleTop", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderStyleBottom", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderStyleLeft", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderStyleRight", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderColorTop", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderColorBottom", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderColorLeft", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderColorRight", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColFooterPaddingLeft", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterPaddingRight", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterPaddingTop", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterPaddingBottom", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFooterHalignment", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFootervalignment", GetType(Integer)))

        Return dsPosts

    End Function


    Public Function CreateDataSet4Designer() As DataSet
        Const CsForeColor = "Black"
        Const CsBackColor = "White"
        Const CsFont = "Arial"
        Const csReportWidth = 800
        Const csFontSizeDefault = 12
        Const csHeight = 25

        Dim dsPosts As Data.DataSet
        dsPosts = New Data.DataSet()
        dsPosts.Tables.Add("ReportsProperties")
        dsPosts.Tables.Add("RowsProperties")
        'Add Report Properites Table
        '#####################################
        Dim dtReports As Data.DataTable
        dtReports = dsPosts.Tables(0)

        'Main Info
        dtReports.Columns.Add("ID", GetType(Integer))
        dtReports.Columns.Add("Code", GetType(String))
        dtReports.Columns.Add("EngName", GetType(String))
        dtReports.Columns.Add("ArbName", GetType(String))
        dtReports.Columns.Add("EngDescription", GetType(String))
        dtReports.Columns.Add("ArbDescription", GetType(String))
        dtReports.Columns.Add("ReportGroupID", GetType(Integer))
        dtReports.Columns.Add("ReportSource", GetType(Integer))

        'Additional Info
        dtReports.Columns.Add("ArbTitle", GetType(String))
        dtReports.Columns.Add("EngTitle", GetType(String))
        dtReports.Columns.Add("RefreshTimeInterval", GetType(Integer))
        dtReports.Columns.Add("Rank", GetType(Integer))

        'Printing Options
        dtReports.Columns.Add("CompanyLogo", GetType(Boolean))
        dtReports.Columns.Add("CompanyText", GetType(Boolean))
        dtReports.Columns.Add("CompanyHeader", GetType(Boolean))

        'Page Orientation
        dtReports.Columns.Add("IsLandscape", GetType(Boolean))
        dtReports.Columns.Add("IsRightToLeft", GetType(Boolean))
        dtReports.Columns.Add("ReportHeader", GetType(Boolean))
        dtReports.Columns.Add("ReportFooter", GetType(Boolean))
        dtReports.Columns.Add("PageHeader", GetType(Boolean))
        dtReports.Columns.Add("PageFooter", GetType(Boolean))

        'DataSouce String
        dtReports.Columns.Add("DataSource", GetType(String))
        dtReports.Columns.Add("CRWName", GetType(String))
        dtReports.Columns.Add("ViewForm", GetType(String))

        'ReportStyle 
        'CRfile
        dtReports.Columns.Add("HeaderBackColor", GetType(String))
        dtReports.Columns.Add("HeaderForeColor", GetType(String))
        dtReports.Columns.Add("HeaderFont", GetType(String))
        dtReports.Columns.Add("HeaderBorderColor", GetType(String))
        dtReports.Columns.Add("HeaderBorderWidth", GetType(Integer))
        dtReports.Columns("HeaderBorderWidth").DefaultValue = 0

        'Margins
        dtReports.Columns.Add("ReportTopMargins", GetType(Integer))
        dtReports.Columns("ReportTopMargins").DefaultValue = 0
        dtReports.Columns.Add("ReportBottomMargins", GetType(Integer))
        dtReports.Columns("ReportBottomMargins").DefaultValue = 0
        dtReports.Columns.Add("ReportLeftMargins", GetType(Integer))
        dtReports.Columns("ReportLeftMargins").DefaultValue = 0
        dtReports.Columns.Add("ReportRightMargins", GetType(Integer))
        dtReports.Columns("ReportRightMargins").DefaultValue = 0
        '

        'Colors
        dtReports.Columns.Add("ReportBackColor", GetType(String))
        dtReports.Columns.Add("ReportForeColor", GetType(String))
        dtReports.Columns.Add("ReportAlternatingColor", GetType(String))

        'Font and Font Properties
        dtReports.Columns.Add("ReportFont", GetType(String))
        dtReports.Columns.Add("ReportFontIsItalic", GetType(String))
        dtReports.Columns.Add("ReportFontIsBold", GetType(String))
        dtReports.Columns.Add("ReportFontIsUnderLine", GetType(String))
        dtReports.Columns.Add("ReportFontSize", GetType(String))
        dtReports.Columns("ReportFontSize").DefaultValue = "12"


        'Added Fields
        ' 0256 - 17-12-2007 Add the following field
        '================= [Begin]
        dtReports.Columns.Add("HeaderFontIsItalic", GetType(String))

        dtReports.Columns.Add("HeaderFontIsBold", GetType(String))
        dtReports.Columns.Add("HeaderFontIsUnderLine", GetType(String))
        dtReports.Columns.Add("HeaderFontSize", GetType(String))
        dtReports.Columns("HeaderFontSize").DefaultValue = "12"
        dtReports.Columns.Add("RowsBorderColor", GetType(String))

        dtReports.Columns.Add("BorderStyle", GetType(String))

        dtReports.Columns.Add("HeaderValignment", GetType(Integer))
        dtReports.Columns.Add("HeaderHalignment", GetType(Integer))


        ' 0256 - 23-12-2007 Add the following field
        '================= [Begin]
        dtReports.Columns.Add("FooterFont", GetType(String))
        dtReports.Columns.Add("FooterFontIsItalic", GetType(String))
        dtReports.Columns.Add("FooterFontIsBold", GetType(String))
        dtReports.Columns.Add("FooterFontIsUnderLine", GetType(String))
        dtReports.Columns.Add("FooterFontSize", GetType(String))
        dtReports.Columns("FooterFontSize").DefaultValue = "12"

        dtReports.Columns.Add("FooterValignment", GetType(Integer))
        dtReports.Columns.Add("FooterHalignment", GetType(Integer))

        dtReports.Columns.Add("FooterBackColor", GetType(String))
        dtReports.Columns.Add("FooterForeColor", GetType(String))
        dtReports.Columns.Add("FooterBorderColor", GetType(String))
        dtReports.Columns.Add("FooterBorderWidth", GetType(Integer))
        dtReports.Columns.Add("FooterBorderStyle", GetType(String))
        '
        '================= [End]

        ' 0256 - 30-12-2007 Add the following field
        '===================== [Begin]
        dtReports.Columns.Add("HeaderHeight", GetType(Integer))
        dtReports.Columns.Add("FooterHeight", GetType(Integer))
        '===================== [End]
        dtReports.Columns.Add("ScaleFactor", GetType(Integer))


        dtReports.Columns.Add("HeaderBorderColorLeft", GetType(String))
        dtReports.Columns.Add("HeaderBorderColorRight", GetType(String))
        dtReports.Columns.Add("HeaderBorderColorTop", GetType(String))
        dtReports.Columns.Add("HeaderBorderColorBottom", GetType(String))

        dtReports.Columns.Add("HeaderStyleTop", GetType(String))
        dtReports.Columns.Add("HeaderStyleBottom", GetType(String))
        dtReports.Columns.Add("HeaderStyleLeft", GetType(String))
        dtReports.Columns.Add("HeaderStyleRight", GetType(String))

        dtReports.Columns.Add("HeaderBorderWidthTop", GetType(Integer))
        dtReports.Columns.Add("HeaderBorderWidthBottom", GetType(Integer))
        dtReports.Columns.Add("HeaderBorderWidthRight", GetType(Integer))
        dtReports.Columns.Add("HeaderBorderWidthLeft", GetType(Integer))


        dtReports.Columns.Add("FooterBorderColorLeft", GetType(String))
        dtReports.Columns.Add("FooterBorderColorRight", GetType(String))
        dtReports.Columns.Add("FooterBorderColorTop", GetType(String))
        dtReports.Columns.Add("FooterBorderColorBottom", GetType(String))

        dtReports.Columns.Add("FooterStyleTop", GetType(String))
        dtReports.Columns.Add("FooterStyleBottom", GetType(String))
        dtReports.Columns.Add("FooterStyleLeft", GetType(String))
        dtReports.Columns.Add("FooterStyleRight", GetType(String))

        dtReports.Columns.Add("FooterBorderWidthTop", GetType(Integer))
        dtReports.Columns.Add("FooterBorderWidthBottom", GetType(Integer))
        dtReports.Columns.Add("FooterBorderWidthRight", GetType(Integer))
        dtReports.Columns.Add("FooterBorderWidthLeft", GetType(Integer))

        'Add Rows Properites Table
        '#####################################

        Dim dtRows As Data.DataTable
        dtRows = dsPosts.Tables(1)
        'Main Info
        dtRows.Columns.Add("ForeColor", GetType(String))
        dtRows.Columns("ForeColor").DefaultValue = CsForeColor
        '----------------------------------------------
        dtRows.Columns.Add("BackColor", GetType(String))
        dtRows.Columns("BackColor").DefaultValue = CsBackColor
        '----------------------------------------------
        dtRows.Columns.Add("Font", GetType(String))
        dtRows.Columns("Font").DefaultValue = CsFont
        '----------------------------------------------
        dtRows.Columns.Add("FontIsItalic", GetType(Boolean))
        dtRows.Columns("FontIsItalic").DefaultValue = False
        '----------------------------------------------
        dtRows.Columns.Add("FontIsBold", GetType(Boolean))
        dtRows.Columns("FontIsBold").DefaultValue = False
        '----------------------------------------------
        dtRows.Columns.Add("FontIsUnderLine", GetType(Boolean))
        dtRows.Columns("FontIsUnderLine").DefaultValue = False
        '----------------------------------------------
        dtRows.Columns.Add("FontSize", GetType(String))
        dtRows.Columns("FontSize").DefaultValue = csFontSizeDefault
        '----------------------------------------------
        dtRows.Columns.Add("RowHieght", GetType(Integer))
        dtRows.Columns("RowHieght").DefaultValue = 20
        '----------------------------------------------
        'Border Colors and styles
        '=================================================
        dtRows.Columns.Add("BorderColor", GetType(String))
        dtRows.Columns("BorderColor").DefaultValue = CsBackColor
        '----------------------------------------------
        dtRows.Columns.Add("BorderStyle", GetType(Integer))
        dtRows.Columns("BorderStyle").DefaultValue = GetBorderStyleIntValue("Default")
        '----------------------------------------------
        dtRows.Columns.Add("BorderWidth", GetType(Integer))
        dtRows.Columns("BorderWidth").DefaultValue = 0
        '----------------------------------------------
        'Border Corners Colors
        dtRows.Columns.Add("TopBorderColor", GetType(String))
        dtRows.Columns("TopBorderColor").DefaultValue = CsBackColor
        '----------------------------------------------
        dtRows.Columns.Add("BottomBorderColor", GetType(String))
        dtRows.Columns("BottomBorderColor").DefaultValue = CsBackColor
        '----------------------------------------------
        dtRows.Columns.Add("LeftBorderColor", GetType(String))
        dtRows.Columns("LeftBorderColor").DefaultValue = CsBackColor
        '----------------------------------------------
        dtRows.Columns.Add("RightBorderColor", GetType(String))
        dtRows.Columns("RightBorderColor").DefaultValue = CsBackColor
        '----------------------------------------------
        'Border Style
        dtRows.Columns.Add("TopBorderStyle", GetType(Integer))
        dtRows.Columns("TopBorderStyle").DefaultValue = GetBorderStyleIntValue("Default")
        '----------------------------------------------
        dtRows.Columns.Add("BottomBorderStyle", GetType(Integer))
        dtRows.Columns("BottomBorderStyle").DefaultValue = GetBorderStyleIntValue("Default")
        '----------------------------------------------
        dtRows.Columns.Add("LeftBorderStyle", GetType(Integer))
        dtRows.Columns("LeftBorderStyle").DefaultValue = GetBorderStyleIntValue("Default")
        '----------------------------------------------
        dtRows.Columns.Add("RightBorderStyle", GetType(Integer))
        dtRows.Columns("RightBorderStyle").DefaultValue = GetBorderStyleIntValue("Default")
        '----------------------------------------------
        'Border Width
        dtRows.Columns.Add("TopBorderWidth", GetType(Integer))
        dtRows.Columns("TopBorderWidth").DefaultValue = 0
        '----------------------------------------------
        dtRows.Columns.Add("BottomBorderWidth", GetType(Integer))
        dtRows.Columns("BottomBorderWidth").DefaultValue = 0
        '----------------------------------------------
        dtRows.Columns.Add("LeftBorderWidth", GetType(Integer))
        dtRows.Columns("LeftBorderWidth").DefaultValue = 0
        '----------------------------------------------
        dtRows.Columns.Add("RightBorderWidth", GetType(Integer))
        dtRows.Columns("RightBorderWidth").DefaultValue = 0
        '----------------------------------------------
        dtRows.Columns.Add("RowWidth", GetType(Integer))
        '----------------------------------------------
        dtRows.Columns.Add("RowPadding", GetType(Integer))
        '----------------------------------------------
        dtRows.Columns.Add("RowMargin", GetType(Integer))
        '----------------------------------------------
        dtRows.Columns.Add("RowVerticalAlignment", GetType(Integer))
        '----------------------------------------------
        dtRows.Columns.Add("RowHorizontalAlignment", GetType(Integer))
        Return dsPosts

    End Function

    Public Function GetDefaultRowsValues(ByVal strFieldName As String, ByVal StrSQLname As String, ByVal strDatatype As String, ByRef dtFieldsDataTable As Data.DataTable, ByVal blnIsView As Boolean) As Boolean
        Dim objNewRow As Data.DataRow
        Const CsForeColor = "Black"
        Const CsBackColor = "White"
        Const CsFont = "Arial"
        Const csReportWidth = 800
        Const csFontSizeDefault = 12
        Const csHeight = 25
        Try

            objNewRow = dtFieldsDataTable.NewRow()
            objNewRow.Item("ID") = 0
            objNewRow.Item("Rank") = ""
            objNewRow.Item("ReportID") = 0
            objNewRow.Item("FieldName") = strFieldName
            objNewRow.Item("DataType") = strDatatype
            objNewRow.Item("EngDescription") = strFieldName
            objNewRow.Item("ArbDescription") = strFieldName
            objNewRow.Item("Status") = False
            objNewRow.Item("FieldLanguage") = 3
            If blnIsView Then
                objNewRow.Item("IsCalculated") = False
                '---------------------------------------------------------------------------
                objNewRow.Item("ColFooterSummaryType") = ""
                objNewRow.Item("ColHeaderSorting") = 0
                objNewRow.Item("BorderColor") = ""
                objNewRow.Item("TopBorderColor") = ""
                objNewRow.Item("BottomBorderColor") = ""
                objNewRow.Item("LeftBorderColor") = ""
                objNewRow.Item("RightBorderColor") = ""
                objNewRow.Item("BorderStyle") = GetBorderStyleIntValue("Default")
                objNewRow.Item("TopBorderStyle") = 0
                objNewRow.Item("BottomBorderStyle") = 0
                objNewRow.Item("LeftBorderStyle") = 0
                objNewRow.Item("RightBorderStyle") = 0
                objNewRow.Item("BorderWidth") = 0
                objNewRow.Item("TopBorderWidth") = 0
                objNewRow.Item("BottomBorderWidth") = 0
                objNewRow.Item("LeftBorderWidth") = 0
                objNewRow.Item("RightBorderWidth") = 0
                objNewRow.Item("EngFooterCaption") = ""
                objNewRow.Item("ArbFooterCaption") = ""
                objNewRow.Item("ColumnWidth") = 0
                objNewRow.Item("ColumnHalignment") = 0
                objNewRow.Item("ColumnValignment") = 0
                objNewRow.Item("ColumnIsHidden") = False
                objNewRow.Item("ColumnIsOverView") = False
                objNewRow.Item("ColumnIsWrap") = False
                objNewRow.Item("ColumnForeColor") = CsForeColor
                objNewRow.Item("ColumnBackColor") = CsBackColor
                objNewRow.Item("ColumnFont") = CsFont
                objNewRow.Item("ColumnFontIsBold") = False
                objNewRow.Item("ColumnFontIsItalic") = False
                objNewRow.Item("ColumnFontIsUnderLine") = False
                objNewRow.Item("ColumnFontSize") = csFontSizeDefault
                objNewRow.Item("TopPadding") = 0
                objNewRow.Item("BottomPadding") = 0
                objNewRow.Item("LeftPadding") = 0
                objNewRow.Item("RightPadding") = 0
                objNewRow.Item("TopMargin") = 0
                objNewRow.Item("BottomMargin") = 0
                objNewRow.Item("LeftMargin") = 0
                objNewRow.Item("RightMargin") = 0
                objNewRow.Item("Format") = ""
                objNewRow.Item("FooterTotal") = ""
                objNewRow.Item("IsSorted") = 0
                objNewRow.Item("IsGroupBy") = False
                objNewRow.Item("Formula") = ""
                objNewRow.Item("ColumnHeaderHalignment") = 0
                objNewRow.Item("ColumnHeaderValignment") = 0
                objNewRow.Item("ColHeaderBackColor") = CsBackColor
                objNewRow.Item("ColHeaderForeColor") = CsForeColor
                objNewRow.Item("ColHeaderFont") = CsFont
                objNewRow.Item("ColHeaderFontIsBold") = False
                objNewRow.Item("ColHeaderFontIsItalic") = False
                objNewRow.Item("ColHeaderFontIsUnderline") = False
                objNewRow.Item("ColHeaderFontSize") = csFontSizeDefault
                objNewRow.Item("ColHeaderBorderWidth") = 0
                objNewRow.Item("ColHeaderBorderWidthTop") = 0
                objNewRow.Item("ColHeaderBorderWidthBottom") = 0
                objNewRow.Item("ColHeaderBorderWidthLeft") = 0
                objNewRow.Item("ColHeaderBorderWidthRight") = 0
                objNewRow.Item("ColHeaderBorderStyle") = GetBorderStyleIntValue("Default")
                objNewRow.Item("ColHeaderBorderStyleTop") = 0
                objNewRow.Item("ColHeaderBorderStyleBottom") = 0
                objNewRow.Item("ColHeaderBorderStyleRight") = 0
                objNewRow.Item("ColHeaderBorderStyleLeft") = 0
                objNewRow.Item("ColHeaderBorderColor") = ""
                objNewRow.Item("ColHeaderBorderColorTop") = ""
                objNewRow.Item("ColHeaderBorderColorBottom") = ""
                objNewRow.Item("ColHeaderBorderColorLeft") = ""
                objNewRow.Item("ColHeaderBorderColorRight") = ""
                objNewRow.Item("ColHeaderPaddingLeft") = 0
                objNewRow.Item("ColHeaderPaddingRight") = 0
                objNewRow.Item("ColHeaderPaddingTop") = 0
                objNewRow.Item("ColHeaderPaddingBottom") = 0
                objNewRow.Item("ColFooterBackColor") = CsBackColor
                objNewRow.Item("ColFooterForeColor") = CsForeColor
                objNewRow.Item("ColFooterFont") = CsFont
                objNewRow.Item("ColFooterFontIsBold") = False
                objNewRow.Item("ColFooterFontIsItalic") = False
                objNewRow.Item("ColFooterFontIsUnderline") = False
                objNewRow.Item("ColFooterFontSize") = csFontSizeDefault
                objNewRow.Item("ColFooterBorderWidth") = 0
                objNewRow.Item("ColFooterBorderWidthTop") = 0
                objNewRow.Item("ColFooterBorderWidthBottom") = 0
                objNewRow.Item("ColFooterBorderWidthLeft") = 0
                objNewRow.Item("ColFooterBorderWidthRight") = 0
                objNewRow.Item("ColFooterBorderStyle") = GetBorderStyleIntValue("Default")
                objNewRow.Item("ColFooterBorderStyleTop") = 0
                objNewRow.Item("ColFooterBorderStyleBottom") = 0
                objNewRow.Item("ColFooterBorderStyleRight") = 0
                objNewRow.Item("ColFooterBorderStyleLeft") = 0
                objNewRow.Item("ColFooterBorderColor") = ""
                objNewRow.Item("ColFooterBorderColorTop") = ""
                objNewRow.Item("ColFooterBorderColorBottom") = ""
                objNewRow.Item("ColFooterBorderColorLeft") = ""
                objNewRow.Item("ColFooterBorderColorRight") = ""
                objNewRow.Item("ColFooterPaddingLeft") = 0
                objNewRow.Item("ColFooterPaddingRight") = 0
                objNewRow.Item("ColFooterPaddingTop") = 0
                objNewRow.Item("ColFooterPaddingBottom") = 0
                objNewRow.Item("ColumnFooterValignment") = 0
                objNewRow.Item("ColumnFooterHalignment") = 0
                '---------------------------------------------------------------------------
            Else
                objNewRow.Item("IsCriteria") = True
                objNewRow.Item("Length") = 0
                objNewRow.Item("DefaultValue") = ""
                objNewRow.Item("Operation") = 0
                objNewRow.Item("MaximumValue") = ""
                objNewRow.Item("MinimumValue") = ""
                objNewRow.Item("SearchID") = 0
                objNewRow.Item("SQLname") = StrSQLname
                objNewRow.Item("FieldEngDefaultSet") = 0
                objNewRow.Item("FieldArbDefaultSet") = 0
                objNewRow.Item("FieldDefaultSetValues") = 0
                objNewRow.Item("DefinedTablesId") = 0
                objNewRow.Item("DefinedColumnsId") = 0
                objNewRow.Item("DefinedValueId") = 0
            End If
            dtFieldsDataTable.Rows.Add(objNewRow)
        Catch ex As Exception

        End Try
    End Function

    Public Function GenerateFieldsDs(ByRef dtFieldsDataTable As Data.DataTable, ByVal intReportID As Integer, ByVal blnIsView As Boolean) As Boolean
        Dim TempDS As New Data.DataSet()
        Dim strQuery As String = String.Empty
        Try
            If blnIsView Then
                strQuery = CONFIG_DATEFORMAT
                strQuery &= "  Select ID,IsCalculated,Rank,ReportID,BorderColor,TopBorderColor,BottomBorderColor,LeftBorderColor,RightBorderColor,BorderStyle,TopBorderStyle," & _
                        "BottomBorderStyle,LeftBorderStyle,RightBorderStyle,BorderWidth,TopBorderWidth,BottomBorderWidth,LeftBorderWidth,RightBorderWidth," & _
                        "EngHeaderCaption As EngDescription,EngFooterCaption,ArbHeaderCaption As ArbDescription,ArbFooterCaption,ColumnName As FieldName,ColumnWidth,ColumnValignment,ColumnHalignment,ColumnIsHidden," & _
                        "ColumnIsWrap,ColumnIsOverView,ColumnForeColor,ColumnBackColor,ColumnFont,ColumnFontIsBold,ColumnFontIsItalic,ColumnFontIsUnderLine,ColumnFontSize," & _
                        "TopPadding,BottomPadding,LeftPadding,RightPadding,TopMargin,BottomMargin,LeftMargin,RightMargin,Format,FooterTotal,IsSorted,IsGroupBy,Formula ,GroupByRank,ColumnHeaderHalignment,ColumnHeaderValignment,FieldLanguage," & _
                        "ColHeaderBackColor,ColHeaderForeColor,ColHeaderFont,ColHeaderFontSize,ColHeaderFontIsItalic,ColHeaderFontIsBold,ColHeaderFontIsUnderline , " & _
                        "ColHeaderBorderWidth,ColHeaderBorderStyle,ColHeaderBorderColor,ColHeaderFontIsStrikeOut,ColHeaderFontIsOverLine,ColHeaderPaddingLeft, " & _
                        "ColHeaderPaddingRight,ColHeaderPaddingTop,ColHeaderPaddingBottom,Ucode," & _
                        "ColFooterBackColor,ColFooterForeColor,ColFooterFont,ColFooterFontSize,ColFooterFontIsItalic,ColFooterFontIsBold,ColFooterFontIsUnderline , " & _
                        "ColFooterBorderWidth,ColFooterBorderStyle,ColFooterBorderColor,ColFooterFontIsStrikeOut,ColFooterFontIsOverLine,ColFooterPaddingLeft, " & _
                        "ColFooterPaddingRight,ColFooterPaddingTop,ColFooterPaddingBottom,ColumnFooterHalignment,ColumnFooterValignment, " & _
                        "ColHeaderBorderStyleTop,ColHeaderBorderStyleBottom,ColHeaderBorderStyleLeft,ColHeaderBorderStyleRight, " & _
                        "ColHeaderBorderColorTop,ColHeaderBorderColorBottom,ColHeaderBorderColorLeft,ColHeaderBorderColorRight, " & _
                        "ColHeaderBorderWidthTop,ColHeaderBorderWidthBottom,ColHeaderBorderWidthLeft,ColHeaderBorderWidthRight, " & _
                        "ColFooterBorderStyleTop,ColFooterBorderStyleBottom,ColFooterBorderStyleLeft,ColFooterBorderStyleRight, " & _
                        "ColFooterBorderColorTop,ColFooterBorderColorBottom,ColFooterBorderColorLeft,ColFooterBorderColorRight, " & _
                        "ColFooterBorderWidthTop,ColFooterBorderWidthBottom,ColFooterBorderWidthLeft,ColFooterBorderWidthRight ,Status ,ColFooterSummaryType,ColHeaderSorting ,DataType "
                strQuery &= " From rpw_ReportsColumnsProperties "
            Else
                strQuery = CONFIG_DATEFORMAT & " Select * "
                strQuery &= " From rpw_ReportsCriteriasProperties "
            End If
            strQuery &= " Where ReportID =" & intReportID
            strQuery &= " Order by Rank "
            TempDS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, Data.CommandType.Text, strQuery)

            Dim dColFieldname As New Data.DataColumn
            '--------------------------------
            If Not blnIsView Then
                CreateCriteriaDataTable(dtFieldsDataTable)
            Else
                CreateViewDataTable(dtFieldsDataTable)
            End If

            GetRowsValues(TempDS, dtFieldsDataTable, blnIsView)
        Catch ex As Exception
        End Try
    End Function

    Public Function AddNewRowToSeesion(ByRef dtFieldsDataTable As Data.DataTable, ByVal blnIsView As Boolean, ByVal intReportId As Integer, ByVal strFieldName As String, ByVal strDataType As String)
        Dim objNewRow As Data.DataRow
        Const CsForeColor = "Black"
        Const CsBackColor = "White"
        Const CsFont = "Arial"
        Const csReportWidth = 800
        Const csFontSizeDefault = 12
        Const csHeight = 25
        Try
            objNewRow = dtFieldsDataTable.NewRow()
            objNewRow.Item("ID") = 0
            objNewRow.Item("Rank") = 0
            objNewRow.Item("ReportID") = intReportId
            objNewRow.Item("FieldName") = strFieldName
            objNewRow.Item("DataType") = strDataType
            objNewRow.Item("EngDescription") = strFieldName
            objNewRow.Item("ArbDescription") = strFieldName
            objNewRow.Item("Status") = True
            objNewRow.Item("FieldLanguage") = 3
            If blnIsView Then
                objNewRow.Item("IsCalculated") = False
                '---------------------------------------------------------------------------
                objNewRow.Item("ColFooterSummaryType") = ""
                objNewRow.Item("ColHeaderSorting") = 0
                objNewRow.Item("BorderColor") = ""
                objNewRow.Item("TopBorderColor") = ""
                objNewRow.Item("BottomBorderColor") = ""
                objNewRow.Item("LeftBorderColor") = ""
                objNewRow.Item("RightBorderColor") = ""
                objNewRow.Item("BorderStyle") = GetBorderStyleIntValue("Default")
                objNewRow.Item("TopBorderStyle") = 0
                objNewRow.Item("BottomBorderStyle") = 0
                objNewRow.Item("LeftBorderStyle") = 0
                objNewRow.Item("RightBorderStyle") = 0
                objNewRow.Item("BorderWidth") = 0
                objNewRow.Item("TopBorderWidth") = 0
                objNewRow.Item("BottomBorderWidth") = 0
                objNewRow.Item("LeftBorderWidth") = 0
                objNewRow.Item("RightBorderWidth") = 0
                objNewRow.Item("EngFooterCaption") = ""
                objNewRow.Item("ArbFooterCaption") = ""
                objNewRow.Item("ColumnWidth") = 0
                objNewRow.Item("ColumnHalignment") = 0
                objNewRow.Item("ColumnValignment") = 0
                objNewRow.Item("ColumnIsHidden") = False
                objNewRow.Item("ColumnIsOverView") = False
                objNewRow.Item("ColumnIsWrap") = False
                objNewRow.Item("ColumnForeColor") = CsForeColor
                objNewRow.Item("ColumnBackColor") = CsBackColor
                objNewRow.Item("ColumnFont") = CsFont
                objNewRow.Item("ColumnFontIsBold") = False
                objNewRow.Item("ColumnFontIsItalic") = False
                objNewRow.Item("ColumnFontIsUnderLine") = False
                objNewRow.Item("ColumnFontSize") = csFontSizeDefault
                objNewRow.Item("TopPadding") = 0
                objNewRow.Item("BottomPadding") = 0
                objNewRow.Item("LeftPadding") = 0
                objNewRow.Item("RightPadding") = 0
                objNewRow.Item("TopMargin") = 0
                objNewRow.Item("BottomMargin") = 0
                objNewRow.Item("LeftMargin") = 0
                objNewRow.Item("RightMargin") = 0
                objNewRow.Item("Format") = ""
                objNewRow.Item("FooterTotal") = ""
                objNewRow.Item("IsSorted") = 0

                objNewRow.Item("IsGroupBy") = False
                objNewRow.Item("Formula") = ""
                objNewRow.Item("ColumnHeaderHalignment") = 0
                objNewRow.Item("ColumnHeaderValignment") = 0
                objNewRow.Item("ColHeaderBackColor") = CsBackColor
                objNewRow.Item("ColHeaderForeColor") = CsForeColor
                objNewRow.Item("ColHeaderFont") = CsFont
                objNewRow.Item("ColHeaderFontIsBold") = False
                objNewRow.Item("ColHeaderFontIsItalic") = False
                objNewRow.Item("ColHeaderFontIsUnderline") = False
                objNewRow.Item("ColHeaderFontSize") = csFontSizeDefault
                objNewRow.Item("ColHeaderBorderWidth") = 0
                objNewRow.Item("ColHeaderBorderWidthTop") = 0
                objNewRow.Item("ColHeaderBorderWidthBottom") = 0
                objNewRow.Item("ColHeaderBorderWidthLeft") = 0
                objNewRow.Item("ColHeaderBorderWidthRight") = 0
                objNewRow.Item("ColHeaderBorderStyle") = GetBorderStyleIntValue("Default")
                objNewRow.Item("ColHeaderBorderStyleTop") = 0
                objNewRow.Item("ColHeaderBorderStyleBottom") = 0
                objNewRow.Item("ColHeaderBorderStyleRight") = 0
                objNewRow.Item("ColHeaderBorderStyleLeft") = 0
                objNewRow.Item("ColHeaderBorderColor") = ""
                objNewRow.Item("ColHeaderBorderColorTop") = ""
                objNewRow.Item("ColHeaderBorderColorBottom") = ""
                objNewRow.Item("ColHeaderBorderColorLeft") = ""
                objNewRow.Item("ColHeaderBorderColorRight") = ""
                objNewRow.Item("ColHeaderPaddingLeft") = 0
                objNewRow.Item("ColHeaderPaddingRight") = 0
                objNewRow.Item("ColHeaderPaddingTop") = 0
                objNewRow.Item("ColHeaderPaddingBottom") = 0
                objNewRow.Item("ColFooterBackColor") = CsBackColor
                objNewRow.Item("ColFooterForeColor") = CsForeColor
                objNewRow.Item("ColFooterFont") = CsFont
                objNewRow.Item("ColFooterFontIsBold") = False
                objNewRow.Item("ColFooterFontIsItalic") = False
                objNewRow.Item("ColFooterFontIsUnderline") = False
                objNewRow.Item("ColFooterFontSize") = csFontSizeDefault
                objNewRow.Item("ColFooterBorderWidth") = 0
                objNewRow.Item("ColFooterBorderWidthTop") = 0
                objNewRow.Item("ColFooterBorderWidthBottom") = 0
                objNewRow.Item("ColFooterBorderWidthLeft") = 0
                objNewRow.Item("ColFooterBorderWidthRight") = 0
                objNewRow.Item("ColFooterBorderStyle") = GetBorderStyleIntValue("Default")
                objNewRow.Item("ColFooterBorderStyleTop") = 0
                objNewRow.Item("ColFooterBorderStyleBottom") = 0
                objNewRow.Item("ColFooterBorderStyleRight") = 0
                objNewRow.Item("ColFooterBorderStyleLeft") = 0
                objNewRow.Item("ColFooterBorderColor") = ""
                objNewRow.Item("ColFooterBorderColorTop") = ""
                objNewRow.Item("ColFooterBorderColorBottom") = ""
                objNewRow.Item("ColFooterBorderColorLeft") = ""
                objNewRow.Item("ColFooterBorderColorRight") = ""
                objNewRow.Item("ColFooterPaddingLeft") = 0
                objNewRow.Item("ColFooterPaddingRight") = 0
                objNewRow.Item("ColFooterPaddingTop") = 0
                objNewRow.Item("ColFooterPaddingBottom") = 0
                objNewRow.Item("ColumnFooterValignment") = 0
                objNewRow.Item("ColumnFooterHalignment") = 0
                '---------------------------------------------------------------------------
            Else
                objNewRow.Item("IsCriteria") = True
                objNewRow.Item("Length") = 0
                objNewRow.Item("DefaultValue") = ""
                objNewRow.Item("Operation") = 0
                objNewRow.Item("MaximumValue") = ""
                objNewRow.Item("MinimumValue") = ""
                objNewRow.Item("SearchID") = 0
                objNewRow.Item("SQLname") = ""
                objNewRow.Item("FieldEngDefaultSet") = 0
                objNewRow.Item("FieldArbDefaultSet") = 0
                objNewRow.Item("FieldDefaultSetValues") = 0
                objNewRow.Item("DefinedTablesId") = 0
                objNewRow.Item("DefinedColumnsId") = 0
                objNewRow.Item("DefinedValueId") = 0
            End If
            dtFieldsDataTable.Rows.Add(objNewRow)
        Catch ex As Exception

        End Try
    End Function
    Private Function GetRowsValues(ByVal TempDs As Data.DataSet, ByRef dtFieldsDataTable As Data.DataTable, ByVal blnIsView As Boolean) As Boolean
        Dim objNewRow As Data.DataRow
        Const CsForeColor = "Black"
        Const CsBackColor = "White"
        Const CsFont = "Arial"
        Const csReportWidth = 800
        Const csFontSizeDefault = 12
        Const csHeight = 25
        Try
            For Each objRow As Data.DataRow In TempDs.Tables(0).Rows
                objNewRow = dtFieldsDataTable.NewRow()
                objNewRow.Item("ID") = IIf(IsDBNull(objRow.Item("ID")), 0, objRow.Item("ID"))
                objNewRow.Item("Rank") = IIf(IsDBNull(objRow.Item("Rank")), "", objRow.Item("Rank"))
                objNewRow.Item("ReportID") = IIf(IsDBNull(objRow.Item("ReportID")), 0, objRow.Item("ReportID"))
                objNewRow.Item("FieldName") = IIf(IsDBNull(objRow.Item("FieldName")), "", objRow.Item("FieldName"))
                objNewRow.Item("DataType") = IIf(IsDBNull(objRow.Item("DataType")), "", objRow.Item("DataType"))
                objNewRow.Item("EngDescription") = IIf(IsDBNull(objRow.Item("EngDescription")), "", objRow.Item("EngDescription"))
                objNewRow.Item("ArbDescription") = IIf(IsDBNull(objRow.Item("ArbDescription")), "", objRow.Item("ArbDescription"))
                objNewRow.Item("Status") = IIf(IsDBNull(objRow.Item("Status")), True, objRow.Item("Status"))
                objNewRow.Item("FieldLanguage") = IIf(IsDBNull(objRow.Item("FieldLanguage")), 3, objRow.Item("FieldLanguage"))
                If blnIsView Then
                    objNewRow.Item("IsCalculated") = IIf(IsDBNull(objRow.Item("IsCalculated")), False, objRow.Item("IsCalculated"))
                    '---------------------------------------------------------------------------
                    objNewRow.Item("ColFooterSummaryType") = IIf(IsDBNull(objRow.Item("ColFooterSummaryType")), "", objRow.Item("ColFooterSummaryType"))
                    objNewRow.Item("ColHeaderSorting") = IIf(IsDBNull(objRow.Item("ColHeaderSorting")), 0, objRow.Item("ColHeaderSorting"))
                    objNewRow.Item("BorderColor") = IIf(IsDBNull(objRow.Item("BorderColor")), "", objRow.Item("BorderColor"))
                    objNewRow.Item("TopBorderColor") = IIf(IsDBNull(objRow.Item("TopBorderColor")), "", objRow.Item("TopBorderColor"))
                    objNewRow.Item("BottomBorderColor") = IIf(IsDBNull(objRow.Item("BottomBorderColor")), "", objRow.Item("BottomBorderColor"))
                    objNewRow.Item("LeftBorderColor") = IIf(IsDBNull(objRow.Item("LeftBorderColor")), "", objRow.Item("LeftBorderColor"))
                    objNewRow.Item("RightBorderColor") = IIf(IsDBNull(objRow.Item("RightBorderColor")), "", objRow.Item("RightBorderColor"))
                    objNewRow.Item("BorderStyle") = IIf(IsDBNull(objRow.Item("BorderStyle")), GetBorderStyleIntValue("Default"), objRow.Item("BorderStyle"))
                    objNewRow.Item("TopBorderStyle") = IIf(IsDBNull(objRow.Item("TopBorderStyle")), 0, objRow.Item("TopBorderStyle"))
                    objNewRow.Item("BottomBorderStyle") = IIf(IsDBNull(objRow.Item("BottomBorderStyle")), 0, objRow.Item("BottomBorderStyle"))
                    objNewRow.Item("LeftBorderStyle") = IIf(IsDBNull(objRow.Item("LeftBorderStyle")), 0, objRow.Item("LeftBorderStyle"))
                    objNewRow.Item("RightBorderStyle") = IIf(IsDBNull(objRow.Item("RightBorderStyle")), 0, objRow.Item("RightBorderStyle"))
                    objNewRow.Item("BorderWidth") = IIf(IsDBNull(objRow.Item("BorderWidth")), 0, objRow.Item("BorderWidth"))
                    objNewRow.Item("TopBorderWidth") = IIf(IsDBNull(objRow.Item("TopBorderWidth")), 0, objRow.Item("BorderWidth"))
                    objNewRow.Item("BottomBorderWidth") = IIf(IsDBNull(objRow.Item("BottomBorderWidth")), 0, objRow.Item("BorderWidth"))
                    objNewRow.Item("LeftBorderWidth") = IIf(IsDBNull(objRow.Item("LeftBorderWidth")), 0, objRow.Item("BorderWidth"))
                    objNewRow.Item("RightBorderWidth") = IIf(IsDBNull(objRow.Item("RightBorderWidth")), 0, objRow.Item("BorderWidth"))
                    objNewRow.Item("EngFooterCaption") = IIf(IsDBNull(objRow.Item("EngFooterCaption")), "", objRow.Item("EngFooterCaption"))
                    objNewRow.Item("ArbFooterCaption") = IIf(IsDBNull(objRow.Item("ArbFooterCaption")), "", objRow.Item("ArbFooterCaption"))
                    objNewRow.Item("ColumnWidth") = IIf(IsDBNull(objRow.Item("ColumnWidth")), 0, objRow.Item("ColumnWidth"))
                    objNewRow.Item("ColumnHalignment") = IIf(IsDBNull(objRow.Item("ColumnHalignment")), 0, objRow.Item("ColumnHalignment"))
                    objNewRow.Item("ColumnValignment") = IIf(IsDBNull(objRow.Item("ColumnValignment")), 0, objRow.Item("ColumnValignment"))
                    objNewRow.Item("ColumnIsHidden") = IIf(IsDBNull(objRow.Item("ColumnIsHidden")), False, objRow.Item("ColumnIsHidden"))
                    objNewRow.Item("ColumnIsOverView") = IIf(IsDBNull(objRow.Item("ColumnIsOverView")), False, objRow.Item("ColumnIsOverView"))
                    objNewRow.Item("ColumnIsWrap") = IIf(IsDBNull(objRow.Item("ColumnIsWrap")), False, objRow.Item("ColumnIsWrap"))
                    objNewRow.Item("ColumnForeColor") = IIf(IsDBNull(objRow.Item("ColumnForeColor")), CsForeColor, objRow.Item("ColumnForeColor"))
                    objNewRow.Item("ColumnBackColor") = IIf(IsDBNull(objRow.Item("ColumnBackColor")), CsBackColor, objRow.Item("ColumnBackColor"))
                    objNewRow.Item("ColumnFont") = IIf(IsDBNull(objRow.Item("ColumnFont")), CsFont, objRow.Item("ColumnFont"))
                    objNewRow.Item("ColumnFontIsBold") = IIf(IsDBNull(objRow.Item("ColumnFontIsBold")), False, objRow.Item("ColumnFontIsBold"))
                    objNewRow.Item("ColumnFontIsItalic") = IIf(IsDBNull(objRow.Item("ColumnFontIsItalic")), False, objRow.Item("ColumnFontIsItalic"))
                    objNewRow.Item("ColumnFontIsUnderLine") = IIf(IsDBNull(objRow.Item("ColumnFontIsUnderLine")), False, objRow.Item("ColumnFontIsUnderLine"))
                    objNewRow.Item("ColumnFontSize") = IIf(IsDBNull(objRow.Item("ColumnFontSize")), csFontSizeDefault, objRow.Item("ColumnFontSize"))
                    objNewRow.Item("TopPadding") = IIf(IsDBNull(objRow.Item("TopPadding")), 0, objRow.Item("TopPadding"))
                    objNewRow.Item("BottomPadding") = IIf(IsDBNull(objRow.Item("BottomPadding")), 0, objRow.Item("BottomPadding"))
                    objNewRow.Item("LeftPadding") = IIf(IsDBNull(objRow.Item("LeftPadding")), 0, objRow.Item("LeftPadding"))
                    objNewRow.Item("RightPadding") = IIf(IsDBNull(objRow.Item("RightPadding")), 0, objRow.Item("RightPadding"))
                    objNewRow.Item("TopMargin") = IIf(IsDBNull(objRow.Item("TopMargin")), 0, objRow.Item("TopMargin"))
                    objNewRow.Item("BottomMargin") = IIf(IsDBNull(objRow.Item("BottomMargin")), 0, objRow.Item("BottomMargin"))
                    objNewRow.Item("LeftMargin") = IIf(IsDBNull(objRow.Item("LeftMargin")), 0, objRow.Item("LeftMargin"))
                    objNewRow.Item("RightMargin") = IIf(IsDBNull(objRow.Item("RightMargin")), 0, objRow.Item("RightMargin"))
                    objNewRow.Item("Format") = IIf(IsDBNull(objRow.Item("Format")), "", objRow.Item("Format"))
                    objNewRow.Item("FooterTotal") = IIf(IsDBNull(objRow.Item("FooterTotal")), "", objRow.Item("FooterTotal"))
                    If (IsDBNull(objRow.Item("IsSorted"))) Then
                        objNewRow.Item("IsSorted") = 0
                    ElseIf objRow.Item("IsSorted") = 1 Then
                        objNewRow.Item("IsSorted") = 1
                    ElseIf objRow.Item("IsSorted") = 2 Then
                        objNewRow.Item("IsSorted") = 2
                    Else
                        objNewRow.Item("IsSorted") = 0
                    End If
                    objNewRow.Item("IsGroupBy") = IIf(IsDBNull(objRow.Item("IsGroupBy")), False, objRow.Item("IsGroupBy"))
                    objNewRow.Item("Formula") = IIf(IsDBNull(objRow.Item("Formula")), "", objRow.Item("Formula"))
                    objNewRow.Item("ColumnHeaderHalignment") = IIf(IsDBNull(objRow.Item("ColumnHeaderHalignment")), 0, objRow.Item("ColumnHeaderHalignment"))
                    objNewRow.Item("ColumnHeaderValignment") = IIf(IsDBNull(objRow.Item("ColumnHeaderValignment")), 0, objRow.Item("ColumnHeaderValignment"))
                    objNewRow.Item("ColHeaderBackColor") = IIf(IsDBNull(objRow.Item("ColHeaderBackColor")), CsBackColor, objRow.Item("ColHeaderBackColor"))
                    objNewRow.Item("ColHeaderForeColor") = IIf(IsDBNull(objRow.Item("ColHeaderForeColor")), CsForeColor, objRow.Item("ColHeaderForeColor"))
                    objNewRow.Item("ColHeaderFont") = IIf(IsDBNull(objRow.Item("ColHeaderFont")), CsFont, objRow.Item("ColHeaderFont"))
                    objNewRow.Item("ColHeaderFontIsBold") = IIf(IsDBNull(objRow.Item("ColHeaderFontIsBold")), False, objRow.Item("ColHeaderFontIsBold"))
                    objNewRow.Item("ColHeaderFontIsItalic") = IIf(IsDBNull(objRow.Item("ColHeaderFontIsItalic")), False, objRow.Item("ColHeaderFontIsItalic"))
                    objNewRow.Item("ColHeaderFontIsUnderline") = IIf(IsDBNull(objRow.Item("ColHeaderFontIsUnderline")), False, objRow.Item("ColHeaderFontIsUnderline"))
                    objNewRow.Item("ColHeaderFontSize") = IIf(IsDBNull(objRow.Item("ColHeaderFontSize")), csFontSizeDefault, objRow.Item("ColHeaderFontSize"))
                    objNewRow.Item("ColHeaderBorderWidth") = IIf(IsDBNull(objRow.Item("ColHeaderBorderWidth")), 0, objRow.Item("ColHeaderBorderWidth"))
                    objNewRow.Item("ColHeaderBorderWidthTop") = IIf(IsDBNull(objRow.Item("ColHeaderBorderWidthTop")), 0, objRow.Item("ColHeaderBorderWidthTop"))
                    objNewRow.Item("ColHeaderBorderWidthBottom") = IIf(IsDBNull(objRow.Item("ColHeaderBorderWidthBottom")), 0, objRow.Item("ColHeaderBorderWidthBottom"))
                    objNewRow.Item("ColHeaderBorderWidthLeft") = IIf(IsDBNull(objRow.Item("ColHeaderBorderWidthLeft")), 0, objRow.Item("ColHeaderBorderWidthLeft"))
                    objNewRow.Item("ColHeaderBorderWidthRight") = IIf(IsDBNull(objRow.Item("ColHeaderBorderWidthRight")), 0, objRow.Item("ColHeaderBorderWidthRight"))
                    objNewRow.Item("ColHeaderBorderStyle") = IIf(IsDBNull(objRow.Item("ColHeaderBorderStyle")), GetBorderStyleIntValue("Default"), objRow.Item("ColHeaderBorderStyle"))
                    objNewRow.Item("ColHeaderBorderStyleTop") = IIf(IsDBNull(objRow.Item("ColHeaderBorderStyleTop")), 0, objRow.Item("ColHeaderBorderStyleTop"))
                    objNewRow.Item("ColHeaderBorderStyleBottom") = IIf(IsDBNull(objRow.Item("ColHeaderBorderStyleBottom")), 0, objRow.Item("ColHeaderBorderStyleBottom"))
                    objNewRow.Item("ColHeaderBorderStyleRight") = IIf(IsDBNull(objRow.Item("ColHeaderBorderStyleRight")), 0, objRow.Item("ColHeaderBorderStyleRight"))
                    objNewRow.Item("ColHeaderBorderStyleLeft") = IIf(IsDBNull(objRow.Item("ColHeaderBorderStyleLeft")), 0, objRow.Item("ColHeaderBorderStyleLeft"))
                    objNewRow.Item("ColHeaderBorderColor") = IIf(IsDBNull(objRow.Item("ColHeaderBorderColor")), "", objRow.Item("ColHeaderBorderColor"))
                    objNewRow.Item("ColHeaderBorderColorTop") = IIf(IsDBNull(objRow.Item("ColHeaderBorderColorTop")), "", objRow.Item("ColHeaderBorderColorTop"))
                    objNewRow.Item("ColHeaderBorderColorBottom") = IIf(IsDBNull(objRow.Item("ColHeaderBorderColorBottom")), "", objRow.Item("ColHeaderBorderColorBottom"))
                    objNewRow.Item("ColHeaderBorderColorLeft") = IIf(IsDBNull(objRow.Item("ColHeaderBorderColorLeft")), "", objRow.Item("ColHeaderBorderColorLeft"))
                    objNewRow.Item("ColHeaderBorderColorRight") = IIf(IsDBNull(objRow.Item("ColHeaderBorderColorRight")), "", objRow.Item("ColHeaderBorderColorRight"))
                    objNewRow.Item("ColHeaderPaddingLeft") = IIf(IsDBNull(objRow.Item("ColHeaderPaddingLeft")), 0, objRow.Item("ColHeaderPaddingLeft"))
                    objNewRow.Item("ColHeaderPaddingRight") = IIf(IsDBNull(objRow.Item("ColHeaderPaddingRight")), 0, objRow.Item("ColHeaderPaddingRight"))
                    objNewRow.Item("ColHeaderPaddingTop") = IIf(IsDBNull(objRow.Item("ColHeaderPaddingTop")), 0, objRow.Item("ColHeaderPaddingTop"))
                    objNewRow.Item("ColHeaderPaddingBottom") = IIf(IsDBNull(objRow.Item("ColHeaderPaddingBottom")), 0, objRow.Item("ColHeaderPaddingBottom"))
                    objNewRow.Item("ColFooterBackColor") = IIf(IsDBNull(objRow.Item("ColFooterBackColor")), CsBackColor, objRow.Item("ColFooterBackColor"))
                    objNewRow.Item("ColFooterForeColor") = IIf(IsDBNull(objRow.Item("ColFooterForeColor")), CsForeColor, objRow.Item("ColFooterForeColor"))
                    objNewRow.Item("ColFooterFont") = IIf(IsDBNull(objRow.Item("ColFooterFont")), CsFont, objRow.Item("ColFooterFont"))
                    objNewRow.Item("ColFooterFontIsBold") = IIf(IsDBNull(objRow.Item("ColFooterFontIsBold")), False, objRow.Item("ColFooterFontIsBold"))
                    objNewRow.Item("ColFooterFontIsItalic") = IIf(IsDBNull(objRow.Item("ColFooterFontIsItalic")), False, objRow.Item("ColFooterFontIsItalic"))
                    objNewRow.Item("ColFooterFontIsUnderline") = IIf(IsDBNull(objRow.Item("ColFooterFontIsUnderline")), False, objRow.Item("ColFooterFontIsUnderline"))
                    objNewRow.Item("ColFooterFontSize") = IIf(IsDBNull(objRow.Item("ColFooterFontSize")), csFontSizeDefault, objRow.Item("ColFooterFontSize"))
                    objNewRow.Item("ColFooterBorderWidth") = IIf(IsDBNull(objRow.Item("ColFooterBorderWidth")), 0, objRow.Item("ColFooterBorderWidth"))
                    objNewRow.Item("ColFooterBorderWidthTop") = IIf(IsDBNull(objRow.Item("ColFooterBorderWidthTop")), 0, objRow.Item("ColFooterBorderWidthTop"))
                    objNewRow.Item("ColFooterBorderWidthBottom") = IIf(IsDBNull(objRow.Item("ColFooterBorderWidthBottom")), 0, objRow.Item("ColFooterBorderWidthBottom"))
                    objNewRow.Item("ColFooterBorderWidthLeft") = IIf(IsDBNull(objRow.Item("ColFooterBorderWidthLeft")), 0, objRow.Item("ColFooterBorderWidthLeft"))
                    objNewRow.Item("ColFooterBorderWidthRight") = IIf(IsDBNull(objRow.Item("ColFooterBorderWidthRight")), 0, objRow.Item("ColFooterBorderWidthRight"))
                    objNewRow.Item("ColFooterBorderStyle") = IIf(IsDBNull(objRow.Item("ColFooterBorderStyle")), GetBorderStyleIntValue("Default"), objRow.Item("ColFooterBorderStyle"))
                    objNewRow.Item("ColFooterBorderStyleTop") = IIf(IsDBNull(objRow.Item("ColFooterBorderStyleTop")), 0, objRow.Item("ColFooterBorderStyleTop"))
                    objNewRow.Item("ColFooterBorderStyleBottom") = IIf(IsDBNull(objRow.Item("ColFooterBorderStyleBottom")), 0, objRow.Item("ColFooterBorderStyleBottom"))
                    objNewRow.Item("ColFooterBorderStyleRight") = IIf(IsDBNull(objRow.Item("ColFooterBorderStyleRight")), 0, objRow.Item("ColFooterBorderStyleRight"))
                    objNewRow.Item("ColFooterBorderStyleLeft") = IIf(IsDBNull(objRow.Item("ColFooterBorderStyleLeft")), 0, objRow.Item("ColFooterBorderStyleLeft"))
                    objNewRow.Item("ColFooterBorderColor") = IIf(IsDBNull(objRow.Item("ColFooterBorderColor")), "", objRow.Item("ColFooterBorderColor"))
                    objNewRow.Item("ColFooterBorderColorTop") = IIf(IsDBNull(objRow.Item("ColFooterBorderColorTop")), "", objRow.Item("ColFooterBorderColorTop"))
                    objNewRow.Item("ColFooterBorderColorBottom") = IIf(IsDBNull(objRow.Item("ColFooterBorderColorBottom")), "", objRow.Item("ColFooterBorderColorBottom"))
                    objNewRow.Item("ColFooterBorderColorLeft") = IIf(IsDBNull(objRow.Item("ColFooterBorderColorLeft")), "", objRow.Item("ColFooterBorderColorLeft"))
                    objNewRow.Item("ColFooterBorderColorRight") = IIf(IsDBNull(objRow.Item("ColFooterBorderColorRight")), "", objRow.Item("ColFooterBorderColorRight"))
                    objNewRow.Item("ColFooterPaddingLeft") = IIf(IsDBNull(objRow.Item("ColFooterPaddingLeft")), 0, objRow.Item("ColFooterPaddingLeft"))
                    objNewRow.Item("ColFooterPaddingRight") = IIf(IsDBNull(objRow.Item("ColFooterPaddingRight")), 0, objRow.Item("ColFooterPaddingRight"))
                    objNewRow.Item("ColFooterPaddingTop") = IIf(IsDBNull(objRow.Item("ColFooterPaddingTop")), 0, objRow.Item("ColFooterPaddingTop"))
                    objNewRow.Item("ColFooterPaddingBottom") = IIf(IsDBNull(objRow.Item("ColFooterPaddingBottom")), 0, objRow.Item("ColFooterPaddingBottom"))
                    objNewRow.Item("ColumnFooterValignment") = IIf(IsDBNull(objRow.Item("ColumnFooterValignment")), 0, objRow.Item("ColumnFooterValignment"))
                    objNewRow.Item("ColumnFooterHalignment") = IIf(IsDBNull(objRow.Item("ColumnFooterHalignment")), 0, objRow.Item("ColumnFooterHalignment"))
                    '---------------------------------------------------------------------------
                Else
                    objNewRow.Item("IsCriteria") = True
                    objNewRow.Item("Length") = IIf(IsDBNull(objRow.Item("Length")), 0, objRow.Item("Length"))
                    objNewRow.Item("DefaultValue") = IIf(IsDBNull(objRow.Item("DefaultValue")), "", objRow.Item("DefaultValue"))
                    objNewRow.Item("Operation") = IIf(IsDBNull(objRow.Item("Operation")), 0, objRow.Item("Operation"))
                    objNewRow.Item("MaximumValue") = IIf(IsDBNull(objRow.Item("MaximumValue")), "", objRow.Item("MaximumValue"))
                    objNewRow.Item("MinimumValue") = IIf(IsDBNull(objRow.Item("MinimumValue")), "", objRow.Item("MinimumValue"))
                    objNewRow.Item("SearchID") = IIf(IsDBNull(objRow.Item("SearchID")), 0, objRow.Item("SearchID"))
                    objNewRow.Item("SQLname") = IIf(IsDBNull(objRow.Item("SQLname")), "", objRow.Item("SQLname"))
                    objNewRow.Item("FieldEngDefaultSet") = IIf(IsDBNull(objRow.Item("FieldEngDefaultSet")), 0, objRow.Item("FieldEngDefaultSet"))
                    objNewRow.Item("FieldArbDefaultSet") = IIf(IsDBNull(objRow.Item("FieldArbDefaultSet")), 0, objRow.Item("FieldArbDefaultSet"))
                    objNewRow.Item("FieldDefaultSetValues") = IIf(IsDBNull(objRow.Item("FieldDefaultSetValues")), 0, objRow.Item("FieldDefaultSetValues"))
                    objNewRow.Item("DefinedTablesId") = IIf(IsDBNull(objRow.Item("DefinedTablesId")), 0, objRow.Item("DefinedTablesId"))
                    objNewRow.Item("DefinedColumnsId") = IIf(IsDBNull(objRow.Item("DefinedColumnsId")), 0, objRow.Item("DefinedColumnsId"))
                    objNewRow.Item("DefinedValueId") = IIf(IsDBNull(objRow.Item("DefinedValueId")), 0, objRow.Item("DefinedValueId"))
                End If
                dtFieldsDataTable.Rows.Add(objNewRow)
            Next
        Catch ex As Exception

        End Try
    End Function
    Public Function GetBorderStyleIntValue(ByVal StrBorderStyle As String)
        If IsNothing(StrBorderStyle) Then Return 0
        Select Case StrBorderStyle.ToUpper

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
            Case "DASHE DDOT DOT"
                Return 5
            Case Else
                Return 0
        End Select
    End Function
    Public Function CreateCriteriaDataTable(ByRef dtColumns As Data.DataTable) As Boolean

        dtColumns.Columns.Add(New Data.DataColumn("ID", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("Rank", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ReportID", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("FieldName", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("DataType", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("Length", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("EngDescription", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ArbDescription", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("DefaultValue", GetType(Object)))
        dtColumns.Columns.Add(New Data.DataColumn("Status", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("Operation", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("FieldLanguage", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("MaximumValue", GetType(Object)))
        dtColumns.Columns.Add(New Data.DataColumn("MinimumValue", GetType(Object)))
        dtColumns.Columns.Add(New Data.DataColumn("SearchID", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("SQLname", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("FieldEngDefaultSet", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("FieldArbDefaultSet", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("FieldDefaultSetValues", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("DefinedTablesId", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("DefinedColumnsId", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("DefinedValueId", GetType(Integer)))
        '*-*-*-*-*-*-*-*-*-*
        dtColumns.Columns.Add(New Data.DataColumn("IsCalculated", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("IsCriteria", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ParmeterValue", GetType(String)))
        '*-*-*-*-*-*-*-*-*-*
    End Function

    Public Function CreateViewDataTable(ByRef dtColumns As Data.DataTable) As Boolean
        Try
            dtColumns.Columns.Add(New Data.DataColumn("Length", GetType(Integer)))
            dtColumns.Columns.Add(New Data.DataColumn("Operation", GetType(String)))
            dtColumns.Columns.Add(New Data.DataColumn("MaximumValue", GetType(Object)))
            dtColumns.Columns.Add(New Data.DataColumn("MinimumValue", GetType(Object)))
            dtColumns.Columns.Add(New Data.DataColumn("SearchID", GetType(Integer)))
            dtColumns.Columns.Add(New Data.DataColumn("SQLname", GetType(String)))
            dtColumns.Columns.Add(New Data.DataColumn("FieldEngDefaultSet", GetType(String)))
            dtColumns.Columns.Add(New Data.DataColumn("FieldArbDefaultSet", GetType(String)))
            dtColumns.Columns.Add(New Data.DataColumn("FieldDefaultSetValues", GetType(String)))
            dtColumns.Columns.Add(New Data.DataColumn("DefinedTablesId", GetType(Integer)))
            dtColumns.Columns.Add(New Data.DataColumn("DefinedColumnsId", GetType(Integer)))
            dtColumns.Columns.Add(New Data.DataColumn("DefinedValueId", GetType(Integer)))
            dtColumns.Columns.Add(New Data.DataColumn("IsCriteria", GetType(Boolean)))
            dtColumns.Columns.Add(New Data.DataColumn("ParmeterValue", GetType(String)))
            '--------------------------------------------
            dtColumns.Columns.Add(New Data.DataColumn("Rank", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("BorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("TopBorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("BottomBorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("LeftBorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("RightBorderColor", GetType(String)))
            dtColumns.Columns.Add(New Data.DataColumn("FieldName", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("GroupByRank", GetType(Integer)))
        'Border Style
        dtColumns.Columns.Add(New Data.DataColumn("BorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("TopBorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("BottomBorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("LeftBorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("RightBorderStyle", GetType(String)))

        'Border Width
        dtColumns.Columns.Add(New Data.DataColumn("BorderWidth", GetType(Integer)))
        dtColumns.Columns("BorderWidth").DefaultValue = 0

        dtColumns.Columns.Add(New Data.DataColumn("TopBorderWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("BottomBorderWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("LeftBorderWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("RightBorderWidth", GetType(Integer)))

        dtColumns.Columns("TopBorderWidth").DefaultValue = 0
        dtColumns.Columns("BottomBorderWidth").DefaultValue = 0
        dtColumns.Columns("LeftBorderWidth").DefaultValue = 0
        dtColumns.Columns("RightBorderWidth").DefaultValue = 0




        'Columns Captions 
            dtColumns.Columns.Add(New Data.DataColumn("EngDescription", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("EngFooterCaption", GetType(String)))
            dtColumns.Columns.Add(New Data.DataColumn("ArbDescription", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ArbFooterCaption", GetType(String)))

        '
        dtColumns.Columns.Add(New Data.DataColumn("ColumnWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnValignment", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnHalignment", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnIsHidden", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnIsWrap", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnIsOverView", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnForeColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnBackColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFont", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFontIsBold", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFontIsItalic", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFontIsUnderLine", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFontSize", GetType(Integer)))
        'Pading
        dtColumns.Columns.Add(New Data.DataColumn("TopPadding", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("BottomPadding", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("LeftPadding", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("RightPadding", GetType(Integer)))
        dtColumns.Columns("TopPadding").DefaultValue = 0
        dtColumns.Columns("BottomPadding").DefaultValue = 0
        dtColumns.Columns("LeftPadding").DefaultValue = 0
        dtColumns.Columns("RightPadding").DefaultValue = 0
        'Marging
        dtColumns.Columns.Add(New Data.DataColumn("TopMargin", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("BottomMargin", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("LeftMargin", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("RightMargin", GetType(Integer)))
        dtColumns.Columns("TopMargin").DefaultValue = 0
        dtColumns.Columns("BottomMargin").DefaultValue = 0
        dtColumns.Columns("LeftMargin").DefaultValue = 0
        dtColumns.Columns("RightMargin").DefaultValue = 0
        dtColumns.Columns.Add(New Data.DataColumn("FooterTotal", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("Format", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("IsSorted", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("IsGroupBy", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("DefaultValue", GetType(Object)))
        dtColumns.Columns.Add(New Data.DataColumn("Iscalculated", GetType(Boolean)))
        dtColumns.Columns("Iscalculated").DefaultValue = False
        dtColumns.Columns.Add(New Data.DataColumn("IsCanceled", GetType(Boolean)))
        dtColumns.Columns("IsCanceled").DefaultValue = False
        dtColumns.Columns.Add(New Data.DataColumn("ID", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ReportID", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("Formula", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnHeaderHalignment", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnHeadervalignment", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("FieldLanguage", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("DataType", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBackColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderForeColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFont", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontSize", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontIsItalic", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontIsBold", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontIsUnderline", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontIsStrikeOut", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderFontIsOverLine", GetType(Boolean)))

        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderWidthTop", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderWidthBottom", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderWidthLeft", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderWidthRight", GetType(Integer)))


        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderStyleTop", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderStyleBottom", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderStyleLeft", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderStyleRight", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderColorTop", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderColorBottom", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderColorLeft", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderBorderColorRight", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderPaddingLeft", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderPaddingRight", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderPaddingTop", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColHeaderPaddingBottom", GetType(Integer)))

        'Footer

        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBackColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterForeColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFont", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontSize", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontIsItalic", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontIsBold", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontIsUnderline", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontIsStrikeOut", GetType(Boolean)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterFontIsOverLine", GetType(Boolean)))

        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderWidth", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderWidthTop", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderWidthBottom", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderWidthLeft", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderWidthRight", GetType(Integer)))


        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderStyle", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderStyleTop", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderStyleBottom", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderStyleLeft", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderStyleRight", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderColor", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderColorTop", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderColorBottom", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderColorLeft", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterBorderColorRight", GetType(String)))

        dtColumns.Columns.Add(New Data.DataColumn("ColFooterPaddingLeft", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterPaddingRight", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterPaddingTop", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColFooterPaddingBottom", GetType(Integer)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFooterHalignment", GetType(Integer)))

            dtColumns.Columns.Add(New Data.DataColumn("Status", GetType(Boolean)))
            dtColumns.Columns.Add(New Data.DataColumn("ColFooterSummaryType", GetType(String)))
            dtColumns.Columns.Add(New Data.DataColumn("ColHeaderSorting", GetType(String)))
        dtColumns.Columns.Add(New Data.DataColumn("ColumnFootervalignment", GetType(Integer)))
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "Class Private Functions"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mCode = mDataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = mDataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = mDataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = mDataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mEngDescription = mDataHandler.DataValue_Out(.Item("EngDescription"), SqlDbType.VarChar)
                mArbDescription = mDataHandler.DataValue_Out(.Item("ArbDescription"), SqlDbType.VarChar)
                mReportGroupID = mDataHandler.DataValue_Out(.Item("ReportGroupID"), SqlDbType.Int)
                mReportSource = mDataHandler.DataValue_Out(.Item("ReportSource"), SqlDbType.Int)
                mEngTitle = mDataHandler.DataValue_Out(.Item("EngTitle"), SqlDbType.VarChar)
                mArbTitle = mDataHandler.DataValue_Out(.Item("ArbTitle"), SqlDbType.VarChar)
                mRefreshTimeInterval = mDataHandler.DataValue_Out(.Item("RefreshTimeInterval"), SqlDbType.Int)
                mRank = mDataHandler.DataValue_Out(.Item("Rank"), SqlDbType.Int)
                mCompanyLogo = mDataHandler.DataValue_Out(.Item("CompanyLogo"), SqlDbType.Bit)
                mCompanyText = mDataHandler.DataValue_Out(.Item("CompanyText"), SqlDbType.Bit)
                mCompanyHeader = mDataHandler.DataValue_Out(.Item("CompanyHeader"), SqlDbType.Bit)
                mIsLandscape = mDataHandler.DataValue_Out(.Item("IsLandscape"), SqlDbType.Bit)
                mIsRightToLeft = mDataHandler.DataValue_Out(.Item("IsRightToLeft"), SqlDbType.Bit)
                mReportHeader = mDataHandler.DataValue_Out(.Item("ReportHeader"), SqlDbType.Bit)
                mReportFooter = mDataHandler.DataValue_Out(.Item("ReportFooter"), SqlDbType.Bit)
                mPageHeader = mDataHandler.DataValue_Out(.Item("PageHeader"), SqlDbType.Bit)
                mPageFooter = mDataHandler.DataValue_Out(.Item("PageFooter"), SqlDbType.Bit)
                mDataSource = mDataHandler.DataValue_Out(.Item("DataSource"), SqlDbType.VarChar)
                mCRWName = mDataHandler.DataValue_Out(.Item("CRWName"), SqlDbType.VarChar)

                mViewForm = mDataHandler.DataValue_Out(.Item("viewform"), SqlDbType.VarChar)

                mHeaderBackColor = mDataHandler.DataValue_Out(.Item("HeaderBackColor"), SqlDbType.VarChar)
                mHeaderForeColor = mDataHandler.DataValue_Out(.Item("HeaderForeColor"), SqlDbType.VarChar)
                mHeaderFont = mDataHandler.DataValue_Out(.Item("HeaderFont"), SqlDbType.VarChar)
                mHeaderBorderColor = mDataHandler.DataValue_Out(.Item("HeaderBorderColor"), SqlDbType.VarChar)
                mHeaderBorderWidth = mDataHandler.DataValue_Out(.Item("HeaderBorderWidth"), SqlDbType.Int)
                mReportTopMargins = mDataHandler.DataValue_Out(.Item("ReportTopMargins"), SqlDbType.Int)
                mReportBottomMargins = mDataHandler.DataValue_Out(.Item("ReportBottomMargins"), SqlDbType.Int)
                mReportLeftMargins = mDataHandler.DataValue_Out(.Item("ReportLeftMargins"), SqlDbType.Int)
                mReportRightMargins = mDataHandler.DataValue_Out(.Item("ReportRightMargins"), SqlDbType.Int)
                mReportBackColor = mDataHandler.DataValue_Out(.Item("ReportBackColor"), SqlDbType.VarChar)
                mReportForeColor = mDataHandler.DataValue_Out(.Item("ReportForeColor"), SqlDbType.VarChar)
                mReportAlternatingColor = mDataHandler.DataValue_Out(.Item("ReportAlternatingColor"), SqlDbType.VarChar)
                mReportFont = mDataHandler.DataValue_Out(.Item("ReportFont"), SqlDbType.VarChar)
                mReportFontIsItalic = mDataHandler.DataValue_Out(.Item("ReportFontIsItalic"), SqlDbType.Bit)
                mReportFontIsBold = mDataHandler.DataValue_Out(.Item("ReportFontIsBold"), SqlDbType.Bit)
                mReportFontIsUnderLine = mDataHandler.DataValue_Out(.Item("ReportFontIsUnderLine"), SqlDbType.Bit)
                mReportFontSize = mDataHandler.DataValue_Out(.Item("ReportFontSize"), SqlDbType.Int)
                mHeaderFontIsItalic = mDataHandler.DataValue_Out(.Item("HeaderFontIsItalic"), SqlDbType.Bit)
                mHeaderFontIsBold = mDataHandler.DataValue_Out(.Item("HeaderFontIsBold"), SqlDbType.Bit)
                mHeaderFontIsUnderLine = mDataHandler.DataValue_Out(.Item("HeaderFontIsUnderLine"), SqlDbType.Bit)
                mHeaderFontSize = mDataHandler.DataValue_Out(.Item("HeaderFontSize"), SqlDbType.Int)
                'mRowsBorderColor = mDataHandler.DataValue_Out(.Item("RowsBorderColor"), SqlDbType.VarChar)
                mHeaderBorderStyle = mDataHandler.DataValue_Out(.Item("HeaderBorderStyle"), SqlDbType.Int)
                mHeaderHalignment = mDataHandler.DataValue_Out(.Item("HeaderHalignment"), SqlDbType.Int)
                mHeaderValignment = mDataHandler.DataValue_Out(.Item("HeaderValignment"), SqlDbType.Int)
                mFooterFont = mDataHandler.DataValue_Out(.Item("FooterFont"), SqlDbType.VarChar)
                mFooterFontIsItalic = mDataHandler.DataValue_Out(.Item("FooterFontIsItalic"), SqlDbType.Bit)
                mFooterFontIsBold = mDataHandler.DataValue_Out(.Item("FooterFontIsBold"), SqlDbType.Bit)
                mFooterFontIsUnderLine = mDataHandler.DataValue_Out(.Item("FooterFontIsUnderLine"), SqlDbType.Bit)
                mFooterFontSize = mDataHandler.DataValue_Out(.Item("FooterFontSize"), SqlDbType.Int)
                mFooterHalignment = mDataHandler.DataValue_Out(.Item("FooterHalignment"), SqlDbType.Int)
                mFooterValignment = mDataHandler.DataValue_Out(.Item("FooterValignment"), SqlDbType.Int)
                mFooterBackColor = mDataHandler.DataValue_Out(.Item("FooterBackColor"), SqlDbType.VarChar)
                mFooterBorderColor = mDataHandler.DataValue_Out(.Item("FooterBorderColor"), SqlDbType.VarChar)
                mFooterBorderStyle = mDataHandler.DataValue_Out(.Item("FooterBorderStyle"), SqlDbType.Int)
                mFooterBorderWidth = mDataHandler.DataValue_Out(.Item("FooterBorderWidth"), SqlDbType.Int)
                mHeaderHeight = mDataHandler.DataValue_Out(.Item("HeaderHeight"), SqlDbType.Int)
                mFooterHeight = mDataHandler.DataValue_Out(.Item("FooterHeight"), SqlDbType.Int)

                mRowsForeColor = mDataHandler.DataValue_Out(.Item("RowsForeColor"), SqlDbType.VarChar)
                mRowsBackColor = mDataHandler.DataValue_Out(.Item("RowsBackColor"), SqlDbType.VarChar)
                mRowsFont = mDataHandler.DataValue_Out(.Item("RowsFont"), SqlDbType.VarChar)
                mRowsFontIsItalic = mDataHandler.DataValue_Out(.Item("RowsFontIsItalic"), SqlDbType.Bit)
                mRowsFontIsBold = mDataHandler.DataValue_Out(.Item("RowsFontIsBold"), SqlDbType.Bit)
                mRowsFontIsUnderLine = mDataHandler.DataValue_Out(.Item("RowsFontIsUnderLine"), SqlDbType.Bit)
                mRowsFontSize = mDataHandler.DataValue_Out(.Item("RowsFontSize"), SqlDbType.Int)
                mRowsHieght = mDataHandler.DataValue_Out(.Item("RowsHieght"), SqlDbType.Int)
                mRowsBorderColor = mDataHandler.DataValue_Out(.Item("RowsBorderColor"), SqlDbType.VarChar)
                mRowsBorderStyle = mDataHandler.DataValue_Out(.Item("RowsBorderStyle"), SqlDbType.Int)
                mRowsBorderWidth = mDataHandler.DataValue_Out(.Item("RowsBorderWidth"), SqlDbType.Int)
                mRowsTopBorderColor = mDataHandler.DataValue_Out(.Item("RowsTopBorderColor"), SqlDbType.VarChar)
                mRowsBottomBorderColor = mDataHandler.DataValue_Out(.Item("RowsBottomBorderColor"), SqlDbType.VarChar)
                mRowsLeftBorderColor = mDataHandler.DataValue_Out(.Item("RowsLeftBorderColor"), SqlDbType.VarChar)
                mRowsRightBorderColor = mDataHandler.DataValue_Out(.Item("RowsRightBorderColor"), SqlDbType.VarChar)
                mRowsTopBorderStyle = mDataHandler.DataValue_Out(.Item("RowsTopBorderStyle"), SqlDbType.Int)
                mRowsBottomBorderStyle = mDataHandler.DataValue_Out(.Item("RowsBottomBorderStyle"), SqlDbType.Int)
                mRowsLeftBorderStyle = mDataHandler.DataValue_Out(.Item("RowsLeftBorderStyle"), SqlDbType.Int)
                mRowsRightBorderStyle = mDataHandler.DataValue_Out(.Item("RowsRightBorderStyle"), SqlDbType.Int)
                mRowsTopBorderWidth = mDataHandler.DataValue_Out(.Item("RowsTopBorderWidth"), SqlDbType.Int)
                mRowsBottomBorderWidth = mDataHandler.DataValue_Out(.Item("RowsBottomBorderWidth"), SqlDbType.Int)
                mRowsLeftBorderWidth = mDataHandler.DataValue_Out(.Item("RowsLeftBorderWidth"), SqlDbType.Int)
                mRowsRightBorderWidth = mDataHandler.DataValue_Out(.Item("RowsRightBorderWidth"), SqlDbType.Int)


                mScaleFactor = mDataHandler.DataValue_Out(.Item("ScaleFactor"), SqlDbType.Int)
                mXMLReport = mDataHandler.DataValue_Out(.Item("XMLReport"), SqlDbType.Xml)

                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportGroupID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportGroupID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportSource", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportSource, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngTitle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngTitle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbTitle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbTitle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RefreshTimeInterval", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRefreshTimeInterval, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRank, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyLogo", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCompanyLogo, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyText", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCompanyText, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyHeader", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCompanyHeader, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsLandscape", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsLandscape, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsRightToLeft", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsRightToLeft, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportHeader", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mReportHeader, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportFooter", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mReportFooter, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PageHeader", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mPageHeader, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PageFooter", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mPageFooter, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DataSource", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDataSource, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CRWName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCRWName, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ViewForm", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mViewForm, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderBackColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHeaderBackColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderForeColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHeaderForeColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderFont", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHeaderFont, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mHeaderBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mHeaderBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportTopMargins", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportTopMargins, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportBottomMargins", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportBottomMargins, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportLeftMargins", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportLeftMargins, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportRightMargins", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportRightMargins, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportBackColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mReportBackColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportForeColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mReportForeColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportAlternatingColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mReportAlternatingColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportFont", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mReportFont, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportFontIsItalic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mReportFontIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportFontIsBold", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mReportFontIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportFontIsUnderLine", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mReportFontIsUnderLine, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportFontSize", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderFontIsItalic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHeaderFontIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderFontIsBold", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHeaderFontIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderFontIsUnderLine", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHeaderFontIsUnderLine, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderFontSize", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mHeaderFontSize, SqlDbType.Int)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mHeaderBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderHalignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mHeaderHalignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderValignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mHeaderValignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterFont", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFooterFont, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterFontIsItalic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFooterFontIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterFontIsBold", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFooterFontIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterFontIsUnderLine", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFooterFontIsUnderLine, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterFontSize", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFooterFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterHalignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFooterHalignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterValignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFooterValignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterBackColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFooterBackColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterForeColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFooterForeColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFooterBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFooterBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFooterBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderHeight", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mHeaderHeight, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterHeight", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFooterHeight, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsForeColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsForeColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBackColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsBackColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsFont", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsFont, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsFontIsItalic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mRowsFontIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsFontIsBold", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mRowsFontIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsFontIsUnderLine", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mRowsFontIsUnderLine, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsFontSize", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsHieght", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsHieght, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsTopBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsTopBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBottomBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsBottomBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsLeftBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsLeftBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsRightBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsRightBorderColor, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBorderStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsBorderStyle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsTopBorderStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsTopBorderStyle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBottomBorderStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsBottomBorderStyle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsLeftBorderStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsLeftBorderStyle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsRightBorderStyle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRowsRightBorderStyle, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsTopBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsTopBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBottomBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsBottomBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsLeftBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsLeftBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsRightBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsRightBorderWidth, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ScaleFactor", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsRightBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@XMLReport", SqlDbType.Xml)).Value = mDataHandler.DataValue_In(mXMLReport, SqlDbType.Xml)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int)


        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region

#Region "Class Destructor"

    Protected Overloads Sub finalized()
        mDataSet.Dispose()
    End Sub

#End Region

End Class
