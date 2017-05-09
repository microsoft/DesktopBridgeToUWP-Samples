# Background tasks sample

This sample demonstrates how to use background tasks from a Win32 console app. Background tasks allow running code even when the app is suspended. 
Background tasks are intended for small work items that do not require user interaction, such as downloading mail, showing a toast notification for an incoming chat message, or reacting to a change in a system condition.
The package includes both a Win32 and a windows runtime component as an in process extension.
In this sample we show how to register for time zone and timer triggers but you can use any triggers.


Build/Deploy and Run the sample
-------------------------------

 - To Debug select the MyDesktopApp.AppxDebug as the default project Ctrl+Shift+B, or select **Build** \> **Build Solution**. 
 - To run the app, just hit F5
 - Go to settings and change your time zone, you will receive notifications from the background task! Your live tile will also receive updates. 
 - Look what happens after 15 min when the time trigger is fired.
 - To package your app, right click on the MyDesktopApp.Package and select Store-->Create App Package, for the full details please follow the instructions [here](https://docs.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)
 

Notes
------

- You can learn more about Background tasks [here](https://msdn.microsoft.com/en-us/library/windows/apps/windows.applicationmodel.background.aspx) 
