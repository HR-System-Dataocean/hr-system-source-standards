
'=========================================================================
'
'Date : 30/12/2007
'                   Class: ColumnsProperties
'                   Table: 
'=========================================================================
'Modifications  : 
'               : [0256] 21-1-2008 Add ColumnHeaderValignment,ColumnHeaderHalignment Fields to class

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState

Public Class ClsRpw_ReportColumnsProperties
    Inherits ClsDataAcessLayer

#Region "Constants"
    Const CsDefaultColumnWidth = 100
    Const CsWhiteColor = "White"
    Const CsBlackColor = "Black"
#End Region
#Region "Class Constructor"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " rpw_ReportsColumnsProperties "
        mInsertParameter = " IsCalculated,rank,ReportID,BorderColor,TopBorderColor,BottomBorderColor,LeftBorderColor,RightBorderColor,BorderStyle,TopBorderStyle," & _
        "BottomBorderStyle,LeftBorderStyle,RightBorderStyle,BorderWidth,TopBorderWidth,BottomBorderWidth,LeftBorderWidth,RightBorderWidth," & _
        "EngHeaderCaption,EngFooterCaption,ArbHeaderCaption,ArbFooterCaption,ColumnName,ColumnWidth,ColumnValignment,ColumnHalignment,ColumnIsHidden," & _
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


        mInsertParameterValues = "@IsCalculated,@rank, @ReportID,@BorderColor,@TopBorderColor,@BottomBorderColor,@LeftBorderColor,@RightBorderColor,@BorderStyle,@TopBorderStyle," & _
        "@BottomBorderStyle,@LeftBorderStyle,@RightBorderStyle,@BorderWidth,@TopBorderWidth,@BottomBorderWidth,@LeftBorderWidth,@RightBorderWidth," & _
        "@EngHeaderCaption,@EngFooterCaption,@ArbHeaderCaption,@ArbFooterCaption,@ColumnName,@ColumnWidth,@ColumnValignment,@ColumnHalignment,@ColumnIsHidden," & _
        "@ColumnIsWrap,@ColumnIsOverView,@ColumnForeColor,@ColumnBackColor,@ColumnFont,@ColumnFontIsBold,@ColumnFontIsItalic,@ColumnFontIsUnderLine,@ColumnFontSize," & _
        "@TopPadding,@BottomPadding,@LeftPadding,@RightPadding,@TopMargin,@BottomMargin,@LeftMargin,@RightMargin,@Format,@FooterTotal,@IsSorted,@IsGroupBy ,@Formula,@GroupByRank,@ColumnHeaderHalignment,@ColumnHeaderValignment,@FieldLanguage," & _
        "@ColHeaderBackColor,@ColHeaderForeColor,@ColHeaderFont,@ColHeaderFontSize,@ColHeaderFontIsItalic,@ColHeaderFontIsBold,@ColHeaderFontIsUnderline , " & _
        "@ColHeaderBorderWidth,@ColHeaderBorderStyle,@ColHeaderBorderColor,@ColHeaderFontIsStrikeOut,@ColHeaderFontIsOverLine,@ColHeaderPaddingLeft, " & _
        "@ColHeaderPaddingRight,@ColHeaderPaddingTop,@ColHeaderPaddingBottom,@Ucode," & _
        "@ColFooterBackColor,@ColFooterForeColor,@ColFooterFont,@ColFooterFontSize,@ColFooterFontIsItalic,@ColFooterFontIsBold,@ColFooterFontIsUnderline , " & _
        "@ColFooterBorderWidth,@ColFooterBorderStyle,@ColFooterBorderColor,@ColFooterFontIsStrikeOut,@ColFooterFontIsOverLine,@ColFooterPaddingLeft, " & _
        "@ColFooterPaddingRight,@ColFooterPaddingTop,@ColFooterPaddingBottom,@ColumnFooterHalignment,@ColumnFooterValignment," & _
        "@ColHeaderBorderStyleTop,@ColHeaderBorderStyleBottom,@ColHeaderBorderStyleLeft,@ColHeaderBorderStyleRight, " & _
        "@ColHeaderBorderColorTop,@ColHeaderBorderColorBottom,@ColHeaderBorderColorLeft,@ColHeaderBorderColorRight, " & _
        "@ColHeaderBorderWidthTop,@ColHeaderBorderWidthBottom,@ColHeaderBorderWidthLeft,@ColHeaderBorderWidthRight, " & _
        "@ColFooterBorderStyleTop,@ColFooterBorderStyleBottom,@ColFooterBorderStyleLeft,@ColFooterBorderStyleRight, " & _
        "@ColFooterBorderColorTop,@ColFooterBorderColorBottom,@ColFooterBorderColorLeft,@ColFooterBorderColorRight, " & _
        "@ColFooterBorderWidthTop,@ColFooterBorderWidthBottom,@ColFooterBorderWidthLeft,@ColFooterBorderWidthRight , @Status , @ColFooterSummaryType ,@ColHeaderSorting ,@DataType  "




        mUpdateParameter = " IsCalculated=@IsCalculated,rank =@rank ,ReportID=@ReportID, BorderColor=@BorderColor, TopBorderColor=@TopBorderColor, BottomBorderColor=@BottomBorderColor, LeftBorderColor=@LeftBorderColor, " & _
        "RightBorderColor=@RightBorderColor, BorderStyle=@BorderStyle, TopBorderStyle=@TopBorderStyle, BottomBorderStyle=@BottomBorderStyle, LeftBorderStyle=@LeftBorderStyle, " & _
        "RightBorderStyle=@RightBorderStyle, BorderWidth=@BorderWidth, TopBorderWidth=@TopBorderWidth, BottomBorderWidth=@BottomBorderWidth, LeftBorderWidth=@LeftBorderWidth, " & _
        "RightBorderWidth=@RightBorderWidth, EngHeaderCaption=@EngHeaderCaption, EngFooterCaption=@EngFooterCaption, ArbHeaderCaption=@ArbHeaderCaption, " & _
        "ArbFooterCaption=@ArbFooterCaption, ColumnName=@ColumnName, ColumnWidth=@ColumnWidth, ColumnValignment=@ColumnValignment, ColumnHalignment=@ColumnHalignment, " & _
        "ColumnIsHidden=@ColumnIsHidden, ColumnIsWrap=@ColumnIsWrap, ColumnIsOverView=@ColumnIsOverView, ColumnForeColor=@ColumnForeColor, ColumnBackColor=@ColumnBackColor, ColumnFont=@ColumnFont," & _
        "ColumnFontIsBold=@ColumnFontIsBold, ColumnFontIsItalic=@ColumnFontIsItalic, ColumnFontIsUnderLine=@ColumnFontIsUnderLine, ColumnFontSize=@ColumnFontSize, TopPadding=@TopPadding, " & _
        "BottomPadding=@BottomPadding, LeftPadding=@LeftPadding, RightPadding=@RightPadding, TopMargin=@TopMargin, BottomMargin=@BottomMargin, LeftMargin=@LeftMargin, RightMargin=@RightMargin, " & _
        "Format=@Format, FooterTotal=@FooterTotal, IsSorted=@IsSorted, IsGroupBy=@IsGroupBy ,Formula=@Formula,GroupByRank=@GroupByRank,ColumnHeaderHalignment=@ColumnHeaderHalignment , ColumnHeaderValignment =@ColumnHeaderValignment,FieldLanguage=@FieldLanguage ," & _
        "ColHeaderBackColor     =@ColHeaderBackColor,ColHeaderForeColor =@ColHeaderForeColor,ColHeaderFont= @ColHeaderFont,ColHeaderFontSize = @ColHeaderFontSize,ColHeaderFontIsItalic = @ColHeaderFontIsItalic," & _
        "ColHeaderFontIsBold    =@ColHeaderFontIsBold,ColHeaderFontIsUnderline  = @ColHeaderFontIsUnderline ,ColHeaderBorderWidth=@ColHeaderBorderWidth, " & _
        "ColHeaderBorderStyle   =@ColHeaderBorderStyle,ColHeaderBorderColor =@ColHeaderBorderColor,ColHeaderFontIsStrikeOut =@ColHeaderFontIsStrikeOut,ColHeaderFontIsOverLine = @ColHeaderFontIsOverLine," & _
        "ColHeaderPaddingLeft   =@ColHeaderPaddingLeft ,ColHeaderPaddingRight = @ColHeaderPaddingRight,ColHeaderPaddingTop = @ColHeaderPaddingTop,ColHeaderPaddingBottom = @ColHeaderPaddingBottom,Ucode  = @Ucode ," & _
        "ColFooterBackColor     =@ColFooterBackColor,ColHeaderForeColor =@ColFooterForeColor,ColFooterFont= @ColFooterFont,ColFooterFontSize = @ColFooterFontSize,ColFooterFontIsItalic = @ColFooterFontIsItalic," & _
        "ColFooterFontIsBold    =@ColFooterFontIsBold,ColFooterFontIsUnderline  = @ColFooterFontIsUnderline ,ColFooterBorderWidth=@ColFooterBorderWidth, " & _
        "ColFooterBorderStyle   =@ColFooterBorderStyle,ColFooterBorderColor =@ColFooterBorderColor,ColFooterFontIsStrikeOut =@ColFooterFontIsStrikeOut,ColFooterFontIsOverLine = @ColFooterFontIsOverLine," & _
        "ColFooterPaddingLeft   =@ColFooterPaddingLeft ,ColFooterPaddingRight = @ColFooterPaddingRight,ColFooterPaddingTop = @ColFooterPaddingTop,ColFooterPaddingBottom = @ColFooterPaddingBottom ," & _
        "ColumnFooterHalignment =@ColumnFooterHalignment,ColumnFooterValignment=@ColumnFooterValignment , " & _
        "ColHeaderBorderStyleTop=@ColHeaderBorderStyleTop,ColHeaderBorderStyleBottom=@ColHeaderBorderStyleBottom,ColHeaderBorderStyleLeft=@ColHeaderBorderStyleLeft,ColHeaderBorderStyleRight=@ColHeaderBorderStyleRight, " & _
        "ColHeaderBorderColorTop=@ColHeaderBorderColorTop,ColHeaderBorderColorBottom=@ColHeaderBorderColorBottom,ColHeaderBorderColorLeft=@ColHeaderBorderColorLeft,ColHeaderBorderColorRight=@ColHeaderBorderColorRight, " & _
        "ColHeaderBorderWidthTop=@ColHeaderBorderWidthTop,ColHeaderBorderWidthBottom=@ColHeaderBorderWidthBottom,ColHeaderBorderWidthLeft=@ColHeaderBorderWidthLeft,ColHeaderBorderWidthRight=@ColHeaderBorderWidthRight, " & _
        "ColFooterBorderStyleTop=@ColFooterBorderStyleTop,ColFooterBorderStyleBottom=@ColFooterBorderStyleBottom,ColFooterBorderStyleLeft=@ColFooterBorderStyleLeft,ColFooterBorderStyleRight=@ColFooterBorderStyleRight, " & _
        "ColFooterBorderColorTop=@ColFooterBorderColorTop,ColFooterBorderColorBottom=@ColFooterBorderColorBottom,ColFooterBorderColorLeft=@ColFooterBorderColorLeft,ColFooterBorderColorRight=@ColFooterBorderColorRight, " & _
        "ColFooterBorderWidthTop=@ColFooterBorderWidthTop,ColFooterBorderWidthBottom=@ColFooterBorderWidthBottom,ColFooterBorderWidthLeft=@ColFooterBorderWidthLeft,ColFooterBorderWidthRight=@ColFooterBorderWidthRight ,Status =@Status , ColFooterSummaryType=@ColFooterSummaryType , ColHeaderSorting=@ColHeaderSorting ,DataType=@DataType "




        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Delete " & mTable
    End Sub

#End Region

#Region "Private Members"

    Private mID As Integer
    Private mReportID As Integer
    Private mRank As Integer
    Private mGroupByRank As Integer
    Private mIsCalculated As Boolean
    Private mBorderColor As String
    Private mTopBorderColor As String
    Private mBottomBorderColor As String
    Private mLeftBorderColor As String
    Private mRightBorderColor As String
    Private mBorderStyle As Integer
    Private mTopBorderStyle As Integer
    Private mBottomBorderStyle As Integer
    Private mLeftBorderStyle As Integer
    Private mRightBorderStyle As Integer
    Private mBorderWidth As Integer
    Private mTopBorderWidth As Integer
    Private mBottomBorderWidth As Integer
    Private mLeftBorderWidth As Integer
    Private mRightBorderWidth As Integer
    Private mEngHeaderCaption As String
    Private mEngFooterCaption As String
    Private mArbHeaderCaption As String
    Private mArbFooterCaption As String
    Private mColumnName As String
    Private mColumnWidth As Integer
    Private mColumnValignment As Integer
    Private mColumnHalignment As Integer
    Private mColumnIsHidden As Boolean
    Private mColumnIsWrap As Boolean
    Private mColumnIsOverView As Boolean
    Private mColumnForeColor As String
    Private mColumnBackColor As String
    Private mColumnFont As String
    Private mColumnFontIsBold As Boolean
    Private mColumnFontIsItalic As Boolean
    Private mColumnFontIsUnderLine As Boolean
    Private mColumnFontSize As Integer
    Private mTopPadding As Integer
    Private mBottomPadding As Integer
    Private mLeftPadding As Integer
    Private mRightPadding As Integer
    Private mTopMargin As Integer
    Private mBottomMargin As Integer
    Private mLeftMargin As Integer
    Private mRightMargin As Integer
    Private mDefaultValue As Object
    Private mFormat As String
    Private mFooterTotal As String
    Private mIsSorted As Integer
    Private mIsGroupBy As Boolean
    Private mFormula As String
    Private mColumnHeaderHalignment
    Private mColumnHeadervalignment
    Private mFieldLanguage As Integer

    Private mColHeaderBackColor As String
    Private mColHeaderForeColor As String
    Private mColHeaderFont As String
    Private mColHeaderFontSize As Integer
    Private mColHeaderFontIsItalic As Boolean
    Private mColHeaderFontIsBold As Boolean
    Private mColHeaderFontIsUnderline As Boolean
    Private mColHeaderBorderWidth As Integer
    Private mColHeaderBorderStyle As Integer
    Private mColHeaderBorderColor As String
    Private mColHeaderFontIsStrikeOut As Boolean
    Private mColHeaderFontIsOverLine As Boolean
    Private mColHeaderPaddingLeft As Integer
    Private mColHeaderPaddingRight As Integer
    Private mColHeaderPaddingTop As Integer
    Private mColHeaderPaddingBottom As Integer
    Private mColHeaderBorderWidthTop As Integer
    Private mColHeaderBorderWidthBottom As Integer
    Private mColHeaderBorderWidthLeft As Integer
    Private mColHeaderBorderWidthRight As Integer

    Private mColHeaderBorderStyleRight As Integer
    Private mColHeaderBorderStyleLeft As Integer
    Private mColHeaderBorderStyleTop As Integer
    Private mColHeaderBorderStyleBottom As Integer

    Private mColHeaderBorderColorLeft As String
    Private mColHeaderBorderColorRight As String
    Private mColHeaderBorderColorTop As String
    Private mColHeaderBorderColorBottom As String

    Private mUcode As Integer

    Private mColFooterBackColor As String
    Private mColFooterForeColor As String
    Private mColFooterFont As String
    Private mColFooterFontSize As Integer
    Private mColFooterFontIsItalic As Boolean
    Private mColFooterFontIsBold As Boolean
    Private mColFooterFontIsUnderline As Boolean
    Private mColFooterBorderWidth As Integer
    Private mColFooterBorderStyle As Integer
    Private mColFooterBorderColor As String
    Private mColFooterFontIsStrikeOut As Boolean
    Private mColFooterFontIsOverLine As Boolean
    Private mColFooterPaddingLeft As Integer
    Private mColFooterPaddingRight As Integer
    Private mColFooterPaddingTop As Integer
    Private mColFooterPaddingBottom As Integer
    Private mColFooterBorderWidthTop As Integer
    Private mColFooterBorderWidthBottom As Integer
    Private mColFooterBorderWidthLeft As Integer
    Private mColFooterBorderWidthRight As Integer

    Private mColFooterBorderStyleRight As Integer
    Private mColFooterBorderStyleLeft As Integer
    Private mColFooterBorderStyleTop As Integer
    Private mColFooterBorderStyleBottom As Integer

    Private mColFooterBorderColorLeft As String
    Private mColFooterBorderColorRight As String
    Private mColFooterBorderColorTop As String
    Private mColFooterBorderColorBottom As String

    '=!=!=
    Private mColumnFooterHalignment As Integer
    Private mColumnFooterValignment As Integer
    Private mStatus As Boolean
    Private mColFooterSummaryType As String
    Private mColHeaderSorting As Integer

    Private mDataType As String


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

    Public Property ReportID() As Integer
        Get
            Return mReportID
        End Get
        Set(ByVal Value As Integer)
            mReportID = Value
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

    Public Property BorderColor() As String
        Get
            Return mBorderColor
        End Get
        Set(ByVal Value As String)
            mBorderColor = Value
        End Set
    End Property

    Public Property TopBorderColor() As String
        Get
            Return mTopBorderColor
        End Get
        Set(ByVal Value As String)
            mTopBorderColor = Value
        End Set
    End Property

    Public Property BottomBorderColor() As String
        Get
            Return mBottomBorderColor
        End Get
        Set(ByVal Value As String)
            mBottomBorderColor = Value
        End Set
    End Property

    Public Property LeftBorderColor() As String
        Get
            Return mLeftBorderColor
        End Get
        Set(ByVal Value As String)
            mLeftBorderColor = Value
        End Set
    End Property

    Public Property RightBorderColor() As String
        Get
            Return mRightBorderColor
        End Get
        Set(ByVal Value As String)
            mRightBorderColor = Value
        End Set
    End Property

    Public Property BorderStyle() As Integer
        Get
            Return mBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mBorderStyle = Value
        End Set
    End Property

    Public Property TopBorderStyle() As Integer
        Get
            Return mTopBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mTopBorderStyle = Value
        End Set
    End Property

    Public Property BottomBorderStyle() As Integer
        Get
            Return mBottomBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mBottomBorderStyle = Value
        End Set
    End Property

    Public Property LeftBorderStyle() As Integer
        Get
            Return mLeftBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mLeftBorderStyle = Value
        End Set
    End Property

    Public Property RightBorderStyle() As Integer
        Get
            Return mRightBorderStyle
        End Get
        Set(ByVal Value As Integer)
            mRightBorderStyle = Value
        End Set
    End Property

    Public Property BorderWidth() As Integer
        Get
            Return mBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mBorderWidth = Value
        End Set
    End Property

    Public Property TopBorderWidth() As Integer
        Get
            Return mTopBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mTopBorderWidth = Value
        End Set
    End Property

    Public Property BottomBorderWidth() As Integer
        Get
            Return mBottomBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mBottomBorderWidth = Value
        End Set
    End Property

    Public Property LeftBorderWidth() As Integer
        Get
            Return mLeftBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mLeftBorderWidth = Value
        End Set
    End Property

    Public Property RightBorderWidth() As Integer
        Get
            Return mRightBorderWidth
        End Get
        Set(ByVal Value As Integer)
            mRightBorderWidth = Value
        End Set
    End Property

    Public Property EngHeaderCaption() As String
        Get
            Return mEngHeaderCaption
        End Get
        Set(ByVal Value As String)
            mEngHeaderCaption = Value
        End Set
    End Property

    Public Property EngFooterCaption() As String
        Get
            Return mEngFooterCaption
        End Get
        Set(ByVal Value As String)
            mEngFooterCaption = Value
        End Set
    End Property

    Public Property ArbHeaderCaption() As String
        Get
            Return mArbHeaderCaption
        End Get
        Set(ByVal Value As String)
            mArbHeaderCaption = Value
        End Set
    End Property

    Public Property ArbFooterCaption() As String
        Get
            Return mArbFooterCaption
        End Get
        Set(ByVal Value As String)
            mArbFooterCaption = Value
        End Set
    End Property

    Public Property ColumnWidth() As Integer
        Get
            Return mColumnWidth
        End Get
        Set(ByVal Value As Integer)
            mColumnWidth = Value
        End Set
    End Property

    Public Property ColumnName() As String
        Get
            Return mColumnName
        End Get
        Set(ByVal Value As String)
            mColumnName = Value
        End Set
    End Property

    Public Property ColumnValignment() As Integer
        Get
            Return mColumnValignment
        End Get
        Set(ByVal Value As Integer)
            mColumnValignment = Value
        End Set
    End Property

    Public Property ColumnHalignment() As Integer
        Get
            Return mColumnHalignment
        End Get
        Set(ByVal Value As Integer)
            mColumnHalignment = Value
        End Set
    End Property

    Public Property ColumnIsHidden() As Boolean
        Get
            Return mColumnIsHidden
        End Get
        Set(ByVal Value As Boolean)
            mColumnIsHidden = Value
        End Set
    End Property

    Public Property ColumnIsWrap() As Boolean
        Get
            Return mColumnIsWrap
        End Get
        Set(ByVal Value As Boolean)
            mColumnIsWrap = Value
        End Set
    End Property

    Public Property ColumnIsOverView() As Boolean
        Get
            Return mColumnIsOverView
        End Get
        Set(ByVal Value As Boolean)
            mColumnIsOverView = Value
        End Set
    End Property

    Public Property ColumnForeColor() As String
        Get
            Return mColumnForeColor
        End Get
        Set(ByVal Value As String)
            mColumnForeColor = Value
        End Set
    End Property

    Public Property ColumnBackColor() As String
        Get
            Return mColumnBackColor
        End Get
        Set(ByVal Value As String)
            mColumnBackColor = Value
        End Set
    End Property

    Public Property ColumnFont() As String
        Get
            Return mColumnFont
        End Get
        Set(ByVal Value As String)
            mColumnFont = Value
        End Set
    End Property

    Public Property ColumnFontIsBold() As Boolean
        Get
            Return mColumnFontIsBold
        End Get
        Set(ByVal Value As Boolean)
            mColumnFontIsBold = Value
        End Set
    End Property

    Public Property ColumnFontIsItalic() As Boolean
        Get
            Return mColumnFontIsItalic
        End Get
        Set(ByVal Value As Boolean)
            mColumnFontIsItalic = Value
        End Set
    End Property

    Public Property ColumnFontIsUnderLine() As Boolean
        Get
            Return mColumnFontIsUnderLine
        End Get
        Set(ByVal Value As Boolean)
            mColumnFontIsUnderLine = Value
        End Set
    End Property

    Public Property ColumnFontSize() As Integer
        Get
            Return mColumnFontSize
        End Get
        Set(ByVal Value As Integer)
            mColumnFontSize = Value
        End Set
    End Property

    Public Property TopPadding() As Integer
        Get
            Return mTopPadding
        End Get
        Set(ByVal Value As Integer)
            mTopPadding = Value
        End Set
    End Property

    Public Property BottomPadding() As Integer
        Get
            Return mBottomPadding
        End Get
        Set(ByVal Value As Integer)
            mBottomPadding = Value
        End Set
    End Property

    Public Property LeftPadding() As Integer
        Get
            Return mLeftPadding
        End Get
        Set(ByVal Value As Integer)
            mLeftPadding = Value
        End Set
    End Property

    Public Property RightPadding() As Integer
        Get
            Return mRightPadding
        End Get
        Set(ByVal Value As Integer)
            mRightPadding = Value
        End Set
    End Property

    Public Property TopMargin() As Integer
        Get
            Return mTopMargin
        End Get
        Set(ByVal Value As Integer)
            mTopMargin = Value
        End Set
    End Property

    Public Property BottomMargin() As Integer
        Get
            Return mBottomMargin
        End Get
        Set(ByVal Value As Integer)
            mBottomMargin = Value
        End Set
    End Property

    Public Property LeftMargin() As Integer
        Get
            Return mLeftMargin
        End Get
        Set(ByVal Value As Integer)
            mLeftMargin = Value
        End Set
    End Property

    Public Property RightMargin() As Integer
        Get
            Return mRightMargin
        End Get
        Set(ByVal Value As Integer)
            mRightMargin = Value
        End Set
    End Property

    Public Property DefaultValue() As Object
        Get
            Return mDefaultValue
        End Get
        Set(ByVal Value As Object)
            mDefaultValue = Value
        End Set
    End Property

    Public Property Format() As String
        Get
            Return mFormat
        End Get
        Set(ByVal Value As String)
            mFormat = Value
        End Set
    End Property

    Public Property FooterTotal() As String
        Get
            Return mFooterTotal
        End Get
        Set(ByVal Value As String)
            mFooterTotal = Value
        End Set
    End Property

    Public Property IsSorted() As Integer
        Get
            Return mIsSorted
        End Get
        Set(ByVal Value As Integer)
            mIsSorted = Value
        End Set
    End Property

    Public Property IsGroupBy() As Boolean
        Get
            Return mIsGroupBy
        End Get
        Set(ByVal Value As Boolean)
            mIsGroupBy = Value
        End Set
    End Property

    Public Property Formula() As String
        Get
            Return mFormula
        End Get
        Set(ByVal value As String)
            mFormula = value
        End Set
    End Property

    Public Property GroupByRank() As Integer
        Get
            Return mGroupByRank
        End Get
        Set(ByVal value As Integer)
            mGroupByRank = value
        End Set
    End Property
    Public Property IsCalculated() As Boolean
        Get
            Return mIsCalculated
        End Get
        Set(ByVal value As Boolean)
            mIsCalculated = value
        End Set
    End Property

    Public Property ColumnHeaderHalignment() As Integer
        Get
            Return mColumnHeaderHalignment
        End Get
        Set(ByVal value As Integer)
            mColumnHeaderHalignment = value
        End Set
    End Property

    Public Property ColumnHeaderValignment() As Integer
        Get
            Return mColumnHeadervalignment
        End Get
        Set(ByVal value As Integer)
            mColumnHeadervalignment = value
        End Set
    End Property

    Public Property FieldLanguage() As Integer
        Get
            Return mFieldLanguage
        End Get
        Set(ByVal value As Integer)
            mFieldLanguage = value
        End Set
    End Property


    Public Property ColHeaderBackColor() As String
        Get
            Return mColHeaderBackColor
        End Get
        Set(ByVal value As String)
            mColHeaderBackColor = value
        End Set
    End Property

    Public Property ColHeaderForeColor() As String
        Get
            Return mColHeaderForeColor
        End Get
        Set(ByVal value As String)
            mColHeaderForeColor = value
        End Set
    End Property

    Public Property ColHeaderFont() As String
        Get
            Return mColHeaderFont
        End Get
        Set(ByVal value As String)
            mColHeaderFont = value
        End Set
    End Property

    Public Property ColHeaderFontSize() As Integer
        Get
            Return mColHeaderFontSize
        End Get
        Set(ByVal value As Integer)
            mColHeaderFontSize = value
        End Set
    End Property

    Public Property ColHeaderFontIsItalic() As Integer
        Get
            Return mColHeaderFontIsItalic
        End Get
        Set(ByVal value As Integer)
            mColHeaderFontIsItalic = value
        End Set
    End Property

    Public Property ColHeaderFontIsBold() As Boolean
        Get
            Return mColHeaderFontIsBold
        End Get
        Set(ByVal value As Boolean)
            mColHeaderFontIsBold = value
        End Set
    End Property

    Public Property ColHeaderFontIsUnderline() As Boolean
        Get
            Return mColHeaderFontIsUnderline
        End Get
        Set(ByVal value As Boolean)
            mColHeaderFontIsUnderline = value
        End Set
    End Property

    Public Property ColHeaderFontIsStrikeOut() As Boolean
        Get
            Return mColHeaderFontIsStrikeOut
        End Get
        Set(ByVal value As Boolean)
            mColHeaderFontIsStrikeOut = value
        End Set
    End Property

    Public Property ColHeaderFontIsOverLine() As Boolean
        Get
            Return mColHeaderFontIsOverLine
        End Get
        Set(ByVal value As Boolean)
            mColHeaderFontIsOverLine = value
        End Set
    End Property

    Public Property ColHeaderBorderWidth() As Integer
        Get
            Return mColHeaderBorderWidth
        End Get
        Set(ByVal value As Integer)
            mColHeaderBorderWidth = value
        End Set
    End Property

    Public Property ColHeaderBorderWidthTop() As Integer
        Get
            Return mColHeaderBorderWidthTop
        End Get
        Set(ByVal value As Integer)
            mColHeaderBorderWidthTop = value
        End Set
    End Property
    Public Property ColHeaderBorderWidthBottom() As Integer
        Get
            Return mColHeaderBorderWidthBottom
        End Get
        Set(ByVal value As Integer)
            mColHeaderBorderWidthBottom = value
        End Set
    End Property
    Public Property ColHeaderBorderWidthRight() As Integer
        Get
            Return mColHeaderBorderWidthRight
        End Get
        Set(ByVal value As Integer)
            mColHeaderBorderWidthRight = value
        End Set
    End Property
    Public Property ColHeaderBorderWidthLeft() As Integer
        Get
            Return mColHeaderBorderWidthLeft
        End Get
        Set(ByVal value As Integer)
            mColHeaderBorderWidthLeft = value
        End Set
    End Property

    Public Property ColHeaderBorderStyle() As Integer
        Get
            Return mColHeaderBorderStyle
        End Get
        Set(ByVal value As Integer)
            mColHeaderBorderStyle = value
        End Set
    End Property
    Public Property ColHeaderBorderStyleLeft() As Integer
        Get
            Return mColHeaderBorderStyleLeft
        End Get
        Set(ByVal value As Integer)
            mColHeaderBorderStyleLeft = value
        End Set
    End Property
    Public Property ColHeaderBorderStyleRight() As Integer
        Get
            Return mColHeaderBorderStyleRight
        End Get
        Set(ByVal value As Integer)
            mColHeaderBorderStyleRight = value
        End Set
    End Property
    Public Property ColHeaderBorderStyleTop() As Integer
        Get
            Return mColHeaderBorderStyleTop
        End Get
        Set(ByVal value As Integer)
            mColHeaderBorderStyleTop = value
        End Set
    End Property
    Public Property ColHeaderBorderStyleBottom() As Integer
        Get
            Return mColHeaderBorderStyleBottom
        End Get
        Set(ByVal value As Integer)
            mColHeaderBorderStyleBottom = value
        End Set
    End Property

    Public Property ColHeaderBorderColor() As String
        Get
            Return mColHeaderBorderColor
        End Get
        Set(ByVal value As String)
            mColHeaderBorderColor = value
        End Set
    End Property
    Public Property ColHeaderBorderColorLeft() As String
        Get
            Return mColHeaderBorderColorLeft
        End Get
        Set(ByVal value As String)
            mColHeaderBorderColorLeft = value
        End Set
    End Property
    Public Property ColHeaderBorderColorRight() As String
        Get
            Return mColHeaderBorderColorRight
        End Get
        Set(ByVal value As String)
            mColHeaderBorderColorRight = value
        End Set
    End Property
    Public Property ColHeaderBorderColorTop() As String
        Get
            Return mColHeaderBorderColorTop
        End Get
        Set(ByVal value As String)
            mColHeaderBorderColorTop = value
        End Set
    End Property
    Public Property ColHeaderBorderColorBottom() As String
        Get
            Return mColHeaderBorderColorBottom
        End Get
        Set(ByVal value As String)
            mColHeaderBorderColorBottom = value
        End Set
    End Property



    Public Property ColHeaderPaddingLeft() As Integer
        Get
            Return mColHeaderPaddingLeft
        End Get
        Set(ByVal value As Integer)
            mColHeaderPaddingLeft = value
        End Set
    End Property
    Public Property ColHeaderPaddingRight() As Integer
        Get
            Return mColHeaderPaddingRight
        End Get
        Set(ByVal value As Integer)
            mColHeaderPaddingRight = value
        End Set
    End Property
    Public Property ColHeaderPaddingTop() As Integer
        Get
            Return mColHeaderPaddingTop
        End Get
        Set(ByVal value As Integer)
            mColHeaderPaddingTop = value
        End Set
    End Property
    Public Property ColHeaderPaddingBottom() As Integer
        Get
            Return mColHeaderPaddingBottom
        End Get
        Set(ByVal value As Integer)
            mColHeaderPaddingBottom = value
        End Set
    End Property


    Public Property Ucode() As Integer
        Get
            Return mUcode
        End Get
        Set(ByVal value As Integer)
            mUcode = value
        End Set
    End Property



    Public Property ColFooterBackColor() As String
        Get
            Return mColFooterBackColor
        End Get
        Set(ByVal value As String)
            mColFooterBackColor = value
        End Set
    End Property

    Public Property ColFooterForeColor() As String
        Get
            Return mColFooterForeColor
        End Get
        Set(ByVal value As String)
            mColFooterForeColor = value
        End Set
    End Property

    Public Property ColFooterFont() As String
        Get
            Return mColFooterFont
        End Get
        Set(ByVal value As String)
            mColFooterFont = value
        End Set
    End Property

    Public Property ColFooterFontSize() As Integer
        Get
            Return mColFooterFontSize
        End Get
        Set(ByVal value As Integer)
            mColFooterFontSize = value
        End Set
    End Property

    Public Property ColFooterFontIsItalic() As Integer
        Get
            Return mColFooterFontIsItalic
        End Get
        Set(ByVal value As Integer)
            mColFooterFontIsItalic = value
        End Set
    End Property

    Public Property ColFooterFontIsBold() As Boolean
        Get
            Return mColFooterFontIsBold
        End Get
        Set(ByVal value As Boolean)
            mColFooterFontIsBold = value
        End Set
    End Property

    Public Property ColFooterFontIsUnderline() As Boolean
        Get
            Return mColFooterFontIsUnderline
        End Get
        Set(ByVal value As Boolean)
            mColFooterFontIsUnderline = value
        End Set
    End Property

    Public Property ColFooterFontIsStrikeOut() As Boolean
        Get
            Return mColFooterFontIsStrikeOut
        End Get
        Set(ByVal value As Boolean)
            mColFooterFontIsStrikeOut = value
        End Set
    End Property

    Public Property ColFooterFontIsOverLine() As Boolean
        Get
            Return mColFooterFontIsOverLine
        End Get
        Set(ByVal value As Boolean)
            mColFooterFontIsOverLine = value
        End Set
    End Property

    Public Property ColFooterBorderWidth() As Integer
        Get
            Return mColFooterBorderWidth
        End Get
        Set(ByVal value As Integer)
            mColFooterBorderWidth = value
        End Set
    End Property

    Public Property ColFooterBorderWidthTop() As Integer
        Get
            Return mColFooterBorderWidthTop
        End Get
        Set(ByVal value As Integer)
            mColFooterBorderWidthTop = value
        End Set
    End Property
    Public Property ColFooterBorderWidthBottom() As Integer
        Get
            Return mColFooterBorderWidthBottom
        End Get
        Set(ByVal value As Integer)
            mColFooterBorderWidthBottom = value
        End Set
    End Property
    Public Property ColFooterBorderWidthRight() As Integer
        Get
            Return mColFooterBorderWidthRight
        End Get
        Set(ByVal value As Integer)
            mColFooterBorderWidthRight = value
        End Set
    End Property
    Public Property ColFooterBorderWidthLeft() As Integer
        Get
            Return mColFooterBorderWidthLeft
        End Get
        Set(ByVal value As Integer)
            mColFooterBorderWidthLeft = value
        End Set
    End Property

    Public Property ColFooterBorderStyle() As Integer
        Get
            Return mColFooterBorderStyle
        End Get
        Set(ByVal value As Integer)
            mColFooterBorderStyle = value
        End Set
    End Property
    Public Property ColFooterBorderStyleLeft() As Integer
        Get
            Return mColFooterBorderStyleLeft
        End Get
        Set(ByVal value As Integer)
            mColFooterBorderStyleLeft = value
        End Set
    End Property
    Public Property ColFooterBorderStyleRight() As Integer
        Get
            Return mColFooterBorderStyleRight
        End Get
        Set(ByVal value As Integer)
            mColFooterBorderStyleRight = value
        End Set
    End Property
    Public Property ColFooterBorderStyleTop() As Integer
        Get
            Return mColFooterBorderStyleTop
        End Get
        Set(ByVal value As Integer)
            mColFooterBorderStyleTop = value
        End Set
    End Property
    Public Property ColFooterBorderStyleBottom() As Integer
        Get
            Return mColFooterBorderStyleBottom
        End Get
        Set(ByVal value As Integer)
            mColFooterBorderStyleBottom = value
        End Set
    End Property

    Public Property ColFooterBorderColor() As String
        Get
            Return mColFooterBorderColor
        End Get
        Set(ByVal value As String)
            mColFooterBorderColor = value
        End Set
    End Property
    Public Property ColFooterBorderColorLeft() As String
        Get
            Return mColFooterBorderColorLeft
        End Get
        Set(ByVal value As String)
            mColFooterBorderColorLeft = value
        End Set
    End Property
    Public Property ColFooterBorderColorRight() As String
        Get
            Return mColFooterBorderColorRight
        End Get
        Set(ByVal value As String)
            mColFooterBorderColorRight = value
        End Set
    End Property
    Public Property ColFooterBorderColorTop() As String
        Get
            Return mColFooterBorderColorTop
        End Get
        Set(ByVal value As String)
            mColFooterBorderColorTop = value
        End Set
    End Property
    Public Property ColFooterBorderColorBottom() As String
        Get
            Return mColFooterBorderColorBottom
        End Get
        Set(ByVal value As String)
            mColFooterBorderColorBottom = value
        End Set
    End Property

    Public Property ColFooterPaddingLeft() As Integer
        Get
            Return mColFooterPaddingLeft
        End Get
        Set(ByVal value As Integer)
            mColFooterPaddingLeft = value
        End Set
    End Property
    Public Property ColFooterPaddingRight() As Integer
        Get
            Return mColFooterPaddingRight
        End Get
        Set(ByVal value As Integer)
            mColFooterPaddingRight = value
        End Set
    End Property
    Public Property ColFooterPaddingTop() As Integer
        Get
            Return mColFooterPaddingTop
        End Get
        Set(ByVal value As Integer)
            mColFooterPaddingTop = value
        End Set
    End Property
    Public Property ColFooterPaddingBottom() As Integer
        Get
            Return mColFooterPaddingBottom
        End Get
        Set(ByVal value As Integer)
            mColFooterPaddingBottom = value
        End Set
    End Property

    Public Property ColumnFooterValignment() As Integer
        Get
            Return mColumnFooterValignment
        End Get
        Set(ByVal value As Integer)
            mColumnFooterValignment = value
        End Set
    End Property

    Public Property ColumnFooterHalignment() As Integer
        Get
            Return mColumnFooterHalignment
        End Get
        Set(ByVal value As Integer)
            mColumnFooterHalignment = value
        End Set
    End Property

    Public Property Status() As Boolean
        Get
            Return mStatus
        End Get
        Set(ByVal value As Boolean)
            mStatus = value
        End Set
    End Property



    Public Property ColFooterSummaryType() As String
        Get
            Return mColFooterSummaryType
        End Get
        Set(ByVal value As String)
            mColFooterSummaryType = value
        End Set
    End Property

    Public Property ColHeaderSorting() As Integer
        Get
            Return mColHeaderSorting
        End Get
        Set(ByVal value As Integer)
            mColHeaderSorting = value
        End Set
    End Property


    Public Property DataType() As String
        Get
            Return mDataType
        End Get
        Set(ByVal value As String)
            mDataType = value
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
            mReportID = 0
            mRank = 0

            mBorderColor = String.Empty
            mTopBorderColor = String.Empty
            mBottomBorderColor = String.Empty
            mLeftBorderColor = String.Empty
            mRightBorderColor = String.Empty

            mBorderStyle = 0
            mTopBorderStyle = 0
            mBottomBorderStyle = 0
            mLeftBorderStyle = 0
            mRightBorderStyle = 0

            mBorderWidth = 0
            mTopBorderWidth = 0
            mBottomBorderWidth = 0
            mLeftBorderWidth = 0
            mRightBorderWidth = 0

            mEngHeaderCaption = String.Empty
            mEngFooterCaption = String.Empty
            mArbHeaderCaption = String.Empty
            mArbFooterCaption = String.Empty

            mColumnWidth = 0
            mColumnName = String.Empty
            mColumnValignment = 0
            mColumnHalignment = 0
            mColumnIsHidden = False
            mColumnIsWrap = False
            mColumnIsOverView = False
            mColumnForeColor = String.Empty
            mColumnBackColor = String.Empty
            mColumnFont = String.Empty
            mColumnFontIsItalic = False
            mColumnFontIsBold = False
            mColumnFontIsUnderLine = False
            mColumnFontSize = 0

            mTopPadding = 0
            mBottomPadding = 0
            mLeftPadding = 0
            mRightPadding = 0
            mTopMargin = 0
            mBottomMargin = 0
            mLeftMargin = 0
            mRightMargin = 0

            mDefaultValue = Nothing
            mFormat = String.Empty
            mFooterTotal = String.Empty
            mIsSorted = False
            mIsGroupBy = False
            mFieldLanguage = 0
            mColHeaderBackColor = String.Empty
            mColHeaderBorderColor = String.Empty
            mColHeaderBorderColorBottom = String.Empty
            mColHeaderBorderColorLeft = String.Empty
            mColHeaderBorderColorTop = String.Empty
            mColHeaderBorderColorRight = String.Empty

            mColHeaderBorderStyle = 0
            mColHeaderBorderStyleBottom = 0
            mColHeaderBorderStyleLeft = 0
            mColHeaderBorderColorRight = 0
            mColHeaderBorderColorTop = 0

            mColHeaderBorderWidth = 0
            mColHeaderBorderWidthBottom = 0
            mColHeaderBorderWidthTop = 0
            mColHeaderBorderWidthLeft = 0
            mColHeaderBorderWidthRight = 0

            mColHeaderFont = String.Empty
            mColHeaderFontSize = 0
            mColHeaderFontIsBold = False
            mColHeaderFontIsItalic = False
            mColHeaderFontIsOverLine = False
            mColHeaderFontIsStrikeOut = False
            mColHeaderFontIsUnderline = False
            mColHeaderForeColor = String.Empty
            mColHeaderPaddingBottom = 0
            mColHeaderPaddingTop = 0
            mColHeaderPaddingLeft = 0
            mColHeaderPaddingRight = 0



            mColFooterBackColor = String.Empty
            mColFooterBorderColor = String.Empty
            mColFooterBorderColorBottom = String.Empty
            mColFooterBorderColorLeft = String.Empty
            mColFooterBorderColorTop = String.Empty
            mColFooterBorderColorRight = String.Empty

            mColFooterBorderStyle = 0
            mColFooterBorderStyleBottom = 0
            mColFooterBorderStyleLeft = 0
            mColFooterBorderColorRight = 0
            mColFooterBorderColorTop = 0

            mColFooterBorderWidth = 0
            mColFooterBorderWidthBottom = 0
            mColFooterBorderWidthTop = 0
            mColFooterBorderWidthLeft = 0
            mColFooterBorderWidthRight = 0

            mColFooterFont = String.Empty
            mColFooterFontSize = 0
            mColFooterFontIsBold = False
            mColFooterFontIsItalic = False
            mColFooterFontIsOverLine = False
            mColFooterFontIsStrikeOut = False
            mColFooterFontIsUnderline = False
            mColFooterForeColor = String.Empty
            mColFooterPaddingBottom = 0
            mColFooterPaddingTop = 0
            mColFooterPaddingLeft = 0
            mColFooterPaddingRight = 0

            mColumnFooterHalignment = 0
            mColumnFooterValignment = 0
            mStatus = False

            ColFooterSummaryType = ""
            ColHeaderSorting = 0

            DataType = ""
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function SetRepColumnsProperties(ByVal IntReportID As Integer, ByVal dtColumnsProperties As Data.DataTable, ByVal StrSizeInfo As String, Optional ByVal arrList As ArrayList = Nothing) As Boolean
        'Check if color is transparent and replace it with white color
        Try
            If arrList Is Nothing Then
                arrList = New ArrayList
                arrList.Add("@#$%^")
            End If

            Dim StrColumnName As String
            'Dim StrColumnCaption As String
            Dim clsReportsMain As New ClsRpw_ReportsMain(mPage)
            Dim IntColumnIndex As Integer = 0
            Dim StrTempColor As String = String.Empty
            Dim IntTempWidth As Integer
            Const CsTempWidth As Integer = 100
            If dtColumnsProperties.Rows.Count > 0 Then
                For Each drDatRow As DataRow In dtColumnsProperties.Rows
                    '//// Group By Columns 
                    StrColumnName = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("FieldName"), SqlDbType.VarChar)
                    ColumnName = StrColumnName
                    If arrList.IndexOf(StrColumnName) >= 0 Then
                        IsGroupBy = True
                        GroupByRank = arrList.IndexOf(StrColumnName)
                    Else
                        IsGroupBy = False
                    End If
                    ReportID = IntReportID
                    Rank = dtColumnsProperties.Rows(IntColumnIndex).Item("Rank")
                    IsCalculated = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("IsCalculated"), SqlDbType.Bit)
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("BorderColor"), SqlDbType.VarChar)
                    BorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("TopBorderColor"), SqlDbType.VarChar)
                    TopBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("BottomBorderColor"), SqlDbType.VarChar)
                    BottomBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("LeftBorderColor"), SqlDbType.VarChar)
                    LeftBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("RightBorderColor"), SqlDbType.VarChar)
                    RightBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    BorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("BorderStyle"), SqlDbType.Int)

                    TopBorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("TopBorderStyle"), SqlDbType.Int)
                    BottomBorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("BottomBorderStyle"), SqlDbType.Int)
                    LeftBorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("LeftBorderStyle"), SqlDbType.Int)
                    RightBorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("RightBorderStyle"), SqlDbType.Int)
                    BorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("BorderWidth"), SqlDbType.Int)
                    TopBorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("TopBorderWidth"), SqlDbType.Int)
                    BottomBorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("BottomBorderWidth"), SqlDbType.Int)
                    LeftBorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("LeftBorderWidth"), SqlDbType.Int)
                    RightBorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("RightBorderWidth"), SqlDbType.Int)
                    EngHeaderCaption = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("EngDescription"), SqlDbType.VarChar)
                    EngFooterCaption = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("EngFooterCaption"), SqlDbType.VarChar)
                    ArbHeaderCaption = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ArbDescription"), SqlDbType.VarChar)
                    ArbFooterCaption = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ArbFooterCaption"), SqlDbType.VarChar)


                    IntTempWidth = GetAndSetColumnsSizes(StrColumnName, StrSizeInfo)
                    If IntTempWidth > 0 Then
                        ColumnWidth = IntTempWidth
                    Else
                        IntTempWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnWidth"), SqlDbType.Int)
                        ColumnWidth = IIf(IntTempWidth <= 0, CsTempWidth, IntTempWidth)
                    End If
                    ColumnHalignment = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnHalignment"), SqlDbType.Int)
                    ColumnValignment = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnValignment"), SqlDbType.Int)
                    ColumnIsHidden = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnIsHidden"), SqlDbType.Bit)
                    ColumnIsOverView = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnIsOverView"), SqlDbType.Bit)
                    ColumnIsWrap = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnIsWrap"), SqlDbType.Bit)

                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnForeColor"), SqlDbType.VarChar)
                    ColumnForeColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "Black", StrTempColor)

                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnBackColor"), SqlDbType.VarChar)
                    ColumnBackColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    ColumnFont = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnFont"), SqlDbType.VarChar)
                    ColumnFontIsBold = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnFontIsBold"), SqlDbType.Bit)
                    ColumnFontIsItalic = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnFontIsItalic"), SqlDbType.Bit)
                    ColumnFontIsUnderLine = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnFontIsUnderLine"), SqlDbType.Bit)
                    ColumnFontSize = clsReportsMain.GetFontSizesfromStrings(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnFontSize"))
                    TopPadding = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("TopPadding"), SqlDbType.Int)
                    BottomPadding = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("BottomPadding"), SqlDbType.Int)
                    LeftPadding = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("LeftPadding"), SqlDbType.Int)
                    RightPadding = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("RightPadding"), SqlDbType.Int)
                    TopMargin = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("TopMargin"), SqlDbType.Int)
                    BottomMargin = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("BottomMargin"), SqlDbType.Int)
                    LeftMargin = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("LeftMargin"), SqlDbType.Int)
                    RightMargin = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("RightMargin"), SqlDbType.Int)
                    DefaultValue = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("DefaultValue"), SqlDbType.VarChar)
                    Format = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("Format"), SqlDbType.VarChar)
                    FooterTotal = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("FooterTotal"), SqlDbType.VarChar)
                    IsSorted = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("IsSorted"), SqlDbType.TinyInt)
                    Formula = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("Formula"), SqlDbType.VarChar)

                    '========= Header Allignment Added By [0256] 0258 [Begin]
                    ColumnHeaderHalignment = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnHeaderHalignment"), SqlDbType.Int)
                    ColumnHeaderValignment = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnHeaderValignment"), SqlDbType.Int)
                    '========= Header Allignment Added By [0256] 0258 [End]
                    FieldLanguage = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("FieldLanguage"), SqlDbType.Int)

                    '//ColumnHeader
                    '========================================================= [Begin]
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBackColor"), SqlDbType.VarChar)
                    ColHeaderBackColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderForeColor"), SqlDbType.VarChar)
                    ColHeaderForeColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    ColHeaderFont = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderFont"), SqlDbType.VarChar)
                    ColHeaderFontIsBold = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderFontIsBold"), SqlDbType.Bit)
                    ColHeaderFontIsItalic = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnFontIsItalic"), SqlDbType.Bit)
                    ColHeaderFontIsUnderline = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderFontIsUnderline"), SqlDbType.Bit)
                    ColHeaderFontIsStrikeOut = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderFontIsStrikeOut"), SqlDbType.Bit)
                    ColHeaderFontIsOverLine = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderFontIsOverLine"), SqlDbType.Bit)

                    ColHeaderFontSize = clsReportsMain.GetFontSizesfromStrings(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderFontSize"))

                    ColHeaderBorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderStyle"), SqlDbType.Int)
                    ColHeaderBorderStyleTop = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderStyleTop"), SqlDbType.Int)
                    ColHeaderBorderStyleBottom = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderStyleBottom"), SqlDbType.Int)
                    ColHeaderBorderStyleRight = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderStyleRight"), SqlDbType.Int)
                    ColHeaderBorderStyleLeft = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderStyleLeft"), SqlDbType.Int)

                    ColHeaderBorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderWidth"), SqlDbType.Int)
                    ColHeaderBorderWidthTop = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderWidthTop"), SqlDbType.Int)
                    ColHeaderBorderWidthBottom = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderWidthBottom"), SqlDbType.Int)
                    ColHeaderBorderWidthRight = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderWidthRight"), SqlDbType.Int)
                    ColHeaderBorderWidthLeft = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderWidthLeft"), SqlDbType.Int)


                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderColor"), SqlDbType.VarChar)
                    ColHeaderBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderColorTop"), SqlDbType.VarChar)
                    ColHeaderBorderColorTop = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderColorBottom"), SqlDbType.VarChar)
                    ColHeaderBorderColorBottom = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderColorRight"), SqlDbType.VarChar)
                    ColHeaderBorderColorRight = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderBorderColorLeft"), SqlDbType.VarChar)
                    ColHeaderBorderColorLeft = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    ColHeaderPaddingLeft = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderPaddingLeft"), SqlDbType.Int)
                    ColHeaderPaddingRight = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderPaddingRight"), SqlDbType.Int)
                    ColHeaderPaddingTop = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderPaddingTop"), SqlDbType.Int)
                    ColHeaderPaddingBottom = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderPaddingBottom"), SqlDbType.Int)

                    '========================================================= [End]

                    '//ColumnFooter
                    '========================================================= [Begin]
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBackColor"), SqlDbType.VarChar)
                    ColFooterBackColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterForeColor"), SqlDbType.VarChar)
                    ColFooterForeColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    ColFooterFont = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterFont"), SqlDbType.VarChar)
                    ColFooterFontIsBold = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterFontIsBold"), SqlDbType.Bit)
                    ColFooterFontIsItalic = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnFontIsItalic"), SqlDbType.Bit)
                    ColFooterFontIsUnderline = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterFontIsUnderline"), SqlDbType.Bit)
                    ColFooterFontIsStrikeOut = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterFontIsStrikeOut"), SqlDbType.Bit)
                    ColFooterFontIsOverLine = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterFontIsOverLine"), SqlDbType.Bit)

                    ColFooterFontSize = clsReportsMain.GetFontSizesfromStrings(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterFontSize"))

                    ColFooterBorderStyle = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderStyle"), SqlDbType.Int)
                    ColFooterBorderStyleTop = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderStyleTop"), SqlDbType.Int)
                    ColFooterBorderStyleBottom = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderStyleBottom"), SqlDbType.Int)
                    ColFooterBorderStyleRight = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderStyleRight"), SqlDbType.Int)
                    ColFooterBorderStyleLeft = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderStyleLeft"), SqlDbType.Int)

                    ColFooterBorderWidth = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderWidth"), SqlDbType.Int)
                    ColFooterBorderWidthTop = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderWidthTop"), SqlDbType.Int)
                    ColFooterBorderWidthBottom = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderWidthBottom"), SqlDbType.Int)
                    ColFooterBorderWidthRight = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderWidthRight"), SqlDbType.Int)
                    ColFooterBorderWidthLeft = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderWidthLeft"), SqlDbType.Int)


                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderColor"), SqlDbType.VarChar)
                    ColFooterBorderColor = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderColorTop"), SqlDbType.VarChar)
                    ColFooterBorderColorTop = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderColorBottom"), SqlDbType.VarChar)
                    ColFooterBorderColorBottom = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderColorRight"), SqlDbType.VarChar)
                    ColFooterBorderColorRight = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)
                    StrTempColor = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterBorderColorLeft"), SqlDbType.VarChar)
                    ColFooterBorderColorLeft = IIf(StrTempColor = "Transparent" Or StrTempColor = "0", "White", StrTempColor)

                    ColFooterPaddingLeft = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterPaddingLeft"), SqlDbType.Int)
                    ColFooterPaddingRight = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterPaddingRight"), SqlDbType.Int)
                    ColFooterPaddingTop = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterPaddingTop"), SqlDbType.Int)
                    ColFooterPaddingBottom = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterPaddingBottom"), SqlDbType.Int)

                    ColumnFooterHalignment = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnFooterHalignment"), SqlDbType.Int)
                    ColumnFooterValignment = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColumnFooterValignment"), SqlDbType.Int)

                    ColFooterSummaryType = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColFooterSummaryType"), SqlDbType.VarChar)
                    ColHeaderSorting = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("ColHeaderSorting"), SqlDbType.Int)
                    DataType = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("DataType"), SqlDbType.VarChar)
                    Status = mDataHandler.DataValue_Out(dtColumnsProperties.Rows(IntColumnIndex).Item("Status"), SqlDbType.Bit)
                    Save()
                    IntColumnIndex += 1
                Next

            End If


        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
        Return True

    End Function
    Public Function UpdateColumnsProperties(ByVal uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByRef dtColumnsTable As Data.DataTable) As Data.DataTable
        Try

            For Each uwcColumn As Infragistics.WebUI.UltraWebGrid.UltraGridColumn In uwgGrid.Columns
                For Each dRow As Data.DataRow In dtColumnsTable.Rows
                    If dRow.Item("FieldName") = uwcColumn.Key Then
                        dRow.Item("ColumnWidth") = CType(uwcColumn.Width.Value, Int32)
                        dRow.Item("IsSorted") = uwcColumn.SortIndicator
                        Exit For
                    End If
                Next
            Next
            Return dtColumnsTable
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
    Public Function GetRepColumnsProperties(ByVal IntReportID As Integer, ByRef dtTargetTable As Data.DataTable) As Boolean
        'Dim ClsColumnProperties As New ClsRpw_ReportColumnsProperties(Page)
        Dim clsReports As New ClsRpw_ReportsMain(mPage)
        Dim mDataHandler As New Venus.Shared.DataHandler
        Try
            Find(" ReportID= " & IntReportID & " Order By Rank ")

            Dim dsColumnsDS As DataSet = mDataSet
            Dim drColDataRow As Data.DataRow
            With dsColumnsDS.Tables(0)
                If .Rows.Count > 0 Then
                    For IntCounter As Integer = 0 To .DataSet.Tables(0).Rows.Count - 1
                        drColDataRow = dtTargetTable.NewRow

                        drColDataRow.Item("ID") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ID"), SqlDbType.Int)
                        drColDataRow.Item("ReportID") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ReportID"), SqlDbType.Int)

                        drColDataRow.Item("ColumnIndex") = .Rows(IntCounter).Item("Rank")
                        drColDataRow.Item("Status") = IIf(.Rows(IntCounter).Item("Status") Is DBNull.Value, False, .Rows(IntCounter).Item("Status"))
                        drColDataRow.Item("ColFooterSummaryType") = IIf(.Rows(IntCounter).Item("ColFooterSummaryType") Is DBNull.Value, "", .Rows(IntCounter).Item("ColFooterSummaryType"))
                        drColDataRow.Item("ColHeaderSorting") = IIf(.Rows(IntCounter).Item("ColHeaderSorting") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderSorting"))
                        drColDataRow.Item("DataType") = IIf(.Rows(IntCounter).Item("DataType") Is DBNull.Value, "", .Rows(IntCounter).Item("DataType"))

                        drColDataRow.Item("BorderColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("BorderColor"), SqlDbType.VarChar)
                        drColDataRow.Item("TopBorderColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("TopBorderColor"), SqlDbType.VarChar)
                        drColDataRow.Item("BottomBorderColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("BottomBorderColor"), SqlDbType.VarChar)
                        drColDataRow.Item("LeftBorderColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("LeftBorderColor"), SqlDbType.VarChar)
                        drColDataRow.Item("RightBorderColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("RightBorderColor"), SqlDbType.VarChar)
                        drColDataRow.Item("BorderStyle") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("BorderStyle"), SqlDbType.Int)
                        drColDataRow.Item("TopBorderStyle") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("TopBorderStyle"), SqlDbType.Int)
                        drColDataRow.Item("BottomBorderStyle") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("BottomBorderStyle"), SqlDbType.Int)
                        drColDataRow.Item("LeftBorderStyle") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("LeftBorderStyle"), SqlDbType.Int)
                        drColDataRow.Item("RightBorderStyle") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("RightBorderStyle"), SqlDbType.Int)
                        drColDataRow.Item("BorderWidth") = IIf(.Rows(IntCounter).Item("BorderWidth") Is DBNull.Value, 0, .Rows(IntCounter).Item("BorderWidth"))
                        drColDataRow.Item("TopBorderWidth") = IIf(.Rows(IntCounter).Item("TopBorderWidth") Is DBNull.Value, 0, .Rows(IntCounter).Item("BorderWidth"))
                        drColDataRow.Item("BottomBorderWidth") = IIf(.Rows(IntCounter).Item("BottomBorderWidth") Is DBNull.Value, 0, .Rows(IntCounter).Item("BorderWidth"))
                        drColDataRow.Item("LeftBorderWidth") = IIf(.Rows(IntCounter).Item("LeftBorderWidth") Is DBNull.Value, 0, .Rows(IntCounter).Item("BorderWidth"))
                        drColDataRow.Item("RightBorderWidth") = IIf(.Rows(IntCounter).Item("RightBorderWidth") Is DBNull.Value, 0, .Rows(IntCounter).Item("BorderWidth"))
                        drColDataRow.Item("EngDescription") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("EngHeaderCaption"), SqlDbType.VarChar)
                        drColDataRow.Item("EngFooterCaption") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("EngFooterCaption"), SqlDbType.VarChar)
                        drColDataRow.Item("ArbDescription") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ArbHeaderCaption"), SqlDbType.VarChar)
                        drColDataRow.Item("ArbFooterCaption") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ArbFooterCaption"), SqlDbType.VarChar)
                        drColDataRow.Item("ColumnWidth") = mDataHandler.DataValue_In(.Rows(IntCounter).Item("ColumnWidth"), SqlDbType.Int)
                        drColDataRow.Item("FieldName") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColumnName"), SqlDbType.VarChar)
                        drColDataRow.Item("ColumnHalignment") = IIf(.Rows(IntCounter).Item("ColumnHalignment") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColumnHalignment"))
                        drColDataRow.Item("ColumnValignment") = IIf(.Rows(IntCounter).Item("ColumnValignment") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColumnValignment"))
                        drColDataRow.Item("ColumnIsHidden") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColumnIsHidden"), SqlDbType.Bit)
                        drColDataRow.Item("ColumnIsOverView") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColumnIsOverView"), SqlDbType.Bit)
                        drColDataRow.Item("ColumnIsWrap") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColumnIsWrap"), SqlDbType.Bit)
                        drColDataRow.Item("ColumnForeColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColumnForeColor"), SqlDbType.VarChar)
                        drColDataRow.Item("ColumnBackColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColumnBackColor"), SqlDbType.VarChar)
                        drColDataRow.Item("ColumnFont") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColumnFont"), SqlDbType.VarChar)
                        drColDataRow.Item("ColumnFontIsBold") = .Rows(IntCounter).Item("ColumnFontIsBold")
                        drColDataRow.Item("ColumnFontIsItalic") = .Rows(IntCounter).Item("ColumnFontIsItalic")
                        drColDataRow.Item("ColumnFontIsUnderLine") = .Rows(IntCounter).Item("ColumnFontIsUnderLine")
                        drColDataRow.Item("ColumnFontSize") = IIf(.Rows(IntCounter).Item("ColumnFontSize") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColumnFontSize"))
                        drColDataRow.Item("TopPadding") = IIf(.Rows(IntCounter).Item("TopPadding") Is DBNull.Value, 0, .Rows(IntCounter).Item("TopPadding"))
                        drColDataRow.Item("BottomPadding") = IIf(.Rows(IntCounter).Item("BottomPadding") Is DBNull.Value, 0, .Rows(IntCounter).Item("BottomPadding"))
                        drColDataRow.Item("LeftPadding") = IIf(.Rows(IntCounter).Item("LeftPadding") Is DBNull.Value, 0, .Rows(IntCounter).Item("LeftPadding"))
                        drColDataRow.Item("RightPadding") = IIf(.Rows(IntCounter).Item("RightPadding") Is DBNull.Value, 0, .Rows(IntCounter).Item("RightPadding"))
                        drColDataRow.Item("TopMargin") = IIf(.Rows(IntCounter).Item("TopMargin") Is DBNull.Value, 0, .Rows(IntCounter).Item("TopMargin"))
                        drColDataRow.Item("BottomMargin") = IIf(.Rows(IntCounter).Item("BottomMargin") Is DBNull.Value, 0, .Rows(IntCounter).Item("BottomMargin"))
                        drColDataRow.Item("LeftMargin") = IIf(.Rows(IntCounter).Item("LeftMargin") Is DBNull.Value, 0, .Rows(IntCounter).Item("LeftMargin"))
                        drColDataRow.Item("RightMargin") = IIf(.Rows(IntCounter).Item("RightMargin") Is DBNull.Value, 0, .Rows(IntCounter).Item("RightMargin"))
                        drColDataRow.Item("Format") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("Format"), SqlDbType.VarChar)
                        drColDataRow.Item("FooterTotal") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("FooterTotal"), SqlDbType.VarChar)
                        drColDataRow.Item("IsCalculated") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("IsCalculated"), SqlDbType.Bit)
                        drColDataRow.Item("IsSorted") = IIf(.Rows(IntCounter).Item("IsSorted") Is DBNull.Value, 0, .Rows(IntCounter).Item("IsSorted"))
                        drColDataRow.Item("IsGroupBy") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("IsGroupBy"), SqlDbType.Bit)
                        drColDataRow.Item("Formula") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("Formula"), SqlDbType.VarChar)
                        drColDataRow.Item("ColumnHeaderHalignment") = mDataHandler.DataValue_In(.Rows(IntCounter).Item("ColumnHeaderHalignment"), SqlDbType.Int)
                        drColDataRow.Item("ColumnHeaderValignment") = mDataHandler.DataValue_In(.Rows(IntCounter).Item("ColumnHeaderValignment"), SqlDbType.Int)
                        drColDataRow.Item("FieldLanguage") = mDataHandler.DataValue_In(.Rows(IntCounter).Item("FieldLanguage"), SqlDbType.Int)
                        drColDataRow.Item("ColHeaderBackColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBackColor"), SqlDbType.VarChar)
                        drColDataRow.Item("ColHeaderForeColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderForeColor"), SqlDbType.VarChar)
                        drColDataRow.Item("ColHeaderFont") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderFont"), SqlDbType.VarChar)
                        drColDataRow.Item("ColHeaderFontIsBold") = .Rows(IntCounter).Item("ColHeaderFontIsBold")
                        drColDataRow.Item("ColHeaderFontIsItalic") = .Rows(IntCounter).Item("ColHeaderFontIsItalic")
                        drColDataRow.Item("ColHeaderFontIsUnderline") = .Rows(IntCounter).Item("ColHeaderFontIsUnderline")
                        drColDataRow.Item("ColHeaderFontSize") = IIf(.Rows(IntCounter).Item("ColHeaderFontSize") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderFontSize"))

                        drColDataRow.Item("ColHeaderBorderWidth") = IIf(.Rows(IntCounter).Item("ColHeaderBorderWidth") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderBorderWidth"))
                        drColDataRow.Item("ColHeaderBorderWidthTop") = IIf(.Rows(IntCounter).Item("ColHeaderBorderWidthTop") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderBorderWidthTop"))
                        drColDataRow.Item("ColHeaderBorderWidthBottom") = IIf(.Rows(IntCounter).Item("ColHeaderBorderWidthBottom") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderBorderWidthBottom"))
                        drColDataRow.Item("ColHeaderBorderWidthLeft") = IIf(.Rows(IntCounter).Item("ColHeaderBorderWidthLeft") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderBorderWidthLeft"))
                        drColDataRow.Item("ColHeaderBorderWidthRight") = IIf(.Rows(IntCounter).Item("ColHeaderBorderWidthRight") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderBorderWidthRight"))

                        drColDataRow.Item("ColHeaderBorderStyle") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBorderStyle"), SqlDbType.Int)
                        drColDataRow.Item("ColHeaderBorderStyleTop") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBorderStyleTop"), SqlDbType.Int)
                        drColDataRow.Item("ColHeaderBorderStyleBottom") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBorderStyleBottom"), SqlDbType.Int)
                        drColDataRow.Item("ColHeaderBorderStyleRight") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBorderStyleRight"), SqlDbType.Int)
                        drColDataRow.Item("ColHeaderBorderStyleLeft") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBorderStyleLeft"), SqlDbType.Int)

                        drColDataRow.Item("ColHeaderBorderColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBorderColor"), SqlDbType.VarChar)
                        drColDataRow.Item("ColHeaderBorderColorTop") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBorderColorTop"), SqlDbType.VarChar)
                        drColDataRow.Item("ColHeaderBorderColorBottom") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBorderColorBottom"), SqlDbType.VarChar)
                        drColDataRow.Item("ColHeaderBorderColorLeft") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBorderColorLeft"), SqlDbType.VarChar)
                        drColDataRow.Item("ColHeaderBorderColorRight") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColHeaderBorderColorRight"), SqlDbType.VarChar)

                        drColDataRow.Item("ColHeaderPaddingLeft") = IIf(.Rows(IntCounter).Item("ColHeaderPaddingLeft") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderPaddingLeft"))
                        drColDataRow.Item("ColHeaderPaddingRight") = IIf(.Rows(IntCounter).Item("ColHeaderPaddingRight") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderPaddingRight"))
                        drColDataRow.Item("ColHeaderPaddingTop") = IIf(.Rows(IntCounter).Item("ColHeaderPaddingTop") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderPaddingTop"))
                        drColDataRow.Item("ColHeaderPaddingBottom") = IIf(.Rows(IntCounter).Item("ColHeaderPaddingBottom") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColHeaderPaddingBottom"))

                        drColDataRow.Item("ColFooterBackColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBackColor"), SqlDbType.VarChar)
                        drColDataRow.Item("ColFooterForeColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterForeColor"), SqlDbType.VarChar)
                        drColDataRow.Item("ColFooterFont") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterFont"), SqlDbType.VarChar)
                        drColDataRow.Item("ColFooterFontIsBold") = .Rows(IntCounter).Item("ColFooterFontIsBold")
                        drColDataRow.Item("ColFooterFontIsItalic") = .Rows(IntCounter).Item("ColFooterFontIsItalic")
                        drColDataRow.Item("ColFooterFontIsUnderline") = .Rows(IntCounter).Item("ColFooterFontIsUnderline")
                        drColDataRow.Item("ColFooterFontSize") = IIf(.Rows(IntCounter).Item("ColFooterFontSize") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColFooterFontSize"))

                        drColDataRow.Item("ColFooterBorderWidth") = IIf(.Rows(IntCounter).Item("ColFooterBorderWidth") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColFooterBorderWidth"))
                        drColDataRow.Item("ColFooterBorderWidthTop") = IIf(.Rows(IntCounter).Item("ColFooterBorderWidthTop") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColFooterBorderWidthTop"))
                        drColDataRow.Item("ColFooterBorderWidthBottom") = IIf(.Rows(IntCounter).Item("ColFooterBorderWidthBottom") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColFooterBorderWidthBottom"))
                        drColDataRow.Item("ColFooterBorderWidthLeft") = IIf(.Rows(IntCounter).Item("ColFooterBorderWidthLeft") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColFooterBorderWidthLeft"))
                        drColDataRow.Item("ColFooterBorderWidthRight") = IIf(.Rows(IntCounter).Item("ColFooterBorderWidthRight") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColFooterBorderWidthRight"))

                        drColDataRow.Item("ColFooterBorderStyle") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBorderStyle"), SqlDbType.Int)
                        drColDataRow.Item("ColFooterBorderStyleTop") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBorderStyleTop"), SqlDbType.Int)
                        drColDataRow.Item("ColFooterBorderStyleBottom") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBorderStyleBottom"), SqlDbType.Int)
                        drColDataRow.Item("ColFooterBorderStyleRight") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBorderStyleRight"), SqlDbType.Int)
                        drColDataRow.Item("ColFooterBorderStyleLeft") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBorderStyleLeft"), SqlDbType.Int)

                        drColDataRow.Item("ColFooterBorderColor") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBorderColor"), SqlDbType.VarChar)
                        drColDataRow.Item("ColFooterBorderColorTop") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBorderColorTop"), SqlDbType.VarChar)
                        drColDataRow.Item("ColFooterBorderColorBottom") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBorderColorBottom"), SqlDbType.VarChar)
                        drColDataRow.Item("ColFooterBorderColorLeft") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBorderColorLeft"), SqlDbType.VarChar)
                        drColDataRow.Item("ColFooterBorderColorRight") = mDataHandler.DataValue_Out(.Rows(IntCounter).Item("ColFooterBorderColorRight"), SqlDbType.VarChar)

                        drColDataRow.Item("ColFooterPaddingLeft") = IIf(.Rows(IntCounter).Item("ColFooterPaddingLeft") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColFooterPaddingLeft"))
                        drColDataRow.Item("ColFooterPaddingRight") = IIf(.Rows(IntCounter).Item("ColFooterPaddingRight") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColFooterPaddingRight"))
                        drColDataRow.Item("ColFooterPaddingTop") = IIf(.Rows(IntCounter).Item("ColFooterPaddingTop") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColFooterPaddingTop"))
                        drColDataRow.Item("ColFooterPaddingBottom") = IIf(.Rows(IntCounter).Item("ColFooterPaddingBottom") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColFooterPaddingBottom"))
                        drColDataRow.Item("ColumnFooterValignment") = IIf(.Rows(IntCounter).Item("ColumnFooterValignment") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColumnFooterValignment"))
                        drColDataRow.Item("ColumnFooterHalignment") = IIf(.Rows(IntCounter).Item("ColumnFooterHalignment") Is DBNull.Value, 0, .Rows(IntCounter).Item("ColumnFooterHalignment"))
                        dtTargetTable.Rows.Add(drColDataRow)
                    Next
                Else
                    Return False
                End If
            End With
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
        Return True
    End Function
    Public Function ReGenerateGridDataSource(ByRef uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal dsDataSource As DataSet, ByVal dtCalcFields As DataTable) As Boolean
        Try
            Dim dcol As DataColumn
            Dim IntRemovedItemIndex As Integer = -1
            If dtCalcFields.Rows.Count > 0 Then
                For IntCounter As Int16 = 0 To dtCalcFields.Rows.Count - 1
                    If dtCalcFields.Rows(IntCounter).Item("IsCanceled") <> True Then
                        dcol = New DataColumn
                        dcol.ColumnName = dtCalcFields.Rows(IntCounter).Item("ColumnName")
                        dcol.DataType = GetType(Global.System.Single)
                        dcol.Expression = dtCalcFields.Rows(IntCounter).Item("Expression")
                        dsDataSource.Tables(0).Columns.Add(dcol)
                    Else
                        Dim IntColumnIndex As UInt16 = uwgGrid.Columns.IndexOf(dtCalcFields.Rows(IntCounter).Item("FieldName"))
                        IntRemovedItemIndex = IntCounter
                        uwgGrid.Columns.RemoveAt(IntColumnIndex)
                    End If

                Next
                If IntRemovedItemIndex <> -1 Then dtCalcFields.Rows.Remove(dtCalcFields.Rows(IntRemovedItemIndex))
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try

    End Function
    Public Function ApplayGridProperties(ByRef uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal dtDataTable As Data.DataTable) As Boolean

        Dim clsRepProperties As New ClsRpw_ReportsMain(mPage)
        Dim mDataHandler As New Venus.Shared.DataHandler

        Dim objFontSize As UI.WebControls.FontSize
        Try

            With dtDataTable.Rows(0)
                uwgGrid.DisplayLayout.RowStyleDefault.BackColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ReportBackColor"), Data.SqlDbType.VarChar))
                uwgGrid.DisplayLayout.RowAlternateStyleDefault.BackColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ReportAlternatingColor"), Data.SqlDbType.VarChar))
                uwgGrid.DisplayLayout.RowStyleDefault.BorderColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("RowsBorderColor"), Data.SqlDbType.VarChar))
                uwgGrid.DisplayLayout.RowStyleDefault.ForeColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ReportForeColor"), Data.SqlDbType.VarChar))

                uwgGrid.Font.Name = mDataHandler.DataValue_Out(.Item("ReportFont"), Data.SqlDbType.VarChar)
                uwgGrid.Font.Bold = IIf(mDataHandler.DataValue_Out(.Item("ReportFontIsBold"), Data.SqlDbType.VarChar) = "Yes", True, False)
                uwgGrid.Font.Italic = IIf(mDataHandler.DataValue_Out(.Item("ReportFontIsItalic"), Data.SqlDbType.VarChar) = "Yes", True, False)
                uwgGrid.Font.Underline = IIf(mDataHandler.DataValue_Out(.Item("ReportFontIsUnderLine"), Data.SqlDbType.VarChar) = "Yes", True, False)

                objFontSize = New UI.WebControls.FontSize

                If .Item("ReportFontSize") Is DBNull.Value Then
                    uwgGrid.Font.Size = UI.WebControls.FontUnit.Point(12)
                Else
                    'uwgGrid.Font.Size = clsRepProperties.GetFontSizesfromStrings(mDataHandler.DataValue_Out(.Item("ReportFontSize"), Data.SqlDbType.VarChar))
                    uwgGrid.Font.Size = UI.WebControls.FontUnit.Point(mDataHandler.DataValue_Out(.Item("ReportFontSize"), Data.SqlDbType.Int))
                End If

                uwgGrid.DisplayLayout.HeaderStyleDefault.HorizontalAlign = CInt(mDataHandler.DataValue_Out(.Item("HeaderHAlignment"), Data.SqlDbType.Int))
                uwgGrid.DisplayLayout.HeaderStyleDefault.VerticalAlign = CInt(mDataHandler.DataValue_Out(.Item("HeaderVAlignment"), Data.SqlDbType.Int))

            End With

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function ApplayGridHeaderNFooter(ByRef uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal dtDataTable As Data.DataTable) As Boolean

        Dim clsRepProperties As New ClsRpw_ReportsMain(mPage)
        Dim mDataHandler As New Venus.Shared.DataHandler

        Dim objFontSize As UI.WebControls.FontSize
        Try
            With dtDataTable.Rows(0)
                If Not .Item("HeaderBorderColor") Is DBNull.Value Then
                    uwgGrid.DisplayLayout.HeaderStyleDefault.BorderDetails.ColorRight = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("HeaderBorderColor"), Data.SqlDbType.VarChar))
                    uwgGrid.DisplayLayout.HeaderStyleDefault.BorderDetails.ColorLeft = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("HeaderBorderColor"), Data.SqlDbType.VarChar))
                    uwgGrid.DisplayLayout.HeaderStyleDefault.BorderDetails.ColorTop = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("HeaderBorderColor"), Data.SqlDbType.VarChar))
                    uwgGrid.DisplayLayout.HeaderStyleDefault.BorderDetails.ColorBottom = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("HeaderBorderColor"), Data.SqlDbType.VarChar))
                End If

                If Not .Item("HeaderForeColor") Is DBNull.Value Then uwgGrid.DisplayLayout.HeaderStyleDefault.ForeColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("HeaderForeColor"), Data.SqlDbType.VarChar))
                If Not .Item("HeaderBackColor") Is DBNull.Value Then uwgGrid.DisplayLayout.HeaderStyleDefault.BackColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("HeaderBackColor"), Data.SqlDbType.VarChar))

                If Not .Item("HeaderFont") Is DBNull.Value Then uwgGrid.DisplayLayout.HeaderStyleDefault.Font.Name = mDataHandler.DataValue_Out(.Item("HeaderFont"), Data.SqlDbType.VarChar)
                If Not .Item("HeaderFontIsBold") Is DBNull.Value Then uwgGrid.DisplayLayout.HeaderStyleDefault.Font.Bold = IIf(mDataHandler.DataValue_Out(.Item("HeaderFontIsBold"), Data.SqlDbType.VarChar) = "Yes", True, False)
                If Not .Item("HeaderFontIsItalic") Is DBNull.Value Then uwgGrid.DisplayLayout.HeaderStyleDefault.Font.Italic = IIf(mDataHandler.DataValue_Out(.Item("HeaderFontIsItalic"), Data.SqlDbType.VarChar) = "Yes", True, False)
                If Not .Item("HeaderFontIsUnderLine") Is DBNull.Value Then uwgGrid.DisplayLayout.HeaderStyleDefault.Font.Underline = IIf(mDataHandler.DataValue_Out(.Item("HeaderFontIsUnderLine"), Data.SqlDbType.VarChar) = "Yes", True, False)
                If Not .Item("BorderStyle") Is DBNull.Value Then uwgGrid.DisplayLayout.HeaderStyleDefault.BorderStyle = mDataHandler.DataValue_Out(.Item("BorderStyle"), Data.SqlDbType.VarChar)

                If Not .Item("HeaderBorderWidth") Is DBNull.Value Then
                    uwgGrid.DisplayLayout.HeaderStyleDefault.BorderStyle = mDataHandler.DataValue_Out(.Item("HeaderBorderWidth"), Data.SqlDbType.Int)
                Else
                    uwgGrid.DisplayLayout.HeaderStyleDefault.BorderWidth = 0
                End If
                'uwgGrid.DisplayLayout.HeaderStyleDefault.BorderWidth = 0


                objFontSize = New UI.WebControls.FontSize
                'uwgGrid.DisplayLayout.HeaderStyleDefault.Font.Size = clsRepProperties.GetFontSizesfromStrings(mDataHandler.DataValue_Out(.Item("HeaderFontSize"), Data.SqlDbType.VarChar))

                uwgGrid.DisplayLayout.HeaderStyleDefault.Font.Size = UI.WebControls.FontUnit.Point(mDataHandler.DataValue_Out(.Item("HeaderFontSize"), Data.SqlDbType.Int))


                'uwgGrid.DisplayLayout.HeaderStyleDefault.HorizontalAlign = CInt(mDataHandler.DataValue_Out(.Item("HeaderHAlignment"), Data.SqlDbType.Int))
                'uwgGrid.DisplayLayout.HeaderStyleDefault.VerticalAlign = CInt(mDataHandler.DataValue_Out(.Item("HeaderVAlignment"), Data.SqlDbType.Int))
                'Footer 
                uwgGrid.DisplayLayout.FooterStyleDefault.BorderDetails.ColorRight = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("FooterBorderColor"), Data.SqlDbType.VarChar))
                uwgGrid.DisplayLayout.FooterStyleDefault.BorderDetails.ColorLeft = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("FooterBorderColor"), Data.SqlDbType.VarChar))
                uwgGrid.DisplayLayout.FooterStyleDefault.BorderDetails.ColorTop = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("FooterBorderColor"), Data.SqlDbType.VarChar))
                uwgGrid.DisplayLayout.FooterStyleDefault.BorderDetails.ColorBottom = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("FooterBorderColor"), Data.SqlDbType.VarChar))

                uwgGrid.DisplayLayout.FooterStyleDefault.ForeColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("FooterForeColor"), Data.SqlDbType.VarChar))
                uwgGrid.DisplayLayout.FooterStyleDefault.BackColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("FooterBackColor"), Data.SqlDbType.VarChar))

                uwgGrid.DisplayLayout.FooterStyleDefault.Font.Name = mDataHandler.DataValue_Out(.Item("FooterFont"), Data.SqlDbType.VarChar)
                uwgGrid.DisplayLayout.FooterStyleDefault.Font.Bold = IIf(mDataHandler.DataValue_Out(.Item("FooterFontIsBold"), Data.SqlDbType.VarChar) = "Yes", True, False)
                uwgGrid.DisplayLayout.FooterStyleDefault.Font.Italic = IIf(mDataHandler.DataValue_Out(.Item("FooterFontIsItalic"), Data.SqlDbType.VarChar) = "Yes", True, False)
                uwgGrid.DisplayLayout.FooterStyleDefault.Font.Underline = IIf(mDataHandler.DataValue_Out(.Item("FooterFontIsUnderLine"), Data.SqlDbType.VarChar) = "Yes", True, False)
                If Not .Item("FooterBorderStyle") Is DBNull.Value Then uwgGrid.DisplayLayout.FooterStyleDefault.BorderStyle = mDataHandler.DataValue_Out(.Item("FooterBorderStyle"), Data.SqlDbType.VarChar)
                objFontSize = New UI.WebControls.FontSize
                'uwgGrid.DisplayLayout.FooterStyleDefault.Font.Size = UI.WebControls.FontUnit.Point(clsRepProperties.GetFontSizesfromStrings(mDataHandler.DataValue_Out(.Item("FooterFontSize"), Data.SqlDbType.VarChar)))
                uwgGrid.DisplayLayout.FooterStyleDefault.Font.Size = UI.WebControls.FontUnit.Point(mDataHandler.DataValue_Out(.Item("FooterFontSize"), SqlDbType.Int))

                If Not .Item("HeaderHeight") Is DBNull.Value Then uwgGrid.DisplayLayout.HeaderStyleDefault.Height = CInt(.Item("HeaderHeight"))
                If Not .Item("FooterHeight") Is DBNull.Value Then uwgGrid.DisplayLayout.FooterStyleDefault.Height = CInt(.Item("FooterHeight"))


            End With

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function ApplayGridColumnsProperties(ByRef uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal dtDataTable As Data.DataTable, ByVal StrSizeInfo As String, Optional ByVal intRowHeight As Integer = 25, Optional ByVal blLanguage As Boolean = False, Optional ByVal blViewer As Boolean = False) As Boolean

        Dim clsRepProperties As New ClsRpw_ReportsMain(mPage)
        Dim mDataHandler As New Venus.Shared.DataHandler
        Dim objFontSize As UI.WebControls.FontSize = Nothing
        Dim IntRowIndex As Integer = 0
        Dim IntColumnIndex As Integer = 0
        Dim IntColumnStyle As Integer
        Dim IntBorderWidth As Integer
        Dim IntColumnWidth As Integer
        Dim StrColumnCaption As String = String.Empty
        Dim strColFooterCaption As String = String.Empty
        Try
            If dtDataTable.Rows.Count > 0 Then
                For Each uwgColumn As Infragistics.WebUI.UltraWebGrid.UltraGridColumn In uwgGrid.Columns
                    IntRowIndex = 0
                    'Iterate Through Columns Settings Table 
                    For Each drDataRow As Data.DataRow In dtDataTable.Rows
                        'Validate If Column Has Settings Or Not And Hence Applay its settings 
                        '//Replace Column Index  with Column Name 
                        'If drDataRow.Item("ColumnIndex") = IntColumnIndex And drDataRow.Item("IsCanceled") <> True Then
                        If uwgColumn.Key = drDataRow.Item("FieldName") And drDataRow.Item("IsCanceled") <> True Then
                            If blViewer Then
                                If blLanguage = True And drDataRow.Item("FieldLanguage") = 2 Or (Not blLanguage And drDataRow.Item("FieldLanguage") = 1) Then
                                    uwgColumn.Hidden = True
                                    Continue For
                                ElseIf blLanguage = True Then
                                    StrColumnCaption = "EngDescription"
                                    strColFooterCaption = "EngFooterCaption"
                                ElseIf blLanguage = False Then
                                    StrColumnCaption = "ArbDescription"
                                    strColFooterCaption = "ArbFooterCaption"
                                End If

                            Else
                                StrColumnCaption = "EngDescription"
                                strColFooterCaption = "EngFooterCaption"
                            End If

                            IntColumnIndex = uwgColumn.Index
                            With dtDataTable.Rows(IntRowIndex)
                                uwgGrid.Columns(IntColumnIndex).Hidden = mDataHandler.DataValue_Out(.Item("ColumnIsHidden"), Data.SqlDbType.Bit)
                                'According to Language Displays English/Arabic Caption 
                                uwgGrid.Columns(IntColumnIndex).Header.Caption = mDataHandler.DataValue_Out(.Item(StrColumnCaption), Data.SqlDbType.VarChar)
                                uwgGrid.Columns(IntColumnIndex).Footer.Caption = mDataHandler.DataValue_Out(.Item(strColFooterCaption), Data.SqlDbType.VarChar)
                                IntColumnWidth = GetAndSetColumnsSizes(uwgColumn.Key, StrSizeInfo)
                                If IntColumnWidth > 0 Then
                                    uwgGrid.Columns(IntColumnIndex).Width = IntColumnWidth
                                ElseIf mDataHandler.DataValue_Out(.Item("ColumnWidth"), Data.SqlDbType.Int) <> 0 Then
                                    uwgGrid.Columns(IntColumnIndex).Width = CInt(.Item("ColumnWidth"))
                                End If

                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Height = intRowHeight
                                uwgGrid.DisplayLayout.Bands(0).DefaultRowHeight = intRowHeight
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Height = intRowHeight
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.VerticalAlign = mDataHandler.DataValue_Out(.Item("ColumnValignment"), Data.SqlDbType.Int)
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.HorizontalAlign = mDataHandler.DataValue_Out(.Item("ColumnHalignment"), Data.SqlDbType.Int)
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Wrap = mDataHandler.DataValue_Out(.Item("ColumnIsWrap"), SqlDbType.Bit)
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.ForeColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColumnForeColor"), Data.SqlDbType.VarChar))
                                'uwgGrid.DisplayLayout.Bands(0).RowStyle.BackColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColumnBackColor"), Data.SqlDbType.VarChar))
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Font.Name = IIf(mDataHandler.DataValue_In(.Item("ColumnFont"), Data.SqlDbType.VarChar) Is DBNull.Value, "Arial", mDataHandler.DataValue_In(.Item("ColumnFont"), Data.SqlDbType.VarChar))
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Font.Bold = mDataHandler.DataValue_Out(.Item("ColumnFontIsBold"), Data.SqlDbType.Bit)
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Font.Italic = mDataHandler.DataValue_Out(.Item("ColumnFontIsItalic"), Data.SqlDbType.Bit)
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Font.Underline = mDataHandler.DataValue_Out(.Item("ColumnFontIsUnderLine"), Data.SqlDbType.Bit)
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Font.Size = UI.WebControls.FontUnit.Point(mDataHandler.DataValue_Out(.Item("ColumnFontSize"), SqlDbType.Int))
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Margin.Bottom = CInt(mDataHandler.DataValue_Out(.Item("BottomMargin"), Data.SqlDbType.Int))
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Margin.Top = CInt(mDataHandler.DataValue_Out(.Item("TopMargin"), Data.SqlDbType.Int))
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Margin.Left = CInt(mDataHandler.DataValue_Out(.Item("LeftMargin"), Data.SqlDbType.Int))
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.Margin.Right = CInt(mDataHandler.DataValue_Out(.Item("RightMargin"), Data.SqlDbType.Int))
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("BorderColor"), Data.SqlDbType.VarChar))
                                '------------------------------------
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.StyleTop = mDataHandler.DataValue_Out(.Item("TopBorderStyle"), Data.SqlDbType.VarChar)
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.StyleBottom = mDataHandler.DataValue_Out(.Item("BottomBorderStyle"), Data.SqlDbType.VarChar)
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.StyleLeft = mDataHandler.DataValue_Out(.Item("LeftBorderStyle"), Data.SqlDbType.VarChar)
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.StyleRight = mDataHandler.DataValue_Out(.Item("RightBorderStyle"), Data.SqlDbType.VarChar)
                                '------------------------------------
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.ColorTop = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("TopBorderColor"), Data.SqlDbType.VarChar))
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.ColorBottom = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("BottomBorderColor"), Data.SqlDbType.VarChar))
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.ColorLeft = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("LeftBorderColor"), Data.SqlDbType.VarChar))
                                uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.ColorRight = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("RightBorderColor"), Data.SqlDbType.VarChar))
                                Try
                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("BorderStyle"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderStyle = mDataHandler.DataValue_Out(.Item("BorderStyle"), Data.SqlDbType.Int)
                                Catch ex As Exception
                                    uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderStyle = 0
                                End Try
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("BorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderWidth = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("BorderWidth"), Data.SqlDbType.Int)))
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("BottomBorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.WidthBottom = CInt(mDataHandler.DataValue_Out(.Item("BottomBorderWidth"), Data.SqlDbType.Int))
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("TopBorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.WidthTop = CInt(mDataHandler.DataValue_Out(.Item("TopBorderWidth"), Data.SqlDbType.Int))
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("LeftBorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.WidthLeft = CInt(mDataHandler.DataValue_Out(.Item("LeftBorderWidth"), Data.SqlDbType.Int))
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("RightBorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.DisplayLayout.Bands(0).RowStyle.BorderDetails.WidthRight = CInt(mDataHandler.DataValue_Out(.Item("RightBorderWidth"), Data.SqlDbType.Int))
                                ''''*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*'''''
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Height = intRowHeight
                                uwgGrid.Columns(IntColumnIndex).CellStyle.VerticalAlign = mDataHandler.DataValue_Out(.Item("ColumnValignment"), Data.SqlDbType.Int)
                                uwgGrid.Columns(IntColumnIndex).CellStyle.HorizontalAlign = mDataHandler.DataValue_Out(.Item("ColumnHalignment"), Data.SqlDbType.Int)
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Wrap = mDataHandler.DataValue_Out(.Item("ColumnIsWrap"), SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).CellStyle.ForeColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColumnForeColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.BackColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColumnBackColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Font.Name = IIf(mDataHandler.DataValue_In(.Item("ColumnFont"), Data.SqlDbType.VarChar) Is DBNull.Value, "Arial", mDataHandler.DataValue_In(.Item("ColumnFont"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Font.Bold = mDataHandler.DataValue_Out(.Item("ColumnFontIsBold"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Font.Italic = mDataHandler.DataValue_Out(.Item("ColumnFontIsItalic"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Font.Underline = mDataHandler.DataValue_Out(.Item("ColumnFontIsUnderLine"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Font.Size = UI.WebControls.FontUnit.Point(mDataHandler.DataValue_Out(.Item("ColumnFontSize"), SqlDbType.Int))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Margin.Bottom = CInt(mDataHandler.DataValue_Out(.Item("BottomMargin"), Data.SqlDbType.Int))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Margin.Top = CInt(mDataHandler.DataValue_Out(.Item("TopMargin"), Data.SqlDbType.Int))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Margin.Left = CInt(mDataHandler.DataValue_Out(.Item("LeftMargin"), Data.SqlDbType.Int))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Margin.Right = CInt(mDataHandler.DataValue_Out(.Item("RightMargin"), Data.SqlDbType.Int))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.BorderColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("BorderColor"), Data.SqlDbType.VarChar))
                                '------------------------------------
                                Try
                                    uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleTop = mDataHandler.DataValue_Out(.Item("TopBorderStyle"), Data.SqlDbType.VarChar)
                                    uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleBottom = mDataHandler.DataValue_Out(.Item("BottomBorderStyle"), Data.SqlDbType.VarChar)
                                    uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleLeft = mDataHandler.DataValue_Out(.Item("LeftBorderStyle"), Data.SqlDbType.VarChar)
                                    uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleRight = mDataHandler.DataValue_Out(.Item("RightBorderStyle"), Data.SqlDbType.VarChar)
                                Catch ex As Exception
                                    uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleTop = 0
                                    uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleBottom = 0
                                    uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleLeft = 0
                                    uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.StyleRight = 0
                                End Try
                                '------------------------------------
                                Dim StrColor = mDataHandler.DataValue_Out(.Item("TopBorderColor"), Data.SqlDbType.VarChar)
                                uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.ColorTop = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("TopBorderColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.ColorBottom = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("BottomBorderColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.ColorLeft = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("LeftBorderColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.ColorRight = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("RightBorderColor"), Data.SqlDbType.VarChar))
                                Try
                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("BorderStyle"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).CellStyle.BorderStyle = mDataHandler.DataValue_Out(.Item("BorderStyle"), Data.SqlDbType.Int)
                                Catch ex As Exception
                                    uwgGrid.Columns(IntColumnIndex).CellStyle.BorderStyle = 0
                                End Try
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("BorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).CellStyle.BorderWidth = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("BorderWidth"), Data.SqlDbType.Int)))
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("BottomBorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.WidthBottom = CInt(mDataHandler.DataValue_Out(.Item("BottomBorderWidth"), Data.SqlDbType.Int))
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("TopBorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.WidthTop = CInt(mDataHandler.DataValue_Out(.Item("TopBorderWidth"), Data.SqlDbType.Int))
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("LeftBorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.WidthLeft = CInt(mDataHandler.DataValue_Out(.Item("LeftBorderWidth"), Data.SqlDbType.Int))
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("RightBorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).CellStyle.BorderDetails.WidthRight = CInt(mDataHandler.DataValue_Out(.Item("RightBorderWidth"), Data.SqlDbType.Int))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Padding.Top = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("TopPadding"), Data.SqlDbType.Int)))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Padding.Bottom = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("BottomPadding"), Data.SqlDbType.Int)))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Padding.Left = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("LeftPadding"), Data.SqlDbType.Int)))
                                uwgGrid.Columns(IntColumnIndex).CellStyle.Padding.Right = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("RightPadding"), Data.SqlDbType.Int)))
                                If Not .Item("FooterTotal") Is DBNull.Value Then
                                    If .Item("FooterTotal").ToString.Length > 0 Then
                                        uwgGrid.Columns(IntColumnIndex).Footer.Total = clsRepProperties.GetFooterTotal(.Item("FooterTotal"))
                                    End If
                                End If
                                uwgGrid.Columns(IntColumnIndex).Format = mDataHandler.DataValue_Out(.Item("format"), Data.SqlDbType.VarChar)
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.HorizontalAlign = mDataHandler.DataValue_Out(.Item("ColumnHeaderHalignment"), Data.SqlDbType.Int)
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.VerticalAlign = mDataHandler.DataValue_Out(.Item("ColumnHeaderValignment"), Data.SqlDbType.Int)
                                '============= [Header][Begin]
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BackColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColHeaderBackColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.ForeColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColHeaderForeColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Name = IIf(mDataHandler.DataValue_In(.Item("ColHeaderFont"), Data.SqlDbType.VarChar) Is DBNull.Value, "Arial", mDataHandler.DataValue_In(.Item("ColHeaderFont"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Bold = mDataHandler.DataValue_Out(.Item("ColHeaderFontIsBold"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Italic = mDataHandler.DataValue_Out(.Item("ColHeaderFontIsItalic"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Underline = mDataHandler.DataValue_Out(.Item("ColHeaderFontIsUnderline"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Strikeout = mDataHandler.DataValue_Out(.Item("ColHeaderFontIsStrikeOut"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Overline = mDataHandler.DataValue_Out(.Item("ColHeaderFontIsOverLine"), Data.SqlDbType.Bit)

                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Font.Size = UI.WebControls.FontUnit.Point(mDataHandler.DataValue_Out(.Item("ColHeaderFontSize"), SqlDbType.Int))

                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("ColHeaderBorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderWidth = IntBorderWidth
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("ColHeaderBorderWidthBottom"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.WidthBottom = IntBorderWidth
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("ColHeaderBorderWidthLeft"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.WidthLeft = IntBorderWidth
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("ColHeaderBorderWidthRight"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.WidthRight = IntBorderWidth
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("ColHeaderBorderWidthTop"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.WidthTop = IntBorderWidth


                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColHeaderBorderColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.ColorBottom = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColHeaderBorderColorBottom"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.ColorLeft = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColHeaderBorderColorLeft"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.ColorRight = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColHeaderBorderColorRight"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.ColorTop = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColHeaderBorderColorTop"), Data.SqlDbType.VarChar))

                                Try
                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("ColHeaderBorderStyle"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderStyle = IntColumnStyle
                                Catch ex As Exception
                                    uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderStyle = 0
                                End Try
                                Try
                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("ColHeaderBorderStyleBottom"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleBottom = IntColumnStyle
                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("ColHeaderBorderStyleLeft"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleLeft = IntColumnStyle
                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("ColHeaderBorderStyleRight"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleRight = IntColumnStyle
                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("ColHeaderBorderStyleTop"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleTop = IntColumnStyle
                                Catch ex As Exception
                                    uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleTop = 0
                                    uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleBottom = 0
                                    uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleLeft = 0
                                    uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.BorderDetails.StyleRight = 0
                                End Try
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Padding.Bottom = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("ColHeaderPaddingBottom"), Data.SqlDbType.Int)))
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Padding.Left = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("ColHeaderPaddingLeft"), Data.SqlDbType.Int)))
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Padding.Right = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("ColHeaderPaddingRight"), Data.SqlDbType.Int)))
                                uwgGrid.Columns(IntColumnIndex).Band.HeaderStyle.Padding.Top = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("ColHeaderPaddingTop"), Data.SqlDbType.Int)))
                                '============ [Header][Eng]
                                '============ [Footer][Begin]
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.HorizontalAlign = mDataHandler.DataValue_Out(.Item("ColumnFooterHalignment"), Data.SqlDbType.Int)
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.VerticalAlign = mDataHandler.DataValue_Out(.Item("ColumnFooterValignment"), Data.SqlDbType.Int)
                                '========= Footer Allignment Added By [0256] 0258 [End]
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BackColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColFooterBackColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.ForeColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColFooterForeColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Name = IIf(mDataHandler.DataValue_In(.Item("ColFooterFont"), Data.SqlDbType.VarChar) Is DBNull.Value, "Arial", mDataHandler.DataValue_In(.Item("ColFooterFont"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Bold = mDataHandler.DataValue_Out(.Item("ColFooterFontIsBold"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Italic = mDataHandler.DataValue_Out(.Item("ColFooterFontIsItalic"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Underline = mDataHandler.DataValue_Out(.Item("ColFooterFontIsUnderline"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Strikeout = mDataHandler.DataValue_Out(.Item("ColFooterFontIsStrikeOut"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Overline = mDataHandler.DataValue_Out(.Item("ColFooterFontIsOverLine"), Data.SqlDbType.Bit)
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Font.Size = UI.WebControls.FontUnit.Point(mDataHandler.DataValue_Out(.Item("ColFooterFontSize"), SqlDbType.Int))

                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("ColFooterBorderWidth"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderWidth = IntBorderWidth
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("ColFooterBorderWidthBottom"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.WidthBottom = IntBorderWidth
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("ColFooterBorderWidthLeft"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.WidthLeft = IntBorderWidth
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("ColFooterBorderWidthRight"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.WidthRight = IntBorderWidth
                                IntBorderWidth = mDataHandler.DataValue_Out(.Item("ColFooterBorderWidthTop"), Data.SqlDbType.Int)
                                If IntBorderWidth > 0 Then uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.WidthTop = IntBorderWidth


                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColFooterBorderColor"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.ColorBottom = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColFooterBorderColorBottom"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.ColorLeft = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColFooterBorderColorLeft"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.ColorRight = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColFooterBorderColorRight"), Data.SqlDbType.VarChar))
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.ColorTop = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ColFooterBorderColorTop"), Data.SqlDbType.VarChar))

                                Try
                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("ColFooterBorderStyle"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderStyle = IntColumnStyle
                                Catch ex As Exception
                                    uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderStyle = 0
                                End Try
                                Try

                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("ColFooterBorderStyleBottom"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleBottom = IntColumnStyle

                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("ColFooterBorderStyleLeft"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleLeft = IntColumnStyle

                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("ColFooterBorderStyleRight"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleRight = IntColumnStyle

                                    IntColumnStyle = mDataHandler.DataValue_Out(.Item("ColFooterBorderStyleTop"), Data.SqlDbType.Int)
                                    If IntColumnStyle <> 0 Then uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleTop = IntColumnStyle
                                Catch ex As Exception
                                    uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleTop = 0
                                    uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleBottom = 0
                                    uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleLeft = 0
                                    uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.BorderDetails.StyleRight = 0
                                End Try
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Padding.Bottom = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("ColFooterPaddingBottom"), Data.SqlDbType.Int)))
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Padding.Left = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("ColFooterPaddingLeft"), Data.SqlDbType.Int)))
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Padding.Right = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("ColFooterPaddingRight"), Data.SqlDbType.Int)))
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.Padding.Top = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("ColFooterPaddingTop"), Data.SqlDbType.Int)))

                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.HorizontalAlign = mDataHandler.DataValue_Out(.Item("ColumnFooterHalignment"), Data.SqlDbType.Int)
                                uwgGrid.Columns(IntColumnIndex).Band.FooterStyle.VerticalAlign = mDataHandler.DataValue_Out(.Item("ColumnFooterValignment"), Data.SqlDbType.Int)
                                'This Action was done only in preview mode 
                                'uwgGrid.Columns(IntColumnIndex).IsGroupByColumn = mDataHandler.DataValue_Out(.Item("IsGroupBy"), Data.SqlDbType.Bit)
                                '============ [Footer] [End]
                            End With
                        End If
                        IntRowIndex += 1
                    Next
                    IntColumnIndex += 1
                Next
            End If
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    'Modifications : 
    '              : 27-12-2007  [0256] 0256 Check Null Values And Ignore Assigning it to grid properties
    Public Function ApplayGridRowsProperties(ByRef uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal dtDataTable As Data.DataTable) As Boolean
        If dtDataTable.Rows.Count > 0 Then
            Dim clsRepProperties As New ClsRpw_ReportsMain(mPage)
            Dim mDataHandler As New Venus.Shared.DataHandler
            Dim objFontSize As UI.WebControls.FontSize
            Try
                With dtDataTable.Rows(0)
                    uwgGrid.DisplayLayout.RowStyleDefault.BorderStyle = mDataHandler.DataValue_Out(.Item("BorderStyle"), Data.SqlDbType.Int)
                    uwgGrid.DisplayLayout.RowStyleDefault.BorderWidth = Global.System.Web.UI.WebControls.Unit.Pixel(CInt(mDataHandler.DataValue_Out(.Item("BorderWidth"), Data.SqlDbType.Int)))
                    uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.StyleRight = mDataHandler.DataValue_Out(.Item("RightBorderStyle"), Data.SqlDbType.Int)
                    uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.StyleLeft = mDataHandler.DataValue_Out(.Item("LeftBorderStyle"), Data.SqlDbType.Int)
                    uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.StyleTop = mDataHandler.DataValue_Out(.Item("TopBorderStyle"), Data.SqlDbType.Int)
                    uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.StyleBottom = mDataHandler.DataValue_Out(.Item("BottomBorderStyle"), Data.SqlDbType.Int)
                    If Not .Item("BorderColor") = Drawing.Color.Transparent.ToString Then
                        uwgGrid.DisplayLayout.RowStyleDefault.BorderColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("BorderColor"), Data.SqlDbType.VarChar))
                    End If
                    If Not .Item("ForeColor") = Drawing.Color.Transparent.ToString Then
                        uwgGrid.DisplayLayout.RowStyleDefault.ForeColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("ForeColor"), Data.SqlDbType.VarChar))
                    End If
                    If Not .Item("BackColor") = Drawing.Color.Transparent.ToString Then
                        uwgGrid.DisplayLayout.RowStyleDefault.BackColor = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("BackColor"), Data.SqlDbType.VarChar))
                    End If
                    If Not .Item("TopBorderColor") = Drawing.Color.Transparent.ToString Then
                        uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.ColorRight = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("TopBorderColor"), Data.SqlDbType.VarChar))
                    End If
                    If Not .Item("BottomBorderColor") = Drawing.Color.Transparent.ToString Then
                        uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.ColorLeft = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("BottomBorderColor"), Data.SqlDbType.VarChar))
                    End If
                    If Not .Item("LeftBorderColor") = Drawing.Color.Transparent.ToString Then
                        uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.ColorTop = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("LeftBorderColor"), Data.SqlDbType.VarChar))
                    End If
                    If Not .Item("RightBorderColor") = Drawing.Color.Transparent.ToString Then
                        uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.ColorBottom = Drawing.Color.FromName(mDataHandler.DataValue_Out(.Item("RightBorderColor"), Data.SqlDbType.VarChar))
                    End If
                    uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.WidthBottom = CInt(mDataHandler.DataValue_Out(.Item("TopBorderWidth"), Data.SqlDbType.Int))
                    uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.WidthTop = CInt(mDataHandler.DataValue_Out(.Item("BottomBorderWidth"), Data.SqlDbType.Int))
                    uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.WidthLeft = CInt(mDataHandler.DataValue_Out(.Item("LeftBorderWidth"), Data.SqlDbType.Int))
                    uwgGrid.DisplayLayout.RowStyleDefault.BorderDetails.WidthRight = CInt(mDataHandler.DataValue_Out(.Item("RightBorderWidth"), Data.SqlDbType.Int))
                    uwgGrid.Font.Name = mDataHandler.DataValue_Out(.Item("Font"), Data.SqlDbType.VarChar)
                    uwgGrid.Font.Bold = .Item("FontIsBold")
                    uwgGrid.Font.Italic = .Item("FontIsItalic")
                    uwgGrid.Font.Underline = .Item("FontIsUnderLine")
                    If .Item("FontSize") Is DBNull.Value Then
                        uwgGrid.Font.Size = UI.WebControls.FontUnit.Point(12)
                    Else
                        uwgGrid.Font.Size = UI.WebControls.FontUnit.Point(mDataHandler.DataValue_Out(.Item("FontSize"), Data.SqlDbType.Int))
                    End If
                    If Not .Item("RowHieght") Is DBNull.Value Or CInt(.Item("RowHieght")) > 0 Then
                        uwgGrid.DisplayLayout.RowHeightDefault = CInt(.Item("RowHieght"))
                    End If
                End With
            Catch ex As Exception
                mPage.Session.Add("ErrorValue", ex)
                mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
                mPage.Response.Redirect("ErrorPage.aspx")
            End Try
        End If
    End Function
    Public Function RestoreDefaultSettings(ByRef uwgGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByRef dsDataSet As Data.DataSet)
        Try
            If Not IsNothing(dsDataSet) Then
                ApplayGridProperties(uwgGrid, dsDataSet.Tables(0))
            End If
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function PrepareFormulaStringDT(ByVal strFormulaToBePrepared As String, ByVal txtTarget As Web.UI.WebControls.TextBox) As String
        Try
            ' Dim strFormula As String = String.Empty
            strFormulaToBePrepared = strFormulaToBePrepared
            strFormulaToBePrepared = strFormulaToBePrepared.Replace("$", "")
            strFormulaToBePrepared = strFormulaToBePrepared.Replace("<", "")
            strFormulaToBePrepared = strFormulaToBePrepared.Replace(">", "")
            txtTarget.Text = String.Empty

            Return strFormulaToBePrepared
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function PrepareFormulaString(ByVal strFormulaToBePrepared As String, ByVal txtTarget As Web.UI.WebControls.TextBox) As String
        Try
            strFormulaToBePrepared = strFormulaToBePrepared
            strFormulaToBePrepared = strFormulaToBePrepared.Replace("$", "")
            strFormulaToBePrepared = strFormulaToBePrepared.Replace("<", "[")
            strFormulaToBePrepared = strFormulaToBePrepared.Replace(">", "]")
            txtTarget.Text = String.Empty
            Return strFormulaToBePrepared
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Public Function GetAndSetColumnsSizes(ByVal StrColumnName As String, ByVal StrsizeInfo As String, ByRef txtColumnWidth As Infragistics.WebUI.WebDataInput.WebNumericEdit)
        Try
            StrsizeInfo = StrsizeInfo.Replace("px$", "$")
            StrsizeInfo = StrsizeInfo.TrimEnd("$")
            Dim Sizes() As String = StrsizeInfo.Split("$")
            Dim strColumnNames(1) As String
            Dim Widthes(1) As Integer
            ReDim strColumnNames(Sizes.GetUpperBound(0))
            ReDim Widthes(Sizes.GetUpperBound(0))
            For I As Integer = 0 To Sizes.Length - 1
                strColumnNames(I) = (Sizes(I).Substring(0, Sizes(I).IndexOf("_")))
                Widthes(I) = CInt((Sizes(I).Substring(Sizes(I).IndexOf("_") + 1)))
            Next
            Dim IntWidthIndex As Integer = Array.LastIndexOf(strColumnNames, StrColumnName)
            If IntWidthIndex >= 0 Then txtColumnWidth.Text = Widthes(IntWidthIndex)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
        Return True
    End Function
    Public Function GetAndSetColumnsSizes(ByVal StrColumnName As String, ByVal StrsizeInfo As String) As Integer
        If StrsizeInfo.Length > 0 Then
            Try
                StrsizeInfo = StrsizeInfo.Replace("px$", "$")
                StrsizeInfo = StrsizeInfo.TrimEnd("$")
                Dim Sizes() As String = StrsizeInfo.Split("$")
                Dim strColumnNames(1) As String
                Dim Widthes(1) As Integer
                ReDim strColumnNames(Sizes.GetUpperBound(0))
                ReDim Widthes(Sizes.GetUpperBound(0))
                For I As Integer = 0 To Sizes.Length - 1
                    strColumnNames(I) = (Sizes(I).Substring(0, Sizes(I).IndexOf("_")))
                    Widthes(I) = CInt((Sizes(I).Substring(Sizes(I).IndexOf("_") + 1)))
                Next
                Dim IntWidthIndex As Integer = Array.LastIndexOf(strColumnNames, StrColumnName)
                If IntWidthIndex >= 0 Then
                    Return Widthes(IntWidthIndex)
                Else
                    Return CsDefaultColumnWidth
                End If

            Catch ex As Exception
                mPage.Session.Add("ErrorValue", ex)
                mErrorHandler.RecordExceptions_DataBase(mInsertCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
                mPage.Response.Redirect("ErrorPage.aspx")
            End Try
        End If
    End Function
#End Region

#Region "Private Functions"

    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)
                mReportID = mDataHandler.DataValue_Out(.Item("ReportID"), SqlDbType.Int)
                mRank = mDataHandler.DataValue_Out(.Item("Rank"), SqlDbType.Int)

                mIsCalculated = mDataHandler.DataValue_Out(.Item("IsCalculated"), SqlDbType.Bit)
                mGroupByRank = mDataHandler.DataValue_Out(.Item("GroupByRank"), SqlDbType.Int)


                mBorderColor = mDataHandler.DataValue_Out(.Item("BorderColor"), SqlDbType.VarChar)
                mTopBorderColor = mDataHandler.DataValue_Out(.Item("TopBorderColor"), SqlDbType.VarChar)
                mBottomBorderColor = mDataHandler.DataValue_Out(.Item("BottomBorderColor"), SqlDbType.VarChar)
                mLeftBorderColor = mDataHandler.DataValue_Out(.Item("LeftBorderColor"), SqlDbType.VarChar)
                mRightBorderColor = mDataHandler.DataValue_Out(.Item("RightBorderColor"), SqlDbType.VarChar)

                mBorderStyle = mDataHandler.DataValue_Out(.Item("BorderStyle"), SqlDbType.Int)
                mTopBorderStyle = mDataHandler.DataValue_Out(.Item("TopBorderStyle"), SqlDbType.Int)
                mBottomBorderStyle = mDataHandler.DataValue_Out(.Item("BottomBorderStyle"), SqlDbType.Int)
                mLeftBorderStyle = mDataHandler.DataValue_Out(.Item("LeftBorderStyle"), SqlDbType.Int)
                mRightBorderStyle = mDataHandler.DataValue_Out(.Item("RightBorderStyle"), SqlDbType.Int)

                mBorderWidth = mDataHandler.DataValue_Out(.Item("BorderWidth"), SqlDbType.Int)
                mTopBorderWidth = mDataHandler.DataValue_Out(.Item("TopBorderWidth"), SqlDbType.Int)
                mBottomBorderWidth = mDataHandler.DataValue_Out(.Item("BottomBorderWidth"), SqlDbType.Int)
                mLeftBorderWidth = mDataHandler.DataValue_Out(.Item("LeftBorderWidth"), SqlDbType.Int)
                mRightBorderWidth = mDataHandler.DataValue_Out(.Item("RightBorderWidth"), SqlDbType.Int)

                mEngHeaderCaption = mDataHandler.DataValue_Out(.Item("EngHeaderCaption"), SqlDbType.VarChar)
                mEngFooterCaption = mDataHandler.DataValue_Out(.Item("EngFooterCaption"), SqlDbType.VarChar)
                mArbHeaderCaption = mDataHandler.DataValue_Out(.Item("ArbHeaderCaption"), SqlDbType.VarChar)
                mArbFooterCaption = mDataHandler.DataValue_Out(.Item("ArbFooterCaption"), SqlDbType.VarChar)

                mColumnWidth = mDataHandler.DataValue_Out(.Item("ColumnWidth"), SqlDbType.Int)
                mColumnName = mDataHandler.DataValue_Out(.Item("ColumnName"), SqlDbType.VarChar)
                mColumnHalignment = mDataHandler.DataValue_Out(.Item("ColumnHalignment"), SqlDbType.Int)
                mColumnValignment = mDataHandler.DataValue_Out(.Item("ColumnValignment"), SqlDbType.Int)
                mColumnIsHidden = mDataHandler.DataValue_Out(.Item("ColumnIsHidden"), SqlDbType.Bit)
                mColumnIsWrap = mDataHandler.DataValue_Out(.Item("ColumnIsWrap"), SqlDbType.Bit)
                mColumnIsOverView = mDataHandler.DataValue_Out(.Item("ColumnIsOverView"), SqlDbType.Bit)
                mColumnForeColor = mDataHandler.DataValue_Out(.Item("ColumnForeColor"), SqlDbType.VarChar)
                mColumnBackColor = mDataHandler.DataValue_Out(.Item("ColumnBackColor"), SqlDbType.VarChar)
                mColumnFont = mDataHandler.DataValue_Out(.Item("ColumnFont"), SqlDbType.VarChar)
                mColumnFontIsItalic = mDataHandler.DataValue_Out(.Item("ColumnFontIsItalic"), SqlDbType.Bit)
                mColumnFontIsBold = mDataHandler.DataValue_Out(.Item("ColumnFontIsBold"), SqlDbType.Bit)
                mColumnFontIsUnderLine = mDataHandler.DataValue_Out(.Item("ColumnFontIsUnderLine"), SqlDbType.Bit)
                mColumnFontSize = mDataHandler.DataValue_Out(.Item("ColumnFontSize"), SqlDbType.Int)

                mTopPadding = mDataHandler.DataValue_Out(.Item("TopPadding"), SqlDbType.Int)
                mBottomPadding = mDataHandler.DataValue_Out(.Item("BottomPadding"), SqlDbType.Int)
                mLeftPadding = mDataHandler.DataValue_Out(.Item("LeftPadding"), SqlDbType.Int)
                mRightPadding = mDataHandler.DataValue_Out(.Item("RightPadding"), SqlDbType.Int)
                mTopMargin = mDataHandler.DataValue_Out(.Item("TopMargin"), SqlDbType.Int)
                mBottomMargin = mDataHandler.DataValue_Out(.Item("BottomMargin"), SqlDbType.Int)
                mLeftMargin = mDataHandler.DataValue_Out(.Item("LeftMargin"), SqlDbType.Int)
                mRightMargin = mDataHandler.DataValue_Out(.Item("RightMargin"), SqlDbType.Int)
                mFormula = mDataHandler.DataValue_Out(.Item("Formula"), SqlDbType.VarChar)
                mFooterTotal = mDataHandler.DataValue_Out(.Item("FooterTotal"), SqlDbType.VarChar)
                mIsSorted = mDataHandler.DataValue_Out(.Item("IsSorted"), SqlDbType.Int)
                mIsGroupBy = mDataHandler.DataValue_Out(.Item("IsGroupBy"), SqlDbType.Bit)
                mFormat = mDataHandler.DataValue_Out(.Item("Format"), SqlDbType.VarChar)
                mColumnHeaderHalignment = mDataHandler.DataValue_Out(.Item("ColumnHeaderHalignment"), SqlDbType.Int)
                mColumnHeadervalignment = mDataHandler.DataValue_Out(.Item("ColumnHeadervalignment"), SqlDbType.Int)

                mFieldLanguage = mDataHandler.DataValue_Out(.Item("FieldLanguage"), SqlDbType.Int)
                'Header 
                '============== [Begin]
                mColHeaderBackColor = mDataHandler.DataValue_Out(.Item("ColHeaderBackColor"), SqlDbType.VarChar)
                mColHeaderForeColor = mDataHandler.DataValue_Out(.Item("ColHeaderForeColor"), SqlDbType.VarChar)
                mColHeaderFont = mDataHandler.DataValue_Out(.Item("ColHeaderFont"), SqlDbType.VarChar)
                mColHeaderFontIsItalic = mDataHandler.DataValue_Out(.Item("ColHeaderFontIsItalic"), SqlDbType.Bit)
                mColHeaderFontIsBold = mDataHandler.DataValue_Out(.Item("ColHeaderFontIsBold"), SqlDbType.Bit)
                mColHeaderFontIsUnderline = mDataHandler.DataValue_Out(.Item("ColHeaderFontIsUnderline"), SqlDbType.Bit)
                mColHeaderFontSize = mDataHandler.DataValue_Out(.Item("ColHeaderFontSize"), SqlDbType.Int)
                mColHeaderFontIsStrikeOut = mDataHandler.DataValue_Out(.Item("ColHeaderFontIsStrikeOut"), SqlDbType.Bit)
                mColHeaderFontIsOverLine = mDataHandler.DataValue_Out(.Item("ColHeaderFontIsOverLine"), SqlDbType.Bit)

                mColHeaderBorderColor = mDataHandler.DataValue_Out(.Item("ColHeaderBorderColor"), SqlDbType.VarChar)
                mColHeaderBorderColorTop = mDataHandler.DataValue_Out(.Item("ColHeaderBorderColorTop"), SqlDbType.VarChar)
                mColHeaderBorderColorBottom = mDataHandler.DataValue_Out(.Item("ColHeaderBorderColorBottom"), SqlDbType.VarChar)
                mColHeaderBorderColorLeft = mDataHandler.DataValue_Out(.Item("ColHeaderBorderColorLeft"), SqlDbType.VarChar)
                mColHeaderBorderColorRight = mDataHandler.DataValue_Out(.Item("ColHeaderBorderColorRight"), SqlDbType.VarChar)


                mColHeaderBorderWidth = mDataHandler.DataValue_Out(.Item("ColHeaderBorderWidth"), SqlDbType.Int)
                mColHeaderBorderWidthTop = mDataHandler.DataValue_Out(.Item("ColHeaderBorderWidthTop"), SqlDbType.Int)
                mColHeaderBorderWidthBottom = mDataHandler.DataValue_Out(.Item("ColHeaderBorderWidthBottom"), SqlDbType.Int)
                mColHeaderBorderWidthLeft = mDataHandler.DataValue_Out(.Item("ColHeaderBorderWidthLeft"), SqlDbType.Int)
                mColHeaderBorderWidthRight = mDataHandler.DataValue_Out(.Item("ColHeaderBorderWidthRight"), SqlDbType.Int)


                mColHeaderBorderStyle = mDataHandler.DataValue_Out(.Item("ColHeaderBorderStyle"), SqlDbType.Int)
                mColHeaderBorderStyleTop = mDataHandler.DataValue_Out(.Item("ColHeaderBorderStyleTop"), SqlDbType.Int)
                mColHeaderBorderStyleBottom = mDataHandler.DataValue_Out(.Item("ColHeaderBorderStyleBottom"), SqlDbType.Int)
                mColHeaderBorderStyleLeft = mDataHandler.DataValue_Out(.Item("ColHeaderBorderStyleLeft"), SqlDbType.Int)
                mColHeaderBorderStyleRight = mDataHandler.DataValue_Out(.Item("ColHeaderBorderStyleRight"), SqlDbType.Int)

                mColHeaderPaddingBottom = mDataHandler.DataValue_Out(.Item("ColHeaderPaddingBottom"), SqlDbType.Int)
                mColHeaderPaddingTop = mDataHandler.DataValue_Out(.Item("ColHeaderPaddingTop"), SqlDbType.Int)
                mColHeaderPaddingLeft = mDataHandler.DataValue_Out(.Item("ColHeaderPaddingLeft"), SqlDbType.Int)
                mColHeaderPaddingRight = mDataHandler.DataValue_Out(.Item("ColHeaderPaddingRight"), SqlDbType.Int)
                '============== [End]


                'Footer 
                '============== [Begin]
                mColFooterBackColor = mDataHandler.DataValue_Out(.Item("ColFooterBackColor"), SqlDbType.VarChar)
                mColFooterForeColor = mDataHandler.DataValue_Out(.Item("ColFooterForeColor"), SqlDbType.VarChar)
                mColFooterFont = mDataHandler.DataValue_Out(.Item("ColFooterFont"), SqlDbType.VarChar)
                mColFooterFontIsItalic = mDataHandler.DataValue_Out(.Item("ColFooterFontIsItalic"), SqlDbType.Bit)
                mColFooterFontIsBold = mDataHandler.DataValue_Out(.Item("ColFooterFontIsBold"), SqlDbType.Bit)
                mColFooterFontIsUnderline = mDataHandler.DataValue_Out(.Item("ColFooterFontIsUnderline"), SqlDbType.Bit)
                mColFooterFontSize = mDataHandler.DataValue_Out(.Item("ColFooterFontSize"), SqlDbType.Int)
                mColFooterFontIsStrikeOut = mDataHandler.DataValue_Out(.Item("ColFooterFontIsStrikeOut"), SqlDbType.Int)
                mColFooterFontIsOverLine = mDataHandler.DataValue_Out(.Item("ColFooterFontIsOverLine"), SqlDbType.Int)

                mColFooterBorderColor = mDataHandler.DataValue_Out(.Item("ColFooterBorderColor"), SqlDbType.VarChar)
                mColFooterBorderColorTop = mDataHandler.DataValue_Out(.Item("ColFooterBorderColorTop"), SqlDbType.VarChar)
                mColFooterBorderColorBottom = mDataHandler.DataValue_Out(.Item("ColFooterBorderColorBottom"), SqlDbType.VarChar)
                mColFooterBorderColorLeft = mDataHandler.DataValue_Out(.Item("ColFooterBorderColorLeft"), SqlDbType.VarChar)
                mColFooterBorderColorRight = mDataHandler.DataValue_Out(.Item("ColFooterBorderColorRight"), SqlDbType.VarChar)


                mColFooterBorderWidth = mDataHandler.DataValue_Out(.Item("ColFooterBorderWidth"), SqlDbType.Int)
                mColFooterBorderWidthTop = mDataHandler.DataValue_Out(.Item("ColFooterBorderWidthTop"), SqlDbType.Int)
                mColFooterBorderWidthBottom = mDataHandler.DataValue_Out(.Item("ColFooterBorderWidthBottom"), SqlDbType.Int)
                mColFooterBorderWidthLeft = mDataHandler.DataValue_Out(.Item("ColFooterBorderWidthLeft"), SqlDbType.Int)
                mColFooterBorderWidthRight = mDataHandler.DataValue_Out(.Item("ColFooterBorderWidthRight"), SqlDbType.Int)


                mColFooterBorderStyle = mDataHandler.DataValue_Out(.Item("ColFooterBorderStyle"), SqlDbType.Int)
                mColFooterBorderStyleTop = mDataHandler.DataValue_Out(.Item("ColFooterBorderStyleTop"), SqlDbType.Int)
                mColFooterBorderStyleBottom = mDataHandler.DataValue_Out(.Item("ColFooterBorderStyleBottom"), SqlDbType.Int)
                mColFooterBorderStyleLeft = mDataHandler.DataValue_Out(.Item("ColFooterBorderStyleLeft"), SqlDbType.Int)
                mColFooterBorderStyleRight = mDataHandler.DataValue_Out(.Item("ColFooterBorderStyleRight"), SqlDbType.Int)

                mColFooterPaddingBottom = mDataHandler.DataValue_Out(.Item("ColFooterPaddingBottom"), SqlDbType.Int)
                mColFooterPaddingTop = mDataHandler.DataValue_Out(.Item("ColFooterPaddingTop"), SqlDbType.Int)
                mColFooterPaddingLeft = mDataHandler.DataValue_Out(.Item("ColFooterPaddingLeft"), SqlDbType.Int)
                mColFooterPaddingRight = mDataHandler.DataValue_Out(.Item("ColFooterPaddingRight"), SqlDbType.Int)

                ColumnFooterValignment = mDataHandler.DataValue_Out(.Item("ColumnFooterValignment"), SqlDbType.Int)
                ColumnFooterHalignment = mDataHandler.DataValue_Out(.Item("ColumnFooterHalignment"), SqlDbType.Int)


                Status = mDataHandler.DataValue_Out(.Item("Status"), SqlDbType.Bit)

                ColFooterSummaryType = mDataHandler.DataValue_Out(.Item("ColFooterSummaryType"), SqlDbType.VarChar)


                ColHeaderSorting = mDataHandler.DataValue_Out(.Item("ColHeaderSorting"), SqlDbType.Int)

                DataType = mDataHandler.DataValue_Out(.Item("DataType"), SqlDbType.VarChar)
                '============== [End]
            End With
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Rank", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRank, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsCalculated", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsCalculated, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GroupByRank", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mGroupByRank, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TopBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mTopBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BottomBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBottomBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LeftBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mLeftBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RightBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRightBorderColor, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TopBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTopBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BottomBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBottomBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LeftBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mLeftBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RightBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRightBorderStyle, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TopBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTopBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BottomBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBottomBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LeftBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mLeftBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RightBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRightBorderWidth, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngHeaderCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngHeaderCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngFooterCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngFooterCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbHeaderCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbHeaderCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbFooterCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngFooterCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColumnWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnHalignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColumnHalignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnValignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColumnValignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnIsHidden", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColumnIsHidden, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnIsWrap", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColumnIsWrap, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnIsOverView", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColumnIsOverView, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnForeColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnForeColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnBackColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnBackColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnFont", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColumnFont, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnFontIsItalic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColumnFontIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnFontIsBold", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColumnFontIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnFontIsUnderLine", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColumnFontIsUnderLine, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnFontSize", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColumnFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TopPadding", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTopPadding, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BottomPadding", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBottomPadding, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LeftPadding", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mLeftPadding, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RightPadding", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRightPadding, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TopMargin", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mTopMargin, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BottomMargin", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mBottomMargin, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LeftMargin", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mLeftMargin, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RightMargin", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRightMargin, SqlDbType.Int)
            'Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefaultValue", SqlDbType.Variant)).Value = mDataHandler.DataValue_In(mDefaultValue, SqlDbType.Variant)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Formula", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFormula, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Format", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFormat, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FooterTotal", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFooterTotal, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsSorted", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mIsSorted, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsGroupBy", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsGroupBy, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnHeadervalignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColumnHeadervalignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnHeaderHalignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColumnHeaderHalignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldLanguage", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mFieldLanguage, SqlDbType.Int)

            '============ [Header][Begin]
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBackColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColHeaderBackColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderForeColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColHeaderForeColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderFont", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColHeaderFont, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderFontSize", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderFontIsBold", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColHeaderFontIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderFontIsItalic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColHeaderFontIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderFontIsUnderline", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColHeaderFontIsUnderline, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderFontIsStrikeOut", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColHeaderFontIsStrikeOut, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderFontIsOverLine", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColHeaderFontIsOverLine, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColHeaderBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderColorTop", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColHeaderBorderColorTop, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderColorBottom", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColHeaderBorderColorBottom, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderColorLeft", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColHeaderBorderColorLeft, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderColorRight", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColHeaderBorderColorRight, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderStyleTop", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderBorderStyleTop, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderStyleBottom", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderBorderStyleBottom, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderStyleRight", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderBorderStyleRight, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderStyleLeft", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderBorderStyleLeft, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderWidthTop", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderBorderWidthTop, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderWidthBottom", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderBorderWidthBottom, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderWidthLeft", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderBorderWidthLeft, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderBorderWidthRight", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderBorderWidthRight, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderPaddingTop", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderPaddingTop, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderPaddingBottom", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderPaddingBottom, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderPaddingLeft", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderPaddingLeft, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderPaddingRight", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderPaddingRight, SqlDbType.Int)
            '============ [Header][End]

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Ucode", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mUcode, SqlDbType.Int)
            '============ [Footer][Begin]
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBackColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColFooterBackColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterForeColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColFooterForeColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterFont", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColFooterFont, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterFontSize", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterFontSize, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterFontIsBold", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColFooterFontIsBold, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterFontIsItalic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColFooterFontIsItalic, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterFontIsUnderline", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColFooterFontIsUnderline, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterFontIsStrikeOut", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColFooterFontIsStrikeOut, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterFontIsOverLine", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mColFooterFontIsOverLine, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColFooterBorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderColorTop", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColFooterBorderColorTop, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderColorBottom", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColFooterBorderColorBottom, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderColorLeft", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColFooterBorderColorLeft, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderColorRight", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColFooterBorderColorRight, SqlDbType.VarChar)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderStyle", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterBorderStyle, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderStyleTop", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterBorderStyleTop, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderStyleBottom", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterBorderStyleBottom, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderStyleRight", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterBorderStyleRight, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderStyleLeft", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterBorderStyleLeft, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderWidth", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterBorderWidth, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderWidthTop", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterBorderWidthTop, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderWidthBottom", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterBorderWidthBottom, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderWidthLeft", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterBorderWidthLeft, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterBorderWidthRight", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterBorderWidthRight, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterPaddingTop", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterPaddingTop, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterPaddingBottom", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterPaddingBottom, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterPaddingLeft", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterPaddingLeft, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterPaddingRight", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColFooterPaddingRight, SqlDbType.Int)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnFooterValignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColumnFooterValignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColumnFooterHalignment", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColumnFooterHalignment, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Status", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mStatus, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColFooterSummaryType", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mColFooterSummaryType, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ColHeaderSorting", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mColHeaderSorting, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DataType", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDataType, SqlDbType.VarChar)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
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
