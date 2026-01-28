Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class CompanyRoles
    Inherits MainPage
    Private mRemarks As String
    Public Property GetStyleSheet() As String
        Get
            If ProfileCls.CurrentTheme = "Blue_En" Then
                Return "../../Common/SpecialStyles/element_style_BE.css"
            ElseIf ProfileCls.CurrentTheme = "Blue_Ar" Then
                Return "../../Common/SpecialStyles/element_style_BA.css"
            ElseIf ProfileCls.CurrentTheme = "Silver_En" Then
                Return "../../Common/SpecialStyles/element_style_SE.css"
            Else
                Return "../../Common/SpecialStyles/element_style_SA.css"
            End If
        End Get
        Set(ByVal Value As String)
            mRemarks = Value
        End Set
    End Property
    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim clsCompaniesRoles As New Clssys_CompanyRoles(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsCompaniesRoles.ConnectionString)

        Dim FrmID As String = ""
        If Request.QueryString.Count > 0 Then
            FrmID = Request.QueryString(0).ToString()
        End If

        Dim Str As String = ""
        Str = Str & "<div><span>" & ObjNavigationHandler.SetLanguage(Page, "Company Roles And Procedures /إجراءات ولوائح الشركة") & "</span></div>"
        Str = Str & "<ul>"
        If clsCompaniesRoles.Find("RoleLevel = 0 " & IIf(FrmID = "", "", " and RelatedForm = " & FrmID)) Then
            For Each dr As Data.DataRow In clsCompaniesRoles.DataSet.Tables(0).Rows
                clsCompaniesRoles.Find("ID = " & dr(0) & IIf(FrmID = "", "", " and RelatedForm = " & FrmID))
                Str = Str & "<li class=""color1""><a>" & ObjNavigationHandler.SetLanguage(Page, clsCompaniesRoles.EngName & "/" & clsCompaniesRoles.ArbName) & "</a>"
                Str = Str & "<ul>"
                If clsCompaniesRoles.Find("RoleLevel = " & clsCompaniesRoles.ID) Then
                    For Each dr1 As Data.DataRow In clsCompaniesRoles.DataSet.Tables(0).Rows
                        clsCompaniesRoles.Find("ID =" & dr1(0))
                        '====================
                        Dim ClsDocumentAttachment As New Clssys_DocumentsInformationAttachments(Me.Page)
                        Dim ClsObjects As New Clssys_Objects(Me.Page)
                        ClsObjects.Find("Code='" & clsCompaniesRoles.Table.ToString().Trim() & "'")

                        If ClsDocumentAttachment.Find("RecordID=" & clsCompaniesRoles.ID & " And ObjectID= " & ClsObjects.ID & " And Isnull(CancelDate , '') = '' ") Then


                            Dim h As New HyperLink
                            h.Text = ObjNavigationHandler.SetLanguage(Page, clsCompaniesRoles.EngName & "/" & clsCompaniesRoles.ArbName)
                          
                        Else
                            Str = Str & "<li><a href=""#"">" & ObjNavigationHandler.SetLanguage(Page, clsCompaniesRoles.EngName & "/" & clsCompaniesRoles.ArbName) & "</a></li>"
                        End If
                       

                    Next
                End If
                Str = Str & "</ul>"
                Str = Str & "</li>"
            Next
        End If
        Str = Str & "</ul>"
        Nav.InnerHtml = Str
    End Sub
End Class
