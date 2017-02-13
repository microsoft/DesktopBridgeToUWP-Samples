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

using System.IO;
using System.Reflection;
using System.Windows;

namespace PhotoStoreDemo
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        internal static string CurrentPhotosPath
        {
            get
            {
                FileInfo fi = new FileInfo(Assembly.GetExecutingAssembly().Location);
                DirectoryInfo di = new DirectoryInfo(Path.Combine(fi.DirectoryName, "Photos"));
                if (!di.Exists)
                {
                    MessageBox.Show($"Folder '{di.FullName}' not found.");
                    Application.Current.Shutdown(1);
                }
                return di.FullName;
            }
        }
    }
}