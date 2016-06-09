
using System;
using System.Threading;

namespace Win32ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread appServiceThread = new Thread(new ThreadStart(ThreadProc));
            appServiceThread.Start();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*****************************");
            Console.WriteLine("**** Classic desktop app ****");
            Console.WriteLine("*****************************");
            Console.ReadLine();
        }

        static async void ThreadProc()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("sample.txt");
            string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Got the shared target text: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
        }
    }
}
