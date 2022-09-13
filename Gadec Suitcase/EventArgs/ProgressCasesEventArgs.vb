'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="ProgressCasesEventArgs"/> provides data for a <see cref="Progress"/> provider.</para>
''' <para>Tracks the progress of treated cases.</para>
''' </summary>
Public Class ProgressCasesEventArgs
    ''' <summary>
    ''' The stock to which the event belongs.
    ''' </summary>
    ''' <returns>The <see cref="Stock"/></returns>
    Public Property Place As Stock
    ''' <summary>
    ''' The name of the folder just read.
    ''' </summary>
    ''' <returns>The foldername.</returns>
    Public Property FolderName As String
    ''' <summary>
    ''' The number of files in the (sub-) folders.
    ''' </summary>
    ''' <returns>The number of files.</returns>
    Public Property FileCount As Integer

    ''' <summary>
    ''' Initializes a new instance of <see cref="ProgressCasesEventArgs"/> with the specified properties.
    ''' <para><see cref="ProgressCasesEventArgs"/> provides data for a <see cref="Progress"/> provider.</para>
    ''' <para>Tracks the progress of the suitcases handled.</para>
    ''' </summary>
    ''' <param name="place">The stock to which the event belongs.</param>
    ''' <param name="folderName">The name of the folder just read.</param>
    ''' <param name="fileCount">The number of files in the (sub-) folders.</param>
    Sub New(place As Stock, folderName As String, fileCount As Integer)
        _Place = place
        _FolderName = folderName
        _FileCount = fileCount
    End Sub

End Class
