# Adding a UWP XAML user experience

This sample demonstrates how to extend a Visual Basic 6 application to use the UWP XAML Map control to visualize the location information of a database application.

AppxManifest
------------

To understand how the UWP and Desktop Bridge tooling enables this scenario, let’s look at the relevant lines in the AppxManifest.xml file for the solution. The entry point here is the VB6 application, that we have converted using the Desktop Bridge. Now we can add a UWP component (implemented in MapUI.exe) to the package, which provides the XAML UI. To facilitate the activation of the modern component from the existing application, we define a protocol extension in the manifest. Furthermore, note that we need to declare two capabilities: (a) ‘runFullTrust’ to keep the VB6 code running at the same level of trust as before the Desktop Bridge conversion and (b) ‘internetClient’ for the Map control to download map data from the internet.
<Applications>
  <Application
       Id="VB6App" Executable="VB6App.exe"
       EntryPoint="Windows.FullTrustApplication">
    <uap:VisualElements ... />
    <Extensions>
      <uap:Extension
          Category="windows.protocol"
          Executable="MapUI.exe"
          EntryPoint=" MapUI.App">
        <uap:Protocol Name="desktopbridgemapsample" />
      </uap:Extension>
    </Extensions>      
  </Application>
</Applications>
<Capabilities>
  <rescap:Capability Name="runFullTrust" />
  <Capability Name="internetClient" />
</Capabilities>

Code Snippets
-------------

To protocol-activate the UWP view from the VB6 application, we invoke the LaunchUriAsync UWP API by wrapping it into a VB6-callable interop function (‘LaunchMap’) and pass in the latitude and longitude as parameters
Private Declare Function LaunchMap Lib "UWPWrappers.dll" _
  (ByVal lat As Double, ByVal lon As Double) As Boolean

Private Sub EiffelTower_Click()
    LaunchMap 48.858222, 2.2945
End Sub

And this is the underlying C++ code for the LaunchMap() API in UWPWrappers.dll, which is wrapping the calls into the actual LaunchUriAsync UWP API:
DllExport bool __stdcall LaunchMap(double lat, double lon)
{
  try
  {
    //
    // format the URI to match the defined protocol:
    // desktopbridgemapsample://location?lat={latitude}&?lon={longitude}
    //
    String ^str = ref new String(L"desktopbridgemapsample://");
    Uri ^uri = ref new Uri(
    str + L"location?lat=" + lat.ToString() + L"&?lon=" + lon.ToString());

    // now launch the UWP component
    Launcher::LaunchUriAsync(uri);
  }
  catch (Exception^ ex) { return false; }
  return true;
}
This now triggers the OnActivated event in the UWP component. The component is written in C++, but could done in any programming language that supports the UWP. Here we check if we got activated for the right protocol. If so, we will perform the actual activation and load the map page passing on the value set that includes the latitude and longitude information.
void App::OnActivated(Windows::ApplicationModel::Activation::IActivatedEventArgs^ e)
{
  if (e->Kind == ActivationKind::Protocol)
  {
    ProtocolActivatedEventArgs^ protocolArgs = (ProtocolActivatedEventArgs^)e;
    Uri ^uri = protocolArgs->Uri;
    if (uri->SchemeName == "desktopbridgemapsample")
    {
      Frame ^rootFrame = ref new Frame();
      Window::Current->Content = rootFrame;
      rootFrame->Navigate(TypeName(MainPage::typeid), uri->Query);
      Window::Current->Activate();
    }
  }
}

Build/Deploy and Run the sample
-------------------------------

This folder contains the source code and project file for the VB6 application in this sample.
This project produces the 'Landmarks.exe' binary that is included in the Store Package for this sample.
To modify and rebuild the VB6 application, you will need to install the legacy Visual Basic 6.0 IDE.

 - If you don't want to rebuild the VB6 code, go to your repo at YOUR_PATH\Samples\VB6withXaml\Xaml.MapUI\Win32 and rename the file extenstion from txt to exe
 - Set the Xaml.MapUI as startup project
 - Change the build configuration to x86
 - Press Ctrl + F5 to run
 - To debug use attach to the proccess or follow the guidance [here - Debugging your Desktop Bridge app section] (https://msdn.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)
 
Windows Store app
------------------
https://www.microsoft.com/store/apps/9n191ncxf2f6  

