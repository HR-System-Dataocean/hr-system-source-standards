Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class DocumentDesign
    Inherits MainPage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        TBHEADER.Style("background-image") = IIf(ProfileCls.CurrentTheme().Contains("Blue") = True, "url('../../Common/Images/BlueHeader.png');", "url('../../Common/Images/SilverHeader.png');")
        Dim _Dwf_ListItem As New ClsDwf_ListItems(Me.Page)
        _Dwf_ListItem.Find("CancelDate is null")
        RetDropDownList(Me.DropDownList_LOV, _Dwf_ListItem.DataSet.Tables(0), "ID", IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")).DataBind()

        Dim _Sys_Search As New Clssys_Searchs(Me.Page)
        _Sys_Search.Find("CancelDate is null")
        RetDropDownList(Me.DropDownList_SearchSrc, _Sys_Search.DataSet.Tables(0), "ID", IIf(ProfileCls.CurrentLanguage() = "Ar", "ArbName", "EngName")).DataBind()

        Dim _Sys_Object As New Clssys_Objects(Me.Page)
        _Sys_Object.Find("CancelDate is null")
        RetDropDownList(Me.DropDownList_KeyTable, _Sys_Object.DataSet.Tables(0), "Code", IIf(ProfileCls.CurrentLanguage() = "Ar", "Code", "Code")).DataBind()

        Dim _Sys_Form As New ClsSys_Forms(Me.Page)
        _Sys_Form.Find("CancelDate is null")
        RetDropDownList(Me.DropDownList_Zooming, _Sys_Form.DataSet.Tables(0), "Code", IIf(ProfileCls.CurrentLanguage() = "Ar", "Code", "Code")).DataBind()
    End Sub
    Public Shared Function RetDropDownList(ByRef DDL As DropDownList, ByRef DT As System.Data.DataTable, ByVal valueMember As String, ByVal DisMember As String) As DropDownList
        DDL.DataSource = DT
        DDL.DataValueField = valueMember
        DDL.DataTextField = DisMember
        Return (DDL)
    End Function
End Class
