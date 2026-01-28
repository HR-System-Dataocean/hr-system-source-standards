Imports Microsoft.VisualBasic
Imports System.Data
Imports Resources
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Public Class ProfileCls
    Public Shared Function RetUserID() As Int64
        If HttpContext.Current.User.Identity.Name IsNot Nothing And HttpContext.Current.User.Identity.Name <> "" Then
            Dim CheckInt64 As Int64
            If Int64.TryParse(HttpContext.Current.User.Identity.Name, CheckInt64) Then
                Return Convert.ToInt32(HttpContext.Current.User.Identity.Name)
            End If
        End If
        Return 43
    End Function
    Public Shared Function RetCompany() As Clssys_Companies
        If HttpContext.Current.Session(MetaData.CurrentCompanyInfo) Is Nothing Then
            Dim Sys_Companie As New Clssys_Companies(New Page)
            Sys_Companie.Find("ID = " & Convert.ToInt32(HttpContext.Current.Session("CompanyID")))
            HttpContext.Current.Session(MetaData.CurrentCompanyInfo) = Sys_Companie.DataSet.Tables(0)
        End If
        If HttpContext.Current.Session(MetaData.CurrentCompanyInfo) IsNot Nothing Then
            Dim DT As Data.DataTable = DirectCast(HttpContext.Current.Session(MetaData.CurrentCompanyInfo), System.Data.DataTable)
            Dim _Sys_Companie As New Clssys_Companies(New Page)
            If DT.Rows.Count > 0 Then
                _Sys_Companie.Find("ID = " & DT.Rows(0)("ID"))
                Return _Sys_Companie
            End If
        End If
        Return Nothing
    End Function
    Public Shared Function RetRefPeople() As Integer
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("ID = " & RetUserID()) Then
            Return Convert.ToInt32(_Sys_User.RelEmployee)
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function RetUserByRefPeople(ByVal EmployeeID As Integer) As Integer
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("RelEmployee = " & EmployeeID) Then
            Return Convert.ToInt32(_Sys_User.ID)
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function CheckUserByCode(ByVal UserCode As String) As Integer
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("Code = '" & UserCode & "'") Then
            Return Convert.ToInt32(_Sys_User.ID)
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function CheckEmployeeByCode(ByVal EmployeeCode As String) As Integer
        Dim _hrs_Employees As New Clshrs_Employees(New Page)
        If _hrs_Employees.FindNOCompany("canceldate is null and ExcludeDate is null and Code = '" & EmployeeCode & "'") Then
            Return Convert.ToInt32(_hrs_Employees.ID)
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function RetEmployeeEng(ByVal EmployeeID As String) As String
        Dim _hrs_Employees As New Clshrs_Employees(New Page)
        If _hrs_Employees.FindNOCompany("canceldate is null and ExcludeDate is null and ID = '" & EmployeeID & "'") Then
            Return _hrs_Employees.EnglishName
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function RetEmployeeArb(ByVal EmployeeID As String) As String
        Dim _hrs_Employees As New Clshrs_Employees(New Page)
        If _hrs_Employees.FindNOCompany("canceldate is null and ExcludeDate is null and ID = '" & EmployeeID & "'") Then
            Return _hrs_Employees.ArabicName
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function RetEmployeeComp(ByVal EmployeeID As String) As Integer
        Dim _hrs_Employees As New Clshrs_Employees(New Page)
        If _hrs_Employees.FindNOCompany("canceldate is null and ExcludeDate is null and ID = '" & EmployeeID & "'") Then
            Return Convert.ToInt32(_hrs_Employees.CompanyID)
        Else
            Return Nothing
        End If
    End Function
    Public Shared ReadOnly Property CurrentLanguage() As String
        Get
            Return Convert.ToString(HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey))
        End Get
    End Property
    Public Shared ReadOnly Property CurrentTheme() As String
        Get
            Return Convert.ToString(HttpContext.Current.Session(MetaData.SessionCurrentThemeKey))
        End Get
    End Property
    Public Shared Sub LoadProfile(ByVal UserID As Int64)
        HttpContext.Current.Session(MetaData.SessionCurrentLanguageKey) = LoadLanguage(UserID)
        HttpContext.Current.Session(MetaData.SessionCurrentThemeKey) = LoadTheme(UserID)
    End Sub
    Private Shared Function LoadLanguage(ByVal UserID As Int64) As String
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("ID = " & UserID) Then
            If _Sys_User.InterfaceLang <> "" Then
                Return _Sys_User.InterfaceLang
            Else
                _Sys_User.InterfaceLang = Localization.Default_lang
                _Sys_User.Update("ID = " & UserID)
            End If
        End If
        Return Localization.Default_lang
    End Function
    Private Shared Function LoadTheme(ByVal UserID As Int64) As String
        Dim _Sys_User As New Clssys_Users(New Page)
        If _Sys_User.Find("ID = " & UserID) Then
            If _Sys_User.InterfaceStyle <> "" Then
                Return _Sys_User.InterfaceStyle
            Else
                _Sys_User.InterfaceStyle = Localization.Default_Theme
                _Sys_User.Update("ID = " & UserID)
            End If
        End If
        Return Localization.Default_Theme
    End Function
End Class
