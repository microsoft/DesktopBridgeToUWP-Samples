# Northwind Sample - End to end example for UWA UI + Win32 legacy code

This end-to-end sample demonstrates how to migrate a Win32 application to a Universal Windows Platfornm (UWP) app via Desktop Bridge. It shows how to take advantage of UWP features, such as touch-friendly UI, smooth animations, and modern application deployment technology, while retaining the Win32 application for existing business logic. It also shows how to use an App Service to communicate between the UWP and Win32 processes.
In this case, the Win32 application process manages a SQL CE database which contains data for the application. We assume this piece of code cannot migrate to UWP because the database format cannot be changed and SQL CE is not supported by UWP. 

This sample uses AppService to communicate between UWP process and Win32 process.


Build/Deploy and Run the sample
-------------------------------

 - Deploy the solution by selecting **Build** \> **Deploy solution**.
 - Make sure the NorthwindCent.DesktopServer.exe was copied to the Appx folder - UwaClient\bin\<x86|x64>\<Debug|Release>\AppX if not rebuild the solution or copy it manually.
 - Press F5 to run!

Notes
------
- To learn more about App Services, see [Windows.ApplicationModel.AppService] (https://msdn.microsoft.com/library/windows/apps/xaml/windows.applicationmodel.appservice.aspx)