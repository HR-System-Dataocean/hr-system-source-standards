Public Class ClsSys_UpdateDB
    Inherits ClsDataAcessLayer
#Region "Class Constructor"
    Public Sub New(ByVal Page As Web.UI.Page)
        MyBase.New(Page)

    End Sub
#End Region
#Region "Public Methods"
	Public Function UpdateHR() As Boolean
		Dim SQL As String

#Region "Scripts Before 01-12-2024"
        '		SQL = "
        'IF COL_LENGTH('sys_DocumentsDetails', 'DocumentID') IS NOT NULL  
        'BEGIN
        '    ALTER TABLE sys_DocumentsDetails
        '    ALTER COLUMN DocumentID int NULL
        'END"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('Att_AttendTransactions', 'ShiftNo') IS NULL  
        'BEGIN
        '    ALTER TABLE Att_AttendTransactions
        '    ADD ShiftNo tinyint 
        'END"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_Employees', 'HasTaqat') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_Employees
        '    ADD HasTaqat bit 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('Att_AttendShifts', 'FirstShiftTimeInFingerprintStart') IS NULL  
        'BEGIN
        '    ALTER TABLE Att_AttendShifts
        '    ADD FirstShiftTimeInFingerprintStart varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        '"
        '		ExecuteUpdate(SQL)


        '		SQL = "
        'IF COL_LENGTH('Att_AttendShifts', 'FirstShiftEntryTimeInClose') IS NULL  
        'BEGIN
        '    ALTER TABLE Att_AttendShifts
        '    ADD FirstShiftEntryTimeInClose varchar(5) 
        'END

        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('Att_AttendShifts', 'FirstShiftTimeOutFingerprintClose') IS NULL  
        'BEGIN
        '    ALTER TABLE Att_AttendShifts
        '    ADD FirstShiftTimeOutFingerprintClose varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('Att_AttendShifts', 'SecondShiftTimeInFingerprintStart') IS NULL  
        'BEGIN
        '    ALTER TABLE Att_AttendShifts
        '    ADD SecondShiftTimeInFingerprintStart varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('Att_AttendShifts', 'SecondShiftEntryTimeInClose') IS NULL  
        'BEGIN
        '    ALTER TABLE Att_AttendShifts
        '    ADD SecondShiftEntryTimeInClose varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('Att_AttendShifts', 'SecondShiftTimeOutFingerprintClose') IS NULL  
        'BEGIN
        '    ALTER TABLE Att_AttendShifts
        '    ADD SecondShiftTimeOutFingerprintClose varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER PROCEDURE [dbo].[Att_GetCustomAttandace]
        ' @EmployeeID     AS INT, 
        ' @Attandancedate AS DATE
        'AS
        'SET DATEFIRST 6

        'SET DATEFORMAT dmy
        '     SELECT AttShD. DayNo                                AS DayNo,                             
        '            AttShD.TimeIn                                AS TimeIn, 
        '            AttShD.TimeOut                               AS TimeOut, 
        '            AttShD.TimeIn2nd                             AS TimeIn2nd, 
        '            AttShD.TimeOut2nd                            AS TimeOut2nd, 
        '            AttShD.IsDayOff                              AS IsDayOff,
        '			AttSh.FirstShiftTimeInFingerprintStart       AS FirstShiftTimeInFingerprintStart,
        '			AttSh.FirstShiftEntryTimeInClose             AS FirstShiftEntryTimeInClose,
        '			AttSh.FirstShiftTimeOutFingerprintClose      AS FirstShiftTimeOutFingerprintClose,
        '			AttSh.SecondShiftTimeInFingerprintStart      AS SecondShiftTimeInFingerprintStart,
        '			AttSh.SecondShiftEntryTimeInClose            AS SecondShiftEntryTimeInClose,
        '			AttSh.SecondShiftTimeOutFingerprintClose     AS SecondShiftTimeOutFingerprintClose
        '     FROM Att_AttendShiftDays AttShD
        '          INNER JOIN Att_AttendShifts AttSh ON Attsh.id = Attshd.AttendShiftID
        '          INNER JOIN Att_AttendAppointment AttApp ON AttApp.AttendaceShiftID = attsh.id
        '          INNER JOIN Att_AttendAppointmentMembers AttAppM ON AttappM.AppointID = AttApp.ID
        '     WHERE AttAppM.EmployeeID = @EmployeeID
        '           AND @Attandancedate BETWEEN AttApp.FromDate AND AttApp.ToDate
        '		   and  DATEPART(dw,@Attandancedate)=DayNo
        '     ORDER BY AttApp.RegDate DESC
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR  ALTER PROCEDURE [dbo].[Att_GetCustomAttandace]
        ' @EmployeeID     AS INT, 
        ' @Attandancedate AS DATE
        'AS
        'SET DATEFIRST 6

        'SET DATEFORMAT dmy
        '     SELECT AttShD. DayNo                                AS DayNo,                             
        '            AttShD.TimeIn                                AS TimeIn, 
        '            AttShD.TimeOut                               AS TimeOut, 
        '            AttShD.TimeIn2nd                             AS TimeIn2nd, 
        '            AttShD.TimeOut2nd                            AS TimeOut2nd, 
        '            AttShD.IsDayOff                              AS IsDayOff,
        '			AttSh.FirstShiftTimeInFingerprintStart       AS FirstShiftTimeInFingerprintStart,
        '			AttSh.FirstShiftEntryTimeInClose             AS FirstShiftEntryTimeInClose,
        '			AttSh.FirstShiftTimeOutFingerprintClose      AS FirstShiftTimeOutFingerprintClose,
        '			AttSh.SecondShiftTimeInFingerprintStart      AS SecondShiftTimeInFingerprintStart,
        '			AttSh.SecondShiftEntryTimeInClose            AS SecondShiftEntryTimeInClose,
        '			AttSh.SecondShiftTimeOutFingerprintClose     AS SecondShiftTimeOutFingerprintClose
        '     FROM Att_AttendShiftDays AttShD
        '          INNER JOIN Att_AttendShifts AttSh ON Attsh.id = Attshd.AttendShiftID
        '          INNER JOIN Att_AttendAppointment AttApp ON AttApp.AttendaceShiftID = attsh.id
        '          INNER JOIN Att_AttendAppointmentMembers AttAppM ON AttappM.AppointID = AttApp.ID
        '     WHERE AttAppM.EmployeeID = @EmployeeID
        '           AND @Attandancedate BETWEEN AttApp.FromDate AND AttApp.ToDate
        '		   AND  DATEPART(dw, @Attandancedate) = DayNo
        '		   AND AttAppM.CancelDate IS NULL
        '     ORDER BY AttApp.RegDate DESC
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_FingerprintSettings'
        ')
        'BEGIN
        '	IF COL_LENGTH('hrs_FingerprintSettings', 'UserMatchColumnName') IS NULL
        '		BEGIN
        '			DROP TABLE hrs_FingerprintSettings
        '		END
        '	END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_FingerprintSettings'
        ')
        '    BEGIN
        '        CREATE TABLE hrs_FingerprintSettings
        '		(
        '			ID int IDENTITY(1,1) NOT NULL,
        '			IsDefault bit,
        '			Code varchar(50) NULL,
        '			EngName varchar(100) NULL,
        '			ArbName nvarchar(100) NULL,
        '			Remarks varchar(2048) NULL,
        '			FingerprintServerIP varchar(50),
        '			FingerprintDatabaseName varchar(50),
        '			FingerprintTableName varchar(100),
        '			UserIdColumnName varchar(50),
        '			CheckInOutColumnName varchar(50),
        '			UsersTableName varchar(100),
        '			UserMatchColumnName varchar(50),
        '			UsersTableIdntity varchar(50),
        '			RegUserId int,
        '			RegDate smalldatetime,
        '			CancelDate smalldatetime
        '		)
        '	END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_FingerprintSettings', 'RegDate') IS NOT NULL  
        'BEGIN
        '	IF((SELECT COUNT(object_definition(default_object_id)) FROM sys.columns WHERE name ='RegDate'
        '	AND    object_id = object_id('hrs_FingerprintSettings')) < 1)
        '	BEGIN
        '		ALTER TABLE hrs_FingerprintSettings
        '		ADD  CONSTRAINT [DF_hrs_FingerprintSettings_RegDate]  DEFAULT (getdate()) FOR [RegDate]
        '	END
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Forms WHERE Code = 'frmFingerprintSettings') = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_Forms]
        '           ([Code]
        '           ,[EngName]
        '           ,[ArbName]
        '           ,[ArbName4S]
        '           ,[EngDescription]
        '           ,[ArbDescription]
        '           ,[Rank]
        '           ,[ModuleID]
        '           ,[Height]
        '           ,[Width]
        '           ,[RegDate])
        '     VALUES
        '           ('frmFingerprintSettings'
        '           ,'frmFingerprintSettings.aspx'
        '           ,'اعدادات برنامج البصمة'
        '           ,''
        '           ,'Fingerprint Settings'
        '           ,'اعدادات برنامج البصمة'
        '           ,0
        '           ,2
        '           ,650
        '           ,1100
        '           ,GETDATE())
        'END
        'ELSE
        'BEGIN
        '	UPDATE [sys_Forms] SET [ArbName] = 'اعدادات برنامج البصمة',[ArbDescription] = 'اعدادات برنامج البصمة' WHERE Code = 'frmFingerprintSettings'
        'END

        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmFingerprintSettings') = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_Menus]
        '           ([Code]
        '           ,[EngName]
        '           ,[ArbName]
        '           ,[ArbName4S]
        '           ,[ParentID]
        '           ,[Rank]
        '           ,[FormID]
        '           ,[ViewFormID]
        '           ,[IsHide]
        '           ,[ViewType]
        '           ,[RegDate])
        '     VALUES
        '           ('frmFingerprintSettings'
        '           ,'frmFingerprintSettings'
        '           ,'اعدادات برنامج البصمة'
        '           ,'اعدادات برنامج البصمة'
        '           ,410
        '           ,6
        '           ,(SELECT ID FROM sys_Forms WHERE Code = 'frmFingerprintSettings')
        '           ,283
        '           ,0
        '           ,1
        '           ,GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].[sys_Menus] SET [ArbName] = 'اعدادات برنامج البصمة', [ArbName4S] = 'اعدادات برنامج البصمة' WHERE Code = 'frmFingerprintSettings'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_FormsPermissions WHERE UserID = 1 AND FormID = 
        '			(SELECT ID FROM sys_Forms WHERE Code = 'frmFingerprintSettings')) = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_FormsPermissions]
        '           ([FormID]
        '           ,[UserID]
        '           ,[AllowView]
        '           ,[AllowAdd]
        '           ,[AllowEdit]
        '           ,[AllowDelete]
        '           ,[AllowPrint]
        '		   ,[RegUserID]
        '           ,[RegDate])
        '     VALUES
        '           ((SELECT ID FROM sys_Forms WHERE Code = 'frmFingerprintSettings')
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '		   ,1
        '           ,GETDATE())
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeeVacationOpenBalanceSettlement', 'PaidDays') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeeVacationOpenBalanceSettlement
        '    ADD PaidDays real 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesTransactions', 'EmployeesVacationsID') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesTransactions
        '    ADD EmployeesVacationsID int 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('sys_Companies', 'CountEmployeeVacationDaysTotal') IS NULL  
        'BEGIN
        '    ALTER TABLE sys_Companies
        '    ADD CountEmployeeVacationDaysTotal bit 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF ((SELECT COUNT(OBJECTPROPERTY(OBJECT_ID('sys_Companies'), 'TableHasIdentity'))) < 1)
        'BEGIN
        '    ALTER TABLE dbo.sys_Companies
        '	ADD ID INT IDENTITY
        '       CONSTRAINT PK_sys_Companies PRIMARY KEY CLUSTERED
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesTransactions', 'TotalVacDaySettlement') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesTransactions
        '    ADD TotalVacDaySettlement int 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesTransactions', 'RemainVacDaySettlement') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesTransactions
        '    ADD RemainVacDaySettlement int 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesTransactions', 'LastPaidDate') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesTransactions
        '    ADD LastPaidDate smalldatetime 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('sys_Companies', 'ZeroBalAfterVac') IS NULL  
        'BEGIN
        '    ALTER TABLE sys_Companies
        '    ADD ZeroBalAfterVac bit 
        'END
        'ELSE IF((SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS
        '		 WHERE TABLE_NAME = 'sys_Companies' AND COLUMN_NAME = 'ZeroBalAfterVac') <> 'bit')
        'BEGIN
        '    ALTER TABLE sys_Companies
        '    DROP COLUMN ZeroBalAfterVac
        '    ALTER TABLE sys_Companies
        '    ADD ZeroBalAfterVac bit 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesOvertimeList'
        ')
        '    BEGIN
        '        CREATE TABLE [dbo].[hrs_EmployeesOvertimeList](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[EmployeeID] [int] NULL,
        '	[StartDate] [smalldatetime] NULL,
        '	[EndDate] [smalldatetime] NOT NULL,
        '	[OvertimeHours] [real] NULL,
        '	[Shift] [int] NULL,
        '	[FingerPrint] [bit] NULL,
        '	[Notes] [varchar](2048) NULL,
        '	[Remarks] [varchar](2048) NULL,
        '	[RegUserID] [int] NULL,
        '	[RegComputerID] [int] NULL,
        '	[RegDate] [smalldatetime] NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_EmployeesOvertimeList] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '	END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF 
        '((SELECT COUNT(object_definition(default_object_id)) FROM sys.columns WHERE name ='RegDate'
        '	AND    object_id = object_id('hrs_EmployeesOvertimeList')) < 1)
        '    BEGIN
        '		ALTER TABLE [dbo].[hrs_EmployeesOvertimeList] ADD  DEFAULT (getdate()) FOR [RegDate]
        '	END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Forms WHERE Code = 'frmEmployeesOvertimeList') = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_Forms]
        '           ([Code]
        '           ,[EngName]
        '           ,[ArbName]
        '           ,[ArbName4S]
        '           ,[EngDescription]
        '           ,[ArbDescription]
        '           ,[Rank]
        '           ,[ModuleID]
        '           ,[Height]
        '           ,[Width]
        '           ,[RegDate])
        '     VALUES
        '           ('frmEmployeesOvertimeList'
        '           ,'frmEmployeesOvertimeList.aspx'
        '           ,'قائمة الاضافي'
        '           ,'قائمة الاضافي'
        '           ,'Employees Overtime List'
        '           ,'قائمة الاضافي'
        '           ,0
        '           ,2
        '           ,650
        '           ,1100
        '           ,GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].[sys_Forms] SET [ArbName] = 'قائمة الاضافي', [ArbName4S] = 'قائمة الاضافي', [ArbDescription] = 'قائمة الاضافي' WHERE Code = 'frmEmployeesOvertimeList'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmEmployeesOvertimeList') = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_Menus]
        '           ([Code]
        '           ,[EngName]
        '           ,[ArbName]
        '           ,[ArbName4S]
        '           ,[ParentID]
        '           ,[Rank]
        '           ,[FormID]
        '           ,[ViewFormID]
        '           ,[IsHide]
        '           ,[ViewType]
        '           ,[RegDate])
        '     VALUES
        '           ('frmEmployeesOvertimeList'
        '           ,'frmEmployeesOvertimeList'
        '           ,'قائمة الاضافي'
        '           ,'قائمة الاضافي'
        '           ,410
        '           ,6
        '           ,(SELECT ID FROM sys_Forms WHERE Code = 'frmEmployeesOvertimeList')
        '           ,283
        '           ,0
        '           ,1
        '           ,GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].[sys_Menus] SET [ArbName] = 'قائمة الاضافي', [ArbName4S] = 'قائمة الاضافي' WHERE Code = 'frmEmployeesOvertimeList'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_FormsPermissions WHERE UserID = 1 AND FormID = 
        '			(SELECT ID FROM sys_Forms WHERE Code = 'frmEmployeesOvertimeList')) = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_FormsPermissions]
        '           ([FormID]
        '           ,[UserID]
        '           ,[AllowView]
        '           ,[AllowAdd]
        '           ,[AllowEdit]
        '           ,[AllowDelete]
        '           ,[AllowPrint]
        '		   ,[RegUserID]
        '           ,[RegDate])
        '     VALUES
        '           ((SELECT ID FROM sys_Forms WHERE Code = 'frmEmployeesOvertimeList')
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '		   ,1
        '           ,GETDATE())
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmFingerprintSettings') <> 0
        'BEGIN
        'UPDATE sys_Menus SET ArbName = 'اعدادات برنامج البصمة' WHERE Code = 'frmFingerprintSettings'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmEmployeesOvertimeList') <> 0
        'BEGIN
        'UPDATE sys_Menus SET ArbName = 'قائمة الاضافي' WHERE Code = 'frmEmployeesOvertimeList'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmEmployeesOvertimeList') <> 0
        'BEGIN
        'UPDATE sys_Menus SET ArbName = 'قائمة الاضافي' WHERE Code = 'frmEmployeesOvertimeList'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_FingerprintSettings', 'FingerprintServerIP') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_FingerprintSettings
        '    ADD FingerprintServerIP nvarchar(50) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF 
        '((SELECT COUNT(object_definition(default_object_id)) FROM sys.columns WHERE name ='RegDate'
        '	AND    object_id = object_id('hrs_FingerprintSettings')) < 1)
        '    BEGIN
        '		ALTER TABLE [dbo].[hrs_FingerprintSettings] ADD  DEFAULT (getdate()) FOR [RegDate]
        '	END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesFingerprints'
        ')
        'BEGIN
        'CREATE TABLE hrs_EmployeesFingerprints
        '(
        '	ID int IDENTITY(1,1) NOT NULL,
        '	FingerprintTime smalldatetime,
        '	UserId int,
        '	UserCode varchar(20),
        '	RegDate smalldatetime,
        '	FingerprintID int
        ')

        'ALTER TABLE hrs_EmployeesFingerprints
        '	ADD  CONSTRAINT [DF_hrs_EmployeesFingerprint_RegDate]  DEFAULT (getdate()) FOR [RegDate]

        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER Proc [dbo].[hrs_FingerprintForCurrntMonth]
        '@FingerprintID int
        'AS
        'BEGIN
        ' DECLARE
        '	@ServerIP Varchar(50),
        '	@DatabaseName Varchar(50),
        '	@FingerprintTableName Varchar(50),
        '	@UserIdColumnName Varchar(50),
        '	@CheckInOutColumnName Varchar(50),
        '	@Query NVARCHAR (MAX),
        '	@LastDateFingerPrint smalldatetime,
        '	@UsersTableIdntity Varchar(50),
        '	@UserMatchColumnName Varchar(50),
        '	@UsersTableName Varchar(50)

        'SELECT 
        '	@ServerIP = FingerprintServerIP,
        '	@DatabaseName = FingerprintDatabaseName,
        '	@FingerprintTableName = FingerprintTableName,
        '	@UserIdColumnName = UserIdColumnName,
        '	@CheckInOutColumnName = CheckInOutColumnName,
        '	@UsersTableIdntity = UsersTableIdntity,
        '	@UserMatchColumnName = UserMatchColumnName,
        '	@UsersTableName = UsersTableName
        'FROM hrs_FingerprintSettings
        'WHERE ID = @FingerprintID

        'IF EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesFingerprints'
        ')
        'BEGIN
        '	DECLARE @result TABLE ([FingerprintTime] smalldatetime);

        '	INSERT INTO @result ([FingerprintTime])
        '	EXEC (N'SELECT TOP (1) [FingerprintTime] FROM [hrs_EmployeesFingerprints] WHERE [FingerprintID] = ' + @FingerprintID
        '	    + N' ORDER BY [FingerprintTime] DESC');

        '	SET @LastDateFingerPrint = (SELECT TOP (1) [FingerprintTime] FROM @result);

        '	SET @Query = NULL
        '	SET @Query = N'INSERT INTO hrs_EmployeesFingerprints ([UserID ],'
        '			   + N'[FingerprintTime], [UserCode], [FingerprintID])'
        '			   + N'SELECT fp.[' + @UserIdColumnName + N'],'
        '	           + N' fp.[' + @CheckInOutColumnName + N'],'
        '			   + N' us.[' + @UserMatchColumnName + N'], '
        '			   + CAST(@FingerprintID AS NVARCHAR(10))
        '			   + N' FROM [' + @DatabaseName + N']'
        '			   + N'.[dbo].[' + @FingerprintTableName + N'] AS fp'
        '			   + N' INNER JOIN [' + @DatabaseName + N'].[dbo].[' + @UsersTableName + N'] AS us'
        '			   + N' ON fp.[' + @UserIdColumnName + N'] = us.[' + @UsersTableIdntity + N']'


        '	IF(@LastDateFingerPrint IS NOT NULL)
        '		BEGIN
        '			DECLARE @ConvertLastDate varchar(20) 
        '			SELECT @ConvertLastDate  = CAST(CONVERT(VARCHAR(20), @LastDateFingerPrint, 113)AS SmallDateTime)
        '			SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] > ''' + @ConvertLastDate + N''''
        '		END
        '	ELSE
        '		BEGIN
        '			SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] > GETDATE()-60'
        '		END

        '		EXEC (@Query)
        '    END
        'END

        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesFingerprints', 'UserId') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_FingerprintSettings
        '    ADD UserId int 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesExcuses'
        ')
        'BEGIN
        'CREATE TABLE [dbo].[hrs_EmployeesExcuses](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[EmployeeID] [int] NULL,
        '	[ExcuseTarget] [nvarchar](50) NULL,
        '	[ExcuseType] [nvarchar](10) NULL,
        '	[ExcuseDate] [smalldatetime] NULL,
        '	[ExcuseHours] [varchar](10) NULL,
        '	[Shift] [int] NULL,
        '	[Notes] [varchar](200) NULL,
        '	[Remarks] [varchar](2048) NULL,
        '	[RegUserID] [int] NULL,
        '	[RegComputerID] [int] NULL,
        '	[RegDate] [smalldatetime] NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_EmployeesExcuses] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        'END
        'ELSE IF NOT EXISTS
        '(
        '    SELECT COUNT(*)
        '    FROM hrs_EmployeesExcuses
        '    WHERE ExcuseDate > '2021-10-01'
        ')
        'BEGIN
        ' DROP TABLE hrs_EmployeesExcuses

        ' CREATE TABLE [dbo].[hrs_EmployeesExcuses](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[EmployeeID] [int] NULL,
        '	[ExcuseTarget] [nvarchar](50) NULL,
        '	[ExcuseType] [nvarchar](10) NULL,
        '	[ExcuseDate] [smalldatetime] NULL,
        '	[ExcuseHours] [varchar](10) NULL,
        '	[Shift] [int] NULL,
        '	[Notes] [varchar](200) NULL,
        '	[Remarks] [varchar](2048) NULL,
        '	[RegUserID] [int] NULL,
        '	[RegComputerID] [int] NULL,
        '	[RegDate] [smalldatetime] NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_EmployeesExcuses] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]

        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClasses', 'AbsentFormula') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClasses
        '    ADD AbsentFormula varchar(2048)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClasses', 'LateFormula') IS NULL  
        'BEGIN
        '   ALTER TABLE hrs_EmployeesClasses
        '   ADD LateFormula varchar(2048)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesClassesWeeklyCustomize'
        ')
        'BEGIN
        'CREATE TABLE [dbo].[hrs_EmployeesClassesWeeklyCustomize](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NULL,
        '	[EngName] [varchar](100) NULL,
        '	[ArbName] [varchar](100) NULL,
        '	[BeginDate] [smalldatetime] NULL,
        '	[EndDate] [smalldatetime] NULL,
        '	[EmployeeClassID] [int] NOT NULL,
        '	[Remarks] [varchar](2048) NULL,
        '	[RegUserID] [int] NULL,
        '	[RegComputerID] [int] NULL,
        '	[RegDate] [smalldatetime] NOT NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_EmployeesClassesCalenderSetCustomize] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]

        'ALTER TABLE [dbo].[hrs_EmployeesClassesWeeklyCustomize] ADD  DEFAULT (getdate()) FOR [RegDate]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Forms WHERE Code = 'frmEmployeesClassesWeeklySetCustomize') = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_Forms]
        '           ([Code]
        '           ,[EngName]
        '           ,[ArbName]
        '           ,[ArbName4S]
        '           ,[EngDescription]
        '           ,[ArbDescription]
        '           ,[Rank]
        '           ,[ModuleID]
        '           ,[Height]
        '           ,[Width]
        '           ,[RegDate])
        '     VALUES
        '           ('frmEmployeesClassesWeeklySetCustomize'
        '           ,'frmEmployeesClassesWeeklySetCustomize.aspx'
        '           ,'تخصيص الدوام'
        '           ,''
        '           ,'Employees Classe sWeekly Set Customize'
        '           ,'تخصيص الدوام'
        '           ,0
        '           ,2
        '           ,650
        '           ,1100
        '           ,GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].[sys_Forms] SET [ArbName] = 'تخصيص الدوام', [ArbDescription] = 'تخصيص الدوام' WHERE Code = 'frmEmployeesClassesWeeklySetCustomize'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmEmployeesClassesWeeklySetCustomize') = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_Menus]
        '           ([Code]
        '           ,[EngName]
        '           ,[ArbName]
        '           ,[ArbName4S]
        '           ,[ParentID]
        '           ,[Rank]
        '           ,[FormID]
        '           ,[ViewFormID]
        '           ,[IsHide]
        '           ,[ViewType]
        '           ,[RegDate])
        '     VALUES
        '           ('frmEmployeesClassesWeeklySetCustomize'
        '           ,'frmEmployeesClassesWeeklySetCustomize'
        '           ,'تخصيص الدوام'
        '           ,'تخصيص الدوام'
        '           ,410
        '           ,6
        '           ,(SELECT ID FROM sys_Forms WHERE Code = 'frmEmployeesClassesWeeklySetCustomize')
        '           ,283
        '           ,0
        '           ,1
        '           ,GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].[sys_Menus] SET [ArbName] = 'تخصيص الدوام', [ArbName4S] = 'تخصيص الدوام' WHERE Code = 'frmEmployeesClassesWeeklySetCustomize'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF(SELECT COUNT(*) FROM sys_FormsPermissions WHERE UserID = 1 AND FormID = 
        '			(SELECT ID FROM sys_Forms WHERE Code = 'frmEmployeesClassesWeeklySetCustomize')) = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_FormsPermissions]
        '           ([FormID]
        '           ,[UserID]
        '           ,[AllowView]
        '           ,[AllowAdd]
        '           ,[AllowEdit]
        '           ,[AllowDelete]
        '           ,[AllowPrint]
        '		   ,[RegUserID]
        '           ,[RegDate])
        '     VALUES
        '           ((SELECT ID FROM sys_Forms WHERE Code = 'frmEmployeesClassesWeeklySetCustomize')
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '		   ,1
        '           ,GETDATE())
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesClassesWeeklyCustomizeDays'
        ')
        'BEGIN
        'CREATE TABLE [dbo].[hrs_EmployeesClassesWeeklyCustomizeDays](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[CustomizeID] [int] NULL,
        '	[DayNo] [int] NULL,
        '	[TimeIn1] [varchar](5) NULL,
        '	[TimeOut1] [varchar](5) NULL,
        '	[TimeIn2] [varchar](5) NULL,
        '	[TimeOut2] [varchar](5) NULL,
        '	[IsDayOff] [bit] NULL,
        '	[RegUserID] [int] NULL,
        '	[RegComputerID] [int] NULL,
        '	[RegDate] [smalldatetime] NOT NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_EmployeesClassesCalenderSetCustomizeDays] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]


        'ALTER TABLE [dbo].[hrs_EmployeesClassesWeeklyCustomizeDays] ADD  DEFAULT (getdate()) FOR [RegDate]


        'ALTER TABLE [dbo].[hrs_EmployeesClassesWeeklyCustomizeDays]  WITH CHECK ADD  CONSTRAINT [FK_hrs_EmployeesClassesCalenderSetCustomizeDays_hrs_EmployeesClassesCalenderSetCustomize] FOREIGN KEY([CustomizeID])
        'REFERENCES [dbo].[hrs_EmployeesClassesWeeklyCustomize] ([ID])

        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClassesWeeklyCustomize', 'EmployeeID') IS NULL  
        'BEGIN
        '   ALTER TABLE hrs_EmployeesClassesWeeklyCustomize
        '   ADD EmployeeID int
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClassesWeeklyCustomize', 'EmployeeClassID') IS NOT NULL  
        'BEGIN
        '   ALTER TABLE hrs_EmployeesClassesWeeklyCustomize
        '   ALTER COLUMN EmployeeClassID int NULL
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF((SELECT COUNT(*) FROM sys_Objects WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize') = 0)
        'BEGIN
        ' INSERT INTO sys_Objects
        ' (Code, 
        '  EngName, 
        '  ArbName)
        ' VALUES
        ' ('hrs_EmployeesClassesWeeklyCustomize', 
        '  'hrs_EmployeesClassesWeeklyCustomize', 
        '  'hrs_EmployeesClassesWeeklyCustomize')
        ' END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF((SELECT COUNT(*) FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize') = 0)
        'BEGIN
        'INSERT INTO sys_Searchs
        '(Code, 
        ' EngName, 
        ' ArbName, 
        ' ObjectID)
        ' VALUES
        ' ('hrs_EmployeesClassesWeeklyCustomize', 
        '  'hrs_EmployeesClassesWeeklyCustomize', 
        '  'hrs_EmployeesClassesWeeklyCustomize', 
        '  (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'))
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF((SELECT COUNT(*) FROM sys_Fields WHERE ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')) = 0)
        'BEGIN
        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'ArbName',
        '  167,
        '  100,
        '  4
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'BeginDate',
        '  58,
        '  4,
        '  5
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'CancelDate',
        '  58,
        '  4,
        '  12
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'Code',
        '  167,
        '  50,
        '  2
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'EmployeeClassID',
        '  56,
        '  4,
        '  7
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'EndDate',
        '  58,
        '  4,
        '  6
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'EngName',
        '  167,
        '  100,
        '  3
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'ID',
        '  56,
        '  4,
        '  1
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'RegComputerID',
        '  56,
        '  4,
        '  10
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'RegDate',
        '  58,
        '  4,
        '  11
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'RegUserID',
        '  56,
        '  4,
        '  9
        ' )

        'INSERT INTO sys_Fields
        '(ObjectID,
        ' FieldName,
        ' FieldType,
        ' FieldLength,
        ' SysColumns_OrderID)
        ' VALUES
        ' ((SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        '  'Remarks',
        '  167,
        '  2048,
        '  8
        ' )
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF((SELECT COUNT(*) FROM sys_SearchsColumns WHERE SearchID = (SELECT ID FROM sys_Searchs 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')) = 0)
        'BEGIN
        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'ID' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'ID',
        ' 'ID',
        ' 0,
        ' 0
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView,
        ' Rank)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'Code' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'Code',
        ' 'ÇáßæÏ',
        ' 1,
        ' 1,
        ' 0
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView,
        ' Rank)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'EngName' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'English Name',
        ' 'ÇÓã ÇáÇäÌáíÒí',
        ' 1,
        ' 1,
        ' 1
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView,
        ' Rank)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'ArbName' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'Arabic Name',
        ' 'ÇáÇÓã ÇáÚÑÈí',
        ' 1,
        ' 1,
        ' 2
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'BeginDate' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'Begin Date',
        ' 'ãä ÊÇÑíÎ',
        ' 0,
        ' 0
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'EndDate' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'End Date',
        ' 'Åáì ÊÇÑíÎ',
        ' 0,
        ' 0
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'EmployeeClassID' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'Employee Class ID',
        ' 'ÎØÉ ÇáÏæÇã',
        ' 0,
        ' 0
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView,
        ' Rank)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'Remarks' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'Remarks',
        ' 'ãáÇÍÙÇÊ',
        ' 1,
        ' 1,
        ' 3
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'RegUserID' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'RegUserID',
        ' 'RegUserID',
        ' 0,
        ' 0
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'RegComputerID' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'RegComputerID',
        ' 'RegComputerID',
        ' 0,
        ' 0
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'RegDate' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'RegDate',
        ' 'RegDate',
        ' 0,
        ' 0
        ')

        'INSERT INTO sys_SearchsColumns
        '(SearchID,
        ' FieldID,
        ' EngName,
        ' ArbName,
        ' IsCriteria,
        ' IsView)
        'VALUES
        '((SELECT ID FROM sys_Searchs WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize'),
        ' (SELECT ID FROM sys_Fields WHERE FieldName = 'CancelDate' AND ObjectID = (SELECT ID FROM sys_Objects 
        '		WHERE Code = 'hrs_EmployeesClassesWeeklyCustomize')),
        ' 'CancelDate',
        ' 'CancelDate',
        ' 0,
        ' 0
        ')
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER  PROCEDURE [dbo].[Att_GetCustomAttandace]
        ' @EmployeeID     AS INT, 
        ' @Attandancedate AS DATE
        'AS
        'SET DATEFIRST 6

        'IF((SELECT COUNT(*) FROM hrs_EmployeesClassesWeeklyCustomize WHERE EmployeeID = @EmployeeID
        '	AND @Attandancedate BETWEEN BeginDate AND EndDate) > 0)
        'BEGIN
        'SET DATEFORMAT dmy
        '     SELECT AttShD. DayNo                                AS DayNo,                             
        '            AttShD.TimeIn1                               AS TimeIn, 
        '            AttShD.TimeOut1                              AS TimeOut, 
        '            AttShD.TimeIn2                               AS TimeIn2nd, 
        '            AttShD.TimeOut2                              AS TimeOut2nd, 
        '            AttShD.IsDayOff                              AS IsDayOff
        '			--AttSh.FirstShiftTimeInFingerprintStart       AS FirstShiftTimeInFingerprintStart,
        '			--AttSh.FirstShiftEntryTimeInClose             AS FirstShiftEntryTimeInClose,
        '			--AttSh.FirstShiftTimeOutFingerprintClose      AS FirstShiftTimeOutFingerprintClose,
        '			--AttSh.SecondShiftTimeInFingerprintStart      AS SecondShiftTimeInFingerprintStart,
        '			--AttSh.SecondShiftEntryTimeInClose            AS SecondShiftEntryTimeInClose,
        '			--AttSh.SecondShiftTimeOutFingerprintClose     AS SecondShiftTimeOutFingerprintClose
        '     FROM hrs_EmployeesClassesWeeklyCustomizeDays AttShD
        '          INNER JOIN hrs_EmployeesClassesWeeklyCustomize AttSh ON Attsh.id = AttShD.CustomizeID

        '     WHERE AttSh.EmployeeID = @EmployeeID
        '           AND @Attandancedate BETWEEN AttSh.BeginDate AND AttSh.EndDate
        '		   AND  DATEPART(dw, @Attandancedate) = DayNo
        '		   AND AttSh.CancelDate IS NULL
        '     ORDER BY AttSh.RegDate DESC
        'END
        'ELSE IF((SELECT COUNT(*) FROM hrs_EmployeesClassesWeeklyCustomizeDays AttShD
        '          INNER JOIN hrs_EmployeesClassesWeeklyCustomize AttSh ON Attsh.id = Attshd.CustomizeID
        '          INNER JOIN Att_AttendAppointment AttApp ON AttApp.AttendaceShiftID = attsh.EmployeeClassID
        '          INNER JOIN Att_AttendAppointmentMembers AttAppM ON AttappM.AppointID = AttApp.ID
        '     WHERE AttAppM.EmployeeID = @EmployeeID
        '           AND @Attandancedate BETWEEN AttApp.FromDate AND AttApp.ToDate
        '		   AND  DATEPART(dw, @Attandancedate) = DayNo
        '		   AND AttAppM.CancelDate IS NULL) > 0)
        'BEGIN
        ' SET DATEFORMAT dmy
        '     SELECT AttShD. DayNo                                AS DayNo,                             
        '            AttShD.TimeIn1                               AS TimeIn, 
        '            AttShD.TimeOut1                              AS TimeOut, 
        '            AttShD.TimeIn2                               AS TimeIn2nd, 
        '            AttShD.TimeOut2                              AS TimeOut2nd, 
        '            AttShD.IsDayOff                              AS IsDayOff
        '			--AttSh.FirstShiftTimeInFingerprintStart       AS FirstShiftTimeInFingerprintStart,
        '			--AttSh.FirstShiftEntryTimeInClose             AS FirstShiftEntryTimeInClose,
        '			--AttSh.FirstShiftTimeOutFingerprintClose      AS FirstShiftTimeOutFingerprintClose,
        '			--AttSh.SecondShiftTimeInFingerprintStart      AS SecondShiftTimeInFingerprintStart,
        '			--AttSh.SecondShiftEntryTimeInClose            AS SecondShiftEntryTimeInClose,
        '			--AttSh.SecondShiftTimeOutFingerprintClose     AS SecondShiftTimeOutFingerprintClose
        '     FROM hrs_EmployeesClassesWeeklyCustomizeDays AttShD
        '          INNER JOIN hrs_EmployeesClassesWeeklyCustomize AttSh ON Attsh.id = Attshd.CustomizeID
        '          INNER JOIN Att_AttendAppointment AttApp ON AttApp.AttendaceShiftID = attsh.EmployeeClassID
        '          INNER JOIN Att_AttendAppointmentMembers AttAppM ON AttappM.AppointID = AttApp.ID
        '     WHERE AttAppM.EmployeeID = @EmployeeID
        '           AND @Attandancedate BETWEEN AttApp.FromDate AND AttApp.ToDate
        '		   AND  DATEPART(dw, @Attandancedate) = DayNo
        '		   AND AttAppM.CancelDate IS NULL
        '     ORDER BY AttApp.RegDate DESC
        'END
        'ELSE 
        'BEGIN
        'SET DATEFORMAT dmy
        '     SELECT AttShD. DayNo                                AS DayNo,                             
        '            AttShD.TimeIn                                AS TimeIn, 
        '            AttShD.TimeOut                               AS TimeOut, 
        '            AttShD.TimeIn2nd                             AS TimeIn2nd, 
        '            AttShD.TimeOut2nd                            AS TimeOut2nd, 
        '            AttShD.IsDayOff                              AS IsDayOff,
        '			AttSh.FirstShiftTimeInFingerprintStart       AS FirstShiftTimeInFingerprintStart,
        '			AttSh.FirstShiftEntryTimeInClose             AS FirstShiftEntryTimeInClose,
        '			AttSh.FirstShiftTimeOutFingerprintClose      AS FirstShiftTimeOutFingerprintClose,
        '			AttSh.SecondShiftTimeInFingerprintStart      AS SecondShiftTimeInFingerprintStart,
        '			AttSh.SecondShiftEntryTimeInClose            AS SecondShiftEntryTimeInClose,
        '			AttSh.SecondShiftTimeOutFingerprintClose     AS SecondShiftTimeOutFingerprintClose
        '     FROM Att_AttendShiftDays AttShD
        '          INNER JOIN Att_AttendShifts AttSh ON Attsh.id = Attshd.AttendShiftID
        '          INNER JOIN Att_AttendAppointment AttApp ON AttApp.AttendaceShiftID = attsh.id
        '          INNER JOIN Att_AttendAppointmentMembers AttAppM ON AttappM.AppointID = AttApp.ID
        '     WHERE AttAppM.EmployeeID = @EmployeeID
        '           AND @Attandancedate BETWEEN AttApp.FromDate AND AttApp.ToDate
        '		   AND  DATEPART(dw, @Attandancedate) = DayNo
        '		   AND AttAppM.CancelDate IS NULL
        '     ORDER BY AttApp.RegDate DESC
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClasses', 'VacCostFormula') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClasses
        '    ADD VacCostFormula varchar(2048)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_ViolationCategory'
        ')
        'BEGIN
        'CREATE TABLE [dbo].[hrs_ViolationCategory](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EngName] [varchar](100) NULL,
        '	[ArbName] [varchar](100) NULL,
        '	[ArbName4S] [varchar](100) NULL,
        '	[Remarks] [varchar](2048) NULL,
        '	[RegUserID] [int] NOT NULL,
        '	[RegComputerID] [int] NULL,
        '	[RegDate] [smalldatetime] NOT NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_ViolationCategory] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]

        'ALTER TABLE [dbo].[hrs_ViolationCategory] ADD  CONSTRAINT [DF_hrs_ViolationCategory_RegDate]  DEFAULT (getdate()) FOR [RegDate]

        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_ViolationTypes'
        ')
        'BEGIN
        'CREATE TABLE [dbo].[hrs_ViolationTypes](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NULL,
        '	[EngName] [varchar](100) NULL,
        '	[ArbName] [varchar](100) NULL,
        '	[ArbName4s] [varchar](100) NULL,
        '	[ViolationCategoryID] [int] NOT NULL,
        '	[BranchID] [int] NOT NULL,
        '	[Vfrom] [float] NOT NULL,
        '	[Vto] [float] NOT NULL,
        '	[IsObstructionOther] [bit] NOT NULL,
        '	[Remarks] [varchar](2048) NULL,
        '	[RegUserID] [int] NOT NULL,
        '	[RegComputerId] [int] NULL,
        '	[RegDate] [smalldatetime] NOT NULL,
        '	[CancelDate] [smalldatetime] NULL,
        '	[ActiveDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_ViolationTypes] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]

        'ALTER TABLE [dbo].[hrs_ViolationTypes] ADD  CONSTRAINT [DF_hrs_ViolationTypes_RegDate]  DEFAULT (getdate()) FOR [RegDate]

        'ALTER TABLE [dbo].[hrs_ViolationTypes]  WITH NOCHECK ADD  CONSTRAINT [FK_hrs_ViolationTypes_hrs_ViolationCategory] FOREIGN KEY([ViolationCategoryID])
        'REFERENCES [dbo].[hrs_ViolationCategory] ([ID])

        'ALTER TABLE [dbo].[hrs_ViolationTypes] CHECK CONSTRAINT [FK_hrs_ViolationTypes_hrs_ViolationCategory]

        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_ViolationPenalties'
        ')
        'BEGIN
        'CREATE TABLE [dbo].[hrs_ViolationPenalties](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[EngName] [varchar](100) NULL,
        '	[ArbName] [varchar](100) NULL,
        '	[ArbName4S] [varchar](100) NULL,
        '	[ViolationTypeId] [int] NOT NULL,
        '	[NumberOfTimes] [int] NOT NULL,
        '	[DeductionPercentage] [float] NOT NULL,
        '	[Remarks] [varchar](2048) NULL,
        '	[RegUserID] [int] NULL,
        '	[RegComputerId] [int] NULL,
        '	[RegDate] [smalldatetime] NOT NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_ViolationPenalties] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]

        'ALTER TABLE [dbo].[hrs_ViolationPenalties] ADD  CONSTRAINT [DF_hrs_ViolationPenalties_RegDate]  DEFAULT (getdate()) FOR [RegDate]

        'ALTER TABLE [dbo].[hrs_ViolationPenalties]  WITH NOCHECK ADD  CONSTRAINT [FK_hrs_ViolationPenalties_hrs_ViolationTypes] FOREIGN KEY([ViolationTypeId])
        'REFERENCES [dbo].[hrs_ViolationTypes] ([ID])
        'ON DELETE CASCADE

        'ALTER TABLE [dbo].[hrs_ViolationPenalties] CHECK CONSTRAINT [FK_hrs_ViolationPenalties_hrs_ViolationTypes]

        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClasses', 'HasFingerPrint') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClasses
        '    ADD HasFingerPrint bit 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('sys_DocumentsDetails', 'CombID') IS NULL  
        'BEGIN
        '    ALTER TABLE sys_DocumentsDetails
        '    ADD CombID nvarchar(250) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('sys_ObjectsAttachments', 'CombID') IS NULL  
        'BEGIN
        '    ALTER TABLE sys_ObjectsAttachments
        '    ADD CombID nvarchar(250) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesTransactions', 'RemainVacSettlement') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesTransactions
        '    ADD RemainVacSettlement money
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER PROCEDURE [dbo].[GetAllEmployeePreviousVacations]
        ' @EmployeeID int
        'AS

        'SET DATEFORMAT dmy
        'SELECT
        ' ID, 
        ' PaidDate,
        ' FinancialWorkingUnits PaidDays,
        ' (ISNULL((SELECT SUM(NumericValue)
        '	      FROM hrs_EmployeesTransactionsDetails
        '		  INNER JOIN hrs_EmployeesTransactionsProjects 
        '		  ON EmpTransProjID = hrs_EmployeesTransactionsProjects.ID
        '		  INNER JOIN hrs_TransactionsTypes 
        '		  ON TransactionTypeID = hrs_TransactionsTypes.ID
        '		  WHERE hrs_EmployeesTransactionsProjects.EmployeeTransactionID =hrs_EmployeesTransactions.ID
        '		  AND [Sign] > 0
        '		  AND IsPaid = 1)
        '		  ,0)
        '			-
        '  ISNULL((SELECT SUM(NumericValue)
        '		  From hrs_EmployeesTransactionsDetails
        '		  INNER JOIN hrs_EmployeesTransactionsProjects 
        '		  On EmpTransProjID = hrs_EmployeesTransactionsProjects.ID
        '		  INNER JOIN hrs_TransactionsTypes 
        '		  On TransactionTypeID = hrs_TransactionsTypes.ID
        '		  WHERE hrs_EmployeesTransactionsProjects.EmployeeTransactionID =hrs_EmployeesTransactions.ID
        '		  AND [Sign] <0
        '		  AND IsPaid = 1)
        '		  ,0)		)AS Amount
        'FROM hrs_EmployeesTransactions 
        'WHERE EmployeeID = @EmployeeID
        'AND CancelDate IS NULL
        'AND ( PrepareType = 'V' )
        'ORDER BY PaidDate

        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER   PROCEDURE [dbo].[GetEmployeeLastVacationSettlement]
        '	@EmployeeID Int
        'As
        'SET DateFormat dmy
        'SELECT TOP 1
        '		ID, 
        '		PaidDate,
        '		FinancialWorkingUnits PaidDays,
        '		(
        '			IsNull((
        '				SELECT SUM(NumericValue)
        '				FROM hrs_EmployeesTransactionsDetails
        '					INNER JOIN hrs_EmployeesTransactionsProjects 
        '					ON EmpTransProjID = hrs_EmployeesTransactionsProjects.ID
        '					INNER JOIN hrs_TransactionsTypes 
        '					ON TransactionTypeID = hrs_TransactionsTypes.ID
        '				WHERE 
        '					hrs_EmployeesTransactionsProjects.EmployeeTransactionID = hrs_EmployeesTransactions.ID
        '					AND [Sign] > 0
        '					AND IsPaid = 1
        '			),0)
        '			-
        '			IsNull((
        '				SELECT SUM(NumericValue)
        '				From hrs_EmployeesTransactionsDetails
        '					INNER JOIN hrs_EmployeesTransactionsProjects 
        '					ON EmpTransProjID = hrs_EmployeesTransactionsProjects.ID
        '					INNER JOIN hrs_TransactionsTypes 
        '					ON TransactionTypeID = hrs_TransactionsTypes.ID
        '				WHERE 
        '					hrs_EmployeesTransactionsProjects.EmployeeTransactionID = hrs_EmployeesTransactions.ID
        '					AND [Sign] < 0
        '					AND IsPaid = 1
        '			),0)	
        '		)AS Amount,
        '		TotalVacDaySettlement,
        '		RemainVacDaySettlement,
        '		RemainVacSettlement
        '	From
        '		hrs_EmployeesTransactions 
        '	WHERE
        '		EmployeeID = @EmployeeID
        '		AND CancelDate IS NULL
        '		AND PrepareType = 'V'
        '	ORDER BY 
        '		PaidDate DESC,
        '		ID DESC


        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeeVacationOpenBalance', 'Days') IS NOT NULL  
        'BEGIN
        '	IF((SELECT DATA_TYPE 
        '		FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'hrs_EmployeeVacationOpenBalance' 
        '		AND COLUMN_NAME = 'Days') <> 'decimal')
        '		BEGIN
        '			ALTER TABLE hrs_EmployeeVacationOpenBalance
        '			ALTER COLUMN Days decimal(5,2)
        '		END
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesFingerprints', 'FingerprintTime') IS NOT NULL  
        'BEGIN
        ' IF((SELECT DATA_TYPE 
        '  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'hrs_EmployeesFingerprints' 
        '  AND COLUMN_NAME = 'FingerprintTime') <> 'datetime')
        '	BEGIN
        '		ALTER TABLE hrs_EmployeesFingerprints
        '		ALTER COLUMN FingerprintTime datetime
        '	END
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER Proc [dbo].[hrs_FingerprintForCurrntMonth]
        '@FingerprintID int
        'AS
        'BEGIN
        ' DECLARE
        '	@ServerIP Varchar(50),
        '	@DatabaseName Varchar(50),
        '	@FingerprintTableName Varchar(50),
        '	@UserIdColumnName Varchar(50),
        '	@CheckInOutColumnName Varchar(50),
        '	@Query NVARCHAR (MAX),
        '	@LastDateFingerPrint DateTime,
        '	@UsersTableIdntity Varchar(50),
        '	@UserMatchColumnName Varchar(50),
        '	@UsersTableName Varchar(50)

        'SELECT 
        '	@ServerIP = FingerprintServerIP,
        '	@DatabaseName = FingerprintDatabaseName,
        '	@FingerprintTableName = FingerprintTableName,
        '	@UserIdColumnName = UserIdColumnName,
        '	@CheckInOutColumnName = CheckInOutColumnName,
        '	@UsersTableIdntity = UsersTableIdntity,
        '	@UserMatchColumnName = UserMatchColumnName,
        '	@UsersTableName = UsersTableName
        'FROM hrs_FingerprintSettings
        'WHERE ID = @FingerprintID

        'IF EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesFingerprints'
        ')
        'BEGIN
        '	DECLARE @result TABLE ([FingerprintTime] datetime);

        '	INSERT INTO @result ([FingerprintTime])
        '	EXEC (N'SELECT TOP (1) [FingerprintTime] FROM [hrs_EmployeesFingerprints] WHERE [FingerprintID] = ' + @FingerprintID
        '	    + N' ORDER BY [FingerprintTime] DESC');

        '	SET @LastDateFingerPrint = (SELECT TOP (1) [FingerprintTime] FROM @result);

        '	SET @Query = NULL
        '	SET @Query = N'INSERT INTO hrs_EmployeesFingerprints ([UserID ],'
        '			   + N'[FingerprintTime], [UserCode], [FingerprintID])'
        '			   + N'SELECT fp.[' + @UserIdColumnName + N'],'
        '	           + N' fp.[' + @CheckInOutColumnName + N'],'
        '			   + N' us.[' + @UserMatchColumnName + N'], '
        '			   + CAST(@FingerprintID AS NVARCHAR(10))
        '			   + N' FROM [' + @DatabaseName + N']'
        '			   + N'.[dbo].[' + @FingerprintTableName + N'] AS fp'
        '			   + N' INNER JOIN [' + @DatabaseName + N'].[dbo].[' + @UsersTableName + N'] AS us'
        '			   + N' ON fp.[' + @UserIdColumnName + N'] = us.[' + @UsersTableIdntity + N']'


        '	IF(@LastDateFingerPrint IS NOT NULL)
        '		BEGIN
        '			DECLARE @ConvertLastDate varchar(20) 
        '			SELECT @ConvertLastDate  = CONVERT(VARCHAR(20), @LastDateFingerPrint, 120)
        '			SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] > ''' + @ConvertLastDate + N''''
        '			+  N' AND fp.[' + @CheckInOutColumnName + N'] BETWEEN ''1900-01-01'' AND ''2079-06-06'' '
        '		END
        '	ELSE
        '		BEGIN
        '			SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] > GETDATE()-60'
        '			+  N' AND fp.[' + @CheckInOutColumnName + N'] BETWEEN ''1900-01-01'' AND ''2079-06-06'' '
        '		END
        '		PRINT(@Query)
        '		EXEC (@Query)
        '    END
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClassesWeeklyCustomizeDays', 'FirstShiftTimeInFingerprintStart') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClassesWeeklyCustomizeDays
        '    ADD FirstShiftTimeInFingerprintStart varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClassesWeeklyCustomizeDays', 'FirstShiftEntryTimeInClose') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClassesWeeklyCustomizeDays
        '    ADD FirstShiftEntryTimeInClose varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClassesWeeklyCustomizeDays', 'FirstShiftTimeOutFingerprintClose') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClassesWeeklyCustomizeDays
        '    ADD FirstShiftTimeOutFingerprintClose varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClassesWeeklyCustomizeDays', 'SecondShiftTimeInFingerprintStart') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClassesWeeklyCustomizeDays
        '    ADD SecondShiftTimeInFingerprintStart varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClassesWeeklyCustomizeDays', 'SecondShiftEntryTimeInClose') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClassesWeeklyCustomizeDays
        '    ADD SecondShiftEntryTimeInClose varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClassesWeeklyCustomizeDays', 'SecondShiftTimeOutFingerprintClose') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClassesWeeklyCustomizeDays
        '    ADD SecondShiftTimeOutFingerprintClose varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesExcuses', 'ExcuseType') IS NOT NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesExcuses
        '    ALTER COLUMN ExcuseType varchar(10) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_EmployeesExcuses', 'ExcuseHours') IS NOT NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesExcuses
        '    ALTER COLUMN ExcuseHours varchar(5) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClasses', 'HasOvertimeList') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClasses
        '    ADD HasOvertimeList Bit 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_Employees', 'BankAccountType') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_Employees
        '    ADD BankAccountType nvarchar(50) 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER PROCEDURE [dbo].[GetAllPreparedEmployeesForSalariesByEmployeeID] 
        '	@FiscalPeriodID As Int,
        '	@RegUserID      As Int,
        '	@GroupID		As Int,
        '	@EmployeeID		As Int,
        '	@FiscalPeriodStart	smalldatetime ,
        '    @FiscalPeriodEnd	smalldatetime ,
        '    @OrgFiscalPeriodStart	smalldatetime ,
        '    @OrgFiscalPeriodEnd	smalldatetime 
        'AS

        'Set Dateformat DMY
        'Declare @FiscalYearID		int
        'Declare @FiscalYearStart	smalldatetime 
        'Declare @FiscalYearEnd		smalldatetime 
        'Declare @Prepared			bit

        'Set		@FiscalPeriodID		= @FiscalPeriodID
        'Set		@FiscalYearID		= (Select FiscalYearID		From sys_FiscalYearsPeriods Where id = @FiscalPeriodID)	

        'Set		@FiscalYearStart	= (Select Top 1 FromDate	From sys_FiscalYears	Inner Join sys_FiscalYearsPeriods On sys_FiscalYears.ID =sys_FiscalYearsPeriods.FiscalyearID Where sys_FiscalYears.ID= @FiscalYearID	Order By FromDate	Asc)
        'Set		@FiscalYearEnd		= (Select Top 1 todate		From sys_FiscalYears	Inner Join sys_FiscalYearsPeriods On sys_FiscalYears.ID =sys_FiscalYearsPeriods.FiscalyearID Where sys_FiscalYears.ID= @FiscalYearID	Order By ToDate		Desc)

        'Select 
        '	e.ID,
        '	e.Code,
        '	IsNull( e.EngName,'') + ',' + IsNull( e.FatherEngName,'') + ',' + IsNull( e.GrandEngName,'') + ',' + IsNull( e.FamilyEngName,'')																							As	FullEngName,
        '	IsNull( e.ArbName,'') + ',' + IsNull( e.FatherArbName,'') + ',' + IsNull( e.GrandArbName,'') + ',' + IsNull( e.FamilyArbName,'')																							As	FullArbName,
        '	Case When (Select count(ID) From hrs_EmployeesTransactions Where IsNull(CancelDate,'')=''  And  Employeeid = e.ID And FiscalYearPeriodID = @FiscalPeriodID And PrepareType ='N')>0	Then 1	Else 0 End						As	Prepared,
        '	Case When ((Select Count(ID) From hrs_Contracts Co Where Co.EmployeeID = e.ID And Co.CancelDate is null) = 1 and (Select Count(ID) From hrs_EmployeesJoins jo Where jo.EmployeeID = e.ID And jo.CancelDate is null) = 1 And (e.JoinDate >= @OrgFiscalPeriodStart)) or ((Select Count(ID) From hrs_EmployeesJoins jo Where jo.EmployeeID = e.ID And jo.CancelDate is null) > 1 And (select MIN(JoinDate) from hrs_EmployeesJoins where EndOfServiceDate is null And CancelDate is null And EmployeeID = e.ID) >= @OrgFiscalPeriodStart) Then 0	Else 2 End	As	FirstPrepare,




        '	(Select IsNull(Sum(DurationDays),0) From hrs_ContractsVacations Inner Join hrs_VacationsTypes On hrs_ContractsVacations.VacationTypeID	= hrs_vacationsTypes.id  Where ContractID = hrs_Contracts.ID And IsPaid = 1)		
        '	* (Case When WorkingUnitsIsHours = 1 Then WorkHoursPerDay Else 1 End )
        '	As AvailableVacation,
        '	((dbo.fn_GetVacationDuringPeriod(e.ID,@FiscalYearStart,@FiscalYearEnd,WorkHoursperDay,-1)	+																																	
        '	Case When 
        '			((Select IsNull(Sum(DurationDays),0) From hrs_ContractsVacations Inner Join hrs_VacationsTypes On hrs_ContractsVacations.VacationTypeID	= hrs_vacationsTypes.id  Where ContractID = hrs_Contracts.ID And IsPaid = 1)
        '			 -
        '			dbo.fn_GetVacationDuringPeriod(e.ID,@FiscalYearStart,@FiscalYearEnd,WorkHoursperDay,1))	< 0 
        '		 Then
        '			(dbo.fn_GetVacationDuringPeriod(e.ID,@FiscalYearStart,@FiscalYearEnd,WorkHoursperDay,1)
        '			-
        '			(Select IsNull(Sum(DurationDays),0) From hrs_ContractsVacations Inner Join hrs_VacationsTypes On hrs_ContractsVacations.VacationTypeID	= hrs_vacationsTypes.id  Where ContractID = hrs_Contracts.ID And IsPaid = 1))
        '		 Else		
        '			0
        '		 End)) * (Case When WorkingUnitsIsHours = 1 Then WorkHoursPerDay Else 1 End )	As TotalPenalty,
        '			-- Calculate the Contracted Days 


        '			(IsNull((DateDiff(hh,@OrgFiscalPeriodStart,case day(@OrgFiscalPeriodEnd) when 31 then dateadd(day,-1,@OrgFiscalPeriodEnd) else @OrgFiscalPeriodEnd end)/24)+1,NoOfDaysPerPeriod) -

        '			Case When (IsNull(hrs_Contracts.EndDate,'01/01/2070') Between @OrgFiscalPeriodStart And @OrgFiscalPeriodEnd And Not hrs_Contracts.StartDate Between @OrgFiscalPeriodStart And @OrgFiscalPeriodEnd) Then (DateDiff(hh,@OrgFiscalPeriodStart,@OrgFiscalPeriodEnd) / 24) - (Datediff(hh,@OrgFiscalPeriodStart,hrs_Contracts.Enddate) / 24)	Else 
        '			Case When (hrs_contracts.StartDate Between @OrgFiscalPeriodStart And @OrgFiscalPeriodEnd And Not IsNull(hrs_Contracts.EndDate,'01/01/2070') Between @OrgFiscalPeriodStart And @OrgFiscalPeriodEnd)Then (DateDiff(hh,@OrgFiscalPeriodStart,@OrgFiscalPeriodEnd) / 24) - (Datediff(hh,hrs_Contracts.StartDate, @OrgFiscalPeriodEnd ) / 24)	Else  
        '			Case When (hrs_contracts.StartDate Between @OrgFiscalPeriodStart And @OrgFiscalPeriodEnd And IsNull(hrs_Contracts.EndDate,'01/01/2070') Between @OrgFiscalPeriodStart And @OrgFiscalPeriodEnd)	Then (DateDiff(hh,@OrgFiscalPeriodStart,@OrgFiscalPeriodEnd) / 24) - (DateDiff(hh,hrs_Contracts.StartDate,hrs_Contracts.EndDate) /24)	Else
        '						0
        '			End
        '			End
        '			End) * (Case When WorkingUnitsIsHours = 1 Then WorkHoursPerDay Else 1 End )																																																			
        '			  As ContractedDays,
        '				 Case When (IsNull(hrs_Contracts.EndDate,'01/01/2070') Between @OrgFiscalPeriodStart And @OrgFiscalPeriodEnd And Not hrs_Contracts.StartDate Between @OrgFiscalPeriodStart And @OrgFiscalPeriodEnd) Then 1	Else
        '				 Case When (hrs_contracts.StartDate > @OrgFiscalPeriodStart And hrs_contracts.StartDate <= @OrgFiscalPeriodEnd And Not IsNull(hrs_Contracts.EndDate,'01/01/2070') Between @OrgFiscalPeriodStart And @OrgFiscalPeriodEnd)Then 1	Else  
        '				 Case When (hrs_contracts.StartDate > @OrgFiscalPeriodStart And hrs_contracts.StartDate <= @OrgFiscalPeriodEnd And IsNull(hrs_Contracts.EndDate,'01/01/2070') Between @OrgFiscalPeriodStart And @OrgFiscalPeriodEnd)	Then 1 Else
        '				 Case When (select COUNT(ID) from hrs_EmployeesVacations where CancelDate IS NULL and EmployeeID = e.ID and ActualStartDate <@OrgFiscalPeriodStart and IsNull(ActualEndDate,'01/01/2070') > @OrgFiscalPeriodStart and VacationTypeID in(select ID from hrs_VacationsTypes where IsAnnual = 1)) > 0 Then (DateDiff(hh,@FiscalperiodStart,@FiscalPeriodEnd)/24)+1
        '				 Else 0
        '				       End
        '					End
        '				End
        '			End AS CalcType
        'From    
        '		hrs_Employees e
        '		Inner Join hrs_Contracts			On hrs_Contracts.EmployeeID = e.ID 
        '		Left  Join hrs_EmployeesClasses		On hrs_EmployeesClasses.ID	= hrs_Contracts.EmployeeClassID
        'Where 
        '		IsNull(e.CancelDate,'')=''
        '		And  IsNull(hrs_Contracts.CancelDate,'')=''	
        '		And	 hrs_Contracts.StartDate <= @OrgFiscalPeriodEnd 
        '		And (hrs_Contracts.EndDate is null or  hrs_Contracts.EndDate > @OrgFiscalPeriodStart)
        '		And (select Count(v.ID) as VCount	From	hrs_EmployeesVacations  v	
        '		LEFT JOIN hrs_VacationsTypes vt ON v.VacationTypeID = vt.ID
        '		Where	IsNull(v.CancelDate,'')=''	And	v.EmployeeID = e.ID	
        '		AND vt.IsAnnual <> 0
        '		AND vt.IsPaid <> 1
        '		And	Convert(smalldatetime,Convert(varchar,v.ActualStartDate ,103)) <= Convert(smalldatetime,Convert(varchar,@OrgFiscalPeriodStart ,103))	And	(v.ActualEndDate Is Null Or  Convert(smalldatetime,Convert(varchar,v.ActualEndDate ,103)) > Convert(smalldatetime,Convert(varchar,@OrgFiscalPeriodEnd ,103))))=0
        '		And e.ID = @EmployeeID
        'Order By 
        '		e.Code
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE FUNCTION [dbo].[AttendanceEffects]
        '(
        '  @EmpID int,
        '  @AttandanceFromDate date,
        '  @AttandanceToDate date,
        '  @intFiscalperiod int,
        '  @IsToAttandanceToDate bit = 0,
        '  @ToAttendDate date,
        '  @isEndOfservice bit = 0,
        '  @FiscalFromdDate date,
        '  @FiscalToDate date
        ')
        'RETURNS @Attendace TABLE
        '   (
        '		WorkingDays int,
        '		AbsentDays  int,
        '		vactiondays int
        '   )
        'AS
        'BEGIN
        '		--declare		@FiscalFromdDate as date=(SELECT TOP 1 fromdate FROM sys_FiscalYearsPeriods WHERE id=@intFiscalperiod)
        '		--declare		@FiscalToDate as date=(SELECT TOP 1 ToDate  FROM sys_FiscalYearsPeriods WHERE id=@intFiscalperiod)
        '		DECLARE		@WorkingDays int	= 0,
        '					@AbsentDays int		= 0,
        '					@vactiondays int	= 0,
        '					@AcutalStartVaction date,
        '					@AcutalEndVaction date,
        '					@joindateDaysDeduction int = 0,
        '					@EndOfserviceDaysDetuction int = 0,
        '					@AttToEndDays int = 0,
        '					@contractID int		= (SELECT TOP 1 id FROM hrs_Contracts
        '					                        WHERE EmployeeID = @EmpID
        '											ORDER BY StartDate DESC)

        '		   DECLARE @NODPP int			= (SELECT TOP 1 NoOfDaysPerPeriod
        '											FROM hrs_EmployeesClasses Ec INNER JOIN hrs_Contracts c
        '											ON c.EmployeeClassID = Ec.ID
        '											WHERE c.ID = @contractID),

        '				@JoinDate date		= (SELECT  joindate FROM hrs_employees WHERE id = @EmpID ),
        '				@lastDayOfMonth int = 30 --DAY((SELECT DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, @FiscalToDate) + 1, 0))))
        '		--IF @NODPP =30
        '		--	BEGIN
        '				SET @WorkingDays = 30
        '				--======== joindate ==============================		{
        '				IF @JoinDate > @FiscalFromdDate
        '				BEGIN
        '				SET @joindateDaysDeduction = DATEDIFF(DAY, @FiscalFromdDate, @JoinDate)
        '				IF((DATEDIFF(DAY, @FiscalFromdDate, @FiscalToDate) + 1) = 31)
        '				    SET @WorkingDays = 30
        '				ELSE
        '					SET @WorkingDays = @lastDayOfMonth
        '				END
        '				--=================================================		}

        '				--================= END Of service ==================		{
        '				IF	   @isEndOfservice = 1
        '				BEGIN
        '				 SET @EndOfserviceDaysDetuction = @WorkingDays - DATEDIFF(DAY,@FiscalFromdDate,@AttandanceToDate) - 1
        '				END
        '				--=================================================		}

        '				--================== Vaction =======================	{	
        '				-- start vaction date smaller than fiscal strat date AND return vaction is null or greater than fiscal END date
        '				--01
        '					--Ýì ÍÇáÉ Çä ÇáÇÌÇÒÉ ÈÏÃÊ ÞÈá ÇáÝÊÑÉ ÇáãÇáíÉ æÇáÚæÏÉ áíÓÊ Ýì ÇáÝÊÑÉ ÇáãÇáíÉ --
        '					-- ÚÏÏ ÇáÇíÇã ÇáÇÌÇÒÉ äÝÓ ÇíÇã ÇáÚãá 
        '				IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '				           WHERE EmployeeID = @EmpID
        '						   AND  CancelDate IS NULL
        '						   AND ActualStartDate  < @FiscalFromdDate
        '						   AND (ActualEndDate IS NULL OR ActualEndDate > @Fiscaltodate )
        '						   AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '													WHERE  IsAnnual = 1
        '													or  IsPaid = -1)
        '												                                               )
        '					BEGIN 

        '						SET @VactionDays = @WorkingDays
        '					END
        '					--=================================================		}
        '				-- StartVaction Greeter than Fiscal start date AND acutl END vaction is null or greeter than ficalto date
        '				--02
        '				-- Ýì ÍÇáÉ ÇáÇÌÇÒÉ ÏÇÎá ÇáÝÊÑÉ ÇáãÇáíÉ æÇáÚæÏÉ ÎÇÑÌ ÇáÝÊÑÉ ÇáãÇáíÉ --
        '					IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '					            WHERE EmployeeID = @EmpID
        '								AND  CancelDate IS NULL
        '								AND ActualStartDate  >= @FiscalFromdDate
        '								AND ActualStartDate  <= @Fiscaltodate
        '								AND (ActualEndDate IS NULL OR ActualEndDate > @Fiscaltodate )
        '									AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '					BEGIN
        '						SET @AcutalStartVaction = (SELECT TOP 1 ActualStartDate FROM hrs_EmployeesVacations
        '						                           WHERE EmployeeID = @EmpID
        '												   AND  CancelDate IS NULL
        '												   AND ActualStartDate  >= @FiscalFromdDate
        '												   AND ActualStartDate  <= @Fiscaltodate
        '												   AND (ActualEndDate IS NULL OR ActualEndDate > @Fiscaltodate )
        '														AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '						IF @JoinDate >= @FiscalFromdDate AND @JoinDate <= @FiscalToDate
        '						SET @VactionDays = @WorkingDays - DATEDIFF (DAY,@JoinDate,@AcutalStartVaction)
        '						ELSE
        '						SET @VactionDays = @WorkingDays - DATEDIFF (DAY,@FiscalFromdDate ,@AcutalStartVaction)
        '					END
        '				--================================================		}
        '				-- EndVactiondate Smaller than Fiscal END date AND Start  vaction is not BETWEEN acual start date AND acual END date
        '				--03
        '				-- ÇáÚæÏÉ ãä ÇáÇÌÇÒÉ ÏÇÎá ÇáÝÊÑÉ æáßä ÈÏÇíÉ ÇáÇÌÇÒÉ áíÓÊ Öãä ÇáÝÑÉ 
        '				IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '				           WHERE EmployeeID=@EmpID
        '						   AND CancelDate IS NULL
        '						   AND ActualEndDate > @FiscalFromdDate
        '						   AND ActualEndDate <= @Fiscaltodate
        '						   AND ActualStartDate NOT BETWEEN  @FiscalFromdDate AND @Fiscaltodate
        '						  		AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '				BEGIN
        '					SET @AcutalEndVaction = (SELECT TOP 1 ActualEndDate FROM hrs_EmployeesVacations
        '												WHERE EmployeeID = @EmpID
        '												AND CancelDate IS NULL
        '												AND ActualEndDate >= @FiscalFromdDate
        '												AND ActualEndDate <= @Fiscaltodate
        '												AND ActualStartDate NOT BETWEEN @FiscalFromdDate AND @Fiscaltodate
        '														AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '					BEGIN
        '							SET @VactionDays = @VactionDays + DATEDIFF (DAY, @FiscalFromdDate, @AcutalEndVaction )
        '					 SET @WorkingDays = @lastDayOfMonth
        '					END

        '				END
        '				--================================================		}
        '				--ÏÇÎá ÇáÝÊÑÉ --
        '				--04
        '				-- IF return date AND start date IN same period ==		{
        '						IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '							WHERE EmployeeID = @EmpID AND  CancelDate IS NULL
        '							AND ActualStartDate BETWEEN @FiscalFromdDate
        '							AND @Fiscaltodate
        '							AND ActualEndDate BETWEEN @FiscalFromdDate AND @Fiscaltodate
        '							AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '													WHERE IsAnnual = 1))
        '				BEGIN

        '					SET @VactionDays = @VactionDays + (SELECT SUM(DATEDIFF (DAY, ActualStartDate, ActualEndDate))
        '														FROM hrs_EmployeesVacations
        '														WHERE EmployeeID = @EmpID
        '														AND  CancelDate IS NULL
        '														AND ActualStartDate BETWEEN @FiscalFromdDate AND  @Fiscaltodate
        '														AND ActualEndDate BETWEEN @FiscalFromdDate AND  @Fiscaltodate
        '														AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1))
        '				SET @WorkingDays = @lastDayOfMonth

        '				END

        '				--================================================		}

        '				--SET @VactionDays= @VactionDays+
        '				--(
        '				--SELECT ISNULL(count(id),0)
        '				-- FROM Att_AttendancePreparationDetails
        '				-- WHERE EmployeeID = @EmpID
        '				-- AND CONVERT(Datetime, GAttendDate, 103) >= CONVERT(Datetime, @AttandanceFromDate, 103)
        '				-- AND CONVERT(Datetime, GAttendDate, 103) <= CONVERT(Datetime, @AttandanceTodate, 103)

        '				-- AND LeavingType IN(SELECT ID FROM hrs_VacationsTypes WHERE IsPaid = -1)
        '				--)
        '				--=============== ABSENT ==========================		{
        '				SET @AbsentDays =
        '				(
        '				SELECT ISNULL(count(id),0)
        '				 FROM Att_AttendancePreparationDetails
        '				 WHERE EmployeeID = @EmpID
        '				 AND CONVERT(Datetime, GAttendDate, 103) >= CONVERT(Datetime, @AttandanceFromDate, 103)
        '				 AND CONVERT(Datetime, GAttendDate, 103) <= CONVERT(Datetime, @AttandanceTodate, 103)
        '				 AND IsAbsent = 1
        '				 AND isnull(LeavingType,0) = 0
        '				)
        '				--================================================		}
        '				IF @IsToAttandanceToDate = 1
        '				BEGIN
        '						SET @AtttoEndDays = @WorkingDays-@vactiondays - DAY(@ToAttendDate) + 1
        '				END
        '				SET @WorkingDays = @WorkingDays - @vactiondays -@AbsentDays - @joindateDaysDeduction - @EndOfserviceDaysDetuction - @AtttoEndDays
        '				IF	@WorkingDays < 0  SET @WorkingDays = 0
        '				IF	@WorkingDays > 30 SET @WorkingDays = 30
        '	--	END
        '		SET @vactiondays = @vactiondays + @AtttoEndDays

        '				INSERT INTO @Attendace (WorkingDays, AbsentDays, vactiondays)
        '								SELECT @WorkingDays, @AbsentDays, @vactiondays

        '		  RETURN
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'ALTER   Proc [dbo].[hrs_FingerprintForCurrntMonth]
        '@FingerprintID int,
        '@FromDate DateTime,
        '@ToDate DateTime
        'AS
        'BEGIN
        'SET DATEFORMAT ymd
        ' DECLARE
        '	@ServerIP Varchar(50),
        '	@DatabaseName Varchar(50),
        '	@FingerprintTableName Varchar(50),
        '	@UserIdColumnName Varchar(50),
        '	@CheckInOutColumnName Varchar(50),
        '	@Query NVARCHAR (MAX),
        '	@LastDateFingerPrint DateTime,
        '	@UsersTableIdntity Varchar(50),
        '	@UserMatchColumnName Varchar(50),
        '	@UsersTableName Varchar(50)

        'SELECT 
        '	@ServerIP = FingerprintServerIP,
        '	@DatabaseName = FingerprintDatabaseName,
        '	@FingerprintTableName = FingerprintTableName,
        '	@UserIdColumnName = UserIdColumnName,
        '	@CheckInOutColumnName = CheckInOutColumnName,
        '	@UsersTableIdntity = UsersTableIdntity,
        '	@UserMatchColumnName = UserMatchColumnName,
        '	@UsersTableName = UsersTableName
        'FROM hrs_FingerprintSettings
        'WHERE ID = @FingerprintID

        'SELECT * FROM hrs_EmployeesFingerprints
        'IF EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesFingerprints'
        ')
        'BEGIN
        '    SET DATEFORMAT ymd
        '	DELETE FROM hrs_EmployeesFingerprints WHERE FingerprintTime BETWEEN @FromDate AND @ToDate

        '	DECLARE @result TABLE ([FingerprintTime] datetime);

        '	INSERT INTO @result ([FingerprintTime])
        '	EXEC (N'SELECT TOP (1) [FingerprintTime] FROM [hrs_EmployeesFingerprints] WHERE [FingerprintID] = ' + @FingerprintID
        '	    + N' ORDER BY [FingerprintTime] DESC');

        '	SET @LastDateFingerPrint = (SELECT TOP (1) [FingerprintTime] FROM @result);

        '	SET @Query = NULL
        '	SET @Query = N'INSERT INTO hrs_EmployeesFingerprints ([UserID ],'
        '			   + N'[FingerprintTime], [UserCode], [FingerprintID])'
        '			   + N'SELECT fp.[' + @UserIdColumnName + N'],'
        '	           + N' fp.[' + @CheckInOutColumnName + N'],'
        '			   + N' us.[' + @UserMatchColumnName + N'], '
        '			   + CAST(@FingerprintID AS NVARCHAR(10))
        '			   + N' FROM [' + @DatabaseName + N']'
        '			   + N'.[dbo].[' + @FingerprintTableName + N'] AS fp'
        '			   + N' INNER JOIN [' + @DatabaseName + N'].[dbo].[' + @UsersTableName + N'] AS us'
        '			   + N' ON fp.[' + @UserIdColumnName + N'] = us.[' + @UsersTableIdntity + N']'


        '	--IF(@LastDateFingerPrint IS NOT NULL)
        '	--	BEGIN
        '	--		DECLARE @ConvertLastDate varchar(20) 
        '	--		SELECT @ConvertLastDate  = CONVERT(VARCHAR(20), @LastDateFingerPrint, 120)
        '	--		SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] > ''' + @ConvertLastDate + N''''
        '	--		+  N' AND fp.[' + @CheckInOutColumnName + N'] BETWEEN ''1900-01-01'' AND ''2079-06-06'' '
        '	--	END
        '	--ELSE
        '	--	BEGIN
        '	--		SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] > GETDATE()-60'
        '	--		+  N' AND fp.[' + @CheckInOutColumnName + N'] BETWEEN ''1900-01-01'' AND ''2079-06-06'' '
        '	--	END
        '	DECLARE @ConvertFromDate varchar(20) 
        '	DECLARE @ConvertToDate varchar(20) 
        '	SELECT @ConvertFromDate  = CONVERT(VARCHAR(20), @FromDate, 120)
        '	SELECT @ConvertToDate  = CONVERT(VARCHAR(20), @ToDate, 120)
        '	SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] BETWEEN ''' + @ConvertFromDate + ''''
        '			    + N' AND ''' + @ConvertToDate + ''''

        '		EXEC (@Query)
        '    END
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "


        'CREATE OR ALTER PROCEDURE [dbo].[hrs_GetContractIDDate]
        '	@EmployeeID Int
        '	,@ChechDate smalldatetime
        'AS
        'SET DATEFORMAT dmy
        'SELECT 
        '	TOP(1) ID
        'FROM 
        '	hrs_Contracts 
        'WHERE
        '	EmployeeID = @EmployeeID 
        '	AND CancelDate IS NULL
        '	AND @ChechDate BETWEEN StartDate AND ISNULL(EndDate,'2079-06-06')
        'ORDER BY 
        '	StartDate DESC
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE   FUNCTION [dbo].[AttendanceEffects]
        '(
        '  @EmpID int,
        '  @AttandanceFromDate date,
        '  @AttandanceToDate date,
        '  @intFiscalperiod int,
        '  @IsToAttandanceToDate bit = 0,
        '  @ToAttendDate date,
        '  @isEndOfservice bit = 0,
        '  @FiscalFromdDate date,
        '  @FiscalToDate date
        ')
        'RETURNS @Attendace TABLE
        '   (
        '		WorkingDays int,
        '		AbsentDays  int,
        '		vactiondays int
        '   )
        'AS
        'BEGIN
        '		--declare		@FiscalFromdDate as date=(SELECT TOP 1 fromdate FROM sys_FiscalYearsPeriods WHERE id=@intFiscalperiod)
        '		--declare		@FiscalToDate as date=(SELECT TOP 1 ToDate  FROM sys_FiscalYearsPeriods WHERE id=@intFiscalperiod)
        '		DECLARE		@WorkingDays int	= 0,
        '					@AbsentDays int		= 0,
        '					@vactiondays int	= 0,
        '					@AcutalStartVaction date,
        '					@AcutalEndVaction date,
        '					@joindateDaysDeduction int = 0,
        '					@EndOfserviceDaysDetuction int = 0,
        '					@AttToEndDays int = 0,
        '					@contractID int		= (SELECT TOP 1 id FROM hrs_Contracts
        '					                        WHERE EmployeeID = @EmpID
        '											ORDER BY StartDate DESC)

        '		   DECLARE @NODPP int			= (SELECT TOP 1 NoOfDaysPerPeriod
        '											FROM hrs_EmployeesClasses Ec INNER JOIN hrs_Contracts c
        '											ON c.EmployeeClassID = Ec.ID
        '											WHERE c.ID = @contractID),

        '				@JoinDate date		= (SELECT  joindate FROM hrs_employees WHERE id = @EmpID ),
        '				@lastDayOfMonth int = 30 --DAY((SELECT DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, @FiscalToDate) + 1, 0))))
        '		--IF @NODPP =30
        '		--	BEGIN
        '				SET @WorkingDays = 30
        '				--======== joindate ==============================		{
        '				IF @JoinDate > @FiscalFromdDate
        '				BEGIN
        '				SET @joindateDaysDeduction = DATEDIFF(DAY, @FiscalFromdDate, @JoinDate)
        '				IF((DATEDIFF(DAY, @FiscalFromdDate, @FiscalToDate) + 1) = 31)
        '				    SET @WorkingDays = 30
        '				ELSE
        '					SET @WorkingDays = @lastDayOfMonth
        '				END
        '				--=================================================		}

        '				--================= END Of service ==================		{
        '				IF	   @isEndOfservice = 1
        '				BEGIN
        '				 SET @EndOfserviceDaysDetuction = @WorkingDays - DATEDIFF(DAY,@FiscalFromdDate,@AttandanceToDate) - 1
        '				END
        '				--=================================================		}

        '				--================== Vaction =======================	{	
        '				-- start vaction date smaller than fiscal strat date AND return vaction is null or greater than fiscal END date
        '				--01
        '					--Ýì ÍÇáÉ Çä ÇáÇÌÇÒÉ ÈÏÃÊ ÞÈá ÇáÝÊÑÉ ÇáãÇáíÉ æÇáÚæÏÉ áíÓÊ Ýì ÇáÝÊÑÉ ÇáãÇáíÉ --
        '					-- ÚÏÏ ÇáÇíÇã ÇáÇÌÇÒÉ äÝÓ ÇíÇã ÇáÚãá 
        '				IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '				           WHERE EmployeeID = @EmpID
        '						   AND  CancelDate IS NULL
        '						   AND ActualStartDate  < @FiscalFromdDate
        '						   AND (ActualEndDate IS NULL OR ActualEndDate > @Fiscaltodate )
        '						   AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '													WHERE  IsAnnual = 1
        '													or  IsPaid = -1)
        '												                                               )
        '					BEGIN 

        '						SET @VactionDays = @WorkingDays
        '					END
        '					--=================================================		}
        '				-- StartVaction Greeter than Fiscal start date AND acutl END vaction is null or greeter than ficalto date
        '				--02
        '				-- Ýì ÍÇáÉ ÇáÇÌÇÒÉ ÏÇÎá ÇáÝÊÑÉ ÇáãÇáíÉ æÇáÚæÏÉ ÎÇÑÌ ÇáÝÊÑÉ ÇáãÇáíÉ --
        '					IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '					            WHERE EmployeeID = @EmpID
        '								AND  CancelDate IS NULL
        '								AND ActualStartDate  >= @FiscalFromdDate
        '								AND ActualStartDate  <= @Fiscaltodate
        '								AND (ActualEndDate IS NULL OR ActualEndDate > @Fiscaltodate )
        '									AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '					BEGIN
        '						SET @AcutalStartVaction = (SELECT TOP 1 ActualStartDate FROM hrs_EmployeesVacations
        '						                           WHERE EmployeeID = @EmpID
        '												   AND  CancelDate IS NULL
        '												   AND ActualStartDate  >= @FiscalFromdDate
        '												   AND ActualStartDate  <= @Fiscaltodate
        '												   AND (ActualEndDate IS NULL OR ActualEndDate > @Fiscaltodate )
        '														AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '						IF @JoinDate >= @FiscalFromdDate AND @JoinDate <= @FiscalToDate
        '						SET @VactionDays = @WorkingDays - DATEDIFF (DAY,@JoinDate,@AcutalStartVaction)
        '						ELSE
        '						SET @VactionDays = @WorkingDays - DATEDIFF (DAY,@FiscalFromdDate ,@AcutalStartVaction)
        '					END
        '				--================================================		}
        '				-- EndVactiondate Smaller than Fiscal END date AND Start  vaction is not BETWEEN acual start date AND acual END date
        '				--03
        '				-- ÇáÚæÏÉ ãä ÇáÇÌÇÒÉ ÏÇÎá ÇáÝÊÑÉ æáßä ÈÏÇíÉ ÇáÇÌÇÒÉ áíÓÊ Öãä ÇáÝÑÉ 
        '				IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '				           WHERE EmployeeID=@EmpID
        '						   AND CancelDate IS NULL
        '						   AND ActualEndDate > @FiscalFromdDate
        '						   AND ActualEndDate <= @Fiscaltodate
        '						   AND ActualStartDate NOT BETWEEN  @FiscalFromdDate AND @Fiscaltodate
        '						  		AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '				BEGIN
        '					SET @AcutalEndVaction = (SELECT TOP 1 ActualEndDate FROM hrs_EmployeesVacations
        '												WHERE EmployeeID = @EmpID
        '												AND CancelDate IS NULL
        '												AND ActualEndDate >= @FiscalFromdDate
        '												AND ActualEndDate <= @Fiscaltodate
        '												AND ActualStartDate NOT BETWEEN @FiscalFromdDate AND @Fiscaltodate
        '														AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '					BEGIN
        '							SET @VactionDays = @VactionDays + DATEDIFF (DAY, @FiscalFromdDate, @AcutalEndVaction )
        '					 SET @WorkingDays = @lastDayOfMonth
        '					END

        '				END
        '				--================================================		}
        '				--ÏÇÎá ÇáÝÊÑÉ --
        '				--04
        '				-- IF return date AND start date IN same period ==		{
        '						IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '							WHERE EmployeeID = @EmpID AND  CancelDate IS NULL
        '							AND ActualStartDate BETWEEN @FiscalFromdDate
        '							AND @Fiscaltodate
        '							AND ActualEndDate BETWEEN @FiscalFromdDate AND @Fiscaltodate
        '							AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '				BEGIN

        '					SET @VactionDays = @VactionDays + (SELECT SUM(DATEDIFF (DAY, ActualStartDate, ActualEndDate))
        '														FROM hrs_EmployeesVacations
        '														WHERE EmployeeID = @EmpID
        '														AND  CancelDate IS NULL
        '														AND ActualStartDate BETWEEN @FiscalFromdDate AND  @Fiscaltodate
        '														AND ActualEndDate BETWEEN @FiscalFromdDate AND  @Fiscaltodate
        '														AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '				SET @WorkingDays = @lastDayOfMonth

        '				END

        '				--================================================		}

        '				--SET @VactionDays= @VactionDays+
        '				--(
        '				--SELECT ISNULL(count(id),0)
        '				-- FROM Att_AttendancePreparationDetails
        '				-- WHERE EmployeeID = @EmpID
        '				-- AND CONVERT(Datetime, GAttendDate, 103) >= CONVERT(Datetime, @AttandanceFromDate, 103)
        '				-- AND CONVERT(Datetime, GAttendDate, 103) <= CONVERT(Datetime, @AttandanceTodate, 103)

        '				-- AND LeavingType IN(SELECT ID FROM hrs_VacationsTypes WHERE IsPaid = -1)
        '				--)
        '				--=============== ABSENT ==========================		{
        '				SET @AbsentDays =
        '				(
        '				SELECT ISNULL(count(id),0)
        '				 FROM Att_AttendancePreparationDetails
        '				 WHERE EmployeeID = @EmpID
        '				 AND CONVERT(Datetime, GAttendDate, 103) >= CONVERT(Datetime, @AttandanceFromDate, 103)
        '				 AND CONVERT(Datetime, GAttendDate, 103) <= CONVERT(Datetime, @AttandanceTodate, 103)
        '				 AND IsAbsent = 1
        '				 AND isnull(LeavingType,0) = 0
        '				)
        '				--================================================		}
        '				IF @IsToAttandanceToDate = 1
        '				BEGIN
        '						SET @AtttoEndDays = @WorkingDays-@vactiondays - DAY(@ToAttendDate) + 1
        '				END
        '				SET @WorkingDays = @WorkingDays - @vactiondays -@AbsentDays - @joindateDaysDeduction - @EndOfserviceDaysDetuction - @AtttoEndDays
        '				IF	@WorkingDays < 0  SET @WorkingDays = 0
        '				IF	@WorkingDays > 30 SET @WorkingDays = 30
        '	--	END
        '		SET @vactiondays = @vactiondays + @AtttoEndDays

        '				INSERT INTO @Attendace (WorkingDays, AbsentDays, vactiondays)
        '								SELECT @WorkingDays, @AbsentDays, @vactiondays

        '		  RETURN
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesClasses', 'AttendanceFromTimeSheet') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesClasses
        '    ADD AttendanceFromTimeSheet Bit 
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'ALTER   Proc [dbo].[hrs_FingerprintForCurrntMonth]
        '@FingerprintID int,
        '@FromDate DateTime,
        '@ToDate DateTime
        'AS
        'BEGIN
        'SET DATEFORMAT ymd
        ' DECLARE
        '	@ServerIP Varchar(50),
        '	@DatabaseName Varchar(50),
        '	@FingerprintTableName Varchar(50),
        '	@UserIdColumnName Varchar(50),
        '	@CheckInOutColumnName Varchar(50),
        '	@Query NVARCHAR (MAX),
        '	@LastDateFingerPrint DateTime,
        '	@UsersTableIdntity Varchar(50),
        '	@UserMatchColumnName Varchar(50),
        '	@UsersTableName Varchar(50)

        'SELECT 
        '	@ServerIP = FingerprintServerIP,
        '	@DatabaseName = FingerprintDatabaseName,
        '	@FingerprintTableName = FingerprintTableName,
        '	@UserIdColumnName = UserIdColumnName,
        '	@CheckInOutColumnName = CheckInOutColumnName,
        '	@UsersTableIdntity = UsersTableIdntity,
        '	@UserMatchColumnName = UserMatchColumnName,
        '	@UsersTableName = UsersTableName
        'FROM hrs_FingerprintSettings
        'WHERE ID = @FingerprintID

        'SET @ToDate =  DATEADD(DAY, 2, @ToDate) 

        'IF EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesFingerprints'
        ')
        'BEGIN
        '    SET DATEFORMAT ymd
        '	DELETE FROM hrs_EmployeesFingerprints WHERE FingerprintTime BETWEEN @FromDate AND @ToDate

        '	DECLARE @result TABLE ([FingerprintTime] datetime);

        '	INSERT INTO @result ([FingerprintTime])
        '	EXEC (N'SELECT TOP (1) [FingerprintTime] FROM [hrs_EmployeesFingerprints] WHERE [FingerprintID] = ' + @FingerprintID
        '	    + N' ORDER BY [FingerprintTime] DESC');

        '	SET @LastDateFingerPrint = (SELECT TOP (1) [FingerprintTime] FROM @result);

        '	SET @Query = NULL
        '	SET @Query = N'INSERT INTO hrs_EmployeesFingerprints ([UserID ],'
        '			   + N'[FingerprintTime], [UserCode], [FingerprintID])'
        '			   + N'SELECT fp.[' + @UserIdColumnName + N'],'
        '	           + N' fp.[' + @CheckInOutColumnName + N'],'
        '			   + N' us.[' + @UserMatchColumnName + N'], '
        '			   + CAST(@FingerprintID AS NVARCHAR(10))
        '			   + N' FROM [' + @DatabaseName + N']'
        '			   + N'.[dbo].[' + @FingerprintTableName + N'] AS fp'
        '			   + N' INNER JOIN [' + @DatabaseName + N'].[dbo].[' + @UsersTableName + N'] AS us'
        '			   + N' ON fp.[' + @UserIdColumnName + N'] = us.[' + @UsersTableIdntity + N']'

        '	DECLARE @ConvertFromDate varchar(20) 
        '	DECLARE @ConvertToDate varchar(20) 
        '	SELECT @ConvertFromDate  = CONVERT(VARCHAR(20), @FromDate, 120)
        '	SELECT @ConvertToDate  = CONVERT(VARCHAR(20), @ToDate, 120)
        '	SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] BETWEEN ''' + @ConvertFromDate + ''''
        '			    + N' AND ''' + @ConvertToDate + ''''

        '		EXEC (@Query)
        '    END
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesVacations', 'RemainingDays') IS NOT NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesVacations
        '    ALTER COLUMN RemainingDays decimal(10,2)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesVacations', 'RemainingBalance') IS NOT NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesVacations
        '    ALTER COLUMN RemainingBalance decimal(10,2)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesTransactions', 'RemainVacDaySettlement') IS NOT NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesTransactions
        '    ALTER COLUMN RemainVacDaySettlement decimal(8,2)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('sys_Companies', 'VacSettlement') IS NULL  
        'BEGIN
        '    ALTER TABLE sys_Companies
        '    ADD VacSettlement bit
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        '-- 2022-03-28
        'create     FUNCTION [dbo].[AttendanceEffects]
        '(
        '  @EmpID int,
        '  @AttandanceFromDate date,
        '  @AttandanceToDate date,
        '  @intFiscalperiod int,
        '  @IsToAttandanceToDate bit = 0,
        '  @ToAttendDate date,
        '  @isEndOfservice bit = 0,
        '  @FiscalFromdDate date,
        '  @FiscalToDate date
        ')
        'RETURNS @Attendace TABLE
        '   (
        '		WorkingDays int,
        '		AbsentDays  int,
        '		vactiondays int
        '   )
        'AS
        'BEGIN
        '		--declare		@FiscalFromdDate as date=(SELECT TOP 1 fromdate FROM sys_FiscalYearsPeriods WHERE id=@intFiscalperiod)
        '		--declare		@FiscalToDate as date=(SELECT TOP 1 ToDate  FROM sys_FiscalYearsPeriods WHERE id=@intFiscalperiod)
        '				DECLARE		@WorkingDays int	= 0,
        '					@AbsentDays int		= 0,
        '					@vactiondays int	= 0,
        '					@AcutalStartVaction date,
        '					@AcutalEndVaction date,
        '					@joindateDaysDeduction int = 0,
        '					@EndOfserviceDaysDetuction int = 0,
        '					@AttToEndDays int = 0,
        '					@contractID int		= (SELECT TOP 1 id FROM hrs_Contracts
        '					                        WHERE EmployeeID = @EmpID
        '											ORDER BY StartDate DESC)

        '		   DECLARE @NODPP int			= (SELECT TOP 1 NoOfDaysPerPeriod
        '											FROM hrs_EmployeesClasses Ec INNER JOIN hrs_Contracts c
        '											ON c.EmployeeClassID = Ec.ID
        '											WHERE c.ID = @contractID),

        '				@JoinDate date		= (SELECT  joindate FROM hrs_employees WHERE id = @EmpID ),
        '				@lastDayOfMonth int = 30 --DAY((SELECT DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, @FiscalToDate) + 1, 0))))
        '		--IF @NODPP =30
        '		--	BEGIN
        '				SET @WorkingDays = 30
        '				--======== joindate ==============================		{
        '				IF @JoinDate > @FiscalFromdDate
        '				BEGIN
        '				SET @joindateDaysDeduction = DATEDIFF(DAY, @FiscalFromdDate, @JoinDate)
        '				IF((DATEDIFF(DAY, @FiscalFromdDate, @FiscalToDate) + 1) = 31)
        '				    SET @WorkingDays = 30
        '				ELSE
        '					SET @WorkingDays = @lastDayOfMonth
        '				END
        '				--=================================================		}

        '				--================= END Of service ==================		{
        '				IF	   @isEndOfservice = 1
        '				BEGIN
        '				SET @EndOfserviceDaysDetuction = @WorkingDays - DATEDIFF(DAY,@FiscalFromdDate,@AttandanceToDate) - 1
        '				END
        '				--=================================================		}

        '				--================== Vaction =======================	{	
        '				-- start vaction date smaller than fiscal strat date AND return vaction is null or greater than fiscal END date
        '				--01
        '					--Ýì ÍÇáÉ Çä ÇáÇÌÇÒÉ ÈÏÃÊ ÞÈá ÇáÝÊÑÉ ÇáãÇáíÉ æÇáÚæÏÉ áíÓÊ Ýì ÇáÝÊÑÉ ÇáãÇáíÉ --
        '					-- ÚÏÏ ÇáÇíÇã ÇáÇÌÇÒÉ äÝÓ ÇíÇã ÇáÚãá
        '				IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '				           WHERE EmployeeID = @EmpID
        '						   AND  CancelDate IS NULL
        '						   AND ActualStartDate  < @FiscalFromdDate
        '						   AND (ActualEndDate IS NULL OR ActualEndDate > @Fiscaltodate )
        '						   AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '													WHERE  IsAnnual = 1
        '													or  IsPaid = -1)
        '												                                               )
        '					BEGIN

        '						SET @VactionDays = @WorkingDays
        '					END
        '					--=================================================		}
        '				-- StartVaction Greeter than Fiscal start date AND acutl END vaction is null or greeter than ficalto date
        '				--02
        '				-- Ýì ÍÇáÉ ÇáÇÌÇÒÉ ÏÇÎá ÇáÝÊÑÉ ÇáãÇáíÉ æÇáÚæÏÉ ÎÇÑÌ ÇáÝÊÑÉ ÇáãÇáíÉ --
        '					IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '					            WHERE EmployeeID = @EmpID
        '								AND  CancelDate IS NULL
        '								AND ActualStartDate  >= @FiscalFromdDate
        '								AND ActualStartDate  <= @Fiscaltodate
        '								AND (ActualEndDate IS NULL OR ActualEndDate > @Fiscaltodate )
        '									AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '					BEGIN
        '						SET @AcutalStartVaction = (SELECT TOP 1 ActualStartDate FROM hrs_EmployeesVacations
        '						                           WHERE EmployeeID = @EmpID
        '												   AND  CancelDate IS NULL
        '												   AND ActualStartDate  >= @FiscalFromdDate
        '												   AND ActualStartDate  <= @Fiscaltodate
        '												   AND (ActualEndDate IS NULL OR ActualEndDate > @Fiscaltodate )
        '														AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '						IF @JoinDate >= @FiscalFromdDate AND @JoinDate <= @FiscalToDate
        '						SET @VactionDays = @WorkingDays - DATEDIFF (DAY,@JoinDate,@AcutalStartVaction)
        '						ELSE
        '						SET @VactionDays = @WorkingDays - DATEDIFF (DAY,@FiscalFromdDate ,@AcutalStartVaction)
        '					END
        '				--================================================		}
        '				-- EndVactiondate Smaller than Fiscal END date AND Start  vaction is not BETWEEN acual start date AND acual END date
        '				--03
        '				-- ÇáÚæÏÉ ãä ÇáÇÌÇÒÉ ÏÇÎá ÇáÝÊÑÉ æáßä ÈÏÇíÉ ÇáÇÌÇÒÉ áíÓÊ Öãä ÇáÝÑÉ
        '				IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '				           WHERE EmployeeID=@EmpID
        '						   AND CancelDate IS NULL
        '						   AND ActualEndDate > @FiscalFromdDate
        '						   AND ActualEndDate <= @Fiscaltodate
        '						   AND ActualStartDate NOT BETWEEN  @FiscalFromdDate AND @Fiscaltodate
        '						  		AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '				BEGIN
        '					SET @AcutalEndVaction = (SELECT TOP 1 ActualEndDate FROM hrs_EmployeesVacations
        '												WHERE EmployeeID = @EmpID
        '												AND CancelDate IS NULL
        '												AND ActualEndDate >= @FiscalFromdDate
        '												AND ActualEndDate <= @Fiscaltodate
        '												AND ActualStartDate NOT BETWEEN @FiscalFromdDate AND @Fiscaltodate
        '														AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '					BEGIN
        '							SET @VactionDays = @VactionDays + DATEDIFF (DAY, @FiscalFromdDate, @AcutalEndVaction )
        '					SET @WorkingDays = @lastDayOfMonth
        '					END

        '				END
        '				--================================================		}
        '				--ÏÇÎá ÇáÝÊÑÉ --
        '				--04
        '				-- IF return date AND start date IN same period ==		{
        '						IF EXISTS(SELECT TOP 1 id FROM hrs_EmployeesVacations
        '							WHERE EmployeeID = @EmpID AND  CancelDate IS NULL
        '							AND ActualStartDate BETWEEN @FiscalFromdDate
        '							AND @Fiscaltodate
        '							AND ActualEndDate BETWEEN @FiscalFromdDate AND @Fiscaltodate
        '							AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '				BEGIN

        '					SET @VactionDays = @VactionDays + (SELECT SUM(DATEDIFF (DAY, ActualStartDate, ActualEndDate))
        '														FROM hrs_EmployeesVacations
        '														WHERE EmployeeID = @EmpID
        '														AND  CancelDate IS NULL
        '														AND ActualStartDate BETWEEN @FiscalFromdDate AND  @Fiscaltodate
        '														AND ActualEndDate BETWEEN @FiscalFromdDate AND  @Fiscaltodate
        '														AND VacationTypeID IN(SELECT ID FROM hrs_VacationsTypes
        '																				WHERE   IsAnnual = 1
        '																				or  IsPaid = -1)
        '																				)
        '				SET @WorkingDays = @lastDayOfMonth

        '				END				--=============== ABSENT ==========================		{
        '				SET @AbsentDays =
        '				(
        '				SELECT ISNULL(count(id),0)
        '				FROM Att_AttendancePreparationDetails
        '				WHERE EmployeeID = @EmpID
        '				AND CONVERT(Datetime, GAttendDate, 103) >= CONVERT(Datetime, @AttandanceFromDate, 103)
        '				AND CONVERT(Datetime, GAttendDate, 103) <= CONVERT(Datetime, @AttandanceTodate, 103)
        '				AND IsAbsent = 1
        '				AND isnull(LeavingType,0) = 0
        '				)
        '				--================================================		}
        '				IF @IsToAttandanceToDate = 1
        '				BEGIN
        '						SET @AtttoEndDays = @WorkingDays-@vactiondays - DAY(@ToAttendDate) + 1
        '				END
        '				SET @WorkingDays = @WorkingDays - @vactiondays -@AbsentDays - @joindateDaysDeduction - @EndOfserviceDaysDetuction - @AtttoEndDays
        '				IF	@WorkingDays < 0  SET @WorkingDays = 0
        '				IF	@WorkingDays > 30 SET @WorkingDays = 30
        '	--	END
        '		SET @vactiondays = @vactiondays + @AtttoEndDays

        '				INSERT INTO @Attendace (WorkingDays, AbsentDays, vactiondays)
        '								SELECT @WorkingDays, @AbsentDays, @vactiondays

        '		  RETURN
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesVacations', 'TotalBalance') IS NOT NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesVacations
        '    ALTER COLUMN TotalBalance decimal(10, 2)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Forms WHERE Code = 'frmUpdateDirectManager') = 0
        'BEGIN
        '	INSERT INTO [dbo].[sys_Forms] ( [Code], [EngName], [ArbName], [ArbName4S], [EngDescription], [ArbDescription], [Rank], [ModuleID], [SearchFormID], [Height], [Width], [Remarks], [RegUserID], [RegComputerID], [RegDate], [CancelDate], [Layout], [LinkTarget], [LinkUrl], [ImageUrl], [MainID]) VALUES ( 'frmUpdateDirectManager', 'frmUpdateDirectManager.aspx', NULL, NULL, 'Update DirectManager', 'تحديث المدير المباشر', 0, 2, NULL, 650, 1100, NULL, NULL, NULL, '20110726 13:23:00.000', NULL, NULL, NULL, NULL, NULL, NULL)
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].[sys_Forms] SET [ArbDescription] = 'تحديث المدير المباشر' WHERE Code = 'frmUpdateDirectManager'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmUpdateDirectManager') = 0
        'BEGIN
        '	INSERT INTO [dbo].[sys_Menus] ( [Code], [EngName], [ArbName], [ArbName4S], [ParentID], [Shortcut], [Rank], [FormID], [ObjectID], [ViewFormID], [IsHide], [Image], [ViewType], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES ( 'frmUpdateDirectManager', 'Update Direct Manager', 'تحديث المدير المباشر', 'تحديث المدير المباشر', 240, NULL, 9, (SELECT ID FROM sys_Forms WHERE Code = 'frmUpdateDirectManager'), NULL, NULL, 0, NULL, 1, NULL, NULL, '20110726 14:00:00.000', NULL)
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].[sys_Menus] SET [ArbName] = 'تحديث المدير المباشر', [ArbName4S] = 'تحديث المدير المباشر' WHERE Code = 'frmUpdateDirectManager'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_FormsPermissions WHERE UserID = 1 AND FormID = 
        '			(SELECT ID FROM sys_Forms WHERE Code = 'frmUpdateDirectManager')) = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_FormsPermissions]
        '           ([FormID]
        '           ,[UserID]
        '           ,[AllowView]
        '           ,[AllowAdd]
        '           ,[AllowEdit]
        '           ,[AllowDelete]
        '           ,[AllowPrint]
        '		   ,[RegUserID]
        '           ,[RegDate])
        '     VALUES
        '           ((SELECT ID FROM sys_Forms WHERE Code = 'frmUpdateDirectManager')
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '           ,1
        '		   ,1
        '           ,GETDATE())
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS(SELECT * FROM sys.views WHERE name = 'fcs_CostCenters1')
        'BEGIN 
        '	EXECUTE('CREATE VIEW fcs_CostCenters1 AS SELECT * FROM CostCenter')
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS(SELECT * FROM sys.views WHERE name = 'fcs_CostCenters2')
        'BEGIN 
        '	EXECUTE('CREATE VIEW fcs_CostCenters2 AS SELECT * FROM CostCenter')
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS(SELECT * FROM sys.views WHERE name = 'fcs_CostCenters3')
        'BEGIN 
        '	EXECUTE('CREATE VIEW fcs_CostCenters3 AS SELECT * FROM CostCenter')
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS(SELECT * FROM sys.views WHERE name = 'fcs_CostCenters4')
        'BEGIN 
        '	EXECUTE('CREATE VIEW fcs_CostCenters4 AS SELECT * FROM CostCenter')
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[hrs_PaidSalaries]'))
        'EXEC dbo.sp_executesql @statement = N'CREATE OR ALTER VIEW [dbo].[hrs_PaidSalaries]
        'AS
        'SELECT TOP (100) PERCENT dbo.hrs_Employees.ID, dbo.hrs_Employees.Code, dbo.fn_GetEmpName(dbo.hrs_Employees.Code, 1) AS FullName, dbo.fn_GetEmpName(dbo.hrs_Employees.Code, 2) AS EmpEngName, 
        '                  dbo.sys_FiscalYearsPeriods.ArbName AS PeriodCode, dbo.sys_FiscalYearsPeriods.ID AS PeriodID, SUM(dbo.hrs_TransactionsTypes.Sign * dbo.hrs_EmployeesTransactionsDetails.NumericValue) AS PaidSalary
        'FROM     dbo.hrs_Employees INNER JOIN
        '                  dbo.hrs_EmployeesTransactions ON dbo.hrs_Employees.ID = dbo.hrs_EmployeesTransactions.EmployeeID INNER JOIN
        '                  dbo.hrs_EmployeesTransactionsProjects ON dbo.hrs_EmployeesTransactions.ID = dbo.hrs_EmployeesTransactionsProjects.EmployeeTransactionID INNER JOIN
        '                  dbo.hrs_EmployeesTransactionsDetails ON dbo.hrs_EmployeesTransactionsProjects.ID = dbo.hrs_EmployeesTransactionsDetails.EmpTransProjID INNER JOIN
        '                  dbo.hrs_TransactionsTypes ON dbo.hrs_EmployeesTransactionsDetails.TransactionTypeID = dbo.hrs_TransactionsTypes.ID INNER JOIN
        '                  dbo.sys_FiscalYearsPeriods ON dbo.hrs_EmployeesTransactions.FiscalYearPeriodID = dbo.sys_FiscalYearsPeriods.ID
        'WHERE  (dbo.hrs_EmployeesTransactions.PrepareType = ''N'') AND (dbo.hrs_EmployeesTransactionsDetails.NumericValue > 0) AND (dbo.hrs_EmployeesTransactionsDetails.TextValue = ''Paid'')
        'GROUP BY dbo.hrs_Employees.Code, dbo.hrs_Employees.ArbName + '' '' + dbo.hrs_Employees.FatherArbName + '' '' + dbo.hrs_Employees.GrandArbName + '' '' + dbo.hrs_Employees.FamilyArbName, 
        '                  dbo.hrs_Employees.EngName + '' '' + dbo.hrs_Employees.FatherEngName + '' '' + dbo.hrs_Employees.GrandEngName + '' '' + dbo.hrs_Employees.FamilyEngName, dbo.sys_FiscalYearsPeriods.ID, dbo.sys_FiscalYearsPeriods.ArbName, 
        '                  dbo.hrs_Employees.ID
        'ORDER BY PeriodID DESC
        '' 
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hrs_SalartDistExec]') AND type in (N'U'))
        'BEGIN
        'CREATE TABLE [dbo].[hrs_SalartDistExec](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[FiscalPeriodID] [int] NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[ProjectID] [int] NOT NULL,
        '	[ProjectPercentage] [decimal](18, 3) NOT NULL,
        '	[ProjectAmount] [decimal](18, 3) NOT NULL,
        ' CONSTRAINT [PK_hrs_SalartDistExec] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDist]') AND type in (N'U'))
        'BEGIN
        'CREATE TABLE [dbo].[hrs_SalaryDist](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EngName] [varchar](100) NOT NULL,
        '	[ArbName] [varchar](100) NOT NULL,
        '	[RegUserID] [int] NULL,
        '	[RegDate] [smalldatetime] NOT NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_SalaryDistribution] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistPlan]') AND type in (N'U'))
        'BEGIN
        'CREATE TABLE [dbo].[hrs_SalaryDistPlan](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [nvarchar](max) NULL,
        '	[SalaryDistributionID] [int] NOT NULL,
        '	[PeriodIDs] [nvarchar](max) NOT NULL,
        '	[Remarks] [varchar](2048) NULL,
        '	[RegUserID] [int] NULL,
        '	[RegComputerID] [int] NULL,
        '	[RegDate] [smalldatetime] NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_SalaryDistPlan] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistPlanMember]') AND type in (N'U'))
        'BEGIN
        'CREATE TABLE [dbo].[hrs_SalaryDistPlanMember](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[SalaryDistPlanID] [int] NULL,
        '	[EmployeeID] [int] NULL,
        '	[Remarks] [varchar](2048) NULL,
        '	[RegUserID] [int] NULL,
        '	[RegComputerID] [int] NULL,
        '	[RegDate] [smalldatetime] NOT NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_SalaryDistPlanMember] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistProjects]') AND type in (N'U'))
        'BEGIN
        'CREATE TABLE [dbo].[hrs_SalaryDistProjects](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[SalaryDistributionID] [int] NOT NULL,
        '	[ProjectID] [int] NOT NULL,
        '	[Percentage] [decimal](5, 2) NOT NULL,
        '	[RegUserID] [int] NULL,
        '	[RegDate] [smalldatetime] NOT NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_SalaryDistProjects] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_hrs_SalaryDistribution_RegDate]') AND type = 'D')
        'BEGIN
        'ALTER TABLE [dbo].[hrs_SalaryDist] ADD  CONSTRAINT [DF_hrs_SalaryDistribution_RegDate]  DEFAULT (getdate()) FOR [RegDate]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_hrs_SalaryDistPlan_RegDate]') AND type = 'D')
        'BEGIN
        'ALTER TABLE [dbo].[hrs_SalaryDistPlan] ADD  CONSTRAINT [DF_hrs_SalaryDistPlan_RegDate]  DEFAULT (getdate()) FOR [RegDate]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_hrs_SalaryDistPlanMember_RegDate]') AND type = 'D')
        'BEGIN
        'ALTER TABLE [dbo].[hrs_SalaryDistPlanMember] ADD  CONSTRAINT [DF_hrs_SalaryDistPlanMember_RegDate]  DEFAULT (getdate()) FOR [RegDate]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_hrs_SalaryDistProjects_RegDate]') AND type = 'D')
        'BEGIN
        'ALTER TABLE [dbo].[hrs_SalaryDistProjects] ADD  CONSTRAINT [DF_hrs_SalaryDistProjects_RegDate]  DEFAULT (getdate()) FOR [RegDate]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_hrs_SalaryDistPlan_hrs_SalaryDist]') AND parent_object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistPlan]'))
        'ALTER TABLE [dbo].[hrs_SalaryDistPlan]  WITH CHECK ADD  CONSTRAINT [FK_hrs_SalaryDistPlan_hrs_SalaryDist] FOREIGN KEY([SalaryDistributionID])
        'REFERENCES [dbo].[hrs_SalaryDist] ([ID])
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_hrs_SalaryDistPlan_hrs_SalaryDist]') AND parent_object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistPlan]'))
        'ALTER TABLE [dbo].[hrs_SalaryDistPlan] CHECK CONSTRAINT [FK_hrs_SalaryDistPlan_hrs_SalaryDist]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_hrs_SalaryDistPlanMember_hrs_Employees]') AND parent_object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistPlanMember]'))
        'ALTER TABLE [dbo].[hrs_SalaryDistPlanMember]  WITH CHECK ADD  CONSTRAINT [FK_hrs_SalaryDistPlanMember_hrs_Employees] FOREIGN KEY([EmployeeID])
        'REFERENCES [dbo].[hrs_Employees] ([ID])
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_hrs_SalaryDistPlanMember_hrs_Employees]') AND parent_object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistPlanMember]'))
        'ALTER TABLE [dbo].[hrs_SalaryDistPlanMember] CHECK CONSTRAINT [FK_hrs_SalaryDistPlanMember_hrs_Employees]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_hrs_SalaryDistPlanMember_hrs_SalaryDistPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistPlanMember]'))
        'ALTER TABLE [dbo].[hrs_SalaryDistPlanMember]  WITH CHECK ADD  CONSTRAINT [FK_hrs_SalaryDistPlanMember_hrs_SalaryDistPlan] FOREIGN KEY([SalaryDistPlanID])
        'REFERENCES [dbo].[hrs_SalaryDistPlan] ([ID])
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_hrs_SalaryDistPlanMember_hrs_SalaryDistPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistPlanMember]'))
        'ALTER TABLE [dbo].[hrs_SalaryDistPlanMember] CHECK CONSTRAINT [FK_hrs_SalaryDistPlanMember_hrs_SalaryDistPlan]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_hrs_SalaryDistProjects_hrs_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistProjects]'))
        'ALTER TABLE [dbo].[hrs_SalaryDistProjects]  WITH CHECK ADD  CONSTRAINT [FK_hrs_SalaryDistProjects_hrs_Projects] FOREIGN KEY([ProjectID])
        'REFERENCES [dbo].[hrs_Projects] ([ID])
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_hrs_SalaryDistProjects_hrs_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistProjects]'))
        'ALTER TABLE [dbo].[hrs_SalaryDistProjects] CHECK CONSTRAINT [FK_hrs_SalaryDistProjects_hrs_Projects]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_hrs_SalaryDistProjects_hrs_SalaryDist]') AND parent_object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistProjects]'))
        'ALTER TABLE [dbo].[hrs_SalaryDistProjects]  WITH CHECK ADD  CONSTRAINT [FK_hrs_SalaryDistProjects_hrs_SalaryDist] FOREIGN KEY([SalaryDistributionID])
        'REFERENCES [dbo].[hrs_SalaryDist] ([ID])
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_hrs_SalaryDistProjects_hrs_SalaryDist]') AND parent_object_id = OBJECT_ID(N'[dbo].[hrs_SalaryDistProjects]'))
        'ALTER TABLE [dbo].[hrs_SalaryDistProjects] CHECK CONSTRAINT [FK_hrs_SalaryDistProjects_hrs_SalaryDist]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'hrs_PaidSalaries', NULL,NULL))
        '	EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
        'Begin DesignProperties = 
        '   Begin PaneConfigurations = 
        '      Begin PaneConfiguration = 0
        '         NumPanes = 4
        '         Configuration = '(H (1[35] 4[26] 2[13] 3) )'
        '      End
        '      Begin PaneConfiguration = 1
        '         NumPanes = 3
        '         Configuration = '(H (1 [50] 4 [25] 3))'
        '      End
        '      Begin PaneConfiguration = 2
        '         NumPanes = 3
        '         Configuration = '(H (1 [50] 2 [25] 3))'
        '      End
        '      Begin PaneConfiguration = 3
        '         NumPanes = 3
        '         Configuration = '(H (4 [30] 2 [40] 3))'
        '      End
        '      Begin PaneConfiguration = 4
        '         NumPanes = 2
        '         Configuration = '(H (1 [56] 3))'
        '      End
        '      Begin PaneConfiguration = 5
        '         NumPanes = 2
        '         Configuration = '(H (2 [66] 3))'
        '      End
        '      Begin PaneConfiguration = 6
        '         NumPanes = 2
        '         Configuration = '(H (4 [50] 3))'
        '      End
        '      Begin PaneConfiguration = 7
        '         NumPanes = 1
        '         Configuration = '(V (3))'
        '      End
        '      Begin PaneConfiguration = 8
        '         NumPanes = 3
        '         Configuration = '(H (1[56] 4[18] 2) )'
        '      End
        '      Begin PaneConfiguration = 9
        '         NumPanes = 2
        '         Configuration = '(H (1 [75] 4))'
        '      End
        '      Begin PaneConfiguration = 10
        '         NumPanes = 2
        '         Configuration = '(H (1[66] 2) )'
        '      End
        '      Begin PaneConfiguration = 11
        '         NumPanes = 2
        '         Configuration = '(H (4 [60] 2))'
        '      End
        '      Begin PaneConfiguration = 12
        '         NumPanes = 1
        '         Configuration = '(H (1) )'
        '      End
        '      Begin PaneConfiguration = 13
        '         NumPanes = 1
        '         Configuration = '(V (4))'
        '      End
        '      Begin PaneConfiguration = 14
        '         NumPanes = 1
        '         Configuration = '(V (2))'
        '      End
        '      ActivePaneConfig = 0
        '   End
        '   Begin DiagramPane = 
        '      Begin Origin = 
        '         Top = 0
        '         Left = 0
        '      End
        '      Begin Tables = 
        '         Begin Table = 'hrs_Employees'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 48
        '               Bottom = 170
        '               Right = 283
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 0
        '         End
        '         Begin Table = 'hrs_EmployeesTransactions'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 331
        '               Bottom = 256
        '               Right = 598
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 10
        '         End
        '         Begin Table = 'hrs_EmployeesTransactionsProjects'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 646
        '               Bottom = 170
        '               Right = 898
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 0
        '         End
        '         Begin Table = 'hrs_EmployeesTransactionsDetails'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 946
        '               Bottom = 311
        '               Right = 1246
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 0
        '         End
        '         Begin Table = 'hrs_TransactionsTypes'
        '            Begin Extent = 
        '               Top = 193
        '               Left = 672
        '               Bottom = 356
        '               Right = 915
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 12
        '         End
        '         Begin Table = 'sys_FiscalYearsPeriods'
        '            Begin Extent = 
        '               Top = 185
        '               Left = 98
        '               Bottom = 348
        '               Right = 300
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 1
        '         End
        '      End
        '   End
        '   Begin SQLPane = 
        '   End
        '   Begin DataPane = 
        '      Begin ParameterDefaults = ''
        '      End
        '' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'hrs_PaidSalaries'
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'hrs_PaidSalaries', NULL,NULL))
        '	EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'      Begin ColumnWidths = 9
        '         Width = 284
        '         Width = 1200
        '         Width = 1104
        '         Width = 3420
        '         Width = 3420
        '         Width = 1284
        '         Width = 1200
        '         Width = 1200
        '         Width = 1200
        '      End
        '   End
        '   Begin CriteriaPane = 
        '      Begin ColumnWidths = 12
        '         Column = 1440
        '         Alias = 1344
        '         Table = 2892
        '         Output = 720
        '         Append = 1400
        '         NewValue = 1170
        '         SortType = 1356
        '         SortOrder = 1416
        '         GroupBy = 1350
        '         Filter = 1356
        '         Or = 1350
        '         Or = 1350
        '         Or = 1350
        '      End
        '   End
        'End
        '' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'hrs_PaidSalaries'
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'hrs_PaidSalaries', NULL,NULL))
        '	EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'hrs_PaidSalaries'
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER PROCEDURE [dbo].[DistributedSalaries]
        '	  @FCode varchar(30) = ''
        '	, @ECode varchar(50) = ''
        '	, @PCode varchar(50) = ''
        'As
        'SELECT 
        '	EMP.Code AS EC
        '	, dbo.fn_GetEmpName(EMP.Code, 1) AS EArbName
        '	, dbo.fn_GetEmpName(EMP.Code, 2) AS EEngName
        '	, FYP.ArbName AS FArbName
        '	, FYP.EngName AS FEngName
        '	, FYP.Code AS FC
        '	, PRJ.ArbName AS PArbName
        '	, PRJ.EngName AS PEngName
        '	, PRJ.Code AS PC
        '	, SDE.ProjectPercentage AS PP
        '	, SDE.ProjectAmount AS PA
        'FROM     
        '	dbo.hrs_SalartDistExec AS SDE 
        '	LEFT OUTER JOIN dbo.sys_FiscalYearsPeriods AS FYP ON SDE.FiscalPeriodID = FYP.ID 
        '	LEFT OUTER JOIN dbo.hrs_Projects AS PRJ ON SDE.ProjectID = PRJ.ID 
        '	LEFT OUTER JOIN dbo.hrs_Employees AS EMP ON SDE.EmployeeID = EMP.ID
        'WHERE
        '		(ISNULL(@ECode,'')='' or EMP.Code = @ECode)
        '	AND (ISNULL(@PCode,'')='' or PRJ.Code = @PCode)
        '	AND (FYP.Code = @FCode OR FYP.EngName = @FCode OR FYP.ArbName = @FCode)
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_ReportsGroups WHERE Code = '020') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_ReportsGroups
        '           (Code, ModuleID, ParentID, EngName, ArbName, ArbName4S, Rank, CompanyID, Remarks, RegUserID, RegComputerID, RegDate, CancelDate)
        '     VALUES
        '           ('020', 2, NULL,	'Salary Distribution Reports', 'تقارير توزيع الراتب' , 'تقارير توزيع الراتب', 1,	14,	NULL, NULL, NULL, GETDATE(), NULL)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Reports WHERE Code = 'SDPW') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Reports
        '           (Code, EngName, ArbName, ArbName4S, EngDescription, ArbDescription, EngTitle, ArbTitle, ReportGroupID, ReportSource, DataSource, ScaleFactor, RegDate)
        '     VALUES
        '           ('SDPW', 'Salary Distribution Project Wise', 'توزيع الراتب على مستوى المشاريع', 'توزيع الراتب على مستوى المشاريع', NULL, NULL, 'Salary Distribution Project Wise', 'توزيع الراتب على مستوى المشاريع', (SELECT TOP 1 ID FROM sys_ReportsGroups WHERE Code = '020'), 1, 'DistributedSalaries', 0,GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].[sys_Reports] SET ArbName = 'توزيع الراتب على مستوى المشاريع', ArbName4S = 'توزيع الراتب على مستوى المشاريع', ArbTitle = 'توزيع الراتب على مستوى المشاريع', ReportGroupID = (SELECT TOP 1 ID FROM sys_ReportsGroups WHERE Code = '020') WHERE Code = 'SDPW'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Reports WHERE Code = 'SDEW') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Reports
        '           (Code, EngName, ArbName, ArbName4S, EngDescription, ArbDescription, EngTitle, ArbTitle, ReportGroupID, ReportSource, DataSource, ScaleFactor, RegDate)
        '     VALUES
        '           ('SDEW', 'Salary Distribution Employee Wise', 'توزيع الراتب على مستوى الموظف', 'توزيع الراتب على مستوى الموظف', NULL, NULL, 'Salary Distribution Employee Wise', 'توزيع الراتب على مستوى الموظف', (SELECT TOP 1 ID FROM sys_ReportsGroups WHERE Code = '020'), 1, 'DistributedSalaries', 0,GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].[sys_Reports] SET ArbName = 'توزيع الراتب على مستوى الموظف', ArbName4S = 'توزيع الراتب على مستوى الموظف', ArbTitle = 'توزيع الراتب على مستوى الموظف', ReportGroupID = (SELECT TOP 1 ID FROM sys_ReportsGroups WHERE Code = '020') WHERE Code = 'SDEW'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM rpw_ReportsCriteriasProperties WHERE ReportID = (SELECT TOP 1 ID FROM sys_Reports WHERE Code = 'SDPW')) = 0
        'BEGIN
        ' INSERT INTO [dbo].rpw_ReportsCriteriasProperties
        '           (Rank, ReportID, FieldName, DataType, Length, EngDescription, ArbDescription, Status, FieldLanguage, SearchID, RegDate)
        '     VALUES
        '           (0, (SELECT TOP 1 ID FROM sys_Reports WHERE Code = 'SDPW'), 'FCode', 'String', 30,	'Fiscal Period Code', 'كود الفترة المالية', 1, 3, 117, GETDATE())

        ' INSERT INTO [dbo].rpw_ReportsCriteriasProperties
        '           (Rank, ReportID, FieldName, DataType, Length, EngDescription, ArbDescription, Status, FieldLanguage, SearchID, RegDate)
        '     VALUES
        '           (1, (SELECT TOP 1 ID FROM sys_Reports WHERE Code = 'SDPW'), 'ECode', 'String', 50,	'Employee Code', 'كود الموظف', 1, 3, 95, GETDATE())

        ' INSERT INTO [dbo].rpw_ReportsCriteriasProperties
        '           (Rank, ReportID, FieldName, DataType, Length, EngDescription, ArbDescription, Status, FieldLanguage, SearchID, RegDate)
        '     VALUES
        '           (2, (SELECT TOP 1 ID FROM sys_Reports WHERE Code = 'SDPW'), 'PCode', 'String', 50,	'Project Code', 'كود المشروع', 1, 3, 11, GETDATE())
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM rpw_ReportsCriteriasProperties WHERE ReportID = (SELECT TOP 1 ID FROM sys_Reports WHERE Code = 'SDEW')) = 0
        'BEGIN
        ' INSERT INTO [dbo].rpw_ReportsCriteriasProperties
        '           (Rank, ReportID, FieldName, DataType, Length, EngDescription, ArbDescription, Status, FieldLanguage, SearchID, RegDate)
        '     VALUES
        '           (0, (SELECT TOP 1 ID FROM sys_Reports WHERE Code = 'SDEW'), 'FCode', 'String', 30,	'Fiscal Period Code', 'كود الفترة المالية', 1, 3, 117, GETDATE())

        ' INSERT INTO [dbo].rpw_ReportsCriteriasProperties
        '           (Rank, ReportID, FieldName, DataType, Length, EngDescription, ArbDescription, Status, FieldLanguage, SearchID, RegDate)
        '     VALUES
        '           (1, (SELECT TOP 1 ID FROM sys_Reports WHERE Code = 'SDEW'), 'ECode', 'String', 50,	'Employee Code', 'كود الموظف', 1, 3, 95, GETDATE())

        ' INSERT INTO [dbo].rpw_ReportsCriteriasProperties
        '           (Rank, ReportID, FieldName, DataType, Length, EngDescription, ArbDescription, Status, FieldLanguage, SearchID, RegDate)
        '     VALUES
        '           (2, (SELECT TOP 1 ID FROM sys_Reports WHERE Code = 'SDEW'), 'PCode', 'String', 50,	'Project Code', 'كود المشروعكود المشروعv', 1, 3, 11, GETDATE())
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Forms WHERE Code = 'frmSalaryDist') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Forms
        '           (Code, EngName, ArbName, ArbName4S, EngDescription, ArbDescription, Rank, ModuleID, Height, Width, RegDate)
        '     VALUES
        '           ('frmSalaryDist','frmSalaryDist.aspx', 'خطة توزيع الراتب', 'خطة توزيع الراتب', 'Salary Distirbution', 'خطة توزيع الراتب', 0, 2, 650, 1100,GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].sys_Forms SET ArbName = 'خطة توزيع الراتب', ArbName4S = 'خطة توزيع الراتب', ArbDescription = 'خطة توزيع الراتب' WHERE Code = 'frmSalaryDist'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Forms WHERE Code = 'frmSalaryDistPlan') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Forms
        '           (Code, EngName, ArbName, ArbName4S, EngDescription, ArbDescription, Rank, ModuleID, Height, Width, RegDate)
        '     VALUES
        '           ('frmSalaryDistPlan', 'frmSalaryDistPlan.aspx', 'اسنادات توزيع الراتب', 'اسنادات توزيع الراتب', 'Salary Distribution Appointment', 'اسنادات توزيع الراتب', 0, 2, 650, 1100, GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].sys_Forms SET ArbName = 'اسنادات توزيع الراتب', ArbName4S = 'اسنادات توزيع الراتب', ArbDescription = 'اسنادات توزيع الراتب' WHERE Code = 'frmSalaryDistPlan'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Forms WHERE Code = 'frmDistributeSalary') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Forms
        '           (Code, EngName, ArbName, ArbName4S, EngDescription, ArbDescription, Rank, ModuleID, Height, Width, RegDate)
        '     VALUES
        '           ('frmDistributeSalary', 'frmEmployeesSelector.aspx?SM=Dis&', 'توزيع الراتب', 'توزيع الراتب', 'Distribute Salary', 'توزيع الراتب', 0, 2, 650, 1100, GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].sys_Forms SET ArbName = 'توزيع الراتب', ArbName4S = 'توزيع الراتب', ArbDescription = 'توزيع الراتب' WHERE Code = 'frmDistributeSalary'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Forms WHERE Code = 'frmSalaryEmployeeDistribution') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Forms
        '           (Code, EngName, ArbName, ArbName4S, EngDescription, ArbDescription, Rank, ModuleID, Height, Width, RegDate)
        '     VALUES
        '           ('frmSalaryEmployeeDistribution', 'frmSalaryEmployeeDistribution.aspx', 'توزيعات موظف معين', 'توزيعات موظف معين', 'Emplyee Salary Distribution', 'توزيعات موظف معين', 0, 2, 650, 1100, GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE [dbo].sys_Forms SET ArbName = 'توزيعات موظف معين', ArbName4S = 'توزيعات موظف معين', ArbDescription = 'توزيعات موظف معين' WHERE Code = 'frmSalaryEmployeeDistribution'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'SalDis') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Menus
        '           (Code, EngName, ArbName, ArbName4S, ParentID, Rank, FormID, ViewFormID, IsHide, ViewType, RegDate)
        '     VALUES
        '           ('SalDis', 'Salary Distributions Process', 'عمليات توزيع الرواتب', 'عمليات توزيع الرواتب', 240, 1, NULL, NULL, 0, 1, GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE sys_Menus SET ArbName = 'عمليات توزيع الرواتب', ArbName4S = 'عمليات توزيع الرواتب' WHERE Code = 'SalDis'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmSalaryDist') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Menus
        '           (Code, EngName, ArbName, ArbName4S, ParentID, Rank, FormID, ViewFormID, IsHide, ViewType, RegDate)
        '     VALUES
        '           ('frmSalaryDist', 'Salary Distirbution', 'خطة توزيع الراتب', 'خطة توزيع الراتب', (SELECT TOP 1 ID FROM sys_Menus WHERE Code = 'SalDis'), 0, (SELECT TOP 1 ID FROM sys_Forms WHERE Code = 'frmSalaryDist'), (SELECT TOP 1 ID FROM sys_Forms WHERE Code = 'frmSalaryDist'), 0, 1, GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE sys_Menus SET ArbName = 'خطة توزيع الراتب', ArbName4S = 'خطة توزيع الراتب' WHERE Code = 'frmSalaryDist'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmSalaryDistPlan') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Menus
        '           (Code, EngName, ArbName, ArbName4S, ParentID, Rank, FormID, ViewFormID, IsHide, ViewType, RegDate)
        '     VALUES
        '           ('frmSalaryDistPlan', 'Salary Distributions Plan', 'اسنادات توزيع الراتب', 'اسنادات توزيع الراتب', (SELECT TOP 1 ID FROM sys_Menus WHERE Code = 'SalDis'), 1, (SELECT TOP 1 ID FROM sys_Forms WHERE Code = 'frmSalaryDistPlan'), (SELECT TOP 1 ID FROM sys_Forms WHERE Code = 'frmSalaryDistPlan'), 0, 1, GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE sys_Menus SET ArbName = 'اسنادات توزيع الراتب', ArbName4S = 'اسنادات توزيع الراتب' WHERE Code = 'frmSalaryDistPlan'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmDistributeSalary') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Menus
        '           (Code, EngName, ArbName, ArbName4S, ParentID, Rank, FormID, ViewFormID, IsHide, ViewType, RegDate)
        '     VALUES
        '           ('frmDistributeSalary', 'Distribute Salary', 'توزيع الراتب', 'توزيع الراتب', (SELECT TOP 1 ID FROM sys_Menus WHERE Code = 'SalDis'), 2, (SELECT TOP 1 ID FROM sys_Forms WHERE Code = 'frmDistributeSalary'), (SELECT TOP 1 ID FROM sys_Forms WHERE Code = 'frmDistributeSalary'), 0, 1, GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE sys_Menus SET ArbName = 'توزيع الراتب', ArbName4S = 'توزيع الراتب' WHERE Code = 'frmDistributeSalary'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmSalaryEmployeeDistribution') = 0
        'BEGIN
        ' INSERT INTO [dbo].sys_Menus
        '           (Code, EngName, ArbName, ArbName4S, ParentID, Rank, FormID, ViewFormID, IsHide, ViewType, RegDate)
        '     VALUES
        '           ('frmSalaryEmployeeDistribution', 'Emplyee Salary Distribution', 'توزيعات موظف معين', 'توزيعات موظف معين', (SELECT TOP 1 ID FROM sys_Menus WHERE Code = 'SalDis'), 1, (SELECT TOP 1 ID FROM sys_Forms WHERE Code = 'frmSalaryEmployeeDistribution'), (SELECT TOP 1 ID FROM sys_Forms WHERE Code = 'frmSalaryEmployeeDistribution'), 0, 1, GETDATE())
        'END
        'ELSE
        'BEGIN
        'UPDATE sys_Menus SET ArbName = 'توزيعات موظف معين', ArbName4S = 'توزيعات موظف معين' WHERE Code = 'frmSalaryEmployeeDistribution'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER FUNCTION [dbo].[fn_CheckEndOfServiceByPeriodWEnd](@EmpID AS INT,@FiscalPeriod as INT)
        'RETURNS INT
        'AS
        'BEGIN

        '		RETURN(ISNULL((
        '			Select top 1 Emp.ID 

        '			From		hrs_Employees			Emp
        '			INNER Join	hrs_Contracts			Cont		On Cont.EmployeeID		= Emp.ID 
        '			Where 	   (Emp.ID		=	@EmpID)
        '				 And   (Emp.CancelDate Is Null)	
        '				 And   (Cont.CancelDate Is Null)															   
        '				 And   (Cont.EndDate Is Null Or Cont.EndDate 		   >= (select ToDate from sys_FiscalYearsPeriods where ID = @FiscalPeriod))       -- Don't get ended contracts
        '				 And   (Emp.ExcludeDate Is Null Or Emp.ExcludeDate     >= (select FromDate from sys_FiscalYearsPeriods where ID = @FiscalPeriod))       -- Don't get ended service employees
        '				 And   (Emp.ExcludeDate Is Null Or Emp.ExcludeDate     > (select ToDate from sys_FiscalYearsPeriods where ID = @FiscalPeriod))       -- Don't get ended service employees
        '				 And   (Cont.StartDate  <= (select ToDate from sys_FiscalYearsPeriods where ID = @FiscalPeriod))       -- Don't get ended contracts
        '				 And   (Emp.JoinDate <= (select ToDate from sys_FiscalYearsPeriods where ID = @FiscalPeriod))       -- Don't get ended service employees
        '			ORDER BY Cont.EndDate DESC
        '		),0))

        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER PROCEDURE [dbo].[FindAllSalaryDist](@employeeID INT)
        'AS
        'BEGIN
        '	SELECT 
        '		dbo.hrs_SalaryDistPlanMember.SalaryDistPlanID AS AppointID
        '		, dbo.hrs_SalaryDist.EngName
        '		, dbo.hrs_SalaryDist.ArbName
        '		, (SELECT TOP 1 FROMDATE FROM sys_FiscalYearsPeriods WHERE ID = (SUBSTRING(dbo.hrs_SalaryDistPlan.PeriodIDs, 0, 4))) AS FromDate
        '		, (SELECT TOP 1 TODATE FROM sys_FiscalYearsPeriods WHERE ID = (SUBSTRING(dbo.hrs_SalaryDistPlan.PeriodIDs, LEN(dbo.hrs_SalaryDistPlan.PeriodIDs)-3, 3))) AS ToDate
        '	FROM     
        '		dbo.hrs_SalaryDist 
        '		INNER JOIN dbo.hrs_SalaryDistPlan ON dbo.hrs_SalaryDist.ID = dbo.hrs_SalaryDistPlan.SalaryDistributionID 
        '		INNER JOIN dbo.hrs_SalaryDistPlanMember ON dbo.hrs_SalaryDistPlan.ID = dbo.hrs_SalaryDistPlanMember.SalaryDistPlanID
        '    WHERE 
        '		hrs_SalaryDistPlanMember.EmployeeID = @employeeID
        '        AND hrs_SalaryDistPlanMember.CancelDate IS NULL
        '	ORDER BY
        '		hrs_SalaryDistPlanMember.RegDate DESC
        'END;
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER VIEW [dbo].[hrs_PaidSalaries]
        'AS
        'SELECT TOP (100) PERCENT dbo.hrs_Employees.ID, dbo.hrs_Employees.Code, dbo.fn_GetEmpName(dbo.hrs_Employees.Code, 1) AS FullName, dbo.fn_GetEmpName(dbo.hrs_Employees.Code, 2) AS EmpEngName, 
        '                  dbo.sys_FiscalYearsPeriods.ArbName AS PeriodCode, dbo.sys_FiscalYearsPeriods.ID AS PeriodID, ROUND(SUM(dbo.hrs_TransactionsTypes.Sign * dbo.hrs_EmployeesTransactionsDetails.NumericValue), 0) AS PaidSalary
        'FROM     dbo.hrs_Employees INNER JOIN
        '                  dbo.hrs_EmployeesTransactions ON dbo.hrs_Employees.ID = dbo.hrs_EmployeesTransactions.EmployeeID INNER JOIN
        '                  dbo.hrs_EmployeesTransactionsProjects ON dbo.hrs_EmployeesTransactions.ID = dbo.hrs_EmployeesTransactionsProjects.EmployeeTransactionID INNER JOIN
        '                  dbo.hrs_EmployeesTransactionsDetails ON dbo.hrs_EmployeesTransactionsProjects.ID = dbo.hrs_EmployeesTransactionsDetails.EmpTransProjID INNER JOIN
        '                  dbo.hrs_TransactionsTypes ON dbo.hrs_EmployeesTransactionsDetails.TransactionTypeID = dbo.hrs_TransactionsTypes.ID INNER JOIN
        '                  dbo.sys_FiscalYearsPeriods ON dbo.hrs_EmployeesTransactions.FiscalYearPeriodID = dbo.sys_FiscalYearsPeriods.ID
        'WHERE  (dbo.hrs_EmployeesTransactions.PrepareType = 'N') AND (dbo.hrs_EmployeesTransactionsDetails.NumericValue > 0) AND (dbo.hrs_EmployeesTransactionsDetails.TextValue = 'Paid')
        'GROUP BY dbo.hrs_Employees.Code, dbo.hrs_Employees.ArbName + ' ' + dbo.hrs_Employees.FatherArbName + ' ' + dbo.hrs_Employees.GrandArbName + ' ' + dbo.hrs_Employees.FamilyArbName, 
        '                  dbo.hrs_Employees.EngName + ' ' + dbo.hrs_Employees.FatherEngName + ' ' + dbo.hrs_Employees.GrandEngName + ' ' + dbo.hrs_Employees.FamilyEngName, dbo.sys_FiscalYearsPeriods.ID, dbo.sys_FiscalYearsPeriods.ArbName, 
        '                  dbo.hrs_Employees.ID
        'ORDER BY PeriodID DESC
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'hrs_PaidSalaries', NULL,NULL))
        '	EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
        'Begin DesignProperties = 
        '   Begin PaneConfigurations = 
        '      Begin PaneConfiguration = 0
        '         NumPanes = 4
        '         Configuration = '(H (1[35] 4[26] 2[13] 3) )'
        '      End
        '      Begin PaneConfiguration = 1
        '         NumPanes = 3
        '         Configuration = '(H (1 [50] 4 [25] 3))'
        '      End
        '      Begin PaneConfiguration = 2
        '         NumPanes = 3
        '         Configuration = '(H (1 [50] 2 [25] 3))'
        '      End
        '      Begin PaneConfiguration = 3
        '         NumPanes = 3
        '         Configuration = '(H (4 [30] 2 [40] 3))'
        '      End
        '      Begin PaneConfiguration = 4
        '         NumPanes = 2
        '         Configuration = '(H (1 [56] 3))'
        '      End
        '      Begin PaneConfiguration = 5
        '         NumPanes = 2
        '         Configuration = '(H (2 [66] 3))'
        '      End
        '      Begin PaneConfiguration = 6
        '         NumPanes = 2
        '         Configuration = '(H (4 [50] 3))'
        '      End
        '      Begin PaneConfiguration = 7
        '         NumPanes = 1
        '         Configuration = '(V (3))'
        '      End
        '      Begin PaneConfiguration = 8
        '         NumPanes = 3
        '         Configuration = '(H (1[56] 4[18] 2) )'
        '      End
        '      Begin PaneConfiguration = 9
        '         NumPanes = 2
        '         Configuration = '(H (1 [75] 4))'
        '      End
        '      Begin PaneConfiguration = 10
        '         NumPanes = 2
        '         Configuration = '(H (1[66] 2) )'
        '      End
        '      Begin PaneConfiguration = 11
        '         NumPanes = 2
        '         Configuration = '(H (4 [60] 2))'
        '      End
        '      Begin PaneConfiguration = 12
        '         NumPanes = 1
        '         Configuration = '(H (1) )'
        '      End
        '      Begin PaneConfiguration = 13
        '         NumPanes = 1
        '         Configuration = '(V (4))'
        '      End
        '      Begin PaneConfiguration = 14
        '         NumPanes = 1
        '         Configuration = '(V (2))'
        '      End
        '      ActivePaneConfig = 0
        '   End
        '   Begin DiagramPane = 
        '      Begin Origin = 
        '         Top = 0
        '         Left = 0
        '      End
        '      Begin Tables = 
        '         Begin Table = 'hrs_Employees'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 48
        '               Bottom = 170
        '               Right = 283
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 0
        '         End
        '         Begin Table = 'hrs_EmployeesTransactions'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 331
        '               Bottom = 256
        '               Right = 598
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 10
        '         End
        '         Begin Table = 'hrs_EmployeesTransactionsProjects'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 646
        '               Bottom = 170
        '               Right = 898
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 0
        '         End
        '         Begin Table = 'hrs_EmployeesTransactionsDetails'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 946
        '               Bottom = 311
        '               Right = 1246
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 0
        '         End
        '         Begin Table = 'hrs_TransactionsTypes'
        '            Begin Extent = 
        '               Top = 193
        '               Left = 672
        '               Bottom = 356
        '               Right = 915
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 12
        '         End
        '         Begin Table = 'sys_FiscalYearsPeriods'
        '            Begin Extent = 
        '               Top = 185
        '               Left = 98
        '               Bottom = 348
        '               Right = 300
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 1
        '         End
        '      End
        '   End
        '   Begin SQLPane = 
        '   End
        '   Begin DataPane = 
        '      Begin ParameterDefaults = ''
        '      End
        '' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'hrs_PaidSalaries'
        'ELSE
        'BEGIN
        '	EXEC sys.sp_updateextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
        'Begin DesignProperties = 
        '   Begin PaneConfigurations = 
        '      Begin PaneConfiguration = 0
        '         NumPanes = 4
        '         Configuration = '(H (1[35] 4[26] 2[13] 3) )'
        '      End
        '      Begin PaneConfiguration = 1
        '         NumPanes = 3
        '         Configuration = '(H (1 [50] 4 [25] 3))'
        '      End
        '      Begin PaneConfiguration = 2
        '         NumPanes = 3
        '         Configuration = '(H (1 [50] 2 [25] 3))'
        '      End
        '      Begin PaneConfiguration = 3
        '         NumPanes = 3
        '         Configuration = '(H (4 [30] 2 [40] 3))'
        '      End
        '      Begin PaneConfiguration = 4
        '         NumPanes = 2
        '         Configuration = '(H (1 [56] 3))'
        '      End
        '      Begin PaneConfiguration = 5
        '         NumPanes = 2
        '         Configuration = '(H (2 [66] 3))'
        '      End
        '      Begin PaneConfiguration = 6
        '         NumPanes = 2
        '         Configuration = '(H (4 [50] 3))'
        '      End
        '      Begin PaneConfiguration = 7
        '         NumPanes = 1
        '         Configuration = '(V (3))'
        '      End
        '      Begin PaneConfiguration = 8
        '         NumPanes = 3
        '         Configuration = '(H (1[56] 4[18] 2) )'
        '      End
        '      Begin PaneConfiguration = 9
        '         NumPanes = 2
        '         Configuration = '(H (1 [75] 4))'
        '      End
        '      Begin PaneConfiguration = 10
        '         NumPanes = 2
        '         Configuration = '(H (1[66] 2) )'
        '      End
        '      Begin PaneConfiguration = 11
        '         NumPanes = 2
        '         Configuration = '(H (4 [60] 2))'
        '      End
        '      Begin PaneConfiguration = 12
        '         NumPanes = 1
        '         Configuration = '(H (1) )'
        '      End
        '      Begin PaneConfiguration = 13
        '         NumPanes = 1
        '         Configuration = '(V (4))'
        '      End
        '      Begin PaneConfiguration = 14
        '         NumPanes = 1
        '         Configuration = '(V (2))'
        '      End
        '      ActivePaneConfig = 0
        '   End
        '   Begin DiagramPane = 
        '      Begin Origin = 
        '         Top = 0
        '         Left = 0
        '      End
        '      Begin Tables = 
        '         Begin Table = 'hrs_Employees'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 48
        '               Bottom = 170
        '               Right = 283
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 0
        '         End
        '         Begin Table = 'hrs_EmployeesTransactions'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 331
        '               Bottom = 256
        '               Right = 598
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 10
        '         End
        '         Begin Table = 'hrs_EmployeesTransactionsProjects'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 646
        '               Bottom = 170
        '               Right = 898
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 0
        '         End
        '         Begin Table = 'hrs_EmployeesTransactionsDetails'
        '            Begin Extent = 
        '               Top = 7
        '               Left = 946
        '               Bottom = 311
        '               Right = 1246
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 0
        '         End
        '         Begin Table = 'hrs_TransactionsTypes'
        '            Begin Extent = 
        '               Top = 193
        '               Left = 672
        '               Bottom = 356
        '               Right = 915
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 12
        '         End
        '         Begin Table = 'sys_FiscalYearsPeriods'
        '            Begin Extent = 
        '               Top = 185
        '               Left = 98
        '               Bottom = 348
        '               Right = 300
        '            End
        '            DisplayFlags = 280
        '            TopColumn = 1
        '         End
        '      End
        '   End
        '   Begin SQLPane = 
        '   End
        '   Begin DataPane = 
        '      Begin ParameterDefaults = ''
        '      End
        '' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'hrs_PaidSalaries'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'hrs_PaidSalaries', NULL,NULL))
        '	EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'      Begin ColumnWidths = 9
        '         Width = 284
        '         Width = 1200
        '         Width = 1104
        '         Width = 3420
        '         Width = 3420
        '         Width = 1284
        '         Width = 1200
        '         Width = 1200
        '         Width = 1200
        '      End
        '   End
        '   Begin CriteriaPane = 
        '      Begin ColumnWidths = 12
        '         Column = 1440
        '         Alias = 1344
        '         Table = 2892
        '         Output = 720
        '         Append = 1400
        '         NewValue = 1170
        '         SortType = 1356
        '         SortOrder = 1416
        '         GroupBy = 1350
        '         Filter = 1356
        '         Or = 1350
        '         Or = 1350
        '         Or = 1350
        '      End
        '   End
        'End
        '' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'hrs_PaidSalaries'
        'ELSE
        'BEGIN
        '	EXEC sys.sp_updateextendedproperty @name=N'MS_DiagramPane2', @value=N'      Begin ColumnWidths = 9
        '         Width = 284
        '         Width = 1200
        '         Width = 1104
        '         Width = 3420
        '         Width = 3420
        '         Width = 1284
        '         Width = 1200
        '         Width = 1200
        '         Width = 1200
        '      End
        '   End
        '   Begin CriteriaPane = 
        '      Begin ColumnWidths = 12
        '         Column = 1440
        '         Alias = 1344
        '         Table = 2892
        '         Output = 720
        '         Append = 1400
        '         NewValue = 1170
        '         SortType = 1356
        '         SortOrder = 1416
        '         GroupBy = 1350
        '         Filter = 1356
        '         Or = 1350
        '         Or = 1350
        '         Or = 1350
        '      End
        '   End
        'End
        '' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'hrs_PaidSalaries'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'hrs_PaidSalaries', NULL,NULL))
        '	EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'hrs_PaidSalaries'
        'ELSE
        'BEGIN
        '	EXEC sys.sp_updateextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'hrs_PaidSalaries'
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'UPDATE hrs_EmployeesTransactionsDetails SET TextValue = 'Paid' WHERE TextValue = 'Piaid'

        'SET ANSI_NULLS ON
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER FUNCTION [dbo].[hrs_EndOfServiceAmount] (@EmployeeID int)   
        '	RETURNS 	money
        '	As  
        'Begin
        'Return isnull((
        'select NumericValue  from hrs_EmployeesTransactionsDetails empdet
        'inner join hrs_EmployeesTransactionsProjects empproj on empproj.id=empdet.EmpTransProjID 
        'inner join hrs_EmployeesTransactions emptrn on emptrn.id=empproj.EmployeeTransactionID 
        'where	
        'emptrn.EmployeeID =@EmployeeID  
        'and emptrn.PrepareType ='E'
        ') ,0)
        'End
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER PROCEDURE [dbo].[hrs_rptChartEmployeesEOS]	--exec [hrs_rptChartEmployeeseos]
        '	@EmployeeCode	 	Varchar(max)   = '',
        '	@EOSCode 		Varchar(max)   = '',
        ' 	@ProfessionCode 		Varchar(max)   = '',
        ' 	@NationalitieCode 		Varchar(max)	  = '',
        ' 	@DepartmentCode		Varchar(max)		='',
        ' 	@BrancheCode			Varchar(max)	  = '',	
        '	@SponsorCode    Varchar(max)	  = '',
        '	@JoinMonth 		Smalldatetime = '01/01/1900',
        '	@ToJoinMonth 		Smalldatetime = '01/01/2020'
        'As
        'Set Dateformat dmy	

        'Select	
        '	hrs_Employees.Code															AS EmpCode,
        '	IsNull(hrs_Employees.EngName,'')+' '+IsNull(hrs_Employees.FatherEngName,'')+' '+IsNull(hrs_Employees.GrandEngName,'') +' '+ IsNull(hrs_Employees.FamilyEngName,'')				As EmpEngName,
        '	IsNull(hrs_Employees.ArbName,'')+' '+IsNull(hrs_Employees.FatherArbName,'')+' '+IsNull(hrs_Employees.GrandArbName,'') +' '+ IsNull(hrs_Employees.FamilyArbName,'')				As EmpArbName,
        '  	sys_Departments.Code as DepartmentsCode,
        '  	sys_Departments.EngName As DepartmentEngName,
        '  	sys_Departments.ArbName As DepartmentArbName,		
        '	sys_Nationalities.EngName AS  NationalityEngName,
        '	sys_Nationalities.ArbName	As NationalityArbName,
        '	CONVERT(date,hrs_Employees.ExcludeDate)					AS EosDate,
        '	sys_Branches.Code						As BranchesCode,
        '	sys_Branches.EngName				As BranchEngName,
        '	sys_Branches.ArbName				As BranchArbName,
        '	hrs_Sponsors.EngName				As SponsorEngName,
        '	hrs_Sponsors.ArbName				As SponsorArbName,
        '	hrs_EndOfServices.ArbName		As EOSArbName,
        '	hrs_EndOfServices.EngName		As EOSEngName,
        '	dbo.hrs_EndOfServiceAmount(hrs_Employees.id) as EOSPaidValue,
        '	CONVERT(date,hrs_employees.JoinDate) as joindate,
        '	CONVERT(date,hrs_employees.BirthDate) as birthdate,
        '	dbo.fn_GetTotalAdditions(c.id,hrs_Employees.ExcludeDate ) Salary,
        '	hrs_Positions.ArbName			As PostionArbName,
        '	hrs_Positions.EngName			As PostionEngName,
        '	hrs_Employees.SSnNo				As ssnno
        'From 
        '	hrs_Employees 
        '		Left Join hrs_EmployeesJoins		On hrs_Employees.ID= hrs_EmployeesJoins.EmployeeID
        '		Left Join hrs_EndOfServices		On hrs_EmployeesJoins.EndOfServiceID= hrs_EndOfServices.ID
        '		Left Join sys_Departments			On sys_Departments.ID=hrs_Employees.DepartmentID
        '		Left Join sys_Nationalities			On sys_Nationalities.ID= hrs_Employees.NationalityID 
        '		inner Join (Select Number,EmployeeID,ID,EndDate,ContractTypeID,StartDate,ContractPeriod,PositionID,ProfessionID,EmployeeClassID,GradeStepID From hrs_Contracts 
        '      Where CancelDate Is Null  
        '	  And ID = (Select Top 1 Cont.ID  From hrs_Contracts Cont 
        '				Where cont.StartDate <= GETDATE() 
        '				--and IsNull(cont.EndDate, '30/12/2070') >= GETDATE() 
        '				and Cont.EmployeeID = hrs_Contracts.EmployeeID 
        '				And cont.CancelDate Is Null 
        '				Order by IsNull(Cont.EndDate,'30/12/2070') Desc)
        '				 ) C On hrs_Employees.ID = C.EmployeeID
        '		Left Join hrs_Professions			On hrs_Professions.ID=C.ProfessionID 
        '		Left Join sys_Branches				On sys_Branches.ID = hrs_Employees.BranchID
        '		Left JOin hrs_Sponsors				On hrs_Sponsors.ID = hrs_Employees.SponsorID
        '		LEFT JOIN hrs_Contracts				On hrs_Contracts.EmployeeID = hrs_Employees.ID and hrs_Contracts.CancelDate is not null
        '		LEFT JOIN hrs_Positions				On hrs_Positions.ID = hrs_Contracts.PositionID
        'Where 
        '	hrs_Employees.CancelDate			Is Null and isnull(hrs_Employees.RegComputerID,0) = 0
        '	and (@EmployeeCode = '' or hrs_Employees.Code					Like @EmployeeCode + '%')
        'AND (@NationalitieCode = '' or sys_Nationalities.Code 				LIKE @NationalitieCode + '%')
        'And (@DepartmentCode = '' or sys_Departments.Code				Like @DepartmentCode+'%')
        'And (@BrancheCode = '' or sys_Branches.Code					Like @BrancheCode + '%')
        'And (@SponsorCode = '' or hrs_Sponsors.Code					Like @SponsorCode +'%')
        'And hrs_Employees.ExcludeDate between @JoinMonth and @ToJoinMonth	
        'And (@EOSCode = '' or hrs_EndOfServices.Code					Like @EOSCode + '%')
        'Order by 
        'Case When IsNumeric(hrs_Employees.Code) = 1 then Right(Replicate('0',51) + hrs_Employees.Code, 50) When IsNumeric(hrs_Employees.Code) = 0 then Left(hrs_Employees.Code + Replicate('',51), 50) Else hrs_Employees.Code End
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('sys_Companies', 'AllowOverVacation') IS NULL  
        'BEGIN
        '    ALTER TABLE sys_Companies
        '    ADD AllowOverVacation
        ' bit
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "


        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_SalaryProductionColumns'
        ')
        'BEGIN
        '    CREATE TABLE [dbo].[hrs_SalaryProductionColumns](
        '														[Id] [int] IDENTITY(1,1) NOT NULL,
        '														[Name] [varchar](250) NOT NULL,
        '														[DataSource] [varchar](1000) NOT NULL,
        '														[RegUserID] [int] NULL,
        '														[RegComputerID] [int] NULL,
        '														[RegDate] [smalldatetime] NOT NULL,
        '														[CancelDate] [smalldatetime] NULL,
        '														CONSTRAINT [PK_SalaryProductionfields] PRIMARY KEY CLUSTERED 
        '														([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        '													) ON [PRIMARY]
        '	SET IDENTITY_INSERT [dbo].[hrs_SalaryProductionColumns] ON 
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (5, N'Employee Code', N'e.Code', NULL, NULL, CAST(N'2022-07-02T13:24:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (6, N'Employee ArbName', N'dbo.fn_GetEmpName(e.Code,1)', NULL, NULL, CAST(N'2022-07-02T13:24:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (8, N'Employee EngName', N'dbo.fn_GetEmpName(e.Code,0)', NULL, NULL, CAST(N'2022-07-02T13:25:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (9, N'Iqama', N'isnull(e.SSnNo,'''')', NULL, NULL, CAST(N'2022-07-02T13:25:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (10, N'Account Number', N'Char(39) + isnull(e.BankAccountNumber,'''')', NULL, NULL, CAST(N'2022-07-02T13:26:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (11, N'Bank Name', N'B.Code', NULL, NULL, CAST(N'2022-07-02T13:26:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (12, N'Net Salary', N'dbo.fn_GetNetSalary(e.ID,@FisicalPeriodId)', NULL, NULL, CAST(N'2022-07-02T13:26:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (13, N'Basic', N'dbo.fn_GetTotalBasic(e.ID,@FisicalPeriodId) ', NULL, NULL, CAST(N'2022-07-02T13:27:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (14, N'House', N'dbo.fn_GetTotalHousing(e.ID,@FisicalPeriodId)', NULL, NULL, CAST(N'2022-07-02T13:27:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (15, N'Other benfits', N'dbo.fn_GetTotalOthers(e.ID,@FisicalPeriodId)', NULL, NULL, CAST(N'2022-07-02T13:27:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (16, N'Dedcutions', N'dbo.fn_GetTotalDeductions(e.ID,@FisicalPeriodId)', NULL, NULL, CAST(N'2022-07-02T13:28:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (17, N'Branch', N'isnull((select ArbName from sys_Branches where ID = e.BranchID),'''')', NULL, NULL, CAST(N'2022-07-02T15:11:00' AS SmallDateTime), NULL)
        '	INSERT [dbo].[hrs_SalaryProductionColumns] ([Id], [Name], [DataSource], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) VALUES (18, N'LaborOffice', N'isnull(e.LaborOfficeNo,'''')', NULL, NULL, CAST(N'2022-07-02T15:12:00' AS SmallDateTime), NULL)
        '	SET IDENTITY_INSERT [dbo].[hrs_SalaryProductionColumns] OFF
        '	ALTER TABLE [dbo].[hrs_SalaryProductionColumns] ADD  CONSTRAINT [DF_SalaryProductionfields_RegDate]  DEFAULT (getdate()) FOR [RegDate]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_SalaryProductionFiles'
        ')
        'BEGIN
        '	CREATE TABLE [dbo].[hrs_SalaryProductionFiles](
        '													[Id] [int] IDENTITY(1,1) NOT NULL,
        '													[Code] [varchar](50) NOT NULL,
        '													[EngName] [varchar](250) NOT NULL,
        '													[ArbName] [varchar](250) NOT NULL,
        '													[RegUserID] [int] NULL,
        '													[RegComputerID] [int] NULL,
        '													[RegDate] [smalldatetime] NOT NULL,
        '													[CancelDate] [smalldatetime] NULL,
        '													CONSTRAINT [PK_SlaryProductionFiles] PRIMARY KEY CLUSTERED 
        '													([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        '												 ) ON [PRIMARY]
        '	SET IDENTITY_INSERT [dbo].[hrs_SalaryProductionFiles] ON 
        '	SET IDENTITY_INSERT [dbo].[hrs_SalaryProductionFiles] OFF
        '	ALTER TABLE [dbo].[hrs_SalaryProductionFiles] ADD  CONSTRAINT [DF_SlaryProductionFiles_RegDate]  DEFAULT (getdate()) FOR [RegDate]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_SalaryProductionFilesColumns'
        ')
        'BEGIN
        '	CREATE TABLE [dbo].[hrs_SalaryProductionFilesColumns](
        '															[Id] [int] IDENTITY(1,1) NOT NULL,
        '															[SalaryProductionFileId] [int] NOT NULL,
        '															[SalaryProductionColumnId] [int] NOT NULL,
        '															[Name] [varchar](250) NULL,
        '															[RegUserID] [int] NULL,
        '															[RegComputerID] [int] NULL,
        '															[RegDate] [smalldatetime] NOT NULL,
        '															[CancelDate] [smalldatetime] NULL,
        '															CONSTRAINT [PK_SalaryProductionFilesFields] PRIMARY KEY CLUSTERED 
        '															([Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        '														) ON [PRIMARY]
        '	SET IDENTITY_INSERT [dbo].[hrs_SalaryProductionFilesColumns] ON 
        '	SET IDENTITY_INSERT [dbo].[hrs_SalaryProductionFilesColumns] OFF
        '	ALTER TABLE [dbo].[hrs_SalaryProductionFilesColumns] ADD  CONSTRAINT [DF_SalaryProductionFilesFields_RegDate]  DEFAULT (getdate()) FOR [RegDate]
        '	ALTER TABLE [dbo].[hrs_SalaryProductionFilesColumns]  WITH CHECK ADD  CONSTRAINT [FK_hrs_SalaryProductionFilesColumns_hrs_SalaryProductionColumns] FOREIGN KEY([SalaryProductionColumnId])
        '	REFERENCES [dbo].[hrs_SalaryProductionColumns] ([Id])
        '	ON DELETE CASCADE
        '	ALTER TABLE [dbo].[hrs_SalaryProductionFilesColumns] CHECK CONSTRAINT [FK_hrs_SalaryProductionFilesColumns_hrs_SalaryProductionColumns]
        '	ALTER TABLE [dbo].[hrs_SalaryProductionFilesColumns]  WITH CHECK ADD  CONSTRAINT [FK_hrs_SalaryProductionFilesColumns_hrs_SalaryProductionFiles] FOREIGN KEY([SalaryProductionFileId])
        '	REFERENCES [dbo].[hrs_SalaryProductionFiles] ([Id])
        '	ON DELETE CASCADE
        '	ALTER TABLE [dbo].[hrs_SalaryProductionFilesColumns] CHECK CONSTRAINT [FK_hrs_SalaryProductionFilesColumns_hrs_SalaryProductionFiles]
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Forms WHERE Code = 'frmSalaryProductionFilesSetting') = 0
        'BEGIN
        '	INSERT INTO [dbo].[sys_Forms] ( [Code], [EngName], [ArbName], [ArbName4S], [EngDescription], [ArbDescription], [Rank], [ModuleID], [SearchFormID], [Height], [Width], [Remarks], [RegUserID], [RegComputerID], [RegDate], [CancelDate], [Layout], [LinkTarget], [LinkUrl], [ImageUrl], [MainID]) 
        '	VALUES ( 'frmSalaryProductionFilesSetting', 'frmSalaryProductionFilesSetting.aspx', NULL, NULL, 'Salary Protection File Settings', 'إعداد ملف حماية الأجور', 0, 2, NULL, 650, 1100, NULL, NULL, NULL, '20110726 13:23:00.000', NULL, NULL, NULL, NULL, NULL, NULL)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmSalaryProductionFilesSetting') = 0
        'BEGIN
        '	INSERT INTO [dbo].[sys_Menus] ( [Code], [EngName], [ArbName], [ArbName4S], [ParentID], [Shortcut], [Rank], [FormID], [ObjectID], [ViewFormID], [IsHide], [Image], [ViewType], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) 
        '	VALUES ( 'frmSalaryProductionFilesSetting', 'Salary Protection File Settings', 'إعداد ملف حماية الأجور', 'إعداد ملف حماية الأجور', (SELECT ID FROM sys_Menus WHERE CODE = '0024'), NULL, 9, (SELECT ID FROM sys_Forms WHERE Code = 'frmSalaryProductionFilesSetting'), NULL, NULL, 0, NULL, 1, NULL, NULL, '20110726 14:00:00.000', NULL)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_FormsPermissions WHERE UserID = 1 AND FormID = 
        '			(SELECT ID FROM sys_Forms WHERE Code = 'frmSalaryProductionFilesSetting')) = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_FormsPermissions] ([FormID] ,[UserID] ,[AllowView] ,[AllowAdd] ,[AllowEdit] ,[AllowDelete] ,[AllowPrint] ,[RegUserID] ,[RegDate])
        '     VALUES ((SELECT ID FROM sys_Forms WHERE Code = 'frmSalaryProductionFilesSetting') ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,GETDATE())
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "


        'IF(SELECT COUNT(*) FROM sys_Forms WHERE Code = 'frmSalaryProtectionSystem2') = 0
        'BEGIN
        '	INSERT INTO [dbo].[sys_Forms] ( [Code], [EngName], [ArbName], [ArbName4S], [EngDescription], [ArbDescription], [Rank], [ModuleID], [SearchFormID], [Height], [Width], [Remarks], [RegUserID], [RegComputerID], [RegDate], [CancelDate], [Layout], [LinkTarget], [LinkUrl], [ImageUrl], [MainID]) 
        '	VALUES ( 'frmSalaryProtectionSystem2', 'frmSalaryProtectionSystem2.aspx', NULL, NULL, 'Salary Protection Bank File', 'ملف حماية الأجور للبنوك', 0, 2, NULL, 650, 1100, NULL, NULL, NULL, '20110726 13:23:00.000', NULL, NULL, NULL, NULL, NULL, NULL)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_Menus WHERE Code = 'frmSalaryProtectionSystem2') = 0
        'BEGIN
        '	INSERT INTO [dbo].[sys_Menus] ( [Code], [EngName], [ArbName], [ArbName4S], [ParentID], [Shortcut], [Rank], [FormID], [ObjectID], [ViewFormID], [IsHide], [Image], [ViewType], [RegUserID], [RegComputerID], [RegDate], [CancelDate]) 
        '	VALUES ( 'frmSalaryProtectionSystem2', 'Salary Protection Bank File', 'ملف حماية الأجور للبنوك', 'ملف حماية الأجور للبنوك', (SELECT ID FROM sys_Menus WHERE CODE = '0024'), NULL, 9, (SELECT ID FROM sys_Forms WHERE Code = 'frmSalaryProtectionSystem2'), NULL, NULL, 0, NULL, 1, NULL, NULL, '20110726 14:00:00.000', NULL)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF(SELECT COUNT(*) FROM sys_FormsPermissions WHERE UserID = 1 AND FormID = 
        '			(SELECT ID FROM sys_Forms WHERE Code = 'frmSalaryProtectionSystem2')) = 0
        'BEGIN
        ' INSERT INTO [dbo].[sys_FormsPermissions] ([FormID] ,[UserID] ,[AllowView] ,[AllowAdd] ,[AllowEdit] ,[AllowDelete] ,[AllowPrint] ,[RegUserID] ,[RegDate])
        '     VALUES ((SELECT ID FROM sys_Forms WHERE Code = 'frmSalaryProtectionSystem2') ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,GETDATE())
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_SalaryProductionFiles', 'CompanyID') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_SalaryProductionFiles
        '    ADD CompanyID int
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesVacations', 'ParentVacationID') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesVacations
        '    ADD ParentVacationID int
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "


        'CREATE OR ALTER PROCEDURE [dbo].[hrs_rptVacationFroms]
        '	@EmpVacID		INT
        'AS
        'BEGIN

        '	SET Dateformat DMY;

        '	Declare @VacType AS Int = (Select Top 1 ID From hrs_VacationsTypes Where IsAnnual=1)

        '	Select 
        '		Emp.Code								As EmpCode,
        '		Emp.SSnNo                               AS EMPIQAMA,
        '		dbo.fn_GetEmpName(Emp.Code,0)			As EmpEngName,
        '		dbo.fn_GetEmpName(Emp.Code,1)			As EmpArbName,
        '		CONVERT(varchar(10),Emp.JoinDate,105)				As JoinDate,

        '		CONVERT(varchar(10),ExpectedStartDate,105)	As ExpectedStartDate,
        '		CONVERT(varchar(10),ExpectedEndDate,105)	As ExpectedEndDate,
        '		CONVERT(varchar(10),ISNULL((select top 1 ActualEndDate from hrs_EmployeesVacations where CancelDate is null and EmployeeID = Emp.ID order by ActualEndDate desc),isnull((select GBalanceDate from hrs_EmployeeVacationOpenBalance where EmployeeID = emp.ID),Emp.JoinDate)),105)
        '		As LastVacationDate,
        '		CONVERT(varchar(10),EmpVac.ActualStartDate,105)	As StartDate,
        '		CONVERT(varchar(10),EmpVac.ActualEndDate,105)		As EndDate,
        '		EmpVac.TotalDays,
        '		EmpVac.RemainingDays,
        '		EmpVac.ConsumDays,
        '		EmpVac.Remarks,

        '		Dep.EngName								As DepEngName,
        '		Dep.ArbName								As DepArbName,

        '		Pos.EngName								As PosEngName,
        '		Pos.ArbName								As PosArbName 

        '		, (SELECT TOP 1 ISNULL(ConsumDays,0) FROM hrs_EmployeesVacations WHERE ParentVacationID = @EmpVacID AND CancelDate IS NULL) AS UnPaid_DAYS
        '		, EmpVac.vactiondays
        '	From hrs_Employees							Emp 
        '	Inner Join hrs_EmployeesVacations			EmpVac		On EmpVac.EmployeeID = Emp.ID
        '	Inner Join hrs_Contracts					Cont		On Cont.EmployeeID = Emp.ID 
        '	Left  Join sys_Departments					Dep			On Dep.ID = Emp.DepartmentID 
        '	Left  Join hrs_Positions					Pos			On Pos.ID = Cont.PositionID 

        '	Where
        '		EmpVac.ID = @EmpVacID
        '		And EmpVac.VacationTypeID = @VacType
        '		And EmpVac.CancelDate Is Null
        '		And Emp.CancelDate Is Null
        'End
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE OR ALTER PROCEDURE [dbo].[GetAllEmployeePreviousVacationsForPeriod] 
        '	-- Add the parameters for the stored procedure here

        '	@EmployeeID AS INT
        '	, @PeriodID AS INT
        'AS
        'BEGIN
        '	SET NOCOUNT ON;

        '	DECLARE @SD AS Date = (SELECT FromDate FROM sys_FiscalYearsPeriods WHERE ID = @PeriodID)
        '	DECLARE @ED AS Date = (SELECT ToDate FROM sys_FiscalYearsPeriods WHERE ID = @PeriodID)
        '	SELECT 
        '		OV.ActualStartDate AS VS, OV.ActualEndDate AS VE, OV.ConsumDays AS VD, VT.ID as VTID, VT.EngName AS VTYPE, @SD AS FSD, @ED AS FED
        '	FROM
        '		hrs_EmployeesVacations OV
        '		INNER JOIN hrs_VacationsTypes VT ON OV.VacationTypeID = VT.ID
        '	WHERE
        '		OV.EmployeeID = @EmployeeID 
        '		AND OV.CancelDate IS NULL
        '		AND (OV.ActualStartDate BETWEEN @SD AND @ED OR OV.ActualEndDate BETWEEN @SD AND @ED)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER FUNCTION [dbo].[GetVacation_BalanceByDate](@EmpCode Varchar(30),@CDate date,@fromdate date=null,@IsLessThanCurrentTodate bit=false)
        'Returns real
        'As
        'Begin
        'Declare @CurrentCDateFicalPeriodID int =(select top 1 id from sys_FiscalYearsPeriods where @CDate between FromDate and ToDate)
        'Declare @EmployeeID int = (select ID from hrs_Employees where Code = @EmpCode)
        'declare @ContractID int = dbo.hrs_GetEmpCurrentContract(@EmployeeID,@CDate)
        'Declare @AnnualVacationID As Int = IsNull((Select Top 1 ID From hrs_VacationsTypes Where IsAnnual= 1 And CancelDate Is Null),0)
        'Declare @ContractStartDate As Smalldatetime = isnull((Select Top(1) StartDate From hrs_Contracts Where EmployeeID = @EmployeeID And CancelDate Is Null And ID = @ContractID),'1900-01-01')-- (isnull(@fiscalyear,'') ='' or FiscalYearPeriodID <@fiscalyear)
        'Declare @lastVacPaymentdate As Smalldatetime = isnull((Select Top 1 PaidDate From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where EmployeeId = @EmployeeID And PrepareType = 'V' and (@IsLessThanCurrentTodate=0 or  PaidDate <=@CDate )   and (@IsLessThanCurrentTodate=0 or FiscalYearPeriodID <=@CurrentCDateFicalPeriodID )  order by PaidDate desc),'1900-01-01')
        'Declare @PaymentTrnsID As int = isnull((Select Top 1 hrs_EmployeesTransactions.id From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where EmployeeId = @EmployeeID And PrepareType = 'V' order by todate desc),0)
        'Declare @LastVacReturn As date = isnull((select MAX(ISNULL(ActualEndDate,'2050-01-01')) from hrs_EmployeesVacations where CancelDate is null and EmployeeID = @EmployeeID and VacationTypeID = @AnnualVacationID and PaymentTrnID is not null),'1900-01-01')
        'Declare @LastVacRemaining As real = (select top 1 isnull(RemainingDays,0) from hrs_EmployeesVacations where CancelDate is null and EmployeeID = @EmployeeID and VacationTypeID = @AnnualVacationID order by ID desc)
        'Declare @OpenBalanceDate As Smalldatetime = isnull((select GBalanceDate from hrs_EmployeeVacationOpenBalance where EmployeeID = @EmployeeID),'1900-01-01')
        'Declare @OpenBalanceDays As real = (select [Days] from hrs_EmployeeVacationOpenBalance where RegComputerID <> 1 and EmployeeID = @EmployeeID)
        'Declare @TotalWDays as real = DATEDIFF(Day,@ContractStartDate,GETDATE())
        'declare @ReqWDays As real = (select RequiredWorkingMonths from hrs_ContractsVacations where ContractID = @ContractID and @TotalWDays between ISNULL(FromMonth,0) and ISNULL(ToMonth,999999))
        'declare @DesDays As real = (select DurationDays from hrs_ContractsVacations where ContractID = @ContractID and @TotalWDays between ISNULL(FromMonth,0) and ISNULL(ToMonth,999999))
        'declare @CalcDays as real = 0
        'declare @Lastvactiondays as int =  isnull((select DATEDIFF(day,ActualStartDate,ActualEndDate ) from hrs_EmployeesVacations where CancelDate is null and EmployeeID = @EmployeeID and VacationTypeID = @AnnualVacationID and PaymentTrnID =@PaymentTrnsID),0)
        'declare @duedays real = 0
        'declare @vactiotypeCalculationCode varchar(3)=(select code from hrs_VactionTypeCalculation where id in(select VactionTypeCaculation from hrs_VacationsTypes where id=@AnnualVacationID))
        'declare @joindate date=(select joindate from hrs_employees where id=@EmployeeID )
        'declare @duedate as date=@joindate
        'declare @WorkingDayFromstart int =dbo.Fn_GetDays360 (@joindate ,@CDate)
        'declare @workingDaysToDueDate int=0
        'declare @ToMonths int
        'declare @DurationDays					int =0
        'declare @RequiredWorkingMonths		    real=0
        'declare @TDays							int=0
        'declare @UnpaidDays						int=0
        'declare @VactionOpenBalance             real=0
        '--ADDED BY MOHANNAD
        '-------------------
        'DECLARE @START_CAL_DATE			date
        'Declare @Contract_StartDate		date
        'Declare @Contract_EndDate		date
        'Declare @JoindateNew			date
        'Declare @EndContract			date
        'Declare CurX Cursor  For
        'Select StartDate,enddate from hrs_Contracts Where EmployeeID = 17
        'Open 	CurX
        'FETCH NEXT FROM CurX into 	
        '			 @Contract_StartDate
        '			,@Contract_EndDate
        'Set @JoindateNew=@Contract_StartDate
        'Set @EndContract=@Contract_EndDate
        'while @@FETCH_STATUS =0
        'Begin
        'FETCH NEXT FROM CurX into 	
        '			 @Contract_StartDate
        '			,@Contract_EndDate
        'if @Contract_StartDate<>Dateadd(Day,1,@EndContract)
        'Begin
        '		Set @Joindate=@Contract_StartDate
        'End
        'End
        'Close CurX
        'Deallocate CurX
        'IF @JoindateNew > @LastVacReturn AND @JoindateNew > @OpenBalanceDate
        'BEGIN
        'SET @WorkingDayFromstart = dbo.Fn_GetDays360 (@JoindateNew ,@CDate)
        'set @vactionopenBalance = 0
        'SET @START_CAL_DATE = @JoindateNew
        'END
        'IF @LastVacReturn > @JoindateNew AND @LastVacReturn > @OpenBalanceDate
        'BEGIN
        'SET @WorkingDayFromstart = dbo.Fn_GetDays360 (@LastVacReturn ,@CDate)
        'set @vactionopenBalance = @LastVacRemaining
        'SET @START_CAL_DATE = @LastVacReturn
        'END
        'IF @OpenBalanceDate > @JoindateNew AND @OpenBalanceDate > @LastVacReturn
        'BEGIN
        'SET @WorkingDayFromstart = dbo.Fn_GetDays360 (@OpenBalanceDate ,@CDate)
        'set @vactionopenBalance=dbo.GetVacation_openBalance(@EmployeeID)
        'SET @START_CAL_DATE = @OpenBalanceDate
        'END
        '-------------------
        'if @vactiotypeCalculationCode ='001' and @lastVacPaymentdate <> '1900-01-01' set @duedate =@lastVacPaymentdate
        'if @vactiotypeCalculationCode ='002' and @lastVacPaymentdate <> '1900-01-01' set @duedate =DATEADD(Day,(@Lastvactiondays-1),@lastVacPaymentdate)
        'If @OpenBalanceDate <> '1900-01-01'	 and @OpenBalanceDate >@duedate			 set @duedate = DATEADD(Day,1,@OpenBalanceDate)
        'if isnull(@fromdate,'')<>'' and @fromdate >@duedate set @duedate =@fromdate
        '--ADDED BY MOHANNAD
        '-------------------
        'SET @duedate = @START_CAL_DATE
        '-------------------
        'set @UnpaidDays=dbo.fn__GetEmployeeVacationPenalty(@duedate,@CDate,@EmployeeID)
        'set @TDays=dbo.Fn_GetDays360(@duedate,@CDate) - @UnpaidDays
        '--set @workingDaysToDueDate =dbo.Fn_GetDays360 (@joindate ,@duedate)
        'set @workingDaysToDueDate =dbo.Fn_GetDays360 (@JoindateNew ,@duedate)
        'if @TDays <= 0 return 0

        '		set @duedays=0
        '		declare @count int =0
        '		declare @CursorIndex int =0
        '		DECLARE Vac_cursor CURSOR Static FOR
        '		SELECT	
        '				isnull(ToMonth,0),
        '				isnull(DurationDays,0),
        '				isnull(RequiredWorkingMonths,0)
        '		FROM	hrs_ContractsVacations
        '		where	ContractID = @ContractId
        '		OPEN Vac_cursor
        '		FETCH NEXT FROM Vac_cursor
        '		INTO @ToMonths,@DurationDays,@RequiredWorkingMonths
        '		WHILE @@FETCH_STATUS = 0
        '		BEGIN
        '				if @ToMonths =0 set @ToMonths =99999
        '				set @CursorIndex = @CursorIndex+1
        '				if @CursorIndex =@@CURSOR_ROWS
        '					begin
        '						If @TDays > 0 set @duedays +=( @TDays / @RequiredWorkingMonths) * @DurationDays
        '					end
        '					else
        '						begin
        '							if @WorkingDayFromstart >@ToMonths
        '								begin
        '									if @workingDaysToDueDate >@ToMonths GOto NextRow									
        '									If @workingDaysToDueDate + @TDays > @ToMonths
        '									begin
        '										declare @remaingduedays int=0
        '										set @remaingduedays = @ToMonths - @workingDaysToDueDate
        '										set @duedays += (@remaingduedays / @RequiredWorkingMonths) * @DurationDays
        '										set @TDays = @TDays - @remaingduedays
        '										GOto NextRow									
        '									end
        '									If @WorkingDayFromstart = @TDays + @UnpaidDays begin
        '										set @TDays = @TDays - @RequiredWorkingMonths
        '										set @duedays = @DurationDays
        '									end
        '									Else if @WorkingDayFromstart > @ToMonths begin
        '										set @duedays += @DurationDays
        '										set @TDays = @TDays - @RequiredWorkingMonths
        '										Goto NextRow
        '									end
        '									Else
        '										Goto NextRow
        '									--end
        '								end
        '								else
        '								 set @duedays += (@TDays / @RequiredWorkingMonths) * @DurationDays set @TDays = 0
        '								end				

        '		NextRow:
        '		FETCH NEXT FROM Vac_cursor
        '		INTO @ToMonths,@DurationDays,@RequiredWorkingMonths
        '		END
        '		CLOSE Vac_cursor;
        '		DEALLOCATE Vac_cursor;
        'return  round(@duedays+@vactionopenBalance,2)
        'Return 0
        'End
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'ALTER FUNCTION [dbo].[GetVacation_BalanceByDate](@EmpCode Varchar(30),@CDate date,@fromdate date=null,@IsLessThanCurrentTodate bit=false)
        'Returns real
        'As
        'Begin
        'Declare @CurrentCDateFicalPeriodID int =(select top 1 id from sys_FiscalYearsPeriods where @CDate between FromDate and ToDate)
        'Declare @EmployeeID int = (select ID from hrs_Employees where Code = @EmpCode)
        'declare @ContractID int = dbo.hrs_GetEmpCurrentContract(@EmployeeID,@CDate)
        'Declare @AnnualVacationID As Int = IsNull((Select Top 1 ID From hrs_VacationsTypes Where IsAnnual= 1 And CancelDate Is Null),0)
        'Declare @ContractStartDate As Smalldatetime = isnull((Select Top(1) StartDate From hrs_Contracts Where EmployeeID = @EmployeeID And CancelDate Is Null And ID = @ContractID),'1900-01-01')-- (isnull(@fiscalyear,'') ='' or FiscalYearPeriodID <@fiscalyear)
        'Declare @lastVacPaymentdate As Smalldatetime = isnull((Select Top 1 PaidDate From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where EmployeeId = @EmployeeID And PrepareType = 'V' and (@IsLessThanCurrentTodate=0 or  PaidDate <=@CDate )   and (@IsLessThanCurrentTodate=0 or FiscalYearPeriodID <=@CurrentCDateFicalPeriodID )  order by PaidDate desc),'1900-01-01')
        'Declare @PaymentTrnsID As int = isnull((Select Top 1 hrs_EmployeesTransactions.id From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where EmployeeId = @EmployeeID And PrepareType = 'V' order by todate desc),0)
        'Declare @LastVacReturn As date = isnull((select MAX(ISNULL(ActualEndDate,'2050-01-01')) from hrs_EmployeesVacations where CancelDate is null and EmployeeID = @EmployeeID and VacationTypeID = @AnnualVacationID and PaymentTrnID is not null),'1900-01-01')
        'Declare @LastVacRemaining As real = (select top 1 isnull(RemainingDays,0) from hrs_EmployeesVacations where CancelDate is null and EmployeeID = @EmployeeID and VacationTypeID = @AnnualVacationID order by ID desc)
        'Declare @OpenBalanceDate As date = isnull((select GBalanceDate from hrs_EmployeeVacationOpenBalance where EmployeeID = @EmployeeID),'1900-01-01')
        'Declare @OpenBalanceDays As real = (select [Days] from hrs_EmployeeVacationOpenBalance where RegComputerID <> 1 and EmployeeID = @EmployeeID)
        'Declare @TotalWDays as real = DATEDIFF(Day,@ContractStartDate,GETDATE())
        'declare @ReqWDays As real = (select RequiredWorkingMonths from hrs_ContractsVacations where ContractID = @ContractID and @TotalWDays between ISNULL(FromMonth,0) and ISNULL(ToMonth,999999))
        'declare @DesDays As real = (select DurationDays from hrs_ContractsVacations where ContractID = @ContractID and @TotalWDays between ISNULL(FromMonth,0) and ISNULL(ToMonth,999999))
        'declare @CalcDays as real = 0
        'declare @Lastvactiondays as int =  isnull((select DATEDIFF(day,ActualStartDate,ActualEndDate ) from hrs_EmployeesVacations where CancelDate is null and EmployeeID = @EmployeeID and VacationTypeID = @AnnualVacationID and PaymentTrnID =@PaymentTrnsID),0)
        'declare @duedays real = 0
        'declare @vactiotypeCalculationCode varchar(3)=(select code from hrs_VactionTypeCalculation where id in(select VactionTypeCaculation from hrs_VacationsTypes where id=@AnnualVacationID))
        'declare @joindate date=(select joindate from hrs_employees where id=@EmployeeID )
        'declare @duedate as date=@joindate
        'declare @WorkingDayFromstart int =dbo.Fn_GetDays360 (@joindate ,@CDate)
        'declare @workingDaysToDueDate int=0
        'declare @ToMonths int
        'declare @DurationDays					int =0
        'declare @RequiredWorkingMonths		    real=0
        'declare @TDays							int=0
        'declare @UnpaidDays						int=0
        'declare @VactionOpenBalance             real=0
        '--ADDED BY MOHANNAD
        '-------------------
        'DECLARE @START_CAL_DATE			date
        'Declare @Contract_StartDate		date
        'Declare @Contract_EndDate		date
        'Declare @JoindateNew			date
        'Declare @EndContract			date
        'Declare CurX Cursor  For
        'Select StartDate,enddate from hrs_Contracts Where EmployeeID = 17
        'Open 	CurX
        'FETCH NEXT FROM CurX into 	
        '			 @Contract_StartDate
        '			,@Contract_EndDate
        'Set @JoindateNew=@Contract_StartDate
        'Set @EndContract=@Contract_EndDate
        'while @@FETCH_STATUS =0
        'Begin
        'FETCH NEXT FROM CurX into 	
        '			 @Contract_StartDate
        '			,@Contract_EndDate
        'if @Contract_StartDate<>Dateadd(Day,1,@EndContract)
        'Begin
        '		Set @Joindate=@Contract_StartDate
        'End
        'End
        'Close CurX
        'Deallocate CurX
        'IF @JoindateNew > @LastVacReturn AND @JoindateNew > @OpenBalanceDate
        'BEGIN
        'SET @WorkingDayFromstart = dbo.Fn_GetDays360 (@JoindateNew ,@CDate)
        'set @vactionopenBalance = 0
        'SET @START_CAL_DATE = @JoindateNew
        'END
        'IF @LastVacReturn > @JoindateNew AND @LastVacReturn > @OpenBalanceDate
        'BEGIN
        'SET @WorkingDayFromstart = dbo.Fn_GetDays360 (@LastVacReturn ,@CDate)
        'set @vactionopenBalance = @LastVacRemaining
        'SET @START_CAL_DATE = @LastVacReturn
        'END
        'IF @OpenBalanceDate > @JoindateNew AND @OpenBalanceDate > @LastVacReturn
        'BEGIN
        'SET @WorkingDayFromstart = dbo.Fn_GetDays360 (@OpenBalanceDate ,@CDate)
        'set @vactionopenBalance=dbo.GetVacation_openBalance(@EmployeeID)
        'SET @START_CAL_DATE = @OpenBalanceDate
        'END
        '-------------------
        'if @vactiotypeCalculationCode ='001' and @lastVacPaymentdate <> '1900-01-01' set @duedate =@lastVacPaymentdate
        'if @vactiotypeCalculationCode ='002' and @lastVacPaymentdate <> '1900-01-01' set @duedate =DATEADD(Day,(@Lastvactiondays-1),@lastVacPaymentdate)
        'If @OpenBalanceDate <> '1900-01-01'	 and @OpenBalanceDate >@duedate			 set @duedate = DATEADD(Day,1,@OpenBalanceDate)
        'if isnull(@fromdate,'')<>'' and @fromdate >@duedate set @duedate =@fromdate
        '--ADDED BY MOHANNAD
        '-------------------
        'SET @duedate = @START_CAL_DATE
        '-------------------
        'set @UnpaidDays=dbo.fn__GetEmployeeVacationPenalty(@duedate,@CDate,@EmployeeID)
        'set @TDays=dbo.Fn_GetDays360(@duedate,@CDate) - @UnpaidDays
        '--set @workingDaysToDueDate =dbo.Fn_GetDays360 (@joindate ,@duedate)
        'set @workingDaysToDueDate =dbo.Fn_GetDays360 (@JoindateNew ,@duedate)
        'if @TDays <= 0 return 0

        '		set @duedays=0
        '		declare @count int =0
        '		declare @CursorIndex int =0
        '		DECLARE Vac_cursor CURSOR Static FOR
        '		SELECT	
        '				isnull(ToMonth,0),
        '				isnull(DurationDays,0),
        '				isnull(RequiredWorkingMonths,0)
        '		FROM	hrs_ContractsVacations
        '		where	ContractID = @ContractId
        '		OPEN Vac_cursor
        '		FETCH NEXT FROM Vac_cursor
        '		INTO @ToMonths,@DurationDays,@RequiredWorkingMonths
        '		WHILE @@FETCH_STATUS = 0
        '		BEGIN
        '				if @ToMonths =0 set @ToMonths =99999
        '				set @CursorIndex = @CursorIndex+1
        '				if @CursorIndex =@@CURSOR_ROWS
        '					begin
        '						If @TDays > 0 set @duedays +=( @TDays / @RequiredWorkingMonths) * @DurationDays
        '					end
        '					else
        '						begin
        '							if @WorkingDayFromstart >@ToMonths
        '								begin
        '									if @workingDaysToDueDate >@ToMonths GOto NextRow									
        '									If @workingDaysToDueDate + @TDays > @ToMonths
        '									begin
        '										declare @remaingduedays int=0
        '										set @remaingduedays = @ToMonths - @workingDaysToDueDate
        '										set @duedays += (@remaingduedays / @RequiredWorkingMonths) * @DurationDays
        '										set @TDays = @TDays - @remaingduedays
        '										GOto NextRow									
        '									end
        '									If @WorkingDayFromstart = @TDays + @UnpaidDays begin
        '										set @TDays = @TDays - @RequiredWorkingMonths
        '										set @duedays = @DurationDays
        '									end
        '									Else if @WorkingDayFromstart > @ToMonths begin
        '										set @duedays += @DurationDays
        '										set @TDays = @TDays - @RequiredWorkingMonths
        '										Goto NextRow
        '									end
        '									Else
        '										Goto NextRow
        '									--end
        '								end
        '								else
        '								 set @duedays += (@TDays / @RequiredWorkingMonths) * @DurationDays set @TDays = 0
        '								end				

        '		NextRow:
        '		FETCH NEXT FROM Vac_cursor
        '		INTO @ToMonths,@DurationDays,@RequiredWorkingMonths
        '		END
        '		CLOSE Vac_cursor;
        '		DEALLOCATE Vac_cursor;
        'return  round(@duedays+@vactionopenBalance,2)
        'Return 0
        'End
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "


        'ALTER PROCEDURE [dbo].[shift_FindAllAttendWorkPlan](@employeeID INT)
        'AS
        '    BEGIN

        '        SELECT Att_AttendAppointmentMembers.AppointID AS AppointID, 
        '              Att_AttendShifts.Code + ' - ' +  Att_AttendShifts.EngName as EngName,  
        '               Att_AttendShifts.Code + ' - ' +  Att_AttendShifts.ArbName AS ArbName, 
        '			   Att_AttendAppointment.FromDate, 
        '               Att_AttendAppointment.ToDate
        '        FROM Att_AttendAppointmentMembers
        '             INNER JOIN Att_AttendAppointment ON Att_AttendAppointment.ID  = Att_AttendAppointmentMembers.AppointID 
        '             INNER JOIN Att_AttendShifts  ON Att_AttendShifts.id = Att_AttendAppointment.AttendaceShiftID 
        '        WHERE Att_AttendAppointmentMembers.EmployeeID = @employeeID
        '              AND Att_AttendAppointmentMembers.CancelDate IS NULL
        '        ORDER BY Att_AttendAppointment.fromdate DESC;
        '    END;
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'IF COL_LENGTH('hrs_EmployeesVacations', 'ZeroingBalance') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesVacations
        '    ADD ZeroingBalance bit
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'Drop table Att_TimeRecords
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER Proc [dbo].[hrs_FingerprintForCurrntMonth]
        '@FingerprintID int,
        '@FromDate DateTime,
        '@ToDate DateTime
        'AS
        'BEGIN
        'SET DATEFORMAT ymd
        ' DECLARE
        '	@ServerIP Varchar(50),
        '	@DatabaseName Varchar(50),
        '	@FingerprintTableName Varchar(50),
        '	@UserIdColumnName Varchar(50),
        '	@CheckInOutColumnName Varchar(50),
        '	@Query NVARCHAR (MAX),
        '	@LastDateFingerPrint DateTime,
        '	@UsersTableIdntity Varchar(50),
        '	@UserMatchColumnName Varchar(50),
        '	@UsersTableName Varchar(50)

        'SELECT 
        '	@ServerIP = FingerprintServerIP,
        '	@DatabaseName = FingerprintDatabaseName,
        '	@FingerprintTableName = FingerprintTableName,
        '	@UserIdColumnName = UserIdColumnName,
        '	@CheckInOutColumnName = CheckInOutColumnName,
        '	@UsersTableIdntity = UsersTableIdntity,
        '	@UserMatchColumnName = UserMatchColumnName,
        '	@UsersTableName = UsersTableName
        'FROM hrs_FingerprintSettings
        'WHERE ID = @FingerprintID

        'SET @ToDate =  DATEADD(DAY, 2, @ToDate) 

        'IF EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesFingerprints'
        ')
        'BEGIN
        '    SET DATEFORMAT ymd
        '	DELETE FROM hrs_EmployeesFingerprints WHERE FingerprintTime BETWEEN @FromDate AND @ToDate

        '	DECLARE @result TABLE ([FingerprintTime] datetime);

        '	INSERT INTO @result ([FingerprintTime])
        '	EXEC (N'SELECT TOP (1) [FingerprintTime] FROM [hrs_EmployeesFingerprints] WHERE [FingerprintID] = ' + @FingerprintID
        '	    + N' ORDER BY [FingerprintTime] DESC');

        '	SET @LastDateFingerPrint = (SELECT TOP (1) [FingerprintTime] FROM @result);

        '	DECLARE @ConvertFromDate varchar(20) 
        '	DECLARE @ConvertToDate varchar(20) 
        '	SELECT @ConvertFromDate  = CONVERT(VARCHAR(20), @FromDate, 120)
        '	SELECT @ConvertToDate  = CONVERT(VARCHAR(20), @ToDate, 120)

        '	--DELETE FROM hrs_EmployeesFingerprints FOR SPECIFIC PERIOD
        '	SET @Query = NULL
        '	SET @Query = N' DELETE FROM hrs_EmployeesFingerprints '
        '	SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] BETWEEN ''' + @ConvertFromDate + ''''
        '			    + N' AND ''' + @ConvertToDate + ''''


        '	-- INSERT INTO hrs_EmployeesFingerprints FOR SPECIFIC PERIOD
        '	SET @Query = NULL
        '	SET @Query = N'INSERT INTO hrs_EmployeesFingerprints ([UserID ],'
        '			   + N'[FingerprintTime], [UserCode], [FingerprintID])'
        '			   + N'SELECT fp.[' + @UserIdColumnName + N'],'
        '	           + N' fp.[' + @CheckInOutColumnName + N'],'
        '			   + N' SUBSTRING(us.[' + @UserMatchColumnName + N'],5,5), '
        '			   + CAST(@FingerprintID AS NVARCHAR(10))
        '			   + N' FROM [' + @ServerIP + '].[' + @DatabaseName + N']'
        '			   + N'.[dbo].[' + @FingerprintTableName + N'] AS fp'
        '			   + N' INNER JOIN [' + @ServerIP + '].[' + @DatabaseName + N'].[dbo].[' + @UsersTableName + N'] AS us'
        '			   + N' ON fp.[' + @UserIdColumnName + N'] = us.[' + @UsersTableIdntity + N']'


        '	SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] BETWEEN ''' + @ConvertFromDate + ''''
        '			    + N' AND ''' + @ConvertToDate + ''''

        '	EXEC (@Query)

        '	--DELETE FROM Att_TimeRecords FOR SPECIFIC PERIOD
        '	--SET @Query = NULL
        '	--SET @Query = N' DELETE FROM Att_TimeRecords '
        '	--SET @Query += N' WHERE Reg_Time BETWEEN ''' + @ConvertFromDate + ''''
        '	--		    + N' AND ''' + @ConvertToDate + ''''

        '	--EXEC (@Query)

        '	--INSERT INTO Att_TimeRecords FOR SPECIFIC PERIOD
        '	SET @Query = NULL
        '	--SET @Query = N'INSERT INTO Att_TimeRecords ([MachineCode],'
        '			 --  + N'[Reg_Time])'
        '			   --+ N'SELECT SUBSTRING(us.[' + @UserMatchColumnName + N'],5,5),'
        '	        --   + N' fp.[' + @CheckInOutColumnName + N']'
        '		--	   + N' FROM [' + @ServerIP + '].[' + @DatabaseName + N']'
        '		--	   + N'.[dbo].[' + @FingerprintTableName + N'] AS fp'
        '		--	   + N' INNER JOIN [' + @ServerIP + '].[' + @DatabaseName + N'].[dbo].[' + @UsersTableName + N'] AS us'
        '		--	   + N' ON fp.[' + @UserIdColumnName + N'] = us.[' + @UsersTableIdntity + N']'

        '--	SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] BETWEEN ''' + @ConvertFromDate + ''''
        '--			    + N' AND ''' + @ConvertToDate + ''''

        '--		EXEC (@Query)

        '    END
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'ALTER FUNCTION [dbo].[GetVacation_BalanceByDate](@EmpCode Varchar(30),@CDate date,@fromdate date=null,@IsLessThanCurrentTodate bit=false)
        'Returns real
        'As
        'Begin
        'Declare @CurrentCDateFicalPeriodID int =(select top 1 id from sys_FiscalYearsPeriods where @CDate between FromDate and ToDate)
        'Declare @EmployeeID int = (select ID from hrs_Employees where Code = @EmpCode)
        'declare @ContractID int = dbo.hrs_GetEmpCurrentContract(@EmployeeID,@CDate)
        'Declare @AnnualVacationID As Int = IsNull((Select Top 1 ID From hrs_VacationsTypes Where IsAnnual= 1 And CancelDate Is Null),0)
        'Declare @ContractStartDate As Smalldatetime = isnull((Select Top(1) StartDate From hrs_Contracts Where EmployeeID = @EmployeeID And CancelDate Is Null And ID = @ContractID),'1900-01-01')-- (isnull(@fiscalyear,'') ='' or FiscalYearPeriodID <@fiscalyear)
        'Declare @lastVacPaymentdate As Smalldatetime = isnull((Select Top 1 PaidDate From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where EmployeeId = @EmployeeID And PrepareType = 'V' and (@IsLessThanCurrentTodate=0 or  PaidDate <=@CDate )   and (@IsLessThanCurrentTodate=0 or FiscalYearPeriodID <=@CurrentCDateFicalPeriodID )  order by PaidDate desc),'1900-01-01')
        'Declare @PaymentTrnsID As int = isnull((Select Top 1 hrs_EmployeesTransactions.id From hrs_EmployeesTransactions Inner join Sys_FiscalYearsPeriods On Hrs_EmployeesTransactions.FiscalYearPeriodID = Sys_FiscalYearsPeriods.Id Where EmployeeId = @EmployeeID And PrepareType = 'V' order by todate desc),0)
        'Declare @LastVacReturn As date = isnull((select MAX(ISNULL(ActualEndDate,'2050-01-01')) from hrs_EmployeesVacations where CancelDate is null and EmployeeID = @EmployeeID and VacationTypeID = @AnnualVacationID and PaymentTrnID is not null),'1900-01-01')
        'Declare @LastVacRemaining As real = (select top 1 isnull(RemainingDays,0) from hrs_EmployeesVacations where CancelDate is null and EmployeeID = @EmployeeID and VacationTypeID = @AnnualVacationID order by ID desc)
        'Declare @OpenBalanceDate As date = isnull((select GBalanceDate from hrs_EmployeeVacationOpenBalance where EmployeeID = @EmployeeID),'1900-01-01')
        'Declare @OpenBalanceDays As real = (select [Days] from hrs_EmployeeVacationOpenBalance where RegComputerID <> 1 and EmployeeID = @EmployeeID)
        'Declare @TotalWDays as real = DATEDIFF(Day,@ContractStartDate,GETDATE())
        'declare @ReqWDays As real = (select RequiredWorkingMonths from hrs_ContractsVacations where ContractID = @ContractID and @TotalWDays between ISNULL(FromMonth,0) and ISNULL(ToMonth,999999))
        'declare @DesDays As real = (select DurationDays from hrs_ContractsVacations where ContractID = @ContractID and @TotalWDays between ISNULL(FromMonth,0) and ISNULL(ToMonth,999999))
        'declare @CalcDays as real = 0
        'declare @Lastvactiondays as int =  isnull((select DATEDIFF(day,ActualStartDate,ActualEndDate ) from hrs_EmployeesVacations where CancelDate is null and EmployeeID = @EmployeeID and VacationTypeID = @AnnualVacationID and PaymentTrnID =@PaymentTrnsID),0)
        'declare @duedays real = 0
        'declare @vactiotypeCalculationCode varchar(3)=(select code from hrs_VactionTypeCalculation where id in(select VactionTypeCaculation from hrs_VacationsTypes where id=@AnnualVacationID))
        'declare @joindate date=(select joindate from hrs_employees where id=@EmployeeID )
        'declare @duedate as date=@joindate
        'declare @WorkingDayFromstart int =dbo.Fn_GetDays360 (@joindate ,@CDate)
        'declare @workingDaysToDueDate int=0
        'declare @ToMonths int
        'declare @DurationDays					int =0
        'declare @RequiredWorkingMonths		    real=0
        'declare @TDays							int=0
        'declare @UnpaidDays						int=0
        'declare @VactionOpenBalance             real=0
        '--ADDED BY MOHANNAD
        '-------------------
        'DECLARE @START_CAL_DATE			date
        'Declare @Contract_StartDate		date
        'Declare @Contract_EndDate		date
        'Declare @JoindateNew			date
        'Declare @EndContract			date
        'Declare CurX Cursor  For
        'Select StartDate,enddate from hrs_Contracts Where EmployeeID = @EmployeeID
        'Open 	CurX
        'FETCH NEXT FROM CurX into 	
        '			 @Contract_StartDate
        '			,@Contract_EndDate
        'Set @JoindateNew=@Contract_StartDate
        'Set @EndContract=@Contract_EndDate
        'while @@FETCH_STATUS =0
        'Begin
        'FETCH NEXT FROM CurX into 	
        '			 @Contract_StartDate
        '			,@Contract_EndDate
        'if @Contract_StartDate<>Dateadd(Day,1,@EndContract)
        'Begin
        '		Set @Joindate=@Contract_StartDate
        'End
        'End
        'Close CurX
        'Deallocate CurX
        'IF @JoindateNew > @LastVacReturn AND @JoindateNew > @OpenBalanceDate
        'BEGIN
        'SET @WorkingDayFromstart = dbo.Fn_GetDays360 (@JoindateNew ,@CDate)
        'set @vactionopenBalance = 0
        'SET @START_CAL_DATE = @JoindateNew
        'END
        'IF @LastVacReturn > @JoindateNew AND @LastVacReturn > @OpenBalanceDate
        'BEGIN
        'SET @WorkingDayFromstart = dbo.Fn_GetDays360 (@LastVacReturn ,@CDate)
        'set @vactionopenBalance = @LastVacRemaining
        'SET @START_CAL_DATE = @LastVacReturn
        'END
        'IF @OpenBalanceDate > @JoindateNew AND @OpenBalanceDate > @LastVacReturn
        'BEGIN
        'SET @WorkingDayFromstart = dbo.Fn_GetDays360 (@OpenBalanceDate ,@CDate)
        'set @vactionopenBalance=dbo.GetVacation_openBalance(@EmployeeID)
        'SET @START_CAL_DATE = @OpenBalanceDate
        'END
        '-------------------
        'if @vactiotypeCalculationCode ='001' and @lastVacPaymentdate <> '1900-01-01' set @duedate =@lastVacPaymentdate
        'if @vactiotypeCalculationCode ='002' and @lastVacPaymentdate <> '1900-01-01' set @duedate =DATEADD(Day,(@Lastvactiondays-1),@lastVacPaymentdate)
        'If @OpenBalanceDate <> '1900-01-01'	 and @OpenBalanceDate >@duedate			 set @duedate = DATEADD(Day,1,@OpenBalanceDate)
        'if isnull(@fromdate,'')<>'' and @fromdate >@duedate set @duedate =@fromdate
        '--ADDED BY MOHANNAD
        '-------------------
        'SET @duedate = @START_CAL_DATE
        '-------------------
        'set @UnpaidDays=dbo.fn__GetEmployeeVacationPenalty(@duedate,@CDate,@EmployeeID)
        'set @TDays=dbo.Fn_GetDays360(@duedate,@CDate) - @UnpaidDays
        '--set @workingDaysToDueDate =dbo.Fn_GetDays360 (@joindate ,@duedate)
        'set @workingDaysToDueDate =dbo.Fn_GetDays360 (@JoindateNew ,@duedate)
        'if @TDays <= 0 return 0

        '		set @duedays=0
        '		declare @count int =0
        '		declare @CursorIndex int =0
        '		DECLARE Vac_cursor CURSOR Static FOR
        '		SELECT	
        '				isnull(ToMonth,0),
        '				isnull(DurationDays,0),
        '				isnull(RequiredWorkingMonths,0)
        '		FROM	hrs_ContractsVacations
        '		where	ContractID = @ContractId
        '		OPEN Vac_cursor
        '		FETCH NEXT FROM Vac_cursor
        '		INTO @ToMonths,@DurationDays,@RequiredWorkingMonths
        '		WHILE @@FETCH_STATUS = 0
        '		BEGIN
        '				if @ToMonths =0 set @ToMonths =99999
        '				set @CursorIndex = @CursorIndex+1
        '				if @CursorIndex =@@CURSOR_ROWS
        '					begin
        '						If @TDays > 0 set @duedays +=( @TDays / @RequiredWorkingMonths) * @DurationDays
        '					end
        '					else
        '						begin
        '							if @WorkingDayFromstart >@ToMonths
        '								begin
        '									if @workingDaysToDueDate >@ToMonths GOto NextRow									
        '									If @workingDaysToDueDate + @TDays > @ToMonths
        '									begin
        '										declare @remaingduedays int=0
        '										set @remaingduedays = @ToMonths - @workingDaysToDueDate
        '										set @duedays += (@remaingduedays / @RequiredWorkingMonths) * @DurationDays
        '										set @TDays = @TDays - @remaingduedays
        '										GOto NextRow									
        '									end
        '									If @WorkingDayFromstart = @TDays + @UnpaidDays begin
        '										set @TDays = @TDays - @RequiredWorkingMonths
        '										set @duedays = @DurationDays
        '									end
        '									Else if @WorkingDayFromstart > @ToMonths begin
        '										set @duedays += @DurationDays
        '										set @TDays = @TDays - @RequiredWorkingMonths
        '										Goto NextRow
        '									end
        '									Else
        '										Goto NextRow
        '									--end
        '								end
        '								else
        '								 set @duedays += (@TDays / @RequiredWorkingMonths) * @DurationDays set @TDays = 0
        '								end				

        '		NextRow:
        '		FETCH NEXT FROM Vac_cursor
        '		INTO @ToMonths,@DurationDays,@RequiredWorkingMonths
        '		END
        '		CLOSE Vac_cursor;
        '		DEALLOCATE Vac_cursor;
        'return  round(@duedays+@vactionopenBalance,2)
        'Return 0
        'End
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER procedure [dbo].[GetEosDetails] --exec GetEosDetails 160

        '@Employeeid as int
        'as 
        'begin 


        'SELECT			
        '				TT.ID						AS	TransactionTypeID,
        '				ETD.NumericValue			AS	Amount ,
        '				ET.FinancialWorkingUnits ,
        '				ET.PrepareType				AS	PrepareType,
        '				TT.ArbName					AS	TransactionArbname,
        '				TT.Sign						AS	Sign,
        '				ETD.TextValue				AS Description
        'FROM			hrs_EmployeesTransactionsDetails  ETD
        'INNER JOIN		hrs_EmployeesTransactionsProjects ETP		on ETP.id=ETD.EmpTransProjID
        'INNER JOIN		hrs_EmployeesTransactions ET				on ET.id=ETP.EmployeeTransactionID
        'INNER JOIN		hrs_employees EMP							on EMP.id=ET.EmployeeID
        'INNER JOIN		hrs_TransactionsTypes TT					on TT.id=ETD.TransactionTypeID

        'where EMP.id=@Employeeid
        'and ET.PrepareType in('E','EN','EV','EL','ET')
        '--AND ET.ID = (SELECT TOP 1 ID FROM hrs_EmployeesTransactions WHERE EmployeeID = @Employeeid AND PrepareType in('E','EN','EV','EL','ET') ORDER BY ID DESC)

        'end
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        '-------Rabie 28-12-2022---------------------------------------
        'ALTER   PROCEDURE [dbo].[GetAllEmployeePreviousVacationsForPeriod] 
        '	-- Add the parameters for the stored procedure here

        '	@EmployeeID AS INT
        '	, @PeriodID AS INT
        'AS
        'BEGIN
        '	SET NOCOUNT ON;
        '	Declare @PrepareDay as int =(select distinct PrepareDay from sys_Branches)
        '	DECLARE @SD AS Date = (SELECT FromDate FROM sys_FiscalYearsPeriods WHERE ID = @PeriodID)
        '	DECLARE @ED AS Date = (SELECT ToDate FROM sys_FiscalYearsPeriods WHERE ID = @PeriodID)
        '	Declare @Previousmonth as int =month(@SD)-1
        '	if  @Previousmonth=0
        '	begin
        '	set @Previousmonth=1
        '	end
        '	Declare @year  int 
        '	if @Previousmonth=1
        'begin
        'set @year=year(@SD)-1
        'end
        'else
        'begin
        'set @year=year(@SD)
        'end
        'set @SD=DATEFROMPARTS( @year,@Previousmonth,(@PrepareDay+1) )
        '	SELECT 
        '		OV.ActualStartDate AS VS, OV.ActualEndDate AS VE, OV.ConsumDays AS VD, VT.ID as VTID, VT.EngName AS VTYPE, @SD AS FSD,@ED AS FED
        '	FROM
        '		hrs_EmployeesVacations OV
        '		INNER JOIN hrs_VacationsTypes VT ON OV.VacationTypeID = VT.ID
        '	WHERE
        '		OV.EmployeeID = @EmployeeID 
        '		AND OV.CancelDate IS NULL
        '		AND (OV.ActualStartDate BETWEEN @SD AND @ED OR OV.ActualEndDate BETWEEN @SD AND @ED)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        '---Rabie 4-1-2023

        'IF Not EXISTS(SELECT 1 FROM sys.columns 
        '          WHERE Name = N'HasFlexableFingerPrint'
        '          AND Object_ID = Object_ID(N'dbo.hrs_EmployeesClasses'))
        'BEGIN
        '  alter table  hrs_EmployeesClasses add HasFlexableFingerPrint bit null
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'CREATE TABLE [dbo].[hrs_HmsCommissionTransfer](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[FiscalPeridID] [int] NOT NULL,
        '	[CommissionCatageryID] [int] NOT NULL,
        '	[EmployeeId] [int] NOT NULL,
        '	[Amount] [float] NULL,
        '	[Dedcution1] [float] NULL,
        '	[Deduction2] [float] NULL,
        '	[NetAmount] [float] NULL,
        '	[CommissionPc] [float] NULL,
        '	[DueAmount] [float] NULL,
        ' CONSTRAINT [PK_hrs_HmsCommissionTransfer] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
        ' CONSTRAINT [IX_hrs_HmsCommissionTransfer] UNIQUE NONCLUSTERED 
        '(
        '	[EmployeeId] ASC,
        '	[FiscalPeridID] ASC,
        '	[CommissionCatageryID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE [dbo].[hrs_HmsCommissionTransfer]  WITH CHECK ADD  CONSTRAINT [FK_hrs_HmsCommissionTransfer_hrs_Employees] FOREIGN KEY([EmployeeId])
        'REFERENCES [dbo].[hrs_Employees] ([ID])
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE [dbo].[hrs_HmsCommissionTransfer] CHECK CONSTRAINT [FK_hrs_HmsCommissionTransfer_hrs_Employees]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE [dbo].[hrs_HmsCommissionTransfer]  WITH CHECK ADD  CONSTRAINT [FK_hrs_HmsCommissionTransfer_hrs_HmsCommissionCategories] FOREIGN KEY([CommissionCatageryID])
        'REFERENCES [dbo].[hrs_HmsCommissionCategories] ([ID])
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE [dbo].[hrs_HmsCommissionTransfer] CHECK CONSTRAINT [FK_hrs_HmsCommissionTransfer_hrs_HmsCommissionCategories]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE [dbo].[hrs_HmsCommissionTransfer]  WITH CHECK ADD  CONSTRAINT [FK_hrs_HmsCommissionTransfer_sys_FiscalYearsPeriods] FOREIGN KEY([FiscalPeridID])
        'REFERENCES [dbo].[sys_FiscalYearsPeriods] ([ID])
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE [dbo].[hrs_HmsCommissionTransfer] CHECK CONSTRAINT [FK_hrs_HmsCommissionTransfer_sys_FiscalYearsPeriods]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'CREATE TABLE [dbo].[hrs_HmsCommissionCategories](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NULL,
        '	[Arbname] [varchar](300) NOT NULL,
        '	[Engname] [varchar](300) NULL,
        '	[IsLineCommission] [bit] NULL,
        '	[TotalCommision] [float] NULL,
        '	[DedcutionPc] [float] NULL,
        '	[SalaryTransactionID] [int] NOT NULL,
        '	[Remarks] [varchar](max) NULL,
        '	[RegUserID] [int] NOT NULL,
        '	[RegDate] [smalldatetime] NOT NULL,
        '	[CancelDate] [smalldatetime] NULL,
        ' CONSTRAINT [PK_hrs_HmsCommissionCategories] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
        ' CONSTRAINT [IX_hrs_HmsCommissionCategories] UNIQUE NONCLUSTERED 
        '(
        '	[Code] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE [dbo].[hrs_HmsCommissionCategories] ADD  CONSTRAINT [DF_hrs_HmsPercentageCategories_RegDate]  DEFAULT (getdate()) FOR [RegDate]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'alter table  hrs_Employees add Hasflexiblesalarydist bit  null
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'alter table hrs_SalartDistExec add BasicAmount     decimal(18, 3) null
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'alter table hrs_SalartDistExec add ExtraAmount     decimal(18, 3) null
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'alter table hrs_SalartDistExec add TransactionCode decimal(18, 3) null
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'alter table [dbo].[sys_Departments] add CostCenterCode varchar(50)
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'UPDATE    hrs_EmployeeExtraItems SET ProjectID = N'101'
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF Not EXISTS(SELECT 1 FROM sys.columns 
        '          WHERE Name = N'IsDGSignature'
        '          AND Object_ID = Object_ID(N'dbo.sys_ObjectsAttachments'))
        'BEGIN

        'alter table sys_ObjectsAttachments add IsDGSignature bit null
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'alter table [dbo].[hrs_EmployeesExcuses] add ExcuseCalcType varchar(50)
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'create FUNCTION  PeriodDateRange ( @minDate_Str NVARCHAR(30), @maxDate_Str NVARCHAR(30))

        'RETURNS  @Result TABLE(Date NVARCHAR(30) NOT NULL, DateNameEnglish NVARCHAR(30) NOT NULL,DateNameArabic NVARCHAR(30) NOT NULL)

        'AS

        'begin

        '    DECLARE @minDate DATETIME, @maxDate DATETIME
        '    SET @minDate = CONVERT(Datetime, @minDate_Str,103)
        '    SET @maxDate = CONVERT(Datetime, @maxDate_Str,103)





        '    WHILE @maxDate >= @minDate
        '    BEGIN

        '        INSERT INTO @Result(Date, DateNameEnglish,DateNameArabic )
        '        SELECT CONVERT(NVARCHAR(10),@minDate,103), CONVERT(NVARCHAR(30),DATENAME(dw,@minDate)),case when  DATENAME(dw,@minDate)= 'Saturday' then 'السبت' when DATENAME(dw,@minDate)= 'Sunday' then  'الأحد'  when DATENAME(dw,@minDate)= 'Monday' then 'الأثنين' when DATENAME(dw,@minDate)= 'Tuesday' then 'الثلاثاء' when DATENAME(dw,@minDate)= 'Wednesday' then 'الاربعاء' when DATENAME(dw,@minDate)= 'Thursday' then 'الخميس' when DATENAME(dw,@minDate)= 'Friday' then 'الجمعة' end  xx
        '		  SET @minDate = (SELECT DATEADD(dd,1,@minDate))
        '    END




        '    return

        'end 

        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER PROCEDURE [dbo].[shift_FindAllAttendWorkPlan](@employeeID INT)
        'AS
        '    BEGIN

        '        SELECT Att_AttendAppointmentMembers.AppointID AS AppointID, 
        '              Att_AttendShifts.Code + ' - ' +  Att_AttendShifts.EngName as EngName,  
        '               Att_AttendShifts.Code + ' - ' +  Att_AttendShifts.ArbName AS ArbName, 
        '			   Att_AttendAppointment.FromDate, 
        '               Att_AttendAppointment.ToDate
        '        FROM Att_AttendAppointmentMembers
        '             INNER JOIN Att_AttendAppointment ON Att_AttendAppointment.ID  = Att_AttendAppointmentMembers.AppointID 
        '             INNER JOIN Att_AttendShifts  ON Att_AttendShifts.id = Att_AttendAppointment.AttendaceShiftID 
        '        WHERE Att_AttendAppointmentMembers.EmployeeID = @employeeID
        '              AND Att_AttendAppointmentMembers.CancelDate IS NULL
        '        ORDER BY Att_AttendAppointment.RegDate DESC;
        '    END;
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'CREATE TABLE [dbo].[Hrs_PaymentTypes](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NULL,
        '	[EngName] [varchar](50) NULL,
        '	[ArbName] [varchar](50) NULL,
        '	[CancelDate] [date] NULL,
        ' CONSTRAINT [PK_Hrs_PaymentTypes] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_Employees', 'paymenttype') IS NULL
        'BEGIN
        '    ALTER TABLE hrs_Employees
        '    ADD paymenttype int
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_VacationsTypes', 'HasPayment') IS NULL
        'BEGIN
        '    ALTER TABLE hrs_VacationsTypes
        '    ADD HasPayment bit
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('sys_Companies', 'VacationFromPrepareDay') IS NULL
        'BEGIN
        '    ALTER TABLE sys_Companies
        '    ADD VacationFromPrepareDay bit
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'create or ALTER    PROCEDURE [dbo].[Att_GetCustomAttandace]
        ' @EmployeeID     AS INT, 
        ' @Attandancedate AS DATE
        'AS
        'SET DATEFIRST 6

        'IF((SELECT COUNT(*) FROM hrs_EmployeesClassesWeeklyCustomize WHERE EmployeeID = @EmployeeID
        '	AND @Attandancedate BETWEEN BeginDate AND EndDate) > 0)
        'BEGIN
        'SET DATEFORMAT dmy
        '     SELECT AttShD. DayNo                                AS DayNo,                             
        '            AttShD.TimeIn1                               AS TimeIn, 
        '            AttShD.TimeOut1                              AS TimeOut, 
        '            AttShD.TimeIn2                               AS TimeIn2nd, 
        '            AttShD.TimeOut2                              AS TimeOut2nd, 
        '            AttShD.IsDayOff                              AS IsDayOff
        '			--AttSh.FirstShiftTimeInFingerprintStart       AS FirstShiftTimeInFingerprintStart,
        '			--AttSh.FirstShiftEntryTimeInClose             AS FirstShiftEntryTimeInClose,
        '			--AttSh.FirstShiftTimeOutFingerprintClose      AS FirstShiftTimeOutFingerprintClose,
        '			--AttSh.SecondShiftTimeInFingerprintStart      AS SecondShiftTimeInFingerprintStart,
        '			--AttSh.SecondShiftEntryTimeInClose            AS SecondShiftEntryTimeInClose,
        '			--AttSh.SecondShiftTimeOutFingerprintClose     AS SecondShiftTimeOutFingerprintClose
        '     FROM hrs_EmployeesClassesWeeklyCustomizeDays AttShD
        '          INNER JOIN hrs_EmployeesClassesWeeklyCustomize AttSh ON Attsh.id = AttShD.CustomizeID

        '     WHERE AttSh.EmployeeID = @EmployeeID
        '           AND @Attandancedate BETWEEN AttSh.BeginDate AND AttSh.EndDate
        '		   AND  DATEPART(dw, @Attandancedate) = DayNo
        '		   AND AttSh.CancelDate IS NULL
        '     ORDER BY AttSh.RegDate DESC
        'END
        'ELSE IF((SELECT COUNT(*) FROM hrs_EmployeesClassesWeeklyCustomizeDays AttShD
        '          INNER JOIN hrs_EmployeesClassesWeeklyCustomize AttSh ON Attsh.id = Attshd.CustomizeID
        '          INNER JOIN Att_AttendAppointment AttApp ON AttApp.AttendaceShiftID = attsh.EmployeeClassID
        '          INNER JOIN Att_AttendAppointmentMembers AttAppM ON AttappM.AppointID = AttApp.ID
        '     WHERE AttAppM.EmployeeID = @EmployeeID
        '           AND @Attandancedate BETWEEN AttApp.FromDate AND AttApp.ToDate
        '		   AND  DATEPART(dw, @Attandancedate) = DayNo
        '		   AND AttAppM.CancelDate IS NULL) > 0)
        'BEGIN
        ' SET DATEFORMAT dmy
        '     SELECT AttShD. DayNo                                AS DayNo,                             
        '            AttShD.TimeIn1                               AS TimeIn, 
        '            AttShD.TimeOut1                              AS TimeOut, 
        '            AttShD.TimeIn2                               AS TimeIn2nd, 
        '            AttShD.TimeOut2                              AS TimeOut2nd, 
        '            AttShD.IsDayOff                              AS IsDayOff,
        '			AttShD.FirstShiftTimeInFingerprintStart       AS FirstShiftTimeInFingerprintStart,
        '			AttShD.FirstShiftEntryTimeInClose             AS FirstShiftEntryTimeInClose,
        '			AttShD.FirstShiftTimeOutFingerprintClose      AS FirstShiftTimeOutFingerprintClose,
        '			AttShD.SecondShiftTimeInFingerprintStart      AS SecondShiftTimeInFingerprintStart,
        '			AttShD.SecondShiftEntryTimeInClose            AS SecondShiftEntryTimeInClose,
        '			AttShD.SecondShiftTimeOutFingerprintClose     AS SecondShiftTimeOutFingerprintClose
        '     FROM hrs_EmployeesClassesWeeklyCustomizeDays AttShD
        '          INNER JOIN hrs_EmployeesClassesWeeklyCustomize AttSh ON Attsh.id = Attshd.CustomizeID
        '          INNER JOIN Att_AttendAppointment AttApp ON AttApp.AttendaceShiftID = attsh.EmployeeClassID
        '          INNER JOIN Att_AttendAppointmentMembers AttAppM ON AttappM.AppointID = AttApp.ID
        '     WHERE AttAppM.EmployeeID = @EmployeeID
        '           AND @Attandancedate BETWEEN AttApp.FromDate AND AttApp.ToDate
        '		   AND  DATEPART(dw, @Attandancedate) = DayNo
        '		   AND AttAppM.CancelDate IS NULL
        '     ORDER BY AttApp.RegDate DESC
        'END
        'ELSE 
        'BEGIN
        'SET DATEFORMAT dmy
        '     SELECT AttShD. DayNo                                AS DayNo,                             
        '            AttShD.TimeIn                                AS TimeIn, 
        '            AttShD.TimeOut                               AS TimeOut, 
        '            AttShD.TimeIn2nd                             AS TimeIn2nd, 
        '            AttShD.TimeOut2nd                            AS TimeOut2nd, 
        '            AttShD.IsDayOff                              AS IsDayOff,
        '			AttSh.FirstShiftTimeInFingerprintStart       AS FirstShiftTimeInFingerprintStart,
        '			AttSh.FirstShiftEntryTimeInClose             AS FirstShiftEntryTimeInClose,
        '			AttSh.FirstShiftTimeOutFingerprintClose      AS FirstShiftTimeOutFingerprintClose,
        '			AttSh.SecondShiftTimeInFingerprintStart      AS SecondShiftTimeInFingerprintStart,
        '			AttSh.SecondShiftEntryTimeInClose            AS SecondShiftEntryTimeInClose,
        '			AttSh.SecondShiftTimeOutFingerprintClose     AS SecondShiftTimeOutFingerprintClose
        '     FROM Att_AttendShiftDays AttShD
        '          INNER JOIN Att_AttendShifts AttSh ON Attsh.id = Attshd.AttendShiftID
        '          INNER JOIN Att_AttendAppointment AttApp ON AttApp.AttendaceShiftID = attsh.id
        '          INNER JOIN Att_AttendAppointmentMembers AttAppM ON AttappM.AppointID = AttApp.ID
        '     WHERE AttAppM.EmployeeID = @EmployeeID
        '           AND @Attandancedate BETWEEN AttApp.FromDate AND AttApp.ToDate
        '		   AND  DATEPART(dw, @Attandancedate) = DayNo
        '		   AND AttAppM.CancelDate IS NULL
        '     ORDER BY AttApp.RegDate DESC
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_Employees', 'WorkE_Mail') IS NULL
        'BEGIN
        '    ALTER TABLE hrs_Employees
        '    add WorkE_Mail varchar(255) null
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_SalaryProductionFiles', 'IsTextFile') IS NULL
        'BEGIN
        '   alter table hrs_SalaryProductionFiles
        'add IsTextFile bit null
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_Projects', 'DepartmentID') IS NULL
        'BEGIN
        '   alter table hrs_Projects
        'add DepartmentID int null
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'CREATE TABLE [dbo].[Hrs_NewEmployee](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[SSNO] [varchar](50) NOT NULL,
        '	[FirstNameEnglish] [varchar](50) NULL,
        '	[FatherNameEnglish] [varchar](50) NULL,
        '	[GrandfatherNameEnglish] [varchar](50) NULL,
        '	[FamilyNameEnglish] [varchar](50) NULL,
        '	[FirstNameArabic] [varchar](50) NULL,
        '	[FatherNameArabic] [varchar](50) NULL,
        '	[GrandfatherNameArabic] [varchar](50) NULL,
        '	[FamilyNameArabic] [varchar](50) NULL,
        '	[BirthDate] [date] NULL,
        '	[BirthCityID] [int] NULL,
        '	[NationalityID] [int] NULL,
        '	[ReligionID] [int] NULL,
        '	[GenderID] [int] NULL,
        '	[MaritalStatusID] [int] NULL,
        '	[BloodGroupID] [int] NULL,
        '	[PersonalEmail] [varchar](50) NULL,
        '	[Mobile] [varchar](50) NULL,
        '	[PassportNo] [varchar](50) NULL,
        '	[BankID] [int] NULL,
        '	[IBANNo] [nchar](10) NULL,
        '	[LastEducationCertificateID] [int] NULL,
        '	[GraduationDate] [date] NULL,
        '	[Remarks] [varchar](max) NULL,
        '	[RegisterDate] [datetime] NULL,
        '	[IsTransfered] [bit] NULL,
        '	[TransfereDate] [datetime] NULL,
        ' CONSTRAINT [PK_Hrs_NewEmployee] PRIMARY KEY CLUSTERED 
        '(
        '	[SSNO] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'CREATE TABLE [dbo].[hrs_EmployeesDecisions](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[EmpId] [nchar](10) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[MaritalStatusID] [int] NULL,
        '	[BankID] [int] NULL,
        '	[NationalityID] [int] NULL,
        '	[BankAccountNumber] [varchar](100) NULL,
        '	[DepartmentID] [int] NULL,
        '	[CompanyID] [int] NOT NULL,
        '	[Remarks] [varchar](2048) NULL,
        '	[BranchID] [int] NULL,
        '	[SponsorID] [int] NULL,
        '	[E_Mail] [varchar](255) NULL,
        '	[Phone] [varchar](100) NULL,
        '	[Mobile] [varchar](100) NULL,
        '	[ManagerID] [int] NULL,
        '	[SectorID] [int] NULL,
        '	[PassPortNo] [nvarchar](20) NULL,
        '	[Cost1] [int] NULL,
        '	[Cost2] [int] NULL,
        '	[Cost3] [int] NULL,
        '	[Cost4] [int] NULL,
        '	[LocationID] [int] NULL,
        '	[WorkE_Mail] [varchar](255) NULL,
        '	[ContractType] [int] NULL,
        '	[Professions] [int] NULL,
        '	[Position] [int] NULL,
        '	[EmployeeClass] [int] NULL,
        '	[LastEducations] [int] NULL,
        '	[GraduationDate] [date] NULL,
        '	[PreviousMaritalStatusID] [int] NULL,
        '	[PreviousBankID] [int] NULL,
        '	[PreviousNationalityID] [int] NULL,
        '	[PreviousBankAccountNumber] [varchar](100) NULL,
        '	[PreviousDepartmentID] [int] NULL,
        '	[PreviousCompanyID] [int] NULL,
        '	[PreviousRemarks] [varchar](2048) NULL,
        '	[PreviousBranchID] [int] NULL,
        '	[PreviousSponsorID] [int] NULL,
        '	[PreviousE_Mail] [varchar](255) NULL,
        '	[PreviousPhone] [varchar](100) NULL,
        '	[PreviousMobile] [varchar](100) NULL,
        '	[PreviousManagerID] [int] NULL,
        '	[PreviousSectorID] [int] NULL,
        '	[PreviousPassPortNo] [nvarchar](20) NULL,
        '	[PreviousCost1] [int] NULL,
        '	[PreviousCost2] [int] NULL,
        '	[PreviousCost3] [int] NULL,
        '	[PreviousCost4] [int] NULL,
        '	[PreviousLocationID] [int] NULL,
        '	[PreviousWorkE_Mail] [varchar](255) NULL,
        '	[PreviousContractType] [int] NULL,
        '	[PreviousProfessions] [int] NULL,
        '	[PreviousPosition] [int] NULL,
        '	[PreviousEmployeeClass] [int] NULL,
        '	[PreviousLastEducations] [int] NULL,
        '	[PreviousGraduationDate] [date] NULL,
        ' CONSTRAINT [PK_hrs_EmployeesDecisions] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_Positions', 'EmployeesNo') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_Positions
        '    ADD EmployeesNo int
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_Positions', 'ApplyValidation') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_Positions
        '    ADD ApplyValidation bit
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_Positions', 'PositionBudget') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_Positions
        '    ADD PositionBudget varchar(5)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_VacationsTypes', 'RoundAnnualVacBalance') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_VacationsTypes
        '    ADD RoundAnnualVacBalance bit
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE dbo.hrs_EmployeesDecisions ADD
        '	RegUserID int NULL,
        '	RegComputerID int NULL,
        '	RegDate smalldatetime NOT NULL CONSTRAINT DF_hrs_EmployeesDecisions_RegDate DEFAULT (getdate())
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_VacationsTypes', 'Religion') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_VacationsTypes
        '    ADD Religion varchar(10)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('Hrs_NewEmployee', 'SSNOIssueDate') IS NULL  
        'BEGIN
        '    ALTER TABLE Hrs_NewEmployee
        '    ADD SSNOIssueDate varchar(50)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('Hrs_NewEmployee', 'SSNOExpireDate') IS NULL  
        'BEGIN
        '    ALTER TABLE Hrs_NewEmployee
        '    ADD SSNOExpireDate varchar(50)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('Hrs_NewEmployee', 'PassportIssueDate') IS NULL  
        'BEGIN
        '    ALTER TABLE Hrs_NewEmployee
        '    ADD PassportIssueDate varchar(50)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('Hrs_NewEmployee', 'PassportExpireDate') IS NULL  
        'BEGIN
        '    ALTER TABLE Hrs_NewEmployee
        '    ADD PassportExpireDate varchar(50)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('Hrs_NewEmployee', 'ProfissionID') IS NULL  
        'BEGIN
        '    ALTER TABLE Hrs_NewEmployee
        '    ADD ProfissionID int
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('Hrs_NewEmployee', 'AddressAsPerContract') IS NULL  
        'BEGIN
        '    ALTER TABLE Hrs_NewEmployee
        '    ADD AddressAsPerContract varchar(500)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'IF COL_LENGTH('hrs_EmployeesDecisions', 'RegUserID') IS NULL  
        'BEGIN
        '    ALTER TABLE hrs_EmployeesDecisions
        '    ADD RegUserID varchar(50)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER Proc [dbo].[hrs_FingerprintForCurrntMonth]
        '@FingerprintID int,
        '@FromDate DateTime,
        '@ToDate DateTime
        'AS
        'BEGIN
        'SET DATEFORMAT ymd
        ' DECLARE
        '	@ServerIP Varchar(50),
        '	@DatabaseName Varchar(50),
        '	@FingerprintTableName Varchar(50),
        '	@UserIdColumnName Varchar(50),
        '	@CheckInOutColumnName Varchar(50),
        '	@Query NVARCHAR (MAX),
        '	@LastDateFingerPrint DateTime,
        '	@UsersTableIdntity Varchar(50),
        '	@UserMatchColumnName Varchar(50),
        '	@UsersTableName Varchar(50)

        'SELECT 
        '	@ServerIP = FingerprintServerIP,
        '	@DatabaseName = FingerprintDatabaseName,
        '	@FingerprintTableName = FingerprintTableName,
        '	@UserIdColumnName = UserIdColumnName,
        '	@CheckInOutColumnName = CheckInOutColumnName,
        '	@UsersTableIdntity = UsersTableIdntity,
        '	@UserMatchColumnName = UserMatchColumnName,
        '	@UsersTableName = UsersTableName
        'FROM hrs_FingerprintSettings
        'WHERE ID = @FingerprintID

        'SET @ToDate =  DATEADD(DAY, 2, @ToDate) 

        'IF EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'hrs_EmployeesFingerprints'
        ')
        'BEGIN
        '    SET DATEFORMAT ymd
        '	DELETE FROM hrs_EmployeesFingerprints WHERE FingerprintTime BETWEEN @FromDate AND @ToDate

        '	DECLARE @result TABLE ([FingerprintTime] datetime);

        '	INSERT INTO @result ([FingerprintTime])
        '	EXEC (N'SELECT TOP (1) [FingerprintTime] FROM [hrs_EmployeesFingerprints] WHERE [FingerprintID] = ' + @FingerprintID
        '	    + N' ORDER BY [FingerprintTime] DESC');

        '	SET @LastDateFingerPrint = (SELECT TOP (1) [FingerprintTime] FROM @result);

        '	DECLARE @ConvertFromDate varchar(20) 
        '	DECLARE @ConvertToDate varchar(20) 
        '	SELECT @ConvertFromDate  = CONVERT(VARCHAR(20), @FromDate, 120)
        '	SELECT @ConvertToDate  = CONVERT(VARCHAR(20), @ToDate, 120)

        '	--DELETE FROM hrs_EmployeesFingerprints FOR SPECIFIC PERIOD
        '	SET @Query = NULL
        '	SET @Query = N' DELETE FROM hrs_EmployeesFingerprints '
        '	SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] BETWEEN ''' + @ConvertFromDate + ''''
        '			    + N' AND ''' + @ConvertToDate + ''''
        '--	select UserIdColumnName,CheckInOutColumnName,UserMatchColumnName,1 from hrs_FingerprintSettings


        '	-- INSERT INTO hrs_EmployeesFingerprints FOR SPECIFIC PERIOD
        '	SET @Query = NULL
        '	SET @Query = N'INSERT INTO hrs_EmployeesFingerprints ([UserID ],'
        '			   + N'[FingerprintTime], [UserCode], [FingerprintID])'
        '			   + N'SELECT  fp.USERID,CHECKTIME,fp.BADGENUMBER,1'
        '			   + N' FROM [' + @ServerIP + '].[' + @DatabaseName + N']'
        '			   + N'.[dbo].[' + @FingerprintTableName + N'] AS fp'
        '			   + N' INNER JOIN [' + @ServerIP + '].[' + @DatabaseName + N'].[dbo].[' + @UsersTableName + N'] AS us'
        '			   + N' ON fp.[' + @UserIdColumnName + N'] = us.[' + @UsersTableIdntity + N']'


        '	SET @Query += N' WHERE fp.[' + @CheckInOutColumnName + N'] BETWEEN ''' + @ConvertFromDate + ''''
        '			    + N' AND ''' + @ConvertToDate + ''''

        '	EXEC (@Query)


        '    END
        'END
        '"
        '		ExecuteUpdate(SQL)

        SQL = "
        CREATE TABLE [dbo].[hrs_employeesDecisionsFields](
        	[DecisionField] [nvarchar](50) NULL,
        	[DecisionAraName] [nvarchar](50) NULL,
        	[DecisionEngName] [nvarchar](50) NULL
        ) ON [PRIMARY]
        "
        ExecuteUpdate(SQL)

        SQL = "
        alter table [dbo].[Hrs_NewEmployee]
        add ProfissionID int,
        SSNOIssueDate date,
        SSNOExpireDate date,
        PassportIssueDate date,
        PassportExpireDate date,
        AddressAsPerContract varchar(50)
        "
        ExecuteUpdate(SQL)

        SQL = "
        alter table [dbo].[hrs_Employees]
        add
        SSNOIssueDate varchar(50),
        SSNOExpireDate varchar(50),
        PassportIssueDate varchar(50),
        PassportExpireDate varchar(50),
        AddressAsPerContract varchar(50)
        "
        ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE hrs_Employees
        'ALTER COLUMN SSNOIssueDate varchar(50)
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE hrs_Employees
        'ALTER COLUMN SSNOExpireDate varchar(50)
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE hrs_Employees
        'ALTER COLUMN PassportIssueDate varchar(50)
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'ALTER TABLE hrs_Employees
        'ALTER COLUMN PassportExpireDate varchar(50)
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "
        'EXEC sp_rename 'dbo.hrs_Projects.CostCenterCode',  'CostCenterCode1', 'COLUMN';
        'ALTER TABLE dbo.hrs_Projects ADD
        '	CostCenterCode2 varchar(50) NULL,
        '	CostCenterCode3 varchar(50) NULL,
        '	CostCenterCode4 varchar(50) NULL
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'Create PROCEDURE [dbo].[hrs_GetNewHiredEmployees]
        '	 @SSNO nvarchar(50),
        '     @FromDate date,
        '	 @ToDate date
        '	AS
        'BEGIN
        '	SET NOCOUNT ON;

        'Select * FROM [dbo].[hrs_NewEmployee]
        'Where 
        '	(ISNULL(@SSNO,'')='' or SSNO=@SSNO)
        '	and (ISNULL(@FromDate,'1900-01-01')='1900-01-01' or RegisterDate >=@FromDate)
        '	and (ISNULL(@ToDate,'3000-01-01')='3000-01-01' or RegisterDate <=@ToDate)
        'END
        '"
        '		ExecuteUpdate(SQL)

        '		SQL = "

        'CREATE TABLE [dbo].[sys_SendEmailSetting](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [nvarchar](50) NULL,
        '	[FormName] [nvarchar](100) NULL,
        '	[SendAfterSave] [bit] NULL,
        '	[SendAfterUpdate] [bit] NULL,
        '	[SendAfterDelete] [bit] NULL,
        '	[FromEmail] [nvarchar](100) NULL,
        '	[ToEmailField] [nvarchar](100) NULL,
        '	[ToTable] [nvarchar](100) NULL,
        '	[ToFixedCondition] [nvarchar](250) NULL,
        '	[ToFormCondition] [nvarchar](250) NULL,
        '	[EmailTitle] [nvarchar](150) NULL,
        '	[EmailBody] [nvarchar](500) NULL,
        '	[RegUserID] [nvarchar](50) NULL,
        '	[RegComputerID] [nvarchar](50) NULL,
        '	[CancelDate] [datetime] NULL,
        '	[RegDate] [datetime] NULL,
        ' CONSTRAINT [PK_sys_SendEmailSetting] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]

        'CREATE TABLE [dbo].[sys_SendEmailSettingFixedEmails](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[SendEmailSettingId] [int] NOT NULL,
        '	[FixedEmail] [nvarchar](100) NULL,
        '	[RegUserID] [nvarchar](50) NULL,
        '	[RegComputerID] [nvarchar](50) NULL,
        '	[CancelDate] [datetime] NULL,
        '	[RegDate] [datetime] NULL,
        ' CONSTRAINT [PK_sys_SendEmailSettingFixedEmails] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '    End Function
        '    Public Function UpdateSS() As Boolean
        '        Dim SQL As String

        '       "
        '		ExecuteUpdate(SQL)
#End Region

        SQL = "
		CREATE TABLE dbo.hrs_ProtectionSalaryPrepared
			(
			FisicalPeriod int NULL,
			EmployeeCode int NULL,
			Prepared bit NULL
			)  ON [PRIMARY]
				"
        ExecuteUpdate(SQL)
        Dim Str As String = "SELECT    count(DecisionField) as myCount FROM hrs_employeesDecisionsFields "
        Dim myCount As Integer
        myCount = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(mConnectionString, Data.CommandType.Text, Str)
        If myCount = 0 Then

            SQL = "
		INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'', N'', N'')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'MaritalStatusID', N'الحالة الإجتماعية', N'Marital Status')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'BankID', N'البنك', N'Bank')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'NationalityID', N'الجنسية', N'Nationality')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'BankAccountNumber', N'رقم الحساب البنكي', N'Bank Account Number')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'DepartmentID', N'القسم', N'Department')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'Remarks', N'الملاحظات', N'Remarks')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'BranchID', N'الفرع', N'Branch')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'SponsorID', N'الكفيل', N'Sponsor')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'E_Mail', N'الايميل', N'E_Mail')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'Phone', N'التليفون', N'Phone')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'Mobile', N'الموبايل', N'Mobile')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'ManagerID', N'المدير المباشر', N'Manager')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'SectorID', N'القطاع', N'Sector')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'PassPortNo', N'رقم الباسبور', N'PassPort No')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'Cost1', N'توزيع 1', N'Cost1')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'Cost2', N'توزيع 2', N'Cost2')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'Cost3', N'توزيع 3', N'Cost3')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'Cost4', N'توزيع 4', N'Cost4')
"
            ExecuteUpdate(SQL)
            SQL = " 
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'LocationID', N'المشروع', N'Location')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'WorkE_Mail', N'إيميل العمل', N'WorkE_Mail')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'ContractType', N'نوع العقد', N'Contract Type')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'Professions', N'المهنة', N'Professions')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'Position', N'الرتبة', N'Position')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'EmployeeClass', N'الفئة', N'Employee Class')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'LastEducations', N'المؤهل الدراسي', N'Last Educations')
"
            ExecuteUpdate(SQL)
            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'GraduationDate', N'تاريخ التخرج', N'Graduation Date')
"

            ExecuteUpdate(SQL)

            SQL = "
INSERT [dbo].[hrs_employeesDecisionsFields] ([DecisionField], [DecisionAraName], [DecisionEngName]) VALUES (N'SSnNo', N'رقم الهوية', N'Identity No')
"

            ExecuteUpdate(SQL)
        End If

        SQL = "
        IF Not EXISTS(SELECT 1 FROM sys.columns 
                  WHERE Name = N'SSnNo'
                  AND Object_ID = Object_ID(N'dbo.hrs_EmployeesDecisions'))
        BEGIN

        alter table hrs_EmployeesDecisions add SSnNo nvarchar(20) null
        END
        "
        ExecuteUpdate(SQL)

        SQL = "
        IF Not EXISTS(SELECT 1 FROM sys.columns 
                  WHERE Name = N'PreviousSSnNo'
                  AND Object_ID = Object_ID(N'dbo.hrs_EmployeesDecisions'))
        BEGIN

        alter table hrs_EmployeesDecisions add PreviousSSnNo nvarchar(20) null
        END
        "
        ExecuteUpdate(SQL)

        SQL = "
         IF Not EXISTS(SELECT 1 FROM sys.columns 
                  WHERE Name = N'ExecuseRequestHoursallowed'
                  AND Object_ID = Object_ID(N'dbo.sys_Companies'))
        BEGIN

        alter table sys_Companies add ExecuseRequestHoursallowed int null
        END
        "
        ExecuteUpdate(SQL)

        SQL = "
       IF NOT EXISTS
       (
            SELECT name
            FROM sysobjects
           WHERE name = 'hrs_ProtectionSalaryPrepared2'
       )
            BEGIN
             CREATE TABLE [dbo].[hrs_ProtectionSalaryPrepared2](
	[FisicalPeriod] [int] NULL,
	[EmployeeCode] [int] NULL,
	[Prepared] [bit] NULL
) ON [PRIMARY]
        	END
        "
        ExecuteUpdate(SQL)


        SQL = "
       CREATE TABLE [dbo].[hrs_VacationsBalance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NULL,
	[Year] [int] NULL,
	[Balance] [decimal](18, 2) NULL,
	[Consumed] [decimal](18, 2) NULL,
	[Remaining] [decimal](18, 2) NULL,
	[BalanceTypeID] [int] NULL,
	[ExpireDate] [date] NULL,
	[Src] [nvarchar](150) NULL,
	[Remarks] [nvarchar](500) NULL,
	[Reguser] [nvarchar](50) NULL,
	[RegDate] [datetime] NULL,
 CONSTRAINT [PK_hrs_VacationsBalance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
        "
        ExecuteUpdate(SQL)

        SQL = "
       CREATE TABLE [dbo].[hrs_VacationsBalanceType](
	[ID] [int] NOT NULL,
	[ArbName] [nvarchar](50) NULL,
	[EngName] [nvarchar](50) NULL,
 CONSTRAINT [PK_hrs_VacationsBalanceType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
        "
        ExecuteUpdate(SQL)

        SQL = "
       ALTER TABLE dbo.hrs_EmployeesClasses ADD
	AdvanceBalance bit NULL,
	VacationTrans bit NULL,
	VactionTransType int NULL,
	TransValue int NULL,
	AddBalanceInAddEmp bit NULL
        "
        ExecuteUpdate(SQL)

        SQL = "INSERT [dbo].[hrs_VacationsBalanceType] ([ID], [ArbName], [EngName]) VALUES (1, N'جديد', N'New') "
        ExecuteUpdate(SQL)

        SQL = "INSERT [dbo].[hrs_VacationsBalanceType] ([ID], [ArbName], [EngName]) VALUES (2, N'مرحل', N'Transfers')"
        ExecuteUpdate(SQL)


        SQL = "
       ALTER TABLE dbo.hrs_VacationsBalance ADD
	EndServiceDate date NULL
"
        ExecuteUpdate(SQL)

        SQL = "
       ALTER TABLE dbo.hrs_EmployeesClasses ADD AccumulatedBalance bit NULL
        "
        ExecuteUpdate(SQL)

        SQL = "
       ALTER TABLE dbo.hrs_EmployeesVacations ADD
	Src [nvarchar](50) NULL,
	VacationRequestID int NULL
        "
        ExecuteUpdate(SQL)


        SQL = "
         ALTER TABLE dbo.hrs_EmployeesExcuses ADD
	Src [nvarchar](50) NULL,
	RequestID int NULL

"
        ExecuteUpdate(SQL)
        SQL = "
       ALTER TABLE dbo.hrs_VacationsBalance ADD
	DueDate date NULL
"
        ExecuteUpdate(SQL)

        SQL = "
       ALTER TABLE dbo.hrs_VacationsBalance ADD
	CancelDate date NULL
"
        ExecuteUpdate(SQL)

        SQL = "
       ALTER PROCEDURE [dbo].[shift_FindAllAttendWorkPlan](@employeeID INT)
AS
    BEGIN
        
        SELECT Att_AttendAppointmentMembers.AppointID AS AppointID, 
              Att_AttendShifts.Code + ' - ' +  Att_AttendShifts.EngName as EngName,  
               Att_AttendShifts.Code + ' - ' +  Att_AttendShifts.ArbName AS ArbName, 
			   Att_AttendAppointment.FromDate, 
               Att_AttendAppointment.ToDate
        FROM Att_AttendAppointmentMembers
             INNER JOIN Att_AttendAppointment ON Att_AttendAppointment.ID  = Att_AttendAppointmentMembers.AppointID 
             INNER JOIN Att_AttendShifts  ON Att_AttendShifts.id = Att_AttendAppointment.AttendaceShiftID 
        WHERE Att_AttendAppointmentMembers.EmployeeID = @employeeID
              AND Att_AttendAppointmentMembers.CancelDate IS NULL
        ORDER BY Att_AttendAppointment.ID DESC;
    END;
"
        ExecuteUpdate(SQL)

        SQL = "
       CREATE TABLE [dbo].[hrs_OfficialVacations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NULL,
	[LineNum] [int] NULL,
	[ArbName] [nvarchar](50) NULL,
	[EngName] [nvarchar](50) NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[RegUserID] [int] NULL,
	[RegDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_hrs_OfficialVacations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "
       CREATE TABLE [dbo].[hrs_EmployeeDepartments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[DepartmentId] [int] NULL,
 CONSTRAINT [PK_hrs_EmployeeDepartments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "
       ALTER TABLE dbo.sys_Companies ADD
	UserDepartmentsPermissions bit NULL
"
        ExecuteUpdate(SQL)

        SQL = "
       CREATE TABLE [dbo].[hrs_EmployeeLocations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[LocationId] [int] NULL,
 CONSTRAINT [PK_hrs_EmployeeLocations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "
     alter table hrs_EmployeesDependants add 
     NationalIDORIqamano varchar(50) NULL
       "
        ExecuteUpdate(SQL)

        SQL = "
     
    
ALTER PROCEDURE [dbo].[hrs_GetEmployeesDependantsData] 
	             @EmployeeId Int,
                 @ObjectId   Int
	 AS
SET NOCOUNT ON;
Select 
       hrs_EmployeesDependants.ID,
       hrs_EmployeesDependants.EmployeeID,
       hrs_EmployeesDependants.DependantTypeID,
       hrs_EmployeesDependants.EngName,
       hrs_EmployeesDependants.ArbName,
	   hrs_EmployeesDependants.ArbName4S,
	   hrs_EmployeesDependants.BirthDate,
	   hrs_EmployeesDependants.BirthCityID,
	   hrs_EmployeesDependants.Sex,
	   hrs_EmployeesDependants.NationalityID,
	   hrs_EmployeesDependants.InsuranceCovered,
	   hrs_EmployeesDependants.Remarks,
	   hrs_EmployeesDependants.RegUserID,
	   hrs_EmployeesDependants.RegComputerID,
	   hrs_EmployeesDependants.RegDate,
	   hrs_EmployeesDependants.CancelDate,
       ( Select top 1 [FileName] From sys_objectsAttachments where sys_ObjectsAttachments.Objectid = @ObjectId  And IsImageView is not null And Isnull(CancelDate,'')='' And  sys_ObjectsAttachments.RecordId = hrs_EmployeesDependants.ID order by IsProfilePicture Desc  ) as FileName,
       @ObjectId as ObjectId,
       hrs_EmployeesDependants.InsurancePercentage,
       hrs_EmployeesDependants.TicketCovered,
       hrs_EmployeesDependants.TicketPercentage,
	   hrs_EmployeesDependants.NationalIDORIqamano
       
From 
      hrs_Employees 
      Inner Join hrs_employeesDependants  on hrs_employeesDependants.EmployeeId = hrs_Employees.Id  
Where 
       IsNull(hrs_employeesDependants.CancelDate,'')=''
       And
       hrs_EmployeesDependants.EmployeeId = @EmployeeId
	   "
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.hrs_OfficialVacations ADD CONSTRAINT
	DF_hrs_OfficialVacations_RegDate DEFAULT (getdate()) FOR RegDate
"
        ExecuteUpdate(SQL)


        SQL = "
         ALTER TABLE dbo.hrs_VacationsTypes ADD
	IsOfficial bit NULL
	
"
        ExecuteUpdate(SQL)

        SQL = "
	
         ALTER TABLE dbo.hrs_HICompanyClasses ADD
	CompanyAmount int null,
	EmployeeAmount int null
"
        ExecuteUpdate(SQL)

        SQL = "
CREATE TABLE [dbo].[Hrs_EmpFPBeforePrepare](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [varchar](50) NULL,
	[TransactionDate] [date] NULL,
	[OriginalTimeIn] [varchar](50) NULL,
	[OriginalTimeOut] [varchar](50) NULL,
	[TimeIn] [varchar](50) NULL,
	[TimeOut] [varchar](50) NULL,
	[Late] [varchar](50) NULL,
	[OverTime] [varchar](50) NULL,
	[RegDate] [date] NULL,
 CONSTRAINT [PK_Hrs_EmpFPBeforePrepare] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

"
        ExecuteUpdate(SQL)

        SQL = "
CREATE TABLE [dbo].[hrs_EmployeesBankHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [nvarchar](50) NULL,
	[BankCode] [int] NULL,
	[BankAccountNumber] [nvarchar](50) NULL,
	[RegUserID] [int] NULL,
	[RegDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_hrs_EmployeesBankHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "
CREATE  VIEW [dbo].[V_Hrs_NewEmployee]
AS
SELECT E.ID, E.Code AS Empcode, dbo.fn_GetEmpName(E.Code, 0) AS Employees_EngName, dbo.fn_GetEmpName(E.Code, 1) AS Employees_ArbName, E.Sex, E.NationalityID, E.DepartmentID, D.EngName AS Department_EngName, D.ArbName, 
                  E.JoinDate, E.Mobile, E.WorkE_Mail, dbo.hrs_Positions.ArbName AS PositionName, dbo.hrs_Positions.EngName AS PositionEngName
FROM     dbo.hrs_Employees AS E LEFT OUTER JOIN
                  dbo.sys_Departments AS D ON D.ID = E.DepartmentID LEFT OUTER JOIN
                      (SELECT EmployeeID, ID, EndDate, ContractTypeID, ProfessionID, PositionID, GradeStepID
                       FROM      dbo.hrs_Contracts
                       WHERE   (CancelDate IS NULL)) AS Contracts ON E.ID = Contracts.EmployeeID LEFT OUTER JOIN
                  dbo.hrs_Positions ON dbo.hrs_Positions.ID = Contracts.PositionID "

        ExecuteUpdate(SQL)

        SQL = "
CREATE TABLE [dbo].[sys_SystemConfig](
	[UseCostCenter] [bit] NULL,
	[MinimumCostCentersCount] [int] NULL,
	[Remarks] [varchar](2048) NULL,
	[RegUserID] [int] NULL,
	[RegComputerID] [int] NULL,
	[RegDate] [smalldatetime] NULL,
	[CancelDate] [smalldatetime] NULL
) ON [PRIMARY] "

        ExecuteUpdate(SQL)
        SQL = "
alter table hrs_SalaryProductionFilesColumns ADD
              [Rank] int null
"
        ExecuteUpdate(SQL)

        SQL = "
CREATE TABLE [dbo].[hrs_BankAccountTypes](
	[ID] [int] NOT NULL,
	[Code] [int] NOT NULL,
	[ArbName] [nvarchar](50) NULL,
	[EngName] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_hrs_BankAccountTypes_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        'SQL = "
        'update hrs_Employees set BankAccountType=1 where BankAccountType='Regular';
        '"
        'ExecuteUpdate(SQL)

        SQL = "
        INSERT into [dbo].[sys_Objects]([ID], [Code], [EngName], [ArbName],[Description]) VALUES (1, 1, N'Banck Card', N'بطاقة بنك',N'Regular')
        "
        ExecuteUpdate(SQL)

        SQL = "
        INSERT into [dbo].[sys_Objects]([ID], [Code], [EngName], [ArbName],[Description]) VALUES (2, 2, N'Salary Card', N'بطاقة راتب',N'Salary')
        "
        ExecuteUpdate(SQL)

        'SQL = "
        'update hrs_Employees set BankAccountType=2 where BankAccountType='Salary'
        '"
        'ExecuteUpdate(SQL)

        SQL = "
alter table hrs_BankAccountTypes ADD CancelDate smalldatetime NULL
"
        ExecuteUpdate(SQL)

        SQL = "
alter table hrs_EmployeesBankHistory alter column BankCode nvarchar(50)
"
        ExecuteUpdate(SQL)

        SQL = "
ALTER PROCEDURE [dbo].[hrs_GetAllExpiredContracts]
	@EmpCode varchar(50) = '', 
	@ExpireFromDate As Datetime = Null,
	@ExpireToDate As Datetime = Null,
	@DeptCode Varchar(50) = '',
	@BranchCode Varchar(50) = '',
	@ContractTypeID Varchar(50) = '',
	@Lang Int = 0
	
As
Set DateFormat DMY
	IF @ExpireFromDate Is Null
	Begin
		Set @ExpireFromDate = '1900-01-01'
	End
	IF @ExpireToDate Is Null
	Begin
		Set @ExpireToDate = GETDATE()
	End
	Set @ExpireFromDate = Cast(CONVERT(char(10),@ExpireFromDate,103)+' 00:00:00' As Datetime)
	Set @ExpireToDate = Cast(CONVERT(char(10),@ExpireToDate,103)+' 23:59:00' As Datetime)
Select 
	hrs_Contracts.ID,
	hrs_Employees.Code As EmpCode,
	Case When @Lang <> 1 Then
		ISNull(hrs_Employees.EngName,'')+','+ISNull(FatherEngName,'')+','+ISNull(GrandEngName,'')+','+ISNull(FamilyEngName,'')
	Else
		ISNull(hrs_Employees.ArbName,'')+','+ISNull(FatherArbName,'')+','+ISNull(GrandArbName,'')+','+ISNull(FamilyArbName,'')
	End
	As EmployeeName,
	hrs_Contracts.Number As ContractNumber,
	Case When @Lang <> 1 Then
		hrs_ContractsTypes.EngName 
	Else
		hrs_ContractsTypes.ArbName
	End
	As ContractType,
	hrs_Contracts.StartDate As FromDate,
	Case When isnull(hrs_Contracts.EndDate,0) = 0 Then
	case when hrs_Contracts.ContractPeriod = 0 then
	null
	else
	isnull(hrs_Contracts.EndDate,Dateadd(DAY,-1,DateAdd(MONTH,IsNull(ContractPeriod,12),hrs_Contracts.StartDate)))
	end
	else
	 hrs_Contracts.EndDate
	End As ToDate
	,

	hrs_Contracts.Number						AS ContNumber,
	hrs_ContractsTypes.EngName				AS ContTypeEngName,
	hrs_ContractsTypes.ArbName				AS ContTypeArbName,
	hrs_Contracts.StartDate					AS ContStartDate,
	Case When isnull(hrs_Contracts.EndDate,0) = 0 Then
	case when hrs_Contracts.ContractPeriod = 0 then
	null
	else
	isnull(hrs_Contracts.EndDate,Dateadd(DAY,-1,DateAdd(MONTH,IsNull(ContractPeriod,12),hrs_Contracts.StartDate)))
	end
	else
	 hrs_Contracts.EndDate
	End AS ContEndDate,
	hrs_Employees.Code						AS EmpCode,
	dbo.fn_GetEmpName (hrs_Employees.Code,0)	AS EmpEngName,
	dbo.fn_GetEmpName (hrs_Employees.Code,1)	AS EmpArbName,
	sys_Departments.EngName						AS DeptEngName,
	sys_Departments.ArbName						AS DeptArbName 
From 
	hrs_Contracts 
	left Join hrs_Employees On hrs_Contracts.EmployeeID = hrs_Employees.ID
	Left join hrs_ContractsTypes On hrs_Contracts.ContractTypeID = hrs_ContractsTypes.ID 
	Left Join sys_Departments On hrs_Employees.DepartmentID = sys_Departments .ID
	Left join sys_Branches On hrs_Employees.BranchID = sys_Branches.ID
Where
	hrs_Contracts.CancelDate Is Null and hrs_Contracts.EndDate is not null And hrs_Employees.ExcludeDate Is Null
	And hrs_Employees.CancelDate Is Null and isnull(hrs_Employees.RegComputerID,0) = 0 
	And hrs_Contracts.EndDate <=  @ExpireToDate
	And (isnull(@EmpCode,'') = '' or hrs_Employees.Code = @EmpCode)
	And (isnull(@ContractTypeID,'') = '' or hrs_ContractsTypes.Code = @ContractTypeID)
	And (isnull(@DeptCode,'') = '' or sys_Departments.Code = @DeptCode)
	And (isnull(@BranchCode,'') = '' or sys_Branches.Code = @BranchCode)
	And (
			Select 
				COUNT(TC.ID) 
			From 
				hrs_Contracts TC 
			Where 
				TC.EmployeeID = hrs_Contracts.EmployeeID 
				And TC.CancelDate Is Null
				And TC.ID <> hrs_Contracts.ID
				And TC.StartDate > isnull(hrs_Contracts.EndDate,Dateadd(DAY,-1,DateAdd(MONTH,IsNull(ContractPeriod,12),hrs_Contracts.StartDate)))
		 ) = 0
"
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.sys_DocumentsDetails ADD ReferenceNumber nvarchar(100) NULL
"
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.hrs_EmployeesBankHistory ADD BankCodeOld nvarchar(50) NULL
"
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.hrs_EmployeesBankHistory ADD BankAccountNumberOld nvarchar(50) NULL
"
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.sys_Locations ADD
	DepartmentID int NULL,
	CostCenterCode1 varchar(50) NULL,
	CostCenterCode2 varchar(50) NULL,
	CostCenterCode3 varchar(50) NULL,
	CostCenterCode4 varchar(50) NULL
"
        ExecuteUpdate(SQL)

        SQL = "
CREATE TABLE [dbo].[hrs_BalanceExpireHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[BalanceType] [int] NULL,
	[OldExpireDate] [date] NULL,
	[NewExpireDate] [date] NULL,
	[RegUserID] [int] NULL,
	[RegDate] [datetime] NULL,
 CONSTRAINT [PK_hrs_BalanceExpireHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.hrs_Employees ADD IsSocialInsuranceIncluded bit NULL
"
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.hrs_TransactionsTypes ADD HasInsuranceTiers bit NULL
"
        ExecuteUpdate(SQL)

        SQL = "
CREATE TABLE [dbo].[hrs_TransactionsTypesTiers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionsTypesId] [int] NULL,
	[SerialNumberTiers] [int] NULL,
	[FinancialPeriodTiers] [date] NULL,
	[BaseFormulaTiers] [nvarchar](100) NULL,
	[BeginFormulaTiers] [nvarchar](100) NULL,
	[EndFormulaTiers] [nvarchar](100) NULL,
 CONSTRAINT [PK_hrs_TransactionsTypesTiers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        'Appraisal Module Scripts
        SQL = "CREATE TABLE [dbo].[App_AppraisalConfigurations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppraisalTypeID] [int] NULL,
	[UserTypeID] [nvarchar](50) NULL,
	[PositionID] [int] NULL,
	[EmployeeID] [int] NULL,
	[Rank] [int] NULL,
	[StageWeight] [int] NULL,
	[HasObjection] [bit] NULL,
	[NoOfObjections] [int] NULL,
	[IsToConfirmOnly] [bit] NULL,
	[CreateEscalation] [bit] NULL,
	[EscalationPeriod] [int] NULL,
	[EscalationMail] [varchar](100) NULL,
	[RegDate] [varchar](50) NULL,
	[RegUserID] [int] NULL,
 CONSTRAINT [PK_App_AppraisalConfigurations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[APP_AppraisalCriterias](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppraisalID] [int] NULL,
	[CriteriaGroupID] [int] NULL,
	[CriteriaID] [int] NULL,
	[ByValue] [bit] NULL,
	[ByPercentage] [bit] NULL,
	[MinimumScore] [int] NULL,
	[MaximumScore] [int] NULL,
	[Weight] [int] NULL,
	[RegUserID] [varchar](50) NULL,
	[RegDate] [date] NULL,
 CONSTRAINT [PK_APP_AppraisalCriterias] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[App_AppraisalEmployeeImprovements](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppraisalId] [int] NULL,
	[EmployeeId] [int] NULL,
	[ConfigurationLevel] [int] NULL,
	[GroupName] [nvarchar](100) NULL,
	[ImproveName] [nvarchar](150) NULL,
	[Remarks] [nvarchar](150) NULL,
 CONSTRAINT [PK_App_AppraisalEmployeeImprovements] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[App_AppraisalEmployeePoints](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppraisalId] [int] NULL,
	[EmployeeId] [int] NULL,
	[ConfigurationLevel] [int] NULL,
	[StrengthPoints] [nvarchar](2000) NULL,
	[WeaknessPoints] [nvarchar](2000) NULL,
 CONSTRAINT [PK_App_AppraisalEmployeePoints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[APP_AppraisalEmployees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppraisalID] [int] NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK_APP_AppraisalEmployees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[App_AppraisalEmployeesKPIs_D](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KPIsHeaderID] [int] NOT NULL,
	[LineNum] [int] NOT NULL,
	[KPIName] [nvarchar](200) NOT NULL,
	[MinScore] [int] NULL,
	[Weight] [int] NOT NULL,
	[RegUserID] [int] NULL,
	[RegDate] [datetime] NOT NULL,
	[CancelDate] [datetime] NULL,
	[MaxScore] [decimal](9, 2) NULL,
 CONSTRAINT [PK__App_Appr__3214EC2773B69AB1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[App_AppraisalEmployeesKPIs_H](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](30) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[AppraisalTypeID] [int] NOT NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NOT NULL,
	[TotalWeight] [decimal](9, 2) NULL,
	[Description] [nvarchar](500) NULL,
	[RegUserID] [int] NULL,
	[RegDate] [datetime] NOT NULL,
	[CancelDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[App_AppraisalNotifications](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppraisalID] [int] NULL,
	[APP_EmployeeID] [int] NULL,
	[EmployeeID] [int] NULL,
	[ConfigurationID] [int] NULL,
	[ConfigurationLevel] [int] NULL,
	[MaxActionDate] [date] NULL,
	[Completed] [bit] NULL,
	[CompleteDate] [date] NULL,
	[IsObjection] [bit] NULL,
	[RegDate] [date] NULL,
	[RegUserID] [varchar](50) NULL,
	[CancelDate] [date] NULL,
 CONSTRAINT [PK_App_AppraisalNotifications] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"

        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[App_AppraisalResult](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppraisalID] [int] NULL,
	[NotificationID] [int] NULL,
	[EmployeeID] [int] NULL,
	[CriteriaGroupID] [int] NULL,
	[CriteriaID] [int] NULL,
	[App_EmployeeID] [int] NULL,
	[Score] [int] NULL,
	[Remarks] [varchar](max) NULL,
	[HasObjection] [bit] NULL,
	[ObjectionDetails] [nvarchar](250) NULL,
	[ObjectionDate] [date] NULL,
	[RegUserID] [varchar](50) NULL,
	[RegDate] [date] NULL,
 CONSTRAINT [PK_App_AppraisalResult] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[APP_Appraisals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[ArabName] [nvarchar](100) NULL,
	[EngName] [nvarchar](100) NULL,
	[AppraisalTypeID] [int] NOT NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[EmployeeID] [int] NULL,
	[Notes] [varchar](max) NULL,
	[NotificationSent] [bit] NULL,
	[IsAcknowledge] [bit] NULL,
	[EscalationSent] [bit] NULL,
	[AppraisalStatusID] [int] NULL,
	[RegDate] [date] NULL,
	[RegUserID] [varchar](50) NULL,
	[CancelDate] [date] NULL,
 CONSTRAINT [PK__APP_Appr__711680165934F4A8] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__APP_Appr__F6FFA8F712A979DB] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
"
        ExecuteUpdate(SQL)
        SQL = "CREATE TABLE [dbo].[App_AppraisalTypeGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[EngName] [varchar](50) NULL,
	[ArbName] [varchar](50) NULL,
	[RegUserID] [varchar](50) NULL,
	[RegDate] [varchar](50) NULL,
	[CancelDate] [varchar](50) NULL,
	[DeleteUserID] [varchar](50) NULL,
 CONSTRAINT [PK_App_AppraisalTypeGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[App_AppraisalTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[EngName] [varchar](50) NULL,
	[ArbName] [varchar](50) NULL,
	[IsOneTimeOnly] [bit] NULL,
	[OneTimePeriod] [int] NULL,
	[AppraisalFrequency] [int] NULL,
	[NotificationPeriod] [int] NULL,
	[escalationMail] [varchar](150) NULL,
	[escalationPeriod] [int] NULL,
	[MinimumImprovementPerc] [int] NULL,
	[KPIsWeight] [int] NULL,
	[AppraisalTypeGroupID] [int] NULL,
	[RegUserID] [varchar](50) NULL,
	[RegDate] [varchar](50) NULL,
	[CancelDate] [varchar](50) NULL,
	[DeleteUserID] [varchar](50) NULL,
 CONSTRAINT [PK_App_AppraisalTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = "CREATE TABLE [dbo].[App_Criteria](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[EngName] [varchar](700) NULL,
	[ArbName] [varchar](700) NULL,
	[Score] [varchar](50) NULL,
	[CriteriaGroupTypeID] [int] NULL,
	[CriteriaGroupID] [int] NULL,
	[DefaultMinimumScore] [int] NULL,
	[DefaultMaximumScore] [int] NULL,
	[DefaultWeight] [int] NULL,
	[RegUserID] [varchar](50) NULL,
	[RegDate] [varchar](50) NULL,
	[CancelDate] [varchar](50) NULL,
	[DeleteUserID] [varchar](50) NULL,
 CONSTRAINT [PK_App_Criteria] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)


        SQL = "CREATE TABLE [dbo].[App_CriteriaGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](500) NULL,
	[EngName] [varchar](500) NULL,
	[ArbName] [varchar](500) NULL,
	[CriteriaGroupTypeID] [int] NULL,
	[RegUserID] [varchar](50) NULL,
	[RegDate] [varchar](50) NULL,
	[CancelDate] [varchar](50) NULL,
	[DeleteUserID] [varchar](50) NULL,
 CONSTRAINT [PK_App_CriteriaGroups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)
        SQL = "CREATE TABLE [dbo].[App_CriteriaGroupTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[ArbName] [varchar](50) NULL,
	[EngName] [varchar](50) NULL,
	[IsResponsibilities] [bit] NULL,
	[IsCompetences] [bit] NULL,
	[IsKPIGroup] [bit] NULL,
	[CancelDate] [date] NULL,
 CONSTRAINT [PK_App_CriteriaGroupTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = " CREATE TABLE [dbo].[App_DepartmentCriteria](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentID] [int] NULL,
	[CriteriaID] [int] NULL,
	[ByValue] [bit] NULL,
	[ByPercentage] [bit] NULL,
	[MinimumScore] [int] NULL,
	[MaximumScore] [int] NULL,
	[RegUserId] [varchar](50) NULL,
	[RegDate] [varchar](50) NULL,
 CONSTRAINT [PK_App_DepartmentCriteria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)

        SQL = " CREATE TABLE [dbo].[App_EvaluatorsTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[EngName] [varchar](50) NULL,
	[ArbName] [varchar](50) NULL,
	[RegUserID] [varchar](50) NULL,
	[RegDate] [varchar](50) NULL,
	[CancelDate] [varchar](50) NULL,
 CONSTRAINT [PK_App_EvaluatorsTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
"
        ExecuteUpdate(SQL)
        SQL = "Alter Table App_CriteriaGroups add 	CriteriaGroupTypeID int NULL "
        ExecuteUpdate(SQL)

        SQL = "  CREATE TABLE [dbo].[APP_Position_Accountability](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Checked] [bit] NULL,
	[CriteriaID] [int] NULL,
	[PositionID] [int] NULL,
	[MinimumScore] [int] NULL,
	[MaximumScore] [int] NULL,
	[Weight] [int] NULL,
 CONSTRAINT [PK_APP_Position_Accountability] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

"
        ExecuteUpdate(SQL)
        SQL = " CREATE TABLE [dbo].[APP_Position_Competences](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Checked] [bit] NULL,
	[CriteriaID] [int] NULL,
	[PositionID] [int] NULL,
	[MinimumScore] [int] NULL,
	[MaximumScore] [int] NULL,
	[Weight] [int] NULL,
 CONSTRAINT [PK_APP_Position_Competences] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 "

        ExecuteUpdate(SQL)
        SQL = " CREATE TABLE [dbo].[App_PositionCriteria](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PositionID] [int] NULL,
	[CriteriaID] [int] NULL,
	[ByValue] [bit] NULL,
	[ByPercentage] [bit] NULL,
	[MinimumScore] [int] NULL,
	[MaximumScore] [varchar](50) NULL,
	[RegUserId] [varchar](50) NULL,
	[RegDate] [varchar](50) NULL,
 CONSTRAINT [PK_App_PositionCriteria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 "
        ExecuteUpdate(SQL)

        SQL = " CREATE TABLE [dbo].[APP_Status](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[ArbName] [varchar](50) NULL,
	[EngName] [varchar](50) NULL,
 CONSTRAINT [PK_APP_Status] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 "
        ExecuteUpdate(SQL)









        SQL = " alter table sys_SystemConfig add PreventChangeContractEndDate bit null "
        ExecuteUpdate(SQL)

        SQL = " ALTER TABLE dbo.sys_SystemConfig ADD MultiBranchedPosition bit NULL "
        ExecuteUpdate(SQL)

        SQL = " CREATE TABLE [dbo].[hrs_JobBranchesPermission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NULL,
	[PositionID] [int] NULL,
	[EmployeeId] [int] NULL,
 CONSTRAINT [PK_hrs_JobBranchesPermission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] "
        ExecuteUpdate(SQL)

        SQL = " CREATE TABLE [dbo].[hrs_JobBranchesPermissionDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JobBranchesPermissionId] [int] NULL,
	[BranchId] [int] NULL,
 CONSTRAINT [PK_hrs_JobBranchesPermissionDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] "
        ExecuteUpdate(SQL)

        SQL = "alter table hrs_positions add AppraisalTypeGroupID int null"
        ExecuteUpdate(SQL)


        SQL = " ALTER Table [dbo].[hrs_PositionCompetences] add
CriteriaGroupID int null,
CriteriaID int null,
MinimumScore int null,
MaximumScore int null,
[Weight] int null "
        ExecuteUpdate(SQL)

        SQL = "  CREATE VIEW [dbo].[V_AppraisalDueEscalation]
AS
SELECT Aprty.EngName, Apr.Code AS AppraisalCode, dbo.fn_GetEmpName(E.Code, 0) AS EmployeeName, ApEmp.ID, ApEmp.ID AS EvaluatorID, dbo.fn_GetEmpName(ApEmp.Code, 0) AS EvaluatorName, Apr.ToDate, con.EscalationPeriod, 
                  con.EscalationMail, Apr.ID AS AppraisalID
FROM     dbo.App_AppraisalNotifications AS nt INNER JOIN
                  dbo.APP_Appraisals AS Apr ON nt.AppraisalID = Apr.ID INNER JOIN
                  dbo.App_AppraisalConfigurations AS con ON Apr.AppraisalTypeID = con.AppraisalTypeID INNER JOIN
                  dbo.App_AppraisalTypes AS Aprty ON Apr.AppraisalTypeID = Aprty.ID INNER JOIN
                  dbo.hrs_Employees AS E ON E.ID = Apr.EmployeeID INNER JOIN
                  dbo.hrs_Employees AS ApEmp ON ApEmp.ID = nt.APP_EmployeeID
WHERE  (nt.APP_EmployeeID = 12) AND (nt.Completed = 0) AND (con.CreateEscalation = 1) AND (GETDATE() > DATEADD(day, con.EscalationPeriod, Apr.ToDate)) AND (Apr.EscalationSent IS NULL) AND (con.EscalationPeriod > 0) OR
                  (nt.APP_EmployeeID = 12) AND (nt.Completed IS NULL) AND (con.CreateEscalation = 1) AND (GETDATE() > DATEADD(day, con.EscalationPeriod, Apr.ToDate)) AND (Apr.EscalationSent IS NULL) AND (con.EscalationPeriod > 0)
 "
        ExecuteUpdate(SQL)




        SQL = "  
CREATE VIEW [dbo].[V_AppraisalDueNotification]
AS
SELECT dbo.hrs_Employees.Code, dbo.fn_GetEmpName(dbo.hrs_Employees.Code, 0) AS EmployeeName, dbo.App_AppraisalTypes.EngName AS AppraisalType, hrs_Employees_1.WorkE_Mail, dbo.APP_Appraisals.FromDate, 
                  dbo.APP_Appraisals.ToDate, dbo.hrs_Employees.ID, dbo.hrs_Employees.WorkE_Mail AS Evaluated_EMail
FROM     dbo.App_AppraisalTypes INNER JOIN
                  dbo.APP_Appraisals ON dbo.App_AppraisalTypes.ID = dbo.APP_Appraisals.AppraisalTypeID INNER JOIN
                  dbo.App_AppraisalNotifications ON dbo.APP_Appraisals.ID = dbo.App_AppraisalNotifications.AppraisalID INNER JOIN
                  dbo.hrs_Employees ON dbo.APP_Appraisals.EmployeeID = dbo.hrs_Employees.ID INNER JOIN
                  dbo.hrs_Employees AS hrs_Employees_1 ON dbo.hrs_Employees.ManagerID = hrs_Employees_1.ID
WHERE  (dbo.App_AppraisalNotifications.Completed = 0) OR
                  (dbo.App_AppraisalNotifications.Completed IS NULL)
 "
        ExecuteUpdate(SQL)


        SQL = "CREATE VIEW [dbo].[V_AppraisalFinalResult]
AS
SELECT A.ID AS AppraisalID, N.EmployeeID, A.Code AS AppraisalCode, APT.ID AS AppraisalTypeID, APT.EngName AS AppraisalType, A.EngName AS AppraisalName, CAST(E.Code AS VARCHAR(10)) + ' - ' + dbo.fn_GetEmpName(E.Code, 0) 
                  AS EmployeeName, CASE WHEN SUM(CASE WHEN R.Score IS NOT NULL THEN 1 ELSE 0 END) = 0 THEN NULL ELSE SUM((CAST(R.Score AS FLOAT) / CASE WHEN AC.MaximumScore > 0 THEN AC.MaximumScore ELSE 1 END) * AC.Weight) 
                  * (MAX(AC_conf.StageWeight) / 100.0) END AS Score, CASE WHEN SUM(CASE WHEN R.Score IS NOT NULL THEN 1 ELSE 0 END) = 0 THEN 'Awaiting Evaluation' WHEN SUM((CAST(R.Score AS FLOAT) 
                  / CASE WHEN AC.MaximumScore > 0 THEN AC.MaximumScore ELSE 1 END) * AC.Weight) * (MAX(AC_conf.StageWeight) / 100.0) < 50 THEN 'Weak' WHEN SUM((CAST(R.Score AS FLOAT) 
                  / CASE WHEN AC.MaximumScore > 0 THEN AC.MaximumScore ELSE 1 END) * AC.Weight) * (MAX(AC_conf.StageWeight) / 100.0) < 65 THEN 'Medium' WHEN SUM((CAST(R.Score AS FLOAT) 
                  / CASE WHEN AC.MaximumScore > 0 THEN AC.MaximumScore ELSE 1 END) * AC.Weight) * (MAX(AC_conf.StageWeight) / 100.0) < 75 THEN 'Good' WHEN SUM((CAST(R.Score AS FLOAT) 
                  / CASE WHEN AC.MaximumScore > 0 THEN AC.MaximumScore ELSE 1 END) * AC.Weight) * (MAX(AC_conf.StageWeight) / 100.0) < 85 THEN 'Very Good' ELSE 'Excellent' END AS AppraisalResult
FROM     dbo.App_AppraisalNotifications AS N INNER JOIN
                  dbo.hrs_Employees AS E ON N.EmployeeID = E.ID INNER JOIN
                  dbo.APP_Appraisals AS A ON N.AppraisalID = A.ID INNER JOIN
                  dbo.App_AppraisalTypes AS APT ON APT.ID = A.AppraisalTypeID INNER JOIN
                  dbo.App_AppraisalConfigurations AS AC_conf ON N.ConfigurationID = AC_conf.ID LEFT OUTER JOIN
                  dbo.App_AppraisalResult AS R ON R.NotificationID = N.ID AND R.EmployeeID = N.EmployeeID LEFT OUTER JOIN
                  dbo.APP_AppraisalCriterias AS AC ON R.CriteriaID = AC.CriteriaID AND R.AppraisalID = AC.AppraisalID
WHERE  (R.HasObjection = 0) OR
                  (R.HasObjection IS NULL)
GROUP BY A.Code, APT.EngName, N.EmployeeID, E.Code, APT.ID, dbo.fn_GetEmpName(E.Code, 0), A.ID, A.EngName
"
        ExecuteUpdate(SQL)


        SQL = " CREATE VIEW [dbo].[V_AppraisalResult]
AS
SELECT App_CriteriaGroups.EngName AS CriteriaGroupName, App_AppraisalEmployeesKPIs_D.KPIName AS CriteriaName, App_AppraisalResult.EmployeeID, App_AppraisalResult.CriteriaGroupID, App_AppraisalResult.CriteriaID, 
                  App_AppraisalResult.AppraisalID, App_AppraisalEmployeesKPIs_D.MinScore AS MinimumScore, App_AppraisalEmployeesKPIs_D.MaxScore AS MaximumScore, App_AppraisalResult.Score AS AppraisalScore, 
                  App_AppraisalResult.NotificationID, App_AppraisalResult.App_EmployeeID
				  ,Round((cast(App_AppraisalResult.Score as float)/App_AppraisalEmployeesKPIs_D.MaxScore ),2)* App_AppraisalEmployeesKPIs_D.Weight As AppraisalScorePercent
FROM     App_AppraisalResult JOIN
                  App_CriteriaGroups ON App_AppraisalResult.CriteriaGroupID = App_CriteriaGroups.ID JOIN
                  app_Criteria ON App_AppraisalResult.CriteriaID = app_Criteria.ID JOIN
                  App_AppraisalEmployeesKPIs_D ON App_AppraisalResult.CriteriaID = App_AppraisalEmployeesKPIs_D.ID AND App_AppraisalEmployeesKPIs_D.ID = App_AppraisalResult.CriteriaID JOIN
                  App_CriteriaGroupTypes ON App_CriteriaGroupTypes.ID = App_CriteriaGroups.CriteriaGroupTypeID
WHERE  App_CriteriaGroupTypes.IsKPIGroup = 1 AND (App_AppraisalResult.HasObjection = 0 OR
                  App_AppraisalResult.HasObjection IS NULL)
UNION ALL
SELECT App_CriteriaGroups.EngName AS CriteriaGroupName, app_Criteria.EngName AS CriteriaName, App_AppraisalResult.EmployeeID, App_AppraisalResult.CriteriaGroupID, App_AppraisalResult.CriteriaID, App_AppraisalResult.AppraisalID, 
                  APP_AppraisalCriterias.MinimumScore, APP_AppraisalCriterias.MaximumScore, App_AppraisalResult.Score AS AppraisalScore, 
				  App_AppraisalResult.NotificationID, App_AppraisalResult.App_EmployeeID
				  ,Round((cast(App_AppraisalResult.Score as float)/APP_AppraisalCriterias.MaximumScore),2)*APP_AppraisalCriterias.Weight as AppraisalScorePercent
FROM     App_AppraisalResult JOIN
                  App_CriteriaGroups ON App_AppraisalResult.CriteriaGroupID = App_CriteriaGroups.ID JOIN
                  app_Criteria ON App_AppraisalResult.CriteriaID = app_Criteria.ID JOIN
                  APP_AppraisalCriterias ON APP_AppraisalCriterias.CriteriaID = app_Criteria.ID AND APP_AppraisalCriterias.CriteriaGroupID = App_CriteriaGroups.ID AND APP_AppraisalCriterias.AppraisalID = App_AppraisalResult.AppraisalID JOIN
                  APP_Appraisals ON APP_Appraisals.EmployeeID = App_AppraisalResult.EmployeeID JOIN
                  App_CriteriaGroupTypes ON App_CriteriaGroupTypes.ID = App_CriteriaGroups.CriteriaGroupTypeID

WHERE  App_CriteriaGroupTypes.IsCompetences = 1 AND (App_AppraisalResult.HasObjection = 0 OR
                  App_AppraisalResult.HasObjection IS NULL)
UNION ALL
SELECT App_CriteriaGroups.EngName AS CriteriaGroupName, app_Criteria.EngName, App_AppraisalResult.EmployeeID, App_AppraisalResult.CriteriaGroupID, App_AppraisalResult.CriteriaID, App_AppraisalResult.AppraisalID, 
                  APP_AppraisalCriterias.MinimumScore, APP_AppraisalCriterias.MaximumScore, App_AppraisalResult.Score AS AppraisalScore,
				  App_AppraisalResult.NotificationID, App_AppraisalResult.App_EmployeeID
				  ,Round((cast(App_AppraisalResult.Score as float)/APP_AppraisalCriterias.MaximumScore),2)*APP_AppraisalCriterias.Weight  as AppraisalScorePercent
FROM     App_AppraisalResult JOIN
                  App_CriteriaGroups ON App_AppraisalResult.CriteriaGroupID = App_CriteriaGroups.ID JOIN
                  app_Criteria ON App_AppraisalResult.CriteriaID = app_Criteria.ID JOIN
                  APP_AppraisalCriterias ON APP_AppraisalCriterias.CriteriaID = app_Criteria.ID AND APP_AppraisalCriterias.CriteriaGroupID = App_CriteriaGroups.ID AND APP_AppraisalCriterias.AppraisalID = App_AppraisalResult.AppraisalID JOIN
                  APP_Appraisals ON APP_Appraisals.EmployeeID = App_AppraisalResult.EmployeeID JOIN
                  App_CriteriaGroupTypes ON App_CriteriaGroupTypes.ID = App_CriteriaGroups.CriteriaGroupTypeID
WHERE  App_CriteriaGroupTypes.IsResponsibilities = 1 AND (App_AppraisalResult.HasObjection = 0 OR
                  App_AppraisalResult.HasObjection IS NULL)
  "

        ExecuteUpdate(SQL)
        SQL = "  CREATE VIEW [dbo].[V_PreviousAppraisalEvaluation]
AS
SELECT dbo.App_AppraisalResult.AppraisalID, dbo.App_AppraisalResult.App_EmployeeID, dbo.App_AppraisalResult.EmployeeID, dbo.App_CriteriaGroups.ID AS CriteriaGroupID, dbo.App_CriteriaGroups.EngName AS CritriaGroupEngName, 
                  dbo.App_CriteriaGroups.ArbName AS CritriaGroupArbName, App_AppraisalEmployeesKPIs_D.ID AS CriteriaID, App_AppraisalEmployeesKPIs_D.KPIName AS CriteriaEngName, 
                  App_AppraisalEmployeesKPIs_D.KPIName AS CriteriaArbName, App_AppraisalEmployeesKPIs_D.MinScore AS MinimumScore, App_AppraisalEmployeesKPIs_D.MaxScore AS MaximumScore, 
                  dbo.App_AppraisalResult.Score AS AppraisalScore, dbo.App_AppraisalResult.HasObjection, dbo.App_AppraisalResult.ObjectionDetails
FROM     dbo.App_AppraisalResult INNER JOIN
                  dbo.APP_Appraisals ON dbo.App_AppraisalResult.AppraisalID = dbo.APP_Appraisals.ID INNER JOIN
                  dbo.App_AppraisalEmployeesKPIs_D ON dbo.App_AppraisalResult.CriteriaID = dbo.App_AppraisalEmployeesKPIs_D.ID INNER JOIN
                  dbo.App_CriteriaGroups ON dbo.App_AppraisalResult.CriteriaGroupID = dbo.App_CriteriaGroups.ID
				  join App_CriteriaGroupTypes on App_CriteriaGroupTypes.ID=App_CriteriaGroups.CriteriaGroupTypeID
WHERE  (dbo.App_CriteriaGroupTypes.IsKPIGroup = 1)
UNION ALL
SELECT dbo.App_AppraisalResult.AppraisalID, dbo.App_AppraisalResult.App_EmployeeID, dbo.App_AppraisalResult.EmployeeID, dbo.App_CriteriaGroups.ID AS CriteriaGroupID, dbo.App_CriteriaGroups.EngName AS CritriaGroupEngName, 
                  dbo.App_CriteriaGroups.ArbName AS CritriaGroupArbName, dbo.App_Criteria.ID AS CriteriaID, dbo.App_Criteria.EngName AS CriteriaEngName, dbo.App_Criteria.ArbName AS CriteriaArbName, 
                  dbo.APP_AppraisalCriterias.MinimumScore, dbo.APP_AppraisalCriterias.MaximumScore, dbo.App_AppraisalResult.Score, dbo.App_AppraisalResult.HasObjection, dbo.App_AppraisalResult.ObjectionDetails
FROM     dbo.App_AppraisalResult INNER JOIN
                  dbo.App_CriteriaGroups ON dbo.App_CriteriaGroups.ID = dbo.App_AppraisalResult.CriteriaGroupID INNER JOIN
                  dbo.App_Criteria ON dbo.App_AppraisalResult.CriteriaID = dbo.App_Criteria.ID AND dbo.App_AppraisalResult.CriteriaGroupID = dbo.App_Criteria.CriteriaGroupID JOIN
                  dbo.APP_AppraisalCriterias ON dbo.APP_AppraisalCriterias.CriteriaID = dbo.App_Criteria.ID AND APP_AppraisalCriterias.AppraisalID = App_AppraisalResult.AppraisalID 
				  AND 
                  APP_AppraisalCriterias.CriteriaGroupID = App_AppraisalResult.CriteriaGroupID
				 join App_CriteriaGroupTypes on App_CriteriaGroupTypes.ID=App_CriteriaGroups.CriteriaGroupTypeID

WHERE  (dbo.App_CriteriaGroupTypes.IsKPIGroup <> 1 or dbo.App_CriteriaGroupTypes.IsKPIGroup is null)

  "
        ExecuteUpdate(SQL)



        SQL = "
CREATE TABLE hrs_EmployeePenalty (
Id INT IDENTITY(1,1) PRIMARY KEY,
    Code INT NULL,    
    EmployeeId INT NOT NULL,                   
    PenaltyDate DATE NOT NULL,                 
    TransactionTypeId INT NOT NULL,             
    DaysNo float NOT NULL,                        
    Notes NVARCHAR(500) NULL,                  
    RegUserID int NULL,              
    RegDate DATETIME DEFAULT GETDATE()     
);
"
        ExecuteUpdate(SQL)
        SQL = "   ALTER     PROCEDURE [dbo].[hrs_EmployeesAttachDocumentsExcel]
    @Lang INT,
    @DepartmentID INT = NULL,
    @UnitID INT = NULL,
    @FromDate DATE=null,
    @ToDate DATE = null,
    @EmployeeID INT = NULL,
	@ContractTypeId int=null,
	@BranchId int=null,
	@SponsorId int=null,
	@ProjectId int=null,
	@NationalityId int=null
	As 

select  hrs_Employees.Code as EmployeeCode,dbo.fn_GetEmpName(hrs_Employees.Code,0) as EmployeeName , sys_Documents.EngName as DocumentName, count(sys_DocumentsDetails.documentid)as DocumentsCount from sys_DocumentsDetails 
join sys_Documents on sys_DocumentsDetails.DocumentID=sys_Documents.ID
join hrs_Employees on sys_DocumentsDetails.RecordID=hrs_Employees.ID
join hrs_Contracts on hrs_Contracts.EmployeeID=hrs_Employees.ID
join hrs_EmployeesClasses on hrs_EmployeesClasses.ID=hrs_Contracts.EmployeeClassID
where 
  (@EmployeeID IS NULL OR hrs_Employees.ID = @EmployeeID)
    AND (ISNULL(@DepartmentID, '') = '' OR hrs_Employees.DepartmentID = @DepartmentID)
            AND (ISNULL(@UnitID, '') = '' OR hrs_Employees.LocationID = @UnitID)
			 AND (ISNULL(@ContractTypeId, '') = '' OR hrs_Contracts.ContractTypeID = @ContractTypeId)
			 AND (ISNULL(@BranchId, '') = '' OR hrs_Employees.BranchID = @BranchId)
			 AND (ISNULL(@SponsorId, '') = '' OR hrs_Employees.SponsorID = @SponsorId)
			 AND (ISNULL(@ProjectId, '') = '' OR hrs_EmployeesClasses.DefaultProjectID = @ProjectId)
			 AND (ISNULL(@NationalityId, '') = '' OR hrs_Employees.NationalityID = @NationalityId)

group by sys_Documents.EngName,hrs_Employees.Code ,dbo.fn_GetEmpName(hrs_Employees.Code,0) 
order by EmployeeCode "

        ExecuteUpdate(SQL)

        SQL = " ALTER VIEW [dbo].[V_AllSSrequestsSendEmail]
AS
SELECT dbo.SS_VFollowup.ID, dbo.SS_VFollowup.VacationType, dbo.SS_VFollowup.RequestSerial AS Code, dbo.SS_VFollowup.EmployeeID, dbo.SS_VFollowup.RequestDate, dbo.SS_VFollowup.RequestEngName, dbo.SS_VFollowup.FormCode, 
                  dbo.SS_VFollowup.RequestArbName, dbo.SS_VFollowup.EmployeeArbName, dbo.SS_VFollowup.EmployeeEngName, dbo.SS_Configuration.UserTypeID, dbo.SS_Configuration.PositionID, 
                  CASE WHEN SS_Configuration.UserTypeID = '1' THEN Manager.WorkE_Mail ELSE CASE WHEN SS_Configuration.UserTypeID = '2' THEN Position.WorkE_Mail ELSE config.WorkE_Mail END END AS Email, 
                  CASE WHEN SS_Configuration.UserTypeID = '1' THEN Manager.EngName ELSE CASE WHEN SS_Configuration.UserTypeID = '2' THEN '' ELSE config.EngName END END AS SSEmpName
FROM     dbo.SS_VFollowup INNER JOIN
                  dbo.SS_Configuration ON dbo.SS_VFollowup.FormCode = dbo.SS_Configuration.FormCode AND dbo.SS_Configuration.Rank = 1 INNER JOIN
                  dbo.hrs_Employees AS empMang ON dbo.SS_VFollowup.EmployeeID = empMang.ID LEFT OUTER JOIN
                  dbo.hrs_Employees AS config ON dbo.SS_Configuration.EmployeeID = config.ID INNER JOIN
                  dbo.hrs_Employees AS Manager ON empMang.ManagerID = Manager.ID LEFT OUTER JOIN
                  dbo.hrs_Contracts AS PositionContract ON dbo.SS_Configuration.PositionID = PositionContract.PositionID AND dbo.SS_Configuration.Rank = 1 LEFT OUTER JOIN
                  dbo.hrs_Employees AS Position ON PositionContract.EmployeeID = Position.ID
WHERE  (dbo.SS_Configuration.Rank = 1) "
        ExecuteUpdate(SQL)

        SQL = " ALTER VIEW [dbo].[V_AllSSRequestActionsSendEmail]
AS
SELECT dbo.SS_RequestActions.ActionSerial, dbo.SS_RequestActions.RequestSerial AS Code, dbo.SS_RequestActions.SS_EmployeeID, dbo.SS_RequestActions.FormCode, dbo.SS_RequestActions.ConfigID, dbo.SS_RequestActions.ActionID, 
                  dbo.SS_RequestActions.IsHidden, dbo.SS_RequestActions.OvertimeType, CASE WHEN (dbo.SS_Configuration.UserTypeId = 2) THEN '' ELSE dbo.hrs_Employees.EngName END AS SSEmpName, dbo.hrs_Employees.ArbName, 
                  dbo.hrs_Employees.WorkE_Mail AS Email, dbo.SS_VFollowup.RequestEngName, dbo.SS_VFollowup.RequestArbName, dbo.SS_RequestActions.RequestSerial
FROM     dbo.SS_RequestActions INNER JOIN
                  dbo.hrs_Employees ON dbo.SS_RequestActions.SS_EmployeeID = dbo.hrs_Employees.ID INNER JOIN
                  dbo.SS_VFollowup ON dbo.SS_RequestActions.FormCode = dbo.SS_VFollowup.FormCode AND dbo.SS_RequestActions.RequestSerial = dbo.SS_VFollowup.ID INNER JOIN
                  dbo.SS_Configuration ON dbo.SS_RequestActions.ConfigID = dbo.SS_Configuration.ID AND dbo.SS_RequestActions.FormCode = dbo.SS_Configuration.FormCode
WHERE  (dbo.SS_RequestActions.ActionID IS NULL) AND (ISNULL(dbo.SS_RequestActions.IsHidden, 0) = 0) "
        ExecuteUpdate(SQL)
        SQL = "alter table Hrs_Employees add UpdateUserID int null
"
        ExecuteUpdate(SQL)
        SQL = " alter table Hrs_Employees add UpdateDate datetime null "
        ExecuteUpdate(SQL)
        SQL = "alter table Hrs_contracts add UpdatedUserID int null

"
        ExecuteUpdate(SQL)
        SQL = "alter table Hrs_contracts add UpdateDate datetime null"
        ExecuteUpdate(SQL)
        SQL = "ALTER TABLE dbo.Hrs_NewEmployee ADD 	CancelDate datetime NULL"
        ExecuteUpdate(SQL)

        SQL = "ALTER TABLE dbo.SS_AdvanceHousingRequest ADD InstallmentsCount int NULL"
        ExecuteUpdate(SQL)

        SQL = "alter table  sys_FiscalYearsPeriods  add PrepareFromDate date null"
        ExecuteUpdate(SQL)

        SQL = "alter table  sys_FiscalYearsPeriods  add PrepareToDate date null"
        ExecuteUpdate(SQL)
        SQL = "ALTER TABLE dbo.hrs_EndOfServices ADD ExcludedFromSSRequests bit NULL"
        ExecuteUpdate(SQL)


        SQL = "ALTER PROCEDURE [dbo].[hrs_GetPayabilitiesBalance]
	 @EmployeeID int
	AS
BEGIN
	SET NOCOUNT ON;

Set Dateformat dmy
Select 
isnull(	Sum(Amount),0)
From
(
Select 
	 (dueAmount - IsNull((Select Sum(Amount) From hrs_EmployeesPayabilitiesSchedulesSettlement HEPSS where  HEPS.ID = HEPSS.EmployeePayabilityScheduleID),0)) * sign As Amount 
From 
     hrs_EmployeesPayabilities  HEP 
     Left	Join hrs_EmployeesPayabilitiesSchedules HEPS On HEP.ID = HEPS.EmployeePayabilityID 
     Inner	Join hrs_TransactionsTypes				TT	 On TT.ID  = HEP.TransactionTypeID 
Where 
	 hep.EmployeeID = @EmployeeID
     And IsNull(HEP.CancelDate,'')=''
)SalaryTable

		
END
"
        ExecuteUpdate(SQL)


        SQL = "ALTER TABLE dbo.sys_Reports ADD ReportType nvarchar(10) NULL;"
        ExecuteUpdate(SQL)

        SQL = "ALTER TABLE dbo.sys_Reports ADD CONSTRAINT DF_sys_Reports_ReportType DEFAULT N'C1' FOR ReportType;"
        ExecuteUpdate(SQL)
        SQL = "update sys_Reports set ReportType='C1' where ReportType is null"
        ExecuteUpdate(SQL)

        SQL = "alter table hrs_VacationsBalance add LastUpdateBy int,LastUpdateDate date"
        ExecuteUpdate(SQL)
        SQL = "
Create PROCEDURE [dbo].[hrs_GetAllExpiredDocumentsExcel]
	@EmpCode varchar(50) = '', 
	@ExpireFromDate As Datetime = Null,
	@ExpireToDate As Datetime = Null,
	@DeptCode Varchar(50) = '',
	@BranchCode Varchar(50) = '',
	@DocumentTypeID Varchar(50) = '1',
    @DocumentTypesGroupID int = null,
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

        SQL = "
ALTER PROCEDURE [dbo].[hrs_GetAllExpiredDocuments]
	@EmpCode varchar(50) = '', 
	@ExpireFromDate As Datetime = Null,
	@ExpireToDate As Datetime = Null,
	@DeptCode Varchar(50) = '',
	@BranchCode Varchar(50) = '',
	@DocumentTypeID Varchar(50) = '1',
    @DocumentTypesGroupID int = null,
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

        SQL = "CREATE TABLE [dbo].[sys_DocumentTypesGroup](
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

ALTER TABLE dbo.Dwf_DocumentTypes ADD
	DocumentTypesGroupId int NULL;

ALTER TABLE dbo.sys_Documents ADD
	DocumentTypesGroupId int NULL;"
        ExecuteUpdate(SQL)

        SQL = " ALTER TABLE dbo.hrs_VacationsBalance ADD
	Posted bit NULL,
	OldExpireDate date NULL "
        ExecuteUpdate(SQL)

        SQL = " ALTER TABLE dbo.hrs_VacationsTypes ADD
	RequiredAttach bit NULL "
        ExecuteUpdate(SQL)

        SQL = " ALTER TABLE dbo.sys_SystemConfig ADD
	AllowDelayInstallmentPart bit NULL "
        ExecuteUpdate(SQL)

        SQL = " ALTER TABLE dbo.sys_SystemConfig ADD
	CompanyId int NULL "
        ExecuteUpdate(SQL)

    End Function

    Public Function UpdateSS() As Boolean
		Dim SQL As String

#Region "SS Scripts Before 01-12-2024"
        '		SQL = "
        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'SS_Configuration'
        ')
        '    BEGIN
        'CREATE TABLE [dbo].[SS_Configuration](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[FormCode] [varchar](50) NULL,
        '	[UserTypeID] [varchar](50) NULL,
        '	[PositionID] [varchar](50) NULL,
        '	[EmployeeID] [varchar](50) NULL,
        '	[CanEdit] [bit] NULL,
        '	[Rank] [int] NULL,
        '	[IsFinal] [bit] NULL,
        ' CONSTRAINT [PK_SS_Configuration] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        'End
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'SS_EndOfServiceRequest'
        ')
        '    BEGIN
        'CREATE TABLE [dbo].[SS_EndOfServiceRequest](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[EOSTypeID] [int] NULL,
        '	[EOSDate] [date] NULL,
        '	[EOSReasons] [varchar](500) NULL,
        '	[EOSRemarks] [varchar](500) NULL,
        '	[ServiceYears] [int] NULL,
        '	[SerciveMonths] [int] NULL,
        '	[ServiceDays] [int] NULL,
        ' CONSTRAINT [PK_SS_EndOfServiceRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]

        'End
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "

        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'SS_ExecuseRequest'
        ')
        '    BEGIN
        'CREATE TABLE [dbo].[SS_ExecuseRequest](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[ExecuseType] [varchar](50) NULL,
        '	[ExecuseReason] [varchar](50) NULL,
        '	[ExecuseDate] [date] NULL,
        '	[ExecuseTime] [varchar](50) NULL,
        '	[ExecuseShift] [varchar](50) NULL,
        '	[ExecuseRemarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_ExecuseRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]

        'End
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'SS_RequestActions'
        ')
        '    BEGIN
        'CREATE TABLE [dbo].[SS_RequestActions](
        '	[ActionSerial] [int] IDENTITY(1,1) NOT NULL,
        '	[RequestSerial] [int] NOT NULL,
        '	[SS_EmployeeID] [int] NULL,
        '	[FormCode] [varchar](50) NULL,
        '	[ConfigID] [int] NULL,
        '	[EmployeeID] [int] NULL,
        '	[Seen] [bit] NULL,
        '	[ActionID] [int] NULL,
        '	[ActionDate] [date] NULL,
        '	[ConfirmedNoOfdays] [int] NULL,
        '	[ActionRemarks] [nvarchar](500) NULL,
        ' CONSTRAINT [PK_RequestActions] PRIMARY KEY CLUSTERED 
        '(
        '	[ActionSerial] ASC,
        '	[RequestSerial] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        'End
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'SS_RequestTypes'
        ')
        '    BEGIN
        'CREATE TABLE [dbo].[SS_RequestTypes](
        '	[RequestID] [int] NULL,
        '	[RequestCode] [varchar](50) NULL,
        '	[RequestArbName] [varchar](50) NULL,
        '	[RequestEngName] [varchar](50) NULL
        ') ON [PRIMARY]
        'End
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'SS_UserActions'
        ')
        '    BEGIN
        'CREATE TABLE [dbo].[SS_UserActions](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[ActionCode] [nchar](10) NULL,
        '	[ActionAraName] [varchar](50) NULL,
        '	[ActionEngName] [varchar](50) NULL,
        ' CONSTRAINT [PK_SS_UserActions] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        'End
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'SS_UserTypes'
        ')
        '    BEGIN
        'CREATE TABLE [dbo].[SS_UserTypes](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[UserTypeCode] [int] NULL,
        '	[UserType] [varchar](50) NULL,
        ' CONSTRAINT [PK_SS_UserTypes] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        'End
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'SS_VacationRequest'
        ')
        '    BEGIN
        'CREATE TABLE [dbo].[SS_VacationRequest](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[VacationTypeID] [int] NOT NULL,
        '	[VacationType] [varchar](50) NULL,
        '	[RequestDate] [date] NULL,
        '	[StartDate] [date] NULL,
        '	[EndDate] [date] NULL,
        '	[TotalBalance] [int] NULL,
        '	[NoOfDays] [int] NULL,
        '	[ContactNo] [varchar](50) NULL,
        '	[AlternativeUser] [varchar](50) NULL,
        '	[Remarks] [varchar](500) NULL,
        '	[RegUser] [nvarchar](50) NULL,
        '	[RegDate] [date] NULL,
        ' CONSTRAINT [PK_SS_AnnualVacationRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC,
        '	[VacationTypeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        'End
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'Create OR ALTER     proc [dbo].[SS_rptAVRequest]

        '@RequestID varchar(50)
        'as

        'select * from
        '(
        'SELECT     SS_VacationRequest.ID as RequestID, dbo.hrs_Employees.Code AS EmpCode, isnull( dbo.hrs_Employees.ArbName,'') + ' ' + isnull(dbo.hrs_Employees.FatherArbName,'') + ' ' + isnull(dbo.hrs_Employees.GrandArbName,'') + ' ' + isnull(dbo.hrs_Employees.FamilyArbName,'') AS EmpNameArb, 
        '                         isnull(dbo.hrs_Employees.EngName,'') + ' ' + isnull(dbo.hrs_Employees.FatherEngName,'') + ' ' + isnull(dbo.hrs_Employees.GrandEngName,'') + ' ' + isnull(dbo.hrs_Employees.FamilyEngName,'') AS EmpNameEng, 
        '                         dbo.SS_VacationRequest.StartDate, dbo.SS_VacationRequest.EndDate, dbo.SS_RequestTypes.RequestArbName, dbo.SS_RequestTypes.RequestEngName, dbo.SS_VacationRequest.VacationType, 
        '                         dbo.SS_VacationRequest.Code AS DocumentNumber, dbo.SS_VacationRequest.RequestDate AS DocumentDate, dbo.sys_Departments.EngName AS DepartmentEng, dbo.sys_Departments.ArbName AS DepartmentArb, 
        '                         dbo.hrs_Employees.JoinDate, dbo.hrs_Positions.ArbName AS PositionArb, dbo.hrs_Positions.EngName AS PositionEng, dbo.SS_VacationRequest.Remarks,TotalBalance
        ',Emp.Code  as AlternativeUserCode,isnull( EMP.ArbName ,'')+ ' ' + isnull(EMP.FatherArbName ,' ')+ ' ' +isnull( EMP.GrandArbName,' ' )+ ' ' + isnull(EMP.FamilyArbName,'') AS ALternativeEmpNameArb
        ' , isnull( EMP.EngName,' ' )+ ' ' + isnull(EMP.FatherEngName,' ' )+ ' ' + isnull(EMP.GrandEngName,' ' )+ ' ' + isnull( EMP.FamilyEngName,'') AS ALternativeEmpNameEng
        ' FROM            dbo.SS_VacationRequest INNER JOIN
        '                         dbo.hrs_Employees ON dbo.SS_VacationRequest.EmployeeID = dbo.hrs_Employees.ID INNER JOIN
        '                         dbo.SS_RequestTypes ON dbo.SS_VacationRequest.VacationType = dbo.SS_RequestTypes.RequestCode INNER JOIN
        '                         dbo.sys_Departments ON dbo.hrs_Employees.DepartmentID = dbo.sys_Departments.ID INNER JOIN
        '                         dbo.hrs_Contracts ON dbo.hrs_Employees.ID = dbo.hrs_Contracts.EmployeeID INNER JOIN
        '                         dbo.hrs_Positions ON dbo.hrs_Contracts.PositionID = dbo.hrs_Positions.ID 
        '						 left join hrs_Employees EMP on SS_VacationRequest.AlternativeUser=EMP.id
        '						 where hrs_Contracts.EndDate is null or hrs_Contracts.EndDate>getdate() and SS_VacationRequest.ID=@RequestID
        '						 )T_Reuest
        '						left join
        '						 (
        '						SELECT SS_RequestActions.FormCode, SS_RequestActions.IsHidden,    	SS_VacationRequest.id, dbo.hrs_Employees.Code AS EmpCode, isnull(dbo.hrs_Employees.ArbName,'') + ' ' + isnull(dbo.hrs_Employees.FatherArbName,'') + ' ' + isnull(dbo.hrs_Employees.GrandArbName,'') + ' ' + isnull(dbo.hrs_Employees.FamilyArbName,'') AS EmpNameArb2, 
        '                         isnull(dbo.hrs_Employees.EngName,'') + ' ' + isnull(dbo.hrs_Employees.FatherEngName,'') + ' ' + isnull(dbo.hrs_Employees.GrandEngName,'') + ' ' + isnull(dbo.hrs_Employees.FamilyEngName,'') AS EmpNameEng2, 
        '                         dbo.SS_VacationRequest.Code AS DocumentNumber, dbo.hrs_Positions.ArbName AS PositionArb2, dbo.hrs_Positions.EngName AS PositionEng2, dbo.SS_RequestActions.ActionRemarks, dbo.SS_UserActions.ActionAraName, 
        '                         dbo.SS_UserActions.ActionEngName, dbo.SS_RequestActions.ActionSerial
        'FROM            dbo.SS_RequestActions INNER JOIN
        '                         dbo.SS_VacationRequest ON dbo.SS_RequestActions.RequestSerial = dbo.SS_VacationRequest.ID INNER JOIN
        '                         dbo.hrs_Employees INNER JOIN
        '                         dbo.hrs_Contracts ON dbo.hrs_Employees.ID = dbo.hrs_Contracts.EmployeeID  INNER JOIN
        '                         dbo.hrs_Positions ON dbo.hrs_Contracts.PositionID = dbo.hrs_Positions.ID ON dbo.SS_RequestActions.SS_EmployeeID = dbo.hrs_Employees.ID left JOIN
        '                         dbo.SS_UserActions ON dbo.SS_RequestActions.ActionID = dbo.SS_UserActions.ID
        '					where (SS_RequestActions.FormCode='SS_0011' or SS_RequestActions.FormCode='SS_0012' or SS_RequestActions.FormCode ='SS_0013')and
        '					hrs_Contracts.EndDate is null or hrs_Contracts.EndDate>getdate() and (SS_RequestActions.IsHidden<>1 or SS_RequestActions.IsHidden is null)
        '						 )T_Action
        '						 on T_Reuest.DocumentNumber= T_Action.DocumentNumber


        'where T_Reuest.RequestID=@RequestID and (T_Action.IsHidden<>1 or T_Action.IsHidden is null)
        'and (T_Action.FormCode='SS_0011' or T_Action.FormCode='SS_0012' or T_Action.FormCode ='SS_0013')
        ' order by T_Action.ActionSerial
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'create or ALTER proc [dbo].[SS_rptExecuseRequest]


        '@RequestID varchar(50)
        'as
        'select * from
        '(
        'SELECT       SS_ExecuseRequest.ID as RequestID, dbo.hrs_Employees.Code AS EmpCode, [dbo].[fn_GetEmpName](hrs_Employees.Code,1) AS EmpNameArb, 
        '                         [dbo].[fn_GetEmpName](hrs_Employees.Code,0) AS EmpNameEng, 

        '                         dbo.SS_ExecuseRequest.Code AS DocumentNumber,dbo.SS_ExecuseRequest.ExecuseDate , dbo.SS_ExecuseRequest.RequestDate AS DocumentDate, dbo.sys_Departments.EngName AS DepartmentEng, dbo.sys_Departments.ArbName AS DepartmentArb, 
        '                         dbo.hrs_Employees.JoinDate, dbo.hrs_Positions.ArbName AS PositionArb, dbo.hrs_Positions.EngName AS PositionEng, dbo.SS_ExecuseRequest.ExecuseRemarks,SS_ExecuseRequest.ExecuseReason as ExecuseReasonEng,case when SS_ExecuseRequest.ExecuseReason='Sick'then 'عذر مرضي' when SS_ExecuseRequest.ExecuseReason='Business'then 'عذر عمل' when SS_ExecuseRequest.ExecuseReason='Personal'then'عذر شخصي' end as ExecuseReasonArb ,case when SS_ExecuseRequest.ExecuseShift='1'then 'First Shift' else 'Second Shift' end as ExecuseShiftEng,case when SS_ExecuseRequest.ExecuseShift='1'then 'الوردية الاولي' else 'الوردية الثانية' end as ExecuseShiftArb ,SS_ExecuseRequest.ExecuseTime,SS_ExecuseRequest.ExecuseType as ExecuseTypeEng,
        '						 case when SS_ExecuseRequest.ExecuseType ='IN'then 'إذن دخول' when SS_ExecuseRequest.ExecuseType ='Out'then'إذن خروج'when SS_ExecuseRequest.ExecuseType ='Full'then 'كامل الدوام'end as ExecuseTypeArb
        ' FROM            dbo.SS_ExecuseRequest INNER JOIN
        '                         dbo.hrs_Employees ON dbo.SS_ExecuseRequest.EmployeeID = dbo.hrs_Employees.ID INNER JOIN

        '                         dbo.sys_Departments ON dbo.hrs_Employees.DepartmentID = dbo.sys_Departments.ID INNER JOIN
        '                         dbo.hrs_Contracts ON dbo.hrs_Employees.ID = dbo.hrs_Contracts.EmployeeID INNER JOIN
        '                         dbo.hrs_Positions ON dbo.hrs_Contracts.PositionID = dbo.hrs_Positions.ID
        '						  where hrs_Contracts.EndDate is null or hrs_Contracts.EndDate>getdate()
        '						 )T_Reuest
        '						 left join
        '						 (
        '						SELECT        dbo.hrs_Employees.Code AS EmpCode,  [dbo].[fn_GetEmpName](hrs_Employees.Code,1)   AS EmpNameArb2, 
        '                          [dbo].[fn_GetEmpName](hrs_Employees.Code,0) AS  EmpNameEng2, 
        '                         dbo.SS_ExecuseRequest.Code AS DocumentNumber, dbo.hrs_Positions.ArbName AS PositionArb2, dbo.hrs_Positions.EngName AS PositionEng2, dbo.SS_RequestActions.ActionRemarks, dbo.SS_UserActions.ActionAraName, 
        '                         dbo.SS_UserActions.ActionEngName, dbo.SS_RequestActions.ActionSerial
        'FROM            dbo.SS_RequestActions INNER JOIN
        '                         dbo.SS_ExecuseRequest ON dbo.SS_RequestActions.RequestSerial = dbo.SS_ExecuseRequest.ID INNER JOIN
        '                         dbo.hrs_Employees INNER JOIN
        '                         dbo.hrs_Contracts ON dbo.hrs_Employees.ID = dbo.hrs_Contracts.EmployeeID INNER JOIN
        '                         dbo.hrs_Positions ON dbo.hrs_Contracts.PositionID = dbo.hrs_Positions.ID ON dbo.SS_RequestActions.SS_EmployeeID = dbo.hrs_Employees.ID left JOIN
        '                         dbo.SS_UserActions ON dbo.SS_RequestActions.ActionID = dbo.SS_UserActions.ID
        '						 where SS_RequestActions.FormCode='SS_0014'
        '						 		and( hrs_Contracts.EndDate is null or hrs_Contracts.EndDate>getdate()) and (SS_RequestActions.IsHidden<>1 or SS_RequestActions.IsHidden is null)
        '						 )T_Action
        '						 on T_Reuest.DocumentNumber= T_Action.DocumentNumber  


        'where T_Reuest.RequestID=@RequestID
        ' order by T_Action.ActionSerial
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'create or ALTER proc [dbo].[SS_rptEOSRequest]


        '@RequestID varchar(50)
        'as
        'select * from
        '(
        'SELECT       SS_EndOfServiceRequest.ID as RequestID, dbo.hrs_Employees.Code AS EmpCode, [dbo].[fn_GetEmpName](hrs_Employees.Code,1)AS EmpNameArb, 
        '                         [dbo].[fn_GetEmpName](hrs_Employees.Code,0) AS EmpNameEng, 

        '                         dbo.SS_EndOfServiceRequest.Code AS DocumentNumber,dbo.SS_EndOfServiceRequest.EOSDate , dbo.SS_EndOfServiceRequest.RequestDate AS DocumentDate, dbo.sys_Departments.EngName AS DepartmentEng, dbo.sys_Departments.ArbName AS DepartmentArb, 
        '                         dbo.hrs_Employees.JoinDate, dbo.hrs_Positions.ArbName AS PositionArb, dbo.hrs_Positions.EngName AS PositionEng, dbo.SS_EndOfServiceRequest.EOSRemarks,SS_EndOfServiceRequest.EOSReasons
        ' FROM            dbo.SS_EndOfServiceRequest INNER JOIN
        '                         dbo.hrs_Employees ON dbo.SS_EndOfServiceRequest.EmployeeID = dbo.hrs_Employees.ID INNER JOIN

        '                         dbo.sys_Departments ON dbo.hrs_Employees.DepartmentID = dbo.sys_Departments.ID INNER JOIN
        '                         dbo.hrs_Contracts ON dbo.hrs_Employees.ID = dbo.hrs_Contracts.EmployeeID INNER JOIN
        '                         dbo.hrs_Positions ON dbo.hrs_Contracts.PositionID = dbo.hrs_Positions.ID
        '						 where hrs_Contracts.EndDate is null or hrs_Contracts.EndDate>getdate()
        '						 )T_Reuest
        '						left join
        '						 (
        '						SELECT        dbo.hrs_Employees.Code AS EmpCode, [dbo].[fn_GetEmpName](hrs_Employees.Code,1) AS EmpNameArb2, 
        '                         [dbo].[fn_GetEmpName](hrs_Employees.Code,0) AS EmpNameEng2, 
        '                         dbo.SS_EndOfServiceRequest.Code AS DocumentNumber, dbo.hrs_Positions.ArbName AS PositionArb2, dbo.hrs_Positions.EngName AS PositionEng2, dbo.SS_RequestActions.ActionRemarks, dbo.SS_UserActions.ActionAraName, 
        '                         dbo.SS_UserActions.ActionEngName, dbo.SS_RequestActions.ActionSerial
        'FROM            dbo.SS_RequestActions INNER JOIN
        '                         dbo.SS_EndOfServiceRequest ON dbo.SS_RequestActions.RequestSerial = dbo.SS_EndOfServiceRequest.ID INNER JOIN
        '                         dbo.hrs_Employees INNER JOIN
        '                         dbo.hrs_Contracts ON dbo.hrs_Employees.ID = dbo.hrs_Contracts.EmployeeID INNER JOIN
        '                         dbo.hrs_Positions ON dbo.hrs_Contracts.PositionID = dbo.hrs_Positions.ID ON dbo.SS_RequestActions.SS_EmployeeID = dbo.hrs_Employees.ID left JOIN
        '                         dbo.SS_UserActions ON dbo.SS_RequestActions.ActionID = dbo.SS_UserActions.ID
        '						 						 where SS_RequestActions.FormCode='SS_0015'
        '												and( hrs_Contracts.EndDate is null or hrs_Contracts.EndDate>getdate()) and (SS_RequestActions.IsHidden<>1 or SS_RequestActions.IsHidden is null)
        '						 )T_Action
        '						 on T_Reuest.DocumentNumber= T_Action.DocumentNumber

        'where T_Reuest.RequestID=@RequestID
        ' order by T_Action.ActionSerial

        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF Not EXISTS(SELECT 1 FROM sys.columns 
        '          WHERE Name = N'ApplyForAll'
        '          AND Object_ID = Object_ID(N'dbo.SS_Configuration'))
        'BEGIN

        'alter table SS_Configuration add ApplyForAll bit null
        'END
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF Not EXISTS(SELECT 1 FROM sys.columns 
        '          WHERE Name = N'IsHidden'
        '          AND Object_ID = Object_ID(N'dbo.SS_RequestActions'))
        'BEGIN

        'alter table dbo.SS_RequestActions add IsHidden bit null
        'END
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF Not EXISTS(SELECT 1 FROM sys.columns 
        '          WHERE Name = N'FormCode'
        '          AND Object_ID = Object_ID(N'dbo.SS_EndOfServiceRequest'))
        'BEGIN

        'alter table SS_EndOfServiceRequest add FormCode varchar(50)
        'END
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF NOT EXISTS
        '(
        '    SELECT name
        '    FROM sysobjects
        '    WHERE name = 'SS_VariousRequestTypes'
        ')
        '    BEGIN
        'CREATE TABLE [dbo].[SS_VariousRequestTypes](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NULL,
        '	[EngName] [varchar](50) NULL,
        '	[ArbName] [varchar](50) NULL,
        '	[ReportName] [nvarchar](50) NULL,
        ' CONSTRAINT [PK_SS_VariousRequestTypes] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        'End
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'ALTER TABLE [SS_VacationRequest] ALTER COLUMN [TotalBalance] decimal(18,2)
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF COL_LENGTH('SS_RequestTypes', 'NotActive') IS NULL
        'BEGIN
        '    ALTER TABLE SS_RequestTypes
        '    ADD NotActive bit
        'END
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF Not EXISTS(SELECT 1 FROM sys.columns 
        '          WHERE Name = N'NoOfTimes'
        '          AND Object_ID = Object_ID(N'dbo.SS_RequestTypes'))
        'BEGIN

        'alter table SS_RequestTypes add NoOfTimes int null
        'END
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'IF Not EXISTS(SELECT 1 FROM sys.columns 
        '          WHERE Name = N'TimesPeriodPerMonth'
        '          AND Object_ID = Object_ID(N'dbo.SS_RequestTypes'))
        'BEGIN

        'alter table SS_RequestTypes add TimesPeriodPerMonth int null
        'END
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'CREATE   TABLE [dbo].[SS_ExitEntryRequest](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_ExitEntryRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'CREATE   TABLE [dbo].[SS_VisaRequest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_VisaRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' CREATE   TABLE [dbo].[SS_LoanLetterRequest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_LoanLetterRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' CREATE   TABLE [dbo].[SS_OtherLetterRequest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_OtherLetterRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' CREATE   TABLE [dbo].[SS_TrainingRequest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_TrainingRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' CREATE   TABLE [dbo].[SS_GrievanceFormRequest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_GrievanceFormRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' CREATE   TABLE [dbo].[SS_InterviewEvaluationFormRequest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_InterviewEvaluationFormRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' CREATE   TABLE [dbo].[SS_AssaultEscalationFormRequest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_AssaultEscalationFormRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' CREATE   TABLE [dbo].[SS_ConflictofInterestFormRequest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_ConflictofInterestFormRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' CREATE   TABLE [dbo].[SS_PhysiciansPrivilegingFormRequest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_PhysiciansPrivilegingFormRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' CREATE   TABLE [dbo].[SS_DaycareSupportReaquest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_DaycareSupportReaquest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        ' CREATE   TABLE [dbo].[SS_EducationSupportRequest]
        '(
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_EducationSupportRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        '--=============Advance Housing Request==================
        'CREATE TABLE [dbo].[SS_AdvanceHousingRequest](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_AdvanceHousingRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        '--=============Advance Salary Request==================

        'CREATE TABLE [dbo].[SS_AdvanceSalaryRequest](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_AdvanceSalaryRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        '--====================ChamberofCommerceLetterRequest========================
        'CREATE TABLE [dbo].[SS_ChamberofCommerceLetterRequest](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_ChamberofCommerceLetterRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        '--=================SCFHSLetterRequest=====طلب خطاب تصنيف هيئة التخصصات الطبية==============

        'CREATE TABLE [dbo].[SS_SCFHSLetterRequest](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_SCFHSLetterRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        '--=================Pay Slip Request==================

        'CREATE TABLE [dbo].[SS_PaySlipRequest](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_PaySlipRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        '--==================  OccurrenceVarianceReportingLetters ===========================
        'Create TABLE [dbo].[SS_OccurrenceVarianceReportingLetters](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_OccurrenceVarianceReportingLetters] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        '--===================  OvertimeRequest ==========================
        'CREATE TABLE [dbo].[SS_OvertimeRequest](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_OvertimeRequest] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        '--==================EducationFeesCompensationApplication ===========================
        'CREATE TABLE [dbo].[SS_EducationFeesCompensationApplication](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_EducationFeesCompensationApplication] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        '--========================BankAccountUpdate=====================
        'CREATE TABLE [dbo].[SS_BankAccountUpdate](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_BankAccountUpdate] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		'New
        '		SQL = "
        '		CREATE TABLE [dbo].[SS_ContactInformationUpdate](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_ContactInformationUpdate] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'CREATE TABLE [dbo].[SS_DependentsInformationUpdate](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_DependentsInformationUpdate] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'CREATE TABLE [dbo].[SS_MedicalInsuranceAdjustments](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_MedicalInsuranceAdjustments] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'CREATE TABLE [dbo].[SS_OtherLegalDocumentUpdates](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_OtherLegalDocumentUpdates] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'CREATE TABLE [dbo].[SS_EmployeeFileUpdate](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_EmployeeFileUpdate] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'CREATE TABLE [dbo].[SS_BusinessORTrainingTravel](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_BusinessORTrainingTravel] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		SQL = "
        'CREATE TABLE [dbo].[SS_AnnualTicketRelatedRequests](
        '	[ID] [int] IDENTITY(1,1) NOT NULL,
        '	[Code] [varchar](50) NOT NULL,
        '	[EmployeeID] [int] NOT NULL,
        '	[RequestDate] [date] NULL,
        '	[Remarks] [varchar](5000) NULL,
        ' CONSTRAINT [PK_SS_AnnualTicketRelatedRequests] PRIMARY KEY CLUSTERED 
        '(
        '	[ID] ASC,
        '	[Code] ASC,
        '	[EmployeeID] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY]
        '"
        '		ExecuteUpdate(SQL)
        '		'New
        '		SQL = "


        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1943, N'SS_ExecuseRequest', N'SS_ExecuseRequest', N'طلب استئذان', NULL, CAST(N'2024-12-08T07:16:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1944, N'SS_EndOfServiceRequest', N'SS_EndOfServiceRequest', N'طلب انهاء خدمة', NULL, CAST(N'2024-12-08T07:41:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1945, N'SS_ExitEntryRequest', N'SS_ExitEntryRequest', N'طلب الخروج والعودة', NULL, CAST(N'2024-12-08T07:53:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1946, N'SS_VisaRequest', N'SS_VisaRequest', N'طلب تأشيرة زيارة', NULL, CAST(N'2024-12-08T07:56:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1947, N'SS_LoanLetterRequest', N'SS_LoanLetterRequest', N'طلب خطاب قرض', NULL, CAST(N'2024-12-08T07:57:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1948, N'SS_OtherLetterRequest', N'SS_OtherLetterRequest', N'طلب خطاب اخر', NULL, CAST(N'2024-12-08T07:59:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1949, N'SS_TrainingRequest', N'SS_TrainingRequest', N'طلب تدريب', NULL, CAST(N'2024-12-08T08:00:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1950, N'SS_GrievanceFormRequest', N'SS_GrievanceFormRequest', N'طلب اسماة تظلم', NULL, CAST(N'2024-12-08T08:02:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1951, N'SS_InterviewEvaluationFormRequest', N'SS_InterviewEvaluationFormRequest', N'طلب استمارة تقييم المقابلة', NULL, CAST(N'2024-12-08T08:03:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1952, N'SS_AssaultEscalationFormRequest', N'SS_AssaultEscalationFormRequest', N'طلب استمارة تصعيد حالات الاعتداء', NULL, CAST(N'2024-12-08T08:05:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1953, N'SS_ConflictofInterestFormRequest', N'SS_ConflictofInterestFormRequest', N'طلب اسمارة تضارب المصالح', NULL, CAST(N'2024-12-08T08:07:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1954, N'SS_PhysiciansPrivilegingFormRequest', N'SS_PhysiciansPrivilegingFormRequest', N'طلب استمارة امتيازات الاطباء', NULL, CAST(N'2024-12-08T08:08:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1955, N'SS_DaycareSupportReaquest', N'SS_DaycareSupportReaquest', N'طلب دعم رعاية الاطفال', NULL, CAST(N'2024-12-08T08:09:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1956, N'SS_EducationSupportRequest', N'SS_EducationSupportRequest', N'طلب دعم تعليم', NULL, CAST(N'2024-12-08T08:13:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1957, N'SS_AdvanceHousingRequest', N'SS_AdvanceHousingRequest', N'طلب بدل سكن', NULL, CAST(N'2024-12-08T08:20:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1958, N'SS_ChamberofCommerceLetterRequest', N'SS_ChamberofCommerceLetterRequest', N'طلب خطاب الغرفة التجارية', NULL, CAST(N'2024-12-08T08:24:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1959, N'SS_SCFHSLetterRequest', N'SS_SCFHSLetterRequest', N'طلب خطاب تصنيف هيئة لتخصات الطبية', NULL, CAST(N'2024-12-08T08:26:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1960, N'SS_PaySlipRequest', N'SS_PaySlipRequest', N'طلب خطاب تعريف الراتب', NULL, CAST(N'2024-12-08T08:27:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1961, N'SS_OccurrenceVarianceReportingLetters', N'SS_OccurrenceVarianceReportingLetters', N'طلبات الابلاغ عن التباين', NULL, CAST(N'2024-12-08T08:29:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1962, N'SS_OvertimeRequest', N'SS_OvertimeRequest', N'طلب الوقت الضافي', NULL, CAST(N'2024-12-08T08:32:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1963, N'SS_EducationFeesCompensationApplication', N'SS_EducationFeesCompensationApplication', N'طلب تعويض رسوم التعليم', NULL, CAST(N'2024-12-08T08:32:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1964, N'SS_BankAccountUpdate', N'SS_BankAccountUpdate', N'طلب تحديث حساب البنك', NULL, CAST(N'2024-12-08T08:33:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1965, N'SS_ContactInformationUpdate', N'SS_ContactInformationUpdate', N'طلب تحديث معلومات الاتصال', NULL, CAST(N'2024-12-08T08:35:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1966, N'SS_DependentsInformationUpdate', N'SS_DependentsInformationUpdate', N'طلب تحديث معلومات المعالين', NULL, CAST(N'2024-12-08T08:36:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1967, N'SS_MedicalInsuranceAdjustments', N'SS_MedicalInsuranceAdjustments', N'طلب تعديل التامين الطبي', NULL, CAST(N'2024-12-08T08:38:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1968, N'SS_OtherLegalDocumentUpdates', N'SS_OtherLegalDocumentUpdates', N'طلب تحديثات المستندات القانونية الأخرى', NULL, CAST(N'2024-12-08T08:40:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1969, N'SS_EmployeeFileUpdate', N'SS_EmployeeFileUpdate', N'طلب تحديث ملف الموظف', NULL, CAST(N'2024-12-08T08:41:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1970, N'SS_BusinessORTrainingTravel', N'SS_BusinessORTrainingTravel', N'طلب سفر عمل او تدريب', NULL, CAST(N'2024-12-08T08:43:00' AS SmallDateTime), NULL)
        '"
        '        ExecuteUpdate(SQL)

        '        SQL = "
        'INSERT [dbo].[sys_Objects] ([ID], [Code], [EngName], [ArbName], [IsFiscalYearClosable], [RegDate], [CancelDate]) VALUES (1971, N'SS_AnnualTicketRelatedRequests', N'SS_AnnualTicketRelatedRequests', N'الطلبات المتعلقة بالتذاكر السنوية', NULL, CAST(N'2024-12-08T08:44:00' AS SmallDateTime), NULL)
        '		"
        '        ExecuteUpdate(SQL)

#End Region

        SQL = "
IF NOT EXISTS
(
    SELECT name
    FROM sysobjects
    WHERE name = 'SS_DelegationSChedule'
)
    BEGIN
        CREATE TABLE [dbo].[SS_DelegationSChedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[DelegatorEmployeeID] [int] NULL,
	[DelegatedEmployeeID] [int] NULL,
	[FromDate] [date] NULL,
	[Todate] [date] NULL,
	[Remarks] [nvarchar](500) NULL,
	[IsCanceled] [bit] NULL,
	[CancelDate] [date] NULL,
	[RegUserID] [nvarchar](50) NULL,
	[RegDate] [date] NULL,
 CONSTRAINT [PK_SS_DelegationSChedule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
	END
"
        ExecuteUpdate(SQL)

        SQL = "
IF NOT EXISTS
(
    SELECT name
    FROM sysobjects
    WHERE name = 'SS_DelegationSCheduleRequests'
)
    BEGIN
       CREATE TABLE [dbo].[SS_DelegationSCheduleRequests](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ScheduleId] [int] NULL,
	[RequestTypeId] [nvarchar](50) NULL,
	[RegUserID] [nvarchar](50) NULL,
	[RegComputerID] [nvarchar](50) NULL,
	[CancelDate] [datetime] NULL,
	[RegDate] [datetime] NULL,
 CONSTRAINT [PK_SS_DelegationSCheduleRequests] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
	END
"
        ExecuteUpdate(SQL)

        SQL = "
       ALTER TABLE dbo.SS_BankAccountUpdate ADD
	BankID int NULL,
	BankAccountNumber varchar(100) NULL
        "
        ExecuteUpdate(SQL)

        SQL = "
       ALTER TABLE dbo.SS_EndOfServiceRequest ADD
	ResignationReasonCode int NULL,
	DepartmentRateCode int NULL,
	OtherResignationReason nvarchar(500) NULL,
	UsRateCode int NULL,
	CanBack bit NULL
        "
        ExecuteUpdate(SQL)

        SQL = "
       
CREATE TABLE [dbo].[SS_ExperienceRate](
	[Code] [int] NULL,
	[ArbName] [nvarchar](100) NULL,
	[EngName] [nvarchar](100) NULL
) ON [PRIMARY]
        "
        ExecuteUpdate(SQL)

        SQL = "
       CREATE TABLE [dbo].[SS_ResignationReason](
	[Code] [int] NOT NULL,
	[ArbName] [nvarchar](100) NULL,
	[EngName] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_SS_ResignationReason] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
        "
        ExecuteUpdate(SQL)


        SQL = "
       INSERT [dbo].[SS_ExperienceRate] ([Code], [ArbName], [EngName]) VALUES (1, N'غير راضٍ على الإطلاق', N'Extremely not Satisfied')
GO
INSERT [dbo].[SS_ExperienceRate] ([Code], [ArbName], [EngName]) VALUES (2, N'غير راضي', N'Not Satisfied')
GO
INSERT [dbo].[SS_ExperienceRate] ([Code], [ArbName], [EngName]) VALUES (3, N'لا بأس', N'It is OK')
GO
INSERT [dbo].[SS_ExperienceRate] ([Code], [ArbName], [EngName]) VALUES (4, N'راضي', N'Satisfied')
GO
INSERT [dbo].[SS_ExperienceRate] ([Code], [ArbName], [EngName]) VALUES (5, N'راضي للغاية', N'Extremely Satisfied')
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (1, N'أجر أعلى / فوائد أفضل', N'Higher Pay / Better Benefits', NULL)
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (2, N'فرصة وظيفية أفضل / تغيير وظيفي', N'Better Career Opportunity / Career Change', NULL)
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (3, N'عدم الرضا عن المدير', N'Dissatisfaction Manager', NULL)
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (4, N'عدم الرضا عن زميل', N'Dissatisfaction Colleague', NULL)
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (5, N'ظروف العمل والبيئة', N'Work Conditions & Environment', NULL)
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (6, N'الحمل / الأبوة والأمومة أو احتياجات الأسرة', N'Pregnancy/Parenting or family needs', NULL)
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (7, N'أسباب شخصية', N'Personal reasons', NULL)
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (8, N'زواج', N'Marriage', NULL)
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (9, N'مزيد من الدراسة', N'Further Study', NULL)
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (10, N'التقاعد', N'Retirement', NULL)
GO
INSERT [dbo].[SS_ResignationReason] ([Code], [ArbName], [EngName], [Description]) VALUES (11, N'سبب آخر', N'Other Reason', NULL)
        "
        ExecuteUpdate(SQL)

        SQL = "
       ALTER TABLE dbo.SS_RequestTypes ADD
	AutoSerialAttach bit NULL
        "
        ExecuteUpdate(SQL)


        SQL = "
       ALTER TABLE dbo.SS_Configuration ADD
	CanBeCanceledInThisLevel bit NULL
        "
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.SS_ContactInformationUpdate ADD
	E_Mail varchar(255) NULL,
	Phone varchar(100) NULL,
	Mobile varchar(100) NULL,
	AddressAsPerContract varchar(500) NULL
"
        ExecuteUpdate(SQL)

        SQL = "
       CREATE TABLE [dbo].[SS_RequestStatuesTypes](
	[ID] [int] NOT NULL,
	[AraName] [nvarchar](50) NULL,
	[EngName] [nvarchar](50) NULL,
 CONSTRAINT [PK_SS_RequestStatues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
        "
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE SS_AdvanceHousingRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE SS_AdvanceSalaryRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE SS_AnnualTicketRelatedRequests ADD
	TicketDate Date NULL
	        "
        ExecuteUpdate(SQL)
        SQL = "
ALTER TABLE SS_AnnualTicketRelatedRequests ADD
	Direction int NULL
	        "
        ExecuteUpdate(SQL)   
        SQL = "
ALTER TABLE SS_AnnualTicketRelatedRequests ADD
	NoOfAdults int NULL
	        "
        ExecuteUpdate(SQL)  
        SQL = "
ALTER TABLE SS_AnnualTicketRelatedRequests ADD
	NoOfChildren int NULL
	        "
        ExecuteUpdate(SQL)
        SQL = "
ALTER TABLE SS_AnnualTicketRelatedRequests ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)
        SQL = "
	ALTER TABLE SS_AssaultEscalationFormRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_BankAccountUpdate ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_BusinessORTrainingTravel ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_ChamberofCommerceLetterRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_ConflictofInterestFormRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_ContactInformationUpdate ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_DaycareSupportReaquest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_DependentsInformationUpdate ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_EducationFeesCompensationApplication ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_EducationSupportRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_EmployeeFileUpdate ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_EndOfServiceRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_ExecuseRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_ExitEntryRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_GrievanceFormRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_InterviewEvaluationFormRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_LoanLetterRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_MedicalInsuranceAdjustments ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
		ALTER TABLE SS_OccurrenceVarianceReportingLetters ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_OtherLegalDocumentUpdates ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_OtherLetterRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_OvertimeRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_PaySlipRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_PhysiciansPrivilegingFormRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
		ALTER TABLE SS_SCFHSLetterRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_TrainingRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_VacationRequest ADD
	RequestStautsTypeID int NULL
	        "
        ExecuteUpdate(SQL)

        SQL = "
	ALTER TABLE SS_VisaRequest ADD
	RequestStautsTypeID int NULL
	"
        ExecuteUpdate(SQL)

        SQL = "
INSERT INTO [dbo].[SS_RequestStatuesTypes]
           ([ID]
           ,[AraName]
           ,[EngName])
     VALUES
           (1
           ,'موافقة'
           ,'Accepted')
		    "
        ExecuteUpdate(SQL)

        SQL = "
INSERT INTO [dbo].[SS_RequestStatuesTypes]
           ([ID]
           ,[AraName]
           ,[EngName])
     VALUES
           (2
           ,'مرفوضة'
           ,'Rejected')
		    "
        ExecuteUpdate(SQL)

        SQL = "
INSERT INTO [dbo].[SS_RequestStatuesTypes]
           ([ID]
           ,[AraName]
           ,[EngName])
     VALUES
           (3
           ,'جديد'
           ,'New')
 "
        ExecuteUpdate(SQL)

        SQL = "
		   INSERT INTO [dbo].[SS_RequestStatuesTypes]
           ([ID]
           ,[AraName]
           ,[EngName])
     VALUES
           (4
           ,'تحت الاجراء'
           ,'Under procedure')
		    "
        ExecuteUpdate(SQL)

        SQL = "
		   INSERT INTO [dbo].[SS_RequestStatuesTypes]
           ([ID]
           ,[AraName]
           ,[EngName])
     VALUES
           (5
           ,'ملغي'
           ,'Canceled')
		    "
        ExecuteUpdate(SQL)


        SQL = "CREATE view [dbo].[V_CancelAnnualVacationsReqouestToEmail]
as 
SELECT dbo.SS_RequestActions.RequestSerial as ID  ,dbo.fn_GetEmpName(ReqEMP.Code,0) as RequesterName, dbo.SS_VacationRequest.Code, dbo.SS_RequestActions.FormCode, dbo.SS_RequestActions.SS_EmployeeID, dbo.hrs_Employees.WorkE_Mail
FROM     dbo.SS_RequestActions INNER JOIN
                  dbo.hrs_Employees ON dbo.SS_RequestActions.SS_EmployeeID = dbo.hrs_Employees.ID INNER JOIN
                  dbo.SS_VacationRequest ON dbo.SS_RequestActions.RequestSerial = dbo.SS_VacationRequest.ID
				join hrs_Employees ReqEMP
				on SS_RequestActions.EmployeeID=ReqEMP.ID
				where SS_RequestActions.ActionID is not null
				and ActionID<>4	
                "
        ExecuteUpdate(SQL)
        SQL = "
create   view [dbo].[V_AnnualvacationAction_EmialToRequester]
as
select ActionSerial As ID ,RequestSerial ,E.WorkE_Mail,VR.Code as RequestCode
,case
when
ActionID=1 then 'Accepted'
when ActionID=2 then 'Rejected'
when ActionID=4 then 'Delegated'
end as Action
,dbo.fn_GetEmpName(SSEMP.code,0) as RequestActionBy
from SS_RequestActions  SS join hrs_Employees E on SS.EmployeeID=E.ID  
join SS_VacationRequest VR
on SS.RequestSerial=VR.ID
join hrs_Employees SSEMP
on SSEMP.id=SS.SS_EmployeeID
      "
        ExecuteUpdate(SQL)

        SQL = "
Create or ALTER VIEW [dbo].[SS_VFollowup]
AS
/*-1==============================الاجازة==================*/ SELECT SS_VacationRequest.ID, SS_VacationRequest.VacationType, SS_VacationRequest.Code AS RequestSerial, SS_VacationRequest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_VacationRequest.RequestDate, 
                         CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي اداري' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                         SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي طبي' ELSE hrs_VacationsTypes.ArbName4S END AS RequestArbName, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                         SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Admin' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                         SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Medical' ELSE hrs_VacationsTypes.EngName END AS RequestEngName, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                         SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0011' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                         SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0012' WHEN SS_VacationRequest.VacationType = 'SS_0018' THEN 'SS_0018' ELSE 'SS_0013' END AS FormCode
,RequestStautsTypeID  From            SS_VacationRequest JOIN
                         hrs_Employees ON SS_VacationRequest.EmployeeID = hrs_Employees.id JOIN
                         hrs_VacationsTypes ON SS_VacationRequest.VacationTypeID = hrs_VacationsTypes.ID INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*-2==============================الاستئذان=======================================================*/ UNION
SELECT        SS_ExecuseRequest.ID, '' AS VacationType, SS_ExecuseRequest.Code AS RequestSerial, SS_ExecuseRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_ExecuseRequest.RequestDate, 'طلب استئذان' AS RequestArbName, 'Execuse Request' AS RequestEngName, 'SS_0014' AS FormCode
,RequestStautsTypeID  From            SS_ExecuseRequest JOIN
                         hrs_Employees ON SS_ExecuseRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*3 ============================انهاء خدمة ===============================================================*/ UNION
SELECT        SS_EndOfServiceRequest.ID, '' AS VacationType, SS_EndOfServiceRequest.Code AS RequestSerial, SS_EndOfServiceRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                         1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_EndOfServiceRequest.RequestDate, CASE WHEN FormCode = 'SS_0015' THEN 'طلب انهاء خدمة' ELSE 'طلب انهاء خدمة طبي' END AS RequestArbName, 
                         CASE WHEN FormCode = 'SS_0015' THEN 'End Of Service Request' ELSE 'End Of Service Medical Request' END AS RequestEngName, FormCode AS FormCode
,RequestStautsTypeID  From            SS_EndOfServiceRequest JOIN
                         hrs_Employees ON SS_EndOfServiceRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*4============================خروج وعودة ==============================================*/ UNION
SELECT        SS_ExitEntryRequest.ID, '' AS VacationType, SS_ExitEntryRequest.Code AS RequestSerial, SS_ExitEntryRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_ExitEntryRequest.RequestDate, 'طلب خروج وعودة' AS RequestArbName, 'Exit & Re-entry Requests' AS RequestEngName, 'SS_00191' AS FormCode
,RequestStautsTypeID  From            SS_ExitEntryRequest JOIN
                         hrs_Employees ON SS_ExitEntryRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*5============================ تأشيرة الأسرة أو تأشيرة الزيارة ==============================================*/ UNION
SELECT        SS_VisaRequest.ID, '' AS VacationType, SS_VisaRequest.Code AS RequestSerial, SS_VisaRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_VisaRequest.RequestDate, 'طلب تأشيرة الأسرة أو تأشيرة الزيارة' AS RequestArbName, 'Family visa or Visit visa Requests' AS RequestEngName, 'SS_00192' AS FormCode
,RequestStautsTypeID  From            SS_VisaRequest JOIN
                         hrs_Employees ON SS_VisaRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*6============================  خطاب قرض ==============================================*/ UNION
SELECT        SS_LoanLetterRequest.ID, '' AS VacationType, SS_LoanLetterRequest.Code AS RequestSerial, SS_LoanLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_LoanLetterRequest.RequestDate, 'طلب خطاب قرض' AS RequestArbName, 'Loan Letter Request' AS RequestEngName, 'SS_00193' AS FormCode
,RequestStautsTypeID  From            SS_LoanLetterRequest JOIN
                         hrs_Employees ON SS_LoanLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*7============================   خطاب آخر ==============================================*/ UNION
SELECT        SS_OtherLetterRequest.ID, '' AS VacationType, SS_OtherLetterRequest.Code AS RequestSerial, SS_OtherLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_OtherLetterRequest.RequestDate, 'طلب خطاب آخر' AS RequestArbName, 'Other Letter Request' AS RequestEngName, 'SS_00194' AS FormCode
,RequestStautsTypeID  From            SS_OtherLetterRequest JOIN
                         hrs_Employees ON SS_OtherLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*8============================    طلب تدريب ==============================================*/ UNION
SELECT        SS_TrainingRequest.ID, '' AS VacationType, SS_TrainingRequest.Code AS RequestSerial, SS_TrainingRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_TrainingRequest.RequestDate, 'طلب تدريب' AS RequestArbName, 'Training Request' AS RequestEngName, 'SS_00195' AS FormCode
,RequestStautsTypeID  From            SS_TrainingRequest JOIN
                         hrs_Employees ON SS_TrainingRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*9============================     استمارة التظلم ==============================================*/ UNION
SELECT        SS_GrievanceFormRequest.ID, '' AS VacationType, SS_GrievanceFormRequest.Code AS RequestSerial, SS_GrievanceFormRequest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_GrievanceFormRequest.RequestDate, 
                         'طلب استمارة التظلم' AS RequestArbName, 'Grievance Form Request' AS RequestEngName, 'SS_00196' AS FormCode
,RequestStautsTypeID  From            SS_GrievanceFormRequest JOIN
                         hrs_Employees ON SS_GrievanceFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*10============================     طلب استمارة تقييم المقابلة ==============================================*/ UNION
SELECT        SS_InterviewEvaluationFormRequest.ID, '' AS VacationType, SS_InterviewEvaluationFormRequest.Code AS RequestSerial, SS_InterviewEvaluationFormRequest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_InterviewEvaluationFormRequest.RequestDate, 
                         'طلب استمارة تقييم المقابلة' AS RequestArbName, 'Interview Evaluation Form Request' AS RequestEngName, 'SS_00197' AS FormCode
,RequestStautsTypeID  From            SS_InterviewEvaluationFormRequest JOIN
                         hrs_Employees ON SS_InterviewEvaluationFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*11============================     طلب استمارة تصعيد حالات الاعتداء ==============================================*/ UNION
SELECT        SS_AssaultEscalationFormRequest.ID, '' AS VacationType, SS_AssaultEscalationFormRequest.Code AS RequestSerial, SS_AssaultEscalationFormRequest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AssaultEscalationFormRequest.RequestDate, 
                         'طلب استمارة تصعيد حالات الاعتداء' AS RequestArbName, 'Assault Escalation Form Request' AS RequestEngName, 'SS_00198' AS FormCode
,RequestStautsTypeID  From            SS_AssaultEscalationFormRequest JOIN
                         hrs_Employees ON SS_AssaultEscalationFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*12============================   طلب استمارة تضارب المصالح ==============================================*/ UNION
SELECT        SS_ConflictofInterestFormRequest.ID, '' AS VacationType, SS_ConflictofInterestFormRequest.Code AS RequestSerial, SS_ConflictofInterestFormRequest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ConflictofInterestFormRequest.RequestDate, 
                         'طلب استمارة تضارب المصالح' AS RequestArbName, 'Conflict of Interest Form Request' AS RequestEngName, 'SS_00199' AS FormCode
,RequestStautsTypeID  From            SS_ConflictofInterestFormRequest JOIN
                         hrs_Employees ON SS_ConflictofInterestFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*13============================   طلب استمارة امتيازات الأطباء ==============================================*/ UNION
SELECT        SS_PhysiciansPrivilegingFormRequest.ID, '' AS VacationType, SS_PhysiciansPrivilegingFormRequest.Code AS RequestSerial, SS_PhysiciansPrivilegingFormRequest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_PhysiciansPrivilegingFormRequest.RequestDate, 
                         'طلب استمارة امتيازات الأطباء' AS RequestArbName, 'Physicians Privileging Form Request' AS RequestEngName, 'SS_001910' AS FormCode
,RequestStautsTypeID  From            SS_PhysiciansPrivilegingFormRequest JOIN
                         hrs_Employees ON SS_PhysiciansPrivilegingFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*14============================   طلب دعم رعاية الأطفال ==============================================*/ UNION
SELECT        SS_DaycareSupportReaquest.ID, '' AS VacationType, SS_DaycareSupportReaquest.Code AS RequestSerial, SS_DaycareSupportReaquest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_DaycareSupportReaquest.RequestDate, 
                         'طلب دعم رعاية الأطفال' AS RequestArbName, 'Daycare Support' AS RequestEngName, 'SS_001911' AS FormCode
,RequestStautsTypeID  From            SS_DaycareSupportReaquest JOIN
                         hrs_Employees ON SS_DaycareSupportReaquest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*15============================طلب دعم التعليم  ==============================================*/ UNION
SELECT        SS_EducationSupportRequest.ID, '' AS VacationType, SS_EducationSupportRequest.Code AS RequestSerial, SS_EducationSupportRequest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_EducationSupportRequest.RequestDate, 
                         'طلب دعم التعليم' AS RequestArbName, 'Education Support' AS RequestEngName, 'SS_001912' AS FormCode
,RequestStautsTypeID  From            SS_EducationSupportRequest JOIN
                         hrs_Employees ON SS_EducationSupportRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*16===========================طلب  بدل السكن مقدم  ==============================================*/ UNION
SELECT        SS_AdvanceHousingRequest.ID, '' AS VacationType, SS_AdvanceHousingRequest.Code AS RequestSerial, SS_AdvanceHousingRequest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AdvanceHousingRequest.RequestDate, 
                         'طلب  بدل السكن مقدم' AS RequestArbName, ' Advance Housing Request' AS RequestEngName, 'SS_001913' AS FormCode
,RequestStautsTypeID  From            SS_AdvanceHousingRequest JOIN
                         hrs_Employees ON SS_AdvanceHousingRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*17===========================طلب  الراتب مقدم  ==============================================*/ UNION
SELECT        SS_AdvanceSalaryRequest.ID, '' AS VacationType, SS_AdvanceSalaryRequest.Code AS RequestSerial, SS_AdvanceSalaryRequest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AdvanceSalaryRequest.RequestDate, 
                         'طلب الراتب مقدم' AS RequestArbName, 'Advance Salary Request' AS RequestEngName, 'SS_001914' AS FormCode
,RequestStautsTypeID  From            SS_AdvanceSalaryRequest JOIN
                         hrs_Employees ON SS_AdvanceSalaryRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*18===========================طلب خطاب الغرفة التجارية ==============================================*/ UNION
SELECT        SS_ChamberofCommerceLetterRequest.ID, '' AS VacationType, SS_ChamberofCommerceLetterRequest.Code AS RequestSerial, SS_ChamberofCommerceLetterRequest.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ChamberofCommerceLetterRequest.RequestDate, 
                         'طلب خطاب الغرفة التجارية' AS RequestArbName, 'Chamber of Commerce Letter Reques' AS RequestEngName, 'SS_001915' AS FormCode
,RequestStautsTypeID  From            SS_ChamberofCommerceLetterRequest JOIN
                         hrs_Employees ON SS_ChamberofCommerceLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*19===========================طلب خطاب تصنيف هيئة التخصصات الطبية ==============================================*/ UNION
SELECT        SS_SCFHSLetterRequest.ID, '' AS VacationType, SS_SCFHSLetterRequest.Code AS RequestSerial, SS_SCFHSLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_SCFHSLetterRequest.RequestDate, 'طلب خطاب تصنيف هيئة التخصصات الطبية' AS RequestArbName, 'SCFHS Letter Request' AS RequestEngName, 'SS_001916' AS FormCode
,RequestStautsTypeID  From            SS_SCFHSLetterRequest JOIN
                         hrs_Employees ON SS_SCFHSLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*20===========================طلب خطاب تعريف الراتب ==============================================*/ UNION
SELECT        SS_PaySlipRequest.ID, '' AS VacationType, SS_PaySlipRequest.Code AS RequestSerial, SS_PaySlipRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_PaySlipRequest.RequestDate, 'طلب خطاب تعريف الراتب' AS RequestArbName, 'Pay Slip Request' AS RequestEngName, 'SS_001917' AS FormCode
,RequestStautsTypeID  From            SS_PaySlipRequest JOIN
                         hrs_Employees ON SS_PaySlipRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*21============================خطابات الإبلاغ عن التباين  ==============================================*/ UNION
SELECT        SS_OccurrenceVarianceReportingLetters.ID, '' AS VacationType, SS_OccurrenceVarianceReportingLetters.Code AS RequestSerial, SS_OccurrenceVarianceReportingLetters.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_OccurrenceVarianceReportingLetters.RequestDate, 
                         '
خطابات الإبلاغ عن التباين' AS RequestArbName, 'Occurrence Variance Reporting Letters' AS RequestEngName, 'SS_001918' AS FormCode
,RequestStautsTypeID  From            SS_OccurrenceVarianceReportingLetters JOIN
                         hrs_Employees ON SS_OccurrenceVarianceReportingLetters.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*22============================طلب وقت اضافي  ==============================================*/ UNION
SELECT        SS_OvertimeRequest.ID, '' AS VacationType, SS_OvertimeRequest.Code AS RequestSerial, SS_OvertimeRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_OvertimeRequest.RequestDate, 'طلب وقت اضافي' AS RequestArbName, 'Over Time' AS RequestEngName, 'SS_001919' AS FormCode
,RequestStautsTypeID  From            SS_OvertimeRequest JOIN
                         hrs_Employees ON SS_OvertimeRequest.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*23===========================طلب تعويض رسوم التعليم  ================================*/ UNION
SELECT        SS_EducationFeesCompensationApplication.ID, '' AS VacationType, SS_EducationFeesCompensationApplication.Code AS RequestSerial, SS_EducationFeesCompensationApplication.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_EducationFeesCompensationApplication.RequestDate, 
                         'طلب تعويض رسوم التعليم' AS RequestArbName, 'Education Fees Compensation Application' AS RequestEngName, 'SS_001920' AS FormCode
,RequestStautsTypeID  From            SS_EducationFeesCompensationApplication JOIN
                         hrs_Employees ON SS_EducationFeesCompensationApplication.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*24===========================طلب تحديث حساب البنك  ================================*/ UNION
SELECT        SS_BankAccountUpdate.ID, '' AS VacationType, SS_BankAccountUpdate.Code AS RequestSerial, SS_BankAccountUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_BankAccountUpdate.RequestDate, 'طلب تحديث حساب البنك' AS RequestArbName, 'Bank Account Update' AS RequestEngName, 'SS_001921' AS FormCode
,RequestStautsTypeID  From            SS_BankAccountUpdate JOIN
                         hrs_Employees ON SS_BankAccountUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*25===========================طلب تحديث معلومات الاتصال  ================================*/ UNION
SELECT        SS_ContactInformationUpdate.ID, '' AS VacationType, SS_ContactInformationUpdate.Code AS RequestSerial, SS_ContactInformationUpdate.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ContactInformationUpdate.RequestDate, 
                         'طلب تحديث معلومات الاتصال' AS RequestArbName, 'Contact Information Update' AS RequestEngName, 'SS_001922' AS FormCode
,RequestStautsTypeID  From            SS_ContactInformationUpdate JOIN
                         hrs_Employees ON SS_ContactInformationUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*26===========================طلب تحديث معلومات المعالين  ================================*/ UNION
SELECT        SS_DependentsInformationUpdate.ID, '' AS VacationType, SS_DependentsInformationUpdate.Code AS RequestSerial, SS_DependentsInformationUpdate.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_DependentsInformationUpdate.RequestDate, 
                         'طلب تحديث معلومات المعالين' AS RequestArbName, 'Dependents Information Update' AS RequestEngName, 'SS_001923' AS FormCode
,RequestStautsTypeID  From            SS_DependentsInformationUpdate JOIN
                         hrs_Employees ON SS_DependentsInformationUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*27===========================طلب تعديلات التأمين الطبي  ================================*/ UNION
SELECT        SS_MedicalInsuranceAdjustments.ID, '' AS VacationType, SS_MedicalInsuranceAdjustments.Code AS RequestSerial, SS_MedicalInsuranceAdjustments.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_MedicalInsuranceAdjustments.RequestDate, 
                         'طلب تعديلات التأمين الطبي' AS RequestArbName, 'Medical Insurance Adjustments' AS RequestEngName, 'SS_001924' AS FormCode
,RequestStautsTypeID  From            SS_MedicalInsuranceAdjustments JOIN
                         hrs_Employees ON SS_MedicalInsuranceAdjustments.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*28===========================طلب تحديثات المستندات القانونية الأخرى  ================================*/ UNION
SELECT        SS_OtherLegalDocumentUpdates.ID, '' AS VacationType, SS_OtherLegalDocumentUpdates.Code AS RequestSerial, SS_OtherLegalDocumentUpdates.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_OtherLegalDocumentUpdates.RequestDate, 
                         'طلب تحديثات المستندات القانونية الأخرى' AS RequestArbName, 'Other Legal Document Updates' AS RequestEngName, 'SS_001925' AS FormCode
,RequestStautsTypeID  From            SS_OtherLegalDocumentUpdates JOIN
                         hrs_Employees ON SS_OtherLegalDocumentUpdates.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*29===========================طلب تحديث ملف الموظف  ================================*/ UNION
SELECT        SS_EmployeeFileUpdate.ID, '' AS VacationType, SS_EmployeeFileUpdate.Code AS RequestSerial, SS_EmployeeFileUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                         + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                         SS_EmployeeFileUpdate.RequestDate, 'طلب تحديث ملف الموظف' AS RequestArbName, 'Employee File Update' AS RequestEngName, 'SS_001926' AS FormCode
,RequestStautsTypeID  From            SS_EmployeeFileUpdate JOIN
                         hrs_Employees ON SS_EmployeeFileUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*30===========================طلب سفر عمل او تدريب  ================================*/ UNION
SELECT        SS_BusinessORTrainingTravel.ID, '' AS VacationType, SS_BusinessORTrainingTravel.Code AS RequestSerial, SS_BusinessORTrainingTravel.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_BusinessORTrainingTravel.RequestDate, 
                         'طلب سفر عمل او تدريب' AS RequestArbName, 'Business OR Training Travel' AS RequestEngName, 'SS_001927' AS FormCode
,RequestStautsTypeID  From            SS_BusinessORTrainingTravel JOIN
                         hrs_Employees ON SS_BusinessORTrainingTravel.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*31===========================الطلبات المتعلقة بالتذاكر السنوية  ================================*/ UNION
SELECT        SS_AnnualTicketRelatedRequests.ID, '' AS VacationType, SS_AnnualTicketRelatedRequests.Code AS RequestSerial, SS_AnnualTicketRelatedRequests.EmployeeID, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, 
                         hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AnnualTicketRelatedRequests.RequestDate, 
                         'الطلبات المتعلقة بالتذاكر السنوية' AS RequestArbName, 'Annual Ticket Related Requests' AS RequestEngName, 'SS_001928' AS FormCode
,RequestStautsTypeID  From            SS_AnnualTicketRelatedRequests JOIN
                         hrs_Employees ON SS_AnnualTicketRelatedRequests.EmployeeID = hrs_Employees.id INNER JOIN
                         sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
"
        ExecuteUpdate(SQL)


        SQL = "ALTER TABLE dbo.hrs_VacationsTypes ADD OverlapWithAnotherVac bit NULL"
        ExecuteUpdate(SQL)


        SQL = "INSERT [dbo].[hrs_VacationsBalanceType] ([ID], [ArbName], [EngName]) VALUES (3, N'تعويض', N'Compensation')"
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.SS_OvertimeRequest ADD
	HoursCount float(53) NULL,
	OvertimeDate date NULL,
	OvertimeType int NULL
"
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.SS_RequestActions ADD
	HoursCount float(53) NULL,
	OvertimeDate date NULL,
	OvertimeType int NULL
"
        ExecuteUpdate(SQL)

        SQL = "
ALTER TABLE dbo.SS_AdvanceHousingRequest ADD InstallmentDate date NULL
"
        ExecuteUpdate(SQL)
        SQL = "
CREATE VIEW [dbo].[V_AllSSrequestsSendEmail]
AS
SELECT dbo.SS_VFollowup.ID, dbo.SS_VFollowup.VacationType, dbo.SS_VFollowup.RequestSerial AS Code, dbo.SS_VFollowup.EmployeeID, dbo.SS_VFollowup.RequestDate, dbo.SS_VFollowup.RequestEngName, dbo.SS_VFollowup.FormCode, 
                  dbo.SS_VFollowup.RequestArbName, dbo.SS_VFollowup.EmployeeArbName, dbo.SS_VFollowup.EmployeeEngName, dbo.SS_Configuration.UserTypeID, dbo.SS_Configuration.PositionID, 
                  CASE WHEN SS_Configuration.UserTypeID = '1' THEN Manager.WorkE_Mail ELSE CASE WHEN SS_Configuration.UserTypeID = '2' THEN Position.WorkE_Mail ELSE config.WorkE_Mail END END AS Email, 
                  CASE WHEN SS_Configuration.UserTypeID = '1' THEN Manager.EngName ELSE CASE WHEN SS_Configuration.UserTypeID = '2' THEN Position.EngName ELSE config.EngName END END AS SSEmpName
FROM     dbo.SS_VFollowup INNER JOIN
                  dbo.SS_Configuration ON dbo.SS_VFollowup.FormCode = dbo.SS_Configuration.FormCode AND dbo.SS_Configuration.Rank = 1 INNER JOIN
                  dbo.hrs_Employees AS empMang ON dbo.SS_VFollowup.EmployeeID = empMang.ID LEFT OUTER JOIN
                  dbo.hrs_Employees AS config ON dbo.SS_Configuration.EmployeeID = config.ID INNER JOIN
                  dbo.hrs_Employees AS Manager ON empMang.ManagerID = Manager.ID LEFT OUTER JOIN
                  dbo.hrs_Contracts AS PositionContract ON dbo.SS_Configuration.PositionID = PositionContract.PositionID AND dbo.SS_Configuration.Rank = 1 LEFT OUTER JOIN
                  dbo.hrs_Employees AS Position ON PositionContract.EmployeeID = Position.ID
WHERE  (dbo.SS_Configuration.Rank = 1)
"
        ExecuteUpdate(SQL)

        SQL = "
CREATE VIEW [dbo].[V_AllSSRequestActionsSendEmail]
AS
SELECT dbo.SS_RequestActions.ActionSerial, dbo.SS_RequestActions.RequestSerial AS Code, dbo.SS_RequestActions.SS_EmployeeID, dbo.SS_RequestActions.FormCode, dbo.SS_RequestActions.ConfigID, dbo.SS_RequestActions.ActionID, 
                  dbo.SS_RequestActions.IsHidden, dbo.SS_RequestActions.OvertimeType, dbo.hrs_Employees.EngName AS SSEmpName, dbo.hrs_Employees.ArbName, dbo.hrs_Employees.WorkE_Mail AS Email, dbo.SS_VFollowup.RequestEngName, 
                  dbo.SS_VFollowup.RequestArbName, dbo.SS_RequestActions.RequestSerial
FROM     dbo.SS_RequestActions INNER JOIN
                  dbo.hrs_Employees ON dbo.SS_RequestActions.SS_EmployeeID = dbo.hrs_Employees.ID INNER JOIN
                  dbo.SS_VFollowup ON dbo.SS_RequestActions.FormCode = dbo.SS_VFollowup.FormCode AND dbo.SS_RequestActions.RequestSerial = dbo.SS_VFollowup.ID
WHERE  (dbo.SS_RequestActions.ActionID IS NULL) AND (ISNULL(dbo.SS_RequestActions.IsHidden, 0) = 0)

"
        ExecuteUpdate(SQL)

        SQL = " Create or ALTER VIEW [dbo].[V_AnnualvacationAction_EmialToRequester]
AS
SELECT SS.ActionSerial AS ID, SS.RequestSerial, E.WorkE_Mail, VR.Code AS RequestCode, CASE WHEN ActionID = 1 THEN 'Accepted' WHEN ActionID = 2 THEN 'Rejected' WHEN ActionID = 4 THEN 'Delegated' END AS Action, 
                  dbo.fn_GetEmpName(SSEMP.Code, 0) AS RequestActionBy, SS.FormCode
FROM     dbo.SS_RequestActions AS SS INNER JOIN
                  dbo.hrs_Employees AS E ON SS.EmployeeID = E.ID INNER JOIN
                  dbo.SS_VacationRequest AS VR ON SS.RequestSerial = VR.ID INNER JOIN
                  dbo.hrs_Employees AS SSEMP ON SSEMP.ID = SS.SS_EmployeeID"
        ExecuteUpdate(SQL)

        SQL = "  CREATE TABLE [dbo].[SS_VisaTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[EngName] [varchar](50) NULL,
	[ArbName] [varchar](50) NULL,
	[CancelDate] [date] NULL,
 CONSTRAINT [PK_SS_VisaTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]    "
        ExecuteUpdate(SQL)
        SQL = "  alter table [dbo].[SS_VisaRequest] add VisaTypeID int null "
        ExecuteUpdate(SQL)
        SQL = "ALTER TABLE dbo.SS_OvertimeRequest ADD 	MinutsCount int NULL
"
        ExecuteUpdate(SQL)

        SQL = " ALTER TABLE dbo.SS_OvertimeRequest ADD CONSTRAINT 	DF_SS_OvertimeRequest_MinutsCount DEFAULT 0 FOR MinutsCount "

        ExecuteUpdate(SQL)

        SQL = "ALTER TABLE dbo.SS_RequestActions ADD CONSTRAINT DF_SS_RequestActions_MinutsCount DEFAULT 0 FOR MinutsCount"
        ExecuteUpdate(SQL)

        SQL ="
ALTER VIEW [dbo].[SS_VNotification]
AS
/*1 ============================الاجازة ==============================================*/ SELECT RequestSerial AS ID, FormCode, ConfigID, SS_VacationRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_VacationRequest.RequestDate, 103) AS RequestDate, SS_VacationRequest.VacationType, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي اداري' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي طبي'
WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي تشغيلي' 
WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'اجازة اخرى تشغيلي' 
 ELSE hrs_VacationsTypes.ArbName4S END AS RequestArbName, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Admin' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Medical' 
WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Operational'
WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'Other Vacation Operational'
ELSE hrs_VacationsTypes.EngName END AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_VacationRequest ON SS_RequestActions.RequestSerial = SS_VacationRequest.ID AND SS_RequestActions.EmployeeID = SS_VacationRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID JOIN
                  hrs_VacationsTypes ON SS_VacationRequest.VacationTypeID = hrs_VacationsTypes.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND (FormCode = 'SS_0011' OR
                  FormCode = 'SS_0012' OR
                  FormCode = 'SS_0013' OR
                  FormCode = 'SS_0030' OR
                  FormCode = 'SS_0031' OR
                  FormCode = 'SS_0018')
/* 2 ============================الاستئذان ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ExecuseRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) AS RequestDate, '' AS ExecuseType, 
                  'طلب استئذان' AS RequestArbName, 'Execuse Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ExecuseRequest ON SS_RequestActions.RequestSerial = SS_ExecuseRequest.ID AND SS_RequestActions.EmployeeID = SS_ExecuseRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_0014'
/*3 ============================انهاء خدمة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EndOfServiceRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب انهاء خدمة' AS RequestArbType, 'End Of Service Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EndOfServiceRequest ON SS_RequestActions.RequestSerial = SS_EndOfServiceRequest.ID AND SS_RequestActions.EmployeeID = SS_EndOfServiceRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND (SS_RequestActions.FormCode = 'SS_0015' OR
                  SS_RequestActions.FormCode = 'SS_0019')
/*4============================خروج وعودة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ExitEntryRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ExitEntryRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خروج وعودة' AS RequestArbName, 'Exit & Re-entry Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ExitEntryRequest ON SS_RequestActions.RequestSerial = SS_ExitEntryRequest.ID AND SS_RequestActions.EmployeeID = SS_ExitEntryRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00191'
/*5============================ تأشيرة الأسرة أو تأشيرة الزيارة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_VisaRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_VisaRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تأشيرة الأسرة أو تأشيرة الزيارة' AS RequestArbName, 'Family visa or Visit visa Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_VisaRequest ON SS_RequestActions.RequestSerial = SS_VisaRequest.ID AND SS_RequestActions.EmployeeID = SS_VisaRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00192'
/*6============================  خطاب قرض ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_LoanLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_LoanLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب قرض' AS RequestArbName, 'Loan Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_LoanLetterRequest ON SS_RequestActions.RequestSerial = SS_LoanLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_LoanLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00193'
/*7============================   خطاب آخر ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OtherLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OtherLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب آخر' AS RequestArbName, 'Other Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OtherLetterRequest ON SS_RequestActions.RequestSerial = SS_OtherLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_OtherLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00194'
/*8============================    طلب تدريب ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_TrainingRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_TrainingRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تدريب' AS RequestArbName, 'Training Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_TrainingRequest ON SS_RequestActions.RequestSerial = SS_TrainingRequest.ID AND SS_RequestActions.EmployeeID = SS_TrainingRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00195'
/*9============================     استمارة التظلم ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_GrievanceFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_GrievanceFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب استمارة التظلم' AS RequestArbName, 'Grievance Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_GrievanceFormRequest ON SS_RequestActions.RequestSerial = SS_GrievanceFormRequest.ID AND SS_RequestActions.EmployeeID = SS_GrievanceFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00196'
/*10============================     طلب استمارة تقييم المقابلة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_InterviewEvaluationFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_InterviewEvaluationFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة تقييم المقابلة' AS RequestArbName, 'Interview Evaluation Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_InterviewEvaluationFormRequest ON SS_RequestActions.RequestSerial = SS_InterviewEvaluationFormRequest.ID AND SS_RequestActions.EmployeeID = SS_InterviewEvaluationFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00197'
/*11============================     طلب استمارة تصعيد حالات الاعتداء ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AssaultEscalationFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_AssaultEscalationFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة تصعيد حالات الاعتداء' AS RequestArbName, 'Assault Escalation Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AssaultEscalationFormRequest ON SS_RequestActions.RequestSerial = SS_AssaultEscalationFormRequest.ID AND SS_RequestActions.EmployeeID = SS_AssaultEscalationFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00198'
/*12============================   طلب استمارة تضارب المصالح ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ConflictofInterestFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ConflictofInterestFormRequest.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب استمارة تضارب المصالح' AS RequestArbName, 'Conflict of Interest Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ConflictofInterestFormRequest ON SS_RequestActions.RequestSerial = SS_ConflictofInterestFormRequest.ID AND SS_RequestActions.EmployeeID = SS_ConflictofInterestFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00199'
/*13============================   طلب استمارة امتيازات الأطباء ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_PhysiciansPrivilegingFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_PhysiciansPrivilegingFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة امتيازات الأطباء' AS RequestArbName, 'Physicians Privileging Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_PhysiciansPrivilegingFormRequest ON SS_RequestActions.RequestSerial = SS_PhysiciansPrivilegingFormRequest.ID AND SS_RequestActions.EmployeeID = SS_PhysiciansPrivilegingFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001910'
/*14============================   طلب دعم رعاية الأطفال ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_DaycareSupportReaquest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_DaycareSupportReaquest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب دعم رعاية الأطفال' AS RequestArbName, 'Daycare Support' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_DaycareSupportReaquest ON SS_RequestActions.RequestSerial = SS_DaycareSupportReaquest.ID AND SS_RequestActions.EmployeeID = SS_DaycareSupportReaquest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001911'
/*15============================طلب دعم التعليم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EducationSupportRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EducationSupportRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب دعم التعليم' AS RequestArbName, 'Education Support' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EducationSupportRequest ON SS_RequestActions.RequestSerial = SS_EducationSupportRequest.ID AND SS_RequestActions.EmployeeID = SS_EducationSupportRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001912'
/*16===========================طلب  بدل السكن مقدم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AdvanceHousingRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AdvanceHousingRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب  بدل السكن مقدم' AS RequestArbName, 'Advance Housing Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AdvanceHousingRequest ON SS_RequestActions.RequestSerial = SS_AdvanceHousingRequest.ID AND SS_RequestActions.EmployeeID = SS_AdvanceHousingRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001913'
/*17===========================طلب  الراتب مقدم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AdvanceSalaryRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AdvanceSalaryRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب  الراتب مقدم' AS RequestArbName, 'Advance Salary Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AdvanceSalaryRequest ON SS_RequestActions.RequestSerial = SS_AdvanceSalaryRequest.ID AND SS_RequestActions.EmployeeID = SS_AdvanceSalaryRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001914'
/*18===========================طلب خطاب الغرفة التجارية ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ChamberofCommerceLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_ChamberofCommerceLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب خطاب الغرفة التجارية' AS RequestArbName, 'Chamber of Commerce Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ChamberofCommerceLetterRequest ON SS_RequestActions.RequestSerial = SS_ChamberofCommerceLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_ChamberofCommerceLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001915'
/*19===========================طلب خطاب تصنيف هيئة التخصصات الطبية ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_SCFHSLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_SCFHSLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب تصنيف هيئة التخصصات الطبية' AS RequestArbName, 'SCFHS Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_SCFHSLetterRequest ON SS_RequestActions.RequestSerial = SS_SCFHSLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_SCFHSLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001916'
/*20===========================طلب خطاب تعريف الراتب ==============================================*/  UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_PaySlipRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_PaySlipRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطابتعريف الراتب' AS RequestArbName, 'Pay Slip Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_PaySlipRequest ON SS_RequestActions.RequestSerial = SS_PaySlipRequest.ID AND SS_RequestActions.EmployeeID = SS_PaySlipRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001917'
/*21============================خطابات الإبلاغ عن التباين  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OccurrenceVarianceReportingLetters.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_OccurrenceVarianceReportingLetters.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب خطابات الإبلاغ عن التباين' AS RequestArbName, 'Occurrence Variance Reporting Letter' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OccurrenceVarianceReportingLetters ON SS_RequestActions.RequestSerial = SS_OccurrenceVarianceReportingLetters.ID AND SS_RequestActions.EmployeeID = SS_OccurrenceVarianceReportingLetters.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001918'
/*22============================طلب وقت اضافي  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OvertimeRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OvertimeRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب وقت اضافي' AS RequestArbName, ' Over Time Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OvertimeRequest ON SS_RequestActions.RequestSerial = SS_OvertimeRequest.ID AND SS_RequestActions.EmployeeID = SS_OvertimeRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001919'
/*23===========================طلب تعويض رسوم التعليم  ================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EducationFeesCompensationApplication.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_EducationFeesCompensationApplication.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب تعويض رسوم التعليم' AS RequestArbName, 'Education Fees Compensation Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EducationFeesCompensationApplication ON SS_RequestActions.RequestSerial = SS_EducationFeesCompensationApplication.ID AND SS_RequestActions.EmployeeID = SS_EducationFeesCompensationApplication.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001920'
/*24===========================طلب تحديث حساب البنك  ================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_BankAccountUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_BankAccountUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث بيانات البنك' AS RequestArbName, 'Bank Acc Update Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_BankAccountUpdate ON SS_RequestActions.RequestSerial = SS_BankAccountUpdate.ID AND SS_RequestActions.EmployeeID = SS_BankAccountUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001921'
/*25============================طلب تحديث معلومات الاتصال  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ContactInformationUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ContactInformationUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث معلومات الاتصال' AS RequestArbName, 'Contact Information Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ContactInformationUpdate ON SS_RequestActions.RequestSerial = SS_ContactInformationUpdate.ID AND SS_RequestActions.EmployeeID = SS_ContactInformationUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001922'
/*26============================طلب تحديث معلومات المعالين  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_DependentsInformationUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_DependentsInformationUpdate.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تحديث معلومات المعالين' AS RequestArbName, 'Dependents Information Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_DependentsInformationUpdate ON SS_RequestActions.RequestSerial = SS_DependentsInformationUpdate.ID AND SS_RequestActions.EmployeeID = SS_DependentsInformationUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001923'
/*27============================طلب تعديلات التأمين الطبي  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_MedicalInsuranceAdjustments.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_MedicalInsuranceAdjustments.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تعديلات التأمين الطبي' AS RequestArbName, 'Medical Insurance Adjustments' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_MedicalInsuranceAdjustments ON SS_RequestActions.RequestSerial = SS_MedicalInsuranceAdjustments.ID AND SS_RequestActions.EmployeeID = SS_MedicalInsuranceAdjustments.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001924'
/*28======================طلب تحديثات المستندات القانونية الأخرى  =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OtherLegalDocumentUpdates.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OtherLegalDocumentUpdates.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تحديثات المستندات القانونية الأخرى' AS RequestArbName, 'Other Legal Document Updates' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OtherLegalDocumentUpdates ON SS_RequestActions.RequestSerial = SS_OtherLegalDocumentUpdates.ID AND SS_RequestActions.EmployeeID = SS_OtherLegalDocumentUpdates.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001925'
/*29======================طلب تحديثات المستندات القانونية الأخرى  =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EmployeeFileUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EmployeeFileUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث ملف الموظف' AS RequestArbName, 'Employee File Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EmployeeFileUpdate ON SS_RequestActions.RequestSerial = SS_EmployeeFileUpdate.ID AND SS_RequestActions.EmployeeID = SS_EmployeeFileUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001926'
/*30====================== طلب سفر عمل او تدريب    =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_BusinessORTrainingTravel.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_BusinessORTrainingTravel.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب سفر عمل او تدريب' AS RequestArbName, 'Business OR Training Travel' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_BusinessORTrainingTravel ON SS_RequestActions.RequestSerial = SS_BusinessORTrainingTravel.ID AND SS_RequestActions.EmployeeID = SS_BusinessORTrainingTravel.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001927'
/*31====================== الطلبات المتعلقة بالتذاكر السنوية    =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AnnualTicketRelatedRequests.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AnnualTicketRelatedRequests.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'الطلبات المتعلقة بالتذاكر السنوية' AS RequestArbName, 'Annual Ticket Related Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AnnualTicketRelatedRequests ON SS_RequestActions.RequestSerial = SS_AnnualTicketRelatedRequests.ID AND SS_RequestActions.EmployeeID = SS_AnnualTicketRelatedRequests.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001928'

"
        ExecuteUpdate(SQL)

        SQL="
ALTER VIEW [dbo].[SS_VFollowup]
AS
/*-1==============================الاجازة==================*/ SELECT SS_VacationRequest.ID, SS_VacationRequest.VacationType, SS_VacationRequest.Code AS RequestSerial, SS_VacationRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_VacationRequest.RequestDate, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي اداري' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي طبي' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي تشغيلي' WHEN SS_VacationRequest.VacationType = 'SS_0031'  THEN 'اجازة اخرى تشغيلي' ELSE hrs_VacationsTypes.ArbName4S END AS RequestArbName, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Admin' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Medical' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Operational' WHEN SS_VacationRequest.VacationType = 'SS_0031'  THEN 'Other Vacation Operational' ELSE hrs_VacationsTypes.EngName END AS RequestEngName, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0011' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0012' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0030' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'SS_0031' WHEN SS_VacationRequest.VacationType = 'SS_0018' THEN 'SS_0018' ELSE 'SS_0013' END AS FormCode, RequestStautsTypeID
FROM     SS_VacationRequest JOIN
                  hrs_Employees ON SS_VacationRequest.EmployeeID = hrs_Employees.id JOIN
                  hrs_VacationsTypes ON SS_VacationRequest.VacationTypeID = hrs_VacationsTypes.ID INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*-2==============================الاستئذان=======================================================*/ UNION
SELECT SS_ExecuseRequest.ID, '' AS VacationType, SS_ExecuseRequest.Code AS RequestSerial, SS_ExecuseRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ExecuseRequest.RequestDate, 'طلب استئذان' AS RequestArbName, 'Execuse Request' AS RequestEngName, 'SS_0014' AS FormCode, RequestStautsTypeID
FROM     SS_ExecuseRequest JOIN
                  hrs_Employees ON SS_ExecuseRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*3 ============================انهاء خدمة ===============================================================*/ UNION
SELECT SS_EndOfServiceRequest.ID, '' AS VacationType, SS_EndOfServiceRequest.Code AS RequestSerial, SS_EndOfServiceRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EndOfServiceRequest.RequestDate, CASE WHEN FormCode = 'SS_0015' THEN 'طلب انهاء خدمة' ELSE 'طلب انهاء خدمة طبي' END AS RequestArbName, 
                  CASE WHEN FormCode = 'SS_0015' THEN 'End Of Service Request' ELSE 'End Of Service Medical Request' END AS RequestEngName, FormCode AS FormCode, RequestStautsTypeID
FROM     SS_EndOfServiceRequest JOIN
                  hrs_Employees ON SS_EndOfServiceRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*4============================خروج وعودة ==============================================*/ UNION
SELECT SS_ExitEntryRequest.ID, '' AS VacationType, SS_ExitEntryRequest.Code AS RequestSerial, SS_ExitEntryRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ExitEntryRequest.RequestDate, 'طلب خروج وعودة' AS RequestArbName, 'Exit & Re-entry Requests' AS RequestEngName, 'SS_00191' AS FormCode, RequestStautsTypeID
FROM     SS_ExitEntryRequest JOIN
                  hrs_Employees ON SS_ExitEntryRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*5============================ تأشيرة الأسرة أو تأشيرة الزيارة ==============================================*/ UNION
SELECT SS_VisaRequest.ID, '' AS VacationType, SS_VisaRequest.Code AS RequestSerial, SS_VisaRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_VisaRequest.RequestDate, 
                  'طلب تأشيرة الأسرة أو تأشيرة الزيارة' AS RequestArbName, 'Family visa or Visit visa Requests' AS RequestEngName, 'SS_00192' AS FormCode, RequestStautsTypeID
FROM     SS_VisaRequest JOIN
                  hrs_Employees ON SS_VisaRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*6============================  خطاب قرض ==============================================*/ UNION
SELECT SS_LoanLetterRequest.ID, '' AS VacationType, SS_LoanLetterRequest.Code AS RequestSerial, SS_LoanLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_LoanLetterRequest.RequestDate, 'طلب خطاب قرض' AS RequestArbName, 'Loan Letter Request' AS RequestEngName, 'SS_00193' AS FormCode, RequestStautsTypeID
FROM     SS_LoanLetterRequest JOIN
                  hrs_Employees ON SS_LoanLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*7============================   خطاب آخر ==============================================*/ UNION
SELECT SS_OtherLetterRequest.ID, '' AS VacationType, SS_OtherLetterRequest.Code AS RequestSerial, SS_OtherLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_OtherLetterRequest.RequestDate, 'طلب خطاب آخر' AS RequestArbName, 'Other Letter Request' AS RequestEngName, 'SS_00194' AS FormCode, RequestStautsTypeID
FROM     SS_OtherLetterRequest JOIN
                  hrs_Employees ON SS_OtherLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*8============================    طلب تدريب ==============================================*/ UNION
SELECT SS_TrainingRequest.ID, '' AS VacationType, SS_TrainingRequest.Code AS RequestSerial, SS_TrainingRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_TrainingRequest.RequestDate, 'طلب تدريب' AS RequestArbName, 'Training Request' AS RequestEngName, 'SS_00195' AS FormCode, RequestStautsTypeID
FROM     SS_TrainingRequest JOIN
                  hrs_Employees ON SS_TrainingRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*9============================     استمارة التظلم ==============================================*/ UNION
SELECT SS_GrievanceFormRequest.ID, '' AS VacationType, SS_GrievanceFormRequest.Code AS RequestSerial, SS_GrievanceFormRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_GrievanceFormRequest.RequestDate, 'طلب استمارة التظلم' AS RequestArbName, 'Grievance Form Request' AS RequestEngName, 'SS_00196' AS FormCode, RequestStautsTypeID
FROM     SS_GrievanceFormRequest JOIN
                  hrs_Employees ON SS_GrievanceFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*10============================     طلب استمارة تقييم المقابلة ==============================================*/ UNION
SELECT SS_InterviewEvaluationFormRequest.ID, '' AS VacationType, SS_InterviewEvaluationFormRequest.Code AS RequestSerial, SS_InterviewEvaluationFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_InterviewEvaluationFormRequest.RequestDate, 'طلب استمارة تقييم المقابلة' AS RequestArbName, 'Interview Evaluation Form Request' AS RequestEngName, 
                  'SS_00197' AS FormCode, RequestStautsTypeID
FROM     SS_InterviewEvaluationFormRequest JOIN
                  hrs_Employees ON SS_InterviewEvaluationFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*11============================     طلب استمارة تصعيد حالات الاعتداء ==============================================*/ UNION
SELECT SS_AssaultEscalationFormRequest.ID, '' AS VacationType, SS_AssaultEscalationFormRequest.Code AS RequestSerial, SS_AssaultEscalationFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AssaultEscalationFormRequest.RequestDate, 'طلب استمارة تصعيد حالات الاعتداء' AS RequestArbName, 'Assault Escalation Form Request' AS RequestEngName, 
                  'SS_00198' AS FormCode, RequestStautsTypeID
FROM     SS_AssaultEscalationFormRequest JOIN
                  hrs_Employees ON SS_AssaultEscalationFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*12============================   طلب استمارة تضارب المصالح ==============================================*/ UNION
SELECT SS_ConflictofInterestFormRequest.ID, '' AS VacationType, SS_ConflictofInterestFormRequest.Code AS RequestSerial, SS_ConflictofInterestFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ConflictofInterestFormRequest.RequestDate, 'طلب استمارة تضارب المصالح' AS RequestArbName, 'Conflict of Interest Form Request' AS RequestEngName, 
                  'SS_00199' AS FormCode, RequestStautsTypeID
FROM     SS_ConflictofInterestFormRequest JOIN
                  hrs_Employees ON SS_ConflictofInterestFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*13============================   طلب استمارة امتيازات الأطباء ==============================================*/ UNION
SELECT SS_PhysiciansPrivilegingFormRequest.ID, '' AS VacationType, SS_PhysiciansPrivilegingFormRequest.Code AS RequestSerial, SS_PhysiciansPrivilegingFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_PhysiciansPrivilegingFormRequest.RequestDate, 'طلب استمارة امتيازات الأطباء' AS RequestArbName, 'Physicians Privileging Form Request' AS RequestEngName, 
                  'SS_001910' AS FormCode, RequestStautsTypeID
FROM     SS_PhysiciansPrivilegingFormRequest JOIN
                  hrs_Employees ON SS_PhysiciansPrivilegingFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*14============================   طلب دعم رعاية الأطفال ==============================================*/ UNION
SELECT SS_DaycareSupportReaquest.ID, '' AS VacationType, SS_DaycareSupportReaquest.Code AS RequestSerial, SS_DaycareSupportReaquest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_DaycareSupportReaquest.RequestDate, 'طلب دعم رعاية الأطفال' AS RequestArbName, 'Daycare Support' AS RequestEngName, 'SS_001911' AS FormCode, RequestStautsTypeID
FROM     SS_DaycareSupportReaquest JOIN
                  hrs_Employees ON SS_DaycareSupportReaquest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*15============================طلب دعم التعليم  ==============================================*/ UNION
SELECT SS_EducationSupportRequest.ID, '' AS VacationType, SS_EducationSupportRequest.Code AS RequestSerial, SS_EducationSupportRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EducationSupportRequest.RequestDate, 'طلب دعم التعليم' AS RequestArbName, 'Education Support' AS RequestEngName, 'SS_001912' AS FormCode, RequestStautsTypeID
FROM     SS_EducationSupportRequest JOIN
                  hrs_Employees ON SS_EducationSupportRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*16===========================طلب  بدل السكن مقدم  ==============================================*/ UNION
SELECT SS_AdvanceHousingRequest.ID, '' AS VacationType, SS_AdvanceHousingRequest.Code AS RequestSerial, SS_AdvanceHousingRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_AdvanceHousingRequest.RequestDate, 'طلب  بدل السكن مقدم' AS RequestArbName, ' Advance Housing Request' AS RequestEngName, 'SS_001913' AS FormCode, RequestStautsTypeID
FROM     SS_AdvanceHousingRequest JOIN
                  hrs_Employees ON SS_AdvanceHousingRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*17===========================طلب  الراتب مقدم  ==============================================*/ UNION
SELECT SS_AdvanceSalaryRequest.ID, '' AS VacationType, SS_AdvanceSalaryRequest.Code AS RequestSerial, SS_AdvanceSalaryRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_AdvanceSalaryRequest.RequestDate, 'طلب الراتب مقدم' AS RequestArbName, 'Advance Salary Request' AS RequestEngName, 'SS_001914' AS FormCode, RequestStautsTypeID
FROM     SS_AdvanceSalaryRequest JOIN
                  hrs_Employees ON SS_AdvanceSalaryRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*18===========================طلب خطاب الغرفة التجارية ==============================================*/ UNION
SELECT SS_ChamberofCommerceLetterRequest.ID, '' AS VacationType, SS_ChamberofCommerceLetterRequest.Code AS RequestSerial, SS_ChamberofCommerceLetterRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ChamberofCommerceLetterRequest.RequestDate, 'طلب خطاب الغرفة التجارية' AS RequestArbName, 'Chamber of Commerce Letter Reques' AS RequestEngName, 
                  'SS_001915' AS FormCode, RequestStautsTypeID
FROM     SS_ChamberofCommerceLetterRequest JOIN
                  hrs_Employees ON SS_ChamberofCommerceLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*19===========================طلب خطاب تصنيف هيئة التخصصات الطبية ==============================================*/ UNION
SELECT SS_SCFHSLetterRequest.ID, '' AS VacationType, SS_SCFHSLetterRequest.Code AS RequestSerial, SS_SCFHSLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_SCFHSLetterRequest.RequestDate, 'طلب خطاب تصنيف هيئة التخصصات الطبية' AS RequestArbName, 'SCFHS Letter Request' AS RequestEngName, 'SS_001916' AS FormCode, RequestStautsTypeID
FROM     SS_SCFHSLetterRequest JOIN
                  hrs_Employees ON SS_SCFHSLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*20===========================طلب خطاب تعريف الراتب ==============================================*/ UNION
SELECT SS_PaySlipRequest.ID, '' AS VacationType, SS_PaySlipRequest.Code AS RequestSerial, SS_PaySlipRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_PaySlipRequest.RequestDate, 'طلب خطاب تعريف الراتب' AS RequestArbName, 'Pay Slip Request' AS RequestEngName, 'SS_001917' AS FormCode, RequestStautsTypeID
FROM     SS_PaySlipRequest JOIN
                  hrs_Employees ON SS_PaySlipRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*21============================خطابات الإبلاغ عن التباين  ==============================================*/ UNION
SELECT SS_OccurrenceVarianceReportingLetters.ID, '' AS VacationType, SS_OccurrenceVarianceReportingLetters.Code AS RequestSerial, SS_OccurrenceVarianceReportingLetters.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_OccurrenceVarianceReportingLetters.RequestDate, '
خطابات الإبلاغ عن التباين' AS RequestArbName, 'Occurrence Variance Reporting Letters' AS RequestEngName, 
                  'SS_001918' AS FormCode, RequestStautsTypeID
FROM     SS_OccurrenceVarianceReportingLetters JOIN
                  hrs_Employees ON SS_OccurrenceVarianceReportingLetters.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*22============================طلب وقت اضافي  ==============================================*/ UNION
SELECT SS_OvertimeRequest.ID, '' AS VacationType, SS_OvertimeRequest.Code AS RequestSerial, SS_OvertimeRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_OvertimeRequest.RequestDate, 'طلب وقت اضافي' AS RequestArbName, 'Over Time' AS RequestEngName, 'SS_001919' AS FormCode, RequestStautsTypeID
FROM     SS_OvertimeRequest JOIN
                  hrs_Employees ON SS_OvertimeRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*23===========================طلب تعويض رسوم التعليم  ================================*/ UNION
SELECT SS_EducationFeesCompensationApplication.ID, '' AS VacationType, SS_EducationFeesCompensationApplication.Code AS RequestSerial, SS_EducationFeesCompensationApplication.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_EducationFeesCompensationApplication.RequestDate, 'طلب تعويض رسوم التعليم' AS RequestArbName, 'Education Fees Compensation Application' AS RequestEngName, 
                  'SS_001920' AS FormCode, RequestStautsTypeID
FROM     SS_EducationFeesCompensationApplication JOIN
                  hrs_Employees ON SS_EducationFeesCompensationApplication.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*24===========================طلب تحديث حساب البنك  ================================*/ UNION
SELECT SS_BankAccountUpdate.ID, '' AS VacationType, SS_BankAccountUpdate.Code AS RequestSerial, SS_BankAccountUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_BankAccountUpdate.RequestDate, 'طلب تحديث حساب البنك' AS RequestArbName, 'Bank Account Update' AS RequestEngName, 'SS_001921' AS FormCode, RequestStautsTypeID
FROM     SS_BankAccountUpdate JOIN
                  hrs_Employees ON SS_BankAccountUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*25===========================طلب تحديث معلومات الاتصال  ================================*/ UNION
SELECT SS_ContactInformationUpdate.ID, '' AS VacationType, SS_ContactInformationUpdate.Code AS RequestSerial, SS_ContactInformationUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ContactInformationUpdate.RequestDate, 'طلب تحديث معلومات الاتصال' AS RequestArbName, 'Contact Information Update' AS RequestEngName, 'SS_001922' AS FormCode, RequestStautsTypeID
FROM     SS_ContactInformationUpdate JOIN
                  hrs_Employees ON SS_ContactInformationUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*26===========================طلب تحديث معلومات المعالين  ================================*/ UNION
SELECT SS_DependentsInformationUpdate.ID, '' AS VacationType, SS_DependentsInformationUpdate.Code AS RequestSerial, SS_DependentsInformationUpdate.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_DependentsInformationUpdate.RequestDate, 'طلب تحديث معلومات المعالين' AS RequestArbName, 'Dependents Information Update' AS RequestEngName, 
                  'SS_001923' AS FormCode, RequestStautsTypeID
FROM     SS_DependentsInformationUpdate JOIN
                  hrs_Employees ON SS_DependentsInformationUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*27===========================طلب تعديلات التأمين الطبي  ================================*/ UNION
SELECT SS_MedicalInsuranceAdjustments.ID, '' AS VacationType, SS_MedicalInsuranceAdjustments.Code AS RequestSerial, SS_MedicalInsuranceAdjustments.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_MedicalInsuranceAdjustments.RequestDate, 'طلب تعديلات التأمين الطبي' AS RequestArbName, 'Medical Insurance Adjustments' AS RequestEngName, 'SS_001924' AS FormCode, 
                  RequestStautsTypeID
FROM     SS_MedicalInsuranceAdjustments JOIN
                  hrs_Employees ON SS_MedicalInsuranceAdjustments.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*28===========================طلب تحديثات المستندات القانونية الأخرى  ================================*/ UNION
SELECT SS_OtherLegalDocumentUpdates.ID, '' AS VacationType, SS_OtherLegalDocumentUpdates.Code AS RequestSerial, SS_OtherLegalDocumentUpdates.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_OtherLegalDocumentUpdates.RequestDate, 'طلب تحديثات المستندات القانونية الأخرى' AS RequestArbName, 'Other Legal Document Updates' AS RequestEngName, 
                  'SS_001925' AS FormCode, RequestStautsTypeID
FROM     SS_OtherLegalDocumentUpdates JOIN
                  hrs_Employees ON SS_OtherLegalDocumentUpdates.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*29===========================طلب تحديث ملف الموظف  ================================*/ UNION
SELECT SS_EmployeeFileUpdate.ID, '' AS VacationType, SS_EmployeeFileUpdate.Code AS RequestSerial, SS_EmployeeFileUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EmployeeFileUpdate.RequestDate, 'طلب تحديث ملف الموظف' AS RequestArbName, 'Employee File Update' AS RequestEngName, 'SS_001926' AS FormCode, RequestStautsTypeID
FROM     SS_EmployeeFileUpdate JOIN
                  hrs_Employees ON SS_EmployeeFileUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*30===========================طلب سفر عمل او تدريب  ================================*/ UNION
SELECT SS_BusinessORTrainingTravel.ID, '' AS VacationType, SS_BusinessORTrainingTravel.Code AS RequestSerial, SS_BusinessORTrainingTravel.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_BusinessORTrainingTravel.RequestDate, 'طلب سفر عمل او تدريب' AS RequestArbName, 'Business OR Training Travel' AS RequestEngName, 'SS_001927' AS FormCode, RequestStautsTypeID
FROM     SS_BusinessORTrainingTravel JOIN
                  hrs_Employees ON SS_BusinessORTrainingTravel.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*31===========================الطلبات المتعلقة بالتذاكر السنوية  ================================*/ UNION
SELECT SS_AnnualTicketRelatedRequests.ID, '' AS VacationType, SS_AnnualTicketRelatedRequests.Code AS RequestSerial, SS_AnnualTicketRelatedRequests.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AnnualTicketRelatedRequests.RequestDate, 'الطلبات المتعلقة بالتذاكر السنوية' AS RequestArbName, 'Annual Ticket Related Requests' AS RequestEngName, 
                  'SS_001928' AS FormCode, RequestStautsTypeID
FROM     SS_AnnualTicketRelatedRequests JOIN
                  hrs_Employees ON SS_AnnualTicketRelatedRequests.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID

"
        ExecuteUpdate(SQL)
        SQL = "ALTER TABLE dbo.hrs_VacationsTypes ADD
	ConsiderAllowedDays bit NULL,
	TimesNoInYear int NULL,
	AllowedDaysNo int NULL;


"
        ExecuteUpdate(SQL)
        SQL = "ALTER TABLE dbo.SS_RequestTypes ADD
	RequiredAttach bit NULL;"
        ExecuteUpdate(SQL)
        SQL = "
alter table SS_BusinessORTrainingTravel add 
FromDate varchar(50) null,
ToDate varchar(50) null,
TravelReasonID int null;
"
        ExecuteUpdate(SQL)
        SQL =
            "
CREATE TABLE [dbo].[SS_TravelReasons](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[EngName] [varchar](50) NULL,
	[ArbName] [varchar](50) NULL,
 CONSTRAINT [PK_SS_TravelReasons] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
"
        ExecuteUpdate(SQL)

        SQL = "Alter Table  SS_PaySlipRequest Add  Recipient varchar(150) null "
        ExecuteUpdate(SQL)

        SQL = "ALTER TABLE dbo.hrs_VacationsTypes ADD
	ExcludedFromSSRequests bit NULL"
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
GO
"
        ExecuteUpdate(SQL)


        SQL = "CREATE TABLE [dbo].[SS_ChangeWorkHoursRequest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[RequestDate] [date] NULL,
	[Remarks] [varchar](5000) NULL,
	[RequesterUser] [varchar](50) NULL,
	[FromTime] [varchar](50) NULL,
	[ToTime] [varchar](50) NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[RequestStautsTypeID] [int] NULL,
 CONSTRAINT [PK_SS_ChangeWorkHoursRequest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[Code] ASC,
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY];
"
        ExecuteUpdate(SQL)

        SQL = "CREATE or alter VIEW [dbo].[SS_VFollowup]
AS
/*-1==============================الاجازة==================*/ SELECT SS_VacationRequest.ID, SS_VacationRequest.VacationType, SS_VacationRequest.Code AS RequestSerial, SS_VacationRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_VacationRequest.RequestDate, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي اداري' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي طبي' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي تشغيلي' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'اجازة اخرى تشغيلي' ELSE hrs_VacationsTypes.ArbName4S END AS RequestArbName, 
                  CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Admin' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Medical' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Operational' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'Other Vacation Operational' ELSE hrs_VacationsTypes.EngName END AS RequestEngName, 
                  CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0011' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0012' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0030' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'SS_0031' WHEN SS_VacationRequest.VacationType = 'SS_0018' THEN 'SS_0018' ELSE 'SS_0013' END AS FormCode, 
                  RequestStautsTypeID
FROM     SS_VacationRequest JOIN
                  hrs_Employees ON SS_VacationRequest.EmployeeID = hrs_Employees.id JOIN
                  hrs_VacationsTypes ON SS_VacationRequest.VacationTypeID = hrs_VacationsTypes.ID INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*-2==============================الاستئذان=======================================================*/ UNION
SELECT SS_ExecuseRequest.ID, '' AS VacationType, SS_ExecuseRequest.Code AS RequestSerial, SS_ExecuseRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ExecuseRequest.RequestDate, 'طلب استئذان' AS RequestArbName, 'Execuse Request' AS RequestEngName, 'SS_0014' AS FormCode, RequestStautsTypeID
FROM     SS_ExecuseRequest JOIN
                  hrs_Employees ON SS_ExecuseRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*3 ============================انهاء خدمة ===============================================================*/ UNION
SELECT SS_EndOfServiceRequest.ID, '' AS VacationType, SS_EndOfServiceRequest.Code AS RequestSerial, SS_EndOfServiceRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EndOfServiceRequest.RequestDate, CASE WHEN FormCode = 'SS_0015' THEN 'طلب انهاء خدمة' ELSE 'طلب انهاء خدمة طبي' END AS RequestArbName, 
                  CASE WHEN FormCode = 'SS_0015' THEN 'End Of Service Request' ELSE 'End Of Service Medical Request' END AS RequestEngName, FormCode AS FormCode, RequestStautsTypeID
FROM     SS_EndOfServiceRequest JOIN
                  hrs_Employees ON SS_EndOfServiceRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*4============================خروج وعودة ==============================================*/ UNION
SELECT SS_ExitEntryRequest.ID, '' AS VacationType, SS_ExitEntryRequest.Code AS RequestSerial, SS_ExitEntryRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ExitEntryRequest.RequestDate, 'طلب خروج وعودة' AS RequestArbName, 'Exit & Re-entry Requests' AS RequestEngName, 'SS_00191' AS FormCode, RequestStautsTypeID
FROM     SS_ExitEntryRequest JOIN
                  hrs_Employees ON SS_ExitEntryRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*5============================ تأشيرة الأسرة أو تأشيرة الزيارة ==============================================*/ UNION
SELECT SS_VisaRequest.ID, '' AS VacationType, SS_VisaRequest.Code AS RequestSerial, SS_VisaRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_VisaRequest.RequestDate, 
                  'طلب تأشيرة الأسرة أو تأشيرة الزيارة' AS RequestArbName, 'Family visa or Visit visa Requests' AS RequestEngName, 'SS_00192' AS FormCode, RequestStautsTypeID
FROM     SS_VisaRequest JOIN
                  hrs_Employees ON SS_VisaRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*6============================  خطاب قرض ==============================================*/ UNION
SELECT SS_LoanLetterRequest.ID, '' AS VacationType, SS_LoanLetterRequest.Code AS RequestSerial, SS_LoanLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_LoanLetterRequest.RequestDate, 'طلب خطاب قرض' AS RequestArbName, 'Loan Letter Request' AS RequestEngName, 'SS_00193' AS FormCode, RequestStautsTypeID
FROM     SS_LoanLetterRequest JOIN
                  hrs_Employees ON SS_LoanLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*7============================   خطاب آخر ==============================================*/ UNION
SELECT SS_OtherLetterRequest.ID, '' AS VacationType, SS_OtherLetterRequest.Code AS RequestSerial, SS_OtherLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_OtherLetterRequest.RequestDate, 'طلب خطاب آخر' AS RequestArbName, 'Other Letter Request' AS RequestEngName, 'SS_00194' AS FormCode, RequestStautsTypeID
FROM     SS_OtherLetterRequest JOIN
                  hrs_Employees ON SS_OtherLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*8============================    طلب تدريب ==============================================*/ UNION
SELECT SS_TrainingRequest.ID, '' AS VacationType, SS_TrainingRequest.Code AS RequestSerial, SS_TrainingRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_TrainingRequest.RequestDate, 'طلب تدريب' AS RequestArbName, 'Training Request' AS RequestEngName, 'SS_00195' AS FormCode, RequestStautsTypeID
FROM     SS_TrainingRequest JOIN
                  hrs_Employees ON SS_TrainingRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*9============================     استمارة التظلم ==============================================*/ UNION
SELECT SS_GrievanceFormRequest.ID, '' AS VacationType, SS_GrievanceFormRequest.Code AS RequestSerial, SS_GrievanceFormRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_GrievanceFormRequest.RequestDate, 'طلب استمارة التظلم' AS RequestArbName, 'Grievance Form Request' AS RequestEngName, 'SS_00196' AS FormCode, RequestStautsTypeID
FROM     SS_GrievanceFormRequest JOIN
                  hrs_Employees ON SS_GrievanceFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*10============================     طلب استمارة تقييم المقابلة ==============================================*/ UNION
SELECT SS_InterviewEvaluationFormRequest.ID, '' AS VacationType, SS_InterviewEvaluationFormRequest.Code AS RequestSerial, SS_InterviewEvaluationFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_InterviewEvaluationFormRequest.RequestDate, 'طلب استمارة تقييم المقابلة' AS RequestArbName, 'Interview Evaluation Form Request' AS RequestEngName, 
                  'SS_00197' AS FormCode, RequestStautsTypeID
FROM     SS_InterviewEvaluationFormRequest JOIN
                  hrs_Employees ON SS_InterviewEvaluationFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*11============================     طلب استمارة تصعيد حالات الاعتداء ==============================================*/ UNION
SELECT SS_AssaultEscalationFormRequest.ID, '' AS VacationType, SS_AssaultEscalationFormRequest.Code AS RequestSerial, SS_AssaultEscalationFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AssaultEscalationFormRequest.RequestDate, 'طلب استمارة تصعيد حالات الاعتداء' AS RequestArbName, 'Assault Escalation Form Request' AS RequestEngName, 
                  'SS_00198' AS FormCode, RequestStautsTypeID
FROM     SS_AssaultEscalationFormRequest JOIN
                  hrs_Employees ON SS_AssaultEscalationFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*12============================   طلب استمارة تضارب المصالح ==============================================*/ UNION
SELECT SS_ConflictofInterestFormRequest.ID, '' AS VacationType, SS_ConflictofInterestFormRequest.Code AS RequestSerial, SS_ConflictofInterestFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ConflictofInterestFormRequest.RequestDate, 'طلب استمارة تضارب المصالح' AS RequestArbName, 'Conflict of Interest Form Request' AS RequestEngName, 
                  'SS_00199' AS FormCode, RequestStautsTypeID
FROM     SS_ConflictofInterestFormRequest JOIN
                  hrs_Employees ON SS_ConflictofInterestFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*13============================   طلب استمارة امتيازات الأطباء ==============================================*/ UNION
SELECT SS_PhysiciansPrivilegingFormRequest.ID, '' AS VacationType, SS_PhysiciansPrivilegingFormRequest.Code AS RequestSerial, SS_PhysiciansPrivilegingFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_PhysiciansPrivilegingFormRequest.RequestDate, 'طلب استمارة امتيازات الأطباء' AS RequestArbName, 'Physicians Privileging Form Request' AS RequestEngName, 
                  'SS_001910' AS FormCode, RequestStautsTypeID
FROM     SS_PhysiciansPrivilegingFormRequest JOIN
                  hrs_Employees ON SS_PhysiciansPrivilegingFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*14============================   طلب دعم رعاية الأطفال ==============================================*/ UNION
SELECT SS_DaycareSupportReaquest.ID, '' AS VacationType, SS_DaycareSupportReaquest.Code AS RequestSerial, SS_DaycareSupportReaquest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_DaycareSupportReaquest.RequestDate, 'طلب دعم رعاية الأطفال' AS RequestArbName, 'Daycare Support' AS RequestEngName, 'SS_001911' AS FormCode, RequestStautsTypeID
FROM     SS_DaycareSupportReaquest JOIN
                  hrs_Employees ON SS_DaycareSupportReaquest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*15============================طلب دعم التعليم  ==============================================*/ UNION
SELECT SS_EducationSupportRequest.ID, '' AS VacationType, SS_EducationSupportRequest.Code AS RequestSerial, SS_EducationSupportRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EducationSupportRequest.RequestDate, 'طلب دعم التعليم' AS RequestArbName, 'Education Support' AS RequestEngName, 'SS_001912' AS FormCode, RequestStautsTypeID
FROM     SS_EducationSupportRequest JOIN
                  hrs_Employees ON SS_EducationSupportRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*16===========================طلب  بدل السكن مقدم  ==============================================*/ UNION
SELECT SS_AdvanceHousingRequest.ID, '' AS VacationType, SS_AdvanceHousingRequest.Code AS RequestSerial, SS_AdvanceHousingRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_AdvanceHousingRequest.RequestDate, 'طلب  بدل السكن مقدم' AS RequestArbName, ' Advance Housing Request' AS RequestEngName, 'SS_001913' AS FormCode, RequestStautsTypeID
FROM     SS_AdvanceHousingRequest JOIN
                  hrs_Employees ON SS_AdvanceHousingRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*17===========================طلب  الراتب مقدم  ==============================================*/ UNION
SELECT SS_AdvanceSalaryRequest.ID, '' AS VacationType, SS_AdvanceSalaryRequest.Code AS RequestSerial, SS_AdvanceSalaryRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_AdvanceSalaryRequest.RequestDate, 'طلب الراتب مقدم' AS RequestArbName, 'Advance Salary Request' AS RequestEngName, 'SS_001914' AS FormCode, RequestStautsTypeID
FROM     SS_AdvanceSalaryRequest JOIN
                  hrs_Employees ON SS_AdvanceSalaryRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*18===========================طلب خطاب الغرفة التجارية ==============================================*/ UNION
SELECT SS_ChamberofCommerceLetterRequest.ID, '' AS VacationType, SS_ChamberofCommerceLetterRequest.Code AS RequestSerial, SS_ChamberofCommerceLetterRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ChamberofCommerceLetterRequest.RequestDate, 'طلب خطاب الغرفة التجارية' AS RequestArbName, 'Chamber of Commerce Letter Reques' AS RequestEngName, 
                  'SS_001915' AS FormCode, RequestStautsTypeID
FROM     SS_ChamberofCommerceLetterRequest JOIN
                  hrs_Employees ON SS_ChamberofCommerceLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*19===========================طلب خطاب تصنيف هيئة التخصصات الطبية ==============================================*/ UNION
SELECT SS_SCFHSLetterRequest.ID, '' AS VacationType, SS_SCFHSLetterRequest.Code AS RequestSerial, SS_SCFHSLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_SCFHSLetterRequest.RequestDate, 'طلب خطاب تصنيف هيئة التخصصات الطبية' AS RequestArbName, 'SCFHS Letter Request' AS RequestEngName, 'SS_001916' AS FormCode, RequestStautsTypeID
FROM     SS_SCFHSLetterRequest JOIN
                  hrs_Employees ON SS_SCFHSLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*20===========================طلب خطاب تعريف الراتب ==============================================*/ UNION
SELECT SS_PaySlipRequest.ID, '' AS VacationType, SS_PaySlipRequest.Code AS RequestSerial, SS_PaySlipRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_PaySlipRequest.RequestDate, 'طلب خطاب تعريف الراتب' AS RequestArbName, 'Pay Slip Request' AS RequestEngName, 'SS_001917' AS FormCode, RequestStautsTypeID
FROM     SS_PaySlipRequest JOIN
                  hrs_Employees ON SS_PaySlipRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*21============================خطابات الإبلاغ عن التباين  ==============================================*/ UNION
SELECT SS_OccurrenceVarianceReportingLetters.ID, '' AS VacationType, SS_OccurrenceVarianceReportingLetters.Code AS RequestSerial, SS_OccurrenceVarianceReportingLetters.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_OccurrenceVarianceReportingLetters.RequestDate, '
خطابات الإبلاغ عن التباين' AS RequestArbName, 'Occurrence Variance Reporting Letters' AS RequestEngName, 
                  'SS_001918' AS FormCode, RequestStautsTypeID
FROM     SS_OccurrenceVarianceReportingLetters JOIN
                  hrs_Employees ON SS_OccurrenceVarianceReportingLetters.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*22============================طلب وقت اضافي  ==============================================*/ UNION
SELECT SS_OvertimeRequest.ID, '' AS VacationType, SS_OvertimeRequest.Code AS RequestSerial, SS_OvertimeRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_OvertimeRequest.RequestDate, 'طلب وقت اضافي' AS RequestArbName, 'Over Time' AS RequestEngName, 'SS_001919' AS FormCode, RequestStautsTypeID
FROM     SS_OvertimeRequest JOIN
                  hrs_Employees ON SS_OvertimeRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*23===========================طلب تعويض رسوم التعليم  ================================*/ UNION
SELECT SS_EducationFeesCompensationApplication.ID, '' AS VacationType, SS_EducationFeesCompensationApplication.Code AS RequestSerial, SS_EducationFeesCompensationApplication.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_EducationFeesCompensationApplication.RequestDate, 'طلب تعويض رسوم التعليم' AS RequestArbName, 'Education Fees Compensation Application' AS RequestEngName, 
                  'SS_001920' AS FormCode, RequestStautsTypeID
FROM     SS_EducationFeesCompensationApplication JOIN
                  hrs_Employees ON SS_EducationFeesCompensationApplication.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*24===========================طلب تحديث حساب البنك  ================================*/ UNION
SELECT SS_BankAccountUpdate.ID, '' AS VacationType, SS_BankAccountUpdate.Code AS RequestSerial, SS_BankAccountUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_BankAccountUpdate.RequestDate, 'طلب تحديث حساب البنك' AS RequestArbName, 'Bank Account Update' AS RequestEngName, 'SS_001921' AS FormCode, RequestStautsTypeID
FROM     SS_BankAccountUpdate JOIN
                  hrs_Employees ON SS_BankAccountUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*25===========================طلب تحديث معلومات الاتصال  ================================*/ UNION
SELECT SS_ContactInformationUpdate.ID, '' AS VacationType, SS_ContactInformationUpdate.Code AS RequestSerial, SS_ContactInformationUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ContactInformationUpdate.RequestDate, 'طلب تحديث معلومات الاتصال' AS RequestArbName, 'Contact Information Update' AS RequestEngName, 'SS_001922' AS FormCode, RequestStautsTypeID
FROM     SS_ContactInformationUpdate JOIN
                  hrs_Employees ON SS_ContactInformationUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*26===========================طلب تحديث معلومات المعالين  ================================*/ UNION
SELECT SS_DependentsInformationUpdate.ID, '' AS VacationType, SS_DependentsInformationUpdate.Code AS RequestSerial, SS_DependentsInformationUpdate.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_DependentsInformationUpdate.RequestDate, 'طلب تحديث معلومات المعالين' AS RequestArbName, 'Dependents Information Update' AS RequestEngName, 
                  'SS_001923' AS FormCode, RequestStautsTypeID
FROM     SS_DependentsInformationUpdate JOIN
                  hrs_Employees ON SS_DependentsInformationUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*27===========================طلب تعديلات التأمين الطبي  ================================*/ UNION
SELECT SS_MedicalInsuranceAdjustments.ID, '' AS VacationType, SS_MedicalInsuranceAdjustments.Code AS RequestSerial, SS_MedicalInsuranceAdjustments.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_MedicalInsuranceAdjustments.RequestDate, 'طلب تعديلات التأمين الطبي' AS RequestArbName, 'Medical Insurance Adjustments' AS RequestEngName, 
                  'SS_001924' AS FormCode, RequestStautsTypeID
FROM     SS_MedicalInsuranceAdjustments JOIN
                  hrs_Employees ON SS_MedicalInsuranceAdjustments.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*28===========================طلب تحديثات المستندات القانونية الأخرى  ================================*/ UNION
SELECT SS_OtherLegalDocumentUpdates.ID, '' AS VacationType, SS_OtherLegalDocumentUpdates.Code AS RequestSerial, SS_OtherLegalDocumentUpdates.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_OtherLegalDocumentUpdates.RequestDate, 'طلب تحديثات المستندات القانونية الأخرى' AS RequestArbName, 'Other Legal Document Updates' AS RequestEngName, 
                  'SS_001925' AS FormCode, RequestStautsTypeID
FROM     SS_OtherLegalDocumentUpdates JOIN
                  hrs_Employees ON SS_OtherLegalDocumentUpdates.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*29===========================طلب تحديث ملف الموظف  ================================*/ UNION
SELECT SS_EmployeeFileUpdate.ID, '' AS VacationType, SS_EmployeeFileUpdate.Code AS RequestSerial, SS_EmployeeFileUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EmployeeFileUpdate.RequestDate, 'طلب تحديث ملف الموظف' AS RequestArbName, 'Employee File Update' AS RequestEngName, 'SS_001926' AS FormCode, RequestStautsTypeID
FROM     SS_EmployeeFileUpdate JOIN
                  hrs_Employees ON SS_EmployeeFileUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*30===========================طلب سفر عمل او تدريب  ================================*/ UNION
SELECT SS_BusinessORTrainingTravel.ID, '' AS VacationType, SS_BusinessORTrainingTravel.Code AS RequestSerial, SS_BusinessORTrainingTravel.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_BusinessORTrainingTravel.RequestDate, 'طلب سفر عمل او تدريب' AS RequestArbName, 'Business OR Training Travel' AS RequestEngName, 'SS_001927' AS FormCode, RequestStautsTypeID
FROM     SS_BusinessORTrainingTravel JOIN
                  hrs_Employees ON SS_BusinessORTrainingTravel.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*31===========================الطلبات المتعلقة بالتذاكر السنوية  ================================*/ UNION
SELECT SS_AnnualTicketRelatedRequests.ID, '' AS VacationType, SS_AnnualTicketRelatedRequests.Code AS RequestSerial, SS_AnnualTicketRelatedRequests.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AnnualTicketRelatedRequests.RequestDate, 'الطلبات المتعلقة بالتذاكر السنوية' AS RequestArbName, 'Annual Ticket Related Requests' AS RequestEngName, 
                  'SS_001928' AS FormCode, RequestStautsTypeID
FROM     SS_AnnualTicketRelatedRequests JOIN
                  hrs_Employees ON SS_AnnualTicketRelatedRequests.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*32===========================طلب تغيير الدوام  ================================*/ UNION
SELECT SS_ChangeWorkHoursRequest.ID, '' AS VacationType, SS_ChangeWorkHoursRequest.Code AS RequestSerial, SS_ChangeWorkHoursRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ChangeWorkHoursRequest.RequestDate, 'طلب تغيير الدوام' AS RequestArbName, 'Change Work Hours' AS RequestEngName, 'SS_001929' AS FormCode, RequestStautsTypeID
FROM     SS_ChangeWorkHoursRequest JOIN
                  hrs_Employees ON SS_ChangeWorkHoursRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
;"
        ExecuteUpdate(SQL)


        SQL = "
create or ALTER VIEW [dbo].[SS_VNotification]
AS
/*1 ============================الاجازة ==============================================*/ SELECT RequestSerial AS ID, FormCode, ConfigID, SS_VacationRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_VacationRequest.RequestDate, 103) AS RequestDate, SS_VacationRequest.VacationType, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي اداري' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي طبي' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي تشغيلي' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'اجازة اخرى تشغيلي' ELSE hrs_VacationsTypes.ArbName4S END AS RequestArbName, 
                  CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Admin' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Medical' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Operational' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'Other Vacation Operational' ELSE hrs_VacationsTypes.EngName END AS RequestEngName, 
                  SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_VacationRequest ON SS_RequestActions.RequestSerial = SS_VacationRequest.ID AND SS_RequestActions.EmployeeID = SS_VacationRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID JOIN
                  hrs_VacationsTypes ON SS_VacationRequest.VacationTypeID = hrs_VacationsTypes.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND (FormCode = 'SS_0011' OR
                  FormCode = 'SS_0012' OR
                  FormCode = 'SS_0013' OR
                  FormCode = 'SS_0030' OR
                  FormCode = 'SS_0031' OR
                  FormCode = 'SS_0018')
/* 2 ============================الاستئذان ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ExecuseRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) AS RequestDate, '' AS ExecuseType, 
                  'طلب استئذان' AS RequestArbName, 'Execuse Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ExecuseRequest ON SS_RequestActions.RequestSerial = SS_ExecuseRequest.ID AND SS_RequestActions.EmployeeID = SS_ExecuseRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_0014'
/*3 ============================انهاء خدمة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EndOfServiceRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب انهاء خدمة' AS RequestArbType, 'End Of Service Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EndOfServiceRequest ON SS_RequestActions.RequestSerial = SS_EndOfServiceRequest.ID AND SS_RequestActions.EmployeeID = SS_EndOfServiceRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND (SS_RequestActions.FormCode = 'SS_0015' OR
                  SS_RequestActions.FormCode = 'SS_0019')
/*4============================خروج وعودة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ExitEntryRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ExitEntryRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خروج وعودة' AS RequestArbName, 'Exit & Re-entry Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ExitEntryRequest ON SS_RequestActions.RequestSerial = SS_ExitEntryRequest.ID AND SS_RequestActions.EmployeeID = SS_ExitEntryRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00191'
/*5============================ تأشيرة الأسرة أو تأشيرة الزيارة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_VisaRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_VisaRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تأشيرة الأسرة أو تأشيرة الزيارة' AS RequestArbName, 'Family visa or Visit visa Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_VisaRequest ON SS_RequestActions.RequestSerial = SS_VisaRequest.ID AND SS_RequestActions.EmployeeID = SS_VisaRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00192'
/*6============================  خطاب قرض ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_LoanLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_LoanLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب قرض' AS RequestArbName, 'Loan Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_LoanLetterRequest ON SS_RequestActions.RequestSerial = SS_LoanLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_LoanLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00193'
/*7============================   خطاب آخر ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OtherLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OtherLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب آخر' AS RequestArbName, 'Other Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OtherLetterRequest ON SS_RequestActions.RequestSerial = SS_OtherLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_OtherLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00194'
/*8============================    طلب تدريب ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_TrainingRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_TrainingRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تدريب' AS RequestArbName, 'Training Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_TrainingRequest ON SS_RequestActions.RequestSerial = SS_TrainingRequest.ID AND SS_RequestActions.EmployeeID = SS_TrainingRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00195'
/*9============================     استمارة التظلم ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_GrievanceFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_GrievanceFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب استمارة التظلم' AS RequestArbName, 'Grievance Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_GrievanceFormRequest ON SS_RequestActions.RequestSerial = SS_GrievanceFormRequest.ID AND SS_RequestActions.EmployeeID = SS_GrievanceFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00196'
/*10============================     طلب استمارة تقييم المقابلة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_InterviewEvaluationFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_InterviewEvaluationFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة تقييم المقابلة' AS RequestArbName, 'Interview Evaluation Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_InterviewEvaluationFormRequest ON SS_RequestActions.RequestSerial = SS_InterviewEvaluationFormRequest.ID AND SS_RequestActions.EmployeeID = SS_InterviewEvaluationFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00197'
/*11============================     طلب استمارة تصعيد حالات الاعتداء ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AssaultEscalationFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_AssaultEscalationFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة تصعيد حالات الاعتداء' AS RequestArbName, 'Assault Escalation Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AssaultEscalationFormRequest ON SS_RequestActions.RequestSerial = SS_AssaultEscalationFormRequest.ID AND SS_RequestActions.EmployeeID = SS_AssaultEscalationFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00198'
/*12============================   طلب استمارة تضارب المصالح ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ConflictofInterestFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ConflictofInterestFormRequest.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب استمارة تضارب المصالح' AS RequestArbName, 'Conflict of Interest Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ConflictofInterestFormRequest ON SS_RequestActions.RequestSerial = SS_ConflictofInterestFormRequest.ID AND SS_RequestActions.EmployeeID = SS_ConflictofInterestFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00199'
/*13============================   طلب استمارة امتيازات الأطباء ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_PhysiciansPrivilegingFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_PhysiciansPrivilegingFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة امتيازات الأطباء' AS RequestArbName, 'Physicians Privileging Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_PhysiciansPrivilegingFormRequest ON SS_RequestActions.RequestSerial = SS_PhysiciansPrivilegingFormRequest.ID AND SS_RequestActions.EmployeeID = SS_PhysiciansPrivilegingFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001910'
/*14============================   طلب دعم رعاية الأطفال ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_DaycareSupportReaquest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_DaycareSupportReaquest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب دعم رعاية الأطفال' AS RequestArbName, 'Daycare Support' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_DaycareSupportReaquest ON SS_RequestActions.RequestSerial = SS_DaycareSupportReaquest.ID AND SS_RequestActions.EmployeeID = SS_DaycareSupportReaquest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001911'
/*15============================طلب دعم التعليم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EducationSupportRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EducationSupportRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب دعم التعليم' AS RequestArbName, 'Education Support' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EducationSupportRequest ON SS_RequestActions.RequestSerial = SS_EducationSupportRequest.ID AND SS_RequestActions.EmployeeID = SS_EducationSupportRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001912'
/*16===========================طلب  بدل السكن مقدم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AdvanceHousingRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AdvanceHousingRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب  بدل السكن مقدم' AS RequestArbName, 'Advance Housing Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AdvanceHousingRequest ON SS_RequestActions.RequestSerial = SS_AdvanceHousingRequest.ID AND SS_RequestActions.EmployeeID = SS_AdvanceHousingRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001913'
/*17===========================طلب  الراتب مقدم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AdvanceSalaryRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AdvanceSalaryRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب  الراتب مقدم' AS RequestArbName, 'Advance Salary Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AdvanceSalaryRequest ON SS_RequestActions.RequestSerial = SS_AdvanceSalaryRequest.ID AND SS_RequestActions.EmployeeID = SS_AdvanceSalaryRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001914'
/*18===========================طلب خطاب الغرفة التجارية ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ChamberofCommerceLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_ChamberofCommerceLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب خطاب الغرفة التجارية' AS RequestArbName, 'Chamber of Commerce Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ChamberofCommerceLetterRequest ON SS_RequestActions.RequestSerial = SS_ChamberofCommerceLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_ChamberofCommerceLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001915'
/*19===========================طلب خطاب تصنيف هيئة التخصصات الطبية ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_SCFHSLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_SCFHSLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب تصنيف هيئة التخصصات الطبية' AS RequestArbName, 'SCFHS Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_SCFHSLetterRequest ON SS_RequestActions.RequestSerial = SS_SCFHSLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_SCFHSLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001916'
/*20===========================طلب خطاب تعريف الراتب ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_PaySlipRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_PaySlipRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطابتعريف الراتب' AS RequestArbName, 'Pay Slip Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_PaySlipRequest ON SS_RequestActions.RequestSerial = SS_PaySlipRequest.ID AND SS_RequestActions.EmployeeID = SS_PaySlipRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001917'
/*21============================خطابات الإبلاغ عن التباين  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OccurrenceVarianceReportingLetters.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_OccurrenceVarianceReportingLetters.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب خطابات الإبلاغ عن التباين' AS RequestArbName, 'Occurrence Variance Reporting Letter' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OccurrenceVarianceReportingLetters ON SS_RequestActions.RequestSerial = SS_OccurrenceVarianceReportingLetters.ID AND SS_RequestActions.EmployeeID = SS_OccurrenceVarianceReportingLetters.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001918'
/*22============================طلب وقت اضافي  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OvertimeRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OvertimeRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب وقت اضافي' AS RequestArbName, ' Over Time Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OvertimeRequest ON SS_RequestActions.RequestSerial = SS_OvertimeRequest.ID AND SS_RequestActions.EmployeeID = SS_OvertimeRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001919'
/*23===========================طلب تعويض رسوم التعليم  ================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EducationFeesCompensationApplication.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_EducationFeesCompensationApplication.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب تعويض رسوم التعليم' AS RequestArbName, 'Education Fees Compensation Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EducationFeesCompensationApplication ON SS_RequestActions.RequestSerial = SS_EducationFeesCompensationApplication.ID AND SS_RequestActions.EmployeeID = SS_EducationFeesCompensationApplication.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001920'
/*24===========================طلب تحديث حساب البنك  ================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_BankAccountUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_BankAccountUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث بيانات البنك' AS RequestArbName, 'Bank Acc Update Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_BankAccountUpdate ON SS_RequestActions.RequestSerial = SS_BankAccountUpdate.ID AND SS_RequestActions.EmployeeID = SS_BankAccountUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001921'
/*25============================طلب تحديث معلومات الاتصال  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ContactInformationUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ContactInformationUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث معلومات الاتصال' AS RequestArbName, 'Contact Information Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ContactInformationUpdate ON SS_RequestActions.RequestSerial = SS_ContactInformationUpdate.ID AND SS_RequestActions.EmployeeID = SS_ContactInformationUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001922'
/*26============================طلب تحديث معلومات المعالين  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_DependentsInformationUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_DependentsInformationUpdate.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تحديث معلومات المعالين' AS RequestArbName, 'Dependents Information Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_DependentsInformationUpdate ON SS_RequestActions.RequestSerial = SS_DependentsInformationUpdate.ID AND SS_RequestActions.EmployeeID = SS_DependentsInformationUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001923'
/*27============================طلب تعديلات التأمين الطبي  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_MedicalInsuranceAdjustments.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_MedicalInsuranceAdjustments.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تعديلات التأمين الطبي' AS RequestArbName, 'Medical Insurance Adjustments' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_MedicalInsuranceAdjustments ON SS_RequestActions.RequestSerial = SS_MedicalInsuranceAdjustments.ID AND SS_RequestActions.EmployeeID = SS_MedicalInsuranceAdjustments.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001924'
/*28======================طلب تحديثات المستندات القانونية الأخرى  =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OtherLegalDocumentUpdates.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OtherLegalDocumentUpdates.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تحديثات المستندات القانونية الأخرى' AS RequestArbName, 'Other Legal Document Updates' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OtherLegalDocumentUpdates ON SS_RequestActions.RequestSerial = SS_OtherLegalDocumentUpdates.ID AND SS_RequestActions.EmployeeID = SS_OtherLegalDocumentUpdates.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001925'
/*29======================طلب تحديثات المستندات القانونية الأخرى  =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EmployeeFileUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EmployeeFileUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث ملف الموظف' AS RequestArbName, 'Employee File Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EmployeeFileUpdate ON SS_RequestActions.RequestSerial = SS_EmployeeFileUpdate.ID AND SS_RequestActions.EmployeeID = SS_EmployeeFileUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001926'
/*30====================== طلب سفر عمل او تدريب    =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_BusinessORTrainingTravel.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_BusinessORTrainingTravel.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب سفر عمل او تدريب' AS RequestArbName, 'Business OR Training Travel' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_BusinessORTrainingTravel ON SS_RequestActions.RequestSerial = SS_BusinessORTrainingTravel.ID AND SS_RequestActions.EmployeeID = SS_BusinessORTrainingTravel.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001927'
/*31====================== الطلبات المتعلقة بالتذاكر السنوية    =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AnnualTicketRelatedRequests.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AnnualTicketRelatedRequests.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'الطلبات المتعلقة بالتذاكر السنوية' AS RequestArbName, 'Annual Ticket Related Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AnnualTicketRelatedRequests ON SS_RequestActions.RequestSerial = SS_AnnualTicketRelatedRequests.ID AND SS_RequestActions.EmployeeID = SS_AnnualTicketRelatedRequests.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001928'
/*32===========================طلب تحديث حساب البنك  ================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ChangeWorkHoursRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ChangeWorkHoursRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تغيير الدوام' AS RequestArbName, 'Change Work Hours Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ChangeWorkHoursRequest ON SS_RequestActions.RequestSerial = SS_ChangeWorkHoursRequest.ID AND SS_RequestActions.EmployeeID = SS_ChangeWorkHoursRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001929'
;
"

        ExecuteUpdate(SQL)


        SQL = "

Create Or ALTER VIEW [dbo].[SS_VNotification]
AS
/*1 ============================الاجازة ==============================================*/  SELECT RequestSerial AS ID, FormCode, ConfigID, SS_VacationRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
 hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_VacationRequest.RequestDate, 103) AS RequestDate, SS_VacationRequest.VacationType, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي اداري' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي طبي' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي تشغيلي'
                  WHEN SS_VacationRequest.VacationType = 'SS_0032' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'طلب إجازة سنوية – إداري داخلي'
                  WHEN SS_VacationRequest.VacationType = 'SS_0033' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'طلب إجازة سنوية – تمريض'
                  WHEN SS_VacationRequest.VacationType = 'SS_0034' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'طلب إجازة سنوية – الإدارة العامة'
                  WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'اجازة اخرى تشغيلي' 
                   WHEN SS_VacationRequest.VacationType = 'SS_0035' THEN 'طلب اجازة اخرى – إداري داخلي' 
                    WHEN SS_VacationRequest.VacationType = 'SS_0036' THEN 'طلب اجازة اخرى – تمريض' 
                     WHEN SS_VacationRequest.VacationType = 'SS_0037' THEN 'طلب اجازة اخرى – الإدارة العامة' 
                  ELSE hrs_VacationsTypes.ArbName4S END AS RequestArbName, 
                  CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Admin' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Medical' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Operational' 
                   WHEN SS_VacationRequest.VacationType = 'SS_0032' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Leave Request – Internal Administration'
                   WHEN SS_VacationRequest.VacationType = 'SS_0033' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Leave Request – Nursing'
                   WHEN SS_VacationRequest.VacationType = 'SS_0034' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Leave Request – General Administration'
                  WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'Other Vacation Operational'
                  WHEN SS_VacationRequest.VacationType = 'SS_0035' THEN 'Other Leave Request – Internal Administration'
                  WHEN SS_VacationRequest.VacationType = 'SS_0036' THEN 'Other Leave Request – Nursing'
                  WHEN SS_VacationRequest.VacationType = 'SS_0037' THEN 'Other Leave Request – General Administration'
                  ELSE hrs_VacationsTypes.EngName END AS RequestEngName, 
                  SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_VacationRequest ON SS_RequestActions.RequestSerial = SS_VacationRequest.ID AND SS_RequestActions.EmployeeID = SS_VacationRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID JOIN
                  hrs_VacationsTypes ON SS_VacationRequest.VacationTypeID = hrs_VacationsTypes.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND (FormCode = 'SS_0011' OR
                  FormCode = 'SS_0012' OR
                  FormCode = 'SS_0013' OR
                  FormCode = 'SS_0030' OR
                  FormCode = 'SS_0031' OR
                  FormCode = 'SS_0032' OR
                  FormCode = 'SS_0033' OR
                  FormCode = 'SS_0034' OR
                   FormCode = 'SS_0035' OR
                  FormCode = 'SS_0036' OR
                  FormCode = 'SS_0037' OR
                  FormCode = 'SS_0018')
/* 2 ============================الاستئذان ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ExecuseRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) AS RequestDate, '' AS ExecuseType, 
                  'طلب استئذان' AS RequestArbName, 'Execuse Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ExecuseRequest ON SS_RequestActions.RequestSerial = SS_ExecuseRequest.ID AND SS_RequestActions.EmployeeID = SS_ExecuseRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_0014'
/*3 ============================انهاء خدمة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EndOfServiceRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب انهاء خدمة' AS RequestArbType, 'End Of Service Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EndOfServiceRequest ON SS_RequestActions.RequestSerial = SS_EndOfServiceRequest.ID AND SS_RequestActions.EmployeeID = SS_EndOfServiceRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND (SS_RequestActions.FormCode = 'SS_0015' OR
                  SS_RequestActions.FormCode = 'SS_0019')
/*4============================خروج وعودة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ExitEntryRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ExitEntryRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خروج وعودة' AS RequestArbName, 'Exit & Re-entry Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ExitEntryRequest ON SS_RequestActions.RequestSerial = SS_ExitEntryRequest.ID AND SS_RequestActions.EmployeeID = SS_ExitEntryRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00191'
/*5============================ تأشيرة الأسرة أو تأشيرة الزيارة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_VisaRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_VisaRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تأشيرة' AS RequestArbName, ' visa Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_VisaRequest ON SS_RequestActions.RequestSerial = SS_VisaRequest.ID AND SS_RequestActions.EmployeeID = SS_VisaRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00192'
/*6============================  خطاب قرض ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_LoanLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_LoanLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب قرض' AS RequestArbName, 'Loan Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_LoanLetterRequest ON SS_RequestActions.RequestSerial = SS_LoanLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_LoanLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00193'
/*7============================   خطاب آخر ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OtherLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OtherLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب آخر' AS RequestArbName, 'Other Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OtherLetterRequest ON SS_RequestActions.RequestSerial = SS_OtherLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_OtherLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00194'
/*8============================    طلب تدريب ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_TrainingRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_TrainingRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تدريب' AS RequestArbName, 'Training Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_TrainingRequest ON SS_RequestActions.RequestSerial = SS_TrainingRequest.ID AND SS_RequestActions.EmployeeID = SS_TrainingRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00195'
/*9============================     استمارة التظلم ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_GrievanceFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_GrievanceFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب استمارة التظلم' AS RequestArbName, 'Grievance Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_GrievanceFormRequest ON SS_RequestActions.RequestSerial = SS_GrievanceFormRequest.ID AND SS_RequestActions.EmployeeID = SS_GrievanceFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00196'
/*10============================     طلب استمارة تقييم المقابلة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_InterviewEvaluationFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_InterviewEvaluationFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة تقييم المقابلة' AS RequestArbName, 'Interview Evaluation Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_InterviewEvaluationFormRequest ON SS_RequestActions.RequestSerial = SS_InterviewEvaluationFormRequest.ID AND SS_RequestActions.EmployeeID = SS_InterviewEvaluationFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00197'
/*11============================     طلب استمارة تصعيد حالات الاعتداء ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AssaultEscalationFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_AssaultEscalationFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة تصعيد حالات الاعتداء' AS RequestArbName, 'Assault Escalation Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AssaultEscalationFormRequest ON SS_RequestActions.RequestSerial = SS_AssaultEscalationFormRequest.ID AND SS_RequestActions.EmployeeID = SS_AssaultEscalationFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00198'
/*12============================   طلب استمارة تضارب المصالح ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ConflictofInterestFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ConflictofInterestFormRequest.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب استمارة تضارب المصالح' AS RequestArbName, 'Conflict of Interest Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ConflictofInterestFormRequest ON SS_RequestActions.RequestSerial = SS_ConflictofInterestFormRequest.ID AND SS_RequestActions.EmployeeID = SS_ConflictofInterestFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00199'
/*13============================   طلب استمارة امتيازات الأطباء ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_PhysiciansPrivilegingFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_PhysiciansPrivilegingFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة امتيازات الأطباء' AS RequestArbName, 'Physicians Privileging Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_PhysiciansPrivilegingFormRequest ON SS_RequestActions.RequestSerial = SS_PhysiciansPrivilegingFormRequest.ID AND SS_RequestActions.EmployeeID = SS_PhysiciansPrivilegingFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001910'
/*14============================   طلب دعم رعاية الأطفال ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_DaycareSupportReaquest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_DaycareSupportReaquest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب دعم رعاية الأطفال' AS RequestArbName, 'Daycare Support' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_DaycareSupportReaquest ON SS_RequestActions.RequestSerial = SS_DaycareSupportReaquest.ID AND SS_RequestActions.EmployeeID = SS_DaycareSupportReaquest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001911'
/*15============================طلب دعم التعليم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EducationSupportRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EducationSupportRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب دعم التعليم' AS RequestArbName, 'Education Support' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EducationSupportRequest ON SS_RequestActions.RequestSerial = SS_EducationSupportRequest.ID AND SS_RequestActions.EmployeeID = SS_EducationSupportRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001912'
/*16===========================طلب  بدل السكن مقدم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AdvanceHousingRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AdvanceHousingRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب  بدل السكن مقدم' AS RequestArbName, 'Advance Housing Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AdvanceHousingRequest ON SS_RequestActions.RequestSerial = SS_AdvanceHousingRequest.ID AND SS_RequestActions.EmployeeID = SS_AdvanceHousingRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001913'
/*17===========================طلب  الراتب مقدم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AdvanceSalaryRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AdvanceSalaryRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب  الراتب مقدم' AS RequestArbName, 'Advance Salary Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AdvanceSalaryRequest ON SS_RequestActions.RequestSerial = SS_AdvanceSalaryRequest.ID AND SS_RequestActions.EmployeeID = SS_AdvanceSalaryRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001914'
/*18===========================طلب خطاب الغرفة التجارية ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ChamberofCommerceLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_ChamberofCommerceLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب خطاب الغرفة التجارية' AS RequestArbName, 'Chamber of Commerce Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ChamberofCommerceLetterRequest ON SS_RequestActions.RequestSerial = SS_ChamberofCommerceLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_ChamberofCommerceLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001915'
/*19===========================طلب خطاب تصنيف هيئة التخصصات الطبية ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_SCFHSLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_SCFHSLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب تصنيف هيئة التخصصات الطبية' AS RequestArbName, 'SCFHS Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_SCFHSLetterRequest ON SS_RequestActions.RequestSerial = SS_SCFHSLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_SCFHSLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001916'
/*20===========================طلب خطاب تعريف الراتب ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_PaySlipRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_PaySlipRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب تعريف الراتب' AS RequestArbName, 'Pay Slip Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_PaySlipRequest ON SS_RequestActions.RequestSerial = SS_PaySlipRequest.ID AND SS_RequestActions.EmployeeID = SS_PaySlipRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001917'
/*21============================خطابات الإبلاغ عن التباين  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OccurrenceVarianceReportingLetters.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_OccurrenceVarianceReportingLetters.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب خطابات الإبلاغ عن التباين' AS RequestArbName, 'Occurrence Variance Reporting Letter' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OccurrenceVarianceReportingLetters ON SS_RequestActions.RequestSerial = SS_OccurrenceVarianceReportingLetters.ID AND SS_RequestActions.EmployeeID = SS_OccurrenceVarianceReportingLetters.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001918'
/*22============================طلب وقت اضافي  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OvertimeRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OvertimeRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب وقت اضافي' AS RequestArbName, ' Over Time Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OvertimeRequest ON SS_RequestActions.RequestSerial = SS_OvertimeRequest.ID AND SS_RequestActions.EmployeeID = SS_OvertimeRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001919'
/*23===========================طلب تعويض رسوم التعليم  ================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EducationFeesCompensationApplication.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_EducationFeesCompensationApplication.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب تعويض رسوم التعليم' AS RequestArbName, 'Education Fees Compensation Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EducationFeesCompensationApplication ON SS_RequestActions.RequestSerial = SS_EducationFeesCompensationApplication.ID AND SS_RequestActions.EmployeeID = SS_EducationFeesCompensationApplication.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001920'
/*24===========================طلب تحديث حساب البنك  ================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_BankAccountUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_BankAccountUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث بيانات البنك' AS RequestArbName, 'Bank Acc Update Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_BankAccountUpdate ON SS_RequestActions.RequestSerial = SS_BankAccountUpdate.ID AND SS_RequestActions.EmployeeID = SS_BankAccountUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001921'
/*25============================طلب تحديث معلومات الاتصال  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ContactInformationUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ContactInformationUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث معلومات الاتصال' AS RequestArbName, 'Contact Information Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ContactInformationUpdate ON SS_RequestActions.RequestSerial = SS_ContactInformationUpdate.ID AND SS_RequestActions.EmployeeID = SS_ContactInformationUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001922'
/*26============================طلب تحديث معلومات المعالين  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_DependentsInformationUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_DependentsInformationUpdate.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تحديث معلومات المعالين' AS RequestArbName, 'Dependents Information Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_DependentsInformationUpdate ON SS_RequestActions.RequestSerial = SS_DependentsInformationUpdate.ID AND SS_RequestActions.EmployeeID = SS_DependentsInformationUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001923'
/*27============================طلب تعديلات التأمين الطبي  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_MedicalInsuranceAdjustments.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_MedicalInsuranceAdjustments.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تعديلات التأمين الطبي' AS RequestArbName, 'Medical Insurance Adjustments' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_MedicalInsuranceAdjustments ON SS_RequestActions.RequestSerial = SS_MedicalInsuranceAdjustments.ID AND SS_RequestActions.EmployeeID = SS_MedicalInsuranceAdjustments.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001924'
/*28======================طلب تحديثات المستندات القانونية الأخرى  =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OtherLegalDocumentUpdates.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OtherLegalDocumentUpdates.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تحديثات المستندات القانونية الأخرى' AS RequestArbName, 'Other Legal Document Updates' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OtherLegalDocumentUpdates ON SS_RequestActions.RequestSerial = SS_OtherLegalDocumentUpdates.ID AND SS_RequestActions.EmployeeID = SS_OtherLegalDocumentUpdates.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001925'
/*29======================طلب تحديثات المستندات القانونية الأخرى  =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EmployeeFileUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EmployeeFileUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث ملف الموظف' AS RequestArbName, 'Employee File Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EmployeeFileUpdate ON SS_RequestActions.RequestSerial = SS_EmployeeFileUpdate.ID AND SS_RequestActions.EmployeeID = SS_EmployeeFileUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001926'
/*30====================== طلب سفر عمل او تدريب    =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_BusinessORTrainingTravel.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_BusinessORTrainingTravel.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب سفر عمل او تدريب' AS RequestArbName, 'Business OR Training Travel' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_BusinessORTrainingTravel ON SS_RequestActions.RequestSerial = SS_BusinessORTrainingTravel.ID AND SS_RequestActions.EmployeeID = SS_BusinessORTrainingTravel.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001927'
/*31====================== الطلبات المتعلقة بالتذاكر السنوية    =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AnnualTicketRelatedRequests.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AnnualTicketRelatedRequests.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'الطلبات المتعلقة بالتذاكر السنوية' AS RequestArbName, 'Annual Ticket Related Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AnnualTicketRelatedRequests ON SS_RequestActions.RequestSerial = SS_AnnualTicketRelatedRequests.ID AND SS_RequestActions.EmployeeID = SS_AnnualTicketRelatedRequests.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001928'
;





"
        ExecuteUpdate(SQL)
        SQL = "

Create Or ALTER VIEW [dbo].[SS_VFollowup]
AS
/*-1==============================الاجازة==================*/ SELECT  
SS_VacationRequest.ID, SS_VacationRequest.VacationType, SS_VacationRequest.Code AS RequestSerial, SS_VacationRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_VacationRequest.RequestDate, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي اداري' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي طبي' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي تشغيلي' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'اجازة اخرى تشغيلي'
                  WHEN SS_VacationRequest.VacationType = 'SS_0035' THEN 'طلب اجازة اخرى – إداري داخلي'
                  WHEN SS_VacationRequest.VacationType = 'SS_0036' THEN 'طلب اجازة اخرى – تمريض'
                  WHEN SS_VacationRequest.VacationType = 'SS_0037' THEN 'طلب اجازة اخرى – الإدارة العامة'
                   WHEN SS_VacationRequest.VacationType = 'SS_0032' THEN 'طلب إجازة سنوية – إداري داخلي'
                    WHEN SS_VacationRequest.VacationType = 'SS_0033' THEN 'طلب إجازة سنوية – تمريض'
                     WHEN SS_VacationRequest.VacationType = 'SS_0034' THEN 'طلب إجازة سنوية – الإدارة العامة'
                  ELSE hrs_VacationsTypes.ArbName4S END AS RequestArbName, 
                  CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Admin' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Medical' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Operational' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'Other Vacation Operational'

                  WHEN SS_VacationRequest.VacationType = 'SS_0032' THEN 'Annual Leave Request – Internal Administration'
                  WHEN SS_VacationRequest.VacationType = 'SS_0033' THEN 'Annual Leave Request – Nursing'
                  WHEN SS_VacationRequest.VacationType = 'SS_0034' THEN 'Annual Leave Request – General Administration'
                  WHEN SS_VacationRequest.VacationType = 'SS_0035' THEN 'Other Leave Request – Internal Administration'
                  WHEN SS_VacationRequest.VacationType = 'SS_0036' THEN 'Other Leave Request – Nursing'
                  WHEN SS_VacationRequest.VacationType = 'SS_0037' THEN 'Other Leave Request – General Administration'
                  ELSE hrs_VacationsTypes.EngName END AS RequestEngName, 
                  CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0011' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0012' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0030' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'SS_0031'
                  WHEN SS_VacationRequest.VacationType = 'SS_0032' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0032'
                  WHEN SS_VacationRequest.VacationType = 'SS_0033' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0033'
                  WHEN SS_VacationRequest.VacationType = 'SS_0034' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0034'
                  WHEN SS_VacationRequest.VacationType = 'SS_0035' THEN 'SS_0035'
                  WHEN SS_VacationRequest.VacationType = 'SS_0036' THEN 'SS_0036'
                  WHEN SS_VacationRequest.VacationType = 'SS_0037' THEN 'SS_0037'
                  WHEN SS_VacationRequest.VacationType = 'SS_0018' THEN 'SS_0018' ELSE 'SS_0013' END AS FormCode, 
                  RequestStautsTypeID
FROM     SS_VacationRequest JOIN
                  hrs_Employees ON SS_VacationRequest.EmployeeID = hrs_Employees.id JOIN
                  hrs_VacationsTypes ON SS_VacationRequest.VacationTypeID = hrs_VacationsTypes.ID INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*-2==============================الاستئذان=======================================================*/ UNION
SELECT SS_ExecuseRequest.ID, '' AS VacationType, SS_ExecuseRequest.Code AS RequestSerial, SS_ExecuseRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ExecuseRequest.RequestDate, 'طلب استئذان' AS RequestArbName, 'Execuse Request' AS RequestEngName, 'SS_0014' AS FormCode, RequestStautsTypeID
FROM     SS_ExecuseRequest JOIN
                  hrs_Employees ON SS_ExecuseRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*3 ============================انهاء خدمة ===============================================================*/ UNION
SELECT SS_EndOfServiceRequest.ID, '' AS VacationType, SS_EndOfServiceRequest.Code AS RequestSerial, SS_EndOfServiceRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EndOfServiceRequest.RequestDate, CASE WHEN FormCode = 'SS_0015' THEN 'طلب انهاء خدمة' ELSE 'طلب انهاء خدمة طبي' END AS RequestArbName, 
                  CASE WHEN FormCode = 'SS_0015' THEN 'End Of Service Request' ELSE 'End Of Service Medical Request' END AS RequestEngName, FormCode AS FormCode, RequestStautsTypeID
FROM     SS_EndOfServiceRequest JOIN
                  hrs_Employees ON SS_EndOfServiceRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*4============================خروج وعودة ==============================================*/ UNION
SELECT SS_ExitEntryRequest.ID, '' AS VacationType, SS_ExitEntryRequest.Code AS RequestSerial, SS_ExitEntryRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ExitEntryRequest.RequestDate, 'طلب خروج وعودة' AS RequestArbName, 'Exit & Re-entry Requests' AS RequestEngName, 'SS_00191' AS FormCode, RequestStautsTypeID
FROM     SS_ExitEntryRequest JOIN
                  hrs_Employees ON SS_ExitEntryRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*5============================ تأشيرة الأسرة أو تأشيرة الزيارة ==============================================*/ UNION
SELECT SS_VisaRequest.ID, '' AS VacationType, SS_VisaRequest.Code AS RequestSerial, SS_VisaRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_VisaRequest.RequestDate, 
                  'طلب تأشيرة' AS RequestArbName, 'visa Requests' AS RequestEngName, 'SS_00192' AS FormCode, RequestStautsTypeID
FROM     SS_VisaRequest JOIN
                  hrs_Employees ON SS_VisaRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*6============================  خطاب قرض ==============================================*/ UNION
SELECT SS_LoanLetterRequest.ID, '' AS VacationType, SS_LoanLetterRequest.Code AS RequestSerial, SS_LoanLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_LoanLetterRequest.RequestDate, 'طلب خطاب قرض' AS RequestArbName, 'Loan Letter Request' AS RequestEngName, 'SS_00193' AS FormCode, RequestStautsTypeID
FROM     SS_LoanLetterRequest JOIN
                  hrs_Employees ON SS_LoanLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*7============================   خطاب آخر ==============================================*/ UNION
SELECT SS_OtherLetterRequest.ID, '' AS VacationType, SS_OtherLetterRequest.Code AS RequestSerial, SS_OtherLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_OtherLetterRequest.RequestDate, 'طلب خطاب آخر' AS RequestArbName, 'Other Letter Request' AS RequestEngName, 'SS_00194' AS FormCode, RequestStautsTypeID
FROM     SS_OtherLetterRequest JOIN
                  hrs_Employees ON SS_OtherLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*8============================    طلب تدريب ==============================================*/ UNION
SELECT SS_TrainingRequest.ID, '' AS VacationType, SS_TrainingRequest.Code AS RequestSerial, SS_TrainingRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_TrainingRequest.RequestDate, 'طلب تدريب' AS RequestArbName, 'Training Request' AS RequestEngName, 'SS_00195' AS FormCode, RequestStautsTypeID
FROM     SS_TrainingRequest JOIN
                  hrs_Employees ON SS_TrainingRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*9============================     استمارة التظلم ==============================================*/ UNION
SELECT SS_GrievanceFormRequest.ID, '' AS VacationType, SS_GrievanceFormRequest.Code AS RequestSerial, SS_GrievanceFormRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_GrievanceFormRequest.RequestDate, 'طلب استمارة التظلم' AS RequestArbName, 'Grievance Form Request' AS RequestEngName, 'SS_00196' AS FormCode, RequestStautsTypeID
FROM     SS_GrievanceFormRequest JOIN
                  hrs_Employees ON SS_GrievanceFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*10============================     طلب استمارة تقييم المقابلة ==============================================*/ UNION
SELECT SS_InterviewEvaluationFormRequest.ID, '' AS VacationType, SS_InterviewEvaluationFormRequest.Code AS RequestSerial, SS_InterviewEvaluationFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_InterviewEvaluationFormRequest.RequestDate, 'طلب استمارة تقييم المقابلة' AS RequestArbName, 'Interview Evaluation Form Request' AS RequestEngName, 
                  'SS_00197' AS FormCode, RequestStautsTypeID
FROM     SS_InterviewEvaluationFormRequest JOIN
                  hrs_Employees ON SS_InterviewEvaluationFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*11============================     طلب استمارة تصعيد حالات الاعتداء ==============================================*/ UNION
SELECT SS_AssaultEscalationFormRequest.ID, '' AS VacationType, SS_AssaultEscalationFormRequest.Code AS RequestSerial, SS_AssaultEscalationFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AssaultEscalationFormRequest.RequestDate, 'طلب استمارة  السكن' AS RequestArbName, 'Accommodation  Form Request' AS RequestEngName, 
                  'SS_00198' AS FormCode, RequestStautsTypeID
FROM     SS_AssaultEscalationFormRequest JOIN
                  hrs_Employees ON SS_AssaultEscalationFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*12============================   طلب استمارة تضارب المصالح ==============================================*/ UNION
SELECT SS_ConflictofInterestFormRequest.ID, '' AS VacationType, SS_ConflictofInterestFormRequest.Code AS RequestSerial, SS_ConflictofInterestFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ConflictofInterestFormRequest.RequestDate, 'طلب استمارة تضارب المصالح' AS RequestArbName, 'Conflict of Interest Form Request' AS RequestEngName, 
                  'SS_00199' AS FormCode, RequestStautsTypeID
FROM     SS_ConflictofInterestFormRequest JOIN
                  hrs_Employees ON SS_ConflictofInterestFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*13============================   طلب استمارة امتيازات الأطباء ==============================================*/ UNION
SELECT SS_PhysiciansPrivilegingFormRequest.ID, '' AS VacationType, SS_PhysiciansPrivilegingFormRequest.Code AS RequestSerial, SS_PhysiciansPrivilegingFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_PhysiciansPrivilegingFormRequest.RequestDate, 'طلب استمارة امتيازات الأطباء' AS RequestArbName, 'Physicians Privileging Form Request' AS RequestEngName, 
                  'SS_001910' AS FormCode, RequestStautsTypeID
FROM     SS_PhysiciansPrivilegingFormRequest JOIN
                  hrs_Employees ON SS_PhysiciansPrivilegingFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*14============================   طلب دعم رعاية الأطفال ==============================================*/ UNION
SELECT SS_DaycareSupportReaquest.ID, '' AS VacationType, SS_DaycareSupportReaquest.Code AS RequestSerial, SS_DaycareSupportReaquest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_DaycareSupportReaquest.RequestDate, 'طلب دعم رعاية الأطفال' AS RequestArbName, 'Daycare Support' AS RequestEngName, 'SS_001911' AS FormCode, RequestStautsTypeID
FROM     SS_DaycareSupportReaquest JOIN
                  hrs_Employees ON SS_DaycareSupportReaquest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*15============================طلب دعم التعليم  ==============================================*/ UNION
SELECT SS_EducationSupportRequest.ID, '' AS VacationType, SS_EducationSupportRequest.Code AS RequestSerial, SS_EducationSupportRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EducationSupportRequest.RequestDate, 'طلب دعم التعليم' AS RequestArbName, 'Education Support' AS RequestEngName, 'SS_001912' AS FormCode, RequestStautsTypeID
FROM     SS_EducationSupportRequest JOIN
                  hrs_Employees ON SS_EducationSupportRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*16===========================طلب  بدل السكن مقدم  ==============================================*/ UNION
SELECT SS_AdvanceHousingRequest.ID, '' AS VacationType, SS_AdvanceHousingRequest.Code AS RequestSerial, SS_AdvanceHousingRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_AdvanceHousingRequest.RequestDate, 'طلب  بدل السكن مقدم' AS RequestArbName, ' Advance Housing Request' AS RequestEngName, 'SS_001913' AS FormCode, RequestStautsTypeID
FROM     SS_AdvanceHousingRequest JOIN
                  hrs_Employees ON SS_AdvanceHousingRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*17===========================طلب  الراتب مقدم  ==============================================*/ UNION
SELECT SS_AdvanceSalaryRequest.ID, '' AS VacationType, SS_AdvanceSalaryRequest.Code AS RequestSerial, SS_AdvanceSalaryRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_AdvanceSalaryRequest.RequestDate, 'طلب الراتب مقدم' AS RequestArbName, 'Advance Salary Request' AS RequestEngName, 'SS_001914' AS FormCode, RequestStautsTypeID
FROM     SS_AdvanceSalaryRequest JOIN
                  hrs_Employees ON SS_AdvanceSalaryRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*18===========================طلب خطاب الغرفة التجارية ==============================================*/ UNION
SELECT SS_ChamberofCommerceLetterRequest.ID, '' AS VacationType, SS_ChamberofCommerceLetterRequest.Code AS RequestSerial, SS_ChamberofCommerceLetterRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ChamberofCommerceLetterRequest.RequestDate, 'طلب خطاب الغرفة التجارية' AS RequestArbName, 'Chamber of Commerce Letter Reques' AS RequestEngName, 
                  'SS_001915' AS FormCode, RequestStautsTypeID
FROM     SS_ChamberofCommerceLetterRequest JOIN
                  hrs_Employees ON SS_ChamberofCommerceLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*19===========================طلب خطاب تصنيف هيئة التخصصات الطبية ==============================================*/ UNION
SELECT SS_SCFHSLetterRequest.ID, '' AS VacationType, SS_SCFHSLetterRequest.Code AS RequestSerial, SS_SCFHSLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_SCFHSLetterRequest.RequestDate, 'طلب خطاب تصنيف هيئة التخصصات الطبية' AS RequestArbName, 'SCFHS Letter Request' AS RequestEngName, 'SS_001916' AS FormCode, RequestStautsTypeID
FROM     SS_SCFHSLetterRequest JOIN
                  hrs_Employees ON SS_SCFHSLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*20===========================طلب خطاب تعريف الراتب ==============================================*/ UNION
SELECT SS_PaySlipRequest.ID, '' AS VacationType, SS_PaySlipRequest.Code AS RequestSerial, SS_PaySlipRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_PaySlipRequest.RequestDate, 'طلب خطاب تعريف الراتب' AS RequestArbName, 'Pay Slip Request' AS RequestEngName, 'SS_001917' AS FormCode, RequestStautsTypeID
FROM     SS_PaySlipRequest JOIN
                  hrs_Employees ON SS_PaySlipRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*21============================خطابات الإبلاغ عن التباين  ==============================================*/ UNION
SELECT SS_OccurrenceVarianceReportingLetters.ID, '' AS VacationType, SS_OccurrenceVarianceReportingLetters.Code AS RequestSerial, SS_OccurrenceVarianceReportingLetters.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_OccurrenceVarianceReportingLetters.RequestDate, '
خطابات الإبلاغ عن التباين' AS RequestArbName, 'Accommodation escalation request' AS RequestEngName, 
                  'SS_001918' AS FormCode, RequestStautsTypeID
FROM     SS_OccurrenceVarianceReportingLetters JOIN
                  hrs_Employees ON SS_OccurrenceVarianceReportingLetters.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*22============================طلب وقت اضافي  ==============================================*/ UNION
SELECT SS_OvertimeRequest.ID, '' AS VacationType, SS_OvertimeRequest.Code AS RequestSerial, SS_OvertimeRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_OvertimeRequest.RequestDate, 'طلب وقت اضافي' AS RequestArbName, 'Over Time' AS RequestEngName, 'SS_001919' AS FormCode, RequestStautsTypeID
FROM     SS_OvertimeRequest JOIN
                  hrs_Employees ON SS_OvertimeRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*23===========================طلب تعويض رسوم التعليم  ================================*/ UNION
SELECT SS_EducationFeesCompensationApplication.ID, '' AS VacationType, SS_EducationFeesCompensationApplication.Code AS RequestSerial, SS_EducationFeesCompensationApplication.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_EducationFeesCompensationApplication.RequestDate, 'طلب تعويض رسوم التعليم' AS RequestArbName, 'Education Fees Compensation Application' AS RequestEngName, 
                  'SS_001920' AS FormCode, RequestStautsTypeID
FROM     SS_EducationFeesCompensationApplication JOIN
                  hrs_Employees ON SS_EducationFeesCompensationApplication.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*24===========================طلب تحديث حساب البنك  ================================*/ UNION
SELECT SS_BankAccountUpdate.ID, '' AS VacationType, SS_BankAccountUpdate.Code AS RequestSerial, SS_BankAccountUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_BankAccountUpdate.RequestDate, 'طلب تحديث حساب البنك' AS RequestArbName, 'Bank Account Update' AS RequestEngName, 'SS_001921' AS FormCode, RequestStautsTypeID
FROM     SS_BankAccountUpdate JOIN
                  hrs_Employees ON SS_BankAccountUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*25===========================طلب تحديث معلومات الاتصال  ================================*/ UNION
SELECT SS_ContactInformationUpdate.ID, '' AS VacationType, SS_ContactInformationUpdate.Code AS RequestSerial, SS_ContactInformationUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ContactInformationUpdate.RequestDate, 'طلب تحديث معلومات الاتصال' AS RequestArbName, 'Contact Information Update' AS RequestEngName, 'SS_001922' AS FormCode, RequestStautsTypeID
FROM     SS_ContactInformationUpdate JOIN
                  hrs_Employees ON SS_ContactInformationUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*26===========================طلب تحديث معلومات المعالين  ================================*/ UNION
SELECT SS_DependentsInformationUpdate.ID, '' AS VacationType, SS_DependentsInformationUpdate.Code AS RequestSerial, SS_DependentsInformationUpdate.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_DependentsInformationUpdate.RequestDate, 'طلب تحديث معلومات المعالين' AS RequestArbName, 'Dependents Information Update' AS RequestEngName, 
                  'SS_001923' AS FormCode, RequestStautsTypeID
FROM     SS_DependentsInformationUpdate JOIN
                  hrs_Employees ON SS_DependentsInformationUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*27===========================طلب تعديلات التأمين الطبي  ================================*/ UNION
SELECT SS_MedicalInsuranceAdjustments.ID, '' AS VacationType, SS_MedicalInsuranceAdjustments.Code AS RequestSerial, SS_MedicalInsuranceAdjustments.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_MedicalInsuranceAdjustments.RequestDate, 'طلب تعديلات التأمين الطبي' AS RequestArbName, 'Medical Insurance Adjustments' AS RequestEngName, 
                  'SS_001924' AS FormCode, RequestStautsTypeID
FROM     SS_MedicalInsuranceAdjustments JOIN
                  hrs_Employees ON SS_MedicalInsuranceAdjustments.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*28===========================طلب تحديثات المستندات القانونية الأخرى  ================================*/ UNION
SELECT SS_OtherLegalDocumentUpdates.ID, '' AS VacationType, SS_OtherLegalDocumentUpdates.Code AS RequestSerial, SS_OtherLegalDocumentUpdates.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_OtherLegalDocumentUpdates.RequestDate, 'طلب تحديثات المستندات القانونية الأخرى' AS RequestArbName, 'Other Legal Document Updates' AS RequestEngName, 
                  'SS_001925' AS FormCode, RequestStautsTypeID
FROM     SS_OtherLegalDocumentUpdates JOIN
                  hrs_Employees ON SS_OtherLegalDocumentUpdates.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*29===========================طلب تحديث ملف الموظف  ================================*/ UNION
SELECT SS_EmployeeFileUpdate.ID, '' AS VacationType, SS_EmployeeFileUpdate.Code AS RequestSerial, SS_EmployeeFileUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EmployeeFileUpdate.RequestDate, 'طلب تحديث ملف الموظف' AS RequestArbName, 'Employee File Update' AS RequestEngName, 'SS_001926' AS FormCode, RequestStautsTypeID
FROM     SS_EmployeeFileUpdate JOIN
                  hrs_Employees ON SS_EmployeeFileUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*30===========================طلب سفر عمل او تدريب  ================================*/ UNION
SELECT SS_BusinessORTrainingTravel.ID, '' AS VacationType, SS_BusinessORTrainingTravel.Code AS RequestSerial, SS_BusinessORTrainingTravel.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_BusinessORTrainingTravel.RequestDate, 'طلب سفر عمل او تدريب' AS RequestArbName, 'Business OR Training Travel' AS RequestEngName, 'SS_001927' AS FormCode, RequestStautsTypeID
FROM     SS_BusinessORTrainingTravel JOIN
                  hrs_Employees ON SS_BusinessORTrainingTravel.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*31===========================الطلبات المتعلقة بالتذاكر السنوية  ================================*/ UNION
SELECT SS_AnnualTicketRelatedRequests.ID, '' AS VacationType, SS_AnnualTicketRelatedRequests.Code AS RequestSerial, SS_AnnualTicketRelatedRequests.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AnnualTicketRelatedRequests.RequestDate, 'الطلبات المتعلقة بالتذاكر السنوية' AS RequestArbName, 'Annual Ticket Related Requests' AS RequestEngName, 
                  'SS_001928' AS FormCode, RequestStautsTypeID
FROM     SS_AnnualTicketRelatedRequests JOIN
                  hrs_Employees ON SS_AnnualTicketRelatedRequests.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*32===========================طلب تغيير الدوام  ================================*/ UNION
SELECT SS_ChangeWorkHoursRequest.ID, '' AS VacationType, SS_ChangeWorkHoursRequest.Code AS RequestSerial, SS_ChangeWorkHoursRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ChangeWorkHoursRequest.RequestDate, 'طلب تغيير الدوام' AS RequestArbName, 'Change Work Hours' AS RequestEngName, 'SS_001929' AS FormCode, 
                  RequestStautsTypeID
FROM     SS_ChangeWorkHoursRequest JOIN
                  hrs_Employees ON SS_ChangeWorkHoursRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
;



"
        ExecuteUpdate(SQL)

        SQL = "alter table hrs_EmployeesPayabilities add 
SRC varchar(50) null,RequestID varchar(50) null"
        ExecuteUpdate(SQL)


        SQL = "alter table SS_AdvanceSalaryRequest add InstallmentDate date null,InstallmentsCount int null "


        ExecuteUpdate(SQL)

        SQL = "


ALTER   VIEW [dbo].[SS_VNotification]
AS
/*1 ============================الاجازة ==============================================*/  SELECT RequestSerial AS ID, FormCode, ConfigID, SS_VacationRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
 hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_VacationRequest.RequestDate, 103) AS RequestDate, SS_VacationRequest.VacationType, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي اداري' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي طبي' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي تشغيلي'
                  WHEN SS_VacationRequest.VacationType = 'SS_0032' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'طلب إجازة سنوية – إداري داخلي'
                  WHEN SS_VacationRequest.VacationType = 'SS_0033' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'طلب إجازة سنوية – تمريض'
                  WHEN SS_VacationRequest.VacationType = 'SS_0034' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'طلب إجازة سنوية – الإدارة العامة'
                  WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'اجازة اخرى تشغيلي' ELSE hrs_VacationsTypes.ArbName4S END AS RequestArbName, 
                  CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Admin' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Medical' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Operational' 
                   WHEN SS_VacationRequest.VacationType = 'SS_0032' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Leave Request – Internal Administration'
                   WHEN SS_VacationRequest.VacationType = 'SS_0033' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Leave Request – Nursing'
                   WHEN SS_VacationRequest.VacationType = 'SS_0034' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Leave Request – General Administration'
                  WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'Other Vacation Operational' ELSE hrs_VacationsTypes.EngName END AS RequestEngName, 
                  SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_VacationRequest ON SS_RequestActions.RequestSerial = SS_VacationRequest.ID AND SS_RequestActions.EmployeeID = SS_VacationRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID JOIN
                  hrs_VacationsTypes ON SS_VacationRequest.VacationTypeID = hrs_VacationsTypes.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND (FormCode = 'SS_0011' OR
                  FormCode = 'SS_0012' OR
                  FormCode = 'SS_0013' OR
                  FormCode = 'SS_0030' OR
                  FormCode = 'SS_0031' OR
                  FormCode = 'SS_0032' OR
                  FormCode = 'SS_0033' OR
                  FormCode = 'SS_0034' OR
                  FormCode = 'SS_0018')
/* 2 ============================الاستئذان ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ExecuseRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ExecuseRequest.RequestDate, 103) AS RequestDate, '' AS ExecuseType, 
                  'طلب استئذان' AS RequestArbName, 'Execuse Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ExecuseRequest ON SS_RequestActions.RequestSerial = SS_ExecuseRequest.ID AND SS_RequestActions.EmployeeID = SS_ExecuseRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_0014'
/*3 ============================انهاء خدمة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EndOfServiceRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EndOfServiceRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب انهاء خدمة' AS RequestArbType, 'End Of Service Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EndOfServiceRequest ON SS_RequestActions.RequestSerial = SS_EndOfServiceRequest.ID AND SS_RequestActions.EmployeeID = SS_EndOfServiceRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND (SS_RequestActions.FormCode = 'SS_0015' OR
                  SS_RequestActions.FormCode = 'SS_0019')
/*4============================خروج وعودة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ExitEntryRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ExitEntryRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خروج وعودة' AS RequestArbName, 'Exit & Re-entry Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ExitEntryRequest ON SS_RequestActions.RequestSerial = SS_ExitEntryRequest.ID AND SS_RequestActions.EmployeeID = SS_ExitEntryRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00191'
/*5============================ تأشيرة الأسرة أو تأشيرة الزيارة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_VisaRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_VisaRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تأشيرة ' AS RequestArbName, ' visa Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_VisaRequest ON SS_RequestActions.RequestSerial = SS_VisaRequest.ID AND SS_RequestActions.EmployeeID = SS_VisaRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00192'
/*6============================  خطاب قرض ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_LoanLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_LoanLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب قرض' AS RequestArbName, 'Loan Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_LoanLetterRequest ON SS_RequestActions.RequestSerial = SS_LoanLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_LoanLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00193'
/*7============================   خطاب آخر ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OtherLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OtherLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب آخر' AS RequestArbName, 'Other Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OtherLetterRequest ON SS_RequestActions.RequestSerial = SS_OtherLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_OtherLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00194'
/*8============================    طلب تدريب ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_TrainingRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_TrainingRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تدريب' AS RequestArbName, 'Training Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_TrainingRequest ON SS_RequestActions.RequestSerial = SS_TrainingRequest.ID AND SS_RequestActions.EmployeeID = SS_TrainingRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00195'
/*9============================     استمارة التظلم ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_GrievanceFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_GrievanceFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب استمارة التظلم' AS RequestArbName, 'Grievance Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_GrievanceFormRequest ON SS_RequestActions.RequestSerial = SS_GrievanceFormRequest.ID AND SS_RequestActions.EmployeeID = SS_GrievanceFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00196'
/*10============================     طلب استمارة تقييم المقابلة ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_InterviewEvaluationFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_InterviewEvaluationFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة تقييم المقابلة' AS RequestArbName, 'Interview Evaluation Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_InterviewEvaluationFormRequest ON SS_RequestActions.RequestSerial = SS_InterviewEvaluationFormRequest.ID AND SS_RequestActions.EmployeeID = SS_InterviewEvaluationFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00197'
/*11============================     طلب استمارة تصعيد حالات الاعتداء ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AssaultEscalationFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_AssaultEscalationFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة تصعيد حالات الاعتداء' AS RequestArbName, 'Accommodation escalation request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AssaultEscalationFormRequest ON SS_RequestActions.RequestSerial = SS_AssaultEscalationFormRequest.ID AND SS_RequestActions.EmployeeID = SS_AssaultEscalationFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00198'
/*12============================   طلب استمارة تضارب المصالح ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ConflictofInterestFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ConflictofInterestFormRequest.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب استمارة تضارب المصالح' AS RequestArbName, 'Conflict of Interest Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ConflictofInterestFormRequest ON SS_RequestActions.RequestSerial = SS_ConflictofInterestFormRequest.ID AND SS_RequestActions.EmployeeID = SS_ConflictofInterestFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_00199'
/*13============================   طلب استمارة امتيازات الأطباء ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_PhysiciansPrivilegingFormRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_PhysiciansPrivilegingFormRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب استمارة امتيازات الأطباء' AS RequestArbName, 'Physicians Privileging Form Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_PhysiciansPrivilegingFormRequest ON SS_RequestActions.RequestSerial = SS_PhysiciansPrivilegingFormRequest.ID AND SS_RequestActions.EmployeeID = SS_PhysiciansPrivilegingFormRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001910'
/*14============================   طلب دعم رعاية الأطفال ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_DaycareSupportReaquest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_DaycareSupportReaquest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب دعم رعاية الأطفال' AS RequestArbName, 'Daycare Support' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_DaycareSupportReaquest ON SS_RequestActions.RequestSerial = SS_DaycareSupportReaquest.ID AND SS_RequestActions.EmployeeID = SS_DaycareSupportReaquest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001911'
/*15============================طلب دعم التعليم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EducationSupportRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EducationSupportRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب دعم التعليم' AS RequestArbName, 'Education Support' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EducationSupportRequest ON SS_RequestActions.RequestSerial = SS_EducationSupportRequest.ID AND SS_RequestActions.EmployeeID = SS_EducationSupportRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001912'
/*16===========================طلب  بدل السكن مقدم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AdvanceHousingRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AdvanceHousingRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب  بدل السكن مقدم' AS RequestArbName, 'Advance Housing Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AdvanceHousingRequest ON SS_RequestActions.RequestSerial = SS_AdvanceHousingRequest.ID AND SS_RequestActions.EmployeeID = SS_AdvanceHousingRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001913'
/*17===========================طلب  الراتب مقدم  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AdvanceSalaryRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AdvanceSalaryRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب  الراتب مقدم' AS RequestArbName, 'Advance Salary Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AdvanceSalaryRequest ON SS_RequestActions.RequestSerial = SS_AdvanceSalaryRequest.ID AND SS_RequestActions.EmployeeID = SS_AdvanceSalaryRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001914'
/*18===========================طلب خطاب الغرفة التجارية ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ChamberofCommerceLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_ChamberofCommerceLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب خطاب الغرفة التجارية' AS RequestArbName, 'Chamber of Commerce Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ChamberofCommerceLetterRequest ON SS_RequestActions.RequestSerial = SS_ChamberofCommerceLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_ChamberofCommerceLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001915'
/*19===========================طلب خطاب تصنيف هيئة التخصصات الطبية ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_SCFHSLetterRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_SCFHSLetterRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطاب تصنيف هيئة التخصصات الطبية' AS RequestArbName, 'SCFHS Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_SCFHSLetterRequest ON SS_RequestActions.RequestSerial = SS_SCFHSLetterRequest.ID AND SS_RequestActions.EmployeeID = SS_SCFHSLetterRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001916'
/*20===========================طلب خطاب تعريف الراتب ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_PaySlipRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_PaySlipRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب خطابتعريف الراتب' AS RequestArbName, 'Pay Slip Letter Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_PaySlipRequest ON SS_RequestActions.RequestSerial = SS_PaySlipRequest.ID AND SS_RequestActions.EmployeeID = SS_PaySlipRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001917'
/*21============================خطابات الإبلاغ عن التباين  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OccurrenceVarianceReportingLetters.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_OccurrenceVarianceReportingLetters.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب خطابات الإبلاغ عن التباين' AS RequestArbName, 'Occurrence Variance Reporting Letter' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OccurrenceVarianceReportingLetters ON SS_RequestActions.RequestSerial = SS_OccurrenceVarianceReportingLetters.ID AND SS_RequestActions.EmployeeID = SS_OccurrenceVarianceReportingLetters.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001918'
/*22============================طلب وقت اضافي  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OvertimeRequest.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OvertimeRequest.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب وقت اضافي' AS RequestArbName, ' Over Time Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OvertimeRequest ON SS_RequestActions.RequestSerial = SS_OvertimeRequest.ID AND SS_RequestActions.EmployeeID = SS_OvertimeRequest.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001919'
/*23===========================طلب تعويض رسوم التعليم  ================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EducationFeesCompensationApplication.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, 
                  SS_EducationFeesCompensationApplication.RequestDate, 103) AS RequestDate, '' AS VacationType, 'طلب تعويض رسوم التعليم' AS RequestArbName, 'Education Fees Compensation Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EducationFeesCompensationApplication ON SS_RequestActions.RequestSerial = SS_EducationFeesCompensationApplication.ID AND SS_RequestActions.EmployeeID = SS_EducationFeesCompensationApplication.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001920'
/*24===========================طلب تحديث حساب البنك  ================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_BankAccountUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_BankAccountUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث بيانات البنك' AS RequestArbName, 'Bank Acc Update Request' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_BankAccountUpdate ON SS_RequestActions.RequestSerial = SS_BankAccountUpdate.ID AND SS_RequestActions.EmployeeID = SS_BankAccountUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001921'
/*25============================طلب تحديث معلومات الاتصال  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_ContactInformationUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_ContactInformationUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث معلومات الاتصال' AS RequestArbName, 'Contact Information Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_ContactInformationUpdate ON SS_RequestActions.RequestSerial = SS_ContactInformationUpdate.ID AND SS_RequestActions.EmployeeID = SS_ContactInformationUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001922'
/*26============================طلب تحديث معلومات المعالين  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_DependentsInformationUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_DependentsInformationUpdate.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تحديث معلومات المعالين' AS RequestArbName, 'Dependents Information Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_DependentsInformationUpdate ON SS_RequestActions.RequestSerial = SS_DependentsInformationUpdate.ID AND SS_RequestActions.EmployeeID = SS_DependentsInformationUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001923'
/*27============================طلب تعديلات التأمين الطبي  ==============================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_MedicalInsuranceAdjustments.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_MedicalInsuranceAdjustments.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تعديلات التأمين الطبي' AS RequestArbName, 'Medical Insurance Adjustments' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_MedicalInsuranceAdjustments ON SS_RequestActions.RequestSerial = SS_MedicalInsuranceAdjustments.ID AND SS_RequestActions.EmployeeID = SS_MedicalInsuranceAdjustments.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001924'
/*28======================طلب تحديثات المستندات القانونية الأخرى  =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_OtherLegalDocumentUpdates.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_OtherLegalDocumentUpdates.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'طلب تحديثات المستندات القانونية الأخرى' AS RequestArbName, 'Other Legal Document Updates' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_OtherLegalDocumentUpdates ON SS_RequestActions.RequestSerial = SS_OtherLegalDocumentUpdates.ID AND SS_RequestActions.EmployeeID = SS_OtherLegalDocumentUpdates.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001925'
/*29======================طلب تحديثات المستندات القانونية الأخرى  =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_EmployeeFileUpdate.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_EmployeeFileUpdate.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب تحديث ملف الموظف' AS RequestArbName, 'Employee File Update' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_EmployeeFileUpdate ON SS_RequestActions.RequestSerial = SS_EmployeeFileUpdate.ID AND SS_RequestActions.EmployeeID = SS_EmployeeFileUpdate.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001926'
/*30====================== طلب سفر عمل او تدريب    =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_BusinessORTrainingTravel.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 1) 
                  AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_BusinessORTrainingTravel.RequestDate, 103) AS RequestDate, '' AS VacationType, 
                  'طلب سفر عمل او تدريب' AS RequestArbName, 'Business OR Training Travel' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_BusinessORTrainingTravel ON SS_RequestActions.RequestSerial = SS_BusinessORTrainingTravel.ID AND SS_RequestActions.EmployeeID = SS_BusinessORTrainingTravel.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001927'
/*31====================== الطلبات المتعلقة بالتذاكر السنوية    =========================================*/ UNION
SELECT RequestSerial AS ID, SS_RequestActions.FormCode, ConfigID, SS_AnnualTicketRelatedRequests.Code AS RequestSerial, hrs_Employees.ID AS EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 
                  1) AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + dbo.fn_GetEmpName(hrs_Employees.Code, 0) AS EmployeeEngName, CONVERT(varchar, SS_AnnualTicketRelatedRequests.RequestDate, 103) AS RequestDate, 
                  '' AS VacationType, 'الطلبات المتعلقة بالتذاكر السنوية' AS RequestArbName, 'Annual Ticket Related Requests' AS RequestEngName, SS_EmployeeID
FROM     SS_RequestActions JOIN
                  SS_AnnualTicketRelatedRequests ON SS_RequestActions.RequestSerial = SS_AnnualTicketRelatedRequests.ID AND SS_RequestActions.EmployeeID = SS_AnnualTicketRelatedRequests.EmployeeID JOIN
                  hrs_Employees ON SS_RequestActions.EmployeeID = hrs_Employees.ID
WHERE  (Seen IS NULL OR
                  Seen = 0) AND SS_RequestActions.FormCode = 'SS_001928'
;


GO



"

        ExecuteUpdate(SQL)



        SQL = "

CREATE OR ALTER VIEW [dbo].[SS_VFollowup]
AS
/*-1==============================الاجازة==================*/ SELECT SS_VacationRequest.ID, SS_VacationRequest.VacationType, SS_VacationRequest.Code AS RequestSerial, SS_VacationRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_VacationRequest.RequestDate, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي اداري' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي طبي' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'اجازة سنوي تشغيلي' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'اجازة اخرى تشغيلي' WHEN SS_VacationRequest.VacationType = 'SS_0035' THEN 'طلب اجازة اخرى – إداري داخلي' WHEN
                   SS_VacationRequest.VacationType = 'SS_0036' THEN 'طلب اجازة اخرى – تمريض' WHEN SS_VacationRequest.VacationType = 'SS_0037' THEN 'طلب اجازة اخرى – الإدارة العامة' WHEN SS_VacationRequest.VacationType = 'SS_0032' THEN 'طلب إجازة سنوية – إداري داخلي'
                   WHEN SS_VacationRequest.VacationType = 'SS_0033' THEN 'طلب إجازة سنوية – تمريض' WHEN SS_VacationRequest.VacationType = 'SS_0034' THEN 'طلب إجازة سنوية – الإدارة العامة' ELSE hrs_VacationsTypes.ArbName4S END AS RequestArbName,
                   CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Admin' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Medical' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'Annual Vacation Operational' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'Other Vacation Operational' WHEN SS_VacationRequest.VacationType = 'SS_0032' THEN 'Annual Leave Request – Internal Administration'
                   WHEN SS_VacationRequest.VacationType = 'SS_0033' THEN 'Annual Leave Request – Nursing' WHEN SS_VacationRequest.VacationType = 'SS_0034' THEN 'Annual Leave Request – General Administration' WHEN SS_VacationRequest.VacationType
                   = 'SS_0035' THEN 'Other Leave Request – Internal Administration' WHEN SS_VacationRequest.VacationType = 'SS_0036' THEN 'Other Leave Request – Nursing' WHEN SS_VacationRequest.VacationType = 'SS_0037' THEN 'Other Leave Request – General Administration'
                   ELSE hrs_VacationsTypes.EngName END AS RequestEngName, CASE WHEN SS_VacationRequest.VacationType = 'SS_0011' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0011' WHEN SS_VacationRequest.VacationType = 'SS_0012' AND SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0012' WHEN SS_VacationRequest.VacationType = 'SS_0030' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0030' WHEN SS_VacationRequest.VacationType = 'SS_0031' THEN 'SS_0031' WHEN SS_VacationRequest.VacationType = 'SS_0032' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0032' WHEN SS_VacationRequest.VacationType = 'SS_0033' AND SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0033' WHEN SS_VacationRequest.VacationType = 'SS_0034' AND 
                  SS_VacationRequest.VacationTypeID = 1 THEN 'SS_0034' WHEN SS_VacationRequest.VacationType = 'SS_0035' THEN 'SS_0035' WHEN SS_VacationRequest.VacationType = 'SS_0036' THEN 'SS_0036' WHEN SS_VacationRequest.VacationType
                   = 'SS_0037' THEN 'SS_0037' WHEN SS_VacationRequest.VacationType = 'SS_0018' THEN 'SS_0018' ELSE 'SS_0013' END AS FormCode, RequestStautsTypeID,SS_VacationRequest.StartDate AS TrxDate
FROM     SS_VacationRequest JOIN
                  hrs_Employees ON SS_VacationRequest.EmployeeID = hrs_Employees.id JOIN
                  hrs_VacationsTypes ON SS_VacationRequest.VacationTypeID = hrs_VacationsTypes.ID INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*-2==============================الاستئذان=======================================================*/ UNION
SELECT SS_ExecuseRequest.ID, '' AS VacationType, SS_ExecuseRequest.Code AS RequestSerial, SS_ExecuseRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ExecuseRequest.RequestDate, 'طلب استئذان' AS RequestArbName, 'Execuse Request' AS RequestEngName, 'SS_0014' AS FormCode, RequestStautsTypeID,SS_ExecuseRequest .ExecuseDate AS TrxDate
FROM     SS_ExecuseRequest JOIN
                  hrs_Employees ON SS_ExecuseRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*3 ============================انهاء خدمة ===============================================================*/ UNION
SELECT SS_EndOfServiceRequest.ID, '' AS VacationType, SS_EndOfServiceRequest.Code AS RequestSerial, SS_EndOfServiceRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EndOfServiceRequest.RequestDate, CASE WHEN FormCode = 'SS_0015' THEN 'طلب انهاء خدمة' ELSE 'طلب انهاء خدمة طبي' END AS RequestArbName, 
                  CASE WHEN FormCode = 'SS_0015' THEN 'End Of Service Request' ELSE 'End Of Service Medical Request' END AS RequestEngName, FormCode AS FormCode, RequestStautsTypeID,SS_EndOfServiceRequest.EOSDate AS TrxDate
FROM     SS_EndOfServiceRequest JOIN
                  hrs_Employees ON SS_EndOfServiceRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*4============================خروج وعودة ==============================================*/ UNION
SELECT SS_ExitEntryRequest.ID, '' AS VacationType, SS_ExitEntryRequest.Code AS RequestSerial, SS_ExitEntryRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ExitEntryRequest.RequestDate, 'طلب خروج وعودة' AS RequestArbName, 'Exit & Re-entry Requests' AS RequestEngName, 'SS_00191' AS FormCode, RequestStautsTypeID,SS_ExitEntryRequest.ExitDate AS TrxDate

FROM     SS_ExitEntryRequest JOIN
                  hrs_Employees ON SS_ExitEntryRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*5============================ تأشيرة الأسرة أو تأشيرة الزيارة ==============================================*/ UNION
SELECT SS_VisaRequest.ID, '' AS VacationType, SS_VisaRequest.Code AS RequestSerial, SS_VisaRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_VisaRequest.RequestDate, 
                  'طلب تأشيرة' AS RequestArbName, 'visa Requests' AS RequestEngName, 'SS_00192' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_VisaRequest JOIN
                  hrs_Employees ON SS_VisaRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*6============================  خطاب قرض ==============================================*/ UNION
SELECT SS_LoanLetterRequest.ID, '' AS VacationType, SS_LoanLetterRequest.Code AS RequestSerial, SS_LoanLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_LoanLetterRequest.RequestDate, 'طلب خطاب قرض' AS RequestArbName, 'Loan Letter Request' AS RequestEngName, 'SS_00193' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_LoanLetterRequest JOIN
                  hrs_Employees ON SS_LoanLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*7============================   خطاب آخر ==============================================*/ UNION
SELECT SS_OtherLetterRequest.ID, '' AS VacationType, SS_OtherLetterRequest.Code AS RequestSerial, SS_OtherLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_OtherLetterRequest.RequestDate, 'طلب خطاب آخر' AS RequestArbName, 'Other Letter Request' AS RequestEngName, 'SS_00194' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_OtherLetterRequest JOIN
                  hrs_Employees ON SS_OtherLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*8============================    طلب تدريب ==============================================*/ UNION
SELECT SS_TrainingRequest.ID, '' AS VacationType, SS_TrainingRequest.Code AS RequestSerial, SS_TrainingRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_TrainingRequest.RequestDate, 'طلب تدريب' AS RequestArbName, 'Training Request' AS RequestEngName, 'SS_00195' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_TrainingRequest JOIN
                  hrs_Employees ON SS_TrainingRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*9============================     استمارة التظلم ==============================================*/ UNION
SELECT SS_GrievanceFormRequest.ID, '' AS VacationType, SS_GrievanceFormRequest.Code AS RequestSerial, SS_GrievanceFormRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_GrievanceFormRequest.RequestDate, 'طلب استمارة التظلم' AS RequestArbName, 'Grievance Form Request' AS RequestEngName, 'SS_00196' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_GrievanceFormRequest JOIN
                  hrs_Employees ON SS_GrievanceFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*10============================     طلب استمارة تقييم المقابلة ==============================================*/ UNION
SELECT SS_InterviewEvaluationFormRequest.ID, '' AS VacationType, SS_InterviewEvaluationFormRequest.Code AS RequestSerial, SS_InterviewEvaluationFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_InterviewEvaluationFormRequest.RequestDate, 'طلب استمارة تقييم المقابلة' AS RequestArbName, 'Interview Evaluation Form Request' AS RequestEngName, 
                  'SS_00197' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_InterviewEvaluationFormRequest JOIN
                  hrs_Employees ON SS_InterviewEvaluationFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*11============================     طلب استمارة تصعيد حالات الاعتداء ==============================================*/ UNION
SELECT SS_AssaultEscalationFormRequest.ID, '' AS VacationType, SS_AssaultEscalationFormRequest.Code AS RequestSerial, SS_AssaultEscalationFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AssaultEscalationFormRequest.RequestDate, 'طلب استمارة تصعيد حالات الاعتداء' AS RequestArbName, 'Assault Escalation Form Request' AS RequestEngName, 
                  'SS_00198' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_AssaultEscalationFormRequest JOIN
                  hrs_Employees ON SS_AssaultEscalationFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*12============================   طلب استمارة تضارب المصالح ==============================================*/ UNION
SELECT SS_ConflictofInterestFormRequest.ID, '' AS VacationType, SS_ConflictofInterestFormRequest.Code AS RequestSerial, SS_ConflictofInterestFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ConflictofInterestFormRequest.RequestDate, 'طلب استمارة تضارب المصالح' AS RequestArbName, 'Conflict of Interest Form Request' AS RequestEngName, 
                  'SS_00199' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_ConflictofInterestFormRequest JOIN
                  hrs_Employees ON SS_ConflictofInterestFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*13============================   طلب استمارة امتيازات الأطباء ==============================================*/ UNION
SELECT SS_PhysiciansPrivilegingFormRequest.ID, '' AS VacationType, SS_PhysiciansPrivilegingFormRequest.Code AS RequestSerial, SS_PhysiciansPrivilegingFormRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_PhysiciansPrivilegingFormRequest.RequestDate, 'طلب استمارة امتيازات الأطباء' AS RequestArbName, 'Physicians Privileging Form Request' AS RequestEngName, 
                  'SS_001910' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_PhysiciansPrivilegingFormRequest JOIN
                  hrs_Employees ON SS_PhysiciansPrivilegingFormRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*14============================   طلب دعم رعاية الأطفال ==============================================*/ UNION
SELECT SS_DaycareSupportReaquest.ID, '' AS VacationType, SS_DaycareSupportReaquest.Code AS RequestSerial, SS_DaycareSupportReaquest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_DaycareSupportReaquest.RequestDate, 'طلب دعم رعاية الأطفال' AS RequestArbName, 'Daycare Support' AS RequestEngName, 'SS_001911' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_DaycareSupportReaquest JOIN
                  hrs_Employees ON SS_DaycareSupportReaquest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*15============================طلب دعم التعليم  ==============================================*/ UNION
SELECT SS_EducationSupportRequest.ID, '' AS VacationType, SS_EducationSupportRequest.Code AS RequestSerial, SS_EducationSupportRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EducationSupportRequest.RequestDate, 'طلب دعم التعليم' AS RequestArbName, 'Education Support' AS RequestEngName, 'SS_001912' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_EducationSupportRequest JOIN
                  hrs_Employees ON SS_EducationSupportRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*16===========================طلب  بدل السكن مقدم  ==============================================*/ UNION
SELECT SS_AdvanceHousingRequest.ID, '' AS VacationType, SS_AdvanceHousingRequest.Code AS RequestSerial, SS_AdvanceHousingRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_AdvanceHousingRequest.RequestDate, 'طلب  بدل السكن مقدم' AS RequestArbName, ' Advance Housing Request' AS RequestEngName, 'SS_001913' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_AdvanceHousingRequest JOIN
                  hrs_Employees ON SS_AdvanceHousingRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*17===========================طلب  الراتب مقدم  ==============================================*/ UNION
SELECT SS_AdvanceSalaryRequest.ID, '' AS VacationType, SS_AdvanceSalaryRequest.Code AS RequestSerial, SS_AdvanceSalaryRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_AdvanceSalaryRequest.RequestDate, 'طلب الراتب مقدم' AS RequestArbName, 'Advance Salary Request' AS RequestEngName, 'SS_001914' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_AdvanceSalaryRequest JOIN
                  hrs_Employees ON SS_AdvanceSalaryRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*18===========================طلب خطاب الغرفة التجارية ==============================================*/ UNION
SELECT SS_ChamberofCommerceLetterRequest.ID, '' AS VacationType, SS_ChamberofCommerceLetterRequest.Code AS RequestSerial, SS_ChamberofCommerceLetterRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ChamberofCommerceLetterRequest.RequestDate, 'طلب خطاب الغرفة التجارية' AS RequestArbName, 'Chamber of Commerce Letter Reques' AS RequestEngName, 
                  'SS_001915' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_ChamberofCommerceLetterRequest JOIN
                  hrs_Employees ON SS_ChamberofCommerceLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*19===========================طلب خطاب تصنيف هيئة التخصصات الطبية ==============================================*/ UNION
SELECT SS_SCFHSLetterRequest.ID, '' AS VacationType, SS_SCFHSLetterRequest.Code AS RequestSerial, SS_SCFHSLetterRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_SCFHSLetterRequest.RequestDate, 'طلب خطاب تصنيف هيئة التخصصات الطبية' AS RequestArbName, 'SCFHS Letter Request' AS RequestEngName, 'SS_001916' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_SCFHSLetterRequest JOIN
                  hrs_Employees ON SS_SCFHSLetterRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*20===========================طلب خطاب تعريف الراتب ==============================================*/ UNION
SELECT SS_PaySlipRequest.ID, '' AS VacationType, SS_PaySlipRequest.Code AS RequestSerial, SS_PaySlipRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_PaySlipRequest.RequestDate, 'طلب خطاب تعريف الراتب' AS RequestArbName, 'Pay Slip Request' AS RequestEngName, 'SS_001917' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_PaySlipRequest JOIN
                  hrs_Employees ON SS_PaySlipRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*21============================خطابات الإبلاغ عن التباين  ==============================================*/ UNION
SELECT SS_OccurrenceVarianceReportingLetters.ID, '' AS VacationType, SS_OccurrenceVarianceReportingLetters.Code AS RequestSerial, SS_OccurrenceVarianceReportingLetters.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_OccurrenceVarianceReportingLetters.RequestDate, '
خطابات الإبلاغ عن التباين' AS RequestArbName, 'Accommodation escalation request' AS RequestEngName, 
                  'SS_001918' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_OccurrenceVarianceReportingLetters JOIN
                  hrs_Employees ON SS_OccurrenceVarianceReportingLetters.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*22============================طلب وقت اضافي  ==============================================*/ UNION
SELECT SS_OvertimeRequest.ID, '' AS VacationType, SS_OvertimeRequest.Code AS RequestSerial, SS_OvertimeRequest.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_OvertimeRequest.RequestDate, 'طلب وقت اضافي' AS RequestArbName, 'Over Time' AS RequestEngName, 'SS_001919' AS FormCode, RequestStautsTypeID, OvertimeDate AS TrxDate
FROM     SS_OvertimeRequest JOIN
                  hrs_Employees ON SS_OvertimeRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*23===========================طلب تعويض رسوم التعليم  ================================*/ UNION
SELECT SS_EducationFeesCompensationApplication.ID, '' AS VacationType, SS_EducationFeesCompensationApplication.Code AS RequestSerial, SS_EducationFeesCompensationApplication.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_EducationFeesCompensationApplication.RequestDate, 'طلب تعويض رسوم التعليم' AS RequestArbName, 'Education Fees Compensation Application' AS RequestEngName, 
                  'SS_001920' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_EducationFeesCompensationApplication JOIN
                  hrs_Employees ON SS_EducationFeesCompensationApplication.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*24===========================طلب تحديث حساب البنك  ================================*/ UNION
SELECT SS_BankAccountUpdate.ID, '' AS VacationType, SS_BankAccountUpdate.Code AS RequestSerial, SS_BankAccountUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_BankAccountUpdate.RequestDate, 'طلب تحديث حساب البنك' AS RequestArbName, 'Bank Account Update' AS RequestEngName, 'SS_001921' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_BankAccountUpdate JOIN
                  hrs_Employees ON SS_BankAccountUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*25===========================طلب تحديث معلومات الاتصال  ================================*/ UNION
SELECT SS_ContactInformationUpdate.ID, '' AS VacationType, SS_ContactInformationUpdate.Code AS RequestSerial, SS_ContactInformationUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_ContactInformationUpdate.RequestDate, 'طلب تحديث معلومات الاتصال' AS RequestArbName, 'Contact Information Update' AS RequestEngName, 'SS_001922' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_ContactInformationUpdate JOIN
                  hrs_Employees ON SS_ContactInformationUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*26===========================طلب تحديث معلومات المعالين  ================================*/ UNION
SELECT SS_DependentsInformationUpdate.ID, '' AS VacationType, SS_DependentsInformationUpdate.Code AS RequestSerial, SS_DependentsInformationUpdate.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_DependentsInformationUpdate.RequestDate, 'طلب تحديث معلومات المعالين' AS RequestArbName, 'Dependents Information Update' AS RequestEngName, 
                  'SS_001923' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_DependentsInformationUpdate JOIN
                  hrs_Employees ON SS_DependentsInformationUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*27===========================طلب تعديلات التأمين الطبي  ================================*/ UNION
SELECT SS_MedicalInsuranceAdjustments.ID, '' AS VacationType, SS_MedicalInsuranceAdjustments.Code AS RequestSerial, SS_MedicalInsuranceAdjustments.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_MedicalInsuranceAdjustments.RequestDate, 'طلب تعديلات التأمين الطبي' AS RequestArbName, 'Medical Insurance Adjustments' AS RequestEngName, 
                  'SS_001924' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_MedicalInsuranceAdjustments JOIN
                  hrs_Employees ON SS_MedicalInsuranceAdjustments.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*28===========================طلب تحديثات المستندات القانونية الأخرى  ================================*/ UNION
SELECT SS_OtherLegalDocumentUpdates.ID, '' AS VacationType, SS_OtherLegalDocumentUpdates.Code AS RequestSerial, SS_OtherLegalDocumentUpdates.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_OtherLegalDocumentUpdates.RequestDate, 'طلب تحديثات المستندات القانونية الأخرى' AS RequestArbName, 'Other Legal Document Updates' AS RequestEngName, 
                  'SS_001925' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_OtherLegalDocumentUpdates JOIN
                  hrs_Employees ON SS_OtherLegalDocumentUpdates.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*29===========================طلب تحديث ملف الموظف  ================================*/ UNION
SELECT SS_EmployeeFileUpdate.ID, '' AS VacationType, SS_EmployeeFileUpdate.Code AS RequestSerial, SS_EmployeeFileUpdate.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) 
                  + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_EmployeeFileUpdate.RequestDate, 'طلب تحديث ملف الموظف' AS RequestArbName, 'Employee File Update' AS RequestEngName, 'SS_001926' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_EmployeeFileUpdate JOIN
                  hrs_Employees ON SS_EmployeeFileUpdate.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*30===========================طلب سفر عمل او تدريب  ================================*/ UNION
SELECT SS_BusinessORTrainingTravel.ID, '' AS VacationType, SS_BusinessORTrainingTravel.Code AS RequestSerial, SS_BusinessORTrainingTravel.EmployeeID, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 
                  1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, 
                  SS_BusinessORTrainingTravel.RequestDate, 'طلب سفر عمل او تدريب' AS RequestArbName, 'Business OR Training Travel' AS RequestEngName, 'SS_001927' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_BusinessORTrainingTravel JOIN
                  hrs_Employees ON SS_BusinessORTrainingTravel.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*31===========================الطلبات المتعلقة بالتذاكر السنوية  ================================*/ UNION
SELECT SS_AnnualTicketRelatedRequests.ID, '' AS VacationType, SS_AnnualTicketRelatedRequests.Code AS RequestSerial, SS_AnnualTicketRelatedRequests.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_AnnualTicketRelatedRequests.RequestDate, 'الطلبات المتعلقة بالتذاكر السنوية' AS RequestArbName, 'Annual Ticket Related Requests' AS RequestEngName, 
                  'SS_001928' AS FormCode, RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_AnnualTicketRelatedRequests JOIN
                  hrs_Employees ON SS_AnnualTicketRelatedRequests.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID
/*32===========================طلب تغيير الدوام  ================================*/ UNION
SELECT SS_ChangeWorkHoursRequest.ID, '' AS VacationType, SS_ChangeWorkHoursRequest.Code AS RequestSerial, SS_ChangeWorkHoursRequest.EmployeeID, 
                  hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 1) + ' ' + '-' + ' ' + sys_branches.arbname AS EmployeeArbName, hrs_Employees.code + '  ' + '-' + '  ' + [dbo].[fn_GetEmpName](hrs_Employees.Code, 0) 
                  + ' ' + '-' + ' ' + sys_branches.engname AS EmployeeEngName, SS_ChangeWorkHoursRequest.RequestDate, 'طلب تغيير الدوام' AS RequestArbName, 'Change Work Hours' AS RequestEngName, 'SS_001929' AS FormCode, 
                  RequestStautsTypeID, RequestDate AS TrxDate
FROM     SS_ChangeWorkHoursRequest JOIN
                  hrs_Employees ON SS_ChangeWorkHoursRequest.EmployeeID = hrs_Employees.id INNER JOIN
                  sys_Branches ON hrs_Employees.BranchID = sys_Branches.ID; 
"
        ExecuteUpdate(SQL)


    End Function
	Public Function ExecuteUpdate(ByVal mySQLQuery As String) As Boolean

        Try
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnectionString, CommandType.Text, mySQLQuery)
        Catch ex As Exception

        End Try
    End Function

#End Region
End Class
