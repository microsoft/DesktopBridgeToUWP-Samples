# JourneyAcrossTheBridge Build2017 sample

This sample takes an existing desktop app and, using the Desktop Bridge, gradually migrates and enhances it into a full Universal Windows Platform (UWP) app. This allows it to run on all Windows 10 devices, including Desktop, Xbox One, HoloLens, phones, and IoT devices.
 See a demo of this sample from our Build 2017 session on Channel 9: [Bring your desktop apps to UWP and the Windows Store using the Desktop Bridge](https://channel9.msdn.com/Events/Build/2017/B8011) 

>Note: For this example we will use a JavaScript UWP project template. We will not be writing any JavaScript code; rather, this template provides us with the default UWP tools to help us prepare our app for the store. We are using JavaScript instead of C# because there are some known issues using the default C# UWP project template.

Step1 – Convert
----------------

First step is conversion. If your app uses an installer, use the [Desktop App Converter](https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-run-desktop-app-converter).  Otherwise see the instructions in the MSDN article [Manually convert your Windows desktop application to a Universal Windows Platform (UWP) app]( https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-manual-conversion) and the [DesktopBridgeToUWP sample on GitHub](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/HelloWorldSample).

Our starting point is a simple Windows Forms app that shows a product inventory stored in a xml file. This app demonstrates how to use a Visual Studio UWP project to package your app. Additionally it shows how to use the [Desktop to UWP extension](http://go.microsoft.com/fwlink/?LinkID=797871&clcid=0x409) to allow a seamless F5 debugging experience. 


Step2 – Enhance
----------------

Since we are now packaged as a Windows app and have app identity, our app can call into UWP APIs it didn’t previously have access to. In this example we are adding a purchase button using the Payment Request API, see [Seller Center](https://seller.microsoft.com/en-us/seller) for more info. Now user can select a row of product and hit the purchase button to easily and quickly buy more of an item for their inventory. 


Step3 - Extend
---------------

In addition to calling more UWP APIs, we can now also add UWP components to our app package, to take advantage of other exciting UWP features. In this example we are adding a background task. With background tasks, an app can respond to triggers to execute code even when the app is not running. In our example we want to use the TimerTrigger to periodically run a maintenance task and notify the user via a toast notification. (BackgroundTask.cs)


Step4 – Migrate
---------------

To take advantage of UWP UI features such as a touch-friendly UI and smooth animations, we are going to migrate the user experience of our project to a XAML front-end using the Telerik UI. We can delete our now obsolete Windows Forms related UI code, however we still want to keep around our desktop app process for our existing business logic, i.e. managing the product data access. To communicate between the two processes, we use an App Service as a pipeline.


Step5 - Expand
---------------

In the final step, we are moving the last remaining pieces of code into the UWP app container process to expand the reach of our application to all Windows 10 devices that run UWP apps.


Build and Run the sample
-------------------------------

 - For Step 1-3, select the Packaging project as the startup project, for Step 4-5, select Step4/Step5 as the startup project right.
 - Hit ctrl F5 to run!
 - To trigger the maintenance task in Step 3, navigate to Debug -> Attach to Process and select WindowsFormsApp.exe process. In the Lifecycle Events drop down you will now see "MaintenanceTask" and you can trigger it by selecting it from the dropdown.
   
   Note: You need to move to a Fall Creators Update build(16220 or above) to see purchase work in steps 2 and 3.