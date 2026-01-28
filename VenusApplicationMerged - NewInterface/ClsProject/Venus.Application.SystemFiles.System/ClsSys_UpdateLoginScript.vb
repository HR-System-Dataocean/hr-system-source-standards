Public Class ClsSys_UpdateLoginScript
    Inherits ClsDataAcessLayer
#Region "Class Constructor"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)

    End Sub
#End Region
#Region "Public Methods"
    Public Function UpdateLogin() As Boolean
        Dim SQL As String


		SQL = "
        IF COL_LENGTH('sys_Companies', 'VacationFromPrepareDay') IS NULL
BEGIN
   alter table sys_Companies
add VacationFromPrepareDay bit null
END
        "
        ExecuteUpdate(SQL)

        SQL = "
       IF COL_LENGTH('sys_Companies', 'ExecuseRequestHoursallowed') IS NULL
BEGIN
   alter table sys_Companies
add ExecuseRequestHoursallowed int null
END
        "
        ExecuteUpdate(SQL)

        SQL = "
        IF COL_LENGTH('sys_Companies', 'EmployeeDocumentsAutoSerial') IS NULL
BEGIN
   alter table sys_Companies
add EmployeeDocumentsAutoSerial bit null
END
        "
        ExecuteUpdate(SQL)

        SQL = "
       IF COL_LENGTH('sys_Companies', 'UserDepartmentsPermissions') IS NULL
BEGIN
   alter table sys_Companies
add UserDepartmentsPermissions bit null
END
        "
        ExecuteUpdate(SQL)


        SQL = "
      alter table hrs_Positions add AppraisalTypeGroupID int null
        "
        ExecuteUpdate(SQL)

        SQL = "
      CREATE TABLE [dbo].[APP_AppraisalEmployeesStartDate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppraisalTypeID] [int] NULL,
	[EmployeeID] [int] NULL,
	[JoinDate] [varchar](50) NULL,
	[LastExternalAppraisalDate] [varchar](50) NULL,
 CONSTRAINT [PK_APP_AppraisalEmployeesStartDate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
        "
        ExecuteUpdate(SQL)

        SQL = "
       CREATE TABLE [dbo].[sys_DocumentTypesGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[EngName] [varchar](100) NULL,
	[ArbName] [varchar](100) NULL,
	[Remarks] [varchar](2048) NULL,
	[RegUserID] [int] NULL,
	[RegComputerID] [int] NULL,
	[RegDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_sys_DocumentTypesGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY];
        "
        ExecuteUpdate(SQL)

		SQL = "alter table sys_Documents add DocumentTypesGroupId int null "
		ExecuteUpdate(SQL)

		SQL = "ALTER PROCEDURE [dbo].[hrs_GetAllExpiredDocuments]
	@EmpCode varchar(50) = '', 
	@ExpireFromDate As Datetime = Null,
	@ExpireToDate As Datetime = Null,
	@DeptCode Varchar(50) = '',
	@BranchCode Varchar(50) = '',
	@DocumentTypeID Varchar(50) = '1',
    @DocumentTypesGroupID int = 0,
	@FilterType Int = 0,
	@Lang Int = 0
As
Set DateFormat DMY
Declare @EmployeeObjectID Int, @DependantObjectID  Int,@DeptID Int,@BranchID Int
Set @EmployeeObjectID = (Select ID From sys_Objects Where Code = 'hrs_Employees' And CancelDate Is Null)
Set @DependantObjectID = (Select ID From sys_Objects Where Code = 'hrs_EmployeesDependants' And CancelDate Is Null)
Set @DeptID = IsNull((Select top(1)ID From sys_Departments where Code = @DeptCode And CancelDate Is Null),0)
Set @BranchID = IsNull((Select top(1)ID From sys_Branches  where Code = @BranchCode  And CancelDate Is Null),0)
Select 
    DD.ID,
	DD.DocumentNumber,
	DD.IssueDate,
	convert(varchar(11),DD.IssueDate,131) as IssueDateH,
	DD.IssueDate_D,
	CAST(DD.ExpiryDate AS DATE) AS ExpiryDate,
	CAST(DD.RegDate AS DATE) AS RegDate,
	DD.ExpiryDate_D,
	convert(varchar(11),DD.ExpiryDate,131) as ExpiryDateH,
	isnull((select ArbName from hrs_Projects where ID = (select top 1 projectID from hrs_ProjectPlacements where PlacementCode = (select top 1 placementcode from hrs_ProjectPlacementEmployees where EmployeeID = DD.RecordID and FromDate <= GETDATE() and (ToDate is null or ToDate >= GETDATE())))),
	(select ArbName from sys_Locations where ID = DD.RecordID)) AS ProjectArb,
	
    isnull((select EngName from hrs_Projects where ID = (select top 1 projectID from hrs_ProjectPlacements where PlacementCode = (select top 1 placementcode from hrs_ProjectPlacementEmployees where EmployeeID = DD.RecordID and FromDate <= GETDATE() and (ToDate is null or ToDate >= GETDATE())))),
	(select EngName from sys_Locations where ID = DD.RecordID)) AS ProjectEng,

	Case When  @Lang  <> 1 Then 
			(Select EngName From sys_Cities Where ID = DD.IssuedCityID)
	Else	(Select ArbName  From sys_Cities Where ID = DD.IssuedCityID)
	End As IssueCity,
	Case When  @Lang <> 1 Then 
			D.EngName
	Else	D.ArbName
	End As DocumentType,
	(Select EngName  From sys_Cities Where ID = DD.IssuedCityID) AS IssueCityEngName ,
	(Select ArbName  From sys_Cities Where ID = DD.IssuedCityID) AS IssueCityArbName ,

	D.EngName	as DocumentTypeEngName,
	D.ArbName	as DocumentTypeArbName,
	
	Case When DD.ObjectID = @EmployeeObjectID Then
			(Select Code From hrs_Employees Where ID = DD.RecordID)
	When DD.ObjectID = @DependantObjectID Then
			(Select hrs_Employees.Code  From hrs_Employees Inner Join hrs_EmployeesDependants On hrs_Employees.ID = hrs_EmployeesDependants.EmployeeID Where hrs_EmployeesDependants.ID = DD.RecordID)
	Else ''
	End	AS EmpCode,
	
	dbo.fn_GetEmpName (	Case When DD.ObjectID = @EmployeeObjectID Then
			(Select Code From hrs_Employees Where ID = DD.RecordID)
	When DD.ObjectID = @DependantObjectID Then
			(Select hrs_Employees.Code  From hrs_Employees Inner Join hrs_EmployeesDependants On hrs_Employees.ID = hrs_EmployeesDependants.EmployeeID Where hrs_EmployeesDependants.ID = DD.RecordID)
	Else ''
	End,0)	AS EmpEngName,
	
	dbo.fn_GetEmpName (	Case When DD.ObjectID = @EmployeeObjectID Then
			(Select Code From hrs_Employees Where ID = DD.RecordID)
	When DD.ObjectID = @DependantObjectID Then
			(Select hrs_Employees.Code  From hrs_Employees Inner Join hrs_EmployeesDependants On hrs_Employees.ID = hrs_EmployeesDependants.EmployeeID Where hrs_EmployeesDependants.ID = DD.RecordID)
	Else ''
	End,1)	AS EmpArbName,
			(Select IsNull(EngName,'') From hrs_EmployeesDependants Where ID = DD.RecordID) as DependantEngName,
			(Select IsNull(ArbName,'') From hrs_EmployeesDependants Where ID = DD.RecordID) as DependantArbName,
		Case When DD.ObjectID = @EmployeeObjectID Then
			(Select ID From hrs_Employees Where ID = DD.RecordID)
	When DD.ObjectID = @DependantObjectID Then
			(Select hrs_Employees.ID  From hrs_Employees Inner Join hrs_EmployeesDependants On hrs_Employees.ID = hrs_EmployeesDependants.EmployeeID Where hrs_EmployeesDependants.ID = DD.RecordID)
	Else ''
	End	AS EmpID,
	Case When DD.ObjectID = @EmployeeObjectID Then
			(Select ID From hrs_EmployeesDependants Where ID = DD.RecordID)
	When DD.ObjectID = @DependantObjectID Then
			(Select hrs_EmployeesDependants.ID  From hrs_Employees Inner Join hrs_EmployeesDependants On hrs_Employees.ID = hrs_EmployeesDependants.EmployeeID Where hrs_EmployeesDependants.ID = DD.RecordID)
	Else ''
	End	AS DepenID,
			Case When DD.ObjectID = @DependantObjectID Then
		(Select ID From hrs_EmployeesDependants Where ID = DD.RecordID)
	    Else	''
	End As DependantID,
	Case When DD.ObjectID = @EmployeeObjectID Then
			Case When @Lang  <> 1 Then
				(Select ISNull(EngName,'')+','+ISNull(FatherEngName,'')+','+ISNull(GrandEngName,'')+','+ISNull(FamilyEngName,'') From hrs_Employees Where ID = DD.RecordID)
			Else
				(Select ISNull(ArbName,'')+','+ISNull(FatherArbName,'')+','+ISNull(GrandArbName,'')+','+ISNull(FamilyArbName,'') From hrs_Employees Where ID = DD.RecordID)
			End
	When DD.ObjectID = @DependantObjectID Then
			Case When  @Lang  <> 1 Then
				(Select ISNull(hrs_Employees.EngName,'')+','+ISNull(FatherEngName,'')+','+ISNull(GrandEngName,'')+','+ISNull(FamilyEngName,'') From hrs_Employees Inner Join hrs_EmployeesDependants On hrs_Employees.ID = hrs_EmployeesDependants.EmployeeID Where hrs_EmployeesDependants.ID = DD.RecordID)
			Else
				(Select ISNull(hrs_Employees.ArbName,'')+','+ISNull(FatherArbName,'')+','+ISNull(GrandArbName,'')+','+ISNull(FamilyArbName,'') From hrs_Employees Inner Join hrs_EmployeesDependants On hrs_Employees.ID = hrs_EmployeesDependants.EmployeeID Where hrs_EmployeesDependants.ID = DD.RecordID)
			End
	Else ''
	End	AS EmployeeName,
	Case When DD.ObjectID = @DependantObjectID Then
		Case When  @Lang  <> 1 Then
			(Select IsNull(EngName,'') From hrs_EmployeesDependants Where ID = DD.RecordID)
		Else
			(Select IsNull(ArbName,'') From hrs_EmployeesDependants Where ID = DD.RecordID)
		End
	Else	''
	End As DependantName
From 
	sys_DocumentsDetails DD 
	Inner Join sys_Documents D On DD.DocumentID = D.ID 
Where
	DD.ExpiryDate Between ISNULL (@ExpireFromDate ,'01/01/1910') And ISNULL(@ExpireToDate,GETDATE())
	And D.CancelDate Is Null
	And DD.CancelDate Is Null
		And 
	(
		@DocumentTypeID Is Null
		OR
		@DocumentTypeID = 0
		OR
		D.Code = @DocumentTypeID
	)
	And 
	(
		@DocumentTypesGroupID Is Null
		OR
		@DocumentTypesGroupID = 0
		OR
		D.DocumentTypesGroupId = @DocumentTypesGroupID
	)
	And (
					(
						 DD.ObjectID = @EmployeeObjectID 
						 And
						 DD.RecordID In (Select 
											E.ID 
										From 
											hrs_Employees E 
										Where 
											E.Code Like ISNULL(@EmpCode,'') + '%'
											And E.CancelDate Is Null and isnull(E.RegComputerID,0) = 0 and E.ExcludeDate is null
											And (@DeptID = 0 OR E.DepartmentID = @DeptID )
											And (@BranchID = 0 OR E.BranchID = @BranchID ))
					)
					OR
					(
						DD.ObjectID = @DependantObjectID  
						And
						DD.RecordID In (Select 
											ED.ID 
										From 
											hrs_EmployeesDependants ED 
											Inner Join  hrs_Employees E On ED.EmployeeID = E.ID 
										Where 
											ED.CancelDate Is Null
											And E.Code Like ISNULL(@EmpCode,'') + '%'
											And E.CancelDate Is Null and isnull(E.RegComputerID,0) = 0 and E.ExcludeDate is null
											And (@DeptID = 0 OR E.DepartmentID = @DeptID )
											And (@BranchID = 0 OR E.BranchID = @BranchID ))
					)		
	)
Order By
	EmpCode
"
        ExecuteUpdate(SQL)

        Return True


    End Function

    Public Function ExecuteUpdate(ByVal mySQLQuery As String) As Boolean

        Try
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, CommandType.Text, mySQLQuery)
        Catch ex As Exception

        End Try
    End Function

#End Region
End Class
