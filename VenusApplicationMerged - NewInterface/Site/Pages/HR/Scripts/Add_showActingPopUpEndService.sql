-- Add showActingPopUpEndService to sys_SystemConfig (default = 0 / false)
IF COL_LENGTH('sys_SystemConfig', 'showActingPopUpEndService') IS NULL
BEGIN
    ALTER TABLE dbo.sys_SystemConfig ADD showActingPopUpEndService bit NOT NULL
        CONSTRAINT DF_sys_SystemConfig_showActingPopUpEndService DEFAULT(0)
END
GO
