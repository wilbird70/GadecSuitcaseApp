'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="SynchronizeReportModel"/> contains data of a synchronized file.</para>
''' </summary>
Public Class SynchronizeReportModel
    ''' <summary>
    ''' The size of the action it contains.
    ''' </summary>
    ''' <returns>Size in bytes.</returns>
    Public Property ActionSize As Long
    ''' <summary>
    ''' The lastwritetime of the sourcefile.
    ''' </summary>
    ''' <returns>The lastwritetime.</returns>
    Public Property SourceTime As Date
    ''' <summary>
    ''' The lastwritetime of the targetfile.
    ''' </summary>
    ''' <returns>The lastwritetime.</returns>
    Public Property TargetTime As Date
    ''' <summary>
    ''' The string of the action that took place, eg. '-->' (copied to target) or '--X' (target deleted).
    ''' </summary>
    ''' <returns>The actionstring.</returns>
    Public Property ActionString As String
    ''' <summary>
    ''' The size of the file.
    ''' </summary>
    ''' <returns>Size in bytes.</returns>
    Public Property FileSize As Long = -1
    ''' <summary>
    ''' Text to display if action failed.
    ''' </summary>
    ''' <returns>The failtext.</returns>
    Public Property FailText As String

    ''' <summary>
    ''' Initializes a new instance of the <see cref="SynchronizeReportModel"/> with the three specified properties.
    ''' <para><see cref="SynchronizeReportModel"/> contains data of a synchronized file.</para>
    ''' </summary>
    ''' <param name="actionSize"></param>
    ''' <param name="sourceTime"></param>
    ''' <param name="targetTime"></param>
    Public Sub New(actionSize As Long, sourceTime As Date, targetTime As Date)
        _ActionSize = actionSize
        _SourceTime = sourceTime
        _TargetTime = targetTime
    End Sub

End Class
