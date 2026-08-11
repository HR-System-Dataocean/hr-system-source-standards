-- Register frmEndActingAssignment (End Acting Assignment & Restore Responsibility)
IF NOT EXISTS (SELECT 1 FROM sys_Forms WHERE Code='frmEndActingAssignment')
    INSERT INTO sys_Forms(Code,EngName,ArbName,ArbName4S,EngDescription,ArbDescription,Rank,ModuleID,Height,Width,RegDate)
    VALUES('frmEndActingAssignment','frmEndActingAssignment.aspx',N'إنهاء التكليف',N'إنهاء التكليف',
           'End Acting Assignment',N'إنهاء التكليف وإعادة إسناد المسؤوليات',0,2,700,1100,GETDATE());

UPDATE sys_Forms
SET ArbName=N'إنهاء التكليف', ArbName4S=N'إنهاء التكليف',
    EngDescription='End Acting Assignment', ArbDescription=N'إنهاء التكليف وإعادة إسناد المسؤوليات'
WHERE Code='frmEndActingAssignment';

DECLARE @FormIDEndActing int=(SELECT ID FROM sys_Forms WHERE Code='frmEndActingAssignment');
DECLARE @ObjectIDEndActing int=(SELECT ID FROM sys_Objects WHERE Code='hrs_ActingEmployeeAssignments');

IF NOT EXISTS (SELECT 1 FROM sys_Menus WHERE Code='frmEndActingAssignment')
    INSERT INTO sys_Menus(Code,EngName,ArbName,ArbName4S,ParentID,Rank,FormID,ObjectID,IsHide,ViewType,RegDate)
    SELECT 'frmEndActingAssignment','End Acting Assignment',N'إنهاء التكليف',N'إنهاء التكليف',
           COALESCE((SELECT TOP 1 ParentID FROM sys_Menus WHERE Code='frmActingEmployeeAssignment'),
                    (SELECT TOP 1 ParentID FROM sys_Menus WHERE Code='frmActingPositionAssignment'),
                    (SELECT TOP 1 ParentID FROM sys_Menus WHERE Code='frmDelegationSChedule'),240),
           COALESCE((SELECT MAX(Rank)+1 FROM sys_Menus WHERE ParentID=COALESCE(
                (SELECT TOP 1 ParentID FROM sys_Menus WHERE Code='frmActingEmployeeAssignment'),
                (SELECT TOP 1 ParentID FROM sys_Menus WHERE Code='frmActingPositionAssignment'),
                (SELECT TOP 1 ParentID FROM sys_Menus WHERE Code='frmDelegationSChedule'),240)),1),
           @FormIDEndActing,@ObjectIDEndActing,0,1,GETDATE();

UPDATE sys_Menus
SET EngName='End Acting Assignment', ArbName=N'إنهاء التكليف', ArbName4S=N'إنهاء التكليف'
WHERE Code='frmEndActingAssignment';

IF NOT EXISTS (SELECT 1 FROM sys_FormsPermissions WHERE UserID=1 AND FormID=@FormIDEndActing)
    INSERT INTO sys_FormsPermissions(FormID,UserID,AllowView,AllowAdd,AllowEdit,AllowDelete,AllowPrint,RegUserID,RegDate)
    VALUES(@FormIDEndActing,1,1,1,1,1,1,1,GETDATE());
GO
