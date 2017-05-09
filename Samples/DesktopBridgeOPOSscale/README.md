# Desktop Bridge OPOS scale Sample

This sample demonstrates how to create a package that includes both a Win32 and Universal Windows Platform (UWP) processes, and communicate between them via an AppService APIs. It also shows how to launch a Win32 process from a UWP app using the new "fullTrustProcess" extension.
In addition this sample code demonstrate how to integrate a .netOPOS library to make an integration between UWP and OPOS to access all supported devices in OPOS library.

Adding the OPOS win32 process to your POS UWP app, allows you to access all the OPOS supported devices, even the devices not native supported in [Windows.​Devices.​Point​OfService] (https://docs.microsoft.com/en-us/uwp/api/windows.devices.pointofservice)


Build/Deploy and Run the sample
-------------------------------

 - Deploy the solution by selecting **Build** \> **Deploy solution**.
 - Make sure the scale.exe was copied to the Appx folder - UWP\bin\x64\Release\AppX if not rebuild the solution or copy it manually.
 - Press F5 to run!
 - Launch the win32 background process (win32 OPOS .net code) by pressing the "Find and Claime Scale" button in the UWA.
 - Request weight and notice the value comming from OPOS object.

Notes
------

- You can learn more about App Services [here] (https://msdn.microsoft.com/library/windows/apps/xaml/windows.applicationmodel.appservice.aspx)