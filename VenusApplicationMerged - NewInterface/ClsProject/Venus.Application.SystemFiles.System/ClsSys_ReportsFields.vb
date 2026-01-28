Public Class ClsSys_ReportsFields
    Inherits ClsDataAcessLayer

#Region "Class Constructor"

    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " "
        mInsertParameter = " "
        mInsertParameterValues = " "
        mUpdateParameter = " "
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    Private mID As Integer
    Private mCode As String
    Private mEngDescription As String
    Private mArbDescription As String
    Private mFieldName As String
    Private mDataType As String
    Private mDefaultValue As Object
    Private mStatus As Boolean
    Private mOperation As Integer
    Private mLength As Integer
    Private mLanguage As Integer
    Private mIsCriteria As Boolean

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

    Public Property FieldName() As String
        Get
            Return mFieldName
        End Get
        Set(ByVal Value As String)
            mFieldName = Value
        End Set
    End Property

    Public Property DataType() As String
        Get
            Return mDataType
        End Get
        Set(ByVal Value As String)
            mDataType = Value
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

    Public Property Status() As Boolean
        Get
            Return mStatus
        End Get
        Set(ByVal Value As Boolean)
            mStatus = Value
        End Set
    End Property

    Public Property Operation() As Integer
        Get
            Return mOperation
        End Get
        Set(ByVal Value As Integer)
            mOperation = Value
        End Set
    End Property

    Public Property Length() As Integer
        Get
            Return mLength
        End Get
        Set(ByVal Value As Integer)
            mLength = Value
        End Set
    End Property

    Public Property Langauge() As Integer
        Get
            Return mLangauge
        End Get
        Set(ByVal Value As Integer)
            mLangauge = Value
        End Set
    End Property

    Public Property IsCriteria() As Boolean
        Get
            Return mIsCriteria
        End Get
        Set(ByVal Value As Boolean)
            mIsCriteria = Value
        End Set
    End Property

#End Region

End Class
