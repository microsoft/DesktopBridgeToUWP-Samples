# Using .NET Satellie Assemblies with AppX Packaging and Deployment

This sample shows how to take a .NET application using satellite assemblies for localization, and how
to package it as an AppX package as part of the Desktop Bridge. This minimizes the download and install
size of the application, especially for those applications with large numbers of resources (or large
numbers of supported languages). You can read more about the process in [the **Using MRT for Converted 
Desktop apps and games** whitepaper](http://aka.ms/MrtForCentennial).

## Structure of this sample

This sample contains various files and directories used to build the AppX package. Here is a summary, and
each of them is described in more detail below.

* The `ContosoDemoVsProject` directory contains a normal C# Desktop Console project that has a helper
class added to support AppX packaging and deployment
* The `ContosoDemoAppXPackageProject` directory contains an AppX manifest and some resource strings
used to create the AppX package. As part of the build process, it copies the outputs from
**ContosoDemoVsProject** for inclusion into the package
* The `ContosoDemoMetadata` directory contains additional metadata for creating the AppX package,
namely the list of languages supported
* The `make.bat` batch file is used to create the AppX package

## ContosoDemoVsProject

This directory contains a normal C# Desktop Console app named `contoso_demo.exe`. This program can be
run directly from the build output directory; it has no hard dependencies on AppX packaging (it does 
require Windows 10, although that dependency can be removed if necessary with additional work to rely 
on reflection). 

Running the program simply prints a few different localized strings; here is an example run:

    Attach debugger now if needed.
    Press any key to continue . . .

    en-US: Hello world
    de-DE: Hallo Welt
    fr-FR: Salut tout le monde
    es-MX: This is the fallback string if we don't have a localized version
    Press any key to continue . . .
 
The application contains localized strings for `en-US`, `de-DE`, and `fr-FR` (as seen) but does not
have any localized strings for `es-MX`, so a fallback value is used (typically this would be English
or another language the app is localized into).
 
The project contains the following interesting items:
 
1. `Program.cs` - represents the normal business logic of a C# Dekstop application; in this 
example it is trivial but you would replace it with your own application
1. `PriResourceResolver.cs` - a helper class you can include as-is into your application to work
with resources that have been converted into AppX resource packages
1. `Strings` - a directory containing `.resx` files for each of the supported languages
 
A typical program only needs to make two calls to `PriResourceResolver` to enable resource lookup:
 
    // Only enable the PriResourceResolver if it is supported (ie, the app has been packaged
    // and is running in a UWP context)
    if (PriResourceResolver.IsSupported())
    {
      PriResourceResolver.Enable();
    }
    
The rest of the `Program.cs` file is basically just test code that explicitly changes the
thread's locale in order to force different localized resources to load. A normal app would 
not do this; they would simply rely on the user's language preferences to set the default UI
Culture. The purpose of the application is to illustrate usage of the PriResourceResolver helper 
class, which bridges the gap between the .NET Satellite Assembly model and the MRT Resource 
Package model.
        
The `PriResourceResolver.cs` file has the logic to actually support resolution of .NET satellite
assemblies by UWP apps. For more information about what this file is doing, please see [the **Using 
MRT for Converted Desktop apps and games** whitepaper](http://aka.ms/MrtForCentennial).

The `Strings` directory contains .NET resource files (`.resx`) that hold the localized resources for 
the original Desktop application (ie, strings in the application itself that already existed before
the application was converted to AppX packaging).

## ContosoDemoAppXPackageProject

This directory contains a hand-crafted set of files and directories needed to create an AppX package
from the output of the VS project. The directory contains the following interesting items:

1. `AppXManifest.xml` - this is the manifest for the package that will be created
1. `Strings` - a directory containing `.resw` files for each of the supported languages
1. `Assets` - a directory containing images to be used for the Start Menu entry etc.

The `AppXManifest.xml` file tells Windows how to install the application and what resources to
display inside the Windows Shell and the Windows Store. For more information about the values
in the manifest, see [the **Using MRT for Converted Desktop apps and games** 
whitepaper](http://aka.ms/MrtForCentennial).

The `Strings` directory contains a set of sub-directories, one for each supported language, and in
each subdirectory is a `Resources.resw` file. Unlike the string files in the `ContosoVsProject` 
folder, these strings are only used by the AppX Manifest (and the Windows Shell, etc.) and are
not used at all by the application at runtime.

The `Assets` folder just contains some images to show for the tile in the Start Menu and in other
places in the shell.

## ContosoDemoMetadata

This directory contains a single file, `languages.txt`, that contains a list of the supported
languages. This file is read by the `make.bat` file (described below).

## make.bat

This is the batch file that makes the AppX package, splits up the individual resource
packages, and signs them so they can be installed. The batch file assumes that you have
already built the `ContosoVsDemo` project and thus have an `.exe` file (and possibly a
`.pdb` file) ready for packaging.

There are several important variables used in the batch file:

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

1. `APP_NAME` is the name you want for the finished `.appx` and `.appxbundle` files
1. `BUILD_FLAVOUR` is either `Debug` or `Release`, depending on which flavour of the
C# project you want to package up
1. `BINARY_SOURCE` is the directory that containsthe build output of the C# project
1. `PROJECT_SOURCE` is the directory that contains the AppX manifest and related artifacts
1. `METADATA_SOURCE` is the directory that contains metadata files related to the build
1. `INTERMEDIATE_FAT` is the directory where intermediate files used in the "fat pack" 
generation are placed
1. `INTERMEDIATE_SPLIT` is the directory where intermediate files used in the split resource
package generation are placed
1. `INTERMEDIATE_BUNDLE=` is the directory where intermediate files used in the 
generation of the AppX bundle are placed
1. `SUPPORTED_LANGUAGES` is the file that contains the list of supported languages
1. `TARGET_VERSION` is the target AppX version - this should be left at `10.0`
1. `PFX_FILE=` is the directory where the signing certificate can be found. **This
should never be inside the `PROJECT_SOURCE` directory!**

The batch file simply copies the build output from `%BINARY_SOURCE%` and then uses the
SDK utilities `makepri`, `makeappx`, and `signtool` to make and sign the AppX package.
For more information about what the batch file does, see [the **Using MRT for Converted 
Desktop apps and games** whitepaper](http://aka.ms/MrtForCentennial).

If you need to create a signing certificate for your AppX, you can use the batch file in
the `SigningCerts` directory that is a peer of this sample.
