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
using System.Windows.Forms;

namespace SystrayComponent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex = null;
            if (!Mutex.TryOpenExisting("MySystrayExtensionMutex", out mutex))
            {
                mutex = new Mutex(false, "MySystrayExtensionMutex");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new SystrayApplicationContext());
                mutex.Close();
            }
        }
    }
}
