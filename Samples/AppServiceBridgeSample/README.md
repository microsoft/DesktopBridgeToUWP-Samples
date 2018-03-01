# AppService Bridge Sample

This sample demonstrates how to create a package that includes both a Win32 and Universal Windows Platform (UWP) processes, and communicate between them via an AppService APIs. It also shows how to launch a Win32 process from a UWP app using the new "fullTrustProcess" extension.

Adding UWP app to your package allows you to integrate features previously unavailable to desktop apps, such as UWP XAML UI frontend. Additionally, by gradually migrating existing Win32/.NET code to move functionality to UWP over time, you can improve the security of your user’s machine and expand your app’s reach to include a wide range of devices such as PCs, Phones, IoT devices, Xbox One and HoloLens.


Build/Deploy and Run the sample
-------------------------------

 - Deploy the solution by selecting **Build** \> **Deploy solution**.
 - Make sure the BackgroundProcess.exe was copied to the Appx folder - UWP\bin\x64\Release\AppX if not rebuild the solution or copy it manually.
 - Press F5 to run!
 - Launch the win32 background process by pressing the "Launch Background Process" button in the UWA.
 - Send requests from the UWA and see the responses.

Notes
------

- You can learn more about App Services [here](https://msdn.microsoft.com/library/windows/apps/xaml/windows.applicationmodel.appservice.aspx)