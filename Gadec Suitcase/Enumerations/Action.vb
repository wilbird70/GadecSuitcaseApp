'Gadec Engineerings Software (c) 2020

''' <summary>
''' A type to indicate what the preferred action should be.
''' </summary>
Public Enum Action
    DoNothing = 0
    DeleteSource = 1
    DeleteTarget = 2
    BecameOlderCopyTarget = 3
    BecameOlderCopySource = 4
    BothChangedCopyTarget = 5
    BothChangedCopySource = 6
    CopySource = 7
    CopyTarget = 8
    DeleteSuitcase = 9
    UpdateSuitcase = 10
    SaveSource = 11
    SaveTarget = 12
    BecameOlderKeepSource = 13
    BecameOlderKeepTarget = 14
    BothChangedKeepSource = 15
    BothChangedKeepTarget = 16
End Enum
