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
using System.Runtime.InteropServices;
using Windows.Storage;
using Windows.ApplicationModel;
using System.Windows;

namespace ClassicWin32Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppData.ExtensionManager = new ExtensionManager("build.classicplugins.demo", this.Dispatcher);


            // show this is a packaged WPF app!
            myText.Text = "I am a packaged WPF app: " + Package.Current.Id.FullName;

            // This is for debug attaching before the program does anything meaningful.
            //MessageBox.Show(Package.Current.Id.FullName);
        }
    }
}
