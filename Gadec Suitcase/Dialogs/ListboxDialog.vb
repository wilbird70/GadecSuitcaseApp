'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="ListboxDialog"/> provides a dialog where the user can select an option from a list.</para>
''' </summary>
Public Class ListboxDialog
    ''' <summary>
    ''' Has the value of which buton was clicked.
    ''' </summary>
    Private _button As Integer = 0
    ''' <summary>
    ''' Determine whether the dialogbox resizing is in progress.
    ''' </summary>
    Private _resizingBusy As Boolean

    'form

    ''' <summary>
    ''' Initializes a new instance the <see cref="ListboxDialog"/>.
    ''' <para><see cref="ListboxDialog"/> provides a dialog where the user can select an option from a list.</para>
    ''' </summary>
    ''' <param name="prompt">Message to prompt the user.</param>
    ''' <param name="items">List of items to show in the listbox.</param>
    ''' <param name="selectedItems">List of previously selected items.</param>
    Sub New(prompt As String, items As String(), selectedItems As String())
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        If IsNothing(selectedItems) Then selectedItems = {}

        Me.Text = Registerizer.GetApplicationVersion()
        Translator.TranslateControles(Me)

        TextLabel.Text = prompt
        InputListBox.Items.AddRange(items.ToArray)
        For i = 0 To InputListBox.Items.Count - 1
            If selectedItems.Contains(InputListBox.Items(i)) Then InputListBox.SetSelected(i, True)
        Next

        Me.ShowDialog()
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user resizes this dialog box.
    ''' <para>It also changes the size and location of its controls.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_ClientSizeChanged(sender As Object, e As EventArgs) Handles Me.ClientSizeChanged
        Try
            If _resizingBusy Then Exit Sub

            _resizingBusy = True
            InputListBox.Width = Me.Width - 40
            ltOK.Left = Me.Width - 184
            ltCancel.Left = Me.Width - 103
            InputListBox.Height = Me.Height - 102
            ltOK.Top = Me.Height - 71
            ltCancel.Top = Me.Height - 71
            Me.Refresh()
            _resizingBusy = False
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    'functions

    ''' <summary>
    ''' Gets the list of currently selected items.
    ''' </summary>
    ''' <returns>List of items.</returns>
    Function GetSelectedItems() As List(Of String)
        Return InputListBox.SelectedItems.Cast(Of String)().ToList
    End Function

    ''' <summary>
    ''' Gets the value of which button was clicked.
    ''' </summary>
    ''' <returns>The button value.</returns>
    Function GetButton() As Integer
        Return _button
    End Function

    'buttons

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the OK button.
    ''' <para>It sets the buttonvalue and closes the dialogbox.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AcceptButton_Click(sender As Object, e As EventArgs) Handles ltOK.Click
        Try
            _button = vbOK
            Me.Hide()
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the Cancel button.
    ''' <para>It sets the buttonvalue and closes the dialogbox.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles ltCancel.Click
        Try
            _button = vbCancel
            Me.Hide()
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

End Class