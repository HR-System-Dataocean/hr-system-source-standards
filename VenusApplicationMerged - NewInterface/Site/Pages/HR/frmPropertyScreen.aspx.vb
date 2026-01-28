Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Partial Class frmPropertyScreen
    Inherits MainPage

    Const CIntialTable = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim IntRecordID As Integer = Request.QueryString.Item("ID")
        Dim StrTableName As String = Request.QueryString.Item("TableName")
        Dim ClsRecord As New Clssys_RecordProperties(Page, StrTableName)
        Dim ClsUser As New Clssys_Users(Page)
        Dim ClsObjects As New Clssys_Objects(Page)
        If Not IsPostBack Then
            ClsRecord.GetRecordProperties(IntRecordID, StrTableName)
            lblRegistrationDate.Text = Convert.ToDateTime(ClsRecord.RegDate).ToShortDateString

            If ClsRecord.CancelDate = Nothing Then
                lblRecordCancelDate.Text = ""
                WebDateChooser1.Value = ""
                WebDateChooser1.Enabled = False
                CheckBox1.Checked = False
            Else
                lblRecordCancelDate.Text = ClsRecord.CancelDate
                WebDateChooser1.Value = ClsRecord.CancelDate
                CheckBox1.Checked = True
            End If

            lblRecordId.Text = ClsRecord.ID
            If Not ClsRecord.RegUserID Is Nothing Then
                ClsUser.Find(" ID= " & ClsRecord.RegUserID)
                lblRegestreduser.Text = ClsUser.EngName
            End If
        End If

        'CheckBox1.Attributes.Add("onclick", "checkRecordCancelDate();")
        ClsObjects.Find("Code='" & Replace(StrTableName, " ", "") & "'")

        UwgPropertyPage.DataSource = ClsRecord.GetRecordHistory(IntRecordID, ClsObjects.ID, "", StrTableName)
        UwgPropertyPage.DataBind()

        For decision = 0 To UwgPropertyPage.Rows.Count - 1
            Dim colName As String
            Dim colId As String = ""
            Try
                colId = UwgPropertyPage.Rows(decision).Cells.FromKey("Field Name").Value.ToString()
            Catch ex As Exception
                ' Handle missing column or null values
                colId = "N/A"
            End Try
            Dim name As String = ""
            Dim mSelectCommand As String = ""
            If colId = "MaritalStatusID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_MaritalStatus where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If
            If colId = "ContractPeriod" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From sys_FiscalYearsPeriods where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If
            If colId = "BankID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From sys_Banks where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()
            End If

            If colId = "ReligionID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_Religions where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()
            End If

            If colId = "BloodGroupID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_BloodGroups where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()
            End If
            If colId = "NationalityID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From sys_Nationalities where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "DepartmentID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From sys_Departments where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "BranchID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From sys_Branches where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If
            If colId = "GradeStepID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_GradesSteps where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "SponsorID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_Sponsors where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "ManagerID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = " [dbo].[fn_GetEmpName](hrs_Employees.Code,1) "
                Else
                    name = "[dbo].[fn_GetEmpName](hrs_Employees.Code,0)"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_Employees where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "SectorID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From sys_Sectors where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "LocationID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From sys_Locations where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "ContractTypeID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_ContractsTypes where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "ProfessionID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_Professions where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "PositionID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_Positions where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If
            If colId = "EmployeeClassID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_EmployeesClasses where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "EducationID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From hrs_Educations where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If colId = "BirthCityID" Then
                If ProfileCls.CurrentLanguage = "Ar" Then
                    name = "ArbName"
                Else
                    name = "EngName"
                End If
                mSelectCommand = " Select top 1 " & name & " as OldData From sys_Cities where ID=" & UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value.ToString()

            End If

            If mSelectCommand <> "" Then
                Dim MyName = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ClsRecord.ConnectionString, Data.CommandType.Text, mSelectCommand)
                UwgPropertyPage.Rows(decision).Cells.FromKey("Old Data").Value = MyName
            End If



        Next

    End Sub

    Protected Sub btnAddField_Click(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnAddField.Click
        Dim IntRecordID As Integer = Request.QueryString.Item("ID")
        Dim StrTableName As String = Request.QueryString.Item("TableName")

        Dim ClsRecord As New Clssys_RecordProperties(Page, StrTableName)
        If CheckBox1.Checked = True Then
            If IsNothing(WebDateChooser1.Value) Then
                If ClsRecord.SetRecordPropertiesCancelDate(IntRecordID, Convert.ToDateTime(DateTime.Now), StrTableName) Then
                    lblRecordCancelDate.Text = Convert.ToDateTime(DateTime.Now)
                End If
            Else
                If WebDateChooser1.Value.ToString = "" Then
                    WebDateChooser1.Value = Convert.ToDateTime(DateTime.Now)
                End If
                If ClsRecord.SetRecordPropertiesCancelDate(IntRecordID, Convert.ToDateTime(WebDateChooser1.Value), StrTableName) Then
                    lblRecordCancelDate.Text = Convert.ToDateTime(WebDateChooser1.Value)
                End If
            End If
        Else
            ClsRecord.SetRecordPropertiesCancelDate(IntRecordID, Nothing, StrTableName)
            lblRecordCancelDate.Text = ""
        End If
        Venus.Shared.Web.ClientSideActions.ClosePage(Page)
    End Sub
End Class
