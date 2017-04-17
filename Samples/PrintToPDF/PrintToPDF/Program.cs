using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintToPDF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            // For the sake of this example, we're just printing the arguments to the console.
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("args[{0}] == {1}", i, args[i]);
            }

            String file = String.Empty;
            if (args.Length == 3)
            {
                //check that there is a XPS file
                file = args[2]; //"Sample.xps";
            }

            //If args < 3 => app is invoked directly and not from OS print operation.  In this
            //case we will simply show a dialog that printer has been installed.
            if (args.Length < 3)
            {
                MessageBox.Show("Printer has been installed.  You can now print to Centennial PDF Printer from any application.", "Centennial PDF Printer");

                return;
            }
            //At this point in time, the app can take this XPS file and choose to do anything
            //with this file.  For e.g., programmatically convert it to another format like
            //PDF.  There are 3P libraries available as well that can be used to achieve
            //the same.

            //For simplicity, we will use the inbox 'Microsoft Print to PDF' soft printer
            //to convert the XPS to PDF by simply adding XPS print job as a job to this PDF
            //Printer.

            try
            {
                // Create print server and print queue.
                LocalPrintServer localPrintServer = new LocalPrintServer();
                PrintQueue defaultPrintQueue = LocalPrintServer.GetDefaultPrintQueue();

                PrintQueueCollection queueCollection = localPrintServer.GetPrintQueues();
                PrintQueue selectedPrintQueue = null;

                foreach (PrintQueue pq in queueCollection)
                {
                    if (pq.FullName == "Microsoft Print to PDF") //PrinterName is a classmember
                    {
                        selectedPrintQueue = pq;
                    }
                }

                try
                {
                    // Print the Xps file while providing XPS validation and progress notifications.
                    PrintSystemJobInfo xpsPrintJob = selectedPrintQueue.AddJob("Centennial Test PDF Print Job", file, false);
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t{0} could not be added to the print queue.", file.ToString());
                    if (e.InnerException.Message == "File contains corrupted data.")
                    {
                        Console.WriteLine("\tIt is not a valid XPS file. Use the isXPS Conformance Tool to debug it.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
