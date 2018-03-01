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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        DataSet ds = null;
        string filenName = @"datastore.xml";
        string appDataFilePath = "";
        Product selectedProduct = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DesktopBridge.Samples\";

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
            dataGridView1.DataMember = "Product";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                selectedProduct = dataGridView1.SelectedRows[0].DataBoundItem as Product;
            }
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int f = 0;
                ds.WriteXml(appDataFilePath);
            }
        }
    }

    public class Product : INotifyPropertyChanged
    {
        public int ProductID { get; set; }
        //public string ProductCode { get { return ProductID.ToString(); } }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string UnitPriceString { get { return UnitPrice.ToString("######.00"); } }
        public int UnitsInStock { get; set; }
        public string UnitsInStockString { get { return UnitsInStock.ToString("#####0"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class ProductList : ObservableCollection<Product>
    {
        public ProductList() { }

        public Product GetProductById(int id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ProductID == id)
                {
                    return Items[i];
                }
            }
            return null;
        }
    }

}
