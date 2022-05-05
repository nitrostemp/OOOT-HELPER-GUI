
# define name of installer
OutFile "Open Ocarina Helper GUI.exe"
 
# define installation directory
InstallDir "$PROGRAMFILES\Open Ocarina Helper GUI\Release"
 
# For removing Start Menu shortcut in Windows 7
RequestExecutionLevel admin
# start default section
Section
	
    # set the installation directory as the destination for the following actions
    SetOutPath $INSTDIR
	File /r "OOOT GUI\bin\Release\*.*"
    # create the uninstaller
    WriteUninstaller "$INSTDIR\uninstall.exe"
 
    # create a shortcut named "new shortcut" in the start menu programs directory
    # point the new shortcut at the program uninstaller

    # point the new shortcut at the program uninstaller

    CreateShortcut "$desktop\Open Ocarina Helper GUI.lnk" "$INSTDIR\OOOT GUI.exe"

SectionEnd
 
# uninstaller section start
Section "uninstall"
 
    # Remove the link from the start menu
    Delete "$SMPROGRAMS\RemoveOOTGUI.lnk"
# Remove the link from the start menu
    Delete "$SMPROGRAMS\Open Ocarina Helper GUI.lnk"
 
    # Delete the uninstaller
    Delete $INSTDIR\uninstaller.exe
 
    RMDir $INSTDIR
# uninstaller section end
SectionEnd