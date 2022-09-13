'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="FolderInfo"/> gets (async) and stores info such as filesize and lastwritetime of all files in the folder and its subfolders.</para>
''' </summary>
Public Class FolderInfo
    ''' <summary>
    ''' Get the number of files in the folder and its subfolders. 
    ''' </summary>
    ''' <returns>The number of files.</returns>
    Public ReadOnly Property Count As Integer
    ''' <summary>
    ''' Get the total filesize of files in the folder and its subfolders. 
    ''' </summary>
    ''' <returns>The total filesize.</returns>
    Public ReadOnly Property Size As Long = 0

    ''' <summary>
    ''' The file info of all files stored in a dictionary.
    ''' <para>The keys are the partial filenames.</para>
    ''' </summary>
    Private ReadOnly _folderInfo As New Dictionary(Of String, FileModel)

    'class

    ''' <summary>
    ''' Initializes a new instance of <see cref="FolderInfo"/> for the specified folder.
    ''' <para><see cref="FolderInfo"/> gets (async) and stores info such as filesize and lastwritetime of all files in the folder and its subfolders.</para>
    ''' </summary>
    ''' <param name="place">Place in the suitcase, <see cref="Stock.Source"/> or <see cref="Stock.Target"/>.</param>
    ''' <param name="folder">The folderpath.</param>
    ''' <param name="progressFiles">The progress provider that tracks the progress of reading files.</param>
    Public Sub New(place As Stock, folder As String, progressFiles As IProgress(Of ProgressFilesEventArgs))
        _folderInfo = New Dictionary(Of String, FileModel)
        Dim fileNames = IO.Directory.GetFiles(folder, "*.*", IO.SearchOption.AllDirectories)
        _Count = fileNames.Count
        Dim length = folder.Length + 1
        Dim lock = New Object
        Parallel.ForEach(fileNames, Sub(fileName)
                                        Dim fileModel = ReadFileInfo(fileName)
                                        SyncLock lock
                                            _folderInfo.Add(fileName.EraseStart(length), fileModel)
                                            _Size += fileModel.Size
                                        End SyncLock
                                        Dim eventArgs = New ProgressFilesEventArgs(place, _Count, _folderInfo.Count)
                                        progressFiles.Report(eventArgs)
                                    End Sub)
    End Sub

    'functions

    ''' <summary>
    ''' Gets the readable folder size (eg. in MB or GB instead of bytes).
    ''' </summary>
    ''' <param name="decimals">Decimals to use.</param>
    ''' <returns>Readable folder size.</returns>
    Public Function SizeString(Optional decimals As Integer = 0) As String
        Return FileSystemHelper.ReadableFileSystemSize(Size, decimals)
    End Function

    ''' <summary>
    ''' Gets the list of partial filenames of the folder.
    ''' </summary>
    ''' <returns>The list of partial filenames.</returns>
    Public Function GetFileList() As String()
        Return _folderInfo.Keys.ToArray
    End Function

    ''' <summary>
    ''' Gets the info about the file such as filesize and lastwritetime.
    ''' </summary>
    ''' <param name="fileName">The partial filename.</param>
    ''' <returns>The file info.</returns>
    Public Function GetInfo(fileName As String) As FileModel
        If Not _folderInfo.ContainsKey(fileName) Then Return Nothing

        Return _folderInfo(fileName)
    End Function

    'private functions

    ''' <summary>
    ''' Reads the info about the file such as filesize and lastwritetime.
    ''' </summary>
    ''' <param name="fileName">The fullname of the file.</param>
    ''' <returns>The file info.</returns>
    Private Function ReadFileInfo(fileName As String) As FileModel
        Dim fileTime = IO.File.GetLastWriteTime(fileName)
        Dim fileSize = New IO.FileInfo(fileName).Length
        Return New FileModel(fileName, fileTime, fileSize)
    End Function

End Class
