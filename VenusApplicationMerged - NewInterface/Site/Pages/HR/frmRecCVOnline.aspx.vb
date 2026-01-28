Imports Venus.Application.SystemFiles.System
Imports Venus.Application.SystemFiles.HumanResource
Imports System.Data
Imports System.Globalization


Partial Class frmRecCVOnline
    Inherits MainPage

#Region "Public Decleration"
    Private mConnString As String = String.Empty

#End Region

#Region "Protected Sub"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim CVcode As String = Request.QueryString.Item("CVcode")
            Dim clsBioGraphies As New ClsRec_BioGraphies(Me)
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(clsBioGraphies.ConnectionString)
            mConnString = clsBioGraphies.ConnectionString
            If Not IsPostBack Then

                If CVcode.Length > 0 Then
                    txtCode.ReadOnly = True
                    btnSave.Enabled = False
                Else
                    txtCode.ReadOnly = False
                    btnSave.Enabled = True
                End If

                GetDropDownList(ddlNationality, True, "Select * From sys_Nationalities Where CancelDate is null ")
                GetDropDownList(ddlPosition, True, "Select * From Rec_Positions Where CancelDate is null ")
                GetList(uwgHistory.DisplayLayout.Bands(0).Columns(8).ValueList, False, "Select * From Rec_Positions Where CancelDate is null ")
                GetList(uwgCertifications.DisplayLayout.Bands(0).Columns(1).ValueList, False, "Select * From Rec_EducationDegree Where CancelDate is null ")

                With ObjNavigationHandler
                    AddValueListGrid(uwgLanguage, "Language_ID", .SetLanguage(Page, "[Select Your Language]/[ إختر أحد اللغات ] "), .SetLanguage(Page, "Engilsh/الانجليزي"), .SetLanguage(Page, "Arabic/عربي"))
                    AddValueListGrid(uwgLanguage, "SLevel_ID", .SetLanguage(Page, "BAD/ضعيف"), .SetLanguage(Page, "GOOD/جيد"), .SetLanguage(Page, "EXCELLENT/ممتاز"))
                    AddValueListGrid(uwgLanguage, "WLevel_ID", .SetLanguage(Page, "BAD/ضعيف"), .SetLanguage(Page, "GOOD/جيد"), .SetLanguage(Page, "EXCELLENT/ممتاز"))
                End With
                If CVcode <> "" Then
                    txtCode.Text = CVcode
                    CheckCode()
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub txtCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtCode.TextChanged
        CheckCode()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As Infragistics.WebUI.WebDataInput.ButtonEventArgs) Handles btnSave.Click
        Try
            Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnString)
            If txtCode.Text.Length = 0 Then
                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, " You Must Enter Code /لابد من إدخال الكود"))
                Return
            End If
            SavePart()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImageButton_Print_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton_Print.Click
        Venus.Shared.Web.ClientSideActions.PrintWindow(Me)
    End Sub

#End Region

#Region "Private Function"
    Private Sub AddValueListGrid(ByVal GridName As Infragistics.WebUI.UltraWebGrid.UltraWebGrid, ByVal Columns As Object, ByVal ParamArray ValueText() As String)
        If IsNumeric(Columns) Then
            GridName.DisplayLayout.Bands(0).Columns(Columns).ValueList.ValueListItems.Clear()
        Else
            GridName.DisplayLayout.Bands(0).Columns.FromKey(Columns).ValueList.ValueListItems.Clear()
        End If
        For I As Integer = 0 To ValueText.Length - 1
            If IsNumeric(Columns) Then
                GridName.DisplayLayout.Bands(0).Columns(Columns).ValueList.ValueListItems.Add(I, ValueText(I))
            Else
                GridName.DisplayLayout.Bands(0).Columns.FromKey(Columns).ValueList.ValueListItems.Add(I, ValueText(I))
            End If
        Next
    End Sub

    Enum SaveType
        Update
        Insert
    End Enum

    Private Function AssignValue(ByRef BioGraphies_Dataset As DataSet, ByVal SaveType As SaveType) As Boolean
        Try
            Dim SqlCommand As String = String.Empty

            Select Case SaveType
                Case Global.frmRecCVOnline.SaveType.Update

                    SqlCommand = "Set DateFormat DMY Update Rec_BioGraphies Set " & _
                                 "Code=@Code," & _
                                 "EngName=@EngName," & _
                                 "ArbName=@ArbName," & _
                                 "ArbName4S=@ArbName4S," & _
                                 "FamilyEngName=@FamilyEngName," & _
                                 "FamilyArbName=@FamilyArbName," & _
                                 "FamilyArbName4S=@FamilyArbName4S," & _
                                 "FatherEngName=@FatherEngName," & _
                                 "FatherArbName=@FatherArbName," & _
                                 "FatherArbName4S=@FatherArbName4S," & _
                                 "GrandEngName=@GrandEngName," & _
                                 "GrandArbName=@GrandArbName," & _
                                 "GrandArbName4S=@GrandArbName4S," & _
                                 "E_Mail=@E_Mail," & _
                                 "Position_ID=@Position_ID," & _
                                 "MaritalStatus=@MaritalStatus," & _
                                 "NODependant=@NODependant," & _
                                 "LastJob=@LastJob," & _
                                 "LastSalary=@LastSalary," & _
                                 "ExpectedSalary=@ExpectedSalary," & _
                                 "Address=@Address," & _
                                 "HasDLicense=@HasDLicense," & _
                                 "Nationality_ID=@Nationality_ID," & _
                                 "IqamaNo=@IqamaNo," & _
                                 "PassportNo=@PassportNo," & _
                                 "HasTIqama=@HasTIqama," & _
                                 "NOFSponser=@NOFSponser," & _
                                 "HSpConditions=@HSpConditions," & _
                                 "SpecialConditions=@SpecialConditions," & _
                                 "IsUsed=@IsUsed," & _
                                 "Remarks=@Remarks," & _
                                 "Mobile=@Mobile," & _
                                 "Phone=@Phone"

                Case Global.frmRecCVOnline.SaveType.Insert
                    SqlCommand = "Set DateFormat DMY Insert Into Rec_BioGraphies ( " & _
                                 "Code," & _
                                 "EngName," & _
                                 "ArbName," & _
                                 "ArbName4S," & _
                                 "FamilyEngName," & _
                                 "FamilyArbName," & _
                                 "FamilyArbName4S," & _
                                 "FatherEngName," & _
                                 "FatherArbName," & _
                                 "FatherArbName4S," & _
                                 "GrandEngName," & _
                                 "GrandArbName," & _
                                 "GrandArbName4S," & _
                                 "E_Mail," & _
                                 "Position_ID," & _
                                 "MaritalStatus," & _
                                 "NODependant," & _
                                 "LastJob," & _
                                 "LastSalary," & _
                                 "ExpectedSalary," & _
                                 "Address," & _
                                 "HasDLicense," & _
                                 "Nationality_ID," & _
                                 "IqamaNo," & _
                                 "PassportNo," & _
                                 "HasTIqama," & _
                                 "NOFSponser," & _
                                 "HSpConditions," & _
                                 "SpecialConditions," & _
                                 "IsUsed," & _
                                 "Remarks," & _
                                 "RegUserID," & _
                                 "RegComputerID," & _
                                 "Mobile," & _
                                 "Phone" & _
                                 ")Values(" & _
                                 " @Code," & _
                                 " @EngName," & _
                                 " @ArbName," & _
                                 " @ArbName4S," & _
                                 " @FamilyEngName," & _
                                 " @FamilyArbName," & _
                                 " @FamilyArbName4S," & _
                                 " @FatherEngName," & _
                                 " @FatherArbName," & _
                                 " @FatherArbName4S," & _
                                 " @GrandEngName," & _
                                 " @GrandArbName," & _
                                 " @GrandArbName4S," & _
                                 " @E_Mail," & _
                                 " @Position_ID," & _
                                 " @MaritalStatus," & _
                                 " @NODependant," & _
                                 " @LastJob," & _
                                 " @LastSalary," & _
                                 " @ExpectedSalary," & _
                                 " @Address," & _
                                 " @HasDLicense," & _
                                 " @Nationality_ID," & _
                                 " @IqamaNo," & _
                                 " @PassportNo," & _
                                 " @HasTIqama," & _
                                 " @NOFSponser," & _
                                 " @HSpConditions," & _
                                 " @SpecialConditions," & _
                                 " @IsUsed," & _
                                 " @Remarks," & _
                                 " @RegUserID," & _
                                 " @RegComputerID," & _
                                 " @Mobile," & _
                                 " @Phone"
            End Select

            Dim cmd As New SqlClient.SqlCommand
            cmd.CommandText = SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.Connection = New SqlClient.SqlConnection(mConnString)

            cmd.Parameters.Add(New SqlClient.SqlParameter("@Code", SqlDbType.VarChar)).Value = txtCode.Text
            'cmd.Parameters.Add(New SqlClient.SqlParameter("@GDate", SqlDbType.DateTime)).Value = mDateTime
            'cmd.Parameters.Add(New SqlClient.SqlParameter("@HDate", SqlDbType.VarChar)).Value = String.Empty
            cmd.Parameters.Add(New SqlClient.SqlParameter("@EngName", SqlDbType.VarChar)).Value = txtEngName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@ArbName", SqlDbType.VarChar)).Value = txtArbName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@ArbName4S", SqlDbType.VarChar)).Value = txtArbName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@FamilyEngName", SqlDbType.VarChar)).Value = txtEngFamilyName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@FamilyArbName", SqlDbType.VarChar)).Value = txtArbFamilyName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@FamilyArbName4S", SqlDbType.VarChar)).Value = txtArbFamilyName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@FatherEngName", SqlDbType.VarChar)).Value = txtEngFathername.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@FatherArbName", SqlDbType.VarChar)).Value = txtArbFatherName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@FatherArbName4S", SqlDbType.VarChar)).Value = txtArbFatherName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@GrandEngName", SqlDbType.VarChar)).Value = txtEngGrandName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@GrandArbName", SqlDbType.VarChar)).Value = txtArbGrandName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@GrandArbName4S", SqlDbType.VarChar)).Value = txtArbGrandName.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@E_Mail", SqlDbType.VarChar)).Value = txtEMail.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@Position_ID", SqlDbType.Int)).Value = ddlPosition.SelectedValue
            cmd.Parameters.Add(New SqlClient.SqlParameter("@MaritalStatus", SqlDbType.VarChar)).Value = ddlMaritalStatus.SelectedValue
            cmd.Parameters.Add(New SqlClient.SqlParameter("@NODependant", SqlDbType.Int)).Value = txtNODependancies.Value
            cmd.Parameters.Add(New SqlClient.SqlParameter("@LastJob", SqlDbType.VarChar)).Value = txtLastJob.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@LastSalary", SqlDbType.Money)).Value = txtLastSalary.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@ExpectedSalary", SqlDbType.Money)).Value = txtExpSalary.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@Address", SqlDbType.VarChar)).Value = txtAddress.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@HasDLicense", SqlDbType.Bit)).Value = HasDriverLic.Checked
            cmd.Parameters.Add(New SqlClient.SqlParameter("@Nationality_ID", SqlDbType.Int)).Value = ddlNationality.SelectedValue
            cmd.Parameters.Add(New SqlClient.SqlParameter("@IqamaNo", SqlDbType.VarChar)).Value = txtIQamaNo.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@PassportNo", SqlDbType.VarChar)).Value = txtPassportNo.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@HasTIqama", SqlDbType.Bit)).Value = HasTransIqama.Checked
            cmd.Parameters.Add(New SqlClient.SqlParameter("@NOFSponser", SqlDbType.Bit)).Value = HasNOSponsor.Checked
            cmd.Parameters.Add(New SqlClient.SqlParameter("@HSpConditions", SqlDbType.Bit)).Value = HasSConditions.Checked
            cmd.Parameters.Add(New SqlClient.SqlParameter("@SpecialConditions", SqlDbType.VarChar)).Value = txtSConditions.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@IsUsed", SqlDbType.Bit)).Value = False
            cmd.Parameters.Add(New SqlClient.SqlParameter("@Remarks", SqlDbType.VarChar)).Value = String.Empty
            cmd.Parameters.Add(New SqlClient.SqlParameter("@Mobile", SqlDbType.VarChar)).Value = txtMobileNo.Text
            cmd.Parameters.Add(New SqlClient.SqlParameter("@Phone", SqlDbType.VarChar)).Value = txtPhoneNo.Text
            If SaveType = Global.frmRecCVOnline.SaveType.Insert Then
                'cmd.Parameters.Add(New SqlClient.SqlParameter("@RegUserID", SqlDbType.Int)).Value = Me.mDataBaseUserRelatedID, SqlDbType.Int, True)
                'cmd.Parameters.Add(New SqlClient.SqlParameter("@RegComputerID", SqlDbType.Int)).Value = mRegComputerID, SqlDbType.Int, True)
                'cmd.Parameters.Add(New SqlClient.SqlParameter("@CompanyID", SqlDbType.Int)).Value = Me.MainCompanyID, SqlDbType.Int, True)
            End If

            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()

            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Function GetValues(ByVal BioGraphies_Dataset As DataSet) As Boolean

        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim mBioGraphiesID As Int64
        Try

            With BioGraphies_Dataset.Tables(0).Rows(0)
                mBioGraphiesID = .Item("ID")
                txtCode.Text = .Item("Code").ToString
                txtEngName.Text = .Item("EngName").ToString
                txtArbName.Text = .Item("ArbName").ToString
                txtEngFamilyName.Text = .Item("FamilyEngName").ToString
                txtArbFamilyName.Text = .Item("FamilyArbName").ToString
                txtEngFathername.Text = .Item("FatherEngName").ToString
                txtArbFatherName.Text = .Item("FatherArbName").ToString
                txtEngGrandName.Text = .Item("GrandEngName").ToString
                txtArbGrandName.Text = .Item("GrandArbName").ToString
                txtEMail.Text = .Item("E_Mail").ToString
                ddlPosition.SelectedValue = .Item("Position_ID")
                ddlMaritalStatus.SelectedValue = .Item("MaritalStatus")
                txtNODependancies.Value = .Item("NODependant")
                txtLastJob.Text = .Item("LastJob").ToString
                txtLastSalary.Value = .Item("LastSalary")
                txtExpSalary.Value = .Item("ExpectedSalary")
                txtAddress.Text = .Item("Address").ToString
                HasDriverLic.Checked = .Item("HasDLicense")
                ddlNationality.SelectedValue = .Item("Nationality_ID")
                txtIQamaNo.Text = .Item("IqamaNo").ToString
                txtPassportNo.Text = .Item("PassportNo").ToString
                HasTransIqama.Checked = .Item("HasTIqama")
                HasNOSponsor.Checked = .Item("NOFSponser")
                HasSConditions.Checked = .Item("HSpConditions")
                txtSConditions.Text = IIf(IsDBNull(.Item("SpecialConditions")), "", .Item("SpecialConditions"))
                txtPhoneNo.Text = .Item("Phone").ToString
                txtMobileNo.Text = .Item("Mobile").ToString
            End With

            Dim mFindDataset As New DataSet
            mFindDataset = Find("Select * From Rec_BioGraphiesDetail1 Where CancelDate is null and BioGraphy_ID = " & mBioGraphiesID)
            uwgReferences.DataSource = mFindDataset.Tables(0).DefaultView
            uwgReferences.DataBind()

            mFindDataset = New DataSet
            mFindDataset = Find("Select * From Rec_BioGraphiesDetail2 Where CancelDate is null and BioGraphy_ID = " & mBioGraphiesID)
            uwgLanguage.DataSource = mFindDataset.Tables(0).DefaultView
            uwgLanguage.DataBind()

            mFindDataset = New DataSet
            mFindDataset = Find("Select * From Rec_BioGraphiesDetail3 Where CancelDate is null and BioGraphy_ID = " & mBioGraphiesID)
            uwgCertifications.DataSource = mFindDataset.Tables(0).DefaultView
            uwgCertifications.DataBind()

            mFindDataset = New DataSet
            mFindDataset = Find("Select * From Rec_BioGraphiesDetail4 Where CancelDate is null and BioGraphy_ID = " & mBioGraphiesID)
            uwgHistory.DataSource = mFindDataset.Tables(0).DefaultView
            uwgHistory.DataBind()

            If (mBioGraphiesID > 0) Then
                StrMode = "E"
            Else
                StrMode = "N"
            End If

            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Function SavePart() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim BioGraphiesID As Int64
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnString)

        Try
            Dim mFindDataSet As New DataSet
            mFindDataSet = Find("Select * From Rec_BioGraphies Where CancelDate is null and Code='" & txtCode.Text & "'")

            BioGraphiesID = mFindDataSet.Tables(0).Rows(0).Item("ID")

            If BioGraphiesID > 0 Then
                If Not AssignValue(mFindDataSet, SaveType.Update) Then
                    Exit Function
                End If

                Dim str As String = "delete from Rec_BioGraphiesDetail1 where BioGraphy_ID = " & BioGraphiesID & " ;" & _
                                    "delete from Rec_BioGraphiesDetail2 where BioGraphy_ID = " & BioGraphiesID & " ;" & _
                                    "delete from Rec_BioGraphiesDetail3 where BioGraphy_ID = " & BioGraphiesID & " ;" & _
                                    "delete from Rec_BioGraphiesDetail4 where BioGraphy_ID = " & BioGraphiesID

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(mConnString, CommandType.Text, str)

            Else
                If Not AssignValue(mFindDataSet, SaveType.Insert) Then
                    Exit Function
                End If

                mFindDataSet = New DataSet
                mFindDataSet = Find("Select * From Rec_BioGraphies Where CancelDate is null and Code='" & txtCode.Text & "'")
                BioGraphiesID = mFindDataSet.Tables(0).Rows(0).Item("ID")
            End If

            If (SaveDG(BioGraphiesID)) Then
                'clsMainOtherFields = New clsSys_MainOtherFields(Me.Page)
                'clsMainOtherFields.CollectDataAndSave(value.Text, ClsBioGraphies.Table, ClsBioGraphies.ID)
                mFindDataSet = New DataSet
                mFindDataSet = Find("Select * From Rec_BioGraphies Where CancelDate is null and Code='" & txtCode.Text & "'")
                value.Text = ""
                AfterOperation()
                GetValues(mFindDataSet)

                Venus.Shared.Web.ClientSideActions.MsgBoxBasic(Page, ObjNavigationHandler.SetLanguage(Page, "Save Dane/تم الحفظ"))
            End If

        Catch ex As Exception

        End Try
    End Function

    Private Function SaveDG(ByVal BiographyID As Integer) As Boolean
        Dim DGRow As Infragistics.WebUI.UltraWebGrid.UltraGridRow
        Dim SqlCommand As String = String.Empty
        Try
            For Each DGRow In uwgReferences.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If

                SqlCommand &= " Set DateFormat DMY Insert Into Rec_BioGraphiesDetail1 " & _
                              "(BioGraphy_ID, EngName, ArbName, Phone, Fax, E_Mail)Values(" & _
                              BiographyID & ", " & _
                              "'" & DGRow.Cells(1).Value & "', " & _
                              "'" & DGRow.Cells(2).Value & "', " & _
                              "'" & DGRow.Cells(3).Value & "', " & _
                              "'" & DGRow.Cells(4).Value & "', " & _
                              "'" & DGRow.Cells(5).Value & "') ; " & vbNewLine

            Next

            For Each DGRow In uwgLanguage.Rows
                If IsNothing(DGRow.Cells(1).Value) Or DGRow.Cells(1).Value = 0 Then
                    Continue For
                End If

                SqlCommand &= " Set DateFormat DMY Insert Into Rec_BioGraphiesDetail2 " & _
                              "(BioGraphy_ID, Language_ID, SLevel_ID, WLevel_ID)Values(" & _
                              BiographyID & ", " & _
                              DGRow.Cells(1).Value & ", " & _
                              DGRow.Cells(2).Value & ", " & _
                              DGRow.Cells(3).Value & ") ;" & vbNewLine

            Next

            For Each DGRow In uwgCertifications.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If

                SqlCommand &= " Set DateFormat DMY Insert Into Rec_BioGraphiesDetail3 " & _
                              "(BioGraphy_ID, EngName, ArbName, GDateFrom, HDateFrom, GDateTo, HDateTo)Values(" & _
                              BiographyID & ", " & _
                              "'" & DGRow.Cells(1).Value & "', " & _
                              "'" & DGRow.Cells(2).Value & "', " & _
                              "'" & DGRow.Cells(3).Value & "', " & _
                              "'" & DGRow.Cells(4).Value & "', " & _
                              "'" & DGRow.Cells(5).Value & "', " & _
                              "'" & DGRow.Cells(6).Value & "') ;" & vbNewLine

            Next

            For Each DGRow In uwgHistory.Rows
                If IsNothing(DGRow.Cells(1).Value) And IsNothing(DGRow.Cells(2).Value) Then
                    Continue For
                End If

                SqlCommand &= " Set DateFormat DMY Insert Into Rec_BioGraphiesDetail4 " & _
                              "(BioGraphy_ID, EngName, ArbName, GDateFrom, HDateFrom, GDateTo, HDateTo, Years, Position_ID)Values(" & _
                              BiographyID & ", " & _
                              "'" & DGRow.Cells(1).Value & "', " & _
                              "'" & DGRow.Cells(2).Value & "', " & _
                              "'" & DGRow.Cells(3).Value & "', " & _
                              "'" & DGRow.Cells(4).Value & "', " & _
                              "'" & DGRow.Cells(5).Value & "', " & _
                              "'" & DGRow.Cells(6).Value & "', " & _
                              IIf(DGRow.Cells(7).Value = "", 0, DGRow.Cells(7).Value) & ", " & _
                              IIf(DGRow.Cells(8).Value = "", 0, DGRow.Cells(8).Value) & ") ;" & vbNewLine

            Next

            Dim cmd As New SqlClient.SqlCommand
            cmd.CommandText = SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.Connection = New SqlClient.SqlConnection(mConnString)
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function SetReturnback() As Boolean
        Dim StrMode As String = Request.QueryString.Item("Mode")
        Dim StrTargetControl As String = Request.QueryString.Item("TargetControl")
        Select Case StrMode
            Case "R"
                Venus.Shared.Web.ClientSideActions.DoReturnBack(Page, StrTargetControl)
            Case Else
                Venus.Shared.Web.ClientSideActions.DoRefresh(Page)
        End Select
    End Function

    Private Function CheckCode() As Boolean
        Dim StrMode As String
        Dim IntId As Integer = Request.QueryString.Item("ID")
        Try
            Dim mFindDataSet As New DataSet

            mFindDataSet = Find("Select * From Rec_BioGraphies Where CancelDate is null and Code='" & txtCode.Text & "'")

            If mFindDataSet.Tables(0).Rows.Count > 0 Then
                IntId = mFindDataSet.Tables(0).Rows(0).Item("ID")
                GetValues(mFindDataSet)
                StrMode = "E"
                txtCode.Enabled = False
                btnSave.Enabled = False

                uwgCertifications.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgHistory.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgLanguage.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No
                uwgReferences.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No

                txtCode.Focus()
            Else
                Clear()
                StrMode = "N"
                txtEngName.Focus()
            End If

        Catch ex As Exception

        End Try
    End Function

    Private Function AfterOperation() As Boolean
        Clear()

        ClientScript.RegisterClientScriptBlock(ClientScript.GetType, "Load", "<script language=""javascript"">DoRefreshBack();</script>")
    End Function

    Private Function Clear() As Boolean
        txtEngName.Text = String.Empty
        txtArbName.Text = String.Empty
        txtEngFamilyName.Text = String.Empty
        txtArbFamilyName.Text = String.Empty
        txtEngFathername.Text = String.Empty
        txtArbFatherName.Text = String.Empty
        txtEngGrandName.Text = String.Empty
        txtArbGrandName.Text = String.Empty
        txtEMail.Text = String.Empty
        ddlPosition.SelectedValue = 0
        ddlMaritalStatus.SelectedValue = 0
        txtNODependancies.Text = 0
        txtLastJob.Text = String.Empty
        txtLastSalary.Text = 0
        txtExpSalary.Text = 0
        txtAddress.Text = String.Empty
        HasDriverLic.Checked = False
        ddlNationality.SelectedValue = 0
        txtIQamaNo.Text = String.Empty
        txtPassportNo.Text = String.Empty
        HasTransIqama.Checked = False
        HasNOSponsor.Checked = False
        HasSConditions.Checked = False
        txtSConditions.Text = String.Empty
        uwgReferences.Rows.Clear()
        uwgHistory.Rows.Clear()
        uwgLanguage.Rows.Clear()
        uwgCertifications.Rows.Clear()
    End Function

    Private Function Find(ByVal Filter As String) As DataSet
        Dim mSqlDataAdapter As Data.SqlClient.SqlDataAdapter

        Try
            Dim mDataset As New DataSet
            mSqlDataAdapter = New SqlClient.SqlDataAdapter(Filter, mConnString)
            mDataset = New DataSet
            mSqlDataAdapter.Fill(mDataset)

            Return mDataset
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function GregToHijri(ByVal Greg As String, ByVal format As String) As String
        Dim allFormats() As String = {"yyyy/MM/dd", _
                                      "yyyy/M/d", _
                                      "dd/MM/yyyy", _
                                      "d/M/yyyy", _
                                      "dd/M/yyyy", _
                                      "d/MM/yyyy", _
                                      "yyyy-MM-dd", _
                                      "yyyy-M-d", _
                                      "dd-MM-yyyy", _
                                      "d-M-yyyy", _
                                      "dd-M-yyyy", _
                                      "d-MM-yyyy", _
                                      "yyyy MM dd", _
                                      "yyyy M d", _
                                      "dd MM yyyy", _
                                      "d M yyyy", _
                                      "dd M yyyy", _
                                      "d MM yyyy"}

        Dim arCul As CultureInfo
        Dim enCul As CultureInfo
        Dim h As HijriCalendar
        Dim g As GregorianCalendar
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New HijriCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

        enCul.DateTimeFormat.Calendar = g

        If Greg.Length > 0 Then
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(Greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.ToString(format, arCul.DateTimeFormat)
            Catch ex As Exception
                Return Nothing
            End Try
        End If
    End Function

    Private Function HijriToGreg(ByVal hijri As String, ByVal format As String) As String
        Dim allFormats() As String = {"yyyy/MM/dd", _
                                      "yyyy/M/d", _
                                      "dd/MM/yyyy", _
                                      "d/M/yyyy", _
                                      "dd/M/yyyy", _
                                      "d/MM/yyyy", _
                                      "yyyy-MM-dd", _
                                      "yyyy-M-d", _
                                      "dd-MM-yyyy", _
                                      "d-M-yyyy", _
                                      "dd-M-yyyy", _
                                      "d-MM-yyyy", _
                                      "yyyy MM dd", _
                                      "yyyy M d", _
                                      "dd MM yyyy", _
                                      "d M yyyy", _
                                      "dd M yyyy", _
                                      "d MM yyyy"}

        Dim arCul As CultureInfo
        Dim enCul As CultureInfo
        Dim h As HijriCalendar
        Dim g As GregorianCalendar
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New HijriCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)

        arCul.DateTimeFormat.Calendar = h

        If hijri.Length > 0 Then
            Try
                Dim tempDate As DateTime = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
                Return tempDate.ToString(format, enCul.DateTimeFormat)
            Catch ex As Exception
                Return Nothing
            End Try
        End If
    End Function

    Private Function GetDropDownList(ByVal DdlValues As Global.System.Web.UI.WebControls.DropDownList, ByVal NullNode As Boolean, ByVal Filter As String) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrSelectCommand As String
        Dim ObjDataset As New DataSet
        Dim Item As Global.System.Web.UI.WebControls.ListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnString)
        Try
            StrSelectCommand = Filter
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnString, CommandType.Text, StrSelectCommand)
            DdlValues.Items.Clear()

            If NullNode Then
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjNavigationHandler.SetLanguage(Page, "[Select Your Choice]/[ إختر أحد الإختيارات ]")

                Item.Value = 0
                DdlValues.Items.Add(Item)
            End If

            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Global.System.Web.UI.WebControls.ListItem
                Item.Text = ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName"))
                If (Item.Text.Trim = "") Then
                    Item.Text = ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "ArbName/EngName"))
                End If
                Item.Value = ObjDataRow("ID")
                DdlValues.Items.Add(Item)
            Next

            If DdlValues.Items.Count > 0 Then
                Return True
            End If

        Catch ex As Exception

        Finally
            ObjDataset.Dispose()
        End Try
    End Function

    Public Function GetList(ByRef DdlValues As Infragistics.WebUI.UltraWebGrid.ValueList, ByVal NullNode As Boolean, ByVal Filter As String) As Boolean
        Dim ObjDataRow As DataRow
        Dim StrCommandString As String
        Dim ObjDataset As New DataSet
        Dim Item As Infragistics.WebUI.UltraWebGrid.ValueListItem
        Dim ObjNavigationHandler As New Venus.Shared.Web.NavigationHandler(mConnString)
        Try
            StrCommandString = Filter
            ObjDataset = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(mConnString, CommandType.Text, StrCommandString)
            DdlValues.ValueListItems.Clear()
            If NullNode Then
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem()
                Item.DisplayText = ObjNavigationHandler.SetLanguage(Page, "[Select Your Choice]/ [ إختر أحد الإختيارات ] ")
                Item.DataValue = 0
                DdlValues.ValueListItems.Add(Item)
            End If
            For Each ObjDataRow In ObjDataset.Tables(0).Rows
                Item = New Infragistics.WebUI.UltraWebGrid.ValueListItem
                Item.DisplayText = ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "EngName/ArbName"))
                If (Item.DisplayText.Trim = "") Then
                    Item.DisplayText = ObjDataRow(ObjNavigationHandler.SetLanguage(Page, "ArbName/EngName"))
                End If
                Item.DataValue = ObjDataRow("ID")
                DdlValues.ValueListItems.Add(Item)
            Next
            If DdlValues.ValueListItems.Count > 0 Then
                Return True
            End If
        Catch ex As Exception

        Finally
            ObjDataset.Dispose()
        End Try
    End Function

#End Region

#Region "PageMethods"
    <System.Web.Services.WebMethod()> _
    Public Shared Function Greg2Hijri(ByVal DateValue As String) As Object
        Dim GDate As String = DateValue
        If ClsDataAcessLayer.IsGreg(GDate) = False Then
            GDate = ClsDataAcessLayer.FormatGregString(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        End If
        Return GDate & "|" & ClsDataAcessLayer.GregToHijri(Convert.ToDateTime(GDate).ToString("dd/MM/yyyy"), "dd/MM/yyyy")
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function Hijri2Greg(ByVal DateValue As String) As Object
        Dim HDate As String = DateValue
        If ClsDataAcessLayer.IsHijri(HDate) = False Then
            HDate = ClsDataAcessLayer.FormatHijri(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy")
        End If
        Return ClsDataAcessLayer.HijriToGreg(HDate, "dd/MM/yyyy") & "|" & HDate
    End Function

#End Region



End Class
