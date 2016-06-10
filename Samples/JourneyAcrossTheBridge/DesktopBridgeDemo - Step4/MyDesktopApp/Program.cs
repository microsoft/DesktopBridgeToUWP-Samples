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
using System.Linq;
using System.Threading;
using Microsoft.Win32;
using Windows.Storage;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.AppService;

namespace MyDesktopApp
{
    class Program
    {
        static AppServiceConnection connection = null;
        static AutoResetEvent appServiceExit;
        static RegistryKey RegKey = null;
        static string CurrentStatus = "";

        /// <summary>
        /// Creates an app service thread
        /// </summary>
        static void Main(string[] args)
        {
            RegKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\UWPSamples\DesktopBridgeDemo", true);
            if (RegKey == null)
            {
                RegKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\UWPSamples\DesktopBridgeDemo", RegistryKeyPermissionCheck.ReadWriteSubTree);
                CurrentStatus = "Available";
                RegKey.SetValue("Status", CurrentStatus);
                ApplicationData.Current.LocalSettings.Values["Status"] = CurrentStatus;
            }
            else
            {
                CurrentStatus = (string)RegKey.GetValue(@"Status");
            }

            appServiceExit = new AutoResetEvent(false);
            Thread appServiceThread = new Thread(new ThreadStart(ThreadProc));
            appServiceThread.Start();
            appServiceExit.WaitOne();
        }

        /// <summary>
        /// Creates the app service connection
        /// </summary>
        static async void ThreadProc()
        {
            connection = new AppServiceConnection();
            connection.AppServiceName = "CommunicationService";
            connection.PackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;
            connection.RequestReceived += Connection_RequestReceived;
            connection.ServiceClosed += Connection_ServiceClosed;

            AppServiceConnectionStatus status = await connection.OpenAsync();
            if (status != AppServiceConnectionStatus.Success)
            {
                appServiceExit.Set();
            }
            else
            {
                ValueSet initialStatus = new ValueSet();
                initialStatus.Add("CurrentStatus", CurrentStatus);
                await connection.SendMessageAsync(initialStatus);
            }
        }

        /// <summary>
        /// Close the app service
        /// </summary>
        private static void Connection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            appServiceExit.Set();
        }

        /// <summary>
        /// Receives message from UWP app and sends a response back
        /// </summary>
        private static void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            string key = args.Request.Message.First().Key;
            string value = args.Request.Message.First().Value.ToString();
            if (key == "StatusUpdate")
            {
                RegKey.SetValue("Status", value);
                ApplicationData.Current.LocalSettings.Values["Status"] = value;
            }
        }
    }
}
