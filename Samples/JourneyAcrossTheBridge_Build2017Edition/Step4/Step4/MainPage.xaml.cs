using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Payments;
using Windows.UI.Popups;
using Windows.Data.Json;
using System.Linq;
using Windows.Storage;
using Windows.Data.Xml.Dom;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Step4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        public ProductList Products { get; set; }
        ApplicationView currentView;
        Product selectedProduct = null;     

        public MainPage()
        {
            this.InitializeComponent();

            // Set up reference to this window so secondary windows can find it
            (App.Current as App).MainPageInstance = this;

            // Register to receive event when this window is closing
            currentView = ApplicationView.GetForCurrentView();
            currentView.Consolidated += CurrentView_Consolidated; ;

            // Register to receive event when connection to desktop server has been built
            (App.Current as App).AppServiceConnected += MainPage_AppServiceConnected; ;
        }

        private async void MainPage_AppServiceConnected(object sender, EventArgs e)
        {
            // App service is connected now so signal console app to send data
            await LoadProductsUsingBridge();
        }

        public async Task LoadProductsUsingBridge()
        {
            if ((App.Current as App).DatabaseServiceConnection != null)
            {
                Products = new ProductList();
                ValueSet valueSet = new ValueSet();
                valueSet.Add("request", "GetProducts");

                AppServiceResponse response = await (App.Current as App).DatabaseServiceConnection.SendMessageAsync(valueSet);
                if (response.Status == AppServiceResponseStatus.Success)
                {
                    var json = (string)(response.Message["response"]);
                    if (!string.IsNullOrEmpty(json))
                    {
                        Products.LoadFromJsonString(json);
                        (App.Current as App).Products = Products;
                        grid.DataContext = Products;
                    }
                }
                else
                {
                    Debug.WriteLine(response.Message);
                }
            }
        }

        public async Task SaveProductsUsingBridge()
        {
            if ((App.Current as App).DatabaseServiceConnection != null)
            {
                ValueSet valueSet = new ValueSet();
                valueSet.Add("request", "SaveProducts");
                StorageFile file = await Windows.Storage.ApplicationData.Current.TemporaryFolder.CreateFileAsync(@"\datastore.xml", CreationCollisionOption.OpenIfExists);
                await savetoXML(file);
            }
        }

        public async Task savetoXML(StorageFile file)
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
            await doc.SaveToFileAsync(file);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
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

        private void grid_SelectionChanged(object sender, Telerik.UI.Xaml.Controls.Grid.DataGridSelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count<object>() > 0)
            {
                selectedProduct = e.AddedItems.ElementAt(0) as Product;
            }
        }

        private async void CurrentView_Consolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        {
            // Since this main window is closing, close down the whole app
            if ((App.Current as App).DatabaseServiceConnection != null)
            {
                // Signal app service console app to close itself
                ValueSet valueSet = new ValueSet();
                valueSet.Add("request", "Exit");
                var ignored = await (App.Current as App).DatabaseServiceConnection.SendMessageAsync(valueSet);
            }
            Application.Current.Exit();
        }
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
        public bool LoadFromJsonString(string json)
        {
            JsonArray result = JsonArray.Parse(json);
            ProductList products = new ProductList();
            foreach (var jsonProd in result)
            {
                JsonObject jsonProdObject = jsonProd.GetObject();
                products.Add(new Product
                {
                    ProductID = Convert.ToInt32(jsonProdObject.GetNamedNumber("ProductID")),
                    ProductName = jsonProdObject.GetNamedString("ProductName"),
                    //CategoryName = jsonProdObject.GetNamedString("CategoryName"),
                    UnitPrice = (decimal)jsonProdObject.GetNamedNumber("UnitPrice"),
                    QuantityPerUnit = jsonProdObject.GetNamedString("QuantityPerUnit"),
                    UnitsInStock = Convert.ToInt16(jsonProdObject.GetNamedNumber("UnitsInStock"))
                }
                            );
            }
            foreach (Product p in products)
            {
                this.Items.Add(p);
            }
            return (Items.Count > 0);
        }
    }
}
