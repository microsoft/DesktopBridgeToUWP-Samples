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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.SDKSamples.Kitchen;
using Windows.Foundation;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        const long APPMODEL_ERROR_NO_PACKAGE = 15700L;
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder packageFullName);

        private Oven _myOven = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int length = 1024;
            StringBuilder sb = new StringBuilder(length);
            int result = GetCurrentPackageFullName(ref length, sb);
            if (result != APPMODEL_ERROR_NO_PACKAGE)
            {
                this.Text += " - " + sb.ToString();
            }

            //Start 2nd client
            //Get location of package, leverage UWP APIs for this
            //note that the Win32 binaries are in the Win32 subfolder of the package location
            string myPath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            myPath = System.IO.Path.Combine(myPath, "Win32\\WindowsFormsApp2.exe");
            System.Diagnostics.Process.Start(myPath);
        }

        private void BreadCompletedHandler1(Oven oven, BreadBakedEventArgs args)
        {
            Action updateOutputText = () =>
            {
                OvenClientOutput.Text += "Baking Complete\r\n";
                OvenClientOutput.Text += "Bread flavor is: " + args.Bread.Flavor + "\r\n";
            };
            //await OvenClientOutput.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, new Windows.UI.Core.DispatchedHandler(updateOutputText));
            this.BeginInvoke(updateOutputText);
        }

        private void bakeBread_Click(object sender, EventArgs e)
        {
            OvenClientOutput.Text += "null check\r\n";
            if (_myOven != null) { _myOven = null; }

            OvenClientOutput.Text += "new oven\r\n";
            _myOven = new Oven();
            _myOven.BreadBaked += new TypedEventHandler<Oven, BreadBakedEventArgs>(BreadCompletedHandler1);

            // Confirm how big the oven is
            OvenClientOutput.Text += "Oven volume is: " + _myOven.Volume.ToString() + "\r\n";

            // Bake a loaf of bread. This will trigger the BreadBaked event listeners
            OvenClientOutput.Text += "bake bread\r\n";
            _myOven.BakeBread("Sourdough");
        }

    }
}

