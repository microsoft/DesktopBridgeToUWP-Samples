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
using Windows.ApplicationModel.Background;

namespace Win32App
{
    class Program
    {
        /// <summary>
        /// Creates thread to register for background tasks
        /// </summary>
        static void Main(string[] args)
        {
            Thread registerBackgroundThread = new Thread(new ThreadStart(ThreadProc));
            registerBackgroundThread.Start();

            Console.ReadLine();
        }

        /// <summary>
        /// Register for background tasks (Time zone trigger & time trigger)
        /// </summary>
        static async void ThreadProc()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("******************************************");
            Console.WriteLine("**** Time zone trigger backgrond task ****");
            Console.WriteLine("******************************************");

            await BackgroundExecutionManager.RequestAccessAsync();

            // Register a TimeZoneTrigger background task with name and trigger
            RegisterBackgroundTask("TimeZoneTriggerTest", new SystemTrigger(SystemTriggerType.TimeZoneChange, false));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Registration completed for Time Zone change system event. Change the time zone to trigger the backgrond task");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*************************************");
            Console.WriteLine("**** Time trigger backgrond task ****");
            Console.WriteLine("*************************************");
            // Register a background TimeTrigger task with name and trigger
            RegisterBackgroundTask("TimerTriggerTest", new TimeTrigger(15, false));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Registration completed for Time trigger event. The backgrond task runs every 15 mins");
            Console.WriteLine("Pin the app to start and see how the tile is updated every time the task is triggered");
            
            // This will result in "value out of expected range" error.
            // It is failing in `toastnotificationactiontrigger.cpp` at `ToastNotificationActionTriggerFactory::ActivateInstance
            RegisterBackgroundTask("ToastNotificationTriggerTest", new ToastNotificationActionTrigger());
        }

        /// <summary>
        /// Register a background task with the taskEntryPoint, name and trigger
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
            builder.TaskEntryPoint = "BackgroundTask.SampleBackgroundTask";
            builder.Register();
        }
    }
} 
