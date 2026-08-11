-- Add SourceForm / SourceID to hrs_EmployeesItems for EOS popup tracking
IF COL_LENGTH('hrs_EmployeesItems', 'SourceForm') IS NULL
    ALTER TABLE dbo.hrs_EmployeesItems ADD SourceForm nvarchar(100) NULL;
GO

IF COL_LENGTH('hrs_EmployeesItems', 'SourceID') IS NULL
    ALTER TABLE dbo.hrs_EmployeesItems ADD SourceID int NULL;
GO
