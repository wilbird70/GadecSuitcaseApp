'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="Main"/> provides the startup (main) dialogbox for this app.</para>
''' </summary>
Public Class Main
    ''' <summary>
    ''' The present <see cref="Storage"/>.
    ''' </summary>
    Private _storage As Storage
    ''' <summary>
    ''' The present <see cref="Suitcase"/>.
    ''' </summary>
    Private _suitcase As Suitcase
    ''' <summary>
    ''' A list of filenames where the user wants to overrule the preferred actions.
    ''' </summary>
    Private _overruleList As String()
    ''' <summary>
    ''' Determines whether the dialog box is initialized.
    ''' </summary>
    Private _isInitialized As Boolean = False
    ''' <summary>
    ''' Determines if is being translated, so no change of language (with dropdownbox) is accepted.
    ''' <para>Note: Especially important when loading the dialogbox.</para>
    ''' </summary>
    Private _translationIsBusy As Boolean = True
    ''' <summary>
    ''' When true it determines that the suitcase list gets the focus, otherwise the store list.
    ''' </summary>
    Private _suitcaseFocus As Boolean = True

    'form

    ''' <summary>
    ''' EventHandler for the event that occurs when the dialogbox is loading.
    ''' <para>It initializes the instance of <see cref="Main"/>.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MyBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim companyName = "Gadec"
            Dim appName = "Suitcase App"
            Registerizer.Initialize(companyName, appName, "{0}\Settings".Compose(appName))
            Dim customCodes = New Dictionary(Of String, String) From {
                {"AppDir", Registerizer.MainSetting("AppDir")},
                {"Support", Registerizer.MainSetting("AppDir") & "\Support"},
                {"AppDataFolder", "{AppData}\{0}\{1}".Compose(companyName, appName)},
                {"LogDataFolder", "{AppData}\{0}\{1}\Logs".Compose(companyName, appName)},
                {"Stores", "{AppData}\{0}\{1}\Stores".Compose(companyName, appName)}
            }
            Composer.SetCustumCodes(customCodes)
            AddHandler Translator.LanguageChangedEvent, AddressOf LanguageChangedEventHandler
            Translator.Initialize("{Support}\SetLanguages.xml".Compose)

            Me.Text = Registerizer.GetApplicationVersion()
            FileSystemHelper.CreateFolder("{AppDataFolder}".Compose, "{LogDataFolder}".Compose, "{Stores}".Compose)

        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the visibility of the dialog changes.
    ''' <para>It reloads the stores and current suitcase.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        Try
            If _isInitialized Then Exit Sub

            _isInitialized = True
            If Not Registerizer.MainSetting("AppUpdate") = "Log-Showed" Then MessageBoxHistory()
            ReloadStoreList()
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the dialog gets the focus.
    ''' <para>It sets the focus on the suitcase list.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Main_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Select Case _suitcaseFocus
            Case True : SuitcaseListBox.Select()
            Case Else : StoreListBox.Select()
        End Select
    End Sub

    'buttons

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the close button.
    ''' <para>It closes the app.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles ltClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the Changelog button.
    ''' <para>It shows a version history messagebox.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ChangelogButton_Click(sender As Object, e As EventArgs) Handles ltChangelog.Click
        Try
            MessageBoxHistory()
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the Passwords button.
    ''' <para>It shows the <see cref="CryptoDialog"/>.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PasswordsButton_Click(sender As Object, e As EventArgs) Handles PasswordsButton.Click
        Try
            Dim dialog = New CryptoDialog
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the Synchronize button.
    ''' <para>It starts the sync process.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ltSynchronize_Click(sender As Object, e As EventArgs) Handles ltSynchronize.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim failureReport = _suitcase.Synchronize(_overruleList)
            _overruleList = {}
            _storage.Save()
            If failureReport.FailedToCopy.Count > 0 Then
                Dim number = failureReport.FailedToCopy.Count
                Dim msg = String.Join(vbLf, failureReport.FailedToCopy)
                MsgBox("Deze ({0}) bestand(en) niet kunnen kopiëren:{2L}{1}".NotYetTranslated(number, msg))
            End If
            If failureReport.FailedToDelete.Count > 0 Then
                Dim number = failureReport.FailedToDelete.Count
                Dim msg = String.Join(vbLf, failureReport.FailedToDelete)
                MsgBox("Deze ({0}) bestand(en) niet kunnen verwijderen:{2L}{1}".NotYetTranslated(number, msg))
            End If
            LoadSelectedSuitcaseAsync()
        Catch ex As Exception
            GadecException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    'listboxes

    ''' <summary>
    ''' EventHandler for the event that occurs when the user changes the selected store.
    ''' <para>It reads the store-file and lists its suitcases in a listbox.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub StoreListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles StoreListBox.SelectedIndexChanged
        Try
            Cursor.Current = Cursors.WaitCursor
            SuitcaseListBox.Items.Clear()
            ClearDialog()
            ReadStore(StoreListBox.SelectedItem)
        Catch ex As Exception
            GadecException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user changes the selected suitcase.
    ''' <para>It reads both folders (source and target) and compares it with the stored data.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SuitcaseListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SuitcaseListBox.SelectedIndexChanged
        Try
            Cursor.Current = Cursors.WaitCursor
            _overruleList = {}
            ClearDialog()
            LoadSelectedSuitcaseAsync()
        Catch ex As Exception
            GadecException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user presses a key while the store list is in focus.
    ''' <para>When it's the right arrow key, the focus changes to the suitcase list.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub StoreListBox_KeyDown(sender As Object, e As KeyEventArgs) Handles StoreListBox.KeyDown
        Try
            If e.KeyCode = Keys.Right Then
                SuitcaseListBox.Select()
                e.SuppressKeyPress = True
                _suitcaseFocus = True
            End If

        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user presses a key while the suitcase list is in focus.
    ''' <para>When it's the left arrow key, the focus changes to the store list.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SuitcaseListBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SuitcaseListBox.KeyDown
        Try
            If e.KeyCode = Keys.Left Then
                StoreListBox.Select()
                e.SuppressKeyPress = True
                _suitcaseFocus = False
            End If

        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the store list lost focus.
    ''' <para>It changes color to the system window color.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub StoreListBox_LostFocus(sender As Object, e As EventArgs) Handles StoreListBox.LostFocus
        StoreListBox.BackColor = SystemColors.Window
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the store list gets focus.
    ''' <para>It changes color to light blue.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub StoreListBox_GotFocus(sender As Object, e As EventArgs) Handles StoreListBox.GotFocus
        StoreListBox.BackColor = Color.AliceBlue
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the suitcase list lost focus.
    ''' <para>It changes color to the system window color.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SuitcaseListBox_LostFocus(sender As Object, e As EventArgs) Handles SuitcaseListBox.LostFocus
        SuitcaseListBox.BackColor = SystemColors.Window
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the suitcase list gets focus.
    ''' <para>It changes color to light blue.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SuitcaseListBox_GotFocus(sender As Object, e As EventArgs) Handles SuitcaseListBox.GotFocus
        SuitcaseListBox.BackColor = Color.AliceBlue
    End Sub


    'textboxes

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the Summary textbox.
    ''' <para>It shows the detailed actions that a sync will do.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SummaryTextBox_Click(sender As Object, e As EventArgs) Handles SummaryTextBox.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            _suitcase.ShowActions(_overruleList)
        Catch ex As Exception
            GadecException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    'comboboxes

    ''' <summary>
    ''' EventHandler for the event that occurs when the user changes the selected language.
    ''' <para>It will translate this dialog and set language as current.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LanguageComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LanguageComboBox.SelectedIndexChanged
        Try
            If _translationIsBusy Then Exit Sub

            Translator.SetLanguange(LanguageComboBox.SelectedIndex)
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    'labels

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the sourcefolder label.
    ''' <para>It starts explorer in the specified folder.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SourceFolderLabel_Click(sender As Object, e As EventArgs) Handles SourceFolderLabel.Click
        Try
            If Not IO.Directory.Exists(_suitcase.SourceFolder) Then Exit Sub

            Shell("explorer {0}".Compose(_suitcase.SourceFolder), AppWinStyle.NormalFocus)
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the targetfolder label.
    ''' <para>It starts explorer in the specified folder.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TargetFolderLabel_Click(sender As Object, e As EventArgs) Handles TargetFolderLabel.Click
        Try
            If Not IO.Directory.Exists(_suitcase.TargetFolder) Then Exit Sub

            Shell("explorer {0}".Compose(_suitcase.TargetFolder), AppWinStyle.NormalFocus)
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks a count label.
    ''' <para>It displays the respective foldersizes.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CountLabels_Click(sender As Object, e As EventArgs) Handles SourceCountLabel.Click, TargetCountLabel.Click
        Try
            SourceCountLabel.Text = "({0}) {1}".Compose(_suitcase.SourceInfo.SizeString, _suitcase.SourceInfo.Count)
            TargetCountLabel.Text = "({0}) {1}".Compose(_suitcase.TargetInfo.SizeString, _suitcase.TargetInfo.Count)
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks a conflict label.
    ''' <para>It displays a listbox that allows the user to overrule preferred actions.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ConflictLabels_Click(sender As Object, e As EventArgs) Handles TargetDeletedDeleteSourceLabel.Click, SourceDeletedDeleteTargetLabel.Click, SourceGotOlderOverwriteSourceLabel.Click, TargetGotOlderOverwriteTargetLabel.Click, BothChangedOverwriteSourceLabel.Click, BothChangedOverwriteTargetLabel.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim label = DirectCast(sender, Label)
            Dim items = _suitcase.ActionData.GetFilesSortedAndFilteredByAction(label.Tag)
            Dim dialog = New ListboxDialog(label.Name.Translate, items, _overruleList)
            If Not dialog.GetButton = vbOK Then Exit Sub

            Dim adjustableList = _overruleList.ToList
            For Each file In _overruleList
                If items.Contains(file) Then adjustableList.Remove(file)
            Next
            adjustableList.AddRange(dialog.GetSelectedItems)
            _overruleList = adjustableList.ToArray
            LoadSelectedSuitcaseAsync()
        Catch ex As Exception
            GadecException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    'eventhandlers

    ''' <summary>
    ''' EventHandler for the event that occurs when the user changes the language.
    ''' <para>It changes the language setting and starts translating (by raising an event).</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LanguageChangedEventHandler(sender As Object, e As LanguageChangedEventArgs)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim flagImageFileName = "{Support}\Lang_{0}.png".Compose(e.Selected)
            LanguagePictureBox.Image = Image.FromFile(flagImageFileName)
            _translationIsBusy = True
            Dim languageKeys = e.AvialableLanguages
            If languageKeys.Count > 0 Then
                Dim languageIndex = 0
                LanguageComboBox.Items.Clear()
                For Each k In languageKeys
                    LanguageComboBox.Items.Add(("Lang_{0}".Compose(k)).Translate)
                    If k = e.Selected Then languageIndex = LanguageComboBox.Items.Count - 1
                Next
                If LanguageComboBox.Items.Count > 0 Then LanguageComboBox.SelectedIndex = languageIndex
            End If
            _translationIsBusy = False
            Dim dataSet = DataSetHelper.LoadFromXml("{Support}\SetContextMenus.xml".Compose)
            Dim storeContextMenuStrip = MenuStripHelper.Create(dataSet.GetTable("Stores"), AddressOf ContextMenuStripClickEventHandler, My.Resources.ResourceManager)
            StoreListBox.ContextMenuStrip = storeContextMenuStrip
            Dim suitcaseContextMenuStrip = MenuStripHelper.Create(dataSet.GetTable("Cases"), AddressOf ContextMenuStripClickEventHandler, My.Resources.ResourceManager)
            SuitcaseListBox.ContextMenuStrip = suitcaseContextMenuStrip
            TranslateControles(Me)
            If _isInitialized Then LoadSelectedSuitcaseAsync()
        Catch ex As Exception
            GadecException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user selects an option in the contextmenu.
    ''' <para>It will executes the selected option.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ContextMenuStripClickEventHandler(sender As Object, e As EventArgs)
        Try
            Select Case DirectCast(sender, ToolStripItem).Tag
                Case "NewStore" : NewStoreProcedure()
                Case "DelStore" : DeleteStoreProcedure()
                Case "RenStore" : RenameStoreProcedure()
                Case "NewCase" : NewSuitcaseProcedure()
                Case "DelCase" : DeleteSuitcaseProcedure()
                Case "RenCase" : RenameSuitcaseProcedure()
                Case "OpenLog" : _suitcase.OpenLog()
            End Select
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the associated progress reports a change.
    ''' <para>It displays its result.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ProgressCasesEventHandler(ByVal sender As Object, ByVal e As ProgressCasesEventArgs)
        Select Case e.Place
            Case Stock.Source
                SourceFolderLabel.Text = FileSystemHelper.LimitDisplayLengthFileName(e.FolderName, 65)
                SourceFolderLabel.ForeColor = Color.Blue
                SourceFolderLabel.Visible = True
                SourceCountLabel.Text = e.FileCount
                SourceCountLabel.Visible = True
            Case Stock.Target
                TargetFolderLabel.Text = FileSystemHelper.LimitDisplayLengthFileName(e.FolderName, 65)
                TargetFolderLabel.ForeColor = Color.Blue
                TargetFolderLabel.Visible = True
                TargetCountLabel.Text = e.FileCount
                TargetCountLabel.Visible = True
        End Select
        Me.Refresh()
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the associated progress reports a change.
    ''' <para>It changes the value of the respective progressbar.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ProgressFilesEventHandler(ByVal sender As Object, ByVal e As ProgressFilesEventArgs)
        Dim progress = If(e.Place = Stock.Source, SourceProgressBar, TargetProgressBar)
        If Not progress.Maximum = e.Maximum Then progress.Maximum = e.Maximum
        progress.Value = e.Value
        progress.Refresh()
    End Sub

    'private subs async

    ''' <summary>
    ''' Reads the (sync) folders of the selected suitcase and determine its preferred actions.
    ''' </summary>
    Private Async Sub LoadSelectedSuitcaseAsync()
        StoreListBox.Enabled = False
        SuitcaseListBox.Enabled = False
        SummaryTextBox.Enabled = False

        Try
            ClearDialog()
            Dim selectedSuitcase = _storage.GetSuitcase(SuitcaseListBox.SelectedItem.ToString)
            If IsNothing(selectedSuitcase) Then Exit Sub

            _suitcase = selectedSuitcase
            Dim progressCases = New Progress(Of ProgressCasesEventArgs)
            AddHandler progressCases.ProgressChanged, AddressOf ProgressCasesEventHandler
            Dim progressFiles = New Progress(Of ProgressFilesEventArgs)
            AddHandler progressFiles.ProgressChanged, AddressOf ProgressFilesEventHandler

            Await _suitcase.ReadFoldersAsync(progressCases, progressFiles)
            Registerizer.UserSetting("LastSelectedCaseName", _suitcase.Name)
            If Not _suitcase.Synchronizable Then
                SummaryTextBox.Text = ""
                ltSynchronize.Enabled = False
                SummaryTextBox.BackColor = Color.Beige
                If SourceFolderLabel.Text = "XXX" Then SourceFolderLabel.Visible = True : SourceFolderLabel.Text = "Not available".Translate
                If TargetFolderLabel.Text = "XXX" Then TargetFolderLabel.Visible = True : TargetFolderLabel.Text = "Not available".Translate
                Exit Sub
            End If

            Dim count = WireUpConflicts()
            Dim summary = GetActionSummary(_overruleList)
            Select Case summary = ""
                Case True
                    SummaryTextBox.Text = "{0}{CL}({1})".Compose("Synchronized".Translate, _suitcase.SourceInfo.SizeString(1))
                    ltSynchronize.Enabled = False
                Case Else
                    SummaryTextBox.Text = summary
                    SummaryTextBox.BackColor = Color.Honeydew
                    ltSynchronize.Enabled = True
            End Select
            If count > 0 Then SummaryTextBox.BackColor = Color.MistyRose

        Catch ex As Exception
            'nothing to do
        Finally
            SourceProgressBar.Value = 0
            TargetProgressBar.Value = 0
            StoreListBox.Enabled = True
            SuitcaseListBox.Enabled = True
            SummaryTextBox.Enabled = True
            Select Case _suitcaseFocus
                Case True : SuitcaseListBox.Select()
                Case Else : StoreListBox.Select()
            End Select
        End Try
    End Sub

    'private subs 

    ''' <summary>
    ''' Translates the controls associated with the specified parent.
    ''' </summary>
    ''' <param name="parent">The parent control.</param>
    Private Sub TranslateControles(ByVal parent As Control)
        For Each control In parent.Controls.ToArray
            If control.Name.StartsWith("lt") Then control.Text = control.Name.Translate
            If control.HasChildren Then TranslateControles(control)
        Next
    End Sub

    ''' <summary>
    ''' Reloads the stores in a listbox.
    ''' </summary>
    Private Sub ReloadStoreList()
        Dim storeFiles = IO.Directory.GetFiles("{Stores}".Compose, "*.xml").ToList
        storeFiles.Sort()
        StoreListBox.Items.Clear()
        If storeFiles.Count = 0 Then ClearDialog() : Exit Sub

        Dim index = 0
        Dim lastSelected = Registerizer.UserSetting("LastSelectedStoreName")
        For i = 0 To storeFiles.Count - 1
            Dim store = IO.Path.GetFileNameWithoutExtension(storeFiles(i))
            StoreListBox.Items.Add(store)
            If store = lastSelected Then index = i
        Next
        StoreListBox.SelectedIndex = index
    End Sub

    ''' <summary>
    ''' Reads the store-file and lists its suitcases in a listbox.
    ''' </summary>
    ''' <param name="storeName"></param>
    Private Sub ReadStore(storeName As String)
        Registerizer.UserSetting("LastSelectedStoreName", storeName)
        Dim fileName = "{Stores}\{0}.xml".Compose(storeName)
        _storage = New Storage(fileName)
        SuitcaseListBox.Items.AddRange(_storage.GetSuitcaseNames)
        If SuitcaseListBox.Items.Count = 0 Then Exit Sub

        Dim index = 0
        Dim lastSelected = Registerizer.UserSetting("LastSelectedCaseName")
        For i = 0 To SuitcaseListBox.Items.Count - 1
            If Not SuitcaseListBox.Items(i) = lastSelected Then Continue For

            index = i
            Exit For
        Next
        SuitcaseListBox.SetSelected(index, True)
    End Sub

    ''' <summary>
    ''' Clears the whole dialog to start over loading/reading.
    ''' </summary>
    Private Sub ClearDialog()
        ltSynchronize.Enabled = False
        ltConflicts.Visible = False
        ltConflictsDel.Visible = False
        ltConflictsOld.Visible = False
        ltConflictsBth.Visible = False
        TargetDeletedDeleteSourceLabel.Visible = False
        SourceDeletedDeleteTargetLabel.Visible = False
        SourceGotOlderOverwriteSourceLabel.Visible = False
        TargetGotOlderOverwriteTargetLabel.Visible = False
        BothChangedOverwriteSourceLabel.Visible = False
        BothChangedOverwriteTargetLabel.Visible = False
        SourceFolderLabel.Visible = False
        SourceFolderLabel.ForeColor = Color.Black
        SourceCountLabel.Visible = False
        TargetFolderLabel.Visible = False
        TargetFolderLabel.ForeColor = Color.Black
        TargetCountLabel.Visible = False
        SummaryTextBox.Text = ""
        SummaryTextBox.BackColor = Color.White
        Me.Refresh()
    End Sub

    ''' <summary>
    ''' Wires up the specified (conflict) label.
    ''' </summary>
    ''' <param name="label">The label.</param>
    ''' <param name="conflicts">List of files with a conflict.</param>
    ''' <param name="position">Position for the label.</param>
    ''' <param name="side">Determine which stock is involved.</param>
    Private Sub ViewConflictsLabel(label As Label, conflicts As String(), position As Integer, side As Stock)
        label.Visible = False
        If conflicts.Count = 0 Then Exit Sub

        Dim count = 0
        For Each file In _overruleList
            If conflicts.Contains(file) Then count += 1
        Next
        Dim text = (conflicts.Count - count).ToString
        If count > 0 Then text = "{0},{1}".Compose(text, count.ToString)
        Select Case side
            Case Stock.Source : label.Text = "SourceCount".Translate(text)
            Case Stock.Target : label.Text = "TargetCount".Translate(text)
        End Select
        label.Visible = True
        label.Top = 3 + (13 * position)
    End Sub

    ''' <summary>
    ''' Allows the user to create a new store.
    ''' </summary>
    Private Sub NewStoreProcedure()
        Dim newStorageName = InputBox("NewName:".Translate, Registerizer.GetApplicationVersion(), "")
        newStorageName = FileSystemHelper.RemoveInvalidFileNameCharacters(newStorageName)
        If newStorageName = "" Then Exit Sub

        Dim newFileName = "{Stores}\{0}.xml".Compose(newStorageName)
        If IO.File.Exists(newFileName) Then MessageBoxInfo("FileExists".Translate) : Exit Sub

        Dim newStore = New DataSet(newStorageName)
        Registerizer.UserSetting("LastSelectedStoreName", newStorageName)
        newStore.ExtendedProperties.Add("FileName", newFileName)
        newStore.WriteXml(newFileName, XmlWriteMode.WriteSchema)
        ReloadStoreList()
    End Sub

    ''' <summary>
    ''' Allows the user to rename a store.
    ''' </summary>
    Private Sub RenameStoreProcedure()
        Dim newStorageName = InputBox("EditName:".Translate, Registerizer.GetApplicationVersion(), _storage.Name)
        newStorageName = FileSystemHelper.RemoveInvalidFileNameCharacters(newStorageName)
        If newStorageName = "" Or newStorageName = _storage.Name Then Exit Sub

        Dim newFileName = "{Stores}\{0}.xml".Compose(newStorageName)
        If IO.File.Exists(newFileName) Then MessageBoxInfo("FileExists".Translate) : Exit Sub

        _storage.Rename(newFileName)
        Registerizer.UserSetting("LastSelectedStoreName", newStorageName)
        ReloadStoreList()
    End Sub

    ''' <summary>
    ''' Allows the user to delete a store.
    ''' </summary>
    Private Sub DeleteStoreProcedure()
        Dim messageResult = MessageBoxQuestion("DelStoreQuestion".Translate(_storage.Name), MessageBoxButtons.YesNo)
        If Not messageResult = vbYes Then Exit Sub

        _storage.Delete()
        ReloadStoreList()
    End Sub

    ''' <summary>
    ''' Allows the user to create a new suitcase.
    ''' </summary>
    Private Sub NewSuitcaseProcedure()
        Dim newCaseName = InputBox("NewName:".Translate, Registerizer.GetApplicationVersion(), "")
        If newCaseName = "" Then Exit Sub
        If _storage.Contains(newCaseName) Then MessageBoxInfo("CaseExists".Translate) : Exit Sub

        Dim sourceFolder = FileSystemHelper.SelectFolder(Registerizer.UserSetting("LastSelectedSourceFolder"))
        If Not IO.Directory.Exists(sourceFolder) Then Exit Sub

        If sourceFolder.Length = 3 Then MessageBoxInfo("OnlyDrive".Translate) : Exit Sub

        Registerizer.UserSetting("LastSelectedSourceFolder", sourceFolder)
        Dim targetFolder = FileSystemHelper.SelectFolder(Registerizer.UserSetting("LastSelectedTargetFolder"))
        If Not IO.Directory.Exists(targetFolder) Then Exit Sub

        If targetFolder.Length = 3 Then MessageBoxInfo("OnlyDrive".Translate) : Exit Sub

        Registerizer.UserSetting("LastSelectedTargetFolder", targetFolder)
        _storage.Add(newCaseName, sourceFolder, targetFolder)
        Registerizer.UserSetting("LastSelectedCaseName", newCaseName)
        ReloadStoreList()
    End Sub

    ''' <summary>
    ''' Allows the user to rename a suitcase.
    ''' </summary>
    Private Sub RenameSuitcaseProcedure()
        Dim newCaseName = InputBox("NewName:".Translate, Registerizer.GetApplicationVersion(), _suitcase.Name)
        If newCaseName = "" Or newCaseName = _suitcase.Name Then Exit Sub
        If _storage.Contains(newCaseName) Then MessageBoxInfo("CaseExists".Translate) : Exit Sub

        _suitcase.Rename(newCaseName)
        Registerizer.UserSetting("LastSelectedCaseName", newCaseName)
        _storage.Save()
        ReloadStoreList()
    End Sub

    ''' <summary>
    ''' Allows the user to delete a suitcase.
    ''' </summary>
    Private Sub DeleteSuitcaseProcedure()
        Dim messageResult = MessageBoxQuestion("DelCaseQuestion".Translate(_suitcase.Name), MessageBoxButtons.YesNo)
        If Not messageResult = vbYes Then Exit Sub

        _suitcase.Delete()
        _storage.Save()
        ReloadStoreList()
    End Sub

    'private functions

    ''' <summary>
    ''' Wires up the conflictlabels.
    ''' </summary>
    ''' <returns>Number of all conflicts.</returns>
    Private Function WireUpConflicts() As Integer
        Dim deleteSource = _suitcase.ActionData.GetFilesSortedAndFilteredByAction(Action.DeleteSource)
        Dim deleteTarget = _suitcase.ActionData.GetFilesSortedAndFilteredByAction(Action.DeleteTarget)
        Dim becameOlderCopyTarget = _suitcase.ActionData.GetFilesSortedAndFilteredByAction(Action.BecameOlderCopyTarget)
        Dim becameOlderCopySource = _suitcase.ActionData.GetFilesSortedAndFilteredByAction(Action.BecameOlderCopySource)
        Dim bothChangedCopyTarget = _suitcase.ActionData.GetFilesSortedAndFilteredByAction(Action.BothChangedCopyTarget)
        Dim bothChangedCopySource = _suitcase.ActionData.GetFilesSortedAndFilteredByAction(Action.BothChangedCopySource)
        Dim deleted = deleteSource.Count + deleteTarget.Count > 0
        Dim becameOlder = becameOlderCopyTarget.Count + becameOlderCopySource.Count > 0
        Dim bothChanged = bothChangedCopyTarget.Count + bothChangedCopySource.Count > 0
        ltConflicts.Visible = deleted Or becameOlder Or bothChanged
        ltConflictsDel.Visible = deleted
        ltConflictsOld.Visible = becameOlder
        ltConflictsBth.Visible = bothChanged
        Dim output = 0
        If deleted Then output += 1
        ltConflictsDel.Top = 3 + (13 * output)
        ViewConflictsLabel(TargetDeletedDeleteSourceLabel, deleteSource, output, Stock.Source)
        ViewConflictsLabel(SourceDeletedDeleteTargetLabel, deleteTarget, output, Stock.Target)
        If becameOlder Then output += 1
        ltConflictsOld.Top = 3 + (13 * output)
        ViewConflictsLabel(SourceGotOlderOverwriteSourceLabel, becameOlderCopyTarget, output, Stock.Source)
        ViewConflictsLabel(TargetGotOlderOverwriteTargetLabel, becameOlderCopySource, output, Stock.Target)
        If bothChanged Then output += 1
        ltConflictsBth.Top = 3 + (13 * output)
        ViewConflictsLabel(BothChangedOverwriteSourceLabel, bothChangedCopyTarget, output, Stock.Source)
        ViewConflictsLabel(BothChangedOverwriteTargetLabel, bothChangedCopySource, output, Stock.Target)
        Return output
    End Function

    ''' <summary>
    ''' Gets the action summary for the summary textbox.
    ''' </summary>
    ''' <param name="overrule">A list of filenames where the user wants to overrule the preferred actions.</param>
    ''' <returns>The summary text.</returns>
    Private Function GetActionSummary(overrule As String()) As String
        Dim container = New Dictionary(Of String, Integer)
        For Each file In _suitcase.ActionData.GetFiles
            Dim rule = If(overrule.Contains(file), 10, 0)
            Dim action = ""
            Select Case _suitcase.ActionData.GetAction(file).Action + rule
                Case 1 : action = "X--"
                Case 2 : action = "--X"
                Case 3, 5, 8, 12, 14, 16 : action = "<--"
                Case 4, 6, 7, 11, 13, 15 : action = "-->"
            End Select
            Select Case container.ContainsKey(action)
                Case True : container(action) += 1
                Case Else : container.Add(action, 1)
            End Select
        Next
        Dim containerKeys = container.Keys.ToList
        containerKeys.Sort()
        Dim output = ""
        For i = 0 To containerKeys.Count - 1
            Select Case True
                Case containerKeys(i) = ""
                Case container(containerKeys(i)) < 2 : output &= "OneFile".Translate(containerKeys(i), container(containerKeys(i)))
                Case container(containerKeys(i)) > 1 : output &= "MoreFiles".Translate(containerKeys(i), container(containerKeys(i)))
            End Select
        Next
        Return output
    End Function

End Class