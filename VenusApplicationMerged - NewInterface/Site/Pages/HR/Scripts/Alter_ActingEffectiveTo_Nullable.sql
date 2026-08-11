-- Allow NULL EffectiveTo on acting assignment tables (open-ended assignments)
IF COL_LENGTH('hrs_ActingEmployeeAssignments', 'EffectiveTo') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_hrs_ActingEmployeeAssignments_Dates')
        ALTER TABLE dbo.hrs_ActingEmployeeAssignments DROP CONSTRAINT CK_hrs_ActingEmployeeAssignments_Dates;
    ALTER TABLE dbo.hrs_ActingEmployeeAssignments ALTER COLUMN EffectiveTo datetime NULL;
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_hrs_ActingEmployeeAssignments_Dates')
        ALTER TABLE dbo.hrs_ActingEmployeeAssignments WITH NOCHECK
        ADD CONSTRAINT CK_hrs_ActingEmployeeAssignments_Dates CHECK (EffectiveTo IS NULL OR EffectiveTo >= EffectiveFrom);
END
GO

IF COL_LENGTH('hrs_ActingPositionAssignments', 'EffectiveTo') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_hrs_ActingPositionAssignments_Dates')
        ALTER TABLE dbo.hrs_ActingPositionAssignments DROP CONSTRAINT CK_hrs_ActingPositionAssignments_Dates;
    ALTER TABLE dbo.hrs_ActingPositionAssignments ALTER COLUMN EffectiveTo datetime NULL;
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name='CK_hrs_ActingPositionAssignments_Dates')
        ALTER TABLE dbo.hrs_ActingPositionAssignments WITH NOCHECK
        ADD CONSTRAINT CK_hrs_ActingPositionAssignments_Dates CHECK (EffectiveTo IS NULL OR EffectiveTo >= EffectiveFrom);
END
GO
