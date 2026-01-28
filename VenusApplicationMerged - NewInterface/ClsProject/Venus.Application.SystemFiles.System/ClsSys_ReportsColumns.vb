'Project           : Venus V.
'Module            : (Account Module)
'Date Created      : 11-11-2007
'Developer         : [MAE]Mah Abdel-aziz
'Description       : 1-Implement Data Acess Layer of sys_ReportColumns table 
'                    2-Allow searching
'                    3-Get list with all codes
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                    5-Implement functions first(),last(),next() and previous() to allow navigation between 
'                       records
'==========================================================================================================
Imports System.Data
Imports System.Drawing
Imports System.Reflection
Imports System.Collections.Generic
Imports Drawing.Text
Imports System
Imports System.Drawing.Text

Public Class Clssys_ReportsColumns
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Initialize insert ,update and delete commands
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  11-11-2007
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_ReportColumns "
        mInsertParameter = " ReportID,ObjectName,cs_Font,cs_BackColor,cs_BackgroundImageFile,cs_BorderColor," & _
        "cs_BorderBottomColor,cs_BorderLeftColor,cs_BorderRightColor,cs_BorderTopColor,cs_BorderBottomStyle," & _
        "cs_BorderLeftStyle,cs_BorderRightStyle,cs_BorderTopStyle,cs_BorderBottomWidth,cs_BorderLeftWidth," & _
        "cs_BorderRightWidth,cs_BorderTopWidth,cs_BorderStyle,cs_BorderWidth,cs_CssClass,cs_ForeColor," & _
        "cs_Width,cs_Height,cs_HorizontalAlignment,cs_VerticalAlignment,cs_BottomMargins,cs_LeftMargins," & _
        "cs_RightMargins,cs_TopMargins,cs_BottomPadding,cs_LeftPadding,cs_RightPadding,cs_TopPadding," & _
        "cs_Textoverflow,cs_Wrap,cs_ChangLinksColor,SortIndicator,Width,AllowGroupBy,AllowNull,AllowResize" & _
        ",AllowRowFiltering,AllowUpdate,[Case],CaseButtonDisplay,CellMultiline,Format,GatherFilterDate,Hidden," & _
        "MergeCells,Type,BaseColumnName,DataType,FieldLen,IsBound,[Key],NullText,EngHeaderCaption,EngFooterCaption," & _
        "ArbHeaderCaption,ArbFooterCaption,IsArabic,Remarks,RegUserID,RegComputerID "

        mInsertParameterValues = " @ReportID,@ObjectName,@cs_Font,@cs_BackColor,@cs_BackgroundImageFile,@cs_BorderColor," & _
        "@cs_BorderBottomColor,@cs_BorderLeftColor,@cs_BorderRightColor,@cs_BorderTopColor," & _
        "@cs_BorderBottomStyle,@cs_BorderLeftStyle,@cs_BorderRightStyle,@cs_BorderTopStyle," & _
        "@cs_BorderBottomWidth,@cs_BorderLeftWidth,@cs_BorderRightWidth,@cs_BorderTopWidth," & _
        "@cs_BorderStyle,@cs_BorderWidth,@cs_CssClass,@cs_ForeColor,@cs_Width,@cs_Height," & _
        "@cs_HorizontalAlignment,@cs_VerticalAlignment,@cs_BottomMargins,@cs_LeftMargins," & _
        "@cs_RightMargins,@cs_TopMargins,@cs_BottomPadding,@cs_LeftPadding,@cs_RightPadding," & _
        "@cs_TopPadding,@cs_Textoverflow,@cs_Wrap,@cs_ChangLinksColor,@SortIndicator,@Width," & _
        "@AllowGroupBy,@AllowNull,@AllowResize,@AllowRowFiltering,@AllowUpdate,@Case," & _
        "@CaseButtonDisplay,@CellMultiline,@Format,@GatherFilterDate,@Hidden,@MergeCells," & _
        "@Type,@BaseColumnName,@DataType,@FieldLen,@IsBound,@Key,@NullText,@EngHeaderCaption," & _
        "@EngFooterCaption,@ArbHeaderCaption,@ArbFooterCaption,@IsArabic,@Remarks,@RegUserID,@RegComputerID "

        mUpdateParameter = "ReportID=@ReportID,ObjectName=@ObjectName,cs_Font=@cs_Font,cs_BackColor=@cs_BackColor," & _
        "cs_BackgroundImageFile=@cs_BackgroundImageFile,cs_BorderColor=@cs_BorderColor," & _
        "cs_BorderBottomColor=@cs_BorderBottomColor,cs_BorderLeftColor=@cs_BorderLeftColor" & _
        ",cs_BorderRightColor=@cs_BorderRightColor,cs_BorderTopColor=@cs_BorderTopColor," & _
        "cs_BorderBottomStyle=@cs_BorderBottomStyle,cs_BorderLeftStyle=@cs_BorderLeftStyle" & _
        ",cs_BorderRightStyle=@cs_BorderRightStyle,cs_BorderTopStyle=@cs_BorderTopStyle," & _
        "cs_BorderBottomWidth=@cs_BorderBottomWidth,cs_BorderLeftWidth=@cs_BorderLeftWidth" & _
        ",cs_BorderRightWidth=@cs_BorderRightWidth,cs_BorderTopWidth=@cs_BorderTopWidth," & _
        "cs_BorderStyle=@cs_BorderStyle,cs_BorderWidth=@cs_BorderWidth,cs_CssClass=@cs_CssClass" & _
        ",cs_ForeColor=@cs_ForeColor,cs_Width=@cs_Width,cs_Height=@cs_Height," & _
        "cs_HorizontalAlignment=@cs_HorizontalAlignment,cs_VerticalAlignment=@cs_VerticalAlignment" & _
        ",cs_BottomMargins=@cs_BottomMargins,cs_LeftMargins=@cs_LeftMargins,cs_RightMargins=@cs_RightMargins" & _
        ",cs_TopMargins=@cs_TopMargins,cs_BottomPadding=@cs_BottomPadding,cs_LeftPadding=@cs_LeftPadding" & _
        ",cs_RightPadding=@cs_RightPadding,cs_TopPadding=@cs_TopPadding,cs_Textoverflow=@cs_Textoverflow" & _
        ",cs_Wrap=@cs_Wrap,cs_ChangLinksColor=@cs_ChangLinksColor,SortIndicator=@SortIndicator,Width=@Width" & _
        ",AllowGroupBy=@AllowGroupBy,AllowNull=@AllowNull,AllowResize=@AllowResize," & _
        "AllowRowFiltering=@AllowRowFiltering,AllowUpdate=@AllowUpdate,[Case]=@Case," & _
        "CaseButtonDisplay=@CaseButtonDisplay,CellMultiline=@CellMultiline,Format=@Format," & _
        "GatherFilterDate=@GatherFilterDate,Hidden=@Hidden,MergeCells=@MergeCells," & _
        "Type=@Type,BaseColumnName=@BaseColumnName,DataType=@DataType,FieldLen=@FieldLen,IsBound=@IsBound," & _
        "[Key]=@Key,NullText=@NullText,EngHeaderCaption=@EngHeaderCaption,EngFooterCaption=@EngFooterCaption" & _
        ",ArbHeaderCaption=@ArbHeaderCaption,ArbFooterCaption=@ArbFooterCaption,Remarks=@Remarks," & _
        "IsArabic=@IsArabic,RegUserID=@RegUserID,RegComputerID=@RegComputerID "

        mSelectCommand = CONFIG_DATEFORMAT & " Select sys_ReportColumns.ID,sys_ReportColumns.ReportID,sys_ReportColumns.ObjectName,sys_ReportColumns.cs_Font," & _
        "sys_ReportColumns.cs_BackColor,sys_ReportColumns.cs_BackgroundImageFile," & _
        "sys_ReportColumns.cs_BorderColor,sys_ReportColumns.cs_BorderBottomColor," & _
        "sys_ReportColumns.cs_BorderLeftColor,sys_ReportColumns.cs_BorderRightColor," & _
        "sys_ReportColumns.cs_BorderTopColor,sys_ReportColumns.cs_BorderBottomStyle," & _
        "sys_ReportColumns.cs_BorderLeftStyle,sys_ReportColumns.cs_BorderRightStyle," & _
        "sys_ReportColumns.cs_BorderTopStyle,sys_ReportColumns.cs_BorderBottomWidth," & _
        "sys_ReportColumns.cs_BorderLeftWidth,sys_ReportColumns.cs_BorderRightWidth," & _
        "sys_ReportColumns.cs_BorderTopWidth,sys_ReportColumns.cs_BorderStyle," & _
        "sys_ReportColumns.cs_BorderWidth,sys_ReportColumns.cs_CssClass," & _
        "sys_ReportColumns.cs_ForeColor,sys_ReportColumns.cs_Width,sys_ReportColumns.cs_Height" & _
        ",sys_ReportColumns.cs_HorizontalAlignment,sys_ReportColumns.cs_VerticalAlignment," & _
        "sys_ReportColumns.cs_BottomMargins,sys_ReportColumns.cs_LeftMargins," & _
        "sys_ReportColumns.cs_RightMargins,sys_ReportColumns.cs_TopMargins," & _
        "sys_ReportColumns.cs_BottomPadding,sys_ReportColumns.cs_LeftPadding," & _
        "sys_ReportColumns.cs_RightPadding,sys_ReportColumns.cs_TopPadding," & _
        "sys_ReportColumns.cs_Textoverflow,sys_ReportColumns.cs_Wrap," & _
        "sys_ReportColumns.cs_ChangLinksColor,sys_ReportColumns.SortIndicator," & _
        "sys_ReportColumns.Width,sys_ReportColumns.AllowGroupBy,sys_ReportColumns.AllowNull" & _
        ",sys_ReportColumns.AllowResize,sys_ReportColumns.AllowRowFiltering," & _
        "sys_ReportColumns.AllowUpdate,sys_ReportColumns.[Case],sys_ReportColumns.CaseButtonDisplay" & _
        ",sys_ReportColumns.CellMultiline,sys_ReportColumns.Format,sys_ReportColumns.GatherFilterDate" & _
        ",sys_ReportColumns.Hidden,sys_ReportColumns.MergeCells,sys_ReportColumns.Type,sys_ReportColumns.BaseColumnName," & _
        "sys_ReportColumns.DataType,sys_ReportColumns.FieldLen,sys_ReportColumns.IsBound," & _
        "sys_ReportColumns.[Key],sys_ReportColumns.NullText,sys_ReportColumns.EngHeaderCaption," & _
        "sys_ReportColumns.EngFooterCaption,sys_ReportColumns.ArbHeaderCaption," & _
        "sys_ReportColumns.ArbFooterCaption,sys_ReportColumns.IsArabic,sys_ReportColumns.Remarks,sys_ReportColumns.RegUserID" & _
        ",sys_ReportColumns.RegComputerID,sys_ReportColumns.RegDate,sys_ReportColumns.CancelDate" & _
        " From  " & mTable

        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Object

    Private mReportID As Integer
    Private mObjectName As String
    Private mcs_Font As String
    Private mcs_BackColor As String
    Private mcs_BackgroundImageFile As String
    Private mcs_BorderColor As String
    Private mcs_BorderBottomColor As String
    Private mcs_BorderLeftColor As String
    Private mcs_BorderRightColor As String
    Private mcs_BorderTopColor As String
    Private mcs_BorderBottomStyle As Int16
    Private mcs_BorderLeftStyle As Int16
    Private mcs_BorderRightStyle As Int16
    Private mcs_BorderTopStyle As Int16
    Private mcs_BorderBottomWidth As Single
    Private mcs_BorderLeftWidth As Single
    Private mcs_BorderRightWidth As Single
    Private mcs_BorderTopWidth As Single
    Private mcs_BorderStyle As Int16
    Private mcs_BorderWidth As Int16
    Private mcs_CssClass As String
    Private mcs_ForeColor As String
    Private mcs_Width As Single
    Private mcs_Height As Single
    Private mcs_HorizontalAlignment As Int16
    Private mcs_VerticalAlignment As Int16
    Private mcs_BottomMargins As Single
    Private mcs_LeftMargins As Single
    Private mcs_RightMargins As Single
    Private mcs_TopMargins As Single
    Private mcs_BottomPadding As Single
    Private mcs_LeftPadding As Single
    Private mcs_RightPadding As Single
    Private mcs_TopPadding As Single
    Private mcs_Textoverflow As Boolean
    Private mcs_Wrap As Boolean
    Private mcs_ChangLinksColor As Boolean
    Private mSortIndicator As Int16
    Private mWidth As Single
    Private mAllowGroupBy As Boolean
    Private mAllowNull As Boolean
    Private mAllowResize As Boolean
    Private mAllowRowFiltering As Boolean
    Private mAllowUpdate As Int16
    Private mCase As Int16
    Private mCaseButtonDisplay As Int16
    Private mCellMultiline As Boolean
    Private mFormat As String
    Private mGatherFilterDate As Boolean
    Private mHidden As Boolean
    Private mMergeCells As Boolean
    Private mType As Int16
    Private mBaseColumnName As String
    Private mDataType As Int16
    Private mFieldLen As Single
    Private mIsBound As Boolean
    Private mKey As String
    Private mNullText As String
    Private mEngHeaderCaption As String
    Private mEngFooterCaption As String
    Private mArbHeaderCaption As String
    Private mArbFooterCaption As String
    Private mIsArabic As Boolean

    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
#End Region
#Region "Public property"
    Public Property ID() As Object
        Get
            Return mID
        End Get
        Set(ByVal Value As Object)
            mID = Value
        End Set
    End Property

    Public Property ObjectName() As String
        Get
            Return mObjectName
        End Get
        Set(ByVal value As String)
            mObjectName = value
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
    Public Property cs_Font() As String
        Get
            Return mcs_Font
        End Get
        Set(ByVal value As String)
            mcs_Font = value
        End Set
    End Property

    Public Property cs_BackColor() As String
        Get
            Return mcs_BackColor
        End Get
        Set(ByVal value As String)
            mcs_BackColor = value
        End Set
    End Property
    Public Property cs_BackgroundImageFile() As String
        Get
            Return mcs_BackgroundImageFile
        End Get
        Set(ByVal value As String)
            mcs_BackgroundImageFile = value
        End Set
    End Property
    Public Property cs_BorderColor() As String
        Get
            Return mcs_BorderColor
        End Get
        Set(ByVal value As String)
            mcs_BorderColor = value
        End Set
    End Property
    Public Property cs_BorderBottomColor() As String
        Get
            Return mcs_BorderBottomColor
        End Get
        Set(ByVal value As String)
            mcs_BorderBottomColor = value
        End Set
    End Property
    Public Property cs_BorderLeftColor() As String
        Get
            Return mcs_BorderLeftColor
        End Get
        Set(ByVal value As String)
            mcs_BorderLeftColor = value
        End Set
    End Property
    Public Property cs_BorderRightColor() As String
        Get
            Return mcs_BorderRightColor
        End Get
        Set(ByVal value As String)
            mcs_BorderRightColor = value
        End Set
    End Property
    Public Property cs_BorderTopColor() As String
        Get
            Return mcs_BorderTopColor
        End Get
        Set(ByVal value As String)
            mcs_BorderTopColor = value
        End Set
    End Property
    Public Property cs_BorderBottomStyle() As Int16
        Get
            Return mcs_BorderBottomStyle
        End Get
        Set(ByVal value As Int16)
            mcs_BorderBottomStyle = value
        End Set
    End Property
    Public Property cs_BorderLeftStyle() As Int16
        Get
            Return mcs_BorderLeftStyle
        End Get
        Set(ByVal value As Int16)
            mcs_BorderLeftStyle = value
        End Set
    End Property
    Public Property cs_BorderRightStyle() As Int16
        Get
            Return mcs_BorderRightStyle
        End Get
        Set(ByVal value As Int16)
            mcs_BorderRightStyle = value
        End Set
    End Property
    Public Property cs_BorderTopStyle() As Int16
        Get
            Return mcs_BorderTopStyle
        End Get
        Set(ByVal value As Int16)
            mcs_BorderTopStyle = value
        End Set
    End Property
    Public Property cs_BorderBottomWidth() As Single
        Get
            Return mcs_BorderBottomWidth
        End Get
        Set(ByVal value As Single)
            mcs_BorderBottomWidth = value
        End Set
    End Property
    Public Property cs_BorderLeftWidth() As Single
        Get
            Return mcs_BorderLeftWidth
        End Get
        Set(ByVal value As Single)
            mcs_BorderLeftWidth = value
        End Set
    End Property
    Public Property cs_BorderRightWidth() As Single
        Get
            Return mcs_BorderRightWidth
        End Get
        Set(ByVal value As Single)
            mcs_BorderRightWidth = value
        End Set
    End Property
    Public Property cs_BorderTopWidth() As Single
        Get
            Return mcs_BorderTopWidth
        End Get
        Set(ByVal value As Single)
            mcs_BorderTopWidth = value
        End Set
    End Property
    Public Property cs_BorderStyle() As Int16
        Get
            Return mcs_BorderStyle
        End Get
        Set(ByVal value As Int16)
            mcs_BorderStyle = value
        End Set
    End Property
    Public Property cs_BorderWidth() As Int16
        Get
            Return mcs_BorderWidth
        End Get
        Set(ByVal value As Int16)
            mcs_BorderWidth = value
        End Set
    End Property
    Public Property cs_CssClass() As String
        Get
            Return mcs_CssClass
        End Get
        Set(ByVal value As String)
            mcs_CssClass = value
        End Set
    End Property

    Public Property cs_ForeColor() As String
        Get
            Return mcs_ForeColor
        End Get
        Set(ByVal value As String)
            mcs_ForeColor = value
        End Set
    End Property
    Public Property cs_Width() As Single
        Get
            Return mcs_Width
        End Get
        Set(ByVal value As Single)
            mcs_Width = value
        End Set
    End Property
    Public Property cs_Height() As Single
        Get
            Return mcs_Height
        End Get
        Set(ByVal value As Single)
            mcs_Height = value
        End Set
    End Property
    Public Property cs_HorizontalAlignment() As Int16
        Get
            Return mcs_HorizontalAlignment
        End Get
        Set(ByVal value As Int16)
            mcs_HorizontalAlignment = value
        End Set
    End Property
    Public Property cs_VerticalAlignment() As Int16
        Get
            Return mcs_VerticalAlignment
        End Get
        Set(ByVal value As Int16)
            mcs_VerticalAlignment = value
        End Set
    End Property
    Public Property cs_BottomMargins() As Single
        Get
            Return mcs_BottomMargins
        End Get
        Set(ByVal value As Single)
            mcs_BottomMargins = value
        End Set
    End Property

    Public Property cs_LeftMargins() As Single
        Get
            Return mcs_LeftMargins
        End Get
        Set(ByVal value As Single)
            mcs_LeftMargins = value
        End Set
    End Property
    Public Property cs_RightMargins() As Single
        Get
            Return mcs_RightMargins
        End Get
        Set(ByVal value As Single)
            mcs_RightMargins = value
        End Set
    End Property
    Public Property cs_TopMargins() As Single
        Get
            Return mcs_TopMargins
        End Get
        Set(ByVal value As Single)
            mcs_TopMargins = value
        End Set
    End Property
    Public Property cs_BottomPadding() As Single
        Get
            Return mcs_BottomPadding
        End Get
        Set(ByVal value As Single)
            mcs_BottomPadding = value
        End Set
    End Property

    Public Property cs_LeftPadding() As Single
        Get
            Return mcs_LeftPadding
        End Get
        Set(ByVal value As Single)
            mcs_LeftPadding = value
        End Set
    End Property
    Public Property cs_RightPadding() As Single
        Get
            Return mcs_RightPadding
        End Get
        Set(ByVal value As Single)
            mcs_RightPadding = value
        End Set
    End Property
    Public Property cs_TopPadding() As Single
        Get
            Return mcs_TopPadding
        End Get
        Set(ByVal value As Single)
            mcs_TopPadding = value
        End Set
    End Property
    Public Property cs_Textoverflow() As Boolean
        Get
            Return mcs_Textoverflow
        End Get
        Set(ByVal value As Boolean)
            mcs_Textoverflow = value
        End Set
    End Property

    Public Property cs_Wrap() As Boolean
        Get
            Return mcs_Wrap
        End Get
        Set(ByVal value As Boolean)
            mcs_Wrap = value
        End Set
    End Property
    Public Property cs_ChangLinksColor() As Boolean
        Get
            Return mcs_ChangLinksColor
        End Get
        Set(ByVal value As Boolean)
            mcs_ChangLinksColor = value
        End Set
    End Property
    Public Property SortIndicator() As Int16
        Get
            Return mSortIndicator
        End Get
        Set(ByVal value As Int16)
            mSortIndicator = value
        End Set
    End Property
    Public Property Width() As Single
        Get
            Return mWidth
        End Get
        Set(ByVal value As Single)
            mWidth = value
        End Set
    End Property
    Public Property AllowGroupBy() As Boolean
        Get
            Return mAllowGroupBy
        End Get
        Set(ByVal value As Boolean)
            mAllowGroupBy = value
        End Set
    End Property
    Public Property AllowNull() As Boolean
        Get
            Return mAllowNull
        End Get
        Set(ByVal value As Boolean)
            mAllowNull = value
        End Set
    End Property
    Public Property AllowResize() As Boolean
        Get
            Return mAllowResize
        End Get
        Set(ByVal value As Boolean)
            mAllowResize = value
        End Set
    End Property
    Public Property AllowRowFiltering() As Boolean
        Get
            Return mAllowResize
        End Get
        Set(ByVal value As Boolean)
            mAllowResize = value
        End Set
    End Property
    Public Property AllowUpdate() As Int16
        Get
            Return mAllowUpdate
        End Get
        Set(ByVal value As Int16)
            mAllowUpdate = value
        End Set
    End Property
    Public Property RepColumnCase() As Int16
        Get
            Return mCase
        End Get
        Set(ByVal value As Int16)
            mCase = value
        End Set
    End Property

    Public Property CaseButtonDisplay() As Int16
        Get
            Return mCaseButtonDisplay
        End Get
        Set(ByVal value As Int16)
            mCaseButtonDisplay = value
        End Set
    End Property
    Public Property CellMultiline() As Boolean
        Get
            Return mCellMultiline
        End Get
        Set(ByVal value As Boolean)
            mCellMultiline = value
        End Set
    End Property
    Public Property Format() As String
        Get
            Return mFormat
        End Get
        Set(ByVal value As String)
            mFormat = value
        End Set
    End Property
    Public Property GatherFilterDate() As Boolean
        Get
            Return mGatherFilterDate
        End Get
        Set(ByVal value As Boolean)
            mGatherFilterDate = value
        End Set
    End Property
    Public Property Hidden() As Boolean
        Get
            Return mHidden
        End Get
        Set(ByVal value As Boolean)
            mHidden = value
        End Set
    End Property
    Public Property Type() As Int16
        Get
            Return mType
        End Get
        Set(ByVal value As Int16)
            mType = value
        End Set
    End Property
    Public Property MergeCells() As Boolean
        Get
            Return mMergeCells
        End Get
        Set(ByVal value As Boolean)
            mMergeCells = value
        End Set
    End Property
    Public Property BaseColumnName() As String
        Get
            Return mBaseColumnName
        End Get
        Set(ByVal value As String)
            mBaseColumnName = value
        End Set
    End Property
    Public Property DataType() As Int16
        Get
            Return mDataType
        End Get
        Set(ByVal value As Int16)
            mDataType = value
        End Set
    End Property
    Public Property FieldLen() As Single
        Get
            Return mFieldLen
        End Get
        Set(ByVal value As Single)
            mFieldLen = value
        End Set
    End Property
    Public Property IsBound() As Boolean
        Get
            Return mIsBound
        End Get
        Set(ByVal value As Boolean)
            mIsBound = value
        End Set
    End Property
    Public Property Key() As String
        Get
            Return mKey
        End Get
        Set(ByVal value As String)
            mKey = value
        End Set
    End Property
    Public Property NullText() As String
        Get
            Return mNullText
        End Get
        Set(ByVal value As String)
            mNullText = value
        End Set
    End Property
    Public Property EngHeaderCaption() As String
        Get
            Return mEngHeaderCaption
        End Get
        Set(ByVal value As String)
            mEngHeaderCaption = value
        End Set
    End Property
    Public Property EngFooterCaption() As String
        Get
            Return mEngFooterCaption
        End Get
        Set(ByVal value As String)
            mEngFooterCaption = value
        End Set
    End Property
    Public Property ArbHeaderCaption() As String
        Get
            Return mArbHeaderCaption
        End Get
        Set(ByVal value As String)
            mArbHeaderCaption = value
        End Set
    End Property
    Public Property ArbFooterCaption() As String
        Get
            Return mArbFooterCaption
        End Get
        Set(ByVal value As String)
            mArbFooterCaption = value
        End Set
    End Property
    Public Property IsArabic() As Boolean
        Get
            Return mIsArabic
        End Get
        Set(ByVal value As Boolean)
            mIsArabic = value
        End Set
    End Property

    Public Property Remarks() As String
        Get
            Return mRemarks
        End Get
        Set(ByVal value As String)
            mRemarks = value
        End Set
    End Property
    Public Property RegUserID() As Object
        Get
            Return mRegUserID
        End Get
        Set(ByVal Value As Object)
            mRegUserID = Value
        End Set
    End Property
    Public Property RegComputerID() As Object
        Get
            Return mRegComputerID
        End Get
        Set(ByVal Value As Object)
            mRegComputerID = Value
        End Set
    End Property
    Public Property RegDate() As Object
        Get
            Return mRegDate
        End Get
        Set(ByVal Value As Object)
            mRegDate = Value
        End Set
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
#Region "Public Function"

    '========================================================================
    'ProcedureName  :  GetList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill Value List with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  11-11-2007
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
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngHeaderCaption"), SqlDbType.VarChar)
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngHeaderCaption/ArbHeaderCaption")), SqlDbType.VarChar)
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbHeaderCaption/EngHeaderCaption")), SqlDbType.VarChar)
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
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  11-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :DropDownList     :used to fill it with English name
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
                'Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/[ برجاء الاختيار ]")

                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                'Item.Text = mDataHandler.DataValue(ObjDataRow("EngHeaderCaption"), SqlDbType.VarChar)
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngHeaderCaption/ArbHeaderCaption")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbHeaderCaption/EngHeaderCaption")), SqlDbType.VarChar)
                End If
                Item.Value = ObjDataRow("ID")
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
    'ProcedureName  :  GetColorsDropDownList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with Colors List
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlColors             :DropDownList     :used to fill it with English name
    '========================================================================
    Public Function GetColorsDropDownList(ByVal DdlColors As Global.System.Web.UI.WebControls.DropDownList) As Boolean
        Try
            DdlColors.DataSource = finalColorList()
            DdlColors.DataBind()
            MainpulateColor(DdlColors)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Find 
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :  11-11-2007
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
            'StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(sys_ReportColumns.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  And IsNull(dbo.hrs_GetRecordViewStatus(sys_ReportColumns.ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
            mErrorHandler.RecordExceptions_DataBase(StrSelectCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '========================================================================
    'ProcedureName  :  Save
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Save new record
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  11-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
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
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  update row that match with critera
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean   
    'Date Created   :  11-11-2007
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
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Delete record that match critera
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :  DataOcean  
    'Date Created   :  11-11-2007
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
    'ProcedureName  :  DeleteAll
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Delete record that match critera
    '                  and return true if operation done otherwise report errors in ErrorPage                    
    'Developer      :  DataOcean  
    'Date Created   :  11-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera in select statment like 'ID=2'
    '========================================================================
    Public Function DeleteAll(ByVal Filter As String) As Boolean
        Dim StrDeleteCommand As String
        Try
            StrDeleteCommand = "Delete from " & mTable & " Where " & Filter
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
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Clear all private attributes in class
    'Developer      :  DataOcean  
    'Date Created   :  11-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0

            mReportID = 0
            mObjectName = String.Empty
            mcs_Font = Nothing
            mcs_BackColor = Nothing
            mcs_BackgroundImageFile = Nothing
            mcs_BorderColor = Nothing
            mcs_BorderBottomColor = Nothing
            mcs_BorderLeftColor = Nothing
            mcs_BorderRightColor = Nothing
            mcs_BorderTopColor = Nothing
            mcs_BorderBottomStyle = Nothing
            mcs_BorderLeftStyle = Nothing
            mcs_BorderRightStyle = Nothing
            mcs_BorderTopStyle = Nothing
            mcs_BorderBottomWidth = Nothing
            mcs_BorderLeftWidth = Nothing
            mcs_BorderRightWidth = Nothing
            mcs_BorderTopWidth = Nothing
            mcs_BorderStyle = Nothing
            mcs_BorderWidth = Nothing
            mcs_CssClass = Nothing
            mcs_ForeColor = Nothing
            mcs_Width = Nothing
            mcs_Height = Nothing
            mcs_HorizontalAlignment = Nothing
            mcs_VerticalAlignment = Nothing
            mcs_BottomMargins = Nothing
            mcs_LeftMargins = Nothing
            mcs_RightMargins = Nothing
            mcs_TopMargins = Nothing
            mcs_BottomPadding = Nothing
            mcs_LeftPadding = Nothing
            mcs_RightPadding = Nothing
            mcs_TopPadding = Nothing
            mcs_Textoverflow = Nothing
            mcs_Wrap = Nothing
            mcs_ChangLinksColor = Nothing
            mSortIndicator = Nothing
            mWidth = Nothing
            mAllowGroupBy = Nothing
            mAllowNull = Nothing
            mAllowResize = Nothing
            mAllowRowFiltering = Nothing
            mAllowUpdate = Nothing
            mCase = Nothing
            mCaseButtonDisplay = Nothing
            mCellMultiline = Nothing
            mFormat = Nothing
            mGatherFilterDate = Nothing
            mHidden = Nothing
            mMergeCells = Nothing
            mType = Nothing
            mBaseColumnName = Nothing
            mDataType = Nothing
            mFieldLen = Nothing
            mIsBound = Nothing
            mKey = Nothing
            mNullText = Nothing
            mEngHeaderCaption = Nothing
            mEngFooterCaption = Nothing
            mArbHeaderCaption = Nothing
            mArbFooterCaption = Nothing


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

#Region "FillIn (List/ddpList)"

    '========================================================================
    'ProcedureName  :  GetStyleTypesList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill Value List with Style types And return true if operation done otherwise report errors in ErrorPage
    'Developer      : 
    'Date Created   :  15-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '========================================================================

    Public Function GetStyleTypesList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim StyleTypesArr() As String = {"Not Set", "None", "Dotted", "Dashed", "Solid", "Double", "Groove", "Ridge", "Inset", "Outset"}
        Dim StrIndex As Integer = 0

        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In StyleTypesArr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = StrIndex
                DdlValues.ValueListItems.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetStyleTypesDropDownList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill DropDownList with Style types And return true if operation done otherwise report errors in ErrorPage
    'Developer      :  
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================

    Public Function GetStyleTypesDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim StyleTypesArr() As String = {"Not Set", "None", "Dotted", "Dashed", "Solid", "Double", "Groove", "Ridge", "Inset", "Outset"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In StyleTypesArr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = StrIndex
                DdlValues.Items.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetHorizontalAlignmentsList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill Value List with 'Horizontal Alignments' types And return true if operation done otherwise report errors in ErrorPage
    'Developer      : 
    'Date Created   :  15-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '========================================================================

    Public Function GetHorizontalAlignmentsList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim HorizontalAlignmentsArr() As String = {"Not Set", "Left", "Center", "Right", "Justified"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In HorizontalAlignmentsArr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = StrIndex
                DdlValues.ValueListItems.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetHorizontalAlignmentsDropDownList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill DropDownList with 'Horizontal Alignments' types And return true if operation done otherwise report errors in ErrorPage
    'Developer      :  
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================

    Public Function GetHorizontalAlignmentsDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim HorizontalAlignmentsArr() As String = {"Not Set", "Left", "Center", "Right", "Justified"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In HorizontalAlignmentsArr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = StrIndex
                DdlValues.Items.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetVerticalAlignmentsList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill Value List with 'Vertical Alignments' types And return true if operation done otherwise report errors in ErrorPage
    'Developer      : 
    'Date Created   :  15-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '========================================================================

    Public Function GetVerticalAlignmentsList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim VerticalAlignmentsArr() As String = {"Not Set", "Top", "Middle", "Bottom"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In VerticalAlignmentsArr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = StrIndex
                DdlValues.ValueListItems.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetVerticalAlignmentsDropDownList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill DropDownList with 'Vertical Alignments' types And return true if operation done otherwise report errors in ErrorPage
    'Developer      :  
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================

    Public Function GetVerticalAlignmentsDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim VerticalAlignmentsArr() As String = {"Not Set", "Top", "Middle", "Bottom"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In VerticalAlignmentsArr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = StrIndex
                DdlValues.Items.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetSortIndicatorList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill Value List with Sort Indicator types And return true if operation done otherwise report errors in ErrorPage
    'Developer      : 
    'Date Created   :  15-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '========================================================================

    Public Function GetSortIndicatorList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim SortIndicatorListArr() As String = {"ASC", "DESC", "None", "Disabled"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In SortIndicatorListArr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = StrIndex
                DdlValues.ValueListItems.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetSortIndicatorDropDownList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill DropDownList with 'Sort Indicator' types And return true if operation done otherwise report errors in ErrorPage
    'Developer      :  
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================

    Public Function GetSortIndicatorDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim SortIndicatorListArr() As String = {"ASC", "DESC", "None", "Disabled"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In SortIndicatorListArr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = StrIndex
                DdlValues.Items.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetAllowUpdateList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill Value List with 'Allow Update' types And return true if operation done otherwise report errors in ErrorPage
    'Developer      : 
    'Date Created   :  15-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '========================================================================

    Public Function GetAllowUpdateList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim AllowUpdateTypesArr() As String = {"Not Set", "Yes", "No", "Row Template Only"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In AllowUpdateTypesArr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = StrIndex
                DdlValues.ValueListItems.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetAllowUpdateDropDownList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill DropDownList with 'Allow Update' types And return true if operation done otherwise report errors in ErrorPage
    'Developer      :  
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================

    Public Function GetAllowUpdateDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim AllowUpdateTypesArr() As String = {"Not Set", "Yes", "No", "Row Template Only"}
        Dim StrIndex As Integer = 0
        Try
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In AllowUpdateTypesArr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = StrIndex
                DdlValues.Items.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetCaseList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill Value List with Case types And return true if operation done otherwise report errors in ErrorPage
    'Developer      : 
    'Date Created   :  15-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '========================================================================

    Public Function GetCaseList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim CaseArr() As String = {"Unchaged", "Lower", "Upper"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In CaseArr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = StrIndex
                DdlValues.ValueListItems.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetCaseDropDownList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill DropDownList with Case types types And return true if operation done otherwise report errors in ErrorPage
    'Developer      :  
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================

    Public Function GetCaseDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim CaseArr() As String = {"Unchaged", "Lower", "Upper"}
        Dim StrIndex As Integer = 0

        Try

            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In CaseArr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = StrIndex
                DdlValues.Items.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetCaseButtonDisplayList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill Value List with Case Button Display types And return true if operation done otherwise report errors in ErrorPage
    'Developer      : 
    'Date Created   :  15-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '========================================================================

    Public Function GetCaseButtonDisplayList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim CaseButtonArr() As String = {"On Mouse Enter", "Always"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In CaseButtonArr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = StrIndex
                DdlValues.ValueListItems.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetCaseButtonDisplayDropDownList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill DropDownList with Case Button Display types And return true if operation done otherwise report errors in ErrorPage
    'Developer      :  
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================

    Public Function GetCaseButtonDisplayDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim CaseButtonArr() As String = {"On Mouse Enter", "Always"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In CaseButtonArr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = StrIndex
                DdlValues.Items.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetTypesList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill Value List with Types And return true if operation done otherwise report errors in ErrorPage
    'Developer      : 
    'Date Created   :  15-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '========================================================================

    Public Function GetTypesList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim TypesArr() As String = {"Not Set", "CheckBox", "DropDownList", "Button", "HyperLink", "Custom"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In TypesArr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = StrIndex
                DdlValues.ValueListItems.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetTypesDropDownList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill DropDownList with Types And return true if operation done otherwise report errors in ErrorPage
    'Developer      :  
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================

    Public Function GetTypesDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim TypesArr() As String = {"Not Set", "CheckBox", "DropDownList", "Button", "HyperLink", "Custom"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In TypesArr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = StrIndex
                DdlValues.Items.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetDataTypeList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill Value List with DataTypes List And return true if operation done otherwise report errors in ErrorPage
    'Developer      : 
    'Date Created   :  15-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '========================================================================

    Public Function GetDataTypeList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim DataTypeArr() As String = {"Boolean", "Byte", "Char", "DateTime", "Decimal", "Double", "Guid", "Int16", "Int32", "Int64", "Object", "SByte", "Single", "String", "UInt16", "UInt32", "UInt64"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In DataTypeArr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = StrIndex
                DdlValues.ValueListItems.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetDataTypeDropDownList
    'Module         :  Hrs
    'Project        :  Venus
    'Description    :  Fill DropDownList with DataTypes List And return true if operation done otherwise report errors in ErrorPage
    'Developer      :  
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================

    Public Function GetDataTypeDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim DataTypeArr() As String = {"Boolean", "Byte", "Char", "DateTime", "Decimal", "Double", "Guid", "Int16", "Int32", "Int64", "Object", "SByte", "Single", "String", "UInt16", "UInt32", "UInt64"}
        Dim StrIndex As Integer = 0
        Try

            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In DataTypeArr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = StrIndex
                DdlValues.Items.Add(Item)
                StrIndex += 1
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetDropDownList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  11-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :DropDownList     :used to fill it with English name
    '========================================================================
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

    Public Function GetFontSizes(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList) As Boolean
        Try

            Dim fonts As New InstalledFontCollection
            For Each family As FontFamily In fonts.Families
            Next
            DdlValues.SelectedValue = "Arial"
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally

        End Try

    End Function
#End Region


#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  11-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------

    '========================================================================
    Public Function FirstRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY ID ASC"
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
    Public Function LastRecord(Optional ByVal Filter As String = "") As Boolean
        Dim StrSelectCommand As String
        Try
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY ID DESC"
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
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY ID ASC"
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
            'StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And  IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 " & IIf(Filter.Trim = "", " ", " AND " & Filter & " ") & " ORDER BY ID DESC"
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

#End Region
#Region "Class Private Function"
    '========================================================================
    'ProcedureName  :  finalColorList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Get All Colors list
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================
    Private Function finalColorList() As List(Of String)
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
    '========================================================================
    'ProcedureName  :  MainpulateColor
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Add background color attribute to every item in color dropdowmlist
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  15-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '========================================================================
    Private Sub MainpulateColor(ByRef ddl As Global.System.Web.UI.WebControls.DropDownList)
        Dim index As Integer
        For index = 0 To ddl.Items.Count - 1
            ddl.Items(index).Attributes.Add("style", "background-color:" & ddl.Items(index).Value)
        Next
        ddl.BackColor = Color.FromName(ddl.SelectedItem.Text)
    End Sub
    '========================================================================
    'ProcedureName  :  GetParameter
    'Module         : (Account Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :DataOcean   
    'Date Created   :11-11-2007
    'Modifacations  :
    'Calls          :
    'From           :Find()
    '               :FirstRecord()
    '               :LastRecord()
    '               :PreviousRecord()
    '               :NextRecord()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Ds             :DataSet     :used its attributes to assign them to private attributes
    '========================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int)

                mReportID = mDataHandler.DataValue_Out(.Item("ReportID"), SqlDbType.Int)
                mObjectName = mDataHandler.DataValue_Out(.Item("ObjectName"), SqlDbType.VarChar)
                mcs_Font = mDataHandler.DataValue_Out(.Item("cs_Font"), SqlDbType.VarChar)
                mcs_BackColor = mDataHandler.DataValue_Out(.Item("cs_BackColor"), SqlDbType.VarChar)
                mcs_BackgroundImageFile = mDataHandler.DataValue_Out(.Item("cs_BackgroundImageFile"), SqlDbType.VarChar)
                mcs_BorderColor = mDataHandler.DataValue_Out(.Item("cs_BorderColor"), SqlDbType.VarChar)
                mcs_BorderBottomColor = mDataHandler.DataValue_Out(.Item("cs_BorderBottomColor"), SqlDbType.VarChar)
                mcs_BorderLeftColor = mDataHandler.DataValue_Out(.Item("cs_BorderLeftColor"), SqlDbType.VarChar)
                mcs_BorderRightColor = mDataHandler.DataValue_Out(.Item("cs_BorderRightColor"), SqlDbType.VarChar)
                mcs_BorderTopColor = mDataHandler.DataValue_Out(.Item("cs_BorderTopColor"), SqlDbType.VarChar)
                mcs_BorderBottomStyle = mDataHandler.DataValue_Out(.Item("cs_BorderBottomStyle"), SqlDbType.TinyInt)
                mcs_BorderLeftStyle = mDataHandler.DataValue_Out(.Item("cs_BorderLeftStyle"), SqlDbType.TinyInt)
                mcs_BorderRightStyle = mDataHandler.DataValue_Out(.Item("cs_BorderRightStyle"), SqlDbType.TinyInt)
                mcs_BorderTopStyle = mDataHandler.DataValue_Out(.Item("cs_BorderTopStyle"), SqlDbType.TinyInt)
                mcs_BorderBottomWidth = mDataHandler.DataValue_Out(.Item("cs_BorderBottomWidth"), SqlDbType.Real)
                mcs_BorderLeftWidth = mDataHandler.DataValue_Out(.Item("cs_BorderLeftWidth"), SqlDbType.Real)
                mcs_BorderRightWidth = mDataHandler.DataValue_Out(.Item("cs_BorderRightWidth"), SqlDbType.Real)
                mcs_BorderTopWidth = mDataHandler.DataValue_Out(.Item("cs_BorderTopWidth"), SqlDbType.Real)
                mcs_BorderStyle = mDataHandler.DataValue_Out(.Item("cs_BorderStyle"), SqlDbType.TinyInt)
                mcs_BorderWidth = mDataHandler.DataValue_Out(.Item("cs_BorderWidth"), SqlDbType.Real)
                mcs_CssClass = mDataHandler.DataValue_Out(.Item("cs_CssClass"), SqlDbType.VarChar)
                mcs_ForeColor = mDataHandler.DataValue_Out(.Item("cs_ForeColor"), SqlDbType.VarChar)
                mcs_Width = mDataHandler.DataValue_Out(.Item("cs_Width"), SqlDbType.Real)
                mcs_Height = mDataHandler.DataValue_Out(.Item("cs_Height"), SqlDbType.Real)
                mcs_HorizontalAlignment = mDataHandler.DataValue_Out(.Item("cs_HorizontalAlignment"), SqlDbType.TinyInt)
                mcs_VerticalAlignment = mDataHandler.DataValue_Out(.Item("cs_VerticalAlignment"), SqlDbType.TinyInt)
                mcs_BottomMargins = mDataHandler.DataValue_Out(.Item("cs_BottomMargins"), SqlDbType.Real)
                mcs_LeftMargins = mDataHandler.DataValue_Out(.Item("cs_LeftMargins"), SqlDbType.Real)
                mcs_RightMargins = mDataHandler.DataValue_Out(.Item("cs_RightMargins"), SqlDbType.Real)
                mcs_TopMargins = mDataHandler.DataValue_Out(.Item("cs_TopMargins"), SqlDbType.Real)
                mcs_BottomPadding = mDataHandler.DataValue_Out(.Item("cs_BottomPadding"), SqlDbType.Real)
                mcs_LeftPadding = mDataHandler.DataValue_Out(.Item("cs_LeftPadding"), SqlDbType.Real)
                mcs_RightPadding = mDataHandler.DataValue_Out(.Item("cs_RightPadding"), SqlDbType.Real)
                mcs_TopPadding = mDataHandler.DataValue_Out(.Item("cs_TopPadding"), SqlDbType.Real)
                mcs_Textoverflow = mDataHandler.DataValue_Out(.Item("cs_Textoverflow"), SqlDbType.Bit)
                mcs_Wrap = mDataHandler.DataValue_Out(.Item("cs_Wrap"), SqlDbType.Bit)
                mcs_ChangLinksColor = mDataHandler.DataValue_Out(.Item("cs_ChangLinksColor"), SqlDbType.Bit)
                mSortIndicator = mDataHandler.DataValue_Out(.Item("SortIndicator"), SqlDbType.TinyInt)
                mWidth = mDataHandler.DataValue_Out(.Item("Width"), SqlDbType.Real)
                mAllowGroupBy = mDataHandler.DataValue_Out(.Item("AllowGroupBy"), SqlDbType.Bit)
                mAllowNull = mDataHandler.DataValue_Out(.Item("AllowNull"), SqlDbType.Bit)
                mAllowResize = mDataHandler.DataValue_Out(.Item("AllowResize"), SqlDbType.Bit)
                mAllowRowFiltering = mDataHandler.DataValue_Out(.Item("AllowRowFiltering"), SqlDbType.Bit)
                mAllowUpdate = mDataHandler.DataValue_Out(.Item("AllowUpdate"), SqlDbType.TinyInt)
                mCase = mDataHandler.DataValue_Out(.Item("Case"), SqlDbType.TinyInt)
                mCaseButtonDisplay = mDataHandler.DataValue_Out(.Item("CaseButtonDisplay"), SqlDbType.TinyInt)
                mCellMultiline = mDataHandler.DataValue_Out(.Item("CellMultiline"), SqlDbType.Bit)
                mFormat = mDataHandler.DataValue_Out(.Item("Format"), SqlDbType.VarChar)
                mGatherFilterDate = mDataHandler.DataValue_Out(.Item("GatherFilterDate"), SqlDbType.Bit)
                mHidden = mDataHandler.DataValue_Out(.Item("Hidden"), SqlDbType.Bit)
                mMergeCells = mDataHandler.DataValue_Out(.Item("MergeCells"), SqlDbType.Bit)
                mType = mDataHandler.DataValue_Out(.Item("Type"), SqlDbType.TinyInt)
                mBaseColumnName = mDataHandler.DataValue_Out(.Item("BaseColumnName"), SqlDbType.VarChar)
                mDataType = mDataHandler.DataValue_Out(.Item("DataType"), SqlDbType.TinyInt)
                mFieldLen = mDataHandler.DataValue_Out(.Item("FieldLen"), SqlDbType.Real)
                mIsBound = mDataHandler.DataValue_Out(.Item("IsBound"), SqlDbType.Bit)
                mKey = mDataHandler.DataValue_Out(.Item("Key"), SqlDbType.VarChar)
                mNullText = mDataHandler.DataValue_Out(.Item("NullText"), SqlDbType.VarChar)
                mEngHeaderCaption = mDataHandler.DataValue_Out(.Item("EngHeaderCaption"), SqlDbType.VarChar)
                mEngFooterCaption = mDataHandler.DataValue_Out(.Item("EngFooterCaption"), SqlDbType.VarChar)
                mArbHeaderCaption = mDataHandler.DataValue_Out(.Item("ArbHeaderCaption"), SqlDbType.VarChar)
                mArbFooterCaption = mDataHandler.DataValue_Out(.Item("ArbFooterCaption"), SqlDbType.VarChar)
                mIsArabic = mDataHandler.DataValue_Out(.Item("IsArabic"), SqlDbType.Bit)

                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int)
                mRegDate = mDataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = mDataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
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
    'Module            : (Account Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sql command  with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :DataOcean   
    'Date Created   :11-11-2007
    'Modifacations  :
    'Calls          :
    'From           :Save()
    '               :Update()
    '               :Delete()
    'To             :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ReportID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReportID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ObjectName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mObjectName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_Font", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mcs_Font, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BackColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mcs_BackColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BackgroundImageFile", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mcs_BackgroundImageFile, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mcs_BorderColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderBottomColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mcs_BorderBottomColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderLeftColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mcs_BorderLeftColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderRightColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mcs_BorderRightColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderTopColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mcs_BorderTopColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderBottomStyle", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mcs_BorderBottomStyle, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderLeftStyle", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mcs_BorderLeftStyle, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderRightStyle", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mcs_BorderRightStyle, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderTopStyle", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mcs_BorderTopStyle, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderBottomWidth", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_BorderBottomWidth, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderLeftWidth", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_BorderLeftWidth, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderRightWidth", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_BorderRightWidth, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderTopWidth", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_BorderTopWidth, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderStyle", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mcs_BorderStyle, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BorderWidth", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_BorderWidth, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_CssClass", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mcs_CssClass, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_ForeColor", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mcs_ForeColor, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_Width", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_Width, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_Height", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_Height, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_HorizontalAlignment", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mcs_HorizontalAlignment, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_VerticalAlignment", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mcs_VerticalAlignment, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BottomMargins", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_BottomMargins, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_LeftMargins", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_LeftMargins, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_RightMargins", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_RightMargins, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_TopMargins", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_TopMargins, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_BottomPadding", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_BottomPadding, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_LeftPadding", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_LeftPadding, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_RightPadding", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_RightPadding, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_TopPadding", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mcs_TopPadding, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_Textoverflow", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mcs_Textoverflow, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_Wrap", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mcs_Wrap, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@cs_ChangLinksColor", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mcs_ChangLinksColor, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SortIndicator", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mSortIndicator, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Width", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mWidth, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AllowGroupBy", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mAllowGroupBy, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AllowNull", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mAllowNull, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AllowResize", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mAllowResize, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AllowRowFiltering", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mAllowRowFiltering, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AllowUpdate", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mAllowUpdate, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Case", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mCase, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CaseButtonDisplay", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mCaseButtonDisplay, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@CellMultiline", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mCellMultiline, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Format", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mFormat, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@GatherFilterDate", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mGatherFilterDate, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Hidden", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mHidden, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MergeCells", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mMergeCells, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Type", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mType, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@BaseColumnName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mBaseColumnName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DataType", SqlDbType.TinyInt)).Value = mDataHandler.DataValue_In(mDataType, SqlDbType.TinyInt)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@FieldLen", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mFieldLen, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsBound", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsBound, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Key", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mKey, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NullText", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mNullText, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngHeaderCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngHeaderCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngFooterCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngFooterCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbHeaderCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbHeaderCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbFooterCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbFooterCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsArabic", SqlDbType.Bit)).Value = mDataHandler.DataValue_In(mIsArabic, SqlDbType.Bit)

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            'If (strMode.Trim.ToUpper = "SAVE") Then
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.mDataBaseUserRelatedID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int)
            'End If

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

#End Region
#Region "Class Destructors"
    Public Sub finalized()
        mDataSet.Dispose()
    End Sub
#End Region
End Class

