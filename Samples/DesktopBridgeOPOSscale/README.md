# Desktop Bridge OPOS scale Sample

This sample demonstrates how to create a package that includes both a Win32 and Universal Windows Platform (UWP) process, and communicate between them via an AppService APIs. It also shows how to launch a Win32 process from a UWP app using the new "fullTrustProcess" extension.
In addition this sample code demonstrates how to integrate a .net POS library to make an integration between UWP and POS to access all supported devices in POS library.
You can download Microsoft POS for .net from [here](https://www.microsoft.com/en-us/download/details.aspx?id=42081)

Adding the OPOS win32 process to your POS UWP app, allows you to access all the OPOS supported devices, even the devices not natively supported in [Windows.​Devices.​Point​OfService](https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice)


Build/Deploy and Run the sample
-------------------------------

 - Install Microsoft POS for .NET 1.14 (Runtime + SDK) from [here](https://www.microsoft.com/en-us/download/details.aspx?id=42081)
 - Deploy the solution by selecting **Build** \> **Deploy solution**.
 - Make sure the backgroundprocess.exe was copied to the Appx folder - UWP\bin\x64\Release\AppX if not rebuild the solution or copy it manually.
 - Select UWP project as your starting project
 - Press F5 to run!
 - Launch the win32 background process (win32 OPOS .net code) by pressing the "Find and Claim Scale" button in the UWA.
 - Request weight and notice the value coming from OPOS object.

Notes
------

- You can learn more about App Services [here](https://msdn.microsoft.com/library/windows/apps/xaml/windows.applicationmodel.appservice.aspx)
