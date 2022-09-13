'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="CryptoDialog"/> provides a dialog for reading and writing passwords in an encrypted file.</para>
''' </summary>
Public Class CryptoDialog
    ''' <summary>
    ''' The filename of the encrypted file.
    ''' </summary>
    Private ReadOnly _fileName As String = "{AppDataFolder}\CryptoBox.wrp".Compose
    ''' <summary>
    ''' The filename of a csv-export-file.
    ''' </summary>
    Private ReadOnly _exportFile As String = "{AppDataFolder}\CryptoBox.csv".Compose
    ''' <summary>
    ''' The password to decrypt and encrypt.
    ''' </summary>
    Private _password As String
    ''' <summary>
    ''' List of all decrypted data.
    ''' </summary>
    Private _rowStrings As List(Of String)
    ''' <summary>
    ''' The current selected row.
    ''' </summary>
    Private _selectedRow As Integer = -1
    ''' <summary>
    ''' Determine if the data has changed.
    ''' </summary>
    Private _isDirty As Boolean = False

    'form

    ''' <summary>
    ''' Initializes a new instance the <see cref="CryptoDialog"/>.
    ''' <para><see cref="CryptoDialog"/> provides a dialog for reading and writing passwords in an encrypted file.</para>
    ''' </summary>
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Me.Text = Registerizer.GetApplicationVersion()
        Me.Controls.ToList.ForEach(Sub(c) If c.Name.StartsWith("lt") Then c.Text = c.Name.Translate)

        Me.ShowDialog()
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the dialogbox is loading.
    ''' <para>It asks the user for his password to read the encrypted file.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim dialog = New PasswordDialog("Geef wachtwoord:", "", "*")
            Dim password = dialog.GetText
            If password = "" Then
                ltOK.Enabled = False
                NewItemButton.Enabled = False
                DeleteItemButton.Enabled = False
                Exit Sub
            End If

            _rowStrings = New List(Of String)
            Dim cryptoWrapper = New CryptoWrapper(password)
            Dim wrapLines = IO.File.ReadAllLines(_fileName)
            Try
                For Each line In wrapLines
                    _rowStrings.Add(cryptoWrapper.DecryptData(line))
                Next
                _password = password
                ReListPasswords()
            Catch ex As Exception
                ltOK.Enabled = False
                NewItemButton.Enabled = False
                DeleteItemButton.Enabled = False
                MsgBox("Wachtwoord niet juist!")
            End Try
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user resizes this dialog box.
    ''' <para>It also changes the size and location of its controls.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Try
            ResizeDialog()
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks on this dialog box.
    ''' <para>It causes the OK button to appear.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_Click(sender As Object, e As EventArgs) Handles Me.Click
        Try
            ltOK.Visible = True
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    'buttons

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the OK button.
    ''' <para>It saves the passwords to an encrypted file.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AcceptButton_Click(sender As Object, e As EventArgs) Handles ltOK.Click
        Try
            SavePasswords()
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the cancel button.
    ''' <para>It checks if data has been changed to prompt the user to save it if necessary.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles ltCancel.Click
        Try
            If Not _isDirty Then Me.Hide() : Exit Sub

            Dim messageResult = MsgBox("Wilt u de wijzigingen opslaan voor het afsluiten?", MsgBoxStyle.YesNoCancel)
            Select Case messageResult
                Case MsgBoxResult.Yes : SavePasswords()
                Case MsgBoxResult.No : Me.Hide()
                Case MsgBoxResult.Cancel
            End Select
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the update button.
    ''' <para>It updates the data (passwords, etc.) in the datagridview.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles ltUpdate.Click
        Try
            Dim line = "{0};{1};{2};{3};{4};{5};".Compose(NameTextBox.Text, CategoryTextBox.Text, UserNameTextBox.Text, PasswordTextBox.Text, WebsiteTextBox.Text, CommentTextBox.Text)
            If Not _rowStrings(_selectedRow) = line Then
                _rowStrings(_selectedRow) = line
                _isDirty = True
            End If
            NameTextBox.Visible = False
            CategoryTextBox.Visible = False
            UserNameTextBox.Visible = False
            PasswordTextBox.Visible = False
            WebsiteTextBox.Visible = False
            CommentTextBox.Visible = False
            ltUpdate.Visible = False
            NewItemButton.Visible = True
            DeleteItemButton.Visible = True
            UpButton.Visible = True
            DownButton.Visible = True
            ltOK.Visible = True
            ltCancel.Visible = True
            ltExport.Visible = True
            Dim rowIndex = PasswordDataGridView.FirstDisplayedScrollingRowIndex
            ReListPasswords()
            PasswordDataGridView.FirstDisplayedScrollingRowIndex = rowIndex
            PasswordDataGridView.CurrentCell = PasswordDataGridView.Item(0, _selectedRow)
            _selectedRow = -1
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the export button.
    ''' <para>It exports all data to a csv-file without encryption.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ExportButton_Click(sender As Object, e As EventArgs) Handles ltExport.Click
        Try
            Dim passwordData = DirectCast(PasswordDataGridView.DataSource, DataTable)
            Dim exportLines = New List(Of String) From {"{Q}{0}{Q};{Q}{1}{Q};{Q}{2}{Q};{Q}{3}{Q};{Q}{4}{Q};{Q}{5}{Q}".Compose("Naam", "Categorie", "Gebruikersnaam", "Wachtwoord", "Website", "Opmerkingen")}
            For Each passwordRow In passwordData.Rows.ToArray
                exportLines.Add("{Q}{0}{Q};{Q}{1}{Q};{Q}{2}{Q};{Q}{3}{Q};{Q}{4}{Q};{Q}{5}{Q}".Compose(passwordRow(1), passwordRow(2), passwordRow(3), passwordRow(4), passwordRow(5), passwordRow(6)))
            Next
            IO.File.WriteAllLines(_exportFile, exportLines.ToArray)
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the new button.
    ''' <para>It creates a new row for password data.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub NewItemButton_Click(sender As Object, e As EventArgs) Handles NewItemButton.Click
        Try
            If _selectedRow = -1 Then
                _selectedRow = PasswordDataGridView.CurrentRow.Index
                _rowStrings.Insert(_selectedRow, ";;;;;;;;;;")
                _isDirty = True
                Dim rowIndex = PasswordDataGridView.FirstDisplayedScrollingRowIndex
                ReListPasswords()
                PasswordDataGridView.FirstDisplayedScrollingRowIndex = rowIndex
                PasswordDataGridView.CurrentCell = PasswordDataGridView.Item(0, _selectedRow)
                _selectedRow = -1
            End If
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the delete button.
    ''' <para>It deletes a row of password data.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DeleteItemButton_Click(sender As Object, e As EventArgs) Handles DeleteItemButton.Click
        Try
            If _selectedRow = -1 Then
                _selectedRow = PasswordDataGridView.CurrentRow.Index
                _rowStrings.RemoveAt(_selectedRow)
                _isDirty = True
                Dim rowIndex = PasswordDataGridView.FirstDisplayedScrollingRowIndex
                ReListPasswords()
                Do While rowIndex > PasswordDataGridView.Rows.Count - 1
                    rowIndex -= 1
                Loop
                Do While _selectedRow > PasswordDataGridView.Rows.Count - 1
                    _selectedRow -= 1
                Loop
                PasswordDataGridView.FirstDisplayedScrollingRowIndex = rowIndex
                PasswordDataGridView.CurrentCell = PasswordDataGridView.Item(0, _selectedRow)
                _selectedRow = -1
            End If
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the up button.
    ''' <para>Shifts up a row of password data.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub UpButton_Click(sender As Object, e As EventArgs) Handles UpButton.Click
        Try
            If Not _selectedRow = -1 Then _selectedRow = -1 : Exit Sub

            _selectedRow = PasswordDataGridView.CurrentRow.Index
            If Not _selectedRow > 1 Then _selectedRow = -1 : Exit Sub

            Dim temporaryLine = _rowStrings(_selectedRow)
            _rowStrings(_selectedRow) = _rowStrings(_selectedRow - 1)
            _rowStrings(_selectedRow - 1) = temporaryLine
            _isDirty = True
            Dim rowIndex = PasswordDataGridView.FirstDisplayedScrollingRowIndex
            ReListPasswords()
            PasswordDataGridView.FirstDisplayedScrollingRowIndex = rowIndex
            PasswordDataGridView.CurrentCell = PasswordDataGridView.Item(0, _selectedRow - 1)
            _selectedRow = -1
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the down button.
    ''' <para>Shifts down a row of password data.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DownButton_Click(sender As Object, e As EventArgs) Handles DownButton.Click
        Try
            If Not _selectedRow = -1 Then _selectedRow = -1 : Exit Sub

            _selectedRow = PasswordDataGridView.CurrentRow.Index
            If Not _selectedRow < PasswordDataGridView.Rows.Count - 1 Then _selectedRow = -1 : Exit Sub

            Dim temporaryLine = _rowStrings(_selectedRow)
            _rowStrings(_selectedRow) = _rowStrings(_selectedRow + 1)
            _rowStrings(_selectedRow + 1) = temporaryLine
            _isDirty = True
            Dim rowIndex = PasswordDataGridView.FirstDisplayedScrollingRowIndex
            ReListPasswords()
            PasswordDataGridView.FirstDisplayedScrollingRowIndex = rowIndex
            PasswordDataGridView.CurrentCell = PasswordDataGridView.Item(0, _selectedRow + 1)
            _selectedRow = -1
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    'datagridviews

    ''' <summary>
    ''' EventHandler for the event that occurs when the user doubleclicks the datagridview.
    ''' <para>Allows the user to change the data in that row.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PasswordsDataGridView_DoubleClick(sender As Object, e As EventArgs) Handles PasswordDataGridView.DoubleClick
        Try
            If Not _selectedRow = -1 Then Exit Sub

            _selectedRow = PasswordDataGridView.CurrentRow.Index
            Dim lineValues = _rowStrings(_selectedRow).Cut
            NameTextBox.Text = lineValues(0)
            CategoryTextBox.Text = lineValues(1)
            UserNameTextBox.Text = lineValues(2)
            PasswordTextBox.Text = lineValues(3)
            WebsiteTextBox.Text = lineValues(4)
            CommentTextBox.Text = lineValues(5)
            NameTextBox.Visible = True
            CategoryTextBox.Visible = True
            UserNameTextBox.Visible = True
            PasswordTextBox.Visible = True
            WebsiteTextBox.Visible = True
            CommentTextBox.Visible = True
            ltUpdate.Visible = True
            NewItemButton.Visible = False
            DeleteItemButton.Visible = False
            UpButton.Visible = False
            DownButton.Visible = False
            ltOK.Visible = False
            ltCancel.Visible = False
            ltExport.Visible = False
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks on a cellcontent.
    ''' <para>It opens a browser to view the linked website.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PasswordsDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles PasswordDataGridView.CellContentClick
        Try
            If TypeOf PasswordDataGridView.Item(e.ColumnIndex, e.RowIndex) IsNot DataGridViewLinkCell OrElse e.RowIndex = -1 Then Exit Sub

            Process.Start("https://{0}".Compose(PasswordDataGridView.Item(e.ColumnIndex, e.RowIndex).Value.ToString()))
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    'private subs

    ''' <summary>
    ''' (Re)list the datagridview with the read or updated password data.
    ''' </summary>
    Private Sub ReListPasswords()
        'opmaken van tabel
        Dim passwordData = New DataTable()
        passwordData.Columns.Add("Row", GetType(String))
        passwordData.Columns.Add("Name", GetType(String))
        passwordData.Columns.Add("Category", GetType(String))
        passwordData.Columns.Add("Username", GetType(String))
        passwordData.Columns.Add("Password", GetType(String))
        passwordData.Columns.Add("Website", GetType(String))
        passwordData.Columns.Add("Comments", GetType(String))
        For Each column As DataColumn In passwordData.Columns
            If Not column.ColumnName.Translate = "XXX" Then column.ColumnName = column.ColumnName.Translate
        Next
        For i = 0 To _rowStrings.Count - 1
            Dim lineValues = _rowStrings(i).Cut
            If lineValues.Count > 5 Then
                Dim number = CStr(i + 1)
                Dim account = lineValues(0)
                Dim category = lineValues(1)
                Dim userName = lineValues(2)
                Dim password = lineValues(3)
                Dim website = lineValues(4)
                Dim comments = lineValues(5)
                passwordData.Rows.Add(number, account, category, userName, password, website, comments)
            End If
        Next
        PasswordDataGridView.DataSource = passwordData
        For Each row In PasswordDataGridView.Rows.ToArray
            Dim hyperLink = row.Cells(5).Value
            If Not hyperLink = "" Then row.Cells(5) = New DataGridViewLinkCell With {.Value = hyperLink, .UseColumnTextForLinkValue = True}
        Next
        ResizeDialog()
    End Sub

    ''' <summary>
    ''' The method for changing the size and location of the controls, along with the size of the dialog box.
    ''' </summary>
    Private Sub ResizeDialog()
        If Me.Width < 313 Then Me.Width = 313
        If Me.Height < 200 Then Me.Height = 200
        PasswordDataGridView.Width = Me.Width - 72
        PasswordDataGridView.Height = Me.Height - 116
        If PasswordDataGridView.Columns.Count > 4 Then
            PasswordDataGridView.Columns(0).Width = PasswordDataGridView.Width * 0.05
            PasswordDataGridView.Columns(1).Width = PasswordDataGridView.Width * 0.15
            PasswordDataGridView.Columns(2).Width = PasswordDataGridView.Width * 0.15
            PasswordDataGridView.Columns(3).Width = PasswordDataGridView.Width * 0.25
            PasswordDataGridView.Columns(4).Width = PasswordDataGridView.Width * 0.15
            PasswordDataGridView.Columns(5).Width = PasswordDataGridView.Width * 0.2
            PasswordDataGridView.Columns(6).Visible = False
        End If
        Dim width = PasswordDataGridView.Width - 30
        NameTextBox.Top = Me.Height - 98
        NameTextBox.Left = 12
        NameTextBox.Width = width * 0.15
        CategoryTextBox.Top = NameTextBox.Top
        CategoryTextBox.Left = NameTextBox.Left + NameTextBox.Width + 6
        CategoryTextBox.Width = width * 0.15
        UserNameTextBox.Top = NameTextBox.Top
        UserNameTextBox.Left = CategoryTextBox.Left + CategoryTextBox.Width + 6
        UserNameTextBox.Width = width * 0.2
        PasswordTextBox.Top = NameTextBox.Top
        PasswordTextBox.Left = UserNameTextBox.Left + UserNameTextBox.Width + 6
        PasswordTextBox.Width = width * 0.15
        WebsiteTextBox.Top = NameTextBox.Top
        WebsiteTextBox.Left = PasswordTextBox.Left + PasswordTextBox.Width + 6
        WebsiteTextBox.Width = width * 0.25
        CommentTextBox.Top = NameTextBox.Top
        CommentTextBox.Left = WebsiteTextBox.Left + WebsiteTextBox.Width + 6
        CommentTextBox.Width = width * 0.1

        ltOK.Top = Me.Height - 72
        ltCancel.Top = Me.Height - 72
        ltUpdate.Top = Me.Height - 72
        ltExport.Top = Me.Height - 72
        ltCancel.Left = Me.Width - 135
        ltOK.Left = Me.Width - 234
        ltUpdate.Left = Me.Width - 333
        ltExport.Left = Me.Width - 432

        DeleteItemButton.Top = Me.Height - 128
        NewItemButton.Top = Me.Height - 160
        DownButton.Top = Me.Height - 192
        UpButton.Top = Me.Height - 224

        NewItemButton.Left = Me.Width - 48
        DeleteItemButton.Left = Me.Width - 48
        UpButton.Left = Me.Width - 48
        DownButton.Left = Me.Width - 48
    End Sub

    ''' <summary>
    ''' The method for saving the password data to an encrypted file.
    ''' </summary>
    Private Sub SavePasswords()
        Dim wrapLines = New List(Of String)
        Dim dialog1 = New PasswordDialog("Geef nieuw wachtwoord:", _password, "*")
        If Not dialog1.GetButton = vbOK OrElse dialog1.GetText.Length < 3 Then Exit Sub

        If Not dialog1.GetText = _password Then
            Dim dialog2 = New PasswordDialog("Geef nogmaals wachtwoord:", "", "*")
            If Not dialog1.GetText = dialog2.GetText Then
                MsgBox("Wachtwoorden niet gelijk aan elkaar", MsgBoxStyle.Exclamation)
                Exit Sub

            End If
        End If

        Dim cryptoWrapper = New CryptoWrapper(dialog1.GetText)
        For Each rowString In _rowStrings
            If rowString.Length > 1 Then wrapLines.Add(cryptoWrapper.EncryptData(rowString))
        Next
        IO.File.WriteAllLines(_fileName, wrapLines.ToArray)
        Me.Hide()
    End Sub

End Class
