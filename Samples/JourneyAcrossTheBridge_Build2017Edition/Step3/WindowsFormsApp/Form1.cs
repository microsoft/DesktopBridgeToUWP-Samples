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
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Payments;

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
            RegisterBackgroundTasks();
        }

        private void RegisterBackgroundTasks()
        {
            foreach (var bgTask in BackgroundTaskRegistration.AllTasks)
            {
                if (bgTask.Value.Name == "MaintenanceTask")
                {
                    bgTask.Value.Unregister(true);
                }
            }

            var requestTask = BackgroundExecutionManager.RequestAccessAsync();
            var builder = new BackgroundTaskBuilder();

            builder.Name = "MaintenanceTask";
            builder.TaskEntryPoint = "BackgroundTasks.MaintenanceTask";
            builder.SetTrigger(new MaintenanceTrigger(360, false));
            BackgroundTaskRegistration task = builder.Register();
        }

        [ComImport, Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize([In] IntPtr hwnd);
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

        private async void button1_Click(object sender, EventArgs e)
        {
            var pm = new PaymentMediator();
            ((IInitializeWithWindow)(object)pm).Initialize(this.Handle);

            PaymentDetails details = new PaymentDetails(new PaymentItem("Total", new PaymentCurrencyAmount(selectedProduct.UnitPrice.ToString(), "USD")), new List<PaymentItem>() { new PaymentItem(selectedProduct.ProductName, new PaymentCurrencyAmount(selectedProduct.UnitPrice.ToString(), "USD")) }) { ShippingOptions = CreateShippingOptions() };
            List<PaymentMethodData> methods = new List<PaymentMethodData>() { new PaymentMethodData(new List<String>() { "basic-card" }) };
            PaymentMerchantInfo merchantInfo = new PaymentMerchantInfo(new Uri("http://www.contoso.com"));
            PaymentOptions options = new PaymentOptions() { RequestShipping = true, ShippingType = PaymentShippingType.Delivery, RequestPayerEmail = PaymentOptionPresence.Optional, RequestPayerName = PaymentOptionPresence.Required, RequestPayerPhoneNumber = PaymentOptionPresence.None };

            var paymentRequest = new PaymentRequest(
                details,
                methods,
                merchantInfo,
                options);

            var res = await pm.SubmitPaymentRequestAsync(paymentRequest);

            UpdateUserStatus(res);
        }

        private async void UpdateUserStatus(PaymentRequestSubmitResult res)
        {
            if (res.Status == PaymentRequestStatus.Succeeded)
            {
                // Sleep just to pretend like we're processing the payment
                Thread.Sleep(2000);

                // Report that we successfully charged their card using on the payment token we were given
                await res.Response.CompleteAsync(PaymentRequestCompletionStatus.Succeeded);
            }
            else
            {
                MessageBox.Show("Payment failed, status: " + res.Status);
            }
        }

        private IReadOnlyList<PaymentShippingOption> CreateShippingOptions()
        {
            List<PaymentShippingOption> paymentShippingOptions = new List<PaymentShippingOption>();

            PaymentShippingOption shippingOption = new PaymentShippingOption("Standard", new PaymentCurrencyAmount("2.00", "USD"), false, "STANDARD");
            paymentShippingOptions.Add(shippingOption);

            shippingOption = new PaymentShippingOption("Two Day", new PaymentCurrencyAmount("5.99", "USD"), false, "TWODAY");
            paymentShippingOptions.Add(shippingOption);

            return paymentShippingOptions;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var item = dataGridView1.SelectedRows[0].DataBoundItem as DataRowView;
                selectedProduct = new Product()
                {
                    ProductID = int.Parse((string)item.Row.ItemArray[0]),
                    ProductName = (string)item.Row.ItemArray[1],
                    QuantityPerUnit = (string)item.Row.ItemArray[2],
                    UnitPrice = decimal.Parse((string)item.Row.ItemArray[3]),
                    UnitsInStock = int.Parse((string)item.Row.ItemArray[4])
                };
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
