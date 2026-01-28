Imports Venus.Application.SystemFiles.System
Public Class ClsAtt_AttendancePreparationProjects
    Inherits ClsDataAcessLayer
#Region "Class Constructors"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " Att_AttendancePreparationProjects "
        mInsertParameter = "" & _
          "TrnsID," & _
          "ProjectID," & _
          "Checkin," & _
          "Checkout," & _
          "TotalTime," & _
          "Overtime," & _
          "HolidayHours," & _
          "IsVacation," & _
          "IsAbsent," & _
          "IsSick," & _
          "IsLeave," & _
          "NotpermitLate," & _
          "AbsentPunishment," & _
          "SickPunishment," & _
          "OTFactor," & _
          "HOTFactor," & _
          "LeavePunishment," & _
          "LatPunishment," & _
          "SalaryPerDay," & _
          "SalaryPerHour," & _
          "OTSalary," & _
          "HOTSalary," & _
          "Remarks," & _
          "RegUserID," & _
          "RegComputerID," & _
          "OOvertime," & _
          "OHolidayHours," & _
          "LinkedCS"
        mInsertParameterValues = "" & _
          " @TrnsID," & _
          " @ProjectID," & _
          " @Checkin," & _
          " @Checkout," & _
          " @TotalTime," & _
          " @Overtime," & _
          " @HolidayHours," & _
          " @IsVacation," & _
          " @IsAbsent," & _
          " @IsSick," & _
          " @IsLeave," & _
          " @NotpermitLate," & _
          " @AbsentPunishment," & _
          " @SickPunishment," & _
          " @OTFactor," & _
          " @HOTFactor," & _
          " @LeavePunishment," & _
          " @LatPunishment," & _
          " @SalaryPerDay," & _
          " @SalaryPerHour," & _
          " @OTSalary," & _
          " @HOTSalary," & _
          " @Remarks," & _
          " @RegUserID," & _
          " @RegComputerID," & _
          " @OOvertime," & _
          " @OHolidayHours," & _
          " @LinkedCS"
        mUpdateParameter = "" & _
          "TrnsID=@TrnsID," & _
          "ProjectID=@ProjectID," & _
          "Checkin=@Checkin," & _
          "Checkout=@Checkout," & _
          "TotalTime=@TotalTime," & _
          "Overtime=@Overtime," & _
          "HolidayHours=@HolidayHours," & _
          "IsVacation=@IsVacation," & _
          "IsAbsent=@IsAbsent," & _
          "IsSick=@IsSick," & _
          "IsLeave=@IsLeave," & _
          "NotpermitLate=@NotpermitLate," & _
          "AbsentPunishment=@AbsentPunishment," & _
          "SickPunishment=@SickPunishment," & _
          "OTFactor=@OTFactor," & _
          "HOTFactor=@HOTFactor," & _
          "LeavePunishment=@LeavePunishment," & _
          "LatPunishment=@LatPunishment," & _
          "SalaryPerDay=@SalaryPerDay," & _
          "SalaryPerHour=@SalaryPerHour," & _
          "OTSalary=@OTSalary," & _
          "HOTSalary=@HOTSalary," & _
          "Remarks=@Remarks," & _
          "OOvertime=@OOvertime," & _
          "OHolidayHours=@OHolidayHours," & _
          "LinkedCS=@LinkedCS"
        mSelectCommand = CONFIG_DATEFORMAT & " Select * From  " & mTable
        mInsertCommand = CONFIG_DATEFORMAT & " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = CONFIG_DATEFORMAT & " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub
#End Region
#Region "Private Members"
    Private mID As Integer
    Private mTrnsID As Integer
    Private mProjectID As Integer
    Private mCheckin As DateTime
    Private mCheckout As DateTime
    Private mTotalTime As Decimal
    Private mOvertime As Decimal
    Private mHolidayHours As Decimal
    Private mIsVacation As Boolean
    Private mIsAbsent As Boolean
    Private mIsSick As Boolean
    Private mIsLeave As Boolean
    Private mNotpermitLate As Decimal
    Private mAbsentPunishment As Decimal
    Private mSickPunishment As Decimal
    Private mOTFactor As Decimal
    Private mHOTFactor As Decimal
    Private mLeavePunishment As Decimal
    Private mLatPunishment As Decimal
    Private mSalaryPerDay As Decimal
    Private mSalaryPerHour As Decimal
    Private mOTSalary As Decimal
    Private mHOTSalary As Decimal
    Private mRemarks As String
    Private mRegUserID As Integer
    Private mRegComputerID As Integer
    Private mRegDate As DateTime
    Private mCancelDate As DateTime
    Private mOOvertime As Decimal
    Private mOHolidayHours As Decimal
    Private mLinkedCS As String

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
    Public Property TrnsID() As Integer
        Get
            Return mTrnsID
        End Get
        Set(ByVal Value As Integer)
            mTrnsID = Value
        End Set
    End Property
    Public Property ProjectID() As Integer
        Get
            Return mProjectID
        End Get
        Set(ByVal Value As Integer)
            mProjectID = Value
        End Set
    End Property
    Public Property Checkin() As DateTime
        Get
            Return mCheckin
        End Get
        Set(ByVal Value As DateTime)
            mCheckin = Value
        End Set
    End Property
    Public Property Checkout() As DateTime
        Get
            Return mCheckout
        End Get
        Set(ByVal Value As DateTime)
            mCheckout = Value
        End Set
    End Property
    Public Property TotalTime() As Decimal
        Get
            Return mTotalTime
        End Get
        Set(ByVal Value As Decimal)
            mTotalTime = Value
        End Set
    End Property
    Public Property Overtime() As Decimal
        Get
            Return mOvertime
        End Get
        Set(ByVal Value As Decimal)
            mOvertime = Value
        End Set
    End Property
    Public Property HolidayHours() As Decimal
        Get
            Return mHolidayHours
        End Get
        Set(ByVal Value As Decimal)
            mHolidayHours = Value
        End Set
    End Property
    Public Property IsVacation() As Boolean
        Get
            Return mIsVacation
        End Get
        Set(ByVal Value As Boolean)
            mIsVacation = Value
        End Set
    End Property
    Public Property IsAbsent() As Boolean
        Get
            Return mIsAbsent
        End Get
        Set(ByVal Value As Boolean)
            mIsAbsent = Value
        End Set
    End Property
    Public Property IsSick() As Boolean
        Get
            Return mIsSick
        End Get
        Set(ByVal Value As Boolean)
            mIsSick = Value
        End Set
    End Property
    Public Property IsLeave() As Boolean
        Get
            Return mIsLeave
        End Get
        Set(ByVal Value As Boolean)
            mIsLeave = Value
        End Set
    End Property
    Public Property NotpermitLate() As Decimal
        Get
            Return mNotpermitLate
        End Get
        Set(ByVal Value As Decimal)
            mNotpermitLate = Value
        End Set
    End Property
    Public Property AbsentPunishment() As Decimal
        Get
            Return mAbsentPunishment
        End Get
        Set(ByVal Value As Decimal)
            mAbsentPunishment = Value
        End Set
    End Property
    Public Property SickPunishment() As Decimal
        Get
            Return mSickPunishment
        End Get
        Set(ByVal Value As Decimal)
            mSickPunishment = Value
        End Set
    End Property
    Public Property OTFactor() As Decimal
        Get
            Return mOTFactor
        End Get
        Set(ByVal Value As Decimal)
            mOTFactor = Value
        End Set
    End Property
    Public Property HOTFactor() As Decimal
        Get
            Return mHOTFactor
        End Get
        Set(ByVal Value As Decimal)
            mHOTFactor = Value
        End Set
    End Property
    Public Property LeavePunishment() As Decimal
        Get
            Return mLeavePunishment
        End Get
        Set(ByVal Value As Decimal)
            mLeavePunishment = Value
        End Set
    End Property
    Public Property LatPunishment() As Decimal
        Get
            Return mLatPunishment
        End Get
        Set(ByVal Value As Decimal)
            mLatPunishment = Value
        End Set
    End Property
    Public Property SalaryPerDay() As Decimal
        Get
            Return mSalaryPerDay
        End Get
        Set(ByVal Value As Decimal)
            mSalaryPerDay = Value
        End Set
    End Property
    Public Property SalaryPerHour() As Decimal
        Get
            Return mSalaryPerHour
        End Get
        Set(ByVal Value As Decimal)
            mSalaryPerHour = Value
        End Set
    End Property
    Public Property OTSalary() As Decimal
        Get
            Return mOTSalary
        End Get
        Set(ByVal Value As Decimal)
            mOTSalary = Value
        End Set
    End Property
    Public Property HOTSalary() As Decimal
        Get
            Return mHOTSalary
        End Get
        Set(ByVal Value As Decimal)
            mHOTSalary = Value
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
    Public Property OOvertime() As Decimal
        Get
            Return mOOvertime
        End Get
        Set(ByVal Value As Decimal)
            mOOvertime = Value
        End Set
    End Property
    Public Property OHolidayHours() As Decimal
        Get
            Return mOHolidayHours
        End Get
        Set(ByVal Value As Decimal)
            mOHolidayHours = Value
        End Set
    End Property
    Public Property LinkedCS() As String
        Get
            Return mLinkedCS
        End Get
        Set(ByVal Value As String)
            mLinkedCS = Value
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
    'Date Created   : 11/10/2015
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
                Item.DisplayText = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/ [ إختر أحد الإختيارات ] ")
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
    'Date Created   : 11/10/2015
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
                Item.Text = ObjNavigationHandler.SetLanguage(mPage, "[Select Your Choice]/ [ إختر أحد الإختيارات ]")
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
    'Date Created   :11/10/2015
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
    'Date Created   :   11/10/2015
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
    'Date Created   :11/10/2015
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
    'Date Created   :11/10/2015 22:10:27
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
    'Date Created   :11/10/2015
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
            mTrnsID = 0
            mProjectID = 0
            mCheckin = Nothing
            mCheckout = Nothing
            mTotalTime = 0
            mOvertime = 0
            mHolidayHours = 0
            mIsVacation = False
            mIsAbsent = False
            mIsSick = False
            mIsLeave = False
            mNotpermitLate = 0
            mAbsentPunishment = 0
            mSickPunishment = 0
            mOTFactor = 0
            mHOTFactor = 0
            mLeavePunishment = 0
            mLatPunishment = 0
            mSalaryPerDay = 0
            mSalaryPerHour = 0
            mOTSalary = 0
            mHOTSalary = 0
            mRemarks = String.Empty
            mRegUserID = 0
            mRegComputerID = 0
            mRegDate = Nothing
            mCancelDate = Nothing
            mOOvertime = 0
            mOHolidayHours = 0
            mLinkedCS = String.Empty
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_ErrorsLog)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
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
    'Date Created   :11/10/2015
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
                mTrnsID = [Shared].DataHandler.DataValue_Out(.Item("TrnsID"), SqlDbType.Int, True)
                mProjectID = [Shared].DataHandler.DataValue_Out(.Item("ProjectID"), SqlDbType.Int, True)
                mCheckin = [Shared].DataHandler.DataValue_Out(.Item("Checkin"), SqlDbType.DateTime)
                mCheckout = [Shared].DataHandler.DataValue_Out(.Item("Checkout"), SqlDbType.DateTime)
                mTotalTime = [Shared].DataHandler.DataValue_Out(.Item("TotalTime"), SqlDbType.Decimal)
                mOvertime = [Shared].DataHandler.DataValue_Out(.Item("Overtime"), SqlDbType.Decimal)
                mHolidayHours = [Shared].DataHandler.DataValue_Out(.Item("HolidayHours"), SqlDbType.Decimal)
                mIsVacation = [Shared].DataHandler.DataValue_Out(.Item("IsVacation"), SqlDbType.Bit)
                mIsAbsent = [Shared].DataHandler.DataValue_Out(.Item("IsAbsent"), SqlDbType.Bit)
                mIsSick = [Shared].DataHandler.DataValue_Out(.Item("IsSick"), SqlDbType.Bit)
                mIsLeave = [Shared].DataHandler.DataValue_Out(.Item("IsLeave"), SqlDbType.Bit)
                mNotpermitLate = [Shared].DataHandler.DataValue_Out(.Item("NotpermitLate"), SqlDbType.Decimal)
                mAbsentPunishment = [Shared].DataHandler.DataValue_Out(.Item("AbsentPunishment"), SqlDbType.Decimal)
                mSickPunishment = [Shared].DataHandler.DataValue_Out(.Item("SickPunishment"), SqlDbType.Decimal)
                mOTFactor = [Shared].DataHandler.DataValue_Out(.Item("OTFactor"), SqlDbType.Decimal)
                mHOTFactor = [Shared].DataHandler.DataValue_Out(.Item("HOTFactor"), SqlDbType.Decimal)
                mLeavePunishment = [Shared].DataHandler.DataValue_Out(.Item("LeavePunishment"), SqlDbType.Decimal)
                mLatPunishment = [Shared].DataHandler.DataValue_Out(.Item("LatPunishment"), SqlDbType.Decimal)
                mSalaryPerDay = [Shared].DataHandler.DataValue_Out(.Item("SalaryPerDay"), SqlDbType.Decimal)
                mSalaryPerHour = [Shared].DataHandler.DataValue_Out(.Item("SalaryPerHour"), SqlDbType.Decimal)
                mOTSalary = [Shared].DataHandler.DataValue_Out(.Item("OTSalary"), SqlDbType.Decimal)
                mHOTSalary = [Shared].DataHandler.DataValue_Out(.Item("HOTSalary"), SqlDbType.Decimal)
                mRemarks = [Shared].DataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = [Shared].DataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = [Shared].DataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
                mRegDate = [Shared].DataHandler.DataValue_Out(.Item("RegDate"), SqlDbType.DateTime)
                mCancelDate = [Shared].DataHandler.DataValue_Out(.Item("CancelDate"), SqlDbType.DateTime)
                mOOvertime = [Shared].DataHandler.DataValue_Out(.Item("OOvertime"), SqlDbType.Decimal)
                mOHolidayHours = [Shared].DataHandler.DataValue_Out(.Item("OHolidayHours"), SqlDbType.Decimal)
                mLinkedCS = [Shared].DataHandler.DataValue_Out(.Item("LinkedCS"), SqlDbType.VarChar)
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
    'Date Created   : 11/10/2015 22:10:27
    'Modifacations  :
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand             :SqlCommand     :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand, ByVal strMode As String) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TrnsID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mTrnsID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@ProjectID", SqlDbType.Int)).Value = [Shared].DataHandler.DataValue_In(mProjectID, SqlDbType.Int)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Checkin", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mCheckin, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Checkout", SqlDbType.DateTime)).Value = [Shared].DataHandler.DataValue_In(mCheckout, SqlDbType.DateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@TotalTime", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mTotalTime, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Overtime", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mOvertime, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HolidayHours", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mHolidayHours, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsVacation", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsVacation, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsAbsent", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsAbsent, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsSick", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsSick, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@IsLeave", SqlDbType.Bit)).Value = [Shared].DataHandler.DataValue_In(mIsLeave, SqlDbType.Bit)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@NotpermitLate", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mNotpermitLate, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@AbsentPunishment", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mAbsentPunishment, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SickPunishment", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mSickPunishment, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OTFactor", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mOTFactor, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HOTFactor", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mHOTFactor, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LeavePunishment", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mLeavePunishment, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LatPunishment", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mLatPunishment, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SalaryPerDay", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mSalaryPerDay, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@SalaryPerHour", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mSalaryPerHour, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OTSalary", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mOTSalary, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@HOTSalary", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mHOTSalary, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OOvertime", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mOOvertime, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@OHolidayHours", SqlDbType.Decimal)).Value = [Shared].DataHandler.DataValue_In(mOHolidayHours, SqlDbType.Decimal)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@LinkedCS", SqlDbType.VarChar)).Value = [Shared].DataHandler.DataValue_In(mLinkedCS, SqlDbType.VarChar)
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
#Region "Class Destructors"
    Public Sub finalized()
        mDataset.Dispose()
    End Sub
#End Region
End Class
