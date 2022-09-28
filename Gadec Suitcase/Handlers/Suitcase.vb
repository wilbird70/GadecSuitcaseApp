'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para>A <see cref="Suitcase"/> has stored data about the previous contents of the synced folders (filesnames and lastwritetimes) and can read (async) the actual contents of the source- and targetfolders.</para>
''' <para>It also has methods to:</para>
''' <para>- show the actions for synchronization.</para>
''' <para>- perform the synchronization actions.</para>
''' <para>- rename this <see cref="Suitcase"/>.</para>
''' <para>- delete this <see cref="Suitcase"/> from the <see cref="Storage"/> it is part of.</para>
''' </summary>
Public Class Suitcase
    ''' <summary>
    ''' Gets the path of the source-folder.
    ''' </summary>
    ''' <returns>The path.</returns>
    Public ReadOnly Property SourceFolder As String
    ''' <summary>
    ''' Gets the path of the target-folder.
    ''' </summary>
    ''' <returns>The path.</returns>
    Public ReadOnly Property TargetFolder As String
    ''' <summary>
    ''' Determines whether the <see cref="Suitcase"/> can be synced.
    ''' </summary>
    ''' <returns>True if both folders are available.</returns>
    Public ReadOnly Property Synchronizable As Boolean
    ''' <summary>
    ''' Gets the name of the <see cref="Suitcase"/>.
    ''' </summary>
    ''' <returns>The name.</returns>
    Public ReadOnly Property Name As String
    ''' <summary>
    ''' Gets the <see cref="FolderInfo"/> of the source-folder.
    ''' </summary>
    ''' <returns>The <see cref="FolderInfo"/>.</returns>
    Public ReadOnly Property SourceInfo As FolderInfo
    ''' <summary>
    ''' Gets the <see cref="FolderInfo"/> of the target-folder.
    ''' </summary>
    ''' <returns>The <see cref="FolderInfo"/>.</returns>
    Public ReadOnly Property TargetInfo As FolderInfo
    ''' <summary>
    ''' Gets the <see cref="ActionInfo"/> with preferred actions for the <see cref="Suitcase"/>.
    ''' </summary>
    ''' <returns>The <see cref="ActionInfo"/>.</returns>
    Public ReadOnly Property ActionData As ActionInfo

    ''' <summary>
    ''' The <see cref="Suitcase"/> database.
    ''' </summary>
    Private ReadOnly _suitcaseData As DataTable
    ''' <summary>
    ''' The fullname of the logfile (in the appdata-folder) for the <see cref="Suitcase"/>.
    ''' </summary>
    Private ReadOnly _logFileName As String

    'class

    ''' <summary>
    ''' Initializes a new instance of <see cref="Suitcase"/> with the data stored in the <see cref="Suitcase"/> database.
    ''' <para>A <see cref="Suitcase"/> has stored data about the previous contents of the synced folders (filesnames and lastwritetimes) and can read the actual contents of the source- and targetfolders.</para>
    ''' <para>It also has methods to:</para>
    ''' <para>- show what actions are needed for synchronization.</para>
    ''' <para>- perform the synchronization actions.</para>
    ''' <para>- rename this <see cref="Suitcase"/>.</para>
    ''' <para>- delete this <see cref="Suitcase"/> from the <see cref="Storage"/> it is part of.</para>
    ''' </summary>
    ''' <param name="suitcaseData">The <see cref="Suitcase"/> database.</param>
    Public Sub New(suitcaseData As DataTable)
        _suitcaseData = suitcaseData
        SourceFolder = _suitcaseData.ExtendedProperties("SourceFolder")
        TargetFolder = _suitcaseData.ExtendedProperties("TargetFolder")
        Synchronizable = IO.Directory.Exists(_SourceFolder) And IO.Directory.Exists(_TargetFolder)
        Name = _suitcaseData.TableName
        _logFileName = "{LogDataFolder}\{0}_{1}.log".Compose(_suitcaseData.DataSet.DataSetName, _suitcaseData.TableName)
    End Sub

    'subs

    ''' <summary>
    ''' Deletes the present the <see cref="Suitcase"/>.
    ''' </summary>
    Public Sub Delete()
        If _suitcaseData.DataSet.Tables.Contains(_Name) Then _suitcaseData.DataSet.Tables.Remove(_Name)
    End Sub

    ''' <summary>
    ''' Renames the present the <see cref="Suitcase"/>.
    ''' </summary>
    ''' <param name="newName">The new name for the <see cref="Suitcase"/>.</param>
    Public Sub Rename(newName As String)
        If _suitcaseData.DataSet.Tables.Contains(newName) Or newName = "" Then Exit Sub

        _suitcaseData.TableName = newName
    End Sub

    ''' <summary>
    ''' Opens the logfile.
    ''' </summary>
    Public Sub OpenLog()
        If IO.File.Exists(_logFileName) Then Shell("notepad '{0}'".Compose(_logFileName), AppWinStyle.NormalFocus)
    End Sub

    ''' <summary>
    ''' Shows the actions for synchronization.
    ''' </summary>
    ''' <param name="overrules">A list of filenames where the preferred action should be overruled.</param>
    Public Sub ShowActions(overrules As String())
        Dim actionSize = _ActionData.Size
        Dim sizeDone As Long = 0
        Dim msg = New Dictionary(Of String, List(Of String))
        Dim progressWindow = New ProgressDialog()
        progressWindow.Show()
        For Each file In _ActionData.GetFiles
            Application.DoEvents()
            Dim sync = Synchronize(file, overrules.Contains(file), True)
            If sync.ActionString = "" Then Continue For

            Dim sizeString = If(sync.FileSize < 0, "       ", FileSystemHelper.ReadableFileSystemSizeRightAligned(sync.FileSize))
            If Not msg.ContainsKey(sync.ActionString) Then msg.Add(sync.ActionString, New List(Of String))
            msg(sync.ActionString).Add("{0}  {1}  {2}".Compose(sync.ActionString, sizeString, file))
            sizeDone += sync.ActionSize
            progressWindow.NextStep("", (sizeDone / actionSize) * 100)
        Next
        progressWindow.NextStep("Preview".Translate, 100)
        For Each key In {"-->", "<--", "X--", "--X"}
            If Not msg.ContainsKey(key) Then Continue For

            progressWindow.NextStep(" ", 100)
            progressWindow.NextStep(If(key.Contains("X"), "Delete File", "Copy File").Translate, 100)
            msg(key).ForEach(Sub(line) progressWindow.NextStep(line, 100))
        Next
    End Sub

    'functions async

    ''' <summary>
    ''' Reads (async) the actual contents of the source- and targetfolders.
    ''' </summary>
    ''' <param name="progressCases">The progress provider that tracks the progress of treated cases.</param>
    ''' <param name="progressFiles">The progress provider that tracks the progress of reading files.</param>
    ''' <returns>The async <see cref="Task"/>.</returns>
    Public Async Function ReadFoldersAsync(progressCases As IProgress(Of ProgressCasesEventArgs), progressFiles As IProgress(Of ProgressFilesEventArgs)) As Task
        Dim folders = New List(Of String)
        Dim newFolders = New Dictionary(Of Stock, String)
        If IO.Directory.Exists(_SourceFolder) Then newFolders.Add(Stock.Source, _SourceFolder)
        If IO.Directory.Exists(_TargetFolder) Then newFolders.Add(Stock.Target, _TargetFolder)
        Await Task.Run(Sub()
                           Parallel.ForEach(newFolders, Sub(folder)
                                                            DeleteEmptyFolders(folder.Value)
                                                            Dim folderInfo = New FolderInfo(folder.Key, folder.Value, progressFiles)
                                                            Select Case folder.Key
                                                                Case Stock.Source : _SourceInfo = folderInfo
                                                                Case Stock.Target : _TargetInfo = folderInfo
                                                            End Select
                                                            Dim eventArgs = New ProgressCasesEventArgs(folder.Key, folder.Value, folderInfo.Count)
                                                            progressCases.Report(eventArgs)
                                                        End Sub)
                       End Sub)
        If _Synchronizable Then _ActionData = New ActionInfo(_SourceInfo, _TargetInfo, _suitcaseData)
    End Function

    'functions

    ''' <summary>
    ''' Performs the synchronization actions.
    ''' </summary>
    ''' <param name="overrules">A list of filenames where the preferred action should be overruled.</param>
    ''' <returns>A <see cref="FailureReportModel"/> to report copy and delete failures.</returns>
    Public Function Synchronize(overrules As String()) As FailureReportModel
        Dim output = New FailureReportModel
        Dim actionSize = _ActionData.Size
        Dim sizeDone As Long = 0
        Dim logTexts = New List(Of String)
        Dim progressWindow = New ProgressDialog()
        progressWindow.Show()
        Try
            logTexts.Add("Synchronized on".Translate(Format(Now, "dd-MM-yyyy, HH:mm:ss")))
            For Each file In _ActionData.GetFiles
                Application.DoEvents()
                Dim sync = Synchronize(file, overrules.Contains(file))
                Select Case sync.FailText
                    Case "Copy" : output.FailedToCopy.Add(FileSystemHelper.LimitDisplayLengthFileName(file, 60))
                    Case "Delete" : output.FailedToDelete.Add(FileSystemHelper.LimitDisplayLengthFileName(file, 60))
                End Select
                If sync.ActionString = "" Then Continue For

                Dim sizeString = If(sync.FileSize < 0, "       ", FileSystemHelper.ReadableFileSystemSizeRightAligned(sync.FileSize))
                Dim msg = "{0}  {1}  {2}".Compose(sync.ActionString, sizeString, file)
                Dim log = "{0}  {1}  {2}  {3}".Compose(sync.SourceTime.ToTimeStamp, sync.ActionString, sync.TargetTime.ToTimeStamp, file)
                sizeDone += sync.ActionSize
                progressWindow.NextStep(msg, (sizeDone / actionSize) * 100)
                logTexts.Add(log)
            Next
        Catch ex As Exception
            ex.Rethrow
        Finally
            TextFileHelper.Insert(_logFileName, logTexts.ToArray)
        End Try
        progressWindow.NextStep(" ", 100)
        progressWindow.NextStep("===  =======  {0}  =======  ===".Compose("Synchronized".Translate), 100)
        Return output
    End Function

    'private subs

    ''' <summary>
    ''' Updates the record of the specified file in the <see cref="Suitcase"/> database with the new lastwritetime or adds a record if not found.
    ''' </summary>
    ''' <param name="fileName">The partial filename.</param>
    ''' <param name="lastWriteTime"></param>
    Private Sub UpdateRow(fileName As String, lastWriteTime As Date)
        Dim row = _suitcaseData.Rows.Find(fileName)
        Select Case IsNothing(row)
            Case True : _suitcaseData.Rows.Add(fileName, lastWriteTime.ToTimeStamp)
            Case Else : row("Date") = lastWriteTime.ToTimeStamp
        End Select
    End Sub

    ''' <summary>
    ''' Deletes the record of the specified file.
    ''' </summary>
    ''' <param name="fileName">The partial filename.</param>
    Private Sub DeleteRow(fileName As String)
        _suitcaseData.Rows.Find(fileName).Delete()
    End Sub

    ''' <summary>
    ''' Deletes (async) all empty subfolders in a folder.
    ''' </summary>
    ''' <param name="path">The path of the folder.</param>
    Private Sub DeleteEmptyFolders(path As String)
        Dim deletedFolders As Integer
        Do
            deletedFolders = 0
            Dim folders = IO.Directory.GetDirectories(path, "*", IO.SearchOption.AllDirectories)
            Parallel.ForEach(folders, Sub(folder)
                                          If IO.Directory.GetFileSystemEntries(folder).Count = 0 Then
                                              Try
                                                  IO.Directory.Delete(folder)
                                                  deletedFolders += 1
                                              Catch ex As Exception
                                                  'Do nothing.
                                              End Try
                                          End If
                                      End Sub)
        Loop While deletedFolders > 0
    End Sub

    'private functions

    ''' <summary>
    ''' Performs the synchronization action of the specified file.
    ''' </summary>
    ''' <param name="fileName">The partial filename.</param>
    ''' <param name="overruled">If true, it overrules the preferred action.</param>
    ''' <param name="pretending">If true, the action is not actually performed.</param>
    ''' <returns></returns>
    Private Function Synchronize(fileName As String, overruled As Boolean, Optional pretending As Boolean = False) As SynchronizeReportModel
        Dim source = _SourceInfo.GetInfo(fileName)
        Dim target = _TargetInfo.GetInfo(fileName)
        Dim action = _ActionData.GetAction(fileName)
        Dim output = New SynchronizeReportModel(action.Size, If(IsNothing(source), Nothing, source.LastWriteTime), If(IsNothing(target), Nothing, target.LastWriteTime))
        Dim rule = If(overruled, 10, 0)
        Select Case action.Action + rule
            Case 1
                output.ActionString = "X--"
                If pretending Then Return output

                If DeleteFile(source.FileName) Then DeleteRow(fileName) : Return output

                output.FailText = "Delete"
                output.ActionString = ""
                Return output
            Case 2
                output.ActionString = "--X"
                If pretending Then Return output

                If DeleteFile(target.FileName) Then DeleteRow(fileName) : Return output

                output.FailText = "Delete"
                output.ActionString = ""
                Return output
            Case 3, 5, 8, 12, 14, 16
                output.ActionString = "<--"
                output.FileSize = target.Size
                If pretending Then Return output

                If CopyFile(target.FileName, "{0}\{1}".Compose(_SourceFolder, fileName)) Then UpdateRow(fileName, _TargetInfo.GetInfo(fileName).LastWriteTime) : Return output

                output.FailText = "Copy"
                output.ActionString = ""
                Return output
            Case 4, 6, 7, 11, 13, 15
                output.ActionString = "-->"
                output.FileSize = source.Size
                If pretending Then Return output

                If CopyFile(source.FileName, "{0}\{1}".Compose(_TargetFolder, fileName)) Then UpdateRow(fileName, _SourceInfo.GetInfo(fileName).LastWriteTime) : Return output

                output.FailText = "Copy"
                output.ActionString = ""
                Return output
            Case 9
                If pretending Then Return output

                DeleteRow(fileName)
                Return output
            Case 10
                If pretending Then Return output

                UpdateRow(fileName, _SourceInfo.GetInfo(fileName).LastWriteTime)
                Return output
            Case Else : Return output
        End Select
    End Function

    ''' <summary>
    ''' Copies the specified file to the specified location.
    ''' </summary>
    ''' <param name="fileName">The fullname of the file.</param>
    ''' <param name="[to]">The fullname to copy to.</param>
    ''' <returns>True if successful, otherwise false.</returns>
    Private Function CopyFile(fileName As String, [to] As String) As Boolean
        Dim folder = IO.Path.GetDirectoryName([to])
        If Not IO.Directory.Exists(folder) Then IO.Directory.CreateDirectory(folder)
        Try
            IO.File.Copy(fileName, [to], True)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Deletes the specified file.
    ''' </summary>
    ''' <param name="fileName">The fullname of the file.</param>
    ''' <returns>True if successful, otherwise false.</returns>
    Private Function DeleteFile(fileName As String) As Boolean
        Try
            IO.File.Delete(fileName)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
