# Calling an OutOfProcess WinRT Component from a .Net Application

This sample demonstrates how to call an Out-Of-Process (OOP) Windows Runtime Component (WinRT Component) from a C# Winforms application using the Desktop Bridge.  It includes a modified version of  the Microsoft.SDKSamples.Kitchen server, which is a native WRL based OOP server, two simple windows forms apps that call the server, and a javascript project that is used to package the application for the Desktop Bridge.

The changes made to the default project templates for this sample are summarized as:
- Modify both Windows Forms projects to:
    - Add a project reference to WRLOutOfProcessWinRTComponent_server.vcxproj
    - Add a PostBuild action to push the project output into a sub-folder of the javascript project
- For the javascript project:
    - Delete unnecessary javascript files - index.html, css and js folders
    - Modify the WinFormsOOPWinRTComponent.Package.jsproj
        - Add a project reference from the WinRT Component to the JSProject
    - Modify the Package.appxmanifest
        - Add the uap5, rescap, and ignorable namespaces
        - Set TargetDeviceFamily Name="Windows.Desktop" and the MinVersion to "10.0.16215.0" or higher
        - Update the Application Executable attribute to point to the winforms exe, and set the entrypoint to Windows.FullTrustApplication
        - Add the "runFullTrust" capability
        - Add the <OutOfProcessServer> extension - VS will not do this for OOP Servers

Build/Deploy and Run the sample
-------------------------------
 - Select WinformsOutOfProcessWinRTComponent.sln in Visual Studio and Build.  This action builds the WinRT Component, builds the Winforms applications, and then pushes the project output of the Winforms application into the Javascript project's Win32 folder.
 - To deploy the app, you need to build packages - In VS right click on the Javascript project and chose Store->Create App Packages.  Do not associate with the store.  Choose x86, Retail as the flavor to build.
 - Now you can install the appx package.

Requirements
-----------------
- Calling WinRT Components from Win32 apps requires the Windows 10 Fall Creators Update or higher
- To build, you'll Windows 10 SDK version 10.0.16215 or higher
