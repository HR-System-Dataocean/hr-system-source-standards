Imports System.Web
Imports System.Web.Services
Imports System.Data
Imports Resources
Imports System.Web.Services.Protocols
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class PoliciesWs
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
    Public Function RetAnnounces() As DataTable
        Dim Strcommand1 As String = ""
        If ProfileCls.CurrentLanguage = "Ar" Then
            Strcommand1 = "select ArbName  from Sys_Announces where IsActive = 1 and canceldate is null"
        Else
            Strcommand1 = "select EngName  from Sys_Announces where IsActive = 1 and canceldate is null"
        End If
        Dim Cls As New Clssys_Announces(New System.Web.UI.Page)
        Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Cls.ConnectionString, System.Data.CommandType.Text, Strcommand1).Tables(0)
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function AuthorizedModules() As DataTable
        Dim UserID As Int32 = ProfileCls.RetUserID()
        Dim ModulesList As New ArrayList()
        ModulesList.Add(0)
        'Get Authorized To User
        Dim _Sys_ModulesPermission As New ClsSys_ModulesPermissions(New System.Web.UI.Page)
        _Sys_ModulesPermission.Find("UserID = " & UserID)
        Dim DT As DataTable = _Sys_ModulesPermission.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            _Sys_ModulesPermission = New ClsSys_ModulesPermissions(New System.Web.UI.Page)
            _Sys_ModulesPermission.Find("ID = " & DT.Rows(i)("ID"))
            If _Sys_ModulesPermission.CanView = True Then
                ModulesList.Add(_Sys_ModulesPermission.ModuleID)
            End If
        Next i
        'Get Authorized To UserGroups
        Dim _Sys_GroupsUser As New Clssys_GroupsUsers(New System.Web.UI.Page)
        _Sys_GroupsUser.Find("UserID = " & UserID)
        Dim DT01 As DataTable = _Sys_GroupsUser.DataSet.Tables(0)
        For i As Integer = 0 To DT01.Rows.Count - 1
            _Sys_GroupsUser.Find("ID = " & DT01.Rows(i)("ID"))
            _Sys_ModulesPermission = New ClsSys_ModulesPermissions(New System.Web.UI.Page)
            _Sys_ModulesPermission.Find("GroupID = " & _Sys_GroupsUser.GroupID)
            Dim DT02 As DataTable = _Sys_ModulesPermission.DataSet.Tables(0)
            For x As Integer = 0 To DT02.Rows.Count - 1
                _Sys_ModulesPermission = New ClsSys_ModulesPermissions(New System.Web.UI.Page)
                _Sys_ModulesPermission.Find("ID = " & DT02.Rows(x)("ID"))
                If _Sys_ModulesPermission.CanView = True Then
                    ModulesList.Add(_Sys_ModulesPermission.ModuleID)
                End If
            Next x
        Next i
        'Get Modules Lists
        Dim _Sys_Module As New Clssys_Modules(New System.Web.UI.Page)
        Dim whrstring As String = ""
        For i As Integer = 0 To ModulesList.Count - 1
            whrstring += ModulesList(i).ToString()
            If i < ModulesList.Count - 1 Then
                whrstring += ","
            End If
        Next i
        _Sys_Module.Find("IsRegistered <> 0 and ID in (" & whrstring & ")  order by Rank")
        Return _Sys_Module.DataSet.Tables(0)
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function AuthorizedPages() As DataTable
        Dim UserID As Int32 = ProfileCls.RetUserID()
        Dim PagesList As New ArrayList()
        PagesList.Add(0)
        'Get Authorized To User
        Dim _Sys_FormsPermission As New ClsSys_FormsPermissions(New System.Web.UI.Page)
        _Sys_FormsPermission.Find1("UserID = " & UserID)
        Dim DT As DataTable = _Sys_FormsPermission.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            If Convert.ToBoolean(DT.Rows(i)("AllowView")) = True Then
                PagesList.Add(DT.Rows(i)("FormID"))
            End If
        Next i
        'Get Authorized To UserGroups
        Dim _Sys_GroupsUser As New Clssys_GroupsUsers(New System.Web.UI.Page)
        _Sys_GroupsUser.Find("UserID = " & UserID)
        Dim DT01 As DataTable = _Sys_GroupsUser.DataSet.Tables(0)
        For i As Integer = 0 To DT01.Rows.Count - 1
            _Sys_FormsPermission = New ClsSys_FormsPermissions(New System.Web.UI.Page)
            _Sys_FormsPermission.Find1("GroupID = " & DT01.Rows(i)("GroupID"))
            Dim DT02 As DataTable = _Sys_FormsPermission.DataSet.Tables(0)
            For x As Integer = 0 To DT02.Rows.Count - 1
                If Convert.ToBoolean(DT02.Rows(x)("AllowView")) = True Then
                    PagesList.Add(DT02.Rows(x)("FormID"))
                End If
            Next x
        Next i
        'Get Modules Lists
        Dim _Sys_Form As New ClsSys_Forms(New System.Web.UI.Page)
        Dim whrstring As String = ""
        For i As Integer = 0 To PagesList.Count - 1
            whrstring += PagesList(i).ToString()
            If i < PagesList.Count - 1 Then
                whrstring += ","
            End If
        Next i
        _Sys_Form.Find("ID in (" & whrstring & ") order by Rank")
        Return _Sys_Form.DataSet.Tables(0)
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function AuthorizedPagesToolBox(ByVal PageID As Int32) As Object(,)
        Dim UserID As Int32 = ProfileCls.RetUserID()
        Dim myobject(1, 4) As Object
        Dim IsAllowAdd As Integer = 0
        Dim IsAllowupdate As Integer = 0
        Dim IsAllowDelete As Integer = 0
        Dim IsAllowPrint As Integer = 0
        'Get Authorized To User
        Dim _Sys_FormsPermission As New ClsSys_FormsPermissions(New System.Web.UI.Page)
        _Sys_FormsPermission.Find1("UserID = " & UserID & " and FormID = " & PageID)
        Dim DT As DataTable = _Sys_FormsPermission.DataSet.Tables(0)
        For i As Integer = 0 To DT.Rows.Count - 1
            IsAllowAdd = IIf(IsAllowAdd = 0, Convert.ToInt32(DT.Rows(i)("AllowAdd")), IsAllowAdd)
            IsAllowupdate = IIf(IsAllowupdate = 0, Convert.ToInt32(DT.Rows(i)("AllowEdit")), IsAllowupdate)
            IsAllowDelete = IIf(IsAllowDelete = 0, Convert.ToInt32(DT.Rows(i)("AllowDelete")), IsAllowDelete)
            IsAllowPrint = IIf(IsAllowPrint = 0, Convert.ToInt32(DT.Rows(i)("AllowPrint")), IsAllowPrint)
        Next i
        'Get Authorized To UserGroups
        Dim _Sys_GroupsUser As New Clssys_GroupsUsers(New System.Web.UI.Page)
        _Sys_GroupsUser.Find("UserID = " & UserID)
        Dim DT01 As DataTable = _Sys_GroupsUser.DataSet.Tables(0)
        For i As Integer = 0 To DT01.Rows.Count - 1
            _Sys_GroupsUser.Find("ID = " & DT01.Rows(i)("ID"))
            _Sys_FormsPermission = New ClsSys_FormsPermissions(New System.Web.UI.Page)
            _Sys_FormsPermission.Find1("GroupID = " & _Sys_GroupsUser.GroupID & " and FormID = " & PageID)
            Dim DT02 As DataTable = _Sys_FormsPermission.DataSet.Tables(0)
            For x As Integer = 0 To DT02.Rows.Count - 1
                IsAllowAdd = IIf(IsAllowAdd = 0, Convert.ToInt32(DT02.Rows(x)("AllowAdd")), IsAllowAdd)
                IsAllowupdate = IIf(IsAllowupdate = 0, Convert.ToInt32(DT02.Rows(x)("AllowEdit")), IsAllowupdate)
                IsAllowDelete = IIf(IsAllowDelete = 0, Convert.ToInt32(DT02.Rows(x)("AllowDelete")), IsAllowDelete)
                IsAllowPrint = IIf(IsAllowPrint = 0, Convert.ToInt32(DT02.Rows(x)("AllowPrint")), IsAllowPrint)
            Next x
        Next i
        myobject(0, 0) = IsAllowAdd
        myobject(0, 1) = IsAllowupdate
        myobject(0, 2) = IsAllowDelete
        myobject(0, 3) = IsAllowPrint
        Return myobject
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function AuthorizedPagesOpen(ByVal Pagename As String) As Boolean
        Dim UserID As Int32 = ProfileCls.RetUserID()
        'Get Authorized To User
        Dim _Sys_Form As New ClsSys_Forms(New System.Web.UI.Page)
        If _Sys_Form.Find("EngName = '" & Pagename & "'") Then
            Dim dtMdls As DataTable = AuthorizedModules()
            Dim intisinmdl As Integer = 0
            For i As Integer = 0 To dtMdls.Rows.Count - 1
                If dtMdls.Rows(i)("ID") = _Sys_Form.ModuleID Then
                    intisinmdl = 1
                    Exit For
                End If
            Next i
            If intisinmdl = 0 Then
                Return False
            End If

            Dim _Sys_FormsPermission As New ClsSys_FormsPermissions(New System.Web.UI.Page)
            _Sys_FormsPermission.Find1("UserID = " & UserID & " and FormID = " & _Sys_Form.ID)
            Dim DT As DataTable = _Sys_FormsPermission.DataSet.Tables(0)
            For i As Integer = 0 To DT.Rows.Count - 1
                _Sys_FormsPermission = New ClsSys_FormsPermissions(New System.Web.UI.Page)
                _Sys_FormsPermission.Find1("ID = " & DT.Rows(i)("ID"))
                Return _Sys_FormsPermission.AllowView
            Next i
            'Get Authorized To UserGroups
            Dim _Sys_GroupsUser As New Clssys_GroupsUsers(New System.Web.UI.Page)
            _Sys_GroupsUser.Find("UserID = " & UserID)
            Dim DT01 As DataTable = _Sys_GroupsUser.DataSet.Tables(0)
            For i As Integer = 0 To DT01.Rows.Count - 1
                _Sys_GroupsUser.Find("ID = " & DT01.Rows(i)("ID"))
                _Sys_FormsPermission = New ClsSys_FormsPermissions(New System.Web.UI.Page)
                _Sys_FormsPermission.Find1("GroupID = " & _Sys_GroupsUser.GroupID & " and FormID = " & _Sys_Form.ID)
                Dim DT02 As DataTable = _Sys_FormsPermission.DataSet.Tables(0)
                For x As Integer = 0 To DT02.Rows.Count - 1
                    _Sys_FormsPermission = New ClsSys_FormsPermissions(New System.Web.UI.Page)
                    _Sys_FormsPermission.Find1("ID = " & DT02.Rows(x)("ID"))
                    Return _Sys_FormsPermission.AllowAdd
                Next x
            Next i
        Else
            Return True
        End If
        Return False
    End Function
End Class