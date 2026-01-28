'======================================================================
'Project name  : Venus V. 
'Program name  : frmSearchScreen.aspx.vb
'Date Created  : DatOcean  
'Date Modified : 19-07-2007  
'Issue #       :       
'Developer     : DataOcean 
'              : [AGR] Abdul elJaleel  [AGR] 
'Description   :
'Modifacations: 
'======================================================================
Imports Venus.Application.SystemFiles.System

Partial Class Interfaces_frmSearchScreen
    Inherits System.Web.UI.Page
    Protected Overrides Sub InitializeCulture()
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        Dim _culture As String = StrLanguage
        Dim ci As New System.Globalization.CultureInfo(_culture)
        Dim StrDateFormat As String = System.Configuration.ConfigurationManager.AppSettings("DATEFORMAT")
        Dim myDateTimePatterns() As String = {StrDateFormat, StrDateFormat}
        Dim DateTimeFormat As New System.Globalization.DateTimeFormatInfo
        DateTimeFormat = ci.DateTimeFormat
        DateTimeFormat.SetAllDateTimePatterns(myDateTimePatterns, "d"c)
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
        System.Threading.Thread.CurrentThread.CurrentUICulture = ci
        MyBase.InitializeCulture()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim IntSearchID As String = Request.QueryString.Item("SearchID")
        Dim StrTargetControl As String = Request.QueryString.Item("TargetControl")
        Dim ClsDataAccessLayer As New ClsDataAcessLayer(Page)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(ClsDataAccessLayer.ConnectionString)
        Dim ObjDs As New Data.DataSet
        Dim ObjSearch As New ClsSys_Search_Main(Page)
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty

        WebHandler.GetCookies(Page, "Lang", StrLanguage)
        TargetControl.Text = StrTargetControl
        lblMainHeader.Text = ObjSearch.ReadSearchName(IntSearchID, StrLanguage)
        ObjSearch.ReadSearchParameter(IntSearchID, pnlCriterias, Me.name, Me.realname, UwgSearch.ID, btnSearch, StrLanguage)
        Venus.Shared.Web.ClientSideActions.GridSelection(Page, Me.UwgSearch)
        SetData()
        btnSearch.Attributes.Add("onclick", "MainSearch_Start()")
    End Sub

    Protected Sub UwgSearchUsers_InitializeDataSource(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs) Handles UwgSearch.InitializeDataSource
        SetData()
    End Sub

    Protected Sub UwgSearchUsers_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.WebUI.UltraWebGrid.LayoutEventArgs) Handles UwgSearch.InitializeLayout
        Me.UwgSearch.Browser = Infragistics.WebUI.UltraWebGrid.BrowserLevel.Xml
        Me.UwgSearch.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.Automatic
        Me.UwgSearch.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical
    End Sub

    Public Function SetData() As Boolean
        Dim ObjSearch As New ClsSys_Search_Main(Page)
        Dim ObjDs As New Data.DataSet
        Dim IntSearchID As String = Request.QueryString.Item("SearchID")
        Dim StrTargetControl As String = Request.QueryString.Item("TargetControl")
        Dim Cond As String = ""
        If Request.QueryString("Cond") Is Nothing Then
            Cond = ""
        Else
            Cond = Request.QueryString("Cond")
        End If
        Dim IntCounter As Integer
        Dim IntNormalWidth As Integer
        Dim WebHandler As New Venus.Shared.Web.WebHandler
        Dim StrLanguage As String = String.Empty
        Try
            Me.UwgSearch.DataSource = Nothing
            Me.UwgSearch.DataBind()
            Me.UwgSearch.Rows.Clear()

            WebHandler.GetCookies(Page, "Lang", StrLanguage)
            If ObjSearch.ViewData(IntSearchID, PrepareSearchCriteria(Me.value.Text), ObjDs, StrLanguage, Cond, True) Then
                Me.UwgSearch.DataSource = ObjDs.Tables(0).DefaultView
                Me.UwgSearch.DataBind()
                txtRankofCodeCell.Value = UwgSearch.Columns.FromKey("Code").Index
                IntNormalWidth = UwgSearch.Width.Value / UwgSearch.Columns.Count
                For IntCounter = 0 To UwgSearch.Columns.Count - 1
                    With UwgSearch.Columns(IntCounter)
                        .Width = New System.Web.UI.WebControls.Unit(IntNormalWidth, UnitType.Pixel)
                    End With
                Next
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Function PrepareSearchCriteria(ByVal StrCriteria As String) As String
        Try
            Dim StrPrepared As String = String.Empty
            Dim ObjStringHandler As New Venus.Shared.StringHandler()
            Dim StrFinalStr As String = String.Empty
            Dim Separator() As Char = {"$", "@"}
            Dim StrFields As String() = StrCriteria.Replace(" And ", "$").Replace(" or  ", "@").Split(Separator)
            Dim StrFieldName As String = String.Empty
            For Each StrField As String In StrFields
                If StrField <> "" Then
                    StrPrepared = StrField
                    If StrPrepared.Contains("4S") Then
                        StrFieldName = StrField.Replace("like", "!").Split("!")(0).Trim()
                        StrPrepared = StrPrepared.Substring(StrPrepared.IndexOf("like", 0) + 4).Trim()
                        StrPrepared = StrPrepared.Replace("'%", "").Replace("%'", "")
                        StrPrepared = ObjStringHandler.ReplaceHamza(StrPrepared)
                        StrFinalStr &= StrFieldName & " like '%" & StrPrepared & IIf(Len(txtSearchAll.Text), "%' or  ", "%' And ")
                    Else
                        StrFinalStr &= StrField & IIf(Len(txtSearchAll.Text), " or  ", " And ")
                    End If
                End If
            Next
            If StrFinalStr.EndsWith(" And ") Then
                StrFinalStr = StrFinalStr.Remove(StrFinalStr.LastIndexOf(" And "), 4)
            ElseIf StrFinalStr.EndsWith(" or  ") Then
                StrFinalStr = StrFinalStr.Remove(StrFinalStr.LastIndexOf(" or  "), 4)
            End If
            Return StrFinalStr
        Catch ex As Exception
            Return StrCriteria
        End Try
    End Function

End Class
