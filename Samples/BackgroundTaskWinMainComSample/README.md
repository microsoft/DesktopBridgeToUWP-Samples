# Background tasks sample

This sample demonstrates how to use native full trust COM background tasks from a WinMain app. Background tasks allow WinMain apps to react to the rich set of triggers like [PushNotificationTrigger](https://docs.microsoft.com/windows/uwp/design/shell/tiles-and-notifications/raw-notification-overview)
Background tasks are intended for small work items that do not require user interaction, such as downloading mail, showing a toast notification for an incoming chat message, or reacting to a change in a system condition.
The package includes a WPF WinMain application that hosts the main window and implements the full trust COM Background task.
In this sample we show how to register for timer triggers but you can use any trigger available for full trust COM Background tasks.
The sample Background task also demonstrates how to perform some work in the background and gracefully handle a cancellation request. Handling cancellations are an important part of the Background task contract and should be abided by to allow the system to conserve resources when necessary.

Build/Deploy and Run the sample
-------------------------------

 - To Debug select the Package as the default project Ctrl+Shift+B, or select **Build** \> **Build Solution**.
 - To run the app, press F5 or run on the Local Machine, and then from the start menu, open "Sample WinMain Background Task App".
 - When the app starts, it will automatically register a 15 minute time triggered task.
 - To start the background task, click the drop-down menu for Lifecycle Events, and select "TimeTriggeredTask". Or use PLMDebug to enumerate the registered background task and activate it from the tool.
 - You can leave the app opened or close it before starting the "TimeTriggeredTask".
 - Look what happens after 15 min when the time trigger is fired.
 - To package your app, right click on the Package and select Publish-->Create App Package, for the full details please follow the instructions [here](https://docs.microsoft.com/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)


Notes
------

- You can see the full API guidance for creating and registering WinMain Background tasks [here](https://docs.microsoft.com/windows/uwp/launch-resume/create-and-register-a-winmain-background-task).
- You can learn more about Background tasks [here](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.background.aspx).
- You can learn more about PLMDebug [here](https://docs.microsoft.com/windows-hardware/drivers/debugger/plmdebug).
