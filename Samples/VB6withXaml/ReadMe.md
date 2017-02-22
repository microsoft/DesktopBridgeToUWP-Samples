# Adding a UWP XAML user experience to VB6 Application

This sample demonstrates how to extend a Visual Basic 6 application to use the UWP XAML Map control to visualize the location information of a database application.

AppxManifest
------------

To understand how the UWP and Desktop Bridge tooling enables this scenario, let’s look at the relevant lines in the [AppxManifest.xml] (https://github.com/Microsoft/DesktopBridgeToUWP-Samples/blob/master/Samples/VB6withXaml/Xaml.MapUI/Package.appxmanifest) file for the solution. The entry point here is the VB6 application, that we have converted using the Desktop Bridge. Now we can add a UWP component (implemented in MapUI.exe) to the package, which provides the XAML UI. To facilitate the activation of the modern component from the existing application, we define a protocol extension in the manifest. Furthermore, note that we need to declare two capabilities: (a) ‘runFullTrust’ to keep the VB6 code running at the same level of trust as before the Desktop Bridge conversion and (b) ‘internetClient’ for the Map control to download map data from the internet.

Build/Deploy and Run the sample
-------------------------------

This folder contains the source code and project file for the VB6 application in this sample.
This project produces the 'Landmarks.exe' binary that is included in the Store Package for this sample.
To modify and rebuild the VB6 application, you will need to install the legacy Visual Basic 6.0 IDE. For more information on how to do it follow the [microsoft support statement for VB 6.0] (https://msdn.microsoft.com/en-us/vstudio/ms788708.aspx) and the [lifecycle support policy FAQ] (https://support.microsoft.com/en-us/help/17959/lifecycle-support-policy-faq-microsoft-developer-tools)

 - If you don't want to rebuild the VB6 code, go to your repo at YOUR_PATH\Samples\VB6withXaml\Xaml.MapUI\Win32 and rename the file extenstion from txt to exe
 - Set the Xaml.MapUI as startup project
 - Change the build configuration to x86
 - Press Ctrl + F5 to run
 - To debug use attach to the proccess or follow the guidance [here - Debugging your Desktop Bridge app section] (https://msdn.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)

Windows Store App
-----------------
 You can also check out the app in the [Window Store] (https://www.microsoft.com/store/apps/9n191ncxf2f6)

  

