''Gadec Engineerings Software (c) 2020

''' <summary>
''' <para>A <see cref="Storage"/> can contain multiple <see cref="Suitcase"/>s stored in an xml-file in the appdata-folder.</para>
''' </summary>
Public Class Storage
    ''' <summary>
    ''' The name of the present <see cref="Storage"/>.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String

    ''' <summary>
    ''' The dataset containing the databases of the <see cref="Suitcase"/>s.
    ''' </summary>
    Private _storage As DataSet
    ''' <summary>
    ''' The fullname of the xml-file.
    ''' </summary>
    Private _fileName As String

    'class

    ''' <summary>
    ''' Initializes a new instance of <see cref="Storage"/> with the data stored in the specified xml-file.
    ''' <para>A <see cref="Storage"/> can contain multiple <see cref="Suitcase"/>s stored in an xml-file in the appdata-folder.</para>
    ''' </summary>
    ''' <param name="fileName">The fullname of the file.</param>
    Public Sub New(fileName As String)
        _fileName = fileName
        Select Case IO.File.Exists(fileName)
            Case True : _storage = DataSetHelper.LoadFromXml(fileName)
            Case Else : _storage = New DataSet(IO.Path.GetFileNameWithoutExtension(fileName)) : Me.Save()
        End Select
        _storage.ExtendedProperties("FileName") = fileName
        _Name = _storage.DataSetName
    End Sub

    'subs

    ''' <summary>
    ''' Saves the present data of the <see cref="Storage"/> in the xml-file.
    ''' </summary>
    Public Sub Save()
        If NotNothing(_storage) Then _storage.WriteXml(_fileName, XmlWriteMode.WriteSchema)
    End Sub

    ''' <summary>
    ''' Renames the filename of the present <see cref="Storage"/>.
    ''' </summary>
    ''' <param name="newFileName">The fullname to be renamed to.</param>
    Public Sub Rename(newFileName As String)
        If IsNothing(_storage) OrElse _fileName = newFileName OrElse IO.File.Exists(newFileName) Then Exit Sub

        If IO.File.Exists(_fileName) Then IO.File.Delete(_fileName)
        _storage.DataSetName = IO.Path.GetFileNameWithoutExtension(newFileName)
        _fileName = newFileName
        Me.Save()
    End Sub

    ''' <summary>
    ''' Deletes the xml-file of the present <see cref="Storage"/>.
    ''' </summary>
    Public Sub Delete()
        If _fileName = "" OrElse Not IO.File.Exists(_fileName) Then Exit Sub

        IO.File.Delete(_fileName)
        _storage = Nothing
        _fileName = ""
    End Sub

    ''' <summary>
    ''' Adds a new <see cref="Suitcase"/> to the <see cref="Storage"/>.
    ''' </summary>
    ''' <param name="newSuitcaseName">The new <see cref="Suitcase"/> name.</param>
    ''' <param name="sourceFolder">The path of the source-folder.</param>
    ''' <param name="targetFolder">The path of the target-folder.</param>
    Public Sub Add(newSuitcaseName As String, sourceFolder As String, targetFolder As String)
        If _storage.Tables.Contains(newSuitcaseName) Or newSuitcaseName = "" Then Exit Sub

        If sourceFolder.Length = 3 Or Not IO.Directory.Exists(sourceFolder) Then Exit Sub

        If targetFolder.Length = 3 Or Not IO.Directory.Exists(targetFolder) Then Exit Sub

        Dim suitcase = DataTableHelper.Create(newSuitcaseName, "File;Date".Cut)
        suitcase.AssignPrimaryKey("File")
        suitcase.ExtendedProperties.Add("SourceFolder", sourceFolder)
        suitcase.ExtendedProperties.Add("TargetFolder", targetFolder)
        _storage.Tables.Add(suitcase)
        Me.Save()
    End Sub

    'functions

    ''' <summary>
    ''' Gets the names of suitcases stored in the present <see cref="Storage"/>.
    ''' </summary>
    ''' <returns>A list of suitcase names.</returns>
    Public Function GetSuitcaseNames() As String()
        Return If(IsNothing(_storage), {}, _storage.GetTableNames)
    End Function

    ''' <summary>
    ''' Determines if the specified <see cref="Suitcase"/> is in the present <see cref="Storage"/>.
    ''' </summary>
    ''' <param name="suitcaseName">The name of the <see cref="Suitcase"/>.</param>
    ''' <returns>True if exists.</returns>
    Public Function Contains(suitcaseName As String) As Boolean
        Return _storage.Tables.Contains(suitcaseName)
    End Function

    ''' <summary>
    ''' Gets a <see cref="Suitcase"/> out of the <see cref="Storage"/>.
    ''' </summary>
    ''' <param name="suitcaseName">The name of the <see cref="Suitcase"/>.</param>
    ''' <returns>The selected <see cref="Suitcase"/>.</returns>
    Public Function GetSuitcase(suitcaseName As String) As Suitcase
        Dim selectedSuitcaseData = _storage.GetTable(suitcaseName, "File")
        If IsNothing(selectedSuitcaseData) Then Return Nothing

        Return New Suitcase(selectedSuitcaseData)
    End Function

End Class
