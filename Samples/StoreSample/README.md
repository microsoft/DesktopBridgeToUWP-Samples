# Store API Sample
This sample demonstrates how to perform in-app purchases using the Windows.Services.Store API from an application that uses the [Desktop Bridge](https://developer.microsoft.com/en-us/windows/bridges/desktop) (Centennial). This is done via a simple purchase workflow that uses store-managed consumables.

**NOTE: If you are creating an application from scratch we recommend using the Windows Universal Platform.** A universal platform Store example can be found [here](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store).

The Store API supports several different operations which are documented at [https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx).

This sample demonstrates the following subset of operations:

  * [In-app purchases](https://msdn.microsoft.com/en-us/windows/uwp/monetize/in-app-purchases-and-trials) using Store-managed consumables.
  * Check the balance of a Store-managed consumable. 
  * Retrieve the list of configured consumable add-ons for a Store application

## System Requirements

  * Windows 10, version 1607 (Anniversary Edition) or later.
  * [Visual Studio 15](https://www.visualstudio.com/visual-studio-pre-release-downloads/)

## Using the Code
This sample is meant to illustrate how it is possible to make Windows 10 API calls such as those found in the [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) namespace from an application using the [Desktop Bridge](https://developer.microsoft.com/en-us/windows/bridges/desktop) (Win32 Windows Forms or Classic Desktop WPF). While this sample application can be associated with a Store app and run as-is, it is much more interesting to go through the steps of preparing your app to call UWP APIs by integrating the purchase dialog from this sample into your application. The following preparatory tasks are needed whether you wish to integrate the sample code into your own application or use it as a standalone sample.

  * [Convert your existing app](https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-root)
  * [Register as an app developer](https://developer.microsoft.com/store/register)
  * [Create a Store App by reserving the name](https://msdn.microsoft.com/en-us/windows/uwp/publish/create-your-app-by-reserving-a-name) of the converted app
  * [Submit your converted app to the Store](https://msdn.microsoft.com/windows/uwp/publish/app-submissions)
  * [Create your store-managed consumables](https://msdn.microsoft.com/windows/uwp/publish/add-on-submissions)

To run the code as-is you need to modify the sample [AppXManifest.xml](cs/Packaging/PackageLayout/AppXManifest.xml) file to include your application identity information. Currently there are dummy values in the manifest for the following items that you will need to change to match your application.

  * Identity Name
  * Identity Publisher
  * Identity Version
  * Properties Display Name
  * Properties PublisherDisplayName
  * Application Id
  * Application DisplayName
  * All of the image assets
  
For more information about what values to use see the documentation for [viewing app identity details](https://msdn.microsoft.com/en-us/windows/uwp/publish/view-app-identity-details). You also need to manually set the Package Layout setting.

  1. Right-click on the *Packaging* project and select **Properties**.
  2. Under *Start Options* set the Package Layout to **PackageLayout**.
  3. Set the *Start Up Tile* option to **InAppPurchases**.

Once you've converted your app, gotten it approved in the Store, and the store-managed consumables are set up you need to make a few minor changes to your project in Visual Studio to enable UWP APIs and get the sample working. They are:

  * [Add Windows 10 WinMD reference to your project](#add-windows-10-reference)
  * [Add reference to System.Runtime.SystemRuntime](#add-systemruntimesystemruntime-reference)
  * [Add theme reference](#add-theme-reference)
  * [Add the dialog](#add-the-dialog)
  * [Start debugging](#debugging)

### Add Windows 10 Reference
 
  1. Right-click on **References** in your project and select "Add Reference...".
  2. Click on the **Browse** button at the bottom of the dialog.
  3. Change the file type filter to "All Files(*.*)" in the bottom right-hand corner of the Browse dialog.
  4. Browse to **C:\Program Files (x86)\Windows Kits\10\UnionMetadata** and select **Windows.winmd**.
  5. Click **Add** to add the reference to your project.
  6. Click **OK** to close the Reference Manager dialog.

### Add System.Runtime.SystemRuntime Reference
If your project already includes a reference to this assembly, you can skip this step.

  1. Right-click on **References** in your project and select **Add Reference...***.
  2. Click on the **Browse** button at the bottom of the dialog.
  3. Browse to **C:\Windows\Microsoft.NET\Framework\v4.0.30319** and select **System.Runtime.WindowsRuntime.dll**.
  4. Click **Add** to add the reference to your project.
  5. Click **OK** to close the Reference Manager dialog.

### Add Theme Reference
The dialogs in the example have been given a minimalistic theme. To ensure they display correctly you must add a *ResourceDictionary* reference in **App.xaml**. Copy the following code snippet into the **<Application.Resources>** section of **App.xaml**. Replace "PROJECT NAME HERE" with the name of your project.

```xml
<ResourceDictionary>
	<ResourceDictionary.MergedDictionaries>
	    <ResourceDictionary Source="/[PROJECT NAME HERE];component/Theme.xaml"/>
	</ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
``` 

### Add the Dialog
The sample code in this repository provides a simple purchase dialog that will retrieve a list of store-managed consumables for your app and show the 3 least expensive add-ons in the various dialogs. The following steps show how to add it to your project.

  1. Copy the **PurchaseDialog** folder from the sample to your main project folder.
  2. In your solution right-click on the project to which you want to add the dialog and select **Add->Existing Item**.
  3. Browse into the newly copied PurchaseDialog folder and select **ExtensionMethods.cs** and **StoreConsumableHelper.cs**.
  4. Click **Add**
  5. Open the *Add Existing Item* dialog again by right-clicking on the project and selecting **Add->Existing Item**.
  6. Browse to the **PurchaseDialog** folder and change the file type filter from Visual C# Files to "XAML Files (*.xaml;*.xoml)".
  7. Select all of the XAML files in the PurchaseDialog folder and click **Add**.
  8. Add some code to your project to launch the new dialog.

```cs
PurchaseWindow purchaseWindow = new PurchaseWindow();
purchaseWindow.ShowDialog();
```

### Debugging
Once the dialog is added to your project, starting a standard Debug session will allow the application to run but the Store API calls will NOT work until you run your app in the context of an .appx package. The Store API calls are meant to run in the context of a Universal Windows Platform Application but because your project is not universal it will run under the old app model. To have your application run in the correct context you need to add a new packaging project to your solution. Instructions for how to set this up can be found here:

  * [Deploy and debug your converted UWP app](https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-deploy-and-debug)

For more information about testing apps that use the Windows.Services.Store namespace see the [official documentation](https://msdn.microsoft.com/en-us/windows/uwp/monetize/in-app-purchases-and-trials?f=255&MSPPError=-2147217396#testing-apps-that-use-the-windows-services-store-namespace).

### Make It Yours
With the dialog added to your project and the Store APIs working you are now free to modify the UX so that it seemlessly integrates with the rest of your application. 

## What About Windows Forms Applications?
This sample shows how to add a purchase dialog to an existing WPF application but what if your application is a Windows Forms application? No problem. You will have to create your own dialogs but the Store API calls are the same and are enabled by [adding the Windows 10 reference](#add-windows-10-reference) as well as the [System.Runtime.WindowsRuntime Reference](#add-system.runtime.systemruntime-reference).

## Questions?
Use the [Issues](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/issues) tab to submit questions, issues, or feedback.