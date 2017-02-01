# Exposing an UWP App Service from WinForms App 

This sample demonstrates how to extend a WinForms data application with an App Service to provide other apps controlled access to its database, even if the WinForms app is not running

AppxManifest
------------
The entry point in the [AppxManifest.xml] (https://github.com/Microsoft/DesktopBridgeToUWP-Samples/blob/master/Samples/WinformsAppService/TestClient/Package.appxmanifest) is the Windows Forms application, that we have packaged using the Desktop Bridge. Now we can add a UWP component (implemented in a Windows Runtime component called ‘MyAppService.dll’) to the package, which provides the App Service implementation. This component gets activated out-of-proc to the main application process, so we also need to declare a package-level extension to explain to the system how the class will get activated.

Build/Deploy and Run the sample
-------------------------------

 - Set the "TestClient" as startup project
 - Build and deploy the project
 - Press Ctrl + F5 to run
 - To debug use attach to the proccess or follow the guidance [here - Debugging your Desktop Bridge app section] (https://msdn.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)
 
Windows Store App
-----------------
You can also check out the app in the [Window Store] (https://www.microsoft.com/en-us/store/p/winforms-appservice/9p7d9b6nk5tn)

  

