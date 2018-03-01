# Calling a C# InProc WinRT Component from a .Net Application

This sample demonstrates how to call an In-Process Windows Runtime Component (WinRT Component) from a C# Winforms application using the Desktop Bridge.  It includes the source for SimpleMathManagedWinRT which is a C# InProc server, a simple windows forms calculator, and a javascript project that is used to package the application for the Desktop Bridge.

The changes to default projects required of this sample are summarized as:
- Modify the WinFormsCalculator.csproj to:
    - Add a project reference to SimpleMathManagedWinRT.vcxproj
    - Add a PostBuild action to push the project output into the Win32 sub-folder of the javascript project
- For the WinFormsCalculator.Package.jsproj:
    - Delete unnecessary javascript files - index.html, css and js folders
    - Add a project reference from the WinRT Component to the JSProject
    - Modify the Package.appxmanifest
        - Add the uap5, rescap, and ignorable namespaces
        - Set TargetDeviceFamily Name="Windows.Desktop" and the MinVersion to "10.0.16215.0" or higher
        - Update the Application Executable attribute to point to the winforms exe, and set the entrypoint to Windows.FullTrustApplication
        - Add the "runFullTrust" capability

Build/Deploy and Run the sample
-------------------------------
 - Select WinFormsManagedWinRTComponent.sln in Visual Studio and Build.  This action builds the WinRT Component, builds the Winforms application, and then pushes the project output of the Winforms application into the Javascript project's Win32 folder.
 - To deploy the app, you need to build packages - In VS right click on the Javascript project and chose Store->Create App Packages.  Do not associate with the store.  Choose x86, Retail as the flavor to build.
 - Now you can install the appx package by double clicking on the .appx file.  To install this package, you will need to install the certificate in your Trusted People store.

Requirements
-----------------
- Calling WinRT Components from Win32 apps requires the Windows 10 Fall Creators Update or higher
- To build, you'll need the Windows 10 SDK version 10.0.16215 or higher
