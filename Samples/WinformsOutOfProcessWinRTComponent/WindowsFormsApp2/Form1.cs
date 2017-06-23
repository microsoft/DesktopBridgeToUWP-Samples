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

namespace WindowsFormsApp2
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

        private void BreadCompletedHandler1(Oven oven, BreadBakedEventArgs args)
        {
            Action updateOutputText = () =>
            {
                textBox1.Text += "Baking Complete\r\n";
                textBox1.Text += "Bread flavor is: " + args.Bread.Flavor + "\r\n";
            };
            this.BeginInvoke(updateOutputText);
        }

        private void bakeBread_Click(object sender, EventArgs e)
        {
            int length = 1024;
            StringBuilder sb = new StringBuilder(length);
            int result = GetCurrentPackageFullName(ref length, sb);
            if (result != APPMODEL_ERROR_NO_PACKAGE)
            {
                this.Text += " - " + sb.ToString();
            }

            //Initialize the oven and register eventlisteners
            if (_myOven == null)
            {
                _myOven = new Microsoft.SDKSamples.Kitchen.Oven();
                _myOven.BreadBaked += new TypedEventHandler<Oven, BreadBakedEventArgs>(BreadCompletedHandler1);
            }

            _myOven.BakeBread("Wheat");
        }
    }
}

