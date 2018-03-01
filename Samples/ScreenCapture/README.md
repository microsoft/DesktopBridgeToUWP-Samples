# Screen Capture Sample

This sample demonstrates how to capture the screen from a Win32 app using Desktop App Bridge.  

For simplicity, the sample is using DDA APIs (Desktop Duplication APIs) to capture the frames of the entire screen and not just for the app.   Although, the smaple does not use any specific capability of the Desktop App Bridge, but bursts the myth that a Win32 app even though packaged into UWP cannot capture the screen frame for pixels outside its own app UI!   This sample code does not rely on UI Access which is currently not supported with Desktop App Bridge.


Build/Deploy and Run the sample
-------------------------------

- Select the MyDesktopApp.AppxDebug as the startup project
- Set the active configuration to Debug, x86 (if building x64 don't forget to update the package layout)
- Hit F5 to run!


See it in Action
----------------

- Run the UWP app when deployed.
- The DDA APIs are able to successfully capture the full screen and show the full screen frame inside its own UI!
