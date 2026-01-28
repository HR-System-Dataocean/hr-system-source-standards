Imports System.Data
Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource

Partial Class _UserProfile
    Inherits MainPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadEmployeeProfile()
        End If
    End Sub
    Protected Sub LoadEmployeeProfile()
        If ProfileCls.RetRefPeople() <> Nothing Then
            Dim HrsEmployees As New Clshrs_Employees(Me)
            HrsEmployees.Find1("ID = " & ProfileCls.RetRefPeople())
            LinkButton9.OnClientClick = "OpenModal1('../../../Interfaces/frmReportsGridViewer.aspx?Language=" & IIf(ProfileCls.CurrentLanguage() = "Ar", "true", "false") & "&Criteria=EmployeeCode&preview=1&ReportCode=EmployeeDetails&sq0=&v=" & HrsEmployees.Code & "',450,844); return false;"
            Dim ClsObjects As New Clssys_Objects(Page)
            Dim ClsDocumentsDetails As New Clssys_DocumentsInformations(Me.Page)
            If ClsObjects.Find(" Code='" & ClsDocumentsDetails.Table.ToString.Trim() & "'") Then
                LinkButton8.OnClientClick = "OpenModal1('../../HR/frmAttachDocuments.aspx?OId=" & ClsObjects.ID & "&RID=" & HrsEmployees.ID & "&isshow=0',280,744); return false;"
            End If
            Dim ClsEmployeesTransactions As New Clshrs_EmployeesTransactions(Me.Page)
            If ClsEmployeesTransactions.Find("PostDate is not null and EmployeeID = " & HrsEmployees.ID & " and PrepareType = 'N' order by FiscalYearPeriodID DESC") Then
                Dim ClsFiscalYearPeriod As New Clssys_FiscalYearsPeriods(Me.Page)
                ClsFiscalYearPeriod.Find("ID = " & ClsEmployeesTransactions.FiscalYearPeriodID)
                LinkButton5.Text = LinkButton5.Text & " - '" & IIf(ProfileCls.CurrentLanguage() = "Ar", ClsFiscalYearPeriod.ArbName, ClsFiscalYearPeriod.EngName) & "'"
                LinkButton5.OnClientClick = "OpenModal1('../../../Interfaces/frmReportsGridViewer.aspx?Language=" & IIf(ProfileCls.CurrentLanguage() = "Ar", "true", "false") & "&Criteria=EmployeeCode|DepartmentCode|BranchCode|ContractTypeCode|FiscalPeriod|BankAccountStatus|Applyed|PayMethod|SectorCode|CostOne|CostTow|CostThree|CostFour|ProjectCode&preview=1&ReportCode=PaySlip&sq0=&v=" & HrsEmployees.Code & "||||" & ClsFiscalYearPeriod.EngName & "|||||||||',450,844); return false;"
            End If
            LinkButton7.Text = LinkButton7.Text & " - '" & DateTime.Now.ToString("yyyy-MM-dd") & "' : '" & DateTime.Now.AddMonths(-1).AddDays(1).ToString("yyyy-MM-dd") & "'"
            LinkButton7.OnClientClick = "OpenModal1('../../../Interfaces/frmReportsGridViewer.aspx?Language=" & IIf(ProfileCls.CurrentLanguage() = "Ar", "true", "false") & "&Criteria=EmpCode|ProfCode|NatCode|DeptCode|Branch|ProjectCode|FromDate|ToData|PlacementCode&preview=1&ReportCode=AttendanceTrans&sq0=&v=" & HrsEmployees.Code & "||||||" & DateTime.Now.AddMonths(-1).AddDays(1).ToString("dd/MM/yyyy") & "|" & DateTime.Now.ToString("dd/MM/yyyy") & "|',450,844); return false;"
            LinkButton6.OnClientClick = "OpenModal1('../../../Interfaces/frmReportsGridViewer.aspx?Language=" & IIf(ProfileCls.CurrentLanguage() = "Ar", "true", "false") & "&Criteria=EmpCode|FromEmpCode|ToEmpCode|DeptCode|LoanTypeCode|LoanNo|FromLoanDate|UpToLoanDate|WithSalary&preview=1&ReportCode=rptLoansTotal&sq0=&v=" & HrsEmployees.Code & "||||||01/01/1900|31/12/2050|1',450,844); return false;"

            LinkButtonSFCHS.OnClientClick = "OpenModal1('../../../Interfaces/frmReportsGridViewer.aspx?Language=" & IIf(ProfileCls.CurrentLanguage() = "Ar", "true", "false") & "&Criteria=EmpCode&preview=1&ReportCode=PrintSCFHSLetter&sq0=&v=" & HrsEmployees.Code & "|',450,844); return false;"

            LblCode.Text += HrsEmployees.Code
            LabelName.Text += HrsEmployees.EnglishName
            LabelBirthDate.Text += HrsEmployees.BirthDate
            LabelJoinDate.Text += HrsEmployees.JoinDate
            LabelPhone.Text += HrsEmployees.Phone
            LabelMobile.Text += HrsEmployees.Mobile
            LabelMail.Text += HrsEmployees.E_Mail

            Dim ClsBranch = New Clssys_Branches(Me)
            ClsBranch.Find("ID = " & HrsEmployees.BranchID)
            LabelBranch.Text += IIf(ProfileCls.CurrentLanguage() = "Ar", ClsBranch.ArbName, ClsBranch.EngName)

            Dim ClsDepartment = New Clssys_Departments(Me)
            ClsDepartment.Find("ID = " & HrsEmployees.DepartmentID)
            Label_Department.Text += IIf(ProfileCls.CurrentLanguage() = "Ar", ClsDepartment.ArbName, ClsDepartment.EngName)

            Dim ClsNationality = New Clssys_Nationality(Me)
            ClsNationality.Find("ID = " & HrsEmployees.NationalityID)
            LabelNationality.Text += IIf(ProfileCls.CurrentLanguage() = "Ar", ClsNationality.ArbName, ClsNationality.EngName)

            Dim ClsContract As New Clshrs_Contracts(Page)
            Dim IntContractId As Integer = 0
            IntContractId = ClsContract.ContractValidatoinId(HrsEmployees.ID, DateTime.Now)
            If ClsContract.Find("ID = " & IntContractId) Then
                Dim ClsPosition = New Clshrs_Positions(Me)
                ClsPosition.Find("ID = " & ClsContract.PositionID)
                Label_Position.Text += IIf(ProfileCls.CurrentLanguage() = "Ar", ClsPosition.ArbName, ClsPosition.EngName)
            End If
            ClsObjects = New Clssys_Objects(Me.Page)
            ClsObjects.Find(" Code = REPLACE('" & HrsEmployees.Table & "',' ' ,'')")
            Dim ObjectID As Integer = ClsObjects.ID
            DisplayImage(HrsEmployees.ID, ObjectID, Image1)


            '========================================
          
          
            '================================
        End If
        Dim ClsDocumentAttachment As New Clssys_DocumentsInformationAttachments(Me.Page)
        Dim clsCompaniesRoles As New Clssys_CompanyRoles(Me)
        clsCompaniesRoles.Find("Code=002")
        Dim ClsObjects2 As New Clssys_Objects(Me.Page)
        ClsObjects2.Find("Code='" & clsCompaniesRoles.Table.ToString().Trim() & "'")

        If ClsDocumentAttachment.Find("RecordID=" & clsCompaniesRoles.ID & " And ObjectID= " & ClsObjects2.ID & " And Isnull(CancelDate , '') = '' ") Then
            lnkDownload.NavigateUrl = "~/Uploads/" & ClsDocumentAttachment.FileName
        End If
    End Sub
    Private Function DisplayImage(ByVal IntRecordId As Integer, ByVal IntObjectId As Integer, ByRef ImgImageControl As System.Web.UI.WebControls.Image) As String
        Dim ClsDocumentAttachment = New Clssys_DocumentsInformationAttachments(Me.Page)
        If ClsDocumentAttachment.Find(" RecordID=" & IntRecordId & " And ObjectID=" & IntObjectId & " And IsImageView is not Null And IsNull(CancelDate , '') = '' Order By IsProfilePicture Desc ") Then
            If ClsDocumentAttachment.ExpiryDate > Date.Now Or ClsDocumentAttachment.ExpiryDate = Nothing Then
                ImgImageControl.ImageUrl = "~/Photos/" & IntObjectId.ToString & "_" & IntRecordId.ToString & "/" + ClsDocumentAttachment.FileName
            Else
                ImgImageControl.ImageUrl = "~/Common/Images/DefaultPicture.jpg"
            End If
        Else
            ImgImageControl.ImageUrl = "~/Common/Images/DefaultPicture.jpg"
        End If
        Return ""
    End Function
End Class
