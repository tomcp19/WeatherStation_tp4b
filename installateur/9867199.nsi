!include "MUI2.nsh"
!define MUI_ABORTWARNING
!define APP_NAME "WeatherApp"
!define MUI_ICON "C:\Users\user\OneDrive\Desktop\Ecole\A2020-SujetsSpeciaux\installateur\fichiers\nuage.ico"

NAME "${APP_NAME}"
OutFile "install.exe"
InstallDir "$Profile\9867199"
InstallDirRegKey HKCU "Software\${APP_NAME}" ""

RequestExecutionLevel user

!insertmacro MUI_PAGE_LICENSE "C:\Users\user\OneDrive\Desktop\Ecole\A2020-SujetsSpeciaux\installateur\fichiers\license.txt"
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_LANGUAGE "English"

Section "WeatherApp" 

SetOutPath "$INSTDIR"

File /r "C:\Users\user\OneDrive\Desktop\Ecole\A2020-SujetsSpeciaux\WeatherStation_tp4b\WeatherApp\bin\Debug\netcoreapp3.1\*.*"

WriteRegStr HKCU "Software\${APP_NAME}" "" $INSTDIR
WriteUninstaller "$INSTDIR\Uninstall.exe"

CreateDirectory $SMPROGRAMS\9867199
CreateShortCut $SMPROGRAMS\9867199\${APP_NAME}.lnk $INSTDIR\${APP_NAME}.exe "" "$INSTDIR\nuage.ico" 0
CreateShortCut $SMPROGRAMS\9867199\${APP_NAME}-Uninstall.lnk "$INSTDIR\Uninstall.exe"
CreateShortCut "$Desktop\${APP_NAME} (9867199).lnk" "$INSTDIR\$(APP_NAME).exe" "" "$INSTDIR\nuage.ico" 0

sectionEnd

Section "Uninstall"

  ;ADD YOUR OWN FILES HERE...

  Delete "$INSTDIR\Uninstall.exe"

  RMDir /r "$INSTDIR"

  Delete "$Desktop\${APP_NAME}.lnk"

  DeleteRegKey /ifempty HKCU "Software\${APP_NAME}"

  RMDir /r "$SMPROGRAMS\9867199"




SectionEnd
