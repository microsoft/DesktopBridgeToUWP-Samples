# WPF picture viewer as a share target

This sample demonstrates how to extend a WPF picture viewer to become a share target. This way users can easily share pictures from Microsoft Edge, the Photos app and other application with our application. Sharing pictures is just one of many scenarios that can be enabled by making your app a sharing target. Several other data formats are supported as well. See the [Windows Dev Center documentation page] (https://msdn.microsoft.com/en-us/library/windows/apps/windows.applicationmodel.datatransfer.standarddataformats.aspx) for a complete list and more details.

AppxManifest
------------

The structure of our [AppxManifest.xml] (https://github.com/Microsoft/DesktopBridgeToUWP-Samples/blob/master/Samples/WPFasShareTarget/ShareUX/Package.appxmanifest) looks very similar to the [VB6withXaml sample] (https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/VB6withXaml). In fact, our sharing UI is also XAML. The only difference here is that we are declaring the ‘windows.shareTarget’ extension instead of protocol activation, to let the system know we want to activate if the user is sharing the specified data type and selects our app as the sharing target.

Build/Deploy and Run the sample
-------------------------------

 - Set the "ShareUX" as startup project
 - Build and deploy the project
 - Press Ctrl + F5 to run
 - To debug use attach to the proccess or follow the guidance [here - Debugging your Desktop Bridge app section] (https://msdn.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)
 
Windows Store App
-----------------
 You can also check out the app in the [Window Store] (https://www.microsoft.com/en-us/store/p/wpf-app-as-sharetarget/9pjcjljlck37)

  

