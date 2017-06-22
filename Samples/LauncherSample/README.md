# Launcher sample

This c++ sample creates a launcher that sets the current working directory for your application entrypoint at runtime. This should be considered as a workaround, ideally you should update your main process code to set the CWD. 
See this blog post for more details on how to properly set your current working directory or alternately use the launcher: [Accessing to the files in the installation folder in a Desktop Bridge application](http://blogs.msdn.microsoft.com/appconsult/2017/06/23/accessing-to-the-files-in-the-installation-folder-in-a-desktop-bridge-application/)

Build and Run the sample
-------------------------------

 - Build the solution.
 - Copy Launcher.exe and Launcher.cfg from the build path to the root of your Windows package.
 - Update your application entrypoint in your package.appxmanifest to Launcher.exe.
 - Configure Launcher.cfg content:
	Put your win32 application executable name that on the first line.  
	Put the current working directory as a relative path.  
	   
	Content of Launcher.cfg should look like this:  
	MyappEntrypoint.exe  
	Win32\MyappDirecotory\
