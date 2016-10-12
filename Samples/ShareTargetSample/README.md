# Sharing content target app sample

This sample demonstrates how a converted Win32 app can receive content shared from another app. 

There are two parts to this package: a Win32 app and Universal Windows Platform (UWP) app. The UWP app utilizes the modern share target APIs (that are not available for Win32 apps), while the business logic of processing the data remains in the Win32 portion. The UWP app launches the Win32 app to process the data.

When walking along the bridge, you can migrate gradually all your logic to the UWP part.

This sample uses classes from the [**Windows.ApplicationModel.DataTransfer**](http://msdn.microsoft.com/library/windows/apps/br205967) and [**Windows.ApplicationModel.DataTransfer.Share**](http://msdn.microsoft.com/library/windows/apps/br205989) namespaces. Some of the classes you might want to review in more detail are the [**ShareOperation**](http://msdn.microsoft.com/library/windows/apps/br205977) class, which you use to manage a share operation, and the [**DataPackageView**](http://msdn.microsoft.com/library/windows/apps/hh738408) class, which you use to get the content being shared. Because each share scenario usually involves two apps – the source app that provides the content and a target app that receives the content – we recommend you install and deploy the [**Sharing content source app sample**](http://go.microsoft.com/fwlink/p/?linkid=231511) so you can see how sharing works from end to end. 

This sample covers how to receive shared content in text format.

## Build/Deploy and Run the sample

 - Deploy the solution by selecting **Build** \> **Deploy solution**.
 - Make sure the Win32Process_CPP.exe was copied to the Appx folder - ShareTarget\bin\x86\Debug\AppX. If not, rebuild the solution or copy it manually.
 - Run the [**Sharing content source app sample**](http://go.microsoft.com/fwlink/p/?linkid=231511) to share the content. Select the share target app: "Share Target"
 - You will see the target app receive content from the sharing app and launch the Win32 part of the package to process the data. 
 
## See also

-	[Share data]( https://msdn.microsoft.com/windows/uwp/app-to-app/share-data)
-	[Receive data]( https://msdn.microsoft.com/en-us/windows/uwp/app-to-app/receive-data)
-	[**DataPackageView**](http://msdn.microsoft.com/library/windows/apps/hh738408)
-	[**ShareOperation**](http://msdn.microsoft.com/library/windows/apps/br205977)
-	[**Windows.ApplicationModel.DataTransfer**](http://msdn.microsoft.com/library/windows/apps/br205967)
-	[**Windows.ApplicationModel.DataTransfer.Share**](http://msdn.microsoft.com/library/windows/apps/br205989)