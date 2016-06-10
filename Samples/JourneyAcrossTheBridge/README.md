# JourneyAcrossTheBridge sample

This sample takes an existing desktop app and gradually migrate and enhance it with the Desktop Bridge to become a full UWP app that can run on all Windows 10 devices such as Xbox One, HoloLens, phones, etc. and take full advantage of the Universal Windows Platform.
As we gradually migrate our app, it remains fully functional every step of the way and picks up additional features that will help keep users engaged.


Step1 - Convert
----------------

As a first step to get our app on the bridge we take it through the conversion process.
This step assumes that you did the conversion. To learn more about the conversion go to the following [article on Dev Center](https://msdn.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-manual-conversion) and demonstrated in this [sample on GitHub](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/HelloWorldSample). Apps that are using an .MSI (or similar) installer, should use the [Desktop App Converter](https://msdn.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-run-desktop-app-converter) for the conversion process. 
Our starting point is a simple Windows Forms app that keeps track of the user’s status by storing it in the registry.
This app demonstrate how to sue the UWP packaging project, that was recently introduced by the Visual Studio team for Visual Studio 15 Preview 2 – see this post for details and instructions.


Step2 – Enhance
----------------

Now that we have brought our app into the UWP app model, an app identity (as specified in the appxmanifest.xml file) is added, which enables our app to call UWP APIs that were not available to our desktop application before. Let’s look at an example to add a Live Tile to our app. The goal is to update the tile on the Start menu whenever the user’s status changes.


Step3 - Extend
---------------

In addition to calling more UWP APIs, we can now also add UWP components to our app package, to take advantage of other exciting UWP features. Examples of this include: adding [App Services](https://msdn.microsoft.com/en-us/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service), becoming a sharing target or a file picker – or adding background tasks. With background tasks, an app can respond to triggers to execute code even when the app is not running – and even if the device is in connected stand-by mode. This way you can always keep your app up to date and create an experience of an app that is always alive. To illustrate this in our example we want to use the TimerTrigger to periodically check the status and notify the user via a toast notification


Step4 – Migrate
---------------

To take advantage of UWP UI features such as a touch-friendly UI and smooth animations, let’s migrate the user experience of our project to a XAML front-end. In practice, the details of the UI migration depend on the Win32 UI framework your app has been using and the complexity of your UI. In this simple example the changes are straight forward, in most real apps, however, this step will require more developer resources.
At this time, we can delete our now obsolete Windows Forms related UI code, however we still want to keep around our desktop app process for our existing business logic, i.e. managing the app’s state via registry. To communicate between the two processes, we are using an App Service as communication pipeline.


Step5 - Expand
---------------

In the final step, we will now expand the reach of our application to all Windows 10 devices that run UWP apps, by moving the last remaining pieces of code into the UWP app container process. To do so, we will replace the registry-based state with the UWP-compliant ApplicationData API. Thus, we can now deploy our app to phones, Xbox One, HoloLens and other Windows devices that can run UWP apps.


Build/Deploy and Run the sample
-------------------------------

 - Press Ctrl+Shift+B, or select **Build** \> **Build Solution**. 
 - Under the BackgroundTasksSample\cs directory you will find the CentennialPackage to deploy during development, run: "add-appxpackage –register AppxManifest.xml" from a PowerShell window.
 - To deploy for production please follow the guidance [here] (https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-deploy-and-debug)
 - To run the app, go to the start menu, find the app tile "Win32App", right click, and choose "Pin to taskbar"
 - Go to settings and change your time zone, you will receive notifications from the background task! Your live tile will also receive updates. 
 - Look what happens after 15 min when the time trigger is fired.
 
