-- Mapping table: payroll transaction type -> retro/difference transaction type
IF OBJECT_ID(N'dbo.hrs_RetroTransactionMapping', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.hrs_RetroTransactionMapping
    (
        ID int IDENTITY(1,1) NOT NULL,
        TransactionTypeID int NOT NULL,
        RetroTransactionTypeID int NULL,
        IsActive bit NOT NULL CONSTRAINT DF_hrs_RetroTransactionMapping_IsActive DEFAULT(0),
        RegUserID int NULL,
        RegDate datetime NOT NULL CONSTRAINT DF_hrs_RetroTransactionMapping_RegDate DEFAULT(GETDATE()),
        CONSTRAINT PK_hrs_RetroTransactionMapping PRIMARY KEY CLUSTERED (ID),
        CONSTRAINT UQ_hrs_RetroTransactionMapping_Txn UNIQUE (TransactionTypeID)
    )
END
GO

-- Register form
IF NOT EXISTS (SELECT 1 FROM sys_Forms WHERE Code = 'frmRetroTransactionMapping')
BEGIN
    INSERT INTO sys_Forms(Code, EngName, ArbName, ArbName4S, EngDescription, ArbDescription, Rank, ModuleID, Height, Width, RegDate)
    VALUES (
        'frmRetroTransactionMapping',
        'frmRetroTransactionMapping.aspx',
        N'ربط بنود الفروقات بأثر رجعي',
        N'ربط بنود الفروقات بأثر رجعي',
        'Linking differences items retrospectively',
        N'ربط بنود الفروقات بأثر رجعي',
        0, 2, 750, 1200, GETDATE()
    )
END
GO

DECLARE @FormID int = (SELECT ID FROM sys_Forms WHERE Code = 'frmRetroTransactionMapping')

-- Menu under Payroll Settings (0024) when available
IF @FormID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM sys_Menus WHERE Code = 'frmRetroTransactionMapping')
BEGIN
    DECLARE @ParentID int = (SELECT TOP 1 ID FROM sys_Menus WHERE CODE = '0024')
    IF @ParentID IS NULL
        SET @ParentID = (SELECT TOP 1 ParentID FROM sys_Menus WHERE Code = 'frmSalaryProductionFilesSetting')
    IF @ParentID IS NULL
        SET @ParentID = 240

    DECLARE @Rank int = ISNULL((SELECT MAX(Rank) + 1 FROM sys_Menus WHERE ParentID = @ParentID), 1)

    INSERT INTO sys_Menus(Code, EngName, ArbName, ArbName4S, ParentID, Rank, FormID, IsHide, ViewType, RegDate)
    VALUES (
        'frmRetroTransactionMapping',
        'Linking differences items retrospectively',
        N'ربط بنود الفروقات بأثر رجعي',
        N'ربط بنود الفروقات بأثر رجعي',
        @ParentID, @Rank, @FormID, 0, 1, GETDATE()
    )
END
GO

DECLARE @FormID2 int = (SELECT ID FROM sys_Forms WHERE Code = 'frmRetroTransactionMapping')
IF @FormID2 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM sys_FormsPermissions WHERE UserID = 1 AND FormID = @FormID2)
BEGIN
    INSERT INTO sys_FormsPermissions(FormID, UserID, AllowView, AllowAdd, AllowEdit, AllowDelete, AllowPrint, RegUserID, RegDate)
    VALUES (@FormID2, 1, 1, 1, 1, 1, 1, 1, GETDATE())
END
GO
