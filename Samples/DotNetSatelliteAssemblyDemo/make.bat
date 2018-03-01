@ECHO OFF

SET APP_NAME=contoso_demo
SET BUILD_FLAVOUR=Debug
SET BINARY_SOURCE=ContosoDemoVsProject\ContosoDemo\bin\%BUILD_FLAVOUR%
SET PROJECT_SOURCE=ContosoDemoAppxPackageProject
SET METADATA_SOURCE=ContosoDemoMetadata
SET INTERMEDIATE_FAT=IntermediateFiles\%APP_NAME%\Fat
SET INTERMEDIATE_SPLIT=IntermediateFiles\%APP_NAME%\Split
SET INTERMEDIATE_BUNDLE=IntermediateFiles\%APP_NAME%\Bundle
SET SUPPORTED_LANGUAGES=%METADATA_SOURCE%\languages.txt
SET TARGET_VERSION=10.0
SET PFX_FILE=..\SigningCerts\contoso_demo.pfx

ECHO Copying latest bits
xcopy /s /y %BINARY_SOURCE%\*.dll %PROJECT_SOURCE%
xcopy /s /y %BINARY_SOURCE%\*.exe %PROJECT_SOURCE%
xcopy /s /y %BINARY_SOURCE%\*.pdb %PROJECT_SOURCE%

IF NOT EXIST %INTERMEDIATE_FAT%. (mkdir %INTERMEDIATE_FAT%)
IF NOT EXIST %INTERMEDIATE_SPLIT%. (mkdir %INTERMEDIATE_SPLIT%)
IF NOT EXIST %INTERMEDIATE_BUNDLE%. (mkdir %INTERMEDIATE_BUNDLE%)

ECHO.
ECHO Making fat config...
makepri createconfig /cf %INTERMEDIATE_FAT%\%APP_NAME%.xml /dq en-US_de-DE_fr-FR /pv %TARGET_VERSION% /o

IF ERRORLEVEL 1 GOTO GENERIC_ERROR

ECHO.
ECHO Making fat PRI...
makepri new /pr %PROJECT_SOURCE% /cf %INTERMEDIATE_FAT%\%APP_NAME%.xml /of %INTERMEDIATE_FAT%\resources.pri /mf AppX /o

IF ERRORLEVEL 1 GOTO GENERIC_ERROR

ECHO.
ECHO Making fat AppX...
makeappx pack /m %PROJECT_SOURCE%\AppXManifest.xml /f %INTERMEDIATE_FAT%\resources.map.txt /p %APP_NAME%.appx /o

IF ERRORLEVEL 1 GOTO GENERIC_ERROR

ECHO.
ECHO Signing fat AppX...
signtool sign /fd SHA256 /a /f %PFX_FILE% %APP_NAME%.appx

IF ERRORLEVEL 1 GOTO SIGNING_ERROR

ECHO.
ECHO ----
ECHO Fat pack done
ECHO ----
ECHO.

ECHO.
ECHO Making split config...
makepri createconfig /cf %INTERMEDIATE_SPLIT%\%APP_NAME%.xml /dq en-US /pv %TARGET_VERSION% /o

IF ERRORLEVEL 1 GOTO GENERIC_ERROR

ECHO.
ECHO Making split PRI...
makepri new /pr %PROJECT_SOURCE% /cf %INTERMEDIATE_SPLIT%\%APP_NAME%.xml /of %INTERMEDIATE_SPLIT%\resources.pri /mf AppX /o

IF ERRORLEVEL 1 GOTO GENERIC_ERROR

ECHO.
ECHO Making split AppXes...
makeappx pack /m %PROJECT_SOURCE%\AppXManifest.xml /f %INTERMEDIATE_SPLIT%\resources.map.txt /p %INTERMEDIATE_BUNDLE%\%APP_NAME%.main.appx /o

IF ERRORLEVEL 1 GOTO GENERIC_ERROR

REM This batch file assumes there's a text file with one language per line
FOR /F %%l IN (%SUPPORTED_LANGUAGES%) DO makeappx pack /r /m %PROJECT_SOURCE%\AppXManifest.xml /f %INTERMEDIATE_SPLIT%\resources.language-%%l.map.txt /p %INTERMEDIATE_BUNDLE%\%APP_NAME%.lang-%%l.appx /o

IF ERRORLEVEL 1 GOTO GENERIC_ERROR

ECHO.
ECHO Making AppX Bundle...
makeappx bundle /d %INTERMEDIATE_BUNDLE% /o /p %APP_NAME%.appxbundle

IF ERRORLEVEL 1 GOTO GENERIC_ERROR

ECHO.
ECHO Signing Bundle...
signtool sign /fd SHA256 /a /f %PFX_FILE% %APP_NAME%.appxbundle

IF ERRORLEVEL 1 GOTO SIGNING_ERROR

GOTO END


:GENERIC_ERROR

ECHO.
ECHO ******
ECHO Error %ERRORLEVEL%; stopping batch file. 
ECHO Verify all environment variables are correct and all files exist
ECHO ******
ECHO.
EXIT /B

:SIGNING_ERROR
ECHO.
ECHO ******
ECHO Error %ERRORLEVEL%; stopping batch file. 
ECHO Verify all environment variables are correct and all files exist.
ECHO Also ensure that the Publisher attribute of the Identity element
ECHO in the AppX manifest matches the Issuer in the signing certificate.
ECHO ******
ECHO.
EXIT /B)


:END

ECHO.
ECHO Done!
