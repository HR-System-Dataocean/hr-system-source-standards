
'==========================================================================
'Program File Name : Payroll.Net
'Project           : Venus V.
'Module            : Hrs (Human Resource Module)
'Developer         : DataOcean
'Date Created      : [18-07-2007]
'Description       : 1-Implement Data Acess Layer of Employees Departments (hrs_EmployeesDepartments table) 
'                    2-Every Employees Department record has Employee ID ,Department ID and Start Date
'==========================================================================
Imports Venus.Application.SystemFiles.System
Public Class Clshrs_EmployeesDepartments
    Inherits ClsDataAcessLayer
#Region "Class Constructors"

    '========================================================================
    'ProcedureName  :  Constractor 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Initialize table name,table fields,parametrs and select,delete and update statments
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    '========================================================================
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)
        mTable = " hrs_EmployeesDepartments "
        mInsertParameter = " EmployeeID,DepartmentID,StartDate,EndDate,Remarks,RegUserID,RegComputerID"
        mInsertParameterValues = " @EmployeeID,@DepartmentID,@StartDate,@EndDate,@Remarks,@RegUserID,@RegComputerID"
        mUpdateParameter = " EmployeeID=@EmployeeID,DepartmentID=@DepartmentID,StartDate=@StartDate,EndDate=@EndDate,Remarks=@Remarks,RegUserID=@RegUserID,RegComputerID=@RegComputerID"
        mSelectCommand = " Select * From  " & mTable
        mInsertCommand = " insert into " & mTable & "( " & mInsertParameter & ")Values(" & mInsertParameterValues & ")"
        mUpdateCommand = " Update " & mTable & " Set " & mUpdateParameter
        mDeleteCommand = " Update " & mTable & " Set CancelDate=GetDate()"
    End Sub

#End Region

#Region "Private Members"

    Private mID As Object
    Private mEmployeeID As Object
    Private mDepartmentID As Object
    Private mStartDate As Date
    Private mRemarks As String
    Private mRegUserID As Object
    Private mRegComputerID As Object
    Private mRegDate As Object
    Private mCancelDate As Object
    Const uwgDepartmentId = 1
    Const uwgStartDate = 2


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
    Public Property EmployeeID() As Object
        Get
            Return mEmployeeID
        End Get
        Set(ByVal Value As Object)
            mEmployeeID = Value
        End Set
    End Property
    Public Property DepartmentID() As Object
        Get
            Return mDepartmentID
        End Get
        Set(ByVal Value As Object)
            mDepartmentID = Value
        End Set
    End Property
    Public Property StartDate() As Date
        Get
            Return mStartDate
        End Get
        Set(ByVal value As Date)
            mStartDate = value
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

    '==========================================================================
    'ProcedureName  :  Find 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Find all rows in hrs_EmployeesDepartments that match criteria
    '                  2-return true if operation done
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : Employees Departments screen (frmEmployeesDepartments.aspx.vb)
    '               : (frmEmployeesDepartments.aspx.vb) TlbMainNavigation_ButtonClicked() 
    '               : (frmEmployeesDepartments.aspx.vb) SetToolbarSetting()
    '               : (frmEmployeesDepartments.aspx.vb) Page_Load()
    '               : (frmEmployeesDepartments.aspx.vb) TlbMainToolbar_ButtonClicked()
    'To             : GetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as criteria to filter rows  ex:'ID=2'
    '========================================================================
    Public Function Find(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = mSelectCommand & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    '==========================================================================
    'ProcedureName  :  FindDepartments 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Find only Departments English Name and Departments Arab Name from sys_Departments
    '                  that have ids in hrs_EmployeesDepartments and match criteria
    '                  2-return true if operation done
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : Employees Departments screen (frmEmployeesDepartments.aspx.vb)
    '               : (frmEmployeesDepartments.aspx.vb) SetToolbarSetting() 
    'To             : GetParameter()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as criteria to filter rows  ex:'EmployeeID=2'
    '========================================================================
    Public Function FindDepartments(ByVal Filter As String) As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = " Select*, (Select EngName From sys_Departments Where sys_Departments.id= hrs_EmployeesDepartments.departmentid)  As DepartmentEnglishName,(Select ArbName From sys_Departments Where sys_Departments.id= hrs_EmployeesDepartments.departmentid)  As DepartmentArabicName From hrs_EmployeesDepartments " & IIf(Len(Filter) > 0, " Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 And " & Filter, "  Where IsNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1 ")
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

    '==========================================================================
    'ProcedureName  :  SaveUpdate 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Used to update record if found(by checking its ID in hrs_EmployeesDepartments table) 
    '                   otherwise insert it as new record
    '                  2-return true if operation done
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : 
    'To             : Update()
    '               : Save()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as criteria to filter rows  ex:'EmployeeID=2'
    '========================================================================
    Public Function SaveUpdate(ByVal Filter As String) As Boolean
        Dim strSQL As String
        Dim Value As Integer
        Try
            strSQL = "Select ID From hrs_EmployeesDepartments Where " & Filter
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

    '==========================================================================
    'ProcedureName  :  Save 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Used to insert new record in hrs_EmployeesDepartments table
    '                  2-return true if operation done
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : SaveUpdate()
    '               :
    'To             : SetParameter()
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

    '==========================================================================
    'ProcedureName  :  Update 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Used to update record if found(by checking its ID in hrs_EmployeesDepartments) 
    '                   otherwise insert it as new record
    '                  2-return true if operation done
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : SaveUpdate() 
    'To             : SetParameter()
    '               : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as critera to filter rows  ex:'EmployeeID=2'
    '========================================================================
    Public Function Update(ByVal Filter As String) As Boolean
        Dim StrUpdateCommand As String
        Try
            StrUpdateCommand = mUpdateCommand & IIf(Len(Filter) > 0, " Where " & Filter, "")
            mSqlCommand = New SqlClient.SqlCommand
            mSqlCommand.Connection = New SqlClient.SqlConnection(mConnectionString)
            mSqlCommand.CommandType = CommandType.Text
            mSqlCommand.CommandText = StrUpdateCommand
            SetParameter(mSqlCommand)
            mSqlCommand.Connection.Open()
            mSqlCommand.ExecuteNonQuery()
            mSqlCommand.Connection.Close()
            Return True
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrUpdateCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        End Try
    End Function

    '==========================================================================
    'ProcedureName  :  Delete 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  1-Used to delete record that match criteria from hrs_EmployeesDepartments table 
    '                  2-return true if operation done
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : (frmEmployeesDepartments.aspx.vb) TlbMainToolbar_ButtonClicked() 
    'To             : SetParameter()
    '               : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Filter             :String     :used as criteria to filter rows  ex:'EmployeeID=2'
    '========================================================================
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

    '==========================================================================
    'ProcedureName  :  Clear 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Clear all object attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : 
    'To             : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================
    Public Function Clear() As Boolean
        Try
            mID = 0
            mEmployeeID = 0
            mDepartmentID = 0
            mRemarks = String.Empty
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


#Region "Navigation Functions"

    '==========================================================================
    'ProcedureName  :  FirstRecord,LastRecord,NextRecord and PreviousRecord 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Used to navigate between all record in hrs_EmployeesDepartments table
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : (frmEmployeesDepartments.aspx.vb) TlbMainNavigation_ButtonClicked()
    'To             : 
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    '========================================================================

    Public Function FirstRecord() As Boolean
        Dim StrSelectCommand As String
        Try
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' and IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " Where isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID >" & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID ASC"
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
            StrSelectCommand = "SELECT TOP 1 * FROM " & mTable & " WHERE ID < " & mID & " And isNull(CancelDate,'')='' And IsNull(dbo.hrs_GetRecordViewStatus(ID,'" & Me.mTable & "'," & Me.mDataBaseUserRelatedID & "," & Me.GroupID & "),0) <> 1  ORDER BY ID DESC"
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
#End Region

    '===============================================================================
    'Date           : 27/06/2007                                                   '
    'Created by     : Eng.ahmed ehssan                                             ' 
    'Input          : Grid,which is represent the complete set of rows             ' 
    '               : EmployeeID,which is represent the forign key of the          '
    '               : employees departments tables                                 '
    'Output         : Boolean,in case of successfully execute the function         '
    '               : the function will return true otherwise false                '
    'Description    : Dimintion Row object and start itrate through the            '
    '               : grid in order to collect all the data form The grid          '
    '               : and create the TSQL statment for the insert                  ' 
    'Steps          : 1- Delete all the rows in Employees Departments              '
    '               :    table related to this employees                           '  
    '               : 2- collect all the rows form the grid and start to           ' 
    '               :    create the tsql statment                                  '
    '               : 3- execute the statment and return back                      ' 
    'Modification   : Mah Abdel-aziz [18-07-2007]
    '               :Add some validations:
    '                 1-From Date is required
    '                 2-No subsequent departments are equal
    '                 2-No subsequent dates are equal
    '===============================================================================

    Public Function SaveEmployeesDepartments(ByVal ObjGrid As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal IntEmployeeId As Integer, Optional ByRef msg As String = "") As Boolean
        Dim ObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Dim prvObjRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow

        Dim StrSqlCommand As String = String.Empty
        Dim ObjSqlCommand As New SqlClient.SqlCommand
        Try
            StrSqlCommand = "Delete from hrs_EmployeesDepartments Where EmployeeID=" & IntEmployeeId & ";" & vbNewLine
            Dim ind As Integer = -1
            For Each ObjRow In ObjGrid.Rows
                ind = ind + 1
                If (ObjRow.Cells(uwgStartDate).Value Is Nothing) Then
                    msg = "From Date is required"
                    Return False
                End If

                If Not ObjRow.Cells(uwgDepartmentId).Value Is Nothing Then
                    StrSqlCommand &= "Insert Into hrs_EmployeesDepartments(EmployeeID,DepartmentID,StartDate)Values" & _
                                   "(" & IntEmployeeId & _
                                   "," & ObjRow.Cells(uwgDepartmentId).Value & _
                                   ",'" & IIf(ObjRow.Cells(uwgStartDate).Value Is Nothing, "", ObjRow.Cells(uwgStartDate).Value) & _
                                   "');" & vbNewLine

                    If ind > 0 Then

                        Dim deptID1 As Int32 = ObjRow.Cells(uwgDepartmentId).Value
                        Dim deptID2 As Int32 = prvObjRow.Cells(uwgDepartmentId).Value
                        If (deptID1 = deptID2) Then
                            msg = "Two subsequent departments are equal "
                            Return False
                        End If


                        Dim d1 As Date = ObjRow.Cells(uwgStartDate).Value
                        Dim d2 As Date = prvObjRow.Cells(uwgStartDate).Value
                        If DateTime.Compare(d1, d2) <= 0 Then
                            msg = "Two Dates are equal or subsequent date less than its previous date "
                            Return False
                        End If
                    End If
                End If
                prvObjRow = ObjRow
            Next

            ObjSqlCommand.Connection = New SqlClient.SqlConnection(ConnectionString)
            ObjSqlCommand.CommandText = CONFIG_DATEFORMAT & " " & StrSqlCommand
            ObjSqlCommand.CommandType = CommandType.Text
            ObjSqlCommand.Connection.Open()
            ObjSqlCommand.ExecuteNonQuery()
            ObjSqlCommand.Connection.Close()
            Return True

        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase(StrSqlCommand, ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            mPage.Response.Redirect("ErrorPage.aspx")
        Finally
            Try
                ObjSqlCommand.Connection.Close()
                ObjSqlCommand.Connection = Nothing
                ObjSqlCommand.Dispose()
            Catch ex As Exception


            End Try

        End Try
    End Function

#End Region

#Region "Class Private Function"

    '==========================================================================
    'ProcedureName  :  GetParameter 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign Result of Dataset to private attributes
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : 
    'To             : Find()
    '               : FindDepartments()
    '               : FirstRecord()
    '               : NextRecord()
    '               : PreviousRecord()
    '               : LastRecord()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Ds                 :DataSet    :used its attributes to assign them to object attributes
    '========================================================================
    Private Function GetParameter(ByVal Ds As DataSet) As Boolean
        Try
            With Ds.Tables(0).Rows(0)
                mID = mDataHandler.DataValue_Out(.Item("ID"), SqlDbType.Int, True)
                mEmployeeID = mDataHandler.DataValue_Out(.Item("EmployeeID"), SqlDbType.Int, True)
                mDepartmentID = mDataHandler.DataValue_Out(.Item("DepartmentID"), SqlDbType.Int, True)
                mStartDate = mDataHandler.DataValue_Out(.Item("StartDate"), SqlDbType.SmallDateTime)
                mRemarks = mDataHandler.DataValue_Out(.Item("Remarks"), SqlDbType.VarChar)
                mRegUserID = mDataHandler.DataValue_Out(.Item("RegUserID"), SqlDbType.Int, True)
                mRegComputerID = mDataHandler.DataValue_Out(.Item("RegComputerID"), SqlDbType.Int, True)
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

    '==========================================================================
    'ProcedureName  :  SetParameter 
    'Module         :  Hrs (Human Resource Module)
    'Project        :  Venus V.
    'Description    :  Assign parameters of sqlcommand values with private attributes values
    '                  and return true if operation done otherwise report errors in ErrorPage
    'Developer      :  DataOcean
    'Date Created   :  [18-07-2007]
    'Modifacations  : 
    'Calls          :
    'From           : 
    'To             : Save()
    '               : Update()
    '               : Delete()
    'fn. Arguments  :
    '---------------------------------------------------------
    'Parmeter Name      : Data Type : Description
    '---------------------------------------------------------
    'Sqlcommand         :SqlCommand :used to set its parameters
    '========================================================================
    Private Function SetParameter(ByRef Sqlcommand As SqlClient.SqlCommand) As Boolean
        Try
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@EmployeeID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mEmployeeID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@DepartmentID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mDepartmentID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@StartDate", SqlDbType.SmallDateTime)).Value = mDataHandler.DataValue_In(mStartDate, SqlDbType.SmallDateTime)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = mDataHandler.DataValue_In(mRemarks, SqlDbType.VarChar)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(Me.DataBaseUserRelatedID, SqlDbType.Int, True)
            Sqlcommand.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mDataHandler.DataValue_In(mRegComputerID, SqlDbType.Int, True)
        Catch ex As Exception
            mPage.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, mDataBaseUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
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

