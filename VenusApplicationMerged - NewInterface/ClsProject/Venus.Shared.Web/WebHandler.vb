Imports System.Web

Public Class WebHandler

#Region "Public Declirations"

    Const MAIN_IMAGE = "Images/logo.bmp" '
    Const PROGRESS_BAR_SIZE = 10 'number of steps in your progress bar
    Const PROGRESS_BAR_STEP = "Images/pro.bmp" 'image for idle steps
    Const PROGRESS_BAR_ACTIVE_STEP = "Images/pro2.bmp" 'image for active step 

#End Region

#Region "Public Constructor"

    Public Sub New()

    End Sub

#End Region

#Region " Public Function"

    Public Function Send_Wait_NEW(ByVal page As System.Web.UI.Page, ByVal msg As String) As Boolean

        page.Response.Write("<div id='mydiv' align='center' style='font-size: 16pt; text-decoration: blink; color: #666666; position: absolute; top: 0px; width: 100%;'>")

        page.Response.Write("<br /><br />" & msg & "<br />")

        page.Response.Write("<img src='" & "../../Pages/HR/Img/waiting.gif" & "'>")

        page.Response.Write("</div>")

        page.Response.Write("<script language=javascript>")

        page.Response.Write("var mydiv= window.document.all.item(""mydiv"");")

        page.Response.Write("function Start_Wait(){mydiv.style.visibility = ""visible"";}")

        page.Response.Write("function Stop_Wait(){ mydiv.style.visibility = ""hidden"";}")

        page.Response.Write("Start_Wait();</script>")

        page.Response.Flush()

    End Function


    Public Function Send_Wait_Info(ByVal page As System.Web.UI.Page) As Boolean

        Dim IntCounter As Integer

        page.Response.Write("<div id='mydiv' align='center'>")

        page.Response.Write("<img src='" & MAIN_IMAGE & "'>")

        page.Response.Write("<div id='mydiv2' align='center'>")

        For IntCounter = 0 To PROGRESS_BAR_SIZE

            page.Response.Write("<img id='pro" & IntCounter.ToString() & "' src='" & PROGRESS_BAR_STEP & "'>&nbsp;")

        Next

        page.Response.Write("</div>")

        page.Response.Write("</div>")

        page.Response.Write("<script language=javascript>")

        page.Response.Write("var counter=0;var countermax = " & PROGRESS_BAR_SIZE & ";function ShowWait()")

        page.Response.Write("{ document.getElementById('pro' + counter).setAttribute('src','" & PROGRESS_BAR_ACTIVE_STEP & "'); if (counter == 0) document.getElementById('pro' + countermax).setAttribute('src','" & PROGRESS_BAR_STEP & "');else {var x=counter - 1; document.getElementById('pro' + x).setAttribute('src','" + PROGRESS_BAR_STEP + "');} counter++;if (counter > countermax) counter=0;}")

        page.Response.Write("function Start_Wait(){mydiv.style.visibility = ""visible"";window.setInterval(""ShowWait()"",100);}")

        page.Response.Write("function Stop_Wait(){ mydiv.style.visibility = ""hidden"";window.clearInterval();}")

        page.Response.Write("Start_Wait();</script>")

        page.Response.Flush()

    End Function

    Public Function SetCookies(ByRef Page As System.Web.UI.Page, ByVal Name As String, ByVal Value As String, Optional ByVal Permanent As Boolean = False) As Boolean
        Dim ObjCookies As New System.Web.HttpCookie(Name, Value)
        Try
            If Permanent Then
                ObjCookies.Expires = DateAdd(DateInterval.Day, 30, Date.Now)
            End If
            Page.Response.Cookies.Add(ObjCookies)
            Page.Response.SetCookie(ObjCookies)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetCookies(ByRef Page As System.Web.UI.Page, ByVal Name As String, ByRef Value As String) As Boolean
        Try
            If Page.Request.Cookies(Name).Value.ToString() = Nothing Then
                Value = 0
            Else
                Value = Page.Request.Cookies(Name).Value
            End If


            If Value = Nothing Then
                Value = 0
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveFile(ByVal ApplicationPath As String, ByVal PostedFile As System.Web.HttpPostedFile, ByVal DefultFolderName As String, ByVal DefultSubFolder As String, ByVal DocumentForlder As String, ByVal ImageName As String, ByRef ErrorMsg As String) As Boolean
        Try

            Dim File As System.Web.HttpPostedFile
            Dim FileName As String


            'Get the Posted File 
            File = PostedFile

            'Prepare File Name

            If Not IO.Directory.Exists(ApplicationPath & DefultFolderName) Then
                IO.Directory.CreateDirectory(ApplicationPath & DefultFolderName)
            End If

            If Not IO.Directory.Exists(ApplicationPath & DefultFolderName & "\" & DefultSubFolder) Then
                IO.Directory.CreateDirectory(ApplicationPath & DefultFolderName & "\" & DefultSubFolder)
            End If

            If Not IO.Directory.Exists(ApplicationPath & DefultFolderName & "\" & DefultSubFolder & "\" & DocumentForlder) Then
                IO.Directory.CreateDirectory(ApplicationPath & DefultFolderName & "\" & DefultSubFolder & "\" & DocumentForlder)
            End If

            'File Saving
            FileName = ImageName
            File.SaveAs(ApplicationPath & DefultFolderName & "\" & DefultSubFolder & "\" & DocumentForlder & "\" & FileName)

            Return True
        Catch ex As Exception
            ErrorMsg = "The Error is occer in saveing file : " & ex.Message & vbNewLine
            Return False
        End Try

    End Function

    'Public Function SaveFile(ByVal PostedFile As System.Web.HttpPostedFile, ByVal Path As String) As Boolean
    '    Try

    '        Dim File As System.Web.HttpPostedFile
    '        Dim FileName As String
    '        Dim StrPathArr() As String
    '        Dim IntCounter As Integer
    '        Dim CurrentPath As String
    '        File = PostedFile
    '        StrPathArr = Path.Split("\")

    '        For IntCounter = 1 To StrPathArr.GetUpperBound(0) - 1
    '            CurrentPath &= "\" & StrPathArr(IntCounter)
    '            If Not IO.Directory.Exists(CurrentPath) Then
    '                IO.Directory.CreateDirectory(CurrentPath)
    '            End If
    '        Next

    '        'Prepare File Name

    '        'File Saving
    '        FileName = ImageName
    '        File.SaveAs(ApplicationPath & DefultFolderName & "\" & DefultSubFolder & "\" & FileName)

    '        Return True
    '    Catch ex As Exception
    '        ErrorMsg = "The Error is occer in saveing file : " & ex.Message & vbNewLine
    '        Return False
    '    End Try

    'End Function

    Public Function Add2History(ByVal ConnectionStr As String, ByVal RecordID As Long, ByVal TableName As String, ByVal orgSQL As String, Optional ByVal SQLWhere As String = "", Optional ByVal SQLWhereNew As String = "", Optional ByVal UserID As Integer = 0, Optional ByVal sqlCommand As SqlClient.SqlCommand = Nothing, Optional ByVal ServerName As String = "") As Boolean
        Try

            Dim ObjDSOLD As New DataSet
            Dim ObjDSNEW As New DataSet
            Dim strSQL As String = String.Empty
            Dim intCounterInner As Integer
            Dim IntCounterOuter As Integer
            Dim StrSqlNewData As String = String.Empty
            Dim ObjinnerDS As DataSet

            If Not sqlCommand Is Nothing Then

                ObjDSOLD = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionStr, CommandType.Text, "Select * From " & TableName & " Where ID = " & RecordID)

                'Execute the forwarded SQL string

                sqlCommand.Connection.Open()
                sqlCommand.ExecuteNonQuery()
                sqlCommand.Connection.Close()

                'Read the new data from the DB after executing the SQL string using the where statement

                ObjDSNEW = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionStr, CommandType.Text, "Select * From " & TableName & " Where ID = " & RecordID)

            ElseIf RecordID Then

                'Read the existing data from the DB before update using the record id

                ObjDSOLD = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionStr, CommandType.Text, "Select * From " & TableName & " Where ID = " & RecordID)

                'Execute the forwarded SQL string

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionStr, CommandType.Text, orgSQL)

                'Read the new data from the DB after executing the SQL string using the where statement

                ObjDSNEW = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionStr, CommandType.Text, "Select * From " & TableName & " Where ID = " & RecordID)


            ElseIf Len(SQLWhere) Then   'If SQL string has   no ID but has where statement

                'Read the existing data from the DB before update using the where statement

                ObjDSOLD = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionStr, CommandType.Text, "Select * From " & TableName & SQLWhere)

                'Execute the forwarded SQL string along with the where statement

                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionStr, CommandType.Text, orgSQL & SQLWhere)

                'Read the new data from the DB after executing the SQL string using the where statement

                ObjDSNEW = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionStr, CommandType.Text, "Select * From " & TableName & IIf(Len(SQLWhereNew), SQLWhereNew, SQLWhere))

            Else  'If the table has one record only 'Settings'

                'Read the existing data from the DB before update

                ObjDSOLD = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionStr, CommandType.Text, "Select * From " & TableName)

                'Execute the forwarded SQL string

                ObjinnerDS = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionStr, CommandType.Text, orgSQL)

                'Read the new data from the DB after executing the SQL string

                ObjDSNEW = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionStr, CommandType.Text, "Select * From " & TableName)

            End If

            If Not ObjinnerDS Is Nothing AndAlso ObjinnerDS.Tables.Count > 0 AndAlso ObjinnerDS.Tables(0).Rows.Count > 0 Then
                If RecordID = 0 Then
                    RecordID = ObjinnerDS.Tables(0).Rows(0)(0)
                End If
            End If

            'Scan both record sets old and new for any changes


            For IntCounterOuter = 0 To ObjDSOLD.Tables(0).Rows.Count - 1
                For intCounterInner = 0 To ObjDSOLD.Tables(0).Columns.Count - 1
                    With ObjDSOLD.Tables(0).Rows(IntCounterOuter)

                        If FixNull(.Item(intCounterInner), ObjDSOLD.Tables(0).Columns(intCounterInner)) <> FixNull(ObjDSNEW.Tables(0).Rows(IntCounterOuter).Item(intCounterInner), ObjDSOLD.Tables(0).Columns(intCounterInner)) Then
                            If ObjDSOLD.Tables(0).Columns(intCounterInner).ColumnName.ToUpper <> "ID" And ObjDSOLD.Tables(0).Columns(intCounterInner).ColumnName.ToUpper <> "REGDATE" Then

                                strSQL = strSQL & " Insert Into sys_History (FieldID, RecordID, OldValue, RegUserID)Select " _
                                                                & "  FieldID    = " & GetFieldID(TableName, ObjDSOLD.Tables(0).Columns(intCounterInner).ColumnName, ConnectionStr) _
                                                                & ", RecordID   = " & RecordID _
                                                                & ", OldValue   = '" & Replace(FixNull(.Item(intCounterInner), ObjDSOLD.Tables(0).Columns(intCounterInner)), "'", "''") _
                                                                & "',RegUserID  = " & UserID & ";" & vbNewLine


                            End If
                        End If
                    End With
                Next
            Next


            If Len(strSQL) Then Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionStr, CommandType.Text, strSQL)
            If Len(StrSqlNewData) Then Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionStr, CommandType.Text, StrSqlNewData)


        Catch ex As Exception

        End Try

    End Function

    Private Function FixNull(ByVal obj As Object, ByVal DataColumn As System.Data.DataColumn) As Object
        Try

            Select Case DataColumn.DataType.Name.ToUpper
                Case "DECIMAL", "DOUBLE"
                    If obj Is DBNull.Value Then
                        Return 0.0
                    Else
                        Return obj
                    End If
                Case "INT32"
                    If obj Is DBNull.Value Then
                        Return 0
                    Else
                        Return obj
                    End If
                Case "DATETIME"
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return CStr(obj)
                    End If
                Case "STRING"
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
                Case Else
                    If obj Is DBNull.Value Then
                        Return ""
                    Else
                        Return obj
                    End If
            End Select

        Catch ex As Exception

        End Try
    End Function

    Private Function GetFieldID(ByVal TableName As String, ByVal FieldName As String, ByVal connectionStr As String) As Integer
        Dim StrSqlstring As String = String.Empty
        Dim IntFieldID As Integer
        Try
            StrSqlstring = "Select FieldID From hrs_vwObjectsFields Where ObjectCode='" & Replace(TableName, " ", "") & "' And FieldName='" & FieldName & "' And ObjectCancelDate is Null"
            IntFieldID = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(connectionStr, CommandType.Text, StrSqlstring)
            Return IntFieldID
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region

#Region "Public Destructor"



#End Region





End Class
