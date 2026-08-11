-- Add ReviewPreviousPeriodSalaries to sys_SystemConfig (default = 0 / false)
IF COL_LENGTH('sys_SystemConfig', 'ReviewPreviousPeriodSalaries') IS NULL
BEGIN
    ALTER TABLE dbo.sys_SystemConfig ADD ReviewPreviousPeriodSalaries bit NOT NULL
        CONSTRAINT DF_sys_SystemConfig_ReviewPreviousPeriodSalaries DEFAULT(0)
END
GO
