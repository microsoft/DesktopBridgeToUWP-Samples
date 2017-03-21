@ECHO OFF

REM
REM See readme.md for information on this batch file
REM

SET CERTIFICATE_PATH=.
SET CERTIFICATE_NAME=contoso_demo

REM If you change the X509_NAME, you *must* also change the Identity in the AppX Manifest
REM to match, otherwise 'signtool' will give a cryptic error.
SET X509_NAME="CN=Contoso Demo Publisher"

SET EXPIRY_DATE=06/30/2017

NET FILE > nul 2> nul
IF ERRORLEVEL 1 (
ECHO.
ECHO ** This batch file must be run from an elevated command-prompt **
EXIT /B
)

ECHO.
ECHO Creating certificate and adding to the Trusted People store.
ECHO.
ECHO The certificate will expire on %EXPIRY_DATE% (MM/DD/YYYY). Edit this batch
ECHO file if you need a different expiry date.
ECHO.

IF NOT EXIST %CERTIFICATE_PATH% mkdir %CERTIFICATE_PATH%

IF EXIST %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.pvk del %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.pvk
IF EXIST %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.cer del %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.cer
IF EXIST %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.pfx del %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.pfx

makecert /n %X509_NAME% /r /h 0 /eku "1.3.6.1.5.5.7.3.3,1.3.6.1.4.1.311.10.3.13" /e %EXPIRY_DATE% /sv %CERTIFICATE_PATH%\contoso_demo.pvk %CERTIFICATE_PATH%\contoso_demo.cer
pvk2pfx /pvk %CERTIFICATE_PATH%\contoso_demo.pvk /spc %CERTIFICATE_PATH%\contoso_demo.cer /pfx %CERTIFICATE_PATH%\contoso_demo.pfx
certutil -addstore TrustedPeople %CERTIFICATE_PATH%\contoso_demo.cer

ECHO.
ECHO Done.
ECHO.
ECHO ** WARNING **
ECHO.
ECHO If this directory is shared with anyone (via a network share or because multiple
ECHO people can logon to the machine) or you did not provide a password for the private 
ECHO key, you should strongly consider removing permissions on the files for any untrusted 
ECHO users. See 'readme.md' for more information
ECHO.
ECHO Here are the current permissions on the files, FYI:
ECHO.
icacls %CERTIFICATE_PATH%\%CERTIFICATE_NAME%.*

