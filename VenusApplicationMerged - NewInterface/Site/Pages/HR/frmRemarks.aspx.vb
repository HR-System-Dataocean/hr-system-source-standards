Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class Interfaces_frmRemarks
    Inherits System.Web.UI.Page
    Protected Sub WebImageButton1_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        If Request.QueryString.Count > 2 Then
            Dim ClsRemarks1 As New ClsRemarks(Page, "", Request.QueryString.Item("RId"), Request.QueryString.Item("OId"))
            ClsRemarks1.TableName = ""
            ClsRemarks1.ID = Request.QueryString.Item("RId")
            ClsRemarks1.ObjectID = Request.QueryString.Item("OId")
            ClsRemarks1.NewUpdateRemarks(txtRemarks.Text)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
            Exit Sub
        End If
        Dim IntRecordID As Int64 = Request.QueryString.Item("ID")
        Dim StrTableName As String = Request.QueryString.Item("TableName")
        Dim ClsRemarks As New ClsRemarks(Page, StrTableName, IntRecordID, 0)
        lblRecordID.Text = IntRecordID
        lblTableName.Text = StrTableName
        ClsRemarks.TableName = StrTableName
        ClsRemarks.ID = IntRecordID
        ClsRemarks.UpdateRemarks(txtRemarks.Text)
        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
    End Sub
    Protected Sub WebImageButton2_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnCancel.Click
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "CloseMe()", True)
        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
    End Sub
    Protected Overrides Sub InitializeCulture()
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage
        If (_culture <> "Auto") Then
            Dim ci As New System.Globalization.CultureInfo(_culture)
            Dim StrDateFormat As String = System.Configuration.ConfigurationManager.AppSettings("DATEFORMAT")
            Dim myDateTimePatterns() As String = {StrDateFormat, StrDateFormat}
            Dim DateTimeFormat As New System.Globalization.DateTimeFormatInfo
            DateTimeFormat = ci.DateTimeFormat
            DateTimeFormat.SetAllDateTimePatterns(myDateTimePatterns, "d"c)
            System.Threading.Thread.CurrentThread.CurrentCulture = ci
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci
        End If
        MyBase.InitializeCulture()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString.Count > 2 Then
            Dim ClsRemarks1 As New ClsRemarks(Page, "", Request.QueryString.Item("RId"), Request.QueryString.Item("OId"))
            Dim StrRemarks1 As String = String.Empty
            lblRecordID.Text = Request.QueryString.Item("RId")
            If Not IsPostBack Then
                If ClsRemarks1.NewReadRemarks(Request.QueryString.Item("RId"), "", StrRemarks1, Request.QueryString.Item("OId")) Then
                    txtRemarks.Text = StrRemarks1
                End If
            End If
            Exit Sub
        End If
        Dim IntRecordID As Int64 = Request.QueryString.Item("ID")
        Dim StrTableName As String = Request.QueryString.Item("TableName")
        Dim ClsRemarks As New ClsRemarks(Page, StrTableName, IntRecordID, 0)
        Dim StrRemarks As String = String.Empty
        Dim Arr(1) As String
        Arr(0) = "txtRemarks"
        lblRecordID.Text = IntRecordID
        lblTableName.Text = StrTableName
        If Not IsPostBack Then
            If ClsRemarks.ReadRemarks(IntRecordID, StrTableName, StrRemarks) Then
                txtRemarks.Text = StrRemarks
            End If
        End If
    End Sub
End Class
