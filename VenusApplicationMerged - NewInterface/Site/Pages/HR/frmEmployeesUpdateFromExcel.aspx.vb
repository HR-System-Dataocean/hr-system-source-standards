Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data
Imports OfficeOpenXml
Imports System.IO

Partial Class frmEmployeesUpdateFromExcel
    Inherits MainPage
#Region "Public Decleration"
    Private ObjClssys_Groups As Clssys_Groups
    Private clsMainOtherFields As clsSys_MainOtherFields
#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ObjClssys_Groups = New Clssys_Groups(Me)
        Dim SearchID As Integer = 0
        Try

            If Not IsPostBack Then
                'Page.Session.Add("ConnectionString", ObjClssys_Groups.ConnectionString)
                'ObjClssys_Groups.AddOnChangeEventToControls("frmEmployeesUpdateFromExcel", Page, UltraWebTab1)

                '================================= Exit & Navigation Notification [ End ]

            End If


            If Not IsPostBack Then UltraWebTab1.SelectedTab = 0

        Catch ex As Exception
            Dim mErrorHandler As New Venus.Shared.ErrorsHandler()
            Page.Session.Add("ErrorValue", ex)
            mErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, ObjClssys_Groups.RegUserID, Venus.Shared.ErrorsHandler.eRecordingType.System_DataBase)
            Page.Response.Redirect("ErrorPage.aspx")
        End Try
    End Sub


#End Region

#Region "Private Functions"

    Protected Sub btnHR_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnHR.Command
        Dim clsEmployees As Clshrs_Employees
        clsEmployees = New Clshrs_Employees(Me)
        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler

        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")

        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)
        Dim filePath As String = ""
        If txtAttachedFile.HasFile Then
            filePath = txtAttachedFile.FileName
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please Select Update File/يجب تحديد ملف التحديث"))
            Exit Sub
        End If
        Dim DT As DataTable = ReadExcelData(filePath, 1)
        Dim RecordsCount As Integer = 0
        If DT.Rows.Count > 0 Then

            For Each row As DataRow In DT.Rows
                If Not String.IsNullOrWhiteSpace(row("رقم الاقامة").ToString()) Then
                    clsEmployees = New Clshrs_Employees(Me)
                    clsEmployees.Find("SSnNo='" & row("رقم الاقامة") & "'")
                    Dim STRUpdate As String = ""
                    Dim str As String = "SELECT  sys_DocumentsDetails.ID FROM    sys_DocumentsDetails  where  sys_DocumentsDetails.DocumentID=1 and sys_DocumentsDetails.ObjectID=281 and  sys_DocumentsDetails.RecordID=" & clsEmployees.ID & " and  isnull(sys_DocumentsDetails.CancelDate,'')=''"
                    Dim DocumntID As String
                    DocumntID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, str)

                    If Not String.IsNullOrWhiteSpace(DocumntID) Then
                        STRUpdate = " UPDATE [dbo].[sys_DocumentsDetails] SET [IssueDate] = '" & row("تاريخ اصدار الاقامة") & "',[ExpiryDate] = '" & row("تاريخ انتهاء الاقامة") & "' WHERE ID=" & DocumntID & "; "
                        STRUpdate &= " UPDATE [dbo].[hrs_Employees] SET [SSNOIssueDate] = '" & row("تاريخ اصدار الاقامة") & "',[SSNOExpireDate] = '" & row("تاريخ انتهاء الاقامة") & "' WHERE hrs_Employees.ID=" & clsEmployees.ID & "; "
                    Else
                        STRUpdate = " INSERT INTO [dbo].[sys_DocumentsDetails]([DocumentID],[ObjectID],[RecordID],[DocumentNumber],[IssueDate],[ExpiryDate],[RegUserID],[RegDate],[IssuedCityID]) VALUES (1,281," & clsEmployees.ID & ",isnull((select max(convert(int,DocumentNumber)) from sys_DocumentsDetails),0)+1,'" & row("تاريخ اصدار الاقامة") & "','" & row("تاريخ انتهاء الاقامة") & "'," & _sys_User.ID & ",getdate(),(select min(id) from sys_cities)); "
                        STRUpdate &= " UPDATE [dbo].[hrs_Employees] SET [SSNOIssueDate] = '" & row("تاريخ اصدار الاقامة") & "',[SSNOExpireDate] = '" & row("تاريخ انتهاء الاقامة") & "' WHERE hrs_Employees.ID=" & clsEmployees.ID & "; "

                    End If
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, CommandType.Text, STRUpdate)

                    Dim strPassPor As String = "SELECT  sys_DocumentsDetails.ID FROM    sys_DocumentsDetails  where  sys_DocumentsDetails.DocumentID=3 and sys_DocumentsDetails.ObjectID=281 and  sys_DocumentsDetails.RecordID=" & clsEmployees.ID & " and  isnull(sys_DocumentsDetails.CancelDate,'')=''"

                    DocumntID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(clsEmployees.ConnectionString, Data.CommandType.Text, strPassPor)

                    If Not String.IsNullOrWhiteSpace(DocumntID) Then
                        STRUpdate = " UPDATE [dbo].[sys_DocumentsDetails] SET [ExpiryDate] = '" & row("تاريخ انتهاء الجواز") & "' WHERE ID=" & DocumntID & "; "
                        STRUpdate &= " UPDATE [dbo].[hrs_Employees] SET [PassportExpireDate] = '" & row("تاريخ انتهاء الجواز") & "',PassPortNo='" & row("رقم الجواز") & "' WHERE hrs_Employees.ID=" & clsEmployees.ID & "; "
                    Else
                        STRUpdate = " INSERT INTO [dbo].[sys_DocumentsDetails]([DocumentID],[ObjectID],[RecordID],[DocumentNumber],[ExpiryDate],[RegUserID],[RegDate],[IssuedCityID]) VALUES (3,281," & clsEmployees.ID & ",isnull((select max(convert(int,DocumentNumber)) from sys_DocumentsDetails),0)+1,'" & row("تاريخ انتهاء الجواز") & "'," & _sys_User.ID & ",getdate(),(select min(id) from sys_cities)); "
                        STRUpdate &= " UPDATE [dbo].[hrs_Employees] SET [PassportExpireDate] = '" & row("تاريخ انتهاء الجواز") & "',PassPortNo='" & row("رقم الجواز") & "' WHERE hrs_Employees.ID=" & clsEmployees.ID & "; "

                    End If
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, CommandType.Text, STRUpdate)
                    RecordsCount = RecordsCount + 1
                End If
            Next

        End If
        Dim msg As String = ObjNavigationHandler.SetLanguage(Page, "Identity and residence data updated successfully..Records Number: /تم تحديث بيانات الهوية والإقامة بنجاح ..عدد السجلات:  ") & RecordsCount.ToString()
        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, msg)
    End Sub
    Protected Sub btnSS_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSS.Command

        ' This code runs when the button is clicked
        Dim User As String = String.Empty
        Dim WebHandler As New Venus.Shared.Web.WebHandler

        WebHandler.GetCookies(Page, "UserID", User)
        Dim _sys_User As New Clssys_Users(Page)
        _sys_User.Find("ID = '" & User & "'")
        Dim clsEmployees As Clshrs_Employees
        clsEmployees = New Clshrs_Employees(Me)
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsEmployees.ConnectionString)

        Dim filePath As String = ""
        If txtBankFile.HasFile Then
            filePath = txtBankFile.FileName
        Else
            Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Please Select Update File/يجب تحديد ملف التحديث"))
            Exit Sub
        End If
        Dim DT As DataTable = ReadExcelData(filePath, 2)
        Dim RecordsCount As Integer = 0
        If DT.Rows.Count > 0 Then
            Dim strHistory As String = ""
            For Each row As DataRow In DT.Rows
                If Not String.IsNullOrWhiteSpace(row("EmployeeCode").ToString()) Then
                    clsEmployees = New Clshrs_Employees(Me)
                    clsEmployees.Find("Code='" & row("EmployeeCode") & "'")
                    If clsEmployees.ID > 0 Then

                        Dim ClsBanksOld As New ClsBasicFiles(Me.Page, "sys_Banks")
                        ClsBanksOld.Find("ID='" & clsEmployees.BankID & "'")

                        Dim ClsBanks As New ClsBasicFiles(Me.Page, "sys_Banks")
                        ClsBanks.Find("Code='" & row("BankCode") & "'")

                        If ClsBanks.ID > 0 Then
                            'clsEmployees.BankID = ClsBanks.ID
                            'clsEmployees.BankAccountNumber = row("BankAccountNumber")
                            'clsEmployees.Update("ID='" & clsEmployees.ID & "'")
                            strHistory &= " UPDATE [dbo].[hrs_Employees] SET [BankID] = " & ClsBanks.ID & ",[BankAccountNumber] = '" & row("BankAccountNumber") & "' WHERE hrs_Employees.ID=" & clsEmployees.ID & "; "
                            strHistory += " INSERT INTO [dbo].[hrs_EmployeesBankHistory] ([EmployeeCode],[BankCode],[BankAccountNumber],[BankCodeOld],[BankAccountNumberOld],[RegUserID],[RegDate])   values (" & row("EmployeeCode") & ",'" & row("BankCode") & "','" & row("BankAccountNumber") & "','" & ClsBanksOld.Code & "','" & clsEmployees.BankAccountNumber & "'," & _sys_User.ID & ",getdate()); "
                            RecordsCount = RecordsCount + 1
                        End If


                    End If
                End If
            Next
            If strHistory <> "" Then
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(clsEmployees.ConnectionString, CommandType.Text, strHistory)
            End If

        End If
        'Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Employees Banks Data Update Done/تم تحديث البيانات البنكية بنجاح"))
        Dim msg As String = ObjNavigationHandler.SetLanguage(Page, "Employees Banks data updated successfully..Records Number: /تم تحديث البيانات البنكية بنجاح ..عدد السجلات:  ") & RecordsCount.ToString()
        Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, msg)
    End Sub

    Public Function ReadExcelData(ByVal myfile As String, ByVal myType As Integer) As DataTable

        Dim dt As DataTable = New DataTable()
        Dim fileName As String = Path.GetFileName(myfile)
        Dim filePath As String = Server.MapPath("~/Uploads/" & fileName)
        If myType = 1 Then
            txtAttachedFile.SaveAs(filePath)
        Else
            txtBankFile.SaveAs(filePath)
        End If

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial
        Using package As New ExcelPackage(New FileInfo(filePath))
            Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets(0)
            Dim colCount = worksheet.Dimension.End.Column
            Dim rowCount = worksheet.Dimension.End.Row

            ' Add columns
            For col = 1 To colCount
                dt.Columns.Add(worksheet.Cells(1, col).Text)
            Next

            ' Add rows
            For row = 2 To rowCount
                Dim newRow = dt.NewRow()
                For col = 1 To colCount
                    newRow(col - 1) = worksheet.Cells(row, col).Text
                Next
                dt.Rows.Add(newRow)
            Next
            Return dt
        End Using
        Return Nothing ' In case user cancels file selection
    End Function


#End Region
End Class
