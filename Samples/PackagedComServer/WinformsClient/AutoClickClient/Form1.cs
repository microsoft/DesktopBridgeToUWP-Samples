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
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        ACDual.Document acDoc = null;
        public Form1()
        {
            InitializeComponent();
            acDoc= new ACDual.Document();
            acDoc.ShowWindow();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            acText.Text = acDoc.text;
            acX.Text = acDoc.x.ToString();
            acY.Text = acDoc.y.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            short resInt16 = 0;

            acDoc.text = acText.Text;

            //set numeric values if valid
            if (short.TryParse(acX.Text, out resInt16))
                acDoc.x = resInt16;
            if (short.TryParse(acY.Text, out resInt16))
                acDoc.y = resInt16;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
