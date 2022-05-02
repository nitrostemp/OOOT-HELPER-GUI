' VBScript to created shortcut
Const strProgramTitle = "Shortcut to OOOT"
Const strProgram =  "%USERPROFILE%\ooot\vs\Release\OOT.exe"
Const strWorkDir = "%USERPROFILE%"
Dim objShortcut, objShell
Set objShell = WScript.CreateObject ("Wscript.Shell")
strLPath = objShell.SpecialFolders ("Desktop")
Set objShortcut = objShell.CreateShortcut (strLPath & "\" & strProgramTitle & ".lnk")
objShortcut.TargetPath = strProgram
objShortcut.WorkingDirectory = strWorkDir
objShortcut.Description = strProgramTitle
objShortcut.Save
WScript.Quit

