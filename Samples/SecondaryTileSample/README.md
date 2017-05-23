# WPF picture viewer as a share target

This sample demonstrates how to create Secondary tiles directly from your desktop appliaction.
This 

AppxManifest
------------

The structure of our [AppxManifest.xml] (https://github.com/Microsoft/DesktopBridgeToUWP-Samples/blob/master/Samples/WPFasShareTarget/ShareUX/Package.appxmanifest) looks very similar to the [VB6withXaml sample] (https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/VB6withXaml). In fact, our sharing UI is also XAML. The only difference here is that we are declaring the ‘windows.shareTarget’ extension instead of protocol activation, to let the system know we want to activate if the user is sharing the specified data type and selects our app as the sharing target.

Build/Deploy and Run the sample
-------------------------------

 - Set the "MyDesktopApp.Package" as startup project deploy and build it.
 - Press Ctrl + F5 to run
 - To debug set the "PhotoStoreDemo.AppxDebug" as a startup project and hit F5


  

