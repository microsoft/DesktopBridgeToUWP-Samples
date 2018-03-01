# Print to PDF  Sample

This sample demonstrates how to create a desktop bridge app that registers a PDF printer.

For simplicity, the sample uses the inbox 'Microsoft Print to PDF' soft printer to convert the printed XPS to PDF by simply adding XPS print job as a job to the inbox PDF printer.
While developing your own app, you can add/create your own logic of converting the printed XPS content to PDF.


Build/Deploy and Run the sample
-------------------------------

 - Select the UWP.Printer.Debug as the startup project
 - Set the active configuration to Debug, x86 (if building x64 don't forget to update the package layout)
 - Hit F5 to run!

 Just deploying the app/installing the app would register the printer with the system.  It is not necessary to run the binary to register the printer.

 
See it in Action
----------------

 - Run your faviorite application (Win32 or UWP)
 - Open your favourtie content to print
 - Select "Send to DesktopBridge PDF Pritner" printer
 - Select the destination folder and file name on prompt
 - Open the PDF file!