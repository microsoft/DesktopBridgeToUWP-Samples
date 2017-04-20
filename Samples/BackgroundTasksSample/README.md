# Background tasks sample

This sample demonstrates how to use background tasks from a Win32 console app. Background tasks allow running code even when the app is suspended. 
Background tasks are intended for small work items that do not require user interaction, such as downloading mail, showing a toast notification for an incoming chat message, or reacting to a change in a system condition.
The package includes both a Win32 and a windows runtime component as an in process extension.
In this sample we show how to register for time zone and timer triggers but you can use any triggers.


Build/Deploy and Run the sample
-------------------------------

 - Press Ctrl+Shift+B, or select **Build** \> **Build Solution**. 
 - Under the BackgroundTasksSample\cs directory you will find the CentennialPackage to deploy during development, run: "add-appxpackage –register AppxManifest.xml" from a PowerShell window.
 - To deploy for production please follow the guidance [here] (https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-deploy-and-debug)
 - To run the app, go to the start menu, find the app tile "Win32App", right click, and choose "Pin to taskbar"
 - Go to settings and change your time zone, you will receive notifications from the background task! Your live tile will also receive updates. 
 - Look what happens after 15 min when the time trigger is fired.
 

Notes
------

- You can learn more about Background tasks [here](https://msdn.microsoft.com/en-us/library/windows/apps/windows.applicationmodel.background.aspx) 
