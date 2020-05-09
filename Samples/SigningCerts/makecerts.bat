@ECHO OFF

REM
REM See readme.md for information on this batch file
REM

SET CERTIFICATE_PATH=.
SET CERTIFICATE_NAME=contoso_demo
SET FULL_CERTIFICATE_NAME=%CERTIFICATE_PATH%\%CERTIFICATE_NAME%

REM If you change the X509_NAME, you *must* also change the Identity in the AppX Manifest
REM to match, otherwise 'signtool' will give a cryptic error.
SET X509_NAME="CN=Contoso Demo Publisher"

REM This is the date the certificate *and also the signature* will expire. It is recommended
REM you only make certificates valid for a short period of time (eg, a few weeks or a month)
REM to minimize the window in which an attacker could steal the certificate (or a signed app)
REM and try to use it against you.
SET EXPIRY_DATE=01/01/2019

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

IF EXIST %FULL_CERTIFICATE_NAME%.pvk del %FULL_CERTIFICATE_NAME%.pvk
IF EXIST %FULL_CERTIFICATE_NAME%.cer del %FULL_CERTIFICATE_NAME%.cer
IF EXIST %FULL_CERTIFICATE_NAME%.pfx del %FULL_CERTIFICATE_NAME%.pfx

REM The EKUs used here are:
REM * 1.3.6.1.5.5.7.3.3: This certificate is used for code signing (not SSL validation, etc.).
REM * 1.3.6.1.4.1.311.10.3.13: The signature expires when the certificate does ("lifetime signing").
REM If you want to test your app run on a non-developer-unlocked device, you will also need some
REM additional EKUs (these are automatically added by the Windows Store if you submit to the Store):
REM * 1.3.6.1.4.1.311.76.5.200: If you want to use Restricted Capabilities (like runFullTrust).
REM * 1.3.6.1.4.1.311.76.5.300: If you want to use General Capabilities (like networking).

makecert /n %X509_NAME% /r /h 0 /eku "1.3.6.1.5.5.7.3.3,1.3.6.1.4.1.311.10.3.13" /e %EXPIRY_DATE% /sv %FULL_CERTIFICATE_NAME%.pvk %FULL_CERTIFICATE_NAME%.cer

pvk2pfx /pvk %FULL_CERTIFICATE_NAME%.pvk /spc %FULL_CERTIFICATE_NAME%.cer /pfx %FULL_CERTIFICATE_NAME%.pfx
certutil -addstore TrustedPeople %FULL_CERTIFICATE_NAME%.cer

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

icacls %FULL_CERTIFICATE_NAME%.*
