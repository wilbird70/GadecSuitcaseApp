'Gadec Engineerings Software (c) 2020

''' <summary>
''' Provides general methods.
''' <para></para>
''' </summary>
Module Functions

    ''' <summary>
    ''' The main exception method for routing to the detailed exception dialogbox.
    ''' </summary>
    ''' <param name="exception">The exception to show.</param>
    Public Sub GadecException(exception As Exception)
        Dim assembly = Reflection.Assembly.GetExecutingAssembly
        Dim appName = "{0} {1}v{2}".Compose(assembly.GetName.Name, assembly.GetName.Version.Major, assembly.GetName.Version.Minor)
        Dim appBuild = Format(IO.File.GetLastWriteTime(assembly.Location), "dd-MM-yyyy - HH:mm:ss")
        MessageBoxException(exception, appName, appBuild, 1)
    End Sub

End Module
