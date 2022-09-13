'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="ProgressFilesEventArgs"/> provides data for a <see cref="Progress"/> provider.</para>
''' <para>Tracks the progress of reading files.</para>
''' </summary>
Public Class ProgressFilesEventArgs
    ''' <summary>
    ''' The stock to which the event belongs.
    ''' </summary>
    ''' <returns>The stockplace.</returns>
    Public Property Place As Stock
    ''' <summary>
    ''' Total size in bytes.
    ''' </summary>
    ''' <returns>Size in bytes.</returns>
    Public Property Maximum As Integer
    ''' <summary>
    ''' Size already read in bytes.
    ''' </summary>
    ''' <returns>Size in bytes.</returns>
    Public Property Value As Integer

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ProgressFilesEventArgs"/> with the specified properties.
    ''' <para><see cref="ProgressFilesEventArgs"/> provides data for a <see cref="Progress"/> provider.</para>
    ''' <para>Tracks the progress of read files.</para>
    ''' </summary>
    ''' <param name="place">The stock to which the event belongs.</param>
    ''' <param name="maximum">Total size in bytes.</param>
    ''' <param name="value">Size already read in bytes.</param>
    Sub New(place As Stock, maximum As Integer, value As Integer)
        _Place = place
        _Maximum = maximum
        _Value = value
    End Sub

End Class
