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
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsDataApp
{
    public partial class Form1 : Form
    {
        DataSet ds = null;
        string filenName = @"datastore.xml";
        string appDataFilePath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string appDataDirectory = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + @"\DesktopBridge.Samples\";

            appDataFilePath = appDataDirectory + filenName;
            if (!File.Exists(appDataFilePath))
            {
                if (!Directory.Exists(appDataDirectory))
                {
                    Directory.CreateDirectory(appDataDirectory);
                }
                string installDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                DirectoryInfo info = Directory.GetParent(installDirectory);
                File.Copy(info.FullName + @"\" + filenName, appDataFilePath);
            }
            ds = new DataSet();
            ds.ReadXml(appDataFilePath);
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "contact";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ds.WriteXml(appDataFilePath);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {            
            ds.WriteXml(appDataFilePath);
        }
    }
}
