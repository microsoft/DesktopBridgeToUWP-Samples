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

// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace PhotoStoreDemo
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                ProcessCommandLine(e.Args);
            }

            base.OnStartup(e);
            MainWindow window = new MainWindow();
            window.Show();

        }
        
        public void Activate()
        {
            this.MainWindow.Activate();
        }

        internal void ProcessCommandLine(string[] args)
        {             
            if (args.Length > 0)
            {
                var path = args[0];
                var sourcePath = new FileInfo(path);
                if (sourcePath.Exists)
                {
                    var destPath = new FileInfo(Path.Combine(PhotosFolder.Current, sourcePath.Name));
                    if (!destPath.Exists)
                    {
                        File.Copy(sourcePath.FullName, destPath.FullName);
                    }
                }
            }
        }
    }
}