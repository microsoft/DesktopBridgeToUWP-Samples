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
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Payments;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Step5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string appDataFilePath = null;
        Product selectedProduct = null;

        public MainPage()
        {
            this.InitializeComponent();
            string appDataDirectory = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + @"\DesktopBridge.Samples\";

            appDataFilePath = appDataDirectory + "datastore.xml";
            if (!File.Exists(appDataFilePath))
            {
                if (!Directory.Exists(appDataDirectory))
                {
                    Directory.CreateDirectory(appDataDirectory);
                }

                File.Copy("datastore.xml", appDataFilePath);
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ProductList products = new ProductList();
            StorageFile file = await StorageFile.GetFileFromPathAsync(appDataFilePath);
            XmlDocument doc = await XmlDocument.LoadFromFileAsync(file);
            XmlNodeList nodes = doc.GetElementsByTagName("Product");
            for (int i=0; i<nodes.Count; i++)
            {
                Product product = new Product();
                product.ProductID = int.Parse(nodes[i].SelectSingleNode("ProductID").InnerText.ToString());
                product.ProductName = nodes[i].SelectSingleNode("ProductName").InnerText.ToString();
                product.QuantityPerUnit = nodes[i].SelectSingleNode("QuantityPerUnit").InnerText.ToString();
                product.UnitPrice = decimal.Parse(nodes[i].SelectSingleNode("UnitPrice").InnerText.ToString());
                product.UnitsInStock = int.Parse(nodes[i].SelectSingleNode("UnitsInStock").InnerText.ToString());
                products.Add(product);
            }
            grid.DataContext = products;
        }


        public class Product : INotifyPropertyChanged
        {
            public int ProductID { get; set; }
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
       
        public async Task save(String path)
        {
            XmlDocument doc = new XmlDocument();
            var arrayOfProduct = doc.CreateElement("ArrayOfProduct");
            doc.AppendChild(arrayOfProduct);
            arrayOfProduct.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            arrayOfProduct.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");


            ProductList products = (ProductList)grid.DataContext;
            foreach (var product in products)
            {
                var productNode = doc.CreateElement("Product");
                var productIdNode = doc.CreateElement("ProductID");
                productIdNode.InnerText = product.ProductID.ToString();
                var productNameNode = doc.CreateElement("ProductName");
                productNameNode.InnerText = product.ProductName.ToString();
                var QuantityPerUnitNode = doc.CreateElement("QuantityPerUnit");
                QuantityPerUnitNode.InnerText = product.QuantityPerUnit.ToString();
                var UnitPriceNode = doc.CreateElement("UnitPrice");
                UnitPriceNode.InnerText = product.UnitPrice.ToString();
                var UnitsInStockNode = doc.CreateElement("UnitsInStock");
                UnitsInStockNode.InnerText = product.UnitsInStock.ToString();

                arrayOfProduct.AppendChild(productNode);
                productNode.AppendChild(productIdNode);
                productNode.AppendChild(productNameNode);
                productNode.AppendChild(QuantityPerUnitNode);
                productNode.AppendChild(UnitPriceNode);
                productNode.AppendChild(UnitsInStockNode);
            }

            StorageFile file = await StorageFile.GetFileFromPathAsync(path);
            await doc.SaveToFileAsync(file);
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

        private async void grid_SelectionChanged(object sender, Telerik.UI.Xaml.Controls.Grid.DataGridSelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count() > 0)
            {
                selectedProduct = e.AddedItems.First() as Product;
                await save(appDataFilePath);
            }

        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var pm = new PaymentMediator();
            PaymentDetails details = new PaymentDetails(new PaymentItem("Total", new PaymentCurrencyAmount(selectedProduct.UnitPrice.ToString(), "USD")), new List<PaymentItem>() { new PaymentItem(selectedProduct.ProductName, new PaymentCurrencyAmount(selectedProduct.UnitPrice.ToString(), "USD")) }) { ShippingOptions = CreateShippingOptions() };
            List<PaymentMethodData> methods = new List<PaymentMethodData>() { new PaymentMethodData(new List<String>() { "basic-card" }) };
            PaymentMerchantInfo merchantInfo = new PaymentMerchantInfo(new Uri("http://www.contoso.com"));
            PaymentOptions options = new PaymentOptions() { RequestShipping = true, ShippingType = PaymentShippingType.Delivery, RequestPayerEmail = PaymentOptionPresence.Optional, RequestPayerName = PaymentOptionPresence.Required, RequestPayerPhoneNumber = PaymentOptionPresence.None };

            var paymentRequest = new PaymentRequest(
                details,
                methods,
                merchantInfo,
                options);

            var result = await pm.SubmitPaymentRequestAsync(paymentRequest);

            UpdateUserStatus(result);
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

        private async void UpdateUserStatus(PaymentRequestSubmitResult result)
        {
            if (result.Status == PaymentRequestStatus.Succeeded)
            {
                // Simulate payment processing wait
                await Task.Delay(TimeSpan.FromSeconds(2));

                // Report that we successfully charged their card using on the payment token we were given
                await result.Response.CompleteAsync(PaymentRequestCompletionStatus.Succeeded);
            }
            else
            {
                await new MessageDialog("Payment failed, status: " + result.Status).ShowAsync();
            }
        }
    }
}
