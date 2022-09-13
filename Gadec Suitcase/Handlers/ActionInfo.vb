'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="ActionInfo"/> lists (async) preferred actions for the files in the source- and targetfolders and the suitcase database.</para>
''' </summary>
Public Class ActionInfo
    ''' <summary>
    ''' The size of the actions it contains.
    ''' </summary>
    ''' <returns>Size in bytes.</returns>
    Public ReadOnly Property Size As Long = 0

    ''' <summary>
    ''' Contains data of the preferred actions for the files.
    ''' </summary>
    Private ReadOnly _actionInfo As New Dictionary(Of String, ActionModel)
    ''' <summary>
    ''' The <see cref="FolderInfo"/> of the source folder.
    ''' <para>Contains data of files in the folder.</para>
    ''' </summary>
    Private ReadOnly _sourceInfo As FolderInfo
    ''' <summary>
    ''' The <see cref="FolderInfo"/> of the target folder.
    ''' <para>Contains data of files in the folder.</para>
    ''' </summary>
    Private ReadOnly _targetInfo As FolderInfo
    ''' <summary>
    ''' The suitcase database as stored in an xml-file in the appdata folder.
    ''' </summary>
    Private ReadOnly _activeCase As DataTable
    ''' <summary>
    ''' Contains the list of partial filenames of the source folder.
    ''' </summary>
    Private ReadOnly _sourceList As String()
    ''' <summary>
    ''' Contains the list of partial filenames of the target folder.
    ''' </summary>
    Private ReadOnly _targetList As String()
    ''' <summary>
    ''' Contains the list of partial filenames as stored in the xml file.
    ''' </summary>
    Private ReadOnly _casingList As String()

    'class

    ''' <summary>
    ''' Initializes a new instance of <see cref="ActionInfo"/> with the specified suitcase database and <see cref="FolderInfo"/> objects.
    ''' <para><see cref="ActionInfo"/> lists (async) preferred actions for the files in the source- and targetfolders and the suitcase database.</para>
    ''' </summary>
    ''' <param name="activeCase">The suitcase database as stored in an xml-file in the appdata folder.</param>
    ''' <param name="sourceInfo">The <see cref="FolderInfo"/> of the source folder.</param>
    ''' <param name="targetInfo">The <see cref="FolderInfo"/> of the target folder.</param>
    Public Sub New(sourceInfo As FolderInfo, targetInfo As FolderInfo, activeCase As DataTable)
        _activeCase = activeCase
        _sourceInfo = sourceInfo
        _targetInfo = targetInfo
        _casingList = _activeCase.GetStringsFromColumn("File")
        _sourceList = _sourceInfo.GetFileList
        _targetList = _targetInfo.GetFileList
        Dim fileNames = _casingList.ToList
        fileNames.AddRange(_sourceList)
        fileNames.AddRange(_targetList)
        Dim lock = New Object
        Parallel.ForEach(fileNames.Distinct.ToList, Sub(fileName)
                                                        Dim action = DefineAction(fileName)
                                                        SyncLock lock
                                                            _actionInfo.Add(fileName, action)
                                                            _Size += action.Size
                                                        End SyncLock
                                                    End Sub)
    End Sub

    'functions

    ''' <summary>
    ''' Gets a sorted list of all partial filenames that have preferred actions.
    ''' </summary>
    ''' <returns>The list of partial filenames.</returns>
    Public Function GetFiles() As String()
        Dim files = _actionInfo.Keys.ToList
        files.Sort()
        Return files.ToArray
    End Function

    ''' <summary>
    ''' Gets a sorted list of partial filenames that have the specified preferred action.
    ''' </summary>
    ''' <param name="action">The preferred action.</param>
    ''' <returns>The list of partial filenames.</returns>
    Public Function GetFilesSortedAndFilteredByAction(action As Action) As String()
        Dim files = _actionInfo.Where(Function(z) z.Value.Action = action).ToDictionary(Function(z) z.Key, Function(z) z.Value).Keys.ToList
        files.Sort()
        Return files.ToArray
    End Function

    ''' <summary>
    ''' Get the the preferred action for the specified partial filename.
    ''' </summary>
    ''' <param name="fileName">The partial filename.</param>
    ''' <returns>The <see cref="ActionModel"/> of the file.</returns>
    Public Function GetAction(fileName As String) As ActionModel
        If Not _actionInfo.ContainsKey(fileName) Then Return Nothing

        Return _actionInfo(fileName)
    End Function

    'private functions

    ''' <summary>
    ''' Defines the preferred action for the specified partial filename.
    ''' </summary>
    ''' <param name="fileName">The partial filename.</param>
    ''' <returns>The <see cref="ActionModel"/> of the file.</returns>
    Private Function DefineAction(fileName As String) As ActionModel
        Dim code = 0
        Dim sourceDate As Date
        Dim targetDate As Date
        Dim casingDate As Date
        If _sourceList.Contains(fileName) Then code += 1 : sourceDate = _sourceInfo.GetInfo(fileName).LastWriteTime
        If _targetList.Contains(fileName) Then code += 2 : targetDate = _targetInfo.GetInfo(fileName).LastWriteTime
        If _casingList.Contains(fileName) Then code += 4 : casingDate = _activeCase.Rows.Find(fileName).GetString("Date").TimeStampToDate
        Dim action As Action = Action.DoNothing
        Select Case True
            Case code = 1 : action = Action.CopySource                                                              'alleen in Bron 
            Case code = 2 : action = Action.CopyTarget                                                              'alleen in Doel
            Case code = 3 AndAlso sourceDate.FileDateEquals(targetDate) : action = Action.UpdateSuitcase            'niet in Suitcase, Bron en Doel gelijk aan elkaar
            Case code = 3 AndAlso sourceDate > targetDate : action = Action.CopySource                              'niet in Suitcase, Bron is nieuwer
            Case code = 3 : action = Action.CopyTarget                                                              'niet in Suitcase, Doel is nieuwer
            Case code = 4 : action = Action.DeleteSuitcase                                                          'alleen in Suitcase
            Case code = 5 : action = Action.DeleteSource                                                            'alleen in Bron en Suitcase
            Case code = 6 : action = Action.DeleteTarget                                                            'alleen in Doel en Suitcase
            Case sourceDate.FileDateEquals(casingDate) AndAlso targetDate.FileDateEquals(casingDate)                'alle drie gelijk
            Case sourceDate.FileDateEquals(casingDate) AndAlso targetDate > casingDate : action = Action.CopyTarget 'alleen Doel is nieuwer
            Case sourceDate.FileDateEquals(casingDate) : action = Action.BecameOlderCopySource                      'alleen Doel is ouder
            Case targetDate.FileDateEquals(casingDate) AndAlso sourceDate > casingDate : action = Action.CopySource 'alleen Bron is nieuwer
            Case targetDate.FileDateEquals(casingDate) : action = Action.BecameOlderCopyTarget                      'alleen Bron is ouder
            Case targetDate.FileDateEquals(sourceDate) : action = Action.UpdateSuitcase                             'alleen Suitcase is anders
            Case sourceDate > targetDate : action = Action.BothChangedCopySource                                    'alle drie anders, Bron nieuwer
            Case Else : action = Action.BothChangedCopyTarget                                                       'alle drie anders, Doel nieuwer
        End Select
        Dim size As Long
        Select Case action
            Case Action.CopySource, Action.BecameOlderCopySource, Action.BothChangedCopySource : size = _sourceInfo.GetInfo(fileName).Size
            Case Action.CopyTarget, Action.BecameOlderCopyTarget, Action.BothChangedCopyTarget : size = _targetInfo.GetInfo(fileName).Size
            Case Action.DeleteSource : size = 1024
            Case Action.DeleteTarget : size = 1024
            Case Else : size = 16
        End Select
        Return New ActionModel(action, size)
    End Function

End Class
