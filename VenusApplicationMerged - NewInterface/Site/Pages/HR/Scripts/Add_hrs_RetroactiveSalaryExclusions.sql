-- Exclusion table for permanently excluded prior-period unprepared salaries
IF OBJECT_ID(N'dbo.hrs_RetroactiveSalaryExclusions', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.hrs_RetroactiveSalaryExclusions
    (
        ID int IDENTITY(1,1) NOT NULL,
        EmployeeID int NOT NULL,
        AccrualPeriodID int NOT NULL,
        PaymentPeriodID int NULL,
        ExclusionReason nvarchar(500) NOT NULL,
        RegUserID int NULL,
        RegDate datetime NOT NULL CONSTRAINT DF_hrs_RetroactiveSalaryExclusions_RegDate DEFAULT(GETDATE()),
        CONSTRAINT PK_hrs_RetroactiveSalaryExclusions PRIMARY KEY CLUSTERED (ID),
        CONSTRAINT UQ_hrs_RetroactiveSalaryExclusions UNIQUE (EmployeeID, AccrualPeriodID)
    )
END
GO
