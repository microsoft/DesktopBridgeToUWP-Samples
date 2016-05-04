# Hello World Sample

This sample demonstrates how you can mannually convert your existant win32 dektop app to UWP and add a live tile that was previously impossible to use.

Build the sample
----------------

- The sample is a wpf hello world app but you can do the same with any xcopy deployable win32 application (yes even VB6:-))
- Press Ctrl+Shift+B, or select **Build** \> **Build Solution**
- Copy the HelloWorldWpfSample.exe to the CentennialPackage under the HelloWorldSample directory
- Pay attention to the AppxManifest.xml in the CentennialPackage you can modify it to change the app display name, add extenstions, update the app version, etc.
 
Deploy and Run the sample
-------------------------

 - To deploy run: add-appxpackage –register AppxManifest.xml from the PowerShell
 - To run go to the start menu and find the apps tile "HelloWorldWpfSample", right click and choose "Pin to taskbar"
 - When the app lunched press the Add Live Tile button in the app and see how your live tile is receiving the updates! 
 
Important Notes
---------------

- Xcopy deployable app has no registry settings and no custom deployment logic. IOW, you can deploy it by just copying files around to the right locations. 
- If your app is not xcopy deployable please use the [Dektop App Converter] (https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-run-desktop-app-converter)
- You can learn more about Live Tiles [here] (https://msdn.microsoft.com/en-us/library/windows/apps/hh465403.aspx)