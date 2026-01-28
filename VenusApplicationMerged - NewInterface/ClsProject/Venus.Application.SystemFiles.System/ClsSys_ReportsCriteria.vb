'Project           : Venus V.
'Module            : (Account Module)
'Date Created      : 07-11-2007
'Developer         : [MAE]Mah Abdel-aziz
'Description       : 1-Implement Data Acess Layer of sys_ReportsCriteria table 
'                    2-Allow searching
'                    3-Get list with all codes
'                    4-Implement functions save(), update() and delete() to allow DML with some critera
'                    5-Implement functions first(),last(),next() and previous() to allow navigation between 
'                       records
'==========================================================================================================


'Imports Microsoft.SqlServer.Management.Smo
'Imports Microsoft.SqlServer.Management.Common
'Imports System.Data.SqlClient
Public Class ClsSys_ReportsCriteria
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Initialize insert ,update and delete commands
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  07-11-2007
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " sys_ReportsCriteria "
        mInsertParameter = " EngCaption,ArbCaption,ArbCaption4S,ParameterField,ParameterType,DefaultValue,MinValue,MaxValue,Report_ID,ObjectName,SubSearchID,IsArabic,Remarks,RegUserID,RegComputerID "
        mInsertParameterValues = " @EngCaption,@ArbCaption,@ArbCaption4S,@ParameterField,@ParameterType,@DefaultValue,@MinValue,@MaxValue,@Report_ID,@ObjectName,@SubSearchID,@IsArabic,@Remarks,@RegUserID,@RegComputerID "
        mUpdateParameter = " EngCaption=@EngCaption,ArbCaption=@ArbCaption,ArbCaption4S=@ArbCaption4S,ParameterField=@ParameterField,ParameterType=@ParameterType,DefaultValue=@DefaultValue,MinValue=@MinValue,MaxValue=@MaxValue,Report_ID=@Report_ID,ObjectName=@ObjectName,SubSearchID=@SubSearchID,IsArabic=@IsArabic,Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID "
        mSelectCommand = CONFIG_DATEFORMAT & " Select sys_ReportsCriteria.ID,sys_ReportsCriteria.EngCaption,sys_ReportsCriteria.ArbCaption,sys_ReportsCriteria.ArbCaption4S,sys_ReportsCriteria.ParameterField,sys_ReportsCriteria.ParameterType,sys_ReportsCriteria.DefaultValue,sys_ReportsCriteria.MinValue,sys_ReportsCriteria.MaxValue,sys_ReportsCriteria.Report_ID,sys_ReportsCriteria.ObjectName,sys_ReportsCriteria.SubSearchID,sys_ReportsCriteria.IsArabic,sys_ReportsCriteria.Remarks,sys_ReportsCriteria.RegUserID,sys_ReportsCriteria.RegComputerID,sys_ReportsCriteria.RegDate,sys_ReportsCriteria.CancelDate From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Object

    Private mEngCaption As String
    Private mArbCaption As String
    Private mArbCaption4S As String
    Private mParameterField As String
    Private mParameterType As Integer
    Private mDefaultValue As String
    Private mMinValue As Single
    Private mMaxValue As Single
    Private mReport_ID As Integer
    Private mObjectName As String
    Private mSubSearchID As Integer
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

    Public Property EngCaption() As String
        Get
            Return mEngCaption
        End Get
        Set(ByVal value As String)
            mEngCaption = value
        End Set
    End Property
    Public Property ArbCaption() As String
        Get
            Return mArbCaption
        End Get
        Set(ByVal value As String)
            mArbCaption = value
            mArbCaption4S = mStringHandler.ReplaceHamza(value)
        End Set
    End Property
    Public Property ArbCaption4S() As String
        Get
            Return mArbCaption4S
        End Get
        Set(ByVal value As String)

            mArbCaption4S = value
        End Set
    End Property
    Public Property ParameterField() As String
        Get
            Return mParameterField
        End Get
        Set(ByVal value As String)
            mParameterField = value
        End Set
    End Property
    Public Property ParameterType() As Integer
        Get
            Return mParameterType
        End Get
        Set(ByVal value As Integer)
            mParameterType = value
        End Set
    End Property
    Public Property DefaultValue() As String
        Get
            Return mDefaultValue
        End Get
        Set(ByVal value As String)
            mDefaultValue = value
        End Set
    End Property
    Public Property MinValue() As Single
        Get
            Return mMinValue
        End Get
        Set(ByVal value As Single)
            mMinValue = value
        End Set
    End Property
    Public Property MaxValue() As Single
        Get
            Return mMaxValue
        End Get
        Set(ByVal value As Single)
            mMaxValue = value
        End Set
    End Property
    Public Property Report_ID() As Integer
        Get
            Return mReport_ID
        End Get
        Set(ByVal value As Integer)
            mReport_ID = value
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
    Public Property SubSearchID() As Integer
        Get
            Return mSubSearchID
        End Get
        Set(ByVal value As Integer)
            mSubSearchID = value
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
    'Date Created   :  07-11-2007
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
                'Item.DisplayText = mDataHandler.DataValue(ObjDataRow("EngCaption"), SqlDbType.VarChar)
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
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with English name column and its value with ID column
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  07-11-2007
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
                'Item.Text = mDataHandler.DataValue(ObjDataRow("EngCaption"), SqlDbType.VarChar)
                Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "EngCaption/ArbCaption")), SqlDbType.VarChar)
                If (Item.Text.Trim = "") Then
                    Item.Text = mDataHandler.DataValue(ObjDataRow(ObjNavigationHandler.SetLanguage(mPage, "ArbName/EngName")), SqlDbType.VarChar)
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
    'ProcedureName  :  GetParTypesList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill Value List with Parameters Types
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  07-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetParTypesList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ParTypesArr() As String = {"Text", "Number", "Binary", "GDate", "HDate", "EList", "AList"}

        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In ParTypesArr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = str
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

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetParTypesDropDownList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with Parmeters Types
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  07-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :DropDownList     :used to fill it with English name
    '========================================================================
    Public Function GetParTypesDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ParTypesArr() As String = {"Text", "Number", "Binary", "GDate", "HDate", "EList", "AList"}


        Try

            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In ParTypesArr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = str
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

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetDefValsList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill Value List with Default Values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  07-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetDefValsList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim Arr() As String = {"NowDate", "NowDateTime", "BoD_DateTime", "NowTime", "BoM_Date", "BoY_Date", "EoM_Date"}

        Try

            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            For Each str As String In Arr
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = str
                Item.DataValue = str
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

        End Try
    End Function

    '========================================================================
    'ProcedureName  :  GetDefValsDropDownList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with Default Values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  07-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :DropDownList     :used to fill it with English name
    '========================================================================
    Public Function GetDefValsDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean

        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim Arr() As String = {"NowDate", "NowDateTime", "BoD_DateTime", "NowTime", "BoM_Date", "BoY_Date", "EoM_Date"}
        Try

            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each str As String In Arr
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = str
                Item.Value = str
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

        End Try
    End Function


    '========================================================================
    'ProcedureName  :  GetParFieldsList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill Value List with Default Values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  07-11-2007
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :ValueList     :used to fill it with English name column
    '========================================================================
    Public Function GetParFieldsList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal RepID As Integer, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem

        Try
            StrSelectCommand = "Select * from sys_Reports where ID=" & RepID & " "
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)


            Dim storedName As String = mDataHandler.DataValue(ObjDataset.Tables(0).Rows(0)("DataSource"), SqlDbType.VarChar)
            If (storedName = Nothing) Then
                Return False
            End If

            StrSelectCommand = "select * from sys.syscolumns inner join sys.sysobjects	sysObj on sys.syscolumns.Id = sysObj .id where sysObj.Type = 'P' and sysObj.name = '" + IIf(storedName.IndexOf(";") > -1, storedName.Split(";")(0), storedName) + "'"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)



            DdlValues.ValueListItems.Clear()

            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                'Item.DisplayText = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ ÅÎÊÑ ÃÍÏ ÇáÅÎÊíÇÑÇÊ ] ")
                Item.DisplayText = IIf(Me.mLangauge = Language.English, " ", " ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If

            Dim Index As Int16 = 1
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = mDataHandler.DataValue(ObjDataRow("name"), SqlDbType.VarChar)
                Item.DataValue = mDataHandler.DataValue(ObjDataRow("xtype"), SqlDbType.Int).ToString & "_" & Index.ToString
                DdlValues.ValueListItems.Add(Item)
                Index = Index + 1

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
    'ProcedureName  :  GetParFieldsDropDownList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with Default Values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  07-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :DropDownList     :used to fill it with English name
    '========================================================================
    Public Function GetParFieldsDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal RepID As Integer, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Try
            StrSelectCommand = "Select * from sys_Reports where ID=" & RepID & " "
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)

            DdlValues.Items.Clear()
            Dim storedName As String = mDataHandler.DataValue(ObjDataset.Tables(0).Rows(0)("DataSource"), SqlDbType.VarChar)
            If (storedName = Nothing) Then
                Return False
            End If

            StrSelectCommand = "select * from sys.syscolumns inner join sys.sysobjects	sysObj on sys.syscolumns.Id = sysObj .id where sysObj.Type = 'P' and sysObj.name = '" + IIf(storedName.IndexOf(";") > -1, storedName.Split(";")(0), storedName) + "'"
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)

            'Dim myServer As New Server(New ServerConnection(New SqlConnection(mConnectionString)))
            'Dim sp As New StoredProcedure(New Database(myServer, mConnectionString.Split(";")(1).Split("=")(1)), storedName)
            'Dim parCol As StoredProcedureParameterCollection = sp.Parameters

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If
            'Dim Index As Int16 = 1
            'For Each pc As StoredProcedureParameterCollection In parCol
            '    Item = New Global.System.Web.UI.WebControls.ListItem
            '    Item.Text = pc.ToString
            '    Item.Value = pc.ToString 'Index
            '    DdlValues.Items.Add(Item)
            '    Index = Index + 1
            'Next
            Dim Index As Int16 = 1
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow("name"), SqlDbType.VarChar)
                Item.Value = mDataHandler.DataValue(ObjDataRow("xtype"), SqlDbType.Int).ToString & "_" & Index.ToString
                DdlValues.Items.Add(Item)
                Index = Index + 1
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
    'ProcedureName  :  GetParFieldsDropDownList
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Fill DropDownList with Default Values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  [MAE]Mah Abdel-aziz   
    'Date Created   :  07-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'DdlValues             :DropDownList     :used to fill it with English name
    '========================================================================
    Public Function GetObjectParFieldsDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal RepID As Integer, ByVal NullNode As Boolean, Optional ByVal Filter As String = "") As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Try
            'StrSelectCommand = "Select * from sys_Reports where ID=" & RepID & " "
            'ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)

            'DdlValues.Items.Clear()
            'Dim storedName As String = mDataHandler.DataValue(ObjDataset.Tables(0).Rows(0)("DataSource"), SqlDbType.VarChar)
            'If (storedName = Nothing) Then
            '    Return False
            'End If

            StrSelectCommand = "select * from sys_Fields fld inner join sys_objects	obj on fld.ObjectID=obj.ID  inner join sys_Reports rep on rep.Datasource = obj.Code where rep.ID = " & RepID & ""
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnectionString, CommandType.Text, StrSelectCommand)

            'Dim myServer As New Server(New ServerConnection(New SqlConnection(mConnectionString)))
            'Dim sp As New StoredProcedure(New Database(myServer, mConnectionString.Split(";")(1).Split("=")(1)), storedName)
            'Dim parCol As StoredProcedureParameterCollection = sp.Parameters

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = IIf(Me.mLangauge = Language.English, "[Select Your Choice]", " [ برجاء الاختيار ] ")
                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If
            'Dim Index As Int16 = 1
            'For Each pc As StoredProcedureParameterCollection In parCol
            '    Item = New Global.System.Web.UI.WebControls.ListItem
            '    Item.Text = pc.ToString
            '    Item.Value = pc.ToString 'Index
            '    DdlValues.Items.Add(Item)
            '    Index = Index + 1
            'Next
            Dim Index As Int16 = 1
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = mDataHandler.DataValue(ObjDataRow("FieldName"), SqlDbType.VarChar)
                Item.Value = ObjDataRow("FieldType") 'mDataHandler.DataValue(ObjDataRow("xtype"), SqlDbType.Int).ToString & "_" & Index.ToString
                DdlValues.Items.Add(Item)
                Index = Index + 1
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
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Find all rows that match criteria or filter and fill  them into Dataset
    'Developer      :  DataOcean   
    'Date Created   :  07-11-2007
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
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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
    'Date Created   :  07-11-2007
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
    'Date Created   :  07-11-2007
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
    'Date Created   :  07-11-2007
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
    'Date Created   :  07-11-2007
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
    'Date Created   :  07-11-2007
    'Modifacations  : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0

            mEngCaption = String.Empty
            mArbCaption = String.Empty
            mArbCaption4S = String.Empty
            mParameterField = String.Empty
            mParameterType = Nothing
            mDefaultValue = Nothing
            mMinValue = Nothing
            mMaxValue = Nothing
            mReport_ID = Nothing

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




#Region "Navigation Functions"
    '========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord
    'Module         :  (Account Module)
    'Project        :  Venus V.
    'Description    :  Navigate betweeen records (all records not canceled and canceld records)
    'Developer      :  DataOcean   
    'Date Created   :  07-11-2007
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
    'ProcedureName  :  GetParameter
    'Module         : (Account Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :DataOcean   
    'Date Created   :07-11-2007
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

                mEngCaption = mDataHandler.DataValue_Out(.Item("EngCaption"), SqlDbType.VarChar)
                mArbCaption = mDataHandler.DataValue_Out(.Item("ArbCaption"), SqlDbType.VarChar)
                mArbCaption4S = mDataHandler.DataValue_Out(.Item("ArbCaption4S"), SqlDbType.VarChar)
                mParameterField = mDataHandler.DataValue_Out(.Item("ParameterField"), SqlDbType.VarChar)
                mParameterType = mDataHandler.DataValue_Out(.Item("ParameterType"), SqlDbType.Int)
                mDefaultValue = mDataHandler.DataValue_Out(.Item("DefaultValue"), SqlDbType.VarChar)
                mMinValue = mDataHandler.DataValue_Out(.Item("MinValue"), SqlDbType.Real)
                mMaxValue = mDataHandler.DataValue_Out(.Item("MaxValue"), SqlDbType.Real)
                mReport_ID = mDataHandler.DataValue_Out(.Item("Report_ID"), SqlDbType.Int)
                mObjectName = mDataHandler.DataValue_Out(.Item("ObjectName"), SqlDbType.VarChar)
                mSubSearchID = mDataHandler.DataValue_Out(.Item("SubSearchID"), SqlDbType.Int)
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
    'Date Created   :07-11-2007
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

            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EngCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mEngCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbCaption", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbCaption, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ArbCaption4S", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mArbCaption4S, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ParameterField", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mParameterField, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ParameterType", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mParameterType, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DefaultValue", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mDefaultValue, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MinValue", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mMinValue, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@MaxValue", SqlDbType.Real)).Value = mDataHandler.DataValue_In(mMaxValue, SqlDbType.Real)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Report_ID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mReport_ID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ObjectName", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mObjectName, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SubSearchID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mSubSearchID, SqlDbType.Int)
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

