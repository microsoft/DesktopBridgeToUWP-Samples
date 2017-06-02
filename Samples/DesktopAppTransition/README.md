# WPF picture viewer with transition/migration/uninstallation

This sample demonstrates how to transition from a WPF app to a packaged app and migrate start tiles, taskbar pins, filetype associations, protocol handlers, as well as do data migration and uninstallation of the previous app.
Please visit the following blog post for more details: https://blogs.msdn.microsoft.com/universal-windows-app-model/2017/03/01/desktop-bridge-smooth-user-transition-data-migration/


Run the sample
-------------------------------
 - Install the MSI 
 - Run the app, pin to start tile and taskbar
 - Associate the app with a .ftj file
 - Install the provided AppxPackage (all entry point transitions happen during installation)
 - Run the packaged up (observe the migration/uninstallation experience)
 
 Build/Deploy
 ------------------------------
 - Build and deploy the project
 - Press Ctrl + F5 to run
 - To debug use attach to the proccess or follow the guidance [here - Debugging your Desktop Bridge app section] (https://msdn.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)

 


  

