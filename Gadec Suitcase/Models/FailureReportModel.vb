'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="FailureReportModel"/> contains the filenames of files where copying or deleting failed.</para>
''' </summary>
Public Class FailureReportModel

    ''' <summary>
    ''' Gets the list of shortened filenames where the copy operation failed.
    ''' </summary>
    ''' <returns>A list of filenames.</returns>
    Public Property FailedToCopy As New List(Of String)
    ''' <summary>
    ''' Gets the list of shortened filenames where the delete operation failed.
    ''' </summary>
    ''' <returns>A list of filenames.</returns>
    Public Property FailedToDelete As New List(Of String)

End Class
