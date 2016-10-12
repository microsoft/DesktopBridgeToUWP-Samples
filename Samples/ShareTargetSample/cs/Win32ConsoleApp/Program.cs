//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Threading;

namespace Win32ConsoleApp
{
    class Program
    {
        /// <summary>
        /// Creates a thread for the app
        /// </summary>
        static void Main(string[] args)
        { 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*****************************");
            Console.WriteLine("**** Classic desktop app ****");
            Console.WriteLine("*****************************");

            // The app launched from the share target UWP part
            if (args.Length > 3 && args[2] == "LaunchedFromShareTargetUWP")
            {
                Thread appShareTargetHandlerThread = new Thread(new ThreadStart(ThreadProc));
                appShareTargetHandlerThread.Start();
            }
            else
            {
                Console.WriteLine("Open the share source from the UWP samples repository and share text");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Proccess the content recived from another app. 
        /// In this sample we just read the content from a file but in real world it can be any business logic that remained in the win32 portion of the package
        /// </summary>
        static async void ThreadProc()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
            // Get the content recived from another app.
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("sample.txt");
            string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Got the shared target text: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
        }
    }
}
