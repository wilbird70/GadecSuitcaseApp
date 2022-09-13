'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="FileModel"/> contains info of a file.</para>
''' </summary>
Public Class FileModel
    ''' <summary>
    ''' The name of the file.
    ''' </summary>
    ''' <returns>The filename.</returns>
    Public Property FileName As String
    ''' <summary>
    ''' The lastwitetime of the file.
    ''' </summary>
    ''' <returns>The lastwitetime.</returns>
    Public Property LastWriteTime As Date
    ''' <summary>
    ''' The size of the file.
    ''' </summary>
    ''' <returns>Size in bytes.</returns>
    Public Property Size As Long

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ActionModel"/> with the specified properties.
    ''' <para><see cref="FileModel"/> contains info of a file.</para>
    ''' </summary>
    ''' <param name="fileName">The name of the file.</param>
    ''' <param name="time">The lastwitetime of the file.</param>
    ''' <param name="size"> The size of the file.</param>
    Public Sub New(fileName As String, lastWriteTime As Date, Size As Long)
        _FileName = fileName
        _LastWriteTime = lastWriteTime
        _Size = Size
    End Sub

End Class
