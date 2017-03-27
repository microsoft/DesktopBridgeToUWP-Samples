# Ole Document support for the Desktop Bridge

This sample demonstrates how to publish support for Ole Documents in an application using the Desktop Bridge.  This sample packages an MFC sample app, Scribble, to include support for publishing its Ole properties in the Packaged Com catalog for use by Win32 apps that support Ole such as WordPad and Microsoft Office apps such as Word and Excel.

AppxManifest
------------
To understand how the UWP and Desktop Bridge enables this scenario, let's look at the relevant lines in the [AppxManifest.xml] (https://github.com/Microsoft/DesktopBridgeToUWP-Samples/blob/master/PackagedOleDocument/PackageFiles/AppManifest.xml) file.  The entry point is the Scribble application that we have converted using the Desktop App Converter.  Now we can add Packaged Ole support through the <windows.comServer> extensions.  These manifest extension registrations are very similar to the existing registry entries.  When the application is installed, this information is registered with COM in a private catalog and is managed by the system.  When a clients attempts to embed a document, the system is able to look in both the Packaged Com catalog and the system registry in order to find and activate the Ole server.   

Build/Deploy and Run the sample
-------------------------------
This folder contains source code for the MFC Scribble sample and a PackageFiles folder for building the UWP app package that includes the manifest and assets created using the Desktop App Converter. To modify and build Scribble, you need to ensure MFC is installed with Visual Studio.  If you rebuild the project, replace Scribble.exe in the  PackageFiles folder and rebuild the package using makeappx.

 - Load Scribble.sln
 - Change the build configuration to Retail|x86, then Build and Deploy the project
 - Copy the output, Scribble.exe into the PackageFiles folder
  - Use makeappx.exe from the Windows 10 SDK to rebuild the package, ie:
 `makeappx pack /l /p Scribble.appx /d packagefiles`
- To sideload the application, you will need to sign it using signtool.  Refer to the MSDN topic on [signing] (https://docs.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-signing).

Once deployed, you can now go into any Ole Document client and Insert a new "Scribble.Document" that will launch the Scribble application.
 
Windows Store App
-----------------
 You can also check out the app in the [Window Store] (https://www.microsoft.com/store/apps/9n4xcm905zkj)
 
 Requirements
----------------
- Packaged Com support in UWP requires Windows 10 Creators Update or higher
- Packaged Com requries the Windows 10 SDK version 10.0.15063 or higher.
