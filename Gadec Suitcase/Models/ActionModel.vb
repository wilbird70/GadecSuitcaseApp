'Gadec Engineerings Software (c) 2020

''' <summary>
''' <para><see cref="ActionModel"/> contains data of the preferred action for the file.</para>
''' </summary>
Public Class ActionModel
    ''' <summary>
    ''' The preferred action.
    ''' </summary>
    ''' <returns>The action.</returns>
    Public Property Action As Action
    ''' <summary>
    ''' The size of the file in bytes.
    ''' </summary>
    ''' <returns>Size in bytes.</returns>
    Public Property Size As Long

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ActionModel"/> with the specified properties.
    ''' <para><see cref="ActionModel"/> contains data of the preferred action for a file.</para>
    ''' </summary>
    ''' <param name="action">The preferred action.</param>
    ''' <param name="size">The size of the file in bytes.</param>
    Public Sub New(action As Action, size As Long)
        _Action = action
        _Size = size
    End Sub

End Class
