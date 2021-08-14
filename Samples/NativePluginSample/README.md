This sample contains three apps:
1) A Host App, which has a plugin model (Extensions) that are native dlls. This app is a converted WPF app that utilizes the UWP AppExtension platform to implement the plugin model and allow the plugins to be delivered via UWP packages.
2) First plugin for the host app (packaged in C++ UWP).
3) Second plugin for the host app (packaged in C# UWP).

The Plugins are two projects. The first project builds the native DLL, the second packages it into a UWP app for distribution. You must build the native DLL first, then build the UWP app. A postbuild step in the native DLL build will place the DLL into the project folder of the UWP packager. The actual UWP packager apps are very basic template apps which just have an AppExtension declaration in the App Manifest. These are normal UWP apps.

The Plugin host is also two projects with the typical Desktop Bridge packager project and the original WPF C# app. The Plugin host uses AppExtensions to discover the other UWP packages that contain a plugin for it. Te host then copies the DLL from the AppExtension folder to its own local AppData folder. It always loads the DLLs from the local AppData folder. Only Desktop Bridge apps can load DLLs from local AppData. The Plugin host then uses events from the AppExtension API to be alerted of changes to the App Extensions so it knows when to unload the DLL, copy in a new one, and then reload the new updated DLL. 
