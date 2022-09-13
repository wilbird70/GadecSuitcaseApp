<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.ltClose = New System.Windows.Forms.Button()
        Me.ltStore = New System.Windows.Forms.Label()
        Me.ltSuitcase = New System.Windows.Forms.Label()
        Me.ltState = New System.Windows.Forms.Label()
        Me.LanguageComboBox = New System.Windows.Forms.ComboBox()
        Me.StoreListBox = New System.Windows.Forms.ListBox()
        Me.SuitcaseListBox = New System.Windows.Forms.ListBox()
        Me.TargetFolderLabel = New System.Windows.Forms.Label()
        Me.SourceFolderLabel = New System.Windows.Forms.Label()
        Me.TargetCountLabel = New System.Windows.Forms.Label()
        Me.SourceCountLabel = New System.Windows.Forms.Label()
        Me.ltSynchronize = New System.Windows.Forms.Button()
        Me.SummaryTextBox = New System.Windows.Forms.TextBox()
        Me.ltConflicts = New System.Windows.Forms.GroupBox()
        Me.ltConflictsDel = New System.Windows.Forms.Label()
        Me.ltConflictsOld = New System.Windows.Forms.Label()
        Me.ltConflictsBth = New System.Windows.Forms.Label()
        Me.TargetDeletedDeleteSourceLabel = New System.Windows.Forms.Label()
        Me.SourceDeletedDeleteTargetLabel = New System.Windows.Forms.Label()
        Me.SourceGotOlderOverwriteSourceLabel = New System.Windows.Forms.Label()
        Me.TargetGotOlderOverwriteTargetLabel = New System.Windows.Forms.Label()
        Me.BothChangedOverwriteSourceLabel = New System.Windows.Forms.Label()
        Me.BothChangedOverwriteTargetLabel = New System.Windows.Forms.Label()
        Me.LanguagePictureBox = New System.Windows.Forms.PictureBox()
        Me.PasswordsButton = New System.Windows.Forms.Button()
        Me.ltChangelog = New System.Windows.Forms.Button()
        Me.SourceProgressBar = New System.Windows.Forms.ProgressBar()
        Me.TargetProgressBar = New System.Windows.Forms.ProgressBar()
        Me.ltConflicts.SuspendLayout()
        CType(Me.LanguagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ltClose
        '
        Me.ltClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ltClose.Location = New System.Drawing.Point(354, 331)
        Me.ltClose.Name = "ltClose"
        Me.ltClose.Size = New System.Drawing.Size(75, 23)
        Me.ltClose.TabIndex = 14
        Me.ltClose.Text = "XXX"
        Me.ltClose.UseVisualStyleBackColor = True
        '
        'ltStore
        '
        Me.ltStore.AutoSize = True
        Me.ltStore.Location = New System.Drawing.Point(12, 9)
        Me.ltStore.Name = "ltStore"
        Me.ltStore.Size = New System.Drawing.Size(28, 13)
        Me.ltStore.TabIndex = 23
        Me.ltStore.Text = "XXX"
        '
        'ltSuitcase
        '
        Me.ltSuitcase.AutoSize = True
        Me.ltSuitcase.Location = New System.Drawing.Point(153, 9)
        Me.ltSuitcase.Name = "ltSuitcase"
        Me.ltSuitcase.Size = New System.Drawing.Size(28, 13)
        Me.ltSuitcase.TabIndex = 24
        Me.ltSuitcase.Text = "XXX"
        '
        'ltState
        '
        Me.ltState.AutoSize = True
        Me.ltState.Location = New System.Drawing.Point(294, 9)
        Me.ltState.Name = "ltState"
        Me.ltState.Size = New System.Drawing.Size(28, 13)
        Me.ltState.TabIndex = 26
        Me.ltState.Text = "XXX"
        '
        'LanguageComboBox
        '
        Me.LanguageComboBox.FormattingEnabled = True
        Me.LanguageComboBox.Location = New System.Drawing.Point(13, 304)
        Me.LanguageComboBox.Name = "LanguageComboBox"
        Me.LanguageComboBox.Size = New System.Drawing.Size(133, 21)
        Me.LanguageComboBox.TabIndex = 48
        '
        'StoreListBox
        '
        Me.StoreListBox.FormattingEnabled = True
        Me.StoreListBox.Location = New System.Drawing.Point(12, 25)
        Me.StoreListBox.Name = "StoreListBox"
        Me.StoreListBox.Size = New System.Drawing.Size(135, 186)
        Me.StoreListBox.TabIndex = 55
        '
        'SuitcaseListBox
        '
        Me.SuitcaseListBox.FormattingEnabled = True
        Me.SuitcaseListBox.Location = New System.Drawing.Point(153, 25)
        Me.SuitcaseListBox.Name = "SuitcaseListBox"
        Me.SuitcaseListBox.Size = New System.Drawing.Size(135, 186)
        Me.SuitcaseListBox.TabIndex = 57
        '
        'TargetFolderLabel
        '
        Me.TargetFolderLabel.AutoSize = True
        Me.TargetFolderLabel.Location = New System.Drawing.Point(12, 244)
        Me.TargetFolderLabel.Name = "TargetFolderLabel"
        Me.TargetFolderLabel.Size = New System.Drawing.Size(28, 13)
        Me.TargetFolderLabel.TabIndex = 59
        Me.TargetFolderLabel.Text = "XXX"
        '
        'SourceFolderLabel
        '
        Me.SourceFolderLabel.AutoSize = True
        Me.SourceFolderLabel.BackColor = System.Drawing.SystemColors.Control
        Me.SourceFolderLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SourceFolderLabel.Location = New System.Drawing.Point(12, 222)
        Me.SourceFolderLabel.Name = "SourceFolderLabel"
        Me.SourceFolderLabel.Size = New System.Drawing.Size(28, 13)
        Me.SourceFolderLabel.TabIndex = 58
        Me.SourceFolderLabel.Text = "XXX"
        '
        'TargetCountLabel
        '
        Me.TargetCountLabel.Location = New System.Drawing.Point(294, 244)
        Me.TargetCountLabel.Name = "TargetCountLabel"
        Me.TargetCountLabel.Size = New System.Drawing.Size(135, 13)
        Me.TargetCountLabel.TabIndex = 61
        Me.TargetCountLabel.Text = "XXX"
        Me.TargetCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SourceCountLabel
        '
        Me.SourceCountLabel.Location = New System.Drawing.Point(294, 222)
        Me.SourceCountLabel.Name = "SourceCountLabel"
        Me.SourceCountLabel.Size = New System.Drawing.Size(135, 13)
        Me.SourceCountLabel.TabIndex = 60
        Me.SourceCountLabel.Text = "XXX"
        Me.SourceCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ltSynchronize
        '
        Me.ltSynchronize.Location = New System.Drawing.Point(260, 331)
        Me.ltSynchronize.Name = "ltSynchronize"
        Me.ltSynchronize.Size = New System.Drawing.Size(93, 23)
        Me.ltSynchronize.TabIndex = 63
        Me.ltSynchronize.Text = "XXX"
        Me.ltSynchronize.UseVisualStyleBackColor = True
        '
        'SummaryTextBox
        '
        Me.SummaryTextBox.BackColor = System.Drawing.SystemColors.Window
        Me.SummaryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SummaryTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SummaryTextBox.Location = New System.Drawing.Point(294, 25)
        Me.SummaryTextBox.Multiline = True
        Me.SummaryTextBox.Name = "SummaryTextBox"
        Me.SummaryTextBox.Size = New System.Drawing.Size(135, 186)
        Me.SummaryTextBox.TabIndex = 64
        Me.SummaryTextBox.WordWrap = False
        '
        'ltConflicts
        '
        Me.ltConflicts.Controls.Add(Me.ltConflictsDel)
        Me.ltConflicts.Controls.Add(Me.ltConflictsOld)
        Me.ltConflicts.Controls.Add(Me.ltConflictsBth)
        Me.ltConflicts.Controls.Add(Me.TargetDeletedDeleteSourceLabel)
        Me.ltConflicts.Controls.Add(Me.SourceDeletedDeleteTargetLabel)
        Me.ltConflicts.Controls.Add(Me.SourceGotOlderOverwriteSourceLabel)
        Me.ltConflicts.Controls.Add(Me.TargetGotOlderOverwriteTargetLabel)
        Me.ltConflicts.Controls.Add(Me.BothChangedOverwriteSourceLabel)
        Me.ltConflicts.Controls.Add(Me.BothChangedOverwriteTargetLabel)
        Me.ltConflicts.Location = New System.Drawing.Point(153, 269)
        Me.ltConflicts.Name = "ltConflicts"
        Me.ltConflicts.Size = New System.Drawing.Size(276, 58)
        Me.ltConflicts.TabIndex = 65
        Me.ltConflicts.TabStop = False
        Me.ltConflicts.Text = "XXX"
        '
        'ltConflictsDel
        '
        Me.ltConflictsDel.AutoSize = True
        Me.ltConflictsDel.Location = New System.Drawing.Point(6, 16)
        Me.ltConflictsDel.Name = "ltConflictsDel"
        Me.ltConflictsDel.Size = New System.Drawing.Size(28, 13)
        Me.ltConflictsDel.TabIndex = 8
        Me.ltConflictsDel.Text = "XXX"
        '
        'ltConflictsOld
        '
        Me.ltConflictsOld.AutoSize = True
        Me.ltConflictsOld.Location = New System.Drawing.Point(6, 29)
        Me.ltConflictsOld.Name = "ltConflictsOld"
        Me.ltConflictsOld.Size = New System.Drawing.Size(28, 13)
        Me.ltConflictsOld.TabIndex = 11
        Me.ltConflictsOld.Text = "XXX"
        '
        'ltConflictsBth
        '
        Me.ltConflictsBth.AutoSize = True
        Me.ltConflictsBth.Location = New System.Drawing.Point(6, 42)
        Me.ltConflictsBth.Name = "ltConflictsBth"
        Me.ltConflictsBth.Size = New System.Drawing.Size(28, 13)
        Me.ltConflictsBth.TabIndex = 17
        Me.ltConflictsBth.Text = "XXX"
        '
        'TargetDeletedDeleteSourceLabel
        '
        Me.TargetDeletedDeleteSourceLabel.ForeColor = System.Drawing.Color.Blue
        Me.TargetDeletedDeleteSourceLabel.Location = New System.Drawing.Point(135, 16)
        Me.TargetDeletedDeleteSourceLabel.Name = "TargetDeletedDeleteSourceLabel"
        Me.TargetDeletedDeleteSourceLabel.Size = New System.Drawing.Size(65, 13)
        Me.TargetDeletedDeleteSourceLabel.TabIndex = 9
        Me.TargetDeletedDeleteSourceLabel.Tag = "1"
        Me.TargetDeletedDeleteSourceLabel.Text = "XXX"
        Me.TargetDeletedDeleteSourceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SourceDeletedDeleteTargetLabel
        '
        Me.SourceDeletedDeleteTargetLabel.ForeColor = System.Drawing.Color.Blue
        Me.SourceDeletedDeleteTargetLabel.Location = New System.Drawing.Point(206, 16)
        Me.SourceDeletedDeleteTargetLabel.Name = "SourceDeletedDeleteTargetLabel"
        Me.SourceDeletedDeleteTargetLabel.Size = New System.Drawing.Size(64, 13)
        Me.SourceDeletedDeleteTargetLabel.TabIndex = 10
        Me.SourceDeletedDeleteTargetLabel.Tag = "2"
        Me.SourceDeletedDeleteTargetLabel.Text = "XXX"
        Me.SourceDeletedDeleteTargetLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SourceGotOlderOverwriteSourceLabel
        '
        Me.SourceGotOlderOverwriteSourceLabel.ForeColor = System.Drawing.Color.Blue
        Me.SourceGotOlderOverwriteSourceLabel.Location = New System.Drawing.Point(135, 29)
        Me.SourceGotOlderOverwriteSourceLabel.Name = "SourceGotOlderOverwriteSourceLabel"
        Me.SourceGotOlderOverwriteSourceLabel.Size = New System.Drawing.Size(65, 13)
        Me.SourceGotOlderOverwriteSourceLabel.TabIndex = 12
        Me.SourceGotOlderOverwriteSourceLabel.Tag = "3"
        Me.SourceGotOlderOverwriteSourceLabel.Text = "XXX"
        Me.SourceGotOlderOverwriteSourceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TargetGotOlderOverwriteTargetLabel
        '
        Me.TargetGotOlderOverwriteTargetLabel.ForeColor = System.Drawing.Color.Blue
        Me.TargetGotOlderOverwriteTargetLabel.Location = New System.Drawing.Point(206, 29)
        Me.TargetGotOlderOverwriteTargetLabel.Name = "TargetGotOlderOverwriteTargetLabel"
        Me.TargetGotOlderOverwriteTargetLabel.Size = New System.Drawing.Size(64, 13)
        Me.TargetGotOlderOverwriteTargetLabel.TabIndex = 13
        Me.TargetGotOlderOverwriteTargetLabel.Tag = "4"
        Me.TargetGotOlderOverwriteTargetLabel.Text = "XXX"
        Me.TargetGotOlderOverwriteTargetLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BothChangedOverwriteSourceLabel
        '
        Me.BothChangedOverwriteSourceLabel.ForeColor = System.Drawing.Color.Blue
        Me.BothChangedOverwriteSourceLabel.Location = New System.Drawing.Point(135, 42)
        Me.BothChangedOverwriteSourceLabel.Name = "BothChangedOverwriteSourceLabel"
        Me.BothChangedOverwriteSourceLabel.Size = New System.Drawing.Size(65, 13)
        Me.BothChangedOverwriteSourceLabel.TabIndex = 18
        Me.BothChangedOverwriteSourceLabel.Tag = "5"
        Me.BothChangedOverwriteSourceLabel.Text = "XXX"
        Me.BothChangedOverwriteSourceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BothChangedOverwriteTargetLabel
        '
        Me.BothChangedOverwriteTargetLabel.ForeColor = System.Drawing.Color.Blue
        Me.BothChangedOverwriteTargetLabel.Location = New System.Drawing.Point(206, 42)
        Me.BothChangedOverwriteTargetLabel.Name = "BothChangedOverwriteTargetLabel"
        Me.BothChangedOverwriteTargetLabel.Size = New System.Drawing.Size(64, 13)
        Me.BothChangedOverwriteTargetLabel.TabIndex = 19
        Me.BothChangedOverwriteTargetLabel.Tag = "6"
        Me.BothChangedOverwriteTargetLabel.Text = "XXX"
        Me.BothChangedOverwriteTargetLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LanguagePictureBox
        '
        Me.LanguagePictureBox.Location = New System.Drawing.Point(117, 268)
        Me.LanguagePictureBox.Name = "LanguagePictureBox"
        Me.LanguagePictureBox.Size = New System.Drawing.Size(30, 30)
        Me.LanguagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.LanguagePictureBox.TabIndex = 66
        Me.LanguagePictureBox.TabStop = False
        '
        'PasswordsButton
        '
        Me.PasswordsButton.Image = Global.GadecSuitcase.My.Resources.Resources.Address
        Me.PasswordsButton.Location = New System.Drawing.Point(12, 275)
        Me.PasswordsButton.Name = "PasswordsButton"
        Me.PasswordsButton.Size = New System.Drawing.Size(24, 23)
        Me.PasswordsButton.TabIndex = 54
        Me.PasswordsButton.UseVisualStyleBackColor = True
        '
        'ltChangelog
        '
        Me.ltChangelog.Image = Global.GadecSuitcase.My.Resources.Resources.Changelog
        Me.ltChangelog.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ltChangelog.Location = New System.Drawing.Point(12, 331)
        Me.ltChangelog.Name = "ltChangelog"
        Me.ltChangelog.Size = New System.Drawing.Size(135, 23)
        Me.ltChangelog.TabIndex = 47
        Me.ltChangelog.Text = "XXX"
        Me.ltChangelog.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ltChangelog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ltChangelog.UseVisualStyleBackColor = True
        '
        'SourceProgressBar
        '
        Me.SourceProgressBar.Location = New System.Drawing.Point(12, 239)
        Me.SourceProgressBar.Name = "SourceProgressBar"
        Me.SourceProgressBar.Size = New System.Drawing.Size(417, 2)
        Me.SourceProgressBar.TabIndex = 68
        '
        'TargetProgressBar
        '
        Me.TargetProgressBar.Location = New System.Drawing.Point(12, 260)
        Me.TargetProgressBar.Name = "TargetProgressBar"
        Me.TargetProgressBar.Size = New System.Drawing.Size(417, 2)
        Me.TargetProgressBar.TabIndex = 69
        '
        'Main
        '
        Me.AcceptButton = Me.ltSynchronize
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ltClose
        Me.ClientSize = New System.Drawing.Size(439, 368)
        Me.Controls.Add(Me.ltConflicts)
        Me.Controls.Add(Me.SummaryTextBox)
        Me.Controls.Add(Me.ltSynchronize)
        Me.Controls.Add(Me.TargetFolderLabel)
        Me.Controls.Add(Me.SourceFolderLabel)
        Me.Controls.Add(Me.SuitcaseListBox)
        Me.Controls.Add(Me.StoreListBox)
        Me.Controls.Add(Me.PasswordsButton)
        Me.Controls.Add(Me.LanguageComboBox)
        Me.Controls.Add(Me.ltChangelog)
        Me.Controls.Add(Me.ltState)
        Me.Controls.Add(Me.ltSuitcase)
        Me.Controls.Add(Me.ltStore)
        Me.Controls.Add(Me.ltClose)
        Me.Controls.Add(Me.TargetCountLabel)
        Me.Controls.Add(Me.SourceCountLabel)
        Me.Controls.Add(Me.SourceProgressBar)
        Me.Controls.Add(Me.TargetProgressBar)
        Me.Controls.Add(Me.LanguagePictureBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "XXX"
        Me.ltConflicts.ResumeLayout(False)
        Me.ltConflicts.PerformLayout()
        CType(Me.LanguagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ltClose As System.Windows.Forms.Button
    Friend WithEvents ltStore As System.Windows.Forms.Label
    Friend WithEvents ltSuitcase As System.Windows.Forms.Label
    Friend WithEvents ltState As System.Windows.Forms.Label
    Friend WithEvents ltChangelog As System.Windows.Forms.Button
    Friend WithEvents LanguageComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents PasswordsButton As System.Windows.Forms.Button
    Friend WithEvents StoreListBox As Windows.Forms.ListBox
    Friend WithEvents SuitcaseListBox As Windows.Forms.ListBox
    Friend WithEvents TargetFolderLabel As Label
    Friend WithEvents SourceFolderLabel As Label
    Friend WithEvents TargetCountLabel As Label
    Friend WithEvents SourceCountLabel As Label
    Friend WithEvents ltSynchronize As Button
    Friend WithEvents SummaryTextBox As TextBox
    Friend WithEvents ltConflicts As GroupBox
    Friend WithEvents ltConflictsDel As Label
    Friend WithEvents ltConflictsOld As Label
    Friend WithEvents ltConflictsBth As Label
    Friend WithEvents TargetDeletedDeleteSourceLabel As Label
    Friend WithEvents SourceDeletedDeleteTargetLabel As Label
    Friend WithEvents SourceGotOlderOverwriteSourceLabel As Label
    Friend WithEvents TargetGotOlderOverwriteTargetLabel As Label
    Friend WithEvents BothChangedOverwriteSourceLabel As Label
    Friend WithEvents BothChangedOverwriteTargetLabel As Label
    Friend WithEvents LanguagePictureBox As PictureBox
    Friend WithEvents SourceProgressBar As ProgressBar
    Friend WithEvents TargetProgressBar As ProgressBar
End Class
