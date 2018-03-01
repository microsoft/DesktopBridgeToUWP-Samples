# AppService Bridge Sample - Win32 part is in C++

This sample demonstrates how to create a package that includes both a Win32 and Universal Windows Platform (UWP) processes, and communicate between them via an AppService APIs. It also shows how to launch a Win32 process from a UWP app using the new "fullTrustProcess" extension.

The Win32 part is written in C++.

Adding UWP app to your package allows you to integrate features previously unavailable to desktop apps, such as UWP XAML UI frontend. Additionally, by gradually migrating existing Win32/.NET code to move functionality to UWP over time, you can improve the security of your user’s machine and expand your app’s reach to include a wide range of devices such as PCs, Phones, IoT devices, Xbox One and HoloLens.


Build/Deploy and Run the sample
-------------------------------

 - Deploy the solution by selecting **Build** \> **Deploy solution**.
 - Make sure the Win32Process_CPP.exe was copied to the Appx folder - UWP\bin\x86\Release\AppX if not rebuild the solution or copy it manually.
 - Press F5 to run!
 - Launch the win32 background process by pressing the "Launch Background Process" button in the UWA.
 - Send requests from the UWA and see the responses.
 - Look at the toast in the corner of your screen:-)

Notes
------

In case you are using a slightly different version of VS than 2015 preview and getting the following error:
"The build tools for v140 (Platform Toolset = 'v140') cannot be found"
or "could not find assembly 'platform.winmd'"

1. Upgrade the project VC++ compiler and libraries.
2. Add the following path to the LIBPATH 
$Your_Path\Microsoft Visual Studio\$YOUR_VER\Enterprise\Common7\IDE\VC\vcpackages

- You can learn more about App Services [here](https://msdn.microsoft.com/library/windows/apps/xaml/windows.applicationmodel.appservice.aspx)