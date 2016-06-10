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
using System.Windows.Forms;
using Microsoft.Win32;
using Windows.ApplicationModel.Background;

namespace MyDesktopApp
{
    static class Program
    {
        public static string CurrentStatus = "";
        public static RegistryKey RegKey = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Writes the user state to the registry and updates the UI
            RegKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\UWPSamples\DesktopBridgeDemo", true);
            if (RegKey == null)
            {
                RegKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\UWPSamples\DesktopBridgeDemo", RegistryKeyPermissionCheck.ReadWriteSubTree);
                CurrentStatus = "Available";
                RegKey.SetValue("Status", CurrentStatus);                                
            }
            else
            {
                CurrentStatus = (string)RegKey.GetValue(@"Status");
            }

            RegisterBackgroundTask("TimerTrigger", new TimeTrigger(15, false));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        /// <summary>
        ///  Register the background task.
        /// </summary>
        public static void RegisterBackgroundTask(String triggerName, IBackgroundTrigger trigger)
        {
            // Check if the task is already registered
            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {
                if (cur.Value.Name == triggerName)
                {
                    // The task is already registered.
                    return;
                }
            }

            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
            builder.Name = triggerName;
            builder.SetTrigger(trigger);
            builder.TaskEntryPoint = "BackgroundTasks.MyBackgroundTask";
            builder.Register();
        }
    }
}
