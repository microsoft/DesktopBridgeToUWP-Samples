# Sharing content target app sample

This sample demonstrates how an app receives content shared from another app in a converted Win32 app.
There are two parts in this package: Win32 and Universal Windows Platform (UWP) processes;
The UWP part utilize the share target modern APIs that are not available for win32 apps while the business logic of proccessing the data remains in the win32 part, the win32 app launches from the UWP to proccess the data.
When walking along the bridge you can migrate gradualy all your logic to the UWP part.
This sample uses classes from the [**Windows.ApplicationModel.DataTransfer**](http://msdn.microsoft.com/library/windows/apps/br205967) and [**Windows.ApplicationModel.DataTransfer.Share**](http://msdn.microsoft.com/library/windows/apps/br205989) namespaces. Some of the classes you might want to review in more detail are the [**ShareOperation**](http://msdn.microsoft.com/library/windows/apps/br205977) class, which you use to manage a share operation, and the [**DataPackageView**](http://msdn.microsoft.com/library/windows/apps/hh738408) class, which you use to get the content being shared. Because each share scenario usually involves two apps—the source app that provides the content and a target app that receives the content—we recommend you install and deploy the  [**Sharing content source app sample**](http://go.microsoft.com/fwlink/p/?linkid=231511) when you install and run this one. That way, you can see how sharing works from end to end. 
This sample covers how to receive shared content in text format.


Build/Deploy and Run the sample
-------------------------------

 - Deploy the solution by selecting **Build** \> **Deploy solution**.
 - Make sure the Win32Process_CPP.exe was copied to the Appx folder - UWP\bin\x86\Release\AppX if not rebuild the solution or copy it manually.
 - Press F5 to run!
 - Run the [**Sharing content source app sample**](http://go.microsoft.com/fwlink/p/?linkid=231511) to share the content and select the share target app - "Share Target"
 
Notes
------

- For more info about the concepts and shareing content target APIs, see these topics:

- [Sharing content source app sample](http://go.microsoft.com/fwlink/p/?linkid=231511)
- [Sharing and exchanging data](http://msdn.microsoft.com/library/windows/apps/hh464923)
- [How to receive files (HTML)](http://msdn.microsoft.com/library/windows/apps/hh758302)
- [How to receive HTML (HTML)](http://msdn.microsoft.com/library/windows/apps/hh758303)
- [How to receive HTML (XAML)](http://msdn.microsoft.com/library/windows/apps/hh973053)
- [How to receive text (HTML)](http://msdn.microsoft.com/library/windows/apps/hh758304)
- [How to receive text (XAML)](http://msdn.microsoft.com/library/windows/apps/hh973054)
- [Quickstart: Receiving shared content (HTML)](http://msdn.microsoft.com/library/windows/apps/hh465255)
- [Receive data](https://msdn.microsoft.com/library/windows/apps/mt243292)
- [**DataPackageView**](http://msdn.microsoft.com/library/windows/apps/hh738408)
- [**ShareOperation**](http://msdn.microsoft.com/library/windows/apps/br205977)
- [**Windows.ApplicationModel.DataTransfer**](http://msdn.microsoft.com/library/windows/apps/br205967)
- [**Windows.ApplicationModel.DataTransfer.Share**](http://msdn.microsoft.com/library/windows/apps/br205989)


