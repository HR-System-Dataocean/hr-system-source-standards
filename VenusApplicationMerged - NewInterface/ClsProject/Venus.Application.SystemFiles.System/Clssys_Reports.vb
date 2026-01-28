Public Class Clssys_Reports
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_Reports "
        mInsertParameter = "" & _
          "Code," & _
          "EngName," & _
          "ArbName," & _
          "ArbName4S," & _
          "EngDescription," & _
          "ArbDescription," & _
          "EngTitle," & _
          "ArbTitle," & _
          "ReportGroupID," & _
          "ParentID," & _
          "ReportSource," & _
          "DataSource," & _
          "CRWName," & _
          "ViewForm," & _
          "ShortCut," & _
          "RefreshTimeInterval," & _
          "Rank," & _
          "CompanyLogo," & _
          "CompanyHeader," & _
          "CompanyText," & _
          "IsLandScape," & _
          "IsRightToLeft," & _
          "ReportHeader," & _
          "ReportFooter," & _
          "PageHeader," & _
          "PageFooter," & _
          "HeaderBackColor," & _
          "HeaderForeColor," & _
          "HeaderFont," & _
          "HeaderBorderColor," & _
          "HeaderBorderWidth," & _
          "ReportTopMargins," & _
          "ReportBottomMargins," & _
          "ReportLeftMargins," & _
          "ReportRightMargins," & _
          "ReportBackColor," & _
          "ReportForeColor," & _
          "ReportAlternatingColor," & _
          "ReportFont," & _
          "ReportFontIsItalic," & _
          "ReportFontIsBold," & _
          "ReportFontIsUnderLine," & _
          "ReportFontSize," & _
          "HeaderFontIsBold," & _
          "HeaderFontIsItalic," & _
          "HeaderFontIsUnderLine," & _
          "HeaderFontSize," & _
          "HeaderBorderStyle," & _
          "HeaderValignment," & _
          "HeaderHalignment," & _
          "FooterFont," & _
          "FooterFontIsItalic," & _
          "FooterFontIsBold," & _
          "FooterFontIsUnderLine," & _
          "FooterFontSize," & _
          "FooterValignment," & _
          "FooterHalignment," & _
          "FooterBackColor," & _
          "FooterForeColor," & _
          "FooterBorderColor," & _
          "FooterBorderStyle," & _
          "FooterBorderWidth," & _
          "HeaderHeight," & _
          "FooterHeight," & _
          "RowsForeColor," & _
          "RowsBackColor," & _
          "RowsFont," & _
          "RowsFontIsItalic," & _
          "RowsFontIsBold," & _
          "RowsFontIsUnderLine," & _
          "RowsFontSize," & _
          "RowsHieght," & _
          "RowsBorderColor," & _
          "RowsTopBorderColor," & _
          "RowsBottomBorderColor," & _
          "RowsLeftBorderColor," & _
          "RowsRightBorderColor," & _
          "RowsBorderStyle," & _
          "RowsTopBorderStyle," & _
          "RowsBottomBorderStyle," & _
          "RowsLeftBorderStyle," & _
          "RowsRightBorderStyle," & _
          "RowsBorderWidth," & _
          "RowsTopBorderWidth," & _
          "RowsBottomBorderWidth," & _
          "RowsLeftBorderWidth," & _
          "RowsRightBorderWidth," & _
          "ScaleFactor," & _
          "Remarks," & _
          "RegUserID," & _
          "RegComputerID," & _
          "XMLReport"
        mInsertParameterValues = "" & _
          " @Code," & _
          " @EngName," & _
          " @ArbName," & _
          " @ArbName4S," & _
          " @EngDescription," & _
          " @ArbDescription," & _
          " @EngTitle," & _
          " @ArbTitle," & _
          " @ReportGroupID," & _
          " @ParentID," & _
          " @ReportSource," & _
          " @DataSource," & _
          " @CRWName," & _
          " @ViewForm," & _
          " @ShortCut," & _
          " @RefreshTimeInterval," & _
          " @Rank," & _
          " @CompanyLogo," & _
          " @CompanyHeader," & _
          " @CompanyText," & _
          " @IsLandScape," & _
          " @IsRightToLeft," & _
          " @ReportHeader," & _
          " @ReportFooter," & _
          " @PageHeader," & _
          " @PageFooter," & _
          " @HeaderBackColor," & _
          " @HeaderForeColor," & _
          " @HeaderFont," & _
          " @HeaderBorderColor," & _
          " @HeaderBorderWidth," & _
          " @ReportTopMargins," & _
          " @ReportBottomMargins," & _
          " @ReportLeftMargins," & _
          " @ReportRightMargins," & _
          " @ReportBackColor," & _
          " @ReportForeColor," & _
          " @ReportAlternatingColor," & _
          " @ReportFont," & _
          " @ReportFontIsItalic," & _
          " @ReportFontIsBold," & _
          " @ReportFontIsUnderLine," & _
          " @ReportFontSize," & _
          " @HeaderFontIsBold," & _
          " @HeaderFontIsItalic," & _
          " @HeaderFontIsUnderLine," & _
          " @HeaderFontSize," & _
          " @HeaderBorderStyle," & _
          " @HeaderValignment," & _
          " @HeaderHalignment," & _
          " @FooterFont," & _
          " @FooterFontIsItalic," & _
          " @FooterFontIsBold," & _
          " @FooterFontIsUnderLine," & _
          " @FooterFontSize," & _
          " @FooterValignment," & _
          " @FooterHalignment," & _
          " @FooterBackColor," & _
          " @FooterForeColor," & _
          " @FooterBorderColor," & _
          " @FooterBorderStyle," & _
          " @FooterBorderWidth," & _
          " @HeaderHeight," & _
          " @FooterHeight," & _
          " @RowsForeColor," & _
          " @RowsBackColor," & _
          " @RowsFont," & _
          " @RowsFontIsItalic," & _
          " @RowsFontIsBold," & _
          " @RowsFontIsUnderLine," & _
          " @RowsFontSize," & _
          " @RowsHieght," & _
          " @RowsBorderColor," & _
          " @RowsTopBorderColor," & _
          " @RowsBottomBorderColor," & _
          " @RowsLeftBorderColor," & _
          " @RowsRightBorderColor," & _
          " @RowsBorderStyle," & _
          " @RowsTopBorderStyle," & _
          " @RowsBottomBorderStyle," & _
          " @RowsLeftBorderStyle," & _
          " @RowsRightBorderStyle," & _
          " @RowsBorderWidth," & _
          " @RowsTopBorderWidth," & _
          " @RowsBottomBorderWidth," & _
          " @RowsLeftBorderWidth," & _
          " @RowsRightBorderWidth," & _
          " @ScaleFactor," & _
          " @Remarks," & _
          " @RegUserID," & _
          " @RegComputerID," & _
          " @XMLReport"
        mUpdateParameter = "" & _
          "Code=@Code," & _
          "EngName=@EngName," & _
          "ArbName=@ArbName," & _
          "ArbName4S=@ArbName4S," & _
          "EngDescription=@EngDescription," & _
          "ArbDescription=@ArbDescription," & _
          "EngTitle=@EngTitle," & _
          "ArbTitle=@ArbTitle," & _
          "ReportGroupID=@ReportGroupID," & _
          "ParentID=@ParentID," & _
          "ReportSource=@ReportSource," & _
          "DataSource=@DataSource," & _
          "CRWName=@CRWName," & _
          "ViewForm=@ViewForm," & _
          "ShortCut=@ShortCut," & _
          "RefreshTimeInterval=@RefreshTimeInterval," & _
          "Rank=@Rank," & _
          "CompanyLogo=@CompanyLogo," & _
          "CompanyHeader=@CompanyHeader," & _
          "CompanyText=@CompanyText," & _
          "IsLandScape=@IsLandScape," & _
          "IsRightToLeft=@IsRightToLeft," & _
          "ReportHeader=@ReportHeader," & _
          "ReportFooter=@ReportFooter," & _
          "PageHeader=@PageHeader," & _
          "PageFooter=@PageFooter," & _
          "HeaderBackColor=@HeaderBackColor," & _
          "HeaderForeColor=@HeaderForeColor," & _
          "HeaderFont=@HeaderFont," & _
          "HeaderBorderColor=@HeaderBorderColor," & _
          "HeaderBorderWidth=@HeaderBorderWidth," & _
          "ReportTopMargins=@ReportTopMargins," & _
          "ReportBottomMargins=@ReportBottomMargins," & _
          "ReportLeftMargins=@ReportLeftMargins," & _
          "ReportRightMargins=@ReportRightMargins," & _
          "ReportBackColor=@ReportBackColor," & _
          "ReportForeColor=@ReportForeColor," & _
          "ReportAlternatingColor=@ReportAlternatingColor," & _
          "ReportFont=@ReportFont," & _
          "ReportFontIsItalic=@ReportFontIsItalic," & _
          "ReportFontIsBold=@ReportFontIsBold," & _
          "ReportFontIsUnderLine=@ReportFontIsUnderLine," & _
          "ReportFontSize=@ReportFontSize," & _
          "HeaderFontIsBold=@HeaderFontIsBold," & _
          "HeaderFontIsItalic=@HeaderFontIsItalic," & _
          "HeaderFontIsUnderLine=@HeaderFontIsUnderLine," & _
          "HeaderFontSize=@HeaderFontSize," & _
          "HeaderBorderStyle=@HeaderBorderStyle," & _
          "HeaderValignment=@HeaderValignment," & _
          "HeaderHalignment=@HeaderHalignment," & _
          "FooterFont=@FooterFont," & _
          "FooterFontIsItalic=@FooterFontIsItalic," & _
          "FooterFontIsBold=@FooterFontIsBold," & _
          "FooterFontIsUnderLine=@FooterFontIsUnderLine," & _
          "FooterFontSize=@FooterFontSize," & _
          "FooterValignment=@FooterValignment," & _
          "FooterHalignment=@FooterHalignment," & _
          "FooterBackColor=@FooterBackColor," & _
          "FooterForeColor=@FooterForeColor," & _
          "FooterBorderColor=@FooterBorderColor," & _
          "FooterBorderStyle=@FooterBorderStyle," & _
          "FooterBorderWidth=@FooterBorderWidth," & _
          "HeaderHeight=@HeaderHeight," & _
          "FooterHeight=@FooterHeight," & _
          "RowsForeColor=@RowsForeColor," & _
          "RowsBackColor=@RowsBackColor," & _
          "RowsFont=@RowsFont," & _
          "RowsFontIsItalic=@RowsFontIsItalic," & _
          "RowsFontIsBold=@RowsFontIsBold," & _
          "RowsFontIsUnderLine=@RowsFontIsUnderLine," & _
          "RowsFontSize=@RowsFontSize," & _
          "RowsHieght=@RowsHieght," & _
          "RowsBorderColor=@RowsBorderColor," & _
          "RowsTopBorderColor=@RowsTopBorderColor," & _
          "RowsBottomBorderColor=@RowsBottomBorderColor," & _
          "RowsLeftBorderColor=@RowsLeftBorderColor," & _
          "RowsRightBorderColor=@RowsRightBorderColor," & _
          "RowsBorderStyle=@RowsBorderStyle," & _
          "RowsTopBorderStyle=@RowsTopBorderStyle," & _
          "RowsBottomBorderStyle=@RowsBottomBorderStyle," & _
          "RowsLeftBorderStyle=@RowsLeftBorderStyle," & _
          "RowsRightBorderStyle=@RowsRightBorderStyle," & _
          "RowsBorderWidth=@RowsBorderWidth," & _
          "RowsTopBorderWidth=@RowsTopBorderWidth," & _
          "RowsBottomBorderWidth=@RowsBottomBorderWidth," & _
          "RowsLeftBorderWidth=@RowsLeftBorderWidth," & _
          "RowsRightBorderWidth=@RowsRightBorderWidth," & _
          "ScaleFactor=@ScaleFactor," & _
          "Remarks=@Remarks," & _
          "XMLReport=@XMLReport"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
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
    Private mEngTitle As String
    Private mArbTitle As String
    Private mReportGroupID As Integer
    Private mParentID As Integer
    Private mReportSource As Integer
    Private mDataSource As String
    Private mCRWName As String
    Private mViewForm As String
    Private mShortCut As String
    Private mRefreshTimeInterval As Integer
    Private mRank As Integer
    Private mCompanyLogo As Boolean
    Private mCompanyHeader As Boolean
    Private mCompanyText As Boolean
    Private mIsLandScape As Boolean
    Private mIsRightToLeft As Boolean
    Private mReportHeader As Boolean
    Private mReportFooter As Boolean
    Private mPageHeader As Boolean
    Private mPageFooter As Boolean
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
    Private mHeaderFontIsBold As Boolean
    Private mHeaderFontIsItalic As Boolean
    Private mHeaderFontIsUnderLine As Boolean
    Private mHeaderFontSize As Integer
    Private mHeaderBorderStyle As Integer
    Private mHeaderValignment As Boolean
    Private mHeaderHalignment As Boolean
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
    Private mRowsTopBorderColor As String
    Private mRowsBottomBorderColor As String
    Private mRowsLeftBorderColor As String
    Private mRowsRightBorderColor As String
    Private mRowsBorderStyle As Integer
    Private mRowsTopBorderStyle As Integer
    Private mRowsBottomBorderStyle As Integer
    Private mRowsLeftBorderStyle As Integer
    Private mRowsRightBorderStyle As Integer
    Private mRowsBorderWidth As Integer
    Private mRowsTopBorderWidth As Integer
    Private mRowsBottomBorderWidth As Integer
    Private mRowsLeftBorderWidth As Integer
    Private mRowsRightBorderWidth As Integer
    Private mScaleFactor As Integer
    Private mXMLReport As String
    Private mRemarks As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As DateTime
    Private mCancelDate As DateTime

#End Region
#Region "Public property"
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
        End Set
    End Property
    Public Property ArbName4S() As String
        Get
            Return mArbName4S
        End Get
        Set(ByVal Value As String)
            mArbName4S = Value
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
    Public Property EngTitle() As String
        Get
            Return mEngTitle
        End Get
        Set(ByVal Value As String)
            mEngTitle = Value
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
    Public Property ReportGroupID() As Integer
        Get
            Return mReportGroupID
        End Get
        Set(ByVal Value As Integer)
            mReportGroupID = Value
        End Set
    End Property
    Public Property ParentID() As Integer
        Get
            Return mParentID
        End Get
        Set(ByVal Value As Integer)
            mParentID = Value
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
        Set(ByVal Value As String)
            mViewForm = Value
        End Set
    End Property
    Public Property ShortCut() As String
        Get
            Return mShortCut
        End Get
        Set(ByVal Value As String)
            mShortCut = Value
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
    Public Property CompanyHeader() As Boolean
        Get
            Return mCompanyHeader
        End Get
        Set(ByVal Value As Boolean)
            mCompanyHeader = Value
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
    Public Property IsLandScape() As Boolean
        Get
            Return mIsLandScape
        End Get
        Set(ByVal Value As Boolean)
            mIsLandScape = Value
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
    Public Property HeaderFontIsBold() As Boolean
        Get
            Return mHeaderFontIsBold
        End Get
        Set(ByVal Value As Boolean)
            mHeaderFontIsBold = Value
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
    Public Property HeaderValignment() As Boolean
        Get
            Return mHeaderValignment
        End Get
        Set(ByVal Value As Boolean)
            mHeaderValignment = Value
        End Set
    End Property
    Public Property HeaderHalignment() As Boolean
        Get
            Return mHeaderHalignment
        End Get
        Set(ByVal Value As Boolean)
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
    Public Property RowsBorderStyle() As Integer
        Get
            Return mRowsBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mRowsBorderStyle = Value
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
    Public Property RowsBorderWidth() As Integer
        Get
            Return mRowsBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mRowsBorderWidth = Value
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
        Set(ByVal Value As Integer)
            mScaleFactor = Value
        End Set
    End Property
    Public Property XMLReport() As String
        Get
            Return mXMLReport
        End Get
        Set(ByVal Value As String)
            mXMLReport = Value
        End Set
    End Property
    Public Property Remarks() As String
        Get
            Return mRemarks
        End Get
        Set(ByVal Value As String)
            mRemarks = Value
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
    Public Property RegDate() As DateTime
        Get
            Return mRegDate
        End Get
        Set(ByVal Value As DateTime)
            mRegDate = Value
        End Set
    End Property
    Public Property CancelDate() As DateTime
        Get
            Return mCancelDate
        End Get
        Set(ByVal Value As DateTime)
            mCancelDate = Value
        End Set
    End Property
#End Region
#Region "Public Function"
    '========================================================================
    'ProcedureName  :  GetList
    'Project        :  Fisalia Module
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean  
    'Date Created   : 10/05/2014
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrCommandString = "Select * From " & Me.mTable & " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()
            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/ [ برجاء الاختيار ] ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
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
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetDropDownList
    'Project        :  Fisalia Module
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean  
    'Date Created   : 10/05/2014
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnectionString)
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter & " Order By EngName", "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 Order By EngName ")
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()
            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/ [ برجاء الاختيار ]")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngName/ArbName")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow(ID)
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

    '========================================================================
    'ProcedureName  :  Find 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :10/05/2014
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, " And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataset = New DataSet
            mSqlDataAdapter.Fill(mDataset)
            If mDataHandler.CheckValidDataObject(mDataset) Then
                GetParameter(mDataset)
            Else
                Clear()
            End If
            If mID > 0 Then
                Return True
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :   Save 
    'Module         :   (Fisalia Module)
    'Project        :   Fisalia Module
    'Description    :   Save new record and return true if operation done otherwise report errors in ErrorPage
    'Developer      :   DataOcean   
    'Date Created   :   10/05/2014
    'Modifacations  :   
    'fn. Arguments  :   
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Save() As Boolean
        Try
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = mInsertCommand
            SetParameter(mSqlCommand, "Save")
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Update 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :10/05/2014
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Dim CLsWebHandlar As New Venus.Shared.Web.WebHandler()
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand, "Update")
            CLsWebHandlar.Add2History(mConnectionString, mID, mTable, "", "", "", Me.mDataBaseUserRelatedID, mSqlCommand, "")
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Delete 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Delete Table row (set Cancel Date)
    'Developer      :  DataOcean   
    'Date Created   :10/05/2014 12:43:05
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function Delete(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = mDeleteCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrDeleteCommand
            SetParameter(mSqlCommand, "Delete")
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrDeleteCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Clear 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Clear Table Columns
    'Developer      :  DataOcean   
    'Date Created   :10/05/2014
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
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
            mReportGroupID = 0
            mParentID = 0
            mReportSource = 0
            mDataSource = String.Empty
            mCRWName = String.Empty
            mViewForm = String.Empty
            mShortCut = String.Empty
            mRefreshTimeInterval = 0
            mRank = 0
            mCompanyLogo = False
            mCompanyHeader = False
            mCompanyText = False
            mIsLandScape = False
            mIsRightToLeft = False
            mReportHeader = False
            mReportFooter = False
            mPageHeader = False
            mPageFooter = False
            mHeaderBackColor = String.Empty
            mHeaderForeColor = String.Empty
            mHeaderFont = String.Empty
            mHeaderBorderColor = String.Empty
            mHeaderBorderWidth = 0
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
            mHeaderFontIsBold = False
            mHeaderFontIsItalic = False
            mHeaderFontIsUnderLine = False
            mHeaderFontSize = 0
            mHeaderBorderStyle = 0
            mHeaderValignment = False
            mHeaderHalignment = False
            mFooterFont = String.Empty
            mFooterFontIsItalic = False
            mFooterFontIsBold = False
            mFooterFontIsUnderLine = False
            mFooterFontSize = 0
            mFooterValignment = 0
            mFooterHalignment = 0
            mFooterBackColor = String.Empty
            mFooterForeColor = String.Empty
            mFooterBorderColor = String.Empty
            mFooterBorderStyle = 0
            mFooterBorderWidth = 0
            mHeaderHeight = 0
            mFooterHeight = 0
            mRowsForeColor = String.Empty
            mRowsBackColor = String.Empty
            mRowsFont = String.Empty
            mRowsFontIsItalic = False
            mRowsFontIsBold = False
            mRowsFontIsUnderLine = False
            mRowsFontSize = 0
            mRowsHieght = 0
            mRowsBorderColor = String.Empty
            mRowsTopBorderColor = String.Empty
            mRowsBottomBorderColor = String.Empty
            mRowsLeftBorderColor = String.Empty
            mRowsRightBorderColor = String.Empty
            mRowsBorderStyle = 0
            mRowsTopBorderStyle = 0
            mRowsBottomBorderStyle = 0
            mRowsLeftBorderStyle = 0
            mRowsRightBorderStyle = 0
            mRowsBorderWidth = 0
            mRowsTopBorderWidth = 0
            mRowsBottomBorderWidth = 0
            mRowsLeftBorderWidth = 0
            mRowsRightBorderWidth = 0
            mScaleFactor = 0
            mXMLReport = String.Empty
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function FindUsersReportsPermission(ByVal UserID As Integer, ByVal Code As String, ByVal EngName As String, ByVal ArbName As String, ByRef Ds As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, "hrs_GetUsersReportsPermissions", UserID, Code, EngName, ArbName)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function FindGroupsReportsPermission(ByVal GroupID As Integer, ByVal Code As String, ByVal EngName As String, ByVal ArbName As String, ByRef Ds As DataSet) As Boolean
        Dim StrSelectCommand As String = String.Empty
        Try
            Ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Me.ConnectionString, "hrs_GetGroupsReportsPermissions", GroupID, Code, EngName, ArbName)
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function GetUsersReportsPermissionsCounts(ByVal IntUserID As Integer, ByVal StrCode As String, ByVal StrEngName As String, ByVal StrArbName As String) As DataSet
        Dim DS As DataSet
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetUsersReportsPermissionsCounter", IntUserID, StrCode, StrEngName, StrArbName)
        Return DS
    End Function

    Public Function GetGroupsReportsPermissionsCounts(ByVal IntGroupID As Integer, ByVal StrCode As String, ByVal StrEngName As String, ByVal StrArbName As String) As DataSet
        Dim DS As DataSet
        DS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, "hrs_GetGroupsReportsPermissionsCounter", IntGroupID, StrCode, StrEngName, StrArbName)
        Return DS
    End Function
#End Region
#Region "Class Private Function"
    '========================================================================
    'ProcedureName  :  GetParameter 
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :10/05/2014
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = [Shared].DataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mCode = [Shared].DataHandler.DataValue_Out(.Item("Code"), SqlDbType.VarChar)
                mEngName = [Shared].DataHandler.DataValue_Out(.Item("EngName"), SqlDbType.VarChar)
                mArbName = [Shared].DataHandler.DataValue_Out(.Item("ArbName"), SqlDbType.VarChar)
                mArbName4S = [Shared].DataHandler.DataValue_Out(.Item("ArbName4S"), SqlDbType.VarChar)
                mEngDescription = [Shared].DataHandler.DataValue_Out(.Item("EngDescription"), SqlDbType.VarChar)
                mArbDescription = [Shared].DataHandler.DataValue_Out(.Item("ArbDescription"), SqlDbType.VarChar)
                mEngTitle = [Shared].DataHandler.DataValue_Out(.Item("EngTitle"), SqlDbType.VarChar)
                mArbTitle = [Shared].DataHandler.DataValue_Out(.Item("ArbTitle"), SqlDbType.VarChar)
                mReportGroupID = [Shared].DataHandler.DataValue_Out(.Item("ReportGroupID"), SqlDbType.Int, True)
                mParentID = [Shared].DataHandler.DataValue_Out(.Item("ParentID"), SqlDbType.Int, True)
                mReportSource = [Shared].DataHandler.DataValue_Out(.Item("ReportSource"), SqlDbType.Int, True)
                mDataSource = [Shared].DataHandler.DataValue_Out(.Item("DataSource"), SqlDbType.VarChar)
                mCRWName = [Shared].DataHandler.DataValue_Out(.Item("CRWName"), SqlDbType.VarChar)
                mViewForm = [Shared].DataHandler.DataValue_Out(.Item("ViewForm"), SqlDbType.VarChar)
                mShortCut = [Shared].DataHandler.DataValue_Out(.Item("ShortCut"), SqlDbType.VarChar)
                mRefreshTimeInterval = [Shared].DataHandler.DataValue_Out(.Item("RefreshTimeInterval"), SqlDbType.Int, True)
                mRank = [Shared].DataHandler.DataValue_Out(.Item("Rank"), SqlDbType.Int, True)
                mCompanyLogo = [Shared].DataHandler.DataValue_Out(.Item("CompanyLogo"), SqlDbType.Bit)
                mCompanyHeader = [Shared].DataHandler.DataValue_Out(.Item("CompanyHeader"), SqlDbType.Bit)
                mCompanyText = [Shared].DataHandler.DataValue_Out(.Item("CompanyText"), SqlDbType.Bit)
                mIsLandScape = [Shared].DataHandler.DataValue_Out(.Item("IsLandScape"), SqlDbType.Bit)
                mIsRightToLeft = [Shared].DataHandler.DataValue_Out(.Item("IsRightToLeft"), SqlDbType.Bit)
                mReportHeader = [Shared].DataHandler.DataValue_Out(.Item("ReportHeader"), SqlDbType.Bit)
                mReportFooter = [Shared].DataHandler.DataValue_Out(.Item("ReportFooter"), SqlDbType.Bit)
                mPageHeader = [Shared].DataHandler.DataValue_Out(.Item("PageHeader"), SqlDbType.Bit)
                mPageFooter = [Shared].DataHandler.DataValue_Out(.Item("PageFooter"), SqlDbType.Bit)
                mHeaderBackColor = [Shared].DataHandler.DataValue_Out(.Item("HeaderBackColor"), SqlDbType.VarChar)
                mHeaderForeColor = [Shared].DataHandler.DataValue_Out(.Item("HeaderForeColor"), SqlDbType.VarChar)
                mHeaderFont = [Shared].DataHandler.DataValue_Out(.Item("HeaderFont"), SqlDbType.VarChar)
                mHeaderBorderColor = [Shared].DataHandler.DataValue_Out(.Item("HeaderBorderColor"), SqlDbType.VarChar)
                mHeaderBorderWidth = [Shared].DataHandler.DataValue_Out(.Item("HeaderBorderWidth"), SqlDbType.Int, True)
                mReportTopMargins = [Shared].DataHandler.DataValue_Out(.Item("ReportTopMargins"), SqlDbType.Int, True)
                mReportBottomMargins = [Shared].DataHandler.DataValue_Out(.Item("ReportBottomMargins"), SqlDbType.Int, True)
                mReportLeftMargins = [Shared].DataHandler.DataValue_Out(.Item("ReportLeftMargins"), SqlDbType.Int, True)
                mReportRightMargins = [Shared].DataHandler.DataValue_Out(.Item("ReportRightMargins"), SqlDbType.Int, True)
                mReportBackColor = [Shared].DataHandler.DataValue_Out(.Item("ReportBackColor"), SqlDbType.VarChar)
                mReportForeColor = [Shared].DataHandler.DataValue_Out(.Item("ReportForeColor"), SqlDbType.VarChar)
                mReportAlternatingColor = [Shared].DataHandler.DataValue_Out(.Item("ReportAlternatingColor"), SqlDbType.VarChar)
                mReportFont = [Shared].DataHandler.DataValue_Out(.Item("ReportFont"), SqlDbType.VarChar)
                mReportFontIsItalic = [Shared].DataHandler.DataValue_Out(.Item("ReportFontIsItalic"), SqlDbType.Bit)
                mReportFontIsBold = [Shared].DataHandler.DataValue_Out(.Item("ReportFontIsBold"), SqlDbType.Bit)
                mReportFontIsUnderLine = [Shared].DataHandler.DataValue_Out(.Item("ReportFontIsUnderLine"), SqlDbType.Bit)
                mReportFontSize = [Shared].DataHandler.DataValue_Out(.Item("ReportFontSize"), SqlDbType.Int, True)
                mHeaderFontIsBold = [Shared].DataHandler.DataValue_Out(.Item("HeaderFontIsBold"), SqlDbType.Bit)
                mHeaderFontIsItalic = [Shared].DataHandler.DataValue_Out(.Item("HeaderFontIsItalic"), SqlDbType.Bit)
                mHeaderFontIsUnderLine = [Shared].DataHandler.DataValue_Out(.Item("HeaderFontIsUnderLine"), SqlDbType.Bit)
                mHeaderFontSize = [Shared].DataHandler.DataValue_Out(.Item("HeaderFontSize"), SqlDbType.Int, True)
                mHeaderBorderStyle = [Shared].DataHandler.DataValue_Out(.Item("HeaderBorderStyle"), SqlDbType.Int, True)
                mHeaderValignment = [Shared].DataHandler.DataValue_Out(.Item("HeaderValignment"), SqlDbType.Bit)
                mHeaderHalignment = [Shared].DataHandler.DataValue_Out(.Item("HeaderHalignment"), SqlDbType.Bit)
                mFooterFont = [Shared].DataHandler.DataValue_Out(.Item("FooterFont"), SqlDbType.VarChar)
                mFooterFontIsItalic = [Shared].DataHandler.DataValue_Out(.Item("FooterFontIsItalic"), SqlDbType.Bit)
                mFooterFontIsBold = [Shared].DataHandler.DataValue_Out(.Item("FooterFontIsBold"), SqlDbType.Bit)
                mFooterFontIsUnderLine = [Shared].DataHandler.DataValue_Out(.Item("FooterFontIsUnderLine"), SqlDbType.Bit)
                mFooterFontSize = [Shared].DataHandler.DataValue_Out(.Item("FooterFontSize"), SqlDbType.Int, True)
                mFooterValignment = [Shared].DataHandler.DataValue_Out(.Item("FooterValignment"), SqlDbType.Int, True)
                mFooterHalignment = [Shared].DataHandler.DataValue_Out(.Item("FooterHalignment"), SqlDbType.Int, True)
                mFooterBackColor = [Shared].DataHandler.DataValue_Out(.Item("FooterBackColor"), SqlDbType.VarChar)
                mFooterForeColor = [Shared].DataHandler.DataValue_Out(.Item("FooterForeColor"), SqlDbType.VarChar)
                mFooterBorderColor = [Shared].DataHandler.DataValue_Out(.Item("FooterBorderColor"), SqlDbType.VarChar)
                mFooterBorderStyle = [Shared].DataHandler.DataValue_Out(.Item("FooterBorderStyle"), SqlDbType.Int, True)
                mFooterBorderWidth = [Shared].DataHandler.DataValue_Out(.Item("FooterBorderWidth"), SqlDbType.Int, True)
                mHeaderHeight = [Shared].DataHandler.DataValue_Out(.Item("HeaderHeight"), SqlDbType.Int, True)
                mFooterHeight = [Shared].DataHandler.DataValue_Out(.Item("FooterHeight"), SqlDbType.Int, True)
                mRowsForeColor = [Shared].DataHandler.DataValue_Out(.Item("RowsForeColor"), SqlDbType.VarChar)
                mRowsBackColor = [Shared].DataHandler.DataValue_Out(.Item("RowsBackColor"), SqlDbType.VarChar)
                mRowsFont = [Shared].DataHandler.DataValue_Out(.Item("RowsFont"), SqlDbType.VarChar)
                mRowsFontIsItalic = [Shared].DataHandler.DataValue_Out(.Item("RowsFontIsItalic"), SqlDbType.Bit)
                mRowsFontIsBold = [Shared].DataHandler.DataValue_Out(.Item("RowsFontIsBold"), SqlDbType.Bit)
                mRowsFontIsUnderLine = [Shared].DataHandler.DataValue_Out(.Item("RowsFontIsUnderLine"), SqlDbType.Bit)
                mRowsFontSize = [Shared].DataHandler.DataValue_Out(.Item("RowsFontSize"), SqlDbType.Int, True)
                mRowsHieght = [Shared].DataHandler.DataValue_Out(.Item("RowsHieght"), SqlDbType.Int, True)
                mRowsBorderColor = [Shared].DataHandler.DataValue_Out(.Item("RowsBorderColor"), SqlDbType.VarChar)
                mRowsTopBorderColor = [Shared].DataHandler.DataValue_Out(.Item("RowsTopBorderColor"), SqlDbType.VarChar)
                mRowsBottomBorderColor = [Shared].DataHandler.DataValue_Out(.Item("RowsBottomBorderColor"), SqlDbType.VarChar)
                mRowsLeftBorderColor = [Shared].DataHandler.DataValue_Out(.Item("RowsLeftBorderColor"), SqlDbType.VarChar)
                mRowsRightBorderColor = [Shared].DataHandler.DataValue_Out(.Item("RowsRightBorderColor"), SqlDbType.VarChar)
                mRowsBorderStyle = [Shared].DataHandler.DataValue_Out(.Item("RowsBorderStyle"), SqlDbType.Int, True)
                mRowsTopBorderStyle = [Shared].DataHandler.DataValue_Out(.Item("RowsTopBorderStyle"), SqlDbType.Int, True)
                mRowsBottomBorderStyle = [Shared].DataHandler.DataValue_Out(.Item("RowsBottomBorderStyle"), SqlDbType.Int, True)
                mRowsLeftBorderStyle = [Shared].DataHandler.DataValue_Out(.Item("RowsLeftBorderStyle"), SqlDbType.Int, True)
                mRowsRightBorderStyle = [Shared].DataHandler.DataValue_Out(.Item("RowsRightBorderStyle"), SqlDbType.Int, True)
                mRowsBorderWidth = [Shared].DataHandler.DataValue_Out(.Item("RowsBorderWidth"), SqlDbType.Int, True)
                mRowsTopBorderWidth = [Shared].DataHandler.DataValue_Out(.Item("RowsTopBorderWidth"), SqlDbType.Int, True)
                mRowsBottomBorderWidth = [Shared].DataHandler.DataValue_Out(.Item("RowsBottomBorderWidth"), SqlDbType.Int, True)
                mRowsLeftBorderWidth = [Shared].DataHandler.DataValue_Out(.Item("RowsLeftBorderWidth"), SqlDbType.Int, True)
                mRowsRightBorderWidth = [Shared].DataHandler.DataValue_Out(.Item("RowsRightBorderWidth"), SqlDbType.Int, True)
                mScaleFactor = [Shared].DataHandler.DataValue_Out(.Item("ScaleFactor"), SqlDbType.Int, True)
                mXMLReport = [Shared].DataHandler.DataValue_Out(.Item("XMLReport"), SqlDbType.VarChar)
                mRemarks = [Shared].DataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  SetParameter
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   : 10/05/2014 12:43:05
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCode, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbName4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbDescription", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbDescription, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngTitle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngTitle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbTitle", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbTitle, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportGroupID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportGroupID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ParentID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mParentID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportSource", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportSource, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DataSource", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDataSource, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CRWName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mCRWName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ViewForm", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mViewForm, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ShortCut", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mShortCut, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RefreshTimeInterval", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRefreshTimeInterval, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRank, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyLogo", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCompanyLogo, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyHeader", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCompanyHeader, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CompanyText", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCompanyText, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsLandScape", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsLandScape, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsRightToLeft", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsRightToLeft, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportHeader", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mReportHeader, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportFooter", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mReportFooter, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PageHeader", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mPageHeader, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@PageFooter", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mPageFooter, SqlDbType.Bit)
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderFontIsBold", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHeaderFontIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderFontIsItalic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHeaderFontIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderFontIsUnderLine", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHeaderFontIsUnderLine, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderFontSize", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mHeaderFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mHeaderBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderValignment", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHeaderValignment, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HeaderHalignment", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHeaderHalignment, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterFont", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFooterFont, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterFontIsItalic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFooterFontIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterFontIsBold", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFooterFontIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterFontIsUnderLine", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mFooterFontIsUnderLine, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterFontSize", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFooterFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterValignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFooterValignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterHalignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFooterHalignment, SqlDbType.Int)
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
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsTopBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsTopBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBottomBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsBottomBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsLeftBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsLeftBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsRightBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsRightBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsTopBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsTopBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsBottomBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsBottomBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsLeftBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsLeftBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RowsRightBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRowsRightBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ScaleFactor", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mScaleFactor, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@XMLReport", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mXMLReport, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            If (strMode.Trim.ToUpper = "SAVE") Then
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region
#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         : (Fisalia Module)
    'Project        :  Fisalia Module
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   : 10/05/2014 12:43:05
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code ASC"
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(StrSelectCommand, mConnectionString)
            mDataset = New DataSet
            mSqlDataAdapter.Fill(mDataset)
            If mDataHandler.CheckValidDataObject(mDataset) Then
                GetParameter(mDataset)
                Return True
            Else
                Clear()
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code DESC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function NextRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code >'" & mCode & "' And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code ASC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    Public Function previousRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE Code <'" & mCode & "' And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY Code DESC"
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
#End Region
#Region "Class Destructors"
    Public Sub finalized()
        mDataset.Dispose()
    End Sub
#End Region
End Class
