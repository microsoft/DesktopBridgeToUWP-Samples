# Desktop To UWP Packaging project Sample

Visual Studio “15” adds support for a new packaging project that makes it easier to build and test apps converted with the Desktop Bridge directly from within Visual Studio to make your development experience more productive:
•The packaging project has a configuration file that allows Visual Studio to deploy any updates you make to the binaries from editing your desktop app projects directly into the UWP app package.
•You can launch and debug the UWP app package directly from within Visual Studio when you hit F5. You can set a break point in your existing code and step through it.

This sample demonstrates how to use the packaging project

Install Desktop App Converter Preview (Project Centennial)
Install Visual Studio “15” Preview 2 or higher
Download Desktop to UWP Packaging VSIX from the Visual Studio Gallery



Build the sample
----------------
- Copy both WpfUWP and DesktopToUWPAppx directories to the root of your c: drive
- Open the solution inside of WpfUWP
- The AppXPackageFileList.xml mapping file in the DesktopToUWPProject contains a mapping from the Win32 debug build directory to the location of the Appx file (in this case DesktopToUWPAppx). If you copied the solution to a different location than c: you will need to edit this mapping. If you update the project and add more files that need to be copied you will also update this file
- Right click on the 
 
Deploy and Run the sample
-------------------------

 - To deploy during development, run: "add-appxpackage –register AppxManifest.xml" from a PowerShell window.
 - To deploy for production please follow the guidance [here] (https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-deploy-and-debug)
 - To run the app, go to the start menu, find the app tile "HelloWorldConsoleSample", right click, and choose "Pin to taskbar"
 - When the app launches, press the “Add Live Tile” button in the app to see your live tile receive  updates! 
 
Important Notes
---------------

- Xcopy deployable app has no registry settings and no custom deployment logic. In other words, you can deploy it by copying files around to the right locations. 
- If your app is not xcopy deployable please use the [Desktop App Converter] (https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-run-desktop-app-converter)
- You can learn more about Live Tiles [here] (https://msdn.microsoft.com/en-us/library/windows/apps/hh465403.aspx)
