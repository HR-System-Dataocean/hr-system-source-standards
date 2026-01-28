Imports Venus.Application.SystemFiles.System

Public Class Clshrs_FormulaSolver

#Region "Public Decleration"

    Private mStrConnectionString As String
    Private mStrTableName As String
    Private mIntID As Integer
    Private mStrErrorMsg As String
    Private mDblOutput As Double
    Private mPassed As Boolean
    Private mEmployeeID As Integer
    Private mFiscalPeriodID As Integer
    Private mDataset As New DataSet
    Private mPage As Global.System.Web.UI.Page
    Private mOverTimePerPeriod As Double
    Private mHolidayHoursperPeriod As Double
    Private mSalaryPricePerHour As Double
    Private mSalaryPricePerDay As Double
    Private mNoOfDaysPerPeriod As Decimal = 0
    Private mNoOfWorkingDays As Double = -1
    Private mBolEndOfContract As Boolean = False
    Private mBolBeginOfContract As Boolean = False
    Private mLevels As Integer = 4
    Private mCurrentLevel As Integer = 1
    Private mChangedTransaction As New Collection
    Private mProjectWorkingUnits As Double
    Private mTotalProjectsWorkingUnits As Double
    Private mWorkOfDays As Boolean
    Private FromDate As DateTime
    Private ToDate As DateTime
    Private FiscFromDate As DateTime
    Private FiscToDate As DateTime
    Private isHasperiod As Integer = 0
    Private mFormulaCalculated As String
    Private mExecutedate As DateTime
    Private mSalarySrc As String = ""
    Private mCProject As Integer = 0
    Private mIsToAttend As Integer = 0
    Private mToAttenddate As DateTime
#End Region

#Region "Public Property"


    Public Property WorkOfDays() As Boolean
        Get
            Return mWorkOfDays
        End Get
        Set(ByVal value As Boolean)
            mWorkOfDays = value
        End Set
    End Property

    Public Property ID() As Integer
        Get
            Return mIntID
        End Get
        Set(ByVal value As Integer)
            mIntID = value
        End Set
    End Property

    Public Property ConnectionString() As String
        Get
            Return mStrConnectionString
        End Get
        Set(ByVal value As String)
            mStrConnectionString = value
        End Set
    End Property

    Public Property TableName() As String
        Get
            Return mStrTableName
        End Get
        Set(ByVal value As String)
            mStrTableName = value
        End Set
    End Property

    Public ReadOnly Property ErorrMsg() As String
        Get
            Return mStrErrorMsg
        End Get
    End Property

    Public ReadOnly Property Output() As Double
        Get
            Return mDblOutput
        End Get
    End Property

    Public ReadOnly Property Passed() As Boolean
        Get
            Return mPassed
        End Get
    End Property

    Public Property EmployeeID() As Integer
        Get
            Return mEmployeeID
        End Get
        Set(ByVal value As Integer)
            mEmployeeID = value
        End Set
    End Property

    Public Property FiscalPeriodID() As Integer
        Get
            Return mFiscalPeriodID
        End Get
        Set(ByVal value As Integer)
            mFiscalPeriodID = value
        End Set
    End Property

    Public Property Page() As Global.System.Web.UI.Page
        Get
            Return mPage
        End Get
        Set(ByVal value As Global.System.Web.UI.Page)
            mPage = value
        End Set
    End Property

    Public Property OverTimePerPeriod() As Double
        Get
            Return mOverTimePerPeriod
        End Get
        Set(ByVal value As Double)
            mOverTimePerPeriod = value
        End Set
    End Property

    Public Property HolidayHoursperPeriod() As Double
        Get
            Return mHolidayHoursperPeriod
        End Get
        Set(ByVal value As Double)
            mHolidayHoursperPeriod = value
        End Set
    End Property

    Public Property SalaryPricePerHour() As Double
        Get
            Return mSalaryPricePerHour
        End Get
        Set(ByVal value As Double)
            mSalaryPricePerHour = value
        End Set
    End Property

    Public Property SalaryPricePerDay() As Double
        Get
            Return mSalaryPricePerDay
        End Get
        Set(ByVal value As Double)
            mSalaryPricePerDay = value
        End Set
    End Property

    Public Property NoOfDaysPerPeriod() As Decimal
        Get
            Return mNoOfDaysPerPeriod
        End Get
        Set(ByVal value As Decimal)
            mNoOfDaysPerPeriod = value
        End Set
    End Property

    Public Property NoOfWorkingDays() As Double
        Get
            Return mNoOfWorkingDays
        End Get
        Set(ByVal value As Double)
            mNoOfWorkingDays = value
        End Set
    End Property

    Public Property BolEndOfContract() As Boolean
        Get
            Return mBolEndOfContract
        End Get
        Set(ByVal value As Boolean)
            mBolEndOfContract = value
        End Set
    End Property

    Public Property BolBeginOfContract() As Boolean
        Get
            Return mBolBeginOfContract
        End Get
        Set(ByVal value As Boolean)
            mBolBeginOfContract = value
        End Set
    End Property

    Public Property Levels() As Integer
        Get
            Return mLevels
        End Get
        Set(ByVal value As Integer)
            mLevels = value
        End Set
    End Property

    Public Property Transaction(ByVal key As String) As Double
        Get
            Return mChangedTransaction.Item(key)
        End Get
        Set(ByVal value As Double)
            mChangedTransaction.Add(value, key)
        End Set
    End Property

    Public Property ProjectWorkingUnits() As Double
        Get
            Return mProjectWorkingUnits
        End Get
        Set(ByVal value As Double)
            mProjectWorkingUnits = value
        End Set
    End Property

    Public Property TotalProjectsWorkingUnits() As Double
        Get
            Return mTotalProjectsWorkingUnits
        End Get
        Set(ByVal value As Double)
            mTotalProjectsWorkingUnits = value
        End Set
    End Property

    Public Property FormulaCalculated() As String
        Get
            Return mFormulaCalculated
        End Get
        Set(ByVal value As String)
            mFormulaCalculated = value
        End Set
    End Property
    Public Property Executedate() As DateTime
        Get
            Return mExecutedate
        End Get
        Set(ByVal value As DateTime)
            mExecutedate = value
        End Set
    End Property

    Public Property SalarySrc() As String
        Get
            Return mSalarySrc
        End Get
        Set(ByVal value As String)
            mSalarySrc = value
        End Set
    End Property

    Public Property CProject() As Integer
        Get
            Return mCProject
        End Get
        Set(ByVal value As Integer)
            mCProject = value
        End Set
    End Property
    Public Property IsToAttend() As Double
        Get
            Return mIsToAttend
        End Get
        Set(ByVal value As Double)
            mIsToAttend = value
        End Set
    End Property

    Public Property ToAttenddate() As DateTime
        Get
            Return mToAttenddate
        End Get
        Set(ByVal value As DateTime)
            mToAttenddate = value
        End Set
    End Property

    'Public Property ToAttenddate() As DateTime
    '    Get
    '        Return mToAttenddate
    '    End Get
    '    Set(ByVal value As DateTime)
    '        mToAttenddate = value
    '    End Set
    'End Property

#End Region

#Region "Public Constructor"

    Public Sub New(ByVal ConnectionString As String, ByVal ObjPage As Global.System.Web.UI.Page)
        mStrConnectionString = ConnectionString
        mPage = ObjPage
        mWorkOfDays = False
    End Sub

#End Region

#Region "Public Functions"

    Private Sub CheckFormula(ByVal Expression As String)
        Try

            If Expression = Nothing Then
                mWorkOfDays = False
            Else

                If Expression.Contains("*<@WUPP@>/<@NDPP@>") Then
                    mWorkOfDays = True
                ElseIf Expression.Contains("/<@NDPP@>*<@WUPP@>") Then
                    mWorkOfDays = True
                ElseIf Expression = "" Then
                    mWorkOfDays = False
                Else
                    mWorkOfDays = False
                End If

            End If

        Catch ex As Exception
            mWorkOfDays = False
        End Try
    End Sub

    Public Function EvaluateExpression(ByVal Expression As String, Optional ByVal IsShift As Integer = 1) As Double

        Dim ArrChar(Expression.Length) As String
        Dim StrCurrentString As String = String.Empty
        Dim IntCurrentIndex As Integer
        Dim Intcounter As Integer

        Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(mPage)
        Dim clsemployee As New Clshrs_Employees(mPage)
        Dim clsCompanies As New Clssys_Companies(mPage)
        Dim clsBranch As New Clssys_Branches(Page)
        clsCompanies.Find("ID = " & ClsFisicalPeriods.MainCompanyID)
        isHasperiod = 1
        If Not ClsFisicalPeriods.Find("ID = " & mFiscalPeriodID) Then
            ClsFisicalPeriods.GetFisicalperiodInfo(DateTime.Now, mFiscalPeriodID, FromDate, ToDate)
            ClsFisicalPeriods.Find("ID = " & mFiscalPeriodID)
            isHasperiod = 0
        End If
        clsemployee.Find("ID = " & EmployeeID)
        clsBranch.Find("ID=" & clsemployee.BranchID)

        FromDate = ClsFisicalPeriods.FromDate
        ToDate = ClsFisicalPeriods.ToDate
        FiscFromDate = ClsFisicalPeriods.FromDate
        FiscToDate = ClsFisicalPeriods.ToDate
        If IsShift = 1 Then
            If clsBranch.PrepareDay > 0 Then
                If clsCompanies.IsHigry = True Then
                    Dim strarr As String() = ClsFisicalPeriods.HFromDate.Split("/")
                    Dim FrmHDate As String = clsBranch.PrepareDay & "/" & IIf(strarr(1) = "01", "12", strarr(1) - 1) & "/" & IIf(strarr(1) = "01", strarr(2) - 1, strarr(2))
                    ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy")
                    FromDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy"), "dd/MM/yyyy")

                    Dim strarr1 As String() = FrmHDate.Split("/")
                    Dim ToHDate As String = clsBranch.PrepareDay - 1 & "/" & IIf(strarr1(1) = "12", "01", strarr1(1) + 1) & "/" & IIf(strarr1(1) = "12", strarr1(2) + 1, strarr1(2))
                    ToDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(ToHDate, "dd/MM/yyyy"), "dd/MM/yyyy")
                Else
                    FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsBranch.PrepareDay)
                    ToDate = FromDate.AddMonths(1).AddDays(-1)
                End If
                If clsBranch.AffectPeriod Then
                    FiscFromDate = FromDate
                    FiscToDate = ToDate
                End If
            Else
                If clsCompanies.PrepareDay > 0 Then
                    If clsCompanies.IsHigry = True Then
                        Dim strarr As String() = ClsFisicalPeriods.HFromDate.Split("/")
                        Dim FrmHDate As String = clsCompanies.PrepareDay & "/" & IIf(strarr(1) = "01", "12", strarr(1) - 1) & "/" & IIf(strarr(1) = "01", strarr(2) - 1, strarr(2))
                        ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy")
                        FromDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(FrmHDate, "dd/MM/yyyy"), "dd/MM/yyyy")

                        Dim strarr1 As String() = FrmHDate.Split("/")
                        Dim ToHDate As String = clsCompanies.PrepareDay - 1 & "/" & IIf(strarr1(1) = "12", "01", strarr1(1) + 1) & "/" & IIf(strarr1(1) = "12", strarr1(2) + 1, strarr1(2))
                        ToDate = ClsDataAcessLayer.FormatGreg(ClsDataAcessLayer.HijriToGreg(ToHDate, "dd/MM/yyyy"), "dd/MM/yyyy")
                    Else
                        FromDate = New DateTime(IIf(FromDate.Month = 1, FromDate.Year - 1, FromDate.Year), FromDate.AddMonths(-1).Month, clsCompanies.PrepareDay)
                        ToDate = FromDate.AddMonths(1).AddDays(-1)
                    End If
                End If
            End If
        End If

        Expression = Replace(Expression, " ", "")

        CheckFormula(Expression)

        If Len(Expression) < 1 Then
            mStrErrorMsg = "The Expression Field is Empty"
            Exit Function
        End If

        For Intcounter = 0 To Expression.Length - 1
            Select Case Expression(Intcounter)
                Case "+", "-", "/", "*", "^", "(", ")"
                    If StrCurrentString.Length > 0 Then
                        ArrChar(IntCurrentIndex) = StrCurrentString
                        IntCurrentIndex += 1
                    End If
                    StrCurrentString = String.Empty
                    ArrChar(IntCurrentIndex) = Expression(Intcounter)
                    IntCurrentIndex += 1
                Case Else
                    StrCurrentString &= Expression(Intcounter)
            End Select
        Next

        If IntCurrentIndex = 0 And StrCurrentString.Length > 0 Then
            ArrChar(IntCurrentIndex) = StrCurrentString
        ElseIf StrCurrentString.Length > 0 Then
            ArrChar(IntCurrentIndex) = StrCurrentString
        Else
            IntCurrentIndex -= 1
        End If
        ReDim Preserve ArrChar(IntCurrentIndex)

        If CheckBraketSyntex(ArrChar) Then
            EvaluateBrackets(ArrChar)
            If CheckFieldSyntex(ArrChar) Then
                EvaluateField(ArrChar)
                If CheckOperandSyntex(ArrChar) Then
                    EvaluatePower(ArrChar)
                    EvaluateMultiplyDivide(ArrChar)
                    EvaluatePlusMinus(ArrChar)
                    Try
                        If ArrChar(0) = "NaN" Or ArrChar(0) = "·Ì” »—ﬁ„" Then
                            ArrChar(0) = 0
                        End If
                    Catch ex As Exception
                    End Try
                    mDblOutput = CDbl(ArrChar(0))
                    mStrErrorMsg = "Complete With No Errors"
                    mPassed = True
                Else
                    mStrErrorMsg = "Syntax Error Check operand"
                    mDblOutput = 0
                    mPassed = False
                End If
            Else
                mStrErrorMsg = "Syntax Error Check Fields"
                mDblOutput = 0
                mPassed = False
            End If
        Else
            mStrErrorMsg = "Syntax Error Check Brakets"
            mDblOutput = 0
            mPassed = False
        End If


    End Function

#End Region

#Region "Private Function"

    Private Function CheckBraketSyntex(ByVal Arr() As String) As Boolean

        Dim IntArrDimension As Integer = Arr.GetUpperBound(0)
        Dim ArryChar(IntArrDimension) As String
        Dim IntCounter As Integer
        Dim IntOpenBraketIndex As Integer
        Dim IntIndex1 As Integer
        Dim IntClosedBraketindex As Integer
        Dim IntIndex2 As Integer

        For IntCounter = 0 To Arr.GetUpperBound(0)
            If Arr(IntCounter) = "(" Then
                If IntIndex2 = IntCounter - 1 Then
                    Return False
                End If
                IntOpenBraketIndex += 1
                IntIndex1 = IntCounter
            ElseIf Arr(IntCounter) = ")" Then
                If IntIndex1 = IntCounter - 1 Then
                    Return False
                End If
                IntClosedBraketindex += 1
                IntIndex2 = IntCounter
            End If
        Next

        If IntOpenBraketIndex = IntClosedBraketindex Then
            Return True
        ElseIf IntOpenBraketIndex = 0 And IntClosedBraketindex = 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function CheckFieldSyntex(ByVal Arr() As String) As Boolean
        Dim IntArrDimension As Integer = Arr.GetUpperBound(0)
        Dim ArryChar(IntArrDimension) As String
        Dim IntCounter As Integer
        Dim IntNumberOfField As Integer
        Dim IntNumberOfOperand As Integer
        Dim BolOperand As Boolean = False

        For IntCounter = 0 To Arr.GetUpperBound(0)

            If Not BolOperand Then
                If (Arr(IntCounter).Substring(0, 1) = "<") Then
                    If IsFieldExpression(Arr(IntCounter)) Then
                        BolOperand = True
                        IntNumberOfField += 1
                    Else
                        Return False
                    End If
                Else
                    If IsNumeric(Arr(IntCounter)) Then
                        BolOperand = True
                        IntNumberOfField += 1
                    Else
                        Return False
                    End If
                End If
            ElseIf BolOperand Then
                If Arr(IntCounter) = "+" Or Arr(IntCounter) = "-" Or Arr(IntCounter) = "*" Or Arr(IntCounter) = "/" Or Arr(IntCounter) = "^" Then
                    BolOperand = False
                    IntNumberOfOperand += 1
                Else
                    Return False
                End If
            End If

        Next

        If IntNumberOfField = IntNumberOfOperand + 1 Then
            Return True
        End If

    End Function

    Private Function CheckOperandSyntex(ByVal Arr() As String) As Boolean
        Dim IntArrDimension As Integer = Arr.GetUpperBound(0)
        Dim ArryChar(IntArrDimension) As String
        Dim IntCounter As Integer
        Dim BolOperand As Boolean = False

        For IntCounter = 0 To Arr.GetUpperBound(0)
            If Not BolOperand Then
                If IsNumeric(Arr(IntCounter)) Then
                    BolOperand = True
                Else
                    Return False
                End If
            ElseIf BolOperand Then
                If Arr(IntCounter) = "+" Or Arr(IntCounter) = "-" Or Arr(IntCounter) = "*" Or Arr(IntCounter) = "/" Or Arr(IntCounter) = "^" Then
                    BolOperand = False
                Else
                    Return False
                End If
            End If
        Next

        Return True
    End Function

    Private Function EvaluateBrackets(ByRef Arr() As String) As Array

        Dim IntArrDimension As Integer = Arr.GetUpperBound(0)
        Dim ArryChar(IntArrDimension) As String
        Dim IntCounter As Integer
        Dim IntOpenBraketIndex As Integer
        Dim IntClosedBraketindex As Integer
        Dim DblValue As Double

        While Arr.GetUpperBound(0) And CheckBracketOperator(Arr)
            If Arr(IntCounter) = "(" Then
                IntOpenBraketIndex = IntCounter
                IntCounter += 1
            ElseIf Arr(IntCounter) = ")" Then
                IntClosedBraketindex = IntCounter
                DblValue = EvaluateSubBraket(Arr, IntOpenBraketIndex, IntClosedBraketindex)
                ReArrangeArrayFromTo(Arr, DblValue, IntOpenBraketIndex, IntClosedBraketindex)
                IntCounter = 0
            Else
                IntCounter += 1
            End If
        End While
        Return Arr

    End Function

    Private Function EvaluateSubBraket(ByVal Arr() As String, ByVal BeginBraketIndex As Integer, ByVal EndBraketIndex As Integer) As Double

        Dim CharArry(Arr.GetUpperBound(0)) As String
        Dim IntCounter As Integer
        Dim IntCurrentIndex As Integer

        For IntCounter = 0 To Arr.GetUpperBound(0)
            If IntCounter > BeginBraketIndex And IntCounter < EndBraketIndex Then
                CharArry(IntCurrentIndex) = Arr(IntCounter)
                IntCurrentIndex += 1
            End If
        Next
        ReDim Preserve CharArry(IntCurrentIndex - 1)
        EvaluateField(CharArry)
        EvaluatePower(CharArry)
        EvaluateMultiplyDivide(CharArry)
        EvaluatePlusMinus(CharArry)
        Return CDbl(CharArry(0))

    End Function

    Private Function EvaluateField(ByRef Arr() As String) As Array
        Dim IntArrDimension As Integer = Arr.GetUpperBound(0)
        Dim ArryChar(IntArrDimension) As String
        Dim IntCounter As Integer
        Dim DblValue As Double

        For IntCounter = 0 To Arr.GetUpperBound(0)
            If Arr(IntCounter).Length < 2 Then
                IntCounter += 1
            ElseIf Arr(IntCounter).Substring(0, 2) = "<$" Then
                DblValue = EvaluateSubField(Arr(IntCounter))
                Arr(IntCounter) = DblValue
                IntCounter += 1
            ElseIf Arr(IntCounter).Substring(0, 2) = "<@" Then
                DblValue = EvaluateSubField2(Arr(IntCounter))
                Arr(IntCounter) = DblValue
                IntCounter += 1
            Else
                IntCounter += 1
            End If
        Next

        Return Arr

    End Function

    Private Function EvaluateSubFieldTest(ByVal Statment As String) As Double
        Dim StrSqlCommand As String
        Dim StrColumnName As String
        Dim DblValue As Double
        Dim IntCheck As Integer
        Dim ObjStringHandler As New Venus.Shared.StringHandler
        Dim ClsContracts As New Clshrs_Contracts(Page)
        Dim IntContractID As Integer
        Dim StrFormulaType As String = String.Empty
        Dim StrSubLevelFormula As String = String.Empty

        Try
            StrColumnName = Statment.Substring(2, Statment.Length - 4)
            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, mFiscalPeriodID)
            StrSqlCommand = "Set Dateformat DMY Select Count(hrs_TransactionsTypes.Code)   From hrs_employees Inner Join hrs_contracts	On hrs_employees.id	= hrs_contracts.employeeid Inner Join hrs_contractsTransactions	On hrs_contracts.id				= hrs_contractsTransactions.Contractid  Inner Join hrs_TransactionsTypes		    On hrs_TransactionsTypes.id	    = hrs_ContractsTransactions.TransactionTypeid Where hrs_contracts.id=" & IntContractID & " And IsNull(hrs_ContractsTransactions.CancelDate,'')=''"
            IntCheck = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
            If IntCheck > 0 Then
                StrSqlCommand = "Set Dateformat DMY Select Amount                          From hrs_employees Inner Join hrs_contracts	On hrs_employees.id	= hrs_contracts.employeeid Inner Join hrs_contractsTransactions	On hrs_contracts.id	            = hrs_contractsTransactions.Contractid  Inner Join hrs_TransactionsTypes			On hrs_TransactionsTypes.id	    = hrs_ContractsTransactions.TransactionTypeid Where hrs_Transactionstypes.Code = '" & StrColumnName & "' And hrs_contracts.id=" & IntContractID
            Else
                StrSqlCommand = "Set Dateformat DMY Select Amount                          From hrs_Employees Inner Join hrs_Contracts	On hrs_Employees.id	= hrs_contracts.employeeid Inner join hrs_GradesSteps			On hrs_Contracts.GradeStepID	= hrs_GradesSteps.id                    Inner Join hrs_GradesStepsTransactions	    On hrs_GradesSteps.ID			= hrs_GradesStepsTransactions.GradeStepID 	  Inner Join hrs_GradesTransactions		On hrs_GradesTransactions.id    = hrs_GradesStepsTransactions.GradeTransactionid  		Inner Join hrs_TransactionsTypes	On hrs_TransactionsTypes.id	= hrs_GradesTransactions.TransactionTypeId Where 	hrs_Transactionstypes.Code = '" & StrColumnName & "' And hrs_contracts.id=" & IntContractID
            End If

            DblValue = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)

            If DblValue = 0 And mLevels >= mCurrentLevel Then
                StrSqlCommand = " Select [FormulaType] from hrs_TransactionsTypes Where Code = '" & StrColumnName & "'"
                If mBolBeginOfContract Then
                    StrSqlCommand = StrSqlCommand.Replace("[FormulaType]", "BeginContractFormula")
                ElseIf mBolEndOfContract Then
                    StrSqlCommand = StrSqlCommand.Replace("[FormulaType]", "EndcontractFormula")
                Else
                    StrSqlCommand = StrSqlCommand.Replace("[FormulaType]", "Formula")
                End If
                mCurrentLevel += 1
                StrSubLevelFormula = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
                EvaluateExpression(StrSubLevelFormula)
                DblValue = Me.Output
            End If

            Return CDbl(DblValue)

        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function EvaluateSubField(ByVal Statment As String) As Double
        Dim StrSqlCommand As String
        Dim StrColumnName As String = Statment.Substring(2, Statment.Length - 4)
        Dim DblValue As Double
        Dim IntCheck As Integer
        Dim ObjStringHandler As New Venus.Shared.StringHandler
        Dim ClsContracts As New Clshrs_Contracts(Page)
        Dim IntContractID As Integer = 0
        If mFiscalPeriodID > 0 Then
            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, mFiscalPeriodID)
        Else
            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, Date.Now)
        End If

        Dim StrFormulaType As String = String.Empty
        Dim StrSubLevelFormula As String = String.Empty
        Try
            StrSqlCommand = " Select IsNull([FormulaType],'') from hrs_TransactionsTypes Where Code = '" & StrColumnName & "'"
            If mBolBeginOfContract Then
                StrSqlCommand = StrSqlCommand.Replace("[FormulaType]", "BeginContractFormula")
            ElseIf mBolEndOfContract Then
                StrSqlCommand = StrSqlCommand.Replace("[FormulaType]", "EndcontractFormula")
            Else
                StrSqlCommand = StrSqlCommand.Replace("[FormulaType]", "Formula")
            End If
            mCurrentLevel += 1
            StrSubLevelFormula = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
            If StrSubLevelFormula = "" Or mLevels <= mCurrentLevel Or StrSubLevelFormula.IndexOf(Statment) <> -1 Then
                If mSalarySrc.Contains("T") And mFiscalPeriodID > 0 Then
                    Dim hrsEmployees As New Clshrs_Employees(mPage)
                    hrsEmployees.Find("ID = " & mEmployeeID)
                    Dim strgetChanges As String = "set dateformat dmy; select Distinct convert(varchar(10),ActiveDate,103) AS Dte from hrs_ContractsTransactions where ActiveDate > '" & hrsEmployees.JoinDate.ToString("dd/MM/yyyy") & "' and ActiveDate >= '" & FromDate.ToString("dd/MM/yyyy") & "' and ActiveDate <= '" & ToDate.ToString("dd/MM/yyyy") & "' and canceldate is null and Active = 1 and contractID in (select ID from hrs_contracts where EmployeeID = " & mEmployeeID & " and canceldate is null)"
                    Dim dtChanges As DataTable = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsContracts.ConnectionString, CommandType.Text, strgetChanges).Tables(0)
                    If Not mSalarySrc.Contains("E") And dtChanges.Rows.Count > 0 And FromDate = FiscFromDate And ToDate = FiscToDate And Not mBolBeginOfContract Then
                        Dim Basedate As Date = FromDate
                        Dim CDblValue As Double = 0
                        Dim CoDblValue As Double = 0
                        Dim daydiff As Integer = 0
                        For i As Integer = 0 To dtChanges.Rows.Count - 1
                            StrSqlCommand = " Set Dateformat DMY; Select top(1) IsNull(Amount,0) From hrs_employees Inner Join hrs_contracts	On hrs_employees.id	= hrs_contracts.employeeid Inner Join hrs_contractsTransactions	On hrs_contracts.id	            = hrs_contractsTransactions.Contractid  Inner Join hrs_TransactionsTypes			On hrs_TransactionsTypes.id	    = hrs_ContractsTransactions.TransactionTypeid Where hrs_Transactionstypes.Code = '" & StrColumnName & "' And hrs_contracts.id=" & IntContractID & " And hrs_contractsTransactions.CancelDate Is Null "
                            StrSqlCommand &= " And hrs_contractsTransactions.ActiveDate <= '" & Basedate.ToString("dd/MM/yyyy") & "' "
                            StrSqlCommand &= " Order By hrs_contractsTransactions.ActiveDate Desc "
                            If mChangedTransaction.Contains(StrColumnName) Then
                                CoDblValue = mChangedTransaction(StrColumnName)
                            Else
                                CoDblValue = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
                            End If

                            daydiff = Convert.ToDateTime(dtChanges.Rows(i)(0)).Subtract(Basedate).Days
                            CDblValue = CoDblValue / (ToDate.Subtract(FromDate).Days + 1) * daydiff
                            Basedate = Convert.ToDateTime(dtChanges.Rows(i)(0))
                            DblValue = DblValue + CDblValue
                        Next i
                        StrSqlCommand = " Set Dateformat DMY; Select top(1) IsNull(Amount,0) From hrs_employees Inner Join hrs_contracts	On hrs_employees.id	= hrs_contracts.employeeid Inner Join hrs_contractsTransactions	On hrs_contracts.id	            = hrs_contractsTransactions.Contractid  Inner Join hrs_TransactionsTypes			On hrs_TransactionsTypes.id	    = hrs_ContractsTransactions.TransactionTypeid Where hrs_Transactionstypes.Code = '" & StrColumnName & "' And hrs_contracts.id=" & IntContractID & " And hrs_contractsTransactions.CancelDate Is Null "
                        StrSqlCommand &= " And hrs_contractsTransactions.ActiveDate <= '" & Basedate.ToString("dd/MM/yyyy") & "' "
                        StrSqlCommand &= " Order By hrs_contractsTransactions.ActiveDate Desc "
                        If mChangedTransaction.Contains(StrColumnName) Then
                            CoDblValue = mChangedTransaction(StrColumnName)
                        Else
                            CoDblValue = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
                        End If
                        daydiff = ToDate.Subtract(Basedate.AddDays(-1)).Days
                        CDblValue = CoDblValue / (ToDate.Subtract(FromDate).Days + 1) * daydiff
                        DblValue = DblValue + CDblValue
                    Else
                        StrSqlCommand = " Set Dateformat DMY; Select top(1) IsNull(Amount,0) From hrs_employees Inner Join hrs_contracts	On hrs_employees.id	= hrs_contracts.employeeid Inner Join hrs_contractsTransactions	On hrs_contracts.id	= hrs_contractsTransactions.Contractid  Inner Join hrs_TransactionsTypes On hrs_TransactionsTypes.id = hrs_ContractsTransactions.TransactionTypeid Where hrs_Transactionstypes.Code = '" & StrColumnName & "' And hrs_contracts.id=" & IntContractID & " And hrs_contractsTransactions.CancelDate Is Null "
                        If mFiscalPeriodID > 0 Then
                            StrSqlCommand &= " And hrs_contractsTransactions.ActiveDate <= '" & FiscToDate.ToString("dd/MM/yyyy") & "' "
                        End If
                        StrSqlCommand &= " Order By hrs_contractsTransactions.ActiveDate Desc "

                        If mChangedTransaction.Contains(StrColumnName) Then
                            DblValue = mChangedTransaction(StrColumnName)
                        Else
                            DblValue = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
                        End If
                    End If
                Else
                    StrSqlCommand = " Set Dateformat DMY; Select top(1) IsNull(Amount,0) From hrs_employees Inner Join hrs_contracts	On hrs_employees.id	= hrs_contracts.employeeid Inner Join hrs_contractsTransactions	On hrs_contracts.id	            = hrs_contractsTransactions.Contractid  Inner Join hrs_TransactionsTypes			On hrs_TransactionsTypes.id	    = hrs_ContractsTransactions.TransactionTypeid Where hrs_Transactionstypes.Code = '" & StrColumnName & "' And hrs_contracts.id=" & IntContractID & " And hrs_contractsTransactions.CancelDate Is Null   "
                    If mFiscalPeriodID > 0 Then
                        StrSqlCommand &= " And hrs_contractsTransactions.ActiveDate <= '" & FiscToDate.ToString("dd/MM/yyyy") & "' "
                    End If
                    StrSqlCommand &= " Order By hrs_contractsTransactions.ActiveDate Desc "

                    If mChangedTransaction.Contains(StrColumnName) Then
                        DblValue = mChangedTransaction(StrColumnName)
                    Else
                        DblValue = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
                    End If
                End If
            Else
                EvaluateExpression(StrSubLevelFormula)
                DblValue = Me.Output
            End If
            Return CDbl(DblValue)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function EvaluateSubField2(ByVal Statment As String) As Double
        Dim StrSqlCommand As String = ""
        Dim StrColumnName As String
        Dim DblValue As Double
        Dim ObjStringHandler As New Venus.Shared.StringHandler
        Try
            StrColumnName = Statment.Substring(2, Statment.Length - 4)
            Select Case StrColumnName
                Case "NDPP"
                    If mNoOfDaysPerPeriod > 0 Then
                        If FormulaCalculated <> "N" Then
                            If mBolBeginOfContract Then
                                Dim hrsEmployees As New Clshrs_Employees(mPage)
                                Dim IntNoOfWorkDays As Integer
                                If hrsEmployees.Find("ID = " & mEmployeeID) Then
                                    Dim dsAttandance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(hrsEmployees.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & mEmployeeID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & mFiscalPeriodID & ",0,NULL,0 ,'" & FiscFromDate.ToString("dd/MM/yyy") & "','" & FiscToDate.ToString("dd/MM/yyy") & "' )")
                                    ' Dim dsAttandance As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(clsEmpClass.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & ClsEmployees.ID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & IntFisicalPeriod & ",0 )")
                                    If dsAttandance.Tables(0).Rows.Count > 0 Then
                                        IntNoOfWorkDays = dsAttandance.Tables(0).Rows(0)("WorkingDays")
                                        Return IntNoOfWorkDays
                                    End If

                                    'If hrsEmployees.JoinDate > FiscFromDate And hrsEmployees.JoinDate <= ToDate And FiscToDate.Day = 31 Then
                                    '    Return FiscToDate.Day - hrsEmployees.JoinDate.Day
                                    'Else
                                    '    Return FiscToDate.Day - hrsEmployees.JoinDate.Day + 1
                                    'End If
                                    '        If hrsEmployees.JoinDate < FiscFromDate Then
                                    '            Dim employeesjoin As New Clshrs_EmployeesJoins(mPage)
                                    '            If employeesjoin.Find("EmployeeID = " & mEmployeeID & " and EndOfServiceDate is null and canceldate is null order by JoinDate ASC") Then
                                    '                Return (FiscToDate.Subtract(employeesjoin.JoinDate).Days + 1) / (FiscToDate.Subtract(FiscFromDate).Days + 1)
                                    '            End If
                                    '        Else
                                    '            ' Return (FiscToDate.Subtract(hrsEmployees.JoinDate).Days + 1) / (FiscToDate.Subtract(FiscFromDate).Days + 1)
                                    '            'New code

                                    '            '========
                                    '            Dim NewFiscToDate As Date = FiscToDate
                                    '            If mNoOfDaysPerPeriod = 30 Then
                                    '                If FiscToDate.Day = 31 Then
                                    '                    NewFiscToDate = FiscToDate.AddDays(-1)
                                    '                End If

                                    '                If hrsEmployees.JoinDate.Day = 1 Then
                                    '                    Return 1
                                    '                Else
                                    '                    Return (NewFiscToDate.Subtract(hrsEmployees.JoinDate).Days + 1) / mNoOfDaysPerPeriod
                                    '                End If


                                    '            Else
                                    '                Return (FiscToDate.Subtract(hrsEmployees.JoinDate).Days + 1) / mNoOfDaysPerPeriod
                                    '            End If




                                    '        End If
                                End If
                                '    Return 0
                                'Else

                                '    Dim hrsEmployees As New Clshrs_Employees(mPage)
                                '    hrsEmployees.Find("ID = " & mEmployeeID)
                                '    StrSqlCommand = "set dateformat dmy; select Top 1 id from hrs_EmployeesVacations where EmployeeID=" & mEmployeeID & " and  CancelDate is null  and ActualEndDate >'" & FiscFromDate & "' and ActualEndDate <='" & FiscToDate & "'  and VacationTypeID in(select ID from hrs_VacationsTypes where IsPaid in(1,-1))"
                                '    Dim IsReutnfromVaction As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
                                '    If IsReutnfromVaction > 0 Then

                                '        Return FiscToDate.Day
                                '    End If

                                '    Return mNoOfDaysPerPeriod
                            End If
                            Return mNoOfDaysPerPeriod
                        Else
                            Return mNoOfDaysPerPeriod
                        End If
                    Else
                        If FormulaCalculated <> "N" Then
                            Return FiscToDate.Day
                        End If

                        'If isHasperiod = 0 Then
                        '    Return 30
                        'End If
                        'Dim hrsEmployees As New Clshrs_Employees(mPage)
                        'hrsEmployees.Find("ID = " & mEmployeeID)
                        'StrSqlCommand = "set dateformat dmy; select Top 1 id from hrs_EmployeesVacations where EmployeeID=" & mEmployeeID & " and  CancelDate is null  and ActualEndDate >'" & FiscFromDate & "' and ActualEndDate <='" & FiscToDate & "' and ActualStartDate not between '" & FiscFromDate & "' and '" & FiscToDate & "' and VacationTypeID in(select ID from hrs_VacationsTypes where IsPaid in(1,-1))"
                        'Dim IsReutnfromVaction As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)

                        'If mBolBeginOfContract Then

                        '    If hrsEmployees.Find("ID = " & mEmployeeID) Then
                        '        If hrsEmployees.JoinDate < FromDate Then
                        '            Dim employeesjoin As New Clshrs_EmployeesJoins(mPage)
                        '            If employeesjoin.Find("EmployeeID = " & mEmployeeID & " and EndOfServiceDate is null and canceldate is null order by JoinDate ASC") Then
                        '                Return 30 ' (FiscToDate.Subtract(employeesjoin.JoinDate).Days + 1) / (ToDate.Subtract(FromDate).Days + 1)
                        '            End If
                        '        Else
                        '            Return FiscToDate.Day - hrsEmployees.JoinDate.Day + 1 '(FiscToDate.Subtract(hrsEmployees.JoinDate).Days + 1) / (ToDate.Subtract(FromDate).Days + 1)
                        '        End If
                        '    End If
                        '    Return 0
                        'ElseIf IsReutnfromVaction > 0 Then

                        '    Return FiscToDate.Day

                        'Else
                        '    'Return ToDate.Subtract(FromDate).Days + 1
                        '    Return FiscToDate.Day 'ToDate.Subtract(FromDate).Days + 1
                        '    'Return 30
                        'End If
                    End If
                Case "NHPP"
                    StrSqlCommand = "Select NoOfHoursPerPeriod From hrs_Employees Inner Join hrs_Contracts On hrs_Contracts.EmployeeID = hrs_Employees.id Inner Join hrs_EmployeesClasses On hrs_Contracts.EmployeeClassid = hrs_employeesClasses.id Where hrs_Employees.id  = " & mEmployeeID & " And hrs_Contracts.CancelDate IS null "
                Case "WHPD"
                    StrSqlCommand = "Select WorkHoursPerDay From hrs_Employees Inner Join hrs_Contracts On hrs_Contracts.EmployeeID = hrs_Employees.id Inner Join hrs_EmployeesClasses On hrs_Contracts.EmployeeClassid = hrs_employeesClasses.id Where hrs_Employees.id = " & mEmployeeID & " And hrs_Contracts.CancelDate IS null and GetDate() between hrs_Contracts.startDate and hrs_Contracts.EndDate"
                Case "OVF"
                    StrSqlCommand = "Select IsNull(OvertimeFactor,0) From hrs_Employees Inner Join hrs_Contracts On hrs_Contracts.EmployeeID = hrs_Employees.id Inner Join hrs_EmployeesClasses On hrs_Contracts.EmployeeClassid	= hrs_employeesClasses.id Where hrs_Employees.id = " & mEmployeeID & " And hrs_Contracts.CancelDate IS null and GetDate() between hrs_Contracts.startDate and IsNull(hrs_Contracts.EndDate,dateadd(dd,1,GetDate()))"
                Case "HOF"
                    StrSqlCommand = "Select IsNull(HolidayFactor,0) From hrs_Employees  Inner Join hrs_Contracts On hrs_Contracts.EmployeeID = hrs_Employees.id Inner Join hrs_EmployeesClasses On hrs_Contracts.EmployeeClassid = hrs_employeesClasses.id Where hrs_Employees.id = " & mEmployeeID & " And hrs_Contracts.CancelDate IS null and GetDate() between hrs_Contracts.startDate and IsNull(hrs_Contracts.EndDate,dateadd(dd,1,GetDate()))"
                Case "TPWU"
                    Return mTotalProjectsWorkingUnits
                Case "PWU"
                    Return mProjectWorkingUnits
                Case "WUPP"
                    If mNoOfWorkingDays > -1 Then
                        Return mNoOfWorkingDays
                    Else
                        StrSqlCommand = "Select IsNull(FinancialWorkingUnits,0) From hrs_EmployeesTransactions Where EmployeeID = " & mEmployeeID & " And FiscalYearPeriodID= " & mFiscalPeriodID & " Order By ID DESC "
                    End If
                Case "OHPP"
                    If mOverTimePerPeriod > 0 Then
                        Return mOverTimePerPeriod
                    Else
                        StrSqlCommand = "Select Sum(IsNull(OvertimeHours,0))  As OverTime From hrs_EmployeesTransactions Inner Join hrs_EmployeesTransactionsProjects On hrs_EmployeesTransactions.id = hrs_EmployeesTransactionsProjects.EmployeeTransactionID Where EmployeeID =  " & mEmployeeID & " And FiscalYearPeriodID= " & mFiscalPeriodID & " Order By ID Desc "
                    End If
                Case "RUPP"
                    If FormulaCalculated <> "N" Then
                        StrSqlCommand = "select case when (select case when DATEDIFF(Day,(select FromDate from sys_FiscalYearsPeriods where ID = " & mFiscalPeriodID & "),(select JoinDate from hrs_employees where ID = " & mEmployeeID & ")) < 0 then 0 else DATEDIFF(Day,(select FromDate from sys_FiscalYearsPeriods where ID = " & mFiscalPeriodID & "),(select JoinDate from hrs_employees where ID = " & mEmployeeID & ")) end - case when (30 - (select DATEDIFF(Day,FromDate,DATEADD(Day,1,ToDate))  from sys_FiscalYearsPeriods where ID = " & mFiscalPeriodID & ")) < 0 then 0 else (select 30 - (select DATEDIFF(Day,FromDate,DATEADD(Day,1,ToDate))  from sys_FiscalYearsPeriods where ID = " & mFiscalPeriodID & ")) end) < 0 then 0 else (select case when DATEDIFF(Day,(select FromDate from sys_FiscalYearsPeriods where ID = " & mFiscalPeriodID & "),(select JoinDate from hrs_employees where ID = " & mEmployeeID & ")) < 0 then 0 else DATEDIFF(Day,(select FromDate from sys_FiscalYearsPeriods where ID = " & mFiscalPeriodID & "),(select JoinDate from hrs_employees where ID = " & mEmployeeID & ")) end + case when (30 - (select DATEDIFF(Day,FromDate,DATEADD(Day,1,ToDate))  from sys_FiscalYearsPeriods where ID = " & mFiscalPeriodID & ")) < 0 then 0 else (select 30 - (select DATEDIFF(Day,FromDate,DATEADD(Day,1,ToDate))  from sys_FiscalYearsPeriods where ID = " & mFiscalPeriodID & ")) end) end"
                    End If
                Case "AVDPP"
                    If FormulaCalculated <> "N" Then
                        Dim AVDays As Int32 = 0
                        Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(mPage)
                        ClsFisicalPeriods.Find("ID = " & mFiscalPeriodID)

                        Dim CntDays As Integer = FiscToDate.Subtract(FiscFromDate).Days + 2
                        Dim IsToEnd As Boolean = False
                        For CounDays As Integer = 0 To CntDays
                            Dim OperDate As DateTime = FiscFromDate.AddDays(CounDays)
                            Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(mPage)
                            hrsEmployeesVacations.Find("EmployeeID = " & mEmployeeID & " and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) >= CONVERT(date,ActualStartDate,103) and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) < CONVERT(date,isnull(ActualEndDate,'01/01/2050'),103) and VacationTypeID in (select ID from hrs_VacationsTypes where IsAnnual = 1)")
                            If (hrsEmployeesVacations.DataSet.Tables(0).Rows.Count > 0) Then
                                If OperDate > FiscToDate Then
                                    IsToEnd = True
                                    Continue For
                                Else
                                    AVDays = AVDays + 1
                                End If
                            End If
                        Next

                        Dim dsAttandance As DataSet
                        If IsToAttend = 0 Then
                            ' dsAttandance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsFisicalPeriods.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & mEmployeeID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & mFiscalPeriodID & ",0,NULL,0 )")
                            dsAttandance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsFisicalPeriods.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & mEmployeeID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & mFiscalPeriodID & ",0,NULL,0 ,'" & FiscFromDate.ToString("dd/MM/yyy") & "','" & FiscToDate.ToString("dd/MM/yyy") & "' )")
                        Else
                            'dsAttandance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsFisicalPeriods.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & mEmployeeID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & mFiscalPeriodID & "," & IsToAttend & ",'" & ToAttenddate.ToString("dd/MM/yyy") & "',0 )")
                            dsAttandance = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsFisicalPeriods.ConnectionString, CommandType.Text, "set dateformat dmy  select * from dbo.AttendanceEffects(" & mEmployeeID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & mFiscalPeriodID & "," & IsToAttend & ",'" & ToAttenddate.ToString("dd/MM/yyy") & "',0 ,'" & FiscFromDate.ToString("dd/MM/yyy") & "','" & FiscToDate.ToString("dd/MM/yyy") & "' )")
                        End If

                        If dsAttandance.Tables(0).Rows.Count > 0 Then
                            ' IntNoOfWorkDays = dsAttandance.Tables(0).Rows(0)("WorkingDays")
                            ' intAbsent = dsAttandance.Tables(0).Rows(0)("AbsentDays")
                            AVDays = dsAttandance.Tables(0).Rows(0)("vactiondays")
                        End If

                        ' Return True
                        'If IsToEnd Then

                        '    'If FiscToDate.Subtract(FiscFromDate).Days <> ToDate.Subtract(FromDate).Days Then
                        '    '    If FiscToDate.Subtract(FiscFromDate).Days > ToDate.Subtract(FromDate).Days Then
                        '    '        AVDays = AVDays - 1
                        '    '    ElseIf FiscToDate.Subtract(FiscFromDate).Days < ToDate.Subtract(FromDate).Days Then
                        '    '        If mNoOfDaysPerPeriod > 0 Then
                        '    '        Else
                        '    '            AVDays = AVDays + 1
                        '    '        End If
                        '    '    End If
                        '    'Else
                        '    '    If mNoOfDaysPerPeriod > 0 And FiscToDate.Subtract(FiscFromDate).Days + 1 > mNoOfDaysPerPeriod Then
                        '    '        AVDays = AVDays - 1
                        '    '    End If
                        '    'End If
                        '    Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(mPage)
                        '    hrsEmployeesVacations.Find("EmployeeID = " & mEmployeeID & " and CONVERT(date,ActualStartDate,103)>= CONVERT(date,'" & FiscFromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,ActualStartDate,103)<= CONVERT(date,'" & FiscToDate.ToString("dd/MM/yyyy") & "',103)  and VacationTypeID in (select ID from hrs_VacationsTypes where IsAnnual = 1)")
                        '    If hrsEmployeesVacations.ID > 0 Then
                        '        If NoOfDaysPerPeriod = 30 Then
                        '            If hrsEmployeesVacations.ActualStartDate.Day = 31 Then
                        '                AVDays = 31 - hrsEmployeesVacations.ActualStartDate.Day + 1
                        '            Else
                        '                '  AVDays = 30 - hrsEmployeesVacations.ActualStartDate.Day + 1
                        '            End If
                        '        End If

                        '        If NoOfDaysPerPeriod <> 30 Then
                        '            If FiscToDate.Day = 31 Then
                        '                ' AVDays = FiscToDate.Subtract(hrsEmployeesVacations.ActualStartDate).Days
                        '            End If
                        '            AVDays = FiscToDate.Subtract(hrsEmployeesVacations.ActualStartDate).Days + 1
                        '        End If

                        '    End If


                        'End If
                        Return IIf(AVDays < 0, 0, AVDays)
                    End If
                Case "AVRPP"
                    If FormulaCalculated <> "N" Then
                        Dim ClsFisicalPeriods As New Clssys_FiscalYearsPeriods(mPage)
                        ClsFisicalPeriods.Find("ID = " & mFiscalPeriodID)

                        Dim FromDateOrg As DateTime = ClsFisicalPeriods.FromDate
                        Dim AVDays As Int32 = 0
                        Dim hrsEmployeesVacations As New Clshrs_EmployeesVacations(mPage)
                        hrsEmployeesVacations.Find("EmployeeID = " & mEmployeeID & " and CONVERT(date,isnull(ActualEndDate,'01/01/2050'),103) between CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,'" & FromDateOrg.ToString("dd/MM/yyyy") & "',103) and VacationTypeID in (select ID from hrs_VacationsTypes where IsAnnual = 1)")
                        If (hrsEmployeesVacations.DataSet.Tables(0).Rows.Count > 0) Then
                            If ClsFisicalPeriods.Find("CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) between CONVERT(date,isnull(FromDate,'01/01/2050'),103) and CONVERT(date,isnull(ToDate,'01/01/2050'),103)") Then
                                Dim CntDays1 As Integer = ClsFisicalPeriods.ToDate.Subtract(ClsFisicalPeriods.FromDate).Days + 1
                                AVDays = 30 - IIf(CntDays1 > 30, 30, CntDays1)
                            End If
                            Dim CntDays As Integer = FromDateOrg.Subtract(FromDate).Days + 1
                            For CounDays As Integer = 0 To CntDays
                                Dim OperDate As DateTime = FromDate.AddDays(CounDays)
                                If OperDate >= FromDateOrg Then
                                    Continue For
                                Else
                                    hrsEmployeesVacations.Find("EmployeeID = " & mEmployeeID & " and CONVERT(date,'" & OperDate.ToString("dd/MM/yyyy") & "',103) between CONVERT(date,ActualStartDate,103) and CONVERT(date,isnull(ActualEndDate,'01/01/2050'),103) and VacationTypeID in (select ID from hrs_VacationsTypes where IsAnnual = 1)")
                                    If (hrsEmployeesVacations.DataSet.Tables(0).Rows.Count > 0) Then
                                    Else
                                        AVDays = AVDays + 1
                                    End If
                                End If
                            Next
                        End If
                        Return AVDays
                    End If
                Case "HUPP"
                    Return CDbl(GetVacationsDays(mEmployeeID, mFiscalPeriodID))
                Case "OHPH"
                    Return mHolidayHoursperPeriod
                Case "SPPH"
                    Return mSalaryPricePerHour
                Case "SPPD"
                    Return mSalaryPricePerDay
                Case "EOB"
                    If FormulaCalculated <> "N" Then
                        Dim EOBFormula As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Select IsNull(EOBFormula,'') From hrs_Employees Inner Join hrs_Contracts On hrs_Contracts.EmployeeID = hrs_Employees.id Inner Join hrs_EmployeesClasses On hrs_Contracts.EmployeeClassid	= hrs_employeesClasses.id Where hrs_Employees.id = " & mEmployeeID & " And GetDate() between hrs_Contracts.startDate and IsNull(hrs_Contracts.EndDate,dateadd(dd,1,GetDate()))")
                        EvaluateExpression(EOBFormula)
                        Dim EOBvalue As Double = Me.Output
                        Return CDbl(EOBvalue)
                    End If
                Case "OTF"
                    If FormulaCalculated <> "N" Then
                        Dim EOBFormula As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Select IsNull(OvertimeFormula,'') From hrs_Employees Inner Join hrs_Contracts On hrs_Contracts.EmployeeID = hrs_Employees.id Inner Join hrs_EmployeesClasses On hrs_Contracts.EmployeeClassid	= hrs_employeesClasses.id Where hrs_Employees.id = " & mEmployeeID & " And GetDate() between hrs_Contracts.startDate and IsNull(hrs_Contracts.EndDate,dateadd(dd,1,GetDate()))")
                        EvaluateExpression(EOBFormula)
                        Dim EOBvalue As Double = Me.Output
                        Return CDbl(EOBvalue)
                    End If
                Case "HDF"
                    If FormulaCalculated <> "N" Then
                        Dim EOBFormula As String = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Select IsNull(HolidayFormula,'') From hrs_Employees Inner Join hrs_Contracts On hrs_Contracts.EmployeeID = hrs_Employees.id Inner Join hrs_EmployeesClasses On hrs_Contracts.EmployeeClassid	= hrs_employeesClasses.id Where hrs_Employees.id = " & mEmployeeID & " And GetDate() between hrs_Contracts.startDate and IsNull(hrs_Contracts.EndDate,dateadd(dd,1,GetDate()))")
                        EvaluateExpression(EOBFormula)
                        Dim EOBvalue As Double = Me.Output
                        Return CDbl(EOBvalue)
                    End If
                Case "NOD"
                    StrSqlCommand = "Select count(ID) From hrs_EmployeesDependants where CancelDate is null and EmployeeID = " & mEmployeeID
                Case "FODHI"
                    StrSqlCommand = "Select isnull(sum(InsurancePercentage),0)/100 From hrs_EmployeesDependants where InsuranceCovered = 1 and CancelDate is null and EmployeeID = " & mEmployeeID
                Case "FODT"
                    StrSqlCommand = "Select isnull(sum(TicketPercentage),0)/100 From hrs_EmployeesDependants where TicketCovered = 1 and CancelDate is null and EmployeeID = " & mEmployeeID
                Case "OTIV"
                    If FormulaCalculated <> "N" Then
                        Dim ClsContracts As New Clshrs_Contracts(Page)
                        Dim IntContractID As Integer = 0
                        If mFiscalPeriodID > 0 Then
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, mFiscalPeriodID)
                        Else
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, Date.Now)
                        End If
                        Dim mCost As Double = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, CommandType.Text, "Set DateFormat DMY; Select [dbo].[hrs_GetTicketValue](" & ClsContracts.ID & ",'" & mExecutedate.ToString("dd/MM/yyyy") & "')")
                        Return mCost
                    End If
                Case "TIV"
                    If FormulaCalculated <> "N" Then
                        Dim ClsContracts As New Clshrs_Contracts(Page)
                        Dim IntContractID As Integer = 0
                        If mFiscalPeriodID > 0 Then
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, mFiscalPeriodID)
                        Else
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, Date.Now)
                        End If
                        Dim intVacType As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Select Top 1 ID From hrs_VacationsTypes Where IsAnnual =1 And CancelDate Is Null")
                        Dim dsContractVac As New Data.DataSet
                        dsContractVac = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ClsContracts.ConnectionString, "GetDurationDaysForPeriod", ClsContracts.ID, intVacType, mExecutedate.ToString("dd/MM/yyyy"), False)
                        Dim mRequired As Integer = dsContractVac.Tables(0).Rows(0).Item("RequiredWorkingMonths")
                        Dim mDurationDays As Integer = dsContractVac.Tables(0).Rows(0).Item("DurationDays")
                        Dim mCost As Double = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, CommandType.Text, "Set DateFormat DMY; Select [dbo].[hrs_GetTicketValue](" & ClsContracts.ID & ",'" & mExecutedate.ToString("dd/MM/yyyy") & "')")
                        Dim OneCost As Double = mCost
                        Dim mCnt As Double = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, CommandType.Text, "Select isnull(sum(TicketPercentage),0)/100 From hrs_EmployeesDependants where TicketCovered = 1 and CancelDate is null and EmployeeID = " & mEmployeeID)
                        Return (OneCost + (OneCost * mCnt)) * (mNoOfWorkingDays / mDurationDays)
                    End If
                Case "CMO"
                    Dim ClsContracts As New Clshrs_Contracts(Page)
                    Dim IntContractID As Integer = 0
                    If mFiscalPeriodID > 0 Then
                        IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, mFiscalPeriodID)
                    Else
                        IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, Date.Now)
                    End If
                    StrSqlCommand = "Select isnull(ContractPeriod,12) From hrs_Contracts where ID = " & IntContractID & " and EmployeeID = " & mEmployeeID
                Case "TIC"
                    Try
                        Dim ClsContracts As New Clshrs_Contracts(Page)
                        Dim clsCompanies As New Clssys_Companies(mPage)
                        clsCompanies.Find("ID = " & clsCompanies.MainCompanyID)

                        Dim IntContractID As Integer = 0
                        If mFiscalPeriodID > 0 Then
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, mFiscalPeriodID)
                        Else
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, Date.Now)
                        End If
                        Dim intVacType As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Select Top 1 ID From hrs_VacationsTypes Where IsAnnual =1 And CancelDate Is Null")
                        Dim dsContractVac As New Data.DataSet
                        dsContractVac = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mStrConnectionString, "GetDurationDaysForPeriod", IntContractID, intVacType, ToDate.ToString("dd/MM/yyyy"), False)
                        Dim mRequired As Integer = dsContractVac.Tables(0).Rows(0).Item("RequiredWorkingMonths")
                        Dim mDurationDays As Integer = dsContractVac.Tables(0).Rows(0).Item("DurationDays")
                        Dim mCost As Double = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Set DateFormat DMY; Select [dbo].[hrs_GetTicketValue](" & IntContractID & ",'" & ToDate.ToString("dd/MM/yyyy") & "')")
                        Dim mCnt As Double = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsContracts.ConnectionString, CommandType.Text, "Select isnull(sum(TicketPercentage),0)/100 From hrs_EmployeesDependants where TicketCovered = 1 and CancelDate is null and EmployeeID = " & mEmployeeID)
                        Return CDbl(((mCost + (mCnt * mCost)) / (mRequired + mDurationDays)) * 360)
                    Catch ex As Exception
                        Return 0
                    End Try
                Case "VDD"
                    Try
                        Dim ClsContracts As New Clshrs_Contracts(Page)
                        Dim clsCompanies As New Clssys_Companies(mPage)
                        clsCompanies.Find("ID = " & clsCompanies.MainCompanyID)

                        Dim IntContractID As Integer = 0
                        If mFiscalPeriodID > 0 Then
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, mFiscalPeriodID)
                        Else
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, Date.Now)
                        End If
                        Dim intVacType As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Select Top 1 ID From hrs_VacationsTypes Where IsAnnual =1 And CancelDate Is Null")
                        Dim dsContractVac As New Data.DataSet
                        dsContractVac = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mStrConnectionString, "GetDurationDaysForPeriod", IntContractID, intVacType, ToDate.ToString("dd/MM/yyyy"), False)
                        Dim mRequired As Integer = dsContractVac.Tables(0).Rows(0).Item("RequiredWorkingMonths")
                        Dim mDurationDays As Integer = dsContractVac.Tables(0).Rows(0).Item("DurationDays")
                        Return mDurationDays
                    Catch ex As Exception
                        Return 0
                    End Try

                Case "HIC"
                    Try
                        Dim ClsContracts As New Clshrs_Contracts(Page)
                        Dim IntContractID As Integer = 0
                        If mFiscalPeriodID > 0 Then
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, mFiscalPeriodID)
                        Else
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, Date.Now)
                        End If
                        StrSqlCommand = "Set DateFormat DMY; Select top 1 IsNull(CompanyAmt,0) From hrs_HIPolicyContract where contractID = " & IntContractID & " and ActiveDate <=  '" & ToDate.ToString("dd/MM/yyyy") & "' order by ActiveDate DESC"
                    Catch ex As Exception
                        Return 0
                    End Try
                Case "AVC"
                    Try
                        Dim ClsContracts As New Clshrs_Contracts(Page)
                        Dim clsCompanies As New Clssys_Companies(mPage)
                        clsCompanies.Find("ID = " & clsCompanies.MainCompanyID)

                        Dim IntContractID As Integer = 0
                        If mFiscalPeriodID > 0 Then
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, mFiscalPeriodID)
                        Else
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, Date.Now)
                        End If
                        Dim mCost As Double = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Set DateFormat DMY; Select ((dbo.hrs_GetVacationValue(" & IntContractID & ",'" & ToDate.ToString("dd/MM/yyyy") & "')  / 30) * dbo.hrs_GetEmpLastVacDays(" & mEmployeeID & "," & IntContractID & ",'" & ToDate.ToString("dd/MM/yyyy") & "'))")
                        Return CDbl(mCost)
                    Catch ex As Exception
                        Return 0
                    End Try
                Case "ESC"
                    Try
                        Dim IntYear, IntMonth, IntDays, IntTotalDays, IntUnPaid As Integer
                        Dim DblAmount As Double = 0
                        Dim ClsEndOfService As New Clshrs_EndOfServices(Page)
                        Dim ClsContracts As New Clshrs_Contracts(Page)
                        Dim IntContractID As Integer = 0
                        If mFiscalPeriodID > 0 Then
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, mFiscalPeriodID)
                        Else
                            IntContractID = ClsContracts.ContractValidatoinId(mEmployeeID, Date.Now)
                        End If
                        Dim intEOSTypeID As Integer = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Select Top 1 ID From hrs_EndOfServices ")
                        Dim JoinDate As Date = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Select JoinDate From hrs_Employees Where ID= " & mEmployeeID)
                        Dim EndDate As Date = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, "Select ToDate From sys_FiscalYearsPeriods Where ID= " & mFiscalPeriodID)

                        ClsEndOfService.GetEmployeeWorkingDays(mEmployeeID, IntContractID, IntYear, IntMonth, IntDays, IntTotalDays, IntUnPaid, EndDate, JoinDate)
                        ClsEndOfService.CalculateEndofServiceCost(mEmployeeID, IntTotalDays, intEOSTypeID, DblAmount)
                        Return Math.Round(DblAmount, 2)
                    Catch ex As Exception
                        Return 0
                    End Try
                Case "PSick"
                    If FormulaCalculated <> "N" Then
                        StrSqlCommand = "select sum(SickPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & mEmployeeID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    End If
                Case "PLeave"
                    If FormulaCalculated <> "N" Then
                        StrSqlCommand = "select sum(LeavePunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & mEmployeeID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    End If
                Case "PDelay"
                    If FormulaCalculated <> "N" Then
                        StrSqlCommand = "select sum(LatPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & mEmployeeID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    End If
                Case "PAbsent"
                    If FormulaCalculated <> "N" Then
                        StrSqlCommand = "select sum(AbsentPunishment * SalaryPerDay) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & mEmployeeID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    End If
                Case "POtime"
                    If FormulaCalculated <> "N" Then
                        StrSqlCommand = "select sum(OTFactor * Overtime * SalaryPerHour) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & mEmployeeID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    End If
                Case "PHOtime"
                    If FormulaCalculated <> "N" Then
                        StrSqlCommand = "select sum(HOTFactor * HolidayHours * SalaryPerHour) from Att_AttendancePreparationProjects where TrnsID in (select ID from Att_AttendancePreparationDetails where EmployeeID = " & mEmployeeID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103))"
                    End If
                Case "AbsentDays"
                    If FormulaCalculated <> "N" Then
                        StrSqlCommand = "select sum(cast (isabsent as int))  from Att_AttendancePreparationDetails where EmployeeID = " & mEmployeeID & " and CONVERT(date,GAttendDate,103) >= CONVERT(date,'" & FromDate.ToString("dd/MM/yyyy") & "',103) and CONVERT(date,GAttendDate,103) <= CONVERT(date,'" & ToDate.ToString("dd/MM/yyyy") & "',103)"
                    End If

                Case "NV"
                    If FormulaCalculated <> "N" Then
                        StrSqlCommand = "select top 1 isnull(FinancialWorkingUnits,0)  from hrs_EmployeesTransactions t inner join hrs_employees e on e.id=t.EmployeeID inner join sys_FiscalYearsPeriods f on f.id=t.FiscalYearPeriodID where e.ID= " & mEmployeeID & " and PrepareType in('NV','NP') and f.ID=" & mFiscalPeriodID
                    End If

                Case "WDPP"


                    If IsToAttend = 0 Then
                        'StrSqlCommand = "set dateformat dmy  select WorkingDays from dbo.AttendanceEffects(" & mEmployeeID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & mFiscalPeriodID & ",0,NULL,0 )"
                        StrSqlCommand = "set dateformat dmy  select WorkingDays from dbo.AttendanceEffects(" & mEmployeeID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & mFiscalPeriodID & ",0,NULL,0 ,'" & FiscFromDate.ToString("dd/MM/yyy") & "','" & FiscToDate.ToString("dd/MM/yyy") & "' )"
                    Else
                        'StrSqlCommand = "set dateformat dmy  select WorkingDays from dbo.AttendanceEffects(" & mEmployeeID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & mFiscalPeriodID & "," & IsToAttend & ",'" & ToAttenddate.ToString("dd/MM/yyy") & "',0 )"
                        StrSqlCommand = "set dateformat dmy  select WorkingDays from dbo.AttendanceEffects(" & mEmployeeID & ",'" & FromDate.ToString("dd/MM/yyy") & "','" & ToDate.ToString("dd/MM/yyy") & "'," & mFiscalPeriodID & "," & IsToAttend & ",'" & ToAttenddate.ToString("dd/MM/yyy") & "',0 ,'" & FiscFromDate.ToString("dd/MM/yyy") & "','" & FiscToDate.ToString("dd/MM/yyy") & "' )"
                    End If


            End Select

            DblValue = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mStrConnectionString, CommandType.Text, StrSqlCommand)
            Return CDbl(DblValue)

        Catch ex As Exception
            Return 0
        End Try

    End Function
    Private Function EvaluatePower(ByRef Arr() As String) As Array

        Dim IntArrDimension As Integer = Arr.GetUpperBound(0)
        Dim ArryChar(IntArrDimension) As String
        Dim DblValue As Double
        Dim IntCounter As Integer


        While Arr.GetUpperBound(0) >= 2 And CheckPowerOperator(Arr)
            If Arr(IntCounter + 1) = "^" Then
                DblValue = EvaluateOperator(Arr(IntCounter), Arr(IntCounter + 2), Arr(IntCounter + 1))
                ReArrangeArray(Arr, DblValue, IntCounter)
            Else
                IntCounter += 2
            End If
        End While
        Return Arr

    End Function

    Private Function EvaluateMultiplyDivide(ByRef Arr() As String) As Array
        Dim IntArrDimension As Integer = Arr.GetUpperBound(0)
        Dim ArryChar(IntArrDimension) As String
        Dim DblValue As Double
        Dim IntCounter As Integer


        While Arr.GetUpperBound(0) >= 2 And CheckMultiplyDividOperator(Arr)
            If Arr(IntCounter + 1) = "*" Or Arr(IntCounter + 1) = "/" Then
                DblValue = EvaluateOperator(Arr(IntCounter), Arr(IntCounter + 2), Arr(IntCounter + 1))
                ReArrangeArray(Arr, DblValue, IntCounter)
            Else
                IntCounter += 2
            End If
        End While
        Return Arr

    End Function

    Private Function EvaluatePlusMinus(ByRef Arr() As String) As Array

        Dim IntArrDimension As Integer = Arr.GetUpperBound(0)
        Dim ArryChar(IntArrDimension) As String
        Dim DblValue As Double
        Dim IntCounter As Integer


        While Arr.GetUpperBound(0) >= 2 And CheckPlusMinusOperator(Arr)
            If Arr(IntCounter + 1) = "+" Or Arr(IntCounter + 1) = "-" Then
                DblValue = EvaluateOperator(Arr(IntCounter), Arr(IntCounter + 2), Arr(IntCounter + 1))
                ReArrangeArray(Arr, DblValue, IntCounter)
            Else
                IntCounter += 2
            End If
        End While
        Return Arr

    End Function

    Private Function EvaluateOperator(ByVal value1 As String, ByVal Value2 As String, ByVal StrOperator As String) As Double
        Select Case StrOperator
            Case "+"
                Return CDbl(value1) + CDbl(Value2)
            Case "-"
                Return CDbl(value1) - CDbl(Value2)
            Case "*"
                Return CDbl(value1) * CDbl(Value2)
            Case "/"
                Return CDbl(value1) / CDbl(Value2)
            Case "^"
                Return CDbl(value1) ^ CDbl(Value2)
        End Select
    End Function

    Private Function ReArrangeArray(ByRef Arr() As String, ByVal MyValue As String, ByVal index As Integer) As Boolean

        Dim ArrChar(Arr.GetUpperBound(0) - 2) As String
        Dim IntCounter As Integer
        Dim IntCurrentIndex As Integer

        For IntCounter = 0 To Arr.GetUpperBound(0)
            If IntCounter = index Then
                ArrChar(IntCurrentIndex) = MyValue
                IntCurrentIndex += 1
            ElseIf IntCounter <> index + 1 And IntCounter <> index + 2 Then
                ArrChar(IntCurrentIndex) = Arr(IntCounter)
                IntCurrentIndex += 1
            End If
        Next
        Arr = ArrChar

    End Function

    Private Function ReArrangeArrayFromTo(ByRef Arr() As String, ByVal MyValue As String, ByVal indexFrom As Integer, ByVal indexTo As Integer) As Boolean

        Dim ArrChar(Arr.GetUpperBound(0) - (indexTo - indexFrom)) As String
        Dim IntCounter As Integer
        Dim IntCurrentIndex As Integer

        For IntCounter = 0 To Arr.GetUpperBound(0)
            If IntCounter = indexFrom Then
                ArrChar(IntCurrentIndex) = MyValue
                IntCurrentIndex += 1
            ElseIf IntCounter < indexFrom Or IntCounter > indexTo Then
                ArrChar(IntCurrentIndex) = Arr(IntCounter)
                IntCurrentIndex += 1
            End If
        Next
        Arr = ArrChar

    End Function

    Private Function CheckPlusMinusOperator(ByVal strArr() As String) As Boolean
        Dim IntCounter As Integer
        For IntCounter = 0 To strArr.GetUpperBound(0)
            If strArr(IntCounter) = "+" Or strArr(IntCounter) = "-" Then
                Return True
            End If
        Next
    End Function

    Private Function CheckMultiplyDividOperator(ByVal strArr() As String) As Boolean
        Dim IntCounter As Integer
        For IntCounter = 0 To strArr.GetUpperBound(0)
            If strArr(IntCounter) = "*" Or strArr(IntCounter) = "/" Then
                Return True
            End If
        Next
    End Function

    Private Function CheckBracketOperator(ByVal strArr() As String) As Boolean
        Dim IntCounter As Integer
        For IntCounter = 0 To strArr.GetUpperBound(0)
            If strArr(IntCounter) = "(" Or strArr(IntCounter) = ")" Then
                Return True
            End If
        Next
    End Function

    Private Function CheckPowerOperator(ByVal strArr() As String) As Boolean
        Dim IntCounter As Integer
        For IntCounter = 0 To strArr.GetUpperBound(0)
            If strArr(IntCounter) = "^" Then
                Return True
            End If
        Next
    End Function

    Private Function IsFieldExpression(ByVal Statment As String) As Boolean
        Dim IntCounter As Integer
        Dim IntACounter As Integer

        If Statment.Substring(0, 2) = "<$" Then
            For IntCounter = 2 To Statment.Length - 1
                Select Case Statment(IntCounter)
                    Case "$", ">"
                        IntACounter += 1
                    Case Else
                        If Not IsAlphaNumirc(Statment(IntCounter)) Then
                            Return False
                        End If
                End Select
            Next

            If IntACounter = 2 Then
                Return True
            End If

        ElseIf Statment.Substring(0, 2) = "<@" Then
            For IntCounter = 2 To Statment.Length - 1
                Select Case Statment(IntCounter)
                    Case "@", ">"
                        IntACounter += 1
                    Case Else
                        If Not IsAlphaNumirc(Statment(IntCounter)) Then
                            Return False
                        End If
                End Select
            Next

            If IntACounter = 2 Then
                Return True
            End If

        Else
            Return True
        End If
    End Function

    Private Function IsAlphaNumirc(ByVal ChrChar As Char) As Boolean
        Dim IntAscii As Integer = Asc(ChrChar)
        If IntAscii >= 65 And IntAscii <= 90 Then
            Return True
        ElseIf IntAscii >= 97 And IntAscii <= 122 Then
            Return True
        ElseIf IntAscii >= 48 And IntAscii <= 57 Then
            Return True
        ElseIf IntAscii = 95 Or IntAscii = 61 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetVacationsDays(ByVal EmployeeId As Integer, Optional ByVal FisicalPeriodId As Integer = 0) As Single
        Dim FisicalStartDate As Date = Nothing
        Dim FisicalEndDate As Date = Nothing
        Dim VacationDays As Single
        Dim VacationEndDate As Date = Nothing
        Dim ClsFisicalYearsPeriods As New Clssys_FiscalYearsPeriods(mPage)
        Dim ClsEmployeesVacations As New Clshrs_EmployeesVacations(Page)
        '!=! Get Fisical Period Dates 
        If FisicalPeriodId = 0 Then
            ClsFisicalYearsPeriods.Find(" GetDate() Between sys_FiscalYearsPeriods.FromDate and sys_FiscalYearsPeriods.ToDate ")
        Else
            ClsFisicalYearsPeriods.Find(" sys_FiscalYearsPeriods.Id = " & FisicalPeriodId)
        End If
        If ClsFisicalYearsPeriods.ID <= 0 Then
            Return 0
        End If

        FisicalStartDate = ClsFisicalYearsPeriods.FromDate
        FisicalEndDate = ClsFisicalYearsPeriods.ToDate
        '===========================================================================================
        'Get AllVacations For Current Employee 
        If ClsEmployeesVacations.Find(" EmployeeId = " & EmployeeId & " And ActualStartDate Is Not Null ") Then
            If ClsEmployeesVacations.ID > 0 Then
                For Each row As Data.DataRow In ClsEmployeesVacations.DataSet.Tables(0).Rows
                    If row.Item("ActualEndDate") Is DBNull.Value Then
                        VacationEndDate = Now.Date.ToShortDateString
                    Else
                        VacationEndDate = row.Item("ActualEndDate")
                    End If

                    If row.Item("ActualStartDate") <= FisicalStartDate And VacationEndDate >= FisicalEndDate Then
                        VacationDays += DateDiff(DateInterval.Day, FisicalStartDate, FisicalEndDate)

                        'OutSide Left 
                    ElseIf VacationEndDate < FisicalStartDate Or row.Item("ActualStartDate") > FisicalEndDate Then
                        VacationDays = VacationDays

                        'Within Period 
                    ElseIf row.Item("ActualStartDate") > FisicalStartDate And (row.Item("ActualStartDate") < FisicalEndDate _
                       And VacationEndDate < FisicalEndDate) Then
                        VacationDays += DateDiff(DateInterval.Day, row.Item("ActualStartDate"), VacationEndDate)
                        'Intersect Including Start Period Date
                    ElseIf row.Item("ActualStartDate") < FisicalStartDate And VacationEndDate > FisicalStartDate _
                       And VacationEndDate < FisicalEndDate Then
                        VacationDays += DateDiff(DateInterval.Day, FisicalStartDate, VacationEndDate)
                        'Intersect Including End Period Date
                    ElseIf (row.Item("ActualStartDate") > FisicalStartDate And row.Item("ActualStartDate") < FisicalEndDate) _
                       And (VacationEndDate > FisicalStartDate And VacationEndDate > FisicalEndDate) Then
                        VacationDays += DateDiff(DateInterval.Day, row.Item("ActualStartDate"), FisicalEndDate)
                    End If
                Next
                'Within Period Totaly
            Else
                Return 0
            End If
            Return VacationDays
        End If
    End Function

#End Region

End Class

