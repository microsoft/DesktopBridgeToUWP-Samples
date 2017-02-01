# MFC application receives push notifications!

This sample demonstrates how to extend an MFC application to receive raw push notifications from a server application regardless whether the client software is running or not. If the application is running it will receive the notification event and process the payload within the application process. If the application is not running, a background task will be triggered to handle the payload, for example to save to disk, notify the user with a toast notification, update the Live Tile, etc.

AppxManifest
------------
In addition to the main MFC application in our [AppxManifest.xml] (https://github.com/Microsoft/DesktopBridgeToUWP-Samples/blob/master/Samples/MFCwithPush/StorePackaging/Package.appxmanifest), we are declaring a background task type “pushNotification”. One thing to note here is that we also need to declare the activatable class that implements the background task in the manifest (as a package-level extension), because we will need the system to activate the component for us when a push notification comes in even when the application is not running.

Build/Deploy and Run the sample
-------------------------------

 - Set the "StorePackaging" as startup project
 - Change the build configuration to x86
 - Build and deploy the project
 - Press Ctrl + F5 to run
 - Launch the Push Test Server from the start menu and send notifications to your app!
 - To debug use attach to the proccess or follow the guidance [here - Debugging your Desktop Bridge app section] (https://msdn.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)
 
Windows Store App
-----------------
 You can also check out the app in the [Window Store] (https://www.microsoft.com/store/apps/9nrhrdq505qv)
 
Important Notes
---------------

####Pay attention when building your own app you will have to replace the secret/sid values to much your App's identity.
####For more details about push notification please follow the documention [here] (https://msdn.microsoft.com/windows/uwp/controls-and-patterns/tiles-and-notifications-raw-notification-overview)
  

