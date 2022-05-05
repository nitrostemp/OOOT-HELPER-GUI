# choose installation path
Page directory /ENABLECANCEL
Page instfiles
 
# define name of installer
OutFile "Open Ocarina Helper GUI.exe"
 
# define installation directory
InstallDir "$PROGRAMFILES\Open Ocarina Helper GUI\"
 
# For removing Start Menu shortcut in Windows 7
RequestExecutionLevel admin

# start default section
Section
		
    # set the installation directory as the destination for the following actions
    SetOutPath $INSTDIR
	
    File /r "OOOT GUI\bin\Release\*.*"
	
    # create the uninstaller
    WriteUninstaller "$INSTDIR\uninstall.exe"
 
    # create a shortcut in the start menu programs directory
    CreateShortcut "$desktop\Open Ocarina Helper GUI.lnk" "$INSTDIR\OOOT GUI.exe"

SectionEnd
 
# uninstaller section start
Section "uninstall"
 
    # Remove the link from the start menu
    Delete "$SMPROGRAMS\RemoveOOTGUI.lnk"
	
    # Remove the link from the start menu
    Delete "$SMPROGRAMS\Open Ocarina Helper GUI.lnk"
 
    # Delete the uninstaller
    Delete $INSTDIR\uninstall.exe
	 
    RMDir /r $INSTDIR
	
# uninstaller section end
SectionEnd