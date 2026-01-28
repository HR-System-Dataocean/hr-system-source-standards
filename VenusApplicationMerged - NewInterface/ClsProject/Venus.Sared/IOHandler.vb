Imports System.IO
Public Class FileHandler

#Region "Public Constructor"

    '*************************************************************
    'NAME:          new  
    'PURPOSE:       the main constractor of the class 
    '*************************************************************

    Public Sub New()
        Try

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Public Decliration"


    '*************************************************************
    'NAME:          PD_ObjErrorHandler 
    'PURPOSE:       to handle all the errors of the module 
    '*************************************************************

    Private PD_ObjErrorHandler As New Venus.Shared.ErrorsHandler

#End Region

#Region "Public Functions"

    '*************************************************************
    'NAME:          Create_File 
    'PURPOSE:       Create file on the local hard disk 
    'PARAMETERS:    Path 
    '               create file on the local hard disk
    'RETURNS:       indication whither the file is created or not 
    '*************************************************************

    Public Function Create_File(ByVal FilePath As String) As Boolean
        Try
            File.Create(FilePath)
            Return True
        Catch ex As Exception
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    '*************************************************************
    'NAME:          Delete_File
    'PURPOSE:       Delete file from the local hard disk 
    'PARAMETERS:    Path 
    '               Delete file from the local hard disk 
    'RETURNS:       indication whither the file is created or not 
    '*************************************************************

    Public Function Delete_File(ByVal FilePath As String) As Boolean
        Try
            File.Delete(FilePath)
            Return True
        Catch ex As Exception
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    '*************************************************************
    'NAME:          Exist_File 
    'PURPOSE:       Check wither the file is exist or not 
    'PARAMETERS:    Path 
    '               check wither the file is exist or not 
    'RETURNS:       indicate if the operation is done successfully or not 
    '*************************************************************

    Public Function Exist_File(ByVal FilePath As String) As Boolean
        Try
            File.Exists(FilePath)
            Return True
        Catch ex As Exception
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    '*************************************************************
    'NAME:          Create Directory 
    'PURPOSE:       To create any directory on the local hard disk 
    'PARAMETERS:    Path 
    '               To create any directory on the local hard disk
    'RETURNS:       indicate if the operation is done successfully or not 
    '*************************************************************

    Public Function Create_Directory(ByVal DirectoryPath As String) As Boolean
        Try
            Directory.CreateDirectory(DirectoryPath)
            Return True
        Catch ex As Exception
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    '*************************************************************
    'NAME:          Delete directory  
    'PURPOSE:       To delete any directory from the local hard disk 
    'PARAMETERS:    Path 
    '               To delete any directory from the local hard disk 
    'RETURNS:       indicate if the operation is done successfully or not 
    '*************************************************************

    Public Function Delete_Directory(ByVal DirectoryPath As String) As Boolean
        Try
            Directory.Delete(DirectoryPath)
            Return True
        Catch ex As Exception
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

    '*************************************************************
    'NAME:          Exist directory  
    'PURPOSE:       To indicate whither the directory is exist or not 
    'PARAMETERS:    Path 
    '               To indicate whither the directory is exist or not 
    'RETURNS:       indicate if the operation is done successfully or not 
    '*************************************************************

    Public Function Exist_Directory(ByVal DirectoryPath As String) As Boolean
        Try
            Directory.Exists(DirectoryPath)
            Return True
        Catch ex As Exception
            PD_ObjErrorHandler.RecordExceptions_DataBase("", ex, Err.Number, 0, ErrorsHandler.eRecordingType.System_ErrorsLog)
            Return False
        End Try
    End Function

#End Region

#Region "Public Destructor"

    '*************************************************************
    'NAME:          finalize 
    'PURPOSE:       the main destructior of the class 
    '*************************************************************

    Protected Overrides Sub finalize()
        PD_ObjErrorHandler = Nothing
    End Sub

#End Region

End Class


Public Class DevicesHandler

#Region "Public Constructor"

#End Region

#Region "Public Decliration"

#End Region

#Region "Public Functions"

#End Region

#Region "Public Destructor"

#End Region

End Class


