# SigningCerts

This directory contains two batch files that are used to help with signing
AppX packages.

1. `makecerts.bat` is used to make a self-signed code-signing certificate and
install it into the **Trusted People** store (so that it can be used to sign
and install AppX packages.
1. `securecerts.bat` is used to modify the ACLs on the created certificate
files to minimize the chance that a malicious user can copy the certificates
and sign code that you will trust.

## makecerts.bat

The `makecerts.bat` batch file generates a signing certicate that can be 
used to sign AppX packages. It also adds the signing certificate to the Trusted 
People store so that the AppX packages signed with the key are trusted by 
Windows.

There are four important variables set at the start of the batch file; you
should modify these before running the script.

    SET CERTIFICATE_PATH=.
    SET CERTIFICATE_NAME=contoso_demo
    SET X509_NAME="CN=Contoso Demo Publisher"
    SET EXPIRY_DATE=06/30/2017

The `CERTIFICATE_PATH` variable indicates the directory into which the
certs should be placed. If you change this value then you must also chage
the value used in the batch files that are used to build the two demo
projects.

The `CERTIFICATE_NAME` variable indicates the base name of the certificate
files. If you change this value then you must also chage
the value used in the batch files that are used to build the two demo
projects.

The `X509_NAME` variable indicates the Issuer name to be used in the certificate.
**Note:** it is *very* important that this name exactly match the name used in the
`<Identity Publisher="...">` attribute in the AppX manifest, otherwise the signing 
step will fail.

The `EXPIRY_DATE` variable determines when the self-signed certificate will
expire. It is recommended that you only generate certificates that are valid
for a short period of time (eg, a month) to minimize risk of the keys falling
into the wrong hands and being used to sign malicious code. 

## securecerts.bat

If the directory containing the certificate files is shared with anyone 
(either via a network share or because multiple users can log on to the
machine) and you **did not** provide a password for the private key, you
should strongly consider removing all permissions on the files for everyone
except yourself.

You can do this by running the `securecerts.bat` file after creating the 
files with `makecerts.bat`. As with the previous batch file, there are two
important environment variables that you should modify (if necessary):

    SET CERTIFICATE_PATH=.
    SET CERTIFICATE_NAME=contoso_demo

These have the same purpose as noted above.

The batch file does three things:

1. Copies all inherited ACEs, then disables inheritance
1. Explicitly removes `Authenticated Users` and `Users`
1. Explicitly adds the current user (as reported by `whoami`).

If you prefer to manually set the ACLs on the files, you can use File
Explorer (Right click -> **Properties** -> **Security** tab -> **Advanced**) 
or you can use the `icacls` command-line tool.
