# Publishing a Com Server with UWP and the Desktop Bridge

This sample demonstrates how to publish a Com Server in an application using the Desktop Bridge.  It includes the source for ACDual, which is an MFC Ole sample demonstrating a dual interface out-of-process (exe) com server, a folder for the source to build a UWP app package, and a sample C# winforms client that can automate the server.

AppxManifest
------------

To understand how the UWP and Desktop Bridge tooling enables this scenario, let's look at the relevant lines in the [AppxManifest.xml] (https://github.com/Microsoft/DesktopBridgeToUWP-Samples/blob/master/Samples/.appxmanifest) file for the solution. The entry point is the MFC application, that we have converted using the Desktop Bridge. Now we can add Packaged Com support to the package, which provides the registration information allowing other Win32 application clients to call it. This is enabled through the <windows.comServer> and <windows.comInterface> extensions.  These extensions allow you to specify the relevant registry entries for the ClsIds, ProgIds, Interfaces and TypeLibs that are supported through Packaged Com and the Desktop Bridge.  These manifest extension registrations are very similar to the existing registry keys.  When the application is installed, this information is  registered with COM in a private catalog and is managed by the system.  Any calls or activations using COM APIs (ie: CLSIdFromProgId(), CoCreateInstance()) will succeed and activate the out-of-process com server in the package.

Build/Deploy and Run the sample
-------------------------------

This folder contains the source code and project file for the ACDual application in this sample, a folder for packaging server into a UWP that includes the manifest and assets created using the Desktop App Converter, and a folder with a C# Winforms app that drives uses the com server.
The ACDual project produces the 'ACDual.exe' binary and 'AutoClik.tlb' typelib that is included in the Store Package for this sample.  To modify and rebuild the application, you need to ensure MFC is installed with Visual Studio.  To build the project:
 - Load ACDual.sln in Visual Studio and Build
 - Copy ACDual.exe and AutoClik.tlb from the build output into the \PackageFiles folder
 - Using makeappx.exe from your Windows 10 SDK folder, rebuild the app package from the command line, ie:
 `makeappx pack /l /p acdual.appx /d packagefiles`

- To sideload the application, you will need to sign it using signtool.  Refer to the MSDN topic on signing (https://docs.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-signing).

- To build the client winforms application, first you will need to install the server package. Then you can load the AutoClickClient.sln file in Visual Studio and build.

Windows Store App
-----------------
 You can also check out the app in the [Window Store] (https://www.microsoft.com/store/apps/9nm1gvnkhjnf)

Requirements
-----------------
- Packaged Com in UWP apps requires the Windows 10 Creators Update or higher
- Packaged Com requires Windows 10 SDK version 10.0.15063 or higher
