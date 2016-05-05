# Hello World Sample

This sample demonstrates how to manually convert your existing Win32 desktop application to a Universal Windows Platform (UWP) app. It also shows you how to add a Live Tile, which were previously only available for native UWP apps. 
The sample is a WPF “Hello world” app, but the steps are the same for any xcopy-deployable Win32 application (yes, even VB6 :-)).

Build the sample
----------------
- Press Ctrl+Shift+B, or select **Build** \> **Build Solution**.
- Copy the HelloWorldConsoleSample.exe from the x64 directory to the CentennialPackage under the HelloWorldSample directory.
- Pay attention to the AppxManifest.xml in the CentennialPackage. You can modify it to change the app display name, add extensions, update the app version, etc.
 
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
