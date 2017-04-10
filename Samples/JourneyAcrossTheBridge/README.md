# JourneyAcrossTheBridge sample

This sample takes an existing desktop app and, using the Desktop Bridge, gradually migrates and enhances it into a full Universal Windows Platform (UWP) app. This allows it to run on all Windows 10 devices, including Desktop, Xbox One, HoloLens, phones, and IoT devices.
As we migrate our app, it remains fully functional every step of the way and picks up additional features that will help keep users engaged.

>Note: For this example we will use a JavaScript UWP project template. We will not be writing any JavaScript code; rather, this template provides us with the default UWP tools to help us prepare our app for the store. We are using JavaScript instead of C# because there are some known issues using the default C# UWP project template.

Step1 - Convert
----------------

Conversion is the first step on the bridge. If your app uses a .MSI (or similar) installer, the [Desktop App Converter](https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-run-desktop-app-converter) can help with the conversion process.  Otherwise, you’ll need to manually convert your app. See the instructions in the MSDN article [Manually convert your Windows desktop application to a Universal Windows Platform (UWP) app]( https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-manual-conversion) and the [DesktopBridgeToUWP sample on GitHub](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/HelloWorldSample).

Our starting point is a simple Windows Forms app that keeps track of the user’s status by storing it in the registry. This app demonstrates how to use a Visual Studio UWP project to package your app. Additionally it shows how to use the [Desktop to UWP extension](http://go.microsoft.com/fwlink/?LinkID=797871&clcid=0x409) to allow a seamless F5 debugging experience. 


Step2 – Enhance
----------------

Now that we have brought our app into the UWP app model, an app identity (as specified in the appxmanifest.xml file) is added. This enables our app to call UWP APIs it didn’t previously have access to. on. Let’s look at an example to add a Live Tile to our app. The goal is to update the tile on the Start menu whenever the user’s status changes.(Form1.cs)


Step3 - Extend
---------------

In addition to calling more UWP APIs, we can now also add UWP components to our app package, to take advantage of other exciting UWP features. Examples of this include: adding [App Services](https://msdn.microsoft.com/en-us/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service), becoming a sharing target or a file picker – or adding background tasks. With background tasks, an app can respond to triggers to execute code even when the app is not running – and even if the device is in connected stand-by mode. This way you can always keep your app up to date and create an experience of an app that is always alive. To illustrate this in our example we want to use the TimerTrigger to periodically check the status and notify the user via a toast notification. (MyBackgroundTask.cs)


Step4 – Migrate
---------------

To take advantage of UWP UI features such as a touch-friendly UI and smooth animations, let’s migrate the user experience of our project to a XAML front-end. In practice, the details of the UI migration depend on the Win32 UI framework your app has been using and the complexity of your UI. In this simple example the changes are straight forward; in most real apps, however, this step will require more developer resources.
At this time, we can delete our now obsolete Windows Forms related UI code, however we still want to keep around our desktop app process for our existing business logic, i.e. managing the app’s state via registry. To communicate between the two processes, we use an App Service as a pipeline.


Step5 - Expand
---------------

In the final step, we will now expand the reach of our application to all Windows 10 devices that run UWP apps, by moving the last remaining pieces of code into the UWP app container process. To do so, we will replace the registry-based state with the UWP-compliant ApplicationData API. Thus, we can now deploy our app to phones, Xbox One, HoloLens and other Windows devices that can run UWP apps.


Build/Debug and Run the sample
-------------------------------

 - Select the MyDesktopApp.AppxDebug as the startup project, right click --> Properties --> set the Package layout to the right location (..\MyDesktopApp.Package\bin\x86\Debug)
 - Set the active configuration to Debug, X86 (if building x64 don't forget to update the package layout)
 - Set your break points and hit F5 to run!

 
 Package the sample
-------------------------------

 - Right click on the MyDesktopApp.Package project --> Store --> Create app package, the JS project is currently the best workaround we have to enable a smooth packaging experience.
 - For step 1 and 2 you can select any CPU for step 3 and 4 please choose x86 and x64 only as it includes CoreCLR binaries


