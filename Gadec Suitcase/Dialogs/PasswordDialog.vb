'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="PasswordDialog"/> provides a dialog where the user can enter text.</para>
''' </summary>
Public Class PasswordDialog
    ''' <summary>
    ''' Has the text the user typed.
    ''' </summary>
    Private _text As String
    ''' <summary>
    ''' Has the value of which buton was clicked.
    ''' </summary>
    Private _button As Integer = vbCancel

    'form

    ''' <summary>
    ''' Initializes a new instance the <see cref="PasswordDialog"/>.
    ''' <para><see cref="PasswordDialog"/> provides a dialog where the user can enter text.</para>
    ''' </summary>
    ''' <param name="prompt">Message to prompt the user.</param>
    ''' <param name="defaultResponse">The default response when the user does not enter any text.</param>
    ''' <param name="character">The character to be displayed instead of the typed character.</param>
    Sub New(prompt As String, defaultResponse As String, passwordChar As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Me.Text = Registerizer.GetApplicationVersion()
        Me.Controls.ToList.ForEach(Sub(c) If c.Name.StartsWith("lt") Then c.Text = c.Name.Translate)
        _text = defaultResponse
        TextLabel.Text = prompt
        InputTextBox.Text = defaultResponse
        InputTextBox.PasswordChar = passwordChar
        Me.ShowDialog()
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the dialog box is activated.
    ''' <para>It selects all text in the textbox and set its focus.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Try
            InputTextBox.SelectionStart = 0
            InputTextBox.SelectionLength = 100
            InputTextBox.Focus()
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    'functions

    ''' <summary>
    ''' Gets the value of which button was clicked.
    ''' </summary>
    ''' <returns>The button value.</returns>
    Function GetButton() As Integer
        'op kunnen vragen, welke van de knoppen is gebruikt om het venster te sluiten
        Return _button
    End Function

    ''' <summary>
    ''' Gets the text the user typed.
    ''' </summary>
    ''' <returns>The textstring.</returns>
    Function GetText() As String
        Return _text
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
            _text = InputTextBox.Text
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