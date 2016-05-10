# AppService Bridge Sample

This sample demonstrates how you can create a package that includes both: win32 and UWP app and communicate between the two via AppService.
It also shows you how to launch a win32 full trust proccess from a UWP via the new "fullTrustProcess" extension.
By adding a UWP to your package you can enhance your app with new UWP features that were previously not available to desktop apps such as: background tasks, app services and UWP XAML UI frontend.
Over time, gradually migrating  existing Win32/.NET code to move functionality into the app container. 
Running your code in the app container benefits the security of your user’s machine, and at the end of the migration results in a UWP app that can run on PCs, Phones, IoT devices, Xbox One and HoloLens.

Build/Deploy and Run the sample
-------------------------------

 - Debloy the solution by selecting **Build** \> **Deploy solution**.
 - Make sure the FullTrustProcess.exe was copied to the Appx folder - UWP\bin\x64\Release\AppX if not rebuild the solution or copy it manually.
 - Press F5 to run!
 - Launch the Full trust process by pressing the "Launch FullTrust Process" button in the UWA.
 - Send requests from the UWA and see the responses.

Notes
------

- You can learn more about App Services [here] (https://msdn.microsoft.com/library/windows/apps/xaml/windows.applicationmodel.appservice.aspx)
