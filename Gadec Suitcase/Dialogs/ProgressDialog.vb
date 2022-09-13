'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="ProgressDialog"/> provides a dialog box for displaying the sync progress.</para>
''' </summary>
Public Class ProgressDialog
    Private _isResizing As Boolean

    'form

    ''' <summary>
    ''' Initializes a new instance the <see cref="ProgressDialog"/>.
    ''' <para><see cref="ProgressDialog"/> provides a dialog box for displaying the sync progress.</para>
    ''' </summary>
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Text = Registerizer.GetApplicationVersion()
        OutputProgressBar.Minimum = 0
        OutputProgressBar.Maximum = 100
        OutputProgressBar.Value = 0
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user resizes this dialog box.
    ''' <para>It also changes the size and location of its controls.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_ClientSizeChanged(sender As Object, e As EventArgs) Handles Me.ClientSizeChanged
        Try
            If _isResizing Then Exit Sub

            _isResizing = True
            OutputTextBox.Width = Me.Width - 40
            OutputTextBox.Height = Me.Height - 78
            Me.Refresh()
            _isResizing = False
        Catch ex As Exception
            GadecException(ex)
        End Try
    End Sub

    'subs

    ''' <summary>
    ''' Adds a step to the progress.
    ''' </summary>
    ''' <param name="text">line of text to add in the textbox.</param>
    ''' <param name="percentageDone">Progress in percentages.</param>
    Sub NextStep(text As String, percentageDone As Integer)
        If Not text = "" Then OutputTextBox.AppendText("{0}{CL}".Compose(text))
        Select Case percentageDone
            Case > 100 : OutputProgressBar.Value = 100
            Case < 0 : OutputProgressBar.Value = 0
            Case Else : OutputProgressBar.Value = percentageDone
        End Select
        OutputProgressBar.Refresh()
    End Sub

    'buttons

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the Close button.
    ''' <para>It closes the dialogbox.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ltClose_Click(sender As Object, e As EventArgs) Handles ltClose.Click
        Me.Hide()
        Me.Dispose()
    End Sub

End Class