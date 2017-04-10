@ECHO OFF

REM
REM See readme.md for information on this batch file
REM

SET CERTIFICATE_PATH=.
SET CERTIFICATE_NAME=contoso_demo

REM First remove typical grants to everyone
icacls %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.* /inheritance:d
icacls %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.* /remove "NT AUTHORITY\Authenticated Users"
icacls %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.* /remove "BUILTIN\Users"

REM Next, explicitly add the current user
whoami > whoami.txt
FOR /f %%n IN (whoami.txt) DO icacls %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.* /grant %%n:F

del whoami.txt

ECHO.
ECHO Here are the current permissions on the files:
ECHO.
icacls %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.*

ECHO.
ECHO Done!
