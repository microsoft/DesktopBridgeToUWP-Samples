# Desktop app bridge to UWP Samples

Desktop Conversion Extensions is a bridge that enables you to convert your classic desktop application (like Win32, Windows Forms, and WPF) or game to a Universal Windows Platform (UWP) app or game. After conversion, your classic desktop app is packaged, serviced, and deployed in the form of a UWP app package (an .appx or an .appxbundle) targeting Windows 10.
There are two parts to the technology that enables desktop apps to be converted to UWP packages. The first is the Desktop App Converter, which takes your existing binaries and repackages them as a UWP package, you can also create the package manually like we show in these samples. Your code is the same, it's just packaged differently. The second piece is comprised of runtime technologies in the Windows Anniversary update that enable a UWP package to have executables that run as full trust instead of in an app container. This technology also gives a converted app a package identity, which is required to use some UWP APIs.
For more info on UWP apps, [see the **Universal Application Platform Guide**](https://msdn.microsoft.com/en-us/windows/uwp/get-started/universal-application-platform-guide)

## Requirements

- Windows 10, version 1607 (Anniversary Edition) or later.
- [Visual Studio 2017 RC](https://www.visualstudio.com/downloads/#visual-studio-community-2017-rc)
- [Desktop Bridge Debugging project](http://go.microsoft.com/fwlink/?LinkID=797871)
- [Windows Software Development Kit (SDK) for Windows 10](https://developer.microsoft.com/en-us/windows/downloads/windows-10-sdk)

## Feedback

- Ask a question or request a feature on [User Voice](https://wpdev.uservoice.com/forums/110705-universal-windows-platform/category/161895-desktop-bridge-centennial)
- File a bug in [GitHub Issues](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/issues)
	
## License

[MIT](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/blob/master/LICENSE)

## Additional resources

- [Bring your desktop app to the Windows Store](https://developer.microsoft.com/en-us/windows/bridges/desktop)
- [Customer Voice](https://www.youtube.com/watch?v=KTNCFvS5pNc)
- [Microsoft Virtual Academy course](http://wndw.ms/UctJ1p)
- [Package driectly from Visual Studio](https://docs.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)
- [Desktop App Converter](https://docs.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-run-desktop-app-converter)
- [Technical Case Studies](https://microsoft.github.io/techcasestudies/#technology=Desktop%20Bridge)
- [Sign up and get started using the Desktop Bridge](https://developer.microsoft.com/en-us/windows/projects/campaigns/windows-desktop-bridge)
 
